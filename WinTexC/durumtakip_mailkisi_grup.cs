using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections; // 
namespace WinTexC
{
    public partial class durumtakip_mailkisi_grup : Form
    {
        ArrayList departman_dizi = new ArrayList();
        public static SqlDbConnect con;
        public static ArrayList secilimail_dizi = new ArrayList();
        public static ArrayList grupnames_dizi= new ArrayList();
        public static ArrayList secilimail2_dizi = new ArrayList();
        public durumtakip_mailkisi_grup()
        {
            InitializeComponent();
        }

        private void durumtakip_mailkisi_Load(object sender, EventArgs e)
        {
            gruplistever();

          /*  con = new SqlDbConnect();
            con.LoginQuery("select aciklama from dr_mailkisi with (NOLOCK) where ekleyen='" + cInfo.cUsername + "'");

                while (con.dbr.Read())
                {

                    Lsts.Items.Add(con.dbr["aciklama"].ToString().Trim());


                }
                con.dbr.Close();
             
            con.Close();

            for (int i = 0; i < LstU.CheckedItems.Count; i++)
            {  LstU.SetItemChecked(i, true);   }
            */
            EnIlkYuklemeler();
        }

        void EnIlkYuklemeler()
        {
            try { 
            // Kullanıcılar
                con = new SqlDbConnect();
                
                departman_dizi.Clear();
                con.LoginQuery("select DISTINCT(aciklama) from  personel with (NOLOCK) where ayrildi <> 'E'");
                LstG.Items.Clear();
                while (con.dbr.Read())
                {
                    if (con.dbr["aciklama"].ToString().Trim() != "")
                    {
                        departman_dizi.Add(con.dbr["aciklama"].ToString().Trim());

                    }

                }
                con.dbr.Close();
                /************************************************************/
                for (int i = 0; i < departman_dizi.Count; i++)
                {


                    for (int j = i + 1; j < departman_dizi.Count; j++)
                    {
                        if (departman_dizi[i].ToString() == departman_dizi[j].ToString())
                        { departman_dizi.Remove(departman_dizi[j]); }
                    }
                }

                foreach (var value in departman_dizi)
                {
                    LstG.Items.Add(value.ToString());

                }


                /***************************************************************************************************************************** 
                con.LoginQuery("select personel,email from  personel with (NOLOCK)");
                LstU.Items.Clear();
                while (con.dbr.Read())
                {

                        LstU.Items.Add(con.dbr["personel"] + "      /" + con.dbr["email"]);

                }
                con.dbr.Close();





                /************************************************************************************************************************** 
                // seçili sipariş

                con.LoginQuery("select DISTINCT( kullanicisipno) From siparis  where  (dosyakapandi is null or dosyakapandi ='H' or dosyakapandi ='') ");
                Order_Lst.Items.Clear();
                Order_Lst.Items.Add("");
                while (con.dbr.Read())
                {
                    if (con.dbr[0].ToString().Trim() != "") Order_Lst.Items.Add(con.dbr[0].ToString().Trim());
                }
                con.dbr.Close();

                //  Gnl8_SelectedIndexChanged(null, null); // or (null, null)

                /*************************************************************************************************************************** 
                // Müşteri Temsilcileri

                // O.ACIK9  modelist
                con.LoginQuery("select DISTINCT(personel) from  personel where rol = 'MUSTERI TEMSILCILIGI'");
                // con.LoginQuery("SELECT DISTINCT(O.ACIK9) FROM ORDERH O WHERE O.YUKTAR='" + Convert.ToDateTime("1899-12-30") + "' AND O.ORDERNO LIKE 'K%' ORDER BY O.ACIK9");
                Mtem_Lst.Items.Clear();
                Mtem_Lst.Items.Add("");
                while (con.dbr.Read())
                {
                    if (con.dbr[0] != "") Mtem_Lst.Items.Add(con.dbr[0].ToString().Trim());
                }

                con.dbr.Close();



                 */

                con.Close();
            }
            catch { }
        }

      

        private void LstG_SelectedIndexChanged(object sender, EventArgs e)
        {
            Gonderi_listesi_ver();
        }
        
