Option Explicit On
Option Strict On

Imports System.Xml
Imports System.Configuration
Imports System.Net
Imports System
Imports System.IO
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.SqlServer.Server
Imports Microsoft.VisualBasic

Module utilOtomatikRezerveSatinalma

    ' otorezervasyontalebi        -- Otomatik Rezervasyon Talebi kılavuzu view
    ' otorezervasyontalebikitdisi -- Otomatik Rezervasyon Talebi kılavuzu view
    ' mtkrezervasyononayistek     -- Malzeme Takip Rezervasyon Onay İstek tablosu , onay bekleyen malzeme rezervasyonları 
    ' mtkrezervasyononayemail     -- Malzeme Takip Rezervasyon Onay Email tablosu , onay bekleyen malzeme rezervasyonları için mail atılacak kullanıcılar

    ' otosatinalmaisemri          -- Otomatik Satınalma İsemri kılavuzu view
    ' otosatinalmaisemrikitdisi   -- Otomatik Satınalma İsemri kılavuzu view

    Dim nMode As Integer = 1

    Private Structure oMtfLines
        Dim cMtf As String
        Dim cUDept As String
        Dim cTDept As String
        Dim cStokNo As String
        Dim cRenk As String
        Dim cBeden As String
    End Structure

    Private Structure oIsemriLines
        Dim cMtf As String
        Dim cDepartman As String
        Dim cStokNo As String
        Dim cRenk As String
        Dim cBeden As String
        Dim nMiktar As Double
        Dim cBirim As String
        Dim nFiyat As Double
        Dim cDoviz As String
        Dim cDurum As String
        Dim cDepo As String
    End Structure

    Private Structure oDeptFirmaMtf
        Dim cDepartman As String
        Dim cFirma As String
        Dim cMtf As String
    End Structure

    Private Structure oM1
        Dim cMusteriNo As String
        Dim cStokTipi As String
        Dim cMTF As String
        Dim cDepartman As String
        Dim cTeminDept As String
        Dim cStokNo As String
        Dim cCinsAciklamasi As String
        Dim cRenk As String
        Dim cBeden As String
        Dim nIhtiyac As Double
        Dim nOrjinalIhtiyac As Double
        Dim cDepo As String
        Dim nSerbest As Double
    End Structure

    Private Structure oMusteriStokTipi
        Dim cMusteriNo As String
        Dim cStokTipi As String
    End Structure

    Public Function OtomatikRezerveSatinalma(cDatabase As String, Optional nMode1 As Integer = 1) As Boolean

        ' yapılan rezervasyon istekleri , açılan işemirleri için onay maili gönder
        ' nMode1 = 1  kit içi
        ' nMode1 = 2  kit dışı

        nMode = nMode1
        OtomatikRezerveSatinalma = False

        Try
            Dim cMailTo As String = ""
            Dim oSQL As New SQLServerClass(True,, cDatabase)
            Dim oSQL2 As New SQLServerClass(True,, cDatabase)
            Dim nMaxCols As Integer = 11
            Dim nRow As Integer = 0
            Dim cSubject As String = "Otomatik Rezervasyon Onay Talebi"
            Dim cBody As String = ""
            Dim cRequestID As String = ""
            Dim cAttachments As String = ""
            Dim aAttachments() As String
            Dim cFileName As String = ""
            Dim nReportID As Double = 0
            Dim cIsemriNo As String = ""
            Dim lReportOK As Boolean = False
            Dim cerrormessage As String = ""
            Dim cIsemirleri As String = ""
            Dim cControlMail As String = ""

            ' önce hesaplamaları yap
            ' rezervasyon isteklerini ve satınalma işemirlerini oluştur
            OtoRezisemri1(cDatabase)

            oSQL.OpenConn()
            oSQL2.OpenConn()

            cControlMail = oSQL.GetSysPar("emailcontrol")

#Region "rezervasyon eMail"

            oSQL.cSQLQuery = "select distinct a.email " +
                            " from mtkrezervasyononayemail a with (NOLOCK) , mtkrezervasyononayistek b with (NOLOCK) " +
                            " where a.requestid = b.requestid " +
                            " and a.email is not null " +
                            " and a.email <> '' " +
                            " and a.senddate is null " +
                            " and (b.onay is null or b.onay = '') " +
                            " and (b.kapandi is null or b.kapandi = '') " +
                            " order by a.email "

            oSQL.GetSQLReader()

            Do While oSQL.oReader.Read

                cMailTo = oSQL.SQLReadString("email")
                cRequestID = ""

                nRow = 0
                ReDim aHTMLRow(nMaxCols, 0)
                aHTMLRow(0, 0).cValue = "Musteri"
                aHTMLRow(1, 0).cValue = "MTF"
                aHTMLRow(2, 0).cValue = "Stok Tipi"
                aHTMLRow(3, 0).cValue = "Stok No"
                aHTMLRow(4, 0).cValue = "Cins Açıklaması"
                aHTMLRow(5, 0).cValue = "Renk"
                aHTMLRow(6, 0).cValue = "Beden"
                aHTMLRow(7, 0).cValue = "Ihtiyaç"
                aHTMLRow(8, 0).cValue = "Birim"
                aHTMLRow(9, 0).cValue = "Depo"
                aHTMLRow(10, 0).cValue = "Rezerve"
                aHTMLRow(11, 0).cValue = "Departman"

                oSQL2.cSQLQuery = "select b.requestid, b.musterino, b.stoktipi, b.malzemetakipno, b.stokno, " +
                                 " b.cinsaciklamasi, b.renk, b.beden, b.ihtiyac, b.depo, " +
                                 " b.rezerve, b.taleptarihi, b.departman, c.birim1 " +
                                 " from mtkrezervasyononayemail a with (NOLOCK) , mtkrezervasyononayistek b with (NOLOCK) , stok c with (NOLOCK) " +
                                 " where a.requestid = b.requestid " +
                                 " and b.stokno = c.stokno " +
                                 " and a.email = '" + cMailTo + "' " +
                                 " and a.senddate is null " +
                                 " and (b.onay is null or b.onay = '') " +
                                 " and (b.kapandi is null or b.kapandi = '') " +
                                 " order by b.musterino, b.malzemetakipno, b.stoktipi, b.stokno, b.renk, b.beden "

                oSQL2.GetSQLReader()

                Do While oSQL2.oReader.Read
                    nRow = nRow + 1
                    ReDim Preserve aHTMLRow(nMaxCols, nRow)
                    aHTMLRow(0, nRow).cValue = oSQL2.SQLReadString("musterino")
                    aHTMLRow(1, nRow).cValue = oSQL2.SQLReadString("malzemetakipno")
                    aHTMLRow(2, nRow).cValue = oSQL2.SQLReadString("stoktipi")
                    aHTMLRow(3, nRow).cValue = oSQL2.SQLReadString("stokno")
                    aHTMLRow(4, nRow).cValue = oSQL2.SQLReadString("cinsaciklamasi")
                    aHTMLRow(5, nRow).cValue = oSQL2.SQLReadString("renk")
                    aHTMLRow(6, nRow).cValue = oSQL2.SQLReadString("beden")
                    aHTMLRow(7, nRow).cValue = oSQL2.SQLReadDouble("ihtiyac").ToString(G_Number3Format)
                    aHTMLRow(8, nRow).cValue = oSQL2.SQLReadString("birim1")
                    aHTMLRow(9, nRow).cValue = oSQL2.SQLReadString("depo")
                    aHTMLRow(10, nRow).cValue = oSQL2.SQLReadDouble("rezerve").ToString(G_Number3Format)
                    aHTMLRow(11, nRow).cValue = oSQL2.SQLReadString("departman")

                    If cRequestID = "" Then
                        cRequestID = "'" + oSQL2.SQLReadString("requestid") + "'"
                    Else
                        cRequestID = cRequestID + ", '" + oSQL2.SQLReadString("requestid") + "'"
                    End If
                Loop
                oSQL2.oReader.Close()

                If nRow > 0 Then
                    cBody = GetHTMLTable(nRow, nMaxCols, "Otomatik Rezervasyon Onay Talebi / v:" + cVersion)
                    If cControlMail.Trim <> "" Then
                        cMailTo = cMailTo + "," + cControlMail
                    End If

                    If SendGoogleMail(cMailTo, cSubject, cBody, , cDatabase) Then

                        oSQL2.cSQLQuery = "update mtkrezervasyononayemail " +
                                        " Set senddate = getdate() " +
                                        " where email = '" + cMailTo + "' " +
                                        " and senddate is null " +
                                        " and requestid in (" + cRequestID + ") "

                        oSQL2.SQLExecute()

                        ActivityLog(cDatabase, "Mesaj", "OMRS eMail Gonderildi", cSubject, cMailTo + " " + cBody.Trim)
                    Else
                        ActivityLog(cDatabase, "Mesaj", "OMRS eMail Gonderilemedi", cSubject, cMailTo + " " + cBody.Trim)
                    End If
                End If
            Loop
            oSQL.oReader.Close()
