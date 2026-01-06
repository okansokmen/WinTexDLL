Option Explicit On
Imports System.IO

Module utilMain

    'Public G_Owner As String = "vera"
    'Public G_Owner As String = "vera_local"
    'Public G_Owner As String = "eroglu_deneme"
    'Public G_Owner As String = "eroglu"
    'Public G_Owner As String = "eroglu_local"

    Public G_decimal As String
    Public G_digitgroup As String
    Public G_DateSeperator As String
    Public G_Selection As String
    Public G_Selection2 As String
    Public lGlobalDebugMode As Boolean = False

    Public Const G_NumberFormat = "###,###,###,###,###,##0"
    Public Const G_Number1Format = "###,###,###,###,###,##0.0"
    Public Const G_Number2Format = "###,###,###,###,###,##0.00"
    Public Const G_Number3Format = "###,###,###,###,###,##0.000"
    Public Const G_Number4Format = "###,###,###,###,###,##0.0000"
    Public Const G_Number5Format = "###,###,###,###,###,##0.00000"
    Public Const G_Number6Format = "###,###,###,###,###,##0.000000"

    Public cSiparisFilter As String = ""

    Public Structure oSQLConn

        Dim cOwner As String

        Dim cServer As String
        Dim cDatabase As String
        Dim cUser As String
        Dim cPassword As String
        Dim cConnStr As String

        Dim cWinTexUser As String
        Dim cPersonel As String

    End Structure

    Public oConnection As oSQLConn

    Public Sub initWinTex()
        Try
            G_decimal = "."
            G_digitgroup = ","
            G_DateSeperator = "."
        Catch ex As System.Exception
            ErrDisp(ex.Message, "initWinTex", , , ex)
        End Try
    End Sub

    Public Function GetPersonel() As String

        GetPersonel = ""

        Try
            Dim OL As Object = Nothing
            Dim olAllUsers As Object = Nothing
            Dim oExchUser As Object = Nothing
            Dim oentry As Object = Nothing
            Dim myitem As Object = Nothing
            Dim User As String = ""
            Dim cMail As String = ""
            Dim oSQLServer As New SQLServerClass

            'Set olAllUsers = Application.Session.AddressLists.Item("All Users").AddressEntries

            'User = Application.Session.CurrentUser.Name

            'Set oentry = olAllUsers.Item(User)

            'Set oExchUser = oentry.GetExchangeUser()

            'cMail = oExchUser.PrimarySmtpAddress

            If Trim(cMail) <> "" Then
                oSQLServer.OpenConn()

                oSQLServer.cSQLQuery = "select top 1 personel " +
                               " from personel with (NOLOCK) " +
                               " where email = '" + Trim(cMail) + "' "

                GetPersonel = oSQLServer.DBReadString

                oSQLServer.CloseConn()
            End If

        Catch ex As System.Exception
            ErrDisp(ex.Message, "GetPersonel", , , ex)
        End Try
    End Function

    Public Sub G_DoEvents()
        Try
            ' do nothing
        Catch ex As System.Exception
            ErrDisp(ex.Message, "G_DoEvents", , , ex)
        End Try
    End Sub

    Public Function StrStrip(cText As String) As String

        StrStrip = ""

        Try
            StrStrip = Replace(cText, Chr(13), " ")
            StrStrip = Trim$(StrStrip)
        Catch ex As System.Exception
            ErrDisp(ex.Message, "StrStrip", , , ex)
        End Try
    End Function

    Public Function StrStrip2(cText As VariantType, Optional cReplaceChar As String = " ") As String

        StrStrip2 = ""

        Try
            StrStrip2 = CStr(cText)
            StrStrip2 = Replace(StrStrip2, Chr(13), cReplaceChar)
            StrStrip2 = Replace(StrStrip2, Chr(10), cReplaceChar)
            StrStrip2 = Replace(StrStrip2, vbTab, cReplaceChar)
            StrStrip2 = Replace(StrStrip2, vbCrLf, cReplaceChar)
            StrStrip2 = Trim$(StrStrip2)
        Catch ex As System.Exception
            ErrDisp(ex.Message, "StrStrip2", , , ex)
        End Try
    End Function

    Public Function StrStrip3(cText As String) As String

        StrStrip3 = ""

        Try
            Dim nCnt As Long
            Dim nMaxLen As Long
            Dim cBuffer As String

            ' asc(/) = 47
            ' asc(-) = 45
            ' asc(|) = 124

            cText = Trim$(cText)
            For nCnt = 1 To 20
                cText = Replace$(cText, "  ", " ")
            Next
            cText = Replace$(cText, " ", "|")
            nMaxLen = Len(cText)

            For nCnt = 1 To nMaxLen
                cBuffer = Mid(cText, nCnt, 1)

                If Asc(cBuffer) = 45 Or
                   Asc(cBuffer) = 47 Or
                   Asc(cBuffer) = 124 Or
                   (Asc(cBuffer) > 47 And Asc(cBuffer) < 58) Or
                   (Asc(cBuffer) > 64 And Asc(cBuffer) < 91) Or
                   (Asc(cBuffer) > 96 And Asc(cBuffer) < 123) Then

                    StrStrip3 = StrStrip3 + cBuffer
                End If
            Next
            StrStrip3 = Replace$(StrStrip3, "|", " ")
            StrStrip3 = Trim$(StrStrip3)
        Catch ex As System.Exception
            ErrDisp(ex.Message, "StrStrip3", , , ex)
        End Try
    End Function

    Public Function StrStrip4(cText As String) As String

        StrStrip4 = ""

        Try
            Dim nCnt As Long
            Dim nMaxLen As Long
            Dim cBuffer As String

            nMaxLen = Len(cText)

            For nCnt = 1 To nMaxLen
                cBuffer = Mid(cText, nCnt, 1)

                If Asc(cBuffer) > 31 Then
                    StrStrip4 = StrStrip4 + cBuffer
                End If
            Next
            StrStrip4 = Trim$(StrStrip4)
        Catch ex As System.Exception
            ErrDisp(ex.Message, "StrStrip4", , , ex)
        End Try
    End Function

    Public Function StrStripLettersNumbers(cText As String,
                                       Optional lReplaceBadCharactersWithBlank As Boolean = True,
                                       Optional lDeleteSpace As Boolean = False,
                                       Optional lReplaceBadCharactersWithUnderscore As Boolean = False) As String
        StrStripLettersNumbers = ""

        Try
            Dim nCnt As Long
            Dim nMaxLen As Long
            Dim cBuffer As String

            nMaxLen = Len(cText)

            For nCnt = 1 To nMaxLen
                cBuffer = Mid(cText, nCnt, 1)
                ' rakkamlar + büyük harfler + küçük harfler
                If (Asc(cBuffer) > 47 And Asc(cBuffer) < 58) Or (Asc(cBuffer) > 64 And Asc(cBuffer) < 91) Or (Asc(cBuffer) > 96 And Asc(cBuffer) < 123) Then
                    If lDeleteSpace Then
                        If cBuffer <> " " Then
                            StrStripLettersNumbers = StrStripLettersNumbers + cBuffer
                        End If
                    Else
                        StrStripLettersNumbers = StrStripLettersNumbers + cBuffer
                    End If
                Else
                    If lReplaceBadCharactersWithUnderscore Then
                        StrStripLettersNumbers = StrStripLettersNumbers + "-"
                    ElseIf lReplaceBadCharactersWithBlank Then
                        StrStripLettersNumbers = StrStripLettersNumbers + IIf(lDeleteSpace, "", " ")
                    End If
                End If
            Next
            If lDeleteSpace Then
                StrStripLettersNumbers = Replace(StrStripLettersNumbers, " ", "")
            End If
            StrStripLettersNumbers = Trim$(StrStripLettersNumbers)
        Catch ex As System.Exception
            ErrDisp(ex.Message, "StrStripLettersNumbers", , , ex)
        End Try
    End Function

    Public Function StrStripLettersNumbers2(cText As String) As String

        StrStripLettersNumbers2 = ""

        Try
            Dim nCnt As Long
            Dim nMaxLen As Long
            Dim cBuffer As String

            nMaxLen = Len(cText)

            For nCnt = 1 To nMaxLen
                cBuffer = Mid(cText, nCnt, 1)
                ' rakkamlar + büyük harfler + küçük harfler
                If (Asc(cBuffer) > 47 And Asc(cBuffer) < 58) Or (Asc(cBuffer) > 64 And Asc(cBuffer) < 91) Or (Asc(cBuffer) > 96 And Asc(cBuffer) < 123) Then
                    StrStripLettersNumbers2 = StrStripLettersNumbers2 + cBuffer
                End If
            Next
            StrStripLettersNumbers2 = Replace(StrStripLettersNumbers2, " ", "")
            StrStripLettersNumbers2 = Trim$(StrStripLettersNumbers2)
        Catch ex As System.Exception
            ErrDisp(ex.Message, "StrStripLettersNumbers2", , , ex)
        End Try
    End Function

    Public Function StrStripNumbers(cText As String,
                                Optional lReplaceBadCharactersWithBlank As Boolean = False,
                                Optional lDeleteSpace As Boolean = False) As String
        StrStripNumbers = ""

        Try
            Dim nCnt As Long
            Dim nMaxLen As Long
            Dim cBuffer As String

            nMaxLen = Len(cText)

            For nCnt = 1 To nMaxLen
                cBuffer = Mid(cText, nCnt, 1)
                If (Asc(cBuffer) > 47 And Asc(cBuffer) < 58) Then
                    StrStripNumbers = StrStripNumbers + cBuffer
                Else
                    If lReplaceBadCharactersWithBlank Then
                        StrStripNumbers = StrStripNumbers + " "
                    End If
                End If
            Next
            If lDeleteSpace Then
                StrStripNumbers = Replace(StrStripNumbers, " ", "")
            End If
            StrStripNumbers = Trim$(StrStripNumbers)
        Catch ex As System.Exception
            ErrDisp(ex.Message, "StrStripNumbers", , , ex)
        End Try
    End Function

    Public Function GetNumFormat(cDECString As String) As String

        GetNumFormat = G_Number2Format

        Try
            Select Case cDECString
                Case "DEC0" : GetNumFormat = G_NumberFormat
                Case "DEC1" : GetNumFormat = G_Number1Format
                Case "DEC2" : GetNumFormat = G_Number2Format
                Case "DEC3" : GetNumFormat = G_Number3Format
                Case "DEC4" : GetNumFormat = G_Number4Format
                Case "DEC5" : GetNumFormat = G_Number5Format
                Case "DEC6" : GetNumFormat = G_Number6Format
            End Select
        Catch ex As System.Exception
            ErrDisp(ex.Message, "GetNumFormat", , , ex)
        End Try
    End Function

    Public Function ConvEng(cReport As String) As String
        Try
            cReport = Replace(cReport, "İ", "I")
            cReport = Replace(cReport, "ş", "s")
            cReport = Replace(cReport, "Ş", "S")
            cReport = Replace(cReport, "ü", "u")
            cReport = Replace(cReport, "Ü", "U")
            cReport = Replace(cReport, "Ğ", "G")
            cReport = Replace(cReport, "ğ", "g")
            cReport = Replace(cReport, "ı", "i")
            cReport = Replace(cReport, "ö", "o")
            cReport = Replace(cReport, "Ö", "O")
            cReport = Replace(cReport, "ç", "c")
            cReport = Replace(cReport, "Ç", "C")

            ConvEng = cReport
        Catch ex As System.Exception
            ErrDisp(ex.Message, "ConvEng", , , ex)
        End Try
    End Function

    Public Function undimmed(aX) As Boolean
        undimmed = False
    End Function

    Public Function CheckFormState(oForm As Form, Optional nMinWidth As Long = 0, Optional nMinHeight As Long = 0, Optional ByRef nWChange As Long = 0, Optional ByRef nHChange As Long = 0) As Boolean

        CheckFormState = True

        Try
            If nMinWidth = 0 Or nMinHeight = 0 Then Exit Function

            If oForm.Width < nMinWidth Then oForm.Width = nMinWidth
            If oForm.Height < nMinHeight Then oForm.Height = nMinHeight

            nWChange = oForm.Width - nMinWidth
            nHChange = oForm.Height - nMinHeight

        Catch ex As System.Exception
            ErrDisp(ex.Message, "CheckFormState", , , ex)
        End Try
    End Function

    Public Function Min(a, b)

        Min = a

        Try
            If a < b Then
                Min = a
            Else
                Min = b
            End If
        Catch ex As System.Exception
            ErrDisp(ex.Message, "Min", , , ex)
        End Try
    End Function

    Public Function Max(a, b)

        Max = a

        Try
            If a > b Then
                Max = a
            Else
                Max = b
            End If
        Catch ex As System.Exception
            ErrDisp(ex.Message, "Max", , , ex)
        End Try
    End Function


    Public Function GetTempFile(Optional cFileExtension As String = "", Optional cFileHeader As String = "TMP", Optional cFilePath As String = "", Optional cSubPath As String = "",
                                Optional lReturnFileNameWithNoExtension As Boolean = False) As String

        Dim cFileName As String = ""
        Dim nCnt As Long = 0

        GetTempFile = ""

        Try
            If cFilePath.Trim = "" Then
                cFilePath = GetSharePath(cSubPath)
            End If

            cFilePath = cFilePath.Trim

            If Right(cFilePath, 1) = "\" Then
                cFilePath = Mid(cFilePath, 1, Len(cFilePath) - 1)
            End If

            Do While True
                nCnt = nCnt + 1
                cFileName = cFilePath.Trim + "\" + cFileHeader.Trim + Microsoft.VisualBasic.Format(nCnt, "00000")
                If cFileExtension.Trim <> "" Then
                    cFileName = cFileName + "." + cFileExtension
                End If
                If Not My.Computer.FileSystem.FileExists(cFileName) Then
                    GetTempFile = cFileName
                    Exit Do
                End If
            Loop

            If lReturnFileNameWithNoExtension Then
                GetTempFile = GetTempFile.Replace("." + cFileExtension, "")
            End If

        Catch ex As Exception
            ErrDisp("GetTempFile : " + ex.Message, "utilroot")
        End Try
    End Function


    Public Function GetSharePath(Optional cSubPath As String = "") As String

        Dim cSharePath As String = ""
        Dim oFile As FileInfo

        GetSharePath = ""

        Try
            cSharePath = GetSysPar("pathofshare")

            If cSharePath = "" Then
                cSharePath = System.Windows.Forms.Application.ExecutablePath
                oFile = New FileInfo(cSharePath)
                cSharePath = oFile.DirectoryName
            End If

            cSharePath = cSharePath.Trim

            If Right(cSharePath, 1) = "\" Then
                cSharePath = Mid(cSharePath, 1, Len(cSharePath) - 1)
            End If

            If cSubPath.Trim <> "" Then
                cSubPath = cSubPath.Replace("\", "")
                cSharePath = cSharePath + "\" + cSubPath
            End If

            If Not My.Computer.FileSystem.DirectoryExists(cSharePath) Then
                My.Computer.FileSystem.CreateDirectory(cSharePath)
            End If

            GetSharePath = cSharePath.Trim

        Catch ex As Exception
            ' do nothing 
            ' ErrDisp("GetSharePath sharepath -- subpath : " + cSharePath + " -- " + cSubPath, "utilroot",,, ex)
        End Try
    End Function

End Module
