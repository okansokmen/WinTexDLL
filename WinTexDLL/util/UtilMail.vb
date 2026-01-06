Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Web
Imports System.IO
Imports System.Net.Mail
Imports System.Threading

Module UtilMail

    Public Structure oEMail
        Dim cRecipient As String
        Dim cSender As String
        Dim cAttachment As String
        Dim cSubject As String
        Dim cMessage As String
    End Structure

    Private Function SendMail(ByVal oMail As oEMail) As Boolean

        Dim oSMTPServer As New System.Net.Mail.SmtpClient 
        Dim oMailMsg As New System.Net.Mail.MailMessage
        Dim oLock As New Object

        System.Threading.Monitor.Enter(oLock)

        SendMail = True

        Try
            'oSMTPServer.Credentials = New Net.NetworkCredential("okansokmen@gmail.com", "Hayabusa1024")
            'oSMTPServer.Port = 587 ' 25
            'oSMTPServer.Host = "smtp.gmail.com"
            'oSMTPServer.EnableSsl = True
            'oMailMsg.To.Add(oMail.cRecipient)
            'oMailMsg.From = New MailAddress(oMail.cSender)
            'oMailMsg.Subject = oMail.cSubject
            'oMailMsg.Body = oMail.cMessage
            'If oMail.cAttachment <> "" Then
            '    oMailMsg.Attachments.Add(New Attachment(oMail.cAttachment))
            'End If
            'oSMTPServer.Send(oMailMsg)

        Catch ex As Exception
            SendMail = False
        Finally
            System.Threading.Monitor.Exit(oLock)
        End Try
    End Function
End Module
