Namespace UHFAPP
	Partial Public Class ConfigForm
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.groupBox1 = New System.Windows.Forms.GroupBox()
			Me.btnGetCW = New System.Windows.Forms.Button()
			Me.btnSetCW = New System.Windows.Forms.Button()
			Me.groupBox3 = New System.Windows.Forms.GroupBox()
			Me.cbRFLink = New System.Windows.Forms.CheckBox()
			Me.btnRFLinkGet = New System.Windows.Forms.Button()
			Me.btnRFLinkSet = New System.Windows.Forms.Button()
			Me.cmbRFLink = New System.Windows.Forms.ComboBox()
			Me.label5 = New System.Windows.Forms.Label()
			Me.groupBox5 = New System.Windows.Forms.GroupBox()
			Me.label2 = New System.Windows.Forms.Label()
			Me.btnGen2Get = New System.Windows.Forms.Button()
			Me.btnGen2Set = New System.Windows.Forms.Button()
			Me.cmbLinkFrequency = New System.Windows.Forms.ComboBox()
			Me.cmbG = New System.Windows.Forms.ComboBox()
			Me.cmbSession = New System.Windows.Forms.ComboBox()
			Me.label26 = New System.Windows.Forms.Label()
			Me.label22 = New System.Windows.Forms.Label()
			Me.cmbSel = New System.Windows.Forms.ComboBox()
			Me.cmbP = New System.Windows.Forms.ComboBox()
			Me.cmbCoding = New System.Windows.Forms.ComboBox()
			Me.cmbDr = New System.Windows.Forms.ComboBox()
			Me.cmbMaxQ = New System.Windows.Forms.ComboBox()
			Me.cmbMinQ = New System.Windows.Forms.ComboBox()
			Me.cmbStartQ = New System.Windows.Forms.ComboBox()
			Me.label21 = New System.Windows.Forms.Label()
			Me.label20 = New System.Windows.Forms.Label()
			Me.label19 = New System.Windows.Forms.Label()
			Me.label18 = New System.Windows.Forms.Label()
			Me.label17 = New System.Windows.Forms.Label()
			Me.label16 = New System.Windows.Forms.Label()
			Me.label15 = New System.Windows.Forms.Label()
			Me.label14 = New System.Windows.Forms.Label()
			Me.cmbQ = New System.Windows.Forms.ComboBox()
			Me.label13 = New System.Windows.Forms.Label()
			Me.cmbT = New System.Windows.Forms.ComboBox()
			Me.label12 = New System.Windows.Forms.Label()
			Me.cmbAction = New System.Windows.Forms.ComboBox()
			Me.label11 = New System.Windows.Forms.Label()
			Me.cmbTarget = New System.Windows.Forms.ComboBox()
			Me.label10 = New System.Windows.Forms.Label()
			Me.groupBox6 = New System.Windows.Forms.GroupBox()
			Me.cbPower = New System.Windows.Forms.CheckBox()
			Me.cmbPower_ANT1 = New System.Windows.Forms.ComboBox()
			Me.label24 = New System.Windows.Forms.Label()
			Me.btnPowerGet_ANT1 = New System.Windows.Forms.Button()
			Me.label23 = New System.Windows.Forms.Label()
			Me.btnPowerSet_ANT1 = New System.Windows.Forms.Button()
			Me.groupBox7 = New System.Windows.Forms.GroupBox()
			Me.comboBox1 = New System.Windows.Forms.ComboBox()
			Me.textBox1 = New System.Windows.Forms.TextBox()
			Me.label6 = New System.Windows.Forms.Label()
			Me.btnWorkModeGet = New System.Windows.Forms.Button()
			Me.btnWorkModeSet = New System.Windows.Forms.Button()
			Me.label28 = New System.Windows.Forms.Label()
			Me.label27 = New System.Windows.Forms.Label()
			Me.panel1 = New System.Windows.Forms.Panel()
			Me.groupBox4 = New System.Windows.Forms.GroupBox()
			Me.groupBox10 = New System.Windows.Forms.GroupBox()
			Me.cmbInput1 = New System.Windows.Forms.ComboBox()
			Me.button12 = New System.Windows.Forms.Button()
			Me.cmbInput2 = New System.Windows.Forms.ComboBox()
			Me.label55 = New System.Windows.Forms.Label()
			Me.label56 = New System.Windows.Forms.Label()
			Me.groupBox17 = New System.Windows.Forms.GroupBox()
			Me.cmbOutput2 = New System.Windows.Forms.ComboBox()
			Me.label58 = New System.Windows.Forms.Label()
			Me.button13 = New System.Windows.Forms.Button()
			Me.cmbOutput1 = New System.Windows.Forms.ComboBox()
			Me.label57 = New System.Windows.Forms.Label()
			Me.btnReset = New System.Windows.Forms.Button()
			Me.groupBox25 = New System.Windows.Forms.GroupBox()
			Me.btnCalibration = New System.Windows.Forms.Button()
			Me.txtCalibration = New System.Windows.Forms.TextBox()
			Me.gbInventoryMode = New System.Windows.Forms.GroupBox()
			Me.checkBox2 = New System.Windows.Forms.CheckBox()
			Me.txtUserLen = New System.Windows.Forms.TextBox()
			Me.label47 = New System.Windows.Forms.Label()
			Me.txtUserPtr = New System.Windows.Forms.TextBox()
			Me.label46 = New System.Windows.Forms.Label()
			Me.cbInventoryMode = New System.Windows.Forms.ComboBox()
			Me.label45 = New System.Windows.Forms.Label()
			Me.button10 = New System.Windows.Forms.Button()
			Me.button11 = New System.Windows.Forms.Button()
			Me.groupBox8 = New System.Windows.Forms.GroupBox()
			Me.label36 = New System.Windows.Forms.Label()
			Me.label33 = New System.Windows.Forms.Label()
			Me.textBox3 = New System.Windows.Forms.TextBox()
			Me.button4 = New System.Windows.Forms.Button()
			Me.label32 = New System.Windows.Forms.Label()
			Me.button3 = New System.Windows.Forms.Button()
			Me.groupBox24 = New System.Windows.Forms.GroupBox()
			Me.checkBox1 = New System.Windows.Forms.CheckBox()
			Me.button8 = New System.Windows.Forms.Button()
			Me.button9 = New System.Windows.Forms.Button()
			Me.label43 = New System.Windows.Forms.Label()
			Me.label44 = New System.Windows.Forms.Label()
			Me.txtWaitTime = New System.Windows.Forms.TextBox()
			Me.txtworkTime = New System.Windows.Forms.TextBox()
			Me.label42 = New System.Windows.Forms.Label()
			Me.label41 = New System.Windows.Forms.Label()
			Me.bgGPIO = New System.Windows.Forms.GroupBox()
			Me.groupBox23 = New System.Windows.Forms.GroupBox()
			Me.comboBox2 = New System.Windows.Forms.ComboBox()
			Me.button6 = New System.Windows.Forms.Button()
			Me.comboBox3 = New System.Windows.Forms.ComboBox()
			Me.label39 = New System.Windows.Forms.Label()
			Me.label40 = New System.Windows.Forms.Label()
			Me.groupBox22 = New System.Windows.Forms.GroupBox()
			Me.button7 = New System.Windows.Forms.Button()
			Me.cmbOutStatus = New System.Windows.Forms.ComboBox()
			Me.label38 = New System.Windows.Forms.Label()
			Me.groupBox19 = New System.Windows.Forms.GroupBox()
			Me.button2 = New System.Windows.Forms.Button()
			Me.button5 = New System.Windows.Forms.Button()
			Me.cmbProtocol = New System.Windows.Forms.ComboBox()
			Me.label35 = New System.Windows.Forms.Label()
			Me.gbWorkMode = New System.Windows.Forms.GroupBox()
			Me.plWorkModePara = New System.Windows.Forms.Panel()
			Me.txtIT = New System.Windows.Forms.TextBox()
			Me.label52 = New System.Windows.Forms.Label()
			Me.btnWorkModeParaGet = New System.Windows.Forms.Button()
			Me.btnWorkModeParaSet = New System.Windows.Forms.Button()
			Me.comRM = New System.Windows.Forms.ComboBox()
			Me.Mode = New System.Windows.Forms.Label()
			Me.label50 = New System.Windows.Forms.Label()
			Me.txtWT = New System.Windows.Forms.TextBox()
			Me.label49 = New System.Windows.Forms.Label()
			Me.cmbInput = New System.Windows.Forms.ComboBox()
			Me.label48 = New System.Windows.Forms.Label()
			Me.label51 = New System.Windows.Forms.Label()
			Me.workMode = New System.Windows.Forms.ComboBox()
			Me.button1 = New System.Windows.Forms.Button()
			Me.btnGetWorkMode = New System.Windows.Forms.Button()
			Me.label29 = New System.Windows.Forms.Label()
			Me.gbIp2 = New System.Windows.Forms.GroupBox()
			Me.ipControlDest = New WindowsFormsControlLibrary1.IPControl()
			Me.btnSetIpDest = New System.Windows.Forms.Button()
			Me.btnGetIpDest = New System.Windows.Forms.Button()
			Me.txtPortDest = New System.Windows.Forms.TextBox()
			Me.label30 = New System.Windows.Forms.Label()
			Me.label31 = New System.Windows.Forms.Label()
			Me.groupBox9 = New System.Windows.Forms.GroupBox()
			Me.rbDisableBuzzer = New System.Windows.Forms.RadioButton()
			Me.rbEnableBuzzer = New System.Windows.Forms.RadioButton()
			Me.btnSetBuzzer = New System.Windows.Forms.Button()
			Me.btnGetBuzzer = New System.Windows.Forms.Button()
			Me.gbIP = New System.Windows.Forms.GroupBox()
			Me.label54 = New System.Windows.Forms.Label()
			Me.label53 = New System.Windows.Forms.Label()
			Me.ipGateway = New WindowsFormsControlLibrary1.IPControl()
			Me.ipControlSubnetMask = New WindowsFormsControlLibrary1.IPControl()
			Me.ipControlLocal = New WindowsFormsControlLibrary1.IPControl()
			Me.btnSetIPLocal = New System.Windows.Forms.Button()
			Me.btnGetIPLocal = New System.Windows.Forms.Button()
			Me.txtLocalPort = New System.Windows.Forms.TextBox()
			Me.label9 = New System.Windows.Forms.Label()
			Me.label25 = New System.Windows.Forms.Label()
			Me.label8 = New System.Windows.Forms.Label()
			Me.groupBox16 = New System.Windows.Forms.GroupBox()
			Me.label34 = New System.Windows.Forms.Label()
			Me.groupBox20 = New System.Windows.Forms.GroupBox()
			Me.label37 = New System.Windows.Forms.Label()
			Me.rbDisable = New System.Windows.Forms.RadioButton()
			Me.GetTemperatureProtect = New System.Windows.Forms.Button()
			Me.btnSetTemperatureProtect = New System.Windows.Forms.Button()
			Me.rbEnable = New System.Windows.Forms.RadioButton()
			Me.cbDualSingelSave = New System.Windows.Forms.CheckBox()
			Me.btnDualSingelGet = New System.Windows.Forms.Button()
			Me.rbDual = New System.Windows.Forms.RadioButton()
			Me.rbSingel = New System.Windows.Forms.RadioButton()
			Me.btnDualSingelSet = New System.Windows.Forms.Button()
			Me.groupBox15 = New System.Windows.Forms.GroupBox()
			Me.rbTagfocusDisable = New System.Windows.Forms.RadioButton()
			Me.btnrbTagfocusGet = New System.Windows.Forms.Button()
			Me.btnrbTagfocusSet = New System.Windows.Forms.Button()
			Me.rbTagfocusEnable = New System.Windows.Forms.RadioButton()
			Me.groupBox14 = New System.Windows.Forms.GroupBox()
			Me.rbFastIDDisable = New System.Windows.Forms.RadioButton()
			Me.rbFastIDEnable = New System.Windows.Forms.RadioButton()
			Me.btnFastIDGet = New System.Windows.Forms.Button()
			Me.btnFastIDSet = New System.Windows.Forms.Button()
			Me.groupBox11 = New System.Windows.Forms.GroupBox()
			Me.cmbRegion = New System.Windows.Forms.ComboBox()
			Me.cbRegionSave = New System.Windows.Forms.CheckBox()
			Me.btnRegionGet = New System.Windows.Forms.Button()
			Me.btnRegionSet = New System.Windows.Forms.Button()
			Me.label1 = New System.Windows.Forms.Label()
			Me.gbAnt = New System.Windows.Forms.GroupBox()
			Me.groupBox2 = New System.Windows.Forms.GroupBox()
			Me.btnAntennaConnectionState = New System.Windows.Forms.Button()
			Me.cbANT2_state = New System.Windows.Forms.CheckBox()
			Me.cbANT1_state = New System.Windows.Forms.CheckBox()
			Me.cbANT3_state = New System.Windows.Forms.CheckBox()
			Me.cbANT4_state = New System.Windows.Forms.CheckBox()
			Me.cbANT5_state = New System.Windows.Forms.CheckBox()
			Me.cbANT6_state = New System.Windows.Forms.CheckBox()
			Me.cbANT8_state = New System.Windows.Forms.CheckBox()
			Me.cbANT7_state = New System.Windows.Forms.CheckBox()
			Me.groupBox12 = New System.Windows.Forms.GroupBox()
			Me.label7 = New System.Windows.Forms.Label()
			Me.cbAntWorkTime = New System.Windows.Forms.CheckBox()
			Me.btnGetANTWorkTime = New System.Windows.Forms.Button()
			Me.btnSetANTWorkTime = New System.Windows.Forms.Button()
			Me.label3 = New System.Windows.Forms.Label()
			Me.cmbAntWorkTime = New System.Windows.Forms.ComboBox()
			Me.txtAntWorkTime = New System.Windows.Forms.TextBox()
			Me.label4 = New System.Windows.Forms.Label()
			Me.groupBox13 = New System.Windows.Forms.GroupBox()
			Me.btnGetANT = New System.Windows.Forms.Button()
			Me.cmbAnt16 = New System.Windows.Forms.CheckBox()
			Me.cbAntSet = New System.Windows.Forms.CheckBox()
			Me.cmbAnt15 = New System.Windows.Forms.CheckBox()
			Me.btnSetAnt = New System.Windows.Forms.Button()
			Me.cmbAnt14 = New System.Windows.Forms.CheckBox()
			Me.cmbAnt2 = New System.Windows.Forms.CheckBox()
			Me.cmbAnt13 = New System.Windows.Forms.CheckBox()
			Me.cmbAnt1 = New System.Windows.Forms.CheckBox()
			Me.cmbAnt12 = New System.Windows.Forms.CheckBox()
			Me.cmbAnt3 = New System.Windows.Forms.CheckBox()
			Me.cmbAnt11 = New System.Windows.Forms.CheckBox()
			Me.cmbAnt4 = New System.Windows.Forms.CheckBox()
			Me.cmbAnt10 = New System.Windows.Forms.CheckBox()
			Me.cmbAnt5 = New System.Windows.Forms.CheckBox()
			Me.cmbAnt9 = New System.Windows.Forms.CheckBox()
			Me.cmbAnt6 = New System.Windows.Forms.CheckBox()
			Me.cmbAnt8 = New System.Windows.Forms.CheckBox()
			Me.cmbAnt7 = New System.Windows.Forms.CheckBox()
			Me.groupBox1.SuspendLayout()
			Me.groupBox3.SuspendLayout()
			Me.groupBox5.SuspendLayout()
			Me.groupBox6.SuspendLayout()
			Me.groupBox7.SuspendLayout()
			Me.panel1.SuspendLayout()
			Me.groupBox4.SuspendLayout()
			Me.groupBox10.SuspendLayout()
			Me.groupBox17.SuspendLayout()
			Me.groupBox25.SuspendLayout()
			Me.gbInventoryMode.SuspendLayout()
			Me.groupBox8.SuspendLayout()
			Me.groupBox24.SuspendLayout()
			Me.bgGPIO.SuspendLayout()
			Me.groupBox23.SuspendLayout()
			Me.groupBox22.SuspendLayout()
			Me.groupBox19.SuspendLayout()
			Me.gbWorkMode.SuspendLayout()
			Me.plWorkModePara.SuspendLayout()
			Me.gbIp2.SuspendLayout()
			Me.groupBox9.SuspendLayout()
			Me.gbIP.SuspendLayout()
			Me.groupBox16.SuspendLayout()
			Me.groupBox20.SuspendLayout()
			Me.groupBox15.SuspendLayout()
			Me.groupBox14.SuspendLayout()
			Me.groupBox11.SuspendLayout()
			Me.gbAnt.SuspendLayout()
			Me.groupBox2.SuspendLayout()
			Me.groupBox12.SuspendLayout()
			Me.groupBox13.SuspendLayout()
			Me.SuspendLayout()
			' 
			' groupBox1
			' 
			Me.groupBox1.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.groupBox1.Controls.Add(Me.btnGetCW)
			Me.groupBox1.Controls.Add(Me.btnSetCW)
			Me.groupBox1.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox1.ForeColor = System.Drawing.Color.Black
			Me.groupBox1.Location = New System.Drawing.Point(902, 291)
			Me.groupBox1.Name = "groupBox1"
			Me.groupBox1.Size = New System.Drawing.Size(355, 68)
			Me.groupBox1.TabIndex = 0
			Me.groupBox1.TabStop = False
			Me.groupBox1.Text = "cw"
			' 
			' btnGetCW
			' 
			Me.btnGetCW.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnGetCW.ForeColor = System.Drawing.Color.Black
			Me.btnGetCW.Location = New System.Drawing.Point(95, 20)
			Me.btnGetCW.Name = "btnGetCW"
			Me.btnGetCW.Size = New System.Drawing.Size(92, 31)
			Me.btnGetCW.TabIndex = 25
			Me.btnGetCW.Text = "ON"
			Me.btnGetCW.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnGetCW.Click += new System.EventHandler(this.btnGetCW_Click);
			' 
			' btnSetCW
			' 
			Me.btnSetCW.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnSetCW.ForeColor = System.Drawing.Color.Black
			Me.btnSetCW.Location = New System.Drawing.Point(214, 20)
			Me.btnSetCW.Name = "btnSetCW"
			Me.btnSetCW.Size = New System.Drawing.Size(90, 31)
			Me.btnSetCW.TabIndex = 24
			Me.btnSetCW.Text = "OFF"
			Me.btnSetCW.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnSetCW.Click += new System.EventHandler(this.btnSetCW_Click);
			' 
			' groupBox3
			' 
			Me.groupBox3.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.groupBox3.Controls.Add(Me.cbRFLink)
			Me.groupBox3.Controls.Add(Me.btnRFLinkGet)
			Me.groupBox3.Controls.Add(Me.btnRFLinkSet)
			Me.groupBox3.Controls.Add(Me.cmbRFLink)
			Me.groupBox3.Controls.Add(Me.label5)
			Me.groupBox3.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox3.ForeColor = System.Drawing.Color.Black
			Me.groupBox3.Location = New System.Drawing.Point(9, 319)
			Me.groupBox3.Name = "groupBox3"
			Me.groupBox3.Size = New System.Drawing.Size(406, 93)
			Me.groupBox3.TabIndex = 27
			Me.groupBox3.TabStop = False
			Me.groupBox3.Text = "RFLink"
			' 
			' cbRFLink
			' 
			Me.cbRFLink.AutoSize = True
			Me.cbRFLink.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbRFLink.Location = New System.Drawing.Point(313, 62)
			Me.cbRFLink.Name = "cbRFLink"
			Me.cbRFLink.Size = New System.Drawing.Size(59, 20)
			Me.cbRFLink.TabIndex = 28
			Me.cbRFLink.Text = "Save"
			Me.cbRFLink.UseVisualStyleBackColor = True
			' 
			' btnRFLinkGet
			' 
			Me.btnRFLinkGet.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnRFLinkGet.ForeColor = System.Drawing.Color.Black
			Me.btnRFLinkGet.Location = New System.Drawing.Point(97, 52)
			Me.btnRFLinkGet.Name = "btnRFLinkGet"
			Me.btnRFLinkGet.Size = New System.Drawing.Size(90, 31)
			Me.btnRFLinkGet.TabIndex = 27
			Me.btnRFLinkGet.Text = "Get"
			Me.btnRFLinkGet.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnRFLinkGet.Click += new System.EventHandler(this.btnRFLinkGet_Click);
			' 
			' btnRFLinkSet
			' 
			Me.btnRFLinkSet.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnRFLinkSet.ForeColor = System.Drawing.Color.Black
			Me.btnRFLinkSet.Location = New System.Drawing.Point(214, 52)
			Me.btnRFLinkSet.Name = "btnRFLinkSet"
			Me.btnRFLinkSet.Size = New System.Drawing.Size(90, 31)
			Me.btnRFLinkSet.TabIndex = 26
			Me.btnRFLinkSet.Text = "Set"
			Me.btnRFLinkSet.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnRFLinkSet.Click += new System.EventHandler(this.btnRFLinkSet_Click);
			' 
			' cmbRFLink
			' 
			Me.cmbRFLink.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbRFLink.FormattingEnabled = True
			Me.cmbRFLink.Items.AddRange(New Object() { "DSB_ASK/FM0/40KHz", "PR_ASK/Miller4.250KHz", "PR_ASK/Miller4/300KHz", "DSB_ASK/FM0/400KHz"})
			Me.cmbRFLink.Location = New System.Drawing.Point(113, 20)
			Me.cmbRFLink.Name = "cmbRFLink"
			Me.cmbRFLink.Size = New System.Drawing.Size(220, 24)
			Me.cmbRFLink.TabIndex = 18
			' 
			' label5
			' 
			Me.label5.AutoSize = True
			Me.label5.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label5.Location = New System.Drawing.Point(48, 26)
			Me.label5.Name = "label5"
			Me.label5.Size = New System.Drawing.Size(64, 16)
			Me.label5.TabIndex = 22
			Me.label5.Text = "RFLink:"
			' 
			' groupBox5
			' 
			Me.groupBox5.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.groupBox5.Controls.Add(Me.label2)
			Me.groupBox5.Controls.Add(Me.btnGen2Get)
			Me.groupBox5.Controls.Add(Me.btnGen2Set)
			Me.groupBox5.Controls.Add(Me.cmbLinkFrequency)
			Me.groupBox5.Controls.Add(Me.cmbG)
			Me.groupBox5.Controls.Add(Me.cmbSession)
			Me.groupBox5.Controls.Add(Me.label26)
			Me.groupBox5.Controls.Add(Me.label22)
			Me.groupBox5.Controls.Add(Me.cmbSel)
			Me.groupBox5.Controls.Add(Me.cmbP)
			Me.groupBox5.Controls.Add(Me.cmbCoding)
			Me.groupBox5.Controls.Add(Me.cmbDr)
			Me.groupBox5.Controls.Add(Me.cmbMaxQ)
			Me.groupBox5.Controls.Add(Me.cmbMinQ)
			Me.groupBox5.Controls.Add(Me.cmbStartQ)
			Me.groupBox5.Controls.Add(Me.label21)
			Me.groupBox5.Controls.Add(Me.label20)
			Me.groupBox5.Controls.Add(Me.label19)
			Me.groupBox5.Controls.Add(Me.label18)
			Me.groupBox5.Controls.Add(Me.label17)
			Me.groupBox5.Controls.Add(Me.label16)
			Me.groupBox5.Controls.Add(Me.label15)
			Me.groupBox5.Controls.Add(Me.label14)
			Me.groupBox5.Controls.Add(Me.cmbQ)
			Me.groupBox5.Controls.Add(Me.label13)
			Me.groupBox5.Controls.Add(Me.cmbT)
			Me.groupBox5.Controls.Add(Me.label12)
			Me.groupBox5.Controls.Add(Me.cmbAction)
			Me.groupBox5.Controls.Add(Me.label11)
			Me.groupBox5.Controls.Add(Me.cmbTarget)
			Me.groupBox5.Controls.Add(Me.label10)
			Me.groupBox5.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox5.ForeColor = System.Drawing.Color.Black
			Me.groupBox5.Location = New System.Drawing.Point(426, 13)
			Me.groupBox5.Name = "groupBox5"
			Me.groupBox5.Size = New System.Drawing.Size(466, 128)
			Me.groupBox5.TabIndex = 28
			Me.groupBox5.TabStop = False
			Me.groupBox5.Text = "Gen2"
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label2.ForeColor = System.Drawing.Color.Maroon
			Me.label2.Location = New System.Drawing.Point(199, 99)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(95, 12)
			Me.label2.TabIndex = 59
			Me.label2.Text = "设置之前先获取."
			' 
			' btnGen2Get
			' 
			Me.btnGen2Get.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnGen2Get.ForeColor = System.Drawing.Color.Black
			Me.btnGen2Get.Location = New System.Drawing.Point(100, 69)
			Me.btnGen2Get.Name = "btnGen2Get"
			Me.btnGen2Get.Size = New System.Drawing.Size(90, 29)
			Me.btnGen2Get.TabIndex = 58
			Me.btnGen2Get.Text = "Get"
			Me.btnGen2Get.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnGen2Get.Click += new System.EventHandler(this.btnGen2Get_Click);
			' 
			' btnGen2Set
			' 
			Me.btnGen2Set.Enabled = False
			Me.btnGen2Set.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnGen2Set.ForeColor = System.Drawing.Color.Black
			Me.btnGen2Set.Location = New System.Drawing.Point(296, 67)
			Me.btnGen2Set.Name = "btnGen2Set"
			Me.btnGen2Set.Size = New System.Drawing.Size(90, 31)
			Me.btnGen2Set.TabIndex = 41
			Me.btnGen2Set.Text = "Set"
			Me.btnGen2Set.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnGen2Set.Click += new System.EventHandler(this.btnGen2Set_Click);
			' 
			' cmbLinkFrequency
			' 
			Me.cmbLinkFrequency.Enabled = False
			Me.cmbLinkFrequency.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbLinkFrequency.FormattingEnabled = True
			Me.cmbLinkFrequency.Items.AddRange(New Object() { "000(40KHz )", "001(160KHz)", "010(200KHz)", "011(250KHz)", "100(300KHz)", "101(320KHz)", "110(400KHz)", "111(640KHz)"})
			Me.cmbLinkFrequency.Location = New System.Drawing.Point(323, 275)
			Me.cmbLinkFrequency.Name = "cmbLinkFrequency"
			Me.cmbLinkFrequency.Size = New System.Drawing.Size(114, 24)
			Me.cmbLinkFrequency.TabIndex = 57
			Me.cmbLinkFrequency.Visible = False
			' 
			' cmbG
			' 
			Me.cmbG.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbG.FormattingEnabled = True
			Me.cmbG.Items.AddRange(New Object() { "0(A)", "1(B)"})
			Me.cmbG.Location = New System.Drawing.Point(312, 26)
			Me.cmbG.Name = "cmbG"
			Me.cmbG.Size = New System.Drawing.Size(114, 24)
			Me.cmbG.TabIndex = 56
			' 
			' cmbSession
			' 
			Me.cmbSession.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbSession.FormattingEnabled = True
			Me.cmbSession.Items.AddRange(New Object() { "00(S0)", "01(S1)", "10(S2)", "11(S3)"})
			Me.cmbSession.Location = New System.Drawing.Point(97, 29)
			Me.cmbSession.Name = "cmbSession"
			Me.cmbSession.Size = New System.Drawing.Size(114, 24)
			Me.cmbSession.TabIndex = 55
			' 
			' label26
			' 
			Me.label26.AutoSize = True
			Me.label26.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label26.Location = New System.Drawing.Point(245, 30)
			Me.label26.Name = "label26"
			Me.label26.Size = New System.Drawing.Size(64, 16)
			Me.label26.TabIndex = 54
			Me.label26.Text = "Target:"
			' 
			' label22
			' 
			Me.label22.AutoSize = True
			Me.label22.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label22.Location = New System.Drawing.Point(197, 283)
			Me.label22.Name = "label22"
			Me.label22.Size = New System.Drawing.Size(120, 16)
			Me.label22.TabIndex = 50
			Me.label22.Text = "linkFrequency:"
			Me.label22.Visible = False
			' 
			' cmbSel
			' 
			Me.cmbSel.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbSel.FormattingEnabled = True
			Me.cmbSel.Items.AddRange(New Object() { "00(ALL)", "01(ALL)", "10(~SL)", "11(SL)"})
			Me.cmbSel.Location = New System.Drawing.Point(89, 261)
			Me.cmbSel.Name = "cmbSel"
			Me.cmbSel.Size = New System.Drawing.Size(114, 24)
			Me.cmbSel.TabIndex = 49
			Me.cmbSel.Visible = False
			' 
			' cmbP
			' 
			Me.cmbP.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbP.FormattingEnabled = True
			Me.cmbP.Items.AddRange(New Object() { "0(No pilot)", "1(Use pilot )"})
			Me.cmbP.Location = New System.Drawing.Point(89, 221)
			Me.cmbP.Name = "cmbP"
			Me.cmbP.Size = New System.Drawing.Size(114, 24)
			Me.cmbP.TabIndex = 48
			Me.cmbP.Visible = False
			' 
			' cmbCoding
			' 
			Me.cmbCoding.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbCoding.FormattingEnabled = True
			Me.cmbCoding.Items.AddRange(New Object() { "00(M=0)", "01(M=2)", "10(M=4)", "11(M=8)"})
			Me.cmbCoding.Location = New System.Drawing.Point(89, 177)
			Me.cmbCoding.Name = "cmbCoding"
			Me.cmbCoding.Size = New System.Drawing.Size(114, 24)
			Me.cmbCoding.TabIndex = 47
			Me.cmbCoding.Visible = False
			' 
			' cmbDr
			' 
			Me.cmbDr.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbDr.FormattingEnabled = True
			Me.cmbDr.Items.AddRange(New Object() { "0(DR=8)", "1(DR=64/3)"})
			Me.cmbDr.Location = New System.Drawing.Point(323, 151)
			Me.cmbDr.Name = "cmbDr"
			Me.cmbDr.Size = New System.Drawing.Size(114, 24)
			Me.cmbDr.TabIndex = 46
			Me.cmbDr.Visible = False
			' 
			' cmbMaxQ
			' 
			Me.cmbMaxQ.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbMaxQ.FormattingEnabled = True
			Me.cmbMaxQ.Items.AddRange(New Object() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15"})
			Me.cmbMaxQ.Location = New System.Drawing.Point(325, 306)
			Me.cmbMaxQ.Name = "cmbMaxQ"
			Me.cmbMaxQ.Size = New System.Drawing.Size(114, 24)
			Me.cmbMaxQ.TabIndex = 45
			Me.cmbMaxQ.Visible = False
			' 
			' cmbMinQ
			' 
			Me.cmbMinQ.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbMinQ.FormattingEnabled = True
			Me.cmbMinQ.Items.AddRange(New Object() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15"})
			Me.cmbMinQ.Location = New System.Drawing.Point(325, 221)
			Me.cmbMinQ.Name = "cmbMinQ"
			Me.cmbMinQ.Size = New System.Drawing.Size(114, 24)
			Me.cmbMinQ.TabIndex = 44
			Me.cmbMinQ.Visible = False
			' 
			' cmbStartQ
			' 
			Me.cmbStartQ.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbStartQ.FormattingEnabled = True
			Me.cmbStartQ.Items.AddRange(New Object() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15"})
			Me.cmbStartQ.Location = New System.Drawing.Point(323, 187)
			Me.cmbStartQ.Name = "cmbStartQ"
			Me.cmbStartQ.Size = New System.Drawing.Size(114, 24)
			Me.cmbStartQ.TabIndex = 43
			Me.cmbStartQ.Visible = False
			' 
			' label21
			' 
			Me.label21.AutoSize = True
			Me.label21.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label21.Location = New System.Drawing.Point(253, 187)
			Me.label21.Name = "label21"
			Me.label21.Size = New System.Drawing.Size(64, 16)
			Me.label21.TabIndex = 42
			Me.label21.Text = "startQ:"
			Me.label21.Visible = False
			' 
			' label20
			' 
			Me.label20.AutoSize = True
			Me.label20.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label20.Location = New System.Drawing.Point(19, 29)
			Me.label20.Name = "label20"
			Me.label20.Size = New System.Drawing.Size(72, 16)
			Me.label20.TabIndex = 41
			Me.label20.Text = "Session:"
			' 
			' label19
			' 
			Me.label19.AutoSize = True
			Me.label19.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label19.Location = New System.Drawing.Point(43, 269)
			Me.label19.Name = "label19"
			Me.label19.Size = New System.Drawing.Size(40, 16)
			Me.label19.TabIndex = 40
			Me.label19.Text = "sel:"
			Me.label19.Visible = False
			' 
			' label18
			' 
			Me.label18.AutoSize = True
			Me.label18.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label18.Location = New System.Drawing.Point(27, 224)
			Me.label18.Name = "label18"
			Me.label18.Size = New System.Drawing.Size(56, 16)
			Me.label18.TabIndex = 39
			Me.label18.Text = "TRext:"
			Me.label18.Visible = False
			' 
			' label17
			' 
			Me.label17.AutoSize = True
			Me.label17.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label17.Location = New System.Drawing.Point(285, 159)
			Me.label17.Name = "label17"
			Me.label17.Size = New System.Drawing.Size(32, 16)
			Me.label17.TabIndex = 38
			Me.label17.Text = "DR:"
			Me.label17.Visible = False
			' 
			' label16
			' 
			Me.label16.AutoSize = True
			Me.label16.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label16.Location = New System.Drawing.Point(19, 180)
			Me.label16.Name = "label16"
			Me.label16.Size = New System.Drawing.Size(64, 16)
			Me.label16.TabIndex = 37
			Me.label16.Text = "Miller:"
			Me.label16.Visible = False
			' 
			' label15
			' 
			Me.label15.AutoSize = True
			Me.label15.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label15.Location = New System.Drawing.Point(271, 221)
			Me.label15.Name = "label15"
			Me.label15.Size = New System.Drawing.Size(48, 16)
			Me.label15.TabIndex = 36
			Me.label15.Text = "minQ:"
			Me.label15.Visible = False
			' 
			' label14
			' 
			Me.label14.AutoSize = True
			Me.label14.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label14.Location = New System.Drawing.Point(271, 306)
			Me.label14.Name = "label14"
			Me.label14.Size = New System.Drawing.Size(48, 16)
			Me.label14.TabIndex = 35
			Me.label14.Text = "maxQ:"
			Me.label14.Visible = False
			' 
			' cmbQ
			' 
			Me.cmbQ.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbQ.FormattingEnabled = True
			Me.cmbQ.Items.AddRange(New Object() { "0(Fixed)", "1(Dynamic)"})
			Me.cmbQ.Location = New System.Drawing.Point(89, 143)
			Me.cmbQ.Name = "cmbQ"
			Me.cmbQ.Size = New System.Drawing.Size(114, 24)
			Me.cmbQ.TabIndex = 34
			Me.cmbQ.Visible = False
			' 
			' label13
			' 
			Me.label13.AutoSize = True
			Me.label13.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label13.Location = New System.Drawing.Point(58, 146)
			Me.label13.Name = "label13"
			Me.label13.Size = New System.Drawing.Size(24, 16)
			Me.label13.TabIndex = 33
			Me.label13.Text = "Q:"
			Me.label13.Visible = False
			' 
			' cmbT
			' 
			Me.cmbT.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbT.FormattingEnabled = True
			Me.cmbT.Items.AddRange(New Object() { "0(Disable)", "1(Enable)"})
			Me.cmbT.Location = New System.Drawing.Point(90, 327)
			Me.cmbT.Name = "cmbT"
			Me.cmbT.Size = New System.Drawing.Size(114, 24)
			Me.cmbT.TabIndex = 32
			Me.cmbT.Visible = False
			' 
			' label12
			' 
			Me.label12.AutoSize = True
			Me.label12.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label12.Location = New System.Drawing.Point(11, 329)
			Me.label12.Name = "label12"
			Me.label12.Size = New System.Drawing.Size(80, 16)
			Me.label12.TabIndex = 31
			Me.label12.Text = "Truncate:"
			Me.label12.Visible = False
			' 
			' cmbAction
			' 
			Me.cmbAction.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbAction.FormattingEnabled = True
			Me.cmbAction.Items.AddRange(New Object() { "000", "001", "010", "011", "100", "101", "110", "111"})
			Me.cmbAction.Location = New System.Drawing.Point(325, 246)
			Me.cmbAction.Name = "cmbAction"
			Me.cmbAction.Size = New System.Drawing.Size(114, 24)
			Me.cmbAction.TabIndex = 30
			Me.cmbAction.Visible = False
			' 
			' label11
			' 
			Me.label11.AutoSize = True
			Me.label11.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label11.Location = New System.Drawing.Point(255, 252)
			Me.label11.Name = "label11"
			Me.label11.Size = New System.Drawing.Size(64, 16)
			Me.label11.TabIndex = 29
			Me.label11.Text = "Action:"
			Me.label11.Visible = False
			' 
			' cmbTarget
			' 
			Me.cmbTarget.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbTarget.FormattingEnabled = True
			Me.cmbTarget.Items.AddRange(New Object() { "000(s0) ", "001(s1)  ", "010(s2) ", "011(s3) ", "100(SL)"})
			Me.cmbTarget.Location = New System.Drawing.Point(89, 296)
			Me.cmbTarget.Name = "cmbTarget"
			Me.cmbTarget.Size = New System.Drawing.Size(114, 24)
			Me.cmbTarget.TabIndex = 28
			Me.cmbTarget.Visible = False
			' 
			' label10
			' 
			Me.label10.AutoSize = True
			Me.label10.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label10.Location = New System.Drawing.Point(19, 299)
			Me.label10.Name = "label10"
			Me.label10.Size = New System.Drawing.Size(64, 16)
			Me.label10.TabIndex = 17
			Me.label10.Text = "Target:"
			Me.label10.Visible = False
			' 
			' groupBox6
			' 
			Me.groupBox6.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.groupBox6.Controls.Add(Me.cbPower)
			Me.groupBox6.Controls.Add(Me.cmbPower_ANT1)
			Me.groupBox6.Controls.Add(Me.label24)
			Me.groupBox6.Controls.Add(Me.btnPowerGet_ANT1)
			Me.groupBox6.Controls.Add(Me.label23)
			Me.groupBox6.Controls.Add(Me.btnPowerSet_ANT1)
			Me.groupBox6.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox6.ForeColor = System.Drawing.Color.Black
			Me.groupBox6.Location = New System.Drawing.Point(9, 9)
			Me.groupBox6.Name = "groupBox6"
			Me.groupBox6.Size = New System.Drawing.Size(406, 113)
			Me.groupBox6.TabIndex = 31
			Me.groupBox6.TabStop = False
			Me.groupBox6.Text = "Power"
			' 
			' cbPower
			' 
			Me.cbPower.AutoSize = True
			Me.cbPower.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbPower.Location = New System.Drawing.Point(308, 76)
			Me.cbPower.Name = "cbPower"
			Me.cbPower.Size = New System.Drawing.Size(59, 20)
			Me.cbPower.TabIndex = 26
			Me.cbPower.Text = "Save"
			Me.cbPower.UseVisualStyleBackColor = True
			' 
			' cmbPower_ANT1
			' 
			Me.cmbPower_ANT1.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbPower_ANT1.FormattingEnabled = True
			Me.cmbPower_ANT1.Items.AddRange(New Object() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30"})
			Me.cmbPower_ANT1.Location = New System.Drawing.Point(117, 33)
			Me.cmbPower_ANT1.Name = "cmbPower_ANT1"
			Me.cmbPower_ANT1.Size = New System.Drawing.Size(216, 24)
			Me.cmbPower_ANT1.TabIndex = 6
			' 
			' label24
			' 
			Me.label24.AutoSize = True
			Me.label24.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label24.Location = New System.Drawing.Point(36, 36)
			Me.label24.Name = "label24"
			Me.label24.Size = New System.Drawing.Size(80, 16)
			Me.label24.TabIndex = 14
			Me.label24.Text = "输出功率:"
			' 
			' btnPowerGet_ANT1
			' 
			Me.btnPowerGet_ANT1.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnPowerGet_ANT1.ForeColor = System.Drawing.Color.Black
			Me.btnPowerGet_ANT1.Location = New System.Drawing.Point(95, 67)
			Me.btnPowerGet_ANT1.Name = "btnPowerGet_ANT1"
			Me.btnPowerGet_ANT1.Size = New System.Drawing.Size(90, 31)
			Me.btnPowerGet_ANT1.TabIndex = 13
			Me.btnPowerGet_ANT1.Text = "Get"
			Me.btnPowerGet_ANT1.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnPowerGet_ANT1.Click += new System.EventHandler(this.btnPowerGet_Click);
			' 
			' label23
			' 
			Me.label23.AutoSize = True
			Me.label23.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label23.Location = New System.Drawing.Point(332, 38)
			Me.label23.Name = "label23"
			Me.label23.Size = New System.Drawing.Size(32, 16)
			Me.label23.TabIndex = 12
			Me.label23.Text = "dBm"
			' 
			' btnPowerSet_ANT1
			' 
			Me.btnPowerSet_ANT1.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnPowerSet_ANT1.ForeColor = System.Drawing.Color.Black
			Me.btnPowerSet_ANT1.Location = New System.Drawing.Point(212, 67)
			Me.btnPowerSet_ANT1.Name = "btnPowerSet_ANT1"
			Me.btnPowerSet_ANT1.Size = New System.Drawing.Size(88, 31)
			Me.btnPowerSet_ANT1.TabIndex = 11
			Me.btnPowerSet_ANT1.Text = "Set"
			Me.btnPowerSet_ANT1.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnPowerSet_ANT1.Click += new System.EventHandler(this.btnPowerSet_Click);
			' 
			' groupBox7
			' 
			Me.groupBox7.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.groupBox7.Controls.Add(Me.comboBox1)
			Me.groupBox7.Controls.Add(Me.textBox1)
			Me.groupBox7.Controls.Add(Me.label6)
			Me.groupBox7.Controls.Add(Me.btnWorkModeGet)
			Me.groupBox7.Controls.Add(Me.btnWorkModeSet)
			Me.groupBox7.Controls.Add(Me.label28)
			Me.groupBox7.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox7.ForeColor = System.Drawing.Color.Black
			Me.groupBox7.Location = New System.Drawing.Point(426, 655)
			Me.groupBox7.Name = "groupBox7"
			Me.groupBox7.Size = New System.Drawing.Size(466, 112)
			Me.groupBox7.TabIndex = 30
			Me.groupBox7.TabStop = False
			Me.groupBox7.Text = "Rrequency"
			' 
			' comboBox1
			' 
			Me.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.comboBox1.FormattingEnabled = True
			Me.comboBox1.Items.AddRange(New Object() { "902.750", "903.250", "903.750", "904.250", "904.750", "905.250", "905.750", "906.250", "906.750", "907.250", "907.750", "908.250", "908.750", "909.250", "909.750", "910.250", "910.750", "911.250", "911.750", "912.250", "912.750", "913.250", "913.750", "914.250", "914.750", "915.250", "915.750", "916.250", "916.8", "916.750", "917.250", "917.750", "918", "918.250", "918.750", "919.2", "919.250", "919.750", "920.250", "920.4", "920.6", "920.8", "920.750", "921.250", "921.750", "922.250", "922.750", "923.250", "923.750", "924.250", "924.750", "925.250", "925.750", "926.250", "926.750", "927.250", "", "865.700", "866.300", "866.900", "867.500", "", "920.625", "920.875", "921.125", "921.375", "921.625", "921.875", "922.125", "922.375", "922.625", "922.875", "923.125", "923.375", "923.625", "923.875", "924.125", "924.375"})
			Me.comboBox1.Location = New System.Drawing.Point(113, 29)
			Me.comboBox1.Name = "comboBox1"
			Me.comboBox1.Size = New System.Drawing.Size(220, 22)
			Me.comboBox1.TabIndex = 23
			' 
			' textBox1
			' 
			Me.textBox1.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.textBox1.Location = New System.Drawing.Point(115, 31)
			Me.textBox1.Name = "textBox1"
			Me.textBox1.Size = New System.Drawing.Size(218, 21)
			Me.textBox1.TabIndex = 22
			Me.textBox1.Text = "922.375"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			' 
			' label6
			' 
			Me.label6.AutoSize = True
			Me.label6.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label6.Location = New System.Drawing.Point(339, 38)
			Me.label6.Name = "label6"
			Me.label6.Size = New System.Drawing.Size(23, 12)
			Me.label6.TabIndex = 21
			Me.label6.Text = "MHz"
			' 
			' btnWorkModeGet
			' 
			Me.btnWorkModeGet.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnWorkModeGet.ForeColor = System.Drawing.Color.Black
			Me.btnWorkModeGet.Location = New System.Drawing.Point(30, 67)
			Me.btnWorkModeGet.Name = "btnWorkModeGet"
			Me.btnWorkModeGet.Size = New System.Drawing.Size(90, 31)
			Me.btnWorkModeGet.TabIndex = 20
			Me.btnWorkModeGet.Text = "Get"
			Me.btnWorkModeGet.UseVisualStyleBackColor = True
			Me.btnWorkModeGet.Visible = False
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnWorkModeGet.Click += new System.EventHandler(this.btnWorkModeGet_Click);
			' 
			' btnWorkModeSet
			' 
			Me.btnWorkModeSet.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnWorkModeSet.ForeColor = System.Drawing.Color.Black
			Me.btnWorkModeSet.Location = New System.Drawing.Point(171, 67)
			Me.btnWorkModeSet.Name = "btnWorkModeSet"
			Me.btnWorkModeSet.Size = New System.Drawing.Size(90, 31)
			Me.btnWorkModeSet.TabIndex = 11
			Me.btnWorkModeSet.Text = "Set"
			Me.btnWorkModeSet.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnWorkModeSet.Click += new System.EventHandler(this.btnWorkModeSet_Click);
			' 
			' label28
			' 
			Me.label28.AutoSize = True
			Me.label28.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label28.Location = New System.Drawing.Point(27, 34)
			Me.label28.Name = "label28"
			Me.label28.Size = New System.Drawing.Size(88, 16)
			Me.label28.TabIndex = 5
			Me.label28.Text = "Frequency:"
			' 
			' label27
			' 
			Me.label27.AutoSize = True
			Me.label27.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label27.ForeColor = System.Drawing.Color.Blue
			Me.label27.Location = New System.Drawing.Point(73, 43)
			Me.label27.Name = "label27"
			Me.label27.Size = New System.Drawing.Size(72, 16)
			Me.label27.TabIndex = 49
			Me.label27.Text = "暂时隐藏"
			' 
			' panel1
			' 
			Me.panel1.AutoScroll = True
			Me.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.panel1.Controls.Add(Me.groupBox4)
			Me.panel1.Controls.Add(Me.btnReset)
			Me.panel1.Controls.Add(Me.groupBox25)
			Me.panel1.Controls.Add(Me.groupBox6)
			Me.panel1.Controls.Add(Me.gbInventoryMode)
			Me.panel1.Controls.Add(Me.groupBox8)
			Me.panel1.Controls.Add(Me.groupBox24)
			Me.panel1.Controls.Add(Me.bgGPIO)
			Me.panel1.Controls.Add(Me.groupBox19)
			Me.panel1.Controls.Add(Me.gbWorkMode)
			Me.panel1.Controls.Add(Me.gbIp2)
			Me.panel1.Controls.Add(Me.groupBox9)
			Me.panel1.Controls.Add(Me.gbIP)
			Me.panel1.Controls.Add(Me.label8)
			Me.panel1.Controls.Add(Me.groupBox16)
			Me.panel1.Controls.Add(Me.groupBox15)
			Me.panel1.Controls.Add(Me.groupBox14)
			Me.panel1.Controls.Add(Me.groupBox11)
			Me.panel1.Controls.Add(Me.gbAnt)
			Me.panel1.Controls.Add(Me.groupBox1)
			Me.panel1.Controls.Add(Me.groupBox7)
			Me.panel1.Controls.Add(Me.groupBox3)
			Me.panel1.Controls.Add(Me.groupBox5)
			Me.panel1.Location = New System.Drawing.Point(0, 0)
			Me.panel1.Name = "panel1"
			Me.panel1.Size = New System.Drawing.Size(1364, 910)
			Me.panel1.TabIndex = 32
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
			' 
			' groupBox4
			' 
			Me.groupBox4.Controls.Add(Me.groupBox10)
			Me.groupBox4.Controls.Add(Me.groupBox17)
			Me.groupBox4.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox4.Location = New System.Drawing.Point(904, 726)
			Me.groupBox4.Name = "groupBox4"
			Me.groupBox4.Size = New System.Drawing.Size(355, 108)
			Me.groupBox4.TabIndex = 74
			Me.groupBox4.TabStop = False
			Me.groupBox4.Text = "GPIO"
			' 
			' groupBox10
			' 
			Me.groupBox10.Controls.Add(Me.cmbInput1)
			Me.groupBox10.Controls.Add(Me.button12)
			Me.groupBox10.Controls.Add(Me.cmbInput2)
			Me.groupBox10.Controls.Add(Me.label55)
			Me.groupBox10.Controls.Add(Me.label56)
			Me.groupBox10.Location = New System.Drawing.Point(8, 63)
			Me.groupBox10.Name = "groupBox10"
			Me.groupBox10.Size = New System.Drawing.Size(341, 39)
			Me.groupBox10.TabIndex = 68
			Me.groupBox10.TabStop = False
			' 
			' cmbInput1
			' 
			Me.cmbInput1.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbInput1.FormattingEnabled = True
			Me.cmbInput1.Items.AddRange(New Object() { "低电平", "高电平"})
			Me.cmbInput1.Location = New System.Drawing.Point(62, 12)
			Me.cmbInput1.Name = "cmbInput1"
			Me.cmbInput1.Size = New System.Drawing.Size(73, 24)
			Me.cmbInput1.TabIndex = 19
			' 
			' button12
			' 
			Me.button12.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button12.ForeColor = System.Drawing.Color.Black
			Me.button12.Location = New System.Drawing.Point(271, 10)
			Me.button12.Name = "button12"
			Me.button12.Size = New System.Drawing.Size(67, 26)
			Me.button12.TabIndex = 26
			Me.button12.Text = "Get"
			Me.button12.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button12.Click += new System.EventHandler(this.button12_Click);
			' 
			' cmbInput2
			' 
			Me.cmbInput2.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbInput2.FormattingEnabled = True
			Me.cmbInput2.Items.AddRange(New Object() { "低电平", "高电平"})
			Me.cmbInput2.Location = New System.Drawing.Point(197, 12)
			Me.cmbInput2.Name = "cmbInput2"
			Me.cmbInput2.Size = New System.Drawing.Size(73, 24)
			Me.cmbInput2.TabIndex = 20
			' 
			' label55
			' 
			Me.label55.AutoSize = True
			Me.label55.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label55.Location = New System.Drawing.Point(3, 14)
			Me.label55.Name = "label55"
			Me.label55.Size = New System.Drawing.Size(64, 16)
			Me.label55.TabIndex = 8
			Me.label55.Text = "input1:"
			' 
			' label56
			' 
			Me.label56.AutoSize = True
			Me.label56.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label56.Location = New System.Drawing.Point(137, 15)
			Me.label56.Name = "label56"
			Me.label56.Size = New System.Drawing.Size(64, 16)
			Me.label56.TabIndex = 9
			Me.label56.Text = "input2:"
			' 
			' groupBox17
			' 
			Me.groupBox17.Controls.Add(Me.cmbOutput2)
			Me.groupBox17.Controls.Add(Me.label58)
			Me.groupBox17.Controls.Add(Me.button13)
			Me.groupBox17.Controls.Add(Me.cmbOutput1)
			Me.groupBox17.Controls.Add(Me.label57)
			Me.groupBox17.Location = New System.Drawing.Point(7, 13)
			Me.groupBox17.Name = "groupBox17"
			Me.groupBox17.Size = New System.Drawing.Size(336, 50)
			Me.groupBox17.TabIndex = 67
			Me.groupBox17.TabStop = False
			' 
			' cmbOutput2
			' 
			Me.cmbOutput2.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbOutput2.FormattingEnabled = True
			Me.cmbOutput2.Items.AddRange(New Object() { "低电平", "高电平"})
			Me.cmbOutput2.Location = New System.Drawing.Point(205, 16)
			Me.cmbOutput2.Name = "cmbOutput2"
			Me.cmbOutput2.Size = New System.Drawing.Size(73, 24)
			Me.cmbOutput2.TabIndex = 68
			' 
			' label58
			' 
			Me.label58.AutoSize = True
			Me.label58.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label58.Location = New System.Drawing.Point(147, 19)
			Me.label58.Name = "label58"
			Me.label58.Size = New System.Drawing.Size(64, 16)
			Me.label58.TabIndex = 67
			Me.label58.Text = "ouput2:"
			' 
			' button13
			' 
			Me.button13.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button13.ForeColor = System.Drawing.Color.Black
			Me.button13.Location = New System.Drawing.Point(284, 12)
			Me.button13.Name = "button13"
			Me.button13.Size = New System.Drawing.Size(46, 31)
			Me.button13.TabIndex = 66
			Me.button13.Text = "Set"
			Me.button13.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button13.Click += new System.EventHandler(this.button13_Click);
			' 
			' cmbOutput1
			' 
			Me.cmbOutput1.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbOutput1.FormattingEnabled = True
			Me.cmbOutput1.Items.AddRange(New Object() { "低电平", "高电平"})
			Me.cmbOutput1.Location = New System.Drawing.Point(68, 16)
			Me.cmbOutput1.Name = "cmbOutput1"
			Me.cmbOutput1.Size = New System.Drawing.Size(73, 24)
			Me.cmbOutput1.TabIndex = 22
			' 
			' label57
			' 
			Me.label57.AutoSize = True
			Me.label57.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label57.Location = New System.Drawing.Point(6, 19)
			Me.label57.Name = "label57"
			Me.label57.Size = New System.Drawing.Size(64, 16)
			Me.label57.TabIndex = 21
			Me.label57.Text = "ouput1:"
			' 
			' btnReset
			' 
			Me.btnReset.Font = New System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnReset.ForeColor = System.Drawing.Color.Black
			Me.btnReset.Location = New System.Drawing.Point(902, 554)
			Me.btnReset.Name = "btnReset"
			Me.btnReset.Size = New System.Drawing.Size(356, 49)
			Me.btnReset.TabIndex = 27
			Me.btnReset.Text = "Reset"
			Me.btnReset.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			' 
			' groupBox25
			' 
			Me.groupBox25.Controls.Add(Me.btnCalibration)
			Me.groupBox25.Controls.Add(Me.txtCalibration)
			Me.groupBox25.Location = New System.Drawing.Point(903, 834)
			Me.groupBox25.Name = "groupBox25"
			Me.groupBox25.Size = New System.Drawing.Size(355, 65)
			Me.groupBox25.TabIndex = 73
			Me.groupBox25.TabStop = False
			Me.groupBox25.Text = "校准"
			Me.groupBox25.Visible = False
			' 
			' btnCalibration
			' 
			Me.btnCalibration.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnCalibration.ForeColor = System.Drawing.Color.Black
			Me.btnCalibration.Location = New System.Drawing.Point(188, 20)
			Me.btnCalibration.Name = "btnCalibration"
			Me.btnCalibration.Size = New System.Drawing.Size(90, 29)
			Me.btnCalibration.TabIndex = 70
			Me.btnCalibration.Text = "校准"
			Me.btnCalibration.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnCalibration.Click += new System.EventHandler(this.btnCalibration_Click);
			' 
			' txtCalibration
			' 
			Me.txtCalibration.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtCalibration.Location = New System.Drawing.Point(21, 24)
			Me.txtCalibration.Name = "txtCalibration"
			Me.txtCalibration.Size = New System.Drawing.Size(119, 26)
			Me.txtCalibration.TabIndex = 69
			' 
			' gbInventoryMode
			' 
			Me.gbInventoryMode.Controls.Add(Me.checkBox2)
			Me.gbInventoryMode.Controls.Add(Me.txtUserLen)
			Me.gbInventoryMode.Controls.Add(Me.label47)
			Me.gbInventoryMode.Controls.Add(Me.txtUserPtr)
			Me.gbInventoryMode.Controls.Add(Me.label46)
			Me.gbInventoryMode.Controls.Add(Me.cbInventoryMode)
			Me.gbInventoryMode.Controls.Add(Me.label45)
			Me.gbInventoryMode.Controls.Add(Me.button10)
			Me.gbInventoryMode.Controls.Add(Me.button11)
			Me.gbInventoryMode.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.gbInventoryMode.Location = New System.Drawing.Point(903, 371)
			Me.gbInventoryMode.Name = "gbInventoryMode"
			Me.gbInventoryMode.Size = New System.Drawing.Size(351, 173)
			Me.gbInventoryMode.TabIndex = 72
			Me.gbInventoryMode.TabStop = False
			' 
			' checkBox2
			' 
			Me.checkBox2.AutoSize = True
			Me.checkBox2.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.checkBox2.Location = New System.Drawing.Point(256, 136)
			Me.checkBox2.Name = "checkBox2"
			Me.checkBox2.Size = New System.Drawing.Size(54, 18)
			Me.checkBox2.TabIndex = 71
			Me.checkBox2.Text = "Save"
			Me.checkBox2.UseVisualStyleBackColor = True
			' 
			' txtUserLen
			' 
			Me.txtUserLen.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtUserLen.Location = New System.Drawing.Point(113, 92)
			Me.txtUserLen.Name = "txtUserLen"
			Me.txtUserLen.Size = New System.Drawing.Size(197, 26)
			Me.txtUserLen.TabIndex = 70
			Me.txtUserLen.Text = "6"
			' 
			' label47
			' 
			Me.label47.AutoSize = True
			Me.label47.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label47.Location = New System.Drawing.Point(6, 88)
			Me.label47.Name = "label47"
			Me.label47.Size = New System.Drawing.Size(80, 16)
			Me.label47.TabIndex = 69
			Me.label47.Text = "User Len:"
			' 
			' txtUserPtr
			' 
			Me.txtUserPtr.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtUserPtr.Location = New System.Drawing.Point(113, 59)
			Me.txtUserPtr.Name = "txtUserPtr"
			Me.txtUserPtr.Size = New System.Drawing.Size(197, 26)
			Me.txtUserPtr.TabIndex = 68
			Me.txtUserPtr.Text = "0"
			' 
			' label46
			' 
			Me.label46.AutoSize = True
			Me.label46.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label46.Location = New System.Drawing.Point(6, 61)
			Me.label46.Name = "label46"
			Me.label46.Size = New System.Drawing.Size(80, 16)
			Me.label46.TabIndex = 67
			Me.label46.Text = "User Ptr:"
			' 
			' cbInventoryMode
			' 
			Me.cbInventoryMode.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbInventoryMode.FormattingEnabled = True
			Me.cbInventoryMode.Items.AddRange(New Object() { "EPC", "EPC+TID", "EPC+TID+USER"})
			Me.cbInventoryMode.Location = New System.Drawing.Point(113, 29)
			Me.cbInventoryMode.Name = "cbInventoryMode"
			Me.cbInventoryMode.Size = New System.Drawing.Size(197, 24)
			Me.cbInventoryMode.TabIndex = 66
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cbInventoryMode.SelectedIndexChanged += new System.EventHandler(this.cbInventoryMode_SelectedIndexChanged);
			' 
			' label45
			' 
			Me.label45.AutoSize = True
			Me.label45.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label45.Location = New System.Drawing.Point(7, 29)
			Me.label45.Name = "label45"
			Me.label45.Size = New System.Drawing.Size(48, 16)
			Me.label45.TabIndex = 66
			Me.label45.Text = "Mode:"
			' 
			' button10
			' 
			Me.button10.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button10.ForeColor = System.Drawing.Color.Black
			Me.button10.Location = New System.Drawing.Point(146, 128)
			Me.button10.Name = "button10"
			Me.button10.Size = New System.Drawing.Size(90, 31)
			Me.button10.TabIndex = 30
			Me.button10.Text = "Set"
			Me.button10.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button10.Click += new System.EventHandler(this.button10_Click);
			' 
			' button11
			' 
			Me.button11.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button11.ForeColor = System.Drawing.Color.Black
			Me.button11.Location = New System.Drawing.Point(31, 128)
			Me.button11.Name = "button11"
			Me.button11.Size = New System.Drawing.Size(90, 31)
			Me.button11.TabIndex = 29
			Me.button11.Text = "Get"
			Me.button11.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button11.Click += new System.EventHandler(this.button11_Click);
			' 
			' groupBox8
			' 
			Me.groupBox8.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.groupBox8.Controls.Add(Me.label36)
			Me.groupBox8.Controls.Add(Me.label33)
			Me.groupBox8.Controls.Add(Me.textBox3)
			Me.groupBox8.Controls.Add(Me.button4)
			Me.groupBox8.Controls.Add(Me.label32)
			Me.groupBox8.Controls.Add(Me.button3)
			Me.groupBox8.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox8.ForeColor = System.Drawing.Color.Black
			Me.groupBox8.Location = New System.Drawing.Point(1276, 557)
			Me.groupBox8.Name = "groupBox8"
			Me.groupBox8.Size = New System.Drawing.Size(65, 77)
			Me.groupBox8.TabIndex = 41
			Me.groupBox8.TabStop = False
			Me.groupBox8.Text = "TemperatureProtect"
			Me.groupBox8.Visible = False
			' 
			' label36
			' 
			Me.label36.AutoSize = True
			Me.label36.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label36.ForeColor = System.Drawing.Color.Blue
			Me.label36.Location = New System.Drawing.Point(141, 39)
			Me.label36.Name = "label36"
			Me.label36.Size = New System.Drawing.Size(72, 16)
			Me.label36.TabIndex = 49
			Me.label36.Text = "暂时隐藏"
			' 
			' label33
			' 
			Me.label33.AutoSize = True
			Me.label33.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label33.Location = New System.Drawing.Point(258, 26)
			Me.label33.Name = "label33"
			Me.label33.Size = New System.Drawing.Size(35, 12)
			Me.label33.TabIndex = 46
			Me.label33.Text = "50-75"
			' 
			' textBox3
			' 
			Me.textBox3.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.textBox3.Location = New System.Drawing.Point(78, 19)
			Me.textBox3.MaxLength = 6
			Me.textBox3.Name = "textBox3"
			Me.textBox3.Size = New System.Drawing.Size(165, 26)
			Me.textBox3.TabIndex = 29
			' 
			' button4
			' 
			Me.button4.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button4.ForeColor = System.Drawing.Color.Black
			Me.button4.Location = New System.Drawing.Point(42, 52)
			Me.button4.Name = "button4"
			Me.button4.Size = New System.Drawing.Size(90, 31)
			Me.button4.TabIndex = 45
			Me.button4.Text = "Get"
			Me.button4.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button4.Click += new System.EventHandler(this.button4_Click);
			' 
			' label32
			' 
			Me.label32.AutoSize = True
			Me.label32.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label32.Location = New System.Drawing.Point(24, 22)
			Me.label32.Name = "label32"
			Me.label32.Size = New System.Drawing.Size(48, 16)
			Me.label32.TabIndex = 6
			Me.label32.Text = "value"
			' 
			' button3
			' 
			Me.button3.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button3.ForeColor = System.Drawing.Color.Black
			Me.button3.Location = New System.Drawing.Point(195, 52)
			Me.button3.Name = "button3"
			Me.button3.Size = New System.Drawing.Size(90, 31)
			Me.button3.TabIndex = 26
			Me.button3.Text = "Set"
			Me.button3.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button3.Click += new System.EventHandler(this.button3_Click);
			' 
			' groupBox24
			' 
			Me.groupBox24.Controls.Add(Me.checkBox1)
			Me.groupBox24.Controls.Add(Me.button8)
			Me.groupBox24.Controls.Add(Me.button9)
			Me.groupBox24.Controls.Add(Me.label43)
			Me.groupBox24.Controls.Add(Me.label44)
			Me.groupBox24.Controls.Add(Me.txtWaitTime)
			Me.groupBox24.Controls.Add(Me.txtworkTime)
			Me.groupBox24.Controls.Add(Me.label42)
			Me.groupBox24.Controls.Add(Me.label41)
			Me.groupBox24.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox24.Location = New System.Drawing.Point(1263, 412)
			Me.groupBox24.Name = "groupBox24"
			Me.groupBox24.Size = New System.Drawing.Size(72, 116)
			Me.groupBox24.TabIndex = 70
			Me.groupBox24.TabStop = False
			Me.groupBox24.Text = "占空比"
			Me.groupBox24.Visible = False
			' 
			' checkBox1
			' 
			Me.checkBox1.AutoSize = True
			Me.checkBox1.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.checkBox1.Location = New System.Drawing.Point(273, 108)
			Me.checkBox1.Name = "checkBox1"
			Me.checkBox1.Size = New System.Drawing.Size(59, 20)
			Me.checkBox1.TabIndex = 31
			Me.checkBox1.Text = "Save"
			Me.checkBox1.UseVisualStyleBackColor = True
			' 
			' button8
			' 
			Me.button8.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button8.ForeColor = System.Drawing.Color.Black
			Me.button8.Location = New System.Drawing.Point(168, 103)
			Me.button8.Name = "button8"
			Me.button8.Size = New System.Drawing.Size(90, 31)
			Me.button8.TabIndex = 30
			Me.button8.Text = "Set"
			Me.button8.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button8.Click += new System.EventHandler(this.button8_Click);
			' 
			' button9
			' 
			Me.button9.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button9.ForeColor = System.Drawing.Color.Black
			Me.button9.Location = New System.Drawing.Point(49, 103)
			Me.button9.Name = "button9"
			Me.button9.Size = New System.Drawing.Size(90, 31)
			Me.button9.TabIndex = 29
			Me.button9.Text = "Get"
			Me.button9.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button9.Click += new System.EventHandler(this.button9_Click);
			' 
			' label43
			' 
			Me.label43.AutoSize = True
			Me.label43.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label43.Location = New System.Drawing.Point(193, 25)
			Me.label43.Name = "label43"
			Me.label43.Size = New System.Drawing.Size(24, 16)
			Me.label43.TabIndex = 28
			Me.label43.Text = "ms"
			' 
			' label44
			' 
			Me.label44.AutoSize = True
			Me.label44.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label44.Location = New System.Drawing.Point(193, 57)
			Me.label44.Name = "label44"
			Me.label44.Size = New System.Drawing.Size(24, 16)
			Me.label44.TabIndex = 27
			Me.label44.Text = "ms"
			' 
			' txtWaitTime
			' 
			Me.txtWaitTime.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtWaitTime.Location = New System.Drawing.Point(84, 58)
			Me.txtWaitTime.Name = "txtWaitTime"
			Me.txtWaitTime.Size = New System.Drawing.Size(108, 26)
			Me.txtWaitTime.TabIndex = 26
			' 
			' txtworkTime
			' 
			Me.txtworkTime.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtworkTime.Location = New System.Drawing.Point(84, 22)
			Me.txtworkTime.Name = "txtworkTime"
			Me.txtworkTime.Size = New System.Drawing.Size(108, 26)
			Me.txtworkTime.TabIndex = 25
			' 
			' label42
			' 
			Me.label42.AutoSize = True
			Me.label42.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label42.Location = New System.Drawing.Point(6, 29)
			Me.label42.Name = "label42"
			Me.label42.Size = New System.Drawing.Size(72, 16)
			Me.label42.TabIndex = 8
			Me.label42.Text = "工作时间"
			' 
			' label41
			' 
			Me.label41.AutoSize = True
			Me.label41.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label41.Location = New System.Drawing.Point(6, 61)
			Me.label41.Name = "label41"
			Me.label41.Size = New System.Drawing.Size(72, 16)
			Me.label41.TabIndex = 7
			Me.label41.Text = "等待时间"
			' 
			' bgGPIO
			' 
			Me.bgGPIO.Controls.Add(Me.groupBox23)
			Me.bgGPIO.Controls.Add(Me.groupBox22)
			Me.bgGPIO.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.bgGPIO.Location = New System.Drawing.Point(903, 609)
			Me.bgGPIO.Name = "bgGPIO"
			Me.bgGPIO.Size = New System.Drawing.Size(355, 108)
			Me.bgGPIO.TabIndex = 69
			Me.bgGPIO.TabStop = False
			Me.bgGPIO.Text = "GPIO"
			' 
			' groupBox23
			' 
			Me.groupBox23.Controls.Add(Me.comboBox2)
			Me.groupBox23.Controls.Add(Me.button6)
			Me.groupBox23.Controls.Add(Me.comboBox3)
			Me.groupBox23.Controls.Add(Me.label39)
			Me.groupBox23.Controls.Add(Me.label40)
			Me.groupBox23.Location = New System.Drawing.Point(8, 63)
			Me.groupBox23.Name = "groupBox23"
			Me.groupBox23.Size = New System.Drawing.Size(341, 39)
			Me.groupBox23.TabIndex = 68
			Me.groupBox23.TabStop = False
			' 
			' comboBox2
			' 
			Me.comboBox2.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.comboBox2.FormattingEnabled = True
			Me.comboBox2.Items.AddRange(New Object() { "低电平", "高电平"})
			Me.comboBox2.Location = New System.Drawing.Point(62, 12)
			Me.comboBox2.Name = "comboBox2"
			Me.comboBox2.Size = New System.Drawing.Size(73, 24)
			Me.comboBox2.TabIndex = 19
			' 
			' button6
			' 
			Me.button6.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button6.ForeColor = System.Drawing.Color.Black
			Me.button6.Location = New System.Drawing.Point(271, 10)
			Me.button6.Name = "button6"
			Me.button6.Size = New System.Drawing.Size(67, 26)
			Me.button6.TabIndex = 26
			Me.button6.Text = "Get"
			Me.button6.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button6.Click += new System.EventHandler(this.button6_Click);
			' 
			' comboBox3
			' 
			Me.comboBox3.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.comboBox3.FormattingEnabled = True
			Me.comboBox3.Items.AddRange(New Object() { "低电平", "高电平"})
			Me.comboBox3.Location = New System.Drawing.Point(197, 12)
			Me.comboBox3.Name = "comboBox3"
			Me.comboBox3.Size = New System.Drawing.Size(73, 24)
			Me.comboBox3.TabIndex = 20
			' 
			' label39
			' 
			Me.label39.AutoSize = True
			Me.label39.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label39.Location = New System.Drawing.Point(3, 14)
			Me.label39.Name = "label39"
			Me.label39.Size = New System.Drawing.Size(64, 16)
			Me.label39.TabIndex = 8
			Me.label39.Text = "input1:"
			' 
			' label40
			' 
			Me.label40.AutoSize = True
			Me.label40.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label40.Location = New System.Drawing.Point(137, 15)
			Me.label40.Name = "label40"
			Me.label40.Size = New System.Drawing.Size(64, 16)
			Me.label40.TabIndex = 9
			Me.label40.Text = "input2:"
			' 
			' groupBox22
			' 
			Me.groupBox22.Controls.Add(Me.button7)
			Me.groupBox22.Controls.Add(Me.cmbOutStatus)
			Me.groupBox22.Controls.Add(Me.label38)
			Me.groupBox22.Location = New System.Drawing.Point(7, 13)
			Me.groupBox22.Name = "groupBox22"
			Me.groupBox22.Size = New System.Drawing.Size(312, 50)
			Me.groupBox22.TabIndex = 67
			Me.groupBox22.TabStop = False
			' 
			' button7
			' 
			Me.button7.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button7.ForeColor = System.Drawing.Color.Black
			Me.button7.Location = New System.Drawing.Point(161, 12)
			Me.button7.Name = "button7"
			Me.button7.Size = New System.Drawing.Size(90, 31)
			Me.button7.TabIndex = 66
			Me.button7.Text = "Set"
			Me.button7.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button7.Click += new System.EventHandler(this.button7_Click);
			' 
			' cmbOutStatus
			' 
			Me.cmbOutStatus.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbOutStatus.FormattingEnabled = True
			Me.cmbOutStatus.Items.AddRange(New Object() { "断开", "闭合"})
			Me.cmbOutStatus.Location = New System.Drawing.Point(68, 16)
			Me.cmbOutStatus.Name = "cmbOutStatus"
			Me.cmbOutStatus.Size = New System.Drawing.Size(73, 24)
			Me.cmbOutStatus.TabIndex = 22
			' 
			' label38
			' 
			Me.label38.AutoSize = True
			Me.label38.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label38.Location = New System.Drawing.Point(6, 19)
			Me.label38.Name = "label38"
			Me.label38.Size = New System.Drawing.Size(64, 16)
			Me.label38.TabIndex = 21
			Me.label38.Text = "继电器:"
			' 
			' groupBox19
			' 
			Me.groupBox19.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.groupBox19.Controls.Add(Me.button2)
			Me.groupBox19.Controls.Add(Me.button5)
			Me.groupBox19.Controls.Add(Me.cmbProtocol)
			Me.groupBox19.Controls.Add(Me.label35)
			Me.groupBox19.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox19.ForeColor = System.Drawing.Color.Black
			Me.groupBox19.Location = New System.Drawing.Point(9, 216)
			Me.groupBox19.Name = "groupBox19"
			Me.groupBox19.Size = New System.Drawing.Size(406, 93)
			Me.groupBox19.TabIndex = 67
			Me.groupBox19.TabStop = False
			Me.groupBox19.Text = "Protocol"
			' 
			' button2
			' 
			Me.button2.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button2.ForeColor = System.Drawing.Color.Black
			Me.button2.Location = New System.Drawing.Point(95, 49)
			Me.button2.Name = "button2"
			Me.button2.Size = New System.Drawing.Size(92, 31)
			Me.button2.TabIndex = 27
			Me.button2.Text = "Get"
			Me.button2.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button2.Click += new System.EventHandler(this.button2_Click_1);
			' 
			' button5
			' 
			Me.button5.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button5.ForeColor = System.Drawing.Color.Black
			Me.button5.Location = New System.Drawing.Point(214, 49)
			Me.button5.Name = "button5"
			Me.button5.Size = New System.Drawing.Size(90, 31)
			Me.button5.TabIndex = 26
			Me.button5.Text = "Set"
			Me.button5.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button5.Click += new System.EventHandler(this.button5_Click);
			' 
			' cmbProtocol
			' 
			Me.cmbProtocol.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbProtocol.FormattingEnabled = True
			Me.cmbProtocol.Items.AddRange(New Object() { "ISO18000-6C", "GB/T 29768", "GJB 7377.1", "ISO18000-6B"})
			Me.cmbProtocol.Location = New System.Drawing.Point(113, 18)
			Me.cmbProtocol.Name = "cmbProtocol"
			Me.cmbProtocol.Size = New System.Drawing.Size(220, 24)
			Me.cmbProtocol.TabIndex = 18
			' 
			' label35
			' 
			Me.label35.AutoSize = True
			Me.label35.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label35.Location = New System.Drawing.Point(36, 24)
			Me.label35.Name = "label35"
			Me.label35.Size = New System.Drawing.Size(80, 16)
			Me.label35.TabIndex = 22
			Me.label35.Text = "Protocol:"
			' 
			' gbWorkMode
			' 
			Me.gbWorkMode.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.gbWorkMode.Controls.Add(Me.plWorkModePara)
			Me.gbWorkMode.Controls.Add(Me.workMode)
			Me.gbWorkMode.Controls.Add(Me.button1)
			Me.gbWorkMode.Controls.Add(Me.btnGetWorkMode)
			Me.gbWorkMode.Controls.Add(Me.label29)
			Me.gbWorkMode.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.gbWorkMode.Location = New System.Drawing.Point(426, 464)
			Me.gbWorkMode.Name = "gbWorkMode"
			Me.gbWorkMode.Size = New System.Drawing.Size(466, 176)
			Me.gbWorkMode.TabIndex = 64
			Me.gbWorkMode.TabStop = False
			Me.gbWorkMode.Text = "work mode"
			' 
			' plWorkModePara
			' 
			Me.plWorkModePara.BackColor = System.Drawing.SystemColors.ControlLightLight
			Me.plWorkModePara.Controls.Add(Me.txtIT)
			Me.plWorkModePara.Controls.Add(Me.label52)
			Me.plWorkModePara.Controls.Add(Me.btnWorkModeParaGet)
			Me.plWorkModePara.Controls.Add(Me.btnWorkModeParaSet)
			Me.plWorkModePara.Controls.Add(Me.comRM)
			Me.plWorkModePara.Controls.Add(Me.Mode)
			Me.plWorkModePara.Controls.Add(Me.label50)
			Me.plWorkModePara.Controls.Add(Me.txtWT)
			Me.plWorkModePara.Controls.Add(Me.label49)
			Me.plWorkModePara.Controls.Add(Me.cmbInput)
			Me.plWorkModePara.Controls.Add(Me.label48)
			Me.plWorkModePara.Controls.Add(Me.label51)
			Me.plWorkModePara.Location = New System.Drawing.Point(15, 50)
			Me.plWorkModePara.Name = "plWorkModePara"
			Me.plWorkModePara.Size = New System.Drawing.Size(438, 113)
			Me.plWorkModePara.TabIndex = 73
			Me.plWorkModePara.Visible = False
			' 
			' txtIT
			' 
			Me.txtIT.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtIT.Location = New System.Drawing.Point(349, 46)
			Me.txtIT.Name = "txtIT"
			Me.txtIT.Size = New System.Drawing.Size(62, 26)
			Me.txtIT.TabIndex = 77
			Me.txtIT.Text = "0"
			' 
			' label52
			' 
			Me.label52.AutoSize = True
			Me.label52.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label52.Location = New System.Drawing.Point(406, 51)
			Me.label52.Name = "label52"
			Me.label52.Size = New System.Drawing.Size(35, 14)
			Me.label52.TabIndex = 83
			Me.label52.Text = "10ms"
			' 
			' btnWorkModeParaGet
			' 
			Me.btnWorkModeParaGet.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnWorkModeParaGet.ForeColor = System.Drawing.Color.Black
			Me.btnWorkModeParaGet.Location = New System.Drawing.Point(74, 76)
			Me.btnWorkModeParaGet.Name = "btnWorkModeParaGet"
			Me.btnWorkModeParaGet.Size = New System.Drawing.Size(91, 31)
			Me.btnWorkModeParaGet.TabIndex = 81
			Me.btnWorkModeParaGet.Text = "Get"
			Me.btnWorkModeParaGet.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnWorkModeParaGet.Click += new System.EventHandler(this.btnWorkModeParaGet_Click);
			' 
			' btnWorkModeParaSet
			' 
			Me.btnWorkModeParaSet.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnWorkModeParaSet.ForeColor = System.Drawing.Color.Black
			Me.btnWorkModeParaSet.Location = New System.Drawing.Point(212, 76)
			Me.btnWorkModeParaSet.Name = "btnWorkModeParaSet"
			Me.btnWorkModeParaSet.Size = New System.Drawing.Size(90, 29)
			Me.btnWorkModeParaSet.TabIndex = 80
			Me.btnWorkModeParaSet.Text = "Set"
			Me.btnWorkModeParaSet.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnWorkModeParaSet.Click += new System.EventHandler(this.btnWorkModeParaSet_Click);
			' 
			' comRM
			' 
			Me.comRM.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.comRM.FormattingEnabled = True
			Me.comRM.Items.AddRange(New Object() { "SerialPort", "UDP"})
			Me.comRM.Location = New System.Drawing.Point(349, 19)
			Me.comRM.Name = "comRM"
			Me.comRM.Size = New System.Drawing.Size(81, 24)
			Me.comRM.TabIndex = 79
			' 
			' Mode
			' 
			Me.Mode.AutoSize = True
			Me.Mode.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.Mode.Location = New System.Drawing.Point(269, 23)
			Me.Mode.Name = "Mode"
			Me.Mode.Size = New System.Drawing.Size(84, 14)
			Me.Mode.TabIndex = 78
			Me.Mode.Text = "OutputMode:"
			' 
			' label50
			' 
			Me.label50.AutoSize = True
			Me.label50.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label50.Location = New System.Drawing.Point(256, 49)
			Me.label50.Name = "label50"
			Me.label50.Size = New System.Drawing.Size(98, 14)
			Me.label50.TabIndex = 76
			Me.label50.Text = "IntervalTime:"
			' 
			' txtWT
			' 
			Me.txtWT.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtWT.Location = New System.Drawing.Point(163, 44)
			Me.txtWT.Name = "txtWT"
			Me.txtWT.Size = New System.Drawing.Size(58, 26)
			Me.txtWT.TabIndex = 75
			Me.txtWT.Text = "100"
			' 
			' label49
			' 
			Me.label49.AutoSize = True
			Me.label49.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label49.Location = New System.Drawing.Point(96, 53)
			Me.label49.Name = "label49"
			Me.label49.Size = New System.Drawing.Size(70, 14)
			Me.label49.TabIndex = 74
			Me.label49.Text = "WorkTime:"
			' 
			' cmbInput
			' 
			Me.cmbInput.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbInput.FormattingEnabled = True
			Me.cmbInput.Items.AddRange(New Object() { "1", "2"})
			Me.cmbInput.Location = New System.Drawing.Point(163, 16)
			Me.cmbInput.Name = "cmbInput"
			Me.cmbInput.Size = New System.Drawing.Size(58, 24)
			Me.cmbInput.TabIndex = 73
			' 
			' label48
			' 
			Me.label48.AutoSize = True
			Me.label48.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label48.Location = New System.Drawing.Point(0, 19)
			Me.label48.Name = "label48"
			Me.label48.Size = New System.Drawing.Size(154, 14)
			Me.label48.TabIndex = 72
			Me.label48.Text = "OptocouplerInputPort:"
			' 
			' label51
			' 
			Me.label51.AutoSize = True
			Me.label51.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label51.Location = New System.Drawing.Point(218, 49)
			Me.label51.Name = "label51"
			Me.label51.Size = New System.Drawing.Size(35, 14)
			Me.label51.TabIndex = 82
			Me.label51.Text = "10ms"
			' 
			' workMode
			' 
			Me.workMode.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.workMode.FormattingEnabled = True
			Me.workMode.Items.AddRange(New Object() { "命令工作模式", "自动工作模式", "触发模式"})
			Me.workMode.Location = New System.Drawing.Point(67, 19)
			Me.workMode.Name = "workMode"
			Me.workMode.Size = New System.Drawing.Size(136, 24)
			Me.workMode.TabIndex = 65
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.workMode.SelectedIndexChanged += new System.EventHandler(this.workMode_SelectedIndexChanged);
			' 
			' button1
			' 
			Me.button1.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button1.ForeColor = System.Drawing.Color.Black
			Me.button1.Location = New System.Drawing.Point(323, 15)
			Me.button1.Name = "button1"
			Me.button1.Size = New System.Drawing.Size(90, 31)
			Me.button1.TabIndex = 26
			Me.button1.Text = "Set"
			Me.button1.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button1.Click += new System.EventHandler(this.button1_Click);
			' 
			' btnGetWorkMode
			' 
			Me.btnGetWorkMode.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnGetWorkMode.ForeColor = System.Drawing.Color.Black
			Me.btnGetWorkMode.Location = New System.Drawing.Point(227, 14)
			Me.btnGetWorkMode.Name = "btnGetWorkMode"
			Me.btnGetWorkMode.Size = New System.Drawing.Size(90, 31)
			Me.btnGetWorkMode.TabIndex = 25
			Me.btnGetWorkMode.Text = "Get"
			Me.btnGetWorkMode.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnGetWorkMode.Click += new System.EventHandler(this.button2_Click);
			' 
			' label29
			' 
			Me.label29.AutoSize = True
			Me.label29.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label29.Location = New System.Drawing.Point(18, 22)
			Me.label29.Name = "label29"
			Me.label29.Size = New System.Drawing.Size(48, 16)
			Me.label29.TabIndex = 6
			Me.label29.Text = "Mode:"
			' 
			' gbIp2
			' 
			Me.gbIp2.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.gbIp2.Controls.Add(Me.ipControlDest)
			Me.gbIp2.Controls.Add(Me.btnSetIpDest)
			Me.gbIp2.Controls.Add(Me.btnGetIpDest)
			Me.gbIp2.Controls.Add(Me.txtPortDest)
			Me.gbIp2.Controls.Add(Me.label30)
			Me.gbIp2.Controls.Add(Me.label31)
			Me.gbIp2.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.gbIp2.Location = New System.Drawing.Point(9, 626)
			Me.gbIp2.Name = "gbIp2"
			Me.gbIp2.Size = New System.Drawing.Size(406, 153)
			Me.gbIp2.TabIndex = 63
			Me.gbIp2.TabStop = False
			Me.gbIp2.Text = "目标IP"
			' 
			' ipControlDest
			' 
			Me.ipControlDest.BackColor = System.Drawing.SystemColors.Control
			Me.ipControlDest.IpData = New String() { "", "", "", ""}
			Me.ipControlDest.Location = New System.Drawing.Point(113, 20)
			Me.ipControlDest.Name = "ipControlDest"
			Me.ipControlDest.Size = New System.Drawing.Size(264, 31)
			Me.ipControlDest.TabIndex = 28
			' 
			' btnSetIpDest
			' 
			Me.btnSetIpDest.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnSetIpDest.ForeColor = System.Drawing.Color.Black
			Me.btnSetIpDest.Location = New System.Drawing.Point(214, 107)
			Me.btnSetIpDest.Name = "btnSetIpDest"
			Me.btnSetIpDest.Size = New System.Drawing.Size(90, 31)
			Me.btnSetIpDest.TabIndex = 26
			Me.btnSetIpDest.Text = "Set"
			Me.btnSetIpDest.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnSetIpDest.Click += new System.EventHandler(this.btnSetIpDest_Click);
			' 
			' btnGetIpDest
			' 
			Me.btnGetIpDest.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnGetIpDest.ForeColor = System.Drawing.Color.Black
			Me.btnGetIpDest.Location = New System.Drawing.Point(99, 107)
			Me.btnGetIpDest.Name = "btnGetIpDest"
			Me.btnGetIpDest.Size = New System.Drawing.Size(90, 31)
			Me.btnGetIpDest.TabIndex = 25
			Me.btnGetIpDest.Text = "Get"
			Me.btnGetIpDest.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnGetIpDest.Click += new System.EventHandler(this.btnGetIpDest_Click);
			' 
			' txtPortDest
			' 
			Me.txtPortDest.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtPortDest.Location = New System.Drawing.Point(113, 69)
			Me.txtPortDest.Name = "txtPortDest"
			Me.txtPortDest.Size = New System.Drawing.Size(218, 26)
			Me.txtPortDest.TabIndex = 24
			' 
			' label30
			' 
			Me.label30.AutoSize = True
			Me.label30.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label30.Location = New System.Drawing.Point(59, 74)
			Me.label30.Name = "label30"
			Me.label30.Size = New System.Drawing.Size(48, 16)
			Me.label30.TabIndex = 7
			Me.label30.Text = "Port:"
			' 
			' label31
			' 
			Me.label31.AutoSize = True
			Me.label31.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label31.Location = New System.Drawing.Point(80, 27)
			Me.label31.Name = "label31"
			Me.label31.Size = New System.Drawing.Size(32, 16)
			Me.label31.TabIndex = 6
			Me.label31.Text = "IP:"
			' 
			' groupBox9
			' 
			Me.groupBox9.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.groupBox9.Controls.Add(Me.rbDisableBuzzer)
			Me.groupBox9.Controls.Add(Me.rbEnableBuzzer)
			Me.groupBox9.Controls.Add(Me.btnSetBuzzer)
			Me.groupBox9.Controls.Add(Me.btnGetBuzzer)
			Me.groupBox9.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox9.Location = New System.Drawing.Point(902, 196)
			Me.groupBox9.Name = "groupBox9"
			Me.groupBox9.Size = New System.Drawing.Size(355, 91)
			Me.groupBox9.TabIndex = 62
			Me.groupBox9.TabStop = False
			Me.groupBox9.Text = "蜂鸣器"
			' 
			' rbDisableBuzzer
			' 
			Me.rbDisableBuzzer.AutoSize = True
			Me.rbDisableBuzzer.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.rbDisableBuzzer.Location = New System.Drawing.Point(206, 20)
			Me.rbDisableBuzzer.Name = "rbDisableBuzzer"
			Me.rbDisableBuzzer.Size = New System.Drawing.Size(66, 20)
			Me.rbDisableBuzzer.TabIndex = 44
			Me.rbDisableBuzzer.TabStop = True
			Me.rbDisableBuzzer.Text = "Close"
			Me.rbDisableBuzzer.UseVisualStyleBackColor = True
			' 
			' rbEnableBuzzer
			' 
			Me.rbEnableBuzzer.AutoSize = True
			Me.rbEnableBuzzer.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.rbEnableBuzzer.Location = New System.Drawing.Point(66, 20)
			Me.rbEnableBuzzer.Name = "rbEnableBuzzer"
			Me.rbEnableBuzzer.Size = New System.Drawing.Size(58, 20)
			Me.rbEnableBuzzer.TabIndex = 43
			Me.rbEnableBuzzer.TabStop = True
			Me.rbEnableBuzzer.Text = "Open"
			Me.rbEnableBuzzer.UseVisualStyleBackColor = True
			' 
			' btnSetBuzzer
			' 
			Me.btnSetBuzzer.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnSetBuzzer.ForeColor = System.Drawing.Color.Black
			Me.btnSetBuzzer.Location = New System.Drawing.Point(194, 51)
			Me.btnSetBuzzer.Name = "btnSetBuzzer"
			Me.btnSetBuzzer.Size = New System.Drawing.Size(90, 31)
			Me.btnSetBuzzer.TabIndex = 26
			Me.btnSetBuzzer.Text = "Set"
			Me.btnSetBuzzer.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnSetBuzzer.Click += new System.EventHandler(this.btnSetBuzzer_Click);
			' 
			' btnGetBuzzer
			' 
			Me.btnGetBuzzer.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnGetBuzzer.ForeColor = System.Drawing.Color.Black
			Me.btnGetBuzzer.Location = New System.Drawing.Point(43, 51)
			Me.btnGetBuzzer.Name = "btnGetBuzzer"
			Me.btnGetBuzzer.Size = New System.Drawing.Size(90, 31)
			Me.btnGetBuzzer.TabIndex = 25
			Me.btnGetBuzzer.Text = "Get"
			Me.btnGetBuzzer.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnGetBuzzer.Click += new System.EventHandler(this.btnGetBuzzer_Click);
			' 
			' gbIP
			' 
			Me.gbIP.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.gbIP.Controls.Add(Me.label54)
			Me.gbIP.Controls.Add(Me.label53)
			Me.gbIP.Controls.Add(Me.ipGateway)
			Me.gbIP.Controls.Add(Me.ipControlSubnetMask)
			Me.gbIP.Controls.Add(Me.ipControlLocal)
			Me.gbIP.Controls.Add(Me.btnSetIPLocal)
			Me.gbIP.Controls.Add(Me.btnGetIPLocal)
			Me.gbIP.Controls.Add(Me.txtLocalPort)
			Me.gbIP.Controls.Add(Me.label9)
			Me.gbIP.Controls.Add(Me.label25)
			Me.gbIP.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.gbIP.Location = New System.Drawing.Point(9, 421)
			Me.gbIP.Name = "gbIP"
			Me.gbIP.Size = New System.Drawing.Size(406, 199)
			Me.gbIP.TabIndex = 61
			Me.gbIP.TabStop = False
			Me.gbIP.Text = "本机IP"
			' 
			' label54
			' 
			Me.label54.AutoSize = True
			Me.label54.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label54.Location = New System.Drawing.Point(40, 123)
			Me.label54.Name = "label54"
			Me.label54.Size = New System.Drawing.Size(72, 16)
			Me.label54.TabIndex = 31
			Me.label54.Text = "   网关:"
			' 
			' label53
			' 
			Me.label53.AutoSize = True
			Me.label53.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label53.Location = New System.Drawing.Point(9, 91)
			Me.label53.Name = "label53"
			Me.label53.Size = New System.Drawing.Size(104, 16)
			Me.label53.TabIndex = 30
			Me.label53.Text = "   子网掩码:"
			' 
			' ipGateway
			' 
			Me.ipGateway.BackColor = System.Drawing.SystemColors.Control
			Me.ipGateway.IpData = New String() { "", "", "", ""}
			Me.ipGateway.Location = New System.Drawing.Point(115, 119)
			Me.ipGateway.Margin = New System.Windows.Forms.Padding(5, 3, 5, 3)
			Me.ipGateway.Name = "ipGateway"
			Me.ipGateway.Size = New System.Drawing.Size(262, 31)
			Me.ipGateway.TabIndex = 29
			' 
			' ipControlSubnetMask
			' 
			Me.ipControlSubnetMask.BackColor = System.Drawing.SystemColors.Control
			Me.ipControlSubnetMask.IpData = New String() { "", "", "", ""}
			Me.ipControlSubnetMask.Location = New System.Drawing.Point(115, 84)
			Me.ipControlSubnetMask.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
			Me.ipControlSubnetMask.Name = "ipControlSubnetMask"
			Me.ipControlSubnetMask.Size = New System.Drawing.Size(262, 31)
			Me.ipControlSubnetMask.TabIndex = 28
			' 
			' ipControlLocal
			' 
			Me.ipControlLocal.BackColor = System.Drawing.SystemColors.Control
			Me.ipControlLocal.IpData = New String() { "", "", "", ""}
			Me.ipControlLocal.Location = New System.Drawing.Point(115, 20)
			Me.ipControlLocal.Name = "ipControlLocal"
			Me.ipControlLocal.Size = New System.Drawing.Size(262, 31)
			Me.ipControlLocal.TabIndex = 27
			' 
			' btnSetIPLocal
			' 
			Me.btnSetIPLocal.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnSetIPLocal.ForeColor = System.Drawing.Color.Black
			Me.btnSetIPLocal.Location = New System.Drawing.Point(214, 156)
			Me.btnSetIPLocal.Name = "btnSetIPLocal"
			Me.btnSetIPLocal.Size = New System.Drawing.Size(90, 31)
			Me.btnSetIPLocal.TabIndex = 26
			Me.btnSetIPLocal.Text = "Set"
			Me.btnSetIPLocal.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnSetIPLocal.Click += new System.EventHandler(this.btnSetIPLocal_Click);
			' 
			' btnGetIPLocal
			' 
			Me.btnGetIPLocal.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnGetIPLocal.ForeColor = System.Drawing.Color.Black
			Me.btnGetIPLocal.Location = New System.Drawing.Point(99, 156)
			Me.btnGetIPLocal.Name = "btnGetIPLocal"
			Me.btnGetIPLocal.Size = New System.Drawing.Size(90, 31)
			Me.btnGetIPLocal.TabIndex = 25
			Me.btnGetIPLocal.Text = "Get"
			Me.btnGetIPLocal.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnGetIPLocal.Click += new System.EventHandler(this.btnGetIPLocal_Click);
			' 
			' txtLocalPort
			' 
			Me.txtLocalPort.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtLocalPort.Location = New System.Drawing.Point(116, 54)
			Me.txtLocalPort.Name = "txtLocalPort"
			Me.txtLocalPort.Size = New System.Drawing.Size(261, 26)
			Me.txtLocalPort.TabIndex = 24
			' 
			' label9
			' 
			Me.label9.AutoSize = True
			Me.label9.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label9.Location = New System.Drawing.Point(80, 27)
			Me.label9.Name = "label9"
			Me.label9.Size = New System.Drawing.Size(32, 16)
			Me.label9.TabIndex = 6
			Me.label9.Text = "IP:"
			' 
			' label25
			' 
			Me.label25.AutoSize = True
			Me.label25.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label25.Location = New System.Drawing.Point(67, 59)
			Me.label25.Name = "label25"
			Me.label25.Size = New System.Drawing.Size(48, 16)
			Me.label25.TabIndex = 7
			Me.label25.Text = "Port:"
			' 
			' label8
			' 
			Me.label8.AutoSize = True
			Me.label8.ForeColor = System.Drawing.Color.Black
			Me.label8.Location = New System.Drawing.Point(205, 664)
			Me.label8.Name = "label8"
			Me.label8.Size = New System.Drawing.Size(0, 12)
			Me.label8.TabIndex = 60
			' 
			' groupBox16
			' 
			Me.groupBox16.BackColor = System.Drawing.Color.Transparent
			Me.groupBox16.Controls.Add(Me.label27)
			Me.groupBox16.Controls.Add(Me.label34)
			Me.groupBox16.Controls.Add(Me.groupBox20)
			Me.groupBox16.Controls.Add(Me.cbDualSingelSave)
			Me.groupBox16.Controls.Add(Me.btnDualSingelGet)
			Me.groupBox16.Controls.Add(Me.rbDual)
			Me.groupBox16.Controls.Add(Me.rbSingel)
			Me.groupBox16.Controls.Add(Me.btnDualSingelSet)
			Me.groupBox16.ForeColor = System.Drawing.Color.Black
			Me.groupBox16.Location = New System.Drawing.Point(1286, 274)
			Me.groupBox16.Name = "groupBox16"
			Me.groupBox16.Size = New System.Drawing.Size(62, 102)
			Me.groupBox16.TabIndex = 47
			Me.groupBox16.TabStop = False
			Me.groupBox16.Text = "DualSingel"
			Me.groupBox16.Visible = False
			' 
			' label34
			' 
			Me.label34.AutoSize = True
			Me.label34.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label34.ForeColor = System.Drawing.Color.Blue
			Me.label34.Location = New System.Drawing.Point(115, 17)
			Me.label34.Name = "label34"
			Me.label34.Size = New System.Drawing.Size(72, 16)
			Me.label34.TabIndex = 48
			Me.label34.Text = "暂时隐藏"
			' 
			' groupBox20
			' 
			Me.groupBox20.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.groupBox20.Controls.Add(Me.label37)
			Me.groupBox20.Controls.Add(Me.rbDisable)
			Me.groupBox20.Controls.Add(Me.GetTemperatureProtect)
			Me.groupBox20.Controls.Add(Me.btnSetTemperatureProtect)
			Me.groupBox20.Controls.Add(Me.rbEnable)
			Me.groupBox20.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox20.Location = New System.Drawing.Point(0, 73)
			Me.groupBox20.Name = "groupBox20"
			Me.groupBox20.Size = New System.Drawing.Size(354, 90)
			Me.groupBox20.TabIndex = 66
			Me.groupBox20.TabStop = False
			Me.groupBox20.Text = "温度保护值"
			Me.groupBox20.Visible = False
			' 
			' label37
			' 
			Me.label37.AutoSize = True
			Me.label37.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label37.ForeColor = System.Drawing.Color.Blue
			Me.label37.Location = New System.Drawing.Point(110, 32)
			Me.label37.Name = "label37"
			Me.label37.Size = New System.Drawing.Size(72, 16)
			Me.label37.TabIndex = 50
			Me.label37.Text = "暂时隐藏"
			' 
			' rbDisable
			' 
			Me.rbDisable.AutoSize = True
			Me.rbDisable.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.rbDisable.Location = New System.Drawing.Point(188, 26)
			Me.rbDisable.Name = "rbDisable"
			Me.rbDisable.Size = New System.Drawing.Size(82, 20)
			Me.rbDisable.TabIndex = 42
			Me.rbDisable.TabStop = True
			Me.rbDisable.Text = "Disable"
			Me.rbDisable.UseVisualStyleBackColor = True
			' 
			' GetTemperatureProtect
			' 
			Me.GetTemperatureProtect.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.GetTemperatureProtect.ForeColor = System.Drawing.Color.Black
			Me.GetTemperatureProtect.Location = New System.Drawing.Point(25, 51)
			Me.GetTemperatureProtect.Name = "GetTemperatureProtect"
			Me.GetTemperatureProtect.Size = New System.Drawing.Size(90, 31)
			Me.GetTemperatureProtect.TabIndex = 40
			Me.GetTemperatureProtect.Text = "Get"
			Me.GetTemperatureProtect.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.GetTemperatureProtect.Click += new System.EventHandler(this.GetTemperatureProtect_Click);
			' 
			' btnSetTemperatureProtect
			' 
			Me.btnSetTemperatureProtect.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnSetTemperatureProtect.ForeColor = System.Drawing.Color.Black
			Me.btnSetTemperatureProtect.Location = New System.Drawing.Point(180, 51)
			Me.btnSetTemperatureProtect.Name = "btnSetTemperatureProtect"
			Me.btnSetTemperatureProtect.Size = New System.Drawing.Size(90, 31)
			Me.btnSetTemperatureProtect.TabIndex = 39
			Me.btnSetTemperatureProtect.Text = "Set"
			Me.btnSetTemperatureProtect.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnSetTemperatureProtect.Click += new System.EventHandler(this.btnSetTemperatureProtect_Click);
			' 
			' rbEnable
			' 
			Me.rbEnable.AutoSize = True
			Me.rbEnable.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.rbEnable.Location = New System.Drawing.Point(43, 25)
			Me.rbEnable.Name = "rbEnable"
			Me.rbEnable.Size = New System.Drawing.Size(74, 20)
			Me.rbEnable.TabIndex = 41
			Me.rbEnable.TabStop = True
			Me.rbEnable.Text = "Enable"
			Me.rbEnable.UseVisualStyleBackColor = True
			' 
			' cbDualSingelSave
			' 
			Me.cbDualSingelSave.AutoSize = True
			Me.cbDualSingelSave.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbDualSingelSave.Location = New System.Drawing.Point(289, 69)
			Me.cbDualSingelSave.Name = "cbDualSingelSave"
			Me.cbDualSingelSave.Size = New System.Drawing.Size(59, 20)
			Me.cbDualSingelSave.TabIndex = 47
			Me.cbDualSingelSave.Text = "Save"
			Me.cbDualSingelSave.UseVisualStyleBackColor = True
			' 
			' btnDualSingelGet
			' 
			Me.btnDualSingelGet.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnDualSingelGet.ForeColor = System.Drawing.Color.Black
			Me.btnDualSingelGet.Location = New System.Drawing.Point(38, 57)
			Me.btnDualSingelGet.Name = "btnDualSingelGet"
			Me.btnDualSingelGet.Size = New System.Drawing.Size(93, 31)
			Me.btnDualSingelGet.TabIndex = 46
			Me.btnDualSingelGet.Text = "Get"
			Me.btnDualSingelGet.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnDualSingelGet.Click += new System.EventHandler(this.btnDualSingelGet_Click);
			' 
			' rbDual
			' 
			Me.rbDual.AutoSize = True
			Me.rbDual.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.rbDual.Location = New System.Drawing.Point(57, 26)
			Me.rbDual.Name = "rbDual"
			Me.rbDual.Size = New System.Drawing.Size(58, 20)
			Me.rbDual.TabIndex = 43
			Me.rbDual.TabStop = True
			Me.rbDual.Text = "Dual"
			Me.rbDual.UseVisualStyleBackColor = True
			' 
			' rbSingel
			' 
			Me.rbSingel.AutoSize = True
			Me.rbSingel.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.rbSingel.Location = New System.Drawing.Point(193, 26)
			Me.rbSingel.Name = "rbSingel"
			Me.rbSingel.Size = New System.Drawing.Size(74, 20)
			Me.rbSingel.TabIndex = 44
			Me.rbSingel.TabStop = True
			Me.rbSingel.Text = "Singel"
			Me.rbSingel.UseVisualStyleBackColor = True
			' 
			' btnDualSingelSet
			' 
			Me.btnDualSingelSet.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnDualSingelSet.ForeColor = System.Drawing.Color.Black
			Me.btnDualSingelSet.Location = New System.Drawing.Point(193, 60)
			Me.btnDualSingelSet.Name = "btnDualSingelSet"
			Me.btnDualSingelSet.Size = New System.Drawing.Size(90, 31)
			Me.btnDualSingelSet.TabIndex = 45
			Me.btnDualSingelSet.Text = "Set"
			Me.btnDualSingelSet.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnDualSingelSet.Click += new System.EventHandler(this.btnDualSingelSet_Click);
			' 
			' groupBox15
			' 
			Me.groupBox15.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.groupBox15.Controls.Add(Me.rbTagfocusDisable)
			Me.groupBox15.Controls.Add(Me.btnrbTagfocusGet)
			Me.groupBox15.Controls.Add(Me.btnrbTagfocusSet)
			Me.groupBox15.Controls.Add(Me.rbTagfocusEnable)
			Me.groupBox15.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox15.ForeColor = System.Drawing.Color.Black
			Me.groupBox15.Location = New System.Drawing.Point(903, 13)
			Me.groupBox15.Name = "groupBox15"
			Me.groupBox15.Size = New System.Drawing.Size(355, 85)
			Me.groupBox15.TabIndex = 46
			Me.groupBox15.TabStop = False
			Me.groupBox15.Text = "Tagfocus"
			' 
			' rbTagfocusDisable
			' 
			Me.rbTagfocusDisable.AutoSize = True
			Me.rbTagfocusDisable.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.rbTagfocusDisable.Location = New System.Drawing.Point(206, 20)
			Me.rbTagfocusDisable.Name = "rbTagfocusDisable"
			Me.rbTagfocusDisable.Size = New System.Drawing.Size(82, 20)
			Me.rbTagfocusDisable.TabIndex = 42
			Me.rbTagfocusDisable.TabStop = True
			Me.rbTagfocusDisable.Text = "Disable"
			Me.rbTagfocusDisable.UseVisualStyleBackColor = True
			' 
			' btnrbTagfocusGet
			' 
			Me.btnrbTagfocusGet.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnrbTagfocusGet.ForeColor = System.Drawing.Color.Black
			Me.btnrbTagfocusGet.Location = New System.Drawing.Point(43, 43)
			Me.btnrbTagfocusGet.Name = "btnrbTagfocusGet"
			Me.btnrbTagfocusGet.Size = New System.Drawing.Size(90, 31)
			Me.btnrbTagfocusGet.TabIndex = 40
			Me.btnrbTagfocusGet.Text = "Get"
			Me.btnrbTagfocusGet.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnrbTagfocusGet.Click += new System.EventHandler(this.btnrbTagfocusGet_Click);
			' 
			' btnrbTagfocusSet
			' 
			Me.btnrbTagfocusSet.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnrbTagfocusSet.ForeColor = System.Drawing.Color.Black
			Me.btnrbTagfocusSet.Location = New System.Drawing.Point(198, 43)
			Me.btnrbTagfocusSet.Name = "btnrbTagfocusSet"
			Me.btnrbTagfocusSet.Size = New System.Drawing.Size(90, 31)
			Me.btnrbTagfocusSet.TabIndex = 39
			Me.btnrbTagfocusSet.Text = "Set"
			Me.btnrbTagfocusSet.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnrbTagfocusSet.Click += new System.EventHandler(this.btnrbTagfocusSet_Click);
			' 
			' rbTagfocusEnable
			' 
			Me.rbTagfocusEnable.AutoSize = True
			Me.rbTagfocusEnable.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.rbTagfocusEnable.Location = New System.Drawing.Point(66, 17)
			Me.rbTagfocusEnable.Name = "rbTagfocusEnable"
			Me.rbTagfocusEnable.Size = New System.Drawing.Size(74, 20)
			Me.rbTagfocusEnable.TabIndex = 41
			Me.rbTagfocusEnable.TabStop = True
			Me.rbTagfocusEnable.Text = "Enable"
			Me.rbTagfocusEnable.UseVisualStyleBackColor = True
			' 
			' groupBox14
			' 
			Me.groupBox14.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.groupBox14.Controls.Add(Me.rbFastIDDisable)
			Me.groupBox14.Controls.Add(Me.rbFastIDEnable)
			Me.groupBox14.Controls.Add(Me.btnFastIDGet)
			Me.groupBox14.Controls.Add(Me.btnFastIDSet)
			Me.groupBox14.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox14.ForeColor = System.Drawing.Color.Black
			Me.groupBox14.Location = New System.Drawing.Point(903, 104)
			Me.groupBox14.Name = "groupBox14"
			Me.groupBox14.Size = New System.Drawing.Size(355, 85)
			Me.groupBox14.TabIndex = 45
			Me.groupBox14.TabStop = False
			Me.groupBox14.Text = "FastID"
			' 
			' rbFastIDDisable
			' 
			Me.rbFastIDDisable.AutoSize = True
			Me.rbFastIDDisable.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.rbFastIDDisable.Location = New System.Drawing.Point(203, 17)
			Me.rbFastIDDisable.Name = "rbFastIDDisable"
			Me.rbFastIDDisable.Size = New System.Drawing.Size(82, 20)
			Me.rbFastIDDisable.TabIndex = 44
			Me.rbFastIDDisable.TabStop = True
			Me.rbFastIDDisable.Text = "Disable"
			Me.rbFastIDDisable.UseVisualStyleBackColor = True
			' 
			' rbFastIDEnable
			' 
			Me.rbFastIDEnable.AutoSize = True
			Me.rbFastIDEnable.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.rbFastIDEnable.Location = New System.Drawing.Point(58, 17)
			Me.rbFastIDEnable.Name = "rbFastIDEnable"
			Me.rbFastIDEnable.Size = New System.Drawing.Size(74, 20)
			Me.rbFastIDEnable.TabIndex = 43
			Me.rbFastIDEnable.TabStop = True
			Me.rbFastIDEnable.Text = "Enable"
			Me.rbFastIDEnable.UseVisualStyleBackColor = True
			' 
			' btnFastIDGet
			' 
			Me.btnFastIDGet.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnFastIDGet.ForeColor = System.Drawing.Color.Black
			Me.btnFastIDGet.Location = New System.Drawing.Point(38, 43)
			Me.btnFastIDGet.Name = "btnFastIDGet"
			Me.btnFastIDGet.Size = New System.Drawing.Size(90, 31)
			Me.btnFastIDGet.TabIndex = 40
			Me.btnFastIDGet.Text = "Get"
			Me.btnFastIDGet.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnFastIDGet.Click += new System.EventHandler(this.btnFastIDGet_Click);
			' 
			' btnFastIDSet
			' 
			Me.btnFastIDSet.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnFastIDSet.ForeColor = System.Drawing.Color.Black
			Me.btnFastIDSet.Location = New System.Drawing.Point(193, 43)
			Me.btnFastIDSet.Name = "btnFastIDSet"
			Me.btnFastIDSet.Size = New System.Drawing.Size(90, 31)
			Me.btnFastIDSet.TabIndex = 39
			Me.btnFastIDSet.Text = "Set"
			Me.btnFastIDSet.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnFastIDSet.Click += new System.EventHandler(this.btnFastIDSet_Click);
			' 
			' groupBox11
			' 
			Me.groupBox11.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.groupBox11.Controls.Add(Me.cmbRegion)
			Me.groupBox11.Controls.Add(Me.cbRegionSave)
			Me.groupBox11.Controls.Add(Me.btnRegionGet)
			Me.groupBox11.Controls.Add(Me.btnRegionSet)
			Me.groupBox11.Controls.Add(Me.label1)
			Me.groupBox11.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox11.ForeColor = System.Drawing.Color.Black
			Me.groupBox11.Location = New System.Drawing.Point(9, 123)
			Me.groupBox11.Name = "groupBox11"
			Me.groupBox11.Size = New System.Drawing.Size(406, 93)
			Me.groupBox11.TabIndex = 44
			Me.groupBox11.TabStop = False
			Me.groupBox11.Text = "Region"
			' 
			' cmbRegion
			' 
			Me.cmbRegion.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbRegion.FormattingEnabled = True
			Me.cmbRegion.Items.AddRange(New Object() { "China1(840~845MHz)", "China2(920~925MHz)", "Europe(865~868MHz)", "USA(902~928MHz)", "Korea(917~923MHz)", "Japan(952~953MHz)", "Taiwan(920~928Mhz)", "South Africa(915~919MHz)", "Peru(915-928 MHz)", "Russia(860MHz-867.6MHz)", "Malaysia", "Australia(920-926MHz)", "Indonesia(920-923MHz)"})
			Me.cmbRegion.Location = New System.Drawing.Point(113, 17)
			Me.cmbRegion.Name = "cmbRegion"
			Me.cmbRegion.Size = New System.Drawing.Size(220, 24)
			Me.cmbRegion.TabIndex = 18
			' 
			' cbRegionSave
			' 
			Me.cbRegionSave.AutoSize = True
			Me.cbRegionSave.Checked = True
			Me.cbRegionSave.CheckState = System.Windows.Forms.CheckState.Checked
			Me.cbRegionSave.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbRegionSave.Location = New System.Drawing.Point(311, 55)
			Me.cbRegionSave.Name = "cbRegionSave"
			Me.cbRegionSave.Size = New System.Drawing.Size(59, 20)
			Me.cbRegionSave.TabIndex = 25
			Me.cbRegionSave.Text = "Save"
			Me.cbRegionSave.UseVisualStyleBackColor = True
			' 
			' btnRegionGet
			' 
			Me.btnRegionGet.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnRegionGet.ForeColor = System.Drawing.Color.Black
			Me.btnRegionGet.Location = New System.Drawing.Point(95, 49)
			Me.btnRegionGet.Name = "btnRegionGet"
			Me.btnRegionGet.Size = New System.Drawing.Size(90, 31)
			Me.btnRegionGet.TabIndex = 24
			Me.btnRegionGet.Text = "Get"
			Me.btnRegionGet.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnRegionGet.Click += new System.EventHandler(this.btnRegionGet_Click);
			' 
			' btnRegionSet
			' 
			Me.btnRegionSet.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnRegionSet.ForeColor = System.Drawing.Color.Black
			Me.btnRegionSet.Location = New System.Drawing.Point(212, 49)
			Me.btnRegionSet.Name = "btnRegionSet"
			Me.btnRegionSet.Size = New System.Drawing.Size(92, 31)
			Me.btnRegionSet.TabIndex = 23
			Me.btnRegionSet.Text = "Set"
			Me.btnRegionSet.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnRegionSet.Click += new System.EventHandler(this.btnRegionSet_Click);
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label1.Location = New System.Drawing.Point(51, 24)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(64, 16)
			Me.label1.TabIndex = 22
			Me.label1.Text = "Region:"
			' 
			' gbAnt
			' 
			Me.gbAnt.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.gbAnt.Controls.Add(Me.groupBox2)
			Me.gbAnt.Controls.Add(Me.groupBox12)
			Me.gbAnt.Controls.Add(Me.groupBox13)
			Me.gbAnt.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.gbAnt.ForeColor = System.Drawing.Color.Black
			Me.gbAnt.Location = New System.Drawing.Point(426, 153)
			Me.gbAnt.Name = "gbAnt"
			Me.gbAnt.Size = New System.Drawing.Size(466, 303)
			Me.gbAnt.TabIndex = 43
			Me.gbAnt.TabStop = False
			Me.gbAnt.Text = "ANT"
			' 
			' groupBox2
			' 
			Me.groupBox2.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.groupBox2.Controls.Add(Me.btnAntennaConnectionState)
			Me.groupBox2.Controls.Add(Me.cbANT2_state)
			Me.groupBox2.Controls.Add(Me.cbANT1_state)
			Me.groupBox2.Controls.Add(Me.cbANT3_state)
			Me.groupBox2.Controls.Add(Me.cbANT4_state)
			Me.groupBox2.Controls.Add(Me.cbANT5_state)
			Me.groupBox2.Controls.Add(Me.cbANT6_state)
			Me.groupBox2.Controls.Add(Me.cbANT8_state)
			Me.groupBox2.Controls.Add(Me.cbANT7_state)
			Me.groupBox2.Location = New System.Drawing.Point(9, 129)
			Me.groupBox2.Name = "groupBox2"
			Me.groupBox2.Size = New System.Drawing.Size(450, 71)
			Me.groupBox2.TabIndex = 60
			Me.groupBox2.TabStop = False
			' 
			' btnAntennaConnectionState
			' 
			Me.btnAntennaConnectionState.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnAntennaConnectionState.ForeColor = System.Drawing.Color.Black
			Me.btnAntennaConnectionState.Location = New System.Drawing.Point(91, 34)
			Me.btnAntennaConnectionState.Name = "btnAntennaConnectionState"
			Me.btnAntennaConnectionState.Size = New System.Drawing.Size(302, 31)
			Me.btnAntennaConnectionState.TabIndex = 41
			Me.btnAntennaConnectionState.Text = "Antenna connection state"
			Me.btnAntennaConnectionState.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnAntennaConnectionState.Click += new System.EventHandler(this.btnAntennaConnectionState_Click);
			' 
			' cbANT2_state
			' 
			Me.cbANT2_state.AutoSize = True
			Me.cbANT2_state.Enabled = False
			Me.cbANT2_state.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbANT2_state.Location = New System.Drawing.Point(67, 16)
			Me.cbANT2_state.Name = "cbANT2_state"
			Me.cbANT2_state.Size = New System.Drawing.Size(48, 16)
			Me.cbANT2_state.TabIndex = 43
			Me.cbANT2_state.Text = "ANT2"
			Me.cbANT2_state.UseVisualStyleBackColor = True
			' 
			' cbANT1_state
			' 
			Me.cbANT1_state.AutoSize = True
			Me.cbANT1_state.Enabled = False
			Me.cbANT1_state.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbANT1_state.Location = New System.Drawing.Point(14, 16)
			Me.cbANT1_state.Name = "cbANT1_state"
			Me.cbANT1_state.Size = New System.Drawing.Size(48, 16)
			Me.cbANT1_state.TabIndex = 42
			Me.cbANT1_state.Text = "ANT1"
			Me.cbANT1_state.UseVisualStyleBackColor = True
			' 
			' cbANT3_state
			' 
			Me.cbANT3_state.AutoSize = True
			Me.cbANT3_state.Enabled = False
			Me.cbANT3_state.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbANT3_state.Location = New System.Drawing.Point(123, 16)
			Me.cbANT3_state.Name = "cbANT3_state"
			Me.cbANT3_state.Size = New System.Drawing.Size(48, 16)
			Me.cbANT3_state.TabIndex = 44
			Me.cbANT3_state.Text = "ANT3"
			Me.cbANT3_state.UseVisualStyleBackColor = True
			' 
			' cbANT4_state
			' 
			Me.cbANT4_state.AutoSize = True
			Me.cbANT4_state.Enabled = False
			Me.cbANT4_state.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbANT4_state.Location = New System.Drawing.Point(177, 16)
			Me.cbANT4_state.Name = "cbANT4_state"
			Me.cbANT4_state.Size = New System.Drawing.Size(48, 16)
			Me.cbANT4_state.TabIndex = 45
			Me.cbANT4_state.Text = "ANT4"
			Me.cbANT4_state.UseVisualStyleBackColor = True
			' 
			' cbANT5_state
			' 
			Me.cbANT5_state.AutoSize = True
			Me.cbANT5_state.Enabled = False
			Me.cbANT5_state.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbANT5_state.Location = New System.Drawing.Point(231, 16)
			Me.cbANT5_state.Name = "cbANT5_state"
			Me.cbANT5_state.Size = New System.Drawing.Size(48, 16)
			Me.cbANT5_state.TabIndex = 46
			Me.cbANT5_state.Text = "ANT5"
			Me.cbANT5_state.UseVisualStyleBackColor = True
			' 
			' cbANT6_state
			' 
			Me.cbANT6_state.AutoSize = True
			Me.cbANT6_state.Enabled = False
			Me.cbANT6_state.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbANT6_state.Location = New System.Drawing.Point(285, 16)
			Me.cbANT6_state.Name = "cbANT6_state"
			Me.cbANT6_state.Size = New System.Drawing.Size(48, 16)
			Me.cbANT6_state.TabIndex = 47
			Me.cbANT6_state.Text = "ANT6"
			Me.cbANT6_state.UseVisualStyleBackColor = True
			' 
			' cbANT8_state
			' 
			Me.cbANT8_state.AutoSize = True
			Me.cbANT8_state.Enabled = False
			Me.cbANT8_state.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbANT8_state.Location = New System.Drawing.Point(388, 16)
			Me.cbANT8_state.Name = "cbANT8_state"
			Me.cbANT8_state.Size = New System.Drawing.Size(48, 16)
			Me.cbANT8_state.TabIndex = 49
			Me.cbANT8_state.Text = "ANT8"
			Me.cbANT8_state.UseVisualStyleBackColor = True
			' 
			' cbANT7_state
			' 
			Me.cbANT7_state.AutoSize = True
			Me.cbANT7_state.Enabled = False
			Me.cbANT7_state.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbANT7_state.Location = New System.Drawing.Point(339, 16)
			Me.cbANT7_state.Name = "cbANT7_state"
			Me.cbANT7_state.Size = New System.Drawing.Size(48, 16)
			Me.cbANT7_state.TabIndex = 48
			Me.cbANT7_state.Text = "ANT7"
			Me.cbANT7_state.UseVisualStyleBackColor = True
			' 
			' groupBox12
			' 
			Me.groupBox12.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.groupBox12.Controls.Add(Me.label7)
			Me.groupBox12.Controls.Add(Me.cbAntWorkTime)
			Me.groupBox12.Controls.Add(Me.btnGetANTWorkTime)
			Me.groupBox12.Controls.Add(Me.btnSetANTWorkTime)
			Me.groupBox12.Controls.Add(Me.label3)
			Me.groupBox12.Controls.Add(Me.cmbAntWorkTime)
			Me.groupBox12.Controls.Add(Me.txtAntWorkTime)
			Me.groupBox12.Controls.Add(Me.label4)
			Me.groupBox12.Location = New System.Drawing.Point(12, 198)
			Me.groupBox12.Name = "groupBox12"
			Me.groupBox12.Size = New System.Drawing.Size(451, 96)
			Me.groupBox12.TabIndex = 45
			Me.groupBox12.TabStop = False
			' 
			' label7
			' 
			Me.label7.AutoSize = True
			Me.label7.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label7.ForeColor = System.Drawing.Color.Maroon
			Me.label7.Location = New System.Drawing.Point(357, 20)
			Me.label7.Name = "label7"
			Me.label7.Size = New System.Drawing.Size(77, 14)
			Me.label7.TabIndex = 66
			Me.label7.Text = "10-65535ms"
			' 
			' cbAntWorkTime
			' 
			Me.cbAntWorkTime.AutoSize = True
			Me.cbAntWorkTime.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbAntWorkTime.Location = New System.Drawing.Point(360, 59)
			Me.cbAntWorkTime.Name = "cbAntWorkTime"
			Me.cbAntWorkTime.Size = New System.Drawing.Size(54, 18)
			Me.cbAntWorkTime.TabIndex = 65
			Me.cbAntWorkTime.Text = "Save"
			Me.cbAntWorkTime.UseVisualStyleBackColor = True
			' 
			' btnGetANTWorkTime
			' 
			Me.btnGetANTWorkTime.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnGetANTWorkTime.ForeColor = System.Drawing.Color.Black
			Me.btnGetANTWorkTime.Location = New System.Drawing.Point(59, 53)
			Me.btnGetANTWorkTime.Name = "btnGetANTWorkTime"
			Me.btnGetANTWorkTime.Size = New System.Drawing.Size(90, 31)
			Me.btnGetANTWorkTime.TabIndex = 64
			Me.btnGetANTWorkTime.Text = "Get"
			Me.btnGetANTWorkTime.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnGetANTWorkTime.Click += new System.EventHandler(this.btnGetANTWorkTime_Click);
			' 
			' btnSetANTWorkTime
			' 
			Me.btnSetANTWorkTime.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnSetANTWorkTime.ForeColor = System.Drawing.Color.Black
			Me.btnSetANTWorkTime.Location = New System.Drawing.Point(229, 54)
			Me.btnSetANTWorkTime.Name = "btnSetANTWorkTime"
			Me.btnSetANTWorkTime.Size = New System.Drawing.Size(90, 31)
			Me.btnSetANTWorkTime.TabIndex = 63
			Me.btnSetANTWorkTime.Text = "Set"
			Me.btnSetANTWorkTime.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnSetANTWorkTime.Click += new System.EventHandler(this.btnSetANTWorkTime_Click);
			' 
			' label3
			' 
			Me.label3.AutoSize = True
			Me.label3.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label3.Location = New System.Drawing.Point(155, 20)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(80, 16)
			Me.label3.TabIndex = 59
			Me.label3.Text = "workTime:"
			' 
			' cmbAntWorkTime
			' 
			Me.cmbAntWorkTime.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbAntWorkTime.FormattingEnabled = True
			Me.cmbAntWorkTime.Items.AddRange(New Object() { "ANT1", "ANT2", "ANT3", "ANT4", "ANT5", "ANT6", "ANT7", "ANT8", "ANT9", "ANT10", "ANT11", "ANT12", "ANT13", "ANT14", "ANT15", "ANT16"})
			Me.cmbAntWorkTime.Location = New System.Drawing.Point(53, 16)
			Me.cmbAntWorkTime.Name = "cmbAntWorkTime"
			Me.cmbAntWorkTime.Size = New System.Drawing.Size(92, 24)
			Me.cmbAntWorkTime.TabIndex = 62
			' 
			' txtAntWorkTime
			' 
			Me.txtAntWorkTime.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtAntWorkTime.Location = New System.Drawing.Point(239, 15)
			Me.txtAntWorkTime.MaxLength = 5
			Me.txtAntWorkTime.Name = "txtAntWorkTime"
			Me.txtAntWorkTime.Size = New System.Drawing.Size(116, 26)
			Me.txtAntWorkTime.TabIndex = 60
			Me.txtAntWorkTime.Text = "200"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtAntWorkTime.TextChanged += new System.EventHandler(this.txtWorkTime_TextChanged);
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtAntWorkTime.LostFocus += new System.EventHandler(this.txtWorkTime_LostFocus);
			' 
			' label4
			' 
			Me.label4.AutoSize = True
			Me.label4.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label4.Location = New System.Drawing.Point(18, 22)
			Me.label4.Name = "label4"
			Me.label4.Size = New System.Drawing.Size(40, 16)
			Me.label4.TabIndex = 61
			Me.label4.Text = "ANT:"
			' 
			' groupBox13
			' 
			Me.groupBox13.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.groupBox13.Controls.Add(Me.btnGetANT)
			Me.groupBox13.Controls.Add(Me.cmbAnt16)
			Me.groupBox13.Controls.Add(Me.cbAntSet)
			Me.groupBox13.Controls.Add(Me.cmbAnt15)
			Me.groupBox13.Controls.Add(Me.btnSetAnt)
			Me.groupBox13.Controls.Add(Me.cmbAnt14)
			Me.groupBox13.Controls.Add(Me.cmbAnt2)
			Me.groupBox13.Controls.Add(Me.cmbAnt13)
			Me.groupBox13.Controls.Add(Me.cmbAnt1)
			Me.groupBox13.Controls.Add(Me.cmbAnt12)
			Me.groupBox13.Controls.Add(Me.cmbAnt3)
			Me.groupBox13.Controls.Add(Me.cmbAnt11)
			Me.groupBox13.Controls.Add(Me.cmbAnt4)
			Me.groupBox13.Controls.Add(Me.cmbAnt10)
			Me.groupBox13.Controls.Add(Me.cmbAnt5)
			Me.groupBox13.Controls.Add(Me.cmbAnt9)
			Me.groupBox13.Controls.Add(Me.cmbAnt6)
			Me.groupBox13.Controls.Add(Me.cmbAnt8)
			Me.groupBox13.Controls.Add(Me.cmbAnt7)
			Me.groupBox13.Location = New System.Drawing.Point(9, 16)
			Me.groupBox13.Name = "groupBox13"
			Me.groupBox13.Size = New System.Drawing.Size(450, 112)
			Me.groupBox13.TabIndex = 59
			Me.groupBox13.TabStop = False
			' 
			' btnGetANT
			' 
			Me.btnGetANT.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnGetANT.ForeColor = System.Drawing.Color.Black
			Me.btnGetANT.Location = New System.Drawing.Point(59, 72)
			Me.btnGetANT.Name = "btnGetANT"
			Me.btnGetANT.Size = New System.Drawing.Size(90, 31)
			Me.btnGetANT.TabIndex = 41
			Me.btnGetANT.Text = "Get"
			Me.btnGetANT.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnGetANT.Click += new System.EventHandler(this.btnGetANT_Click);
			' 
			' cmbAnt16
			' 
			Me.cmbAnt16.AutoSize = True
			Me.cmbAnt16.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbAnt16.Location = New System.Drawing.Point(388, 46)
			Me.cmbAnt16.Name = "cmbAnt16"
			Me.cmbAnt16.Size = New System.Drawing.Size(54, 16)
			Me.cmbAnt16.TabIndex = 57
			Me.cmbAnt16.Text = "ANT16"
			Me.cmbAnt16.UseVisualStyleBackColor = True
			' 
			' cbAntSet
			' 
			Me.cbAntSet.AutoSize = True
			Me.cbAntSet.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbAntSet.Location = New System.Drawing.Point(360, 82)
			Me.cbAntSet.Name = "cbAntSet"
			Me.cbAntSet.Size = New System.Drawing.Size(54, 18)
			Me.cbAntSet.TabIndex = 58
			Me.cbAntSet.Text = "Save"
			Me.cbAntSet.UseVisualStyleBackColor = True
			' 
			' cmbAnt15
			' 
			Me.cmbAnt15.AutoSize = True
			Me.cmbAnt15.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbAnt15.Location = New System.Drawing.Point(339, 46)
			Me.cmbAnt15.Name = "cmbAnt15"
			Me.cmbAnt15.Size = New System.Drawing.Size(54, 16)
			Me.cmbAnt15.TabIndex = 56
			Me.cmbAnt15.Text = "ANT15"
			Me.cmbAnt15.UseVisualStyleBackColor = True
			' 
			' btnSetAnt
			' 
			Me.btnSetAnt.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnSetAnt.ForeColor = System.Drawing.Color.Black
			Me.btnSetAnt.Location = New System.Drawing.Point(229, 72)
			Me.btnSetAnt.Name = "btnSetAnt"
			Me.btnSetAnt.Size = New System.Drawing.Size(90, 31)
			Me.btnSetAnt.TabIndex = 40
			Me.btnSetAnt.Text = "Set"
			Me.btnSetAnt.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnSetAnt.Click += new System.EventHandler(this.btnSetAnt_Click);
			' 
			' cmbAnt14
			' 
			Me.cmbAnt14.AutoSize = True
			Me.cmbAnt14.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbAnt14.Location = New System.Drawing.Point(285, 46)
			Me.cmbAnt14.Name = "cmbAnt14"
			Me.cmbAnt14.Size = New System.Drawing.Size(54, 16)
			Me.cmbAnt14.TabIndex = 55
			Me.cmbAnt14.Text = "ANT14"
			Me.cmbAnt14.UseVisualStyleBackColor = True
			' 
			' cmbAnt2
			' 
			Me.cmbAnt2.AutoSize = True
			Me.cmbAnt2.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbAnt2.Location = New System.Drawing.Point(67, 24)
			Me.cmbAnt2.Name = "cmbAnt2"
			Me.cmbAnt2.Size = New System.Drawing.Size(48, 16)
			Me.cmbAnt2.TabIndex = 43
			Me.cmbAnt2.Text = "ANT2"
			Me.cmbAnt2.UseVisualStyleBackColor = True
			' 
			' cmbAnt13
			' 
			Me.cmbAnt13.AutoSize = True
			Me.cmbAnt13.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbAnt13.Location = New System.Drawing.Point(231, 46)
			Me.cmbAnt13.Name = "cmbAnt13"
			Me.cmbAnt13.Size = New System.Drawing.Size(54, 16)
			Me.cmbAnt13.TabIndex = 54
			Me.cmbAnt13.Text = "ANT13"
			Me.cmbAnt13.UseVisualStyleBackColor = True
			' 
			' cmbAnt1
			' 
			Me.cmbAnt1.AutoSize = True
			Me.cmbAnt1.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbAnt1.Location = New System.Drawing.Point(14, 24)
			Me.cmbAnt1.Name = "cmbAnt1"
			Me.cmbAnt1.Size = New System.Drawing.Size(48, 16)
			Me.cmbAnt1.TabIndex = 42
			Me.cmbAnt1.Text = "ANT1"
			Me.cmbAnt1.UseVisualStyleBackColor = True
			' 
			' cmbAnt12
			' 
			Me.cmbAnt12.AutoSize = True
			Me.cmbAnt12.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbAnt12.Location = New System.Drawing.Point(177, 46)
			Me.cmbAnt12.Name = "cmbAnt12"
			Me.cmbAnt12.Size = New System.Drawing.Size(54, 16)
			Me.cmbAnt12.TabIndex = 53
			Me.cmbAnt12.Text = "ANT12"
			Me.cmbAnt12.UseVisualStyleBackColor = True
			' 
			' cmbAnt3
			' 
			Me.cmbAnt3.AutoSize = True
			Me.cmbAnt3.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbAnt3.Location = New System.Drawing.Point(123, 24)
			Me.cmbAnt3.Name = "cmbAnt3"
			Me.cmbAnt3.Size = New System.Drawing.Size(48, 16)
			Me.cmbAnt3.TabIndex = 44
			Me.cmbAnt3.Text = "ANT3"
			Me.cmbAnt3.UseVisualStyleBackColor = True
			' 
			' cmbAnt11
			' 
			Me.cmbAnt11.AutoSize = True
			Me.cmbAnt11.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbAnt11.Location = New System.Drawing.Point(123, 46)
			Me.cmbAnt11.Name = "cmbAnt11"
			Me.cmbAnt11.Size = New System.Drawing.Size(54, 16)
			Me.cmbAnt11.TabIndex = 52
			Me.cmbAnt11.Text = "ANT11"
			Me.cmbAnt11.UseVisualStyleBackColor = True
			' 
			' cmbAnt4
			' 
			Me.cmbAnt4.AutoSize = True
			Me.cmbAnt4.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbAnt4.Location = New System.Drawing.Point(177, 24)
			Me.cmbAnt4.Name = "cmbAnt4"
			Me.cmbAnt4.Size = New System.Drawing.Size(48, 16)
			Me.cmbAnt4.TabIndex = 45
			Me.cmbAnt4.Text = "ANT4"
			Me.cmbAnt4.UseVisualStyleBackColor = True
			' 
			' cmbAnt10
			' 
			Me.cmbAnt10.AutoSize = True
			Me.cmbAnt10.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbAnt10.Location = New System.Drawing.Point(67, 46)
			Me.cmbAnt10.Name = "cmbAnt10"
			Me.cmbAnt10.Size = New System.Drawing.Size(54, 16)
			Me.cmbAnt10.TabIndex = 51
			Me.cmbAnt10.Text = "ANT10"
			Me.cmbAnt10.UseVisualStyleBackColor = True
			' 
			' cmbAnt5
			' 
			Me.cmbAnt5.AutoSize = True
			Me.cmbAnt5.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbAnt5.Location = New System.Drawing.Point(231, 24)
			Me.cmbAnt5.Name = "cmbAnt5"
			Me.cmbAnt5.Size = New System.Drawing.Size(48, 16)
			Me.cmbAnt5.TabIndex = 46
			Me.cmbAnt5.Text = "ANT5"
			Me.cmbAnt5.UseVisualStyleBackColor = True
			' 
			' cmbAnt9
			' 
			Me.cmbAnt9.AutoSize = True
			Me.cmbAnt9.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbAnt9.Location = New System.Drawing.Point(14, 46)
			Me.cmbAnt9.Name = "cmbAnt9"
			Me.cmbAnt9.Size = New System.Drawing.Size(48, 16)
			Me.cmbAnt9.TabIndex = 50
			Me.cmbAnt9.Text = "ANT9"
			Me.cmbAnt9.UseVisualStyleBackColor = True
			' 
			' cmbAnt6
			' 
			Me.cmbAnt6.AutoSize = True
			Me.cmbAnt6.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbAnt6.Location = New System.Drawing.Point(285, 24)
			Me.cmbAnt6.Name = "cmbAnt6"
			Me.cmbAnt6.Size = New System.Drawing.Size(48, 16)
			Me.cmbAnt6.TabIndex = 47
			Me.cmbAnt6.Text = "ANT6"
			Me.cmbAnt6.UseVisualStyleBackColor = True
			' 
			' cmbAnt8
			' 
			Me.cmbAnt8.AutoSize = True
			Me.cmbAnt8.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbAnt8.Location = New System.Drawing.Point(388, 24)
			Me.cmbAnt8.Name = "cmbAnt8"
			Me.cmbAnt8.Size = New System.Drawing.Size(48, 16)
			Me.cmbAnt8.TabIndex = 49
			Me.cmbAnt8.Text = "ANT8"
			Me.cmbAnt8.UseVisualStyleBackColor = True
			' 
			' cmbAnt7
			' 
			Me.cmbAnt7.AutoSize = True
			Me.cmbAnt7.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbAnt7.Location = New System.Drawing.Point(339, 24)
			Me.cmbAnt7.Name = "cmbAnt7"
			Me.cmbAnt7.Size = New System.Drawing.Size(48, 16)
			Me.cmbAnt7.TabIndex = 48
			Me.cmbAnt7.Text = "ANT7"
			Me.cmbAnt7.UseVisualStyleBackColor = True
			' 
			' ConfigForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 12F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.ClientSize = New System.Drawing.Size(1364, 910)
			Me.Controls.Add(Me.panel1)
			Me.Name = "ConfigForm"
			Me.Text = "ConfigForm"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.Load += new System.EventHandler(this.ConfigForm_Load);
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigForm_FormClosing);
			Me.groupBox1.ResumeLayout(False)
			Me.groupBox3.ResumeLayout(False)
			Me.groupBox3.PerformLayout()
			Me.groupBox5.ResumeLayout(False)
			Me.groupBox5.PerformLayout()
			Me.groupBox6.ResumeLayout(False)
			Me.groupBox6.PerformLayout()
			Me.groupBox7.ResumeLayout(False)
			Me.groupBox7.PerformLayout()
			Me.panel1.ResumeLayout(False)
			Me.panel1.PerformLayout()
			Me.groupBox4.ResumeLayout(False)
			Me.groupBox10.ResumeLayout(False)
			Me.groupBox10.PerformLayout()
			Me.groupBox17.ResumeLayout(False)
			Me.groupBox17.PerformLayout()
			Me.groupBox25.ResumeLayout(False)
			Me.groupBox25.PerformLayout()
			Me.gbInventoryMode.ResumeLayout(False)
			Me.gbInventoryMode.PerformLayout()
			Me.groupBox8.ResumeLayout(False)
			Me.groupBox8.PerformLayout()
			Me.groupBox24.ResumeLayout(False)
			Me.groupBox24.PerformLayout()
			Me.bgGPIO.ResumeLayout(False)
			Me.groupBox23.ResumeLayout(False)
			Me.groupBox23.PerformLayout()
			Me.groupBox22.ResumeLayout(False)
			Me.groupBox22.PerformLayout()
			Me.groupBox19.ResumeLayout(False)
			Me.groupBox19.PerformLayout()
			Me.gbWorkMode.ResumeLayout(False)
			Me.gbWorkMode.PerformLayout()
			Me.plWorkModePara.ResumeLayout(False)
			Me.plWorkModePara.PerformLayout()
			Me.gbIp2.ResumeLayout(False)
			Me.gbIp2.PerformLayout()
			Me.groupBox9.ResumeLayout(False)
			Me.groupBox9.PerformLayout()
			Me.gbIP.ResumeLayout(False)
			Me.gbIP.PerformLayout()
			Me.groupBox16.ResumeLayout(False)
			Me.groupBox16.PerformLayout()
			Me.groupBox20.ResumeLayout(False)
			Me.groupBox20.PerformLayout()
			Me.groupBox15.ResumeLayout(False)
			Me.groupBox15.PerformLayout()
			Me.groupBox14.ResumeLayout(False)
			Me.groupBox14.PerformLayout()
			Me.groupBox11.ResumeLayout(False)
			Me.groupBox11.PerformLayout()
			Me.gbAnt.ResumeLayout(False)
			Me.groupBox2.ResumeLayout(False)
			Me.groupBox2.PerformLayout()
			Me.groupBox12.ResumeLayout(False)
			Me.groupBox12.PerformLayout()
			Me.groupBox13.ResumeLayout(False)
			Me.groupBox13.PerformLayout()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private groupBox1 As System.Windows.Forms.GroupBox
		Private WithEvents btnGetCW As System.Windows.Forms.Button
		Private WithEvents btnSetCW As System.Windows.Forms.Button
		Private groupBox3 As System.Windows.Forms.GroupBox
		Private WithEvents btnRFLinkGet As System.Windows.Forms.Button
		Private WithEvents btnRFLinkSet As System.Windows.Forms.Button
		Private cmbRFLink As System.Windows.Forms.ComboBox
		Private label5 As System.Windows.Forms.Label
		Private groupBox5 As System.Windows.Forms.GroupBox
		Private cmbQ As System.Windows.Forms.ComboBox
		Private label13 As System.Windows.Forms.Label
		Private cmbT As System.Windows.Forms.ComboBox
		Private label12 As System.Windows.Forms.Label
		Private cmbAction As System.Windows.Forms.ComboBox
		Private label11 As System.Windows.Forms.Label
		Private cmbTarget As System.Windows.Forms.ComboBox
		Private label10 As System.Windows.Forms.Label
		Private cmbCoding As System.Windows.Forms.ComboBox
		Private cmbDr As System.Windows.Forms.ComboBox
		Private cmbMaxQ As System.Windows.Forms.ComboBox
		Private cmbMinQ As System.Windows.Forms.ComboBox
		Private cmbStartQ As System.Windows.Forms.ComboBox
		Private label21 As System.Windows.Forms.Label
		Private label20 As System.Windows.Forms.Label
		Private label19 As System.Windows.Forms.Label
		Private label18 As System.Windows.Forms.Label
		Private label17 As System.Windows.Forms.Label
		Private label16 As System.Windows.Forms.Label
		Private label15 As System.Windows.Forms.Label
		Private label14 As System.Windows.Forms.Label
		Private cmbSession As System.Windows.Forms.ComboBox
		Private label26 As System.Windows.Forms.Label
		Private label22 As System.Windows.Forms.Label
		Private cmbSel As System.Windows.Forms.ComboBox
		Private cmbP As System.Windows.Forms.ComboBox
		Private cmbLinkFrequency As System.Windows.Forms.ComboBox
		Private cmbG As System.Windows.Forms.ComboBox
		Private groupBox6 As System.Windows.Forms.GroupBox
		Private label23 As System.Windows.Forms.Label
		Private WithEvents btnPowerSet_ANT1 As System.Windows.Forms.Button
		Private cmbPower_ANT1 As System.Windows.Forms.ComboBox
		Private groupBox7 As System.Windows.Forms.GroupBox
		Private WithEvents btnWorkModeSet As System.Windows.Forms.Button
		Private label28 As System.Windows.Forms.Label
		Private WithEvents panel1 As System.Windows.Forms.Panel
		Private WithEvents btnGen2Get As System.Windows.Forms.Button
		Private WithEvents btnGen2Set As System.Windows.Forms.Button
		Private groupBox8 As System.Windows.Forms.GroupBox
		Private WithEvents GetTemperatureProtect As System.Windows.Forms.Button
		Private WithEvents btnSetTemperatureProtect As System.Windows.Forms.Button
		Private WithEvents btnPowerGet_ANT1 As System.Windows.Forms.Button
		Private WithEvents btnWorkModeGet As System.Windows.Forms.Button
		Private gbAnt As System.Windows.Forms.GroupBox
		Private WithEvents btnGetANT As System.Windows.Forms.Button
		Private WithEvents btnSetAnt As System.Windows.Forms.Button
		Private cmbAnt7 As System.Windows.Forms.CheckBox
		Private cmbAnt6 As System.Windows.Forms.CheckBox
		Private cmbAnt5 As System.Windows.Forms.CheckBox
		Private cmbAnt4 As System.Windows.Forms.CheckBox
		Private cmbAnt3 As System.Windows.Forms.CheckBox
		Private cmbAnt2 As System.Windows.Forms.CheckBox
		Private cmbAnt1 As System.Windows.Forms.CheckBox
		Private cmbAnt16 As System.Windows.Forms.CheckBox
		Private cmbAnt15 As System.Windows.Forms.CheckBox
		Private cmbAnt14 As System.Windows.Forms.CheckBox
		Private cmbAnt13 As System.Windows.Forms.CheckBox
		Private cmbAnt12 As System.Windows.Forms.CheckBox
		Private cmbAnt11 As System.Windows.Forms.CheckBox
		Private cmbAnt10 As System.Windows.Forms.CheckBox
		Private cmbAnt9 As System.Windows.Forms.CheckBox
		Private cmbAnt8 As System.Windows.Forms.CheckBox
		Private cbAntSet As System.Windows.Forms.CheckBox
		Private groupBox11 As System.Windows.Forms.GroupBox
		Private cmbRegion As System.Windows.Forms.ComboBox
		Private label1 As System.Windows.Forms.Label
		Private cbRegionSave As System.Windows.Forms.CheckBox
		Private WithEvents btnRegionGet As System.Windows.Forms.Button
		Private WithEvents btnRegionSet As System.Windows.Forms.Button
		Private rbDisable As System.Windows.Forms.RadioButton
		Private rbEnable As System.Windows.Forms.RadioButton
		Private WithEvents txtAntWorkTime As System.Windows.Forms.TextBox
		Private label3 As System.Windows.Forms.Label
		Private WithEvents btnGetANTWorkTime As System.Windows.Forms.Button
		Private WithEvents btnSetANTWorkTime As System.Windows.Forms.Button
		Private cmbAntWorkTime As System.Windows.Forms.ComboBox
		Private label4 As System.Windows.Forms.Label
		Private groupBox12 As System.Windows.Forms.GroupBox
		Private groupBox13 As System.Windows.Forms.GroupBox
		Private cbAntWorkTime As System.Windows.Forms.CheckBox
		Private label7 As System.Windows.Forms.Label
		Private cbRFLink As System.Windows.Forms.CheckBox
		Private groupBox14 As System.Windows.Forms.GroupBox
		Private WithEvents btnFastIDGet As System.Windows.Forms.Button
		Private WithEvents btnFastIDSet As System.Windows.Forms.Button
		Private groupBox15 As System.Windows.Forms.GroupBox
		Private WithEvents btnrbTagfocusGet As System.Windows.Forms.Button
		Private WithEvents btnrbTagfocusSet As System.Windows.Forms.Button
		Private WithEvents btnReset As System.Windows.Forms.Button
		Private groupBox16 As System.Windows.Forms.GroupBox
		Private cbDualSingelSave As System.Windows.Forms.CheckBox
		Private WithEvents btnDualSingelGet As System.Windows.Forms.Button
		Private WithEvents btnDualSingelSet As System.Windows.Forms.Button
		Private rbSingel As System.Windows.Forms.RadioButton
		Private rbDual As System.Windows.Forms.RadioButton
		Private rbTagfocusDisable As System.Windows.Forms.RadioButton
		Private rbTagfocusEnable As System.Windows.Forms.RadioButton
		Private WithEvents textBox1 As System.Windows.Forms.TextBox
		Private label6 As System.Windows.Forms.Label
		Private label8 As System.Windows.Forms.Label
		Private label2 As System.Windows.Forms.Label
		Private gbIP As System.Windows.Forms.GroupBox
		Private WithEvents btnSetIPLocal As System.Windows.Forms.Button
		Private WithEvents btnGetIPLocal As System.Windows.Forms.Button
		Private txtLocalPort As System.Windows.Forms.TextBox
		Private label25 As System.Windows.Forms.Label
		Private label9 As System.Windows.Forms.Label
		Private groupBox9 As System.Windows.Forms.GroupBox
		Private WithEvents btnSetBuzzer As System.Windows.Forms.Button
		Private WithEvents btnGetBuzzer As System.Windows.Forms.Button
		Private gbIp2 As System.Windows.Forms.GroupBox
		Private WithEvents btnSetIpDest As System.Windows.Forms.Button
		Private WithEvents btnGetIpDest As System.Windows.Forms.Button
		Private txtPortDest As System.Windows.Forms.TextBox
		Private label30 As System.Windows.Forms.Label
		Private label31 As System.Windows.Forms.Label
		Private rbDisableBuzzer As System.Windows.Forms.RadioButton
		Private rbEnableBuzzer As System.Windows.Forms.RadioButton
		Private rbFastIDDisable As System.Windows.Forms.RadioButton
		Private rbFastIDEnable As System.Windows.Forms.RadioButton
		Private ipControlLocal As WindowsFormsControlLibrary1.IPControl
		Private ipControlDest As WindowsFormsControlLibrary1.IPControl
		Private gbWorkMode As System.Windows.Forms.GroupBox
		Private WithEvents button1 As System.Windows.Forms.Button
		Private WithEvents btnGetWorkMode As System.Windows.Forms.Button
		Private label29 As System.Windows.Forms.Label
		Private WithEvents workMode As System.Windows.Forms.ComboBox
		Private groupBox20 As System.Windows.Forms.GroupBox
		Private textBox3 As System.Windows.Forms.TextBox
		Private WithEvents button3 As System.Windows.Forms.Button
		Private label32 As System.Windows.Forms.Label
		Private label33 As System.Windows.Forms.Label
		Private WithEvents button4 As System.Windows.Forms.Button
		Private label34 As System.Windows.Forms.Label
		Private label27 As System.Windows.Forms.Label
		Private groupBox19 As System.Windows.Forms.GroupBox
		Private WithEvents button2 As System.Windows.Forms.Button
		Private WithEvents button5 As System.Windows.Forms.Button
		Private cmbProtocol As System.Windows.Forms.ComboBox
		Private label35 As System.Windows.Forms.Label
		Private comboBox1 As System.Windows.Forms.ComboBox
		Private bgGPIO As System.Windows.Forms.GroupBox
		Private WithEvents button7 As System.Windows.Forms.Button
		Private WithEvents button6 As System.Windows.Forms.Button
		Private cmbOutStatus As System.Windows.Forms.ComboBox
		Private label38 As System.Windows.Forms.Label
		Private groupBox22 As System.Windows.Forms.GroupBox
		Private groupBox23 As System.Windows.Forms.GroupBox
		Private comboBox2 As System.Windows.Forms.ComboBox
		Private comboBox3 As System.Windows.Forms.ComboBox
		Private label39 As System.Windows.Forms.Label
		Private label40 As System.Windows.Forms.Label
		Private label37 As System.Windows.Forms.Label
		Private label36 As System.Windows.Forms.Label
		Private groupBox24 As System.Windows.Forms.GroupBox
		Private label43 As System.Windows.Forms.Label
		Private label44 As System.Windows.Forms.Label
		Private txtWaitTime As System.Windows.Forms.TextBox
		Private txtworkTime As System.Windows.Forms.TextBox
		Private label42 As System.Windows.Forms.Label
		Private label41 As System.Windows.Forms.Label
		Private WithEvents button8 As System.Windows.Forms.Button
		Private WithEvents button9 As System.Windows.Forms.Button
		Private checkBox1 As System.Windows.Forms.CheckBox
		Private gbInventoryMode As System.Windows.Forms.GroupBox
		Private txtUserLen As System.Windows.Forms.TextBox
		Private label47 As System.Windows.Forms.Label
		Private txtUserPtr As System.Windows.Forms.TextBox
		Private label46 As System.Windows.Forms.Label
		Private WithEvents cbInventoryMode As System.Windows.Forms.ComboBox
		Private label45 As System.Windows.Forms.Label
		Private WithEvents button10 As System.Windows.Forms.Button
		Private WithEvents button11 As System.Windows.Forms.Button
		Private plWorkModePara As System.Windows.Forms.Panel
		Private comRM As System.Windows.Forms.ComboBox
		Private Mode As System.Windows.Forms.Label
		Private txtIT As System.Windows.Forms.TextBox
		Private label50 As System.Windows.Forms.Label
		Private txtWT As System.Windows.Forms.TextBox
		Private label49 As System.Windows.Forms.Label
		Private cmbInput As System.Windows.Forms.ComboBox
		Private label48 As System.Windows.Forms.Label
		Private WithEvents btnWorkModeParaGet As System.Windows.Forms.Button
		Private WithEvents btnWorkModeParaSet As System.Windows.Forms.Button
		Private label52 As System.Windows.Forms.Label
		Private label51 As System.Windows.Forms.Label
		Private ipGateway As WindowsFormsControlLibrary1.IPControl
		Private ipControlSubnetMask As WindowsFormsControlLibrary1.IPControl
		Private label54 As System.Windows.Forms.Label
		Private label53 As System.Windows.Forms.Label
		Private label24 As System.Windows.Forms.Label
		Private cbPower As System.Windows.Forms.CheckBox
		Private checkBox2 As System.Windows.Forms.CheckBox
		Private groupBox2 As System.Windows.Forms.GroupBox
		Private WithEvents btnAntennaConnectionState As System.Windows.Forms.Button
		Private cbANT2_state As System.Windows.Forms.CheckBox
		Private cbANT1_state As System.Windows.Forms.CheckBox
		Private cbANT3_state As System.Windows.Forms.CheckBox
		Private cbANT4_state As System.Windows.Forms.CheckBox
		Private cbANT5_state As System.Windows.Forms.CheckBox
		Private cbANT6_state As System.Windows.Forms.CheckBox
		Private cbANT8_state As System.Windows.Forms.CheckBox
		Private cbANT7_state As System.Windows.Forms.CheckBox
		Private groupBox25 As System.Windows.Forms.GroupBox
		Private WithEvents btnCalibration As System.Windows.Forms.Button
		Private txtCalibration As System.Windows.Forms.TextBox
		Private groupBox4 As System.Windows.Forms.GroupBox
		Private groupBox10 As System.Windows.Forms.GroupBox
		Private cmbInput1 As System.Windows.Forms.ComboBox
		Private WithEvents button12 As System.Windows.Forms.Button
		Private cmbInput2 As System.Windows.Forms.ComboBox
		Private label55 As System.Windows.Forms.Label
		Private label56 As System.Windows.Forms.Label
		Private groupBox17 As System.Windows.Forms.GroupBox
		Private cmbOutput2 As System.Windows.Forms.ComboBox
		Private label58 As System.Windows.Forms.Label
		Private WithEvents button13 As System.Windows.Forms.Button
		Private cmbOutput1 As System.Windows.Forms.ComboBox
		Private label57 As System.Windows.Forms.Label
	End Class
End Namespace
