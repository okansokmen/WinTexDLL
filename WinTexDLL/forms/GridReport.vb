Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports DevExpress.XtraPivotGrid
Imports System.Data.SqlClient
Imports DevExpress.XtraCharts
Imports DevExpress.XtraCharts.Wizard
Imports DevExpress.XtraCharts.Designer

Public Class GridReport

    Public Sub init(cSQL As String)

        GetData(cSQL)

        Me.Show()
    End Sub

    Private Sub GetData(cSQL As String)

        Dim ConnYage As SqlClient.SqlConnection
        Dim oDS As New DataSet()
        Dim oDataAdapter As SqlDataAdapter
        Dim oCol As System.Data.DataColumn
        Dim lSwitch As Boolean = True
        Dim nCount As Integer = 0
        Dim lData As Boolean = False
        Dim lCol As Boolean = False
        Dim lRow As Boolean = False

        If cSQL.Trim = "" Then Exit Sub

        ConnYage = OpenConn(3000)

        oDataAdapter = New SqlDataAdapter(cSQL, ConnYage)
        oDataAdapter.SelectCommand.CommandTimeout = 3000
        oDataAdapter.Fill(oDS)
        PivotGridControl1.DataSource = oDS.Tables(0)

        For Each oCol In oDS.Tables(0).Columns
            nCount = nCount + 1
            Select Case oCol.DataType.Name
                Case "Decimal"
                    AddField(oCol.ColumnName, 1)
                    lData = True
                Case "DateTime", "Date"
                    AddField(oCol.ColumnName, 4)
                Case "String"
                    If lSwitch Then
                        AddField(oCol.ColumnName, 3)
                        lSwitch = False
                        lCol = True
                    Else
                        AddField(oCol.ColumnName, 2)
                        lSwitch = True
                        lRow = True
                    End If
                Case Else
                    ' do nothing
            End Select
        Next
        Call CloseConn(ConnYage)
    End Sub

    Private Sub AddField(cFieldName As String, nType As Integer)
        Select Case nType
            Case 1 ' Decimal, DataArea
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
        End Select
    End Sub

    Private Sub SimpleButton3_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton3.Click
        ' pivot yazdır
        Me.PivotGridControl1.ShowRibbonPrintPreview()
    End Sub

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click
        ' Exit
        Me.Close()
    End Sub

    Private Sub SimpleButton2_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton2.Click
        ' chart
        Dim oChart As ChartControl
        Dim oWiz As ChartDesigner

        oChart = Me.ChartControl1
        oWiz = New ChartDesigner(oChart)
        oWiz.ShowDialog()
        oChart.Refresh()

        Me.SplitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
    End Sub

    Private Sub SimpleButton4_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton4.Click
        ' grafik yazdır
        Me.ChartControl1.ShowRibbonPrintPreview()
    End Sub

    Private Sub SimpleButton5_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton5.Click
        ' grafik max
        Me.SplitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
    End Sub

    
    Private Sub SimpleButton7_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton7.Click
        ' pivot max
        Me.SplitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
    End Sub

    Private Sub SimpleButton6_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton6.Click
        ' pivot + grafik
        Me.SplitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both
    End Sub

    Private Sub GridReport_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Maximized
        PivotGridControl1.FieldsCustomization()
    End Sub

    Private Sub GridReport_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        Me.WindowState = FormWindowState.Maximized
    End Sub
End Class
