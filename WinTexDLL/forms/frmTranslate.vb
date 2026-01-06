Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors

Partial Public Class frmTranslate

    Public Sub New()
        InitializeComponent()

        Dim dataSource As BindingList(Of Customer) = GetDataSource()
        gridControl.DataSource = dataSource
        bsiRecordsCount.Caption = "RECORDS : " & dataSource.Count
    End Sub
    Private Sub bbiPrintPreview_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs) Handles bbiPrintPreview.ItemClick
        gridControl.ShowRibbonPrintPreview()
    End Sub
    Public Function GetDataSource() As BindingList(Of Customer)
        Dim result As New BindingList(Of Customer)()
        result.Add(New Customer() With {.ID = 1, .Name = "ACME", .Address = "2525 E El Segundo Blvd", .City = "El Segundo", .State = "CA", .ZipCode = "90245", .Phone = "(310) 536-0611"})
        result.Add(New Customer() With {.ID = 2, .Name = "Electronics Depot", .Address = "2455 Paces Ferry Road NW", .City = "Atlanta", .State = "GA", .ZipCode = "30339", .Phone = "(800) 595-3232"})
        Return result
    End Function
    Public Class Customer
        <Key, Display(AutoGenerateField:=False)>
        Public Property ID() As Integer
        <Required>
        Public Property Name() As String
        Public Property Address() As String
        Public Property City() As String
        Public Property State() As String
        <Display(Name:="Zip Code")>
        Public Property ZipCode() As String
        Public Property Phone() As String
    End Class

    Private Sub frmTranslate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'TESDataSet.tercume' table. You can move, or remove it, as needed.
        Me.TercumeTableAdapter.Fill(Me.TESDataSet.tercume)

    End Sub

End Class
