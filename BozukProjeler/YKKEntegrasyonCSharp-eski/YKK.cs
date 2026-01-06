using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace YKKEntegrasyon
{
    public class YKK
    {
        public void main()
        {
            GetAddresses();
            MessageBox.Show ("xxx");
        }

        static void GetAddresses()
        {
            try
            {
                YkkWebApi api = new YkkWebApi();
                Console.WriteLine("> Adresler sorgulanıyor...");

                List<APICustomerAdress> addresses = api.GetDeliveryAddress(200, 1);
                addresses.ForEach(a => {
                    Console.WriteLine("> {0}-{1}-{2}", a.DeliveryCode1, a.DeliveryCode2, a.DeliveryAdress);
                });
                Console.WriteLine("> Address sorgulama tamamlandı....");
            }
            catch (YkkClientException yex)
            {
                Console.WriteLine("> Status :{0}", yex.StatusCode);
                Console.WriteLine("> Response :{0}", yex.Response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("> exception :{0}", ex.Message);
            }
        }

        static void GetProducts()
        {
            try
            {
                YkkWebApi api = new YkkWebApi();
                Console.WriteLine("> Ürünler sorgulanıyor...");
                List<APICUSTOMERSPRODUCT> prods = api.GetProducts(null, 500, 1);
                prods.ForEach(a => {
                    Console.WriteLine("> {0}-{1}-{2}", a.Itemcode, a.CustomersProductCode, a.Itemname);
                });
                Console.WriteLine("> {0} adet Ürün alındı.", prods.Count);
            }
            catch (YkkClientException yex)
            {
                Console.WriteLine("> Status :{0}", yex.StatusCode);
                Console.WriteLine("> Response :{0}", yex.Response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("> exception :{0}", ex.Message);
            }
        }
    }
}
