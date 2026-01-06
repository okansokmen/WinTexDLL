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
    Public value As String         ' İşleminiz başarılı ise işlem sonucu olan KARGO TAKİP NUMARASI bu alanda dönülecektir
End Class
