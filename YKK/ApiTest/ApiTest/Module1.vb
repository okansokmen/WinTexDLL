Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json
Imports System.Net
Imports System.Net.Security
Imports System.Net.ServicePointManager
Imports System.Security.Cryptography.X509Certificates

Module Module1
    ' en son çalışan kod
    ' https://ykkapi.ykk.com.tr/swagger/index.html
    Private Const LoginUrl As String = "https://ykkapi.ykk.com.tr/Authentication/Login"
    Private Const GetOrderUrl As String = "https://ykkapi.ykk.com.tr/api/Order/GetOrder"
    Private Const OrderUrl As String = "https://ykkapi.ykk.com.tr/api/Order/PostOrder"

    Dim cToken As String = ""

    Sub Main2()
        Try

            cToken = GetTokenAsync().Result

            If Not String.IsNullOrEmpty(cToken) Then
                Console.WriteLine("Token alındı: " & cToken)

                Console.WriteLine(vbNewLine & "Sipariş durumu sorgulamak istiyor musunuz? (E/H)")
                Dim queryAnswer = Console.ReadKey()
                Console.WriteLine()


                If queryAnswer.Key = ConsoleKey.E Then
                    Dim orderData = GetOrderDataAsync().Result
                    If Not String.IsNullOrEmpty(orderData) Then
                        Dim formattedJson = JsonConvert.SerializeObject(
                            JsonConvert.DeserializeObject(orderData), Formatting.Indented)
                        Console.WriteLine(vbNewLine & "Sipariş Verileri:")
                        Console.WriteLine(formattedJson)
                    End If
                End If

                Console.WriteLine(vbNewLine & "Sipariş göndermek istiyor musunuz? (E/H)")
                Dim answer = Console.ReadKey()
                Console.WriteLine()

                If answer.Key = ConsoleKey.E Then
                    Dim result = SendOrderAsync(cToken).Result
                    Console.WriteLine(result)
                End If
            End If

        Catch ex As Exception
            Console.WriteLine("Hata oluştu: " & ex.Message)
        End Try

        Console.WriteLine(vbNewLine & "Çıkmak için bir tuşa basın...")
        Console.ReadKey()
    End Sub

    Private Async Function GetTokenAsync() As Task(Of String)
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Using client As New HttpClient()
                client.Timeout = TimeSpan.FromSeconds(30)

                Dim loginData = New With {
                    .username = "ykk@eroglugiyim.com",
                    .password = "€r0Glu2019!"
                }

                Dim json = JsonConvert.SerializeObject(loginData)
                Dim content = New StringContent(json, Encoding.UTF8, "application/json")

                Dim response = Await client.PostAsync(LoginUrl, content)
                response.EnsureSuccessStatusCode()
                Return Await response.Content.ReadAsStringAsync()
            End Using

        Catch ex As Exception
            Console.WriteLine("Token alınırken hata oluştu: " & ex.Message)
            If ex.InnerException IsNot Nothing Then
                Console.WriteLine("Detay: " & ex.InnerException.Message)
            End If
        End Try
        Return String.Empty
    End Function


    Private Async Function GetOrderDataAsync() As Task(Of String)
        Try
            SPMAyar()

            Using client As New HttpClient()
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " & cToken)
                client.Timeout = TimeSpan.FromSeconds(30)

                Dim encodedContract = Uri.EscapeDataString("W25-PS26 PROTO REQUEST EROGLU")
                Dim url = GetOrderUrl & "?purchaseContractNos=" & encodedContract

                'Dim requestUri As New Uri(url)
                'Dim servicePoint = ServicePointManager.FindServicePoint(requestUri)
                'servicePoint.Expect100Continue = False

                Dim response = Await client.GetAsync(url)
                response.EnsureSuccessStatusCode()
                Return Await response.Content.ReadAsStringAsync()
            End Using

        Catch ex As Exception
            Console.WriteLine("Sipariş verileri alınırken hata oluştu: " & ex.Message)
            If ex.InnerException IsNot Nothing Then
                Console.WriteLine("Detay: " & ex.InnerException.Message)
            End If
        End Try
        Return String.Empty
    End Function

    Private Async Function SendOrderAsync(token As String) As Task(Of String)
        Try
            SPMAyar()

            Using client As New HttpClient()
                'client.DefaultRequestHeaders.Authorization = New Headers.AuthenticationHeaderValue("Bearer", token)
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " & cToken)
                client.Timeout = TimeSpan.FromSeconds(30)

                Dim orderData = New With {
                    .DeliverCode1 = "007810",
                    .DeliverCode2 = "000",
                    .BuyerCode = "000135",
                    .PURCHASECONTRACTNO = "W25-PS26 PROTO Test",
                    .AssociatedMail = "",
                    .TimeConvention = 2,
                    .RequestDate = DateTime.Now.ToString("yyyy-MM-dd"),
                    .SampleOrder = False,
                    .NoCommercial = False,
                    .CompleteDelivery = True,
                    .OrderComment = "",
                    .APIOrderItems = New List(Of Object) From {
                        New With {
                            .ProductCode = "4920894",
                            .CustomersProductCode = "",
                            .Count = 1,
                            .COLOR = "",
                            .Length = "",
                            .Description = ""
                        },
                        New With {
                            .ProductCode = "4920846",
                            .CustomersProductCode = "",
                            .Count = 1,
                            .COLOR = "S0466",
                            .Length = "",
                            .Description = ""
                        }
                    }
                }

                Dim json = JsonConvert.SerializeObject(orderData)
                Console.WriteLine("Gönderilen JSON:")
                Console.WriteLine(JsonConvert.SerializeObject(orderData, Formatting.Indented))

                Dim content = New StringContent(json, Encoding.UTF8, "application/json")

                Dim response = Await client.PostAsync(OrderUrl, content)
                Dim responseContent = Await response.Content.ReadAsStringAsync()

                If Not response.IsSuccessStatusCode Then
                    Return $"Sipariş gönderilirken hata oluştu. Durum Kodu: {response.StatusCode}" & vbNewLine &
                           $"Hata Detayı: {responseContent}"
                End If

                Return "Sipariş başarıyla gönderildi. Yanıt: " & responseContent
            End Using

        Catch ex As Exception
            Return "Sipariş gönderilirken hata oluştu: " & ex.Message
        End Try
    End Function

    Private Sub SPMAyar()
        Try
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 Or SecurityProtocolType.Ssl3  ' DirectCast(3072, System.Net.SecurityProtocolType)

            ServicePointManager.ServerCertificateValidationCallback =
            Function(sender As Object, certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors)

                If certificate Is Nothing Then
                    Return False
                End If

                Dim expectedThumbprint As String = "684CD5721BD21B73D5FE722F870C605B3662583A"

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
            Console.WriteLine("SPMAyar : " & ex.Message)
        End Try
    End Sub

End Module