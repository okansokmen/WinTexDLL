Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Runtime.InteropServices
Imports BLEDeviceAPI
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Windows.Forms
Imports UHFAPP.multidevice

Namespace UHFAPP
	Public Class UHFAPI
		Implements IUHF



		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFSetTempVal(ByVal tempVal As Byte) As Integer
		End Function
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetTempVal(ByVal tempVal() As Byte) As Integer
		End Function


		' [DllImport("UHFAPI.dll", CallingConvention = CallingConvention.Cdecl)]
		' private extern static int UHFSetIp(byte[] ip, byte[] port);
		'[DllImport("UHFAPI.dll", CallingConvention = CallingConvention.Cdecl)]
		'private extern static int UHFGetIp(byte[] ip, byte[] port);

'        
'         * 函数功能：  获取本机 IP 和端口号
'         * 输出参数：  ipbuf + postbuf， IP+端口号
'			           mask:掩码，4字节
'			           gate:网关，4字节
'         * 返回值：   0:成功    其它：失败
'         
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetIp(ByVal ip() As Byte, ByVal port() As Byte, ByVal mask() As Byte, ByVal gate() As Byte) As Integer
		End Function
'        
'         * 函数功能：  设置本机 IP 和端口号
'         * 输入参数：  ipbuf： IP， 
'			           postbuf：端口号
'			           mask:掩码，4字节
'			           gate：网关，4字节
'
'         * 返回值：   0:成功    其它：失败
'         
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFSetIp(ByVal ipbuf() As Byte, ByVal postbuf() As Byte, ByVal mask() As Byte, ByVal gate() As Byte) As Integer
		End Function



		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFSetDestIp(ByVal ip() As Byte, ByVal port() As Byte) As Integer
		End Function
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetDestIp(ByVal ip() As Byte, ByVal port() As Byte) As Integer
		End Function

		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFSetWorkMode(ByVal mode As Byte) As Integer
		End Function
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetWorkMode(ByVal mode() As Byte) As Integer
		End Function

		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFSetBeep(ByVal mode As Byte) As Integer
		End Function
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetBeep(ByVal mode() As Byte) As Integer
		End Function



		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function TCPConnect(ByVal ip As StringBuilder, ByVal hostport As UInteger) As Integer
		End Function
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Sub TCPDisconnect()
		End Sub

		'打开串口
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Public Shared Function ComOpenWithBaud(ByVal port As Integer, ByVal baudrate As Integer) As Integer
		End Function

		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function ComOpen(ByVal comName As Integer) As Integer
		End Function
		'关闭串口
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Sub ClosePort()
		End Sub

'''        ********************************************************************************************************
'''           * 功能：获取硬件版本号
'''           * 输出：version[0]--版本号长度 ,  version[1--x]--版本号
'''           ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetHardwareVersion(ByVal version() As Byte) As Integer
		End Function
'''        ********************************************************************************************************
'''          * 功能：获取软件版本号
'''          * 输出：version[0]--版本号长度 ,  version[1--x]--版本号
'''          ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetSoftwareVersion(ByVal version() As Byte) As Integer
		End Function
'''        ********************************************************************************************************
'''           * 功能：获取ID号
'''           * 输出：id--整型ID号
'''           ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetDeviceID(ByRef id As Integer) As Integer
		End Function

'''        ********************************************************************************************************
'''        * 功能：设置功率
'''        * 输入：saveflag  -- 1:保存设置   0：不保存
'''        * 输入：uPower -- 功率（DB）
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFSetPower(ByVal save As Byte, ByVal uPower As Byte) As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：设置天线功率
'''        * 输入：saveflag  -- 1:保存设置   0：不保存
'''        * 输入：num -- 天线编号(1~N)
'''                read_power -- 接收功率（DB）
'''                write_power -- 发送功率（DB）
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFSetAntennaPower(ByVal save As Byte, ByVal num As Byte, ByVal read_power As Byte, ByVal write_power As Byte) As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：获取功率
'''        * 输出：uPower -- 功率（DB）
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetPower(ByRef uPower As Byte) As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：获取天线功率
'''        * 输出：ppower -- 天线功率,格式为（天线编号+读功率+写功率+天线编号+读功率+写功率+.......+天线编号+读功率+写功率）
'''		        nBytesReturned -- ppower数据长度 
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetAntennaPower(ByVal ppower() As Byte, ByVal nBytesReturned() As Integer) As Integer
		End Function




'''        ********************************************************************************************************
'''        * 功能：设置跳频
'''        * 输入：nums -- 跳频个数
'''        * 输入：Freqbuf--频点数组（整型） ，如920125，921250.....
'''       ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFSetJumpFrequency(ByVal nums As Byte, ByVal Freqbuf() As Integer) As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：获取跳频
'''        * 输出：Freqbuf[0]--频点个数， Freqbuf[1]..[x]--频点数组（整型）
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetJumpFrequency(ByVal Freqbuf() As Integer) As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：设置Gen2参数
'''        * 输入：
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFSetGen2(ByVal Target As Byte, ByVal Action As Byte, ByVal T As Byte, ByVal Q As Byte, ByVal StartQ As Byte, ByVal MinQ As Byte, ByVal MaxQ As Byte, ByVal D As Byte, ByVal C As Byte, ByVal P As Byte, ByVal Sel As Byte, ByVal Session As Byte, ByVal G As Byte, ByVal LF As Byte) As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：获取Gen2参数
'''        * 输入：
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetGen2(ByRef Target As Byte, ByRef Action As Byte, ByRef T As Byte, ByRef Q As Byte, ByRef StartQ As Byte, ByRef MinQ As Byte, ByRef MaxQ As Byte, ByRef D As Byte, ByRef Coding As Byte, ByRef P As Byte, ByRef Sel As Byte, ByRef Session As Byte, ByRef G As Byte, ByRef LF As Byte) As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：设置CW
'''        * 输入：flag -- 1:开CW，  0：关CW
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFSetCW(ByVal flag As Byte) As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：获取CW
'''        * 输出：flag -- 1:开CW，  0：关CW
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetCW(ByRef flag As Byte) As Integer
		End Function

'''        ********************************************************************************************************
'''        * 功能：天线设置
'''        * 输入：saveflag -- 1:掉电保存，  0：不保存
'''        * 输入：buf--2bytes, 共16bits, 每bit 置1选择对应天线
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFSetANT(ByVal saveflag As Byte, ByVal buf() As Byte) As Integer
		End Function

'''        ********************************************************************************************************
'''        * 功能：获取天线设置
'''        * 输出：buf--2bytes, 共16bits,
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetANT(ByVal buf() As Byte) As Integer
		End Function

'''        ********************************************************************************************************
'''        * 功能：区域设置
'''        * 输入：saveflag -- 1:掉电保存，  0：不保存
'''        * 输入：region -- 0x01(China1),0x02(China2),0x04(Europe),0x08(USA),0x16(Korea),0x32(Japan)
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFSetRegion(ByVal saveflag As Byte, ByVal region As Byte) As Integer
		End Function

'''        ********************************************************************************************************
'''        * 功能：获取区域设置
'''        * 输出：region -- 0x01(China1),0x02(China2),0x04(Europe),0x08(USA),0x16(Korea),0x32(Japan)
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetRegion(ByRef region As Byte) As Integer
		End Function

'''        ********************************************************************************************************
'''        * 功能：获取当前温度
'''        * 输出：temperature -- 整型
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetTemperature(ByRef temperature As Integer) As Integer
		End Function

'''        ********************************************************************************************************
'''        * 功能：设置温度保护
'''        * 输入：flag -- 1:温度保护， 0：无温度保护
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFSetTemperatureProtect(ByVal flag As Byte) As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：获取温度保护
'''        * 输出：flag -- 1:温度保护， 0：无温度保护
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetTemperatureProtect(ByRef flag As Byte) As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：设置天线工作时间
'''        * 输入：antnum -- 天线号
'''        * 输入：saveflag -- 1:掉电保存， 0：不保存
'''        * 输入：WorkTime -- 工作时间 ，单位ms, 范围 10-65535ms
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFSetANTWorkTime(ByVal antnum As Byte, ByVal saveflag As Byte, ByVal WorkTime As Integer) As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：获取天线工作时间
'''        * 输入：antnum -- 天线号
'''        * 输出：WorkTime -- 工作时间 ，单位ms, 范围 10-65535ms
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetANTWorkTime(ByVal antnum As Byte, ByRef WorkTime As Integer) As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：设置链路组合
'''        * 输入：saveflag -- 1:掉电保存， 0：不保存
'''        * 输入：mode --  0:DSB_ASK/FM0/40KHZ , 1:PR_ASK/Miller4/250KHZ , 2:PR_ASK/Miller4/300KHZ, 3:DSB_ASK/FM0/400KHZ
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFSetRFLink(ByVal saveflag As Byte, ByVal mode As Byte) As Integer
		End Function

'''        ********************************************************************************************************
'''        * 功能：获取链路组合
'''        * 输出：mode --  0:DSB_ASK/FM0/40KHZ , 1:PR_ASK/Miller4/250KHZ , 2:PR_ASK/Miller4/300KHZ, 3:DSB_ASK/FM0/400KHZ
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetRFLink(ByRef uMode As Byte) As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：设置FastID功能
'''        * 输入：flag -- 1:开启， 0：关闭
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFSetFastID(ByVal flag As Byte) As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：获取FastID功能
'''        * 输出：flag -- 1:开启， 0：关闭
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetFastID(ByRef flag As Byte) As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：设置Tagfocus功能
'''        * 输入：flag -- 1:开启， 0：关闭
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFSetTagfocus(ByVal flag As Byte) As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：获取Tagfocus功能
'''        * 输出：flag -- 1:开启， 0：关闭
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetTagfocus(ByRef flag As Byte) As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：设置软件复位
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFSetSoftReset() As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：设置Dual和Singel模式
'''        * 输入：saveflag -- 1:掉电保存， 0：不保存
'''        * 输入：mode -- 1:设置Singel模式， 0：设置Dual模式
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFSetDualSingelMode(ByVal saveflag As Byte, ByVal mode As Byte) As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：获取Dual和Singel模式
'''        * 输出：mode -- 1:设置Singel模式， 0：设置Dual模式
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetDualSingelMode(ByRef mode As Byte) As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：设置寻标签过滤设置
'''        * 输入：saveflag -- 1:掉电保存， 0：不保存
'''        * 输入：bank --  0x01:EPC , 0x02:TID, 0x03:USR
'''        * 输入：startaddr 起始地址，单位：字节
'''        * 输入：datalen 数据长度， 单位:字节
'''        * 输入：databuf 数据
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFSetFilter(ByVal saveflag As Byte, ByVal bank As Byte, ByVal startaddr As Integer, ByVal datalen As Integer, ByVal databuf() As Byte) As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：设置EPC和TID模式
'''        * 输入：saveflag -- 1:掉电保存， 0：不保存
'''        * 输入：mode -- 1：开启EPC和TID， 0:关闭
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFSetEPCTIDMode(ByVal saveflag As Byte, ByVal mode As Byte) As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：获取EPC和TID模式
'''        * 输出：mode -- 1：开启EPC和TID， 0:关闭
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetEPCTIDMode(ByRef mode As Byte) As Integer
		End Function

'''        ********************************************************************************************************
'''       * 功能：恢复出厂设置
'''       ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFSetDefaultMode() As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：单次盘存标签
'''        * 输出：uLenUii -- UII长度
'''        * 输出：uUii -- UII数据
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFInventorySingle(ByRef uLenUii As Byte, ByVal uUii() As Byte) As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：连续盘存标签
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFInventory() As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：停止盘存标签
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFStopGet() As Integer
		End Function
