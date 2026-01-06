Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports WinForm_Test
Imports System.Threading

Namespace UHFAPP.excel
	Friend Class ExcelUtils
		''' <summary>
		'''
		''' </summary>
		''' <param name="fileName">文件路径</param>
		''' <param name="myDGV">控件DataGridView</param>
		Private Sub ExportExcels(ByVal fileName As String, ByVal myDGV As DataGridView)
			Dim saveFileName As String = ""
			Dim saveDialog As New SaveFileDialog()
			saveDialog.DefaultExt = "xls"
			saveDialog.Filter = "Excel文件|*.xls"
			saveDialog.FileName = fileName
			saveDialog.ShowDialog()
			saveFileName = saveDialog.FileName
			If saveFileName.IndexOf(":") < 0 Then
				Return '被点了取消
			End If
			Dim xlApp As New Microsoft.Office.Interop.Excel.Application()
			If xlApp Is Nothing Then
				MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel")
				Return
			End If
			Dim workbooks As Microsoft.Office.Interop.Excel.Workbooks = xlApp.Workbooks
			Dim workbook As Microsoft.Office.Interop.Excel.Workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet)
			Dim worksheet As Microsoft.Office.Interop.Excel.Worksheet = DirectCast(workbook.Worksheets(1), Microsoft.Office.Interop.Excel.Worksheet) '取得sheet1
			'写入标题
			For i As Integer = 0 To myDGV.ColumnCount - 1
				worksheet.Cells(1, i + 1) = myDGV.Columns(i).HeaderText
			Next i
			'写入数值
			For r As Integer = 0 To myDGV.Rows.Count - 1
				For i As Integer = 0 To myDGV.ColumnCount - 1
					worksheet.Cells(r + 2, i + 1) = myDGV.Rows(r).Cells(i).Value
				Next i
				System.Windows.Forms.Application.DoEvents()
			Next r
			worksheet.Columns.EntireColumn.AutoFit() '列宽自适应
			If saveFileName <> "" Then
				Try
					workbook.Saved = True
					workbook.SaveCopyAs(saveFileName)
				Catch ex As Exception
					MessageBox.Show("导出文件时出错,文件可能正被打开！" & vbLf & ex.Message)
				End Try
			End If
			xlApp.Quit()
			GC.Collect() '强行销毁
			MessageBox.Show("文件： " & fileName & ".xls 保存成功", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information)
		End Sub


		''' <summary>
		'''
		''' </summary>
		''' <param name="fileName">文件路径</param>
		''' <param name="myDGV">控件DataGridView</param>
		Public Shared Sub ExportExcels(ByVal fileName As String, ByVal lvEPC As ListView)
			If lvEPC.Items.Count = 0 Then
				MessageBox.Show("请先盘点标签!")
				Return
			End If
			Dim msg As String = "正在导出数据到文件..."
			Dim f As New frmWaitingBox(Sub(obj, args)
				Dim xlApp As New Microsoft.Office.Interop.Excel.Application()
				If xlApp Is Nothing Then
					frmWaitingBox.message_Conflict = "无法创建Excel对象，可能您的电脑未安装Excel"
					Thread.Sleep(1000)
					Return
				End If
				Dim workbooks As Microsoft.Office.Interop.Excel.Workbooks = xlApp.Workbooks
				Dim workbook As Microsoft.Office.Interop.Excel.Workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet)
				Dim worksheet As Microsoft.Office.Interop.Excel.Worksheet = DirectCast(workbook.Worksheets(1), Microsoft.Office.Interop.Excel.Worksheet) '取得sheet1
			   ' ExcelApplication1.ActiveSheet.Rows[1].numberformat:='@';

				'写入标题
				worksheet.Cells(1, 1) = "EPC"
				worksheet.Cells(1, 2) = "TID"
				worksheet.Cells(1, 3) = "USER"
				worksheet.Cells(1, 4) = "天线"
				worksheet.Cells(1, 5) = "读取次数"
				worksheet.Cells(1, 6) = "RSSI"
				'worksheet.Cells[1, 6] = "时间";



				'写入数值
				lvEPC.Invoke(New EventHandler(Sub()
					Dim len As Integer = lvEPC.Items.Count
					Dim range As Microsoft.Office.Interop.Excel.Range = worksheet.get_Range(worksheet.Cells(2, 6), worksheet.Cells(len + 1, 6))
					range.NumberFormat = "@"
					For r As Integer = 0 To len - 1
						worksheet.Cells(r + 2, 1) = vbTab & lvEPC.Items(r).SubItems("EPC").Text
						worksheet.Cells(r + 2, 2) = vbTab & lvEPC.Items(r).SubItems("TID").Text
						System.Windows.Forms.Application.DoEvents()
						frmWaitingBox.message_Conflict = "总标签数:" & len & ",已经保存:" & (r + 1)
					Next r
				End Sub))

				worksheet.Columns.EntireColumn.AutoFit() '列宽自适应



				If fileName <> "" Then
					Try

						workbook.Saved = True
						workbook.SaveCopyAs(fileName)
						workbooks.Close()
						MessageBox.Show("文件： " & fileName & "保存成功", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information)
					Catch ex As Exception
						MessageBox.Show("导出文件时出错,文件可能正被打开！" & vbLf & ex.Message)
					End Try
				End If
				xlApp.Quit()
				System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp)
				GC.Collect() '强行销毁
			End Sub, msg)
			f.ShowDialog()




		End Sub

	End Class
End Namespace
