VERSION 5.00
Object = "{F0D2F211-CCB0-11D0-A316-00AA00688B10}#1.0#0"; "msdatlst.ocx"
Object = "{3B7C8863-D78F-101B-B9B5-04021C009402}#1.2#0"; "richtx32.ocx"
Begin VB.Form frmMain 
   BorderStyle     =   4  'Fixed ToolWindow
   Caption         =   "AutoText"
   ClientHeight    =   3120
   ClientLeft      =   3450
   ClientTop       =   7455
   ClientWidth     =   10605
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3120
   ScaleWidth      =   10605
   StartUpPosition =   2  'CenterScreen
   Begin VB.CheckBox chkPlainText 
      Caption         =   "Insert as plain text (unformatted)"
      Height          =   375
      Left            =   6000
      TabIndex        =   6
      Top             =   2220
      Width           =   2655
   End
   Begin VB.CommandButton cmdView 
      Height          =   300
      Left            =   7440
      Style           =   1  'Graphical
      TabIndex        =   5
      Top             =   0
      Width           =   375
   End
   Begin RichTextLib.RichTextBox rtb 
      Height          =   1815
      Left            =   0
      TabIndex        =   4
      Top             =   360
      Width           =   8655
      _ExtentX        =   15266
      _ExtentY        =   3201
      _Version        =   393217
      ScrollBars      =   2
      AutoVerbMenu    =   -1  'True
      TextRTF         =   $"frmMain.frx":0000
   End
   Begin MSDataListLib.DataCombo dcAutoText 
      Height          =   315
      Left            =   3360
      TabIndex        =   2
      Top             =   0
      Width           =   4095
      _ExtentX        =   7223
      _ExtentY        =   556
      _Version        =   393216
      Text            =   ""
   End
   Begin MSDataListLib.DataCombo dcCompany 
      Bindings        =   "frmMain.frx":0082
      Height          =   315
      Left            =   0
      TabIndex        =   1
      Top             =   0
      Width           =   1455
      _ExtentX        =   2566
      _ExtentY        =   556
      _Version        =   393216
      ListField       =   ""
      BoundColumn     =   ""
      Text            =   ""
      Object.DataMember      =   ""
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
   End
   Begin VB.CommandButton cmdInsert 
      Caption         =   "Insert"
      Height          =   300
      Left            =   7800
      TabIndex        =   0
      Top             =   0
      Width           =   855
   End
   Begin MSDataListLib.DataCombo dcCountries 
      Bindings        =   "frmMain.frx":00AC
      Height          =   315
      Left            =   1440
      TabIndex        =   3
      Top             =   0
      Width           =   1935
      _ExtentX        =   3413
      _ExtentY        =   556
      _Version        =   393216
      ListField       =   ""
      BoundColumn     =   ""
      Text            =   ""
      Object.DataMember      =   ""
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False

'AutoText fixes applied by George Adamson of SoftwareUnity Oct 2006:
'   - Removed Project References to:
'       - Microsoft Word 11.0 Object Library
'       - Microsoft Data Binding Collection VB 6.0 (SP4)
'       - Microsoft DAO 3.6 Object Library
'       - OLE Automation

Option Explicit

Private Declare Function OpenClipboard Lib "user32" (ByVal hwnd As Long) As Long
Private Declare Function RegisterClipboardFormat Lib "user32" Alias "RegisterClipboardFormatA" (ByVal lpString As String) As Long
Private Declare Function EmptyClipboard Lib "user32" () As Long
Private Declare Function CloseClipboard Lib "user32" () As Long
Private Declare Function SetClipboardData Lib "user32" (ByVal wFormat As Long, ByVal hMem As Long) As Long
Private Declare Function GlobalAlloc Lib "kernel32" (ByVal wFlags As Long, ByVal dwBytes As Long) As Long
Private Declare Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" (ByVal Destination As Long, ByVal Source As Any, ByVal Length As Long)
Private Declare Function GlobalUnlock Lib "kernel32" (ByVal hMem As Long) As Long
Private Declare Function GlobalLock Lib "kernel32" (ByVal hMem As Long) As Long
Private Declare Function GlobalFree Lib "kernel32" (ByVal hMem As Long) As Long

Private Const GMEM_DDESHARE = &H2000
Private Const GMEM_MOVEABLE = &H2

