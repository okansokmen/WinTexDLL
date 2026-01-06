Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Runtime.InteropServices

Namespace UHFAPP
	Public Class UHFAPI_RFMicronMagnus_S3
		Inherits UHFAPI


'''        ********************************************************************************************************
'''        * 功能：读取 Sensor Code
'''        * 输入：epc： EPC号，16个字节
'''
'''               antNum:  天线号， 1个字节
'''
'''	           powerValue： 功率值，2个字节
'''
'''          输出：data， 2个字节
'''
'''          返回值：2：数据长度    -1：获取失败
'''        * 
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetSensorCode(ByVal epc() As Byte, ByVal antNum As Byte, ByVal powerValue() As Byte, ByVal outData() As Byte) As Integer
		End Function


'''        ********************************************************************************************************
'''        * 功能：读取 Calibration Data
'''        * 输入：epc： EPC号，16个字节
'''
'''               antNum:  天线号， 1个字节
'''
'''	           powerValue： 功率值，2个字节
'''
'''          输出：data， 8个字节
'''
'''          返回值：8：数据长度    -1：获取失败
'''
'''        * 
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetCalibrationData(ByVal epc() As Byte, ByVal antNum As Byte, ByVal powerValue() As Byte, ByVal outData() As Byte) As Integer
		End Function


'''        ********************************************************************************************************
'''        * 功能：读取 On-Chip RSSI
'''        * 输入：epc： EPC号，16个字节
'''
'''               antNum:  天线号， 1个字节
'''
'''	           powerValue： 功率值，2个字节
'''
'''          输出：data， 2个字节
'''
'''          返回值：2：数据长度    -1：获取失败
'''
'''        * 
'''        ********************************************************************************************************
	  <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
	  Private Shared Function UHFGetOnChipRSSI(ByVal epc() As Byte, ByVal antNum As Byte, ByVal powerValue() As Byte, ByVal outData() As Byte) As Integer
	  End Function


'''        ********************************************************************************************************
'''        * 功能：读取 Temperture Code
'''        * 输入：epc： EPC号，16个字节
'''
'''               antNum:  天线号， 1个字节
'''
'''	           powerValue： 功率值，2个字节
'''
'''          输出：data， 2个字节
'''
'''          返回值：2：数据长度    -1：获取失败
'''
'''        * 
'''        ********************************************************************************************************
	  <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
	  Private Shared Function UHFGetTempertureCode(ByVal epc() As Byte, ByVal antNum As Byte, ByVal powerValue() As Byte, ByVal outData() As Byte) As Integer
	  End Function


'''        ********************************************************************************************************
'''        * 功能：读取 On-Chip RSSI+ Temp Code
'''        * 输入：epc： EPC号，16个字节
'''
'''               antNum:  天线号， 1个字节
'''
'''	           powerValue： 功率值，2个字节
'''
'''          输出：data， 4个字节  ,  RSSI: data[0] data[1]   ,  TempCode: data[2] data[3]  
'''
'''          返回值：4：数据长度    -1：获取失败
'''
'''        * 
'''        ********************************************************************************************************
	  <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
	  Private Shared Function UHFGetOnChipRSSIAndTempCode(ByVal epc() As Byte, ByVal antNum As Byte, ByVal powerValue() As Byte, ByVal outData() As Byte) As Integer
	  End Function



'''        ********************************************************************************************************
'''        * 功能：开始   盘点 Calibration Data+ Sensor Code+ On-Chip RSSI+ Tempe Code
'''        * 输入： 
'''
'''               antNum:  天线号， 1个字节
'''
'''	           powerValue： 功率值，2个字节
'''
'''
'''          返回值：0：发送成功    1：发送失败
'''        * 
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFInventoryTempTag(ByVal antNum As Byte, ByVal powerValue() As Byte) As Integer
		End Function


		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHF_GetTempTagReceived(ByRef uLenUii As Integer, ByVal uUii() As Byte) As Integer
		End Function




'''        ********************************************************************************************************
'''        * 功能：开始   盘点 On-Chip RSSI+ Tempe Code
'''        * 输入： 
'''
'''               antNum:  天线号， 1个字节
'''
'''	           powerValue： 功率值，2个字节
'''
'''
'''          返回值：0：发送成功    1：发送失败
'''
'''        * 
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFInventoryTempTag2(ByVal antNum As Byte, ByVal powerValue() As Byte) As Integer
		End Function

	   <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
	   Private Shared Function UHF_GetTempTagReceived2(ByRef uLenUii As Integer, ByVal uUii() As Byte) As Integer
	   End Function

		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFPerformInventory(ByVal mode As Integer, ByVal param() As Byte, ByVal paramlen As Integer) As Integer
		End Function



