Option Explicit On
'Option Strict On

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

Public Class frmDoviz

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

    Private Sub frmDoviz_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            lLoading = True
            DXInitGridView(oGrid, oView, PanelControl2)
            DateNavigator1.DateTime = Now.Date
            LoadData()
            lLoading = False

        Catch ex As Exception
            ErrDisp("frmDoviz_Load : " + ex.Message.Trim, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub InitGrid()
        Try
            Dim dTarih As Date = DateNavigator1.DateTime
            Dim oColumn As GridColumn = Nothing
            Dim cSQL As String = ""
            Dim oSQL As New SQLServerClass
            Dim aKurCinsi() As String = {"Alis Kuru", "Satis Kuru", "Efektif Alis Kuru", "Efektif Satis Kuru", "On Maliyet Kuru", "On Maliyet Kuru 2", "Serbest Kur"}

            Dim riTextEdit As New RepositoryItemTextEdit
            Dim riNumericEdit As New RepositoryItemTextEdit
            Dim riLookupEdit As New RepositoryItemGridLookUpEdit
            Dim riComboBox As New RepositoryItemComboBox

            riComboBox.Items.AddRange(aKurCinsi)

            riNumericEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric

            cSQL = "Select doviz, aciklama, yabanciadi, ulke " +
                    " from doviz with (NOLOCK) " +
                    " where doviz is not null " +
                    " and doviz <> '' "

            riLookupEdit.DataSource = oSQL.SQLSelectOpenCloseConnection(cSQL)
            riLookupEdit.ValueMember = "doviz"
            riLookupEdit.DisplayMember = "doviz"
            riLookupEdit.PopulateViewColumns()
            oSQL = Nothing

            oGrid.RepositoryItems.Add(riTextEdit)
            oGrid.RepositoryItems.Add(riNumericEdit)
            oGrid.RepositoryItems.Add(riLookupEdit)
            oGrid.RepositoryItems.Add(riComboBox)

            oColumn = oView.Columns.ColumnByFieldName("ID")
            oColumn.Visible = False

            oColumn = oView.Columns.ColumnByFieldName("Doviz")
            oColumn.Caption = "Döviz Cinsi"
            oColumn.Fixed = FixedStyle.Left
            oColumn.Visible = True
            oColumn.OptionsColumn.ReadOnly = False
            oColumn.OptionsColumn.AllowEdit = True
            oColumn.ColumnEdit = riLookupEdit
            oColumn.BestFit()

            oColumn = oView.Columns.ColumnByFieldName("KurCinsi")
            oColumn.Caption = "Kur Cinsi"
            oColumn.Fixed = FixedStyle.Left
            oColumn.Visible = True
            oColumn.OptionsColumn.ReadOnly = False
            oColumn.OptionsColumn.AllowEdit = True
            oColumn.ColumnEdit = riComboBox
            oColumn.BestFit()

            oColumn = oView.Columns.ColumnByFieldName("Kur")
            oColumn.Caption = "Kur"
            oColumn.Fixed = FixedStyle.Right
            oColumn.Visible = True
            oColumn.OptionsColumn.ReadOnly = False
            oColumn.OptionsColumn.AllowEdit = True
            oColumn.ColumnEdit = riNumericEdit
            oColumn.BestFit()

            oGrid.Refresh()

        Catch ex As Exception
            ErrDisp("InitGrid : " + ex.Message.Trim, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        ' ok / kaydet
        Try
            Dim dTarih As Date = DateNavigator1.DateTime
            Dim cSQL As String = ""
            Dim oSQL As New SQLServerClass
            Dim nRow As Integer = 0
            Dim cDoviz As String = ""
            Dim cKurCinsi As String = ""
            Dim nKur As Double = 0
            Dim oColumn As GridColumn

            If oGrid.DataSource Is Nothing Then Exit Sub

            oSQL.OpenConn()

            cSQL = "set dateformat dmy " +
                " delete dovkur " +
                " where tarih = '" + dTarih.Date.ToShortDateString + "' "

            oSQL.SQLExecute(cSQL)

            For nRow = 0 To oView.DataRowCount - 1

                oColumn = oView.Columns.ColumnByFieldName("Doviz")
                cDoviz = oView.GetRowCellValue(nRow, oColumn).ToString

                oColumn = oView.Columns.ColumnByFieldName("KurCinsi")
                cKurCinsi = oView.GetRowCellValue(nRow, oColumn).ToString

                oColumn = oView.Columns.ColumnByFieldName("Kur")
                nKur = Convert.ToDouble(oView.GetRowCellValue(nRow, oColumn))

                cSQL = "set dateformat dmy " +
                    " insert dovkur (doviz, kurcinsi, kur, tarih, modificationdate, kaynak) " +
                    " values ('" + SQLWriteString(cDoviz, 3) + "'," +
                    " '" + SQLWriteString(cKurCinsi, 30) + "', " +
                    SQLWriteDecimal(nKur) + ", " +
                    " '" + dTarih.Date.ToShortDateString + "', " +
                    " getdate(), " +
                    " 'frmDoviz' ) "

                oSQL.SQLExecute(cSQL)
            Next
            oSQL.CloseConn()
            oSQL = Nothing

            Me.Close()

        Catch ex As Exception
            ErrDisp("SimpleButton1_Click : " + ex.Message.Trim, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        ' iptal
        On Error Resume Next
        Me.Close()
    End Sub

    Private Sub DateNavigator1_SelectionChanged(sender As Object, e As EventArgs) Handles DateNavigator1.SelectionChanged
        Try
            If lLoading Then Exit Sub
            LoadData()

        Catch ex As Exception
            ErrDisp("DateNavigator1_SelectionChanged : " + ex.Message.Trim, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        Try
            If Not oGrid.IsPrintingAvailable Then
                MsgBox("Yazdırma işlemi yapılamıyor")
            Else
                oGrid.ShowRibbonPrintPreview()
            End If

        Catch ex As Exception
            ErrDisp("SimpleButton3_Click : " + ex.Message.Trim, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        ' kurları çek 
        Try
            If Not Confirmed("TCMB kur sayfasından kurlar okunacaktır" + vbCrLf + "TCBM sitesinde " + DateNavigator1.DateTime.ToShortDateString + " tarihinde kur verisi bulunursa veri tabanına yazılacaktır", Me) Then
                Exit Sub
            End If
            If Not MBKur(DateNavigator1.DateTime) Then
                MessageBox.Show("Dikkat : " + DateNavigator1.DateTime.ToShortDateString + " tarihinin kur bilgileri TCMB sayfasından alınamadı")
                Exit Sub
            End If
            LoadData()

        Catch ex As Exception
            ErrDisp("SimpleButton4_Click : " + ex.Message.Trim, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        Try
            Dim nRow As Integer = 0
            Dim oSelectedRowHandles As Int32() = oView.GetSelectedRows()
            Dim oSelectedRowHandle As Int32 = 0

            If oGrid.DataSource Is Nothing Then Exit Sub
            If Not Confirmed("Seçilmiş satırlar silinecektir", Me) Then Exit Sub

            For nRow = 0 To oSelectedRowHandles.Length - 1
                oSelectedRowHandle = oSelectedRowHandles(nRow)
                If (oSelectedRowHandle >= 0) Then
                    ' silinebilirmi kontrollerini yap
                    ' silinecek satırlardan çıkart
                    'oView.InvertRowSelection(oSelectedRowHandle)
                End If
            Next

            oView.DeleteSelectedRows()

        Catch ex As Exception
            ErrDisp("SimpleButton5_Click : " + ex.Message.Trim, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub LoadData()
        Try
            Dim oRecords As BindingList(Of Record_DovKur)

            oRecords = GetData_DovKur(DateNavigator1.DateTime)
            oGrid.DataSource = oRecords
            InitGrid()

            Me.Text = "Döviz Kurları : " + DateNavigator1.DateTime.ToShortDateString

        Catch ex As Exception
            ErrDisp("LoadData : " + ex.Message.Trim, Me.Name,,, ex)
        End Try
    End Sub

End Class