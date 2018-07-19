# EasyAppxInstall
C# console app that enables simple, easy, seamless installation of sideloaded UWP apps 

## Where to get the program:
You can get the program from here [EasyAppInstall Releases](https://github.com/colinkiama/EasyAppInstall/releases)

# Requirements:
- Windows 10 Anniversary Update (Build 14393)
- Sideload Mode or Developer Mode enabled. (Settings > Update & Security > For developers > Select "Sideload apps")

## How to use the program:
### 1. Using Windows Explorer
Start "EasyAppxInstall.exe" in the directory where app package you want to install is.
The program will try to install a certificate first. Either a cerificate file that is in the directory. If none exist, it will look for a .appxbundle file then .appx file before giving up.

Then, it tries to install the app package (.appx/.appxbundle) while trying to install dependencies located in dependencies folders

### 2. Using a command line program (e.g Command Prompt):
You can run the app with a path to a directory or file as a parameter.

#### Usage Examples: 

Install package (will also try to install dependencies if included in a default app package output layout) from current directory (requires path installation):
```
C:\PackageInstallDirectory\>EasyAppInsall
```

Install app package from its path (no dependencies):
```
// EasyAppInstall.exe [App Package Path]
C:\>EasyAppInsall.exe C:\AppPackages\MyApp.appxbundle
```

Install app package with chosen depednency path:
```
// EasyAppInstall.exe [App Package Path] [Dependencies Directory*]
// *Note: Please ensure the directory is contains dependencies for you device's architecure (x86, x64, arm, arm64 etc.)
C:\>EasyAppInsall.exe C:\AppPackages\MyApp.appxbundle C:\Dependencies
```



### For heavy command line users:
You can follow this tutorial to add the folder where this progam is contained to the Path environment variable: https://helpdeskgeek.com/windows-10/add-windows-path-environment-variable/

After you do this, you will no longer need to navigate to where the program is installed to open in. You will be able to simply use "easyappinstall" from anywhere in the command line!

## Help:
For any questions or if you want to speak to me about anything, email me here: colinkiama@hotmail.co.uk
