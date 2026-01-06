'Imports Microsoft.VisualBasic
'Imports System
'Imports System.Drawing
'Imports System.Collections
'Imports System.ComponentModel
'Imports System.Windows.Forms
'Imports System.Data
'Imports System.Data.SqlClient

Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.ComponentModel

Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Sql
Imports DevExpress.XtraEditors.Repository

Public Class frmSipPictures

    Dim oSQL As SQLServerClass
    Dim cSiparisNo As String

    Public Sub init(Optional cSiparisNo1 As String = "")
        cSiparisNo = cSiparisNo1.Trim
        oSQL = New SQLServerClass
        oSQL.OpenConn()
        Me.ShowDialog()
    End Sub

    Private Sub frmSipPictures_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try
            RepositoryItemPictureEdit1.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
            GetData()

        Catch ex As Exception
            ErrDisp("frmSipPictures_Load : " + ex.Message, Me.Name, "",, ex)
        End Try
    End Sub

    Private Sub GetData()

        Try

            oSQL.cSQLQuery = "select konu, notlar, resim, siparisno " +
                             " from sipresimler with (NOLOCK) " +
                             " where siparisno = '" + cSiparisNo.Trim + "' "

            oSQL.FillDataTable()
            GridControl1.DataSource = oSQL.oDataTable

        Catch ex As Exception
            ErrDisp("GetData : " + ex.Message, Me.Name, "",, ex)
        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' çıkış
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' sakla
        Try
            Dim oDataRow As DataRow
            Dim cSRLineNo As String

            oSQL.cSQLQuery = "delete sipresimler " +
                             " where siparisno = '" + cSiparisNo.Trim + "' "
            oSQL.SQLExecute()

            For Each oDataRow In oSQL.oDataTable.Rows

                cSRLineNo = oSQL.GetSequenceFisNo("srlineno")

                oSQL.cSQLQuery = "insert sipresimler (siparisno, konu, notlar, srlineno) " +
                                 " values ('" + SQLWriteString(cSiparisNo, 30) + "' , " +
                                 " '" + SQLWriteString(DataTableReadString(oDataRow.Item(0)), 200) + "' , " +
                                 " '" + SQLWriteString(DataTableReadString(oDataRow.Item(1))) + "' , " +
                                 " '" + cSRLineNo + "' ) "

                oSQL.SQLExecute()

                If IsDBNull(oDataRow.Item(2)) Then
                    ' nothing
                ElseIf IsNothing(oDataRow.Item(2)) Then
                    ' nothing
                Else
                    DataTable(oDataRow.Item(2), cSRLineNo)
                End If
            Next

            Me.Close()

        Catch ex As Exception
            ErrDisp("Button1_Click : " + ex.Message, Me.Name, "",, ex)
        End Try
    End Sub

    Private Sub frmSipPictures_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        oSQL.CloseConn()
    End Sub

    Private Sub DataTable(aData() As Byte, cSRLineNo As String)

        Try
            Dim oSQLCommand As New SqlCommand

            oSQLCommand.Connection = oSQL.oConnection
            oSQLCommand.CommandText = "update sipresimler " +
                                      " set resim = @IM " +
                                      " where siparisno = '" + cSiparisNo.Trim + "' " +
                                      " and srlineno = '" + cSRLineNo.Trim + "' "

            oSQLCommand.Parameters.AddWithValue("@IM", aData)
            oSQLCommand.ExecuteNonQuery()

        Catch ex As Exception
            ErrDisp("DataTable : " + ex.Message, Me.Name, "",, ex)
        End Try
    End Sub

    Private Function DataTableReadString(oData As Object) As String

        DataTableReadString = ""

        Try
            If IsDBNull(oData) Then
                ' do nothing
            ElseIf IsNothing(oData) Then
                ' do nothing
            Else
                DataTableReadString = oData.ToString.Trim
            End If

        Catch ex As Exception
            ErrDisp("DataTableReadString : " + ex.Message, Me.Name, "",, ex)
        End Try
    End Function

End Class