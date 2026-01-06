Imports System.Net
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Security
Imports System.Net.Http
Imports System.Text
Imports System.Net.ServicePointManager
Imports Newtonsoft.Json

<ComClass(HTMain.ClassId, HTMain.InterfaceId, HTMain.EventsId)>
Public Class HTMain

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "7941c201-b737-4a97-a657-2982f0f752be"
    Public Const InterfaceId As String = "69bb92d8-67cd-4cf3-ad94-8c0c7365c9de"
    Public Const EventsId As String = "6b7f208a-66e2-49a4-a8f3-324cdc6ca610"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub

    ' https://ykkapi.ykk.com.tr/swagger/index.html

    Private Const LoginUrl As String = "https://ykkapi.ykk.com.tr/Authentication/Login"
    Private Const GetOrderUrl As String = "https://ykkapi.ykk.com.tr/api/Order/GetOrder"
    Private Const OrderUrl As String = "https://ykkapi.ykk.com.tr/api/Order/PostOrder"

    Public Shared cWinTexYKKVersion As String = My.Application.Info.Version.ToString.Trim

    Public Shared Function TestWinTexYKK() As String

        TestWinTexYKK = "WinTexYKK ÇALIŞMIYOR"

        Try
            Return "WinTexYKK Çalışıyor"
        Catch ex As Exception
            ErrDisp("TestWinTexDLL : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Sub YKKSysPar()
        Try
            Dim ofrmSysPar As New frmSysPar
            ofrmSysPar.ShowDialog()

        Catch ex As Exception
            ErrDisp("YKKSysPar : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Function init(Optional cServer As String = "", Optional cDatabase As String = "", Optional cUser As String = "sa", Optional cPassword As String = "") As Boolean
        ' init database connection
        Dim cSQL As String = ""

        init = False

        Try
            oConnection.cServer = cServer.Trim
            oConnection.cDatabase = cDatabase.Trim
            oConnection.cUser = cUser.Trim
            oConnection.cPassword = cPassword.Trim

            oConnection.cConnStr = "Data Source=" + oConnection.cServer + ";" +
                                    "Initial Catalog=" + oConnection.cDatabase + ";" +
                                    "uid=" + oConnection.cUser + ";" +
                                    "pwd=" + oConnection.cPassword + ""

            GetYKKParameters()

            init = True

        Catch ex As Exception
            init = False
            ErrDisp("SQL Connect : " + ex.Message + " " + cServer.Trim + " " + cDatabase.Trim + " " + cUser.Trim + " " + cPassword.Trim,,,, ex)
        End Try
    End Function

    ' yeni kodlar
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
                Exit Function
            End If

            oSQL.OpenConn()

            'Console.WriteLine("Token alındı: " & token)

            oSQL.cSQLQuery = "select top 1 ykksiparisno " +
                                " from isemri with (NOLOCK) " +
                                " where isemrino = '" + cIsemriNo + "' "
            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                cSiparisNo = oSQL.SQLReadString("ykksiparisno")
            Else
                oSQL.oReader.Close()
                oSQL.CloseConn()
                Exit Function
            End If
            oSQL.oReader.Close()

            If cSiparisNo.Trim = "" Then
                oSQL.CloseConn()
                Exit Function
            End If

            If cResult.Trim <> "" Then
                CreateLog(, "Sorgulanan isemri : " + cIsemriNo.Trim + " YKK Siparis : " + cSiparisNo)
            End If

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

                    If cResult.Trim <> "" Then
                        CreateLog(, "Okunan isemri : " + cIsemriNo.Trim + " YKK Siparis : " + cSiparisNo + " Sonuc : " + cResult.Trim + " Kapanis : " + SQLWriteDate(dKapanis))
                    End If

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
                                            " and termintarihi <> '01.01.1950' "
                            oSQL.SQLExecute()

                        End If

                        'Console.WriteLine($" - Product: {cStokNo}, Quantity: {nMiktar}, Lead Time: {dTermin}")
                        CreateLog(, "Stok No : " + cStokNo + " Miktar : " + SQLWriteDecimal(nMiktar) + " Termin : " + SQLWriteDate(dTermin))
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
                End If

            Else
                CreateLog(, "Sorgulaması başarısız isemri : " + cIsemriNo.Trim)
            End If

            oSQL.CloseConn()

            GetIsemriDurum2 = cResult.Trim

        Catch ex As Exception
            ErrDisp("GetIsemriDurum2: " & ex.Message, "HTMain",,, ex)
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
                " and a.firma = '" + oConnection.cYKKFirma.Trim + "' " +
                " and (a.ykksiparisno is null or a.ykksiparisno = '') "

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
                Exit Function
            End If
            oSQL.oReader.Close()

            If cYKKSiparisNo.Trim <> "" Then
                oSQL.CloseConn()
                Exit Function
            End If

            If cBuyerCode.Trim = "" Then
                cBuyerCode = cDefaultBuyerCode.Trim
            End If

            ' YYK ya termin1 (baslamatarihi) gönderiliyor

            dTarih = #1/1/1950#

            oSQL.cSQLQuery = "select top 1 baslamatarihi, malzemetakipno " +
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
            Else

                cResult = Replace(cResult, Chr(34), "")

                oSQL.cSQLQuery = "update isemri set " +
                                " ykksiparisno = '" + cResult.Trim + "', " +
                                " ykkgonderim = getdate(), " +
                                " ykksondurumtarih = getdate(), " +
                                " ykksondurum = 'Gonderildi' " +
                                " where isemrino = '" + cIsEmriNo.Trim + "' "

                oSQL.SQLExecute()

                SendIsemri2 = cResult.Trim
            End If

            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp("SendIsemri2 : " + ex.Message, "HTMain",,, ex)
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
            ErrDisp("SendOrderDataAsync : " + ex.Message, "HTMain",,, ex)
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
                ErrDisp("GetTokenAsync detay: " & ex.InnerException.Message, "HTMain",,, ex)
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
                ErrDisp("GetOrderDataAsync detay: " & ex.InnerException.Message, "HTMain",,, ex)
            End If
        End Try
        Return String.Empty
    End Function

    ' eski kodlar

    'Public Function GetIsemriDurum(ByVal cIsemriNo As String) As String

    '    Dim oSQL As New SQLServerClass
    '    Dim api As New YkkWebApi()
    '    Dim oSiparisler As New List(Of String)()
    '    Dim oSiparisDetay As List(Of APIOrderDetailResult)
    '    Dim cOrderno As String = ""
    '    Dim cSQL As String
    '    Dim cResult As String = ""
    '    Dim nCnt As Integer = 0
    '    Dim cStokNo As String = ""
    '    Dim dTermin As Date = #1/1/1950#
    '    Dim dKapanis As Date = #1/1/1950#
    '    Dim nMiktar As Double = 0

    '    GetIsemriDurum = ""

    '    Try
    '        SPMAyar()

    '        oSQL.OpenConn()

    '        cSQL = "select top 1 ykksiparisno " +
    '                " from isemri with (NOLOCK) " +
    '                " where isemrino = '" + cIsemriNo + "' "

    '        oSQL.GetSQLReader(cSQL)

    '        If oSQL.oReader.Read Then
    '            cOrderno = oSQL.SQLReadString("ykksiparisno")
    '        Else
    '            oSQL.oReader.Close()
    '            oSQL.CloseConn()
    '            Exit Function
    '        End If
    '        oSQL.oReader.Close()

    '        If cOrderno.Trim = "" Then
    '            oSQL.CloseConn()
    '            Exit Function
    '        End If

    '        oSiparisler.Add(cOrderno)
    '        oSiparisDetay = api.GetOrder(oSiparisler, Nothing, Nothing, Nothing, Nothing)
    '        cResult = oSiparisDetay(0).OrderStatus

    '        If oSiparisDetay(0).orderCompleteDate IsNot Nothing Then
    '            dKapanis = oSiparisDetay(0).orderCompleteDate.Value.Date
    '        End If

    '        For nCnt = 0 To oSiparisDetay(0).ApiOrderItemsDetailResults.Count - 1

    '            dTermin = #1/1/1950#
    '            nMiktar = 0
    '            cStokNo = ""

    '            If oSiparisDetay(0).ApiOrderItemsDetailResults(nCnt).Salesquantity IsNot Nothing Then
    '                If IsNumeric(oSiparisDetay(0).ApiOrderItemsDetailResults(nCnt).Salesquantity) Then
    '                    nMiktar = oSiparisDetay(0).ApiOrderItemsDetailResults(nCnt).Salesquantity
    '                End If
    '            End If

    '            If oSiparisDetay(0).ApiOrderItemsDetailResults(nCnt).customerProductCode IsNot Nothing Then
    '                cStokNo = oSiparisDetay(0).ApiOrderItemsDetailResults(nCnt).customerProductCode
    '            End If

    '            If oSiparisDetay(0).ApiOrderItemsDetailResults(nCnt).leadTimeDate IsNot Nothing Then
    '                dTermin = oSiparisDetay(0).ApiOrderItemsDetailResults(nCnt).leadTimeDate.Value.Date
    '            End If

    '            If cStokNo.Trim <> "" Then
    '                cSQL = "set dateformat dmy " +
    '                        " update isemrilines set " +
    '                        " termintarihi = '" + SQLWriteDate(dTermin) + "', " +
    '                        " uretilen = " + SQLWriteDecimal(nMiktar) +
    '                        " where isemrino = '" + cIsemriNo.Trim + "' " +
    '                        " and stokno = '" + cStokNo.Trim + "' "

    '                oSQL.SQLExecute(cSQL)
    '            End If
    '        Next

    '        cSQL = "update isemri set " +
    '                " ykksondurumtarih = getdate(), " +
    '                " ykksondurum = '" + SQLWriteString(cResult, 50) + "' " +
    '                " where isemrino = '" + cIsemriNo.Trim + "' "

    '        oSQL.SQLExecute(cSQL)

    '        If dKapanis <> #1/1/1950# Then

    '            cSQL = "set dateformat dmy " +
    '                    " update isemri set " +
    '                    " kilitle = 'E', " +
    '                    " isemriok = 'E', " +
    '                    " oktarihi = '" + SQLWriteDate(dKapanis) + "' " +
    '                    " where isemrino = '" + cIsemriNo.Trim + "' "

    '            oSQL.SQLExecute(cSQL)

    '            cSQL = "update isemrilines set " +
    '                    " kapandi = 'E' " +
    '                    " where isemrino = '" + cIsemriNo.Trim + "' "

    '            oSQL.SQLExecute(cSQL)
    '        End If

    '        oSQL.CloseConn()

    '        GetIsemriDurum = cResult.Trim

    '    Catch yex As YkkClientException
    '        ErrDisp("Sipariş sorgulama YKK hatası : " + yex.StatusCode + " " + yex.Response, "SiparisSorgula",,, yex)
    '    Catch ex As Exception
    '        ErrDisp("Sipariş sorgulama teknik hata", "SiparisSorgula",,, ex)
    '    End Try
    'End Function

    'Public Function SendIsemri(ByVal cIsEmriNo As String) As String

    '    Dim oSQL As New SQLServerClass
    '    Dim cSQL As String = ""
    '    Dim cBuyerCode As String = ""
    '    Dim cDeliveryCode1 As String = ""
    '    Dim cDeliveryCode2 As String = ""
    '    Dim cSasNo As String = ""
    '    Dim cResult As String = ""
    '    Dim dTarih As Date = #1/1/1950#
    '    Dim cNotlar As String = ""
    '    Dim cDurum As String = ""
    '    Dim cYKKSiparisNo As String = ""
    '    Dim cMail As String = ""
    '    Dim cMTF As String = ""

    '    Dim api As New YkkWebApi()
    '    Dim oItemList As New List(Of APIOrderItems)()
    '    Dim oItem As APIOrderItems
    '    Dim oSiparisler As New List(Of String)()
    '    Dim oSiparisDetay As List(Of APIOrderDetailResult)
    '    Dim cDefaultBuyerCode As String = ""

    '    SendIsemri = ""

    '    Try
    '        SPMAyar()

    '        If cIsEmriNo.Trim = "" Then Exit Function

    '        cDefaultBuyerCode = GetSysPar("ykkdefaultbuyer")

    '        oSQL.OpenConn()

    '        cSQL = "select a.teslimyeri, a.tarih, a.notlar, a.ykksiparisno, b.email, "

    '        cSQL = cSQL +
    '            " ykkbuyercode = (select top 1 x.ykkbuyer " +
    '                            " from firma x with (NOLOCK) , isemrilines y with (NOLOCK) , siparis z with (NOLOCK) , sipmodel q with (NOLOCK) " +
    '                            " where y.malzemetakipno = q.malzemetakipno " +
    '                            " and q.siparisno = z.kullanicisipno " +
    '                            " and z.musterino = x.firma " +
    '                            " and y.isemrino = a.isemrino " +
    '                            " and x.ykkbuyer is not null " +
    '                            " and x.ykkbuyer <> ''), "
    '        cSQL = cSQL +
    '            " deliverycode1 = (select top 1 kod1 " +
    '                            " from isemriteslimyeri with (NOLOCK) " +
    '                            " where teslimyeri = a.teslimyeri), "
    '        cSQL = cSQL +
    '            " deliverycode2 = (select top 1 kod2 " +
    '                            " from isemriteslimyeri with (NOLOCK) " +
    '                            " where teslimyeri = a.teslimyeri) "
    '        cSQL = cSQL +
    '            " from isemri a with (NOLOCK) , personel b with (NOLOCK) " +
    '            " where a.takipelemani = b.personel " +
    '            " And a.isemrino = '" + cIsEmriNo.Trim + "' " +
    '            " and a.firma = '" + oConnection.cYKKFirma.Trim + "' " +
    '            " and (a.ykksiparisno is null or a.ykksiparisno = '') "

    '        oSQL.GetSQLReader(cSQL)

    '        If oSQL.oReader.Read Then
    '            cYKKSiparisNo = oSQL.SQLReadString("ykksiparisno")
    '            cBuyerCode = oSQL.SQLReadString("ykkbuyercode")
    '            cDeliveryCode1 = oSQL.SQLReadString("deliverycode1")
    '            cDeliveryCode2 = oSQL.SQLReadString("deliverycode2")
    '            cNotlar = oSQL.SQLReadString("notlar")
    '            cMail = oSQL.SQLReadString("email")
    '        Else
    '            oSQL.oReader.Close()
    '            oSQL.CloseConn()
    '            Exit Function
    '        End If
    '        oSQL.oReader.Close()

    '        If cYKKSiparisNo.Trim <> "" Then
    '            oSQL.CloseConn()
    '            Exit Function
    '        End If

    '        If cBuyerCode.Trim = "" Then
    '            cBuyerCode = cDefaultBuyerCode.Trim
    '        End If

    '        cSQL = "select termintarihi, malzemetakipno " +
    '                " from isemrilines with (NOLOCK) " +
    '                " where isemrino = '" + cIsEmriNo.Trim + "' " +
    '                " and termintarihi is not null " +
    '                " and termintarihi <> '01.01.1950' " +
    '                " order by termintarihi "

    '        oSQL.GetSQLReader(cSQL)

    '        If oSQL.oReader.Read Then
    '            dTarih = oSQL.SQLReadDate("termintarihi")
    '            cMTF = oSQL.SQLReadString("malzemetakipno")
    '        End If
    '        oSQL.oReader.Close()

    '        cSQL = "select a.stokno, a.renk, a.beden, b.birim1, " +
    '                " miktar = sum(coalesce(a.miktar1,0)) " +
    '                " from isemrilines a with (NOLOCK) , stok b with (NOLOCK) " +
    '                " where a.stokno = b.stokno " +
    '                " and a.isemrino = '" + cIsEmriNo.Trim + "' " +
    '                " group by a.stokno, a.renk, a.beden, b.birim1 " +
    '                " order by a.stokno, a.renk, a.beden, b.birim1 "

    '        oSQL.GetSQLReader(cSQL)

    '        Do While oSQL.oReader.Read
    '            oItem = api.CreateApiOrderItem(String.Empty, oSQL.SQLReadString("stokno"), String.Empty, String.Empty, CInt(oSQL.SQLReadDouble("miktar")))
    '            oItemList.Add(oItem)
    '        Loop
    '        oSQL.oReader.Close()

    '        If cMTF.Trim = "" Then
    '            cSasNo = cIsEmriNo
    '        Else
    '            cSasNo = cIsEmriNo + "-" + cMTF
    '        End If

    '        cResult = api.SendOrder(cSasNo, dTarih, APIOrderTimeConvention._2, False, cBuyerCode, True, cDeliveryCode1, cDeliveryCode2, cNotlar, False, cMail, oItemList)

    '        If IsNothing(cResult) Then
    '            SendIsemri = ""
    '        Else
    '            oSiparisler.Add(cResult.Trim)
    '            oSiparisDetay = api.GetOrder(oSiparisler, Nothing, Nothing, Nothing, Nothing)
    '            cDurum = oSiparisDetay.Item(0).OrderStatus

    '            cSQL = "update isemri set " +
    '                    " ykksiparisno = '" + cResult.Trim + "', " +
    '                    " ykkgonderim = getdate(), " +
    '                    " ykksondurumtarih = getdate(), " +
    '                    " ykksondurum = '" + SQLWriteString(cDurum, 50) + "' " +
    '                    " where isemrino = '" + cIsEmriNo.Trim + "' "

    '            oSQL.SQLExecute(cSQL)

    '            SendIsemri = cResult.Trim
    '        End If

    '        oSQL.CloseConn()

    '    Catch ex As Exception
    '        ErrDisp("SendIsemri : " + ex.Message, "HTMain",,, ex)
    '    End Try
    'End Function

    'Public Sub GetAddresses()
    '    Try
    '        SPMAyar()

    '        Dim api As New YkkWebApi()
    '        Console.WriteLine("> Adresler sorgulanıyor...")

    '        Dim addresses As List(Of APICustomerAdress) = api.GetDeliveryAddress(200, 1)
    '        addresses.ForEach(Sub(a)
    '                              Console.WriteLine("> {0}-{1}-{2}", a.DeliveryCode1, a.DeliveryCode2, a.DeliveryAdress)
    '                          End Sub)
    '        Console.WriteLine("> Address sorgulama tamamlandı....")
    '    Catch yex As YkkClientException
    '        Console.WriteLine("> Status :{0}", yex.StatusCode)
    '        Console.WriteLine("> Response :{0}", yex.Response)
    '    Catch ex As Exception
    '        Console.WriteLine("> exception :{0}", ex.Message)
    '    End Try
    'End Sub

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
            ErrDisp("SPMAyar", "htmain.vb",,, ex)
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

End Class


