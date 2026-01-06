Option Explicit On
Option Strict On
Imports System.IO

Module UtilLogFile

    Public Sub AddEventLog(cMessage As String, Optional nCase As Integer = 1)
        Try
            Dim oEventLog As New System.Diagnostics.EventLog

            If cMessage.Trim = "" Then Exit Sub

            If Not System.Diagnostics.EventLog.SourceExists("WintexAI") Then
                System.Diagnostics.EventLog.CreateEventSource("WintexAI", "WintexAILog")
            End If

            oEventLog.Source = "WintexAI"
            oEventLog.Log = "WintexAILog"

            cMessage = "WintexAI versiyon : " + My.Application.Info.Version.ToString.Trim + vbCrLf +
                        "Server : " + oConnection.cServer.Trim + " " + vbCrLf +
                        "Database : " + oConnection.cDatabase.Trim + " " + vbCrLf +
                        "User : " + oConnection.cUser.Trim + " " + vbCrLf +
                        "Tarih / Saat : " + Now.ToString + " " + vbCrLf +
                        "Mesaj : " + cMessage.Trim

            Select Case nCase
                Case 1
                    oEventLog.WriteEntry(cMessage, EventLogEntryType.Information)
                Case 2
                    oEventLog.WriteEntry(cMessage, EventLogEntryType.Error)
            End Select

            oEventLog = Nothing

        Catch ex As Exception
            ' do nothing
        End Try
    End Sub

    Public Sub CreateLog(Optional ByVal cLogFileName As String = "WintexAILog", Optional ByVal cLogMessage As String = "")
        Try
            Dim cTodaysDate As String = String.Format("{0:dd_MM_yyyy}", DateTime.Now)
            Dim cFileName As String = "C:\wintex\WintexAILogs\" + cLogFileName + "_" + cTodaysDate + ".txt"
            Dim cMessage As String = ""

            cMessage = "WinTexAI versiyon : " + My.Application.Info.Version.ToString.Trim + vbCrLf +
                        "Server : " + oConnection.cServer.Trim + " " + vbCrLf +
                        "Database : " + oConnection.cDatabase.Trim + " " + vbCrLf +
                        "User : " + oConnection.cUser.Trim + " " + vbCrLf +
                        "Tarih / Saat : " + Now.ToString + " " + vbCrLf +
                        "Log : " + cLogMessage.Trim + vbCrLf

            Debug.WriteLine("WintexAILog Log : " + cMessage)

            DosCreateDirectory("C:\wintex\WintexAILogs")

            File.AppendAllText(cFileName,
                                   vbCrLf +
                                   "-------------------------------------------------------" + vbCrLf +
                                   cMessage + vbCrLf)
        Catch ex As Exception
            ' do nothing
        End Try
    End Sub

    Public Sub ErrDisp(Optional ByVal cErrorMessage As String = "", Optional ByVal cFormName As String = "", Optional ByVal cSQL As String = "", Optional lShowMessage As Boolean = False, Optional oEx As Exception = Nothing)
        Try
            Dim cTodaysDate As String = String.Format("{0:dd_MM_yyyy}", DateTime.Now)
            Dim cFileName As String = "C:\wintex\WinTexAIErrors\WinTexAIError" + "_" + cTodaysDate + ".txt"
            Dim cMessage As String = ""

            cMessage = "WinTexAI versiyon : " + My.Application.Info.Version.ToString.Trim + vbCrLf +
                        "Server : " + oConnection.cServer.Trim + " " + vbCrLf +
                        "Database : " + oConnection.cDatabase.Trim + " " + vbCrLf +
                        "User : " + oConnection.cUser.Trim + " " + vbCrLf +
                        "Tarih / Saat : " + Now.ToString + " " + vbCrLf +
                        IIf(cFormName.Trim = "", "", "Module : " + cFormName.Trim + vbCrLf).ToString +
                        IIf(cSQL.Trim = "", "", "SQL : " + cSQL + vbCrLf).ToString

            If oEx Is Nothing Then
                If Err.Description IsNot Nothing Then
                    cMessage = cMessage + "Err Description : " + Err.Description.ToString.Trim + vbCrLf
                End If

                If Err.Source IsNot Nothing Then
                    cMessage = cMessage + "Err Source : " + Err.Source.ToString.Trim + vbCrLf
                End If

                If Err.Number <> 0 Then
                    cMessage = cMessage + "Err Number : " + Err.Number.ToString + vbCrLf
                End If
            Else
                If oEx.Message IsNot Nothing Then
                    cMessage = cMessage + "Err Message : " + oEx.Message.Trim + vbCrLf
                End If

                If oEx.InnerException IsNot Nothing Then
                    cMessage = cMessage + "Err Inner Exception : " + oEx.InnerException.Message.Trim + vbCrLf
                End If

                If oEx.GetBaseException IsNot Nothing Then
                    cMessage = cMessage + "Err Base Excepton : " + oEx.GetBaseException.Message.Trim + vbCrLf
                End If

                If oEx.StackTrace IsNot Nothing Then
                    cMessage = cMessage + "Err Stack Trace : " + oEx.StackTrace.ToString + vbCrLf
                End If
            End If

            If cErrorMessage.Trim <> "" Then
                cMessage = cMessage +
                        cErrorMessage
            End If

            Debug.WriteLine("WinTexAI Hata : " + cMessage)

            If lShowMessage Then
                MsgBox("WinTexAI Hata : " + cMessage)
            End If

            If InStr(cMessage, "404") = 0 Then

                DosCreateDirectory("C:\wintex\WinTexAIErrors")

                File.AppendAllText(cFileName,
                                   vbCrLf +
                                   "-------------------------------------------------------" + vbCrLf +
                                   cMessage + vbCrLf)
            End If

        Catch ex As Exception
            ' do nothing
        End Try
    End Sub

    Public Function DosCreateDirectory(cDirectoryPath As String) As Boolean
        Try
            DosCreateDirectory = False

            If (My.Computer.FileSystem.DirectoryExists(cDirectoryPath) = False) Then
                My.Computer.FileSystem.CreateDirectory(cDirectoryPath)
            End If

            DosCreateDirectory = True

        Catch ex As Exception
            ErrDisp(ex.Message, "DosCreateDirectory", , , ex)
            DosCreateDirectory = False
        End Try
    End Function

End Module
