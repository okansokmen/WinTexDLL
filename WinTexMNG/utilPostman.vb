Option Explicit On

Imports RestSharp
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Http
Imports System.Text
Imports System.Threading.Tasks
Imports System.Net
Imports System.IO
' utilByexpress e bak
' kargo zamanı ve interline express aynı

' https://schema.postman.com

' http://webpostman.kargozamani.com:9999/user/login
' panel user  : benimpabucum@gmail.com
' panel şifre : Bp123654

' http://webpostman.kargozamani.com:9999/restapi/client
' API authorization : 7vCPjKLqSNdQDVx8cpHGUEbRJkgW1aBy9rX0M2nh

' Kargo Firması     : KARGOZAMANI

' Panel İçin
' Panel Url         : https://webpostman.kargozamani.com/
' Panel User        : ayakkabilarim@kargozamani.com
' Panel Şifre       : Ak20681*

' Kargozamani Api & entegrasyon İçin
' Api Url           : https://webpostman.kargozamani.com/restapi/client
' Api From          : ayakkabilarim@kargozamani.com
' Api Authorization : pJNOPa7t3nsUCrE4WFvGx8MjBTS06yR29dzfHXQq

' Interlineexpress

' Api & entegrasyon İçin
' API KEY          : 4NgyXODmG1KAqaELhnk3fcT5FB6JRHWC8PbItSQz
' API MAIL         : mares@interlinekargo.com
' API ENDPOINT URL : http://webpostman.interlineexpress.com:9999/
' API URL          : http://webpostman.interlineexpress.com:9999/restapi/client
' ŞUBE KODU        : IDM
' ŞUBE ADI         : İNTERLINE DAĞITIM

' Portal / Panel
' Panel URL        : http://webpostman.interlineexpress.com:9999/user/login
' Panel Username   : mares@interlinekargo.com
' Panel Password   : Mares.3434

' consignment_type_id
' http://webpostman.interlineexpress.com:9999/restapi/client/consignment_types/102
' 1 => Koli,
' 2 => Paket,
' 3 => Dosya,
' 4 => A4 Zarf,
' 5 => A5 Zarf,
' 6 => Diplomat,
' 7 => Dergi,
' 8 => Diğer,

' amount_type_id
' http://webpostman.interlineexpress.com:9999/restapi/client/consignment_types/103
' 1 => Toplu Gönderi (Toplu Zarf dergi kurye (kargo değil) adresli veya adressiz dağıtım. Taşıma bedeli gönderene fatura edilir. (amount) Kapıda Tahsilat bedeli girilmemelidir. )
' 2 => Ücret Alıcı (Taşıma bedeli alıcıdan alınır. Tahsilatlı kargo olamaz. Eğer Gönderi İade olmadıysa Taşıma bedeli gönderene fatura edilmez. İade Durumunda ise Taşıma bedeli gönderene fatura edilir.).)
' 3 => Peşin Ödeme (Tahsilat bedeli (amount) var ise alıcıdan tahsil edilir. Yani Kapıda Nakit Tahsilatlı kargo olur. Eğer Tahsilat bedeli (amount) yok ise tahsilatsız kargo olur. Her iki Durumdada alıcıdan taşıma bedeli alınmaz. Taşıma bedeli gönderene fatura edilir. )
' 5 => Kredi Kartı ( Gönderen Müşteri Sanal Ödeme ile hesabına aktarım. Kargo Dağıtım Zincirinde olan durumlarda geçerlidir. Özel talep ile aktif edilir. Taşıma bedeli gönderene fatura edilir.)
' 6 => Kapıda Kredi Kartı (Kapıda Kredi kartı ile tahsilat tutar kargo demektir. Tahsilat tutarı (amount) zorunludur. Alıcıdan ayrıca taşıma bedeli alınmaz. Taşıma bedeli gönderene fatura edilir.)

' add_service_type_id
' http://webpostman.interlineexpress.com:9999/restapi/client/consignment_types/105
' 1 => Kontrollü Teslim,
' 2 => Alma Haberli,
' 3 => Değer Konulmuş,
' 4 => GİTTİ GİDİYOR,
' 5 => N11,
' 6 => TRENDYOL,
' 7 => HEPSİ BURADA,
' 8 => ÇİCEK SEPETİ,
' 9 => T-SOFT E-TİCARET,

