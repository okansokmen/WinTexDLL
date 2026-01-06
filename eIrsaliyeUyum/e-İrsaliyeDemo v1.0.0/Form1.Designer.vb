Namespace eIrsaliyeUyum
	Partial Public Class Form1
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
            Me.tabControl1 = New System.Windows.Forms.TabControl()
            Me.tabPage1 = New System.Windows.Forms.TabPage()
            Me.groupBox10 = New System.Windows.Forms.GroupBox()
            Me.groupBox13 = New System.Windows.Forms.GroupBox()
            Me.SetDespatchReceiptAdvicesTaken = New System.Windows.Forms.Button()
            Me.btnGetInboxReceiptAdvices = New System.Windows.Forms.Button()
            Me.groupBox12 = New System.Windows.Forms.GroupBox()
            Me.btnInboxReponseStatus = New System.Windows.Forms.Button()
            Me.btnSendDespatchResponse = New System.Windows.Forms.Button()
            Me.groupbox = New System.Windows.Forms.GroupBox()
            Me.groupBox9 = New System.Windows.Forms.GroupBox()
            Me.label18 = New System.Windows.Forms.Label()
            Me.label21 = New System.Windows.Forms.Label()
            Me.label20 = New System.Windows.Forms.Label()
            Me.label16 = New System.Windows.Forms.Label()
            Me.label17 = New System.Windows.Forms.Label()
            Me.label12 = New System.Windows.Forms.Label()
            Me.btnGetInboxDespatch = New System.Windows.Forms.Button()
            Me.btnGetInboxDespatches = New System.Windows.Forms.Button()
            Me.btnGetInboxDespatchList = New System.Windows.Forms.Button()
            Me.btnGetInboxDespatchData = New System.Windows.Forms.Button()
            Me.btnGetInboxDespatchPdf = New System.Windows.Forms.Button()
            Me.GetInboxDespatchView = New System.Windows.Forms.Button()
            Me.groupBox11 = New System.Windows.Forms.GroupBox()
            Me.btnGetOutboxDespatchData = New System.Windows.Forms.Button()
            Me.label26 = New System.Windows.Forms.Label()
            Me.label25 = New System.Windows.Forms.Label()
            Me.label24 = New System.Windows.Forms.Label()
            Me.label23 = New System.Windows.Forms.Label()
            Me.label22 = New System.Windows.Forms.Label()
            Me.label19 = New System.Windows.Forms.Label()
            Me.btnGetOutboxDespatchView = New System.Windows.Forms.Button()
            Me.btnGetOutboxDespatchList = New System.Windows.Forms.Button()
            Me.btnGetOutboxDespatchPdf = New System.Windows.Forms.Button()
            Me.btnGetOutboxDespatch = New System.Windows.Forms.Button()
            Me.btnGetOutboxDespatches = New System.Windows.Forms.Button()
            Me.groupBox5 = New System.Windows.Forms.GroupBox()
            Me.groupBox6 = New System.Windows.Forms.GroupBox()
            Me.btnQueryInboxDespatchStatus = New System.Windows.Forms.Button()
            Me.label15 = New System.Windows.Forms.Label()
            Me.label11 = New System.Windows.Forms.Label()
            Me.btnQueryOutboxDespatchStatus = New System.Windows.Forms.Button()
            Me.groupBox8 = New System.Windows.Forms.GroupBox()
            Me.label14 = New System.Windows.Forms.Label()
            Me.btnGetInboxDespatchStatusWithLogs = New System.Windows.Forms.Button()
            Me.label13 = New System.Windows.Forms.Label()
            Me.btnGetOutboxDespatchStatusWithLogs = New System.Windows.Forms.Button()
            Me.groupBox1 = New System.Windows.Forms.GroupBox()
            Me.groupBox3 = New System.Windows.Forms.GroupBox()
            Me.btnSendDespatch = New System.Windows.Forms.Button()
            Me.label6 = New System.Windows.Forms.Label()
            Me.groupBox4 = New System.Windows.Forms.GroupBox()
            Me.btnCompressedSendDespatch = New System.Windows.Forms.Button()
            Me.label10 = New System.Windows.Forms.Label()
            Me.groupBox2 = New System.Windows.Forms.GroupBox()
            Me.label8 = New System.Windows.Forms.Label()
            Me.label7 = New System.Windows.Forms.Label()
            Me.label9 = New System.Windows.Forms.Label()
            Me.btnSaveDraftDespatch = New System.Windows.Forms.Button()
            Me.btnSendDraft = New System.Windows.Forms.Button()
            Me.btnCancelDraft = New System.Windows.Forms.Button()
            Me.label28 = New System.Windows.Forms.Label()
            Me.label27 = New System.Windows.Forms.Label()
            Me.label5 = New System.Windows.Forms.Label()
            Me.label4 = New System.Windows.Forms.Label()
            Me.txtVKNAlici = New System.Windows.Forms.TextBox()
            Me.txtGondericiVKN = New System.Windows.Forms.TextBox()
            Me.txtDespatchUUID = New System.Windows.Forms.TextBox()
            Me.txtDespatchID = New System.Windows.Forms.TextBox()
            Me.btnCreateDespatchFromDespatchInfoXml = New System.Windows.Forms.Button()
            Me.btnPreviewDespatch = New System.Windows.Forms.Button()
            Me.btnCreateDespatchFromDespatchXML = New System.Windows.Forms.Button()
            Me.btnSaveDespatchXml = New System.Windows.Forms.Button()
            Me.tabPage2 = New System.Windows.Forms.TabPage()
            Me.tabControl2 = New System.Windows.Forms.TabControl()
            Me.tabPage3 = New System.Windows.Forms.TabPage()
            Me.label3 = New System.Windows.Forms.Label()
            Me.label2 = New System.Windows.Forms.Label()
            Me.label1 = New System.Windows.Forms.Label()
            Me.txtConnectionTestPassword = New System.Windows.Forms.TextBox()
            Me.txtConnectionTestUserName = New System.Windows.Forms.TextBox()
            Me.txtConnectionTestUri = New System.Windows.Forms.TextBox()
            Me.btnSaveConnectionTestInfo = New System.Windows.Forms.Button()
            Me.tabPage4 = New System.Windows.Forms.TabPage()
            Me.openFileDialog1 = New System.Windows.Forms.OpenFileDialog()
            Me.tabControl1.SuspendLayout()
            Me.tabPage1.SuspendLayout()
            Me.groupBox10.SuspendLayout()
            Me.groupBox13.SuspendLayout()
            Me.groupBox12.SuspendLayout()
            Me.groupbox.SuspendLayout()
            Me.groupBox9.SuspendLayout()
            Me.groupBox11.SuspendLayout()
            Me.groupBox5.SuspendLayout()
            Me.groupBox6.SuspendLayout()
            Me.groupBox8.SuspendLayout()
            Me.groupBox1.SuspendLayout()
            Me.groupBox3.SuspendLayout()
            Me.groupBox4.SuspendLayout()
            Me.groupBox2.SuspendLayout()
            Me.tabPage2.SuspendLayout()
            Me.tabControl2.SuspendLayout()
            Me.tabPage3.SuspendLayout()
            Me.SuspendLayout()
            '
            'tabControl1
            '
            Me.tabControl1.Controls.Add(Me.tabPage1)
            Me.tabControl1.Controls.Add(Me.tabPage2)
            Me.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tabControl1.Location = New System.Drawing.Point(0, 0)
            Me.tabControl1.Margin = New System.Windows.Forms.Padding(2)
            Me.tabControl1.Name = "tabControl1"
            Me.tabControl1.SelectedIndex = 0
            Me.tabControl1.Size = New System.Drawing.Size(993, 526)
            Me.tabControl1.TabIndex = 0
            '
            'tabPage1
            '
            Me.tabPage1.Controls.Add(Me.groupBox10)
            Me.tabPage1.Controls.Add(Me.groupbox)
            Me.tabPage1.Controls.Add(Me.groupBox5)
            Me.tabPage1.Controls.Add(Me.groupBox1)
            Me.tabPage1.Controls.Add(Me.label28)
            Me.tabPage1.Controls.Add(Me.label27)
            Me.tabPage1.Controls.Add(Me.label5)
            Me.tabPage1.Controls.Add(Me.label4)
            Me.tabPage1.Controls.Add(Me.txtVKNAlici)
            Me.tabPage1.Controls.Add(Me.txtGondericiVKN)
            Me.tabPage1.Controls.Add(Me.txtDespatchUUID)
            Me.tabPage1.Controls.Add(Me.txtDespatchID)
            Me.tabPage1.Controls.Add(Me.btnCreateDespatchFromDespatchInfoXml)
            Me.tabPage1.Controls.Add(Me.btnPreviewDespatch)
            Me.tabPage1.Controls.Add(Me.btnCreateDespatchFromDespatchXML)
            Me.tabPage1.Controls.Add(Me.btnSaveDespatchXml)
            Me.tabPage1.Location = New System.Drawing.Point(4, 22)
            Me.tabPage1.Margin = New System.Windows.Forms.Padding(2)
            Me.tabPage1.Name = "tabPage1"
            Me.tabPage1.Padding = New System.Windows.Forms.Padding(2)
            Me.tabPage1.Size = New System.Drawing.Size(985, 500)
            Me.tabPage1.TabIndex = 0
            Me.tabPage1.Text = "İrsaliye Gönder"
            Me.tabPage1.UseVisualStyleBackColor = True
            '
            'groupBox10
            '
            Me.groupBox10.BackColor = System.Drawing.SystemColors.Info
            Me.groupBox10.Controls.Add(Me.groupBox13)
            Me.groupBox10.Controls.Add(Me.groupBox12)
            Me.groupBox10.Location = New System.Drawing.Point(457, 87)
            Me.groupBox10.Margin = New System.Windows.Forms.Padding(2)
            Me.groupBox10.Name = "groupBox10"
            Me.groupBox10.Padding = New System.Windows.Forms.Padding(2)
            Me.groupBox10.Size = New System.Drawing.Size(205, 352)
            Me.groupBox10.TabIndex = 24
            Me.groupBox10.TabStop = False
            Me.groupBox10.Text = "İrsaliye Yanıtı Gönderme/Alma"
            '
            'groupBox13
            '
            Me.groupBox13.BackColor = System.Drawing.Color.AntiqueWhite
            Me.groupBox13.Controls.Add(Me.SetDespatchReceiptAdvicesTaken)
            Me.groupBox13.Controls.Add(Me.btnGetInboxReceiptAdvices)
            Me.groupBox13.Location = New System.Drawing.Point(14, 89)
            Me.groupBox13.Margin = New System.Windows.Forms.Padding(2)
            Me.groupBox13.Name = "groupBox13"
            Me.groupBox13.Padding = New System.Windows.Forms.Padding(2)
            Me.groupBox13.Size = New System.Drawing.Size(178, 111)
            Me.groupBox13.TabIndex = 25
            Me.groupBox13.TabStop = False
            '
            'SetDespatchReceiptAdvicesTaken
            '
            Me.SetDespatchReceiptAdvicesTaken.Location = New System.Drawing.Point(19, 53)
            Me.SetDespatchReceiptAdvicesTaken.Margin = New System.Windows.Forms.Padding(2)
            Me.SetDespatchReceiptAdvicesTaken.Name = "SetDespatchReceiptAdvicesTaken"
            Me.SetDespatchReceiptAdvicesTaken.Size = New System.Drawing.Size(139, 41)
            Me.SetDespatchReceiptAdvicesTaken.TabIndex = 1
            Me.SetDespatchReceiptAdvicesTaken.Text = "İrs. Yanıtı Alındı İşareti Kaldır"
            Me.SetDespatchReceiptAdvicesTaken.UseVisualStyleBackColor = True
            '
            'btnGetInboxReceiptAdvices
            '
            Me.btnGetInboxReceiptAdvices.Location = New System.Drawing.Point(19, 9)
            Me.btnGetInboxReceiptAdvices.Margin = New System.Windows.Forms.Padding(2)
            Me.btnGetInboxReceiptAdvices.Name = "btnGetInboxReceiptAdvices"
            Me.btnGetInboxReceiptAdvices.Size = New System.Drawing.Size(139, 39)
            Me.btnGetInboxReceiptAdvices.TabIndex = 0
            Me.btnGetInboxReceiptAdvices.Text = "Gelen Yeni İrsaliye Yanıtlarını al"
            Me.btnGetInboxReceiptAdvices.UseVisualStyleBackColor = True
            '
            'groupBox12
            '
            Me.groupBox12.BackColor = System.Drawing.Color.AntiqueWhite
            Me.groupBox12.Controls.Add(Me.btnInboxReponseStatus)
            Me.groupBox12.Controls.Add(Me.btnSendDespatchResponse)
            Me.groupBox12.Location = New System.Drawing.Point(14, 17)
            Me.groupBox12.Margin = New System.Windows.Forms.Padding(2)
            Me.groupBox12.Name = "groupBox12"
            Me.groupBox12.Padding = New System.Windows.Forms.Padding(2)
            Me.groupBox12.Size = New System.Drawing.Size(178, 63)
            Me.groupBox12.TabIndex = 24
            Me.groupBox12.TabStop = False
            '
            'btnInboxReponseStatus
            '
            Me.btnInboxReponseStatus.Location = New System.Drawing.Point(19, 37)
            Me.btnInboxReponseStatus.Margin = New System.Windows.Forms.Padding(2)
            Me.btnInboxReponseStatus.Name = "btnInboxReponseStatus"
            Me.btnInboxReponseStatus.Size = New System.Drawing.Size(139, 19)
            Me.btnInboxReponseStatus.TabIndex = 23
            Me.btnInboxReponseStatus.Text = "İrs.Yant Durm Sorgula"
            Me.btnInboxReponseStatus.UseVisualStyleBackColor = True
            '
            'btnSendDespatchResponse
            '
            Me.btnSendDespatchResponse.Location = New System.Drawing.Point(19, 13)
            Me.btnSendDespatchResponse.Margin = New System.Windows.Forms.Padding(2)
            Me.btnSendDespatchResponse.Name = "btnSendDespatchResponse"
            Me.btnSendDespatchResponse.Size = New System.Drawing.Size(139, 19)
            Me.btnSendDespatchResponse.TabIndex = 22
            Me.btnSendDespatchResponse.Text = "İrsaliye Yanıtı Gönder"
            Me.btnSendDespatchResponse.UseVisualStyleBackColor = True
            '
            'groupbox
            '
            Me.groupbox.BackColor = System.Drawing.SystemColors.Info
            Me.groupbox.Controls.Add(Me.groupBox9)
            Me.groupbox.Controls.Add(Me.groupBox11)
            Me.groupbox.Location = New System.Drawing.Point(248, 86)
            Me.groupbox.Margin = New System.Windows.Forms.Padding(2)
            Me.groupbox.Name = "groupbox"
            Me.groupbox.Padding = New System.Windows.Forms.Padding(2)
            Me.groupbox.Size = New System.Drawing.Size(205, 353)
            Me.groupbox.TabIndex = 21
            Me.groupbox.TabStop = False
            Me.groupbox.Text = "İrsaliye ve Görüntü Dosyalarını Alma"
            '
            'groupBox9
            '
            Me.groupBox9.BackColor = System.Drawing.Color.AntiqueWhite
            Me.groupBox9.Controls.Add(Me.label18)
            Me.groupBox9.Controls.Add(Me.label21)
            Me.groupBox9.Controls.Add(Me.label20)
            Me.groupBox9.Controls.Add(Me.label16)
            Me.groupBox9.Controls.Add(Me.label17)
            Me.groupBox9.Controls.Add(Me.label12)
            Me.groupBox9.Controls.Add(Me.btnGetInboxDespatch)
            Me.groupBox9.Controls.Add(Me.btnGetInboxDespatches)
            Me.groupBox9.Controls.Add(Me.btnGetInboxDespatchList)
            Me.groupBox9.Controls.Add(Me.btnGetInboxDespatchData)
            Me.groupBox9.Controls.Add(Me.btnGetInboxDespatchPdf)
            Me.groupBox9.Controls.Add(Me.GetInboxDespatchView)
            Me.groupBox9.Location = New System.Drawing.Point(14, 19)
            Me.groupBox9.Margin = New System.Windows.Forms.Padding(2)
            Me.groupBox9.Name = "groupBox9"
            Me.groupBox9.Padding = New System.Windows.Forms.Padding(2)
            Me.groupBox9.Size = New System.Drawing.Size(178, 165)
            Me.groupBox9.TabIndex = 19
            Me.groupBox9.TabStop = False
            Me.groupBox9.Text = "Gelen İrsaliyeler"
            '
            'label18
            '
            Me.label18.AutoSize = True
            Me.label18.Location = New System.Drawing.Point(-1, 137)
            Me.label18.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label18.Name = "label18"
            Me.label18.Size = New System.Drawing.Size(19, 13)
            Me.label18.TabIndex = 17
            Me.label18.Text = "14"
            '
            'label21
            '
            Me.label21.AutoSize = True
            Me.label21.Location = New System.Drawing.Point(-1, 116)
            Me.label21.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label21.Name = "label21"
            Me.label21.Size = New System.Drawing.Size(19, 13)
            Me.label21.TabIndex = 30
            Me.label21.Text = "13"
            '
            'label20
            '
            Me.label20.AutoSize = True
            Me.label20.Location = New System.Drawing.Point(-1, 94)
            Me.label20.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label20.Name = "label20"
            Me.label20.Size = New System.Drawing.Size(19, 13)
            Me.label20.TabIndex = 30
            Me.label20.Text = "12"
            '
            'label16
            '
            Me.label16.AutoSize = True
            Me.label16.Location = New System.Drawing.Point(-1, 68)
            Me.label16.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label16.Name = "label16"
            Me.label16.Size = New System.Drawing.Size(19, 13)
            Me.label16.TabIndex = 29
            Me.label16.Text = "11"
            '
            'label17
            '
            Me.label17.AutoSize = True
            Me.label17.Location = New System.Drawing.Point(-1, 46)
            Me.label17.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label17.Name = "label17"
            Me.label17.Size = New System.Drawing.Size(19, 13)
            Me.label17.TabIndex = 18
            Me.label17.Text = "10"
            '
            'label12
            '
            Me.label12.AutoSize = True
            Me.label12.Location = New System.Drawing.Point(4, 23)
            Me.label12.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label12.Name = "label12"
            Me.label12.Size = New System.Drawing.Size(13, 13)
            Me.label12.TabIndex = 10
            Me.label12.Text = "9"
            '
            'btnGetInboxDespatch
            '
            Me.btnGetInboxDespatch.Location = New System.Drawing.Point(18, 21)
            Me.btnGetInboxDespatch.Margin = New System.Windows.Forms.Padding(2)
            Me.btnGetInboxDespatch.Name = "btnGetInboxDespatch"
            Me.btnGetInboxDespatch.Size = New System.Drawing.Size(152, 19)
            Me.btnGetInboxDespatch.TabIndex = 22
            Me.btnGetInboxDespatch.Text = "GetInboxDespatch"
            Me.btnGetInboxDespatch.UseVisualStyleBackColor = True
            '
            'btnGetInboxDespatches
            '
            Me.btnGetInboxDespatches.Location = New System.Drawing.Point(18, 43)
            Me.btnGetInboxDespatches.Margin = New System.Windows.Forms.Padding(2)
            Me.btnGetInboxDespatches.Name = "btnGetInboxDespatches"
            Me.btnGetInboxDespatches.Size = New System.Drawing.Size(152, 19)
            Me.btnGetInboxDespatches.TabIndex = 24
            Me.btnGetInboxDespatches.Text = "GetInboxDespatches"
            Me.btnGetInboxDespatches.UseVisualStyleBackColor = True
            '
            'btnGetInboxDespatchList
            '
            Me.btnGetInboxDespatchList.Location = New System.Drawing.Point(18, 67)
            Me.btnGetInboxDespatchList.Margin = New System.Windows.Forms.Padding(2)
            Me.btnGetInboxDespatchList.Name = "btnGetInboxDespatchList"
            Me.btnGetInboxDespatchList.Size = New System.Drawing.Size(152, 19)
            Me.btnGetInboxDespatchList.TabIndex = 25
            Me.btnGetInboxDespatchList.Text = "GetInboxDespatchList"
            Me.btnGetInboxDespatchList.UseVisualStyleBackColor = True
            '
            'btnGetInboxDespatchData
            '
            Me.btnGetInboxDespatchData.Location = New System.Drawing.Point(18, 137)
            Me.btnGetInboxDespatchData.Margin = New System.Windows.Forms.Padding(2)
            Me.btnGetInboxDespatchData.Name = "btnGetInboxDespatchData"
            Me.btnGetInboxDespatchData.Size = New System.Drawing.Size(152, 19)
            Me.btnGetInboxDespatchData.TabIndex = 28
            Me.btnGetInboxDespatchData.Text = "GetInboxDespatchData"
            Me.btnGetInboxDespatchData.UseVisualStyleBackColor = True
            '
            'btnGetInboxDespatchPdf
            '
            Me.btnGetInboxDespatchPdf.Location = New System.Drawing.Point(18, 90)
            Me.btnGetInboxDespatchPdf.Margin = New System.Windows.Forms.Padding(2)
            Me.btnGetInboxDespatchPdf.Name = "btnGetInboxDespatchPdf"
            Me.btnGetInboxDespatchPdf.Size = New System.Drawing.Size(152, 19)
            Me.btnGetInboxDespatchPdf.TabIndex = 27
            Me.btnGetInboxDespatchPdf.Text = "GetInboxDespatchPdf"
            Me.btnGetInboxDespatchPdf.UseVisualStyleBackColor = True
            '
            'GetInboxDespatchView
            '
            Me.GetInboxDespatchView.Location = New System.Drawing.Point(18, 114)
            Me.GetInboxDespatchView.Margin = New System.Windows.Forms.Padding(2)
            Me.GetInboxDespatchView.Name = "GetInboxDespatchView"
            Me.GetInboxDespatchView.Size = New System.Drawing.Size(152, 19)
            Me.GetInboxDespatchView.TabIndex = 28
            Me.GetInboxDespatchView.Text = "GetInboxDespatchView"
            Me.GetInboxDespatchView.UseVisualStyleBackColor = True
            '
            'groupBox11
            '
            Me.groupBox11.BackColor = System.Drawing.Color.AntiqueWhite
            Me.groupBox11.Controls.Add(Me.btnGetOutboxDespatchData)
            Me.groupBox11.Controls.Add(Me.label26)
            Me.groupBox11.Controls.Add(Me.label25)
            Me.groupBox11.Controls.Add(Me.label24)
            Me.groupBox11.Controls.Add(Me.label23)
            Me.groupBox11.Controls.Add(Me.label22)
            Me.groupBox11.Controls.Add(Me.label19)
            Me.groupBox11.Controls.Add(Me.btnGetOutboxDespatchView)
            Me.groupBox11.Controls.Add(Me.btnGetOutboxDespatchList)
            Me.groupBox11.Controls.Add(Me.btnGetOutboxDespatchPdf)
            Me.groupBox11.Controls.Add(Me.btnGetOutboxDespatch)
            Me.groupBox11.Controls.Add(Me.btnGetOutboxDespatches)
            Me.groupBox11.Location = New System.Drawing.Point(14, 188)
            Me.groupBox11.Margin = New System.Windows.Forms.Padding(2)
            Me.groupBox11.Name = "groupBox11"
            Me.groupBox11.Padding = New System.Windows.Forms.Padding(2)
            Me.groupBox11.Size = New System.Drawing.Size(178, 158)
            Me.groupBox11.TabIndex = 18
            Me.groupBox11.TabStop = False
            Me.groupBox11.Text = "Giden İrsaliyeler"
            '
            'btnGetOutboxDespatchData
            '
            Me.btnGetOutboxDespatchData.Location = New System.Drawing.Point(18, 132)
            Me.btnGetOutboxDespatchData.Margin = New System.Windows.Forms.Padding(2)
            Me.btnGetOutboxDespatchData.Name = "btnGetOutboxDespatchData"
            Me.btnGetOutboxDespatchData.Size = New System.Drawing.Size(152, 19)
            Me.btnGetOutboxDespatchData.TabIndex = 28
            Me.btnGetOutboxDespatchData.Text = "GetOutboxDespatchData"
            Me.btnGetOutboxDespatchData.UseVisualStyleBackColor = True
            '
            'label26
            '
            Me.label26.AutoSize = True
            Me.label26.Location = New System.Drawing.Point(-1, 134)
            Me.label26.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label26.Name = "label26"
            Me.label26.Size = New System.Drawing.Size(19, 13)
            Me.label26.TabIndex = 13
            Me.label26.Text = "20"
            '
            'label25
            '
            Me.label25.AutoSize = True
            Me.label25.Location = New System.Drawing.Point(-1, 110)
            Me.label25.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label25.Name = "label25"
            Me.label25.Size = New System.Drawing.Size(19, 13)
            Me.label25.TabIndex = 13
            Me.label25.Text = "19"
            '
            'label24
            '
            Me.label24.AutoSize = True
            Me.label24.Location = New System.Drawing.Point(-1, 84)
            Me.label24.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label24.Name = "label24"
            Me.label24.Size = New System.Drawing.Size(19, 13)
            Me.label24.TabIndex = 13
            Me.label24.Text = "18"
            '
            'label23
            '
            Me.label23.AutoSize = True
            Me.label23.Location = New System.Drawing.Point(-1, 61)
            Me.label23.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label23.Name = "label23"
            Me.label23.Size = New System.Drawing.Size(19, 13)
            Me.label23.TabIndex = 13
            Me.label23.Text = "17"
            '
            'label22
            '
            Me.label22.AutoSize = True
            Me.label22.Location = New System.Drawing.Point(-1, 40)
            Me.label22.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label22.Name = "label22"
            Me.label22.Size = New System.Drawing.Size(19, 13)
            Me.label22.TabIndex = 13
            Me.label22.Text = "16"
            '
            'label19
            '
            Me.label19.AutoSize = True
            Me.label19.Location = New System.Drawing.Point(-1, 22)
            Me.label19.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label19.Name = "label19"
            Me.label19.Size = New System.Drawing.Size(19, 13)
            Me.label19.TabIndex = 13
            Me.label19.Text = "15"
            '
            'btnGetOutboxDespatchView
            '
            Me.btnGetOutboxDespatchView.Location = New System.Drawing.Point(18, 108)
            Me.btnGetOutboxDespatchView.Margin = New System.Windows.Forms.Padding(2)
            Me.btnGetOutboxDespatchView.Name = "btnGetOutboxDespatchView"
            Me.btnGetOutboxDespatchView.Size = New System.Drawing.Size(152, 19)
            Me.btnGetOutboxDespatchView.TabIndex = 28
            Me.btnGetOutboxDespatchView.Text = "GetOutboxDespatchView"
            Me.btnGetOutboxDespatchView.UseVisualStyleBackColor = True
            '
            'btnGetOutboxDespatchList
            '
            Me.btnGetOutboxDespatchList.Location = New System.Drawing.Point(18, 61)
            Me.btnGetOutboxDespatchList.Margin = New System.Windows.Forms.Padding(2)
            Me.btnGetOutboxDespatchList.Name = "btnGetOutboxDespatchList"
            Me.btnGetOutboxDespatchList.Size = New System.Drawing.Size(152, 19)
            Me.btnGetOutboxDespatchList.TabIndex = 24
            Me.btnGetOutboxDespatchList.Text = "GetOutboxDespatchList"
            Me.btnGetOutboxDespatchList.UseVisualStyleBackColor = True
            '
            'btnGetOutboxDespatchPdf
            '
            Me.btnGetOutboxDespatchPdf.Location = New System.Drawing.Point(18, 84)
            Me.btnGetOutboxDespatchPdf.Margin = New System.Windows.Forms.Padding(2)
            Me.btnGetOutboxDespatchPdf.Name = "btnGetOutboxDespatchPdf"
            Me.btnGetOutboxDespatchPdf.Size = New System.Drawing.Size(152, 19)
            Me.btnGetOutboxDespatchPdf.TabIndex = 27
            Me.btnGetOutboxDespatchPdf.Text = "GetOutboxDespatchPdf"
            Me.btnGetOutboxDespatchPdf.UseVisualStyleBackColor = True
            '
            'btnGetOutboxDespatch
            '
            Me.btnGetOutboxDespatch.Location = New System.Drawing.Point(18, 19)
            Me.btnGetOutboxDespatch.Margin = New System.Windows.Forms.Padding(2)
            Me.btnGetOutboxDespatch.Name = "btnGetOutboxDespatch"
            Me.btnGetOutboxDespatch.Size = New System.Drawing.Size(152, 19)
            Me.btnGetOutboxDespatch.TabIndex = 23
            Me.btnGetOutboxDespatch.Text = "GetOutboxDespatch"
            Me.btnGetOutboxDespatch.UseVisualStyleBackColor = True
            '
            'btnGetOutboxDespatches
            '
            Me.btnGetOutboxDespatches.Location = New System.Drawing.Point(18, 39)
            Me.btnGetOutboxDespatches.Margin = New System.Windows.Forms.Padding(2)
            Me.btnGetOutboxDespatches.Name = "btnGetOutboxDespatches"
            Me.btnGetOutboxDespatches.Size = New System.Drawing.Size(152, 19)
            Me.btnGetOutboxDespatches.TabIndex = 26
            Me.btnGetOutboxDespatches.Text = "GetOutboxDespatches"
            Me.btnGetOutboxDespatches.UseVisualStyleBackColor = True
            '
            'groupBox5
            '
            Me.groupBox5.BackColor = System.Drawing.SystemColors.Info
            Me.groupBox5.Controls.Add(Me.groupBox6)
            Me.groupBox5.Controls.Add(Me.groupBox8)
            Me.groupBox5.Location = New System.Drawing.Point(14, 314)
            Me.groupBox5.Margin = New System.Windows.Forms.Padding(2)
            Me.groupBox5.Name = "groupBox5"
            Me.groupBox5.Padding = New System.Windows.Forms.Padding(2)
            Me.groupBox5.Size = New System.Drawing.Size(230, 171)
            Me.groupBox5.TabIndex = 21
            Me.groupBox5.TabStop = False
            Me.groupBox5.Text = "İrsaliye Durum Sorgulama"
            '
            'groupBox6
            '
            Me.groupBox6.BackColor = System.Drawing.Color.AntiqueWhite
            Me.groupBox6.Controls.Add(Me.btnQueryInboxDespatchStatus)
            Me.groupBox6.Controls.Add(Me.label15)
            Me.groupBox6.Controls.Add(Me.label11)
            Me.groupBox6.Controls.Add(Me.btnQueryOutboxDespatchStatus)
            Me.groupBox6.Location = New System.Drawing.Point(10, 17)
            Me.groupBox6.Margin = New System.Windows.Forms.Padding(2)
            Me.groupBox6.Name = "groupBox6"
            Me.groupBox6.Padding = New System.Windows.Forms.Padding(2)
            Me.groupBox6.Size = New System.Drawing.Size(212, 66)
            Me.groupBox6.TabIndex = 19
            Me.groupBox6.TabStop = False
            Me.groupBox6.Text = "Durum Kodu Sorgulama"
            '
            'btnQueryInboxDespatchStatus
            '
            Me.btnQueryInboxDespatchStatus.Location = New System.Drawing.Point(18, 41)
            Me.btnQueryInboxDespatchStatus.Margin = New System.Windows.Forms.Padding(2)
            Me.btnQueryInboxDespatchStatus.Name = "btnQueryInboxDespatchStatus"
            Me.btnQueryInboxDespatchStatus.Size = New System.Drawing.Size(184, 21)
            Me.btnQueryInboxDespatchStatus.TabIndex = 20
            Me.btnQueryInboxDespatchStatus.Text = "QueryInboxDespatchStatus"
            Me.btnQueryInboxDespatchStatus.UseVisualStyleBackColor = True
            '
            'label15
            '
            Me.label15.AutoSize = True
            Me.label15.Location = New System.Drawing.Point(2, 43)
            Me.label15.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label15.Name = "label15"
            Me.label15.Size = New System.Drawing.Size(13, 13)
            Me.label15.TabIndex = 19
            Me.label15.Text = "7"
            '
            'label11
            '
            Me.label11.AutoSize = True
            Me.label11.Location = New System.Drawing.Point(2, 19)
            Me.label11.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label11.Name = "label11"
            Me.label11.Size = New System.Drawing.Size(13, 13)
            Me.label11.TabIndex = 10
            Me.label11.Text = "6"
            '
            'btnQueryOutboxDespatchStatus
            '
            Me.btnQueryOutboxDespatchStatus.Location = New System.Drawing.Point(18, 15)
            Me.btnQueryOutboxDespatchStatus.Margin = New System.Windows.Forms.Padding(2)
            Me.btnQueryOutboxDespatchStatus.Name = "btnQueryOutboxDespatchStatus"
            Me.btnQueryOutboxDespatchStatus.Size = New System.Drawing.Size(184, 21)
            Me.btnQueryOutboxDespatchStatus.TabIndex = 7
            Me.btnQueryOutboxDespatchStatus.Text = "QueryOutboxDespatchStatus"
            Me.btnQueryOutboxDespatchStatus.UseVisualStyleBackColor = True
            '
            'groupBox8
            '
            Me.groupBox8.BackColor = System.Drawing.Color.AntiqueWhite
            Me.groupBox8.Controls.Add(Me.label14)
            Me.groupBox8.Controls.Add(Me.btnGetInboxDespatchStatusWithLogs)
            Me.groupBox8.Controls.Add(Me.label13)
            Me.groupBox8.Controls.Add(Me.btnGetOutboxDespatchStatusWithLogs)
            Me.groupBox8.Location = New System.Drawing.Point(10, 88)
            Me.groupBox8.Margin = New System.Windows.Forms.Padding(2)
            Me.groupBox8.Name = "groupBox8"
            Me.groupBox8.Padding = New System.Windows.Forms.Padding(2)
            Me.groupBox8.Size = New System.Drawing.Size(212, 75)
            Me.groupBox8.TabIndex = 18
            Me.groupBox8.TabStop = False
            Me.groupBox8.Text = "Loglar ve Durum Kodu"
            '
            'label14
            '
            Me.label14.AutoSize = True
            Me.label14.Location = New System.Drawing.Point(2, 43)
            Me.label14.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label14.Name = "label14"
            Me.label14.Size = New System.Drawing.Size(13, 13)
            Me.label14.TabIndex = 20
            Me.label14.Text = "8"
            '
            'btnGetInboxDespatchStatusWithLogs
            '
            Me.btnGetInboxDespatchStatusWithLogs.Location = New System.Drawing.Point(18, 41)
            Me.btnGetInboxDespatchStatusWithLogs.Margin = New System.Windows.Forms.Padding(2)
            Me.btnGetInboxDespatchStatusWithLogs.Name = "btnGetInboxDespatchStatusWithLogs"
            Me.btnGetInboxDespatchStatusWithLogs.Size = New System.Drawing.Size(184, 21)
            Me.btnGetInboxDespatchStatusWithLogs.TabIndex = 19
            Me.btnGetInboxDespatchStatusWithLogs.Text = "GetInboxDespatchStatusWithLogs"
            Me.btnGetInboxDespatchStatusWithLogs.UseVisualStyleBackColor = True
            '
            'label13
            '
            Me.label13.AutoSize = True
            Me.label13.Location = New System.Drawing.Point(2, 20)
            Me.label13.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label13.Name = "label13"
            Me.label13.Size = New System.Drawing.Size(13, 13)
            Me.label13.TabIndex = 18
            Me.label13.Text = "7"
            '
            'btnGetOutboxDespatchStatusWithLogs
            '
            Me.btnGetOutboxDespatchStatusWithLogs.Location = New System.Drawing.Point(18, 15)
            Me.btnGetOutboxDespatchStatusWithLogs.Margin = New System.Windows.Forms.Padding(2)
            Me.btnGetOutboxDespatchStatusWithLogs.Name = "btnGetOutboxDespatchStatusWithLogs"
            Me.btnGetOutboxDespatchStatusWithLogs.Size = New System.Drawing.Size(184, 21)
            Me.btnGetOutboxDespatchStatusWithLogs.TabIndex = 9
            Me.btnGetOutboxDespatchStatusWithLogs.Text = "GetOutboxDespatchStatusWithLogs"
            Me.btnGetOutboxDespatchStatusWithLogs.UseVisualStyleBackColor = True
            '
            'groupBox1
            '
            Me.groupBox1.BackColor = System.Drawing.SystemColors.Info
            Me.groupBox1.Controls.Add(Me.groupBox3)
            Me.groupBox1.Controls.Add(Me.groupBox4)
            Me.groupBox1.Controls.Add(Me.groupBox2)
            Me.groupBox1.Location = New System.Drawing.Point(15, 84)
            Me.groupBox1.Margin = New System.Windows.Forms.Padding(2)
            Me.groupBox1.Name = "groupBox1"
            Me.groupBox1.Padding = New System.Windows.Forms.Padding(2)
            Me.groupBox1.Size = New System.Drawing.Size(230, 228)
            Me.groupBox1.TabIndex = 17
            Me.groupBox1.TabStop = False
            Me.groupBox1.Text = "İrsaliye Gönderme"
            '
            'groupBox3
            '
            Me.groupBox3.BackColor = System.Drawing.Color.AntiqueWhite
            Me.groupBox3.Controls.Add(Me.btnSendDespatch)
            Me.groupBox3.Controls.Add(Me.label6)
            Me.groupBox3.Location = New System.Drawing.Point(10, 17)
            Me.groupBox3.Margin = New System.Windows.Forms.Padding(2)
            Me.groupBox3.Name = "groupBox3"
            Me.groupBox3.Padding = New System.Windows.Forms.Padding(2)
            Me.groupBox3.Size = New System.Drawing.Size(212, 50)
            Me.groupBox3.TabIndex = 19
            Me.groupBox3.TabStop = False
            Me.groupBox3.Text = "Direkt GİB'e gönderme"
            '
            'btnSendDespatch
            '
            Me.btnSendDespatch.Location = New System.Drawing.Point(15, 19)
            Me.btnSendDespatch.Margin = New System.Windows.Forms.Padding(2)
            Me.btnSendDespatch.Name = "btnSendDespatch"
            Me.btnSendDespatch.Size = New System.Drawing.Size(184, 20)
            Me.btnSendDespatch.TabIndex = 0
            Me.btnSendDespatch.Text = "SendDespatch"
            Me.btnSendDespatch.UseVisualStyleBackColor = True
            '
            'label6
            '
            Me.label6.AutoSize = True
            Me.label6.Location = New System.Drawing.Point(2, 21)
            Me.label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label6.Name = "label6"
            Me.label6.Size = New System.Drawing.Size(13, 13)
            Me.label6.TabIndex = 10
            Me.label6.Text = "1"
            '
            'groupBox4
            '
            Me.groupBox4.BackColor = System.Drawing.Color.AntiqueWhite
            Me.groupBox4.Controls.Add(Me.btnCompressedSendDespatch)
            Me.groupBox4.Controls.Add(Me.label10)
            Me.groupBox4.Location = New System.Drawing.Point(10, 167)
            Me.groupBox4.Margin = New System.Windows.Forms.Padding(2)
            Me.groupBox4.Name = "groupBox4"
            Me.groupBox4.Padding = New System.Windows.Forms.Padding(2)
            Me.groupBox4.Size = New System.Drawing.Size(212, 50)
            Me.groupBox4.TabIndex = 20
            Me.groupBox4.TabStop = False
            Me.groupBox4.Text = "Sıkıştırılmış Zip gönderme"
            '
            'btnCompressedSendDespatch
            '
            Me.btnCompressedSendDespatch.Enabled = False
            Me.btnCompressedSendDespatch.Location = New System.Drawing.Point(15, 19)
            Me.btnCompressedSendDespatch.Margin = New System.Windows.Forms.Padding(2)
            Me.btnCompressedSendDespatch.Name = "btnCompressedSendDespatch"
            Me.btnCompressedSendDespatch.Size = New System.Drawing.Size(184, 20)
            Me.btnCompressedSendDespatch.TabIndex = 0
            Me.btnCompressedSendDespatch.Text = "CompressedSendDespatch"
            Me.btnCompressedSendDespatch.UseVisualStyleBackColor = True
            '
            'label10
            '
            Me.label10.AutoSize = True
            Me.label10.Location = New System.Drawing.Point(2, 21)
            Me.label10.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label10.Name = "label10"
            Me.label10.Size = New System.Drawing.Size(13, 13)
            Me.label10.TabIndex = 10
            Me.label10.Text = "5"
            '
            'groupBox2
            '
            Me.groupBox2.BackColor = System.Drawing.Color.AntiqueWhite
            Me.groupBox2.Controls.Add(Me.label8)
            Me.groupBox2.Controls.Add(Me.label7)
            Me.groupBox2.Controls.Add(Me.label9)
            Me.groupBox2.Controls.Add(Me.btnSaveDraftDespatch)
            Me.groupBox2.Controls.Add(Me.btnSendDraft)
            Me.groupBox2.Controls.Add(Me.btnCancelDraft)
            Me.groupBox2.Location = New System.Drawing.Point(10, 72)
            Me.groupBox2.Margin = New System.Windows.Forms.Padding(2)
            Me.groupBox2.Name = "groupBox2"
            Me.groupBox2.Padding = New System.Windows.Forms.Padding(2)
            Me.groupBox2.Size = New System.Drawing.Size(212, 91)
            Me.groupBox2.TabIndex = 18
            Me.groupBox2.TabStop = False
            Me.groupBox2.Text = "Taslaklı Çalışma"
            '
            'label8
            '
            Me.label8.AutoSize = True
            Me.label8.Location = New System.Drawing.Point(2, 20)
            Me.label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label8.Name = "label8"
            Me.label8.Size = New System.Drawing.Size(13, 13)
            Me.label8.TabIndex = 18
            Me.label8.Text = "2"
            '
            'label7
            '
            Me.label7.AutoSize = True
            Me.label7.Location = New System.Drawing.Point(2, 42)
            Me.label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label7.Name = "label7"
            Me.label7.Size = New System.Drawing.Size(13, 13)
            Me.label7.TabIndex = 17
            Me.label7.Text = "3"
            '
            'label9
            '
            Me.label9.AutoSize = True
            Me.label9.Location = New System.Drawing.Point(2, 64)
            Me.label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label9.Name = "label9"
            Me.label9.Size = New System.Drawing.Size(13, 13)
            Me.label9.TabIndex = 13
            Me.label9.Text = "4"
            '
            'btnSaveDraftDespatch
            '
            Me.btnSaveDraftDespatch.Location = New System.Drawing.Point(15, 16)
            Me.btnSaveDraftDespatch.Margin = New System.Windows.Forms.Padding(2)
            Me.btnSaveDraftDespatch.Name = "btnSaveDraftDespatch"
            Me.btnSaveDraftDespatch.Size = New System.Drawing.Size(184, 20)
            Me.btnSaveDraftDespatch.TabIndex = 14
            Me.btnSaveDraftDespatch.Text = "SaveDraftDespatch"
            Me.btnSaveDraftDespatch.UseVisualStyleBackColor = True
            '
            'btnSendDraft
            '
            Me.btnSendDraft.Location = New System.Drawing.Point(15, 39)
            Me.btnSendDraft.Margin = New System.Windows.Forms.Padding(2)
            Me.btnSendDraft.Name = "btnSendDraft"
            Me.btnSendDraft.Size = New System.Drawing.Size(184, 20)
            Me.btnSendDraft.TabIndex = 15
            Me.btnSendDraft.Text = "SendDraft"
            Me.btnSendDraft.UseVisualStyleBackColor = True
            '
            'btnCancelDraft
            '
            Me.btnCancelDraft.Location = New System.Drawing.Point(15, 61)
            Me.btnCancelDraft.Margin = New System.Windows.Forms.Padding(2)
            Me.btnCancelDraft.Name = "btnCancelDraft"
            Me.btnCancelDraft.Size = New System.Drawing.Size(184, 20)
            Me.btnCancelDraft.TabIndex = 16
            Me.btnCancelDraft.Text = "CancelDraft"
            Me.btnCancelDraft.UseVisualStyleBackColor = True
            '
            'label28
            '
            Me.label28.AutoSize = True
            Me.label28.Location = New System.Drawing.Point(13, 56)
            Me.label28.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label28.Name = "label28"
            Me.label28.Size = New System.Drawing.Size(51, 13)
            Me.label28.TabIndex = 6
            Me.label28.Text = "Alici VKN"
            '
            'label27
            '
            Me.label27.AutoSize = True
            Me.label27.Location = New System.Drawing.Point(13, 35)
            Me.label27.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label27.Name = "label27"
            Me.label27.Size = New System.Drawing.Size(77, 13)
            Me.label27.TabIndex = 6
            Me.label27.Text = "Gönderici VKN"
            '
            'label5
            '
            Me.label5.AutoSize = True
            Me.label5.Location = New System.Drawing.Point(13, 7)
            Me.label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label5.Name = "label5"
            Me.label5.Size = New System.Drawing.Size(69, 13)
            Me.label5.TabIndex = 6
            Me.label5.Text = "İrsaliye UUID"
            '
            'label4
            '
            Me.label4.AutoSize = True
            Me.label4.Location = New System.Drawing.Point(352, 6)
            Me.label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label4.Name = "label4"
            Me.label4.Size = New System.Drawing.Size(56, 13)
            Me.label4.TabIndex = 6
            Me.label4.Text = "İrsaliye No"
            '
            'txtVKNAlici
            '
            Me.txtVKNAlici.Location = New System.Drawing.Point(112, 56)
            Me.txtVKNAlici.Margin = New System.Windows.Forms.Padding(2)
            Me.txtVKNAlici.Name = "txtVKNAlici"
            Me.txtVKNAlici.Size = New System.Drawing.Size(136, 20)
            Me.txtVKNAlici.TabIndex = 5
            '
            'txtGondericiVKN
            '
            Me.txtGondericiVKN.Location = New System.Drawing.Point(112, 35)
            Me.txtGondericiVKN.Margin = New System.Windows.Forms.Padding(2)
            Me.txtGondericiVKN.Name = "txtGondericiVKN"
            Me.txtGondericiVKN.Size = New System.Drawing.Size(136, 20)
            Me.txtGondericiVKN.TabIndex = 5
            '
            'txtDespatchUUID
            '
            Me.txtDespatchUUID.Location = New System.Drawing.Point(112, 6)
            Me.txtDespatchUUID.Margin = New System.Windows.Forms.Padding(2)
            Me.txtDespatchUUID.Name = "txtDespatchUUID"
            Me.txtDespatchUUID.Size = New System.Drawing.Size(218, 20)
            Me.txtDespatchUUID.TabIndex = 5
            '
            'txtDespatchID
            '
            Me.txtDespatchID.Location = New System.Drawing.Point(412, 5)
            Me.txtDespatchID.Margin = New System.Windows.Forms.Padding(2)
            Me.txtDespatchID.Name = "txtDespatchID"
            Me.txtDespatchID.Size = New System.Drawing.Size(174, 20)
            Me.txtDespatchID.TabIndex = 5
            '
            'btnCreateDespatchFromDespatchInfoXml
            '
            Me.btnCreateDespatchFromDespatchInfoXml.Location = New System.Drawing.Point(854, 11)
            Me.btnCreateDespatchFromDespatchInfoXml.Name = "btnCreateDespatchFromDespatchInfoXml"
            Me.btnCreateDespatchFromDespatchInfoXml.Size = New System.Drawing.Size(104, 37)
            Me.btnCreateDespatchFromDespatchInfoXml.TabIndex = 4
            Me.btnCreateDespatchFromDespatchInfoXml.Text = "Despatch Info'dan İrsaliye"
            Me.btnCreateDespatchFromDespatchInfoXml.UseVisualStyleBackColor = True
            '
            'btnPreviewDespatch
            '
            Me.btnPreviewDespatch.Enabled = False
            Me.btnPreviewDespatch.Location = New System.Drawing.Point(854, 110)
            Me.btnPreviewDespatch.Margin = New System.Windows.Forms.Padding(2)
            Me.btnPreviewDespatch.Name = "btnPreviewDespatch"
            Me.btnPreviewDespatch.Size = New System.Drawing.Size(104, 20)
            Me.btnPreviewDespatch.TabIndex = 3
            Me.btnPreviewDespatch.Text = "İrsaliye Önizleme"
            Me.btnPreviewDespatch.UseVisualStyleBackColor = True
            '
            'btnCreateDespatchFromDespatchXML
            '
            Me.btnCreateDespatchFromDespatchXML.Location = New System.Drawing.Point(854, 56)
            Me.btnCreateDespatchFromDespatchXML.Margin = New System.Windows.Forms.Padding(2)
            Me.btnCreateDespatchFromDespatchXML.Name = "btnCreateDespatchFromDespatchXML"
            Me.btnCreateDespatchFromDespatchXML.Size = New System.Drawing.Size(104, 45)
            Me.btnCreateDespatchFromDespatchXML.TabIndex = 2
            Me.btnCreateDespatchFromDespatchXML.Text = "XML'den İrsaliye Oluştur"
            Me.btnCreateDespatchFromDespatchXML.UseVisualStyleBackColor = True
            '
            'btnSaveDespatchXml
            '
            Me.btnSaveDespatchXml.Location = New System.Drawing.Point(854, 142)
            Me.btnSaveDespatchXml.Margin = New System.Windows.Forms.Padding(2)
            Me.btnSaveDespatchXml.Name = "btnSaveDespatchXml"
            Me.btnSaveDespatchXml.Size = New System.Drawing.Size(104, 37)
            Me.btnSaveDespatchXml.TabIndex = 1
            Me.btnSaveDespatchXml.Text = "İrsaliye XML'i Kaydet"
            Me.btnSaveDespatchXml.UseVisualStyleBackColor = True
            '
            'tabPage2
            '
            Me.tabPage2.Controls.Add(Me.tabControl2)
            Me.tabPage2.Location = New System.Drawing.Point(4, 22)
            Me.tabPage2.Margin = New System.Windows.Forms.Padding(2)
            Me.tabPage2.Name = "tabPage2"
            Me.tabPage2.Padding = New System.Windows.Forms.Padding(2)
            Me.tabPage2.Size = New System.Drawing.Size(985, 500)
            Me.tabPage2.TabIndex = 1
            Me.tabPage2.Text = "Bağlantı Ayarları"
            Me.tabPage2.UseVisualStyleBackColor = True
            '
            'tabControl2
            '
            Me.tabControl2.Controls.Add(Me.tabPage3)
            Me.tabControl2.Controls.Add(Me.tabPage4)
            Me.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tabControl2.Location = New System.Drawing.Point(2, 2)
            Me.tabControl2.Margin = New System.Windows.Forms.Padding(2)
            Me.tabControl2.Name = "tabControl2"
            Me.tabControl2.SelectedIndex = 0
            Me.tabControl2.Size = New System.Drawing.Size(981, 496)
            Me.tabControl2.TabIndex = 4
            '
            'tabPage3
            '
            Me.tabPage3.Controls.Add(Me.label3)
            Me.tabPage3.Controls.Add(Me.label2)
            Me.tabPage3.Controls.Add(Me.label1)
            Me.tabPage3.Controls.Add(Me.txtConnectionTestPassword)
            Me.tabPage3.Controls.Add(Me.txtConnectionTestUserName)
            Me.tabPage3.Controls.Add(Me.txtConnectionTestUri)
            Me.tabPage3.Controls.Add(Me.btnSaveConnectionTestInfo)
            Me.tabPage3.Location = New System.Drawing.Point(4, 22)
            Me.tabPage3.Margin = New System.Windows.Forms.Padding(2)
            Me.tabPage3.Name = "tabPage3"
            Me.tabPage3.Padding = New System.Windows.Forms.Padding(2)
            Me.tabPage3.Size = New System.Drawing.Size(973, 470)
            Me.tabPage3.TabIndex = 0
            Me.tabPage3.Text = "Test Sistemi"
            Me.tabPage3.UseVisualStyleBackColor = True
            '
            'label3
            '
            Me.label3.AutoSize = True
            Me.label3.Location = New System.Drawing.Point(13, 66)
            Me.label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label3.Name = "label3"
            Me.label3.Size = New System.Drawing.Size(28, 13)
            Me.label3.TabIndex = 1
            Me.label3.Text = "Şifre"
            '
            'label2
            '
            Me.label2.AutoSize = True
            Me.label2.Location = New System.Drawing.Point(13, 43)
            Me.label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label2.Name = "label2"
            Me.label2.Size = New System.Drawing.Size(64, 13)
            Me.label2.TabIndex = 1
            Me.label2.Text = "Kullanıcı Adı"
            '
            'label1
            '
            Me.label1.AutoSize = True
            Me.label1.Location = New System.Drawing.Point(13, 20)
            Me.label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label1.Name = "label1"
            Me.label1.Size = New System.Drawing.Size(59, 13)
            Me.label1.TabIndex = 1
            Me.label1.Text = "Service Uri"
            '
            'txtConnectionTestPassword
            '
            Me.txtConnectionTestPassword.Location = New System.Drawing.Point(82, 66)
            Me.txtConnectionTestPassword.Margin = New System.Windows.Forms.Padding(2)
            Me.txtConnectionTestPassword.Name = "txtConnectionTestPassword"
            Me.txtConnectionTestPassword.Size = New System.Drawing.Size(419, 20)
            Me.txtConnectionTestPassword.TabIndex = 2
            Me.txtConnectionTestPassword.Text = "Uyumsoft"
            '
            'txtConnectionTestUserName
            '
            Me.txtConnectionTestUserName.Location = New System.Drawing.Point(82, 43)
            Me.txtConnectionTestUserName.Margin = New System.Windows.Forms.Padding(2)
            Me.txtConnectionTestUserName.Name = "txtConnectionTestUserName"
            Me.txtConnectionTestUserName.Size = New System.Drawing.Size(419, 20)
            Me.txtConnectionTestUserName.TabIndex = 2
            Me.txtConnectionTestUserName.Text = "Uyumsoft"
            '
            'txtConnectionTestUri
            '
            Me.txtConnectionTestUri.Location = New System.Drawing.Point(82, 20)
            Me.txtConnectionTestUri.Margin = New System.Windows.Forms.Padding(2)
            Me.txtConnectionTestUri.Name = "txtConnectionTestUri"
            Me.txtConnectionTestUri.Size = New System.Drawing.Size(419, 20)
            Me.txtConnectionTestUri.TabIndex = 2
            Me.txtConnectionTestUri.Text = "https://efatura-test.uyumsoft.com.tr/services/despatchintegration"
            '
            'btnSaveConnectionTestInfo
            '
            Me.btnSaveConnectionTestInfo.Location = New System.Drawing.Point(444, 99)
            Me.btnSaveConnectionTestInfo.Margin = New System.Windows.Forms.Padding(2)
            Me.btnSaveConnectionTestInfo.Name = "btnSaveConnectionTestInfo"
            Me.btnSaveConnectionTestInfo.Size = New System.Drawing.Size(56, 29)
            Me.btnSaveConnectionTestInfo.TabIndex = 0
            Me.btnSaveConnectionTestInfo.Text = "Kaydet"
            Me.btnSaveConnectionTestInfo.UseVisualStyleBackColor = True
            '
            'tabPage4
            '
            Me.tabPage4.Location = New System.Drawing.Point(4, 22)
            Me.tabPage4.Margin = New System.Windows.Forms.Padding(2)
            Me.tabPage4.Name = "tabPage4"
            Me.tabPage4.Padding = New System.Windows.Forms.Padding(2)
            Me.tabPage4.Size = New System.Drawing.Size(973, 470)
            Me.tabPage4.TabIndex = 1
            Me.tabPage4.Text = "Canlı Sistem"
            Me.tabPage4.UseVisualStyleBackColor = True
            '
            'openFileDialog1
            '
            Me.openFileDialog1.FileName = "openFileDialog1"
            '
            'Form1
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(993, 526)
            Me.Controls.Add(Me.tabControl1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Margin = New System.Windows.Forms.Padding(2)
            Me.Name = "Form1"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Uyum eIrsaliye  Customer Web Servis Demo"
            Me.tabControl1.ResumeLayout(False)
            Me.tabPage1.ResumeLayout(False)
            Me.tabPage1.PerformLayout()
            Me.groupBox10.ResumeLayout(False)
            Me.groupBox13.ResumeLayout(False)
            Me.groupBox12.ResumeLayout(False)
            Me.groupbox.ResumeLayout(False)
            Me.groupBox9.ResumeLayout(False)
            Me.groupBox9.PerformLayout()
            Me.groupBox11.ResumeLayout(False)
            Me.groupBox11.PerformLayout()
            Me.groupBox5.ResumeLayout(False)
            Me.groupBox6.ResumeLayout(False)
            Me.groupBox6.PerformLayout()
            Me.groupBox8.ResumeLayout(False)
            Me.groupBox8.PerformLayout()
            Me.groupBox1.ResumeLayout(False)
            Me.groupBox3.ResumeLayout(False)
            Me.groupBox3.PerformLayout()
            Me.groupBox4.ResumeLayout(False)
            Me.groupBox4.PerformLayout()
            Me.groupBox2.ResumeLayout(False)
            Me.groupBox2.PerformLayout()
            Me.tabPage2.ResumeLayout(False)
            Me.tabControl2.ResumeLayout(False)
            Me.tabPage3.ResumeLayout(False)
            Me.tabPage3.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

#End Region

        Private tabControl1 As System.Windows.Forms.TabControl
		Private tabPage1 As System.Windows.Forms.TabPage
		Private tabPage2 As System.Windows.Forms.TabPage
		Private txtConnectionTestUri As System.Windows.Forms.TextBox
		Private label1 As System.Windows.Forms.Label
		Private WithEvents btnSaveConnectionTestInfo As System.Windows.Forms.Button
		Private tabControl2 As System.Windows.Forms.TabControl
		Private tabPage3 As System.Windows.Forms.TabPage
		Private tabPage4 As System.Windows.Forms.TabPage
		Private label3 As System.Windows.Forms.Label
		Private label2 As System.Windows.Forms.Label
		Private txtConnectionTestPassword As System.Windows.Forms.TextBox
		Private txtConnectionTestUserName As System.Windows.Forms.TextBox
		Private WithEvents btnSendDespatch As System.Windows.Forms.Button
		Private WithEvents btnSaveDespatchXml As System.Windows.Forms.Button
		Private WithEvents btnPreviewDespatch As System.Windows.Forms.Button
		Private WithEvents btnCreateDespatchFromDespatchXML As System.Windows.Forms.Button
		Private openFileDialog1 As System.Windows.Forms.OpenFileDialog
		Private WithEvents btnCreateDespatchFromDespatchInfoXml As System.Windows.Forms.Button
		Private label5 As System.Windows.Forms.Label
		Private label4 As System.Windows.Forms.Label
		Private txtDespatchUUID As System.Windows.Forms.TextBox
		Private txtDespatchID As System.Windows.Forms.TextBox
		Private WithEvents btnQueryOutboxDespatchStatus As System.Windows.Forms.Button
		Private WithEvents btnGetOutboxDespatchStatusWithLogs As System.Windows.Forms.Button
		Private label6 As System.Windows.Forms.Label
		Private label9 As System.Windows.Forms.Label
		Private WithEvents btnSaveDraftDespatch As System.Windows.Forms.Button
		Private WithEvents btnSendDraft As System.Windows.Forms.Button
		Private WithEvents btnCancelDraft As System.Windows.Forms.Button
		Private groupBox1 As System.Windows.Forms.GroupBox
		Private groupBox3 As System.Windows.Forms.GroupBox
		Private groupBox4 As System.Windows.Forms.GroupBox
		Private WithEvents btnCompressedSendDespatch As System.Windows.Forms.Button
		Private label10 As System.Windows.Forms.Label
		Private groupBox2 As System.Windows.Forms.GroupBox
		Private label8 As System.Windows.Forms.Label
		Private label7 As System.Windows.Forms.Label
		Private groupBox5 As System.Windows.Forms.GroupBox
		Private groupBox6 As System.Windows.Forms.GroupBox
		Private label11 As System.Windows.Forms.Label
		Private groupBox8 As System.Windows.Forms.GroupBox
		Private label13 As System.Windows.Forms.Label
		Private WithEvents btnQueryInboxDespatchStatus As System.Windows.Forms.Button
		Private label15 As System.Windows.Forms.Label
		Private label14 As System.Windows.Forms.Label
		Private WithEvents btnGetInboxDespatchStatusWithLogs As System.Windows.Forms.Button
		Private WithEvents btnGetInboxDespatch As System.Windows.Forms.Button
		Private WithEvents btnGetInboxDespatchData As System.Windows.Forms.Button
		Private WithEvents GetInboxDespatchView As System.Windows.Forms.Button
		Private WithEvents btnGetInboxDespatchPdf As System.Windows.Forms.Button
		Private WithEvents btnGetInboxDespatchList As System.Windows.Forms.Button
		Private WithEvents btnGetOutboxDespatches As System.Windows.Forms.Button
		Private WithEvents btnGetInboxDespatches As System.Windows.Forms.Button
		Private WithEvents btnGetOutboxDespatchList As System.Windows.Forms.Button
		Private WithEvents btnGetOutboxDespatch As System.Windows.Forms.Button
		Private groupbox As System.Windows.Forms.GroupBox
		Private groupBox9 As System.Windows.Forms.GroupBox
		Private label18 As System.Windows.Forms.Label
		Private label21 As System.Windows.Forms.Label
		Private label20 As System.Windows.Forms.Label
		Private label16 As System.Windows.Forms.Label
		Private label17 As System.Windows.Forms.Label
		Private label12 As System.Windows.Forms.Label
		Private groupBox11 As System.Windows.Forms.GroupBox
		Private WithEvents btnGetOutboxDespatchData As System.Windows.Forms.Button
		Private label26 As System.Windows.Forms.Label
		Private label25 As System.Windows.Forms.Label
		Private label24 As System.Windows.Forms.Label
		Private label23 As System.Windows.Forms.Label
		Private label22 As System.Windows.Forms.Label
		Private label19 As System.Windows.Forms.Label
		Private WithEvents btnGetOutboxDespatchView As System.Windows.Forms.Button
		Private WithEvents btnGetOutboxDespatchPdf As System.Windows.Forms.Button
		Private WithEvents btnSendDespatchResponse As System.Windows.Forms.Button
		Private groupBox10 As System.Windows.Forms.GroupBox
		Private groupBox12 As System.Windows.Forms.GroupBox
		Private WithEvents btnInboxReponseStatus As System.Windows.Forms.Button
		Private groupBox13 As System.Windows.Forms.GroupBox
		Private WithEvents btnGetInboxReceiptAdvices As System.Windows.Forms.Button
		Private WithEvents SetDespatchReceiptAdvicesTaken As System.Windows.Forms.Button
		Private label27 As System.Windows.Forms.Label
		Private txtGondericiVKN As System.Windows.Forms.TextBox
		Private label28 As System.Windows.Forms.Label
		Private txtVKNAlici As System.Windows.Forms.TextBox
	End Class
End Namespace

