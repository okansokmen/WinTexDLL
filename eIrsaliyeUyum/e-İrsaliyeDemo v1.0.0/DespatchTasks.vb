Imports eIrsaliyeUyum.DespatchConnect
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace eIrsaliyeUyum

	Friend Class DespatchTasks

		Public Shared Instance As New DespatchTasks()

		Public Function CreateClient() As DespatchIntegrationClient
			Dim username As String = UtilRoot.oUyumConnect.cUyumUsername              ' My.Settings.Default.WebServiceTestUsername
			Dim password As String = UtilRoot.oUyumConnect.cUyumPassword              ' My.Settings.Default.WebServiceTestPassword
			Dim serviceuri As String = UtilRoot.oUyumConnect.cUyumEirsaliyeServiceUrl ' My.Settings.Default.WebServiceTestUri

			Dim client As New DespatchIntegrationClient()
			client.Endpoint.Address = New System.ServiceModel.EndpointAddress(serviceuri)
			client.ClientCredentials.UserName.UserName = username
			client.ClientCredentials.UserName.Password = password
			Return client
		End Function
	End Class

End Namespace
