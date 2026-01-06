Option Explicit On
'Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.IO

Imports Microsoft.SqlServer.Server
Imports Microsoft.InteropFormTools

Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Sql
Imports DevExpress.XtraEditors.Repository

Module UtilDXGrid

    Public Sub DXInitGridView(ByRef oGridControl As GridControl, ByRef oGridView As GridView, ByVal oParentControl As Control,
                              Optional lMultiSelect As Boolean = True)
        Try

            oGridView.OptionsBehavior.Editable = True
            oGridView.OptionsBehavior.ReadOnly = False
            oGridView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True
            oGridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True
            oGridView.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.True
            oGridView.OptionsBehavior.AlignGroupSummaryInGroupRow = DevExpress.Utils.DefaultBoolean.True
            oGridView.OptionsBehavior.AllowGroupExpandAnimation = DevExpress.Utils.DefaultBoolean.True
            oGridView.OptionsBehavior.AllowIncrementalSearch = True
            oGridView.OptionsBehavior.AllowPartialGroups = DevExpress.Utils.DefaultBoolean.True
            oGridView.OptionsBehavior.AllowPartialRedrawOnScrolling = True
            oGridView.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True
            oGridView.OptionsBehavior.AllowSortAnimation = DevExpress.Utils.DefaultBoolean.True
            oGridView.OptionsBehavior.AllowValidationErrors = True

            oGridView.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.True
            oGridView.OptionsClipboard.AllowCsvFormat = DevExpress.Utils.DefaultBoolean.True
            oGridView.OptionsClipboard.AllowExcelFormat = DevExpress.Utils.DefaultBoolean.True
            oGridView.OptionsClipboard.AllowHtmlFormat = DevExpress.Utils.DefaultBoolean.True
            oGridView.OptionsClipboard.AllowRtfFormat = DevExpress.Utils.DefaultBoolean.True
            oGridView.OptionsClipboard.AllowTxtFormat = DevExpress.Utils.DefaultBoolean.True
            oGridView.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Append

            oGridView.OptionsCustomization.AllowColumnMoving = True
            oGridView.OptionsCustomization.AllowColumnResizing = True
            oGridView.OptionsCustomization.AllowFilter = True
            oGridView.OptionsCustomization.AllowGroup = True
            oGridView.OptionsCustomization.AllowMergedGrouping = DevExpress.Utils.DefaultBoolean.True
            oGridView.OptionsCustomization.AllowQuickHideColumns = True
            oGridView.OptionsCustomization.AllowRowSizing = True
            oGridView.OptionsCustomization.AllowSort = True

            oGridView.OptionsFilter.AllowAutoFilterConditionChange = DevExpress.Utils.DefaultBoolean.True
            oGridView.OptionsFilter.AllowColumnMRUFilterList = True
            oGridView.OptionsFilter.AllowFilterEditor = True
            oGridView.OptionsFilter.AllowFilterIncrementalSearch = True
            oGridView.OptionsFilter.AllowMRUFilterList = True
            oGridView.OptionsFilter.AllowMultiSelectInCheckedFilterPopup = True
            oGridView.OptionsFilter.ShowAllTableValuesInCheckedFilterPopup = True
            oGridView.OptionsFilter.ShowAllTableValuesInFilterPopup = True

            oGridView.OptionsFind.AllowFindPanel = True
            oGridView.OptionsFind.AllowMruItems = True
            oGridView.OptionsFind.ShowClearButton = True
            oGridView.OptionsFind.ShowCloseButton = True
            oGridView.OptionsFind.ShowFindButton = True
            oGridView.OptionsFind.ShowSearchNavButtons = True

            oGridView.OptionsMenu.EnableColumnMenu = True
            oGridView.OptionsMenu.EnableFooterMenu = True
            oGridView.OptionsMenu.EnableGroupPanelMenu = True
            oGridView.OptionsMenu.EnableGroupRowMenu = True
            oGridView.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.True
            oGridView.OptionsMenu.ShowAutoFilterRowItem = True
            oGridView.OptionsMenu.ShowConditionalFormattingItem = True
            oGridView.OptionsMenu.ShowDateTimeGroupIntervalItems = True
            oGridView.OptionsMenu.ShowFooterItem = True
            oGridView.OptionsMenu.ShowGroupSortSummaryItems = True
            oGridView.OptionsMenu.ShowGroupSummaryEditorItem = True
            oGridView.OptionsMenu.ShowSplitItem = True
            oGridView.OptionsMenu.ShowSummaryItemMode = DevExpress.Utils.DefaultBoolean.True

            oGridView.OptionsNavigation.AutoFocusNewRow = True
            oGridView.OptionsNavigation.AutoMoveRowFocus = True
            oGridView.OptionsNavigation.EnterMoveNextColumn = True
            oGridView.OptionsNavigation.UseOfficePageNavigation = True
            oGridView.OptionsNavigation.UseTabKey = True

            If lMultiSelect Then
                oGridView.OptionsSelection.MultiSelect = True
                oGridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect
                oGridView.OptionsSelection.ResetSelectionClickOutsideCheckboxSelector = True
                oGridView.OptionsSelection.CheckBoxSelectorColumnWidth = 80
                oGridView.OptionsSelection.ShowCheckBoxSelectorChangesSelectionNavigation = DevExpress.Utils.DefaultBoolean.True
                oGridView.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True
                oGridView.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True
                oGridView.OptionsSelection.ShowCheckBoxSelectorInPrintExport = DevExpress.Utils.DefaultBoolean.False
            End If

            oGridView.OptionsSelection.EnableAppearanceFocusedCell = True
            oGridView.OptionsSelection.EnableAppearanceFocusedRow = True
            oGridView.OptionsSelection.EnableAppearanceHideSelection = True
            oGridView.OptionsSelection.EnableAppearanceHotTrackedRow = DevExpress.Utils.DefaultBoolean.True
            oGridView.OptionsSelection.UseIndicatorForSelection = True

            oGridView.OptionsScrollAnnotations.ShowCustomAnnotations = DevExpress.Utils.DefaultBoolean.True
            oGridView.OptionsScrollAnnotations.ShowErrors = DevExpress.Utils.DefaultBoolean.True
            oGridView.OptionsScrollAnnotations.ShowFocusedRow = DevExpress.Utils.DefaultBoolean.True
            oGridView.OptionsScrollAnnotations.ShowSelectedRows = DevExpress.Utils.DefaultBoolean.True

            oGridView.OptionsLayout.StoreAllOptions = True

            oGridView.OptionsView.RowAutoHeight = DevExpress.Utils.DefaultBoolean.True
            oGridView.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.True
            oGridView.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.True
            oGridView.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True
            oGridView.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom

            'oGridView.OptionsView.BestFitMaxRowCount = 100
            'oGridView.OptionsView.BestFitMode = GridBestFitMode.Fast

            oGridControl.UseEmbeddedNavigator = True

            oGridControl.MainView = oGridView
            oGridControl.Parent = oParentControl
            oGridControl.Dock = DockStyle.Fill

        Catch ex As Exception
            ErrDisp(ex.Message, "DXInitGridView",  ,, ex)
        End Try
    End Sub

    Public Function DXGetGridDataSource(ByVal cSQL As String, ByRef oGridControl As DevExpress.XtraGrid.GridControl) As Boolean

        DXGetGridDataSource = False

        Try
            Dim oConnParam As MsSqlConnectionParameters
            Dim oQuery As CustomSqlQuery
            Dim oDS As SqlDataSource

            oConnParam = New MsSqlConnectionParameters
            oConnParam.ServerName = oConnection.cServer
            oConnParam.DatabaseName = oConnection.cDatabase
            oConnParam.UserName = oConnection.cUser
            oConnParam.Password = oConnection.cPassword
            oConnParam.AuthorizationType = MsSqlAuthorizationType.SqlServer

            oQuery = New CustomSqlQuery("customQuery1", cSQL)

            oDS = New SqlDataSource(oConnParam)
            oDS.Queries.Add(oQuery)
            oDS.Fill()

            oGridControl.DataSource = oDS
            oGridControl.DataMember = oQuery.Name

            DXGetGridDataSource = True

        Catch ex As Exception
            ErrDisp(ex.Message, "DXGetGridDataSource", cSQL,, ex)
        End Try
    End Function

End Module
