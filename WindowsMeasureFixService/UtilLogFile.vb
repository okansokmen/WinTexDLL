Option Explicit On
Option Strict On

Imports System.IO

Module UtilLogFile

    Public cVersion As String = My.Application.Info.Version.ToString.Trim

    Public Sub CreateLog(Optional ByVal cLogMessage As String = "")
        Try
            Dim cTodaysDate As String = String.Format("{0:dd_MM_yyyy}", DateTime.Now)
            Dim cFileName As String = "C:\wintex\WinTexMeasurefixLogs\WinTexMeasurefixLog_" + cTodaysDate + ".txt"
            Dim cMessage As String = ""

            cMessage = vbCrLf +
                        "-------------------------------------------------------" + vbCrLf +
                        "WinTexMeasurefix versiyon : " + cVersion + vbCrLf +
                        "Tarih / Saat : " + Now.ToString + " " + vbCrLf +
                        "Log : " + cLogMessage.Trim

            oService1.CreateLog(cMessage, 1)

            If (My.Computer.FileSystem.DirectoryExists("C:\wintex\WinTexMeasurefixLogs") = False) Then
                My.Computer.FileSystem.CreateDirectory("C:\wintex\WinTexMeasurefixLogs")
            End If

            File.AppendAllText(cFileName, cMessage + vbCrLf)

        Catch ex As Exception
            ' do nothing
        End Try
    End Sub

    Public Sub ErrDisp(Optional ByVal cErrorMessage As String = "", Optional ByVal cFormName As String = "", Optional ByVal cSQL As String = "", Optional ByVal lShowMessage As Boolean = False, Optional oEx As Exception = Nothing)
        Try
            Dim cMessage As String = ""
            Dim cTodaysDate As String = String.Format("{0:dd_MM_yyyy}", DateTime.Now)
            Dim cFileName As String = "C:\wintex\WinTexMeasurefixErrors\WinTexMeasurefixError_" + cTodaysDate + ".txt"

            cMessage = vbCrLf +
                        "-------------------------------------------------------" + vbCrLf +
                        "WinTexMeasurefix versiyon : " + cVersion + vbCrLf +
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
                cMessage = cMessage + cErrorMessage
            End If

            oService1.CreateLog(cMessage, 2)

            If (My.Computer.FileSystem.DirectoryExists("C:\wintex\WinTexMeasurefixErrors") = False) Then
                My.Computer.FileSystem.CreateDirectory("C:\wintex\WinTexMeasurefixErrors")
            End If

            File.AppendAllText(cFileName, cMessage + vbCrLf)

        Catch ex As Exception
            ' do nothing
        End Try
    End Sub

End Module
