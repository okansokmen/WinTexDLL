Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text


Namespace BLEDeviceAPI.interfaces
	Friend Interface IUHFProtocolParse
'''        
'''         * 
'''         * 解析设置蜂鸣器返回的数据
'''         * 
'''         * @param inData 设置蜂鸣器返回的原始数据
'''         * @return true: 设置成功  ,flase:设置设备
'''         
		Function parseBeepData(ByVal inData() As Byte) As Boolean

'''        
'''         * 解析扫描条码返回的数据
'''         * @param inData  蓝牙返回的条码原始数据
'''         * @return 返回解析后的条码数据
'''         
		Function parseBarcodeData(ByVal inData() As Byte) As Byte()

'''        
'''         * 解析获取电量返回的数据
'''         * @param inData  蓝牙返回的原始数据
'''         * @return 返回解析后的电量
'''         
		Function parseBatteryData(ByVal inData() As Byte) As Integer

'''        
'''         * 解析写标签返回的数据
'''         * @param inData 蓝牙返回的原始数据
'''         * @return true:写标签成功    false:写标签失败
'''         
		Function parseWriteData(ByVal inData() As Byte) As Boolean

'''        
'''         * 解析读标签返回的数据
'''         * @param inData 蓝牙返回的原始数据
'''         * @return 返回解析后的标签数据
'''         
		Function parseReadData(ByVal inData() As Byte) As String

'''        
'''         *  解析锁标签返回的数据
'''         * @param inData 蓝牙返回的原始数据
'''         * @return true:锁成功， false:锁失败
'''         
		Function parseLockData(ByVal inData() As Byte) As Boolean

'''        
'''         * 解析销毁标签返回的数据
'''         * 
'''         * @param inData 蓝牙返回的原始数据
'''         * @return true:销毁标签成功， false:销毁标签失败
'''         
		Function parseKillData(ByVal inData() As Byte) As Boolean


'''        
'''         * 解析单次盘点标签返回的数据
'''         * @return 返回单次盘点标签的数据
'''         
		Function parseInventorySingleTagData(ByVal data() As Byte) As UHFTAGInfo

'''        
'''        * 解析盘点标签返回的数据
'''        * @return 返回单次盘点标签的数据
'''        
		Function parseInventoryTagData(ByVal data() As Byte) As List(Of UHFTAGInfo)
'''        
'''         * 解析设置功率返回的数据
'''         * @param inData 蓝牙返回的原始数据
'''         * @return true:设置功率成功, flase:设置功率失败
'''         
		Function parseSetPowerData(ByVal iData() As Byte) As Boolean

'''        
'''         * 解析获取功率返回的数据
'''         * @param inData 蓝牙返回的原始数据
'''         * @return 返回功率
'''         
		Function parseGetPowerData(ByVal iData() As Byte) As Integer

'''        
'''         * 解析设置协议返回的数据
'''         * @param inData 蓝牙返回的原始数据
'''         * @return true:设置协议成功， false:设置协议失败
'''         
		Function parseSetProtocolData(ByVal inData() As Byte) As Boolean

'''        
'''         * 解析获取协议返回的数据
'''         * @param inData 蓝牙返回的原始数据
'''         * @return true:获取协议成功， false:获取协议失败
'''         
		Function parseGetProtocolData(ByVal inData() As Byte) As Integer

'''        
'''         * 解析开始盘点标签返回的数据
'''         * 
'''         * @return true:开始盘点成功，false:开始盘点失败
'''         
		Function parseStartInventoryTagData() As Boolean



'''        
'''         * 解析停止盘点标签返回的数据
'''         * 
'''         * @return true:停止盘点成功，false:停止盘点失败
'''         
		Function parseStopInventoryData(ByVal inData() As Byte) As Boolean

		'************************************send begin*********************************************************************
'''        
'''         * 
'''         * 获取设置蜂鸣器的发送数据
'''         * 
'''         * @param isOpen  true:表示打开蜂鸣器， false:表示关闭蜂鸣器
'''         * 
'''         * @return 发送的数据
'''         
		Function getBeepSendData(ByVal isOpen As Boolean) As Byte()

'''        
'''         * 
'''         * 获取扫描条码的发送数据
'''         * 
'''         * @return 发送的数据
'''         
		Function getScanBarcodeSendData() As Byte()

'''        
'''         * 
'''         * 获取电量的发送数据
'''         * 
'''         * @return 发送的数据
'''         
		Function getBatterySendData() As Byte()

