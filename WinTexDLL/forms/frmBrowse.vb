Option Explicit On
'Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.IO
Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms

Imports Microsoft.SqlServer.Server
Imports Microsoft.InteropFormTools

Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Sql
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors
Imports DevExpress.XtraLayout
Imports DevExpress.Utils.Extensions
Imports DevExpress.Utils.Menu
Imports DevExpress.XtraBars

Public Class frmBrowse

    Dim oGrid As DevExpress.XtraGrid.GridControl
    Dim oView As DevExpress.XtraGrid.Views.Grid.GridView
    Dim lLoading As Boolean = False
    Dim nMode As Integer = 1
    Dim oPopupMenu As PopupMenu
    Dim oBarManager As BarManager
    Dim oButton1 As BarButtonItem
    Dim oButton2 As BarButtonItem

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        oGrid = New DevExpress.XtraGrid.GridControl
        oView = New DevExpress.XtraGrid.Views.Grid.GridView
    End Sub

    Public Sub init(Optional nMode1 As Integer = 1, Optional lModal As Boolean = True)


        nMode = nMode1

        If lModal Then
            Me.ShowDialog()
        Else
            Me.Show()
        End If
    End Sub

    Private Sub Button1_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs)
        ' Satır Ekle
        Dim cSonuc As String = ""
        Dim ofrmGetString As New frmGetString
        Dim nMax As Integer
        Dim nCnt As Integer

        cSonuc = ofrmGetString.init("Eklenecek Satır Adedini Giriniz", "Satır Adedi")

        If IsNumeric(cSonuc) Then
            nMax = CInt(cSonuc)
            For nCnt = 1 To nMax
                oView.AddNewRow()
            Next
        End If
    End Sub

    Private Sub Button2_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs)
        ' Excele Aktar
        Dim path As String = "wintex.xlsx"
        ' Open the created XLSX file with the default application.
        DestroyFile(path)
        oView.ExportToXlsx(path)
        Process.Start(path)
    End Sub

    Private Sub frmBrowse_Load(sender As Object, e As EventArgs) Handles Me.Load

        oBarManager = New BarManager
        oBarManager.Form = Me
        oPopupMenu = New PopupMenu

        oButton1 = New BarButtonItem(oBarManager, "Satır Ekle")
        oPopupMenu.AddItem(oButton1)
        AddHandler oButton1.ItemClick, AddressOf Me.Button1_ItemClick

        oButton2 = New BarButtonItem(oBarManager, "Excele Aktar")
        oPopupMenu.AddItem(oButton2)
        AddHandler oButton2.ItemClick, AddressOf Me.Button2_ItemClick

        DropDownButton1.DropDownControl = oPopupMenu

        lLoading = True
        DXInitGridView(oGrid, oView, PanelControl2, False)
        LoadData()
        lLoading = False
    End Sub

    Private Sub LoadData()

        Try
            Dim oRecords As BindingList(Of Record_OlcuYerleri)

            oRecords = GetData_OlcuYerleri()
            oGrid.DataSource = oRecords
            InitGrid()

            Me.Text = "Ölçü Yerleri"

        Catch ex As Exception
            ErrDisp("LoadData : " + ex.Message.Trim, Me.Name,,, ex)
        End Try
    End Sub

    Public Function GetData_OlcuYerleri() As BindingList(Of Record_OlcuYerleri)

        GetData_OlcuYerleri = Nothing

        Try
            Dim oRecord As Record_OlcuYerleri
            Dim oRecords As New BindingList(Of Record_OlcuYerleri)()
            Dim oSQL As New SQLServerClass
            Dim cSQL As String = ""

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select sirano, olcuyeri, grading, siralama, tolerans, ingilizceolcuyeri, arapcaolcuyeri " +
                            " from olcuyerleri with (NOLOCK) " +
                            " where olcuyeri Is Not null " +
                            " and olcuyeri <> '' " +
                            " order by olcuyeri  "

            oSQL.GetSQLReader(cSQL)

            Do While oSQL.oReader.Read

                oRecord = New Record_OlcuYerleri

                oRecord.SiraNo = oSQL.SQLReadDouble("sirano")
                oRecord.OlcuYeri = oSQL.SQLReadString("olcuyeri")
                oRecord.Grading = oSQL.SQLReadDouble("grading")
                oRecord.Siralama = oSQL.SQLReadDouble("siralama")
                oRecord.Tolerans = oSQL.SQLReadDouble("tolerans")
                oRecord.ingilizceOlcuYeri = oSQL.SQLReadString("ingilizceolcuyeri")
                oRecord.ArapcaOlcuYeri = oSQL.SQLReadString("arapcaolcuyeri")

                oRecords.Add(oRecord)
            Loop
            oSQL.oReader.Close()
            oSQL.CloseConn()

            Return oRecords

        Catch ex As Exception
            ErrDisp(ex.Message, "GetData_OlcuYerleri",  ,, ex)
        End Try
    End Function

    Public Class Record_OlcuYerleri

        Implements INotifyPropertyChanged

        Public Sub New()
        End Sub

        Private nSiraNo As Double
        <DisplayName("SiraNo")>
        Public Property SiraNo() As Double
            Get
                Return nSiraNo
            End Get
            Set(ByVal value As Double)
                If nSiraNo <> value Then
                    nSiraNo = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Private cOlcuYeri As String
        <DisplayName("OlcuYeri")>
        Public Property OlcuYeri() As String
            Get
                Return cOlcuYeri
            End Get
            Set(ByVal value As String)
                If cOlcuYeri <> value Then
                    'If String.IsNullOrEmpty(value) Then
                    '    Throw New Exception()
                    'End If
                    cOlcuYeri = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Private nGrading As Double
        <DisplayName("Grading")>
        Public Property Grading() As Double
            Get
                Return nGrading
            End Get
            Set(ByVal value As Double)
                If Not nGrading.Equals(value) Then
                    nGrading = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Private nSiralama As Double
        <DisplayName("Siralama")>
        Public Property Siralama() As Double
            Get
                Return nSiralama
            End Get
            Set(ByVal value As Double)
                If Not nSiralama.Equals(value) Then
                    nSiralama = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Private nTolerans As Double
        <DisplayName("Tolerans")>
        Public Property Tolerans() As Double
            Get
                Return nTolerans
            End Get
            Set(ByVal value As Double)
                If Not nTolerans.Equals(value) Then
                    nTolerans = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Private cingilizceOlcuYeri As String
        <DisplayName("IngilizceOlcuYeri")>
        Public Property ingilizceOlcuYeri() As String
            Get
                Return cingilizceOlcuYeri
            End Get
            Set(ByVal value As String)
                If cingilizceOlcuYeri <> value Then
                    'If String.IsNullOrEmpty(value) Then
                    '    Throw New Exception()
                    'End If
                    cingilizceOlcuYeri = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Private cArapcaOlcuYeri As String
        <DisplayName("ArapcaOlcuYeri")>
        Public Property ArapcaOlcuYeri() As String
            Get
                Return cArapcaOlcuYeri
            End Get
            Set(ByVal value As String)
                If cArapcaOlcuYeri <> value Then
                    'If String.IsNullOrEmpty(value) Then
                    '    Throw New Exception()
                    'End If
                    cArapcaOlcuYeri = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return String.Format("SiraNo = {0}, OlcuYeri = {1}, Grading = {2}, Siralama = {3}, Tolerans = {4}, Ingilizce = {5}, Arapca = {6}", nSiraNo, cOlcuYeri, nGrading, nSiralama, nTolerans, cingilizceOlcuYeri, cArapcaOlcuYeri)
        End Function

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Protected Sub OnPropertyChanged(<CallerMemberName> Optional propertyName As String = "")
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
    End Class

    Private Sub InitGrid()

        Try
            Dim oColumn As GridColumn = Nothing

            Dim riTextEdit As New RepositoryItemTextEdit
            Dim riNumericEdit As New RepositoryItemTextEdit
            Dim riLookupEdit As New RepositoryItemGridLookUpEdit
            Dim riComboBox As New RepositoryItemComboBox

            riNumericEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric

            oGrid.RepositoryItems.Add(riTextEdit)
            oGrid.RepositoryItems.Add(riNumericEdit)
            oGrid.RepositoryItems.Add(riLookupEdit)
            oGrid.RepositoryItems.Add(riComboBox)

            oColumn = oView.Columns.ColumnByFieldName("SiraNo")
            oColumn.Visible = False

            oColumn = oView.Columns.ColumnByFieldName("OlcuYeri")
            oColumn.Caption = "Olcu Yeri"
            oColumn.Fixed = FixedStyle.Left
            oColumn.Visible = True
            oColumn.OptionsColumn.ReadOnly = False
            oColumn.OptionsColumn.AllowEdit = True
            oColumn.ColumnEdit = riTextEdit
            oColumn.BestFit()

            oColumn = oView.Columns.ColumnByFieldName("Grading")
            oColumn.Caption = "Grading"
            oColumn.Fixed = FixedStyle.Right
            oColumn.Visible = True
            oColumn.OptionsColumn.ReadOnly = False
            oColumn.OptionsColumn.AllowEdit = True
            oColumn.ColumnEdit = riNumericEdit
            oColumn.BestFit()

            oColumn = oView.Columns.ColumnByFieldName("Siralama")
            oColumn.Caption = "Siralama"
            oColumn.Fixed = FixedStyle.Right
            oColumn.Visible = True
            oColumn.OptionsColumn.ReadOnly = False
            oColumn.OptionsColumn.AllowEdit = True
            oColumn.ColumnEdit = riNumericEdit
            oColumn.BestFit()

            oColumn = oView.Columns.ColumnByFieldName("Tolerans")
            oColumn.Caption = "Tolerans"
            oColumn.Fixed = FixedStyle.Right
            oColumn.Visible = True
            oColumn.OptionsColumn.ReadOnly = False
            oColumn.OptionsColumn.AllowEdit = True
            oColumn.ColumnEdit = riNumericEdit
            oColumn.BestFit()

            oColumn = oView.Columns.ColumnByFieldName("ingilizceOlcuYeri")
            oColumn.Caption = "Ingilizce Olcu Yeri"
            oColumn.Fixed = FixedStyle.Left
            oColumn.Visible = True
            oColumn.OptionsColumn.ReadOnly = False
            oColumn.OptionsColumn.AllowEdit = True
            oColumn.ColumnEdit = riTextEdit
            oColumn.BestFit()

            oColumn = oView.Columns.ColumnByFieldName("ArapcaOlcuYeri")
            oColumn.Caption = "Arapca Olcu Yeri"
            oColumn.Fixed = FixedStyle.Left
            oColumn.Visible = True
            oColumn.OptionsColumn.ReadOnly = False
            oColumn.OptionsColumn.AllowEdit = True
            oColumn.ColumnEdit = riTextEdit
            oColumn.BestFit()

            oGrid.Refresh()

        Catch ex As Exception
            ErrDisp("InitGrid : " + ex.Message.Trim, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        ' çıkış
        Me.Close()
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        ' kaydet
        Try
            Dim oSQL As New SQLServerClass
            Dim nRow As Integer = 0
            Dim oColumn As GridColumn

            Dim nSiraNo As Double = 0
            Dim cOlcuYeri As String = ""
            Dim nGrading As Double = 0
            Dim nSiralama As Double = 0
            Dim nTolerans As Double = 0
            Dim cIngilizceOlcuYeri As String = ""
            Dim cArapcaOlcuYeri As String = ""

            If oGrid.DataSource Is Nothing Then Exit Sub

            oSQL.OpenConn()

            oSQL.cSQLQuery = "delete olcuyeri "

            oSQL.SQLExecute()

            For nRow = 0 To oView.DataRowCount - 1

                nSiraNo = 0
                cOlcuYeri = ""
                nGrading = 0
                nSiralama = 0
                nTolerans = 0
                cIngilizceOlcuYeri = ""
                cArapcaOlcuYeri = ""

                oColumn = oView.Columns.ColumnByFieldName("SiraNo")
                If Not IsNothing(oView.GetRowCellValue(nRow, oColumn)) Then
                    nSiraNo = Convert.ToDouble(oView.GetRowCellValue(nRow, oColumn))
                End If

                oColumn = oView.Columns.ColumnByFieldName("OlcuYeri")
                If Not IsNothing(oView.GetRowCellValue(nRow, oColumn)) Then
                    cOlcuYeri = oView.GetRowCellValue(nRow, oColumn).ToString
                End If

                oColumn = oView.Columns.ColumnByFieldName("Grading")
                If Not IsNothing(oView.GetRowCellValue(nRow, oColumn)) Then
                    nGrading = Convert.ToDouble(oView.GetRowCellValue(nRow, oColumn))
                End If

                oColumn = oView.Columns.ColumnByFieldName("Siralama")
                If Not IsNothing(oView.GetRowCellValue(nRow, oColumn)) Then
                    nSiralama = Convert.ToDouble(oView.GetRowCellValue(nRow, oColumn))
                End If

                oColumn = oView.Columns.ColumnByFieldName("Tolerans")
                If Not IsNothing(oView.GetRowCellValue(nRow, oColumn)) Then
                    nTolerans = Convert.ToDouble(oView.GetRowCellValue(nRow, oColumn))
                End If

                oColumn = oView.Columns.ColumnByFieldName("ingilizceOlcuYeri")
                If Not IsNothing(oView.GetRowCellValue(nRow, oColumn)) Then
                    cIngilizceOlcuYeri = oView.GetRowCellValue(nRow, oColumn).ToString
                End If

                oColumn = oView.Columns.ColumnByFieldName("ArapcaOlcuYeri")
                If Not IsNothing(oView.GetRowCellValue(nRow, oColumn)) Then
                    cArapcaOlcuYeri = oView.GetRowCellValue(nRow, oColumn).ToString
                End If

                If cOlcuYeri.Trim <> "" Then

                    oSQL.cSQLQuery = "select top 1 olcuyeri " +
                                    " from olcuyerleri with (NOLOCK) " +
                                    " where olcuyeri = '" + SQLWriteString(cOlcuYeri, 300) + "' "

                    If Not oSQL.CheckExists Then

                        oSQL.cSQLQuery = "insert olcuyerleri (olcuyeri, grading, siralama, tolerans, ingilizceolcuyeri, arapcaolcuyeri) " +
                                    " values ('" + SQLWriteString(cOlcuYeri, 300) + "'," +
                                    SQLWriteDecimal(nGrading) + ", " +
                                    SQLWriteDecimal(nSiralama) + ", " +
                                    SQLWriteDecimal(nTolerans) + ", " +
                                    " '" + SQLWriteString(cIngilizceOlcuYeri, 300) + "', " +
                                    " N'" + cArapcaOlcuYeri + "'  ) "

                        oSQL.SQLExecute()
                    End If
                End If
            Next
            oSQL.CloseConn()
            oSQL = Nothing

            Me.Close()

        Catch ex As Exception
            ErrDisp("Kaydet : " + ex.Message.Trim, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        ' yazdır
        oGrid.ShowRibbonPrintPreview()
    End Sub

End Class