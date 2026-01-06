Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Threading
Imports System.Drawing
Imports System.Windows.Forms

Public Class AdvancedPicture

    Public Sub init(cFileName1 As String, Optional cFileName2 As String = "", Optional nScaleWidth As Integer = 100, Optional nScaleHeight As Integer = 100)

        Dim oImage1 As Image = Nothing
        Dim oImage2 As Image = Nothing
        Dim oImage As Image = Nothing

        If My.Computer.FileSystem.FileExists(cFileName1.Trim) Then
            oImage1 = New Bitmap(Image.FromFile(cFileName1), New Size(nScaleWidth, nScaleHeight))
        Else
            oImage1 = Nothing
        End If

        If My.Computer.FileSystem.FileExists(cFileName2.Trim) Then
            oImage2 = New Bitmap(Image.FromFile(cFileName2), New Size(nScaleWidth, nScaleHeight))
        Else
            oImage2 = Nothing
        End If

        If Not IsNothing(oImage1) And Not IsNothing(oImage2) Then
            oImage = MergeImages(oImage1, oImage2)
        ElseIf Not IsNothing(oImage1) And IsNothing(oImage2) Then
            oImage = oImage1
        ElseIf IsNothing(oImage1) And Not IsNothing(oImage2) Then
            oImage = oImage2
        End If

        PictureEdit1.Image = oImage
        Me.Show()
    End Sub

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click
        Me.Close()
    End Sub

    Private Sub AdvanvedPicture_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub SimpleButton3_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton3.Click
        Clipboard.Clear()
        Clipboard.SetImage(PictureEdit1.Image)
    End Sub
End Class