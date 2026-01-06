Option Explicit On
Option Strict On

Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.SqlServer.Server
Imports System.Deployment

Module UtilLogFile

    Public oEventLog As System.Diagnostics.EventLog
    Public cServiceName As String = "UyumGatewayService"
    Public cVersion As String = My.Application.Info.Version.ToString.Trim
    Public cSrvTipi As String = "NORMAL"

    Public Sub CreateLog(Optional ByVal cLogFileName As String = "WinTexLog", Optional ByVal cLogMessage As String = "")
        Try
            Dim cTodaysDate As String = String.Format("{0:dd_MM_yyyy}", DateTime.Now)
            Dim cFileName As String = "C:\wintex\UyumGatewayServiceLog\" + cServiceName + "_" + cTodaysDate + ".txt"
            Dim cMessage As String = ""

            If cLogMessage.Trim = "" Then Exit Sub

            cMessage = "UyumGatewayService LOG Tarih / Saat : " + Now.ToString + " " + vbCrLf +
                        "Servis Versiyon : " + cVersion + vbCrLf +
                        "Servis Tipi : " + cSrvTipi + vbCrLf +
                        "Log : " + cLogMessage.Trim + vbCrLf

            If (My.Computer.FileSystem.DirectoryExists("C:\wintex\UyumGatewayServiceLog") = False) Then
                My.Computer.FileSystem.CreateDirectory("C:\wintex\UyumGatewayServiceLog")
            End If

            File.AppendAllText(cFileName, cMessage)

            oEventLog.WriteEntry(cMessage.Trim)

        Catch ex As Exception
            ' do nothing
            'MsgBox("CreateLog hatası : " + ex.Message)
        End Try
    End Sub

    Public Sub ErrDisp(oEx As Exception, Optional ByVal cFormName As String = "", Optional ByVal cSQL As String = "", Optional ByVal lShowMessage As Boolean = False)
        Try
            Dim cMessage As String = ""
            Dim cTodaysDate As String = String.Format("{0:dd_MM_yyyy}", DateTime.Now)
            Dim cFileName As String = "C:\wintex\UyumGatewayServiceError\" + cServiceName + "_" + cTodaysDate + ".txt"


            cMessage = "UyumGatewayService Hata Tarih / Saat : " + Now.ToString + " " + vbCrLf +
                        "Servis Versiyon : " + cVersion + vbCrLf +
                        "Servis Tipi : " + cSrvTipi + vbCrLf +
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

            'Debug.WriteLine("UyumGatewayService Hata : " + cMessage)

            'If lShowMessage Then
            '    MsgBox("UyumGatewayService Hata : " + cMessage)
            'End If

            If (My.Computer.FileSystem.DirectoryExists("C:\wintex\UyumGatewayServiceError") = False) Then
                My.Computer.FileSystem.CreateDirectory("C:\wintex\UyumGatewayServiceError")
            End If

            File.AppendAllText(cFileName,
                                   vbCrLf +
                                   "-------------------------------------------------------" + vbCrLf +
                                   "Servis Versiyon : " + cVersion + vbCrLf +
                                   "Servis Tipi : " + cSrvTipi + vbCrLf +
                                   cMessage + vbCrLf)

            oEventLog.WriteEntry(cMessage.Trim)

        Catch ex As Exception
            ' do nothing
            ' MsgBox("WinTexDLL Err On Err : " + ex.Message + vbCrLf + "Original Error : " + cErrorMessage)
        End Try
    End Sub

End Module
