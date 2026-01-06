Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Runtime.InteropServices
Imports BLEDeviceAPI
Imports UHFAPP.RFID_HF

Namespace UHFAPP.RFID
   Friend Class HF14443API


   <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
   Private Shared Function UsbOpen() As Integer
   End Function
   <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
   Private Shared Sub UsbClose()
   End Sub

'''
''' *@brief: get HF module version
''' *@param[out] pcVer:the point of HF module version
''' *@param[out] pcVerLen:the point of HF module version length
''' *@return:0->success,others->failure
''' 
   <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
   Private Shared Function HFGetVer(ByVal pcVer() As Byte, ByVal pcVerLen() As Byte) As Integer
   End Function


'''
''' *@brief: Turn on HF antenna
''' *@return:0->success,others->failure
''' 
  <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
  Private Shared Function HFTurnOnRF() As Integer
  End Function

'''
''' *@brief: Turn off HF antenna
''' *@return:0->success,others->failure
''' 
  <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
  Private Shared Function HFTurnOffRF() As Integer
  End Function

'''
''' *@brief: request 14443A card
''' *@param[in] cMode:0x26 request idle, 0x52 request all
''' *@param[out] pcCardType:card type (2 bytes)
''' *@return:0->success,others->failure
''' 
  <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
  Private Shared Function HFRequestTypeA(ByVal cMode As Integer, ByVal pcCardType() As Byte) As Integer
  End Function

'''
''' *@brief: Anticoll 14443A card
''' *@param[out] pcSnr:the point of card serial number(less than 8 bytes)
''' *@param[out] pcSnrLen:the point of card serial number length
''' *@return:0->success,others->failure
''' 
   <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
   Private Shared Function HFAnticollTypeA(ByVal pcSnr() As Byte, ByVal pcSnrLen() As Byte) As Integer
   End Function

'''
''' *@brief: Select 14443A card
''' *@param[out] SAK:the point of card type(1 byte)
''' *@return:0->success,others->failure
''' 
 <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
 Private Shared Function HFSelectTypeA(ByVal SAK() As Byte) As Integer
 End Function

'''
''' *@brief: Activate 14443A card,include request¡¢anticoll¡¢select,
''' *@param[in] cMode:0x26 request idle, 0x52 request all
''' *@param[out] pcATQA:type0~1(2bytes)+pcUid length(1bytes)+pcUid(n bytes)+type2(1bytes)
''' *@return:0->success,others->failure
''' 
  <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
  Private Shared Function HFActivateTypeA(ByVal cMode As Integer, ByVal pcATQA() As Byte) As Integer
  End Function

'''
''' *@brief: Halt 14443A card
''' *@return:0->success,others->failure
''' 
 <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
 Private Shared Function HFHaltTypeA() As Integer
 End Function

'''
''' *@brief: Authentication 14443A card
''' *@Param[in] cMode:0x60 A type key, 0x61 B type key
''' *@Param[in] cBlock:the cBlock of card,such as M1 card value 0~63
''' *@Param[in] pcKey:6 bytes key value
''' *@Return:0->success,others->failure
''' 
 <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
 Private Shared Function HFAuthentication(ByVal cMode As Byte, ByVal cBlock As Byte, ByVal pcKey() As Byte) As Integer
 End Function

'''
''' *@brief: Read 14443A card
''' *@Param[in] cBlock:the cBlock of card,such as S50 card value 0~63,S70 0~255
''' *@Param[out] bdata:16 bytes cBlock data
''' *@Return:0->success,others->failure
''' 
 <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
 Private Shared Function HFReadBlock(ByVal cBlock As Byte, ByVal bdata() As Byte) As Integer
 End Function

'''
''' *@brief: Write 14443A card
''' *@Param[in] cBlock:the cBlock of card,such as S50 card value 0~63,S70 0~255
''' *@Param[in] pcBlockData:16 bytes cBlock data
''' *@Return:0->success,others->failure
''' 
 <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
 Private Shared Function HFWriteBlock(ByVal cBlock As Byte, ByVal pcBlockData() As Byte) As Integer
 End Function

