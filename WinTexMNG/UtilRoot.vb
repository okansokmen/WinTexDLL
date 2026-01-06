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

        Dim cMNGApiUrl As String
        Dim cMNGIadeUrl As String
        Dim cMNGApiUserName As String
        Dim cMNGApiPassword As String
        Dim cMNGFirma As String

        Dim cByExpressApiUrl As String
        Dim cByExpressApiUserName As String
        Dim cByExpressApiPassword As String
        Dim cByExpressFirma As String
        Dim cByExpressSozlesmeNo As String

        Dim cPTTApiUrl As String
        Dim cPTTApiUserName As String
        Dim cPTTApiPassword As String
        Dim cPTTFirma As String
        Dim cPTTMusteri As String

        Dim cPTTBilgiUrl As String
        Dim cPTTBilgiUserName As String
        Dim cPTTBilgiPassword As String

        Dim cNacUrl As String
        Dim cNacUserName As String
        Dim cNacPassword As String
        Dim cNacSmsBasligi As String

        Dim cEContUrl As String
        Dim cEContUserName As String
        Dim cEContPassword As String

        Dim cYurticiUrl As String
        Dim cYurticiAONormal_UserName As String
        Dim cYurticiAONormal_Password As String
        Dim cYurticiGONormal_UserName As String
        Dim cYurticiGONormal_Password As String
        Dim cYurticiAOTahsilatli_UserName As String
        Dim cYurticiAOTahsilatli_Password As String
        Dim cYurticiGOTahsilatli_UserName As String
        Dim cYurticiGOTahsilatli_Password As String

        Dim cKZPanelUrl As String
        Dim cKZPanelUserName As String
        Dim cKZPanelPassword As String
        Dim cKZApiUrl As String
        Dim cKZApiUserName As String
        Dim cKZApiPassword As String

        Dim cIEPanelUrl As String
        Dim cIEPanelUserName As String
        Dim cIEPanelPassword As String
        Dim cIEApiUrl As String
        Dim cIEApiUserName As String
        Dim cIEApiPassword As String

        Dim cArasApiUrl As String
        Dim cArasWinTexKodu As String
        Dim cArasMusteriKodu As String
        ' okuyan kullanıcı
        Dim cArasApiUserName As String
        Dim cArasApiPassword As String
        ' yazan kullanıcı
        Dim cArasApiUserName2 As String
        Dim cArasApiPassword2 As String

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

    Public Sub GetYurticiParameters()

        Try
            Dim Connyage As SqlConnection

            Connyage = OpenConn()

            oConnection.cYurticiUrl = GetSysParConnected("yurticiurl", Connyage, "http://webservices.yurticikargo.com:8080/KOPSWebServices/ShippingOrderDispatcherServices?wsdl")

            If oConnection.cOwner = "gecit" Then
                ' Alıcı Ödemeli Normal
                oConnection.cYurticiAONormal_UserName = GetSysParConnected("yurticiaonusername", Connyage, "1160N977588062A")
                oConnection.cYurticiAONormal_Password = GetSysParConnected("yurticiaonpassword", Connyage, "YXYDe59xRA2a5Xfg")
                ' Gönderici Ödemeli Normal
                oConnection.cYurticiGONormal_UserName = GetSysParConnected("yurticigonusername", Connyage, "1160N977588062G")
                oConnection.cYurticiGONormal_Password = GetSysParConnected("yurticigonpassword", Connyage, "pk9w6VJxn9r8V7Nv")
                ' Alıcı Ödemeli Tahsilatlı
                oConnection.cYurticiAOTahsilatli_UserName = GetSysParConnected("yurticiaotusername", Connyage, "1160T977588062A")
                oConnection.cYurticiAOTahsilatli_Password = GetSysParConnected("yurticiaotpassword", Connyage, "rChY45wvn4091700")
                ' Gönderici Ödemeli Tahsilatlı
                oConnection.cYurticiGOTahsilatli_UserName = GetSysParConnected("yurticigotusername", Connyage, "1160T977588062G")
                oConnection.cYurticiGOTahsilatli_Password = GetSysParConnected("yurticigotpassword", Connyage, "e44VCw905Gv3yH07")
            Else
                ' Alıcı Ödemeli Normal
                oConnection.cYurticiAONormal_UserName = GetSysParConnected("yurticiaonusername", Connyage)
                oConnection.cYurticiAONormal_Password = GetSysParConnected("yurticiaonpassword", Connyage)
                ' Gönderici Ödemeli Normal
                oConnection.cYurticiGONormal_UserName = GetSysParConnected("yurticigonusername", Connyage)
                oConnection.cYurticiGONormal_Password = GetSysParConnected("yurticigonpassword", Connyage)
                ' Alıcı Ödemeli Tahsilatlı
                oConnection.cYurticiAOTahsilatli_UserName = GetSysParConnected("yurticiaotusername", Connyage)
                oConnection.cYurticiAOTahsilatli_Password = GetSysParConnected("yurticiaotpassword", Connyage)
                ' Gönderici Ödemeli Tahsilatlı
                oConnection.cYurticiGOTahsilatli_UserName = GetSysParConnected("yurticigotusername", Connyage)
                oConnection.cYurticiGOTahsilatli_Password = GetSysParConnected("yurticigotpassword", Connyage)
            End If

            Connyage.Close()

        Catch ex As Exception
            ErrDisp("GetYurticiParameters : " + ex.Message, "utilroot",,, ex)
        End Try
    End Sub

    Public Sub GetMNGParameters()

        Try
            Dim Connyage As SqlConnection

            Connyage = OpenConn()

            oConnection.cMNGApiUrl = GetSysParConnected("mngapiurl", Connyage, "http://service.mngkargo.com.tr/tservis/musterikargosiparis.asmx")
            oConnection.cMNGFirma = GetSysParConnected("mngfirma", Connyage, "MNG")

            If oConnection.cOwner = "gecit" Then
                oConnection.cMNGApiUserName = GetSysParConnected("mngapiusername", Connyage, "35615719")
                oConnection.cMNGApiPassword = GetSysParConnected("mngapipassword", Connyage, "356TST2425XGHPRFTG")
            Else
                oConnection.cMNGApiUserName = GetSysParConnected("mngapiusername", Connyage)
                oConnection.cMNGApiPassword = GetSysParConnected("mngapipassword", Connyage)
            End If

            Connyage.Close()

        Catch ex As Exception
            ErrDisp("GetMNGParameters : " + ex.Message, "utilroot",,, ex)
        End Try
    End Sub

    Public Sub GetArasParameters()

        Try
            Dim Connyage As SqlConnection

            Connyage = OpenConn()

            oConnection.cArasApiUrl = GetSysParConnected("arasapiurl", Connyage, "https://customerservices.araskargo.com.tr/ArasCargoCustomerIntegrationService/ArasCargoIntegrationService.svc")
            oConnection.cArasWinTexKodu = GetSysParConnected("araswintexkodu", Connyage, "ARAS")

            If oConnection.cOwner = "gecit" Then
                oConnection.cArasApiUserName = GetSysParConnected("arasapiusername", Connyage, "mares")
                oConnection.cArasApiPassword = GetSysParConnected("arasapipassword", Connyage, "my343827")
                oConnection.cArasMusteriKodu = GetSysParConnected("arasmusterikodu", Connyage, "2329754550911")
            Else
                oConnection.cArasApiUserName = GetSysParConnected("arasapiusername", Connyage)
                oConnection.cArasApiPassword = GetSysParConnected("arasapipassword", Connyage)
                oConnection.cArasMusteriKodu = GetSysParConnected("arasmusterikodu", Connyage)
            End If

            Connyage.Close()

        Catch ex As Exception
            ErrDisp("GetArasParameters : " + ex.Message, "utilroot",,, ex)
        End Try
    End Sub

    Public Sub GetInterlineExpressParameters()
        ' API KEY :          4NgyXODmG1KAqaELhnk3fcT5FB6JRHWC8PbItSQz
        ' API MAIL :         mares@interlinekargo.com
        ' API ENDPOINT URL : http://webpostman.interlineexpress.com:9999/
        ' ŞUBE KODU :  IDM
        ' ŞUBE ADI : İNTERLINE DAĞITIM

        Try
            oConnection.cIEPanelUrl = "http://webpostman.interlineexpress.com"
            oConnection.cIEApiUrl = "http://webpostman.interlineexpress.com:9999/restapi/client"
            oConnection.cIEPanelUserName = "mares@interlinekargo.com"
            oConnection.cIEPanelPassword = ""
            oConnection.cIEApiUserName = "mares@interlinekargo.com"
            oConnection.cIEApiPassword = "4NgyXODmG1KAqaELhnk3fcT5FB6JRHWC8PbItSQz"

        Catch ex As Exception
            ErrDisp("GetInterlineExpressParameters : " + ex.Message, "utilroot",,, ex)
        End Try
    End Sub

    Public Sub GetKargoZamaniParameters()

        Try
            Dim Connyage As SqlConnection

            Connyage = OpenConn()

            oConnection.cKZPanelUrl = GetSysParConnected("kzpanelurl", Connyage, "https://webpostman.kargozamani.com/")
            oConnection.cKZApiUrl = GetSysParConnected("kzapiurl", Connyage, "https://webpostman.kargozamani.com/restapi/client")

            If oConnection.cOwner = "gecit" Then
                oConnection.cKZPanelUserName = GetSysParConnected("kzpanelusername", Connyage, "ayakkabilarim@kargozamani.com")
                oConnection.cKZPanelPassword = GetSysParConnected("kzpanelpassword", Connyage, "Ak20681*")
                oConnection.cKZApiUserName = GetSysParConnected("kzapiusername", Connyage, "ayakkabilarim@kargozamani.com")
                oConnection.cKZApiPassword = GetSysParConnected("kzapipassword", Connyage, "pJNOPa7t3nsUCrE4WFvGx8MjBTS06yR29dzfHXQq")
            Else
                oConnection.cKZPanelUserName = GetSysParConnected("kzpanelusername", Connyage)
                oConnection.cKZPanelPassword = GetSysParConnected("kzpanelpassword", Connyage)
                oConnection.cKZApiUserName = GetSysParConnected("kzapiusername", Connyage)
                oConnection.cKZApiPassword = GetSysParConnected("kzapipassword", Connyage)
            End If

            Connyage.Close()

        Catch ex As Exception
            ErrDisp("GetKargoZamaniParameters : " + ex.Message, "utilroot",,, ex)
        End Try
    End Sub

    Public Sub GetByExpressParameters()

        Try
            Dim Connyage As SqlConnection

            Connyage = OpenConn()

            oConnection.cByExpressApiUrl = GetSysParConnected("byexpressapiurl", Connyage, "http://apidemo.byexpresskargo.net")
            oConnection.cByExpressApiUserName = GetSysParConnected("byexpressapiusername", Connyage, "")
            oConnection.cByExpressApiPassword = GetSysParConnected("byexpressapipassword", Connyage, "")
            oConnection.cByExpressSozlesmeNo = GetSysParConnected("byexpresssozlesmeno", Connyage, "")
            oConnection.cByExpressFirma = GetSysParConnected("byexpressfirma", Connyage, "BYEXPRESS")

            Connyage.Close()

        Catch ex As Exception
            ErrDisp("GetByExpressParameters : " + ex.Message, "utilroot",,, ex)
        End Try
    End Sub

    Public Sub GetPTTParameters()

        Try
            Dim Connyage As SqlConnection

            Connyage = OpenConn()

            oConnection.cPTTApiUrl = GetSysParConnected("pttapiurl", Connyage, "https://pttws.ptt.gov.tr/PttVeriYukleme/services/Sorgu?wsdl")
            oConnection.cPTTFirma = GetSysParConnected("pttfirma", Connyage, "PTT")

            oConnection.cPTTBilgiUrl = GetSysParConnected("pttbilgiurl", Connyage, "https://pttws.ptt.gov.tr/PttBilgi/services/Sorgu?wsdl")
            oConnection.cPTTBilgiUserName = GetSysParConnected("pttbilgiusername", Connyage, "pttUser")
            oConnection.cPTTBilgiPassword = GetSysParConnected("pttbilgipassword", Connyage, "PttBilgi*2015")

            If oConnection.cOwner = "gecit" Then
                oConnection.cPTTApiUserName = GetSysParConnected("pttapiusername", Connyage, "admin")
                oConnection.cPTTApiPassword = GetSysParConnected("pttapipassword", Connyage, "34Ep0040") ' "159263Mm*-"
                oConnection.cPTTMusteri = GetSysParConnected("pttmusteri", Connyage, "504101012")
            Else
                oConnection.cPTTApiUserName = GetSysParConnected("pttapiusername", Connyage)
                oConnection.cPTTApiPassword = GetSysParConnected("pttapipassword", Connyage) ' "159263Mm*-"
                oConnection.cPTTMusteri = GetSysParConnected("pttmusteri", Connyage)
            End If

            Connyage.Close()

        Catch ex As Exception
            ErrDisp("GetPTTParameters : " + ex.Message, "utilroot",,, ex)
        End Try
    End Sub

    Public Sub GetEContParameters()

        Try

            Dim Connyage As SqlConnection

            Connyage = OpenConn()

            oConnection.cEContUrl = GetSysParConnected("econturl", Connyage, "http://ee.econt.com/services/")

            If oConnection.cOwner = "gecit" Then
                oConnection.cEContUserName = GetSysParConnected("econtusername", Connyage, "leylabgood@gmail.com")
                oConnection.cEContPassword = GetSysParConnected("econtpassword", Connyage, "12345654321Aq.")
            Else
                oConnection.cEContUserName = GetSysParConnected("econtusername", Connyage)
                oConnection.cEContPassword = GetSysParConnected("econtusername", Connyage)
            End If

            Connyage.Close()

        Catch ex As Exception
            ErrDisp("GetEContParameters : " + ex.Message, "utilroot",,, ex)
        End Try
    End Sub

    Public Sub GetNacParameters()

        Try

            Dim Connyage As SqlConnection

            Connyage = OpenConn()

            oConnection.cNacUrl = GetSysParConnected("nacurl", Connyage, "https://smslogin.nac.com.tr")

            If oConnection.cOwner = "gecit" Then
                oConnection.cNacUserName = GetSysParConnected("nacusername", Connyage, "benimpabucum")
                oConnection.cNacPassword = GetSysParConnected("nacpassword", Connyage, "y6nEA9mk")
                oConnection.cNacSmsBasligi = GetSysParConnected("nacsmsbasligi", Connyage, "02129459535")
            Else
                oConnection.cNacUserName = GetSysParConnected("nacusername", Connyage)
                oConnection.cNacPassword = GetSysParConnected("nacpassword", Connyage)
                oConnection.cNacSmsBasligi = GetSysParConnected("nacsmsbasligi", Connyage)
            End If

            Connyage.Close()

        Catch ex As Exception
            ErrDisp("GetNacParameters : " + ex.Message, "utilroot",,, ex)
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

    Public Function GetServiceConnectionParameters(ByVal cSiparisNo As String, Optional ByRef cUserName As String = "",
                                                   Optional ByRef cPassword As String = "", Optional ByRef cFirmaSozlesme As String = "",
                                                   Optional ByVal cKargoFirmasi As String = "", Optional ByVal cTipi As String = "GOT",
                                                   Optional ByRef cSiparisNo2 As String = "", Optional lBilgiGonderme As Boolean = False) As Boolean
        ' cTipi = GOT , Gönderici Ödemeli Tahsilatlı
        ' cTipi = AOT , Alıcı Ödemeli Tahsilatlı
        ' cTipi = GO  , Gönderici Ödemeli Normal
        ' cTipi = AO  , Alıcı Ödemeli Normal

        Dim oSQL As New SQLServerClass
        Dim lCoklu As Boolean = False

        GetServiceConnectionParameters = False

        Try
            If cSiparisNo.Trim = "" Then Exit Function

            Select Case cKargoFirmasi
                Case "ARAS"
                    If lBilgiGonderme Then
                        cUserName = oConnection.cArasApiUserName2
                        cPassword = oConnection.cArasApiPassword2
                    Else
                        cUserName = oConnection.cArasApiUserName
                        cPassword = oConnection.cArasApiPassword
                    End If
                    cFirmaSozlesme = oConnection.cArasMusteriKodu
                Case "BYEXPRESS"
                    cUserName = oConnection.cByExpressApiUserName
                    cPassword = oConnection.cByExpressApiPassword
                    cFirmaSozlesme = oConnection.cByExpressSozlesmeNo
                Case "MNG"
                    cUserName = oConnection.cMNGApiUserName
                    cPassword = oConnection.cMNGApiPassword
                    cFirmaSozlesme = ""
                Case "PTT"
                    cUserName = oConnection.cPTTApiUserName
                    cPassword = oConnection.cPTTApiPassword
                    cFirmaSozlesme = oConnection.cPTTMusteri
                Case "YURTICI"
                    Select Case cTipi
                        Case "GOT"
                            cUserName = oConnection.cYurticiGOTahsilatli_UserName
                            cPassword = oConnection.cYurticiGOTahsilatli_Password
                        Case "AOT"
                            cUserName = oConnection.cYurticiAOTahsilatli_UserName
                            cPassword = oConnection.cYurticiAOTahsilatli_Password
                        Case "GO"
                            cUserName = oConnection.cYurticiGONormal_UserName
                            cPassword = oConnection.cYurticiGONormal_Password
                        Case "AO"
                            cUserName = oConnection.cYurticiAONormal_UserName
                            cPassword = oConnection.cYurticiAONormal_Password
                    End Select
                    cFirmaSozlesme = ""
                    cSiparisNo2 = cSiparisNo
            End Select

            If cKargoFirmasi <> "YURTICI" Then

                oSQL.OpenConn()

                oSQL.cSQLQuery = "select a.siparisno, a.siparisno2, b.username, b.password, b.firmasozlesme, b.username2, b.password2 " +
                                " from sipperakende a with (NOLOCK) , firmakargoaraci b with (NOLOCK) " +
                                " where a.kargofirmasi = b.kargofirmasi " +
                                " and a.aracikargo = b.aracikargo " +
                                " and b.username is not null " +
                                " and b.username <> '' " +
                                " and b.password is not null " +
                                " and b.password <> '' "

                If InStr(cSiparisNo, ";") = 0 Then
                    oSQL.cSQLQuery = oSQL.cSQLQuery +
                    " and (a.siparisno = '" + cSiparisNo.Trim + "' or a.siparisno2 = '" + cSiparisNo.Trim + "') "
                Else
                    lCoklu = True
                    cSiparisNo = Replace(cSiparisNo, ";", "','")

                    oSQL.cSQLQuery = oSQL.cSQLQuery +
                    " and (a.siparisno in ('" + cSiparisNo.Trim + "') or a.siparisno2 in ('" + cSiparisNo.Trim + "')) "
                End If

                oSQL.GetSQLReader()

                Do While oSQL.oReader.Read

                    If cKargoFirmasi = "ARAS" And lBilgiGonderme Then
                        cUserName = oSQL.SQLReadString("username2")
                        cPassword = oSQL.SQLReadString("password2")
                    Else
                        cUserName = oSQL.SQLReadString("username")
                        cPassword = oSQL.SQLReadString("password")
                    End If
                    cFirmaSozlesme = oSQL.SQLReadString("firmasozlesme")

                    If cKargoFirmasi = "MNG" And lCoklu Then
                        If oSQL.SQLReadString("siparisno2") = "" Then
                            If cSiparisNo2.Trim = "" Then
                                cSiparisNo2 = oSQL.SQLReadString("siparisno")
                            Else
                                cSiparisNo2 = cSiparisNo2 + ";" + oSQL.SQLReadString("siparisno")
                            End If
                        Else
                            If cSiparisNo2.Trim = "" Then
                                cSiparisNo2 = oSQL.SQLReadString("siparisno2")
                            Else
                                cSiparisNo2 = cSiparisNo2 + ";" + oSQL.SQLReadString("siparisno2")
                            End If
                        End If
                    Else
                        If oSQL.SQLReadString("siparisno2") = "" Then
                            cSiparisNo2 = oSQL.SQLReadString("siparisno")
                        Else
                            cSiparisNo2 = oSQL.SQLReadString("siparisno2")
                        End If
                    End If
                Loop
                oSQL.oReader.Close()
                oSQL.CloseConn()
            End If

            If cUserName.Trim <> "" And cPassword.Trim <> "" Then
                GetServiceConnectionParameters = True
            End If

        Catch ex As Exception
            ErrDisp("GetServiceConnectionParameters : " + ex.Message, "utilroot",,, ex)
        End Try
    End Function

    Public Function GetSiparisNo2(ByVal cSiparisNo As String) As String

        GetSiparisNo2 = ""

        If cSiparisNo.Trim = "" Then Exit Function

        Try
            Dim oSQL As New SQLServerClass

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select top 1 siparisno2 " +
                        " from sipperakende with (NOLOCK) " +
                        " where siparisno = '" + cSiparisNo.Trim + "' "

            GetSiparisNo2 = oSQL.DBReadString()

            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp("GetSiparisNo2 : " + ex.Message, "utilroot",,, ex)
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

End Module
