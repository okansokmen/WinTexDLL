Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json.Serialization
Imports Newtonsoft.Json
Namespace UHFAPP

   Public Class DeviceInfo


'INSTANT VB NOTE: The field id was renamed since Visual Basic does not allow fields to have the same case-insensitive name as other class members:
		Private id_Conflict As Integer

		<JsonProperty("id")>
		Public Property Id As Integer
			Get
				Return id_Conflict
			End Get
			Set(ByVal value As Integer)
				id_Conflict = value
			End Set
		End Property
		Private _type As String

		<JsonProperty("_type")>
		Public Property Type As String
			Get
				Return _type
			End Get
			Set(ByVal value As String)
				_type = value
			End Set
		End Property
'INSTANT VB NOTE: The field ip was renamed since Visual Basic does not allow fields to have the same case-insensitive name as other class members:
		Private ip_Conflict As String

		<JsonProperty("ip")>
		Public Property Ip As String
			Get
				Return ip_Conflict
			End Get
			Set(ByVal value As String)
				ip_Conflict = value
			End Set
		End Property
'INSTANT VB NOTE: The field port was renamed since Visual Basic does not allow fields to have the same case-insensitive name as other class members:
		Private port_Conflict As Integer

		<JsonProperty("port")>
		Public Property Port As Integer
			Get
				Return port_Conflict
			End Get
			Set(ByVal value As Integer)
				port_Conflict = value
			End Set
		End Property

   End Class
End Namespace
