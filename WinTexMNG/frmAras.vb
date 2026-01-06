Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

' sorgulama için
' Production Ortamı Linki
' https://customerservices.araskargo.com.tr/ArasCargoCustomerIntegrationService/ArasCargoIntegrationService.svc
' Test Ortamı Linki 
' http://customerservicestest.araskargo.com.tr/ArasCargoIntegrationService.svc
' Test Ortamı Bilgileri
' Username: neodyum
' Password: nd2580
' CustomerCode = 1932448851342

' gönderi kaydı yapmak için
' https://appls-srv.araskargo.com.tr/arascargoservice/arascargoservice.asmx

Public Class frmAras

    Private Sub frmAras_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim Connyage As SqlConnection = Nothing

            Connyage = OpenConn()

            If oConnection.cOwner = "gecit" Then
                TextBox2.Text = GetSysParConnected("arasapiusername", Connyage, "mares")
                TextBox3.Text = GetSysParConnected("arasapipassword", Connyage, "my343827")
                TextBox5.Text = GetSysParConnected("arasmusterikodu", Connyage, "2329754550911")
                TextBox6.Text = GetSysParConnected("arasapiusername2", Connyage, "mares")
                TextBox7.Text = GetSysParConnected("arasapipassword2", Connyage, "x3b96n8iok")
            Else
                TextBox2.Text = GetSysParConnected("arasapiusername", Connyage)
                TextBox3.Text = GetSysParConnected("arasapipassword", Connyage)
                TextBox5.Text = GetSysParConnected("arasmusterikodu", Connyage)
                TextBox6.Text = GetSysParConnected("arasapiusername2", Connyage)
                TextBox7.Text = GetSysParConnected("arasapipassword2", Connyage)
            End If

            TextBox1.Text = GetSysParConnected("arasapiurl", Connyage, "https://customerservices.araskargo.com.tr/ArasCargoCustomerIntegrationService/ArasCargoIntegrationService.svc")
            TextBox4.Text = GetSysParConnected("araswintexkodu", Connyage, "ARAS")

            Connyage.Close()

        Catch ex As Exception
            ErrDisp("frmAras_Load", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' kaydet
        Try
            Dim Connyage As SqlConnection = Nothing

            Connyage = OpenConn()

            SetSysParConnected("arasapiurl", SQLWriteString(TextBox1.Text, 200), Connyage)
            SetSysParConnected("arasapiusername", SQLWriteString(TextBox2.Text, 200), Connyage)
            SetSysParConnected("arasapipassword", SQLWriteString(TextBox3.Text, 200), Connyage)
            SetSysParConnected("araswintexkodu", SQLWriteString(TextBox4.Text, 200), Connyage)
            SetSysParConnected("arasmusterikodu", SQLWriteString(TextBox5.Text, 200), Connyage)
            SetSysParConnected("arasapiusername2", SQLWriteString(TextBox6.Text, 200), Connyage)
            SetSysParConnected("arasapipassword2", SQLWriteString(TextBox7.Text, 200), Connyage)

            oConnection.cArasApiUrl = TextBox1.Text.Trim
            oConnection.cArasApiUserName = TextBox2.Text.Trim
            oConnection.cArasApiPassword = TextBox3.Text.Trim
            oConnection.cArasWinTexKodu = TextBox4.Text.Trim
            oConnection.cArasMusteriKodu = TextBox5.Text.Trim
            oConnection.cArasApiUserName2 = TextBox6.Text.Trim
            oConnection.cArasApiPassword2 = TextBox7.Text.Trim

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
        ' goto url
        On Error Resume Next
        Process.Start("iexplore.exe", TextBox1.Text)
    End Sub

End Class