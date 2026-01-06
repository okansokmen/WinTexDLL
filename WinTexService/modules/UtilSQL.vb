Option Explicit On
Option Strict On

Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.SqlServer.Server

Module UtilSQL
    Public Function SQLWriteDecimal(ByVal nValue As Object, Optional ByVal lFullClean As Boolean = False) As String
        SQLWriteDecimal = "0"
        Try
            If IsNumeric(nValue) Then
                nValue = CDbl(nValue)
            Else
                nValue = 0
            End If
            SQLWriteDecimal = nValue.ToString
            If LCase(SQLWriteDecimal) = "nan" Then SQLWriteDecimal = "0"
            If SQLWriteDecimal = "Infinity" Then
                SQLWriteDecimal = "0"
            End If
            If lFullClean Then
                SQLWriteDecimal = SQLWriteDecimal.Replace(",", "")
                SQLWriteDecimal = SQLWriteDecimal.Replace(".", "")
            Else
                SQLWriteDecimal = SQLWriteDecimal.Replace(",", ".")
            End If
        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Function

    Public Function SQLWriteInteger(ByVal nValue As Integer) As String
        SQLWriteInteger = "0"
        Try
            SQLWriteInteger = nValue.ToString
            SQLWriteInteger = SQLWriteInteger.Replace(",", ".")
        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Function

    Public Function SQLWriteDate(ByVal dValue As Date) As String
        SQLWriteDate = "01.01.1950"
        Try
            If IsDate(dValue) Then
                SQLWriteDate = Mid(dValue.Date.ToString, 1, 10)
            End If
        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Function

    Public Function SQLWriteString(ByVal cValue As String, Optional ByVal nLength As Integer = 0) As String
        SQLWriteString = ""
        Try
            If cValue.Trim = "" Then Exit Function

            SQLWriteString = cValue.Replace("'", " ")

            If nLength <> 0 Then
                SQLWriteString = Mid(SQLWriteString, 1, nLength).Trim
            End If
        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Function

    Public Function SQLReadString(ByVal oReader As SqlDataReader, Optional ByVal cFieldName As String = "", Optional ByVal nWidth As Integer = 0) As String
        SQLReadString = ""
        Try
            If cFieldName = "" Then
                If IsDBNull(oReader.GetValue(0)) Then
                    SQLReadString = ""
                ElseIf IsNothing(oReader.GetValue(0)) Then
                    SQLReadString = ""
                Else
                    SQLReadString = oReader.GetString(0).Trim()
                End If
            Else
                If IsDBNull(oReader.GetValue(oReader.GetOrdinal(cFieldName))) Then
                    SQLReadString = ""
                ElseIf IsNothing(oReader.GetValue(oReader.GetOrdinal(cFieldName))) Then
                    SQLReadString = ""
                Else
                    SQLReadString = oReader.GetString(oReader.GetOrdinal(cFieldName)).Trim()
                End If
            End If
            SQLReadString = SQLReadString.Replace("'", "")
            If nWidth > 0 Then
                SQLReadString = Mid(SQLReadString, 1, nWidth).Trim
            End If
            If IsNothing(SQLReadString) Then
                SQLReadString = ""
            End If
            If IsDBNull(SQLReadString) Then
                SQLReadString = ""
            End If

        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Function

    Public Function SQLReadDouble(ByVal oReader As SqlDataReader, Optional ByVal cFieldName As String = "") As Double
        SQLReadDouble = 0
        Try
            If cFieldName = "" Then
                If IsDBNull(oReader.GetValue(0)) Then
                    SQLReadDouble = 0
                ElseIf IsNothing(oReader.GetValue(0)) Then
                    SQLReadDouble = 0
                Else
                    SQLReadDouble = oReader.GetDecimal(0)
                End If
            Else
                If IsDBNull(oReader.GetValue(oReader.GetOrdinal(cFieldName))) Then
                    SQLReadDouble = 0
                ElseIf IsNothing(oReader.GetValue(oReader.GetOrdinal(cFieldName))) Then
                    SQLReadDouble = 0
                Else
                    SQLReadDouble = oReader.GetDecimal(oReader.GetOrdinal(cFieldName))
                End If
            End If

        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Function

    Public Function SQLReadInteger(ByVal oReader As SqlDataReader, Optional ByVal cFieldName As String = "") As Integer
        SQLReadInteger = 0
        Try
            If cFieldName = "" Then
                If IsDBNull(oReader.GetValue(0)) Then
                    SQLReadInteger = 0
                ElseIf IsNothing(oReader.GetValue(0)) Then
                    SQLReadInteger = 0
                Else
                    SQLReadInteger = oReader.GetInt32(0)
                End If
            Else
                If IsDBNull(oReader.GetValue(oReader.GetOrdinal(cFieldName))) Then
                    SQLReadInteger = 0
                ElseIf IsNothing(oReader.GetValue(oReader.GetOrdinal(cFieldName))) Then
                    SQLReadInteger = 0
                Else
                    SQLReadInteger = oReader.GetInt32(oReader.GetOrdinal(cFieldName))
                End If
            End If

        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Function

    Public Function SQLReadDate(ByVal oReader As SqlDataReader, Optional ByVal cFieldName As String = "") As Date
        SQLReadDate = #1/1/1950#
        Try
            If cFieldName = "" Then
                If IsDBNull(oReader.GetValue(0)) Then
                    SQLReadDate = #1/1/1950#
                ElseIf IsNothing(oReader.GetValue(0)) Then
                    SQLReadDate = #1/1/1950#
                ElseIf IsDate(oReader.GetDateTime(0)) Then
                    SQLReadDate = oReader.GetDateTime(0)
                End If
            Else
                If IsDBNull(oReader.GetValue(oReader.GetOrdinal(cFieldName))) Then
                    SQLReadDate = #1/1/1950#
                ElseIf IsNothing(oReader.GetValue(oReader.GetOrdinal(cFieldName))) Then
                    SQLReadDate = #1/1/1950#
                ElseIf IsDate(oReader.GetValue(oReader.GetOrdinal(cFieldName))) Then
                    SQLReadDate = oReader.GetDateTime(oReader.GetOrdinal(cFieldName))
                End If
            End If

        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Function

    Public Function CreateTempView(cSQL As String, Optional cViewName As String = "", Optional cViewNameHeader As String = "tmpv_", Optional lDeleteIfViewNameExists As Boolean = True,
                                   Optional cDatabase As String = "") As String

        Dim oSQL As New SQLServerClass(True,, cDatabase)
        Dim cSQL2 As String = ""

        CreateTempView = ""

        Try
            oSQL.OpenConn()

            If Trim(cViewName) = "" Then
                CreateTempView = cViewNameHeader + CStr(Int(Rnd() * 10000))
            Else
                CreateTempView = Trim(cViewName)
            End If

            If lDeleteIfViewNameExists Then
                DropView(CreateTempView)
            Else
                Do While True
                    cSQL2 = "select * " +
                            " from sysobjects with (NOLOCK) " +
                            " where id = object_id('" + Trim(CreateTempView) + "') "

                    If oSQL.CheckExists(cSQL2) Then
                        CreateTempView = cViewNameHeader + CStr(Int(Rnd() * 10000))
                    Else
                        Exit Do
                    End If
                Loop
            End If

            cSQL = "create view " + CreateTempView + " as " + cSQL
            oSQL.SQLExecute(cSQL)

            oSQL.CloseConn()
            oSQL = Nothing

        Catch ex As Exception
            ErrDisp(ex, "CreateTempView", cSQL)
        End Try
    End Function

    Public Sub DropView(cViewName As String, Optional cDatabase As String = "")

        Dim oSQL As New SQLServerClass(True,, cDatabase)
        Dim cSQL As String = ""

        Try
            If cViewName.Trim = "" Then Exit Sub

            oSQL.OpenConn()

            cSQL = "select * " +
                    " from sysobjects with (NOLOCK) " +
                    " where id = object_id('" + cViewName.Trim + "') "

            If oSQL.CheckExists(cSQL) Then
                cSQL = "drop view " + cViewName.Trim
                oSQL.SQLExecute(cSQL)
            End If

            oSQL.CloseConn()
            oSQL = Nothing

        Catch ex As Exception
            ErrDisp(ex, "DropView", cSQL)
        End Try
    End Sub

    Public Function GetTmpTableName(Optional cNameSeed As String = "", Optional cDatabase As String = "") As String

        Dim oSQL As New SQLServerClass(True,, cDatabase)
        Dim cSQL As String = ""

        GetTmpTableName = ""

        Try

            oSQL.OpenConn()

            GetTmpTableName = "tmpt_" + cNameSeed + CStr(Int(Rnd() * 10000))

            Do While True
                cSQL = "select * " +
                        " from sysobjects with (NOLOCK) " +
                        " where id = object_id('" + Trim(GetTmpTableName) + "') "

                If oSQL.CheckExists(cSQL) Then
                    GetTmpTableName = "tmpt_" + cNameSeed + CStr(Int(Rnd() * 10000))
                Else
                    Exit Do
                End If
            Loop

            oSQL.CloseConn()
            oSQL = Nothing

            DropTable(GetTmpTableName)

        Catch ex As Exception
            ErrDisp(ex, "UtilSQL", cSQL)
        End Try
    End Function

    Public Function CreateTempTable(cSQL As String, Optional cNameSeed As String = "", Optional cTableName As String = "", Optional cDatabase As String = "") As String

        Dim oSQL As New SQLServerClass(True,, cDatabase)

        CreateTempTable = ""

        Try

            oSQL.OpenConn()

            If cSQL.Trim = "" Then Exit Function

            If cTableName.Trim = "" Then
                cTableName = GetTmpTableName(cNameSeed)
            Else
                cTableName = cTableName.Trim
            End If

            DropTable(cTableName.Trim)

            cSQL = "create table " + cTableName.Trim + " " + cSQL
            oSQL.SQLExecute(cSQL)

            oSQL.CloseConn()
            oSQL = Nothing

            CreateTempTable = cTableName

        Catch ex As Exception
            ErrDisp(ex, "UtilSQL", cSQL)
        End Try
    End Function

    Public Sub DropTable(ByVal cTableName As String, Optional cDatabase As String = "")

        Dim cSQL As String = ""
        Dim oSQL As New SQLServerClass(True,, cDatabase)

        Try
            If IsNothing(cTableName) Then Exit Sub
            If cTableName.Trim = "" Then Exit Sub

            oSQL.OpenConn()

            cSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + cTableName.Trim + "]') AND type in (N'U')) " +
                    " DROP TABLE [dbo].[" + cTableName.Trim + "] "

            oSQL.SQLExecute(cSQL)

            oSQL.CloseConn()
            oSQL = Nothing

        Catch ex As Exception
            ErrDisp(ex, "UtilSQL", cSQL)
        End Try
    End Sub

End Module