' distribution_type_id
' http://webpostman.interlineexpress.com:9999/restapi/client/consignment_types/106
' 1 => Ertesi Gün Teslimat,
' 2 => Aynı Gün Teslimat,
' 3 => Geri Dönüşümlü Kargo,
' 4 => Uçak Kargo,
' 5 => Motokurye,
' 6 => Toplama,
' 7 => Şube Sevk Kolileri,

Module utilPostman

    Private Enum IO
        SendData
        RecieveData
    End Enum

    Public Function PostmanGonderiYukle(Optional cSiparisNo As String = "", Optional ByRef cSonuc As String = "", Optional ByRef cErrorMessage As String = "",
                                        Optional ByVal nMode As Integer = 1) As Boolean

        ' nMode = 1 kargozamani / FAVORI 2024
        ' nMode = 2 kargozamani / MARES BESIKTAS
        ' nMode = 3 interlineexpress / INTERLINE DAGITIM

        PostmanGonderiYukle = False

        Try
            Dim sipno As String = ""
            Dim oSQL As New SQLServerClass
            Dim oSQL2 As New SQLServerClass
            Dim basarili As Integer = 0
            Dim basarisiz As Integer = 0
            Dim cKargoTakipNo As String = ""
            Dim lSonuc As Boolean = False
            Dim cPostData As String = ""
            Dim cError As String = ""
            Dim cAciklama As String = ""
            Dim nCnt As Integer = 0
            Dim cBranchCode As String = ""
            Dim cStartBranch As String = ""

            ' Method POST
            SPMAyar()

            If nMode = 1 Or nMode = 2 Then
                cBranchCode = "113"
                cStartBranch = "Sair nedim"
            Else
                cBranchCode = "IDM"
                cStartBranch = "İNTERLINE DAĞITIM"
                ' 580 => ARAS MİRAÇ,
                ' HEP => HEPSİJET,
                ' IDM => İNTERLINE DAĞITIM,
                ' 880 => PTT M,
                ' SRM => SÜRAT İNTERLİNE,
                ' SRY => SÜRAT KARGO,
                ' 600 => YURTİÇİ T,
            End If

            Debug.WriteLine("Gönderiler yüklenmeye başladı")
            CreateLog("PostManGonder", "Sipariş gönderiliyor " + cSiparisNo)

            oSQL.OpenConn()
            oSQL2.OpenConn()

            oSQL.cSQLQuery = "select *, CONVERT(DECIMAL(18,2), finaltutar) AS GGF " +
                            " from sipperakende with (NOLOCK) " +
                            " where kapandi is null " +
                            " and iptal is null " +
                            " and kargostatu = 'SIPARIS HAZIRLANIYOR' "

            If nMode = 1 Or nMode = 2 Then
                oSQL.cSQLQuery = oSQL.cSQLQuery +
                    " and aracikargo in ('FAVORI 2024','MARES BESIKTAS') "
            Else
                oSQL.cSQLQuery = oSQL.cSQLQuery +
                    " and aracikargo = 'INTERLINE DAGITIM' "
            End If

            If cSiparisNo.Trim <> "" Then
                oSQL.cSQLQuery = oSQL.cSQLQuery +
                            " and siparisno = '" + cSiparisNo.Trim + "' "
            End If

            oSQL.cSQLQuery = oSQL.cSQLQuery +
                            " order by tarih "

            oSQL.GetSQLReader()

            Do While oSQL.oReader.Read()

                Debug.WriteLine("Sipariş yükleniyor : " + oSQL.oReader("siparisno"))

                Dim item = New With {
                    Key .GGF = oSQL.oReader("GGF"),
                    Key .siparisno = oSQL.oReader("siparisno"),
                    Key .odemetipi = oSQL.oReader("odemetipi"),
                    Key .adi = oSQL.oReader("adi"),
                    Key .soyadi = oSQL.oReader("soyadi"),
                    Key .ili = oSQL.oReader("ili"),
                    Key .ilcesi = oSQL.oReader("ilcesi"),
                    Key .adres = oSQL.oReader("adres"),
                    Key .telefon = oSQL.oReader("telefon"),
                    Key .satici = oSQL.oReader("firma")
                }

                Dim tutar = item.GGF.ToString().Trim()
                sipno = item.siparisno.ToString().Trim()
                Dim odemetipi = 0

                If item.odemetipi.ToString().Trim() = "PESIN - UCRET GONDERICI" OrElse item.odemetipi.ToString().Trim() = "SIRKETE PESIN-UCRET GONDERICI" Then
                    odemetipi = 3
                    tutar = "0"
                ElseIf item.odemetipi.ToString().Trim() = "SIRKETE PESIN-UCRET ALICI" Then
                    odemetipi = 2
                    tutar = "0"
                ElseIf item.odemetipi.ToString().Trim() = "KAPIDA NAKIT" Then
                    odemetipi = 3
                    tutar = item.GGF.ToString().Trim()
                ElseIf item.odemetipi.ToString().Trim() = "KAPIDA KREDI KARTI" Then
                    odemetipi = 6
                    tutar = item.GGF.ToString().Trim()
                End If

                cPostData = WebUtility.UrlEncode("customer") + "=" + WebUtility.UrlEncode(item.adi.ToString().Trim() + " " + item.soyadi.ToString().Trim()) + "&" +
                            WebUtility.UrlEncode("province_name") + "=" + WebUtility.UrlEncode(item.ili.ToString().Trim()) + "&" +
                            WebUtility.UrlEncode("county_name") + "=" + WebUtility.UrlEncode(item.ilcesi.ToString().Trim()) + "&" +
                            WebUtility.UrlEncode("address") + "=" + WebUtility.UrlEncode(item.adres.ToString().Trim() + " " + item.ilcesi.ToString().Trim() + " " + item.ili.ToString().Trim()) + "&" +
                            WebUtility.UrlEncode("telephone") + "=" + WebUtility.UrlEncode(item.telefon.ToString().Trim()) + "&" +
                            WebUtility.UrlEncode("branch_code") + "=" + WebUtility.UrlEncode(cBranchCode) + "&" +
                            WebUtility.UrlEncode("barcode") + "=" + WebUtility.UrlEncode(item.siparisno.ToString().Trim()) + "&" +
                            WebUtility.UrlEncode("amount") + "=" + WebUtility.UrlEncode(tutar) + "&" +
                            WebUtility.UrlEncode("summary") + "=" + WebUtility.UrlEncode("AYAKKABI") + "&" +
                            WebUtility.UrlEncode("start_branch") + "=" + WebUtility.UrlEncode(cStartBranch) + "&" +
                            WebUtility.UrlEncode("quantity") + "=" + WebUtility.UrlEncode("1") + "&" +
                            WebUtility.UrlEncode("consignment_type_id") + "=" + WebUtility.UrlEncode("3") + "&" +
                            WebUtility.UrlEncode("amount_type_id") + "=" + WebUtility.UrlEncode(odemetipi.ToString()) + "&" +
                            WebUtility.UrlEncode("order_number") + "=" + WebUtility.UrlEncode(item.siparisno.ToString().Trim()) + "&" +
                            WebUtility.UrlEncode("seller") + "=" + WebUtility.UrlEncode(item.satici.ToString().Trim())

                cSonuc = CallAPI(lSonuc, cErrorMessage, cPostData, nMode, IO.SendData)

                If lSonuc Then
                    Dim trendssList = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(cSonuc)

                    cError = trendssList("error").ToString().Trim()

                    If cError = "true" Then
                        PostmanGonderiYukle = False
                        cErrorMessage = trendssList("result").ToString().Trim()
                        cErrorMessage = StrStripLettersNumbers(cErrorMessage)

                        CreateLog("PostManGonder", "Sipariş gönderilemedi " + nCnt.ToString + " " + cSiparisNo + " " + cErrorMessage)
                    Else
                        PostmanGonderiYukle = True

                        oSQL2.cSQLQuery = "update sipperakende " +
                                         " set kargoyakayityollandi = 'E', " +
                                         " kargoyakayityollanmatarihi = getdate(), " +
                                         " postmanwrite = getdate() " +
                                         " where siparisno = '" + sipno + "' "

                        oSQL2.SQLExecute()

                        CreateLog("PostManGonder", "Sipariş gönderildi " + nCnt.ToString + " " + cSiparisNo)
                    End If
                End If

                basarili += 1
            Loop
            oSQL.oReader.Close()

            oSQL.CloseConn()
            oSQL2.CloseConn()

            Debug.WriteLine("Gönderiler yüklendi")
            Debug.WriteLine(basarili)

        Catch ex As Exception
            ErrDisp("PostmanGonderiYukle", "utilpostman",,, ex)
        End Try
    End Function

    Public Function PostmanSonDurumuAl(Optional cSiparisNo As String = "", Optional ByRef cSonuc As String = "", Optional ByRef cErrorMessage As String = "",
                                        Optional ByVal nMode As Integer = 1) As Boolean

        ' Postman Data PUT
        ' nMode = 1 kargozamani / FAVORI 2024
        ' nMode = 2 kargozamani / MARES BESIKTAS
        ' nMode = 3 interlineexpress / INTERLINE DAGITIM

        PostmanSonDurumuAl = False

        Try
            Dim sipno As String = ""
            Dim oSQL As New SQLServerClass
            Dim sifaf = New List(Of Dictionary(Of String, Object))()
            Dim cDesi As String = ""
            Dim cKargoTakipUrl As String = ""
            Dim cMusteriBarkod As String = ""
            Dim cAraciKargo As String = ""
            Dim cKargoTakipNo As String = ""
            Dim lSonuc As Boolean = False
            Dim cData As String = ""
            Dim cPostData As String = ""
            Dim cKargoStatu As String = ""
            Dim cDurum As String = ""
            Dim cKargoMesaj As String = ""
            Dim nCnt As Integer = 0
            Dim nBegin As Integer = 1
            Dim nEnd As Integer = 3

            SPMAyar()

            Debug.WriteLine("Sipariş sorgulama başladı")
            CreateLog("PostManSorgu", "Sipariş sorgulanıyor mode / siparis : " + nMode.ToString + " / " + cSiparisNo)

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select * , CONVERT(DECIMAL(18,2),finaltutar) AS GGF " +
                            " from sipperakende with (NOLOCK) " +
                            " where printbatch >= '0000013000' " +
                            " and aracikargo in ('FAVORI 2024','MARES BESIKTAS','INTERLINE DAGITIM') "

            If cSiparisNo.Trim <> "" Then
                oSQL.cSQLQuery = oSQL.cSQLQuery +
                            " and siparisno = '" + cSiparisNo.Trim + "' "
            End If

            oSQL.cSQLQuery = oSQL.cSQLQuery +
                            " order by tarih "

            oSQL.GetSQLReader()

            Do While oSQL.oReader.Read()

                cAraciKargo = oSQL.SQLReadString("aracikargo")

                Dim row = New Dictionary(Of String, Object)()
                For i As Integer = 0 To oSQL.oReader.FieldCount - 1
                    row(oSQL.oReader.GetName(i)) = oSQL.oReader.GetValue(i)
                Next i
                sifaf.Add(row)
            Loop
            oSQL.oReader.Close()

            If cSiparisNo.Trim <> "" And cAraciKargo = "INTERLINE DAGITIM" Then
                nBegin = 3
                nEnd = 3
            End If

            For Each item In sifaf
                sipno = item("siparisno").ToString().Trim()
                Debug.WriteLine("Sipariş durumu sorgulanıyor" + cSiparisNo)

                cPostData = WebUtility.UrlEncode("durums") + "=" + WebUtility.UrlEncode("-1") + "&" +
                            WebUtility.UrlEncode("sipno") + "=" + WebUtility.UrlEncode(sipno)

                For nCnt = nBegin To nEnd

                    cSonuc = CallAPI(lSonuc, cErrorMessage, cPostData, nCnt, IO.RecieveData)

                    If lSonuc Then

                        cDurum = ""

                        Dim trendssList = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(cSonuc)

                        If trendssList("toplam_kayit").ToString() = "1" Then
                            cData = trendssList("data").ToString()
                            cData = Replace(cData, "[", "")
                            cData = Replace(cData, "]", "")

                            Dim trendss = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(cData)
                            cKargoStatu = trendss("statu_no").ToString()
                            cKargoMesaj = trendss("sonuc").ToString()

                            Select Case cKargoStatu
                                Case "00" : cDurum = "SIPARIS HAZIRLANIYOR" ' GONDERI KARGO ISLEMI YAPILMADI
                                Case "01" : cDurum = "CIKIS SUBEDE"
                                Case "10" : cDurum = "TESLIM EDILDI"
                                Case "20", "21", "22", "23", "24" : cDurum = "IADE"
                                Case "30", "50", "60" : cDurum = "SORUNLU"
                                Case "40" : cDurum = "YOLDA"
                                Case "41" : cDurum = "VARIS SUBEDE"
                                Case "42" : cDurum = "DAGITIMDA"
                                Case Else : cDurum = "SIPARIS HAZIRLANIYOR"
                            End Select

                            cDesi = trendss("desi").ToString().Substring(0, trendss("desi").ToString().Length - 3)
                            cKargoTakipNo = trendss("cikisno")
                            cKargoTakipUrl = "http://kargotakip.araskargo.com.tr/mainpage.aspx?code=" + trendss("cikisno")
                            cMusteriBarkod = trendss("musteribarkod")

                            oSQL.cSQLQuery = "update sipperakende " +
                                        " set kargokgdesi = " + cDesi + " , " +
                                        " kargofirmasi = 'ARAS', " +
                                        " cikis_sube = 'AYAZAĞA', " +
                                        " kargostatu = '" + cDurum + "', " +
                                        " kargostatutarihi = getdate(), " +
                                        " kargotakipno = '" + cKargoTakipNo + "', " +
                                        " kargotakipurl = '" + cKargoTakipUrl + "', " +
                                        " kargosondurumkodu = '" + SQLWriteString(cKargoStatu, 3) + "', " +
                                        " kargosondurumtarihi = getdate(), " +
                                        " kargomesaj = '" + SQLWriteString(cKargoMesaj, 100) + "', " +
                                        " postmanread = getdate() " +
                                        " where (siparisno = '" + cMusteriBarkod.Trim + "' or siparisno2 = '" + cMusteriBarkod.Trim + "') "

                            oSQL.SQLExecute()

                            Select Case cKargoStatu.Trim
                                Case "10"
                                    ' teslim edildi
                                    oSQL.cSQLQuery = "update sipperakende " +
                                                " set kapandi = 'E', " +
                                                " kapanmatarihi = getdate(), " +
                                                " iade = null, " +
                                                " iadetarihi = null " +
                                                " where (siparisno = '" + cMusteriBarkod.Trim + "' or siparisno2 = '" + cMusteriBarkod.Trim + "') "

                                    oSQL.SQLExecute()
                                Case "20", "21", "22", "23", "24"
                                    ' iade
                                    oSQL.cSQLQuery = "update sipperakende " +
                                                " set iade = 'E', " +
                                                " iadetarihi = getdate(), " +
                                                " kapandi = null, " +
                                                " kapanmatarihi = null " +
                                                " where (siparisno = '" + cMusteriBarkod.Trim + "' or siparisno2 = '" + cMusteriBarkod.Trim + "') "

                                    oSQL.SQLExecute()
                            End Select

                            CreateLog("PostManSorgu", "Sipariş durumu alındı " + nCnt.ToString + " " + cSiparisNo + " " + cDurum)
                        Else
                            CreateLog("PostManSorgu", "Sipariş sorgulanamadı " + nCnt.ToString + " " + cSiparisNo + " " + cDurum)
                        End If
                    End If
                Next
            Next

            oSQL.CloseConn()

            PostmanSonDurumuAl = True

            Debug.WriteLine("Sipariş sorgulama tamamlandı")

        Catch ex As Exception
            ErrDisp("PostmanSonDurumuAl", "utilpostman",,, ex)
        End Try
    End Function

    Private Function CallAPI(Optional ByRef lSonuc As Boolean = False, Optional ByRef cErrorMessage As String = "", Optional postData As String = "",
                     Optional nCase As Integer = 1, Optional oIO As IO = IO.SendData) As String

        Dim cCredentials As String = ""
        Dim parts As String()
        Dim url As String = ""
        Dim cResult As String = ""

        CallAPI = ""

        If oIO = IO.SendData Then
            If nCase = 1 Or nCase = 2 Then
                url = "https://webpostman.kargozamani.com/restapi/client/consignment/add"
            Else
                url = "http://webpostman.interlineexpress.com:9999/restapi/client/consignment/add"
            End If
        Else
            If nCase = 1 Or nCase = 2 Then
                url = "https://webpostman.kargozamani.com/restapi/client/tracking"
            Else
                url = "http://webpostman.interlineexpress.com:9999/restapi/client/tracking"
            End If
        End If

        Dim userAgent As String = "Mozilla/5.0 (Windows; U; Windows NT 5.1; tr; rv:1.9.0.6) Gecko/2009011913 Firefox/3.0.6"
        Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
        request.UserAgent = userAgent
        request.Method = "POST"

        Select Case nCase
            Case 1
                ' FAVORI 2024
                cCredentials = "bp@gmail.com / pJNOPa7t3nsUCrE4WFvGx8MjBTS06yR29dzfHXQq"
                Dim header As New List(Of String) From {"Authorization: pJNOPa7t3nsUCrE4WFvGx8MjBTS06yR29dzfHXQq",
                                                        "From: bp@gmail.com"}
                For Each h As String In header
                    parts = h.Split(":")
                    request.Headers.Add(parts(0).Trim(), parts(1).Trim())
                Next
            Case 2
                ' MARES BESIKTAS
                cCredentials = "mares@gmail.com / kyIzFv9NAP7bVZdEp0mY5wrR3LBScqMsKHx1QX26"
                Dim header2 As New List(Of String) From {"Authorization: kyIzFv9NAP7bVZdEp0mY5wrR3LBScqMsKHx1QX26",
                                                         "From: mares@gmail.com"}
                For Each h As String In header2
                    parts = h.Split(":")
                    request.Headers.Add(parts(0).Trim(), parts(1).Trim())
                Next
            Case 3
                ' INTERLINE DAGITIM
                cCredentials = "mares@interlinekargo.com / 4NgyXODmG1KAqaELhnk3fcT5FB6JRHWC8PbItSQz"
                Dim header3 As New List(Of String) From {"Authorization: 4NgyXODmG1KAqaELhnk3fcT5FB6JRHWC8PbItSQz",
                                                         "From: mares@interlinekargo.com"}
                For Each h As String In header3
                    parts = h.Split(":")
                    request.Headers.Add(parts(0).Trim(), parts(1).Trim())
                Next
        End Select

        Dim byteArray As Byte() = System.Text.Encoding.UTF8.GetBytes(postData)
        request.ContentType = "application/x-www-form-urlencoded"
        request.ContentLength = byteArray.Length

        Using dataStream As Stream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
        End Using

        Try
            CreateLog("PostManSorgu", "CallAPI start : " + nCase.ToString + " / " + postData)

            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                Using reader As New StreamReader(response.GetResponseStream())
                    lSonuc = True
                    cResult = reader.ReadToEnd()
                    CreateLog("PostManSorgu", "CallAPI success : " + nCase.ToString + " / " + cCredentials + " / " + postData + " / " + cResult)
                    Return cResult
                End Using
            End Using

            CreateLog("PostManSorgu", "CallAPI fail-1 : " + nCase.ToString + " / " + cErrorMessage + " / " + cCredentials + " / " + postData)

            ' HTTP durum kodları
            ' 200 İstek başarı ile işlendi
            ' 201 İstek başarı ile işlendi ve kayıt oluşturuldu
            ' 401 Kimlik doğrulama hatası
            ' 404 İstenen kayıt bulunamadı
            ' 422 Veri hatası
            ' 500 Sunucu hatası

        Catch ex As WebException
            lSonuc = False
            Using response As HttpWebResponse = CType(ex.Response, HttpWebResponse)
                Using reader As New StreamReader(response.GetResponseStream())
                    cErrorMessage = reader.ReadToEnd()
                    CreateLog("PostManSorgu", "CallAPI fail-2 : " + nCase.ToString + " / " + cErrorMessage + " / " + cCredentials + " / " + postData)
                End Using
            End Using
        End Try
    End Function

    Private Sub SPMAyar()
        Try
            ' ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12 Or SecurityProtocolType.Ssl3
            ServicePointManager.ServerCertificateValidationCallback = Function(sender, certificate, chain, sslPolicyErrors) True

        Catch ex As Exception
            Console.WriteLine("SPMAyar : " & ex.Message)
        End Try
    End Sub


End Module
