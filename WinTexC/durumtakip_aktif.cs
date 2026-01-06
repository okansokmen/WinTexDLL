using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using System.IO;
using System.Collections; //  
using System.Data.SqlClient;


using Excel = Microsoft.Office.Interop.Excel;
using System.Globalization;


using System.Net; // 
using System.Net.Mail;// 
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

using System.Linq.Expressions;
using System.Reflection;
using DevExpress.XtraEditors.Controls;
using DevExpress.Utils;
using System.Data.Linq.SqlClient;
using DevExpress.XtraGrid.Views.Grid;

namespace WinTexC
{
    public partial class durumtakip_aktif : Form
    {
        public static SqlDbConnect con;
        User_Bilgileri Users = new User_Bilgileri();
        public static Order_Bilgileri Order;
        public static Gidercekmail_Bilgileri cMail;
        List<Gidercekmail_Bilgileri> cMaillist = new List<Gidercekmail_Bilgileri>();

        List<Order_Bilgileri> NewOrder = new List<Order_Bilgileri>();
        public static ArrayList yetki_dizi = new ArrayList();
      

        public static ArrayList Musteri_Dizi = new ArrayList();
        public static ArrayList mmno_Dizi = new ArrayList();
        public static ArrayList modeladi_Dizi = new ArrayList();
        public static ArrayList mtem_Dizi = new ArrayList();
        public static ArrayList order_Dizi = new ArrayList();
        public static ArrayList uretimyerleri_Dizi = new ArrayList();
        public static ArrayList secilimail_dizi = new ArrayList();
        ArrayList grupnames_dizi = new ArrayList();
        public static ArrayList tarihler_Dizi = new ArrayList();
        ArrayList departman_dizi = new ArrayList();
        public static ArrayList mesaj_dizi = new ArrayList();
        int Sayi, Sayi2 = 0;
        string dateString, format;
        DateTime result;
        DateTime result2;
        DateTime result3;
        CultureInfo provider = CultureInfo.InvariantCulture;
        int sira = 0;
        public static ArrayList mail_dizi = new ArrayList();
        string eski_mesaj = "";
        string tumytarihler = "";
        int msj_id = 0;
        string a = "";

        bool flag = false;
        
        public class veriler
        {
            public string oktipi;
            public string Renk;
            public string plgonderitarihi;
            public string OKtar2;
            public string pltarihi;
            public string OKTar;
            public string notlar;
            public string sorumlu;
            public string teminsuresi;
            public string ok;
            public string ulineno;
            public string sirano;
            public string uyari;
        }

        public static veriler ss = new veriler();

        public static veriler CPOrder;
        List<veriler> CPOrderlist = new List<veriler>();
        double renkgon, renkok = 0;
        
        public durumtakip_aktif()
        {
            InitializeComponent();
        }

        private void durumtakip_aktif_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            Lst.Columns.Add("Müşteri ana", 150, HorizontalAlignment.Left);
            Lst.Columns.Add("Müşteri Adı", 150, HorizontalAlignment.Left);
            Lst.Columns.Add("Ülke", 150, HorizontalAlignment.Left);
            Lst.Columns.Add("Pazarlama Sorumlusu", 150, HorizontalAlignment.Left);
            Lst.Columns.Add("Müşteri Temsilcisi", 150, HorizontalAlignment.Left);
            Lst.Columns.Add("Tasarımcı", 150, HorizontalAlignment.Left);
            Lst.Columns.Add("Sip.Ad.Aralığı", 150, HorizontalAlignment.Left);
            Lst.Columns.Add("Aracı Ofis", 150, HorizontalAlignment.Left);
            //   Lst.Columns.Add("Açılış Tarihi", 50, HorizontalAlignment.Left);
            Lst.Columns.Add("Son Güncelleme", 150, HorizontalAlignment.Left);
            Lst.Columns.Add("Güncel Durum", 150, HorizontalAlignment.Left);
            //MessageBox.Show("-->"+cInfo.cUsername+"<--");

            Tar1.Value = DateTime.Now.AddMonths(-1);

            Tar2.Value = DateTime.Now.AddMonths(-8);
            size_ozel_liste();
            EnIlkYuklemeler();
            checkBox2.Checked = true;
            ilkYuklemeler("ilk");
            checkBox2.Checked = false;
          


            if ("ADMIN".Contains(cInfo.cUsername) || cInfo.cUsername.Contains("MELİH") || cInfo.cUsername.Contains("OKAN"))
            { pbtn_personel.Visibility = DevExpress.XtraBars.BarItemVisibility.Always; }
            else { pbtn_personel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never; }

            if ("ADMIN".Contains(cInfo.cUsername) || "TEMS".Contains(cInfo.cDepartman) || cInfo.cUsername.Contains("MELİH") || cInfo.cUsername.Contains("OKAN"))
            { pbtn_sipkapat.Visibility = DevExpress.XtraBars.BarItemVisibility.Always; pbtn_user.Visibility = DevExpress.XtraBars.BarItemVisibility.Always; }
            else { pbtn_sipkapat.Visibility = DevExpress.XtraBars.BarItemVisibility.Never; pbtn_user.Visibility = DevExpress.XtraBars.BarItemVisibility.Never; }
            Cursor.Current = Cursors.Default;

        }

