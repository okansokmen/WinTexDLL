Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports DevExpress.DataProcessing
Imports DevExpress.Utils.Design

Module UtilUyumsoftStokFis

    Const cModuleName As String = "UtilUyumsoftStokFis"

    Dim cGUID As String = ""

    Public Function UyumStokFisCheck(Optional cFilter As String = "", Optional ByRef cMessage As String = "") As Boolean

        Dim cSQL As String = ""
        Dim Connyage As SqlConnection = Nothing
        Dim Connyage2 As SqlConnection = Nothing
        Dim dr As SqlDataReader = Nothing
        Dim dr2 As SqlDataReader = Nothing
        Dim cSonuc As String = ""
        Dim cStokNo As String = ""
        Dim cAciklama As String = ""
        Dim cUyumStokID As String = ""
        Dim cUyumBirimID As String = ""
        Dim cUyumStokTipiID As String = ""
        Dim cError As String = ""
        Dim cAnaStokGrubu As String = ""
        Dim cGtipID As String = ""
        Dim cKdvID As String = ""
        Dim cMuhasebeID As String = ""
        Dim cMessage2 As String = ""
        Dim cAciklama2 As String = ""
        Dim cStokFisNo As String = ""
        Dim dTarih As Date = #1/1/1950#
        Dim cStokHareketKodu As String = ""

        Dim cUyumYerelDoviz As String = ""
        Dim nYerelKur As Double = 0
        Dim nAktarDovizKur As Double = 0

        Dim nFiyat As Double = 0
        Dim cDoviz As String = ""
        Dim nKur As Double = 0

        Dim nAktarFiyat As Double = 0
        Dim cAktarDoviz As String = ""
        Dim nAktarKur As Double = 0

        UyumStokFisCheck = False

        Try
            Connyage = OpenConn()
            Connyage2 = OpenConn()

            cUyumYerelDoviz = GetSysParConnected("UyumYerelDoviz", Connyage, "TL")

            cSQL = "update a " +
                    " set a.createuser = a.username " +
                    " from stokfis a with (NOLOCK), stokfislines b with (NOLOCK), stok c with (NOLOCK) " +
                    " where a.stokfisno = b.stokfisno " +
                    " and b.stokno = c.stokno " +
                    " and (a.createuser is null or a.createuser = '') " +
                    cFilter

            ExecuteSQLCommandConnected(cSQL, Connyage)

            ' fişlerin fiyat alanını düzenle

            cSQL = "UPDATE stokfislines " +
                    " Set dovizcinsi = 'USD' " +
                    " where stokhareketno in (SELECT b.stokhareketno " +
                                             " from stokfis a with (NOLOCK), stokfislines b with (NOLOCK), stok c with (NOLOCK) " +
                                             " where a.stokfisno = b.stokfisno " +
                                             " and b.stokno = c.stokno " +
                                             cFilter + " ) " +
                    " and dovizcinsi = '$' " +
                    " and (uyumid2 is null or uyumid2 = 0) "

            ExecuteSQLCommandConnected(cSQL, Connyage)

            cSQL = "UPDATE isemrilines " +
                    " Set doviz = 'USD' " +
                    " where isemrino in (SELECT b.isemrino " +
                                             " from stokfis a with (NOLOCK), stokfislines b with (NOLOCK), stok c with (NOLOCK) " +
                                             " where a.stokfisno = b.stokfisno " +
                                             " and b.stokno = c.stokno " +
                                             cFilter + " ) " +
                    " and doviz = '$' "

            ExecuteSQLCommandConnected(cSQL, Connyage)

            ' fiyatı NULL satırlara SIFIR yaz

            cSQL = "set dateformat dmy " +
                    " UPDATE stokfislines " +
                    " SET birimfiyat = 0 " +
                    " where stokhareketno in (SELECT b.stokhareketno " +
                                             " from stokfis a with (NOLOCK), stokfislines b with (NOLOCK), stok c with (NOLOCK) " +
                                             " where a.stokfisno = b.stokfisno " +
                                             " and b.stokno = c.stokno " +
                                             cFilter + " ) " +
                    " And birimfiyat is NULL " +
                    " and (uyumid2 is null or uyumid2 = 0) "

            ExecuteSQLCommandConnected(cSQL, Connyage)

            ' işemrine bağlı fakat fiyatsız satırları fiyatlandır

            cSQL = "set dateformat dmy " +
                    " UPDATE stokfislines " +
                    " SET birimfiyat = (SELECT TOP 1 birimfiyat " +
                                " FROM isemrilines WITH (NOLOCK) " +
                                " WHERE isemrino = stokfislines.isemrino " +
                                " AND stokno = stokfislines.stokno " +
                                " AND renk = stokfislines.renk " +
                                " AND beden = stokfislines.beden " +
                                " AND malzemetakipkodu = stokfislines.malzemetakipkodu), " +
                    " dovizcinsi = (SELECT TOP 1 dovizcinsi  " +
                                " FROM isemrilines WITH (NOLOCK) " +
                                " WHERE isemrino = stokfislines.isemrino " +
                                " AND stokno = stokfislines.stokno " +
                                " AND renk = stokfislines.renk " +
                                " AND beden = stokfislines.beden " +
                                " AND malzemetakipkodu = stokfislines.malzemetakipkodu), " +
                    " kolino = 'otofiyat' " +
                    " WHERE stokhareketno in (SELECT b.stokhareketno " +
                                             " from stokfis a with (NOLOCK), stokfislines b with (NOLOCK), stok c with (NOLOCK) " +
                                             " where a.stokfisno = b.stokfisno " +
                                             " and b.stokno = c.stokno " +
                                             cFilter + " ) " +
                    " AND (birimfiyat IS NULL OR birimfiyat = 0) " +
                    " AND stokhareketkodu in ('02 Tedarikten Giris','02 Tedarikten iade') " +
                    " AND isemrino IS NOT NULL " +
                    " AND isemrino <> '' " +
                    " and (uyumid2 is null or uyumid2 = 0) "

            ExecuteSQLCommandConnected(cSQL, Connyage)

            ' fişlerin döviz alanlarını düzenle 

            ' döviz cinsi olmayan satırların döviz cinslerini fişteki firmadan çekmeye çalış

            cSQL = "select v.* " +
                    " from (select w.stokhareketno, w.stokfisno, w.faturatarihi, " +
                            " doviz = coalesce((case when w.firma2 Is null Or w.firma2 = '' then w.doviz1 else w.doviz2 end),'" + cUyumYerelDoviz + "') " +
                            " from (select distinct b.stokhareketno, a.stokfisno, a.firma, a.firma2, a.faturatarihi, "
            cSQL = cSQL +
                                    " doviz1  = (select top 1 uyumdoviz " +
                                                " from firma with (NOLOCK) " +
                                                " where firma = a.firma), "
            cSQL = cSQL +
                                    " doviz2 = (select top 1 uyumdoviz " +
                                                " from firma with (NOLOCK) " +
                                                " where firma = a.firma2) "
            cSQL = cSQL +
                                    " From stokfis a with (NOLOCK), stokfislines b with (NOLOCK), stok c with (NOLOCK) " +
                                    " Where a.stokfisno = b.stokfisno " +
                                    " And b.stokno = c.stokno " +
                                    " and (b.uyumid2 is null or b.uyumid2 = 0) " +
                                    cFilter + " ) w ) v "

            cSQL = cSQL +
                    " where v.doviz Is Not NULL " +
                    " and v.doviz <> '' " +
                    " order by v.stokfisno, v.stokhareketno "

            dr = GetSQLReader(cSQL, Connyage)

            Do While dr.Read
                ' döviz
                cSQL = "UPDATE stokfislines " +
                        " SET dovizcinsi = '" + SQLReadString(dr, "doviz") + "' " +
                        " where stokfisno = '" + SQLReadString(dr, "stokfisno") + "' " +
                        " and stokhareketno = " + SQLReadDouble(dr, "stokhareketno").ToString +
                        " and (dovizcinsi is null or dovizcinsi = '') " +
                        " and (uyumid2 is null or uyumid2 = 0) "

                ExecuteSQLCommand(cSQL)
            Loop
            dr.Close()

            ' dovizcinsi NULL satırlara yerel döviz cinsini yaz

            cSQL = "set dateformat dmy " +
                    " UPDATE stokfislines " +
                    " SET dovizcinsi = '" + SQLWriteString(cUyumYerelDoviz, 3) + "' " +
                    " where stokhareketno in (SELECT b.stokhareketno " +
                                             " from stokfis a with (NOLOCK), stokfislines b with (NOLOCK), stok c with (NOLOCK) " +
                                             " where a.stokfisno = b.stokfisno " +
                                             " and b.stokno = c.stokno " +
                                             cFilter + " ) " +
                    " And (dovizcinsi Is null Or dovizcinsi = '') " +
                    " and (uyumid2 is null or uyumid2 = 0) "

            ExecuteSQLCommandConnected(cSQL, Connyage)

            ' kuru eksik satırları MB satış kuruna çevir

            ' TL kuru eksik satırları kurla

            cSQL = "set dateformat dmy " +
                    " UPDATE stokfislines " +
                    " SET kur = 1 " +
                    " where stokhareketno in (SELECT b.stokhareketno " +
                                             " from stokfis a with (NOLOCK), stokfislines b with (NOLOCK), stok c with (NOLOCK) " +
                                             " where a.stokfisno = b.stokfisno " +
                                             " and b.stokno = c.stokno " +
                                             cFilter + " ) " +
                    " And dovizcinsi in ('TL','YTL') " +
                    " And (kur Is NULL Or kur = 0) "

            ExecuteSQLCommandConnected(cSQL, Connyage)

            'cSQL = "set dateformat dmy " +
            '        " UPDATE stokfislines " +
            '        " SET kur = dbo.getkur(dovizcinsi,(SELECT TOP 1 faturatarihi " +
            '                                            " FROM stokfis with (NOLOCK) " +
            '                                            " WHERE stokfisno = stokfislines.stokfisno)) " +
            '        " where stokhareketno in (SELECT b.stokhareketno " +
            '                                 " from stokfis a with (NOLOCK), stokfislines b with (NOLOCK), stok c with (NOLOCK) " +
            '                                 " where a.stokfisno = b.stokfisno " +
            '                                 " and b.stokno = c.stokno " +
            '                                 cFilter + " ) " +
            '        " And (kur Is NULL Or kur = 0) "

            'ExecuteSQLCommandConnected(cSQL, Connyage)

            ' aktarımda kullanılacak döviz, fiyat, kur hesapları

            cSQL = "Select v.*, " +
                    " aktardovizkur = CASE WHEN v.aktardoviz = v.dovizcinsi AND v.kur <> 0 " +
                                        " THEN v.kur " +
                                        " else dbo.getfirmakur(v.aktarfirma, v.aktardoviz,v.faturatarihi) " +
                                        " END, " +
                    " yerelkur = CASE WHEN v.dovizcinsi = '" + cUyumYerelDoviz + "' AND v.kur <> 0 " +
                                        " THEN v.kur " +
                                        " else dbo.getfirmakur(v.aktarfirma, '" + cUyumYerelDoviz + "' , v.faturatarihi) " +
                                        " END " +
                    " from ( SELECT w.*, " +
                            " kur = (case when w.firma2 is null or w.firma2 = '' then w.kur1 else w.kur2 end), " +
                            " aktarfirma = (case when w.firma2 is null or w.firma2 = '' then w.firma else w.firma2 end), " +
                            " aktardoviz = (CASE " +
                                            " WHEN w.stokhareketkodu in ('07 Satis', '07 Satis Iade') " +
                                                " THEN  " +
                                                    " Case WHEN COALESCE(w.doviz2,'') <> '' THEN COALESCE(w.doviz2,'') " +
                                                         " WHEN COALESCE(w.doviz1,'') <> '' THEN COALESCE(w.doviz1,'') " +
                                                         " Else COALESCE(w.dovizcinsi,'') " +
                                                    " End " +
                                            " WHEN w.stokhareketkodu in ('02 Tedarikten Giris', '02 Tedarikten iade') " +
                                                " THEN  " +
                                                    " Case WHEN COALESCE(w.doviz3,'') <> '' THEN COALESCE(w.doviz3,'') " +
                                                         " Else COALESCE(w.dovizcinsi,'') " +
                                                    " End " +
                                            " Else COALESCE(w.dovizcinsi,'') " +
                                            " End ) " +
                            " FROM ( SELECT a.stokfisno, a.firma, a.firma2, b.stokhareketno, b.stokhareketkodu, a.faturatarihi, b.birimfiyat, b.dovizcinsi, "

            cSQL = cSQL +
                                    " kur1 = dbo.getfirmakur(a.firma,  b.dovizcinsi, a.faturatarihi), " +
                                    " kur2 = dbo.getfirmakur(a.firma2, b.dovizcinsi, a.faturatarihi), " +
                                    " doviz1 = (select top 1 uyumdoviz   from firma with (NOLOCK) where firma = a.firma), " +
                                    " doviz2 = (select top 1 uyumdoviz   from firma with (NOLOCK) where firma = a.firma2), " +
                                    " doviz3 = (select top 1 uyumdoviz02 from firma with (NOLOCK) where firma = a.firma) " +
                                    " From stokfis a with (NOLOCK), stokfislines b with (NOLOCK), stok c with (NOLOCK) " +
                                    " Where a.stokfisno = b.stokfisno " +
                                    " And b.stokno = c.stokno " +
                                    " and (b.uyumid2 is null or b.uyumid2 = 0) " +
                                    cFilter + " ) w ) v " +
                    " ORDER by v.stokfisno, v.stokhareketno "

            dr = GetSQLReader(cSQL, Connyage)

            Do While dr.Read

                nAktarFiyat = 0
                nAktarKur = 0

                cDoviz = SQLReadString(dr, "dovizcinsi")
                nFiyat = SQLReadDouble(dr, "birimfiyat")
                nKur = SQLReadDouble(dr, "kur")
                nYerelKur = SQLReadDouble(dr, "yerelkur")
                cAktarDoviz = SQLReadString(dr, "aktardoviz")
                nAktarDovizKur = SQLReadDouble(dr, "aktardovizkur")

                If cAktarDoviz = cUyumYerelDoviz Then
                    nAktarKur = 1
                Else
                    nAktarKur = nAktarDovizKur / nYerelKur
                End If

                If cAktarDoviz = cDoviz Then
                    nAktarFiyat = nFiyat
                Else
                    nAktarFiyat = nFiyat * nKur / nAktarDovizKur
                End If

                cSQL = "UPDATE stokfislines " +
                        " SET aktardoviz = '" + SQLWriteString(cAktarDoviz, 3) + "', " +
                        " aktarfiyat = " + SQLWriteDecimal(nAktarFiyat) + ", " +
                        " aktarkur = " + SQLWriteDecimal(nAktarKur) +
                        " WHERE stokfisno = '" + SQLReadString(dr, "stokfisno") + "' " +
                        " and stokhareketno = " + SQLReadDouble(dr, "stokhareketno").ToString +
                        " and (uyumid2 is null or uyumid2 = 0) "

                ExecuteSQLCommand(cSQL)

            Loop
            dr.Close()

            ' uyumid kontrolleri

            cSQL = "select a.stokfisno, a.fistarihi, " +
                " a.belgeno, a.belgetarihi, " +
                " a.faturano, a.faturatarihi, a.firma, a.firma2, " +
                " a.transfered, a.transfereddate, a.donottransfer, " +
                " b.stokhareketkodu,  " +
                " c.anastokgrubu, c.stoktipi, " +
                " b.stokno, b.renk, b.beden, b.malzemetakipkodu, b.partino, b.depo, " +
                " b.netmiktar1, b.birim1, " +
                " b.aktarfiyat, b.aktarkur, b.aktardoviz, "

            cSQL = cSQL +
                " stokid = c.uyumid, " +
                " birimid = d.uyumid, " +
                " firmaid = e.uyumid, " +
                " dovizid = f.uyumid, " +
                " depoid = g.uyumid, " +
                " stoktipiid = h.uyumid, "

            cSQL = cSQL +
                " firma2id = (select top 1 uyumid " +
                            " from firma with (NOLOCK) " +
                            " where firma = a.firma2) "

            cSQL = cSQL +
                " from stokfis a with (NOLOCK) , " +
                " stokfislines b with (NOLOCK) , " +
                " stok c with (NOLOCK), " +
                " birim d with (NOLOCK) , " +
                " firma e with (NOLOCK) , " +
                " doviz f with (NOLOCK) , " +
                " depo g with (NOLOCK) , " +
                " stoktipi h with (NOLOCK) "

            cSQL = cSQL +
                " where a.stokfisno = b.stokfisno " +
                " And b.stokno = c.stokno " +
                " And b.birim1 = d.birim " +
                " And a.firma = e.firma " +
                " And b.aktardoviz = f.doviz " +
                " And b.depo = g.depo " +
                " And c.stoktipi = h.kod " +
                " And b.stokno Is Not null " +
                " And b.stokno <> '' " +
                " and (b.uyumid2 is null or b.uyumid2 = 0) " +
                cFilter

            dr = GetSQLReader(cSQL, Connyage)

            Do While dr.Read
                If SQLReadString(dr, "firma2") = "" Then
                    If SQLReadDouble(dr, "firmaid") = 0 Then
                        cError = "Firma (" + SQLReadString(dr, "firma") + ") uyum id bulunamadı"
                        If InStr(cSonuc, cError) = 0 Then
                            cSonuc = cSonuc + cError + vbCrLf
                        End If
                    End If
                Else
                    If SQLReadDouble(dr, "firma2id") = 0 Then
                        cError = "Firma (" + SQLReadString(dr, "firma2") + ") uyum id bulunamadı"
                        If InStr(cSonuc, cError) = 0 Then
                            cSonuc = cSonuc + cError + vbCrLf
                        End If
                    End If
                End If
                If SQLReadDouble(dr, "stokid") = 0 Then
                    cError = "Stok (" + SQLReadString(dr, "stokno") + ") uyum id bulunamadı"
                    If InStr(cSonuc, cError) = 0 Then
                        cSonuc = cSonuc + cError + vbCrLf
                    End If
                End If
                If SQLReadDouble(dr, "stoktipiid") = 0 Then
                    cError = "Stok tipi (" + SQLReadString(dr, "stoktipi") + ") uyum id bulunamadı"
                    If InStr(cSonuc, cError) = 0 Then
                        cSonuc = cSonuc + cError + vbCrLf
                    End If
                End If
                If SQLReadDouble(dr, "depoid") = 0 Then
                    cError = "Depo (" + SQLReadString(dr, "depo") + ") uyum id bulunamadı"
                    If InStr(cSonuc, cError) = 0 Then
                        cSonuc = cSonuc + cError + vbCrLf
                    End If
                End If
                If SQLReadDouble(dr, "birimid") = 0 Then
                    cError = "Birim (" + SQLReadString(dr, "birim1") + ") uyum id bulunamadı"
                    If InStr(cSonuc, cError) = 0 Then
                        cSonuc = cSonuc + cError + vbCrLf
                    End If
                End If
                If SQLReadDouble(dr, "dovizid") = 0 Then
                    cError = "Doviz (" + SQLReadString(dr, "aktardoviz") + ") uyum id bulunamadı"
                    If InStr(cSonuc, cError) = 0 Then
                        cSonuc = cSonuc + cError + vbCrLf
                    End If
                End If
            Loop
            dr.Close()

            Connyage2.Close()
            Connyage.Close()

            cMessage = cSonuc

            UyumStokFisCheck = (cSonuc.Trim = "")

        Catch ex As Exception
            ErrDisp("UyumStokFisCheck", cModuleName,,, ex)
        End Try
    End Function

    Public Function UyumStokFisSil(Optional cFilter As String = "", Optional ByRef cMessage As String = "") As Boolean

        Dim oService As UyumWebService.UyumWebService
        Dim oToken As UyumWebService.UyumToken
        Dim oRequest As UyumWebService.UyumServiceRequestOfObjectDeleteIn
        Dim cResult As String = ""

        Dim ConnYage As SqlConnection
        Dim dr As SqlDataReader
        Dim cSQL As String = ""
        Dim nID As Double = 0
        Dim cStokHareketNo As String = ""
        Dim nCnt As Integer = 0

        UyumStokFisSil = False

        Try
            cMessage = ""

            UyumStokFisSil = True

            cGUID = Guid.NewGuid.ToString
            WEBServisPerformans(cGUID, "UyumWebService", "ObjectDeleteIn", "stokfisi", cFilter)

            ConnYage = OpenConn()

            ' uyum tarafını sil

            For nCnt = 1 To 3

                ' nCnt = 1 fiili üretim firması     / uyumid
                ' nCnt = 2 resmi ihracat firması    / uyumid3
                ' nCnt = 3 resmi üretim firması     / uyumid2

                initUyumServices(nCnt, True)

                oService = New UyumWebService.UyumWebService
                oService.Url = oUyum.cURLUyumWebService

                oToken = New UyumWebService.UyumToken
                oToken.UserName = oUyum.cUserName
                oToken.Password = oUyum.cPassword

                oRequest = New UyumWebService.UyumServiceRequestOfObjectDeleteIn
                oRequest.Token = oToken
                oRequest.Value = New UyumWebService.ObjectDeleteIn
                oRequest.Value.ObjectCollectionTypeName = "INV.ItemMCollection,INV"

                cStokHareketNo = ""

                Select Case nCnt
                    Case 1
                        cSQL = "select distinct uid = b.uyumid " +
                                " from stokfis a with (NOLOCK), stokfislines b with (NOLOCK), stok c with (NOLOCK) " +
                                " where a.stokfisno = b.stokfisno " +
                                " and b.stokno = c.stokno " +
                                " and b.uyumid is not null " +
                                " and b.uyumid <> 0 " +
                                cFilter
                    Case 2
                        cSQL = "select distinct uid = b.uyumid3 " +
                                " from stokfis a with (NOLOCK), stokfislines b with (NOLOCK), stok c with (NOLOCK) " +
                                " where a.stokfisno = b.stokfisno " +
                                " And b.stokno = c.stokno " +
                                " and b.uyumid3 is not null " +
                                " and b.uyumid3 <> 0 " +
                                cFilter
                    Case 3
                        cSQL = "select distinct uid = b.uyumid2 " +
                                " from stokfis a with (NOLOCK), stokfislines b with (NOLOCK), stok c with (NOLOCK) " +
                                " where a.stokfisno = b.stokfisno " +
                                " And b.stokno = c.stokno " +
                                " and b.uyumid2 is not null " +
                                " and b.uyumid2 <> 0 " +
                                cFilter
                End Select

                dr = GetSQLReader(cSQL, ConnYage)

                Do While dr.Read

                    nID = SQLReadDouble(dr, "uid")

                    If nID <> 0 Then
                        oRequest.Value.Id = CInt(nID)
                        cResult = oService.DeleteObject(oRequest)
                        cMessage = cMessage.Trim + " " + cResult.Trim
                    End If
                Loop
                dr.Close()
            Next

            cSQL = "update stokfislines " +
                    " set uyumid = 0, " +
                    " uyumid2 = 0, " +
                    " uyumid3 = 0, " +
                    " transferred = 'H', " +
                    " transfereddate = '01.01.1950', " +
                    " uyumhareketkoduid = 0 " +
                    " where stokfisno in (Select a.stokfisno " +
                                        " from stokfis a With (NOLOCK), stokfislines b With (NOLOCK), stok c With (NOLOCK) " +
                                        " where a.stokfisno = b.stokfisno " +
                                        " and b.stokno = c.stokno " +
                                        cFilter + ") "
            ExecuteSQLCommand(cSQL)

            cSQL = "update stokfis " +
                    " Set transfered = 'H', " +
                    " transfereddate = '01.01.1950', " +
                    " kilitle = 'H', " +
                    " finanskilit = 'H' " +
                    " where stokfisno in (Select a.stokfisno " +
                                        " from stokfis a With (NOLOCK), stokfislines b With (NOLOCK), stok c With (NOLOCK) " +
                                        " where a.stokfisno = b.stokfisno " +
                                        " and b.stokno = c.stokno " +
                                        cFilter + ") "
            ExecuteSQLCommand(cSQL)

            ConnYage.Close()

            WEBServisPerformans(cGUID)

        Catch ex As Exception
            ErrDisp("UyumStokFisSil", cModuleName,,, ex)
        End Try
    End Function

    Private Function BeforeAfterProcesses(cFilter As String, nCase As Integer) As Boolean

        BeforeAfterProcesses = False

        Try
            Dim oSQL1 As New SQLServerClass
            Dim oSQL2 As New SQLServerClass
            Dim cStokFisNo As String = ""

            oSQL1.OpenConn()
            oSQL2.OpenConn()

            oSQL1.cSQLQuery = "select distinct a.stokfisno " +
                                " from stokfis a with (NOLOCK) , stokfislines b with (NOLOCK) , stok c with (NOLOCK) , firma d with (NOLOCK) " +
                                " where a.stokfisno = b.stokfisno " +
                                " And b.stokno = c.stokno " +
                                " And a.firma = d.firma " +
                                " And b.stokfisno Is Not null " +
                                " And b.stokfisno <> '' " +
                                cFilter

            oSQL1.GetSQLReader()

            Do While oSQL1.oReader.Read
                cStokFisNo = oSQL1.SQLReadString("stokfisno")
                If nCase = 1 Then
                    oSQL2.ExecuteStoredProcedure("uyum_stok_fisi_aktarim_oncesi", cStokFisNo)
                Else
                    oSQL2.ExecuteStoredProcedure("uyum_stok_fisi_aktarim_sonrasi", cStokFisNo)
                End If
                BeforeAfterProcesses = True
            Loop
            oSQL1.oReader.Close()

            oSQL1.CloseConn()
            oSQL2.CloseConn()

        Catch ex As Exception
            ErrDisp("BeforeAfterProcesses", cModuleName,,, ex)
        End Try
    End Function

    Public Function UyumStokFisAktar(Optional cFilter As String = "", Optional ByRef cMessage As String = "") As Boolean

        Dim ConnYage As SqlConnection
        Dim dr As SqlDataReader
        Dim ConnYage2 As SqlConnection
        Dim dr2 As SqlDataReader
        Dim cSQL As String = ""
        Dim cBelgeNo As String = ""
        Dim nLineNo As Integer = 0
        Dim nKDV As Double = 18
        Dim nKur As Decimal = 1
        Dim nFisSayac As Integer = 0
        Dim cStokHareketNo As String = ""
        Dim cDoviz As String = ""
        Dim cUyumHareketKodu As String = ""
        Dim lOK As Boolean = False
        Dim nVatID As Double = 0
        Dim cBuffer As String = ""
        Dim nFisAdedi As Integer = 0
        Dim cStokFisNo As String = ""
        Dim lAktarildi As Boolean = True

        Dim cuyumgithalat As String = ""
        Dim cuyumgalis As String = ""
        Dim cuyumgsatisiade As String = ""
        Dim cuyumgyigdsatisiade As String = ""
        Dim cuyumgyigisatisiade As String = ""
        Dim cuyumgstok As String = ""

        Dim cuyumcydgdsatis As String = ""
        Dim cuyumcydgisatis As String = ""
        Dim cuyumcydgisatis2 As String = ""
        Dim cuyumcyigdsatis As String = ""
        Dim cuyumcyigisatis As String = ""
        Dim cuyumcihracat As String = ""
        Dim cuyumcsatis As String = ""
        Dim cuyumcyialisiade As String = ""
        Dim cuyumcstok As String = ""
        Dim cuyumisevkiyattransfer As String = ""
        Dim cuyumasevkiyattransfer As String = ""
        Dim lPreProcess As Boolean = False
        Dim cFFirmaAktar As String = ""

        Dim nUyumHareketKoduId As Double = 0
        Dim nDovizId As Double = 0
        Dim cAktarDoviz As String = ""
        Dim nAktarKur As Double = 0

        UyumStokFisAktar = True
        cMessage = ""

        Try
            'cSQL = "select top 1 b.stokno " +
            '        " from stokfis a with (NOLOCK), stokfislines b with (NOLOCK), stok c with (NOLOCK) " +
            '        " where a.stokfisno = b.stokfisno " +
            '        " And b.stokno = c.stokno " +
            '        " And (c.uyumid Is null Or c.uyumid = 0) " +
            '        cFilter

            'If CheckExists(cSQL) Then
            '    cMessage = "Uyumid si olmayan stoklar var"
            '    UyumStokFisAktar = False
            '    Exit Function
            'End If

            'CheckStokCodes(cStokFisNo)

            lPreProcess = BeforeAfterProcesses(cFilter, 1)

            ConnYage = OpenConn()

            ' giris
            cuyumgithalat = GetSysParConnected("uyumgithalat", ConnYage, "1296")                   ' İTHALAT İRSALİYESİ
            cuyumgalis = GetSysParConnected("uyumgalis", ConnYage, "1292")                         ' ALIŞ İRSALİYESİ
            cuyumgsatisiade = GetSysParConnected("uyumgsatisiade", ConnYage, "1302")               ' SATIŞ İADE İRSALİYESİ
            cuyumgyigdsatisiade = GetSysParConnected("uyumgyigdsatisiade", ConnYage, "2879")       ' YURT İÇİ GRUP DIŞI SATIŞ İADE İRSALİYESİ
            cuyumgyigisatisiade = GetSysParConnected("uyumgyigisatisiade", ConnYage, "2878")       ' YURT İÇİ GRUP İÇİ SATIŞ İADE İRSALİYESİ
            cuyumgstok = GetSysParConnected("uyumgstok", ConnYage, "1330")                         ' STOK GİRİŞİ
            ' çıkış
            cuyumcydgdsatis = GetSysParConnected("uyumcydgdsatis", ConnYage, "2866")               ' YURT DIŞI GRUP DIŞI SATIŞ İRSALİYESİ
            cuyumcydgisatis = GetSysParConnected("uyumcydgisatis", ConnYage, "2865")               ' YURT DIŞI GRUP İÇİ SATIŞ İRSALİYESİ
            cuyumcydgisatis2 = "3043"
            cuyumcyigdsatis = GetSysParConnected("uyumcyigdsatis", ConnYage, "2864")               ' YURT İÇİ GRUP DIŞI SATIŞ İRSALİYESİ
            cuyumcyigisatis = GetSysParConnected("uyumcyigisatis", ConnYage, "2863")               ' YURT İÇİ GRUP İÇİ SATIŞ İRSALİYESİ
            cuyumcihracat = GetSysParConnected("uyumcihracat", ConnYage, "1303")                   ' İHRACAT İRSALİYESİ
            cuyumcsatis = GetSysParConnected("uyumcsatis", ConnYage, "1297")                       ' SATIŞ İRSALİYESİ
            cuyumcyialisiade = GetSysParConnected("uyumcyialisiade", ConnYage, "1308")             ' YURT İÇİ ALIŞ İADE İRSALİYESİ
            cuyumcstok = GetSysParConnected("uyumcstok", ConnYage, "1331")                         ' STOK ÇIKIŞI
            cuyumisevkiyattransfer = GetSysParConnected("uyumisevkiyattransfer", ConnYage, "3017") ' istanbul satış transfer 
            cuyumasevkiyattransfer = GetSysParConnected("uyumasevkiyattransfer", ConnYage, "3028") ' aksaray satış transfer 

            If Not lPreProcess Then

                cSQL = "select a.stokfisno, a.stokfistipi, a.firma, " +
                        " b.stokhareketno, b.malzemetakipkodu, b.stokhareketkodu, b.isemrino, " +
                        " b.stokno, b.renk, b.beden, " +
                        " d.grupfirmasi, d.yurtdisimusteri, d.uyumyurtdisisiparis, "

                cSQL = cSQL +
                    " uyumhareketkoduid = (select top 1 uyumid " +
                                        " from stokhareketkodu with (NOLOCK) " +
                                        " where kod = b.stokhareketkodu), "
                cSQL = cSQL +
                    " ithalat = (select top 1 x.ithalat " +
                                        " from isemri x with (NOLOCK) , isemrilines y with (NOLOCK) " +
                                        " where x.isemrino = y.isemrino " +
                                        " And y.isemrino = b.isemrino " +
                                        " And y.stokno = b.stokno " +
                                        " And y.renk = b.renk " +
                                        " And y.beden = b.beden) "
                cSQL = cSQL +
                    " from stokfis a with (NOLOCK) , " +
                    " stokfislines b with (NOLOCK) , " +
                    " stok c with (NOLOCK) , " +
                    " firma d with (NOLOCK) "

                cSQL = cSQL +
                    " where a.stokfisno = b.stokfisno " +
                    " And b.stokno = c.stokno " +
                    " And a.firma = d.firma " +
                    " And b.stokno Is Not null " +
                    " And b.stokno <> '' " +
                    " And (b.uyumid2 is null or b.uyumid2 = 0) " +
                    cFilter

                dr = GetSQLReader(cSQL, ConnYage)

                Do While dr.Read

                    cUyumHareketKodu = SQLReadDouble(dr, "uyumhareketkoduid").ToString

                    Select Case SQLReadString(dr, "stokfistipi")
                        Case "Giris"
                            Select Case SQLReadString(dr, "stokhareketkodu")
                                Case "01 Uretimden iade"
                                    cUyumHareketKodu = cuyumgsatisiade  ' SATIŞ İADE İRSALİYESİ
                                Case "02 Tedarikten Giris"
                                    If SQLReadString(dr, "ithalat") = "E" Then
                                        cUyumHareketKodu = cuyumgithalat ' İTHALAT İRSALİYESİ
                                    Else
                                        cUyumHareketKodu = cuyumgalis   ' ALIŞ İRSALİYESİ
                                    End If
                                Case "03 Mlz Uretimden iade"
                                    cUyumHareketKodu = cuyumgsatisiade  ' SATIŞ İADE İRSALİYESİ
                                Case "04 Mlz Uretimden Giris"
                                    cUyumHareketKodu = cuyumgalis       ' ALIŞ İRSALİYESİ
                                Case "05 Diger Giris"
                                    cUyumHareketKodu = cuyumgalis       ' ALIŞ İRSALİYESİ
                                Case "06 Tamirden Giris"
                                    cUyumHareketKodu = cuyumgalis       ' ALIŞ İRSALİYESİ
                                Case "07 Satis Iade"
                                    cUyumHareketKodu = cuyumgsatisiade  ' SATIŞ İADE İRSALİYESİ
                                Case "08 SAYIM GIRIS"
                                    cUyumHareketKodu = cuyumgalis       ' ALIŞ İRSALİYESİ
                                Case "11 Uretimden Giris"
                                    cUyumHareketKodu = cuyumgalis       ' ALIŞ İRSALİYESİ
                                Case "12 Konsinye Giris"
                                    cUyumHareketKodu = cuyumgalis       ' ALIŞ İRSALİYESİ
                                Case "13 Fason Giris"
                                    cUyumHareketKodu = cuyumgalis       ' ALIŞ İRSALİYESİ
                                Case "55 Kontrol Oncesi Giris"
                                    cUyumHareketKodu = cuyumgalis       ' ALIŞ İRSALİYESİ
                                Case "D1 Devir Giris"
                                    cUyumHareketKodu = cuyumgalis       ' ALIŞ İRSALİYESİ
                                Case Else
                                    cUyumHareketKodu = cuyumgalis       ' ALIŞ İRSALİYESİ
                            End Select
                        Case "Cikis"
                            Select Case SQLReadString(dr, "stokhareketkodu")
                                Case "01 Uretime Cikis"
                                    cUyumHareketKodu = cuyumcsatis          ' SATIŞ İRSALİYESİ
                                Case "02 Tedarikten iade"
                                    cUyumHareketKodu = cuyumcyialisiade     ' YURT İÇİ ALIŞ İADE İRSALİYESİ
                                Case "03 Mlz Uretim Cikis"
                                    cUyumHareketKodu = cuyumcsatis          ' SATIŞ İRSALİYESİ
                                Case "04 Mlz Uretime iade"
                                    cUyumHareketKodu = cuyumcsatis          ' SATIŞ İRSALİYESİ
                                Case "05 Diger Cikis"
                                    cUyumHareketKodu = cuyumcsatis          ' SATIŞ İRSALİYESİ
                                Case "06 Tamire Cikis"
                                    cUyumHareketKodu = cuyumcsatis          ' SATIŞ İRSALİYESİ
                                Case "07 Satis"
                                    cUyumHareketKodu = cuyumcsatis          ' SATIŞ İRSALİYESİ

                                    If SQLReadString(dr, "grupfirmasi") = "E" Then
                                        ' grup içi
                                        If SQLReadString(dr, "uyumyurtdisisiparis") = "E" Then
                                            ' yurt dışı
                                            cUyumHareketKodu = cuyumcydgisatis  ' YURT DIŞI GRUP İÇİ SATIŞ İRSALİYESİ
                                        Else
                                            ' yurt içi
                                            cUyumHareketKodu = cuyumcyigisatis  ' YURT İÇİ GRUP İÇİ SATIŞ İRSALİYESİ
                                        End If
                                    Else
                                        ' grup dışı
                                        If SQLReadString(dr, "uyumyurtdisisiparis") = "E" Then
                                            ' yurt dışı
                                            cUyumHareketKodu = cuyumcydgdsatis  ' YURT DIŞI GRUP DIŞI SATIŞ İRSALİYESİ
                                        Else
                                            ' yurt içi
                                            cUyumHareketKodu = cuyumcyigdsatis  ' YURT İÇİ GRUP DIŞI SATIŞ İRSALİYESİ
                                        End If
                                    End If
                                Case "08 SAYIM CIKIS"
                                    cUyumHareketKodu = cuyumcsatis          ' SATIŞ İRSALİYESİ
                                Case "11 UiD"
                                    cUyumHareketKodu = cuyumcsatis          ' SATIŞ İRSALİYESİ
                                Case "12 Konsinye Cikis"
                                    cUyumHareketKodu = cuyumcsatis          ' SATIŞ İRSALİYESİ
                                Case "13 Fason Cikis"
                                    cUyumHareketKodu = cuyumcsatis          ' SATIŞ İRSALİYESİ
                                Case "15 IMHA"
                                    cUyumHareketKodu = cuyumcsatis          ' SATIŞ İRSALİYESİ
                                Case "55 Kontrol Oncesi Cikis"
                                    cUyumHareketKodu = cuyumcsatis          ' SATIŞ İRSALİYESİ
                                Case "D1 Devir Cikis"
                                    cUyumHareketKodu = cuyumcsatis          ' SATIŞ İRSALİYESİ
                                Case Else
                                    cUyumHareketKodu = cuyumcsatis          ' SATIŞ İRSALİYESİ
                            End Select
                    End Select

                    If cUyumHareketKodu.Trim <> "" Then

                        lOK = True

                        cSQL = "update stokfislines " +
                            " set uyumhareketkoduid = " + cUyumHareketKodu +
                            " where stokhareketno = " + SQLReadDouble(dr, "stokhareketno").ToString

                        ExecuteSQLCommand(cSQL)
                    End If
                Loop
                dr.Close()

                If Not lOK Then
                    UyumStokFisAktar = False
                    ConnYage.Close()
                    cMessage = "Fiş aktarılmayacak olarak işaretli"
                    Exit Function
                End If
            End If

            ' 07 Satış fişlerinde ihracat siparişi açılacaksa uyum da ihracat siparişlerini aç
            ' satış fişlerinde MAMUL dışı ve ithalat ise sipariş açma ihtimali olabilir
            ' uyum da siparişten ihracat dosyası oluşturacaklar

            ' aşağıda otomatik açılacak siprişler uyumhareketkoduid alanını otomatik 10241024 olarak doldururlar

            ConnYage2 = OpenConn()

            cSQL = "select distinct a.stokfisno " +
                    " from stokfis a with (NOLOCK) , stokfislines b with (NOLOCK) , stok c with (NOLOCK) " +
                    " where a.stokfisno = b.stokfisno " +
                    " and b.stokno = c.stokno " +
                    " and (b.uyumid2 is null or b.uyumid2 = 0) " +
                    cFilter

            dr2 = GetSQLReader(cSQL, ConnYage2)

            Do While dr2.Read

                cStokFisNo = SQLReadString(dr2, "stokfisno")

                ' fiili üretim firmasına malzeme ihracat siparişi eklenecek mi
                UtilUyumsoftStokFisindenSatisSiparisiEkle(1, cStokFisNo, cFilter, cMessage)
                ' resmi ihracat firmasına malzeme ihracat siparişi eklenecek mi
                UtilUyumsoftStokFisindenSatisSiparisiEkle(2, cStokFisNo, cFilter, cMessage)

                ' mamul siparişinin satış fişindeysek ilgili siparişin id si
                ' stok fiş satırları hareket tipi, döviz cinsi ve fiyatlarına göre yeniden gruplanır
                nFisAdedi = 0

                cSQL = "select distinct a.stokfisno, a.faturatarihi, a.belgeno, a.stokfistipi, a.firma, " +
                        " b.aktardoviz, b.aktarkur, b.uyumhareketkoduid, "

                cSQL = cSQL +
                        " firmaid = e.uyumid, " +
                        " dovizid = d.uyumid "

                cSQL = cSQL +
                        " from stokfis a with (NOLOCK) , " +
                        " stokfislines b with (NOLOCK) , " +
                        " stok c with (NOLOCK) , " +
                        " doviz d with (NOLOCK) , " +
                        " firma e with (NOLOCK) "

                cSQL = cSQL +
                        " where a.stokfisno = b.stokfisno " +
                        " and b.stokno = c.stokno " +
                        " And a.firma = e.firma " +
                        " And b.aktardoviz = d.doviz " +
                        " and b.stokno is not null " +
                        " and b.stokno <> '' " +
                        " And a.stokfisno = '" + cStokFisNo.Trim + "' " +
                        " and (b.uyumid2 is null or b.uyumid2 = 0) " +
                        cFilter

                dr = GetSQLReader(cSQL, ConnYage)

                Do While dr.Read
                    nFisAdedi = nFisAdedi + 1
                Loop
                dr.Close()

                nFisSayac = 0

                cSQL = "select distinct a.stokfisno, a.faturatarihi, a.belgeno, a.stokfistipi, a.firma, " +
                        " b.aktardoviz, b.aktarkur, b.uyumhareketkoduid, "

                cSQL = cSQL +
                        " ffirmaaktar = (select top 1 ffirmaaktar " +
                                        " from firma with (NOLOCK) " +
                                        " where firma = a.firma), "
                cSQL = cSQL +
                        " firmaid = e.uyumid, " +
                        " dovizid = d.uyumid "

                cSQL = cSQL +
                        " from stokfis a with (NOLOCK) , " +
                        " stokfislines b with (NOLOCK) , " +
                        " stok c with (NOLOCK) , " +
                        " doviz d with (NOLOCK) , " +
                        " firma e with (NOLOCK) "

                cSQL = cSQL +
                        " where a.stokfisno = b.stokfisno " +
                        " and b.stokno = c.stokno " +
                        " And a.firma = e.firma " +
                        " And b.aktardoviz = d.doviz " +
                        " and b.stokno is not null " +
                        " and b.stokno <> '' " +
                        " And a.stokfisno = '" + cStokFisNo.Trim + "' " +
                        " and (b.uyumid2 is null or b.uyumid2 = 0) " +
                        cFilter

                cSQL = cSQL +
                        " order by b.aktardoviz, b.aktarkur, b.uyumhareketkoduid "

                dr = GetSQLReader(cSQL, ConnYage)

                Do While dr.Read

                    If nFisAdedi > 1 Then
                        nFisSayac = nFisSayac + 1
                    End If

                    lAktarildi = True
                    cBuffer = ""
                    cUyumHareketKodu = SQLReadDouble(dr, "uyumhareketkoduid").ToString.Trim
                    cFFirmaAktar = SQLReadString(dr, "ffirmaaktar")
                    nUyumHareketKoduId = SQLReadDouble(dr, "uyumhareketkoduid")
                    nDovizId = SQLReadDouble(dr, "dovizid")
                    cAktarDoviz = SQLReadString(dr, "aktardoviz")
                    nAktarKur = SQLReadDouble(dr, "aktarkur")

                    Select Case cUyumHareketKodu
                        Case cuyumisevkiyattransfer ' istanbul satış transfer 
                            lAktarildi = CreateIrsaliye(10, nFisSayac, cStokFisNo, nUyumHareketKoduId, nDovizId, cAktarDoviz, nAktarKur, cBuffer, cFilter)
                            cMessage = cMessage + cBuffer

                        Case cuyumasevkiyattransfer ' aksaray satış transfer 
                            lAktarildi = CreateIrsaliye(11, nFisSayac, cStokFisNo, nUyumHareketKoduId, nDovizId, cAktarDoviz, nAktarKur, cBuffer, cFilter)
                            cMessage = cMessage + cBuffer

                        Case cuyumcydgdsatis ' YURT DIŞI GRUP DIŞI SATIŞ İRSALİYESİ
                            If cFFirmaAktar = "E" Then
                                ' Fiili üretim firmasından müşteriye
                                lAktarildi = CreateIrsaliye(1, nFisSayac, cStokFisNo, nUyumHareketKoduId, nDovizId, cAktarDoviz, nAktarKur, cBuffer, cFilter)
                                cMessage = cMessage + cBuffer
                            Else
                                ' Fiili üretim firmasından müşteriye
                                lAktarildi = CreateIrsaliye(1, nFisSayac, cStokFisNo, nUyumHareketKoduId, nDovizId, cAktarDoviz, nAktarKur, cBuffer, cFilter)
                                cMessage = cMessage + cBuffer
                                If lAktarildi Then
                                    ' Resmi üretim firmasından resmi ihracat firmasına
                                    lAktarildi = CreateIrsaliye(3, nFisSayac, cStokFisNo, nUyumHareketKoduId, nDovizId, cAktarDoviz, nAktarKur, cBuffer, cFilter, True)
                                    cMessage = cMessage + cBuffer
                                End If
                                If lAktarildi Then
                                    ' Resmi ihracat firmasından müşteriye
                                    lAktarildi = CreateIrsaliye(2, nFisSayac, cStokFisNo, nUyumHareketKoduId, nDovizId, cAktarDoviz, nAktarKur, cBuffer, cFilter)
                                    cMessage = cMessage + cBuffer
                                End If
                            End If

                        Case cuyumcyigisatis ' YURT İÇİ GRUP İÇİ SATIŞ İRSALİYESİ
                            ' Fiili üretim firmasından müşteriye
                            lAktarildi = CreateIrsaliye(1, nFisSayac, cStokFisNo, nUyumHareketKoduId, nDovizId, cAktarDoviz, nAktarKur, cBuffer, cFilter)
                            cMessage = cMessage + cBuffer
                            'If lAktarildi Then
                            '    ' Resmi üretim firmasından müşteriye
                            '    lAktarildi = CreateIrsaliye(4, nFisSayac, cStokFisNo, SQLReadDouble(dr, "uyumhareketkoduid"), SQLReadDouble(dr, "dovizid"), SQLReadString(dr, "aktardoviz"), SQLReadDouble(dr, "aktarkur"), cBuffer, cFilter, True)
                            '    cMessage = cMessage + cBuffer
                            'End If

                        Case cuyumgithalat ' İTHALAT İRSALİYESİ
                            ' fiili üretim firmasına ithalat siparişine bağlı giriş kaydı
                            lAktarildi = CreateIrsaliye(5, nFisSayac, cStokFisNo, nUyumHareketKoduId, nDovizId, cAktarDoviz, nAktarKur, cBuffer, cFilter)
                            cMessage = cMessage + cBuffer
                            If lAktarildi Then
                                ' resmi üretim firmasına ithalat siparişine bağlı giriş kaydı
                                lAktarildi = CreateIrsaliye(6, nFisSayac, cStokFisNo, nUyumHareketKoduId, nDovizId, cAktarDoviz, nAktarKur, cBuffer, cFilter)
                                cMessage = cMessage + cBuffer
                            End If

                        Case cuyumcydgisatis, cuyumcydgisatis2 ' YURT DIŞI GRUP İÇİ SATIŞ İRSALİYESİ
                            ' fiili üretim firmasına mamül dışı ihracat siparişi
                            lAktarildi = CreateIrsaliye(7, nFisSayac, cStokFisNo, nUyumHareketKoduId, nDovizId, cAktarDoviz, nAktarKur, cBuffer, cFilter,, cUyumHareketKodu)
                            cMessage = cMessage + cBuffer
                            If lAktarildi Then
                                ' Resmi üretim firmasından resmi ihracat firmasına
                                lAktarildi = CreateIrsaliye(9, nFisSayac, cStokFisNo, nUyumHareketKoduId, nDovizId, cAktarDoviz, nAktarKur, cBuffer, cFilter,, cUyumHareketKodu)
                                cMessage = cMessage + cBuffer
                            End If
                            If lAktarildi Then
                                ' Resmi ihracat firmasından müşteriye
                                lAktarildi = CreateIrsaliye(8, nFisSayac, cStokFisNo, nUyumHareketKoduId, nDovizId, cAktarDoviz, nAktarKur, cBuffer, cFilter,, cUyumHareketKodu)
                                cMessage = cMessage + cBuffer
                            End If

                        Case Else ' diger
                            ' fiili üretim firmasına gönder
                            lAktarildi = CreateIrsaliye(99, nFisSayac, cStokFisNo, nUyumHareketKoduId, nDovizId, cAktarDoviz, nAktarKur, cBuffer, cFilter)
                            cMessage = cMessage + cBuffer
                    End Select

                    If Not lAktarildi Then
                        UyumStokFisSil(" and a.stokfisno = '" + cStokFisNo.Trim + "' ")
                    End If
                Loop
                dr.Close()
            Loop
            dr2.Close()

            ConnYage2.Close()
            ConnYage.Close()

            BeforeAfterProcesses(cFilter, 2)

            UyumStokFisAktar = True

        Catch ex As Exception
            UyumStokFisAktar = False
            ErrDisp("UyumStokEkle", cModuleName,,, ex)
        End Try
    End Function

    Private Function CreateIrsaliye(nCase As Integer, nFisSayac As Integer, cStokFisNo As String, nUyumHareketKoduid As Double, nDovizid As Double, cDoviz As String, nKur As Double,
                                     Optional ByRef cMessage As String = "", Optional cFilter As String = "", Optional lSevkCari As Boolean = False, Optional cDocTraId As String = "") As Boolean

        Dim oService As UyumsoftSaveWebService.UyumSaveWebService
        Dim oToken As UyumsoftSaveWebService.UyumToken
        Dim oResult As UyumsoftSaveWebService.ServiceResultOfBoolean
        Dim Inparam As UyumsoftSaveWebService.UyumServiceRequestOfItemDef
        Dim oFis As UyumsoftSaveWebService.ItemDef          ' INVT_ITEM_M tablosu
        Dim oSatir As UyumsoftSaveWebService.ItemDetailDef  ' INVT_ITEM_D tablosu
        Dim oSatirlar As List(Of UyumsoftSaveWebService.ItemDetailDef)

        Dim Connyage As SqlConnection
        Dim dr As SqlDataReader
        Dim cSQL As String = ""
        Dim cStokHareketNo As String = ""
        Dim nLineNo As Integer = 0
        Dim nKDV As Double = 0
        Dim nVatId As Double = 0
        Dim cStokFisTipi As String = ""
        Dim cPartiNoField As String = ""
        Dim cUyumIDFieldName As String = ""
        Dim cVoucherSerial As String = ""

        Dim cBelgeNo As String = ""
        Dim dFaturaTarihi As Date = #1/1/1950#
        Dim nFirmaid As Double = 0
        Dim nSevkCariid As Double = 0
        Dim nMode As Integer = 0
        Dim cuyumcyigisatis As String = ""
        Dim nDueDate As Double = 0
        Dim cVadeTipi As String = ""
        Dim cIthalatDosyaNo As String = ""
        Dim cIhracatDosyaNo As String = ""
        Dim cUyumUsername As String = ""
        Dim cUyumPassword As String = ""
        Dim cNotlar As String = ""
        Dim cHareketTipi As String = ""
        Dim cDepartman As String = ""
        Dim cMTF As String = ""
        Dim cPartiNo As String = ""
        Dim cUyumTransDepo As String = ""
        Dim cUyumTransFirma As String = ""

        CreateIrsaliye = True
        cMessage = ""

        Try
            GetUyumUserFromStokFis(cStokFisNo, cUyumUsername, cUyumPassword)

            Connyage = OpenConn()

            cUyumIDFieldName = GetUyumIDFieldName(nCase)

            cuyumcyigisatis = GetSysParConnected("uyumcyigisatis", Connyage, "2863")             ' YURT İÇİ GRUP İÇİ SATIŞ İRSALİYESİ

            If nCase = 10 Or nCase = 11 Then
                cUyumTransDepo = GetSysParConnected("UyumTransDepo", Connyage)
                cUyumTransFirma = GetSysParConnected("UyumTransFirma", Connyage)
            End If

            cHareketTipi = ""

            cSQL = "select distinct stokhareketkodu " +
                    " from stokfislines with (NOLOCK) " +
                    " where stokfisno = '" + cStokFisNo.Trim + "' " +
                    " and stokhareketkodu is not null " +
                    " and stokhareketkodu <> '' " +
                    " and uyumhareketkoduid = " + nUyumHareketKoduid.ToString

            dr = GetSQLReader(cSQL, Connyage)

            Do While dr.Read
                If cHareketTipi.Trim = "" Then
                    cHareketTipi = SQLReadString(dr, "stokhareketkodu")
                Else
                    If InStr(cHareketTipi, SQLReadString(dr, "stokhareketkodu")) = 0 Then
                        cHareketTipi = cHareketTipi + "," + SQLReadString(dr, "stokhareketkodu")
                    End If
                End If
            Loop
            dr.Close()

            cSQL = "select top 1 stokfistipi, belgeno, firma, departman, firma2, " +
                    " faturatarihi, ithalatdosyano, ihracatdosyano, notlar, "

            cSQL = cSQL +
                    " firmaid = (select top 1 uyumid " +
                                " from firma with (NOLOCK) " +
                                " where firma = stokfis.firma), "
            cSQL = cSQL +
                    " firma2id = (select top 1 uyumid " +
                                " from firma with (NOLOCK) " +
                                " where firma = stokfis.firma2), "
            cSQL = cSQL +
                    " sevkcariid = (select top 1 uyumid " +
                                " from firma with (NOLOCK) " +
                                " where firma = stokfis.sevkcarisi), "
            cSQL = cSQL +
                    " ddp1 = (select top 1 vadetipi " +
                                " from firma with (NOLOCK) " +
                                " where firma = stokfis.firma), "
            cSQL = cSQL +
                    " ddp2 = (select top 1 vadetipi " +
                                " from firma with (NOLOCK) " +
                                " where firma = stokfis.firma2) "
            cSQL = cSQL +
                    " from stokfis with (NOLOCK) " +
                    " where stokfisno = '" + cStokFisNo.Trim + "' "

            dr = GetSQLReader(cSQL, Connyage)

            If dr.Read Then
                cNotlar = SQLReadString(dr, "notlar")
                cStokFisTipi = SQLReadString(dr, "stokfistipi")
                cBelgeNo = SQLReadString(dr, "belgeno")
                dFaturaTarihi = SQLReadDate(dr, "faturatarihi")
                cIthalatDosyaNo = SQLReadString(dr, "ithalatdosyano")
                cIhracatDosyaNo = SQLReadString(dr, "ihracatdosyano")
                nSevkCariid = SQLReadDouble(dr, "sevkcariid")
                cDepartman = SQLReadString(dr, "departman")

                If SQLReadString(dr, "firma2") = "" Then
                    nFirmaid = SQLReadDouble(dr, "firmaid")
                    cVadeTipi = SQLReadString(dr, "ddp1")
                    If IsNumeric(cVadeTipi) Then
                        nDueDate = CDbl(cVadeTipi)
                    End If
                Else
                    nFirmaid = SQLReadDouble(dr, "firma2id")
                    cVadeTipi = SQLReadString(dr, "ddp2")
                    If IsNumeric(cVadeTipi) Then
                        nDueDate = CDbl(cVadeTipi)
                    End If
                End If
            End If
            dr.Close()

            If cStokFisTipi = "Giris" Then
                cPartiNoField = ""
            Else
                cPartiNoField = "w.partino,"
            End If

            Select Case nCase
                Case 1, 5, 7         ' fiili üretim firması Eroğlu
                    initUyumServices(1)
                    cVoucherSerial = "1"
                Case 2, 9            ' resmi ihracat firması Erpa
                    initUyumServices(2)
                    cVoucherSerial = "2"
                Case 3, 4, 6, 8, 99  ' resmi üretim firmasından resmi ihracat firmasına
                    initUyumServices(3)
                    cVoucherSerial = "3"
                Case 10, 11          ' fiili üretim firması Eroğlu
                    initUyumServices(1)
                    cVoucherSerial = "3"
            End Select

            oService = New UyumsoftSaveWebService.UyumSaveWebService
            oService.Timeout = 300000

            oService.Url = oUyum.cURLUyumSaveWebService

            oFis = New UyumsoftSaveWebService.ItemDef ' INVT_ITEM_M tablosu

            oFis.CoCode = oUyum.cCoCode
            oFis.BranchCode = oUyum.cBranchCode
            oFis.WhouseCode = oUyum.cWhouseCode
            oFis.DocTraId = CInt(nUyumHareketKoduid)
            oFis.DocDate = dFaturaTarihi
            oFis.ReceiptDate = dFaturaTarihi
            oFis.CurrencyOption = UyumsoftSaveWebService.CurrencyOption.Belge_Kuru
            oFis.VoucherSerial = cVoucherSerial
            oFis.VoucherNo = SQLWriteString(cBelgeNo, 15)

            If cUyumTransDepo.Trim <> "" Then
                oFis.WhouseId2 = CInt(cUyumTransDepo)
            End If

            oFis.Note1 = cStokFisNo.Trim
            oFis.Note2 = SQLWriteString(cNotlar, 100)
            oFis.Note3 = cBelgeNo.Trim

            oFis.GnlNote6 = cDepartman + " işlemi yaptırılmak üzere"
            oFis.GnlNote7 = cIthalatDosyaNo.Trim
            oFis.GnlNote8 = cIhracatDosyaNo.Trim
            oFis.GnlNote9 = cHareketTipi.Trim

            If cStokFisTipi = "Giris" And nDueDate > 0 Then
                oFis.DueDay = CInt(nDueDate)
                'oFis.DueDate = DateAdd(DateInterval.Day, nDueDate, dFaturaTarihi)
            End If

            If nFisSayac = 0 Then
                oFis.DocNo = cStokFisNo.Trim
            Else
                oFis.DocNo = cStokFisNo.Trim + "-" + nFisSayac.ToString
            End If

            If lSevkCari And nSevkCariid <> 0 Then
                oFis.ShippingEntityId = CInt(nSevkCariid)
            End If

            Select Case nCase
                Case 1, 7 ' fiili üretim firması müşteriye satış yapıyor / diğer işlemler
                    oFis.EntityId = CInt(nFirmaid)
                    oFis.CatCode1Id = 1001000001   ' bu fişi uyum to uyum transferi ile kopyalama
                Case 2, 9 ' resmi ihracat firması müşteriye satış yapıyor
                    oFis.EntityId = CInt(nFirmaid)
                    oFis.CatCode1Id = 1001000001   ' bu fişi uyum to uyum transferi ile kopyalama
                Case 3, 8 ' resmi üretim firması resmi ihracat firmasına satış yapıyor
                    oFis.EntityId = CInt(oUyum.cIhracatFirmaId)
                    oFis.CatCode1Id = 1001000001   ' bu fişi uyum to uyum transferi ile kopyalama
                    ' hareket kodu yurt içi grup içi olacak
                    If cDocTraId.Trim = "" Then
                        oFis.DocTraId = CInt(cuyumcyigisatis)
                    Else
                        oFis.DocTraId = CInt(cDocTraId)
                    End If
                Case 4 ' resmi üretim firması müşteriye satış yapıyor
                    oFis.EntityId = CInt(nFirmaid)
                    oFis.CatCode1Id = 1001000001   ' bu fişi uyum to uyum transferi ile kopyalama
                Case 5 ' fiili üretim firması ithalat siparişi 
                    oFis.EntityId = CInt(nFirmaid)
                    oFis.CatCode1Id = 1001000001   ' bu fişi uyum to uyum transferi ile kopyalama
                Case 6 ' resmi üretim firması ithalat siparişi 
                    oFis.EntityId = CInt(nFirmaid)
                    oFis.CatCode1Id = 1001000001   ' bu fişi uyum to uyum transferi ile kopyalama
                Case 10, 11
                    oFis.EntityId = CInt(cUyumTransFirma)
                    oFis.CatCode1Id = 1001000001   ' bu fişi uyum to uyum transferi ile kopyalama
                Case 99 ' fiili üretim firması diger
                    oFis.EntityId = CInt(nFirmaid)
            End Select

            oFis.CurRateTypeId = 235    ' 235 mb alış , 234 mb satış
            oFis.CurId = CInt(nDovizid) ' gnld_currency / döviz kodları
            oFis.CurTra = CDec(nKur)

            oFis.SourceApp = UyumsoftSaveWebService.SourceApplication.İrsaliye
            oFis.SourceApp2 = UyumsoftSaveWebService.SourceApplication.İrsaliye
            oFis.SourceApp3 = UyumsoftSaveWebService.SourceApplication.İrsaliye

            oFis.CountyId = 103 ' Türkiye
            oFis.IsWaybil = True

            oSatirlar = New List(Of UyumsoftSaveWebService.ItemDetailDef)

            For nMode = 1 To 2

                nLineNo = 0

                cSQL = GetSQL(nMode, cStokFisNo, cDoviz, nKur, nUyumHareketKoduid, cFilter, nCase)

                dr = GetSQLReader(cSQL, Connyage)

                Do While dr.Read

                    nLineNo = nLineNo + 10
                    ' satırları yaz
                    oSatir = New UyumsoftSaveWebService.ItemDetailDef  ' INVT_ITEM_D tablosu

                    oSatir.IsLotGenerate = True
                    oSatir.LineNo = nLineNo
                    oSatir.WhouseId = CInt(SQLReadDouble(dr, "depoid"))
                    oSatir.LineType = UyumsoftSaveWebService.LineType.S         ' stok
                    ' stok kodu
                    oSatir.DcardId = CInt(SQLReadDouble(dr, "stokid"))          ' INVD_ITEM        / stok kodu
                    oSatir.ItemNameManual = SQLReadString(dr, "cinsaciklamasi", 50)
                    ' notlar
                    'oSatir.Note1 = Mid(SQLReadString(dr, "anastokgrubu") + "/" + SQLReadString(dr, "stoktipi"), 1, 50).Trim
                    'oSatir.Note2 = Mid(SQLReadString(dr, "depo") + "/" + SQLReadString(dr, "isemrino"), 1, 50).Trim

                    oSatir.Note1 = IIf(SQLReadString(dr, "renk") = "HEPSI", "", SQLReadString(dr, "renk") + "/").ToString +
                                   IIf(SQLReadString(dr, "beden") = "HEPSI", "", SQLReadString(dr, "beden")).ToString

                    oSatir.Note2 = SQLReadString(dr, "stoktipi")

                    'If nMode = 1 Then
                    If SQLReadString(dr, "malzemetakipkodu") = "" Then
                        oSatir.LotCode = SQLReadString(dr, "partino")
                        oSatir.Note3 = SQLReadString(dr, "partino")
                    Else
                        oSatir.LotCode = SQLReadString(dr, "malzemetakipkodu")
                        oSatir.Note3 = SQLReadString(dr, "malzemetakipkodu")
                    End If
                    'Else
                    '    cMTF = GetMTFParti(1, cStokFisNo, cDoviz, nKur, nUyumHareketKoduid, cFilter, SQLReadString(dr, "stokno"), SQLReadString(dr, "renk"), SQLReadString(dr, "beden"))
                    '    cPartiNo = ""
                    '    If cMTF.Trim = "" Then
                    '        cPartiNo = GetMTFParti(2, cStokFisNo, cDoviz, nKur, nUyumHareketKoduid, cFilter, SQLReadString(dr, "stokno"), SQLReadString(dr, "renk"), SQLReadString(dr, "beden"))
                    '        oSatir.LotCode = cPartiNo
                    '        oSatir.Note3 = cPartiNo
                    '    Else
                    '        oSatir.LotCode = cMTF
                    '        oSatir.Note3 = cMTF
                    '    End If
                    'End If

                    oSatir.NoteLarge = cHareketTipi.Trim
                    ' fiyat
                    oSatir.UnitPriceTra = CDec(SQLReadDouble(dr, "aktarfiyat"))
                    ' kur
                    oSatir.CurRateTypeId = 235         ' 235 mb alış , 234 mb satış
                    oSatir.CurTraId = CInt(nDovizid)   ' gnld_currency / döviz kodları
                    oSatir.CurRateTra = CDec(nKur)
                    ' kdv
                    nKDV = SQLReadDouble(dr, "kdv")
                    nVatId = SQLReadDouble(dr, "vatid")
                    If nVatId = 0 Then
                        ' no show
                    Else
                        oSatir.VatId = CInt(nVatId)
                    End If
                    ' kdv hariç
                    oSatir.VatStatus = UyumsoftSaveWebService.VatStatus.Hariç
                    ' miktar
                    oSatir.Qty = CDec(SQLReadDouble(dr, "netmiktar1"))
                    oSatir.UnitId = CInt(SQLReadDouble(dr, "birimid"))          ' INVD_UNIT / birimler 

                    ' ihracat siparisi id
                    'Select Case nCase
                    '    Case 1, 5, 7, 99    ' fiili üretim firması Eroğlu
                    '        If SQLReadDouble(dr, "uyumsiparisid") <> 0 Then
                    '            oSatir.SourceMId = CInt(SQLReadDouble(dr, "uyumsiparisid"))
                    '        End If
                    '    Case 2, 9           ' resmi ihracat firması Erpa
                    '        oSatir.SourceMId = CInt(SQLReadDouble(dr, "uyumsiparisid2"))
                    '    Case 3, 4, 6, 8     ' resmi üretim firmasından resmi ihracat firmasına
                    '        oSatir.SourceMId = CInt(SQLReadDouble(dr, "uyumsiparisid2"))
                    'End Select

                    ' satınalma siparişi / ithalat sipariş bağlantısı girişi
                    Select Case nCase
                        Case 1, 2, 3, 4
                            ' satış siparişi girişi
                            If SQLReadString(dr, "anastokgrubu") = "MAMUL" Then
                                Select Case nCase
                                    Case 1 ' fiili üretim firması müşteriye satış yapıyor / diğer işlemler
                                        If SQLReadDouble(dr, "uyumsiparisid") <> 0 Then
                                            ' ihracat mamül siparişi
                                            oSatir.SourceMId = CInt(SQLReadDouble(dr, "uyumsiparisid"))
                                            oFis.SourceApp = UyumsoftSaveWebService.SourceApplication.SatışSiparişi
                                            'oFis.SourceApp = UyumsoftSaveWebService.SourceApplication.İrsaliye
                                        End If
                                    Case 2 ' resmi ihracat firması müşteriye satış yapıyor
                                        If SQLReadDouble(dr, "uyumsiparisid2") <> 0 Then
                                            ' ihracat mamül siparişi
                                            oSatir.SourceMId = CInt(SQLReadDouble(dr, "uyumsiparisid2"))
                                            'oFis.SourceApp = UyumsoftSaveWebService.SourceApplication.SatışSiparişi
                                            oFis.SourceApp = UyumsoftSaveWebService.SourceApplication.İrsaliye
                                        End If
                                    Case 3 ' resmi üretim firması resmi ihracat firmasına satış yapıyor 
                                    ' bu durumun satış siparişi açılmıyor
                                    Case 4 ' resmi üretim firması müşteriye satış yapıyor
                                        If SQLReadDouble(dr, "uyumsiparisid2") <> 0 Then
                                            ' ihracat mamül siparişi
                                            oSatir.SourceMId = CInt(SQLReadDouble(dr, "uyumsiparisid2"))
                                            'oFis.SourceApp = UyumsoftSaveWebService.SourceApplication.SatışSiparişi
                                            oFis.SourceApp = UyumsoftSaveWebService.SourceApplication.İrsaliye
                                        End If
                                End Select
                            End If

                        Case 5 ' fiili üretim firması
                            If SQLReadDouble(dr, "isemriuyumid") <> 0 Then
                                oSatir.SourceMId = CInt(SQLReadDouble(dr, "isemriuyumid"))
                                oFis.SourceApp = UyumsoftSaveWebService.SourceApplication.SatınalmaSiparişi
                            End If

                        Case 6 ' resmi üretim firması
                            If SQLReadDouble(dr, "isemriuyumid2") <> 0 Then
                                oSatir.SourceMId = CInt(SQLReadDouble(dr, "isemriuyumid2"))
                                oFis.SourceApp = UyumsoftSaveWebService.SourceApplication.SatınalmaSiparişi
                            End If

                        Case 7
                            If SQLReadDouble(dr, "uyumsipid") <> 0 Then
                                oSatir.SourceMId = CInt(SQLReadDouble(dr, "uyumsipid"))
                                'oFis.SourceApp = UyumsoftSaveWebService.SourceApplication.SatışSiparişi
                                oFis.SourceApp = UyumsoftSaveWebService.SourceApplication.İrsaliye
                            End If

                        Case 9
                            If SQLReadDouble(dr, "uyumsipid2") <> 0 Then
                                oSatir.SourceMId = CInt(SQLReadDouble(dr, "uyumsipid2"))
                                'oFis.SourceApp = UyumsoftSaveWebService.SourceApplication.SatışSiparişi
                                oFis.SourceApp = UyumsoftSaveWebService.SourceApplication.İrsaliye
                            End If

                    End Select
                    ' satırlara yaz
                    oSatirlar.Add(oSatir)
                Loop

                dr.Close()
            Next

            ' update edilecek stokfislines id leri
            cStokHareketNo = ""

            cSQL = "select distinct b.stokhareketno "

            cSQL = cSQL +
                    " from stokfis a with (NOLOCK) , " +
                    " stokfislines b with (NOLOCK) , " +
                    " stok c with (NOLOCK) , " +
                    " birim d with (NOLOCK) , " +
                    " firma e with (NOLOCK) , " +
                    " doviz f with (NOLOCK) , " +
                    " depo g with (NOLOCK) , " +
                    " stoktipi h with (NOLOCK) "

            cSQL = cSQL +
                    " where a.stokfisno = b.stokfisno " +
                    " and b.stokno = c.stokno " +
                    " and b.birim1 = d.birim " +
                    " and a.firma = e.firma " +
                    " and b.aktardoviz = f.doviz " +
                    " and b.depo = g.depo " +
                    " and c.stoktipi = h.kod " +
                    " and b.stokno is not null " +
                    " and b.stokno <> '' " +
                    " and a.stokfisno = '" + cStokFisNo.Trim + "' " +
                    " and b.aktardoviz = '" + cDoviz.Trim + "' " +
                    " and b.aktarkur = " + SQLWriteDecimal(nKur) + " " +
                    " and b.uyumhareketkoduid = " + nUyumHareketKoduid.ToString + " " +
                    " and (b." + cUyumIDFieldName + " is null or b." + cUyumIDFieldName + " = 0) " +
                    cFilter

            dr = GetSQLReader(cSQL, Connyage)

            Do While dr.Read
                ' update listesi olustur
                If cStokHareketNo.Trim = "" Then
                    cStokHareketNo = "'" + SQLReadDouble(dr, "stokhareketno").ToString + "'"
                Else
                    cStokHareketNo = cStokHareketNo + ",'" + SQLReadDouble(dr, "stokhareketno").ToString + "'"
                End If
            Loop
            dr.Close()

            ' fiş detayını oluştur
            oFis.Details = oSatirlar.ToArray()

            cGUID = Guid.NewGuid.ToString
            WEBServisPerformans(cGUID, "UyumsoftSaveWebService", "SaveWaybill", "stokfisi", cStokFisNo)

            ' fişi oluştur
            oToken = New UyumsoftSaveWebService.UyumToken

            oToken.UserName = cUyumUsername  ' oUyum.cUserName
            oToken.Password = cUyumPassword  ' oUyum.cPassword

            Inparam = New UyumsoftSaveWebService.UyumServiceRequestOfItemDef

            Inparam.Token = New UyumsoftSaveWebService.UyumServiceRequestOfItemDef().Token
            Inparam.Token = oToken
            Inparam.Value = oFis

            ' irsaliyeyi yaz
            oResult = New UyumsoftSaveWebService.ServiceResultOfBoolean

            oResult = oService.SaveWaybill(Inparam)

            WEBServisPerformans(cGUID)

            If IsNumeric(oResult.Message) Then
                CreateIrsaliye = True
                UpdateStokFisLines(nCase, cStokFisNo, cStokHareketNo, oResult.Message.ToString)
                CreateLog("UyumStokFisi", "Uyum Servis : " + oService.Url + vbCrLf +
                                          "StokFisNo / StokHareketNo : " + cStokFisNo + " / " + cStokHareketNo + " -> " + nCase.ToString + " - " + oResult.Message.ToString)
            Else
                CreateIrsaliye = False
                cMessage = oResult.Message.ToString
                CreateLog("UyumStokFisiHata", "Uyum Servis : " + oService.Url + vbCrLf +
                                              "StokFisNo / StokHareketNo : " + cStokFisNo + " / " + cStokHareketNo + " -> " + nCase.ToString + " - " + oResult.Message.ToString + vbCrLf +
                                              "Uyum username :  " + cUyumUsername + vbCrLf +
                                              "Uyum password : " + cUyumPassword + vbCrLf)
            End If

            Connyage.Close()
            oService.Dispose()


        Catch ex As Exception
            ErrDisp("CreateIrsaliye", cModuleName,,, ex)
            CreateIrsaliye = False
        End Try
    End Function

    Private Function GetMTFParti(nCase As Integer, cStokFisNo As String, cDoviz As String, nKur As Double, nUyumHareketKoduid As Double, cFilter As String,
                                 cStokNo As String, cRenk As String, cBeden As String) As String

        GetMTFParti = ""

        Try
            Dim Connyage As SqlConnection
            Dim dr As SqlDataReader
            Dim cSQL As String = ""
            Dim cBuffer As String = ""

            Connyage = OpenConn()

            If nCase = 1 Then
                cSQL = "select distinct b.malzemetakipkodu "
            Else
                cSQL = "select distinct b.partino "
            End If

            cSQL = cSQL +
                        " from stokfis a with (NOLOCK) , " +
                        " stokfislines b with (NOLOCK) , " +
                        " stok c with (NOLOCK) , " +
                        " birim d with (NOLOCK) , " +
                        " firma e with (NOLOCK) , " +
                        " doviz f with (NOLOCK) , " +
                        " depo g with (NOLOCK) , " +
                        " stoktipi h with (NOLOCK) "

            cSQL = cSQL +
                        " where a.stokfisno = b.stokfisno " +
                        " And b.stokno = c.stokno " +
                        " And b.birim1 = d.birim " +
                        " And a.firma = e.firma " +
                        " And b.aktardoviz = f.doviz " +
                        " And b.depo = g.depo " +
                        " And c.stoktipi = h.kod " +
                        " And b.stokno = '" + cStokNo + "' " +
                        " and b.renk = '" + cRenk + "' " +
                        " and b.beden = '" + cBeden + "' " +
                        " and a.stokfisno = '" + cStokFisNo.Trim + "' " +
                        " and b.aktardoviz = '" + cDoviz.Trim + "' " +
                        " and b.aktarkur = " + SQLWriteDecimal(nKur) + " " +
                        " and b.uyumhareketkoduid = " + nUyumHareketKoduid.ToString + " " +
                        " and (b.uyumid2 is null or b.uyumid2 = 0) " +
                        " and b.stokhareketkodu not in ('07 Satis','07 Satis Iade') " +
                        cFilter

            If nCase = 1 Then
                cSQL = cSQL +
                        " and b.malzemetakipkodu is not null " +
                        " and b.malzemetakipkodu <> '' "
            Else
                cSQL = cSQL +
                        " and b.partino is not null " +
                        " and b.partino <> '' "
            End If

            dr = GetSQLReader(cSQL, Connyage)

            Do While dr.Read
                If nCase = 1 Then
                    If cBuffer.Trim = "" Then
                        cBuffer = SQLReadString(dr, "malzemetakipkodu")
                    Else
                        If InStr(cBuffer, SQLReadString(dr, "malzemetakipkodu")) = 0 Then
                            cBuffer = cBuffer + "," + SQLReadString(dr, "malzemetakipkodu")
                        End If
                    End If
                Else
                    If cBuffer.Trim = "" Then
                        cBuffer = SQLReadString(dr, "partino")
                    Else
                        If InStr(cBuffer, SQLReadString(dr, "partino")) = 0 Then
                            cBuffer = cBuffer + "," + SQLReadString(dr, "partino")
                        End If
                    End If
                End If
            Loop
            dr.Close()

            Connyage.Close()

            GetMTFParti = cBuffer.Trim

        Catch ex As Exception
            ErrDisp("GetMTFParti", cModuleName,,, ex)
        End Try
    End Function

    Private Function GetUyumIDFieldName(nCase As Integer) As String

        GetUyumIDFieldName = ""

        Select Case nCase
            Case 1, 5, 7, 10, 11   ' fiili üretim firması
                GetUyumIDFieldName = "uyumid"
            Case 2, 9              ' resmi ihracat firması
                GetUyumIDFieldName = "uyumid3"
            Case 3, 4, 6, 8, 99     ' resmi üretim firması
                GetUyumIDFieldName = "uyumid2"
        End Select
    End Function

    Private Sub UpdateStokFisLines(nCase As Integer, Optional cStokFisNo As String = "", Optional cStokHareketNo As String = "", Optional cUyumID As String = "", Optional lAdd As Boolean = True)

        Dim cSQL As String = ""
        Dim cUyumIdFieldName As String = ""

        Try
            If cStokFisNo.Trim = "" And cStokHareketNo.Trim = "" Then
                Exit Sub
            End If

            cUyumIdFieldName = GetUyumIDFieldName(nCase)

            If lAdd Then
                cSQL = "update stokfis " +
                        " set transfered = 'E', " +
                        " transfereddate = getdate(), " +
                        " kilitle = 'E' " +
                        " where stokfisno is not null "

                If cStokFisNo.Trim <> "" Then
                    cSQL = cSQL +
                        " and stokfisno = '" + cStokFisNo.Trim + "' "
                End If

                If cStokHareketNo.Trim <> "" Then
                    cSQL = cSQL +
                        " and stokfisno in (select stokfisno " +
                                            " from stokfislines with (NOLOCK) " +
                                            " where stokhareketno in (" + cStokHareketNo.Trim + ")) "
                End If

                ExecuteSQLCommand(cSQL)

                cSQL = "update stokfislines " +
                        " set " + cUyumIdFieldName + " = " + cUyumID + ", " +
                        " transferred = 'E', " +
                        " transfereddate = getdate() " +
                        " where stokfisno is not null "

                If cStokFisNo.Trim <> "" Then
                    cSQL = cSQL +
                        " and stokfisno = '" + cStokFisNo.Trim + "' "
                End If

                If cStokHareketNo.Trim <> "" Then
                    cSQL = cSQL +
                        " and stokhareketno in (" + cStokHareketNo.Trim + ") "
                End If

                ExecuteSQLCommand(cSQL)

            Else
                cSQL = "update stokfis " +
                        " set transfered = 'H', " +
                        " transfereddate = '01.01.1950', " +
                        " kilitle = 'H', " +
                        " finanskilit = 'H' " +
                        " where stokfisno is not null "

                If cStokFisNo.Trim <> "" Then
                    cSQL = cSQL +
                        " and stokfisno = '" + cStokFisNo.Trim + "' "
                End If

                If cStokHareketNo.Trim <> "" Then
                    cSQL = cSQL +
                        " and stokfisno in (select stokfisno " +
                                            " from stokfislines with (NOLOCK) " +
                                            " where stokhareketno in (" + cStokHareketNo.Trim + ")) "
                End If

                ExecuteSQLCommand(cSQL)

                cSQL = "update stokfislines " +
                        " set uyumid = 0, " +
                        " uyumid2 = 0, " +
                        " uyumid3 = 0, " +
                        " transferred = 'H', " +
                        " transfereddate = '01.01.1950' " +
                        " where stokfisno is not null "

                If cStokFisNo.Trim <> "" Then
                    cSQL = cSQL +
                        " and stokfisno = '" + cStokFisNo.Trim + "' "
                End If

                If cStokHareketNo.Trim <> "" Then
                    cSQL = cSQL +
                        " and stokhareketno in (" + cStokHareketNo.Trim + ") "
                End If

                ExecuteSQLCommand(cSQL)
            End If

        Catch ex As Exception
            ErrDisp("UpdateStokFisLines", cModuleName,,, ex)
        End Try
    End Sub

    Private Sub CheckStokCodes(cStokFisNo As String)
        ' önce eksik stok kartlarını açmaya çalış
        Dim cSQL As String = ""
        Dim dr As SqlDataReader
        Dim cSonuc As String = ""
        Dim cStokNo As String
        Dim cAciklama As String
        Dim cUyumStokID As String = ""
        Dim cUyumBirimID As String = ""
        Dim cUyumStokTipiID As String = ""
        Dim cError As String = ""
        Dim cAnaStokGrubu As String = ""
        Dim cGtipID As String = ""
        Dim cKdvID As String = ""
        Dim cMuhasebeID As String = ""
        Dim cMessage2 As String = ""
        Dim cAciklama2 As String = ""
        Dim Connyage As SqlConnection

        Try
            Connyage = OpenConn()

            cSQL = "select distinct x.stokno, y.cinsaciklamasi, y.birim1, y.anastokgrubu, "

            cSQL = cSQL +
                    " aciklama2 = LTrim(case when y.anastokgrubu = 'MAMUL'  " +
                                " then rtrim(coalesce((select top 1 coalesce(k.karisim,'') + ' ' + coalesce(m.sex,'')  " +
                                        " From siparis k with (NOLOCK) , sipmodel l with (NOLOCK) , ymodel m with (NoLOCK)  " +
                                        " Where k.kullanicisipno = l.siparisno " +
                                        " and l.modelno = m.modelno " +
                                        " and l.modelno = y.stokno " +
                                        " order by k.bilgisayarsipno desc),'')) + ' ' + y.stoktipi " +
                                " Else y.kompozisyon " +
                                " End), "
            cSQL = cSQL +
                    " birimid = z.uyumid, " +
                    " stoktipiid = q.uyumid, " +
                    " gtipid = (select top 1 uyumid " +
                                " from gtip with (NOLOCK) " +
                                " where gtip = y.gtip), " +
                    " kdvid = (select top 1 uyumid " +
                                " from kdvgroup with (NOLOCK) " +
                                " where oran = y.kdv ), " +
                    " muhasebeid = q.uyummuhsablonid "

            cSQL = cSQL +
                    " from stokfislines x with (NOLOCK) , " +
                    " stok y with (NOLOCK) , " +
                    " birim z with (NOLOCK) , " +
                    " stoktipi q with (NOLOCK) "

            cSQL = cSQL +
                    " where x.stokno = y.stokno " +
                    " and y.birim1 = z.birim " +
                    " and y.stoktipi = q.kod " +
                    " and x.stokno Is Not null " +
                    " and x.stokno <> '' " +
                    " and (y.uyumid is null or y.uyumid = 0) " +
                    " and z.uyumid is not null " +
                    " and z.uyumid <> 0 " +
                    " and q.uyumid is not null " +
                    " and q.uyumid <> 0 " +
                    " and x.stokfisno = '" + cStokFisNo.Trim + "' "

            dr = GetSQLReader(cSQL, Connyage)

            Do While dr.Read
                cStokNo = SQLReadString(dr, "stokno")
                cAciklama = SQLReadString(dr, "cinsaciklamasi")
                cUyumBirimID = CStr(SQLReadDouble(dr, "birimid"))
                cUyumStokTipiID = CStr(SQLReadDouble(dr, "stoktipiid"))
                cAnaStokGrubu = SQLReadString(dr, "anastokgrubu")
                cGtipID = CStr(SQLReadDouble(dr, "gtipid"))
                cKdvID = CStr(SQLReadDouble(dr, "kdvid"))
                cMuhasebeID = CStr(SQLReadDouble(dr, "muhasebeid"))
                cAciklama2 = SQLReadString(dr, "aciklama2")

                cUyumStokID = UyumStokInsert(cStokNo, cAciklama, cUyumBirimID, cUyumStokTipiID, cAnaStokGrubu, cMessage2, cGtipID, cKdvID, cMuhasebeID, cAciklama2)
            Loop
            dr.Close()

            Connyage.Close()

        Catch ex As Exception
            ErrDisp("CheckStokCodes", cModuleName,,, ex)
        End Try
    End Sub

    Private Function GetSQL(nMode As Integer, cStokFisNo As String, cDoviz As String, nKur As Double, nUyumHareketKoduid As Double, cFilter As String, nCase As Integer) As String
        ' nMode = 1 , 02 Tedarikten Giris   Hareketlerinde renk ve beden istenmiyor        
        ' nMode = 2 , normal mod
        Dim cSQL As String = ""
        Dim cUyumIDFieldName As String = ""

        GetSQL = ""

        Try
            cUyumIDFieldName = GetUyumIDFieldName(nCase)

            cSQL = "SELECT w.stokfisno, w.stokfistipi, w.cinsaciklamasi, w.anastokgrubu, w.stoktipi, " +
                    " w.isemrino, w.stokno, w.renk, w.beden, w.depo,  w.uyumsipid, " +
                    " w.uyumsipid2, w.birim1, w.aktarfiyat, w.aktarkur, w.aktardoviz, " +
                    " w.kdv, w.vatid, w.isemriuyumid, w.isemriuyumid2, w.uyumsiparisid, " +
                    " w.uyumsiparisid2, w.stokid, w.birimid, w.firmaid, w.dovizid, " +
                    " w.depoid, w.stoktipiid, w.malzemetakipkodu , w.partino, " +
                    " netmiktar1 = SUM(COALESCE(netmiktar1,0)) "

            cSQL = cSQL +
                    " FROM ( " +
                        " select b.stokhareketno, a.stokfisno, a.stokfistipi, " +
                        " c.cinsaciklamasi, c.anastokgrubu, c.stoktipi, " +
                        " b.isemrino, b.stokno, b.malzemetakipkodu, b.partino, b.depo, " +
                        " b.uyumsipid, b.uyumsipid2, " +
                        " b.netmiktar1, b.birim1, " +
                        " b.aktarfiyat, b.aktarkur, b.aktardoviz, " +
                        " c.kdv, "

            If nMode = 1 Then
                cSQL = cSQL +
                        " renk = 'HEPSI', " +
                        " beden = 'HEPSI', "
            Else
                cSQL = cSQL +
                        " b.renk, " +
                        " b.beden, "
            End If

            ' ithalat irsaliyesi bağlantısı / fiili üretim firması referansı
            cSQL = cSQL +
                        " isemriuyumid = (select top 1 x.uyumid " +
                                    " from isemri x with (NOLOCK) , isemrilines y with (NOLOCK) " +
                                    " where x.isemrino = y.isemrino " +
                                    " And y.isemrino = b.isemrino " +
                                    " And y.stokno = b.stokno " +
                                    " And y.renk = b.renk " +
                                    " And y.beden = b.beden), "
            ' ithalat irsaliyesi bağlantısı / resmi üretim firması referansı
            cSQL = cSQL +
                        " isemriuyumid2 = (select top 1 x.uyumid2 " +
                                    " from isemri x with (NOLOCK) , isemrilines y with (NOLOCK) " +
                                    " where x.isemrino = y.isemrino " +
                                    " And y.isemrino = b.isemrino " +
                                    " And y.stokno = b.stokno " +
                                    " And y.renk = b.renk " +
                                    " And y.beden = b.beden), "
            cSQL = cSQL +
                        " uyumsiparisid = (select top 1 x.uyumsiparisid " +
                                    " from siparis x with (NOLOCK) , sipmodel y with (NOLOCK) " +
                                    " where x.kullanicisipno = y.siparisno " +
                                    " And x.kullanicisipno = b.partino), "
            cSQL = cSQL +
                        " uyumsiparisid2 = (select top 1 x.uyumsiparisid2 " +
                                    " from siparis x with (NOLOCK) , sipmodel y with (NOLOCK) " +
                                    " where x.kullanicisipno = y.siparisno " +
                                    " And x.kullanicisipno = b.partino), "
            cSQL = cSQL +
                        " vatid = (select top 1 uyumid " +
                                    " from kdvgroup with (NOLOCK) " +
                                    " where oran = c.kdv), "
            cSQL = cSQL +
                        " stokid = c.uyumid, " +
                        " birimid = d.uyumid, " +
                        " firmaid = e.uyumid, " +
                        " dovizid = f.uyumid, " +
                        " depoid = g.uyumid, " +
                        " stoktipiid = h.uyumid "

            cSQL = cSQL +
                        " from stokfis a with (NOLOCK) , " +
                        " stokfislines b with (NOLOCK) , " +
                        " stok c with (NOLOCK) , " +
                        " birim d with (NOLOCK) , " +
                        " firma e with (NOLOCK) , " +
                        " doviz f with (NOLOCK) , " +
                        " depo g with (NOLOCK) , " +
                        " stoktipi h with (NOLOCK) "

            cSQL = cSQL +
                        " where a.stokfisno = b.stokfisno " +
                        " And b.stokno = c.stokno " +
                        " And b.birim1 = d.birim " +
                        " And a.firma = e.firma " +
                        " And b.aktardoviz = f.doviz " +
                        " And b.depo = g.depo " +
                        " And c.stoktipi = h.kod " +
                        " And b.stokno Is Not null " +
                        " And b.stokno <> '' " +
                        " And a.stokfisno = '" + cStokFisNo.Trim + "' " +
                        " And b.aktardoviz = '" + cDoviz.Trim + "' " +
                        " And b.aktarkur = " + SQLWriteDecimal(nKur) + " " +
                        " And b.uyumhareketkoduid = " + nUyumHareketKoduid.ToString + " " +
                        " and (b." + cUyumIDFieldName + " is null or b." + cUyumIDFieldName + " = 0)  " +
                        cFilter

            If nMode = 1 Then
                cSQL = cSQL +
                        " and b.stokhareketkodu = '02 Tedarikten Giris' "
            Else
                cSQL = cSQL +
                        " and b.stokhareketkodu <> '02 Tedarikten Giris' "
            End If

            cSQL = cSQL +
                    " ) w " +
                    " group by w.stokfisno, w.stokfistipi,  w.cinsaciklamasi, w.anastokgrubu, w.stoktipi, " +
                    " w.isemrino, w.stokno, w.renk, w.beden, w.depo,  w.uyumsipid, " +
                    " w.uyumsipid2, w.birim1, w.aktarfiyat, w.aktarkur, w.aktardoviz, " +
                    " w.kdv, w.vatid, w.isemriuyumid, w.isemriuyumid2, w.uyumsiparisid, " +
                    " w.uyumsiparisid2, w.stokid, w.birimid, w.firmaid, w.dovizid, " +
                    " w.depoid, w.stoktipiid, w.malzemetakipkodu, w.partino "

            cSQL = cSQL +
                    " order by w.stokfisno, w.malzemetakipkodu, w.stokno, w.renk, w.beden "

            GetSQL = cSQL

        Catch ex As Exception
            ErrDisp("GetSQL", cModuleName,,, ex)
        End Try
    End Function


    Private Sub GetUyumUserFromStokFis(ByVal cStokFisNo As String, ByRef cUserName As String, ByRef cPassword As String)

        Try
            Dim oSQL As New SQLServerClass

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select b.uyumusername, b.uyumpassword " +
                            " from stokfis a with (NOLOCK) , personel b with (NOLOCK) " +
                            " where a.createuser = b.username " +
                            " and a.stokfisno = '" + cStokFisNo.Trim + "' "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                cUserName = oSQL.SQLReadString("uyumusername")
                cPassword = oSQL.SQLReadString("uyumpassword")
            End If

            oSQL.oReader.Close()
            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp("GetUyumUserFromStokFis", cModuleName,,, ex)
        End Try

    End Sub
End Module
