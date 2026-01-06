Option Explicit On

Imports System.Globalization

Module utilMNG

    ' gecit ayakkabı kargo entegrasyonu
    ' OAuth Yeniden Yönlendirme URL'si
    ' http://78.188.110.130
    ' MNG İsemci Güvenlik Dizgisi
    ' C5eM3bY5hB7rU7mO5dS8rD6dU6xP1fH6oX7mM0yC6hA2bY1oK5

    ' Müşteriler tarafından Depoya ONAY KODU ile yapılacak olan gönderiler için 
    ' Kullanılacak Method = " GelecekIadeSiparisKayit"
    ' Test Servis URLdir https//service.mngkargo.com.tr/musterikargosiparis/musterisiparisnew.asmx
    ' Onay kodu / Sipariş No  ile data gönderimi sağlandıktan sonra ; Takip işlemi için aşağıda belirtilen servis kullanılabilecektir. 
    ' Kullanılacak Method = " GelecekIadeSiparisKontrol" 
    ' Test için kullanabileceğiniz  Kullanıcı Adı ve Şifresi
    ' Test Kullanıcı Adı     : 301458719
    ' Test Kullanıcı şifresi : Test..1234

    Private Structure oMNGData
        Dim cAraciKargo As String
        Dim cUserName As String
        Dim cPassword As String
        Dim cSiparisler As String
    End Structure

    Public Function MNGIadeSiparisKayit2(cSiparisNo As String, Optional ByRef cSonuc As String = "", Optional ByRef cErrorMessage As String = "") As Boolean

        cSonuc = ""
        MNGIadeSiparisKayit2 = False

        Try
            If CheckFirmaCalisilmasin("MNG") Then Exit Function
            If Not GetServiceConnectionParameters(cSiparisNo, oConnection.cMNGApiUserName, oConnection.cMNGApiPassword,, "MNG") Then Exit Function

            Dim oSQL As New SQLServerClass

            Dim oBinding As System.ServiceModel.BasicHttpBinding = GetBinding()
            Dim oEndPointAddress = New System.ServiceModel.EndpointAddress(oConnection.cMNGIadeUrl)
            'Dim oMNG As New mngiade.  ' (oBinding, oEndPointAddress)
            'oMNG.InnerChannel.OperationTimeout = New TimeSpan(0, 12, 0, 0, 0)


        Catch ex As Exception
            ErrDisp("MNGIadeSiparisKayit2",,,, ex)
        End Try
    End Function

    Public Function MNGSendOrder2(cSiparisNo As String, Optional ByRef cSonuc As String = "", Optional ByRef cErrorMessage As String = "") As Boolean

        cSonuc = ""
        MNGSendOrder2 = False

        Try
            If CheckFirmaCalisilmasin("MNG") Then Exit Function
            If Not GetServiceConnectionParameters(cSiparisNo, oConnection.cMNGApiUserName, oConnection.cMNGApiPassword,, "MNG") Then Exit Function

            Dim oSQL As New SQLServerClass

            Dim oBinding As System.ServiceModel.BasicHttpBinding = GetBinding()
            Dim oEndPointAddress = New System.ServiceModel.EndpointAddress(oConnection.cMNGApiUrl)
            Dim oMNG As New musterikargosiparis.MusteriKargoSiparisSoapClient(oBinding, oEndPointAddress)
            oMNG.InnerChannel.OperationTimeout = New TimeSpan(0, 12, 0, 0, 0)

            Dim nDesi As Double = 0
            Dim nKG As Double = 0
            Dim cGonderi As String = ""
            Dim cGonderiler As String = ""
            Dim nAdet As Double = 0
            Dim nTutar As Double = 0
            Dim nCnt As Integer = 0
            Dim cKargoFirmasi As String = ""
            Dim cAraciKargo As String = ""

            ' Ticari gönderiler için mecburi olup irsaliye no veya faturalı irsaliye numarasıdır. 
            ' İrsaliye numarası sonradan kargo takibi için de kullanılabilmektedir.
            Dim pChIrsaliyeNo As String = ""

            ' Gönderinin mali değeridir. 
            ' Özellikle kapıda tahsilatlı gönderilerde bu parametre ile ürün bedeli verilmelidir.
            ' Ondalik ayraç olarak virgül “,” kullanılmalıdır.
            Dim pPrKiymet As String = ""

            ' Gönderi üzerine yapıştırılacak olan barkot etiketinin açılımıdır. 
            ' Sipariş Numarası ile aynı olması önerilir. 
            ' Toplu halde verilen gönderiler, şube yetkili personelin önüne geldiğinde üzerindeki barkot okutularak sistemdeki datanın ekrana gelmesi sağlanmaktadır. 
            ' Bu nedenle 6 karakterden uzun sipariş numarası kullanan müşterilerimizin gönderi üzerine yapıştırılacak barkot hazırlamaları tavsiye edilir.
            Dim pChBarkod As String = ""

            ' Maksimum 200 karakter uzunluğa kadar açıklayıcı bilgidir.
            Dim pChIcerik As String = "Ayakkabi"

            ' NORMAL 
            ' GUNICI 
            ' AKSAM_TESLIMAT 
            ' ONCELIKLI
            Dim pGonderiHizmetSekli As String = "NORMAL"

            ' 1 - Adrese Teslim
            ' 2 - Alıcısı Haberli 
            ' 3 - Telefon İhbarlı
            Dim pTeslimSekli As Integer = 1

            ' Alıcı için standart 2 tip sms seçeğini bulunmaktadır. Sms hizmeti ücretlidir. 
            ' 1 - Gönderi Varış Şubesine Ulaştığında 
            ' 2 – Gönderi Fatura Kesildiğinde 
            ' 3 - ikisindede
            ' 0 - SMS gönderme
            Dim pFlAlSms As Integer = 0

            ' Gönderi yerine ulaştığında göndericiye Sms gönderilip gönderilmeyeceği belirtilen parametredir. Sms hizmeti ücretlidir
            ' Sms gönderilmeyecekse 0 gönderilecekse 1 değeri gönderilir
            Dim pFlGnSms As Integer = 0

            ' Dosya/Zarf gönderilerinde Kilo ve Desi bilgisi 0 (sıfır) kabul edilir
            ' Desi = Round ((En x Boy x Yükseklik) / 3000,0)
            ' Her parça bilgisi şu formatta olmalıdır. → Kilo : Desi : KgDesi:İçerik:ParçaNo:
            ' pKargoParcaList = Parça1_String ; Parça2_String ; Parça3_String ;
            ' pKargoParcaList = 1:1:1:Urun1:1: #:PARCABARKODTEXT1:; 1:1:1:Urun2:2:#:PARCABARKODTEXT2:;
            Dim pKargoParcaList As String = ""

            ' Alıcı müşterinin MNG Kargo sistemindeki tekil numarasıdır. Bilinmiyorsa boş bırakılabilir. (Bu numara MNG Kargo tarafından belirlenmektedir.)
            Dim pAliciMusteriMngNo As String = ""

            ' Alıcı müşterinin sizin tarafınızdan belirlenmiş olan Bayi Numarasıdır.
            ' Bunun için sürekli gönderi yaptığınız alıcı bayilerinizin daha önceden belirlenen Excel formatında MNG Kargo ile paylaşılmış ve ilgili bayiler MNG Kargo sistemine kayıt edilmiş olmalıdır.
            ' Verilen bayi numarası alıcılarınız içerisinde tekil bir değer olmalıdır.
            Dim pAliciMusteriBayiNo As String = ""

            ' Gönderinin Alıcı adıdır. 
            ' Her ne kadar bayi numarası verildiğinde gerek olmasa bile bu ve sonradan açıklayacağımız bazı adres değerlerini belirtmeniz oluşabilecek bazı hataların önüne geçecektir. 
            Dim pAliciMusteriAdi As String = ""

            ' Hazırlanacak gönderinin sonradan takibini sağlayacak en önemli anahtar değerdir. 
            ' Sonradan açıklayacağımız Kargo Takip ve Durum sorgulama web servisimizde bu Sipariş Numarasını kullanarak kargo ve durum bilgilerine ulaşabilirsiniz. 
            ' Aynı sipariş numarası farklı bir gönderide kullanılamaz. Tekil olmalıdır.
            Dim pChSiparisNo As String = ""

            ' Gönderici Ödemeli : P 
            ' Alıcı Ödemeli : U 
            ' Platform Öder : PL (PlatformKod ve PlatformKısaKod Bilgisi Gereklidir.)
            Dim pLuOdemeSekli As String = "P"

            ' Standart olarak çift tırnak içinde “0” şeklinde sıfır değeri gönderilir. 
            ' Kayıtlı adres dışına gönderiler için “1” değeri verilir. 
            ' Alıcı bayi numarası verildiğinde, MNG sisteminde kayıtlı olan adresinin dışında bir adrese gönderim yapılmak istendiğinde “1” değeri gönderilir.
            Dim pFlAdresFarkli As String = "0"

            ' Alıcı müşterinin(bayinin) adresinin il bilgisi gönderilir.
            ' İSTANBUL gibi Türkçe karakter ve büyük harfle il bilgisi.
            Dim pChIl As String = ""

            ' Alıcı müşterinin(bayinin) adresinin ilçe bilgisi gönderilir.
            ' ŞİŞLİ gibi Türkçe karakter ve büyük harfle ilçe bilgisi.
            Dim pChIlce As String = ""

            ' Alıcı müşterinin(bayinin) adresinin il ve ilçe hariç kalan kısmı gönderilir. 
            ' Adres kriterlerini Semt, Mahalle, Meydan / Bulvar, Cadde Sokak… gibi ayrıştırarak gönderemeyecek müşterilerimiz bu adres bölümünü kullanabilir.
            Dim pChAdres As String = ""

            ' Alıcı müşterinin adres detay bilgileridir. 
            ' Boş bırakılabilecekleri gibi, kayıtlı olmayan bayilerde daha hızlı ve doğru varış şubesi tespit edilmesinde fayda sağlamaktalar.
            Dim pChSemt As String = ""
            Dim pChMahalle As String = ""
            Dim pChMeydanBulvar As String = ""
            Dim pChCadde As String = ""
            Dim pChSokak As String = ""

            ' Ev Telefonu
            Dim pChTelEv As String = ""

            ' Cep Telefonu
            Dim pChTelCep As String = ""

            ' İş Telefonu
            Dim pChTelIs As String = ""

            ' Fax No
            Dim pChFax As String = ""

            ' Alıcı eMail
            Dim pChEmail As String = ""

            ' Alıcı vergi dairesi adıdır.
            Dim pChVergiDairesi As String = "SAHIS" ' şahıs

            ' Alıcıya ait vergi numarasıdır.
            Dim pChVergiNumarasi As String = "11111111111"

            ' 0 veya 1 değeri alabilir. 
            ' Ürün bedeli kapıda tahsil edilecek gönderilerde 1 değeri verilir. 
            ' 1 değeri verilecek ise, prKiymet(Ürün fatura bedeli) değeri boş olmamalıdır.
            Dim pFlKapidaOdeme As Integer = 1

            ' NAKIT veya KREDI_KARTI değerlerini alabilir. 
            ' Ürün bedeli kapıda tahsil edilecek gönderilerde tahsilat şeklinin belirtilmesi için kullanılır.
            Dim pMalBedeliOdemeSekli As String = ""

            ' Bilgi aktarımı yapılacak olan platforma ait MngKargo tarafındaki tanım kodudur. 
            ' N11, GG, HB, TRND değeri alabilir.
            ' Platform gönderilerine ait bilgilerin elektronik olarak ilgili platformlara gönderilmesi için kulanılmaktadır.
            Dim pPlatformKisaAdi As String = ""

            ' İlgili platforma ait ; kullanılmakta olan satış yada kampanya kodu bilgisidir.
            Dim pPlatformSatisKodu As String = ""

            ' Web servisini kullanan müşterinin MNG Kargo sisteminde kayıtlı müşteri numarası
            Dim pKullaniciAdi As String = oConnection.cMNGApiUserName

            ' Web servisini kullanan müşterinin MNG Kargo sisteminde kayıtlı müşteri numarasına bağlı şifresi
            Dim pSifre As String = oConnection.cMNGApiPassword
            Dim cKargocuTahsilatYapmaz As String = "H"
            Dim cOdemeTipi As String = ""

            ' servisten dönen sonuç
            ' 1 Kayıt Başarılı.
            ' 0 Kayıt Başarısız. (Hata Kodları )
            ' E002:Kayıt sırasında bir hata Oluştu! [2]
            ' E003:Kullanıcı adi veya sifresi yanlış tekrar kontrol ediniz. [3] 
            ' E004:Kullanıcı adı veya şifre boş olamaz. [4] 
            ' E005:BU SİPARİS NUMARASINA AİT KAYIT ZATEN VAR! [5] 
            ' E006:Kullanıcı adı rakamlardan olmuşmalıdır. Gönderilen değer:[6]
            ' E007:pChIl parametresi boş yada null olamaz. [7] 
            ' E008:pChIlce parametresi boş yada null olamaz. [8] 
            ' E010:pChAdres parametresi boş yada null olamaz. [10]
            ' E011:pAliciMusteriBayiNo parametresi maksimum 20 karakterden oluşabilir [11]
            ' E014:pChSiparisNo parametresi boş yada null değer alamaz. [14]
            ' E015:Kapıda Ödeme Seçeneği için pPrKiymet parametresi null veya 0 (sıfır) değer alamaz. Gönderilen değer:\"" + pPrKiymet +"\"" + " [15]
            ' E015:pPrKiymet parametresi decimal değerden başka değer alamaz. Gönderilen değer:\"" + pPrKiymet + "\"" + " [15]"
            ' E016:pFlAlSms parametresi \ "1\" yada \"0\" değerlerinden başka değer alamaz. Gönderilen değer: \"" + pFlAlSms.ToString() + "\"" + " [16]
            ' E017:pFlGnSms parametresi \"1\" yada \"0\" değerlerinden başka değer alamaz. Gönderilen değer:\"" + pFlGnSms.ToString() + "\"" + " [17]
            ' E018:pLuOdemeSekli parametresi \ "P\" yada \"U\" değerlerinden başka değer alamaz. Gönderilen değer: \"" + pLuOdemeSekli + "\"" + " [18]
            ' E019:pFlAdresFarkli parametresi \"1\" yada \"0\" değerlerinden başka değer alamaz. Gönderilen değer:\"" + pFlAdresFarkli + "\"" + " [19]
            ' E020:pKargoParcaList parametresinde hata tespit edildi. Parametrenin formatı "A1:B1:C1:D1:E1:;A2:B2:C2:D2:E2:;A3:B3:C3:D3:E3:;..." şeklinde olmalıdır. [20] 
            ' E021:pChTelCep parametresi hatalı [21]
            ' E021:pChTelEv parametresi hatalı [21]
            ' E021:pChTelIs parametresi hatalı [21]
            ' E022:pChIcerik parametresi maksimum 200 karakterden oluşabilir [22]
            ' E023:Toplam KgDesi parametresi maksimum 500 olabilir [23]
            ' E024:pChSiparisNo parametresi maksimum 30 karakterden oluşabilir [24]
            ' E024:pMalBedeliOdemeSekli parametresi hatalı ! Beklenen değer NAKIT yada KREDI_KARTI olmalı [24]
            ' E025:pGonderiHizmetSekli parametresi hatalı ! Beklenen değer NORMAL , ONCELIKLI, GUNICI yada AKSAM_TESLIMAT olmalı [25]
            ' E026:Alıcı Müşteri Adı Yeterli Uzunlukta Değil ! [26]"; E026:pPlatformKisaAdi parametresi hatalı ! Beklenen değer N11 yada GG olmalı [26]
            ' E027:pPlatformSatisKodu için pPlatformKisaAdi parametresini de belirtmelisiniz ! Beklenen değer N11 yada GG olmalı [27]
            ' E028:pPlatformKisaAdi belirttiğinizde pPlatformSatisKodu parametresini de belirtmelisiniz ![28]

            If cSiparisNo.Trim = "" Then Exit Function

            ' ortalama erkek ayakkabı kutusu
            nDesi = 3 ' Math.Round((33 * 20 * 11) / 3000, 0)
            nKG = 1

            oSQL.OpenConn()

            'oSQL.cSQLQuery = "select stokno, renk, beden, adet, fiyat, doviz " +
            '                " from sipperakendelines with (NOLOCK) " +
            '                " where siparisno = '" + cSiparisNo.Trim + "' "

            'oSQL.GetSQLReader()

            'Do While oSQL.oReader.Read

            '    For nCnt = 1 To oSQL.SQLReadDouble("adet")
            '        nAdet = nAdet + 1
            '        ' Dosya/Zarf gönderilerinde Kilo ve Desi bilgisi 0 (sıfır) kabul edilir
            '        ' Desi = Round ((En x Boy x Yükseklik) / 3000,0)
            '        ' Her parça bilgisi şu formatta olmalıdır. → Kilo : Desi : KgDesi:İçerik:ParçaNo:
            '        ' pKargoParcaList = Parça1_String ; Parça2_String ; Parça3_String ;
            '        ' pKargoParcaList = 1:1:1:Urun1:1: #:PARCABARKODTEXT1:; 1:1:1:Urun2:2:#:PARCABARKODTEXT2:;
            '        cGonderi = "1:" + nKG.ToString + ":" + nKG.ToString + ":" + oSQL.SQLReadString("stokno") + " " + oSQL.SQLReadString("renk") + ":" + nAdet.ToString + ":;"
            '        cGonderiler = cGonderi + cGonderiler
            '    Next

            '    nTutar = nTutar + (oSQL.SQLReadDouble("adet") * oSQL.SQLReadDouble("fiyat"))
            'Loop
            'oSQL.oReader.Close()

            'pKargoParcaList = cGonderiler
            pKargoParcaList = "1:1:1:AYAKKABI:1:;"

            oSQL.cSQLQuery = "select top 1 tarih, adi, soyadi, ili, " +
                            " ilcesi, telefon, adres, kargofirmasi, odemetipi, " +
                            " teslimsekli, finaltutar, " +
                            " kargocutahsilatyapmaz = (select top 1 kargocutahsilatyapmaz " +
                                                        " from odemetipi with (NOLOCK)  " +
                                                        " where kod = sipperakende.odemetipi) " +
                            " from sipperakende with (NOLOCK) " +
                            " where siparisno = '" + cSiparisNo.Trim + "' "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                cKargoFirmasi = oSQL.SQLReadString("kargofirmasi")
                pChIrsaliyeNo = cSiparisNo.Trim
                pChBarkod = cSiparisNo.Trim
                pChSiparisNo = cSiparisNo.Trim
                pAliciMusteriAdi = oSQL.SQLReadString("adi") + " " + oSQL.SQLReadString("soyadi")
                pChIl = oSQL.SQLReadString("ili").ToUpper
                pChIlce = oSQL.SQLReadString("ilcesi").ToUpper
                pChAdres = oSQL.SQLReadString("adres")
                pChTelEv = oSQL.SQLReadString("telefon")

                cKargocuTahsilatYapmaz = oSQL.SQLReadString("kargocutahsilatyapmaz")
                cOdemeTipi = oSQL.SQLReadString("odemetipi")

                If cKargocuTahsilatYapmaz = "E" Or cOdemeTipi = "PESIN - UCRET ALICI" Or cOdemeTipi = "PESIN - UCRET GONDERICI" Then
                    pPrKiymet = "0"
                    pFlKapidaOdeme = 0
                Else
                    pPrKiymet = Replace(CStr(oSQL.SQLReadDouble("finaltutar")), ".", ",")
                    pFlKapidaOdeme = 1
                End If

                Select Case cOdemeTipi
                    Case "PESIN - UCRET ALICI"
                        pLuOdemeSekli = "U"
                    Case Else
                        pLuOdemeSekli = "P"
                End Select

                Select Case cOdemeTipi
                    Case "KAPIDA KREDI KARTI"
                        pMalBedeliOdemeSekli = "KREDI_KARTI"
                    Case Else
                        pMalBedeliOdemeSekli = "NAKIT"
                End Select

                Select Case oSQL.SQLReadString("teslimsekli")
                    Case "KAPIDA TESLIM"
                        pTeslimSekli = 1
                    Case "KARGO ACENTESINDE TESLIM"
                        pTeslimSekli = 2
                End Select
            End If
            oSQL.oReader.Close()

            If cKargoFirmasi.Trim.ToLower <> "mng" Then
                oSQL.CloseConn()
                oSQL = Nothing
                oMNG = Nothing
                Exit Function
            End If

            cSonuc = oMNG.SiparisGirisiDetayliV3(pChIrsaliyeNo, pPrKiymet, pChBarkod, pChIcerik, pGonderiHizmetSekli,
                                                 pTeslimSekli, pFlAlSms, pFlGnSms, pKargoParcaList, pAliciMusteriMngNo,
                                                 pAliciMusteriBayiNo, pAliciMusteriAdi, pChSiparisNo, pLuOdemeSekli, pFlAdresFarkli,
                                                 pChIl, pChIlce, pChAdres, pChSemt, pChMahalle,
                                                 pChMeydanBulvar, pChCadde, pChSokak, pChTelEv, pChTelCep,
                                                 pChTelIs, pChFax, pChEmail, pChVergiDairesi, pChVergiNumarasi,
                                                 pFlKapidaOdeme, pMalBedeliOdemeSekli, pPlatformKisaAdi, pPlatformSatisKodu, pKullaniciAdi,
                                                 pSifre)
            If cSonuc.Trim = "1" Then
                ' başarılı
                oSQL.cSQLQuery = "update sipperakende " +
                                " set kargoyakayityollandi = 'E', " +
                                " kargoyakayityollanmatarihi = getdate() " +
                                " where siparisno = '" + cSiparisNo.Trim + "' "

                oSQL.SQLExecute()

                oSQL.cSQLQuery = "select top 1 aracikargo " +
                                " from firma with (NOLOCK) " +
                                " where firma = 'MNG' "

                cAraciKargo = oSQL.DBReadString()

                If cAraciKargo.Trim <> "" Then

                    oSQL.cSQLQuery = "update sipperakende " +
                                " set aracikargo = '" + cAraciKargo + "' " +
                                " where siparisno = '" + cSiparisNo.Trim + "' " +
                                " and (aracikargo is null or aracikargo = '') "

                    oSQL.SQLExecute()
                End If

                MNGSendOrder2 = True
            End If

            oSQL.CloseConn()
            oSQL = Nothing
            oMNG = Nothing

        Catch ex As Exception
            cErrorMessage = ex.Message.Trim
            ErrDisp("MNGSendOrder2",,,, ex)
        End Try
    End Function

    Public Function MNGQueryOrder2(cSiparisNo As String, Optional ByRef cSonuc As String = "", Optional ByRef cErrorMessage As String = "") As Boolean

        ' KARGO_STATU 
        ' 0 - Henüz işlem yapılmadı 
        ' 1 - Sipariş Kargoya Verildi
        ' 2 - Transfer Aşamasında 
        ' 3 - Gönderi Teslim Birimine Ulaştı 
        ' 4 - Gönderi Teslimat Adresine Yönlendirildi. 
        ' 5 - Teslim Edildi. (Alıcı Adı ve Soyadı – Tarihi) 
        ' 6 - [kod] Teslim Edilemedi (Nedeni ile) 
        ' 7 - Göndericiye Teslim Edildi / IADE

        cSonuc = ""
        cErrorMessage = ""
        MNGQueryOrder2 = False

        Try
            Dim cSiparisNo2 As String = ""
            Dim cSiparisNo3 As String = ""
            Dim cFilter As String = ""
            Dim nCnt As Integer = 0
            Dim aMNG() As oMNGData

            If CheckFirmaCalisilmasin("MNG") Then Exit Function
            'If Not GetServiceConnectionParameters(cSiparisNo, oConnection.cMNGApiUserName, oConnection.cMNGApiPassword,, "MNG",, cSiparisNo2) Then Exit Function

            Dim oSQL As New SQLServerClass

            ' oConnection.cMNGApiUrl = http://service.mngkargo.com.tr/tservis/musterikargosiparis.asmx

            Dim oBinding As System.ServiceModel.BasicHttpBinding = GetBinding()
            Dim oEndPointAddress = New System.ServiceModel.EndpointAddress(oConnection.cMNGApiUrl)
            Dim oMNG As New musterikargosiparis.MusteriKargoSiparisSoapClient(oBinding, oEndPointAddress)
            oMNG.InnerChannel.OperationTimeout = New TimeSpan(0, 12, 0, 0, 0)

            Dim oSonuc As DataSet
            Dim oTable As DataTable
            Dim oRow As DataRow
            Dim oColumn As DataColumn
            Dim cBuffer As String = ""
            Dim cKargoTakipNo As String = ""
            Dim cCikisSube As String = ""
            Dim cCikisSubeTelefon As String = ""
            Dim cTeslimSube As String = ""
            Dim cTeslimSubeTelefon As String = ""
            Dim cKargoStatu As String = ""
            Dim nKargoKgDesi As Double = 0
            Dim nKargoUrunBedeli As Double = 0
            Dim cKargoTakipUrl As String = ""
            Dim cKargoStatuTarihi As String = ""
            Dim dKargoStatuTarihi As Date = #1/1/1950#
            Dim cDurum As String = ""
            Dim cGonderiCikisTarihi As String = ""
            Dim dGonderiCikisTarihi As Date = #1/1/1950#
            Dim cKargoMesaj As String = ""

            ReDim aMNG(0)
            nCnt = -1

            If InStr(cSiparisNo, ";") = 0 Then
                cFilter = " and (a.siparisno = '" + cSiparisNo.Trim + "' or a.siparisno2 = '" + cSiparisNo.Trim + "') "
            Else
                cFilter = Replace(cSiparisNo, ";", "','")
                cFilter = " and (a.siparisno in ('" + cFilter.Trim + "') or a.siparisno2 in ('" + cFilter.Trim + "')) "
            End If

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select distinct b.aracikargo, b.username, b.password  " +
                            " from sipperakende a with (NOLOCK) , firmakargoaraci b with (NOLOCK) " +
                            " where a.kargofirmasi = b.kargofirmasi " +
                            " and a.aracikargo = b.aracikargo " +
                            cFilter

            oSQL.GetSQLReader()

            Do While oSQL.oReader.Read
                nCnt = nCnt + 1
                ReDim Preserve aMNG(nCnt)
                aMNG(nCnt).cAraciKargo = oSQL.SQLReadString("aracikargo")

                ' Web servisini kullanan müşterinin MNG Kargo sisteminde kayıtlı müşteri numarası
                If oSQL.SQLReadString("username") = "" Then
                    aMNG(nCnt).cUserName = oConnection.cMNGApiUserName
                Else
                    aMNG(nCnt).cUserName = oSQL.SQLReadString("username")
                End If

                ' Web servisini kullanan müşterinin MNG Kargo sisteminde kayıtlı müşteri numarasına bağlı şifresi
                If oSQL.SQLReadString("password") = "" Then
                    aMNG(nCnt).cPassword = oConnection.cMNGApiPassword
                Else
                    aMNG(nCnt).cPassword = oSQL.SQLReadString("password")
                End If

                aMNG(nCnt).cSiparisler = ""
            Loop
            oSQL.oReader.Close()

            For nCnt = 0 To UBound(aMNG)

                cSiparisNo2 = ""

                oSQL.cSQLQuery = "select a.siparisno, a.siparisno2 " +
                                " from sipperakende a with (NOLOCK) , firmakargoaraci b with (NOLOCK) " +
                                " where a.kargofirmasi = b.kargofirmasi " +
                                " and a.aracikargo = b.aracikargo " +
                                " and b.aracikargo = '" + aMNG(nCnt).cAraciKargo + "' " +
                                " and b.username = '" + aMNG(nCnt).cUserName + "' " +
                                " and b.password = '" + aMNG(nCnt).cPassword + "' " +
                                cFilter +
                                " order by a.siparisno "

                oSQL.GetSQLReader()

                Do While oSQL.oReader.Read
                    If oSQL.SQLReadString("siparisno2") = "" Then
                        If cSiparisNo2.Trim = "" Then
                            cSiparisNo2 = oSQL.SQLReadString("siparisno")
                        Else
                            cSiparisNo2 = cSiparisNo2 + ";" + oSQL.SQLReadString("siparisno")
                        End If
                    Else
                        If cSiparisNo2.Trim = "" Then
                            cSiparisNo2 = oSQL.SQLReadString("siparisno2")
                        Else
                            cSiparisNo2 = cSiparisNo2 + ";" + oSQL.SQLReadString("siparisno2")
                        End If
                    End If
                Loop
                oSQL.oReader.Close()

                aMNG(nCnt).cSiparisler = cSiparisNo2
            Next

            For nCnt = 0 To UBound(aMNG)

                'oSonuc = Nothing

                Try
                    oSonuc = oMNG.KargoBilgileriByReferans(aMNG(nCnt).cUserName, aMNG(nCnt).cPassword, aMNG(nCnt).cSiparisler, "", "", "", "", "", "", cErrorMessage)
                Catch ex As Exception
                    oSonuc = Nothing
                    If cErrorMessage.Trim = "" Then
                        cErrorMessage = ex.Message
                    Else
                        cErrorMessage = cErrorMessage + ex.Message
                    End If
                End Try

                If Not (oSonuc Is Nothing) Then
                    For Each oTable In oSonuc.Tables
                        If Not IsNothing(oTable) Then
                            For Each oRow In oTable.Rows
                                If Not IsNothing(oRow) Then
                                    cSonuc = ""
                                    dKargoStatuTarihi = #1/1/1950#
                                    cKargoStatuTarihi = ""
                                    dGonderiCikisTarihi = #1/1/1950#
                                    cGonderiCikisTarihi = ""
                                    nKargoUrunBedeli = 0
                                    nKargoKgDesi = 0
                                    cKargoTakipUrl = ""
                                    cKargoStatu = ""
                                    cDurum = ""
                                    cKargoTakipNo = ""
                                    cCikisSube = ""
                                    cTeslimSube = ""
                                    cSiparisNo3 = ""

                                    For Each oColumn In oTable.Columns
                                        cBuffer = oColumn.ColumnName.ToString.Trim + "::" + oRow(oColumn).ToString.Trim
                                        If cSonuc.Trim = "" Then
                                            cSonuc = cBuffer
                                        Else
                                            cSonuc = cSonuc + ";;" + cBuffer
                                        End If
                                        If oColumn.ColumnName.ToString.Trim = "SIPARIS_NO" Then
                                            cSiparisNo3 = oRow(oColumn).ToString.Trim
                                        End If
                                        If oColumn.ColumnName.ToString.Trim = "KARGO_STATU_TARIHI" Then
                                            dKargoStatuTarihi = WebReadDate(oRow(oColumn), 1)
                                            cKargoStatuTarihi = oRow(oColumn).ToString.Trim
                                        End If
                                        If oColumn.ColumnName.ToString.Trim = "GONDERI_CIKIS_TARIHI" Then
                                            dGonderiCikisTarihi = WebReadDate(oRow(oColumn), 1)
                                            cGonderiCikisTarihi = oRow(oColumn).ToString.Trim
                                        End If
                                        If oColumn.ColumnName.ToString.Trim = "KARGO_URUN_BEDELI" Then
                                            nKargoUrunBedeli = WebReadDouble(oRow(oColumn))
                                        End If
                                        If oColumn.ColumnName.ToString.Trim = "KARGO_KGDESI" Then
                                            nKargoKgDesi = WebReadDouble(oRow(oColumn))
                                        End If
                                        If oColumn.ColumnName.ToString.Trim = "KARGO_TAKIP_URL" Then
                                            cKargoTakipUrl = oRow(oColumn).ToString.Trim
                                        End If
                                        If oColumn.ColumnName.ToString.Trim = "KARGO_STATU" Then
                                            cKargoStatu = oRow(oColumn).ToString.Trim
                                        End If
                                        If oColumn.ColumnName.ToString.Trim = "KARGO_STATU_ACIKLAMA" Then
                                            cDurum = oRow(oColumn).ToString.Trim
                                            cKargoMesaj = oRow(oColumn).ToString.Trim
                                        End If
                                        If oColumn.ColumnName.ToString.Trim = "MNG_GONDERI_NO" Then
                                            cKargoTakipNo = oRow(oColumn).ToString.Trim
                                        End If
                                        If oColumn.ColumnName.ToString.Trim = "CIKIS_SUBESI" Then
                                            cCikisSube = oRow(oColumn).ToString.Trim
                                        End If
                                        If oColumn.ColumnName.ToString.Trim = "TESLIM_SUBESI" Then
                                            cTeslimSube = oRow(oColumn).ToString.Trim
                                        End If
                                    Next

                                    If cDurum.Trim <> "" And cSiparisNo3.Trim <> "" Then

                                        Select Case cKargoStatu.Trim
                                            Case "0" : cDurum = "SIPARIS HAZIRLANIYOR" ' GONDERI KARGO ISLEMI YAPILMADI
                                            Case "1" : cDurum = "CIKIS SUBEDE"
                                            Case "2" : cDurum = "YOLDA"
                                            Case "3" : cDurum = "VARIS SUBEDE"
                                            Case "4" : cDurum = "DAGITIMDA"
                                            Case "5" : cDurum = "TESLIM EDILDI"
                                            Case "6" : cDurum = "SORUNLU"
                                            Case "7" : cDurum = "IADE"
                                            Case Else : cDurum = "SIPARIS HAZIRLANIYOR"
                                        End Select

                                        oSQL.cSQLQuery = "update sipperakende " +
                                                    " set kargotakipno = '" + SQLWriteString(cKargoTakipNo, 30) + "', " +
                                                    " kargotakipurl = '" + SQLWriteString(cKargoTakipUrl, 150) + "', " +
                                                    " kargosondurumkodu = '" + SQLWriteString(cKargoStatu, 3) + "', " +
                                                    " kargosondurumtarihi = getdate(), " +
                                                    " kargokgdesi = " + SQLWriteDecimal(nKargoKgDesi) + ", " +
                                                    " kargotahsilattutari = " + SQLWriteDecimal(nKargoUrunBedeli) + ", " +
                                                    " kargostatutarihi = '" + SQLWriteDateTime(dKargoStatuTarihi) + "', " +
                                                    " kargostatutarihiorjinal = '" + SQLWriteString(cKargoStatuTarihi, 30) + "', " +
                                                    " gondericikistarihi = '" + SQLWriteDateTime(dGonderiCikisTarihi) + "', " +
                                                    " gondericikistarihiorjinal = '" + SQLWriteString(cGonderiCikisTarihi, 30) + "', " +
                                                    " kargomesaj = '" + SQLWriteString(cKargoMesaj, 100) + "', " +
                                                    " kargostatu = '" + SQLWriteString(cDurum, 100) + "', " +
                                                    " cikis_sube = '" + SQLWriteString(cCikisSube, 30) + "', " +
                                                    " teslim_sube = '" + SQLWriteString(cTeslimSube, 30) + "' " +
                                                    " where (siparisno = '" + cSiparisNo3.Trim + "' or siparisno2 = '" + cSiparisNo3.Trim + "') "
                                        oSQL.SQLExecute()

                                        Select Case cKargoStatu.Trim
                                            Case "5"
                                                ' teslim edildi
                                                oSQL.cSQLQuery = "update sipperakende " +
                                                    " set kapandi = 'E', " +
                                                    " kapanmatarihi = '" + SQLWriteDateTime(dKargoStatuTarihi) + "', " +
                                                    " iade = null, " +
                                                    " iadetarihi = null " +
                                                    " where (siparisno = '" + cSiparisNo3.Trim + "' or siparisno2 = '" + cSiparisNo3.Trim + "') "
                                                oSQL.SQLExecute()
                                            Case "7"
                                                ' iade
                                                oSQL.cSQLQuery = "update sipperakende " +
                                                    " set iade = 'E', " +
                                                    " iadetarihi = '" + SQLWriteDateTime(dKargoStatuTarihi) + "', " +
                                                    " kapandi = null, " +
                                                    " kapanmatarihi = null " +
                                                    " where (siparisno = '" + cSiparisNo3.Trim + "' or siparisno2 = '" + cSiparisNo3.Trim + "') "
                                                oSQL.SQLExecute()
                                        End Select
                                    End If
                                End If
                            Next
                        End If
                    Next
                End If
            Next

            oSQL.CloseConn()

            oSQL = Nothing
            oMNG = Nothing
            MNGQueryOrder2 = True

        Catch ex As Exception
            If InStr(ex.Message, "GUNLUK SORGULAMA SINIRI ASILDI") = 0 Then
                cErrorMessage = ex.Message.Trim
                ErrDisp("MNGQueryOrder2",,,, ex)
            End If
        End Try
    End Function

    Public Function MNGCancelOrder2(ByVal cSiparisNo As String, Optional ByRef cErrorMessage As String = "") As Boolean

        Dim nSonuc As Integer = 0
        Dim cKargoTakipNo As String = ""

        cErrorMessage = ""
        MNGCancelOrder2 = False

        Try
            If CheckFirmaCalisilmasin("MNG") Then Exit Function
            If Not GetServiceConnectionParameters(cSiparisNo, oConnection.cMNGApiUserName, oConnection.cMNGApiPassword,, "MNG") Then Exit Function

            Dim oSQL As New SQLServerClass

            Dim oBinding As System.ServiceModel.BasicHttpBinding = GetBinding()
            Dim oEndPointAddress = New System.ServiceModel.EndpointAddress(oConnection.cMNGApiUrl)
            Dim oMNG As New musterikargosiparis.MusteriKargoSiparisSoapClient(oBinding, oEndPointAddress)
            oMNG.InnerChannel.OperationTimeout = New TimeSpan(0, 12, 0, 0, 0)

            Dim dTarih As Date = Now
            ' Web servisini kullanan müşterinin MNG Kargo sisteminde kayıtlı müşteri numarası
            Dim pKullaniciAdi As String = oConnection.cMNGApiUserName
            ' Web servisini kullanan müşterinin MNG Kargo sisteminde kayıtlı müşteri numarasına bağlı şifresi
            Dim pSifre As String = oConnection.cMNGApiPassword
            Dim cSonuc As String = ""
            Dim cErrorMessage1 As String = ""
            Dim cErrorMessage2 As String = ""
            Dim oProvider As CultureInfo = CultureInfo.InvariantCulture
            Dim cSiparisNo2 As String = ""

            cSiparisNo2 = GetSiparisNo2(cSiparisNo)

            If cSiparisNo2.Trim = "" Then
                cSiparisNo2 = cSiparisNo.Trim
            End If

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select top 1 kargotakipno, tarih " +
                            " from sipperakende with (NOLOCK)  " +
                            " where (siparisno = '" + cSiparisNo.Trim + "' or siparisno2 = '" + cSiparisNo.Trim + "') "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                cKargoTakipNo = oSQL.SQLReadString("kargotakipno")
                dTarih = oSQL.SQLReadDate("tarih")
            End If
            oSQL.oReader.Close()

            nSonuc = oMNG.TekBarkodGonderiIptali(pKullaniciAdi, pSifre, cSiparisNo2, cKargoTakipNo, cErrorMessage1)

            If nSonuc = 1 Then

                oSQL.cSQLQuery = "update sipperakende " +
                                " set iptal = 'E', " +
                                " iptaltarih = getdate() " +
                                " where (siparisno = '" + cSiparisNo.Trim + "' or siparisno2 = '" + cSiparisNo.Trim + "') "
                oSQL.SQLExecute()

                MNGCancelOrder2 = True
            Else
                cSonuc = oMNG.MusteriSiparisIptal(pKullaniciAdi, pSifre, cSiparisNo2, dTarih.ToString("dd.mm.yyyy", oProvider), cErrorMessage2)

                If cSonuc = "1" Or cErrorMessage2.Trim = "" Then

                    oSQL.cSQLQuery = "update sipperakende " +
                                " set iptal = 'E', " +
                                " iptaltarih = getdate() " +
                                " where (siparisno = '" + cSiparisNo.Trim + "' or siparisno2 = '" + cSiparisNo.Trim + "') "
                    oSQL.SQLExecute()

                    MNGCancelOrder2 = True
                End If
            End If

            oSQL.CloseConn()
            oSQL = Nothing

            If cErrorMessage2.Trim = "" Then
                cErrorMessage = cErrorMessage2
            Else
                cErrorMessage = cErrorMessage1
            End If

        Catch ex As Exception
            cErrorMessage = ex.Message.Trim
            ErrDisp("MNGCancelOrder2",,,, ex)
        End Try
    End Function

    Private Function GetBinding() As System.ServiceModel.BasicHttpBinding

        GetBinding = Nothing

        Try
            Dim oBinding = New System.ServiceModel.BasicHttpBinding()

            oBinding.MaxReceivedMessageSize = 2147483647
            oBinding.MaxBufferPoolSize = 2147483647
            oBinding.MaxBufferSize = 2147483647
            oBinding.ReaderQuotas.MaxDepth = 2147483647
            oBinding.ReaderQuotas.MaxStringContentLength = 2147483647
            oBinding.ReaderQuotas.MaxArrayLength = 2147483647
            oBinding.ReaderQuotas.MaxBytesPerRead = 2147483647
            oBinding.ReaderQuotas.MaxNameTableCharCount = 2147483647
            oBinding.SendTimeout = New TimeSpan(0, 12, 0, 0, 0)
            oBinding.ReceiveTimeout = New TimeSpan(0, 12, 0, 0, 0)
            oBinding.OpenTimeout = New TimeSpan(0, 12, 0, 0, 0)
            oBinding.CloseTimeout = New TimeSpan(0, 12, 0, 0, 0)

            GetBinding = oBinding

        Catch ex As Exception
            ErrDisp("GetBinding",,,, ex)
        End Try
    End Function

End Module
