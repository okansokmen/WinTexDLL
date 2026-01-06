Option Explicit On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns

Public Class UretimisemriEdit

    Dim cIsEmriNo As String = ""
    Dim lKaydetCik As Boolean = False
    Dim oDataTable As DataTable

    Public Function init(cIsEmriNo1 As String) As Boolean

        init = False

        Try
            cIsEmriNo = cIsEmriNo1.Trim
            Me.ShowDialog()
            init = lKaydetCik

        Catch ex As Exception
            ErrDisp("init : " + ex.Message, Me.Name,,, ex)
        End Try
    End Function

    Private Sub UretimisemriEdit_Load(sender As Object, e As EventArgs) Handles Me.Load
        EnableFields()
        FillSearchLookUpEdit()
        GetData()
    End Sub

    Private Sub EnableFields()

        Try
            TextEdit1.Enabled = False
            TextEdit2.Enabled = False
            TextEdit3.Enabled = False
            TextEdit4.Enabled = False
            TextEdit5.Enabled = False
            TextEdit6.Enabled = False
            TextEdit7.Enabled = False
            TextEdit8.Enabled = False

            DateEdit3.Enabled = False

        Catch ex As Exception
            ErrDisp("EnableFields : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub FillSearchLookUpEdit()

        Try
            Dim oSQL As New SQLServerClass

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select Firma, Aciklama, Sehir, Yetkili1, SiraNo " +
                            " from firma with (NOLOCK) " +
                            " where firmatipi like 'FASONCU' " +
                            " and Firma is not null " +
                            " and Firma <> '' "

            oDataTable = New DataTable
            oDataTable = oSQL.GetDataTable("firma")

            oSQL.CloseConn()

            SearchLookUpEdit1.Properties.PopulateViewColumns()
            SearchLookUpEdit1.Properties.DataSource = oDataTable
            SearchLookUpEdit1.Properties.ValueMember = "SiraNo"
            SearchLookUpEdit1.Properties.DisplayMember = "Firma"

        Catch ex As Exception
            ErrDisp("FillSearchLookUpEdit : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub GetData()

        Try
            Dim oSQL As New SQLServerClass

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select uretimtakipno, isemrino, modelno, departman, firma, " +
                            " baslama_tar, bitis_tar, istenenadet, uretilenadet, siparisgelis, " +
                            " sevkplani, musteri, imalatci, " +
                            " firmasirano = (select top 1 sirano from firma with (NOLOCK) where firma = uretimisemriplanlama.firma ) " +
                            " from uretimisemriplanlama with (NOLOCK)  " +
                            " where isemrino = '" + cIsEmriNo.Trim + "' "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                TextEdit1.Text = oSQL.SQLReadString("isemrino")
                TextEdit2.Text = oSQL.SQLReadString("uretimtakipno")
                TextEdit3.Text = oSQL.SQLReadString("departman")
                TextEdit4.Text = oSQL.SQLReadString("modelno")
                TextEdit5.Text = oSQL.SQLReadDouble("istenenadet").ToString(G_NumberFormat)
                TextEdit6.Text = oSQL.SQLReadDouble("uretilenadet").ToString(G_NumberFormat)
                TextEdit7.Text = oSQL.SQLReadString("sevkplani")
                TextEdit8.Text = oSQL.SQLReadString("imalatci")

                SearchLookUpEdit1.EditValue = oSQL.SQLReadDouble("firmasirano")

                DateEdit1.EditValue = oSQL.SQLReadDate("baslama_tar")
                DateEdit2.EditValue = oSQL.SQLReadDate("bitis_tar")
                DateEdit3.EditValue = oSQL.SQLReadDate("siparisgelis")

            End If
            oSQL.oReader.Close()

            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp("GetData : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub SaveData()

        Try
            Dim oSQL As New SQLServerClass

            oSQL.OpenConn()

            oSQL.cSQLQuery = "update uretimisemri " +
                            " set firma = '" + SearchLookUpEdit1.Text.Trim + "' " +
                            " where isemrino = '" + TextEdit1.Text.Trim + "' "

            oSQL.SQLExecute()

            oSQL.cSQLQuery = "set dateformat dmy " +
                            " update uretimisdetayi " +
                            " set baslama_tar = '" + SQLWriteDate(DateEdit1.EditValue) + "' , " +
                            " bitis_tar = '" + SQLWriteDate(DateEdit2.EditValue) + "' " +
                            " where isemrino = '" + TextEdit1.Text.Trim + "' " +
                            " and modelno = '" + TextEdit4.Text.Trim + "' "

            oSQL.SQLExecute()

            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp("SaveData : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Function CheckFields() As Boolean

        CheckFields = False

        Try
            If SearchLookUpEdit1.Text.Trim = "" Then
                MsgBox("Atölye boş olamaz")
                Exit Function
            End If

            If DateEdit1.EditValue > DateEdit2.EditValue Then
                MsgBox("Başlangıç tarihi , bitiş tarihinden sonra olamaz")
                Exit Function
            End If

            CheckFields = True

        Catch ex As Exception
            ErrDisp("CheckFields : " + ex.Message, Me.Name,,, ex)
        End Try
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' ok
        Try
            If Not CheckFields() Then Exit Sub
            SaveData()
            lKaydetCik = True
            Me.Close()

        Catch ex As Exception
            ErrDisp("Button1_Click : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' çıkış
        Try
            lKaydetCik = False
            Me.Close()

        Catch ex As Exception
            ErrDisp("Button2_Click : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

End Class