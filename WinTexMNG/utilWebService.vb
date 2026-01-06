Option Explicit On
Option Strict On

Imports System
Imports System.Globalization

Module utilWebService

    Public Function WebReadString(oValue As Object) As String

        WebReadString = ""

        On Error Resume Next

        If IsNothing(oValue) Then Exit Function
        WebReadString = oValue.ToString.Trim

    End Function

    Public Function WebReadInt(oValue As Object) As Integer

        WebReadInt = 0

        On Error Resume Next

        If IsNothing(oValue) Then Exit Function
        If Not IsNumeric(oValue) Then Exit Function
        WebReadInt = CInt(oValue)

    End Function

    Public Function WebReadDouble(oValue As Object) As Double

        WebReadDouble = 0

        On Error Resume Next

        If IsNothing(oValue) Then Exit Function
        If Not IsNumeric(oValue) Then Exit Function
        WebReadDouble = CDbl(oValue)

    End Function

    Public Function WebReadDate(oValue As Object, nCase As Integer) As Date

        Dim cTarih As String = ""
        Dim oProvider As CultureInfo = CultureInfo.InvariantCulture

        WebReadDate = #1/1/1950#

        On Error Resume Next

        If IsNothing(oValue) Then Exit Function

        cTarih = oValue.ToString
        cTarih = Replace(cTarih, "T", " ")
        cTarih = Mid(cTarih, 1, 19)

        If nCase = 1 Then
            ' mng
            WebReadDate = DateTime.ParseExact(cTarih, "dd-MM-yyyy HH:mm:ss", oProvider)
        Else
            ' byexpress
            WebReadDate = DateTime.ParseExact(cTarih, "yyyy-MM-dd HH:mm:ss", oProvider)
        End If

    End Function

    Public Function WebReadDate2(oValue As Object) As Date

        Dim cTarih As String = ""
        Dim oProvider As CultureInfo = CultureInfo.InvariantCulture

        On Error Resume Next

        If IsNothing(oValue) Then Exit Function

        cTarih = oValue.ToString
        WebReadDate2 = DateTime.ParseExact(cTarih, "yyyyMMdd HHmmss", oProvider)

    End Function

End Module
