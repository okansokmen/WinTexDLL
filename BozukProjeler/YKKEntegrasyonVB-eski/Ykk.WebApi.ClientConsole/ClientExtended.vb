Imports System.Net.Http.Headers

Namespace Ykk.WebApi.ClientConsole
    Partial Public Class Client
        Public Property Username() As String
        Public Property Password() As String

        Private Sub PrepareRequest(ByVal client As System.Net.Http.HttpClient, ByVal request As System.Net.Http.HttpRequestMessage, ByVal url As String)
            If request.RequestUri.AbsolutePath.Contains("Authentication") Then
                Return
            End If
            Dim token = Me.LoginAsync(New LoginModel With {
                .Password = Me.Password,
                .Username = Me.Username
            }).Result
            request.Headers.Authorization = New AuthenticationHeaderValue("Bearer", token)
        End Sub
    End Class
End Namespace
