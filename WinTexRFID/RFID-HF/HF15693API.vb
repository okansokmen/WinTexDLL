Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Runtime.InteropServices
Imports BLEDeviceAPI


Namespace UHFAPP.RFID_HF
	Friend Class HF15693API
'''        
'''         *@brief: 15693 inventory
'''         *@Param[in] cMode:inventory cMode,0~3
'''         *@Param[in] AFI:AFI value
'''         *@Param[out] pcData:the point of receive data
'''         *@Param[out] dataLen:the length of received
'''         *@Return:0->success,others->failure
'''         
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function HF15693Inventory(ByVal cMode As Byte, ByVal AFI As Byte, ByVal pcData() As Byte, ByVal dataLen() As Byte) As Integer
		End Function
'''        
'''         *@brief: ISO15693 Get card system information 
'''         *@Param[in] cMode:less than 10
'''         *@Param[in] pcUid:the point of pcUid
'''         *@Param[in] cUidLen:the length of pcUid
'''         *@Param[out] pcInfo:the point of system infoemation 
'''         *@Param[out] pcInfoLen:the point of system information length
'''         *@Return:0->success,others->failure
'''         
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function HF15693GetSystemInfo(ByVal cMode As Byte, ByVal pcUid() As Byte, ByVal cUidLen As Byte, ByVal pcInfo() As Byte, ByVal pcInfoLen() As Byte) As Integer
		End Function

'''        
'''         *@brief: ISO15693 read
'''         *@Param[in] cMode:less than 10
'''         *@Param[in] pcUid:the point of pcUid
'''         *@Param[in] cUidLen:the length of pcUid
'''         *@Param[in] iStartBlock:start address of cBlock
'''         *@Param[in] cBlockNum:cBlock number
'''         *@Param[out] pData:the point of receive data
'''         *@Param[out] pDataLen:the length of received data
'''         *@Return:0->success,others->failure
'''         
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function HF15693Read(ByVal cMode As Byte, ByVal pcUid() As Byte, ByVal cUidLen As Integer, ByVal iStartBlock As Integer, ByVal cBlockNum As Integer, ByVal pData() As Byte, ByVal pDataLen() As Byte) As Integer
		End Function


'''        
'''         *@brief: ISO15693 Write
'''         *@Param[in] cMode:less than 10
'''         *@Param[in] pcUid:the point of pcUid
'''         *@Param[in] cUidLen:the length of pcUid
'''         *@Param[in] iStartBlock:address cBlock start
'''         *@Param[in] cBlockNum:cBlock number
'''         *@Param[in] pwData:the point of write data
'''         *@Param[in] wLen:the length of write data
'''         *@Return:0->success,others->failure
'''         
		 <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		 Private Shared Function HF15693Write(ByVal cMode As Byte, ByVal pcUid() As Byte, ByVal cUidLen As Integer, ByVal iStartBlock As Integer, ByVal cBlockNum As Integer, ByVal pwData() As Byte, ByVal wLen As Byte) As Integer
		 End Function


'''         
'''   *@brief: ISO15693 stay quite
'''   *@Return:0->success,others->failure
'''   
		 <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		 Private Shared Function HF15693StayQuite() As Integer
		 End Function


'''         
'''          *@brief: ISO15693 Lock cBlock
'''          *@Param[in] cMode:less than 10
'''          *@Param[in] pcUid:the point of pcUid
'''          *@Param[in] cUidLen:the length of pcUid
'''          *@Param[in] iStartBlock:address cBlock start
'''          *@Param[in] cBlockNum:cBlock number
'''          *@Return:0->success,others->failure
'''          
		 <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		 Private Shared Function HF15693LockBlock(ByVal cMode As Byte, ByVal pcUid() As Byte, ByVal cUidLen As Byte, ByVal iStartBlock As Integer, ByVal cBlockNum As Byte) As Integer
		 End Function

