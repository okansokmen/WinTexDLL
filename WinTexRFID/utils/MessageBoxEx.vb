Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.Drawing

Namespace UHFAPP
	Public Class MessageBoxEx
		Private Shared _owner As IWin32Window
		Private Shared _hookProc As HookProc
		Private Shared _hHook As IntPtr

		Public Shared Function Show(ByVal text As String) As DialogResult
			Initialize()
			Return MessageBox.Show(text)
		End Function

		Public Shared Function Show(ByVal text As String, ByVal caption As String) As DialogResult
			Initialize()
			Return MessageBox.Show(text, caption)
		End Function

		Public Shared Function Show(ByVal text As String, ByVal caption As String, ByVal buttons As MessageBoxButtons) As DialogResult
			Initialize()
			Return MessageBox.Show(text, caption, buttons)
		End Function

		Public Shared Function Show(ByVal text As String, ByVal caption As String, ByVal buttons As MessageBoxButtons, ByVal icon As MessageBoxIcon) As DialogResult
			Initialize()
			Return MessageBox.Show(text, caption, buttons, icon)
		End Function

		Public Shared Function Show(ByVal text As String, ByVal caption As String, ByVal buttons As MessageBoxButtons, ByVal icon As MessageBoxIcon, ByVal defButton As MessageBoxDefaultButton) As DialogResult
			Initialize()
			Return MessageBox.Show(text, caption, buttons, icon, defButton)
		End Function

		Public Shared Function Show(ByVal text As String, ByVal caption As String, ByVal buttons As MessageBoxButtons, ByVal icon As MessageBoxIcon, ByVal defButton As MessageBoxDefaultButton, ByVal options As MessageBoxOptions) As DialogResult
			Initialize()
			Return MessageBox.Show(text, caption, buttons, icon, defButton, options)
		End Function

		Public Shared Function Show(ByVal owner As IWin32Window, ByVal text As String) As DialogResult
			_owner = owner
			Initialize()
			Return MessageBox.Show(owner, text)
		End Function

		Public Shared Function Show(ByVal owner As IWin32Window, ByVal text As String, ByVal caption As String) As DialogResult
			_owner = owner
			Initialize()
			Return MessageBox.Show(owner, text, caption)
		End Function

		Public Shared Function Show(ByVal owner As IWin32Window, ByVal text As String, ByVal caption As String, ByVal buttons As MessageBoxButtons) As DialogResult
			_owner = owner
			Initialize()
			Return MessageBox.Show(owner, text, caption, buttons)
		End Function

		Public Shared Function Show(ByVal owner As IWin32Window, ByVal text As String, ByVal caption As String, ByVal buttons As MessageBoxButtons, ByVal icon As MessageBoxIcon) As DialogResult
			_owner = owner
			Initialize()
			Return MessageBox.Show(owner, text, caption, buttons, icon)
		End Function

		Public Shared Function Show(ByVal owner As IWin32Window, ByVal text As String, ByVal caption As String, ByVal buttons As MessageBoxButtons, ByVal icon As MessageBoxIcon, ByVal defButton As MessageBoxDefaultButton) As DialogResult
			_owner = owner
			Initialize()
			Return MessageBox.Show(owner, text, caption, buttons, icon, defButton)
		End Function

		Public Shared Function Show(ByVal owner As IWin32Window, ByVal text As String, ByVal caption As String, ByVal buttons As MessageBoxButtons, ByVal icon As MessageBoxIcon, ByVal defButton As MessageBoxDefaultButton, ByVal options As MessageBoxOptions) As DialogResult
			_owner = owner
			Initialize()
			Return MessageBox.Show(owner, text, caption, buttons, icon, defButton, options)
		End Function

		Public Delegate Function HookProc(ByVal nCode As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr

		Public Delegate Sub TimerProc(ByVal hWnd As IntPtr, ByVal uMsg As UInteger, ByVal nIDEvent As UIntPtr, ByVal dwTime As UInteger)

		Public Const WH_CALLWNDPROCRET As Integer = 12

		Public Enum CbtHookAction As Integer
			HCBT_MOVESIZE = 0
			HCBT_MINMAX = 1
			HCBT_QS = 2
			HCBT_CREATEWND = 3
			HCBT_DESTROYWND = 4
			HCBT_ACTIVATE = 5
			HCBT_CLICKSKIPPED = 6
			HCBT_KEYSKIPPED = 7
			HCBT_SYSCOMMAND = 8
			HCBT_SETFOCUS = 9
		End Enum

		<DllImport("user32.dll")>
		Private Shared Function GetWindowRect(ByVal hWnd As IntPtr, ByRef lpRect As Rectangle) As Boolean
		End Function

		<DllImport("user32.dll")>
		Private Shared Function MoveWindow(ByVal hWnd As IntPtr, ByVal X As Integer, ByVal Y As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal bRepaint As Boolean) As Integer
		End Function

		<DllImport("User32.dll")>
		Public Shared Function SetTimer(ByVal hWnd As IntPtr, ByVal nIDEvent As UIntPtr, ByVal uElapse As UInteger, ByVal lpTimerFunc As TimerProc) As UIntPtr
		End Function

		<DllImport("User32.dll")>
		Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
		End Function

		<DllImport("user32.dll")>
		Public Shared Function SetWindowsHookEx(ByVal idHook As Integer, ByVal lpfn As HookProc, ByVal hInstance As IntPtr, ByVal threadId As Integer) As IntPtr
		End Function

		<DllImport("user32.dll")>
		Public Shared Function UnhookWindowsHookEx(ByVal idHook As IntPtr) As Integer
		End Function

		<DllImport("user32.dll")>
		Public Shared Function CallNextHookEx(ByVal idHook As IntPtr, ByVal nCode As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
		End Function

		<DllImport("user32.dll")>
		Public Shared Function GetWindowTextLength(ByVal hWnd As IntPtr) As Integer
		End Function

		<DllImport("user32.dll")>
		Public Shared Function GetWindowText(ByVal hWnd As IntPtr, ByVal text As StringBuilder, ByVal maxLength As Integer) As Integer
		End Function

		<DllImport("user32.dll")>
		Public Shared Function EndDialog(ByVal hDlg As IntPtr, ByVal nResult As IntPtr) As Integer
		End Function

		<StructLayout(LayoutKind.Sequential)>
		Public Structure CWPRETSTRUCT
			Public lResult As IntPtr
			Public lParam As IntPtr
			Public wParam As IntPtr
			Public message As UInteger
			Public hwnd As IntPtr
		End Structure

		Shared Sub New()
			_hookProc = New HookProc(AddressOf MessageBoxHookProc)
			_hHook = IntPtr.Zero
		End Sub

		Private Shared Sub Initialize()
			If _hHook <> IntPtr.Zero Then
				Throw New NotSupportedException("multiple calls are not supported")
			End If

			If _owner IsNot Nothing Then
				_hHook = SetWindowsHookEx(WH_CALLWNDPROCRET, _hookProc, IntPtr.Zero, AppDomain.GetCurrentThreadId())
			End If
		End Sub

		Private Shared Function MessageBoxHookProc(ByVal nCode As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
			If nCode < 0 Then
				Return CallNextHookEx(_hHook, nCode, wParam, lParam)
			End If

			Dim msg As CWPRETSTRUCT = DirectCast(Marshal.PtrToStructure(lParam, GetType(CWPRETSTRUCT)), CWPRETSTRUCT)
			Dim hook As IntPtr = _hHook

			If msg.message = CInt(CbtHookAction.HCBT_ACTIVATE) Then
				Try
					CenterWindow(msg.hwnd)
				Finally
					UnhookWindowsHookEx(_hHook)
					_hHook = IntPtr.Zero
				End Try
			End If

			Return CallNextHookEx(hook, nCode, wParam, lParam)
		End Function

		Private Shared Sub CenterWindow(ByVal hChildWnd As IntPtr)
			Dim recChild As New Rectangle(0, 0, 0, 0)
			Dim success As Boolean = GetWindowRect(hChildWnd, recChild)

			Dim width As Integer = recChild.Width - recChild.X
			Dim height As Integer = recChild.Height - recChild.Y

			Dim recParent As New Rectangle(0, 0, 0, 0)
			success = GetWindowRect(_owner.Handle, recParent)

			Dim ptCenter As New Point(0, 0)
			ptCenter.X = recParent.X + ((recParent.Width - recParent.X) \ 2)
			ptCenter.Y = recParent.Y + ((recParent.Height - recParent.Y) \ 2)

			Dim ptStart As New Point(0, 0)
			ptStart.X = (ptCenter.X - (width \ 2))
			ptStart.Y = (ptCenter.Y - (height \ 2))

			ptStart.X = If(ptStart.X < 0, 0, ptStart.X)
			ptStart.Y = If(ptStart.Y < 0, 0, ptStart.Y)

			Dim result As Integer = MoveWindow(hChildWnd, ptStart.X, ptStart.Y, width, height, False)
		End Sub
	End Class
End Namespace
