Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Public Class frmAbit

    Public Sub init(nCase As Integer)

        Me.Show()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' ithalat
        ReadBeyannameler("T")
        WriteBeyannameler("T")
        ' ihracat
        ReadBeyannameler("H")
        WriteBeyannameler("H")
    End Sub


    Private Sub mbox(cMessage As String)
        TextBox1.Text = Now.ToString + " - > " + cMessage + vbCrLf + TextBox1.Text
        TextBox1.Refresh()
    End Sub

    Private Sub frmAbit_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class