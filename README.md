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
