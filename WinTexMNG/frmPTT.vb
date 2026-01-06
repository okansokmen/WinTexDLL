Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Public Class frmPTT

    Private Sub frmPTT_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try
            Dim Connyage As SqlConnection = Nothing

            Connyage = OpenConn()

            'TextBox2.Enabled = False
            'TextBox3.Enabled = False
            'TextBox5.Enabled = False

            ' production : https://pttws.ptt.gov.tr/PttVeriYukleme/services/Sorgu?wsdl
            ' test : https://pttws.ptt.gov.tr/PttVeriYuklemeTest/services/Sorgu?wsdl

            If oConnection.cOwner = "gecit" Then
                TextBox2.Text = GetSysParConnected("pttapiusername", Connyage, "PttWs")
                TextBox3.Text = GetSysParConnected("pttapipassword", Connyage, "34Ep0040") ' "159263Mm*-"
                TextBox5.Text = GetSysParConnected("pttmusteri", Connyage, "504101012")
            Else
                TextBox2.Text = GetSysParConnected("pttapiusername", Connyage)
                TextBox3.Text = GetSysParConnected("pttapipassword", Connyage)
                TextBox5.Text = GetSysParConnected("pttmusteri", Connyage)
            End If

            TextBox1.Text = GetSysParConnected("pttapiurl", Connyage, "https://pttws.ptt.gov.tr/PttVeriYukleme/services/Sorgu?wsdl")
            TextBox4.Text = GetSysParConnected("pttfirma", Connyage, "PTT")

            TextBox6.Text = GetSysParConnected("pttbilgiurl", Connyage, "https://pttws.ptt.gov.tr/PttBilgi/services/Sorgu?wsdl")
            TextBox7.Text = GetSysParConnected("pttbilgiusername", Connyage, "pttUser")
            TextBox8.Text = GetSysParConnected("pttbilgipassword", Connyage, "PttBilgi*2015") ' "159263Mm*-"

            Connyage.Close()

        Catch ex As Exception
            ErrDisp("frmPTT_Load", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' kaydet
        Try
            Dim Connyage As SqlConnection = Nothing

            Connyage = OpenConn()

            SetSysParConnected("pttapiurl", SQLWriteString(TextBox1.Text, 200), Connyage)
            SetSysParConnected("pttapiusername", SQLWriteString(TextBox2.Text, 200), Connyage)
            SetSysParConnected("pttapipassword", SQLWriteString(TextBox3.Text, 200), Connyage)
            SetSysParConnected("pttfirma", SQLWriteString(TextBox4.Text, 200), Connyage)
            SetSysParConnected("pttmusteri", SQLWriteString(TextBox5.Text, 200), Connyage)

            SetSysParConnected("pttbilgiurl", SQLWriteString(TextBox6.Text, 200), Connyage)
            SetSysParConnected("pttbilgiusername", SQLWriteString(TextBox7.Text, 200), Connyage)
            SetSysParConnected("pttbilgipassword", SQLWriteString(TextBox8.Text, 200), Connyage)

            oConnection.cPTTApiUrl = TextBox1.Text.Trim
            oConnection.cPTTApiUserName = TextBox2.Text.Trim
            oConnection.cPTTApiPassword = TextBox3.Text.Trim
            oConnection.cPTTFirma = TextBox4.Text.Trim
            oConnection.cPTTMusteri = TextBox5.Text.Trim

            oConnection.cPTTBilgiUrl = TextBox6.Text.Trim
            oConnection.cPTTBilgiUserName = TextBox7.Text.Trim
            oConnection.cPTTBilgiPassword = TextBox8.Text.Trim

            Connyage.Close()

            Me.Close()

        Catch ex As Exception
            ErrDisp("Button1_Click", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Çık
        On Error Resume Next
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        On Error Resume Next
        Process.Start("iexplore.exe", TextBox1.Text)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        On Error Resume Next
        Process.Start("iexplore.exe", TextBox6.Text)
    End Sub
End Class