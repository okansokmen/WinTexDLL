Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Public Class frmTicimaxParameters

    Public Sub init()
        Me.ShowDialog()
    End Sub

    Private Sub frmTicimaxParameters_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim oSQL As New SQLServerClass

            oSQL.OpenConn()

            If oConnection.cOwner = "gecit" Then
                TextBox1.Text = oSQL.GetSysPar("ticimaxapiurunurl", "https://benimpabucum.com/servis/UrunServis.svc")
                TextBox2.Text = oSQL.GetSysPar("ticimaxapisiparisurl", "https://benimpabucum.com/servis/SiparisServis.svc")
                TextBox3.Text = oSQL.GetSysPar("ticimaxapiuyeurl", "https://benimpabucum.com/servis/UyeServis.svc")
                TextBox4.Text = oSQL.GetSysPar("ticimaxapicustomurl", "https://benimpabucum.com/servis/CustomServis.svc")
                TextBox5.Text = oSQL.GetSysPar("ticimaxadminurl", "https://www.benimpabucum.com/admin")
                TextBox6.Text = oSQL.GetSysPar("ticimaxadminusername", "edeors1@gmail.com")
                TextBox7.Text = oSQL.GetSysPar("ticimaxadminpassword", "14871487")
                TextBox8.Text = oSQL.GetSysPar("ticimaxfirma", "TICIMAX")
                TextBox9.Text = oSQL.GetSysPar("ticimaxyetkikodu", "H0ET3D5EU3VIFZM42BZUFQ3E6GFCRZ")

                TextBox10.Text = oSQL.GetSysPar("ticimaxanakategori", "kadin-001")
                NumericUpDown1.Value = CDec(oSQL.GetSysPar("ticimaxanakategoriid", "22"))
                TextBox11.Text = oSQL.GetSysPar("ticimaxmarka", "Benim Papucum")
                NumericUpDown2.Value = CDec(oSQL.GetSysPar("ticimaxmarkaid", "6"))
                NumericUpDown3.Value = CDec(oSQL.GetSysPar("ticimaxtedarikciid", "7"))
                TextBox12.Text = oSQL.GetSysPar("ticimaxsatisbirimi", "Adet")
                NumericUpDown4.Value = CDec(oSQL.GetSysPar("ticimaxdesi", "3"))
                NumericUpDown5.Value = CDec(oSQL.GetSysPar("ticimaxkargoucreti", "0"))
                NumericUpDown6.Value = CDec(oSQL.GetSysPar("ticimaxkdvorani", "0"))

                TextBox13.Text = oSQL.GetSysPar("ticimaxsiparistipi", "Normal")
                TextBox14.Text = oSQL.GetSysPar("ticimaxkargofirmasi", "MNG")
                TextBox15.Text = oSQL.GetSysPar("ticimaxaracikargo", "MARES")
                TextBox16.Text = oSQL.GetSysPar("ticimaxodemetipi", "KAPIDA KREDI KARTI")
                TextBox17.Text = oSQL.GetSysPar("ticimaxteslimsekli", "ADRESE TESLIM")
                TextBox18.Text = oSQL.GetSysPar("ticimaxkomisyoncufirma", "BENIM PABUCUM")
                TextBox19.Text = oSQL.GetSysPar("ticimaxusername", "TICIMAX")

            Else

                TextBox1.Text = oSQL.GetSysPar("ticimaxapiurunurl", "https://domain.com/servis/UrunServis.svc")
                TextBox2.Text = oSQL.GetSysPar("ticimaxapisiparisurl", "https://domain.com/servis/SiparisServis.svc")
                TextBox3.Text = oSQL.GetSysPar("ticimaxapiuyeurl", "https://domain.com/servis/UyeServis.svc")
                TextBox4.Text = oSQL.GetSysPar("ticimaxapicustomurl", "https://domain.com/servis/CustomServis.svc")
                TextBox5.Text = oSQL.GetSysPar("ticimaxadminurl", "https://www.domain.com/admin")
                TextBox6.Text = oSQL.GetSysPar("ticimaxadminusername")
                TextBox7.Text = oSQL.GetSysPar("ticimaxadminpassword")
                TextBox8.Text = oSQL.GetSysPar("ticimaxfirma", "TICIMAX")
                TextBox9.Text = oSQL.GetSysPar("ticimaxyetkikodu")

                TextBox10.Text = oSQL.GetSysPar("ticimaxanakategori")
                NumericUpDown1.Value = CDec(oSQL.GetSysPar("ticimaxanakategoriid", "1"))
                TextBox11.Text = oSQL.GetSysPar("ticimaxmarka")
                NumericUpDown2.Value = CDec(oSQL.GetSysPar("ticimaxmarkaid", "1"))
                NumericUpDown3.Value = CDec(oSQL.GetSysPar("ticimaxtedarikciid", "1"))
                TextBox12.Text = oSQL.GetSysPar("ticimaxsatisbirimi")
                NumericUpDown4.Value = CDec(oSQL.GetSysPar("ticimaxdesi", "1"))
                NumericUpDown5.Value = CDec(oSQL.GetSysPar("ticimaxkargoucreti", "1"))
                NumericUpDown6.Value = CDec(oSQL.GetSysPar("ticimaxkdvorani", "1"))

                TextBox13.Text = oSQL.GetSysPar("ticimaxsiparistipi")
                TextBox14.Text = oSQL.GetSysPar("ticimaxkargofirmasi")
                TextBox15.Text = oSQL.GetSysPar("ticimaxaracikargo")
                TextBox16.Text = oSQL.GetSysPar("ticimaxodemetipi")
                TextBox17.Text = oSQL.GetSysPar("ticimaxteslimsekli")
                TextBox18.Text = oSQL.GetSysPar("ticimaxkomisyoncufirma")
                TextBox19.Text = oSQL.GetSysPar("ticimaxusername")

            End If

            oSQL.CloseConn()

            TabFormControl1.SelectedPage = TabFormControl1.Pages(0)

        Catch ex As Exception
            ErrDisp("frmTicimaxParameters_Load", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' kaydet
        Try
            Dim oSQL As New SQLServerClass

            oSQL.OpenConn()

            oSQL.SetSysPar("ticimaxapiurunurl", SQLWriteString(TextBox1.Text, 200))
            oSQL.SetSysPar("ticimaxapisiparisurl", SQLWriteString(TextBox2.Text, 200))
            oSQL.SetSysPar("ticimaxapiuyeurl", SQLWriteString(TextBox3.Text, 200))
            oSQL.SetSysPar("ticimaxapicustomurl", SQLWriteString(TextBox4.Text, 200))
            oSQL.SetSysPar("ticimaxadminurl", SQLWriteString(TextBox5.Text, 200))
            oSQL.SetSysPar("ticimaxadminusername", SQLWriteString(TextBox6.Text, 200))
            oSQL.SetSysPar("ticimaxadminpassword", SQLWriteString(TextBox7.Text, 200))
            oSQL.SetSysPar("ticimaxfirma", SQLWriteString(TextBox8.Text, 200))
            oSQL.SetSysPar("ticimaxyetkikodu", SQLWriteString(TextBox9.Text, 200))

            oSQL.SetSysPar("ticimaxanakategori", SQLWriteString(TextBox10.Text, 200))
            oSQL.SetSysPar("ticimaxanakategoriid", NumericUpDown1.Value.ToString)
            oSQL.SetSysPar("ticimaxmarka", SQLWriteString(TextBox11.Text, 200))
            oSQL.SetSysPar("ticimaxmarkaid", NumericUpDown2.Value.ToString)
            oSQL.SetSysPar("ticimaxtedarikciid", NumericUpDown3.Value.ToString)
            oSQL.SetSysPar("ticimaxsatisbirimi", SQLWriteString(TextBox12.Text, 200))
            oSQL.SetSysPar("ticimaxdesi", NumericUpDown4.Value.ToString)
            oSQL.SetSysPar("ticimaxkargoucreti", NumericUpDown5.Value.ToString)
            oSQL.SetSysPar("ticimaxkdvorani", NumericUpDown6.Value.ToString)

            oSQL.SetSysPar("ticimaxsiparistipi", SQLWriteString(TextBox13.Text, 200))
            oSQL.SetSysPar("ticimaxkargofirmasi", SQLWriteString(TextBox14.Text, 200))
            oSQL.SetSysPar("ticimaxaracikargo", SQLWriteString(TextBox15.Text, 200))
            oSQL.SetSysPar("ticimaxodemetipi", SQLWriteString(TextBox16.Text, 200))
            oSQL.SetSysPar("ticimaxteslimsekli", SQLWriteString(TextBox17.Text, 200))
            oSQL.SetSysPar("ticimaxkomisyoncufirma", SQLWriteString(TextBox18.Text, 200))
            oSQL.SetSysPar("ticimaxusername", SQLWriteString(TextBox19.Text, 200))

            oConnection.cTiciMaxApiUrunUrl = TextBox1.Text.Trim
            oConnection.cTiciMaxApiSiparisUrl = TextBox2.Text.Trim
            oConnection.cTiciMaxApiUyeUrl = TextBox3.Text.Trim
            oConnection.cTiciMaxApiCustomUrl = TextBox4.Text.Trim
            oConnection.cTiciMaxAdminURL = TextBox5.Text.Trim
            oConnection.cTiciMaxAdminUserName = TextBox6.Text.Trim
            oConnection.cTiciMaxAdminPassword = TextBox7.Text.Trim
            oConnection.cTiciMaxFirma = TextBox8.Text.Trim
            oConnection.cTiciMaxYetkiKodu = TextBox9.Text.Trim

            oConnection.cTiciMaxAnaKategori = TextBox10.Text.Trim
            oConnection.cTiciMaxAnaKategoriID = NumericUpDown1.Value.ToString
            oConnection.cTiciMaxMarka = TextBox11.Text.Trim
            oConnection.cTiciMaxMarkaID = NumericUpDown2.Value.ToString
            oConnection.cTiciMaxTedarikciID = NumericUpDown3.Value.ToString
            oConnection.cTiciMaxSatisBirimi = TextBox12.Text.Trim
            oConnection.cTiciMaxDesi = NumericUpDown4.Value.ToString
            oConnection.cTiciMaxKargoUcreti = NumericUpDown5.Value.ToString
            oConnection.cTiciMaxKdvOrani = NumericUpDown6.Value.ToString

            oConnection.cTiciMaxSiparisTipi = TextBox13.Text.Trim
            oConnection.cTiciMaxKargoFirmasi = TextBox14.Text.Trim
            oConnection.cTiciMaxAraciKargo = TextBox15.Text.Trim
            oConnection.cTiciMaxOdemeTipi = TextBox16.Text.Trim
            oConnection.cTiciMaxTeslimSekli = TextBox17.Text.Trim
            oConnection.cTiciMaxKomisyoncuFirma = TextBox18.Text.Trim
            oConnection.cTiciMaxUserName = TextBox19.Text.Trim

            oSQL.CloseConn()

            Me.Close()

        Catch ex As Exception
            ErrDisp("Button1_Click", Me.Name,,, ex)
        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' close
        Me.Close()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        On Error Resume Next
        Process.Start("iexplore.exe", TextBox1.Text)
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        On Error Resume Next
        Process.Start("iexplore.exe", TextBox2.Text)
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        On Error Resume Next
        Process.Start("iexplore.exe", TextBox3.Text)
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        On Error Resume Next
        Process.Start("iexplore.exe", TextBox4.Text)
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        On Error Resume Next
        Process.Start("iexplore.exe", TextBox5.Text)
    End Sub
End Class