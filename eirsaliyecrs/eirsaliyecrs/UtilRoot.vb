Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Threading
Imports System.Drawing
Imports System.Windows.Forms
Imports System.IO
Imports Microsoft.SqlServer.Server
Imports Microsoft.VisualBasic.FileIO.FileSystem

Module UtilRoot


    Public Structure oSQLConn
        Dim cServer As String
        Dim cDatabase As String
        Dim cUser As String
        Dim cPassword As String
        Dim cConnStr As String

        Dim cCRSUser As String
        Dim cCRSPassword As String

        Dim cWinTexUser As String
    End Structure

    Public oConnection As oSQLConn

    'Public Sub GetCRSeIrsaliyeServiceParameters(ByRef cURL As String, ByRef cUsername As String, ByRef cPassword As String)

    '    Try
    '        Dim oSQL As New SQLServerClass

    '        oSQL.OpenConn()

    '        cURL = oSQL.GetSysPar("UrlCrsEirsaliyeService", "https://connect-test.crssoft.com/Services/DespatchIntegration")
    '        cUsername = oSQL.GetSysPar("CrsUsername", "CrsDemo85")
    '        cPassword = oSQL.GetSysPar("CrsPassword", "11223385")

    '        oSQL.CloseConn()
    '        oSQL = Nothing

    '    Catch ex As Exception
    '        ErrDisp("GetCRSeIrsaliyeServiceParameters", "utilroot",,, ex)
    '    End Try

    'End Sub

End Module
