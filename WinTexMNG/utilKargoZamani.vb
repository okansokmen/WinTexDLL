Option Explicit On

Imports RestSharp
Imports System
Imports System.Linq
Imports System.Collections.Generic
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Text
Imports System.Threading.Tasks
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Module utilKargoZamani

    Public Function KZ_SendOrder(cSiparisNo As String) As Boolean

        KZ_SendOrder = True

        Try
            SendPacket(cSiparisNo)

        Catch ex As Exception
            ErrDisp("KZ_SendOrder",,,, ex)
        End Try
    End Function

    Private Async Sub SendPacket(cSiparisNo As String)
        'province_name 
        'https://webpostman.kargozamani.com/restapi/client/province

        'county_name 
        'https://webpostman.kargozamani.com/restapi/client/county/{province_id}

        'district	
        'https://webpostman.kargozamani.com/restapi/client/district/{province_id}/{county_id}

        'amount_type_id
        'https://webpostman.kargozamani.com/restapi/client/consignment_types/103
        '1 => Toplu Gönderi (Toplu Zarf dergi kurye (kargo değil) adresli veya adressiz dağıtım. Taşıma bedeli gönderene fatura edilir. (amount) Kapıda Tahsilat bedeli girilmemelidir. )
        '2 => Ücret Alıcı (Taşıma bedeli alıcıdan alınır. Tahsilatlı kargo olamaz. Eğer Gönderi İade olmadıysa Taşıma bedeli gönderene fatura edilmez. İade Durumunda ise Taşıma bedeli gönderene fatura edilir.).)
        '3 => Peşin Ödeme (Tahsilat bedeli (amount) var ise alıcıdan tahsil edilir. Yani Kapıda Nakit Tahsilatlı kargo olur. Eğer Tahsilat bedeli (amount) yok ise tahsilatsız kargo olur. Her iki Durumdada alıcıdan taşıma bedeli alınmaz. Taşıma bedeli gönderene fatura edilir. )
        '5 => Kredi Kartı ( Gönderen Müşteri Sanal Ödeme ile hesabına aktarım. Kargo Dağıtım Zincirinde olan durumlarda geçerlidir. Özel talep ile aktif edilir. Taşıma bedeli gönderene fatura edilir.)
        '6 => Kapıda Kredi Kartı (Kapıda Kredi kartı ile tahsilat tutar kargo demektir. Tahsilat tutarı (amount) zorunludur. Alıcıdan ayrıca taşıma bedeli alınmaz. Taşıma bedeli gönderene fatura edilir.)

        'branch_code
        'https://webpostman.kargozamani.com/restapi/client/branches/63
        '109 => ARAS AYAZAĞA,
        '50 => KARGO,
        '880 => PTT KARGO,

        'currency_name
        'https://webpostman.kargozamani.com/restapi/client/currency

        'add_service_type_id
        'https://webpostman.kargozamani.com/restapi/client/consignment_types/105
        '1 => Kontrollü Teslim,
        '2 => Alma Haberli,
        '3 => Değer Konulmuş,
        '4 => GİTTİ GİDİYOR,
        '5 => N11,
        '6 => TRENYOL,
        '7 => HEPSİ BURADA,
        '8 => ÇİCEK SEPETİ,
        '9 => T-SOFT E-TİCARET,

        'distribution_type_id
        'https://webpostman.kargozamani.com/restapi/client/consignment_types/106
        '1 => Ertesi Gün Teslimat,
        '2 => Aynı Gün Teslimat,
        '3 => Geri Dönüşümlü Kargo,
        '4 => Uçak Kargo,
        '5 => Motokurye,
        '6 => Toplama,
        '7 => Şube Sevk Kolileri,

        Try
            Dim oSQL As New SQLServerClass
            Dim cSonuc As String = ""
            Dim cErrorMessage As String = ""
            Dim oContent As StringContent
            Dim cCustomer As String = ""
            Dim nAmount As Double = 0
            Dim cAmountTypeId As String = ""
            Dim cConsignmentTypeId As String = ""
            Dim cKargocuTahsilatYapmaz As String = ""
            Dim cOdemeTipi As String = ""
            Dim cTeslimSekli As String = ""
            Dim cSeller As String = ""

            oSQL.OpenConn()

            Dim oClient = New HttpClient()
            Dim oRequest = New HttpRequestMessage(HttpMethod.Post, oConnection.cKZApiUrl + "/consignment/add")
            oRequest.Headers.Add("From", oConnection.cKZApiUserName)
            oRequest.Headers.Add("Authorization", oConnection.cKZApiPassword)

            oSQL.cSQLQuery = "select top 1 tarih, adi, soyadi, ili, " +
                            " ilcesi, telefon, adres, kargofirmasi, odemetipi, " +
                            " teslimsekli, finaltutar, firma, " +
                            " kargocutahsilatyapmaz = (select top 1 kargocutahsilatyapmaz " +
                                                        " from odemetipi with (NOLOCK)  " +
                                                        " where kod = sipperakende.odemetipi) " +
                            " from sipperakende with (NOLOCK) " +
                            " where siparisno = '" + cSiparisNo.Trim + "' "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then

                cCustomer = oSQL.SQLReadString("adi") + " " + oSQL.SQLReadString("soyadi")
                nAmount = oSQL.SQLReadDouble("finaltutar")
                cKargocuTahsilatYapmaz = oSQL.SQLReadString("kargocutahsilatyapmaz")
                cOdemeTipi = oSQL.SQLReadString("odemetipi")
                cTeslimSekli = oSQL.SQLReadString("teslimsekli")
                cSeller = oSQL.SQLReadString("firma")

                'consignment_type_id	
                'https://webpostman.kargozamani.com/restapi/client/consignment_types/102
                '1 => Koli,
                '2 => Paket,
                '3 => Dosya,
                '4 => A4 Zarf,
                '5 => A5 Zarf,
                '6 => Diplomat,
                '7 => Dergi,
                '8 => Diğer,
                cConsignmentTypeId = "2"

                'amount_type_id
                '1 => Toplu Gönderi,
                '2 => Ücret Alıcı,
                '3 => Peşin Ödeme,
                '5 => Kredi Kartı (Müşteri),
                '6 => Kapıda Kredi Kartı,
                Select Case cOdemeTipi
                    Case "KAPIDA KREDI KARTI"
                        cAmountTypeId = "6"
                    Case "KAPIDA NAKIT"
                        cAmountTypeId = "3"
                    Case "PESIN - UCRET ALICI"
                        cAmountTypeId = "2"
                    Case "PESIN - UCRET GONDERICI"
                        cAmountTypeId = "5"
                    Case Else
                        cAmountTypeId = "6"
                End Select

                oContent = New StringContent("{" & vbCrLf &
                        "    ""customer"": """ + cCustomer + """," & vbCrLf &
                        "    ""province_name"": """ + oSQL.SQLReadString("ili") + """," & vbCrLf &
                        "    ""county_name"": """ + oSQL.SQLReadString("ilcesi") + """," & vbCrLf &
                        "    ""address"": """ + oSQL.SQLReadString("adres") + """," & vbCrLf &
                        "    ""telephone"": """ + oSQL.SQLReadString("telefon") + """," & vbCrLf &
                        "    ""branch_code"": ""109""," & vbCrLf &
                        "    ""barcode"": """ + cSiparisNo.Trim + """," & vbCrLf &
                        "    ""amount"": """ + SQLWriteDecimal(nAmount) + """," & vbCrLf &
                        "    ""currency_name"": ""TL""," & vbCrLf &
                        "    ""summary"": ""Kargo""," & vbCrLf &
                        "    ""quantity"": ""1""," & vbCrLf &
                        "    ""consignment_type_id"": """ + cConsignmentTypeId + """," & vbCrLf &
                        "    ""amount_type_id"": """ + cAmountTypeId + """," & vbCrLf &
                        "    ""add_service_type_id"": ""0""," & vbCrLf &
                        "    ""tax_rate"": ""8""," & vbCrLf &
                        "    ""order_number"": """ + cSiparisNo.Trim + """," & vbCrLf &
                        "    ""seller"": """ + cSeller.Trim + """," & vbCrLf &
                        "    ""weight"": ""1.00""," & vbCrLf &
                        "    ""total_weight"": ""1.00""," & vbCrLf &
                        "    ""bulk_weight"": ""1.00""," & vbCrLf &
                        "    ""total_bulk"": ""1.00""," & vbCrLf &
                        "    ""bulk_width"": """"," & vbCrLf &
                        "    ""bulk_height"": """"," & vbCrLf &
                        "    ""bulk_length"": """"," & vbCrLf &
                        "    ""bulk_value"": ""1.00""," & vbCrLf &
                        "    ""goods_value"": ""0,00""," & vbCrLf &
                        "    ""postman_sender"": """"" & vbCrLf &
                        "}", Nothing, "application/json")
                oRequest.Content = oContent
            End If
            oSQL.oReader.Close()

            Dim oResponse = Await oClient.SendAsync(oRequest)

            oResponse.EnsureSuccessStatusCode()

            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp("SendPacket",,,, ex)
        End Try
    End Sub

End Module
