Option Explicit On
Option Strict On

Imports Stimulsoft.Report
Imports Stimulsoft.Report.Dictionary
Imports Stimulsoft.Report.Print

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.IO
Imports System.Reflection
Imports System.Xml
Imports System.Windows.Forms

Imports Microsoft.SqlServer.Server

Public Class frmGetDevXReport

    Dim cRepCls As String = ""
    Public cResult As String = ""

    Public Sub init(cReportClass As String)
        cRepCls = cReportClass.Trim
        cResult = ""
        Me.ShowDialog()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        ' ok
        cResult = ListBox1.SelectedItem.ToString.Trim
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        ' Iptal
        cResult = ""
        Me.Close()
    End Sub

    Private Sub frmGetDevXReport_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If ListBox1.Items.Count = 0 Then
            cResult = ""
        End If
    End Sub

    Private Sub frmGetDevXReport_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ' fill
        Dim ConnYage As SqlConnection
        Dim odr As SqlDataReader
        Dim cSQL As String = ""

        ListBox1.Items.Clear()

        ConnYage = OpenConn()

        cSQL = "select reportname from devxreports where reportclass = '" + cRepCls.Trim + "' "

        odr = GetSQLReader(cSQL, ConnYage)

        Do While odr.Read
            ListBox1.Items.Add(SQLReadString(odr, "reportname"))
        Loop
        odr.Close()
        odr = Nothing
        CloseConn(ConnYage)
    End Sub
End Class