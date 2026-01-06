Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Public Class frmByExpress

    Private Sub frmByExpress_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim Connyage As SqlConnection = Nothing

            'TextBox6.Enabled = False
            'TextBox7.Enabled = False
            'TextBox9.Enabled = False

            Connyage = OpenConn()

            If oConnection.cOwner = "gecit" Then
                TextBox6.Text = GetSysParConnected("byexpressapiusername", Connyage, "GRYNR_GIYIM")
                TextBox7.Text = GetSysParConnected("byexpressapipassword", Connyage, "GRYNR_GIYIM")
                TextBox9.Text = GetSysParConnected("byexpresssozlesmeno", Connyage, "28")
            Else
                TextBox6.Text = GetSysParConnected("byexpressapiusername", Connyage)
                TextBox7.Text = GetSysParConnected("byexpressapipassword", Connyage)
                TextBox9.Text = GetSysParConnected("byexpresssozlesmeno", Connyage)
            End If

            TextBox8.Text = GetSysParConnected("byexpressfirma", Connyage, "BYEXPRESS")

            Connyage.Close()

        Catch ex As Exception
            ErrDisp("frmByExpress_Load", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' kaydet
        Try
            Dim Connyage As SqlConnection = Nothing

            Connyage = OpenConn()

            SetSysParConnected("byexpressapiusername", SQLWriteString(TextBox6.Text, 200), Connyage)
            SetSysParConnected("byexpressapipassword", SQLWriteString(TextBox7.Text, 200), Connyage)
            SetSysParConnected("byexpressfirma", SQLWriteString(TextBox8.Text, 200), Connyage)
            SetSysParConnected("byexpresssozlesmeno", SQLWriteString(TextBox9.Text, 200), Connyage)

            oConnection.cByExpressApiUserName = TextBox6.Text.Trim
            oConnection.cByExpressApiPassword = TextBox7.Text.Trim
            oConnection.cByExpressFirma = TextBox8.Text.Trim
            oConnection.cByExpressSozlesmeNo = TextBox9.Text.Trim

            Connyage.Close()

            Me.Close()

        Catch ex As Exception
            ErrDisp("Button1_Click", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' iptal
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        On Error Resume Next
        Process.Start("iexplore.exe", "https://api.byexpresskargo.net")
    End Sub
End Class