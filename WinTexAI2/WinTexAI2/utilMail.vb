Option Explicit On
Imports System.IO
Imports Outlook = Microsoft.Office.Interop.Outlook

Module utilMail

    Dim oMail As Outlook.MailItem

    Public Function GetSelectedMail(Optional ByRef cMessage As String = "", Optional ByRef pdfPath As String = "") As Outlook.MailItem

        Dim myOlApp As Outlook.Application = Nothing
        Dim myOlExp As Outlook.Explorer = Nothing
        Dim myOlSel As Outlook.Selection = Nothing
        Dim mySender As Outlook.AddressEntry = Nothing
        Dim oAppt As Outlook.AppointmentItem = Nothing
        Dim oPA As Outlook.PropertyAccessor = Nothing

        Try
            GetSelectedMail = Nothing

            Dim strSenderID As String = String.Empty
            Dim MsgTxt As String = "Senders of selected items:"
            Dim X As Long

            ' Initialize Outlook application
            myOlApp = New Outlook.Application()
            myOlExp = myOlApp.ActiveExplorer
            myOlSel = myOlExp.Selection

            ' Loop through selected items
            For X = 1 To myOlSel.Count
                If myOlSel.Item(X).Class = Outlook.OlObjectClass.olMail Then
                    oMail = TryCast(myOlSel.Item(X), Outlook.MailItem)
                    If oMail IsNot Nothing Then
                        ' Use TextBox2 (or DevExpress TextEdit2)
                        cMessage = oMail.Subject ' Replace with TextEdit2.Text for DevExpress
                        Exit For
                    End If
                ElseIf myOlSel.Item(X).Class = Outlook.OlObjectClass.olAppointment Then
                    MessageBox.Show("Lütfen eMail seçiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Else
                    MessageBox.Show("Lütfen eMail seçiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            Next

            pdfPath = SaveFirstPdfAttachment(oMail, "c:\wintex\rpadocs")

            GetSelectedMail = oMail

        Catch ex As System.Exception
            ErrDisp(ex.Message, "GetSelectedMail", , , ex)
            MessageBox.Show("Error processing Outlook selection: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Clean up COM objects to prevent Outlook from hanging
            Try
                If oPA IsNot Nothing Then System.Runtime.InteropServices.Marshal.ReleaseComObject(oPA)
                If oAppt IsNot Nothing Then System.Runtime.InteropServices.Marshal.ReleaseComObject(oAppt)
                If mySender IsNot Nothing Then System.Runtime.InteropServices.Marshal.ReleaseComObject(mySender)
                If myOlSel IsNot Nothing Then System.Runtime.InteropServices.Marshal.ReleaseComObject(myOlSel)
                If myOlExp IsNot Nothing Then System.Runtime.InteropServices.Marshal.ReleaseComObject(myOlExp)
                If myOlApp IsNot Nothing Then System.Runtime.InteropServices.Marshal.ReleaseComObject(myOlApp)
            Catch ex As System.Exception
                ErrDisp(ex.Message, "GetSelectedMail - COM Cleanup", , , ex)
            End Try
        End Try
    End Function

    Public Function SaveFirstPdfAttachment(oMail As Outlook.MailItem, targetFolder As String) As String

        SaveFirstPdfAttachment = ""

        Try

            If oMail Is Nothing Then
                MessageBox.Show("No mail item selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return ""
            End If

            If oMail.Attachments Is Nothing OrElse oMail.Attachments.Count = 0 Then
                MessageBox.Show("No attachments.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return ""
            End If

            ' Outlook collections are 1-based
            Dim firstPdf As Outlook.Attachment = Nothing
            For i As Integer = 1 To oMail.Attachments.Count
                Dim att As Outlook.Attachment = oMail.Attachments.Item(i)
                If att IsNot Nothing AndAlso att.FileName IsNot Nothing AndAlso
               att.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase) Then
                    firstPdf = att
                    Exit For
                End If
            Next

            If firstPdf Is Nothing Then Exit Function  ' no PDF found

            Directory.CreateDirectory(targetFolder)

            ' Build a unique file path to avoid overwrite
            Dim destPath As String = Path.Combine(targetFolder, firstPdf.FileName)
            destPath = MakeUniquePath(destPath)

            firstPdf.SaveAsFile(destPath)
            Return destPath
            ' Optional: notify/log success
            ' MessageBox.Show("Saved: " & destPath)

        Catch ex As System.Exception
            ErrDisp(ex.Message, "SaveFirstPdfAttachment", , , ex)
        End Try
    End Function

    Private Function MakeUniquePath(cPath As String) As String

        MakeUniquePath = ""

        Try
            Dim dir = Path.GetDirectoryName(cPath)
            Dim name = Path.GetFileNameWithoutExtension(cPath)
            Dim ext = Path.GetExtension(cPath)
            Dim candidate = cPath
            Dim n = 1
            While File.Exists(candidate)
                candidate = Path.Combine(dir, $"{name} ({n}){ext}")
                n += 1
            End While
            Return candidate
        Catch ex As System.Exception
            ErrDisp(ex.Message, "MakeUniquePath", , , ex)
        End Try
    End Function

End Module
