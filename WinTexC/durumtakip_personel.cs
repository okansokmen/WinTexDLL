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
    public partial class durumtakip_personel : Form
    {
        SqlDbConnect con;
        List<veriler> NewOrder = new List<veriler>();
        public class veriler
        {
            public string username;
            public string personel;
            public string aciklama;
            public string departman;
            public string email;
            public string emailpass;
            public string mgrup;
            public string keywords;
             


        }

        public static veriler ss = new veriler();

        public durumtakip_personel()
        {
            InitializeComponent();
        }

        private void durumtakip_personel_Load(object sender, EventArgs e)
        {
            ilk();
            liste_ver();
        }

        void ilk()
        {

             
            con = new SqlDbConnect();
          
            taciklama.Properties.Items.Clear();
            
            con.LoginQuery("select DISTINCT(aciklama) from  personel with (NOLOCK) where ayrildi <> 'E'");
            while (con.dbr.Read())
            {
                if (con.dbr["aciklama"].ToString().Trim() != "") { 
                taciklama.Properties.Items.Add(con.dbr["aciklama"].ToString().Trim());
                }

            }
            con.dbr.Close();



            tdepartman.Properties.Items.Clear();

            con.LoginQuery("select DISTINCT(departman) from  personel with (NOLOCK) where ayrildi <> 'E'");
            while (con.dbr.Read())
            {
                if (con.dbr["departman"].ToString().Trim() != "")
                {
                    tdepartman.Properties.Items.Add(con.dbr["departman"].ToString().Trim());
                }

            }
            con.dbr.Close();


            ss = new veriler();


            con.Close();

    









        }
        DataTable table = new DataTable();
      

        private DataTable BuildDataTable()
        {
            table.Columns.Clear();
            table.Rows.Clear();



            /*public string username;
            public string personel;
            public string aciklama;
            public string departman;
            public string email;
            public string emailpass;
            public string mgrup;
            public string keywords;*/
            table.Columns.Add("username");
            table.Columns.Add("personel");
            table.Columns.Add("aciklama");
            table.Columns.Add("departman");
            table.Columns.Add("email");
            table.Columns.Add("emailpass");
            table.Columns.Add("mgrup");
            table.Columns.Add("keywords");
            
            foreach (var value in NewOrder)
            {table.Rows.Add(new Object[] { value.username, value.personel, value.aciklama, value.departman, value.email, value.emailpass, value.mgrup, value.keywords });}

            NewOrder.Clear();

            return table;
        }





        void liste_ver()
        {
            gridControl1.DataSource = null;
              try
                {

                    con = new SqlDbConnect();
                  /* con.SqlQuery("select oktipi,Renk,plgonderitarihi,OKtar2,pltarihi,OKTar,notlar,sorumlu,teminsuresi,ok,ulineno,sirano,uyari from sipok with (NOLOCK) where siparisno='" + secili_siparis.Text.Trim() + "' ");
                     SqlDataAdapter sqlDataAdap = new SqlDataAdapter(con.Cmd);
                     DataTable dtRecord = new DataTable();
                     sqlDataAdap.Fill(dtRecord);
                     grid_cp.DataSource = dtRecord;*/

                    con.LoginQuery("select* from personel with (NOLOCK)  where ayrildi <> 'E'");
                    while (con.dbr.Read())
                    {
                    ss = new veriler();
                    ss.aciklama = con.dbr["aciklama"].ToString().Trim();
                    ss.departman = con.dbr["departman"].ToString().Trim();
                    ss.email = con.dbr["email"].ToString().Trim();
                    ss.emailpass = con.dbr["emailpass"].ToString().Trim();
                    ss.mgrup = con.dbr["mgrup"].ToString().Trim();
                    ss.personel = con.dbr["personel"].ToString().Trim();
                    ss.username = con.dbr["username"].ToString().Trim();
                    ss.keywords = con.dbr["keywords"].ToString().Trim();
                    NewOrder.Add(ss);
                    }
                    con.dbr.Close();

                    con.Close();
                    gridControl1.DataSource = BuildDataTable();
                
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Bir sorun oluştu, lütfen yazılımcı ile irtibata geçiniz. " + Environment.NewLine + Ex.ToString(), "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


             

        }

        private void gridView3_Click(object sender, EventArgs e)
        {
            if (gridView3.GetFocusedRowCellValue("username") != null)
            {
                kayit_no.Text = gridView3.GetFocusedRowCellValue("username").ToString().Trim();
            }
            if (gridView3.GetFocusedRowCellValue("personel") != null)
            {
                tpersonel.Text = gridView3.GetFocusedRowCellValue("personel").ToString().Trim();
            }
             
            if (gridView3.GetFocusedRowCellValue("aciklama") != null)
            {
                taciklama.Text = gridView3.GetFocusedRowCellValue("aciklama").ToString().Trim();
            }

            if (gridView3.GetFocusedRowCellValue("departman") != null)
            {
                tdepartman.Text = gridView3.GetFocusedRowCellValue("departman").ToString().Trim();
            }

            if (gridView3.GetFocusedRowCellValue("email") != null)
            {
                temail.Text = gridView3.GetFocusedRowCellValue("email").ToString().Trim();
            }

            if (gridView3.GetFocusedRowCellValue("emailpass") != null)
            {
                temailpass.Text = gridView3.GetFocusedRowCellValue("emailpass").ToString().Trim();
            }

            if (gridView3.GetFocusedRowCellValue("mgrup") != null)
            {
                tmgrup.Text = gridView3.GetFocusedRowCellValue("mgrup").ToString().Trim();
            }

            if (gridView3.GetFocusedRowCellValue("keywords") != null)
            {
                tkeywords.Text = gridView3.GetFocusedRowCellValue("keywords").ToString().Trim();
            }










        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void btn_msjgonder_Click(object sender, EventArgs e)
        {
            try
            {

                con = new SqlDbConnect();


 con.SqlQuery("UPDATE personel SET  aciklama=@aciklama,departman=@departman,email=@email,emailpass=@emailpass,personel=@personel,mgrup=@mgrup,keywords=@keywords  where username='" + kayit_no.Text.Trim() + "'");

                con.Cmd.Parameters.AddWithValue("@aciklama", taciklama.Text.Trim());
                con.Cmd.Parameters.AddWithValue("@departman", tdepartman.Text.Trim());
                con.Cmd.Parameters.AddWithValue("@email", temail.EditValue.ToString());
                con.Cmd.Parameters.AddWithValue("@emailpass", temailpass.EditValue.ToString());
                con.Cmd.Parameters.AddWithValue("@personel", tpersonel.Text.Trim());
                con.Cmd.Parameters.AddWithValue("@mgrup", tmgrup.Text.Trim());
                con.Cmd.Parameters.AddWithValue("@keywords", tkeywords.Text.Trim());
                con.QueryNonEx();

                con.Close();

                kutu_temizle();
                liste_ver();
            }
            catch { }

        }
        void kutu_temizle()
        {

            kayit_no.Text = "";


            taciklama.Text = "";
            tdepartman.Text = "";
            temail.Text = "";
            temailpass.Text = "";
            tpersonel.Text = "";
            tmgrup.Text = "";
            tkeywords.Text = "";
            
        }


        /*
        Asc <=> Convert.ToInt32
Chr <=> Convert.ToChar
Len <=> String.Length
Mid <=> String.Substring

        string sString = "Hello";
byte [] abData = System.Text.Encoding.ASCII.GetBytes(sString);


         Public Function EnCypher(WStr As String)
'<EhHeader>
On Error GoTo EnCypher_Error
'</EhHeader>

Dim code As Integer
Dim EnStr As String
Dim i As Integer

EnStr = ""
code = 0
For i = 1 To Len(WStr)
    code = Asc(Mid(WStr, i, 1))
    EnStr = EnStr + Chr(2 * code + i)
Next
EnCypher = EnStr

'<EhFooter>
Exit Function

EnCypher_Error:
ErrDisp "Hata : WinTex8.utilcypher.Function.EnCypher "
Resume Next
'</EhFooter>
End Function

Public Function Decypher(WStr As String)
'<EhHeader>
On Error GoTo Decypher_Error
'</EhHeader>

Dim code As Integer
Dim DeStr As String
Dim i As Integer

DeStr = ""
code = 0
For i = 1 To Len(WStr)
    code = Asc(Mid(WStr, i, 1))
    DeStr = DeStr + Chr((code - i) / 2)
Next
Decypher = DeStr

'<EhFooter>
Exit Function

Decypher_Error:
ErrDisp "Hata : WinTex8.utilcypher.Function.Decypher "
Resume Next
'</EhFooter>
End Function

         
         
         */



    }
}
