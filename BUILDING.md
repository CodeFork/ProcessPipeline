# Building Asmichi.ProcessPipeline

## Environment

- Windows 10 1803
    - Visual Studio 2017
        - Managed Desktop Development Workload
        - .NET Framework SDKs and Targeting Packs: 4.6.1, 4.7.1
        - .NET Core 2.1 Development Tool
    - nuget.exe 4.7.1 or later

- WSL Ubuntu 18.04
    - `apt-get install make gcc`
    - .NET Core SDK 2.1
        - See https://www.microsoft.com/net/download/linux-package-manager/ubuntu18-04/sdk-current for installation instructions.

## Writing and Testing code

### Windows Version

Just open src/ProcessPipeline.sln.

### Linux Version

For the Linux version, (TBD)

## Building Package

```powershell
.\build\BuildPackages.ps1
```
