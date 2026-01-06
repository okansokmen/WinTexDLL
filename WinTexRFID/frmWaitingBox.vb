Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms

Namespace WinForm_Test
	Partial Public Class frmWaitingBox
		Inherits Form

		#Region "Properties"
		Private _MaxWaitTime As Integer
		Private _WaitTime As Integer
		Private _CancelEnable As Boolean
		Private _AsyncResult As IAsyncResult
		Private _Method As EventHandler(Of EventArgs)
		Private _IsShown As Boolean = True
		Private ReadOnly _EffectCount As Integer = 10
		Private ReadOnly _EffectTime As Integer = 500
		''' <summary>
		''' 控制界面显示的特性
		''' </summary>
		Private _Timer As Timer
		Private privateMessage As String
		Public Property Message As String
			Get
				Return privateMessage
			End Get
			Private Set(ByVal value As String)
				privateMessage = value
			End Set
		End Property
		Public Property TimeSpan As Integer
		Public Property FormEffectEnable As Boolean
'INSTANT VB NOTE: The field message was renamed since Visual Basic does not allow fields to have the same case-insensitive name as other class members:
		Public Shared message_Conflict As String = ""
		#End Region



		#Region "frmWaitingBox"
		Public Sub New(ByVal method As EventHandler(Of EventArgs), ByVal maxWaitTime As Integer, ByVal waitMessage As String, ByVal cancelEnable As Boolean, ByVal timerVisable As Boolean)
			maxWaitTime *= 1000
			Initialize(method, maxWaitTime,waitMessage, cancelEnable, timerVisable)
		End Sub
		Public Sub New(ByVal method As EventHandler(Of EventArgs))
			Dim maxWaitTime As Integer=60*1000
			Dim waitMessage As String = "Please wait..."
			Dim cancelEnable As Boolean=True
			Dim timerVisable As Boolean=True
			Initialize(method, maxWaitTime,waitMessage, cancelEnable, timerVisable)
		End Sub
		Public Sub New(ByVal method As EventHandler(Of EventArgs), ByVal waitMessage As String)
			Dim maxWaitTime As Integer = 60 * 1000
			Dim cancelEnable As Boolean = True
			Dim timerVisable As Boolean = True
			Initialize(method, maxWaitTime, waitMessage, cancelEnable, timerVisable)
		End Sub
		Public Sub New(ByVal method As EventHandler(Of EventArgs), ByVal cancelEnable As Boolean, ByVal timerVisable As Boolean)
			Dim maxWaitTime As Integer = 60*1000
			Dim waitMessage As String = "正在处理数据，请稍后..."
			Initialize(method, maxWaitTime,waitMessage, cancelEnable, timerVisable)
		End Sub
		#End Region

		#Region "Initialize"
		Private Sub Initialize(ByVal method As EventHandler(Of EventArgs), ByVal maxWaitTime As Integer, ByVal waitMessage As String, ByVal cancelEnable As Boolean, ByVal timerVisable As Boolean)
			InitializeComponent()
			'initialize form
			Me.FormBorderStyle = FormBorderStyle.None
			Me.StartPosition = FormStartPosition.CenterParent
			Me.ShowInTaskbar = False
		  '  Color[] c = GetRandColor();
		   ' this.panel1.BackColor = c[0];
		  '  this.BackColor = c[1];
			Me.labMessage.Text = waitMessage
			message_Conflict = waitMessage
			_Timer = New Timer()
			_Timer.Interval = _EffectTime\_EffectCount
			AddHandler _Timer.Tick, AddressOf _Timer_Tick
			Me.Opacity = 0
			FormEffectEnable = True
			'para
			TimeSpan = 500
			Message = String.Empty
			_CancelEnable = cancelEnable
			_MaxWaitTime = maxWaitTime
			_WaitTime = 0
			_Method = method
		   ' this.pictureBoxCancel.Visible = _CancelEnable;
		  '  this.labTimer.Visible = timerVisable;
			Me.timer1.Interval = TimeSpan
			Me.timer1.Start()
		End Sub
		#End Region

		#Region "Color"
'        
'        private Color[] GetRandColor()
'        {
'            int rMax = 248;
'            int rMin = 204;
'            int gMax = 250;
'            int gMin = 215;
'            int bMax = 250;
'            int bMin = 240;
'            Random r = new Random(DateTime.Now.Millisecond);
'            int r1 = r.Next(rMin, rMax);
'            int r2 = r1 + 5;
'            int g1 = r.Next(gMin, gMax);
'            int g2 = g1 + 5;
'            int b1 = r.Next(bMin, bMax);
'            int b2 = b1 + 5;
'            Color c1 = Color.FromArgb(r1, g1, b1);
'            Color c2 = Color.FromArgb(r2, g2, b2);
'            Color[] c = { c1, c2 };
'            return c;
'        }
'        
		#End Region

		#Region "Events"
		Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
			Me.Message = "您结束了当前操作！"
			Me.Close()
		End Sub

		Private Sub timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles timer1.Tick
			Try
				_WaitTime += TimeSpan
				labMessage.Text = message_Conflict
				'this.labTimer.Text = ; //string.Format("{0}秒", _WaitTime / 1000);
				If Not Me._AsyncResult.IsCompleted Then
					If _WaitTime > _MaxWaitTime Then
						Message = String.Format("处理数据超时{0}秒，结束当前操作！", _MaxWaitTime \ 1000)
						Me.Close()
					End If
				Else
					Me.Message = String.Empty
					Me.Close()
				End If
			Catch ex As Exception

			End Try

		End Sub

		Private Sub frmWaitingBox_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown

			_AsyncResult = _Method.BeginInvoke(Nothing, Nothing, Nothing, Nothing)
			'Effect
			If FormEffectEnable Then
				_Timer.Start()
			Else
				Me.Opacity = 1
			End If
		End Sub
		Private Sub frmWaitingBox_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
			Try
				If FormEffectEnable Then
					If Me.Opacity >= 1 Then
						e.Cancel = True
					End If
					_Timer.Start()
				End If
			Catch ex As Exception
			End Try
		End Sub
		Private Sub _Timer_Tick(ByVal sender As Object, ByVal e As EventArgs)
			Try
				If _IsShown Then
					If Me.Opacity >= 1 Then
						_Timer.Stop()
						_IsShown = False
					End If
					Me.Opacity += 1.00 / _EffectCount
				Else
					If Me.Opacity <= 0 Then
						_Timer.Stop()
						_IsShown = True
						Me.Close()
					End If
					Me.Opacity -= 1.00 / _EffectCount
				End If
			Catch ex As Exception
			End Try
		End Sub
		#End Region


	End Class
End Namespace
