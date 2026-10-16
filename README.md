# Computer 2

A Windows Store app (C# and XAML, Windows 8 and 8.1) that helps you choose a computer: build a desktop PC from compatible parts, or find the laptop, tablet or phone that matches the specs you pick. It is the second generation of [Computer](https://github.com/Mohammad-Diab/Computer), the VB.NET program that prices a PC from its parts. This repository also holds the VB.NET Ribbon drafts it started from, the tool that prepared its data, and the HTML and XAML prototypes of its pages.

## Features

The latest version (`v3.0.0`, the `Computer` app):

- Build a desktop PC part by part: the platform (AMD or Intel), a motherboard and a processor that fits its socket, RAM, graphic card, storage, case and power supply, and accessories, then a review of the build.
- View the specifications of processors, motherboards, graphic cards and storage devices, with pictures and brand logos.
- Find the laptop or tablet that matches the specs you choose, and compare devices side by side.
- An About page with the version, build date and credits.

The older versions (`v2.1.0` to `v3.0.0-beta.1`) show a device catalog by category (desktop, laptop, tablet, Mac, all-in-one and mobile) and a search for the devices that match the chosen specs, with "Don't care" for the specs that don't matter.

The specs come from text files, one per part or device (processors, motherboards, graphic cards, hard disks, laptops and tablets), in `Data\Computer`.

## Versions

| Tag | Version | Folder | Date |
|---|---|---|---|
| `v2.0.0-alpha.1` | Computer 2: WPF Ribbon draft (VB.NET), tabs and buttons only | `Ribbon draft (VB)` | 2012-01-28 |
| `v2.0.0-alpha.2` | Computer 2 Pro: the same draft with a lock image and a control theme | `Ribbon draft (VB)` | 2012-04-05 |
| `v2.0.0-alpha.3` | The Ribbon draft with a login form | `Ribbon draft (VB)` | 2012-07-01 |
| `v2.1.0` | Computer 2, the Windows Store app (split and items pages, search) | `Computer2.sln` | 2013-11-23 |
| `v2.2.0-alpha.1` | Computer 2 moved to a new Windows 8.1 project (unfinished) | `Computer 2.sln` | 2013-11-24 |
| `v3.0.0-beta.1` | Computer 2 rewritten as a Windows 8.1 Hub app | `Computer 2.sln` | 2014-02-23 |
| `v3.0.0` | Computer, a new Windows 8.1 app with pictures of the parts | `Computer.sln` | 2014-03-20 |

The Store app of each tag is at the repository root.

## Repository layout

| Folder | What it holds |
|---|---|
| `Computer.sln`, `Computer` | The app (latest version). |
| `Data\Computer` | The spec files the app reads (341 text files). |
| `Ribbon draft (VB)` | The first design of Computer 2: a WPF Ribbon window in VB.NET (.NET Framework 4.0), right to left, in Arabic. |
| `Tools\Do-it` | Development tool, not part of the Releases: a WPF tool (C#, .NET Framework 4.5) that reads the spec files of the processors, motherboards, graphic cards, hard disks and tablets and saves them as `.Mhmd` data files. Its input and output folders are fixed in the code (`C:\Users\User\Desktop\Computer` and `C:\Users\User\Desktop\Files`). |
| `Prototypes` | HTML applications (`.hta`) of the desktop, laptop and tablet pages, the first XAML pages and classes, the full and snapped views of the desktop page, and the code that wrote the HTML spec table of each part. Drafts only: no project builds them. |

## Requirements

Windows 10 version 1709 or later, or Windows 11, x64.

## Install

1. Download the `.7z` file of the version you want from [Releases](https://github.com/Mohammad-Diab/Computer-2/releases) and extract it.
2. Double-click `Install.cmd` in the extracted folder and accept the administrator prompt. The script adds the package's certificate to **Trusted People** on the PC, installs the framework packages from the `Dependencies` folder that are missing (.NET Native 2.2 and VCLibs 14.0, x64), then installs the signed app package. Developer Mode is not needed.
3. Start the app from the Start menu.

The latest version carries its spec files in the package and copies them to its data folder on the first start. The older versions read the spec files from their data folder only: their download has a `Data\Computer` folder, and `Install.cmd` copies it to `%LOCALAPPDATA%\Packages\<package family name>\RoamingState\Computer` after installing the app. To do it by hand, copy `Data\Computer` there before the first start.

The package is signed with a self-signed certificate, which is why the certificate is added to Trusted People first; only that step needs elevation, the app is installed for the current user. To remove the app, uninstall it from the Start menu.

## Building

```
git clone https://github.com/Mohammad-Diab/Computer-2.git
```

### The app

The project is a Windows 8.1 app, and the Windows 8.1 SDK no longer comes with Visual Studio, so it builds as a Universal Windows app: `Computer\Directory.Build.targets` adds the UWP package and a Windows 10 manifest with the same names and logos, with Mohammad Diab as publisher (`Computer\_uwp`). Needs Visual Studio (or its Build Tools) with the **Universal Windows Platform development** workload and the Windows SDK 10.0.22621.0:

```
set P=/p:Configuration=Release /p:Platform=x64 /p:TargetPlatformIdentifier=UAP /p:TargetPlatformVersion=10.0.22621.0 /p:TargetPlatformMinVersion=10.0.16299.0 /p:UseDotNetNativeToolchain=true
MSBuild Computer\Computer.csproj /t:Restore %P%
MSBuild Computer\Computer.csproj %P% /p:AppxBundle=Never /p:AppxPackageSigningEnabled=false /p:GenerateAppxPackageOnBuild=true /p:UapAppxPackageBuildMode=SideloadOnly /p:AppxPackageDir=<output>\
```

The package is written to `<output>`; Release builds compile with .NET Native and take a few minutes. The package carries `Data\Computer`; on the first start the app copies it to `Computer` in its roaming folder (`%LOCALAPPDATA%\Packages\<package family name>\RoamingState\Computer`) and reads it from there. The older versions don't carry the data: copy `Data\Computer` to that folder before their first start.

The older versions build the same way after checking out their tag: copy `Computer\Directory.Build.targets` and `Computer\_uwp` next to that version's `.csproj` and change the identity, names and logos in `_uwp\Package.appxmanifest` to the ones in its `Package.appxmanifest`, and add a 44x44 logo (`Assets\Square44x44Logo.png`), which a Windows 10 manifest needs.

### The Ribbon draft

The draft was made with Microsoft Ribbon for WPF, which is no longer available; `Ribbon draft (VB)\Directory.Build.targets` builds it on the Ribbon in .NET Framework 4.5 instead:

```
MSBuild "Ribbon draft (VB)\Computer  2.sln" /p:Configuration=Release /p:TargetFrameworkVersion=v4.5 /p:TargetFrameworkProfile= /p:TargetFrameworkRootPath=<net45>\build
```

The earlier drafts (`v2.0.0-alpha.1` and `v2.0.0-alpha.2`) build the same way after copying that file into `Ribbon draft (VB)`.

### Do it

```
MSBuild Tools\Do-it\WpfApplication1.sln /p:Configuration=Release /p:TargetFrameworkRootPath=<net45>\build
```

`<net45>` is the extracted [Microsoft.NETFramework.ReferenceAssemblies.net45](https://www.nuget.org/packages/Microsoft.NETFramework.ReferenceAssemblies.net45) package; recent Visual Studio versions don't ship the .NET Framework 4.5 reference assemblies. The exe is written to the project's `bin\Release`.

## Credits

- Specifications: collected by Mohammad Diab (2012 to 2013).
- Product pictures: from his specification files and the product pages he saved.
- Icons and app pictures: drawn for this app; their sources are in `Computer\_uwp\icon` and `Computer\_uwp\art`.
- Computer glyph in the logos and splash screens of `v2.1.0` to `v3.0.0-beta.1` (and in the splash draft `Computer\_uwp\art\xaml\SplashScreen.xaml`): based on "computer-symbolic" from the GNOME Symbolic Icon Theme (now part of the Adwaita icon theme) by the [GNOME Project](https://www.gnome.org), licensed under [CC BY-SA 3.0 US](https://creativecommons.org/licenses/by-sa/3.0/us/). Those images are under the same licence.
- Brand logos and product names belong to their owners.
- The memory, power supply, case, optical drive, monitor, mouse and keyboard options in the desktop builder are generic: they are not from the data.

## Author

Mohammad Diab

## License

[Apache 2.0](LICENSE), except the images based on the GNOME icon listed under Credits (CC BY-SA 3.0 US).