'''        ********************************************************************************************************
'''        * 功能：写入 Calibration Data
'''        * 输入：epc： EPC号，16个字节
'''
'''               antNum:  天线号， 1个字节
'''
'''	           powerValue： 功率值，2个字节
'''
'''               data， Calibration Data , 8个字节
'''
'''          返回值：0：成功    -1：失败
'''
'''        * 
'''        ********************************************************************************************************
	   <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
	   Private Shared Function UHFWriteCalibrationData(ByVal epc() As Byte, ByVal antNum As Byte, ByVal powerValue() As Byte, ByVal data() As Byte) As Integer
	   End Function

'''         ********************************************************************************************************
'''        * 功能：读取 Calibration Data
'''        * 输入：mode:功能字
'''		        epc： EPC号，16个字节
'''               antNum:  天线号， 1个字节
'''	           powerValue： 功率值，2个字节
'''
'''          输出：data， 8个字节
'''          返回值：8：数据长度    -1：获取失败
'''        * 
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetCalibrationDataEX(ByVal mode As Byte, ByVal epc() As Byte, ByVal antNum As Byte, ByVal powerValue() As Byte, ByVal data() As Byte) As Integer
		End Function




		Private Shared uhf As UHFAPI_RFMicronMagnus_S3 = Nothing
		Friend Sub New()
		End Sub
		Public Overloads Shared Function getInstance() As UHFAPI_RFMicronMagnus_S3
			If uhf Is Nothing Then
				uhf = New UHFAPI_RFMicronMagnus_S3()
			End If
			Return uhf
		End Function

		''' <summary>
		''' 读取 On-Chip RSSI+ Temp Code
		''' </summary>
		''' <param name="epc"></param>
		''' <param name="ant"></param>
		''' <param name="power"></param>
		''' <returns></returns>
		Public Function ReadOnChipRSSIAndTempCode(ByVal epc() As Byte, ByVal ant As Integer, ByVal power As Integer) As String
			Dim len As Integer = 4
			Dim antNum As Byte = CByte(ant)
			Dim powerValue() As Byte = getPowerToBytes(power)
			Dim outData(len - 1) As Byte
			len = UHFGetOnChipRSSIAndTempCode(epc, antNum, powerValue, outData)
			If len <> -1 Then
				Dim strData As String = DataConvert.ByteArrayToHexString(outData, len)
				Return strData
			End If

			Return Nothing
		End Function

		''' <summary>
		''' 读取 On-Chip RSSI+ Temp Code+ Calibration Data
		''' </summary>
		''' <param name="epc"></param>
		''' <param name="ant"></param>
		''' <param name="power"></param>
		''' <returns></returns>
		Public Function ReadOnChipRSSI_TempCode_CalibrationData(ByVal epc() As Byte, ByVal ant As Integer, ByVal power As Integer) As String
			Dim len As Integer = 256
			Dim antNum As Byte = CByte(ant)
			Dim powerValue() As Byte = getPowerToBytes(power)
			Dim outData(len - 1) As Byte
			len = UHFGetCalibrationDataEX(7,epc, antNum, powerValue, outData)
			If len <> -1 Then
				Dim strData As String = DataConvert.ByteArrayToHexString(outData, len)
				Return strData
			End If
			Return Nothing
		End Function
		''' <summary>
		''' 读取 Sensor Code
		''' </summary>
		''' <param name="epc"></param>
		''' <param name="ant"></param>
		''' <param name="power"></param>
		''' <returns></returns>
		Public Function ReadSensorCode(ByVal epc() As Byte, ByVal ant As Integer, ByVal power As Integer) As String
			Dim len As Integer = 2
			Dim antNum As Byte = CByte(ant)
			Dim powerValue() As Byte = getPowerToBytes(power)
			Dim outData(len - 1) As Byte
			len = UHFGetSensorCode(epc, antNum, powerValue, outData)
			If len<>-1 Then
				Dim strData As String = DataConvert.ByteArrayToHexString(outData, len)
				Return strData
			End If

			Return Nothing
		End Function
		''' <summary>
		''' 读取 Calibration Data
		''' </summary>
		''' <param name="epc"></param>
		''' <param name="ant"></param>
		''' <param name="power"></param>
		''' <returns></returns>
		Public Function ReadCalibrationData(ByVal epc() As Byte, ByVal ant As Integer, ByVal power As Integer) As String
			Dim len As Integer = 8
			Dim antNum As Byte = CByte(ant)
			Dim powerValue() As Byte = getPowerToBytes(power)
			Dim outData(len - 1) As Byte
			len=UHFGetCalibrationData(epc, antNum, powerValue, outData)
			If len<>0 Then
				Dim strData As String = DataConvert.ByteArrayToHexString(outData, len)
				Return strData
			End If

			Return Nothing
		End Function
		''' <summary>
		''' 读取 On-Chip RSSI
		''' </summary>
		''' <param name="epc"></param>
		''' <param name="ant"></param>
		''' <param name="power"></param>
		''' <returns></returns>
		Public Function ReadOnChipRSSI(ByVal epc() As Byte, ByVal ant As Integer, ByVal power As Integer) As String
			Dim len As Integer = 2
			Dim antNum As Byte = CByte(ant)
			Dim powerValue() As Byte = getPowerToBytes(power)
			Dim outData(len - 1) As Byte
			len=UHFGetOnChipRSSI(epc, antNum, powerValue, outData)
			If len<>-1 Then
				Dim strData As String = DataConvert.ByteArrayToHexString(outData, len)
				Return strData
			End If

			Return Nothing
		End Function
		''' <summary>
		''' 读取 Temperture Code
		''' </summary>
		''' <param name="epc"></param>
		''' <param name="ant"></param>
		''' <param name="power"></param>
		''' <returns></returns>
		Public Function ReadTempertureCode(ByVal epc() As Byte, ByVal ant As Integer, ByVal power As Integer) As String
			Dim len As Integer = 2
			Dim antNum As Byte = CByte(ant)
			Dim powerValue() As Byte = getPowerToBytes(power)
			Dim outData(len - 1) As Byte
			len=UHFGetTempertureCode(epc, antNum, powerValue, outData)
			If len<>-1 Then
				Dim strData As String = DataConvert.ByteArrayToHexString(outData, len)
				Return strData
			End If

			Return Nothing
		End Function

		Public Function InventoryTempTag(ByVal ant As Integer, ByVal power As Integer) As Boolean
			Dim antNum As Byte = CByte(ant)
			Dim powerValue() As Byte = getPowerToBytes(power)

			If UHFInventoryTempTag(antNum, powerValue) = 0 Then
				Return True
			End If
			Return False
		End Function
		Public Function InventoryTempTag_OnChipRSSI_TempeCode(ByVal ant As Integer, ByVal power As Integer) As Boolean
			Dim antNum As Byte = CByte(ant)
			Dim powerValue() As Byte = getPowerToBytes(power)

			If UHFInventoryTempTag2(antNum, powerValue) = 0 Then
				Return True
			End If
			Return False
		End Function

		Public Function PerformInventory(ByVal ant As Integer, ByVal power As Integer) As Boolean
			Dim mode As Integer = 2
			Dim p(3) As Byte
			Dim powerValue() As Byte = getPowerToBytes(power)
			p(0) = CByte(3)
			p(1) = CByte(ant)
			p(2) = CByte(powerValue(0))
			p(3) = CByte(powerValue(1))
			If UHFPerformInventory(mode, p, p.Length) = 0 Then
				Return True
			End If
			Return False
		End Function

		''' <summary>
		''' 获取连续盘存标签数据
		''' </summary>
		''' <param name="uLenUii">UII长度</param>
		''' <param name="uUii">UII数据</param>
		''' <returns></returns>
		Private Function GetTempTagReceived(ByRef uLenUii As Integer, ByRef uUii() As Byte) As Boolean
			If UHF_GetTempTagReceived(uLenUii, uUii) = 0 Then
				Return True
			End If
			Return False
		End Function
		Private Function GetTempTagReceived_OnChipRSSI_TempeCode(ByRef uLenUii As Integer, ByRef uUii() As Byte) As Boolean
			If UHF_GetTempTagReceived2(uLenUii, uUii) = 0 Then
				Return True
			End If
			Return False
		End Function
		'读取epc
		Public Function uhfGetTempTagReceived(ByRef epc As String, ByRef calibrationData As String, ByRef sensorCode As String, ByRef rssiCode As String, ByRef tempeCode As String, ByRef rssi As String, ByRef ant As Integer) As Boolean

				Dim uLen As Integer = 0
				Dim bufData(149) As Byte
				If GetTempTagReceived(uLen, bufData) Then 'GetTempTagReceived   GetReceived_EX
					'  uUii = 
					'  1个字节uii长度+ UII数据+ 
					'  1个字节TID数据长度+TID数据
					'  8个字节CalibrationData +
					'  2个字节Sensor Code + 
					'  2个字节Rssi code +
					'  2个字节Tempe code +
					'  2个字节RSSI +
					'  1个字节Ant +

					Dim uii_len As Integer = bufData(0) 'uii长度
					Dim uii_index As Integer = 1 'uii的起始地址
					Dim tid_leng As Integer = bufData(uii_len + 1) 'tid数据长度

					Dim tid_idex As Integer = uii_len + 1 'tid起始位
					If tid_leng > 0 Then
						tid_idex = uii_len + 2 'tid起始位
					End If

					Dim calibrationData_index As Integer = tid_idex + tid_leng + 1
					Dim sensorcode_index As Integer = calibrationData_index + 8
					Dim rssiCode_index As Integer = sensorcode_index + 2
					Dim tempe_index As Integer = rssiCode_index + 2
					Dim rssi_index As Integer = tempe_index + 2
					Dim ant_index As Integer = rssi_index + 2

					Dim buffCalibrationData(7) As Byte
					Dim buffSensorCode(1) As Byte
					Dim buffRssiCode(1) As Byte
					Dim buffTempe(1) As Byte
					Dim buffRssi(1) As Byte
					Dim buffAnt(0) As Byte

					Dim buffUii(uii_len - 1) As Byte
					Dim buffEPC(uii_len - 3) As Byte

					Array.Copy(bufData, uii_index, buffUii, 0, buffUii.Length)
					Array.Copy(buffUii, 2, buffEPC, 0, buffEPC.Length)

					Dim buffTid(tid_leng - 1) As Byte
					If buffTid.Length > 0 Then
						Array.Copy(bufData, tid_idex, buffTid, 0, buffTid.Length)
					End If
					Array.Copy(bufData, calibrationData_index, buffCalibrationData, 0, buffCalibrationData.Length)
					Array.Copy(bufData, sensorcode_index, buffSensorCode, 0, buffSensorCode.Length)
					Array.Copy(bufData, rssiCode_index, buffRssiCode, 0, buffRssiCode.Length)
					Array.Copy(bufData, tempe_index, buffTempe, 0, buffTempe.Length)
					Array.Copy(bufData, rssi_index, buffRssi, 0, buffRssi.Length)
					Array.Copy(bufData, ant_index, buffAnt, 0, buffAnt.Length)

					'string strData = BitConverter.ToString(bufData, 0, uLen).Replace("-", "");
					'epc_data = strData.Substring(6, uii_len * 2 - 4);  //Epc
					'tid_data = strData.Substring(tid_idex * 2, tid_leng * 2); //Tid
					'string temp = strData.Substring(rssi_index * 2, 4);
					'rssi_data = ((Convert.ToInt32(temp, 16) - 65535) / 10).ToString();// RSSI  =  (0xFED6   -65535)/10
					'ant_data = Convert.ToInt32((strData.Substring(ant_index * 2, 2)), 16).ToString();


					epc = DataConvert.ByteArrayToHexString(buffEPC, buffEPC.Length)
					calibrationData = DataConvert.ByteArrayToHexString(buffCalibrationData, buffCalibrationData.Length)
					sensorCode = DataConvert.ByteArrayToHexString(buffSensorCode, buffSensorCode.Length)
					rssiCode = DataConvert.ByteArrayToHexString(buffRssiCode, buffRssiCode.Length)
					tempeCode = DataConvert.ByteArrayToHexString(buffTempe, buffTempe.Length)
