Imports DevExpress.DashboardCommon
Imports Microsoft.InteropFormTools
Imports DevExpress.DashboardWin
Imports DevExpress.DataAccess.Sql
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.XtraPivotGrid
Imports DevExpress.XtraCharts
Imports DevExpress.XtraCharts.Wizard
Imports System.IO
Imports DevExpress.DataAccess
Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports DevExpress.XtraCharts.Designer

<InteropForm()> Public Class frmDashboardViewer

    Dim nRefreshTime As Double = 600
    Dim nDakika As Integer = 0
    Dim cViewName As String = ""
    Dim aTempTables() As String
    Dim cSQLx As String = ""

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        AddHandler SqlDataSource.ValidateCustomSqlQueryGlobal, AddressOf SqlDataSource_ValidateCustomSqlQueryGlobal
    End Sub

    Public Sub init()
        Try
            ReDim aTempTables(0)
            LoadXMLFile()
            Me.Show()
        Catch ex As Exception
            ErrDisp("init : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub frmDashboardViewer_Load(sender As Object, e As EventArgs) Handles Me.Load

        Timer1.Enabled = False
        DashboardViewer1.AllowPrintDashboard = True
        DashboardViewer1.AllowPrintDashboardItems = True
        XtraTabControl1.SelectedTabPageIndex = 0
        Me.WindowState = FormWindowState.Maximized

    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        Try
            Me.Close()
        Catch ex As Exception
            ErrDisp("Cikis : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        Try
            LoadXMLFile()
        Catch ex As Exception
            ErrDisp("YenidenYukle : " + ex.Message, Me.Name)
        End Try
    End Sub

    'Private Sub dashboardViewer1_DashboardItemDoubleClick(ByVal sender As Object, ByVal e As DashboardItemMouseActionEventArgs) Handles DashboardViewer1.DashboardItemDoubleClick
    '    Try
    '        Dim underlyingData As DashboardUnderlyingDataSet = e.GetUnderlyingData()

    '        If underlyingData IsNot Nothing Then
    '            Dim form As New XtraForm()
    '            form.Text = "Detay Kayıtlar"
    '            Dim grid As New DataGrid()
    '            grid.Parent = form
    '            grid.Dock = DockStyle.Fill
    '            grid.DataSource = underlyingData
    '            form.ShowDialog()
    '            form.Dispose()
    '        End If

    '    Catch ex As Exception
    '        ErrDisp("Detay : " + ex.Message, Me.Name)
    '    End Try
    'End Sub

    Private Sub BarButtonItem4_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem4.ItemClick
        Try
            SaveFileDialog1.Filter = "PDF Adobe|*.pdf"
            SaveFileDialog1.Title = "PDF Dosyası Kaydet"
            SaveFileDialog1.ShowDialog()

            ' If the file name is not an empty string open it for saving.
            If SaveFileDialog1.FileName <> "" Then
                DashboardViewer1.ExportToPdf(SaveFileDialog1.FileName)
            End If

        Catch ex As Exception
            ErrDisp("ExportPDF : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub BarButtonItem5_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem5.ItemClick
        Try
            SaveFileDialog1.Filter = "JPeg Image|*.jpg"
            SaveFileDialog1.Title = "Resim Dosyası Kaydet"
            SaveFileDialog1.ShowDialog()

            ' If the file name is not an empty string open it for saving.
            If SaveFileDialog1.FileName <> "" Then
                DashboardViewer1.ExportToImage(SaveFileDialog1.FileName)
            End If

        Catch ex As Exception
            ErrDisp("ExportJPG : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub BarButtonItem6_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem6.ItemClick
        Try
            Dim cTempFile As String = ""
            Dim oForm As New AdvancedEditor

            cTempFile = GetTempFile("xml")
            My.Computer.FileSystem.WriteAllText(cTempFile, oReportDevX.cReport, False)
            oForm.init(cTempFile)
            DestroyFile(cTempFile)

        Catch ex As Exception
            ErrDisp("Source : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub BarButtonItem7_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem7.ItemClick
        Try
            RefreshDashboard()
        Catch ex As Exception
            ErrDisp("YenidenHesapla : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            If nDakika = nRefreshTime Then
                RefreshDashboard()
                nDakika = 0
            Else
                nDakika = nDakika + 1
            End If

        Catch ex As Exception
            ErrDisp("TimerTick : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub BarButtonItem8_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem8.ItemClick
        Try
            Dim oSQL As New SQLServerClass
            Dim cSQL As String = ""
            Dim cValue As String = ""

            oSQL.OpenConn()

            cSQL = "select top 1 parametervalue " +
                " from syspar with (NOLOCK) " +
                " where parametername = 'refreshtime' "

            oSQL.GetSQLReader(cSQL)

            If oSQL.oReader.Read Then
                cValue = oSQL.SQLReadString("parametervalue")
                If IsNumeric(cValue) Then
                    nRefreshTime = CDbl(cValue)
                End If
            End If
            oSQL.oReader.Close()

            oSQL.CloseConn()

            Me.Text = "DevExpress Dashboard Görüntüleyici (Peryot " + nRefreshTime.ToString + " Dakika) "

            nDakika = 0
            Timer1.Enabled = True
            Timer1.Interval = 1000 * 60 * nRefreshTime
            Timer1.Start()

        Catch ex As Exception
            ErrDisp("TimerStart : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub BarButtonItem9_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem9.ItemClick
        Try
            Timer1.Stop()
        Catch ex As Exception
            ErrDisp("TimerStop : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub RefreshDashboard()
        Try
            DashboardViewer1.ReloadData()
            DevXRestoreReport()
        Catch ex As Exception
            ErrDisp("RefreshDashboard : " + ex.Message, Me.Name)
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
            ErrDisp("AddPivotField : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub BarButtonItem10_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem10.ItemClick
        ' pivot alanlarını göster
        Try
            XtraTabControl1.SelectedTabPageIndex = 1
            PivotGridControl1.FieldsCustomization()

        Catch ex As Exception
            ErrDisp("BarButtonItem10_ItemClick : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub BarButtonItem11_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem11.ItemClick
        Try
            DashboardViewer1.ShowRibbonPrintPreview()

        Catch ex As Exception
            ErrDisp("BarButtonItem11_ItemClick : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub BarButtonItem12_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem12.ItemClick
        Try
            PivotGridControl1.ShowRibbonPrintPreview()

        Catch ex As Exception
            ErrDisp("BarButtonItem12_ItemClick : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub BarButtonItem13_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem13.ItemClick
        Try
            ChartControl1.ShowRibbonPrintPreview()

        Catch ex As Exception
            ErrDisp("BarButtonItem13_ItemClick : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub BarButtonItem14_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem14.ItemClick
        Try
            GridControl1.ShowRibbonPrintPreview()

        Catch ex As Exception
            ErrDisp("BarButtonItem14_ItemClick : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub BarButtonItem15_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem15.ItemClick
        ' chart
        Try
            Dim oChart As ChartControl
            Dim oWiz As ChartDesigner

            XtraTabControl1.SelectedTabPageIndex = 2
            oChart = Me.ChartControl1
            oWiz = New ChartDesigner(oChart)
            oWiz.ShowDialog()
            oChart.Refresh()

        Catch ex As Exception
            ErrDisp("BarButtonItem15_ItemClick : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub BarButtonItem16_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem16.ItemClick
        Try
            DevXSaveReport()

        Catch ex As Exception
            ErrDisp("BarButtonItem16_ItemClick : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub DevXSaveReport()
        ' save DevXReport to Database
        Dim cRecID As String = ""
        Dim cSQL As String = ""
        Dim aPLF As Byte() = Nothing
        Dim aPGF As Byte() = Nothing
        Dim aPCF As Byte() = Nothing
        Dim aCF As Byte() = Nothing
        Dim aGridLayout As Byte() = Nothing
        Dim oMemoryStream As MemoryStream = Nothing
        Dim oCMD As SqlCommand
        Dim ConnYage As SqlConnection

        Try
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

            oMemoryStream = New MemoryStream
            GridControl1.FocusedView.SaveLayoutToStream(oMemoryStream)
            ReDim aGridLayout(oMemoryStream.Length)
            oMemoryStream.Seek(0, System.IO.SeekOrigin.Begin)
            oMemoryStream.Read(aGridLayout, 0, aGridLayout.Length)
            oMemoryStream.Dispose()

            cRecID = oReportDevX.cReportID

            cSQL = "update devxdashboards " +
                    " set fileplf = @plf, " +
                    " filepcf = @pcf, " +
                    " filepgf = @pgf, " +
                    " filecf = @cf, " +
                    " filegridlayout = @gridlayout " +
                    " where reportid = " + oReportDevX.cReportID

            ConnYage = OpenConn()
            oCMD = New SqlCommand(cSQL, ConnYage)
            oCMD.Parameters.Add("@plf", SqlDbType.Image).Value = aPLF
            oCMD.Parameters.Add("@pcf", SqlDbType.Image).Value = aPCF
            oCMD.Parameters.Add("@pgf", SqlDbType.Image).Value = aPGF
            oCMD.Parameters.Add("@cf", SqlDbType.Image).Value = aCF
            oCMD.Parameters.Add("@gridlayout", SqlDbType.Image).Value = aGridLayout
            oCMD.ExecuteNonQuery()
            CloseConn(ConnYage)

        Catch ex As Exception
            ErrDisp("DevXSaveReport : " + ex.Message, Me.Name, cSQL,, ex)
        End Try
    End Sub

    Private Sub DevXRestoreReport(Optional lRetrieveData As Boolean = True)
        ' restore DevXReport from Database
        Dim cSQL As String = ""
        Dim ConnYage As SqlConnection
        Dim odr As SqlDataReader
        Dim oDS As New DataSet()
        Dim oDataAdapter As SqlDataAdapter = Nothing
        Dim aPLF As Byte() = Nothing
        Dim aPGF As Byte() = Nothing
        Dim aPCF As Byte() = Nothing
        Dim aCF As Byte() = Nothing
        Dim aGridLayout As Byte() = Nothing
        Dim oMemoryStream As MemoryStream = Nothing
        Dim oCol As System.Data.DataColumn
        Dim lSwitch As Boolean = True
        Dim nCount As Integer = 0
        Dim lData As Boolean = False
        Dim lCol As Boolean = False
        Dim lRow As Boolean = False

        Try

            GridControl1.DataSource = Nothing
            GridControl1.ForceInitialize()

            PivotGridControl1.DataSource = Nothing
            PivotGridControl1.ForceInitialize()

            ChartControl1.DataSource = Nothing

            DropView(cViewName)

            If oReportDevX.cReportSQL1.Trim = "" Then
                If cSQLx.Trim = "" Then
                    Exit Sub
                Else
                    cViewName = CreateTempView(cSQLx.Trim)
                End If
            Else
                cViewName = CreateTempView(oReportDevX.cReportSQL1.Trim)
            End If

            ConnYage = OpenConn()
            cSQL = "select * from " + cViewName
            oDataAdapter = New SqlDataAdapter(cSQL, ConnYage)
            oDataAdapter.Fill(oDS, cViewName)

            ' PivotGrid and Chart
            PivotGridControl1.BeginUpdate()
            PivotGridControl1.DataSource = oDS.Tables(cViewName)

            PivotGridControl1.Fields.Clear()

            For Each oCol In oDS.Tables(cViewName).Columns
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
            PivotGridControl1.EndUpdate()

            ' grid control
            GridControl1.BeginUpdate()
            GridControl1.DataSource = oDS.Tables(cViewName)
            GridControl1.Refresh()
            GridControl1.EndUpdate()

            If lRetrieveData Then

                ConnYage = OpenConn()

                cSQL = "select fileplf, filepgf, filepcf, filecf, filegridlayout " +
                        " from devxdashboards with (NOLOCK) " +
                        " where reportid = " + oReportDevX.cReportID

                odr = GetSQLReader(cSQL, ConnYage)

                If odr.Read Then
                    If Not IsDBNull(odr("fileplf")) Then aPLF = CType(odr("fileplf"), Byte())
                    If Not IsDBNull(odr("filepgf")) Then aPGF = CType(odr("filepgf"), Byte())
                    If Not IsDBNull(odr("filepcf")) Then aPCF = CType(odr("filepcf"), Byte())
                    If Not IsDBNull(odr("filecf")) Then aCF = CType(odr("filecf"), Byte())
                    If Not IsDBNull(odr("filegridlayout")) Then aGridLayout = CType(odr("filegridlayout"), Byte())
                End If
                odr.Close()
                odr = Nothing

                CloseConn(ConnYage)

                If Not IsNothing(aPGF) Then
                    oMemoryStream = New MemoryStream(aPGF)
                    oMemoryStream.Seek(0, System.IO.SeekOrigin.Begin)
                    PivotGridControl1.DataSource = oMemoryStream
                    oMemoryStream.Dispose()
                End If

                If Not IsNothing(aPLF) Then
                    oMemoryStream = New MemoryStream(aPLF)
                    oMemoryStream.Seek(0, System.IO.SeekOrigin.Begin)
                    PivotGridControl1.RestoreLayoutFromStream(oMemoryStream, DevExpress.Utils.OptionsLayoutBase.FullLayout)
                    oMemoryStream.Dispose()
                End If

                If Not IsNothing(aPCF) Then
                    oMemoryStream = New MemoryStream(aPCF)
                    oMemoryStream.Seek(0, System.IO.SeekOrigin.Begin)
                    PivotGridControl1.LoadCollapsedStateFromStream(oMemoryStream)
                    oMemoryStream.Dispose()
                End If

                If Not IsNothing(aGridLayout) Then
                    oMemoryStream = New MemoryStream(aGridLayout)
                    oMemoryStream.Seek(0, System.IO.SeekOrigin.Begin)
                    GridControl1.FocusedView.RestoreLayoutFromStream(oMemoryStream)
                    oMemoryStream.Dispose()
                End If

                If Not IsNothing(aCF) Then
                    oMemoryStream = New MemoryStream(aCF)
                    oMemoryStream.Seek(0, System.IO.SeekOrigin.Begin)
                    ChartControl1.LoadFromStream(oMemoryStream)
                    oMemoryStream.Dispose()
                End If

            End If

            ' Chart
            ChartControl1.DataSource = PivotGridControl1
            ChartControl1.Refresh()

            CloseConn(ConnYage)

        Catch ex As Exception
            ErrDisp("DevXRestoreReport : " + ex.Message, Me.Name, cSQL,, ex)
        End Try
    End Sub

    Private Sub BarButtonItem17_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem17.ItemClick
        Try
            DevXRestoreReport(False)

        Catch ex As Exception
            ErrDisp("Reset pivot grafik liste : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub frmDashboardViewer_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed

        Dim nCnt As Integer
        Dim ConnYage As SqlConnection = Nothing

        Try
            DevXSaveReport()

            DropView(cViewName)

            ConnYage = OpenConn()
            For nCnt = 0 To UBound(aTempTables)
                DropTable(aTempTables(nCnt), ConnYage)
            Next
            ConnYage.Close()

        Catch ex As Exception
            ErrDisp("Form closed : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub SqlDataSource_ValidateCustomSqlQueryGlobal(sender As Object, e As ValidateCustomSqlQueryEventArgs)
        Try
            Dim customQuery As CustomSqlQuery = e.CustomSqlQuery
            Dim validationResult As Boolean = True
            ' Insert your custom validation logic here. 
            e.Valid = validationResult

        Catch ex As Exception
            ErrDisp("SqlDataSource_ValidateCustomSqlQueryGlobal : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub DashboardViewer1_ConfigureDataConnection(sender As Object, e As DashboardConfigureDataConnectionEventArgs) Handles DashboardViewer1.ConfigureDataConnection
        Try
            ' Checks the name of the connection for which the event has been raised.
            If e.DataSourceName = "WinTexDataSource" Then
                Dim oSqlParams As MsSqlConnectionParameters = CType(e.ConnectionParameters, MsSqlConnectionParameters)
                oSqlParams.AuthorizationType = MsSqlAuthorizationType.SqlServer
                oSqlParams.ServerName = oConnection.cServer
                oSqlParams.DatabaseName = oConnection.cDatabase
                oSqlParams.UserName = oConnection.cUser
                oSqlParams.Password = oConnection.cPassword
            End If
        Catch ex As Exception
            ErrDisp("ConfigureDataConnection : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub DashboardViewer1_DashboardLoaded(sender As Object, e As DashboardLoadedEventArgs) Handles DashboardViewer1.DashboardLoaded

        Dim cSQL As String = ""
        Dim cTempTable As String = ""
        Dim cTempView As String = ""
        Dim nCnt As Integer = -1

        Try
            Dim oDS As DashboardSqlDataSource = Nothing
            Dim oCustomQuery As CustomSqlQuery = Nothing

            Me.Cursor = Cursors.WaitCursor

            For Each oDS In e.Dashboard.DataSources
                For Each oCustomQuery In oDS.Queries

                    Select Case oCustomQuery.Name
                        Case oReportDevX.cReportQueryName1 : oCustomQuery.Sql = oReportDevX.cReportSQL1
                        Case oReportDevX.cReportQueryName2 : oCustomQuery.Sql = oReportDevX.cReportSQL2
                        Case oReportDevX.cReportQueryName3 : oCustomQuery.Sql = oReportDevX.cReportSQL3
                        Case oReportDevX.cReportQueryName4 : oCustomQuery.Sql = oReportDevX.cReportSQL4
                        Case oReportDevX.cReportQueryName5 : oCustomQuery.Sql = oReportDevX.cReportSQL5
                        Case oReportDevX.cReportQueryName6 : oCustomQuery.Sql = oReportDevX.cReportSQL6
                        Case oReportDevX.cReportQueryName7 : oCustomQuery.Sql = oReportDevX.cReportSQL7
                        Case oReportDevX.cReportQueryName8 : oCustomQuery.Sql = oReportDevX.cReportSQL8
                        Case oReportDevX.cReportQueryName9 : oCustomQuery.Sql = oReportDevX.cReportSQL9
                        Case oReportDevX.cReportQueryName10 : oCustomQuery.Sql = oReportDevX.cReportSQL10
                    End Select

                    cSQLx = oCustomQuery.Sql
                    cTempView = CreateTempView(oCustomQuery.Sql, , "tmpvddb_", False)
                    cTempTable = GetTempTableName("ddb")

                    nCnt = nCnt + 1
                    ReDim Preserve aTempTables(nCnt)
                    aTempTables(nCnt) = cTempTable

                    cSQL = "select * into " + cTempTable + " from " + cTempView

                    If ExecuteSQLCommand(cSQL) Then
                        oCustomQuery.Sql = "select * from " + cTempTable + " with (NOLOCK) "
                    End If

                    DropView(cTempView)
                Next
            Next

            Me.Cursor = Cursors.Default

        Catch ex As Exception
            ErrDisp("DashboardViewer1_DashboardLoaded : " + ex.Message, Me.Name, cSQL,, ex)
        End Try
    End Sub

    Private Sub LoadXMLFile()
        Try
            Dim cTempFile As String = ""
            Dim oDashboard As New Dashboard
            Dim nCnt As Integer = 0

            ' Loads a dashboard from an XML file.
            cTempFile = GetTempFile("xml")
            My.Computer.FileSystem.WriteAllText(cTempFile, oReportDevX.cReport, False)
            oDashboard.LoadFromXml(cTempFile)
            DestroyFile(cTempFile)

            DashboardViewer1.Dashboard = oDashboard
            DashboardViewer1.ReloadData()
            DevXRestoreReport()

        Catch ex As Exception
            ErrDisp("LoadXMLFile : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

End Class