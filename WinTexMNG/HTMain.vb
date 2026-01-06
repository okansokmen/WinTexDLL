Imports System.Threading

<ComClass(HTMain.ClassId, HTMain.InterfaceId, HTMain.EventsId)>
Public Class HTMain

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "53BEB76D-226C-4996-9AF7-A9AB195B687B"
    Public Const InterfaceId As String = "6C532B6A-266A-484A-9CED-DECCDA5B6761"
    Public Const EventsId As String = "1EFA43F7-C603-4C5B-996E-C86343CDDD3E"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub

    Public Shared cWinTexMNGVersion As String = My.Application.Info.Version.ToString.Trim

    Public Sub eContGetCountries()
        eCont_GetCountries()
    End Sub

    Public Sub eContGetCities(Optional cCountryCode3 As String = "LUX")
        eCont_GetCities(cCountryCode3)
    End Sub

#Region "Takipsan"

    Public Function TakipsanGetInfo(cSiparisNo As String, Optional cUsername As String = "eroglumisir", Optional cPassword As String = "121312",
                                     Optional ByRef nTotalTargetQuantity As Double = 0, Optional ByRef nTotalScannedQuantity As Double = 0,
                                     Optional ByRef cErrorMessage As String = "") As Boolean

        Try
            TakipsanGetInfo = TakipsanGetInfo2(cSiparisNo, cUsername, cPassword, nTotalTargetQuantity, nTotalScannedQuantity, cErrorMessage)

        Catch ex As Exception
            ErrDisp("TakipsanGetInfo : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

#End Region

#Region "Postman"
    Public Function PostmanSendOrder(cSiparisNo As String, Optional ByRef cSonuc As String = "", Optional ByRef cErrorMessage As String = "",
                                     Optional nMode As Integer = 1) As Boolean

        ' nMode = 1 kargozamani / FAVORI 2024
        ' nMode = 2 kargozamani / MARES BESIKTAS
        ' nMode = 3 interlineexpress / INTERLINE DAGITIM

        PostmanSendOrder = True

        Try
            PostmanSendOrder = PostmanGonderiYukle(cSiparisNo, cSonuc, cErrorMessage, nMode)

        Catch ex As Exception
            ErrDisp("PostmanSendOrder : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function PostmanQueryOrder(cSiparisNo As String, Optional ByRef cSonuc As String = "", Optional ByRef cErrorMessage As String = "",
                                      Optional nMode As Integer = 1) As Boolean

        ' nMode = 1 kargozamani / FAVORI 2024
        ' nMode = 2 kargozamani / MARES BESIKTAS
        ' nMode = 3 interlineexpress / INTERLINE DAGITIM

        PostmanQueryOrder = True

        Try
            PostmanQueryOrder = PostmanSonDurumuAl(cSiparisNo, cSonuc, cErrorMessage, nMode)

        Catch ex As Exception
            ErrDisp("PostmanQueryOrder : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

#End Region

#Region "InterlineExpress"

    Public Function IESendOrder(cSiparisNo As String, Optional ByRef cSonuc As String = "", Optional ByRef cErrorMessage As String = "") As Boolean

        IESendOrder = True

        Try
            IESendOrder = PostmanGonderiYukle(cSiparisNo, cSonuc, cErrorMessage, 3)

        Catch ex As Exception
            ErrDisp("IESendOrder : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

#End Region

#Region "KargoZamani"

    Public Sub KargoZamaniSysPar()
        Try
            Dim ofrmKargoZamani As New frmKargoZamani
            ofrmKargoZamani.ShowDialog()

        Catch ex As Exception
            ErrDisp("KargoZamaniSysPar : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Function KZSendOrder(cSiparisNo As String) As Boolean

        KZSendOrder = False

        Try
            KZSendOrder = KZ_SendOrder(cSiparisNo)

        Catch ex As Exception
            ErrDisp("KZSendOrder : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

#End Region

#Region "Aras"

    Public Sub ArasSysPar()
        Try
            Dim ofrmAras As New frmAras
            ofrmAras.ShowDialog()

        Catch ex As Exception
            ErrDisp("ArasSysPar : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Function ArasSendOrder(cSiparisNo As String, Optional ByRef cSonuc As String = "", Optional ByRef cErrorMessage As String = "") As Boolean

        ArasSendOrder = True

        Try
            ArasSendOrder = ArasSendOrder1(cSiparisNo, cSonuc, cErrorMessage)

        Catch ex As Exception
            ErrDisp("ArasSendOrder : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function ArasQueryOrder(cSiparisNo As String, Optional ByRef cSonuc As String = "", Optional ByRef cErrorMessage As String = "") As Boolean

        ArasQueryOrder = True

        Try
            ArasQueryOrder = ArasQueryOrder1(cSiparisNo, cSonuc, cErrorMessage)

        Catch ex As Exception
            ErrDisp("ArasQueryOrder : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

#End Region

#Region "Yurtici"

    Public Sub YurticiSysPar()
        Try
            Dim ofrmYurtici As New frmYurtici
            ofrmYurtici.ShowDialog()

        Catch ex As Exception
            ErrDisp("YurticiSysPar : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Function YurticiSendOrder(cSiparisNo As String, Optional ByRef cSonuc As String = "", Optional ByRef cErrorMessage As String = "") As Boolean

        YurticiSendOrder = True

        Try
            YurticiSendOrder = YurticiSendOrder1(cSiparisNo, cSonuc, cErrorMessage)
        Catch ex As Exception
            ErrDisp("YurticiSendOrder : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function YurticiQueryOrder(cSiparisNo As String, Optional ByRef cSonuc As String = "", Optional ByRef cErrorMessage As String = "") As Boolean

        YurticiQueryOrder = True

        Try
            YurticiQueryOrder = YurticiQueryOrder1(cSiparisNo, cSonuc, cErrorMessage)
        Catch ex As Exception
            ErrDisp("YurticiQueryOrder : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function YurticiCancelOrder(ByVal cSiparisNo As String, Optional ByRef cErrorMessage As String = "") As Boolean

        YurticiCancelOrder = True

        Try
            YurticiCancelOrder = YurticiCancelOrder1(cSiparisNo, cErrorMessage)
        Catch ex As Exception
            ErrDisp("YurticiCancelOrder : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

#End Region

#Region "PTT"

    Public Sub PTTSysPar()
        Try
            Dim ofrmPTT As New frmPTT
            ofrmPTT.ShowDialog()

        Catch ex As Exception
            ErrDisp("PTTSysPar : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Function PTTIlceSorgula() As Boolean

        PTTIlceSorgula = False

        Try
            PTTIlceSorgula = PTTIlceSorgula1()

        Catch ex As Exception
            ErrDisp("PTTIlceSorgula",,,, ex)
        End Try
    End Function

    Public Function PTTEkHizmetSorgula() As Boolean

        PTTEkHizmetSorgula = False

        Try
            PTTEkHizmetSorgula = PTTEkHizmetSorgula1()

        Catch ex As Exception
            ErrDisp("PTTEkHizmetSorgula",,,, ex)
        End Try
    End Function

    Public Function PTTSendOrder(cSiparisNo As String, Optional ByRef cSonuc As String = "") As Boolean

        PTTSendOrder = False

        Try
            PTTSendOrder = PTTSendOrder1(cSiparisNo, cSonuc)
            ' 2752938850810                 
            ' 880000000200                  
        Catch ex As Exception
            ErrDisp("PTTSendOrder",,,, ex)
        End Try
    End Function

    Public Function PTTQueryOrder(cSiparisNo As String, Optional ByRef cSonuc As String = "", Optional ByRef cErrorMessage As String = "") As Boolean

        PTTQueryOrder = False

        Try
            PTTQueryOrder = PTTQueryOrder1(cSiparisNo, cSonuc, cErrorMessage)
        Catch ex As Exception
            ErrDisp("PTTQueryOrder",,,, ex)
        End Try
    End Function

    Public Function PTTCancelOrder(cSiparisNo As String, Optional ByRef cErrorMessage As String = "") As Boolean

        PTTCancelOrder = False

        Try
            PTTCancelOrder = PTTCancelOrder1(cSiparisNo, cErrorMessage)
        Catch ex As Exception
            ErrDisp("PTTCancelOrder",,,, ex)
        End Try
    End Function

#End Region

#Region "Byexpress"

    Public Sub ByExpressSysPar()
        Try
            Dim ofrmByExpress As New frmByExpress
            ofrmByExpress.ShowDialog()

        Catch ex As Exception
            ErrDisp("ByExpressSysPar : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Function ByExpressSendOrder(cSiparisNo As String, Optional ByRef cKargoTakipNo As String = "", Optional ByRef cErrorMessage As String = "", Optional ByVal lTest As Boolean = False) As Boolean

        ByExpressSendOrder = False

        Try
            ByExpressSendOrder = ByExpressGonderiYukle(cSiparisNo, cKargoTakipNo, cErrorMessage, lTest)
        Catch ex As Exception
            ErrDisp("ByExpressSendOrder",,,, ex)
        End Try
    End Function

    Public Function ByExpressCancelOrder(cSiparisNo As String, Optional ByRef cErrorMessage As String = "", Optional ByVal lTest As Boolean = False) As Boolean

        ByExpressCancelOrder = False

        Try
            ByExpressCancelOrder = ByExpressGonderiIptal(cSiparisNo, cErrorMessage, lTest)
        Catch ex As Exception
            ErrDisp("ByExpressCancelOrder",,,, ex)
        End Try
    End Function

    Public Function ByExpressQueryOrder(Optional cSiparisNo As String = "", Optional ByVal cKargoTakipNo As String = "", Optional ByRef cSonuc As String = "", Optional ByRef cErrorMessage As String = "", Optional ByVal lTest As Boolean = False) As Boolean

        ByExpressQueryOrder = False

        Try
            ByExpressQueryOrder = ByExpressSonDurumuAl(cSiparisNo, cKargoTakipNo, cSonuc, cErrorMessage, lTest)
        Catch ex As Exception
            ErrDisp("ByExpressQueryOrder",,,, ex)
        End Try
    End Function

#End Region

#Region "MNG"

    Public Sub MNGSysPar()
        Try
            Dim ofrmMNG As New frmMNG
            ofrmMNG.ShowDialog()

        Catch ex As Exception
            ErrDisp("MNGSysPar : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Function MNGSendOrder(cSiparisNo As String, Optional ByRef cSonuc As String = "", Optional ByRef cErrorMessage As String = "") As Boolean

        MNGSendOrder = False

        Try
            MNGSendOrder = MNGSendOrder2(cSiparisNo, cSonuc, cErrorMessage)
        Catch ex As Exception
            ErrDisp("MNGSendOrder",,,, ex)
        End Try
    End Function

    Public Function MNGQueryOrder(cSiparisNo As String, Optional ByRef cSonuc As String = "", Optional ByRef cErrorMessage As String = "") As Boolean

        MNGQueryOrder = False

        Try
            MNGQueryOrder = MNGQueryOrder2(cSiparisNo, cSonuc, cErrorMessage)
        Catch ex As Exception
            ErrDisp("MNGQueryOrder",,,, ex)
        End Try
    End Function

    Public Function MNGCancelOrder(cSiparisNo As String, Optional ByRef cErrorMessage As String = "") As Boolean

        MNGCancelOrder = False

        Try
            MNGCancelOrder = MNGCancelOrder2(cSiparisNo, cErrorMessage)
        Catch ex As Exception
            ErrDisp("MNGCancelOrder",,,, ex)
        End Try
    End Function

#End Region

#Region "NAC"

    Public Sub NacSysPar()
        Try
            Dim ofrmNacSms As New frmNacSms
            ofrmNacSms.ShowDialog()

        Catch ex As Exception
            ErrDisp("NacSysPar : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Function GetNacCredit() As Double

        GetNacCredit = 0

        Try
            GetNacCredit = NacGetCredit()

        Catch ex As Exception
            ErrDisp("GetNacCredit : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function SendNacSms(cTelNo As String, Optional cTitle As String = "Dikkat", Optional cMessage As String = "Test mesajıdır", Optional ByRef nPkgID As Double = 0) As Boolean

        SendNacSms = True

        Try
            SendNacSms = NacSendSms(cTelNo, cTitle, cMessage, nPkgID)

        Catch ex As Exception
            ErrDisp("SendNacSms : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function SendSmsSipPerakende(cSiparisNo As String) As Boolean

        SendSmsSipPerakende = False

        Try
            If cSiparisNo.Trim = "" Then Exit Function

            Dim oSQL As New SQLServerClass
            Dim cTelNo As String = ""
            Dim cAcentaTelNo As String = ""
            Dim cStatu As String = ""
            Dim cTitle As String = ""
            Dim cMessage As String = ""
            Dim cMessage2 As String = ""
            Dim cLogMessage As String = ""
            Dim nPkgID As Double = 0
            Dim nTelNo As Double = 0
            Dim nAcentaTelNo As Double = 0
            Dim cAdi As String = ""
            Dim cSoyadi As String = ""
            Dim cKargoFirmasi As String = ""
            Dim cTeslimSube As String = ""
            Dim cTakipNo As String = ""
            Dim cFirma As String = ""
            Dim cSmsKargoStatu As String = ""
            Dim lSMSGonderildi As Boolean = False

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select top 1 adi, soyadi, telefon, kargofirmasi, aracikargo, " +
                            " kargostatu, cikis_sube, teslim_sube, firma, kargotakipno, smskargostatu, " +
                            " acentatel = (select top 1 telex from firma with (NOLOCK) where firma = sipperakende.firma) " +
                            " from sipperakende with (NOLOCK) " +
                            " where telefon is not null " +
                            " and telefon <> '' " +
                            " and siparisno  = '" + cSiparisNo.Trim + "' "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then

                cAdi = oSQL.SQLReadString("adi")
                cSoyadi = oSQL.SQLReadString("soyadi")
                cKargoFirmasi = oSQL.SQLReadString("kargofirmasi")
                cTeslimSube = oSQL.SQLReadString("teslim_sube")
                cTakipNo = oSQL.SQLReadString("kargotakipno")
                cFirma = oSQL.SQLReadString("firma")

                cSmsKargoStatu = oSQL.SQLReadString("smskargostatu")
                cStatu = oSQL.SQLReadString("kargostatu")
                cTitle = oSQL.SQLReadString("firma")

                cTelNo = oSQL.SQLReadString("telefon")

                If IsNumeric(cTelNo) Then
                    If CDbl(cTelNo) > 5000000000 Then
                        cTelNo = Format(CDbl(cTelNo), "00000000000")
                    Else
                        cTelNo = ""
                    End If
                Else
                    cTelNo = ""
                End If

                cAcentaTelNo = oSQL.SQLReadString("acentatel")

                If IsNumeric(cAcentaTelNo) Then
                    If CDbl(cAcentaTelNo) > 5000000000 Then
                        cAcentaTelNo = Format(CDbl(cAcentaTelNo), "00000000000")
                    Else
                        cAcentaTelNo = ""
                    End If
                Else
                    cAcentaTelNo = ""
                End If

                Select Case cStatu
                    Case "SIPARIS HAZIRLANIYOR"
                        cMessage = cFirma + " " + cSiparisNo.Trim + " siparisiniz hazirlaniyor. " +
                                   cKargoFirmasi
                    Case "TESLIM EDILDI"
                        cMessage = cFirma + " " + cSiparisNo.Trim + " siparisiniz teslim edildi. " +
                                   cKargoFirmasi + " kargo takip no " + cTakipNo
                    Case "CIKIS SUBEDE"
                        cMessage = cFirma + " " + cSiparisNo.Trim + " siparisiniz cikis subede. " +
                                   cKargoFirmasi + " kargo takip no " + cTakipNo
                    Case "DAGITIMDA"
                        cMessage = cFirma + " " + cSiparisNo.Trim + " siparisiniz dagitimda. " +
                                   cKargoFirmasi + " kargo takip no " + cTakipNo
                    Case "İPTAL EDİLDİ"
                        cMessage = cFirma + " " + cSiparisNo.Trim + " siparisiniz iptal edildi. " +
                                   cKargoFirmasi + " kargo takip no " + cTakipNo
                    Case "SIPARIS ALINDI"
                        cMessage = cFirma + " " + cSiparisNo.Trim + " siparisiniz alindi. " +
                                   cKargoFirmasi + " kargo takip no " + cTakipNo
                    Case "IADE"
                        cMessage = cFirma + " " + cSiparisNo.Trim + " siparisiniz iade edildi. " +
                                   cKargoFirmasi + " kargo takip no " + cTakipNo
                    Case "VARIS SUBEDE"
                        cMessage = cFirma + " " + cSiparisNo.Trim + " siparisiniz " + cKargoFirmasi + " " + cTeslimSube + " varış subesinde. " +
                                   cKargoFirmasi + " kargo takip no " + cTakipNo
                    Case "SORUNLU"
                        cMessage = cFirma + " " + cSiparisNo.Trim + " siparisiniz iade edilebilir." +
                                   "Teslim almak icin " + cKargoFirmasi + " kargo " + cTeslimSube + " ile irtibata geciniz. Takip no " + cTakipNo
                    Case "YOLDA"
                        cMessage = cFirma + " " + cSiparisNo.Trim + " siparisiniz yolda. " +
                                   cKargoFirmasi + " kargo takip no " + cTakipNo
                End Select
            End If
            oSQL.oReader.Close()

            If cTelNo <> "" And cMessage.Trim <> "" And cSmsKargoStatu.Trim <> cStatu.Trim Then

                lSMSGonderildi = False

                If NacSendSms(cTelNo, cTitle, cMessage, nPkgID) Then

                    oSQL.cSQLQuery = "update sipperakende " +
                                    " set smskargostatu = '" + cStatu + "', " +
                                    " smsgonderme = getdate() " +
                                    " where siparisno = '" + cSiparisNo.Trim + "' "

                    oSQL.SQLExecute()

                    cLogMessage = "Başarılı Müşteri SMS gönderimi : " + nPkgID.ToString + " " + cMessage + " " + cTelNo
                    CreateLog("SMSMusteriLog", cLogMessage)

                    lSMSGonderildi = True

                    SendSmsSipPerakende = True
                Else
                    cLogMessage = "BAŞARISIZ Müşteri SMS gönderimi : " + cMessage + " " + cTelNo
                    CreateLog("SMSMusteriErr", cLogMessage)
                End If

                If cAcentaTelNo <> "" And lSMSGonderildi And cStatu = "SORUNLU" Then

                    cMessage = cAdi + " " + cSoyadi + " " + cTelNo + " müşterisinin " + cSiparisNo + " siparişi sorunludur." +
                               cKargoFirmasi + " kargo " + cTeslimSube + " ile irtibata geciniz." +
                               "Takip no " + cTakipNo

                    If NacSendSms(cAcentaTelNo, cTitle, cMessage, nPkgID) Then
                        cLogMessage = "Başarılı Acenta SMS gönderimi : " + nPkgID.ToString + " " + cMessage + " " + cAcentaTelNo
                        CreateLog("SMSAcentaLog", cLogMessage)
                    Else
                        cLogMessage = "BAŞARISIZ Acenta SMS gönderimi : " + cMessage + " " + cAcentaTelNo
                        CreateLog("SMSAcentaErr", cLogMessage)
                    End If
                End If
            End If

            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp("SendSmsSipPerakende : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

#End Region

#Region "Init / Test / Version"

    Public Shared Function TestWinTexMNG() As String

        TestWinTexMNG = "TestWinTexMNG ÇALIŞMIYOR"

        Try
            Return "TestWinTexMNG Çalışıyor"
        Catch ex As Exception
            ErrDisp("TestWinTexMNG  " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function ShowVersionInfo() As String

        ShowVersionInfo = ""

        Try
            ShowVersionInfo = My.Application.Info.Version.ToString.Trim

        Catch ex As Exception
            ErrDisp("ShowVersionInfo  " + ex.Message, "HTMain")
        End Try
    End Function

    Public Function init(Optional cServer As String = "", Optional cDatabase As String = "", Optional cUser As String = "sa", Optional cPassword As String = "") As Boolean
        ' init database connection
        Dim oSQL As SQLServerClass

        init = False

        Try
            oConnection.cServer = cServer.Trim
            oConnection.cDatabase = cDatabase.Trim
            oConnection.cUser = cUser.Trim
            oConnection.cPassword = cPassword.Trim

            oConnection.cConnStr = "Data Source=" + oConnection.cServer + ";" +
                                    "Initial Catalog=" + oConnection.cDatabase + ";" +
                                    "uid=" + oConnection.cUser + ";" +
                                    "pwd=" + oConnection.cPassword + ""

            oSQL = New SQLServerClass

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select top 1 firmname " +
                                " from sysinfo with (NOLOCK) " +
                                " where firmname Is Not null " +
                                " And firmname <> '' "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                oConnection.cOwner = oSQL.SQLReadString("firmname").ToLower
            End If
            oSQL.oReader.Close()

            oSQL.CloseConn()
            oSQL = Nothing

            GetInterlineExpressParameters()
            GetMNGParameters()
            GetByExpressParameters()
            GetPTTParameters()
            GetNacParameters()
            GetEContParameters()
            GetYurticiParameters()
            GetKargoZamaniParameters()
            GetArasParameters()

            init = True

        Catch ex As Exception
            init = False
            ErrDisp("SQL Connect : " + ex.Message + " " + cServer.Trim + " " + cDatabase.Trim + " " + cUser.Trim + " " + cPassword.Trim,,,, ex)
        End Try
    End Function

#End Region

#Region "Perakende Sipariş Toplu İşlemler"

    Public Function SipPerakendeSonDurum(Optional nCase As Integer = 1, Optional lForceExecute As Boolean = False) As Boolean

        SipPerakendeSonDurum = False

        Try
            Dim tBegin As DateTime = DateTime.Parse("8:00:00 AM")
            Dim tEnd As DateTime = DateTime.Parse("10:00:00 PM")

            If lForceExecute Then
                CreateLog(, "SipPerakendeSonDurum Force Execute")
            Else
                If tBegin.TimeOfDay < DateTime.Now.TimeOfDay And
                    tEnd.TimeOfDay > DateTime.Now.TimeOfDay And
                    DateTime.Now.DayOfWeek <> DayOfWeek.Sunday Then
                    ' ok execute
                Else
                    Exit Function
                End If
            End If

            Dim oSQL As New SQLServerClass
            Dim cErrorMessage As String = ""
            Dim cMessage As String = ""
            Dim cSiparisNo As String = ""
            Dim cKargoFirmasi As String = ""
            Dim cKargoSonDurumKodu As String = ""
            Dim cSonuc As String = ""
            Dim cKargoTakipNo As String = ""
            Dim cBuffer As String = ""
            Dim aSiparis() As String
            Dim nCnt As Integer = 0
            Dim nCnt1 As Integer = 0
            Dim nCntSiparis As Integer = -1
            Dim lOK As Boolean = False
            Dim lMNG_Calisilmasin As Boolean = False
            Dim lBYEXPRESS_Calisilmasin As Boolean = False
            Dim lPTT_Calisilmasin As Boolean = False
            Dim lYURTICI_Calisilmasin As Boolean = False
            Dim lAras_Calisilmasin As Boolean = False
            Dim aAraciKargo() As String

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select top 1 firma " +
                            " from firma with (NOLOCK) " +
                            " where firma = 'ARAS' " +
                            " and calisilmasin = 'E' "

            lAras_Calisilmasin = oSQL.CheckExists()

            oSQL.cSQLQuery = "select top 1 firma " +
                            " from firma with (NOLOCK) " +
                            " where firma = 'MNG' " +
                            " and calisilmasin = 'E' "

            lMNG_Calisilmasin = oSQL.CheckExists()

            oSQL.cSQLQuery = "select top 1 firma " +
                            " from firma with (NOLOCK) " +
                            " where firma = 'BYEXPRESS' " +
                            " and calisilmasin = 'E' "

            lBYEXPRESS_Calisilmasin = oSQL.CheckExists()

            oSQL.cSQLQuery = "select top 1 firma " +
                            " from firma with (NOLOCK) " +
                            " where firma = 'PTT' " +
                            " and calisilmasin = 'E' "

            lPTT_Calisilmasin = oSQL.CheckExists()

            oSQL.cSQLQuery = "select top 1 firma " +
                            " from firma with (NOLOCK) " +
                            " where firma = 'YURTICI' " +
                            " and calisilmasin = 'E' "

            lYURTICI_Calisilmasin = oSQL.CheckExists()

            oSQL.cSQLQuery = "update sipperakende " +
                            " set kargostatu = 'SIPARIS ALINDI' " +
                            " where (yazdirildi is null or yazdirildi = 'H' or yazdirildi = '') " +
                            " and (kapandi = 'H' or kapandi is null or kapandi = '') " +
                            " and (iptal = 'H' or iptal is null or iptal = '') " +
                            " and (iade = 'H' or iade is null or iade = '') "

            oSQL.SQLExecute()

            'oSQL.cSQLQuery = "update sipperakende " +
            '                " set kargostatu = 'SIPARIS HAZIRLANIYOR' " +
            '                " where yazdirildi = 'E' " +
            '                " and (kapandi = 'H' or kapandi is null or kapandi = '') " +
            '                " and (iptal = 'H' or iptal is null or iptal = '') " +
            '                " and (iade = 'H' or iade is null or iade = '') " +
            '                " and (kargostatu is null or kargostatu = '') "

            'oSQL.SQLExecute()

            ' ByExpress , PTT , Yurtici , Aras

            Select Case nCase
                Case 1
                    ' kapanmamış, iptal olmamış, iade olmamışlar 
                    oSQL.cSQLQuery = "Select siparisno, kargofirmasi, kargosondurumkodu, kargotakipno, kargostatutarihi, aracikargo " +
                            " from sipperakende with (NOLOCK) " +
                            " where (kapandi = 'H' or kapandi is null or kapandi = '') " +
                            " and (iade = 'H' or iade is null or iade = '') " +
                            " and tarih > dateadd(day,-365,getdate()) " +
                            " and siparisno is not null " +
                            " and siparisno <> '' " +
                            " and kargofirmasi in ('BYEXPRESS','PTT','YURTICI','ARAS') " +
                            " and kargoyakayityollandi = 'E' " +
                            " union "

                    oSQL.cSQLQuery = oSQL.cSQLQuery +
                            " Select siparisno, kargofirmasi, kargosondurumkodu, kargotakipno, kargostatutarihi, aracikargo " +
                            " from sipperakende with (NOLOCK) " +
                            " where (kapandi = 'H' or kapandi is null or kapandi = '') " +
                            " and (iade = 'H' or iade is null or iade = '') " +
                            " and tarih > dateadd(day,-365,getdate()) " +
                            " and siparisno is not null " +
                            " and siparisno <> '' " +
                            " and kargofirmasi = 'ARAS' " +
                            " order by kargostatutarihi, siparisno "
                Case = 2
                    ' iade siparisler
                    oSQL.cSQLQuery = "Select siparisno, kargofirmasi, kargosondurumkodu, kargotakipno, aracikargo " +
                            " from sipperakende with (NOLOCK) " +
                            " where iade = 'E' " +
                            " and kargoyakayityollandi = 'E' " +
                            " and kargoyakayityollanmatarihi > dateadd(day,-62,getdate()) " +
                            " and siparisno is not null " +
                            " and siparisno <> '' " +
                            " and kargofirmasi in ('BYEXPRESS','PTT','YURTICI','ARAS') " +
                            " order by kargostatutarihi, siparisno "
                Case 3
                    ' kapali siparisler
                    oSQL.cSQLQuery = "Select siparisno, kargofirmasi, kargosondurumkodu, kargotakipno, aracikargo " +
                            " from sipperakende with (NOLOCK) " +
                            " where kapandi = 'E' " +
                            " and kargoyakayityollandi = 'E' " +
                            " and kargoyakayityollanmatarihi > dateadd(day,-62,getdate()) " +
                            " and siparisno is not null " +
                            " and siparisno <> '' " +
                            " and kargofirmasi in ('BYEXPRESS','PTT','YURTICI','ARAS') " +
                            " order by kargostatutarihi, siparisno "
                Case 4
                    ' iptal siparisler
                    oSQL.cSQLQuery = "Select siparisno, kargofirmasi, kargosondurumkodu, kargotakipno, aracikargo " +
                            " from sipperakende with (NOLOCK) " +
                            " where iptal = 'E' " +
                            " and kargoyakayityollandi = 'E' " +
                            " and kargoyakayityollanmatarihi > dateadd(day,-62,getdate()) " +
                            " and siparisno is not null " +
                            " and siparisno <> '' " +
                            " and kargofirmasi in ('BYEXPRESS','PTT','YURTICI','ARAS') " +
                            " order by kargostatutarihi, siparisno "
            End Select

            oSQL.GetSQLReader()

            Do While oSQL.oReader.Read

                If oSQL.SQLReadString("aracikargo") = "FAVORI 2024" Or
                    oSQL.SQLReadString("aracikargo") = "MARES BESIKTAS" Or
                    oSQL.SQLReadString("aracikargo") = "INTERLINE DAGITIM" Then

                    PostmanSonDurumuAl(oSQL.SQLReadString("siparisno"), cSonuc, cErrorMessage, 1)
                    CreateLog("WinTexPostmanLog", "Siparis FAVORI sorgulandı : " + oSQL.SQLReadString("siparisno"))

                    PostmanSonDurumuAl(oSQL.SQLReadString("siparisno"), cSonuc, cErrorMessage, 2)
                    CreateLog("WinTexPostmanLog", "Siparis MARES sorgulandı : " + oSQL.SQLReadString("siparisno"))

                    PostmanSonDurumuAl(oSQL.SQLReadString("siparisno"), cSonuc, cErrorMessage, 3)
                    CreateLog("WinTexPostmanLog", "Siparis INTERLINE sorgulandı : " + oSQL.SQLReadString("siparisno"))
                End If

                'If oSQL.SQLReadString("aracikargo") = "MARES BESIKTAS" Then
                '    PostmanSonDurumuAl(oSQL.SQLReadString("siparisno"), cSonuc, cErrorMessage, 2)
                '    CreateLog("WinTexPostmanLog", "Siparis MARES sorgulandı : " + oSQL.SQLReadString("siparisno"))
                'End If

                'If oSQL.SQLReadString("aracikargo") = "INTERLINE DAGITIM" Then
                '    PostmanSonDurumuAl(oSQL.SQLReadString("siparisno"), cSonuc, cErrorMessage, 3)
                '    CreateLog("WinTexPostmanLog", "Siparis INTERLINE sorgulandı : " + oSQL.SQLReadString("siparisno"))
                'End If

                Select Case oSQL.SQLReadString("kargofirmasi").ToLower
                    Case "aras"
                        If Not lAras_Calisilmasin Then
                            If ArasQueryOrder1(oSQL.SQLReadString("siparisno"), cSonuc, cErrorMessage) Then
                                CreateLog("WinTexArasLog", "Siparis Aras kargoda sorgulandı : " + oSQL.SQLReadString("siparisno") + vbCrLf +
                                          "Sonuç : " + cSonuc + vbCrLf +
                                          "Hata Mesajı : " + cErrorMessage + vbCrLf)
                            Else
                                CreateLog("WinTexArasLog", "Dikkat siparis Aras kargoda bulunamadi : " + oSQL.SQLReadString("siparisno") + vbCrLf +
                                          "Hata Mesajı : " + cErrorMessage + vbCrLf)
                            End If
                        End If

                    Case "byexpress"
                        If Not lBYEXPRESS_Calisilmasin Then
                            If ByExpressSonDurumuAl(oSQL.SQLReadString("siparisno"), oSQL.SQLReadString("kargotakipno"), cSonuc, cErrorMessage) Then
                                cMessage = Replace$(cSonuc, ";;", vbCrLf)
                                CreateLog("WinTexByexpressLog", "Siparis ByExpress kargoda sorgulandı : " + oSQL.SQLReadString("siparisno") + vbCrLf +
                                          "Sonuç : " + cSonuc + vbCrLf +
                                          "Hata Mesajı : " + cErrorMessage + vbCrLf)
                            Else
                                CreateLog("WinTexByexpressLog", "Dikkat siparis ByExpress kargoda bulunamadi : " + oSQL.SQLReadString("siparisno") + vbCrLf +
                                          "Hata Mesajı : " + cErrorMessage + vbCrLf)
                            End If
                        End If
                    Case "ptt"
                        If Not lPTT_Calisilmasin Then
                            If PTTQueryOrder1(oSQL.SQLReadString("siparisno"), cSonuc, cErrorMessage) Then
                                CreateLog("WinTexPTTLog", "Siparis PTT kargoda sorgulandı : " + oSQL.SQLReadString("siparisno") + vbCrLf +
                                          "Sonuç : " + cSonuc + vbCrLf +
                                          "Hata Mesajı : " + cErrorMessage + vbCrLf)
                            Else
                                CreateLog("WinTexPTTLog", "Dikkat siparis PTT kargoda bulunamadi : " + oSQL.SQLReadString("siparisno") + vbCrLf +
                                          "Hata Mesajı : " + cErrorMessage + vbCrLf)
                            End If
                        End If
                    Case "yurtici"
                        If Not lYURTICI_Calisilmasin Then
                            If YurticiQueryOrder1(oSQL.SQLReadString("siparisno"), cSonuc, cErrorMessage) Then
                                CreateLog("WinTexYurticiLog", "Siparis Yurtici kargoda sorgulandı : " + oSQL.SQLReadString("siparisno") + vbCrLf +
                                          "Sonuç : " + cSonuc + vbCrLf +
                                          "Hata Mesajı : " + cErrorMessage + vbCrLf)
                            Else
                                CreateLog("WinTexYurticiLog", "Dikkat siparis Yurtici kargoda bulunamadi : " + oSQL.SQLReadString("siparisno") + vbCrLf +
                                          "Hata Mesajı : " + cErrorMessage + vbCrLf)
                            End If
                        End If
                End Select
            Loop
            oSQL.oReader.Close()

            ' MNG 
            If Not lMNG_Calisilmasin Then

                oSQL.cSQLQuery = "select distinct aracikargo " +
                                " from sipperakende with (NOLOCK) " +
                                " where aracikargo is not null " +
                                " and aracikargo <> '' " +
                                " and kargoyakayityollandi = 'E' " +
                                " and siparisno is not null " +
                                " and siparisno <> '' " +
                                " and kargofirmasi = 'MNG' " +
                                " order by aracikargo "

                aAraciKargo = oSQL.SQLtoStringArray()

                For nCnt1 = 0 To UBound(aAraciKargo)

                    nCntSiparis = -1
                    nCnt = 0
                    cBuffer = ""
                    lOK = False
                    ReDim aSiparis(0)

                    Select Case nCase
                        Case 1
                            ' kapanmamış, iptal olmamış, iade olmamışlar 
                            oSQL.cSQLQuery = "Select siparisno, kargofirmasi, kargosondurumkodu, kargotakipno " +
                            " from sipperakende with (NOLOCK) " +
                            " where (kapandi = 'H' or kapandi is null or kapandi = '') " +
                            " and (iptal = 'H' or iptal is null or iptal = '') " +
                            " and (iade = 'H' or iade is null or iade = '') " +
                            " and kargoyakayityollandi = 'E' "
                        Case = 2
                            ' iade siparisler
                            oSQL.cSQLQuery = "Select siparisno, kargofirmasi, kargosondurumkodu, kargotakipno " +
                            " from sipperakende with (NOLOCK) " +
                            " where iade = 'E' " +
                            " and kargoyakayityollandi = 'E' " +
                            " and kargoyakayityollanmatarihi > dateadd(day,-62,getdate()) "
                        Case 3
                            ' kapali siparisler
                            oSQL.cSQLQuery = "Select siparisno, kargofirmasi, kargosondurumkodu, kargotakipno " +
                            " from sipperakende with (NOLOCK) " +
                            " where kapandi = 'E' " +
                            " and kargoyakayityollandi = 'E' " +
                            " and kargoyakayityollanmatarihi > dateadd(day,-62,getdate()) "
                        Case 4
                            ' iptal siparisler
                            oSQL.cSQLQuery = "Select siparisno, kargofirmasi, kargosondurumkodu, kargotakipno " +
                            " from sipperakende with (NOLOCK) " +
                            " where iptal = 'E' " +
                            " and kargoyakayityollandi = 'E' " +
                            " and kargoyakayityollanmatarihi > dateadd(day,-62,getdate()) "
                    End Select

                    oSQL.cSQLQuery = oSQL.cSQLQuery +
                            " and siparisno is not null " +
                            " and siparisno <> '' " +
                            " and kargofirmasi = 'MNG' " +
                            " and aracikargo = '" + aAraciKargo(nCnt1) + "' " +
                            " order by kargostatutarihi, siparisno "

                    oSQL.GetSQLReader()

                    Do While oSQL.oReader.Read
                        lOK = True
                        If cBuffer.Trim = "" Then
                            cBuffer = oSQL.SQLReadString("siparisno")
                        Else
                            cBuffer = cBuffer + ";" + oSQL.SQLReadString("siparisno")
                        End If
                        If nCnt = 49 Then
                            nCntSiparis = nCntSiparis + 1
                            ReDim Preserve aSiparis(nCntSiparis)
                            aSiparis(nCntSiparis) = cBuffer

                            nCnt = 0
                            cBuffer = ""
                        Else
                            nCnt = nCnt + 1
                        End If
                    Loop
                    oSQL.oReader.Close()

                    If lOK Then
                        If cBuffer.Trim <> "" Then
                            nCntSiparis = nCntSiparis + 1
                            ReDim Preserve aSiparis(nCntSiparis)
                            aSiparis(nCntSiparis) = cBuffer
                        End If

                        For nCnt = 0 To aSiparis.GetUpperBound(0)

                            cSonuc = ""
                            cErrorMessage = ""
                            cMessage = ""

                            If MNGQueryOrder2(aSiparis(nCnt), cSonuc, cErrorMessage) Then
                                cMessage = Replace$(cSonuc, ";;", vbCrLf)
                                CreateLog("WinTexMNGLog", "Siparis MNG kargoda sorgulandı : " + aSiparis(nCnt) + vbCrLf +
                                      "Sonuç : " + cSonuc + vbCrLf +
                                      "Hata Mesajı : " + cErrorMessage + vbCrLf)
                            Else
                                CreateLog("WinTexMNGLog", "Dikkat siparis MNG kargoda bulunamadi : " + aSiparis(nCnt) + vbCrLf +
                                      "Hata Mesajı : " + cErrorMessage + vbCrLf)
                            End If
                        Next
                    End If
                Next
            End If

            ' iptal siparişlerin statusunu güncelle
            oSQL.cSQLQuery = "update sipperakende " +
                            " set kargostatu = 'İPTAL EDİLDİ' " +
                            " where iptal = 'E' "
            oSQL.SQLExecute()

            'oSQL.cSQLQuery = "update sipperakende " +
            '                " set kargostatu = 'YOLDA' " +
            '                " where yazdirildi = 'E' " +
            '                " and (kapandi = 'H' or kapandi is null or kapandi = '') " +
            '                " and (iptal = 'H' or iptal is null or iptal = '') " +
            '                " and (iade = 'H' or iade is null or iade = '') " +
            '                " and kargostatu not in ('SIPARIS ALINDI','SIPARIS HAZIRLANIYOR','CIKIS SUBEDE','İPTAL EDİLDİ','VARIS SUBEDE','DAGITIMDA','TESLIM EDILDI','SORUNLU','IADE') "
            'oSQL.SQLExecute()

            oSQL.CloseConn()

            oSQL = Nothing

            SipPerakendeSonDurum = True

        Catch ex As Exception
            ErrDisp("SipPerakendeSonDurum",,,, ex)
        End Try
    End Function

    Public Function YenidenYolla() As Boolean

        YenidenYolla = False

        Try
            Dim oSQL As New SQLServerClass
            Dim cErrorMessage As String = ""
            Dim cKargoTakipNo As String = ""
            Dim cSonuc As String = ""
            Dim lMNG_Calisilmasin As Boolean = False
            Dim lBYEXPRESS_Calisilmasin As Boolean = False
            Dim lPTT_Calisilmasin As Boolean = False
            Dim lYURTICI_Calisilmasin As Boolean = False
            Dim lAras_Calisilmasin As Boolean = False

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select top 1 firma " +
                            " from firma with (NOLOCK) " +
                            " where firma = 'ARAS' " +
                            " and calisilmasin = 'E' "

            lAras_Calisilmasin = oSQL.CheckExists()

            oSQL.cSQLQuery = "select top 1 firma " +
                            " from firma with (NOLOCK) " +
                            " where firma = 'MNG' " +
                            " and calisilmasin = 'E' "

            lMNG_Calisilmasin = oSQL.CheckExists()

            oSQL.cSQLQuery = "select top 1 firma " +
                            " from firma with (NOLOCK) " +
                            " where firma = 'BYEXPRESS' " +
                            " and calisilmasin = 'E' "

            lBYEXPRESS_Calisilmasin = oSQL.CheckExists()

            oSQL.cSQLQuery = "select top 1 firma " +
                            " from firma with (NOLOCK) " +
                            " where firma = 'PTT' " +
                            " and calisilmasin = 'E' "

            lPTT_Calisilmasin = oSQL.CheckExists()

            oSQL.cSQLQuery = "select top 1 firma " +
                            " from firma with (NOLOCK) " +
                            " where firma = 'YURTICI' " +
                            " and calisilmasin = 'E' "

            lYURTICI_Calisilmasin = oSQL.CheckExists()

            oSQL.cSQLQuery = "select siparisno, kargofirmasi, aracikargo " +
                            " from sipperakende with (NOLOCK) " +
                            " where convert(date,yazdirmatarihi) >= dateadd(day,-30,getdate()) " +
                            " and yazdirildi = 'E' " +
                            " and (kargotakipno is null or kargotakipno = '') " +
                            " and siparisno is not null " +
                            " and siparisno <> '' " +
                            " and kargofirmasi is not null " +
                            " and kargofirmasi <> '' " +
                            " order by siparisno "

            oSQL.GetSQLReader()

            Do While oSQL.oReader.Read

                If oSQL.SQLReadString("aracikargo") = "FAVORI 2024" Then

                    PostmanGonderiYukle(oSQL.SQLReadString("siparisno"), cSonuc, cErrorMessage, 1)
                    CreateLog("WinTexPostmanLog", "Siparis FAVORI yollandı : " + oSQL.SQLReadString("siparisno") + vbCrLf +
                              "Sonuç : " + cSonuc + vbCrLf +
                              "Hata mesajı : " + cErrorMessage + vbCrLf)
                End If

                If oSQL.SQLReadString("aracikargo") = "MARES BESIKTAS" Then

                    PostmanGonderiYukle(oSQL.SQLReadString("siparisno"), cSonuc, cErrorMessage, 2)
                    CreateLog("WinTexPostmanLog", "Siparis MARES yollandı : " + oSQL.SQLReadString("siparisno") + vbCrLf +
                              "Sonuç : " + cSonuc + vbCrLf +
                              "Hata mesajı : " + cErrorMessage + vbCrLf)
                End If

                If oSQL.SQLReadString("aracikargo") = "INTERLINE DAGITIM" Then

                    PostmanGonderiYukle(oSQL.SQLReadString("siparisno"), cSonuc, cErrorMessage, 3)
                    CreateLog("WinTexPostmanLog", "Siparis INTERLINE yollandı : " + oSQL.SQLReadString("siparisno") + vbCrLf +
                              "Sonuç : " + cSonuc + vbCrLf +
                              "Hata mesajı : " + cErrorMessage + vbCrLf)
                End If

                Select Case oSQL.SQLReadString("kargofirmasi").ToLower
                    Case "aras"
                        If Not lAras_Calisilmasin Then
                            If ArasSendOrder1(oSQL.SQLReadString("siparisno"), cSonuc, cErrorMessage) Then
                                CreateLog("WinTexArasLog", "Siparis Aras kargoya yollandı : " + oSQL.SQLReadString("siparisno") + vbCrLf +
                                          "Sonuç : " + cSonuc + vbCrLf +
                                          "Hata mesajı : " + cErrorMessage + vbCrLf)
                            Else
                                CreateLog("WinTexArasLog", "Dikkat siparis Aras kargoya yollanamadı : " + oSQL.SQLReadString("siparisno") + vbCrLf +
                                          "Hata Mesajı : " + cErrorMessage + vbCrLf)
                            End If
                        End If

                    Case "byexpress"
                        If Not lBYEXPRESS_Calisilmasin Then
                            If ByExpressGonderiYukle(oSQL.SQLReadString("siparisno"), cKargoTakipNo, cErrorMessage) Then
                                CreateLog("WinTexByexpressLog", "Siparis ByExpress kargoya yollandı : " + oSQL.SQLReadString("siparisno") + vbCrLf +
                                          "Kargo takip no : " + cKargoTakipNo + vbCrLf +
                                          "Hata mesajı : " + cErrorMessage + vbCrLf)
                            Else
                                CreateLog("WinTexByexpressLog", "Dikkat siparis ByExpress kargoya yollanamadı : " + oSQL.SQLReadString("siparisno") + vbCrLf +
                                          "Hata Mesajı : " + cErrorMessage + vbCrLf)
                            End If
                        End If
                    Case "ptt"
                        If Not lPTT_Calisilmasin Then
                            If PTTSendOrder1(oSQL.SQLReadString("siparisno"), cErrorMessage) Then
                                CreateLog("WinTexPTTLog", "Siparis PTT kargodaya yollandı : " + oSQL.SQLReadString("siparisno") + vbCrLf +
                                          "Hata Mesajı : " + cErrorMessage + vbCrLf)
                            Else
                                CreateLog("WinTexPTTLog", "Dikkat siparis PTT kargoya yollanamadı : " + oSQL.SQLReadString("siparisno") + vbCrLf +
                                          "Hata Mesajı : " + cErrorMessage + vbCrLf)
                            End If
                        End If
                    Case "mng"
                        If Not lMNG_Calisilmasin Then
                            If MNGSendOrder2(oSQL.SQLReadString("siparisno"), cErrorMessage) Then
                                CreateLog("WinTexMNGLog", "Siparis MNG kargoya yollandı : " + oSQL.SQLReadString("siparisno") + vbCrLf +
                                          "Hata mesajı : " + cErrorMessage + vbCrLf)
                            Else
                                CreateLog("WinTexMNGLog", "Dikkat siparis MNG kargoya yollanamadı : " + oSQL.SQLReadString("siparisno") + vbCrLf +
                                          "Hata Mesajı : " + cErrorMessage + vbCrLf)
                            End If
                        End If
                    Case "yurtici"
                        If Not lYURTICI_Calisilmasin Then
                            If YurticiSendOrder1(oSQL.SQLReadString("siparisno"), cErrorMessage) Then
                                CreateLog("WinTexYurticiLog", "Siparis Yurtici kargodaya yollandı : " + oSQL.SQLReadString("siparisno") + vbCrLf +
                                          "Hata Mesajı : " + cErrorMessage + vbCrLf)
                            Else
                                CreateLog("WinTexYurticiLog", "Dikkat siparis Yurtici kargoya yollanamadı : " + oSQL.SQLReadString("siparisno") + vbCrLf +
                                          "Hata Mesajı : " + cErrorMessage + vbCrLf)
                            End If
                        End If
                End Select
            Loop
            oSQL.oReader.Close()

            oSQL.CloseConn()

            YenidenYolla = True

        Catch ex As Exception
            ErrDisp("YenidenYolla",,,, ex)
        End Try
    End Function

#End Region

End Class


