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

namespace WinTexC
{
    public partial class durumtakip_siparisyetkilileri : Form
    {
        SqlDbConnect con;
        durumtakip_aktif AnaForm_F = new durumtakip_aktif();
        int msj_id = 0;
        public durumtakip_siparisyetkilileri()
        {
            InitializeComponent();
        }

        private static readonly string LOG_FILENAME = Path.GetTempPath() + "WintexCLog.txt";

        public static void LogMessageToFile(string msg)

        {

            msg = string.Format("{0:G}: {1}rn ------->", DateTime.Now, msg);

            File.AppendAllText(LOG_FILENAME, msg);

        }
        private void durumtakip_siparisyetkilileri_Load(object sender, EventArgs e)
        {
            if (secili_siparis.Text.Trim() == "") { MessageBox.Show("Bir sipariş seçilmemiş"); return; }
            else
            {
                try
                {
                    byte varmi = 0;
                    con = new SqlDbConnect();


                    con.LoginQuery("select * from durumtakip_sipyetkili where siparisno='" + secili_siparis.Text.Trim() + "'");
                    while (con.dbr.Read())
                    {
                        if (con.dbr["siparisno"].ToString().Trim() != "") { varmi = 1; }
                    }
                    con.dbr.Close();

                    if (varmi == 0)
                    {
                        con.SqlQuery("INSERT INTO durumtakip_sipyetkili (siparisno) VALUES (@siparisno)");
                        con.Cmd.Parameters.AddWithValue("@siparisno", secili_siparis.Text.Trim());
                        con.QueryNonEx();
                    }
                    else
                    {
                        con.LoginQuery("select * from durumtakip_sipyetkili where siparisno='" + secili_siparis.Text.Trim() + "'");
                        while (con.dbr.Read())
                        {
                            taksesuar.Text = con.dbr["aksesuar"].ToString().Trim();
                            taksesuar_mail.Text = con.dbr["aksesuar_mail"].ToString().Trim();
                            taksesuar_keywords.Text = con.dbr["aksesuar_keywords"].ToString().Trim();

                            tkumas.Text = con.dbr["kumas"].ToString().Trim();
                            tkumas_mail.Text = con.dbr["kumas_mail"].ToString().Trim();
                            tkumas_keywords.Text = con.dbr["kumas_keywords"].ToString().Trim();

                            tmodelhane.Text = con.dbr["modelhane"].ToString().Trim();
                            tmodelhane_mail.Text = con.dbr["modelhane_mail"].ToString().Trim();
                            tmodelhane_keywords.Text = con.dbr["modelhane_keywords"].ToString().Trim();

                            turetim.Text = con.dbr["uretim"].ToString().Trim();
                            turetim_mail.Text = con.dbr["uretim_mail"].ToString().Trim();
                            turetim_keywords.Text = con.dbr["uretim_keywords"].ToString().Trim();

                            tlabratuar.Text = con.dbr["labratuar"].ToString().Trim();
                            tlabratuar_mail.Text = con.dbr["labratuar_mail"].ToString().Trim();
                            tlabratuar_keywords.Text = con.dbr["labratuar_keywords"].ToString().Trim();

                            tkalite.Text = con.dbr["kalite"].ToString().Trim();
                            tkalite_mail.Text = con.dbr["kalite_mail"].ToString().Trim();
                            tkalite_keywords.Text = con.dbr["kalite_keywords"].ToString().Trim();
                        }
                        con.dbr.Close();


                    }



                    con.Close();


                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Bir sorun oluştu (durumtakip_siparisyetkili), lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    LogMessageToFile("durumtakip_aciklamalogs Listesi Oluşturulurken Hata Oluştu " + Environment.NewLine + Ex.ToString());

                }

            }

            ilk();
            doldur();

            BuildDataTable();

        }

