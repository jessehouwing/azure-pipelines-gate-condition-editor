param(
    [Parameter(Mandatory = $true)]
    [ValidateSet('Validate', 'Populate')]
    [string] $Mode,
    [Parameter(Mandatory = $true)]
    [string] $CacheDirectory,
    [string] $SourceDirectory
)

$ErrorActionPreference = 'Stop'
$dependencies = [ordered]@{
    'Tools\Microsoft.Identity.Client.dll' = @{
        Name = 'Microsoft.Identity.Client'
        PublicKeyToken = '0a613f4dd989e8ae'
    }
    'Tools\Microsoft.TeamFoundation.DistributedTask.Orchestration.Server.dll' = @{
        Name = 'Microsoft.TeamFoundation.DistributedTask.Orchestration.Server'
        PublicKeyToken = 'b03f5f7f11d50a3a'
    }
    'Tools\Microsoft.TeamFoundation.DistributedTask.WebApi.dll' = @{
        Name = 'Microsoft.TeamFoundation.DistributedTask.WebApi'
        PublicKeyToken = 'b03f5f7f11d50a3a'
    }
    'Tools\Microsoft.TeamFoundation.Framework.Server.dll' = @{
        Name = 'Microsoft.TeamFoundation.Framework.Server'
        PublicKeyToken = 'b03f5f7f11d50a3a'
    }
    'Tools\Microsoft.VisualStudio.Services.Common.dll' = @{
        Name = 'Microsoft.VisualStudio.Services.Common'
        PublicKeyToken = 'b03f5f7f11d50a3a'
    }
    'Tools\Microsoft.VisualStudio.Services.WebApi.dll' = @{
        Name = 'Microsoft.VisualStudio.Services.WebApi'
        PublicKeyToken = 'b03f5f7f11d50a3a'
    }
    'Tools\Newtonsoft.Json.dll' = @{
        Name = 'Newtonsoft.Json'
        PublicKeyToken = '30ad4fe6b2a6aeed'
    }
    'Tools\Plugins\Microsoft.TeamFoundation.DistributedTask.Orchestration.Server.Extensions.dll' = @{
        Name = 'Microsoft.TeamFoundation.DistributedTask.Orchestration.Server.Extensions'
        PublicKeyToken = 'b03f5f7f11d50a3a'
    }
}

function Assert-TrustedAssembly {
    param(
        [Parameter(Mandatory = $true)]
        [string] $Path,
        [Parameter(Mandatory = $true)]
        [string] $RelativePath
    )

    if (-not (Test-Path -LiteralPath $Path -PathType Leaf)) {
        throw "Required server dependency is missing: $RelativePath"
    }
    $signature = Get-AuthenticodeSignature -FilePath $Path
    if ($signature.Status -ne 'Valid' -or
        $signature.SignerCertificate.Subject -notmatch '(^|,\s*)O=Microsoft Corporation(,|$)') {
        throw "Server dependency has no valid Microsoft signature: $RelativePath"
    }
    $assembly = [Reflection.AssemblyName]::GetAssemblyName($Path)
    $expected = $dependencies[$RelativePath]
    $token = ($assembly.GetPublicKeyToken() | ForEach-Object { $_.ToString('x2') }) -join ''
    if ($assembly.Name -ne $expected.Name -or $token -ne $expected.PublicKeyToken) {
        throw "Server dependency identity mismatch: $RelativePath"
    }
}

if ($Mode -eq 'Populate') {
    if (-not $SourceDirectory) {
        throw 'SourceDirectory is required when populating the cache.'
    }
    $source = (Resolve-Path -LiteralPath $SourceDirectory).Path.TrimEnd('\')
    New-Item -ItemType Directory -Path $CacheDirectory -Force | Out-Null
    foreach ($relativePath in $dependencies.Keys) {
        $sourcePath = Join-Path $source $relativePath
        Assert-TrustedAssembly -Path $sourcePath -RelativePath $relativePath
        $destinationPath = Join-Path $CacheDirectory $relativePath
        New-Item -ItemType Directory -Path (Split-Path -Parent $destinationPath) -Force | Out-Null
        Copy-Item -LiteralPath $sourcePath -Destination $destinationPath
    }
}

function Get-CacheFilePaths {
    param(
        [Parameter(Mandatory = $true)]
        [string] $Directory,
        [string] $Prefix = ''
    )

    foreach ($entry in Get-ChildItem -LiteralPath $Directory -Force) {
        if ($entry.Attributes -band [IO.FileAttributes]::ReparsePoint) {
            throw "Unexpected reparse point in DLL cache: $($entry.Name)"
        }
        $relativePath = if ($Prefix) { Join-Path $Prefix $entry.Name } else { $entry.Name }
        if ($entry.PSIsContainer) {
            Get-CacheFilePaths -Directory $entry.FullName -Prefix $relativePath
        }
        else {
            $relativePath
        }
    }
}

$cache = (Resolve-Path -LiteralPath $CacheDirectory).Path.TrimEnd('\')
$actualPaths = @(Get-CacheFilePaths -Directory $cache)
$expectedPaths = @($dependencies.Keys)
$unexpectedPaths = @($actualPaths | Where-Object { $_ -notin $expectedPaths })
$missingPaths = @($expectedPaths | Where-Object { $_ -notin $actualPaths })
if ($unexpectedPaths.Count -gt 0 -or $missingPaths.Count -gt 0) {
    throw "Cached DLL set mismatch. Missing: $($missingPaths -join ', '); unexpected: $($unexpectedPaths -join ', ')."
}

foreach ($relativePath in $dependencies.Keys) {
    Assert-TrustedAssembly -Path (Join-Path $cache $relativePath) -RelativePath $relativePath
}

Write-Output "Validated $($expectedPaths.Count) Microsoft-signed Server DLLs under $cache."
