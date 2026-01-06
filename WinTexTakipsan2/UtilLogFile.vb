Option Explicit On
Option Strict On

Imports System
Imports System.Reflection
Imports System.IO
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.SqlServer.Server

Module UtilLogFile

    Public Sub CreateLog(Optional ByVal cLogFileName As String = "WinTexTakipsanLog", Optional ByVal cLogMessage As String = "")
        Try
            Dim cTodaysDate As String = String.Format("{0:dd_MM_yyyy}", DateTime.Now)
            Dim cFileName As String = "C:\wintex\WinTexTakipsanLogs\" + cLogFileName + "_" + cTodaysDate + ".txt"
            Dim cMessage As String = ""

            cMessage = vbCrLf +
                        "-------------------------------------------------------" + vbCrLf +
                        "WinTexTakipsan versiyon : " + Assembly.GetExecutingAssembly().GetName().Version.ToString + vbCrLf +
                        "Tarih / Saat : " + Now.ToString + " " + vbCrLf +
                        "Log : " + cLogMessage.Trim + vbCrLf

            Console.WriteLine(cMessage)

            If (My.Computer.FileSystem.DirectoryExists("C:\wintex\WinTexTakipsanLogs") = False) Then
                My.Computer.FileSystem.CreateDirectory("C:\wintex\WinTexTakipsanLogs")
            End If

            File.AppendAllText(cFileName, cMessage)

        Catch ex As Exception
            ' do nothing
            'MsgBox("CreateLog hatası : " + ex.Message)
        End Try
    End Sub

    Public Sub ErrDisp(Optional ByVal cErrorMessage As String = "", Optional ByVal cFormName As String = "", Optional ByVal cSQL As String = "",
                       Optional ByVal lShowMessage As Boolean = False, Optional oEx As Exception = Nothing)
        Try
            Dim cMessage As String = ""
            Dim cTodaysDate As String = String.Format("{0:dd_MM_yyyy}", DateTime.Now)

            cMessage = "WinTexTakipsan versiyon : " + Assembly.GetExecutingAssembly().GetName().Version.ToString + vbCrLf +
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

            If lShowMessage Then
                MsgBox(cMessage)
            End If

            Debug.WriteLine("WinTexTakipsan Hata : " + cMessage)
            Console.WriteLine(cMessage)

            If (My.Computer.FileSystem.DirectoryExists("C:\wintex\WinTexTakipsanErrors") = False) Then
                My.Computer.FileSystem.CreateDirectory("C:\wintex\WinTexTakipsanErrors")
            End If

            File.AppendAllText("C:\wintex\WinTexTakipsanErrors\WinTexTakipsanError" + "_" + cTodaysDate + ".txt",
                                   vbCrLf +
                                   "-------------------------------------------------------" + vbCrLf +
                                   cMessage + vbCrLf)

        Catch ex As Exception
            ' do nothing
            ' MsgBox("WinTexDLL Err On Err : " + ex.Message + vbCrLf + "Original Error : " + cErrorMessage)
        End Try
    End Sub

End Module
