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
    Private Sub frmCrsSoft_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try
            Dim oSQL As New SQLServerClass

            oSQL.OpenConn()

            TextBox1.Text = oSQL.GetSysPar("UrlCrsEirsaliyeService", "https://connect-test.crssoft.com/Services/DespatchIntegration")
            TextBox2.Text = oSQL.GetSysPar("CrsUsername", "CrsDemo85")
            TextBox3.Text = oSQL.GetSysPar("CrsPassword", "11223385")

            oSQL.CloseConn()
            oSQL = Nothing

        Catch ex As Exception
            ErrDisp("frmCrsSoft_Load", Me.Name,,, ex)
        End Try
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

            oSQL.SetSysPar("UrlCrsEirsaliyeService", TextBox1.Text)
            oSQL.SetSysPar("CrsUsername", TextBox2.Text)
            oSQL.SetSysPar("CrsPassword", TextBox3.Text)

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
        TextBox1.Text = "https://connect-test.crssoft.com/Services/DespatchIntegration"
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ' canlı
        TextBox1.Text = "https://connect.crssoft.com/Services/DespatchIntegration"
    End Sub
End Class