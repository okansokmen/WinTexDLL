Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text


Namespace BLEDeviceAPI
   Public Class Utils
		''' <summary>
		''' 复制数组
		''' </summary>
		''' <typeparam name="T">泛型数组类型</typeparam>
		''' <param name="sourceArray">原数组</param>
		''' <param name="copyLen">要复制的数组长度，也就是新数组的长度</param>
		''' <returns>返回新数组</returns>
		Public Shared Function CopyArray(Of T)(ByVal sourceArray() As T, ByVal copyLen As Integer) As T()
			Dim outData(copyLen - 1) As T
			Array.Copy(sourceArray, outData, copyLen)
			Return outData
		End Function
		''' <summary>
		''' 复制数组
		''' </summary>
		''' <typeparam name="T">泛型数组类型</typeparam>
		''' <param name="sourceArray">原数组</param>
		''' <param name="copyLen">要复制的数组长度，也就是新数组的长度</param>
		''' <returns>返回新数组</returns>
		Public Shared Function CopyArray(Of T)(ByVal sourceArray() As T, ByVal start As Integer, ByVal copyLen As Integer) As T()
			Dim outData(copyLen - 1) As T
			Array.Copy(sourceArray, start,outData, 0,copyLen)
			Return outData
		End Function
   End Class
End Namespace
