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

    Public Structure oSQLConn

        Dim cOwner As String

        Dim cServer As String
        Dim cDatabase As String
        Dim cUser As String
        Dim cPassword As String
        Dim cConnStr As String

    End Structure

    Public oConnection As oSQLConn

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

    Public Function GetTempFile(Optional cFileExtension As String = "", Optional cFileHeader As String = "TMP", Optional cFilePath As String = "", Optional cSubPath As String = "",
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
            ErrDisp("GetSharePath : " + ex.Message, "utilroot")
        End Try
    End Function

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
            ErrDisp("Decypher : " + ex.Message, "utilroot")
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

    Public Function SearchFit(cText As String) As String

        SearchFit = cText.Trim

        Try
            Dim cBuffer As String = cText.Trim
            Dim cResult As String = ""
            Dim cHarf As String = ""
            Dim cHarf2 As String = ""
            Dim nCnt As Integer = 0
            Dim nLen As Integer = 0

            If cBuffer = "" Then Exit Function
            nLen = cBuffer.Length
            For nCnt = 1 To nLen
                cHarf = Mid(cBuffer, nCnt, 1)
                Select Case cHarf
                    Case "â", "Â"
                        cHarf2 = "[âÂaA]"
                    Case "ê", "Ê"
                        cHarf2 = "[êÊeE]"
                    Case "ş", "Ş"
                        cHarf2 = "[şŞsS]"
                    Case "ğ", "Ğ"
                        cHarf2 = "[ğĞgG]"
                    Case "ü", "Ü", "û", "Û"
                        cHarf2 = "[üÜuUûÛ]"
                    Case "ı", "I", "i", "İ", "î", "Î"
                        cHarf2 = "[ıIiİîÎ]"
                    Case "ö", "Ö", "ô", "Ô"
                        cHarf2 = "[öÖoOôÔ]"
                    Case "ç", "Ç"
                        cHarf2 = "[çÇcC]"
                    Case Else
                        cHarf2 = cHarf
                End Select
                cResult = cResult + cHarf2
            Next

            cResult = Replace(cResult, ".", "_")
            cResult = "%" + cResult + "%"
            cResult = Replace(cResult, "%_", "%")
            cResult = Replace(cResult, "_%", "%")

            SearchFit = cResult

        Catch ex As Exception
            ' do nothing 
        End Try
    End Function

    Public Function CheckFirmaCalisilmasin(cFirma As String) As Boolean

        CheckFirmaCalisilmasin = False

        Try
            Dim oSQL As New SQLServerClass

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select top 1 firma " +
                            " from firma with (NOLOCK) " +
                            " where firma = '" + cFirma.Trim + "' " +
                            " and calisilmasin = 'E' "

            CheckFirmaCalisilmasin = oSQL.CheckExists()

            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp("CheckFirmaCalisilmasin : " + ex.Message, "utilroot",,, ex)
        End Try
    End Function

End Module
