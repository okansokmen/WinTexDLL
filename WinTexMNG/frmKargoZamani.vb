Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Public Class frmKargoZamani

    Private Sub frmKargoZamani_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try
            Dim Connyage As SqlConnection = Nothing

            Connyage = OpenConn()

            ' Panel

            TextBox1.Text = GetSysParConnected("kzpanelurl", Connyage, "https://webpostman.kargozamani.com/")

            If oConnection.cOwner = "gecit" Then
                TextBox2.Text = GetSysParConnected("kzpanelusername", Connyage, "ayakkabilarim@kargozamani.com")
                TextBox3.Text = GetSysParConnected("kzpanelpassword", Connyage, "Ak20681*")
            Else
                TextBox2.Text = GetSysParConnected("kzpanelusername", Connyage)
                TextBox3.Text = GetSysParConnected("kzpanelpassword", Connyage)
            End If

            ' API

            TextBox4.Text = GetSysParConnected("kzapiurl", Connyage, "https://webpostman.kargozamani.com/restapi/client")

            If oConnection.cOwner = "gecit" Then
                TextBox5.Text = GetSysParConnected("kzapiusername", Connyage, "ayakkabilarim@kargozamani.com")
                TextBox6.Text = GetSysParConnected("kzapipassword", Connyage, "pJNOPa7t3nsUCrE4WFvGx8MjBTS06yR29dzfHXQq")
            Else
                TextBox5.Text = GetSysParConnected("kzapiusername", Connyage)
                TextBox6.Text = GetSysParConnected("kzapipassword", Connyage)
            End If

            Connyage.Close()

        Catch ex As Exception
            ErrDisp("frmKargoZamani_Load", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' kaydet 
        Try
            Dim Connyage As SqlConnection = Nothing

            Connyage = OpenConn()

            SetSysParConnected("kzpanelurl", SQLWriteString(TextBox1.Text, 200), Connyage)
            SetSysParConnected("kzpanelusername", SQLWriteString(TextBox2.Text, 200), Connyage)
            SetSysParConnected("kzpanelpassword", SQLWriteString(TextBox3.Text, 200), Connyage)
            SetSysParConnected("kzapiurl", SQLWriteString(TextBox4.Text, 200), Connyage)
            SetSysParConnected("kzapiusername", SQLWriteString(TextBox5.Text, 200), Connyage)
            SetSysParConnected("kzapipassword", SQLWriteString(TextBox6.Text, 200), Connyage)

            oConnection.cKZPanelUrl = TextBox1.Text.Trim
            oConnection.cKZPanelUserName = TextBox2.Text.Trim
            oConnection.cKZPanelPassword = TextBox3.Text.Trim
            oConnection.cKZApiUrl = TextBox4.Text.Trim
            oConnection.cKZApiUserName = TextBox5.Text.Trim
            oConnection.cKZApiPassword = TextBox6.Text.Trim

            Connyage.Close()

            Me.Close()

        Catch ex As Exception
            ErrDisp("Button4_Click", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' çık
        On Error Resume Next
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        On Error Resume Next
        Process.Start("iexplore.exe", TextBox1.Text)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        On Error Resume Next
        Process.Start("iexplore.exe", TextBox4.Text)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        'utilKargoZamani.KZ_GetData()
    End Sub
End Class