'''         
'''          *@brief: ISO15693 select card
'''          *@Param[out] pcInfo:the point of card information 
'''          *@Param[out] pcInfoLen:the point of card information length
'''          *@Return:0->success,others->failure
'''          
		 <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		 Private Shared Function HF15693Select(ByVal pcInfo() As Byte, ByVal pcInfoLen() As Byte) As Integer
		 End Function

'''         
'''          *@brief: ISO15693 reset to ready
'''          *@Param[out] pcInfo:the point of card information 
'''          *@Param[out] pcInfoLen:the point of card information length
'''          *@Return:0->success,others->failure
'''          
		 <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		 Private Shared Function HF15693ResetReady(ByVal pcInfo() As Byte, ByVal pcInfoLen() As Byte) As Integer
		 End Function

'''         
'''  *@brief: ISO15693 Write AFI
'''  *@Param[in] cMode:less than 10
'''  *@Param[in] pcUid:the point of pcUid
'''  *@Param[in] cUidLen:the length of pcUid
'''  *@Param[in] cAFI:AFI value
'''  *@Return:0->success,others->failure
'''  
		 <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		 Private Shared Function HF15693WriteAFI(ByVal cMode As Byte, ByVal pcUid() As Byte, ByVal cUidLen As Integer, ByVal cAFI As Byte) As Integer
		 End Function

'''         
'''          *@brief: ISO15693 Lock AFI
'''          *@Param[in] cMode:less than 10
'''          *@Param[in] pcUid:the point of pcUid
'''          *@Param[in] cUidLen:the length of pcUid
'''          *@Return:0->success,others->failure
'''          
		 <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		 Private Shared Function HF15693LockAFI(ByVal cMode As Byte, ByVal pcUid() As Byte, ByVal cUidLen As Byte) As Integer
		 End Function

'''        
''' *@brief: ISO15693 write DSFID
''' *@Param[in] cMode:less than 10
''' *@Param[in] pcUid:the point of pcUid
''' *@Param[in] cUidLen:the length of pcUid
''' *@Param[in] cDSFID:DSFID value
''' *@Return:0->success,others->failure
''' 
 <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
 Private Shared Function HF15693WriteDsfid(ByVal cMode As Byte, ByVal pcUid() As Byte, ByVal cUidLen As Byte, ByVal cDSFID As Byte) As Integer
 End Function

'''
''' *@brief: ISO15693 Lock DSFID
''' *@Param[in] cMode:less than 10
''' *@Param[in] pcUid:the point of pcUid
''' *@Param[in] cUidLen:the length of pcUid
''' *@Return:0->success,others->failure
''' 
 <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
 Private Shared Function HF15693LockDSFID(ByVal cMode As Byte, ByVal pcUid() As Byte, ByVal cUidLen As Byte) As Integer
 End Function

'''
''' *@brief: ISO15693 get multiple blocks
''' *@Param[in] cMode:less than 10
''' *@Param[in] pcUid:the point of pcUid
''' *@Param[in] cUidLen:the length of pcUid
''' *@Param[in] iStartBlock:address cBlock start
''' *@Param[out] cBlockNum:cBlock number
''' *@Param[out] pcData:the point of receive data
''' *@Return:0->success,others->failure
''' 
 <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
 Private Shared Function HF15693GetMultipleBlock(ByVal cMode As Byte, ByVal pcUid() As Byte, ByVal cUidLen As Byte, ByVal iStartBlock As Integer, ByVal cBlockNum As Byte, ByVal pcData() As Byte, ByVal pcDataLen() As Byte) As Integer
 End Function

