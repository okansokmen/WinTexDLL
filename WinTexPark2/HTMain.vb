Option Explicit On

Imports System.Xml
Imports System.Configuration
Imports System.Net
Imports System
Imports System.IO
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Linq

Module HTMain

    Sub Main()
        Try
            Dim cStokUretim As String = "1"
            Dim cFisNo As String = ""
            Dim cPDF As String = "E"
            Dim lPDF As Boolean = False
            Dim nStokUretim As Integer = 1
            Dim args As String() = Environment.GetCommandLineArgs()
            Dim lSonuc As Boolean = False
            Dim oParkIrsaliye As New ParkIrsaliye

            If args.Length = 1 Then

                ' release mode
                'oConnection.cOwner = "jeansco"
                'oConnection.cServer = "10.40.0.252"
                'oConnection.cDatabase = "jeansco"
                'oConnection.cUser = "wintex"
                'oConnection.cPassword = "wintex"
                'cStokUretim = "1"
                'cFisNo = "0000232947"
                'cPDF = "E"

                'debug mode
                oConnection.cOwner = "jeansco"
                oConnection.cServer = "monster"
                oConnection.cDatabase = "jeansco"
                oConnection.cUser = "sa"
                oConnection.cPassword = "Hayabusa1024"
                cStokUretim = "2"
                cFisNo = "0000001754"
                cPDF = "E"

                MsgBox("debug mode " + vbCrLf +
                        "server : " + oConnection.cServer + vbCrLf +
                        "database : " + oConnection.cDatabase + vbCrLf +
                        "user : " + oConnection.cUser + vbCrLf +
                        "password : " + oConnection.cPassword + vbCrLf +
                        "stokuretim : " + cStokUretim + vbCrLf +
                        "fisno : " + cFisNo + vbCrLf +
                        "pdf : " + cPDF)
            Else
                oConnection.cOwner = "jeansco"
                oConnection.cServer = args(1).ToString.Trim
                oConnection.cDatabase = args(2).ToString.Trim
                oConnection.cUser = args(3).ToString.Trim
                oConnection.cPassword = args(4).ToString.Trim
                cStokUretim = args(5).ToString.Trim
                cFisNo = args(6).ToString.Trim
                cPDF = args(7).ToString.Trim

                'MsgBox("real mode " + vbCrLf +
                '        "server : " + args(1).ToString.Trim + vbCrLf +
                '        "database : " + args(2).ToString.Trim + vbCrLf +
                '        "user : " + args(3).ToString.Trim + vbCrLf +
                '        "password : " + args(4).ToString.Trim + vbCrLf +
                '        "stokuretim : " + args(5).ToString.Trim + vbCrLf +
                '        "fisno : " + args(6).ToString.Trim + vbCrLf +
                '        "pdf : " + args(7).ToString.Trim)
            End If

            If cStokUretim.Trim = "1" Then
                nStokUretim = 1
            Else
                nStokUretim = 2
            End If

            If cPDF.Trim = "E" Then
                lPDF = True
            Else
                lPDF = False
            End If

            oConnection.cConnStr = "Data Source=" + oConnection.cServer + ";" +
                                   "Initial Catalog=" + oConnection.cDatabase + ";" +
                                   "uid=" + oConnection.cUser + ";" +
                                   "pwd=" + oConnection.cPassword + ""

            If oParkIrsaliye.init() Then
                lSonuc = oParkIrsaliye.SendEIrsaliye(nStokUretim, cFisNo, lPDF)
            End If

        Catch ex As Exception
            ErrDisp("Main: " & ex.Message, "Program",,, ex)
        End Try
    End Sub

End Module
