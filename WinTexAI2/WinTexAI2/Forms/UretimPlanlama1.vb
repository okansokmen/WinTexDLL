Option Explicit On
Option Strict On

Imports System.Data.SqlClient
Imports DevExpress.Utils
Imports DevExpress.XtraEditors.ColorWheel
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base ' For GridCell
Imports DevExpress.XtraEditors ' For XtraMessageBox
Imports Microsoft.Win32
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrintingLinks
Imports System.Drawing ' Added for coloring rows
Imports System.IO

Public Class UretimPlanlama1

    Private Const REG_LAYOUT_BASE As String = "WinTexAI2\\UretimPlanlama1" ' Stored under HKCU\Software\...
    Private Const REG_LAYOUT_MASTER As String = REG_LAYOUT_BASE & "\\Master"
    Private Const REG_LAYOUT_DETAIL As String = REG_LAYOUT_BASE & "\\Detail"
    Private Const REG_LAYOUT_DETAIL2 As String = REG_LAYOUT_BASE & "\\Detail2"
    Private Shared ReadOnly MIN_ALLOWED_DATE As New Date(2000, 1, 1)
    Private Shared ReadOnly MAX_ALLOWED_DATE As New Date(2100, 12, 31)

    Private ReadOnly _highlightRowHandles As New Dictionary(Of GridView, Integer)()
    Private ReadOnly _imageCache As New Dictionary(Of String, Image)(StringComparer.OrdinalIgnoreCase)

    Public Sub init()
        Try
            AddHandler Me.FormClosing, AddressOf UretimPlanlama1_FormClosing
            Me.Show()
        Catch ex As System.Exception
            ErrDisp(ex.Message, "UretimPlanlama1.init", , , ex)
        End Try
    End Sub

    Private Sub UretimPlanlama1_Load(sender As Object, e As EventArgs) Handles Me.Load

        Me.WindowState = FormWindowState.Maximized

        PopulateGrid()
    End Sub

    Private Sub SaveGridLayout(view As GridView, isDetail As Boolean)
        Try
            If view Is Nothing Then Exit Sub
            Dim key As String = REG_LAYOUT_MASTER
            Dim tagStr As String = TryCast(view.Tag, String)
            If String.Equals(tagStr, "MASTER", StringComparison.OrdinalIgnoreCase) Then
                key = REG_LAYOUT_MASTER
            ElseIf String.Equals(tagStr, "DETAIL2", StringComparison.OrdinalIgnoreCase) Then
                key = REG_LAYOUT_DETAIL2
            ElseIf String.Equals(tagStr, "DETAIL", StringComparison.OrdinalIgnoreCase) Then
                key = REG_LAYOUT_DETAIL
            Else
                ' Fallback to LevelName if available
                Try
                    Dim lvlProp = view.GetType().GetProperty("LevelName")
                    If lvlProp IsNot Nothing Then
                        Dim lvlName As String = Convert.ToString(lvlProp.GetValue(view, Nothing))
                        If String.Equals(lvlName, "Hareketler", StringComparison.OrdinalIgnoreCase) Then
                            key = REG_LAYOUT_DETAIL2
                        ElseIf Not String.IsNullOrEmpty(lvlName) Then
                            key = REG_LAYOUT_DETAIL
                        Else
                            key = REG_LAYOUT_MASTER
                        End If
                    Else
                        key = If(isDetail, REG_LAYOUT_DETAIL, REG_LAYOUT_MASTER)
                    End If
                Catch
                    key = If(isDetail, REG_LAYOUT_DETAIL, REG_LAYOUT_MASTER)
                End Try
            End If
            view.SaveLayoutToRegistry(key)
        Catch ex As Exception
            ErrDisp(ex.Message, "SaveGridLayout", , , ex)
        End Try
    End Sub

    Private Sub RestoreGridLayout(view As GridView, isDetail As Boolean)
        Try
            If view Is Nothing Then Exit Sub
            Dim key As String = REG_LAYOUT_MASTER
            Dim tagStr As String = TryCast(view.Tag, String)
            If String.Equals(tagStr, "MASTER", StringComparison.OrdinalIgnoreCase) Then
                key = REG_LAYOUT_MASTER
            ElseIf String.Equals(tagStr, "DETAIL2", StringComparison.OrdinalIgnoreCase) Then
                key = REG_LAYOUT_DETAIL2
            ElseIf String.Equals(tagStr, "DETAIL", StringComparison.OrdinalIgnoreCase) Then
                key = REG_LAYOUT_DETAIL
            Else
                ' Fallback to LevelName if available
                Try
                    Dim lvlProp = view.GetType().GetProperty("LevelName")
                    If lvlProp IsNot Nothing Then
                        Dim lvlName As String = Convert.ToString(lvlProp.GetValue(view, Nothing))
                        If String.Equals(lvlName, "Hareketler", StringComparison.OrdinalIgnoreCase) Then
                            key = REG_LAYOUT_DETAIL2
                        ElseIf Not String.IsNullOrEmpty(lvlName) Then
                            key = REG_LAYOUT_DETAIL
                        Else
                            key = REG_LAYOUT_MASTER
                        End If
                    Else
                        key = If(isDetail, REG_LAYOUT_DETAIL, REG_LAYOUT_MASTER)
                    End If
                Catch
                    key = If(isDetail, REG_LAYOUT_DETAIL, REG_LAYOUT_MASTER)
                End Try
            End If
            view.RestoreLayoutFromRegistry(key)
        Catch ex As Exception
            ' ignore if no layout yet
        End Try
    End Sub

    Private Sub HookPersistEvents(view As GridView, isDetail As Boolean)
        Try
            If view Is Nothing Then Exit Sub
            ' Remove then add to avoid duplicates
            RemoveHandler view.ColumnWidthChanged, AddressOf GridView_LayoutChanged
            RemoveHandler view.ColumnPositionChanged, AddressOf GridView_LayoutChanged
            RemoveHandler view.EndSorting, AddressOf GridView_LayoutChanged
            AddHandler view.ColumnWidthChanged, AddressOf GridView_LayoutChanged
            AddHandler view.ColumnPositionChanged, AddressOf GridView_LayoutChanged
            AddHandler view.EndSorting, AddressOf GridView_LayoutChanged
            ' Tag to know detail vs master inside event; keep DETAIL2 if already set
            If isDetail Then
                Dim existing As String = TryCast(view.Tag, String)
                If Not String.Equals(existing, "DETAIL2", StringComparison.OrdinalIgnoreCase) Then
                    view.Tag = "DETAIL"
                End If
            Else
                view.Tag = "MASTER"
            End If
        Catch ex As Exception
            ErrDisp(ex.Message, "HookPersistEvents", , , ex)
        End Try
    End Sub

    Private Sub GridView_LayoutChanged(sender As Object, e As EventArgs)
        Try
            Dim gv = TryCast(sender, GridView)
            If gv Is Nothing Then Exit Sub
            Dim isDetail As Boolean = (TryCast(gv.Tag, String) = "DETAIL")
            SaveGridLayout(gv, isDetail)
        Catch ex As Exception
            ErrDisp(ex.Message, "GridView_LayoutChanged", , , ex)
        End Try
    End Sub

    Private Sub UretimPlanlama1_FormClosing(sender As Object, e As FormClosingEventArgs)
        Try
            Dim master = TryCast(GridControl1.MainView, GridView)
            SaveGridLayout(master, False)
        Catch ex As Exception
            ErrDisp(ex.Message, "UretimPlanlama1_FormClosing", , , ex)
        Finally
            Try
                For Each kv In _imageCache
                    If kv.Value IsNot Nothing Then kv.Value.Dispose()
                Next
                _imageCache.Clear()
            Catch
            End Try
        End Try
    End Sub

    Private Function RegistryLayoutExists(key As String) As Boolean
        Try
            Using rk As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\\" & key)
                Return rk IsNot Nothing
            End Using
        Catch
            Return False
        End Try
    End Function

    Private Function GetSQL() As String
        Try
            Dim cSQL As String = String.Empty

            cSQL = "select a.UretimTakipNo, a.isemrino, a.aciklama, "

            cSQL = cSQL &
                " SiparisNo = (select top 1 siparisno " &
                                " from sipmodel with (NOLOCK) " &
                                " where uretimtakipno = a.uretimtakipno) , "
            cSQL = cSQL &
                " c.ModelNo, a.Departman, a.Firma, b.baslama_tar, b.bitis_tar, "

            ' adetler

            cSQL = cSQL &
                " SiparisAdet = (select sum(coalesce(adet,0)) " &
                                " from sipmodel with (NOLOCK) " &
                                " where uretimtakipno = a.uretimtakipno) , "
            cSQL = cSQL &
                " IsemriAdet = sum(coalesce(c.adet,0)) , "

            cSQL = cSQL &
                " UretilenAdet = (select sum(coalesce(z.adet,0)) " &
                                " from uretharfis x with (NOLOCK), uretharfislines y with (NOLOCK), uretharrba z with (NOLOCK) " &
                                " where x.uretfisno = y.uretfisno " &
                                " and y.uretfisno = z.uretfisno " &
                                " and y.ulineno = z.ulineno " &
                                " and z.uretimtakipno = a.uretimtakipno " &
                                " and z.isemrino = a.isemrino " &
                                " and x.cikisdept = a.departman) , "
            cSQL = cSQL &
                " SevkAdet = (select sum((coalesce(y.koliend,0) - coalesce(y.kolibeg,0) + 1) * coalesce(z.adet,0)) " &
                                " from sevkform x With (nolock) , sevkformlines y With (nolock) , sevkformlinesrba z With (nolock) " &
                                " where x.sevkformno = y.sevkformno" &
                                " And y.sevkformno = z.sevkformno" &
                                " and y.ulineno = z.ulineno" &
                                " And exists (select siparisno from sipmodel with (NOLOCK) where uretimtakipno = a.uretimtakipno and sevkiyattakipno = y.sevkiyattakipno) ) , "
            ' tarih alanları

            cSQL = cSQL &
                " SiparisGelis = (select top 1 x.siparistarihi " &
                                " from siparis x with (NOLOCK), sipmodel y with (NOLOCK) " &
                                " where x.kullanicisipno = y.siparisno " &
                                " and y.uretimtakipno = a.uretimtakipno " &
                                " and y.modelno = c.modelno " &
                                " order by x.siparistarihi), "
            cSQL = cSQL &
                " PlanlananKesimOk = (select top 1 x.kesimplok " &
                                " from siparis x with (NOLOCK), sipmodel y with (NOLOCK) " &
                                " where x.kullanicisipno = y.siparisno " &
                                " and y.uretimtakipno = a.uretimtakipno " &
                                " and y.modelno = c.modelno " &
                                " order by x.kesimplok), "
            cSQL = cSQL &
                " GercekKesimOk = (select top 1 x.KesimOK " &
                                " from siparis x with (NOLOCK), sipmodel y with (NOLOCK) " &
                                " where x.kullanicisipno = y.siparisno " &
                                " and y.uretimtakipno = a.uretimtakipno " &
                                " and y.modelno = c.modelno " &
                                " order by x.KesimOK), "
            cSQL = cSQL &
                " UretimeCikis = (select top 1 x.eksevktarihi1 " &
                                " from siparis x with (NOLOCK), sipmodel y with (NOLOCK) " &
                                " where x.kullanicisipno = y.siparisno " &
                                " and y.uretimtakipno = a.uretimtakipno " &
                                " and y.modelno = c.modelno " &
                                " order by x.eksevktarihi1), "
            cSQL = cSQL &
                " UretimeCikisHaftasi = datepart(week,(select top 1 x.eksevktarihi1 " &
                                " from siparis x with (NOLOCK), sipmodel y with (NOLOCK) " &
                                " where x.kullanicisipno = y.siparisno " &
                                " and y.uretimtakipno = a.uretimtakipno " &
                                " and y.modelno = c.modelno " &
                                " order by x.eksevktarihi1)), "
            cSQL = cSQL &
                " KumasOk = (select top 1 x.eksevktarihi2 " &
                                " from siparis x with (NOLOCK), sipmodel y with (NOLOCK) " &
                                " where x.kullanicisipno = y.siparisno " &
                                " and y.uretimtakipno = a.uretimtakipno " &
                                " and y.modelno = c.modelno " &
                                " order by x.eksevktarihi2), "
            cSQL = cSQL &
                " MalzemeOk = (select top 1 x.eksevktarihi3 " &
                                " from siparis x with (NOLOCK), sipmodel y with (NOLOCK) " &
                                " where x.kullanicisipno = y.siparisno " &
                                " and y.uretimtakipno = a.uretimtakipno " &
                                " and y.modelno = c.modelno " &
                                " order by x.eksevktarihi3), "
            cSQL = cSQL &
                " OrjinalSevk = (select top 1 sonsevktar " &
                                " From sipmodel with (NOLOCK) " &
                                " where uretimtakipno = a.uretimtakipno " &
                                " and sonsevktar is not null " &
                                " and sonsevktar <> '01.01.1950' " &
                                " ORDER BY sonsevktar) , "
            cSQL = cSQL &
                " RevizeSevk = (select top 1 ilksevktar " &
                                " From sipmodel with (NOLOCK) " &
                                " where uretimtakipno = a.uretimtakipno " &
                                " and ilksevktar is not null " &
                                " and ilksevktar <> '01.01.1950' " &
                                " ORDER BY ilksevktar) , "
            cSQL = cSQL &
                " RevizeSevkHafta = datepart(week,(select top 1 ilksevktar " &
                                " From sipmodel with (NOLOCK) " &
                                " where uretimtakipno = a.uretimtakipno " &
                                " and ilksevktar is not null " &
                                " and ilksevktar <> '01.01.1950' " &
                                " ORDER BY ilksevktar)), "
            cSQL = cSQL &
                " GercekSevk = (select top 1 x.sevktar " &
                                " from sevkform x With (nolock) , sevkformlines y With (nolock) , sevkformlinesrba z With (nolock) " &
                                " where x.sevkformno = y.sevkformno" &
                                " And y.sevkformno = z.sevkformno" &
                                " and y.ulineno = z.ulineno" &
                                " And exists (select siparisno from sipmodel with (NOLOCK) where uretimtakipno = a.uretimtakipno and sevkiyattakipno = y.sevkiyattakipno) ) , "
            cSQL = cSQL &
                " EkSevk4 = (select top 1 x.eksevktarihi4 " &
                                " from siparis x with (NOLOCK), sipmodel y with (NOLOCK) " &
                                " where x.kullanicisipno = y.siparisno " &
                                " and y.uretimtakipno = a.uretimtakipno " &
                                " and y.modelno = c.modelno " &
                                " order by x.eksevktarihi4), "
            cSQL = cSQL &
                " EkSevk5 = (select top 1 x.eksevktarihi5 " &
                                " from siparis x with (NOLOCK), sipmodel y with (NOLOCK) " &
                                " where x.kullanicisipno = y.siparisno " &
                                " and y.uretimtakipno = a.uretimtakipno " &
                                " and y.modelno = c.modelno " &
                                " order by x.eksevktarihi5), "
            cSQL = cSQL &
                " Musteri = (select top 1 x.musterino " &
                                " from siparis x with (NOLOCK), sipmodel y with (NOLOCK) " &
                                " where x.kullanicisipno = y.siparisno " &
                                " and y.uretimtakipno = a.uretimtakipno " &
                                " and y.modelno = c.modelno), "
            cSQL = cSQL &
                " Imalatci = (select top 1 x.imalatci " &
                                " from siparis x with (NOLOCK), sipmodel y with (NOLOCK) " &
                                " where x.kullanicisipno = y.siparisno " &
                                " and y.uretimtakipno = a.uretimtakipno " &
                                " and y.modelno = c.modelno), "
            cSQL = cSQL &
                " MusteriSiparisNo = (select top 1 y.musterisiparisno " &
                                " from siparis x with (NOLOCK), sipmodel y with (NOLOCK) " &
                                " where x.kullanicisipno = y.siparisno " &
                                " and y.uretimtakipno = a.uretimtakipno " &
                                " and y.modelno = c.modelno), "
            'cSQL = cSQL &
            '    " Resim = (select top 1 case " &
            '                            " when z.videodosyasi Like '%modelon%' then replace(z.videodosyasi,'modelon','modelon\thumbs') " &
            '                            " when z.videodosyasi Like '%numuneresim%' then replace(z.videodosyasi,'numuneresim','numuneresim\thumbs') " &
            '                            " else z.videodosyasi " &
            '                            " end " &
            '                    " from siparis x with (NOLOCK), sipmodel y with (NOLOCK) , ymodel z with (NOLOCK) " &
            '                    " where x.kullanicisipno = y.siparisno " &
            '                    " and y.modelno = z.modelno " &
            '                    " and y.uretimtakipno = a.uretimtakipno " &
            '                    " and y.modelno = c.modelno ) , "
            cSQL = cSQL &
                " SatirDuzeltildi = 'H' "

            cSQL = cSQL &
                " from uretimisemri a with (NOLOCK), uretimisdetayi b with (NOLOCK), uretimisrba c with (NOLOCK) " &
                " where a.isemrino = b.isemrino " &
                " and b.isemrino = c.isemrino " &
                " and b.ulineno = c.ulineno " &
                " and a.departman like 'D_K_M' "
            '" and (a.ok is null or a.ok = 'H' or a.ok = '') "

            ' reçetesi olanlar
            'cSQL = cSQL &
            '    " and exists (select modelno  " &
            '                " from modelhammadde with (NOLOCK)  " &
            '                " where modelno = c.modelno " &
            '                " and uretimdepartmani = a.departman) "

            ' açık siparişler
            cSQL = cSQL &
                cSiparisFilter

            cSQL = cSQL &
                " group by a.uretimtakipno, a.isemrino, a.aciklama, c.modelno, a.departman, a.firma, b.baslama_tar, b.bitis_tar " &
                " order by a.uretimtakipno, a.isemrino, a.aciklama, c.modelno, a.departman, a.firma, b.baslama_tar, b.bitis_tar "

            Return cSQL

        Catch ex As System.Exception
            ErrDisp(ex.Message, "UretimPlanlama1.GetSQL", , , ex)
            Return ""
        End Try
    End Function

    Private Sub PopulateGrid()
        Try
            Cursor.Current = Cursors.WaitCursor

            ' Ensure any pending material calculations are done
            MalzemeHesapla()

            ' 1. Master data
            Dim oSQL As New SQLServerClass()
            oSQL.init(4)
            Dim connectionString As String = oSQL.cConnectionString

            Dim masterSql As String = GetSQL()
            Dim masterDt As New DataTable("Master")
            Using conn As New SqlConnection(connectionString)
                Using da As New SqlDataAdapter(masterSql, conn)
                    da.Fill(masterDt)
                End Using
            End Using

            ' Normalize sentinel dates (01.01.1950 -> NULL)
            For Each col As DataColumn In masterDt.Columns
                If col.DataType Is GetType(DateTime) Then
                    For Each row As DataRow In masterDt.Rows
                        If Not row.IsNull(col) AndAlso CDate(row(col)) = #1/1/1950# Then
                            row(col) = DBNull.Value
                        End If
                    Next
                End If
            Next

            ' 2. Detail data
            Dim detailSql As String = "select uretimtakipno, isemrino, stokno, cinsaciklamasi, renk, " +
                                      " beden, kullanimmiktari, ihtiyac, isemriverilen, karsilanan, " +
                                      " uretimicincikis, uretimdeniade, uretimecikis, stokmiktari, birim, tedarikci, " +
                                      " baslama, bitis, mtf, malzemetakipno, sirano " +
                                      " from uretimismalzeme WITH (NOLOCK) " +
                                      " order by stokno, renk, beden"

            Dim detailDt As New DataTable("Detail")
            Using conn As New SqlConnection(connectionString)
                Using da As New SqlDataAdapter(detailSql, conn)
                    da.Fill(detailDt)
                End Using
            End Using

            ' 3. Filter detail rows to those existing in master
            If detailDt.Columns.Contains("isemrino") AndAlso masterDt.Columns.Contains("isemrino") Then
                Dim allowed As New HashSet(Of String)(
                    masterDt.AsEnumerable().
                        Where(Function(r) Not r.IsNull("isemrino")).
                        Select(Function(r) r("isemrino").ToString()))
                For i As Integer = detailDt.Rows.Count - 1 To 0 Step -1
                    Dim v = detailDt.Rows(i)("isemrino")
                    Dim key As String = If(v Is Nothing OrElse v Is DBNull.Value, "", v.ToString())
                    If Not allowed.Contains(key) Then
                        detailDt.Rows.RemoveAt(i)
                    End If
                Next
            End If

            ' 4. DataSet + Relation
            Dim ds As New DataSet()
            ds.Tables.Add(masterDt)
            ds.Tables.Add(detailDt)
            ds.Relations.Add("MasterDetail",
                             masterDt.Columns("isemrino"),
                             detailDt.Columns("isemrino"),
                             False)

            ' 5. Bind (keep DataSet so ExportToExcel can access both)
            GridControl1.DataSource = ds
            GridControl1.DataMember = "Master"

            ' Ensure second-level node exists in level tree
            EnsureDetail2LevelNode()

            ' 6. Master view formatting
            Dim masterView As GridView = TryCast(GridControl1.MainView, GridView)
            If masterView IsNot Nothing Then
                ' Restore previously saved layout for master grid
                RestoreGridLayout(masterView, False)

                ConfigureGridViewForReadOnlyAndSelection(masterView)
                For Each col As GridColumn In masterView.Columns
                    If col.ColumnType = GetType(Integer) OrElse
                       col.ColumnType = GetType(Long) OrElse
                       col.ColumnType = GetType(Decimal) OrElse
                       col.ColumnType = GetType(Double) OrElse
                       col.ColumnType = GetType(Single) Then
                        col.DisplayFormat.FormatType = FormatType.Numeric
                        col.DisplayFormat.FormatString = "#,0.##"
                    End If
                    col.BestFit()
                Next
                masterView.OptionsView.ColumnAutoWidth = False
                masterView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always
                ' Enable sorting / filtering / searching
                masterView.OptionsCustomization.AllowSort = True
                masterView.OptionsCustomization.AllowFilter = True
                masterView.OptionsCustomization.AllowGroup = True
                masterView.OptionsMenu.EnableColumnMenu = True
                masterView.OptionsMenu.EnableFooterMenu = True
                masterView.OptionsMenu.ShowAutoFilterRowItem = True
                masterView.OptionsFind.AlwaysVisible = True
                masterView.OptionsFind.HighlightFindResults = True
                masterView.OptionsFind.FindNullPrompt = "Ara..."
                masterView.OptionsView.ShowAutoFilterRow = True
                masterView.OptionsView.ShowGroupPanel = True
                ' Hide SatirDuzeltildi from user view
                Dim satirColMaster = masterView.Columns.ColumnByFieldName("SatirDuzeltildi")
                If satirColMaster IsNot Nothing Then
                    satirColMaster.Visible = False
                End If
                ' Editors
                Dim dateEdit As RepositoryItemDateEdit = CreateDateEdit()
                GridControl1.RepositoryItems.Add(dateEdit)

                Dim firmaValues As List(Of String) = LoadFirmaValues()
                Dim firmaCombo As RepositoryItemComboBox = CreateFirmaCombo(firmaValues)
                GridControl1.RepositoryItems.Add(firmaCombo)

                Dim aciklamaEdit As RepositoryItemTextEdit = CreateAciklamaEditor()
                GridControl1.RepositoryItems.Add(aciklamaEdit)

                masterView.OptionsBehavior.Editable = True
                masterView.OptionsBehavior.ReadOnly = False ' ensure grid not globally read-only
                For Each col As GridColumn In masterView.Columns
                    Dim fn = col.FieldName.ToLowerInvariant()
                    If fn = "baslama_tar" OrElse fn = "bitis_tar" OrElse fn = "planlanankesimok" OrElse fn = "gercekkesimok" OrElse fn = "uretimecikis" OrElse fn = "kumasok" OrElse fn = "malzemeok" OrElse fn = "eksevk4" OrElse fn = "eksevk5" Then
                        col.OptionsColumn.AllowEdit = True
                        col.OptionsColumn.ReadOnly = False
                        col.ColumnEdit = dateEdit
                    ElseIf fn = "firma" Then
                        col.OptionsColumn.AllowEdit = True
                        col.OptionsColumn.ReadOnly = False
                        col.ColumnEdit = firmaCombo
                    ElseIf fn = "aciklama" Then
                        col.OptionsColumn.AllowEdit = True
                        col.OptionsColumn.ReadOnly = False
                        col.ColumnEdit = aciklamaEdit
                    Else
                        col.OptionsColumn.AllowEdit = False
                        col.OptionsColumn.ReadOnly = True
                    End If
                Next

                ' Attach validation and auto-fix events
                RemoveHandler masterView.ValidatingEditor, AddressOf MasterGrid_ValidatingEditor
                AddHandler masterView.ValidatingEditor, AddressOf MasterGrid_ValidatingEditor
                RemoveHandler masterView.CellValueChanged, AddressOf MasterGrid_CellValueChanged
                AddHandler masterView.CellValueChanged, AddressOf MasterGrid_CellValueChanged

                '' Picture column: show image from file path stored in Resim
                'Dim resimCol As GridColumn = masterView.Columns.ColumnByFieldName("Resim")
                'Dim picCol As GridColumn = masterView.Columns.ColumnByFieldName("Resim_Pic")
                'If picCol Is Nothing Then
                '    picCol = masterView.Columns.AddVisible("Resim_Pic", "Resim")
                '    picCol.UnboundType = DevExpress.Data.UnboundColumnType.Object
                '    picCol.OptionsColumn.AllowEdit = False
                '    picCol.OptionsColumn.ReadOnly = True
                '    picCol.Width = 90
                '    picCol.VisibleIndex = 0
                '    Dim picEdit As New RepositoryItemPictureEdit()
                '    picEdit.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze
                '    picEdit.CustomHeight = 80
                '    picEdit.NullText = ""
                '    GridControl1.RepositoryItems.Add(picEdit)
                '    picCol.ColumnEdit = picEdit

                '    ' Handler to supply image data
                '    RemoveHandler masterView.CustomUnboundColumnData, AddressOf MasterView_CustomUnboundColumnData
                '    AddHandler masterView.CustomUnboundColumnData, AddressOf MasterView_CustomUnboundColumnData
                'End If
                '' Hide path column if present
                'If resimCol IsNot Nothing Then
                '    resimCol.Visible = False
                'End If

                HookPersistEvents(masterView, False)
            End If

            ' 7. Detail view handler (remove duplicate then add)
            RemoveHandler GridControl1.ViewRegistered, AddressOf GridControl1_ViewRegistered
            AddHandler GridControl1.ViewRegistered, AddressOf GridControl1_ViewRegistered

            ' 8. Expand all master rows to show all detail views (REMOVED per requirement: start collapsed)
            'If masterView IsNot Nothing Then
            '    masterView.BeginUpdate()
            '    For i As Integer = 0 To masterView.DataRowCount - 1
            '        masterView.ExpandMasterRow(i)
            '    Next
            '    masterView.EndUpdate()
            'End If


        Catch ex As Exception
            ErrDisp(ex.Message, "UretimPlanlama1.PopulateGrid", , , ex)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub GridControl1_ViewRegistered(sender As Object, e As DevExpress.XtraGrid.ViewOperationEventArgs)
        Try
            Dim detailView As GridView = TryCast(e.View, GridView)
            If detailView Is Nothing Then Return

            ConfigureGridViewForReadOnlyAndSelection(detailView)

            ' Hide key / duplicate columns if present
            Dim colU As GridColumn = detailView.Columns.ColumnByFieldName("UretimTakipNo")
            If colU IsNot Nothing Then colU.Visible = False
            Dim colIse As GridColumn = detailView.Columns.ColumnByFieldName("isemrino")
            If colIse IsNot Nothing Then colIse.Visible = False
            Dim colSir As GridColumn = detailView.Columns.ColumnByFieldName("sirano")
            If colSir IsNot Nothing Then colSir.Visible = False
            Dim colMtf As GridColumn = detailView.Columns.ColumnByFieldName("mtf")
            If colMtf IsNot Nothing Then colMtf.Visible = False

            ' Numeric formatting
            For Each col As GridColumn In detailView.Columns
                If col.ColumnType = GetType(Integer) OrElse
                   col.ColumnType = GetType(Long) OrElse
                   col.ColumnType = GetType(Decimal) OrElse
                   col.ColumnType = GetType(Double) OrElse
                   col.ColumnType = GetType(Single) Then
                    col.DisplayFormat.FormatType = FormatType.Numeric
                    col.DisplayFormat.FormatString = "#,0.##"
                End If
            Next

            detailView.OptionsView.ColumnAutoWidth = False
            detailView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always
            ' Enable sorting / filtering / searching for detail
            detailView.OptionsCustomization.AllowSort = True
            detailView.OptionsCustomization.AllowFilter = True
            detailView.OptionsCustomization.AllowGroup = True
            detailView.OptionsMenu.EnableColumnMenu = True
            detailView.OptionsMenu.EnableFooterMenu = True
            detailView.OptionsMenu.ShowAutoFilterRowItem = True
            detailView.OptionsFind.AlwaysVisible = True
            detailView.OptionsFind.HighlightFindResults = True
            detailView.OptionsFind.FindNullPrompt = "Ara..."
            detailView.OptionsView.ShowAutoFilterRow = True
            detailView.BestFitColumns()

            RestoreGridLayout(detailView, True)
            ' Enforce filter row and grouping each time (layout may override)
            detailView.OptionsView.ShowAutoFilterRow = True
            detailView.OptionsView.ShowGroupPanel = True
            detailView.OptionsCustomization.AllowGroup = True
            HookPersistEvents(detailView, True)

            ' Hook second-level (detail-of-detail) under the first-level detail view
            AttachDetail2Handlers(detailView)

            ' If this view is the second-level (Hareketler) template, mark it
            Try
                Dim lvlProp = detailView.GetType().GetProperty("LevelName")
                If lvlProp IsNot Nothing Then
                    Dim lvlName As String = Convert.ToString(lvlProp.GetValue(detailView, Nothing))
                    If String.Equals(lvlName, "Hareketler", StringComparison.OrdinalIgnoreCase) Then
                        detailView.Tag = "DETAIL2"
                        RestoreGridLayout(detailView, True)
                    End If
                End If
            Catch
            End Try
        Catch ex As Exception
            ErrDisp(ex.Message, "GridControl1_ViewRegistered", , , ex)
        End Try
    End Sub

    ' Detail-of-detail data loader (stocks movements for a detail row)
    Private Function LoadDetail2Data(mtk As String, stokno As String, renk As String, beden As String) As DataTable
        Dim dt As New DataTable()
        Try
            Dim sql As String = "select a.stokfisno, a.fistarihi, a.stokfistipi, b.stokhareketkodu, a.belgeno, a.belgetarihi, a.firma, " &
                                " b.isemrino, b.depo, b.partino, b.birimfiyat, b.dovizcinsi, b.kur, b.netmiktar1, b.birim1 " &
                                " from stokfis a with (NOLOCK), stokfislines b with (NOLOCK) " &
                                " where a.stokfisno = b.stokfisno " &
                                " and coalesce(b.malzemetakipkodu,'') = @mtk " &
                                " and b.stokno = @stokno " &
                                " and coalesce(b.renk,'') = @renk " &
                                " and coalesce(b.beden,'') = @beden " &
                                " order by a.fistarihi, a.stokfistipi desc"

            Dim oSQL As New SQLServerClass()
            oSQL.init(4)
            Using conn As New SqlClient.SqlConnection(oSQL.cConnectionString)
                Using cmd As New SqlClient.SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@mtk", If(mtk, String.Empty))
                    cmd.Parameters.AddWithValue("@stokno", If(stokno, String.Empty))
                    cmd.Parameters.AddWithValue("@renk", If(renk, String.Empty))
                    cmd.Parameters.AddWithValue("@beden", If(beden, String.Empty))
                    Using da As New SqlClient.SqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ErrDisp(ex.Message, "LoadDetail2Data", , , ex)
        End Try
        Return dt
    End Function

    ' Attach second-level (detail-of-detail) handlers to the first-level detail view
    Private Sub AttachDetail2Handlers(detailView As GridView)
        Try
            ' Avoid duplicates
            RemoveHandler detailView.MasterRowGetRelationCount, AddressOf DetailView_MasterRowGetRelationCount
            RemoveHandler detailView.MasterRowGetRelationName, AddressOf DetailView_MasterRowGetRelationName
            RemoveHandler detailView.MasterRowGetChildList, AddressOf DetailView_MasterRowGetChildList

            AddHandler detailView.MasterRowGetRelationCount, AddressOf DetailView_MasterRowGetRelationCount
            AddHandler detailView.MasterRowGetRelationName, AddressOf DetailView_MasterRowGetRelationName
            AddHandler detailView.MasterRowGetChildList, AddressOf DetailView_MasterRowGetChildList

            detailView.OptionsDetail.EnableMasterViewMode = True
            detailView.OptionsDetail.AllowExpandEmptyDetails = True
            detailView.OptionsDetail.ShowDetailTabs = False
            detailView.OptionsView.ShowDetailButtons = True
        Catch ex As Exception
            ErrDisp(ex.Message, "AttachDetail2Handlers", , , ex)
        End Try
    End Sub

    Private Sub DetailView_MasterRowGetRelationCount(sender As Object, e As DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationCountEventArgs)
        Try
            Dim gv = TryCast(sender, GridView)
            If gv Is Nothing Then Return
            Dim hasRequired As Boolean =
                (gv.Columns.ColumnByFieldName("stokno") IsNot Nothing) AndAlso
                (gv.Columns.ColumnByFieldName("renk") IsNot Nothing) AndAlso
                (gv.Columns.ColumnByFieldName("beden") IsNot Nothing)
            e.RelationCount = If(hasRequired, 1, 0)
        Catch ex As Exception
            ErrDisp(ex.Message, "DetailView_MasterRowGetRelationCount", , , ex)
        End Try
    End Sub

    Private Sub DetailView_MasterRowGetRelationName(sender As Object, e As DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventArgs)
        Try
            e.RelationName = "Hareketler"
        Catch ex As Exception
            ErrDisp(ex.Message, "DetailView_MasterRowGetRelationName", , , ex)
        End Try
    End Sub

    Private Sub DetailView_MasterRowGetChildList(sender As Object, e As DevExpress.XtraGrid.Views.Grid.MasterRowGetChildListEventArgs)
        Try
            Dim gv = TryCast(sender, GridView)
            If gv Is Nothing Then Return
            If gv.Columns.ColumnByFieldName("stokno") Is Nothing Then Return

            Dim stokno As String = Convert.ToString(gv.GetRowCellValue(e.RowHandle, "stokno")).Trim()
            Dim renk As String = Convert.ToString(gv.GetRowCellValue(e.RowHandle, "renk")).Trim()
            Dim beden As String = Convert.ToString(gv.GetRowCellValue(e.RowHandle, "beden")).Trim()
            Dim mtk As String = Convert.ToString(gv.GetRowCellValue(e.RowHandle, "malzemetakipno")).Trim()

            Dim dt As DataTable = LoadDetail2Data(mtk, stokno, renk, beden)
            e.ChildList = dt.DefaultView
        Catch ex As Exception
            ErrDisp(ex.Message, "DetailView_MasterRowGetChildList", , , ex)
        End Try
    End Sub

    ' Ensure a level node exists for the second-level relation under the first-level relation
    Private Sub EnsureDetail2LevelNode()
        Try
            Dim masterNode As DevExpress.XtraGrid.GridLevelNode = Nothing
            For Each n As DevExpress.XtraGrid.GridLevelNode In GridControl1.LevelTree.Nodes
                If String.Equals(n.RelationName, "MasterDetail", StringComparison.OrdinalIgnoreCase) Then
                    masterNode = n
                    Exit For
                End If
            Next
            If masterNode Is Nothing Then Return

            Dim exists As Boolean = False
            For Each n As DevExpress.XtraGrid.GridLevelNode In masterNode.Nodes
                If String.Equals(n.RelationName, "Hareketler", StringComparison.OrdinalIgnoreCase) Then
                    exists = True
                    Exit For
                End If
            Next
            If Not exists Then
                Dim template As New DevExpress.XtraGrid.Views.Grid.GridView(GridControl1)
                template.Name = "Detail2Template"
                template.OptionsView.ShowGroupPanel = True
                template.OptionsView.ShowAutoFilterRow = True
                template.OptionsBehavior.Editable = False
                template.OptionsBehavior.ReadOnly = True

                Dim node As New DevExpress.XtraGrid.GridLevelNode()
                node.RelationName = "Hareketler"
                node.LevelTemplate = template
                masterNode.Nodes.Add(node)
            End If
        Catch ex As Exception
            ErrDisp(ex.Message, "EnsureDetail2LevelNode", , , ex)
        End Try
    End Sub

    Private Function CreateDateEdit() As RepositoryItemDateEdit
        Dim ri As New RepositoryItemDateEdit()
        With ri
            .Mask.EditMask = "dd.MM.yyyy"
            .Mask.UseMaskAsDisplayFormat = True
            .DisplayFormat.FormatType = FormatType.DateTime
            .DisplayFormat.FormatString = "dd.MM.yyyy"
            .EditFormat.FormatType = FormatType.DateTime
            .EditFormat.FormatString = "dd.MM.yyyy"
            .CalendarTimeProperties.DisplayFormat.FormatString = "dd.MM.yyyy"
            .AllowNullInput = DevExpress.Utils.DefaultBoolean.True
            .NullText = ""
            .NullDate = Date.MinValue
            .Buttons.Clear()
            .Buttons.Add(New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo))
            .Buttons.Add(New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear))
        End With
        AddHandler ri.ButtonClick, AddressOf DateEdit_ButtonClick
        Return ri
    End Function

    Private Sub DateEdit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        Try
            If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Clear Then
                ' Clear current cell value
                Dim gv = TryCast(GridControl1.FocusedView, GridView)
                If gv IsNot Nothing Then
                    gv.SetFocusedValue(DBNull.Value)
                    gv.UpdateCurrentRow()
                End If
            End If
        Catch ex As Exception
            ErrDisp(ex.Message, "DateEdit_ButtonClick", , , ex)
        End Try
    End Sub

    Private Sub MasterGrid_ValidatingEditor(sender As Object, e As DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs)
        Try
            Dim view = TryCast(sender, GridView)
            If view Is Nothing Then Return
            Dim col = view.FocusedColumn
            If col Is Nothing Then Return
            Dim fname = col.FieldName.ToLowerInvariant()
            If fname <> "baslama_tar" AndAlso
                fname <> "bitis_tar" AndAlso
                fname <> "planlanankesimok" _
                AndAlso fname <> "gercekkesimok" _
                AndAlso fname <> "uretimecikis" _
                AndAlso fname <> "kumasok" _
                AndAlso fname <> "malzemeok" _
                AndAlso fname <> "eksevk4" _
                AndAlso fname <> "eksevk5" Then Return

            ' Accept clears
            If e.Value Is Nothing OrElse e.Value Is DBNull.Value Then Return
            If TypeOf e.Value Is DateTime AndAlso DirectCast(e.Value, DateTime) = Date.MinValue Then
                e.Value = Nothing
                Return
            End If
            If String.IsNullOrWhiteSpace(e.Value.ToString()) Then
                e.Value = Nothing
                Return
            End If

            Dim d As DateTime
            If Not DateTime.TryParse(e.Value.ToString(), d) Then
                e.Valid = False
                e.ErrorText = "Geçersiz tarih"
                Return
            End If

            If d < MIN_ALLOWED_DATE OrElse d > MAX_ALLOWED_DATE Then
                e.Valid = False
                e.ErrorText = "Tarih aralığı: " & MIN_ALLOWED_DATE.ToString("dd.MM.yyyy") & " - " & MAX_ALLOWED_DATE.ToString("dd.MM.yyyy")
                Return
            End If

            ' Additional cross-field validation
            If fname = "bitis_tar" Then
                Dim basVal = view.GetFocusedRowCellValue("baslama_tar")
                Dim basDate As DateTime
                If basVal IsNot Nothing AndAlso basVal IsNot DBNull.Value AndAlso DateTime.TryParse(basVal.ToString(), basDate) Then
                    If d < basDate Then
                        e.Valid = False
                        e.ErrorText = "Bitiş tarihi başlangıçtan küçük olamaz"
                    End If
                End If
            End If

        Catch ex As Exception
            ErrDisp(ex.Message, "MasterGrid_ValidatingEditor", , , ex)
        End Try
    End Sub

    Private Sub MasterGrid_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs)
        Try
            Dim view = TryCast(sender, GridView)
            If view Is Nothing Then Return
            Dim fname = e.Column.FieldName.ToLowerInvariant()

            ' If baslama_tar changed ensure bitis_tar >= baslama_tar
            If fname = "baslama_tar" Then
                Dim basVal = e.Value
                Dim bitVal = view.GetRowCellValue(e.RowHandle, "bitis_tar")
                Dim basDate As DateTime
                Dim bitDate As DateTime
                If basVal IsNot Nothing AndAlso basVal IsNot DBNull.Value AndAlso DateTime.TryParse(basVal.ToString(), basDate) Then
                    If bitVal IsNot Nothing AndAlso bitVal IsNot DBNull.Value AndAlso DateTime.TryParse(bitVal.ToString(), bitDate) Then
                        If bitDate < basDate Then
                            view.SetRowCellValue(e.RowHandle, "bitis_tar", basDate)
                        End If
                    End If
                End If
            End If

            ' Fields considered user-editable (date columns)
            Dim editableFields As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase) From {
                "baslama_tar", "bitis_tar", "planlanankesimok", "gercekkesimok", "uretimecikis", "kumasok", "malzemeok", "eksevk4", "eksevk5", "firma", "aciklama"
            }

            ' If any editable field changed, set SatirDuzeltildi = 'E'
            If editableFields.Contains(e.Column.FieldName) Then
                Dim satirCol = view.Columns.ColumnByFieldName("SatirDuzeltildi")
                If satirCol IsNot Nothing Then
                    Dim cur = view.GetRowCellValue(e.RowHandle, satirCol)
                    If cur Is Nothing OrElse Not String.Equals(cur.ToString(), "E", StringComparison.OrdinalIgnoreCase) Then
                        ' Avoid recursion flag is simple because next event will be for SatirDuzeltildi only
                        view.SetRowCellValue(e.RowHandle, satirCol, "E")
                    End If
                End If
            End If

        Catch ex As Exception
            ErrDisp(ex.Message, "MasterGrid_CellValueChanged", , , ex)
        End Try
    End Sub

    ' Configure grid for read-only and multi-cell selection
    Private Sub ConfigureGridViewForReadOnlyAndSelection(view As GridView)
        Try
            view.OptionsBehavior.Editable = False
            view.OptionsBehavior.ReadOnly = True

            ' Multi–cell selection
            view.OptionsSelection.MultiSelect = True
            view.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect
            view.OptionsSelection.EnableAppearanceFocusedCell = True
            view.OptionsSelection.EnableAppearanceFocusedRow = True

            ' Focus/selection visuals
            view.Appearance.FocusedCell.BackColor = Color.LightYellow
            view.Appearance.FocusedCell.Options.UseBackColor = True
            view.Appearance.SelectedRow.BackColor = Color.LightSkyBlue
            view.Appearance.SelectedRow.Options.UseBackColor = True
            view.Appearance.FocusedRow.BackColor = Color.LightYellow
            view.Appearance.FocusedRow.Options.UseBackColor = True

            ' Remove old handlers first to avoid duplicates
            RemoveHandler view.MouseUp, AddressOf GridView_MouseUpForSum
            AddHandler view.MouseUp, AddressOf GridView_MouseUpForSum

            RemoveHandler view.RowStyle, AddressOf GridView_RowStyle
            AddHandler view.RowStyle, AddressOf GridView_RowStyle

            RemoveHandler view.MouseDown, AddressOf GridView_MouseDownForHighlight
            AddHandler view.MouseDown, AddressOf GridView_MouseDownForHighlight

            ' Initialize highlight handle storage
            If Not _highlightRowHandles.ContainsKey(view) Then
                _highlightRowHandles(view) = -1
            End If
        Catch ex As Exception
            ErrDisp(ex.Message, "ConfigureGridViewForReadOnlyAndSelection", , , ex)
        End Try
    End Sub

    Private Sub GridView_MouseDownForHighlight(sender As Object, e As MouseEventArgs)
        Try
            Dim gv = TryCast(sender, GridView)
            If gv Is Nothing Then Return
            Dim hit = gv.CalcHitInfo(e.Location)
            If hit.InRowCell AndAlso hit.RowHandle >= 0 Then
                If hit.Column IsNot Nothing AndAlso hit.Column.VisibleIndex = 0 Then
                    _highlightRowHandles(gv) = hit.RowHandle
                Else
                    _highlightRowHandles(gv) = -1
                End If
                gv.InvalidateRows()
            End If
        Catch ex As Exception
            ErrDisp(ex.Message, "GridView_MouseDownForHighlight", , , ex)
        End Try
    End Sub

    ' Row style: only color row if it is the stored highlight row (clicked first cell) and not selected
    Private Sub GridView_RowStyle(sender As Object, e As RowStyleEventArgs)
        Try
            Dim gv = TryCast(sender, GridView)
            If gv Is Nothing Then Return
            If e.RowHandle < 0 Then Return
            ' Keep selection visuals
            If gv.IsRowSelected(e.RowHandle) Then Return

            Dim targetHandle As Integer = -1
            If _highlightRowHandles.TryGetValue(gv, targetHandle) Then
                If e.RowHandle = targetHandle Then
                    e.Appearance.BackColor = Color.Yellow
                    e.Appearance.Options.UseBackColor = True
                End If
            End If
        Catch ex As Exception
            ErrDisp(ex.Message, "GridView_RowStyle", , , ex)
        End Try
    End Sub

    Private Sub GridView_MouseUpForSum(sender As Object, e As MouseEventArgs)
        ' Optional: implement summing of selected cells. Left minimal.
    End Sub

    Public Sub RefreshData()
        Try
            PopulateGrid()
        Catch ex As Exception
            ErrDisp(ex.Message, "UretimPlanlama1.RefreshData", , , ex)
        End Try
    End Sub

    Public Sub ExportToExcel()
        Try
            ' Validate data source
            Dim ds As DataSet = TryCast(GridControl1.DataSource, DataSet)
            If ds Is Nothing OrElse Not ds.Tables.Contains("Master") Then
                XtraMessageBox.Show("Veri yok", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            Dim masterDt As DataTable = ds.Tables("Master")
            Dim detailDt As DataTable = If(ds.Tables.Contains("Detail"), ds.Tables("Detail"), Nothing)
            Dim rel As DataRelation = Nothing
            If ds.Relations.Contains("MasterDetail") Then
                rel = ds.Relations("MasterDetail")
            End If

            ' Build a flattened export table: one row per detail; master columns repeated
            Dim exportDt As New DataTable("Export")

            ' Add master columns
            For Each mc As DataColumn In masterDt.Columns
                exportDt.Columns.Add(mc.ColumnName, mc.DataType)
            Next

            ' Add detail columns (prefixed to avoid collisions)
            Dim detailPrefix As String = "Det_"
            Dim detailCols As New List(Of DataColumn)
            If detailDt IsNot Nothing Then
                For Each dc As DataColumn In detailDt.Columns
                    Dim colName As String = detailPrefix & dc.ColumnName
                    Dim uniqueName As String = colName
                    Dim idx As Integer = 1
                    While exportDt.Columns.Contains(uniqueName)
                        uniqueName = colName & "_" & idx.ToString()
                        idx += 1
                    End While
                    exportDt.Columns.Add(uniqueName, dc.DataType)
                    detailCols.Add(dc)
                Next
            End If

            ' Populate rows
            For Each mRow As DataRow In masterDt.Rows
                Dim children() As DataRow = If(rel IsNot Nothing, mRow.GetChildRows(rel), Nothing)
                If children IsNot Nothing AndAlso children.Length > 0 Then
                    For Each dRow As DataRow In children
                        Dim newRow As DataRow = exportDt.NewRow()
                        ' master values
                        For Each mc As DataColumn In masterDt.Columns
                            newRow(mc.ColumnName) = If(mRow.IsNull(mc), DBNull.Value, mRow(mc))
                        Next
                        ' detail values
                        If detailDt IsNot Nothing Then
                            For i As Integer = 0 To detailCols.Count - 1
                                Dim dc As DataColumn = detailCols(i)
                                Dim targetColName As String = exportDt.Columns(masterDt.Columns.Count + i).ColumnName
                                newRow(targetColName) = If(dRow.IsNull(dc), DBNull.Value, dRow(dc))
                            Next
                        End If
                        exportDt.Rows.Add(newRow)
                    Next
                Else
                    ' No detail rows: add one row with only master values
                    Dim newRow As DataRow = exportDt.NewRow()
                    For Each mc As DataColumn In masterDt.Columns
                        newRow(mc.ColumnName) = If(mRow.IsNull(mc), DBNull.Value, mRow(mc))
                    Next
                    exportDt.Rows.Add(newRow)
                End If
            Next

            If exportDt.Rows.Count = 0 Then
                XtraMessageBox.Show("Dışa aktarılacak kayıt yok", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' Ask user for file path
            Dim dlg As New SaveFileDialog With {
                .Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*",
                .Title = "Save as Excel File",
                .FileName = "UretimPlanlama_Flat_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".xlsx"
            }
            If dlg.ShowDialog() <> DialogResult.OK Then Return

            ' Use a temporary hidden form to host the grid (ensures BindingContext) and export the flattened table
            Using tempForm As New Form()
                tempForm.ShowInTaskbar = False
                tempForm.StartPosition = FormStartPosition.Manual
                tempForm.Location = New Point(-2000, -2000)
                tempForm.Size = New Size(1, 1)

                Using tempGrid As New DevExpress.XtraGrid.GridControl(), tempView As New DevExpress.XtraGrid.Views.Grid.GridView(tempGrid)
                    tempGrid.MainView = tempView
                    tempGrid.ViewCollection.Add(tempView)
                    tempGrid.DataSource = exportDt
                    tempGrid.Dock = DockStyle.Fill
                    tempForm.Controls.Add(tempGrid)

                    ' Create handles and initialize bindings
                    tempForm.CreateControl()
                    tempGrid.CreateControl()
                    tempGrid.ForceInitialize()
                    tempView.PopulateColumns()
                    tempView.BestFitColumns()

                    Dim opts As New DevExpress.XtraPrinting.XlsxExportOptionsEx With {
                        .ExportType = DevExpress.Export.ExportType.DataAware,
                        .AllowGrouping = DevExpress.Utils.DefaultBoolean.True,
                        .ShowGridLines = True,
                        .SheetName = "UretimPlanlama"
                    }

                    tempGrid.ExportToXlsx(dlg.FileName, opts)
                End Using
            End Using

            ' Open file
            Try
                Process.Start(New ProcessStartInfo With {.FileName = dlg.FileName, .UseShellExecute = True})
            Catch exOpen As Exception
                ErrDisp(exOpen.Message, "ExportToExcel-Open", , , exOpen)
                XtraMessageBox.Show("Export başarılı fakat açılamadı: " & exOpen.Message, "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try

            XtraMessageBox.Show("Excel dosyası oluşturuldu", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            ErrDisp(ex.Message, "UretimPlanlama1.ExportToExcel", , , ex)
            XtraMessageBox.Show("Export hata: " & ex.Message, "Export", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub ResetGridLayout()
        Try
            For Each key In New String() {REG_LAYOUT_MASTER, REG_LAYOUT_DETAIL, REG_LAYOUT_DETAIL2}
                Try
                    Registry.CurrentUser.DeleteSubKeyTree("Software\\" & key, False)
                Catch
                End Try
            Next
            RefreshData()
        Catch ex As Exception
            ErrDisp(ex.Message, "ResetGridLayout", , , ex)
        End Try
    End Sub

    ' Load Firma values from DB and create a ComboBox editor items list
    Private Function LoadFirmaValues() As List(Of String)
        Dim setVals As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        Dim cSQL As String = "select firma from firma with (NOLOCK) where firma is not null and firma <> ''"
        Try
            Dim oSQL As New SQLServerClass()
            oSQL.init(4)
            Using conn As New SqlClient.SqlConnection(oSQL.cConnectionString)
                conn.Open()
                Using cmd As New SqlClient.SqlCommand(cSQL, conn)
                    Using rdr As SqlClient.SqlDataReader = cmd.ExecuteReader()
                        While rdr.Read()
                            Dim v As Object = rdr(0)
                            If v IsNot Nothing AndAlso Not Convert.IsDBNull(v) Then
                                Dim s As String = v.ToString().Trim()
                                If s <> String.Empty Then setVals.Add(s)
                            End If
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ErrDisp(ex.Message, "LoadFirmaValues", , , ex)
        End Try
        Dim result As New List(Of String)(setVals)
        result.Sort(StringComparer.CurrentCultureIgnoreCase)
        Return result
    End Function

    Private Function CreateFirmaCombo(items As IEnumerable(Of String)) As RepositoryItemComboBox
        Dim ri As New RepositoryItemComboBox()
        ri.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard ' allow typing
        ri.Items.Clear()
        For Each s As String In items
            ri.Items.Add(s)
        Next
        Return ri
    End Function

    ' Helper to create a text editor for Aciklama (max 100 chars)
    Private Function CreateAciklamaEditor() As RepositoryItemTextEdit
        Dim ri As New RepositoryItemTextEdit()
        ri.MaxLength = 100
        ri.NullText = ""
        Return ri
    End Function


    ' Supplies image data for the unbound picture column from the file path in Resim
    'Private Sub MasterView_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs)
    '    Try
    '        If e.Column Is Nothing OrElse e.Column.FieldName <> "Resim_Pic" Then Return
    '        If Not e.IsGetData Then Return
    '        Dim gv = TryCast(sender, GridView)
    '        If gv Is Nothing Then Return

    '        Dim pathObj As Object = gv.GetListSourceRowCellValue(e.ListSourceRowIndex, "Resim")
    '        Dim path As String = If(pathObj Is Nothing OrElse Convert.IsDBNull(pathObj), String.Empty, Convert.ToString(pathObj))
    '        If String.IsNullOrWhiteSpace(path) Then
    '            e.Value = Nothing
    '            Return
    '        End If

    '        Dim key As String = path.Trim()
    '        Dim img As Image = Nothing
    '        If _imageCache.TryGetValue(key, img) Then
    '            e.Value = img
    '            Return
    '        End If

    '        If File.Exists(key) Then
    '            Using fs As New FileStream(key, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
    '                img = Image.FromStream(fs)
    '            End Using
    '            _imageCache(key) = img
    '            e.Value = img
    '        Else
    '            e.Value = Nothing
    '        End If
    '    Catch
    '        e.Value = Nothing
    '    End Try
    'End Sub
End Class

'Public Class ResimColumnHelper
'    Public Shared Sub Initialize(resimCol As GridColumn)
'        If resimCol Is Nothing Then Return
'        resimCol.Visible = True
'        resimCol.OptionsColumn.AllowEdit = False
'        resimCol.Width = 100 ' set a default width
'    End Sub
'End Class