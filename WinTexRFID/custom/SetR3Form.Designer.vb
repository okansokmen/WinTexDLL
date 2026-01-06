Namespace UHFAPP.custom
	Partial Public Class SetR3Form
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
			Me.lvParm = New System.Windows.Forms.ListView()
			Me.cId = New System.Windows.Forms.ColumnHeader()
			Me.cName = New System.Windows.Forms.ColumnHeader()
			Me.cValue = New System.Windows.Forms.ColumnHeader()
			Me.specification = New System.Windows.Forms.ColumnHeader()
			Me.btnSet = New System.Windows.Forms.Button()
			Me.SuspendLayout()
			' 
			' lvParm
			' 
			Me.lvParm.BackColor = System.Drawing.SystemColors.ControlLightLight
			Me.lvParm.Columns.AddRange(New System.Windows.Forms.ColumnHeader() { Me.cId, Me.cName, Me.cValue, Me.specification})
			Me.lvParm.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.lvParm.FullRowSelect = True
			Me.lvParm.Location = New System.Drawing.Point(12, 12)
			Me.lvParm.Name = "lvParm"
			Me.lvParm.Size = New System.Drawing.Size(1245, 536)
			Me.lvParm.TabIndex = 4
			Me.lvParm.UseCompatibleStateImageBehavior = False
			Me.lvParm.View = System.Windows.Forms.View.Details
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.lvParm.DoubleClick += new System.EventHandler(this.lvParm_DoubleClick);
			' 
			' cId
			' 
			Me.cId.Text = "Id"
			Me.cId.Width = 50
			' 
			' cName
			' 
			Me.cName.Text = "Name"
			Me.cName.Width = 150
			' 
			' cValue
			' 
			Me.cValue.Text = "Value"
			Me.cValue.Width = 100
			' 
			' specification
			' 
			Me.specification.Text = "Specification"
			Me.specification.Width = 1300
			' 
			' btnSet
			' 
			Me.btnSet.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnSet.Location = New System.Drawing.Point(566, 566)
			Me.btnSet.Name = "btnSet"
			Me.btnSet.Size = New System.Drawing.Size(135, 53)
			Me.btnSet.TabIndex = 5
			Me.btnSet.Text = "Set"
			Me.btnSet.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
			' 
			' SetR3Form
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 12F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(1269, 631)
			Me.Controls.Add(Me.btnSet)
			Me.Controls.Add(Me.lvParm)
			Me.Name = "SetR3Form"
			Me.Text = "R5Form"
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private WithEvents lvParm As System.Windows.Forms.ListView
		Private cName As System.Windows.Forms.ColumnHeader
		Private cValue As System.Windows.Forms.ColumnHeader
		Private WithEvents btnSet As System.Windows.Forms.Button
		Private cId As System.Windows.Forms.ColumnHeader
		Private specification As System.Windows.Forms.ColumnHeader
	End Class
End Namespace
