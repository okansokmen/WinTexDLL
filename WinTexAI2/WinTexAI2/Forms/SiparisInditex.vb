Option Explicit On

Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Windows.Forms
Imports System.Threading.Tasks
Imports System.Runtime.CompilerServices
Imports DevExpress.XtraPdfViewer
Imports Newtonsoft.Json.Linq
Imports Outlook = Microsoft.Office.Interop.Outlook
Imports HtmlAgilityPack

Public Class SiparisInditex

    Dim oMail As Outlook.MailItem
    Dim lFirstLoad As Boolean
    Dim cFileName As String
    Dim pdfPath As String
    Dim nDatabase As Integer
    Dim lCBLoad As Boolean
    Dim nMode As Integer
    Dim cInputFilter As String
    Dim cFilter As String
    Dim oSiparis As OrderDoc
    Dim cFinalJson As String

    Public Sub init(Optional nCase As Integer = 1, Optional cInputFilter1 As String = "", Optional nDatabase1 As Integer = 1)
        Try
            nMode = nCase
            cInputFilter = cInputFilter1
            nDatabase = nDatabase1
            cFilter = ""
            G_Selection = ""
            G_Selection2 = ""

            Me.Show()
        Catch ex As System.Exception
            ErrDisp(ex.Message, "init", , , ex)
        End Try
    End Sub

    Private Sub SiparisInditex_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            nDatabase = 0

            initWinTex()
            LoadCombo4()

            ' sertifikalar listesi
            ListView1.View = System.Windows.Forms.View.Details ' Enable Details view for multi-column display
            ListView1.FullRowSelect = True ' Select entire row
            ListView1.MultiSelect = False ' Single selection (change to True for multi-select)
            ListView1.GridLines = True ' Show grid lines for table-like appearance
            ListView1.LabelEdit = False ' Disable editing labels
            ListView1.AllowColumnReorder = False ' Optional: Prevent column reordering

            ListView1.Columns.Add("Sertifika", 200) ' Width in pixels

            ' ön sipariş listesi
            ListView2.View = System.Windows.Forms.View.Details ' Enable Details view for multi-column display
            ListView2.FullRowSelect = True ' Select entire row
            ListView2.MultiSelect = False ' Single selection (change to True for multi-select)
            ListView2.GridLines = True ' Show grid lines for table-like appearance
            ListView2.LabelEdit = False ' Disable editing labels
            ListView2.AllowColumnReorder = False ' Optional: Prevent column reordering

            ListView2.Columns.Add("On Siparis", 200) ' Width in pixels
            ListView2.Columns.Add("Satir No", 100) ' Width in pixels

            ' Configure WebBrowser1 to be restricted and have scroll bars
            WebBrowser1.AllowNavigation = False ' Prevent navigation to other URLs
            WebBrowser1.AllowWebBrowserDrop = False ' Prevent drag and drop
            WebBrowser1.IsWebBrowserContextMenuEnabled = False ' Disable context menu
            WebBrowser1.WebBrowserShortcutsEnabled = False ' Disable keyboard shortcuts
            WebBrowser1.ScriptErrorsSuppressed = True ' Suppress script errors

            ' Set scroll bars to always be visible
            WebBrowser1.ScrollBarsEnabled = True

        Catch ex As System.Exception
            ErrDisp(ex.Message, "SiparisInditex_Load", , , ex)
        End Try
    End Sub

    Private Sub CheckEdit1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit1.CheckedChanged
        Try
            ' eski modele bağla
            If CheckEdit1.Checked Then
                lCBLoad = True
                CheckEdit2.Checked = False
                CheckEdit3.Checked = False
                lCBLoad = False
            End If
            FillModel()
        Catch ex As System.Exception
            ErrDisp(ex.Message, "CheckEdit1_CheckedChanged", , , ex)
        End Try
    End Sub

    Private Sub CheckEdit2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit2.CheckedChanged
        Try
            ' eski modelden kopyala
            If CheckEdit2.Checked Then
                lCBLoad = True
                CheckEdit1.Checked = False
                CheckEdit3.Checked = False
                lCBLoad = False
            End If
            FillModel()
        Catch ex As System.Exception
            ErrDisp(ex.Message, "CheckEdit2_CheckedChanged", , , ex)
        End Try
    End Sub

    Private Sub CheckEdit3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit3.CheckedChanged
        Try
            ' yeni model
            If CheckEdit3.Checked Then
                lCBLoad = True
                CheckEdit1.Checked = False
                CheckEdit2.Checked = False
                lCBLoad = False
            End If
            FillModel()
        Catch ex As System.Exception
            ErrDisp(ex.Message, "CheckEdit3_CheckedChanged", , , ex)
        End Try
    End Sub

    Private Sub FillModel()
        Try
            If lCBLoad Then Exit Sub

            If CheckEdit1.Checked Then
                ' eski modele bağla
                SimpleButton10.Enabled = True

                TextEdit4.Text = ""
                TextEdit4.Enabled = False

                ComboBoxEdit6.Text = "YOK"
                ComboBoxEdit6.Enabled = False

                TextEdit8.Text = ""
                SimpleButton13.Enabled = False

            ElseIf CheckEdit2.Checked Then
                ' eski modelden kopyala
                SimpleButton10.Enabled = True

                TextEdit4.Text = TextEdit7.Text
                TextEdit4.Enabled = True

                ComboBoxEdit6.Text = "YOK"
                ComboBoxEdit6.Enabled = False

                TextEdit8.Text = ""
                SimpleButton13.Enabled = False

            ElseIf CheckEdit3.Checked Then
                ' yeni model
                TextEdit7.Text = "YOK"
                SimpleButton10.Enabled = False

                TextEdit4.Enabled = True

                ComboBoxEdit6.Enabled = True

                SimpleButton13.Enabled = True
            End If

            If Not (CheckEdit1.Checked Or CheckEdit2.Checked Or CheckEdit3.Checked) Then
                SimpleButton10.Enabled = True
                TextEdit7.Text = "YOK"

                TextEdit4.Enabled = True
                TextEdit4.Text = ""

                ComboBoxEdit6.Enabled = True
                ComboBoxEdit6.Text = "YOK"
            End If
        Catch ex As System.Exception
            ErrDisp(ex.Message, "FillModel", , , ex)
        End Try
    End Sub

    Private Sub LoadCombo4()
        Try
            ComboBoxEdit4.Properties.Items.Clear()
            ComboBoxEdit4.Properties.Items.Add("TURKIYE")
            ComboBoxEdit4.Properties.Items.Add("MISIR")
        Catch ex As System.Exception
            ErrDisp(ex.Message, "LoadCombo4", , , ex)
        End Try
    End Sub

    Private Sub ComboBoxEdit4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEdit4.SelectedIndexChanged
        Try
            Select Case Trim(ComboBoxEdit4.Text)
                Case "TURKIYE"
                    nDatabase = 1
                    LoadCombo1()
                    LoadCombo6()
                    LoadCombo12()
                    LoadGoogleAI(1)

                Case "MISIR"
                    nDatabase = 2
                    LoadCombo1()
                    LoadCombo6()
                    LoadCombo12()
                    LoadGoogleAI(2)

            End Select
        Catch ex As System.Exception
            ErrDisp(ex.Message, "ComboBoxEdit4_SelectedIndexChanged", , , ex)
        End Try
    End Sub

    Private Sub LoadCombo1()
        Try
            If nDatabase = 0 Then
                MsgBox("Üretim ülkesi seçiniz")
                Exit Sub
            End If

            Dim oSQLServer As New SQLServerClass

            ComboBoxEdit1.Properties.Items.Clear()

            oSQLServer.init(nDatabase)
            oSQLServer.OpenConn()
            oSQLServer.cSQLQuery = "select firma " +
                                    " from firma with (NOLOCK) " +
                                    " where firmatipi like 'M_STER_' " +
                                    " and aiprompt is not null " +
                                    " and aijson is not null " +
                                    " order by firma "

            oSQLServer.GetSQLReader()

            Do While oSQLServer.oReader.Read
                ComboBoxEdit1.Properties.Items.Add(oSQLServer.SQLReadString("firma"))
            Loop
            oSQLServer.oReader.Close()
            oSQLServer.CloseConn()
        Catch ex As System.Exception
            ErrDisp(ex.Message, "LoadCombo1", , , ex)
        End Try
    End Sub

    Private Sub LoadCombo6()
        Try
            If nDatabase = 0 Then
                MsgBox("Üretim ülkesi seçiniz")
                Exit Sub
            End If

            Dim oSQLServer As New SQLServerClass

            ComboBoxEdit6.Properties.Items.Clear()
            ComboBoxEdit6.Properties.Items.Add("YOK")

            oSQLServer.init(nDatabase)
            oSQLServer.OpenConn()
            oSQLServer.cSQLQuery = "select distinct formno " +
                                    " from frmuretim with (NOLOCK) " +
                                    " where formno is Not Null " +
                                    " and formno <> '' " +
                                    " order by formno "

            oSQLServer.GetSQLReader()

            Do While oSQLServer.oReader.Read
                ComboBoxEdit6.Properties.Items.Add(oSQLServer.SQLReadString("formno"))
            Loop
            oSQLServer.oReader.Close()
            oSQLServer.CloseConn()
        Catch ex As System.Exception
            ErrDisp(ex.Message, "LoadCombo6", , , ex)
        End Try
    End Sub

    Private Sub LoadCombo12()
        Try
            If nDatabase = 0 Then
                MsgBox("Üretim ülkesi seçiniz")
                Exit Sub
            End If

            Dim oSQLServer As New SQLServerClass

            ComboBoxEdit12.Properties.Items.Clear()

            oSQLServer.init(nDatabase)
            oSQLServer.OpenConn()
            oSQLServer.cSQLQuery = "Select firma " +
                                    " from firma with (NOLOCK) " +
                                    " where uretici = 'E' " +
                                    " and firma is not null " +
                                    " and firma <> '' " +
                                    " order by firma "

            oSQLServer.GetSQLReader()

            Do While oSQLServer.oReader.Read
                ComboBoxEdit12.Properties.Items.Add(oSQLServer.SQLReadString("firma"))
            Loop
            oSQLServer.oReader.Close()
            oSQLServer.CloseConn()
        Catch ex As System.Exception
            ErrDisp(ex.Message, "LoadCombo12", , , ex)
        End Try
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Try
            If nDatabase = 0 Then
                MsgBox("Üretim ülkesi seçiniz")
                Exit Sub
            End If

            If Trim(ComboBoxEdit1.Text) = "" Then
                MsgBox("Müşteri seçiniz")
                Exit Sub
            End If

            Dim oSQLServer As New SQLServerClass
            Dim oSQLServer2 As New SQLServerClass
            Dim cSip1 As String = ""
            Dim cSip2 As String = ""

            oSQLServer.init(nDatabase)
            oSQLServer.OpenConn()

            oSQLServer.cSQLQuery = "select top 1 kullanicisipno " +
                        " from siparis with (NOLOCK) " +
                        " where musterino = '" + Trim(ComboBoxEdit1.Text) + "' " +
                        " and kullanicisipno is not null " +
                        " and kullanicisipno <> '' " +
                        " order by kullanicisipno desc "

            cSip1 = oSQLServer.DBReadString

            oSQLServer.CloseConn()

            oSQLServer2.init(nDatabase)
            oSQLServer2.OpenConn()

            oSQLServer2.cSQLQuery = "select top 1 siparisno " +
                        " from rpa_siparis with (NOLOCK) " +
                        " where musterino = '" + Trim(ComboBoxEdit1.Text) + "' " +
                        " and siparisno is not null " +
                        " and siparisno <> '' " +
                        " order by siparisno desc "

            cSip2 = oSQLServer2.DBReadString

            oSQLServer2.CloseConn()

            If cSip1 > cSip2 Then
                TextEdit1.Text = cSip1
            Else
                TextEdit1.Text = cSip2
            End If
        Catch ex As System.Exception
            ErrDisp(ex.Message, "SimpleButton1_Click", , , ex)
        End Try
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        Try
            Me.Close()
        Catch ex As System.Exception
            ErrDisp(ex.Message, "SimpleButton3_Click", , , ex)
        End Try
    End Sub

    Private Sub SimpleButton6_Click(sender As Object, e As EventArgs) Handles SimpleButton6.Click
        Try
            ' sertifika çıkart
            If ListView1.Items.Count >= 1 Then
                If ListView1.SelectedIndices.Count = 0 Then
                    ' Select the last item by setting its Selected property
                    ListView1.Items(ListView1.Items.Count - 1).Selected = True
                End If
                ' Remove the first selected item
                If ListView1.SelectedIndices.Count > 0 Then
                    ListView1.Items.RemoveAt(ListView1.SelectedIndices(0))
                End If
            End If
        Catch ex As System.Exception
            ErrDisp(ex.Message, "SimpleButton6_Click", , , ex)
        End Try
    End Sub

    Private Sub SimpleButton7_Click(sender As Object, e As EventArgs) Handles SimpleButton7.Click
        Try
            ' ön sipariş çıkart
            If ListView2.Items.Count >= 1 Then
                If ListView2.SelectedIndices.Count = 0 Then
                    ' Select the last item by setting its Selected property
                    ListView2.Items(ListView2.Items.Count - 1).Selected = True
                End If
                ' Remove the first selected item
                If ListView2.SelectedIndices.Count > 0 Then
                    ListView2.Items.RemoveAt(ListView2.SelectedIndices(0))
                End If
            End If
        Catch ex As System.Exception
            ErrDisp(ex.Message, "SimpleButton7_Click", , , ex)
        End Try
    End Sub

    Private Sub SimpleButton9_Click(sender As Object, e As EventArgs) Handles SimpleButton9.Click
        Try
            ' ön maliyet seç
            If Trim(ComboBoxEdit1.Text) = "" Then
                MsgBox("Müşteri seçiniz")
                Exit Sub
            End If

            If nDatabase = 0 Then
                MsgBox("Üretim ülkesi seçiniz")
                Exit Sub
            End If

            Dim oSelectFromListboxt As New SelectFromListbox
            oSelectFromListboxt.init(1, Trim(ComboBoxEdit1.Text), nDatabase)

            TextEdit6.Text = G_Selection
        Catch ex As System.Exception
            ErrDisp(ex.Message, "SimpleButton9_Click", , , ex)
        End Try
    End Sub

    Private Sub SimpleButton10_Click(sender As Object, e As EventArgs) Handles SimpleButton10.Click
        Try
            ' eski model seç
            If Trim(ComboBoxEdit1.Text) = "" Then
                MsgBox("Müşteri seçiniz")
                Exit Sub
            End If

            If nDatabase = 0 Then
                MsgBox("Üretim ülkesi seçiniz")
                Exit Sub
            End If

            Dim oSelectFromListboxt As New SelectFromListbox
            oSelectFromListboxt.init(2, Trim(ComboBoxEdit1.Text), nDatabase)

            TextEdit7.Text = G_Selection
            TextEdit4.Text = G_Selection
        Catch ex As System.Exception
            ErrDisp(ex.Message, "SimpleButton10_Click", , , ex)
        End Try
    End Sub

    Private Sub ComboBoxEdit1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEdit1.SelectedIndexChanged
        Try
            ' müşteri firma seçildi
            Dim oSQLServer As New SQLServerClass

            If Trim(ComboBoxEdit1.Text) = "" Then
                MsgBox("Müşteri seçiniz")
                Exit Sub
            End If

            If nDatabase = 0 Then
                MsgBox("Üretim ülkesi seçiniz")
                Exit Sub
            End If

            oSQLServer.init(nDatabase)
            oSQLServer.OpenConn()
            oSQLServer.cSQLQuery = "select top 1 ozelalan3 " +
                            " from firma with (NOLOCK) " +
                            " where firma = '" + Trim$(ComboBoxEdit1.Text) + "' "

            oSQLServer.GetSQLReader()

            If oSQLServer.oReader.Read Then
                TextEdit5.Text = oSQLServer.SQLReadString("ozelalan3")
            End If
            oSQLServer.oReader.Close()
        Catch ex As System.Exception
            ErrDisp(ex.Message, "ComboBoxEdit1_SelectedIndexChanged", , , ex)
        End Try
    End Sub

    Private Sub SimpleButton11_Click(sender As Object, e As EventArgs) Handles SimpleButton11.Click
        Try
            ' ön sipariş seç

            If Trim(ComboBoxEdit1.Text) = "" Then
                MsgBox("Müşteri seçiniz")
                Exit Sub
            End If

            If nDatabase = 0 Then
                MsgBox("Üretim ülkesi seçiniz")
                Exit Sub
            End If

            Dim oSelectFromListboxt As New SelectFromListbox
            oSelectFromListboxt.init(3, Trim(ComboBoxEdit1.Text), nDatabase)

            If Trim(G_Selection) = "" Then Exit Sub
            If Trim(G_Selection2) = "" Then Exit Sub

            If ListView2.Items.Count >= 1 Then
                For nRow As Integer = 0 To ListView2.Items.Count - 1
                    If ListView2.Items(nRow).Text.Trim = Trim(G_Selection) And ListView2.Items(nRow).SubItems(1).Text.Trim = Trim(G_Selection2) Then
                        MsgBox(Trim(G_Selection) + " " + Trim(G_Selection2) + " ön siparişi daha önce eklenmiş")
                        Exit Sub
                    End If
                Next
            End If

            Dim newItem As New ListViewItem(Trim(G_Selection))
            newItem.SubItems.Add(Trim(G_Selection2))
            ListView2.Items.Add(newItem)
        Catch ex As System.Exception
            ErrDisp(ex.Message, "SimpleButton11_Click", , , ex)
        End Try
    End Sub

    Private Sub SimpleButton12_Click(sender As Object, e As EventArgs) Handles SimpleButton12.Click
        Try
            ' sertifika ekle

            If nDatabase = 0 Then
                MsgBox("Üretim ülkesi seçiniz")
                Exit Sub
            End If

            Dim oSelectFromListboxt As New SelectFromListbox
            oSelectFromListboxt.init(4, "", nDatabase)

            If Trim(G_Selection) = "" Then Exit Sub

            If ListView1.Items.Count >= 1 Then
                For nRow As Integer = 0 To ListView1.Items.Count - 1
                    If ListView1.Items(nRow).Text = Trim(G_Selection) Then
                        MsgBox(Trim(G_Selection) & " sertifikası daha önce eklenmiş")
                        Exit Sub
                    End If
                Next
            End If

            Dim newItem As New ListViewItem(Trim(G_Selection))
            ListView1.Items.Add(newItem)
        Catch ex As System.Exception
            ErrDisp(ex.Message, "SimpleButton12_Click", , , ex)
        End Try
    End Sub

    Private Sub SimpleButton13_Click(sender As Object, e As EventArgs) Handles SimpleButton13.Click
        Try
            ' ana model tipi seç
            If nDatabase = 0 Then
                MsgBox("Üretim ülkesi seçiniz")
                Exit Sub
            End If

            Dim oSelectFromListboxt As New SelectFromListbox
            oSelectFromListboxt.init(5, "", nDatabase)

            TextEdit8.Text = Trim(G_Selection)
        Catch ex As System.Exception
            ErrDisp(ex.Message, "SimpleButton13_Click", , , ex)
        End Try
    End Sub

    Private Sub SimpleButton8_Click(sender As Object, e As EventArgs) Handles SimpleButton8.Click
        Try
            ' outlook mail seç

            Dim cMessage As String = ""

            If Trim(ComboBoxEdit1.Text) = "" Then
                MsgBox("Lütfen müşterifirmayı seçiniz")
                Exit Sub
            End If

            If Trim(ComboBoxEdit1.Text) = "" Then
                MsgBox("Lütfen sipariş numarasını giriniz")
                Exit Sub
            End If

            oMail = GetSelectedMail(cMessage, pdfPath)

            TextEdit2.Text = cMessage
            cFileName = Path.GetFileName(pdfPath)

            PdfViewer1.LoadDocument(pdfPath)
            PdfViewer1.ZoomMode = PdfZoomMode.FitToWidth
            PdfViewer1.CurrentPageNumber = 1

            TextEdit3.Text = pdfPath

            If oMail Is Nothing Then
                MsgBox("Lütfen bir eMail seçiniz")
                Exit Sub
            End If

            If oMail.Attachments.Count = 0 Then
                MsgBox("Lütfen eMail eklerini kontrol ediniz")
                Exit Sub
            End If
        Catch ex As System.Exception
            ErrDisp(ex.Message, "SimpleButton8_Click", , , ex)
        End Try
    End Sub

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        Try
            XtraTabControl1.SelectedTabPage = XtraTabControl1.TabPages(3)
            UpdateSplitterTo50()

            ' Open file dialog to select PDF
            Using openFileDialog As New OpenFileDialog

                openFileDialog.Filter = "PDF Files|*.pdf"
                openFileDialog.Title = "Select a PDF File"

                If openFileDialog.ShowDialog() = DialogResult.OK Then

                    pdfPath = openFileDialog.FileName
                    cFileName = Path.GetFileName(pdfPath)

                    PdfViewer1.LoadDocument(pdfPath)
                    PdfViewer1.ZoomMode = PdfZoomMode.FitToWidth
                    PdfViewer1.CurrentPageNumber = 1

                    TextEdit3.Text = pdfPath
                End If
            End Using
        Catch ex As System.Exception
            ErrDisp(ex.Message, "SimpleButton5_Click", , , ex)
        End Try
    End Sub

    Private Sub PdfViewer1_DocumentChanged(sender As Object, e As EventArgs) Handles PdfViewer1.DocumentChanged
        Try
            PdfViewer1.ZoomMode = PdfZoomMode.FitToWidth
            PdfViewer1.CurrentPageNumber = 1
        Catch ex As System.Exception
            ErrDisp(ex.Message, "PdfViewer1_DocumentChanged", , , ex)
        End Try
    End Sub

    Private Sub UpdateSplitterTo50()
        Try
            If SplitContainerControl1.Horizontal Then
                ' Top/Bottom panels: position is Panel1 height in pixels
                SplitContainerControl1.SplitterPosition = SplitContainerControl1.ClientSize.Height \ 2
            Else
                ' Left/Right panels: position is Panel1 width in pixels
                SplitContainerControl1.SplitterPosition = SplitContainerControl1.ClientSize.Width \ 2
            End If
        Catch ex As System.Exception
            ErrDisp(ex.Message, "UpdateSplitterTo50", , , ex)
        End Try
    End Sub

    Private Sub SiparisInditex_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Try
            UpdateSplitterTo50()
        Catch ex As System.Exception
            ErrDisp(ex.Message, "SiparisInditex_Resize", , , ex)
        End Try
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        ' save
        Try
            Dim oSQLServer As New SQLServerClass
            Dim oSQLServer2 As New SQLServerClass
            Dim cAppPath As String = ""
            Dim cPersonel As String = ""
            Dim cEskiModel As String = ""
            Dim cModelKopyala As String = ""
            Dim cYeniModel As String = ""
            Dim cFabric1 As String = ""
            Dim cFabric2 As String = ""
            Dim nRow As Long = 0
            Dim cBeden As String = ""
            Dim cBedenBel As String = ""
            Dim cBedenBoy As String = ""
            Dim nCol As Integer = 0
            Dim nCnt As Integer = 0
            Dim cDestination_code As String
            Dim cDestination_description As String
            Dim dDelivery_date As Date
            Dim cAssortment_code As String
            Dim cPayment_term As String
            Dim aBeden As String()
            Dim nTotal As Integer = 0
            Dim cPayment_code As String = ""
            Dim nAdet As Integer = 0
            Dim nMaxRows As Integer = 0
            Dim nMaxCols As Integer = 0

            If oConnection.cOwner <> "eroglu_deneme" And oConnection.cOwner <> "eroglu_local" Then

                If nDatabase = 0 Then
                    MsgBox("Üretim ülkesi seçiniz")
                    Exit Sub
                End If

                cPersonel = GetPersonel()

                If ListView1.Items.Count = 0 Then
                    MsgBox("En az 1 sertifika seçmelisiniz")
                    Exit Sub
                End If

                If ListView2.Items.Count = 0 Then
                    MsgBox("En az 1 ön sipariş seçmelisiniz")
                    Exit Sub
                End If

                If ListView1.Items(0).ToString = "" Then
                    MsgBox("En az 1 sertifika seçmelisiniz")
                    Exit Sub
                End If

                If CheckEdit1.Checked Then
                    cEskiModel = "E"
                Else
                    cEskiModel = "H"
                End If

                If CheckEdit2.Checked Then
                    cModelKopyala = "E"
                Else
                    cModelKopyala = "H"
                End If

                If CheckEdit3.Checked Then
                    cYeniModel = "E"
                Else
                    cYeniModel = "H"
                End If

                If Trim(TextEdit1.Text) = "" Then
                    MsgBox("Yeni sipariş no alanını doldurunuz")
                    Exit Sub
                End If

                'If Trim(TextEdit2.Text) = "" Then
                '    MsgBox("eMail seçiniz")
                '    Exit Sub
                'End If

                If Trim(TextEdit4.Text) = "" Or Trim(TextEdit4.Text) = "YOK" Then
                    MsgBox("Yeni model no alanını doldurunuz")
                    Exit Sub
                Else
                    If CheckEdit1.Checked Then
                        If Trim(TextEdit4.Text) <> Trim(TextEdit7.Text) Then
                            MsgBox("Eski ve yeni model no aynı olmalı")
                            Exit Sub
                        End If
                    End If
                    If CheckEdit2.Checked Then
                        If Trim(TextEdit4.Text) = Trim(TextEdit7.Text) Then
                            MsgBox("Eski ve yeni model no farklı olmalı")
                            Exit Sub
                        End If
                    End If
                End If

                If Trim(ComboBoxEdit1.Text) = "" Then
                    MsgBox("Müşteri seçiniz")
                    Exit Sub
                End If

                If Trim(TextEdit6.Text) = "" Then
                    MsgBox("Ön maliyet seçiniz")
                    Exit Sub
                End If

                If cYeniModel = "E" Then
                    If Trim(TextEdit8.Text) = "" Then
                        MsgBox("Ana model tipi seçiniz")
                        Exit Sub
                    End If
                End If

                If Trim(ComboBoxEdit4.Text) = "" Then
                    MsgBox("Üretim ülkesi seçiniz")
                    Exit Sub
                End If

                If CheckEdit3.Checked Then
                    If Trim(ComboBoxEdit6.Text) = "" Or Trim(ComboBoxEdit6.Text) = "YOK" Then
                        MsgBox("Rota şablonu seçiniz")
                        Exit Sub
                    End If
                End If

                If Trim(ComboBoxEdit12.Text) = "" Then
                    MsgBox("Üretim yeri (firması) seçiniz")
                    Exit Sub
                End If
            End If

            oSQLServer.init(nDatabase)
            oSQLServer.OpenConn()

            oSQLServer.cSQLQuery = "select top 1 kullanicisipno " +
                                    " from siparis with (NOLOCK) " +
                                    " where kullanicisipno = '" + Trim(TextEdit1.Text) + "' "

            If oSQLServer.CheckExists Then
                oSQLServer.CloseConn()
                MsgBox(Trim(TextEdit1.Text) + " sipariş numarası kullanılmış başka sipariş no yazınız")
                Exit Sub
            End If

            oSQLServer.cSQLQuery = "select top 1 siparisno " +
                                    " from rpa_siparis with (NOLOCK) " +
                                    " where siparisno = '" + Trim(TextEdit1.Text) + "' "

            If oSQLServer.CheckExists Then
                oSQLServer.CloseConn()
                MsgBox(Trim(TextEdit1.Text) + " RPA sipariş numarası kullanılmış başka sipariş no yazınız")
                Exit Sub
            End If

            If CheckEdit1.Checked Then

                TextEdit4.Text = TextEdit7.Text
                ComboBoxEdit6.Text = "YOK"

                oSQLServer.cSQLQuery = "select top 1 modelno " +
                                    " from ymodel with (NOLOCK) " +
                                    " where modelno = '" + Trim(TextEdit4.Text) + "' "

                If Not oSQLServer.CheckExists Then
                    oSQLServer.CloseConn()
                    MsgBox(Trim(TextEdit4.Text) + " modeli bulunamadi")
                    Exit Sub
                End If
            End If

            If CheckEdit2.Checked Or CheckEdit3.Checked Then

                oSQLServer.cSQLQuery = "select top 1 modelno " +
                                    " from ymodel with (NOLOCK) " +
                                    " where modelno = '" + Trim(TextEdit4.Text) + "' "

                If oSQLServer.CheckExists Then
                    oSQLServer.CloseConn()
                    MsgBox(Trim(TextEdit4.Text) + " kullanılmış başka yeni model no seçiniz")
                    Exit Sub
                End If
            End If

            oSQLServer2.init(nDatabase)
            oSQLServer2.OpenConn()

            'cAppPath = oSQLServer.GetSysPar("pathofshare")
            'If Mid(Trim(cAppPath), 1, 1) <> "\" Then cAppPath = cAppPath & "\"
            'cAppPath = cAppPath + "rpasiparis"

            cAppPath = "c:\rpasiparis"
            DosCreateDirectory(cAppPath)

            cFileName = cAppPath + "\" + Trim(TextEdit1.Text) + ".msg"

            '        oMail.SaveAs(cFileName, OlSaveAsType.olMSG)

            ' first stage

            ' Delete existing records for this order

            oSQLServer2.cSQLQuery = "delete rpa_siparis " +
                                    " where siparisno = '" + Trim(TextEdit1.Text) + "' "
            oSQLServer2.SQLExecute()

            oSQLServer2.cSQLQuery = "delete rpa_sipmodel " +
                                    " where siparisno = '" + Trim(TextEdit1.Text) + "' "
            oSQLServer2.SQLExecute()

            oSQLServer2.cSQLQuery = "delete rpa_sipsertifika " +
                                    " where siparisno = '" + Trim(TextEdit1.Text) + "' "
            oSQLServer2.SQLExecute()

            oSQLServer2.cSQLQuery = "delete rpa_siponsiparis " +
                                    " where siparisno = '" + Trim(TextEdit1.Text) + "' "
            oSQLServer2.SQLExecute()

            ' insert new records for this order

            oSQLServer2.cSQLQuery = "insert rpa_siparis (siparisno, siparistarihi, musterino, rota, modelno, " +
                                    " calismano, uretimyeri, uretimulkesi, musterimodelno, musterisiparisno, " +
                                    " eskimodelno, musterigrubu, maildosyasi, createuser, creationdate, " +
                                    " eskimodel, modelkopyala, yenimodel, anamodeltipi, robotokudu  ) "

            oSQLServer2.cSQLQuery = oSQLServer2.cSQLQuery +
                        " values ('" + SQLWriteString(TextEdit1.Text, 30) + "', " +
                        " getdate(), " +
                        " '" + SQLWriteString(ComboBoxEdit1.Text, 30) + "', " +
                        " '" + SQLWriteString(ComboBoxEdit6.Text, 30) + "', " +
                        " '" + SQLWriteString(TextEdit4.Text, 30) + "', "

            oSQLServer2.cSQLQuery = oSQLServer2.cSQLQuery +
                        " '" + SQLWriteString(ComboBoxEdit6.Text, 30) + "', " +
                        " '" + SQLWriteString(ComboBoxEdit12.Text, 30) + "', " +
                        " '" + SQLWriteString(ComboBoxEdit4.Text, 30) + "', " +
                        " '" + SQLWriteString(oSiparis.style_no.ToString.Trim, 30) + "', " +
                        " '" + SQLWriteString(oSiparis.order_no.ToString.Trim, 30) + "', "

            oSQLServer2.cSQLQuery = oSQLServer2.cSQLQuery +
                        " '" + SQLWriteString(TextEdit7.Text, 30) + "', " +
                        " '" + SQLWriteString(TextEdit5.Text, 30) + "', " +
                        " '" + SQLWriteString(cFileName, 254) + "', " +
                        " '" + SQLWriteString(cPersonel, 30) + "', " +
                        " getdate() , "

            oSQLServer2.cSQLQuery = oSQLServer2.cSQLQuery +
                        " '" + cEskiModel + "', " +
                        " '" + cModelKopyala + "', " +
                        " '" + cYeniModel + "', " +
                        " '" + SQLWriteString(TextEdit8.Text, 30) + "' , " +
                        " 'E' ) "

            oSQLServer2.SQLExecute()

            ' second stage
            If oSiparis.fabric_content IsNot Nothing AndAlso oSiparis.fabric_content.Length > 0 Then
                cFabric1 = oSiparis.fabric_content(0)
                If oSiparis.fabric_content.Length > 1 Then cFabric2 = oSiparis.fabric_content(1)
            End If

            oSQLServer2.cSQLQuery = "set dateformat dmy " +
                                    " UPDATE rpa_siparis SET " +
                                    " ai_date                  = '" & SQLWriteDate(oSiparis.date) & "' , " &
                                    " ai_season                = '" & SQLWriteString(oSiparis.season, 100) & "' , " &
                                    " ai_production_group      = '" & SQLWriteString(oSiparis.production_group, 100) & "' , " &
                                    " ai_production_main_group = '" & SQLWriteString(oSiparis.production_main_group, 100) & "' , " &
                                    " ai_gender                = '" & SQLWriteString(oSiparis.gender, 100) & "' , " &
                                    " ai_barcode_item_nr       = '" & SQLWriteString(oSiparis.barcode_item_nr, 100) & "' , " &
                                    " ai_agent_name            = '" & SQLWriteString(oSiparis.agent_name, 100) & "' , " &
                                    " ai_agent_code            = '" & SQLWriteString(oSiparis.agent_code, 100) & "' , " &
                                    " ai_preorder_po           = '" & SQLWriteString(oSiparis.preorder_po, 100) & "' , " &
                                    " ai_currency_code         = '" & SQLWriteString(oSiparis.currency_code, 100) & "' , " &
                                    " ai_unit_price            =  " & SQLWriteDecimal(oSiparis.unit_price) & " , " &
                                    " ai_total_amount          =  " & SQLWriteDecimal(oSiparis.total_amount) & " , " &
                                    " ai_fabric_content1       = '" & SQLWriteString(cFabric1, 100) & "' , " &
                                    " ai_fabric_content2       = '" & SQLWriteString(cFabric2, 100) & "' , " &
                                    " ai_address               = '" & SQLWriteString(oSiparis.address, 500) & "' , " &
                                    " ai_supplier              = '" & SQLWriteString(oSiparis.supplier, 100) & "' , " &
                                    " ai_supplier_code         = '" & SQLWriteString(oSiparis.supplier_code, 100) & "' , " &
                                    " ai_style_no              = '" & SQLWriteString(oSiparis.style_no, 100) & "' , " &
                                    " ai_terms_of_del          = '" & SQLWriteString(oSiparis.terms_of_del, 100) & "' , " &
                                    " ai_shipment_mode         = '" & SQLWriteString(oSiparis.shipment_mode, 100) & "' , " &
                                    " ai_from                  = '" & SQLWriteString(oSiparis.from_, 100) & "' , " &
                                    " ai_to                    = '" & SQLWriteString(oSiparis.to_, 100) & "' , " &
                                    " ai_terms_of_payment      = '" & SQLWriteString(oSiparis.terms_of_payment, 100) & "' , " &
                                    " ai_foreign_amount        =  " & SQLWriteDecimal(oSiparis.foreign_amount) & " , " &
                                    " ai_domestic_amount       =  " & SQLWriteDecimal(oSiparis.domestic_amount) & " , " &
                                    " ai_supplier_address      = '" & SQLWriteString(oSiparis.supplier_address, 500) & "' , " &
                                    " ai_color_no              = '" & SQLWriteString(oSiparis.color_no, 100) & "' , " &
                                    " ai_color                 = '" & SQLWriteString(oSiparis.color, 100) & "' " &
                                    " where siparisno = '" & TextEdit1.Text.Trim & "' "

            oSQLServer2.SQLExecute()

            If IsDate(oSiparis.delivery_date_1) Then

                oSQLServer2.cSQLQuery = "set dateformat dmy " &
                                        " UPDATE rpa_siparis SET " &
                                        " ai_delivery_date_1 = '" & SQLWriteDate(oSiparis.delivery_date_1) & "' " &
                                        " where siparisno = '" & TextEdit1.Text.Trim & "' "
                oSQLServer2.SQLExecute()
            End If

            If IsDate(oSiparis.delivery_date_2) Then

                oSQLServer2.cSQLQuery = "set dateformat dmy " &
                                        " UPDATE rpa_siparis SET " &
                                        " ai_delivery_date_2 = '" & SQLWriteDate(oSiparis.delivery_date_2) & "' " &
                                        " where siparisno = '" & TextEdit1.Text.Trim & "' "
                oSQLServer2.SQLExecute()
            End If

            ' sertifikaları yaz

            If ListView1.Items.Count >= 1 Then

                For Each oItem As ListViewItem In ListView1.Items

                    If oItem.Text.Trim <> "" Then

                        oSQLServer2.cSQLQuery = "insert rpa_sipsertifika (siparisno, sertifikatipi) " &
                                                " values ('" + SQLWriteString(TextEdit1.Text, 30) + "', " &
                                                " '" + oItem.Text.Trim + "' ) "
                        oSQLServer2.SQLExecute()
                    End If
                Next
            End If

            ' ön siparişleri yaz

            If ListView2.Items.Count >= 1 Then

                For Each oItem As ListViewItem In ListView2.Items

                    If oItem.Text.Trim <> "" And oItem.SubItems(1).Text.Trim <> "" Then

                        oSQLServer2.cSQLQuery = "insert rpa_siponsiparis (siparisno, onsiparisno, onsiparisulke2sirano) " &
                                                " values ('" + SQLWriteString(TextEdit1.Text, 30) + "', " &
                                                " '" + oItem.Text.Trim + "', " &
                                                oItem.SubItems(1).Text.Trim + " ) "
                        oSQLServer2.SQLExecute()
                    End If
                Next
            End If

            ' Process assortments and save to database
            If oSiparis.xassortments IsNot Nothing AndAlso oSiparis.xassortments.Length > 0 Then

                ' Insert new assortments - flatten the 2D data into individual records
                For Each assortmentTable As AssortmentTable In oSiparis.xassortments

                    cDestination_code = assortmentTable.Destination_code.ToString.Trim
                    cDestination_description = assortmentTable.Destination_description.ToString.Trim
                    dDelivery_date = CDate(assortmentTable.Delivery_date)
                    cAssortment_code = assortmentTable.assortment_code.ToString.Trim
                    cPayment_term = assortmentTable.payment_term.ToString.Trim
                    nTotal = assortmentTable.TotalRowTotal
                    nMaxRows = assortmentTable.Quantities.GetUpperBound(0)


                    ReDim aBeden(0)
                    nCnt = -1
                    For nCol = 0 To assortmentTable.ColumnHeaders.GetUpperBound(0)
                        If assortmentTable.ColumnHeaders(nCol) <> "" _
                            And assortmentTable.ColumnHeaders(nCol) <> "TOTAL" _
                            And assortmentTable.ColumnHeaders(nCol) <> "TOPLAM" Then
                            ' Only add unique sizes to the array
                            nCnt += 1
                            ReDim Preserve aBeden(nCnt)
                            aBeden(nCnt) = assortmentTable.ColumnHeaders(nCol)
                        End If
                    Next
                    nMaxCols = aBeden.GetUpperBound(0)

                    For nRow = 0 To nMaxRows
                        For nCol = 0 To nMaxCols
                            cBedenBoy = assortmentTable.SizeNames(nRow).ToString.Trim
                            cBedenBel = aBeden(nCol).ToString.Trim
                            cBeden = cBedenBel + "/" + cBedenBoy
                            nAdet = assortmentTable.Quantities(nRow, nCol)

                            'If nAdet > 0 Then
                            oSQLServer2.cSQLQuery = "insert rpa_sipmodel (siparisno, renk, beden, adet, ilksevktar, ambalaj, tasimasekli, musterisiparisno) " +
                                                " values ('" + SQLWriteString(TextEdit1.Text, 30) + "', " +
                                                " '" + SQLWriteString(oSiparis.color_no.ToString.Trim, 30) + "', " +
                                                " '" + SQLWriteString(cBeden, 30) + "', " +
                                                SQLWriteDecimal(nAdet) + ", " +
                                                " '" + SQLWriteDate(assortmentTable.Delivery_date) + "', " +
                                                " '" + SQLWriteString("", 30) + "', " +
                                                " '" + SQLWriteString(oSiparis.shipment_mode.ToString.Trim, 30) + "', " +
                                                " '" + SQLWriteString(oSiparis.order_no.ToString.Trim, 30) + "' ) "

                            oSQLServer2.SQLExecute()
                            'End If
                        Next
                    Next
                Next
            End If

            oSQLServer2.CLRExecute("RPASiparis2")

            oSQLServer.CloseConn()
            oSQLServer2.CloseConn()

            ' send mail to robot
            'RPASendMail

            MsgBox(Trim(TextEdit1.Text) + " siparişi WinTex programına aktarıldı" + vbCrLf + "Lütfen WinTex ten kontrolünüzü yapınız")

            EmptyFields()

            ' exit
            'Me.Close()

        Catch ex As System.Exception
            ErrDisp(ex.Message, "Save",,, ex)
        End Try
    End Sub

    Private Sub WebBrowser1_Navigating(sender As Object, e As WebBrowserNavigatingEventArgs) Handles WebBrowser1.Navigating
        Try
            ' Only allow about:blank and data: URLs, prevent all other navigation
            If Not (e.Url.ToString().StartsWith("about:") OrElse e.Url.ToString().StartsWith("data:")) Then
                e.Cancel = True
            End If
        Catch ex As System.Exception
            ErrDisp(ex.Message, "WebBrowser1_Navigating", , , ex)
        End Try
    End Sub

    Private Sub WebBrowser1_NewWindow(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles WebBrowser1.NewWindow
        Try
            ' Prevent new windows from opening
            e.Cancel = True
        Catch ex As System.Exception
            ErrDisp(ex.Message, "WebBrowser1_NewWindow", , , ex)
        End Try
    End Sub

    Private Sub WebBrowserReset()
        ' WebBrowser1 is DevExpress/WinForms WebBrowser (IE-based)
        With WebBrowser1
            .Stop()
            .Navigate("about:blank")
        End With
    End Sub

    Private Async Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        ' send to AI
        Try
            Dim cMessage As String = ""
            Dim htmlContent As String = ""

            If ComboBoxEdit1.Text.Trim = "" Then
                MsgBox("Lütfen müşteri firmayı seçiniz")
                Exit Sub
            End If

            If pdfPath.Trim = "" Then
                MsgBox("Lütfen PDF dosyasını seçiniz")
                Exit Sub
            End If

            ' Test API connectivity
            If Not TestApiConnectivity(cMessage) Then
                MsgBox("Error API connectivity test failed. Check API key and endpoint. " + cMessage)
                Exit Sub
            End If

            WebBrowserReset()

            ' Convert PDF to JSON using Google Gemini AI
            Stage2(cFileName, pdfPath, cFinalJson, ComboBoxEdit1.Text.Trim, nDatabase)

            oSiparis = OrderDeserializeAndConvert(cFinalJson)

            If IsNothing(oSiparis) Then
                MsgBox("JSON verisi geçersiz veya eksik")
                Exit Sub
            End If

            'htmlContent = ConvertJsonToHtmlWithGemini(cFinalJson)
            htmlContent = PurchaseOrderRenderer.RenderPurchaseOrderHtml(oSiparis)

            ' Display the HTML content in WebBrowser
            If Not String.IsNullOrEmpty(htmlContent) Then
                Await LoadHtmlAsync(WebBrowser1, htmlContent)
            Else
                WebBrowser1.DocumentText = "<html><body><h2>Error: Could not convert JSON to HTML</h2></body></html>"
            End If

            MsgBox("JSON verisi başarıyla AI'ye gönderildi ve HTML çıktısı alındı" + vbCrLf + "Eğer sonuç görüntü kaynak PDF ile aynı değilse Yapay Zeka tuşuna bir kez daha tıklayınız", MsgBoxStyle.Information, "Başarılı")

        Catch ex As System.Exception
            ErrDisp(ex.Message, "SimpleButton4_Click", , , ex)
        End Try
    End Sub

    Private Sub EmptyFields()
        Try
            ' Prevent cascading events while resetting
            lCBLoad = True

            ' Clear internal state
            nDatabase = 0
            nMode = 0
            cFilter = ""
            cInputFilter = ""
            cFileName = ""
            pdfPath = ""
            cFinalJson = ""
            oSiparis = Nothing

            ' Reset check options
            CheckEdit1.Checked = False
            CheckEdit2.Checked = False
            CheckEdit3.Checked = False

            ' Reset text inputs
            TextEdit1.Text = ""  ' New order no
            TextEdit2.Text = ""  ' Selected mail subject/info
            TextEdit3.Text = ""  ' PDF path
            TextEdit4.Text = ""  ' New model no
            TextEdit5.Text = ""  ' ozelalan3
            TextEdit6.Text = ""  ' Ön maliyet
            TextEdit7.Text = ""  ' Old model no
            TextEdit8.Text = ""  ' Ana model tipi

            ' Reset combo boxes (text only; items will be (re)loaded by selection of country)
            ComboBoxEdit1.Text = ""  ' Customer
            ComboBoxEdit4.Text = ""  ' Country
            ComboBoxEdit6.Text = ""  ' Rota şablonu
            ComboBoxEdit12.Text = "" ' Üretim yeri

            ' Clear list views
            ListView1.Items.Clear()   ' Certificates
            ListView2.Items.Clear()   ' Preorders

            ' Reset PDF viewer and web browser
            Try
                If PdfViewer1 IsNot Nothing Then
                    PdfViewer1.CloseDocument()
                End If
            Catch
                ' ignore viewer reset errors
            End Try

            WebBrowserReset()

            ComboBoxEdit6.Enabled = True

            TextEdit4.Enabled = True
            TextEdit7.Enabled = True
            TextEdit8.Enabled = True

            SimpleButton10.Enabled = True
            SimpleButton13.Enabled = True

            lCBLoad = False

        Catch ex As Exception
            ErrDisp(ex.Message, "EmptyFields", , , ex)
        End Try
    End Sub
End Class

