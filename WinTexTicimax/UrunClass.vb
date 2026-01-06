Option Explicit On

Imports WinTexTicimax.UrunServis
Imports System.Net
Imports System.Windows.Forms

Public Class UrunClass

    Dim oClient As UrunServisClient
    Dim oMarkalar As New List(Of Marka)
    Dim oTedarikciler As New List(Of Tedarikci)
    Dim oKategoriler As New List(Of Kategori)

    Public Sub New()

        Dim oBinding As System.ServiceModel.BasicHttpBinding = GetBinding("BasicHttpBinding_IUrunServis")
        Dim oEndPointAddress As System.ServiceModel.EndpointAddress = GetEndpointAddress(oConnection.cTiciMaxApiUrunUrl)

        oClient = New UrunServisClient(oBinding, oEndPointAddress)
    End Sub

    Public Sub CloseClient()
        On Error Resume Next
        oClient.Close()
    End Sub

    Public Sub LoadSabitler(Optional nCase As Integer = 0)
        Try
            Select Case nCase
                Case 1
                    oMarkalar = oClient.SelectMarka(oConnection.cTiciMaxYetkiKodu, 0)
                Case 2
                    oTedarikciler = oClient.SelectTedarikci(oConnection.cTiciMaxYetkiKodu, 0)
                Case 3
                    oKategoriler = oClient.SelectKategori(oConnection.cTiciMaxYetkiKodu, 0, "")
                Case Else
                    oMarkalar = oClient.SelectMarka(oConnection.cTiciMaxYetkiKodu, 0)
                    oTedarikciler = oClient.SelectTedarikci(oConnection.cTiciMaxYetkiKodu, 0)
                    oKategoriler = oClient.SelectKategori(oConnection.cTiciMaxYetkiKodu, 0, "")
            End Select

        Catch ex As Exception
            ErrDisp(ex.Message, "LoadSabitler",, True, ex)
        End Try
    End Sub

    Public Function SendProducts() As Boolean

        SendProducts = False

        Try
            Dim cStokNo As String = ""
            Dim cRenk As String = ""
            Dim cBeden As String = ""
            Dim oSQL As New SQLServerClass
            Dim lOk As Boolean = False

            oSQL.OpenConn()
            oSQL.cSQLQuery = GetSQLQuery(2)
            oSQL.GetSQLReader()
            Do While oSQL.oReader.Read
                cStokNo = oSQL.SQLReadString("stokno")
                cRenk = oSQL.SQLReadString("renk")
                cBeden = oSQL.SQLReadString("beden")
                lOk = SendProduct(cStokNo, cRenk, cBeden)
            Loop
            oSQL.oReader.Close()
            oSQL.CloseConn()

            SendProducts = True

        Catch ex As Exception
            ErrDisp(ex.Message, "SendProducts",, True, ex)
        End Try
    End Function

    Public Function SendProduct(cStokNo As String, cRenk As String, cBeden As String) As Boolean

        ' ürün resmi 1280 x 1080 gitmeli

        SendProduct = False

        Try
            Dim oSQL As New SQLServerClass
            Dim oUrunKarti As New UrunKarti
            Dim oUrunKartiAyar As New UrunKartiAyar
            Dim oVaryasyonAyar As New VaryasyonAyar
            Dim cStokKodu As String = cStokNo.Trim + "/" + cRenk.Trim
            Dim cVaryasyonKodu As String = ""
            Dim nSonuc As Integer = 0
            Dim cBarcode As String = ""
            Dim nSatisFiyati1 As Double = 0
            Dim nSatisFiyati2 As Double = 0
            Dim nSatisFiyati3 As Double = 0
            Dim nSatisFiyati4 As Double = 0
            Dim nAlisFiyati1 As Double = 0
            Dim nAlisFiyati2 As Double = 0
            Dim nAlisFiyati3 As Double = 0
            Dim nAlisFiyati4 As Double = 0
            Dim nStokMiktari As Double = 0
            Dim nToplamStokMiktari As Double = 0
            Dim nTiciMaxID As Double = 0
            Dim nTiciMaxVaryasyonID As Double = 0
            Dim cUrunSayfaAdresi As String = Replace(cStokNo.Trim + "-" + cRenk.Trim, " ", "-").Trim
            Dim cUrunAciklama As String = ""

            Dim oUrunKartiEtiketListe As New List(Of UrunKartiEtiket)
            Dim oUrunKartiTeknikDetayListe As New List(Of UrunKartiTeknikDetay)
            Dim oResimler As New List(Of String)
            Dim oCategories As New List(Of Integer)
            Dim oUrunKartlari As New List(Of UrunKarti)
            Dim oVaryasyonResimler As List(Of String)
            Dim oVaryasyonlar As New List(Of Varyasyon)
            Dim oVaryasyonOzellikler As List(Of VaryasyonOzellik)
            Dim oVaryasyonOzellik As VaryasyonOzellik
            Dim oVaryasyon As Varyasyon
            Dim oUrunKartiEtiket As New UrunKartiEtiket
            Dim oUrunKartiTeknikDetay As New UrunKartiTeknikDetay

            oUrunKartiEtiket = New UrunKartiEtiket
            oUrunKartiEtiket.EtiketID = 1
            oUrunKartiEtiketListe.Add(oUrunKartiEtiket)

            oUrunKartiTeknikDetay = New UrunKartiTeknikDetay
            oUrunKartiTeknikDetay.DegerID = 1
            oUrunKartiTeknikDetay.OzellikID = 1
            oUrunKartiTeknikDetayListe.Add(oUrunKartiTeknikDetay)

            With oUrunKartiAyar
                .TedarikciKodunaGoreGuncelle = True

                .AciklamaGuncelle = True

                .AktifGuncelle = False
                .VitrinGuncelle = False
                .FirsatUrunuGuncelle = False
                .YeniUrunGuncelle = False
                .KategoriGuncelle = False

                .SeoAnahtarKelimeGuncelle = False
                .SeoSayfaAciklamaGuncelle = False
                .SeoSayfaBaslikGuncelle = False
                .SeoNoFollowGuncelle = False
                .SeoNoIndexGuncelle = False

                .AdwordsAciklamaGuncelle = False
                .AdwordsKategoriGuncelle = False
                .AdwordsTipGuncelle = False

                .MarkaGuncelle = True
                .OnYaziGuncelle = True
                .SatisBirimiGuncelle = True
                .TedarikciGuncelle = True
                .UcretsizKargoGuncelle = True
                .UrunAdiGuncelle = False
                .UserAgent = "" ' boş gönderilebilir 
                .AramaAnahtarKelimeGuncelle = True
                .AsortiGrupGuncelle = True
                .OncekiKategoriEslestirmeleriniTemizle = True
                .TahminiTeslimSuresiGuncelle = True
                .TahminiTeslimSuresiGosterGuncelle = True
                .UyeAlimMaksGuncelle = True
                .UyeAlimMinGuncelle = True
                .FBStoreGosterGuncelle = True
                .MaksTaksitSayisiGuncelle = True
                .KargoTipiGuncelle = True
                .OzelAlan1Guncelle = True
                .OzelAlan2Guncelle = True
                .OzelAlan3Guncelle = True
                .OzelAlan4Guncelle = True
                .OzelAlan5Guncelle = True

                .TahminiTeslimSuresiTarihGuncelle = False
                .UrunAdresiniElleOlustur = False
                .ParaPuanGuncelle = False
                .EtiketGuncelle = False
                .TeknikDetayGuncelle = False

                .OncekiResimleriSil = False
                .UrunResimGuncelle = False
                .Base64Resim = False
                .ResimleriIndirme = False
                .ResimOlmayanlaraResimEkle = False
            End With

            With oVaryasyonAyar
                .TedarikciKodunaGoreGuncelle = True
                ' zorunlu alanlar
                .SatisFiyatiGuncelle = False
                .ParaBirimiGuncelle = True
                ' opsiyonel alanlar
                .AktifGuncelle = False

                .AlisFiyatiGuncelle = False
                .IndirimliFiyatiGuncelle = False
                .UyeTipiFiyat1Guncelle = False
                .UyeTipiFiyat2Guncelle = False
                .UyeTipiFiyat3Guncelle = False
                .UyeTipiFiyat4Guncelle = False
                .UyeTipiFiyat5Guncelle = False

                .KdvDahilGuncelle = False
                .KdvOraniGuncelle = False

                .BarkodGuncelle = True
                .StokKoduGuncelle = True
                .StokAdediGuncelle = True
                .EksiStokAdediGuncelle = True
                .KargoUcretiGuncelle = True
                .KargoAgirligiGuncelle = True

                .OncekiResimleriSil = False
                .ResimOlmayanlaraResimEkle = False
                .UrunResimGuncelle = False
            End With

            ' resimler
            oResimler.Add("http://www.benimpapucum.com/resim1.png")

            'oCategories.Add(oKategoriler.Item(0).ID)
            oCategories.Add(22)

            ' Ürün varyasyon özelliklerini belirleme

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select distinct stokno, renk, beden, barcode, " +
                        " ticimaxid, ticimaxvaryasyonid," +
                        " satisfiyati1, satisfiyati2, satisfiyati3, satisfiyati4, " +
                        " alisfiyati1, alisfiyati2, alisfiyati3, alisfiyati4, "

            oSQL.cSQLQuery = oSQL.cSQLQuery +
                        " stokmiktari = (select sum(coalesce(giren,0) - coalesce(cikan,0)) " +
                                       " from satilabilirstokticimax(stokrb2.stokno, stokrb2.renk, stokrb2.beden)) "

            oSQL.cSQLQuery = oSQL.cSQLQuery +
                        " from stokrb2 with (NOLOCK) " +
                        " where stokno = '" + cStokNo.Trim + "' " +
                        " and renk = '" + cRenk.Trim + "' " +
                        " and beden = '" + cBeden.Trim + "' "

            oSQL.GetSQLReader()

            Do While oSQL.oReader.Read

                cVaryasyonKodu = oSQL.SQLReadString("stokno") + "/" + oSQL.SQLReadString("renk") + "/" + oSQL.SQLReadString("beden")

                cBarcode = oSQL.SQLReadString("barcode")
                ' TiciMaxID stokno ve renk bazında
                nTiciMaxID = oSQL.SQLReadDouble("ticimaxid")
                ' TiciMaxVaryasyonID stokno ve renk bazında, 1 üründe 1 renk oluyor yani her üründe tek varyasyon var
                nTiciMaxVaryasyonID = oSQL.SQLReadDouble("ticimaxvaryasyonid")

                'If oSQL.SQLReadDouble("satisfiyati2") = 0 Then
                '    nSatisFiyati1 = oSQL.SQLReadDouble("satisfiyati1")
                'Else
                nSatisFiyati1 = oSQL.SQLReadDouble("satisfiyati1")
                'End If

                'If nSatisFiyati1 = 0 Then
                '    nSatisFiyati1 = 1000
                'End If

                nSatisFiyati2 = 0 ' oSQL.SQLReadDouble("satisfiyati2")
                nSatisFiyati3 = 0 ' oSQL.SQLReadDouble("satisfiyati3")
                nSatisFiyati4 = 0 ' oSQL.SQLReadDouble("satisfiyati4")

                'If oSQL.SQLReadDouble("alisfiyati2") = 0 Then
                '    nAlisFiyati1 = oSQL.SQLReadDouble("alisfiyati1")
                'Else
                nAlisFiyati1 = oSQL.SQLReadDouble("alisfiyati2")
                'End If

                nAlisFiyati2 = 0 ' oSQL.SQLReadDouble("alisfiyati2")
                nAlisFiyati3 = 0 ' oSQL.SQLReadDouble("alisfiyati3")
                nAlisFiyati4 = 0 ' oSQL.SQLReadDouble("alisfiyati4")

                nStokMiktari = oSQL.SQLReadDouble("stokmiktari")

                oVaryasyonOzellikler = New List(Of VaryasyonOzellik)

                oVaryasyonOzellik = New VaryasyonOzellik
                With oVaryasyonOzellik
                    .Tanim = "Renk"
                    .Tur = BLEnumsVaryasyonTuru.Renk
                    .Deger = oSQL.SQLReadString("renk")
                End With
                oVaryasyonOzellikler.Add(oVaryasyonOzellik)

                oVaryasyonOzellik = New VaryasyonOzellik
                With oVaryasyonOzellik
                    .Tanim = "Numara"
                    .Tur = BLEnumsVaryasyonTuru.Beden
                    .Deger = oSQL.SQLReadString("beden")
                End With
                oVaryasyonOzellikler.Add(oVaryasyonOzellik)

                oVaryasyonResimler = New List(Of String)
                oVaryasyonResimler.Add("http: //www.benimpapucum.com/varyasyonluresim.png")

                ' Ürün varyasyonları. en az 1 adet zorunlu
                oVaryasyon = New Varyasyon
                With oVaryasyon
                    ' zorunlu alanlar
                    .TedarikciKodu = cVaryasyonKodu
                    .StokKodu = cVaryasyonKodu
                    .AlisFiyati = nAlisFiyati1
                    .SatisFiyati = nSatisFiyati1
                    .StokAdedi = nStokMiktari
                    .EksiStokAdedi = 0

                    .ID = nTiciMaxVaryasyonID
                    .UrunKartiID = 0
                    .ParaBirimiID = 1
                    ' opsiyonel alanlar
                    .Barkod = cBarcode
                    .Desi = CDbl(oConnection.cTiciMaxDesi)
                    .KargoUcreti = CDbl(oConnection.cTiciMaxKargoUcreti)
                    .KdvDahil = True
                    .KdvOrani = CInt(oConnection.cTiciMaxKdvOrani)
                    .Aktif = True

                    .IndirimliFiyati = nSatisFiyati1

                    .UyeTipiFiyat1 = nSatisFiyati1
                    .UyeTipiFiyat2 = nSatisFiyati2
                    .UyeTipiFiyat3 = nSatisFiyati3
                    .UyeTipiFiyat4 = nSatisFiyati4
                    .UyeTipiFiyat5 = 0

                    .SatisFiyati1 = nSatisFiyati1
                    .SatisFiyati2 = nSatisFiyati2
                    .SatisFiyati3 = nSatisFiyati3
                    .SatisFiyati4 = nSatisFiyati4
                    .SatisFiyati5 = 0
                    .SatisFiyati6 = 0
                    .SatisFiyati7 = 0
                    .SatisFiyati8 = 0
                    .SatisFiyati9 = 0
                    .SatisFiyati10 = 0
                    .SatisFiyati11 = 0
                    .SatisFiyati12 = 0
                    .SatisFiyati13 = 0
                    .SatisFiyati14 = 0
                    .SatisFiyati15 = 0
                    .SatisFiyati16 = 0
                    .SatisFiyati17 = 0
                    .SatisFiyati18 = 0
                    .SatisFiyati19 = 0
                    .SatisFiyati20 = 0

                    .UrunKartiAktif = True
                    .UrunAgirligi = 1
                    .TahminiTeslimSuresi = 1
                    .TahminiTeslimSuresiGoster = True

                    '.GtipKodu = "6405"

                    '.Resimler = oVaryasyonResimler
                    .Ozellikler = oVaryasyonOzellikler
                End With

                oVaryasyonlar.Add(oVaryasyon)
            Loop
            oSQL.oReader.Close()


            oSQL.cSQLQuery = "select top 1 stokno, cinsaciklamasi " +
                        " from stok with (NOLOCK) " +
                        " where stokno = '" + cStokNo.Trim + "' "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then

                cUrunAciklama = StrConv(oSQL.SQLReadString("cinsaciklamasi") + " " + cRenk, VbStrConv.ProperCase)

                With oUrunKarti
                    ' mecburi alanlar
                    .TedarikciKodu = cStokNo.Trim + "/" + cRenk.Trim ' zorunlu tekil anahtar şeklinde olması gerekiyor

                    .ID = nTiciMaxID ' 0 ise yeni ürünkartı ekler, sıfırdan büyük ise o id'ye sahip olan ürün kartını günceller
                    .UrunSayfaAdresi = cUrunSayfaAdresi

                    If nTiciMaxID = 0 Then
                        .UrunAdi = cUrunAciklama
                        .Aciklama = cUrunAciklama
                        .AramaAnahtarKelime = oSQL.SQLReadString("cinsaciklamasi")
                    End If

                    .SeoAnahtarKelime = oSQL.SQLReadString("cinsaciklamasi")
                    .SeoSayfaAciklama = oSQL.SQLReadString("cinsaciklamasi")
                    .SeoSayfaBaslik = oSQL.SQLReadString("cinsaciklamasi")
                    .SeoNoFollow = False
                    .SeoNoIndex = False

                    .AdwordsAciklama = oSQL.SQLReadString("cinsaciklamasi")
                    .AdwordsKategori = "AYAKKABI"
                    .AdwordsTip = oSQL.SQLReadString("cinsaciklamasi")

                    .AnaKategori = oConnection.cTiciMaxAnaKategori.Trim             ' oKategoriler.Item(0).Kod
                    .AnaKategoriID = CInt(oConnection.cTiciMaxAnaKategoriID)        ' oKategoriler.Item(0).ID

                    .Marka = oConnection.cTiciMaxMarka.Trim                         ' oMarkalar.Item(0).Tanim
                    .MarkaID = CInt(oConnection.cTiciMaxMarkaID)                    ' oMarkalar.Item(0).ID

                    .TedarikciID = CInt(oConnection.cTiciMaxTedarikciID)            ' oTedarikciler.Item(0).ID

                    .SatisBirimi = oConnection.cTiciMaxSatisBirimi.Trim
                    .UcretsizKargo = False

                    ' opsiyonel alanlar
                    .Aktif = True
                    .YeniUrun = True
                    .Vitrin = False
                    .FirsatUrunu = False
                    .OnYazi = oSQL.SQLReadString("cinsaciklamasi")

                    .FBStoreGoster = True
                    .UyeAlimMin = 1
                    .UyeAlimMax = 999
                    .UrunAdediMinimumDeger = 1
                    .UrunAdediVarsayilanDeger = 1
                    .UrunAdediKademeDeger = 1
                    .PuanDeger = 0
                    .EklemeTarihi = Now.Date

                    .IndirimliFiyatOzellik = 0 ' Bu alanda 3 farklı değer girebiliyoruz  0 = devamlı indirim, 1 = stok adedine göre indirim(1 olduğu zaman IndirimliFiyatOzellikStok1 ve IndirimliFiyatOzellikStok2 alanlarının doldurulması zorunludur bu iki stok adedi arasındayken indirim geçerli olur) , 2 = Tarihe Göre indirim (2 olduğu zaman       IndirimliFiyatOzellikTarih1 ve IndirimliFiyatOzellikTarih2 alanları doldurulması zorunludur. Bu iki tarih aralığında indirim geçerli olur.)    
                    .IndirimliFiyatOzellikStok1 = 21
                    .IndirimliFiyatOzellikStok2 = 23
                    .IndirimliFiyatOzellikTarih1 = Now.Date
                    .IndirimliFiyatOzellikTarih2 = Now.Date
                    .MaksTaksitSayisi = 9

                    .OzelAlan1 = oSQL.SQLReadString("cinsaciklamasi") ' eklenecek extra özellikleri temsil eder
                    .OzelAlan2 = ""
                    .OzelAlan3 = ""
                    .OzelAlan4 = ""
                    .OzelAlan5 = ""

                    .TahminiTeslimSuresi = 1
                    .TahminiTeslimSuresiGoster = True

                    .Kategoriler = oCategories
                    .Varyasyonlar = oVaryasyonlar
                    '.Resimler = oResimler
                    '.Etiketler = oUrunKartiEtiketListe
                    '.TeknikDetaylar = oUrunKartiTeknikDetayListe
                End With

                oUrunKartlari.Add(oUrunKarti)

                nSonuc = oClient.SaveUrun(oConnection.cTiciMaxYetkiKodu, oUrunKartlari, oUrunKartiAyar, oVaryasyonAyar)
            End If
            oSQL.oReader.Close()

            If oUrunKartlari(0).ID <> 0 Then

                SendProduct = True

                For Each oVaryasyon In oVaryasyonlar

                    oSQL.cSQLQuery = "update stokrb2 " +
                                    " set ticimaxid = " + oUrunKartlari(0).ID.ToString + ", " +
                                    " ticimaxvaryasyonid = " + oVaryasyon.ID.ToString + ", " +
                                    " ticimaxupdate = getdate() " +
                                    " where stokno = '" + cStokNo.Trim + "' " +
                                    " and renk = '" + cRenk.Trim + "' " +
                                    " and beden = '" + cBeden.Trim + "' "

                    oSQL.SQLExecute()
                Next
            End If

            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp(ex.Message, "SendProduct",,, ex)
        End Try
    End Function

    Public Sub GetList(nCase As Integer, Optional oForm As Form = Nothing)

        Try
            Dim ofrmListView As New frmListView

            Select Case nCase
                Case 1
                    oKategoriler = oClient.SelectKategori(oConnection.cTiciMaxYetkiKodu, 0, "")
                    ofrmListView.init(oKategoriler, "Kategoriler", oForm)

                Case 2
                    oMarkalar = oClient.SelectMarka(oConnection.cTiciMaxYetkiKodu, 0)
                    ofrmListView.init(oMarkalar, "Markalar", oForm)

                Case 3
                    oTedarikciler = oClient.SelectTedarikci(oConnection.cTiciMaxYetkiKodu, 0)
                    ofrmListView.init(oTedarikciler, "Tedarikciler", oForm)

                Case 4
                    Dim oUrunFiltre As New UrunFiltre

                    With oUrunFiltre
                        .Aktif = -1
                        .Firsat = -1
                        .Indirimli = -1
                        .Vitrin = -1
                        .KategoriID = 0
                        .MarkaID = 0
                        .UrunKartiID = 0
                        .ToplamStokAdediBas = 0
                        .ToplamStokAdediSon = 999999
                        .TedarikciID = 0
                    End With

                    Dim nUrunSayisi As Integer = oClient.SelectUrunCount(oConnection.cTiciMaxYetkiKodu, oUrunFiltre)

                    If nUrunSayisi = 0 Then
                        MsgBox("Ürün bulunamadı")
                        Exit Sub
                    End If

                    Dim oUrunSayfalama As New UrunSayfalama

                    With oUrunSayfalama
                        .BaslangicIndex = 0
                        .KayitSayisi = nUrunSayisi
                        .SiralamaDegeri = "ID"
                        .SiralamaYonu = "ASC"
                    End With

                    Dim oUrun As New List(Of UrunKarti)

                    oUrun = oClient.SelectUrun(oConnection.cTiciMaxYetkiKodu, oUrunFiltre, oUrunSayfalama)
                    ofrmListView.init(oUrun, "Ürünler", oForm)

                Case 5
                    Dim oTDG As New List(Of TeknikDetayGrup)
                    oTDG = oClient.SelectTeknikDetayGrup(oConnection.cTiciMaxYetkiKodu, 0, "")
                    ofrmListView.init(oTDG, "Teknik Detay Grupları", oForm)

                Case 6
                    Dim oTDO As New List(Of TeknikDetayOzellik)
                    oTDO = oClient.SelectTeknikDetayOzellik(oConnection.cTiciMaxYetkiKodu, 0, "")
                    ofrmListView.init(oTDO, "Teknik Detay Özellik", oForm)

                Case 7
                    Dim oTDD As New List(Of TeknikDetayDeger)
                    oTDD = oClient.SelectTeknikDetayDeger(oConnection.cTiciMaxYetkiKodu, 0, "")
                    ofrmListView.init(oTDD, "Teknik Detay Değer", oForm)

                Case 8
                    Dim oEtiket As New List(Of Etiket)
                    oEtiket = oClient.SelectEtiket(oConnection.cTiciMaxYetkiKodu, 0)
                    ofrmListView.init(oEtiket, "Etiketler", oForm)

                Case 9
                    If nUrunID <= 0 Then
                        MsgBox("Ürün seçiniz")
                        Exit Sub
                    End If
                    Dim oUrunEtiket As New List(Of UrunEtiket)
                    oUrunEtiket = oClient.SelectUrunEtiket(oConnection.cTiciMaxYetkiKodu, 0, nUrunID)
                    ofrmListView.init(oUrunEtiket, "Ürün Etiketleri", oForm)

                Case 10
                    Dim oVaryasyonFiltre As New VaryasyonFiltre

                    With oVaryasyonFiltre
                        .Aktif = -1
                        .UrunID = -1
                        .UrunKartiID = -1
                    End With

                    Dim nVaryasyonSayisi As Integer = oClient.SelectVaryasyonCount(oConnection.cTiciMaxYetkiKodu, oVaryasyonFiltre)

                    If nVaryasyonSayisi = 0 Then
                        MsgBox("Varyasyon bulunamadı")
                        Exit Sub
                    End If

                    Dim oUrunSayfalama As New UrunSayfalama

                    With oUrunSayfalama
                        .BaslangicIndex = 0
                        .KayitSayisi = nVaryasyonSayisi
                        .SiralamaDegeri = "ID"
                        .SiralamaYonu = "ASC"
                    End With

                    Dim oSelectVaryasyonAyar As New SelectVaryasyonAyar

                    With oSelectVaryasyonAyar
                        .KategoriGetir = True
                    End With

                    Dim oVaryasyon As New List(Of Varyasyon)

                    oVaryasyon = oClient.SelectVaryasyon(oConnection.cTiciMaxYetkiKodu, oVaryasyonFiltre, oUrunSayfalama, oSelectVaryasyonAyar)
                    ofrmListView.init(oVaryasyon, "Varyasyonlar", oForm)

                Case 11
                    Dim oAsortiGrup As New List(Of AsortiGrup)
                    oAsortiGrup = oClient.SelectAsortiGrup(oConnection.cTiciMaxYetkiKodu, 0)
                    ofrmListView.init(oAsortiGrup, "Asorti Grupları", oForm)

                Case 12
                    Dim oEkSecenekDeger As New List(Of EkSecenekDeger)
                    oEkSecenekDeger = oClient.SelectEkSecenekDeger(oConnection.cTiciMaxYetkiKodu, 0, "")
                    ofrmListView.init(oEkSecenekDeger, "Ek Seçenek Değerler", oForm)

                Case 13
                    Dim oEkSecenekGrup As New List(Of EkSecenekGrup)
                    oEkSecenekGrup = oClient.SelectEkSecenekGrup(oConnection.cTiciMaxYetkiKodu, "")
                    ofrmListView.init(oEkSecenekGrup, "Ek Seçenek Grupları", oForm)

                Case 14
                    Dim oParaBirimi As New List(Of ParaBirimi)
                    oParaBirimi = oClient.SelectParaBirimi(oConnection.cTiciMaxYetkiKodu, 0)
                    ofrmListView.init(oParaBirimi, "Dövizler", oForm)

            End Select

        Catch ex As Exception
            ErrDisp(ex.Message, "UrunClassGetList",, True, ex)
        End Try
    End Sub
End Class
