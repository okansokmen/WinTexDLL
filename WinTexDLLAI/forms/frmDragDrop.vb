Option Explicit On

Imports System.Xml
Imports Microsoft.Office.Interop.Outlook

Public Class frmDragDrop

    Dim lReturn As Boolean
    Dim nMode As Integer
    Dim cDocValue As String = ""
    Dim cDocType As String = ""
    Dim cDocSubType As String = ""

    ' ****** Drag-Drop doesn't work if VS is started in Administrator mode ******

    Public Function init2(cFieldValue As String, cDocType1 As String, cDocSubType1 As String)

        lReturn = True
        nMode = 4
        cDocValue = cFieldValue.Trim
        cDocType = cDocType1.Trim
        cDocSubType = cDocSubType1.Trim

        Me.ShowDialog()

        init2 = lReturn
    End Function

    Public Function init(nMode1 As Integer, cKey1 As String) As Boolean
        ' nMode = 1 , siparis eMail, cKey1 = siparisno
        lReturn = True
        nMode = nMode1
        cDocValue = cKey1

        Me.ShowDialog()

        init = lReturn
    End Function

    Private Sub frmDragDrop_Load(sender As Object, e As EventArgs) Handles Me.Load
        TextEdit1.Enabled = False
        TextEdit1.AllowDrop = False
        PanelControl1.AllowDrop = False
        Me.AllowDrop = True
    End Sub

    Private Sub frmDragDrop_DragEnter(sender As Object, e As DragEventArgs) Handles Me.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Or e.Data.GetDataPresent("FileGroupDescriptor") Then
            e.Effect = DragDropEffects.All
        End If
    End Sub

    Private Sub frmDragDrop_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop

        Try
            Dim strDestinationPath As String = GetSharePath()
            Dim strDestinationFile As String = ""
            Dim strFilename As String = ""
            Dim lFileDragDrop As Boolean = False
            Dim lOutlookDragDrop As Boolean = False
            Dim aFiles() As String
            Dim dCreationDate As Date = #1/1/1950#
            Dim dModificationDate As Date = #1/1/1950#
            Dim cCreationUser As String = ""
            Dim cSenderName As String = ""
            Dim cFileExtension As String = "msg"
            Dim cFileType As String = "email"
            Dim oSQL As New SQLServerClass

            Select Case nMode
                Case 1 ' sipariş eMail veya Orjinal Döküman
                    cDocType = "Siparis"
                    cDocSubType = "ORJINAL SIPARIS"
                Case 2 ' numune 
                    cDocType = "OnModel"
                    cDocSubType = "ORJINAL NUMUNE"
                Case 3 ' ön sipariş
                    cDocType = "onsiparis"
                    cDocSubType = "ORJINAL ONSIPARIS"
                Case 4 ' diğer
                    ' do nothing
            End Select

            If e.Data.GetDataPresent("FileGroupDescriptor") Then
                lOutlookDragDrop = True
            ElseIf e.Data.GetDataPresent(DataFormats.FileDrop) Then
                lFileDragDrop = True
            End If

            'If Not IO.Directory.Exists(strDestinationPath) Then IO.Directory.CreateDirectory(strDestinationPath)

            If lFileDragDrop Then
                If e.Data.GetDataPresent(DataFormats.FileDrop) Then
                    aFiles = e.Data.GetData(DataFormats.FileDrop)
                    dCreationDate = IO.File.GetCreationTime(aFiles(0))
                    dModificationDate = IO.File.GetLastWriteTime(aFiles(0))
                    If dModificationDate < dCreationDate Then
                        dCreationDate = dModificationDate
                    End If

                    Dim fi As New IO.FileInfo(aFiles(0))
                    Dim fs As System.Security.AccessControl.FileSecurity = fi.GetAccessControl
                    Dim owner As System.Security.Principal.NTAccount = CType(fs.GetOwner(GetType(System.Security.Principal.NTAccount)), System.Security.Principal.NTAccount)
                    cCreationUser = owner.Value.ToString.Trim
                    cSenderName = owner.Value.ToString.Trim

                    AddDocument(cDocValue, cDocType, cDocSubType, aFiles(0))

                    TextEdit1.Text = cCreationUser.Trim + " " + dCreationDate.ToString
                    lReturn = True
                End If
            End If

            If lOutlookDragDrop Then
                If e.Data.GetDataPresent("FileGroupDescriptor") Then
                    'Get the name of the dragged file/message
                    Dim theStream As IO.Stream = DirectCast(e.Data.GetData("FileGroupDescriptor"), IO.Stream)
                    Dim fileGroupDescriptor As Byte() = New Byte(511) {}
                    theStream.Read(fileGroupDescriptor, 0, 512)

                    Dim fileName As New System.Text.StringBuilder("")
                    Dim i As Integer = 76
                    While fileGroupDescriptor(i) <> 0
                        fileName.Append(Convert.ToChar(fileGroupDescriptor(i)))
                        i += 1
                    End While
                    theStream.Close()

                    strFilename = fileName.ToString
                End If

                'Check if user dragged the Message or Attachment
                If IO.Path.GetExtension(strFilename).ToUpper = ".MSG" Then

                    'Message dragged and dropped
                    Dim objMI As MailItem
                    Dim objOL As New Application

                    For Each objMI In objOL.ActiveExplorer.Selection()
                        strFilename = RemoveIllegalChar(objMI.Subject) & ".msg"
                        'strDestinationFile = IO.Path.Combine(strDestinationPath, strFilename)
                        strDestinationFile = AddDocument(cDocValue, cDocType, cDocSubType, , cFileExtension, cFileType, strFilename)

                        Try
                            objMI.SaveAs(strDestinationFile, OlSaveAsType.olMSG)
                            'Check file copied OK then add to Attachment Table and File List
                        Catch ex As System.Exception
                            TextEdit1.Text = "Error copying email"
                            Exit Sub
                        End Try

                        TextEdit1.Text = "Success, email copied OK"
                    Next

                Else
                    'Attachment dragged and dropped
                    'Check if File/Message already exists
                    'strDestinationFile = IO.Path.Combine(strDestinationPath, strFilename)
                    strDestinationFile = AddDocument(cDocValue, cDocType, cDocSubType, , cFileExtension, cFileType, strFilename)

                    Try
                        Dim ms As IO.MemoryStream = DirectCast(e.Data.GetData("FileContents", True), IO.MemoryStream)
                        Dim fileBytes As Byte() = New Byte(CInt(ms.Length - 1)) {}
                        ms.Position = 0
                        ms.Read(fileBytes, 0, CInt(ms.Length))

                        Dim fs As New IO.FileStream(strDestinationFile, IO.FileMode.Create)
                        fs.Write(fileBytes, 0, CInt(fileBytes.Length))

                        fs.Close()
                    Catch ex As System.Exception
                        TextEdit1.Text = "Error copying attachment file"
                        Exit Sub
                    End Try

                    TextEdit1.Text = "Success, attachment file copied OK"
                End If

                Dim oMailItem As Microsoft.Office.Interop.Outlook.MailItem
                Dim oOutlook As New Microsoft.Office.Interop.Outlook.Application
                oMailItem = oOutlook.CreateItemFromTemplate(strDestinationFile)
                dCreationDate = oMailItem.ReceivedTime
                cCreationUser = oMailItem.SenderEmailAddress.ToString.Trim
                cSenderName = oMailItem.SenderName.ToString.Trim
                TextEdit1.Text = cCreationUser.Trim + " " + dCreationDate.ToString
                lReturn = True
            End If

            MsgBox("Gönderen eMail : " + cCreationUser + vbCrLf +
                   "Gönderen Adı : " + cSenderName + vbCrLf +
                   "Gönderim Tarihi : " + dCreationDate.ToString)

            oSQL.OpenConn()

            If cDocSubType.Trim <> "" Then

                oSQL.cSQLQuery = "select top 1 docsubtype " +
                                " from docsubtype with (NOLOCK) " +
                                " where docsubtype = '" + cDocSubType.Trim + "' "

                If Not oSQL.CheckExists Then
                    oSQL.cSQLQuery = "insert docsubtype (docsubtype) values ('" + cDocSubType.Trim + "') "
                    oSQL.SQLExecute()
                End If
            End If

            Select Case nMode
                Case 1
                    oSQL.cSQLQuery = "set dateformat dmy " +
                                    " update siparis " +
                                    " set orjdokcreationdate = '" + dCreationDate.ToString("dd.MM.yyyy HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + "', " +
                                    " orjsendername = '" + SQLWriteString(cSenderName, 50) + "', " +
                                    " orjdokcreationuser = '" + SQLWriteString(cCreationUser, 30) + "' " +
                                    " where kullanicisipno = '" + cDocValue.Trim + "' "

                    oSQL.SQLExecute()
                Case 2
                    oSQL.cSQLQuery = "set dateformat dmy " +
                                    " update onmodel " +
                                    " set orjdokcreationdate = '" + dCreationDate.ToString("dd.MM.yyyy HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + "', " +
                                    " orjsendername = '" + SQLWriteString(cSenderName, 50) + "', " +
                                    " orjdokcreationuser = '" + SQLWriteString(cCreationUser, 30) + "' " +
                                    " where onmodelno = '" + cDocValue.Trim + "' "

                    oSQL.SQLExecute()
                Case 3
                    oSQL.cSQLQuery = "set dateformat dmy " +
                                    " update onsiparis " +
                                    " set orjdokcreationdate = '" + dCreationDate.ToString("dd.MM.yyyy HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + "', " +
                                    " orjsendername = '" + SQLWriteString(cSenderName, 50) + "', " +
                                    " orjdokcreationuser = '" + SQLWriteString(cCreationUser, 30) + "' " +
                                    " where onsiparisno = '" + cDocValue.Trim + "' "

                    oSQL.SQLExecute()
                Case 4
                    ' do nothing 
            End Select

            oSQL.CloseConn()

            Me.Close()

        Catch ex As system.Exception
            ErrDisp("frmDragDrop_DragDrop", Me.Name,,, ex)
        End Try
    End Sub

    Friend Function RemoveIllegalChar(ByVal StringToCheck As String) As String
        'purpose: Eliminate characters that are not allowed in file/folder name
        RemoveIllegalChar = ""

        Try
            Dim sIllegal As String = "\,/,:,*,?,<,>,|,~," & Chr(34)
            Dim arIllegal() As String = Split(sIllegal, ",")
            Dim sReturn As String = ""
            'Dim strString2 As String

            'sReturn = StringToCheck
            'Remove all characters above 127 ascii and place result imto sReturn
            For Each c As Char In StringToCheck
                sReturn = sReturn & IIf(Asc(c) > 127, "_", c)
            Next

            For i = 0 To arIllegal.Length - 1
                sReturn = Replace(sReturn, arIllegal(i), "_")
            Next

            'Remove leading spaces and return
            Return sReturn.TrimStart

        Catch ex As system.Exception
            ErrDisp("RemoveIllegalChar", Me.Name,,, ex)
        End Try
    End Function

End Class