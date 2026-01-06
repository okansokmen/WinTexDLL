Imports Microsoft.VisualBasic

Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.IO
Imports System.Data.SqlClient

Imports DevExpress.XtraPivotGrid
Imports DevExpress.XtraCharts
Imports DevExpress.XtraCharts.Wizard
Imports DevExpress.Data.PivotGrid
Imports DevExpress.Spreadsheet
Imports DevExpress.XtraCharts.Designer
Imports DevExpress.XtraPivotGrid.Customization
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid

Public Class DevXReports

    Private Structure oSonuc
        Dim cTipi As String
        Dim cBeden As String
        Dim nAdet As Double
    End Structure

    Dim cSonuc As String = ""
    Dim cViewName As String = ""
    Dim cRClass As String = ""
    Dim cReportName As String = ""
    Dim nOpMode As Integer = 1

    Dim cServer As String = ""
    Dim cDatabase As String = ""
    Dim cUsername As String = ""
    Dim cPassword As String = ""

    Dim lOLAP As Boolean = False
    Dim cRegKey As String = "" '  WinTex\\OLAP\\TicSipOlapGrid
    Dim cOlapConnectionString As String = ""

    Public Sub init(cReportClass As String, Optional cSQL As String = "", Optional ByVal nMode As Integer = 0, Optional cView As String = "",
                    Optional cServer1 As String = "", Optional cDatabase1 As String = "", Optional cUsername1 As String = "", Optional cPassword1 As String = "")
        Try
            Dim oSQL As SQLServerClass

            nOpMode = nMode

            If cServer1.Trim = "" Then
                cServer = oConnection.cServer.Trim
                cDatabase = oConnection.cDatabase.Trim
                cUsername = oConnection.cUser.Trim
                cPassword = oConnection.cPassword.Trim
            Else
                cServer = cServer1.Trim
                cDatabase = cDatabase1.Trim
                cUsername = cUsername1.Trim
                cPassword = cPassword1.Trim
            End If

            AdvBandedGridView1.BestFitMaxRowCount = 50

            AdvBandedGridView1.OptionsView.ColumnAutoWidth = True
            AdvBandedGridView1.OptionsView.ShowAutoFilterRow = True
            AdvBandedGridView1.OptionsView.ShowBands = True
            AdvBandedGridView1.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways
            AdvBandedGridView1.OptionsView.ShowChildrenInGroupPanel = True
            AdvBandedGridView1.OptionsView.ShowColumnHeaders = True
            AdvBandedGridView1.OptionsView.ShowDetailButtons = True
            AdvBandedGridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways
            AdvBandedGridView1.OptionsView.ShowFooter = True
            AdvBandedGridView1.OptionsView.ShowGroupExpandCollapseButtons = True
            AdvBandedGridView1.OptionsView.ShowGroupPanel = True
            AdvBandedGridView1.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.True
            AdvBandedGridView1.OptionsView.ShowIndicator = True
            'AdvBandedGridView1.OptionsView.ShowPreview = True
            'AdvBandedGridView1.OptionsView.ShowPreviewRowLines = DevExpress.Utils.DefaultBoolean.True
            AdvBandedGridView1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.True
            AdvBandedGridView1.OptionsView.ShowViewCaption = True

            AdvBandedGridView1.OptionsBehavior.AutoUpdateTotalSummary = True
            AdvBandedGridView1.OptionsBehavior.SummariesIgnoreNullValues = True

            AdvBandedGridView1.OptionsCustomization.AllowBandMoving = True
            AdvBandedGridView1.OptionsCustomization.AllowBandResizing = True
            AdvBandedGridView1.OptionsCustomization.AllowChangeBandParent = True
            AdvBandedGridView1.OptionsCustomization.AllowChangeColumnParent = True
            AdvBandedGridView1.OptionsCustomization.AllowColumnMoving = True
            AdvBandedGridView1.OptionsCustomization.AllowColumnResizing = True
            AdvBandedGridView1.OptionsCustomization.AllowFilter = True
            AdvBandedGridView1.OptionsCustomization.AllowGroup = True
            AdvBandedGridView1.OptionsCustomization.AllowMergedGrouping = DevExpress.Utils.DefaultBoolean.True
            AdvBandedGridView1.OptionsCustomization.AllowQuickHideColumns = True
            AdvBandedGridView1.OptionsCustomization.AllowRowSizing = True
            AdvBandedGridView1.OptionsCustomization.AllowSort = True

            AdvBandedGridView1.OptionsDetail.AllowOnlyOneMasterRowExpanded = True
            AdvBandedGridView1.OptionsDetail.AutoZoomDetail = True
            AdvBandedGridView1.OptionsDetail.AllowZoomDetail = True
            AdvBandedGridView1.OptionsDetail.EnableMasterViewMode = True
            AdvBandedGridView1.OptionsDetail.ShowDetailTabs = True
            AdvBandedGridView1.OptionsDetail.ShowEmbeddedDetailIndent = DevExpress.Utils.DefaultBoolean.True
            AdvBandedGridView1.OptionsDetail.SmartDetailExpand = True
            AdvBandedGridView1.OptionsDetail.SmartDetailExpandButtonMode = DevExpress.XtraGrid.Views.Grid.DetailExpandButtonMode.AlwaysEnabled
            AdvBandedGridView1.OptionsDetail.SmartDetailHeight = True

            AdvBandedGridView1.OptionsFilter.AllowAutoFilterConditionChange = DevExpress.Utils.DefaultBoolean.True
            AdvBandedGridView1.OptionsFilter.AllowColumnMRUFilterList = True
            AdvBandedGridView1.OptionsFilter.AllowFilterEditor = True
            AdvBandedGridView1.OptionsFilter.AllowFilterIncrementalSearch = True
            AdvBandedGridView1.OptionsFilter.AllowMRUFilterList = True
            AdvBandedGridView1.OptionsFilter.AllowMultiSelectInCheckedFilterPopup = True
            AdvBandedGridView1.OptionsFilter.ColumnFilterPopupMaxRecordsCount = 50
            AdvBandedGridView1.OptionsFilter.ColumnFilterPopupMode = DevExpress.XtraGrid.Columns.ColumnFilterPopupMode.Classic
            AdvBandedGridView1.OptionsFilter.ColumnFilterPopupRowCount = 50
            AdvBandedGridView1.OptionsFilter.DefaultFilterEditorView = FilterEditorViewMode.TextAndVisual
            AdvBandedGridView1.OptionsFilter.FilterEditorAggregateEditing = FilterControlAllowAggregateEditing.AggregateWithCondition
            AdvBandedGridView1.OptionsFilter.FilterEditorAllowCustomExpressions = DevExpress.Utils.DefaultBoolean.True
            AdvBandedGridView1.OptionsFilter.FilterEditorUseMenuForOperandsAndOperators = True
            AdvBandedGridView1.OptionsFilter.InHeaderSearchMode = DevExpress.XtraGrid.Views.Grid.GridInHeaderSearchMode.TextFilter
            AdvBandedGridView1.OptionsFilter.MRUColumnFilterListCount = 50
            AdvBandedGridView1.OptionsFilter.MRUFilterListCount = 50
            AdvBandedGridView1.OptionsFilter.MRUFilterListPopupCount = 50
            AdvBandedGridView1.OptionsFilter.ShowAllTableValuesInCheckedFilterPopup = True
            AdvBandedGridView1.OptionsFilter.ShowAllTableValuesInFilterPopup = True
            AdvBandedGridView1.OptionsFilter.ShowCustomFunctions = DevExpress.Utils.DefaultBoolean.True
            AdvBandedGridView1.OptionsFilter.ShowInHeaderSearchTextMode = DevExpress.XtraGrid.Views.Grid.ShowInHeaderSearchTextMode.Text
            AdvBandedGridView1.OptionsFilter.UseNewCustomFilterDialog = True

            If cView.Trim = "" Then

                oSQL = New SQLServerClass(False, cServer, cDatabase, cUsername, cPassword)

                With oSQL
                    .OpenConn()
                    .cSQLQuery = cSQL.Trim
                    cView = .CreateTempView()
                    .CloseConn()
                End With
            End If

            cViewName = cView.Trim

            cRClass = cReportClass.Trim

            GetData()

            Select Case nMode
                Case 1 ' pivot
                    XtraTabControl1.SelectedTabPageIndex = 0
                Case 2 ' grafik
                    XtraTabControl1.SelectedTabPageIndex = 1
                Case 3 ' liste
                    XtraTabControl1.SelectedTabPageIndex = 2
            End Select

            Me.Show()

        Catch ex As Exception
            ErrDisp("init", Me.Name,,, ex)
        End Try
    End Sub

    Public Function initOLAP(Optional cReportClass1 As String = "TicSipOlapGrid", Optional cReportName1 As String = "SiparisNo",
                             Optional cConnectionString As String = "provider=MSOLAP;data source=datasrv2;initial catalog=Oxxo_Report_Cubes;cube name=6Weeks_Stores;Query Timeout=100;") As String

        initOLAP = ""

        Try
            lOLAP = True
            cRClass = cReportClass1.Trim
            cReportName = cReportName1.Trim
            cRegKey = "WinTex\\OLAP\\" + cReportClass1.Trim
            cOlapConnectionString = cConnectionString.Trim

            GetOLAPData()

            XtraTabControl1.SelectedTabPageIndex = 0

            Me.ShowDialog()

            initOLAP = cSonuc

        Catch ex As Exception
            ErrDisp("initOLAP", Me.Name,,, ex)
        End Try
    End Function

    Private Sub AddToRows(cFieldName As String, Optional nIndex As Integer = -1)
        PivotGridControl1.Fields("[NTS PROGRESS 6WEEKS SUBE].[" + cFieldName + "].[" + cFieldName + "]").Area = PivotArea.RowArea
        PivotGridControl1.Fields("[NTS PROGRESS 6WEEKS SUBE].[" + cFieldName + "].[" + cFieldName + "]").Visible = True
        If nIndex <> -1 Then
            PivotGridControl1.Fields("[NTS PROGRESS 6WEEKS SUBE].[" + cFieldName + "].[" + cFieldName + "]").AreaIndex = nIndex
        End If
    End Sub

    Private Sub AddToData(cFieldName As String, Optional nIndex As Integer = -1)
        PivotGridControl1.Fields("[Measures].[" + cFieldName + "]").Area = PivotArea.DataArea
        PivotGridControl1.Fields("[Measures].[" + cFieldName + "]").Visible = True
        If nIndex <> -1 Then
            PivotGridControl1.Fields("[Measures].[" + cFieldName + "]").AreaIndex = nIndex
        End If
    End Sub

    Private Sub AddToFilter(cFieldName As String, Optional nIndex As Integer = -1)
        PivotGridControl1.Fields("[NTS PROGRESS 6WEEKS SUBE].[" + cFieldName + "].[" + cFieldName + "]").Area = PivotArea.FilterArea
        PivotGridControl1.Fields("[NTS PROGRESS 6WEEKS SUBE].[" + cFieldName + "].[" + cFieldName + "]").Visible = True
        If nIndex <> -1 Then
            PivotGridControl1.Fields("[NTS PROGRESS 6WEEKS SUBE].[" + cFieldName + "].[" + cFieldName + "]").AreaIndex = nIndex
        End If
    End Sub

    Private Sub OLAP6WeeksStyle(Optional lRefreshData As Boolean = False)

        Dim oField As PivotGridField

        If cRClass = "6Weeks" Then
            For Each oField In PivotGridControl1.Fields
                oField.Visible = False
            Next

            ' Rows
            AddToRows("DIST REGION", 0)
            AddToRows("SEASON", 1)
            AddToRows("DEPT", 2)
            AddToRows("CLASS", 3)
            AddToRows("STYLE", 4)
            AddToRows("MODEL", 5)
            AddToRows("COLOR", 6)
            AddToRows("SIZE", 7)

            ' Cols
            AddToData("WK 1", 0)
            AddToData("WK 2", 1)
            AddToData("WK 3", 2)
            AddToData("WK 3 TOTALS", 3)
            AddToData("WK3_P", 4)
            AddToData("WK 4", 5)
            AddToData("WK 5", 6)
            AddToData("WK 6", 7)
            AddToData("WK6TOTAL", 8)
            AddToData("WK6_P", 9)
            AddToData("SLSRT", 10)
            AddToData("RETSL", 11)
            AddToData("RETSHP", 12)
            AddToData("RETST", 13)

            ' Filter
            AddToFilter("TEMA", 0)
            AddToFilter("FIT", 1)
            AddToFilter("COL", 2)
            AddToFilter("SUBE KODU", 3)
            AddToFilter("ULKE KODU", 4)
            AddToFilter("FIT GRUBU", 5)
            AddToFilter("PACA OZELLIGI", 6)
            AddToFilter("YAKA GRUBU", 7)
            AddToFilter("KOL BOYU", 8)
            AddToFilter("RISE GRUBU", 9)
            AddToFilter("BEDEN BOYU", 10)
            AddToFilter("MGZGRUP", 11)
            AddToFilter("KISA ISMI", 12)
        End If

        If lRefreshData Then
            PivotGridControl1.RefreshData()
            PivotGridControl1.BestFit()
        End If

    End Sub

    Private Sub GetOLAPData()

        Try

            ' Specify the OLAP connection settings.
            PivotGridControl1.OLAPDataProvider = OLAPDataProvider.Adomd
            PivotGridControl1.OLAPConnectionString = cOlapConnectionString
            If Not PivotGridControl1.IsOLAPDataSource Then
                MsgBox("OLAP bağlantısı yapılamadı")
                Me.Close()
            End If
            ' get the fields
            PivotGridControl1.RetrieveFields()

            OLAP6WeeksStyle()

            ' Set the Customization Forms style.
            PivotGridControl1.OptionsCustomization.CustomizationFormStyle = CustomizationFormStyle.Excel2007
            PivotGridControl1.RefreshData()
            PivotGridControl1.BestFit()

        Catch ex As Exception
            ErrDisp("GetOLAPData", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub RestoreLayout()

        Try
            PivotGridControl1.RestoreLayoutFromRegistry(cRegKey)
            PivotGridControl1.Refresh()
            PivotGridControl1.BestFit()

        Catch ex As Exception
            ErrDisp("RestoreLayout", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub DevXReports_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            DevXRestoreReport(True)
            Me.WindowState = FormWindowState.Maximized

        Catch ex As Exception
            ErrDisp("DevXReports_Load", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub GetData(Optional lRefresh As Boolean = False)

        Dim oSQL As SQLServerClass
        Dim oDataTable As New DataTable
        Dim oCol As System.Data.DataColumn
        Dim lSwitch As Boolean = True
        Dim nCount As Integer = 0
        Dim lData As Boolean = False
        Dim lCol As Boolean = False
        Dim lRow As Boolean = False

        Try
            oSQL = New SQLServerClass(False, cServer, cDatabase, cUsername, cPassword)

            With oSQL

                .OpenConn()

                oDataTable = .GetDataTableFromView(cViewName)

                ' PivotGrid and Chart
                PivotGridControl1.DataSource = oDataTable
                PivotGridControl1.Refresh()

                If Not lRefresh Then
                    For Each oCol In oDataTable.Columns
                        nCount = nCount + 1
                        Select Case oCol.DataType.Name
                            Case "Int16", "Int32", "Int64", "UInt16", "UInt32", "UInt64", "Byte", "Single", "Boolean", "SByte"
                                AddPivotField(oCol.ColumnName, 1)
                                lData = True
                            Case "Decimal", "Double"
                                AddPivotField(oCol.ColumnName, 5)
                                lData = True
                            Case "DateTime", "Date", "TimeSpan"
                                AddPivotField(oCol.ColumnName, 4)
                            Case "Char", "String", "GUID"
                                If lSwitch Then
                                    AddPivotField(oCol.ColumnName, 3)
                                    lSwitch = False
                                    lCol = True
                                Else
                                    AddPivotField(oCol.ColumnName, 2)
                                    lSwitch = True
                                    lRow = True
                                End If
                            Case Else
                                ' do nothing
                        End Select
                    Next
                End If

                ' Chart
                ChartControl1.DataSource = PivotGridControl1
                ChartControl1.Refresh()

                ' list
                GridControl1.DataSource = oDataTable
                GridControl1.Refresh()
                For Each oColumn As Views.BandedGrid.BandedGridColumn In AdvBandedGridView1.Columns
                    oColumn.AllowSummaryMenu = True
                Next

                .CloseConn()
            End With

        Catch ex As Exception
            ErrDisp("GetData", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub GetGridData()

        Dim oSQL As SQLServerClass

        Try
            oSQL = New SQLServerClass(False, cServer, cDatabase, cUsername, cPassword)

            With oSQL
                .OpenConn()
                GridControl1.DataSource = Nothing
                GridControl1.Refresh()
                GridControl1.DataSource = .GetDataTableFromView(cViewName)
                .CloseConn()
            End With

        Catch ex As Exception
            ErrDisp("AddPivotField", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub AddPivotField(cFieldName As String, nType As Integer)
        Try
            Select Case nType
                Case 1 ' integer, DataArea
                    Dim oField As PivotGridField = PivotGridControl1.Fields.Add(cFieldName, PivotArea.DataArea)
                    oField.Caption = cFieldName
                    oField.UnboundType = DevExpress.Data.UnboundColumnType.Bound
                    oField.Visible = False
                    oField.CellFormat.FormatType = DevExpress.Utils.FormatType.Custom
                    oField.CellFormat.FormatString = G_NumberFormat
                Case 2 ' String, RowArea
                    Dim oField As PivotGridField = PivotGridControl1.Fields.Add(cFieldName, PivotArea.RowArea)
                    oField.Caption = cFieldName
                    oField.UnboundType = DevExpress.Data.UnboundColumnType.Bound
                    oField.Visible = False
                Case 3 ' String, ColumnArea
                    Dim oField As PivotGridField = PivotGridControl1.Fields.Add(cFieldName, PivotArea.ColumnArea)
                    oField.Caption = cFieldName
                    oField.UnboundType = DevExpress.Data.UnboundColumnType.Bound
                    oField.Visible = False
                Case 4 ' DateTime, FilterArea
                    Dim oField As PivotGridField = PivotGridControl1.Fields.Add(cFieldName, PivotArea.FilterArea)
                    oField.Caption = cFieldName
                    oField.UnboundType = DevExpress.Data.UnboundColumnType.Bound
                    oField.Visible = False
                    oField.CellFormat.FormatType = DevExpress.Utils.FormatType.Custom
                    oField.CellFormat.FormatString = "d"
                Case 5 ' Decimal, DataArea
                    Dim oField As PivotGridField = PivotGridControl1.Fields.Add(cFieldName, PivotArea.DataArea)
                    oField.Caption = cFieldName
                    oField.UnboundType = DevExpress.Data.UnboundColumnType.Bound
                    oField.Visible = False
                    oField.CellFormat.FormatType = DevExpress.Utils.FormatType.Custom
                    oField.CellFormat.FormatString = G_Number2Format
            End Select

        Catch ex As Exception
            ErrDisp("AddPivotField", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub DevXDeleteReport(cReplClass As String, Optional ByVal cRepName As String = "")

        Dim oForm As New frmGetDevXReport
        Dim oSQL As SQLServerClass

        Try
            If lOLAP Then
                cRepName = cReportName.Trim
            End If

            If cRepName.Trim = "" Then
                oForm.init(cReplClass)
                cRepName = oForm.cResult
                oForm = Nothing
                If cRepName.Trim = "" Then Exit Sub
            End If

            oSQL = New SQLServerClass

            With oSQL
                .OpenConn()
                .cSQLQuery = "delete devxreports " +
                            " where reportname = '" + cRepName.Trim + "' " +
                            " and reportclass = '" + cReplClass.Trim + "' "
                .SQLExecute()
                .CloseConn()
            End With

            MsgBox("Rapor silindi : " + cRepName.Trim)

        Catch ex As Exception
            ErrDisp("DevXDeleteReport", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub DevXExportReport()

        Dim oFileDialogue As New SaveFileDialog
        Dim oForm As New frmGetDevXReport
        Dim cReportFileName As String = ""
        Dim oMemoryStream As MemoryStream = Nothing
        Dim oFileStream As FileStream = Nothing

        Try
            If lOLAP Then
                MsgBox("OLAP ta bu işlem kullanılamaz")
                Exit Sub
            End If

            If cReportName.Trim = "" Then
                cReportName = InputBox("Rapor adı", "Çıkış için boş bırakınız", cReportName.Trim)
                If cReportName.Trim = "" Then Exit Sub
            End If

            oFileDialogue.Title = "Choose Save Location"
            oFileDialogue.DefaultExt = ".plf"
            oFileDialogue.FileName = cRClass.Trim + "_" + cReportName.Trim
            oFileDialogue.ShowDialog()
            cReportFileName = Replace(oFileDialogue.FileName, ".plf", "")
            oFileDialogue = Nothing

            DestroyFile(cReportFileName.Trim + ".plf")
            DestroyFile(cReportFileName.Trim + ".pgf")
            DestroyFile(cReportFileName.Trim + ".pcf")
            DestroyFile(cReportFileName.Trim + ".cf")

            oMemoryStream = New MemoryStream
            PivotGridControl1.SaveLayoutToStream(oMemoryStream, DevExpress.Utils.OptionsLayoutBase.FullLayout)
            oFileStream = New FileStream(cReportFileName + ".plf", FileMode.Create, FileAccess.Write)
            oMemoryStream.WriteTo(oFileStream)
            oMemoryStream.Dispose()
            oFileStream.Dispose()

            oMemoryStream = New MemoryStream
            PivotGridControl1.SaveCollapsedStateToStream(oMemoryStream)
            oFileStream = New FileStream(cReportFileName + ".pcf", FileMode.Create, FileAccess.Write)
            oMemoryStream.WriteTo(oFileStream)
            oMemoryStream.Dispose()
            oFileStream.Dispose()

            oMemoryStream = New MemoryStream
            PivotGridControl1.SavePivotGridToStream(oMemoryStream)
            oFileStream = New FileStream(cReportFileName + ".pgf", FileMode.Create, FileAccess.Write)
            oMemoryStream.WriteTo(oFileStream)
            oMemoryStream.Dispose()
            oFileStream.Dispose()

            oMemoryStream = New MemoryStream
            ChartControl1.SaveToStream(oMemoryStream)
            oFileStream = New FileStream(cReportFileName + ".cf", FileMode.Create, FileAccess.Write)
            oMemoryStream.WriteTo(oFileStream)
            oMemoryStream.Dispose()
            oFileStream.Dispose()

        Catch ex As Exception
            ErrDisp("DevXExportReport", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub DevXImportReport()

        Dim oSQL As SQLServerClass
        Dim oDataTable As New DataTable
        Dim aPLF As Byte() = Nothing
        Dim aPGF As Byte() = Nothing
        Dim aPCF As Byte() = Nothing
        Dim aCF As Byte() = Nothing
        Dim cReportClass As String = ""
        Dim nPos As Integer = 0
        Dim oFileDialogue As New OpenFileDialog
        Dim cReportFileName As String = ""
        Dim cExtension As String = ""
        Dim oMemoryStream As MemoryStream = Nothing
        Dim oFile As FileInfo

        Try
            If lOLAP Then
                MsgBox("OLAP ta bu işlem kullanılamaz")
                Exit Sub
            End If

            oFileDialogue.Title = "Choose File To Read"
            oFileDialogue.DefaultExt = ""
            oFileDialogue.FileName = ""
            oFileDialogue.ShowDialog()
            cReportFileName = oFileDialogue.FileName
            oFile = New FileInfo(cReportFileName)
            cExtension = oFile.Extension
            cReportFileName = Replace(cReportFileName, cExtension, "")
            oFileDialogue = Nothing

            oMemoryStream = New MemoryStream
            oMemoryStream = FileToMemoryStream(cReportFileName + ".pgf")
            oMemoryStream.Seek(0, SeekOrigin.Begin)
            PivotGridControl1.DataSource = oMemoryStream
            ReDim aPGF(oMemoryStream.Length)
            oMemoryStream.Seek(0, System.IO.SeekOrigin.Begin)
            oMemoryStream.Read(aPGF, 0, aPGF.Length)
            oMemoryStream.Dispose()

            oMemoryStream = New MemoryStream
            oMemoryStream = FileToMemoryStream(cReportFileName + ".plf")
            oMemoryStream.Seek(0, SeekOrigin.Begin)
            PivotGridControl1.RestoreLayoutFromStream(oMemoryStream, DevExpress.Utils.OptionsLayoutBase.FullLayout)
            ReDim aPLF(oMemoryStream.Length)
            oMemoryStream.Seek(0, System.IO.SeekOrigin.Begin)
            oMemoryStream.Read(aPLF, 0, aPLF.Length)
            oMemoryStream.Dispose()

            oMemoryStream = New MemoryStream
            oMemoryStream = FileToMemoryStream(cReportFileName + ".pcf")
            oMemoryStream.Seek(0, SeekOrigin.Begin)
            PivotGridControl1.LoadCollapsedStateFromStream(oMemoryStream)
            ReDim aPCF(oMemoryStream.Length)
            oMemoryStream.Seek(0, System.IO.SeekOrigin.Begin)
            oMemoryStream.Read(aPCF, 0, aPCF.Length)
            oMemoryStream.Dispose()

            oMemoryStream = New MemoryStream
            oMemoryStream = FileToMemoryStream(cReportFileName + ".cf")
            oMemoryStream.Seek(0, SeekOrigin.Begin)
            ChartControl1.LoadFromStream(oMemoryStream)
            ReDim aCF(oMemoryStream.Length)
            oMemoryStream.Seek(0, System.IO.SeekOrigin.Begin)
            oMemoryStream.Read(aCF, 0, aCF.Length)
            oMemoryStream.Dispose()

            nPos = InStr(cReportFileName.Trim, "_")
            If nPos > 0 Then
                cReportClass = Mid(cReportFileName.Trim, 1, nPos - 1)
                cReportName = Mid(cReportFileName.Trim, nPos + 1)
            End If

            If cReportClass.Trim <> "" Then
                cReportClass = cRClass.Trim
            End If

            cReportName = InputBox("Rapor adı", "Çıkış için boş bırakınız", cReportName)
            If cReportName.Trim = "" Then Exit Sub

            cReportClass = InputBox("Rapor sınıfı", "Çıkış için boş bırakınız", cReportClass)
            If cReportClass.Trim = "" Then Exit Sub

            ChartControl1.DataSource = PivotGridControl1

            oSQL = New SQLServerClass

            With oSQL
                .OpenConn()
                .SaveDevXReport(cReportName, cReportClass, aPLF, aPCF, aPGF, aCF)
                .CloseConn()
            End With

            oSQL = New SQLServerClass(False, cServer, cDatabase, cUsername, cPassword)

            With oSQL
                .OpenConn()

                oDataTable = .GetDataTableFromView(cViewName)

                ' PivotGrid and Chart
                PivotGridControl1.DataSource = oDataTable
                PivotGridControl1.Refresh()

                ' Chart
                ChartControl1.DataSource = PivotGridControl1
                ChartControl1.Refresh()

                ' List
                GridControl1.DataSource = oDataTable
                GridControl1.Refresh()

                .CloseConn()
            End With

            MsgBox("Rapor import edildi : " + cReportName.Trim)

        Catch ex As Exception
            ErrDisp("DevXImportReport", Me.Name,,, ex)
        End Try
    End Sub

    Private Function FileToMemoryStream(cFileName As String) As MemoryStream

        FileToMemoryStream = Nothing

        Try
            Dim bData As Byte()
            Dim br As BinaryReader = New BinaryReader(System.IO.File.OpenRead(cFileName.Trim))
            bData = br.ReadBytes(br.BaseStream.Length)
            Dim ms As MemoryStream = New MemoryStream(bData, 0, bData.Length)
            ms.Write(bData, 0, bData.Length)
            FileToMemoryStream = ms

        Catch ex As Exception
            ErrDisp("FileToMemoryStream", Me.Name,,, ex)
        End Try
    End Function

    Private Sub DevXSaveCurrentReportToFile(cRepName As String, cRepClass As String)

        Dim oSQL As SQLServerClass
        Dim cRecID As String = ""
        Dim cReportFileName As String = ""
        Dim oMemoryStream As MemoryStream = Nothing
        Dim oFileStream As FileStream = Nothing

        Try
            If lOLAP Then Exit Sub

            cRepName = InputBox("Rapor adı", "Çıkış için boş bırakınız", cRepName.Trim)
            If cRepName.Trim = "" Then Exit Sub

            cReportFileName = GetTempFile("plf", "devx", , "devxreports", True)

            DestroyFile(cReportFileName + ".plf")
            DestroyFile(cReportFileName + ".pcf")
            DestroyFile(cReportFileName + ".pgf")
            DestroyFile(cReportFileName + ".cf")

            oMemoryStream = New MemoryStream
            PivotGridControl1.SaveLayoutToStream(oMemoryStream, DevExpress.Utils.OptionsLayoutBase.FullLayout)
            oFileStream = New FileStream(cReportFileName + ".plf", FileMode.Create, FileAccess.Write)
            oMemoryStream.WriteTo(oFileStream)
            oMemoryStream.Dispose()
            oFileStream.Dispose()

            oMemoryStream = New MemoryStream
            PivotGridControl1.SaveCollapsedStateToStream(oMemoryStream)
            oFileStream = New FileStream(cReportFileName + ".pcf", FileMode.Create, FileAccess.Write)
            oMemoryStream.WriteTo(oFileStream)
            oMemoryStream.Dispose()
            oFileStream.Dispose()

            oMemoryStream = New MemoryStream
            PivotGridControl1.SavePivotGridToStream(oMemoryStream)
            oFileStream = New FileStream(cReportFileName + ".pgf", FileMode.Create, FileAccess.Write)
            oMemoryStream.WriteTo(oFileStream)
            oMemoryStream.Dispose()
            oFileStream.Dispose()

            oMemoryStream = New MemoryStream
            ChartControl1.SaveToStream(oMemoryStream)
            oFileStream = New FileStream(cReportFileName + ".cf", FileMode.Create, FileAccess.Write)
            oMemoryStream.WriteTo(oFileStream)
            oMemoryStream.Dispose()
            oFileStream.Dispose()

            oSQL = New SQLServerClass

            With oSQL
                .OpenConn()

                cRecID = .GetFisNo("reportid")

                .cSQLQuery = "delete devxreports " +
                            " where reportname = '" + cRepName.Trim + "' " +
                            " and reportclass = '" + cRepClass.Trim + "' "
                .SQLExecute()

                .cSQLQuery = "insert into devxreports (reportid, reportname, reportclass, reportfilepath)  " +
                            " values ('" + cRecID.Trim + "', " +
                            " '" + SQLWriteString(cRepName.Trim) + "', " +
                            " '" + SQLWriteString(cRepClass.Trim) + "'," +
                            " '" + cReportFileName.Trim + "') "
                .SQLExecute()

                .CloseConn()
            End With

            MsgBox("Rapor kaydedildi : " + cReportFileName.Trim)

        Catch ex As Exception
            ErrDisp("DevXSaveReport", Me.Name,,, ex)
        End Try
    End Sub

    Private Function DevXLoadFileToCurrentReport(cRepClass As String) As String

        Dim oSQL As SQLServerClass
        Dim oForm As New frmGetDevXReport
        Dim oDataTable As New DataTable
        Dim cRepName As String = ""
        Dim cReportFileName As String = ""
        Dim oPLFStream As New MemoryStream()
        Dim oPCFStream As New MemoryStream()
        Dim oPGFStream As New MemoryStream()
        Dim oCFStream As New MemoryStream()

        DevXLoadFileToCurrentReport = ""

        Try
            If lOLAP Then Exit Function
            If cRepClass.Trim = "" Then Exit Function
            oForm.init(cRepClass)
            cRepName = oForm.cResult
            oForm = Nothing
            If cRepName.Trim = "" Then Exit Function

            oSQL = New SQLServerClass

            With oSQL
                .OpenConn()

                .cSQLQuery = "select reportfilepath " +
                            " from devxreports with (NOLOCK) " +
                            " where reportname = '" + cRepName.Trim + "' " +
                            " and reportclass = '" + cRepClass.Trim + "' "

                cReportFileName = .DBReadString

                .CloseConn()
            End With

            oPGFStream = FileToMemoryStream(cReportFileName + ".pgf")
            oPGFStream.Seek(0, SeekOrigin.Begin)
            PivotGridControl1.DataSource = oPGFStream

            oPLFStream = FileToMemoryStream(cReportFileName + ".plf")
            oPLFStream.Seek(0, SeekOrigin.Begin)
            PivotGridControl1.RestoreLayoutFromStream(oPLFStream, DevExpress.Utils.OptionsLayoutBase.FullLayout)

            oPCFStream = FileToMemoryStream(cReportFileName + ".pcf")
            oPCFStream.Seek(0, SeekOrigin.Begin)
            PivotGridControl1.LoadCollapsedStateFromStream(oPCFStream)

            oCFStream = FileToMemoryStream(cReportFileName + ".cf")
            oCFStream.Seek(0, SeekOrigin.Begin)
            ChartControl1.LoadFromStream(oCFStream)

            oSQL = New SQLServerClass(False, cServer, cDatabase, cUsername, cPassword)

            With oSQL
                .OpenConn()

                oDataTable = .GetDataTableFromView(cViewName)

                ChartControl1.DataSource = PivotGridControl1

                ' PivotGrid and Chart
                PivotGridControl1.DataSource = oDataTable
                PivotGridControl1.Refresh()

                ' Chart
                ChartControl1.DataSource = PivotGridControl1
                ChartControl1.Refresh()

                ' List
                GridControl1.DataSource = oDataTable
                GridControl1.Refresh()

                DevXLoadFileToCurrentReport = cRepName.Trim

                .CloseConn()
            End With

            MsgBox("Rapor okundu : " + cRepName.Trim)

        Catch ex As Exception
            ErrDisp("DevXLoadReport", Me.Name,,, ex)
        End Try
    End Function

    Private Sub DevXSaveReport()
        ' save DevXReport to Database
        Dim oSQL As New SQLServerClass
        Dim aPLF As Byte() = Nothing
        Dim aPGF As Byte() = Nothing
        Dim aPCF As Byte() = Nothing
        Dim aCF As Byte() = Nothing
        Dim oMemoryStream As MemoryStream = Nothing

        Try
            If cReportName.Trim = "" Then
                cReportName = InputBox("Rapor adı", "Çıkış için boş bırakınız", cReportName.Trim)
                If cReportName.Trim = "" Then Exit Sub
            End If

            If cRClass.Trim = "" Then
                cRClass = InputBox("Rapor sınıfı", "Çıkış için boş bırakınız", cRClass.Trim)
                If cRClass.Trim = "" Then Exit Sub
            End If

            oMemoryStream = New MemoryStream
            PivotGridControl1.SaveLayoutToStream(oMemoryStream, DevExpress.Utils.OptionsLayoutBase.FullLayout)
            ReDim aPLF(oMemoryStream.Length)
            oMemoryStream.Seek(0, System.IO.SeekOrigin.Begin)
            oMemoryStream.Read(aPLF, 0, aPLF.Length)
            oMemoryStream.Dispose()

            oMemoryStream = New MemoryStream
            PivotGridControl1.SaveCollapsedStateToStream(oMemoryStream)
            ReDim aPCF(oMemoryStream.Length)
            oMemoryStream.Seek(0, System.IO.SeekOrigin.Begin)
            oMemoryStream.Read(aPCF, 0, aPCF.Length)
            oMemoryStream.Dispose()

            oMemoryStream = New MemoryStream
            PivotGridControl1.SavePivotGridToStream(oMemoryStream)
            ReDim aPGF(oMemoryStream.Length)
            oMemoryStream.Seek(0, System.IO.SeekOrigin.Begin)
            oMemoryStream.Read(aPGF, 0, aPGF.Length)
            oMemoryStream.Dispose()

            oMemoryStream = New MemoryStream
            ChartControl1.SaveToStream(oMemoryStream)
            ReDim aCF(oMemoryStream.Length)
            oMemoryStream.Seek(0, System.IO.SeekOrigin.Begin)
            oMemoryStream.Read(aCF, 0, aCF.Length)
            oMemoryStream.Dispose()

            oSQL.OpenConn()
            oSQL.SaveDevXReport(cReportName, cRClass, aPLF, aPCF, aPGF, aCF)
            oSQL.CloseConn()

            If Not lOLAP Then
                MsgBox("Rapor kaydedildi : " + cReportName.Trim)
            End If

        Catch ex As Exception
            ErrDisp("DevXSaveReport", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub DevXRestoreReport(Optional lGetFirstReport As Boolean = False)
        ' restore DevXReport from Database
        Dim oSQL As SQLServerClass
        Dim oForm As New frmGetDevXReport
        Dim aPLF As Byte() = Nothing
        Dim aPGF As Byte() = Nothing
        Dim aPCF As Byte() = Nothing
        Dim aCF As Byte() = Nothing
        Dim oMemoryStream As MemoryStream = Nothing
        Dim lOK As Boolean = False

        Try
            If lGetFirstReport Then

                oSQL = New SQLServerClass

                With oSQL
                    .OpenConn()

                    .cSQLQuery = "select reportid, reportname, reportclass, fileplf, filepgf, filepcf, filecf " +
                            " from devxreports with (NOLOCK) " +
                            " where reportclass = '" + cRClass.Trim + "' " +
                            " order by reportid "

                    .GetSQLReader()

                    If .oReader.Read Then
                        cReportName = SQLReadString(oSQL.oReader, "reportname")
                        aPLF = CType(.oReader("fileplf"), Byte())
                        aPGF = CType(.oReader("filepgf"), Byte())
                        aPCF = CType(.oReader("filepcf"), Byte())
                        aCF = CType(.oReader("filecf"), Byte())
                        lOK = True
                    End If
                    .oReader.Close()

                    .CloseConn()
                End With
            Else
                oForm.init(cRClass)
                cReportName = oForm.cResult
                oForm = Nothing
                If cReportName.Trim = "" Then Exit Sub

                oSQL = New SQLServerClass

                With oSQL
                    .OpenConn()

                    .cSQLQuery = "select reportid, reportname, reportclass, fileplf, filepgf, filepcf, filecf " +
                            " from devxreports with (NOLOCK) " +
                            " where reportname = '" + cReportName.Trim + "' " +
                            " and reportclass = '" + cRClass.Trim + "' " +
                            " order by reportid "

                    .GetSQLReader()

                    If .oReader.Read Then
                        aPLF = CType(.oReader("fileplf"), Byte())
                        aPGF = CType(.oReader("filepgf"), Byte())
                        aPCF = CType(.oReader("filepcf"), Byte())
                        aCF = CType(.oReader("filecf"), Byte())
                        lOK = True
                    End If
                    .oReader.Close()

                    .CloseConn()
                End With
            End If

            If Not lOK Then Exit Sub

            oMemoryStream = New MemoryStream(aPGF)
            oMemoryStream.Seek(0, System.IO.SeekOrigin.Begin)
            PivotGridControl1.DataSource = oMemoryStream
            oMemoryStream.Dispose()

            oMemoryStream = New MemoryStream(aPLF)
            oMemoryStream.Seek(0, System.IO.SeekOrigin.Begin)
            PivotGridControl1.RestoreLayoutFromStream(oMemoryStream, DevExpress.Utils.OptionsLayoutBase.FullLayout)
            oMemoryStream.Dispose()

            oMemoryStream = New MemoryStream(aPCF)
            oMemoryStream.Seek(0, System.IO.SeekOrigin.Begin)
            PivotGridControl1.LoadCollapsedStateFromStream(oMemoryStream)
            oMemoryStream.Dispose()

            oMemoryStream = New MemoryStream(aCF)
            oMemoryStream.Seek(0, System.IO.SeekOrigin.Begin)
            ChartControl1.LoadFromStream(oMemoryStream)
            oMemoryStream.Dispose()

            oSQL = New SQLServerClass(False, cServer, cDatabase, cUsername, cPassword)

            With oSQL
                .OpenConn()

                ChartControl1.DataSource = PivotGridControl1

                ' PivotGrid and Chart
                PivotGridControl1.DataSource = .GetDataTableFromView(cViewName)
                PivotGridControl1.Refresh()
                ' Chart
                ChartControl1.DataSource = PivotGridControl1
                ChartControl1.Refresh()
                ' List
                GridControl1.DataSource = Nothing
                GridControl1.Refresh()

                .CloseConn()
            End With

            MsgBox("Rapor okundu : " + cReportName.Trim)

        Catch ex As Exception
            ErrDisp("DevXRestoreReport", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        ' pivot alanlarını göster
        On Error Resume Next
        Me.PivotGridControl1.FieldsCustomization()
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        ' pivot yazdır
        On Error Resume Next
        Me.PivotGridControl1.ShowRibbonPrintPreview()
    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        ' chart
        Try
            Dim oChart As ChartControl
            Dim oWiz As ChartDesigner

            oChart = Me.ChartControl1
            oWiz = New ChartDesigner(oChart)
            oWiz.ShowDialog()
            oChart.Refresh()

        Catch ex As Exception
            ErrDisp("BarButtonItem3_ItemClick", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub BarButtonItem4_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem4.ItemClick
        ' grafik yazdır
        On Error Resume Next
        Me.ChartControl1.ShowRibbonPrintPreview()
    End Sub

    Private Sub BarButtonItem5_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem5.ItemClick
        On Error Resume Next
        GetGridData()
    End Sub

    Private Sub BarButtonItem6_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem6.ItemClick
        ' Print
        On Error Resume Next
        Me.GridControl1.ShowRibbonPrintPreview()
    End Sub

    Private Sub BarButtonItem7_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem7.ItemClick
        ' save to database
        On Error Resume Next
        DevXSaveReport()
    End Sub

    Private Sub BarButtonItem8_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem8.ItemClick
        ' get from database
        On Error Resume Next
        DevXRestoreReport()
    End Sub

    Private Sub BarButtonItem9_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem9.ItemClick
        ' rapor sil
        On Error Resume Next
        DevXDeleteReport(cRClass)
    End Sub

    Private Sub BarButtonItem10_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem10.ItemClick
        ' import devx report
        On Error Resume Next
        DevXImportReport()
    End Sub

    Private Sub BarButtonItem11_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem11.ItemClick
        ' export devx report
        On Error Resume Next
        DevXExportReport()
    End Sub

    Private Sub BarButtonItem12_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem12.ItemClick
        ' refresh
        On Error Resume Next
        If lOLAP Then
            GetOLAPData()
        Else
            GetData(True)
        End If
    End Sub

    Private Sub BarButtonItem13_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem13.ItemClick
        On Error Resume Next
        Me.Close()
    End Sub

    Private Sub PivotGridControl1_CustomCellDisplayText(sender As Object, e As PivotCellDisplayTextEventArgs) Handles PivotGridControl1.CustomCellDisplayText

        On Error Resume Next

        If Not IsNothing(e.Value) Then

            Select Case e.Value.GetType
                Case GetType(System.Int16), GetType(System.Int32), GetType(System.Int64)
                    e.DisplayText = Strings.FormatNumber(e.Value, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Case GetType(System.Decimal), GetType(System.Int32), GetType(System.Double), GetType(System.Object)
                    e.DisplayText = Strings.FormatNumber(e.Value, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            End Select

            Select Case e.DataField.Caption
                Case "SLSRT", "WK3_P", "WK6_P"
                    e.DisplayText = Strings.FormatPercent(e.Value)
            End Select
        End If
    End Sub

    Private Sub PivotGridControl1_CustomDrawFieldValue(sender As Object, e As PivotCustomDrawFieldValueEventArgs) Handles PivotGridControl1.CustomDrawFieldValue

        On Error Resume Next

        Select Case e.ValueType
            Case PivotGridValueType.Value
                Select Case e.Area
                    Case PivotArea.ColumnArea
                        e.Appearance.BackColor = Color.LightYellow
                    Case PivotArea.RowArea
                        e.Appearance.BackColor = Color.LightGreen
                End Select
            Case PivotGridValueType.Total
                e.Appearance.BackColor = Color.Yellow
            Case PivotGridValueType.GrandTotal
                e.Appearance.BackColor = Color.LightBlue
        End Select
    End Sub

    Private Sub BarButtonItem14_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem14.ItemClick

        Try
            If Not lOLAP Then Exit Sub
            ' pivot layout unun sıfırla 
            GetOLAPData()
            PivotGridControl1.RestoreLayoutFromRegistry("WinTex\\OLAP\\OlapGridNull", DevExpress.Utils.OptionsLayoutBase.FullLayout)
            PivotGridControl1.Refresh()

        Catch ex As Exception
            ErrDisp("BarButtonItem14_ItemClick", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub OLAPCreateReturnValue()
        Try
            Dim nRow As Integer = 0
            Dim nCol As Integer = 0
            Dim nCnt As Integer = 0
            Dim cCellValue As String = ""
            Dim nCellValue As Double = 0
            Dim nRowSum As Double = 0
            Dim oCells As PivotGridCells = PivotGridControl1.Cells
            Dim oCellSelection As Rectangle = oCells.Selection
            Dim oInfo As PivotCellEventArgs
            Dim cBeden As String = ""
            Dim oRowFields() As PivotGridField
            Dim aSonuc() As oSonuc
            Dim nFound As Integer = 0
            Dim cTipi As String = ""
            Dim lOK As Boolean = False

            Dim nMaxRow As Integer = oCellSelection.Y + oCellSelection.Height - 1
            Dim nMaxCol As Integer = oCellSelection.X + oCellSelection.Width - 1

            ReDim aSonuc(0)
            cSonuc = ""

            For nRow = oCellSelection.Y To nMaxRow
                For nCol = oCellSelection.X To nMaxCol
                    cBeden = ""
                    nCellValue = 0

                    oInfo = oCells.GetCellInfo(nCol, nRow)

                    cTipi = oInfo.DataField.Caption

                    oRowFields = oInfo.GetRowFields()
                    For nCnt = 0 To oRowFields.Count - 1
                        If oRowFields(nCnt).Caption = "SIZE" Then
                            cBeden = PivotGridControl1.GetFieldValue(oRowFields(nCnt), nRow).ToString
                            Exit For
                        End If
                    Next

                    cCellValue = oInfo.DisplayText
                    If IsNumeric(cCellValue) Then
                        nCellValue = Math.Round(CDbl(cCellValue), 0)
                    End If

                    If cTipi.Trim <> "" And cBeden.Trim <> "" And nCellValue <> 0 Then

                        lOK = True

                        If aSonuc(0).cBeden = "" Then
                            aSonuc(0).cTipi = cTipi.Trim
                            aSonuc(0).cBeden = cBeden.Trim
                            aSonuc(0).nAdet = nCellValue
                        Else
                            nFound = -1
                            For nCnt = 0 To UBound(aSonuc)
                                If aSonuc(nCnt).cBeden = cBeden And aSonuc(nCnt).cTipi = cTipi Then
                                    nFound = nCnt
                                    Exit For
                                End If
                            Next
                            If nFound = -1 Then
                                nFound = UBound(aSonuc) + 1
                                ReDim Preserve aSonuc(nFound)
                                aSonuc(nFound).cTipi = cTipi
                                aSonuc(nFound).cBeden = cBeden
                                aSonuc(nFound).nAdet = nCellValue
                            Else
                                aSonuc(nFound).nAdet = aSonuc(nFound).nAdet + nCellValue
                            End If
                        End If
                    End If
                Next

                For nCol = 0 To PivotGridControl1.Cells.ColumnCount
                    cBeden = ""
                    nCellValue = 0

                    If Not IsNothing(oCells.GetCellInfo(nCol, nRow)) Then

                        oInfo = oCells.GetCellInfo(nCol, nRow)

                        cTipi = oInfo.DataField.Caption

                        If (cTipi.Trim = "RETSL" Or cTipi.Trim = "RETSHP" Or cTipi.Trim = "RETST") Then

                            oRowFields = oInfo.GetRowFields()
                            For nCnt = 0 To oRowFields.Count - 1
                                If oRowFields(nCnt).Caption = "SIZE" Then
                                    cBeden = PivotGridControl1.GetFieldValue(oRowFields(nCnt), nRow).ToString
                                    Exit For
                                End If
                            Next

                            cCellValue = oInfo.DisplayText
                            If IsNumeric(cCellValue) Then
                                nCellValue = Math.Round(CDbl(cCellValue), 0)
                            End If

                            If cTipi.Trim <> "" And cBeden.Trim <> "" And nCellValue <> 0 Then

                                If aSonuc(0).cBeden = "" Then
                                    aSonuc(0).cTipi = cTipi.Trim
                                    aSonuc(0).cBeden = cBeden.Trim
                                    aSonuc(0).nAdet = nCellValue
                                Else
                                    nFound = -1
                                    For nCnt = 0 To UBound(aSonuc)
                                        If aSonuc(nCnt).cBeden = cBeden And aSonuc(nCnt).cTipi = cTipi Then
                                            nFound = nCnt
                                            Exit For
                                        End If
                                    Next
                                    If nFound = -1 Then
                                        nFound = UBound(aSonuc) + 1
                                        ReDim Preserve aSonuc(nFound)
                                        aSonuc(nFound).cTipi = cTipi
                                        aSonuc(nFound).cBeden = cBeden
                                        aSonuc(nFound).nAdet = nCellValue
                                    Else
                                        aSonuc(nFound).nAdet = aSonuc(nFound).nAdet + nCellValue
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next
            Next

            If lOK Then
                For nCnt = 0 To UBound(aSonuc)
                    If cSonuc = "" Then
                        cSonuc = aSonuc(nCnt).cTipi + ":" + aSonuc(nCnt).cBeden + ":" + SQLWriteString(aSonuc(nCnt).nAdet)
                    Else
                        cSonuc = cSonuc + ";" + aSonuc(nCnt).cTipi + ":" + aSonuc(nCnt).cBeden + ":" + SQLWriteString(aSonuc(nCnt).nAdet)
                    End If
                Next
            End If

        Catch ex As Exception
            ErrDisp("OLAPCreateReturnValue", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub DevXReports_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        Try
            If lOLAP Then
                ' seçim yapıldı mı yapılmadı mı
                If PivotGridControl1.Cells.Selection.Width = 0 And PivotGridControl1.Cells.Selection.Height = 0 Then
                    If Confirmed("Seçim yapılmamış yine de çıkılacaktır", Me) Then
                        e.Cancel = False
                    Else
                        e.Cancel = True
                    End If
                    ' seçim yapılmadı ise hiç bir şey kaydetme 
                    cSonuc = ""
                    Exit Sub
                End If
                ' seçim yapıldıysa  
                ' sonuç değeri oluştur
                OLAPCreateReturnValue()
                ' pivot layout kaydet
                PivotGridControl1.SaveLayoutToRegistry(cRegKey)
                ' rapor verilerini kaydet
                DevXSaveReport()
            End If

        Catch ex As Exception
            ErrDisp("DevXReports_FormClosing", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub DevXReports_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        DropView(cViewName)
    End Sub

    Private Sub BarButtonItem15_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem15.ItemClick
        On Error Resume Next
        PivotGridControl1.ExpandAll()
    End Sub

    Private Sub BarButtonItem16_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem16.ItemClick
        On Error Resume Next
        PivotGridControl1.BestFit()
    End Sub

    Private Sub BarButtonItem17_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem17.ItemClick
        On Error Resume Next
        OLAP6WeeksStyle(True)
    End Sub

    Private Sub BarButtonItem18_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem18.ItemClick
        On Error Resume Next
        RestoreLayout()
    End Sub

    Private Sub BarButtonItem19_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem19.ItemClick
        On Error Resume Next

        Dim path As String = "wintex.xlsx"
        ' Open the created XLSX file with the default application.
        DestroyFile(path)
        GridControl1.ExportToXlsx(path)
        Process.Start(path)
    End Sub
End Class