#End Region

#Region "işemri eMail"

            ' işemirleri için  eMail gönder

            If (My.Computer.FileSystem.DirectoryExists("C:\wintex\Temp") = False) Then
                My.Computer.FileSystem.CreateDirectory("C:\wintex\Temp")
            End If

            oSQL2.cSQLQuery = "SELECT DISTINCT a.isemrino , a.emailsatinalmaci , a.emailsatici , c.reportid " +
                            " FROM isemri a WITH (NOLOCK) , departman b WITH (NOLOCK) , stireports c WITH (NOLOCK) " +
                            " WHERE a.departman = b.departman  " +
                            " AND b.ReportName = c.reportname " +
                            " AND a.createuser = 'CLR' " +
                            " AND (a.emailgonderildi IS NULL or a.emailgonderildi = 'H' OR a.emailgonderildi = '') " +
                            " and a.emailsatinalmaci is not null " +
                            " and a.emailsatinalmaci <> '' " +
                            " and a.emailsatici is not null " +
                            " and a.emailsatici <> '' " +
                            " ORDER BY a.isemrino "

            oSQL2.GetSQLReader()

            Do While oSQL2.oReader.Read

                cIsemriNo = oSQL2.SQLReadString("isemrino")
                nReportID = CDbl(oSQL2.SQLReadInteger("reportid"))

                cMailTo = oSQL2.SQLReadString("emailsatici") + "," + oSQL2.SQLReadString("emailsatinalmaci")

                If cControlMail.Trim <> "" Then
                    cMailTo = cMailTo + "," + cControlMail
                End If

                cSubject = "Eroğlu satınalma siparişi : " + cIsemriNo
                cBody = "Eroğlu satınalma siparişi : " + cIsemriNo

                cFileName = "C:\wintex\Temp\" + "ErogluSatinalma-" + cIsemriNo + ".xls"

                If My.Computer.FileSystem.FileExists(cFileName) Then
                    My.Computer.FileSystem.DeleteFile(cFileName)
                End If

                If StiReportToExcel(nReportID.ToString, cFileName, cIsemriNo,,,,,,,,,, cDatabase) Then

                    If My.Computer.FileSystem.FileExists(cFileName) Then

                        If SendGoogleMail(cMailTo, cSubject, cBody, cFileName, cDatabase) Then

                            oSQL.cSQLQuery = "update isemri " +
                                            " SET emailgonderildi = 'E' " +
                                            " WHERE isemrino = '" + cIsemriNo + "' "
                            oSQL.SQLExecute()

                            ActivityLog(cDatabase, "Mesaj", "OMSS1 eMail Gonderildi", cSubject, cMailTo + " " + cBody.Trim)
                        Else

                            oSQL.cSQLQuery = "update isemri " +
                                            " SET emailgonderildi = 'H' " +
                                            " WHERE isemrino = '" + cIsemriNo + "' "
                            oSQL.SQLExecute()

                            ActivityLog(cDatabase, "Mesaj", "OMSS1 eMail Gonderilemedi", cSubject, cMailTo + " " + cBody.Trim)
                        End If

                        My.Computer.FileSystem.DeleteFile(cFileName)
                    End If
                End If
            Loop
            oSQL2.oReader.Close()

#End Region

            oSQL.CloseConn()
            oSQL2.CloseConn()

            oSQL = Nothing
            oSQL2 = Nothing

            OtomatikRezerveSatinalma = True

        Catch ex As Exception
            ErrDisp(ex, "OtomatikRezerveSatinalma")
        End Try
    End Function

    Public Sub OtoRezisemri1(cDatabase As String)

        ' otorezervasyontalebi        -- Kit içi malzemeler için otomatik rezervasyon talebi kılavuzu view
        ' otorezervasyontalebikitdisi -- Kit dışı malzemeler için otomatik rezervasyon talebi kılavuzu view
        ' otosatinalmaisemri          -- Kit içi malzemeler için otomatik satınalma işemri kılavuzu view
        ' otosatinalmaisemrikitdisi   -- Kit dışı malzemeler için otomatik satınalma işemri kılavuzu view
        ' mtkrezervasyononayistek     -- Malzeme Takip Rezervasyon Onay İstek tablosu , onay bekleyen malzeme rezervasyonları 
        ' mtkrezervasyononayemail     -- Malzeme Takip Rezervasyon Onay Email tablosu , onay bekleyen malzeme rezervasyonları için mail atılacak kullanıcılar

        Dim cSQL As String = ""

        Try
            Dim aMusteriStokTipi() As oMusteriStokTipi
            Dim aDeptFirmaMtf() As oDeptFirmaMtf
            Dim aIsemriLines() As oIsemriLines
            Dim aMtfLines() As oMtfLines
            Dim aM1() As oM1

            Dim oSQL As New SQLServerClass(True,, cDatabase)
            Dim oSQL2 As New SQLServerClass(True,, cDatabase)
            Dim oSQL3 As New SQLServerClass(True,, cDatabase)

            Dim cTable As String = ""
            Dim cTable3 As String = ""
            Dim nIhtiyac As Double = 0
            Dim nSerbest As Double = 0
            Dim nMiktar As Double
            Dim cDepo As String = ""
            Dim cStokNo As String = ""
            Dim cRenk As String = ""
            Dim cBeden As String = ""
            Dim cMusteriNo As String = ""
            Dim cStokTipi As String = ""
            Dim cMTF As String = ""
            Dim cCinsAciklamasi As String = ""
            Dim cMailTo As String = ""
            Dim nMaxCols As Integer = 0
            Dim nRow As Integer = 0
            Dim cMessage As String = ""
            Dim cSubject As String = ""
            Dim cRequestId As String = ""
            Dim cReqIdList As String = ""
            Dim nCnt As Integer = 0
            Dim nCnt1 As Integer = 0
            Dim aEMail() As String
            Dim nFiyat As Double = 0
            Dim cDoviz As String = ""
            Dim cIsemriNo As String = ""
            Dim cFilter As String = ""
            Dim cTakipElemani As String = ""
            Dim cIsemirleri As String = ""
            Dim cFilter2 As String = ""
            Dim cSQLIstekHeader As String = ""
            Dim cSQLIstekBody As String = ""
            Dim nCntIstek As Integer = 0

            Dim cSQLMailHeader As String = ""
            Dim cSQLMailBody As String = ""
            Dim nCntMail As Integer = 0

            Dim cSQLIsemriUpdate As String = ""
            Dim cSQLIsemriHeader As String = ""
            Dim cSQLIELHeader As String = ""
            Dim cSQLIELBody As String = ""
            Dim nCntIEL As Integer = 0

            Dim cSQLMtf As String = ""
            Dim nCntMtf As Integer = 0

            Dim nCntIsemri1 As Integer = 0
            Dim nCntIsemri2 As Integer = 0

            ' Batch insert configuration

            Const BATCH_SIZE As Integer = 500

            ' Batch collections for isemri

            Dim batchIsemriHeader As New System.Text.StringBuilder()
            Dim batchIsemriLines As New System.Text.StringBuilder()
            Dim nBatchIsemriCount As Integer = 0
            Dim nBatchIsemriLinesCount As Integer = 0

            ' kit içi / dışı ayarı

            Dim cOtoSatinalmaIsemriView As String = "otosatinalmaisemri"
            Dim cOtoRezervasyonTalebiView As String = "otorezervasyontalebi"

            oSQL.OpenConn()
            oSQL2.OpenConn()
            oSQL3.OpenConn()

            ' 1 rezervasyonları yap
            ' 2 kit içi satınalma işemirlerini oluştur
            ' 3 kit dışı satınalma işemirlerini oluştur 

            If nMode = 1 Then
                ' kit içi
                cOtoSatinalmaIsemriView = "otosatinalmaisemri"
                cOtoRezervasyonTalebiView = "otorezervasyontalebi"
            Else
                ' kit dışı
                cOtoSatinalmaIsemriView = "otosatinalmaisemrikitdisi"
                cOtoRezervasyonTalebiView = "otorezervasyontalebikitdisi"
            End If

