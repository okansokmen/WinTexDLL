Option Explicit On
Option Strict On

Imports System.Data.SqlClient

Module UtilSiparis

    Public Function SipPerakendeSonDurum(cServer As String, cDatabase As String, cUsername As String, cPassword As String, Optional nCase As Integer = 1) As Boolean

        SipPerakendeSonDurum = False

        Try
            Dim tBegin As DateTime = DateTime.Parse("9:00:00 AM")
            Dim tEnd As DateTime = DateTime.Parse("8:00:00 PM")

            If tBegin.TimeOfDay < DateTime.Now.TimeOfDay And tEnd.TimeOfDay > DateTime.Now.TimeOfDay Then
                ' ok execute
            Else
                CreateLogFile("SipPerakendeSonDurum No Execute")
                Exit Function
            End If

            Dim oSQL As New SQLServerClass(True,, cDatabase)
            Dim oWinTexMNG As New WinTexMNG.HTMain
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
            Dim nCntSiparis As Integer = -1
            Dim lOK As Boolean = False

            If Not oWinTexMNG.init(cServer, cDatabase, cUsername, cPassword) Then
                oSQL = Nothing
                oWinTexMNG = Nothing
                CreateLogFile("SipPerakendeSonDurum veritabanına bağlanamadı : " + cServer + " , " + cDatabase + " , " + cUsername + " , " + cPassword)
                Exit Function
            End If

            oSQL.OpenConn()

            oSQL.cSQLQuery = "update sipperakende " +
                            " set kargostatu = 'SIPARIS ALINDI' " +
                            " where (yazdirildi is null or yazdirildi = 'H' or yazdirildi = '') " +
                            " and (kapandi = 'H' or kapandi is null or kapandi = '') " +
                            " and (iptal = 'H' or iptal is null or iptal = '') " +
                            " and (iade = 'H' or iade is null or iade = '') "

            oSQL.SQLExecute()

            oSQL.cSQLQuery = "update sipperakende " +
                            " set kargostatu = 'SIPARIS HAZIRLANIYOR' " +
                            " where yazdirildi = 'E' " +
                            " and (kapandi = 'H' or kapandi is null or kapandi = '') " +
                            " and (iptal = 'H' or iptal is null or iptal = '') " +
                            " and (iade = 'H' or iade is null or iade = '') " +
                            " and (kargostatu is null or kargostatu = '') "

            oSQL.SQLExecute()

            ' ByExpress ve PTT

            Select Case nCase
                Case 1
                    ' kapanmamış, iptal olmamış, iade olmamışlar 
                    oSQL.cSQLQuery = "Select siparisno, kargofirmasi, kargosondurumkodu, kargotakipno " +
                            " from sipperakende with (NOLOCK) " +
                            " where (kapandi = 'H' or kapandi is null or kapandi = '') " +
                            " and (iptal = 'H' or iptal is null or iptal = '') " +
                            " and (iade = 'H' or iade is null or iade = '') " +
                            " and kargoyakayityollandi = 'E' " +
                            " and siparisno is not null " +
                            " and siparisno <> '' " +
                            " and kargofirmasi in ('BYEXPRESS','PTT') " +
                            " order by kargostatutarihi, siparisno "
                Case = 2
                    ' iade siparisler
                    oSQL.cSQLQuery = "Select siparisno, kargofirmasi, kargosondurumkodu, kargotakipno " +
                            " from sipperakende with (NOLOCK) " +
                            " where iade = 'E' " +
                            " and kargoyakayityollandi = 'E' " +
                            " and kargoyakayityollanmatarihi > dateadd(day,-62,getdate()) " +
                            " and siparisno is not null " +
                            " and siparisno <> '' " +
                            " and kargofirmasi in ('BYEXPRESS','PTT') " +
                            " order by kargostatutarihi, siparisno "
                Case 3
                    ' kapali siparisler
                    oSQL.cSQLQuery = "Select siparisno, kargofirmasi, kargosondurumkodu, kargotakipno " +
                            " from sipperakende with (NOLOCK) " +
                            " where kapandi = 'E' " +
                            " and kargoyakayityollandi = 'E' " +
                            " and kargoyakayityollanmatarihi > dateadd(day,-62,getdate()) " +
                            " and siparisno is not null " +
                            " and siparisno <> '' " +
                            " and kargofirmasi in ('BYEXPRESS','PTT') " +
                            " order by kargostatutarihi, siparisno "
            End Select

            oSQL.GetSQLReader()

            Do While oSQL.oReader.Read
                Select Case oSQL.SQLReadString("kargofirmasi").ToLower
                    Case "byexpress"
                        If oWinTexMNG.ByExpressQueryOrder(oSQL.SQLReadString("siparisno"), oSQL.SQLReadString("kargotakipno"), cSonuc, cErrorMessage) Then
                            cMessage = Replace$(cSonuc, ";;", vbCrLf)
                            CreateLogFile("Siparis ByExpress kargoda sorgulandı : " + oSQL.SQLReadString("siparisno") + vbCrLf +
                                          "Sonuç : " + cSonuc + vbCrLf +
                                          "Hata Mesajı : " + cErrorMessage + vbCrLf)
                        Else
                            CreateLogFile("Dikkat siparis ByExpress kargoda bulunamadi : " + oSQL.SQLReadString("siparisno") + vbCrLf +
                                          "Hata Mesajı : " + cErrorMessage + vbCrLf)
                        End If
                    Case "ptt"
                        If oWinTexMNG.PTTQueryOrder(oSQL.SQLReadString("siparisno"), cSonuc, cErrorMessage) Then
                            CreateLogFile("Siparis PTT kargoda sorgulandı : " + oSQL.SQLReadString("siparisno") + vbCrLf +
                                          "Sonuç : " + cSonuc + vbCrLf +
                                          "Hata Mesajı : " + cErrorMessage + vbCrLf)
                        Else
                            CreateLogFile("Dikkat siparis PTT kargoda bulunamadi : " + oSQL.SQLReadString("siparisno") + vbCrLf +
                                          "Hata Mesajı : " + cErrorMessage + vbCrLf)
                        End If
                End Select
            Loop
            oSQL.oReader.Close()

            ' MNG 
            Select Case nCase
                Case 1
                    ' kapanmamış, iptal olmamış, iade olmamışlar 
                    oSQL.cSQLQuery = "Select siparisno, kargofirmasi, kargosondurumkodu, kargotakipno " +
                            " from sipperakende with (NOLOCK) " +
                            " where (kapandi = 'H' or kapandi is null or kapandi = '') " +
                            " and (iptal = 'H' or iptal is null or iptal = '') " +
                            " and (iade = 'H' or iade is null or iade = '') " +
                            " and kargoyakayityollandi = 'E' " +
                            " and siparisno is not null " +
                            " and siparisno <> '' " +
                            " and kargofirmasi = 'MNG' " +
                            " order by kargostatutarihi, siparisno "
                Case = 2
                    ' iade siparisler
                    oSQL.cSQLQuery = "Select siparisno, kargofirmasi, kargosondurumkodu, kargotakipno " +
                            " from sipperakende with (NOLOCK) " +
                            " where iade = 'E' " +
                            " and kargoyakayityollandi = 'E' " +
                            " and kargoyakayityollanmatarihi > dateadd(day,-62,getdate()) " +
                            " and siparisno is not null " +
                            " and siparisno <> '' " +
                            " and kargofirmasi = 'MNG' " +
                            " order by kargostatutarihi, siparisno "
                Case 3
                    ' kapali siparisler
                    oSQL.cSQLQuery = "Select siparisno, kargofirmasi, kargosondurumkodu, kargotakipno " +
                            " from sipperakende with (NOLOCK) " +
                            " where kapandi = 'E' " +
                            " and kargoyakayityollandi = 'E' " +
                            " and kargoyakayityollanmatarihi > dateadd(day,-62,getdate()) " +
                            " and siparisno is not null " +
                            " and siparisno <> '' " +
                            " and kargofirmasi = 'MNG' " +
                            " order by kargostatutarihi, siparisno "
            End Select

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

                    If oWinTexMNG.MNGQueryOrder(oSQL.SQLReadString("siparisno"), cSonuc, cErrorMessage) Then
                        cMessage = Replace$(cSonuc, ";;", vbCrLf)
                        CreateLogFile("Siparis MNG kargoda sorgulandı : " + oSQL.SQLReadString("siparisno") + vbCrLf +
                                          "Sonuç : " + cSonuc + vbCrLf +
                                          "Hata Mesajı : " + cErrorMessage + vbCrLf)
                    Else
                        CreateLogFile("Dikkat siparis MNG kargoda bulunamadi : " + oSQL.SQLReadString("siparisno") + vbCrLf +
                                          "Hata Mesajı : " + cErrorMessage + vbCrLf)
                    End If
                Next
            End If

            ' iptal siparişlerin statusunu güncelle
            oSQL.cSQLQuery = "update sipperakende " +
                            " set kargostatu = 'İPTAL EDİLDİ' " +
                            " where iptal = 'E' "
            oSQL.SQLExecute()

            oSQL.cSQLQuery = "update sipperakende " +
                            " set kargostatu = 'YOLDA' " +
                            " where (kapandi = 'H' or kapandi is null or kapandi = '') " +
                            " and (iptal = 'H' or iptal is null or iptal = '') " +
                            " and (iade = 'H' or iade is null or iade = '') " +
                            " and kargostatu not in ('SIPARIS ALINDI','SIPARIS HAZIRLANIYOR','CIKIS SUBEDE','YOLDA','VARIS SUBEDE','DAGITIMDA','TESLIM EDILDI','SORUNLU','IADE') "
            oSQL.SQLExecute()

            oSQL.CloseConn()

            oSQL = Nothing
            oWinTexMNG = Nothing

            SipPerakendeSonDurum = True

        Catch ex As Exception
            ErrDisp(ex, "SipPerakendeSonDurum")
        End Try
    End Function

End Module
