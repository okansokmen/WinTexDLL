Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Public Class frmMNG

    Private Sub frmSysPar_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim Connyage As SqlConnection = Nothing

            Connyage = OpenConn()

            If oConnection.cOwner = "gecit" Then
                TextBox2.Text = GetSysParConnected("mngapiusername", Connyage, "35615719")
                TextBox3.Text = GetSysParConnected("mngapipassword", Connyage, "356TST2425XGHPRFTG")
            Else
                TextBox2.Text = GetSysParConnected("mngapiusername", Connyage)
                TextBox3.Text = GetSysParConnected("mngapipassword", Connyage)
            End If

            TextBox1.Text = GetSysParConnected("mngapiurl", Connyage, "http://service.mngkargo.com.tr/tservis/musterikargosiparis.asmx")
            TextBox4.Text = GetSysParConnected("mngfirma", Connyage, "MNG")
            TextBox5.Text = GetSysParConnected("mngiadeurl", Connyage, "http://service.mngkargo.com.tr/musterikargosiparis/musterisiparisnew.asmx")

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

            SetSysParConnected("mngapiurl", SQLWriteString(TextBox1.Text, 200), Connyage)
            SetSysParConnected("mngapiusername", SQLWriteString(TextBox2.Text, 200), Connyage)
            SetSysParConnected("mngapipassword", SQLWriteString(TextBox3.Text, 200), Connyage)
            SetSysParConnected("mngfirma", SQLWriteString(TextBox4.Text, 200), Connyage)
            SetSysParConnected("mngiadeurl", SQLWriteString(TextBox5.Text, 200), Connyage)

            oConnection.cMNGApiUrl = TextBox1.Text.Trim
            oConnection.cMNGApiUserName = TextBox2.Text.Trim
            oConnection.cMNGApiPassword = TextBox3.Text.Trim
            oConnection.cMNGFirma = TextBox4.Text.Trim
            oConnection.cMNGIadeUrl = TextBox5.Text.Trim

            Connyage.Close()

            Me.Close()

        Catch ex As Exception
            ErrDisp("Button1_Click", Me.Name,,, ex)
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
End Class