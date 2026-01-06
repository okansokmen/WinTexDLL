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

Module utilStyleShoots

    Dim cSource As String = ""
    Dim cDestination As String = ""
    Dim cSharePath As String = ""
    Dim cBarkodluNumuneOn As String = ""
    Dim cBarkodluNumuneArka As String = ""
    Dim cBarkodluNumuneDocs As String = ""

    ' source 
    ' \\192.168.10.127\styleshoots
    ' username : styleshootssharing
    ' password : eroglushare

    ' destination
    ' \\192.168.1.204\styleshoots
    ' username : wintex
    ' password : W456s456+

    ' wintex shared docs
    ' \\192.168.1.57\BarkodluNumuneOn 
    ' \\192.168.1.57\BarkodluNumuneArka 
    ' \\192.168.1.57\BarkodluNumuneDocs 
    ' username : wintex
    ' password : W456s456+

    Public Sub StyleShootsMoveFiles()

        Try
            Dim oSQL As New SQLServerClass
            Dim cCommand As String = ""
            Dim nSonuc As Integer = 0

            oSQL.OpenConn()

            cSharePath = oSQL.GetSysPar("pathofshare")
            cBarkodluNumuneOn = cSharePath + "\BarkodluNumuneOn"
            cBarkodluNumuneArka = cSharePath + "\BarkodluNumuneArka"
            cBarkodluNumuneDocs = cSharePath + "\BarkodluNumuneDocs"

            cSource = oSQL.GetSysPar("SSOriginalPath")
            cDestination = oSQL.GetSysPar("SSDestinationPath")

            If (System.IO.Directory.Exists(cBarkodluNumuneOn) = False) Then
                System.IO.Directory.CreateDirectory(cBarkodluNumuneOn)
            End If

            If (System.IO.Directory.Exists(cBarkodluNumuneArka) = False) Then
                System.IO.Directory.CreateDirectory(cBarkodluNumuneArka)
            End If

            If (System.IO.Directory.Exists(cBarkodluNumuneDocs) = False) Then
                System.IO.Directory.CreateDirectory(cBarkodluNumuneDocs)
            End If

            ' xcopy \\192.168.10.127\styleshoots\*.* \\192.168.1.204\styleshoots /s /d
            cCommand = "xcopy " + cSource.Trim + "\*.* " + cDestination.Trim + " /s /d /i "

            nSonuc = Shell(cCommand, AppWinStyle.MinimizedNoFocus, True)

            ForEachFileAndFolder(cDestination, AddressOf dirAction2, AddressOf FileAction2)

            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp("StyleShootsMoveFiles", "utilStyleShoots",,, ex)
        End Try
    End Sub

    Public Sub ForEachFileAndFolder(ByVal sourceFolder As String,
                                    ByVal directoryCallBack As Action(Of DirectoryInfo),
                                    ByVal fileCallBack As Action(Of FileInfo))
        Try
            If Directory.Exists(sourceFolder) Then
                Try
                    For Each foldername As String In Directory.GetDirectories(sourceFolder)
                        If directoryCallBack IsNot Nothing Then
                            directoryCallBack.Invoke(New DirectoryInfo(foldername))
                        End If

                        ForEachFileAndFolder(foldername, directoryCallBack, fileCallBack)
                    Next
                Catch ex As UnauthorizedAccessException
                    Trace.TraceWarning(ex.Message)
                End Try

                If fileCallBack IsNot Nothing Then
                    For Each filename As String In Directory.GetFiles(sourceFolder)
                        fileCallBack.Invoke(New FileInfo(filename))
                    Next
                End If
            End If

        Catch ex As Exception
            ErrDisp("ForEachFileAndFolder", "utilStyleShoots",,, ex)
        End Try
    End Sub

    Public Sub dirAction2(ByVal dirInfo As DirectoryInfo)
        ' do nothing for directories
    End Sub

    Public Sub FileAction2(ByVal fileInfo As FileInfo)

        Try
            Dim cFullName As String = fileInfo.FullName
            Dim cFileName As String = fileInfo.Name
            Dim cFullNameEski As String = ""
            Dim cOnModelNo As String = ""
            Dim cBarcode As String = ""
            Dim lOK As Boolean = False

            If Mid(cFileName.Trim, 21, 1) = "_" Then
                cOnModelNo = Strings.Mid(cFileName, 1, 10)
                cBarcode = Strings.Mid(cFileName, 11, 10)

                If InStr(LCase(cFileName), ".png") > 0 Then
                    ' büyük resim ekli dökümanlara ekle
                    lOK = AddToDocs(cOnModelNo, cBarcode, cFullName, cFileName)
                ElseIf InStr(LCase(cFileName), ".jpg") > 0 Then
                    ' küçük resim 
                    If InStr(LCase(cFileName), "+") > 0 Then
                        lOK = AddToDocs(cOnModelNo, cBarcode, cFullName, cFileName)
                    Else
                        lOK = AddToPictures(cOnModelNo, cBarcode, cFullName, cFileName)
                    End If
                End If

                If lOK Then
                    cFullNameEski = Replace(cFullName, cDestination, cSource)
                    DestroyFile(cFullNameEski)
                End If
            End If

        Catch ex As Exception
            ErrDisp("FileAction2", "utilStyleShoots",,, ex)
        End Try
    End Sub

    Private Function AddToPictures(cOnModelNo As String, cBarcode As String, cFullName As String, cFileName As String) As Boolean

        AddToPictures = False

        Try
            Dim oSQL As SQLServerClass
            Dim oSQL2 As SQLServerClass
            Dim cLazerFisNo As String = ""
            Dim cYikamaReceteNo As String = ""
            Dim cBarcode2 As String = ""

            oSQL = New SQLServerClass
            oSQL.OpenConn()

            oSQL2 = New SQLServerClass
            oSQL2.OpenConn()

            ' yıkama kodu + lazer kodu aynı olan diğer barkodları bul
            oSQL.cSQLQuery = "select top 1 lazerfisno, yikamareceteno " +
                            " from onmodelbarkod with (NOLOCK) " +
                            " where onmodelno = '" + cOnModelNo + "' " +
                            " and onmodelbarkodno = '" + cBarcode + "' "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                cLazerFisNo = oSQL.SQLReadString("lazerfisno")
                cYikamaReceteNo = oSQL.SQLReadString("yikamareceteno")
            End If
            oSQL.oReader.Close()

            oSQL.cSQLQuery = "select distinct onmodelbarkodno " +
                             " from onmodelbarkod with (NOLOCK) " +
                             " where onmodelno = '" + cOnModelNo + "' " +
                             " and onmodelbarkodno is not null " +
                             " and onmodelbarkodno <> '' "

            If cLazerFisNo.Trim = "" Then
                oSQL.cSQLQuery = oSQL.cSQLQuery +
                             " and (lazerfisno is null or lazerfisno = '')  "
            Else
                oSQL.cSQLQuery = oSQL.cSQLQuery +
                             " and lazerfisno = '" + cLazerFisNo + "' "
            End If

            If cYikamaReceteNo.Trim = "" Then
                oSQL.cSQLQuery = oSQL.cSQLQuery +
                             " and (yikamareceteno is null or yikamareceteno = '')  "
            Else
                oSQL.cSQLQuery = oSQL.cSQLQuery +
                             " and yikamareceteno = '" + cYikamaReceteNo + "' "
            End If

            oSQL.GetSQLReader()

            Do While oSQL.oReader.Read

                cBarcode2 = oSQL.SQLReadString("onmodelbarkodno")

                If InStr(LCase(cFileName), "front") > 0 Then

                    If (System.IO.File.Exists(cBarkodluNumuneOn + "\" + cFileName) = False) Then
                        System.IO.File.Copy(cFullName, cBarkodluNumuneOn + "\" + cFileName)
                    End If

                    oSQL2.cSQLQuery = "update onmodelbarkod " +
                                      " set resimon = '" + cBarkodluNumuneOn + "\" + cFileName + "' " +
                                      " where onmodelno = '" + cOnModelNo + "' " +
                                      " and onmodelbarkodno = '" + cBarcode2 + "' "

                    oSQL2.SQLExecute()

                ElseIf InStr(LCase(cFileName), "back") > 0 Then

                    If (System.IO.File.Exists(cBarkodluNumuneArka + "\" + cFileName) = False) Then
                        System.IO.File.Copy(cFullName, cBarkodluNumuneArka + "\" + cFileName)
                    End If

                    oSQL2.cSQLQuery = "update onmodelbarkod " +
                                      " set resimarka = '" + cBarkodluNumuneArka + "\" + cFileName + "' " +
                                      " where onmodelno = '" + cOnModelNo + "' " +
                                      " and onmodelbarkodno = '" + cBarcode2 + "' "

                    oSQL2.SQLExecute()
                End If
            Loop
            oSQL.oReader.Close()

            oSQL.CloseConn()
            oSQL2.CloseConn()

            AddToPictures = True

        Catch ex As Exception
            ErrDisp("AddToPictures", "utilStyleShoots",,, ex)
        End Try
    End Function

    Private Function AddToDocs(cOnModelNo As String, cBarcode As String, cFullName As String, cFileName As String) As Boolean

        AddToDocs = False

        Try
            Dim oSQL As SQLServerClass
            Dim oSQL2 As SQLServerClass
            Dim cLazerFisNo As String = ""
            Dim cYikamaReceteNo As String = ""
            Dim cBarcode2 As String = ""

            If (System.IO.File.Exists(cBarkodluNumuneDocs + "\" + cFileName) = False) Then

                oSQL = New SQLServerClass
                oSQL.OpenConn()

                oSQL2 = New SQLServerClass
                oSQL2.OpenConn()

                System.IO.File.Copy(cFullName, cBarkodluNumuneDocs + "\" + cFileName)

                ' yıkama kodu + lazer kodu aynı olan diğer barkodları bul
                oSQL.cSQLQuery = "select top 1 lazerfisno, yikamareceteno " +
                                 " from onmodelbarkod with (NOLOCK) " +
                                 " where onmodelno = '" + cOnModelNo + "' " +
                                 " and onmodelbarkodno = '" + cBarcode + "' "

                oSQL.GetSQLReader()

                If oSQL.oReader.Read Then
                    cLazerFisNo = oSQL.SQLReadString("lazerfisno")
                    cYikamaReceteNo = oSQL.SQLReadString("yikamareceteno")
                End If
                oSQL.oReader.Close()

                oSQL.cSQLQuery = "select distinct onmodelbarkodno " +
                                 " from onmodelbarkod with (NOLOCK) " +
                                 " where onmodelno = '" + cOnModelNo + "' " +
                                 " and onmodelbarkodno is not null " +
                                 " and onmodelbarkodno <> '' "

                If cLazerFisNo.Trim = "" Then
                    oSQL.cSQLQuery = oSQL.cSQLQuery +
                                " and (lazerfisno is null or lazerfisno = '')  "
                Else
                    oSQL.cSQLQuery = oSQL.cSQLQuery +
                                " and lazerfisno = '" + cLazerFisNo + "' "
                End If

                If cYikamaReceteNo.Trim = "" Then
                    oSQL.cSQLQuery = oSQL.cSQLQuery +
                                " and (yikamareceteno is null or yikamareceteno = '')  "
                Else
                    oSQL.cSQLQuery = oSQL.cSQLQuery +
                                " and yikamareceteno = '" + cYikamaReceteNo + "' "
                End If

                oSQL.GetSQLReader()

                Do While oSQL.oReader.Read

                    cBarcode2 = oSQL.SQLReadString("onmodelbarkodno")

                    oSQL2.cSQLQuery = "delete documents " +
                                " where docvalue = '" + cBarcode2 + "' " +
                                " and doctype = 'onmodelbarkod' " +
                                " and docpath = '" + SQLWriteString(cBarkodluNumuneDocs + "\" + cFileName, 255) + "' "

                    oSQL2.SQLExecute()

                    oSQL2.cSQLQuery = "insert documents (docvalue, doctype, rdocname, vdocname, docpath, " +
                                " type, extension, docsubtype, duzletmetarihi, duzeltmesaati, " +
                                " username) "

                    oSQL2.cSQLQuery = oSQL2.cSQLQuery +
                                " values ('" + SQLWriteString(cBarcode2, 30) + "', " +
                                " 'onmodelbarkod', " +
                                " 'onmodelbarkod', " +
                                " '" + SQLWriteString(cFileName, 150) + "', " +
                                " '" + SQLWriteString(cBarkodluNumuneDocs + "\" + cFileName, 255) + "', "

                    oSQL2.cSQLQuery = oSQL2.cSQLQuery +
                                " 'Picture File', " +
                                " 'png', " +
                                " 'ORJINAL RESIM', " +
                                " convert(date,getdate()), " +
                                " CONVERT(CHAR(8),GETDATE(),108), "

                    oSQL2.cSQLQuery = oSQL2.cSQLQuery +
                                " 'SISTEM' ) "

                    oSQL2.SQLExecute()
                Loop
                oSQL.oReader.Close()

                oSQL.CloseConn()
                oSQL2.CloseConn()

                AddToDocs = True
            End If

        Catch ex As Exception
            ErrDisp("AddToDocs", "utilStyleShoots",,, ex)
        End Try
    End Function

End Module
