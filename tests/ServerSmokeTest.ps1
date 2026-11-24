param(
    [Parameter(Mandatory = $true)]
    [string] $Executable,
    [string] $TfsInstallationDirectory
)

$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.Windows.Forms
$assembly = [Reflection.Assembly]::LoadFrom((Resolve-Path $Executable).Path)
$program = $assembly.GetType('ServerTaskExpressionTester.Program', $true)
$flags = [Reflection.BindingFlags]'Static,NonPublic'
if ($TfsInstallationDirectory) {
    $serverPath = (Resolve-Path -LiteralPath $TfsInstallationDirectory).Path.TrimEnd('\')
    foreach ($assemblyPath in @(
        'Tools\Microsoft.TeamFoundation.DistributedTask.WebApi.dll',
        'Tools\Plugins\Microsoft.TeamFoundation.DistributedTask.Orchestration.Server.Extensions.dll'
    )) {
        if (-not (Test-Path -LiteralPath (Join-Path $serverPath $assemblyPath) -PathType Leaf)) {
            throw "The selected server directory is missing $assemblyPath."
        }
    }
}
else {
    $serverPath = $program.GetMethod('FindServerInstallation', $flags).Invoke($null, @())
    if (-not $serverPath) {
        throw 'No supported Azure DevOps Server installation was found.'
    }
}
$program.GetField('serverPath', $flags).SetValue($null, $serverPath)
$resolver = [Delegate]::CreateDelegate([ResolveEventHandler], $program.GetMethod('CurrentDomain_AssemblyResolve', $flags))
[AppDomain]::CurrentDomain.add_AssemblyResolve($resolver)

try {
    $wrongToken = 'Microsoft.TeamFoundation.DistributedTask.WebApi, Version=20.0.0.0, Culture=neutral, PublicKeyToken=0011223344556677'
    if ($resolver.Invoke($null, [ResolveEventArgs]::new($wrongToken))) {
        throw 'An assembly with a different public key token was accepted.'
    }

    $form = [Activator]::CreateInstance($assembly.GetType('ServerTaskExpressionTester.Form1', $true))
    $formType = $form.GetType()
    $instanceFlags = [Reflection.BindingFlags]'Instance,NonPublic'
    $response = $formType.GetField('responseEditor', $instanceFlags).GetValue($form)
    $expression = $formType.GetField('expressionEditor', $instanceFlags).GetValue($form)
    $status = $formType.GetField('Status', $instanceFlags).GetValue($form)
    $log = $formType.GetField('Llog', $instanceFlags).GetValue($form)
    $response.Text = '{"name":"example"}'

    foreach ($case in @(
        @{ Expression = "eq(length('abc'), 3)"; Expected = 'Success' },
        @{ Expression = "eq(length('abc'), 2)"; Expected = 'Failed' },
        @{ Expression = "eq(count(jsonpath('$.name')), 1)"; Expected = 'Success' },
        @{ Expression = "eq(count(split('a,b', ',')), 2)"; Expected = 'Success' },
        @{ Expression = "isNullOrEmpty('')"; Expected = 'Success' }
    )) {
        $expression.Text = $case.Expression
        $formType.GetMethod('Evaluate', $instanceFlags).Invoke($form, @())
        if ($status.Text -ne $case.Expected) {
            throw "Expression '$($case.Expression)': expected $($case.Expected), got $($status.Text). $($log.Text)"
        }
    }

    $plugin = [AppDomain]::CurrentDomain.GetAssemblies() |
        Where-Object { $_.GetName().Name -eq 'Microsoft.TeamFoundation.DistributedTask.Orchestration.Server.Extensions' }
    $pluginPath = Join-Path $serverPath 'Tools\Plugins\Microsoft.TeamFoundation.DistributedTask.Orchestration.Server.Extensions.dll'
    if (-not $plugin -or
        (Get-FileHash -LiteralPath $plugin.Location -Algorithm SHA256).Hash -ne
        (Get-FileHash -LiteralPath $pluginPath -Algorithm SHA256).Hash) {
        throw 'The server extensions assembly was not loaded from the selected installation.'
    }
    $authPath = Join-Path $serverPath 'Tools\Microsoft.Identity.Client.dll'
    if (Test-Path $authPath) {
        $authName = [Reflection.AssemblyName]::GetAssemblyName($authPath)
        $olderAuthName = [Reflection.AssemblyName]::new($authName.FullName)
        $olderAuthName.Version = [Version]'1.0.0.0'
        $resolvedAuth = $resolver.Invoke($null, [ResolveEventArgs]::new($olderAuthName.FullName))
        if (-not $resolvedAuth -or
            (Get-FileHash -LiteralPath $resolvedAuth.Location -Algorithm SHA256).Hash -ne
            (Get-FileHash -LiteralPath $authPath -Algorithm SHA256).Hash) {
            throw 'The authentication assembly was not resolved from the selected installation.'
        }
        $authAssembly = [Reflection.Assembly]::Load($authName)
        if ((Get-FileHash -LiteralPath $authAssembly.Location -Algorithm SHA256).Hash -ne
            (Get-FileHash -LiteralPath $authPath -Algorithm SHA256).Hash) {
            throw 'The authentication assembly was not loaded from the selected installation.'
        }
    }
    Write-Output "Server expression smoke tests passed: $serverPath"
}
finally {
    if ($form) { $form.Dispose() }
    [AppDomain]::CurrentDomain.remove_AssemblyResolve($resolver)
}