        void EnIlkYuklemeler()
        {
            // Kullanıcılar

            try
            {
                con = new SqlDbConnect();
                departman_dizi.Clear();
                con.LoginQuery("select DISTINCT(aciklama) from  personel with (NOLOCK) where ayrildi <> 'E'");
                LstG.Items.Clear();
                while (con.dbr.Read())
                {
                    if (con.dbr["aciklama"].ToString().Trim() != "")
                    {
                        departman_dizi.Add(con.dbr["aciklama"].ToString().Trim());
                        
                    }

                }
                con.dbr.Close();
                /************************************************************/
                for (int i = 0; i < departman_dizi.Count; i++)
                {


                    for (int j = i + 1; j < departman_dizi.Count; j++)
                    {
                        if (departman_dizi[i].ToString() == departman_dizi[j].ToString())
                        { departman_dizi.Remove(departman_dizi[j]); }
                    }
                }

                foreach (var value in departman_dizi) {
                    LstG.Items.Add(value);

                }



                turetim.Items.Clear();
                turetim.Items.Add("");
                con.LoginQuery("select personel,email from  personel with (NOLOCK)  where aciklama like '%ÜRETİM PLANLAMA%' and ayrildi <> 'E'");
                LstU.Items.Clear();
                while (con.dbr.Read())
                {

                    turetim.Items.Add(con.dbr["personel"].ToString().Trim());

                }
                con.dbr.Close();




                /************************************************************/

                /***************************************************************************************************************************** 
                con.LoginQuery("select personel,email from  personel with (NOLOCK)");
                LstU.Items.Clear();
                while (con.dbr.Read())
                {

                        LstU.Items.Add(con.dbr["personel"] + "      /" + con.dbr["email"]);

                }
                con.dbr.Close();





                /************************************************************************************************************************** 
                // seçili sipariş

                con.LoginQuery("select DISTINCT( kullanicisipno) From siparis  where  (dosyakapandi is null or dosyakapandi ='H' or dosyakapandi ='') ");
                Order_Lst.Items.Clear();
                Order_Lst.Items.Add("");
                while (con.dbr.Read())
                {
                    if (con.dbr[0].ToString().Trim() != "") Order_Lst.Items.Add(con.dbr[0].ToString().Trim());
                }
                con.dbr.Close();

                //  Gnl8_SelectedIndexChanged(null, null); // or (null, null)

                /*************************************************************************************************************************** 
                // Müşteri Temsilcileri

                // O.ACIK9  modelist
                con.LoginQuery("select DISTINCT(personel) from  personel where rol = 'MUSTERI TEMSILCILIGI'");
                // con.LoginQuery("SELECT DISTINCT(O.ACIK9) FROM ORDERH O WHERE O.YUKTAR='" + Convert.ToDateTime("1899-12-30") + "' AND O.ORDERNO LIKE 'K%' ORDER BY O.ACIK9");
                Mtem_Lst.Items.Clear();
                Mtem_Lst.Items.Add("");
                while (con.dbr.Read())
                {
                    if (con.dbr[0] != "") Mtem_Lst.Items.Add(con.dbr[0].ToString().Trim());
                }

                con.dbr.Close();



                 */

                con.Close();
            }
            catch
            { 

                LogMessageToFile("İlk yükleme sorunu.--->"+DateTime.Now.ToString() );
                // MessageBox.Show("Mailiniz kuyruğa kayıt edilemedi, mailler gönderilemedi. Yazılım danışmanına bildiriniz", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private static readonly string LOG_FILENAME = Path.GetTempPath() + "AldersLog.txt";

        public static void LogMessageToFile(string msg)

        {   msg = string.Format("{0:G}: {1}rn ------->", DateTime.Now + "/" + cInfo.cUsername, msg);
            File.AppendAllText(LOG_FILENAME, msg);
        }
            
        static string tarihlerarasindami(string orderno,string tarih, DateTime tar1, DateTime tar2) {
                        
            string format;
            CultureInfo provider = CultureInfo.InvariantCulture;

            format = "dd.MM.yyyy";
            List<string> tarihler = tarih.Split(',').ToList<string>();

            //MessageBox.Show(tarih);
            byte varmi = 0;

            foreach (string value in tarihler) 
            {
                if (value.Trim()!="") {
                // MessageBox.Show("tfunc---->"+DateTime.ParseExact(value, format, provider).ToString()+ "///////"+ tar1.ToString());
                if (DateTime.ParseExact(value, format, provider) >= tar1 && DateTime.ParseExact(value, format, provider) <= tar2)
                    { varmi++; }
                }
            }

            /*  
               where DateTime.ParseExact(p.YuklemeTarihi, format, provider) >= DateTime.ParseExact(String.Format("{0:dd.MM.yyyy}", Tar1.Value), format, provider) && 
               DateTime.ParseExact(p.YuklemeTarihi, format, provider) <= DateTime.ParseExact(String.Format("{0:dd.MM.yyyy}", Tar2.Value), format, provider)
            
            */

         //   MessageBox.Show(varmi.ToString());
            // retrun "0";

            if (varmi != 0) { return orderno; } else { return "0"; }
            
        }


        public static Bitmap GetImageByName(string imageName)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            string resourceName = asm.GetName().Name + ".Properties.Resources";
            var rm = new System.Resources.ResourceManager(resourceName, asm);
            return (Bitmap)rm.GetObject(imageName);

        }
        private void ilkYuklemeler(string yukleme)
        {
            Lst.Items.Clear();
            ımageListBoxControl1.Items.Clear();


            if (yukleme == "ilk") {
            NewOrder.Clear();
            Musteri_Dizi.Clear();
            mmno_Dizi.Clear();
            modeladi_Dizi.Clear();
            mtem_Dizi.Clear();
            order_Dizi.Clear();
            uretimyerleri_Dizi.Clear();
            }
            //subitem eklemek için itemin listede bulunduğu sırayı tutar. 
            
            Lst.GridLines = true;
            //  Lst.Bounds = new Rectangle(new Point(10, 10), new Size(300, 200));
            //  Lst.BackColor = Color.SkyBlue;
            Lst.FullRowSelect = true;

            //   Lst.Activation = ItemActivation.Standard;
            //    Lst.View = View.Details;             //add item to listView1  
            
            ImageList imgs = new ImageList();
            imgs.ImageSize = new Size(130, 160);

            //subitem eklemek için itemin listede bulunduğu sırayı tutar. 
            
            string Metin = "";
            
            //Label1.Text = "LT=0";
            // Label1.Visible = false;
            // con.LoginQuery("SELECT TOP 50 * from ORDERH witdh (NOLOCK) ORDER BY ISTAR desc");
            /*  if (!CheckBox1.Checked) { Metin = "AND O.ISTAR>='" + String.Format("{0:yyyy-MM-dd}", Tar1.Value) + "' AND O.ISTAR<='" + String.Format("{0:yyyy-MM-dd}", Tar2.Value) + "' "; }
                if (Gnl9.Text != "") {Metin = Metin + "AND O.OOZEL1='" + Gnl9.Text + "' ";}
                if (Gnl10.Text != "") { Metin = Metin + "AND O.OOZEL2='" + Gnl10.Text + "' ";}
                if (Gnl8.Text != "") { Metin = "AND O.ORDERNO='" + Gnl8.Text + "' "; }
             */
             
            // NOT Like '%CSE%'
            /*   if (GnlSirala.Text == "İstanbul Siparişleri") { Siralama = "ORDER BY O.ISTAR"; }
               else if (GnlSirala.Text == "Urfa Siparişleri") { Siralama = "ORDER BY O.ISTAR"; }
               else if (GnlSirala.Text == "Tesise Göre") { Siralama = "ORDER BY O.ORDERNO"; }
               else { Siralama = " ORDER BY O.ISTAR"; } */
            if (yukleme == "ilk")
            {
                try {
                con = new SqlDbConnect();
                con.LoginQuery("select a.kullanicisipno,yuklemetarihleri=dbo.getsevktarih(a.kullanicisipno),uretimyerleri=dbo.getatolyelersip(a.kullanicisipno),sevkpl=dbo.getsevkplulke(a.kullanicisipno),yuklemeler=dbo.getsevktarihadetulke(a.kullanicisipno),a.musterino, c.videodosyasi,resim =  replace( c.videodosyasi,'modelon','modelon\thumbs'), a.sorumlu,a.siparisgrubu,a.siparistarihi,a.ilksevktarihi,c.aciklama, toplamadet  = SUM(b.adet) From siparis a with (NOLOCK) ,  sipmodel b with (NOLOCK) , ymodel c with (NOLOCK) where a.siparistarihi>'09.09.2018' and  a.kullanicisipno = b.modelno and b.modelno = c.modelno and(a.dosyakapandi is null or a.dosyakapandi = 'H' or a.dosyakapandi = '') and (a.sipariskapali is null or a.sipariskapali = 'H' or a.sipariskapali = '') group by a.kullanicisipno, c.videodosyasi,a.sorumlu,a.siparisgrubu,a.siparistarihi,a.ilksevktarihi,c.aciklama,a.musterino order by a.ilksevktarihi");
                Sayi2 = 0;
                Sayi = 0;
                while (con.dbr.Read())
                {
                    Order = new Order_Bilgileri();
                    /*************************************************************************** ******************************************************************************************************************************/
                    if (con.dbr["resim"].ToString().Trim() != "" || con.dbr["resim"].ToString().Trim() != null)
                    {
                        //Order.Resim = con.dbr["RESIM"].ToString().Trim();
                        if (File.Exists(con.dbr["resim"].ToString().Trim()))
                        { Order.Resimk = @con.dbr["resim"].ToString().Trim(); }
                        else
                        {
                            Order.Resimk = "";
                            if (File.Exists(con.dbr["videodosyasi"].ToString().Trim()))
                            { Order.Resim = @con.dbr["videodosyasi"].ToString().Trim(); }
                            else
                            {
                                   // var bmp = new Bitmap(Alders.Properties.Resources.ALTEX3);

                                    Order.Resim = @Global_Config.Path + "bossa.jpg";
                                   // Order.Resim ="bossa.jpg";
                                }

                        }

                    }
                    else
                    {

                        if (File.Exists(con.dbr["videodosyasi"].ToString().Trim()))
                        { Order.Resim = @con.dbr["videodosyasi"].ToString().Trim(); }
                        else
                        {
                               // Order.Resim = "bossa.jpg";
                                Order.Resim = @Global_Config.Path + "bossa.jpg";

                            }

                    }
                    Order.sevkpl = con.dbr["sevkpl"].ToString().Trim();
                    Order.Yuklemeler= con.dbr["yuklemeler"].ToString().Trim();
                    Order.MusteriTemsilcisi = con.dbr["sorumlu"].ToString().Trim();
                    Order.SiparisNo = con.dbr["kullanicisipno"].ToString().Trim();
                    Order.MusteriSipNo = "";
                    Order.ModelAdi = con.dbr["aciklama"].ToString().Trim();
                    Order.SiparisTarihi = String.Format("{0:dd.MM.yyyy}", con.dbr["siparistarihi"]).ToString();

                    if (con.dbr["ilksevktarihi"].ToString().Trim() != "")
                    { Order.YuklemeTarihi = String.Format("{0:dd.MM.yyyy}", con.dbr["ilksevktarihi"]).ToString(); }
                    else { Order.YuklemeTarihi = String.Format("{0:dd.MM.yyyy}", DateTime.Now); }
                        //yuklemetarihleri
                    Order.Yuklemetarihleri= con.dbr["yuklemetarihleri"].ToString().Trim();
                    Order.MusteriAdi = con.dbr["musterino"].ToString().Trim();
                    Order.siparisgrubu = con.dbr["siparisgrubu"].ToString().Trim();

                    if (con.dbr["toplamadet"].ToString().Trim() != "")
                    {
                        Order.SiparisAdeti = con.dbr["toplamadet"].ToString().Trim();
                    }
                    else { Order.SiparisAdeti = "0"; }

                    //kalanadet



                    //uretimyerleri
                    Order.Uretim_yerleri = con.dbr["uretimyerleri"].ToString().Trim();


                    uretimyerleri_Dizi.Add(Order.Uretim_yerleri);
                    Musteri_Dizi.Add(Order.MusteriAdi.Trim());

                    mmno_Dizi.Add(Order.MusteriSipNo.Trim());
                    modeladi_Dizi.Add(Order.ModelAdi.Trim());
                    mtem_Dizi.Add(Order.MusteriTemsilcisi.Trim());
                    order_Dizi.Add(Order.SiparisNo.Trim());




                    NewOrder.Add(Order);

                }



                con.dbr.Close();

                con.Close();
                }
                 catch (Exception Ex)
                {
                    LogMessageToFile("Sipariş Listesi Oluşturulurken Hata Oluştu->" + secili_siparis.Text + "/" + cInfo.cUsername + "/" + Environment.NewLine + Ex.ToString());
                    MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }



            ImgLst.Images.Clear();

          //  ımageListBoxControl1.Items.Clear();


            format = "dd.MM.yyyy";


            /***************************/
            // set up the "main query"
            var Kat_List = from p in NewOrder
                           orderby DateTime.ParseExact(p.YuklemeTarihi, format, provider) ascending
                           select p;
            Kat_List.ToList();

            // if str1 is not null, add a where-condition



            if (Uretimyeri_Secimi.Text.Trim() != "")
            {
                Kat_List = from p in Kat_List
                           //where p.Uretim_yerleri like '%"++"%'

                           .Where(oh => oh.Uretim_yerleri.Contains(Uretimyeri_Secimi.Text.Trim()))
                           orderby DateTime.ParseExact(p.YuklemeTarihi, format, provider) ascending
                           select p;

                Kat_List.ToList();
            }

            if (turetim.Text.Trim() != "")
            {
                if (turetim.Text.Trim() == "BULENT ALAGOZ")
                {

                    Kat_List = from p in Kat_List
                               where (p.MusteriAdi == "H&M" || p.MusteriAdi == "OS" || p.MusteriAdi == "WEEKDAY" || p.MusteriAdi == "MONKI" || p.MusteriAdi == "MQ")
                               orderby DateTime.ParseExact(p.YuklemeTarihi, format, provider) ascending
                               select p;

                    Kat_List.ToList();

                }
                else if (turetim.Text.Trim() == "TUBA KAPLAN") {

                    Kat_List = from p in Kat_List
                               where (p.MusteriAdi == "LINDEX" || p.MusteriAdi == "OPUS" || p.MusteriAdi == "LİNDEX" || p.MusteriAdi == "SAINSBURYS" || p.MusteriAdi == "INDISKA" || p.MusteriAdi == "LPP" || p.MusteriAdi == "ZARA" || p.MusteriAdi == "ZARA KIDS" || p.MusteriAdi == "MASSIMO DUTTI")
                               orderby DateTime.ParseExact(p.YuklemeTarihi, format, provider) ascending
                               select p;

                    Kat_List.ToList();

                }
                else if (turetim.Text.Trim() == "UMUT AKSOY")
                {

                    Kat_List = from p in Kat_List
                               where (p.MusteriAdi == "COS" || p.MusteriAdi == "OTHER STORIES" || p.MusteriAdi == "OS" || p.MusteriAdi == "ARKET" )
                               orderby DateTime.ParseExact(p.YuklemeTarihi, format, provider) ascending
                               select p;

                    Kat_List.ToList();

                }


            }



            tarihler_Dizi.Clear();
            if (checkBox2.Checked)

            {
                try { 
                string gelens = "";
                tarihler_Dizi.Clear();
                foreach (var value in Kat_List)
                // foreach (var value in NewOrder)
                {
                    gelens = "";
                    gelens = tarihlerarasindami(value.SiparisNo, value.Yuklemetarihleri, Tar1.Value, Tar2.Value);
                  // MessageBox.Show(tarihlerarasindami(value.SiparisNo, value.Yuklemetarihleri, Tar1.Value, Tar2.Value)+ "/"+ value.SiparisNo+ "/" + value.Yuklemetarihleri);
                    if (gelens != "0" && gelens!="")
                     {
                        tarihler_Dizi.Add(gelens);

                        //MessageBox.Show("gelens-->" + gelens);
                    }

                }

                //MessageBox.Show("dizi içinde -> "+tarihler_Dizi.Count.ToString());

                tumytarihler = "";

                if (tarihler_Dizi.Count > 0) {


                    foreach (var value in tarihler_Dizi)
                    // foreach (var value in NewOrder)
                    {
                        tumytarihler = tumytarihler + ","+value;
                    }
                        //  tumytarihler = string.Join(",", tarihler_Dizi);

                        //  MessageBox.Show("sipnolar-->"+tumytarihler);
                     //   txt_msj.Text = tumytarihler;


                        Kat_List = from p in Kat_List

                            .Where(oh => tumytarihler.ToUpper().Contains(oh.SiparisNo.Trim().ToUpper()))
                          // where SqlMethods.Like(tumytarihler,p.SiparisNo)
                           //where p.SiparisNo.IndexOf(tumytarihler) >= 0


                           orderby DateTime.ParseExact(p.YuklemeTarihi, format, provider) ascending
                           select p;

                Kat_List.ToList();


                //    MessageBox.Show("Kat_List->" + Kat_List.Count().ToString() + " ---->tumytarihler->"+ tumytarihler);
                  
                }


                    /* var Sir_List = from p in Kat_List
                                    orderby DateTime.ParseExact(p.YuklemeTarihi, format, provider) ascending
                                    select p;
                     Sir_List.ToList();*/
                }
                catch { }
            }


            var Sir_List = from p in Kat_List
                            orderby DateTime.ParseExact(p.YuklemeTarihi, format, provider) ascending
                            select p;
             Sir_List.ToList();

            /*  if (GnlSirala.Text == "Sipariş Adetine Göre")
             {
                 Sir_List = from p in Sir_List

                            orderby Convert.ToInt32(p.SiparisAdeti) descending
                            select p;

                 Sir_List.ToList();
             }
             else if (GnlSirala.Text == "Yükleme Tarihine Göre")
             {
                 Sir_List = from p in Sir_List
                            orderby DateTime.ParseExact(p.YuklemeTarihi, format, provider) ascending
                            select p;

                 Sir_List.ToList();
             }
             else if (GnlSirala.Text == "Organik (GOTS) Sertfikalı")
             {
                 Sir_List = from p in Sir_List
                            where p.Organik == "1"
                            orderby DateTime.ParseExact(p.YuklemeTarihi, format, provider) ascending
                            select p;

                 Sir_List.ToList();
             }
             else if (GnlSirala.Text == "EKO-TEKS Sertfikalı")
             {
                 Sir_List = from p in Sir_List
                            where p.Organik == "2"
                            orderby DateTime.ParseExact(p.YuklemeTarihi, format, provider) ascending
                            select p;

                 Sir_List.ToList();
             }
             else if (GnlSirala.Text == "Yükleme Bekleyenler")
             {
                 Sir_List = from p in Sir_List
                            where p.Renklendirme == "4"
                            orderby DateTime.ParseExact(p.YuklemeTarihi, format, provider) ascending
                            select p;

                 Sir_List.ToList();
             }
             else if (GnlSirala.Text == "Planlama Bekleyenler")
             {
                 Sir_List = from p in Sir_List
                            where p.k_planlama == "1"
                            orderby DateTime.ParseExact(p.YuklemeTarihi, format, provider) ascending
                            select p;

                 Sir_List.ToList();
             }
             else if (GnlSirala.Text == "Bekleyen Siparişler")
             {
                 Sir_List = from p in Sir_List
                            where p.k_sac == "1"
                            orderby DateTime.ParseExact(p.YuklemeTarihi, format, provider) ascending
                            select p;

                 Sir_List.ToList();
             }
             else if (GnlSirala.Text == "Teknik Dosyası Yüklenmişler")
             {
                 Sir_List = from p in Sir_List
                            where p.TD == "1"
                            orderby DateTime.ParseExact(p.YuklemeTarihi, format, provider) ascending
                            select p;

                 Sir_List.ToList();
             }
             else if (GnlSirala.Text == "Çin")
             {
                 Sir_List = from p in Sir_List
                            where p.cintesti == "1"
                            orderby DateTime.ParseExact(p.YuklemeTarihi, format, provider) ascending
                            select p;

                 Sir_List.ToList();
             } 

             else
             {
                 Sir_List = from p in Sir_List
                            orderby DateTime.ParseExact(p.YuklemeTarihi, format, provider) ascending
                            select p;
                 Sir_List.ToList();
             }

            */
            if (Musteri_Lst.Text != "")
            {
                Sir_List = from p in Sir_List
                           where p.MusteriAdi.Trim() == Musteri_Lst.Text.Trim()
                           orderby DateTime.ParseExact(p.YuklemeTarihi, format, provider) ascending
                           select p;

                Sir_List.ToList();
            }
            else {
                Sir_List = from p in Sir_List
                      
                           orderby DateTime.ParseExact(p.YuklemeTarihi, format, provider) ascending
                           select p;

                Sir_List.ToList();

            }

              
            

            if (Order_Lst.Text != "")
            {

                Sir_List = from p in Sir_List
                           where p.SiparisNo.Trim() == Order_Lst.Text.Trim()
                           orderby DateTime.ParseExact(p.YuklemeTarihi, format, provider) ascending
                           select p;

                Sir_List.ToList();


            }
            else
            {
                Sir_List = from p in Sir_List

                           orderby DateTime.ParseExact(p.YuklemeTarihi, format, provider) ascending
                           select p;

                Sir_List.ToList();

            }

            if (model_adi.Text != "")
            {
                Sir_List = from p in Sir_List
                           where p.ModelAdi.Trim() == model_adi.Text.Trim()
                           orderby DateTime.ParseExact(p.YuklemeTarihi, format, provider) ascending
                           select p;

                Sir_List.ToList();
            }
            else
            {
                Sir_List = from p in Sir_List

                           orderby DateTime.ParseExact(p.YuklemeTarihi, format, provider) ascending
                           select p;

                Sir_List.ToList();

            }

            if (Mtem_Lst.Text != "")
            {
                Sir_List = from p in Sir_List
                           where p.MusteriTemsilcisi.Trim() == Mtem_Lst.Text.Trim()
                           orderby DateTime.ParseExact(p.YuklemeTarihi, format, provider) ascending
                           select p;

                Sir_List.ToList();
            }
            else
            {
                Sir_List = from p in Sir_List

                           orderby DateTime.ParseExact(p.YuklemeTarihi, format, provider) ascending
                           select p;

                Sir_List.ToList();

            }


            

            /*         if (GnlSirala.Text == "Urfa Siparişleri")
                     {
                         Lin_List.Where(p => p.SiparisNo.Substring(4, 1) == "U");
                     }
             */


            /******************************
             * where p.SiparisNo.Substring(2, 1) == "2"   ekip2 top
             * where p.SiparisNo.Substring(2, 1) == "1"   ekip1 top
             * where p.SiparisNo.Substring(4, 1) == "U"  urfa
             *  where p.SiparisNo.Substring(4, 1) != "U"   ist siparişleri
             * 
             * where p.MusteriAdi == Musteri_Lst.Text   müşteriye göre
             *  where p.Organik == "1"  organik
             * 
             * orderby DateTime.ParseExact(p.YuklemeTarihi, format, provider) ascending    yükleme tarihine göre
             * 
             *  orderby Convert.ToInt32(p.SiparisAdeti) descending   adete göre
             * 
             * ******/


            Sayi2 = 0;
            Sayi = 0;
            con = new SqlDbConnect();


            


            foreach (var value in Sir_List)
            // foreach (var value in NewOrder)
            {
                Sayi2++;
                ImgLst.ImageSize = new Size(130, 160);

                try
                {
                    if (value.Resimk != "" && value.Resimk != null)
                    {
                        //   if (value.Resimk == "bossa.jpg") { ImgLst.Images.Add(Properties.Resources.bossa); } else { ImgLst.Images.Add(Image.FromFile(value.Resimk));  }
                        ImgLst.Images.Add(Image.FromFile(value.Resimk));
                    }
                    else {

                       // if (value.Resim == "bossa.jpg") { ImgLst.Images.Add(Properties.Resources.bossa); } else { ImgLst.Images.Add(Image.FromFile(value.Resim)); }
                           ImgLst.Images.Add(Image.FromFile(value.Resim)); 
                    }
                }
                catch {

                    LogMessageToFile("Liste yapılırken resim hatası > " + value.SiparisNo);
                }
                sira = 0;
                // sira = Lst.Items.Count;
                sira = ımageListBoxControl1.Items.Count;
                // MessageBox.Show(sira.ToString()+"<---->" +value.SiparisNo.Trim());


                /*
                ListViewItem lvitem = new ListViewItem("Sipariş Kodu  : " + value.SiparisNo.Trim());
                lvitem.SubItems.Add("Model Adı     : " + value.ModelAdi);
                lvitem.SubItems.Add("Sipariş Tarihi: " + String.Format("{0:dd.MM.yyyy}", value.SiparisTarihi));
                lvitem.SubItems.Add("Yükleme Tarihi: " + String.Format("{0:dd.MM.yyyy}", value.YuklemeTarihi));
                lvitem.SubItems.Add("Müşteri Adı   : " + value.MusteriAdi);
                lvitem.SubItems.Add("Sipariş Adeti : " + value.SiparisAdeti);
                lvitem.SubItems.Add("Grubu/Atölye  : " + value.siparisgrubu + " / "+value.Uretim_yerleri);
                lvitem.SubItems.Add("M.Temsilci    : " + value.MusteriTemsilcisi);
                lvitem.SubItems.Add("Planlanan Sevk: " + value.sevkpl);
                lvitem.SubItems.Add("Yüklenmiş     : " +value.Yuklemeler);
                  */

                Sayi = Sayi + Convert.ToInt32(value.SiparisAdeti);
               // lvitem.ImageIndex = sira;
                //Lst.Items.Add(lvitem);

                a = "";
                a = a + " Sipariş Kodu    : " + value.SiparisNo.Trim();
                a = a + "\r\n Model Adı       : " + value.ModelAdi;
                //  a = a + "\r\n Sipariş Tarihi  : " + String.Format("{0:dd.MM.yyyy}", value.SiparisTarihi) + "/sira: " + sira.ToString() + "/resim: " + value.Resim;
                a = a + "\r\n Sipariş Tarihi  : " + String.Format("{0:dd.MM.yyyy}", value.SiparisTarihi);
                a = a + "\r\n Yükleme Tarihi  : " + String.Format("{0:dd.MM.yyyy}", value.YuklemeTarihi);
                a = a + "\r\n Müşteri Adı     : " + value.MusteriAdi;
                a = a + "\r\n Sipariş Adeti   : " + value.SiparisAdeti;
                a = a + "\r\n Grubu/Atölye    : " + value.siparisgrubu + " / " + value.Uretim_yerleri;
                a = a + "\r\n M.Temsilci      : " + value.MusteriTemsilcisi;
                //  a = a + "\r\n Planlanan Sevk  : " + value.sevkpl + " <---> " + value.Yuklemetarihleri;
                a = a + "\r\n Planlanan Sevk  : " + value.sevkpl ;
                a = a + "\r\n Yüklenmiş       : " + value.Yuklemeler;




                ImageListBoxItem v1 = new ImageListBoxItem(a, sira);


                ımageListBoxControl1.Items.Add(v1);
                /*
                Lst.Items.Add(new ListViewItem { ImageIndex = sira, Text = "test",
                    SubItems = {
                      "Sipariş Kodu  : " + value.SiparisNo.Trim()
                    , "Model Adı     : " + value.ModelAdi 
                    , "Sipariş Tarihi: " + String.Format("{0:dd.MM.yyyy}", value.SiparisTarihi)
                    , "Yükleme Tarihi: " + String.Format("{0:dd.MM.yyyy}", value.YuklemeTarihi)
                    , "Müşteri Adı   : " + value.MusteriAdi, "Sipariş Adeti : " + value.SiparisAdeti
                    , "Grubu/Atölye  : " + value.siparisgrubu + " / "+value.Uretim_yerleri
                    , "M.Temsilci    : " + value.MusteriTemsilcisi
                    , "Planlanan Sevk: " + value.sevkpl
                    , "Yüklenmiş     : " +value.Yuklemeler

                    } });
                    */

                /* If DtO(12) = CStr(-1).Trim Then lvitem.BackColor = Color.Red;
                On Error Resume Next*/

                /*
                    if (Kontrol_List[index].Renk_Kodu != 0)
                    {
                        if (Kontrol_List[index].Renk_Kodu == 1)
                        {
                            Lst.Items[sira].BackColor = Color.Yellow; Lst.Items[sira].ForeColor = Color.Black;
                        }
                        else if (Kontrol_List[index].Renk_Kodu == 2)
                        {
                           // MessageBox.Show("sira " + sira.ToString() + "--" + Kontrol_List[index].OrderNo + "-----" + Kontrol_List[index].Renk_Kodu);
                            Lst.Items[sira].BackColor = Color.Orange; Lst.Items[sira].ForeColor = Color.Black;
                        }
                        else if (Kontrol_List[index].Renk_Kodu == 3)
                        {
                            Lst.Items[sira].BackColor = Color.Red; Lst.Items[sira].ForeColor = Color.White;
                        }
                        else if (Kontrol_List[index].Renk_Kodu == 4)
                        {
                            Lst.Items[sira].BackColor = Color.Green; Lst.Items[sira].ForeColor = Color.White;
                        }




                    }
                    else
                    {

                        //MessageBox.Show(Kontrol_List[index].Renk_Kodu.ToString());
                        if (Kontrol_List[index].planlama == 1 || Kontrol_List[index].Renk == 1 || Kontrol_List[index].Kumas == 1 || Kontrol_List[index].imKumas == 1 || Kontrol_List[index].Beden == 1 || Kontrol_List[index].Aksesuar == 1 || Yukleme_Tarihi < DateTime.Now) { Lst.Items[sira].BackColor = Color.Red; Lst.Items[sira].ForeColor = Color.White; }
                    }

                */


                /*
                if (value.Renklendirme != "0")
                {
                    if (value.Renklendirme == "1")
                    {
                        Lst.Items[sira].BackColor = Color.Yellow; Lst.Items[sira].ForeColor = Color.Black;
                    }
                    else if (value.Renklendirme == "2")
                    {
                        // MessageBox.Show("sira " + sira.ToString() + "--" + Kontrol_List[index].OrderNo + "-----" + Kontrol_List[index].Renk_Kodu);
                        Lst.Items[sira].BackColor = Color.Orange; Lst.Items[sira].ForeColor = Color.Black;
                    }
                    else if (value.Renklendirme == "3")
                    {
                        Lst.Items[sira].BackColor = Color.Red; Lst.Items[sira].ForeColor = Color.White;
                    }
                    else if (value.Renklendirme == "4")
                    {
                        Lst.Items[sira].BackColor = Color.Green; Lst.Items[sira].ForeColor = Color.White;
                    }
                }
                else
                {



                    if (value.k_planlama == "1" || value.k_renk == "1" || value.k_kumas == "1" || value.k_ikumas == "1" || value.k_bedenseti == "1" || value.k_aksesuar == "1" || Yukleme_Tarihi < DateTime.Now) { Lst.Items[sira].BackColor = Color.Red; Lst.Items[sira].ForeColor = Color.White; }
                    else { Lst.Items[sira].BackColor = Color.White; Lst.Items[sira].ForeColor = Color.Black; }
                }
                */






            }

            con.Close();

            ımageListBoxControl1.ImageList = ImgLst;


            //  MessageBox.Show("Lst sıfır " + Kontrol_List[0].OrderNo);


            label15.Text = "Sipariş Sayısı: " + Convert.ToString(Sayi2); // sipar sayısı
            label16.Text = "Toplam Adet : " + Convert.ToString(Sayi); // sip adet sayısı
            Sayi2 = 0;
            Sayi = 0;
            //   if (Gnl7.Text == "" || Gnl7.Text == String.Empty || Gnl7.TextLength == 0) { Gnl7.Text = "[-] Listbox  Seç!!!!"; } else {  }

            if (Lst.Items.Count != 0)
            {

                //  Lst.Items[0].Selected = true;
                //  Lst.Items[0].Focused = true;

                //Doldur();
            }

            if (yukleme == "ilk")
            {
                Musteri_Getir();
               // mmno_Getir();
                modeladi_Getir();
                mtem_Getir();
                order_Getir();

                uretimyeri_Getir();
            }
       



            Cursor.Current = Cursors.Default;
        }

        public void kuyrukkayda_al(string tum_mailler,string sipno) {

            try
            {
                eski_mesaj = "";
                Dosya_Hazirla("", sipno);
                con = new SqlDbConnect();

                con.SqlQuery("INSERT INTO durumtakip_mailq (smtp,username,usermail,usermailpass,gidecekler,konu,mesaj,msj_id,gonpanel,ektarih,dosya,siparisno) VALUES (@smtp,@username,@usermail,@usermailpass,@gidecekler,@konu,@mesaj,@msj_id,@gonpanel,@ektarih,@dosya,@siparisno)");
                con.Cmd.Parameters.AddWithValue("@smtp", "smtp.bentas.com");
                con.Cmd.Parameters.AddWithValue("@username", cInfo.cUsername);
                con.Cmd.Parameters.AddWithValue("@usermail", cInfo.cUsermail);
                con.Cmd.Parameters.AddWithValue("@usermailpass", cInfo.cUsermailpass);
                con.Cmd.Parameters.AddWithValue("@gidecekler", tum_mailler);
                con.Cmd.Parameters.AddWithValue("@konu", sipno + " " + cInfo.cUsername + "  (" + cInfo.cDepartman + ")"); //DateTime.Now.ToString("MM-dd-yyyy / HH:mm");
                con.Cmd.Parameters.AddWithValue("@mesaj", eski_mesaj);
                con.Cmd.Parameters.AddWithValue("@msj_id", msj_id);
                con.Cmd.Parameters.AddWithValue("@gonpanel", "durumtakip");
                con.Cmd.Parameters.AddWithValue("@ektarih", DateTime.Now);
                con.Cmd.Parameters.AddWithValue("@dosya", Order.Resim);
                con.Cmd.Parameters.AddWithValue("@siparisno", sipno);
                con.QueryNonEx();
                con.Close();

                Heryerden.Sonuc = 1;
               // MessageBox.Show("Mesajını kayıt edildi, Mailiniz kuyruğa alındı en kısa sürede sizin adınıza gönderilecektir !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                Heryerden.Sonuc = 0;

                LogMessageToFile("Mailiniz kuyruğa kayıt edilemedi, mailler gönderilemedi.");
                // MessageBox.Show("Mailiniz kuyruğa kayıt edilemedi, mailler gönderilemedi. Yazılım danışmanına bildiriniz", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }
        void uretimyeri_Getir()
        {
            //Remove duplicates from array
            /*******************************************************************/
           
            if (!Uretimyeri_Secimi.Items.Contains(""))
            {
                Uretimyeri_Secimi.Items.Add("");
            }


            for (int i = 0; i < uretimyerleri_Dizi.Count; i++)
            {
                if (!Uretimyeri_Secimi.Items.Contains(uretimyerleri_Dizi[i]))
                {
                    Uretimyeri_Secimi.Items.Add(uretimyerleri_Dizi[i]);
                }

                if (!Uretimyeri_Secimi.Items.Contains(uretimyerleri_Dizi[i]))
                {
                    Uretimyeri_Secimi.Items.Add(uretimyerleri_Dizi[i]);
                }
            }
            /*******************************************************************/

            uretimyerleri_Dizi.Clear();


        }
        void Musteri_Getir()
        {  
            //Remove duplicates from array
            /*******************************************************************/

            if (!Musteri_Lst.Items.Contains(""))
            {
                Musteri_Lst.Items.Add("");
            }


            for (int i = 0; i < Musteri_Dizi.Count; i++)
            {
                if (!Musteri_Lst.Items.Contains(Musteri_Dizi[i]))
                {
                    Musteri_Lst.Items.Add(Musteri_Dizi[i]);
                }

                if (!Musteri_Lst.Items.Contains(Musteri_Dizi[i]))
                {
                    Musteri_Lst.Items.Add(Musteri_Dizi[i]);
                }
            }
            /*******************************************************************/


            /*    foreach (string Mus in Musteri_Dizi)
                {
                    Musteri_Lst.Items.Add(Mus);
                }
    */
            Musteri_Dizi.Clear();


        }

        /*
         void mmno_Getir()
        {  


            if (!mmno_Lst.Items.Contains(""))
            {
                mmno_Lst.Items.Add("");
            }


            for (int i = 0; i < mmno_Dizi.Count; i++)
            {
                if (!mmno_Lst.Items.Contains(mmno_Dizi[i]))
                {
                    mmno_Lst.Items.Add(mmno_Dizi[i]);
                }

                if (!mmno_Lst.Items.Contains(mmno_Dizi[i]))
                {
                    mmno_Lst.Items.Add(mmno_Dizi[i]);
                }
            }
          

            mmno_Dizi.Clear();


        }*/
   
        void modeladi_Getir()
        {   //Remove duplicates from array
            /************************************************************/
         if (!model_adi.Properties.Items.Contains(""))
            { model_adi.Properties.Items.Add(""); }
         
            for (int i = 0; i < modeladi_Dizi.Count; i++)
            {
                if (!model_adi.Properties.Items.Contains(modeladi_Dizi[i]))
                {
                    model_adi.Properties.Items.Add(modeladi_Dizi[i]);
                }

                if (!model_adi.Properties.Items.Contains(modeladi_Dizi[i]))
                {
                    model_adi.Properties.Items.Add(modeladi_Dizi[i]);
                }
            }
            /************************************************************/
            modeladi_Dizi.Clear();
        }

        private void LstG_ItemChecking(object sender, DevExpress.XtraEditors.Controls.ItemCheckingEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Gonderi_listesi_ver();
            Cursor.Current = Cursors.Default;

        }

        private void LstG_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Gonderi_listesi_ver();
            Cursor.Current = Cursors.Default;
        }
        private void LstG_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Gonderi_listesi_ver();
            Cursor.Current = Cursors.Default;
        }
        private void LstG_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Gonderi_listesi_ver();
            Cursor.Current = Cursors.Default;
        }
        void Gonderi_listesi_ver() {

            secilimail_dizi.Clear();
            LstU.Items.Clear();

            for (int i = 0; i < LstG.CheckedItems.Count; i++)
            {
                // colorum  =colorum+","+ LstU.CheckedItems[i].ToString() + " ";

                if (LstG.CheckedItems[i].ToString().Trim() != "") { secilimail_dizi.Add(LstG.CheckedItems[i].ToString().Trim()); }

            }


            if (secilimail_dizi.Count > 0)
            {
                con = new SqlDbConnect();

            
                foreach (string part in secilimail_dizi)
                {
                    try
                    {
                        con.LoginQuery("select personel,email from personel with (NOLOCK) where aciklama like '%" + part + "%' and ayrildi <> 'E'");

                        while (con.dbr.Read())
                        {
                            LstU.Items.Add(con.dbr["personel"].ToString().Trim() + "      /" + con.dbr["email"].ToString().Trim());
                        }
                        con.dbr.Close();
                    }
                    catch {

                        MessageBox.Show("Departman kişileri veritabanından çekilemedi", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LogMessageToFile("Departman kişileri veritabanından çekilemedi");
                    }

                }

                con.Close();

             /*   for (int i = 0; i < LstU.Items.Count; i++)
                {
                    LstU.SetItemChecked(i, true);
                }
                */

            }

            secilimail_dizi.Clear();

        }

        private void LstG_MouseClick(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Gonderi_listesi_ver();
            Cursor.Current = Cursors.Default;

        }

        private void LstG_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Gonderi_listesi_ver();
            Cursor.Current = Cursors.Default;
        }

        private void Musteri_Lst_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            ilkYuklemeler("");
            Cursor.Current = Cursors.Default;
        }