'''
''' *@brief: initial E wallet value
''' *@Param[in] cBlock:the cBlock of card,such as S50 card value 0~63,S70 0~255
''' *@Param[in] lValue:32bit
''' *@Return:0->success,others->failure
''' 
<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
Private Shared Function HFInitValue(ByVal cBlock As Byte, ByVal lValue As Long) As Integer
End Function

'''
''' *@brief: read E wallet value
''' *@Param[in] cBlock:the cBlock of card,such as S50 card value 0~63,S70 0~255
''' *@Param[out] plValue:point of 32bit value
''' *@Return:0->success,others->failure
''' 
<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
Private Shared Function HFReadValue(ByVal cBlock As Byte, ByVal plValue() As Long) As Integer
End Function

'''
''' *@brief: decrease E wallet value
''' *@Param[in] blockValue:the cBlock of value saved 
''' *@Param[in] blockResult:the cBlock of operate
''' *@Param[in] value:32bit value
''' *@Return:0->success,others->failure
''' 
<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
Private Shared Function HFDecValue(ByVal blockValue As Byte, ByVal blockResult As Byte, ByVal value As Long) As Integer
End Function

'''
''' *@brief: increase E wallet value
''' *@Param[in] blockValue:the cBlock of value saved 
''' *@Param[in] blockResult:the cBlock of operate
''' *@Param[in] value:32bit value
''' *@Return:0->success,others->failure
''' 
<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
Private Shared Function HFIncValue(ByVal blockValue As Byte, ByVal blockResult As Byte, ByVal value As Long) As Integer
End Function

'''
''' *@brief: Restore E wallet value
''' *@Param[in] cBlock:the cBlock address
''' *@Return:0->success,others->failure
''' 
<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
Private Shared Function HFRestore(ByVal cBlock As Byte) As Integer
End Function

'''
''' *@brief: Transfer E wallet value
''' *@Param[in] cBlock:the cBlock address
''' *@Return:0->success,others->failure
''' 
<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
Private Shared Function HFTransfer(ByVal cBlock As Byte) As Integer
End Function

'''
''' *@brief: aticoll ul card
''' *@Param[out] pcSnr:the point of card serial number
''' *@Param[out] pcSnrLen:the point of card serial number length
''' *@Return:0->success,others->failure
''' 
 <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
 Private Shared Function HFUlAnticoll(ByVal pcSnr() As Byte, ByVal pcSnrLen() As Byte) As Integer
 End Function

'''
''' *@brief: write ul card
''' *@Param[in] cBlock:the address of card
''' *@Param[in] pcWriteData:the point of write data
''' *@Param[in] cWriteLen:the length of write  data
''' *@Return:0->success,others->failure
''' 
  <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
  Private Shared Function HFUlWrite(ByVal cBlock As Byte, ByVal pcWriteData() As Byte, ByVal cWriteLen As Byte) As Integer
  End Function

'''
''' *@brief: reset cpu card
''' *@Param[out] cardType:the point of card type
''' *@Param[out] pcUid:the point of write data
''' *@Param[out] cUidLen:the point of pcUid length
''' *@Param[out] pcATR:the point of card reset data
''' *@Param[out] pcATRLen:the length of cpu card atq data
''' *@Return:0->success,others->failure
''' 
 <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
 Private Shared Function HFResetTypeA(ByVal cardType() As Byte, ByVal pcUid() As Byte, ByVal cUidLen() As Byte, ByVal pcATR() As Byte, ByVal pcATRLen() As Byte) As Integer
 End Function

'''
''' *@brief: rats typeA cpu card
''' *@Param[out] pcATR:the point of card reset data
''' *@Param[out] pcInfoLen:the length of cpu card atq data
''' *@Return:0->success,others->failure
''' 
 <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
 Private Shared Function HFRatsTypeA(ByVal pcATR() As Byte, ByVal pcATRLen() As Byte) As Integer
 End Function

