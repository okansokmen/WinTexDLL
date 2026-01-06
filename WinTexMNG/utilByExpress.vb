Option Explicit On

Imports RestSharp
Imports System.Net.Http

Module utilByExpress


    Public Class ByExpressSonDurum
        Public id As Integer                    ' 98,
        Public evrak_no As String               ' "TS201238"
        Public evrak_turu As String             ' Gönderim / İade
        Public musteri_takip_no As String       ' "2020"
        Public kargo_takip_no As String         ' "ABF9191D17"
        Public son_hareket_tarihi As String     ' "2020-07-15T17:01:19.363"
        Public islem_nedeni As Integer          ' 0
        Public islem_neden_aciklama As String   ' null
        Public son_durum As Integer             ' 70
        Public son_durum_aciklama As String     ' "Kargo Teslim Edildi", 
        Public kargof As String
        Public onay_tarihi As String            ' "2020-07-15T16:05:28.8"
        Public gonderi_yukleme_tarihi As String ' "1901-01-01T00:00:00"
        Public teslim_alan As String            ' "GHGFH FGHFGHFG"
        Public teslim_tarihi As String          ' "2020-07-15T17:01:19.363"
        Public cikis_sube As String             ' "GÖNDEREN ŞUBE"
        Public cikis_sube_telefon As String     ' "ASASD@JSHJKH.COM"
        Public teslim_sube As String            ' "ALICI ŞUBE"
        Public teslim_sube_telefon As String    ' "ASASD@JSHJKH.COM"
        Public altckn As String
        Public alvrgn As String
        Public alici As String                  ' "kjhkjhRSOY"
        Public yetkili As String
        Public telefon As String                ' "535656876"
        Public eposta As String
        Public ulke As String                   ' "Türkiye"
        Public sehir As String                  ' "ANKARA"
        Public ilce As String                   ' "AKYURT"
        Public mahalle As String
        Public site As String
        Public adres As String                  ' "MİLLET 87609870987098 7h u B BLOK DAİRE 7"
        Public odeme_sekli As String            ' "NAKİT"
        Public odeme_kimden As String           ' "PÖ"
        Public teslim_sekli As String           ' "Şubeden Teslim"
        Public tasima_durum As String           ' "Gönderim"
        Public altcari As String
        Public ozelalan1 As String
        Public ozelalan2 As String
        Public urun_bedeli As String            ' "100"
        Public fiyat As Double                  ' 18.8800
        Public kargodan_gelen As Double         ' 0.0
        Public firmaya_odenen As Double         ' 0.0
        Public kontrollu_teslimat As String
        Public adet As String                   ' "1"
        Public desi As String                   ' "2"
        Public icerik As String                 ' "TUNİK"
    End Class

    Public Class oSonDurumlar
        Public sonDurumlar As List(Of ByExpressSonDurum)
    End Class

    Public Class ByExpressRequest
        Public SozlesmeNumarasi As Integer    ' zorunlu
        Public KullaniciAdi As String         ' zorunlu
        Public Sifre As String                ' zorunlu
        Public TakipNo As String              ' zorunlu
    End Class

    Public Class ByExpressSendOrder
        Public SozlesmeNumarasi As Integer    ' zorunlu
        Public KullaniciAdi As String         ' zorunlu
        Public Sifre As String                ' zorunlu
        Public TakipNo As String              ' zorunlu
        Public Alici As String                ' zorunlu
        Public Tckn As String
        Public Vrgn As String
        Public Yetkili As String
        Public Il As String                   ' zorunlu
        Public Ilce As String                 ' zorunlu
        Public Mahalle As String
        Public Site As String
        Public Adres As String                ' zorunlu
        Public Telefon As String              ' zorunlu
        Public EPosta As String
        Public TeslimYeri As Integer          ' 1-Adrese Teslim 2-Şubede Teslim
        Public KontrolluTeslimat As String    ' Ürün teslim edilmeden önce alıcının kargoyu açıp kontrol etmesi için bu hizmet kullanılır. Ptt kargo tercihinde bunu kullanabilirsiniz. Kullanmak için KT değeri girilmelidir.
        Public OdemeKimden As String          ' Gönderici ödemeli UG, Alıcı ödemeli UA.
        Public UrunBedeli As String
        Public OdemeTuru As Integer           ' 1-Nakit 2-Kredi K.
        Public Adet As Integer                ' Kaç adet paket gönderileceği girilir. Eğer birden fazla paket var ise bu paketlerin desileri aralarında : sembolü bırakılarak girilmelidir. Örneğin 3 adet paket için 2:0:6 şeklinde desi girilmelidir
        Public Desi As String                 ' Gönderilen Dosya ise sıfır 0 girilebilir
        Public Icerik As String               ' Kargo içeriği girilir.
        Public AltCari As String              ' Gönderici bir şubeniz, bayiniz, ya da size bağlı bir yer ise, onun; Bünyemizdeki KULLANICI Adı Alt cari listeniz konusunda müşteri temsilcinizle mutabakat yapmalısınız
        Public OzelAlan1 As String            ' Bu alana girdiğiniz bilgilere göre Müşteri portalımızdan raporlar çekebilirsiniz
        Public OzelAlan2 As String            ' Bu alana girdiğiniz bilgilere göre Müşteri portalımızdan raporlar çekebilirsiniz
    End Class

    Public Class ByExpressReturn
        Public success As Boolean      ' True ise işleminiz başarılı, false ise işleminiz hata almıştır
        Public errorMessage As String  ' İşleminiz hata aldıysa, hata mesajı
        Public value As Object         ' İşleminiz başarılı ise işlem sonucu olan KARGO TAKİP NUMARASI bu alanda dönülecektir
    End Class

    Private Function GetGonderiDurumAciklama(nKod As Integer) As String
        ' İade-Devir-Yönlendirme-Hasar Neden Kodları
        ' Gönderi Durum Kodları
        Dim cSonuc As String = ""

        Select Case nKod
            Case 10 : cSonuc = "Kargo Çıkış Şubede"
            Case 20 : cSonuc = "Kargo Çıkış Transfer Merkezinde"
            Case 30 : cSonuc = "Kargo Yola Çıktı"
            Case 40 : cSonuc = "Kargo Varış Transfer Merkezinde"
            Case 50 : cSonuc = "Kargo Varış Şubede"
            Case 60 : cSonuc = "Kargo Kuryede"
            Case 70 : cSonuc = "Kargo Teslim Edildi"
            Case 80 : cSonuc = "Kargo Iade Süreci Başlatıldı"
            Case 90 : cSonuc = "Kargo Hasar Süreci Başlatıldı"
            Case 95 : cSonuc = "Kargo Yönlendirme Süreci Başlatıldı"
        End Select

        GetGonderiDurumAciklama = cSonuc
    End Function

    Private Function GetIadeSureciNedenAciklama(nKod As Integer) As String
        ' İade-Devir-Yönlendirme-Hasar Neden Kodları
        ' İade Süreci Neden Kodları
        Dim cSonuc As String = ""

        Select Case nKod
            Case 10 : cSonuc = "Alıcı Kargoyu Kabul Etmiyor"
            Case 20 : cSonuc = "Alıcı Sehir Dısında"
            Case 30 : cSonuc = "Alıcı Ücreti Ödeyemediginden"
            Case 40 : cSonuc = "Alıcı Ürünü Açıp Bakmak İstiyor"
            Case 50 : cSonuc = "Alıcı Adreste Tanınmıyor"
            Case 60 : cSonuc = "Alıcı Yerinde Yok"
            Case 70 : cSonuc = "Dağıtım Alanı Dışında"
        End Select

        GetIadeSureciNedenAciklama = cSonuc
    End Function

    Private Function GetDagitimSubedeBeklemeAciklama(nKod As Integer) As String
        ' İade-Devir-Yönlendirme-Hasar Neden Kodları
        ' Dağıtım Şubede Bekleme Neden Kodları (Devir) 
        Dim cSonuc As String = ""

        Select Case nKod
            Case 110 : cSonuc = "Dağıtıma Hazırlanıyor"
            Case 120 : cSonuc = "Özel Teslim Günü Bekliyor"
            Case 130 : cSonuc = "Alıcı Sehir Dısında"
            Case 140 : cSonuc = "Ücretinden Dolayı"
            Case 150 : cSonuc = "Afet-Pandemi"
            Case 160 : cSonuc = "Alıcı Adreste Tanınmıyor"
            Case 170 : cSonuc = "Alıcı Yerinde Yok"
            Case 180 : cSonuc = "Hasar Tespit Tazmin İşlemleri Devam Ediyor"
            Case 190 : cSonuc = "Göndericinin İade Onayı Bekleniyor"
        End Select

        GetDagitimSubedeBeklemeAciklama = cSonuc
    End Function

    Private Function GetHasarSureciNedenAciklama(nKod As Integer) As String
        ' İade-Devir-Yönlendirme-Hasar Neden Kodları
        ' Hasar Süreci Neden Kodları
        Dim cSonuc As String = ""

        Select Case nKod
            Case 400 : cSonuc = "Kırılmış"
            Case 410 : cSonuc = "Yırtılmış"
            Case 420 : cSonuc = "Paket Açılmış"
            Case 430 : cSonuc = "Bozulmuş"
            Case 440 : cSonuc = "Islanmış"
        End Select

        GetHasarSureciNedenAciklama = cSonuc
    End Function

    Private Function GetYonlendirmeSureciNedenAciklama(nKod As Integer) As String
        ' İade-Devir-Yönlendirme-Hasar Neden Kodları
        ' Yönlendirme Süreci Neden Kodları
        Dim cSonuc As String = ""

        Select Case nKod
            Case 300 : cSonuc = "Alıcı Adreste Tanınmıyor"
            Case 310 : cSonuc = "Adres hatalı"
        End Select

        GetYonlendirmeSureciNedenAciklama = cSonuc
    End Function

    Public Function ByExpressGonderiYukle(cSiparisNo As String, Optional ByRef cKargoTakipNo As String = "", Optional ByRef cErrorMessage As String = "", Optional ByVal lTest As Boolean = False) As Boolean

        ByExpressGonderiYukle = False

        Try
            If CheckFirmaCalisilmasin("BYEXPRESS") Then Exit Function
            If Not GetServiceConnectionParameters(cSiparisNo, oConnection.cByExpressApiUserName, oConnection.cByExpressApiPassword, oConnection.cByExpressSozlesmeNo, "BYEXPRESS") Then Exit Function

            Dim cURL As String = ""
            Dim oSend As New ByExpressSendOrder
            Dim oSQL As New SQLServerClass
            Dim cKargoFirmasi As String = ""
            Dim cAraciKargo As String = ""
            Dim cKargocuTahsilatYapmaz As String = "H"
            Dim cOdemeTipi As String = ""

            oSend.SozlesmeNumarasi = CInt(oConnection.cByExpressSozlesmeNo) ' zorunlu
            oSend.KullaniciAdi = oConnection.cByExpressApiUserName ' zorunlu
            oSend.Sifre = oConnection.cByExpressApiPassword ' zorunlu
            oSend.TakipNo = cSiparisNo.Trim ' zorunlu

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select top 1 tarih, adi, soyadi, ili, " +
                            " ilcesi, telefon, adres, kargofirmasi, odemetipi, " +
                            " teslimsekli, finaltutar, " +
                            " kargocutahsilatyapmaz = (select top 1 kargocutahsilatyapmaz " +
                                                    " from odemetipi with (NOLOCK) " +
                                                    " where kod = sipperakende.odemetipi) " +
                            " from sipperakende with (NOLOCK) " +
                            " where (siparisno = '" + cSiparisNo.Trim + "' or siparisno2 = '" + cSiparisNo.Trim + "') "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then

                cKargoFirmasi = oSQL.SQLReadString("kargofirmasi")
                cOdemeTipi = oSQL.SQLReadString("odemetipi")
                cKargocuTahsilatYapmaz = oSQL.SQLReadString("kargocutahsilatyapmaz")

                oSend.Alici = oSQL.SQLReadString("adi") + " " + oSQL.SQLReadString("soyadi") ' zorunlu
                oSend.Tckn = ""
                oSend.Vrgn = ""
                oSend.Yetkili = oSQL.SQLReadString("adi") + " " + oSQL.SQLReadString("soyadi")
                oSend.Il = oSQL.SQLReadString("ili") ' zorunlu
                oSend.Ilce = oSQL.SQLReadString("ilcesi") ' zorunlu
                oSend.Mahalle = ""
                oSend.Site = ""
                oSend.Adres = oSQL.SQLReadString("adres") ' zorunlu
                oSend.Telefon = oSQL.SQLReadString("telefon") ' zorunlu
                oSend.EPosta = ""
                oSend.KontrolluTeslimat = "" ' Ürün teslim edilmeden önce alıcının kargoyu açıp kontrol etmesi için bu hizmet kullanılır. Ptt kargo tercihinde bunu kullanabilirsiniz. Kullanmak için KT değeri girilmelidir.

                ' 1-Adrese Teslim 2-Şubede Teslim
                Select Case oSQL.SQLReadString("teslimsekli")
                    Case "SUBE TESLIM"
                        oSend.TeslimYeri = 2
                    Case Else
                        oSend.TeslimYeri = 1
                End Select

                ' Gönderici ödemeli UG, Alıcı ödemeli UA.
                Select Case oSQL.SQLReadString("odemetipi")
                    Case "PESIN - UCRET ALICI"
                        oSend.OdemeKimden = "UA"
                    Case Else
                        oSend.OdemeKimden = "UG"
                End Select

                ' 1-Nakit 2-Kredi K.
                Select Case oSQL.SQLReadString("odemetipi")
                    Case "KAPIDA KREDI KARTI"
                        oSend.OdemeTuru = 2
                    Case Else
                        oSend.OdemeTuru = 1
                End Select

                If cKargocuTahsilatYapmaz = "E" Or cOdemeTipi = "PESIN - UCRET ALICI" Or cOdemeTipi = "PESIN - UCRET GONDERICI" Then
                    oSend.UrunBedeli = "0"
                Else
                    oSend.UrunBedeli = SQLWriteDecimal(oSQL.SQLReadDouble("finaltutar"))
                End If

                oSend.Adet = 1 ' Kaç adet paket gönderileceği girilir. Eğer birden fazla paket var ise bu paketlerin desileri aralarında : sembolü bırakılarak girilmelidir. Örneğin 3 adet paket için 2:0:6 şeklinde desi girilmelidir
                oSend.Desi = "3" ' Gönderilen Dosya ise sıfır 0 girilebilir
                oSend.Icerik = "AYAKKABI" ' Kargo içeriği girilir.
                oSend.AltCari = "" ' Gönderici bir şubeniz, bayiniz, ya da size bağlı bir yer ise, onun; Bünyemizdeki KULLANICI Adı Alt cari listeniz konusunda müşteri temsilcinizle mutabakat yapmalısınız
                oSend.OzelAlan1 = "" ' Bu alana girdiğiniz bilgilere göre Müşteri portalımızdan raporlar çekebilirsiniz
                oSend.OzelAlan2 = "" ' Bu alana girdiğiniz bilgilere göre Müşteri portalımızdan raporlar çekebilirsiniz
            End If
            oSQL.oReader.Close()

            If cKargoFirmasi.Trim.ToLower <> "byexpress" Then
                cErrorMessage = "Siparis kargo firmasi byexpress olmalidir"
                Exit Function
            End If

            If lTest Then
                cURL = "http://apidemo.byexpresskargo.net/Gonderi/Yukle"
            Else
                cURL = "https://api.byexpresskargo.net/Gonderi/Yukle"
            End If

            Dim oClient = New RestClient(cURL)

            Dim oRequest = New RestRequest(Method.POST)

            oRequest.AddHeader("cache-control", "no-cache")
            oRequest.AddHeader("content-type", "application/json")
            oRequest.AddParameter("application/json", Newtonsoft.Json.JsonConvert.SerializeObject(oSend), ParameterType.RequestBody)

            Dim oResponse As IRestResponse = oClient.Execute(oRequest)
            Dim oReturn As New ByExpressReturn
            oReturn = Newtonsoft.Json.JsonConvert.DeserializeObject(oResponse.Content, oReturn.GetType())

            If oReturn Is Nothing Then
                cErrorMessage = "Servis sonuc donmedi"
            Else
                If oReturn.success Then

                    cKargoTakipNo = oReturn.value.ToString.Trim

                    oSQL.cSQLQuery = "update sipperakende " +
                                    " set kargotakipno = '" + SQLWriteString(cKargoTakipNo, 30) + "', " +
                                    " kargoyakayityollandi = 'E', " +
                                    " kargoyakayityollanmatarihi = getdate() " +
                                    " where (siparisno = '" + cSiparisNo.Trim + "' or siparisno2 = '" + cSiparisNo.Trim + "') "

                    oSQL.SQLExecute()

                    oSQL.cSQLQuery = "select top 1 aracikargo " +
                                    " from firma with (NOLOCK) " +
                                    " where firma = 'BYEXPRESS' "

                    cAraciKargo = oSQL.DBReadString()

                    ' update wintex 
                    If cAraciKargo.Trim <> "" Then

                        oSQL.cSQLQuery = "update sipperakende " +
                                    " set aracikargo = '" + cAraciKargo + "' " +
                                    " where (siparisno = '" + cSiparisNo.Trim + "' or siparisno2 = '" + cSiparisNo.Trim + "') " +
                                    " and (aracikargo is null or aracikargo = '') "

                        oSQL.SQLExecute()
                    End If

                    ByExpressGonderiYukle = True
                Else
                    cErrorMessage = oReturn.errorMessage.ToString.Trim
                End If
            End If

            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp("ByExpressGonderiYukle",,,, ex)
        End Try
    End Function

    Public Function ByExpressGonderiIptal(cSiparisNo As String, Optional ByRef cErrorMessage As String = "", Optional ByVal lTest As Boolean = False) As Boolean

        ' gönderi iptal kendi takip numaramız ile yapılır

        ByExpressGonderiIptal = False

        Try
            If CheckFirmaCalisilmasin("BYEXPRESS") Then Exit Function
            If Not GetServiceConnectionParameters(cSiparisNo, oConnection.cByExpressApiUserName, oConnection.cByExpressApiPassword, oConnection.cByExpressSozlesmeNo, "BYEXPRESS") Then Exit Function

            Dim cURL As String = ""
            Dim oSend As New ByExpressRequest
            Dim oSQL As SQLServerClass
            Dim cSiparisNo2 As String = ""

            cSiparisNo2 = GetSiparisNo2(cSiparisNo)

            If cSiparisNo2.Trim = "" Then
                cSiparisNo2 = cSiparisNo.Trim
            End If

            oSend.SozlesmeNumarasi = CInt(oConnection.cByExpressSozlesmeNo)     ' zorunlu
            oSend.KullaniciAdi = oConnection.cByExpressApiUserName              ' zorunlu
            oSend.Sifre = oConnection.cByExpressApiPassword                     ' zorunlu
            oSend.TakipNo = cSiparisNo2.Trim                                    ' zorunlu

            If lTest Then
                cURL = "http://apidemo.byexpresskargo.net/Gonderi/Iptal"
            Else
                cURL = "https://api.byexpresskargo.net/Gonderi/Iptal"
            End If

            Dim oClient = New RestClient(cURL)

            Dim oRequest = New RestRequest(Method.POST)

            oRequest.AddHeader("cache-control", "no-cache")
            oRequest.AddHeader("content-type", "application/json")
            oRequest.AddParameter("application/json", Newtonsoft.Json.JsonConvert.SerializeObject(oSend), ParameterType.RequestBody)

            Dim oResponse As IRestResponse = oClient.Execute(oRequest)
            Dim oReturn As New ByExpressReturn
            oReturn = Newtonsoft.Json.JsonConvert.DeserializeObject(oResponse.Content, oReturn.GetType())

            If oReturn Is Nothing Then
                cErrorMessage = "Servis sonuc donmedi"
            Else
                If oReturn.success Then
                    oSQL = New SQLServerClass
                    oSQL.OpenConn()

                    oSQL.cSQLQuery = "update sipperakende " +
                                    " set iptal = 'E', " +
                                    " iptaltarih = getdate() " +
                                    " where (siparisno = '" + cSiparisNo.Trim + "' or siparisno2 = '" + cSiparisNo.Trim + "') "
                    oSQL.SQLExecute()

                    oSQL.CloseConn()
                    oSQL = Nothing

                    ByExpressGonderiIptal = True
                Else
                    cErrorMessage = oReturn.errorMessage.ToString.Trim
                End If
            End If

        Catch ex As Exception
            ErrDisp("ByExpressGonderiIptal",,,, ex)
        End Try
    End Function

    Public Function ByExpressSonDurumuAl(Optional cSiparisNo As String = "", Optional ByVal cKargoTakipNo As String = "", Optional ByRef cSonuc As String = "", Optional ByRef cErrorMessage As String = "", Optional ByVal lTest As Boolean = False) As Boolean

        ByExpressSonDurumuAl = False

        Try
            If CheckFirmaCalisilmasin("BYEXPRESS") Then Exit Function
            If Not GetServiceConnectionParameters(cSiparisNo, oConnection.cByExpressApiUserName, oConnection.cByExpressApiPassword, oConnection.cByExpressSozlesmeNo, "BYEXPRESS") Then Exit Function

            Dim cURL As String = ""
            Dim oSend As New ByExpressRequest
            Dim cCikisSube As String = ""
            Dim cCikisSubeTelefon As String = ""
            Dim cTeslimSube As String = ""
            Dim cTeslimSubeTelefon As String = ""
            Dim cDurum As String = ""
            Dim oSQL As SQLServerClass
            Dim cKargoTakipUrl As String = ""
            Dim nKargoStatu As Double = 0
            Dim nKargoKgDesi As Double = 0
            Dim nKargoUrunBedeli As Double = 0
            Dim cKargoStatuTarihi As String = ""
            Dim dKargoStatuTarihi As Date = #1/1/1950#
            Dim cGonderiCikisTarihi As String = ""
            Dim dGonderiCikisTarihi As Date = #1/1/1950#
            Dim cEvrakTuru As String = ""
            Dim cSiparisNo2 As String = ""

            cSiparisNo2 = GetSiparisNo2(cSiparisNo)

            If cSiparisNo2.Trim = "" Then
                cSiparisNo2 = cSiparisNo.Trim
            End If

            oSend.SozlesmeNumarasi = CInt(oConnection.cByExpressSozlesmeNo) ' zorunlu
            oSend.KullaniciAdi = oConnection.cByExpressApiUserName ' zorunlu
            oSend.Sifre = oConnection.cByExpressApiPassword ' zorunlu

            If cKargoTakipNo.Trim = "" Then
                If cSiparisNo2.Trim <> "" Then
                    oSend.TakipNo = cSiparisNo2.Trim ' zorunlu
                    If lTest Then
                        cURL = "http://apidemo.byexpresskargo.net/Gonderi/MusteriTakipNoIleSonDurumuAl"
                    Else
                        cURL = "https://api.byexpresskargo.net/Gonderi/MusteriTakipNoIleSonDurumuAl"
                    End If
                End If
            Else
                oSend.TakipNo = cKargoTakipNo.Trim ' zorunlu
                If lTest Then
                    cURL = "http://apidemo.byexpresskargo.net/Gonderi/KargoTakipNoIleSonDurumuAl"
                Else
                    cURL = "https://api.byexpresskargo.net/Gonderi/KargoTakipNoIleSonDurumuAl"
                End If
            End If

            If cURL.Trim = "" Then Exit Function

            Dim oClient = New RestClient(cURL)

            Dim oRequest = New RestRequest(Method.POST)

            oRequest.AddHeader("cache-control", "no-cache")
            oRequest.AddHeader("content-type", "application/json")
            oRequest.AddParameter("application/json", Newtonsoft.Json.JsonConvert.SerializeObject(oSend), ParameterType.RequestBody)

            Dim oResponse As IRestResponse = oClient.Execute(oRequest)

            If oResponse.IsSuccessful Then

                Dim oReturn As New ByExpressReturn
                oReturn = Newtonsoft.Json.JsonConvert.DeserializeObject(oResponse.Content, oReturn.GetType())

                If oReturn Is Nothing Then
                    cErrorMessage = "Servis sonuc donmedi"
                Else
                    If oReturn.success Then
                        ByExpressSonDurumuAl = True

                        Dim oReturn2 As New ByExpressReturn
                        oReturn2 = Newtonsoft.Json.JsonConvert.DeserializeObject(oResponse.Content, oReturn2.GetType())

                        cSonuc = oReturn2.value.ToString

                        Dim oSonDurum As New oSonDurumlar
                        oSonDurum = Newtonsoft.Json.JsonConvert.DeserializeObject(oReturn2.value.ToString, oSonDurum.GetType())

                        If oSonDurum Is Nothing Then
                            cErrorMessage = cSiparisNo + " siparisi için servis sonuc vermedi"
                        Else
                            If oSonDurum.sonDurumlar.Count > 0 Then
                                cEvrakTuru = oSonDurum.sonDurumlar(0).evrak_turu.ToString
                                cKargoTakipNo = oSonDurum.sonDurumlar(0).kargo_takip_no.ToString
                                cCikisSube = oSonDurum.sonDurumlar(0).cikis_sube.ToString
                                cCikisSubeTelefon = oSonDurum.sonDurumlar(0).cikis_sube_telefon.ToString
                                cTeslimSube = oSonDurum.sonDurumlar(0).teslim_sube.ToString
                                cTeslimSubeTelefon = oSonDurum.sonDurumlar(0).teslim_sube_telefon.ToString
                                cDurum = oSonDurum.sonDurumlar(0).son_durum_aciklama.ToString
                                cKargoTakipUrl = "https://www.byexpresskargo.com.tr/kargotakip/" + cKargoTakipNo
                                nKargoStatu = WebReadDouble(oSonDurum.sonDurumlar(0).son_durum)
                                dKargoStatuTarihi = WebReadDate(oSonDurum.sonDurumlar(0).son_hareket_tarihi, 2)
                                cKargoStatuTarihi = oSonDurum.sonDurumlar(0).son_hareket_tarihi.ToString
                                dGonderiCikisTarihi = WebReadDate(oSonDurum.sonDurumlar(0).onay_tarihi, 2)
                                cGonderiCikisTarihi = oSonDurum.sonDurumlar(0).onay_tarihi.ToString
                                nKargoKgDesi = WebReadDouble(oSonDurum.sonDurumlar(0).desi)
                                nKargoUrunBedeli = WebReadDouble(oSonDurum.sonDurumlar(0).urun_bedeli)

                                If nKargoStatu = 70 And cEvrakTuru = "İade" Then
                                    ' göndericiye iade olayına da 70 geliyor
                                    nKargoStatu = 95
                                End If

                                Select Case nKargoStatu
                                    Case 10 : cDurum = "CIKIS SUBEDE"
                                    Case 20 : cDurum = "YOLDA"
                                    Case 30 : cDurum = "YOLDA"
                                    Case 40 : cDurum = "YOLDA"
                                    Case 50 : cDurum = "VARIS SUBEDE"
                                    Case 60 : cDurum = "DAGITIMDA"
                                    Case 70 : cDurum = "TESLIM EDILDI"
                                    Case 80 : cDurum = "SORUNLU"
                                    Case 90 : cDurum = "IADE"
                                    Case 95 : cDurum = "IADE"
                                    Case Else
                                        If cKargoTakipNo.Trim <> "" Then
                                            cDurum = "SIPARIS HAZIRLANIYOR"
                                        End If
                                End Select

                                oSQL = New SQLServerClass
                                oSQL.OpenConn()

                                oSQL.cSQLQuery = "update sipperakende " +
                                                " set kargotakipno = '" + SQLWriteString(cKargoTakipNo, 30) + "', " +
                                                " kargotakipurl = '" + SQLWriteString(cKargoTakipUrl, 150) + "', " +
                                                " kargosondurumkodu = '" + SQLWriteDecimal(nKargoStatu) + "', " +
                                                " kargosondurumtarihi = getdate(), " +
                                                " kargokgdesi = " + SQLWriteDecimal(nKargoKgDesi) + ", " +
                                                " kargotahsilattutari = " + SQLWriteDecimal(nKargoUrunBedeli) + ", " +
                                                " kargostatu = '" + SQLWriteString(cDurum, 100) + "', " +
                                                " kargostatutarihi = '" + SQLWriteDateTime(dKargoStatuTarihi) + "', " +
                                                " kargostatutarihiorjinal = '" + SQLWriteString(cKargoStatuTarihi, 30) + "', " +
                                                " gondericikistarihi = '" + SQLWriteDateTime(dGonderiCikisTarihi) + "', " +
                                                " gondericikistarihiorjinal = '" + SQLWriteString(cGonderiCikisTarihi, 30) + "', " +
                                                " cikis_sube = '" + SQLWriteString(cCikisSube, 30) + "', " +
                                                " cikis_sube_telefon = '" + SQLWriteString(cCikisSubeTelefon, 30) + "', " +
                                                " teslim_sube = '" + SQLWriteString(cTeslimSube, 30) + "', " +
                                                " teslim_sube_telefon = '" + SQLWriteString(cTeslimSubeTelefon, 30) + "' " +
                                                " where (siparisno = '" + cSiparisNo.Trim + "' or siparisno2 = '" + cSiparisNo.Trim + "') "

                                oSQL.SQLExecute()

                                Select Case nKargoStatu
                                    Case 70
                                        ' teslim edildi
                                        oSQL.cSQLQuery = "update sipperakende " +
                                                    " set kapandi = 'E', " +
                                                    " kapanmatarihi = '" + SQLWriteDateTime(dKargoStatuTarihi) + "', " +
                                                    " iade = null, " +
                                                    " iadetarihi = null " +
                                                    " where (siparisno = '" + cSiparisNo.Trim + "' or siparisno2 = '" + cSiparisNo.Trim + "') "

                                        oSQL.SQLExecute()
                                    Case 90, 95
                                        ' iade
                                        oSQL.cSQLQuery = "update sipperakende " +
                                                    " set iade = 'E', " +
                                                    " iadetarihi = '" + SQLWriteDateTime(dKargoStatuTarihi) + "', " +
                                                    " kapandi = null, " +
                                                    " kapanmatarihi = null " +
                                                    " where (siparisno = '" + cSiparisNo.Trim + "' or siparisno2 = '" + cSiparisNo.Trim + "') "

                                        oSQL.SQLExecute()
                                    Case Else
                                        oSQL.cSQLQuery = "update sipperakende " +
                                                    " set iade = null, " +
                                                    " iadetarihi = null, " +
                                                    " kapandi = null, " +
                                                    " kapanmatarihi = null " +
                                                    " where (siparisno = '" + cSiparisNo.Trim + "' or siparisno2 = '" + cSiparisNo.Trim + "') "

                                        oSQL.SQLExecute()

                                End Select

                                oSQL.CloseConn()
                                oSQL = Nothing
                            End If
                        End If
                    Else
                        cErrorMessage = oReturn.errorMessage.ToString.Trim
                    End If
                End If
            Else
                cErrorMessage = oResponse.StatusDescription
            End If

        Catch ex As Exception
            ErrDisp("ByExpressSonDurumuAl",,,, ex)
        End Try
    End Function

End Module