        void ilk() {

            try {


                con = new SqlDbConnect();
                //select personel from personel where  ( mgrup like '%COS%' or mgrup like '%HEPSİ%') and (keywords like '%kumaş%' or aciklama like '%kumaş%')
                taksesuar.Properties.Items.Clear();
                con.LoginQuery("select personel from personel where ( mgrup like '%"+secili_musteri.Text.Trim()+ "%' or mgrup like '%HEPSİ%') and (keywords like '%kumaş%' or aciklama like '%aksesuar%')");
                while (con.dbr.Read())
                { taksesuar.Properties.Items.Add(con.dbr["personel"].ToString().Trim());  }
                con.dbr.Close();


                tkumas.Properties.Items.Clear();
                con.LoginQuery("select personel from personel where  (mgrup like '%" + secili_musteri.Text.Trim() + "%' or mgrup like '%HEPSİ%') and (keywords like '%kumaş%' or aciklama like '%kumaş%')");
                while (con.dbr.Read())
                { tkumas.Properties.Items.Add(con.dbr["personel"].ToString().Trim()); }
                con.dbr.Close();

                tmodelhane.Properties.Items.Clear();
                con.LoginQuery("select personel from personel where  (mgrup like '%" + secili_musteri.Text.Trim() + "%' or mgrup like '%HEPSİ%') and (keywords like '%modelhane%' or aciklama like '%modelhane%')");
                while (con.dbr.Read())
                { tmodelhane.Properties.Items.Add(con.dbr["personel"].ToString().Trim()); }
                con.dbr.Close();


                turetim.Properties.Items.Clear();
                con.LoginQuery("select personel from personel where ( mgrup like '%" + secili_musteri.Text.Trim() + "%' or mgrup like '%HEPSİ%') and (keywords like '%üretim%' or aciklama like '%üretim%')");
                while (con.dbr.Read())
                { turetim.Properties.Items.Add(con.dbr["personel"].ToString().Trim()); }
                con.dbr.Close();


                tlabratuar.Properties.Items.Clear();
                con.LoginQuery("select personel from personel where  (mgrup like '%" + secili_musteri.Text.Trim() + "%' or mgrup like '%HEPSİ%') and (keywords like '%laboratuvar%' or aciklama like '%laboratuvar%')");
                while (con.dbr.Read())
                { tlabratuar.Properties.Items.Add(con.dbr["personel"].ToString().Trim()); }
                con.dbr.Close();


                tkalite.Properties.Items.Clear();
                con.LoginQuery("select personel from personel where  (mgrup like '%" + secili_musteri.Text.Trim() + "%' or mgrup like '%HEPSİ%') and (keywords like '%kalite%' or aciklama like '%kalite%')");
                while (con.dbr.Read())
                { tkalite.Properties.Items.Add(con.dbr["personel"].ToString().Trim()); }
                con.dbr.Close();


                con.Close();


            }
            catch (Exception Ex)
            {
                MessageBox.Show("Bir sorun oluştu (durumtakip_siparisyetkili), lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                LogMessageToFile("durumtakip_siparisyetkili Listesi Oluşturulurken Hata Oluştu " + Environment.NewLine + Ex.ToString());

            }

        }
        void doldur() {

        }




 
        private void btn_msjgonder_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {

                con = new SqlDbConnect();

                /*con.LoginQuery("select * from durumtakip_sipyetkili where siparisno='" + secili_siparis.Text.Trim() + "'");
                                       while (con.dbr.Read())
                                       {
                                           taksesuar.Text = con.dbr["aksesuar"].ToString().Trim();
                                           tkumas.Text = con.dbr["kumas"].ToString().Trim();

                                           tmodelhane.Text = con.dbr["modelhane"].ToString().Trim();
                                           turetim.Text = con.dbr["uretim"].ToString().Trim();

                                           tlabratuar.Text = con.dbr["labratuar"].ToString().Trim();
                                           tkalite.Text = con.dbr["kalite"].ToString().Trim();*/


                con.SqlQuery("UPDATE durumtakip_sipyetkili SET "+
                    "aksesuar=@aksesuar,aksesuar_mail=@aksesuar_mail,aksesuar_keywords=@aksesuar_keywords," +
                    "kumas=@kumas,kumas_mail=@kumas_mail,kumas_keywords=@kumas_keywords," +
                     "modelhane=@modelhane,modelhane_mail=@modelhane_mail,modelhane_keywords=@modelhane_keywords," +
                     "uretim=@uretim,uretim_mail=@uretim_mail,uretim_keywords=@uretim_keywords," +
                     "labratuar=@labratuar,labratuar_mail=@labratuar_mail,labratuar_keywords=@labratuar_keywords," +
                     "kalite=@kalite,kalite_mail=@kalite_mail,kalite_keywords=@kalite_keywords," +
                     "ekname=@ekname,ektarih=@ektarih" +
                     " where siparisno='" + secili_siparis.Text.Trim() + "'");

                con.Cmd.Parameters.AddWithValue("@aksesuar", taksesuar.Text.Trim());
                con.Cmd.Parameters.AddWithValue("@aksesuar_mail", taksesuar_mail.Text.Trim());
                con.Cmd.Parameters.AddWithValue("@aksesuar_keywords", taksesuar_keywords.Text.Trim());
                con.Cmd.Parameters.AddWithValue("@kumas", tkumas.Text.Trim());
                con.Cmd.Parameters.AddWithValue("@kumas_mail", tkumas_mail.Text.Trim());
                con.Cmd.Parameters.AddWithValue("@kumas_keywords", tkumas_keywords.Text.Trim());
                con.Cmd.Parameters.AddWithValue("@modelhane", tmodelhane.Text.Trim());
                con.Cmd.Parameters.AddWithValue("@modelhane_mail", tmodelhane_mail.Text.Trim());
                con.Cmd.Parameters.AddWithValue("@modelhane_keywords", tmodelhane_keywords.Text.Trim());

                con.Cmd.Parameters.AddWithValue("@uretim", turetim.Text.Trim());
                con.Cmd.Parameters.AddWithValue("@uretim_mail", turetim_mail.Text.Trim());
                con.Cmd.Parameters.AddWithValue("@uretim_keywords", turetim_keywords.Text.Trim());

                con.Cmd.Parameters.AddWithValue("@labratuar", tlabratuar.Text.Trim());
                con.Cmd.Parameters.AddWithValue("@labratuar_mail", tlabratuar_mail.Text.Trim());
                con.Cmd.Parameters.AddWithValue("@labratuar_keywords", tlabratuar_keywords.Text.Trim());

                con.Cmd.Parameters.AddWithValue("@kalite", tkalite.Text.Trim());
                con.Cmd.Parameters.AddWithValue("@kalite_mail", tkalite_mail.Text.Trim());
                con.Cmd.Parameters.AddWithValue("@kalite_keywords", tkalite_keywords.Text.Trim());

                
                con.Cmd.Parameters.AddWithValue("@ekname", cInfo.cUsername);
                con.Cmd.Parameters.AddWithValue("@ektarih", DateTime.Now);
                con.QueryNonEx();

                con.Close();


                MessageBox.Show("Güncelleme Yapıldı");
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Bir sorun oluştu (durumtakip_siparisyetkili update), lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                LogMessageToFile("durumtakip_siparisyetkili update Oluşturulurken Hata Oluştu " + Environment.NewLine + Ex.ToString());

            }

        }

