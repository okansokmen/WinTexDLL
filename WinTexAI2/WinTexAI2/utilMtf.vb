Option Explicit On
Option Strict On

Module utilMtf

    Public Const G_isemriicinGelenGiris As String = " (stokhareketkodu = '04 Mlz Uretimden Giris' " +
                                                   " or stokhareketkodu = '06 Tamirden Giris' " +
                                                   " or stokhareketkodu = '02 Tedarikten Giris'  " +
                                                   " or stokhareketkodu = '05 Diger Giris' ) "

    Public Const G_isemriicinGelenCikis As String = " (stokhareketkodu = '02 Tedarikten iade' " +
                                                   " or stokhareketkodu = '06 Tamire Cikis' " +
                                                   " or stokhareketkodu = '04 Mlz Uretime iade' " +
                                                   " or stokhareketkodu = '05 Diger Cikis' ) "
    Private Structure ModelHammadde
        Dim cReceteNo As String
        Dim cModelNo As String
        Dim cModelRenk As String
        Dim cModelBeden As String
        Dim cHammaddeKodu As String
        Dim cHammaddeRenk As String
        Dim cHammaddeBeden As String
        Dim cMalTakipEsasi As String
        Dim cTeminDept As String
        Dim cUretimDepartmani As String
        Dim nKullanimMiktari As Double
        Dim cHesaplama As String
        Dim cMiktarBirimi As String
        Dim cMalzemeTakipNo As String
        Dim cUlke As String
    End Structure

    Private Structure oDept
        Dim cDepartman As String
        Dim nIsemriAdet As Double
        Dim nUretilenAdet As Double
        Dim nNetUretilenAdet As Double
        Dim lIsemirleriOK As Boolean
        Dim lUretimTamamlandi As Boolean
    End Structure

    Private Structure MRB
        Dim cModelNo As String
        Dim cRenk As String
        Dim cBeden As String
        Dim cReceteNo As String
        Dim cUlke As String
        Dim nAdet As Double
        Dim nKesimIsEmriAdet As Double
        Dim nKesimAdet As Double
        Dim nKesimAdetNet As Double
        Dim nSiraNo As Double
    End Structure

    Private Structure MTF
        Dim cStokNo As String
        Dim cRenk As String
        Dim cBeden As String
        Dim cUDept As String
        Dim cMDept As String
        Dim nKullanimMiktari As Double
        Dim nMiktar As Double
        Dim nMiktarKesimIsemri As Double
        Dim nMiktarKesim As Double
        Dim nMiktarKesimNet As Double
        Dim cBirim As String
        Dim cHesaplama As String
        Dim nSiraNo As Double
        Dim nKarsilanan As Double
        Dim nYuvarla As Double
        Dim cTamSayiYap As String
    End Structure

    Private Structure oUMFL
        Dim cStokNo As String
        Dim cRenk As String
        Dim cBeden As String
        Dim nMiktar As Double
        Dim cBirim As String
        Dim cTedarikci As String
    End Structure

    Public Sub MalzemeHesapla()
        Try
            Dim oSQL As New SQLServerClass
            Dim oSQL2 As New SQLServerClass
            Dim cDepartman As String = ""
            Dim cFirma As String = ""
            Dim cModelNo As String = ""
            Dim cIsemriNo As String = ""
            Dim cUretimTakipNo As String = ""
            Dim nFirmaAdedi As Integer = 0
            Dim cSiparisNo As String = ""
            Dim cMTF As String = ""
            Dim nCnt As Integer = 0
            Dim cMTFStok As String = ""

            oSQL2.init(4)
            oSQL2.OpenConn()

            oSQL.init(4)
            oSQL.OpenConn()

            ' Clear target table first (consider TRUNCATE if no FKs)
            oSQL.cSQLQuery = "delete uretimismalzeme"
            oSQL.SQLExecute()

            ' Get candidate work orders
            oSQL.cSQLQuery = "select distinct a.uretimtakipno, a.isemrino, a.departman, a.firma, c.modelno, "

            oSQL.cSQLQuery = oSQL.cSQLQuery +
                             " firmaadedi = (select count (distinct firma) " +
                                            " from uretimisemri with (NOLOCK) " +
                                            " where uretimtakipno = a.uretimtakipno) "

            oSQL.cSQLQuery = oSQL.cSQLQuery +
                             " From uretimisemri a with (NOLOCK), uretimisdetayi b with (NOLOCK), uretimisrba c with (NOLOCK)  " +
                             " Where a.isemrino = b.isemrino " +
                             " And b.isemrino = c.isemrino  " +
                             " And b.ulineno = c.ulineno " +
                             " And a.departman Like 'D_K_M' " +
                             " And (a.ok is null or a.ok = 'H' or a.ok = '') "

            ' eğer modelin reçete veya alt reçetede malzemesi varsa...
            oSQL.cSQLQuery = oSQL.cSQLQuery +
                             " And exists (select modelno " +
                                        " from modelhammadde with (NOLOCK) " +
                                        " where modelno = c.modelno " +
                                        " union " +
                                        " select modelno " +
                                        " from modelhammadde2 with (NOLOCK) " +
                                        " where modelno = c.modelno ) "

            oSQL.cSQLQuery = oSQL.cSQLQuery +
                             " And exists (select x.dosyakapandi " +
                                        " From siparis x with (NOLOCK) , sipmodel y with (NOLOCK) " +
                                        " Where x.kullanicisipno = y.siparisno " +
                                        " And y.uretimtakipno = a.uretimtakipno " +
                                        " And (x.dosyakapandi Is null Or x.dosyakapandi = '' or x.dosyakapandi = 'H') ) "

            oSQL.GetSQLReader()

            Dim sb As New System.Text.StringBuilder()
            Dim firstSelect As Boolean = True

            Do While oSQL.oReader.Read

                nFirmaAdedi = oSQL.SQLReadInteger("firmaadedi")
                cUretimTakipNo = oSQL.SQLReadString("uretimtakipno")
                cIsemriNo = oSQL.SQLReadString("isemrino")
                cModelNo = oSQL.SQLReadString("modelno")
                cDepartman = oSQL.SQLReadString("departman")
                cFirma = oSQL.SQLReadString("firma")

                oSQL2.cSQLQuery = "select top 1 siparisno , malzemetakipno " +
                                  " from sipmodel with (NOLOCK) " +
                                  " where uretimtakipno = '" + cUretimTakipNo + "' "

                oSQL2.GetSQLReader()

                If oSQL2.oReader.Read Then
                    cSiparisNo = oSQL2.SQLReadString("siparisno")
                    cMTF = oSQL2.SQLReadString("malzemetakipno")
                End If
                oSQL2.oReader.Close()

                oSQL2.cSQLQuery = "select count (distinct uretimtakipno) " +
                                " from sipmodel with (NOLOCK) " +
                                " where siparisno = '" + cSiparisNo + "' "

                nCnt = oSQL2.DBReadInteger

                If nFirmaAdedi > 1 Or nCnt > 1 Then
                    ' Complex case: per-isemri generator (inserts + detailed recursive calc)
                    MTKFastGenerate2(cIsemriNo)
                Else
                    ' Simple case: batch via UNION ALL (defer update of movement columns to mass update later)
                    If firstSelect Then
                        sb.AppendLine("INSERT INTO uretimismalzeme " +
                                      " (uretimtakipno, malzemetakipno, isemrino, stokno, renk, " +
                                      " beden, ihtiyac, birim, uretimicincikis, uretimdeniade, " +
                                      " mtf, kullanimmiktari ) ")
                        firstSelect = False
                    Else
                        sb.AppendLine(" UNION ALL ")
                    End If

                    sb.AppendLine(" SELECT " &
                                   " malzemetakipno = '" & cMTF & "' , " &
                                   " uretimtakipno = '" & cUretimTakipNo & "' , " &
                                   " isemrino = '" & cIsemriNo & "' , " &
                                   " mtk.stokno, mtk.renk, mtk.beden, mtk.ihtiyac, mtk.birim, " &
                                   " uretimicincikis = 0, uretimdeniade = 0, 'E', " &
                                   " kullanimmiktari = (SELECT TOP 1 COALESCE(kullanimmiktari,0) " +
                                                        " FROM modelhammadde WITH (NOLOCK) " &
                                                        " WHERE modelno = '" & cModelNo & "' " &
                                                        " AND hammaddekodu = mtk.stokno " &
                                                        " AND (hammadderenk = mtk.renk OR hammadderenk = 'HEPSI') " &
                                                        " AND (hammaddebeden = mtk.beden OR hammaddebeden = 'HEPSI')) " &
                                   " FROM mtkfislines mtk WITH (NOLOCK) " &
                                   " WHERE mtk.malzemetakipno = '" + cMTF + "' ")
                End If
            Loop
            oSQL.oReader.Close()

            ' Execute batched INSERT if any simple rows collected
            If sb.Length > 0 Then
                oSQL.SQLExecute(sb.ToString())
            End If

            ' MASS UPDATE of uretimicincikis / uretimdeniade for all rows (both simple and complex cases)
            ' Aggregate movements once and join back.
            'oSQL.cSQLQuery =
            '    " with hareket as ( " &
            '    " select b.malzemetakipkodu, b.stokno, b.renk, b.beden, " &
            '    " uretimicincikis = SUM(CASE WHEN b.stokhareketkodu='01 Uretime Cikis'  THEN COALESCE(b.netmiktar1,0) ELSE 0 END), " &
            '    " uretimdeniade   = SUM(CASE WHEN b.stokhareketkodu='01 Uretimden iade' THEN COALESCE(b.netmiktar1,0) ELSE 0 END) " &
            '    " from stokfis a with (NOLOCK) " &
            '    " join stokfislines b with (NOLOCK) on a.stokfisno = b.stokfisno " &
            '    " where exists (select uretimtakipno " +
            '                  " from uretimismalzeme with (NOLOCK) " &
            '                  " malzemetakipno = b.malzemetakipkodu) " &
            '    " group by b.malzemetakipkodu, b.stokno, b.renk, b.beden ) "

            'oSQL.cSQLQuery = oSQL.cSQLQuery &
            '    " update u set " &
            '    " u.uretimicincikis = h.uretimicincikis, " &
            '    " u.uretimdeniade  = h.uretimdeniade " &
            '    " from uretimismalzeme u " &
            '    " join hareket h on h.malzemetakipkodu = u.uretimtakipno " &
            '    " and h.stokno = u.stokno " &
            '    " and h.renk = u.renk " &
            '    " and h.beden = u.beden "

            ' departman kontrolü yapmıyoruz
            ' firmaya çıkan bütün malzemeler toplanıyor
            oSQL.cSQLQuery = "update a set " +
                            " uretimecikis = (SELECT top 1 case when x.belgetarihi is null or x.belgetarihi = '01.01.1950' then x.fistarihi else x.belgetarihi end  " +
                                                " FROM stokfis x WITH (NOLOCK) , stokfislines y WITH (NOLOCK) " +
                                                " where x.stokfisno = y.stokfisno " +
                                                " AND y.malzemetakipkodu = a.malzemetakipno " +
                                                " AND y.stokno = a.stokno " +
                                                " AND y.renk = a.renk  " +
                                                " AND y.beden = a.beden " +
                                                " AND x.firma = b.firma " +
                                                " AND y.stokhareketkodu  in ('01 Uretime Cikis','05 Diger Cikis') " +
                                                " order by x.belgetarihi, x.fistarihi ) , " +
                            " uretimicincikis = (SELECT SUM(COALESCE(y.netmiktar1,0)) " +
                                                " FROM stokfis x WITH (NOLOCK) , stokfislines y WITH (NOLOCK) " +
                                                " where x.stokfisno = y.stokfisno " +
                                                " And y.malzemetakipkodu = a.malzemetakipno " +
                                                " And y.stokno = a.stokno " +
                                                " And y.renk = a.renk  " +
                                                " And y.beden = a.beden " +
                                                " And x.firma = b.firma " +
                                                " And y.stokhareketkodu  in ('01 Uretime Cikis','05 Diger Cikis') ) , " +
                            " uretimdeniade = (SELECT SUM(COALESCE(y.netmiktar1,0)) " +
                                                " FROM stokfis x WITH (NOLOCK) , stokfislines y WITH (NOLOCK) " +
                                                " where x.stokfisno = y.stokfisno " +
                                                " AND y.malzemetakipkodu = a.malzemetakipno " +
                                                " AND y.stokno = a.stokno " +
                                                " AND y.renk = a.renk " +
                                                " AND y.beden = a.beden " +
                                                " AND x.firma = b.firma " +
                                                " AND y.stokhareketkodu in ('01 Uretimden iade','05 Diger Giris') ) " +
                            " FROM uretimismalzeme a , uretimisemri b " +
                            " WHERE a.uretimtakipno = b.uretimtakipno " +
                            " AND a.isemrino = b.isemrino "

            oSQL.SQLExecute()

            ' bazı alanlar miktarlar bölünse bile aynı kalıyor, MTF den oku
            cMTFStok = "select x.malzemetakipno, x.stokno, r.cinsaciklamasi, x.renk, x.beden, x.isemriverilen, " +
                        " karsilanan = coalesce(x.isemriicingelen,0) + coalesce(x.isemriharicigelen,0), " +
                        " stokmiktari = (select sum(coalesce(y.donemgiris1,0)) - sum(coalesce(y.donemcikis1,0)) " +
                                        " from stokrb y with (NOLOCK) " +
                                        " where y.stokno = x.stokno " +
                                        " And y.renk = x.renk " +
                                        " And y.beden = x.beden ), " +
                        " baslama = (select top 1 z.baslamatarihi " +
                                        " from isemrilines z with (NOLOCK) " +
                                        " where z.malzemetakipno = x.malzemetakipno " +
                                        " And z.stokno = x.stokno" +
                                        " And z.renk = x.renk " +
                                        " And z.beden = x.beden " +
                                        " And z.baslamatarihi Is Not null " +
                                        " And z.baslamatarihi <> '01.01.1950' " +
                                        " order by z.baslamatarihi), " +
                        " bitis = (select top 1 z.termintarihi " +
                                        " from isemrilines z with (NOLOCK)  " +
                                        " where z.malzemetakipno = x.malzemetakipno  " +
                                        " and z.stokno = x.stokno " +
                                        " and z.renk = x.renk " +
                                        " and z.beden = x.beden " +
                                        " and z.termintarihi is not null " +
                                        " and z.termintarihi <> '01.01.1950' " +
                                        " order by z.termintarihi desc), " +
                        " tedarikci = (select top 1 q.firma  " +
                                        " from isemri q with (NOLOCK) , isemrilines z with (NOLOCK) " +
                                        " where q.isemrino = z.isemrino  " +
                                        " and z.malzemetakipno = x.malzemetakipno  " +
                                        " and z.stokno = x.stokno " +
                                        " and z.renk = x.renk " +
                                        " and z.beden = x.beden )  " +
                        " from mtkfislines x with (NOLOCK) , stok r with (NOLOCK) " +
                        " where x.stokno = r.stokno "

            oSQL.cSQLQuery = "update a " +
                        " set a.cinsaciklamasi = rtrim(b.cinsaciklamasi) , " +
                        " a.isemriverilen = b.isemriverilen , " +
                        " a.karsilanan = b.karsilanan , " +
                        " a.stokmiktari = b.stokmiktari , " +
                        " a.baslama = b.baslama , " +
                        " a.bitis = b.bitis , " +
                        " a.tedarikci = rtrim(b.tedarikci) " +
                        " from uretimismalzeme a with (NOLOCK), (" + cMTFStok + ") b " +
                        " where a.malzemetakipno = b.malzemetakipno " +
                        " And a.stokno = b.stokno " +
                        " And a.renk = b.renk " +
                        " And a.beden = b.beden "

            oSQL.SQLExecute()

            oSQL.CloseConn()
            oSQL2.CloseConn()

        Catch ex As System.Exception
            ErrDisp(ex.Message, "UretimPlanlama1.MalzemeHesapla", , , ex)
        End Try
    End Sub

    Private Function MTKFastGenerate2(ByVal cIsEmriNo As String) As Integer

        Dim oSQL As New SQLServerClass
        Dim cUTF As String = ""
        Dim cMTF As String = ""
        Dim cModelNo As String = ""
        Dim cReceteNo As String = ""
        Dim cUlke As String = ""
        Dim cUDept As String = ""
        Dim cFirma As String = ""

        Dim nCnt1 As Integer = 0
        Dim nCnt2 As Integer = 0
        Dim nCnt3 As Integer = 0
        Dim nCnt4 As Integer = 0
        Dim aMRBA() As MRB = Nothing
        Dim aMTF() As MTF = Nothing
        Dim aMH() As ModelHammadde = Nothing
        Dim cSQL As String = ""
        Dim nCnt As Integer = 0
        Dim cMusteri As String = ""
        Dim nKesilenAdet As Double = 0
        Dim nSiparisAdet As Double = 0
        Dim nReceteAdet As Double = 0
        Dim nUretimToleransi As Double = 0
        Dim lAltModelDetay As Boolean = False
        Dim cSipModelTableName As String = ""
        Dim lFirst As Boolean = True
        Dim nFound As Integer = -1
        Dim cStokNo As String = ""
        Dim cRenk As String = ""
        Dim cBeden As String = ""
        Dim cMDept As String = ""
        Dim cBirim As String = ""
        Dim nMiktar As Double = 0
        Dim nMiktarKesimIsemri As Double = 0
        Dim nMiktarKesim As Double = 0
        Dim nMiktarKesimNet As Double = 0
        Dim cParameters As String = ""
        Dim cModelRenk As String = ""
        Dim cModelBeden As String = ""
        Dim cModelUlke As String = ""
        Dim lKesimIsEmrineGore As Boolean = False
        Dim lKesimIsEmriOK As Boolean = False
        Dim lKesileneGore As Boolean = False
        Dim lKesimOK As Boolean = False
        Dim nKesimIsEmriAdet As Double = 0
        Dim cOwnerCompany As String = ""
        Dim cKesimDept As String = ""
        Dim lRBA As Boolean = False
        Dim lReceteler As Boolean = False
        Dim lOK1 As Boolean = False

        MTKFastGenerate2 = 0

        Try
            If cIsEmriNo.Trim = "" Then
                Exit Function
            End If

            ' buraya sadece dikim işemirleri geliyor 

            oSQL.init(4)
            oSQL.OpenConn()

            oSQL.cSQLQuery = "select top 1 " +
                            " a.uretimtakipno, a.isemrino, c.modelno, a.departman, a.firma, c.renk, c.beden, " +
                            " adet = sum(coalesce(c.adet, 0)) , "

            oSQL.cSQLQuery = oSQL.cSQLQuery +
                            " tolerans = (select max(uretimtoleransi)  " +
                                        " from modeluretim with (NOLOCK) " +
                                        " where modelno = c.modelno ) , "

            oSQL.cSQLQuery = oSQL.cSQLQuery +
                            " malzemetakipno = (select top 1 malzemetakipno " +
                                        " from sipmodel with (NOLOCK) " +
                                        " where uretimtakipno = a.uretimtakipno " +
                                        " And modelno = c.modelno " +
                                        " And renk = c.renk " +
                                        " And beden = c.beden ) , "

            oSQL.cSQLQuery = oSQL.cSQLQuery +
                            " receteno = (select top 1 receteno " +
                                        " from sipmodel with (NOLOCK) " +
                                        " where uretimtakipno = a.uretimtakipno " +
                                        " And modelno = c.modelno " +
                                        " And renk = c.renk " +
                                        " And beden = c.beden ) , "

            oSQL.cSQLQuery = oSQL.cSQLQuery +
                            " ulke = (select top 1 ulke " +
                                        " from sipmodel with (NOLOCK) " +
                                        " where uretimtakipno = a.uretimtakipno " +
                                        " And modelno = c.modelno " +
                                        " And renk = c.renk " +
                                        " And beden = c.beden ) "

            oSQL.cSQLQuery = oSQL.cSQLQuery +
                            " from uretimisemri a with (NOLOCK), uretimisdetayi b with (NOLOCK), uretimisrba c with (NOLOCK) " +
                            " where a.isemrino = b.isemrino " +
                            " And b.isemrino = c.isemrino " +
                            " And b.ulineno = c.ulineno " +
                            " And a.isemrino = '" + cIsEmriNo + "' " +
                            " group by  a.uretimtakipno, a.isemrino, c.modelno, a.departman, a.firma, c.renk, c.beden "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                cMTF = oSQL.SQLReadString("malzemetakipno")
                cUTF = oSQL.SQLReadString("uretimtakipno")
                cFirma = oSQL.SQLReadString("firma")
                cModelNo = oSQL.SQLReadString("modelno")
                cReceteNo = oSQL.SQLReadString("receteno")
                cUlke = oSQL.SQLReadString("ulke")
                nUretimToleransi = oSQL.SQLReadDouble("tolerans")
            End If
            oSQL.oReader.Close()
            oSQL.oReader = Nothing

            If cUlke.Trim = "" Then
                cUlke = "HEPSI"
            End If

            ' model adetlerini REÇETE BAZINDA dikim işemrinden hesapla

            oSQL.cSQLQuery = "select c.renk, c.beden, " +
                            " adet = sum(coalesce(c.adet,0)) " +
                            " from uretimisemri a with (NOLOCK), uretimisdetayi b with (NOLOCK), uretimisrba c with (NOLOCK) " +
                            " where a.isemrino = b.isemrino " +
                            " and b.isemrino = c.isemrino " +
                            " and b.ulineno = c.ulineno " +
                            " and a.isemrino = '" + cIsEmriNo + "' " +
                            " group by c.renk, c.beden " +
                            " order by c.renk, c.beden "
            nCnt = -1

            oSQL.GetSQLReader()

            Do While oSQL.oReader.Read
                ' Model Reçeteye Göre RBA
                lRBA = True

                nCnt = nCnt + 1
                ReDim Preserve aMRBA(nCnt)

                aMRBA(nCnt).cModelNo = cModelNo
                aMRBA(nCnt).cReceteNo = cReceteNo
                aMRBA(nCnt).cUlke = cUlke
                aMRBA(nCnt).cRenk = oSQL.SQLReadString("renk")
                aMRBA(nCnt).cBeden = oSQL.SQLReadString("beden")
                aMRBA(nCnt).nAdet = oSQL.SQLReadDouble("adet")
            Loop
            oSQL.oReader.Close()
            oSQL.oReader = Nothing

            If nCnt = -1 Then
                Exit Function
            End If

            ' Buffer BOM
            If cReceteNo.Trim = "" Then
                oSQL.cSQLQuery = "select receteno = '', a.modelno, a.modelrenk, a.modelbeden, a.hammaddekodu, " +
                                " a.hammadderenk, a.hammaddebeden, b.maltakipesasi, a.temindept, " +
                                " a.uretimdepartmani, a.kullanimmiktari, a.fire, a.hesaplama, a.miktarbirimi, a.malzemetakipno, a.ulke " +
                                " from modelhammadde a WITH (NOLOCK), stok b WITH (NOLOCK) " +
                                " where a.hammaddekodu = b.stokno " +
                                " and a.kullanimmiktari Is Not null " +
                                " and a.kullanimmiktari <> 0 " +
                                " and a.modelno = '" + cModelNo + "' " +
                                " and exists (select departman " +
                                            " from uretimisemri with (NOLOCK) " +
                                            " where uretimtakipno = '" + cUTF + "' " +
                                            " and firma = '" + cFirma + "' " +
                                            " and departman = a.uretimdepartmani) "
            Else
                oSQL.cSQLQuery = " select a.receteno, a.modelno, a.modelrenk, a.modelbeden, a.hammaddekodu, " +
                                " a.hammadderenk, a.hammaddebeden, b.maltakipesasi, a.temindept, " +
                                " a.uretimdepartmani, a.kullanimmiktari, a.fire, a.hesaplama, a.miktarbirimi, a.malzemetakipno, a.ulke " +
                                " from modelhammadde2 a WITH (NOLOCK), stok b WITH (NOLOCK) " +
                                " where a.hammaddekodu = b.stokno " +
                                " And a.kullanimmiktari Is Not null " +
                                " And a.kullanimmiktari <> 0 " +
                                " And a.modelno = '" + cModelNo + "' " +
                                " and a.receteno = '" + cReceteNo + "' " +
                                " and exists (select departman " +
                                            " from uretimisemri with (NOLOCK) " +
                                            " where uretimtakipno = '" + cUTF + "' " +
                                            " and firma = '" + cFirma + "' " +
                                            " and departman = a.uretimdepartmani) "
            End If

            nCnt = -1

            oSQL.GetSQLReader()

            Do While oSQL.oReader.Read

                lReceteler = True

                nCnt = nCnt + 1
                ReDim Preserve aMH(nCnt)

                aMH(nCnt).cReceteNo = oSQL.SQLReadString("receteno")
                aMH(nCnt).cModelNo = oSQL.SQLReadString("modelno")
                aMH(nCnt).cModelRenk = oSQL.SQLReadString("modelrenk")
                aMH(nCnt).cModelBeden = oSQL.SQLReadString("modelbeden")
                aMH(nCnt).cHammaddeKodu = oSQL.SQLReadString("hammaddekodu")
                aMH(nCnt).cHammaddeRenk = oSQL.SQLReadString("hammadderenk")
                aMH(nCnt).cHammaddeBeden = oSQL.SQLReadString("hammaddebeden")
                aMH(nCnt).cMalTakipEsasi = oSQL.SQLReadString("maltakipesasi")
                aMH(nCnt).cTeminDept = oSQL.SQLReadString("temindept")
                aMH(nCnt).cUretimDepartmani = oSQL.SQLReadString("uretimdepartmani")
                aMH(nCnt).nKullanimMiktari = oSQL.SQLReadDouble("kullanimmiktari")
                aMH(nCnt).cHesaplama = oSQL.SQLReadString("hesaplama")
                aMH(nCnt).cMiktarBirimi = oSQL.SQLReadString("miktarbirimi")
                aMH(nCnt).cMalzemeTakipNo = oSQL.SQLReadString("malzemetakipno")
                aMH(nCnt).cUlke = oSQL.SQLReadString("ulke")
            Loop
            oSQL.oReader.Close()
            oSQL.oReader = Nothing

            ' HEPSI düzeltmeleri yapılıyor
            For nCnt = 0 To UBound(aMH)
                If aMH(nCnt).cModelRenk = "" Then aMH(nCnt).cModelRenk = "HEPSI"
                If aMH(nCnt).cModelBeden = "" Then aMH(nCnt).cModelBeden = "HEPSI"
                If aMH(nCnt).cHammaddeRenk = "" Then aMH(nCnt).cHammaddeRenk = "HEPSI"
                If aMH(nCnt).cHammaddeBeden = "" Then aMH(nCnt).cHammaddeBeden = "HEPSI"
                If aMH(nCnt).cUlke = "" Then aMH(nCnt).cUlke = "HEPSI"
            Next

            If lRBA And lReceteler Then
                ' Hem reçeteler hem de RBA var ise RBA yı BOM ile çarp
                lFirst = True
                For nCnt = 0 To UBound(aMRBA)
                    For nCnt3 = 0 To UBound(aMH)
                        cModelRenk = IIf(aMH(nCnt3).cModelRenk = "HEPSI", aMRBA(nCnt).cRenk, aMH(nCnt3).cModelRenk).ToString
                        cModelBeden = IIf(aMH(nCnt3).cModelBeden = "HEPSI", aMRBA(nCnt).cBeden, aMH(nCnt3).cModelBeden).ToString
                        cModelUlke = IIf(aMH(nCnt3).cUlke = "HEPSI", aMRBA(nCnt).cUlke, aMH(nCnt3).cUlke).ToString

                        lOK1 = True

                        If aMRBA(nCnt).cReceteNo.Trim = "" Then
                            ' sadece ana reçetede ülke olur
                            lOK1 = (cModelRenk = aMRBA(nCnt).cRenk And
                                    cModelBeden = aMRBA(nCnt).cBeden And
                                    cModelUlke = aMRBA(nCnt).cUlke)
                        Else
                            ' alternatif reçetelerde ülke olmaz
                            lOK1 = (cModelRenk = aMRBA(nCnt).cRenk And
                                    cModelBeden = aMRBA(nCnt).cBeden)
                        End If

                        If lOK1 Then

                            cStokNo = aMH(nCnt3).cHammaddeKodu
                            cMDept = aMH(nCnt3).cTeminDept
                            cUDept = aMH(nCnt3).cUretimDepartmani
                            cBirim = aMH(nCnt3).cMiktarBirimi
                            cRenk = IIf(aMH(nCnt3).cHammaddeRenk = "HEPSI", cModelRenk, aMH(nCnt3).cHammaddeRenk).ToString
                            cBeden = IIf(aMH(nCnt3).cHammaddeBeden = "HEPSI", cModelBeden, aMH(nCnt3).cHammaddeBeden).ToString

                            Select Case aMH(nCnt3).cMalTakipEsasi
                                Case "1"
                                    cRenk = "HEPSI"
                                    cBeden = "HEPSI"
                                Case "2"
                                    cBeden = "HEPSI"
                                Case "3"
                                    cRenk = "HEPSI"
                            End Select

                            If cRenk.Trim = "" Then
                                cRenk = "HEPSI"
                            End If

                            If cBeden.Trim = "" Then
                                cBeden = "HEPSI"
                            End If

                            nMiktar = aMRBA(nCnt).nAdet * aMH(nCnt3).nKullanimMiktari

                            If lFirst Then
                                ReDim aMTF(0)
                                aMTF(0).cStokNo = cStokNo
                                aMTF(0).cRenk = cRenk
                                aMTF(0).cBeden = cBeden
                                aMTF(0).nMiktar = nMiktar
                                aMTF(0).nKullanimMiktari = aMH(nCnt3).nKullanimMiktari
                                lFirst = False
                            Else
                                nFound = -1
                                For nCnt4 = 0 To UBound(aMTF)
                                    If aMTF(nCnt4).cStokNo = cStokNo And
                                        aMTF(nCnt4).cRenk = cRenk And
                                        aMTF(nCnt4).cBeden = cBeden Then
                                        nFound = nCnt4
                                        Exit For
                                    End If
                                Next

                                If nFound = -1 Then
                                    nFound = UBound(aMTF) + 1
                                    ReDim Preserve aMTF(nFound)
                                    aMTF(nFound).cStokNo = cStokNo
                                    aMTF(nFound).cRenk = cRenk
                                    aMTF(nFound).cBeden = cBeden
                                    aMTF(nFound).nMiktar = nMiktar
                                    aMTF(nFound).nKullanimMiktari = aMH(nCnt3).nKullanimMiktari
                                Else
                                    aMTF(nFound).nMiktar = aMTF(nFound).nMiktar + nMiktar
                                End If
                            End If
                        End If
                    Next
                Next

                If Not lFirst Then
                    For nCnt1 = 0 To UBound(aMTF)
                        UpdateMTKFisLines(oSQL, cUTF, cIsEmriNo, aMTF(nCnt1).cStokNo, aMTF(nCnt1).cRenk, aMTF(nCnt1).cBeden,
                                          aMTF(nCnt1).nMiktar, aMTF(nCnt1).nKullanimMiktari)
                    Next
                End If
            End If

            oSQL.cSQLQuery = "update uretimismalzeme " +
                            " set malzemetakipno = '" + cMTF + "' " +
                            " where isemrino = '" + cIsEmriNo + "' "

            oSQL.SQLExecute()

            oSQL.CloseConn()

            ' The END
            MTKFastGenerate2 = 1

        Catch Err As Exception
            ErrDisp(Err.Message, "MTKFastGenerate", cSQL,, Err)
        End Try
    End Function

    Private Sub UpdateMTKFisLines(oSQL As SQLServerClass, cUTF As String, cIsEmriNo As String, cStokno As String, cHRenk As String, cHBeden As String,
                                  nMiktar As Double, nKullanimMiktari As Double, Optional lForceRecursive As Boolean = True)

        Dim cSQL As String = ""
        Dim nMiktar2 As Double = 0
        Dim cTakipEsasi As String = ""
        Dim aMTF() As MTF = Nothing
        Dim nCnt1 As Integer = -1
        Dim lNonRecursiveCalculation As Boolean = False
        Dim cBirim As String = ""
        Dim cUDept As String = ""
        Dim cMDept As String = ""

        Try
            If cStokno.Trim = "" Then Exit Sub

            If oSQL.GetSysPar("mtfhizlihesaplama", "1") = "1" Then
                lNonRecursiveCalculation = True
            End If

            oSQL.cSQLQuery = "select top 1 maltakipesasi, paratakipesasi, temindepartmani, uretimdepartmani, birim1 " +
                            " from stok WITH (NOLOCK) " +
                            " where stokno = '" + cStokno.Trim + "' "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then

                cTakipEsasi = oSQL.SQLReadString("maltakipesasi")

                Select Case cTakipEsasi
                    Case "1"
                        cHRenk = "HEPSI"
                        cHBeden = "HEPSI"
                    Case "2"
                        cHBeden = "HEPSI"
                    Case "3"
                        cHRenk = "HEPSI"
                End Select

                If cMDept.Trim = "" Then
                    cMDept = oSQL.SQLReadString("temindepartmani")
                End If

                If cUDept.Trim = "" Then
                    cUDept = oSQL.SQLReadString("uretimdepartmani")
                End If

                If cBirim.Trim = "" Then
                    cBirim = oSQL.SQLReadString("birim1")
                End If
            End If
            oSQL.oReader.Close()
            oSQL.oReader = Nothing

            oSQL.cSQLQuery = "select top 1 sirano " +
                            " from uretimismalzeme WITH (NOLOCK) " +
                            " where uretimtakipno = '" + cUTF.Trim + "' " +
                            " and isemrino = '" + cIsEmriNo.Trim + "' " +
                            " and stokno = '" + cStokno.Trim + "' " +
                            " and renk = '" + cHRenk.Trim + "' " +
                            " and beden = '" + cHBeden.Trim + "' "

            If oSQL.CheckExists Then

                oSQL.cSQLQuery = "update uretimismalzeme set " +
                                " ihtiyac = coalesce(ihtiyac,0) + " + SQLWriteDecimal(nMiktar) + " , " +
                                " kullanimmiktari = coalesce(kullanimmiktari,0) + " + SQLWriteDecimal(nKullanimMiktari) +
                                " where uretimtakipno = '" + cUTF.Trim + "' " +
                                " and isemrino = '" + cIsEmriNo.Trim + "' " +
                                " and stokno = '" + cStokno.Trim + "' " +
                                " and renk = '" + cHRenk.Trim + "' " +
                                " and beden = '" + cHBeden.Trim + "' "

                oSQL.SQLExecute()
            Else
                oSQL.cSQLQuery = "insert uretimismalzeme (uretimtakipno, isemrino, stokno, renk, beden, ihtiyac, birim, kullanimmiktari, mtf) " +
                                " values ('" + cUTF.Trim + "', " +
                                " '" + cIsEmriNo.Trim + "', " +
                                " '" + cStokno.Trim + "', " +
                                " '" + cHRenk.Trim + "', " +
                                " '" + cHBeden.Trim + "', " +
                                SQLWriteDecimal(nMiktar) + ", " +
                                " '" + cBirim.Trim + "', " +
                                SQLWriteDecimal(nKullanimMiktari) + ", " +
                                " 'H' ) "

                oSQL.SQLExecute()
            End If

            ' Recursive olarak hammadde ağacını hesapla
            If lForceRecursive Then

                oSQL.cSQLQuery = "select a.hammaddekodu, a.hamrenk, a.hambeden, a.fire, a.miktar, " +
                                " a.hesaplama, b.maltakipesasi, b.birim1, a.temindept, a.departman " +
                                " from strecete a WITH (NOLOCK), stok b WITH (NOLOCK) " +
                                " where a.hammaddekodu = b.stokno " +
                                " and a.anahammadde = '" + cStokno.Trim + "' " +
                                " and a.hammaddekodu <> '" + cStokno.Trim + "' " +
                                " and (a.anarenk = '" + cHRenk.Trim + "' or a.anarenk = 'HEPSI') " +
                                " and (a.anabeden = '" + cHBeden.Trim + "' or a.anabeden = 'HEPSI') "

                If oSQL.CheckExists Then

                    oSQL.GetSQLReader(cSQL)

                    Do While oSQL.oReader.Read

                        nCnt1 = nCnt1 + 1
                        ReDim Preserve aMTF(nCnt1)

                        aMTF(nCnt1).cStokNo = oSQL.SQLReadString("hammaddekodu")
                        aMTF(nCnt1).cRenk = IIf(oSQL.SQLReadString("hamrenk") = "HEPSI", cHRenk.Trim, oSQL.SQLReadString("hamrenk")).ToString
                        aMTF(nCnt1).cBeden = IIf(oSQL.SQLReadString("hambeden") = "HEPSI", cHBeden.Trim, oSQL.SQLReadString("hambeden")).ToString

                        Select Case oSQL.SQLReadString("maltakipesasi")
                            Case "1"
                                aMTF(nCnt1).cRenk = "HEPSI"
                                aMTF(nCnt1).cBeden = "HEPSI"
                            Case "2"
                                aMTF(nCnt1).cBeden = "HEPSI"
                            Case "3"
                                aMTF(nCnt1).cRenk = "HEPSI"
                        End Select

                        aMTF(nCnt1).nMiktar = oSQL.SQLReadDouble("miktar")
                        aMTF(nCnt1).cHesaplama = oSQL.SQLReadString("hesaplama")
                        aMTF(nCnt1).cBirim = oSQL.SQLReadString("birim1")
                        aMTF(nCnt1).cMDept = oSQL.SQLReadString("temindept")
                        aMTF(nCnt1).cUDept = oSQL.SQLReadString("departman")

                        aMTF(nCnt1).nMiktar = aMTF(nCnt1).nMiktar * nMiktar
                        aMTF(nCnt1).nKullanimMiktari = aMTF(nCnt1).nMiktar
                    Loop
                    oSQL.oReader.Close()
                    oSQL.oReader = Nothing

                    If Not lNonRecursiveCalculation And nCnt1 > -1 Then
                        For nCnt1 = 0 To UBound(aMTF)
                            ' recurse and recalc
                            UpdateMTKFisLines(oSQL, cUTF, cIsEmriNo, aMTF(nCnt1).cStokNo, aMTF(nCnt1).cRenk, aMTF(nCnt1).cBeden,
                                              aMTF(nCnt1).nMiktar, aMTF(nCnt1).nKullanimMiktari)
                        Next
                    End If
                End If
            End If

        Catch ex As Exception
            ErrDisp(ex.Message, "UpdateMTKFisLines", cSQL,, ex)
        End Try
    End Sub

End Module
