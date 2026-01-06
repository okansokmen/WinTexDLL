Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Threading
Imports System.Drawing
Imports System.Windows.Forms
Imports System.IO
Imports Microsoft.SqlServer.Server
Imports Microsoft.VisualBasic.FileIO.FileSystem

Module UtilRoot

    Public lGlobalDebugMode As Boolean = False

    Public Const G_NumberFormat = "###,###,###,###,###,##0"
    Public Const G_Number1Format = "###,###,###,###,###,##0.0"
    Public Const G_Number2Format = "###,###,###,###,###,##0.00"
    Public Const G_Number3Format = "###,###,###,###,###,##0.000"
    Public Const G_Number4Format = "###,###,###,###,###,##0.0000"
    Public Const G_Number5Format = "###,###,###,###,###,##0.00000"
    Public Const G_Number6Format = "###,###,###,###,###,##0.000000"

    Public Gl_Personel As String = ""
    Public Gl_UserName As String = ""
    Public Gl_ActivePass As String = ""
    Public GL_PersonelFaceTemplate As String = ""
    Public GL_PersonelResim As String = ""
    Public GL_Similarity As Double = 0
    Public G_DXDBCounter As Integer = 0

    ' global definitions 
    Public Structure oExRate
        Dim cCinsi As String
        Dim cKisaltma As String
        Dim nAlis As Double
        Dim nSatis As Double
        Dim nEfektifAlis As Double
        Dim nEfektifSatis As Double
    End Structure

    Public Structure Active
        Dim Camera As DirectX.Capture.Filter
        Dim CaptureInfo As DirectX.Capture.Capture
        Dim ConfWindow As frmCameraConfiguration
        Dim PathVideo As String
    End Structure

    Public CaptureInformation As Active
    Public Dispositivos As Object

    Public Structure oCameraInfo
        Dim nCameraNo As Integer
        Dim nVideoCompressor As Integer
        Dim nWidth As Integer
        Dim nHeight As Integer
        Dim nFrameRate As Double
        Dim cFileName As String
    End Structure

    Public oCamera As oCameraInfo

    Public Structure oSQLConn

        Dim cOwner As String

        Dim cServer As String
        Dim cDatabase As String
        Dim cUser As String
        Dim cPassword As String
        Dim cConnStr As String

        Dim cWinTexUser As String
        Dim cPersonel As String

        Dim nPersonelUyumID As Double
        Dim cUyumUsername As String
        Dim cUyumPassword As String

        Dim cAbitUser As String
        Dim cAbitPassword As String

        Dim lWinTexDllLog As Boolean
        Dim lWinTexDllError As Boolean

        Dim cYKKApiUrl As String
        Dim cYKKApiTestUrl As String
        Dim cYKKApiUserName As String
        Dim cYKKApiPassword As String
        Dim cYKKFirma As String
    End Structure

    Public oConnection As oSQLConn

    Public Structure oStimulusReport
        Dim cReportID As String
        Dim cReportName As String
        Dim cReportClass As String
        Dim cReportSQL As String
        Dim cReportVariable1 As String
        Dim cReportVariable2 As String
        Dim cReportVariable3 As String
        Dim cReportVariable4 As String
        Dim cReportVariable5 As String
        Dim cReportVariable6 As String
        Dim cReportVariable7 As String
        Dim cReportVariable8 As String
        Dim cReportVariable9 As String
        Dim cReportVariable10 As String
        Dim cReport As String
        Dim cPrinterName As String
        Dim nCopies As Double
        Dim cDigerDiller As String
        Dim oReportStimulus As Stimulsoft.Report.StiReport
    End Structure

    Public oReport As oStimulusReport

    Public Structure oDevXReport
        Dim cReportID As String
        Dim cReportName As String
        Dim cReportClass As String
        Dim cReportSQL As String

        Dim cReportQueryName1 As String
        Dim cReportQueryName2 As String
        Dim cReportQueryName3 As String
        Dim cReportQueryName4 As String
        Dim cReportQueryName5 As String
        Dim cReportQueryName6 As String
        Dim cReportQueryName7 As String
        Dim cReportQueryName8 As String
        Dim cReportQueryName9 As String
        Dim cReportQueryName10 As String

        Dim cReportSQL1 As String
        Dim cReportSQL2 As String
        Dim cReportSQL3 As String
        Dim cReportSQL4 As String
        Dim cReportSQL5 As String
        Dim cReportSQL6 As String
        Dim cReportSQL7 As String
        Dim cReportSQL8 As String
        Dim cReportSQL9 As String
        Dim cReportSQL10 As String
        ' cReportVariable komple yeni SQL query oldu
        Dim cReportVariable1 As String
        Dim cReportVariable2 As String
        Dim cReportVariable3 As String
        Dim cReportVariable4 As String
        Dim cReportVariable5 As String
        Dim cReportVariable6 As String
        Dim cReportVariable7 As String
        Dim cReportVariable8 As String
        Dim cReportVariable9 As String
        Dim cReportVariable10 As String

        Dim cReport As String
        Dim cPrinterName As String
        Dim nCopies As Double
        Dim oReportDeveloperExpress As DevExpress.XtraReports.UI.XtraReport
        Dim cReportViewName As String
    End Structure

    Public oReportDevX As oDevXReport

    Public cRootDir As String = ""
    Public nGlobalReportParameter As Integer

    Public Sub GetSysDefaults()

        Dim ConnYage As SqlClient.SqlConnection

        Try
            ConnYage = OpenConn()
            cRootDir = GetSysParConnected("RootDir", ConnYage)
            CloseConn(ConnYage)

        Catch ex As Exception
            ErrDisp("GetSysDefaults : " + ex.Message.Trim, "utilroot")
        End Try
    End Sub

    Public Sub DeleteReport(ByVal cReportid As String)

        If cReportid.Trim = "" Then Exit Sub

        ExecuteSQLCommand("delete stireports where reportid = " + cReportid)
    End Sub

    Public Sub DestroyFile(ByVal cFileName As String)

        Try
            If cFileName.Trim = "" Then Exit Sub
            If My.Computer.FileSystem.FileExists(cFileName.Trim) Then
                My.Computer.FileSystem.DeleteFile(cFileName.Trim)
            End If

        Catch ex As Exception
            ErrDisp("DestroyFile : " + ex.Message, "utilroot")
        End Try
    End Sub

    Public Function MoveRenameFile(ByVal cSubsystem As String, ByVal cFileName As String, ByVal cConvertedName As String) As String

        Dim cAppDir As String = cRootDir ' System.AppDomain.CurrentDomain.BaseDirectory
        Dim cNewFileName As String = ""
        Dim cExtension As String = ""
        Dim cNewDir As String = ""
        Dim cNewThumbFileName As String = ""
        Dim oFile As System.IO.FileInfo
        Dim oBitMap As Image

        MoveRenameFile = ""

        Try
            If cSubsystem.Trim = "" Or cFileName.Trim = "" Or cConvertedName.Trim = "" Then Exit Function

            cNewDir = cAppDir + cSubsystem
            oFile = My.Computer.FileSystem.GetFileInfo(cFileName)
            cExtension = oFile.Extension
            cNewFileName = cNewDir + "\" + cConvertedName + IIf(cExtension = "", "", "." + cExtension).ToString
            cNewThumbFileName = cNewDir + "\Thumbs\" + cConvertedName + IIf(cExtension = "", "", "." + cExtension).ToString
            cNewFileName = cNewFileName.Replace("..", ".")
            cNewThumbFileName = cNewThumbFileName.Replace("..", ".")
            If Not My.Computer.FileSystem.DirectoryExists(cNewDir) Then
                My.Computer.FileSystem.CreateDirectory(cNewDir)
            End If
            If Not My.Computer.FileSystem.DirectoryExists(cNewDir + "\Thumbs") Then
                My.Computer.FileSystem.CreateDirectory(cNewDir + "\Thumbs")
            End If

            My.Computer.FileSystem.CopyFile(cFileName, cNewFileName, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            MoveRenameFile = cNewFileName

            oBitMap = New Bitmap(Image.FromFile(cFileName), New Size(90, 60))
            oBitMap.Save(cNewThumbFileName)

        Catch ex As Exception
            ErrDisp("StorePictures : " + ex.Message, "utilroot")
        End Try
    End Function

    Public Sub ConvertPictures(ByVal cSubsystem As String)
        ' convert pictures to thumbs
        Dim cAppDir As String = cRootDir ' System.AppDomain.CurrentDomain.BaseDirectory
        Dim aFiles(0) As String
        Dim nCnt As Integer
        Dim oFile As System.IO.FileInfo
        Dim cFileName As String = ""
        Dim cNewDir As String = ""
        Dim cDir As String = ""
        Dim cThumbFile As String = ""
        Dim nFiles As Integer
        Dim oBitMap As Image

        Try
            cNewDir = cAppDir + cSubsystem
            If Not My.Computer.FileSystem.DirectoryExists(cNewDir + "\Thumbs") Then
                My.Computer.FileSystem.CreateDirectory(cNewDir + "\Thumbs")
            End If
            nFiles = My.Computer.FileSystem.GetFiles(cNewDir).Count
            If nFiles > 0 Then
                ReDim aFiles(nFiles - 1)
                My.Computer.FileSystem.GetFiles(cNewDir).CopyTo(aFiles, 0)
                For nCnt = 0 To UBound(aFiles)
                    oFile = My.Computer.FileSystem.GetFileInfo(aFiles(nCnt))
                    cFileName = oFile.Name
                    cDir = oFile.DirectoryName
                    cThumbFile = cDir + "\Thumbs\" + cFileName

                    If Not My.Computer.FileSystem.FileExists(cThumbFile) Then
                        oBitMap = New Bitmap(Image.FromFile(aFiles(nCnt)), New Size(90, 60))
                        oBitMap.Save(cThumbFile)
                    End If
                Next
            End If

        Catch ex As Exception
            ErrDisp("ConvertPictures : " + ex.Message, "utilroot")
        End Try
    End Sub

    Public Function GetThumbFile(ByVal cFileName As String) As String

        Dim oFile As System.IO.FileInfo
        Dim cDir As String = ""

        GetThumbFile = ""
        Try
            oFile = My.Computer.FileSystem.GetFileInfo(cFileName)
            GetThumbFile = oFile.Name
            cDir = oFile.DirectoryName
            GetThumbFile = cDir + "\Thumbs\" + GetThumbFile
            If Not My.Computer.FileSystem.FileExists(GetThumbFile) Then
                GetThumbFile = cFileName
            End If

        Catch ex As Exception
            ErrDisp("GetThumbFile : " + ex.Message, "utilroot")
        End Try
    End Function

    Public Function Confirmed(ByVal cMessage As String, ByVal oForm As System.Windows.Forms.IWin32Window) As Boolean

        Dim ds As DialogResult

        Confirmed = False

        ds = MessageBox.Show(oForm, cMessage, "WARNING", MessageBoxButtons.YesNo)
        If ds = System.Windows.Forms.DialogResult.Yes Then
            Confirmed = True
        End If
    End Function

    Public Function Confirmed2(ByVal cMessage As String) As Boolean

        Dim ds As DialogResult

        Confirmed2 = False

        ds = MessageBox.Show(cMessage, "Warning")
        If ds = System.Windows.Forms.DialogResult.Yes Then
            Confirmed2 = True
        End If
    End Function


    Public Function MergeImages(ByVal Pic1 As Image, ByVal pic2 As Image) As Image

        Dim MergedImage As Image ' This will be the finished merged image

        Dim Wide, High As Integer ' Size of merged image
        ' Calculate Width and Height needed for composite image
        ' First, the Width:
        Wide = Pic1.Width + pic2.Width

        ' Height: Ensure that the new image is high enough for both images
        ' that we plan to place inside it.
        If Pic1.Height >= pic2.Height Then
            High = Pic1.Height
        Else
            High = pic2.Height
        End If

        ' Create an empty Bitmap the correct size to hold both images side by side
        Dim bm As New Bitmap(Wide, High)
        ' Get the Graphics object for this bitmap
        Dim gr As Graphics = Graphics.FromImage(bm)

        ' Draw a black line round the outside (optional, but sometimes looks better when printed)
        gr.DrawRectangle(Pens.Black, 0, 0, Wide - 1, High - 1)
        ' Draw the first source image at left side of new image
        gr.DrawImage(Pic1, 0, 0)
        ' Draw second source image, offset to the right edge of first source image
        gr.DrawImage(pic2, Pic1.Width, 0)

        ' Assign the merged bitmap you have just created as the image
        ' you are going to return for printing
        MergedImage = bm

        ' Finished with the Graphics object – dispose of it
        gr.Dispose()

        ' You now have an Image named MergedImage which you can print.
        Return MergedImage

    End Function

    Public Function GetTempFile(Optional cFileExtension As String = "", Optional cFileHeader As String = "TMP", Optional cFilePath As String = "", Optional cSubPath As String = "", _
                                Optional lReturnFileNameWithNoExtension As Boolean = False) As String

        Dim cFileName As String = ""
        Dim nCnt As Long = 0

        GetTempFile = ""

        Try
            If cFilePath.Trim = "" Then
                cFilePath = GetSharePath(cSubPath)
            End If

            cFilePath = cFilePath.Trim

            If Right(cFilePath, 1) = "\" Then
                cFilePath = Mid(cFilePath, 1, Len(cFilePath) - 1)
            End If

            Do While True
                nCnt = nCnt + 1
                cFileName = cFilePath.Trim + "\" + cFileHeader.Trim + Microsoft.VisualBasic.Format(nCnt, "00000")
                If cFileExtension.Trim <> "" Then
                    cFileName = cFileName + "." + cFileExtension
                End If
                If Not My.Computer.FileSystem.FileExists(cFileName) Then
                    GetTempFile = cFileName
                    Exit Do
                End If
            Loop

            If lReturnFileNameWithNoExtension Then
                GetTempFile = GetTempFile.Replace("." + cFileExtension, "")
            End If

        Catch ex As Exception
            ErrDisp("GetTempFile : " + ex.Message, "utilroot")
        End Try
    End Function

    Public Function GetSharePath(Optional cSubPath As String = "") As String

        Dim cSharePath As String = ""
        Dim oFile As FileInfo

        GetSharePath = ""

        Try
            cSharePath = GetSysPar("pathofshare")

            If cSharePath = "" Then
                cSharePath = System.Windows.Forms.Application.ExecutablePath
                oFile = New FileInfo(cSharePath)
                cSharePath = oFile.DirectoryName
            End If

            cSharePath = cSharePath.Trim

            If Right(cSharePath, 1) = "\" Then
                cSharePath = Mid(cSharePath, 1, Len(cSharePath) - 1)
            End If

            If cSubPath.Trim <> "" Then
                cSubPath = cSubPath.Replace("\", "")
                cSharePath = cSharePath + "\" + cSubPath
            End If

            If Not My.Computer.FileSystem.DirectoryExists(cSharePath) Then
                My.Computer.FileSystem.CreateDirectory(cSharePath)
            End If

            GetSharePath = cSharePath.Trim

        Catch ex As Exception
            ' do nothing 
            ' ErrDisp("GetSharePath sharepath -- subpath : " + cSharePath + " -- " + cSubPath, "utilroot",,, ex)
        End Try
    End Function

    Public Sub AssingReportVariables()
        Try
            oReport.oReportStimulus.Item("cVariable1") = oReport.cReportVariable1
            oReport.oReportStimulus.Item("cVariable2") = oReport.cReportVariable2
            oReport.oReportStimulus.Item("cVariable3") = oReport.cReportVariable3
            oReport.oReportStimulus.Item("cVariable4") = oReport.cReportVariable4
            oReport.oReportStimulus.Item("cVariable5") = oReport.cReportVariable5
            oReport.oReportStimulus.Item("cVariable6") = oReport.cReportVariable6
            oReport.oReportStimulus.Item("cVariable7") = oReport.cReportVariable7
            oReport.oReportStimulus.Item("cVariable8") = oReport.cReportVariable8
            oReport.oReportStimulus.Item("cVariable9") = oReport.cReportVariable9
            oReport.oReportStimulus.Item("cVariable10") = oReport.cReportVariable10

        Catch ex As Exception
            ErrDisp("AssingReportVariables : " + ex.Message, "utilroot",,, ex)
        End Try
    End Sub

    Public Function Decypher(WStr As String) As String
        Decypher = ""
        Try
            Dim code As Integer
            Dim DeStr As String
            Dim i As Integer
            DeStr = ""
            code = 0
            For i = 1 To Len(WStr)
                code = Asc(Mid(WStr, i, 1))
                DeStr = DeStr + Chr(CInt((code - i) / 2))
            Next
            Decypher = DeStr
        Catch ex As Exception
            ErrDisp("Decypher : " + ex.Message, "utilroot",,, ex)
        End Try
    End Function

    Public Sub Pause(nSeconds As Double)
        Try
            Dim nStart As DateTime

            nStart = DateTime.Now    ' Set start time.

            Do While Now < DateAdd(DateInterval.Second, nSeconds, nStart)
                Application.DoEvents()    ' Yield to other processes.
            Loop

        Catch ex As Exception
            ' do nothing 
        End Try
    End Sub

    Public Sub WEBServisPerformans(cIslemGuid As String, Optional cServisAdi As String = "", Optional cIslem As String = "", Optional cWinTex As String = "", Optional cFisNo As String = "")
        Try
            Dim oSQL As New SQLServerClass

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select top 1 sirano " +
                            " from webservisperformans with (NOLOCK) " +
                            " where islemguid = '" + cIslemGuid.Trim + "' "

            If oSQL.CheckExists Then
                oSQL.cSQLQuery = "update webservisperformans set " +
                                    " bitir = getdate() " +
                                    " where islemguid = '" + cIslemGuid.Trim + "' "
                oSQL.SQLExecute()
            Else
                oSQL.cSQLQuery = "insert webservisperformans (islemguid, servisadi, islem, wintex, fisno, basla) " +
                                " values ('" + cIslemGuid.Trim + "', " +
                                " '" + SQLWriteString(cServisAdi.Trim, 50) + "', " +
                                " '" + SQLWriteString(cIslem.Trim, 50) + "', " +
                                " '" + SQLWriteString(cWinTex.Trim, 50) + "', " +
                                " '" + SQLWriteString(cFisNo.Trim, 30) + "', " +
                                " getdate())  "

                oSQL.SQLExecute()
            End If

            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp("WEBServisPerformans", "utilroot",,, ex)
        End Try
    End Sub

    Public Function StrStripLettersNumbers(cText As String,
                                   Optional lReplaceBadCharactersWithBlank As Boolean = True,
                                   Optional lDeleteSpace As Boolean = False,
                                   Optional nMaxChars As Integer = 0) As String

        StrStripLettersNumbers = cText

        Try
            Dim nCnt As Integer = 0
            Dim nMaxLen As Integer = 0
            Dim cBuffer As String = ""

            nMaxLen = Len(cText)
            StrStripLettersNumbers = ""

            For nCnt = 1 To nMaxLen
                cBuffer = Mid(cText, nCnt, 1)

                If (Asc(cBuffer) > 47 And Asc(cBuffer) < 58) _
                Or (Asc(cBuffer) > 64 And Asc(cBuffer) < 91) _
                Or (Asc(cBuffer) > 96 And Asc(cBuffer) < 123) _
                Or cBuffer = "-" Then

                    If lDeleteSpace Then
                        If cBuffer <> " " Then
                            StrStripLettersNumbers += cBuffer
                        End If
                    Else
                        StrStripLettersNumbers += cBuffer
                    End If
                Else
                    If lReplaceBadCharactersWithBlank Then StrStripLettersNumbers += IIf(lDeleteSpace, "", " ").ToString
                End If
            Next
            If lDeleteSpace Then
                StrStripLettersNumbers = Replace(StrStripLettersNumbers, " ", "")
            End If
            If nMaxChars <> 0 And StrStripLettersNumbers.Trim <> "" Then
                StrStripLettersNumbers = Mid(StrStripLettersNumbers, 1, nMaxChars)
            End If
            StrStripLettersNumbers = StrStripLettersNumbers.Trim

        Catch ex As Exception
            ErrDisp("StrStripLettersNumbers", "utilroot",,, ex)
        End Try
    End Function

    Public Sub GetFileNamePath(ByVal cFullPath As String, ByRef cFileName As String, ByRef cFilePath As String)
        Try
            Dim oFile As System.IO.FileInfo

            oFile = My.Computer.FileSystem.GetFileInfo(cFullPath)
            cFilePath = oFile.DirectoryName
            cFileName = oFile.Name

        Catch ex As Exception
            ErrDisp("GetFileNamePath", "utilroot",,, ex)
        End Try
    End Sub

    Public Function AddDocument(cDocValue As String, cDocType As String, cDocSubType As String, Optional cOriginalFileName As String = "",
                                Optional cFileExtension As String = "jpg", Optional cFileType As String = "Picture File",
                                Optional cOriginalFile As String = "", Optional lUnique As Boolean = True) As String
        AddDocument = ""

        Try
            Dim oSQL As New SQLServerClass
            Dim cAppPath As String = GetSharePath()
            Dim cBareFileName As String = cDocType.Trim + "_" + cDocValue.Trim + "_" + Guid.NewGuid.ToString
            Dim cDocName As String = cBareFileName.Trim + "." + cFileExtension
            Dim cFileName As String = cAppPath + "\docs\" + cDocName

            If cOriginalFileName.Trim = "" Then
                If cOriginalFile.Trim = "" Then
                    cOriginalFile = cDocName
                End If
            Else
                Dim fi As New IO.FileInfo(cOriginalFileName)
                cOriginalFile = fi.Name.Trim
                cFileExtension = fi.Extension.Trim
                cFileExtension = Strings.Replace(cFileExtension, ".", "")

                cDocName = cBareFileName.Trim + "." + cFileExtension
                cFileName = cAppPath + "\docs\" + cDocName
                IO.File.Copy(cOriginalFileName, cFileName)
            End If

            Select Case cFileExtension
                Case "jpg", "jpeg", "bmp", "diff"
                    cFileType = "Picture File"
                Case "xls", "xlsx"
                    cFileType = "Excel"
                Case "doc", "docx"
                    cFileType = "Word"
                Case "pdf"
                    cFileType = "Power Point"
                Case Else
                    cFileType = "Diger"
            End Select

            oSQL.OpenConn()

            If lUnique Then
                oSQL.cSQLQuery = "update documents " +
                                " set pasif = 'E' " +
                                " where docvalue = '" + cDocValue.Trim + "' " +
                                " and doctype = '" + cDocType.Trim + "' " +
                                " and docsubtype = '" + cDocSubType.Trim + "' "

                oSQL.SQLExecute()
            End If

            oSQL.cSQLQuery = "delete documents " +
                            " where docvalue = '" + cDocValue.Trim + "' " +
                            " and doctype = '" + cDocType.Trim + "' " +
                            " and docpath = '" + cFileName.Trim + "' "

            oSQL.SQLExecute()

            oSQL.cSQLQuery = "insert documents (docvalue, doctype, rdocname, vdocname, docpath, " +
                            " type, extension, docsubtype, duzletmetarihi, duzeltmesaati, " +
                            " username) "

            oSQL.cSQLQuery = oSQL.cSQLQuery +
                            " values ('" + SQLWriteString(cDocValue, 30) + "', " +
                            " '" + SQLWriteString(cDocType, 30) + "', " +
                            " '" + SQLWriteString(cOriginalFile, 150) + "', " +
                            " '" + SQLWriteString(cBareFileName, 150) + "', " +
                            " '" + SQLWriteString(cFileName, 255) + "', "

            oSQL.cSQLQuery = oSQL.cSQLQuery +
                            " '" + SQLWriteString(cFileType, 30) + "', " +
                            " '" + SQLWriteString(cFileExtension, 30) + "', " +
                            " '" + SQLWriteString(cDocSubType, 30) + "', " +
                            " convert(date,getdate()), " +
                            " convert(char(8),getdate(),108), "

            oSQL.cSQLQuery = oSQL.cSQLQuery +
                            " 'wintexdll') "

            oSQL.SQLExecute()

            oSQL.CloseConn()

            AddDocument = cFileName.Trim

        Catch ex As Exception
            ErrDisp("AddDocument", "utilroot",,, ex)
        End Try
    End Function

    Public Function GetBedenlerFromBedenSeti(cBedenSeti As String) As String()

        Dim oSQL As SQLServerClass
        Dim nCnt As Integer = 0
        Dim nCnt2 As Integer = 0
        Dim cBeden As String = ""
        Dim aBeden() As String

        ReDim aBeden(0)

        GetBedenlerFromBedenSeti = aBeden

        If cBedenSeti.Trim = "" Then Exit Function

        Try
            oSQL = New SQLServerClass

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select top 1 * " +
                         " from bedenseti with (NOLOCK) " +
                         " where bedenseti = '" + cBedenSeti + "' "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                For nCnt = 1 To 99
                    cBeden = "b" + nCnt.ToString("00")
                    If oSQL.SQLReadString(cBeden) <> "" Then
                        If aBeden(0) = "" Then
                            aBeden(0) = oSQL.SQLReadString(cBeden)
                        Else
                            nCnt2 = nCnt2 + 1
                            ReDim Preserve aBeden(nCnt2)
                            aBeden(nCnt2) = oSQL.SQLReadString(cBeden)
                        End If
                    End If
                Next
            End If
            oSQL.oReader.Close()

            oSQL.CloseConn()

            GetBedenlerFromBedenSeti = aBeden

        Catch ex As Exception
            ErrDisp("GetBedenlerFromBedenSeti", "utilroot",,, ex)
        End Try
    End Function

    Public Function StringToNumber(cTextNumber As String) As Double

        StringToNumber = 0

        Try
            Dim nPos As Integer = 0
            Dim nCnt As Integer = 0
            Dim nTamSayi As Double = 0
            Dim nOndalik As Double = 0
            Dim cOndalik As String = ""
            Dim nOndalikLen As Integer = 0
            Dim nOndalikDiv As Double = 0
            Dim cOndalikDiv As String = ""

            If cTextNumber.Trim = "" Then Exit Function

            nPos = InStr(cTextNumber, ",")
            If nPos = 0 Then
                nPos = InStr(cTextNumber, ".")
            End If

            If nPos = 0 Then
                nTamSayi = CDbl(cTextNumber)
                nOndalik = 0
            Else
                nTamSayi = CDbl(Mid(cTextNumber, 1, nPos - 1))
                cOndalik = RTrim(Mid(cTextNumber, nPos + 1, 100))
                nOndalikLen = Len(cOndalik)
                cOndalikDiv = "1"
                For nCnt = 1 To nOndalikLen
                    cOndalikDiv = cOndalikDiv + "0"
                Next
                nOndalikDiv = CDbl(cOndalikDiv)
                nOndalik = CDbl(cOndalik) / nOndalikDiv
            End If

            StringToNumber = nTamSayi + nOndalik

        Catch ex As Exception
            ErrDisp("StringToNumber", "utilroot",,, ex)
        End Try
    End Function

    Public Function NumberToString(nNumber As Double) As String

        NumberToString = "0"

        Try
            Dim nPos As Integer = 0

            If nNumber = 0 Then Exit Function
            nNumber = Math.Round(nNumber, 1)
            NumberToString = String.Format("##,###0.0", nNumber)
            nPos = InStr(NumberToString, ".")
            If nPos <> 0 Then
                NumberToString = Mid(NumberToString, 1, nPos + 1)
            End If
            If nNumber > 0 Then
                NumberToString = "+" + NumberToString
            End If

        Catch ex As Exception
            ErrDisp("NumberToString", "utilroot",,, ex)
        End Try
    End Function

End Module
