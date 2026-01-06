Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Threading

Namespace UHFAPP
	Partial Public Class WaitForm
		Inherits Form

		Private Shared loadingForm As WaitForm = Nothing
		Public Sub New()
			InitializeComponent()

		End Sub

		''' <summary>
		''' 显示加载
		''' </summary>
		Public Shared Sub ShowLoading()
			Try
				Dim loadingTh As New Thread(New ThreadStart(Sub()
					loadingForm = New WaitForm()
					loadingForm.StartPosition = FormStartPosition.CenterParent
					Application.Run(loadingForm)
					loadingForm.Dispose()
					loadingForm = Nothing
					loadingTh = Nothing
				End Sub))
				loadingTh.IsBackground = True
				loadingTh.Priority = ThreadPriority.Highest
				loadingTh.Start()
			Catch
			End Try
		End Sub
		''' <summary>
		''' 隐藏加载
		''' </summary>
		Public Shared Sub HideLoading()
			Try
				If loadingForm IsNot Nothing Then
					loadingForm.Invoke(New EventHandler(Sub()
						loadingForm.Close()
					End Sub))
				End If
			Catch
			End Try
		End Sub
	End Class


End Namespace
