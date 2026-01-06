Option Explicit On
Imports System.IO
Imports System.Net
Imports Newtonsoft.Json.Linq

Module utilGoogleAI

    Public cGoogleApiKey As String = "AIzaSyD3AIFNNNzhesT2LsAfMuAROnFvsiBf1LM" ' AIzaSyCBOHSMpo2tjo2BeLwTNP_zaqvC6-5n2hY   Replace with your Gemini API key
    Public cGoogleBaseUrl As String = "https://generativelanguage.googleapis.com/v1beta"
    Public cGoogleModel As String = "gemini-2.5-flash"
    Public cGoogleHTMLModel As String = "gemini-2.5-flash"
    ' gemini-1.5-pro            Early versions with smaller token window
    ' gemini-1.5-flash          Early versions with smaller token window
    ' gemini-1.5-flash-latest   Early versions with smaller token window
    ' gemini-2.0-flash-lite     Cost-efficient variant, low latency 8,192 token
    ' gemini-2.0-flash          Multimodal, live API experimental, agentic features 8,192 token
    ' gemini-2.5-flash-lite     High throughput, cost-efficient, multimodal 65,536 token
    ' gemini-2.5-flash          Balanced price-performance, "thinking" process support 65,536 token
    ' gemini-2.5-pro            Balanced price-performance, "thinking" process support 65,535 token

    Public Function TestApiConnectivity(Optional ByRef cMessage As String = "") As Boolean
        Try
            cMessage &= vbCrLf & "Testing API connectivity..."

            Dim testUrl As String = $"{cGoogleBaseUrl}/models?key={cGoogleApiKey}"
            Dim request As HttpWebRequest = CType(WebRequest.Create(testUrl), HttpWebRequest)
            request.Method = "GET"

            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                If response.StatusCode = HttpStatusCode.OK Then
                    Return True
                End If
            End Using
            Return False

        Catch ex As System.Exception
            cMessage &= vbCrLf & "API connectivity test failed: " & ex.Message
            ErrDisp(ex.Message, "TestApiConnectivity", , , ex)
            Return False
        End Try
    End Function

    Public Function ConvertJsonToHtmlWithGemini(jsonData As String) As String
        ' we do not use this method anymore
        Try
            Dim requestPayload As String = ""
            Dim apiUrl As String = $"{cGoogleBaseUrl}/models/{cGoogleHTMLModel}:generateContent?key={cGoogleApiKey}"

            ' Create the prompt for Gemini
            Dim prompt As String = "Convert the following JSON data to a well-formatted, human-readable HTML page." +
                                   "Include proper styling with CSS, create tables for tabular data, and make it visually appealing." +
                                   "Use Bootstrap or inline CSS for styling." +
                                   "Return ONLY valid HTML without markdown formatting." +
                                   "Do Not summarize Or omit any details—extract everything verbatim where possible." +
                                   "Output only the extracted information strictly In the following JSON Structure, With no additional text, explanations, Or deviations." +
                                   "Ensure the HTML Is valid And complete." +
                                   "Trim currency/units/symbols; preserve units separately only If present As labels.Dates : Keep as found (string)." +
                                   "Whitespace and repeats: Normalize whitespace; If a field repeats In the HTML, prefer the most specific/structured occurrence. " +
                                   "Here Is the JSON data: " + vbCrLf + jsonData

            requestPayload = "{" +
                            """contents"": [{" +
                                """parts"": [{" +
                                    """text"": """ + prompt.Replace("""", "\""").Replace(vbCrLf, "\n") + """" +
                                "}]" +
                            "}]," +
                            """generationConfig"": {" +
                                """temperature"": 0.7," +
                                """maxOutputTokens"": 65535" +
                            "}" +
                        "}"

            ' Create HTTP request
            Dim request As HttpWebRequest = CType(WebRequest.Create(apiUrl), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/json"
            request.Accept = "application/json"  ' Use Accept property instead of Headers.Add

            ' Write request body
            Using streamWriter As New StreamWriter(request.GetRequestStream())
                streamWriter.Write(requestPayload)
            End Using

            ' Get response
            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                Using streamReader As New StreamReader(response.GetResponseStream())
                    Dim responseText As String = streamReader.ReadToEnd()

                    ' Parse the Gemini response JSON to extract the HTML content with error handling
                    Dim responseJson As JObject = JObject.Parse(responseText)

                    ' Add robust null checking for the JSON structure
                    If responseJson("candidates") Is Nothing OrElse
                       responseJson("candidates").Count = 0 OrElse
                       responseJson("candidates")(0)("content") Is Nothing OrElse
                       responseJson("candidates")(0)("content")("parts") Is Nothing OrElse
                       responseJson("candidates")(0)("content")("parts").Count = 0 OrElse
                       responseJson("candidates")(0)("content")("parts")(0)("text") Is Nothing Then

                        ' Log the actual response for debugging
                        Dim errorMsg As String = "Invalid Gemini API response structure. Response: " + responseText
                        ErrDisp(errorMsg, "ConvertJsonToHtmlWithGemini")
                        Return "<html><body><h2>Error: Invalid API response structure</h2><p>" + errorMsg + "</p></body></html>"
                    End If

                    Dim htmlContent As String = responseJson("candidates")(0)("content")("parts")(0)("text").ToString()

                    ' Clean up the HTML content (remove markdown code blocks if present)
                    htmlContent = htmlContent.Replace("```html", "").Replace("```", "").Trim()

                    Return htmlContent
                End Using
            End Using

        Catch ex As System.Exception
            ErrDisp(ex.Message, "ConvertJsonToHtmlWithGemini", , , ex)
            Return "<html><body><h2>Error converting JSON to HTML</h2><p>" + ex.Message + "</p></body></html>"
        End Try
    End Function

    Public Sub LoadGoogleAI(nCase As Integer)

        Try
            Dim oSQL As New SQLServerClass

            oSQL.init(nCase)
            oSQL.OpenConn()
            cGoogleApiKey = oSQL.GetSysPar("googleapikey").ToString.Trim()
            cGoogleModel = "gemini-2.5-pro" ' oSQL.GetSysPar("googlemodel").ToString.Trim()
            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp(ex.Message, "LoadGoogleAI", , , ex)
        End Try
    End Sub

End Module
