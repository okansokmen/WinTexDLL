Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports BLEDeviceAPI

Namespace UHFAPP.custom.TempertureTag2
   Public Class TempertureInfo
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
'INSTANT VB NOTE: The field temperture was renamed since Visual Basic does not allow fields to have the same case-insensitive name as other class members:
		Private temperture_Conflict As String
'INSTANT VB NOTE: The field time was renamed since Visual Basic does not allow fields to have the same case-insensitive name as other class members:
		Private time_Conflict As String

		Public Property Time As String
			Get
				Return time_Conflict
			End Get
			Set(ByVal value As String)
				time_Conflict = value
			End Set
		End Property


		Public Property Temperture As String
			Get
				Return temperture_Conflict
			End Get
			Set(ByVal value As String)
				temperture_Conflict = value
			End Set
		End Property



   End Class
End Namespace
