Option Explicit On
Option Strict On

Imports System.Configuration
Imports System.Timers
Imports System.Diagnostics
Imports System.Data.SqlClient

Imports System.Xml
Imports System.Net
Imports System
Imports System.IO
Imports System.Globalization

Public Class WinTexServiceMain

    Public oTimer1 As System.Timers.Timer
    Public oTimer2 As System.Timers.Timer

    Dim lOUS2PopulateQueue As Boolean = False
    Dim lOUS2Execute As Boolean = False

    Dim cOUSList As String = "'STI-Report'," +
                             "'OUS2 Test Mesajı'," +
                             "'DOS Komutu Calistir'," +
                             "'Styleshoots Transfer'," +
                             "'Styleshoots Yedekleme'," +
                             "'RPA Inditex Siparis'," +
                             "'Postgre SQL islet'," +
                             "'SQL baska veritabani islet'," +
                             "'SQL islet'," +
                             "'Merkez Bankasindan Gunluk Kurlarin Cekilmesi'," +
                             "'Merkez Bankasindan Eksik Kurlarin Cekilmesi'," +
                             "'Logo - Gunluk Kurlarin WinTexe Cekilmesi'," +
                             "'Uyumsoft - Gunluk Kurlarin WinTexe Cekilmesi'," +
                             "'Uyumsoft - Stok Kartlarini Aktar'," +
                             "'Uyumsoft - Stok Hareketlerini Aktar'," +
                             "'Uyumsoft - Model Recetelerini Aktar'," +
                             "'Uyumsoft - Alinan Ihracat Siparislerini Aktar'," +
                             "'Uyumsoft - Uretim Emirlerini Aktar'," +
                             "'Uyumsoft - Verilen (Satinalma) Siparisler Aktar'," +
                             "'Uyumsoft - Uretim (Hareket Fisleri) Aktar'," +
                             "'MTF Bakimi'," +
                             "'STF Bakimi'," +
                             "'UTF Bakimi'," +
                             "'Hizli Stok Bakimi'," +
                             "'Üretim İşemirleri Bakımı'," +
                             "'Çorap Konveyor'," +
                             "'Çorap Stok Bakimi'," +
                             "'Çorap Malzeme İşemirleri Bakımı'," +
                             "'On Maliyet3 eMail Tekrar Gonderim'," +
                             "'Num.Kum.Satinalma Talep eMail Tekrar Gonderim'," +
                             "'Num.Kum.Rezervasyon Talep eMail Tekrar Gonderim'," +
                             "'Perakende Siparis Son Durum'," +
                             "'Perakende Siparis Son Durum Iadeler'," +
                             "'Perakende Siparis Son Durum Kapalilar'," +
                             "'Perakende Siparis Son Durum Iptaller'," +
                             "'Perakende Siparis Musteri SMS'," +
                             "'Stok Bakim Stored Procedure'," +
                             "'Oto Rezervasyon Satinalma Isemri'," +
                             "'Oto Rezervasyon Satinalma Isemri kit disi'," +
                             "'Oto Rezervasyon Satinalma uyari'," +
                             "'Oto Rezervasyon Satinalma uyari kit disi'," +
                             "'TiciMax Urun Upload'," +
                             "'TiciMax Siparis Upload'," +
                             "'TiciMax Siparis Download'"

    Dim cServer As String = ""
    Dim cDatabase As String = ""
    Dim cUsername As String = ""
    Dim cPassword As String = ""

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.CanHandlePowerEvent = True
        Me.CanHandleSessionChangeEvent = True
        Me.CanPauseAndContinue = True
        Me.CanShutdown = True
        Me.CanStop = True

        EventLog1 = New System.Diagnostics.EventLog

        If Not System.Diagnostics.EventLog.SourceExists("WintexService") Then
            System.Diagnostics.EventLog.CreateEventSource("WintexService", "WintexServiceLog")
        End If

        EventLog1.Source = "WintexService"
        EventLog1.Log = "WintexServiceLog"

        lOUS2PopulateQueue = False
        lOUS2Execute = False
        oWinTexServiceMain = Me

    End Sub

    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Add code here to start your service. This method should set things
        ' in motion so your service can do its work.

        Try
#If DEBUG Then
            System.Diagnostics.Debugger.Launch()
            Do While Not Debugger.IsAttached
                Threading.Thread.Sleep(1000)
            Loop
