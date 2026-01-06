Public Class AnaMenu
    Public Sub init()
        Me.Show()
    End Sub

    Private Sub AnaMenu_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        ' Markalar indir
        Dim oForm As New UrunClass
        oForm.GetList(2, Me)
        oForm.CloseClient()
    End Sub

    Private Sub BarButtonItem4_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem4.ItemClick
        ' Kategorileri indir
        Dim oForm As New UrunClass
        oForm.GetList(1, Me)
        oForm.CloseClient()
    End Sub

    Private Sub BarButtonItem5_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem5.ItemClick
        ' Tedarikçiler indir
        Dim oForm As New UrunClass
        oForm.GetList(3, Me)
        oForm.CloseClient()
    End Sub

    Private Sub BarButtonItem6_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem6.ItemClick
        ' Ürünleri indir
        Dim oForm As New UrunClass
        oForm.GetList(4, Me)
        oForm.CloseClient()
    End Sub

    Private Sub BarButtonItem7_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem7.ItemClick
        Me.Close()
    End Sub

    Private Sub BarButtonItem8_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem8.ItemClick
        ' Teknik detay grupları indir
        Dim oForm As New UrunClass
        oForm.GetList(5, Me)
        oForm.CloseClient()
    End Sub

    Private Sub BarButtonItem9_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem9.ItemClick
        ' Teknik detay özellikleri indir
        Dim oForm As New UrunClass
        oForm.GetList(6, Me)
        oForm.CloseClient()
    End Sub

    Private Sub BarButtonItem10_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem10.ItemClick
        ' Teknik detay değerleri indir
        Dim oForm As New UrunClass
        oForm.GetList(7, Me)
        oForm.CloseClient()
    End Sub

    Private Sub BarButtonItem11_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem11.ItemClick
        ' Etikeler indir
        Dim oForm As New UrunClass
        oForm.GetList(8, Me)
        oForm.CloseClient()
    End Sub

    Private Sub BarButtonItem12_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem12.ItemClick
        ' Ürün etiketleri indir
        Dim oForm As New UrunClass
        oForm.GetList(9, Me)
        oForm.CloseClient()
    End Sub

    Private Sub BarButtonItem13_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem13.ItemClick
        ' Varyasyonlar indir
        Dim oForm As New UrunClass
        oForm.GetList(10, Me)
        oForm.CloseClient()
    End Sub

    Private Sub BarButtonItem14_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem14.ItemClick
        ' Asorti grupları indir
        Dim oForm As New UrunClass
        oForm.GetList(11, Me)
        oForm.CloseClient()
    End Sub

    Private Sub BarButtonItem15_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem15.ItemClick
        ' Ek seçenek değerleri indir
        Dim oForm As New UrunClass
        oForm.GetList(12, Me)
        oForm.CloseClient()
    End Sub

    Private Sub BarButtonItem16_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem16.ItemClick
        ' Ek seçenek grupları indir
        Dim oForm As New UrunClass
        oForm.GetList(13, Me)
        oForm.CloseClient()
    End Sub

    Private Sub BarButtonItem17_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem17.ItemClick
        ' Döviz indir
        Dim oForm As New UrunClass
        oForm.GetList(14, Me)
        oForm.CloseClient()
    End Sub

    Private Sub BarButtonItem18_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem18.ItemClick
        On Error Resume Next
        Dim oForm As New frmTicimaxParameters
        oForm.init()
    End Sub

    Private Sub BarButtonItem21_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem21.ItemClick
        ' siparişleri indir
        Dim oForm As New SiparisClass
        oForm.GetList(1, Me)
        oForm.CloseClient()
    End Sub

    Private Sub BarButtonItem25_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem25.ItemClick
        ' ürünleri gönder
        Try
            Dim oSQL As New SQLServerClass
            Dim oDataTable As New DataTable

            oSQL.OpenConn()
            oSQL.cSQLQuery = GetSQLQuery(2)
            oDataTable = oSQL.SQLSelect()
            oSQL.CloseConn()

            Dim ofrmListView As New frmListView
            ofrmListView.init2(oDataTable, "WinTex Ürünler", Me, 1)

        Catch ex As Exception
            ErrDisp("BarButtonItem25_ItemClick", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub BarButtonItem26_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem26.ItemClick
        ' Ödeme Tipleri
        Dim oForm As New SiparisClass
        oForm.GetList(2, Me)
        oForm.CloseClient()
    End Sub

    Private Sub BarButtonItem27_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem27.ItemClick
        ' üyeler
        Dim oForm As New UyeClass
        oForm.GetList(1, Me)
        oForm.CloseClient()
    End Sub

    Private Sub BarButtonItem28_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem28.ItemClick
        ' üye adresleri
        Dim oForm As New UyeClass
        oForm.GetList(2, Me)
        oForm.CloseClient()
    End Sub

    Private Sub BarButtonItem29_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem29.ItemClick
        ' üye türleri
        Dim oForm As New UyeClass
        oForm.GetList(3, Me)
        oForm.CloseClient()
    End Sub

    Private Sub BarButtonItem30_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem30.ItemClick
        ' siparişleri ticimax a gönder
        Try
            Dim oSQL As New SQLServerClass
            Dim oDataTable As New DataTable

            oSQL.OpenConn()
            oSQL.cSQLQuery = GetSQLQuery(3)
            oDataTable = oSQL.SQLSelect()
            oSQL.CloseConn()

            Dim ofrmListView As New frmListView
            ofrmListView.init2(oDataTable, "WinTex Siparişlerinden TiciMax Sistemini Güncelle", Me, 2)

        Catch ex As Exception
            ErrDisp("BarButtonItem30_ItemClick", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub BarButtonItem31_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem31.ItemClick
        Try
            Dim oSiparis As New SiparisClass
            oSiparis.ReadSiparisFromTiciMax()
            oSiparis.CloseClient()
            oSiparis = Nothing
            MsgBox("TiciMax ta açılan yeni siparişler WinTexe yüklendi")

        Catch ex As Exception
            ErrDisp("BarButtonItem31_ItemClick", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub BarButtonItem32_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem32.ItemClick
        Try
            Dim oUrun As New UrunClass
            oUrun.SendProducts()
            oUrun.CloseClient()
            oUrun = Nothing
            MsgBox("Ürünler TiciMaxa Gönderildi")

        Catch ex As Exception
            ErrDisp("BarButtonItem32_ItemClick : " + ex.Message, Me.Name,,, ex)
        End Try

    End Sub

    Private Sub BarButtonItem33_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem33.ItemClick
        Try
            Dim oSiparis As New SiparisClass
            oSiparis.WriteSiparislerToTiciMax()
            oSiparis.CloseClient()
            oSiparis = Nothing
            MsgBox("Siparişler TiciMaxa Gönderildi")

        Catch ex As Exception
            ErrDisp("BarButtonItem33_ItemClick : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub
End Class