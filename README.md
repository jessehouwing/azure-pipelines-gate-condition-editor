# Azure Pipelines Gate Condition Editor

Ever wondered what to put in the Success Criteria field?

![What to put here?](./what-here.png?raw=true "Screenshot")

Or queued 182 releases to debug the Success Criteria Condition? This little tool will help you locally evaluate your condition syntax in seconds instead of minutes or hours.

![Screenshot](./screenshot.png?raw=true "Screenshot")

# Learn more

[I wrote this little utility while debugging a condition for one of my own extensions and blogged about the experience](https://jessehouwing.net/vsts-release-create-complex-release-gate/).

# Prerequisites

To run you must have following prerequisites installed:

  * [Azure DevOps Server 2019 or later](https://visualstudio.microsoft.com/downloads/)
 
It suffices to install Azure DevOps Server on your workstation (Windows 10 x64). You do not need to configure Azure DevOps Server after installation, you won't need IIS to be enabled or SQL Server to be installed either. The installation is used to load the parser and functions. Since the assemblies required cannot be redistributed, this is the easiest way to ensure the utility can load the required dependencies.

The tool detects Server 2019, 2020, 2022 and 2025 (25H2). If multiple versions are installed, it uses the newest installation with the required assemblies. The server's `Tools` and `Tools\Plugins` directories supply the parser, extension functions and their dependencies.

## Building

Build the solution with Visual Studio MSBuild and a supported server installed. By default the newest registered installation is used. To select a registry version, set `AzureDevOpsServerVersion` to `17.0`, `18.0`, `19.0` or `20.0` (2019 through 2025 respectively). Alternatively, set `TfsInstallationDirectory` to the server installation root; this takes precedence over registry discovery and is also required for `dotnet msbuild`, which does not read the Windows registry. The selected build version does not change runtime selection.

For example:

```powershell
msbuild ServerTaskExpressionTester.sln /p:Configuration=Release /p:AzureDevOpsServerVersion=20.0
```

The expression smoke test runs against the locally installed server using Windows PowerShell:

```powershell
powershell -NoProfile -STA -File tests\ServerSmokeTest.ps1 -Executable ServerTaskExpressionTester\bin\Release\ServerTaskExpressionTester.exe
```

### GitHub Actions build

The `Build gate condition editor` workflow runs on a fresh `windows-2025-vs2026`
hosted runner with Visual Studio 2026 MSBuild 18.x for pushes, pull requests and
manual runs. It downloads the pinned
Azure DevOps Server Express 20.0.37104.1 installer, checks its SHA-256 and Microsoft
Authenticode signature, and installs the binaries with `/Silent /NoRestart`.
It does not run the configuration wizard or `TfsConfig`, provision a server,
or configure IIS, SQL Server or Search. MSBuild receives the registered installation
path explicitly and the expression smoke test uses those installed assemblies.

The installer comes from Microsoft's [official downloads page](https://learn.microsoft.com/azure/devops/server/download/azuredevopsserver)
(stable Express EXE, not the release candidate). Express is free for up to five
users/devices under Microsoft's [Azure DevOps Server license terms](https://visualstudio.microsoft.com/license-terms/azure-devops-server/);
this workflow uses one unconfigured instance on a disposable runner. Microsoft's
[installation documentation](https://learn.microsoft.com/azure/devops/server/install/unattended#download-and-install)
separates this binary-only installation from server configuration.
When updating the installer, update its versioned URL and SHA-256 together after
verifying its Microsoft signature.

The `gate-condition-editor` artifact contains only the executable, its configuration
and symbols, never the installer or server assemblies. The workflow also packages
the Marketplace extension and uploads the VSIX as a separate, short-lived
`gate-condition-editor-vsix` artifact. The eight-DLL Actions cache is disabled by
default and cannot run in a public repository: public-repository caches can be
accessed by fork pull requests, and permission for private storage alone does not
establish redistribution rights. For a private repository, enable it only after
establishing the applicable rights by setting `SERVER_DLL_CACHE_ENABLED` to `true`.
Otherwise the workflow installs the verified installer. DLLs are never uploaded as
workflow artifacts or included in the VSIX.

### GitHub Actions release

The `Release gate condition editor` workflow replaces the Azure Pipelines
Marketplace/GitHub release steps. It can be started manually from `main`; enter the
next `1.1.<patch>` version (greater than the latest GitHub release) and leave
`publish_marketplace` unchecked to package only. Publishing requires explicitly
checking that input and passing the `marketplace-production` GitHub Environment
gate. Packaging and publishing use the `azdo-marketplace` package, query-version,
publish, and wait-for-validation actions. The publish action uses GitHub OIDC
through Azure login; no PAT or direct TFX invocation is used.

A PowerShell step bundles `ServerTaskExpressionTester.exe` and its PDB into a ZIP.
The VSIX and that ZIP are attested with GitHub build provenance and verified before
publication. The release job attaches the same attested ZIP alongside the VSIX and
links both attestations from the GitHub Release. The release queries the current
Marketplace version and skips a duplicate publish when that version is already
present; in that case it downloads the published VSIX and requires its digest to
match the attested package. Before the Marketplace approval gate, the workflow
creates a GitHub draft containing the attested VSIX and tester ZIP. Retries reuse
and re-verify those exact draft assets rather than deleting or replacing them, so
they can recover even if a later build produces different bytes. After Marketplace
validation, the workflow publishes the draft once by changing it from draft to
published; an already-published release is verified and left unchanged. Enable
GitHub immutable releases in repository settings to prevent changes after that
final publication.

The `marketplace-production` environment is configured to allow deployments from
`main` and require the sole maintainer as reviewer. **Prevent self-review** is
disabled so the maintainer can approve their own release. The environment contains
the non-secret `AZURE_CLIENT_ID` and `AZURE_TENANT_ID` variables, and the Entra app
has a federated credential for this repository and environment. The app must remain
a member of the `jessehouwing` Marketplace publisher. Azure DevOps Pipeline workload
identity federation does not automatically grant GitHub Actions federation.

The legacy `azure-pipelines.yml` continues building and publishing its pipeline
artifact, but no longer publishes to the Marketplace or creates GitHub Releases.
No ORAS packaging is used.

Running the downloaded executable still requires a local Azure DevOps Server
installation as described above.
