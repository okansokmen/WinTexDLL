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
using System.Collections;
using System.Diagnostics;

using System.Collections;
using System.Data.SqlClient;

namespace WinTexC
{
    public partial class excel_read_zara_askili : Form
    {

        SqlDbConnect con;

        public class DRows
        {
            

            public string secuency;
            public string totalboxes;
            public string unitperbox;
            public string model;
            public string quality;
            public string collourref;
            public string size;
            public string units;
            public string observastion;
            public string dosyano;
            public string sevknos;

            public string kolibeg;
            public string koliend;
                      
            public string sevkformno;
            public string siparisno;
            public string modelno;
            public string sevkiyattakipno;
            public string sevkemrino;
            public string bedenseti;
            public string ulineno;
            public string musterino;
            public string username;
            public string atolye;
            public string renk;

            //  renk,adet,sevkformno,siparisno,modelno,sevkiyattakipno,sevkemrino,bedenseti,ulineno,musterino,degistirmetarihi,degistirmesaati,username,atolye,tasimasekli


        };

        DRows rowsdata;




        List<DRows> Rowdatalist = new List<DRows>();

        public excel_read_zara_askili()
        {
            InitializeComponent();
        }

        private void excel_read_zara_askili_Load(object sender, EventArgs e)
        {

            /*
             con = new SqlDbConnect();
            con.LoginQuery("Select sevkformno From sysinfo");
            while (con.dbr.Read())
            {
               MessageBox.Show(con.dbr["sevkformno"].ToString().Trim());
            }

            con.dbr.Close();
            con.Close();
            */

        }