#Region "Rezervasyon"

            ' MTF recalc
            If nMode = 1 Then
                ' kit içi  
                oSQL.cSQLQuery = "SELECT DISTINCT malzemetakipno " +
                                " FROM otorezervasyontalebi " +
                                " UNION " +
                                " SELECT DISTINCT malzemetakipno " +
                                " FROM otosatinalmaisemri "
            Else
                ' kit dışı  
                oSQL.cSQLQuery = "SELECT DISTINCT malzemetakipno " +
                                " FROM otorezervasyontalebikitdisi " +
                                " UNION " +
                                " SELECT DISTINCT malzemetakipno " +
                                " FROM otosatinalmaisemrikitdisi "
            End If

            oSQL.GetSQLReader()

            While oSQL.oReader.Read
                cMTF = oSQL.SQLReadString("malzemetakipno")
                If cMTF.Trim <> "" Then
                    oSQL2.cSQLQuery = "exec FastMTFBuild '" + cMTF + "' , 1 "
                    oSQL2.SQLExecute()
                End If
            End While
            oSQL.oReader.Close()

            ' rezervasyonlar tablosu 

            oSQL.cSQLQuery = " (malzemetakipno char(30) null, " +
                            " musterino CHAR(30) null, " +
                            " stoktipi CHAR(30) null, " +
                            " stokno CHAR(30) null, " +
                            " cinsaciklamasi CHAR(250) null, " +
                            " renk CHAR(30) null, " +
                            " beden CHAR(30) null, " +
                            " temindept CHAR(30) null, " +
                            " departman CHAR(30) null, " +
                            " birim1 CHAR(30) null, " +
                            " depo CHAR(30) null, " +
                            " serbestmiktar decimal(18,6) null, " +
                            " ihtiyac decimal(18,6) null, " +
                            " ilksevktar datetime null) "

            cTable3 = oSQL.CreateTempTable()

            oSQL.cSQLQuery = "insert into " + cTable3 + " (malzemetakipno, musterino, stoktipi, stokno, cinsaciklamasi, renk, beden, temindept, depo, serbestmiktar, " +
                            " departman, birim1, ilksevktar, ihtiyac ) " +
                            " select malzemetakipno, musterino, stoktipi, stokno, cinsaciklamasi, renk, beden, temindept, depo, serbestmiktar, " +
                            " departman, birim1, ilksevktar, ihtiyac = coalesce(ihtiyac,0) - coalesce(isemribeklenen,0) - coalesce(isemriharicigelen,0) " +
                            " from " + cOtoRezervasyonTalebiView +
                            " where serbestmiktar Is Not null " +
                            " And serbestmiktar > 0 " +
                            " And ihtiyac Is Not null " +
                            " And coalesce(ihtiyac,0) > coalesce(isemribeklenen,0) + coalesce(isemriharicigelen,0) " +
                            " order by ilksevktar "

            oSQL.SQLExecute(,, 0)

            oSQL.cSQLQuery = "create index " + cTable3 + "_ndx on " + cTable3 + " (musterino asc, stoktipi asc) "

            oSQL.SQLExecute()

            ' serbest miktarları depo bazında al

            oSQL.cSQLQuery = " (stokno CHAR(30) null, " +
                            " renk CHAR(30) null, " +
                            " beden CHAR(30) null, " +
                            " depo CHAR(30) null, " +
                            " serbestmiktar decimal(18,6) null ) "

            cTable = oSQL.CreateTempTable()

            oSQL.cSQLQuery = "insert into " + cTable + " (stokno, renk, beden, depo, serbestmiktar) " +
                            " select distinct stokno, renk, beden, depo, serbestmiktar " +
                            " from " + cTable3

            oSQL.SQLExecute()

            oSQL.cSQLQuery = "create index " + cTable + "_ndx on " + cTable + " (stokno asc, renk asc, beden asc) "

            oSQL.SQLExecute()

            ' onaylanmamış ve red edilmemiş yani cevap verilmemiş rezervasyon isteklerini kapat

            oSQL.cSQLQuery = "update mtkrezervasyononayistek set " +
                            " kapandi = 'E' , " +
                            " kapanditarih = getdate() " +
                            " where (kapandi is null or kapandi = '' or kapandi = 'H') " +
                            " and (onay is null or onay = '') "

            oSQL.SQLExecute()

            ' eMailler musterino ve stok tipine göre gruplanacak

            ReDim aMusteriStokTipi(0)
            nCnt = -1

            oSQL.cSQLQuery = "Select distinct musterino, stoktipi " +
                            " from " + cTable3 +
                            " order by musterino, stoktipi "

            oSQL.GetSQLReader()

            Do While oSQL.oReader.Read
                nCnt = nCnt + 1
                ReDim Preserve aMusteriStokTipi(nCnt)
                aMusteriStokTipi(nCnt).cMusteriNo = oSQL.SQLReadString("musterino")
                aMusteriStokTipi(nCnt).cStokTipi = oSQL.SQLReadString("stoktipi")
            Loop
            oSQL.oReader.Close()

            ReDim aM1(0)
            nCnt = -1

            ' buffer malzemeler

            oSQL.cSQLQuery = "select musterino, stoktipi, malzemetakipno, stokno, cinsaciklamasi, " +
                            " renk, beden, ihtiyac, departman, ilksevktar, temindept " +
                            " from " + cTable3 +
                            " where coalesce(ihtiyac,0) > 0 " +
                            " order by ilksevktar, stokno, renk, beden, malzemetakipno  "

            oSQL.GetSQLReader()

            Do While oSQL.oReader.Read
                nCnt = nCnt + 1
                ReDim Preserve aM1(nCnt)
                aM1(nCnt).cMusteriNo = oSQL.SQLReadString("musterino")
                aM1(nCnt).cStokTipi = oSQL.SQLReadString("stoktipi")
                aM1(nCnt).cMTF = oSQL.SQLReadString("malzemetakipno")
                aM1(nCnt).cStokNo = oSQL.SQLReadString("stokno")
                aM1(nCnt).cCinsAciklamasi = oSQL.SQLReadString("cinsaciklamasi")
                aM1(nCnt).cRenk = oSQL.SQLReadString("renk")
                aM1(nCnt).cBeden = oSQL.SQLReadString("beden")
                aM1(nCnt).nIhtiyac = oSQL.SQLReadDouble("ihtiyac")
                aM1(nCnt).nOrjinalIhtiyac = oSQL.SQLReadDouble("ihtiyac")
                aM1(nCnt).cDepartman = oSQL.SQLReadString("departman")
                aM1(nCnt).cTeminDept = oSQL.SQLReadString("temindept")
            Loop
            oSQL.oReader.Close()

            ' rezervasyon taleplerini yap
            cSQLMailHeader = "insert into mtkrezervasyononayemail (email, requestid, malzemetakipno, departman, createdate) values "
            cSQLMailBody = ""

            cSQLIstekHeader = "insert into mtkrezervasyononayistek (requestid, musterino, malzemetakipno, stoktipi, stokno,  " +
                              " cinsaciklamasi, renk, beden, ihtiyac, depo, " +
                              " rezerve, taleptarihi, departman, temindept ) values "
            cSQLIstekBody = ""

            For nCnt = 0 To aM1.GetUpperBound(0)

                Do While True

                    cDepo = ""
                    nSerbest = 0
                    nMiktar = 0
                    nIhtiyac = 0

                    oSQL2.cSQLQuery = "select top 1 depo, serbestmiktar " +
                                    " from " + cTable +
                                    " where stokno = '" + aM1(nCnt).cStokNo + "' " +
                                    " and renk = '" + aM1(nCnt).cRenk + "' " +
                                    " and beden = '" + aM1(nCnt).cBeden + "' " +
                                    " and serbestmiktar is not null " +
                                    " and serbestmiktar >= 0 " +
                                    " order by serbestmiktar desc , depo "

                    oSQL2.GetSQLReader()

                    If oSQL2.oReader.Read Then
                        cDepo = oSQL2.SQLReadString("depo")
                        nSerbest = oSQL2.SQLReadDouble("serbestmiktar")
                    End If
                    oSQL2.oReader.Close()

                    If nSerbest <= 0 Or cDepo.Trim = "" Then
                        Exit Do
                    End If

                    If aM1(nCnt).nIhtiyac >= nSerbest Then
                        nMiktar = nSerbest
                        aM1(nCnt).nIhtiyac = aM1(nCnt).nIhtiyac - nMiktar
                    Else
                        nMiktar = aM1(nCnt).nIhtiyac
                        aM1(nCnt).nIhtiyac = 0
                    End If

                    If nMiktar > 0 Then

                        oSQL2.cSQLQuery = "update " + cTable +
                                       " set serbestmiktar = coalesce(serbestmiktar,0) - " + SQLWriteDecimal(nMiktar) +
                                       " where stokno = '" + aM1(nCnt).cStokNo + "' " +
                                       " and renk = '" + aM1(nCnt).cRenk + "' " +
                                       " and beden = '" + aM1(nCnt).cBeden + "' " +
                                       " and depo = '" + cDepo + "' "

                        oSQL2.SQLExecute()

                        cRequestId = oSQL2.GetSequenceFisNo("mtkrezonayistek")

                        cSQL = " ( '" + cRequestId + "' , " +
                                       " '" + aM1(nCnt).cMusteriNo + "' , " +
                                       " '" + aM1(nCnt).cMTF + "' , " +
                                       " '" + aM1(nCnt).cStokTipi + "' , " +
                                       " '" + aM1(nCnt).cStokNo + "' , "
                        cSQL = cSQL +
                                       " '" + aM1(nCnt).cCinsAciklamasi + "' , " +
                                       " '" + aM1(nCnt).cRenk + "' , " +
                                       " '" + aM1(nCnt).cBeden + "' , " +
                                       SQLWriteDecimal(aM1(nCnt).nOrjinalIhtiyac) + " , " +
                                       " '" + cDepo + "' , "
                        cSQL = cSQL +
                                       SQLWriteDecimal(nMiktar) + " , " +
                                       " getdate() , " +
                                       " '" + aM1(nCnt).cDepartman + "' , " +
                                       " '" + aM1(nCnt).cTeminDept + "' ) "

                        nCntIstek += 1
                        If cSQLIstekBody.Trim = "" Then
                            cSQLIstekBody = cSQL
                        Else
                            cSQLIstekBody = cSQLIstekBody + " , " + cSQL
                        End If
                        If nCntIstek >= BATCH_SIZE Then
                            oSQL2.cSQLQuery = cSQLIstekHeader + cSQLIstekBody
                            oSQL2.SQLExecute()
                            cSQLIstekBody = ""
                            nCntIstek = 0
                        End If

                        ' onay maili alıcıları

                        ReDim aEMail(0)
                        nCnt1 = -1

                        oSQL2.cSQLQuery = "select distinct b.email " +
                                        " from onaysureci a with (NOLOCK) , personel b with (NOLOCK) " +
                                        " where a.onaylayacakpersonel = b.personel " +
                                        " And a.onaysistemi = 'REZERVASYON' " +
                                        " and a.musterino in ('" + aM1(nCnt).cMusteriNo + "','HEPSI') " +
                                        " and a.stoktipi in ('" + aM1(nCnt).cStokTipi + "','HEPSI') " +
                                        " and b.email is not null " +
                                        " and b.email <> '' " +
                                        " order by b.email "

                        oSQL2.GetSQLReader()

                        Do While oSQL2.oReader.Read
                            nCnt1 = nCnt1 + 1
                            ReDim Preserve aEMail(nCnt1)
                            aEMail(nCnt1) = oSQL2.SQLReadString("email")
                        Loop
                        oSQL2.oReader.Close()

                        If nCnt1 >= 0 Then
                            ' eMail kayıtlarını yap
                            For nCnt1 = 0 To aEMail.GetUpperBound(0)

                                If aEMail(nCnt1).Trim <> "" Then

                                    cSQL = " ( '" + aEMail(nCnt1).Trim + "', " +
                                        " '" + cRequestId + "' , " +
                                        " '" + aM1(nCnt).cMTF + "' , " +
                                        " '" + aM1(nCnt).cDepartman + "' , " +
                                        " getdate() ) "

                                    nCntMail += 1
                                    If cSQLMailBody.Trim = "" Then
                                        cSQLMailBody = cSQL
                                    Else
                                        cSQLMailBody = cSQLMailBody + " , " + cSQL
                                    End If
                                    If nCntMail >= BATCH_SIZE Then
                                        oSQL2.cSQLQuery = cSQLMailHeader + cSQLMailBody
                                        oSQL2.SQLExecute()
                                        cSQLMailBody = ""
                                        nCntMail = 0
                                    End If
                                End If
                            Next
                        End If
                    End If

                    If aM1(nCnt).nIhtiyac <= 0 Or nMiktar <= 0 Then
                        Exit Do
                    End If
                Loop
            Next

            If cSQLIstekBody.Trim <> "" Then
                oSQL2.cSQLQuery = cSQLIstekHeader + cSQLIstekBody
                oSQL2.SQLExecute()
            End If

            If cSQLMailBody.Trim <> "" Then
                oSQL2.cSQLQuery = cSQLMailHeader + cSQLMailBody
                oSQL2.SQLExecute()
            End If

            oSQL.DropTable(cTable)
            oSQL.DropTable(cTable3)
