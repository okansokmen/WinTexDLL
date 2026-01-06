Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports BLEDeviceAPI
Imports System.Runtime.InteropServices

Namespace UHFAPP.barcode
	Partial Public Class HidInputForm
		Inherits Form

		'UHFAPI uhf = UHFAPI.getInstance();
		Private Shared ondataReceived As UHFAPP.UHFAPI.OnDataReceived = Nothing
		Private Const CELL_INVALID As Byte=0
		Private Const CELL_CONNECT_ID As Byte = 1
		Private Const CELL_CONNECT_IP As Byte = 2
		Private Const CELL_UHF_PC As Byte = 3
		Private Const CELL_UHF_RSSI As Byte = 4
		Private Const CELL_UHF_ANTENNA As Byte = 5
		Private Const CELL_UHF_EPC As Byte = 6
		Private Const CELL_UHF_TID As Byte = 7
		Private Const CELL_UHF_USER As Byte = 8
		Private Const CELL_UHF_RESERVE As Byte = 9
		Private Const CELL_BARCODE As Byte = 10
		Private Const CELL_UHF_SENSOR As Byte = 11
		Private Shared Format As Integer = 0

		Public Sub New()
			InitializeComponent()
			cmbFormat.SelectedIndex = 1



		   ' ondataReceived = null;
		  ' UHFAPP.UHFAPI.OnDataReceived  ondataReceived = new UHFAPI.OnDataReceived(DataReceived);


		End Sub
		'[MarshalAs(UnmanagedType.LPArray, SizeConst = 4096)] byte[]
		Public Shared Sub DataReceived(ByVal pdata As IntPtr, ByVal len As Short)
			Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") & " DataReceived begin")
			Dim contentLen As Short
			Dim index As Short = 0
			Dim type As Byte

			Dim cellData(len - 1) As Byte
			Marshal.Copy(pdata, cellData, 0, len)
		  ' cellData= Utils.CopyArray(pdata,0,len);

			Dim pcontent() As Byte
		   ' printf("OnReceivedData:");
			For i As Integer = 0 To len - 1
				' Console.WriteLine("%02X", cellData[i]);
			Next i
			Dim temp() As Byte = Nothing
			Dim str As String
			'  printf("\n");
			Do While index < len
'INSTANT VB WARNING: An assignment within expression was extracted from the following statement:
'ORIGINAL LINE: type = cellData[index++];
				type = cellData(index)
				index += 1
				If (cellData(index) And &H80) = &H80 Then
					contentLen = CShort(((cellData(index) And &H7F) << 7) Or (cellData(index + 1) And &H7F))
					index += 2
				Else
'INSTANT VB WARNING: An assignment within expression was extracted from the following statement:
'ORIGINAL LINE: contentLen = cellData[index++];
					contentLen = cellData(index)
					index += 1
				End If

				pcontent = BLEDeviceAPI.Utils.CopyArray(cellData, index, contentLen)
				Select Case type
				Case CELL_UHF_PC
				   ' printf("PC:%02X%02X\n", pcontent[0], pcontent[1]);
				Case CELL_UHF_RSSI
				   ' printf("RSSI:%02X%02X\n", pcontent[0], pcontent[1]);
				Case CELL_UHF_ANTENNA
					'  printf("Antenna:%d\n", pcontent[0]);
				Case CELL_UHF_EPC
					'UHFAPI.PrintTextToCursor(System.Text.ASCIIEncoding.GetEncoding("GBK").GetBytes("你好你好"));
				   ' UHFAPI.PrintTextToCursor(pcontent);
					'  printf("EPC:");
					For i As Integer = 0 To contentLen - 1
						'  printf("%02X", pcontent[i]);
					Next i
						'   printf("\n");

						temp = BLEDeviceAPI.Utils.CopyArray(pcontent, 0, contentLen)
						str = DataConvert.ByteArrayToHexString(temp).Replace(" ", "") &vbLf
					temp= System.Text.ASCIIEncoding.ASCII.GetBytes(str)

					 UHFAPI.PrintTextToCursor(Format, temp, CShort(temp.Length))
				Case CELL_UHF_TID
					'  printf("TID:");
					For i As Integer = 0 To contentLen - 1
						'   printf("%02X", pcontent[i]);
					Next i
						'   printf("\n");
						temp = BLEDeviceAPI.Utils.CopyArray(pcontent, 0, contentLen)
						str = DataConvert.ByteArrayToHexString(temp).Replace(" ", "") & vbLf
					temp = System.Text.ASCIIEncoding.ASCII.GetBytes(str)

					UHFAPI.PrintTextToCursor(Format, temp, CShort(temp.Length))
				Case CELL_UHF_USER
				'  printf("User:");
					For i As Integer = 0 To contentLen - 1
						'   printf("%02X", pcontent[i]);
					Next i
						' printf("\n");
						temp = BLEDeviceAPI.Utils.CopyArray(pcontent, 0, contentLen)
						str = DataConvert.ByteArrayToHexString(temp).Replace(" ", "") & vbLf
					temp = System.Text.ASCIIEncoding.ASCII.GetBytes(str)

					UHFAPI.PrintTextToCursor(Format, temp, CShort(temp.Length))
				Case CELL_UHF_RESERVE
					'  printf("Reserve:");
					For i As Integer = 0 To contentLen - 1
						'   printf("%02X", pcontent[i]);
					Next i
					'  printf("\n");
				Case CELL_BARCODE
					'  printf("Barcode:");
					For i As Integer = 0 To contentLen - 1
						'   printf("%02X", pcontent[i]);
					Next i
					Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") & " barcode")
						UHFAPI.PrintTextToCursor(Format, BLEDeviceAPI.Utils.CopyArray(pcontent, 0, contentLen), contentLen)
						Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") & " barcode end")
					'   printf("\n");
				Case Else
					'  printf("unknow parameter:%d\n", type);
				End Select
				index += contentLen
			Loop
		End Sub
	 '  ENUM_CHAR_CODE_ANSI=0
		'ENUM_CHAR_CODE_UTF8




		Private Sub MainForm_eventOpen(ByVal open As Boolean)
			If open Then
				ondataReceived = AddressOf DataReceived
				UHFAPI.setOnDataReceived(ondataReceived)
				Format = cmbFormat.SelectedIndex
				UHFAPI.getInstance().GetSoftwareVersion()
			Else
				ondataReceived = Nothing
			End If
		End Sub

		Private Sub HidInputForm_VisibleChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Me.VisibleChanged
			If DirectCast(sender, HidInputForm).Visible Then
				AddHandler MainForm.eventOpen, AddressOf MainForm_eventOpen

			Else
				RemoveHandler MainForm.eventOpen, AddressOf MainForm_eventOpen
			End If
		End Sub

		Public Sub openState(ByVal isOpen As Boolean)
			If isOpen AndAlso ondataReceived Is Nothing Then
				ondataReceived = AddressOf DataReceived
				UHFAPI.setOnDataReceived(ondataReceived)
			End If

		End Sub

		Private Sub cmbFormat_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbFormat.SelectedIndexChanged
			If Not Me.cmbFormat.IsHandleCreated Then Return

			Format = cmbFormat.SelectedIndex
		End Sub
	End Class
End Namespace
