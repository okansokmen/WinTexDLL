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

using DevExpress.XtraGrid.Views.Grid;
namespace WinTexC
{
    public partial class durumtakip_cp : Form
    {
        public static SqlDbConnect con;
        public static Firmoklist Firmaok;
        string dateString, format;
        DateTime result;
        DateTime result2;
        DateTime result3;
        CultureInfo provider = CultureInfo.InvariantCulture;
        durumtakip_aktif AnaForm_F = new durumtakip_aktif();

        List<Firmoklist> Firmokliste = new List<Firmoklist>();
         ArrayList siparis_renk_dizi = new ArrayList();
        string musterino = "";
        ArrayList mail_dizi = new ArrayList();
        int msj_id = 0;
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


        }

        public static  veriler ss = new veriler();

         public static veriler Order;
        List<veriler> NewOrder = new List<veriler>();


        public durumtakip_cp()
        {
            InitializeComponent();

          

       
            
        }

        private static readonly string LOG_FILENAME = Path.GetTempPath() + "AldersLog.txt";

        public static void LogMessageToFile(string msg)

        {

            msg = string.Format("{0:G}: {1}rn ------->", DateTime.Now + "/" +cInfo.cUsername, msg);

            File.AppendAllText(LOG_FILENAME, msg);

        }
        void ilk() {

            //select renk, adet = sum(coalesce(adet, 0)) from sipmodel where renk is not null and renk<> '' and siparisno = 'ZR-1971-052-ELB' group by renk
            
            con = new SqlDbConnect();
            siparis_renk_dizi.Clear();
            gRenk.Properties.Items.Clear();
            gRenk.Properties.Items.Add("HEPSI");
            con.LoginQuery("select renk from sipmodel where renk is not null and renk<> '' and siparisno = '" + secili_siparis.Text.Trim() + "' group by renk");
            while (con.dbr.Read())
            {
                gRenk.Properties.Items.Add( con.dbr["renk"].ToString().Trim());
                siparis_renk_dizi.Add(con.dbr["renk"].ToString().Trim());
            }
            con.dbr.Close();

            gSorumlu.Properties.Items.Clear();


            con.LoginQuery("select personel,email from personel with (NOLOCK)");
            while (con.dbr.Read())
            {
                gSorumlu.Properties.Items.Add(con.dbr["personel"].ToString().Trim());
            }
            con.dbr.Close();



            Order = new veriler();
            

            con.Close();

           


           






        }
        
        private void durumtakip_cp_Load(object sender, EventArgs e)
        {
            musteri_ver();
            ilk();
            liste_ver();
                       
                if ("ADMIN".Contains(cInfo.cUsername) || cInfo.cUsername.Contains("MELİH") || cInfo.cUsername.Contains("OKAN") || cInfo.cUsername.Trim() == secili_mt.Text.Trim())
            {
              groupControl1.Visible = true;
            }
            else { groupControl1.Visible = false; }

            
        }

        DataTable table = new DataTable();
        double gecgonderi, gecok,durum;

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

            
             
            //view.Columns[5].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            foreach (var value in NewOrder)
            {
                gecgonderi = 0;
                gecok = 0;
                durum = 0;

                //if (value.plgonderitarihi.Trim() != "" && value.pltarihi.Trim() != "")
                //{


                //    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //    if (value.OKtar2.ToString().Trim() == "")
                //    {
                //        if (DateTime.Parse(value.plgonderitarihi) < DateTime.Now)
                //        {
                //            gecgonderi = GetBusinessDays(DateTime.Now, DateTime.Parse(value.plgonderitarihi));

                //            durum = 1;
                //        }
                //        else if (DateTime.Parse(value.plgonderitarihi) == DateTime.Now)
                //        {
                //            durum = 1;
                //            gecgonderi = 0;
                //        }

                //    }
                //    else
                //    {

                //        if (DateTime.Parse(value.plgonderitarihi) < DateTime.Parse(value.OKtar2))
                //        {
                //            gecgonderi = GetBusinessDays(DateTime.Parse(value.OKtar2), DateTime.Parse(value.plgonderitarihi));

                //        }
                //        else if (DateTime.Parse(value.plgonderitarihi) == DateTime.Parse(value.OKtar2))
                //        { gecgonderi = 0; }
                //        else
                //        {
                //            gecgonderi = GetBusinessDays(DateTime.Parse(value.OKtar2), DateTime.Parse(value.plgonderitarihi));
                //        }


                //    }

                //    /////////////////////////////////////////////////////////////////////////////////////////////////////////////



                //    if (value.OKTar.ToString().Trim() == "")
                //    {
                //        if (DateTime.Parse(value.pltarihi) < DateTime.Now)
                //        {
                //            durum = 1;
                //            gecok = GetBusinessDays(DateTime.Now, DateTime.Parse(value.pltarihi));
                //        }
                //        else if (DateTime.Parse(value.pltarihi) == DateTime.Now)
                //        { durum = 1; gecok = 0; }

                //    }
                //    else
                //    {
                //        if (DateTime.Parse(value.pltarihi) < DateTime.Parse(value.OKTar))
                //        {
                //            gecok = GetBusinessDays(DateTime.Parse(value.OKTar), DateTime.Parse(value.pltarihi));
                //        }
                //        else if (DateTime.Parse(value.pltarihi) == DateTime.Parse(value.OKTar))
                //        {
                //            gecok = 0;
                //        }

                //        else
                //        {
                //            gecok = GetBusinessDays(DateTime.Parse(value.OKTar), DateTime.Parse(value.pltarihi));
                //        }

                //    }

                //    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //    if (value.ok != "" && value.ok == "H")
                //    {
                //        durum = 2;

                //    }

                //}

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

                if (value.plgonderitarihi.Trim() == "" && value.pltarihi.Trim() == "") {

                    table.Rows.Add(new Object[] { value.oktipi,
                    value.Renk,
                    DateTime.Parse("01.01.2018").ToString("dd.MM.yyyy"),
                    DateTime.Parse("01.01.2018").ToString("dd.MM.yyyy"),
                   value.OKtar2,
                   value.OKTar,
                    value.notlar,
                    value.sorumlu,value.teminsuresi,value.ok, Math.Round(gecgonderi, MidpointRounding.ToEven), Math.Round(gecok, MidpointRounding.ToEven),value.sirano,value.uyari });


                }
                else {


                    table.Rows.Add(new Object[] { value.oktipi,
                    value.Renk,
                    DateTime.Parse(value.plgonderitarihi).ToString("dd.MM.yyyy"),
                    DateTime.Parse(value.pltarihi).ToString("dd.MM.yyyy"),
                   value.OKtar2,
                   value.OKTar,
                    value.notlar,
                    value.sorumlu,value.teminsuresi,value.ok, Math.Round(gecgonderi, MidpointRounding.ToEven), Math.Round(gecok, MidpointRounding.ToEven),value.sirano,value.uyari });


                }




                /*repositoryItemDateEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
                repositoryItemDateEdit1.Mask.EditMask = "g";
                repositoryItemDateEdit1.Mask.UseMaskAsDisplayFormat = true;*/
            }

