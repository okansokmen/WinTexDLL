Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace UHFAPP.utils
	Public Class EpcInfo
		Public Sub New(ByVal epc As String, ByVal count As Integer, ByVal epcBytes() As Byte, ByVal tidBytes() As Byte)
			Me.epc_Conflict = epc
			Me.count_Conflict = count
			Me.epcBytes_Conflict=epcBytes
			Me.epcBytes_Conflict = epcBytes
			If epcBytes IsNot Nothing AndAlso epcBytes.Length > 0 AndAlso tidBytes IsNot Nothing AndAlso tidBytes.Length > 0 Then
				epcAndTidBytes_Conflict = New Byte((epcBytes.Length + tidBytes.Length) - 1){}
				For k As Integer = 0 To epcBytes.Length - 1
					epcAndTidBytes_Conflict(k)=epcBytes(k)
				Next k
				For k As Integer = 0 To tidBytes.Length - 1
					epcAndTidBytes_Conflict(k + epcBytes.Length) = tidBytes(k)
				Next k
			ElseIf epcBytes IsNot Nothing AndAlso epcBytes.Length > 0 Then
				epcAndTidBytes_Conflict = epcBytes
			ElseIf tidBytes IsNot Nothing AndAlso tidBytes.Length > 0 Then
				epcAndTidBytes_Conflict = tidBytes
			Else
			  epcAndTidBytes_Conflict = New Byte(){}
			End If
		End Sub
'INSTANT VB NOTE: The field epc was renamed since Visual Basic does not allow fields to have the same case-insensitive name as other class members:
		Private epc_Conflict As String

		Public Property Epc As String
			Get
				Return epc_Conflict
			End Get
			Set(ByVal value As String)
				epc_Conflict = value
			End Set
		End Property

'INSTANT VB NOTE: The field count was renamed since Visual Basic does not allow fields to have the same case-insensitive name as other class members:
		Private count_Conflict As Integer

		Public Property Count As Integer
			Get
				Return count_Conflict
			End Get
			Set(ByVal value As Integer)
				count_Conflict = value
			End Set
		End Property

'INSTANT VB NOTE: The field epcBytes was renamed since Visual Basic does not allow fields to have the same case-insensitive name as other class members:
		Private epcBytes_Conflict() As Byte

		Public Property EpcBytes As Byte()
			Get
				Return epcBytes_Conflict
			End Get
			Set(ByVal value As Byte())
				epcBytes_Conflict = value
			End Set
		End Property

'INSTANT VB NOTE: The field tidBytes was renamed since Visual Basic does not allow fields to have the same case-insensitive name as other class members:
		Private tidBytes_Conflict() As Byte

		Public Property TidBytes As Byte()
			Get
				Return tidBytes_Conflict
			End Get
			Set(ByVal value As Byte())
				tidBytes_Conflict = value
			End Set
		End Property


'INSTANT VB NOTE: The field epcAndTidBytes was renamed since Visual Basic does not allow fields to have the same case-insensitive name as other class members:
		Private epcAndTidBytes_Conflict() As Byte

		Public Property EpcAndTidBytes As Byte()
			Get
				Return epcAndTidBytes_Conflict
			End Get
			Set(ByVal value As Byte())
				epcAndTidBytes_Conflict = value
			End Set
		End Property

	End Class
End Namespace
