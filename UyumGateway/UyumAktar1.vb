Imports System
Imports System.Configuration
Imports System.IO
Imports System.Windows.Forms
Imports System.Data
Imports Npgsql
Imports System.Threading.Tasks.TaskExtensions
Imports Microsoft.VisualBasic
Imports C1.Win.C1TrueDBGrid

Public Class UyumAktar1

    Dim oKaynak As PostgreClass
    Dim oHedef As PostgreClass

    Public SLLU As String = ConfigurationManager.AppSettings("SLLU")       '  Kaynak Veritabanı  Where Şartı
    Public SLLG As String = ConfigurationManager.AppSettings("SLLG")       '  Kaynak Veritabanı  Where Şartı  0 olanlar
    Public SLLG13 As String = ConfigurationManager.AppSettings("SLLG13")   '  Kaynak Veritabanı  Where Şartı 0 ve 3 olanlar
    Public NSrg As String = ConfigurationManager.AppSettings("NSrg")       '  Veritabanı1 deki Log dosyasını Dönen değerde hata yoksa update sorgusu
    Public HSrg As String = ConfigurationManager.AppSettings("HSrg")       '  Veritabanı1 deki Log dosyasını Dönen değerde hata varsa update sorgusu
    Public DSrg As String = ConfigurationManager.AppSettings("DSrg")       '  Veritabanı1 deki Log dosyasını xp_op = 9 yapma sorgusu

    Public VTB1 As String = ConfigurationManager.AppSettings("VTB1")       '  Kaynak Veritabanı için Veritabanı Adı
    Public SRV1 As String = ConfigurationManager.AppSettings("SRV1")       '  Kaynak Veritabanı için Sunucu Adı-Adresi
    Public VTB2 As String = ConfigurationManager.AppSettings("VTB2")       '  Hedef Veritabanı için Veritabanı Adı
    Public SRV2 As String = ConfigurationManager.AppSettings("SRV2")       '  Hedef Veritabanı için Sunucu Adı-Adresi

    Public OD01 As String = ConfigurationManager.AppSettings("OD01")       '  UPDATE KOMUT İÇİNDE GELEN VERİLERDE TEMİZLENECEK ÖZEL DURUMLAR 01
    Public OD02 As String = ConfigurationManager.AppSettings("OD02")       '  UPDATE KOMUT İÇİNDE GELEN VERİLERDE TEMİZLENECEK ÖZEL DURUMLAR 02
    Public OD03 As String = ConfigurationManager.AppSettings("OD03")       '  UPDATE KOMUT İÇİNDE GELEN VERİLERDE TEMİZLENECEK ÖZEL DURUMLAR 03
    Public OD04 As String = ConfigurationManager.AppSettings("OD04")       '  UPDATE KOMUT İÇİNDE GELEN VERİLERDE TEMİZLENECEK ÖZEL DURUMLAR 04
    Public OD05 As String = ConfigurationManager.AppSettings("OD05")       '  UPDATE KOMUT İÇİNDE GELEN VERİLERDE TEMİZLENECEK ÖZEL DURUMLAR 05
    Public nl As Integer = 0

    Public USR1 As String = "uyum"
    Public USR2 As String = "uyum"
    Public PSW1 As String = "12345"
    Public PSW2 As String = "12345"
    Public M As Double = 0
    Public Mn As Double = Convert.ToDouble(ConfigurationManager.AppSettings("Mn"))
    Public Hr As Double = Convert.ToDouble(ConfigurationManager.AppSettings("Hr"))

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Icon = My.Resources.wintex
    End Sub

    Private Sub UyumMain_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try
            oKaynak = New PostgreClass(SRV1, VTB1, USR1, PSW1)
            oHedef = New PostgreClass(SRV2, VTB2, USR2, PSW2)

            Me.MdiParent = HTMain

            Me.C1TrueDBGrid2.Columns.Add(New C1.Win.C1TrueDBGrid.C1DataColumn("ID", GetType(String)))
            Me.C1TrueDBGrid2.Columns.Add(New C1.Win.C1TrueDBGrid.C1DataColumn("Komut", GetType(String)))
            Me.C1TrueDBGrid2.Columns.Add(New C1.Win.C1TrueDBGrid.C1DataColumn("Tip", GetType(String)))
            Me.C1TrueDBGrid2.SetDataBinding()

            Me.C1TrueDBGrid1.FilterBar = True
            Me.C1TrueDBGrid1.AllowSort = False

            Me.C1TrueDBGrid2.FilterBar = True
            Me.C1TrueDBGrid2.AllowSort = False

            Me.Text = "Kaynak : " + SRV1 + "/" + VTB1 + " Hedef : " + SRV2 + "/" + VTB2

            Me.WindowState = FormWindowState.Maximized

        Catch ex As Exception
            ErrDisp(ex, Me.Name)
        End Try
    End Sub

    Private Sub KomutOlustur()

        Dim aDelimiterChars() As Char = {System.Convert.ToChar(";"), ControlChars.Tab}
        Dim nRow As Integer = 0
        Dim si As Integer = 0
        Dim CiftTirnak As String = ControlChars.Quote

        Dim Kmt As String = ""
        Dim Typ As String = ""
        Dim Id As String = ""
        Dim tbln As String = ""
        Dim Fldn As String = ""
        Dim text As String = ""
        Dim opId As String = ""
        Dim Oprs As String = ""
        Dim words() As String
        Dim word As String = ""

        Dim nm As Integer = 0
        Dim c As Integer = 0
        Dim kk0 As String = ""
        Dim kk1 As String = ""
        Dim kk2 As String = ""
        Dim kk3 As String = ""

        Dim w2 As String = ""
        Dim uz1 As Integer = 0
        Dim uz2 As Integer = 0

        Try

            For nRow = 0 To Me.C1TrueDBGrid1.RowCount - 1

                Kmt = ""
                Typ = ""

                Id = Me.C1TrueDBGrid1.Columns("ID").CellValue(nRow).ToString
                tbln = Me.C1TrueDBGrid1.Columns("TABLE_NAME").CellValue(nRow).ToString
                Fldn = Me.C1TrueDBGrid1.Columns("TABLE_FIED_M").CellValue(nRow).ToString
                text = Me.C1TrueDBGrid1.Columns("OP_DATA").CellValue(nRow).ToString
                opId = Me.C1TrueDBGrid1.Columns("OP_ID").CellValue(nRow).ToString
                Oprs = Me.C1TrueDBGrid1.Columns("OP").CellValue(nRow).ToString

                If OD01.Trim <> "" Then text = text.Replace(OD01, "")
                If OD02.Trim <> "" Then text = text.Replace(OD02, "")
                If OD03.Trim <> "" Then text = text.Replace(OD03, "")
                If OD04.Trim <> "" Then text = text.Replace(OD04, "")
                If OD05.Trim <> "" Then text = text.Replace(OD05, "")

                text = text.Replace(": " + CiftTirnak + CiftTirnak, ": " + CiftTirnak + "_" + CiftTirnak)
                text = text.Replace(CiftTirnak + ":", ";")
                text = text.Replace("{" + CiftTirnak, ";")
                text = text.Replace(", " + CiftTirnak, ";")
                text = text.Replace(" " + CiftTirnak, ";")
                text = text.Replace("}", "")
                text = text.Replace("'", "")
                text = text.Replace(CiftTirnak, "")

                words = text.Split(aDelimiterChars)

                Select Case Oprs
                    Case "D"
                        Kmt = "Delete from " + tbln + " where " + Fldn + "=" + opId + ";"
                        Typ = "Delete Command"

                    Case "I"
                        Typ = "Insert Command"
                        nm = 0
                        c = 0
                        kk1 = ""
                        kk2 = ""
                        kk3 = "INSERT INTO " + tbln + " ("

                        For Each word In words
                            If word <> "" And word <> "." And word <> ":" Then
                                w2 = word
                                If w2.Substring(0, 1) = ":" Then w2 = word.Substring(1, (word.Length - 1))

                                c = c + 1
                                Select Case c
                                    Case 1
                                        If (w2 = "amt_net" And tbln = "hrmt_payroll") Then
                                            nm = 1
                                            kk1 = kk1 + w2 + ", zz_rodenen, "
                                        Else
                                            kk1 = kk1 + w2 + ","
                                        End If
                                    Case 2
                                        w2 = w2.Replace(" ", "")
                                        If (tbln = "hrmt_payroll" And nm = 1) Then
                                            kk2 = kk2 + "'" + w2 + "','" + w2 + "',"
                                            nm = 0
                                        ElseIf (w2 = "null") Then
                                            kk2 = kk2 + w2 + ","
                                        Else
                                            kk2 = kk2 + "'" + w2 + "',"
                                        End If
                                        c = 0
                                End Select
                            End If
                        Next
                        If (kk1.Length > 2 And kk2.Length > 2) Then
                            uz1 = kk1.Length - 1
                            uz2 = kk2.Length - 1
                            Kmt = kk3 + kk1.Substring(0, uz1) + ")  VALUES(" + kk2.Substring(0, uz2) + ");"
                        End If

                    Case "U"
                        Typ = "Update Command"
                        c = 0
                        kk0 = ""
                        kk1 = ""
                        kk2 = ""
                        kk3 = "UPDATE " + tbln + " set "
                        Kmt = ""
                        nl = 0

                        For Each word In words
                            If (word <> "" And word <> "." And word <> ":") Then
                                w2 = word
                                If (w2.Substring((word.Length - 1), 1) = ".") Then w2 = word.Substring(0, (word.Length - 1))

                                c = c + 1
                                Select Case c
                                    Case 1
                                        kk1 = w2
                                    Case 2
                                        w2 = w2.Replace(" ", "")
                                        kk2 = w2

                                        If (w2 = "null" Or w2 = "") Then
                                            kk2 = ""
                                            If (kk1.IndexOf("_date") > -1) Then kk2 = "1.01.0001"
                                            If (kk1.IndexOf("_id") > -1) Then kk2 = "0"
                                            If (kk1.IndexOf("record") > -1) Then kk2 = "0"
                                            If (kk1.IndexOf("source") > -1) Then kk2 = "0"
                                            If (kk1.IndexOf("serial") > -1) Then kk2 = "0"
                                            If (kk1.IndexOf("zz_expense_") > -1) Then kk2 = "0"
                                            If (kk1.IndexOf("voucher_no") > -1) Then kk2 = "0"
                                        ElseIf (w2 = "") Then
                                            kk2 = ""
                                        End If

                                        If kk0 = "" Then
                                            If (kk1 = "note_large") Then
                                                kk2 = "-"
                                            ElseIf (kk1 = "amt_net" And tbln = "hrmt_payroll") Then
                                                kk0 = kk1 + "='" + kk2 + "', zz_rodenen='" + kk2 + "'"
                                            Else
                                                kk0 = kk1 + "='" + kk2 + "'"
                                            End If
                                        Else
                                            If (kk1 = "note_large") Then
                                                kk2 = "-"
                                            ElseIf (kk1 = "amt_net" And tbln = "hrmt_payroll") Then
                                                kk0 = kk0 + "," + kk1 + "='" + kk2 + "', zz_rodenen ='" + kk2 + "'"
                                            Else
                                                kk0 = kk0 + "," + kk1 + "='" + kk2 + "'"
                                            End If
                                        End If

                                        c = 0
                                End Select
                            End If
                        Next
                        Kmt = kk3 + kk0 + " where " + Fldn + "=" + opId + ";"
                End Select

                si = C1TrueDBGrid2.AddRows(1)
                C1TrueDBGrid2.Rows(si).Item(0) = Id
                C1TrueDBGrid2.Rows(si).Item(1) = Kmt
                C1TrueDBGrid2.Rows(si).Item(2) = Typ
                C1TrueDBGrid2.Refresh()

                C1SuperLabel1.Text = "Toplam Satır " + Me.C1TrueDBGrid1.RowCount.ToString
                C1SuperLabel1.Refresh()
            Next

            C1TrueDBGrid1.Splits(0).DisplayColumns("ID").AutoSize()
            C1TrueDBGrid1.Splits(0).DisplayColumns("TABLE_NAME").AutoSize()
            C1TrueDBGrid1.Splits(0).DisplayColumns("TABLE_FIED_M").AutoSize()
            C1TrueDBGrid1.Splits(0).DisplayColumns("OP_DATA").AutoSize()
            C1TrueDBGrid1.Splits(0).DisplayColumns("OP").AutoSize()
            C1TrueDBGrid1.Refresh()

            'C1TrueDBGrid2.Splits(0).DisplayColumns("ID").AutoSize()
            'C1TrueDBGrid2.Splits(0).DisplayColumns("Komut").AutoSize()
            'C1TrueDBGrid2.Splits(0).DisplayColumns("Tip").AutoSize()
            'C1TrueDBGrid2.Refresh()

        Catch ex As Exception
            ErrDisp(ex, Me.Name)
        End Try
    End Sub

    Private Sub KomutYaz()

        Dim cMsj As String = ""
        Dim cMID As String = ""
        Dim cSorgu As String = ""
        Dim cSQL As String = ""
        Dim nRow As Integer = 0
        Dim cErrorMessage As String = ""

        Try
            For nRow = 0 To Me.C1TrueDBGrid2.RowCount - 1
                cMsj = ""
                cSorgu = ""
                cMID = Me.C1TrueDBGrid2.Columns(0).CellValue(nRow).ToString
                cSQL = Me.C1TrueDBGrid2.Columns(1).CellValue(nRow).ToString
                If oHedef.PGExecuteOpenCloseConnection(cSQL, cErrorMessage) Then
                    cMsj = "OK"
                    cSorgu = NSrg + "xp_error = '" + cMsj + "' where id = " + cMID
                Else
                    cMsj = cErrorMessage
                    If cMsj = "" Then cMsj = "Bilinmeyen Hata"
                    cSorgu = HSrg + "xp_error = '" + Mid(cMsj, 1, 100).Trim + "' where id = " + cMID
                End If
                C1TrueDBGrid2.Rows(nRow).Item(2) = cMsj
                oKaynak.PGExecuteOpenCloseConnection(cSorgu)


                C1SuperLabel2.Text = "Aktarılan Satır " + nRow.ToString
                C1SuperLabel2.Refresh()
            Next

            C1Button4.Enabled = False
            MessageBox.Show("İşlem tamamlandı")

        Catch ex As Exception
            ErrDisp(ex, Me.Name)
        End Try
    End Sub

    Private Sub EkranTemizle()
        Try
            C1SuperLabel1.Text = "Toplam Satır"
            C1SuperLabel2.Text = "Aktarılan Satır"

            C1TrueDBGrid1.DataSource = Nothing
            C1TrueDBGrid1.Rows.Clear()
            C1TrueDBGrid1.Refresh()

            C1TrueDBGrid2.Rows.Clear()
            C1TrueDBGrid2.Refresh()

            C1Button4.Enabled = True

        Catch ex As Exception
            ErrDisp(ex, Me.Name)
        End Try
    End Sub

    Private Sub RunTransfer(nCase As Integer)

        Dim cErrorMessage As String = ""
        Dim oDataTable As DataTable
        Dim cSQL As String = ""

        Try
            Select Case nCase
                Case 1
                    cSQL = SLLG13
                Case 2
                    cSQL = SLLG
            End Select

            EkranTemizle()
            If oKaynak.PGExecuteOpenCloseConnection(SLLU, cErrorMessage) Then
                oDataTable = oKaynak.PGSelectOpenCloseConnection(cSQL, cErrorMessage)
                If oDataTable Is Nothing Then
                    MessageBox.Show("Hata : " + cErrorMessage)
                Else
                    C1TrueDBGrid1.DataSource = oDataTable
                    C1TrueDBGrid1.Refresh()
                    KomutOlustur()
                    MessageBox.Show("Kayıtlar okundu" + vbCrLf + cSQL)
                End If
            Else
                MessageBox.Show("Hata : " + cErrorMessage)
            End If

        Catch ex As Exception
            ErrDisp(ex, Me.Name)
        End Try

    End Sub

    Private Sub C1Button5_Click_1(sender As Object, e As EventArgs) Handles C1Button5.Click
        GroupBox1.Enabled = False
        EkranTemizle()
        GroupBox1.Enabled = True
    End Sub

    Private Sub C1Button1_Click_1(sender As Object, e As EventArgs) Handles C1Button1.Click
        GroupBox1.Enabled = False
        RunTransfer(2)
        GroupBox1.Enabled = True
    End Sub

    Private Sub C1Button3_Click_1(sender As Object, e As EventArgs) Handles C1Button3.Click
        GroupBox1.Enabled = False
        RunTransfer(1)
        GroupBox1.Enabled = True
    End Sub

    Private Sub C1Button4_Click_1(sender As Object, e As EventArgs) Handles C1Button4.Click
        GroupBox1.Enabled = False
        KomutYaz()
        GroupBox1.Enabled = True
    End Sub

    Private Sub C1Button2_Click_1(sender As Object, e As EventArgs) Handles C1Button2.Click
        Me.Close()
    End Sub
End Class