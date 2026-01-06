Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Public Class frmCrsSoft

    Dim nMode As Integer = 0
    Dim cDemoUserName As String = ""
    Dim cDemoPassword As String = ""
    Dim cRealUserName As String = ""
    Dim cRealPassword As String = ""

    Public Sub init(Optional nCase As Integer = 1)
        ' nCase = 1 , CRS
        ' nCase = 2 , TürkKEP
        ' nCase = 3 , eDokSis
        ' nCase = 4 , park 
        nMode = nCase
        Me.ShowDialog()
    End Sub

    Private Sub frmCrsSoft_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try
            Dim oSQL As New SQLServerClass
            Dim cSFE As String = ""
            Dim cUFE As String = ""
            Dim cServis As String = ""

            LoadCombos()

            oSQL.OpenConn()

            Select Case nMode
                Case 1
                    Select Case oConnection.cOwner
                        Case "jeanci konfeksiyon"
                            cDemoUserName = "efatura1"
                            cDemoPassword = "Efatura123!"
                            cRealUserName = "baris"
                            cRealPassword = "Jnc/21296"

                        Case "mothouse"
                            cDemoUserName = "CrsDemo85"
                            cDemoPassword = "11223385"

                        Case "bolero"
                            cDemoUserName = "bolerotestws"
                            cDemoPassword = "Gg123123"
                            cRealUserName = "162973523"
                            cRealPassword = "kzdy43b4"
                    End Select

                    Me.Text = "Cross Rational Solutions eIrsaliye Entegrasyon Parametreleri"
                    TextBox1.Text = oSQL.GetSysPar("UrlCrsEirsaliyeService", "https://connect-test.crssoft.com/Services/DespatchIntegration")
                    TextBox2.Text = oSQL.GetSysPar("CrsUsername", cDemoUserName)
                    TextBox3.Text = oSQL.GetSysPar("CrsPassword", cDemoPassword)
                    TextBox4.Text = oSQL.GetSysPar("CrsPortal", "https://edunya-test.crssoft.com/")
                Case 2
                    Me.Text = "TürkKEP eIrsaliye Entegrasyon Parametreleri"
                    TextBox1.Text = oSQL.GetSysPar("TurkKepService", "http://efinttestws.turkkep.com.tr/EFaturaEntegrasyon2.asmx")
                    TextBox2.Text = oSQL.GetSysPar("TurkKepUsername", cDemoUserName)
                    TextBox3.Text = oSQL.GetSysPar("TurkKepPassword", cDemoPassword)
                    TextBox4.Text = oSQL.GetSysPar("TurkKepPortal", "https://eftestprt.turkkep.com.tr/")
                Case 3
                    Me.Text = "eDokSis eIrsaliye Entegrasyon Parametreleri"
                    TextBox1.Text = oSQL.GetSysPar("eDokSisService", "https://efaturatest.edoksis.net/IrsaliyeWebService.asmx")
                    TextBox2.Text = oSQL.GetSysPar("eDokSisUsername", cDemoUserName)
                    TextBox3.Text = oSQL.GetSysPar("eDokSisPassword", cDemoPassword)
                    TextBox4.Text = oSQL.GetSysPar("eDokSisPortal", "https://efaturatest.edoksis.net")
                Case 4
                    If oConnection.cOwner = "jeansco" Or oConnection.cOwner = "jeanci konfeksiyon" Then
                        cDemoUserName = "park-entegrasyon"
                        cDemoPassword = "cmc2017*!Park"
                    End If

                    Me.Text = "Park eIrsaliye Entegrasyon Parametreleri"
                    TextBox1.Text = oSQL.GetSysPar("ParkService", "https://wstest.parkentegrasyon.com.tr/EFaturaIntegration.asmx")
                    TextBox2.Text = oSQL.GetSysPar("ParkUsername", cDemoUserName)
                    TextBox3.Text = oSQL.GetSysPar("ParkPassword", cDemoPassword)
                    TextBox4.Text = oSQL.GetSysPar("ParkPortal", "https://wstest.parkentegrasyon.com.tr")
                    TextBox5.Text = oSQL.GetSysPar("ParkSeri", "JNC")
            End Select

            cSFE = oSQL.GetSysPar("irsservicestok", "Butun satirlar")
            ComboBox1.SelectedText = cSFE

            cUFE = oSQL.GetSysPar("irsserviceuretim", "Butun satirlar")
            ComboBox2.SelectedText = cUFE

            cServis = oSQL.GetSysPar("irsservicesaglayici", "crs")
            ComboBox3.SelectedText = cServis

            If oSQL.GetSysPar("irsdraft", "0") = "1" Then
                CheckEdit1.Checked = True
            Else
                CheckEdit1.Checked = False
            End If

            oSQL.CloseConn()
            oSQL = Nothing

        Catch ex As Exception
            ErrDisp("frmCrsSoft_Load", Me.Name,,, ex)
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

        ComboBox3.Items.Clear()
        ComboBox3.Items.Add("crs")
        ComboBox3.Items.Add("turkkep")
        ComboBox3.Items.Add("edoksis")
        ComboBox3.Items.Add("park")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Process.Start("iexplore.exe", TextBox1.Text)
        Catch ex As Exception
            ErrDisp("Button1_Click", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' ok
        Try
            Dim oSQL As New SQLServerClass

            oSQL.OpenConn()

            Select Case nMode
                Case 1
                    oSQL.SetSysPar("UrlCrsEirsaliyeService", TextBox1.Text)
                    oSQL.SetSysPar("CrsUsername", TextBox2.Text)
                    oSQL.SetSysPar("CrsPassword", TextBox3.Text)
                    oSQL.SetSysPar("CrsPortal", TextBox4.Text)
                Case 2
                    oSQL.SetSysPar("TurkKepService", TextBox1.Text)
                    oSQL.SetSysPar("TurkKepUsername", TextBox2.Text)
                    oSQL.SetSysPar("TurkKepPassword", TextBox3.Text)
                    oSQL.SetSysPar("TurkKepPortal", TextBox4.Text)
                Case 3
                    oSQL.SetSysPar("eDokSisService", TextBox1.Text)
                    oSQL.SetSysPar("eDokSisUsername", TextBox2.Text)
                    oSQL.SetSysPar("eDokSisPassword", TextBox3.Text)
                    oSQL.SetSysPar("eDokSisPortal", TextBox4.Text)
                Case 4
                    oSQL.SetSysPar("ParkService", TextBox1.Text)
                    oSQL.SetSysPar("ParkUsername", TextBox2.Text)
                    oSQL.SetSysPar("ParkPassword", TextBox3.Text)
                    oSQL.SetSysPar("ParkPortal", TextBox4.Text)
                    oSQL.SetSysPar("ParkSeri", TextBox5.Text)
            End Select

            oSQL.SetSysPar("irsservicestok", ComboBox1.Text.Trim)
            oSQL.SetSysPar("irsserviceuretim", ComboBox2.Text.Trim)
            oSQL.SetSysPar("irsservicesaglayici", ComboBox3.Text.Trim)

            If CheckEdit1.Checked Then
                oSQL.SetSysPar("irsdraft", "1")
            Else
                oSQL.SetSysPar("irsdraft", "0")
            End If

            oSQL.CloseConn()
            oSQL = Nothing

            Me.Close()

        Catch ex As Exception
            ErrDisp("SimpleButton3_Click", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' iptal
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' test
        Select Case nMode
            Case 1
                TextBox1.Text = "https://connect-test.crssoft.com/Services/DespatchIntegration"
                TextBox4.Text = "https://edunya-test.crssoft.com"
            Case 2
                TextBox1.Text = "http://efinttestws.turkkep.com.tr/EFaturaEntegrasyon2.asmx"
                TextBox4.Text = "https://eftestprt.turkkep.com.tr"
            Case 3
                TextBox1.Text = "https://efaturatest.edoksis.net/IrsaliyeWebService.asmx"
                TextBox4.Text = "https://efaturatest.edoksis.net"
            Case 4
                TextBox1.Text = "https://wstest.parkentegrasyon.com.tr/EFaturaIntegration.asmx"
                TextBox4.Text = "https://wstest.parkentegrasyon.com.tr"
        End Select

        TextBox2.Text = cDemoUserName
        TextBox3.Text = cDemoPassword
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ' canlı
        Select Case nMode
            Case 1
                TextBox1.Text = "https://connect.crssoft.com/Services/DespatchIntegration"
                TextBox4.Text = "https://edunya.crssoft.com"
            Case 2
                TextBox1.Text = "https://efintws.turkkep.com.tr/EFaturaEntegrasyon2.asmx"
                TextBox4.Text = "https://efportal.turkkep.com.tr"
            Case 3
                TextBox1.Text = "https://efatura.edoksis.net/IrsaliyeWebService.asmx"
                TextBox4.Text = "https://efatura.edoksis.net"
            Case 4
                TextBox1.Text = "https://wstest.parkentegrasyon.com.tr/EFaturaIntegration.asmx"
                TextBox4.Text = "https://wstest.parkentegrasyon.com.tr"
        End Select

        TextBox2.Text = cRealUserName
        TextBox3.Text = cRealPassword

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            Process.Start("iexplore.exe", TextBox4.Text)

        Catch ex As Exception
            ErrDisp("Button6_Click", Me.Name,,, ex)
        End Try
    End Sub

End Class