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
    public partial class durumtakip_aciklamalar : Form
    {
         SqlDbConnect con;
        durumtakip_aktif AnaForm_F = new durumtakip_aktif();
        int msj_id = 0;
        public durumtakip_aciklamalar()
        {
            InitializeComponent();
        }
        private static readonly string LOG_FILENAME = Path.GetTempPath() + "AldersLog.txt";

        public static void LogMessageToFile(string msg)

        {

            msg = string.Format("{0:G}: {1}rn ------->", DateTime.Now, msg);

            File.AppendAllText(LOG_FILENAME, msg);

        }

        private void durumtakip_aciklamalar_Load(object sender, EventArgs e)
        { ilk(); }

        void ilk() {
            if (secili_siparis.Text.Trim() == "") { MessageBox.Show("Bir sipariş seçilmemiş"); return; }
            else {
                try
                {
                    byte varmi = 0;
                con = new SqlDbConnect();


                con.LoginQuery("select * from durumtakip_aciklamalogs where siparisno='" + secili_siparis.Text.Trim()+"'");
                while (con.dbr.Read())
                {
                    if (con.dbr["siparisno"].ToString().Trim() != "") { varmi = 1; }
                }
                con.dbr.Close();




                if (varmi == 0)
                {

                    con.SqlQuery("INSERT INTO durumtakip_aciklamalogs (siparisno) VALUES (@siparisno)");
                    con.Cmd.Parameters.AddWithValue("@siparisno", secili_siparis.Text.Trim());
                    con.QueryNonEx();

                }
                else {
                    con.LoginQuery("select * from durumtakip_aciklamalogs where siparisno='" + secili_siparis.Text.Trim() + "'");
                    while (con.dbr.Read())
                    {
                        ac_sonname.Text = con.dbr["ac_name"].ToString().Trim();
                        ac_sontarih.Text = con.dbr["ac_date"].ToString().Trim();

                            pl_sonname.Text = con.dbr["pl_name"].ToString().Trim();
                            pl_sontarih.Text = con.dbr["pl_date"].ToString().Trim();

                            ku_sonname.Text = con.dbr["kum_name"].ToString().Trim();
                            ku_sontarih.Text = con.dbr["kum_date"].ToString().Trim();

                            kes_sonname.Text = con.dbr["kes_name"].ToString().Trim();
                            kes_sontarih.Text = con.dbr["kes_date"].ToString().Trim();

                             olcu_sonname.Text = con.dbr["olcu_name"].ToString().Trim();
                             olcu_sontarih.Text = con.dbr["olcu_date"].ToString().Trim();

                            mal_sonname.Text = con.dbr["malz_name"].ToString().Trim();
                            mal_sontarih.Text = con.dbr["malz_date"].ToString().Trim();

                            sevk_sonname.Text = con.dbr["sevk_name"].ToString().Trim();
                            sevk_sontarih.Text = con.dbr["sevk_date"].ToString().Trim();

                            kap_sonname.Text = con.dbr["kapanis_name"].ToString().Trim();
                            kap_sontarih.Text = con.dbr["kapanis_date"].ToString().Trim();

                            fin_sonname.Text = con.dbr["fin_name"].ToString().Trim();
                            fin_sontarih.Text = con.dbr["fin_date"].ToString().Trim();

                            ur_sonname.Text = con.dbr["ur_name"].ToString().Trim();
                            ur_sontarih.Text = con.dbr["ur_date"].ToString().Trim();

                        }
                    con.dbr.Close();


                }
                 
                 

                con.Close();


            }
                catch (Exception Ex)
            {
                MessageBox.Show("Bir sorun oluştu (durumtakip_aciklamalogs), lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    LogMessageToFile("durumtakip_aciklamalogs Listesi Oluşturulurken Hata Oluştu " + Environment.NewLine + Ex.ToString());

                }

        }


            doldur();
        }
        void doldur() {



            try
            {
                
                con = new SqlDbConnect();


                con.LoginQuery("select * from siparis where kullanicisipno='" + secili_siparis.Text.Trim() + "'");
                while (con.dbr.Read())
                {
                   

                    r_aciklama.Text = con.dbr["genelnotlar"].ToString().Trim();

                    pl_aciklama.Text = con.dbr["planlamanotlari"].ToString().Trim();

                    ku_aciklama.Text = con.dbr["kumasnotlari"].ToString().Trim();
                    kes_aciklama.Text = con.dbr["kesimnotlari"].ToString().Trim();
                    olcu_aciklama.Text = con.dbr["olcunotlari"].ToString().Trim();
                    mal_aciklama.Text = con.dbr["malzemenotlari"].ToString().Trim();
                    sevk_aciklama.Text = con.dbr["sevkiyatnotlari"].ToString().Trim();
                    kap_aciklama.Text = con.dbr["kapanisnotlar"].ToString().Trim();
                    fin_aciklama.Text = con.dbr["parasalnotlar"].ToString().Trim();
                    ur_aciklama.Text = con.dbr["uretimnotlari"].ToString().Trim();

                }
                con.dbr.Close();


                con.LoginQuery("select * from durumtakip_aciklamalogs where siparisno='" + secili_siparis.Text.Trim() + "'");
                while (con.dbr.Read())
                {
                    ac_sonname.Text = con.dbr["ac_name"].ToString().Trim();
                    ac_sontarih.Text = con.dbr["ac_date"].ToString().Trim();

                    pl_sonname.Text = con.dbr["pl_name"].ToString().Trim();
                    pl_sontarih.Text = con.dbr["pl_date"].ToString().Trim();

                    ku_sonname.Text = con.dbr["kum_name"].ToString().Trim();
                    ku_sontarih.Text = con.dbr["kum_date"].ToString().Trim();

                    kes_sonname.Text = con.dbr["kes_name"].ToString().Trim();
                    kes_sontarih.Text = con.dbr["kes_date"].ToString().Trim();

                    olcu_sonname.Text = con.dbr["olcu_name"].ToString().Trim();
                    olcu_sontarih.Text = con.dbr["olcu_date"].ToString().Trim();

                    mal_sonname.Text = con.dbr["malz_name"].ToString().Trim();
                    mal_sontarih.Text = con.dbr["malz_date"].ToString().Trim();

                    sevk_sonname.Text = con.dbr["sevk_name"].ToString().Trim();
                    sevk_sontarih.Text = con.dbr["sevk_date"].ToString().Trim();

                    kap_sonname.Text = con.dbr["kapanis_name"].ToString().Trim();
                    kap_sontarih.Text = con.dbr["kapanis_date"].ToString().Trim();

                    fin_sonname.Text = con.dbr["fin_name"].ToString().Trim();
                    fin_sontarih.Text = con.dbr["fin_date"].ToString().Trim();

                    ur_sonname.Text = con.dbr["ur_name"].ToString().Trim();
                    ur_sontarih.Text = con.dbr["ur_date"].ToString().Trim();

                }
                con.dbr.Close();




                con.Close();


            }
            catch (Exception Ex)
            {
                MessageBox.Show("Bir sorun oluştu (genelnotlar), lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                LogMessageToFile("siparis genelnotlar Listesi Oluşturulurken Hata Oluştu " + Environment.NewLine + Ex.ToString());

            }



        }
        private void btn_ac_msjgonder_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {

                con = new SqlDbConnect();


                con.SqlQuery("UPDATE siparis SET  genelnotlar=@genelnotlar where kullanicisipno='" + secili_siparis.Text.Trim() + "'");
                con.Cmd.Parameters.AddWithValue("@genelnotlar", r_aciklama.Text.Trim());
                con.QueryNonEx();



                con.SqlQuery("UPDATE durumtakip_aciklamalogs SET  ac_name=@ac_name,ac_date=@ac_date where siparisno='" + secili_siparis.Text.Trim() + "'");
                con.Cmd.Parameters.AddWithValue("@ac_name", cInfo.cUsername);
                con.Cmd.Parameters.AddWithValue("@ac_date", DateTime.Now);
                con.QueryNonEx();




                string tum_mailler = txt_gonmails.Text.Trim();

                string Subject = secili_siparis.Text + " / " + secili_modeladi.Text + " Açıklamalardan > Genel Açıklama Hakkında ";


                con.SqlQuery("INSERT INTO durumtakip_ms (orderno,uname,umail,mesaj,tarih,udepartman,bilgi) VALUES (@orderno,@uname,@umail,@mesaj,@tarih,@udepartman,@bilgi)");
                con.Cmd.Parameters.AddWithValue("@orderno", secili_siparis.Text);
                con.Cmd.Parameters.AddWithValue("@uname", cInfo.cUsername);
                con.Cmd.Parameters.AddWithValue("@umail", cInfo.cUsermail);
                con.Cmd.Parameters.AddWithValue("@mesaj", "Açıklamalardan > Genel Açıklama: " + r_aciklama.Text.Trim());
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

                con.QueryNonEx();

                Heryerden.Gun1 = 1;




                msj_id = 0;
                con.LoginQuery("SELECT MAX(id) FROM durumtakip_ms");

                while (con.dbr.Read())
                {     msj_id = (int)con.dbr[0];  }
                con.dbr.Close();

                //  SELECT job_id FROM jobs WHERE job_id = @@IDENTITY;


                con.Close();






                if (tum_mailler.Trim() != "")
                {

                    //MessageBox.Show("mail_dizi->"+ mail_dizi.Count.ToString() + "/cInfo.cUsermail->"+ cInfo.cUsermail + "/cUsermailpass " + cInfo.cUsermailpass + " /cUsername" + cInfo.cUsername);

                    try
                    {

                        AnaForm_F.SendEmail(tum_mailler, Subject, secili_siparis.Text.Trim(),"");
                        MessageBox.Show("Mail ve mesajınız Gönderildi !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LogMessageToFile("Mail gönderilemedi " + cInfo.cUsername + "/" + cInfo.cUsermail + "/" + cInfo.cUsermailpass + "/" + Environment.NewLine + Ex.ToString());


                        try
                        {
                            Heryerden.Sonuc = 0;
                            AnaForm_F.kuyrukkayda_al(tum_mailler, secili_siparis.Text.Trim());


                            if (Heryerden.Sonuc == 1)
                            {
                                MessageBox.Show("Mesajınız kayıt edildi, Mailiniz kuyruğa alındı en kısa sürede sizin adınıza gönderilecektir !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }

                        }
                        catch (Exception Es)
                        {
                            MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Es.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            MessageBox.Show("Mailiniz kuyruğa kayıt edilemedi, mailler gönderilemedi. Yazılım danışmanına bildiriniz", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            LogMessageToFile("Mail gönderilemedi - kuyruğada alınmadı. işlem iptal " + cInfo.cUsername + "/" + cInfo.cUsermail + "/" + cInfo.cUsermailpass + "/" + Environment.NewLine + Ex.ToString());


                        }

                    }


                }




            }
            catch (Exception Ex)
            {
                MessageBox.Show("Bir sorun oluştu (aciklama girerken), lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                LogMessageToFile("siparis aciklama girerken Hata Oluştu " + Environment.NewLine + Ex.ToString());

            }



            doldur();
            Cursor.Current = Cursors.Default;
        }

        private void pl_btngonder_Click(object sender, EventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;
            try
            {

                con = new SqlDbConnect();
                con.SqlQuery("UPDATE siparis SET  planlamanotlari=@planlamanotlari where kullanicisipno='" + secili_siparis.Text.Trim() + "'");
                con.Cmd.Parameters.AddWithValue("@planlamanotlari", pl_aciklama.Text.Trim());
                con.QueryNonEx();

                con.SqlQuery("UPDATE durumtakip_aciklamalogs SET  pl_name=@pl_name,pl_date=@pl_date where siparisno='" + secili_siparis.Text.Trim() + "'");
                con.Cmd.Parameters.AddWithValue("@pl_name", cInfo.cUsername);
                con.Cmd.Parameters.AddWithValue("@pl_date", DateTime.Now);
                con.QueryNonEx();

                string tum_mailler = txt_gonmails.Text.Trim();

                string Subject = secili_siparis.Text + " / " + secili_modeladi.Text + " Açıklamalardan > Planlama Açıklama Hakkında ";


                con.SqlQuery("INSERT INTO durumtakip_ms (orderno,uname,umail,mesaj,tarih,udepartman,bilgi) VALUES (@orderno,@uname,@umail,@mesaj,@tarih,@udepartman,@bilgi)");
                con.Cmd.Parameters.AddWithValue("@orderno", secili_siparis.Text);
                con.Cmd.Parameters.AddWithValue("@uname", cInfo.cUsername);
                con.Cmd.Parameters.AddWithValue("@umail", cInfo.cUsermail);
                con.Cmd.Parameters.AddWithValue("@mesaj", "Açıklamalardan > Planlama Açıklama: " + pl_aciklama.Text.Trim());
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

                con.QueryNonEx();
                Heryerden.Gun1 = 1;




                msj_id = 0;
                con.LoginQuery("SELECT MAX(id) FROM durumtakip_ms");

                while (con.dbr.Read())
                {

                    msj_id = (int)con.dbr[0];

                }
                con.dbr.Close();
                //  SELECT job_id FROM jobs WHERE job_id = @@IDENTITY;


                con.Close();






                if (tum_mailler.Trim() != "")
                {

                    //MessageBox.Show("mail_dizi->"+ mail_dizi.Count.ToString() + "/cInfo.cUsermail->"+ cInfo.cUsermail + "/cUsermailpass " + cInfo.cUsermailpass + " /cUsername" + cInfo.cUsername);

                    try
                    {

                        AnaForm_F.SendEmail(tum_mailler, Subject, secili_siparis.Text.Trim(),"");
                        MessageBox.Show("Mail ve mesajınız Gönderildi !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LogMessageToFile("Mail gönderilemedi " + cInfo.cUsername + "/" + cInfo.cUsermail + "/" + cInfo.cUsermailpass + "/" + Environment.NewLine + Ex.ToString());


                        try
                        {
                            Heryerden.Sonuc = 0;
                            AnaForm_F.kuyrukkayda_al(tum_mailler, secili_siparis.Text.Trim());


                            if (Heryerden.Sonuc == 1)
                            {
                                MessageBox.Show("Mesajınız kayıt edildi, Mailiniz kuyruğa alındı en kısa sürede sizin adınıza gönderilecektir !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }

                        }
                        catch (Exception Es)
                        {
                            MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Es.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            MessageBox.Show("Mailiniz kuyruğa kayıt edilemedi, mailler gönderilemedi. Yazılım danışmanına bildiriniz", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            LogMessageToFile("Mail gönderilemedi - kuyruğada alınmadı. işlem iptal " + cInfo.cUsername + "/" + cInfo.cUsermail + "/" + cInfo.cUsermailpass + "/" + Environment.NewLine + Ex.ToString());


                        }

                    }


                }




            }
            catch (Exception Ex)
            {
                MessageBox.Show("Bir sorun oluştu (aciklama planlama girerken), lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                LogMessageToFile("siparis aciklama planlama girerken Hata Oluştu " + Environment.NewLine + Ex.ToString());

            }



            doldur();
            Cursor.Current = Cursors.Default;
        }

        private void ku_btngonder_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {

                con = new SqlDbConnect();
                con.SqlQuery("UPDATE siparis SET  kumasnotlari=@kumasnotlari where kullanicisipno='" + secili_siparis.Text.Trim() + "'");
                con.Cmd.Parameters.AddWithValue("@kumasnotlari", ku_aciklama.Text.Trim());
                con.QueryNonEx();

                con.SqlQuery("UPDATE durumtakip_aciklamalogs SET  kum_name=@kum_name,kum_date=@kum_date where siparisno='" + secili_siparis.Text.Trim() + "'");
                con.Cmd.Parameters.AddWithValue("@kum_name", cInfo.cUsername);
                con.Cmd.Parameters.AddWithValue("@kum_date", DateTime.Now);
                con.QueryNonEx();




                string tum_mailler = txt_gonmails.Text.Trim();

                string Subject = secili_siparis.Text + " / " + secili_modeladi.Text + " Açıklamalardan > Kumaş Açıklama ";


                con.SqlQuery("INSERT INTO durumtakip_ms (orderno,uname,umail,mesaj,tarih,udepartman,bilgi) VALUES (@orderno,@uname,@umail,@mesaj,@tarih,@udepartman,@bilgi)");
                con.Cmd.Parameters.AddWithValue("@orderno", secili_siparis.Text);
                con.Cmd.Parameters.AddWithValue("@uname", cInfo.cUsername);
                con.Cmd.Parameters.AddWithValue("@umail", cInfo.cUsermail);
                con.Cmd.Parameters.AddWithValue("@mesaj", "Açıklamalardan > Kumaş Açıklama: " + ku_aciklama.Text.Trim());
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

                con.QueryNonEx();
                Heryerden.Gun1 = 1;




                msj_id = 0;
                con.LoginQuery("SELECT MAX(id) FROM durumtakip_ms");

                while (con.dbr.Read())
                {

                    msj_id = (int)con.dbr[0];

                }
                con.dbr.Close();
                //  SELECT job_id FROM jobs WHERE job_id = @@IDENTITY;


                con.Close();






                if (tum_mailler.Trim() != "")
                {

                    //MessageBox.Show("mail_dizi->"+ mail_dizi.Count.ToString() + "/cInfo.cUsermail->"+ cInfo.cUsermail + "/cUsermailpass " + cInfo.cUsermailpass + " /cUsername" + cInfo.cUsername);

                    try
                    {

                        AnaForm_F.SendEmail(tum_mailler, Subject, secili_siparis.Text.Trim(),"");
                        MessageBox.Show("Mail ve mesajınız Gönderildi !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LogMessageToFile("Mail gönderilemedi " + cInfo.cUsername + "/" + cInfo.cUsermail + "/" + cInfo.cUsermailpass + "/" + Environment.NewLine + Ex.ToString());


                        try
                        {
                            Heryerden.Sonuc = 0;
                            AnaForm_F.kuyrukkayda_al(tum_mailler, secili_siparis.Text.Trim());


                            if (Heryerden.Sonuc == 1)
                            {
                                MessageBox.Show("Mesajınız kayıt edildi, Mailiniz kuyruğa alındı en kısa sürede sizin adınıza gönderilecektir !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }

                        }
                        catch (Exception Es)
                        {
                            MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Es.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            MessageBox.Show("Mailiniz kuyruğa kayıt edilemedi, mailler gönderilemedi. Yazılım danışmanına bildiriniz", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            LogMessageToFile("Mail gönderilemedi - kuyruğada alınmadı. işlem iptal " + cInfo.cUsername + "/" + cInfo.cUsermail + "/" + cInfo.cUsermailpass + "/" + Environment.NewLine + Ex.ToString());


                        }

                    }


                }




            }
            catch (Exception Ex)
            {
                MessageBox.Show("Bir sorun oluştu (aciklama kumaş girerken), lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                LogMessageToFile("siparis aciklama kumaş girerken Hata Oluştu " + Environment.NewLine + Ex.ToString());

            }



            doldur();
            Cursor.Current = Cursors.Default;

        }

        private void kes_btngonder_Click(object sender, EventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;
            try
            {

                con = new SqlDbConnect();
                con.SqlQuery("UPDATE siparis SET  kesimnotlari=@kesimnotlari where kullanicisipno='" + secili_siparis.Text.Trim() + "'");
                con.Cmd.Parameters.AddWithValue("@kesimnotlari", kes_aciklama.Text.Trim());
                con.QueryNonEx();

                con.SqlQuery("UPDATE durumtakip_aciklamalogs SET  kes_name=@kes_name,kes_date=@kes_date where siparisno='" + secili_siparis.Text.Trim() + "'");
                con.Cmd.Parameters.AddWithValue("@kes_name", cInfo.cUsername);
                con.Cmd.Parameters.AddWithValue("@kes_date", DateTime.Now);
                con.QueryNonEx();




                string tum_mailler = txt_gonmails.Text.Trim();

                string Subject = secili_siparis.Text + " / " + secili_modeladi.Text + " Açıklamalardan > Kesim Açıklama ";


                con.SqlQuery("INSERT INTO durumtakip_ms (orderno,uname,umail,mesaj,tarih,udepartman,bilgi) VALUES (@orderno,@uname,@umail,@mesaj,@tarih,@udepartman,@bilgi)");
                con.Cmd.Parameters.AddWithValue("@orderno", secili_siparis.Text);
                con.Cmd.Parameters.AddWithValue("@uname", cInfo.cUsername);
                con.Cmd.Parameters.AddWithValue("@umail", cInfo.cUsermail);
                con.Cmd.Parameters.AddWithValue("@mesaj", "Açıklamalardan > Kesim Açıklama: " + r_aciklama.Text.Trim());
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

                con.QueryNonEx();
                Heryerden.Gun1 = 1;




                msj_id = 0;
                con.LoginQuery("SELECT MAX(id) FROM durumtakip_ms");

                while (con.dbr.Read())
                {

                    msj_id = (int)con.dbr[0];

                }
                con.dbr.Close();
                //  SELECT job_id FROM jobs WHERE job_id = @@IDENTITY;


                con.Close();






                if (tum_mailler.Trim() != "")
                {

                    //MessageBox.Show("mail_dizi->"+ mail_dizi.Count.ToString() + "/cInfo.cUsermail->"+ cInfo.cUsermail + "/cUsermailpass " + cInfo.cUsermailpass + " /cUsername" + cInfo.cUsername);

                    try
                    {

                        AnaForm_F.SendEmail(tum_mailler, Subject, secili_siparis.Text.Trim(),"");
                        MessageBox.Show("Mail ve mesajınız Gönderildi !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LogMessageToFile("Mail gönderilemedi " + cInfo.cUsername + "/" + cInfo.cUsermail + "/" + cInfo.cUsermailpass + "/" + Environment.NewLine + Ex.ToString());


                        try
                        {
                            Heryerden.Sonuc = 0;
                            AnaForm_F.kuyrukkayda_al(tum_mailler, secili_siparis.Text.Trim());


                            if (Heryerden.Sonuc == 1)
                            {
                                MessageBox.Show("Mesajınız kayıt edildi, Mailiniz kuyruğa alındı en kısa sürede sizin adınıza gönderilecektir !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }

                        }
                        catch (Exception Es)
                        {
                            MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Es.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            MessageBox.Show("Mailiniz kuyruğa kayıt edilemedi, mailler gönderilemedi. Yazılım danışmanına bildiriniz", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            LogMessageToFile("Mail gönderilemedi - kuyruğada alınmadı. işlem iptal " + cInfo.cUsername + "/" + cInfo.cUsermail + "/" + cInfo.cUsermailpass + "/" + Environment.NewLine + Ex.ToString());


                        }

                    }


                }




            }
            catch (Exception Ex)
            {
                MessageBox.Show("Bir sorun oluştu (aciklama kesim girerken), lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                LogMessageToFile("siparis aciklama kesim girerken Hata Oluştu " + Environment.NewLine + Ex.ToString());

            }



            doldur();
            Cursor.Current = Cursors.Default;

        }

        private void olcu_btngonder_Click(object sender, EventArgs e)
        {
            //////////////////////////////////////////////////////////

            Cursor.Current = Cursors.WaitCursor;
            try
            {

                con = new SqlDbConnect();
                con.SqlQuery("UPDATE siparis SET  olcunotlari=@olcunotlari where kullanicisipno='" + secili_siparis.Text.Trim() + "'");
                con.Cmd.Parameters.AddWithValue("@olcunotlari", olcu_aciklama.Text.Trim());
                con.QueryNonEx();

                con.SqlQuery("UPDATE durumtakip_aciklamalogs SET  olcu_name=@olcu_name,olcu_date=@olcu_date where siparisno='" + secili_siparis.Text.Trim() + "'");
                con.Cmd.Parameters.AddWithValue("@olcu_name", cInfo.cUsername);
                con.Cmd.Parameters.AddWithValue("@olcu_date", DateTime.Now);
                con.QueryNonEx();




                string tum_mailler = txt_gonmails.Text.Trim();

                string Subject = secili_siparis.Text + " / " + secili_modeladi.Text + " Açıklamalardan > Ölçü Açıklama ";


                con.SqlQuery("INSERT INTO durumtakip_ms (orderno,uname,umail,mesaj,tarih,udepartman,bilgi) VALUES (@orderno,@uname,@umail,@mesaj,@tarih,@udepartman,@bilgi)");
                con.Cmd.Parameters.AddWithValue("@orderno", secili_siparis.Text);
                con.Cmd.Parameters.AddWithValue("@uname", cInfo.cUsername);
                con.Cmd.Parameters.AddWithValue("@umail", cInfo.cUsermail);
                con.Cmd.Parameters.AddWithValue("@mesaj", "Açıklamalardan > Ölçü Açıklama: " + olcu_aciklama.Text.Trim());
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

                con.QueryNonEx();
                Heryerden.Gun1 = 1;




                msj_id = 0;
                con.LoginQuery("SELECT MAX(id) FROM durumtakip_ms");

                while (con.dbr.Read())
                {

                    msj_id = (int)con.dbr[0];

                }
                con.dbr.Close();
                //  SELECT job_id FROM jobs WHERE job_id = @@IDENTITY;


                con.Close();






                if (tum_mailler.Trim() != "")
                {

                    //MessageBox.Show("mail_dizi->"+ mail_dizi.Count.ToString() + "/cInfo.cUsermail->"+ cInfo.cUsermail + "/cUsermailpass " + cInfo.cUsermailpass + " /cUsername" + cInfo.cUsername);

                    try
                    {

                        AnaForm_F.SendEmail(tum_mailler, Subject, secili_siparis.Text.Trim(),"");
                        MessageBox.Show("Mail ve mesajınız Gönderildi !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LogMessageToFile("Mail gönderilemedi " + cInfo.cUsername + "/" + cInfo.cUsermail + "/" + cInfo.cUsermailpass + "/" + Environment.NewLine + Ex.ToString());


                        try
                        {
                            Heryerden.Sonuc = 0;
                            AnaForm_F.kuyrukkayda_al(tum_mailler, secili_siparis.Text.Trim());


                            if (Heryerden.Sonuc == 1)
                            {
                                MessageBox.Show("Mesajınız kayıt edildi, Mailiniz kuyruğa alındı en kısa sürede sizin adınıza gönderilecektir !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }

                        }
                        catch (Exception Es)
                        {
                            MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Es.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            MessageBox.Show("Mailiniz kuyruğa kayıt edilemedi, mailler gönderilemedi. Yazılım danışmanına bildiriniz", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            LogMessageToFile("Mail gönderilemedi - kuyruğada alınmadı. işlem iptal " + cInfo.cUsername + "/" + cInfo.cUsermail + "/" + cInfo.cUsermailpass + "/" + Environment.NewLine + Ex.ToString());


                        }

                    }


                }




            }
            catch (Exception Ex)
            {
                MessageBox.Show("Bir sorun oluştu (aciklama Ölçü girerken), lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                LogMessageToFile("siparis aciklama Ölçü girerken Hata Oluştu " + Environment.NewLine + Ex.ToString());

            }



            doldur();
            Cursor.Current = Cursors.Default;


        }

        private void mal_btngonder_Click(object sender, EventArgs e)
        {//////////////////////////////////////////////////////////////

            Cursor.Current = Cursors.WaitCursor;
            try
            {

                con = new SqlDbConnect();
                con.SqlQuery("UPDATE siparis SET  malzemenotlari=@malzemenotlari where kullanicisipno='" + secili_siparis.Text.Trim() + "'");
                con.Cmd.Parameters.AddWithValue("@malzemenotlari", mal_aciklama.Text.Trim());
                con.QueryNonEx();

                con.SqlQuery("UPDATE durumtakip_aciklamalogs SET  malz_name=@malz_name,malz_date=@malz_date where siparisno='" + secili_siparis.Text.Trim() + "'");
                con.Cmd.Parameters.AddWithValue("@malz_name", cInfo.cUsername);
                con.Cmd.Parameters.AddWithValue("@malz_date", DateTime.Now);
                con.QueryNonEx();

                string tum_mailler = txt_gonmails.Text.Trim();

                string Subject = secili_siparis.Text + " / " + secili_modeladi.Text + " Açıklamalardan > Malzeme Açıklama ";


                con.SqlQuery("INSERT INTO durumtakip_ms (orderno,uname,umail,mesaj,tarih,udepartman,bilgi) VALUES (@orderno,@uname,@umail,@mesaj,@tarih,@udepartman,@bilgi)");
                con.Cmd.Parameters.AddWithValue("@orderno", secili_siparis.Text);
                con.Cmd.Parameters.AddWithValue("@uname", cInfo.cUsername);
                con.Cmd.Parameters.AddWithValue("@umail", cInfo.cUsermail);
                con.Cmd.Parameters.AddWithValue("@mesaj", "Açıklamalardan > Malzeme Açıklama: " + mal_aciklama.Text.Trim());
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

                con.QueryNonEx();
                Heryerden.Gun1 = 1;




                msj_id = 0;
                con.LoginQuery("SELECT MAX(id) FROM durumtakip_ms");

                while (con.dbr.Read())
                {

                    msj_id = (int)con.dbr[0];

                }
                con.dbr.Close();
                //  SELECT job_id FROM jobs WHERE job_id = @@IDENTITY;


                con.Close();






                if (tum_mailler.Trim() != "")
                {

                    //MessageBox.Show("mail_dizi->"+ mail_dizi.Count.ToString() + "/cInfo.cUsermail->"+ cInfo.cUsermail + "/cUsermailpass " + cInfo.cUsermailpass + " /cUsername" + cInfo.cUsername);

                    try
                    {

                        AnaForm_F.SendEmail(tum_mailler, Subject, secili_siparis.Text.Trim(),"");
                        MessageBox.Show("Mail ve mesajınız Gönderildi !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LogMessageToFile("Mail gönderilemedi " + cInfo.cUsername + "/" + cInfo.cUsermail + "/" + cInfo.cUsermailpass + "/" + Environment.NewLine + Ex.ToString());


                        try
                        {
                            Heryerden.Sonuc = 0;
                            AnaForm_F.kuyrukkayda_al(tum_mailler, secili_siparis.Text.Trim());


                            if (Heryerden.Sonuc == 1)
                            {
                                MessageBox.Show("Mesajınız kayıt edildi, Mailiniz kuyruğa alındı en kısa sürede sizin adınıza gönderilecektir !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }

                        }
                        catch (Exception Es)
                        {
                            MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Es.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            MessageBox.Show("Mailiniz kuyruğa kayıt edilemedi, mailler gönderilemedi. Yazılım danışmanına bildiriniz", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            LogMessageToFile("Mail gönderilemedi - kuyruğada alınmadı. işlem iptal " + cInfo.cUsername + "/" + cInfo.cUsermail + "/" + cInfo.cUsermailpass + "/" + Environment.NewLine + Ex.ToString());


                        }

                    }


                }




            }
            catch (Exception Ex)
            {
                MessageBox.Show("Bir sorun oluştu (aciklama Malzeme girerken), lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                LogMessageToFile("siparis aciklama Malzeme girerken Hata Oluştu " + Environment.NewLine + Ex.ToString());

            }



            doldur();
            Cursor.Current = Cursors.Default;


        }

        private void sevk_btngonder_Click(object sender, EventArgs e)
        {//////////////////////////////////////////////////////////////////
            Cursor.Current = Cursors.WaitCursor;
            try
            {

                con = new SqlDbConnect();
                con.SqlQuery("UPDATE siparis SET  sevkiyatnotlari=@sevkiyatnotlari where kullanicisipno='" + secili_siparis.Text.Trim() + "'");
                con.Cmd.Parameters.AddWithValue("@sevkiyatnotlari", sevk_aciklama.Text.Trim());
                con.QueryNonEx();

                con.SqlQuery("UPDATE durumtakip_aciklamalogs SET  sevk_name=@sevk_name,sevk_date=@sevk_date where siparisno='" + secili_siparis.Text.Trim() + "'");
                con.Cmd.Parameters.AddWithValue("@sevk_name", cInfo.cUsername);
                con.Cmd.Parameters.AddWithValue("@sevk_date", DateTime.Now);
                con.QueryNonEx();




                string tum_mailler = txt_gonmails.Text.Trim();

                string Subject = secili_siparis.Text + " / " + secili_modeladi.Text + " Açıklamalardan > Sevkiyat Açıklama ";


                con.SqlQuery("INSERT INTO durumtakip_ms (orderno,uname,umail,mesaj,tarih,udepartman,bilgi) VALUES (@orderno,@uname,@umail,@mesaj,@tarih,@udepartman,@bilgi)");
                con.Cmd.Parameters.AddWithValue("@orderno", secili_siparis.Text);
                con.Cmd.Parameters.AddWithValue("@uname", cInfo.cUsername);
                con.Cmd.Parameters.AddWithValue("@umail", cInfo.cUsermail);
                con.Cmd.Parameters.AddWithValue("@mesaj", "Açıklamalardan > Sevkiyat Açıklama: " + sevk_aciklama.Text.Trim());
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

                con.QueryNonEx();
                Heryerden.Gun1 = 1;




                msj_id = 0;
                con.LoginQuery("SELECT MAX(id) FROM durumtakip_ms");

                while (con.dbr.Read())
                {

                    msj_id = (int)con.dbr[0];

                }
                con.dbr.Close();
                //  SELECT job_id FROM jobs WHERE job_id = @@IDENTITY;


                con.Close();






                if (tum_mailler.Trim() != "")
                {

                    //MessageBox.Show("mail_dizi->"+ mail_dizi.Count.ToString() + "/cInfo.cUsermail->"+ cInfo.cUsermail + "/cUsermailpass " + cInfo.cUsermailpass + " /cUsername" + cInfo.cUsername);

                    try
                    {

                        AnaForm_F.SendEmail(tum_mailler, Subject, secili_siparis.Text.Trim(),"");
                        MessageBox.Show("Mail ve mesajınız Gönderildi !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LogMessageToFile("Mail gönderilemedi " + cInfo.cUsername + "/" + cInfo.cUsermail + "/" + cInfo.cUsermailpass + "/" + Environment.NewLine + Ex.ToString());


                        try
                        {
                            Heryerden.Sonuc = 0;
                            AnaForm_F.kuyrukkayda_al(tum_mailler, secili_siparis.Text.Trim());


                            if (Heryerden.Sonuc == 1)
                            {
                                MessageBox.Show("Mesajınız kayıt edildi, Mailiniz kuyruğa alındı en kısa sürede sizin adınıza gönderilecektir !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }

                        }
                        catch (Exception Es)
                        {
                            MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Es.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            MessageBox.Show("Mailiniz kuyruğa kayıt edilemedi, mailler gönderilemedi. Yazılım danışmanına bildiriniz", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            LogMessageToFile("Mail gönderilemedi - kuyruğada alınmadı. işlem iptal " + cInfo.cUsername + "/" + cInfo.cUsermail + "/" + cInfo.cUsermailpass + "/" + Environment.NewLine + Ex.ToString());


                        }

                    }


                }




            }
            catch (Exception Ex)
            {
                MessageBox.Show("Bir sorun oluştu (aciklama Sevkiyat girerken), lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                LogMessageToFile("siparis aciklama Sevkiyat girerken Hata Oluştu " + Environment.NewLine + Ex.ToString());

            }



            doldur();
            Cursor.Current = Cursors.Default;


        }

        private void kap_btngonder_Click(object sender, EventArgs e)
        {
            //////////////////////////////////

            Cursor.Current = Cursors.WaitCursor;
            try
            {

                con = new SqlDbConnect();
                con.SqlQuery("UPDATE siparis SET  kapanisnotlar=@kapanisnotlar where kullanicisipno='" + secili_siparis.Text.Trim() + "'");
                con.Cmd.Parameters.AddWithValue("@kapanisnotlar", kap_aciklama.Text.Trim());
                con.QueryNonEx();

                con.SqlQuery("UPDATE durumtakip_aciklamalogs SET  kapanis_name=@kapanis_name,kapanis_date=@kapanis_date where siparisno='" + secili_siparis.Text.Trim() + "'");
                con.Cmd.Parameters.AddWithValue("@kapanis_name", cInfo.cUsername);
                con.Cmd.Parameters.AddWithValue("@kapanis_date", DateTime.Now);
                con.QueryNonEx();




                string tum_mailler = txt_gonmails.Text.Trim();

                string Subject = secili_siparis.Text + " / " + secili_modeladi.Text + " Açıklamalardan > Kapanış Notu ";


                con.SqlQuery("INSERT INTO durumtakip_ms (orderno,uname,umail,mesaj,tarih,udepartman,bilgi) VALUES (@orderno,@uname,@umail,@mesaj,@tarih,@udepartman,@bilgi)");
                con.Cmd.Parameters.AddWithValue("@orderno", secili_siparis.Text);
                con.Cmd.Parameters.AddWithValue("@uname", cInfo.cUsername);
                con.Cmd.Parameters.AddWithValue("@umail", cInfo.cUsermail);
                con.Cmd.Parameters.AddWithValue("@mesaj", "Açıklamalardan > Kapanış Notu: " + kap_aciklama.Text.Trim());
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

                con.QueryNonEx();
                Heryerden.Gun1 = 1;




                msj_id = 0;
                con.LoginQuery("SELECT MAX(id) FROM durumtakip_ms");

                while (con.dbr.Read())
                {

                    msj_id = (int)con.dbr[0];

                }
                con.dbr.Close();
                //  SELECT job_id FROM jobs WHERE job_id = @@IDENTITY;


                con.Close();






                if (tum_mailler.Trim() != "")
                {

                    //MessageBox.Show("mail_dizi->"+ mail_dizi.Count.ToString() + "/cInfo.cUsermail->"+ cInfo.cUsermail + "/cUsermailpass " + cInfo.cUsermailpass + " /cUsername" + cInfo.cUsername);

                    try
                    {

                        AnaForm_F.SendEmail(tum_mailler, Subject, secili_siparis.Text.Trim(),"");
                        MessageBox.Show("Mail ve mesajınız Gönderildi !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LogMessageToFile("Mail gönderilemedi " + cInfo.cUsername + "/" + cInfo.cUsermail + "/" + cInfo.cUsermailpass + "/" + Environment.NewLine + Ex.ToString());


                        try
                        {
                            Heryerden.Sonuc = 0;
                            AnaForm_F.kuyrukkayda_al(tum_mailler, secili_siparis.Text.Trim());


                            if (Heryerden.Sonuc == 1)
                            {
                                MessageBox.Show("Mesajınız kayıt edildi, Mailiniz kuyruğa alındı en kısa sürede sizin adınıza gönderilecektir !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }

                        }
                        catch (Exception Es)
                        {
                            MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Es.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            MessageBox.Show("Mailiniz kuyruğa kayıt edilemedi, mailler gönderilemedi. Yazılım danışmanına bildiriniz", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            LogMessageToFile("Mail gönderilemedi - kuyruğada alınmadı. işlem iptal " + cInfo.cUsername + "/" + cInfo.cUsermail + "/" + cInfo.cUsermailpass + "/" + Environment.NewLine + Ex.ToString());


                        }

                    }


                }




            }
            catch (Exception Ex)
            {
                MessageBox.Show("Bir sorun oluştu (aciklama Kapanış Notu girerken), lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                LogMessageToFile("siparis aciklama Kapanış Notu girerken Hata Oluştu " + Environment.NewLine + Ex.ToString());

            }



            doldur();
            Cursor.Current = Cursors.Default;

        }

        private void fin_btngonder_Click(object sender, EventArgs e)
        {
            ////////////////////////////////////////////////////////

            Cursor.Current = Cursors.WaitCursor;
            try
            {

                con = new SqlDbConnect();
                con.SqlQuery("UPDATE siparis SET  parasalnotlar=@parasalnotlar where kullanicisipno='" + secili_siparis.Text.Trim() + "'");
                con.Cmd.Parameters.AddWithValue("@parasalnotlar", fin_aciklama.Text.Trim());
                con.QueryNonEx();

                con.SqlQuery("UPDATE durumtakip_aciklamalogs SET  fin_name=@fin_name,fin_date=@fin_date where siparisno='" + secili_siparis.Text.Trim() + "'");
                con.Cmd.Parameters.AddWithValue("@fin_name", cInfo.cUsername);
                con.Cmd.Parameters.AddWithValue("@fin_date", DateTime.Now);
                con.QueryNonEx();




                string tum_mailler = txt_gonmails.Text.Trim();

                string Subject = secili_siparis.Text + " / " + secili_modeladi.Text + " Açıklamalardan > Finans Açıklama ";


                con.SqlQuery("INSERT INTO durumtakip_ms (orderno,uname,umail,mesaj,tarih,udepartman,bilgi) VALUES (@orderno,@uname,@umail,@mesaj,@tarih,@udepartman,@bilgi)");
                con.Cmd.Parameters.AddWithValue("@orderno", secili_siparis.Text);
                con.Cmd.Parameters.AddWithValue("@uname", cInfo.cUsername);
                con.Cmd.Parameters.AddWithValue("@umail", cInfo.cUsermail);
                con.Cmd.Parameters.AddWithValue("@mesaj", "Açıklamalardan > Finans Açıklama: " + fin_aciklama.Text.Trim());
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

                con.QueryNonEx();
                Heryerden.Gun1 = 1;




                msj_id = 0;
                con.LoginQuery("SELECT MAX(id) FROM durumtakip_ms");

                while (con.dbr.Read())
                {

                    msj_id = (int)con.dbr[0];

                }
                con.dbr.Close();
                //  SELECT job_id FROM jobs WHERE job_id = @@IDENTITY;


                con.Close();






                if (tum_mailler.Trim() != "")
                {

                    //MessageBox.Show("mail_dizi->"+ mail_dizi.Count.ToString() + "/cInfo.cUsermail->"+ cInfo.cUsermail + "/cUsermailpass " + cInfo.cUsermailpass + " /cUsername" + cInfo.cUsername);

                    try
                    {

                        AnaForm_F.SendEmail(tum_mailler, Subject, secili_siparis.Text.Trim(),"");
                        MessageBox.Show("Mail ve mesajınız Gönderildi !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LogMessageToFile("Mail gönderilemedi " + cInfo.cUsername + "/" + cInfo.cUsermail + "/" + cInfo.cUsermailpass + "/" + Environment.NewLine + Ex.ToString());


                        try
                        {
                            Heryerden.Sonuc = 0;
                            AnaForm_F.kuyrukkayda_al(tum_mailler, secili_siparis.Text.Trim());


                            if (Heryerden.Sonuc == 1)
                            {
                                MessageBox.Show("Mesajınız kayıt edildi, Mailiniz kuyruğa alındı en kısa sürede sizin adınıza gönderilecektir !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }

                        }
                        catch (Exception Es)
                        {
                            MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Es.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            MessageBox.Show("Mailiniz kuyruğa kayıt edilemedi, mailler gönderilemedi. Yazılım danışmanına bildiriniz", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            LogMessageToFile("Mail gönderilemedi - kuyruğada alınmadı. işlem iptal " + cInfo.cUsername + "/" + cInfo.cUsermail + "/" + cInfo.cUsermailpass + "/" + Environment.NewLine + Ex.ToString());


                        }

                    }


                }




            }
            catch (Exception Ex)
            {
                MessageBox.Show("Bir sorun oluştu (aciklama Finans girerken), lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                LogMessageToFile("siparis aciklama Finans girerken Hata Oluştu " + Environment.NewLine + Ex.ToString());

            }



            doldur();
            Cursor.Current = Cursors.Default;

        }

        private void ur_btngonder_Click(object sender, EventArgs e)
        {
            ////////////////////////////////////////////////////////

            Cursor.Current = Cursors.WaitCursor;
            try
            {

                con = new SqlDbConnect();
                con.SqlQuery("UPDATE siparis SET  uretimnotlari=@uretimnotlari where kullanicisipno='" + secili_siparis.Text.Trim() + "'");
                con.Cmd.Parameters.AddWithValue("@uretimnotlari", ur_aciklama.Text.Trim());
                con.QueryNonEx();

                con.SqlQuery("UPDATE durumtakip_aciklamalogs SET  ur_name=@ur_name,ur_date=@ur_date where siparisno='" + secili_siparis.Text.Trim() + "'");
                con.Cmd.Parameters.AddWithValue("@ur_name", cInfo.cUsername);
                con.Cmd.Parameters.AddWithValue("@ur_date", DateTime.Now);
                con.QueryNonEx();




                string tum_mailler = txt_gonmails.Text.Trim();

                string Subject = secili_siparis.Text + " / " + secili_modeladi.Text + " Açıklamalardan > Üretim Açıklama ";


                con.SqlQuery("INSERT INTO durumtakip_ms (orderno,uname,umail,mesaj,tarih,udepartman,bilgi) VALUES (@orderno,@uname,@umail,@mesaj,@tarih,@udepartman,@bilgi)");
                con.Cmd.Parameters.AddWithValue("@orderno", secili_siparis.Text);
                con.Cmd.Parameters.AddWithValue("@uname", cInfo.cUsername);
                con.Cmd.Parameters.AddWithValue("@umail", cInfo.cUsermail);
                con.Cmd.Parameters.AddWithValue("@mesaj", "Açıklamalardan > Üretim Açıklama: " + ur_aciklama.Text.Trim());
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

                con.QueryNonEx();
                Heryerden.Gun1 = 1;




                msj_id = 0;
                con.LoginQuery("SELECT MAX(id) FROM durumtakip_ms");

                while (con.dbr.Read())
                {

                    msj_id = (int)con.dbr[0];

                }
                con.dbr.Close();
                //  SELECT job_id FROM jobs WHERE job_id = @@IDENTITY;


                con.Close();






                if (tum_mailler.Trim() != "")
                {

                    //MessageBox.Show("mail_dizi->"+ mail_dizi.Count.ToString() + "/cInfo.cUsermail->"+ cInfo.cUsermail + "/cUsermailpass " + cInfo.cUsermailpass + " /cUsername" + cInfo.cUsername);

                    try
                    {

                        AnaForm_F.SendEmail(tum_mailler, Subject, secili_siparis.Text.Trim(),"");
                        MessageBox.Show("Mail ve mesajınız Gönderildi !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LogMessageToFile("Mail gönderilemedi " + cInfo.cUsername + "/" + cInfo.cUsermail + "/" + cInfo.cUsermailpass + "/" + Environment.NewLine + Ex.ToString());


                        try
                        {
                            Heryerden.Sonuc = 0;
                            AnaForm_F.kuyrukkayda_al(tum_mailler, secili_siparis.Text.Trim());


                            if (Heryerden.Sonuc == 1)
                            {
                                MessageBox.Show("Mesajınız kayıt edildi, Mailiniz kuyruğa alındı en kısa sürede sizin adınıza gönderilecektir !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }

                        }
                        catch (Exception Es)
                        {
                            MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Es.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            MessageBox.Show("Mailiniz kuyruğa kayıt edilemedi, mailler gönderilemedi. Yazılım danışmanına bildiriniz", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            LogMessageToFile("Mail gönderilemedi - kuyruğada alınmadı. işlem iptal " + cInfo.cUsername + "/" + cInfo.cUsermail + "/" + cInfo.cUsermailpass + "/" + Environment.NewLine + Ex.ToString());


                        }

                    }


                }




            }
            catch (Exception Ex)
            {
                MessageBox.Show("Bir sorun oluştu (aciklama Üretim girerken), lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                LogMessageToFile("siparis aciklama Üretim girerken Hata Oluştu " + Environment.NewLine + Ex.ToString());

            }



            doldur();
            Cursor.Current = Cursors.Default;
        }
    }
}
