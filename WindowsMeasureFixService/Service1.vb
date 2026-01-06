Imports Dart.Sockets
Imports System.Configuration

Public Class Service1

    Dim server1 As Server
    Dim oMFClients As New List(Of MFClient)

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' Add any initialization after the InitializeComponent() call.
        Me.CanHandlePowerEvent = True
        Me.CanHandleSessionChangeEvent = True
        Me.CanPauseAndContinue = True
        Me.CanShutdown = True
        Me.CanStop = True

        EventLog1 = New System.Diagnostics.EventLog

        If Not System.Diagnostics.EventLog.SourceExists("WintexMFService") Then
            System.Diagnostics.EventLog.CreateEventSource("WintexMFService", "WintexMFServiceLog")
        End If

        EventLog1.Source = "WintexMFService"
        EventLog1.Log = "WintexMFServiceLog"

    End Sub

    Protected Overrides Sub OnStart(ByVal args() As String)

#If DEBUG Then
        System.Diagnostics.Debugger.Launch()
        Do While Not Debugger.IsAttached
            Threading.Thread.Sleep(1000)
        Loop
#End If

        oConnection.cServer = ConfigurationSettings.AppSettings("SERVER")
        oConnection.cDatabase = ConfigurationSettings.AppSettings("DATABASE")
        oConnection.cUser = ConfigurationSettings.AppSettings("USERNAME")
        oConnection.cPassword = ConfigurationSettings.AppSettings("PASSWORD")

        server1 = New Server
        server1.Start(AddressOf server1_NewConnection, 8008, Nothing)

        CreateLog("Service Start")
    End Sub

    Protected Overrides Sub OnStop()
        On Error Resume Next
        CreateLog("Service Stop")
    End Sub

    Protected Overrides Sub OnPause()
        On Error Resume Next
        CreateLog("Service Paused")
    End Sub

    Protected Overrides Sub OnContinue()
        On Error Resume Next
        CreateLog("Service Continue")
    End Sub

    Protected Overrides Sub OnShutdown()
        On Error Resume Next
        CreateLog("Service Shutdown")
    End Sub

    Private Sub server1_NewConnection(ByVal client As Tcp, ByVal state As Object)
        'Read first data sent by client
        On Error Resume Next
        ReadMeasureFix(client)
    End Sub

    Private Sub client_ReadAsyncCompleted(ByVal client As TcpBase, ByVal data As Data, ByVal ex As Exception, ByVal state As Object)
        'Data is null if client is not connected.

        Dim nSelectionId As Integer = 0
        Dim oMFClient As MFClient
        Dim cMessage As String = ""
        Dim cMsg As String = ""
        Dim oDict As New Dictionary(Of String, Object)
        Dim nCnt As Integer = 0

        Try
            If Not (data Is Nothing) Then
                cMessage = System.Text.Encoding.UTF8.GetString(data.Buffer)

                If Not (cMessage Is Nothing) Then
                    CreateLog("MeasureFix (" + client.RemoteEndPoint.ToString.Trim + ") -> WinTex : " + cMessage)

                    cMsg = Replace(cMessage, vbCr, "")
                    cMsg = Replace(cMsg, vbLf, "")
                    cMsg = Replace(cMsg, vbNullChar, "")

                    oDict = New System.Web.Script.Serialization.JavaScriptSerializer().Deserialize(Of Object)(cMsg)

                    Select Case oDict.Item("message").ToString().Trim
                        Case "Connect"
                            oMFClient = New MFClient
                            oMFClient.cMakineNo = oDict.Item("params").item("Table").item("Name").ToString.Trim
                            oMFClient.cSerialNo = oDict.Item("params").item("Table").item("SerialNo").ToString.Trim
                            oMFClient.cRemoteEndPoint = client.RemoteEndPoint.ToString.Trim
                            oMFClients.Add(oMFClient)

                        Case "Login"
                            For Each oMFClient In oMFClients
                                If oMFClient.cRemoteEndPoint = client.RemoteEndPoint.ToString.Trim Then
                                    oMFClient.cUserName = oDict.Item("params").item("Name").ToString.Trim
                                    oMFClient.Login()
                                    Exit For
                                End If
                            Next

                        Case "Logout"
                            For Each oMFClient In oMFClients
                                If oMFClient.cRemoteEndPoint = client.RemoteEndPoint.ToString.Trim Then
                                    oMFClient.cUserName = oDict.Item("params").item("Name").ToString.Trim
                                    oMFClient.Logout()
                                    oMFClients.Remove(oMFClient)
                                    Exit For
                                End If
                            Next

                        Case "ItemId"
                            For Each oMFClient In oMFClients
                                If oMFClient.cRemoteEndPoint = client.RemoteEndPoint.ToString.Trim Then
                                    If oDict.ContainsKey("SelectionId") Then
                                        nSelectionId = CInt(oDict.Item("params").item("SelectionId"))
                                        cMsg = oMFClient.SelectOlcuTablosu(nSelectionId)
                                    Else
                                        oMFClient.cBarkod = oDict.Item("params").item("ItemId").ToString.Trim
                                        cMsg = oMFClient.GetSipModelBeden()
                                    End If
                                    SendtoMeasureFix(client, cMsg)
                                    Exit Sub
                                End If
                            Next

                        Case "MeasureResults"
                            For Each oMFClient In oMFClients
                                If oMFClient.cRemoteEndPoint = client.RemoteEndPoint.ToString.Trim Then
                                    oMFClient.cSiparisNo = oDict.Item("params").item("ItemId").ToString.Trim
                                    If oMFClient.cSiparisNo <> "" Then
                                        oMFClient.WriteMeasures(cMessage)
                                    End If
                                End If
                            Next

                    End Select
                End If
            End If

        Catch ex2 As Exception
            ErrDisp(ex2.Message, "client_ReadAsyncCompleted", cMessage,, ex2)
        Finally
            ReadMeasureFix(client)
        End Try
    End Sub

    Private Sub client_WriteAsyncCompleted(ByVal client As TcpBase, ByVal data As Data, ByVal ex As Exception, ByVal state As Object)
        ' Read for more data.
        On Error Resume Next

        Dim cMessage As String = ""

        If Not (data Is Nothing) Then
            cMessage = System.Text.Encoding.UTF8.GetString(data.Buffer)
            CreateLog("WinTex -> MeasureFix (" + client.RemoteEndPoint.ToString.Trim + ") : " + cMessage)
        End If

        ReadMeasureFix(client)
    End Sub

    Async Function ReadMeasureFix(ByVal client As TcpBase) As Task
        Try
            Dim buffer(1023) As Byte
            client.ReadAsync(buffer, 0, buffer.Length, AddressOf client_ReadAsyncCompleted, Nothing)

        Catch ex As Exception
            ErrDisp(ex.Message, "ReadMeasureFix",,, ex)
        End Try
    End Function

    Async Function SendtoMeasureFix(ByVal client As TcpBase, ByVal cMessage As String) As Task

        Try
            Dim nCnt As Integer = 0

            cMessage = Replace(cMessage, vbNullChar, "")
            cMessage = cMessage + vbCrLf

            Dim buffer() As Byte = System.Text.Encoding.UTF8.GetBytes(cMessage)
            ' tek tırnak karakterini çift tırnak karakterine çevir
            For nCnt = 0 To buffer.GetUpperBound(0)
                If buffer(nCnt) = 39 Then
                    buffer(nCnt) = 34
                End If
            Next

            client.WriteAsync(buffer, 0, buffer.Length, AddressOf client_WriteAsyncCompleted, Nothing)

        Catch ex As Exception
            ErrDisp(ex.Message, "SendtoMeasureFix",,, ex)
        End Try
    End Function

    Async Function CreateLog(cMessage As String, Optional nCase As Integer = 1) As Task
        Try
            If cMessage.Trim = "" Then Exit Function

            cMessage = cMessage + vbCrLf +
                        "Servis Versiyonu : " + cVersion + vbCrLf +
                        "İşlem Parçacağı Adedi : " + Process.GetCurrentProcess.Threads.Count.ToString

            Select Case nCase
                Case 1
                    EventLog1.WriteEntry(cMessage, EventLogEntryType.Information)
                Case 2
                    EventLog1.WriteEntry(cMessage, EventLogEntryType.Error)
            End Select

        Catch ex As Exception
            ' do nothing
        End Try
    End Function

End Class
