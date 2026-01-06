using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;

using DevExpress.XtraGrid.Views.Grid;

using System.Collections;
using System.IO;


using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace WinTexC
{
    public partial class durumtakip_cp_musteri : Form
    {
        SqlDbConnect con;
        ArrayList sorumlu_dizi = new ArrayList();

      

        public durumtakip_cp_musteri()
        {
            InitializeComponent();
        }
        string cSQL;   

       
        private void durumtakip_cp_musteri_Load(object sender, EventArgs e)
        {

            liste_ver();

        }
        void liste_ver() {

            Cursor.Current = Cursors.WaitCursor;
            if (secili_musteri.Text == String.Empty) { return; }

            cSQL = "select * from frmoklist with (NOLOCK) where formno='" + secili_musteri.Text + "'";

            sorumlu_dizi.Clear();
            try
            {



                con = new SqlDbConnect();
                con.SqlQuery(cSQL);
                SqlDataAdapter sqlDataAdap = new SqlDataAdapter(con.Cmd);
                DataTable dtRecord = new DataTable();
                sqlDataAdap.Fill(dtRecord);
                gridControl1.DataSource = dtRecord;

                con.LoginQuery("select personel,email from personel with (NOLOCK)");
                while (con.dbr.Read())
                {
                    sorumlu_dizi.Add(con.dbr["personel"].ToString().Trim());
                }
                con.dbr.Close();

                con.Close();
            }
            catch { MessageBox.Show("veriler yüklenemedi"); }


            Cursor.Current = Cursors.Default;



            RepositoryItemComboBox _riEditor = new RepositoryItemComboBox();
            _riEditor.Items.AddRange(sorumlu_dizi);
            gridControl1.RepositoryItems.Add(_riEditor);
            gridView1.Columns[8].ColumnEdit = _riEditor;

            RepositoryItemComboBox _riEditor2 = new RepositoryItemComboBox();
            _riEditor2.Items.AddRange(new string[] { "E","H" });
            gridControl1.RepositoryItems.Add(_riEditor2);
            gridView1.Columns[5].ColumnEdit = _riEditor2;


            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            gridView1.Columns[3].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].Width = 33;

            gridView1.Columns[4].Visible = false;
            gridView1.Columns[6].Visible = false;

            gridView1.Columns[10].DisplayFormat.FormatType = FormatType.Numeric;
            // gridView1.Columns(0).OptionsColumn.FixedWidth =True 
            // gridView1.Columns(1).Width = 33 

        }
        private static readonly string LOG_FILENAME = Path.GetTempPath() + "AldersLog.txt";
        public static void LogMessageToFile(string msg)

        {

            msg = string.Format("{0:G}: {1}rn", DateTime.Now, msg);

            File.AppendAllText(LOG_FILENAME, msg);

        }
        private void gridView3_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            /*ColumnView view = gridControl1.FocusedView as ColumnView;
            view.CloseEditor();
            cSQL = "select * from frmoklist with (NOLOCK) where formno='" + secili_musteri.Text + "'";

            if (view.UpdateCurrentRow())
            {
                con = new SqlDbConnect();
                con.SqlQuery(cSQL);
                SqlDataAdapter sqlDataAdap = new SqlDataAdapter(con.Cmd);
                DataTable dtRecord = new DataTable();
                DataSet ds = new DataSet();
                sqlDataAdap.Update(ds, "frmoklist");
                gridControl1.DataSource = dtRecord;

                gridView1.Columns[0].OptionsColumn.ReadOnly = true;
                */
            /*
         gridView1.Columns(0).OptionsColumn.FixedWidth =True 
         gridView1.Columns(1).Width = 33    

        }*/

           
        }

        private void btn_msjgonder_Click(object sender, EventArgs e)
        {
            con = new SqlDbConnect();

            if (gridView1.DataRowCount > 0) {  
            try
            {
                con.SqlQuery("delete from frmoklist Where formno='" + secili_musteri.Text.Trim() + "'");
                con.QueryNonEx();

                    MessageBox.Show("silindi", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            catch { MessageBox.Show("SİLME İŞLEMİ GERÇEKLEŞEMEDİ", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning); } }

            for (int i = 0; i < gridView1.DataRowCount; i++)
            {
               

                if (gridView1.GetRowCellValue(i, "oktipi").ToString().Trim() != "")
                {

                   // MessageBox.Show(gridView1.GetRowCellValue(i, "oktipi").ToString().Trim());
                   // if (gridView1.GetRowCellValue(i, "sirano").ToString().Trim() != "")
                   // {//insert

                        try
                        {   

                            con.SqlQuery("INSERT INTO frmoklist (oktipi,oktipieng,aciklama,formno,modeldetay,renkdetay,bedendetay,sira,sorumlu,temin) VALUES (@oktipi,@oktipieng,@aciklama,@formno,@modeldetay,@renkdetay,@bedendetay,@sira,@sorumlu,@temin)");
                            con.Cmd.Parameters.AddWithValue("@oktipi", gridView1.GetRowCellValue(i, "oktipi").ToString().Trim());
                            con.Cmd.Parameters.AddWithValue("@oktipieng", gridView1.GetRowCellValue(i, "oktipieng").ToString().Trim());
                            con.Cmd.Parameters.AddWithValue("@aciklama", gridView1.GetRowCellValue(i, "aciklama").ToString().Trim());
                            con.Cmd.Parameters.AddWithValue("@formno", secili_musteri.Text.Trim());
                            con.Cmd.Parameters.AddWithValue("@modeldetay", "H");

                            if (gridView1.GetRowCellValue(i, "renkdetay").ToString().Trim().ToUpper() == "E" || gridView1.GetRowCellValue(i, "renkdetay").ToString().Trim().ToUpper() == "H")
                            { con.Cmd.Parameters.AddWithValue("@renkdetay", gridView1.GetRowCellValue(i, "renkdetay").ToString().Trim().ToUpper()); }
                            else { con.Cmd.Parameters.AddWithValue("@renkdetay", "H"); }
                            con.Cmd.Parameters.AddWithValue("@bedendetay", "H");
                            con.Cmd.Parameters.AddWithValue("@sira", gridView1.GetRowCellValue(i, "sira").ToString().Trim());
                            con.Cmd.Parameters.AddWithValue("@sorumlu", gridView1.GetRowCellValue(i, "sorumlu").ToString().Trim());
                            con.Cmd.Parameters.AddWithValue("@temin", gridView1.GetRowCellValue(i, "temin").ToString().Trim());
                            con.QueryNonEx();
                            MessageBox.Show("Kayıt Tamamlanmıştır", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                        }
                        catch { LogMessageToFile("durumtakip_cp_musteri kaydet all, insert sorunu"); MessageBox.Show("kayıtta Sorun oluştu ->" + gridView1.GetRowCellValue(i, "oktipi").ToString().Trim(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

                   // }
                    



                }



          
            }
            con.Close();

            liste_ver();
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
         /*   GridView view = sender as GridView;
            if (view == null) return;
            e.Appearance.BackColor = Color.NavajoWhite;*/
        }

        private void btn_yenisatir_Click(object sender, EventArgs e)
        {
            gridView1.AddNewRow();
        }

        private void btn_satirsil_Click(object sender, EventArgs e)
        {
            gridView1.DeleteRow(gridView1.FocusedRowHandle);
        }

        private void gridView1_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            gridView1.SetRowCellValue(e.RowHandle,"formno",secili_musteri.Text.Trim());
            gridView1.SetRowCellValue(e.RowHandle, "renkdetay", "H");
        }
    }
}
