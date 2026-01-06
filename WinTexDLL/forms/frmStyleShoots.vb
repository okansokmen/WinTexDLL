Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Public Class frmStyleShoots

    Public Sub init()
        Me.ShowDialog()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Sakla / Çık
        SaveData
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' çıkış
        Me.Close()
    End Sub

    Private Sub SaveData()

        Try
            Dim oSQL As New SQLServerClass

            oSQL.OpenConn()

            oSQL.SetSysPar("SSOriginalPath", TextBox1.Text)
            oSQL.SetSysPar("SSOPUsername", TextBox2.Text)
            oSQL.SetSysPar("SSOPPassword", TextBox3.Text)
            oSQL.SetSysPar("SSOPDomain", TextBox7.Text)

            oSQL.SetSysPar("SSDestinationPath", TextBox4.Text)
            oSQL.SetSysPar("SSDPUsername", TextBox5.Text)
            oSQL.SetSysPar("SSDPPassword", TextBox6.Text)
            oSQL.SetSysPar("SSDPDomain", TextBox8.Text)

            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp("SaveData", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub frmStyleShoots_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try
            Dim oSQL As New SQLServerClass

            oSQL.OpenConn()

            TextBox1.Text = oSQL.GetSysPar("SSOriginalPath")
            TextBox2.Text = oSQL.GetSysPar("SSOPUsername")
            TextBox3.Text = oSQL.GetSysPar("SSOPPassword")
            TextBox7.Text = oSQL.GetSysPar("SSOPDomain")

            TextBox4.Text = oSQL.GetSysPar("SSDestinationPath")
            TextBox5.Text = oSQL.GetSysPar("SSDPUsername")
            TextBox6.Text = oSQL.GetSysPar("SSDPPassword")
            TextBox8.Text = oSQL.GetSysPar("SSDPDomain")

            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp("frmStyleShoots_Load", Me.Name,,, ex)
        End Try
    End Sub

End Class