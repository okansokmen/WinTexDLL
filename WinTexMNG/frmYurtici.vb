Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Public Class frmYurtici

    ' UserLanguage hep TR olacak
    ' CleanResult true olacak
    ' TestMode eğer test ise true olacak 

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' çıkış
        On Error Resume Next
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' kaydet / çık
        Try
            Dim Connyage As SqlConnection = Nothing

            Connyage = OpenConn()

            SetSysParConnected("yurticiurl", SQLWriteString(TextBox1.Text, 200), Connyage)
            ' Alıcı Ödemeli Normal
            SetSysParConnected("yurticiaonusername", SQLWriteString(TextBox2.Text, 200), Connyage)
            SetSysParConnected("yurticiaonpassword", SQLWriteString(TextBox3.Text, 200), Connyage)
            ' Gönderici Ödemeli Normal
            SetSysParConnected("yurticigonusername", SQLWriteString(TextBox4.Text, 200), Connyage)
            SetSysParConnected("yurticigonpassword", SQLWriteString(TextBox5.Text, 200), Connyage)
            ' Alıcı Ödemeli Tahsilatlı
            SetSysParConnected("yurticiaotusername", SQLWriteString(TextBox6.Text, 200), Connyage)
            SetSysParConnected("yurticiaotpassword", SQLWriteString(TextBox7.Text, 200), Connyage)
            ' Gönderici Ödemeli Tahsilatlı
            SetSysParConnected("yurticigotusername", SQLWriteString(TextBox8.Text, 200), Connyage)
            SetSysParConnected("yurticigotpassword", SQLWriteString(TextBox9.Text, 200), Connyage)

            oConnection.cYurticiUrl = TextBox1.Text.Trim
            ' Alıcı Ödemeli Normal
            oConnection.cYurticiAONormal_UserName = TextBox2.Text.Trim
            oConnection.cYurticiAONormal_Password = TextBox3.Text.Trim
            ' Gönderici Ödemeli Normal
            oConnection.cYurticiGONormal_UserName = TextBox4.Text.Trim
            oConnection.cYurticiGONormal_Password = TextBox5.Text.Trim
            ' Alıcı Ödemeli Tahsilatlı
            oConnection.cYurticiAOTahsilatli_UserName = TextBox6.Text.Trim
            oConnection.cYurticiAOTahsilatli_Password = TextBox7.Text.Trim
            ' Gönderici Ödemeli Tahsilatlı
            oConnection.cYurticiGOTahsilatli_UserName = TextBox8.Text.Trim
            oConnection.cYurticiGOTahsilatli_Password = TextBox9.Text.Trim

            Connyage.Close()

            Me.Close()

        Catch ex As Exception
            ErrDisp("Button1_Click", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub frmYurtici_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try
            Dim Connyage As SqlConnection = Nothing

            Connyage = OpenConn()

            TextBox1.Text = GetSysParConnected("yurticiurl", Connyage, "http://webservices.yurticikargo.com:8080/KOPSWebServices/ShippingOrderDispatcherServices?wsdl")

            If oConnection.cOwner = "gecit" Then
                ' Alıcı Ödemeli Normal
                TextBox2.Text = GetSysParConnected("yurticiaonusername", Connyage, "1160N977588062A")
                TextBox3.Text = GetSysParConnected("yurticiaonpassword", Connyage, "YXYDe59xRA2a5Xfg")
                ' Gönderici Ödemeli Normal
                TextBox4.Text = GetSysParConnected("yurticigonusername", Connyage, "1160N977588062G")
                TextBox5.Text = GetSysParConnected("yurticigonpassword", Connyage, "pk9w6VJxn9r8V7Nv")
                ' Alıcı Ödemeli Tahsilatlı
                TextBox6.Text = GetSysParConnected("yurticiaotusername", Connyage, "1160T977588062A")
                TextBox7.Text = GetSysParConnected("yurticiaotpassword", Connyage, "rChY45wvn4091700")
                ' Gönderici Ödemeli Tahsilatlı
                TextBox8.Text = GetSysParConnected("yurticigotusername", Connyage, "1160T977588062G")
                TextBox9.Text = GetSysParConnected("yurticigotpassword", Connyage, "e44VCw905Gv3yH07")
            Else
                ' Alıcı Ödemeli Normal
                TextBox2.Text = GetSysParConnected("yurticiaonusername", Connyage)
                TextBox3.Text = GetSysParConnected("yurticiaonpassword", Connyage)
                ' Gönderici Ödemeli Normal
                TextBox4.Text = GetSysParConnected("yurticigonusername", Connyage)
                TextBox5.Text = GetSysParConnected("yurticigonpassword", Connyage)
                ' Alıcı Ödemeli Tahsilatlı
                TextBox6.Text = GetSysParConnected("yurticiaotusername", Connyage)
                TextBox7.Text = GetSysParConnected("yurticiaotpassword", Connyage)
                ' Gönderici Ödemeli Tahsilatlı
                TextBox8.Text = GetSysParConnected("yurticigotusername", Connyage)
                TextBox9.Text = GetSysParConnected("yurticigotpassword", Connyage)
            End If

            Connyage.Close()

        Catch ex As Exception
            ErrDisp("frmYurtici_Load", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        On Error Resume Next
        Process.Start("iexplore.exe", TextBox1.Text)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' test setup
        ' UserLanguage hep TR olacak
        ' https://testws.yurticikargo.com
        TextBox1.Text = "http://testwebservices.yurticikargo.com:9090/KOPSWebServices/ShippingOrderDispatcherServices?wsdl"
        ' Alıcı Ödemeli Normal
        TextBox2.Text = "YKTEST"
        TextBox3.Text = "YK"
        ' Gönderici Ödemeli Normal
        TextBox4.Text = "YKTEST"
        TextBox5.Text = "YK"
        ' Alıcı Ödemeli Tahsilatlı
        TextBox6.Text = "YKTEST"
        TextBox7.Text = "YK"
        ' Gönderici Ödemeli Tahsilatlı
        TextBox8.Text = "YKTEST"
        TextBox9.Text = "YK"
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ' live setup
        ' UserLanguage hep TR olacak
        ' https://ws.yurticikargo.com
        TextBox1.Text = "http://webservices.yurticikargo.com:8080/KOPSWebServices/ShippingOrderDispatcherServices?wsdl"

        If oConnection.cOwner = "gecit" Then
            ' Alıcı Ödemeli Normal
            TextBox2.Text = "1160N977588062A"
            TextBox3.Text = "YXYDe59xRA2a5Xfg"
            ' Gönderici Ödemeli Normal
            TextBox4.Text = "1160N977588062G"
            TextBox5.Text = "pk9w6VJxn9r8V7Nv"
            ' Alıcı Ödemeli Tahsilatlı
            TextBox6.Text = "1160T977588062A"
            TextBox7.Text = "rChY45wvn4091700"
            ' Gönderici Ödemeli Tahsilatlı
            TextBox8.Text = "1160T977588062G"
            TextBox9.Text = "e44VCw905Gv3yH07"
        End If
    End Sub
End Class