'''        ********************************************************************************************************
'''          * 功能：获取连续盘存标签数据
'''          * 输出：uLenUii -- UII长度
'''          * 输出：uUii -- UII数据
'''          ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHF_GetReceived_EX(ByRef uLenUii As Integer, ByVal uUii() As Byte) As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：读标签数据区
'''        * 输入：uAccessPwd -- 4字节密码
'''        * 输入：FilterBank -- 启动过滤的bank号， 1：EPC, 2:TID, 3:USR
'''        * 输入：FilterStartaddr -- 启动过滤的起始地址， 单位：字节
'''        * 输入：FilterLen -- 启动过滤的长度， 单位：字节
'''        * 输入：FilterData -- 启动过滤的数据
'''        * 输入：uBank -- 读取数据的bank
'''        * 输入：uPtr --  读取数据的起始地址， 单位：字
'''        * 输入：uCnt --  读取数据的长度， 单位：字
'''        * 输出：uReadDatabuf --  读取到的数据
'''        * 输出：uReadDataLen --  读取到的数据长度
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFReadData(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal uBank As Byte, ByVal uPtr As Integer, ByVal uCnt As Integer, ByVal uReadDatabuf() As Byte, ByRef uReadDataLen As Integer) As Integer
		End Function

'''        ********************************************************************************************************
'''          * 功能：写标签数据区
'''          * 输入：uAccessPwd -- 4字节密码
'''          * 输入：FilterBank -- 启动过滤的bank号， 1：EPC, 2:TID, 3:USR
'''          * 输入：FilterStartaddr -- 启动过滤的起始地址， 单位：bit
'''          * 输入：FilterLen -- 启动过滤的长度， 单位：bit
'''          * 输入：FilterData -- 启动过滤的数据
'''          * 输入：uBank -- 写入数据的bank
'''          * 输入：uPtr --  写入数据的起始地址， 单位：字
'''          * 输入：uCnt --  写入数据的长度， 单位：字
'''          * 输入：uWriteDatabuf --  写入的数据
'''          ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFWriteData(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal uBank As Byte, ByVal uPtr As Integer, ByVal uCnt As Byte, ByVal uDatabuf() As Byte) As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：LOCK标签
'''        * 输入：uAccessPwd -- 4字节密码
'''        * 输入：FilterBank -- 启动过滤的bank号， 1：EPC, 2:TID, 3:USR
'''        * 输入：FilterStartaddr -- 启动过滤的起始地址， 单位：字节
'''        * 输入：FilterLen -- 启动过滤的长度， 单位：字节
'''        * 输入：FilterData -- 启动过滤的数据
'''        * 输入：lockbuf --  3字节，第0-9位为Action位， 第10-19位为Mask位
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFLockTag(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal lockbuf() As Byte) As Integer
		End Function

'''        ********************************************************************************************************
'''        * 功能：KILL标签
'''        * 输入：uAccessPwd -- 4字节密码
'''        * 输入：FilterBank -- 启动过滤的bank号， 1：EPC, 2:TID, 3:USR
'''        * 输入：FilterStartaddr -- 启动过滤的起始地址， 单位：字节
'''        * 输入：FilterLen -- 启动过滤的长度， 单位：字节
'''        * 输入：FilterData -- 启动过滤的数据
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFKillTag(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte) As Integer
		End Function

'''        ********************************************************************************************************
'''          * 功能：BlockWrite 特定长度的数据到标签的特定地址
'''          * 输入：uAccessPwd -- 4字节密码
'''          * 输入：FilterBank -- 启动过滤的bank号， 1：EPC, 2:TID, 3:USR
'''          * 输入：FilterStartaddr -- 启动过滤的起始地址， 单位：字节
'''          * 输入：FilterLen -- 启动过滤的长度， 单位：字节
'''          * 输入：FilterData -- 启动过滤的数据
'''          * 输入：uBank -- 块号  1：EPC, 2:TID, 3:USR
'''          * 输入：uPtr --  写入数据的起始地址， 单位：字
'''          * 输入：uCnt --   写入数据的长度， 单位：字
'''          * 输入：uWriteDatabuf --  写入的数据
'''          ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFBlockWriteData(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal uBank As Byte, ByVal uPtr As Integer, ByVal uCnt As Integer, ByVal uDatabuf() As Byte) As Integer
		End Function

'''        ********************************************************************************************************
'''        * 功能：BlockErase 特定长度的数据到标签的特定地址
'''        * 输入：uAccessPwd -- 4字节密码
'''        * 输入：FilterBank -- 启动过滤的bank号， 1：EPC, 2:TID, 3:USR
'''        * 输入：FilterStartaddr -- 启动过滤的起始地址， 单位：字节
'''        * 输入：FilterLen -- 启动过滤的长度， 单位：字节
'''        * 输入：FilterData -- 启动过滤的数据
'''        * 输入：uBank -- 块号  1：EPC, 2:TID, 3:USR
'''        * 输入：uPtr --  读取数据的起始地址， 单位：字
'''        * 输入：uCnt --  读取数据的长度， 单位：字
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFBlockEraseData(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal uBank As Byte, ByVal uPtr As Integer, ByVal uCnt As Byte) As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：设置QT命令参数
'''        * 输入：uAccessPwd -- 4字节密码
'''        * 输入：FilterBank -- 启动过滤的bank号， 1：EPC, 2:TID, 3:USR
'''        * 输入：FilterStartaddr -- 启动过滤的起始地址， 单位：字节
'''        * 输入：FilterLen -- 启动过滤的长度， 单位：字节
'''        * 输入：FilterData -- 启动过滤的数据
'''        * 输入：QTData --  bit0：（0：表示无近距离控制 ， 1：表示启用近距离控制）  bit1：(0:表示启用private Memory map, 1：启用public memory map)
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFSetQT(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal QTData As Byte) As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：获取QT命令参数
'''        * 输入：uAccessPwd -- 4字节密码
'''        * 输入：FilterBank -- 启动过滤的bank号， 1：EPC, 2:TID, 3:USR
'''        * 输入：FilterStartaddr -- 启动过滤的起始地址， 单位：字节
'''        * 输入：FilterLen -- 启动过滤的长度， 单位：字节
'''        * 输入：FilterData -- 启动过滤的数据
'''        * 输出：QTData --  bit0：（0：表示无近距离控制 ， 1：表示启用近距离控制）  bit1：(0:表示启用private Memory map, 1：启用public memory map)
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetQT(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByRef QTData As Byte) As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：QT标签读操作
'''        * 输入：uAccessPwd -- 4字节密码
'''        * 输入：FilterBank -- 启动过滤的bank号， 1：EPC, 2:TID, 3:USR
'''        * 输入：FilterStartaddr -- 启动过滤的起始地址， 单位：字节
'''        * 输入：FilterLen -- 启动过滤的长度， 单位：字节
'''        * 输入：FilterData -- 启动过滤的数据
'''        * 输入：QTData --  bit0：（0：表示无近距离控制 ， 1：表示启用近距离控制）  
'''        * 输入：uBank -- 块号  1：EPC, 2:TID, 3:USR
'''        * 输入：uPtr --  读取数据的起始地址， 单位：字
'''        * 输入：uCnt --  读取数据的长度， 单位：字
'''        * 输出：uReadDatabuf --  读出的数据
'''        * 输出：uReadDataLen --  读出的数据长度
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFReadQT(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal QTData As Byte, ByVal uBank As Byte, ByVal uPtr As Integer, ByVal uCnt As Byte, ByVal uReadDatabuf() As Byte, ByRef uReadDataLen As Byte) As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：QT标签写操作
'''        * 输入：uAccessPwd -- 4字节密码
'''        * 输入：FilterBank -- 启动过滤的bank号， 1：EPC, 2:TID, 3:USR
'''        * 输入：FilterStartaddr -- 启动过滤的起始地址， 单位：字节
'''        * 输入：FilterLen -- 启动过滤的长度， 单位：字节
'''        * 输入：FilterData -- 启动过滤的数据
'''        * 输入：QTData --  bit0：（0：表示无近距离控制 ， 1：表示启用近距离控制）  
'''        * 输入：uBank -- 块号  1：EPC, 2:TID, 3:USR
'''        * 输入：uPtr --  读取数据的起始地址， 单位：字
'''        * 输入：uCnt --  读取数据的长度， 单位：字
'''        * 输入：uWriteDatabuf --  写入的数据
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFWriteQT(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal QTData As Byte, ByVal uBank As Byte, ByVal uPtr As Integer, ByVal uCnt As Byte, ByVal uDatabuf() As Byte) As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：Block Permalock操作
'''        * 输入：uAccessPwd -- 4字节密码
'''        * 输入：FilterBank -- 启动过滤的bank号， 1：EPC, 2:TID, 3:USR
'''        * 输入：FilterStartaddr -- 启动过滤的起始地址， 单位：字节
'''        * 输入：FilterLen -- 启动过滤的长度， 单位：字节
'''        * 输入：FilterData -- 启动过滤的数据
'''        * 输入：ReadLock --  bit0：（0：表示Read ， 1：表示Permalock）  
'''        * 输入：uBank -- 块号  1：EPC, 2:TID, 3:USR
'''        * 输入：uPtr --  Block起始地址 ，单位为16个块
'''        * 输入：uRange --  Block范围，单位为16个块
'''        * 输入：uMaskbuf -- 块掩码数据，2个字节，16bit 对应16个块
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFBlockPermalock(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal ReadLock As Byte, ByVal uBank As Byte, ByVal uPtr As Integer, ByVal uRange As Byte, ByVal uMaskbuf() As Byte) As Integer
		End Function

'''        ********************************************************************************************************
'''        * 功能：激活或失效EM4124标签
'''        * 输入：cmd --整形
'''        * 输入：uAccessPwd -- 4字节密码
'''        * 输入：FilterBank -- 启动过滤的bank号， 1：EPC, 2:TID, 3:USR
'''        * 输入：FilterStartaddr -- 启动过滤的起始地址， 单位：字节
'''        * 输入：FilterData -- 启动过滤的数据
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFDeactivate(ByVal cmd As Integer, ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte) As Integer
		End Function

'''        ********************************************************************************************************
'''        * 功能：获取协议类型  
'''        * 输出：type  0x00 表示 ISO18000-6C 协议,    0x01 表示 GB/T 29768 国标协议,      0x02 表示 GJB 7377.1 国军标协议
'''
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetProtocolType(ByVal type() As Byte) As Integer
		End Function


'''        ********************************************************************************************************
'''        * 功能：设置协议类型
'''        * 输入：type  0x00 表示 ISO18000-6C 协议,0x01 表示 GB/T 29768 国标协议,0x02 表示 GJB 7377.1 国军标协议
'''        * 
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFSetProtocolType(ByVal type As Byte) As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：国标LOCK标签
'''        * 输入：uAccessPwd -- 4字节密码
'''        * 输入：FilterBank -- 启动过滤的bank号， 1：EPC, 2:TID, 3:USR
'''        * 输入：FilterStartaddr -- 启动过滤的起始地址， 单位：字节
'''        * 输入：FilterLen -- 启动过滤的长度， 单位：字节
'''        * 输入：FilterData -- 启动过滤的数据
'''
'''        * 输入：memory 存储区：  0x00 表示标签信息区,   0x10 表示编码区,   0x20 表示安全区,   0x3x 表示用户区 0x30-0x3F 表示用户区编号 0 到编号 15
'''                config 配置：    0x00 表示配置存储区属性,    0x01 表示配置安全模式
'''
'''
'''		        action:  
'''
'''                      配置存储区属性:  0x00:可读可写,  0x01:可读不可写,  0x02:不可读可写,  0x03:不可读不可写
'''
'''			          配置安全模式:    0x00:保留,   0x01:不需要鉴别,   0x02:需要鉴别,不需要安全通信,   0x03:需要鉴别,需要安全通信
'''
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGBTagLock(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal memory As Byte, ByVal config As Byte, ByVal action As Byte) As Integer
		End Function



'''        ********************************************************************************************************
'''         * 功能：获取继电器和 IO 控制输出设置状态
'''         * 输入：outData[0]:    0:低电平   1：高电平
'''                 outData[1]:    0:低电平   1：高电平
'''           返回值：2：数据长度    -1：获取失败
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetIOControl(ByVal inputData() As Byte) As Integer
		End Function

'''        ********************************************************************************************************
'''        * 功能：继电器和 IO 控制输出设置
'''        * 输入：output1:    0:低电平   1：高电平
'''                output2:    0:低电平   1：高电平
'''		        outStatus： 0：断开    1：闭合
'''          返回值：0：设置成功     -1：设置失败
'''        * 
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFSetIOControl(ByVal output1 As Byte, ByVal output2 As Byte, ByVal outStatus As Byte) As Integer
		End Function



		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFSetOutputIO(ByVal output() As Byte, ByVal outputLen As Byte) As Integer
		End Function

		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetIOStatus(ByVal statusData() As Byte, ByVal dataLen() As Integer) As Integer
		End Function



		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFBuildDateTime(ByVal build_date() As Byte, ByVal build_time() As Byte) As Integer
		End Function

		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetVersionCode(ByVal datetime() As Byte) As Integer
		End Function


'''        ********************************************************************************************************
'''        * 功能：设置连续寻卡工作及等待时间
'''        * 输入：DByte4 为掉电保存标志，0 表示不保存，1 表示保存；DByte3、DByte2 为工作时间，高字节在前，DByte1、DByte0 为等待时间，高字节在前
'''
'''
'''          返回值：0：设置成功     -1：设置失败
'''
'''        * 
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFSetWorkTime(ByVal save As Byte, ByVal work1 As Byte, ByVal work2 As Byte, ByVal wait1 As Byte, ByVal wait2 As Byte) As Integer
		End Function

'''        ********************************************************************************************************
'''        * 功能：获取连续寻卡工作及等待时间
'''        * 输出：DByte[0]、DByte[1] 表示工作时间；DByte[2]、DByte[3] 表示等待时间，单位为 ms，高位在前，最大 65535ms
'''
'''          返回值：4：数据长度    -1：获取失败
'''        * 
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetWorkTime(ByVal data() As Byte) As Integer
		End Function



