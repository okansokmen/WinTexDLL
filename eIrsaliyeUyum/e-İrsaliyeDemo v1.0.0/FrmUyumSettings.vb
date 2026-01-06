Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Namespace eIrsaliyeUyum

    Public Class FrmUyumSettings

        Public Sub init()
            Me.ShowDialog()
        End Sub

        Private Sub FrmUyumSettings_Load(sender As Object, e As EventArgs) Handles Me.Load
            Try
                Dim oSQL As New SQLServerClass
                Dim cSFE As String = ""
                Dim cUFE As String = ""
                Dim cServis As String = ""

                LoadCombos()

                oSQL.OpenConn()

                Me.Text = "Uyumsoft eIrsaliye Entegrasyon Parametreleri"
                TextBox1.Text = oSQL.GetSysPar("UyumEirsaliyeServiceUrl")
                TextBox2.Text = oSQL.GetSysPar("UyumUsername")
                TextBox3.Text = oSQL.GetSysPar("UyumPassword")
                TextBox6.Text = oSQL.GetSysPar("UyumPortalUrl")
                TextBox5.Text = oSQL.GetSysPar("UyumPortalUsername")
                TextBox4.Text = oSQL.GetSysPar("UyumPortalPassword")
                TextBox7.Text = oSQL.GetSysPar("UyumSaticiFirma", "DAHILI")

                cSFE = oSQL.GetSysPar("Uyumirsservicestok", "Butun satirlar")
                ComboBox1.SelectedText = cSFE

                cUFE = oSQL.GetSysPar("Uyumirsserviceuretim", "Butun satirlar")
                ComboBox2.SelectedText = cUFE

                If oSQL.GetSysPar("Uyumirsdraft", "0") = "1" Then
                    CheckBox1.Checked = True
                Else
                    CheckBox1.Checked = False
                End If

                oSQL.CloseConn()
                oSQL = Nothing

            Catch ex As Exception
                ErrDisp("FrmUyumSettings_Load", Me.Name,,, ex)
            End Try
        End Sub

        Private Sub LoadCombos()

            ComboBox1.Items.Clear()
            ComboBox1.Items.Add("Butun satirlar")
            ComboBox1.Items.Add("Ana stok grubu + stok tipi gruplu")
            ComboBox1.Items.Add("Stok kodu gruplu")
            ComboBox1.Items.Add("Stok kodu + renk gruplu")

            ComboBox2.Items.Clear()
            ComboBox2.Items.Add("Butun satirlar")
            ComboBox2.Items.Add("Ana model tipi + model no gruplu")
            ComboBox2.Items.Add("Siparis gruplu")

        End Sub

        Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
            Try
                Process.Start("iexplore.exe", TextBox1.Text)
            Catch ex As Exception
                ErrDisp("Button1_Click", Me.Name,,, ex)
            End Try
        End Sub

        Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
            Try
                Process.Start("iexplore.exe", TextBox6.Text)
            Catch ex As Exception
                ErrDisp("Button6_Click", Me.Name,,, ex)
            End Try
        End Sub

        Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
            ' ok
            Try
                Dim oSQL As New SQLServerClass

                oSQL.OpenConn()

                oSQL.SetSysPar("UyumEirsaliyeServiceUrl", TextBox1.Text)
                oSQL.SetSysPar("UyumUsername", TextBox2.Text)
                oSQL.SetSysPar("UyumPassword", TextBox3.Text)

                oSQL.SetSysPar("UyumPortalUrl", TextBox6.Text)
                oSQL.SetSysPar("UyumPortalUsername", TextBox5.Text)
                oSQL.SetSysPar("UyumPortalPassword", TextBox4.Text)

                oSQL.SetSysPar("Uyumirsservicestok", ComboBox1.Text.Trim)
                oSQL.SetSysPar("Uyumirsserviceuretim", ComboBox2.Text.Trim)
                oSQL.SetSysPar("UyumSaticiFirma", TextBox7.Text.Trim)

                If CheckBox1.Checked Then
                    oSQL.SetSysPar("Uyumirsdraft", "1")
                Else
                    oSQL.SetSysPar("Uyumirsdraft", "0")
                End If

                oSQL.CloseConn()
                oSQL = Nothing

                Me.Close()

            Catch ex As Exception
                ErrDisp("Button6_Click", Me.Name,,, ex)
            End Try
        End Sub

        Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
            ' iptal
            Me.Close()
        End Sub

        Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
            ' test
            TextBox1.Text = "https://efatura-test.uyumsoft.com.tr/Services/DespatchIntegration"
            TextBox6.Text = "https://portal-test.uyumsoft.com.tr"
            TextBox2.Text = "Uyumsoft"
            TextBox3.Text = "Uyumsoft"
            TextBox4.Text = "Uyumsoft"
            TextBox5.Text = "Uyumsoft"
        End Sub

        Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
            ' canlı
            TextBox1.Text = "https://efatura.uyumsoft.com.tr/Services/DespatchIntegration"
            TextBox6.Text = "https://portal.uyumsoft.com.tr"
        End Sub

    End Class

End Namespace




