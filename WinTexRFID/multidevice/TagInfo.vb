Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports BLEDeviceAPI

Namespace UHFAPP.multidevice
	Public Class TagInfo
		' #define CONTENT_TYPE_INVALID        0
		' #define CONTENT_TYPE_EPC            1
		' #define CONTENT_TYPE_TID            2
		' #define CONTENT_TYPE_USER           3
		' #define CONTENT_TYPE_RSSI           4
		' #define CONTENT_TYPE_ANT            5
		' #define CONTENT_TYPE_ID             6
		' #define CONTENT_TYPE_IP             7


'INSTANT VB NOTE: The field id was renamed since Visual Basic does not allow fields to have the same case-insensitive name as other class members:
		Private id_Conflict As Integer

		Public Property Id As Integer
			Get
				Return id_Conflict
			End Get
			Set(ByVal value As Integer)
				id_Conflict = value
			End Set
		End Property


'INSTANT VB NOTE: The field errCode was renamed since Visual Basic does not allow fields to have the same case-insensitive name as other class members:
		Private errCode_Conflict As Integer

		Public Property ErrCode As Integer
			Get
				Return errCode_Conflict
			End Get
			Set(ByVal value As Integer)
				errCode_Conflict = value
			End Set
		End Property

'INSTANT VB NOTE: The field uhfTagInfo was renamed since Visual Basic does not allow fields to have the same case-insensitive name as other class members:
		Private uhfTagInfo_Conflict As UHFTAGInfo

		Public Property UhfTagInfo As UHFTAGInfo
			Get
				Return uhfTagInfo_Conflict
			End Get
			Set(ByVal value As UHFTAGInfo)
				uhfTagInfo_Conflict = value
			End Set
		End Property



	End Class
End Namespace
