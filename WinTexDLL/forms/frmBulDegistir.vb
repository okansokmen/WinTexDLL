Public Class frmBulDegistir

    Dim nMode As Integer

    Public Sub init(Optional nMode1 As Integer = 1)
        ' nMode = 1 , Stimulus replace
        ' nMode = 3 , DevxDashboards replace
        ' nMode = 4 , Stimulus Logo Gizle
        ' nMode = 5 , Stimulus Logo Göster
        nMode = nMode1
        Me.ShowDialog()
    End Sub

    Private Sub frmBulDegistir_Load(sender As Object, e As EventArgs) Handles Me.Load
        Select Case nMode
            Case 4
                TextBox1.Text = "<ImageBytes>"
                TextBox2.Text = "<Enabled>False</Enabled> <ImageBytes>"
            Case 5
                TextBox1.Text = "<ImageBytes>"
                TextBox2.Text = "<Enabled>True</Enabled> <ImageBytes>"
        End Select
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        On Error Resume Next
        Me.Close()
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        ' OK
        Try
            Dim oSQL As New SQLServerClass
            Dim oSQL2 As New SQLServerClass
            Dim nReportID As Integer = 0
            Dim cReport1 As String = ""
            Dim cReport2 As String = ""

            If TextBox1.Text.Trim = "" Then Exit Sub
            If TextBox2.Text.Trim = "" Then Exit Sub

            Me.Enabled = False

            oSQL2.OpenConn()
            oSQL.OpenConn()

            Select Case nMode
                Case 1, 4, 5
                    oSQL.cSQLQuery = "select reportid, report, nvreport, digerdiller " +
                                    " from stireports with (NOLOCK) " +
                                    " where report like '%" + TextBox1.Text.Trim + "%' " +
                                    " order by reportid "

                    oSQL.GetSQLReader()

                    Do While oSQL.oReader.Read
                        cReport1 = ""
                        cReport2 = ""

                        nReportID = oSQL.SQLReadInteger("reportid")

                        cReport1 = oSQL.SQLReadString("report")
                        If cReport1.Trim <> "" Then
                            cReport1 = Replace(cReport1, TextBox1.Text.Trim, TextBox2.Text.Trim)

                            oSQL2.cSQLQuery = "update stireports " +
                                                " set report = '" + cReport1 + "' " +
                                                " where reportid = " + nReportID.ToString
                            oSQL2.SQLExecute()
                        End If

                        If oSQL.SQLReadString("digerdiller").ToUpper = "E" Then
                            cReport2 = oSQL.SQLReadString("nvreport")
                            If cReport2.Trim <> "" Then
                                cReport2 = Replace(cReport2, TextBox1.Text.Trim, TextBox2.Text.Trim)

                                oSQL2.cSQLQuery = "update stireports " +
                                                    " set nvreport = '" + cReport2 + "' " +
                                                    " where reportid = " + nReportID.ToString
                                oSQL2.SQLExecute()
                            End If
                        End If

                    Loop
                    oSQL.oReader.Close()

                    MsgBox("Stimulus raporlarında " + TextBox1.Text.Trim + " bulunup " + TextBox2.Text.Trim + " ile değiştirildi")

                Case 3

                    oSQL.cSQLQuery = "select reportid, report " +
                                    " from devxdashboards with (NOLOCK) " +
                                    " where report like '%" + TextBox1.Text.Trim + "%' " +
                                    " order by reportid "

                    oSQL.GetSQLReader()

                    Do While oSQL.oReader.Read
                        cReport1 = ""
                        cReport2 = ""

                        nReportID = oSQL.SQLReadInteger("reportid")

                        cReport1 = oSQL.SQLReadString("report")
                        If cReport1.Trim <> "" Then
                            cReport1 = Replace(cReport1, TextBox1.Text.Trim, TextBox2.Text.Trim)

                            oSQL2.cSQLQuery = "update devxdashboards " +
                                                " set report = '" + cReport1 + "' " +
                                                " where reportid = " + nReportID.ToString
                            oSQL2.SQLExecute()
                        End If

                    Loop
                    oSQL.oReader.Close()

                    MsgBox("DevxDashboard raporlarında " + TextBox1.Text.Trim + " bulunup " + TextBox2.Text.Trim + " ile değiştirildi")
            End Select

            oSQL.CloseConn()
            oSQL2.CloseConn()

            Me.Enabled = True
            Me.Close()

        Catch ex As Exception
            ErrDisp("SimpleButton1_Click", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        TextBox1.Text = ""
        TextBox1.Text = ""
    End Sub

End Class