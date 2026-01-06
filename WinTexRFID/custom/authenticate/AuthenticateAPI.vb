Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Runtime.InteropServices

Namespace UHFAPP.custom.authenticate
	Friend Class AuthenticateAPI
'''        ********************************************************************************************************
'''        * 功能：验证标签
'''        * 输入： 
'''        password -- 访问密码
'''        bank -- 掩码的数据区(0x00 为 Reserve 0x01 为 EPC，0x02 表示 TID，0x03 表示 USR)。
'''        addr -- 掩码的地址
'''        mDataLen -- 掩码的长度
'''        mData -- 掩码数据
'''        keyID -- Authenticate命令用的KeyID，默认为0x00
'''        tLen -- IChallenge_TAM1数据长度,固定为10
'''        tData -- IChallenge_TAM1数据
'''        *输出
'''        recvLen -- 输出数据长度
'''        recvData -- 输出数据
'''        返回值：0：执行成功    1：发送失败
'''        * 
'''        ********************************************************************************************************

		 <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		 Private Shared Function UHFAuthenticate(ByVal password As Integer, ByVal filterBank As Byte, ByVal filterAddr As Short, ByVal filterData() As Byte, ByVal filterDataLen As Integer, ByVal keyID As Byte, ByVal tLen As Short, ByVal tData() As Byte, ByVal recvLen() As Short, ByVal recvData() As Byte) As Integer
		 End Function





		'function:AES encrypto or decrypto data
		'in
		'isEnc -- 1 encrypto  0,decrypto
		'keylen == shoulde be 16 or  24 or 32
		'in-out inbuf -- in date 
		'inlen -- the length of input bytes,must be N*16
		'return -1--key length error, others -- the length of inbuf return
		 <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		 Private Shared Function AESHandle(ByVal isEnc As Byte, ByVal key() As Byte, ByVal keylen As Integer, ByVal inbuf() As Byte, ByVal inLen As Long) As Integer
		 End Function



		 ''' <summary>
		 ''' 加密
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
				 Dim result As Integer = UHFAuthenticate(password, filterBank, CShort(filterAddr), filterData, filterDataLen, keyID, CShort(tData.Length), tData, recvLen, recvData)
				 If result = 0 Then
					 Dim len As Integer = recvLen(0)
					  Dim data(len - 1) As Byte
					 Array.Copy(recvData, 0, data,0,len)
					 Return data

				 Else
					 Return Nothing
				 End If
			 Catch ex As Exception

			 End Try
			 Return Nothing
		 End Function

		 Public Function AesDecrypto(ByVal key() As Byte, ByVal data() As Byte) As Byte()
			 Dim result As Integer = AESHandle(0, key, 16, data, data.Length)
			 If result = -1 Then
				 Return Nothing
			 End If
			 Return data
		 End Function


	End Class
End Namespace