        private void btn_sec_Click(object sender, EventArgs e)
        {
            Rowdatalist.Clear();

            t_ihrno.Text = "";
            t_msn.Text = "";
            t_sevkisemri.Text = "";
            
            t_sevkno.Text = "";
            t_sipno.Text = "";
             t_star.Text = "";
            t_stno.Text = "";
            topsatsayi.Text = "";
            sname.Text = "";


            t_topadet.Text = "";
            yenisevk.Text = "";

            OpenFileDialog op1 = new OpenFileDialog();
            op1.Multiselect = false;
            op1.ShowDialog();
            op1.Filter = "allfiles|*.xls";

            textBox1.Text = op1.FileName;
            Cursor.Current = Cursors.WaitCursor;

            if (textBox1.Text.Trim() == "") { return; }

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(textBox1.Text.Trim(), 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            ///Excel._Worksheet xlWorksheet = (Excel._Worksheet)xlWorkbook.Sheets[sname.Text.Trim()];

            object misValue = System.Reflection.Missing.Value;
            sname.Items.Clear();
            sname.Items.Add("");

            foreach (Excel._Worksheet Worksheet in xlWorkbook.Worksheets)
            {
                sname.Items.Add(Worksheet.Name);
            }

            xlWorkbook.Close(false, misValue, misValue);
            xlApp.Quit();


            releaseObject(xlWorkbook);
            releaseObject(xlApp);
            Cursor.Current = Cursors.Default;




          //   MessageBox.Show("Database -> " + cInfo.cDatabase);
        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
        private void btn_read_Click(object sender, EventArgs e)
        {
            if (sname.Text == "" || textBox1.Text == "" || lnumber.Text == "")
            {

                MessageBox.Show("Worksheet name ve dosya seçimini doğru yapınız");
                return;

            }
            Cursor.Current = Cursors.WaitCursor;




            Rowdatalist.Clear();





            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(textBox1.Text.Trim(), 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            Excel._Worksheet xlWorksheet = (Excel._Worksheet)xlWorkbook.Sheets[sname.Text.Trim()];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            object misValue = System.Reflection.Missing.Value;
            int rowCount = Convert.ToInt32(lnumber.Text.ToString());// xlRange.Rows.Count;
            int colCount = 9;//xlRange.Columns.Count;

            txt_log.Text = "";
            string temp = "";


            /*
            if (Convert.ToString((xlRange.Cells[25,3] as Excel.Range).Value2) != null)
            {     MessageBox.Show(Convert.ToString((xlRange.Cells[25, 3] as Excel.Range).Value2)); }
            */




            /*
             
             MessageBox.Show("d14->"+Convert.ToString(xlWorksheet.get_Range("D14", "D14").Value2));
             MessageBox.Show("g12->" + Convert.ToString(xlWorksheet.get_Range("g12", "g12").Value2));
             MessageBox.Show("f8->" + Convert.ToString(xlWorksheet.get_Range("f8", "f8").Value2));

             */


            string mOrdernoNo = "";
            int SevkNo = 0;
            string ihrno = "";
            string sevktar = "";
            string siparisno = "";

            con = new SqlDbConnect();

            con.LoginQuery("Select sevkformno From sysinfo");
            while (con.dbr.Read())
            {

                SevkNo = Convert.ToInt32(con.dbr["sevkformno"]) + 1;
            }

            con.dbr.Close();


            /*
            con.SqlQuery("UPDATE sysinfo SET sevkformno=@sevkformno");
            con.Cmd.Parameters.AddWithValue("@sevkformno", SevkNo);
            con.QueryNonEx();
            */


            /*

            */




            // string[] mno = ((Convert.ToString((xlRange.Cells[6, 1] as Excel.Range).Value2).Replace("_", ""))).Replace("Nº de pedido/ Order number:", "").Split('-');
            //14.7 shipment date
            if ((xlRange.Cells[6, 1] as Excel.Range).Value2 != null)
            {
                mOrdernoNo = ((Convert.ToString((xlRange.Cells[6, 1] as Excel.Range).Value2).Replace("_", ""))).Replace("Nº de pedido/ Order number:", "").Trim();

 
              //  Substring(0, 5);

                string[] ihno = ((Convert.ToString((xlRange.Cells[5, 1] as Excel.Range).Value2).Replace("_", ""))).Replace("Nº de albarán/ Delivery note:", "").Split('/');
                //14.7 shipment date

                if (ihno[1].Trim().Length == 2)
                {
                    ihrno = ihno[0].Trim() + "-000" + ihno[1].Trim();
                }
                else if (ihno[1].Trim().Length == 3)
                { ihrno = ihno[0].Trim() + "-00" + ihno[1].Trim(); }
                else if (ihno[1].Trim().Length == 4)
                { ihrno = ihno[0].Trim() + "-0" + ihno[1].Trim(); }

                else { ihrno = ihno[0].Trim() + "-" + ihno[1].Trim(); }

                sevktar = (((Convert.ToString((xlRange.Cells[14, 7] as Excel.Range).Value2).Replace("_", ""))).Replace("Fecha de Envío / Date of shipment:", "")).Replace("..", ".");
            }
            else {

                MessageBox.Show("Order Number okunamadı, lütfen dosyanın doğruluğunu kontrol ediniz");

            }


            con.LoginQuery("select top 1 siparisno From sipmodel where musterisiparisno = '" + mOrdernoNo.Trim() + "'");
            while (con.dbr.Read())
            {
                siparisno = con.dbr["siparisno"].ToString().Trim();
                //  MessageBox.Show(con.dbr["siparisno"].ToString().Trim());
            }
            con.dbr.Close();

            t_sipno.Text = siparisno;

            t_ihrno.Text = ihrno;
            t_msn.Text = mOrdernoNo;
            t_star.Text = sevktar;
            //t_sipno.Text = siparisno;

            if (siparisno == "") {

                btn_devam.Visible = true;

                MessageBox.Show("Müşteri Orderno sistemle eşleşmedi, lütfen sipariş numarasını seçiniz. Eğer yoksa böyle bir kayıt yoktur.");
                t_sipno.Text = "";
                t_sipno.Items.Clear();

                con.LoginQuery("select  siparisno From sipmodel where musterisiparisno like '%" + mOrdernoNo.Trim().Substring(0,5) + "%'");
                while (con.dbr.Read())
                {

                   // siparisno = con.dbr["siparisno"].ToString().Trim();
                    t_sipno.Items.Add(con.dbr["siparisno"].ToString().Trim());


                    //  MessageBox.Show(con.dbr["siparisno"].ToString().Trim());
                }
                con.dbr.Close();


            }


            if (SevkNo.ToString().Trim().Length == 4) { t_sevkno.Text = "000000" + SevkNo.ToString(); }
            else if (SevkNo.ToString().Trim().Length == 5) { t_sevkno.Text = "00000" + SevkNo.ToString(); }
            else if (SevkNo.ToString().Trim().Length == 6) { t_sevkno.Text = "0000" + SevkNo.ToString(); }
            else if (SevkNo.ToString().Trim().Length == 7) { t_sevkno.Text = "000" + SevkNo.ToString(); }
            else if (SevkNo.ToString().Trim().Length == 8) { t_sevkno.Text = "00" + SevkNo.ToString(); }
            else if (SevkNo.ToString().Trim().Length == 9) { t_sevkno.Text = "0" + SevkNo.ToString(); }
            else if (SevkNo.ToString().Trim().Length == 10) { t_sevkno.Text = SevkNo.ToString(); }
            else {
                 t_sevkno.Text = SevkNo.ToString();
            }
            //  MessageBox.Show("select top 1 siparisno From sipmodel where musterisiparisno = '" + mOrdernoNo.Trim() + "'");



            // string aranacak= (Convert.ToString((xlRange.Cells[6, 1] as Excel.Range).Value2).Replace("_", "")).Substring(0, 6);


            t_stno.Items.Clear();

            con.Close();

            xlWorkbook.Close(false, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorksheet);
            releaseObject(xlWorkbook);
            releaseObject(xlApp);
            Cursor.Current = Cursors.Default;


            if (t_sipno.Text.Trim() != "")
            {list_icerik();}
        }

        private void list_icerik() {
            Rowdatalist.Clear();
            btn_devam.Visible = false;




            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(textBox1.Text.Trim(), 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            Excel._Worksheet xlWorksheet = (Excel._Worksheet)xlWorkbook.Sheets[sname.Text.Trim()];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            object misValue = System.Reflection.Missing.Value;
            int rowCount = Convert.ToInt32(lnumber.Text.ToString());// xlRange.Rows.Count;
            int colCount = 9;//xlRange.Columns.Count;

            txt_log.Text = "";
            string temp = "";

            con = new SqlDbConnect();


            /*sevkiiyattakip no*/
            //con.LoginQuery("select DISTINCT sevkiyattakipno From sipmodel where siparisno = '" + t_sipno.Text.Trim() + "'");

            // con.LoginQuery("select sevkiyattakipno From sipmodel where siparisno = '" + t_sipno.Text.Trim() + "'");
            con.LoginQuery("select sevkiyattakipno, sum(adet) as toplamadet , ulke,ilksevktar from sipmodel where siparisno ='" + t_sipno.Text.Trim() + "' group by sevkiyattakipno , ulke , ilksevktar");
            while (con.dbr.Read())
            {

                t_stno.Items.Add(con.dbr["sevkiyattakipno"].ToString().Trim()+" ("+ con.dbr["ulke"].ToString().Trim() +"/"+   con.dbr["toplamadet"].ToString().Trim()    + " adet/"+ con.dbr["ilksevktar"].ToString().Trim() + ") ");
                //  MessageBox.Show(con.dbr["siparisno"].ToString().Trim());
            }
            con.dbr.Close();

         


            // dosyano.Text = "select top 1 siparisno From sipmodel where musterisiparisno = '" + mOrdernoNo.Trim() + "'";


            int v;


            for (int i = 21; i <= rowCount; i++)
            {

                if (Convert.ToString((xlRange.Cells[i, 4] as Excel.Range).Value2) != null)
                {
                    // if (Int32.TryParse((xlRange.Cells[i, 5] as Excel.Range).Value2, out v))
                    // {

                    rowsdata = new DRows();


                    rowsdata.dosyano = t_ihrno.Text.Trim();
                   

                    if (Convert.ToString((xlRange.Cells[i, 1] as Excel.Range).Value2) == "" || Convert.ToString((xlRange.Cells[i, 1] as Excel.Range).Value2) == null)
                    {
                        rowsdata.kolibeg = "10";
                        rowsdata.koliend = "10";
                        rowsdata.secuency = "1-1";
                    }
                    else
                    {

                        if ((xlRange.Cells[i, 1] as Excel.Range).Value2.Contains("-"))
                        {
                            string[] words = { };
                            words = Convert.ToString((xlRange.Cells[i, 1] as Excel.Range).Value2).Trim().Split('-');

                            rowsdata.kolibeg = words[0];
                            rowsdata.koliend = words[1];
                            rowsdata.secuency = rowsdata.kolibeg + "-" + rowsdata.koliend;
                        }
                        else
                        {

                            rowsdata.kolibeg = Convert.ToString((xlRange.Cells[i, 1] as Excel.Range).Value2).Trim();
                            rowsdata.koliend = Convert.ToString((xlRange.Cells[i, 1] as Excel.Range).Value2).Trim();
                            rowsdata.secuency = rowsdata.kolibeg + "-" + rowsdata.koliend;

                        }

                    }

                    if (Convert.ToString((xlRange.Cells[i, 2] as Excel.Range).Value2) == "" || Convert.ToString((xlRange.Cells[i, 2] as Excel.Range).Value2) == null)
                    {
                        rowsdata.totalboxes = "1";
                    }
                    else
                    {
                        rowsdata.totalboxes = Convert.ToString((xlRange.Cells[i, 2] as Excel.Range).Value2);
                    }

                    if (Convert.ToString((xlRange.Cells[i, 3] as Excel.Range).Value2) == "" || Convert.ToString((xlRange.Cells[i, 3] as Excel.Range).Value2) == null)
                    {
                        rowsdata.unitperbox = "1";
                    }
                    else
                    {
                        rowsdata.unitperbox = Convert.ToString((xlRange.Cells[i, 3] as Excel.Range).Value2);
                    }



                    rowsdata.model = Convert.ToString((xlRange.Cells[i, 4] as Excel.Range).Value2);
                    rowsdata.quality = Convert.ToString((xlRange.Cells[i, 5] as Excel.Range).Value2);
                    rowsdata.collourref = Convert.ToString((xlRange.Cells[i, 7] as Excel.Range).Value2);
                    rowsdata.size = Convert.ToString((xlRange.Cells[i, 9] as Excel.Range).Value2);
                    rowsdata.units = Convert.ToString((xlRange.Cells[i, 11] as Excel.Range).Value2);
                    rowsdata.observastion = Convert.ToString((xlRange.Cells[i, 13] as Excel.Range).Value2);
                    rowsdata.sevknos = t_sevkno.Text.Trim();


                    /*
                     
            public string sevkformno; *******
            public string siparisno;
            public string modelno;
            public string sevkiyattakipno;
            public string sevkemrino;
            public string bedenseti;
            public string ulineno;   *****
            public string musterino;
            public string username;
            public string atolye; ********
                     
                     */
                    /**atolye**************************************************************************** 
                    con.LoginQuery("select atolye From sipmodel where musterisiparisno = '" + mOrdernoNo.Trim() + "'");
                    while (con.dbr.Read())
                    {
                        rowsdata.atolye = con.dbr["siparisno"].ToString().Trim();
                        //  MessageBox.Show(con.dbr["siparisno"].ToString().Trim());
                    }

                    con.dbr.Close();

                    /********************************************************************************/
                    //select* From sipmodel where siparisno = 'ZR-1971-169-ELB'
                    rowsdata.renk = "";
                    con.LoginQuery("select renk From sipmodel where siparisno = '" + rowsdata.collourref + "'");
                    while (con.dbr.Read())
                    {
                        rowsdata.renk = con.dbr["renk"].ToString().Trim();
                        //  MessageBox.Show(con.dbr["siparisno"].ToString().Trim());
                    }
                    con.dbr.Close();

                    if (rowsdata.renk=="")
                    {

                        con.LoginQuery("select top 1 renk From sipmodel where siparisno = '" + rowsdata.collourref + "' and renk  is not null");
                        while (con.dbr.Read())
                        {
                            if (con.dbr["renk"].ToString().Trim() != "")
                            {
                                rowsdata.renk = con.dbr["renk"].ToString().Trim();
                            }
                            //  MessageBox.Show(con.dbr["siparisno"].ToString().Trim());
                        }
                        con.dbr.Close();

                    }
                    if (rowsdata.renk == "")
                    {
                        rowsdata.renk = rowsdata.collourref;

                    }

                        con.LoginQuery("Select sevkformlineno From sysinfo");
                    while (con.dbr.Read())
                    {

                        rowsdata.ulineno = (Convert.ToInt32(con.dbr["sevkformlineno"]) + 1).ToString();

                    }
                    con.dbr.Close();


                    con.LoginQuery("select musterino,bedenseti1 From siparis where kullanicisipno = '" + t_sipno.Text.Trim() + "'");
                    while (con.dbr.Read())
                    {
                        rowsdata.musterino = con.dbr["musterino"].ToString().Trim();
                        rowsdata.bedenseti = con.dbr["bedenseti1"].ToString().Trim();

                    }
                    con.dbr.Close();


                    con.LoginQuery("select firma From sipmodel where siparisno = '" + t_sipno.Text.Trim() + "'");
                    while (con.dbr.Read())
                    {
                        rowsdata.atolye = con.dbr["firma"].ToString().Trim();
                        //  MessageBox.Show(con.dbr["siparisno"].ToString().Trim());
                    }

                    con.dbr.Close();
                    /********************************************************************************/

                    /********************************************************************************/


                    /********************************************************************************/

                    /********************************************************************************/






                    Rowdatalist.Add(rowsdata);

                    //  }
                }

                topsatsayi.Text = Rowdatalist.Count.ToString();







                for (int j = 1; j <= colCount; j++)
                {

                    //  MessageBox.Show(xlWorksheet.Cells[i, j].ToString());
                    temp = "";
                    // if (Convert.ToString((xlRange.Cells[i, j] as Excel.Range).Value2) != null)
                    if (Convert.ToString((xlRange.Cells[i, 4] as Excel.Range).Value2) != null)
                    {




                        // if (Int32.TryParse((xlRange.Cells[i, 5] as Excel.Range).Value2, out v))
                        // {

                        temp = Convert.ToString((xlRange.Cells[i, j] as Excel.Range).Value2);
                        txt_log.Text = txt_log.Text + "\r\n" + "Row : " + i.ToString() + " / Coloum : " + j.ToString() + "  /  --> " + temp;

                        txt_log.Select(txt_log.Text.Length - 1, 0);
                        txt_log.SelectionStart = txt_log.TextLength;
                        txt_log.ScrollToCaret();
                        // }


                    }
                    //MessageBox.Show(i.ToString() + "/"+j.ToString()+ "--->" +temp);

                }
            }

            con.Close();
            int Okunan_topadet = 0;


            if (Rowdatalist.Count > 0)
            {


                data.Rows.Clear();
                data.Refresh();

                data.ColumnCount = 17;
                data.ColumnHeadersVisible = true;

                // Sütun başlığına ait style tanımlıyoruz.
                DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
                columnHeaderStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                columnHeaderStyle.BackColor = Color.Red;
                columnHeaderStyle.ForeColor = Color.White;
                columnHeaderStyle.Font = new Font("Calibri", 12, FontStyle.Regular);
                data.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
                data.Columns[0].Name = "Dosya No";
                data.Columns[1].Name = "secuency";
                data.Columns[2].Name = "totalboxes";
                data.Columns[3].Name = "unitperbox";
                data.Columns[4].Name = "model";
                data.Columns[5].Name = "quality";
                data.Columns[6].Name = "collourref";
                data.Columns[7].Name = "size";
                data.Columns[8].Name = "units";
                data.Columns[9].Name = "observastion";
                data.Columns[10].Name = "Sevk No";
                data.Columns[11].Name = "ulineno";
                data.Columns[12].Name = "musterino";
                data.Columns[13].Name = "bedenseti";
                data.Columns[14].Name = "atölye";
                data.Columns[15].Name = "";
                data.Columns[16].Name = "";
                /*  for (int i = 0; i < Rowdatalist.Count; i++)
              {*/

              
                foreach (var value in Rowdatalist)
                {
                    //    data.Columns[0].Width = 70;
                    DataGridViewRow row = (DataGridViewRow)data.Rows[0].Clone();
                    row.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    // row.DefaultCellStyle.Font = new Font("Calibri", 10.5F, FontStyle.Regular);
                    // DataRow d = (DataRow)Rowdatalist[i];
                    row.Cells[0].Value = value.dosyano;
                    row.Cells[1].Value = value.secuency;
                    row.Cells[2].Value = value.totalboxes;
                    row.Cells[3].Value = value.unitperbox;
                    row.Cells[4].Value = value.model;
                    row.Cells[5].Value = value.quality;
                    row.Cells[6].Value = value.collourref;
                    row.Cells[7].Value = value.size;
                    row.Cells[8].Value = value.units;
                    row.Cells[9].Value = value.observastion;
                    row.Cells[10].Value = value.sevknos;
                    row.Cells[11].Value = value.ulineno;
                    row.Cells[12].Value = value.musterino;
                    row.Cells[13].Value = value.bedenseti;
                    row.Cells[14].Value = value.atolye;
                    row.Cells[15].Value = "";
                    row.Cells[16].Value = "";

                    Okunan_topadet = Okunan_topadet + Convert.ToInt32(value.units);
                    data.Rows.Add(row);
                }
            }
            t_topadet.Text = Okunan_topadet.ToString();

            xlWorkbook.Close(false, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorksheet);
            releaseObject(xlWorkbook);
            releaseObject(xlApp);
            Cursor.Current = Cursors.Default;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start(textBox1.Text.Trim().Replace('/', '\\'));
        }

        private void btn_kydt_Click(object sender, EventArgs e)
        {
            if (t_ihrno.Text.Trim() != "" && t_msn.Text.Trim() != "" && t_star.Text.Trim() != "" && t_sipno.Text.Trim() != "" && t_sevkno.Text.Trim() != "" && t_sevkisemri.Text.Trim() != "" && Rowdatalist.Count>0)
            {   Update_et();



            }
            else {  MessageBox.Show("Bilgiler eksik olmamalı, lütfen eksikleri gideriniz");   }
                                  
        }





        private void Update_et()
        {


            byte basla = 0;
            int cSqlToplamAdet = 0;
            string cSqlsevkno = "";
            // Kullanıcılar *************************************/
            con = new SqlDbConnect();




            con.SqlQuery("UPDATE sysinfo SET sevkformno=@sevkformno");
            con.Cmd.Parameters.AddWithValue("@sevkformno", t_sevkno.Text);
            con.QueryNonEx();




            string[] gelen_t_stno = t_stno.Text.Trim().Split('(');
            //14.7 shipment date




            // con.LoginQuery("Select * From sevkform Where sevktar='"+ DateTime.Parse( t_star.Text.Trim())+ "' and ihracatdosyano='" + t_ihrno.Text.Trim() + "'  ");
            con.LoginQuery("select a.sevkformsirano, a.sevkformno,a.ihracatdosyano, CONVERT(INT,(select SUM(adet) from sevkformlines where sevkformno=a.sevkformno ) )as toplamadet  from sevkform a, sevkformlines b where a.sevkformno = b.sevkformno and a.ihracatdosyano = '" + t_ihrno.Text.Trim() + "' and  a.sevktar='" + DateTime.Parse(t_star.Text.Trim()) + "'  group by a.ihracatdosyano, a.sevkformno, b.sevkformno, a.sevkformsirano");
            while (con.dbr.Read())
            {
                basla++;
                cSqlToplamAdet = Convert.ToInt32(con.dbr["toplamadet"].ToString());
                cSqlsevkno = con.dbr["sevkformno"].ToString();
                /*  txtAd.Text = con.dbr["Ad"].ToString();
                  txt_kod.Text = con.dbr["Kod"].ToString();
                  trimkart.Text = con.dbr["Trim"].ToString();

                  dateTimePicker1.Text = con.dbr["Htarih"].ToString();
                  Durum.Text = con.dbr["Durum"].ToString();*/

            }
            con.dbr.Close();


            yenisevk.Text = cSqlsevkno;






            con.Close();
            MessageBox.Show(basla.ToString() + "//"+cSqlToplamAdet.ToString());

            string metin = "";
            con = new SqlDbConnect();

            //SqlCommand cmd = new SqlCommand("UPDATE kisiler SET Ad=@ad,Soyad=@soyad,Yas=@yas,Tarih=@tarih,Onay=@onay WHERE ID=@id ", baglanti);


            if (basla >0 && cSqlToplamAdet ==Convert.ToUInt32(t_topadet.Text.Trim()))
            {
                try
                {
                    

                    con.SqlQuery("delete from  sevkformlines Where siparisno='" + t_sipno.Text.Trim() + "' and modelno='" + t_sipno.Text.Trim() + "' and  sevkiyattakipno='"+ t_stno.Text.Trim()+"'");
                    con.QueryNonEx();

                    //MessageBox.Show("Silindi");
                    /**********************************************************************************************************************************/
                    foreach (var value in Rowdatalist)
                    {
                        // MessageBox.Show(Convert.ToDecimal(value.units).ToString() +" / "+ value.units);
                        string cSQL = "INSERT INTO sevkformlines (kolibeg, koliend, renk, adet, sevkformno, " +
                                " siparisno, modelno, sevkiyattakipno, sevkemrino, bedenseti, " +
                                " ulineno, uretimtakipno, musterino, degistirmetarihi, degistirmesaati, " +
                                " username, atolye, tasimasekli) ";

                        cSQL += " VALUES (" + value.kolibeg.ToString() + ", " +
                            value.koliend.ToString() + ", " +
                      "'" + value.renk.ToString() + "', " +
                            value.units.ToString() + ", " +
                      "'" + cSqlsevkno + "', " +
                      "'" + t_sipno.Text.Trim() + "', " +
                      "'" + t_sipno.Text.Trim() + "', " +
                      "'" + gelen_t_stno[0].Trim() + "', " +
                      "'" + t_sevkisemri.Text.Trim() + "', " +
                      "'" + value.bedenseti.ToString() + "', " +
                            value.ulineno.ToString() + ", " +
                      "'" + t_sipno.Text.Trim() + "', " +
                      "'" + value.musterino.ToString() + "', " +
                     " getdate(), " +
                      "'', " +
                      "'" + cInfo.cUsername.ToString() + "', " +
                      "'" + value.atolye.ToString() + "', " +
                      "'" + t_tasimasekli.Text.Trim() + "' " +
                            " ) ";
                        //  MessageBox.Show(cSQL);

                        // con.SqlQuery(cSQL);
                       // con.QueryNonEx();



                            con.SqlQuery("INSERT INTO sevkformlines (kolibeg,koliend,renk,adet,sevkformno,siparisno,modelno,sevkiyattakipno,sevkemrino,bedenseti,ulineno,uretimtakipno,musterino,degistirmetarihi,degistirmesaati,username,atolye,tasimasekli) VALUES (@kolibeg,@koliend,@renk,@adet,@sevkformno,@siparisno,@modelno,@sevkiyattakipno,@sevkemrino,@bedenseti,@ulineno,@uretimtakipno,@musterino,@degistirmetarihi,@degistirmesaati,@username,@atolye,@tasimasekli)");
                            con.Cmd.Parameters.Add("@kolibeg", Convert.ToDecimal(value.kolibeg.ToString()));
                            con.Cmd.Parameters.Add("@koliend", Convert.ToDecimal(value.koliend));
                            con.Cmd.Parameters.Add("@renk", value.renk.Trim());
                            con.Cmd.Parameters.Add("@adet", Convert.ToDecimal(value.units));
                            con.Cmd.Parameters.Add("@sevkformno", cSqlsevkno);
                            con.Cmd.Parameters.Add("@modelno", t_sipno.Text.Trim());
                            con.Cmd.Parameters.Add("@siparisno", t_sipno.Text.Trim());
                            con.Cmd.Parameters.Add("@sevkiyattakipno", gelen_t_stno[0].Trim());
                            con.Cmd.Parameters.Add("@sevkemrino", t_sevkisemri.Text.Trim());
                            con.Cmd.Parameters.Add("@bedenseti", value.bedenseti.Trim());
                            con.Cmd.Parameters.Add("@ulineno", Convert.ToDecimal(value.ulineno));
                            con.Cmd.Parameters.Add("@uretimtakipno", t_sipno.Text.Trim());
                            con.Cmd.Parameters.Add("@musterino", value.musterino.Trim());
                            con.Cmd.Parameters.Add("@degistirmetarihi", DateTime.Now);
                            con.Cmd.Parameters.Add("@degistirmesaati", "");
                            con.Cmd.Parameters.Add("@username", cInfo.cUsername.Trim());
                            con.Cmd.Parameters.Add("@atolye", value.atolye.Trim());
                            con.Cmd.Parameters.Add("@tasimasekli", t_tasimasekli.Text.Trim());
                            con.QueryNonEx(); 

                            con.SqlQuery("INSERT INTO sevkformlinesrba (sevkformno,renk,beden,adet,ulineno) VALUES (@sevkformno,@renk,@beden,@adet,@ulineno)");
                            con.Cmd.Parameters.Add("@renk", value.renk);
                            con.Cmd.Parameters.Add("@adet", Convert.ToDecimal(value.units));
                            con.Cmd.Parameters.Add("@sevkformno", t_sevkno.Text.Trim());
                            con.Cmd.Parameters.Add("@beden", value.size.Trim());
                            con.Cmd.Parameters.Add("@ulineno", Convert.ToDecimal(value.ulineno));
                            con.QueryNonEx();
                        MessageBox.Show("Güncelleme başarılı");

                    }



                    }
                    catch { MessageBox.Show("Güncelleme Durumu Problemli"); }


                }
                else
                {
                    /**** Kullanıcılar *******************************************************/


                        con.SqlQuery("INSERT INTO sevkform (sevktar,turkcenotlar,sevkiyattakipno,OK,sevkformno,ihracatdosyano,tasimasekli) VALUES (@sevktar,@turkcenotlar,@sevkiyattakipno,@OK,@sevkformno,@ihracatdosyano,@tasimasekli)");
                con.Cmd.Parameters.Add("@sevktar", DateTime.Parse(t_star.Text.Trim()));
                con.Cmd.Parameters.Add("@turkcenotlar", "Bu kayıt excel okutularak, " + cInfo.cUsername +"("+DateTime.Now+") tarafından oluşturulmuştur");
                con.Cmd.Parameters.Add("@sevkiyattakipno", gelen_t_stno[0].Trim());
                con.Cmd.Parameters.Add("@OK", "H");
                con.Cmd.Parameters.Add("@sevkformno", t_sevkno.Text.Trim());   
                con.Cmd.Parameters.Add("@ihracatdosyano", t_ihrno.Text.Trim());
                con.Cmd.Parameters.Add("@tasimasekli", t_tasimasekli.Text.Trim() );
                con.QueryNonEx();



                foreach (var value in Rowdatalist)
                {

                   // MessageBox.Show(Convert.ToDecimal(value.kolibeg).ToString() + " / kolibeg_> " + value.kolibeg + " / renk_> "+ value.renk.Trim() +" koli end_>"+ value.koliend);

                    string cSQL = "INSERT INTO sevkformlines (kolibeg, koliend, renk, adet, sevkformno, " +
                                    " siparisno, modelno, sevkiyattakipno, sevkemrino, bedenseti, " +
                                    " ulineno, uretimtakipno, musterino, degistirmetarihi, degistirmesaati, " +
                                    " username, atolye, tasimasekli) " ;

                    cSQL += " VALUES ("+ value.kolibeg.ToString() + ", " +
                            value.koliend.ToString() + ", " +
                      "'"+  value.renk.ToString() + "', " +
                            value.units.ToString() + ", " +
                      "'" + t_sevkno.Text.Trim() + "', " +
                      "'" + t_sipno.Text.Trim() + "', " +
                      "'" + t_sipno.Text.Trim() + "', " +
                      "'" + gelen_t_stno[0].Trim() + "', " +
                      "'" + t_sevkisemri.Text.Trim() + "', " +
                      "'" + value.bedenseti.ToString() + "', " +
                            value.ulineno.ToString() + ", " +
                      "'" + t_sipno.Text.Trim() + "', " +
                      "'" + value.musterino.ToString() + "', " +
                     " getdate(), " +
                      "'', " +
                      "'" +cInfo.cUsername.ToString() + "', " +
                      "'" + value.atolye.ToString() + "', " +
                      "'" + t_tasimasekli.Text.Trim() + "' " +
                            " ) ";
                  //  MessageBox.Show(cSQL);

                    /*con.SqlQuery(cSQL);
                    con.QueryNonEx();*/
                 
                    con.SqlQuery("INSERT INTO sevkformlines (kolibeg,koliend,renk,adet,sevkformno,siparisno,modelno,sevkiyattakipno,sevkemrino,bedenseti,ulineno,uretimtakipno,musterino,degistirmetarihi,degistirmesaati,username,atolye,tasimasekli) VALUES (@kolibeg,@koliend,@renk,@adet,@sevkformno,@siparisno,@modelno,@sevkiyattakipno,@sevkemrino,@bedenseti,@ulineno,@uretimtakipno,@musterino,@degistirmetarihi,@degistirmesaati,@username,@atolye,@tasimasekli)");
                    con.Cmd.Parameters.Add("@kolibeg", Convert.ToDecimal(value.kolibeg.ToString() ));
                    con.Cmd.Parameters.Add("@koliend", Convert.ToDecimal(value.koliend));
                    con.Cmd.Parameters.Add("@renk", value.renk.Trim());
                    con.Cmd.Parameters.Add("@adet", Convert.ToDecimal(value.units));
                    con.Cmd.Parameters.Add("@sevkformno",t_sevkno.Text.Trim());
                    con.Cmd.Parameters.Add("@modelno", t_sipno.Text.Trim());
                    con.Cmd.Parameters.Add("@siparisno", t_sipno.Text.Trim());
                    con.Cmd.Parameters.Add("@sevkiyattakipno", gelen_t_stno[0].Trim());
                    con.Cmd.Parameters.Add("@sevkemrino", t_sevkisemri.Text.Trim());
                    con.Cmd.Parameters.Add("@bedenseti", value.bedenseti.Trim());
                    con.Cmd.Parameters.Add("@ulineno", Convert.ToDecimal(value.ulineno));
                    con.Cmd.Parameters.Add("@uretimtakipno", t_sipno.Text.Trim());
                    con.Cmd.Parameters.Add("@musterino", value.musterino.Trim());
                    con.Cmd.Parameters.Add("@degistirmetarihi", DateTime.Now);
                    con.Cmd.Parameters.Add("@degistirmesaati", "");
                    con.Cmd.Parameters.Add("@username", cInfo.cUsername.Trim());
                    con.Cmd.Parameters.Add("@atolye", value.atolye.Trim());
                    con.Cmd.Parameters.Add("@tasimasekli", t_tasimasekli.Text.Trim());
                    con.QueryNonEx(); 
                   
                    con.SqlQuery("INSERT INTO sevkformlinesrba (sevkformno,renk,beden,adet,ulineno) VALUES (@sevkformno,@renk,@beden,@adet,@ulineno)");
                    con.Cmd.Parameters.Add("@renk", value.renk);
                    con.Cmd.Parameters.Add("@adet", Convert.ToDecimal(value.units));
                    con.Cmd.Parameters.Add("@sevkformno", t_sevkno.Text.Trim());
                    con.Cmd.Parameters.Add("@beden", value.size.Trim());
                    con.Cmd.Parameters.Add("@ulineno", Convert.ToDecimal(value.ulineno));
                    con.QueryNonEx();

                }

                
                
                MessageBox.Show("Ekleme Yapıldı");

              

 
            }

            con.Close();
 
        }

        private void t_stno_SelectedIndexChanged(object sender, EventArgs e)
        {

            // MessageBox.Show("değişim geldi->"+ t_stno.Text.Trim());

            string[] gelen_t_stno = t_stno.Text.Trim().Split('(');
            //14.7 shipment date
          
            if (gelen_t_stno[0].Trim() != "") {

                con = new SqlDbConnect();
                con.LoginQuery("select top 1 sevkemrino From sevkplfislines where sevkiyattakipno ='" + gelen_t_stno[0].Trim() + "'");
                while (con.dbr.Read())
                {
                    t_sevkisemri.Text = con.dbr["sevkemrino"].ToString();
                   // MessageBox.Show("değişim geldi 2 ->" + con.dbr["sevkemrino"].ToString());
                }
                con.dbr.Close();
                con.Close();

            } 
        }

        private void btn_devam_Click(object sender, EventArgs e)
        {
            list_icerik();
        }

        private void t_stno_DropDown(object sender, EventArgs e)
        {
            ComboBox senderComboBox = (ComboBox)sender;
            int width = senderComboBox.DropDownWidth;
            Graphics g = senderComboBox.CreateGraphics();
            Font font = senderComboBox.Font;
            int vertScrollBarWidth =
                (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                ? SystemInformation.VerticalScrollBarWidth : 0;

            int newWidth;
            foreach (string s in ((ComboBox)sender).Items)
            {
                newWidth = (int)g.MeasureString(s, font).Width
                    + vertScrollBarWidth;
                if (width < newWidth)
                {
                    width = newWidth;
                }
            }
            senderComboBox.DropDownWidth = width;
        }
    }
}
