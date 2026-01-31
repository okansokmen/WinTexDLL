Option Explicit On
Option Strict On

Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.SqlServer.Server
Imports System.Net.Mail
Imports System.Net
Imports System.Security.Cryptography.X509Certificates

Module UtilMail

    Public Structure oHTMLRow
        Dim cValue As String
        Dim cHTMLLink As String
        Dim nBackgroundColor As Long
        Dim nForehroundColor As Long
    End Structure

    Public aHTMLRow(0, 0) As oHTMLRow

    Public Function GetHTMLTable(nRows As Integer, nCols As Integer, Optional cHeader As String = "Rapor") As String

        GetHTMLTable = ""

        Try
            Dim cText As String = ""
            Dim nRow As Integer = 0
            Dim nCol As Integer = 0

            cText = " <table align='center' width='1000' style='border:1px solid #333'> " +
                    " <tr> " +
                        " <td align='center'> " + cHeader + "</td> " +
                    " </tr> "

            cText = cText +
                    " <tr> " +
                    " <table align='center' width='1000' border='1' cellspacing='1' cellpadding='1' style='border:1px solid #ccc'> "

            For nRow = 0 To nRows
                cText = cText + " <tr> "
                For nCol = 0 To nCols
                    If Trim$(aHTMLRow(nCol, nRow).cHTMLLink) = "" Then
                        If nRow = 0 Then
                            cText = cText + "<th>" + aHTMLRow(nCol, nRow).cValue + "</th>"
                        Else
                            cText = cText + "<td>" + aHTMLRow(nCol, nRow).cValue + "</td>"
                        End If
                    Else
                        cText = cText + "<td><a title = 'Siparis detaylari için tiklayiniz' href=" + aHTMLRow(nCol, nRow).cHTMLLink + ">" + aHTMLRow(nCol, nRow).cValue + "</a></td>"
                    End If
                Next
                cText = cText + " </tr> "
            Next

            cText = cText +
                    " </table> " +
                    " </tr> " +
                    " </table> "

            GetHTMLTable = cText

        Catch ex As Exception
            ErrDisp(ex, "GetHTMLTable")
        End Try
    End Function

    Public Function SendGoogleMail(cMailTo As String, cSubject As String, cBody As String, Optional cAttachments As String = "", Optional cDatabase As String = "") As Boolean

        SendGoogleMail = False

        Try
            Dim cMailServerAddr As String = ""
            Dim cMailFromAddr As String = ""
            Dim cMailUserName As String = ""
            Dim cMailPassWord As String = ""
            Dim cMailPort As String = ""
            Dim oSQL As New SQLServerClass(True,, cDatabase)

            oSQL.OpenConn()

            cMailServerAddr = oSQL.GetSysPar("smtpserveraddress")
            cMailFromAddr = oSQL.GetSysPar("smtpfromaddress")
            cMailUserName = oSQL.GetSysPar("smtpusername")
            cMailPassWord = oSQL.GetSysPar("smtppassword")
            cMailPort = oSQL.GetSysPar("smtpport")

            If cMailServerAddr.Trim = "" Then
                cMailServerAddr = "smtp.gmail.com"
                cMailFromAddr = "wintexprogram@gmail.com"
                cMailUserName = "wintexprogram@gmail.com"
                cMailPassWord = "Wintexprogram2016"
                cMailPort = "587"
            End If

            oSQL.CloseConn()
            oSQL = Nothing

            If InStr(cMailServerAddr, "yandex") > 0 Or cMailServerAddr = "smtp.office365.com" Then
                SendGoogleMail = SendYMail(cMailTo, cSubject, cBody, cAttachments,, cMailFromAddr, cMailUserName, cMailPassWord, cMailServerAddr, CInt(cMailPort))
            Else
                SendGoogleMail = SendGMail(cMailTo, cSubject, cBody, cAttachments,, cMailFromAddr, cMailUserName, cMailPassWord, cMailServerAddr, CInt(cMailPort), True)
            End If

        Catch ex As Exception
            ErrDisp(ex, "SendGoogleMail")
        End Try
    End Function

    Private Function SendGMail(Optional ByVal cRecipients As String = "okansokmen@gmail.com",
                              Optional ByVal cSubject As String = "WinTex uyari mesajidir",
                              Optional ByVal cBody As String = "Lutfen mesajin eklerini kontrol ediniz",
                              Optional ByVal cAttachments As String = "",
                              Optional ByVal cCarbonCopys As String = "",
                              Optional ByVal cFromAddress As String = "wintexprogram@gmail.com",
                              Optional ByVal cUserName As String = "wintexprogram@gmail.com",
                              Optional ByVal cPassword As String = "Wintexprogram2016",
                              Optional ByVal cServer As String = "smtp.gmail.com",
                              Optional ByVal nPort As Integer = 587,
                              Optional ByVal lHTML As Boolean = False,
                              Optional ByVal lEnableSsl As Boolean = True,
                              Optional ByVal lTLS As Boolean = True,
                              Optional ByVal lServerCertificateValidationCallback As Boolean = True) As Boolean
        ' cRecipients is comma delimited strings
        ' cAttachments is comma delimited strings 
        Dim oEmail As System.Net.Mail.MailMessage = Nothing
        Dim SMTPServer As SmtpClient = Nothing
        Dim nCnt As Integer = 0
        Dim aRecipients() As String
        Dim aCarbonCopys() As String
        Dim aAttachments() As String
        Dim cErrMsg As String = ""
        Dim cValidRecipients As String = ""
        Dim cValidCarbonCopys As String = ""
        Dim cInvalidEmails As String = ""

        SendGMail = False

        Try
            ' Noktalı virgülleri virgüle çevir
            cRecipients = cRecipients.Replace(";", ",")

            ' Sondaki gereksiz virgülü temizle
            If Right$(cRecipients, 1) = "," Then
                cRecipients = Left$(cRecipients, Len(cRecipients) - 1)
            End If

            ' Noktalı virgülleri virgüle çevir
            cCarbonCopys = cCarbonCopys.Replace(";", ",")

            ' Sondaki gereksiz virgülü temizle
            If Right$(cCarbonCopys, 1) = "," Then
                cCarbonCopys = Left$(cCarbonCopys, Len(cCarbonCopys) - 1)
            End If

            ' Validate Recipients
            If cRecipients.Trim <> "" Then
                If InStr(cRecipients, ",") = 0 Then
                    ' Single recipient
                    If IsValidEmail(cRecipients.Trim) Then
                        cValidRecipients = cRecipients.Trim
                    Else
                        cInvalidEmails = cRecipients.Trim
                    End If
                Else
                    ' Multiple recipients
                    aRecipients = Split(cRecipients, ",")
                    For nCnt = 0 To aRecipients.GetUpperBound(0)
                        If aRecipients(nCnt).Trim <> "" Then
                            If IsValidEmail(aRecipients(nCnt).Trim) Then
                                If cValidRecipients = "" Then
                                    cValidRecipients = aRecipients(nCnt).Trim
                                Else
                                    cValidRecipients = cValidRecipients + "," + aRecipients(nCnt).Trim
                                End If
                            Else
                                If cInvalidEmails = "" Then
                                    cInvalidEmails = aRecipients(nCnt).Trim
                                Else
                                    cInvalidEmails = cInvalidEmails + "," + aRecipients(nCnt).Trim
                                End If
                            End If
                        End If
                    Next
                End If
            End If

            ' Validate Carbon Copys
            If cCarbonCopys.Trim <> "" Then
                If InStr(cCarbonCopys, ",") = 0 Then
                    ' Single CC
                    If IsValidEmail(cCarbonCopys.Trim) Then
                        cValidCarbonCopys = cCarbonCopys.Trim
                    Else
                        If cInvalidEmails = "" Then
                            cInvalidEmails = cCarbonCopys.Trim
                        Else
                            cInvalidEmails = cInvalidEmails + "," + cCarbonCopys.Trim
                        End If
                    End If
                Else
                    ' Multiple CCs
                    aCarbonCopys = Split(cCarbonCopys, ",")
                    For nCnt = 0 To aCarbonCopys.GetUpperBound(0)
                        If aCarbonCopys(nCnt).Trim <> "" Then
                            If IsValidEmail(aCarbonCopys(nCnt).Trim) Then
                                If cValidCarbonCopys = "" Then
                                    cValidCarbonCopys = aCarbonCopys(nCnt).Trim
                                Else
                                    cValidCarbonCopys = cValidCarbonCopys + "," + aCarbonCopys(nCnt).Trim
                                End If
                            Else
                                If cInvalidEmails = "" Then
                                    cInvalidEmails = aCarbonCopys(nCnt).Trim
                                Else
                                    cInvalidEmails = cInvalidEmails + "," + aCarbonCopys(nCnt).Trim
                                End If
                            End If
                        End If
                    Next
                End If
            End If

            ' Log invalid emails if any
            If cInvalidEmails <> "" Then
                ErrDisp(New Exception("Invalid email addresses found: " + cInvalidEmails), "SendGMail Email Validation")
            End If

            cErrMsg = IIf(cValidRecipients.Trim = "", "", "To : " + cValidRecipients.Trim).ToString +
                      IIf(cValidCarbonCopys.Trim = "", "", " CC : " + cValidCarbonCopys.Trim).ToString +
                      IIf(cAttachments.Trim = "", "", " Attachments : " + cAttachments.Trim).ToString +
                      " From : " + cFromAddress.Trim +
                      " Server : " + cServer.Trim +
                      " Username : " + cUserName.Trim +
                      " Password : " + cPassword.Trim +
                      " Port : " + nPort.ToString

            ' Check if there are any valid recipients
            If cValidRecipients.Trim = "" Then
                ErrDisp(New Exception("No valid email recipients found. Email not sent."), "SendGMail")
                Exit Function
            Else
                If lTLS Then
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                End If

                If lServerCertificateValidationCallback Then
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = Function(se As Object,
                                                                                      cert As System.Security.Cryptography.X509Certificates.X509Certificate,
                                                                                      chain As System.Security.Cryptography.X509Certificates.X509Chain,
                                                                                      sslerror As System.Net.Security.SslPolicyErrors) True
                End If

                oEmail = New System.Net.Mail.MailMessage
                oEmail.From = New MailAddress(cFromAddress)
                oEmail.Subject = cSubject.Trim
                oEmail.IsBodyHtml = lHTML
                oEmail.Body = cBody.Trim

                ' Add validated recipients
                If InStr(cValidRecipients, ",") = 0 Then
                    oEmail.To.Add(cValidRecipients.Trim)
                Else
                    aRecipients = Split(cValidRecipients, ",")
                    For nCnt = 0 To aRecipients.GetUpperBound(0)
                        If aRecipients(nCnt).Trim <> "" Then
                            oEmail.To.Add(aRecipients(nCnt).Trim)
                        End If
                    Next
                End If

                ' Add validated carbon copys
                If cValidCarbonCopys.Trim <> "" Then
                    If InStr(cValidCarbonCopys, ",") = 0 Then
                        oEmail.CC.Add(cValidCarbonCopys.Trim)
                    Else
                        aCarbonCopys = Split(cValidCarbonCopys, ",")
                        For nCnt = 0 To aCarbonCopys.GetUpperBound(0)
                            If aCarbonCopys(nCnt).Trim <> "" Then
                                oEmail.CC.Add(aCarbonCopys(nCnt).Trim)
                            End If
                        Next
                    End If
                End If

                If cAttachments.Trim <> "" Then
                    If InStr(cAttachments, ",") = 0 Then
                        oEmail.Attachments.Add(New Attachment(cAttachments.Trim))
                    Else
                        aAttachments = Split(cAttachments, ",")
                        For nCnt = 0 To aAttachments.GetUpperBound(0)
                            If aAttachments(nCnt).Trim <> "" Then
                                oEmail.Attachments.Add(New Attachment(aAttachments(nCnt).Trim))
                            End If
                        Next
                    End If
                End If

                SMTPServer = New System.Net.Mail.SmtpClient(cServer.Trim, nPort)
                SMTPServer.Host = cServer.Trim
                SMTPServer.Port = nPort
                SMTPServer.EnableSsl = lEnableSsl
                SMTPServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network
                SMTPServer.UseDefaultCredentials = False
                SMTPServer.Credentials = New System.Net.NetworkCredential(cUserName.Trim, cPassword.Trim)
                SMTPServer.Send(oEmail)
                SMTPServer.Dispose()

                oEmail.Dispose()
                SendGMail = True
            End If

        Catch ex As System.Net.Mail.SmtpFailedRecipientException
            ' Tek bir alıcı başarısız oldu (bazı alıcılar gidebilir, biri fail olabilir)
            ErrDisp(ex, "SendGMail: Recipient failed. " &
                      "FailedRecipient=" & ex.FailedRecipient & " " &
                      "StatusCode=" & ex.StatusCode.ToString() & " " &
                      "Inner=" & If(ex.InnerException Is Nothing, "", ex.InnerException.Message) & " " & cErrMsg)

        Catch ex As System.Net.Mail.SmtpFailedRecipientsException
            ' Birden fazla alıcı başarısız oldu
            Dim failed As String = ""
            For Each r In ex.InnerExceptions
                failed &= " [" & r.FailedRecipient & ":" & r.StatusCode.ToString() & "]"
            Next
            ErrDisp(ex, "SendGMail: Multiple recipients failed. " &
                      "Failed=" & failed & " " &
                      "Inner=" & If(ex.InnerException Is Nothing, "", ex.InnerException.Message) & " " & cErrMsg)

        Catch ex As System.Net.Mail.SmtpException
            ' Auth / TLS / server / bağlantı / politika hataları genelde burada
            ErrDisp(ex, "SendGMail: SMTP error. " &
                      "StatusCode=" & ex.StatusCode.ToString() & " " &
                      "Inner=" & If(ex.InnerException Is Nothing, "", ex.InnerException.Message) & " " & cErrMsg)

        Catch ex As System.Net.Mail.SmtpFailedRecipientException
            ErrDisp(ex, "SendGMail: Failed recipient. " &
                      "FailedRecipient=" & ex.FailedRecipient & " " &
                      "Inner=" & If(ex.InnerException Is Nothing, "", ex.InnerException.Message) & " " & cErrMsg)

        Catch ex As System.Security.Authentication.AuthenticationException
            ' TLS/SSL handshake / sertifika doğrulama
            ErrDisp(ex, "SendGMail: TLS/SSL authentication failed. " &
                      "Inner=" & If(ex.InnerException Is Nothing, "", ex.InnerException.Message) & " " & cErrMsg)

        Catch ex As System.Net.WebException
            ' Ağ katmanı hataları (proxy, bağlantı, DNS vs.) bazen buradan gelebilir
            ErrDisp(ex, "SendGMail: Network(WebException). " &
                      "Status=" & ex.Status.ToString() & " " &
                      "Inner=" & If(ex.InnerException Is Nothing, "", ex.InnerException.Message) & " " & cErrMsg)

        Catch ex As System.Net.Sockets.SocketException
            ' DNS/port/timeout/connection refused gibi düşük seviye ağ hataları
            ErrDisp(ex, "SendGMail: Socket error. " &
                      "ErrorCode=" & ex.ErrorCode.ToString() & " " &
                      "Inner=" & If(ex.InnerException Is Nothing, "", ex.InnerException.Message) & " " & cErrMsg)

        Catch ex As TimeoutException
            ErrDisp(ex, "SendGMail: Timeout. " &
                      "Inner=" & If(ex.InnerException Is Nothing, "", ex.InnerException.Message) & " " & cErrMsg)

        Catch ex As UnauthorizedAccessException
            ' Attachment dosyasına erişim yok, vb.
            ErrDisp(ex, "SendGMail: Unauthorized file access (attachment?). " &
                      "Inner=" & If(ex.InnerException Is Nothing, "", ex.InnerException.Message) & " " & cErrMsg)

        Catch ex As System.IO.FileNotFoundException
            ErrDisp(ex, "SendGMail: Attachment file not found. " &
                      "File=" & ex.FileName & " " &
                      "Inner=" & If(ex.InnerException Is Nothing, "", ex.InnerException.Message) & " " & cErrMsg)

        Catch ex As System.IO.DirectoryNotFoundException
            ErrDisp(ex, "SendGMail: Attachment folder not found. " &
                      "Inner=" & If(ex.InnerException Is Nothing, "", ex.InnerException.Message) & " " & cErrMsg)

        Catch ex As System.IO.IOException
            ErrDisp(ex, "SendGMail: File I/O error (attachment?). " &
                      "Inner=" & If(ex.InnerException Is Nothing, "", ex.InnerException.Message) & " " & cErrMsg)

        Catch ex As System.FormatException
            ' MailAddress parse, invalid address format vb.
            ErrDisp(ex, "SendGMail: Format error (address/body). " &
                      "Inner=" & If(ex.InnerException Is Nothing, "", ex.InnerException.Message) & " " & cErrMsg)

        Catch ex As ArgumentException
            ErrDisp(ex, "SendGMail: Argument error. " &
                      "Inner=" & If(ex.InnerException Is Nothing, "", ex.InnerException.Message) & " " & cErrMsg)

        Catch ex As ArgumentOutOfRangeException
            ErrDisp(ex, "SendGMail: Argument out of range (port?). " &
                      "Inner=" & If(ex.InnerException Is Nothing, "", ex.InnerException.Message) & " " & cErrMsg)

        Catch ex As InvalidOperationException
            ErrDisp(ex, "SendGMail: Invalid operation. " &
                      "Inner=" & If(ex.InnerException Is Nothing, "", ex.InnerException.Message) & " " & cErrMsg)

        Catch ex As Exception
            ' En sona genel yakalama
            ErrDisp(ex, "SendGMail: Unexpected error. " &
                      "Inner=" & If(ex.InnerException Is Nothing, "", ex.InnerException.Message) & " " & cErrMsg)

        Finally
            ' Her koşulda düzgün kapat
            Try
                If SMTPServer IsNot Nothing Then SMTPServer.Dispose()
            Catch
            End Try

            Try
                If oEmail IsNot Nothing Then oEmail.Dispose()
            Catch
            End Try

        End Try
    End Function

    ' Helper function to validate email addresses
    Private Function IsValidEmail(ByVal email As String) As Boolean
        IsValidEmail = False

        Try
            If email.Trim = "" Then
                Return False
            End If

            ' Basic validation checks
            If InStr(email, "@") = 0 Then
                Return False
            End If

            If InStr(email, ".") = 0 Then
                Return False
            End If

            ' Check for @ symbol position
            Dim atPos As Integer = InStr(email, "@")
            If atPos = 1 Or atPos = Len(email) Then
                Return False
            End If

            ' Check for multiple @ symbols
            If InStr(atPos + 1, email, "@") > 0 Then
                Return False
            End If

            ' Check for domain part (after @)
            Dim domain As String = Mid$(email, atPos + 1)
            If InStr(domain, ".") = 0 Then
                Return False
            End If

            ' Check for invalid characters
            If InStr(email, " ") > 0 Then
                Return False
            End If

            ' Try to create a MailAddress object (most reliable validation)
            Dim addr As New System.Net.Mail.MailAddress(email)
            IsValidEmail = (addr.Address = email)

        Catch ex As Exception
            IsValidEmail = False
        End Try
    End Function

    Public Function SendYMail(Optional ByVal cRecipients As String = "okansokmen@gmail.com",
                              Optional ByVal cSubject As String = "WinTex uyari mesajidir",
                              Optional ByVal cBody As String = "Lutfen mesajin eklerini kontrol ediniz",
                              Optional ByVal cAttachments As String = "",
                              Optional ByVal cCarbonCopys As String = "",
                              Optional ByVal cFromAddress As String = "",
                              Optional ByVal cUserName As String = "",
                              Optional ByVal cPassword As String = "",
                              Optional ByVal cServer As String = "",
                              Optional ByVal nPort As Integer = 587) As Boolean
        ' Yandex Mail
        ' cRecipients is comma delimited strings
        ' cAttachments is comma delimited strings 
        Dim oEmail As System.Net.Mail.MailMessage = Nothing
        Dim SMTPServer As SmtpClient = Nothing
        Dim nCnt As Integer = 0
        Dim aRecipients() As String
        Dim aCarbonCopys() As String
        Dim aAttachments() As String
        Dim cErrMsg As String = ""

        SendYMail = False

        Try
            cErrMsg = IIf(cRecipients.Trim = "", "", "To : " + cRecipients.Trim).ToString +
                      IIf(cCarbonCopys.Trim = "", "", " CC : " + cCarbonCopys.Trim).ToString +
                      IIf(cAttachments.Trim = "", "", " Attachments : " + cAttachments.Trim).ToString +
                      " From : " + cFromAddress.Trim +
                      " Server : " + cServer.Trim +
                      " Username : " + cUserName.Trim +
                      " Password : " + cPassword.Trim +
                      " Port : " + nPort.ToString

            If cRecipients.Trim = "" Then
                Exit Function
            Else
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

                System.Net.ServicePointManager.ServerCertificateValidationCallback = Function(se As Object,
                                                                                  cert As System.Security.Cryptography.X509Certificates.X509Certificate,
                                                                                  chain As System.Security.Cryptography.X509Certificates.X509Chain,
                                                                                  sslerror As System.Net.Security.SslPolicyErrors) True

                oEmail = New System.Net.Mail.MailMessage
                oEmail.From = New System.Net.Mail.MailAddress(cFromAddress)
                oEmail.Subject = cSubject.Trim
                oEmail.IsBodyHtml = True
                oEmail.Body = cBody.Trim

                If InStr(cRecipients, ",") = 0 Then
                    oEmail.To.Add(cRecipients.Trim)
                Else
                    aRecipients = Split(cRecipients, ",")
                    For nCnt = 0 To aRecipients.GetUpperBound(0)
                        If aRecipients(nCnt).Trim <> "" Then
                            oEmail.To.Add(aRecipients(nCnt).Trim)
                        End If
                    Next
                End If

                If cCarbonCopys.Trim <> "" Then
                    If InStr(cCarbonCopys, ",") = 0 Then
                        oEmail.CC.Add(cCarbonCopys.Trim)
                    Else
                        aCarbonCopys = Split(cCarbonCopys, ",")
                        For nCnt = 0 To aCarbonCopys.GetUpperBound(0)
                            If aCarbonCopys(nCnt).Trim <> "" Then
                                oEmail.CC.Add(aCarbonCopys(nCnt).Trim)
                            End If
                        Next
                    End If
                End If

                If cAttachments.Trim <> "" Then
                    If InStr(cAttachments, ",") = 0 Then
                        oEmail.Attachments.Add(New System.Net.Mail.Attachment(cAttachments.Trim))
                    Else
                        aAttachments = Split(cAttachments, ",")
                        For nCnt = 0 To aAttachments.GetUpperBound(0)
                            If aAttachments(nCnt).Trim <> "" Then
                                oEmail.Attachments.Add(New System.Net.Mail.Attachment(aAttachments(nCnt).Trim))
                            End If
                        Next
                    End If
                End If

                SMTPServer = New System.Net.Mail.SmtpClient(cServer.Trim, nPort)
                SMTPServer.Host = cServer.Trim
                SMTPServer.Port = nPort
                SMTPServer.EnableSsl = True
                SMTPServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network
                SMTPServer.UseDefaultCredentials = False
                SMTPServer.Credentials = New System.Net.NetworkCredential(cUserName.Trim, cPassword.Trim)
                SMTPServer.Send(oEmail)
                SMTPServer.Dispose()

                oEmail.Dispose()
                SendYMail = True
            End If

        Catch ex As SmtpException
            oEmail.Dispose()
            MsgBox("Sending Email Failed. Smtp Error." + ex.Message + " " + cErrMsg)
        Catch ex As ArgumentOutOfRangeException
            oEmail.Dispose()
            MsgBox("Sending Email Failed. Check Port Number." + ex.Message + " " + cErrMsg)
        Catch Ex As InvalidOperationException
            oEmail.Dispose()
            MsgBox("Sending Email Failed. Check Port Number." + Ex.Message + " " + cErrMsg)
        End Try
    End Function

End Module
