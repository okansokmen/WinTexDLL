Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports BLEDeviceAPI

Namespace UHFAPP
	Public Interface IUHF


#Region "协议"
		  ''' <summary>
		  ''' 设置协议类型
		  ''' </summary>
		  ''' <param name="type">type  0x00 表示 ISO18000-6C 协议,0x01 表示 GB/T 29768 国标协议,0x02 表示 GJB 7377.1 国军标协议</param>
		  ''' <returns></returns>
		 Function SetProtocol(ByVal type As Byte) As Boolean
		''' <summary>
		''' 获取协议类型
		''' </summary>
		''' <returns></returns>
		 Function GetProtocol() As Integer
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
		 Function GBTagLock(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal memory As Byte, ByVal config As Byte, ByVal action As Byte) As Boolean

#End Region

		  Function OpenUsb() As Boolean
		  Sub CloseUsb()

		  #Region "TCPIP"



		  '设置蜂鸣器 工作模式：0x00表示关闭蜂鸣器；0x01表示打开蜂鸣器
		 Function UHFSetBuzzer(ByVal mode As Byte) As Boolean
		  '获取蜂鸣器 工作模式：0x00表示关闭蜂鸣器；0x01表示打开蜂鸣器
		 Function UHFGetBuzzer(ByVal mode() As Byte) As Boolean



		 Function SetLocalIP(ByVal ip As String, ByVal port As Integer, ByVal mask As String, ByVal gate As String) As Boolean

		 Function GetLocalIP(ByVal ip As StringBuilder, ByVal port As StringBuilder, ByVal mask As StringBuilder, ByVal gate As StringBuilder) As Boolean


		 Function SetDestIP(ByVal ip As String, ByVal port As Integer) As Boolean

		 Function GetDestIP(ByVal ip As StringBuilder, ByVal port As StringBuilder) As Boolean


  #End Region

  #Region "串口、版本号、ID"
		 ''' <summary>
		 ''' 打开串口，或者usb
		 ''' </summary>
		 ''' <returns></returns>
		 Function Open() As Boolean
			''' <summary>
		 ''' 关闭串口，或者usb
			''' </summary>
			''' <returns></returns>
		 Function Close() As Boolean
		  ''' <summary>
		  ''' 获取硬件版本
		  ''' </summary>
		  ''' <returns></returns>
		 Function GetHardwareVersion() As String
		  ''' <summary>
		  ''' 获取软件版本
		  ''' </summary>
		  ''' <returns></returns>
		 Function GetSoftwareVersion() As String
		  ''' <summary>
		  ''' 获取设备ID
		  ''' </summary>
		  ''' <returns>id--整型ID号</returns>
		 Function GetUHFGetDeviceID() As Integer
  #End Region

  #Region "频率、功率"
		  ''' <summary>
		  ''' 设置功率
		  ''' </summary>
		  ''' <param name="save">1:保存设置   0：不保存</param>
		  ''' <param name="uPower">功率（DB）</param>
		 ''' <returns></returns>
		   Function SetPower(ByVal save As Byte, ByVal uPower As Byte) As Boolean
		''' <summary>
		''' 获取功率
		''' </summary>
		''' <param name="uPower">功率（DB）</param>
		''' <returns></returns>
		   Function GetPower(ByRef uPower As Byte) As Boolean
		  ''' <summary>
		  ''' 获取跳频
		  ''' </summary>
		  ''' <param name="Freqbuf">Freqbuf[0]--频点个数， Freqbuf[1]..[x]--频点数组（整型）</param>
		  ''' <returns></returns>
		   Function GetJumpFrequency(ByRef Freqbuf() As Integer) As Boolean
		  ''' <summary>
		  ''' 设置跳频
		  ''' </summary>
		  ''' <param name="nums">跳频个数</param>
		  ''' <param name="Freqbuf">Freqbuf--频点数组（整型） ，如920125，921250.....</param>
		  ''' <returns></returns>
		   Function SetJumpFrequency(ByVal nums As Byte, ByVal Freqbuf() As Integer) As Boolean
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
		   Function SetGen2(ByVal Target As Byte, ByVal Action As Byte, ByVal T As Byte, ByVal Q As Byte, ByVal StartQ As Byte, ByVal MinQ As Byte, ByVal MaxQ As Byte, ByVal D As Byte, ByVal C As Byte, ByVal P As Byte, ByVal Sel As Byte, ByVal Session As Byte, ByVal G As Byte, ByVal LF As Byte) As Boolean
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
		   Function GetGen2(ByRef Target As Byte, ByRef Action As Byte, ByRef T As Byte, ByRef Q As Byte, ByRef StartQ As Byte, ByRef MinQ As Byte, ByRef MaxQ As Byte, ByRef D As Byte, ByRef Coding As Byte, ByRef P As Byte, ByRef Sel As Byte, ByRef Session As Byte, ByRef G As Byte, ByRef LF As Byte) As Boolean
  #End Region

		  ''' <summary>
		  ''' 设置CW
		  ''' </summary>
		  ''' <param name="flag">flag -- 1:开CW，  0：关CW</param>
		  ''' <returns></returns>
		   Function SetCW(ByVal flag As Byte) As Boolean
		   ''' <summary>
		   ''' 功能：获取CW
		   ''' </summary>
		   ''' <param name="flag">flag -- 1:开CW，  0：关CW</param>
		   ''' <returns></returns>
		   Function GetCW(ByRef flag As Byte) As Boolean
		   ''' <summary>
		   ''' 天线设置
		  ''' </summary>
		  ''' <param name="saveflag">saveflag -- 1:掉电保存，  0：不保存</param>
		  ''' <param name="buf">buf--2bytes, 共16bits, 每bit 置1选择对应天线</param>
		  ''' <returns></returns>
		   Function SetANT(ByVal saveflag As Byte, ByVal buf() As Byte) As Boolean
		   ''' <summary>
		   ''' 获取天线设置
		   ''' </summary>
		  ''' <param name="buf">buf--2bytes, 共16bits</param>
		   ''' <returns></returns>
		   Function GetANT(ByVal buf() As Byte) As Boolean
		   ''' <summary>
		  ''' 区域设置
		   ''' </summary>
		  ''' <param name="saveflag">1:掉电保存，  0：不保存</param>
		  ''' <param name="region">0x01(China1),0x02(China2),0x04(Europe),0x08(USA),0x16(Korea),0x32(Japan)</param>
		   ''' <returns></returns>
		   Function SetRegion(ByVal saveflag As Byte, ByVal region As Byte) As Boolean
		   ''' <summary>
		  ''' 获取区域设置
		   ''' </summary>
		  ''' <param name="region"> 0x01(China1),0x02(China2),0x04(Europe),0x08(USA),0x16(Korea),0x32(Japan)</param>
		   ''' <returns></returns>
		   Function GetRegion(ByRef region As Byte) As Boolean
		  ''' <summary>
		  ''' 获取模块温度
		  ''' </summary>
		  ''' <param name="temperature">回传的温度</param>
		  ''' <returns>返回true表示获取成功，temperature参数可以使用。返回false表示获取失败，temperature参数不可以使用</returns>
		   Function GetTemperature() As String
		   ''' <summary>
		   ''' 设置温度保护
		  ''' </summary>
		  ''' <param name="flag">1:温度保护， 0：无温度保护</param>
		  ''' <returns></returns>
		   Function SetTemperatureProtect(ByVal flag As Byte) As Boolean
		   ''' <summary>
		   ''' 获取温度保护
		   ''' </summary>
		   ''' <param name="flag">1:温度保护， 0：无温度保护</param>
		   ''' <returns></returns>
		   Function GetTemperatureProtect(ByRef flag As Byte) As Boolean
		   ''' <summary>
		  ''' 设置天线工作时间
		   ''' </summary>
		  ''' <param name="antnum">天线号</param>
		  ''' <param name="saveflag">1:掉电保存， 0：不保存</param>
		  ''' <param name="WorkTime">工作时间 ，单位ms, 范围 10-65535ms</param>
		   ''' <returns></returns>
		   Function SetANTWorkTime(ByVal antnum As Byte, ByVal saveflag As Byte, ByVal WorkTime As Integer) As Boolean
		   ''' <summary>
		  ''' 获取天线工作时间
		   ''' </summary>
		  ''' <param name="antnum">天线号</param>
		  ''' <param name="WorkTime">工作时间 ，单位ms, 范围 10-65535ms</param>
		   ''' <returns></returns>
		   Function GetANTWorkTime(ByVal antnum As Byte, ByRef WorkTime As Integer) As Boolean
		   ''' <summary>
		  ''' 设置链路组合
		   ''' </summary>
		  ''' <param name="saveflag">1:掉电保存， 0：不保存</param>
		  ''' <param name="mode">0:DSB_ASK/FM0/40KHZ , 1:PR_ASK/Miller4/250KHZ , 2:PR_ASK/Miller4/300KHZ, 3:DSB_ASK/FM0/400KHZ</param>
		   ''' <returns></returns>
		   Function SetRFLink(ByVal saveflag As Byte, ByVal mode As Byte) As Boolean
		   ''' <summary>
		  ''' 获取链路组合
		   ''' </summary>
		  ''' <param name="uMode">0:DSB_ASK/FM0/40KHZ , 1:PR_ASK/Miller4/250KHZ , 2:PR_ASK/Miller4/300KHZ, 3:DSB_ASK/FM0/400KHZ</param>
		   ''' <returns></returns>
		   Function GetRFLink(ByRef uMode As Byte) As Boolean
		  ''' <summary>
		  ''' 设置FastID功能
		  ''' </summary>
		  ''' <param name="flag">1:开启， 0：关闭</param>
		 ''' <returns></returns>
		   Function SetFastID(ByVal flag As Byte) As Boolean
		  ''' <summary>
		''' 获取FastID功能
		  ''' </summary>
		''' <param name="flag">1:开启， 0：关闭</param>
		  ''' <returns></returns>
		   Function GetFastID(ByRef flag As Byte) As Boolean
		''' <summary>
		''' 设置Tagfocus功能
		''' </summary>
		''' <param name="flag">1:开启， 0：关闭</param>
		''' <returns></returns>
		   Function SetTagfocus(ByVal flag As Byte) As Boolean
		   ''' <summary>
		  ''' 获取Tagfocus功能
		   ''' </summary>
		  ''' <param name="flag">1:开启， 0：关闭</param>
		   ''' <returns></returns>
		   Function GetTagfocus(ByRef flag As Byte) As Boolean
		   ''' <summary>
		  ''' 设置软件复位
		 ''' </summary>
		 ''' <returns></returns>
		   Function SetSoftReset() As Boolean
		   ''' <summary>
			''' 设置Dual和Singel模式
		   ''' </summary>
		   ''' <param name="saveflag">1:掉电保存， 0：不保存</param>
		   ''' <param name="mode">1:设置Singel模式， 0：设置Dual模式</param>
		   ''' <returns></returns>
		   Function SetDualSingelMode(ByVal saveflag As Byte, ByVal mode As Byte) As Boolean
		   ''' <summary>
		  ''' 获取Dual和Singel模式
		   ''' </summary>
		  ''' <param name="mode">1:设置Singel模式， 0：设置Dual模式</param>
		   ''' <returns></returns>
		   Function GetDualSingelMode(ByRef mode As Byte) As Boolean
		   ''' <summary>
		  ''' 设置寻标签过滤设置
		   ''' </summary>
		  ''' <param name="saveflag">1:掉电保存， 0：不保存</param>
		  ''' <param name="bank">0x01:EPC , 0x02:TID, 0x03:USR</param>
		  ''' <param name="startaddr">起始地址，单位：字节</param>
		  ''' <param name="datalen">数据长度， 单位:字节</param>
		  ''' <param name="databuf">数据</param>
		   ''' <returns></returns>
		   Function SetFilter(ByVal saveflag As Byte, ByVal bank As Byte, ByVal startaddr As Integer, ByVal datalen As Integer, ByVal databuf() As Byte) As Boolean
		   ''' <summary>
		  ''' 设置EPC和TID模式
		   ''' </summary>
		  ''' <param name="saveflag">1:掉电保存， 0：不保存</param>
		  ''' <param name="mode">1：开启EPC和TID， 0:关闭</param>
		  ''' <returns></returns>
		  ' bool SetEPCTIDMode(byte saveflag, byte mode);
		   ''' <summary>
		  ''' 获取EPC和TID模式
		   ''' </summary>
		  ''' <param name="mode">1：开启EPC和TID， 0:关闭</param>
		   ''' <returns></returns>
		  ' bool GetEPCTIDMode(ref byte mode);
		   ''' <summary>
		  ''' 恢复出厂设置
		   ''' </summary>
		   ''' <returns></returns>
		   Function SetDefaultMode() As Boolean
			''' <summary>
			''' 单次盘存标签
			''' </summary>
			''' <param name="uLenUii">UII长度</param>
			''' <param name="uUii">UII数据</param>
			''' <returns></returns>
		   Function InventorySingle(ByRef uLenUii As Byte, ByRef uUii() As Byte) As Boolean
		   ''' <summary>
		  ''' 连续盘存标签
		   ''' </summary>
		   ''' <returns></returns>
		   Function Inventory() As Boolean
		   ''' <summary>
		  ''' 停止盘存标签
		   ''' </summary>
		   ''' <returns></returns>
		   Function StopGet() As Boolean
		  ''' <summary>
		  ''' 获取连续盘存标签数据
		  ''' </summary>
		  ''' <param name="uLenUii">UII长度</param>
		  ''' <param name="uUii">UII数据</param>
		 ''' <returns></returns>
		   Function GetReceived_EX(ByRef uLenUii As Integer, ByRef uUii() As Byte) As Boolean
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
		   Function ReadData(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal uBank As Byte, ByVal uPtr As Integer, ByVal uCnt As Integer) As String


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
		   Function WriteData(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal uBank As Byte, ByVal uPtr As Integer, ByVal uCnt As Byte, ByVal uDatabuf() As Byte) As Boolean
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
		   Function LockTag(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal lockbuf() As Byte) As Boolean
		   ''' <summary>
		  '''  KILL标签
		   ''' </summary>
		  ''' <param name="uAccessPwd">4字节密码</param>
		  ''' <param name="FilterBank">启动过滤的bank号， 1：EPC, 2:TID, 3:USR</param>
		  ''' <param name="FilterStartaddr">启动过滤的起始地址， 单位：字节</param>
		  ''' <param name="FilterLen">启动过滤的长度， 单位：字节</param>
		  ''' <param name="FilterData">启动过滤的数据</param>
		   ''' <returns></returns>
		   Function KillTag(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte) As Boolean
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
		   Function BlockWriteData(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal uBank As Byte, ByVal uPtr As Integer, ByVal uCnt As Integer, ByVal uDatabuf() As Byte) As Boolean
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
		   Function BlockEraseData(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal uBank As Byte, ByVal uPtr As Integer, ByVal uCnt As Byte) As Boolean
 #Region "QT 相关"
		   ''' <summary>
		  ''' 设置QT命令参数
		   ''' </summary>
		  ''' <param name="uAccessPwd">4字节密码</param>
		  ''' <param name="FilterBank">启动过滤的bank号， 1：EPC, 2:TID, 3:USR</param>
		  ''' <param name="FilterStartaddr">启动过滤的起始地址， 单位：字节</param>
		  ''' <param name="FilterLen">启动过滤的长度， 单位：字节</param>
		  ''' <param name="FilterData">启动过滤的数据</param>
		  ''' <param name="QTData">bit0：（0：表示无近距离控制 ， 1：表示启用近距离控制）  bit1：(0:表示启用private Memory map, 1：启用 memory map)</param>
		   ''' <returns></returns>
		   Function SetQT(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal QTData As Byte) As Boolean
			''' <summary>
		  ''' 获取QT命令参数
			''' </summary>
		  ''' <param name="uAccessPwd">4字节密码</param>
		  ''' <param name="FilterBank">启动过滤的bank号， 1：EPC, 2:TID, 3:USR</param>
		  ''' <param name="FilterStartaddr">启动过滤的起始地址， 单位：字节</param>
		  ''' <param name="FilterLen">启动过滤的长度， 单位：字节</param>
		  ''' <param name="FilterData">启动过滤的数据</param>
		  ''' <param name="QTData">bit0：（0：表示无近距离控制 ， 1：表示启用近距离控制）  bit1：(0:表示启用private Memory map, 1：启用 memory map)</param>
			''' <returns></returns>
		   Function GetQT(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByRef QTData As Byte) As Boolean
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
		   Function ReadQT(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal QTData As Byte, ByVal uBank As Byte, ByVal uPtr As Integer, ByVal uCnt As Byte) As String
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
		   Function WriteQT(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal QTData As Byte, ByVal uBank As Byte, ByVal uPtr As Integer, ByVal uCnt As Byte, ByVal uDatabuf() As Byte) As Boolean
 #End Region 
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
		   Function BlockPermalock(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal ReadLock As Byte, ByVal uBank As Byte, ByVal uPtr As Integer, ByVal uRange As Byte, ByVal uMaskbuf() As Byte) As Boolean

		  '读取epc
		   Function uhfGetReceived(ByRef epc As String, ByRef tid As String, ByRef rssi As String, ByRef ant As String) As Boolean
		   Function uhfGetReceived() As UHFTAGInfo
		   Function InventorySingle(ByRef epc As String) As Boolean


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
		   Function Deactivate(ByVal cmd As Integer, ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte) As Boolean

		   Function SetWorkMode(ByVal mode As Byte) As Boolean
		   Function GetWorkMode(ByVal mode() As Byte) As Boolean

		   ''' <summary>
		   ''' 设置温度过热保护
		  ''' </summary>
		  ''' <param name="tempVal">50-75</param>
		  ''' <returns></returns>
		   Function SetTemperatureVal(ByVal tempVal As Byte) As Boolean

		  ''' <summary>
		  ''' 获取温度过热保护值
		  ''' </summary>
		  ''' <returns></returns>
		   Function GetTemperatureVal() As Integer

		  ''' <summary>
		  ''' 获取gpio输入
		  ''' </summary>
		  ''' <param name="outData">
		  '''       outData[0]:    0:低电平   1：高电平
		  '''       outData[1]:    0:低电平   1：高电平
		  ''' 
		  ''' </param>
		  ''' <returns></returns>
		   Function getIOControl(ByVal outData() As Byte) As Boolean
		  ''' <summary>
		  ''' 设置gpio输出
		  ''' </summary>
		  ''' <param name="ouput1">0:低电平   1：高电平</param>
		  ''' <param name="ouput2">0:低电平   1：高电平</param>
		  ''' <param name="outStatus">继电器 0：断开    1：闭合</param>
		  ''' <returns></returns>
		   Function setIOControl(ByVal ouput1 As Byte, ByVal ouput2 As Byte, ByVal outStatus As Byte) As Boolean


			''' <summary>
		   ''' 设置工作时间和等待時間
			''' </summary>
			''' <param name="workTime">工作時間</param>
			''' <param name="waitTime">等待時間</param>
			''' <param name="isSave">是否保存</param>
			''' <returns></returns>
		   Function setWorkAndWaitTime(ByVal workTime As Integer, ByVal waitTime As Integer, ByVal isSave As Boolean) As Boolean
		   ''' <summary>
		   ''' 獲取工作时间和等待時間
		   ''' </summary>
		   ''' <param name="workTime">工作時間</param>
		   ''' <param name="waitTime">等待時間</param>
		   ''' <param name="isSave">是否保存</param>
		   ''' <returns></returns>
		   Function getWorkAndWaitTime(ByRef workTime As Integer, ByRef waitTime As Integer) As Boolean


		   ''' <summary>
		   ''' 设置EPC模式
		   ''' </summary>
		   ''' <returns></returns>
		   Function setEPCMode(ByVal isSave As Boolean) As Boolean
		   ''' <summary>
		   ''' 设置EPC 和TID 模式
		   ''' </summary>
		   ''' <returns></returns>
		   Function setEPCAndTIDMode(ByVal isSave As Boolean) As Boolean
		   ''' <summary>
		   ''' 设置EPC+TID+User模式
		   ''' </summary>
		   ''' <param name="userAddress">user区域起始地址</param>
		   ''' <param name="userLenth">user区长度</param>
		   ''' <returns></returns>
		   Function setEPCAndTIDUSERMode(ByVal isSave As Boolean, ByVal userAddress As Byte, ByVal userLenth As Byte) As Boolean
		   ''' <summary>
		   ''' 获取盘点模式
		   ''' </summary>
		   ''' <param name="userAddress">user区域起始地址</param>
		   ''' <param name="userLenth">user区长度</param>
		   ''' <returns>  0x00:EPC模式;  0x01:EPC+TID模式;  0x02:EPC+TID+USER模式</returns>
		   Function getEPCTIDUSERMode(ByRef userAddress As Byte, ByRef userLenth As Byte) As Integer

		   ''' <summary>
		   ''' 升级固件
		   ''' </summary>
		   ''' <param name="flag">1: R2000， 0:STM32  </param>
		   ''' <returns></returns>
		   Function jump2Boot(ByVal flag As Byte) As Boolean
		   Function startUpd() As Boolean
		   Function updating(ByVal buf() As Byte, ByVal len As Integer) As Boolean
		   Function stopUpdate() As Boolean

		   Function GetSTM32Version() As String
		   Function GetAPIVersion() As String
	End Interface
End Namespace
