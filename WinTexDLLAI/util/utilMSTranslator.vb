Option Explicit On
Option Strict On

Imports System
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Xml.Linq

Module utilMSTranslator

    <Serializable()> Public Class AdmAccessToken

        Public access_token As String
        Public token_type As String
        Public expires_in As String
        Public scope As String

    End Class

    Public Function MSTranslate(txtToTranslate As String, Optional cInputLanguage As String = "en", Optional cOutputLanguage As String = "tr") As String
        ' aR , Arabic
        ' bg , Bulgarian
        ' ca , Catalan
        ' zh -CHS, Chinese Simplified
        ' zh -CHT, Chinese Traditional
        ' cs , Czech
        ' da , Danish
        ' nl , Dutch
        ' en , English
        ' et , Estonian
        ' fi , Finnish
        ' fr , French
        ' de , German
        ' el , Greek
        ' HT , Haitian Creole
        ' he , Hebrew
        ' hi , Hindi
        ' mww, Hmong Daw
        ' hu , Hungarian
        ' ID , Indonesian
        ' iT , Italian
        ' ja , Japanese
        ' tlh , Klingon
        ' tlh -Qaak, Klingon(pIqaD)
        ' ko , Korean
        ' lv , Latvian
        ' lt , Lithuanian
        ' ms , Malay
        ' MT , Maltese
        ' no , Norwegian
        ' fa , Persian
        ' pl , Polish
        ' pt , Portuguese
        ' ro , Romanian
        ' ru , Russian
        ' sk , Slovak
        ' sl , Slovenian
        ' ES , Spanish
        ' sv , Swedish
        ' th , Thai
        ' tr , Turkish
        ' uk , Ukrainian
        ' ur , Urdu
        ' vi , Vietnamese
        ' cy , Welsh

        MSTranslate = ""

        Try
            Dim clientID As String = "okansokmen"
            Dim clientSecret As String = "q/MXsQ6KbpxdIrEtIknAb6K7cnt6d3E2JagBwihVY00="
            Dim strTranslatorAccessURI As String = "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13"
            Dim strRequestDetails As String = String.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&scope=http://api.microsofttranslator.com", _
                                                             System.Web.HttpUtility.UrlEncode(clientID), HttpUtility.UrlEncode(clientSecret))
            Dim webRequest As System.Net.WebRequest = System.Net.WebRequest.Create(strTranslatorAccessURI)
            webRequest.ContentType = "application/x-www-form-urlencoded"
            webRequest.Method = "POST"
            Dim bytes() As Byte = System.Text.Encoding.ASCII.GetBytes(strRequestDetails)
            webRequest.ContentLength = bytes.Length

            Using outputStream As System.IO.Stream = webRequest.GetRequestStream()
                outputStream.Write(bytes, 0, bytes.Length)
            End Using
            Dim webResponse As System.Net.WebResponse = webRequest.GetResponse()
            ' Make sure you add a reference to System.Runtime.Serialization here
            Dim AdmToken As New AdmAccessToken
            Dim serializer As System.Runtime.Serialization.Json.DataContractJsonSerializer = New System.Runtime.Serialization.Json.DataContractJsonSerializer(AdmToken.GetType())
            Dim token As AdmAccessToken = DirectCast(serializer.ReadObject(webResponse.GetResponseStream()), AdmAccessToken)
            Dim headerValue As String = "Bearer " + token.access_token

            Dim uri As String = "http://api.microsofttranslator.com/v2/Http.svc/Translate?text=" + HttpUtility.UrlEncode(txtToTranslate) + "&from=" + cInputLanguage + "&to=" + cOutputLanguage

            Dim translationWebRequest As System.Net.WebRequest = System.Net.WebRequest.Create(uri)
            translationWebRequest.Headers.Add("Authorization", headerValue)
            Dim response As System.Net.WebResponse = Nothing
            response = translationWebRequest.GetResponse()
            Dim stream As System.IO.Stream = response.GetResponseStream()
            Dim encode As System.Text.Encoding = System.Text.Encoding.GetEncoding("utf-8")
            Dim translatedStream As System.IO.StreamReader = New System.IO.StreamReader(stream, encode)
            ' Be sure to add references to System.Xml and System.Xml.Linq
            Dim xTranslation As System.Xml.XmlDocument = New System.Xml.XmlDocument()
            xTranslation.LoadXml(translatedStream.ReadToEnd())

            MSTranslate = xTranslation.InnerText

        Catch ex As Exception
            MsgBox("Translator : " + ex.Message)
        End Try

    End Function
End Module