Private Declare Function SetWindowPos Lib "user32" _
  (ByVal hwnd As Long, ByVal _
  hWndInsertAfter As Long, ByVal x As Long, ByVal y As _
  Long, ByVal cx As Long, ByVal cy As Long, ByVal wFlags _
  As Long) As Long

' Set some constant values (from WIN32API.TXT).
Const conHwndTopmost = -1
Const conHwndNoTopmost = -2
Const conSwpNoActivate = &H10
Const conSwpShowWindow = &H40
Private Const ADODBconn As String = "Provider=SQLOLEDB;User ID=sa;Pwd=841241;Initial Catalog=Steppes;Data Source=SELSVR01;SERVER=SELSVR01;DATABASE=Steppes"
'Private Const ADODBconn As String = "Provider=SQLOLEDB;User ID=sa;Pwd=x;Initial Catalog=Steppes;Data Source=.;SERVER=.;DATABASE=Steppes"

Private Sub cmdInsert_Click()
Dim sRTF As String
Dim oWord As Object

    On Error Resume Next
    AppActivate "Microsoft Word"
    On Error GoTo Err_Hand
    
    sRTF = IIf(chkPlainText.Value, rtb.Text, rtb.TextRTF)

    'Copy the contents of the Rich Text to the clipboard
    Dim lSuccess    As Long
    Dim lRTF        As Long
    Dim hGlobal     As Long
    Dim lpString    As Long

    lSuccess = OpenClipboard(Me.hwnd)
    lRTF = RegisterClipboardFormat("Rich Text Format")
    lSuccess = EmptyClipboard
    hGlobal = GlobalAlloc(GMEM_MOVEABLE Or GMEM_DDESHARE, Len(sRTF))
    lpString = GlobalLock(hGlobal)
    CopyMemory lpString, sRTF, Len(sRTF)
    GlobalUnlock hGlobal
    SetClipboardData lRTF, hGlobal
    CloseClipboard
    GlobalFree hGlobal

    'Paste into a new Word document
    DoEvents
    Set oWord = GetObject(, "Word.Application")

    If Not Err Then
        oWord.Visible = True
        oWord.Activate
        If oWord.Documents.Count Then
            oWord.Selection.Paste
            oWord.Visible = True
        End If
    End If

    Set oWord = Nothing

Exit Sub

Err_Hand:
If Err.Number <> 0 Then
    If Err.Number = "429" Then  '429="ActiveX component can't create object"
        MsgBox ("You are trying to insert text into a document but Microsoft Word is not open." & vbCrLf & vbCrLf & "Please open a document and try again.")
    Else
        MsgBox ("Err Num:" & Err.Number & vbCr & "Source:" & Err.Source & vbCr & "Desc:" & Err.Description)
    End If
End If

End Sub

Private Sub cmdView_Click()

    On Error GoTo Err_Hand:
    
    'show or hide autotext preview box
    If rtb.Height < 1000 Then
        frmMain.Height = 3000
        rtb.Height = frmMain.Height - dcCompany.Height - chkPlainText.Height - 500
    Else
        rtb.Height = 0
        frmMain.Height = 690
    End If
    
    Exit Sub

Err_Hand:
    MsgBox ("Err Num:" & Err.Number & vbCr & "Source:" & Err.Source & vbCr & "Desc:" & Err.Description)
            
End Sub

Private Sub dcAutoText_Change()

On Error GoTo Err_Hand:

    If dcAutoText.BoundText = "" Then Exit Sub

    Dim rs      As New ADODB.Recordset
    Dim mstream As New ADODB.Stream
    Dim strSQL  As String
    Dim oConn   As New ADODB.Connection
    oConn.Open ADODBconn

    'sql query to get ntext column from section
    strSQL = "SELECT AutoText FROM AutoText WHERE AutoTextID = " & CStr(dcAutoText.BoundText)
    
    rs.Open strSQL, oConn, adOpenKeyset, adLockOptimistic

    'set stream object to ntext column in db and copy, then move to begining of text stream
    With mstream
        .Type = adTypeText
        .Open
        .WriteText rs.Fields("AutoText").Value, adWriteChar
        .Position = 0
    End With
    
    'write out the section text
    rtb.TextRTF = mstream.ReadText
              
    rs.Close
    mstream.Close
    Set rs = Nothing
    Set mstream = Nothing

Exit Sub

