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


End Module