#End If
            ' populate queue / interval 1 dakika
            oTimer1 = New System.Timers.Timer With {.Interval = 60000}
            AddHandler oTimer1.Elapsed, New ElapsedEventHandler(AddressOf Timer1Ticking)
            oTimer1.AutoReset = True
            oTimer1.Enabled = True
            oTimer1.Start()

            ' execute queue / interval 1 dakika
            oTimer2 = New System.Timers.Timer With {.Interval = 60000}
            AddHandler oTimer2.Elapsed, New ElapsedEventHandler(AddressOf Timer2Ticking)
            oTimer2.AutoReset = True
            oTimer2.Enabled = True
            oTimer2.Start()

            If (My.Computer.FileSystem.DirectoryExists("C:\wintex\Temp") = False) Then
                My.Computer.FileSystem.CreateDirectory("C:\wintex\Temp")
            End If

            Environment.SetEnvironmentVariable("TEMP", "C:\wintex\Temp", EnvironmentVariableTarget.Process)
            Environment.SetEnvironmentVariable("TMP", "C:\wintex\Temp", EnvironmentVariableTarget.Process)

            CreateLog("Service Start")


        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Sub

    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.
        Try
            oTimer1.Stop()
            oTimer1.Dispose()

            oTimer2.Stop()
            oTimer2.Dispose()

            CreateLog("Service End")

        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Sub

    Protected Overrides Sub OnPause()
        Try
            CreateLog("Service Paused")

        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Sub

    Protected Overrides Sub OnContinue()
        Try
            CreateLog("Service Continue")

        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Sub

    Protected Overrides Sub OnShutdown()
        Try
            CreateLog("Service Shutdown")

        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Sub

    Private Sub Timer1Ticking(ByVal sender As Object, ByVal e As ElapsedEventArgs)
        Try
            'RunOUS2PopulateQueue()

            If lOUS2PopulateQueue Then Exit Sub
            lOUS2PopulateQueue = True
            OUS2PopulateQueue()
            lOUS2PopulateQueue = False

        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Sub

    Private Sub Timer2Ticking(ByVal sender As Object, ByVal e As ElapsedEventArgs)
        Try
            'RunOUS2Execute()

            If lOUS2Execute Then Exit Sub
            lOUS2Execute = True
            OUS2Execute()
            lOUS2Execute = False

        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Sub

    'Private Sub RunOUS2PopulateQueue()
    '    Try
    '        Dim oThread As Threading.Thread

    '        oThread = New Threading.Thread(AddressOf OUS2PopulateQueue) With {.IsBackground = True}
    '        oThread.Start()

    '    Catch ex As Exception
    '        ErrDisp(ex)
    '    End Try
    'End Sub

    'Private Sub RunOUS2Execute()
    '    Try
    '        Dim oThread As Threading.Thread

    '        oThread = New Threading.Thread(AddressOf OUS2Execute) With {.IsBackground = True}
    '        oThread.Start()

    '    Catch ex As Exception
    '        ErrDisp(ex)
    '    End Try
    'End Sub

    Private Sub OUS2PopulateQueue()

        Try
            Dim nCnt As Integer
            Dim cServerName As String
            Dim cDatabaseName As String
            Dim cUsernameName As String
            Dim cPasswordName As String
            Dim cServer As String
            Dim cDatabase As String
            Dim cUsername As String
            Dim cPassword As String

            For nCnt = 1 To 10
                cServerName = "SERVER" + nCnt.ToString
                cDatabaseName = "DATABASE" + nCnt.ToString
                cUsernameName = "USERNAME" + nCnt.ToString
                cPasswordName = "PASSWORD" + nCnt.ToString

                cServer = ConfigurationManager.AppSettings(cServerName)
                cDatabase = ConfigurationManager.AppSettings(cDatabaseName)
                cUsername = ConfigurationManager.AppSettings(cUsernameName)
                cPassword = ConfigurationManager.AppSettings(cPasswordName)

                If Not (cServer.Trim = "" Or cDatabase.Trim = "" Or cUsername.Trim = "" Or cPassword.Trim = "") Then
                    OUS2PopQue(cDatabase)
                End If
            Next

        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Sub

    Private Sub OUS2PopQue(cDatabase As String)

        Dim oSQL As SQLServerClass
        Dim cGun As String = ""
        Dim dNow As Date
        Dim nBugun As Double = 0
        Dim cOUSNo As String = ""
        Dim cTekrar As String = ""

        Dim nYil As Double = 0
        Dim nAy As Double = 0
        Dim cAy As String = ""
        Dim nSaat As Double = 0
        Dim nDakika As Double = 0

        Dim cYil2 As String = ""
        Dim nYil2 As Double = 0
        Dim cAy2 As String = ""
        Dim nAy2 As Double = 0
        Dim cGun2 As String = ""
        Dim nGun2 As Double = 0
        Dim cSaat2 As String = ""
        Dim nSaat2 As Double = 0
        Dim cDakika2 As String = ""
        Dim nDakika2 As Double = 0

        Try
            oSQL = New SQLServerClass(True,, cDatabase)

            dNow = Now

            Select Case Weekday(dNow)
                Case 1 : cGun = "PAZAR"
                Case 2 : cGun = "PAZARTESI"
                Case 3 : cGun = "SALI"
                Case 4 : cGun = "CARSAMBA"
                Case 5 : cGun = "PERSEMBE"
                Case 6 : cGun = "CUMA"
                Case 7 : cGun = "CUMARTESI"
            End Select

            nYil = Year(dNow)
            nAy = Month(dNow)
            cAy = monthstr2(nAy)
            nSaat = Hour(dNow)
            nDakika = Minute(dNow)
            nBugun = Day(dNow)

            oSQL.OpenConn()

            If cDatabase.Trim = "gecit" Then
                ' eklenmiş , 1 saat geçmiş , bitmemiş görevleri sil
                oSQL.cSQLQuery = "delete ous2queue " +
                                 " where DateDiff(Hour, createdate, GETDATE()) > 1 " +
                                 " and bitis is NULL "
                oSQL.SQLExecute()
            End If

            oSQL.cSQLQuery = "select a.sirano, a.ousno, a.tekrar, a.gun, a.ay, a.yil, a.saat, a.dakika, a.ous " +
                            " from ous2 a with (NOLOCK) " +
                            " where a.baslangic <= getdate() " +
                            " and a.bitis >= getdate() " +
                            " and a.ousno is not null " +
                            " and a.ousno <> '' " +
                            " and a.tekrar is not null " +
                            " and a.tekrar <> '' " +
                            " and a.tekrar <> 'BELIRSIZ' " +
                            " and a.serviscalistirsin is not null " +
                            " and a.serviscalistirsin = 'E' " +
                            " and a.ous in (" + cOUSList + ") " +
                            " and not exists (select x.*, y.ous " +
                                            " from ous2queue x with (NOLOCK) , ous2 y with (NOLOCK) " +
                                            " where x.ousno = y.ousno " +
                                            " and x.bitis is null " +
                                            " and y.ous = a.ous ) " +
                            " order by a.ousno "

            oSQL.GetSQLReader()

            Do While oSQL.oReader.Read

                cOUSNo = oSQL.SQLReadString("ousno")
                cTekrar = oSQL.SQLReadString("tekrar")

                cGun2 = oSQL.SQLReadString("gun")
                If IsNumeric(cGun2) Then
                    nGun2 = CDbl(cGun2)
                Else
                    nGun2 = 0
                End If

                cAy2 = oSQL.SQLReadString("ay")
                If IsNumeric(cAy2) Then
                    nAy2 = CDbl(cAy2)
                Else
                    nAy2 = 0
                End If

                cYil2 = oSQL.SQLReadString("yil")
                If IsNumeric(cYil2) Then
                    nYil2 = CDbl(cYil2)
                Else
                    nYil2 = 0
                End If

                cSaat2 = oSQL.SQLReadString("saat")
                If IsNumeric(cSaat2) Then
                    nSaat2 = CDbl(cSaat2)
                Else
                    nSaat2 = 0
                End If

                cDakika2 = oSQL.SQLReadString("dakika")
                If IsNumeric(cDakika2) Then
                    nDakika2 = CDbl(cDakika2)
                Else
                    nDakika2 = 0
                End If

                Select Case cTekrar
                    Case "SAATLIK"
                        If nDakika = nDakika2 Then
                            OUS2AddToQueue(cOUSNo, cDatabase)
                        End If
                    Case "GUNLUK"
                        If nSaat = nSaat2 And nDakika = nDakika2 Then
                            OUS2AddToQueue(cOUSNo, cDatabase)
                        End If
                    Case "HAFTALIK"
                        If cGun = cGun2 And nSaat = nSaat2 And nDakika = nDakika2 Then
                            OUS2AddToQueue(cOUSNo, cDatabase)
                        End If
                    Case "YILLIK"
                        If cAy = cAy2 And cGun = cGun2 And nSaat = nSaat2 And nDakika = nDakika2 Then
                            OUS2AddToQueue(cOUSNo, cDatabase)
                        End If
                    Case "BIRDEFA"
                        If nYil = nYil2 And cAy = cAy2 And cGun = cGun2 And nSaat = nSaat2 And nDakika = nDakika2 Then
                            OUS2AddToQueue(cOUSNo, cDatabase)
                        End If
                End Select
            Loop
            oSQL.oReader.Close()

            oSQL.CloseConn()
            oSQL = Nothing

        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Sub

    Private Sub OUS2AddToQueue(cOUSNo As String, cDatabase As String)
        Try
            Dim oSQL As SQLServerClass

            If cOUSNo.Trim = "" Then Exit Sub

            oSQL = New SQLServerClass(True,, cDatabase)
            oSQL.cSQLQuery = "insert ous2queue (ousno, createdate, ousversion) values ('" + SQLWriteString(cOUSNo, 10) + "',getdate(),'" + cVersion + "') "
            oSQL.SQLExecuteOpenCloseConnection()
            oSQL = Nothing

        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Sub

    Private Function monthstr2(mn As Double) As String

        monthstr2 = "OCAK"

        Try
            Select Case mn
                Case 1 : monthstr2 = "OCAK"
                Case 2 : monthstr2 = "SUBAT"
                Case 3 : monthstr2 = "MART"
                Case 4 : monthstr2 = "NISAN"
                Case 5 : monthstr2 = "MAYIS"
                Case 6 : monthstr2 = "HAZIRAN"
                Case 7 : monthstr2 = "TEMMUZ"
                Case 8 : monthstr2 = "AGUSTOS"
                Case 9 : monthstr2 = "EYLUL"
                Case 10 : monthstr2 = "EKIM"
                Case 11 : monthstr2 = "KASIM"
                Case 12 : monthstr2 = "ARALIK"
            End Select

        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Function

    Private Sub OUS2Execute()

        Try
            Dim nCnt As Integer
            Dim cServerName As String
            Dim cDatabaseName As String
            Dim cUsernameName As String
            Dim cPasswordName As String
            Dim cServer As String
            Dim cDatabase As String
            Dim cUsername As String
            Dim cPassword As String

            For nCnt = 1 To 10
                cServerName = "SERVER" + nCnt.ToString
                cDatabaseName = "DATABASE" + nCnt.ToString
                cUsernameName = "USERNAME" + nCnt.ToString
                cPasswordName = "PASSWORD" + nCnt.ToString

                cServer = ConfigurationManager.AppSettings(cServerName)
                cDatabase = ConfigurationManager.AppSettings(cDatabaseName)
                cUsername = ConfigurationManager.AppSettings(cUsernameName)
                cPassword = ConfigurationManager.AppSettings(cPasswordName)

                If Not (cServer Is Nothing) Then
                    If Not (cServer.Trim = "" Or cDatabase.Trim = "" Or cUsername.Trim = "" Or cPassword.Trim = "") Then
                        OUS2MultiDBExecute(cServer, cDatabase, cUsername, cPassword)
                    End If
                End If
            Next

        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Sub

    Private Sub OUS2MultiDBExecute(cServer As String, cDatabase As String, cUsername As String, cPassword As String)
        Try
            Dim oSQL As SQLServerClass
            Dim cOUSNo As String = ""
            Dim cOUS As String = ""
            Dim nSiraNo As Double = 0
            Dim cAciklama As String = ""

            oSQL = New SQLServerClass(True,, cDatabase)

            If Not oSQL.OpenConn() Then Exit Sub

            ' baslamamis ilk isi ele alir
            oSQL.cSQLQuery = "select top 1 a.sirano, a.ousno, b.ous, b.aciklama " +
                            " from ous2queue a with (NOLOCK), ous2 b with (NOLOCK) " +
                            " where a.ousno = b.ousno " +
                            " and a.ousno is not null " +
                            " and a.ousno <> '' " +
                            " and a.ousno <> 'BELIRSIZ' " +
                            " and a.baslangic is null " +
                            " and b.ous in (" + cOUSList + ") " +
                            " and b.serviscalistirsin is not null " +
                            " and b.serviscalistirsin = 'E' " +
                            " order by a.sirano "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                nSiraNo = oSQL.SQLReadDouble("sirano")
                cOUSNo = oSQL.SQLReadString("ousno")
                cOUS = oSQL.SQLReadString("ous")
                cAciklama = oSQL.SQLReadString("aciklama")
            Else
                'CreateLog("Başlatılacak görev bulunamadı")
            End If
            oSQL.oReader.Close()

            If nSiraNo = 0 Then Exit Sub

            oSQL.SetSysPar("wintexserviceversion", cVersion)

            CreateLog("Görev Başlatıldı : " + cOUS + " / " + cAciklama)

            oSQL.cSQLQuery = "update ous2queue " +
                             " set baslangic = getdate() , " +
                             " MachineName = 'TBA' " +
                             " where sirano = " + CStr(nSiraNo)

            oSQL.SQLExecute()

            If OUS2CoreExecute(cOUSNo, cServer, cDatabase, cUsername, cPassword) Then

                CreateLog("Görev başarıldı : " + cOUS + " / " + cAciklama)

                oSQL.cSQLQuery = "update ous2queue " +
                                 " set sonuc = 'success' , " +
                                 " bitis = getdate() , " +
                                 " MachineName = 'TBA' " +
                                 " where sirano = " + CStr(nSiraNo)

                oSQL.SQLExecute()

            Else
                CreateLog("Görev BAŞARISIZ : " + cOUS + " / " + cAciklama)

                oSQL.cSQLQuery = "update ous2queue " +
                                 " set sonuc = 'fail', " +
                                 " bitis = getdate() , " +
                                 " MachineName = 'TBA' " +
                                 " where sirano = " + CStr(nSiraNo)

                oSQL.SQLExecute()

            End If

            oSQL.CloseConn()
            oSQL = Nothing

        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Sub

    Private Sub StyleShoots(cCommand1 As String)

        Try
            Dim cStyleShootsKaynakDomain As String = ConfigurationManager.AppSettings("StyleShootsKaynakDomain").ToString()
            Dim cStyleShootsKaynakPath As String = ConfigurationManager.AppSettings("StyleShootsKaynakPath").ToString()
            Dim cStyleShootsKaynakUsername As String = ConfigurationManager.AppSettings("StyleShootsKaynakUsername").ToString()
            Dim cStyleShootsKaynakPassword As String = ConfigurationManager.AppSettings("StyleShootsKaynakPassword").ToString()

            Dim cStyleShootsHedefDomain As String = ConfigurationManager.AppSettings("StyleShootsHedefDomain").ToString()
            Dim cStyleShootsHedefPath As String = ConfigurationManager.AppSettings("StyleShootsHedefPath").ToString()
            Dim cStyleShootsHedefUsername As String = ConfigurationManager.AppSettings("StyleShootsHedefUsername").ToString()
            Dim cStyleShootsHedefPassword As String = ConfigurationManager.AppSettings("StyleShootsHedefPassword").ToString()

            Dim cCommand2 As String = "Net Use " + cStyleShootsKaynakPath + " /user:" + cStyleShootsKaynakUsername + " " + cStyleShootsKaynakPassword
            Dim cCommand3 As String = "Net Use " + cStyleShootsHedefPath + " /user:" + cStyleShootsHedefUsername + " " + cStyleShootsHedefPassword

            Process.Start("cmd.exe", " /k " + cCommand2 + " & " + cCommand3 + " & " + cCommand1)
            ' xcopy \\192.168.10.127\styleshoots\*.* \\192.168.1.204\styleshoots /s /d /y

        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Sub

    Public Function OUS2CoreExecute(cOUSNo As String, cServer As String, cDatabase As String, cUsername As String, cPassword As String) As Boolean

        Dim cParametre1 As String = ""
        Dim cParametre2 As String = ""
        Dim cParametre3 As String = ""
        Dim cParametre4 As String = ""
        Dim cParametre5 As String = ""
        Dim cParametre6 As String = ""
        Dim cParametre7 As String = ""
        Dim cParametre8 As String = ""
        Dim cParametre9 As String = ""
        Dim cParametre10 As String = ""
        Dim cSQLCheck As String = ""
        Dim nReportID As Double = 0
        Dim cSQL As String = ""
        Dim cOUS As String = ""
        Dim cFileName As String = ""
        Dim cTarih1 As String = ""
        Dim cTarih2 As String = ""
        Dim cMessage As String = ""
        Dim lOK As Boolean = False
        Dim oSQL As SQLServerClass
        Dim oSQL2 As SQLServerClass
        Dim oSQL3 As PostgreClass
        Dim oWinTexDLL As WinTexDLL.HTMain
        Dim tBegin As DateTime = DateTime.Parse("8:00:00 AM")
        Dim tEnd As DateTime = DateTime.Parse("10:00:00 PM")
        Dim nSonuc As Integer = 0

        OUS2CoreExecute = False

        Try
            If cOUSNo.Trim = "" Then Exit Function

            oSQL = New SQLServerClass(True,, cDatabase)

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select top 1 ous, reportid, " +
                            " parametre1, parametre2, parametre3, parametre4, parametre5, " +
                            " parametre6, parametre7, parametre8, parametre9, parametre10, " +
                            " sqlcheck " +
                            " from ous2 with (NOLOCK)  " +
                            " where ousno = '" + cOUSNo.Trim + "' "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                nReportID = CDbl(oSQL.SQLReadInteger("reportid"))
                cOUS = oSQL.SQLReadString("ous")
                cParametre1 = oSQL.SQLReadString("parametre1")
                cParametre2 = oSQL.SQLReadString("parametre2")
                cParametre3 = oSQL.SQLReadString("parametre3")
                cParametre4 = oSQL.SQLReadString("parametre4")
                cParametre5 = oSQL.SQLReadString("parametre5")
                cParametre6 = oSQL.SQLReadString("parametre6")
                cParametre7 = oSQL.SQLReadString("parametre7")
                cParametre8 = oSQL.SQLReadString("parametre8")
                cParametre9 = oSQL.SQLReadString("parametre9")
                cParametre10 = oSQL.SQLReadString("parametre10")
                cSQLCheck = oSQL.SQLReadString("sqlcheck")
            End If
            oSQL.oReader.Close()

            cParametre1 = cParametre1.Replace("||", "'")
            cParametre2 = cParametre2.Replace("||", "'")
            cParametre3 = cParametre3.Replace("||", "'")
            cParametre4 = cParametre4.Replace("||", "'")
            cParametre5 = cParametre5.Replace("||", "'")
            cParametre6 = cParametre6.Replace("||", "'")
            cParametre7 = cParametre7.Replace("||", "'")
            cParametre8 = cParametre8.Replace("||", "'")
            cParametre9 = cParametre9.Replace("||", "'")
            cParametre10 = cParametre10.Replace("||", "'")
            cSQLCheck = cSQLCheck.Replace("||", "'")

            lOK = True

            If Trim$(cSQLCheck) <> "" Then
                oSQL.cSQLQuery = Trim$(cSQLCheck)
                lOK = oSQL.CheckExists()
            End If

            oSQL.CloseConn()
            oSQL = Nothing

            If cOUS = "STI-Report" And nReportID <> 0 Then
                OUS2CoreExecute = STIEvent(cParametre1, nReportID, cParametre2, cParametre3, lOK, cDatabase, cParametre4, cParametre5)
                Exit Function
            End If

            If lOK Then

                Select Case cOUS

                    Case "Oto Rezervasyon Satinalma uyari"
                        OUS2CoreExecute = ORSHatirlatma(cDatabase, 1)
                        Exit Function

                    Case "Oto Rezervasyon Satinalma uyari kit disi"
                        OUS2CoreExecute = ORSHatirlatma(cDatabase, 2)
                        Exit Function

                    Case "Oto Rezervasyon Satinalma Isemri"
                        OUS2CoreExecute = OtomatikRezerveSatinalma(cDatabase, 1)
                        Exit Function

                    Case "Oto Rezervasyon Satinalma Isemri kit disi"
                        OUS2CoreExecute = OtomatikRezerveSatinalma(cDatabase, 2)
                        Exit Function

                    Case "Styleshoots Transfer"
                        oWinTexDLL = New WinTexDLL.HTMain
                        If oWinTexDLL.init(cServer, cDatabase, cUsername, cPassword) Then
                            oWinTexDLL.StyleShootsCopy()
                        End If
                        oWinTexDLL = Nothing

                    Case "Styleshoots Yedekleme"
                        StyleShoots(cParametre1)

                    Case "DOS Komutu Calistir"
                        nSonuc = Shell(cParametre1, AppWinStyle.MinimizedNoFocus, True)

                    Case "RPA Inditex Siparis"
                        oWinTexDLL = New WinTexDLL.HTMain
                        If oWinTexDLL.init(cServer, cDatabase, cUsername, cPassword) Then
                            If Not oWinTexDLL.RPASiparis Then
                                CreateLog("Dikkat RPASiparis calistirilamadi : " + cServer + " , " + cDatabase + " , " + cUsername + " , " + cPassword)
                                Exit Function
                            End If
                        End If
                        oWinTexDLL = Nothing

                    Case "Perakende Siparis Musteri SMS"

                        If tBegin.TimeOfDay < DateTime.Now.TimeOfDay And
                            tEnd.TimeOfDay > DateTime.Now.TimeOfDay And
                            DateTime.Now.DayOfWeek <> DayOfWeek.Sunday Then

                            Dim oKargo As New WinTexMNG.HTMain

                            If oKargo.init(cServer, cDatabase, cUsername, cPassword) Then

                                oSQL2 = New SQLServerClass(True,, cDatabase)
                                oSQL2.OpenConn()
                                oSQL2.cSQLQuery = "select distinct siparisno " +
                                                " from sipperakende with (NOLOCK) " +
                                                " where telefon is Not null " +
                                                " And telefon <> '' " +
                                                " And kargostatu in ('YOLDA','VARIS SUBEDE','SORUNLU','IADE') " +
                                                " And kargostatu <> coalesce(smskargostatu,'') " +
                                                " And kargostatu is Not null " +
                                                " And kargostatu <> '' " +
                                                " And (kapandi = 'H' or kapandi is null or kapandi = '') " +
                                                " And (iptal = 'H' or iptal is null or iptal = '') " +
                                                " And kargostatutarihi > coalesce(smsgonderme,'01.01.1950') " +
                                                " And kargostatutarihi Is Not null " +
                                                " And kargostatutarihi <> '01.01.1950' " +
                                                " And kargostatutarihi > dateadd(day,-3,getdate()) " +
                                                " order by siparisno  "

                                oSQL2.GetSQLReader()

                                Do While oSQL2.oReader.Read
                                    oKargo.SendSmsSipPerakende(oSQL2.SQLReadString("siparisno"))
                                Loop
                                oSQL2.oReader.Close()
                                oSQL2.CloseConn()
                                oSQL2 = Nothing
                                CreateLog("Perakende Siparis Musteri SMS tamamlandı : " + cDatabase)
                            End If
                            oKargo = Nothing
                        Else
                            CreateLog("Perakende Siparis Musteri SMS YAPILMADI : " + cDatabase)
                        End If

                    Case "TiciMax Urun Upload"
                        Dim oTiciMax As New WinTexTicimax.HTMain
                        If oTiciMax.init(cServer, cDatabase, cUsername, cPassword) Then
                            If oTiciMax.UploadUrunler Then
                                CreateLog("TiciMax Urun Upload tamamlandı : " + cDatabase)
                            End If
                        End If
                        oTiciMax = Nothing

                    Case "TiciMax Siparis Upload"
                        Dim oTiciMax As New WinTexTicimax.HTMain
                        If oTiciMax.init(cServer, cDatabase, cUsername, cPassword) Then
                            oTiciMax.UploadSiparisler()
                            CreateLog("TiciMax Sipariş Upload tamamlandı : " + cDatabase)
                        End If
                        oTiciMax = Nothing

                    Case "TiciMax Siparis Download"
                        Dim oTiciMax As New WinTexTicimax.HTMain
                        If oTiciMax.init(cServer, cDatabase, cUsername, cPassword) Then
                            oTiciMax.DownloadSiparisler()
                            CreateLog("TiciMax Siparis Download tamamlandı : " + cDatabase)
                        End If
                        oTiciMax = Nothing

                    Case "OUS2 Test Mesajı"
                        CreateLog("Job Test : OK")

                    Case "Stok Bakim Stored Procedure"
                        oSQL2 = New SQLServerClass(True,, cDatabase)
                        oSQL2.OpenConn()
                        OUS2CoreExecute = oSQL2.CLRExecute("stokbakim")
                        oSQL2.CloseConn()
                        oSQL2 = Nothing
                        Exit Function

                    Case "Perakende Siparis Son Durum"
                        If tBegin.TimeOfDay < DateTime.Now.TimeOfDay And
                            tEnd.TimeOfDay > DateTime.Now.TimeOfDay And
                            DateTime.Now.DayOfWeek <> DayOfWeek.Sunday Then

                            Dim oKargo As New WinTexMNG.HTMain

                            If oKargo.init(cServer, cDatabase, cUsername, cPassword) Then
                                If oKargo.SipPerakendeSonDurum(1) Then
                                    CreateLog("Perakende Siparis Son Durum tamamlandı : " + cDatabase)
                                Else
                                    OUS2CoreExecute = False
                                    oKargo = Nothing
                                    Exit Function
                                End If
                            End If
                            oKargo = Nothing
                        Else
                            CreateLog("Perakende Siparis Son Durum mesai dışı saatler olması sebebiyle YAPILMADI : " + cDatabase)
                        End If

                    Case "Perakende Siparis Son Durum Iadeler"
                        Dim oKargo As New WinTexMNG.HTMain
                        If oKargo.init(cServer, cDatabase, cUsername, cPassword) Then
                            If oKargo.SipPerakendeSonDurum(2, True) Then
                                CreateLog("Perakende Siparis Son Durum Iadeler tamamlandı : " + cDatabase)
                            Else
                                OUS2CoreExecute = False
                                oKargo = Nothing
                                Exit Function
                            End If
                        End If
                        oKargo = Nothing

                    Case "Perakende Siparis Son Durum Kapalilar"
                        Dim oKargo As New WinTexMNG.HTMain
                        If oKargo.init(cServer, cDatabase, cUsername, cPassword) Then
                            If oKargo.SipPerakendeSonDurum(3, True) Then
                                CreateLog("Perakende Siparis Son Durum Kapalilar tamamlandı : " + cDatabase)
                            Else
                                OUS2CoreExecute = False
                                oKargo = Nothing
                                Exit Function
                            End If
                        End If
                        oKargo = Nothing

                    Case "Perakende Siparis Son Durum Iptaller"
                        Dim oKargo As New WinTexMNG.HTMain
                        If oKargo.init(cServer, cDatabase, cUsername, cPassword) Then
                            If oKargo.SipPerakendeSonDurum(4, True) Then
                                CreateLog("Perakende Siparis Son Durum Iptaller tamamlandı : " + cDatabase)
                            Else
                                OUS2CoreExecute = False
                                oKargo = Nothing
                                Exit Function
                            End If
                        End If
                        oKargo = Nothing

                    Case "On Maliyet3 eMail Tekrar Gonderim"
                        If Not OnMaliyet3MailTekrar(cDatabase) Then
                            Exit Function
                        End If

                    Case "Num.Kum.Satinalma Talep eMail Tekrar Gonderim"
                        If Not NumKumSatMailTekrar(cDatabase) Then
                            Exit Function
                        End If

                    Case "Num.Kum.Rezervasyon Talep eMail Tekrar Gonderim"
                        If Not NumKumRezMailTekrar(cDatabase) Then
                            Exit Function
                        End If

                    Case "Postgre SQL islet"
                        If cParametre1.Trim <> "" And cParametre2.Trim <> "" Then
                            oSQL3 = New PostgreClass(,,,, cParametre1)
                            OUS2CoreExecute = oSQL3.SQLExecuteOpenCloseConnection(cParametre2)
                            oSQL3 = Nothing
                            CreateLog("Postgre SQL islet tamamlandı : " + cParametre1 + " -> " + cParametre2)
                        End If

                    Case "SQL baska veritabani islet"
                        If cParametre1.Trim <> "" And cParametre2.Trim <> "" Then
                            oSQL2 = New SQLServerClass(False,,,,, cParametre1)
                            OUS2CoreExecute = oSQL2.SQLExecuteOpenCloseConnection(cParametre2,, 32767)
                            oSQL2 = Nothing
                            CreateLog("SQL baska veritabani islet tamamlandı : " + cParametre1 + " -> " + cParametre2)
                        End If

                    Case "SQL islet"
                        If cParametre1.Trim <> "" Then
                            oSQL2 = New SQLServerClass(True,, cDatabase)
                            OUS2CoreExecute = oSQL2.SQLExecuteOpenCloseConnection(cParametre1,, 32767)
                            oSQL2 = Nothing
                            CreateLog("SQL islet tamamlandı : " + cDatabase + " -> " + cParametre2)
                        End If

                    Case "MTF Bakimi"
                        oSQL2 = New SQLServerClass(True,, cDatabase)
                        oSQL2.OpenConn()
                        OUS2CoreExecute = oSQL2.CLRExecute("FastMTFBuildAll")
                        oSQL2.CloseConn()
                        oSQL2 = Nothing

                    Case "STF Bakimi"
                        oSQL2 = New SQLServerClass(True,, cDatabase)
                        oSQL2.OpenConn()
                        OUS2CoreExecute = oSQL2.CLRExecute("FastSTFBuildAll", " and (a.dosyakapandi is null or a.dosyakapandi = '' or a.dosyakapandi = 'H') ")
                        oSQL2.CloseConn()
                        oSQL2 = Nothing

                    Case "UTF Bakimi"
                        oSQL2 = New SQLServerClass(True,, cDatabase)
                        oSQL2.OpenConn()
                        OUS2CoreExecute = oSQL2.CLRExecute("FastUTFBuildAll")
                        oSQL2.CloseConn()
                        oSQL2 = Nothing

                    Case "Hizli Stok Bakimi"
                        oSQL2 = New SQLServerClass(True,, cDatabase)
                        oSQL2.OpenConn()
                        OUS2CoreExecute = oSQL2.CLRExecute("HizliStokBakimi")
                        oSQL2.CloseConn()
                        oSQL2 = Nothing

                    Case "Çorap Konveyor"
                        If Not WUAKonteynerAktar(cDatabase) Then
                            Exit Function
                        End If

                    Case "Çorap Stok Bakimi"
                        CorapStokBakimi(cDatabase)

                    Case "Çorap Malzeme İşemirleri Bakımı"
                        CorapMlzIsEmriBakim(cDatabase)

                    Case "Üretim İşemirleri Bakımı"
                        UretimIsemriBakimi("", cDatabase)

                    Case "Merkez Bankasindan Gunluk Kurlarin Cekilmesi"
                        oWinTexDLL = New WinTexDLL.HTMain
                        If oWinTexDLL.init(cServer, cDatabase, cUsername, cPassword) Then
                            If Not oWinTexDLL.GetKurFromMBXML() Then
                                CreateLog("Dikkat GetKurFromMBXML kurlar çekilemedi : " + cServer + " , " + cDatabase + " , " + cUsername + " , " + cPassword)
                                Exit Function
                            End If
                        End If
                        oWinTexDLL = Nothing

                    Case "Merkez Bankasindan Eksik Kurlarin Cekilmesi"
                        oWinTexDLL = New WinTexDLL.HTMain
                        If oWinTexDLL.init(cServer, cDatabase, cUsername, cPassword) Then
                            If Not oWinTexDLL.GetKurlarFromMBXML() Then
                                CreateLog("Dikkat GetKurlarFromMBXML kurlar çekilemedi : " + cServer + " , " + cDatabase + " , " + cUsername + " , " + cPassword)
                                Exit Function
                            End If
                        End If
                        oWinTexDLL = Nothing

                    Case "Logo - Gunluk Kurlarin WinTexe Cekilmesi"
                        If cParametre1.Trim <> "" Then
                            oWinTexDLL = New WinTexDLL.HTMain
                            If oWinTexDLL.init(cServer, cDatabase, cUsername, cPassword) Then
                                If Not oWinTexDLL.GetKurFromLogo(cParametre1.Trim) Then
                                    Exit Function
                                End If
                            End If
                            oWinTexDLL = Nothing
                        End If

                    Case "Uyumsoft - Gunluk Kurlarin WinTexe Cekilmesi"
                        If cParametre1.Trim <> "" Then
                            oWinTexDLL = New WinTexDLL.HTMain
                            If oWinTexDLL.init(cServer, cDatabase, cUsername, cPassword) Then
                                If Not oWinTexDLL.GetKurFromUyum(cParametre1.Trim) Then
                                    Exit Function
                                End If
                            End If
                            oWinTexDLL = Nothing
                        End If

                    Case "Üretim ve Malzeme İşemirleri Performans Hesapları"
                    Case "SQL DB Backup"
                    Case "SQL Rapor"
                    Case "Bütün Bedenlerin Sıralamasını Tazele"
                    Case "Veritabani Hizlandirma"
                    Case "Butun Fotograflari Kontrol Et"
                    Case "Personel Fotograflarini Kontrol Et"
                    Case "Stok Fotograflarini Kontrol Et"
                    Case "Tasarim Fotograflarini Kontrol Et"
                    Case "Tasarim Gorusme Fotograflarini Kontrol Et"
                    Case "Numune Fotograflarini Kontrol Et"
                    Case "Model Fotograflarini Kontrol Et"
                    Case "Modellerin Stok Kartlarini Ac / Guncelle"
                    Case "Stok Kartlarini Otomatik Kapat"
                    Case "Stok Hareket Kurlarini Tamamla"
                    Case "Siparis Renk Degisikligi", "Siparis Adet Degisikligi", "Siparis Beden Seti Degisikligi", "Siparis Notlar Degisikligi", "Siparis Termin Tarihi Degisikligi", "Stok Karti Degisikligi", "Yeni Stok Karti", "Rezervasyon Uyarisi"
                    Case "5.10.3 Koli No Gruplu Stok Hareket Fişleri Listesi"
                    Case "Malzeme Isemrileri Excel"
                    Case "UGM ABIT Bilgi Aktarimi"
                    Case "Siparislerin Onmaliyet1 Hesaplamasi"

                    'Case "Isemirlerini YYK ya Transfer Et"
                    '    If Not YKKAktarOUS(cParametre1) Then
                    '        Exit Function
                    '    End If
                    'Case "YKK ya Transfer Edilmis Isemirlerini Sorgula"
                    '    If Not YKKSorgulaOUS(cParametre1) Then
                    '        Exit Function
                    '    End If

                    Case "Uyumsoft - Stok Kartlarini Aktar",
                         "Uyumsoft - Stok Hareketlerini Aktar",
                         "Uyumsoft - Model Recetelerini Aktar",
                         "Uyumsoft - Alinan Ihracat Siparislerini Aktar",
                         "Uyumsoft - Uretim Emirlerini Aktar",
                         "Uyumsoft - Verilen (Satinalma) Siparisler Aktar",
                         "Uyumsoft - Uretim (Hareket Fisleri) Aktar"

                        If cDatabase <> "TESDENEME" Then
                            If Not UyumEntegrasyon(cOUS, cParametre1, cServer, cDatabase, cUsername, cPassword) Then
                                Exit Function
                            End If
                        End If

                End Select
            End If

            OUS2CoreExecute = True

        Catch ex As Exception
            ErrDisp(ex, "OUS2CoreExecute")
        End Try
    End Function

    Public Sub CreateLog(cMessage As String, Optional nCase As Integer = 1)
        Try
            If cMessage.Trim = "" Then Exit Sub

            cMessage = cMessage + vbCrLf +
                        "Servis Versiyonu : " + cVersion + vbCrLf +
                        "İşlem Parçacağı Adedi : " + Process.GetCurrentProcess.Threads.Count.ToString

            Select Case nCase
                Case 1
                    EventLog1.WriteEntry(cMessage, EventLogEntryType.Information)
                Case 2
                    EventLog1.WriteEntry(cMessage, EventLogEntryType.Error)
            End Select

        Catch ex As Exception
            ' do nothing
        End Try
    End Sub

    Private Sub SendVersion()

        Try
            Dim oSQL As New SQLServerClass

            oSQL.OpenConn()

            oSQL.SetSysPar("wintexserviceversion", cVersion)

            oSQL.CloseConn()

        Catch ex As Exception
            ' do nothing 
        End Try
    End Sub

End Class