        private void btn_temizle_Click(object sender, EventArgs e)
        {

        }

        

        private void taksesuar_SelectedIndexChanged(object sender, EventArgs e)
        {
            taksesuar_verileri();
        }
        void taksesuar_verileri() {
            try
            {
                con = new SqlDbConnect();
                //select personel from personel where  ( mgrup like '%COS%' or mgrup like '%HEPSİ%') and (keywords like '%kumaş%' or aciklama like '%kumaş%')

                con.LoginQuery("select email,keywords from personel where personel='" + taksesuar.Text.Trim() + "'");
                while (con.dbr.Read())
                {

                    taksesuar_mail.Text = con.dbr["email"].ToString().Trim();
                    taksesuar_keywords.Text = con.dbr["keywords"].ToString().Trim();
                }
                con.dbr.Close();
                con.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Bir sorun oluştu (durumtakip_siparisyetkili-akseuar seletbox), lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                LogMessageToFile("durumtakip_siparisyetkili  taksesuar Oluşturulurken Hata Oluştu " + Environment.NewLine + Ex.ToString());

            }
        }
        private void tkumas_SelectedIndexChanged(object sender, EventArgs e)
        {
            tkumas_verileri();


        }
        void tkumas_verileri() {
            try
            {
                con = new SqlDbConnect();

                con.LoginQuery("select email,keywords from personel where personel='" + tkumas.Text.Trim() + "'");
                while (con.dbr.Read())
                {

                    tkumas_mail.Text = con.dbr["email"].ToString().Trim();
                    tkumas_keywords.Text = con.dbr["keywords"].ToString().Trim();
                }
                con.dbr.Close();
                con.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Bir sorun oluştu (durumtakip_siparisyetkili-tkumas seletbox), lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                LogMessageToFile("durumtakip_siparisyetkili  tkumas Listesi Oluşturulurken Hata Oluştu " + Environment.NewLine + Ex.ToString());

            }
        }
        private void tmodelhane_SelectedIndexChanged(object sender, EventArgs e)
        {
            tmodelhane_verileri();
        }