#End Region

#Region "Isemri"

            batchIsemriHeader.Clear()
            batchIsemriLines.Clear()
            nBatchIsemriCount = 0
            nBatchIsemriLinesCount = 0

            cMusteriNo = ""
            cStokTipi = ""
            cIsemirleri = ""

            If nMode = 1 Then
                ' kit içi satınalma işemri
                oSQL.cSQLQuery = "SELECT DISTINCT w.satinalmaci, w.satinalmaciemail, w.firma, w.saticiemail, w.temindept, w.malzemetakipno, w.departman2 " +
                                " from ( " +
                                " select distinct satinalmaci, satinalmaciemail, firma, saticiemail, temindept, malzemetakipno, " +
                                " departman2 = CASE WHEN departman = 'ILIK/DUGME' THEN 'UTU / PAKET' ELSE departman end "
            Else
                ' kit dışı satınalma işemri
                oSQL.cSQLQuery = "SELECT DISTINCT w.satinalmaci, w.satinalmaciemail, w.firma, w.saticiemail, w.temindept, w.departman2 " +
                                " from ( " +
                                " select distinct satinalmaci, satinalmaciemail, firma, saticiemail, temindept, " +
                                " departman2 = CASE WHEN departman = 'ILIK/DUGME' THEN 'UTU / PAKET' ELSE departman end "
            End If

            oSQL.cSQLQuery = oSQL.cSQLQuery +
                                " from " + cOtoSatinalmaIsemriView +
                                " where temindept Is Not null " +
                                " And temindept <> '' " +
                                " and firma is not null " +
                                " and firma <> '' " +
                                " and ihtiyac is not null " +
                                " and ihtiyac > 0 " +
                                " and coalesce(ihtiyac,0) > coalesce(isemribeklenen,0) + coalesce(isemriharicigelen,0) " +
                                " and satinalmaciemail is not null " +
                                " and satinalmaciemail <> '' " +
                                " and saticiemail is not null " +
                                " and saticiemail <> '' " +
                                " and malzemetakipno is not null " +
                                " and malzemetakipno <> '' " +
                                " ) w "

            If nMode = 1 Then
                ' kit içi satınalma işemri
                oSQL.cSQLQuery = oSQL.cSQLQuery +
                                " order by w.malzemetakipno, w.satinalmaci, w.firma "
            Else
                ' kit dışı satınalma işemri
                oSQL.cSQLQuery = oSQL.cSQLQuery +
                                " order by w.satinalmaci, w.firma "
            End If

            oSQL.GetSQLReader()

            ' Reset batch counters

            batchIsemriHeader.Clear()
            batchIsemriLines.Clear()
            nBatchIsemriCount = 0
            nBatchIsemriLinesCount = 0

            Do While oSQL.oReader.Read

                cIsemriNo = GetMlzIsemriNo2(cDatabase)

                If cIsemirleri.Trim = "" Then
                    cIsemirleri = "'" + cIsemriNo + "'"
                Else
                    If InStr(cIsemirleri, cIsemriNo) = 0 Then
                        cIsemirleri = cIsemirleri + ", '" + cIsemriNo + "'"
                    End If
                End If

                If nMode = 1 Then
                    ' kit içi satınalma işemri
                    If oSQL.SQLReadString("departman2") = "UTU / PAKET" Then
                        cFilter2 = " and departman in ('ILIK/DUGME','UTU / PAKET') " +
                                   " and malzemetakipno = '" + oSQL.SQLReadString("malzemetakipno") + "' "
                    Else
                        cFilter2 = " and departman = '" + oSQL.SQLReadString("departman2") + "' " +
                                   " and malzemetakipno = '" + oSQL.SQLReadString("malzemetakipno") + "' "
                    End If
                Else
                    ' kit dışı satınalma işemri
                    If oSQL.SQLReadString("departman2") = "UTU / PAKET" Then
                        cFilter2 = " and departman in ('ILIK/DUGME','UTU / PAKET') "
                    Else
                        cFilter2 = " and departman = '" + oSQL.SQLReadString("departman2") + "' "
                    End If
                End If

                ' Build batch insert for isemri

                Dim isemriRow As String = " ('" + cIsemriNo + "', " +
                    " convert(date,getdate()), " +
                    " '" + oSQL.SQLReadString("temindept") + "', " +
                    " '" + oSQL.SQLReadString("firma") + "', " +
                    " '" + oSQL.SQLReadString("saticiemail") + "', " +
                    " '" + oSQL.SQLReadString("satinalmaci") + "', " +
                    " '" + oSQL.SQLReadString("satinalmaciemail") + "', " +
                    " 'CLR', 'CLR', getdate(), getdate() ) "

                If nBatchIsemriCount = 0 Then
                    batchIsemriHeader.Clear()
                    batchIsemriHeader.Append("insert into isemri (isemrino, tarih, departman, firma, emailsatici, " +
                                                                " takipelemani, emailsatinalmaci, username, createuser, creationdate, " +
                                                                " modificationdate) values ")
                    batchIsemriHeader.Append(isemriRow)
                Else
                    batchIsemriHeader.Append(" , ")
                    batchIsemriHeader.Append(isemriRow)
                End If
                nBatchIsemriCount += 1

                ' Flush isemri batch if limit reached

                If nBatchIsemriCount >= BATCH_SIZE Then
                    oSQL2.cSQLQuery = batchIsemriHeader.ToString()
                    oSQL2.SQLExecute()
                    batchIsemriHeader.Clear()
                    nBatchIsemriCount = 0
                End If

                ' Get lines for this isemri

                oSQL2.cSQLQuery = "select malzemetakipno, stokno, cinsaciklamasi, renk, beden, departman, " +
                                " fiyat1, doviz1, musterino, stoktipi, " +
                                " miktar = coalesce(ihtiyac,0) - coalesce(isemribeklenen,0) - coalesce(isemriharicigelen,0), " +
                                " uretimecikisdepo, satinalmadeposu " +
                                " from " + cOtoSatinalmaIsemriView +
                                " where firma = '" + oSQL.SQLReadString("firma") + "' " +
                                " and temindept = '" + oSQL.SQLReadString("temindept") + "' " +
                                " and satinalmaciemail  = '" + oSQL.SQLReadString("satinalmaciemail") + "' " +
                                " and saticiemail  = '" + oSQL.SQLReadString("saticiemail") + "' " +
                                " and ihtiyac is not null " +
                                " and ihtiyac > 0 " +
                                " and coalesce(ihtiyac,0) > coalesce(isemribeklenen,0) + coalesce(isemriharicigelen,0) " +
                                " and satinalmaciemail is not null " +
                                " and satinalmaciemail <> '' " +
                                " and saticiemail is not null " +
                                " and saticiemail <> '' " +
                                " and malzemetakipno is not null " +
                                " and malzemetakipno <> '' " +
                                cFilter2

                If nMode = 1 Then
                    ' kit içi satınalma işemri
                    oSQL2.cSQLQuery = oSQL2.cSQLQuery +
                                " order by malzemetakipno, stokno, renk, beden "
                Else
                    ' kit dışı satınalma işemri
                    oSQL2.cSQLQuery = oSQL2.cSQLQuery +
                                " order by stokno, renk, beden "
                End If

                oSQL2.GetSQLReader()

                Do While oSQL2.oReader.Read

                    Dim lineRow As String = " ('" + cIsemriNo + "', " +
                        " '" + oSQL2.SQLReadString("stokno") + "', " +
                        " '" + oSQL2.SQLReadString("renk") + "', " +
                        " '" + oSQL2.SQLReadString("beden") + "', " +
                        " '" + oSQL2.SQLReadString("satinalmadeposu") + "', " +
                        SQLWriteDecimal(oSQL2.SQLReadDouble("miktar")) + ", " +
                        SQLWriteDecimal(oSQL2.SQLReadDouble("miktar")) + ", " +
                        SQLWriteDecimal(oSQL2.SQLReadDouble("fiyat1")) + ", " +
                        " '" + oSQL2.SQLReadString("doviz1") + "', " +
                        " getdate() , " +
                        " '" + oSQL2.SQLReadString("malzemetakipno") + "' , " +
                        " 'E' , getdate() , CONVERT(CHAR(8),GETDATE(),108) , 'CLR' , " +
                        " '" + oSQL2.SQLReadString("departman") + "' ) "

                    If nBatchIsemriLinesCount = 0 Then
                        batchIsemriLines.Clear()
                        batchIsemriLines.Append("set dateformat dmy " +
                                                " insert into isemrilines (isemrino, stokno, renk, beden, depo, " +
                                                " miktar1, brutmiktar1, fiyat, doviz, baslamatarihi, " +
                                                " malzemetakipno, onaylimalzeme, degistirmetarihi, degistirmesaati, username, " +
                                                " departman ) values ")

                        batchIsemriLines.Append(lineRow)
                    Else
                        batchIsemriLines.Append(" , ")
                        batchIsemriLines.Append(lineRow)
                    End If
                    nBatchIsemriLinesCount += 1

                    ' Flush lines batch if limit reached

                    If nBatchIsemriLinesCount >= BATCH_SIZE Then
                        oSQL3.cSQLQuery = batchIsemriLines.ToString()
                        oSQL3.SQLExecute()
                        batchIsemriLines.Clear()
                        nBatchIsemriLinesCount = 0
                    End If
                Loop
                oSQL2.oReader.Close()
            Loop
            oSQL.oReader.Close()

            ' Flush remaining isemri batch

            If nBatchIsemriCount > 0 Then
                oSQL2.cSQLQuery = batchIsemriHeader.ToString()
                oSQL2.SQLExecute()
            End If

            ' Flush remaining lines batch

            If nBatchIsemriLinesCount > 0 Then
                oSQL3.cSQLQuery = batchIsemriLines.ToString()
                oSQL3.SQLExecute()
            End If
