 @ECHO OFF
 
rem Builds the msi file for release
rem Nick Casey <nickoil@hotmail.com> 2011-10-15 for Steppes Travel
 @ECHO Building AutoText installation files...
 @ECHO.
 
rem Delete previously built files so that we check their existance and error report 
 DEL AutoText_Setup.exe
 DEL setup.exe
 DEL AutoText.msi
 DEL AutoText.wixpdb
 DEL AutoText.wixobj
 DEL AutoText.exe
 
rem Get the OS paths depending on x86/x64 
 @ECHO Getting OS paths...
 @ECHO.
 
 SET ProgramFilesPath=%ProgramFiles%
 SET SystemPath=%SYSTEMROOT%\System32
 
 IF NOT EXIST "%ProgramFiles(x86)%" GOTO CONTINUE
     SET ProgramFilesPath=%ProgramFiles(x86)%
     SET SystemPath=%SYSTEMROOT%\SysWOW64
 
 :CONTINUE
 
 @ECHO Program files folder: %ProgramFilesPath%
 @ECHO System folder: %SystemPath%
 @ECHO.
 
rem Get a timestamp with which to rename the old wxs file
rem Parse the date (e.g., Fri 02/08/2008)
 SET cur_yyyy=%date:~6,4%
 SET cur_mm=%date:~3,2%
 SET cur_dd=%date:~0,2%
 
rem Parse the time (e.g., 11:17:13.49)
 SET cur_hh=%time:~0,2%
 IF %cur_hh% lss 10 (SET cur_hh=0%time:~1,1%)
 SET cur_nn=%time:~3,2%
 SET cur_ss=%time:~6,2%
 SET cur_ms=%time:~9,2%
 
 SET timestamp=%cur_yyyy%%cur_mm%%cur_dd%-%cur_hh%%cur_nn%%cur_ss%%cur_ms% 
 
rem Rebuild the exe
rem This automatically copies the exe to the installation folder

 @ECHO Rebuilding the AutoText executable...
 @ECHO.
 MSBUILD "..\AutoText\AutoText.sln" /p:Configuration=Release /t:rebuild 
 
 IF ERRORLEVEL 1 GOTO FAILED_BUILDEXE  

rem Update the version number, first saving the original with a timestamp
 IF EXIST tempfile DEL tempfile
 @ECHO.
 @ECHO Renaming current AutoText.wxs to AutoText.wxs.%timestamp% ...
 COPY AutoText.wxs AutoText.wxs.%timestamp% 
 CSCRIPT /nologo WiXVersionManager.vbs "AutoText.wxs" > tempfile
 
rem If there was an error with the version update, the AutoText.wxs insnt filled out properly
 FOR /F "usebackq" %%A IN ('tempfile') DO set size=%%~zA
 IF %size% LSS 2000 GOTO FAILED_INITWXSFILE
 
 DEL AutoText.wxs 
 @ECHO.
 @ECHO Creating AutoText.wxs with new version number ...
 REN tempfile AutoText.wxs
 @ECHO.

rem Build the msi file
 @ECHO Building the msi installation file ...
 @ECHO.
 
 "%ProgramFilesPath%\Windows Installer XML v3.5\BIN\candle.exe" -ext WiXNetFxExtension AutoText.wxs -out AutoText.wixobj
 "%ProgramFilesPath%\Windows Installer XML v3.5\BIN\light.exe" -ext WixUIExtension -cultures:en-us -ext WiXNetFxExtension AutoText.wixobj -out AutoText.msi
 
 IF NOT EXIST AutoText.msi GOTO FAILED_MSIBUILD
 
rem Build the bootstrapper 
 @ECHO Building the bootstrapper ...
 @ECHO.
 MSBUILD autotext_bootstrapconfig.xml /target:Bootstrapper
 
 IF NOT EXIST Setup.exe GOTO FAILED_BOOTSTRAPPERBUILD
 
rem Compress the msi and setup into a single self extracting doc
rem Must use the 32 version of IExpress not the 64 bit one else the 
rem SED wont extract on 32 bit OSes. Frustratingly 64 bit is default on
rem 64 bit OSes so need to force 32 bit
 @ECHO Building the self extracting document ...
 @ECHO.

 %SystemPath%\IExpress /N AutoText_Setup.SED
 
 IF NOT EXIST AutoText_Setup.exe GOTO FAILED_SEDBUILD

 @ECHO.
 @ECHO AutoText installation build completed
 @ECHO.
 @ECHO Please distribute AutoText_Setup.exe
 
 GOTO END
 
 
 
 
rem Error Handlers
 
 
 :FAILED_BUILDEXE
 
 @ECHO.
 @ECHO *** Failed to build AutotText.exe. Exiting installer build. ***
 GOTO END 
 
 :FAILED_INITWXSFILE
 
 @ECHO.
 @ECHO *** Version updating failed. Exiting installer build. ***
 GOTO END 
 
 :FAILED_MSIBUILD
 @ECHO.
 @ECHO *** Failed to build AutoText.msi. Exiting installer build. ***
 GOTO END 
 
 :FAILED_BOOTSTRAPPERBUILD
 @ECHO.
 @ECHO *** Failed to build setup.exe bootstrapper. Exiting installer build. ***
 GOTO END 
 
 :FAILED_SEDBUILD
  @ECHO.
  @ECHO *** Failed to build AutoText_Setup.exe self-extracting exe. Exiting installer build. ***
 GOTO END 
 
 :END