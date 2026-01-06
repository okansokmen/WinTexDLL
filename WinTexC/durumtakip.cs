using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinTexC
{
    class durumtakip
    {
    }

    public class Order_Bilgileri
    {
        public string SiparisNo;
        public string Resim;
        public string Resimk;
        public string MusteriTemsilcisi;
        public string siparisgrubu;
        public string MusteriSipNo;
        public string ModelAdi;
        public string SiparisTarihi;
        public string YuklemeTarihi;
        public string MusteriAdi;
        public string SiparisAdeti;
        public string Yuklemeler;
        public string sevkpl;
        public string Uretim_yerleri;
        public string Yuklemetarihleri;

    }

    public class Firmoklist
    {
        public string oktipi;
        public string oktipieng;
        public string aciklama;
        public string sirano;
        public string formno;
        public string renkdetay;
        public string bedendetay;
        public string modeldetay;
        public string sira;
        public string sorumlu;
        public string temin;
        
    }


    public class Gidercekmail_Bilgileri {

        public string id;
        public string tarih;

        public string username;
        public string usermail;
        public string usermailpass;
        public string gidecekler;
        public string konu;
        public string mesaj;
        public string gdurum;
        public string gtarih;
        public string msj_id;
        public string gonpanel;
        public string ektarih;
        public string smtp;

        public string dosya;
        public string resimekle;
        public string siparisno;

        public string gonderildimi;
        public string sebebi;








    }

    public class Global_Config
    {
        public static string Userid = "sa";
        // public static string Pass   = "9453731211";
        public static string Pass = "123321";
        public static string Server = "DESKTOP-LA35RA4\\SQLVERI";
        //public static string Server = "85.97.202.203";


        // public static string Server = "82.151.132.6";  
        // public static string Server = "81.213.109.151";//Mail sunucu
        public static string DB = "DURUMTAKIP";
       //public static string Path    = "D:/resim/"; 
        public static string Path;
        //public static string Path = "\\\\10.0.0.9/yage2012/wintex/";
        public static string imPath = "D:/Sentez/VOGUE/imResim/";
        public static string dosyalarPath ;


        public static byte Resize = 0;

    }

}
