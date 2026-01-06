Option Explicit On

Imports WinTexTicimax.UyeServis
Imports System.Net
Imports System.Windows.Forms

Public Class UyeClass

    Dim oClient As UyeServisClient

    Public Sub New()

        Dim oBinding As System.ServiceModel.BasicHttpBinding = GetBinding("BasicHttpBinding_IUyeServis")
        Dim oEndPointAddress As System.ServiceModel.EndpointAddress = GetEndpointAddress(oConnection.cTiciMaxApiUyeUrl)

        oClient = New UyeServisClient(oBinding, oEndPointAddress)
    End Sub

    Public Sub CloseClient()
        On Error Resume Next
        oClient.Close()
    End Sub

    Public Sub GetList(nCase As Integer, Optional oForm As Form = Nothing)
        Try
            Dim ofrmListView As New frmListView

            Select Case nCase
                Case 1
                    Dim oUyeFilitre As New UyeFiltre

                    With oUyeFilitre
                        .Aktif = -1
                        .AlisverisYapti = -1
                        .MailIzin = -1
                        .SmsIzin = -1
                        .UyeID = -1
                    End With

                    Dim oUyeSayfalama As New UyeSayfalama

                    With oUyeSayfalama
                        .KayitSayisi = 500000
                        .SayfaNo = 0
                        .SiralamaDegeri = "id"
                        .SiralamaYonu = "asc"
                    End With

                    Dim oUyeler As New List(Of Uye)

                    oUyeler = oClient.SelectUyeler(oConnection.cTiciMaxYetkiKodu, oUyeFilitre, oUyeSayfalama)
                    ofrmListView.init(oUyeler, "Üyeler", oForm)

                Case 2
                    If nUyeID = 0 Then
                        MsgBox("Dikket Üye Seçiniz")
                        Exit Sub
                    End If
                    Dim oUyeAdres As New List(Of UyeAdres)
                    oUyeAdres = oClient.SelectUyeAdres(oConnection.cTiciMaxYetkiKodu, 0, nUyeID)
                    ofrmListView.init(oUyeAdres, "Üye Adresleri", oForm)

                Case 3
                    Dim oUyeTuru As New List(Of UyeTuru)
                    oUyeTuru = oClient.SelectUyeTuru(oConnection.cTiciMaxYetkiKodu, 0)
                    ofrmListView.init(oUyeTuru, "Üye Türleri", oForm)

            End Select

        Catch ex As Exception
            ErrDisp(ex.Message, "UyeClassGetList",,, ex)
        End Try
    End Sub

End Class
