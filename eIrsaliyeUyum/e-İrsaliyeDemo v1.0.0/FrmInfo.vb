Imports eIrsaliyeUyum.DespatchConnect
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms

Namespace eIrsaliyeUyum
	Partial Public Class FrmInfo
		Inherits Form

		Public Sub New()
			InitializeComponent()
		End Sub
		Public Sub New(ByVal type As Integer)

			Select Case type

				Case 9
					lblCalledMethod.Text = "GetInboxDespatch"
			End Select

		End Sub
	'    public DespatchIntegrationClient CreateClient()
	'    {
	'        var client = new DespatchIntegrationClient();

	'        client.ClientCredentials.UserName.UserName = txtConnectionTestUserName.Text;
	'        client.ClientCredentials.UserName.Password = txtConnectionTestPassword.Text;
	'        client.Endpoint.Address = new System.ServiceModel.EndpointAddress(txtConnectionTestUri.Text);

	'        return client;

	'    }
	End Class
End Namespace