        void Gonderi_listesi_ver()
        {

            secilimail_dizi.Clear();
           LstU.Items.Clear();
            try
            {
                for (int i = 0; i < LstG.CheckedItems.Count; i++)
                {
                    // colorum  =colorum+","+ LstU.CheckedItems[i].ToString() + " ";

                    if (LstG.CheckedItems[i].ToString().Trim() != "") { secilimail_dizi.Add(LstG.CheckedItems[i].ToString().Trim()); }

                }
            }
            catch
            { }

                if (secilimail_dizi.Count > 0)
            {
                con = new SqlDbConnect();


                foreach (string part in secilimail_dizi.ToArray())
                {
                   // MessageBox.Show(secilimail_dizi.Count.ToString() +"-----"+part);
                    try
                    {

                         con.LoginQuery("select personel,email from personel with (NOLOCK) where aciklama like '%" + part + "%' and ayrildi <> 'E'");

                        while (con.dbr.Read())
                        {
                            //MessageBox.Show(secilimail_dizi.Count.ToString() + "-----" + part +"----"+ con.dbr["personel"].ToString().Trim());
                            LstU.Items.Add(con.dbr["personel"].ToString().Trim() + "      /" + con.dbr["email"].ToString().Trim());
                        }
                        con.dbr.Close();
                    }
                    catch
                    {

                        MessageBox.Show("Departman kişileri veritabanından çekilemedi", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                      //  LogMessageToFile("Departman kişileri veritabanından çekilemedi");
                    }

                }

                con.Close();

                /*   for (int i = 0; i < LstU.Items.Count; i++)
                   {
                       LstU.SetItemChecked(i, true);
                   }
                   */

            }

            secilimail_dizi.Clear();

        }


         void kisi_listesi_ver()
        {
          //  Lsts.Items.Clear();
            secilimail_dizi.Clear();
           Lsts.Items.Clear();
            try
            {
                if (LstU.CheckedItems.Count > 0)
                {
                    for (int i = 0; i < LstU.CheckedItems.Count; i++)
                    {
                        // colorum  =colorum+","+ LstU.CheckedItems[i].ToString() + " ";

                        if (LstU.CheckedItems[i].ToString().Trim() != null && LstU.CheckedItems[i].ToString().Trim() != "")
                        {

                            if (!secilimail_dizi.Contains(LstU.CheckedItems[i].ToString().Trim()))
                            {
                                secilimail_dizi.Add(LstU.CheckedItems[i].ToString().Trim());
                            }



                            // Lsts.Items.Add(LstU.CheckedItems[i].ToString().Trim());
                        }

                    }
                }
            }
            catch(Exception ex) {
                MessageBox.Show("Burada bir hata verdi -> " + ex.ToString());
            }


            if (secilimail_dizi.Count > 0)
            {

                // MessageBox.Show("Seçili gruplar -> "+secilimail_dizi.Count.ToString());
             
               /* for (int i = 0; i < secilimail_dizi.Count; i++)
                {


                    for (int j = i + 1; j < secilimail_dizi.Count; j++)
                    {
                        if (secilimail_dizi[i].ToString() == secilimail_dizi[j].ToString())
                        { secilimail_dizi.Remove(secilimail_dizi[j]); }
                    }
                }



                if (Lsts.CheckedItems.Count > 0)
                {
                    for (int i = 0; i < Lsts.CheckedItems.Count; i++)
                    {
                        // colorum  =colorum+","+ LstU.CheckedItems[i].ToString() + " ";

                        if (Lsts.CheckedItems[i].ToString().Trim() != "")
                        {

                            if (!secilimail2_dizi.Contains(Lsts.CheckedItems[i].ToString().Trim()))
                            {
                                secilimail2_dizi.Add(Lsts.CheckedItems[i].ToString().Trim());
                            }


                        //    secilimail2_dizi.Add(Lsts.CheckedItems[i].ToString().Trim());

                            // Lsts.Items.Add(LstU.CheckedItems[i].ToString().Trim());
                        }

                    }
                }
                */


                for (int i = 0; i < secilimail_dizi.Count; i++)

                {
                  /*  if (secilimail2_dizi.Contains(secilimail_dizi[i].ToString().Trim())) { } else { 
                    Lsts.Items.Add(secilimail_dizi[i].ToString().Trim());
                    }*/


                    Lsts.Items.Add(secilimail_dizi[i].ToString().Trim());
                }

            }

                 /*   for (int i = 0; i < Lsts.Items.Count; i++)
            {
                Lsts.SetItemChecked(i, true);
            }*/

            secilimail2_dizi.Clear();
        }

 

        private void LstG_MouseClick(object sender, MouseEventArgs e)
        {
           Gonderi_listesi_ver();

        }

        private void LstG_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Gonderi_listesi_ver();
        }










