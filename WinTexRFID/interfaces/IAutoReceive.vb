Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace UHFAPP.interfaces
	Public Interface IAutoReceive
		 Function Connect() As Boolean
		 Sub DisConnect()
	End Interface
End Namespace
