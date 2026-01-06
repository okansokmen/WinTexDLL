Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.Data.SqlClient

Imports DevExpress.XtraBars.Ribbon
Imports DevExpress.XtraSpreadsheet
Imports DevExpress.Spreadsheet


Public Class frmSpreadSheet

    Public Sub init(Optional cParameter As String = "", Optional nMode As Integer = 1)

        Select Case nMode
            Case 1
                ' open as plain excel
            Case 2
                ' open as SQL query 
                If cParameter.Trim <> "" Then
                    BindSpreadToSQL(cParameter)
                End If
            Case 3
                ' open as document viewer 
                If cParameter.Trim <> "" Then
                    SpreadsheetControl1.LoadDocument(cParameter)
                End If
        End Select
        Me.Show()
    End Sub

    Public Sub init2(oDataTable As DataTable)
        Try
            Dim oWorkSheet As Worksheet

            'Dim oRow As DataRow
            Dim nCol As Integer = 0
            Dim nRow As Integer = 0

            SpreadsheetControl1.Document.Worksheets.Add()
            oWorkSheet = SpreadsheetControl1.Document.Worksheets(0)

            For nCol = 0 To oDataTable.Columns.Count - 1
                For nRow = 0 To oDataTable.Rows.Count - 1
                    oWorkSheet.Cells(nRow, nCol).Value = oDataTable.Rows(nRow).ItemArray(nCol).ToString
                Next
            Next

            SpreadsheetControl1.Refresh()

            Me.Show()

        Catch ex As Exception
            ErrDisp("init2 : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub BindSpreadToSQL(cSQL As String)

        Dim ConnYage As SqlClient.SqlConnection
        Dim oDS As New DataSet()
        Dim oDataAdapter As SqlDataAdapter
        Dim oWorkSheet As Worksheet = SpreadsheetControl1.Document.Worksheets(0)

        Try
            If cSQL.Trim = "" Then Exit Sub

            ConnYage = OpenConn(3000)

            oDataAdapter = New SqlDataAdapter(cSQL, ConnYage)
            oDataAdapter.SelectCommand.CommandTimeout = 3000
            oDataAdapter.Fill(oDS)
            oWorkSheet.Import(oDS.Tables(0), True, 0, 0)
            SpreadsheetControl1.Refresh()

            Call CloseConn(ConnYage)

        Catch ex As Exception
            ErrDisp("BindSpreadToSQL : " + ex.Message, Me.Name, cSQL)
        End Try
    End Sub

    Private Sub frmSpreadSheet_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Maximized
    End Sub
End Class