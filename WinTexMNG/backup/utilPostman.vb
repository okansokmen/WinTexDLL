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

' https://schema.postman.com

' http://webpostman.kargozamani.com:9999/user/login
' panel user  : benimpabucum@gmail.com
' panel şifre : Bp123654

' http://webpostman.kargozamani.com:9999/restapi/client
' API authorization : 7vCPjKLqSNdQDVx8cpHGUEbRJkgW1aBy9rX0M2nh

' Kargo Firması : KARGOZAMANI

' Panel İçin
' Panel Url   : https://webpostman.kargozamani.com/
' Panel User  : ayakkabilarim@kargozamani.com
' Panel Şifre : Ak20681*

' Api & entegrasyon İçin
' Api Url           : https://webpostman.kargozamani.com/restapi/client
' Api From          : ayakkabilarim@kargozamani.com
' Api Authorization : pJNOPa7t3nsUCrE4WFvGx8MjBTS06yR29dzfHXQq

Module utilPostman

    Public Async Sub PostmanGonderiYukle(Optional cSiparisNo As String = "")

        Try
            Dim sipno As String = ""
            Dim oSQL As New SQLServerClass
            Dim basarili As Integer = 0
            Dim basarisiz As Integer = 0
            Dim url As String = "https://webpostman.kargozamani.com/restapi/client/consignment/add"
            Dim cKargoTakipNo As String = ""

            Debug.WriteLine("Gönderiler yüklenmeye başladı")

            oSQL.OpenConn()

            oSQL.cSQLQuery = "SET DATEFORMAT YMD " +
                            " SELECT *, CONVERT(DECIMAL(18,2), finaltutar) AS GGF " +
                            " from sipperakende with (NOLOCK) " +
                            " where trendyolsipno is null " +
                            " and kargostatu = 'SIPARIS HAZIRLANIYOR' " +
                            " and kapandi is null " +
                            " and iptal is null " +
                            " and odemeislemno is null "

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
                    Key .telefon = oSQL.oReader("telefon")
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

                Dim data = New Dictionary(Of String, String) From {
                    {"customer", $"{item.adi.ToString().Trim()} {item.soyadi.ToString().Trim()}"},
                    {"province_name", item.ili.ToString().Trim()},
                    {"county_name", item.ilcesi.ToString().Trim()},
                    {"address", $"{item.adres.ToString().Trim()} {item.ilcesi.ToString().Trim()} {item.ili.ToString().Trim()}"},
                    {"telephone", item.telefon.ToString().Trim()},
                    {"branch_code", "113"},
                    {"barcode", item.siparisno.ToString().Trim()},
                    {"amount", tutar},
                    {"summary", "AYAKKABI"},
                    {"start_branch", "Sair nedim"},
                    {"quantity", "1"},
                    {"consignment_type_id", "3"},
                    {"amount_type_id", odemetipi.ToString()},
                    {"order_number", item.siparisno.ToString().Trim()},
                    {"seller", "Benim Pabucum"}
                }

                Await CallAPI2("POST", url, data, item.siparisno.ToString.Trim)
                basarili += 1
            Loop
            oSQL.oReader.Close()

            oSQL.CloseConn()

            Debug.WriteLine("Gönderiler yüklendi")
            Debug.WriteLine(basarili)

        Catch ex As Exception
            ErrDisp("PostmanGonderiYukle",,,, ex)
        End Try
    End Sub

    Private Async Function CallAPI2(ByVal method As String, ByVal url As String, ByVal data As Dictionary(Of String, String), ByVal cSiparisNo As String) As Task

        Dim oSQL As SQLServerClass

        Try
            Using client = New HttpClient()
                client.DefaultRequestHeaders.Add("Authorization", "pJNOPa7t3nsUCrE4WFvGx8MjBTS06yR29dzfHXQq")
                client.DefaultRequestHeaders.Add("From", "bp@gmail.com")

                Dim response As HttpResponseMessage = Nothing

                If method = "GET" Then
                    Dim queryString = (New FormUrlEncodedContent(data)).ReadAsStringAsync().Result
                    response = Await client.GetAsync($"{url}?{queryString}")
                ElseIf method = "POST" Then
                    Dim content = New FormUrlEncodedContent(data)
                    response = Await client.PostAsync(url, content)
                ElseIf method = "PUT" Then
                    Dim content = New FormUrlEncodedContent(data)
                    response = Await client.PutAsync(url, content)
                ElseIf method = "DELETE" Then
                    Dim content = New FormUrlEncodedContent(data)
                    response = Await client.DeleteAsync(url)
                End If

                If response.IsSuccessStatusCode Then
                    If method = "POST" Then
                        oSQL = New SQLServerClass
                        oSQL.OpenConn()

                        oSQL.cSQLQuery = "update sipperakende " +
                                        " set postmanwrite = getdate() " +
                                        " where siparisno = '" + cSiparisNo + "' "

                        oSQL.SQLExecute()
                        oSQL.CloseConn()
                    End If
                Else
                    Console.WriteLine($"Error: {response.StatusCode}")
                End If
            End Using

        Catch ex As Exception
            ErrDisp("CallAPI2",,,, ex)
        End Try
    End Function

    Public Async Sub PostmanSonDurumuAl(Optional cSiparisNo As String = "")

        Try
            Dim sipno As String = ""
            Dim oSQL As New SQLServerClass
            Dim sifaf = New List(Of Dictionary(Of String, Object))()
            Dim sayi As Integer = 0
            Dim cDesi As String = ""
            Dim cKargoTakipUrl As String = ""
            Dim cMusteriBarkod As String = ""
            Dim url As String = "https://webpostman.kargozamani.com/restapi/client/tracking"
            Dim cKargoTakipNo As String = ""

            Debug.WriteLine("Sipariş sorgulama başladı")

            oSQL.OpenConn()

            oSQL.cSQLQuery = "SET DATEFORMAT YMD " +
                            " select * , CONVERT(DECIMAL(18,2),finaltutar) AS GGF " +
                            " from sipperakende with (NOLOCK) " +
                            " where CONVERT(int,printbatch) >= 13000  " +
                            " and aracikargo = 'FAVORI 2024' " +
                            " and trendyolsipno is null " +
                            " and kapandi is null " +
                            " and iptal is null " +
                            " and ticimaxsipno is null " +
                            " and odemeislemno is null "

            If cSiparisNo.Trim <> "" Then
                oSQL.cSQLQuery = oSQL.cSQLQuery +
                            " and siparisno = '" + cSiparisNo.Trim + "' "
            End If

            oSQL.cSQLQuery = oSQL.cSQLQuery +
                            " order by tarih "

            oSQL.GetSQLReader()

            Do While oSQL.oReader.Read()
                Dim row = New Dictionary(Of String, Object)()
                For i As Integer = 0 To oSQL.oReader.FieldCount - 1
                    row(oSQL.oReader.GetName(i)) = oSQL.oReader.GetValue(i)
                Next i
                sifaf.Add(row)
            Loop
            oSQL.oReader.Close()

            For Each item In sifaf
                sipno = item("siparisno").ToString().Trim()
                Debug.WriteLine("Sipariş durumu sorgulanıyor" + sipno)

                Dim data = New Dictionary(Of String, Object) From {
                    {"durums", -1},
                    {"sipno", sipno}
                }

                Dim kargolar = Await CallAPI("POST", url, data)
                sayi += 1

                Dim trendssList = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(kargolar)
                If trendssList("toplam_kayit").ToString() = "1" Then
                    Dim trendss = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(trendssList("data").ToString())
                    If trendss("durum").ToString() <> "0" Then
                        Debug.WriteLine(trendss)
                        If trendss("durum").ToString() = "4" Then

                            cDesi = trendss("desi").ToString().Substring(0, trendss("desi").ToString().Length - 3)
                            cKargoTakipNo = trendss("cikisno")
                            cKargoTakipUrl = "http://kargotakip.araskargo.com.tr/mainpage.aspx?code=" + trendss("cikisno")
                            cMusteriBarkod = trendss("musteribarkod")

                            oSQL.cSQLQuery = "update sipperakende " +
                                        " set sevkedildi='E', " +
                                        " sevktarihi = getdate(), " +
                                        " kargokgdesi = " + cDesi + " , " +
                                        " kargofirmasi = 'ARAS', " +
                                        " cikis_sube = 'AYAZAĞA', " +
                                        " kargostatu = 'YOLDA', " +
                                        " kargostatutarihi = getdate(), " +
                                        " kargotakipno = '" + cKargoTakipNo + "', " +
                                        " kargotakipurl = '" + cKargoTakipUrl + "', " +
                                        " postmanread = getdate() " +
                                        " where siparisno = '" + cMusteriBarkod + "' "

                            oSQL.SQLExecute()
                        End If
                    End If
                End If
            Next item

            oSQL.CloseConn()

            Debug.WriteLine("Sipariş sorgulama tamamlandı")

        Catch ex As Exception
            ErrDisp("PostmanSonDurumuAl",,,, ex)
        End Try
    End Sub

    Private Async Function CallAPI(ByVal method As String, ByVal url As String, Optional ByVal data As Dictionary(Of String, Object) = Nothing) As Task(Of String)

        Try
            Using client = New HttpClient()

                client.DefaultRequestHeaders.Add("Authorization", "pJNOPa7t3nsUCrE4WFvGx8MjBTS06yR29dzfHXQq")
                client.DefaultRequestHeaders.Add("From", "bp@gmail.com")
                Dim content = New StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json")
                Dim response As New HttpResponseMessage()

                Select Case method
                    Case "GET"
                        response = Await client.GetAsync(url + "?" + Await New FormUrlEncodedContent(data).ReadAsStringAsync())
                    Case "POST"
                        response = Await client.PostAsync(url, content)
                    Case "PUT"
                        response = Await client.PutAsync(url, content)
                    Case "DELETE"
                        response = Await client.DeleteAsync(url)
                End Select

                Return Await response.Content.ReadAsStringAsync()

            End Using

        Catch ex As Exception
            ErrDisp("CallAPI",,,, ex)
        End Try
    End Function



    Private Function HTTPDurumAciklamasi(nHataKodu As Integer) As String

        HTTPDurumAciklamasi = "Bilinmeyen hata kodu"

        Select Case nHataKodu
            Case 200 : HTTPDurumAciklamasi = "İstek başarı ile işlendi"
            Case 201 : HTTPDurumAciklamasi = "İstek başarı ile işlendi ve kayıt oluşturuldu"
            Case 401 : HTTPDurumAciklamasi = "Kimlik doğrulama hatası"
            Case 404 : HTTPDurumAciklamasi = "İstenen kayıt bulunamadı"
            Case 422 : HTTPDurumAciklamasi = "Veri hatası"
            Case 500 : HTTPDurumAciklamasi = "Sunucu hatası"
        End Select
    End Function

    Private Function StatuNoAciklamasi(cStatuKodu As String) As String

        StatuNoAciklamasi = "Bilinmeyen statu kodu"

        Select Case cStatuKodu
            Case "00" : StatuNoAciklamasi = "Kabul Bekliyor"
            Case "01" : StatuNoAciklamasi = "Kabul Edildi"
            Case "10" : StatuNoAciklamasi = "Teslim Edildi"
            Case "20" : StatuNoAciklamasi = "İade - İade Süreci Başlatıldı"
            Case "21" : StatuNoAciklamasi = "İade - Göndericiye İade Edildi"
            Case "22" : StatuNoAciklamasi = "İade - Kurye İade Sürecini Başlattı"
            Case "23" : StatuNoAciklamasi = "İade - İade Çıkış Şubesine Ulaştı"
            Case "24" : StatuNoAciklamasi = "İade - Şubeden İade Süreci Başlatıldı"
            Case "30" : StatuNoAciklamasi = "Teslim Edilemedi"
            Case "40" : StatuNoAciklamasi = "Transfer Sürecinde"
            Case "41" : StatuNoAciklamasi = "Teslimat Şubesinde"
            Case "42" : StatuNoAciklamasi = "Kurye Dağıtımda"
            Case "50" : StatuNoAciklamasi = "Teslim Edilemedi - Tekrar Dağıtım Planlanında"
            Case "60" : StatuNoAciklamasi = "Teslim Edilemedi - Teslimat Şubesinde"
        End Select
    End Function

    Private Function GetSehir(cKod As String) As String

        cKod = "Bilinmeyen şehir kodu"

        Select Case cKod
            Case "1" : cKod = "ADANA"
            Case "2" : cKod = "ADIYAMAN"
            Case "3" : cKod = "AFYONKARAHİSAR"
            Case "4" : cKod = "AĞRI"
            Case "5" : cKod = "AMASYA"
            Case "6" : cKod = "ANKARA"
            Case "7" : cKod = "ANTALYA"
            Case "8" : cKod = "ARTVİN"
            Case "9" : cKod = "AYDIN"
            Case "10" : cKod = "BALIKESİR"
            Case "11" : cKod = "BİLECİK"
            Case "12" : cKod = "BİNGÖL"
            Case "13" : cKod = "BİTLİS"
            Case "14" : cKod = "BOLU"
            Case "15" : cKod = "BURDUR"
            Case "16" : cKod = "BURSA"
            Case "17" : cKod = "ÇANAKKALE"
            Case "18" : cKod = "ÇANKIRI"
            Case "19" : cKod = "ÇORUM"
            Case "20" : cKod = "DENİZLİ"
            Case "21" : cKod = "DİYARBAKIR"
            Case "22" : cKod = "EDİRNE"
            Case "23" : cKod = "ELAZIĞ"
            Case "24" : cKod = "ERZİNCAN"
            Case "25" : cKod = "ERZURUM"
            Case "26" : cKod = "ESKİŞEHİR"
            Case "27" : cKod = "GAZİANTEP"
            Case "28" : cKod = "GİRESUN"
            Case "29" : cKod = "GÜMÜŞHANE"
            Case "30" : cKod = "HAKKARİ"
            Case "31" : cKod = "HATAY"
            Case "32" : cKod = "ISPARTA"
            Case "33" : cKod = "MERSİN"
            Case "34" : cKod = "İSTANBUL"
            Case "35" : cKod = "İZMİR"
            Case "36" : cKod = "KARS"
            Case "37" : cKod = "KASTAMONU"
            Case "38" : cKod = "KAYSERİ"
            Case "39" : cKod = "KIRKLARELİ"
            Case "40" : cKod = "KIRŞEHİR"
            Case "41" : cKod = "KOCAELİ"
            Case "42" : cKod = "KONYA"
            Case "43" : cKod = "KÜTAHYA"
            Case "44" : cKod = "MALATYA"
            Case "45" : cKod = "MANİSA"
            Case "46" : cKod = "KAHRAMANMARAŞ"
            Case "47" : cKod = "MARDİN"
            Case "48" : cKod = "MUĞLA"
            Case "49" : cKod = "MUŞ"
            Case "50" : cKod = "NEVŞEHİR"
            Case "51" : cKod = "NİĞDE"
            Case "52" : cKod = "ORDU"
            Case "53" : cKod = "RİZE"
            Case "54" : cKod = "SAKARYA"
            Case "55" : cKod = "SAMSUN"
            Case "56" : cKod = "SİİRT"
            Case "57" : cKod = "SİNOP"
            Case "58" : cKod = "SİVAS"
            Case "59" : cKod = "TEKİRDAĞ"
            Case "60" : cKod = "TOKAT"
            Case "61" : cKod = "TRABZON"
            Case "62" : cKod = "TUNCELİ"
            Case "63" : cKod = "ŞANLIURFA"
            Case "64" : cKod = "UŞAK"
            Case "65" : cKod = "VAN"
            Case "66" : cKod = "YOZGAT"
            Case "67" : cKod = "ZONGULDAK"
            Case "68" : cKod = "AKSARAY"
            Case "69" : cKod = "BAYBURT"
            Case "70" : cKod = "KARAMAN"
            Case "71" : cKod = "KIRIKKALE"
            Case "72" : cKod = "BATMAN"
            Case "73" : cKod = "ŞIRNAK"
            Case "74" : cKod = "BARTIN"
            Case "75" : cKod = "ARDAHAN"
            Case "76" : cKod = "IĞDIR"
            Case "77" : cKod = "YALOVA"
            Case "78" : cKod = "KARABÜK"
            Case "79" : cKod = "KİLİS"
            Case "80" : cKod = "OSMANİYE"
            Case "81" : cKod = "DÜZCE"
            Case "82" : cKod = "YURTDIŞI"
        End Select
    End Function

End Module
