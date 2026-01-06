using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using System.Data.SqlClient;
using System.Data;

namespace WinTexC
{
    public class Main
    {
        public string Test()
        {
            return "Test OK";
           
        }

        

    }

    class Heryerden
    {
        public static byte Gun1;

        public static byte Sonuc;

    }
    class User_Bilgileri
    {
        public int User_id;
        public string User_yetki;
        public string User_aciklama;
        public string User_name;
        public string User_pass;
        public string User_ad;
        public string User_email;
    }
        public class cInfo {

        public static string cServer;
        public static  string cDatabase;
        public static string cwintexname;
        public static string cUsername;
        public static string cUsermail;
        public static string cUsermailpass;
        public static string cPassword;
        public static string cdbUser;
        public static string cDepartman;


    }
   
    public class SqlDbConnect
    {
        private SqlConnection _con;



        public SqlCommand Cmd;
        public SqlCommand Cmd_Kon;

        private SqlDataAdapter _da;
        private DataTable _dt;
        private IDataReader _idr;




        public SqlDataReader dbr;
        public SqlDataReader dbr_Kon;


        /*******************************************/

        public SqlDbConnect()
        { 
            /*   
            GlobalData.dz[8] = "";// Name
            GlobalData.dz[9] = "";// Kullanıcıyetki*/
            /*
            dz(8) = UsernameTextBox.Text.Trim                     ' Name Kullanıcı
            dz 9 yetki
            dz(4) = UsernameTextBox.Text.Trim + "@" + dz(7)       ' Mail adresi
            dz(5) = PasswordTextBox.Text.Trim                     ' Mail Şifre

             */           
            //string BaglantiAdresi = "Server=FERHATPC;Database=alderswintex;User Id=sa;Password=123321; connection timeout=290;";
            //string BaglantiAdresi = "Server="+ veritabani .cServer+ ";Database=alderswintex;User Id=sa;Password=Wintex12sa; connection timeout=290;";
            string BaglantiAdresi = "Server="+ cInfo.cServer+ ";Database=" + cInfo.cDatabase + ";User Id=" + cInfo.cdbUser + ";Password=" + cInfo.cPassword + "; connection timeout=290;";
            _con = new SqlConnection(BaglantiAdresi);
             ConnectionState state = _con.State;
            if (state != ConnectionState.Open)
            {
                _con.Open();
            }





        }

        public void SqlQuery(string queryText)
        {
            Cmd = new SqlCommand(queryText, _con);
        }

        /*
         */
        public void LoginQuery(string queryText)
        {
            Cmd = new SqlCommand(queryText, _con);
            dbr = Cmd.ExecuteReader();
        }


        public void KontrolQuery(string queryText)
        {
            Cmd_Kon = new SqlCommand(queryText, _con);
            dbr_Kon = Cmd_Kon.ExecuteReader();

        }

        public DataTable QueryEx()
        {
            _da = new SqlDataAdapter(Cmd);
            _dt = new DataTable();
            _da.Fill(_dt);
            return _dt;
        }

        public void QueryNonEx()
        {
            Cmd.ExecuteNonQuery();
        }



        public void Close()
        {
            _con.Close();
        }




    }

}
