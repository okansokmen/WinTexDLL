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

    Public Function GetErrFilename(Optional ByVal cLogFileName As String = "WinTexLog") As String

        GetErrFilename = ""

        Try
            Dim cTodaysDate As String = String.Format("{0:dd_MM_yyyy}", DateTime.Now)
            GetErrFilename = "C:\wintex\WinTexDllError\WinTexDLLError" + "_" + cTodaysDate + ".txt"

        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Function

    Public Sub CreateLog(Optional ByVal cLogFileName As String = "WinTexLog", Optional ByVal cLogMessage As String = "")
        Try
            Dim cTodaysDate As String = String.Format("{0:dd_MM_yyyy}", DateTime.Now)
            Dim cFileName As String = "C:\wintex\WinTexDllLogs\" + cLogFileName + "_" + cTodaysDate + ".txt"
            Dim cMessage As String = ""

            cMessage = vbCrLf +
                        "-------------------------------------------------------" + vbCrLf +
                        "Program: " + Application.ProductName.ToString + vbCrLf +
                        "Tarih / Saat : " + Now.ToString + " " + vbCrLf +
                        "Log : " + cLogMessage.Trim + vbCrLf

            If (My.Computer.FileSystem.DirectoryExists("C:\wintex\WinTexDllLogs") = False) Then
                My.Computer.FileSystem.CreateDirectory("C:\wintex\WinTexDllLogs")
            End If

            File.AppendAllText(cFileName, cMessage)

        Catch ex As Exception
            ' do nothing
            'MsgBox("CreateLog hatası : " + ex.Message)
        End Try
    End Sub

    Public Sub ErrDisp(oEx As Exception, Optional ByVal cFormName As String = "", Optional ByVal cSQL As String = "", Optional ByVal lShowMessage As Boolean = False)
        Try
            Dim cMessage As String = ""
            Dim cTodaysDate As String = String.Format("{0:dd_MM_yyyy}", DateTime.Now)

            cMessage = "Tarih / Saat : " + Now.ToString + " " + vbCrLf +
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

            Debug.WriteLine("WinTexDLL Hata : " + cMessage)

            If lShowMessage Then
                MsgBox("WinTexDLL Hata : " + cMessage)
            End If

            If (My.Computer.FileSystem.DirectoryExists("C:\wintex\WinTexDllError") = False) Then
                My.Computer.FileSystem.CreateDirectory("C:\wintex\WinTexDllError")
            End If

            File.AppendAllText("C:\wintex\WinTexDllError\WinTexDLLError" + "_" + cTodaysDate + ".txt",
                                   vbCrLf +
                                   "-------------------------------------------------------" + vbCrLf +
                                    "Program: " + Application.ProductName.Trim + vbCrLf +
                                   cMessage + vbCrLf)

        Catch ex As Exception
            ' do nothing
            ' MsgBox("WinTexDLL Err On Err : " + ex.Message + vbCrLf + "Original Error : " + cErrorMessage)
        End Try
    End Sub

End Module
