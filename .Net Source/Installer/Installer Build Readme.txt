Steppes AutoText Installer Build
Nick Casey <nickoil@hotmail.com> 2011-10-15

Required Files
--------------

1) Microsoft C# 2010 Express http://www.microsoft.com/visualstudio/en-us/products/2010-editions/visual-csharp-express
2) Wix (latest version currently 3.5) http://wix.sourceforge.net/index.html
3) AutoText.exe.config - the config file containing the correct database connection details
4) TSDatabase.dll - a database connection library
5) AutoText_BuildInstaller.bat - batch file for running the entire build process
6) AutoText.wxs - WiX configuration file to build the msi to specification
7) WixVersionManager.vbs - updates the version in the wxs file so that the new msi will always overwrite the old
8) MSBuild.exe - for recompiling autotext.exe and creating bootstrapping setup.exe (makes sure that .Net framework and the Windows Installer are installed). 
9) AutoText_BootstrapConfig.exe - Configuration file for gathering bootstrap files and building setup.exe
10) AutoText_Setup.SED - Config for creating self-extracting document (collating setup and msi into single file).



Build A Distribution File
--------------------------

a) Make sure required files are present and latest version of WiX is installed
b) Edit AutoText_BuildInstaller.bat so that executables run from the correct WiX bin folder
c) Edit AutoText.exe.config so that it contains the correct connection string for the live database
c) Run AutoText_BuildInstaller.bat to build MSI files. This will:
	- rebuild the autotext executable, copying AutoText.exe into the Installer folder
	- save the previous WiX config file (.wxs) with a timestamp extension
	- change the version in the WiX config file so that it will overwrite previously installed versions
	- compile and link to build the autotext.msi installer creating AutoText.wixobj and AutoText.wixpdb as intermediate files
	- create the bootstrap setup.exe to check that prerequisites, Windows Installer and .Net 4 Client Profile Framework are installed
	- create a self-extracting exe containing setup.exe and autotext.msi. Setup.exe will run automatically on extraction.