'INSTANT VB WARNING: Instant VB cannot determine whether both operands of this division are integer types - if they are then you should use the VB integer division operator:
					rssi = (((((buffRssi(0) And &HFF) << 8) Or (buffRssi(1) And &HFF)) - 65535) / 10).ToString() ' RSSI  =  (0xFED6   -65535)/10
					ant = buffAnt(0)

					Return True
				Else
					Return False
				End If

		End Function
		Public Function uhfGetTempTagReceived_OnChipRSSI_TempeCode(ByRef epc As String, ByRef rssiCode As String, ByRef tempeCode As String, ByRef rssi As String, ByRef ant As Integer) As Boolean
			Dim uLen As Integer = 0
			Dim bufData(149) As Byte
			If GetTempTagReceived_OnChipRSSI_TempeCode(uLen, bufData) Then 'GetTempTagReceived   GetReceived_EX

				Dim data As String = DataConvert.ByteArrayToHexString(bufData, bufData.Length)

				'  uUii = 
				'  1个字节uii长度+ UII数据+ 
				'  1个字节TID数据长度+TID数据
				'  2个字节Rssi code +
				'  2个字节Tempe code +
				'  2个字节RSSI +
				'  1个字节Ant +

				Dim uii_len As Integer = bufData(0) 'uii长度
				Dim uii_index As Integer = 1 'uii的起始地址
				Dim tid_leng As Integer = bufData(uii_len + 1) 'tid数据长度

				Dim tid_idex As Integer = uii_len + 1 'tid起始位
				If tid_leng > 0 Then
					tid_idex = uii_len + 2 'tid起始位
				End If
				Dim rssiCode_index As Integer = tid_idex + tid_leng + 1
				Dim tempe_index As Integer = rssiCode_index + 2
				Dim rssi_index As Integer = tempe_index + 2
				Dim ant_index As Integer = rssi_index + 2

				Dim buffRssiCode(1) As Byte
				Dim buffTempe(1) As Byte
				Dim buffRssi(1) As Byte
				Dim buffAnt(0) As Byte

				Dim buffUii(uii_len - 1) As Byte
				Dim buffEPC(uii_len - 3) As Byte

				Array.Copy(bufData, uii_index, buffUii, 0, buffUii.Length)
				Array.Copy(buffUii, 2, buffEPC, 0, buffEPC.Length)

				Dim buffTid(tid_leng - 1) As Byte
				If buffTid.Length > 0 Then
					Array.Copy(bufData, tid_idex, buffTid, 0, buffTid.Length)
				End If

				Array.Copy(bufData, rssiCode_index, buffRssiCode, 0, buffRssiCode.Length)
				Array.Copy(bufData, tempe_index, buffTempe, 0, buffTempe.Length)
				Array.Copy(bufData, rssi_index, buffRssi, 0, buffRssi.Length)
				Array.Copy(bufData, ant_index, buffAnt, 0, buffAnt.Length)

				'string strData = BitConverter.ToString(bufData, 0, uLen).Replace("-", "");
				'epc_data = strData.Substring(6, uii_len * 2 - 4);  //Epc
				'tid_data = strData.Substring(tid_idex * 2, tid_leng * 2); //Tid
				'string temp = strData.Substring(rssi_index * 2, 4);
				'rssi_data = ((Convert.ToInt32(temp, 16) - 65535) / 10).ToString();// RSSI  =  (0xFED6   -65535)/10
				'ant_data = Convert.ToInt32((strData.Substring(ant_index * 2, 2)), 16).ToString();


				epc = DataConvert.ByteArrayToHexString(buffEPC, buffEPC.Length)
				rssiCode = DataConvert.ByteArrayToHexString(buffRssiCode, buffRssiCode.Length)
				tempeCode = DataConvert.ByteArrayToHexString(buffTempe, buffTempe.Length)
'INSTANT VB WARNING: Instant VB cannot determine whether both operands of this division are integer types - if they are then you should use the VB integer division operator:
				rssi = (((((buffRssi(0) And &HFF) << 8) Or (buffRssi(1) And &HFF)) - 65535) / 10).ToString() ' RSSI  =  (0xFED6   -65535)/10
				ant = buffAnt(0)

				Return True
			Else
				Return False
			End If

		End Function

		Private Function getPowerToBytes(ByVal power As Integer) As Byte()
			power = power * 100
			Dim b1 As Integer = (power >> 8) And &HFF
			Dim b2 As Integer = power And &HFF
			Dim powerValue() As Byte = { CByte(b1), CByte(b2) }

			Return powerValue
		End Function

		Public Function WriteCalibrationData(ByVal epc() As Byte, ByVal antNum As Integer, ByVal powerValues As Integer, ByVal data() As Byte) As Boolean
			Dim ant As Byte = CByte(antNum)
			Dim power() As Byte = getPowerToBytes(powerValues)

			If UHFWriteCalibrationData(epc, ant, power, data) = 0 Then
				Return True
			Else
				Return False
			End If
		End Function



	End Class
End Namespace
