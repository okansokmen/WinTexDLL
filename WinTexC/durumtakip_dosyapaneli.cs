using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Collections; //  
namespace WinTexC
{
    public partial class durumtakip_dosyapaneli : Form
    {
        SqlDbConnect con;
        durumtakip_aktif AnaForm_F = new durumtakip_aktif();
        int msj_id = 0;
        public durumtakip_dosyapaneli()
        {
            InitializeComponent();
        }

        private static readonly string LOG_FILENAME = Path.GetTempPath() + "AldersLog.txt";

        public static void LogMessageToFile(string msg)

        {
             msg = string.Format("{0:G}: {1}rn ------->", DateTime.Now, msg);
             File.AppendAllText(LOG_FILENAME, msg);
         }


        private void ilk()
        {
            try
            {
                con = new SqlDbConnect();


                data.Rows.Clear();
                data.Refresh();

                data.ColumnCount = 6;
                data.ColumnHeadersVisible = true;

                // Sütun başlığına ait style tanımlıyoruz.
                DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
                columnHeaderStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                columnHeaderStyle.BackColor = Color.Red;
                columnHeaderStyle.ForeColor = Color.White;
                columnHeaderStyle.Font = new System.Drawing.Font("Calibri", 11, FontStyle.Bold);
                data.ColumnHeadersDefaultCellStyle = columnHeaderStyle;


                data.Columns[0].Name = "Dosya Adı";
                data.Columns[1].Name = "Açıklama";
                data.Columns[2].Name = "Ekleyen";
                data.Columns[3].Name = "Ekleme Tarihi";
                data.Columns[4].Name = "Dosya yolu";

                data.Columns[5].Name = "no";
                data.Columns[0].Width = 200;
                data.Columns[1].Width = 150;
                data.Columns[5].Width = 0;

                con.LoginQuery("SELECT * FROM durumtakip_files where orderno = '" + secili_siparis.Text.Trim() + "' ");
                while (con.dbr.Read())
                {
                    DataGridViewRow row = (DataGridViewRow)data.Rows[0].Clone();
                    row.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    row.DefaultCellStyle.Font = new System.Drawing.Font("Calibri", 10, FontStyle.Regular);

                    row.Cells[0].Value = con.dbr["dadi"].ToString().Trim();
                    row.Cells[1].Value = con.dbr["aciklama"].ToString().Trim();
                    row.Cells[2].Value = con.dbr["ekname"].ToString().Trim();
                    row.Cells[3].Value = String.Format("{0:dd-MM-yyyy HH:mm:ss}", con.dbr["ektarih"]).ToString();
                    row.Cells[4].Value = con.dbr["yolu"].ToString().Trim();
                    row.Cells[5].Value = con.dbr["id"].ToString().Trim();



                    data.Rows.Add(row);

                }
                con.dbr.Close();
                con.Close();
            }
            catch (Exception Ex)
            {
                LogMessageToFile("Dosya paneli  data row olmadı->" + secili_siparis.Text + "/" + cInfo.cUsername + "/" + Environment.NewLine + Ex.ToString());
                // MessageBox.Show("Bir sorun oluştu, dosya açılamadı lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



        }



        private void dosya_sec_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                OpenFileDialog op1 = new OpenFileDialog();
                op1.Multiselect = false;
                op1.ShowDialog();


                op1.Filter = "allfiles|*.jpg";


                if (new FileInfo(op1.FileName).Length > 8000000)
                {

                    //long fileSize = new FileInfo(op1.FileName).Length;
                    //MessageBox.Show("dosya boyutu > 7000 byte --->"+ fileSize.ToString());

                    MessageBox.Show("Dosya boyutu 7 MB lık limiti geçiyor. Lütfen küçültüp tekrar deneyiniz");

                    return;
                }
                else


                    file.Text = op1.FileName;



                if (op1.FileName == "" || file.Text == "") { return; }
                string[] FName;




                foreach (string s in op1.FileNames)
                {

                    FName = s.Split('\\');





                    //D:/Sentez/VOGUE/Resim/TESCO.gif

                    //MessageBox.Show(FName[FName.Length - 1]);
                    string newFilename = "";
                    Random rand = new Random();
                    string[] nfn = FName[FName.Length - 1].Split('.');
                    newFilename = Path.GetFileNameWithoutExtension(s) + "_"+secili_siparis.Text.Trim()+"_" + rand.Next(1, 10000) + "" + Path.GetExtension(s);

                    if (File.Exists(@Global_Config.dosyalarPath + newFilename))
                    {
                        // File.Delete(@Global_Config.dosyalarPath + newFilename);
                        MessageBox.Show("Bu dosya aynı isimle daha önce eklenmiştir. Dosya adını değiştirip tekrar deneyiniz.");
                        return;
                    }

                    File.Copy(@s, Global_Config.dosyalarPath + newFilename);
                    file.Text = Global_Config.dosyalarPath + newFilename;
                    MessageBox.Show("Dosya Eklenmiştir. Dosya adını doldurup Kaydet düğmesine basıp işlemi tamamlayınız.");

                }
            }
            catch (Exception Ex)
            {
                LogMessageToFile("Dosya paneli  file eklenemedi->" + secili_siparis.Text + "/" + cInfo.cUsername + "/" + Environment.NewLine + Ex.ToString());
                // MessageBox.Show("Bir sorun oluştu, dosya açılamadı lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        public void Temizle()
        {
            dadi.Text = "";


            file.Text = "";
            dacik.Text = "";
            did.Text = "";
            //dyol.Text = "";
            dac.Visible = false;
            dsil.Visible = false;
          //  dsave.Visible = false;
            // btn_onay.Visible = false;

        }

        private void btn_msjgonder_Click(object sender, EventArgs e)
        {
            if (file.Text.Trim() == "" || dadi.Text.Trim() == "") { MessageBox.Show("Boş Bırakmayınız"); return; }

            Cursor.Current = Cursors.WaitCursor;
            con = new SqlDbConnect();
            string tum_mailler = txt_gonmails.Text.Trim();
            
            string Subject = secili_siparis.Text + " / " + secili_modeladi.Text + " Dosya Eklendi, Adı:" + dadi.Text.Trim() + " ";
            if (did.Text == "")
            {
                /**** Kullanıcılar *******************************************************/

                try
                {
                    con.SqlQuery("INSERT INTO durumtakip_files (orderno,dadi,yolu,aciklama,ekname,ektarih) VALUES (@orderno,@dadi,@yolu,@aciklama,@ekname,@ektarih)");
                    if (String.IsNullOrEmpty(secili_siparis.Text.Trim())) { con.Cmd.Parameters.AddWithValue("@orderno", DBNull.Value); } else con.Cmd.Parameters.AddWithValue("@orderno", secili_siparis.Text.Trim());
                    if (String.IsNullOrEmpty(dadi.Text.Trim())) { con.Cmd.Parameters.AddWithValue("@dadi", DBNull.Value); } else con.Cmd.Parameters.AddWithValue("@dadi", dadi.Text.Trim());
                    if (String.IsNullOrEmpty(file.Text.ToString().Trim())) { con.Cmd.Parameters.AddWithValue("@yolu", DBNull.Value); } else con.Cmd.Parameters.AddWithValue("@yolu", file.Text.ToString().Trim());
                    if (String.IsNullOrEmpty(dacik.Text.ToString().Trim())) { con.Cmd.Parameters.AddWithValue("@aciklama", DBNull.Value); } else con.Cmd.Parameters.AddWithValue("@aciklama", dacik.Text.ToString().Trim());
                    con.Cmd.Parameters.Add("@ekname", cInfo.cUsername);
                    con.Cmd.Parameters.Add("@ektarih", DateTime.Now);
                    con.QueryNonEx();
                }
                catch (Exception Ex)
                {
                    LogMessageToFile("Dosya paneli  insert olmadı->" + secili_siparis.Text + "/" + cInfo.cUsername + "/" + Environment.NewLine + Ex.ToString());
                    // MessageBox.Show("Bir sorun oluştu, dosya açılamadı lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (dadi.Text.Trim().IndexOf("Kumaş") != -1 || dadi.Text.Trim().IndexOf("Kesim") != -1 || dadi.Text.Trim().IndexOf("Dikim") != -1)
                {
                 //   mailler = "burak@karahangrup.com.tr," + AnaForm_F.Temsilci_Mail_Ver(HerYerden.Order_Temsilci) + "," + AnaForm_F.Planlamaci_Mail_Ver(HerYerden.Order_Planlama);

                }

                else
                {
                    //mailler = "mahir@karahangrup.com.tr,aksesuar@karahangrup.com.tr,rasim.deniz@karahangrup.com.tr," + AnaForm_F.Temsilci_Mail_Ver(HerYerden.Order_Temsilci) + "," + AnaForm_F.Planlamaci_Mail_Ver(HerYerden.Order_Planlama);

                }


                try
                {

                
                con = new SqlDbConnect();

                con.SqlQuery("INSERT INTO durumtakip_ms (orderno,uname,umail,mesaj,tarih,udepartman,bilgi) VALUES (@orderno,@uname,@umail,@mesaj,@tarih,@udepartman,@bilgi)");
                con.Cmd.Parameters.AddWithValue("@orderno", secili_siparis.Text);
                con.Cmd.Parameters.AddWithValue("@uname", cInfo.cUsername);
                con.Cmd.Parameters.AddWithValue("@umail", cInfo.cUsermail);
                con.Cmd.Parameters.AddWithValue("@mesaj", "Dosya Eklendi, Adı: " + dadi.Text.Trim() + ", açıklama (varsa): "+dacik.Text.Trim()+".");
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


                }
                catch (Exception Ex)
                {
                    LogMessageToFile("Dosya paneli  ms insert olmadı->" + secili_siparis.Text + "/" + cInfo.cUsername + "/" + Environment.NewLine + Ex.ToString());
                    // MessageBox.Show("Bir sorun oluştu, dosya açılamadı lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }





                //////////////////////////////////////////////////////////////////////////////


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
                        if (mailcheck.Checked == true) { AnaForm_F.SendEmail(tum_mailler, Subject, secili_siparis.Text.Trim(), file.Text.Trim()); }
                        else { AnaForm_F.SendEmail(tum_mailler, Subject, secili_siparis.Text.Trim(), ""); }
                     
                        MessageBox.Show("Mail ve mesajınız Gönderildi !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);



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
                //////////////////////////////////////////////////////////////////////////////




                ilk();

                



                if (dadi.Text.Trim().IndexOf("Kumaş") != -1 || dadi.Text.Trim().IndexOf("Kesim") != -1 || dadi.Text.Trim().IndexOf("Dikim") != -1)
                {
                   // @AnaForm_F.SendEmail(mailler, HerYerden.Order_No + " Siparişin " + dadi.Text.Trim() + " dosyasını inceleyip onaylayabilirsiniz.");

                }
                else
                {

                    //@AnaForm_F.SendEmail(mailler, HerYerden.Order_No + " Siparişin " + dadi.Text.Trim() + " dosyası Yüklenmiştir");

                }

                MessageBox.Show("Ekleme Yapıldı");

                Temizle();
            }
            else
            {

                try
                {

                    con.SqlQuery("UPDATE durumtakip_files SET  dadi=@dadi,yolu=@yolu,aciklama=@aciklama where id='" + did.Text.Trim() + "'");
                    if (String.IsNullOrEmpty(dadi.Text.Trim())) { con.Cmd.Parameters.AddWithValue("@dadi", DBNull.Value); } else con.Cmd.Parameters.AddWithValue("@dadi", dadi.Text.Trim());
                    if (String.IsNullOrEmpty(file.Text.ToString().Trim())) { con.Cmd.Parameters.AddWithValue("@yolu", DBNull.Value); } else con.Cmd.Parameters.AddWithValue("@yolu", file.Text.ToString().Trim());
                    if (String.IsNullOrEmpty(dacik.Text.ToString().Trim())) { con.Cmd.Parameters.AddWithValue("@aciklama", DBNull.Value); } else con.Cmd.Parameters.AddWithValue("@aciklama", dacik.Text.ToString().Trim());
                }
                catch (Exception Ex)
                {
                    LogMessageToFile("Dosya paneli  update olmadı->" + secili_siparis.Text + "/" + cInfo.cUsername + "/" + Environment.NewLine + Ex.ToString());
                    // MessageBox.Show("Bir sorun oluştu, dosya açılamadı lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


                con.QueryNonEx();
                ilk();

                MessageBox.Show("Güncelleme Yapıldı");
                Temizle();

            }
            con.Close();

            Cursor.Current = Cursors.Default;
        }

        private void data_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                did.Text = data.Rows[e.RowIndex].Cells[5].Value.ToString().Trim();
                file.Text = data.Rows[e.RowIndex].Cells[4].Value.ToString().Trim();
                dadi.Text = data.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
            }
            catch (Exception Ex)
            {
                LogMessageToFile("Dosya paneli  data.Rows[e.RowIndex].Cells[5] double click açılmadı->" + secili_siparis.Text + "/" + cInfo.cUsername + "/" + Environment.NewLine + Ex.ToString());
                // MessageBox.Show("Bir sorun oluştu, dosya açılamadı lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            dac.Visible = true;
            dsil.Visible = true;
            //dsave.Visible = true;
        }

        private void durumtakip_dosyapaneli_Load(object sender, EventArgs e)
        {
            Temizle();
            ilk();
        }

        private void dac_Click(object sender, EventArgs e)
        {


            if (file.Text.Trim() != "")

                try
                {
                    Process.Start(file.Text.Replace('/', '\\'));

                    //vb
                    //Process.Start(file.Text.Trim());
                }
                catch (Exception Ex)
                {
                    LogMessageToFile("Dosya paneli  dosya açılmadı->" + secili_siparis.Text + "/" + cInfo.cUsername + "/" + Environment.NewLine + Ex.ToString());
                   // MessageBox.Show("Bir sorun oluştu, dosya açılamadı lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            else
                MessageBox.Show("Ekleme Yaptıktan Sonra Açabilirsiniz.");
        }

      

        private void dsil_Click(object sender, EventArgs e)
        {
            


            if (secili_siparis.Text.Trim() != "")
            {

                DialogResult cikis = new DialogResult();
                cikis = MessageBox.Show(secili_siparis.Text.Trim() + " nolu Siparişin seçili dosyasını silmek için devam etmek istiyormusunuz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (cikis == DialogResult.Yes)
                {
                    //Application.Exit();
                                 Cursor.Current = Cursors.WaitCursor;


                try
                {
                   


                        if (File.Exists(@file.Text))
                        {
                            File.Delete(@file.Text);
                        }


                        con = new SqlDbConnect();
                    con.SqlQuery("delete from durumtakip_files Where id='" + did.Text + "' ");
                    con.QueryNonEx();
                    con.Close();

                     


                        ilk();
                    Temizle();
                 } catch (Exception Ex)
                    {
                        LogMessageToFile("Dosya paneli  dosya silemedi->" + secili_siparis.Text + "/" + cInfo.cUsername + "/" + Environment.NewLine + Ex.ToString());
                        MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }




                    

 
                }
                if (cikis == DialogResult.No)
                {

                }

                Cursor.Current = Cursors.Default;

            }
            else
            {


            }


        }

        private void dsave_Click(object sender, EventArgs e)
        {

            if (file.Text.Trim() != "")

                try
                {
                  //  Process.Start(file.Text.Replace('/', '\\'));
                   
                    string destination = @file.Text.Replace('/', '\\');
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Multiselect = true;
                    if (DialogResult.OK == ofd.ShowDialog())
                    {
                        foreach (string file in ofd.FileNames)
                        {
                            File.Copy(file, Path.Combine(destination, Path.GetFileName(file)));
                        }
                    }

                 

 


                }
                catch (Exception Ex)
                {
                    LogMessageToFile("Dosya paneli  dosya kendi pc sine kayıt olmadı->" + secili_siparis.Text + "/" + cInfo.cUsername + "/" + Environment.NewLine + Ex.ToString());
                    // MessageBox.Show("Bir sorun oluştu, dosya açılamadı lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            else
                MessageBox.Show("Ekleme Yaptıktan Sonra Açabilirsiniz.");




           
        }
    }
}
