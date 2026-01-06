using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YKKEntegrasyon
{
    class Program
    {
        static void Main(string[] args)
        {


            //Gonderim_1();//DESC:Eşleşme yapılmış ürün için customer kod ile gönderilmesi (lenght var, color var) WO00000022

            //Gonderim_2(); //DESC:Müşteri kodu ile  uzunluğu olan +  uzunluğu olmayan item için sipariş açma "WO00000023"

            //Gonderim_2_Hatali(); //DESC: Yanlış Adres kodu ile Sipariş Geçilmesi

            //Gonderim_3(); //DESC: item1 customercodlu lengthsiz + item2 Müşteri kodlu lenghtli WO00000025

            //Gonderim_3_Hatali(); //DESC:Yanlış Uzunluk Bilgileri ile sipariş denemesi

            //Gonderim_4(); //DESC: buyercode ile sipariş gönderme WO00000027

            //Gonderim_4_Hatali();//DESC:Yanlış buyercode ile sipariş gönderme

            //Gonderim_5(); //DESC: Quantity kodu M ve P olan 2 ürün için sadece ürün kodu, uzunluk ve renk bilgileri ile sipariş denemesi WO00000029

            //Gonderim_5_hatali();  //DESC: Quantity kodu M ve P olan 2 ürün için sadece ürün kodu ile hatalı sipariş denemesi

            //Gonderim_6(); //DESC: Sadece Eşleştirme Kodu ile Sipariş denemesi WO00000028

            //Gonderim_6_Hatali(); // Uzunluk bilgisi gerekmeyen ürün için uzunluk bilgisi gönderilmesi

            SiparisSorgula("WO00000027"); //DESC: Siparişin sorgulanması

            //GetAddresses(); //Adreslerin alınması

            //GetProducts(); //Ürünlerin alınması

            Console.ReadLine();

        }


        static void SiparisSorgula(string orderno)
        {
            try
            {
                YkkWebApi api = new YkkWebApi();
                //Sipariş sorgulama
                Console.WriteLine("> Sipariş Sorgulama işlemi başlıyor.... Web Order No : {0}", orderno);
                List<string> orderNos = new List<string>();
                orderNos.Add(orderno);

                List<APIOrderDetailResult> orderDetail = api.GetOrder(orderNos, null, null, null, null);

                Console.WriteLine("> Sipariş Sorgulama Web Order No : {0}, Sipariş Durumu :", orderno, orderDetail[0].OrderStatus);
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

        #region Test Senaryoları

        /// <summary>
        /// Eşleşme yapılmış ürün için customer kod ile gönderilmesi (lenght var, color var)
        /// </summary>
        static void Gonderim_1()
        {
            YkkWebApi api = new YkkWebApi();

            List<APIOrderItems> itemList = new List<APIOrderItems>();

            APIOrderItems item1 = api.CreateApiOrderItem("0089294", "1040002733", "580", "7", 100);
            APIOrderItems item2 = api.CreateApiOrderItem("0089294", "1040002734", "580", "8", 110);
            APIOrderItems item3 = api.CreateApiOrderItem("0089294", "1040002735", "580", "9", 50);
            APIOrderItems item4 = api.CreateApiOrderItem("0852495", "1040003367", "196", "6", 51);
            var deliveryCode1 = "003497";
            var deliveryCode2 = "001";
            var buyercode = "";


            itemList.Add(item1);
            itemList.Add(item2);
            itemList.Add(item3);
            itemList.Add(item4);


            string sasNo = "ORN00" + DateTime.Now.Year.ToString() + Convert.ToInt32(DateTime.Now.TimeOfDay.TotalMinutes);

            string siparisNo = "";
            try
            {
                //Sipariş Gönderme

                siparisNo = api.SendOrder(sasNo, DateTime.Now.AddDays(3), APIOrderTimeConvention._2, false,
                    buyercode, true, deliveryCode1, deliveryCode2,
                        $"Örnek sipariş açıklaması {sasNo}", true, itemList);


                Console.WriteLine("> Gönderim Başarılı.");
                Console.WriteLine("> Gönderim Başarılı. Web Order No : {0}", siparisNo);
                //Sipariş sorgulama
                Console.WriteLine("> Sipariş Sorgulama işlemi başlıyor.... Web Order No : {0}", siparisNo);
                List<string> orderNos = new List<string>();
                orderNos.Add(siparisNo);

                List<APIOrderDetailResult> orderDetail = api.GetOrder(orderNos, null, null, null, null);

                Console.WriteLine("> Sipariş Sorgulama Web Order No : {0}, Sipariş Durumu : {1}", siparisNo, orderDetail[0].OrderStatus);

            }
            catch (YkkClientException yex)
            {
                Console.WriteLine("> Status :{0}", yex.StatusCode);
                Console.WriteLine("> Response :{0}", yex.Response);
            }
            catch (Exception ex)
            {

                Console.WriteLine("> Hata : {0}", ex.Message);
            }
        }

        /// <summary>
        ///Müşteri kodu ile  uzunluğu olan +  uzunluğu olmayan item için sipariş açma
        ////// </summary>
        static void Gonderim_2()
        {
            YkkWebApi api = new YkkWebApi();

            List<APIOrderItems> itemList = new List<APIOrderItems>();

            APIOrderItems item1 = api.CreateApiOrderItem("4164156", "", "580", "", 100);
            APIOrderItems item2 = api.CreateApiOrderItem("4449247", "", "580", "12.5", 110);
            var deliveryCode1 = "003497";
            var deliveryCode2 = "001";
            var buyercode = "";


            itemList.Add(item1);
            itemList.Add(item2);


            string sasNo = "ORN00" + DateTime.Now.Year.ToString() + Convert.ToInt32(DateTime.Now.TimeOfDay.TotalMinutes);

            string siparisNo = "";
            try
            {
                //Sipariş Gönderme

                siparisNo = api.SendOrder(sasNo, DateTime.Now.AddDays(3), APIOrderTimeConvention._2, false,
                    buyercode, true, deliveryCode1, deliveryCode2,
                        $"Örnek sipariş açıklaması {sasNo}", true, itemList);


                Console.WriteLine("> Gönderim Başarılı.");
                Console.WriteLine("> Gönderim Başarılı. Web Order No : {0}", siparisNo);
                //Sipariş sorgulama
                Console.WriteLine("> Sipariş Sorgulama işlemi başlıyor.... Web Order No : {0}", siparisNo);
                List<string> orderNos = new List<string>();
                orderNos.Add(siparisNo);

                List<APIOrderDetailResult> orderDetail = api.GetOrder(orderNos, null, null, null, null);

                Console.WriteLine("> Sipariş Sorgulama Web Order No : {0}, Sipariş Durumu : {1}", siparisNo, orderDetail[0].OrderStatus);

            }
            catch (YkkClientException yex)
            {
                Console.WriteLine("> Status :{0}", yex.StatusCode);
                Console.WriteLine("> Response :{0}", yex.Response);
            }
            catch (Exception ex)
            {

                Console.WriteLine("> Hata : {0}", ex.Message);
            }
        }

        /// <summary>
        ///Yanlış Adres kodu ile Sipariş Geçilmesi
        ///Şirketinize ait  bir adres bilgisi bulunamadı hatası
        ////// </summary>
        static void Gonderim_2_Hatali()
        {
            YkkWebApi api = new YkkWebApi();

            List<APIOrderItems> itemList = new List<APIOrderItems>();

            APIOrderItems item1 = api.CreateApiOrderItem("4164156", "", "580", "", 100);
            APIOrderItems item2 = api.CreateApiOrderItem("4449247", "", "580", "12.5", 110);
            var deliveryCode1 = "003497";
            var deliveryCode2 = "999";
            var buyercode = "";


            itemList.Add(item1);
            itemList.Add(item2);


            string sasNo = "ORN00" + DateTime.Now.Year.ToString() + Convert.ToInt32(DateTime.Now.TimeOfDay.TotalMinutes);

            string siparisNo = "";
            try
            {
                //Sipariş Gönderme

                siparisNo = api.SendOrder(sasNo, DateTime.Now.AddDays(3), APIOrderTimeConvention._2, false,
                    buyercode, true, deliveryCode1, deliveryCode2,
                        $"Örnek sipariş açıklaması {sasNo}", true, itemList);


                Console.WriteLine("> Gönderim Başarılı.");
                Console.WriteLine("> Gönderim Başarılı. Web Order No : {0}", siparisNo);
                //Sipariş sorgulama
                Console.WriteLine("> Sipariş Sorgulama işlemi başlıyor.... Web Order No : {0}", siparisNo);
                List<string> orderNos = new List<string>();
                orderNos.Add(siparisNo);

                List<APIOrderDetailResult> orderDetail = api.GetOrder(orderNos, null, null, null, null);

                Console.WriteLine("> Sipariş Sorgulama Web Order No : {0}, Sipariş Durumu : {1}", siparisNo, orderDetail[0].OrderStatus);

            }
            catch (YkkClientException yex)
            {
                Console.WriteLine("> Status :{0}", yex.StatusCode);
                Console.WriteLine("> Response :{0}", yex.Response);
            }
            catch (Exception ex)
            {

                Console.WriteLine("> Hata : {0}", ex.Message);
            }
        }

        /// <summary>
        ///item1 customercodlu lengthsiz + item2 Müşteri kodlu lenghtli 
        ////// </summary>
        static void Gonderim_3()
        {
            YkkWebApi api = new YkkWebApi();

            List<APIOrderItems> itemList = new List<APIOrderItems>();

            APIOrderItems item1 = api.CreateApiOrderItem("4164156", "", "580", "", 100);
            APIOrderItems item2 = api.CreateApiOrderItem("0089294", "1040002733", "580", "7", 110);
            var deliveryCode1 = "003497";
            var deliveryCode2 = "001";
            var buyercode = "";


            itemList.Add(item1);
            itemList.Add(item2);


            string sasNo = "ORN00" + DateTime.Now.Year.ToString() + Convert.ToInt32(DateTime.Now.TimeOfDay.TotalMinutes);

            string siparisNo = "";
            try
            {
                //Sipariş Gönderme

                siparisNo = api.SendOrder(sasNo, DateTime.Now.AddDays(3), APIOrderTimeConvention._2, false,
                    buyercode, true, deliveryCode1, deliveryCode2,
                        $"Örnek sipariş açıklaması {sasNo}", true, itemList);


                Console.WriteLine("> Gönderim Başarılı.");
                Console.WriteLine("> Gönderim Başarılı. Web Order No : {0}", siparisNo);
                //Sipariş sorgulama
                Console.WriteLine("> Sipariş Sorgulama işlemi başlıyor.... Web Order No : {0}", siparisNo);
                List<string> orderNos = new List<string>();
                orderNos.Add(siparisNo);

                List<APIOrderDetailResult> orderDetail = api.GetOrder(orderNos, null, null, null, null);

                Console.WriteLine("> Sipariş Sorgulama Web Order No : {0}, Sipariş Durumu : {1}", siparisNo, orderDetail[0].OrderStatus);

            }
            catch (YkkClientException yex)
            {
                Console.WriteLine("> Status :{0}", yex.StatusCode);
                Console.WriteLine("> Response :{0}", yex.Response);
            }
            catch (Exception ex)
            {

                Console.WriteLine("> Hata : {0}", ex.Message);
            }
        }

        /// <summary>
        ///buyercode ile sipariş gönderme
        ////// </summary>
        static void Gonderim_4()
        {
            YkkWebApi api = new YkkWebApi();

            List<APIOrderItems> itemList = new List<APIOrderItems>();
            APIOrderItems item1 = api.CreateApiOrderItem("0089294", "1040002733", "580", "7", 100);
            APIOrderItems item2 = api.CreateApiOrderItem("0089294", "1040002734", "580", "8", 110);
            APIOrderItems item3 = api.CreateApiOrderItem("0089294", "1040002735", "580", "9", 50);
            APIOrderItems item4 = api.CreateApiOrderItem("0852495", "1040003367", "196", "6", 51);
            var deliveryCode1 = "003497";
            var deliveryCode2 = "001";
            var buyercode = "101281";

            itemList.Add(item1);
            itemList.Add(item2);
            itemList.Add(item3);
            itemList.Add(item4);

            string sasNo = "ORN00" + DateTime.Now.Year.ToString() + Convert.ToInt32(DateTime.Now.TimeOfDay.TotalMinutes);

            string siparisNo = "";
            try
            {
                //Sipariş Gönderme

                siparisNo = api.SendOrder(sasNo, DateTime.Now.AddDays(3), APIOrderTimeConvention._2, false,
                    buyercode, true, deliveryCode1, deliveryCode2,
                        $"Örnek sipariş açıklaması {sasNo}", true, itemList);


                Console.WriteLine("> Gönderim Başarılı.");
                Console.WriteLine("> Gönderim Başarılı. Web Order No : {0}", siparisNo);
                //Sipariş sorgulama
                Console.WriteLine("> Sipariş Sorgulama işlemi başlıyor.... Web Order No : {0}", siparisNo);
                List<string> orderNos = new List<string>();
                orderNos.Add(siparisNo);

                List<APIOrderDetailResult> orderDetail = api.GetOrder(orderNos, null, null, null, null);

                Console.WriteLine("> Sipariş Sorgulama Web Order No : {0}, Sipariş Durumu : {1}", siparisNo, orderDetail[0].OrderStatus);

            }
            catch (YkkClientException yex)
            {
                Console.WriteLine("> Status :{0}", yex.StatusCode);
                Console.WriteLine("> Response :{0}", yex.Response);
            }
            catch (Exception ex)
            {

                Console.WriteLine("> Hata : {0}", ex.Message);
            }
        }

        /// <summary>
        ///Yanlış buyercode ile sipariş gönderme
        ///
        ////// </summary>
        static void Gonderim_4_Hatali()
        {
            YkkWebApi api = new YkkWebApi();

            List<APIOrderItems> itemList = new List<APIOrderItems>();
            APIOrderItems item1 = api.CreateApiOrderItem("0089294", "1040002733", "580", "7", 100);
            APIOrderItems item2 = api.CreateApiOrderItem("0089294", "1040002734", "580", "8", 110);
            APIOrderItems item3 = api.CreateApiOrderItem("0089294", "1040002735", "580", "9", 50);
            APIOrderItems item4 = api.CreateApiOrderItem("0852495", "1040003367", "196", "6", 51);
            var deliveryCode1 = "003497";
            var deliveryCode2 = "001";
            var buyercode = "999999";

            itemList.Add(item1);
            itemList.Add(item2);
            itemList.Add(item3);
            itemList.Add(item4);

            string sasNo = "ORN00" + DateTime.Now.Year.ToString() + Convert.ToInt32(DateTime.Now.TimeOfDay.TotalMinutes);

            string siparisNo = "";
            try
            {
                //Sipariş Gönderme

                siparisNo = api.SendOrder(sasNo, DateTime.Now.AddDays(3), APIOrderTimeConvention._2, false,
                    buyercode, true, deliveryCode1, deliveryCode2,
                        $"Örnek sipariş açıklaması {sasNo}", true, itemList);


                Console.WriteLine("> Gönderim Başarılı.");
                Console.WriteLine("> Gönderim Başarılı. Web Order No : {0}", siparisNo);
                //Sipariş sorgulama
                Console.WriteLine("> Sipariş Sorgulama işlemi başlıyor.... Web Order No : {0}", siparisNo);
                List<string> orderNos = new List<string>();
                orderNos.Add(siparisNo);

                List<APIOrderDetailResult> orderDetail = api.GetOrder(orderNos, null, null, null, null);

                Console.WriteLine("> Sipariş Sorgulama Web Order No : {0}, Sipariş Durumu : {1}", siparisNo, orderDetail[0].OrderStatus);

            }
            catch (YkkClientException yex)
            {
                Console.WriteLine("> Status :{0}", yex.StatusCode);
                Console.WriteLine("> Response :{0}", yex.Response);
            }
            catch (Exception ex)
            {

                Console.WriteLine("> Hata : {0}", ex.Message);
            }
        }

        /// <summary>
        ///Yanlış Uzunluk Bilgileri ile sipariş denemesi
        ////// </summary>
        static void Gonderim_3_Hatali()
        {
            YkkWebApi api = new YkkWebApi();

            List<APIOrderItems> itemList = new List<APIOrderItems>();

            APIOrderItems item1 = api.CreateApiOrderItem("", "1040129592", "", "10", 100); //Olması Gereken 11
            APIOrderItems item2 = api.CreateApiOrderItem("", "1040129409", "", "12", 110);
            var deliveryCode1 = "003497";
            var deliveryCode2 = "001";
            var buyercode = "";


            itemList.Add(item1);
            itemList.Add(item2);


            string sasNo = "ORN00" + DateTime.Now.Year.ToString() + Convert.ToInt32(DateTime.Now.TimeOfDay.TotalMinutes);

            string siparisNo = "";
            try
            {
                //Sipariş Gönderme

                siparisNo = api.SendOrder(sasNo, DateTime.Now.AddDays(3), APIOrderTimeConvention._2, false,
                    buyercode, true, deliveryCode1, deliveryCode2,
                        $"Örnek sipariş açıklaması {sasNo}", true, itemList);


                Console.WriteLine("> Gönderim Başarılı.");
                Console.WriteLine("> Gönderim Başarılı. Web Order No : {0}", siparisNo);
                //Sipariş sorgulama
                Console.WriteLine("> Sipariş Sorgulama işlemi başlıyor.... Web Order No : {0}", siparisNo);
                List<string> orderNos = new List<string>();
                orderNos.Add(siparisNo);

                List<APIOrderDetailResult> orderDetail = api.GetOrder(orderNos, null, null, null, null);

                Console.WriteLine("> Sipariş Sorgulama Web Order No : {0}, Sipariş Durumu : {1}", siparisNo, orderDetail[0].OrderStatus);

            }
            catch (YkkClientException yex)
            {
                Console.WriteLine("> Status :{0}", yex.StatusCode);
                Console.WriteLine("> Response :{0}", yex.Response);
            }
            catch (Exception ex)
            {

                Console.WriteLine("> Hata : {0}", ex.Message);
            }
        }

        /// <summary>
        ///Quantity kodu M ve P olan 2 ürün için sadece ürün kodu ile sipariş denemesi
        ////// </summary>
        static void Gonderim_5()
        {
            YkkWebApi api = new YkkWebApi();

            List<APIOrderItems> itemList = new List<APIOrderItems>();

            APIOrderItems item1 = api.CreateApiOrderItem("0852495", "", "580", "12", 100); //QuantityKodu=P
            APIOrderItems item2 = api.CreateApiOrderItem("0578758", "", "580", "", 110); // QuantityKodu=M
            var deliveryCode1 = "003497";
            var deliveryCode2 = "001";
            var buyercode = "";


            itemList.Add(item1);
            itemList.Add(item2);


            string sasNo = "ORN00" + DateTime.Now.Year.ToString() + Convert.ToInt32(DateTime.Now.TimeOfDay.TotalMinutes);

            string siparisNo = "";
            try
            {
                //Sipariş Gönderme

                siparisNo = api.SendOrder(sasNo, DateTime.Now.AddDays(3), APIOrderTimeConvention._2, false,
                    buyercode, true, deliveryCode1, deliveryCode2,
                        $"Örnek sipariş açıklaması {sasNo}", true, itemList);


                Console.WriteLine("> Gönderim Başarılı.");
                Console.WriteLine("> Gönderim Başarılı. Web Order No : {0}", siparisNo);
                //Sipariş sorgulama
                Console.WriteLine("> Sipariş Sorgulama işlemi başlıyor.... Web Order No : {0}", siparisNo);
                List<string> orderNos = new List<string>();
                orderNos.Add(siparisNo);

                List<APIOrderDetailResult> orderDetail = api.GetOrder(orderNos, null, null, null, null);

                Console.WriteLine("> Sipariş Sorgulama Web Order No : {0}, Sipariş Durumu : {1}", siparisNo, orderDetail[0].OrderStatus);

            }
            catch (YkkClientException yex)
            {
                Console.WriteLine("> Status :{0}", yex.StatusCode);
                Console.WriteLine("> Response :{0}", yex.Response);
            }
            catch (Exception ex)
            {

                Console.WriteLine("> Hata : {0}", ex.Message);
            }
        }

        /// <summary>
        ///Quantity kodu M ve P olan 2 ürün için sadece ürün kodu ile hatalı sipariş denemesi
        ////// </summary>
        static void Gonderim_5_hatali()
        {
            YkkWebApi api = new YkkWebApi();

            List<APIOrderItems> itemList = new List<APIOrderItems>();

            APIOrderItems item1 = api.CreateApiOrderItem("0852495", "", "", "", 100); //QuantityKodu=P
            APIOrderItems item2 = api.CreateApiOrderItem("0578758", "", "", "", 110); // QuantityKodu=M
            var deliveryCode1 = "003497";
            var deliveryCode2 = "001";
            var buyercode = "";


            itemList.Add(item1);
            itemList.Add(item2);


            string sasNo = "ORN00" + DateTime.Now.Year.ToString() + Convert.ToInt32(DateTime.Now.TimeOfDay.TotalMinutes);

            string siparisNo = "";
            try
            {
                //Sipariş Gönderme

                siparisNo = api.SendOrder(sasNo, DateTime.Now.AddDays(3), APIOrderTimeConvention._2, false,
                    buyercode, true, deliveryCode1, deliveryCode2,
                        $"Örnek sipariş açıklaması {sasNo}", true, itemList);


                Console.WriteLine("> Gönderim Başarılı.");
                Console.WriteLine("> Gönderim Başarılı. Web Order No : {0}", siparisNo);
                //Sipariş sorgulama
                Console.WriteLine("> Sipariş Sorgulama işlemi başlıyor.... Web Order No : {0}", siparisNo);
                List<string> orderNos = new List<string>();
                orderNos.Add(siparisNo);

                List<APIOrderDetailResult> orderDetail = api.GetOrder(orderNos, null, null, null, null);

                Console.WriteLine("> Sipariş Sorgulama Web Order No : {0}, Sipariş Durumu : {1}", siparisNo, orderDetail[0].OrderStatus);

            }
            catch (YkkClientException yex)
            {
                Console.WriteLine("> Status :{0}", yex.StatusCode);
                Console.WriteLine("> Response :{0}", yex.Response);
            }
            catch (Exception ex)
            {

                Console.WriteLine("> Hata : {0}", ex.Message);
            }
        }

        /// <summary>
        ///Uzunluk Bilgisi İstenmeyen Ürün için uzunluk bilgisi gönderilmesi
        ////// </summary>
        static void Gonderim_6_Hatali()
        {
            YkkWebApi api = new YkkWebApi();

            List<APIOrderItems> itemList = new List<APIOrderItems>();

            APIOrderItems item1 = api.CreateApiOrderItem("0852495", "", "580", "20.5", 100); //QuantityKodu=P
            APIOrderItems item2 = api.CreateApiOrderItem("0578758", "", "580", "12", 110); // QuantityKodu=M
            var deliveryCode1 = "003497";
            var deliveryCode2 = "001";
            var buyercode = "";


            itemList.Add(item1);
            itemList.Add(item2);


            string sasNo = "ORN00" + DateTime.Now.Year.ToString() + Convert.ToInt32(DateTime.Now.TimeOfDay.TotalMinutes);

            string siparisNo = "";
            try
            {
                //Sipariş Gönderme

                siparisNo = api.SendOrder(sasNo, DateTime.Now.AddDays(3), APIOrderTimeConvention._2, false,
                    buyercode, true, deliveryCode1, deliveryCode2,
                        $"Örnek sipariş açıklaması {sasNo}", true, itemList);


                Console.WriteLine("> Gönderim Başarılı.");
                Console.WriteLine("> Gönderim Başarılı. Web Order No : {0}", siparisNo);
                //Sipariş sorgulama
                Console.WriteLine("> Sipariş Sorgulama işlemi başlıyor.... Web Order No : {0}", siparisNo);
                List<string> orderNos = new List<string>();
                orderNos.Add(siparisNo);

                List<APIOrderDetailResult> orderDetail = api.GetOrder(orderNos, null, null, null, null);

                Console.WriteLine("> Sipariş Sorgulama Web Order No : {0}, Sipariş Durumu : {1}", siparisNo, orderDetail[0].OrderStatus);

            }
            catch (YkkClientException yex)
            {
                Console.WriteLine("> Status :{0}", yex.StatusCode);
                Console.WriteLine("> Response :{0}", yex.Response);
            }
            catch (Exception ex)
            {

                Console.WriteLine("> Hata : {0}", ex.Message);
            }
        }

        /// <summary>
        ///Sadece Eşleştirme Kodu ile Sipariş denemesi
        ////// </summary>
        static void Gonderim_6()
        {
            YkkWebApi api = new YkkWebApi();

            List<APIOrderItems> itemList = new List<APIOrderItems>();

            APIOrderItems item1 = api.CreateApiOrderItem("", "1040126447", "", "", 100);
            APIOrderItems item2 = api.CreateApiOrderItem("", "1040126448", "", "", 110);
            var deliveryCode1 = "003497";
            var deliveryCode2 = "001";
            var buyercode = "";


            itemList.Add(item1);
            itemList.Add(item2);


            string sasNo = "ORN00" + DateTime.Now.Year.ToString() + Convert.ToInt32(DateTime.Now.TimeOfDay.TotalMinutes);

            string siparisNo = "";
            try
            {
                //Sipariş Gönderme

                siparisNo = api.SendOrder(sasNo, DateTime.Now.AddDays(3), APIOrderTimeConvention._2, false,
                    buyercode, true, deliveryCode1, deliveryCode2,
                        $"Örnek sipariş açıklaması {sasNo}", true, itemList);


                Console.WriteLine("> Gönderim Başarılı.");
                Console.WriteLine("> Gönderim Başarılı. Web Order No : {0}", siparisNo);
                //Sipariş sorgulama
                Console.WriteLine("> Sipariş Sorgulama işlemi başlıyor.... Web Order No : {0}", siparisNo);
                List<string> orderNos = new List<string>();
                orderNos.Add(siparisNo);

                List<APIOrderDetailResult> orderDetail = api.GetOrder(orderNos, null, null, null, null);

                Console.WriteLine("> Sipariş Sorgulama Web Order No : {0}, Sipariş Durumu : {1}", siparisNo, orderDetail[0].OrderStatus);

            }
            catch (YkkClientException yex)
            {
                Console.WriteLine("> Status :{0}", yex.StatusCode);
                Console.WriteLine("> Response :{0}", yex.Response);
            }
            catch (Exception ex)
            {

                Console.WriteLine("> Hata : {0}", ex.Message);
            }
        }

        #endregion

        #region Ürün ve Adres Bilgileri
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
        #endregion
    }
}
