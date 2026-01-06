Option Explicit On
Option Strict On

Imports System.Configuration
Imports System.Timers
Imports System.Diagnostics
Imports System.Data.SqlClient
Imports System.Deployment

Module Main

    Public Sub Main()

        Dim cServer As String = ""
        Dim cDatabase As String = ""
        Dim cUsername As String = ""
        Dim cPassword As String = ""

        Dim cPath As String = System.Reflection.Assembly.GetExecutingAssembly().Location
        Dim aLines() As String
        Dim pos As Integer = 0
        Dim resstr As String = ""
        Dim lstr As String = ""
        Dim rstr As String = ""

        Try

            cPath = Strings.Replace(cPath, "WinTexSchedule.exe", "config.wtx")
            aLines = IO.File.ReadAllLines(cPath)

            For Each cLine In aLines
                lstr = ""
                rstr = ""
                resstr = cLine
                If resstr.Trim <> "" Then
                    resstr = resstr.Trim
                    pos = InStr(resstr, "=")
                    If pos > 0 Then
                        ' EXTRACT left and right
                        lstr = LCase$(Trim$(Left(resstr, pos - 1)))
                        rstr = Trim$(Right(resstr, Len(resstr) - pos))
                        Select Case lstr
                            Case "server"
                                If cServer = "" Then
                                    cServer = rstr.Trim
                                End If
                            Case "database"
                                If cDatabase = "" Then
                                    cDatabase = rstr.Trim
                                End If
                            Case "user"
                                If cUsername = "" Then
                                    cUsername = rstr.Trim
                                End If
                            Case "password"
                                If cPassword = "" Then
                                    cPassword = rstr.Trim
                                End If
                        End Select

                    End If
                End If
            Next

            'cServer = ConfigurationManager.AppSettings("SERVER")
            'cDatabase = ConfigurationManager.AppSettings("DATABASE")
            'cUsername = ConfigurationManager.AppSettings("USERNAME")
            'cPassword = ConfigurationManager.AppSettings("PASSWORD")

            If init(cServer, cDatabase, cUsername, cPassword) Then
                Takvim()
            End If

        Catch ex As Exception
            ErrDisp("Maint : " + ex.Message + " " + cServer.Trim + " " + cDatabase.Trim + " " + cUsername.Trim + " " + cPassword.Trim,,,, ex)
        End Try
    End Sub

    Public Function init(Optional cServer As String = "", Optional cDatabase As String = "", Optional cUser As String = "sa", Optional cPassword As String = "",
                         Optional cWinTexUser As String = "") As Boolean
        ' init database connection
        Dim cSQL As String = ""

        init = False

        Try
            oConnection.cServer = cServer.Trim
            oConnection.cDatabase = cDatabase.Trim
            oConnection.cUser = cUser.Trim
            oConnection.cPassword = cPassword.Trim
            oConnection.cWinTexUser = cWinTexUser

            oConnection.cConnStr = "Data Source=" + oConnection.cServer + ";" +
                                   "Initial Catalog=" + oConnection.cDatabase + ";" +
                                   "uid=" + oConnection.cUser + ";" +
                                   "pwd=" + oConnection.cPassword + ""
            init = True

        Catch ex As Exception
            init = False
            ErrDisp("SQL Connect : " + ex.Message + " " + cServer.Trim + " " + cDatabase.Trim + " " + cUser.Trim + " " + cPassword.Trim,,,, ex)
        End Try
    End Function

    Public Sub Takvim(Optional cFilter As String = "")
        Try
            Dim oTakvim As New Takvim
            oTakvim.init(cFilter)

        Catch ex As Exception
            ErrDisp("Takvim : " + ex.Message,,,, ex)
        End Try
    End Sub

End Module
