Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Public Class frmNacSms

    Private Sub frmNacSms_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim Connyage As SqlConnection = Nothing

            Connyage = OpenConn()

            TextBox1.Text = GetSysParConnected("nacurl", Connyage, "https://smslogin.nac.com.tr")

            If oConnection.cOwner = "gecit" Then
                TextBox2.Text = GetSysParConnected("nacusername", Connyage, "benimpabucum")
                TextBox3.Text = GetSysParConnected("nacpassword", Connyage, "y6nEA9mk")
                TextBox4.Text = GetSysParConnected("nacsmsbasligi", Connyage, "02129459535")
            Else
                TextBox2.Text = GetSysParConnected("nacusername", Connyage)
                TextBox3.Text = GetSysParConnected("nacpassword", Connyage)
                TextBox4.Text = GetSysParConnected("nacsmsbasligi", Connyage)
            End If

            Connyage.Close()

        Catch ex As Exception
            ErrDisp("frmNacSms_Load", Me.Name,,, ex)
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' sakla / çık
        Try
            Dim Connyage As SqlConnection = Nothing

            Connyage = OpenConn()

            SetSysParConnected("nacurl", SQLWriteString(TextBox1.Text, 200), Connyage)
            SetSysParConnected("nacusername", SQLWriteString(TextBox2.Text, 200), Connyage)
            SetSysParConnected("nacpassword", SQLWriteString(TextBox3.Text, 200), Connyage)
            SetSysParConnected("nacsmsbasligi", SQLWriteString(TextBox4.Text, 200), Connyage)

            oConnection.cNacUrl = TextBox1.Text.Trim
            oConnection.cNacUserName = TextBox2.Text.Trim
            oConnection.cNacPassword = TextBox3.Text.Trim
            oConnection.cNacSmsBasligi = TextBox4.Text.Trim

            Connyage.Close()

            Me.Close()

        Catch ex As Exception
            ErrDisp("Button1_Click", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' iptal / çık
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        On Error Resume Next
        Process.Start("iexplore.exe", TextBox1.Text)
    End Sub
End Class