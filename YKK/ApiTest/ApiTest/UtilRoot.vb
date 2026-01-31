Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Threading
Imports System.Drawing
Imports System.IO
Imports System.Reflection
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
        Dim cYKKApiUrl As String
        Dim cYKKApiTestUrl As String
        Dim cYKKApiUserName As String
        Dim cYKKApiPassword As String
        Dim cYKKFirma As String
        Dim cYKKDefaultBuyer As String
        Dim cYKKPortalUrl As String
        Dim cYKKPortalTestUrl As String
        Dim cYKKTestUserName As String
        Dim cYKKTestPassword As String
        Dim cPersonel As String
        Dim cModelNo As String
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
            ErrDisp("DestroyFile : " + ex.Message, "utilroot",,, ex)
        End Try
    End Sub


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
                cSharePath = System.Reflection.Assembly.GetExecutingAssembly().Location
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
            ErrDisp("GetSharePath : " + ex.Message, "utilroot",,, ex)
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
            ErrDisp("Decypher : " + ex.Message, "utilroot",,, ex)
        End Try
    End Function

    Public Sub Pause(nSeconds As Double)
        Try
            Dim nStart As DateTime

            nStart = DateTime.Now    ' Set start time.

            Do While Now < DateAdd(DateInterval.Second, nSeconds, nStart)
                System.Threading.Thread.Sleep(0)     ' Yield to other processes.
            Loop

        Catch ex As Exception
            ' do nothing 
        End Try
    End Sub

    Public Sub GetYKKParameters()

        Try
            Dim Connyage As SqlConnection
            Dim oDr As SqlDataReader
            Dim cSQL As String = ""

            Connyage = OpenConn()

            cSQL = "select top 1 firmname " +
                    " from sysinfo with (NOLOCK) " +
                    " where firmname is not null " +
                    " and firmname <> '' "

            oDr = GetSQLReader(cSQL, Connyage)

            If oDr.Read Then
                oConnection.cOwner = SQLReadString(oDr, "firmname").ToLower
            End If
            oDr.Close()

            oConnection.cYKKApiUrl = GetSysParConnected("ykkapiurl", Connyage, "https://ykkapi.ykk.com.tr")
            oConnection.cYKKApiTestUrl = GetSysParConnected("ykkapitesturl", Connyage, "https://ykkapitest.ykk.com.tr")
            oConnection.cYKKPortalUrl = GetSysParConnected("ykkportalurl", Connyage, "https://eorder.ykk.com.tr")
            oConnection.cYKKPortalTestUrl = GetSysParConnected("ykkportaltesturl", Connyage, "https://eordertest.ykk.com.tr")
            oConnection.cYKKTestUserName = GetSysParConnected("ykktestusername", Connyage, "tahagiyim1@tahagiyim.com")
            oConnection.cYKKTestPassword = GetSysParConnected("ykktestpassword", Connyage, "123456Xy")

            If oConnection.cOwner = "eroglu" Then
                oConnection.cYKKApiUserName = GetSysParConnected("ykkapiusername", Connyage, "ykk@eroglugiyim.com")         ' tahagiyim1@tahagiyim.com
                oConnection.cYKKApiPassword = GetSysParConnected("ykkapipassword", Connyage, "€r0Glu2019!")                 ' 123456Xy
                oConnection.cYKKFirma = GetSysParConnected("ykkfirma", Connyage, "320 01 Y02")
                oConnection.cYKKDefaultBuyer = GetSysParConnected("ykkdefaultbuyer", Connyage, "TK0226")
            Else
                oConnection.cYKKApiUserName = GetSysParConnected("ykkapiusername", Connyage)
                oConnection.cYKKApiPassword = GetSysParConnected("ykkapipassword", Connyage)
                oConnection.cYKKFirma = GetSysParConnected("ykkfirma", Connyage)
                oConnection.cYKKDefaultBuyer = GetSysParConnected("ykkdefaultbuyer", Connyage)
            End If

            Connyage.Close()

        Catch ex As Exception
            ErrDisp("GetYKKParameters : " + ex.Message, "utilroot",,, ex)
        End Try
    End Sub
End Module