        private void LstU_SelectedIndexChanged(object sender, EventArgs e)
        {
            kisi_listesi_ver();
        }

       

        private void LstU_Click(object sender, EventArgs e)
        {
           // kisi_listesi_ver();

        }

        private void btn_kaydet_Click(object sender, EventArgs e)
        {
            if (grupname.Text.Trim() == "") { MessageBox.Show("Lütfen mail grup adını doldurunuz"); return; }
            try { 
            con = new SqlDbConnect();


            con.SqlQuery("delete from  dr_mailkisi_grup Where ekleyen='" + cInfo.cUsername + "' and grupname='"+ grupname .Text.Trim()+ "'");
            con.QueryNonEx();


            for (int i = 0; i < Lsts.CheckedItems.Count; i++)
            {
                // colorum  =colorum+","+ LstU.CheckedItems[i].ToString() + " ";


                if (Lsts.CheckedItems[i].ToString().Trim() != "")
                {

          //        secilimail2_dizi.Add(Lsts.CheckedItems[i].ToString().Trim());
                    con.SqlQuery("INSERT INTO dr_mailkisi_grup (ekleyen,aciklama,grupname) VALUES (@ekleyen,@aciklama,@grupname)");
                    con.Cmd.Parameters.AddWithValue("@ekleyen", cInfo.cUsername);
                    con.Cmd.Parameters.AddWithValue("@aciklama", Lsts.CheckedItems[i].ToString().Trim());
                    con.Cmd.Parameters.AddWithValue("@grupname", grupname.Text.Trim());
                    con.QueryNonEx();
                    




                    
                    // Lsts.Items.Add(LstU.CheckedItems[i].ToString().Trim());
                }

            }
            MessageBox.Show("Eklemeler yapıldı !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Heryerden.Gun1 = 1;
           // this.Close();
            con.Close();
            }
            catch (Exception ex){ MessageBox.Show("hata " + ex.ToString()); }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            // flag = true;


            for (int i = 0; i < Lsts.Items.Count; i++)
            {


                if (checkBox1.Checked == true)
                {
                    Lsts.SetItemChecked(i, true);
                }
                else
                {
                    Lsts.SetItemChecked(i, false);
                }



            }


            // flag = false;
            Cursor.Current = Cursors.Default;
        }
        string colorum;
        private void isaretlenen_veriler()
        {

         /*   color.Text = "";
            colorum = "";
            for (int i = 0; i < LstU.CheckedItems.Count; i++)
            {
                // colorum  =colorum+","+ LstU.CheckedItems[i].ToString() + " ";
                if (colorum == "") { colorum = LstU.CheckedItems[i].ToString(); }
                else { colorum = colorum + "," + LstU.CheckedItems[i].ToString(); }

            }

            color.Text = colorum.Trim();
            */

        }
        private void LstG_Click(object sender, EventArgs e)
        {
            Gonderi_listesi_ver();
        }

        private void LstG_DoubleClick(object sender, EventArgs e)
        {
            Gonderi_listesi_ver();

        }

        private void LstU_DoubleClick(object sender, EventArgs e)
        {
           // kisi_listesi_ver();
        }

        private void LstU_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            kisi_listesi_ver();

        }

        private void LstG_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            Gonderi_listesi_ver();

        }

