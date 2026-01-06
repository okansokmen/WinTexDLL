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

namespace WinTexC
{
    public partial class excel_read_lpp : Form
    {

        public class DRows
        {
            public String sdosyano;
            public String sCtnno;
            public String sindexno;
            public String sunit;
            public String sset;
            public String sunitctn;
            public String squantitty;
            public String scnts;
            public String slenght;
            public String swidth;
            public String sheight;
            public String sweight;



        };

        DRows rowsdata;

        


         List<DRows> Rowdatalist = new List<DRows>();
        public excel_read_lpp()
        {
            InitializeComponent();
        }

        private void excel_read_lpp_Load(object sender, EventArgs e)
        {

        }

        private void btn_sec_Click(object sender, EventArgs e)
        {
            OpenFileDialog op1 = new OpenFileDialog();
            op1.Multiselect = false;
            op1.ShowDialog();
            op1.Filter = "allfiles|*.xls";
          
            textBox1.Text = op1.FileName;
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

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start(textBox1.Text.Trim().Replace('/', '\\'));
        }

        private void btn_read_Click(object sender, EventArgs e)
        {
            if (sname.Text == "" || textBox1.Text == "" || lnumber.Text == "") {

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
            int colCount = 12;//xlRange.Columns.Count;

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
            int v;
            for (int i = 9; i <= rowCount; i++)
            {
                if (Convert.ToString((xlRange.Cells[i, 4] as Excel.Range).Value2) != null)
                {
                    // if (Int32.TryParse((xlRange.Cells[i, 5] as Excel.Range).Value2, out v))
                    // {
                         
                        rowsdata = new DRows();
                    rowsdata.sdosyano = Convert.ToString((xlRange.Cells[3, 6] as Excel.Range).Value2);
                    rowsdata.sCtnno = Convert.ToString((xlRange.Cells[i, 2] as Excel.Range).Value2);
                    rowsdata.sindexno = Convert.ToString((xlRange.Cells[i, 3] as Excel.Range).Value2);
                    rowsdata.sunit = Convert.ToString((xlRange.Cells[i, 4] as Excel.Range).Value2);
                    rowsdata.sset = Convert.ToString((xlRange.Cells[i, 5] as Excel.Range).Value2);
                    rowsdata.sunitctn = Convert.ToString((xlRange.Cells[i, 6] as Excel.Range).Value2);
                    rowsdata.squantitty = Convert.ToString((xlRange.Cells[i, 7] as Excel.Range).Value2);
                    rowsdata.scnts = Convert.ToString((xlRange.Cells[i, 8] as Excel.Range).Value2);
                    rowsdata.slenght = Convert.ToString((xlRange.Cells[i, 9] as Excel.Range).Value2);
                    rowsdata.swidth = Convert.ToString((xlRange.Cells[i, 10] as Excel.Range).Value2);
                    rowsdata.sheight = Convert.ToString((xlRange.Cells[i, 11] as Excel.Range).Value2);
                    rowsdata.sweight = Convert.ToString((xlRange.Cells[i, 12] as Excel.Range).Value2);






                    Rowdatalist.Add(rowsdata);

                   //  }
                }

                topsatsayi.Text = Rowdatalist.Count.ToString();
                dosyano.Text = Convert.ToString((xlRange.Cells[6, 1] as Excel.Range).Value2);

                for (int j = 1; j <= colCount; j++)
                {

                     //  MessageBox.Show(xlWorksheet.Cells[i, j].ToString());
                    temp = "";
                    // if (Convert.ToString((xlRange.Cells[i, j] as Excel.Range).Value2) != null)
                    if (Convert.ToString((xlRange.Cells[i, 4] as Excel.Range).Value2) != null )
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

            if (Rowdatalist.Count > 0)
            {


                data.Rows.Clear();
                data.Refresh();

                data.ColumnCount = 13;
                data.ColumnHeadersVisible = true;

                // Sütun başlığına ait style tanımlıyoruz.
                DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
                columnHeaderStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                columnHeaderStyle.BackColor = Color.Red;
                columnHeaderStyle.ForeColor = Color.White;
                columnHeaderStyle.Font = new Font("Calibri", 12, FontStyle.Regular);
                data.ColumnHeadersDefaultCellStyle = columnHeaderStyle;


                /*    public String secuency;
            public String totalboxes;
            public String unitperbox;
            public String model;
            public String quality;
            public String collourref;
            public String size;
            public String units;
            public String observastion;
            public String dosyano;*/
                data.Columns[0].Name = "Dosya No";
                data.Columns[1].Name = "CTN NO.";
                data.Columns[2].Name = "INDEX NO.";
                data.Columns[3].Name = "UNIT";
                data.Columns[4].Name = "SET (Units/Polybag)";
                data.Columns[5].Name = "UNIT/CTN";
                data.Columns[6].Name = "QUANTITY";
                data.Columns[7].Name = "CTNS QTY";
                data.Columns[8].Name = "LENGHT (mm)";
                data.Columns[9].Name = "WIDTH (mm)";
                data.Columns[10].Name = "HEIGHT (mm)";
                data.Columns[11].Name = "G. WEIGHT (kg)";

                data.Columns[12].Name = "";



                /*  for (int i = 0; i < Rowdatalist.Count; i++)

                  {*/
                foreach (var value in Rowdatalist)
                {
                    //    data.Columns[0].Width = 70;
                    DataGridViewRow row = (DataGridViewRow)data.Rows[0].Clone();
                    row.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    // row.DefaultCellStyle.Font = new Font("Calibri", 10.5F, FontStyle.Regular);


                    // DataRow d = (DataRow)Rowdatalist[i];



                    row.Cells[0].Value = value.sdosyano;
                    row.Cells[1].Value = value.sCtnno;
                    row.Cells[2].Value = value.sindexno;
                    row.Cells[3].Value = value.sunit;
                    row.Cells[4].Value = value.sset;
                    row.Cells[5].Value = value.sunitctn;

                    row.Cells[6].Value = value.squantitty;
                    row.Cells[7].Value = value.scnts;
                    row.Cells[8].Value = value.slenght;
                    row.Cells[9].Value = value.swidth;
                    row.Cells[10].Value = value.sheight;
                    row.Cells[11].Value = value.sweight;
                    row.Cells[12].Value = "";

                   




                    data.Rows.Add(row);
                }
            }


            xlWorkbook.Close(false, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorksheet);
            releaseObject(xlWorkbook);
            releaseObject(xlApp);
            Cursor.Current = Cursors.Default;
        }

         
    }
}
