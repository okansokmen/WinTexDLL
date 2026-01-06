using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net; // 
using System.Net.Mail;// 

using DevExpress.XtraGrid.Views.Grid;
namespace WinTexC
{
    public partial class durumtakip_cp_rapor : Form
    {
        SqlDbConnect con;

        DataTable table = new DataTable();

        public class veriler
        {

            public string oktipi;
            public string Renk;
            public string plgonderitarihi;
            public string OKtar2;
            public string pltarihi;
            public string OKTar;
            public string notlar;
            public string sorumlu;
            public string teminsuresi;
            public string ok;
            public string ulineno;
            public string sirano;
            public string uyari;
            public string siparisno;

        }

        public static veriler ss = new veriler();

        public static veriler Order;
        List<veriler> NewOrder = new List<veriler>();


        public durumtakip_cp_rapor()
        {
            InitializeComponent();
        }

        private void durumtakip_cp_rapor_Load(object sender, EventArgs e)
        {
            if ("ADMIN".Contains(cInfo.cUsername) || "MELİH".Contains(cInfo.cUsername))
            {

                groupControl1.Visible = true;
            }
            else { groupControl1.Visible = false; }


            liste_ver();
        }

        double gecgonderi, gecok, durum, renkgon, renkok;

        private void btn_msjgonder_Click(object sender, EventArgs e)
        {
            pltariheliste_ver();
        }


        void liste_ver()
        {
            gridControl1.DataSource = null;
            NewOrder.Clear();
                 try
                {

                    con = new SqlDbConnect();
                    /* con.SqlQuery("select oktipi,Renk,plgonderitarihi,OKtar2,pltarihi,OKTar,notlar,sorumlu,teminsuresi,ok,ulineno,sirano,uyari from sipok with (NOLOCK) where siparisno='" + secili_siparis.Text.Trim() + "' ");
                     SqlDataAdapter sqlDataAdap = new SqlDataAdapter(con.Cmd);
                     DataTable dtRecord = new DataTable();
                     sqlDataAdap.Fill(dtRecord);
                     grid_cp.DataSource = dtRecord;*/


                    con.LoginQuery("select siparisno,oktipi,Renk,plgonderitarihi,OKtar2,pltarihi,OKTar,notlar,sorumlu,teminsuresi,ok,ulineno,sirano,uyari from sipok with (NOLOCK) where plgonderitarihi>='01.11.2018'");
                    while (con.dbr.Read())
                    {
                        Order = new veriler();
                    Order.siparisno = con.dbr["siparisno"].ToString().Trim();
                    Order.oktipi = con.dbr["oktipi"].ToString().Trim();
                        Order.Renk = con.dbr["Renk"].ToString().Trim();
                        Order.plgonderitarihi = DateTime.Parse(con.dbr["plgonderitarihi"].ToString().Trim()).ToString("dd.MM.yyyy")  ;
                        Order.OKtar2 = con.dbr["OKtar2"].ToString().Trim();
                        Order.pltarihi = DateTime.Parse(con.dbr["pltarihi"].ToString().Trim()).ToString("dd.MM.yyyy");
                        Order.OKTar = con.dbr["OKTar"].ToString().Trim();
                        Order.notlar = con.dbr["notlar"].ToString().Trim();
                        Order.sorumlu = con.dbr["sorumlu"].ToString().Trim();
                        Order.teminsuresi = con.dbr["teminsuresi"].ToString().Trim();
                        Order.ok = con.dbr["ok"].ToString().Trim();
                        Order.uyari = con.dbr["uyari"].ToString().Trim();
                        Order.sirano = con.dbr["sirano"].ToString().Trim();
                        NewOrder.Add(Order);
                    }
                    con.dbr.Close();

                    con.Close();

                    // this.grid_cp.DataSource = BuildDataTable();



                    /*
                    for (int i = 0; i < gridView1.DataRowCount; i++)
                    {
                        this.gridView1.SetRowCellValue(i, gridView1.Columns[0],1);
                        this.gridView1.SetRowCellValue(i, gridView1.Columns[1], "asd");
                        this.gridView1.SetRowCellValue(i, gridView1.Columns[2], 3);
                        this.gridView1.UpdateCurrentRow();

                      ///  MessageBox.Show(i.ToString());
                    }*/






                    //  MessageBox.Show(NewOrder.Count.ToString() + "/" + dtRecords.Rows.Count.ToString());


                    gridControl1.DataSource = BuildDataTable();





                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }





                



            






        }