Err_Hand:
        MsgBox ("Err Num:" & Err.Number & vbCr & "Source:" & Err.Source & vbCr & "Desc:" & Err.Description)

End Sub


Private Sub dcCompany_Change()

On Error GoTo Err_Hand:

Dim oConn           As New ADODB.Connection
oConn.Open ADODBconn

Dim oRsCo           As New ADODB.Recordset
Dim intCompanyID    As Integer
Dim intFirstCountry As Integer

oRsCo.CursorLocation = adUseClient
intCompanyID = dcCompany.BoundText

'get list of countries that the selected company has entries for
If dcCompany.BoundText = 0 Then
    oConn.AutoTextGetCountryListAll oRsCo
Else
    oConn.AutoTextGetCountryList intCompanyID, oRsCo
End If


If oRsCo.EOF Then
 
    'set dropdowns to nothing as no entries returned
    Set dcCountries.RowSource = Nothing
    dcCountries.Refresh
    dcCountries.BoundText = ""
    Set dcAutoText.RowSource = Nothing
    dcAutoText.Refresh
    dcAutoText.BoundText = ""
    rtb.TextRTF = ""
    
Else

    'populate country dropdown
    intFirstCountry = oRsCo("CountryID")
    dcCountries.BoundColumn = "CountryID"
    dcCountries.ListField = "CountryName"
    Set dcCountries.RowSource = oRsCo
    dcCountries.Refresh
    dcCountries.BoundText = intFirstCountry

End If

Exit Sub

Err_Hand:
        MsgBox ("Err Num:" & Err.Number & vbCr & "Source:" & Err.Source & vbCr & "Desc:" & Err.Description)

End Sub


Private Sub dcCountries_Change()

'On Error GoTo Err_Hand:

If dcCountries.BoundText = "" Then Exit Sub

Dim oConn           As New ADODB.Connection
oConn.Open ADODBconn

Dim oRsCon          As New ADODB.Recordset
Dim intCountryID    As Integer
Dim intCompanyID    As Integer
Dim intFirstAutotext As Integer

oRsCon.CursorLocation = adUseClient
intCompanyID = dcCompany.BoundText
intCountryID = CInt(dcCountries.BoundText)

'get autotext entries names
If dcCompany.BoundText = 0 Then
    oConn.AutoTextGetAutoTextListAllCountries intCountryID, oRsCon
Else
    oConn.AutoTextGetAutoTextList intCompanyID, intCountryID, oRsCon
End If


If oRsCon.EOF Then
 
    'no db entries so clear dropdown
    Set dcAutoText.RowSource = Nothing
    dcAutoText.Refresh
    dcAutoText.BoundText = ""
    rtb.TextRTF = ""
    
Else

    'populate autotext entries names dropdown
    intFirstAutotext = oRsCon("AutotextID")
    dcAutoText.BoundColumn = "AutotextID"
    dcAutoText.ListField = "AutotextName"
    Set dcAutoText.RowSource = oRsCon
    dcAutoText.Refresh
    dcAutoText.BoundText = intFirstAutotext

End If

Exit Sub

Err_Hand:
        MsgBox ("Err Num:" & Err.Number & vbCr & "Source:" & Err.Source & vbCr & "Desc:" & Err.Description)

End Sub



Private Sub Form_Load()

On Error GoTo Err_Hand:

'set auttext application to always be the topmost window
SetWindowPos hwnd, conHwndTopmost, 0, 0, 0, 0, conSwpNoActivate Or conSwpShowWindow

rtb.Height = 0
frmMain.Height = 645
frmMain.Width = 8745

Dim oConn As New ADODB.Connection
Dim oRs   As New ADODB.Recordset

oConn.Open ADODBconn

oRs.CursorLocation = adUseClient
oRs.CursorType = adOpenStatic
oRs.LockType = adLockOptimistic

oConn.AutoTextGetCompanyList oRs

oRs.ActiveConnection = Nothing

'populate the company dropdown
dcCompany.BoundColumn = "CompanyID"
dcCompany.ListField = "CompanyName"
Set dcCompany.RowSource = oRs
dcCompany.BoundText = 0

Exit Sub

Err_Hand:
        MsgBox ("Err Num:" & Err.Number & vbCr & "Source:" & Err.Source & vbCr & "Desc:" & Err.Description)

End Sub
