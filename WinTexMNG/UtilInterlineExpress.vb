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

' burası çalışmıyor postman den mode=3 ile halledildi
' örnek kod olarak kaldı

Module UtilInterlineExpress

    ' kargo zamanı ile aynı çalışma şekli var
    ' API KEY   : 4NgyXODmG1KAqaELhnk3fcT5FB6JRHWC8PbItSQz
    ' API MAIL  : mares@interlinekargo.com
    ' API URL   : http://webpostman.interlineexpress.com:9999/
    ' ŞUBE KODU : IDM
    ' ŞUBE ADI  : İNTERLINE DAĞITIM

    ' Portal
    ' URL       : http://webpostman.interlineexpress.com:9999/user/login
    ' Username  : mares@interlinekargo.com
    ' PAssword  : Mares.3434

    Dim cSonuc As String = ""
    Dim cErrorMessage As String = ""

    Public Function IE_SendOrder(cSiparisNo As String, Optional ByRef cSonuc1 As String = "", Optional ByRef cErrorMessage1 As String = "") As Boolean

        IE_SendOrder = True

        Try
            Task.Run(Function() MainAsync(cSiparisNo)).Wait()

            cSonuc1 = cSonuc.Trim
            cErrorMessage1 = cErrorMessage.Trim

            If cSonuc.Trim = "OK" Then
                IE_SendOrder = True
            Else
                IE_SendOrder = False
            End If

        Catch ex As Exception
            ErrDisp("IE_SendOrder",,,, ex)
        End Try
    End Function

    Private Async Function MainAsync(cSiparisNo As String) As Task
        Await SendPacket(cSiparisNo)
    End Function

    Private Async Function SendPacket(cSiparisNo As String) As Task
        Try
            Dim oSQL As New SQLServerClass
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
            Dim oRequest = New HttpRequestMessage(HttpMethod.Post, oConnection.cIEApiUrl + "/consignment/add")
            oRequest.Headers.Add("From", oConnection.cIEApiUserName)
            oRequest.Headers.Add("Authorization", oConnection.cIEApiPassword)

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
                'https://webpostman.interlineexpress.com/restapi/client/consignment_types/102
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

            cErrorMessage = oResponse.ReasonPhrase

            If oResponse.IsSuccessStatusCode Then
                cSonuc = "OK"
            Else
                cSonuc = "HATA"
            End If

            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp("SendPacket",,,, ex)
        End Try
    End Function

End Module
