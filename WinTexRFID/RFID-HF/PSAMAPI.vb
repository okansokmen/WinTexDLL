Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Runtime.InteropServices
Imports BLEDeviceAPI

Namespace UHFAPP.RFID_HF
	Friend Class PSAMAPI
'''        
'''         *@brief: initial smart card module 
'''         *@Param[in] cSlotNum:slot number,value(0/1) 
'''         *@Return:0->success,others->failure
'''         
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function SmartCardInit(ByVal cSlotNum As Byte) As Integer
		End Function

'''        
'''         *@brief: free smart card module 
'''         *@Param[in] cSlotNum:slot number,value(0/1/2) 
'''         *@Return:0->success,others->failure
'''         
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function SmartCardFree(ByVal cSlotNum As Byte) As Integer
		End Function


'''        
'''         *@brief: reset smart card 
'''         *@Param[in] cSlotNum:slot number,value(0/1/2) 
'''         *@Param[out] pcATR:the point of out data
'''         *@Param[out] pcATRLen:the point of out data length
'''         *@Return:0->success,others->failure
'''         
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function SmartCardReset(ByVal cSlotNum As Byte, ByVal pcATR() As Byte, ByVal pcATRLen() As Byte) As Integer
		End Function

'''        
'''         *@brief: smart card transfer command
'''         *@Param[in] cSlotNum:slot number,value(0/1/2) 
'''         *@Param[in] pcInCmd:the point of command data 
'''         *@Param[in] cLen:command length
'''         *@Param[out] pcOutCmd:the point of out data
'''         *@Param[out] pcOutLen:the point of out data length
'''         *@Return:0->success,others->failure
'''         
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function SmartCardTransferCmd(ByVal cSlotNum As Byte, ByVal pcInCmd() As Byte, ByVal cLen As Byte, ByVal pcOutCmd() As Byte, ByVal pcOutLen() As Byte) As Integer
		End Function

		Private Const SUCCESS As Integer = 0
		''' <summary>
		''' 
		''' </summary>
		''' <param name="cSlotNum">cSlotNum:slot number,value(0/1) </param>
		''' <returns></returns>
		Public Function Init(ByVal cSlotNum As Byte) As Boolean
			Return SmartCardInit(cSlotNum) = 0
		End Function
		''' <summary>
		''' 
		''' </summary>
		''' <param name="cSlotNum"></param>
		''' <returns></returns>
		Public Function Free(ByVal cSlotNum As Byte) As Boolean
			Return SmartCardFree(cSlotNum) = 0
		End Function
		''' <summary>
		''' 
		''' </summary>
		''' <param name="cSlotNum"></param>
		''' <returns></returns>
		Public Function Reset(ByVal cSlotNum As Byte) As Byte()
			Dim pcATR(255) As Byte
			Dim pcATRLen(0) As Byte
			Dim result As Integer= SmartCardReset(cSlotNum, pcATR, pcATRLen)
			If result <> SUCCESS Then
				Return Nothing
			End If
			Return BLEDeviceAPI.Utils.CopyArray(pcATR, pcATRLen(0))

		End Function
		''' <summary>
		''' 
		''' </summary>
		''' <param name="cSlotNum"></param>
		''' <param name="pcInCmd"></param>
		''' <returns></returns>
		Public Function TransferCmd(ByVal cSlotNum As Byte, ByVal pcInCmd() As Byte) As Byte()
			Dim pcOutCmd(255) As Byte
			Dim pcOutLen(0) As Byte

			Dim result As Integer = SmartCardTransferCmd(cSlotNum, pcInCmd, CByte(pcInCmd.Length), pcOutCmd, pcOutLen)
			If result <> SUCCESS Then
				Return Nothing
			End If
			Return BLEDeviceAPI.Utils.CopyArray(pcOutCmd, pcOutLen(0))
		End Function
	End Class
End Namespace