'''        ********************************************************************************************************
'''        * 功能：设置EPC TID USER模式
'''
'''        * 输入：saveflag -- 1:掉电保存， 0：不保存
'''
'''        * 输入：Memory 0x00，表示关闭； 0x01，表示开启EPC+TID模式（默认 地址为 0x00, 长 度 为 6 个 字 ） ； 0x02, 表 示 开 启EPC+TID+USER模式
'''
'''        * 输入：address 为USER区的起始地址（单位为字）
'''        * 输入：为USER区的长度（单位为字）
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFSetEPCTIDUSERMode(ByVal saveflag As Byte, ByVal memory As Byte, ByVal address As Byte, ByVal lenth As Byte) As Integer
		End Function
'''        ********************************************************************************************************
'''        * 功能：获取EPC TID USER 模式
'''        * 输入：rev1 :保留数据，传入0
'''        * 输入：rev2 :保留数据，传入0
'''        * 输出：mode[0]，Memory 0x00，表示关闭； 0x01，表示开启EPC+TID模式（默认 地址为 0x00, 长 度 为 6 个 字 ） ； 0x02, 表 示 开 启EPC+TID+USER模式
'''        * 输出：mode[1]address 为USER区的起始地址（单位为字）
'''        * 输出：mode[2]为USER区的长度（单位为字）
'''        * 返回值：3：正确，其它：错误
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetEPCTIDUSERMode(ByVal rev1 As Byte, ByVal rev2 As Byte, ByVal mode() As Byte) As Integer
		End Function





'        
'        初始化温度标签
'        return: 0--success, -1--unknow error, others--error code
'        mask_bank：掩码的数据区(0x00 为 Reserve 0x01 为 EPC， 0x02 表示 TID， 0x03 表示USR)。
'        mask_addr：为掩码的地址(bit为单位)，高字节在前。
'        mask_len：为掩码的长度(bit为单位)，高字节在前。
'        mask_data：为掩码数据，mask_len为0时，这里没有数据
'        

		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFInitRegFile(ByVal mask_bank As Byte, ByVal mask_addr As Integer, ByVal mask_len As Integer, ByVal mask_data() As Byte) As Integer
		End Function

'        
'        读取标签温度
'        return: 0--success, -1--unknow error, others--error code
'        mask_bank：掩码的数据区(0x00 为 Reserve 0x01 为 EPC， 0x02 表示 TID， 0x03 表示USR)。
'        mask_addr：为掩码的地址(bit为单位)，高字节在前。
'        mask_len：为掩码的长度(bit为单位)，高字节在前。
'        mask_data：为掩码数据，mask_len为0时，这里没有数据
'        temp:output,the point of temperature
'        
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFReadTagTemp(ByVal mask_bank As Byte, ByVal mask_addr As Integer, ByVal mask_len As Integer, ByVal mask_data() As Byte, ByVal outtemp() As Single) As Integer
		End Function

		'level:0-close log output, 3-base log,4-detail log
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function SetLogLevel(ByVal level As Integer) As Integer
		End Function


'''        ********************************************************************************************************
'''        * 功能：设置是否保存传输过程中的日志文件，默认不保存
'''        * 输入： 
'''        *save -- 0-不保存、1-保存日志文件
'''        *返回值：无 
'''        ********************************************************************************************************
		 <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		 Private Shared Function SaveLogFile(ByVal lsaveevel As Integer) As Integer
		 End Function



		' zjx 20191127 UHF升级--- start ---
'        
'            flag: 0,upgrade reader application
'	              1,upgrade UHF module
'	              2,upgrade reader bootloader 
'	              3,upgrade Ex10 SDK firmware
'            
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFJump2Boot(ByVal flag As Byte) As Integer
		End Function

		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFStartUpd() As Integer
		End Function

		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFUpdating(ByVal buf() As Byte) As Integer
		End Function

		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHF_Upgrade(ByVal buf() As Byte, ByVal length As Integer) As Integer
		End Function

		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFStopUpdate() As Integer
		End Function



'''        ********************************************************************************************************
'''* 功能：获取读写器软件版本号
'''* 输出：version[0]--版本号长度 ,  version[1--x]--版本号
'''********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetReaderVersion(ByVal version() As Byte) As Integer
		End Function


		' zjx 20191127 UHF升级--- end ---


		'''**************************  zjx 20200416 触发工作模式参数设置获取  -------- start -------- *************************
'''        ********************************************************************************************************
'''        * 功能：设置触发工作模式参数
'''        * 输入：
'''                para[0],	     触发IO：0x00表示触发输入1；0x01表示触发输入2。
'''                para[1],para[2]  触发工作时间：表示有触发输入时读卡工作时间，单位是10ms，高字节先，低字节后。
'''                para[3],para[4]	触发时间间隔：表示触发工作时间结束后，间隔多久再判断触发输入IO口进行触发。
'''                para[5]     	标签输出方式：0x00表示串口输出，0x01表示UDP输出
'''        * 
'''        * 返回值：   0:成功    其它：失败
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFSetWorkModePara(ByVal para() As Byte) As Integer
		End Function


'''        ********************************************************************************************************
'''        * 功能：获取触发工作模式参数
'''        * 输出：
'''                para[0],	     触发IO：0x00表示触发输入1；0x01表示触发输入2。
'''                para[1],para[2]  触发工作时间：表示有触发输入时读卡工作时间，单位是10ms，高字节先，低字节后。
'''                para[3],para[4]	 触发时间间隔：表示触发工作时间结束后，间隔多久再判断触发输入IO口进行触发。
'''                para[5]     	 标签输出方式：0x00表示串口输出，0x01表示UDP输出
'''        * 
'''        * 返回值：   0:成功    其它：失败
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetWorkModePara(ByVal para() As Byte) As Integer
		End Function


		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UsbOpen() As Integer
		End Function
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Sub UsbClose()
		End Sub

		'''**************************  zjx 20200416 触发工作模式参数设置获取   -------- end -------- *************************




		'''************************************************************************************
		'获取当前连接的ip信息
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Public Shared Function LinkGetInfo(ByVal info() As Byte, ByVal len As Integer) As Integer
		End Function

		'选择要操作的id，根据LinkGetInfo获取id信息
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Public Shared Function LinkSelect(ByVal id As Integer) As Integer
		End Function

		'获取当前可以操作的id
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Public Shared Function LinkGetSelected() As Integer
		End Function

		'断开所以连接
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Public Shared Sub LinkCloseAll()
		End Sub


