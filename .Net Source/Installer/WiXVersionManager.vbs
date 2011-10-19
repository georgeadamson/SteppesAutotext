REM Updates the version number in the WiX system file
REM Nick Casey <nickoil@hotmail.com> 2011-10-15 for Steppes Travel

REM Echos new file - be careful not to echo anything but the file lines

args = WScript.Arguments.Count
 
If args < 1 Then
   WScript.Quit
End If

wxsFileName = WScript.Arguments(0)

Set objFS = CreateObject("Scripting.FileSystemObject")
Set objFile = objFS.OpenTextFile(wxsFileName)

Set re = new RegExp  

re.Pattern = "<\?define ProductVersion=""(\d+)\.(\d+)\.(\d+)"" \?>"
re.IgnoreCase = true

Do Until objFile.AtEndOfStream

	fileLine = objFile.readLine

	Set matches = re.Execute(fileLine)
	If matches.Count > 0 Then

		Set productLine = matches(0)

		If productLine.SubMatches.Count > 0 Then

			fileLine = "<?define ProductVersion=""" & _
									productLine.SubMatches(0) & "." & _
									productLine.SubMatches(1) & "." & _
									CStr(CInt(productLine.SubMatches(2)) + 1) & _
									""" ?>  <!-- NMC This line is auto edited by the batch file DO NOT CHANGE IT MANUALLY -->"
		End If

	End If
	
	wscript.echo fileline
	
Loop


objFile.Close()