        private void gridView3_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                  renkgon = Convert.ToDouble(gridView3.GetRowCellValue(e.RowHandle, "Gönderim Gecikme"));
                  renkok = Convert.ToDouble(gridView3.GetRowCellValue(e.RowHandle, "OK Gecikme"));
              //  durum = Convert.ToDouble(gridView3.GetRowCellValue(e.RowHandle, "kirmizi"));


            }
            catch { }

            try
            {
                if ((renkgon >= 0 && renkgon <= 2 )|| (renkok >= 0 && renkok <= 2)) { e.Appearance.BackColor = Color.NavajoWhite; }
                if (renkgon < 0 || renkok < 0) { e.Appearance.BackColor = Color.Salmon; e.Appearance.ForeColor = Color.White; }
                                            

            }
            catch { }


            /* if (durum == 1) { e.Appearance.BackColor = Color.Red; e.Appearance.ForeColor = Color.White; }
             if (durum == 2) { e.Appearance.BackColor = Color.Orange; e.Appearance.ForeColor = Color.Black; }
             */

            GridView view = sender as GridView;
            if (view == null) return;


            view.Columns["Plan.Gönderi Tar."].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            view.Columns["Plan.Gönderi Tar."].DisplayFormat.FormatString = "D";


            view.Columns[2].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            view.Columns[2].DisplayFormat.FormatString = "D";
            view.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            view.Columns[3].DisplayFormat.FormatString = "D";

