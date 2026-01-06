Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Public Class frmRPAParameters

    Public Sub init()
        Me.ShowDialog()
    End Sub

    Private Sub frmRPAParameters_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim oSQL As New SQLServerClass

            oSQL.OpenConn()

            TextBox1.Text = oSQL.GetSysPar("RPADatabase", "ROBO")
            TextBox2.Text = oSQL.GetSysPar("RPAUsername", Gl_UserName)
            TextBox3.Text = oSQL.GetSysPar("RPAPassword", Gl_ActivePass)
            TextBox4.Text = oSQL.GetSysPar("RPAServer", "192.168.1.8")

            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp("frmRPAParameters_Load", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' ok
        Try
            Dim oSQL As New SQLServerClass

            oSQL.OpenConn()

            oSQL.SetSysPar("RPADatabase", TextBox1.Text)
            oSQL.SetSysPar("RPAUsername", TextBox2.Text)
            oSQL.SetSysPar("RPAPassword", TextBox3.Text)
            oSQL.SetSysPar("RPAServer", TextBox4.Text)

            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp("Button2_Click", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' iptal
        Me.Close()
    End Sub
End Class