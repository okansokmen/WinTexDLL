Option Explicit On
Option Strict On

Imports System.Configuration
Imports System.Timers
Imports System.Diagnostics
Imports System.Data.SqlClient

Public Class YKKService

    Public oTimer1 As System.Timers.Timer
    Public oTimer2 As System.Timers.Timer

    Dim lOUS2PopulateQueue As Boolean = False
    Dim lOUS2Execute As Boolean = False

    Dim cOUSList As String = "'Isemirlerini YYK ya Transfer Et'," +
                             "'YKK ya Transfer Edilmis Isemirlerini Sorgula'"

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

        If Not System.Diagnostics.EventLog.SourceExists("WintexYKKService") Then
            System.Diagnostics.EventLog.CreateEventSource("WintexYKKService", "WintexYKKServiceLog")
        End If

        EventLog1.Source = "WintexYKKService"
        EventLog1.Log = "WintexYKKServiceLog"

        lOUS2PopulateQueue = False
        lOUS2Execute = False
        oYKKService = Me
    End Sub

    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Add code here to start your service. This method should set things
        ' in motion so your service can do its work.
        Try
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
            If lOUS2Execute Then Exit Sub
            lOUS2Execute = True
            OUS2Execute()
            lOUS2Execute = False

        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Sub

    Private Sub OUS2PopulateQueue()

        Dim oSQL As New SQLServerClass
        Dim cSQL As String = ""
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

            If Not oSQL.OpenConn() Then Exit Sub

            ' eklenmiş , 6 saat geçmiş , bitmemiş görevleri sil
            cSQL = "delete ous2queue " +
                    " where DateDiff(Hour, createdate, GETDATE()) > 1 " +
                    " and bitis Is NULL "

            oSQL.SQLExecute(cSQL)

            cSQL = "select sirano, ousno, tekrar, gun, ay, yil, saat, dakika " +
                " from ous2 with (NOLOCK) "

            cSQL = cSQL +
                " where baslangic <= getdate() " +
                " and bitis >= getdate() " +
                " and ousno is not null " +
                " and ousno <> '' " +
                " and tekrar is not null " +
                " and tekrar <> '' " +
                " and tekrar <> 'BELIRSIZ' " +
                " and serviscalistirsin is not null " +
                " and serviscalistirsin = 'E' " +
                " and ous in (" + cOUSList + ") " +
                " and not exists (select sirano from ous2queue with (NOLOCK) where ousno = ous2.ousno and bitis is null) " +
                " order by ousno "

            oSQL.GetSQLReader(cSQL)

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

                cDakika2 = oSQL.SQLReadString("Dakika")
                If IsNumeric(cDakika2) Then
                    nDakika2 = CDbl(cDakika2)
                Else
                    nDakika2 = 0
                End If

                Select Case cTekrar
                    Case "SAATLIK"
                        If nDakika = nDakika2 Then
                            OUS2AddToQueue(cOUSNo)
                        End If
                    Case "GUNLUK"
                        If nSaat = nSaat2 And nDakika = nDakika2 Then
                            OUS2AddToQueue(cOUSNo)
                        End If
                    Case "HAFTALIK"
                        If cGun = cGun2 And nSaat = nSaat2 And nDakika = nDakika2 Then
                            OUS2AddToQueue(cOUSNo)
                        End If
                    Case "YILLIK"
                        If cAy = cAy2 And cGun = cGun2 And nSaat = nSaat2 And nDakika = nDakika2 Then
                            OUS2AddToQueue(cOUSNo)
                        End If
                    Case "BIRDEFA"
                        If nYil = nYil2 And cAy = cAy2 And cGun = cGun2 And nSaat = nSaat2 And nDakika = nDakika2 Then
                            OUS2AddToQueue(cOUSNo)
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

    Private Sub OUS2AddToQueue(cOUSNo As String)
        Try
            Dim cSQL As String = ""
            Dim oSQL As SQLServerClass

            If cOUSNo.Trim = "" Then Exit Sub

            oSQL = New SQLServerClass
            cSQL = "insert ous2queue (ousno, createdate, ousversion) values ('" + SQLWriteString(cOUSNo, 10) + "',getdate(),'" + cVersion + "') "
            oSQL.SQLExecuteOpenCloseConnection(cSQL)
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
            Dim oSQL As New SQLServerClass
            Dim cSQL As String = ""
            Dim cOUSNo As String = ""
            Dim cOUS As String = ""
            Dim nSiraNo As Double = 0
            Dim cAciklama As String = ""

            If Not oSQL.OpenConn() Then Exit Sub

            ' baslamamis ilk isi ele alir
            cSQL = "select top 1 a.sirano, a.ousno, b.ous, b.aciklama " +
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

            oSQL.GetSQLReader(cSQL)

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

            CreateLog("Görev Başlatıldı : " + cOUS + " / " + cAciklama)

            cSQL = "update ous2queue " +
                    " set baslangic = getdate() " +
                    " where sirano = " + CStr(nSiraNo)

            oSQL.SQLExecute(cSQL)

            If OUS2CoreExecute(cOUSNo) Then
                CreateLog("Görev başarıldı : " + cOUS + " / " + cAciklama)

                cSQL = "update ous2queue " +
                        " set sonuc = 'success', bitis = getdate() " +
                        " where sirano = " + CStr(nSiraNo)

                oSQL.SQLExecute(cSQL)
            Else
                CreateLog("Görev BAŞARISIZ : " + cOUS + " / " + cAciklama)

                cSQL = "update ous2queue " +
                        " set sonuc = 'fail', bitis = getdate() " +
                        " where sirano = " + CStr(nSiraNo)

                oSQL.SQLExecute(cSQL)
            End If

            oSQL.CloseConn()
            oSQL = Nothing

        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Sub

    Public Function OUS2CoreExecute(cOUSNo As String) As Boolean

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
        Dim oSQL As New SQLServerClass

        OUS2CoreExecute = False

        Try
            If cOUSNo.Trim = "" Then Exit Function

            If Not oSQL.OpenConn() Then Exit Function

            cSQL = "select top 1 ous, reportid, " +
                    " parametre1, parametre2, parametre3, parametre4, parametre5, " +
                    " parametre6, parametre7, parametre8, parametre9, parametre10, " +
                    " sqlcheck " +
                    " from ous2 with (NOLOCK)  " +
                    " where ousno = '" + cOUSNo.Trim + "' "

            oSQL.GetSQLReader(cSQL)

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
                cSQL = Trim$(cSQLCheck)
                lOK = oSQL.CheckExists(cSQL)
            End If

            oSQL.CloseConn()
            oSQL = Nothing

            If lOK Then
                Select Case cOUS
                    Case "Isemirlerini YYK ya Transfer Et"
                        If Not YKKAktarOUS(cParametre1) Then
                            Exit Function
                        End If
                    Case "YKK ya Transfer Edilmis Isemirlerini Sorgula"
                        If Not YKKSorgulaOUS(cParametre1) Then
                            Exit Function
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
                        "YKK Servis Versiyonu : " + cVersion + vbCrLf +
                        "YKK İşlem Parçacağı Adedi : " + Process.GetCurrentProcess.Threads.Count.ToString

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

End Class
