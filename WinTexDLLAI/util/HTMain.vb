Imports System
Imports System.Net
Imports System.Net.Mail
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Collections.Generic
'Imports GroupDocs
Imports System.IO
Imports System.Text
Imports System.Web.Mail
Imports System.Security.Cryptography.X509Certificates

<ComClass(HTMain.ClassId, HTMain.InterfaceId, HTMain.EventsId)>
Public Class HTMain

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "d57a92ba-582f-46ef-8a01-c6ba50c94b0f"
    Public Const InterfaceId As String = "f36d8121-17ea-4cca-bd31-d4e9e47164c6"
    Public Const EventsId As String = "d61a1d0e-b912-4c28-9028-5d649915ee8a"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Shared cWinTexDLLVersion As String = My.Application.Info.Version.ToString.Trim

    Dim oCRS As eIrsaliye
    Dim oTurkKEP As TurkKEPeIrsaliye
    Dim oEDokSis As eDokSisIrsaliye
    Dim oPark As ParkIrsaliye

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub DosShellRun(cCommand As String)

        Try
            Dim nSonuc As Integer = 0

            If cCommand.Trim = "" Then Exit Sub
            nSonuc = Shell(cCommand, AppWinStyle.MinimizedNoFocus, True)

        Catch ex As Exception
            ErrDisp("DosShellRun : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

#Region "Ünsped / ABIT entegrasyonu"

    Public Sub Abit(Optional dBaslangicTarihi As Date = #1/1/1950#, Optional dBitisTarihi As Date = #1/1/1950#)
        Try
            AbitReadWrite(dBaslangicTarihi, dBitisTarihi)

        Catch ex As Exception
            ErrDisp("Abit : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

#End Region

#Region "YKK fermuar satınalma entegrasyonu"

    Public Sub YKKSysPar()
        Try
            Dim ofrmSysPar As New frmSysPar
            ofrmSysPar.ShowDialog()

        Catch ex As Exception
            ErrDisp("YKKSysPar : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

#End Region

#Region "Eliar boyama / yıkama otomasyonu entegrasyonu"

    Public Sub EliarSysPar()
        Try
            Dim ofrmEliar As New frmEliar
            ofrmEliar.ShowDialog()

        Catch ex As Exception
            ErrDisp("EliarSysPar : " + ex.Message, "HTMain")
        End Try
    End Sub

#End Region

#Region "Park eIrsaliye entegrasyonu"

    Public Sub ParkSysPar()
        Try
            Dim ofrmcrssoft As New frmCrsSoft
            ofrmcrssoft.init(4)

        Catch ex As Exception
            ErrDisp("ParkSysPar : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Sub Parkinit()
        Try
            oPark = New ParkIrsaliye
            oPark.init()

        Catch ex As Exception
            ErrDisp("Parkinit : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Function ParkSend(ByVal nCase As Integer, ByVal cFisNo As String, Optional ByVal lPDF As Boolean = False) As Boolean

        ParkSend = False

        Try
            ParkSend = oPark.SendEIrsaliye(nCase, cFisNo, lPDF)

        Catch ex As Exception
            ErrDisp("ParkSend : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function ParkStatus(ByVal cUID As String, ByRef cIrsaliyeNumarasi As String) As String
        ' Park ta sorgulamalar uid üstünden oluyor
        ' cIrsaliyeID burada UID oluyor
        ParkStatus = ""

        Try
            ParkStatus = oPark.GetStatus(cUID, cIrsaliyeNumarasi)
        Catch ex As Exception
            ErrDisp("ParkStatus : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function ParkPDF(ByVal cUID As String, ByVal Optional cFisNo As String = "", ByVal Optional cFisTipi As String = "") As String
        ' Park ta sorgulamalar uid üstünden oluyor
        ' cIrsaliyeID burada UID oluyor
        ParkPDF = ""

        Try
            ParkPDF = oPark.DespatchPDF(cUID, cFisNo, cFisTipi)
        Catch ex As Exception
            ErrDisp("ParkPDF : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Sub ParkDispose()
        Try
            oPark = Nothing
        Catch ex As Exception
            ErrDisp("ParkDispose : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

#End Region

#Region "eDoksis eIrsaliye entegrasyonu"

    Public Sub eDokSisInit()
        Try
            oEDokSis = New eDokSisIrsaliye

        Catch ex As Exception
            ErrDisp("eDokSisInit : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Sub eDokSisSysPar()
        Try
            Dim ofrmcrssoft As New frmCrsSoft
            ofrmcrssoft.init(3)

        Catch ex As Exception
            ErrDisp("eDokSisSysPar : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Function eDokSisSend(ByVal nCase As Integer, ByVal cFisNo As String, Optional ByVal lPDF As Boolean = False) As Boolean

        eDokSisSend = False

        Try
            eDokSisSend = oEDokSis.SendEIrsaliye(nCase, cFisNo, lPDF)

        Catch ex As Exception
            ErrDisp("eDokSisSend : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Sub eDokSisDispose()
        Try
            oEDokSis = Nothing

        Catch ex As Exception
            ErrDisp("eDokSisDispose : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Function eDokSisPDF(ByVal cIrsaliyeID As String, ByVal Optional cFisNo As String = "", ByVal Optional cFisTipi As String = "") As String

        eDokSisPDF = ""

        Try
            eDokSisPDF = oEDokSis.DespatchPDF(cIrsaliyeID, cFisNo, cFisTipi)
        Catch ex As Exception
            ErrDisp("eDokSisPDF : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

#End Region

#Region "TurkKEP eIrsaliye entegrasyonu"

    Public Function TurkKEPinit() As Boolean
        TurkKEPinit = False
        Try
            oTurkKEP = New TurkKEPeIrsaliye
            If oTurkKEP.cToken = "" Then Exit Function
            TurkKEPinit = True

        Catch ex As Exception
            ErrDisp("TurkKEPinit : " + ex.Message, "HTMain")
        End Try
    End Function

    Public Function TurkKEPKontor() As Long
        TurkKEPKontor = oTurkKEP.EArsivKalanKontorSorgula()
    End Function

    Public Sub TurkKEPDispose()
        Try
            oTurkKEP = Nothing

        Catch ex As Exception
            ErrDisp("TurkKEPDispose : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Sub TurkKEPSysPar()
        Try
            Dim ofrmcrssoft As New frmCrsSoft
            ofrmcrssoft.init(2)

        Catch ex As Exception
            ErrDisp("TurkKEPSysPar : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Function TurkKEPSend(ByVal nCase As Integer, ByVal cFisNo As String, Optional ByVal lPDF As Boolean = False) As Boolean

        TurkKEPSend = False

        Try
            TurkKEPSend = oTurkKEP.SendEIrsaliye(nCase, cFisNo, lPDF)
        Catch ex As Exception
            ErrDisp("TurkKEPSend : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function TurkKEPGetIrsaliyeNoUUID(ByVal cIrsaliyeID As String, ByRef cIrsaliyeNumarasi As String, ByRef cUUID As String) As Boolean

        TurkKEPGetIrsaliyeNoUUID = False

        Try
            TurkKEPGetIrsaliyeNoUUID = oTurkKEP.GetIrsaliyeNoUUID(cIrsaliyeID, cIrsaliyeNumarasi, cUUID)
        Catch ex As Exception
            ErrDisp("TurkKEPGetIrsaliyeNoUUID : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function TurkKEPPDF(ByVal cIrsaliyeID As String, ByVal Optional cFisNo As String = "", ByVal Optional cFisTipi As String = "") As String

        TurkKEPPDF = ""

        Try
            TurkKEPPDF = oTurkKEP.DespatchPDF(cIrsaliyeID, cFisNo, cFisTipi)
        Catch ex As Exception
            ErrDisp("TurkKEPPDF : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function TurkKEPGetStatus(ByVal cIrsaliyeID As String, ByRef cIrsaliyeNumarasi As String, ByRef cUUID As String) As String

        TurkKEPGetStatus = ""

        Try
            TurkKEPGetStatus = oTurkKEP.GetStatus(cIrsaliyeID, cIrsaliyeNumarasi, cUUID)
        Catch ex As Exception
            ErrDisp("TurkKEPGetStatus : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

#End Region

#Region "StyleShoots"

    Public Sub StyleShootsSysPar()
        Try
            Dim ofrmStyleShoots As New frmStyleShoots
            ofrmStyleShoots.init()

        Catch ex As Exception
            ErrDisp("StyleShootsSysPar : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Sub StyleShootsCopy()
        Try
            StyleShootsMoveFiles()

        Catch ex As Exception
            ErrDisp("StyleShootsCopy : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

#End Region

#Region "CRS eIrsaliye entegrasyonu"

    Public Sub CRSinit()
        Try
            oCRS = New eIrsaliye
            oCRS.init()

        Catch ex As Exception
            ErrDisp("CRSinit : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Sub CRSSysPar()
        Try
            Dim ofrmcrssoft As New frmCrsSoft
            ofrmcrssoft.init(1)

        Catch ex As Exception
            ErrDisp("CRSSysPar : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Function CRSCheckValidIrsaliye(ByVal nCase As Integer, ByVal cFisNo As String) As Boolean

        CRSCheckValidIrsaliye = False

        Try
            CRSCheckValidIrsaliye = CheckValidEIrsaliye(nCase, cFisNo)
        Catch ex As Exception
            ErrDisp("CRSCheckValidIrsaliye : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function CRSSend(ByVal nCase As Integer, ByVal cFisNo As String, Optional ByVal lPDF As Boolean = False) As Boolean

        CRSSend = False

        Try
            CRSSend = oCRS.SendEIrsaliye(nCase, cFisNo, lPDF)
        Catch ex As Exception
            ErrDisp("CRSSend : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function CRSStatus(ByVal cIrsaliyeID As String) As String

        CRSStatus = ""

        Try
            CRSStatus = oCRS.DespatchStatus(cIrsaliyeID)
        Catch ex As Exception
            ErrDisp("CRSStatus : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function CRSPDF(ByVal cIrsaliyeID As String, ByVal Optional cFisNo As String = "", ByVal Optional cFisTipi As String = "") As String

        CRSPDF = ""

        Try
            CRSPDF = oCRS.DespatchPDF(cIrsaliyeID, cFisNo, cFisTipi)
        Catch ex As Exception
            ErrDisp("CRSPDF : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Sub CRSDispose()
        Try
            oCRS = Nothing
        Catch ex As Exception
            ErrDisp("CRSDispose : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

#End Region

#Region "WinTexDLL Ana Prosedürler"

    Public Function WinTexDllVersion() As String
        Try
            WinTexDllVersion = cWinTexDLLVersion

        Catch ex As Exception
            ErrDisp("WinTexDLLVersion : " + ex.Message, "HTMain",,, ex)
            WinTexDllVersion = ""
        End Try
    End Function

    Public Function TestDLL() As Boolean
        TestDLL = False
        Try
            Dim oTestWinTexDLL As New TestWinTexDLL
            oTestWinTexDLL.init(Me)
        Catch ex As Exception
            ErrDisp("TestDLL : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function TestWinTexDLL() As String

        TestWinTexDLL = "WinTexDLL ÇALIŞMIYOR"

        Try
            Return "WinTexDLL Çalışıyor"
        Catch ex As Exception
            ErrDisp("TestWinTexDLL : " + ex.Message, "HTMain")
        End Try
    End Function

    Public Function init(Optional cServer As String = "", Optional cDatabase As String = "", Optional cUser As String = "sa", Optional cPassword As String = "", Optional cConnStr As String = "",
                         Optional cWintexUser As String = "", Optional cAbitUser As String = "", Optional cAbitPassword As String = "") As Boolean
        ' init database connection
        Dim cSQL As String = ""
        Dim oSQL As SQLServerClass

        init = False

        Try
            oConnection.cServer = cServer.Trim
            oConnection.cDatabase = cDatabase.Trim
            oConnection.cUser = cUser.Trim
            oConnection.cPassword = cPassword.Trim

            oConnection.cWinTexUser = cWintexUser.Trim
            oConnection.cAbitUser = cAbitUser.Trim
            oConnection.cAbitPassword = cAbitPassword.Trim

            If cConnStr.Trim = "" Then
                oConnection.cConnStr = "Data Source=" + oConnection.cServer + ";" +
                                        "Initial Catalog=" + oConnection.cDatabase + ";" +
                                        "uid=" + oConnection.cUser + ";" +
                                        "pwd=" + oConnection.cPassword + ""
            Else
                oConnection.cConnStr = cConnStr.Trim
            End If

            If TestConnection() Then
                init = True
                If GetSysPar("WinTexDllLog") = "1" Then
                    oConnection.lWinTexDllLog = True
                Else
                    oConnection.lWinTexDllLog = False
                End If

                If GetSysPar("WinTexDllError") = "1" Then
                    oConnection.lWinTexDllError = True
                Else
                    oConnection.lWinTexDllError = False
                End If

                oSQL = New SQLServerClass()
                oSQL.OpenConn()

                If cWintexUser.Trim = "" Then
                    oConnection.cPersonel = ""
                    oConnection.nPersonelUyumID = 0
                    oConnection.cUyumUsername = ""
                    oConnection.cUyumPassword = ""
                Else
                    oSQL.cSQLQuery = "select top 1 personel, uyumid, uyumusername, uyumpassword  " +
                                    " from personel with (NOLOCK) " +
                                    " where username = '" + cWintexUser.Trim + "' "

                    oSQL.GetSQLReader()

                    If oSQL.oReader.Read Then
                        oConnection.cPersonel = oSQL.SQLReadString("personel")
                        oConnection.nPersonelUyumID = oSQL.SQLReadDouble("uyumid")
                        oConnection.cUyumUsername = oSQL.SQLReadString("uyumusername")
                        oConnection.cUyumPassword = oSQL.SQLReadString("uyumpassword")
                    End If
                    oSQL.oReader.Close()
                End If

                oSQL.cSQLQuery = "select top 1 firmname " +
                                " from sysinfo with (NOLOCK) " +
                                " where firmname is not null " +
                                " and firmname <> '' "

                oSQL.GetSQLReader()

                If oSQL.oReader.Read Then
                    oConnection.cOwner = oSQL.SQLReadString("firmname").ToLower
                End If
                oSQL.oReader.Close()

                oSQL.CloseConn()
                oSQL = Nothing

            End If

            'InitGroupDocs()
            STILoadTrLocalization()

        Catch ex As Exception
            init = False
            ErrDisp("SQL Connect : " + ex.Message + " " + cServer.Trim + " " + cDatabase.Trim + " " + cUser.Trim + " " + cPassword.Trim)
        End Try
    End Function

    Public Sub ShowVersionInfo()
        Try
            Dim oForm As New AboutBoxWinTex
            oForm.ShowDialog()
        Catch ex As Exception
            ErrDisp("ShowVersionInfo : " + ex.Message, "HTMain")
        End Try
    End Sub

#End Region

#Region "Döviz kurları , Merkez Bankası"

    Public Sub GunlukDovizKuruGirisi()

        Try
            Dim oForm As New frmDoviz
            oForm.init()

        Catch ex As Exception
            ErrDisp("GunlukDovizKuruGirisi : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Function GetKurlarFromMBXML() As Boolean
        ' eksik kurları toplu olarak çek
        GetKurlarFromMBXML = False

        Try
            ' yeni rutin 16.10.2021
            GetKurlarFromMBXML = MBKurlar()
        Catch ex As Exception
            ErrDisp("GetKurlarFromMBXML : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function GetKurFromMBXML2(cDoviz As String,
                                     Optional dTarih As Date = #1/1/1950#,
                                     Optional ByRef nAlis As Double = 0,
                                     Optional ByRef nSatis As Double = 0,
                                     Optional ByRef nEfektifAlis As Double = 0,
                                     Optional ByRef nEfektifSatis As Double = 0) As Boolean

        ' tek bir günün kurlarını oku
        GetKurFromMBXML2 = False

        Try
            ' eski rutin hatalı
            'GetKurFromMBXML2 = MBKurOku(cDoviz, dTarih, nAlis, nSatis, nEfektifAlis, nEfektifSatis)

            ' yeni rutin 16.10.2021
            GetKurFromMBXML2 = MBKurOku2(cDoviz, dTarih, nAlis, nSatis, nEfektifAlis, nEfektifSatis)
        Catch ex As Exception
            ErrDisp("GetKurFromMBXML2 : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function GetKurFromMBXML(Optional dTarih As Date = #1/1/1950#) As Boolean

        GetKurFromMBXML = False

        Try
            GetKurFromMBXML = MBKur(dTarih)
        Catch ex As Exception
            ErrDisp("GetKurFromMBXML : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function GetKurFromLogo(Optional cFilter As String = "") As Boolean

        GetKurFromLogo = False

        Try
            GetKurFromLogo = LogoKur(cFilter)
        Catch ex As Exception
            ErrDisp("GetKurFromLogo : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function GetKurFromUyum(Optional cFilter As String = "") As Boolean

        GetKurFromUyum = False

        Try
            GetKurFromUyum = UyumKur(cFilter)
        Catch ex As Exception
            ErrDisp("GetKurFromUyum : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

#End Region

#Region "Stimulsoft reporting"

    Public Sub OlcuTablosuYazdir(cFisNo As String)
        Try
            OlcuTablosuYazdir1(cFisNo)

        Catch ex As Exception
            ErrDisp("OlcuTablosuYazdir : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Sub StiBulDegistir(nMode As Integer)
        ' nMode = 1 , Stimulus
        ' nMode = 2 , DevxReports
        ' nMode = 3 , DevxDashboards
        ' nMode = 4 , Stimulus Logo gizle
        Try

            Dim oForm As New frmBulDegistir
            oForm.init(nMode)

        Catch ex As Exception
            ErrDisp("StiBulDegistir : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Sub ReportDesigner(Optional cReportID As String = "", Optional cReportClass As String = "", Optional cReportVariable1 As String = "", Optional cReportSQL As String = "",
                              Optional cReportVariable2 As String = "", Optional cReportVariable3 As String = "", Optional cReportVariable4 As String = "", Optional cReportVariable5 As String = "",
                              Optional cReportVariable6 As String = "", Optional cReportVariable7 As String = "", Optional cReportVariable8 As String = "", Optional cReportVariable9 As String = "",
                              Optional cReportVariable10 As String = "", Optional nScreenType As Integer = 1)
        Try
            If nScreenType = 1 Then

                Dim oStiDsgnr As New StiReportDesign

                If cReportID.Trim = "" Then
                    oStiDsgnr.initNewReport(cReportID, cReportClass, cReportVariable1, cReportSQL,
                                        cReportVariable2, cReportVariable3, cReportVariable4, cReportVariable5,
                                        cReportVariable6, cReportVariable7, cReportVariable8, cReportVariable9,
                                        cReportVariable10)
                Else
                    oStiDsgnr.init(cReportID, cReportClass, cReportVariable1, cReportSQL,
                                        cReportVariable2, cReportVariable3, cReportVariable4, cReportVariable5,
                                        cReportVariable6, cReportVariable7, cReportVariable8, cReportVariable9,
                                        cReportVariable10)
                End If
            Else
                Dim oForm As New ReportDesigner

                If cReportID.Trim = "" Then
                    oForm.initNewReport(cReportID, cReportClass, cReportVariable1, cReportSQL,
                                        cReportVariable2, cReportVariable3, cReportVariable4, cReportVariable5,
                                        cReportVariable6, cReportVariable7, cReportVariable8, cReportVariable9,
                                        cReportVariable10)
                Else
                    oForm.init(cReportID, cReportClass, cReportVariable1, cReportSQL,
                                        cReportVariable2, cReportVariable3, cReportVariable4, cReportVariable5,
                                        cReportVariable6, cReportVariable7, cReportVariable8, cReportVariable9,
                                        cReportVariable10)
                End If
            End If

        Catch ex As Exception
            ErrDisp("ReportDesigner : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Sub ReportToViewer(Optional cReportID As String = "", Optional cReportVariable1 As String = "", Optional cReportVariable2 As String = "", Optional cReportVariable3 As String = "",
                              Optional cReportVariable4 As String = "", Optional cReportVariable5 As String = "", Optional cReportVariable6 As String = "", Optional cReportVariable7 As String = "",
                              Optional cReportVariable8 As String = "", Optional cReportVariable9 As String = "", Optional cReportVariable10 As String = "", Optional nScreenType As Integer = 1)
        Try
            If cReportID.Trim = "" Then
                Exit Sub
            End If

            Select Case nScreenType
                Case 1
                    Dim oForm As New StiReportView

                    oForm.initViewer(cReportID, cReportVariable1, cReportVariable2, cReportVariable3,
                                   cReportVariable4, cReportVariable5, cReportVariable6, cReportVariable7,
                                   cReportVariable8, cReportVariable9, cReportVariable10)
                Case 2
                    Dim oForm As New ReportViewer

                    oForm.initViewer(cReportID, cReportVariable1, cReportVariable2, cReportVariable3,
                                 cReportVariable4, cReportVariable5, cReportVariable6, cReportVariable7,
                                 cReportVariable8, cReportVariable9, cReportVariable10)
                Case 3
                    Dim oForm As New frmSTIDashboardViewer

                    oForm.initViewer(cReportID, cReportVariable1, cReportVariable2, cReportVariable3,
                                 cReportVariable4, cReportVariable5, cReportVariable6, cReportVariable7,
                                 cReportVariable8, cReportVariable9, cReportVariable10)
            End Select

        Catch ex As Exception
            ErrDisp("ReportToViewer : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Sub ReportToPrinter(Optional cReportID As String = "", Optional cReportVariable1 As String = "", Optional cPrinterName As String = "", Optional nCopies As Double = 1,
                               Optional cReportVariable2 As String = "", Optional cReportVariable3 As String = "", Optional cReportVariable4 As String = "", Optional cReportVariable5 As String = "",
                               Optional cReportVariable6 As String = "", Optional cReportVariable7 As String = "", Optional cReportVariable8 As String = "", Optional cReportVariable9 As String = "",
                               Optional cReportVariable10 As String = "")
        Try
            If cReportID.Trim = "" Then
                Exit Sub
            End If

            oReport.cReportID = cReportID
            oReport.cReportVariable1 = cReportVariable1.Trim
            oReport.cReportVariable2 = cReportVariable2.Trim
            oReport.cReportVariable3 = cReportVariable3.Trim
            oReport.cReportVariable4 = cReportVariable4.Trim
            oReport.cReportVariable5 = cReportVariable5.Trim
            oReport.cReportVariable6 = cReportVariable6.Trim
            oReport.cReportVariable7 = cReportVariable7.Trim
            oReport.cReportVariable8 = cReportVariable8.Trim
            oReport.cReportVariable9 = cReportVariable9.Trim
            oReport.cReportVariable10 = cReportVariable10.Trim
            oReport.cPrinterName = cPrinterName
            oReport.nCopies = nCopies
            STDirectPrint()

        Catch ex As Exception
            ErrDisp("ReportToPrinter : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Sub ReportToPDF(Optional cReportID As String = "", Optional cReportVariable1 As String = "", Optional cFileName As String = "c:\StimulusReport.PDF", Optional cReportVariable2 As String = "",
                            Optional cReportVariable3 As String = "", Optional cReportVariable4 As String = "", Optional cReportVariable5 As String = "", Optional cReportVariable6 As String = "",
                            Optional cReportVariable7 As String = "", Optional cReportVariable8 As String = "", Optional cReportVariable9 As String = "", Optional cReportVariable10 As String = "",
                            Optional nQuality As Integer = 1)
        Try
            If cReportID.Trim = "" Then
                Exit Sub
            End If

            oReport.cReportID = cReportID
            oReport.cReportVariable1 = cReportVariable1.Trim
            oReport.cReportVariable2 = cReportVariable2.Trim
            oReport.cReportVariable3 = cReportVariable3.Trim
            oReport.cReportVariable4 = cReportVariable4.Trim
            oReport.cReportVariable5 = cReportVariable5.Trim
            oReport.cReportVariable6 = cReportVariable6.Trim
            oReport.cReportVariable7 = cReportVariable7.Trim
            oReport.cReportVariable8 = cReportVariable8.Trim
            oReport.cReportVariable9 = cReportVariable9.Trim
            oReport.cReportVariable10 = cReportVariable10.Trim
            STSaveToPDF(cFileName, nQuality)

        Catch ex As Exception
            ErrDisp("ReportToPDF : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Sub ReportToFile(Optional cReportID As String = "", Optional cReportVariable1 As String = "", Optional cFileName As String = "c:\StimulusReport.PDF", Optional cReportVariable2 As String = "",
                            Optional cReportVariable3 As String = "", Optional cReportVariable4 As String = "", Optional cReportVariable5 As String = "", Optional cReportVariable6 As String = "",
                            Optional cReportVariable7 As String = "", Optional cReportVariable8 As String = "", Optional cReportVariable9 As String = "", Optional cReportVariable10 As String = "",
                            Optional cFileType As String = "PDF")
        Try
            If cReportID.Trim = "" Then
                Exit Sub
            End If

            oReport.cReportID = cReportID
            oReport.cReportVariable1 = cReportVariable1.Trim
            oReport.cReportVariable2 = cReportVariable2.Trim
            oReport.cReportVariable3 = cReportVariable3.Trim
            oReport.cReportVariable4 = cReportVariable4.Trim
            oReport.cReportVariable5 = cReportVariable5.Trim
            oReport.cReportVariable6 = cReportVariable6.Trim
            oReport.cReportVariable7 = cReportVariable7.Trim
            oReport.cReportVariable8 = cReportVariable8.Trim
            oReport.cReportVariable9 = cReportVariable9.Trim
            oReport.cReportVariable10 = cReportVariable10.Trim
            STSaveToFile(cFileName, cFileType)

        Catch ex As Exception
            ErrDisp("ReportToFile : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Function StiReportsBackup(Optional cBackupDir As String = "", Optional lOUS2 As Boolean = False) As Boolean

        StiReportsBackup = False

        Try
            StiReportsBackup = StiBackup(cBackupDir, lOUS2)
        Catch ex As Exception
            ErrDisp("STIReportsBackup : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Sub ReportToMrt(Optional cReportID As String = "")
        Try
            If cReportID.Trim = "" Then
                Exit Sub
            End If
            STExportSourceToFile(cReportID)

        Catch ex As Exception
            ErrDisp("ReportToMrt : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Sub MrtToReport()
        Try
            STImportFileToReport()
        Catch ex As Exception
            ErrDisp("MrtToReport : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

#End Region

#Region "Resimler"

    Public Function SuperPicture(cFileName1 As String, Optional cFileName2 As String = "", Optional nScaleWidth As Integer = 640, Optional nScaleHeight As Integer = 480) As IntPtr
        Try
            Dim oForm As New AdvancedPicture

            oForm.init(cFileName1, cFileName2, nScaleWidth, nScaleHeight)
            SuperPicture = oForm.Handle

        Catch ex As Exception
            ErrDisp("SuperPicture : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Sub StitchPictures(cFileName1 As String, cFileName2 As String, cFileName3 As String, Optional nScaleWidth As Integer = 640, Optional nScaleHeight As Integer = 480)

        Dim oImage1 As Image = Nothing
        Dim oImage2 As Image = Nothing
        Dim oImage As Image = Nothing

        Try
            If My.Computer.FileSystem.FileExists(cFileName1.Trim) Then
                oImage1 = New Bitmap(Image.FromFile(cFileName1), New Size(nScaleWidth, nScaleHeight))
            Else
                oImage1 = Nothing
            End If

            If My.Computer.FileSystem.FileExists(cFileName2.Trim) Then
                oImage2 = New Bitmap(Image.FromFile(cFileName2), New Size(nScaleWidth, nScaleHeight))
            Else
                oImage2 = Nothing
            End If

            If Not IsNothing(oImage1) And Not IsNothing(oImage2) Then
                oImage = MergeImages(oImage1, oImage2)
            ElseIf Not IsNothing(oImage1) And IsNothing(oImage2) Then
                oImage = oImage1
            ElseIf IsNothing(oImage1) And Not IsNothing(oImage2) Then
                oImage = oImage2
            End If

            DestroyFile(cFileName3)
            If Not IsNothing(oImage) Then
                oImage.Save(cFileName3)
            End If

        Catch ex As Exception
            ErrDisp("StitchPictures : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Sub TakePicture(Optional ByRef nCameraNo As Integer = 0, Optional ByVal cFileName As String = "c:\WinTexResim")

        Try
            oCamera.cFileName = cFileName.Trim

            Dispositivos = New DirectX.Capture.Filters()

            DestroyFile(cFileName + ".avi")
            DestroyFile(cFileName + ".jpg")

            If nCameraNo = -1 Then
                ' add camera
                Dim AddCamera As New frmAddCamera()
                AddCamera.init()
                If oCamera.nCameraNo = -1 Then
                    Exit Sub
                End If
                ' configure camera
                CaptureInformation.ConfWindow = New frmCameraConfiguration()
                CaptureInformation.ConfWindow.init()
                AssignWinTexCameraKeys()
            Else
                RetrieveWinTexCameraKeys()
            End If

            oCamera.cFileName = cFileName.Trim
            ConfParamCam()

            Dim oCAM As New frmCaptureVideo
            oCAM.init()

            CaptureInformation.CaptureInfo.DisposeCapture()
            CaptureInformation.CaptureInfo = Nothing
            CaptureInformation.Camera = Nothing
            CaptureInformation.ConfWindow = Nothing
            oCAM = Nothing
            Dispositivos = Nothing

        Catch ex As Exception
            ErrDisp("TakePicture : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    'Public Sub ImageLoadAsThumb(ByRef oIPictureDisp As stdole.IPictureDisp, ByVal cFileName As String, Optional ByVal nWidth As Integer = 90, Optional ByVal nHeight As Integer = 60)
    '    Dim oBitMap As Image
    '    Try
    '        oBitMap = New Bitmap(Image.FromFile(cFileName), New Size(nWidth, nHeight))

    '    Catch ex As Exception
    '        ErrDisp("ImageLoadAsThumb : " + ex.Message)
    '    End Try
    'End Sub

    Public Sub ImageResizeAndSave(ByVal cSourceFileName As String, ByVal cDestinationFileName As String, Optional ByVal nWidth As Integer = 90, Optional ByVal nHeight As Integer = 60)

        Dim oBitMap As Image

        Try
            oBitMap = New Bitmap(Image.FromFile(cSourceFileName), New Size(nWidth, nHeight))
            oBitMap.Save(cDestinationFileName)

        Catch ex As Exception
            ErrDisp("ImageResizeAndSave : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Sub ImageResize(ByRef oBitMap As Image, Optional ByVal nWidth As Integer = 90, Optional ByVal nHeight As Integer = 60, Optional ByVal cFileName As String = "")
        Try
            oBitMap = New Bitmap(oBitMap, New Size(nWidth, nHeight))
            If cFileName.Trim <> "" Then
                oBitMap.Save(cFileName)
            End If

        Catch ex As Exception
            ErrDisp("ImageResize : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

#End Region

#Region "Google GMail"
    ' gmail
    ' server   : smtp.gmail.com
    ' username : wintexokansokmen@gmail.com
    ' password : Okansokmenwintex
    ' username : wintexprogram@gmail.com
    ' password : Wintexprogram2016
    ' port     : 587
    ' TLS      : true

    ' yandex
    ' server   : smtp.yandex.com.tr	
    ' mail     : wintexprogram@yandex.com.tr
    ' username : wintexprogram
    ' password : Wintex2022.
    ' port     : 465
    ' TLS      : true

    ' hotmail
    ' server   : smtp.office365.com
    ' username : wintex@wintexprogram.com 
    ' password : Wintex2024
    ' username : wintexprogram@hotmail.com
    ' password : Wintex2022.
    ' port     : 587
    ' SSL      : true 

    Public Function SendGMail(Optional ByVal cRecipients As String = "okansokmen@gmail.com",
                              Optional ByVal cSubject As String = "WinTex uyari mesajidir",
                              Optional ByVal cBody As String = "Lutfen mesajin eklerini kontrol ediniz",
                              Optional ByVal cAttachments As String = "",
                              Optional ByVal cCarbonCopys As String = "",
                              Optional ByVal cFromAddress As String = "",
                              Optional ByVal cUserName As String = "",
                              Optional ByVal cPassword As String = "",
                              Optional ByVal cServer As String = "",
                              Optional ByVal nPort As Integer = 587,
                              Optional ByVal lHTML As Boolean = False,
                              Optional ByVal lEnableSsl As Boolean = True,
                              Optional ByVal lTLS As Boolean = False,
                              Optional ByVal lServerCertificateValidationCallback As Boolean = False) As Boolean
        ' cRecipients is comma delimited strings
        ' cAttachments is comma delimited strings 
        Dim aRecipients() As String
        Dim aCarbonCopys() As String
        Dim aAttachments() As String
        Dim nCnt As Integer = 0
        Dim cErrMsg As String = ""
        Dim oEmail As System.Net.Mail.MailMessage = Nothing
        Dim SMTPServer As SmtpClient = Nothing

        SendGMail = False

        Try
            cErrMsg = IIf(cRecipients.Trim = "", "", "To : " + cRecipients.Trim).ToString +
                      IIf(cCarbonCopys.Trim = "", "", " CC : " + cCarbonCopys.Trim).ToString +
                      IIf(cAttachments.Trim = "", "", " Attachments : " + cAttachments.Trim).ToString +
                      " From : " + cFromAddress.Trim +
                      " Server : " + cServer.Trim +
                      " Username : " + cUserName.Trim +
                      " Password : " + cPassword.Trim +
                      " Port : " + nPort.ToString

            If cRecipients.Trim = "" Then
                Exit Function
            Else
                ' create and populate eMail
                oEmail = New System.Net.Mail.MailMessage
                oEmail.From = New System.Net.Mail.MailAddress(cFromAddress)
                oEmail.Subject = cSubject.Trim
                oEmail.IsBodyHtml = lHTML
                oEmail.Body = cBody.Trim

                If InStr(cRecipients, ",") = 0 Then
                    oEmail.To.Add(cRecipients.Trim)
                Else
                    aRecipients = Split(cRecipients, ",")
                    For nCnt = 0 To aRecipients.GetUpperBound(0)
                        If aRecipients(nCnt).Trim <> "" Then
                            oEmail.To.Add(aRecipients(nCnt).Trim)
                        End If
                    Next
                End If

                If cCarbonCopys.Trim <> "" Then
                    If InStr(cCarbonCopys, ",") = 0 Then
                        oEmail.CC.Add(cCarbonCopys.Trim)
                    Else
                        aCarbonCopys = Split(cCarbonCopys, ",")
                        For nCnt = 0 To aCarbonCopys.GetUpperBound(0)
                            If aCarbonCopys(nCnt).Trim <> "" Then
                                oEmail.CC.Add(aCarbonCopys(nCnt).Trim)
                            End If
                        Next
                    End If
                End If

                If cAttachments.Trim <> "" Then
                    If InStr(cAttachments, ",") = 0 Then
                        oEmail.Attachments.Add(New System.Net.Mail.Attachment(cAttachments.Trim))
                    Else
                        aAttachments = Split(cAttachments, ",")
                        For nCnt = 0 To aAttachments.GetUpperBound(0)
                            If aAttachments(nCnt).Trim <> "" Then
                                oEmail.Attachments.Add(New System.Net.Mail.Attachment(aAttachments(nCnt).Trim))
                            End If
                        Next
                    End If
                End If

                If lTLS Then
                    ' STARTTLS
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                End If

                If lServerCertificateValidationCallback Then
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = Function(se As Object,
                                                                                      cert As System.Security.Cryptography.X509Certificates.X509Certificate,
                                                                                      chain As System.Security.Cryptography.X509Certificates.X509Chain,
                                                                                      sslerror As System.Net.Security.SslPolicyErrors) True
                End If

                SMTPServer = New System.Net.Mail.SmtpClient(cServer.Trim, nPort)
                SMTPServer.Host = cServer.Trim
                SMTPServer.Port = nPort
                SMTPServer.EnableSsl = lEnableSsl
                SMTPServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network
                SMTPServer.UseDefaultCredentials = False
                SMTPServer.Credentials = New System.Net.NetworkCredential(cUserName.Trim, cPassword.Trim)
                SMTPServer.Send(oEmail)
                SMTPServer.Dispose()

                oEmail.Dispose()
                SendGMail = True
            End If

        Catch ex As SmtpException
            oEmail.Dispose()
            MsgBox("Sending Email Failed. Smtp Error." + ex.Message + " " + cErrMsg)
        Catch ex As ArgumentOutOfRangeException
            oEmail.Dispose()
            MsgBox("Sending Email Failed. Check Port Number." + ex.Message + " " + cErrMsg)
        Catch Ex As InvalidOperationException
            oEmail.Dispose()
            MsgBox("Sending Email Failed. Check Port Number." + Ex.Message + " " + cErrMsg)
        End Try
    End Function

#End Region

#Region "Luxand face recognition"

    Public Function FaceRecognition(Optional ByVal nMode As Integer = 1,
                                    Optional ByRef cPersonel As String = "",
                                    Optional ByRef cUserName As String = "",
                                    Optional ByRef cActivePass As String = "",
                                    Optional ByRef cPersonelFaceTemplate As String = "",
                                    Optional ByRef cPersonelResim As String = "",
                                    Optional ByRef nSimilarity As Double = 0) As Boolean
        ' nMode = 1 , personel kayıt
        ' nMode = 2 , yüz tanıma
        FaceRecognition = False

        Try
            Dim ofrmLuxand As New frmLuxand

            ofrmLuxand.init(nMode)

            cPersonel = Gl_Personel
            cUserName = Gl_UserName
            cActivePass = Gl_ActivePass
            cPersonelFaceTemplate = GL_PersonelFaceTemplate
            cPersonelResim = GL_PersonelResim
            nSimilarity = GL_Similarity

            FaceRecognition = True
        Catch ex As Exception
            ErrDisp("FaceRecognition : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

#End Region

#Region "DeveloperExpress / DevX"

    Public Function OLAPDevX(Optional cReportClass As String = "TicSipOlapGrid", Optional cReportName As String = "SiparisNo", Optional cConnectionString As String = "") As String

        OLAPDevX = ""

        Try
            Dim oForm As New DevXReports

            OLAPDevX = oForm.initOLAP(cReportClass, cReportName, cConnectionString)

        Catch ex As Exception
            ErrDisp("OLAPDevX : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function DDBReportsBackup(Optional cBackupDir As String = "") As Boolean

        DDBReportsBackup = False

        Try
            DDBReportsBackup = DDBBackup(cBackupDir)
        Catch ex As Exception
            ErrDisp("DDBReportsBackup : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function ReportToDevX(cReportClass As String, Optional cSQL As String = "", Optional ByVal nMode As Integer = 1, Optional cView As String = "",
                                 Optional cServer1 As String = "", Optional cDatabase1 As String = "", Optional cUsername1 As String = "",
                                 Optional cPassword1 As String = "") As IntPtr
        Try
            Dim oForm As New DevXReports

            oForm.init(cReportClass, cSQL, nMode, cView, cServer1, cDatabase1, cUsername1, cPassword1)
            ReportToDevX = oForm.Handle

        Catch ex As Exception
            ErrDisp("ReportToDevX : " + ex.Message, "HTMain", cSQL,, ex)
        End Try
    End Function

    Public Function DevXPDFViewer(Optional cPDFFileName As String = "") As IntPtr
        Try
            Dim oForm As New frmPDFViewer

            oForm.init(cPDFFileName)
            DevXPDFViewer = oForm.Handle

        Catch ex As Exception
            ErrDisp("DevXPDFViewer : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Sub DevXReportDesign(Optional cReportID As String = "", Optional cReportClass As String = "", Optional cReportSQL As String = "")

        Dim oForm As New DevXReportDesigner

        Try
            If cReportID.Trim = "" Then
                oReportDevX.cReportID = ""
                oReportDevX.cReportName = ""
                oReportDevX.cReportClass = cReportClass.Trim
                oReportDevX.cReportSQL = cReportSQL.Trim
                oReportDevX.cReport = ""
                oForm.initNewReport()
            Else
                oReportDevX.cReportID = cReportID.Trim
                oReportDevX.cReportClass = cReportClass.Trim
                oReportDevX.cReportSQL = cReportSQL.Trim
                oForm.init()
            End If

        Catch ex As Exception
            ErrDisp("DevXReportDesign : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Function DevXReportView(cReportID As String, cReportSQL As String) As IntPtr

        Dim oForm As New DevXReportViewer

        Try
            oReportDevX.cReportID = cReportID
            oReportDevX.cReportSQL = cReportSQL
            oForm.init()
            DevXReportView = oForm.Handle

        Catch ex As Exception
            ErrDisp("DevXReportView : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function DevXDashBoardDesign(Optional cReportID As String = "", Optional cReportClass As String = "",
                                   Optional cSQL1 As String = "", Optional cHeader1 As String = "Sorgu1",
                                   Optional cSQL2 As String = "", Optional cHeader2 As String = "Sorgu2",
                                   Optional cSQL3 As String = "", Optional cHeader3 As String = "Sorgu3",
                                   Optional cSQL4 As String = "", Optional cHeader4 As String = "Sorgu4",
                                   Optional cSQL5 As String = "", Optional cHeader5 As String = "Sorgu5",
                                   Optional cSQL6 As String = "", Optional cHeader6 As String = "Sorgu6",
                                   Optional cSQL7 As String = "", Optional cHeader7 As String = "Sorgu7",
                                   Optional cSQL8 As String = "", Optional cHeader8 As String = "Sorgu8",
                                   Optional cSQL9 As String = "", Optional cHeader9 As String = "Sorgu9",
                                   Optional cSQL10 As String = "", Optional cHeader10 As String = "Sorgu10") As IntPtr
        ' cReportVariable komple yeni SQL query oldu
        Try
            oReportDevX.cReportName = ""
            oReportDevX.cReport = ""

            oReportDevX.cReportSQL1 = cSQL1.Trim : oReportDevX.cReportQueryName1 = cHeader1.Trim
            oReportDevX.cReportSQL2 = cSQL2.Trim : oReportDevX.cReportQueryName2 = cHeader2.Trim
            oReportDevX.cReportSQL3 = cSQL3.Trim : oReportDevX.cReportQueryName3 = cHeader3.Trim
            oReportDevX.cReportSQL4 = cSQL4.Trim : oReportDevX.cReportQueryName4 = cHeader4.Trim
            oReportDevX.cReportSQL5 = cSQL5.Trim : oReportDevX.cReportQueryName5 = cHeader5.Trim
            oReportDevX.cReportSQL6 = cSQL6.Trim : oReportDevX.cReportQueryName6 = cHeader6.Trim
            oReportDevX.cReportSQL7 = cSQL7.Trim : oReportDevX.cReportQueryName7 = cHeader7.Trim
            oReportDevX.cReportSQL8 = cSQL8.Trim : oReportDevX.cReportQueryName8 = cHeader8.Trim
            oReportDevX.cReportSQL9 = cSQL9.Trim : oReportDevX.cReportQueryName9 = cHeader9.Trim
            oReportDevX.cReportSQL10 = cSQL10.Trim : oReportDevX.cReportQueryName10 = cHeader10.Trim

            If cReportID.Trim = "" Then
                oReportDevX.cReportID = GetFisNo("reportid")
                oReportDevX.cReportClass = cReportClass.Trim

                Dim oForm As New frmDashboardDesigner
                oForm.init()
                DevXDashBoardDesign = oForm.Handle
            Else
                oReportDevX.cReportID = cReportID.Trim
                oReportDevX.cReportClass = cReportClass.Trim

                If DevXLoadReport(3) Then
                    Dim oForm As New frmDashboardDesigner
                    oForm.init()
                    DevXDashBoardDesign = oForm.Handle
                Else
                    MsgBox("Dashboard yüklenemedi")
                End If
            End If

        Catch ex As Exception
            ErrDisp("DevXDashBoardDesign : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function DevXDashBoardView(cReportID As String,
                                   Optional cSQL1 As String = "", Optional cHeader1 As String = "Sorgu1",
                                   Optional cSQL2 As String = "", Optional cHeader2 As String = "Sorgu2",
                                   Optional cSQL3 As String = "", Optional cHeader3 As String = "Sorgu3",
                                   Optional cSQL4 As String = "", Optional cHeader4 As String = "Sorgu4",
                                   Optional cSQL5 As String = "", Optional cHeader5 As String = "Sorgu5",
                                   Optional cSQL6 As String = "", Optional cHeader6 As String = "Sorgu6",
                                   Optional cSQL7 As String = "", Optional cHeader7 As String = "Sorgu7",
                                   Optional cSQL8 As String = "", Optional cHeader8 As String = "Sorgu8",
                                   Optional cSQL9 As String = "", Optional cHeader9 As String = "Sorgu9",
                                   Optional cSQL10 As String = "", Optional cHeader10 As String = "Sorgu10",
                                   Optional cHandle As String = "") As IntPtr
        Try
            Dim nHandle As Long = 0

            oReportDevX.cReportID = cReportID

            If DevXLoadReport(3) Then
                oReportDevX.cReportSQL1 = cSQL1.Trim : oReportDevX.cReportQueryName1 = cHeader1.Trim
                oReportDevX.cReportSQL2 = cSQL2.Trim : oReportDevX.cReportQueryName2 = cHeader2.Trim
                oReportDevX.cReportSQL3 = cSQL3.Trim : oReportDevX.cReportQueryName3 = cHeader3.Trim
                oReportDevX.cReportSQL4 = cSQL4.Trim : oReportDevX.cReportQueryName4 = cHeader4.Trim
                oReportDevX.cReportSQL5 = cSQL5.Trim : oReportDevX.cReportQueryName5 = cHeader5.Trim
                oReportDevX.cReportSQL6 = cSQL6.Trim : oReportDevX.cReportQueryName6 = cHeader6.Trim
                oReportDevX.cReportSQL7 = cSQL7.Trim : oReportDevX.cReportQueryName7 = cHeader7.Trim
                oReportDevX.cReportSQL8 = cSQL8.Trim : oReportDevX.cReportQueryName8 = cHeader8.Trim
                oReportDevX.cReportSQL9 = cSQL9.Trim : oReportDevX.cReportQueryName9 = cHeader9.Trim
                oReportDevX.cReportSQL10 = cSQL10.Trim : oReportDevX.cReportQueryName10 = cHeader10.Trim

                Dim oForm As New frmDashboardViewer
                oForm.init()
                DevXDashBoardView = oForm.Handle
            Else
                MsgBox("Dashboard yüklenemedi : " + cReportID)
            End If

        Catch ex As Exception
            ErrDisp("DevXDashBoardView : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Sub ReportToDDBXML(Optional cReportID As String = "")
        Try
            If cReportID.Trim = "" Then
                Exit Sub
            End If
            DDBExportSourceToFile(cReportID)

        Catch ex As Exception
            ErrDisp("ReportToDDBXML : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Sub DDBXMLToReport()
        Try
            DDBImportFileToReport()
        Catch ex As Exception
            ErrDisp("DDBXMLToReport : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Function DevXScheduler(Optional nMode As Integer = 0, Optional cTableName As String = "", Optional lModal As Boolean = True) As IntPtr
        Try
            Dim oForm As New frmGant4

            oForm.init(nMode, cTableName, lModal)
            DevXScheduler = oForm.Handle

        Catch ex As Exception
            ErrDisp("DDBXMLToReport : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function DevXSpreadSheet(Optional cParameter As String = "", Optional nMode As Integer = 1) As IntPtr
        Try
            Dim oForm As New frmSpreadSheet

            oForm.init(cParameter, nMode)
            DevXSpreadSheet = oForm.Handle

        Catch ex As Exception
            ErrDisp("DevXSpreadSheet : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function SuperEditor(cFileName As String) As IntPtr
        Try
            Dim oForm As New AdvancedEditor

            oForm.init(cFileName)
            SuperEditor = oForm.Handle

        Catch ex As Exception
            ErrDisp("SuperEditor : " + ex.Message + " " + cFileName, "HTMain",,, ex)
        End Try
    End Function

    Public Function ReportToPivot(cSQL As String) As IntPtr
        Try
            Dim oForm As New GridReport

            oForm.init(cSQL)
            ReportToPivot = oForm.Handle

        Catch ex As Exception
            ErrDisp("ReportToPivot : " + ex.Message, "HTMain", cSQL,, ex)
        End Try
    End Function

    Public Function ReportToSpread(cSQL As String) As IntPtr
        Try
            Dim oForm As New SpreadReport

            oForm.init(cSQL)
            ReportToSpread = oForm.Handle

        Catch ex As Exception
            ErrDisp("ReportToSpread : " + ex.Message, "HTMain", cSQL,, ex)
        End Try
    End Function

#End Region

#Region "Uyumsoft entegrasyonu"

    Public Sub UyumSysPar()
        Try
            Dim ofrmUyumParameters As New frmUyumParameters
            ofrmUyumParameters.ShowDialog()

        Catch ex As Exception
            ErrDisp("UyumSysPar : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Function UyumCarilerTable() As String

        UyumCarilerTable = ""

        Try
            UyumCarilerTable = UyumCarilerTable2()
        Catch ex As Exception
            ErrDisp("UyumCarilerTable", "HTMain",,, ex)
        End Try
    End Function

    Public Function UyumStoklarTable() As String

        UyumStoklarTable = ""

        Try
            UyumStoklarTable = UyumStoklarTable2()
        Catch ex As Exception
            ErrDisp("UyumStoklarTable", "HTMain",,, ex)
        End Try
    End Function

    Public Function UyumSatinalmaSiparisAdd(cIsemriNo As String, Optional ByRef cMessage As String = "") As Boolean

        UyumSatinalmaSiparisAdd = False

        Try
            UyumSatinalmaSiparisAdd = UyumSatinalmaSiparisEkle(1, cIsemriNo, cMessage)
            If UyumSatinalmaSiparisAdd Then
                UyumSatinalmaSiparisAdd = UyumSatinalmaSiparisEkle(2, cIsemriNo, cMessage)
            End If

        Catch ex As Exception
            ErrDisp("UyumSatinalmaSiparisAdd", "HTMain",,, ex)
        End Try
    End Function

    Public Function UyumSatinalmaSiparisDelete(cIsemriNo As String, Optional ByRef cMessage As String = "") As Boolean

        UyumSatinalmaSiparisDelete = False

        Try
            UyumSatinalmaSiparisDelete = UyumGenelSil(cIsemriNo, 2, cMessage)
            If UyumSatinalmaSiparisDelete Then
                UyumSatinalmaSiparisDelete = UyumGenelSil(cIsemriNo, 5, cMessage)
            End If

        Catch ex As Exception
            ErrDisp("UyumSatinalmaSiparisDelete", "HTMain",,, ex)
        End Try
    End Function

    Public Function UyumUretimSiparisAdd(ByVal cSiparisNo As String, Optional ByRef cMessage As String = "") As Boolean

        UyumUretimSiparisAdd = False

        Try
            UyumUretimSiparisAdd = UyumUretimSiparisEkle(1, cSiparisNo, cMessage)
            If UyumUretimSiparisAdd Then
                UyumUretimSiparisAdd = UyumUretimSiparisEkle(2, cSiparisNo, cMessage)
            End If

        Catch ex As Exception
            ErrDisp("UyumUretimSiparisAdd", "HTMain",,, ex)
        End Try
    End Function

    Public Function UyumUretimSiparisDelete(cSiparisNo As String, Optional ByRef cMessage As String = "") As Boolean

        UyumUretimSiparisDelete = False

        Try
            UyumUretimSiparisDelete = UyumGenelSil(cSiparisNo, 3, cMessage)
            If UyumUretimSiparisDelete Then
                UyumUretimSiparisDelete = UyumGenelSil(cSiparisNo, 4, cMessage)
            End If

        Catch ex As Exception
            ErrDisp("UyumUretimSiparisDelete", "HTMain",,, ex)
        End Try
    End Function

    Public Function UyumUretimAdd(cUretFisNo As String, Optional ByRef cMessage As String = "") As Boolean

        UyumUretimAdd = False

        Try
            UyumUretimAdd = UyumUretimEkle(cUretFisNo, cMessage)

        Catch ex As Exception
            ErrDisp("UyumUretimAdd", "HTMain",,, ex)
        End Try
    End Function

    Public Function UyumUretimAdd2(cUretFisNo As String, Optional ByRef cMessage As String = "") As Boolean
        ' stok irsaliyesi olarak üretim fişini atar
        UyumUretimAdd2 = False

        Try
            UyumUretimAdd2 = UyumUretimStokFisi(cUretFisNo, cMessage)

        Catch ex As Exception
            ErrDisp("UyumUretimAdd2", "HTMain",,, ex)
        End Try
    End Function

    Public Function UyumUretimDelete(cUretFisNo As String, Optional ByRef cMessage As String = "") As Boolean

        UyumUretimDelete = False

        Try
            UyumUretimDelete = UyumUretimSil(cUretFisNo, cMessage)

        Catch ex As Exception
            ErrDisp("UyumUretimDelete", "HTMain",,, ex)
        End Try
    End Function

    Public Function UyumUretimDelete2(cUretFisNo As String, Optional ByRef cMessage As String = "") As Boolean
        ' stok irsaliyesi olarak atılmış üretim fişini siler
        UyumUretimDelete2 = False

        Try
            UyumUretimDelete2 = UyumUretimSil2(cUretFisNo, cMessage)

        Catch ex As Exception
            ErrDisp("UyumUretimDelete2", "HTMain",,, ex)
        End Try
    End Function

    Public Function UyumUIEAdd(cSiparisNo As String, Optional ByRef cMessage As String = "") As Boolean

        UyumUIEAdd = False

        Try
            UyumUIEAdd = UyumUIEEkle(cSiparisNo, cMessage)

        Catch ex As Exception
            ErrDisp("UyumUIEAdd", "HTMain",,, ex)
        End Try
    End Function

    Public Function UyumUIEDelete(cSiparisNo As String, Optional ByRef cMessage As String = "") As Boolean

        UyumUIEDelete = False

        Try
            UyumUIEDelete = UyumUIESil(cSiparisNo, cMessage)

        Catch ex As Exception
            ErrDisp("UyumUIEDelete", "HTMain",,, ex)
        End Try
    End Function

    Public Function UyumBOMAdd(cModelNo As String, Optional ByRef cMessage As String = "") As Boolean

        UyumBOMAdd = False

        Try
            UyumBOMAdd = UyumBOMEkle(cModelNo, cMessage)

        Catch ex As Exception
            ErrDisp("UyumBOMAdd", "HTMain",,, ex)
        End Try
    End Function

    Public Function UyumBOMDelete(cModelNo As String, Optional ByRef cMessage As String = "") As Boolean

        UyumBOMDelete = False

        Try
            UyumBOMDelete = UyumBOMSil(cModelNo, cMessage)

        Catch ex As Exception
            ErrDisp("UyumBOMDelete", "HTMain",,, ex)
        End Try
    End Function

    Public Function UyumStokAdd(Optional cFilter As String = "", Optional ByRef cMessage As String = "", Optional lSilent As Boolean = False) As Boolean

        UyumStokAdd = False

        Try
            UyumStokAdd = UyumStokEkle(cFilter, cMessage, lSilent)

        Catch ex As Exception
            ErrDisp("UyumStokAdd", "HTMain",,, ex)
        End Try
    End Function

    Public Function UyumStokDelete(cStokNo As String, Optional ByRef cMessage As String = "") As Boolean

        UyumStokDelete = False

        Try
            UyumStokDelete = UyumGenelSil(cStokNo, 1, cMessage)

        Catch ex As Exception
            ErrDisp("UyumStokDelete", "HTMain",,, ex)
        End Try
    End Function

    Public Sub UyumCariWinTexOtoUpdate()
        Try
            UyumCariWinTexUpdate()

        Catch ex As Exception
            ErrDisp("UyumCariWinTexOtoUpdate", "HTMain",,, ex)
        End Try
    End Sub

    Public Function UyumStokFisiAktar(Optional cFilter As String = "", Optional ByRef cMessage As String = "") As Boolean

        UyumStokFisiAktar = False

        Try
            UyumStokFisiAktar = UyumStokFisAktar(cFilter, cMessage)

        Catch ex As Exception
            ErrDisp("UyumStokFisiAktar", "HTMain",,, ex)
        End Try
    End Function

    Public Function UyumStokFisiDelete(Optional cFilter As String = "", Optional ByRef cMessage As String = "") As Boolean

        UyumStokFisiDelete = False

        Try
            UyumStokFisiDelete = UyumStokFisSil(cFilter, cMessage)

        Catch ex As Exception
            ErrDisp("UyumStokFisiDelete", "HTMain",,, ex)
        End Try
    End Function

    Public Function UyumStokFisiKontrol(Optional cFilter As String = "", Optional ByRef cMessage As String = "") As Boolean

        UyumStokFisiKontrol = False

        Try
            UyumStokFisiKontrol = UyumStokFisCheck(cFilter, cMessage)

        Catch ex As Exception
            ErrDisp("UyumStokFisiKontrol", "HTMain",,, ex)
        End Try
    End Function

#End Region

#Region "Language Translator , tercüman"

    Public Sub Translator(Optional ByVal cInput As String = "", Optional ByRef cOutput As String = "", Optional ByVal cInputLanguage As String = "en", Optional ByVal cOutputLanguage As String = "tr")
        Try
            If cInput.Trim = "" Then
                cOutput = ""
                Exit Sub
            End If
            cOutput = MSTranslate(cInput, cInputLanguage, cOutputLanguage)
        Catch ex As Exception
            ErrDisp("Translator : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Sub Translate()
        Try
            Dim ofrmLanguage As New frmLanguage
            ofrmLanguage.init()

        Catch ex As Exception
            ErrDisp("Translate : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

#End Region

#Region "RPA prosedürler"

    'Public Sub ZaraPDFtoSiparis(Optional cPDF As String = "")
    '    ZaraPDFtoSiparis1(cPDF)
    'End Sub

    Public Sub RPAParametre()
        Try
            Dim ofrmRPAParameters As New frmRPAParameters
            ofrmRPAParameters.init()

        Catch ex As Exception
            ErrDisp("RPAParametre : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Function RPASiparis() As Boolean

        RPASiparis = True

        Try
            RPASiparis = RPASiparis1()

        Catch ex As Exception
            ErrDisp("RPASiparis : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function
#End Region

#Region "Diğer prosedürler"

    Public Function WinTexDragDrop2(cFieldValue As String, cDocType As String, cDocSubType As String) As Boolean

        WinTexDragDrop2 = False

        Try
            Dim ofrmDragDrop As New frmDragDrop
            WinTexDragDrop2 = ofrmDragDrop.init2(cFieldValue, cDocType, cDocSubType)

        Catch ex As Exception
            ErrDisp("WinTexDragDrop : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function WinTexDragDrop(nMode As Integer, cKey As String) As Boolean

        WinTexDragDrop = False

        Try
            Dim ofrmDragDrop As New frmDragDrop
            WinTexDragDrop = ofrmDragDrop.init(nMode, cKey)

        Catch ex As Exception
            ErrDisp("WinTexDragDrop : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Sub RotatePDF(cPDFFileToRotate As String, Optional nRotateAngle As Integer = 270)
        Try
            RotatePDFFile(cPDFFileToRotate, nRotateAngle)

        Catch ex As Exception
            ErrDisp("RotatePDF : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Function DownloadFTP(Optional ftpAddress As String = "",
                                Optional ftpUser As String = "",
                                Optional ftpPassword As String = "",
                                Optional fileToDownload As String = "",
                                Optional downloadTargetFolder As String = "") As Boolean
        DownloadFTP = False

        Try
            DownloadFTP = DownloadSingleFile(ftpAddress, ftpUser, ftpPassword, fileToDownload, downloadTargetFolder, False)

        Catch ex As Exception
            ErrDisp("DownloadFTP : " + ex.Message, "htmain",,, ex)
        End Try
    End Function

    Public Function WintexMAP(Optional cSQL As String = "") As IntPtr
        Try
            Dim oForm As New frmMap

            oForm.init(cSQL)
            WintexMAP = oForm.Handle

        Catch ex As Exception
            ErrDisp("WintexMAP : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Sub ResimSpread(Optional cSiparisNo As String = "")
        Try
            Dim oForm As New frmSipPictures
            oForm.init(cSiparisNo)

        Catch ex As Exception
            ErrDisp("ResimSpread : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Sub DevxBrowse(Optional nMode As Integer = 1, Optional lModal As Boolean = True)

        Try
            Dim ofrmBrowse As New frmBrowse
            ofrmBrowse.init(nMode, lModal)

        Catch ex As Exception
            ErrDisp("DevxBrowse : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub


    'Private Sub InitGroupDocs()

    '    Try
    '        Dim LData As String = "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiPz4NCjxMaWNlbnNlPg0KICAgIDxEYXRhPg0KICAgICAgICA8TGljZW5zZWRUbz5MaWNlbnNlZTwvTGljZW5zZWRUbz4NCiAgICAgICAgPEVtYWlsVG8+bGljZW5zZWVAZW1haWwuY29tPC9FbWFpbFRvPg0KICAgICAgICA8TGljZW5zZVR5cGU+RGV2ZWxvcGVyIE9FTTwvTGljZW5zZVR5cGU+DQogICAgICAgIDxMaWNlbnNlTm90ZT5MaW1pdGVkIHRvIDEwMDAgZGV2ZWxvcGVyLCB1bmxpbWl0ZWQgcGh5c2ljYWwgbG9jYXRpb25zPC9MaWNlbnNlTm90ZT4NCiAgICAgICAgPE9yZGVySUQ+Nzg0Mzc4NTc3ODU8L09yZGVySUQ+DQogICAgICAgIDxVc2VySUQ+MTE5Nzg5MjQzNzk8L1VzZXJJRD4NCiAgICAgICAgPE9FTT5UaGlzIGlzIGEgcmVkaXN0cmlidXRhYmxlIGxpY2Vuc2U8L09FTT4NCiAgICAgICAgPFByb2R1Y3RzPg0KICAgICAgICAgICAgPFByb2R1Y3Q+R3JvdXBEb2NzLlRvdGFsIFByb2R1Y3QgRmFtaWx5PC9Qcm9kdWN0Pg0KICAgICAgICA8L1Byb2R1Y3RzPg0KICAgICAgICA8RWRpdGlvblR5cGU+RW50ZXJwcmlzZTwvRWRpdGlvblR5cGU+DQogICAgICAgIDxTZXJpYWxOdW1iZXI+e0YyQjk3MDQ1LTFCMjktNEIzRi1CRDUzLTYwMUVGRkExNUFBOX08L1NlcmlhbE51bWJlcj4NCiAgICAgICAgPFN1YnNjcmlwdGlvbkV4cGlyeT4yMDk5MTIzMTwvU3Vic2NyaXB0aW9uRXhwaXJ5Pg0KICAgICAgICA8TGljZW5zZVZlcnNpb24+MTcuMDwvTGljZW5zZVZlcnNpb24+DQogICAgPC9EYXRhPg0KICAgIDxTaWduYXR1cmU+UVhOd2IzTmxMbFJ2ZEdGc0lGQnliMlIxWTNRZ1JtRnRhV3g1PC9TaWduYXR1cmU+DQo8L0xpY2Vuc2U+"
    '        Dim Stream As Stream = New MemoryStream(Convert.FromBase64String(LData))
    '        Dim license As GroupDocs.Viewer.License = New GroupDocs.Viewer.License

    '        Stream.Seek(0, SeekOrigin.Begin)
    '        license.SetLicense(Stream)

    '    Catch ex As Exception
    '        ErrDisp("InitGroupDocs", "HTMain",,, ex)
    '    End Try
    'End Sub

    'Public Function GroupDocsShow(Optional cFileName As String = "") As String
    '    Try
    '        Dim cOutputFile As String = Path.GetTempFileName()  ' "C:\wintex\groupdocs\output.pdf"
    '        If cFileName.Trim = "" Then Exit Function

    '        If (My.Computer.FileSystem.DirectoryExists("C:\wintex\groupdocs") = False) Then
    '            My.Computer.FileSystem.CreateDirectory("C:\wintex\groupdocs")
    '        End If

    '        Dim oViewer As New GroupDocs.Viewer.Viewer(cFileName)
    '        Dim oOptions As New GroupDocs.Viewer.Options.PdfViewOptions(cOutputFile)

    '        oViewer.View(oOptions)

    '        GroupDocsShow = cOutputFile

    '    Catch ex As Exception
    '        ErrDisp("GroupDocsShow", "HTMain",,, ex)
    '    End Try
    'End Function
#End Region

End Class