'''        ********************************************************************************************************
'''        * function:get status of antennas linked
'''        * out:link_status,status of antenna linked,bit0~bit15 indicate antenna1~antenna16,bit 0/not link 1/linked
'''        * return：0：success    -1：failure
'''        ********************************************************************************************************
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Public Shared Function UHFGetAntennaLinkStatus(ByVal link_status() As Short) As Integer
		End Function

		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Public Shared Function UHFVerifyVoltage(ByVal value() As Integer) As Integer
		End Function




'        
'           按块写无源电子标签带水墨屏显示
'           pwd：4 个字节的块写密码
'           sector：掩码的数据区(0x00 为 Reserve，0x01 为 EPC，0x02 表示 TID，0x03 表示 USR)。
'           mask_addr：为掩码的地址。
'           mask_len：为掩码的长度。
'           mask_data：为掩码数据。
'           w_addr：为写入数据区的地址（单位是字）。
'           w_len：为写入的数据长度（单位是字）。
'           w_data：为写入的具体数据（txt 文件中的数据）。
'           
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Public Shared Function UHFWriteScreenBlockData(ByVal pwd() As Byte, ByVal sector As Byte, ByVal mask_addr As Short, ByVal mask_len As Short, ByVal mask_data() As Byte, ByVal type As Byte, ByVal w_addr As Short, ByVal w_len As Short, ByVal w_data() As Byte) As Integer
		End Function



		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Public Shared Function UHFUploadUserParam(ByVal param() As Byte, ByVal paramLen As Short) As Integer
		End Function

		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Public Shared Function UHFDownloadUserParam(ByVal param() As Byte, ByVal paramLen() As Short) As Integer
		End Function


		'return 0,no data, > 0 tag length, < 0 error code
		'tdata tag data, type+length+content+...+type+length+content
		'type:1-epc,2-tid,3-user,4-rssi,5-antenna,6-id
		'
		' #define CONTENT_TYPE_INVALID        0
		' #define CONTENT_TYPE_EPC            1
		' #define CONTENT_TYPE_TID            2
		' #define CONTENT_TYPE_USER           3
		' #define CONTENT_TYPE_RSSI           4
		' #define CONTENT_TYPE_ANT            5
		' #define CONTENT_TYPE_ID             6
		' #define CONTENT_TYPE_IP             7
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFGetTagData(ByVal tdata() As Byte, ByVal recvlen As Integer) As Integer
		End Function


		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFInventorySingle(ByVal id As Integer) As Integer
		End Function

		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFStopSingle(ByVal id As Integer) As Integer
		End Function

		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFInventoryById(ByVal id As Integer) As Integer
		End Function

		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Private Shared Function UHFStopById(ByVal id As Integer) As Integer
		End Function



   '   typedef enum{CELL_INVALID=0, CELL_CONNECT_ID=1, CELL_CONNECT_IP, CELL_UHF_PC, CELL_UHF_RSSI, CELL_UHF_ANTENNA, CELL_UHF_EPC, CELL_UHF_TID, CELL_UHF_USER,CELL_UHF_RESERVE,CELL_BARCODE, CELL_UHF_SENSOR} CELL_DATA_TYPE;









		<UnmanagedFunctionPointer(CallingConvention.Cdecl)>
		Public Delegate Sub OnDataReceived(ByVal epc As IntPtr, ByVal recvLen As Short) '[MarshalAs(UnmanagedType.LPArray, SizeConst = 4096)]


		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Public Shared Sub setOnDataReceived(ByVal onDataRecved As OnDataReceived)
		End Sub

		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
		Public Shared Function PrintTextToCursor(ByVal codeType As Integer, ByVal text() As Byte, ByVal len As Short) As Integer
		End Function

		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.StdCall)>
		Public Shared Function BindUDP(ByVal bindport As Integer) As Integer
		End Function
		<DllImport("UHFAPI.dll", CallingConvention := CallingConvention.StdCall)>
		Public Shared Sub UnbindUDP()
		End Sub

'typedef enum {CHAR_CODE_ASCII=1, CHAR_CODE_GB2312=2, CHAR_CODE_GBK=3, CHAR_CODE_BIG5=4, CHAR_CODE_UTF8=5}ENUM_CHAR_CODE_TYPE;


'extern "C" UHFAPI_API int PrintTextToCursor(ENUM_CHAR_CODE_TYPE type, const char *text, unsigned short len);

		Private Shared Sub recvDataCallback(ByVal epc As IntPtr, ByVal recvLen As Short)
			Console.WriteLine("entry call back")
		End Sub

		Private Shared uhf As UHFAPI = Nothing

		Friend Sub New()
		End Sub

		Public Shared Function getInstance() As UHFAPI
			If uhf Is Nothing Then
				uhf = New UHFAPI()
			End If
			Return uhf
		End Function

#Region "usb"
		Public Function OpenUsb() As Boolean Implements IUHF.OpenUsb
			Dim result As Integer = UsbOpen()
			If result = 0 Then
				Return True
			End If
			Return False
		End Function
		Public Sub CloseUsb() Implements IUHF.CloseUsb
			UsbClose()
		End Sub
		#End Region


		#Region "协议"
		Public Function SetProtocol(ByVal type As Byte) As Boolean Implements IUHF.SetProtocol
			If UHFSetProtocolType(type) = 0 Then
				Return True
			End If
			Return False
		End Function
		Public Function GetProtocol() As Integer Implements IUHF.GetProtocol
			Dim type(0) As Byte
			If UHFGetProtocolType(type) = 0 Then
				Return type(0)
			End If
			Return -1
		End Function
		#End Region


		#Region " 国标标签Lock"



		''' 
		''' <summary>
		''' 国标标签Lock
		''' </summary>
		''' <param name="uAccessPwd">4字节密码</param>
		''' <param name="FilterBank">启动过滤的bank号， 1：EPC, 2:TID, 3:USR</param>
		''' <param name="FilterStartaddr">启动过滤的起始地址， 单位：bit</param>
		''' <param name="FilterLen">启动过滤的长度， 单位：字节</param>
		''' <param name="FilterData">启动过滤的数据</param>
		''' <param name="memory"></param>
		''' <param name="config"></param>
		''' <param name="action"></param>
		''' <returns></returns>
		Public Function GBTagLock(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal memory As Byte, ByVal config As Byte, ByVal action As Byte) As Boolean Implements IUHF.GBTagLock
			If UHFGBTagLock(uAccessPwd, FilterBank, FilterStartaddr, FilterLen, FilterData, memory, config, action) = 0 Then
				Return True
			End If
			Return False
		End Function

		#End Region



		#Region "TCPIP"
		''' <summary>
		''' 连接主机
		''' </summary>
		''' <param name="ip">ip地址</param>
		''' <param name="port">端口</param>
		''' <returns>返回true表示成功，返回false表示失败</returns>
		Public Function TcpConnect(ByVal ip As String, ByVal port As UInteger) As Boolean

			If ip Is Nothing OrElse ip = "" Then
				Return False
			End If
			ip = ip.Trim()

			If Not StringUtils.isIP(ip) Then
				Return False
			End If
			Dim bIp As New StringBuilder()
			bIp.Append(ip)

			Console.WriteLine("TCPConnect(" & ip & ", " & port & ")")
			Dim result As Integer = TCPConnect(bIp, port)
			If result = 0 Then
				Return True
			End If
			Return False
		End Function
		''' <summary>
		''' 断开主机
		''' </summary>
		''' <returns></returns>
		Public Sub TcpDisconnect2()
			TCPDisconnect()
		End Sub


		'设置蜂鸣器 工作模式：0x00表示关闭蜂鸣器；0x01表示打开蜂鸣器
		Public Function UHFSetBuzzer(ByVal mode As Byte) As Boolean Implements IUHF.UHFSetBuzzer
			If UHFSetBeep(mode) = 0 Then
				Return True
			Else
				Return False
			End If

		End Function
		'获取蜂鸣器 工作模式：0x00表示关闭蜂鸣器；0x01表示打开蜂鸣器
		Public Function UHFGetBuzzer(ByVal mode() As Byte) As Boolean Implements IUHF.UHFGetBuzzer
			If UHFGetBeep(mode) = 0 Then
				Return True
			Else
				Return False
			End If

		End Function

		Public Function SetLocalIP(ByVal ip As String, ByVal port As Integer, ByVal mask As String, ByVal gate As String) As Boolean Implements IUHF.SetLocalIP

			If Not StringUtils.isIP(ip) Then
				Return False
			End If
			If Not StringUtils.isIP(mask) Then
				Return False
			End If
			If Not StringUtils.isIP(gate) Then
				Return False
			End If
			Dim bPort(1) As Byte
			Dim bIP(3) As Byte
			Dim bmask(3) As Byte
			Dim bgate(3) As Byte

			Dim hexPort As String = DataConvert.DecimalToHexString(port)
			bPort = DataConvert.HexStringToByteArray("0000".Substring(0, 4 - hexPort.Length) & hexPort)

			Dim strIp() As String = ip.Split("."c)
			For k As Integer = 0 To strIp.Length - 1
				bIP(k) = Byte.Parse(strIp(k))
			Next k

			Dim temp() As String = mask.Split("."c)
			For k As Integer = 0 To temp.Length - 1
				bmask(k) = Byte.Parse(temp(k))
			Next k

			temp = gate.Split("."c)
			For k As Integer = 0 To temp.Length - 1
				bgate(k) = Byte.Parse(temp(k))
			Next k



			If UHFSetIp(bIP, bPort, bmask, bgate) = 0 Then ', bmask, bgate
				Return True
			Else
				Return False
			End If
		End Function

		Public Function GetLocalIP(ByVal ip As StringBuilder, ByVal port As StringBuilder, ByVal mask As StringBuilder, ByVal gate As StringBuilder) As Boolean Implements IUHF.GetLocalIP
			Dim sIP(3) As Byte
			Dim sPort(1) As Byte

			Dim sMask(3) As Byte
			Dim sGate(3) As Byte
			Dim startTime As Integer = Environment.TickCount
			If UHFGetIp(sIP, sPort, sMask, sGate) = 0 Then
				' MessageBoxEx.Show((Environment.TickCount-startTime)+"");


				If ip IsNot Nothing Then
					ip.Append(sIP(0))
					ip.Append(".")
					ip.Append(sIP(1))
					ip.Append(".")
					ip.Append(sIP(2))
					ip.Append(".")
					ip.Append(sIP(3))
				End If
				If port IsNot Nothing Then
					Dim hexPort As String = DataConvert.ByteArrayToHexString(sPort).Replace(" ", "")
					Dim iPort As Integer = Convert.ToInt32(hexPort, 16)
					port.Append(iPort)
				End If

				If sMask IsNot Nothing Then
					If sMask(0) = 0 AndAlso sMask(1) = 0 AndAlso sMask(2) = 0 AndAlso sMask(3) = 0 Then
						sMask(0) = 255
						sMask(1) = 255
						sMask(2) = 255
						sMask(3) = 0
					End If
					mask.Append(sMask(0))
					mask.Append(".")
					mask.Append(sMask(1))
					mask.Append(".")
					mask.Append(sMask(2))
					mask.Append(".")
					mask.Append(sMask(3))
				End If


				If sGate IsNot Nothing Then
					If sGate(0) = 0 AndAlso sGate(1) = 0 AndAlso sGate(2) = 0 AndAlso sGate(3) = 0 Then
						sGate(0) = sIP(0)
						sGate(1) = sIP(1)
						sGate(2) = sIP(2)
						sGate(3) = 1
					End If
					gate.Append(sGate(0))
					gate.Append(".")
					gate.Append(sGate(1))
					gate.Append(".")
					gate.Append(sGate(2))
					gate.Append(".")
					gate.Append(sGate(3))
				End If


				Return True
			Else
				Return False
			End If
		End Function



		Public Function SetDestIP(ByVal ip As String, ByVal port As Integer) As Boolean Implements IUHF.SetDestIP
			If ip Is Nothing OrElse ip = "" Then
				Return False
			End If
			ip = ip.Trim()

			If Not StringUtils.isIP(ip) Then
				Return False
			End If
			Dim bPort(1) As Byte
			Dim bIP(3) As Byte

			Dim hexPort As String = DataConvert.DecimalToHexString(port)
			bPort = DataConvert.HexStringToByteArray("0000".Substring(0, 4 - hexPort.Length) & hexPort)

			Dim strIp() As String = ip.Split("."c)
			For k As Integer = 0 To strIp.Length - 1
				bIP(k) = Byte.Parse(strIp(k))
			Next k


			If UHFSetDestIp(bIP, bPort) = 0 Then
				Return True
			Else
				Return False
			End If
		End Function

		Public Function GetDestIP(ByVal ip As StringBuilder, ByVal port As StringBuilder) As Boolean Implements IUHF.GetDestIP
			Dim sIP(3) As Byte
			Dim sPort(1) As Byte

			If UHFGetDestIp(sIP, sPort) = 0 Then
				If ip IsNot Nothing Then
					ip.Append(sIP(0))
					ip.Append(".")
					ip.Append(sIP(1))
					ip.Append(".")
					ip.Append(sIP(2))
					ip.Append(".")
					ip.Append(sIP(3))
				End If
				If port IsNot Nothing Then
					Dim hexPort As String = DataConvert.ByteArrayToHexString(sPort).Replace(" ", "")
					Dim iPort As Integer = Convert.ToInt32(hexPort, 16)
					port.Append(iPort)
				End If
				Return True
			Else
				Return False
			End If
		End Function


		#End Region

		#Region "串口、版本号、ID"
		''' <summary>
		''' 打开串口
		''' </summary>
		''' <param name="com">串口号：0,1,2....</param>
		''' <returns>返回0表示成功，返回1表示失败</returns>
		Public Function Open(ByVal comName As Integer) As Boolean
			Dim result As Integer = ComOpen(comName)
			If result = 0 Then
				Return True
			End If
			Return False
		End Function

		Public Function Open() As Boolean Implements IUHF.Open
			Return False
		End Function
		''' <summary>
		''' 关闭串口
		''' </summary>
		''' <returns></returns>
		Public Function Close() As Boolean Implements IUHF.Close
			ClosePort()
			Return True
		End Function




		''' <summary>
		''' 获取硬件版本
		''' </summary>
		''' <returns></returns>
		Public Function GetHardwareVersion() As String Implements IUHF.GetHardwareVersion
			Dim version(49) As Byte
			If UHFGetHardwareVersion(version) = 0 Then
				Dim len As Integer = version(0)
				Dim versionTemp(len - 1) As Byte
				Array.Copy(version, 1, versionTemp, 0, len)
				Return System.Text.Encoding.ASCII.GetString(versionTemp) ' DataConvert.ByteArrayToHexString(versionTemp);
			End If
			Return String.Empty
		End Function
		''' <summary>
		''' 获取软件版本
		''' </summary>
		''' <returns></returns>
		Public Function GetSoftwareVersion() As String Implements IUHF.GetSoftwareVersion
			Dim version(49) As Byte
			If UHFGetSoftwareVersion(version) = 0 Then
				Dim len As Integer = version(0)
				Dim versionTemp(len - 1) As Byte
				Array.Copy(version, 1, versionTemp, 0, len)
				Return System.Text.Encoding.ASCII.GetString(versionTemp) 'DataConvert.ByteArrayToHexString(versionTemp);
			End If
			Return String.Empty
		End Function
		Public Function GetSTM32Version() As String Implements IUHF.GetSTM32Version
			Dim version(49) As Byte
			If UHFGetReaderVersion(version) = 0 Then
				Dim len As Integer = version(0)
				Dim versionTemp(len - 1) As Byte
				Array.Copy(version, 1, versionTemp, 0, len)
				Return System.Text.Encoding.ASCII.GetString(versionTemp) 'DataConvert.ByteArrayToHexString(versionTemp);
			End If
			Return String.Empty
		End Function


		''' <summary>
		''' 获取设备ID
		''' </summary>
		''' <returns>id--整型ID号</returns>
		Public Function GetUHFGetDeviceID() As Integer Implements IUHF.GetUHFGetDeviceID
			Dim id As Integer = -1
			UHFGetDeviceID(id)
			Return id
		End Function
		#End Region

		#Region "频率、功率"
		''' <summary>
		''' 设置功率
		''' </summary>
		''' <param name="save">1:保存设置   0：不保存</param>
		''' <param name="uPower">功率（DB）</param>
		''' <returns></returns>
		Public Function SetPower(ByVal save As Byte, ByVal uPower As Byte) As Boolean Implements IUHF.SetPower
			If UHFSetPower(save, uPower) = 0 Then
				Return True
			End If
			Return False
		End Function
		''' <summary>
		''' 获取功率
		''' </summary>
		''' <param name="uPower">功率（DB）</param>
		''' <returns></returns>
		Public Function GetPower(ByRef uPower As Byte) As Boolean Implements IUHF.GetPower
			If UHFGetPower(uPower) = 0 Then
				Return True
			End If
			Return False
		End Function

		''' <summary>
		''' 设置功率
		''' </summary>
		''' <param name="save">1:保存设置   0：不保存</param>
		''' <param name="num">天线</param>
		''' <param name="uPower">功率（DB）</param>
		''' <returns></returns>
		Public Function SetAntennaPower(ByVal save As Byte, ByVal num As Byte, ByVal uPower As Byte) As Boolean
			If UHFSetAntennaPower(save, num, uPower, uPower) = 0 Then
				Return True
			End If
			Return False
		End Function
		''' <summary>
		''' 获取功率
		''' </summary>
		''' <param name="uPower">功率（DB）</param>
		''' <returns></returns>
		Public Function GetAntennaPower(ByVal uPower() As Byte) As Boolean
			Dim data(99) As Byte
			Dim resultLen(0) As Integer
			If UHFGetAntennaPower(data, resultLen) = 0 Then
				Dim k As Integer = 0
				Do While k < resultLen(0) \ 3
					uPower(k) = data(k * 3 + 1)
					k += 1
				Loop


				Return True
			End If
			Return False
		End Function

		''' <summary>
		''' 获取跳频
		''' </summary>
		''' <param name="Freqbuf">Freqbuf[0]--频点个数， Freqbuf[1]..[x]--频点数组（整型）</param>
		''' <returns></returns>
		Public Function GetJumpFrequency(ByRef Freqbuf() As Integer) As Boolean Implements IUHF.GetJumpFrequency
			Dim temp(511) As Integer
			If UHFGetJumpFrequency(temp) = 0 Then
				Dim len As Integer = temp(0)
				Dim freqData(len - 1) As Integer
				Array.Copy(temp, 1, freqData, 0, len)
				Freqbuf = freqData
				Return True
			End If
			Return False
		End Function
		''' <summary>
		''' 设置跳频
		''' </summary>
		''' <param name="nums">跳频个数</param>
		''' <param name="Freqbuf">Freqbuf--频点数组（整型） ，如920125，921250.....</param>
		''' <returns></returns>
		Public Function SetJumpFrequency(ByVal nums As Byte, ByVal Freqbuf() As Integer) As Boolean Implements IUHF.SetJumpFrequency
			If UHFSetJumpFrequency(nums, Freqbuf) = 0 Then
				Return True
			End If
			Return False
		End Function
		#End Region

		#Region "session"
		''' <summary>
		''' 设置session
		''' </summary>
		''' <param name="Target"></param>
		''' <param name="Action"></param>
		''' <param name="T"></param>
		''' <param name="Q"></param>
		''' <param name="StartQ"></param>
		''' <param name="MinQ"></param>
		''' <param name="MaxQ"></param>
		''' <param name="D"></param>
		''' <param name="C"></param>
		''' <param name="P"></param>
		''' <param name="Sel"></param>
		''' <param name="Session"></param>
		''' <param name="G"></param>
		''' <param name="LF"></param>
		''' <returns></returns>
		Public Function SetGen2(ByVal Target As Byte, ByVal Action As Byte, ByVal T As Byte, ByVal Q As Byte, ByVal StartQ As Byte, ByVal MinQ As Byte, ByVal MaxQ As Byte, ByVal D As Byte, ByVal C As Byte, ByVal P As Byte, ByVal Sel As Byte, ByVal Session As Byte, ByVal G As Byte, ByVal LF As Byte) As Boolean Implements IUHF.SetGen2
			If UHFSetGen2(Target, Action, T, Q, StartQ, MinQ, MaxQ, D, C, P, Sel, Session, G, LF) = 0 Then
				Return True
			End If
			Return False
		End Function
		''' <summary>
		''' 获取session
		''' </summary>
		''' <param name="Target"></param>
		''' <param name="Action"></param>
		''' <param name="T"></param>
		''' <param name="Q"></param>
		''' <param name="StartQ"></param>
		''' <param name="MinQ"></param>
		''' <param name="MaxQ"></param>
		''' <param name="D"></param>
		''' <param name="Coding"></param>
		''' <param name="P"></param>
		''' <param name="Sel"></param>
		''' <param name="Session"></param>
		''' <param name="G"></param>
		''' <param name="LF"></param>
		''' <returns></returns>
		Public Function GetGen2(ByRef Target As Byte, ByRef Action As Byte, ByRef T As Byte, ByRef Q As Byte, ByRef StartQ As Byte, ByRef MinQ As Byte, ByRef MaxQ As Byte, ByRef D As Byte, ByRef Coding As Byte, ByRef P As Byte, ByRef Sel As Byte, ByRef Session As Byte, ByRef G As Byte, ByRef LF As Byte) As Boolean Implements IUHF.GetGen2
			If UHFGetGen2(Target, Action, T, Q, StartQ, MinQ, MaxQ, D, Coding, P, Sel, Session, G, LF) = 0 Then
				Return True
			End If
			Return False
		End Function
		#End Region

		#Region "连续波、天线、区域、模块温度、温度保护"
		''' <summary>
		''' 设置CW
		''' </summary>
		''' <param name="flag">flag -- 1:开CW，  0：关CW</param>
		''' <returns></returns>
		Public Function SetCW(ByVal flag As Byte) As Boolean Implements IUHF.SetCW
			If UHFSetCW(flag) = 0 Then
				Return True
			End If
			Return False
		End Function
		''' <summary>
		''' 功能：获取CW
		''' </summary>
		''' <param name="flag">flag -- 1:开CW，  0：关CW</param>
		''' <returns></returns>
		Public Function GetCW(ByRef flag As Byte) As Boolean Implements IUHF.GetCW
			If UHFGetCW(flag) = 0 Then
				Return True
			End If
			Return False
		End Function
		''' <summary>
		''' 天线设置
		''' </summary>
		''' <param name="saveflag">saveflag -- 1:掉电保存，  0：不保存</param>
		''' <param name="buf">buf--2bytes, 共16bits, 每bit 置1选择对应天线</param>
		''' <returns></returns>
		Public Function SetANT(ByVal saveflag As Byte, ByVal buf() As Byte) As Boolean Implements IUHF.SetANT
			If UHFSetANT(saveflag, buf) = 0 Then
				Return True
			End If
			Return False
		End Function
		''' <summary>
		''' 获取天线设置
		''' </summary>
		''' <param name="buf">buf--2bytes, 共16bits</param>
		''' <returns></returns>
		Public Function GetANT(ByVal buf() As Byte) As Boolean Implements IUHF.GetANT
			If UHFGetANT(buf) = 0 Then
				Return True
			End If
			Return False
		End Function
		''' <summary>
		''' 获取天线设置
		''' </summary>
		''' <param name="buf">buf--2bytes, 共16bits</param>
		''' <returns></returns>
		Public Function GetANTLinkStatus(ByVal buf() As Short) As Boolean
			If UHFGetAntennaLinkStatus(buf) = 0 Then
				Return True
			End If
			Return False
		End Function
		''' <summary>
		''' 区域设置
		''' </summary>
		''' <param name="saveflag">1:掉电保存，  0：不保存</param>
		''' <param name="region">0x01(China1),0x02(China2),0x04(Europe),0x08(USA),0x16(Korea),0x32(Japan)</param>
		''' <returns></returns>
		Public Function SetRegion(ByVal saveflag As Byte, ByVal region As Byte) As Boolean Implements IUHF.SetRegion
			If UHFSetRegion(saveflag, region) = 0 Then
				Return True
			End If
			Return False
		End Function
		''' <summary>
		''' 获取区域设置
		''' </summary>
		''' <param name="region"> 0x01(China1),0x02(China2),0x04(Europe),0x08(USA),0x16(Korea),0x32(Japan)</param>
		''' <returns></returns>
		Public Function GetRegion(ByRef region As Byte) As Boolean Implements IUHF.GetRegion
			If UHFGetRegion(region) = 0 Then
				Return True
			End If
			Return False
		End Function
		''' <summary>
		''' 获取模块温度
		''' </summary>
		''' <param name="temperature">回传的温度</param>
		''' <returns>返回true表示获取成功，temperature参数可以使用。返回false表示获取失败，temperature参数不可以使用</returns>
		Public Function GetTemperature() As String Implements IUHF.GetTemperature
			Dim temperature As Integer = 0
			If UHFGetTemperature(temperature) = 0 Then
				Return temperature.ToString()
			End If
			Return String.Empty
		End Function
		''' <summary>
		''' 设置温度保护
		''' </summary>
		''' <param name="flag">1:温度保护， 0：无温度保护</param>
		''' <returns></returns>
		Public Function SetTemperatureProtect(ByVal flag As Byte) As Boolean Implements IUHF.SetTemperatureProtect
			If UHFSetTemperatureProtect(flag) = 0 Then
				Return True
			End If
			Return False
		End Function
		''' <summary>
		''' 获取温度保护
		''' </summary>
		''' <param name="flag">1:温度保护， 0：无温度保护</param>
		''' <returns></returns>
		Public Function GetTemperatureProtect(ByRef flag As Byte) As Boolean Implements IUHF.GetTemperatureProtect
			If UHFGetTemperatureProtect(flag) = 0 Then
				Return True
			End If
			Return False
		End Function
		''' <summary>
		''' 设置天线工作时间
		''' </summary>
		''' <param name="antnum">天线号</param>
		''' <param name="saveflag">1:掉电保存， 0：不保存</param>
		''' <param name="WorkTime">工作时间 ，单位ms, 范围 10-65535ms</param>
		''' <returns></returns>
		Public Function SetANTWorkTime(ByVal antnum As Byte, ByVal saveflag As Byte, ByVal WorkTime As Integer) As Boolean Implements IUHF.SetANTWorkTime
			If UHFSetANTWorkTime(antnum, saveflag, WorkTime) = 0 Then
				Return True
			End If
			Return False
		End Function
		''' <summary>
		''' 获取天线工作时间
		''' </summary>
		''' <param name="antnum">天线号</param>
		''' <param name="WorkTime">工作时间 ，单位ms, 范围 10-65535ms</param>
		''' <returns></returns>
		Public Function GetANTWorkTime(ByVal antnum As Byte, ByRef WorkTime As Integer) As Boolean Implements IUHF.GetANTWorkTime
			If UHFGetANTWorkTime(antnum, WorkTime) = 0 Then
				Return True
			End If
			Return False
		End Function
		#End Region

		#Region "链路组合、FastID、Tagfocus、Dual和Singel模式、软件复位、恢复出厂设置"

		''' <summary>
		''' 设置链路组合
		''' </summary>
		''' <param name="saveflag">1:掉电保存， 0：不保存</param>
		''' <param name="mode">0:DSB_ASK/FM0/40KHZ , 1:PR_ASK/Miller4/250KHZ , 2:PR_ASK/Miller4/300KHZ, 3:DSB_ASK/FM0/400KHZ</param>
		''' <returns></returns>
		Public Function SetRFLink(ByVal saveflag As Byte, ByVal mode As Byte) As Boolean Implements IUHF.SetRFLink

			If UHFSetRFLink(saveflag, mode) = 0 Then
				Return True
			End If
			Return False
		End Function
		''' <summary>
		''' 获取链路组合
		''' </summary>
		''' <param name="uMode">0:DSB_ASK/FM0/40KHZ , 1:PR_ASK/Miller4/250KHZ , 2:PR_ASK/Miller4/300KHZ, 3:DSB_ASK/FM0/400KHZ</param>
		''' <returns></returns>
		Public Function GetRFLink(ByRef uMode As Byte) As Boolean Implements IUHF.GetRFLink
			If UHFGetRFLink(uMode) = 0 Then
				Return True
			End If

			Return False
		End Function
		''' <summary>
		''' 设置FastID功能
		''' </summary>
		''' <param name="flag">1:开启， 0：关闭</param>
		''' <returns></returns>
		Public Function SetFastID(ByVal flag As Byte) As Boolean Implements IUHF.SetFastID
			If UHFSetFastID(flag) = 0 Then
				Return True
			End If
			Return False
		End Function
		''' <summary>
		''' 获取FastID功能
		''' </summary>
		''' <param name="flag">1:开启， 0：关闭</param>
		''' <returns></returns>
		Public Function GetFastID(ByRef flag As Byte) As Boolean Implements IUHF.GetFastID
			If UHFGetFastID(flag) = 0 Then
				Return True
			End If
			Return False

		End Function
		''' <summary>
		''' 设置Tagfocus功能
		''' </summary>
		''' <param name="flag">1:开启， 0：关闭</param>
		''' <returns></returns>
		Public Function SetTagfocus(ByVal flag As Byte) As Boolean Implements IUHF.SetTagfocus
			If UHFSetTagfocus(flag) = 0 Then
				Return True
			End If
			Return False
		End Function
		''' <summary>
		''' 获取Tagfocus功能
		''' </summary>
		''' <param name="flag">1:开启， 0：关闭</param>
		''' <returns></returns>
		Public Function GetTagfocus(ByRef flag As Byte) As Boolean Implements IUHF.GetTagfocus
			If UHFGetTagfocus(flag) = 0 Then
				Return True
			End If
			Return False
		End Function
		''' <summary>
		''' 设置软件复位
		''' </summary>
		''' <returns></returns>
		Public Function SetSoftReset() As Boolean Implements IUHF.SetSoftReset
			If UHFSetSoftReset() = 0 Then
				Return True
			End If
			Return False
		End Function
		''' <summary>
		''' 设置Dual和Singel模式
		''' </summary>
		''' <param name="saveflag">1:掉电保存， 0：不保存</param>
		''' <param name="mode">1:设置Singel模式， 0：设置Dual模式</param>
		''' <returns></returns>
		Public Function SetDualSingelMode(ByVal saveflag As Byte, ByVal mode As Byte) As Boolean Implements IUHF.SetDualSingelMode
			If UHFSetDualSingelMode(saveflag, mode) = 0 Then
				Return True
			End If
			Return False
		End Function
		''' <summary>
		''' 获取Dual和Singel模式
		''' </summary>
		''' <param name="mode">1:设置Singel模式， 0：设置Dual模式</param>
		''' <returns></returns>
		Public Function GetDualSingelMode(ByRef mode As Byte) As Boolean Implements IUHF.GetDualSingelMode
			If UHFGetDualSingelMode(mode) = 0 Then
				Return True
			End If
			Return False
		End Function


		''' <summary>
		''' 恢复出厂设置
		''' </summary>
		''' <returns></returns>
		Public Function SetDefaultMode() As Boolean Implements IUHF.SetDefaultMode
			If UHFSetDefaultMode() = 0 Then
				Return True
			End If
			Return False
		End Function
		#End Region

		#Region "读、写、锁、销毁"
		''' <summary>
		''' 读取数据
		''' </summary>
		''' <param name="uAccessPwd">4字节密码</param>
		''' <param name="FilterBank">启动过滤的bank号， 1：EPC, 2:TID, 3:USR</param>
		''' <param name="FilterStartaddr">启动过滤的起始地址， 单位：字节</param>
		''' <param name="FilterLen">启动过滤的长度， 单位：字节</param>
		''' <param name="FilterData">启动过滤的数据</param>
		''' <param name="uBank">读取数据的bank</param>
		''' <param name="uPtr">读取数据的起始地址， 单位：字</param>
		''' <param name="uCnt">读取数据的长度， 单位：字</param>
		''' <returns>返回十六进制数据，读取失败返回""</returns>
		Public Function ReadData(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal uBank As Byte, ByVal uPtr As Integer, ByVal uCnt As Integer) As String Implements IUHF.ReadData
			Try
				Dim uReadDatabuf(2047) As Byte

				Dim uReadDataLen As Integer = 0

				Dim sb As New StringBuilder()
				sb.Append(vbCrLf & "密码：" & DataConvert.ByteArrayToHexString(uAccessPwd))
				sb.Append(vbCrLf & "过滤数据块（ 1：EPC, 2:TID, 3:USR）：" & FilterBank)
				sb.Append(vbCrLf & "过滤起始地址：" & FilterStartaddr)
				sb.Append(vbCrLf & "过滤长度：" & FilterLen)
				sb.Append(vbCrLf & "过滤数据：" & DataConvert.ByteArrayToHexString(FilterData))
				sb.Append(vbCrLf)
				sb.Append(vbCrLf & "读取的数据块：" & uBank)
				sb.Append(vbCrLf & "读取的数据起始地址：" & uPtr)
				sb.Append(vbCrLf & "读取的数据长度：" & uCnt)
				sb.Append(vbCrLf)

				FileManage.WriterFile("C:\Users\Administrator\Desktop\UHFLog.txt", sb.ToString(), True)

				Dim result As Integer = UHFReadData(uAccessPwd, FilterBank, FilterStartaddr, FilterLen, FilterData, uBank, uPtr, uCnt, uReadDatabuf, uReadDataLen)
				If result = 0 Then
					Return DataConvert.ByteArrayToHexString(uReadDatabuf, uReadDataLen)
				End If
			Catch ex As Exception

			End Try

			Return String.Empty
		End Function


		''' <summary>
		''' 写标签数据区
		''' </summary>
		''' <param name="uAccessPwd">4字节密码</param>
		''' <param name="FilterBank">启动过滤的bank号， 1：EPC, 2:TID, 3:USR</param>
		''' <param name="FilterStartaddr">启动过滤的起始地址， 单位：bit</param>
		''' <param name="FilterLen">启动过滤的长度， 单位：bit</param>
		''' <param name="FilterData">启动过滤的数据</param>
		''' <param name="uBank">写入数据的bank</param>
		''' <param name="uPtr">写入数据的起始地址， 单位：字</param>
		''' <param name="uCnt">写入数据的长度， 单位：字</param>
		''' <param name="uDatabuf">写入的数据</param>
		''' <returns></returns>
		Public Function WriteData(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal uBank As Byte, ByVal uPtr As Integer, ByVal uCnt As Byte, ByVal uDatabuf() As Byte) As Boolean Implements IUHF.WriteData
			If UHFWriteData(uAccessPwd, FilterBank, FilterStartaddr, FilterLen, FilterData, uBank, uPtr, uCnt, uDatabuf) = 0 Then
				Return True
			End If
			Return False
		End Function
		''' <summary>
		''' 写标签到ECP
		''' </summary>
		''' <param name="accessPwd">4字节密码</param>
		''' <param name="filterBank">启动过滤的bank号， 1：EPC, 2:TID, 3:USR</param>
		''' <param name="filterPtr">启动过滤的起始地址， 单位：bit</param>
		''' <param name="filterCnt">启动过滤的长度， 单位：bit</param>
		''' <param name="filterData">启动过滤的数据</param>
		''' <param name="writeData">写入的EPC数据</param>
		''' <returns></returns>
		Public Function writeDataToEpc(ByVal accessPwd() As Byte, ByVal filterBank As Byte, ByVal filterPtr As Integer, ByVal filterCnt As Integer, ByVal filterData() As Byte, ByVal writeData() As Byte) As Boolean
			If writeData Is Nothing OrElse writeData.Length = 0 OrElse (writeData.Length Mod 2 <> 0) Then
				Throw New Exception("The length of the written data must be a multiple of 2.")
			End If
			Dim newWriteData(writeData.Length + 1) As Byte
			newWriteData(0) = CByte((writeData.Length \ 2) << 3)
			newWriteData(1) = 0
			Array.Copy(writeData, 0, newWriteData, 2, writeData.Length)
			Dim cnt As Byte = CByte(newWriteData.Length \ 2)

			Return Me.WriteData(accessPwd, filterBank, filterPtr, filterCnt, filterData, 1, 1, cnt, newWriteData)
		End Function


		''' <summary>
		''' LOCK标签
		''' </summary>
		''' <param name="uAccessPwd"> 4字节密码</param>
		''' <param name="FilterBank">启动过滤的bank号， 1：EPC, 2:TID, 3:USR</param>
		''' <param name="FilterStartaddr">启动过滤的起始地址， 单位：字节</param>
		''' <param name="FilterLen">启动过滤的长度， 单位：字节</param>
		''' <param name="FilterData">启动过滤的数据</param>
		''' <param name="lockbuf">3字节，第0-9位为Action位， 第10-19位为Mask位</param>
		''' <returns></returns>
		Public Function LockTag(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal lockbuf() As Byte) As Boolean Implements IUHF.LockTag
			If UHFLockTag(uAccessPwd, FilterBank, FilterStartaddr, FilterLen, FilterData, lockbuf) = 0 Then
				Return True
			End If
			Return False
		End Function
		''' <summary>
		'''  KILL标签
		''' </summary>
		''' <param name="uAccessPwd">4字节密码</param>
		''' <param name="FilterBank">启动过滤的bank号， 1：EPC, 2:TID, 3:USR</param>
		''' <param name="FilterStartaddr">启动过滤的起始地址， 单位：字节</param>
		''' <param name="FilterLen">启动过滤的长度， 单位：字节</param>
		''' <param name="FilterData">启动过滤的数据</param>
		''' <returns></returns>
		Public Function KillTag(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte) As Boolean Implements IUHF.KillTag
			If UHFKillTag(uAccessPwd, FilterBank, FilterStartaddr, FilterLen, FilterData) = 0 Then

				Return True
			End If
			Return False

		End Function
		''' <summary>
		''' BlockWrite 特定长度的数据到标签的特定地址
		''' </summary>
		''' <param name="uAccessPwd">4字节密码</param>
		''' <param name="FilterBank">启动过滤的bank号， 1：EPC, 2:TID, 3:USR</param>
		''' <param name="FilterStartaddr">启动过滤的起始地址， 单位：字节</param>
		''' <param name="FilterLen">启动过滤的长度， 单位：字节</param>
		''' <param name="FilterData">启动过滤的数据</param>
		''' <param name="uBank">块号  1：EPC, 2:TID, 3:USR</param>
		''' <param name="uPtr">读取数据的起始地址， 单位：字</param>
		''' <param name="uCnt">读取数据的长度， 单位：字</param>
		''' <param name="uDatabuf">写入的数据</param>
		''' <returns></returns>
		Public Function BlockWriteData(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal uBank As Byte, ByVal uPtr As Integer, ByVal uCnt As Integer, ByVal uDatabuf() As Byte) As Boolean Implements IUHF.BlockWriteData
			Dim sb As New StringBuilder()
			sb.Append(vbCrLf & "UHFBlockWriteData================>")
			sb.Append(vbCrLf & "密码：" & DataConvert.ByteArrayToHexString(uAccessPwd))
			sb.Append(vbCrLf & "过滤数据块（ 1：EPC, 2:TID, 3:USR）：" & FilterBank)
			sb.Append(vbCrLf & "过滤起始地址：" & FilterStartaddr)
			sb.Append(vbCrLf & "过滤长度：" & FilterLen)
			sb.Append(vbCrLf & "过滤数据：" & DataConvert.ByteArrayToHexString(FilterData))
			sb.Append(vbCrLf)
			sb.Append(vbCrLf & "写入的数据块：" & uBank)
			sb.Append(vbCrLf & "写入的数据起始地址：" & uPtr)
			sb.Append(vbCrLf & "写入的数据长度：" & uCnt)
			sb.Append(vbCrLf & "写入的数据内容：" & DataConvert.ByteArrayToHexString(uDatabuf))
			sb.Append(vbCrLf)

			FileManage.WriterFile("C:\Users\Administrator\Desktop\UHFLog.txt", sb.ToString(), True)

			If UHFBlockWriteData(uAccessPwd, FilterBank, FilterStartaddr, FilterLen, FilterData, uBank, uPtr, uCnt, uDatabuf) = 0 Then
				Return True
			End If
			Return False
		End Function
		''' <summary>
		''' BlockErase 特定长度的数据到标签的特定地址
		''' </summary>
		''' <param name="uAccessPwd">4字节密码</param>
		''' <param name="FilterBank">启动过滤的bank号， 1：EPC, 2:TID, 3:USR</param>
		''' <param name="FilterStartaddr">启动过滤的起始地址， 单位：字节</param>
		''' <param name="FilterLen">启动过滤的长度， 单位：字节</param>
		''' <param name="FilterData">启动过滤的数据</param>
		''' <param name="uBank">块号  1：EPC, 2:TID, 3:USR</param>
		''' <param name="uPtr">读取数据的起始地址， 单位：字</param>
		''' <param name="uCnt">读取数据的长度， 单位：字</param>
		''' <returns></returns>
		Public Function BlockEraseData(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal uBank As Byte, ByVal uPtr As Integer, ByVal uCnt As Byte) As Boolean Implements IUHF.BlockEraseData
			If UHFBlockEraseData(uAccessPwd, FilterBank, FilterStartaddr, FilterLen, FilterData, uBank, uPtr, uCnt) = 0 Then

				Return True
			End If
			Return False
		End Function
		''' <summary>
		''' Block Permalock操作
		''' </summary>
		''' <param name="uAccessPwd">4字节密码</param>
		''' <param name="FilterBank">启动过滤的bank号， 1：EPC, 2:TID, 3:USR</param>
		''' <param name="FilterStartaddr">启动过滤的起始地址， 单位：字节</param>
		''' <param name="FilterLen">启动过滤的长度， 单位：字节</param>
		''' <param name="FilterData">启动过滤的数据</param>
		''' <param name="ReadLock">bit0：（0：表示Read ， 1：表示Permalock）  </param>
		''' <param name="uBank">块号  1：EPC, 2:TID, 3:USR</param>
		''' <param name="uPtr">Block起始地址 ，单位为16个块</param>
		''' <param name="uRange">Block范围，单位为16个块</param>
		''' <param name="uMaskbuf">块掩码数据，2个字节，16bit 对应16个块</param>
		''' <returns></returns>
		Public Function BlockPermalock(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal ReadLock As Byte, ByVal uBank As Byte, ByVal uPtr As Integer, ByVal uRange As Byte, ByVal uMaskbuf() As Byte) As Boolean Implements IUHF.BlockPermalock
			If UHFBlockPermalock(uAccessPwd, FilterBank, FilterStartaddr, FilterLen, FilterData, ReadLock, uBank, uPtr, uRange, uMaskbuf) = 0 Then
				Return True

			End If
			Return False
		End Function
		#End Region

		#Region "QT 相关"
		''' <summary>
		''' 设置QT命令参数
		''' </summary>
		''' <param name="uAccessPwd">4字节密码</param>
		''' <param name="FilterBank">启动过滤的bank号， 1：EPC, 2:TID, 3:USR</param>
		''' <param name="FilterStartaddr">启动过滤的起始地址， 单位：字节</param>
		''' <param name="FilterLen">启动过滤的长度， 单位：字节</param>
		''' <param name="FilterData">启动过滤的数据</param>
		''' <param name="QTData">bit0：（0：表示无近距离控制 ， 1：表示启用近距离控制）  bit1：(0:表示启用private Memory map, 1：启用public memory map)</param>
		''' <returns></returns>
		Public Function SetQT(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal QTData As Byte) As Boolean Implements IUHF.SetQT
			If UHFSetQT(uAccessPwd, FilterBank, FilterStartaddr, FilterLen, FilterData, QTData) = 0 Then
				Return True
			End If
			Return False

		End Function
		''' <summary>
		''' 获取QT命令参数
		''' </summary>
		''' <param name="uAccessPwd">4字节密码</param>
		''' <param name="FilterBank">启动过滤的bank号， 1：EPC, 2:TID, 3:USR</param>
		''' <param name="FilterStartaddr">启动过滤的起始地址， 单位：字节</param>
		''' <param name="FilterLen">启动过滤的长度， 单位：字节</param>
		''' <param name="FilterData">启动过滤的数据</param>
		''' <param name="QTData">bit0：（0：表示无近距离控制 ， 1：表示启用近距离控制）  bit1：(0:表示启用private Memory map, 1：启用public memory map)</param>
		''' <returns></returns>
		Public Function GetQT(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByRef QTData As Byte) As Boolean Implements IUHF.GetQT
			If UHFGetQT(uAccessPwd, FilterBank, FilterStartaddr, FilterLen, FilterData, QTData) = 0 Then
				Return True
			End If
			Return False
		End Function
		''' <summary>
		''' QT标签读操作
		''' </summary>
		''' <param name="uAccessPwd">4字节密码</param>
		''' <param name="FilterBank">启动过滤的bank号， 1：EPC, 2:TID, 3:USR</param>
		''' <param name="FilterStartaddr">启动过滤的起始地址， 单位：字节</param>
		''' <param name="FilterLen">启动过滤的长度， 单位：字节</param>
		''' <param name="FilterData">启动过滤的数据</param>
		''' <param name="QTData">bit0：（0：表示无近距离控制 ， 1：表示启用近距离控制）  </param>
		''' <param name="uBank">块号  1：EPC, 2:TID, 3:USR</param>
		''' <param name="uPtr">读取数据的起始地址， 单位：字</param>
		''' <param name="uCnt">读取数据的长度， 单位：字</param>
		''' <param name="uReadDatabuf">读出的数据</param>
		''' <param name="uReadDataLen">读出的数据长度</param>
		''' <returns></returns>
		Public Function ReadQT(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal QTData As Byte, ByVal uBank As Byte, ByVal uPtr As Integer, ByVal uCnt As Byte) As String Implements IUHF.ReadQT
			Dim uReadDatabuf(511) As Byte
			Dim uReadDataLen As Byte = 0
			If UHFReadQT(uAccessPwd, FilterBank, FilterStartaddr, FilterLen, FilterData, QTData, uBank, uPtr, uCnt, uReadDatabuf, uReadDataLen) = 0 Then
				Dim strData As String = DataConvert.ByteArrayToHexString(uReadDatabuf, uReadDataLen) 'BitConverter.ToString(uReadDatabuf, 0, uReadDataLen).Replace("-", "");
				Return strData
			End If
			Return String.Empty
		End Function
		''' <summary>
		'''   QT标签写操作
		''' </summary>
		''' <param name="uAccessPwd"> 4字节密码</param>
		''' <param name="FilterBank">启动过滤的bank号， 1：EPC, 2:TID, 3:USR</param>
		''' <param name="FilterStartaddr"> 启动过滤的起始地址， 单位：字节</param>
		''' <param name="FilterLen">启动过滤的长度， 单位：字节</param>
		''' <param name="FilterData">启动过滤的数据</param>
		''' <param name="QTData">bit0：（0：表示无近距离控制 ， 1：表示启用近距离控制） </param>
		''' <param name="uBank">块号  1：EPC, 2:TID, 3:USR</param>
		''' <param name="uPtr">读取数据的起始地址， 单位：字</param>
		''' <param name="uCnt">读取数据的长度， 单位：字</param>
		''' <param name="uDatabuf">写入的数据</param>
		''' <returns></returns>
		Public Function WriteQT(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal QTData As Byte, ByVal uBank As Byte, ByVal uPtr As Integer, ByVal uCnt As Byte, ByVal uDatabuf() As Byte) As Boolean Implements IUHF.WriteQT
			If UHFWriteQT(uAccessPwd, FilterBank, FilterStartaddr, FilterLen, FilterData, QTData, uBank, uPtr, uCnt, uDatabuf) = 0 Then
				Return True
			End If
			Return False
		End Function
		#End Region

		#Region "盘点相关"
		''' <summary>
		''' 设置寻标签过滤设置
		''' </summary>
		''' <param name="saveflag">1:掉电保存， 0：不保存</param>
		''' <param name="bank">0x01:EPC , 0x02:TID, 0x03:USR</param>
		''' <param name="startaddr">起始地址，单位：字节</param>
		''' <param name="datalen">数据长度， 单位:字节</param>
		''' <param name="databuf">数据</param>
		''' <returns></returns>
		Public Function SetFilter(ByVal saveflag As Byte, ByVal bank As Byte, ByVal startaddr As Integer, ByVal datalen As Integer, ByVal databuf() As Byte) As Boolean Implements IUHF.SetFilter
			If UHFSetFilter(saveflag, bank, startaddr, datalen, databuf) = 0 Then
				Return True
			End If
			Return False
		End Function
		''' <summary>
		''' 单次盘存标签
		''' </summary>
		''' <param name="uLenUii">UII长度</param>
		''' <param name="uUii">UII数据</param>
		''' <returns></returns>
		Public Function InventorySingle(ByRef uLenUii As Byte, ByRef uUii() As Byte) As Boolean Implements IUHF.InventorySingle
			If UHFInventorySingle(uLenUii, uUii) = 0 Then
				Return True
			End If
			Return False
		End Function
		''' <summary>
		''' 连续盘存标签
		''' </summary>
		''' <returns></returns>
		Public Function Inventory() As Boolean Implements IUHF.Inventory

			Dim result As Integer = UHFInventory()
			If result = 0 Then
				Return True
			Else
				Return False
			End If
		End Function
		''' <summary>
		''' 停止盘存标签
		''' </summary>
		''' <returns></returns>
		Public Function StopGet() As Boolean Implements IUHF.StopGet

			If UHFStopGet() = 0 Then
				Return True
			Else
				Return False
			End If

		End Function
		''' <summary>
		''' 获取连续盘存标签数据
		''' </summary>
		''' <param name="uLenUii">UII长度</param>
		''' <param name="uUii">UII数据</param>
		''' <returns></returns>
		Public Function GetReceived_EX(ByRef uLenUii As Integer, ByRef uUii() As Byte) As Boolean Implements IUHF.GetReceived_EX
			If UHF_GetReceived_EX(uLenUii, uUii) = 0 Then
				Return True
			End If
			Return False
		End Function
		'读取epc
		Public Function uhfGetReceived(ByRef epc As String, ByRef tid As String, ByRef rssi As String, ByRef ant As String) As Boolean Implements IUHF.uhfGetReceived
			Dim uLen As Integer = 0
			Dim bufData(255) As Byte
			If GetReceived_EX(uLen, bufData) Then
				'  uUii = 1byteUII长度+UII数据+1byteTID数据+TID+2bytesRSSI
				Dim epc_data As String = String.Empty
				Dim uii_data As String = String.Empty 'uii数据
				Dim tid_data As String = String.Empty 'tid数据
				Dim rssi_data As String = String.Empty
				Dim ant_data As String = String.Empty

				Dim uii_len As Integer = bufData(0) 'uii长度
				Dim tid_leng As Integer = bufData(uii_len + 1) 'tid数据长度
				Dim tid_idex As Integer = uii_len + 2 'tid起始位
				Dim rssi_index As Integer = 1 + uii_len + 1 + tid_leng
				Dim ant_index As Integer = rssi_index + 2

				Dim strData As String = BitConverter.ToString(bufData, 0, uLen).Replace("-", "")
				epc_data = strData.Substring(6, uii_len * 2 - 4) 'Epc
				tid_data = strData.Substring(tid_idex * 2, tid_leng * 2) 'Tid
				Dim temp As String = strData.Substring(rssi_index * 2, 4)
				Dim rssiTemp As Integer = Convert.ToInt32(temp, 16) - 65535
				rssi_data = CSng(rssiTemp) / 10.0.ToString() ' RSSI  =  (0xFED6   -65535)/10
				ant_data = Convert.ToInt32((strData.Substring(ant_index * 2, 2)), 16).ToString()

				epc = epc_data
				tid = tid_data
				rssi = rssi_data
				ant = ant_data
				Return True
			Else
				Return False
			End If
		End Function

		Public Function uhfGetReceived() As UHFTAGInfo Implements IUHF.uhfGetReceived
			Dim uLen As Integer = 0
			Dim bufData(255) As Byte
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
					If tid_data.Length < 8 Then
						tid_data = ""
					End If
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
				info.User = user_data

				Return info
			Else
				Return Nothing
			End If
		End Function

		Public Function InventorySingle(ByRef epc As String) As Boolean Implements IUHF.InventorySingle
			Dim tid As String = String.Empty
			Dim rssi As String = String.Empty
			Dim uLen As Byte = 0
			Dim bufData(255) As Byte
			If UHFInventorySingle(uLen, bufData) = 0 Then
				'  uUii = 1byteUII长度+UII数据+1byteTID数据+TID+2bytesRSSI
				Dim epc_data As String = String.Empty
				Dim uii_data As String = String.Empty 'uii数据
				Dim tid_data As String = String.Empty 'tid数据
				Dim rssi_data As String = String.Empty

				' int uii_len = bufData[0];//uii长度
				Dim epclen As Integer = ((bufData(0) >> 3)) * 2
				' int tid_leng = bufData[uii_len + 1];//tid数据长度
				'  int tid_idex = uii_len + 2;//tid起始位
				' int rssi_index = 1 + uii_len + 1 + tid_leng;

				Dim strData As String = BitConverter.ToString(bufData, 0, uLen).Replace("-", "")
				epc_data = strData.Substring(4, epclen * 2) 'Epc
				'   tid_data = strData.Substring(tid_idex * 2, tid_leng * 2); //Tid
				'    string temp = strData.Substring(rssi_index * 2, 4);
				'    rssi_data = ((Convert.ToInt32(temp, 16) - 65535) / 10).ToString();// RSSI  =  (0xFED6   -65535)/10

				epc = epc_data
				'  tid = tid_data;
				'  rssi = rssi_data;
				Return True
			Else
				Return False
			End If
		End Function

		''' <summary>
		''' 设置EPC模式
		''' </summary>
		''' <returns></returns>
		Public Function setEPCMode(ByVal isSave As Boolean) As Boolean Implements IUHF.setEPCMode
			Dim flag As Integer = If(isSave, 1, 0)
			If UHFSetEPCTIDUSERMode(CByte(flag), 0, 0, 0) = 0 Then
				Return True
			End If
			Return False
		End Function
		''' <summary>
		''' 设置EPC+TID模式
		''' </summary>
		''' <returns></returns>
		Public Function setEPCAndTIDMode(ByVal isSave As Boolean) As Boolean Implements IUHF.setEPCAndTIDMode
			Dim flag As Integer = If(isSave, 1, 0)
			If UHFSetEPCTIDUSERMode(CByte(flag), &H1, 0, 0) = 0 Then
				Return True
			End If
			Return False
		End Function
		''' <summary>
		''' 设置EPC+TID+USER模式
		''' </summary>
		''' <returns></returns>
		Public Function setEPCAndTIDUSERMode(ByVal isSave As Boolean, ByVal userAddress As Byte, ByVal userLenth As Byte) As Boolean Implements IUHF.setEPCAndTIDUSERMode
			Dim flag As Integer = If(isSave, 1, 0)
			If UHFSetEPCTIDUSERMode(CByte(flag), &H2, userAddress, userLenth) = 0 Then
				Return True
			End If
			Return False
		End Function
		''' <summary>
		''' 获取盘点模式
		''' </summary>
		''' <param name="userAddress">user区的起始地址</param>
		''' <param name="userLenth">user区的长度</param>
		''' <returns>0:EPC;  1:EPC+TID;  2:EPC+TID:USER</returns>
		Public Function getEPCTIDUSERMode(ByRef userAddress As Byte, ByRef userLenth As Byte) As Integer Implements IUHF.getEPCTIDUSERMode
			Dim mode(9) As Byte
			Dim result As Integer = UHFGetEPCTIDUSERMode(0, 0, mode)
			If result > 0 Then
				userAddress = mode(1)
				userLenth = mode(2)
				Return mode(0)
			Else
				Return -1
			End If
		End Function

		#End Region

		#Region "EM4124标签"
		''' <summary>
		''' 激活或失效EM4124标签
		''' </summary>
		''' <param name="cmd"></param>
		''' <param name="uAccessPwd">4字节密码</param>
		''' <param name="FilterBank">启动过滤的bank号， 1：EPC, 2:TID, 3:USR</param>
		''' <param name="FilterStartaddr">启动过滤的起始地址， 单位：bit</param>
		''' <param name="FilterLen">启动过滤的数据长度</param>
		''' <param name="FilterData">启动过滤的数据</param>
		''' <returns></returns>
		Public Function Deactivate(ByVal cmd As Integer, ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte) As Boolean Implements IUHF.Deactivate
			If UHFDeactivate(cmd, uAccessPwd, FilterBank, FilterStartaddr, FilterLen, FilterData) = 0 Then
				Return True
			Else
				Return False
			End If

		End Function

		#End Region

		#Region "工作模式"
		''' <summary>
		''' 工作模式
		''' </summary>
		''' <param name="mode">0:命令工作模式   1:自动工作模式   2:触发模式</param>
		''' <returns></returns>
		Public Function SetWorkMode(ByVal mode As Byte) As Boolean Implements IUHF.SetWorkMode
			If UHFSetWorkMode(mode) = 0 Then
				Return True
			Else
				Return False
			End If
		End Function
		Public Function GetWorkMode(ByVal mode() As Byte) As Boolean Implements IUHF.GetWorkMode
			If UHFGetWorkMode(mode) = 0 Then
				Return True
			Else
				Return False
			End If
		End Function

		''' <summary>
		''' 设置触发工作模式参数
		''' </summary>
		''' <param name="ioControl">触发IO：0x00表示触发输入1；0x01表示触发输入2</param>
		''' <param name="workTime">触发工作时间：表示有触发输入时读卡工作时间，单位是10ms</param>
		''' <param name="intervalTime">触发时间间隔：表示触发工作时间结束后，间隔多久再判断触发输入IO口进行触发</param>
		''' <param name="mode">0x00表示串口输出，0x01表示UDP输出</param>
		''' <returns></returns>
		Public Function SetWorkModePara(ByVal ioControl As Byte, ByVal workTime As Integer, ByVal intervalTime As Integer, ByVal mode As Byte) As Boolean
			Dim para(5) As Byte
			para(0) = ioControl
			para(1) = CByte((workTime >> 8) And &HFF)
			para(2) = CByte(workTime And &HFF)
			para(3) = CByte((intervalTime >> 8) And &HFF)
			para(4) = CByte(intervalTime And &HFF)
			para(5) = mode
			Dim result As Integer = UHFSetWorkModePara(para)
			Return result = 0
		End Function
		Public Function GetWorkModePara(ByRef ioControl As Byte, ByRef workTime As Integer, ByRef intervalTime As Integer, ByRef mode As Byte) As Boolean
			Dim para(5) As Byte
			If UHFGetWorkModePara(para) = 0 Then
				ioControl = para(0)
				workTime = (para(1) << 8) Or (para(2) And &HFF)
				intervalTime = (para(3) << 8) Or (para(4) And &HFF)
				mode = para(5)
				Return True
			End If
			Return False
		End Function


		#End Region

		#Region "过热保护"
		''' <summary>
		''' 设置温度过热保护
		''' </summary>
		''' <param name="tempVal">50-75</param>
		''' <returns></returns>
		Public Function SetTemperatureVal(ByVal tempVal As Byte) As Boolean Implements IUHF.SetTemperatureVal
			If tempVal < 50 OrElse tempVal > 75 Then
				Return False
			End If

			If UHFSetTempVal(tempVal) = 0 Then
				Return True
			Else
				Return False
			End If
		End Function

		''' <summary>
		''' 获取温度过热保护值
		''' </summary>
		''' <returns></returns>
		Public Function GetTemperatureVal() As Integer Implements IUHF.GetTemperatureVal
			Dim tempVal(4) As Byte
			If UHFGetTempVal(tempVal) = 0 Then
				Return tempVal(0)
			Else
				Return -1
			End If
		End Function
		#End Region

		#Region "GPIO"
		''' <summary>
		''' 获取gpio输入
		''' </summary>
		''' <param name="outData">
		'''       outData[0]:    0:低电平   1：高电平
		'''       outData[1]:    0:低电平   1：高电平
		''' 
		''' </param>
		''' <returns></returns>
		Public Function getIOControl(ByVal outData() As Byte) As Boolean Implements IUHF.getIOControl
			Dim tempVal(4) As Byte
			If UHFGetIOControl(tempVal) = 0 Then
				If outData IsNot Nothing AndAlso outData.Length >= 2 Then
					outData(0) = tempVal(0)
					outData(1) = tempVal(1)
				End If
				Return True
			Else
				Return False
			End If
		End Function
		''' <summary>
		''' 设置gpio输出
		''' </summary>
		''' <param name="ouput1">0:低电平   1：高电平</param>
		''' <param name="ouput2">0:低电平   1：高电平</param>
		''' <param name="outStatus">继电器 0：断开    1：闭合</param>
		''' <returns></returns>
		Public Function setIOControl(ByVal ouput1 As Byte, ByVal ouput2 As Byte, ByVal outStatus As Byte) As Boolean Implements IUHF.setIOControl
			If ouput1 <> 0 AndAlso ouput1 <> 1 Then
				Return False
			End If
			If ouput2 <> 0 AndAlso ouput2 <> 1 Then
				Return False
			End If
			If outStatus <> 0 AndAlso outStatus <> 1 Then
				Return False
			End If
			If UHFSetIOControl(ouput1, ouput2, outStatus) = 0 Then
				Return True
			Else
				Return False
			End If
		End Function


		Public Function SetOutput(ByVal outData() As Byte) As Boolean
			Dim result As Integer = UHFSetOutputIO(outData, CByte(outData.Length))
			Return result = 0

		End Function
		Public Function GetInputStatus(ByVal statusData() As Byte) As Boolean
			Dim temp(9) As Byte
			Dim dataLen(0) As Integer
			Dim result As Integer = UHFGetIOStatus(temp, dataLen)
			If result = 0 Then
				statusData(0) = temp(1)
				statusData(1) = temp(3)
				Return True
			End If
			Return False
		End Function


		#End Region

		#Region "工作时间等待时间"
		''' <summary>
		''' 设置工作时间和等待時間
		''' </summary>
		''' <param name="workTime">工作時間</param>
		''' <param name="waitTime">等待時間</param>
		''' <param name="isSave">是否保存</param>
		''' <returns></returns>
		Public Function setWorkAndWaitTime(ByVal workTime As Integer, ByVal waitTime As Integer, ByVal isSave As Boolean) As Boolean Implements IUHF.setWorkAndWaitTime
			Dim work1 As Byte = CByte((workTime >> 8) And &HFF)
			Dim work2 As Byte = CByte(workTime And &HFF)
			Dim wait1 As Byte = CByte((waitTime >> 8) And &HFF)
			Dim wait2 As Byte = CByte(waitTime And &HFF)

			If UHFSetWorkTime(CByte(If(isSave, 1, 0)), work1, work2, wait1, wait2) = 0 Then
				Return True
			Else
				Return False
			End If
		End Function
		''' <summary>
		''' 獲取工作时间和等待時間
		''' </summary>
		''' <param name="workTime">工作時間</param>
		''' <param name="waitTime">等待時間</param>
		''' <param name="isSave">是否保存</param>
		''' <returns></returns>
		Public Function getWorkAndWaitTime(<System.Runtime.InteropServices.Out()> ByRef workTime As Integer, <System.Runtime.InteropServices.Out()> ByRef waitTime As Integer) As Boolean Implements IUHF.getWorkAndWaitTime
			Dim data(99) As Byte
			Dim result As Integer = UHFGetWorkTime(data)

			If result = -1 Then
				workTime = -1
				waitTime = -1
				Return False
			End If

			workTime = (data(1) Or (data(0) << 8))
			waitTime = (data(3) Or (data(2) << 8))
			Return True
		End Function
		#End Region

		#Region "升级"


		Public Function jump2Boot(ByVal flag As Byte) As Boolean Implements IUHF.jump2Boot
			Dim reuslt As Integer = UHFJump2Boot(flag)
			Return reuslt = 0
		End Function

		Public Function startUpd() As Boolean Implements IUHF.startUpd
			Dim reuslt As Integer = UHFStartUpd()
			Return reuslt = 0
		End Function

		Public Function updating(ByVal data() As Byte, ByVal len As Integer) As Boolean Implements IUHF.updating
			Dim reuslt As Integer = UHF_Upgrade(data, len)
			Return reuslt = 0
		End Function

		Public Function stopUpdate() As Boolean Implements IUHF.stopUpdate
			Dim reuslt As Integer = UHFStopUpdate()
			Return reuslt = 0
		End Function
		#End Region

		Public Function GetAPIVersion() As String Implements IUHF.GetAPIVersion
			Dim time(39) As Byte
			Dim result As Integer = UHFGetVersionCode(time)
			Return "Ver" & result & ".0 (" & System.Text.ASCIIEncoding.ASCII.GetString(time, 0, time.Length).Replace(vbNullChar, "") & ")"

		End Function

		Public Function WriteScreenBlockData(ByVal pwd() As Byte, ByVal sector As Byte, ByVal mask_addr As Short, ByVal mask_len As Short, ByVal mask_data() As Byte, ByVal type As Byte, ByVal w_addr As Short, ByVal w_len As Short, ByVal w_data() As Byte) As Boolean

			Return UHFWriteScreenBlockData(pwd, sector, mask_addr, mask_len, mask_data, type, w_addr, w_len, w_data) = 0
		End Function

		'''************多设备连接*************
		'''************************************************************************************

		'获取当前连接的ip信息
		Public Function LinkGetDeviceInfo() As List(Of DeviceInfo)
			Dim info(511) As Byte
			Dim resultLen As Integer = LinkGetInfo(info, info.Length)
			If resultLen > 0 Then
				Dim jsonstring As String = System.Text.ASCIIEncoding.ASCII.GetString(info, 0, resultLen).Replace(vbNullChar, "")
				Dim list As New List(Of DeviceInfo)()
				Dim obj As Object = JsonConvert.DeserializeObject(Of System.Dynamic.ExpandoObject)(jsonstring)
				Dim arr As JArray = JArray.FromObject(obj) '(JArray)JsonConvert.DeserializeObject(jsonstring);


				For k As Integer = 0 To arr.Count - 1
					Dim _id As Object = arr(k)("id")
					Dim _type As Object = arr(k)("type")
					Dim _ip As Object = arr(k)("ip")
					Dim _port As Object = arr(k)("port")
					Dim deviceInfo As New DeviceInfo()
					If _id IsNot Nothing Then
						deviceInfo.Id = Integer.Parse(_id.ToString())
					End If
					If _type IsNot Nothing Then
						deviceInfo.Type = _type.ToString()
					End If
					If _port IsNot Nothing Then
						deviceInfo.Port = Integer.Parse(_port.ToString())
					End If
					If _ip IsNot Nothing Then
						deviceInfo.Ip = _ip.ToString()
					End If
					list.Add(deviceInfo)
				Next k
				Return list

			End If
			Return Nothing
		End Function
		'选择要操作的id，根据LinkGetInfo获取id信息
		Public Function LinkSelectDevice(ByVal id As Integer) As Boolean
			Return LinkSelect(id) = 0
		End Function
		'获取当前可以操作的id
		Public Function LinkGetSelectedDevice() As Integer
			Return LinkGetSelected()
		End Function

		Public Sub LinkDisConnectAllDevice()
			LinkCloseAll()
		End Sub

		Public Function InventoryById(ByVal id As Integer) As Boolean
		   Return UHFInventoryById(id)=0
		End Function
		Public Function StopById(ByVal id As Integer) As Boolean
			Return UHFStopById(id)=0
		End Function



		Public Function CalibrationVoltage() As Integer
			Dim value(0) As Integer
			If UHFVerifyVoltage(value) = 0 Then
				Dim temp As Integer = value(0)
				Return temp
			End If
			Return -1

		End Function


		' #define CONTENT_TYPE_INVALID        0
		' #define CONTENT_TYPE_EPC            1
		' #define CONTENT_TYPE_TID            2
		' #define CONTENT_TYPE_USER           3
		' #define CONTENT_TYPE_RSSI           4
		' #define CONTENT_TYPE_ANT            5
		' #define CONTENT_TYPE_ID             6
		' #define CONTENT_TYPE_IP             7
		' #define CONTENT_TYPE_SENSOR         8 
		'return 0,no data, > 0 tag length, < 0 error code
		'tdata tag data, type+length+content+...+type+length+content
		'type:1-epc,2-tid,3-user,4-rssi,5-antenna,6-id
		'06 02 00 00     01 0E 30 00 E2 00 51 57 88 18 01 61 22 20 2F 60 04 02 FD B1 05 01 01
		Public Function getTagData() As TagInfo
			Dim info As New TagInfo()
			Dim tagTempData(149) As Byte 'Array.Clear(tagTempData, 0, tagTempData.Length);
			Dim result As Integer = UHFGetTagData(tagTempData, tagTempData.Length)
			info.ErrCode = result
			If result > 0 Then
				' if (tagTempData[0] == 0)
				If True Then
					Dim hex As String = BitConverter.ToString(tagTempData, 0, result)
				   ' Console.WriteLine("hex=" + hex + " result=" + result);
				End If
				Dim index As Integer = 0
				Dim uhfinfo As New UHFTAGInfo()
				Do
					If index > result Then
						Exit Do
					End If
					Dim type As Integer = tagTempData(index)
					index = index + 1
					If index > result Then
						Exit Do
					End If
					Dim len As Integer = tagTempData(index)
					index = index + 1
					If index + len > result Then
						Exit Do
					End If
					Dim data() As Byte = BLEDeviceAPI.Utils.CopyArray(Of Byte)(tagTempData, index, len)
					index = index + len

					If type = 1 Then
						'epc
						uhfinfo.Epc = BitConverter.ToString(data, 2, data.Length - 2).Replace("-", "")
					ElseIf type = 2 Then
						'tid
						uhfinfo.Tid = BitConverter.ToString(data, 0, data.Length).Replace("-", "")
					ElseIf type = 3 Then
						'user
						uhfinfo.User = BitConverter.ToString(data, 0, data.Length).Replace("-", "")
					ElseIf type = 4 Then
						'rssi
						Dim rssiTemp As Integer = (data(1) Or (data(0) << 8)) - 65535
						Dim rssi_data As String = CSng(rssiTemp) / 10.0.ToString() ' RSSI  =  (0xFED6   -65535)/10
						If Not rssi_data.Contains(".") Then
							rssi_data = rssi_data & ".0"
						End If
						uhfinfo.Rssi = rssi_data
					ElseIf type = 5 Then
						'ant
						uhfinfo.Ant = data(0).ToString()
					ElseIf type = 6 Then
						'id
						info.Id = data(1)
					ElseIf type = 8 Then
						'Sensor
						uhfinfo.Sensor = BitConverter.ToString(data, 0, data.Length).Replace("-", "")
					End If
				Loop
				info.UhfTagInfo = uhfinfo

			End If
			Return info
		End Function


		#Region "温度标签"
		Public Function InitRegFile(ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte) As Boolean
			Dim result As Integer = UHFInitRegFile(FilterBank, FilterStartaddr, FilterLen, FilterData)
			If result = 0 Then
				Return True
			End If
			Return False
		End Function
		Public Function ReadTagTemperature(ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal outtemp() As Single) As Boolean
			Dim result As Integer = UHFReadTagTemp(FilterBank, FilterStartaddr, FilterLen, FilterData, outtemp)
			If result = 0 Then
				Return True
			End If
			Return False
		End Function

'INSTANT VB NOTE: The parameter debug was renamed since it may cause conflicts with calls to static members of the user-defined type with this name:
		Public Function SetDebug(ByVal debug_Conflict As Boolean) As Boolean
			Return SetLogLevel(If(debug_Conflict, 3, 0))=0
		End Function

'INSTANT VB NOTE: The parameter debug was renamed since it may cause conflicts with calls to static members of the user-defined type with this name:
		Public Function SaveLog(ByVal debug_Conflict As Boolean) As Boolean
			Return SaveLogFile(If(debug_Conflict, 1, 0)) = 0
		End Function


		#End Region ' 温度标签end

	End Class




End Namespace






