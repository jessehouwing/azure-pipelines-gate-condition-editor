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
 
It suffices to simpy install Azure DevOps Server on your workstation (Windows 10 x64). You do not need to configure Azure DevOps Server after installation, you won't need IIS to be enabled or SQL Server to be installed either. The installation is used to load the parser and functions. Since the assemblies required cannot be redistributed, this is the easiest way to ensure the utility can load the required dependencies.
