Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms

Namespace UHFAPP.custom
	Partial Public Class WriteEPCSimpleDemo
		Inherits Form

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub btnRead_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRead.Click
			If Not Me.btnRead.IsHandleCreated Then Return

			Dim uAccessPwd(7) As Byte
			Dim FilterBank As Byte = 1
			Dim FilterStartaddr As Integer = 0
			Dim FilterLen As Integer = 0
			Dim FilterData(1) As Byte
			Dim uBank As Byte = 1
			Dim uPtr As Integer = 2
			Dim uCnt As Integer = 2

		  Dim data As String= UHFAPI.getInstance().ReadData(uAccessPwd, FilterBank, FilterStartaddr, FilterLen, FilterData, uBank, uPtr, uCnt)
		  txtDataR.Text = ""
		  label3.Text = "读取失败"
		  label3.ForeColor = Color.Red
		  If data Is Nothing OrElse data.Length = 0 Then
			  Return
		  End If
		  label3.Text = "读取成功！"
		  label3.ForeColor = Color.Green
		  txtDataR.Text = data.Replace(" ","")

		End Sub
		Private z As String = "000000000000000000000000000000000000000000000000000000000000000000000000"
		Private Sub btnWrite_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnWrite.Click
			If Not Me.btnWrite.IsHandleCreated Then Return

			Dim accessPwd(3) As Byte
			Dim filterBank As Byte = 1
			Dim filterPtr As Integer = 0
			Dim filterCnt As Integer = 0
			Dim filterData(0) As Byte
			Dim text As String = txtDataW.Text.Replace(" ","")
			label3.ForeColor = Color.Red
			If text.Length = 0 Then
				label3.Text = "录入的数据不能为空!"
				Return
			End If
			If text.Length < 8 Then
				text = z.Substring(0, 8 - text.Length) & text
			End If
			Dim writeData() As Byte=DataConvert.HexStringToByteArray(text)
			label3.Text = "录入失败"

			Dim result As Boolean = UHFAPI.getInstance().writeDataToEpc(accessPwd, filterBank, filterPtr, filterCnt, filterData, writeData)
			If result Then
				label3.ForeColor = Color.Green
				label3.Text = "录入成功"
			End If
		End Sub

		Private Sub WriteEPCSimpleDemo_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
			Dim result As Boolean = UHFAPI.getInstance().OpenUsb()
			If Not result Then
				MessageBox.Show("找不到设备!")
				Close()
			End If
		End Sub

		Private Sub WriteEPCSimpleDemo_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
			UHFAPI.getInstance().CloseUsb()
		End Sub
	End Class
End Namespace