            view.Columns[4].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            view.Columns[4].DisplayFormat.FormatString = "D";
            view.Columns[5].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            view.Columns[5].DisplayFormat.FormatString = "D";
        }

        private DataTable BuildDataTable()
        {
            table.Columns.Clear();
            table.Rows.Clear();

            table.Columns.Add("Konu");
            table.Columns.Add("Renk");
            //table.Columns.Add("c3", typeof(DateTime));
            table.Columns.Add("Plan.Gönderi Tar.", typeof(DateTime));
            table.Columns.Add("Plan.Ok Tar.", typeof(DateTime));
            table.Columns.Add("Gerçek.Gön.Tar");
            table.Columns.Add("Gerçek.Ok.Tar");
            table.Columns.Add("Notlar");
            table.Columns.Add("Sorumlu");
            table.Columns.Add("Temin");
            table.Columns.Add("Sonuç");
            table.Columns.Add("Gönderim Gecikme");
            table.Columns.Add("OK Gecikme");

            table.Columns.Add("Kayıt No");
            table.Columns.Add("Uyarı");
            
            table.Columns.Add("Sipariş");
             
            foreach (var value in NewOrder)
            {
                gecgonderi = 0;
                gecok = 0;
                durum = 0;
                

                if (value.plgonderitarihi.Trim() != "" && value.pltarihi.Trim() != "")
                {


                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (value.OKtar2.ToString().Trim() == "")
                    {
                        if (DateTime.Parse(DateTime.Parse(value.plgonderitarihi).ToString("dd.MM.yyyy")) < DateTime.Parse(DateTime.Now.ToString("dd.MM.yyyy")))
                        {
                            gecgonderi = GetBusinessDays(DateTime.Parse(DateTime.Now.ToString("dd.MM.yyyy")), DateTime.Parse(DateTime.Parse(value.plgonderitarihi).ToString("dd.MM.yyyy")));

                            durum = 1;
                        }
                        else if (DateTime.Parse(DateTime.Parse(value.plgonderitarihi).ToString("dd.MM.yyyy")) == DateTime.Parse(DateTime.Now.ToString("dd.MM.yyyy")))
                        {
                            //MessageBox.Show(DateTime.Parse(value.plgonderitarihi).ToString("dd.MM.yyyy")+"/"+ DateTime.Now.ToString("dd.MM.yyyy") + "value.OKtar2 boş ve günler eşit");
                           // durum = 1;
                            gecgonderi = 0;
                          

                        }
                        else if (DateTime.Parse(DateTime.Parse(value.plgonderitarihi).ToString("dd.MM.yyyy")) > DateTime.Parse(DateTime.Now.ToString("dd.MM.yyyy")))
                        {
                            //MessageBox.Show(DateTime.Parse(value.plgonderitarihi).ToString("dd.MM.yyyy")+"/"+ DateTime.Now.ToString("dd.MM.yyyy") + "value.OKtar2 boş ve günler eşit");
                            // durum = 1;

                            gecgonderi = GetBusinessDays(DateTime.Parse(DateTime.Now.ToString("dd.MM.yyyy")), DateTime.Parse(DateTime.Parse(value.plgonderitarihi).ToString("dd.MM.yyyy")));

                        }

                    }
                    else
                    {
                        //
                        if (DateTime.Parse(DateTime.Parse(value.plgonderitarihi).ToString("dd.MM.yyyy")) < DateTime.Parse(DateTime.Parse(value.OKtar2).ToString("dd.MM.yyyy")))
                        {
                            gecgonderi = GetBusinessDays(DateTime.Parse(DateTime.Parse(value.OKtar2).ToString("dd.MM.yyyy")), DateTime.Parse(DateTime.Parse(value.plgonderitarihi).ToString("dd.MM.yyyy")));

                        }
                        else if (DateTime.Parse(DateTime.Parse(value.plgonderitarihi).ToString("dd.MM.yyyy")) == DateTime.Parse(DateTime.Parse(value.OKtar2).ToString("dd.MM.yyyy")))
                        {
                            gecgonderi = 0;
                           
                        }
                        else if (DateTime.Parse(DateTime.Parse(value.plgonderitarihi).ToString("dd.MM.yyyy")) > DateTime.Parse(DateTime.Parse(value.OKtar2).ToString("dd.MM.yyyy")))
                        {
                            gecgonderi = GetBusinessDays(DateTime.Parse(DateTime.Parse(value.OKtar2).ToString("dd.MM.yyyy")), DateTime.Parse(DateTime.Parse(value.plgonderitarihi).ToString("dd.MM.yyyy")));


                        }
                        else
                        {
                            gecgonderi = GetBusinessDays(DateTime.Parse(DateTime.Parse(value.OKtar2).ToString("dd.MM.yyyy")), DateTime.Parse(DateTime.Parse(value.plgonderitarihi).ToString("dd.MM.yyyy")));
                        }


                    }

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////



                    if (value.OKTar.ToString().Trim() == "")
                    {
                        if (DateTime.Parse(DateTime.Parse(value.pltarihi).ToString("dd.MM.yyyy")) < DateTime.Parse(DateTime.Now.ToString("dd.MM.yyyy")))//pl tarihi daha gelmemiş, şuan daha büyük
                        {
                            durum = 1;
                            gecok = GetBusinessDays(DateTime.Parse(DateTime.Now.ToString("dd.MM.yyyy")), DateTime.Parse(DateTime.Parse(value.pltarihi).ToString("dd.MM.yyyy")));
                        }
                        else if (DateTime.Parse(DateTime.Parse(value.pltarihi).ToString("dd.MM.yyyy")) > DateTime.Parse(DateTime.Now.ToString("dd.MM.yyyy")))// şuandan büyükse
                        {
                            gecok = GetBusinessDays(DateTime.Parse(DateTime.Now.ToString("dd.MM.yyyy")), DateTime.Parse(DateTime.Parse(value.pltarihi).ToString("dd.MM.yyyy")));

                        }
                        else if (DateTime.Parse(DateTime.Parse(value.pltarihi).ToString("dd.MM.yyyy")) == DateTime.Parse(DateTime.Now.ToString("dd.MM.yyyy")))
                        { durum = 1; gecok = 0; }

                    }
                    else
                    {
                        if (DateTime.Parse(DateTime.Parse(value.pltarihi).ToString("dd.MM.yyyy")) < DateTime.Parse(DateTime.Parse(value.OKTar).ToString("dd.MM.yyyy")))
                        {
                            gecok = GetBusinessDays(DateTime.Parse(DateTime.Parse(value.OKTar).ToString("dd.MM.yyyy")), DateTime.Parse(DateTime.Parse(value.pltarihi).ToString("dd.MM.yyyy")));
                        }

                        else if (DateTime.Parse(DateTime.Parse(value.pltarihi).ToString("dd.MM.yyyy")) > DateTime.Parse(DateTime.Parse(value.OKTar).ToString("dd.MM.yyyy")))
                        {
                            gecok = GetBusinessDays(DateTime.Parse(DateTime.Parse(value.OKTar).ToString("dd.MM.yyyy")), DateTime.Parse(DateTime.Parse(value.pltarihi).ToString("dd.MM.yyyy")));
                        }
                        else if (DateTime.Parse(DateTime.Parse(value.pltarihi).ToString("dd.MM.yyyy")) == DateTime.Parse(DateTime.Parse(value.OKTar).ToString("dd.MM.yyyy")))
                        {
                            gecok = 0;
                        }

                        else
                        {
                            gecok = GetBusinessDays(DateTime.Parse(DateTime.Parse(value.OKTar).ToString("dd.MM.yyyy")), DateTime.Parse(DateTime.Parse(value.pltarihi).ToString("dd.MM.yyyy")));
                        }

                    }

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    if (value.ok != "" && value.ok == "H")
                    {
                        durum = 3;

                    }
                    /*  if(value.OKtar2.ToString().Trim() == "" && value.OKTar.ToString().Trim() == "")
                      { durum = 1; }
                      if (value.OKtar2.ToString().Trim() != "" && value.OKTar.ToString().Trim() != "")
                          { durum = 1; }
                          */
                }



                if (value.plgonderitarihi.Trim() == "" && value.pltarihi.Trim() == "")
                {

                    table.Rows.Add(new Object[] { value.oktipi,
                    value.Renk,
                    DateTime.Parse("01.01.2018").ToString("dd.MM.yyyy"),
                    DateTime.Parse("01.01.2018").ToString("dd.MM.yyyy"),
                   value.OKtar2,
                   value.OKTar,
                    value.notlar,
                    value.sorumlu,value.teminsuresi,value.ok, Math.Round(gecgonderi, MidpointRounding.ToEven), Math.Round(gecok, MidpointRounding.ToEven),value.sirano,value.uyari });


                }
                else
                {


                    table.Rows.Add(new Object[] { value.oktipi,
                    value.Renk,
                    DateTime.Parse(value.plgonderitarihi).ToString("dd.MM.yyyy"),
                    DateTime.Parse(value.pltarihi).ToString("dd.MM.yyyy"),
                   value.OKtar2,
                   value.OKTar,
                    value.notlar,
                    value.sorumlu,value.teminsuresi,value.ok, Math.Round(gecgonderi, MidpointRounding.ToEven), Math.Round(gecok, MidpointRounding.ToEven),value.sirano,value.uyari });


                }

/*
                table.Rows.Add(new Object[] {
                    
                    value.oktipi,
                    value.Renk,
                    DateTime.Parse(value.plgonderitarihi).ToString("dd.MM.yyyy"),
                    DateTime.Parse(value.pltarihi).ToString("dd.MM.yyyy"),
                    value.OKtar2, value.OKTar, value.notlar, value.sorumlu, value.teminsuresi, value.ok, Math.Round(gecgonderi, MidpointRounding.ToEven), Math.Round(gecok, MidpointRounding.ToEven), value.sirano, value.uyari, value.siparisno });
*/
            }

            NewOrder.Clear();

            return table;
        }

        private void btn_oktarihinegore_Click(object sender, EventArgs e)
        {
            oktariheliste_ver();
        }

        private void gridView3_DoubleClick(object sender, EventArgs e)
        {
            if (gridView3.GetFocusedRowCellValue("Kayıt No") != null)
            {
                kayit_no.Text = gridView3.GetFocusedRowCellValue("Kayıt No").ToString().Trim();

                //Sorumlu
                if (gridView3.GetFocusedRowCellValue("Sorumlu") != null || gridView3.GetFocusedRowCellValue("Sorumlu").ToString().Trim() != "")
                {

                    try {

                        con = new SqlDbConnect();
                        con.LoginQuery("select email from personel with (NOLOCK) where personel='" + gridView3.GetFocusedRowCellValue("Sorumlu").ToString().Trim() + "'");
                        while (con.dbr.Read())
                        {
                            txt_gonmails.Text = con.dbr["email"].ToString().Trim();

                        }
                        con.dbr.Close();
                        con.Close();


                    }
                    catch { }

 
                }


                try
                {
                    if (gridView3.GetFocusedRowCellValue("Konu") != null && gridView3.GetFocusedRowCellValue("Sipariş") != null)
                    {
                        not_gonderi.Text = gridView3.GetFocusedRowCellValue("Sipariş").ToString().Trim() + " <->" + gridView3.GetFocusedRowCellValue("Konu").ToString().Trim() + " hakkında ";
                    }
                }
                catch { }


            }
        }


        private void btn_ok_Click(object sender, EventArgs e)
        {

            try
            { SendEmail(txt_gonmails.Text.Trim(), "","");  }catch{  }


        }


        public void SendEmail(string gidecekler, string subject, string sipno)
        {

            try
            {
                int _port = 587;
                string _host = "smtp.bentas.com";
                string _clientUserName = cInfo.cUsermail;
                string _fromMail = cInfo.cUsermail;
                string _clientUserPassword = cInfo.cUsermailpass;

                bool _enableSsl = false;

                MailMessage mailMessage = new MailMessage();
                mailMessage.To.Add(@gidecekler);
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = not_gonderi.Text.Trim();
                mailMessage.Subject = "CP Hatırlatma - " + cInfo.cUsername + "  (" + cInfo.cDepartman + ")";
                mailMessage.From = new MailAddress(_fromMail);

                using (SmtpClient smtp = new SmtpClient(_host, _port))
                {
                    if (cInfo.cUsermailpass.ToString().Trim() == "")
                    {
                        //smtp.Credentials = new NetworkCredential("durumtakip@karahangrup.com.tr", "1karahan2");
                    }
                    else
                    { smtp.Credentials = new NetworkCredential(_fromMail, _clientUserPassword); }


                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.EnableSsl = _enableSsl;
                    smtp.Send(@mailMessage);
                    smtp.Dispose();
                }


                MessageBox.Show("Mesajınız gönderildi");

            }
            catch {

                MessageBox.Show("Mail Gönderilemedi, sorun oluştu. Destek irtibata geçiniz");
            }




        }


        public static double GetBusinessDays(DateTime startD, DateTime endD)
        {
            double calcBusinessDays = 1 + ((endD - startD).TotalDays * 5 - (startD.DayOfWeek - endD.DayOfWeek) * 2) / 7;

            if ((int)endD.DayOfWeek == 6) calcBusinessDays--;

            if ((int)startD.DayOfWeek == 0) calcBusinessDays--;

            return calcBusinessDays - 1;
        }


        void pltariheliste_ver()
        {
            gridControl1.DataSource = null;
            NewOrder.Clear();
            try
            {
                

                con = new SqlDbConnect();
                con.LoginQuery("select siparisno,oktipi,Renk,plgonderitarihi,OKtar2,pltarihi,OKTar,notlar,sorumlu,teminsuresi,ok,ulineno,sirano,uyari from sipok with (NOLOCK) where plgonderitarihi>='" + DateTime.Parse(tar1.Text.Trim())+ "' and plgonderitarihi<='" + DateTime.Parse(tar2.Text.Trim()) + "'");
                while (con.dbr.Read())
                {
                    Order = new veriler();
                    Order.siparisno = con.dbr["siparisno"].ToString().Trim();
                    Order.oktipi = con.dbr["oktipi"].ToString().Trim();
                    Order.Renk = con.dbr["Renk"].ToString().Trim();
                    Order.plgonderitarihi = con.dbr["plgonderitarihi"].ToString().Trim();
                    Order.OKtar2 = con.dbr["OKtar2"].ToString().Trim();
                    Order.pltarihi = con.dbr["pltarihi"].ToString().Trim();
                    Order.OKTar = con.dbr["OKTar"].ToString().Trim();
                    Order.notlar = con.dbr["notlar"].ToString().Trim();
                    Order.sorumlu = con.dbr["sorumlu"].ToString().Trim();
                    Order.teminsuresi = con.dbr["teminsuresi"].ToString().Trim();
                    Order.ok = con.dbr["ok"].ToString().Trim();
                    Order.uyari = con.dbr["uyari"].ToString().Trim();
                    Order.sirano = con.dbr["sirano"].ToString().Trim();
                    NewOrder.Add(Order);
                }
                con.dbr.Close();

                con.Close();

                // this.grid_cp.DataSource = BuildDataTable();



                /*
                for (int i = 0; i < gridView1.DataRowCount; i++)
                {
                    this.gridView1.SetRowCellValue(i, gridView1.Columns[0],1);
                    this.gridView1.SetRowCellValue(i, gridView1.Columns[1], "asd");
                    this.gridView1.SetRowCellValue(i, gridView1.Columns[2], 3);
                    this.gridView1.UpdateCurrentRow();

                  ///  MessageBox.Show(i.ToString());
                }*/






                //  MessageBox.Show(NewOrder.Count.ToString() + "/" + dtRecords.Rows.Count.ToString());


                gridControl1.DataSource = BuildDataTable();





            }
            catch (Exception Ex)
            {
                MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

             




        }

        void oktariheliste_ver()
        {
            gridControl1.DataSource = null;
            NewOrder.Clear();
            try
            {


                con = new SqlDbConnect();
                con.LoginQuery("select siparisno,oktipi,Renk,plgonderitarihi,OKtar2,pltarihi,OKTar,notlar,sorumlu,teminsuresi,ok,ulineno,sirano,uyari from sipok with (NOLOCK) where pltarihi>='" + DateTime.Parse(tar1.Text.Trim()) + "' and pltarihi<='" + DateTime.Parse(tar2.Text.Trim()) + "'");
                while (con.dbr.Read())
                {
                    Order = new veriler();
                    Order.siparisno = con.dbr["siparisno"].ToString().Trim();
                    Order.oktipi = con.dbr["oktipi"].ToString().Trim();
                    Order.Renk = con.dbr["Renk"].ToString().Trim();
                    Order.plgonderitarihi = con.dbr["plgonderitarihi"].ToString().Trim();
                    Order.OKtar2 = con.dbr["OKtar2"].ToString().Trim();
                    Order.pltarihi = con.dbr["pltarihi"].ToString().Trim();
                    Order.OKTar = con.dbr["OKTar"].ToString().Trim();
                    Order.notlar = con.dbr["notlar"].ToString().Trim();
                    Order.sorumlu = con.dbr["sorumlu"].ToString().Trim();
                    Order.teminsuresi = con.dbr["teminsuresi"].ToString().Trim();
                    Order.ok = con.dbr["ok"].ToString().Trim();
                    Order.uyari = con.dbr["uyari"].ToString().Trim();
                    Order.sirano = con.dbr["sirano"].ToString().Trim();
                    NewOrder.Add(Order);
                }
                con.dbr.Close();

                con.Close();

                // this.grid_cp.DataSource = BuildDataTable();



                /*
                for (int i = 0; i < gridView1.DataRowCount; i++)
                {
                    this.gridView1.SetRowCellValue(i, gridView1.Columns[0],1);
                    this.gridView1.SetRowCellValue(i, gridView1.Columns[1], "asd");
                    this.gridView1.SetRowCellValue(i, gridView1.Columns[2], 3);
                    this.gridView1.UpdateCurrentRow();

                  ///  MessageBox.Show(i.ToString());
                }*/






                //  MessageBox.Show(NewOrder.Count.ToString() + "/" + dtRecords.Rows.Count.ToString());


                gridControl1.DataSource = BuildDataTable();





            }
            catch (Exception Ex)
            {
                MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }






        }


    }
}
