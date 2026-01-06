Option Explicit On
Option Strict On

Imports System.Xml
Imports System.Configuration
Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json
Imports System.Net
Imports System.Net.Security
Imports System.Net.ServicePointManager
Imports System.Security.Cryptography.X509Certificates

Module UtilGenel

    Private Const LoginUrl As String = "https://ykkapi.ykk.com.tr/Authentication/Login"
    Private Const GetOrderUrl As String = "https://ykkapi.ykk.com.tr/api/Order/GetOrder"
    Private Const OrderUrl As String = "https://ykkapi.ykk.com.tr/api/Order/PostOrder"

    Public oYKKService As YKKService
    Dim cToken As String = ""

    Public Function YKKAktarOUS(cFilter As String) As Boolean

        Dim cSQL As String = ""

        YKKAktarOUS = False

        Try
            Dim oSQL As SQLServerClass
            Dim cSonuc As String = ""
            Dim cYKK As String = ""
            Dim conaysizisemrikontrol As String = "0"
            Dim cMessage As String = ""

            cToken = GetTokenAsync().Result

            If String.IsNullOrEmpty(cToken) Then
                Exit Function
            End If

            oSQL = New SQLServerClass
            oSQL.OpenConn()

            cSQL = "select top 1 parametervalue " +
                    " from syspar with (NOLOCK) " +
                    " where parametername = 'ykkfirma' "

            cYKK = oSQL.DBReadString(cSQL)

            cSQL = "select top 1 parametervalue " +
                    " from syspar with (NOLOCK) " +
                    " where parametername = 'onaysizisemrikontrol' "

            conaysizisemrikontrol = oSQL.DBReadString(cSQL)

            cSQL = "select distinct a.isEmriNo " +
                    " from isemri a with (NOLOCK), isemrilines b with (NOLOCK), stok c with (NOLOCK) " +
                    " where a.isEmriNo = b.isEmriNo " +
                    " and b.stokno = c.stokno " +
                    " and a.isEmriNo is not null " +
                    " and a.isEmriNo <> '' " +
                    " and (a.ykksiparisno is null or a.ykksiparisno = '') " +
                    " and a.firma = '" + cYKK.Trim + "' " +
                    " and a.teslimyeri is not null " +
                    " and a.teslimyeri <> '' " +
                    " and (a.isemriok is null or a.isemriok = 'H' or a.isemriok = '') " +
                    cFilter

            If conaysizisemrikontrol.Trim = "1" Then
                cSQL += " and a.onay = 'E' "
            End If

            oSQL.GetSQLReader(cSQL)

            Do While oSQL.oReader.Read
                cSonuc = SendIsemri2(oSQL.SQLReadString("isemrino"))
                cMessage = "YKK işemri aktarımı / işemri no : " + oSQL.SQLReadString("isemrino") + vbCrLf +
                           "YKK API sonucu : " + cSonuc
                oYKKService.CreateLog(cMessage, 1)
            Loop

            oSQL.oReader.Close()
            oSQL.CloseConn()
            oSQL = Nothing

            YKKAktarOUS = True

        Catch ex As Exception
            ErrDisp(ex, "YKKAktarOUS", cSQL)
        End Try
    End Function

    Public Function YKKSorgulaOUS(cFilter As String) As Boolean

        Dim cSQL As String = ""

        YKKSorgulaOUS = False

        Try
            Dim oSQL As SQLServerClass
            Dim cYKK As String = ""
            Dim cSonuc As String = ""
            Dim cMessage As String = ""

            cToken = GetTokenAsync().Result

            If String.IsNullOrEmpty(cToken) Then
                Exit Function
            End If

            oSQL = New SQLServerClass
            oSQL.OpenConn()

            cSQL = "select distinct a.isEmriNo  " +
                    " from isemri a with (NOLOCK), isemrilines b with (NOLOCK), stok c with (NOLOCK) " +
                    " where a.isEmriNo = b.isEmriNo " +
                    " and b.stokno = c.stokno " +
                    " and a.isEmriNo is not null " +
                    " and a.isEmriNo <> '' " +
                    " and a.ykksiparisno is not null " +
                    " and a.ykksiparisno <> '' " +
                    " and (a.isemriok is null or a.isemriok = 'H' or a.isemriok = '') " +
                    cFilter

            oSQL.GetSQLReader(cSQL)

            Do While oSQL.oReader.Read
                cSonuc = GetIsemriDurum2(oSQL.SQLReadString("isemrino"))
                cMessage = "YKK işemri no : " + oSQL.SQLReadString("isemrino") + vbCrLf +
                           "YKK iş emri son durumu : " + cSonuc
                oYKKService.CreateLog(cMessage, 1)
            Loop

            oSQL.oReader.Close()
            oSQL.CloseConn()
            oSQL = Nothing

            YKKSorgulaOUS = True

        Catch ex As Exception
            ErrDisp(ex, "YKKSorgulaOUS", cSQL)
        End Try
    End Function

    Public Function GetIsemriDurum2(ByVal cIsemriNo As String) As String

        GetIsemriDurum2 = ""

        Try
            Dim oSQL As New SQLServerClass
            Dim cSiparisNo As String = ""
            Dim cOrderData As String = ""
            Dim cResult As String = ""
            Dim cKapanis As String = ""
            Dim dKapanis As Date = #1/1/1950#
            Dim cTermin As String = ""
            Dim dTermin As Date = #1/1/1950#
            Dim nMiktar As Double = 0
            Dim cStokNo As String = ""

            CreateLogFile("Sorgulanan isemri : " + cIsemriNo.Trim)

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select top 1 ykksiparisno " +
                                " from isemri with (NOLOCK) " +
                                " where isemrino = '" + cIsemriNo + "' "
            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                cSiparisNo = oSQL.SQLReadString("ykksiparisno")
            Else
                oSQL.oReader.Close()
                oSQL.CloseConn()
                CreateLogFile("Isemri bulunamadi : " + cIsemriNo)
                Exit Function
            End If
            oSQL.oReader.Close()

            If cSiparisNo.Trim = "" Then
                oSQL.CloseConn()
                CreateLogFile("Isemrinde YKK Siparis no bos")
                Exit Function
            End If

            CreateLogFile("YKK Siparis : " + cSiparisNo)

            Dim orderData = GetOrderDataAsync(cSiparisNo).Result

            If Not String.IsNullOrEmpty(orderData) Then

                Dim formattedJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(orderData), Newtonsoft.Json.Formatting.Indented)
                Dim orders As List(Of Order) = JsonConvert.DeserializeObject(Of List(Of Order))(formattedJson)

                For Each order In orders

                    cResult = ""
                    dKapanis = #1/1/1950#

                    If Not IsNothing(order.orderStatus) Then
                        cResult = order.orderStatus.ToString.Trim
                    End If

                    If Not IsNothing(order.orderCompleteDate) Then
                        cKapanis = order.orderCompleteDate.ToString
                        cKapanis = Mid(cKapanis, 1, 10)
                        dKapanis = CDate(cKapanis)
                    End If

                    CreateLogFile("Okunan isemri : " + cIsemriNo.Trim +
                                      " YKK Siparis : " + cSiparisNo +
                                      " Sonuc : " + cResult.Trim +
                                      " Kapanis : " + SQLWriteDate(dKapanis))

                    'Console.WriteLine($"Status: {cResult}")
                    'Console.WriteLine($"Complete Date: {dKapanis}")
                    'Console.WriteLine("Items:")

                    For Each item In order.apiOrderItemsDetailResults

                        cTermin = ""
                        dTermin = #1/1/1950#
                        nMiktar = 0
                        cStokNo = ""

                        If Not IsNothing(item.customerProductCode) Then
                            cStokNo = item.customerProductCode.ToString.Trim
                        End If

                        If Not IsNothing(item.salesQuantity) Then
                            nMiktar = CDbl(item.salesQuantity)
                        End If

                        If Not IsNothing(item.leadTimeDate) Then
                            cTermin = item.leadTimeDate.ToString
                            cTermin = Mid(cTermin, 1, 10)
                            dTermin = CDate(cTermin)
                        End If

                        If cStokNo.Trim <> "" Then

                            oSQL.cSQLQuery = "set dateformat dmy " +
                                            " update isemrilines set " +
                                            " termintarihi = '" + SQLWriteDate(dTermin) + "', " +
                                            " uretilen = " + SQLWriteDecimal(nMiktar) +
                                            " where isemrino = '" + cIsemriNo.Trim + "' " +
                                            " and stokno = '" + cStokNo.Trim + "' "
                            oSQL.SQLExecute()
                        End If

                        'Console.WriteLine($" - Product: {cStokNo}, Quantity: {nMiktar}, Lead Time: {dTermin}")
                        'Console.WriteLine("Stok No : " + cStokNo +
                        '                  " Miktar : " + SQLWriteDecimal(nMiktar) +
                        '                  " Termin : " + SQLWriteDate(dTermin))
                    Next
                Next

                'Console.WriteLine(vbNewLine & "Sipariş Verileri:")
                'Console.WriteLine(formattedJson)

                oSQL.cSQLQuery = "update isemri set " +
                                " ykksondurumtarih = getdate(), " +
                                " ykksondurum = '" + SQLWriteString(cResult, 50) + "' " +
                                " where isemrino = '" + cIsemriNo.Trim + "' "
                oSQL.SQLExecute()

                If dKapanis <> #1/1/1950# Then

                    oSQL.cSQLQuery = "set dateformat dmy " +
                                    " update isemri set " +
                                    " kilitle = 'E', " +
                                    " isemriok = 'E', " +
                                    " oktarihi = '" + SQLWriteDate(dKapanis) + "' " +
                                    " where isemrino = '" + cIsemriNo.Trim + "' "
                    oSQL.SQLExecute()

                    oSQL.cSQLQuery = "update isemrilines set " +
                                    " kapandi = 'E' " +
                                    " where isemrino = '" + cIsemriNo.Trim + "' "
                    oSQL.SQLExecute()

                    CreateLogFile("Isemri kapatildi : " + cIsemriNo.Trim + " kapanıs tarihi : " + SQLWriteDate(dKapanis))
                End If

            Else
                CreateLogFile("Sorgulaması başarısız isemri : " + cIsemriNo.Trim)
            End If

            oSQL.CloseConn()

            CreateLogFile("Sorgulaması sonucu : " + cIsemriNo.Trim + " " + cResult.Trim)

            GetIsemriDurum2 = cResult.Trim

        Catch ex As Exception
            ErrDisp(ex, "GetIsemriDurum2")
        End Try
    End Function

    Public Function SendIsemri2(ByVal cIsEmriNo As String) As String

        SendIsemri2 = ""

        Try
            Dim oSQL As SQLServerClass
            Dim cBuyerCode As String = ""
            Dim cDeliverCode1 As String = ""
            Dim cDeliverCode2 As String = ""
            Dim cSasNo As String = ""
            Dim cResult As String = ""
            Dim dTarih As Date = #1/1/1950#
            Dim cNotlar As String = ""
            Dim cDurum As String = ""
            Dim cYKKSiparisNo As String = ""
            Dim cMail As String = ""
            Dim cMTF As String = ""
            Dim cDefaultBuyerCode As String = ""

            Dim oOrder As New Order
            Dim oOrderList As New List(Of Order)
            Dim oItem As ApiOrderItemDetailResult
            Dim oItemList As New List(Of ApiOrderItemDetailResult)

            If cIsEmriNo.Trim = "" Then
                Exit Function
            End If

            CreateLogFile("Gonderilen isemri : " + cIsEmriNo)

            oSQL = New SQLServerClass
            oSQL.OpenConn()

            cDefaultBuyerCode = oSQL.GetSysPar("ykkdefaultbuyer")

            oSQL.cSQLQuery = "select a.teslimyeri, a.tarih, a.notlar, a.ykksiparisno, b.email, "

            oSQL.cSQLQuery = oSQL.cSQLQuery +
                " ykkbuyercode = (select top 1 x.ykkbuyer " +
                                " from firma x with (NOLOCK) , isemrilines y with (NOLOCK) , siparis z with (NOLOCK) , sipmodel q with (NOLOCK) " +
                                " where y.malzemetakipno = q.malzemetakipno " +
                                " and q.siparisno = z.kullanicisipno " +
                                " and z.musterino = x.firma " +
                                " and y.isemrino = a.isemrino " +
                                " and x.ykkbuyer is not null " +
                                " and x.ykkbuyer <> '') , "
            oSQL.cSQLQuery = oSQL.cSQLQuery +
                " delivercode1 = (select top 1 kod1 " +
                                " from isemriteslimyeri with (NOLOCK) " +
                                " where teslimyeri = a.teslimyeri) , "
            oSQL.cSQLQuery = oSQL.cSQLQuery +
                " delivercode2 = (select top 1 kod2 " +
                                " from isemriteslimyeri with (NOLOCK) " +
                                " where teslimyeri = a.teslimyeri) "
            oSQL.cSQLQuery = oSQL.cSQLQuery +
                " from isemri a with (NOLOCK) , personel b with (NOLOCK) " +
                " where a.takipelemani = b.personel " +
                " and a.isemrino = '" + cIsEmriNo.Trim + "' "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                cYKKSiparisNo = oSQL.SQLReadString("ykksiparisno")
                cBuyerCode = oSQL.SQLReadString("ykkbuyercode")
                cDeliverCode1 = oSQL.SQLReadString("delivercode1")
                cDeliverCode2 = oSQL.SQLReadString("delivercode2")
                cNotlar = oSQL.SQLReadString("notlar")
                cMail = oSQL.SQLReadString("email")
            Else
                oSQL.oReader.Close()
                oSQL.CloseConn()
                CreateLogFile("Isemri bulunamadi : " + cIsEmriNo)
                Exit Function
            End If
            oSQL.oReader.Close()

            If cYKKSiparisNo.Trim <> "" Then
                oSQL.CloseConn()
                CreateLogFile("Isemri daha once YKK ya gonderilmis : " + cIsEmriNo)
                Exit Function
            End If

            If cBuyerCode.Trim = "" Then
                cBuyerCode = cDefaultBuyerCode.Trim
            End If

            oSQL.cSQLQuery = "select termintarihi, malzemetakipno " +
                            " from isemrilines with (NOLOCK) " +
                            " where isemrino = '" + cIsEmriNo.Trim + "' " +
                            " and termintarihi is not null " +
                            " and termintarihi <> '01.01.1950' " +
                            " order by termintarihi "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                dTarih = oSQL.SQLReadDate("termintarihi")
                cMTF = oSQL.SQLReadString("malzemetakipno")
            End If
            oSQL.oReader.Close()

            oSQL.cSQLQuery = "select a.stokno, a.renk, a.beden, b.birim1, " +
                            " miktar = sum(coalesce(a.miktar1,0)) " +
                            " from isemrilines a with (NOLOCK) , stok b with (NOLOCK) " +
                            " where a.stokno = b.stokno " +
                            " and a.isemrino = '" + cIsEmriNo.Trim + "' " +
                            " group by a.stokno, a.renk, a.beden, b.birim1 " +
                            " order by a.stokno, a.renk, a.beden, b.birim1 "

            oSQL.GetSQLReader()

            Do While oSQL.oReader.Read

                oItem = New ApiOrderItemDetailResult
                oItem.customersProductCode = oSQL.SQLReadString("stokno")
                oItem.productCode = String.Empty
                oItem.color = String.Empty
                oItem.length = String.Empty
                oItem.count = CInt(oSQL.SQLReadDouble("miktar"))

                oItemList.Add(oItem)
            Loop
            oSQL.oReader.Close()

            If cMTF.Trim = "" Then
                cSasNo = cIsEmriNo
            Else
                cSasNo = cIsEmriNo + "-" + cMTF
            End If

            oOrder.deliverCode1 = cDeliverCode1
            oOrder.deliverCode2 = cDeliverCode2
            oOrder.buyerCode = cBuyerCode
            oOrder.purchaseContractNo = cSasNo
            oOrder.associatedMail = cMail
            oOrder.timeConvention = 2
            oOrder.RequestDate = dTarih.ToString("yyyy-MM-dd")
            oOrder.sampleOrder = False
            oOrder.noCommercial = False
            oOrder.completeDelivery = True
            oOrder.orderComment = SQLWriteString(cNotlar, 104)
            oOrder.ApiOrderItems = oItemList

            cResult = SendOrderDataAsync(oOrder).Result

            If cResult.Trim = "" Then
                SendIsemri2 = ""
                CreateLogFile("Isemri GONDERILEMEDI : " + cSasNo)
            Else

                cResult = Replace(cResult, Chr(34), "")

                oSQL.cSQLQuery = "update isemri set " +
                                " ykksiparisno = '" + cResult.Trim + "', " +
                                " ykkgonderim = getdate(), " +
                                " ykksondurumtarih = getdate(), " +
                                " ykksondurum = 'Gonderildi' " +
                                " where isemrino = '" + cIsEmriNo.Trim + "' "

                oSQL.SQLExecute()

                CreateLogFile("Isemri gonderildi : " + cSasNo + " " + cResult.Trim)

                SendIsemri2 = cResult.Trim
            End If

            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp(ex, "SendIsemri2")
        End Try
    End Function

    Private Async Function SendOrderDataAsync(oOrder As Order) As Task(Of String)
        Try
            SPMAyar()

            Using client As New HttpClient()

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " & cToken)
                'client.Timeout = TimeSpan.FromSeconds(30)

                Dim json = JsonConvert.SerializeObject(oOrder)
                'Console.WriteLine("Gönderilen JSON:")
                'Console.WriteLine(JsonConvert.SerializeObject(oOrder, Formatting.Indented))

                Dim content = New StringContent(json, Encoding.UTF8, "application/json")

                Dim response = Await client.PostAsync(OrderUrl, content).ConfigureAwait(False)
                Dim responseContent = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)

                If Not response.IsSuccessStatusCode Then
                    CreateLogFile("Isemri gönderilirken hata oluştu. " +
                                  " Durum Kodu: " + response.StatusCode.ToString +
                                  " Hata Detayı: " + responseContent.ToString)
                    Return ""
                End If

                CreateLogFile("Basariyla gonderilen isemri : " + oOrder.purchaseContractNo)
                Return responseContent
            End Using

        Catch ex As Exception
            ErrDisp(ex, "SendOrderDataAsync")
        End Try
    End Function

    Private Async Function GetTokenAsync() As Task(Of String)
        Try
            Using client As New HttpClient()

                'client.Timeout = TimeSpan.FromSeconds(30)

                Dim loginData = New With {
                    .username = "ykk@eroglugiyim.com",
                    .password = "€r0Glu2019!"
                }

                Dim json = JsonConvert.SerializeObject(loginData)
                Dim content = New StringContent(json, Encoding.UTF8, "application/json")

                Dim response = Await client.PostAsync(LoginUrl, content).ConfigureAwait(False)
                'response.EnsureSuccessStatusCode()
                Return Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
            End Using

        Catch ex As Exception
            ErrDisp(ex, "GetTokenAsync token alınırken hata oluştu")
            If ex.InnerException IsNot Nothing Then
                ErrDisp(ex, "GetTokenAsync detay: " & ex.InnerException.Message)
            End If
        End Try
        Return String.Empty
    End Function

    Private Async Function GetOrderDataAsync(cSiparisNo As String) As Task(Of String)
        Try
            SPMAyar()

            Using client As New HttpClient()

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " & cToken)
                'client.Timeout = TimeSpan.FromSeconds(30)

                Dim encodedContract = Uri.EscapeDataString(cSiparisNo)
                Dim url = GetOrderUrl & "?orderCodes=" & encodedContract

                Dim response = Await client.GetAsync(url).ConfigureAwait(False)
                'response.EnsureSuccessStatusCode()
                Return Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
            End Using

        Catch ex As Exception
            ErrDisp(ex, "GetOrderDataAsync sipariş verileri alınırken hata oluştu")
            If ex.InnerException IsNot Nothing Then
                ErrDisp(ex, "GetOrderDataAsync detay: " & ex.InnerException.Message)
            End If
        End Try
        Return String.Empty
    End Function

    Private Sub SPMAyar()
        Try
            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)

            ServicePointManager.ServerCertificateValidationCallback =
            Function(sender As Object, certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors)

                If certificate Is Nothing Then
                    Return False
                End If

                Dim expectedThumbprint As String = "684CD5721BD21B73D5FE722F870C605B3662583A"

                Try
                    'Console.WriteLine("Sertifika doğrulama başladı")
                    Dim cert As New X509Certificate2(certificate)
                    Dim receivedThumbprint As String = cert.Thumbprint.Replace(" ", "").ToUpper()
                    Dim expectedThumbprintClean As String = expectedThumbprint.Replace(" ", "").ToUpper()

                    'Console.WriteLine("Sertifika Detayları:")
                    'Console.WriteLine($"Subject: {certificate.Subject}")
                    'Console.WriteLine($"Issuer: {certificate.Issuer}")
                    'Console.WriteLine($"Alınan Thumbprint: {receivedThumbprint}")
                    'Console.WriteLine($"Beklenen Thumbprint: {expectedThumbprintClean}")

                    If receivedThumbprint = expectedThumbprintClean Then
                        'Console.WriteLine("Sertifika doğrulama başarılı")
                        Return True
                    Else
                        'Console.WriteLine($"Sertifika doğrulama hatası:")
                        'Console.WriteLine($"Beklenen: {expectedThumbprintClean}")
                        'Console.WriteLine($"Alınan: {receivedThumbprint}")
                        Return False
                    End If

                Catch ex As Exception
                    ErrDisp(ex, "SPMAyar")
                    Return False
                End Try
            End Function

        Catch ex As Exception
            ErrDisp(ex, "SPMAyar")
        End Try
    End Sub

    Public Class Order
        Public Property purchaseOrderNo As String
        Public Property purchaseContractNo As String
        Public Property orderStatus As String
        Public Property buyerCode As String
        Public Property priceListVersion As String
        Public Property customerCode As String
        Public Property completeDelivery As Boolean
        Public Property deliverCode1 As String
        Public Property deliverCode2 As String
        Public Property noCommercial As Boolean
        Public Property timeConvention As Integer
        Public Property sampleOrder As Boolean
        Public Property currencyCode As String
        Public Property associatedMail As String
        Public Property itemDetailCount As Integer
        Public Property confirmDate As String
        Public Property leadTimeDate As String
        Public Property waybillDate As String
        Public Property billDate As String
        Public Property orderCompleteDate As String
        Public Property RequestDate As String
        Public Property orderComment As String
        Public Property ApiOrderItems As List(Of ApiOrderItemDetailResult)
        Public Property apiOrderItemsDetailResults As List(Of ApiOrderItemDetailResult)
    End Class

    Public Class ApiOrderItemDetailResult
        Public Property length As String
        Public Property color As String

        ' üretilen adet / ykk dan geliyor
        Public Property salesQuantity As Double
        Public Property customerProductCode As String

        ' istenen adet / ben gönderiyorum
        Public Property count As Integer
        Public Property customersProductCode As String

        Public Property productCode As String
        Public Property leadTimeDate As String
        Public Property description As String
    End Class

End Module