'''        
'''         * 获取写标签的发送数据<br>
'''         * 
'''         * @param accessPwd
'''         *            标签的ACCESS PASSWORD（4字 节）<br>
'''         * @param filterBank
'''         *            过滤的数据块<br>
'''         * @param filterPtr
'''         *            过滤的起始地址(单位:bit)<br>
'''         * @param filterCnt
'''         *            过滤的数据长度(单位:bit)<br>
'''         * @param filterData
'''         *            过滤的数据<br>
'''         * @param bank
'''         *            写入的数据块<br>
'''         * @param ptr
'''         *            写入的起始地址(单位:字)<br>
'''         * @param cnt
'''         *            写入的数据长度(单位:字)<br>
'''         * @param writeData
'''         *            写入的数据
'''         *            
'''         * @return    发送的数据
'''         * 
'''         
		Function getWriteSendData(ByVal accessPwd As String, ByVal filterBank As Integer, ByVal filterPtr As Integer, ByVal filterCnt As Integer, ByVal filterData As String, ByVal bank As Integer, ByVal ptr As Integer, ByVal cnt As Integer, ByVal writeData As String) As Byte()

'''        
'''         * 获取读标签的发送数据
'''         * 
'''         * @param accessPwd
'''         *            访问密码<br>
'''         * @param filterBank
'''         *            过滤的数据块<br>
'''         * @param filterPtr
'''         *            过滤的起始地址(单位:bit)<br>
'''         * @param filterCnt
'''         *            过滤的数据长度(单位:bit)<br>
'''         * @param filterData
'''         *            过滤的数据<br>
'''         * @param bank
'''         *            读取的数据块<br>
'''         * @param ptr
'''         *            读取的起始地址(单位:字)<br>
'''         * @param cnt
'''         *            读取的数据长度(单位:字)<br>
'''         * 
'''         * @return 发送的数据<br>
'''         
		Function getReadSendData(ByVal accessPwd As String, ByVal filterBank As Integer, ByVal filterPtr As Integer, ByVal filterCnt As Integer, ByVal filterData As String, ByVal bank As Integer, ByVal ptr As Integer, ByVal cnt As Integer) As Byte()

'''        
'''         * 获取锁标签需要发送的数据 <br>
'''         * 
'''         * @param accessPwd
'''         *            标签的ACCESS PASSWORD（4字 节）<br>
'''         * @param filterBank
'''         *            标签的存储区<br>
'''         * @param filterPtr
'''         *            过滤起始地址(单位:bit)<br>
'''         * @param filterCnt
'''         *            过滤数据长度(单位:bit)<br>
'''         * @param filterData
'''         *            过滤数据<br>
'''         * @param lockCode
'''         *            锁定码<br>
'''         * @return 发送的数据<br>
'''         * 
'''         
		Function getLockSendData(ByVal accessPwd As String, ByVal filterBank As Integer, ByVal filterPtr As Integer, ByVal filterCnt As Integer, ByVal filterData As String, ByVal lockCode As String) As Byte()

'''        
'''         * 获取锁标签的锁定码
'''         * @param lockBank 要锁定的区域
'''         * @param lockMode 锁定的模式
'''         * @return 返回null 表示失败
'''         
		Function generateLockCode(ByVal lockBank As List(Of Integer), ByVal lockMode As Integer) As String

'''        
'''         * 获取销毁标签的发送数据
'''         * 
'''         * @param accessPwd
'''         *            标签的ACCESS PASSWORD（4字 节）<br>
'''         * @param filterBank
'''         *            标签的存储区<br>
'''         * @param filterPtr
'''         *            过滤起始地址(单位:bit)<br>
'''         * @param filterCnt
'''         *            过滤数据长度(单位:bit)<br>
'''         * @param filterData
'''         *            过滤数据<br>
'''         *            
'''         * @return  发送的数据<br>
'''         
		Function getKillSendData(ByVal accessPwd As String, ByVal filterBank As Integer, ByVal filterPtr As Integer, ByVal filterCnt As Integer, ByVal filterData As String) As Byte()


'''        
'''         * 
'''         * 获取协议需要发送数据<br>
'''         * 
'''         * @return  发送的数据 
'''         * 
'''         
		Function getProtocolSendData() As Byte()

