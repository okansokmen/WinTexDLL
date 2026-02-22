Option Explicit On
Option Strict On

Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports Microsoft.SqlServer.Server
Imports System.Deployment

' Visual Studio 2026 Enterprise License Key   : BW43B-4PNFP-V7GB7-36MBD-3PJTH
' Visual Studio 2026 Professional License Key : DDBD6-NPCDC-2FH2P-PDP9M-G3F6W

Module UtilLogFile
    Public cVersion As String = My.Application.Info.Version.ToString.Trim

    Public Sub ErrDisp(oEx As Exception, Optional ByVal cFormName As String = "", Optional ByVal cSQL As String = "", Optional ByVal lShowMessage As Boolean = False)
        Try
            Dim cMessage As String = ""
            Dim cTodaysDate As String = String.Format("{0:dd_MM_yyyy}", DateTime.Now)
            Dim cFileName As String = "C:\wintexservice\WinTexServiceError\WinTexServiceError" + "_" + cTodaysDate + ".txt"

            cMessage = "Error Date / Time : " + Now.ToString + " " + vbCrLf +
                        IIf(cFormName.Trim = "", "", "Module : " + cFormName.Trim + vbCrLf).ToString +
                        IIf(cSQL.Trim = "", "", "SQL : " + cSQL + vbCrLf).ToString

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

            oWinTexServiceMain.CreateLog(cMessage, 2)

            'Debug.WriteLine("WinTexDLL Hata : " + cMessage)

            'If lShowMessage Then
            '    MsgBox("WinTexDLL Hata : " + cMessage)
            'End If

            If (My.Computer.FileSystem.DirectoryExists("C:\wintexservice\WinTexServiceError") = False) Then
                My.Computer.FileSystem.CreateDirectory("C:\wintexservice\WinTexServiceError")
            End If

            File.AppendAllText(cFileName,
                                   vbCrLf +
                                   "-------------------------------------------------------" + vbCrLf +
                                   "Servis Versiyon : " + cVersion + vbCrLf +
                                   cMessage + vbCrLf)
        Catch ex As Exception
            ' do nothing
        End Try
    End Sub

    Public Sub CreateLogFile(Optional ByVal cLogMessage As String = "")
        Try
            Dim cTodaysDate As String = String.Format("{0:dd_MM_yyyy}", DateTime.Now)
            Dim cFileName As String = "C:\wintexservice\WinTexServiceLog\WinTexServiceLog" + "_" + cTodaysDate + ".txt"

            If cLogMessage.Trim = "" Then Exit Sub

            cLogMessage = "Log Date / Time : " + Now.ToString + " " + vbCrLf +
                        cLogMessage.Trim

            If (My.Computer.FileSystem.DirectoryExists("C:\wintexservice\WinTexServiceLog") = False) Then
                My.Computer.FileSystem.CreateDirectory("C:\wintexservice\WinTexServiceLog")
            End If

            File.AppendAllText(cFileName,
                                   vbCrLf +
                                   "-------------------------------------------------------" + vbCrLf +
                                   "Servis Versiyon : " + cVersion + vbCrLf +
                                   cLogMessage + vbCrLf)
        Catch ex As Exception
            ' do nothing
        End Try
    End Sub
End Module
