Option Explicit On
Option Strict On

Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports Microsoft.SqlServer.Server

Module UtilLogFile

    Public Sub AddEventLog(cMessage As String, Optional nCase As Integer = 1)
        Try
            Dim oEventLog As New System.Diagnostics.EventLog
            Dim cSource As String = "WinTexCRSIrsaliye"
            Dim cLog As String = "WinTexCRSIrsaliyeLog"

            If cMessage.Trim = "" Then Exit Sub

            If Not System.Diagnostics.EventLog.SourceExists(cSource) Then
                System.Diagnostics.EventLog.CreateEventSource(cSource, cLog)
            End If

            oEventLog.Source = cSource
            oEventLog.Log = cLog

            cMessage = "eIrsaliyeCRS versiyon : " + My.Application.Info.Version.ToString.Trim + vbCrLf +
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

    Public Sub CreateLog(Optional ByVal cLogFileName As String = "CRSIrsaliyeLog", Optional ByVal cLogMessage As String = "")
        Try
            Dim cTodaysDate As String = String.Format("{0:dd_MM_yyyy}", DateTime.Now)
            Dim cFileName As String = "C:\wintex\WinTexCRSLogs\" + cLogFileName + "_" + cTodaysDate + ".txt"
            Dim cMessage As String = ""

            cMessage = "eirsaliyecrs versiyon : " + My.Application.Info.Version.ToString.Trim + vbCrLf +
                        "Server : " + oConnection.cServer.Trim + " " + vbCrLf +
                        "Database : " + oConnection.cDatabase.Trim + " " + vbCrLf +
                        "User : " + oConnection.cUser.Trim + " " + vbCrLf +
                        "Tarih / Saat : " + Now.ToString + " " + vbCrLf +
                        "Log : " + cLogMessage.Trim + vbCrLf

            Debug.WriteLine("WinTexDLL Log : " + cMessage)

            'If oConnection.lWinTexDllLog Then
            If (My.Computer.FileSystem.DirectoryExists("C:\wintex\WinTexCRSLogs") = False) Then
                My.Computer.FileSystem.CreateDirectory("C:\wintex\WinTexCRSLogs")
            End If

            File.AppendAllText(cFileName,
                                   vbCrLf +
                                   "-------------------------------------------------------" + vbCrLf +
                                   cMessage + vbCrLf)
            'End If

        Catch ex As Exception
            ' do nothing
        End Try
    End Sub

    Public Sub ErrDisp(Optional ByVal cErrorMessage As String = "", Optional ByVal cFormName As String = "", Optional ByVal cSQL As String = "", Optional ByVal lShowMessage As Boolean = False, Optional oEx As Exception = Nothing)
        Try
            Dim cTodaysDate As String = String.Format("{0:dd_MM_yyyy}", DateTime.Now)
            Dim cFileName As String = "C:\wintex\WinTexCRSError\CRSIrsaliyeError" + "_" + cTodaysDate + ".txt"
            Dim cMessage As String = ""

            cMessage = "eirsaliyecrs versiyon : " + My.Application.Info.Version.ToString.Trim + vbCrLf +
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

            Debug.WriteLine("eirsaliyecrs Hata : " + cMessage)

            If lShowMessage Then
                MsgBox("eirsaliyecrs Hata : " + cMessage)
            End If

            'If oConnection.lWinTexDllError Then
            If (My.Computer.FileSystem.DirectoryExists("C:\wintex\WinTexCRSError") = False) Then
                My.Computer.FileSystem.CreateDirectory("C:\wintex\WinTexCRSError")
            End If

            File.AppendAllText(cFileName,
                                   vbCrLf +
                                   "-------------------------------------------------------" + vbCrLf +
                                   cMessage + vbCrLf)
            'End If

        Catch ex As Exception
            ' do nothing
        End Try
    End Sub

End Module