        private void btn_sil_Click(object sender, EventArgs e)
        {
         
        }

        private void btn_hepsini_sil_Click(object sender, EventArgs e)
        {
            if (grupname.Text.Trim() != "")
            {

                DialogResult cikis = new DialogResult();
                cikis = MessageBox.Show(grupname.Text.Trim() + " adlı mail gurubunu silmek için devam etmek istiyormusunuz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (cikis == DialogResult.Yes)
                {
                    //Application.Exit();
                    Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        con = new SqlDbConnect();
                        con.SqlQuery("delete from dr_mailkisi_grup Where grupname='" + grupname.Text.Trim() + "' and ekleyen='" + cInfo.cUsername + "'");
                        con.QueryNonEx();

                        kutu_temizle();
                        //liste_ver();
                        MessageBox.Show("Silme işlemi tamamlandı !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        con.Close();
                    }
                    catch {
                        //LogMessageToFile("durum takipte sip hep sil sorun var->" + secili_siparis.Text + "" + cInfo.cUsername);
                    }

                    Cursor.Current = Cursors.Default;

                    //MessageBox.Show(secili_siparis.Text.Trim() + " nolu Sipariş durum takipte kapatıldı", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (cikis == DialogResult.No)
                {

                }

            }
            else
            {


            }
        }

    
        void gruplistever() {
            grupnames_dizi.Clear();
            lstgrup.Items.Clear();

            con = new SqlDbConnect();


         
                // MessageBox.Show(secilimail_dizi.Count.ToString() +"-----"+part);
                try
                {
                
                    con.LoginQuery("select grupname from dr_mailkisi_grup with (NOLOCK) where ekleyen='" + cInfo.cUsername+"'");

                    while (con.dbr.Read())
                    {
                    //MessageBox.Show(secilimail_dizi.Count.ToString() + "-----" + part +"----"+ con.dbr["personel"].ToString().Trim());
                    //lstgrup.Items.Add(con.dbr["grupname"].ToString().Trim());

                    if (!grupnames_dizi.Contains(con.dbr["grupname"].ToString().Trim()))
                    {
                        grupnames_dizi.Add(con.dbr["grupname"].ToString().Trim());
                    }

 //grupnames_dizi.Add(con.dbr["grupname"].ToString().Trim());
                    }
                    con.dbr.Close();


                for (int i = 0; i < grupnames_dizi.Count; i++)
                {


                    for (int j = i + 1; j < grupnames_dizi.Count; j++)
                    {
                        if (grupnames_dizi[i].ToString() == grupnames_dizi[j].ToString())
                        { grupnames_dizi.Remove(grupnames_dizi[j]); }
                    }
                }
 

              //  MessageBox.Show(grupnames_dizi.Count.ToString());
                foreach (var value in grupnames_dizi)
                {
                    lstgrup.Items.Add(value);

                }







            }
                catch
                {

                    MessageBox.Show("Grupkaydı veritabanından çekilemedi", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //  LogMessageToFile("Departman kişileri veritabanından çekilemedi");
                }
 

            con.Close();

        }



        void kutu_temizle() {

            grupname.Text = "";
            Lsts.Items.Clear();


            gruplistever();

        }

        private void lstgrup_MouseClick(object sender, MouseEventArgs e)
        {
            grupname.Text = lstgrup.SelectedItem.ToString();


            try
            {
                Lsts.Items.Clear();
                con = new SqlDbConnect();
                con.LoginQuery("select aciklama from dr_mailkisi_grup with (NOLOCK) where ekleyen='" + cInfo.cUsername + "' and grupname='"+ grupname.Text .Trim()+ "'");

                while (con.dbr.Read())
                {

                    Lsts.Items.Add(con.dbr["aciklama"].ToString().Trim());


                }
                con.dbr.Close();

                con.Close();

                for (int i = 0; i < Lsts.CheckedItems.Count; i++)
                { Lsts.SetItemChecked(i, true); }
            }
            catch { }
        }

        private void btn_temizle_Click(object sender, EventArgs e)
        {
            kutu_temizle();
        }
    }
}
