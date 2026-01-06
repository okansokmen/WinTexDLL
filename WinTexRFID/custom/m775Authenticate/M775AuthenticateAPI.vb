Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Runtime.InteropServices

Namespace UHFAPP.custom.m775Authenticate
	Friend Class M775AuthenticateAPI
'        
'与上述方法参数一致，区别在于返回值不同，如Impinj M775返回：Challenge：6个字节，Tags Shortened TID：8个字节，Tag Response：8个字节
'

	  <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
	  Private Shared Function UHFAuthenticateCommon(ByVal password As Integer, ByVal filterBank As Byte, ByVal filterAddr As Short, ByVal filterData() As Byte, ByVal filterDataLen As Integer, ByVal keyID As Byte, ByVal tLen As Short, ByVal tData() As Byte, ByVal recvLen() As Short, ByVal recvData() As Byte) As Integer
	  End Function


	  ''' <summary>
	  '''  
	  ''' </summary>
	  ''' <param name="password"> 访问密码</param>
	  ''' <param name="filterBank">掩码的数据区(0x00 为 Reserve 0x01 为 EPC，0x02 表示 TID，0x03 表示 USR)。</param>
	  ''' <param name="filterAddr">掩码的地址</param>
	  ''' <param name="filterDataLen">掩码的长度</param>
	  ''' <param name="filterData">掩码数据</param>
	  ''' <param name="keyID">Authenticate命令用的KeyID，默认为0x00</param>
	  ''' <param name="tData">IChallenge_TAM1数据， IChallenge_TAM1数据长度,固定为10</param>
	  ''' <param name="recvLen">输出数据长度</param>
	  ''' <param name="recvData">输出数据</param>
	  ''' <returns></returns>
	  Public Function UHFAuthenticate(ByVal password As Integer, ByVal filterBank As Byte, ByVal filterAddr As Integer, ByVal filterDataLen As Integer, ByVal filterData() As Byte, ByVal keyID As Byte, ByVal tData() As Byte) As Byte()
		  Try
			  Dim recvLen(214) As Short
			  Dim recvData(1023) As Byte
			  Dim result As Integer = UHFAuthenticateCommon(password, filterBank, CShort(filterAddr), filterData, filterDataLen, keyID, CShort(tData.Length), tData, recvLen, recvData)
			  If result = 0 Then
				  Dim len As Integer = recvLen(0)
				  Dim data(len - 1) As Byte
				  Array.Copy(recvData, 0, data, 0, len)
				  Return data

			  Else
				  Return Nothing
			  End If
		  Catch ex As Exception

		  End Try
		  Return Nothing
	  End Function
	End Class
End Namespace
