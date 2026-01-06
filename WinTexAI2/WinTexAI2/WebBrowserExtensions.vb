Imports System.Runtime.CompilerServices
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Text

Module WebBrowserExtensions

    <Extension>
    Public Async Function LoadHtmlAsync(wb As WebBrowser, html As String, Optional timeoutMs As Integer = 8000) As Task
        Try
            ' Ensure we are on UI thread
            If wb.InvokeRequired Then
                Dim tcs As New TaskCompletionSource(Of Boolean)()
                wb.BeginInvoke(Async Sub()
                                   Try
                                       Await LoadHtmlAsync(wb, html, timeoutMs)
                                       tcs.TrySetResult(True)
                                   Catch ex As Exception
                                       tcs.TrySetException(ex)
                                   End Try
                               End Sub)
                Await tcs.Task
                Return
            End If

            ' Make sure control handle exists
            If Not wb.IsHandleCreated Then wb.CreateControl()

            ' Ensure a clean about:blank is fully loaded
            Await EnsureBlankAsync(wb, timeoutMs)

            ' Now write the HTML into a new document (prevents partial renders)
            If wb.Document Is Nothing Then
                wb.DocumentText = "<!doctype html><html><head><meta charset=""utf-8""></head><body></body></html>"
                Await WaitReadyAsync(wb, timeoutMs)
            End If

            wb.Document.OpenNew(True)
            wb.Document.Write(EnsureHead(html))
            ' Close the doc to flush the parser
            Try
                wb.Document.InvokeScript("eval", New Object() {"document.close()"})
            Catch
                ' ignore if scripting disabled
            End Try

        Catch ex As System.Exception
            ErrDisp(ex.Message, "LoadHtmlAsync", , , ex)
        End Try
    End Function

    ' Make sure there is <head> with charset and (optional) CSS/BASE you need
    Private Function EnsureHead(html As String) As String
        Try
            If String.IsNullOrWhiteSpace(html) Then
                Return "<!doctype html><html><head><meta charset=""utf-8""></head><body></body></html>"
            End If
            Dim s = html.Trim()
            If Not s.ToLower().Contains("<head") Then
                ' Wrap minimal shell if only body fragment was given
                Dim sb As New StringBuilder()
                sb.AppendLine("<!doctype html>")
                sb.AppendLine("<html>")
                sb.AppendLine("<head><meta charset=""utf-8""></head>")
                sb.AppendLine("<body>")
                sb.AppendLine(s)
                sb.AppendLine("</body></html>")
                Return sb.ToString()
            End If
            ' Ensure charset to avoid "blank" due to encoding issues
            If Not s.ToLower().Contains("charset=") Then
                s = s.Replace("<head", "<head><meta charset=""utf-8"">")
            End If
            Return s

        Catch ex As System.Exception
            ErrDisp(ex.Message, "EnsureHead", , , ex)
            Return "<!doctype html><html><head><meta charset=""utf-8""></head><body></body></html>"
        End Try
    End Function

    Private Async Function EnsureBlankAsync(wb As WebBrowser, timeoutMs As Integer) As Task
        Try
            ' If we already have a doc and it's complete, we can overwrite immediately
            If wb.ReadyState = WebBrowserReadyState.Complete AndAlso wb.Document IsNot Nothing Then Return

            Dim tcs As New TaskCompletionSource(Of Boolean)()
            Dim handler As WebBrowserDocumentCompletedEventHandler = Nothing
            Dim timedOut As Boolean = False

            handler =
                Sub(s, e)
                    Try
                        ' Only proceed when about:blank has completed
                        If e.Url IsNot Nothing AndAlso e.Url.ToString().StartsWith("about:blank", StringComparison.OrdinalIgnoreCase) AndAlso wb.ReadyState = WebBrowserReadyState.Complete Then
                            RemoveHandler wb.DocumentCompleted, handler
                            tcs.TrySetResult(True)
                        End If
                    Catch ex As System.Exception
                        ErrDisp(ex.Message, "EnsureBlankAsync - Handler", , , ex)
                    End Try
                End Sub

            AddHandler wb.DocumentCompleted, handler
            wb.Stop()
            wb.AllowNavigation = True
            wb.ScriptErrorsSuppressed = True
            wb.Navigate("about:blank")

            Dim delayTask = Task.Delay(timeoutMs).ContinueWith(Sub() timedOut = True)
            Await Task.WhenAny(tcs.Task, delayTask)

            If timedOut Then
                RemoveHandler wb.DocumentCompleted, handler
                Throw New TimeoutException("Timed out waiting for about:blank.")
            End If

        Catch ex As System.Exception
            ErrDisp(ex.Message, "EnsureBlankAsync", , , ex)
        End Try
    End Function

    Private Async Function WaitReadyAsync(wb As WebBrowser, timeoutMs As Integer) As Task
        Try
            If wb.ReadyState = WebBrowserReadyState.Complete Then Return

            Dim tcs As New TaskCompletionSource(Of Boolean)()
            Dim handler As WebBrowserDocumentCompletedEventHandler = Nothing
            Dim timedOut As Boolean = False

            handler =
                Sub(s, e)
                    Try
                        If wb.ReadyState = WebBrowserReadyState.Complete Then
                            RemoveHandler wb.DocumentCompleted, handler
                            tcs.TrySetResult(True)
                        End If
                    Catch ex As System.Exception
                        ErrDisp(ex.Message, "WaitReadyAsync - Handler", , , ex)
                    End Try
                End Sub

            AddHandler wb.DocumentCompleted, handler

            Dim delayTask = Task.Delay(timeoutMs).ContinueWith(Sub() timedOut = True)
            Await Task.WhenAny(tcs.Task, delayTask)

            If timedOut Then
                RemoveHandler wb.DocumentCompleted, handler
                Throw New TimeoutException("Timed out waiting for WebBrowser ready state.")
            End If

        Catch ex As System.Exception
            ErrDisp(ex.Message, "WaitReadyAsync", , , ex)
        End Try
    End Function

End Module