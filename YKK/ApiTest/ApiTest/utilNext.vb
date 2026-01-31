Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Text
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Module utilNext

    Public Async Function CreateRiskAnalysisAsync() As Task(Of (Id As String, Message As String))

        Dim url As String = "https://next.eroglugiyim.com/api/CreateRiskAnalysis"

        ' Create request object
        Dim payload As New Dictionary(Of String, String) From {
            {"PersonelName", oConnection.cPersonel},
            {"ModelNo", oConnection.cModelNo}
        }

        ' Serialize JSON using Newtonsoft.Json
        Dim json As String = JsonConvert.SerializeObject(payload)

        Using client As New HttpClient()

            client.DefaultRequestHeaders.Accept.Clear()
            client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("*/*"))

            Using content As New StringContent(json, Encoding.UTF8)

                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;odata.metadata=minimal;odata.streaming=true")

                Using response As HttpResponseMessage = Await client.PostAsync(url, content)

                    Dim responseText As String = Await response.Content.ReadAsStringAsync()

                    If Not response.IsSuccessStatusCode Then
                        Throw New HttpRequestException($"HTTP {CInt(response.StatusCode)} {response.ReasonPhrase}{Environment.NewLine}{responseText}")
                    End If

                    ' Parse response JSON
                    Dim obj As JObject = JObject.Parse(responseText)

                    Dim id As String = obj("Id").ToString()
                    Dim message As String = obj("Message").ToString()

                    Return (id, message)
                End Using
            End Using
        End Using
    End Function

End Module

