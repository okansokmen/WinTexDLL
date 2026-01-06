Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace UHFAPP.RFID_HF
	Friend Class ISO15693Entity
'INSTANT VB NOTE: The field uid was renamed since Visual Basic does not allow fields to have the same case-insensitive name as other class members:
		Private uid_Conflict() As Byte
'INSTANT VB NOTE: The field afi was renamed since Visual Basic does not allow fields to have the same case-insensitive name as other class members:
		Private afi_Conflict As Byte = 0
'INSTANT VB NOTE: The field desfid was renamed since Visual Basic does not allow fields to have the same case-insensitive name as other class members:
		Private desfid_Conflict As Byte = 0
'INSTANT VB NOTE: The field type was renamed since Visual Basic does not allow fields to have the same case-insensitive name as other class members:
		Private type_Conflict As TagType

'''            
'''         * 标签类型<br>
'''         * Tag type<br>
'''         *
'''         * @author liuruifeng
'''         
		Public Enum TagType
			ICODE2 = 0
			TI2048 = 4
			STLRIS64K = 8
			EM4033 = 12
			UNKNOWN = 100

		End Enum

		Public Property Afi As Byte
			Get
				Return afi_Conflict
			End Get
			Set(ByVal value As Byte)
				afi_Conflict = value
			End Set
		End Property


		Public Property Desfid As Byte
			Get
				Return desfid_Conflict
			End Get
			Set(ByVal value As Byte)
				desfid_Conflict = value
			End Set
		End Property

		Public Property Uid As Byte()
			Get
				Return uid_Conflict
			End Get
			Set(ByVal value As Byte())
				uid_Conflict = value
			End Set
		End Property

		Public Property Type As TagType
			Get
				Return type_Conflict
			End Get
			Set(ByVal value As TagType)
				type_Conflict = value
			End Set
		End Property


	End Class
End Namespace
