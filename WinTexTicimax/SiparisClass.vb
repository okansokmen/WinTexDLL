Option Explicit On

Imports WinTexTicimax.SiparisServis
Imports System.Net
Imports System.Windows.Forms

Public Class SiparisClass

    Dim oClient As SiparisServisClient

    Public Sub New()

        Dim oBinding As System.ServiceModel.BasicHttpBinding = GetBinding("BasicHttpBinding_ISiparisServis")
        Dim oEndPointAddress As System.ServiceModel.EndpointAddress = GetEndpointAddress(oConnection.cTiciMaxApiSiparisUrl)

        oClient = New SiparisServisClient(oBinding, oEndPointAddress)
    End Sub

    Public Sub CloseClient()
        On Error Resume Next
        oClient.Close()
    End Sub

    Public Sub GetList(nCase As Integer, Optional oForm As Form = Nothing)

        Try
            Dim ofrmListView As New frmListView
            Dim oSiparis As List(Of WebSiparis)
            Dim oSiparisOdemeTipleri As List(Of SiparisOdemeTipleri)
            Dim oWebSiparisSayfalama As WebSiparisSayfalama
            Dim oWebSiparisFiltre As WebSiparisFiltre

            Select Case nCase
                Case 1
                    ' sadece ön sipariş durumundakileri indiriyor
                    oWebSiparisFiltre = GetWebSiparisFilitre(0)
                    oWebSiparisSayfalama = GetWebSiparisSayfalama()

                    oSiparis = New List(Of WebSiparis)
                    oSiparis = oClient.SelectSiparis(oConnection.cTiciMaxYetkiKodu, oWebSiparisFiltre, oWebSiparisSayfalama)
                    ofrmListView.init(oSiparis, "Siparişler", oForm, 3)

                Case 2
                    oSiparisOdemeTipleri = New List(Of SiparisOdemeTipleri)
                    oSiparisOdemeTipleri = oClient.GetOdemeTipleri(oConnection.cTiciMaxYetkiKodu)
                    ofrmListView.init(oSiparisOdemeTipleri, "Sipariş Ödeme Tipleri", oForm)
            End Select

        Catch ex As Exception
            ErrDisp(ex.Message, "GetList",,, ex)
        End Try
    End Sub

    Private Function GetWebSiparisFilitre(Optional nSiparisDurumu As Integer = -1) As WebSiparisFiltre

        GetWebSiparisFilitre = Nothing

        Try
            Dim oWebSiparisFiltre As New WebSiparisFiltre
            Dim oWebSiparisEntegrasyon As New WebSiparisEntegrasyon

            With oWebSiparisEntegrasyon
                .AlanDeger = ""
                .Deger = ""
                .EntegrasyonKodu = ""
                .EntegrasyonParamsAktif = True
                .TabloAlan = ""
                .Tanim = ""
            End With

            With oWebSiparisFiltre
                .EntegrasyonParams = oWebSiparisEntegrasyon
                .EntegrasyonAktarildi = -1
                .IptalEdilmisUrunler = True
                .FaturaNo = ""

                .OdemeDurumu = -1
                ' Ödeme Durumu Değişkenleri
                ' Onay bekliyor 0
                ' Onaylandı 1
                ' Hatalı 2
                ' İade edilmiş 3
                ' İptal edilmiş 4

                .OdemeTipi = -1
                ' Ödeme Tipi Değişkenleri
                ' Kredi kartı 0
                ' Havale 1
                ' Kapıda ödeme nakit 2
                ' Kapıda ödeme kredi kartı 3
                ' Mobil ödeme 4
                ' Bkm express 5
                ' Paypal 6
                ' Cari 7
                ' Mail order 8
                ' Ipara 9
                ' Nakit 10
                ' Payuoneclick 11
                ' Cari kredi 12
                ' Garantipay 13
                ' PayuBkmexpress 14
                ' Nestpay 15
                ' Paycell 16
                ' Iyzipay 17
                ' Hopi 18
                ' Paybyme 19
                ' Hediye çeki 20
                ' Paygurumobil 21
                ' Paynet 22
                ' Telr 23
                ' Compay 24
                ' Paytr 25
                ' Maximum mobil 26
                ' Magazada öde 27

                .SiparisDurumu = nSiparisDurumu
                ' Sipariş Durumu Değişkenleri
                ' Ön sipariş 0
                ' Onay bekliyor 1
                ' Onaylandı 2
                ' Ödeme bekliyor 3
                ' Paketleniyor 4
                ' Tedarik ediliyor 5
                ' Kargoya verildi 6
                ' Teslim edildi 7
                ' Iptal edildi 8
                ' Iade edildi 9
                ' Silinmiş 10
                ' Iade talebi alındı 11
                ' Iade ulaştu ödeme yapılacak 12
                ' Iade ödemesi yapıldı 13
                ' Teslimat öncesi iptal talebi 14
                ' Iptal talebi 15
                ' Kısmi iade talebi 16
                ' Kısmi iade yapıldı 17

                .SiparisID = -1
                .SiparisKaynagi = ""
                .SiparisKodu = ""
                .SiparisTarihiBas = New Date(2020, 1, 1)
                .SiparisTarihiSon = New Date(2099, 1, 1)
                .TedarikciID = -1
                .UyeID = -1
                .SiparisNo = ""
                .UyeTelefon = ""
            End With

            GetWebSiparisFilitre = oWebSiparisFiltre

        Catch ex As Exception
            ErrDisp(ex.Message, "GetWebSiparisFilitre",,, ex)
        End Try
    End Function

    Private Function GetWebSiparisSayfalama() As WebSiparisSayfalama

        GetWebSiparisSayfalama = Nothing

        Try
            Dim oWebSiparisSayfalama As New WebSiparisSayfalama

            With oWebSiparisSayfalama
                .BaslangicIndex = 0
                .KayitSayisi = 500000
                .SiralamaDegeri = "id"
                .SiralamaYonu = "Desc"
            End With

            GetWebSiparisSayfalama = oWebSiparisSayfalama

        Catch ex As Exception
            ErrDisp(ex.Message, "GetWebSiparisSayfalama",,, ex)
        End Try
    End Function

    Public Function ReadSiparisFromTiciMax() As Boolean
        ' bütün yeni eklenmiş siparişleri TiciMAx tan indir WinTex e yükle
        ReadSiparisFromTiciMax = False

        Try
            Dim oSiparis As WebSiparis
            Dim oSiparisler As List(Of WebSiparis)
            Dim oWebSiparisSayfalama As WebSiparisSayfalama
            Dim oWebSiparisFiltre As WebSiparisFiltre

            ' sadece ön sipariş durumundakileri indiriyor
            oWebSiparisFiltre = GetWebSiparisFilitre(0)
            oWebSiparisSayfalama = GetWebSiparisSayfalama()

            oSiparisler = New List(Of WebSiparis)
            oSiparisler = oClient.SelectSiparis(oConnection.cTiciMaxYetkiKodu, oWebSiparisFiltre, oWebSiparisSayfalama)

            For Each oSiparis In oSiparisler
                WriteSiparisToWinTex(oSiparis)
            Next

            ReadSiparisFromTiciMax = True

        Catch ex As Exception
            ErrDisp(ex.Message, "ReadSiparisFromTiciMax",,, ex)
        End Try
    End Function

    Public Function WriteSiparisToWinTex(oSiparis As WebSiparis) As Boolean
        ' Siparis Durumunu eğer aktarım başarılıysa Onaylandi yapıyor
        ' Eğer aktarım yapılamıyorsa Iptal yapıyor
        WriteSiparisToWinTex = False

        Try
            Dim nCnt As Integer = 0
            Dim oSQL As SQLServerClass
            Dim oUrun2 As WebSiparisUrun
            Dim oDetay As WebSiparisUrunEkSecenekOzellik
            Dim nID As Integer = 0
            Dim cAdi As String = ""
            Dim cSoyadi As String = ""
            Dim nDurum As Integer = 0
            Dim dTarih As Date = #1/1/1950#
            Dim cKargocu As String = ""
            Dim cSiparisDurumu As String = ""
            Dim nTutar As Double = 0
            Dim cOzelAlan1 As String = ""
            Dim cOzelAlan2 As String = ""
            Dim cOzelAlan3 As String = ""
            Dim cAdres As String = ""
            Dim cTelefon As String = ""
            Dim cIl As String = ""
            Dim cIlce As String = ""
            Dim cPostakodu As String = ""
            Dim cSemt As String = ""
            Dim cUlke As String = ""
            Dim aUrun() As String
            Dim cUrun As String = ""
            Dim cStokNo As String = ""
            Dim cRenk As String = ""
            Dim cBeden As String = ""
            Dim nAdet As Double = 0
            Dim nFiyat As Double = 0
            Dim cNotlar As String = ""
            Dim cSiparisNo As String = ""
            Dim oOdemeListe As New List(Of WebSiparisOdeme)
            Dim nOdemeTipi As Integer = 0
            Dim cOdemeTipi As String = oConnection.cTiciMaxSiparisTipi
            Dim lOdemeonayli As Boolean = False

            Dim oSiparisServisClient = New SiparisServisClient
            Dim oSetSiparisDurumRequest As New SetSiparisDurumRequest
            Dim oWebSiparisDurumlari As New WebSiparisDurumlari

            nID = WebReadInt(oSiparis.ID)
            cAdi = WebReadString(oSiparis.UyeAdi)
            cSoyadi = WebReadString(oSiparis.UyeSoyadi)

            'Onay bekliyor 0
            'Onaylandı 1
            'Hatalı 2
            'İade edilmiş 3
            nDurum = WebReadInt(oSiparis.Durum)

            dTarih = oSiparis.DuzenlemeTarihi
            cSiparisDurumu = WebReadString(oSiparis.SiparisDurumu)
            nTutar = WebReadDouble(oSiparis.Tutar)
            cNotlar = WebReadString(oSiparis.SiparisNotu)
            cKargocu = WebReadString(oSiparis.KargoFirmaTanim)

            oOdemeListe = oSiparisServisClient.SelectSiparisOdeme(oConnection.cTiciMaxYetkiKodu, nID, 0)

            If Not IsNothing(oOdemeListe) Then
                For nCnt = 0 To oOdemeListe.Count - 1
                    If oOdemeListe(nCnt).Onaylandi = 1 Then
                        lOdemeonayli = True
                        nOdemeTipi = oOdemeListe(nCnt).OdemeTipi
                        Select Case nOdemeTipi
                            Case 3
                                cOdemeTipi = "KAPIDA KREDI KARTI"
                            Case 2
                                cOdemeTipi = "KAPIDA NAKIT"
                            Case Else
                                cOdemeTipi = "PESIN - UCRET GONDERICI"
                        End Select
                    End If
                Next
            End If

            If Not lOdemeonayli Then
                ' odemesi onaylanmamış
                Exit Function
            End If

            If nDurum = 2 Then
                ' hatalı siparişleri alma
                Exit Function
            End If

            If InStr(cKargocu, "MNG") > 0 Then
                cKargocu = "MNG"
            ElseIf InStr(cKargocu, "BYEXPRESS") > 0 Then
                cKargocu = "BYEXPRESS"
            ElseIf InStr(cKargocu, "PTT") > 0 Then
                cKargocu = "PTT"
            ElseIf InStr(cKargocu, "YURTICI") > 0 Then
                cKargocu = "YURTICI"
            Else
                cKargocu = WebReadString(oConnection.cTiciMaxKargoFirmasi)
            End If

            cOzelAlan1 = WebReadString(oSiparis.OzelAlan1)
            cOzelAlan2 = WebReadString(oSiparis.OzelAlan2)
            cOzelAlan3 = WebReadString(oSiparis.OzelAlan3)

            ' Teslimat Adresi
            cTelefon = WebReadString(oSiparis.TeslimatAdresi.AliciTelefon)
            cIl = WebReadString(oSiparis.TeslimatAdresi.Il)
            cIlce = WebReadString(oSiparis.TeslimatAdresi.Ilce)
            cPostakodu = WebReadString(oSiparis.TeslimatAdresi.PostaKodu)
            cSemt = WebReadString(oSiparis.TeslimatAdresi.Semt)
            cUlke = WebReadString(oSiparis.TeslimatAdresi.Ulke.Tanim)
            cAdres = Replace(WebReadString(oSiparis.TeslimatAdresi.Adres), vbLf, " ") + " " + cSemt + " " + cUlke

            If InStr(oSiparis.Urunler(0).StokKodu, "/") = 0 Then
                ' ürün stok kodlaması WinTex te tanımsız
                oWebSiparisDurumlari = WebSiparisDurumlari.Iptal
                oSetSiparisDurumRequest.Durum = oWebSiparisDurumlari
                oSetSiparisDurumRequest.KargoTakipNo = ""
                oSetSiparisDurumRequest.MailBilgilendir = False
                oSetSiparisDurumRequest.SiparisID = nID
                oClient.SetSiparisDurum(oConnection.cTiciMaxYetkiKodu, oSetSiparisDurumRequest)

                CreateLog("WinTexTiciMaxSiparisDownloadLog", "Siparisteki ürünlerin stok kodlaması stokno+renk olarak ayrıştırılamıyor : " + oSiparis.Urunler(0).StokKodu.Trim + " / " + nID.ToString)
                Exit Function
            End If

            oSQL = New SQLServerClass
            oSQL.OpenConn()

            oSQL.cSQLQuery = "select top 1 siparisno " +
                            " from sipperakende with (NOLOCK) " +
                            " where ticimaxid = " + nID.ToString

            If oSQL.CheckExists Then
                ' bu sipariş var
                cSiparisNo = oSQL.DBReadString
            Else
                ' bu sipariş yeni
                cSiparisNo = "88" + oSQL.GetSequenceFisNo("siparisfisno")

                oSQL.cSQLQuery = "insert sipperakende (siparisno, tarih, siparistipi, notlar, adi, " +
                                " soyadi, ili, ilcesi, telefon, adres, " +
                                " kargofirmasi, odemetipi, teslimsekli, firma, tutar, " +
                                " finaltutar, creationuser, creationdate, modificationuser, modificationdate, " +
                                " kargostatu, aracikargo, ticimaxid, ticimaxupdate ) "

                oSQL.cSQLQuery = oSQL.cSQLQuery +
                                " values ('" + cSiparisNo + "', " +
                                " '" + SQLWriteDate(dTarih) + "', " +
                                " '" + SQLWriteString(oConnection.cTiciMaxSiparisTipi, 30) + "', " +
                                " '" + SQLWriteString(cNotlar) + "', " +
                                " '" + SQLWriteString(cAdi, 50) + "', "

                oSQL.cSQLQuery = oSQL.cSQLQuery +
                                " '" + SQLWriteString(cSoyadi, 50) + "', " +
                                " '" + SQLWriteString(cIl, 50) + "', " +
                                " '" + SQLWriteString(cIlce, 50) + "', " +
                                " '" + SQLWriteString(cTelefon, 50) + "', " +
                                " '" + SQLWriteString(cAdres) + "', "

                oSQL.cSQLQuery = oSQL.cSQLQuery +
                                " '" + SQLWriteString(cKargocu) + "', " +
                                " '" + SQLWriteString(cOdemeTipi, 30) + "', " +
                                " '" + SQLWriteString(oConnection.cTiciMaxTeslimSekli, 30) + "', " +
                                " '" + SQLWriteString(oConnection.cTiciMaxKomisyoncuFirma, 30) + "', " +
                                SQLWriteDecimal(nTutar) + ", "

                oSQL.cSQLQuery = oSQL.cSQLQuery +
                                SQLWriteDecimal(nTutar) + ", " +
                                " '" + SQLWriteString(oConnection.cTiciMaxUserName, 30) + "', " +
                                " getdate(), " +
                                " '" + SQLWriteString(oConnection.cTiciMaxUserName, 30) + "', " +
                                " getdate(), "

                oSQL.cSQLQuery = oSQL.cSQLQuery +
                                " 'SIPARIS ALINDI', " +
                                " '" + SQLWriteString(oConnection.cTiciMaxAraciKargo, 30) + "', " +
                                SQLWriteDecimal(nID) + ", " +
                                " getdate() ) "

                oSQL.SQLExecute()

                oSQL.cSQLQuery = "delete sipperakendelines " +
                                " where siparisno = '" + cSiparisNo + "' "

                oSQL.SQLExecute()

                oSQL.cSQLQuery = "delete sipperakendelines2 " +
                                " where siparisno = '" + cSiparisNo + "' "

                oSQL.SQLExecute()

                nDurum = 0

                For Each oUrun2 In oSiparis.Urunler
                    cUrun = WebReadString(oUrun2.StokKodu)
                    aUrun = Split(cUrun, "/")
                    cStokNo = aUrun(0)
                    cRenk = aUrun(1)
                    nAdet = WebReadDouble(oUrun2.Adet)
                    nFiyat = WebReadDouble(oUrun2.SatisAniSatisFiyat)

                    For Each oDetay In oUrun2.EkSecenekList
                        cBeden = WebReadString(oDetay.Tanim)
                    Next

                    oSQL.cSQLQuery = "insert sipperakendelines (siparisno, stokno, renk, beden, adet, fiyat, doviz) " +
                                                " values ('" + cSiparisNo + "', " +
                                                " '" + SQLWriteString(cStokNo, 30) + "', " +
                                                " '" + SQLWriteString(cRenk, 30) + "', " +
                                                " '" + SQLWriteString(cBeden, 30) + "', " +
                                                SQLWriteDecimal(nAdet) + ", " +
                                                SQLWriteDecimal(nFiyat) + ", " +
                                                " 'TL' )  "
                    oSQL.SQLExecute()

                    For nCnt = 1 To nAdet

                        oSQL.cSQLQuery = "insert into sipperakendelines2 (siparisno, stokno, renk, beden, adet, " +
                                        " fiyat, doviz) "

                        oSQL.cSQLQuery = oSQL.cSQLQuery +
                                        " values ('" + cSiparisNo + "', " +
                                        " '" + SQLWriteString(cStokNo, 30) + "', " +
                                        " '" + SQLWriteString(cRenk, 30) + "', " +
                                        " '" + SQLWriteString(cBeden, 30) + "', " +
                                        " 1, "

                        oSQL.cSQLQuery = oSQL.cSQLQuery +
                                        SQLWriteDecimal(nFiyat) + ", " +
                                        " 'TL' ) "

                        oSQL.SQLExecute()
                    Next
                Next
            End If

            oSQL.CloseConn()

            oClient.SetSiparisAktarildi(oConnection.cTiciMaxYetkiKodu, nID)

            oWebSiparisDurumlari = WebSiparisDurumlari.Onaylandi
            oSetSiparisDurumRequest.Durum = oWebSiparisDurumlari
            oSetSiparisDurumRequest.KargoTakipNo = ""
            oSetSiparisDurumRequest.MailBilgilendir = False
            oSetSiparisDurumRequest.SiparisID = nID
            oClient.SetSiparisDurum(oConnection.cTiciMaxYetkiKodu, oSetSiparisDurumRequest)

            WriteSiparisToWinTex = True

            CreateLog("WinTexTiciMaxSiparisDownloadLog", "Siparis : " + cSiparisNo + " / " + nID.ToString)

        Catch ex As Exception
            ErrDisp(ex.Message, "WriteSiparisToWinTex",,, ex)
        End Try
    End Function

    Public Function WriteSiparislerToTiciMax() As Boolean

        WriteSiparislerToTiciMax = False

        Try
            Dim oSQL As New SQLServerClass

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select siparisno " +
                        " from sipperakende with (NOLOCK) " +
                        " where (iptal = 'H' or iptal = '' or iptal is null) " +
                        " and (kapandi = 'H' or kapandi = '' or kapandi is null) " +
                        " and ticimaxid is not null " +
                        " and ticimaxid <> 0 " +
                        " and siparisno is not null " +
                        " and siparisno <> '' " +
                        " order by siparisno "

            oSQL.GetSQLReader()

            Do While oSQL.oReader.Read
                WriteSiparisToTiciMax(oSQL.SQLReadString("siparisno"))
            Loop
            oSQL.oReader.Close()
            oSQL.CloseConn()

            WriteSiparislerToTiciMax = True

        Catch ex As Exception
            ErrDisp(ex.Message, "WriteSiparislerToTiciMax",,, ex)
        End Try
    End Function

    Public Function WriteSiparisToTiciMax(cSiparisNo As String) As Boolean

        WriteSiparisToTiciMax = False

        Try
            Dim oSQL As New SQLServerClass
            Dim oSetSiparisDurumRequest As New SetSiparisDurumRequest
            Dim oWebSiparisDurumlari As New WebSiparisDurumlari
            Dim nSiparisID As Integer = 0

            If cSiparisNo.Trim = "" Then Exit Function

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select top 1 kargostatu, " +
                            " iptal, iptaltarih, kapandi, kapanmatarihi, hazirlaniyor, hazirlanmatarihi, " +
                            " sevkedildi, sevktarihi, kargoyakayityollandi, kargoyakayityollanmatarihi, " +
                            " iade, iadetarihi, yazdirildi, yazdirmatarihi, " +
                            " kargotakipno, cikis_sube, cikis_sube_telefon, teslim_sube, teslim_sube_telefon, " +
                            " kargosondurumkodu, kargosondurumtarihi, kargokgdesi, kargostatutarihi, kargotakipurl, " +
                            " komisyontutari, kargotutari, tahsilatkomisyontutari, kargotahsilattutari, " +
                            " odemeonaylandi, odemeonaytarihi, gondericikistarihi, ticimaxid, ticimaxupdate " +
                            " from sipperakende with (NOLOCK) " +
                            " where siparisno = '" + cSiparisNo.Trim + "' " +
                            " and ticimaxid is not null " +
                            " and ticimaxid <> 0 "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then

                nSiparisID = CInt(oSQL.SQLReadDouble("ticimaxid"))

                If oSQL.SQLReadString("kargotakipno") <> "" Then
                    oClient.SetSiparisKargoyaVerildi(oConnection.cTiciMaxYetkiKodu, nSiparisID)
                    oClient.SaveKargoTakipNo(oConnection.cTiciMaxYetkiKodu, nSiparisID, "", oSQL.SQLReadString("kargotakipno"), oSQL.SQLReadString("kargotakipurl"), "", True)
                End If

                Select Case oSQL.SQLReadString("kargostatu")
                    Case "SIPARIS HAZIRLANIYOR"
                        oWebSiparisDurumlari = WebSiparisDurumlari.Paketleniyor
                    Case "CIKIS SUBEDE", "VARIS SUBEDE", "DAGITIMDA"
                        oWebSiparisDurumlari = WebSiparisDurumlari.KargoyaVerildi
                    Case "TESLIM EDILDI"
                        oWebSiparisDurumlari = WebSiparisDurumlari.TeslimEdildi
                        oClient.SetSiparisTeslimEdildi(oConnection.cTiciMaxYetkiKodu, nSiparisID)
                    Case "İPTAL EDİLDİ"
                        oWebSiparisDurumlari = WebSiparisDurumlari.Iptal
                    Case "SORUNLU"
                        oWebSiparisDurumlari = WebSiparisDurumlari.TeslimEdilemedi
                    Case Else
                        oWebSiparisDurumlari = WebSiparisDurumlari.Onaylandi
                End Select

                oSetSiparisDurumRequest.Durum = oWebSiparisDurumlari
                oSetSiparisDurumRequest.KargoTakipNo = ""
                oSetSiparisDurumRequest.MailBilgilendir = False
                oSetSiparisDurumRequest.SiparisID = nSiparisID
                oClient.SetSiparisDurum(oConnection.cTiciMaxYetkiKodu, oSetSiparisDurumRequest)

            End If
            oSQL.oReader.Close()

            If nSiparisID <> 0 Then
                oSQL.cSQLQuery = "update sipperakende " +
                                " set ticimaxupdate = getdate() " +
                                " where ticimaxid = '" + nSiparisID.ToString + "' "

                oSQL.SQLExecute()

                CreateLog("WinTexTiciMaxSiparisUploadLog", "Siparis : " + cSiparisNo + " / " + nSiparisID.ToString)
            End If

            oSQL.CloseConn()

            WriteSiparisToTiciMax = True

        Catch ex As Exception
            ErrDisp(ex.Message, "WriteSiparisToTiciMax",,, ex)
        End Try
    End Function

End Class