        void tmodelhane_verileri() {

            try
            {
                con = new SqlDbConnect();

                con.LoginQuery("select email,keywords from personel where personel='" + tmodelhane.Text.Trim() + "'");
                while (con.dbr.Read())
                {

                    tmodelhane_mail.Text = con.dbr["email"].ToString().Trim();
                    tmodelhane_keywords.Text = con.dbr["keywords"].ToString().Trim();
                }
                con.dbr.Close();
                con.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Bir sorun oluştu (durumtakip_siparisyetkili-tmodelhane seletbox), lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                LogMessageToFile("durumtakip_siparisyetkili  tmodelhane Listesi Oluşturulurken Hata Oluştu " + Environment.NewLine + Ex.ToString());

            }

        }

        private void turetim_SelectedIndexChanged(object sender, EventArgs e)
        {
            turetim_verileri();
        }
        void turetim_verileri() {
            try
            {
                con = new SqlDbConnect();

                con.LoginQuery("select email,keywords from personel where personel='" + turetim.Text.Trim() + "'");
                while (con.dbr.Read())
                {

                    turetim_mail.Text = con.dbr["email"].ToString().Trim();
                    turetim_keywords.Text = con.dbr["keywords"].ToString().Trim();
                }
                con.dbr.Close();
                con.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Bir sorun oluştu (durumtakip_siparisyetkili-turetim seletbox), lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                LogMessageToFile("durumtakip_siparisyetkili  turetim Listesi Oluşturulurken Hata Oluştu " + Environment.NewLine + Ex.ToString());

            }
        }

        private void tlabratuar_SelectedIndexChanged(object sender, EventArgs e)
        {
            tlabratuar_verileri();
        }
        void tlabratuar_verileri() {
            try
            {
                con = new SqlDbConnect();

                con.LoginQuery("select email,keywords from personel where personel='" + tlabratuar.Text.Trim() + "'");
                while (con.dbr.Read())
                {

                    tlabratuar_mail.Text = con.dbr["email"].ToString().Trim();
                    tlabratuar_keywords.Text = con.dbr["keywords"].ToString().Trim();
                }
                con.dbr.Close();
                con.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Bir sorun oluştu (durumtakip_siparisyetkili-tlabratuar seletbox), lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                LogMessageToFile("durumtakip_siparisyetkili  tlabratuar Listesi Oluşturulurken Hata Oluştu " + Environment.NewLine + Ex.ToString());

            }
        }

        private void tkalite_SelectedIndexChanged(object sender, EventArgs e)
        {
            tkalite_verileri();
        }
         void tkalite_verileri()
        {
            try
            {
                con = new SqlDbConnect();

                con.LoginQuery("select email,keywords from personel where personel='" + tkalite.Text.Trim() + "'");
                while (con.dbr.Read())
                {

                    tkalite_mail.Text = con.dbr["email"].ToString().Trim();
                    tkalite_keywords.Text = con.dbr["keywords"].ToString().Trim();
                }
                con.dbr.Close();
                con.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Bir sorun oluştu (durumtakip_siparisyetkili-tlabratuar seletbox), lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                LogMessageToFile("durumtakip_siparisyetkili  tlabratuar Listesi Oluşturulurken Hata Oluştu " + Environment.NewLine + Ex.ToString());

            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            taksesuar_verileri();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            tkumas_verileri();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            tmodelhane_verileri();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            turetim_verileri();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            tlabratuar_verileri();
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            tkalite_verileri();
        }



        DataTable table = new DataTable();


        void BuildDataTable()
        {
            try
            {
                gridControl1.DataSource = null;


                table.Columns.Clear();
            table.Rows.Clear();
 
         
            table.Columns.Add("personel");
            table.Columns.Add("aciklama");
            table.Columns.Add("email");
            table.Columns.Add("mgrup");
            table.Columns.Add("keywords");

        
                con = new SqlDbConnect();

                con.LoginQuery("select personel,aciklama,email,mgrup,keywords from personel with (NOLOCK) where ayrildi <> 'E' ");
                while (con.dbr.Read())
                { //  tlabratuar_mail.Text = con.dbr["email"].ToString().Trim();
                    table.Rows.Add(new Object[] { con.dbr["personel"].ToString().Trim(), con.dbr["aciklama"].ToString().Trim(), con.dbr["email"].ToString().Trim(), con.dbr["mgrup"].ToString().Trim(), con.dbr["keywords"].ToString().Trim()});
                }
                con.dbr.Close();
                con.Close();
         


           

           // return table;


            gridControl1.DataSource = table;

            }
            catch (Exception Ex)
            {
                MessageBox.Show("Bir sorun oluştu (durumtakip_siparisyetkili-BuildDataTable ), lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                LogMessageToFile("durumtakip_siparisyetkili  BuildDataTable Listesi Oluşturulurken Hata Oluştu " + Environment.NewLine + Ex.ToString());

            }
        }


    }
}
