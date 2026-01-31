Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json
Imports System.Net
Imports System.Net.Security
Imports System.Net.ServicePointManager
Imports System.Security.Cryptography.X509Certificates

Module YKKModule

    ' https://ykkapi.ykk.com.tr/swagger/index.html
    Private Const LoginUrl As String = "https://ykkapi.ykk.com.tr/Authentication/Login"
    Private Const GetOrderUrl As String = "https://ykkapi.ykk.com.tr/api/Order/GetOrder"
    Private Const OrderUrl As String = "https://ykkapi.ykk.com.tr/api/Order/PostOrder"

    Dim cToken As String = ""
    Dim cIsemriNo As String = ""

    Public Sub Main()

        Try
            Dim cGetSet As String = "G"
            Dim args As String() = Environment.GetCommandLineArgs()

            If args.Length = 1 Then
                oConnection.cServer = "MONSTER\MSSQLSERVER2"
                oConnection.cDatabase = "TES"
                oConnection.cUser = "sa"
                oConnection.cPassword = "Hayabusa1024"
                cGetSet = "N1"
                cIsemriNo = "0000310154"
                oConnection.cPersonel = "VOLKAN POLATMAN"
                oConnection.cModelNo = "1031097%041%DANNY%BLACK%WIND25"
            Else
                oConnection.cServer = args(1).ToString.Trim
                oConnection.cDatabase = args(2).ToString.Trim
                oConnection.cUser = args(3).ToString.Trim
                oConnection.cPassword = args(4).ToString.Trim
                cGetSet = args(5).ToString.Trim
                cIsemriNo = args(6).ToString.Trim
                oConnection.cPersonel = args(7).ToString.Trim
                oConnection.cModelNo = args(8).ToString.Trim
            End If

            oConnection.cPersonel = Replace(oConnection.cPersonel, "%", " ")
            oConnection.cModelNo = Replace(oConnection.cModelNo, "%", " ")

            oConnection.cConnStr = "Data Source=" + oConnection.cServer + ";" +
                                   "Initial Catalog=" + oConnection.cDatabase + ";" +
                                   "uid=" + oConnection.cUser + ";" +
                                   "pwd=" + oConnection.cPassword + ""
            GetYKKParameters()

            Select Case cGetSet
                Case "G"
                    GetIsemriDurum2(cIsemriNo)
                Case "S"
                    SendIsemri2(cIsemriNo)
                Case "N1"
                    Dim result = utilNext.CreateRiskAnalysisAsync().GetAwaiter().GetResult()
                    NumuneGonderildi(result.Id, result.Message)
            End Select


        Catch ex As Exception
            ErrDisp("Main: " & ex.Message, "YKKModule",,, ex)
        End Try
    End Sub

    Private Sub NumuneGonderildi(cId As String, cMessage As String)

        Try
            Dim oSQL As New SQLServerClass
            Dim cOnModelNo As String = ""

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select top 1 onmodelno " +
                        " from onmodel with (NOLOCK) " +
                        " where modelno = '" + oConnection.cModelNo + "' "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                cOnModelNo = oSQL.SQLReadString("onmodelno")
            End If
            oSQL.oReader.Close()

            If cOnModelNo = "" Then
                MsgBox("Numune model no onmodel tablosunda bulunamadi : " + oConnection.cModelNo)
                oSQL.CloseConn()
                Exit Sub
            End If

            oSQL.cSQLQuery = "update onmodel3 set " +
                        " risktarih = getdate(), " +
                        " riskpersonel = '" + SQLWriteString(oConnection.cPersonel, 50) + "', " +
                        " riskcevapid = '" + SQLWriteString(cId, 50) + "', " +
                        " riskcevap = '" + SQLWriteString(cMessage, 50) + "' " +
                        " where onmodelno = '" + cOnModelNo + "' "
            oSQL.SQLExecute()

            Console.WriteLine("Numune risk analizi guncellendi : " + oConnection.cModelNo + " Id : " + cId + " Message : " + cMessage)

            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp("NumuneGonderildi: " & ex.Message, "YKKModule",,, ex)
        End Try
    End Sub

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

            Dim token = GetTokenAsync().Result

            If String.IsNullOrEmpty(token) Then
                CreateLog(, "Login başarısız")
                Console.WriteLine("Token ALINAMADI")
                Exit Function
            End If

            oSQL.OpenConn()

            Console.WriteLine("Token alındı: " & token)

            oSQL.cSQLQuery = "select top 1 ykksiparisno " +
                                " from isemri with (NOLOCK) " +
                                " where isemrino = '" + cIsemriNo + "' "
            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                cSiparisNo = oSQL.SQLReadString("ykksiparisno")
            Else
                oSQL.oReader.Close()
                oSQL.CloseConn()
                Console.WriteLine("Isemri bulunamadi : " + cIsemriNo)
                Exit Function
            End If
            oSQL.oReader.Close()

            If cSiparisNo.Trim = "" Then
                oSQL.CloseConn()
                Console.WriteLine("Isemrinde YKK Siparis no bos")
                Exit Function
            End If

            Console.WriteLine("Sorgulanan isemri : " + cIsemriNo.Trim + " YKK Siparis : " + cSiparisNo)

            Dim orderData = GetOrderDataAsync(token, cSiparisNo).Result

            If Not String.IsNullOrEmpty(orderData) Then

                Dim formattedJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(orderData), Formatting.Indented)
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

                    Console.WriteLine("Okunan isemri : " + cIsemriNo.Trim +
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

                            oSQL.cSQLQuery = "update isemrilines set " +
                                            " uretilen = " + SQLWriteDecimal(nMiktar) +
                                            " where isemrino = '" + cIsemriNo.Trim + "' " +
                                            " and stokno = '" + cStokNo.Trim + "' "
                            oSQL.SQLExecute()

                            ' termin2 YKK dan gelen ilk termin tarihi 
                            oSQL.cSQLQuery = "set dateformat dmy " +
                                            " update isemrilines set " +
                                            " termintarihi = '" + SQLWriteDate(dTermin) + "' " +
                                            " where isemrino = '" + cIsemriNo.Trim + "' " +
                                            " and stokno = '" + cStokNo.Trim + "' " +
                                            " and (termintarihi is null or termintarihi = '01.01.1950') "
                            oSQL.SQLExecute()

                            ' termin3 YKK dan gelen son termin tarihi
                            oSQL.cSQLQuery = "set dateformat dmy " +
                                            " update isemrilines set " +
                                            " termintarihi2 = '" + SQLWriteDate(dTermin) + "' " +
                                            " where isemrino = '" + cIsemriNo.Trim + "' " +
                                            " and stokno = '" + cStokNo.Trim + "' " +
                                            " and termintarihi is not null " +
                                            " and termintarihi <> '01.01.1950' " +
                                            " and termintarihi <> '" + SQLWriteDate(dTermin) + "' "
                            oSQL.SQLExecute()
                        End If

                        'Console.WriteLine($" - Product: {cStokNo}, Quantity: {nMiktar}, Lead Time: {dTermin}")
                        Console.WriteLine("Stok No : " + cStokNo +
                                          " Miktar : " + SQLWriteDecimal(nMiktar) +
                                          " Termin : " + SQLWriteDate(dTermin))
                    Next
                Next

                Console.WriteLine(vbNewLine & "Sipariş Verileri:")
                Console.WriteLine(formattedJson)

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

                    Console.WriteLine("Isemri kapatildi : " + cIsemriNo.Trim + " kapanıs tarihi : " + SQLWriteDate(dKapanis))
                End If

            Else
                Console.WriteLine("Sorgulaması başarısız isemri : " + cIsemriNo.Trim)
            End If

            oSQL.CloseConn()

            GetIsemriDurum2 = cResult.Trim

        Catch ex As Exception
            ErrDisp("GetIsemriDurum2: " & ex.Message, "YKKModule",,, ex)
        End Try
    End Function

    Public Function SendIsemri2(ByVal cIsEmriNo As String) As String

        SendIsemri2 = ""

        Try
            Dim oSQL As New SQLServerClass
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
            Dim cToken As String = ""

            Dim oOrder As New Order
            Dim oOrderList As New List(Of Order)
            Dim oItem As ApiOrderItemDetailResult
            Dim oItemList As New List(Of ApiOrderItemDetailResult)

            If cIsEmriNo.Trim = "" Then
                Exit Function
            End If

            cToken = GetTokenAsync().Result

            If String.IsNullOrEmpty(cToken) Then
                CreateLog(, "Login başarısız")
                Console.WriteLine("Token ALINAMADI")
                Exit Function
            End If

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
                " and a.isemrino = '" + cIsEmriNo.Trim + "' " +
                " and a.firma = '" + oConnection.cYKKFirma.Trim + "' "

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
                Console.WriteLine("Isemri bulunamadi")
                Exit Function
            End If
            oSQL.oReader.Close()

            If cYKKSiparisNo.Trim <> "" Then
                oSQL.CloseConn()
                Console.WriteLine("Isemri daha once YKK ya gonderilmis")
                Exit Function
            End If

            If cBuyerCode.Trim = "" Then
                cBuyerCode = cDefaultBuyerCode.Trim
            End If

            dTarih = #1/1/1950#

            oSQL.cSQLQuery = "select baslamatarihi, malzemetakipno " +
                            " from isemrilines with (NOLOCK) " +
                            " where isemrino = '" + cIsEmriNo.Trim + "' " +
                            " order by baslamatarihi "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                dTarih = oSQL.SQLReadDate("baslamatarihi")
                cMTF = oSQL.SQLReadString("malzemetakipno")
            End If
            oSQL.oReader.Close()

            If dTarih = #1/1/1950# Then
                dTarih = Now.Date
            End If

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

            cResult = SendOrderDataAsync(cToken, oOrder).Result

            If cResult.Trim = "" Then
                SendIsemri2 = ""
                Console.WriteLine("Isemri GONDERILEMEDI : " + cSasNo)
            Else

                cResult = Replace(cResult, Chr(34), "")

                oSQL.cSQLQuery = "update isemri set " +
                                " ykksiparisno = '" + cResult.Trim + "', " +
                                " ykkgonderim = getdate(), " +
                                " ykksondurumtarih = getdate(), " +
                                " ykksondurum = 'Gonderildi' " +
                                " where isemrino = '" + cIsEmriNo.Trim + "' "

                oSQL.SQLExecute()

                Console.WriteLine("Isemri gonderildi : " + cSasNo + " " + cResult.Trim)

                SendIsemri2 = cResult.Trim
            End If

            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp("SendIsemri2 : " + ex.Message, "YKKModule",,, ex)
        End Try
    End Function

    Private Async Function SendOrderDataAsync(cToken As String, oOrder As Order) As Task(Of String)
        Try
            SPMAyar()

            Using client As New HttpClient()

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " & cToken)
                'client.Timeout = TimeSpan.FromSeconds(30)

                Dim json = JsonConvert.SerializeObject(oOrder)
                Console.WriteLine("Gönderilen JSON:")
                Console.WriteLine(JsonConvert.SerializeObject(oOrder, Formatting.Indented))

                Dim content = New StringContent(json, Encoding.UTF8, "application/json")

                Dim response = Await client.PostAsync(OrderUrl, content).ConfigureAwait(False)
                Dim responseContent = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)

                If Not response.IsSuccessStatusCode Then
                    ErrDisp("Isemri gönderilirken hata oluştu. " +
                            " Durum Kodu: " + response.StatusCode.ToString +
                            " Hata Detayı: " + responseContent.ToString)
                    Return ""
                End If

                CreateLog(, "Basariyla sorgulanan isemri : " + oOrder.purchaseContractNo)
                Return responseContent
            End Using

        Catch ex As Exception
            ErrDisp("SendOrderDataAsync : " + ex.Message, "YKKModule",,, ex)
        End Try
    End Function

    Private Async Function GetTokenAsync() As Task(Of String)
        Try
            Using client As New HttpClient()

                'client.Timeout = TimeSpan.FromSeconds(30)

                Dim loginData = New With {
                    .username = oConnection.cYKKApiUserName.Trim,
                    .password = oConnection.cYKKApiPassword.Trim
                }

                Dim json = JsonConvert.SerializeObject(loginData)
                Dim content = New StringContent(json, Encoding.UTF8, "application/json")

                Dim response = Await client.PostAsync(LoginUrl, content).ConfigureAwait(False)
                'response.EnsureSuccessStatusCode()
                Return Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
            End Using

        Catch ex As Exception
            ErrDisp("GetTokenAsync token alınırken hata oluştu: " & ex.Message, "HTMain",,, ex)
            If ex.InnerException IsNot Nothing Then
                ErrDisp("GetTokenAsync detay: " & ex.InnerException.Message, "YKKModule",,, ex)
            End If
        End Try
        Return String.Empty
    End Function

    Private Async Function GetOrderDataAsync(token As String, cSiparisNo As String) As Task(Of String)
        Try
            SPMAyar()

            Using client As New HttpClient()

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " & token)
                'client.Timeout = TimeSpan.FromSeconds(30)

                Dim encodedContract = Uri.EscapeDataString(cSiparisNo)
                Dim url = GetOrderUrl & "?orderCodes=" & encodedContract

                Dim response = Await client.GetAsync(url).ConfigureAwait(False)
                'response.EnsureSuccessStatusCode()
                Return Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
            End Using

        Catch ex As Exception
            ErrDisp("GetOrderDataAsync sipariş verileri alınırken hata oluştu: " & ex.Message, "HTMain",,, ex)
            If ex.InnerException IsNot Nothing Then
                ErrDisp("GetOrderDataAsync detay: " & ex.InnerException.Message, "YKKModule",,, ex)
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

                'Dim expectedThumbprint As String = "684CD5721BD21B73D5FE722F870C605B3662583A"
                Dim expectedThumbprint As String = "2F471D3DC8ED45CC2F399316FD945138C3E4C784"

                Try
                    Console.WriteLine("Sertifika doğrulama başladı")
                    Dim cert As New X509Certificate2(certificate)
                    Dim receivedThumbprint As String = cert.Thumbprint.Replace(" ", "").ToUpper()
                    Dim expectedThumbprintClean As String = expectedThumbprint.Replace(" ", "").ToUpper()

                    Console.WriteLine("Sertifika Detayları:")
                    Console.WriteLine($"Subject: {certificate.Subject}")
                    Console.WriteLine($"Issuer: {certificate.Issuer}")
                    Console.WriteLine($"Alınan Thumbprint: {receivedThumbprint}")
                    Console.WriteLine($"Beklenen Thumbprint: {expectedThumbprintClean}")

                    If receivedThumbprint = expectedThumbprintClean Then
                        Console.WriteLine("Sertifika doğrulama başarılı")
                        Return True
                    Else
                        Console.WriteLine($"Sertifika doğrulama hatası:")
                        Console.WriteLine($"Beklenen: {expectedThumbprintClean}")
                        Console.WriteLine($"Alınan: {receivedThumbprint}")
                        Return False
                    End If

                Catch ex As Exception
                    Console.WriteLine("Sertifika doğrulama hatası: " & ex.Message)
                    Return False
                End Try
            End Function

        Catch ex As Exception
            ErrDisp("SPMAyar", "YKKModule",,, ex)
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
        Public Property noCommercial As String
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
