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

            TextBox1.Text = GetSysParConnected("ykkapiurl", Connyage, "https://eorder.ykk.com.tr")          ' https://ykkapitest.ykk.com.tr/
            TextBox2.Text = GetSysParConnected("ykkapitesturl", Connyage, "https:/eordertest.ykk.com.tr")   ' https://ykkapitest.ykk.com.tr/
            TextBox3.Text = GetSysParConnected("ykkapiusername", Connyage, "ykk@eroglugiyim.com")           ' tahagiyim1@tahagiyim.com
            TextBox4.Text = GetSysParConnected("ykkapipassword", Connyage, "€r0Glu2019!")                   ' 123456Xy
            TextBox5.Text = GetSysParConnected("ykkfirma", Connyage, "320 03 Y04")
            TextBox6.Text = GetSysParConnected("ykkdefaultbuyer", Connyage, "TK0226")

            Connyage.Close()

        Catch ex As Exception
            ErrDisp("FrmSysPar_Load", Me.Name,,, ex)
        End Try

    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
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

            oConnection.cYKKApiUrl = TextBox1.Text.Trim
            oConnection.cYKKApiTestUrl = TextBox2.Text.Trim
            oConnection.cYKKApiUserName = TextBox3.Text.Trim
            oConnection.cYKKApiPassword = TextBox4.Text.Trim
            oConnection.cYKKFirma = TextBox5.Text.Trim

            Connyage.Close()

            Me.Close()

        Catch ex As Exception
            ErrDisp("Button1_Click", Me.Name,,, ex)
        End Try

    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        ' çık
        Me.Close()
    End Sub
End Class