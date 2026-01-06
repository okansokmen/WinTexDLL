Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports DevExpress.XtraGrid
Imports System.Data.SqlClient

Public Class SpreadReport

    Public Sub init(cSQL As String)

        GetData(cSQL)

        Me.Show()
    End Sub

    Public Sub init2(oDataTable As DataTable)
        Try
            GridControl1.DataSource = oDataTable
            Me.Show()
        Catch ex As Exception
            ErrDisp("init2 : " + ex.Message, Me.Name)
        End Try
    End Sub

    Public Sub init3(oDataSet As DataSet)
        Try
            GridControl1.DataSource = oDataSet
            Me.Show()
        Catch ex As Exception
            ErrDisp("init3 : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub GetData(cSQL As String)

        Dim ConnYage As SqlClient.SqlConnection
        Dim oDS As New DataSet()
        Dim oDataAdapter As SqlDataAdapter

        Try
            If cSQL.Trim = "" Then Exit Sub

            ConnYage = OpenConn(3000)

            oDataAdapter = New SqlDataAdapter(cSQL, ConnYage)
            oDataAdapter.SelectCommand.CommandTimeout = 3000
            oDataAdapter.Fill(oDS)
            GridControl1.DataSource = oDS.Tables(0)

            Call CloseConn(ConnYage)

        Catch ex As Exception
            ErrDisp("GetData : " + ex.Message, Me.Name, cSQL)
        End Try

    End Sub

    Private Sub SimpleButton3_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton3.Click
        ' Print
        Me.GridControl1.ShowRibbonPrintPreview()
    End Sub

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click
        ' Exit
        Me.Close()
    End Sub

    Private Sub SpreadReport_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Maximized
    End Sub
End Class