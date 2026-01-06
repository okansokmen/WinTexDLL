Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Public Class frmStatus
    Public Sub init()
        Me.WindowState = FormWindowState.Maximized
        Me.Show()
    End Sub
    Public Sub ShowMessage(cMessage As String)
        TextBox1.Text = Now.ToString + " - > " + cMessage + vbCrLf + TextBox1.Text
        Me.Refresh()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub frmStatus_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class