            NewOrder.Clear();

            return table;
        }

        public static double GetBusinessDays(DateTime startD, DateTime endD)
        {
            double calcBusinessDays = 1 + ((endD - startD).TotalDays * 5 - (startD.DayOfWeek - endD.DayOfWeek) * 2) / 7;

            if ((int)endD.DayOfWeek == 6) calcBusinessDays--;

            if ((int)startD.DayOfWeek == 0) calcBusinessDays--;

            return calcBusinessDays - 1;
        }
        void liste_ver()
        {
            gridControl1.DataSource = null;
            

            if (secili_siparis.Text.Trim() != "")
            {


                try
                {

                    con = new SqlDbConnect();
                    /* con.SqlQuery("select oktipi,Renk,plgonderitarihi,OKtar2,pltarihi,OKTar,notlar,sorumlu,teminsuresi,ok,ulineno,sirano,uyari from sipok with (NOLOCK) where siparisno='" + secili_siparis.Text.Trim() + "' ");
                     SqlDataAdapter sqlDataAdap = new SqlDataAdapter(con.Cmd);
                     DataTable dtRecord = new DataTable();
                     sqlDataAdap.Fill(dtRecord);
                     grid_cp.DataSource = dtRecord;*/


                    con.LoginQuery("select oktipi,Renk,plgonderitarihi,OKtar2,pltarihi,OKTar,notlar,sorumlu,teminsuresi,ok,ulineno,sirano,uyari from sipok with (NOLOCK) where siparisno='" + secili_siparis.Text.Trim() + "' ");
                    while (con.dbr.Read())
                    {
                        Order = new veriler();
                        Order.oktipi = con.dbr["oktipi"].ToString().Trim();
                        Order.Renk = con.dbr["Renk"].ToString().Trim();
                        //    Order.plgonderitarihi = String.Format("{0:MM.dd.YYYY}", con.dbr["plgonderitarihi"].ToString().Trim());
                        Order.plgonderitarihi = con.dbr["plgonderitarihi"].ToString().Trim();
                        Order.OKtar2 = con.dbr["OKtar2"].ToString().Trim();
                        Order.pltarihi = con.dbr["pltarihi"].ToString().Trim();
                        Order.OKTar = con.dbr["OKTar"].ToString().Trim();
                        Order.notlar = con.dbr["notlar"].ToString().Trim();
                        Order.sorumlu = con.dbr["sorumlu"].ToString().Trim();
                        Order.teminsuresi = con.dbr["teminsuresi"].ToString().Trim();
                        Order.ok = con.dbr["ok"].ToString().Trim();
                        Order.uyari = con.dbr["uyari"].ToString().Trim();
                        Order.sirano= con.dbr["sirano"].ToString().Trim();
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
                    /*
                    this.gridView3.Columns[2].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    this.gridView3.Columns[2].DisplayFormat.FormatString = "g";
                    */


                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }





                try
                {
                    con = new SqlDbConnect();
                    con.SqlQuery("select ozet,tarih,oktipi,Renk,plgonderitarihi,pltarihi,notlar,sorumlu,teminsuresi from sipoklog with (NOLOCK) where siparisno='" + secili_siparis.Text.Trim() + "' ");
                    SqlDataAdapter sqlDataAdap = new SqlDataAdapter(con.Cmd);
                    DataTable dtRecord = new DataTable();
                    sqlDataAdap.Fill(dtRecord);
                    grid_log.DataSource = dtRecord;



                    con.Close();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }



            }



             


        }


         


         

        void kutu_temizle() {

            kayit_no.Text = "";

            gKonu.Text = "";
            gRenk.Text = "";
            gPlangontar.Text = "";
            gPlanOktar.Text = "";
            gSorumlu.Text = "";
            gTemin.Text = "";
            gnot.Text = "";
            gcheckuyari.Checked = true;

            eKonu.Text = "";
            eRenk.Text = "";
            ePlangontarih.Text = "";
            ePlanOktarih.Text = "";
            eSorumlu.Text = "";
            eTemin.Text = "";
            enot.Text = "";
            echeckuyari.Checked = false;

            gPlanOktar.Enabled = true;
            gPlangontar.Enabled = true;
        }
       
        private void btn_sil_Click(object sender, EventArgs e)
        {

        }

        double renkgon, renkok = 0;
        



        int kalan_zaman(DateTime tar1, DateTime tar2)
        {


            //DateTime result = DateTime.ParseExact(kalan_time, format, provider);


            result2 = DateTime.ParseExact(String.Format("{0:dd.MM.yyyy}", DateTime.Now).ToString(), format, provider);
            double kalan_is_gun = GetBusinessDays(result2, result) % 5;
            int kalan_hafta = Convert.ToInt32(GetBusinessDays(result2, result)) / 5;
            // btn_kalanzaman.Text = kalan_hafta.ToString() + " H - " + kalan_is_gun + " G";

            //  MessageBox.Show(kalan_time +"-->"+ kalan_hafta.ToString() + " H - " + kalan_is_gun + " G");
            return 1;
        }

       

        private void btn_msjgonder_Click_1(object sender, EventArgs e)
        {


            if (gSorumlu.Text.Trim() == "" || gPlangontar.Text.Trim() == "" || gPlanOktar.Text.Trim() == "" || gKonu.Text.Trim() == "" || gRenk.Text.Trim() == "" || gTemin.Text.Trim() == "")
            {
                MessageBox.Show("Boş yerleri doldurmalısınız !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (kayit_no.Text.Trim() != "")
            {//update

                try
                {

                    con = new SqlDbConnect();
                    // oktipi,Renk,plgonderitarihi,OKtar2,pltarihi,OKTar,notlar,sorumlu,teminsuresi,ok,ulineno,sirano

                    string semail = "";

                    con.LoginQuery("select email from personel with (NOLOCK) where personel='" + gSorumlu.Text.Trim() + "'");
                    while (con.dbr.Read())
                    {
                        semail = con.dbr["email"].ToString().Trim();

                    }
                    con.dbr.Close();


                    con.SqlQuery("UPDATE sipok SET  oktipi=@oktipi,Renk=@Renk,plgonderitarihi=@plgonderitarihi,pltarihi=@pltarihi,notlar=@notlar,sorumlu=@sorumlu,teminsuresi=@teminsuresi,semail=@semail,uyari=@uyari where sirano='" + kayit_no.Text.Trim() + "'");

                    con.Cmd.Parameters.AddWithValue("@oktipi", gKonu.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@Renk", gRenk.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@plgonderitarihi", gPlangontar.EditValue.ToString());
                    con.Cmd.Parameters.AddWithValue("@pltarihi", gPlanOktar.EditValue.ToString());
                    con.Cmd.Parameters.AddWithValue("@notlar", gnot.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@sorumlu", gSorumlu.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@teminsuresi", gTemin.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@semail", semail);

                    if (gcheckuyari.Checked == true)
                    {
                        con.Cmd.Parameters.AddWithValue("@uyari", "E");
                    }
                    else { con.Cmd.Parameters.AddWithValue("@uyari", "H"); }

                    con.QueryNonEx();


                    string ozet = "Update";

                    if (gKonu.Text.Trim() != eKonu.Text.Trim()) { ozet = ozet + ",Konu"; }
                    if (gRenk.Text.Trim() != eRenk.Text.Trim()) { ozet = ozet + ",Renk"; }
                    if (gPlangontar.Text.Trim() != ePlangontarih.Text.Trim()) { ozet = ozet + ",Gön.Tarihi"; }
                    if (gPlanOktar.Text.Trim() != ePlanOktarih.Text.Trim()) { ozet = ozet + ",OkTarihi"; }
                    if (gnot.Text.Trim() != enot.Text.Trim()) { ozet = ozet + ",not"; }
                    if (gSorumlu.Text.Trim() != eSorumlu.Text.Trim()) { ozet = ozet + ",sorumlu"; }
                    if (gTemin.Text.Trim() != eTemin.Text.Trim()) { ozet = ozet + ",Temin"; }

                    if ((gcheckuyari.Checked == true && echeckuyari.Checked == true) || (gcheckuyari.Checked == false && echeckuyari.Checked == false))
                    {

                    }
                    else
                    {
                        if ((gcheckuyari.Checked == false && echeckuyari.Checked == true))
                        {
                            ozet = ozet + ",Uyarı Maili Kapatıldı";
                        }
                    }

                    con.SqlQuery("INSERT INTO sipoklog (siparisno,oktipi,Renk,plgonderitarihi,pltarihi,notlar,sorumlu,teminsuresi,uyari,oktipi1,Renk1,plgonderitarihi1,pltarihi1,notlar1,sorumlu1,teminsuresi1,uyari1,tarih,username,ozet) VALUES (@siparisno,@oktipi,@Renk,@plgonderitarihi,@pltarihi,@notlar,@sorumlu,@teminsuresi,@uyari,@oktipi1,@Renk1,@plgonderitarihi1,@pltarihi1,@notlar1,@sorumlu1,@teminsuresi1,@uyari1,@tarih,@username,@ozet)");
                    con.Cmd.Parameters.AddWithValue("@siparisno", secili_siparis.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@oktipi", gKonu.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@Renk", gRenk.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@plgonderitarihi", gPlangontar.EditValue.ToString());
                    con.Cmd.Parameters.AddWithValue("@pltarihi", gPlanOktar.EditValue.ToString());
                    con.Cmd.Parameters.AddWithValue("@notlar", gnot.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@sorumlu", gSorumlu.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@teminsuresi", gTemin.Text.Trim());

                    if (gcheckuyari.Checked == true) { con.Cmd.Parameters.AddWithValue("@uyari", "E"); }
                    else { con.Cmd.Parameters.AddWithValue("@uyari", "H"); }

                    con.Cmd.Parameters.AddWithValue("@oktipi1", eKonu.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@Renk1", eRenk.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@plgonderitarihi1", ePlangontarih.EditValue.ToString());
                    con.Cmd.Parameters.AddWithValue("@pltarihi1", ePlanOktarih.EditValue.ToString());
                    con.Cmd.Parameters.AddWithValue("@notlar1", enot.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@sorumlu1", eSorumlu.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@teminsuresi1", eTemin.Text.Trim());

                    if (echeckuyari.Checked == true) { con.Cmd.Parameters.AddWithValue("@uyari1", "E"); } else { con.Cmd.Parameters.AddWithValue("@uyari1", "H"); }
                    
                    con.Cmd.Parameters.AddWithValue("@tarih", DateTime.Now);
                    con.Cmd.Parameters.AddWithValue("@username", cInfo.cUsername);
                    con.Cmd.Parameters.AddWithValue("@ozet", ozet);
                    con.QueryNonEx();


                    con.Close();



                    kutu_temizle();
                    liste_ver();

                    MessageBox.Show("Güncelleme işlemi tamamlandı !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }
            else
            {
                //insert

                try
                {

                    con = new SqlDbConnect();
                    // oktipi,Renk,plgonderitarihi,OKtar2,pltarihi,OKTar,notlar,sorumlu,teminsuresi,ok,ulineno,sirano

                    string semail = "";

                    con.LoginQuery("select email from personel with (NOLOCK) where personel='" + gSorumlu.Text.Trim() + "'");
                    while (con.dbr.Read())
                    {
                        semail = con.dbr["email"].ToString().Trim();

                    }
                    con.dbr.Close();


                    con.SqlQuery("INSERT INTO sipok (siparisno,ModelKodu,Beden,oktipi,Renk,plgonderitarihi,pltarihi,notlar,sorumlu,teminsuresi,semail,uyari) VALUES (@siparisno,@ModelKodu,@Beden,@oktipi,@Renk,@plgonderitarihi,@pltarihi,@notlar,@sorumlu,@teminsuresi,@semail,@uyari)");
                    con.Cmd.Parameters.AddWithValue("@siparisno", secili_siparis.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@ModelKodu", "HEPSI");
                    con.Cmd.Parameters.AddWithValue("@Beden", "HEPSI");
                    con.Cmd.Parameters.AddWithValue("@oktipi", gKonu.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@Renk", gRenk.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@plgonderitarihi", gPlangontar.EditValue.ToString());
                    con.Cmd.Parameters.AddWithValue("@pltarihi", gPlanOktar.EditValue.ToString());
                    con.Cmd.Parameters.AddWithValue("@notlar", gnot.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@sorumlu", gSorumlu.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@teminsuresi", gTemin.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@semail", semail);
                    con.Cmd.Parameters.AddWithValue("@uyari", "E");
                    con.QueryNonEx();


                    con.SqlQuery("INSERT INTO sipoklog (siparisno,oktipi,Renk,plgonderitarihi,pltarihi,notlar,sorumlu,teminsuresi,uyari,tarih,username,ozet) VALUES (@siparisno,@oktipi,@Renk,@plgonderitarihi,@pltarihi,@notlar,@sorumlu,@teminsuresi,@uyari,@tarih,@username,@ozet)");
                    con.Cmd.Parameters.AddWithValue("@siparisno", secili_siparis.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@oktipi", gKonu.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@Renk", gRenk.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@plgonderitarihi", gPlangontar.EditValue.ToString());
                    con.Cmd.Parameters.AddWithValue("@pltarihi", gPlanOktar.EditValue.ToString());
                    con.Cmd.Parameters.AddWithValue("@notlar", gnot.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@sorumlu", gSorumlu.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@teminsuresi", gTemin.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@uyari", "E");
                    con.Cmd.Parameters.AddWithValue("@tarih", DateTime.Now);
                    con.Cmd.Parameters.AddWithValue("@username", cInfo.cUsername);
                    con.Cmd.Parameters.AddWithValue("@ozet", "Yeni");
                    con.QueryNonEx();

                    con.Close();



                    kutu_temizle();
                    liste_ver();
                    MessageBox.Show("Kayıt işlemi tamamlandı !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
                catch (Exception Ex)
                {
                    LogMessageToFile("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz->" + secili_siparis.Text + "/" + cInfo.cUsername + "/" + Environment.NewLine + Ex.ToString());
                    MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }
        }

        private void gridView3_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;
 
             
            view.Columns["Plan.Gönderi Tar."].DisplayFormat.FormatType =  DevExpress.Utils.FormatType.DateTime;
            view.Columns["Plan.Gönderi Tar."].DisplayFormat.FormatString = "D";


            view.Columns[2].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            view.Columns[2].DisplayFormat.FormatString = "D";
            view.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            view.Columns[3].DisplayFormat.FormatString = "D";

            view.Columns[4].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            view.Columns[4].DisplayFormat.FormatString = "D";
            view.Columns[5].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            view.Columns[5].DisplayFormat.FormatString = "D";

            /*
            this.gridView3.Columns[2].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridView3.Columns[2].DisplayFormat.FormatString = "g";

            this.gridView3.Columns[2].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridView3.Columns[2].DisplayFormat.FormatString = "g";
            */

            //if (e.RowHandle != view.FocusedRowHandle &&
            //  ((e.RowHandle % 2 == 0 && e.Column.VisibleIndex % 2 == 1) ||
            //  (e.Column.VisibleIndex % 2 == 0 && e.RowHandle % 2 == 1)))
            //   e.Appearance.BackColor = Color.NavajoWhite; 

            /* if (e.RowHandle % 2 ==1 ) e.Appearance.BackColor = Color.NavajoWhite;

             view.SetRowCellValue(e.RowHandle, cGecikmegon, "1 ->" + 1);

             string test = (gridView1.GetRowCellValue(e.RowHandle, cPlangontarih)).ToString();


             if(gridView1.GetRowCellValue(e.RowHandle, cPlangontarih) != null)
             {


                gridView1.SetRowCellValue(e.RowHandle,cGecikmegon,"1");
             }


             for (int i = 0; i <= view.RowCount; i++) {

                 view.SetRowCellValue(i, cGecikmegon, "1 ->" + 1);

             }


            table.Columns.Add("Gönderim Gecikme");
            table.Columns.Add("OK Gecikme")


             gridView1.SetRowCellValue(e.RowHandle, cGecikmegon, "1");*/




            try
            {
                renkgon = Convert.ToDouble(gridView3.GetRowCellValue(e.RowHandle, "Gönderim Gecikme"));
                renkok = Convert.ToDouble(gridView3.GetRowCellValue(e.RowHandle, "OK Gecikme"));
                //  durum = Convert.ToDouble(gridView3.GetRowCellValue(e.RowHandle, "kirmizi"));


            }
            catch { }

            try
            {
                if ((renkgon >= 0 && renkgon <= 2) || (renkok >= 0 && renkok <= 2)) { e.Appearance.BackColor = Color.NavajoWhite; }
                if (renkgon < 0 || renkok < 0) { e.Appearance.BackColor = Color.Salmon; e.Appearance.ForeColor = Color.White; }


            }
            catch { }





        }

        private void btn_gon_Click(object sender, EventArgs e)
        {
            if (cInfo.cUsername.Trim() == secili_mt.Text.Trim() || gSorumlu.Text.Trim() == cInfo.cUsername.Trim() || "ADMIN".Contains(cInfo.cUsername) || cInfo.cUsername.Contains("MELİH"))
            {
                Cursor.Current = Cursors.WaitCursor;
                // if (gSorumlu.Text.Trim() != cInfo.cUsername) { MessageBox.Show("Yekili Kişi yapabilir"); }
                if (kayit_no.Text.Trim() != "")
            {//update

                   // MessageBox.Show("geldi ");

                 //   return;
                   


                try
                {
                    con = new SqlDbConnect();
                    // oktipi,Renk,plgonderitarihi,OKtar2,pltarihi,OKTar,notlar,sorumlu,teminsuresi,ok,ulineno,sirano

                    string semail = "";

                    con.LoginQuery("select email from personel with (NOLOCK) where personel='" + gSorumlu.Text.Trim() + "'");
                    while (con.dbr.Read())
                    {
                        semail = con.dbr["email"].ToString().Trim();

                    }
                    con.dbr.Close();


                    con.SqlQuery("UPDATE sipok SET  OKtar2=@OKtar2 where sirano='" + kayit_no.Text.Trim() + "'");
                    con.Cmd.Parameters.AddWithValue("@OKtar2", DateTime.Now); 

 


                    con.QueryNonEx();

                    con.Close();




                    /**********************************************************************************************/

                    //string tum_mailler = String.Join(",", mail_dizi.ToArray());
                    mail_dizi.Clear();
                    string tum_mailler = "";

                    planlamaci_ver();
                    mailler(txt_gonmails.Text.Trim());
                    tum_mailler = String.Join(",", mail_dizi.ToArray());

                    string Subject = secili_siparis.Text + " / " + secili_modeladi.Text + " " +gKonu.Text.Trim()+" Gönderimi  Yapıldı ";
                    con = new SqlDbConnect();

                    con.SqlQuery("INSERT INTO durumtakip_ms (orderno,uname,umail,mesaj,tarih,udepartman,bilgi) VALUES (@orderno,@uname,@umail,@mesaj,@tarih,@udepartman,@bilgi)");
                    con.Cmd.Parameters.AddWithValue("@orderno", secili_siparis.Text);
                    con.Cmd.Parameters.AddWithValue("@uname", cInfo.cUsername);
                    con.Cmd.Parameters.AddWithValue("@umail", cInfo.cUsermail);
                    con.Cmd.Parameters.AddWithValue("@mesaj", "CP den > Konu: " + gKonu.Text.Trim() + " gönderimi yapıldı, not :" + not_gonderi.Text.Trim() +".");
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


                    not_gonderi.Text = "";

                    con.QueryNonEx();
                    Heryerden.Gun1 = 1;

                    if (not_gonderi.Text.Trim() != "")
                    {
                        con.SqlQuery("UPDATE sipok SET notlar=@notlar where sirano='" + kayit_no.Text.Trim() + "'");

                        con.Cmd.Parameters.AddWithValue("@notlar", not_gonderi.Text.Trim() + "-->" + cInfo.cUsername + "-->" + DateTime.Now.ToString());

                        con.QueryNonEx();
                    }


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
                    /*************************************************************************************************************************/







                    kutu_temizle();
                    liste_ver();





                }
                catch (Exception Ex)
                {
                    LogMessageToFile("durumtakip_cp->btn_gon_Click->" + secili_siparis.Text + "/" + cInfo.cUsername + "/" + Environment.NewLine + Ex.ToString());
                    MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
                Cursor.Current = Cursors.Default;
            }
            else { MessageBox.Show("Lütfen siparişin müşteri temsilcisine başvurun "); return; }
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (cInfo.cUsername.Trim() == secili_mt.Text.Trim() || gSorumlu.Text.Trim() == cInfo.cUsername.Trim() || "ADMIN".Contains(cInfo.cUsername) || cInfo.cUsername.Contains("MELİH"))
            {
                Cursor.Current = Cursors.WaitCursor;
                // if (gSorumlu.Text.Trim() != cInfo.cUsername) { MessageBox.Show("Yekili Kişi yapabilir"); return; }
                if (kayit_no.Text.Trim() != "")
                {//update

                if (sonuc_ok.Text.Trim() == "") { MessageBox.Show("Sonuç belirtmediniz"); return; }


                try
                    {
                        con = new SqlDbConnect();
                        // oktipi,Renk,plgonderitarihi,OKtar2,pltarihi,OKTar,notlar,sorumlu,teminsuresi,ok,ulineno,sirano

                        string semail = "";

                        con.LoginQuery("select email from personel with (NOLOCK) where personel='" + gSorumlu.Text.Trim() + "'");
                        while (con.dbr.Read())
                        {
                            semail = con.dbr["email"].ToString().Trim();

                        }
                        con.dbr.Close();


                        con.SqlQuery("UPDATE sipok SET  OKtar=@OKtar, ok=@ok where sirano='" + kayit_no.Text.Trim() + "'");
                        con.Cmd.Parameters.AddWithValue("@OKtar", DateTime.Now);
                        con.Cmd.Parameters.AddWithValue("@ok", sonuc_ok.Text.Trim());



                        con.QueryNonEx();

                        con.Close();




                    /**********************************************************************************************/

                    //string tum_mailler = String.Join(",", mail_dizi.ToArray());
                    mail_dizi.Clear();
                    string tum_mailler = "";

                    planlamaci_ver();

                        
                    mailler(txt_gonmails.Text.Trim());

                        if (semail.Trim() != "") { mail_dizi.Add(semail); }
                        tum_mailler = String.Join(",", mail_dizi.ToArray());

                    // mesajda    string Subject = secili_siparis.Text + " / " + secili_modeladi.Text + " CP konu:" + gKonu + " OK  Hakkında. Sonuç: " + sonuc_ok.Text.Trim();

                        string Subject = secili_siparis.Text + " / " + secili_modeladi.Text + " " + gKonu.Text.Trim() + " Renk(" + gRenk.Text.Trim() + "), okeyi sonuçlandı , sonuç :" + sonuc_ok.Text.Trim();
                        con = new SqlDbConnect();

                        con.SqlQuery("INSERT INTO durumtakip_ms (orderno,uname,umail,mesaj,tarih,udepartman,bilgi) VALUES (@orderno,@uname,@umail,@mesaj,@tarih,@udepartman,@bilgi)");
                        con.Cmd.Parameters.AddWithValue("@orderno", secili_siparis.Text);
                        con.Cmd.Parameters.AddWithValue("@uname", cInfo.cUsername);
                        con.Cmd.Parameters.AddWithValue("@umail", cInfo.cUsermail);
                        con.Cmd.Parameters.AddWithValue("@mesaj", "CP den > Konu: " + gKonu.Text.Trim() + " Renk(" + gRenk.Text.Trim() + "), Sonuç : " + sonuc_ok.Text.Trim()+"   --> Not: " + not_ok.Text.Trim() + ".");
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


                        not_gonderi.Text = "";

                        con.QueryNonEx();
                        Heryerden.Gun1 = 1;

                    if (not_ok.Text.Trim() != "")
                    {
                        con.SqlQuery("UPDATE sipok SET notlar=@notlar where sirano='" + kayit_no.Text.Trim() + "'");

                        con.Cmd.Parameters.AddWithValue("@notlar", not_ok.Text.Trim() + "-->" + cInfo.cUsername + "-->" + DateTime.Now.ToString());

                        con.QueryNonEx();
                    }



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
                    /*************************************************************************************************************************/






                    kutu_temizle();
                    liste_ver();






                }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Şöyle bir sorun oluşmuş bunu iletiniz ---> "+ ex.ToString());
                    }

                }
                Cursor.Current = Cursors.Default;
            }
            else { MessageBox.Show("Lütfen siparişin müşteri temsilcisine başvurun "); return; }

        }

        private void Btn_islemler_Click(object sender, EventArgs e)
        {
            popup_islemler.ShowPopup(Control.MousePosition);
        }

        private void pbtn_mustericpgetir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            con = new SqlDbConnect();
            // oktipi,oktipieng,Renk,plgonderitarihi,OKtar2,pltarihi,OKTar,notlar,sorumlu,teminsuresi,ok,ulineno,sirano
            string musterino = "";
            con.LoginQuery("select musterino from siparis with (NOLOCK) where kullanicisipno='" + secili_siparis.Text.Trim() + "'");
            while (con.dbr.Read())
            {
                musterino = con.dbr["musterino"].ToString().Trim();

            }
            con.dbr.Close();

            //*************************************************************************************************************************
            string semail = "";

            con.LoginQuery("select email from personel with (NOLOCK) where personel='" + gSorumlu.Text.Trim() + "'");
            while (con.dbr.Read())
            {
                semail = con.dbr["email"].ToString().Trim();

            }
            con.dbr.Close();

            //*************************************************************************************************************************************************


            Firmokliste.Clear();
            con.LoginQuery("select * from frmoklist with (NOLOCK) where formno='" + musterino + "'");
            while (con.dbr.Read())
            {
                Firmaok = new Firmoklist();

                Firmaok.oktipi=  con.dbr["oktipi"].ToString().Trim();
                Firmaok.oktipieng = con.dbr["oktipieng"].ToString().Trim();
                Firmaok.aciklama = con.dbr["aciklama"].ToString().Trim();
                Firmaok.bedendetay = con.dbr["bedendetay"].ToString().Trim();
                Firmaok.renkdetay = con.dbr["renkdetay"].ToString().Trim();
                Firmaok.modeldetay = con.dbr["modeldetay"].ToString().Trim();
                Firmaok.formno = con.dbr["formno"].ToString().Trim();
                Firmaok.sira = con.dbr["sira"].ToString().Trim();
                Firmaok.sirano = con.dbr["sirano"].ToString().Trim();
          
                Firmaok.sorumlu = con.dbr["sorumlu"].ToString().Trim();
                Firmaok.temin = con.dbr["temin"].ToString().Trim();
                Firmokliste.Add(Firmaok);


            }
            con.dbr.Close();







            string semail2 = "";
            string sqlq = "";
            foreach (var value in Firmokliste)

            {
                try { 
                

                    con.LoginQuery("select email from personel with (NOLOCK) where personel='" + value.sorumlu + "'");
                    while (con.dbr.Read())
                    {
                        semail2 = con.dbr["email"].ToString().Trim();
                    }
                    con.dbr.Close();

                    if (value.renkdetay == "E")
                    {
                        /************************************************************/
                        for (int i = 0; i < siparis_renk_dizi.Count; i++)
                        {

                            // mail_dizi[i].ToString()


                            con.SqlQuery("INSERT INTO sipok (siparisno,ModelKodu,pltarihi,plgonderitarihi,Beden,oktipi,oktipieng,Renk,notlar,sorumlu,teminsuresi,semail,uyari) VALUES (@siparisno,@ModelKodu,@pltarihi,@plgonderitarihi,@Beden,@oktipi,@oktipieng,@Renk,@notlar,@sorumlu,@teminsuresi,@semail,@uyari)");
                            con.Cmd.Parameters.AddWithValue("@siparisno", secili_siparis.Text.Trim());
                            con.Cmd.Parameters.AddWithValue("@ModelKodu", "HEPSI");

                            con.Cmd.Parameters.AddWithValue("@pltarihi", DateTime.Now);
                            con.Cmd.Parameters.AddWithValue("@plgonderitarihi", DateTime.Now);

                            con.Cmd.Parameters.AddWithValue("@Beden", "HEPSI");
                            con.Cmd.Parameters.AddWithValue("@oktipi", value.oktipi);
                            con.Cmd.Parameters.AddWithValue("@oktipieng", value.oktipieng);
                            con.Cmd.Parameters.AddWithValue("@Renk", siparis_renk_dizi[i].ToString());

                            con.Cmd.Parameters.AddWithValue("@notlar", value.aciklama);
                            con.Cmd.Parameters.AddWithValue("@sorumlu", value.sorumlu);

                            if (value.temin != "")
                            {
                                con.Cmd.Parameters.AddWithValue("@teminsuresi", decimal.Parse(value.temin));
                            }
                            else { con.Cmd.Parameters.AddWithValue("@teminsuresi", 0); }

                            con.Cmd.Parameters.AddWithValue("@semail", semail2);
                            con.Cmd.Parameters.AddWithValue("@uyari", "E");
                            con.QueryNonEx();


                        }
                        /************************************************************/

                    }
                    else if (value.renkdetay == "H")
                    {
                        sqlq= "INSERT INTO sipok (siparisno,ModelKodu,Beden,oktipi,oktipieng,Renk,notlar,sorumlu,teminsuresi,semail,uyari) VALUES (@siparisno,@ModelKodu,@Beden,@oktipi,@oktipieng,@Renk,@notlar,@sorumlu,@teminsuresi,@semail,@uyari)";
                        con.SqlQuery(sqlq);
                        con.Cmd.Parameters.AddWithValue("@siparisno", secili_siparis.Text.Trim());
                        
                        con.Cmd.Parameters.AddWithValue("@ModelKodu", "HEPSI");
                        con.Cmd.Parameters.AddWithValue("@Beden", "HEPSI");
                        con.Cmd.Parameters.AddWithValue("@oktipi", value.oktipi);
                        con.Cmd.Parameters.AddWithValue("@oktipieng", value.oktipieng);
                        con.Cmd.Parameters.AddWithValue("@Renk", "HEPSI");

                        con.Cmd.Parameters.AddWithValue("@notlar", value.aciklama);
                        con.Cmd.Parameters.AddWithValue("@sorumlu", value.sorumlu);
                        if (value.temin != "")
                        {
                            con.Cmd.Parameters.AddWithValue("@teminsuresi", decimal.Parse(value.temin));
                        }
                        else { con.Cmd.Parameters.AddWithValue("@teminsuresi", 0); }
                        con.Cmd.Parameters.AddWithValue("@semail", semail2);
                        con.Cmd.Parameters.AddWithValue("@uyari", "E");
                        con.QueryNonEx();
                    }



            } catch (Exception Ex)
            {
               // MessageBox.Show("sqlq >>>>>"+ secili_siparis.Text.Trim() + ">>" + Environment.NewLine + Ex.ToString());
            }


        }
              

  con.Close();





            liste_ver();
        }

        void  planlamaci_ver() {

            if (musterino == "H&M" || musterino == "COS" || musterino == "OTHER STORIES" || musterino == "OS" || musterino == "WEEKDAY" || musterino == "MONKI" || musterino == "MQ" || musterino == "ARKET")
            { mail_dizi.Add("bulentalagoz@alders.com.tr"); }
             
           else if (musterino == "LINDEX" || musterino == "OPUS" || musterino == "LİNDEX" || musterino == "SAINSBURYS" || musterino == "INDISKA" || musterino == "LPP" || musterino == "ZARA" || musterino == "ZARA KIDS" || musterino == "MASSIMO DUTTI")

                { mail_dizi.Add("tubakaplan@alders.com.tr"); }
             
            
        }

        void mailler(string mails) {


            if(mails.Trim()!="")
            try
            {
                string[] words = mails.ToString().Split(',');
               foreach (string i in words)
                {
                        if (i.Trim() != "") { 
                         mail_dizi.Add(i.Trim());
                        }
                    }
            }
            catch { }

            try
            {
                con = new SqlDbConnect();
                con.LoginQuery("select email from personel with (NOLOCK) where personel='" + secili_mt.Text.Trim() + "'");
                while (con.dbr.Read())
                {
                    mail_dizi.Add(con.dbr["email"].ToString().Trim());
                }
                con.dbr.Close();
                con.Close();
            }
            catch { }


            /************************************************************/
            for (int i = 0; i < mail_dizi.Count; i++)
            {


                for (int j = i + 1; j < mail_dizi.Count; j++)
                {
                    if (mail_dizi[i].ToString() == mail_dizi[j].ToString())
                    { mail_dizi.Remove(mail_dizi[j]); }
                }
            }
            /************************************************************/


        }

        void musteri_ver() {
            
            try
            {
                con = new SqlDbConnect();
                con.LoginQuery("select musterino from siparis with (NOLOCK) where kullanicisipno='" + secili_siparis.Text.Trim() + "'");
                while (con.dbr.Read())
                {
                    musterino = con.dbr["musterino"].ToString().Trim();

                }
                con.dbr.Close();
                con.Close();
               
            }
            catch { }
            
        }
        private void btn_gonderi_msj_Click(object sender, EventArgs e)
        {

           

            Cursor.Current = Cursors.WaitCursor;

            if (cInfo.cUsername.Trim() == secili_mt.Text.Trim() || gSorumlu.Text.Trim() == cInfo.cUsername.Trim() || "ADMIN".Contains(cInfo.cUsername) || cInfo.cUsername.Contains("MELİH"))
            {

                if (not_gonderi.Text.Trim() == "") { MessageBox.Show("Lütfen notunuzu giriniz"); return; }

                if (gKonu.Text.Trim() == "" && kayit_no.Text.Trim() == "") { MessageBox.Show("Lütfen Değiştirmek İstediğiniz konuyu Seçin"); return; }

                if (secili_siparis.Text.Trim() == "") { MessageBox.Show("Lütfen Değiştirmek İstediğiniz Siparişi Seçin"); return; }

                else if (secili_siparis.Text.Trim() == "") { MessageBox.Show("Lütfen Siparişi Seçin"); return; }
                else
                {
                    //string tum_mailler = String.Join(",", mail_dizi.ToArray());


                    mail_dizi.Clear();
                    string tum_mailler = "";

                    planlamaci_ver();
                    mailler(txt_gonmails.Text.Trim());
                    tum_mailler = String.Join(",", mail_dizi.ToArray());

                    string Subject = secili_siparis.Text + " / " + secili_modeladi.Text + " CP Gönderi Hakkında ";

                    con = new SqlDbConnect();

                    con.SqlQuery("INSERT INTO durumtakip_ms (orderno,uname,umail,mesaj,tarih,udepartman,bilgi) VALUES (@orderno,@uname,@umail,@mesaj,@tarih,@udepartman,@bilgi)");
                    con.Cmd.Parameters.AddWithValue("@orderno", secili_siparis.Text);
                    con.Cmd.Parameters.AddWithValue("@uname", cInfo.cUsername);
                    con.Cmd.Parameters.AddWithValue("@umail", cInfo.cUsermail);
                    con.Cmd.Parameters.AddWithValue("@mesaj", "CP den > Konu: " + gKonu.Text.Trim() + ", " + not_gonderi.Text.Trim());
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


                    not_gonderi.Text = "";

                    con.QueryNonEx();
                    Heryerden.Gun1 = 1;






                    if (not_gonderi.Text.Trim() != "") { 
                    con.SqlQuery("UPDATE sipok SET notlar=@notlar where sirano='" + kayit_no.Text.Trim() + "'");
                     
                    con.Cmd.Parameters.AddWithValue("@notlar", not_gonderi.Text.Trim() +"-->"+cInfo.cUsername+"-->"+DateTime.Now.ToString());
                     
                    con.QueryNonEx();
                    }



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


                    kutu_temizle();
                    liste_ver();

                }

            }
            else { MessageBox.Show("Lütfen siparişin müşteri temsilcisine başvurun "); return; }
        }

        private void btn_ok_msj_Click(object sender, EventArgs e)
        {
            if (cInfo.cUsername.Trim() == secili_mt.Text.Trim() || gSorumlu.Text.Trim() == cInfo.cUsername.Trim() || "ADMIN".Contains(cInfo.cUsername) ||    cInfo.cUsername.Contains("MELİH"))
            {  

                if (not_ok.Text.Trim() == "" && sonuc_ok.Text.Trim() == "") { MessageBox.Show("Lütfen notunuzu giriniz veya sonucu olumlu yada olumsuz olarak seçiniz"); return; }


            Cursor.Current = Cursors.WaitCursor;


            if (gKonu.Text.Trim() == "" && kayit_no.Text.Trim() == "") { MessageBox.Show("Lütfen Değiştirmek İstediğiniz konuyu Seçin"); return; }

            if (secili_siparis.Text.Trim() == "") { MessageBox.Show("Lütfen Değiştirmek İstediğiniz Siparişi Seçin"); return; }

            else if (secili_siparis.Text.Trim() == "") { MessageBox.Show("Lütfen Siparişi Seçin"); return; }
            else
            {
                //string tum_mailler = String.Join(",", mail_dizi.ToArray());
                mail_dizi.Clear();
                string tum_mailler = "";

                planlamaci_ver();
                mailler(txt_gonmails.Text.Trim());
                tum_mailler = String.Join(",", mail_dizi.ToArray());

                string Subject = secili_siparis.Text + " / " + secili_modeladi.Text + " CP konu:" +gKonu+ " Renk(" + gRenk.Text.Trim() + "),  OK  Hakkında. Sonuç: " + sonuc_ok.Text.Trim();
                con = new SqlDbConnect();

                con.SqlQuery("INSERT INTO durumtakip_ms (orderno,uname,umail,mesaj,tarih,udepartman,bilgi) VALUES (@orderno,@uname,@umail,@mesaj,@tarih,@udepartman,@bilgi)");
                con.Cmd.Parameters.AddWithValue("@orderno", secili_siparis.Text);
                con.Cmd.Parameters.AddWithValue("@uname", cInfo.cUsername);
                con.Cmd.Parameters.AddWithValue("@umail", cInfo.cUsermail);
                con.Cmd.Parameters.AddWithValue("@mesaj", "CP den > Konu: " + gKonu.Text.Trim() + " Renk("+gRenk.Text.Trim()+"), " + not_gonderi.Text.Trim());
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


                not_gonderi.Text = "";

                con.QueryNonEx();
                Heryerden.Gun1 = 1;

                if (not_ok.Text.Trim() != "")
                {
                    con.SqlQuery("UPDATE sipok SET notlar=@notlar where sirano='" + kayit_no.Text.Trim() + "'");

                    con.Cmd.Parameters.AddWithValue("@notlar", not_ok.Text.Trim() + "-->" + cInfo.cUsername + "-->" + DateTime.Now.ToString());

                    con.QueryNonEx();
                }


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

                     
                }


                kutu_temizle();
                liste_ver();

            }



            }
            else { MessageBox.Show("Lütfen siparişin müşteri temsilcisine başvurun "); return; }


        }

        private void btn_temizle_Click(object sender, EventArgs e)
        {
            kutu_temizle();
        }

        private void btn_hepsini_sil_Click(object sender, EventArgs e)
        {
               

            if (secili_siparis.Text.Trim() != "")
            {

                DialogResult cikis = new DialogResult();
                cikis = MessageBox.Show(secili_siparis.Text.Trim() + " nolu Siparişin tüm cp aşamalırını silmek için devam etmek istiyormusunuz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (cikis == DialogResult.Yes)
                {
                    //Application.Exit();
                    Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        con = new SqlDbConnect();
                        con.SqlQuery("delete from  sipok Where siparisno='" + secili_siparis.Text.Trim() + "'");
                        con.QueryNonEx();

                        kutu_temizle();
                        liste_ver();
                        MessageBox.Show("Silme işlemi tamamlandı !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        con.Close();
                    }
                    catch { LogMessageToFile("durum takipte sip hep sil sorun var->" + secili_siparis.Text + "" + cInfo.cUsername); }

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

        private void gridView3_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {

        }

        private void pbtn_asamalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (secili_siparis.Text.Trim() != "" && secili_mt.Text.Trim() != "")
            {
                

                Cursor.Current = Cursors.WaitCursor;
                durumtakip_cp_musteri drcpm = new durumtakip_cp_musteri();
                drcpm.MdiParent = this.ParentForm;
                drcpm.secili_musteri.Text = musterino;

                drcpm.Show();
                Cursor.Current = Cursors.Default;
            }
            else
            {
                MessageBox.Show("Lütfen bir sipariş seçiniz");

            }


        }

        private void btn_sil_Click_1(object sender, EventArgs e)
        {

            if (kayit_no.Text.Trim() != "")
            {//update

                try
                {

                    con = new SqlDbConnect();
                    // oktipi,Renk,plgonderitarihi,OKtar2,pltarihi,OKTar,notlar,sorumlu,teminsuresi,ok,ulineno,sirano


                    con.SqlQuery("delete from  sipok Where sirano='" + kayit_no.Text.Trim() + "'");
                    con.QueryNonEx();




                    con.SqlQuery("INSERT INTO sipoklog (siparisno,oktipi,Renk,plgonderitarihi,pltarihi,notlar,sorumlu,teminsuresi,uyari,tarih,username,ozet) VALUES (@siparisno,@oktipi,@Renk,@plgonderitarihi,@pltarihi,@notlar,@sorumlu,@teminsuresi,@uyari,@tarih,@username,@ozet)");
                    con.Cmd.Parameters.AddWithValue("@siparisno", secili_siparis.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@oktipi", gKonu.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@Renk", gRenk.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@plgonderitarihi", gPlangontar.EditValue.ToString());
                    con.Cmd.Parameters.AddWithValue("@pltarihi", gPlanOktar.EditValue.ToString());
                    con.Cmd.Parameters.AddWithValue("@notlar", gnot.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@sorumlu", gSorumlu.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@teminsuresi", gTemin.Text.Trim());
                    con.Cmd.Parameters.AddWithValue("@uyari", "E");
                    con.Cmd.Parameters.AddWithValue("@tarih", DateTime.Now);
                    con.Cmd.Parameters.AddWithValue("@username", cInfo.cUsername);
                    con.Cmd.Parameters.AddWithValue("@ozet", "Silme");
                    con.QueryNonEx();

                    con.Close();



                    kutu_temizle();
                    liste_ver();
                    MessageBox.Show("Silme işlemi tamamlandı !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void grid_cp_Click(object sender, EventArgs e)
        {

        }

        

        private void gridView3_DoubleClick(object sender, EventArgs e)
        {

            /*table.Columns.Add("Konu");
            table.Columns.Add("Renk");
            
            table.Columns.Add("Plan.Gönderi Tar.");
            table.Columns.Add("Plan.Ok Tar.");
            table.Columns.Add("Gerçek.Gön.Tar");
            table.Columns.Add("Gerçek.Ok.Tar");
            table.Columns.Add("Notlar");
            table.Columns.Add("Sorumlu");
            table.Columns.Add("Temin");
            table.Columns.Add("Sonuç");
            table.Columns.Add("Gönderim Gecikme");
            table.Columns.Add("OK Gecikme");

            table.Columns.Add("Kayıt No");
            table.Columns.Add("Uyarı");*/
            if (gridView3.GetFocusedRowCellValue("Kayıt No") != null)
            {
                kayit_no.Text = gridView3.GetFocusedRowCellValue("Kayıt No").ToString().Trim();



                if (gridView3.GetFocusedRowCellValue("Gerçek.Gön.Tar") != "")
                {
                    gPlangontar.Enabled = false;

                }
                else { gPlangontar.Enabled = true; }

                if (gridView3.GetFocusedRowCellValue("Gerçek.Ok.Tar") != "" )
                {
                    gPlanOktar.Enabled = false;

                }
                else { gPlanOktar.Enabled = true; }


                if (gridView3.GetFocusedRowCellValue("Konu") != null)
                {
                    gKonu.Text = gridView3.GetFocusedRowCellValue("Konu").ToString().Trim();
                    eKonu.Text = gridView3.GetFocusedRowCellValue("Konu").ToString().Trim();

                }
                if (gridView3.GetFocusedRowCellValue("Renk") != null)
                {
                    gRenk.Text = gridView3.GetFocusedRowCellValue("Renk").ToString().Trim();
                    eRenk.Text = gridView3.GetFocusedRowCellValue("Renk").ToString().Trim();

                }
                if (gridView3.GetFocusedRowCellValue("Plan.Gönderi Tar.") != null)
                {
                    gPlangontar.Text = gridView3.GetFocusedRowCellValue("Plan.Gönderi Tar.").ToString().Trim();
                    ePlangontarih.Text = gridView3.GetFocusedRowCellValue("Plan.Gönderi Tar.").ToString().Trim();

                }

                if (gridView3.GetFocusedRowCellValue("Plan.Ok Tar.") != null)
                {

                    gPlanOktar.Text = gridView3.GetFocusedRowCellValue("Plan.Ok Tar.").ToString().Trim();
                    ePlanOktarih.Text = gridView3.GetFocusedRowCellValue("Plan.Ok Tar.").ToString().Trim();
                }
                if (gridView3.GetFocusedRowCellValue("Sorumlu") != null)
                {
                    gSorumlu.Text = gridView3.GetFocusedRowCellValue("Sorumlu").ToString().Trim();
                    eSorumlu.Text = gridView3.GetFocusedRowCellValue("Sorumlu").ToString().Trim();

                }

                if (gridView3.GetFocusedRowCellValue("Temin") != null)
                {
                    gTemin.Text = gridView3.GetFocusedRowCellValue("Temin").ToString().Trim();
                    eTemin.Text = gridView3.GetFocusedRowCellValue("Temin").ToString().Trim();

                }
                if (gridView3.GetFocusedRowCellValue("Notlar") != null)
                {
                    gnot.Text = gridView3.GetFocusedRowCellValue("Notlar").ToString().Trim();
                    enot.Text = gridView3.GetFocusedRowCellValue("Notlar").ToString().Trim();

                }

                if (gridView3.GetFocusedRowCellValue("Uyarı") != null)
                {
                    if (gridView3.GetFocusedRowCellValue("Uyarı").ToString().Trim() == "E" || gridView3.GetFocusedRowCellValue("Uyarı").ToString().Trim() == "")
                    {

                        gcheckuyari.Checked = true;
                        echeckuyari.Checked = true;

                    }
                    else
                    {
                        gcheckuyari.Checked = false;
                        echeckuyari.Checked = false;
                    }

                }



            }
        }


           




    }
}