#End Region

#Region "Post Processing"

            ' post processing of created isemri records

            If cIsemirleri.Trim <> "" Then

                ' delete empty lines and their isemri headers if no lines remain

                oSQL.cSQLQuery = "DELETE isemrilines " +
                                 " where isemrino in (" + cIsemirleri + ") " +
                                 " And (stokno Is NULL Or stokno = '') "

                oSQL.SQLExecute(,, 0)

                oSQL.cSQLQuery = "DELETE isemri " +
                                 " WHERE isemrino in (" + cIsemirleri + ") " +
                                 " AND NOT EXISTS (SELECT isemrino " +
                                                 " FROM isemrilines WITH (NOLOCK)  " +
                                                 " WHERE isemrilines.isemrino = isemri.isemrino) "
                oSQL.SQLExecute(,, 0)

                ' update isemri 

                oSQL.cSQLQuery = "update a SET " +
                                " a.odemesekli = b.paymentterms, " +
                                " a.kurtipi = b.kurcinsi, " +
                                " a.vadetipi = b.vadetipi, " +
                                " a.teslimyeri = b.teslimyeri, " +
                                " a.iban = (SELECT TOP 1 bankibans " +
                                                " FROM firmabanka WITH (NOLOCK) " +
                                                " WHERE firma = a.firma ) " +
                                " from isemri a , firma b " +
                                " where a.firma = b.firma " +
                                " And a.isemrino in (" + cIsemirleri + ") " +
                                " ; "

                ' MTF satırlarına isemri verilen miktarı refresh et 

                oSQL.cSQLQuery = oSQL.cSQLQuery +
                                " update x set " +
                                " isemriverilen = (select sum(coalesce(b.miktar1,0)) " +
                                                " from isemri a with (NOLOCK) , isemrilines b with (NOLOCK) " +
                                                " where a.isemrino = b.isemrino " +
                                                " And a.departman = x.temindept " +
                                                " And b.departman = x.departman " +
                                                " And b.malzemetakipno = x.malzemetakipno " +
                                                " And b.stokno = x.stokno " +
                                                " And b.renk = x.renk " +
                                                " And b.beden = x.beden )  " +
                                " from mtkfislines x " +
                                " where exists (select a.isemrino " +
                                                " from isemri a with (NOLOCK) , isemrilines b with (NOLOCK) " +
                                                " where a.isemrino = b.isemrino " +
                                                " And a.isemrino in (" + cIsemirleri + ") " +
                                                " And b.malzemetakipno = x.malzemetakipno " +
                                                " And b.stokno = x.stokno " +
                                                " And b.renk = x.renk " +
                                                " And b.beden = x.beden " +
                                                " And a.departman = x.temindept " +
                                                " And b.departman = x.departman)  "
                oSQL.SQLExecute(,, 0)

                ' Call doaftersave_isemri stored procedure for all created isemri records

                CallDoAfterSaveIsemri(cIsemirleri, cDatabase)
            End If