        private void Mtem_Lst_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            ilkYuklemeler("");
            Cursor.Current = Cursors.Default;
        }

        private void Order_Lst_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            ilkYuklemeler("");
            Cursor.Current = Cursors.Default;
        }

        private void model_adi_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            ilkYuklemeler("");
            Cursor.Current = Cursors.Default;
        }

        private void labelControl17_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (Application.OpenForms.OfType<durumtakip_mailkisi>().Any())
            {
                Application.OpenForms.OfType<durumtakip_mailkisi>().First().Close();
            }

            durumtakip_mailkisi durumtakip_mailkisi = new durumtakip_mailkisi();

            durumtakip_mailkisi.MdiParent = this.ParentForm;
            durumtakip_mailkisi.Show();
            Cursor.Current = Cursors.Default;
        }

        private void Lst_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        void mtem_Getir()
        {
            //Remove duplicates from array
            /***************************************************************/
            if (!Mtem_Lst.Items.Contains(""))
            { Mtem_Lst.Items.Add(""); }


            for (int i = 0; i < mtem_Dizi.Count; i++)
            {
                if (!Mtem_Lst.Items.Contains(mtem_Dizi[i]))
                {
                    Mtem_Lst.Items.Add(mtem_Dizi[i]);
                }

                if (!Mtem_Lst.Items.Contains(mtem_Dizi[i]))
                {
                    Mtem_Lst.Items.Add(mtem_Dizi[i]);
                }
            }
            /************************************************************/

            mtem_Dizi.Clear();
        }

        void order_Getir()
        {
            //Remove duplicates from array
 
            /**********************************************************
            if (!Order_Lst.Properties.Items.Contains(""))
            { Order_Lst.Properties.Items.Add(""); }**/

            for (int i = 0; i < order_Dizi.Count; i++)
            {
                if (!Order_Lst.Properties.Items.Contains(order_Dizi[i]))
                {
                    Order_Lst.Properties.Items.Add(order_Dizi[i]);
                   
                }


                


              /*  if (!Order_Lst.Properties.Items.Contains(order_Dizi[i]))
                {
                    Order_Lst.Properties.Items.Add(order_Dizi[i]);
                }
                */
            }
            /************************************************************/

            order_Dizi.Clear();


        }

        private void ekleyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Doldur();
            Cursor.Current = Cursors.Default;
        }

        private void btn_kalanzaman_Click(object sender, EventArgs e)
        {

        }

        private void btn_msjgonder_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            mail_dizi.Clear();

            if (secili_siparis.Text.Trim() == "") { MessageBox.Show("Lütfen Değiştirmek İstediğiniz Siparişi Seçin"); return; }
            else if (txt_msj.Text.Trim() == "") { MessageBox.Show("Lütfen Güncellemek için bir açıklama girin !"); return; }
            else if (secili_siparis.Text.Trim() == "") { MessageBox.Show("Lütfen Siparişi Seçin"); return; }
            else
            {
               /* if (Planlamaci_Mail_Ver(HerYerden.Order_Planlama).Trim() == Global_USER.User_email)
                {

                    mail_dizi.Add(Planlamaci_Mail_Ver(HerYerden.Order_Planlama).Trim());

                }*/


                İsaretlenenVerilerMail();
                /************************************************************/
                for (int i = 0; i < mail_dizi.Count; i++)
                {


                    for (int j = i + 1; j < mail_dizi.Count; j++)
                    {
                        if (mail_dizi[i].ToString() == mail_dizi[j].ToString())
                        { mail_dizi.Remove(mail_dizi[j]); }
                    }
                }
                /************************************************************/

               string tum_mailler = String.Join(",", mail_dizi.ToArray());

                string Subject = secili_siparis.Text + " / "+ secili_modeladi.Text +" Hakkında";



                try
                {
                    con = new SqlDbConnect();

                    con.SqlQuery("INSERT INTO durumtakip_ms (orderno,uname,umail,mesaj,tarih,udepartman,bilgi) VALUES (@orderno,@uname,@umail,@mesaj,@tarih,@udepartman,@bilgi)");
                    con.Cmd.Parameters.AddWithValue("@orderno", secili_siparis.Text);
                    con.Cmd.Parameters.AddWithValue("@uname", cInfo.cUsername);
                    con.Cmd.Parameters.AddWithValue("@umail", cInfo.cUsermail);
                    con.Cmd.Parameters.AddWithValue("@mesaj", txt_msj.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@tarih", DateTime.Now);     //DateTime.Now.ToString("MM-dd-yyyy / HH:mm");




                    if (String.IsNullOrEmpty(cInfo.cDepartman))
                    {
                        con.Cmd.Parameters.AddWithValue("@udepartman", DBNull.Value);
                    }
                    else
                    {
                        con.Cmd.Parameters.AddWithValue("@udepartman", cInfo.cDepartman);
                    }


                    if (String.IsNullOrEmpty(tum_mailler))
                    {
                        con.Cmd.Parameters.AddWithValue("@bilgi", DBNull.Value);
                    }
                    else
                    {
                        con.Cmd.Parameters.AddWithValue("@bilgi", tum_mailler);
                    }

                    //con.Cmd.Parameters.Add("@uname", Convert.ToString(Users.User_ad));
                    // con.Cmd.Parameters.Add("@uaciklama", Convert.ToString(Users.User_aciklama));



                    txt_msj.Text = "";




                    con.QueryNonEx();





                    msj_id = 0;
                    con.LoginQuery("SELECT MAX(id) FROM durumtakip_ms");

                    while (con.dbr.Read())
                    {

                        msj_id = (int)con.dbr[0];

                    }
                    con.dbr.Close();
                    //  SELECT job_id FROM jobs WHERE job_id = @@IDENTITY;

                }
                catch {

                    MessageBox.Show("Yazdığınız Mesaj Kayıt EDİLEMEDİ", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LogMessageToFile("Yazdığınız Mesaj Kayıt EDİLEMEDİ" + secili_siparis.Text +"/"+ cInfo.cUsername);
                }

                con.Close();


              

               
 

                Doldur();

                if (mail_dizi.Count > 0)
                {

                    //MessageBox.Show("mail_dizi->"+ mail_dizi.Count.ToString() + "/cInfo.cUsermail->"+ cInfo.cUsermail + "/cUsermailpass " + cInfo.cUsermailpass + " /cUsername" + cInfo.cUsername);

                    try
                    {
                        SendEmail(tum_mailler, Subject,secili_siparis.Text.Trim(),"");
                        MessageBox.Show("Mail ve mesajınız Gönderildi !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        LogMessageToFile("normal mesaj atamadı, kuyruğua alıyor" + secili_siparis.Text + "/" + cInfo.cUsername);

                        try
                        {
                            eski_mesaj = "";
                            Dosya_Hazirla("", secili_siparis.Text.Trim());
                            con = new SqlDbConnect();

                            con.SqlQuery("INSERT INTO durumtakip_mailq (smtp,username,usermail,usermailpass,gidecekler,konu,mesaj,msj_id,gonpanel,ektarih,dosya,siparisno) VALUES (@smtp,@username,@usermail,@usermailpass,@gidecekler,@konu,@mesaj,@msj_id,@gonpanel,@ektarih,@dosya,@siparisno)");
                            con.Cmd.Parameters.AddWithValue("@smtp", "smtp.bentas.com");
                            con.Cmd.Parameters.AddWithValue("@username", cInfo.cUsername);
                            con.Cmd.Parameters.AddWithValue("@usermail", cInfo.cUsermail);
                            con.Cmd.Parameters.AddWithValue("@usermailpass", cInfo.cUsermailpass);
                            con.Cmd.Parameters.AddWithValue("@gidecekler", tum_mailler);
                            con.Cmd.Parameters.AddWithValue("@konu", secili_siparis.Text.Trim() + " " + cInfo.cUsername + "  (" + cInfo.cDepartman + ")"); //DateTime.Now.ToString("MM-dd-yyyy / HH:mm");
                            con.Cmd.Parameters.AddWithValue("@mesaj", eski_mesaj);
                            con.Cmd.Parameters.AddWithValue("@msj_id", msj_id);
                            con.Cmd.Parameters.AddWithValue("@gonpanel", "durumtakip");
                            con.Cmd.Parameters.AddWithValue("@ektarih", DateTime.Now);
                            con.Cmd.Parameters.AddWithValue("@dosya", Order.Resim);
                            con.Cmd.Parameters.AddWithValue("@siparisno", secili_siparis.Text.Trim());
                            con.QueryNonEx();
                            con.Close();


                            MessageBox.Show("Mesajını kayıt edildi, Mailiniz kuyruğa alındı en kısa sürede sizin adınıza gönderilecektir !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            MessageBox.Show("Mailiniz kuyruğa kayıt edilemedi, mailler gönderilemedi. Yazılım danışmanına bildiriniz", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            LogMessageToFile("normal mesaj atamadı, kuyruğuda kayıt yapılmadı" + secili_siparis.Text + "/" + cInfo.cUsername);

                        }

                    }


                    // MessageBox.Show("Mesajınız Gönderildi !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                     


                    /* foreach (int i in LstYetki.CheckedIndices)
                     {
                         LstYetki.SetItemCheckState(i, CheckState.Unchecked);
                     }

                     SendEmail(HerYerden.tum_mailler, Subject);
                     HerYerden.tum_mailler = "";
                     HerYerden.tum_yetkiler = "";
                     */
                }
                mail_dizi.Clear();
                yetki_dizi.Clear();


            }
        }


        
        public void SendEmail(string gidecekler, string subject, string sipno,string dosya)
        {

            Dosya_Hazirla(subject, sipno);

           
            int _port = 587;
            // string _host = "82.151.132.6"; 82.151.132.6
            string _host = "smtp.bentas.com";
            string _clientUserName = cInfo.cUsermail;
            /*string _fromMail = "durumtakip@karahangrup.com.tr";
            string _clientUserPassword = "1karahan2";*/
            string _fromMail = cInfo.cUsermail;

            string _clientUserPassword = cInfo.cUsermailpass;

            bool _enableSsl = false;

            MailMessage mailMessage = new MailMessage();

            mailMessage.To.Add(@gidecekler);
            mailMessage.IsBodyHtml = true;
            
            mailMessage.Body = @eski_mesaj;
            mailMessage.Subject = @subject + " - " + cInfo.cUsername + "  (" + cInfo.cDepartman + ")";
            mailMessage.From = new MailAddress(_fromMail);

             if (Order.Resim.Trim() != null && Order.Resim.Trim() !="")
            { 

            mailMessage.Attachments.Add(new Attachment(Order.Resim));

            }

            if (dosya != "")
            {

                if (File.Exists(@dosya))
                {
                    mailMessage.Attachments.Add(new Attachment(dosya));
                }

            

            }

            using (SmtpClient smtp = new SmtpClient(_host, _port))
            {
                if (cInfo.cUsermailpass.ToString().Trim() == "")
                {
                    //smtp.Credentials = new NetworkCredential("durumtakip@karahangrup.com.tr", "1karahan2");
                }
                else
                { smtp.Credentials = new NetworkCredential(_fromMail, _clientUserPassword); }


                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = _enableSsl;
                // smtp.Send(@mailMessage);

                smtp.Send(@mailMessage);

                smtp.Dispose();
                
            }
             
        }
        public  void Dosya_Hazirla(string subject,string sipno)
        {


            /*
            new ListViewItem("Sipariş Kodu  : " + value.SiparisNo.Trim());
            lvitem.SubItems.Add("Model Adı     : " + value.ModelAdi);
            lvitem.SubItems.Add("Sipariş Tarihi: " + String.Format("{0:dd.MM.yyyy}", value.SiparisTarihi));
            lvitem.SubItems.Add("Yükleme Tarihi: " + String.Format("{0:dd.MM.yyyy}", value.YuklemeTarihi));
            lvitem.SubItems.Add("Müşteri Adı   : " + value.MusteriAdi);
            lvitem.SubItems.Add("Sipariş Adeti : " + value.SiparisAdeti);
            lvitem.SubItems.Add("Sipariş Grubu : " + value.siparisgrubu);
            lvitem.SubItems.Add("M.Temsilci    : " + value.MusteriTemsilcisi);


            secili_siparis.Text= Lst.FocusedItem.SubItems[0].Text.Replace("Sipariş Kodu  : ", "").Trim();
            */

            try
            {
                eski_mesaj = "<html><head>";
                // tekli HerYerden.Mail_Html = HerYerden.Mail_Html + "<style>.CSSTableGenerator {margin:0px;padding:0px;width:100%;box-shadow: 10px 10px 5px #888888;border:1px solid #000000;-moz-border-radius-bottomleft:0px;-webkit-border-bottom-left-radius:0px;border-bottom-left-radius:0px;-moz-border-radius-bottomright:0px;-webkit-border-bottom-right-radius:0px;border-bottom-right-radius:0px;-moz-border-radius-topright:0px;-webkit-border-top-right-radius:0px;border-top-right-radius:0px;-moz-border-radius-topleft:0px;-webkit-border-top-left-radius:0px;border-top-left-radius:0px;}.CSSTableGenerator table{border-collapse: collapse;border-spacing: 0;width:100%;margin:0px;padding:0px;}.CSSTableGenerator tr:last-child td:last-child {-moz-border-radius-bottomright:0px;-webkit-border-bottom-right-radius:0px;border-bottom-right-radius:0px;}.CSSTableGenerator table tr:first-child td:first-child {-moz-border-radius-topleft:0px;-webkit-border-top-left-radius:0px;border-top-left-radius:0px;}.CSSTableGenerator table tr:first-child td:last-child {-moz-border-radius-topright:0px;-webkit-border-top-right-radius:0px;border-top-right-radius:0px;}.CSSTableGenerator tr:last-child td:first-child{-moz-border-radius-bottomleft:0px;-webkit-border-bottom-left-radius:0px;border-bottom-left-radius:0px;}.CSSTableGenerator tr:hover td{}.CSSTableGenerator tr:nth-child(odd){ background-color:#ffffff; }.CSSTableGenerator tr:nth-child(even)    {  background-color:#ffaa56;}.CSSTableGenerator td{vertical-align:middle;border:1px solid #000000;border-width:0px 1px 1px 0px;text-align:left;padding:7px;font-size:12px;font-weight:bold;font-family:Arial;font-weight:normal;color:#000000;text-align:center;}.CSSTableGenerator tr:last-child td{border-width:0px 1px 0px 0px;}.CSSTableGenerator tr td:last-child{border-width:0px 0px 1px 0px;}.CSSTableGenerator tr:last-child td:last-child{border-width:0px 0px 0px 0px;}.CSSTableGenerator2 tr:first-child td:first-child{border-width:0px 0px 1px 0px;}.CSSTableGenerator2 tr:first-child td:last-child{border-width:0px 0px 1px 1px;}.CSSTableGenerator2 {margin:0px;padding:0px;width:100%;box-shadow: 10px 10px 5px #888888;border:1px solid #000000;-moz-border-radius-bottomleft:0px;-webkit-border-bottom-left-radius:0px;border-bottom-left-radius:0px;-moz-border-radius-bottomright:0px;-webkit-border-bottom-right-radius:0px;border-bottom-right-radius:0px;-moz-border-radius-topright:0px;-webkit-border-top-right-radius:0px;border-top-right-radius:0px;-moz-border-radius-topleft:0px;-webkit-border-top-left-radius:0px;border-top-left-radius:0px;}</style>";
                /*ikili*/
                eski_mesaj = eski_mesaj + "<style>.CSSTableGenerator {margin:0px;padding:0px;width:100%;box-shadow: 10px 10px 5px #888888;border:1px solid #000000;-moz-border-radius-bottomleft:0px;-webkit-border-bottom-left-radius:0px;border-bottom-left-radius:0px;-moz-border-radius-bottomright:0px;-webkit-border-bottom-right-radius:0px;border-bottom-right-radius:0px;-moz-border-radius-topright:0px;-webkit-border-top-right-radius:0px;border-top-right-radius:0px;-moz-border-radius-topleft:0px;-webkit-border-top-left-radius:0px;border-top-left-radius:0px;}.CSSTableGenerator table{border-collapse: collapse;border-spacing: 0;width:100%;margin:0px;padding:0px;}.CSSTableGenerator tr:last-child td:last-child {-moz-border-radius-bottomright:0px;-webkit-border-bottom-right-radius:0px;border-bottom-right-radius:0px;}.CSSTableGenerator table tr:first-child td:first-child {-moz-border-radius-topleft:0px;-webkit-border-top-left-radius:0px;border-top-left-radius:0px;}.CSSTableGenerator table tr:first-child td:last-child {-moz-border-radius-topright:0px;-webkit-border-top-right-radius:0px;border-top-right-radius:0px;}.CSSTableGenerator tr:last-child td:first-child{-moz-border-radius-bottomleft:0px;-webkit-border-bottom-left-radius:0px;border-bottom-left-radius:0px;}.CSSTableGenerator tr:hover td{}.CSSTableGenerator tr:nth-child(odd){ background-color:#ffffff; }.CSSTableGenerator tr:nth-child(even)    {  background-color:#ffaa56;}.CSSTableGenerator td{vertical-align:middle;border:1px solid #000000;border-width:0px 1px 1px 0px;text-align:left;padding:7px;font-size:12px;font-weight:bold;font-family:Arial;font-weight:normal;color:#000000;text-align:center;}.CSSTableGenerator tr:last-child td{border-width:0px 1px 0px 0px;}.CSSTableGenerator tr td:last-child{border-width:0px 0px 1px 0px;}.CSSTableGenerator tr:last-child td:last-child{border-width:0px 0px 0px 0px;}.CSSTableGenerator2 tr:first-child td:first-child{border-width:0px 0px 1px 0px;}.CSSTableGenerator2 tr:first-child td:last-child{border-width:0px 0px 1px 1px;}.CSSTableGenerator2 {margin:0px;padding:0px;width:100%;box-shadow: 10px 10px 5px #888888;border:1px solid #000000;-moz-border-radius-bottomleft:0px;-webkit-border-bottom-left-radius:0px;border-bottom-left-radius:0px;-moz-border-radius-bottomright:0px;-webkit-border-bottom-right-radius:0px;border-bottom-right-radius:0px;-moz-border-radius-topright:0px;-webkit-border-top-right-radius:0px;border-top-right-radius:0px;-moz-border-radius-topleft:0px;-webkit-border-top-left-radius:0px;border-top-left-radius:0px;}.CSSTableGenerator2 table{border-collapse: collapse;border-spacing: 0;width:100%;margin:0px;padding:0px;}.CSSTableGenerator2 tr:last-child td:last-child {-moz-border-radius-bottomright:0px;-webkit-border-bottom-right-radius:0px;border-bottom-right-radius:0px;}.CSSTableGenerator2 table tr:first-child td:first-child {-moz-border-radius-topleft:0px;-webkit-border-top-left-radius:0px;border-top-left-radius:0px;}.CSSTableGenerator2 table tr:first-child td:last-child {-moz-border-radius-topright:0px;-webkit-border-top-right-radius:0px;border-top-right-radius:0px;}.CSSTableGenerator2 tr:last-child td:first-child{-moz-border-radius-bottomleft:0px;-webkit-border-bottom-left-radius:0px;border-bottom-left-radius:0px;}.CSSTableGenerator2 tr:hover td{}.CSSTableGenerator2 tr:nth-child(odd){ background-color:#ffffff; }.CSSTableGenerator2 tr:nth-child(even)    {  background-color:#f0f0f0;}.CSSTableGenerator2 td{vertical-align:middle;border:1px solid #000000;border-width:0px 1px 1px 0px;text-align:left;padding:7px;font-size:12px;font-weight:bold;font-family:Arial;font-weight:normal;color:#000000;text-align:left;}.CSSTableGenerator2 tr:last-child td{border-width:0px 1px 0px 0px;}.CSSTableGenerator2 tr td:last-child{border-width:0px 0px 1px 0px;}.CSSTableGenerator2 tr:last-child td:last-child{border-width:0px 0px 0px 0px;}.CSSTableGenerator2 tr:first-child td:first-child{border-width:0px 0px 1px 0px;}.CSSTableGenerator2 tr:first-child td:last-child{border-width:0px 0px 1px 1px;}  </style>";
                eski_mesaj = eski_mesaj + "<meta charset='utf-8'><style>body{ font:Arial; text-align:center; font-weight:bold;} td{padding:0px 5px 0px 5px; text-align:center;}</style></head><body>";
                eski_mesaj = eski_mesaj + "<div class='CSS_Table_Example' style='max-width:400px;'><div class='CSSTableGenerator' style='max-width:400px;' ><div style='background: -webkit-gradient( linear, left top, left bottom, color-stop(0.05, #ff7f00),color-stop(1, #bf5f00) ); padding:10px; text-align:center;font-family: Arial;font-weight: normal; color: #000000; text-align: center;  font:Arial; padding-top:10px; padding-bottom:10px;'>Sipariş Bilgileri</div>";

                con = new SqlDbConnect();

                con.LoginQuery("select a.kullanicisipno,a.musterino, c.videodosyasi,resim =  replace( c.videodosyasi,'modelon','modelon\thumbs'), a.sorumlu,a.siparisgrubu,a.siparistarihi,a.ilksevktarihi,c.aciklama, toplamadet  = SUM(b.adet) From siparis a with (NOLOCK) ,  sipmodel b with (NOLOCK) , ymodel c with (NOLOCK) where  a.kullanicisipno = b.modelno and b.modelno = c.modelno and a.kullanicisipno='" + sipno + "'  group by a.kullanicisipno, c.videodosyasi,a.sorumlu,a.siparisgrubu,a.siparistarihi,a.ilksevktarihi,c.aciklama,a.musterino order by a.ilksevktarihi");
                while (con.dbr.Read())
                {

                    if (con.dbr["resim"].ToString().Trim() != "" || con.dbr["resim"].ToString().Trim() != null)
                    {
                        //Order.Resim = con.dbr["RESIM"].ToString().Trim();
                        if (File.Exists(con.dbr["resim"].ToString().Trim()))
                        { Order.Resimk = @con.dbr["resim"].ToString().Trim(); }
                        else
                        {

                            Order.Resimk = "";

                            if (File.Exists(con.dbr["videodosyasi"].ToString().Trim()))
                            { Order.Resim = @con.dbr["videodosyasi"].ToString().Trim(); }
                            else
                            { Order.Resim = @Global_Config.Path + "bossa.jpg"; }

                        }

                    }
                    else
                    {

                        if (File.Exists(con.dbr["videodosyasi"].ToString().Trim()))
                        { Order.Resim = @con.dbr["videodosyasi"].ToString().Trim(); }
                        else
                        { Order.Resim = @Global_Config.Path + "bossa.jpg"; }

                    }

                    if (Order.Resimk != null && Order.Resimk != "")
                    {
                        Order.Resim = Order.Resimk;

                    }



                    Order.MusteriTemsilcisi = con.dbr["sorumlu"].ToString().Trim();
                    Order.SiparisNo = con.dbr["kullanicisipno"].ToString().Trim();
                    Order.MusteriSipNo = "";
                    Order.ModelAdi = con.dbr["aciklama"].ToString().Trim();
                    Order.SiparisTarihi = String.Format("{0:dd.MM.yyyy}", con.dbr["siparistarihi"]).ToString();

                    if (con.dbr["ilksevktarihi"].ToString().Trim() != "")
                    { Order.YuklemeTarihi = String.Format("{0:dd.MM.yyyy}", con.dbr["ilksevktarihi"]).ToString(); }
                    else { Order.YuklemeTarihi = String.Format("{0:dd.MM.yyyy}", DateTime.Now); }

                    Order.MusteriAdi = con.dbr["musterino"].ToString().Trim();
                    Order.siparisgrubu = con.dbr["siparisgrubu"].ToString().Trim();

                    if (con.dbr["toplamadet"].ToString().Trim() != "")
                    {
                        Order.SiparisAdeti = con.dbr["toplamadet"].ToString().Trim();
                    }
                    else { Order.SiparisAdeti = "0"; }




                    //Gnl14.Text = con.dbr[4].ToString();




                    //HerYerden.Mail_Html =eski_mesaj + "<table style='margin:0px;padding:0px;  width: 400px; background-color:#ffaa56; color:#fff; text-align:center;'><tr><td>Sipariş Bilgileri</td></tr></table>";

                    eski_mesaj = eski_mesaj + "<table style='border:1px solid #000000;margin:0px;padding:0px;   font-weight:bold; max-width:400px;'>";
                    eski_mesaj = eski_mesaj + "<tr><td width='150' style='background-color:#ffaa56;'>Müşteri Adı / Sipariş Kodu / Model Adı / Sipariş Tarihi </td><td style='background-color:#ffaa56; text-align:left; text-align:left;'>" + Order.MusteriAdi + " / " + Order.SiparisNo + " / " + Order.ModelAdi + " / " + Order.SiparisTarihi + "</td></tr>";
                    eski_mesaj = eski_mesaj + "<tr><td >Toplam Sip.Adet / İlk Yük.Tarihi </td><td style='text-align:left; text-align:left;'>" + Convert.ToDouble(Order.SiparisAdeti).ToString("###.###,##").Trim()   + " Adet / " + Order.YuklemeTarihi + "</td></tr>";
                    eski_mesaj = eski_mesaj + "<tr><td style='background-color:#ffaa56;'>Müşteri Temsilcisi /  Sipariş Grubu</td><td style='background-color:#ffaa56; text-align:left; text-align:left;'>" + Order.MusteriTemsilcisi + " / " + Order.siparisgrubu + "</td></tr>";

                    /*
                    eski_mesaj = eski_mesaj + "<tr><td style='background-color:#ffaa56;'>Yükleme Tarihi     </td><td style='background-color:#ffaa56; text-align:left;'>" + Order.YuklemeTarihi + "</td></tr>";
                    eski_mesaj = eski_mesaj + "<tr><td>Müşteri Adı         </td><td style='text-align:left;'>" + Order.MusteriAdi + "</td></tr>";
                    eski_mesaj = eski_mesaj + "<tr><td>Sipariş Adeti      </td><td style='text-align:left;'>" + Order.SiparisAdeti + "</td></tr>";
                    eski_mesaj = eski_mesaj + "<tr><td style='background-color:#ffaa56;'>Sipariş Grubu        </td><td style='background-color:#ffaa56; text-align:left;'>" + Order.siparisgrubu + "</td></tr>";
                    eski_mesaj = eski_mesaj + "<tr><td>Müşteri Temsilcisi </td><td style='text-align:left;'>" + Order.MusteriTemsilcisi + "</td></tr>";
                    */
                    eski_mesaj = eski_mesaj + "</table></div></div>";


                    eski_mesaj = eski_mesaj + "<br>  <div class='CSS_Table_Example2' style='width:%100'><div class='CSSTableGenerator2' style='width:%100'><div style=' background-color:#d8d8d8; padding:10px; text-align:center;font-family: Arial;font-weight: bold; color: #fff; text-align: center; color:#000;'>Durum Takip</div><table style='text-align:left;'>";
                    //    eski_mesaj =eski_mesaj + "    <br>" + Gnl1.Text;

                    //HerYerden.Mail_Html = "<html><head><style type='text/css'>div{color:red;}.blue{color:blue;}</style></head><body><div class='blue'>Ishouldbeblue</div></body></html>";


                }
                con.dbr.Close();

                /******************************************************************************************************************************/
                mesaj_dizi.Clear();
                con.LoginQuery("Select * From durumtakip_ms Where orderno='" + sipno + "'");
                while (con.dbr.Read())
                {
                    mesaj_dizi.Add(String.Format("{0:dd-MM-yy / HH:mm}", con.dbr["tarih"]) + " --> ( " + con.dbr["udepartman"].ToString().Trim() + " ) " + con.dbr["uname"].ToString().Trim() + "  -->  " + con.dbr["mesaj"].ToString());
                }

                con.dbr.Close();
                con.Close();
                mesaj_dizi.Reverse();


                foreach (string value in mesaj_dizi)
                {

                    eski_mesaj = eski_mesaj + "<tr><td>" + value + "</td></tr>";

                }


                // eski_mesaj =eski_mesaj + "<br>********************************************************************************************************************************<br>";


                /*
                    Array.Clear(Ayir, 0, Ayir.Length);
                    Ayir = eski_mesaj.Split('<');
                    Array.Reverse(Ayir);


                    foreach (string value in Ayir)
                    {

                       eski_mesaj =eski_mesaj + "<tr><td> >" + value + "</td></tr>";

                    }

                */




                eski_mesaj = eski_mesaj + "</table></div></div></body></html>";
                // MessageBox.Show(eski_mesaj);
            }
            catch {
                LogMessageToFile("mail dosyası oluşturulamadı/" + cInfo.cUsername);

            }

        }

        public void mail_kontrol() {



            byte varmi = 0;
            con = new SqlDbConnect();

            try
            {
                con.LoginQuery("select * from durumtakip_parametre");
                while (con.dbr.Read())
                {
                    if (con.dbr["mailqkontrol"].ToString().Trim() == "1") { varmi = 1; }
                }
                con.dbr.Close();
            }
            catch { }




            if (varmi == 1 )
            {
                



            cMaillist.Clear();
            try
            {
                con.LoginQuery("select * from  durumtakip_mailq   with (NOLOCK)  where  gdurum is null or gdurum =''");
                while (con.dbr.Read())
                {



                    cMail = new Gidercekmail_Bilgileri();
                    cMail.id = con.dbr["id"].ToString().Trim();
                    cMail.tarih = DateTime.Now.ToString();



                    cMail.username= con.dbr["username"].ToString().Trim();
                    cMail.usermail= con.dbr["usermail"].ToString().Trim();
                    cMail.usermailpass= con.dbr["usermailpass"].ToString().Trim();
        cMail.gidecekler= con.dbr["gidecekler"].ToString().Trim();
        cMail.konu= con.dbr["konu"].ToString().Trim();
        cMail.mesaj= con.dbr["mesaj"].ToString().Trim();
        cMail.gdurum= con.dbr["gdurum"].ToString().Trim();
        cMail.gtarih= con.dbr["gtarih"].ToString().Trim();
        cMail.msj_id= con.dbr["msj_id"].ToString().Trim();
        cMail.gonpanel= con.dbr["gonpanel"].ToString().Trim();
        cMail.ektarih= con.dbr["ektarih"].ToString().Trim();
        cMail.smtp= con.dbr["smtp"].ToString().Trim();

        cMail.dosya= con.dbr["dosya"].ToString().Trim();
        cMail.resimekle= con.dbr["resimekle"].ToString().Trim();
        cMail.siparisno= con.dbr["siparisno"].ToString().Trim();

        cMail.gonderildimi= con.dbr["gonderildimi"].ToString().Trim();
        cMail.sebebi= con.dbr["sebebi"].ToString().Trim();




        cMaillist.Add(cMail);


                }
                con.dbr.Close();
            }
            catch { }
             
            if (cMaillist.Count > 0) {

                foreach (var value in cMaillist)
                // foreach (var value in NewOrder)
                {

                    if (value.usermail == "" || value.usermailpass  == "" || value.smtp  == "") { return; }

                    try
                    {

                        /******************************************/
                        int _port = 587;
                        // string _host = "82.151.132.6"; 82.151.132.6
                        string _host = value.smtp;
                        string _clientUserName = value.usermail;
                        /*string _fromMail = "durumtakip@karahangrup.com.tr";
                        string _clientUserPassword = "1karahan2";*/
                        string _fromMail = value.usermail;

                        string _clientUserPassword = value.usermailpass;

                        bool _enableSsl = false;

                        MailMessage mailMessage = new MailMessage();

                        mailMessage.To.Add(value.gidecekler);
                        mailMessage.IsBodyHtml = true;

                        mailMessage.Body = value.mesaj;
                        mailMessage.Subject = value.konu;
                        mailMessage.From = new MailAddress(_fromMail);


                        if (File.Exists(value.dosya)) { mailMessage.Attachments.Add(new Attachment(value.dosya)); }


                        using (SmtpClient smtp = new SmtpClient(_host, _port))
                        {
                            smtp.Credentials = new NetworkCredential(_fromMail, _clientUserPassword);


                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.EnableSsl = _enableSsl;
                            // smtp.Send(@mailMessage);

                            smtp.Send(@mailMessage);

                            smtp.Dispose();

                        }
                        /********************************************/


                    }
                    catch (Exception Ex)
                      {
                        con.SqlQuery("UPDATE durumtakip_mailq SET gonderildimi='1' , sebebi='" + Ex.ToString() + "' where id='" + value.id + "'");
                        con.QueryNonEx();


                        LogMessageToFile("sıradaki mail atılamadı mailkontrol()/" + Ex.ToString());
                    }








                    try
                    {
                        
                        con.SqlQuery("UPDATE durumtakip_mailq SET gdurum='1' , gtarih='" + DateTime.Parse(value.tarih) + "' where id='" + value.id + "'");
                        con.QueryNonEx();
                    }
                    catch {

                        LogMessageToFile("atılan mailler güncellenemedi->" + cInfo.cUsername);
                    }

                }


                cMaillist.Clear();

                }






            }//if varmi 1

            con.Close();
            
        }






        public void İsaretlenenVerilerMail()
        {
            if (LstU.CheckedItems.Count > 0)
            {
                for (int i = 0; i < LstU.CheckedItems.Count; i++)//Şimdi yapmamız gereken checkedlistboxımızda seçtiğimiz verilerin içinde for döngüsü dönmekolcak bunun içinde checkedlistboxımızın CheckedItems.Count'unu kullanacağız...
                {
                    // SectigimizVeriler += checkedListBox1.CheckedItems[i].ToString() + " ";

                    string[] words = LstU.CheckedItems[i].ToString().Split('/');

                    if (words[1].Trim() != "")
                    {
                        //  mail_dizi.Add(words[1].Trim());


                        if (!mail_dizi.Contains(words[1].Trim()))
                        {
                            mail_dizi.Add(words[1].Trim());
                        }
                    }


                    Array.Clear(words, 0, words.Length);

                }
            }
            if (Lsts.CheckedItems.Count > 0)
            {
                for (int i = 0; i < Lsts.CheckedItems.Count; i++)//Şimdi yapmamız gereken checkedlistboxımızda seçtiğimiz verilerin içinde for döngüsü dönmekolcak bunun içinde checkedlistboxımızın CheckedItems.Count'unu kullanacağız...
                {
                    // SectigimizVeriler += checkedListBox1.CheckedItems[i].ToString() + " ";

                    string[] words = Lsts.CheckedItems[i].ToString().Split('/');

                    if (words[1].Trim() != "")
                    {
                        //  mail_dizi.Add(words[1].Trim());


                        if (!mail_dizi.Contains(words[1].Trim()))
                        {
                            mail_dizi.Add(words[1].Trim());
                        }
                    }

                    Array.Clear(words, 0, words.Length);

                }

            }









            if (Lst_Grup.CheckedItems.Count > 0)
            {
                con = new SqlDbConnect();
                for (int i = 0; i < Lst_Grup.CheckedItems.Count; i++)//Şimdi yapmamız gereken checkedlistboxımızda seçtiğimiz verilerin içinde for döngüsü dönmekolcak bunun içinde checkedlistboxımızın CheckedItems.Count'unu kullanacağız...
                {
                    // SectigimizVeriler += checkedListBox1.CheckedItems[i].ToString() + " ";

                    if (Lst_Grup.CheckedItems[i].ToString() != "")
                    {
                        ////////////////////////////////////////////////////

                        try
                        {
                            con.LoginQuery("select aciklama from dr_mailkisi_grup with (NOLOCK) where ekleyen='" + cInfo.cUsername + "' and grupname='"+ Lst_Grup.CheckedItems[i].ToString() + "'");

                            while (con.dbr.Read())
                            {

                                if (con.dbr["aciklama"].ToString().Trim() != "")
                                {
                                    string[] words = con.dbr["aciklama"].ToString().Trim().Split('/');

                                    if (words[1].Trim() != "")
                                    {
                                        //  mail_dizi.Add(words[1].Trim());


                                        if (!mail_dizi.Contains(words[1].Trim()))
                                        {
                                            mail_dizi.Add(words[1].Trim());
                                        }
                                    }
                                    Array.Clear(words, 0, words.Length);

                                }


                            }
                            con.dbr.Close();
                        }
                        catch
                        {

                            LogMessageToFile("size özel mail listesi çalışmıyor->" + cInfo.cUsername);
                        }





                      ////////////////////////////////////////////////////
                    }
                }
                con.Close();
            }




        }



        

        public int FindMyText(string txtToSearch, int searchStart, int searchEnd)
        {
            // Unselect the previously searched string
            if (searchStart > 0 && searchEnd > 0 && indexOfSearchText >= 0)
            {
                Gnl1.Undo();
            }

            // Set the return value to -1 by default.
            int retVal = -1;

            // A valid starting index should be specified.
            // if indexOfSearchText = -1, the end of search
            if (searchStart >= 0 && indexOfSearchText >= 0)
            {
                // A valid ending index
                if (searchEnd > searchStart || searchEnd == -1)
                {
                    // Find the position of search string in RichTextBox
                    indexOfSearchText = Gnl1.Find(txtToSearch, searchStart, searchEnd, RichTextBoxFinds.None);
                    // Determine whether the text was found in richTextBox1.
                    if (indexOfSearchText != -1)
                    {
                        // Return the index to the specified search text.
                        retVal = indexOfSearchText;
                    }
                }
            }
            return retVal;
        }


        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            start = 0;
            indexOfSearchText = 0;
        }
        int start = 0;
        int indexOfSearchText = 0;
        private void btn_Ara_Click(object sender, EventArgs e)
        {
            int startindex = 0;

            if (txtSearch.Text.Length > 0)
                startindex = FindMyText(txtSearch.Text.Trim(), start, Gnl1.Text.Length);

            // If string was found in the RichTextBox, highlight it
            if (startindex >= 0)
            {
                // Set the highlight color as red
                Gnl1.SelectionColor = Color.Red;
                Gnl1.SelectionFont = new Font(Gnl1.Font.FontFamily, Gnl1.Font.Size, FontStyle.Bold);


                // Find the end index. End Index = number of characters in textbox
                int endindex = txtSearch.Text.Length;

                Gnl1.ScrollToCaret();
                // Highlight the search string
                Gnl1.Select(startindex, endindex);
                // mark the start position after the position of
                // last search string
                start = startindex + endindex;
            }

            else { indexOfSearchText = 0; start = 0; }
        }

        private void Guncelleme_Tick(object sender, EventArgs e)
        {
            if (Heryerden.Gun1 == 1) {

                Doldur();

                size_ozel_liste();
               


                Heryerden.Gun1 = 0;

            }

           
        }


        string kalan_time="";
        private void Lst_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;


            if (Lst.SelectedItems != null && Lst.SelectedItems.Count > 0)
            {

                for (int i = 0; i < Lst.Items.Count; i++)
                {
                    int ii = 1;
                    Lst.Items[i].BackColor = Color.White;
                    Lst.Items[i].SubItems[0].ForeColor = SystemColors.WindowText;
                    Lst.Items[i].SubItems[1].ForeColor = SystemColors.WindowText;
                    Lst.Items[i].SubItems[2].ForeColor = SystemColors.WindowText;
                    Lst.Items[i].SubItems[3].ForeColor = SystemColors.WindowText;
                    Lst.Items[i].SubItems[4].ForeColor = SystemColors.WindowText;
                    //   Lst.Items[i].SubItems[5].ForeColor = SystemColors.WindowText;
                    ii++;
                }


                Lst.FocusedItem.BackColor = SystemColors.MenuHighlight;
                Lst.FocusedItem.SubItems[0].ForeColor = Color.White;
                Lst.FocusedItem.SubItems[1].ForeColor = Color.White;
                Lst.FocusedItem.SubItems[2].ForeColor = Color.White;
                Lst.FocusedItem.SubItems[3].ForeColor = Color.White;
                Lst.FocusedItem.SubItems[4].ForeColor = Color.White;
                // Lst.FocusedItem.SubItems[5].ForeColor = Color.White;

            }
                kalan_time = "";
            //HerYerden.Order_No = Lst.FocusedItem.SubItems[0].Text.Replace("Sipariş Kodu       : ", "").Trim();

            secili_siparis.Text = Lst.FocusedItem.SubItems[0].Text.Replace("Sipariş Kodu  : ", "").Trim();
            secili_mt.Text = Lst.FocusedItem.SubItems[7].Text.Replace("M.Temsilci    : ", "").Trim();
            kalan_time = Lst.FocusedItem.SubItems[3].Text.Replace("Yükleme Tarihi: ", "").Trim();


            Doldur();

            if (kalan_time != "") {  
            kalan_zaman(); }
            Cursor.Current = Cursors.Default;
        }






        void size_ozel_liste() {
            con = new SqlDbConnect();

            Lsts.Items.Clear();

            Lsts.Items.Add("Ferhat Uğurlu      /ferhatugurlu@alders.com.tr");
            /* Lsts.Items.Add("Nurcan      /nurcan@alders.com.tr");
             Lsts.Items.Add("Melih Ayan      /melihayan@alders.com.tr");
             Lsts.Items.Add("Halis      /halis@alders.com.tr");

             Lsts.Items.Add("Can Alagöz      /canalagoz@alders.com.tr");
             Lsts.Items.Add("Handan      /handan@alders.com.tr");
             Lsts.Items.Add("Halis      /halis@alders.com.tr");

             Lsts.Items.Add("ihr fatih      /fatihkocamese@alders.com.tr");
             Lsts.Items.Add("ihr dilek      /dilekkarakus@alders.com.tr");
             Lsts.Items.Add("urt bülent      /bulentalagoz@alders.com.tr");



             Lsts.Items.Add("Artun Düzay      /artunduzay@alders.com.tr");
             Lsts.Items.Add("çağr      /cagriozal@alders.com.tr");
             Lsts.Items.Add("Tuğba      /tuba@alders.com.tr");*/


        
            try
            {
                con.LoginQuery("select aciklama from dr_mailkisi with (NOLOCK) where ekleyen='" + cInfo.cUsername + "' ");

                while (con.dbr.Read())
                {

                    Lsts.Items.Add(con.dbr["aciklama"].ToString().Trim());


                }
                con.dbr.Close();
            }
            catch {

                LogMessageToFile("size özel mail listesi çalışmıyor->" + cInfo.cUsername);
            }

           
            for (int i = 0; i < LstU.CheckedItems.Count; i++)
            {
                LstU.SetItemChecked(i, true);
            }




            Lst_Grup.Items.Clear();
            grupnames_dizi.Clear();
            try
            {
                con.LoginQuery("select grupname from dr_mailkisi_grup with (NOLOCK) where ekleyen='" + cInfo.cUsername + "' ");

                while (con.dbr.Read())
                {

                    if (!grupnames_dizi.Contains(con.dbr["grupname"].ToString().Trim()))
                    {
                        grupnames_dizi.Add(con.dbr["grupname"].ToString().Trim());
                    }

                     


                }
                con.dbr.Close();
            }
            catch
            {

                LogMessageToFile("size özel mail listesi çalışmıyor->" + cInfo.cUsername);
            }


            con.Close();
            foreach (var value in grupnames_dizi)
            {
                Lst_Grup.Items.Add(value);

            }


        }
        void Doldur() {


            con = new SqlDbConnect();
            ekleyen.Items.Clear();
            ekleyen.Items.Add("");

            if (Lst.Focused == false && secili_siparis.Text == String.Empty) { return; }
            try
            {
                con.LoginQuery("Select DISTINCT(uname) From durumtakip_ms Where orderno='" + secili_siparis.Text.Trim() + "'");
                while (con.dbr.Read())
                {
                    ekleyen.Items.Add(con.dbr["uname"].ToString().Trim());
                }
                con.dbr.Close();
            }
            catch {

                LogMessageToFile("siparişe yazanlar ekleyen gelmiyor->" + cInfo.cUsername);
            }







            Gnl1.Clear();


            try
            {
                if (ekleyen.Text.Trim() != "")
            {
                con.LoginQuery("Select * From durumtakip_ms Where orderno='" + secili_siparis.Text.Trim() + "' and uname like '%" + ekleyen.Text.Trim() + "%'");
            }
            else
            {
                con.LoginQuery("Select * From durumtakip_ms Where orderno='" + secili_siparis.Text.Trim() + "'");
            }


            while (con.dbr.Read())
            {
                Gnl1.Text = Gnl1.Text + "\r-->" + String.Format("{0:dd-MM-yyyy / HH:mm}", con.dbr["tarih"]) + " --> ( " + con.dbr["udepartman"].ToString().Trim() + " ) " + con.dbr["uname"].ToString().Trim() + "  -->  " + con.dbr["mesaj"].ToString().Trim();
            }

            con.dbr.Close();
            }
            catch {
                LogMessageToFile("siparişe yazanlar mesajlar gelmiyor->" + cInfo.cUsername);

            }

            con.Close();

            Gnl1.SelectionStart = Gnl1.TextLength;
            Gnl1.ScrollToCaret();



           

            /*
           if (data_uretimbilgileri.Rows.Count > 0) { data_uretimbilgileri.Refresh();  data_uretimbilgileri.Rows.Clear(); data_uretimbilgileri.Refresh(); }
           if (data_aksesuar.Rows.Count > 0) { data_aksesuar.Rows.Clear(); }
           if (data_kumas.Rows.Count > 0) { data_kumas.Rows.Clear(); }
            */
            data_uretimbilgileri.DataSource = null;
            data_aksesuar.DataSource = null;
            data_kumas.DataSource = null;

            data_kesim.DataSource = null;
            data_dikim.DataSource = null;
            data_utupaket.DataSource = null;
            data_uretimisemirleri.DataSource = null;
            data_yukleme.DataSource = null;

            liste_ver();
        }

        DataTable table = new DataTable();
        double gecgonderi, gecok, durum;

        private DataTable BuildDataTable()
        {
            table.Columns.Clear();
            table.Rows.Clear();

            table.Columns.Add("Konu");
            table.Columns.Add("Renk");

            table.Columns.Add("Gönderim Gecikme");
            table.Columns.Add("OK Gecikme");


            table.Columns.Add("Plan.Gönderi Tar.", typeof(DateTime));
            table.Columns.Add("Plan.Ok Tar.", typeof(DateTime));
            table.Columns.Add("Gerçek.Gön.Tar");
            table.Columns.Add("Gerçek.Ok.Tar");
            table.Columns.Add("Notlar");
            table.Columns.Add("Sorumlu");
            table.Columns.Add("Temin");
            table.Columns.Add("Sonuç");
          

            table.Columns.Add("Kayıt No");
            table.Columns.Add("Uyarı");

            foreach (var value in CPOrderlist)
            {
                gecgonderi = 0;
                gecok = 0;
                durum = 0;
                               
                if (value.plgonderitarihi.Trim() != "" && value.pltarihi.Trim() != "")
                {


                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (value.OKtar2.ToString().Trim() == "")
                    {
                        if (DateTime.Parse(DateTime.Parse(value.plgonderitarihi).ToString("dd.MM.yyyy")) < DateTime.Parse(DateTime.Now.ToString("dd.MM.yyyy")))
                        {
                            gecgonderi = GetBusinessDays(DateTime.Parse(DateTime.Now.ToString("dd.MM.yyyy")), DateTime.Parse(DateTime.Parse(value.plgonderitarihi).ToString("dd.MM.yyyy")));

                            durum = 1;
                        }
                        else if (DateTime.Parse(DateTime.Parse(value.plgonderitarihi).ToString("dd.MM.yyyy")) == DateTime.Parse(DateTime.Now.ToString("dd.MM.yyyy")))
                        {
                            //MessageBox.Show(DateTime.Parse(value.plgonderitarihi).ToString("dd.MM.yyyy")+"/"+ DateTime.Now.ToString("dd.MM.yyyy") + "value.OKtar2 boş ve günler eşit");
                            // durum = 1;
                            gecgonderi = 0;


                        }
                        else if (DateTime.Parse(DateTime.Parse(value.plgonderitarihi).ToString("dd.MM.yyyy")) > DateTime.Parse(DateTime.Now.ToString("dd.MM.yyyy")))
                        {
                            //MessageBox.Show(DateTime.Parse(value.plgonderitarihi).ToString("dd.MM.yyyy")+"/"+ DateTime.Now.ToString("dd.MM.yyyy") + "value.OKtar2 boş ve günler eşit");
                            // durum = 1;

                            gecgonderi = GetBusinessDays(DateTime.Parse(DateTime.Now.ToString("dd.MM.yyyy")), DateTime.Parse(DateTime.Parse(value.plgonderitarihi).ToString("dd.MM.yyyy")));

                        }

                    }
                    else
                    {
                        //
                        if (DateTime.Parse(DateTime.Parse(value.plgonderitarihi).ToString("dd.MM.yyyy")) < DateTime.Parse(DateTime.Parse(value.OKtar2).ToString("dd.MM.yyyy")))
                        {
                            gecgonderi = GetBusinessDays(DateTime.Parse(DateTime.Parse(value.OKtar2).ToString("dd.MM.yyyy")), DateTime.Parse(DateTime.Parse(value.plgonderitarihi).ToString("dd.MM.yyyy")));

                        }
                        else if (DateTime.Parse(DateTime.Parse(value.plgonderitarihi).ToString("dd.MM.yyyy")) == DateTime.Parse(DateTime.Parse(value.OKtar2).ToString("dd.MM.yyyy")))
                        {
                            gecgonderi = 0;

                        }
                        else if (DateTime.Parse(DateTime.Parse(value.plgonderitarihi).ToString("dd.MM.yyyy")) > DateTime.Parse(DateTime.Parse(value.OKtar2).ToString("dd.MM.yyyy")))
                        {
                            gecgonderi = GetBusinessDays(DateTime.Parse(DateTime.Parse(value.OKtar2).ToString("dd.MM.yyyy")), DateTime.Parse(DateTime.Parse(value.plgonderitarihi).ToString("dd.MM.yyyy")));


                        }
                        else
                        {
                            gecgonderi = GetBusinessDays(DateTime.Parse(DateTime.Parse(value.OKtar2).ToString("dd.MM.yyyy")), DateTime.Parse(DateTime.Parse(value.plgonderitarihi).ToString("dd.MM.yyyy")));
                        }


                    }

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////



                    if (value.OKTar.ToString().Trim() == "")
                    {
                        if (DateTime.Parse(DateTime.Parse(value.pltarihi).ToString("dd.MM.yyyy")) < DateTime.Parse(DateTime.Now.ToString("dd.MM.yyyy")))//pl tarihi daha gelmemiş, şuan daha büyük
                        {
                            durum = 1;
                            gecok = GetBusinessDays(DateTime.Parse(DateTime.Now.ToString("dd.MM.yyyy")), DateTime.Parse(DateTime.Parse(value.pltarihi).ToString("dd.MM.yyyy")));
                        }
                        else if (DateTime.Parse(DateTime.Parse(value.pltarihi).ToString("dd.MM.yyyy")) > DateTime.Parse(DateTime.Now.ToString("dd.MM.yyyy")))// şuandan büyükse
                        {
                            gecok = GetBusinessDays(DateTime.Parse(DateTime.Now.ToString("dd.MM.yyyy")), DateTime.Parse(DateTime.Parse(value.pltarihi).ToString("dd.MM.yyyy")));

                        }
                        else if (DateTime.Parse(DateTime.Parse(value.pltarihi).ToString("dd.MM.yyyy")) == DateTime.Parse(DateTime.Now.ToString("dd.MM.yyyy")))
                        { durum = 1; gecok = 0; }

                    }
                    else
                    {
                        if (DateTime.Parse(DateTime.Parse(value.pltarihi).ToString("dd.MM.yyyy")) < DateTime.Parse(DateTime.Parse(value.OKTar).ToString("dd.MM.yyyy")))
                        {
                            gecok = GetBusinessDays(DateTime.Parse(DateTime.Parse(value.OKTar).ToString("dd.MM.yyyy")), DateTime.Parse(DateTime.Parse(value.pltarihi).ToString("dd.MM.yyyy")));
                        }

                        else if (DateTime.Parse(DateTime.Parse(value.pltarihi).ToString("dd.MM.yyyy")) > DateTime.Parse(DateTime.Parse(value.OKTar).ToString("dd.MM.yyyy")))
                        {
                            gecok = GetBusinessDays(DateTime.Parse(DateTime.Parse(value.OKTar).ToString("dd.MM.yyyy")), DateTime.Parse(DateTime.Parse(value.pltarihi).ToString("dd.MM.yyyy")));
                        }
                        else if (DateTime.Parse(DateTime.Parse(value.pltarihi).ToString("dd.MM.yyyy")) == DateTime.Parse(DateTime.Parse(value.OKTar).ToString("dd.MM.yyyy")))
                        {
                            gecok = 0;
                        }

                        else
                        {
                            gecok = GetBusinessDays(DateTime.Parse(DateTime.Parse(value.OKTar).ToString("dd.MM.yyyy")), DateTime.Parse(DateTime.Parse(value.pltarihi).ToString("dd.MM.yyyy")));
                        }

                    }

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    if (value.ok != "" && value.ok == "H")
                    {
                        durum = 3;

                    }
                    /*  if(value.OKtar2.ToString().Trim() == "" && value.OKTar.ToString().Trim() == "")
                      { durum = 1; }
                      if (value.OKtar2.ToString().Trim() != "" && value.OKTar.ToString().Trim() != "")
                          { durum = 1; }
                          */
                }

                table.Rows.Add(new Object[] { value.oktipi, value.Renk, Math.Round(gecgonderi, MidpointRounding.ToEven), Math.Round(gecok, MidpointRounding.ToEven), value.plgonderitarihi, value.pltarihi, value.OKtar2, value.OKTar, value.notlar, value.sorumlu, value.teminsuresi, value.ok, value.sirano, value.uyari });

            }

            CPOrderlist.Clear();

            return table;
        }

      
        void liste_ver()
        {
            gridControl1.DataSource = null;


            if (secili_siparis.Text.Trim() != "")
            {


                try
                {

                    con = new SqlDbConnect();
                    /* con.SqlQuery("select oktipi,Renk,plgonderitarihi,OKtar2,pltarihi,OKTar,notlar,sorumlu,teminsuresi,ok,ulineno,sirano,uyari from sipok with (NOLOCK) where siparisno='" + secili_siparis.Text.Trim() + "' ");
                     SqlDataAdapter sqlDataAdap = new SqlDataAdapter(con.Cmd);
                     DataTable dtRecord = new DataTable();
                     sqlDataAdap.Fill(dtRecord);
                     grid_cp.DataSource = dtRecord;*/


                    con.LoginQuery("select oktipi,Renk,plgonderitarihi,OKtar2,pltarihi,OKTar,notlar,sorumlu,teminsuresi,ok,ulineno,sirano,uyari from sipok with (NOLOCK) where siparisno='" + secili_siparis.Text.Trim() + "' ");
                    while (con.dbr.Read())
                    {
                        CPOrder= new veriler();
                        CPOrder.oktipi = con.dbr["oktipi"].ToString().Trim();
                        CPOrder.Renk = con.dbr["Renk"].ToString().Trim();

                        if (con.dbr["plgonderitarihi"].ToString().Trim() != "")
                        {
                            CPOrder.plgonderitarihi = con.dbr["plgonderitarihi"].ToString().Trim();
                        }
                        else { CPOrder.plgonderitarihi = "01.01.1950"; }


                        CPOrder.OKtar2 = con.dbr["OKtar2"].ToString().Trim();
                        if (con.dbr["pltarihi"].ToString().Trim() != "")
                        {
                          
                            CPOrder.pltarihi = con.dbr["pltarihi"].ToString().Trim();
                        }
                        else { CPOrder.pltarihi = "01.01.1950"; }


                        CPOrder.OKTar = con.dbr["OKTar"].ToString().Trim();
                        CPOrder.notlar = con.dbr["notlar"].ToString().Trim();
                        CPOrder.sorumlu = con.dbr["sorumlu"].ToString().Trim();
                        CPOrder.teminsuresi = con.dbr["teminsuresi"].ToString().Trim();
                        CPOrder.ok = con.dbr["ok"].ToString().Trim();
                        CPOrder.uyari = con.dbr["uyari"].ToString().Trim();
                        CPOrder.sirano = con.dbr["sirano"].ToString().Trim();
                        CPOrderlist.Add(CPOrder);
                    }
                    con.dbr.Close();

                    con.Close();
                     
                    gridControl1.DataSource = BuildDataTable();

                     

                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


                 



            }






        }

        private void btn_aciklama_Click(object sender, EventArgs e)
        {
            if (secili_siparis.Text.Trim() != "")
            {
                Cursor.Current = Cursors.WaitCursor;
                Cursor.Current = Cursors.WaitCursor;
                try
                {


                    if (Application.OpenForms.OfType<durumtakip_aciklamalar>().Any())
                    {
                        Application.OpenForms.OfType<durumtakip_aciklamalar>().First().Close();
                    }
                    durumtakip_aciklamalar dras = new durumtakip_aciklamalar();

                    Cursor.Current = Cursors.WaitCursor;

                    mail_dizi.Clear();

                 
                    /* if (Planlamaci_Mail_Ver(HerYerden.Order_Planlama).Trim() == Global_USER.User_email)
                     {

                         mail_dizi.Add(Planlamaci_Mail_Ver(HerYerden.Order_Planlama).Trim());

                     }*/


                    İsaretlenenVerilerMail();

           
                /************************************************************/
                for (int i = 0; i < mail_dizi.Count; i++)
                    {


                        for (int j = i + 1; j < mail_dizi.Count; j++)
                        {
                            if (mail_dizi[i].ToString() == mail_dizi[j].ToString())
                            {
                                mail_dizi.Remove(mail_dizi[j]);
                            }
                        }
                    }
                    /************************************************************/

                   dras.txt_gonmails.Text= String.Join(",", mail_dizi.ToArray());
                   dras.txt_bildirimadet.Text = mail_dizi.Count.ToString();
              



                dras.MdiParent = this.ParentForm;
                dras.secili_siparis.Text = secili_siparis.Text.Trim();
                dras.secili_modeladi.Text = secili_modeladi.Text.Trim();
                dras.Show();
                Cursor.Current = Cursors.Default;





                
                 
                }
                catch (Exception ex)
                {

                }
                Cursor.Current = Cursors.Default;





            }
            else {
                MessageBox.Show("Lütfen bir sipariş seçiniz");

            }

        }

        private void tab_aksesular_Click(object sender, EventArgs e)
        {
      





        }

        private void tbtn_aks_Click(object sender, EventArgs e)
        {


            Cursor.Current = Cursors.WaitCursor;

            if (secili_siparis.Text == String.Empty) { return; }
            con = new SqlDbConnect();

            string cSQL;  // MTF
            cSQL = "select v.*, ";

            cSQL = cSQL +
                   " IhtiyacTutarTL = coalesce(v.ihtiyac,0) * coalesce(v.fiyat,0) * (case when v.Doviz = 'TL' then 1 " +
                                                                                    " when v.Doviz = '' then 0 " +
                                                                                    " else v.TLKur " +
                                                                                    " end), ";
            cSQL = cSQL +
                   " isEmriVerilenTutarTL = coalesce(v.isEmriVerilen,0) * coalesce(v.fiyat,0) * (case when v.Doviz = 'TL' then 1 " +
                                                                                    " when v.Doviz = '' then 0 " +
                                                                                    " else v.TLKur " +
                                                                                    " end), ";
            cSQL = cSQL +
                   " KarsilananTutarTL = coalesce(v.Karsilanan,0) * coalesce(v.fiyat,0) * (case when v.Doviz = 'TL' then 1 " +
                                                                                    " when v.Doviz = '' then 0 " +
                                                                                    " else v.TLKur " +
                                                                                    " end), ";
            cSQL = cSQL +
               " UretimeCikanTutarTL = coalesce(v.UretimeCikan,0) * coalesce(v.fiyat,0) * (case when v.Doviz = 'TL' then 1 " +
                                                                                " when v.Doviz = '' then 0 " +
                                                                                " else v.TLKur " +
                                                                                " end), ";
            cSQL = cSQL +
               " TedarikeiadeTutarTL = coalesce(v.TedarikeiadeMiktari,0) * coalesce(v.fiyat,0) * (case when v.Doviz = 'TL' then 1 " +
                                                                                " when v.Doviz = '' then 0 " +
                                                                                " else v.TLKur " +
                                                                                " end), ";
            cSQL = cSQL +
               " UretimdeniadeTutarTL = coalesce(v.UretimdeniadeMiktari,0) * coalesce(v.fiyat,0) * (case when v.Doviz = 'TL' then 1 " +
                                                                                " when v.Doviz = '' then 0 " +
                                                                                " else v.TLKur " +
                                                                                " end), ";
            cSQL = cSQL +
               " KalanTutarTL = coalesce(v.KalanMiktar,0) * coalesce(v.fiyat,0) * (case when v.Doviz = 'TL' then 1 " +
                                                                                " when v.Doviz = '' then 0 " +
                                                                                " else v.TLKur " +
                                                                                " end), ";
            cSQL = cSQL +
               " IhtiyacTutarEUR = coalesce(v.ihtiyac,0) * coalesce(v.fiyat,0) * (case when v.Doviz = 'EUR' then 1 " +
                                                                                " when v.Doviz = '' then 0 " +
                                                                                " else v.TLKur / v.EURKur " +
                                                                                " end), ";
            cSQL = cSQL +
               " isEmriVerilenTutarEUR = coalesce(v.isEmriVerilen,0) * coalesce(v.fiyat,0) * (case when v.Doviz = 'EUR' then 1 " +
                                                                                " when v.Doviz = '' then 0 " +
                                                                                " else v.TLKur / v.EURKur " +
                                                                                " end), ";
            cSQL = cSQL +
               " KarsilananTutarEUR = coalesce(v.Karsilanan,0) * coalesce(v.fiyat,0) * (case when v.Doviz = 'EUR' then 1 " +
                                                                                " when v.Doviz = '' then 0 " +
                                                                                " else v.TLKur / v.EURKur " +
                                                                                " end), ";
            cSQL = cSQL +
               " UretimeCikanTutarEUR = coalesce(v.UretimeCikan,0) * coalesce(v.fiyat,0) * (case when v.Doviz = 'EUR' then 1 " +
                                                                                " when v.Doviz = '' then 0 " +
                                                                                " else v.TLKur / v.EURKur " +
                                                                                " end), ";
            cSQL = cSQL +
               " TedarikeiadeTutarEUR = coalesce(v.TedarikeiadeMiktari,0) * coalesce(v.fiyat,0) * (case when v.Doviz = 'EUR' then 1 " +
                                                                                " when v.Doviz = '' then 0 " +
                                                                                " else v.TLKur / v.EURKur " +
                                                                                " end), ";
            cSQL = cSQL +
               " UretimdeniadeTutarEUR = coalesce(v.UretimdeniadeMiktari,0) * coalesce(v.fiyat,0) * (case when v.Doviz = 'EUR' then 1 " +
                                                                                " when v.Doviz = '' then 0 " +
                                                                                " else v.TLKur / v.EURKur " +
                                                                                " end), ";
            cSQL = cSQL +
               " KalanTutarEUR = coalesce(v.KalanMiktar,0) * coalesce(v.fiyat,0) * (case when v.Doviz = 'EUR' then 1 " +
                                                                                " when v.Doviz = '' then 0 " +
                                                                                " else v.TLKur / v.EURKur " +
                                                                                " end) ";
            cSQL = cSQL +
                " from (select w.*, " +
                        " TLKur = dbo.getkur(w.Doviz,w.KurTarihi), " +
                        " EURKur = dbo.getkur('EUR',w.KurTarihi) ";
            cSQL = cSQL +
                        " from (select a.MalzemeTakipNo, b.StokNo, c.CinsAciklamasi, c.AnaStokGrubu, c.StokTipi, c.Birim1, b.Renk, b.Beden,  " +
                             " ihtiyac = sum(coalesce(b.ihtiyac,0)) , " +
                             " isEmriVerilen = sum(coalesce(b.isemriverilen,0)) , " +
                             " Karsilanan = sum(coalesce(b.isemriicingelen,0) + coalesce(b.isemriharicigelen,0)) , " +
                             " UretimeCikan = sum(coalesce(b.uretimicincikis,0) - coalesce(b.uretimdeniade,0)), " +
                             " Kalite = coalesce((select top 1 kalite from stokdokuma with (NOLOCK) where stokno = b.stokno),'BELIRSIZ'), " +
                             " Tedarikci = coalesce(dbo.getmtftedarikci(a.malzemetakipno,b.stokno,b.renk,b.beden),'BELIRSIZ'), " +
                             " IsemriTarihleri = coalesce(dbo.getmtfisemritarihleri(a.malzemetakipno,b.stokno,b.renk,b.beden),'BELIRSIZ'), " +
                             " Terminler = coalesce(dbo.getmtftermin(a.malzemetakipno,b.stokno,b.renk, b.beden),'BELIRSIZ'), ";
            cSQL = cSQL +
                            " TedarikeiadeMiktari = coalesce((select sum(coalesce(y.netmiktar1,0)) " +
                                    " from stokfis x with (NOLOCK), stokfislines y with (NOLOCK) " +
                                    " where x.stokfisno = y.stokfisno " +
                                    " and y.malzemetakipkodu = a.malzemetakipno " +
                                    " and y.stokno = b.stokno " +
                                    " and y.renk = b.renk " +
                                    " and y.beden = b.beden " +
                                    " and y.stokhareketkodu = '02 Tedarikten iade'),0), ";
            cSQL = cSQL +
                            " UretimdeniadeMiktari = coalesce((select sum(coalesce(y.netmiktar1,0)) " +
                                    " from stokfis x with (NOLOCK), stokfislines y with (NOLOCK) " +
                                    " where x.stokfisno = y.stokfisno " +
                                    " and y.malzemetakipkodu = a.malzemetakipno " +
                                    " and y.stokno = b.stokno " +
                                    " and y.renk = b.renk " +
                                    " and y.beden = b.beden " +
                                    " and y.stokhareketkodu = '01 Uretimden iade'),0), ";
            cSQL = cSQL +
                            " KalanMiktar = coalesce((select sum(coalesce(donemgiris1,0) - coalesce(donemcikis1,0)) " +
                                    " from stokrb with (NOLOCK) " +
                                    " where malzemetakipkodu = a.malzemetakipno " +
                                    " and stokno = b.stokno " +
                                    " and renk = b.renk " +
                                    " and beden = b.beden),0), ";
            cSQL = cSQL +
                            " Fiyat = coalesce((select top 1 y.fiyat " +
                                     " from isemri x with (NOLOCK), isemrilines y with (NOLOCK) " +
                                     " where x.isemrino = y.isemrino " +
                                     " and y.malzemetakipno = a.malzemetakipno " +
                                     " and y.stokno = b.stokno " +
                                     " and y.renk = b.renk " +
                                     " and y.beden = b.beden " +
                                     " and y.fiyat is not null " +
                                     " and y.fiyat > 0 " +
                                     " order by x.tarih desc),0), ";
            cSQL = cSQL +
                            " Doviz = coalesce((select top 1 y.doviz " +
                                     " from isemri x with (NOLOCK), isemrilines y with (NOLOCK) " +
                                     " where x.isemrino = y.isemrino " +
                                     " and y.malzemetakipno = a.malzemetakipno " +
                                     " and y.stokno = b.stokno " +
                                     " and y.renk = b.renk " +
                                     " and y.beden = b.beden " +
                                     " and y.fiyat is not null " +
                                     " and y.fiyat > 0 " +
                                     " order by x.tarih desc),''), ";
            cSQL = cSQL +
                            " KurTarihi = coalesce((select top 1 x.tarih " +
                                     " from isemri x with (NOLOCK), isemrilines y with (NOLOCK) " +
                                     " where x.isemrino = y.isemrino " +
                                     " and y.malzemetakipno = a.malzemetakipno " +
                                     " and y.stokno = b.stokno " +
                                     " and y.renk = b.renk " +
                                     " and y.beden = b.beden " +
                                     " and y.fiyat is not null " +
                                     " and y.fiyat > 0 " +
                                     " order by x.tarih desc),convert(date,getdate())), ";
            cSQL = cSQL +
                            " ilkSevkTarihi = (select min(x.ilksevktarihi) " +
                                     " from siparis x with (NOLOCK), sipmodel y with (NOLOCK) " +
                                     " where x.kullanicisipno = y.siparisno " +
                                     " and y.malzemetakipno = a.malzemetakipno), ";
            cSQL = cSQL +
                           " SonSevkTarihi = (select min(x.sonsevktarihi) " +
                                    " from siparis x with (NOLOCK), sipmodel y with (NOLOCK) " +
                                    " where x.kullanicisipno = y.siparisno " +
                                    " and y.malzemetakipno = a.malzemetakipno), ";
            cSQL = cSQL +
                            " Musteri = coalesce((select top 1 x.musterino " +
                                     " from siparis x with (NOLOCK), sipmodel y with (NOLOCK) " +
                                     " where x.kullanicisipno = y.siparisno " +
                                     " and y.malzemetakipno = a.malzemetakipno),'BELIRSIZ'), ";
            cSQL = cSQL +
                            " MusteriSiparisNo = coalesce((select top 1 x.musterisipno " +
                                     " from siparis x with (NOLOCK), sipmodel y with (NOLOCK) " +
                                     " where x.kullanicisipno = y.siparisno " +
                                     " and y.malzemetakipno = a.malzemetakipno),'BELIRSIZ') ";
            cSQL = cSQL +
                            " from mtkfis a with (NOLOCK), mtkfislines b with (NOLOCK), stok c with (NOLOCK) " +
                            " Where a.malzemetakipno = b.malzemetakipno ";
            cSQL = cSQL +
                            " and a.malzemetakipno in (select y.malzemetakipno " +
                                                     " from siparis x with (NOLOCK), sipmodel y with (NOLOCK), ymodel z with (NOLOCK) " +
                                                     " where x.kullanicisipno = y.siparisno " +
                                                     " and y.modelno = z.modelno ) ";
            //   cFilter + ") ";
            cSQL = cSQL +
                            " and b.stokno = c.stokno " +
                            " and b.ihtiyac is not null " +
                            " and b.ihtiyac > 0   and c.anastokgrubu like '%AKSESUAR%' and a.malzemetakipno='"+ secili_siparis.Text.Trim() + "' ";

            /*  Select Case nDetail
                  Case 1    ' MTF ihtiyacindan fazla üretime cikis yapilmislar
                      cSQL = cSQL + 
                                  " and coalesce(b.ihtiyac,0) < coalesce(b.uretimicincikis,0) - coalesce(b.uretimdeniade,0) "
                  Case 2    ' MTf ihtiyacindan fazla stoklara giris yapilmislar
                      cSQL = cSQL + 
                                  " and coalesce(b.ihtiyac,0) < coalesce(b.isemriicingelen,0) + coalesce(b.isemriharicigelen,0) "
                  Case 3    ' Kumaşlar
                      cSQL = cSQL + 
                                  " and c.anastokgrubu like '%KUMAS%' "
                  Case 4    ' Aksesuarlar
                      cSQL = cSQL + 
                                  " and c.anastokgrubu like '%AKSESUAR%' "
              End Select*/

            cSQL = cSQL +
                                " group by a.malzemetakipno, b.stokno, c.cinsaciklamasi, c.anastokgrubu, c.stoktipi, c.birim1, b.renk, b.beden) w) v ";






            try
            {
                con.SqlQuery(cSQL);


                SqlDataAdapter sqlDataAdap = new SqlDataAdapter(con.Cmd);

                DataTable dtRecord = new DataTable();
                sqlDataAdap.Fill(dtRecord);
                data_aksesuar.DataSource = dtRecord;
            }
            catch {
                LogMessageToFile("aksesuardataview da sorun var->"+secili_siparis.Text+"" + cInfo.cUsername);

            }
            con.Close();
            Cursor.Current = Cursors.Default;
        }

        private void tbnt_kumas_Click(object sender, EventArgs e)
        {


            Cursor.Current = Cursors.WaitCursor;
            if (secili_siparis.Text == String.Empty) { return; }

            con = new SqlDbConnect();
            string cSQL;  // MTF
            cSQL = "select v.*, ";

            cSQL = cSQL +
                   " IhtiyacTutarTL = coalesce(v.ihtiyac,0) * coalesce(v.fiyat,0) * (case when v.Doviz = 'TL' then 1 " +
                                                                                    " when v.Doviz = '' then 0 " +
                                                                                    " else v.TLKur " +
                                                                                    " end), ";
            cSQL = cSQL +
                   " isEmriVerilenTutarTL = coalesce(v.isEmriVerilen,0) * coalesce(v.fiyat,0) * (case when v.Doviz = 'TL' then 1 " +
                                                                                    " when v.Doviz = '' then 0 " +
                                                                                    " else v.TLKur " +
                                                                                    " end), ";
            cSQL = cSQL +
                   " KarsilananTutarTL = coalesce(v.Karsilanan,0) * coalesce(v.fiyat,0) * (case when v.Doviz = 'TL' then 1 " +
                                                                                    " when v.Doviz = '' then 0 " +
                                                                                    " else v.TLKur " +
                                                                                    " end), ";
            cSQL = cSQL +
               " UretimeCikanTutarTL = coalesce(v.UretimeCikan,0) * coalesce(v.fiyat,0) * (case when v.Doviz = 'TL' then 1 " +
                                                                                " when v.Doviz = '' then 0 " +
                                                                                " else v.TLKur " +
                                                                                " end), ";
            cSQL = cSQL +
               " TedarikeiadeTutarTL = coalesce(v.TedarikeiadeMiktari,0) * coalesce(v.fiyat,0) * (case when v.Doviz = 'TL' then 1 " +
                                                                                " when v.Doviz = '' then 0 " +
                                                                                " else v.TLKur " +
                                                                                " end), ";
            cSQL = cSQL +
               " UretimdeniadeTutarTL = coalesce(v.UretimdeniadeMiktari,0) * coalesce(v.fiyat,0) * (case when v.Doviz = 'TL' then 1 " +
                                                                                " when v.Doviz = '' then 0 " +
                                                                                " else v.TLKur " +
                                                                                " end), ";
            cSQL = cSQL +
               " KalanTutarTL = coalesce(v.KalanMiktar,0) * coalesce(v.fiyat,0) * (case when v.Doviz = 'TL' then 1 " +
                                                                                " when v.Doviz = '' then 0 " +
                                                                                " else v.TLKur " +
                                                                                " end), ";
            cSQL = cSQL +
               " IhtiyacTutarEUR = coalesce(v.ihtiyac,0) * coalesce(v.fiyat,0) * (case when v.Doviz = 'EUR' then 1 " +
                                                                                " when v.Doviz = '' then 0 " +
                                                                                " else v.TLKur / v.EURKur " +
                                                                                " end), ";
            cSQL = cSQL +
               " isEmriVerilenTutarEUR = coalesce(v.isEmriVerilen,0) * coalesce(v.fiyat,0) * (case when v.Doviz = 'EUR' then 1 " +
                                                                                " when v.Doviz = '' then 0 " +
                                                                                " else v.TLKur / v.EURKur " +
                                                                                " end), ";
            cSQL = cSQL +
               " KarsilananTutarEUR = coalesce(v.Karsilanan,0) * coalesce(v.fiyat,0) * (case when v.Doviz = 'EUR' then 1 " +
                                                                                " when v.Doviz = '' then 0 " +
                                                                                " else v.TLKur / v.EURKur " +
                                                                                " end), ";
            cSQL = cSQL +
               " UretimeCikanTutarEUR = coalesce(v.UretimeCikan,0) * coalesce(v.fiyat,0) * (case when v.Doviz = 'EUR' then 1 " +
                                                                                " when v.Doviz = '' then 0 " +
                                                                                " else v.TLKur / v.EURKur " +
                                                                                " end), ";
            cSQL = cSQL +
               " TedarikeiadeTutarEUR = coalesce(v.TedarikeiadeMiktari,0) * coalesce(v.fiyat,0) * (case when v.Doviz = 'EUR' then 1 " +
                                                                                " when v.Doviz = '' then 0 " +
                                                                                " else v.TLKur / v.EURKur " +
                                                                                " end), ";
            cSQL = cSQL +
               " UretimdeniadeTutarEUR = coalesce(v.UretimdeniadeMiktari,0) * coalesce(v.fiyat,0) * (case when v.Doviz = 'EUR' then 1 " +
                                                                                " when v.Doviz = '' then 0 " +
                                                                                " else v.TLKur / v.EURKur " +
                                                                                " end), ";
            cSQL = cSQL +
               " KalanTutarEUR = coalesce(v.KalanMiktar,0) * coalesce(v.fiyat,0) * (case when v.Doviz = 'EUR' then 1 " +
                                                                                " when v.Doviz = '' then 0 " +
                                                                                " else v.TLKur / v.EURKur " +
                                                                                " end) ";
            cSQL = cSQL +
                " from (select w.*, " +
                        " TLKur = dbo.getkur(w.Doviz,w.KurTarihi), " +
                        " EURKur = dbo.getkur('EUR',w.KurTarihi) ";
            cSQL = cSQL +
                        " from (select a.MalzemeTakipNo, b.StokNo, c.CinsAciklamasi, c.AnaStokGrubu, c.StokTipi, c.Birim1, b.Renk, b.Beden,  " +
                             " ihtiyac = sum(coalesce(b.ihtiyac,0)) , " +
                             " isEmriVerilen = sum(coalesce(b.isemriverilen,0)) , " +
                             " Karsilanan = sum(coalesce(b.isemriicingelen,0) + coalesce(b.isemriharicigelen,0)) , " +
                             " UretimeCikan = sum(coalesce(b.uretimicincikis,0) - coalesce(b.uretimdeniade,0)), " +
                             " Kalite = coalesce((select top 1 kalite from stokdokuma with (NOLOCK) where stokno = b.stokno),'BELIRSIZ'), " +
                             " Tedarikci = coalesce(dbo.getmtftedarikci(a.malzemetakipno,b.stokno,b.renk,b.beden),'BELIRSIZ'), " +
                             " IsemriTarihleri = coalesce(dbo.getmtfisemritarihleri(a.malzemetakipno,b.stokno,b.renk,b.beden),'BELIRSIZ'), " +
                             " Terminler = coalesce(dbo.getmtftermin(a.malzemetakipno,b.stokno,b.renk, b.beden),'BELIRSIZ'), ";
            cSQL = cSQL +
                            " TedarikeiadeMiktari = coalesce((select sum(coalesce(y.netmiktar1,0)) " +
                                    " from stokfis x with (NOLOCK), stokfislines y with (NOLOCK) " +
                                    " where x.stokfisno = y.stokfisno " +
                                    " and y.malzemetakipkodu = a.malzemetakipno " +
                                    " and y.stokno = b.stokno " +
                                    " and y.renk = b.renk " +
                                    " and y.beden = b.beden " +
                                    " and y.stokhareketkodu = '02 Tedarikten iade'),0), ";
            cSQL = cSQL +
                            " UretimdeniadeMiktari = coalesce((select sum(coalesce(y.netmiktar1,0)) " +
                                    " from stokfis x with (NOLOCK), stokfislines y with (NOLOCK) " +
                                    " where x.stokfisno = y.stokfisno " +
                                    " and y.malzemetakipkodu = a.malzemetakipno " +
                                    " and y.stokno = b.stokno " +
                                    " and y.renk = b.renk " +
                                    " and y.beden = b.beden " +
                                    " and y.stokhareketkodu = '01 Uretimden iade'),0), ";
            cSQL = cSQL +
                            " KalanMiktar = coalesce((select sum(coalesce(donemgiris1,0) - coalesce(donemcikis1,0)) " +
                                    " from stokrb with (NOLOCK) " +
                                    " where malzemetakipkodu = a.malzemetakipno " +
                                    " and stokno = b.stokno " +
                                    " and renk = b.renk " +
                                    " and beden = b.beden),0), ";
            cSQL = cSQL +
                            " Fiyat = coalesce((select top 1 y.fiyat " +
                                     " from isemri x with (NOLOCK), isemrilines y with (NOLOCK) " +
                                     " where x.isemrino = y.isemrino " +
                                     " and y.malzemetakipno = a.malzemetakipno " +
                                     " and y.stokno = b.stokno " +
                                     " and y.renk = b.renk " +
                                     " and y.beden = b.beden " +
                                     " and y.fiyat is not null " +
                                     " and y.fiyat > 0 " +
                                     " order by x.tarih desc),0), ";
            cSQL = cSQL +
                            " Doviz = coalesce((select top 1 y.doviz " +
                                     " from isemri x with (NOLOCK), isemrilines y with (NOLOCK) " +
                                     " where x.isemrino = y.isemrino " +
                                     " and y.malzemetakipno = a.malzemetakipno " +
                                     " and y.stokno = b.stokno " +
                                     " and y.renk = b.renk " +
                                     " and y.beden = b.beden " +
                                     " and y.fiyat is not null " +
                                     " and y.fiyat > 0 " +
                                     " order by x.tarih desc),''), ";
            cSQL = cSQL +
                            " KurTarihi = coalesce((select top 1 x.tarih " +
                                     " from isemri x with (NOLOCK), isemrilines y with (NOLOCK) " +
                                     " where x.isemrino = y.isemrino " +
                                     " and y.malzemetakipno = a.malzemetakipno " +
                                     " and y.stokno = b.stokno " +
                                     " and y.renk = b.renk " +
                                     " and y.beden = b.beden " +
                                     " and y.fiyat is not null " +
                                     " and y.fiyat > 0 " +
                                     " order by x.tarih desc),convert(date,getdate())), ";
            cSQL = cSQL +
                            " ilkSevkTarihi = (select min(x.ilksevktarihi) " +
                                     " from siparis x with (NOLOCK), sipmodel y with (NOLOCK) " +
                                     " where x.kullanicisipno = y.siparisno " +
                                     " and y.malzemetakipno = a.malzemetakipno), ";
            cSQL = cSQL +
                           " SonSevkTarihi = (select min(x.sonsevktarihi) " +
                                    " from siparis x with (NOLOCK), sipmodel y with (NOLOCK) " +
                                    " where x.kullanicisipno = y.siparisno " +
                                    " and y.malzemetakipno = a.malzemetakipno), ";
            cSQL = cSQL +
                            " Musteri = coalesce((select top 1 x.musterino " +
                                     " from siparis x with (NOLOCK), sipmodel y with (NOLOCK) " +
                                     " where x.kullanicisipno = y.siparisno " +
                                     " and y.malzemetakipno = a.malzemetakipno),'BELIRSIZ'), ";
            cSQL = cSQL +
                            " MusteriSiparisNo = coalesce((select top 1 x.musterisipno " +
                                     " from siparis x with (NOLOCK), sipmodel y with (NOLOCK) " +
                                     " where x.kullanicisipno = y.siparisno " +
                                     " and y.malzemetakipno = a.malzemetakipno),'BELIRSIZ') ";
            cSQL = cSQL +
                            " from mtkfis a with (NOLOCK), mtkfislines b with (NOLOCK), stok c with (NOLOCK) " +
                            " Where a.malzemetakipno = b.malzemetakipno ";
            cSQL = cSQL +
                            " and a.malzemetakipno in (select y.malzemetakipno " +
                                                     " from siparis x with (NOLOCK), sipmodel y with (NOLOCK), ymodel z with (NOLOCK) " +
                                                     " where x.kullanicisipno = y.siparisno " +
                                                     " and y.modelno = z.modelno ) ";
            //   cFilter + ") ";
            cSQL = cSQL +
                            " and b.stokno = c.stokno " +
                            " and b.ihtiyac is not null " +
                            " and b.ihtiyac > 0   and c.anastokgrubu like '%KUMAS%' and a.malzemetakipno='" + secili_siparis.Text.Trim() + "' ";

            /*  Select Case nDetail
                  Case 1    ' MTF ihtiyacindan fazla üretime cikis yapilmislar
                      cSQL = cSQL + 
                                  " and coalesce(b.ihtiyac,0) < coalesce(b.uretimicincikis,0) - coalesce(b.uretimdeniade,0) "
                  Case 2    ' MTf ihtiyacindan fazla stoklara giris yapilmislar
                      cSQL = cSQL + 
                                  " and coalesce(b.ihtiyac,0) < coalesce(b.isemriicingelen,0) + coalesce(b.isemriharicigelen,0) "
                  Case 3    ' Kumaşlar
                      cSQL = cSQL + 
                                  " and c.anastokgrubu like '%KUMAS%' "
                  Case 4    ' Aksesuarlar
                      cSQL = cSQL + 
                                  " and c.anastokgrubu like '%AKSESUAR%' "
              End Select*/

            cSQL = cSQL +
                                " group by a.malzemetakipno, b.stokno, c.cinsaciklamasi, c.anastokgrubu, c.stoktipi, c.birim1, b.renk, b.beden) w) v ";





            try
            {

                con.SqlQuery(cSQL);


            SqlDataAdapter sqlDataAdap = new SqlDataAdapter(con.Cmd);

            DataTable dtRecord = new DataTable();
            sqlDataAdap.Fill(dtRecord);
            data_kumas.DataSource = dtRecord;
            }
            catch { LogMessageToFile("kumas view da sorun var->" + secili_siparis.Text + "" + cInfo.cUsername); }
            con.Close();

            Cursor.Current = Cursors.Default;
        }

        private void tbtn_uretimbilgileri_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (secili_siparis.Text == String.Empty) { return; }

            con = new SqlDbConnect();
            string cSQL;  // MTF
         





            cSQL = "select x.*, " +
                   " GidecekAdet = (coalesce(x.SiparisAdedi,0) - coalesce(x.SevkiyatAdedi,0)), " +
                   " GidenYuzde = cast((coalesce(x.SevkiyatAdedi,0) / coalesce(x.SiparisAdedi,0) * 100) as decimal(10,0)) ";



            cSQL = cSQL +
                   " from (select KullaniciSipNo, SiparisTarihi, ilkSevkTarihi, SonSevkTarihi, MusteriNo, " +
                        " MusteriSipNo, Sorumlu, SiparisGrubu, Sezon, DosyaKapandi, " +
                        " ModelNo = (select top 1 modelno " +
                                    " from sipmodel with (NOLOCK) " +
                                    " where siparisno = siparis.kullanicisipno), ";
            cSQL = cSQL +
                        " ModelAciklama = (select top 1 aciklama " +
                                    " from ymodel " +
                                    " with (NOLOCK) " +
                                    " where modelno = (select top 1 modelno " +
                                                    " from sipmodel with (NOLOCK) " +
                                                    " where siparisno = siparis.kullanicisipno)), ";
            cSQL = cSQL +
                        " KesimAdedi = (select sum(coalesce(a.adet,0)) " +
                                    " from uretharrba a with (NOLOCK) , uretharfis b with (NOLOCK), uretharfislines c with (NOLOCK)  " +
                                    " where a.uretfisno = b.uretfisno " +
                                    " and b.uretfisno = c.uretfisno " +
                                    " and a.ulineno = c.ulineno " +
                                    " and a.uretimtakipno in (select uretimtakipno " +
                                                            " from sipmodel with (NOLOCK) " +
                                                            " where siparisno = siparis.kullanicisipno) " +
                                    " and b.cikisdept in ('KESİM','KESIM')), ";
            cSQL = cSQL +
                        " DikimAdedi = (select sum(coalesce(a.adet,0)) " +
                                    " from uretharrba a with (NOLOCK) , uretharfis b with (NOLOCK), uretharfislines c with (NOLOCK)  " +
                                    " where a.uretfisno = b.uretfisno " +
                                    " and b.uretfisno = c.uretfisno " +
                                    " and a.ulineno = c.ulineno " +
                                    " and a.uretimtakipno in (select uretimtakipno " +
                                                            " from sipmodel with (NOLOCK) " +
                                                            " where siparisno = siparis.kullanicisipno) " +
                                    " and b.cikisdept in ('DİKİM','DIKIM')), ";
            cSQL = cSQL +
                        " PaketAdedi = (select sum(coalesce(a.adet,0)) " +
                                    " from uretharrba a with (NOLOCK) , uretharfis b with (NOLOCK), uretharfislines c with (NOLOCK)  " +
                                    " where a.uretfisno = b.uretfisno " +
                                    " and b.uretfisno = c.uretfisno " +
                                    " and a.ulineno = c.ulineno " +
                                    " and a.uretimtakipno in (select uretimtakipno " +
                                                            " from sipmodel with (NOLOCK) " +
                                                            " where siparisno = siparis.kullanicisipno) " +
                                    " and b.cikisdept = 'PAKET'), ";
            cSQL = cSQL +
                        " SiparisAdedi  = (select sum(coalesce(adet,0)) " +
                                    " from sipmodel"  +
                                    " with (NOLOCK) " +
                                    " where siparisno = siparis.kullanicisipno), ";
            cSQL = cSQL +
                        " SevkiyatAdedi = (select sum((b.koliend - b.kolibeg + 1) * c.adet) " +
                                    " from sevkform a with (NOLOCK), sevkformlines b with (NOLOCK), sevkformlinesrba c with (NOLOCK) " +
                                    " where a.sevkformno = b.sevkformno " +
                                    " and b.sevkformno = c.sevkformno " +
                                    " and b.ulineno = c.ulineno " +
                                    " and b.siparisno = siparis.kullanicisipno " +

                                    " ), ";
            //        IIf(CLng(getsyspar("cekionay", "integer")) = 1, " and a.ok = 'E' ", "") + " ), "; 
            cSQL = cSQL +
                        " ihracatAdedi = (select sum(coalesce(satismiktari,0)) " +
                                    " from pozisyonmal with (NOLOCK) " +
                                    " where siparisno = siparis.kullanicisipno) ";
            cSQL = cSQL +
                        " from siparis with (NOLOCK) " +
                        " where kullanicisipno is not null " +
                        " and kullanicisipno = '" + secili_siparis.Text.Trim() + "' " +
                        ") x ";








            try { 
            con.SqlQuery(cSQL);


            SqlDataAdapter sqlDataAdap = new SqlDataAdapter(con.Cmd);

            DataTable dtRecord = new DataTable();
            sqlDataAdap.Fill(dtRecord);
            data_uretimbilgileri.DataSource = dtRecord;
            }
            catch { LogMessageToFile("uretimbilgileri view da sorun var->" + secili_siparis.Text + "" + cInfo.cUsername); }
            con.Close();
            Cursor.Current = Cursors.Default;
        }
 

        private void Btn_islemler_Click(object sender, EventArgs e)
        {
            popup_islemler.ShowPopup(Control.MousePosition);
        }

        private void btn_dosya_Click(object sender, EventArgs e)
        {
            if (secili_siparis.Text.Trim() != "")
            {
                Cursor.Current = Cursors.WaitCursor;

                if (Application.OpenForms.OfType<durumtakip_dosyapaneli>().Any())
                {
                    Application.OpenForms.OfType<durumtakip_dosyapaneli>().First().Close();
                }

                durumtakip_dosyapaneli dr_dosyapaneli = new durumtakip_dosyapaneli();
                dr_dosyapaneli.secili_siparis.Text = secili_siparis.Text.Trim();
                dr_dosyapaneli.secili_modeladi.Text = secili_modeladi.Text.Trim();
                


                mail_dizi.Clear();


                İsaretlenenVerilerMail();
                /************************************************************/
                for (int i = 0; i < mail_dizi.Count; i++)
                {


                    for (int j = i + 1; j < mail_dizi.Count; j++)
                    {
                        if (mail_dizi[i].ToString() == mail_dizi[j].ToString())
                        {
                            mail_dizi.Remove(mail_dizi[j]);
                        }
                    }
                }
                /************************************************************/

                dr_dosyapaneli.txt_gonmails.Text = String.Join(",", mail_dizi.ToArray());
                dr_dosyapaneli.txt_bildirimadet.Text = mail_dizi.Count.ToString();



                dr_dosyapaneli.Show();
                Cursor.Current = Cursors.Default;
            }
            else { MessageBox.Show("Lütfen bir sipariş seçiniz"); }
        }

        private void pbtn_sipkapat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (secili_siparis.Text.Trim() != "")
            {

                DialogResult cikis = new DialogResult();
                cikis = MessageBox.Show(secili_siparis.Text.Trim() + " nolu Siparişi sadece bu ekrandan kapatmak için devam etmek istiyormusunuz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (cikis == DialogResult.Yes)
                {
                    //Application.Exit();
                    Cursor.Current = Cursors.WaitCursor;
                    try { 
                    con = new SqlDbConnect();

                    con.SqlQuery("UPDATE siparis SET sipariskapali='E' where kullanicisipno='"+secili_siparis.Text.Trim()+"'");
                    con.QueryNonEx();

                    con.Close();
                    }
                    catch { LogMessageToFile("durum takipte sip kapanışta sorun var->" + secili_siparis.Text + "" + cInfo.cUsername); }

                    Cursor.Current = Cursors.Default;

                    MessageBox.Show(secili_siparis.Text.Trim() + " nolu Sipariş durum takipte kapatıldı", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (cikis == DialogResult.No)
                {
                    
                }

            }
            else {


            }


        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;


            NewOrder.Clear();
            Lst.Items.Clear();
            ImgLst.Images.Clear();

            ilkYuklemeler("ilk");
            
            CheckBox1.Checked = false;
            
            Cursor.Current = Cursors.Default;

        }

        private void tbnt_uretimisemirleri_Click(object sender, EventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;
            if (secili_siparis.Text == String.Empty) { return; }

            con = new SqlDbConnect();
            string cSQL;  // MTF
                          /* cSQL = "select a.uretimtakipno, a.isemrino, a.tarih, a.departman, a.firma, a.ok, a.eleman, a.modificationdate, a.username, " + 
                       " b.baslama_tar, b.bitis_tar, b.modelno, b.bedenseti, b.parca, b.fiyati, b.doviz, " + 
                       " istenen = sum(coalesce(b.toplamadet,0)), " + 
                       " hafta = DatePart(ww, b.bitis_tar) " + 
                       " from uretimisemri a with (NOLOCK), uretimisdetayi b with (NOLOCK), uretplfis c with (NOLOCK) " + 
                       " where a.uretimtakipno = b.uretimtakipno " + 
                       " and a.isemrino = b.isemrino " + 
                       " and a.uretimtakipno = c.uretimtakipno " + 
                       " and a.uretimtakipno='" + secili_siparis.Text + "' " + 
                       " group by a.uretimtakipno, a.isemrino, a.tarih, a.departman, a.firma, a.ok, a.eleman, a.modificationdate, a.username, " + 
                       " b.baslama_tar, b.bitis_tar, b.modelno, b.bedenseti, b.parca, b.fiyati, b.doviz " + 
                       " order by a.uretimtakipno, a.isemrino, b.modelno, b.bedenseti, b.parca ";*/

            cSQL = "select b.uretimtakipno, b.isemrino, a.uretfisno, a.fistarihi, b.harekettipi, " +
        " a.belgeno, a.faturano, a.cikisdept, a.cikisfirm_atl, b.modelno, " +
        " b.bedenseti, b.parca, b.toplamadet, a.modificationdate, a.username, b.fiyati, b.fiyatdoviz  " +
        " from uretharfis a with (NOLOCK), uretharfislines b with (NOLOCK) " +
        " where a.uretfisno = b.uretfisno " +
        "and b.uretimtakipno='" + secili_siparis.Text + "' " +
        //" and b.modelno = '" + cModelNo + "' " +
        " order by a.uretfisno, b.uretimtakipno, b.modelno, b.bedenseti, b.parca ";

            try { 
           con.SqlQuery(cSQL);


            SqlDataAdapter sqlDataAdap = new SqlDataAdapter(con.Cmd);

            DataTable dtRecord = new DataTable();
            sqlDataAdap.Fill(dtRecord);
            data_uretimisemirleri.DataSource = dtRecord;
            }
            catch { LogMessageToFile("uretimisemirleri view da sorun var->" + secili_siparis.Text + "" + cInfo.cUsername); }
            con.Close();

            Cursor.Current = Cursors.Default;
        }

        private void tbnt_kesimbilgileri_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (secili_siparis.Text == String.Empty) { return; }

            con = new SqlDbConnect();
            string cSQL;  // MTF
            cSQL = "select b.uretimtakipno, b.isemrino, a.uretfisno, a.fistarihi, b.harekettipi, " +
        " a.belgeno, a.faturano, a.cikisdept, a.cikisfirm_atl, b.modelno, " +
        " b.bedenseti, b.parca, b.toplamadet, a.modificationdate, a.username, b.fiyati, b.fiyatdoviz  " +
        " from uretharfis a with (NOLOCK), uretharfislines b with (NOLOCK) " +
        " where a.uretfisno = b.uretfisno " +
        "  and b.uretimtakipno='" + secili_siparis.Text + "' " +

         "  and a.cikisdept='KESİM' " +
        //  " and b.modelno = '" + cModelNo + "' " +
        " order by a.uretfisno, b.uretimtakipno, b.modelno, b.bedenseti, b.parca ";

            try { 
            con.SqlQuery(cSQL);


            SqlDataAdapter sqlDataAdap = new SqlDataAdapter(con.Cmd);

            DataTable dtRecord = new DataTable();
            sqlDataAdap.Fill(dtRecord);
            data_kesim.DataSource = dtRecord;
            }
            catch { LogMessageToFile("kesimbilgileri view da sorun var->" + secili_siparis.Text + "" + cInfo.cUsername); }
            con.Close();

            Cursor.Current = Cursors.Default;
        }

        private void tbnt_dikimbilgileri_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (secili_siparis.Text == String.Empty) { return; }

            con = new SqlDbConnect();
            string cSQL;  // MTF
            cSQL = "select b.uretimtakipno, b.isemrino, a.uretfisno, a.fistarihi, b.harekettipi, " +
        " a.belgeno, a.faturano, a.cikisdept, a.cikisfirm_atl, b.modelno, " +
        " b.bedenseti, b.parca, b.toplamadet, a.modificationdate, a.username, b.fiyati, b.fiyatdoviz  " +
        " from uretharfis a with (NOLOCK), uretharfislines b with (NOLOCK) " +
        " where a.uretfisno = b.uretfisno " +
        "  and b.uretimtakipno='" + secili_siparis.Text + "' " +

         "  and a.cikisdept='DİKİM' " +
        //  " and b.modelno = '" + cModelNo + "' " +
        " order by a.uretfisno, b.uretimtakipno, b.modelno, b.bedenseti, b.parca ";

            try { 
            con.SqlQuery(cSQL);


            SqlDataAdapter sqlDataAdap = new SqlDataAdapter(con.Cmd);

            DataTable dtRecord = new DataTable();
            sqlDataAdap.Fill(dtRecord);
            data_dikim.DataSource = dtRecord;
            }
            catch { LogMessageToFile("dikimbilgileri view da sorun var->" + secili_siparis.Text + "" + cInfo.cUsername); }
            con.Close();

            Cursor.Current = Cursors.Default;
        }

        private void tbnt_utupaketbilgileri_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (secili_siparis.Text == String.Empty) { return; }

            con = new SqlDbConnect();
            string cSQL;  // MTF
            cSQL = "select b.uretimtakipno, b.isemrino, a.uretfisno, a.fistarihi, b.harekettipi, " +
        " a.belgeno, a.faturano, a.cikisdept, a.cikisfirm_atl, b.modelno, " +
        " b.bedenseti, b.parca, b.toplamadet, a.modificationdate, a.username, b.fiyati, b.fiyatdoviz  " +
        " from uretharfis a with (NOLOCK), uretharfislines b with (NOLOCK) " +
        " where a.uretfisno = b.uretfisno " +
        "  and b.uretimtakipno='" + secili_siparis.Text + "' " +

         "  and a.cikisdept='PAKET' " +
        //  " and b.modelno = '" + cModelNo + "' " +
        " order by a.uretfisno, b.uretimtakipno, b.modelno, b.bedenseti, b.parca ";

            try
            {
                con.SqlQuery(cSQL);


                SqlDataAdapter sqlDataAdap = new SqlDataAdapter(con.Cmd);

                DataTable dtRecord = new DataTable();
                sqlDataAdap.Fill(dtRecord);
                data_utupaket.DataSource = dtRecord;
            }
            catch { LogMessageToFile("tbnt_utupaketbilgileri_Click view da sorun var->" + secili_siparis.Text + "" + cInfo.cUsername); }
            con.Close();

            Cursor.Current = Cursors.Default;
        }

        private void tbnt_yuklemebilgileri_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (secili_siparis.Text == String.Empty) { return; }

            con = new SqlDbConnect();
            string cSQL;  // MTF
            cSQL = "select f.*, " +
                " fark = coalesce(f.SiparisAdet,0) - coalesce(f.IhracatAdet,0), " +
                " yuzde = (case when coalesce(f.SiparisAdet,0) = 0 then 0 else coalesce(f.IhracatAdet,0) / coalesce(f.SiparisAdet,0) * 100 end) ";


            cSQL = cSQL +
                    " from (select sevkiyattakipno, " +
                            " SiparisAdet = sum(coalesce(adet,0)), " +
                            " PlanlananIlkSevkiyat = min(ilksevktar), ";
        cSQL = cSQL + 
                        " CekiListesiAdet = (Select Sum(coalesce(b.adet,0) * (coalesce(a.koliend,0) - coalesce(a.kolibeg,0) + 1)) " + 
                                        " from sevkformlines a with (NOLOCK) , sevkformlinesrba b with (NOLOCK) " + 
                                        " where a.sevkformno = b.sevkformno " + 
                                        " and a.ulineno = b.ulineno " + 
                                        " and a.siparisno = '" + secili_siparis.Text + "' " +
                                        " and a.sevkiyattakipno = sipmodel.sevkiyattakipno), ";
        cSQL = cSQL + 
                        " IhracatAdet = (select sum(coalesce(b.satismiktari,0)) " + 
                                        " from pozisyon a with (NOLOCK) , pozisyonmal b with (NOLOCK) " + 
                                        " where a.dosyano = b.dosyano " + 
                                        " and b.siparisno = '" + secili_siparis.Text + "' " +
                                        " and b.sevkiyattakipno = sipmodel.sevkiyattakipno) ";
        cSQL = cSQL + 
                        " from sipmodel with (NOLOCK) " + 
                        " where siparisno = '" + secili_siparis.Text + "' " +
                        " group by sevkiyattakipno) f " +
                " order by f.sevkiyattakipno ";


            try
            {
                con.SqlQuery(cSQL);


                SqlDataAdapter sqlDataAdap = new SqlDataAdapter(con.Cmd);

                DataTable dtRecord = new DataTable();
                sqlDataAdap.Fill(dtRecord);
                data_yukleme.DataSource = dtRecord;
            }
            catch { LogMessageToFile("tbnt_utupaketbilgileri_Click view da sorun var->" + secili_siparis.Text + "" + cInfo.cUsername); }
            con.Close();

            Cursor.Current = Cursors.Default;
        }

        private void Uretimyeri_Secimi_SelectedIndexChanged(object sender, EventArgs e)
        {
            ilkYuklemeler("");
        }
   
        private void pbtn_cp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (secili_siparis.Text.Trim() != "" && secili_mt.Text.Trim() != "")
            {

                Cursor.Current = Cursors.WaitCursor;

                if (Application.OpenForms.OfType<durumtakip_cp>().Any())
                {
                    Application.OpenForms.OfType<durumtakip_cp>().First().Close();
                }
                durumtakip_cp drcp = new durumtakip_cp();
          
                drcp.MdiParent = this.ParentForm;
                drcp.secili_siparis.Text = secili_siparis.Text.Trim();
                drcp.secili_mt.Text = secili_mt.Text.Trim();
                drcp.secili_modeladi.Text = secili_modeladi.Text.Trim();
                mail_dizi.Clear();


                İsaretlenenVerilerMail();
                /************************************************************/
                for (int i = 0; i < mail_dizi.Count; i++)
                {


                    for (int j = i + 1; j < mail_dizi.Count; j++)
                    {
                        if (mail_dizi[i].ToString() == mail_dizi[j].ToString())
                        {
                            mail_dizi.Remove(mail_dizi[j]);
                        }
                    }
                }
                /************************************************************/

                drcp.txt_gonmails.Text = String.Join(",", mail_dizi.ToArray());
                drcp.txt_bildirimadet.Text = mail_dizi.Count.ToString();


                drcp.Show();
                Cursor.Current = Cursors.Default;
            }
            else
            {
                MessageBox.Show("Lütfen bir sipariş seçiniz");

            }
        }

        private void data_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (secili_siparis.Text.Trim() != "" && secili_mt.Text.Trim() != "")
            {
                Cursor.Current = Cursors.WaitCursor;
                durumtakip_cp drcp = new durumtakip_cp();
                drcp.MdiParent = this.ParentForm;
                drcp.secili_siparis.Text = secili_siparis.Text.Trim();
                drcp.secili_mt.Text = secili_mt.Text.Trim();
                drcp.secili_modeladi.Text = secili_modeladi.Text.Trim();
                drcp.Show();
                Cursor.Current = Cursors.Default;
            }
            else
            {
                MessageBox.Show("Lütfen bir sipariş seçiniz");

            }
        }

        private void mailvarmi_Tick(object sender, EventArgs e)
        {
            mail_kontrol();
        }

        private void ımageListBoxControl1_Click(object sender, EventArgs e)
        {
            string itemText = ımageListBoxControl1.GetItemText(1);

            string itemText2 =ımageListBoxControl1.SelectedItem.ToString();

            MessageBox.Show(itemText2);
        }
        public static List<string> parcala(string cumle, string ilk, string son)
        {
            List<string> res = new List<string>();
            foreach (System.Text.RegularExpressions.Match item in System.Text.RegularExpressions.Regex.Matches(cumle, ilk + ".*?" + son))
            {
                res.Add(item.Value.Substring(ilk.Length, item.Length - ilk.Length - son.Length));

            }
            return res;

        }

        public string Bul(string metin, string basla, string bitir)
        {
            string sonuc;
            try
            {
                int IcerikBaslangicIndex = metin.IndexOf(basla) + basla.Length;
                int IcerikBitisIndex = metin.Substring(IcerikBaslangicIndex).IndexOf(bitir);
                sonuc = metin.Substring(IcerikBaslangicIndex, IcerikBitisIndex);
            }
            catch (Exception)
            {
                sonuc = null;
            }

            return sonuc;
        }
        private void ımageListBoxControl1_Click_1(object sender, EventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                secili_siparis.Text = Bul(ımageListBoxControl1.SelectedItem.ToString(), " Sipariş Kodu", " Model Adı").Replace(":", "").Trim();
                secili_modeladi.Text = Bul(ımageListBoxControl1.SelectedItem.ToString(), "Model Adı", "Sipariş Tarihi").Replace(":", "").Trim();
                kalan_time = "";
                //HerYerden.Order_No = Lst.FocusedItem.SubItems[0].Text.Replace("Sipariş Kodu       : ", "").Trim();

                secili_mt.Text = Bul(ımageListBoxControl1.SelectedItem.ToString(), "M.Temsilci", "Planlanan Sevk").Replace(":", "").Trim();
                kalan_time = Bul(ımageListBoxControl1.SelectedItem.ToString(), "Yükleme Tarihi", "Müşteri Adı").Replace(":", "").Trim();


                Doldur();

                if (kalan_time != "")
                {
                    kalan_zaman();
                }
                Cursor.Current = Cursors.Default;
            }
            catch { }
        }

        private void pbtn_user_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (secili_siparis.Text.Trim() != "" && secili_mt.Text.Trim() != "")
            {

                try
                {
                    con = new SqlDbConnect();

                    string musterino = "";
                    con.LoginQuery("select musterino from siparis with (NOLOCK) where kullanicisipno='" + secili_siparis.Text.Trim() + "'");
                    while (con.dbr.Read())
                    {
                        musterino = con.dbr["musterino"].ToString().Trim();

                    }
                    con.dbr.Close();
                    con.Close();


                    Cursor.Current = Cursors.WaitCursor;
                    durumtakip_siparisyetkilileri drsipyet = new durumtakip_siparisyetkilileri();
                    drsipyet.MdiParent = this.ParentForm;
                    drsipyet.secili_siparis.Text = secili_siparis.Text.Trim();
                    drsipyet.secili_mt.Text = secili_mt.Text.Trim();

                    drsipyet.secili_musteri.Text = musterino;
                    drsipyet.Show();
                    Cursor.Current = Cursors.Default;
                }
                catch { }

                 


            }
            else
            {
                MessageBox.Show("Lütfen bir sipariş seçiniz");

            }
        }

        private void pbtn_personel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (secili_siparis.Text.Trim() != "" && secili_mt.Text.Trim() != "")
            {
                Cursor.Current = Cursors.WaitCursor;
                durumtakip_personel drpers = new durumtakip_personel();
                drpers.MdiParent = this.ParentForm;
           
                drpers.Show();
                Cursor.Current = Cursors.Default;
            }
            else
            {
                MessageBox.Show("Lütfen bir sipariş seçiniz");

            }
        }

        private void turetim_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            ilkYuklemeler("");
            Cursor.Current = Cursors.Default;
        }

        private void Tar1_ValueChanged(object sender, EventArgs e)
        {
            //if (flag) return;
            Cursor.Current = Cursors.WaitCursor;
            checkBox2.Checked = true;
           // flag = true;
            ilkYuklemeler("");

            checkBox2.Checked = false;
           // flag = false;
            Cursor.Current = Cursors.Default;
        }

        private void Tar2_ValueChanged(object sender, EventArgs e)
        {
           // if (flag) return;
            Cursor.Current = Cursors.WaitCursor;
           // flag = true;
            checkBox2.Checked = true;
            ilkYuklemeler("");
            checkBox2.Checked = false;
           // flag = false;
            Cursor.Current = Cursors.Default;
        }

        private void gridView3_RowCellStyle_1(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                renkgon = Convert.ToDouble(gridView3.GetRowCellValue(e.RowHandle, "Gönderim Gecikme"));
                renkok = Convert.ToDouble(gridView3.GetRowCellValue(e.RowHandle, "OK Gecikme"));
                //  durum = Convert.ToDouble(gridView3.GetRowCellValue(e.RowHandle, "kirmizi"));


            }
            catch { }

            try
            {
                if ((renkgon >= 0 && renkgon <= 2) || (renkok >= 0 && renkok <= 2)) { e.Appearance.BackColor = Color.NavajoWhite; }
                if (renkgon < 0 || renkok < 0) { e.Appearance.BackColor = Color.Salmon; e.Appearance.ForeColor = Color.White; }


            }
            catch { }
        }

        private void Order_Lst_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void Order_Lst_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            ilkYuklemeler("");
            Cursor.Current = Cursors.Default;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            // flag = true;


            for (int i = 0; i < Lsts.Items.Count; i++)
            {


                if (checkBox4.Checked == true)
                {
                    Lsts.SetItemChecked(i, true);
                }
                else
                {
                    Lsts.SetItemChecked(i, false);
                }



            }


            // flag = false;
            Cursor.Current = Cursors.Default;
        }

        private void model_adi_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            ilkYuklemeler("");
            Cursor.Current = Cursors.Default;
        }

        private void labelControl7_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (Application.OpenForms.OfType<durumtakip_mailkisi_grup>().Any())
            {
                Application.OpenForms.OfType<durumtakip_mailkisi_grup>().First().Close();
            }

            durumtakip_mailkisi_grup durumtakip_mailkisi = new durumtakip_mailkisi_grup();

            durumtakip_mailkisi.MdiParent = this.ParentForm;
            durumtakip_mailkisi.Show();
            Cursor.Current = Cursors.Default;
        }

        private void gridView3_RowCellStyle_2(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                renkgon = Convert.ToDouble(gridView3.GetRowCellValue(e.RowHandle, "Gönderim Gecikme"));
                renkok = Convert.ToDouble(gridView3.GetRowCellValue(e.RowHandle, "OK Gecikme"));
                //  durum = Convert.ToDouble(gridView3.GetRowCellValue(e.RowHandle, "kirmizi"));


            }
            catch { }

            try
            {
                if ((renkgon >= 0 && renkgon <= 2) || (renkok >= 0 && renkok <= 2)) { e.Appearance.BackColor = Color.NavajoWhite; }
                if (renkgon < 0 || renkok < 0) { e.Appearance.BackColor = Color.Salmon; e.Appearance.ForeColor = Color.White; }


            }
            catch { }

            GridView view = sender as GridView;
            if (view == null) return;


            view.Columns["Plan.Gönderi Tar."].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            view.Columns["Plan.Gönderi Tar."].DisplayFormat.FormatString = "D";


            view.Columns[2].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            view.Columns[2].DisplayFormat.FormatString = "D";
            view.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            view.Columns[3].DisplayFormat.FormatString = "D";

            view.Columns[4].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            view.Columns[4].DisplayFormat.FormatString = "D";
            view.Columns[5].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            view.Columns[5].DisplayFormat.FormatString = "D";
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            // flag = true;


            for (int i = 0; i < Lst_Grup.Items.Count; i++)
            {


                if (checkBox5.Checked == true)
                {
                    Lst_Grup.SetItemChecked(i, true);
                }
                else
                {
                    Lst_Grup.SetItemChecked(i, false);
                }



            }


            // flag = false;
            Cursor.Current = Cursors.Default;
        }

        private void gridView3_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                renkgon = Convert.ToDouble(gridView3.GetRowCellValue(e.RowHandle, "Gönderim Gecikme"));
                renkok = Convert.ToDouble(gridView3.GetRowCellValue(e.RowHandle, "OK Gecikme"));
                //  durum = Convert.ToDouble(gridView3.GetRowCellValue(e.RowHandle, "kirmizi"));


            }
            catch { }

            try
            {
                if ((renkgon >= 0 && renkgon <= 2) || (renkok >= 0 && renkok <= 2)) { e.Appearance.BackColor = Color.NavajoWhite; }
                if (renkgon < 0 || renkok < 0) { e.Appearance.BackColor = Color.Salmon; e.Appearance.ForeColor = Color.White; }


            }
            catch { }

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            // flag = true;
        

            for (int i = 0; i < LstU.Items.Count; i++)
            {


                if (checkBox3.Checked == true)
                {
                    LstU.SetItemChecked(i, true);
                }
                else {
                    LstU.SetItemChecked(i, false);
                }


               
            }
                        
        
            // flag = false;
            Cursor.Current = Cursors.Default;
        }

        private void data_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        void kalan_zaman() {

            try
            {
                DateTime result = DateTime.ParseExact(kalan_time, format, provider);
                result2 = DateTime.ParseExact(String.Format("{0:dd.MM.yyyy}", DateTime.Now).ToString(), format, provider);
                double kalan_is_gun = GetBusinessDays(result2, result) % 5;
                int kalan_hafta = Convert.ToInt32(GetBusinessDays(result2, result)) / 5;
                btn_kalanzaman.Text = kalan_hafta.ToString() + " H - " + kalan_is_gun + " G";

                //  Mess }ageBox.Show(kalan_time +"-->"+ kalan_hafta.ToString() + " H - " + kalan_is_gun + " G");
            }
            catch { }
        }
        public static double GetBusinessDays(DateTime startD, DateTime endD)
        {
            double calcBusinessDays = 1 + ((endD - startD).TotalDays * 5 - (startD.DayOfWeek - endD.DayOfWeek) * 2) / 7;

            if ((int)endD.DayOfWeek == 6) calcBusinessDays--;

            if ((int)startD.DayOfWeek == 0) calcBusinessDays--;

            return calcBusinessDays - 1;
        }







    }
}