'''
''' *@brief: ISO15693 transfer command
''' *@Param[in] pcInCmd:the point of command data 
''' *@Param[in] cLen:command length
''' *@Param[out] pcOutCmd:the point of out data
''' *@Param[out] pcOutLen:the point of out data length
''' *@Return:0->success,others->failure
''' 
 <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
 Private Shared Function HF15693TransferCmd(ByVal pcInCmd() As Byte, ByVal cLen As Byte, ByVal pcOutCmd() As Byte, ByVal pcOutLen() As Byte) As Integer
 End Function


		Private Const SUCCESS As Integer = 0


		''' <summary>
		''' 寻卡
		''' </summary>
		Public Function Inventory() As ISO15693Entity
			Dim cMode As Byte = 1
			Dim AFI As Byte = 0

			Dim pcData(511) As Byte
			Dim dataLen(9) As Byte
			If HF15693Inventory(cMode, AFI, pcData, dataLen) <> SUCCESS Then
				Return Nothing
			End If
			pcData = BLEDeviceAPI.Utils.CopyArray(pcData, dataLen(0))
			' 声明uid数组
			Dim cUID(7) As Byte
			For i As Integer = 0 To 7
				cUID(i) = pcData(i + 2)
			Next i

			Dim entity As New ISO15693Entity()

			Dim type As ISO15693Entity.TagType = ISO15693Entity.TagType.UNKNOWN

			If pcData.Length > 8 Then
				Select Case pcData(8)
					Case 4
						type = ISO15693Entity.TagType.ICODE2
					Case 7
						type = ISO15693Entity.TagType.TI2048
					Case 2
						type = ISO15693Entity.TagType.STLRIS64K
					Case 22 ' 0x16 EM4033
						type = ISO15693Entity.TagType.EM4033

					Case Else
				End Select
			End If

			entity.Type = type
			entity.Uid = cUID

			If entity.Type = ISO15693Entity.TagType.EM4033 Then
				Return entity
			End If
			Dim cInfo() As Byte
			If entity.Type = ISO15693Entity.TagType.STLRIS64K Then
				cInfo = GetSystemInfo(8, Nothing)
			Else
				cInfo = GetSystemInfo(0, Nothing)
			End If

			' 获取信息成功
			If cInfo IsNot Nothing AndAlso cInfo.Length > 10 Then
				entity.Afi = cInfo(10)
				entity.Desfid = cInfo(9)
			End If
			Return entity
		End Function

		''' <summary>
		''' 
		''' </summary>
		 ''' <param name="cMode">less than 10</param>
		''' <param name="pcUid"></param>
		''' <returns></returns>
		 Public Function GetSystemInfo(ByVal cMode As Byte, ByVal pcUid() As Byte) As Byte()
			 Dim pcInfo(511) As Byte
			 Dim pcInfoLen(9) As Byte
			 Dim len As Integer = 0
			 If pcUid IsNot Nothing Then
				 len = pcUid.Length
			 End If

			 Dim result As Integer = HF15693GetSystemInfo(cMode, pcUid, CByte(len), pcInfo, pcInfoLen)
			 If result <> SUCCESS Then
				 Return Nothing
			 End If
			Return BLEDeviceAPI.Utils.CopyArray(pcInfo, pcInfoLen(0))
		End Function
		 ''' <summary>
		 ''' 
		 ''' </summary>
		 ''' <param name="cMode">less than 10</param>
		 ''' <param name="pcUid">the point of pcUid</param>
		 ''' <param name="iStartBlock">start address of cBlock</param>
		 ''' <param name="cBlockNum">cBlock number</param>
		 ''' <returns></returns>
		 Public Function Read(ByVal entity As ISO15693Entity, ByVal block As Integer) As Byte()

			 Dim origUID() As Byte = entity.Uid
			 If entity.Type = ISO15693Entity.TagType.EM4033 Then
				 Return Nothing
			 End If
			 Dim result As Integer = -1
			 Dim pData(511) As Byte
			 Dim pDataLen(9) As Byte
			 If entity.Type = ISO15693Entity.TagType.STLRIS64K Then
				 result = HF15693Read(0, origUID, origUID.Length, block, 1, pData, pDataLen)
			 Else
				 result = HF15693Read(0, origUID, origUID.Length, block, 1, pData, pDataLen)
			 End If

			 If result <> SUCCESS Then
				 Return Nothing
			 End If
			Return BLEDeviceAPI.Utils.CopyArray(pData, pDataLen(0))

		End Function
		 ''' <summary>
		 ''' 写数据
		 ''' </summary>
		 ''' <param name="cMode">less than 10</param>
		 ''' <param name="pcUid">the point of pcUid</param>
		 ''' <param name="iStartBlock">address cBlock start</param>
		 ''' <param name="cBlockNum">cBlock number</param>
		 ''' <param name="pwData"></param>
		 ''' <returns></returns>
		 Public Function Write(ByVal entity As ISO15693Entity, ByVal block As Integer, ByVal pszData() As Byte) As Boolean


			 Dim uid() As Byte = entity.Uid
			 Dim iRes As Integer = -1
			 If entity.Type = ISO15693Entity.TagType.ICODE2 Then
				 iRes = HF15693Write(CByte(0), uid, 0, block, 1, pszData, CByte(pszData.Length))

			 ElseIf entity.Type = ISO15693Entity.TagType.TI2048 Then
				 iRes = HF15693Write(CByte(4), uid, 0, block, 1, pszData, CByte(pszData.Length))

			 ElseIf entity.Type = ISO15693Entity.TagType.STLRIS64K Then
				 iRes = HF15693Write(CByte(0), uid, 0, block, 1, pszData, CByte(pszData.Length))

			 Else
				 iRes = HF15693Write(CByte(0), uid, 0, block, 1, pszData, CByte(pszData.Length))
			 End If


			 Return iRes = SUCCESS

		 End Function
		  ''' <summary>
		  ''' 写afi
		  ''' </summary>
		  ''' <param name="entity"></param>
		  ''' <param name="afi"></param>
		  ''' <returns></returns>
		 Public Function WriteAFI(ByVal entity As ISO15693Entity, ByVal afi As Byte) As Boolean
			 Dim res As Integer = -1
			 If entity.Type = ISO15693Entity.TagType.ICODE2 Then
				 res = HF15693WriteAFI(0, Nothing, 0, afi)
			 ElseIf entity.Type = ISO15693Entity.TagType.TI2048 Then
				 res = HF15693WriteAFI(4, Nothing, 0, afi)
			 ElseIf entity.Type = ISO15693Entity.TagType.STLRIS64K Then
				 res = HF15693WriteAFI(0, Nothing, 0, afi)
			 Else
				 res = HF15693WriteAFI(0, Nothing, 0, afi)
			 End If

			 Return res = SUCCESS
		 End Function
		''' <summary>
		'''锁afi
		''' </summary>
		''' <returns></returns>
		 Public Function LockAFI() As Boolean
			 Dim res As Integer = HF15693LockAFI(0, Nothing, 0)

			 Return res = SUCCESS
		 End Function
		 ''' <summary>
		 ''' 写dsfid
		 ''' </summary>
		 ''' <param name="entity"></param>
		 ''' <param name="dsfid"></param>
		 ''' <returns></returns>
		 Public Function WriteDsfid(ByVal entity As ISO15693Entity, ByVal dsfid As Byte) As Boolean
			 Dim res As Integer = -1
			 If entity.Type = ISO15693Entity.TagType.ICODE2 Then
				 res = HF15693WriteDsfid(0, Nothing, 0, dsfid)
			 ElseIf entity.Type = ISO15693Entity.TagType.TI2048 Then
				 res = HF15693WriteDsfid(4, Nothing, 0, dsfid)
			 ElseIf entity.Type = ISO15693Entity.TagType.STLRIS64K Then
				 res = HF15693WriteDsfid(0, Nothing, 0, dsfid)
			 Else
				 res = HF15693WriteDsfid(0, Nothing, 0, dsfid)
			 End If

			 Return res = SUCCESS
		 End Function
		 ''' <summary>
		 ''' 锁Dsfid
		 ''' </summary>
		 ''' <returns></returns>
		 Public Function LockDsfid() As Boolean
			 Dim res As Integer = HF15693LockDSFID(0, Nothing, 0)

			 Return res = SUCCESS
		 End Function

	End Class
End Namespace
