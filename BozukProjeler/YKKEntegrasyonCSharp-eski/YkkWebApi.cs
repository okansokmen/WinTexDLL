using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YKKEntegrasyon
{
    public class YkkWebApi : IDisposable
    {

        public YkkWebApi()
        {

        }
        public readonly CultureInfo En = new CultureInfo("en-GB");
        public Client Client { get; set; }
        private string Url = System.Configuration.ConfigurationManager.AppSettings["apiurl"]?.ToString();
        private string UserName = System.Configuration.ConfigurationManager.AppSettings["apiusername"]?.ToString();
        private string Password = System.Configuration.ConfigurationManager.AppSettings["apipassword"]?.ToString();

        private void Login()
        {
            try
            {
                Url = System.Configuration.ConfigurationManager.AppSettings["apiurl"]?.ToString();
                UserName = System.Configuration.ConfigurationManager.AppSettings["apiusername"]?.ToString();
                Password = System.Configuration.ConfigurationManager.AppSettings["apipassword"]?.ToString();

                Client = null;
                Client = new Client(Url)
                {
                    Username = UserName,
                    Password = Password
                };
            }
            catch (YkkClientException yex)
            {
                throw yex;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// Eşleştirilmiş ürünlerin alınması için kullanılır. topValue:500, pagenumber:1 olduğunda ilk 500 ürün döner, pagenumber:2 olduğunda ikinci 500 adet ürün dönüşü sağlanır. topValue int.maxvalue verilir
        /// ve pagenumber:1 verilir ise tüm veriyi tek sayfada dönecektir.
        /// </summary>
        /// <param name="topValue">Kaç adet ürün request edilecek ?</param>
        /// <param name="pagenumber">select edilecek sayfa sayısı </param>
        /// <returns>List APICUSTOMERSPRODUCT</returns>
        public List<APICUSTOMERSPRODUCT> GetProducts(string customerProductCode, int? topValue, int? pagenumber)
        {
            try
            {
                Login();
                ICollection<APICUSTOMERSPRODUCT> products = Client.GetMappedProductsAsync(customerProductCode, pagenumber, topValue).GetAwaiter().GetResult();

                if (products != null)
                    return products.ToList();
                else
                    return null;
            }
            catch (YkkClientException yex)
            {

                throw yex;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Şirketinizin sistemde kayıtlı teslimat adreslerini döner. topValue:100, pagenumber:1 olduğunda ilk 100 adres döner, pagenumber:2 olduğunda ikinci 100 adet ürün dönüşü sağlanır. topValue int.maxvalue verilir
        /// ve pagenumber:1 verilir ise tüm veriyi tek sayfada dönecektir.
        /// </summary>
        /// <param name="topValue"></param>
        /// <param name="pagenumber"></param>
        /// <returns>ApiCustomerAddress</returns>
        public List<APICustomerAdress> GetDeliveryAddress(int topValue, int pagenumber)
        {
            try
            {
                Login();
                ICollection<APICustomerAdress> addresses = Client.GetAllAdressAsync(pagenumber, topValue).GetAwaiter().GetResult();
                if (addresses != null)
                    return addresses.ToList();
                else
                    return null;
            }
            catch (YkkClientException yex)
            {

                throw yex;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Belirtilen teslimat tarihleri aralığındaki siparişlerin iletilmesi sağlanır.
        /// </summary>
        /// <param name="requestStartDate">Başlangıç Tarihi</param>
        /// <param name="requestEndDate">Bitiş Tarihi</param>
        /// <returns>List ApiOrderDetailResult</returns>
        public List<APIOrderDetailResult> GetOrder(List<string> orderNos, DateTime? createStartDate, DateTime? createEndDate, DateTime? requestStartDate, DateTime? requestEndDate)
        {
            try
            {
                Login();
                ICollection<APIOrderDetailResult> orders = Client.GetOrderAsync(orderNos, createStartDate, createEndDate, requestStartDate, requestEndDate).GetAwaiter().GetResult();
                if (orders != null)
                    return orders.ToList();
                else
                    return null;
            }
            catch (YkkClientException yex)
            {

                throw yex;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Sipariş Gönderimi yapılır
        /// </summary>
        /// <param name="_sasNo">Şirketinize ait Satınalma sipariş numarası (SASNO)</param>
        /// <param name="_requestDate">Teslimat Tarihi</param>
        /// <param name="_time">_1 Ögleden önce | _2 Öğleden sonra teslimat</param>
        /// <param name="_sampleOrder">Örnek sipariş mi ?</param>
        /// <param name="_buyerCode">Marka Kodu</param>
        /// <param name="_completeDelivery">Sipariş tamamlandığında gönderim</param>
        /// <param name="_deliveryCode1">Teslimat Adres Kodu 1 (Adres listesinden)</param>
        /// <param name="_deliveryCode2">Teslimat Adres Kodu 2 (Adres Listesinden)</param>
        /// <param name="_orderComment">Şipariş Açıklaması (Max:105 karakter)</param>
        /// <param name="_noCommercial">Ticari olmayan sipariş</param>
        /// <param name="_orderItems">Sipariş Kalemleri</param>
        /// <returns></returns>
        public string SendOrder(string _sasNo, DateTime _requestDate, APIOrderTimeConvention _time, bool _sampleOrder, string _buyerCode, bool _completeDelivery, string _deliveryCode1,
            string _deliveryCode2, string _orderComment, bool _noCommercial, List<APIOrderItems> _orderItems)
        {
            try
            {
                APIOrder order = new APIOrder();
                order.BuyerCode = _buyerCode;
                order.CompleteDelivery = _completeDelivery; //Siparişin tamamı teslim edilecek mi ? Parçalı teslim olacak ise FALSE değer verilmeli.
                order.DeliverCode1 = _deliveryCode1; //Addressler üzerinden alınan DeliveryCode1 değeri
                order.DeliverCode2 = _deliveryCode2; //Adresslerden alınan DeliveryCode2 değeri
                order.OrderComment = _orderComment.Length > 105 ? _orderComment.Substring(0, 105) : _orderComment; //Sipariş için açıklama değeri girilebilir (Maximum 105 karakter)
                order.NoCommercial = _noCommercial; //Ticari bir sipariş olup olmaması. Default false;
                order.Purchasecontractno = _sasNo; //Firmanızın varsa SAS numarasıdır. (Maximum 15 karakter)
                order.RequestDate = _requestDate; //Siparişin teslim zamanı
                order.SampleOrder = _sampleOrder; //Örnek sipariş mi ? (50'nin altındaki ürünler için )
                //order.TimeConvention = APIOrderTimeConvention._1 //AM
                //order.TimeConvention = APIOrderTimeConvention._2; //PM
                order.TimeConvention = _time;
                order.ApiOrderItems = _orderItems;
                Login();
                return Client.PostOrderAsync(order).GetAwaiter().GetResult();

            }
            catch (YkkClientException yex)
            {
                throw yex;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Sipariş kalemi yaratır ( Send Order Parametresi içerisindeki objenin yaratılması için eklenmiş functiondır )
        /// </summary>
        /// <param name="_productCode">Ykk Ürün Kodu</param>
        /// <param name="_customerProductCode">Müşteri Ürün Kodu</param>
        /// <param name="_color">Renk kodu</param>
        /// <param name="_length">Uzunluk (eng culture için)</param>
        /// <param name="_count">Sipariş Adeti</param>
        /// <returns></returns>
        public APIOrderItems CreateApiOrderItem(string _productCode, string _customerProductCode, string _color, string _length, int _count)
        {
            return new APIOrderItems
            {
                ProductCode = _productCode, //YKK Product Code bilgisi girilebilir.
                CustomersProductCode = _customerProductCode, //Eşleştirilmiş Customer Product Code Bilgisi Girilebilir.
                Color = _color,
                Length = ConvertDecimalEN(ConvertStringToDecimalEN(_length)), //Decimal(5,1) değer olmalıdır.
                Count = _count //Kalem Adeti
            };

        }

        public string ConvertDecimalEN(decimal _length)
        {
            string retval = string.Empty;
            try
            {
                //retval = val.ToString("N2", _tr);
                return _length.ToString(this.En);
            }
            catch
            {

            }
            return retval;
        }
        public decimal ConvertStringToDecimalEN(string _length)
        {
            decimal retval = 0;
            try
            {
                //retval = val.ToString("N2", _tr);
                return Convert.ToDecimal(_length, this.En);//_length.ToString(this.En);
            }
            catch
            {

            }
            return retval;
        }

        public string ConvertStringDecimalEN(decimal val)
        {
            string retval = string.Empty;
            try
            {
                //retval = val.ToString("N2", _tr);
                return val.ToString(this.En);
            }
            catch
            {

            }
            return retval;
        }


        public void Dispose()
        {
            if (Client != null)
                Client = null;
            GC.Collect();
            GC.SuppressFinalize(this);
        }
    }
}
