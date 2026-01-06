Option Explicit On

Public Class SelectFromListbox

    Dim nMode As Integer = 1
    Dim cInputFilter As String = ""
    Dim cFilter As String = ""
    Dim nDatabase As Integer = 1
    Dim nRecordCount As Integer = 0

    ' Pagination variables
    Dim nCurrentPage As Integer = 1
    Dim nPageSize As Integer = 200
    Dim nTotalRecords As Long = 0
    Dim nTotalPages As Integer = 0
    Dim lIsLoading As Boolean = False
    
    ' Data storage for current page (for virtual mode)
    Private currentPageData As New List(Of ListViewItem)

    ' Performance helpers
    Private columnsConfigured As Boolean = False

    Public Sub init(Optional nCase As Integer = 1, Optional cInputFilter1 As String = "", Optional nDatabase1 As Integer = 1)
        Try
            nMode = nCase
            cInputFilter = cInputFilter1
            nDatabase = nDatabase1
            cFilter = ""
            G_Selection = ""
            G_Selection2 = ""

            ' Reset pagination
            nCurrentPage = 1
            nTotalRecords = 0
            nTotalPages = 0
            currentPageData.Clear()
            columnsConfigured = False

            Me.ShowDialog()

        Catch ex As Exception
            ErrDisp(ex.Message, "init", , , ex)
        End Try
    End Sub

    Private Sub SelectFromListbox_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ConfigureListView()
            ConfigurePaginationControls()
            GetTotalRecordCount()
            LoadCurrentPage()

        Catch ex As Exception
            ErrDisp(ex.Message, "SelectFromListbox_Load", , , ex)
        End Try
    End Sub

    Private Sub ConfigureListView()
        Try
            ListView1.View = System.Windows.Forms.View.Details
            ListView1.FullRowSelect = True
            ListView1.MultiSelect = False
            ListView1.GridLines = True
            ListView1.LabelEdit = False
            ListView1.AllowColumnReorder = False
            ' Remove virtual mode to use regular pagination
            ListView1.VirtualMode = False

            ' Enable double buffering to reduce flicker and speed up painting
            Try
                Dim prop = ListView1.GetType().GetProperty("DoubleBuffered", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
                If prop IsNot Nothing Then
                    prop.SetValue(ListView1, True, Nothing)
                End If
            Catch
                ' ignore if reflection fails
            End Try

        Catch ex As Exception
            ErrDisp(ex.Message, "ConfigureListView", , , ex)
        End Try
    End Sub

    Private Sub ConfigurePaginationControls()
        Try
            UpdatePaginationUI()
        Catch ex As Exception
            ErrDisp(ex.Message, "ConfigurePaginationControls", , , ex)
        End Try
    End Sub

    Private Sub GetTotalRecordCount()
        Try
            lIsLoading = True
            Dim oSQL As New SQLServerClass

            oSQL.init(nDatabase)
            oSQL.OpenConn()

            Dim cCountSQL As String = GetCountQuery()
            If cCountSQL.Trim <> "" Then
                nTotalRecords = oSQL.DBReadInteger(cCountSQL)
                nTotalPages = CInt(Math.Ceiling(nTotalRecords / CDbl(nPageSize)))
            End If

            oSQL.CloseConn()
            UpdatePaginationUI()
            lIsLoading = False

        Catch ex As Exception
            ErrDisp(ex.Message, "GetTotalRecordCount", , , ex)
            lIsLoading = False
        End Try
    End Sub

    Private Function GetCountQuery() As String

        GetCountQuery = ""

        Try

            Select Case nMode
                Case 1
                    GetCountQuery = "SELECT COUNT(*) " +
                        " FROM maliyetheader WITH (NOLOCK) " +
                        " WHERE (dosyakapandi IS NULL OR dosyakapandi = '' OR dosyakapandi = 'H') " +
                        " AND (iptal IS NULL OR iptal = '' OR iptal = 'H') " +
                        " AND (kilitle IS NULL OR kilitle = '' OR kilitle = 'H') " +
                        " AND musteri = '" + Trim$(cInputFilter) + "' " +
                        " AND (nihaimaliyet IS NULL OR nihaimaliyet = 'H' OR nihaimaliyet = '') " +
                        cFilter

                Case 2
                    GetCountQuery = "SELECT COUNT(DISTINCT c.modelno) " +
                        " FROM siparis a WITH (NOLOCK), sipmodel b WITH (NOLOCK), ymodel c WITH (NOLOCK) " +
                        " WHERE a.kullanicisipno = b.siparisno " +
                        " AND b.modelno = c.modelno " +
                        " AND a.musterino = '" + Trim(cInputFilter) + "' " +
                        cFilter

                Case 3
                    Dim cOzelAlan3 As String = GetOzelAlan3()
                    If Trim(cOzelAlan3) = "" Then
                        GetCountQuery = "SELECT COUNT(DISTINCT a.OnSiparisNo + CAST(b.SiraNo AS VARCHAR)) " +
                            " FROM onsiparis a WITH (NOLOCK), onsiparisulke2 b WITH (NOLOCK), onsiparisonayistek d WITH (NOLOCK) " +
                            " WHERE a.musterino = '" + Trim(cInputFilter) + "' " +
                            " AND a.onsiparisno = b.onsiparisno " +
                            " AND b.onsiparisno = d.onsiparisno " +
                            " AND d.onaylandi = 'E' " +
                            " AND a.onsiparisno IS NOT NULL " +
                            " AND a.onsiparisno <> '' " +
                            " AND (a.iptal IS NULL OR a.iptal = 'H' OR a.iptal = '') " +
                            cFilter
                    Else
                        GetCountQuery = "SELECT COUNT(DISTINCT a.OnSiparisNo + CAST(b.sirano AS VARCHAR)) " +
                            " FROM onsiparis a WITH (NOLOCK), onsiparisulke2 b WITH (NOLOCK), firma c WITH (NOLOCK), onsiparisonayistek d WITH (NOLOCK) " +
                            " WHERE a.musterino = c.firma " +
                            " AND c.ozelalan3 = '" + Trim(cOzelAlan3) + "' " +
                            " AND a.onsiparisno = b.onsiparisno " +
                            " AND b.onsiparisno = d.onsiparisno " +
                            " AND d.onaylandi = 'E' " +
                            " AND a.onsiparisno IS NOT NULL " +
                            " AND a.onsiparisno <> '' " +
                            " AND (a.iptal IS NULL OR a.iptal = 'H' OR a.iptal = '') " +
                            cFilter
                    End If

                Case 4
                    GetCountQuery = "SELECT COUNT(*) " +
                        " FROM sertifikatipi WITH (NOLOCK) " +
                        " WHERE sertifikatipi IS NOT NULL " +
                        " AND sertifikatipi <> '' " +
                        cFilter

                Case 5
                    GetCountQuery = "SELECT COUNT(*) " +
                        " FROM anamodeltipi WITH (NOLOCK) " +
                        " WHERE anamodeltipi IS NOT NULL " +
                        " AND anamodeltipi <> '' " +
                        cFilter
            End Select

        Catch ex As Exception
            ErrDisp(ex.Message, "GetCountQuery", , , ex)
        End Try
    End Function

    Private Function GetOzelAlan3() As String

        GetOzelAlan3 = ""

        Try
            Dim oSQL As New SQLServerClass

            oSQL.init(nDatabase)
            oSQL.OpenConn()

            oSQL.cSQLQuery = "SELECT TOP 1 ozelalan3 " +
                " FROM firma WITH (NOLOCK) " +
                " WHERE firma = '" + Trim(cInputFilter) + "' "

            GetOzelAlan3 = oSQL.DBReadString()
            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp(ex.Message, "GetOzelAlan3", , , ex)
        End Try
    End Function

    Private Sub LoadCurrentPage()
        Try
            If lIsLoading Then Exit Sub
            lIsLoading = True

            ' Only configure columns once per session/mode
            If Not columnsConfigured Then
                SetupColumnsForMode()
                columnsConfigured = True
            End If

            ' Build items in memory first for speed, then add in bulk
            Dim pageItems As New List(Of ListViewItem)(nPageSize)

            Dim oSQL As New SQLServerClass
            oSQL.init(nDatabase)
            oSQL.OpenConn()
            
            Dim cPagedSQL As String = GetPagedQuery()
            If cPagedSQL.Trim <> "" Then
                oSQL.cSQLQuery = cPagedSQL
                oSQL.GetSQLReader()
                
                BuildItemsFromReader(oSQL, pageItems)
                
                oSQL.oReader.Close()
            End If
            
            oSQL.CloseConn()

            ' Apply to ListView in one shot for speed
            ListView1.BeginUpdate()
            Try
                ListView1.Items.Clear()
                If pageItems.Count > 0 Then
                    ListView1.Items.AddRange(pageItems.ToArray())
                End If
                currentPageData = pageItems
            Finally
                ListView1.EndUpdate()
            End Try

            UpdatePaginationUI()
        Catch ex As Exception
            ErrDisp(ex.Message, "LoadCurrentPage", , , ex)
        Finally
            lIsLoading = False
        End Try
    End Sub

    Private Function GetPagedQuery() As String

        GetPagedQuery = ""

        Try
            Dim nOffset As Integer = (nCurrentPage - 1) * nPageSize

            Select Case nMode
                Case 1
                    GetPagedQuery = "SELECT calismano, grubu, modelno, modelaciklama, tarih, musteritemsilcisi " +
                        " FROM maliyetheader WITH (NOLOCK) " +
                        " WHERE (dosyakapandi IS NULL OR dosyakapandi = '' OR dosyakapandi = 'H') " +
                        " AND (iptal IS NULL OR iptal = '' OR iptal = 'H') " +
                        " AND (kilitle IS NULL OR kilitle = '' OR kilitle = 'H') " +
                        " AND musteri = '" + Trim$(cInputFilter) + "' " +
                        " AND (nihaimaliyet IS NULL OR nihaimaliyet = 'H' OR nihaimaliyet = '') " +
                        cFilter +
                        " ORDER BY calismano " +
                        " OFFSET " + nOffset.ToString() + " ROWS " +
                        " FETCH NEXT " + nPageSize.ToString() + " ROWS ONLY"

                Case 2
                    GetPagedQuery = "SELECT DISTINCT c.modelno, c.aciklama, c.musterimodelno, c.anamodeltipi " +
                        " FROM siparis a WITH (NOLOCK), sipmodel b WITH (NOLOCK), ymodel c WITH (NOLOCK) " +
                        " WHERE a.kullanicisipno = b.siparisno " +
                        " AND b.modelno = c.modelno " +
                        " AND a.musterino = '" + Trim(cInputFilter) + "' " +
                        cFilter +
                        " ORDER BY c.modelno " +
                        " OFFSET " + nOffset.ToString() + " ROWS " +
                        " FETCH NEXT " + nPageSize.ToString() + " ROWS ONLY"

                Case 3
                    Dim cOzelAlan3 As String = GetOzelAlan3()
                    If Trim(cOzelAlan3) = "" Then
                        GetPagedQuery = "SELECT DISTINCT a.OnSiparisNo, b.SiraNo, a.musterino, a.tarih, a.Marka, a.UrunGrubu, " +
                            " a.ModelNo, a.Kumas, a.Renk, a.Yikama, a.BirimMt, a.Tedarikci, a.Kapandi, " +
                            " b.ulke, b.termin, b.plkesimtarihi, b.teyittarihi, b.kumasmiktar, b.kumastermin, " +
                            " OnSiparisAdet = b.adet, " +
                            " BagliSiparisAdet = (SELECT SUM(COALESCE(y.adet,0)) " +
                                                " FROM siparis x WITH (NOLOCK), sipmodel y WITH (NOLOCK) " +
                                                " WHERE x.kullanicisipno = y.siparisno " +
                                                " AND x.onsiparisno = a.OnSiparisNo " +
                                                " AND x.onsiparisulke2sirano = b.sirano) " +
                            " FROM onsiparis a WITH (NOLOCK), onsiparisulke2 b WITH (NOLOCK), onsiparisonayistek d WITH (NOLOCK) " +
                            " WHERE a.musterino = '" + Trim(cInputFilter) + "' " +
                            " AND a.onsiparisno = b.onsiparisno " +
                            " AND b.onsiparisno = d.onsiparisno " +
                            " AND d.onaylandi = 'E' " +
                            " AND a.onsiparisno IS NOT NULL " +
                            " AND a.onsiparisno <> '' " +
                            " AND (a.iptal IS NULL OR a.iptal = 'H' OR a.iptal = '') " +
                            cFilter +
                            " ORDER BY a.tarih DESC, a.OnSiparisNo DESC, b.SiraNo DESC " +
                            " OFFSET " + nOffset.ToString() + " ROWS " +
                            " FETCH NEXT " + nPageSize.ToString() + " ROWS ONLY"
                    Else
                        GetPagedQuery = "SELECT DISTINCT a.OnSiparisNo, b.sirano, a.musterino, a.tarih, a.Marka, a.UrunGrubu, " +
                            " a.ModelNo, a.Kumas, a.Renk, a.Yikama, a.BirimMt, a.Tedarikci, a.Kapandi, " +
                            " b.ulke, b.termin, b.plkesimtarihi, b.teyittarihi, b.kumasmiktar, b.kumastermin, " +
                            " OnSiparisAdet = b.adet, " +
                            " BagliSiparisAdet = (SELECT SUM(COALESCE(y.adet,0)) " +
                                                " FROM siparis x WITH (NOLOCK), sipmodel y WITH (NOLOCK) " +
                                                " WHERE x.kullanicisipno = y.siparisno " +
                                                " AND x.onsiparisno = a.OnSiparisNo " +
                                                " AND x.onsiparisulke2sirano = b.sirano) " +
                            " FROM onsiparis a WITH (NOLOCK), onsiparisulke2 b WITH (NOLOCK), firma c WITH (NOLOCK), onsiparisonayistek d WITH (NOLOCK) " +
                            " WHERE a.musterino = c.firma " +
                            " AND c.ozelalan3 = '" + Trim(cOzelAlan3) + "' " +
                            " AND a.onsiparisno = b.onsiparisno " +
                            " AND b.onsiparisno = d.onsiparisno " +
                            " AND d.onaylandi = 'E' " +
                            " AND a.onsiparisno IS NOT NULL " +
                            " AND a.onsiparisno <> '' " +
                            " AND (a.iptal IS NULL OR a.iptal = 'H' OR a.iptal = '') " +
                            cFilter +
                            " ORDER BY a.tarih DESC, a.OnSiparisNo DESC, b.sirano DESC " +
                            " OFFSET " + nOffset.ToString() + " ROWS " +
                            " FETCH NEXT " + nPageSize.ToString() + " ROWS ONLY"
                    End If

                Case 4
                    GetPagedQuery = "SELECT sertifikatipi, aciklama " +
                        " FROM sertifikatipi WITH (NOLOCK) " +
                        " WHERE sertifikatipi IS NOT NULL " +
                        " AND sertifikatipi <> '' " +
                        cFilter +
                        " ORDER BY sertifikatipi " +
                        " OFFSET " + nOffset.ToString() + " ROWS " +
                        " FETCH NEXT " + nPageSize.ToString() + " ROWS ONLY"

                Case 5
                    GetPagedQuery = "SELECT anamodeltipi, aciklama " +
                        " FROM anamodeltipi WITH (NOLOCK) " +
                        " WHERE anamodeltipi IS NOT NULL " +
                        " AND anamodeltipi <> '' " +
                        cFilter +
                        " ORDER BY anamodeltipi " +
                        " OFFSET " + nOffset.ToString() + " ROWS " +
                        " FETCH NEXT " + nPageSize.ToString() + " ROWS ONLY"
            End Select

        Catch ex As Exception
            ErrDisp(ex.Message, "GetPagedQuery", , , ex)
        End Try
    End Function

    Private Sub SetupColumnsForMode()
        Try
            ListView1.BeginUpdate()
            Try
                ListView1.Columns.Clear()
                
                Select Case nMode
                    Case 1
                        ListView1.Columns.Add("Ön Maliyet No", 100)
                        ListView1.Columns.Add("Grubu", 100)
                        ListView1.Columns.Add("Model No", 100)
                        ListView1.Columns.Add("Model Açıklama", 180)
                        ListView1.Columns.Add("Tarih", 100)
                        ListView1.Columns.Add("Müşteri Temsilcisi", 140)

                    Case 2
                        ListView1.Columns.Add("Model No", 200)
                        ListView1.Columns.Add("Açıklama", 260)
                        ListView1.Columns.Add("Müşteri Model No", 200)
                        ListView1.Columns.Add("Ana Model Tipi", 140)

                    Case 3
                        ListView1.Columns.Add("Ön Sipariş No", 120)
                        ListView1.Columns.Add("Satır No", 80)
                        ListView1.Columns.Add("Müşteri", 120)
                        ListView1.Columns.Add("Tarih", 100)
                        ListView1.Columns.Add("Marka", 120)
                        ListView1.Columns.Add("Ürün Grubu", 120)
                        ListView1.Columns.Add("Model No", 160)
                        ListView1.Columns.Add("Kumaş", 180)
                        ListView1.Columns.Add("Renk", 100)
                        ListView1.Columns.Add("Yıkama", 100)
                        ListView1.Columns.Add("Birim mt", 100)
                        ListView1.Columns.Add("Tedarikçi", 120)
                        ListView1.Columns.Add("Kapandı", 80)
                        ListView1.Columns.Add("Ülke", 100)
                        ListView1.Columns.Add("Termin", 100)
                        ListView1.Columns.Add("Pl.Kesim Tarihi", 120)
                        ListView1.Columns.Add("Teyit Tarihi", 120)
                        ListView1.Columns.Add("Kumaş Miktar", 120)
                        ListView1.Columns.Add("Kumaş Termin", 120)
                        ListView1.Columns.Add("Ön Sipariş Adet", 120)
                        ListView1.Columns.Add("Bağlı Siparişler Adet", 150)

                    Case 4
                        ListView1.Columns.Add("Sertifika Tipi", 240)
                        ListView1.Columns.Add("Açıklama", 260)

                    Case 5
                        ListView1.Columns.Add("Ana Model Tipi", 240)
                        ListView1.Columns.Add("Açıklama", 260)
                End Select
            Finally
                ListView1.EndUpdate()
            End Try

        Catch ex As Exception
            ErrDisp(ex.Message, "SetupColumnsForMode", , , ex)
        End Try
    End Sub

    Private Sub BuildItemsFromReader(oSQL As SQLServerClass, ByRef pageItems As List(Of ListViewItem))
        Try
            Select Case nMode
                Case 1
                    Do While oSQL.oReader.Read
                        Dim newItem As New ListViewItem(oSQL.SQLReadString("calismano"))
                        newItem.SubItems.Add(oSQL.SQLReadString("grubu"))
                        newItem.SubItems.Add(oSQL.SQLReadString("modelno"))
                        newItem.SubItems.Add(oSQL.SQLReadString("modelaciklama"))
                        newItem.SubItems.Add(FormatSafeDate(oSQL.SQLReadDate("tarih")))
                        newItem.SubItems.Add(oSQL.SQLReadString("musteritemsilcisi"))
                        pageItems.Add(newItem)
                    Loop

                Case 2
                    Do While oSQL.oReader.Read
                        Dim newItem As New ListViewItem(oSQL.SQLReadString("modelno"))
                        newItem.SubItems.Add(oSQL.SQLReadString("aciklama"))
                        newItem.SubItems.Add(oSQL.SQLReadString("musterimodelno"))
                        newItem.SubItems.Add(oSQL.SQLReadString("anamodeltipi"))
                        pageItems.Add(newItem)
                    Loop

                Case 3
                    Do While oSQL.oReader.Read
                        Dim newItem As New ListViewItem(oSQL.SQLReadString("OnSiparisNo"))
                        newItem.SubItems.Add(oSQL.SQLReadDouble("sirano").ToString("F0"))
                        newItem.SubItems.Add(oSQL.SQLReadString("musterino"))
                        newItem.SubItems.Add(FormatSafeDate(oSQL.SQLReadDate("tarih")))
                        newItem.SubItems.Add(oSQL.SQLReadString("marka"))
                        newItem.SubItems.Add(oSQL.SQLReadString("UrunGrubu"))
                        newItem.SubItems.Add(oSQL.SQLReadString("ModelNo"))
                        newItem.SubItems.Add(oSQL.SQLReadString("Kumas"))
                        newItem.SubItems.Add(oSQL.SQLReadString("Renk"))
                        newItem.SubItems.Add(oSQL.SQLReadString("Yikama"))
                        newItem.SubItems.Add(oSQL.SQLReadDouble("BirimMt").ToString("F2"))
                        newItem.SubItems.Add(oSQL.SQLReadString("Tedarikci"))
                        newItem.SubItems.Add(oSQL.SQLReadString("Kapandi"))
                        newItem.SubItems.Add(oSQL.SQLReadString("ulke"))
                        newItem.SubItems.Add(FormatSafeDate(oSQL.SQLReadDate("termin")))
                        newItem.SubItems.Add(FormatSafeDate(oSQL.SQLReadDate("plkesimtarihi")))
                        newItem.SubItems.Add(FormatSafeDate(oSQL.SQLReadDate("teyittarihi")))
                        newItem.SubItems.Add(oSQL.SQLReadDouble("kumasmiktar").ToString("F2"))
                        newItem.SubItems.Add(FormatSafeDate(oSQL.SQLReadDate("kumastermin")))
                        newItem.SubItems.Add(oSQL.SQLReadDouble("OnSiparisAdet").ToString("F0"))
                        newItem.SubItems.Add(oSQL.SQLReadDouble("BagliSiparisAdet").ToString("F0"))
                        pageItems.Add(newItem)
                    Loop

                Case 4
                    Do While oSQL.oReader.Read
                        Dim newItem As New ListViewItem(oSQL.SQLReadString("sertifikatipi"))
                        newItem.SubItems.Add(oSQL.SQLReadString("aciklama"))
                        pageItems.Add(newItem)
                    Loop

                Case 5
                    Do While oSQL.oReader.Read
                        Dim newItem As New ListViewItem(oSQL.SQLReadString("anamodeltipi"))
                        newItem.SubItems.Add(oSQL.SQLReadString("aciklama"))
                        pageItems.Add(newItem)
                    Loop
            End Select

        Catch ex As Exception
            ErrDisp(ex.Message, "BuildItemsFromReader", , , ex)
        End Try
    End Sub

    Private Function FormatSafeDate(dateValue As Date) As String
        Try
            If dateValue = #1/1/1950# OrElse dateValue = Date.MinValue Then
                Return ""
            Else
                Return dateValue.ToString("dd/MM/yyyy")
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Sub UpdatePaginationUI()
        Try
            ' Update page information label
            If Me.Controls.ContainsKey("lblPageInfo") Then
                Dim lblPageInfo As Label = CType(Me.Controls("lblPageInfo"), Label)
                lblPageInfo.Text = $"Sayfa {nCurrentPage} / {Math.Max(nTotalPages, 1)} (Toplam: {nTotalRecords:N0} kayıt)"
            End If

            ' Update navigation buttons
            If Me.Controls.ContainsKey("btnFirst") Then
                Dim btnFirst As Button = CType(Me.Controls("btnFirst"), Button)
                btnFirst.Enabled = nCurrentPage > 1
            End If

            If Me.Controls.ContainsKey("btnPrevious") Then
                Dim btnPrevious As Button = CType(Me.Controls("btnPrevious"), Button)
                btnPrevious.Enabled = nCurrentPage > 1
            End If

            If Me.Controls.ContainsKey("btnNext") Then
                Dim btnNext As Button = CType(Me.Controls("btnNext"), Button)
                btnNext.Enabled = nCurrentPage < nTotalPages
            End If

            If Me.Controls.ContainsKey("btnLast") Then
                Dim btnLast As Button = CType(Me.Controls("btnLast"), Button)
                btnLast.Enabled = nCurrentPage < nTotalPages
            End If

        Catch ex As Exception
            ErrDisp(ex.Message, "UpdatePaginationUI", , , ex)
        End Try
    End Sub

    ' Navigation helpers for simplicity
    Private Sub GoToPage(target As Integer)
        Try
            If target < 1 Then target = 1
            If nTotalPages > 0 AndAlso target > nTotalPages Then target = nTotalPages
            If target = nCurrentPage Then Return
            nCurrentPage = target
            LoadCurrentPage()
        Catch ex As Exception
            ErrDisp(ex.Message, "GoToPage", , , ex)
        End Try
    End Sub

    ' Navigation button events
    Private Sub btnFirst_Click(sender As Object, e As EventArgs)
        Try
            GoToPage(1)
        Catch ex As Exception
            ErrDisp(ex.Message, "btnFirst_Click", , , ex)
        End Try
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs)
        Try
            GoToPage(nCurrentPage - 1)
        Catch ex As Exception
            ErrDisp(ex.Message, "btnPrevious_Click", , , ex)
        End Try
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs)
        Try
            GoToPage(nCurrentPage + 1)
        Catch ex As Exception
            ErrDisp(ex.Message, "btnNext_Click", , , ex)
        End Try
    End Sub

    Private Sub btnLast_Click(sender As Object, e As EventArgs)
        Try
            GoToPage(nTotalPages)
        Catch ex As Exception
            ErrDisp(ex.Message, "btnLast_Click", , , ex)
        End Try
    End Sub

    ' Existing event handlers with error handling
    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Try
            SecimYapildi()
        Catch ex As Exception
            ErrDisp(ex.Message, "SimpleButton2_Click", , , ex)
        End Try
    End Sub

    Private Sub SecimYapildi()
        Try
            If ListView1.Items.Count > 0 Then
                If ListView1.SelectedIndices.Count > 0 Then
                    G_Selection = ListView1.SelectedItems(0).Text.Trim
                    If ListView1.SelectedItems(0).SubItems.Count > 1 Then
                        G_Selection2 = ListView1.SelectedItems(0).SubItems(1).Text.Trim
                    End If
                    Me.Close()
                End If
            End If

        Catch ex As Exception
            ErrDisp(ex.Message, "SecimYapildi", , , ex)
        End Try
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        Try
            G_Selection = ""
            G_Selection2 = ""
            Me.Close()
        Catch ex As System.Exception
            ErrDisp(ex.Message, "SimpleButton3_Click", , , ex)
        End Try
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Try
            ApplyFilter()
        Catch ex As Exception
            ErrDisp(ex.Message, "SimpleButton1_Click", , , ex)
        End Try
    End Sub

    Private Sub ApplyFilter()
        Try
            If Trim(TextEdit1.Text) = "" Then
                cFilter = ""
            Else
                Select Case nMode
                    Case 1
                        cFilter = " AND (calismano LIKE '%" + Trim(TextEdit1.Text) + "%' " +
                        " OR grubu LIKE '%" + Trim(TextEdit1.Text) + "%' " +
                        " OR modelno LIKE '%" + Trim(TextEdit1.Text) + "%' " +
                        " OR modelaciklama LIKE '%" + Trim(TextEdit1.Text) + "%' " +
                        " OR musteritemsilcisi LIKE '%" + Trim(TextEdit1.Text) + "%') "
                    Case 2
                        cFilter = " AND (c.modelno LIKE '%" + Trim(TextEdit1.Text) + "%' " +
                        " OR c.aciklama LIKE '%" + Trim(TextEdit1.Text) + "%' " +
                        " OR c.musterimodelno LIKE '%" + Trim(TextEdit1.Text) + "%' " +
                        " OR c.anamodeltipi LIKE '%" + Trim(TextEdit1.Text) + "%') "
                    Case 3
                        cFilter = " AND (a.OnSiparisNo LIKE '%" + Trim(TextEdit1.Text) + "%' " +
                        " OR a.Marka LIKE '%" + Trim(TextEdit1.Text) + "%' " +
                        " OR a.UrunGrubu LIKE '%" + Trim(TextEdit1.Text) + "%' " +
                        " OR a.ModelNo LIKE '%" + Trim(TextEdit1.Text) + "%' " +
                        " OR a.Kumas LIKE '%" + Trim(TextEdit1.Text) + "%' " +
                        " OR a.Renk LIKE '%" + Trim(TextEdit1.Text) + "%' " +
                        " OR a.Yikama LIKE '%" + Trim(TextEdit1.Text) + "%' " +
                        " OR a.Tedarikci LIKE '%" + Trim(TextEdit1.Text) + "%' " +
                        " OR b.ulke LIKE '%" + Trim(TextEdit1.Text) + "%') "
                    Case 4
                        cFilter = " AND (sertifikatipi LIKE '%" + Trim(TextEdit1.Text) + "%' " +
                        " OR aciklama LIKE '%" + Trim(TextEdit1.Text) + "%') "
                    Case 5
                        cFilter = " AND (anamodeltipi LIKE '%" + Trim(TextEdit1.Text) + "%' " +
                        " OR aciklama LIKE '%" + Trim(TextEdit1.Text) + "%') "
                End Select
            End If

            ' Reset to first page when applying filter
            nCurrentPage = 1
            GetTotalRecordCount()
            LoadCurrentPage()

        Catch ex As Exception
            ErrDisp(ex.Message, "ApplyFilter", , , ex)
        End Try
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        Try
            SecimYapildi()
        Catch ex As System.Exception
            ErrDisp(ex.Message, "ListView1_DoubleClick", , , ex)
        End Try
    End Sub

    ' Legacy method for compatibility - now returns the current page query
    Private Function GetSQLQuery() As String

        GetSQLQuery = ""

        Try
            GetSQLQuery = GetPagedQuery()
        Catch ex As Exception
            ErrDisp(ex.Message, "GetSQLQuery", , , ex)
        End Try
    End Function

    ' Keyboard navigation for quick page jumps
    Private Sub txtPageNumber_KeyPress(sender As Object, e As KeyPressEventArgs)
        Try
            ' Allow only numbers and control characters
            If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
                e.Handled = True
            End If
        Catch ex As Exception
            ErrDisp(ex.Message, "txtPageNumber_KeyPress", , , ex)
        End Try
    End Sub

    Private Sub txtPageNumber_KeyDown(sender As Object, e As KeyEventArgs)
        Try
            If e.KeyCode = Keys.Enter Then
                Dim txtPageNumber As TextBox = CType(sender, TextBox)
                Dim targetPage As Integer
                If Integer.TryParse(txtPageNumber.Text, targetPage) Then
                    If targetPage >= 1 AndAlso targetPage <= nTotalPages Then
                        nCurrentPage = targetPage
                        LoadCurrentPage()
                    Else
                        txtPageNumber.Text = nCurrentPage.ToString()
                    End If
                End If
            End If
        Catch ex As Exception
            ErrDisp(ex.Message, "txtPageNumber_KeyDown", , , ex)
        End Try
    End Sub

End Class