'''
''' *@brief: Halt 14443A card
''' *@return:0->success,others->failure
''' 
 <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
 Private Shared Function HFHaltTypeB() As Integer
 End Function

'''
''' *@brief: reset type B card
''' *@Param[out] pcInfo:the point of receive command
''' *@Param[out] pcInfoLen:the length of received
''' *@Return:0->success,others->failure
''' 
 <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
 Private Shared Function HFResetTypeB(ByVal pcInfo() As Byte, ByVal pcInfoLen() As Byte) As Integer
 End Function


'''
''' *@brief: get pcUid of type B card
''' *@Param[out] pcUid:the point of type B card pcUid
''' *@Param[out] cUidLen:the length of pcUid
''' *@Return:0->success,others->failure
''' 
 <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
 Private Shared Function HFGetUidTypeB(ByVal pcUid() As Byte, ByVal cUidLen() As Byte) As Integer
 End Function

'''
''' *@brief: execute cpu command
''' *@Param[in] pcInCos:the point of send command
''' *@Param[in] cInLen:the length of send command
''' *@Param[out] pcOutCos:the point of receive command
''' *@Param[out] pcOutLen:the length of received
''' *@Return:0->success,others->failure
''' 
 <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
 Private Shared Function HFCpuCommand(ByVal pcInCos() As Byte, ByVal cInLen As Byte, ByVal pcOutCos() As Byte, ByVal pcOutLen() As Byte) As Integer
 End Function





   Private Const SUCCESS As Integer = 0
   #Region "usb"
	  Public Function OpenUsb() As Boolean
		 Dim result As Integer = UsbOpen()
		 If result = SUCCESS Then
			 Return True
		 End If
		 Return False
	  End Function
	  Public Sub CloseUsb()
		  UsbClose()
	  End Sub

	  ''' <summary>
	  ''' 获取版本号
	  ''' </summary>
	  ''' <returns></returns>
	  Public Function GetRFIDVerion() As String
		  Dim version(99) As Byte
		  Dim outLen(0) As Byte
		  Dim result As Integer = HFGetVer(version, outLen)
		  If result <> SUCCESS Then
			  Return Nothing
		  End If
		  Return System.Text.ASCIIEncoding.ASCII.GetString(version, 0, outLen(0))
	  End Function
	  ''' <summary>
	  ''' Turn on HF antenna
	 ''' </summary>
	  ''' <returns></returns>
	  Public Function TurnOn() As Boolean
		  Return HFTurnOnRF() = SUCCESS
	  End Function
	   ''' <summary>
	  ''' Turn off HF antenna
	   ''' </summary>
	   ''' <returns></returns>
	  Public Function TurnOff() As Boolean
		  Return HFTurnOffRF() = SUCCESS
	  End Function

	   ''' <summary>
	   ''' 寻卡
	   ''' </summary>
	  ''' <param name="mode">0x26 request idle, 0x52 request all</param>
	  ''' <returns>pcCardType:card type (2 bytes)</returns>
	  Public Function RequestTypeA(ByVal mode As Integer) As Byte()
		   Dim pcCardType(1) As Byte
		   Dim result As Integer= HFRequestTypeA(mode,pcCardType)
		   If result <> SUCCESS Then
			   Return Nothing
		   End If
		   Return pcCardType
	  End Function

	  ''' <summary>
	  ''' Anticoll 14443A card
	  ''' </summary>
	  ''' <returns>卡号ID</returns>
	  Public Function AnticollTypeA() As Byte()
		  Dim pcSnr(99) As Byte
		  Dim pcSnrLen(0) As Byte
		  Dim result As Integer = HFAnticollTypeA(pcSnr, pcSnrLen)
		  If result <> SUCCESS Then
			  Return Nothing
		  End If
			Return BLEDeviceAPI.Utils.CopyArray(pcSnr, pcSnrLen(0))
		End Function

	   ''' <summary>
	   ''' 获取卡片类型
	   ''' </summary>
	   ''' <returns></returns>
	  Public Function SelectTypeA() As Integer
		  Dim SAK(0) As Byte
		  Dim result As Integer = HFSelectTypeA(SAK)
		  If result <> SUCCESS Then
			  Return -1
		  End If
		  Return SAK(0)
	  End Function


	  ''' <summary>
	  '''  认证
	  ''' </summary>
	  ''' <param name="cMode">cMode:0x60 A type key, 0x61 B type key</param>
	  ''' <param name="cBlock">cBlock:the cBlock of card,such as M1 card value 0~63</param>
	  ''' <param name="pcKey">pcKey:6 bytes key value</param>
	  ''' <returns>success,others->failure</returns>

	  Public Function Authentication(ByVal cMode As Byte, ByVal cBlock As Byte, ByVal pcKey() As Byte) As Boolean
		  Return HFAuthentication(cMode, cBlock, pcKey)=0
	  End Function
	   ''' <summary>
	   ''' 读卡
	   ''' </summary>
	  ''' <param name="cBlock">the cBlock of card,such as S50 card value 0~63,S70 0~255</param>
	  ''' <returns>16 bytes cBlock data</returns>
	  Public Function ReadBlock(ByVal cBlock As Byte) As Byte()
		  Dim buff(15) As Byte
		  Dim result As Integer = HFReadBlock(cBlock, buff)
		  If result <> SUCCESS Then
			  Return Nothing
		  End If
		  Return buff
	  End Function
	   ''' <summary>
	   ''' 写卡
	   ''' </summary>
	   ''' <param name="cBlock"></param>
	   ''' <param name="pcBlockData"></param>
	   ''' <returns></returns>
	  Public Function WriteBlock(ByVal cBlock As Byte, ByVal pcBlockData() As Byte) As Boolean

		  Return HFWriteBlock(cBlock, pcBlockData) = SUCCESS
	  End Function

	   ''' <summary>
	  ''' rats typeA cpu card
	   ''' </summary>
	   ''' <param name="pcATR"></param>
	   ''' <param name="pcATRLen"></param>
	  Public Function RatsTypeA() As Byte()
		  Dim pcATR(511) As Byte
		  Dim pcATRLen(0) As Byte

		  Dim result As Integer = HFRatsTypeA(pcATR, pcATRLen)
		  If result <> SUCCESS Then
			  Return Nothing
		  End If

			Return BLEDeviceAPI.Utils.CopyArray(pcATR, pcATRLen(0))
		End Function

	   ''' <summary>
	  ''' execute cpu command
	   ''' </summary>
	   ''' <param name="cmd"></param>
	   ''' <returns></returns>
	  Public Function CpuCommand(ByVal cmd() As Byte) As Byte()
		  Dim pcOutCos(511) As Byte
		  Dim pcOutLen(0) As Byte
		  Dim result As Integer = HFCpuCommand(cmd, CByte(cmd.Length), pcOutCos, pcOutLen)
		  If result <> SUCCESS Then
			  Return Nothing
		  End If
			Return BLEDeviceAPI.Utils.CopyArray(pcOutCos, pcOutLen(0))
		End Function

	   ''' <summary>
	  ''' get pcUid of type B card
	   ''' </summary>
	   ''' <returns></returns>
	  Public Function GetUidTypeB() As Byte()
		  Dim pcUid(511) As Byte
		  Dim cUidLen(0) As Byte
		  Dim result As Integer = HFGetUidTypeB(pcUid, cUidLen)
		  If result <> SUCCESS Then
			  Return Nothing
		  End If
			Return BLEDeviceAPI.Utils.CopyArray(pcUid, cUidLen(0))
		End Function
	  Public Function ResetTypeB() As Byte()

		  Dim pcInfo(99) As Byte
		  Dim pcInfoLen(9) As Byte

		  Dim result As Integer = HFResetTypeB(pcInfo, pcInfoLen)
		  If result <> SUCCESS Then
			  Return Nothing
		  End If
			Return BLEDeviceAPI.Utils.CopyArray(pcInfo, pcInfoLen(0))
		End Function

   #End Region



   End Class
End Namespace
