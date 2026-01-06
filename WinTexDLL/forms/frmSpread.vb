Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports DevExpress.Spreadsheet
Imports System.Data.SqlClient

Public Class frmSpread

    Dim cSQLSource As String = ""

    Public Sub init(Optional cSQL As String = "")
        cSQLSource = cSQL.Trim
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

    Private Sub BindSpreadToSQL()

        Dim ConnYage As SqlClient.SqlConnection
        Dim oDS As New DataSet()
        Dim oDataAdapter As SqlDataAdapter
        Dim cSQL As String = ""
        Dim oWorkSheet As Worksheet = SpreadsheetControl1.Document.Worksheets(0)
        Dim cViewName As String

        Try
            ConnYage = OpenConn()

            cViewName = CreateTempView(cSQLSource)

            cSQL = "select * from " + cViewName

            oDataAdapter = New SqlDataAdapter(cSQL, ConnYage)
            oDataAdapter.Fill(oDS, cViewName)
            oWorkSheet.Import(oDS.Tables(cViewName), True, 0, 0)
            SpreadsheetControl1.Refresh()
            Call CloseConn(ConnYage)
            DropView(cViewName)

        Catch ex As Exception
            ErrDisp("BindSpreadToSQL : " + ex.Message, Me.Name, cSQL)
        End Try
    End Sub

    Private Sub frmSpread_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If cSQLSource Is Nothing Then
            Else
                If cSQLSource.Trim <> "" Then
                    BindSpreadToSQL()
                End If
            End If

        Catch ex As Exception
            ErrDisp("frmSpread_Load : " + ex.Message, Me.Name)
        End Try
    End Sub
End Class