#End Region

            oSQL.CloseConn()
            oSQL2.CloseConn()
            oSQL3.CloseConn()

            oSQL = Nothing
            oSQL2 = Nothing
            oSQL3 = Nothing

        Catch ex As Exception
            ErrDisp(ex, "OtoRezisemri1")
        End Try
    End Sub

    Public Function GetMlzIsemriNo2(cDatabase As String) As String

        GetMlzIsemriNo2 = "0000000000"

        Dim oSQL As New SQLServerClass(True,, cDatabase)

        Try
            Dim cIsemriNo As String = ""
            Dim nFisNo As Double = 0

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select top 1 mlzisemrino " +
                             " from sysinfo "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                nFisNo = oSQL.SQLReadDouble("mlzisemrino")
            End If
            oSQL.oReader.Close()

            oSQL.cSQLQuery = "select max(isemrino) " +
                             " from isemri "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                cIsemriNo = If(IsDBNull(oSQL.oReader(0)), "", oSQL.oReader(0).ToString())
            End If
            oSQL.oReader.Close()

            If cIsemriNo.Trim <> "" AndAlso IsNumeric(cIsemriNo) Then
                If nFisNo < CDbl(cIsemriNo) Then
                    nFisNo = CDbl(cIsemriNo)
                End If
            End If

            Do While True
                nFisNo = nFisNo + 1

                cIsemriNo = nFisNo.ToString("0000000000")

                oSQL.cSQLQuery = "select top 1 isemrino " +
                                 " from isemri " +
                                 " where isemrino = '" + cIsemriNo + "' "

                If Not oSQL.CheckExists() Then
                    Exit Do
                End If
            Loop

            oSQL.cSQLQuery = "update sysinfo set mlzisemrino = " + CStr(nFisNo)

            oSQL.SQLExecute()

            GetMlzIsemriNo2 = nFisNo.ToString("0000000000")

            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp(ex, "GetMlzIsemriNo2")
        End Try
    End Function

    Public Function ORSHatirlatma(cDatabase As String, Optional nMode1 As Integer = 1) As Boolean

        ' nMode1 = 1 kit içi
        ' nMode1 = 2 kit dışı

        nMode = nMode1

        ORSHatirlatma = False

        Try
            Dim oSQL As New SQLServerClass(True,, cDatabase)
            Dim oSQL2 As New SQLServerClass(True,, cDatabase)
            Dim cMailTo As String = ""
            Dim nRow As Integer = 0
            Dim nMaxCols As Integer = 0
            Dim cSubject As String = ""
            Dim cBody As String = ""
            Dim cControlMail As String = ""
            Dim cIsemriNo As String = ""
            Dim nReportID As Double = 0
            Dim cFileName As String = ""
            Dim lReportOK As Boolean = False
            Dim cOtoSatinalmaIsemriView As String = "otosatinalmaisemri"

            If nMode = 1 Then
                cOtoSatinalmaIsemriView = "otosatinalmaisemri"
            Else
                cOtoSatinalmaIsemriView = "otosatinalmaisemrikitdisi"
            End If

            oSQL.OpenConn()
            oSQL2.OpenConn()

            cControlMail = oSQL.GetSysPar("emailcontrol")

            ' rezervasyon onay talebi uyarı mailleri gönder

            oSQL.cSQLQuery = "select distinct satinalmamudurumail " +
                            " from otorezervasyontalebiuyari " +
                            " where satinalmamudurumail is not null " +
                            " and satinalmamudurumail <> '' "

            oSQL.GetSQLReader()

            Do While oSQL.oReader.Read

                cMailTo = oSQL.SQLReadString("satinalmamudurumail")

                nRow = 0
                nMaxCols = 7
                ReDim aHTMLRow(nMaxCols, 0)
                aHTMLRow(0, 0).cValue = "Musteri"
                aHTMLRow(1, 0).cValue = "MTF"
                aHTMLRow(2, 0).cValue = "Stok Tipi"
                aHTMLRow(3, 0).cValue = "Stok No"
                aHTMLRow(4, 0).cValue = "Cins Açıklaması"
                aHTMLRow(5, 0).cValue = "Renk"
                aHTMLRow(6, 0).cValue = "Beden"
                aHTMLRow(7, 0).cValue = "eMail"

                oSQL2.cSQLQuery = "select musterino, malzemetakipno, stokno, renk, beden, stoktipi, cinsaciklamasi, cnt, mailto " +
                                " from otorezervasyontalebiuyari  " +
                                " where satinalmamudurumail = '" + cMailTo + "' " +
                                " and cnt >= 2 " +
                                " order by musterino, malzemetakipno, stokno, renk, beden "

                oSQL2.GetSQLReader()

                Do While oSQL2.oReader.Read

                    nRow = nRow + 1
                    ReDim Preserve aHTMLRow(nMaxCols, nRow)
                    aHTMLRow(0, nRow).cValue = oSQL2.SQLReadString("musterino")
                    aHTMLRow(1, nRow).cValue = oSQL2.SQLReadString("malzemetakipno")
                    aHTMLRow(2, nRow).cValue = oSQL2.SQLReadString("stoktipi")
                    aHTMLRow(3, nRow).cValue = oSQL2.SQLReadString("stokno")
                    aHTMLRow(4, nRow).cValue = oSQL2.SQLReadString("cinsaciklamasi")
                    aHTMLRow(5, nRow).cValue = oSQL2.SQLReadString("renk")
                    aHTMLRow(6, nRow).cValue = oSQL2.SQLReadString("beden")
                    aHTMLRow(7, nRow).cValue = oSQL2.SQLReadString("mailto")
                Loop
                oSQL2.oReader.Close()

                If nRow > 0 Then
                    cBody = GetHTMLTable(nRow, nMaxCols, "Otomatik Rezervasyon Gecikme Uyarısı / v:" + cVersion)
                    cSubject = "Otomatik rezervasyon aksaması uyarı mesajıdır"
                    If cControlMail.Trim <> "" Then
                        cMailTo = cMailTo + "," + cControlMail
                    End If

                    If SendGoogleMail(cMailTo, cSubject, cBody, , cDatabase) Then
                        ActivityLog(cDatabase, "Mesaj", "OMRSU eMail Gonderildi", cSubject, cMailTo + " " + cBody.Trim)
                    Else
                        ActivityLog(cDatabase, "Mesaj", "OMRSU eMail Gonderilemedi", cSubject, cMailTo + " " + cBody.Trim)
                    End If
                End If
            Loop
            oSQL.oReader.Close()

            ' işemri uyarı mailleri gönder

            cMailTo = cControlMail ' "erkan.cetin@eroglugiyim.com,adnan.demir@eroglugiyim.com,volkan.polatman@eroglugiyim.com"

            nRow = 0
            nMaxCols = 7
            ReDim aHTMLRow(nMaxCols, 0)
            aHTMLRow(0, 0).cValue = "Musteri"
            aHTMLRow(1, 0).cValue = "Stok Tipi"
            aHTMLRow(2, 0).cValue = "Tedarikci"
            aHTMLRow(3, 0).cValue = "Stok No"
            aHTMLRow(4, 0).cValue = "Aciklama"
            aHTMLRow(5, 0).cValue = "Fiyat"
            aHTMLRow(6, 0).cValue = "Doviz"
            aHTMLRow(7, 0).cValue = "Aksiyon"

            ' satınalmacı eMail tanımlanmamış

            oSQL2.cSQLQuery = "select distinct musterino, stoktipi, firma, stokno, cinsaciklamasi, fiyat1, doviz1 " +
                            " from " + cOtoSatinalmaIsemriView +
                            " where temindept is not null " +
                            " and temindept <> '' " +
                            " and firma is not null " +
                            " and firma <> '' " +
                            " and malzemetakipno is not null " +
                            " and malzemetakipno <> '' " +
                            " and ihtiyac is not null " +
                            " and ihtiyac > 0 " +
                            " and coalesce(ihtiyac,0) > coalesce(isemribeklenen,0) + coalesce(isemriharicigelen,0) " +
                            " and (satinalmaciemail is null or satinalmaciemail = '') " +
                            " order by musterino, stoktipi, firma "

            oSQL2.GetSQLReader()

            Do While oSQL2.oReader.Read

                nRow = nRow + 1
                ReDim Preserve aHTMLRow(nMaxCols, nRow)
                aHTMLRow(0, nRow).cValue = oSQL2.SQLReadString("musterino")
                aHTMLRow(1, nRow).cValue = oSQL2.SQLReadString("stoktipi")
                aHTMLRow(2, nRow).cValue = oSQL2.SQLReadString("firma")
                aHTMLRow(3, nRow).cValue = oSQL2.SQLReadString("stokno")
                aHTMLRow(4, nRow).cValue = oSQL2.SQLReadString("cinsaciklamasi")
                aHTMLRow(5, nRow).cValue = oSQL2.SQLReadDouble("fiyat1").ToString(G_Number2Format)
                aHTMLRow(6, nRow).cValue = oSQL2.SQLReadString("doviz1")
                aHTMLRow(7, nRow).cValue = "Onay sürecine satınalmacı eMail tanımlaması yapılmalıdır"
            Loop
            oSQL2.oReader.Close()

            ' satıcı eMail tanımlanmamış

            oSQL2.cSQLQuery = "Select distinct musterino, stoktipi, firma, stokno, cinsaciklamasi, fiyat1, doviz1 " +
                            " from " + cOtoSatinalmaIsemriView +
                            " where temindept Is Not null " +
                            " And temindept <> '' " +
                            " and firma is not null " +
                            " and firma <> '' " +
                            " and malzemetakipno is not null " +
                            " and malzemetakipno <> '' " +
                            " and ihtiyac is not null " +
                            " and ihtiyac > 0 " +
                            " and coalesce(ihtiyac,0) > coalesce(isemribeklenen,0) + coalesce(isemriharicigelen,0) " +
                            " and (saticiemail is null or saticiemail = '') " +
                            " order by musterino, stoktipi, firma "

            oSQL2.GetSQLReader()

            Do While oSQL2.oReader.Read

                nRow = nRow + 1
                ReDim Preserve aHTMLRow(nMaxCols, nRow)
                aHTMLRow(0, nRow).cValue = oSQL2.SQLReadString("musterino")
                aHTMLRow(1, nRow).cValue = oSQL2.SQLReadString("stoktipi")
                aHTMLRow(2, nRow).cValue = oSQL2.SQLReadString("firma")
                aHTMLRow(3, nRow).cValue = oSQL2.SQLReadString("stokno")
                aHTMLRow(4, nRow).cValue = oSQL2.SQLReadString("cinsaciklamasi")
                aHTMLRow(5, nRow).cValue = oSQL2.SQLReadDouble("fiyat1").ToString(G_Number2Format)
                aHTMLRow(6, nRow).cValue = oSQL2.SQLReadString("doviz1")
                aHTMLRow(7, nRow).cValue = "Tedarikçi kartındaki satici eMaillerine tanımlama yapılmalıdır"
            Loop
            oSQL2.oReader.Close()

            If nRow > 0 Then
                cBody = GetHTMLTable(nRow, nMaxCols, "Satınalma İşemri Oluşturmada Eksik Tanımlamalar / v:" + cVersion)
                cSubject = "Otomatik işemri oluşturma uyarı mesajıdır, müşteri / satınalmacı / tedarkçi eksik tanımlanmalar tamamlanmalıdır"

                If SendGoogleMail(cMailTo, cSubject, cBody, , cDatabase) Then
                    ActivityLog(cDatabase, "Mesaj", "OMRSU eMail Gonderildi", cSubject, cMailTo + " " + cBody.Trim)
                Else
                    ActivityLog(cDatabase, "Mesaj", "OMRSU eMail Gonderilemedi", cSubject, cMailTo + " " + cBody.Trim)
                End If
            End If

            ' gönderilememiş işemirlerini gönder 

            oSQL.cSQLQuery = "select distinct a.isemrino , a.emailsatici , a.emailsatinalmaci , c.reportid " +
                                " FROM isemri a WITH (NOLOCK) , departman b WITH (NOLOCK) , stireports c WITH (NOLOCK) " +
                                " WHERE a.departman = b.departman  " +
                                " AND b.reportname = c.reportname " +
                                " AND a.createuser = 'CLR' " +
                                " AND (a.emailgonderildi IS NULL or a.emailgonderildi = 'H' OR a.emailgonderildi = '') " +
                                " ORDER BY a.isemrino "
            oSQL.GetSQLReader()

            Do While oSQL.oReader.Read

                cMailTo = oSQL.SQLReadString("emailsatici") + "," + oSQL.SQLReadString("emailsatinalmaci")
                cIsemriNo = oSQL.SQLReadString("isemrino")
                nReportID = CDbl(oSQL.SQLReadInteger("reportid"))
                cFileName = "C:\wintex\Temp\" + "ErogluSatinalma-" + cIsemriNo + ".xls"
                cSubject = "Eroğlu satınalma siparişi : " + cIsemriNo
                cBody = "Eroğlu satınalma siparişi : " + cIsemriNo

                If My.Computer.FileSystem.FileExists(cFileName) Then
                    My.Computer.FileSystem.DeleteFile(cFileName)
                End If

                lReportOK = False

                If StiReportToExcel(nReportID.ToString, cFileName, cIsemriNo,,,,,,,,,, cDatabase) Then
                    If My.Computer.FileSystem.FileExists(cFileName) Then
                        lReportOK = True
                    End If
                End If

                If lReportOK Then

                    If cControlMail.Trim <> "" Then
                        cMailTo = cMailTo + "," + cControlMail
                    End If

                    If SendGoogleMail(cMailTo, cSubject, cBody, cFileName, cDatabase) Then

                        oSQL2.cSQLQuery = "update isemri " +
                                          " SET emailgonderildi = 'E' " +
                                          " WHERE isemrino = '" + cIsemriNo + "' "
                        oSQL2.SQLExecute()

                        ActivityLog(cDatabase, "Mesaj", "OMSS2 eMail Gonderildi", cSubject, cMailTo + " " + cBody.Trim)
                    Else

                        oSQL2.cSQLQuery = "update isemri " +
                                          " SET emailgonderildi = 'H' " +
                                          " WHERE isemrino = '" + cIsemriNo + "' "
                        oSQL2.SQLExecute()

                        ActivityLog(cDatabase, "Mesaj", "OMSS2 eMail Gonderilemedi", cSubject, cMailTo + " " + cBody.Trim)
                    End If

                End If
            Loop
            oSQL.oReader.Close()

            oSQL.CloseConn()
            oSQL2.CloseConn()

            ORSHatirlatma = True

        Catch ex As Exception
            ErrDisp(ex, "ORSHatirlatma")
        End Try
    End Function

    ' Helper subroutine to call doaftersave_isemri stored procedure for all created isemri records
    Private Sub CallDoAfterSaveIsemri(cIsemirleri As String, cDatabase As String)
        ' Call doaftersave_isemri stored procedure for each isemrino
        If cIsemirleri.Trim = "" Then
            Return
        End If

        Try
            Dim oSQL As New SQLServerClass(True,, cDatabase)
            oSQL.OpenConn()

            ' Remove quotes and split the string into array
            Dim cleanedString As String = cIsemirleri.Replace("'", "").Replace(" ", "")
            Dim aIsemriNo() As String = cleanedString.Split(","c)

            ' Call stored procedure for each isemrino
            For Each isemriNo As String In aIsemriNo
                If isemriNo.Trim <> "" Then
                    oSQL.cSQLQuery = "EXEC doaftersave_isemri @cisemrino = '" + isemriNo.Trim + "'"
                    oSQL.SQLExecute()
                End If
            Next

            oSQL.CloseConn()
            oSQL = Nothing

        Catch ex As Exception
            ErrDisp(ex, "CallDoAfterSaveIsemri")
        End Try
    End Sub

End Module
