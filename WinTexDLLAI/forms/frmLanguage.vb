Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.IO

Imports System.ComponentModel

Imports Microsoft.SqlServer.Server
Imports Microsoft.InteropFormTools
Imports System.Windows.Forms

Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Sql
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors
Imports DevExpress.XtraLayout
Imports DevExpress.Utils.Extensions
Imports DevExpress.XtraSpreadsheet.Forms
Imports DevExpress.DashboardCommon.Native

Public Class frmLanguage

    Dim oGrid As DevExpress.XtraGrid.GridControl
    Dim oView As DevExpress.XtraGrid.Views.Grid.GridView
    Dim lLoading As Boolean = False

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        oGrid = New DevExpress.XtraGrid.GridControl
        oView = New DevExpress.XtraGrid.Views.Grid.GridView
    End Sub

    Public Sub init()
        Me.ShowDialog()
    End Sub

    Private Sub frmLanguage_Load(sender As Object, e As EventArgs) Handles Me.Load
        lLoading = True
        DXInitGridView(oGrid, oView, PanelControl2)
        LoadData()
        lLoading = False
    End Sub

    Private Sub LoadData()
        Try
            Dim oRecords As BindingList(Of Record_IslemGrubu)

            oRecords = GetData_IslemGrubu()
            oGrid.DataSource = oRecords
            InitGrid()

        Catch ex As Exception
            ErrDisp("LoadData : " + ex.Message.Trim, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub InitGrid()
        Try
            Dim oColumn As GridColumn = Nothing

            Dim riNumericEdit As New RepositoryItemTextEdit
            Dim riTextEdit As New RepositoryItemTextEdit

            oGrid.RepositoryItems.Add(riNumericEdit)
            oGrid.RepositoryItems.Add(riTextEdit)

            oColumn = oView.Columns.ColumnByFieldName("ID")
            oColumn.Caption = "ID"
            oColumn.Fixed = FixedStyle.Left
            oColumn.Visible = True
            oColumn.OptionsColumn.ReadOnly = True
            oColumn.OptionsColumn.AllowEdit = False
            oColumn.ColumnEdit = riNumericEdit
            oColumn.Width = 5

            oColumn = oView.Columns.ColumnByFieldName("IslemGrubu")
            oColumn.Caption = "Türkçe"
            oColumn.Fixed = FixedStyle.Left
            oColumn.Visible = True
            oColumn.OptionsColumn.ReadOnly = True
            oColumn.OptionsColumn.AllowEdit = False
            oColumn.ColumnEdit = riTextEdit
            oColumn.Width = 30

            oColumn = oView.Columns.ColumnByFieldName("Arabic")
            oColumn.Caption = "Arabic"
            oColumn.Fixed = FixedStyle.Left
            oColumn.Visible = True
            oColumn.OptionsColumn.ReadOnly = False
            oColumn.OptionsColumn.AllowEdit = True
            oColumn.ColumnEdit = riTextEdit
            'oColumn.BestFit()

            oGrid.Refresh()

        Catch ex As Exception
            ErrDisp("InitGrid : " + ex.Message.Trim, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        ' kaydet
        Dim oSQL As New SQLServerClass
        Dim nRow As Integer = 0
        Dim nID As Double = 0
        Dim cIslemGrubu As String = ""
        Dim cArabic As String = ""
        Dim oColumn As GridColumn

        If oGrid.DataSource Is Nothing Then Exit Sub

        oSQL.OpenConn()

        For nRow = 0 To oView.DataRowCount - 1

            oColumn = oView.Columns.ColumnByFieldName("ID")
            nID = CDbl(oView.GetRowCellValue(nRow, oColumn))

            oColumn = oView.Columns.ColumnByFieldName("IslemGrubu")
            cIslemGrubu = oView.GetRowCellValue(nRow, oColumn).ToString

            oColumn = oView.Columns.ColumnByFieldName("Arabic")
            If Not (oView.GetRowCellValue(nRow, oColumn) Is Nothing) Then
                cArabic = oView.GetRowCellValue(nRow, oColumn).ToString

                oSQL.cSQLQuery = "update islemgrubu " +
                            " set arabic = N'" + cArabic.Trim + "' " +
                            " where sirano = " + nID.ToString

                oSQL.SQLExecute()
            End If
        Next
        oSQL.CloseConn()
        oSQL = Nothing

        Me.Close()

    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        ' çıkış 
        Me.Close()
    End Sub

End Class