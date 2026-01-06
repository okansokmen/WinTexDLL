Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Runtime.InteropServices
Imports UHFAPP.custom.TempertureTag2
Imports BLEDeviceAPI

Namespace UHFAPP
	Public Class TempertureTag2
		Inherits UHFAPI

'       
'            开始读取标签温度
'            return: 0--success, -1--unknow error, others--error code
'            mask_bank：掩码的数据区(0x00 为 Reserve 0x01 为 EPC， 0x02 表示 TID， 0x03 表示USR)。
'            mask_addr：为掩码的地址(bit为单位)，高字节在前。
'            mask_len：为掩码的长度(bit为单位)，高字节在前。
'            mask_data：为掩码数据，mask_len为0时，这里没有数据
'            min_temp:minum of limited temperature
'            max_temp:maxum of limited temperature
'            work_delay: start logging after delayed time
'            work_interval:interval of working
'        
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFStartLogging(ByVal mask_bank As Byte, ByVal mask_addr As Byte, ByVal mask_len As Integer, ByVal mask_data() As Byte, ByVal min_temp As Single, ByVal max_temp As Single, ByVal work_delay As Integer, ByVal work_interval As Integer) As Integer
		End Function


'        
'        停止读取标签温度
'        return: 0--success, -1--unknow error, others--error code
'        mask_bank：掩码的数据区(0x00 为 Reserve 0x01 为 EPC， 0x02 表示 TID， 0x03 表示USR)。
'        mask_addr：为掩码的地址(bit为单位)，高字节在前。
'        mask_len：为掩码的长度(bit为单位)，高字节在前。
'        mask_data：为掩码数据，mask_len为0时，这里没有数据
'        password: password,default 0
'        
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFStopLogging(ByVal mask_bank As Byte, ByVal mask_addr As Integer, ByVal mask_len As Integer, ByVal mask_data() As Byte, ByVal password As Long) As Integer
		End Function

'        
'        模式检查
'        return: 0--success, -1--unknow error, others--error code
'        mask_bank：掩码的数据区(0x00 为 Reserve 0x01 为 EPC， 0x02 表示 TID， 0x03 表示USR)。
'        mask_addr：为掩码的地址(bit为单位)，高字节在前。
'        mask_len：为掩码的长度(bit为单位)，高字节在前。
'        mask_data：为掩码数据，mask_len为0时，这里没有数据
'        password: password,default 0
'        
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFCheckOpMode(ByVal mask_bank As Byte, ByVal mask_addr As Integer, ByVal mask_len As Integer, ByVal mask_data() As Byte) As Integer
		End Function