'''        
'''         * 
'''         * 获取设置协议的发送数据<br>
'''         * @param protocol
'''         *            0x00 表示 ISO18000-6C 协议,  0x01 表示 GB/T 29768 国标协议,  0x02 表示 GJB 7377.1 国军标协议
'''         * @return  发送的数据 
'''         * 
'''         
		Function setProtocolSendData(ByVal protocol As Integer) As Byte()

'''        
'''         * 
'''         * 获取功率的发送数据<br>
'''         * 
'''         * @return 发送的数据 
'''         
		Function getPowerSendData() As Byte()

'''        
'''         * 获取设置功率的发送数据<br>
'''         * 
'''         * @param power 功率
'''         * @return  发送的数据 
'''         
		Function setPowerSendData(ByVal power As Integer, ByVal sava As Boolean) As Byte()

'''        
'''         * 获取开启单次盘点标签的发送数据
'''         * 
'''         * @return 发送的数据 
'''         
		Function getInventorySingleTagSendData() As Byte()

'''        
'''         * 获取循环盘点标签的发送数据
'''         * 
'''         * @return 发送的数据 
'''         
		Function getStartInventoryTagSendData() As Byte()

'''        
'''         * 获取停止循环盘点标签的发送数据
'''         * 
'''         * @return 发送的数据 
'''         
		Function getStopInventorySendData() As Byte()

'''        
'''         *获取在循环盘点标签的模式中,读取缓存标签的发送数据
'''         * 
'''         * @return 发送的数据 
'''         
		Function getReadTagSendData() As Byte()

		''' <summary>
		''' 获取R2、R6缓存数据数量
		''' </summary>
		''' <returns></returns>
		Function getAllTagNumFromFlashSendData() As Byte()

		''' <summary>
		''' 解析R2 R6获取数量返回的数据
		''' </summary>
		''' <param name="inData"></param>
		''' <returns></returns>
		Function parseTagNumFromFlashData(ByVal inData() As Byte) As Integer

		''' <summary>
		''' 删除flash缓存的标签
		''' </summary>
		''' <returns></returns>      
		Function getDeleteAllTagToFlashSendData() As Byte()

		''' <summary>
		''' 解析删除flash,标签返回的数据
		''' </summary>
		''' <param name="inData"></param>
		''' <returns></returns>
		Function parseDeleteAllTagToFlashData(ByVal inData() As Byte) As Boolean

		''' <summary>
		''' 获取flash缓存的标签在发送数据
		''' </summary>
		''' <returns></returns>
		Function getTagDataFromFlashSendData() As Byte()

		''' <summary>
		''' 解析读取flash 缓存返回的数据
		''' </summary>
		''' <param name="inData"></param>
		''' <returns></returns>
		Function parseTagDataFromFlashData(ByVal inData() As Byte) As List(Of UHFTAGInfo)

		Function setFrequencyModeSendData(ByVal save As Byte, ByVal freMode As Integer) As Byte()
		Function parseSetFrequencyModeData(ByVal inData() As Byte) As Boolean

		Function getFrequencyModeSendData() As Byte()
		Function parseGetFrequencyModeData(ByVal inData() As Byte) As Integer

		Function parserUhfTagBuff_EpcTidUser(ByVal tagsBuff() As Byte) As List(Of UHFTAGInfo)


		''' <summary>
		''' 获取蜂鸣器状态发送的数据
		''' </summary>
		''' <param name="inData"></param>
		''' <returns></returns>
		Function getBeepStatusSendData() As Byte()
		''' <summary>
		''' 解析获取蜂鸣器返回的数据
		''' </summary>
		''' <param name="inData"></param>
		''' <returns></returns>
		Function parseGetBeepStatusData(ByVal inData() As Byte) As Integer

		''' <summary>
		''' 获取模块温度
		''' </summary>
		''' <param name="inData"></param>
		''' <returns></returns>
		Function parseTemperatureData(ByVal inData() As Byte) As Integer
		Function getTemperatureSendData() As Byte()

		''' <summary>
		''' 硬件版本
		''' </summary>
		''' <returns></returns>
		Function getHardwareVersionSendData() As Byte()
		Function parseHardwareVersionData(ByVal inData() As Byte) As String

		''' <summary>
		''' 软件版本
		''' </summary>
		''' <returns></returns>
		Function getSoftwareVersionSendData() As Byte()
		Function parseSoftwareVersionData(ByVal inData() As Byte) As String

		''' <summary>
		''' 模块id
		''' </summary>
		''' <returns></returns>
		Function getDeviceIDSendData() As Byte()
		Function parseDeviceIDData(ByVal inData() As Byte) As Integer

		'盘点过滤
		Function setFilterSendData(ByVal saveflag As Byte, ByVal bank As Byte, ByVal startaddr As Integer, ByVal datalen As Integer, ByVal databuf() As Byte) As Byte()
		Function parseFilterData(ByVal inData() As Byte) As Boolean

		'Gen2
		Function setGen2SendData(ByVal Target As Byte, ByVal Action As Byte, ByVal T As Byte, ByVal Q As Byte, ByVal StartQ As Byte, ByVal MinQ As Byte, ByVal MaxQ As Byte, ByVal D As Byte, ByVal C As Byte, ByVal P As Byte, ByVal Sel As Byte, ByVal Session As Byte, ByVal G As Byte, ByVal LF As Byte) As Byte()
		Function parseSetGen2Data(ByVal inData() As Byte) As Boolean

		Function getGen2SendData() As Byte()
		Function parseGetGen2Data(ByVal inData() As Byte, ByVal inLen As Integer, ByRef Target As Byte, ByRef Action As Byte, ByRef T As Byte, ByRef Q As Byte, ByRef StartQ As Byte, ByRef MinQ As Byte, ByRef MaxQ As Byte, ByRef D As Byte, ByRef Coding As Byte, ByRef P As Byte, ByRef Sel As Byte, ByRef Session As Byte, ByRef G As Byte, ByRef LF As Byte) As Boolean

		#Region "天线"
		Function setANTSendData(ByVal saveflag As Byte, ByVal buf() As Byte) As Byte()
		Function parseSetANTDData(ByVal inData() As Byte) As Boolean

		Function getANTSendData() As Byte()
		Function parseGetANTDData(ByVal inData() As Byte) As Byte()
		'--------
		Function getANTWorkTimeSendData(ByVal antnum As Byte) As Byte()
		Function parseGetANTWorkTimeData(ByVal inData() As Byte) As Integer

		Function setANTWorkTimeSendData(ByVal antnum As Byte, ByVal saveflag As Byte, ByVal WorkTime As Integer) As Byte()
		Function parseSetANTWorkTimeData(ByVal inData() As Byte) As Boolean
		#End Region

		#Region "链路"
		Function setRFLinkSendData(ByVal saveflag As Byte, ByVal mode As Byte) As Byte()
		Function getRFLinkSendData() As Byte()
		Function parseGetRFLinkData(ByVal inData() As Byte) As Integer
		Function parseSetRFLinkData(ByVal inData() As Byte) As Boolean

		#End Region

		#Region "EPC+TID"
		'byte[] setEPCTIDModeSendData(byte saveflag, byte mode);
		'bool parseSetEPCTIDModeData(byte[] inData);
		'byte[] getEPCTIDModeSendData();
		'int parseGetEPCTIDModeData(byte[] inData);
		#End Region

		#Region "IP"
		Function setDestIpSendData(ByVal ipbuf() As Byte, ByVal postbuff() As Byte) As Byte()
		Function getIpSendData() As Byte()
		Function parseGetIpData(ByVal inData() As Byte, ByVal ipbuf() As Byte, ByVal postbuff() As Byte) As Boolean
		Function parseSetIpData(ByVal inData() As Byte) As Boolean
		Function setIpSendData(ByVal ipbuf() As Byte, ByVal postbuff() As Byte) As Byte()
		Function getDestIpSendData() As Byte()
		Function parseGetDestIpData(ByVal inData() As Byte, ByVal ipbuf() As Byte, ByVal postbuff() As Byte) As Boolean
		Function parseSetDestIpData(ByVal inData() As Byte) As Boolean
		#End Region

		#Region "focus"
		Function setTagfocusSendData(ByVal flag As Byte) As Byte()
		Function parseSetTagfocusData(ByVal inData() As Byte) As Boolean
		Function getTagfocusSendData() As Byte()
		Function parseGetTagfocusData(ByVal inData() As Byte) As Integer
		#End Region


		#Region " FastID"
		Function setFastIDSendData(ByVal flag As Byte) As Byte()
		Function parseSetFastIDData(ByVal inData() As Byte) As Boolean

		Function getFastIDSendData() As Byte()
		Function parseGetFastIDData(ByVal inData() As Byte) As Integer
		#End Region

		#Region "cw 连续波"
		Function setCWSendData(ByVal flag As Byte) As Byte()
		Function parseSetCWData(ByVal inData() As Byte) As Boolean

		Function getCWSendData() As Byte()
		Function parseGetCWData(ByVal inData() As Byte) As Integer
		#End Region

		#Region " 工作模式 (命令工作模式、自动模式)"

		Function setSetWorkModeSendData(ByVal mode As Byte) As Byte()
		Function parseSetWorkModeData(ByVal inData() As Byte) As Boolean

		Function getWorkModeSendData() As Byte()
		Function parseGetWorkModeData(ByVal inData() As Byte) As Integer

		#End Region

		#Region "复位"

		Function setSoftResetSendData() As Byte()
		Function parseSetSoftResetData(ByVal inData() As Byte) As Boolean

		#End Region


		#Region "gpio、继电器"

		 Function setIOControlSendData(ByVal output1 As Byte, ByVal output2 As Byte, ByVal outStatus As Byte) As Byte()
		 Function parseSetIOControlData(ByVal inData() As Byte) As Boolean

		 Function getIOControlSendData() As Byte()
		 Function UHFGetIOControl_RecvData(ByVal inData() As Byte) As Byte()
		#End Region

		#Region "BlockPermalock"
		 Function getBlockPermalockSendData(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal ReadLock As Byte, ByVal uBank As Byte, ByVal uPtr As Integer, ByVal uRange As Byte, ByVal uMaskbuf() As Byte) As Byte()
		 Function parseBlockPermalocData(ByVal uRange As Byte, ByVal inData() As Byte, ByVal uMaskbuf() As Byte) As Boolean
		#End Region

		#Region "擦除"
		 Function blockEraseDataSendData(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal uBank As Byte, ByVal uPtr As Integer, ByVal uCnt As Byte) As Byte()
		 Function parseBlockEraseData(ByVal inData() As Byte) As Boolean

		 Function blockWriteDataSendData(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal uBank As Byte, ByVal uPtr As Integer, ByVal uCnt As Integer, ByVal uWriteDatabuf() As Byte) As Byte()
		 Function parseBlockWriteData(ByVal inData() As Byte) As Boolean
		#End Region

		 #Region "QT"

		 Function setQTSendData(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal QTData As Byte) As Byte()
		 Function parseSetQTData(ByVal inData() As Byte) As Boolean


		 Function getQTSendData(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte) As Byte()
		 Function parseGetQTData(ByVal inData() As Byte) As Integer


		#End Region

		 Function getGBTagLockSendData(ByVal uAccessPwd() As Byte, ByVal FilterBank As Byte, ByVal FilterStartaddr As Integer, ByVal FilterLen As Integer, ByVal FilterData() As Byte, ByVal memory As Byte, ByVal config As Byte, ByVal action As Byte) As Byte()
		 Function parseGBTagLockData(ByVal inData() As Byte) As Boolean



		Function setJumpFrequencySendData(ByVal nums As Byte, ByVal Freqbuf() As Integer) As Byte()
		Function parseSetJumpFrequencyData(ByVal inData() As Byte) As Boolean
		Function getJumpFrequencySendData() As Byte()
		Function parseGetJumpFrequencyData(ByVal inData() As Byte) As Integer()

		Function setEpcTidUserModeSendData(ByVal saveFlag As Byte, ByVal memory As Byte, ByVal userAddress As Byte, ByVal useLen As Byte) As Byte()
		Function parseSetEpcTidUserModeyData(ByVal inData() As Byte) As Boolean

		Function getEpcTidUserModeSendData() As Byte()
		Function parseGetEpcTidUserModeyData(ByVal inData() As Byte, ByRef mode As Integer, ByRef userAddress As Byte, ByRef useLen As Byte) As Boolean


		#Region ""
		Function jump2BootSendData(ByVal flag As Integer) As Byte()
		Function parseJump2BootData(ByVal inData() As Byte) As Boolean

		Function startUpdSendData() As Byte()
		Function parseStartUpdData(ByVal inData() As Byte) As Boolean

		Function updatingSendData(ByVal data() As Byte) As Byte()
		Function parseUpdatingData(ByVal inData() As Byte) As Boolean

		Function stopUpdateSendData() As Byte()
		Function parseStopUpdateData(ByVal inData() As Byte) As Boolean
		#End Region
	End Interface
End Namespace

