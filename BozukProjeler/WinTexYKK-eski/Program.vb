Imports System
Imports System.Linq

Friend Class Program
    '    Shared Sub Main2(ByVal args() As String)


    '        Gonderim_1();//DESC:Eşleşme yapılmış ürün için customer kod ile gönderilmesi (lenght var, color var) WO00000022

    '        Gonderim_2(); //DESC:Müşteri kodu ile  uzunluğu olan +  uzunluğu olmayan item için sipariş açma "WO00000023"

    '        Gonderim_2_Hatali(); //DESC: Yanlış Adres kodu ile Sipariş Geçilmesi

    '        Gonderim_3(); //DESC: item1 customercodlu lengthsiz + item2 Müşteri kodlu lenghtli WO00000025

    '        Gonderim_3_Hatali(); //DESC:Yanlış Uzunluk Bilgileri ile sipariş denemesi

    '        Gonderim_4(); //DESC: buyercode ile sipariş gönderme WO00000027

    '        Gonderim_4_Hatali();//DESC:Yanlış buyercode ile sipariş gönderme

    '        Gonderim_5(); //DESC: Quantity kodu M ve P olan 2 ürün için sadece ürün kodu, uzunluk ve renk bilgileri ile sipariş denemesi WO00000029

    '        Gonderim_5_hatali();  //DESC: Quantity kodu M ve P olan 2 ürün için sadece ürün kodu ile hatalı sipariş denemesi

    '        Gonderim_6(); //DESC: Sadece Eşleştirme Kodu ile Sipariş denemesi WO00000028

    '        Gonderim_6_Hatali(); // Uzunluk bilgisi gerekmeyen ürün için uzunluk bilgisi gönderilmesi

    '        SiparisSorgula("WO00000020"); //DESC: Siparişin sorgulanması

    '        GetAddresses() 'Adreslerin alınması

    '        GetProducts() 'Ürünlerin alınması

    '        Console.ReadLine()

    '    End Sub


    '    Private Shared Sub SiparisSorgula(ByVal orderno As String)
    '        Try
    '            Dim api As New YkkWebApi()
    '            'Sipariş sorgulama
    '            Console.WriteLine("> Sipariş Sorgulama işlemi başlıyor.... Web Order No : {0}", orderno)
    '            Dim orderNos As New List(Of String)()
    '            orderNos.Add(orderno)

    '            Dim orderDetail As List(Of APIOrderDetailResult) = api.GetOrder(orderNos, Nothing, Nothing, Nothing, Nothing)

    '            Console.WriteLine("> Sipariş Sorgulama Web Order No : {0}, Sipariş Durumu :", orderno, orderDetail(0).OrderStatus)
    '        Catch yex As YkkClientException
    '            Console.WriteLine("> Status :{0}", yex.StatusCode)
    '            Console.WriteLine("> Response :{0}", yex.Response)
    '        Catch ex As Exception
    '            Console.WriteLine("> exception :{0}", ex.Message)
    '        End Try
    '    End Sub

    '#Region "Test Senaryoları"

    '    ''' <summary>
    '    ''' Eşleşme yapılmış ürün için customer kod ile gönderilmesi (lenght var, color var)
    '    ''' </summary>
    '    Private Shared Sub Gonderim_1()
    '        Dim api As New YkkWebApi()

    '        Dim itemList As New List(Of APIOrderItems)()

    '        Dim item1 As APIOrderItems = api.CreateApiOrderItem("0089294", "1040002733", "580", "7", 100)
    '        Dim item2 As APIOrderItems = api.CreateApiOrderItem("0089294", "1040002734", "580", "8", 110)
    '        Dim item3 As APIOrderItems = api.CreateApiOrderItem("0089294", "1040002735", "580", "9", 50)
    '        Dim item4 As APIOrderItems = api.CreateApiOrderItem("0852495", "1040003367", "196", "6", 51)
    '        Dim deliveryCode1 = "003497"
    '        Dim deliveryCode2 = "001"
    '        Dim buyercode = String.Empty


    '        itemList.Add(item1)
    '        itemList.Add(item2)
    '        itemList.Add(item3)
    '        itemList.Add(item4)


    '        Dim sasNo As String = "ORN00" & Date.Now.Year.ToString() & Convert.ToInt32(Date.Now.TimeOfDay.TotalMinutes)

    '        Dim siparisNo As String = String.Empty
    '        Try
    '            'Sipariş Gönderme

    '            siparisNo = api.SendOrder(sasNo, Date.Now.AddDays(3), APIOrderTimeConvention._2, False, buyercode, True, deliveryCode1, deliveryCode2, $"Örnek sipariş açıklaması {sasNo}", True, itemList)


    '            Console.WriteLine("> Gönderim Başarılı.")
    '            Console.WriteLine("> Gönderim Başarılı. Web Order No : {0}", siparisNo)
    '            'Sipariş sorgulama
    '            Console.WriteLine("> Sipariş Sorgulama işlemi başlıyor.... Web Order No : {0}", siparisNo)
    '            Dim orderNos As New List(Of String)()
    '            orderNos.Add(siparisNo)

    '            Dim orderDetail As List(Of APIOrderDetailResult) = api.GetOrder(orderNos, Nothing, Nothing, Nothing, Nothing)

    '            Console.WriteLine("> Sipariş Sorgulama Web Order No : {0}, Sipariş Durumu : {1}", siparisNo, orderDetail(0).OrderStatus)

    '        Catch yex As YkkClientException
    '            Console.WriteLine("> Status :{0}", yex.StatusCode)
    '            Console.WriteLine("> Response :{0}", yex.Response)
    '        Catch ex As Exception
    '            Console.WriteLine("> Hata : {0}", ex.Message)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    '''Müşteri kodu ile  uzunluğu olan +  uzunluğu olmayan item için sipariş açma
    '    '//// </summary>
    '    Private Shared Sub Gonderim_2()
    '        Dim api As New YkkWebApi()

    '        Dim itemList As New List(Of APIOrderItems)()

    '        Dim item1 As APIOrderItems = api.CreateApiOrderItem("4164156", String.Empty, "580", String.Empty, 100)
    '        Dim item2 As APIOrderItems = api.CreateApiOrderItem("4449247", String.Empty, "580", "12.5", 110)
    '        Dim deliveryCode1 = "003497"
    '        Dim deliveryCode2 = "001"
    '        Dim buyercode = String.Empty


    '        itemList.Add(item1)
    '        itemList.Add(item2)


    '        Dim sasNo As String = "ORN00" & Date.Now.Year.ToString() & Convert.ToInt32(Date.Now.TimeOfDay.TotalMinutes)

    '        Dim siparisNo As String = String.Empty
    '        Try
    '            'Sipariş Gönderme

    '            siparisNo = api.SendOrder(sasNo, Date.Now.AddDays(3), APIOrderTimeConvention._2, False, buyercode, True, deliveryCode1, deliveryCode2, $"Örnek sipariş açıklaması {sasNo}", True, itemList)


    '            Console.WriteLine("> Gönderim Başarılı.")
    '            Console.WriteLine("> Gönderim Başarılı. Web Order No : {0}", siparisNo)
    '            'Sipariş sorgulama
    '            Console.WriteLine("> Sipariş Sorgulama işlemi başlıyor.... Web Order No : {0}", siparisNo)
    '            Dim orderNos As New List(Of String)()
    '            orderNos.Add(siparisNo)

    '            Dim orderDetail As List(Of APIOrderDetailResult) = api.GetOrder(orderNos, Nothing, Nothing, Nothing, Nothing)

    '            Console.WriteLine("> Sipariş Sorgulama Web Order No : {0}, Sipariş Durumu : {1}", siparisNo, orderDetail(0).OrderStatus)

    '        Catch yex As YkkClientException
    '            Console.WriteLine("> Status :{0}", yex.StatusCode)
    '            Console.WriteLine("> Response :{0}", yex.Response)
    '        Catch ex As Exception
    '            Console.WriteLine("> Hata : {0}", ex.Message)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    '''Yanlış Adres kodu ile Sipariş Geçilmesi
    '    '''Şirketinize ait  bir adres bilgisi bulunamadı hatası
    '    '//// </summary>
    '    Private Shared Sub Gonderim_2_Hatali()
    '        Dim api As New YkkWebApi()

    '        Dim itemList As New List(Of APIOrderItems)()

    '        Dim item1 As APIOrderItems = api.CreateApiOrderItem("4164156", String.Empty, "580", String.Empty, 100)
    '        Dim item2 As APIOrderItems = api.CreateApiOrderItem("4449247", String.Empty, "580", "12.5", 110)
    '        Dim deliveryCode1 = "003497"
    '        Dim deliveryCode2 = "999"
    '        Dim buyercode = String.Empty


    '        itemList.Add(item1)
    '        itemList.Add(item2)


    '        Dim sasNo As String = "ORN00" & Date.Now.Year.ToString() & Convert.ToInt32(Date.Now.TimeOfDay.TotalMinutes)

    '        Dim siparisNo As String = String.Empty
    '        Try
    '            'Sipariş Gönderme

    '            siparisNo = api.SendOrder(sasNo, Date.Now.AddDays(3), APIOrderTimeConvention._2, False, buyercode, True, deliveryCode1, deliveryCode2, $"Örnek sipariş açıklaması {sasNo}", True, itemList)


    '            Console.WriteLine("> Gönderim Başarılı.")
    '            Console.WriteLine("> Gönderim Başarılı. Web Order No : {0}", siparisNo)
    '            'Sipariş sorgulama
    '            Console.WriteLine("> Sipariş Sorgulama işlemi başlıyor.... Web Order No : {0}", siparisNo)
    '            Dim orderNos As New List(Of String)()
    '            orderNos.Add(siparisNo)

    '            Dim orderDetail As List(Of APIOrderDetailResult) = api.GetOrder(orderNos, Nothing, Nothing, Nothing, Nothing)

    '            Console.WriteLine("> Sipariş Sorgulama Web Order No : {0}, Sipariş Durumu : {1}", siparisNo, orderDetail(0).OrderStatus)

    '        Catch yex As YkkClientException
    '            Console.WriteLine("> Status :{0}", yex.StatusCode)
    '            Console.WriteLine("> Response :{0}", yex.Response)
    '        Catch ex As Exception
    '            Console.WriteLine("> Hata : {0}", ex.Message)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    '''item1 customercodlu lengthsiz + item2 Müşteri kodlu lenghtli 
    '    '//// </summary>
    '    Private Shared Sub Gonderim_3()
    '        Dim api As New YkkWebApi()

    '        Dim itemList As New List(Of APIOrderItems)()

    '        Dim item1 As APIOrderItems = api.CreateApiOrderItem("4164156", String.Empty, "580", String.Empty, 100)
    '        Dim item2 As APIOrderItems = api.CreateApiOrderItem("0089294", "1040002733", "580", "7", 110)
    '        Dim deliveryCode1 = "003497"
    '        Dim deliveryCode2 = "001"
    '        Dim buyercode = String.Empty


    '        itemList.Add(item1)
    '        itemList.Add(item2)


    '        Dim sasNo As String = "ORN00" & Date.Now.Year.ToString() & Convert.ToInt32(Date.Now.TimeOfDay.TotalMinutes)

    '        Dim siparisNo As String = String.Empty
    '        Try
    '            'Sipariş Gönderme

    '            siparisNo = api.SendOrder(sasNo, Date.Now.AddDays(3), APIOrderTimeConvention._2, False, buyercode, True, deliveryCode1, deliveryCode2, $"Örnek sipariş açıklaması {sasNo}", True, itemList)


    '            Console.WriteLine("> Gönderim Başarılı.")
    '            Console.WriteLine("> Gönderim Başarılı. Web Order No : {0}", siparisNo)
    '            'Sipariş sorgulama
    '            Console.WriteLine("> Sipariş Sorgulama işlemi başlıyor.... Web Order No : {0}", siparisNo)
    '            Dim orderNos As New List(Of String)()
    '            orderNos.Add(siparisNo)

    '            Dim orderDetail As List(Of APIOrderDetailResult) = api.GetOrder(orderNos, Nothing, Nothing, Nothing, Nothing)

    '            Console.WriteLine("> Sipariş Sorgulama Web Order No : {0}, Sipariş Durumu : {1}", siparisNo, orderDetail(0).OrderStatus)

    '        Catch yex As YkkClientException
    '            Console.WriteLine("> Status :{0}", yex.StatusCode)
    '            Console.WriteLine("> Response :{0}", yex.Response)
    '        Catch ex As Exception
    '            Console.WriteLine("> Hata : {0}", ex.Message)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    '''buyercode ile sipariş gönderme
    '    '//// </summary>
    '    Private Shared Sub Gonderim_4()
    '        Dim api As New YkkWebApi()

    '        Dim itemList As New List(Of APIOrderItems)()
    '        Dim item1 As APIOrderItems = api.CreateApiOrderItem("0089294", "1040002733", "580", "7", 100)
    '        Dim item2 As APIOrderItems = api.CreateApiOrderItem("0089294", "1040002734", "580", "8", 110)
    '        Dim item3 As APIOrderItems = api.CreateApiOrderItem("0089294", "1040002735", "580", "9", 50)
    '        Dim item4 As APIOrderItems = api.CreateApiOrderItem("0852495", "1040003367", "196", "6", 51)
    '        Dim deliveryCode1 = "003497"
    '        Dim deliveryCode2 = "001"
    '        Dim buyercode = "101281"

    '        itemList.Add(item1)
    '        itemList.Add(item2)
    '        itemList.Add(item3)
    '        itemList.Add(item4)

    '        Dim sasNo As String = "ORN00" & Date.Now.Year.ToString() & Convert.ToInt32(Date.Now.TimeOfDay.TotalMinutes)

    '        Dim siparisNo As String = String.Empty
    '        Try
    '            'Sipariş Gönderme

    '            siparisNo = api.SendOrder(sasNo, Date.Now.AddDays(3), APIOrderTimeConvention._2, False, buyercode, True, deliveryCode1, deliveryCode2, $"Örnek sipariş açıklaması {sasNo}", True, itemList)


    '            Console.WriteLine("> Gönderim Başarılı.")
    '            Console.WriteLine("> Gönderim Başarılı. Web Order No : {0}", siparisNo)
    '            'Sipariş sorgulama
    '            Console.WriteLine("> Sipariş Sorgulama işlemi başlıyor.... Web Order No : {0}", siparisNo)
    '            Dim orderNos As New List(Of String)()
    '            orderNos.Add(siparisNo)

    '            Dim orderDetail As List(Of APIOrderDetailResult) = api.GetOrder(orderNos, Nothing, Nothing, Nothing, Nothing)

    '            Console.WriteLine("> Sipariş Sorgulama Web Order No : {0}, Sipariş Durumu : {1}", siparisNo, orderDetail(0).OrderStatus)

    '        Catch yex As YkkClientException
    '            Console.WriteLine("> Status :{0}", yex.StatusCode)
    '            Console.WriteLine("> Response :{0}", yex.Response)
    '        Catch ex As Exception
    '            Console.WriteLine("> Hata : {0}", ex.Message)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    '''Yanlış buyercode ile sipariş gönderme
    '    '''
    '    '//// </summary>
    '    Private Shared Sub Gonderim_4_Hatali()
    '        Dim api As New YkkWebApi()

    '        Dim itemList As New List(Of APIOrderItems)()
    '        Dim item1 As APIOrderItems = api.CreateApiOrderItem("0089294", "1040002733", "580", "7", 100)
    '        Dim item2 As APIOrderItems = api.CreateApiOrderItem("0089294", "1040002734", "580", "8", 110)
    '        Dim item3 As APIOrderItems = api.CreateApiOrderItem("0089294", "1040002735", "580", "9", 50)
    '        Dim item4 As APIOrderItems = api.CreateApiOrderItem("0852495", "1040003367", "196", "6", 51)
    '        Dim deliveryCode1 = "003497"
    '        Dim deliveryCode2 = "001"
    '        Dim buyercode = "999999"

    '        itemList.Add(item1)
    '        itemList.Add(item2)
    '        itemList.Add(item3)
    '        itemList.Add(item4)

    '        Dim sasNo As String = "ORN00" & Date.Now.Year.ToString() & Convert.ToInt32(Date.Now.TimeOfDay.TotalMinutes)

    '        Dim siparisNo As String = String.Empty
    '        Try
    '            'Sipariş Gönderme

    '            siparisNo = api.SendOrder(sasNo, Date.Now.AddDays(3), APIOrderTimeConvention._2, False, buyercode, True, deliveryCode1, deliveryCode2, $"Örnek sipariş açıklaması {sasNo}", True, itemList)


    '            Console.WriteLine("> Gönderim Başarılı.")
    '            Console.WriteLine("> Gönderim Başarılı. Web Order No : {0}", siparisNo)
    '            'Sipariş sorgulama
    '            Console.WriteLine("> Sipariş Sorgulama işlemi başlıyor.... Web Order No : {0}", siparisNo)
    '            Dim orderNos As New List(Of String)()
    '            orderNos.Add(siparisNo)

    '            Dim orderDetail As List(Of APIOrderDetailResult) = api.GetOrder(orderNos, Nothing, Nothing, Nothing, Nothing)

    '            Console.WriteLine("> Sipariş Sorgulama Web Order No : {0}, Sipariş Durumu : {1}", siparisNo, orderDetail(0).OrderStatus)

    '        Catch yex As YkkClientException
    '            Console.WriteLine("> Status :{0}", yex.StatusCode)
    '            Console.WriteLine("> Response :{0}", yex.Response)
    '        Catch ex As Exception

    '            Console.WriteLine("> Hata : {0}", ex.Message)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    '''Yanlış Uzunluk Bilgileri ile sipariş denemesi
    '    '//// </summary>
    '    Private Shared Sub Gonderim_3_Hatali()
    '        Dim api As New YkkWebApi()

    '        Dim itemList As New List(Of APIOrderItems)()

    '        Dim item1 As APIOrderItems = api.CreateApiOrderItem(String.Empty, "1040129592", String.Empty, "10", 100) 'Olması Gereken 11
    '        Dim item2 As APIOrderItems = api.CreateApiOrderItem(String.Empty, "1040129409", String.Empty, "12", 110)
    '        Dim deliveryCode1 = "003497"
    '        Dim deliveryCode2 = "001"
    '        Dim buyercode = String.Empty


    '        itemList.Add(item1)
    '        itemList.Add(item2)


    '        Dim sasNo As String = "ORN00" & Date.Now.Year.ToString() & Convert.ToInt32(Date.Now.TimeOfDay.TotalMinutes)

    '        Dim siparisNo As String = String.Empty
    '        Try
    '            'Sipariş Gönderme

    '            siparisNo = api.SendOrder(sasNo, Date.Now.AddDays(3), APIOrderTimeConvention._2, False, buyercode, True, deliveryCode1, deliveryCode2, $"Örnek sipariş açıklaması {sasNo}", True, itemList)


    '            Console.WriteLine("> Gönderim Başarılı.")
    '            Console.WriteLine("> Gönderim Başarılı. Web Order No : {0}", siparisNo)
    '            'Sipariş sorgulama
    '            Console.WriteLine("> Sipariş Sorgulama işlemi başlıyor.... Web Order No : {0}", siparisNo)
    '            Dim orderNos As New List(Of String)()
    '            orderNos.Add(siparisNo)

    '            Dim orderDetail As List(Of APIOrderDetailResult) = api.GetOrder(orderNos, Nothing, Nothing, Nothing, Nothing)

    '            Console.WriteLine("> Sipariş Sorgulama Web Order No : {0}, Sipariş Durumu : {1}", siparisNo, orderDetail(0).OrderStatus)

    '        Catch yex As YkkClientException
    '            Console.WriteLine("> Status :{0}", yex.StatusCode)
    '            Console.WriteLine("> Response :{0}", yex.Response)
    '        Catch ex As Exception

    '            Console.WriteLine("> Hata : {0}", ex.Message)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    '''Quantity kodu M ve P olan 2 ürün için sadece ürün kodu ile sipariş denemesi
    '    '//// </summary>
    '    Private Shared Sub Gonderim_5()
    '        Dim api As New YkkWebApi()

    '        Dim itemList As New List(Of APIOrderItems)()

    '        Dim item1 As APIOrderItems = api.CreateApiOrderItem("0852495", String.Empty, "580", "12", 100) 'QuantityKodu=P
    '        Dim item2 As APIOrderItems = api.CreateApiOrderItem("0578758", String.Empty, "580", String.Empty, 110) ' QuantityKodu=M
    '        Dim deliveryCode1 = "003497"
    '        Dim deliveryCode2 = "001"
    '        Dim buyercode = String.Empty


    '        itemList.Add(item1)
    '        itemList.Add(item2)


    '        Dim sasNo As String = "ORN00" & Date.Now.Year.ToString() & Convert.ToInt32(Date.Now.TimeOfDay.TotalMinutes)

    '        Dim siparisNo As String = String.Empty
    '        Try
    '            'Sipariş Gönderme

    '            siparisNo = api.SendOrder(sasNo, Date.Now.AddDays(3), APIOrderTimeConvention._2, False, buyercode, True, deliveryCode1, deliveryCode2, $"Örnek sipariş açıklaması {sasNo}", True, itemList)


    '            Console.WriteLine("> Gönderim Başarılı.")
    '            Console.WriteLine("> Gönderim Başarılı. Web Order No : {0}", siparisNo)
    '            'Sipariş sorgulama
    '            Console.WriteLine("> Sipariş Sorgulama işlemi başlıyor.... Web Order No : {0}", siparisNo)
    '            Dim orderNos As New List(Of String)()
    '            orderNos.Add(siparisNo)

    '            Dim orderDetail As List(Of APIOrderDetailResult) = api.GetOrder(orderNos, Nothing, Nothing, Nothing, Nothing)

    '            Console.WriteLine("> Sipariş Sorgulama Web Order No : {0}, Sipariş Durumu : {1}", siparisNo, orderDetail(0).OrderStatus)

    '        Catch yex As YkkClientException
    '            Console.WriteLine("> Status :{0}", yex.StatusCode)
    '            Console.WriteLine("> Response :{0}", yex.Response)
    '        Catch ex As Exception

    '            Console.WriteLine("> Hata : {0}", ex.Message)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    '''Quantity kodu M ve P olan 2 ürün için sadece ürün kodu ile hatalı sipariş denemesi
    '    '//// </summary>
    '    Private Shared Sub Gonderim_5_hatali()
    '        Dim api As New YkkWebApi()

    '        Dim itemList As New List(Of APIOrderItems)()

    '        Dim item1 As APIOrderItems = api.CreateApiOrderItem("0852495", String.Empty, String.Empty, String.Empty, 100) 'QuantityKodu=P
    '        Dim item2 As APIOrderItems = api.CreateApiOrderItem("0578758", String.Empty, String.Empty, String.Empty, 110) ' QuantityKodu=M
    '        Dim deliveryCode1 = "003497"
    '        Dim deliveryCode2 = "001"
    '        Dim buyercode = String.Empty


    '        itemList.Add(item1)
    '        itemList.Add(item2)


    '        Dim sasNo As String = "ORN00" & Date.Now.Year.ToString() & Convert.ToInt32(Date.Now.TimeOfDay.TotalMinutes)

    '        Dim siparisNo As String = String.Empty
    '        Try
    '            'Sipariş Gönderme

    '            siparisNo = api.SendOrder(sasNo, Date.Now.AddDays(3), APIOrderTimeConvention._2, False, buyercode, True, deliveryCode1, deliveryCode2, $"Örnek sipariş açıklaması {sasNo}", True, itemList)


    '            Console.WriteLine("> Gönderim Başarılı.")
    '            Console.WriteLine("> Gönderim Başarılı. Web Order No : {0}", siparisNo)
    '            'Sipariş sorgulama
    '            Console.WriteLine("> Sipariş Sorgulama işlemi başlıyor.... Web Order No : {0}", siparisNo)
    '            Dim orderNos As New List(Of String)()
    '            orderNos.Add(siparisNo)

    '            Dim orderDetail As List(Of APIOrderDetailResult) = api.GetOrder(orderNos, Nothing, Nothing, Nothing, Nothing)

    '            Console.WriteLine("> Sipariş Sorgulama Web Order No : {0}, Sipariş Durumu : {1}", siparisNo, orderDetail(0).OrderStatus)

    '        Catch yex As YkkClientException
    '            Console.WriteLine("> Status :{0}", yex.StatusCode)
    '            Console.WriteLine("> Response :{0}", yex.Response)
    '        Catch ex As Exception

    '            Console.WriteLine("> Hata : {0}", ex.Message)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    '''Uzunluk Bilgisi İstenmeyen Ürün için uzunluk bilgisi gönderilmesi
    '    '//// </summary>
    '    Private Shared Sub Gonderim_6_Hatali()
    '        Dim api As New YkkWebApi()

    '        Dim itemList As New List(Of APIOrderItems)()

    '        Dim item1 As APIOrderItems = api.CreateApiOrderItem("0852495", String.Empty, "580", "20.5", 100) 'QuantityKodu=P
    '        Dim item2 As APIOrderItems = api.CreateApiOrderItem("0578758", String.Empty, "580", "12", 110) ' QuantityKodu=M
    '        Dim deliveryCode1 = "003497"
    '        Dim deliveryCode2 = "001"
    '        Dim buyercode = String.Empty


    '        itemList.Add(item1)
    '        itemList.Add(item2)


    '        Dim sasNo As String = "ORN00" & Date.Now.Year.ToString() & Convert.ToInt32(Date.Now.TimeOfDay.TotalMinutes)

    '        Dim siparisNo As String = String.Empty
    '        Try
    '            'Sipariş Gönderme

    '            siparisNo = api.SendOrder(sasNo, Date.Now.AddDays(3), APIOrderTimeConvention._2, False, buyercode, True, deliveryCode1, deliveryCode2, $"Örnek sipariş açıklaması {sasNo}", True, itemList)


    '            Console.WriteLine("> Gönderim Başarılı.")
    '            Console.WriteLine("> Gönderim Başarılı. Web Order No : {0}", siparisNo)
    '            'Sipariş sorgulama
    '            Console.WriteLine("> Sipariş Sorgulama işlemi başlıyor.... Web Order No : {0}", siparisNo)
    '            Dim orderNos As New List(Of String)()
    '            orderNos.Add(siparisNo)

    '            Dim orderDetail As List(Of APIOrderDetailResult) = api.GetOrder(orderNos, Nothing, Nothing, Nothing, Nothing)

    '            Console.WriteLine("> Sipariş Sorgulama Web Order No : {0}, Sipariş Durumu : {1}", siparisNo, orderDetail(0).OrderStatus)

    '        Catch yex As YkkClientException
    '            Console.WriteLine("> Status :{0}", yex.StatusCode)
    '            Console.WriteLine("> Response :{0}", yex.Response)
    '        Catch ex As Exception

    '            Console.WriteLine("> Hata : {0}", ex.Message)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    '''Sadece Eşleştirme Kodu ile Sipariş denemesi
    '    '//// </summary>
    '    Private Shared Sub Gonderim_6()
    '        Dim api As New YkkWebApi()

    '        Dim itemList As New List(Of APIOrderItems)()

    '        Dim item1 As APIOrderItems = api.CreateApiOrderItem(String.Empty, "1040126447", String.Empty, String.Empty, 100)
    '        Dim item2 As APIOrderItems = api.CreateApiOrderItem(String.Empty, "1040126448", String.Empty, String.Empty, 110)
    '        Dim deliveryCode1 = "003497"
    '        Dim deliveryCode2 = "001"
    '        Dim buyercode = String.Empty


    '        itemList.Add(item1)
    '        itemList.Add(item2)


    '        Dim sasNo As String = "ORN00" & Date.Now.Year.ToString() & Convert.ToInt32(Date.Now.TimeOfDay.TotalMinutes)

    '        Dim siparisNo As String = String.Empty
    '        Try
    '            'Sipariş Gönderme

    '            siparisNo = api.SendOrder(sasNo, Date.Now.AddDays(3), APIOrderTimeConvention._2, False, buyercode, True, deliveryCode1, deliveryCode2, $"Örnek sipariş açıklaması {sasNo}", True, itemList)


    '            Console.WriteLine("> Gönderim Başarılı.")
    '            Console.WriteLine("> Gönderim Başarılı. Web Order No : {0}", siparisNo)
    '            'Sipariş sorgulama
    '            Console.WriteLine("> Sipariş Sorgulama işlemi başlıyor.... Web Order No : {0}", siparisNo)
    '            Dim orderNos As New List(Of String)()
    '            orderNos.Add(siparisNo)

    '            Dim orderDetail As List(Of APIOrderDetailResult) = api.GetOrder(orderNos, Nothing, Nothing, Nothing, Nothing)

    '            Console.WriteLine("> Sipariş Sorgulama Web Order No : {0}, Sipariş Durumu : {1}", siparisNo, orderDetail(0).OrderStatus)

    '        Catch yex As YkkClientException
    '            Console.WriteLine("> Status :{0}", yex.StatusCode)
    '            Console.WriteLine("> Response :{0}", yex.Response)
    '        Catch ex As Exception

    '            Console.WriteLine("> Hata : {0}", ex.Message)
    '        End Try
    '    End Sub

    '#End Region

    '#Region "Ürün ve Adres Bilgileri"
    '    Public Shared Sub GetAddresses()
    '        Try
    '            Dim api As New YkkWebApi()
    '            Console.WriteLine("> Adresler sorgulanıyor...")

    '            Dim addresses As List(Of APICustomerAdress) = api.GetDeliveryAddress(200, 1)
    '            addresses.ForEach(Sub(a)
    '                                  Console.WriteLine("> {0}-{1}-{2}", a.DeliveryCode1, a.DeliveryCode2, a.DeliveryAdress)
    '                              End Sub)
    '            Console.WriteLine("> Address sorgulama tamamlandı....")
    '        Catch yex As YkkClientException
    '            Console.WriteLine("> Status :{0}", yex.StatusCode)
    '            Console.WriteLine("> Response :{0}", yex.Response)
    '        Catch ex As Exception

    '            Console.WriteLine("> exception :{0}", ex.Message)
    '        End Try
    '    End Sub

    '    Public Shared Sub GetProducts()
    '        Try
    '            Dim api As New YkkWebApi()
    '            Console.WriteLine("> Ürünler sorgulanıyor...")
    '            Dim prods As List(Of APICUSTOMERSPRODUCT) = api.GetProducts(Nothing, 500, 1)
    '            prods.ForEach(Sub(a)
    '                              Console.WriteLine("> {0}-{1}-{2}", a.Itemcode, a.CustomersProductCode, a.Itemname)
    '                          End Sub)
    '            Console.WriteLine("> {0} adet Ürün alındı.", prods.Count)
    '        Catch yex As YkkClientException
    '            Console.WriteLine("> Status :{0}", yex.StatusCode)
    '            Console.WriteLine("> Response :{0}", yex.Response)
    '        Catch ex As Exception

    '            Console.WriteLine("> exception :{0}", ex.Message)
    '        End Try
    '    End Sub
    '#End Region
End Class
