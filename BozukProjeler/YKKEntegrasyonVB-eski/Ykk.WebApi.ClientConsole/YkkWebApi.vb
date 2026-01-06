Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace Ykk.WebApi.ClientConsole
	Public Class YkkWebApi
		Implements IDisposable

		Public Sub New()

		End Sub
		Public ReadOnly En As New CultureInfo("en-GB")
		Public Property Client() As Client
		Private ReadOnly Property Url() As String
			Get
				Return System.Configuration.ConfigurationManager.AppSettings("apiurl")?.ToString()
			End Get
		End Property
		Private ReadOnly Property UserName() As String
			Get
				Return System.Configuration.ConfigurationManager.AppSettings("apiusername")?.ToString()
			End Get
		End Property
		Private ReadOnly Property Password() As String
			Get
				Return System.Configuration.ConfigurationManager.AppSettings("apipassword")?.ToString()
			End Get
		End Property

		Private Sub Login()
			Try
				Client = Nothing
				Client = New Client(Url) With {
					.Username = UserName,
					.Password = Password
				}
			Catch yex As YkkClientException
				Throw yex
			Catch ex As Exception

				Throw ex
			End Try
		End Sub


		''' <summary>
		''' Eşleştirilmiş ürünlerin alınması için kullanılır. topValue:500, pagenumber:1 olduğunda ilk 500 ürün döner, pagenumber:2 olduğunda ikinci 500 adet ürün dönüşü sağlanır. topValue int.maxvalue verilir
		''' ve pagenumber:1 verilir ise tüm veriyi tek sayfada dönecektir.
		''' </summary>
		''' <param name="topValue">Kaç adet ürün request edilecek ?</param>
		''' <param name="pagenumber">select edilecek sayfa sayısı </param>
		''' <returns>List APICUSTOMERSPRODUCT</returns>
		Public Function GetProducts(ByVal customerProductCode As String, ByVal topValue? As Integer, ByVal pagenumber? As Integer) As List(Of APICUSTOMERSPRODUCT)
			Try
				Login()
				Dim products As ICollection(Of APICUSTOMERSPRODUCT) = Client.GetMappedProductsAsync(customerProductCode, pagenumber, topValue).GetAwaiter().GetResult()

				If products IsNot Nothing Then
					Return products.ToList()
				Else
					Return Nothing
				End If
			Catch yex As YkkClientException

				Throw yex
			Catch ex As Exception

				Throw ex
			End Try
		End Function
		''' <summary>
		''' Şirketinizin sistemde kayıtlı teslimat adreslerini döner. topValue:100, pagenumber:1 olduğunda ilk 100 adres döner, pagenumber:2 olduğunda ikinci 100 adet ürün dönüşü sağlanır. topValue int.maxvalue verilir
		''' ve pagenumber:1 verilir ise tüm veriyi tek sayfada dönecektir.
		''' </summary>
		''' <param name="topValue"></param>
		''' <param name="pagenumber"></param>
		''' <returns>ApiCustomerAddress</returns>
		Public Function GetDeliveryAddress(ByVal topValue As Integer, ByVal pagenumber As Integer) As List(Of APICustomerAdress)
			Try
				Login()
				Dim addresses As ICollection(Of APICustomerAdress) = Client.GetAllAdressAsync(pagenumber, topValue).GetAwaiter().GetResult()
				If addresses IsNot Nothing Then
					Return addresses.ToList()
				Else
					Return Nothing
				End If
			Catch yex As YkkClientException

				Throw yex
			Catch ex As Exception

				Throw ex
			End Try
		End Function
		''' <summary>
		''' Belirtilen teslimat tarihleri aralığındaki siparişlerin iletilmesi sağlanır.
		''' </summary>
		''' <param name="requestStartDate">Başlangıç Tarihi</param>
		''' <param name="requestEndDate">Bitiş Tarihi</param>
		''' <returns>List ApiOrderDetailResult</returns>
		Public Function GetOrder(ByVal orderNos As List(Of String), ByVal createStartDate? As Date, ByVal createEndDate? As Date, ByVal requestStartDate? As Date, ByVal requestEndDate? As Date) As List(Of APIOrderDetailResult)
			Try
				Login()
				Dim orders As ICollection(Of APIOrderDetailResult) = Client.GetOrderAsync(orderNos, createStartDate, createEndDate, requestStartDate, requestEndDate).GetAwaiter().GetResult()
				If orders IsNot Nothing Then
					Return orders.ToList()
				Else
					Return Nothing
				End If
			Catch yex As YkkClientException

				Throw yex
			Catch ex As Exception

				Throw ex
			End Try
		End Function

		''' <summary>
		''' Sipariş Gönderimi yapılır
		''' </summary>
		''' <param name="_sasNo">Şirketinize ait Satınalma sipariş numarası (SASNO)</param>
		''' <param name="_requestDate">Teslimat Tarihi</param>
		''' <param name="_time">_1 Ögleden önce | _2 Öğleden sonra teslimat</param>
		''' <param name="_sampleOrder">Örnek sipariş mi ?</param>
		''' <param name="_buyerCode">Marka Kodu</param>
		''' <param name="_completeDelivery">Sipariş tamamlandığında gönderim</param>
		''' <param name="_deliveryCode1">Teslimat Adres Kodu 1 (Adres listesinden)</param>
		''' <param name="_deliveryCode2">Teslimat Adres Kodu 2 (Adres Listesinden)</param>
		''' <param name="_orderComment">Şipariş Açıklaması (Max:105 karakter)</param>
		''' <param name="_noCommercial">Ticari olmayan sipariş</param>
		''' <param name="_orderItems">Sipariş Kalemleri</param>
		''' <returns></returns>
		Public Function SendOrder(ByVal _sasNo As String, ByVal _requestDate As Date, ByVal _time As APIOrderTimeConvention, ByVal _sampleOrder As Boolean, ByVal _buyerCode As String, ByVal _completeDelivery As Boolean, ByVal _deliveryCode1 As String, ByVal _deliveryCode2 As String, ByVal _orderComment As String, ByVal _noCommercial As Boolean, ByVal _orderItems As List(Of APIOrderItems)) As String
			Try
				Dim order As New APIOrder()
				order.BuyerCode = _buyerCode
				order.CompleteDelivery = _completeDelivery 'Siparişin tamamı teslim edilecek mi ? Parçalı teslim olacak ise FALSE değer verilmeli.
				order.DeliverCode1 = _deliveryCode1 'Addressler üzerinden alınan DeliveryCode1 değeri
				order.DeliverCode2 = _deliveryCode2 'Adresslerden alınan DeliveryCode2 değeri
				order.OrderComment = If(_orderComment.Length > 105, _orderComment.Substring(0,105), _orderComment) 'Sipariş için açıklama değeri girilebilir (Maximum 105 karakter)
				order.NoCommercial = _noCommercial 'Ticari bir sipariş olup olmaması. Default false;
				order.Purchasecontractno = _sasNo 'Firmanızın varsa SAS numarasıdır. (Maximum 15 karakter)
				order.RequestDate = _requestDate 'Siparişin teslim zamanı
				order.SampleOrder = _sampleOrder 'Örnek sipariş mi ? (50'nin altındaki ürünler için )
				'order.TimeConvention = APIOrderTimeConvention._1 //AM
				'order.TimeConvention = APIOrderTimeConvention._2; //PM
				order.TimeConvention = _time
				order.ApiOrderItems = _orderItems
				Login()
				Return Client.PostOrderAsync(order).GetAwaiter().GetResult()

			Catch yex As YkkClientException
				Throw yex
			Catch ex As Exception

				Throw ex
			End Try
		End Function

		''' <summary>
		''' Sipariş kalemi yaratır ( Send Order Parametresi içerisindeki objenin yaratılması için eklenmiş functiondır )
		''' </summary>
		''' <param name="_productCode">Ykk Ürün Kodu</param>
		''' <param name="_customerProductCode">Müşteri Ürün Kodu</param>
		''' <param name="_color">Renk kodu</param>
		''' <param name="_length">Uzunluk (eng culture için)</param>
		''' <param name="_count">Sipariş Adeti</param>
		''' <returns></returns>
		Public Function CreateApiOrderItem(ByVal _productCode As String, ByVal _customerProductCode As String, ByVal _color As String, ByVal _length As String, ByVal _count As Integer) As APIOrderItems
			Return New APIOrderItems With {
				.ProductCode = _productCode,
				.CustomersProductCode = _customerProductCode,
				.Color = _color,
				.Length = ConvertDecimalEN(ConvertStringToDecimalEN(_length)),
				.Count = _count
			}

		End Function

		Public Function ConvertDecimalEN(ByVal _length As Decimal) As String
			Dim retval As String = String.Empty
			Try
				'retval = val.ToString("N2", _tr);
				Return _length.ToString(Me.En)
			Catch

			End Try
			Return retval
		End Function
		Public Function ConvertStringToDecimalEN(ByVal _length As String) As Decimal
			Dim retval As Decimal = 0
			Try
				'retval = val.ToString("N2", _tr);
				Return Convert.ToDecimal(_length, Me.En) '_length.ToString(this.En);
			Catch

			End Try
			Return retval
		End Function

		Public Function ConvertStringDecimalEN(ByVal val As Decimal) As String
			Dim retval As String = String.Empty
			Try
				'retval = val.ToString("N2", _tr);
				Return val.ToString(Me.En)
			Catch

			End Try
			Return retval
		End Function


		Public Sub Dispose() Implements IDisposable.Dispose
			If Client IsNot Nothing Then
				Client = Nothing
			End If
			GC.Collect()
			GC.SuppressFinalize(Me)
		End Sub
	End Class
End Namespace
