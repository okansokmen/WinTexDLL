Option Strict On

Imports System.ComponentModel
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.BandedGrid
Imports System.IO
Imports DevExpress.Data
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmListView

    Dim oList As List(Of Object)
    Dim cHeader As String = ""
    Dim oBindinglist As Object
    Dim oSource As Object
    Dim lEdit As Boolean
    Dim nMode As Integer

    Public Sub init(oList1 As IEnumerable(Of Object), Optional cHeader1 As String = "Liste", Optional oForm As Form = Nothing, Optional nMode1 As Integer = 0)

        oList = New List(Of Object)(oList1)
        oBindinglist = New BindingList(Of Object)(oList)
        oSource = New BindingSource(oBindinglist, Nothing)

        nMode = nMode1

        cHeader = cHeader1.Trim

        If Not IsNothing(oForm) Then
            Me.MdiParent = oForm
        End If

        Me.Show()
    End Sub

    Public Sub init2(oDataTable As DataTable, Optional cHeader1 As String = "Liste", Optional oForm As Form = Nothing, Optional nMode1 As Integer = 0)

        oSource = oDataTable
        nMode = nMode1

        cHeader = cHeader1.Trim

        If Not IsNothing(oForm) Then
            Me.MdiParent = oForm
        End If

        Me.Show()
    End Sub

    Private Sub frmListView_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' nMode = 1 , upload ürün
        ' nMode = 2 , upload siparis 
        ' nMode = 3 , download siparis
        Try
            'GridControl1.MainView.Dispose()
            'Dim AdvBandedView As AdvBandedGridView = New AdvBandedGridView(GridControl1)
            'GridControl1.MainView = AdvBandedView

            GridView1.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full
            GridView1.OptionsView.BestFitMaxRowCount = 100
            GridView1.OptionsView.ColumnAutoWidth = False
            GridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True
            GridView1.OptionsView.HeaderFilterButtonShowMode = DevExpress.XtraEditors.Controls.FilterButtonShowMode.Button
            GridView1.OptionsView.ShowAutoFilterRow = True
            GridView1.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow
            GridView1.OptionsView.ShowChildrenInGroupPanel = True
            GridView1.OptionsView.ShowColumnHeaders = True
            GridView1.OptionsView.ShowDetailButtons = True
            GridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways
            GridView1.OptionsView.ShowFooter = True
            GridView1.OptionsView.ShowGroupedColumns = True
            GridView1.OptionsView.ShowGroupExpandCollapseButtons = True
            GridView1.OptionsView.ShowGroupPanel = True
            GridView1.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.True
            GridView1.OptionsView.ShowIndicator = True
            GridView1.OptionsView.ShowPreviewRowLines = DevExpress.Utils.DefaultBoolean.True
            GridView1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.True
            GridView1.OptionsView.ShowViewCaption = True And
            GridView1.OptionsView.FilterCriteriaDisplayStyle = DevExpress.XtraEditors.FilterCriteriaDisplayStyle.Visual

            GridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False
            GridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False
            GridView1.OptionsBehavior.SummariesIgnoreNullValues = True
            GridView1.OptionsSelection.MultiSelect = True

            Select Case nMode
                Case 1
                    Button4.Enabled = True
                    Button5.Enabled = False
                Case 2
                    Button4.Enabled = True
                    Button5.Enabled = False
                Case 3
                    Button4.Enabled = False
                    Button5.Enabled = True
                Case Else
                    Button4.Enabled = False
                    Button5.Enabled = False
            End Select

            Select Case nMode
                Case 0
                    lEdit = False
                Case 1
                    lEdit = True
            End Select

            If lEdit Then
                GridView1.OptionsBehavior.Editable = True
                GridView1.OptionsBehavior.ReadOnly = False
            Else
                GridView1.OptionsBehavior.Editable = False
                GridView1.OptionsBehavior.ReadOnly = True
            End If

            RefreshMe(oSource)

            'GridView1.BestFitColumns()
            Me.Text = cHeader

        Catch ex As Exception
            ErrDisp("frmListView_Load", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub RefreshMe(oSource As Object)
        Try
            GridControl1.BeginUpdate()
            GridView1.Columns.Clear()
            GridControl1.DataSource = Nothing
            GridControl1.DataSource = oSource
            GridControl1.EndUpdate()

        Catch ex As Exception
            ErrDisp("RefreshMe", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Çıkış
        On Error Resume Next
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Yazdır
        On Error Resume Next
        Me.GridControl1.ShowRibbonPrintPreview()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' excel
        Try
            Dim cFilePath = Path.GetTempFileName
            cFilePath = Replace(cFilePath, ".tmp", ".xlsx")
            Me.GridControl1.ExportToXlsx(cFilePath)
            System.Diagnostics.Process.Start(cFilePath)

        Catch ex As Exception
            ErrDisp("Button3_Click", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' Gönder / Upload
        Try
            Dim nSelectedRows() As Integer
            Dim nRow As Integer = 0
            Dim oSQL As SQLServerClass
            Dim cStokNo As String = ""
            Dim cRenk As String = ""
            Dim cBeden As String = ""
            Dim cSiparisNo As String = ""
            Dim lOK As Boolean = False
            Dim oDataTable As DataTable

            If GridView1.SelectedRowsCount = 0 Then
                MsgBox("Dikkat upload edilecek satırları seçmelisiniz")
                Exit Sub
            End If

            Select Case nMode
                Case 1
                    ' ürünleri gönder
                    Dim oUrun As New UrunClass

                    nSelectedRows = GridView1.GetSelectedRows
                    For Each nRow In nSelectedRows
                        cStokNo = GridView1.GetRowCellValue(nRow, "StokNo").ToString.Trim
                        cRenk = GridView1.GetRowCellValue(nRow, "Renk").ToString.Trim
                        cBeden = GridView1.GetRowCellValue(nRow, "Beden").ToString.Trim
                        lOK = oUrun.SendProduct(cStokNo, cRenk, cBeden)
                    Next

                    oSQL = New SQLServerClass
                    oSQL.OpenConn()
                    oSQL.cSQLQuery = GetSQLQuery(2)
                    oDataTable = New DataTable
                    oDataTable = oSQL.SQLSelect()
                    oSQL.CloseConn()

                    RefreshMe(oDataTable)

                    oUrun.CloseClient()
                    oUrun = Nothing

                Case 2
                    ' siparişleri gönder
                    Dim oSiparis As New SiparisClass

                    nSelectedRows = GridView1.GetSelectedRows
                    For Each nRow In nSelectedRows
                        cSiparisNo = GridView1.GetRowCellValue(nRow, "siparisno").ToString.Trim
                        lOK = oSiparis.WriteSiparisToTiciMax(cSiparisNo)
                    Next

                    oSQL = New SQLServerClass
                    oSQL.OpenConn()
                    oSQL.cSQLQuery = GetSQLQuery(3)
                    oDataTable = New DataTable
                    oDataTable = oSQL.SQLSelect()
                    oSQL.CloseConn()

                    RefreshMe(oDataTable)

                    oSiparis.CloseClient()
                    oSiparis = Nothing
            End Select

        Catch ex As Exception
            ErrDisp("Button4_Click", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        Try
            Dim nRow As Integer = 0

            nRow = GridView1.FocusedRowHandle

            If nRow < 0 Then Exit Sub

            Select Case cHeader
                Case "Siparişler"
                    nSiparisID = CInt(GridView1.GetRowCellValue(nRow, "ID"))
                Case "Ürünler"
                    nUrunID = CInt(GridView1.GetRowCellValue(nRow, "ID"))
                Case "Üyeler"
                    nUyeID = CInt(GridView1.GetRowCellValue(nRow, "ID"))
            End Select

            Exit Sub

        Catch ex As Exception
            ErrDisp("GridView1_FocusedRowChanged", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub GridView1_RowStyle(sender As Object, e As RowStyleEventArgs) Handles GridView1.RowStyle

        Try
            Dim oCellValue As Object = Nothing
            Dim nSelected As Integer = 0

            Select Case nMode
                'Case 1
                '    oCellValue = GridView1.GetRowCellValue(e.RowHandle, "Secim")

                '    If IsDBNull(oCellValue) Then
                '        e.Appearance.BackColor = System.Drawing.Color.LightCoral
                '    Else
                '        nSelected = Convert.ToInt32(oCellValue)
                '        Select Case nSelected
                '            Case 0
                '                e.Appearance.BackColor = System.Drawing.Color.LightGreen
                '            Case 1
                '                e.Appearance.BackColor = System.Drawing.Color.Yellow
                '        End Select
                '        e.HighPriority = True
                '    End If
            End Select

        Catch ex As Exception
            ErrDisp("GridView1_RowStyle", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ' indir
        Try
            Dim nRow As Integer = 0
            Dim nSelectedRows() As Integer
            Dim oWebSiparis As WinTexTicimax.SiparisServis.WebSiparis
            Dim oSiparis As SiparisClass

            Select Case nMode
                Case 3
                    If GridView1.SelectedRowsCount = 0 Then
                        MsgBox("Dikkat sipariş satırlarını seçmelisiniz")
                        Exit Sub
                    End If

                    oSiparis = New SiparisClass
                    nSelectedRows = GridView1.GetSelectedRows
                    For Each nRow In nSelectedRows
                        oWebSiparis = New WinTexTicimax.SiparisServis.WebSiparis
                        oWebSiparis = TryCast(GridView1.GetRow(nRow), WinTexTicimax.SiparisServis.WebSiparis)
                        oSiparis.WriteSiparisToWinTex(oWebSiparis)
                    Next
                    oSiparis.CloseClient()

            End Select
        Catch ex As Exception
            ErrDisp("Button5_Click", Me.Name,,, ex)
        End Try
    End Sub

End Class