'''        ********************************************************************************************************
'''          * 功能：获取连续盘存标签数据
'''          * 输出：uLenUii -- UII长度
'''          * 输出：uUii -- UII数据
'''         ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHF_GetReceived_EX(ByRef uLenUii As Integer, ByVal uUii() As Byte) As Integer
		End Function


		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFSetEPCTIDUSERMode(ByVal saveflag As Byte, ByVal memory As Byte, ByVal address As Byte, ByVal lenth As Byte) As Integer
		End Function


'        
'        读取标签电压
'        return:大于0表示temperature, -1--unknow error, others--error code
'        mask_bank：掩码的数据区(0x00 为 Reserve 0x01 为 EPC， 0x02 表示 TID， 0x03 表示USR)。
'        mask_addr：为掩码的地址(bit为单位)，高字节在前。
'        mask_len：为掩码的长度(bit为单位)，高字节在前。
'        mask_data：为掩码数据，mask_len为0时，这里没有数据
'        password: password,default 0
'        voltage[out]：返回的标签电压值
'        
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFReadTagVoltage(ByVal mask_bank As Byte, ByVal mask_addr As Integer, ByVal mask_len As Integer, ByVal mask_data() As Byte, ByVal voltage() As Single) As Integer
		End Function

'        
'        读取多个定时测温温度值
'        return:大于0表示temperature, -1--unknow error, others--error code
'        mask_bank：掩码的数据区(0x00 为 Reserve 0x01 为 EPC， 0x02 表示 TID， 0x03 表示USR)。
'        mask_addr：为掩码的地址(bit为单位)，高字节在前。
'        mask_len：为掩码的长度(bit为单位)，高字节在前。
'        mask_data：为掩码数据，mask_len为0时，这里没有数据
'        t_start:为读取的起始温度值序号，即从第几个温度值开始读取，高字节在 前。
'        t_num：为要读取的温度值数量，最大为 50，即每次最大只能读取 50 个温度值。标签里的温度值数量小于 50，则有多少就读取多少。
'        totalNum[out]：温度记录总数
'        returnNum[out]：当前返回的温度个数
'        temp[out]：获取的温度数组
'        
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFReadMultiTemp(ByVal mask_bank As Byte, ByVal mask_addr As Integer, ByVal mask_len As Integer, ByVal mask_data() As Byte, ByVal t_start As Integer, ByVal t_num As Byte, ByVal totalNum() As Integer, ByVal returnNum() As Byte, ByVal temp() As Single) As Integer
		End Function


		Public Function uhfGetReceivedTempertureInfo() As TempertureInfo
			Dim uLen As Integer = 0
			Dim bufData(149) As Byte
			If GetReceived_EX(uLen, bufData) Then
				Dim epc_data As String = String.Empty
				Dim uii_data As String = String.Empty 'uii数据
				Dim tid_data As String = String.Empty 'tid数据
				Dim rssi_data As String = String.Empty
				Dim ant_data As String = String.Empty
				Dim user_data As String = String.Empty

				Dim uii_len As Integer = bufData(0) 'uii长度
				Dim tid_leng As Integer = bufData(uii_len + 1) 'tid数据长度
				Dim tid_idex As Integer = uii_len + 2 'tid起始位
				Dim rssi_index As Integer = 1 + uii_len + 1 + tid_leng
				Dim ant_index As Integer = rssi_index + 2

				Dim strData As String = BitConverter.ToString(bufData, 0, uLen).Replace("-", "")
				epc_data = strData.Substring(6, uii_len * 2 - 4) 'Epc

				If tid_leng > 12 Then
					tid_data = strData.Substring(tid_idex * 2, 24) 'Tid
					user_data = strData.Substring(tid_idex * 2 + 24, (tid_leng - 12) * 2) 'Tid
				Else
					tid_data = strData.Substring(tid_idex * 2, tid_leng * 2) 'Tid
				End If

				Dim temp As String = strData.Substring(rssi_index * 2, 4)
				Dim rssiTemp As Integer = Convert.ToInt32(temp, 16) - 65535
				rssi_data = CSng(rssiTemp) / 10.0.ToString() ' RSSI  =  (0xFED6   -65535)/10
				If Not rssi_data.Contains(".") Then
					rssi_data = rssi_data & ".0"
				End If
				ant_data = Convert.ToInt32((strData.Substring(ant_index * 2, 2)), 16).ToString()

				Dim info As New UHFTAGInfo()
				info.Epc = epc_data
				info.Tid = tid_data
				info.Rssi = rssi_data
				info.Ant = ant_data
			   ' info.User = user_data;



				Dim tempInfo As New TempertureInfo()
				tempInfo.UhfTagInfo = info
				If user_data.Length = 8 Then
					Dim temperture_data() As Byte = DataConvert.HexStringToByteArray(user_data)
					Dim itemp As Integer = temperture_data(0) Or ((temperture_data(1) And 3) << 8)
					tempInfo.Temperture = convert10BitFloat(itemp) & ""
					Dim itime As Integer = temperture_data(2) Or ((temperture_data(3) And 127) << 8)
					tempInfo.Time = itime & ""
				End If
				Return tempInfo


			Else
				Return Nothing
			End If
		End Function


'        
'        public TempertureInfo uhfGetReceivedTempertureInfo()
'        {
'            int uLen = 0;
'            byte[] bufData = new byte[150];
'            if (GetReceived_EX(ref uLen, ref bufData))
'            {
'
'                int uii_len = bufData[0];//uii长度
'                byte[] epc_data = new byte[uii_len - 2];
'                byte[] temperture_data = new byte[4];
'                byte[] rssi_data = new byte[2];
'                byte[] ant_data = new byte[1];
'
'                int temperture_idex = 3 + epc_data.Length + 1;
'                int rssi_index = temperture_idex + temperture_data.Length;
'                int ant_index = rssi_index + rssi_data.Length;
'                //epc数据
'                Array.Copy(bufData, 3, epc_data, 0, epc_data.Length);// strData.Substring(6, uii_len * 2 - 4);  //Epc
'                //温度数据
'                if (bufData[temperture_idex - 1] > 0)
'                {
'                    Array.Copy(bufData, temperture_idex, temperture_data, 0, temperture_data.Length);
'                }
'                else
'                {
'                    rssi_index = temperture_idex;
'                    ant_index = rssi_index + rssi_data.Length;
'                }
'                //rssi
'                Array.Copy(bufData, rssi_index, rssi_data, 0, rssi_data.Length);
'                //ant
'                Array.Copy(bufData, ant_index, ant_data, 0, ant_data.Length);
'
'
'                int rssiTemp = ((rssi_data[0] << 8) | (rssi_data[1])) - 65535;
'                string strRssi = ((float)rssiTemp / 10.0).ToString();// RSSI  =  (0xFED6   -65535)/10
'                if (!strRssi.Contains("."))
'                {
'                    strRssi = strRssi + ".0";
'                }
'
'
'                UHFTAGInfo info = new UHFTAGInfo();
'                info.Epc = DataConvert.ByteArrayToHexString(epc_data);
'                info.Rssi = strRssi;
'                info.Ant = ant_data[0].ToString();
'                TempertureInfo tempInfo = new TempertureInfo();
'                tempInfo.UhfTagInfo = info;
'                int itemp = temperture_data[0] | ((temperture_data[1] & 3) << 8);
'                if (bufData[temperture_idex - 1] > 0)
'                {
'                    tempInfo.Temperture = convert10BitFloat(itemp) + "";
'
'                    int itime = temperture_data[2] | ((temperture_data[3] & 127) << 8);
'                    tempInfo.Time = itime + "";
'
'                }
'                return tempInfo;
'            }
'            else
'            {
'                return null;
'            }
'        }
'        
		Private Function convert10BitFloat(ByVal temp As Integer) As Single
			Dim ret As SByte = CSByte(temp >> 2)
			Dim f As Single = ret
			If f > 0 Then
				f += CSng((temp And &H3) * 0.25)
			Else
				f -= CSng((temp And &H3) * 0.25)
				f += 1
			End If
			Return f
		End Function
		''' <summary>
		''' 开始读温度
		''' </summary>
		''' <param name="filter_bank"></param>
		''' <param name="filter_addr"></param>
		''' <param name="filter_len"></param>
		''' <param name="filter_data"></param>
		''' <param name="password"></param>
		''' <returns></returns>
		Public Function StartLogging(ByVal filter_bank As Byte, ByVal filter_addr As Byte, ByVal filter_len As Integer, ByVal filter_data() As Byte, ByVal min_temp As Single, ByVal max_temp As Single, ByVal work_delay As Integer, ByVal work_interval As Integer) As Boolean

			Dim result As Integer = UHFStartLogging(filter_bank, filter_addr, filter_len, filter_data, min_temp, max_temp, work_delay, work_interval)
			If result = 0 Then
				Return True
			Else
				Return False
			End If
		End Function
		''' <summary>
		''' 停止读温度
		''' </summary>
		''' <param name="filter_bank"></param>
		''' <param name="filter_addr"></param>
		''' <param name="filter_len"></param>
		''' <param name="filter_data"></param>
		''' <param name="password"></param>
		''' <returns></returns>
		Public Function StopLogging(ByVal filter_bank As Byte, ByVal filter_addr As Integer, ByVal filter_len As Integer, ByVal filter_data() As Byte, ByVal password As Long) As Boolean

			Dim result As Integer = UHFStopLogging(filter_bank, filter_addr, filter_len, filter_data, password)
			If result = 0 Then
				Return True
			Else
				Return False
			End If
		End Function

		''' <summary>
		''' 
		''' </summary>
		''' <param name="filter_bank"></param>
		''' <param name="filter_addr"></param>
		''' <param name="filter_len"></param>
		''' <param name="filter_data"></param>
		''' <param name="password"></param>
		''' <returns></returns>
		Public Function CheckOpMode(ByVal filter_bank As Byte, ByVal filter_addr As Integer, ByVal filter_len As Integer, ByVal filter_data() As Byte) As Integer

			Dim result As Integer = UHFCheckOpMode(filter_bank, filter_addr, filter_len, filter_data)
			Return result
		End Function


		Public Function setEPCAndTemperature() As Boolean
			Dim result As Integer = UHFSetEPCTIDUSERMode(CByte(0), CByte(3), CByte(0), CByte(0))
			Return result = 0
		End Function

		Public Function ReadTagVoltage(ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal outtemp() As Single) As Boolean
			Dim result As Integer = UHFReadTagVoltage(FilterBank, FilterStartaddr, FilterLen, FilterData, outtemp)
			If result = 0 Then
				Return True
			End If
			Return False
		End Function

		Public Function ReadMultiTemp(ByVal mask_bank As Byte, ByVal mask_addr As Integer, ByVal mask_len As Integer, ByVal mask_data() As Byte, ByVal t_start As Integer, ByVal t_num As Byte, ByVal totalNum() As Integer, ByVal returnNum() As Byte, ByVal temp() As Single) As Boolean
			Dim result As Integer = UHFReadMultiTemp(mask_bank, mask_addr, mask_len, mask_data, t_start, t_num, totalNum, returnNum, temp)
			If result = 0 Then
				Return True
			End If
			Return False
		End Function

	End Class
End Namespace
