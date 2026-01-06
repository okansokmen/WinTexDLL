Imports DevExpress.XtraGrid.Views.Grid ' <-- Add this import for GridView
Imports System.Data.SqlClient
Imports DevExpress.XtraTabbedMdi

Partial Public Class HTMain

    Private _mdiTabs As XtraTabbedMdiManager

    Public Sub New()
        Try
            InitializeComponent()
        Catch ex As System.Exception
            ErrDisp(ex.Message, "HTMain.New", , , ex)
        End Try
    End Sub

    Private Sub EnsureTabbedMdi()
        Try
            If _mdiTabs Is Nothing Then
                _mdiTabs = New XtraTabbedMdiManager()
                _mdiTabs.MdiParent = Me
                _mdiTabs.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPagesAndTabControlHeader
                _mdiTabs.FloatOnDoubleClick = False
                _mdiTabs.HeaderButtonsShowMode = DevExpress.XtraTab.TabButtonShowMode.WhenNeeded
            End If
        Catch ex As Exception
            ErrDisp(ex.Message, "EnsureTabbedMdi", , , ex)
        End Try
    End Sub

    ' Parse and apply command-line arguments
    Private Sub ApplyCommandLineArgs(args As IEnumerable(Of String))
        Try
            If args Is Nothing Then Exit Sub

            ' Helper lambdas
            Dim getVal As Func(Of String, String) =
                Function(key As String)
                    For Each a In args
                        If a Is Nothing Then Continue For
                        Dim s = a.Trim()
                        If s.Length = 0 Then Continue For
                        If s.StartsWith("--") Then s = s.Substring(2)
                        If s.StartsWith("-") OrElse s.StartsWith("/") Then s = s.Substring(1)
                        Dim kv = s.Split(New Char() {"="c, ":"c}, 2, StringSplitOptions.None)
                        If kv.Length >= 1 AndAlso kv(0).Equals(key, StringComparison.OrdinalIgnoreCase) Then
                            Return If(kv.Length = 2, kv(1), "")
                        End If
                    Next
                    Return Nothing
                End Function

            Dim hasSw As Func(Of String, Boolean) =
                Function(key As String)
                    For Each a In args
                        If a Is Nothing Then Continue For
                        Dim s = a.Trim()
                        If s.Length = 0 Then Continue For
                        If s.StartsWith("--") Then s = s.Substring(2)
                        If s.StartsWith("-") OrElse s.StartsWith("/") Then s = s.Substring(1)
                        If s.Equals(key, StringComparison.OrdinalIgnoreCase) Then Return True
                    Next
                    Return False
                End Function

            ' open target form
            'Dim openVal = getVal("open")
            'If String.Equals(openVal, "UretimPlanlama1", StringComparison.OrdinalIgnoreCase) OrElse
            '   String.Equals(openVal, "uretimplanlama1", StringComparison.OrdinalIgnoreCase) OrElse
            '   hasSw("uretimplanlama") Then

            '    Dim up As UretimPlanlama1 = Me.MdiChildren.OfType(Of UretimPlanlama1)().FirstOrDefault()
            '    If up Is Nothing Then
            '        up = New UretimPlanlama1 With {.MdiParent = Me}
            '        up.init()
            '    End If
            'End If

            ' filter: all | open
            Dim filterVal = getVal("filter")
            If Not String.IsNullOrWhiteSpace(filterVal) Then
                If filterVal.Equals("all", StringComparison.OrdinalIgnoreCase) Then
                    cSiparisFilter = ""
                ElseIf filterVal.Equals("open", StringComparison.OrdinalIgnoreCase) Then
                    cSiparisFilter = " and exists (select x.dosyakapandi " &
                                     " from siparis x with (NOLOCK), sipmodel y with (NOLOCK) " &
                                     " where x.kullanicisipno = y.siparisno " &
                                     " and y.uretimtakipno = a.uretimtakipno " &
                                     " and (x.dosyakapandi is null or x.dosyakapandi = '' or x.dosyakapandi = 'H')) "
                End If
            End If

            ' owner override (optional): --owner=vera
            Dim ownerVal = getVal("owner")
            If Not String.IsNullOrWhiteSpace(ownerVal) Then
                oConnection.cOwner = ownerVal
            End If

            ' expand/collapse rows: --expand=1 | --collapse=1
            Dim expandVal = getVal("expand")
            Dim collapseVal = getVal("collapse")

            ' database connection via command-line: --server= --database= --username= --password=
            Dim srv = getVal("server")
            Dim db = getVal("database")
            Dim usr = getVal("username")
            If String.IsNullOrWhiteSpace(usr) Then usr = getVal("user")
            If String.IsNullOrWhiteSpace(usr) Then usr = getVal("uid")
            Dim pwd = getVal("password")
            If String.IsNullOrWhiteSpace(pwd) Then pwd = getVal("pwd")

            If Not String.IsNullOrWhiteSpace(srv) AndAlso
               Not String.IsNullOrWhiteSpace(db) AndAlso
               Not String.IsNullOrWhiteSpace(usr) AndAlso
               Not String.IsNullOrWhiteSpace(pwd) Then

                ' Apply to global connection settings (used by SQLServerClass default constructor)
                oConnection.cServer = srv.Trim()
                oConnection.cDatabase = db.Trim()
                oConnection.cUser = usr.Trim()
                oConnection.cPassword = pwd.Trim()

                ' Optional: quick validation (non-fatal)
                Try
                    Dim test As New SQLServerClass(False, oConnection.cServer, oConnection.cDatabase, oConnection.cUser, oConnection.cPassword)
                    If test.OpenConn() Then
                        test.CloseConn()
                    End If
                Catch
                End Try
            End If

            ' refresh target if open
            'Dim upForm As UretimPlanlama1 = Me.MdiChildren.OfType(Of UretimPlanlama1)().FirstOrDefault()
            'If upForm IsNot Nothing Then
            '    upForm.RefreshData()

            '    Dim gridView As GridView = TryCast(upForm.GridControl1.MainView, GridView)
            '    If gridView IsNot Nothing Then
            '        If String.Equals(expandVal, "1") Then
            '            For i As Integer = 0 To gridView.RowCount - 1
            '                gridView.ExpandMasterRow(i)
            '            Next
            '        ElseIf String.Equals(collapseVal, "1") Then
            '            For i As Integer = 0 To gridView.RowCount - 1
            '                gridView.CollapseMasterRow(i)
            '            Next
            '        End If
            '    End If
            'End If

        Catch ex As Exception
            ErrDisp(ex.Message, "ApplyCommandLineArgs", , , ex)
        End Try
    End Sub

    Private Sub HTMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            initWinTex()

            Me.Text = "WinTex Version - " & My.Application.Info.Version.ToString() & " - " & If(Environment.Is64BitProcess, "64-bit", "32-bit")

            ' Ensure tabbed MDI UI
            EnsureTabbedMdi()

            ' Apply command line after default init/open
            ApplyCommandLineArgs(My.Application.CommandLineArgs)

            'If InStr(oConnection.cOwner, "vera") > 0 Then
            '    ' Automatically open UretimPlanlama1 on startup and set as active MDI child
            '    Dim oUretimPlanlama1 As New UretimPlanlama1
            '    oUretimPlanlama1.MdiParent = Me
            '    oUretimPlanlama1.init()
            '    Me.ribbonPage1.Visible = False
            'Else
            '    Me.RibbonPage2.Visible = False
            'End If

        Catch ex As System.Exception
            ErrDisp(ex.Message, "HTMain_Load", , , ex)
        End Try
    End Sub

    Private Sub xcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles xcel.ItemClick
        Try
            ' Find the first open UretimPlanlama1 MDI child
            For Each frm As Form In Me.MdiChildren
                If TypeOf frm Is UretimPlanlama1 Then
                    Dim upForm As UretimPlanlama1 = CType(frm, UretimPlanlama1)
                    upForm.ExportToExcel()
                    Exit For
                End If
            Next
        Catch ex As System.Exception
            ErrDisp(ex.Message, "xcel_ItemClick", , , ex)
        End Try
    End Sub

    Private Sub BarButtonItem4_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem4.ItemClick
        Try
            ' Expand all master rows in the first open UretimPlanlama1 MDI child
            For Each frm As Form In Me.MdiChildren
                If TypeOf frm Is UretimPlanlama1 Then
                    Dim upForm As UretimPlanlama1 = CType(frm, UretimPlanlama1)
                    Dim gridView As GridView = TryCast(upForm.GridControl1.MainView, GridView)
                    If gridView IsNot Nothing Then
                        For i As Integer = 0 To gridView.RowCount - 1
                            gridView.ExpandMasterRow(i)
                        Next
                    End If
                    Exit For
                End If
            Next
        Catch ex As System.Exception
            ErrDisp(ex.Message, "BarButtonItem4_ItemClick", , , ex)
        End Try
    End Sub

    Private Sub BarButtonItem5_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem5.ItemClick
        Try
            ' Collapse all master rows in the first open UretimPlanlama1 MDI child
            For Each frm As Form In Me.MdiChildren
                If TypeOf frm Is UretimPlanlama1 Then
                    Dim upForm As UretimPlanlama1 = CType(frm, UretimPlanlama1)
                    Dim gridView As GridView = TryCast(upForm.GridControl1.MainView, GridView)
                    If gridView IsNot Nothing Then
                        For i As Integer = 0 To gridView.RowCount - 1
                            gridView.CollapseMasterRow(i)
                        Next
                    End If
                    Exit For
                End If
            Next
        Catch ex As System.Exception
            ErrDisp(ex.Message, "BarButtonItem5_ItemClick", , , ex)
        End Try
    End Sub

    Private Sub BarButtonItem6_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem6.ItemClick
        ' kaydet (parameterized) - reverted to only baslama_tar and bitis_tar
        Try
            Dim cFirmaEski As String = ""

            Dim targetForm As UretimPlanlama1 = Me.MdiChildren.OfType(Of UretimPlanlama1)().FirstOrDefault()
            If targetForm Is Nothing Then
                MessageBox.Show("Uretim planlama ekranı açık değil", "Kaydet", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim gv As GridView = TryCast(targetForm.GridControl1.MainView, GridView)
            If gv Is Nothing Then
                MessageBox.Show("Grid bulunamadı", "Kaydet", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            gv.CloseEditor()
            gv.UpdateCurrentRow()

            ' --- Detect if there is anything to save (SatirDuzeltildi = 'E') ---
            Dim hasChanges As Boolean = False
            For i As Integer = 0 To gv.DataRowCount - 1
                Dim rh = gv.GetVisibleRowHandle(i)
                If rh < 0 Then Continue For
                Dim sat = gv.GetRowCellValue(rh, "SatirDuzeltildi")
                If sat IsNot Nothing AndAlso sat IsNot DBNull.Value AndAlso sat.ToString().Trim().ToUpperInvariant() = "E" Then
                    hasChanges = True
                    Exit For
                End If
            Next

            If Not hasChanges Then
                MessageBox.Show("Kaydedilecek değişiklik yok", "Kaydet", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            ' --- Ask user confirmation ---
            If MessageBox.Show("Değişiklikleri kaydetmek istiyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) <> DialogResult.Yes Then
                Exit Sub
            End If

            Dim oSQL As New SQLServerClass
            oSQL.init(4)
            oSQL.OpenConn()

            For i As Integer = 0 To gv.DataRowCount - 1
                Dim rowHandle = gv.GetVisibleRowHandle(i)
                If rowHandle < 0 Then Continue For

                Dim SatirDuzeltildi = gv.GetRowCellValue(rowHandle, "SatirDuzeltildi")
                If SatirDuzeltildi Is Nothing OrElse SatirDuzeltildi Is DBNull.Value Then Continue For
                Dim cSatirDuzeltildi As String = SatirDuzeltildi.ToString().Trim()
                If cSatirDuzeltildi = String.Empty Or cSatirDuzeltildi = "H" Then Continue For

                Dim UtfObj = gv.GetRowCellValue(rowHandle, "UretimTakipNo")
                If UtfObj Is Nothing OrElse UtfObj Is DBNull.Value Then Continue For
                Dim cUtfNo As String = UtfObj.ToString().Trim()
                If cUtfNo = String.Empty Then Continue For

                Dim isemObj = gv.GetRowCellValue(rowHandle, "isemrino")
                If isemObj Is Nothing OrElse isemObj Is DBNull.Value Then Continue For
                Dim cIsemNo As String = isemObj.ToString().Trim()
                If cIsemNo = String.Empty Then Continue For

                Dim firmaObj = gv.GetRowCellValue(rowHandle, "Firma")
                If firmaObj Is Nothing OrElse firmaObj Is DBNull.Value Then Continue For
                Dim cFirma As String = firmaObj.ToString().Trim()
                If cFirma = String.Empty Then Continue For

                Dim aciklamaObj = gv.GetRowCellValue(rowHandle, "aciklama")
                If aciklamaObj Is Nothing OrElse aciklamaObj Is DBNull.Value Then Continue For
                Dim cAciklama As String = aciklamaObj.ToString().Trim()

                ' --- Save baslama_tar & bitis_tar (uretimisdetayi) ---
                Dim basObj = gv.GetRowCellValue(rowHandle, "baslama_tar")
                Dim dBaslama As Date = If(basObj IsNot Nothing AndAlso basObj IsNot DBNull.Value, Convert.ToDateTime(basObj), #1/1/1950#)

                Dim bitObj = gv.GetRowCellValue(rowHandle, "bitis_tar")
                Dim dBitis As Date = If(bitObj IsNot Nothing AndAlso bitObj IsNot DBNull.Value, Convert.ToDateTime(bitObj), #1/1/1950#)

                oSQL.cSQLQuery = "Set dateformat dmy " &
                                 " update uretimisdetayi Set " &
                                 " baslama_tar = '" & SQLWriteDate(dBaslama) & "' , " +
                                 " bitis_tar = '" & SQLWriteDate(dBitis) + "' " &
                                 " where isemrino = '" & cIsemNo & "' "
                oSQL.SQLExecute()

                oSQL.cSQLQuery = "update uretimisemri set " &
                                 " aciklama = '" & SQLWriteString(cAciklama, 100) & "' " +
                                 " where isemrino = '" & cIsemNo & "' "
                oSQL.SQLExecute()

                ' --- Save firma (uretimis emri) ---
                oSQL.cSQLQuery = "select top 1 firma " &
                                " from uretimisemri with (NOLOCK) " &
                                " where isemrino = '" & cIsemNo & "' "

                cFirmaEski = oSQL.DBReadString

                If cFirmaEski.Trim <> cFirma.Trim Then

                    oSQL.cSQLQuery = "update uretimisemri set " &
                                    " firma = '" & cFirma.Trim & "' " &
                                    " where uretimtakipno = '" & cUtfNo.Trim & "' " &
                                    " and firma = '" & cFirmaEski.Trim & "' "

                    oSQL.SQLExecute()
                End If

                ' --- Save siparis date fields ---
                Dim SiparisObj = gv.GetRowCellValue(rowHandle, "SiparisNo")
                If SiparisObj Is Nothing OrElse SiparisObj Is DBNull.Value Then Continue For
                Dim cSiparisNo As String = SiparisObj.ToString().Trim()
                If cSiparisNo = String.Empty Then Continue For

                Dim PlanlananKesimOk = gv.GetRowCellValue(rowHandle, "PlanlananKesimOk")
                Dim dPlanlananKesimOk As Date = If(PlanlananKesimOk IsNot Nothing AndAlso PlanlananKesimOk IsNot DBNull.Value, Convert.ToDateTime(PlanlananKesimOk), #1/1/1950#)

                Dim GercekKesimOk = gv.GetRowCellValue(rowHandle, "GercekKesimOk")
                Dim dGercekKesimOk As Date = If(GercekKesimOk IsNot Nothing AndAlso GercekKesimOk IsNot DBNull.Value, Convert.ToDateTime(GercekKesimOk), #1/1/1950#)

                Dim UretimeCikis = gv.GetRowCellValue(rowHandle, "UretimeCikis")
                Dim dUretimeCikis As Date = If(UretimeCikis IsNot Nothing AndAlso UretimeCikis IsNot DBNull.Value, Convert.ToDateTime(UretimeCikis), #1/1/1950#)

                Dim kumasObj = gv.GetRowCellValue(rowHandle, "KumasOk")
                Dim dKumasOk As Date = If(kumasObj IsNot Nothing AndAlso kumasObj IsNot DBNull.Value, Convert.ToDateTime(kumasObj), #1/1/1950#)

                Dim MalzemeOk = gv.GetRowCellValue(rowHandle, "MalzemeOk")
                Dim dMalzemeOk As Date = If(MalzemeOk IsNot Nothing AndAlso MalzemeOk IsNot DBNull.Value, Convert.ToDateTime(MalzemeOk), #1/1/1950#)

                Dim EkSevk4 = gv.GetRowCellValue(rowHandle, "EkSevk4")
                Dim dEkSevk4 As Date = If(EkSevk4 IsNot Nothing AndAlso EkSevk4 IsNot DBNull.Value, Convert.ToDateTime(EkSevk4), #1/1/1950#)

                Dim EkSevk5 = gv.GetRowCellValue(rowHandle, "EkSevk5")
                Dim dEkSevk5 As Date = If(EkSevk5 IsNot Nothing AndAlso EkSevk5 IsNot DBNull.Value, Convert.ToDateTime(EkSevk5), #1/1/1950#)

                oSQL.cSQLQuery = "set dateformat dmy " +
                                 " update siparis set " +
                                 " kesimplok = '" + SQLWriteDate(dPlanlananKesimOk) + "' , " +
                                 " KesimOK = '" + SQLWriteDate(dGercekKesimOk) + "' , " +
                                 " eksevktarihi1 = '" + SQLWriteDate(dUretimeCikis) + "' , " +
                                 " eksevktarihi2 = '" + SQLWriteDate(dKumasOk) + "' , " +
                                 " eksevktarihi3 = '" + SQLWriteDate(dMalzemeOk) + "' , " +
                                 " eksevktarihi4 = '" + SQLWriteDate(dEkSevk4) + "' , " +
                                 " eksevktarihi5 = '" + SQLWriteDate(dEkSevk5) + "'  " +
                                 " where kullanicisipno = '" + cSiparisNo + "' "
                oSQL.SQLExecute()
            Next

            oSQL.CloseConn()
            MessageBox.Show("Kayıtlar güncellendi", "Kaydet", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            ErrDisp(ex.Message, "BarButtonItem6_ItemClick", , , ex)
        End Try
    End Sub


    Private Sub BarButtonItem9_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem9.ItemClick
        ' bütün siparişler
        Try
            cSiparisFilter = ""

            Dim oUretimPlanlama1 As New UretimPlanlama1
            oUretimPlanlama1.MdiParent = Me
            oUretimPlanlama1.init()
        Catch ex As System.Exception
            ErrDisp(ex.Message, "BarButtonItem3_ItemClick", , , ex)
        End Try

    End Sub

    Private Sub BarButtonItem10_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem10.ItemClick
        ' açık siparişler
        Try
            Me.Cursor = Cursors.WaitCursor
            cSiparisFilter = " and exists (select x.dosyakapandi " &
                                        " from siparis x with (NOLOCK), sipmodel y with (NOLOCK) " &
                                        " where x.kullanicisipno = y.siparisno " &
                                        " and y.uretimtakipno = a.uretimtakipno " &
                                        " and (x.dosyakapandi is null or x.dosyakapandi = '' or x.dosyakapandi = 'H')) "

            Dim oUretimPlanlama1 As New UretimPlanlama1
            oUretimPlanlama1.MdiParent = Me
            oUretimPlanlama1.init()
            Me.Cursor = Cursors.Default
        Catch ex As System.Exception
            Me.Cursor = Cursors.Default
            ErrDisp(ex.Message, "BarButtonItem3_ItemClick", , , ex)
        End Try
    End Sub

    Private Sub BarButtonItem3_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        ' Gannt
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim oTakvim As New Takvim
            oTakvim.MdiParent = Me
            oTakvim.init()
            Me.Cursor = Cursors.Default
        Catch ex As System.Exception
            Me.Cursor = Cursors.Default
            ErrDisp(ex.Message, "BarButtonItem3_ItemClick_1", , , ex)
        End Try
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        ' inditex sipariş aktarımı
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim oSiparisInditex As New SiparisInditex
            oSiparisInditex.MdiParent = Me
            oSiparisInditex.init()
            Me.Cursor = Cursors.Default
        Catch ex As System.Exception
            Me.Cursor = Cursors.Default
            ErrDisp(ex.Message, "BarButtonItem2_ItemClick", , , ex)
        End Try
    End Sub

    Private Sub BarButtonItem8_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem8.ItemClick
        ' çıkış
        On Error Resume Next
        Application.Exit()
    End Sub


End Class
