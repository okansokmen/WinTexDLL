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

namespace WinTexC
{
    public partial class durumtakip_cpdetay : Form
    {

        public static SqlDbConnect con;
        public durumtakip_cpdetay()
        {
            InitializeComponent();
        }

        private void durumtakip_cpdetay_Load(object sender, EventArgs e)
        {


            if (secili_siparis.Text.Trim() != "") { 
            con = new SqlDbConnect();





            con.SqlQuery("select * from sipok where siparisno='" + secili_siparis.Text.Trim() + "'with (NOLOCK)");


            SqlDataAdapter sqlDataAdap = new SqlDataAdapter(con.Cmd);

            DataTable dtRecord = new DataTable();
            sqlDataAdap.Fill(dtRecord);
            grid_cp.DataSource = dtRecord;



            con.Close();
                                      }
        }
    }
}
