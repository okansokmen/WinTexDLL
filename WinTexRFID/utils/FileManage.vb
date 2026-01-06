Imports System
Imports System.Linq
Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports System.Drawing
Imports BLEDeviceAPI
Imports System.Drawing.Imaging


Namespace UHFAPP
	Public Class FileManage

		Private Shared path As String = Environment.CurrentDirectory & "\log.txt"
		''' <summary>
		''' 写入数据
		''' </summary>
		''' <param name="path">文件路径</param>
		''' <param name="data">数据</param>
		''' <param name="appdend">是否将数据追加到文件末尾</param>
		Public Shared Sub WriterFile(ByVal path As String, ByVal data As String, ByVal appdend As Boolean)
			Try
				Dim sw As New StreamWriter(path, appdend)
				sw.Write(data)

				sw.Close()
			Catch ex As System.Exception

			End Try
		End Sub
		Public Shared Function ReadFileToBytes(ByVal path As String) As Byte()

			Dim fsr As New FileStream(path, FileMode.Open)
			'开辟内存区域 1024 * 1024 bytes
			Dim readBytes((1024 * 1024) - 1) As Byte
			'开始读数据
			Dim count As Integer = fsr.Read(readBytes, 0, readBytes.Length)
			'关闭文件流
			fsr.Close()

			Dim byteData(count - 1) As Byte
			Array.Copy(readBytes, 0, byteData, 0, count)
			Return byteData

		End Function

		Public Shared Function ReadFile(ByVal path As String) As String
			Dim data As String = ""
			Dim sr As New StreamReader(path, Encoding.Default)
			data = sr.ReadToEnd()
			sr.Close()
			Return data
		End Function


		Public Shared Function ReadFileBmp(ByVal imgPath As String) As String
			Try

				Dim bmp As Bitmap = DirectCast(Image.FromFile(imgPath).Clone(), Bitmap)
				Dim r As New Rectangle(0, 0, bmp.Width, bmp.Height)
				bmp = bmp.Clone(r, PixelFormat.Format4bppIndexed)

				bmp.RotateFlip(RotateFlipType.Rotate90FlipX)

				r = New Rectangle(0, 0, bmp.Width, bmp.Height)
				bmp = bmp.Clone(r, PixelFormat.Format1bppIndexed)
				bmp.Save("C:\info.bmp", ImageFormat.Bmp)
				Dim data() As Byte = ReadFileToBytes("C:\info.bmp") '128


				If data Is Nothing OrElse data.Length = 0 Then
					Return ""
				End If
				If data.Length > 2368 * 2 Then
					data = BLEDeviceAPI.Utils.CopyArray(data, data.Length - 2368 * 2, 2368 * 2)
				End If
				Return DataConvert.ByteArrayToHexString(data)
			Catch ex As Exception

			End Try
			Return Nothing

		End Function
		''' <summary>
		''' 记录APP日志
		''' </summary>
		''' <param name="log"></param>
		Public Shared Sub WriterLog(ByVal type As LogType, ByVal log As String)
			WriterFile(path, log,True)

		End Sub


		Public Enum LogType
		   [Error]
		   Debug

		End Enum





	End Class
End Namespace
