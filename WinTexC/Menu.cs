using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
namespace WinTexC
{
    public partial class Menu : Form
    {

        public static SqlDbConnect con;


        public Menu()
        {
            InitializeComponent();
        }
       
        private void button1_Click(object sender, EventArgs e)
        {

            
            /*
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"Book1.xlsx", 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            Excel._Worksheet xlWorksheet = (Excel._Worksheet)xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            for (int i = 1; i <= rowCount; i++)
            {
                for (int j = 1; j <= colCount; j++)
                {
                    MessageBox.Show(xlWorksheet.Cells[i, j].ToString());
                }
            }
                    
            */



        }
        public void Close_All()
        {

            if (ActiveMdiChild != null)
            {

                Form frms = ActiveMdiChild;
                frms.Close();
            }

        }
        private void lPPToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // Close_All();
            excel_read_lpp SA = new excel_read_lpp() { MdiParent = this };
            SA.Show();


        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void Menu_Load(object sender, EventArgs e)
        {
            //   MessageBox.Show("Database-->"+cInfo.cDatabase+"<--");

            if (cInfo.cwintexname == "") { cInfo.cwintexname = "ADMIN"; }

            if (cInfo.cwintexname != "")
            {


                con = new SqlDbConnect();

                con.LoginQuery("select personel,departman,email,emailpass from  personel with (NOLOCK) where username='" + cInfo.cwintexname + "'");

                while (con.dbr.Read())
                {
                    cInfo.cUsermail = con.dbr["email"].ToString().Trim();
                    cInfo.cUsermailpass = con.dbr["emailpass"].ToString().Trim();
                    cInfo.cDepartman = con.dbr["departman"].ToString().Trim();
                    cInfo.cUsername = con.dbr["personel"].ToString().Trim();
                }
                con.dbr.Close();


                /*"         \\\\10.0.0.9/yage2012/wintex/";   
                
                  string tum_mailler = String.Join(",", mail_dizi.ToArray());
                   string[] gelen_t_stno = t_stno.Text.Trim().Split('(');
                 */



                con.LoginQuery("select parametervalue from syspar where parametername='pathofshare'");

                while (con.dbr.Read())
                {

                    //c#
                    //Global_Config.Path = con.dbr["parametervalue"].ToString().Trim();
                    //Global_Config.Path= "\\" + (Global_Config.Path.Replace("\\", "/"));
                    //Global_Config.Path = (Global_Config.Path.Replace("\\", "//")).Replace("////", " \\\\") +"/";
                    //Global_Config.dosyalarPath = Global_Config.Path + "/durumtakip/";


                    //vb
                   Global_Config.Path = con.dbr["parametervalue"].ToString().Trim();
                   Global_Config.dosyalarPath = Global_Config.Path + "\\durumtakip\\";



                }
                con.dbr.Close();

               // MessageBox.Show(Global_Config.Path);


                con.Close();

            }


            // MessageBox.Show("Global_Config-->" + Global_Config.Path + "Database-->" + cInfo.cDatabase + " |--username-->"+ cInfo.cUsermail +" |-->email->"+ cInfo.cUsermail + " |--->pass-->"+ cInfo.cUsermailpass +" |-->departman->"+ cInfo.cDepartman + " |--wintexname->" + cInfo.cwintexname);

            Cursor.Current = Cursors.WaitCursor;




            /* cInfo.cUsername = "Ferhat Uğurlu";
             cInfo.cUsermail = "ferhatugurlu@alders.com.tr";
             cInfo.cUsermailpass = "F222555u";
             cInfo.cDepartman = "Yazılım";*/

            durumtakip_aktif da = new durumtakip_aktif() { MdiParent = this };
            da.Show();
            Cursor.Current = Cursors.Default;
        }

        private void btn_zarakolili_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btn_lpp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           // Close_All();
            excel_read_lpp SA = new excel_read_lpp() { MdiParent = this };
            SA.Show();
        }

        private void btn_zaraaskili_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //// Close_All();
            excel_read_zara_askili SA = new excel_read_zara_askili() { MdiParent = this };
            SA.Show();
        }

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Close_All();
            excel_read_zara_askili SA = new excel_read_zara_askili() { MdiParent = this };
            SA.Show();

        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Close_All();
            excel_read_lpp SA = new excel_read_lpp() { MdiParent = this };
            SA.Show();
        }

        public void CheckOpened(string name)
        {
         //   FormCollection fc = Application.OpenForms;

            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name == name)
                {
                  frm.Close();


                }
            }
            
        }


        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            /*
            FormCollection fc = Application.OpenForms;
            foreach (Form frm in fc)
            {
                //iterate through
            }*/


                //CheckOpened("durumtakip_aktif");
               
            /*
            try
            {
                if (Application.OpenForms.OfType<durumtakip_aktif>().Any())
                {
                    durumtakip_aktif frm = new durumtakip_aktif();
                    frm.MdiParent = this;
                    frm.Close();
             
                }
                else
                {
                    durumtakip_aktif frm = new durumtakip_aktif();
                    frm.MdiParent = this;
                    frm.Show();
                  
                }
            }
            catch (Exception ex)
            {

            }*/


            {
                Application.OpenForms.OfType<durumtakip_aktif>().First().Close();
            }

            durumtakip_aktif frm = new durumtakip_aktif();
            frm.MdiParent = this;
            frm.Show();
            /* cInfo.cUsername = "Ferhat Uğurlu";
             cInfo.cUsermail = "ferhatugurlu@alders.com.tr";
             cInfo.cUsermailpass = "F222555u";
             cInfo.cDepartman = "Yazılım";*/

            /* durumtakip_aktif da = new durumtakip_aktif();
             da.MdiParent = this;
             da.Show();*/
            Cursor.Current = Cursors.Default;
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //  CheckOpened("durumtakip_cp_rapor");

                {
                    Application.OpenForms.OfType<durumtakip_cp_rapor>().First().Close();
                }

                durumtakip_cp_rapor ddcr = new durumtakip_cp_rapor();
                ddcr.MdiParent = this;
                ddcr.Show();
        }
    }
}
