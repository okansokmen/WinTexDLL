Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Public Class frmSysPar

    Private Sub frmSysPar_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try
            Dim Connyage As SqlConnection

            Connyage = OpenConn()

            If oConnection.cOwner = "eroglu" Then
                TextBox3.Text = GetSysParConnected("ykkapiusername", Connyage, "ykk@eroglugiyim.com")
                TextBox4.Text = GetSysParConnected("ykkapipassword", Connyage, "€r0Glu2019!")
                TextBox5.Text = GetSysParConnected("ykkfirma", Connyage, "320 01 Y02")
                TextBox6.Text = GetSysParConnected("ykkdefaultbuyer", Connyage, "TK0226")
            Else
                TextBox3.Text = GetSysParConnected("ykkapiusername", Connyage)
                TextBox4.Text = GetSysParConnected("ykkapipassword", Connyage)
                TextBox5.Text = GetSysParConnected("ykkfirma", Connyage)
                TextBox6.Text = GetSysParConnected("ykkdefaultbuyer", Connyage)
            End If

            TextBox1.Text = GetSysParConnected("ykkapiurl", Connyage, "https://ykkapi.ykk.com.tr")
            TextBox2.Text = GetSysParConnected("ykkapitesturl", Connyage, "https://ykkapitest.ykk.com.tr")
            TextBox7.Text = GetSysParConnected("ykkportalurl", Connyage, "https://eorder.ykk.com.tr")
            TextBox8.Text = GetSysParConnected("ykkportaltesturl", Connyage, "https://eordertest.ykk.com.tr")
            TextBox9.Text = GetSysParConnected("ykktestusername", Connyage, "tahagiyim1@tahagiyim.com")
            TextBox10.Text = GetSysParConnected("ykktestpassword", Connyage, "123456Xy")

            Connyage.Close()

        Catch ex As Exception
            ErrDisp("FrmSysPar_Load", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' kaydet
        Try
            Dim Connyage As SqlConnection = Nothing

            Connyage = OpenConn()

            SetSysParConnected("ykkapiurl", SQLWriteString(TextBox1.Text, 200), Connyage)
            SetSysParConnected("ykkapitesturl", SQLWriteString(TextBox2.Text, 200), Connyage)
            SetSysParConnected("ykkapiusername", SQLWriteString(TextBox3.Text, 200), Connyage)
            SetSysParConnected("ykkapipassword", SQLWriteString(TextBox4.Text, 200), Connyage)
            SetSysParConnected("ykkfirma", SQLWriteString(TextBox5.Text, 200), Connyage)
            SetSysParConnected("ykkdefaultbuyer", SQLWriteString(TextBox6.Text, 200), Connyage)
            SetSysParConnected("ykkportalurl", SQLWriteString(TextBox7.Text, 200), Connyage)
            SetSysParConnected("ykkportaltesturl", SQLWriteString(TextBox8.Text, 200), Connyage)
            SetSysParConnected("ykktestusername", SQLWriteString(TextBox9.Text, 200), Connyage)
            SetSysParConnected("ykktestpassword", SQLWriteString(TextBox10.Text, 200), Connyage)

            oConnection.cYKKApiUrl = TextBox1.Text.Trim
            oConnection.cYKKApiTestUrl = TextBox2.Text.Trim
            oConnection.cYKKApiUserName = TextBox3.Text.Trim
            oConnection.cYKKApiPassword = TextBox4.Text.Trim
            oConnection.cYKKFirma = TextBox5.Text.Trim
            oConnection.cYKKDefaultBuyer = TextBox6.Text.Trim
            oConnection.cYKKPortalUrl = TextBox7.Text.Trim
            oConnection.cYKKPortalTestUrl = TextBox8.Text.Trim
            oConnection.cYKKTestUserName = TextBox9.Text.Trim
            oConnection.cYKKTestPassword = TextBox10.Text.Trim

            Connyage.Close()

            Me.Close()

        Catch ex As Exception
            ErrDisp("Button1_Click", Me.Name,,, ex)
        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' çık
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        On Error Resume Next
        Process.Start("iexplore.exe", TextBox1.Text)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        On Error Resume Next
        Process.Start("iexplore.exe", TextBox2.Text)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        On Error Resume Next
        Process.Start("iexplore.exe", TextBox7.Text)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        On Error Resume Next
        Process.Start("iexplore.exe", TextBox8.Text)
    End Sub
End Class