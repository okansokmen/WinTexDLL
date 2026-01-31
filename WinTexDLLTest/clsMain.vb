''This is fully working VB.NET code that was converted from C#.
''The Origninal code can be found here http://forums.microsoft.com/MSDN/ShowPost.aspx?PostID=13748&SiteID=1 and was posted by FabioPoroli
''-ProphetBeal 05/22/2008-

Imports System.Runtime.InteropServices
Imports System.Text

Friend Delegate Sub FilenameDroppedHandler(ByVal sender As Object, ByVal filename As String)
Friend Delegate Sub StreamDroppedHandler(ByVal sender As Object, ByVal filename As String, ByVal stream As NativeMethods.IStream)
Friend Delegate Sub StorageDroppedHandler(ByVal sender As Object, ByVal filename As String, ByVal storage As NativeMethods.IStorage)
Public Delegate Sub DroppedByteArrayAvailableHandler(ByVal sender As Object, ByVal filename As String, ByVal bytes As Byte())


Friend Interface IDropTargetControl
    Sub Activate()
    Sub DragEnter(ByVal filename As String)
    Sub DragLeave()
    Sub DragOver()
    Sub DragDrop()
End Interface

Friend Class FileDropTarget
    Implements NativeMethods.IOleDropTarget

    <DllImport("shell32.dll", CharSet:=CharSet.Auto)>
    Public Shared Function DragQueryFile(ByVal hDrop As HandleRef, ByVal iFile As Integer, ByVal lpszFile As StringBuilder, ByVal cch As Integer) As Integer
    End Function

    Private owner As IDropTargetControl
    Private cachedEffect As DragDropEffects

    Public Sub New(ByVal owner As IDropTargetControl)
        Try
            Me.owner = owner
            cachedEffect = DragDropEffects.None
        Catch ex As Exception
            Throw New Exception("Unable to initialise object.", ex)
        End Try
    End Sub

    'Private Function GetFilename(ByVal dataObject As NativeMethods.IOleDataObject) As String
    '    Dim filename As String = String.Empty
    '    Try

    '        Dim medium As New NativeMethods.STGMEDIUM()
    '        Dim format As New NativeMethods.FORMATETC()
    '        format.cfFormat = CUShort(NativeMethods.ShellClipboardFormats.CFSTR_FILENAMEW.Id)
    '        format.dwAspect = NativeMethods.DVASPECT_CONTENT
    '        format.lindex = -1
    '        format.ptd = New IntPtr(0)
    '        format.tymed = NativeMethods.TYMED_HGLOBAL Or NativeMethods.TYMED_ISTORAGE Or NativeMethods.TYMED_ISTREAM Or NativeMethods.TYMED_FILE
    '        dataObject.OleGetData(format, medium)

    '        Dim ptr As IntPtr = NativeMethods.GlobalLock(New HandleRef(Nothing, medium.unionmember))
    '        'filename = System.Text.Encoding.UTF8.GetString(ptr) 'New Sring(CType(ptr, Char))
    '        filename = System.Runtime.InteropServices.Marshal.PtrToStringAuto(ptr)
    '        NativeMethods.GlobalUnlock(New HandleRef(Nothing, medium.unionmember))
    '    Catch ex As Exception
    '        Throw New Exception("Unable to determine the file name.", ex)
    '    End Try
    '    Return filename
    'End Function

    'Private Function OleDragEnter(ByVal pDataObj As Object, ByVal grfKeyState As Integer, ByVal pt As Long, ByRef pdwEffect As Integer) As Integer Implements NativeMethods.IOleDropTarget.OleDragEnter
    '    Try
    '        ' Default to DROPEFFECT_NONE
    '        cachedEffect = DragDropEffects.None

    '        '
    '        ' Does the data object support CFSTR_FILEDESCRIPTOR
    '        If QueryGetFileDescriptorArray(DirectCast(pDataObj, NativeMethods.IOleDataObject)) Then
    '            ' Retrieve the list of files/folders
    '            Dim dataObject As NativeMethods.IOleDataObject = DirectCast(pDataObj, NativeMethods.IOleDataObject)
    '            Dim files As NativeMethods.FILEDESCRIPTOR() = GetFileDescriptorArray(dataObject)
    '            If files Is Nothing OrElse files.Length = 0 Then
    '                cachedEffect = DragDropEffects.Copy
    '                Dim filename As String = GetFilename(dataObject)
    '                owner.DragEnter(filename)
    '            Else
    '                Dim firstFile As NativeMethods.FILEDESCRIPTOR = files(0)
    '                Dim firstFilename As String = firstFile.cFileName
    '                ' Indicate that we can copy the item(s)
    '                cachedEffect = DragDropEffects.Copy
    '                owner.DragEnter(firstFilename)
    '            End If
    '        End If

    '        pdwEffect = CInt(cachedEffect)
    '        Return NativeMethods.S_OK
    '    Catch ex As Exception
    '        Throw New Exception("Error in the OLE Drag Enter.", ex)
    '    End Try

    'End Function

    Private Function GetFilenames(ByVal dataObject As NativeMethods.IOleDataObject) As String()
        Dim filenames As New Generic.List(Of String)
        Try

            Dim medium As New NativeMethods.STGMEDIUM()
            Dim format As New NativeMethods.FORMATETC()
            format.cfFormat = CUShort(NativeMethods.CF_HDROP)
            format.dwAspect = NativeMethods.DVASPECT_CONTENT
            format.lindex = -1
            format.ptd = New IntPtr(0)
            format.tymed = NativeMethods.TYMED_HGLOBAL Or NativeMethods.TYMED_ISTORAGE Or NativeMethods.TYMED_ISTREAM Or NativeMethods.TYMED_FILE
            Dim result As Integer = dataObject.OleGetData(format, medium)

            Dim hRef As New HandleRef(Nothing, medium.unionmember)

            If NativeMethods.Succeeded(result) Then
                'Dim cFiles As Integer = NativeMethods.DragQueryFile(hRef, &HFFFFFFFF, Nothing, 0)
                Dim cFiles As Integer = DragQueryFile(hRef, &HFFFFFFFF, Nothing, 0)

                For i As Integer = 0 To cFiles - 1
                    Dim sb As New StringBuilder(256)
                    'NativeMethods.DragQueryFile(hRef, i, sb, sb.Capacity + 1)
                    DragQueryFile(hRef, i, sb, sb.Capacity + 1)

                    filenames.Add(sb.ToString)
                Next
            End If
        Catch ex As Exception
            Throw New Exception("Unable to determine the file name.", ex)
        End Try
        Return filenames.ToArray
    End Function

    Private Function OleDragEnter(ByVal pDataObj As Object, ByVal grfKeyState As Integer, ByVal pt As Long, ByRef pdwEffect As Integer) As Integer Implements NativeMethods.IOleDropTarget.OleDragEnter
        Try
            ' Default to DROPEFFECT_NONE
            cachedEffect = DragDropEffects.None

            '
            ' Does the data object support CFSTR_FILEDESCRIPTOR
            If QueryGetFileDescriptorArray(DirectCast(pDataObj, NativeMethods.IOleDataObject)) Then
                ' Retrieve the list of files/folders
                Dim dataObject As NativeMethods.IOleDataObject = DirectCast(pDataObj, NativeMethods.IOleDataObject)
                Dim files As NativeMethods.FILEDESCRIPTOR() = GetFileDescriptorArray(dataObject)
                If files Is Nothing OrElse files.Length = 0 Then
                    cachedEffect = DragDropEffects.Copy
                    Dim filenames() As String = GetFilenames(dataObject)
                    If filenames.Length > 0 Then
                        owner.DragEnter(filenames(0))
                    End If
                Else
                    Dim firstFile As NativeMethods.FILEDESCRIPTOR = files(0)
                    Dim firstFilename As String = firstFile.cFileName
                    ' Indicate that we can copy the item(s)
                    cachedEffect = DragDropEffects.Copy
                    owner.DragEnter(firstFilename)
                End If
            End If

            pdwEffect = CInt(cachedEffect)
            Return NativeMethods.S_OK
        Catch ex As Exception
            Throw New Exception("Error in the OLE Drag Enter.", ex)
        End Try

    End Function

    Private Function OleDragOver(ByVal grfKeyState As Integer, ByVal pt As Long, ByRef pdwEffect As Integer) As Integer Implements NativeMethods.IOleDropTarget.OleDragOver
        Try
            pdwEffect = CInt(cachedEffect)
            owner.DragOver()
            Return NativeMethods.S_OK
        Catch ex As Exception
            Throw New Exception("Error in the OLE Drag Over.", ex)
        End Try
    End Function

    Private Function OleDragLeave() As Integer Implements NativeMethods.IOleDropTarget.OleDragLeave
        Try
            cachedEffect = DragDropEffects.None
            owner.DragLeave()
            Return NativeMethods.S_OK
        Catch ex As Exception
            Throw New Exception("Error in the OLE Drag Leave.", ex)
        End Try
    End Function

    'Private Function OleDrop(ByVal pDataObj As Object, ByVal grfKeyState As Integer, ByVal pt As Long, ByRef pdwEffect As Integer) As Integer Implements NativeMethods.IOleDropTarget.OleDrop
    '    Dim result As Int64
    '    Try

    '        ' Default to DROPEFFECT_NONE
    '        cachedEffect = DragDropEffects.None

    '        ' Retrieve the list of files/folders
    '        Dim dataObject As NativeMethods.IOleDataObject = DirectCast(pDataObj, NativeMethods.IOleDataObject)
    '        Dim files As NativeMethods.FILEDESCRIPTOR() = GetFileDescriptorArray(dataObject)

    '        If files IsNot Nothing AndAlso files.Length > 0 Then
    '            result = CopyFileContents(DirectCast(pDataObj, NativeMethods.IOleDataObject), files)
    '            If NativeMethods.Succeeded(CInt(result)) Then
    '                owner.DragDrop()
    '                cachedEffect = DragDropEffects.Copy
    '                owner.Activate()
    '            End If
    '        Else
    '            Dim filename As String = GetFilename(dataObject)
    '            SaveFilename(filename)
    '            owner.DragDrop()
    '            owner.Activate()
    '        End If
    '        pdwEffect = CInt(cachedEffect)
    '        Return NativeMethods.S_OK
    '    Catch ex As Exception
    '        Throw New Exception("Error in the OLE Drop.", ex)
    '    End Try
    'End Function

    Private Function OleDrop(ByVal pDataObj As Object, ByVal grfKeyState As Integer, ByVal pt As Long, ByRef pdwEffect As Integer) As Integer Implements NativeMethods.IOleDropTarget.OleDrop
        Dim result As Int64
        Try

            ' Default to DROPEFFECT_NONE
            cachedEffect = DragDropEffects.None

            ' Retrieve the list of files/folders
            Dim dataObject As NativeMethods.IOleDataObject = DirectCast(pDataObj, NativeMethods.IOleDataObject)
            Dim files As NativeMethods.FILEDESCRIPTOR() = GetFileDescriptorArray(dataObject)

            If files IsNot Nothing AndAlso files.Length > 0 Then
                result = CopyFileContents(DirectCast(pDataObj, NativeMethods.IOleDataObject), files)
                If NativeMethods.Succeeded(CInt(result)) Then
                    owner.DragDrop()
                    cachedEffect = DragDropEffects.Copy
                    owner.Activate()
                End If
            Else
                For Each filename As String In GetFilenames(dataObject)
                    SaveFilename(filename)
                Next
                owner.DragDrop()
                owner.Activate()
            End If
            pdwEffect = CInt(cachedEffect)
            Return NativeMethods.S_OK
        Catch ex As Exception
            Throw New Exception("Error in the OLE Drop.", ex)
        End Try
    End Function

    Private Function GetFileDescriptorArray(ByVal dataObject As NativeMethods.IOleDataObject) As NativeMethods.FILEDESCRIPTOR()
        Dim result As Integer
        Dim files As NativeMethods.FILEDESCRIPTOR()
        Dim formatType As Type
        Dim hGlobal As HandleRef
        Dim pdata As IntPtr
        Try
            Dim format As New NativeMethods.FORMATETC()
            Dim medium As New NativeMethods.STGMEDIUM()
            '
            ' Query the data object for CFSTR_FILEDESCRIPTORW
            format.cfFormat = CUShort(NativeMethods.ShellClipboardFormats.CFSTR_FILEDESCRIPTORW.Id)
            format.dwAspect = NativeMethods.DVASPECT_CONTENT
            format.lindex = -1
            format.ptd = New IntPtr(0)
            format.tymed = NativeMethods.TYMED_HGLOBAL
            result = dataObject.OleGetData(format, medium)
            If NativeMethods.Succeeded(result) Then
                formatType = GetType(NativeMethods.FILEDESCRIPTORW)
            Else
                '
                ' Query the data object for CFSTR_FILEDESCRIPTORA
                format = New NativeMethods.FORMATETC()
                format.cfFormat = CUShort(NativeMethods.ShellClipboardFormats.CFSTR_FILEDESCRIPTORA.Id)
                format.dwAspect = NativeMethods.DVASPECT_CONTENT
                format.lindex = -1
                format.ptd = New IntPtr(0)
                format.tymed = NativeMethods.TYMED_HGLOBAL
                result = dataObject.OleGetData(format, medium)
                If NativeMethods.Succeeded(result) Then
                    formatType = GetType(NativeMethods.FILEDESCRIPTORA)
                Else
                    '
                    ' This data object does not support CFSTR_FILEDESCRIPTOR
                    Return New NativeMethods.FILEDESCRIPTOR(-1) {}
                End If
            End If
            hGlobal = New HandleRef(Nothing, medium.unionmember)
            pdata = NativeMethods.GlobalLock(hGlobal)
            Try
                '
                ' Determine the number of items in the array
                Dim fgd As NativeMethods.FILEGROUPDESCRIPTORW = DirectCast(Marshal.PtrToStructure(pdata, GetType(NativeMethods.FILEGROUPDESCRIPTORW)), NativeMethods.FILEGROUPDESCRIPTORW)
                '
                ' Allocate an array of FILEDESCRIPTOR structures
                files = New NativeMethods.FILEDESCRIPTOR(CInt(fgd.cItems) - 1) {}
                '
                ' Set our pointer offset to the beginning of the FILEDESCRIPTOR* array
                pdata = CType((CInt(pdata) + Marshal.SizeOf(pdata)), IntPtr)
                For index As Integer = 0 To CInt(fgd.cItems) - 1
                    '
                    ' Walk the array, converting each FILEDESCRIPTOR* to a FILEDESCRIPTOR
                    Dim fdA As NativeMethods.FILEDESCRIPTORA = DirectCast(Marshal.PtrToStructure(pdata, GetType(NativeMethods.FILEDESCRIPTORA)), NativeMethods.FILEDESCRIPTORA)
                    Dim fdW As NativeMethods.FILEDESCRIPTORW = DirectCast(Marshal.PtrToStructure(pdata, GetType(NativeMethods.FILEDESCRIPTORW)), NativeMethods.FILEDESCRIPTORW)
                    files(index) = New NativeMethods.FILEDESCRIPTOR()
                    If formatType Is GetType(NativeMethods.FILEDESCRIPTORW) Then
                        files(index).dwFlags = fdW.dwFlags
                        files(index).clsid = fdW.clsid
                        files(index).sizel = fdW.sizel
                        files(index).pointl = fdW.pointl
                        files(index).dwFileAttributes = fdW.dwFileAttributes
                        files(index).ftCreationTime = fdW.ftCreationTime
                        files(index).ftLastAccessTime = fdW.ftLastAccessTime
                        files(index).ftLastWriteTime = fdW.ftLastWriteTime
                        files(index).nFileSizeHigh = fdW.nFileSizeHigh
                        files(index).nFileSizeLow = fdW.nFileSizeLow
                        files(index).cFileName = fdW.cFileName
                    Else
                        files(index).dwFlags = fdA.dwFlags
                        files(index).clsid = fdA.clsid
                        files(index).sizel = fdA.sizel
                        files(index).pointl = fdA.pointl
                        files(index).dwFileAttributes = fdA.dwFileAttributes
                        files(index).ftCreationTime = fdA.ftCreationTime
                        files(index).ftLastAccessTime = fdA.ftLastAccessTime
                        files(index).ftLastWriteTime = fdA.ftLastWriteTime
                        files(index).nFileSizeHigh = fdA.nFileSizeHigh
                        files(index).nFileSizeLow = fdA.nFileSizeLow
                        files(index).cFileName = fdA.cFileName
                    End If
                    '
                    ' Advance to the next item in the array
                    pdata = CType((CInt(pdata) + Marshal.SizeOf(formatType)), IntPtr)
                Next
            Catch
                '
                ' Bail on any exceptions
                files = New NativeMethods.FILEDESCRIPTOR(-1) {}
            End Try
            NativeMethods.GlobalUnlock(hGlobal)
            NativeMethods.ReleaseStgMedium(medium)
            Return files
        Catch ex As Exception
            Throw New Exception("Unable to Get the File Descriptor Array.", ex)
        End Try
    End Function

    Private Function CopyFileContents(ByVal dataObject As NativeMethods.IOleDataObject, ByVal files As NativeMethods.FILEDESCRIPTOR()) As Int64
        Try
            Dim result As Int64 = NativeMethods.E_FAIL
            Dim medium As New NativeMethods.STGMEDIUM()
            If files.Length > 0 Then
                For index As Integer = 0 To files.Length - 1
                    Dim filename As String = files(index).cFileName
                    '
                    ' If the object is a folder, make sure it exists.
                    '
                    ' TODO: Make sure that the specified directory exists
                    If (files(index).dwFileAttributes And NativeMethods.FILE_ATTRIBUTE_DIRECTORY) = NativeMethods.FILE_ATTRIBUTE_DIRECTORY Then
                    Else
                        '
                        ' Otherwise, create the file and save its contents
                        result = SaveToFile(dataObject, NativeMethods.ShellClipboardFormats.CFSTR_FILECONTENTS, index, filename)
                        If NativeMethods.Failed(result) Then
                            Exit For
                        End If
                    End If
                Next
                NativeMethods.ReleaseStgMedium(medium)
            End If
            Return result
        Catch ex As Exception
            Throw New Exception("Unable to Copy the file contents.", ex)
        End Try
    End Function

    Private Function SaveToFile(ByVal pdata As NativeMethods.IOleDataObject, ByVal cfFormat As DataFormats.Format, ByVal index As Integer, ByVal filename As String) As Int64
        Dim result As Int64
        Try
            Dim format As New NativeMethods.FORMATETC()
            Dim medium As New NativeMethods.STGMEDIUM()
            '
            ' Get the data for this file
            format.cfFormat = CUShort(cfFormat.Id)
            format.dwAspect = NativeMethods.DVASPECT_CONTENT
            format.lindex = index
            format.tymed = NativeMethods.TYMED_HGLOBAL Or NativeMethods.TYMED_ISTORAGE Or NativeMethods.TYMED_ISTREAM
            result = pdata.OleGetData(format, medium)
            If NativeMethods.Failed(result) Then
                Return result
            End If
            '
            ' Save the data to the specified file based on the medium
            Try
                Select Case medium.tymed
                    Case NativeMethods.TYMED_HGLOBAL
                        ' TODO: Save HGLOBAL data to a file
                        Exit Select
                    Case NativeMethods.TYMED_ISTREAM
                        SaveStreamToStream(filename, DirectCast(Marshal.GetTypedObjectForIUnknown(medium.unionmember, GetType(NativeMethods.IStream)), NativeMethods.IStream))
                        Exit Select
                    Case NativeMethods.TYMED_ISTORAGE
                        SaveStorageToStream(filename, DirectCast(Marshal.GetTypedObjectForIUnknown(medium.unionmember, GetType(NativeMethods.IStorage)), NativeMethods.IStorage))
                        Exit Select
                End Select
            Catch
                result = NativeMethods.E_FAIL
            End Try
            NativeMethods.ReleaseStgMedium(medium)
            Return result
        Catch ex As Exception
            Throw New Exception("Unable to save to file.", ex)
        End Try
    End Function

    Public Event StorageDropped As StorageDroppedHandler
    Public Event StreamDropped As StreamDroppedHandler
    Public Event FilenameDropped As FilenameDroppedHandler
    ' public event EventHandler HGlobalDropped;
    Public Sub SaveFilename(ByVal filename As String)
        Try
            RaiseEvent FilenameDropped(Me, filename)
        Catch ex As Exception
            Throw New Exception("Unable to Raise event FilenameDropped.", ex)
        End Try
    End Sub

    Public Sub SaveStreamToStream(ByVal filename As String, ByVal stream As NativeMethods.IStream)
        Try
            RaiseEvent StreamDropped(Me, filename, stream)
        Catch ex As Exception
            Throw New Exception("Unable to Raise event StreamDropped.", ex)
        End Try
    End Sub

    Public Sub SaveStorageToStream(ByVal filename As String, ByVal storage As NativeMethods.IStorage)
        Try
            RaiseEvent StorageDropped(Me, filename, storage)
        Catch ex As Exception
            Throw New Exception("Unable to Raise event StorageDropped.", ex)
        End Try
    End Sub

    Private Function QueryGetFileDescriptorArray(ByVal dataObject As NativeMethods.IOleDataObject) As Boolean
        '
        ' Called to determine if the specified data object supports
        ' CFSTR_FILEDESCRIPTORA or CFSTR_FILEDESCRIPTORW
        Dim result As Integer
        Try
            Dim format As New NativeMethods.FORMATETC()
            '
            ' Determine if the data object supports CFSTR_FILEDESCRIPTORA
            format.cfFormat = CUShort(NativeMethods.ShellClipboardFormats.CFSTR_FILEDESCRIPTORA.Id)
            format.dwAspect = NativeMethods.DVASPECT_CONTENT
            format.lindex = -1
            format.ptd = New IntPtr(0)
            format.tymed = NativeMethods.TYMED_HGLOBAL

            result = dataObject.OleQueryGetData(format)
            If NativeMethods.Succeeded(result) Then
                Return True
            End If
            '
            ' Determine if the data object supports CFSTR_FILEDESCRIPTORW
            format.cfFormat = CUShort(NativeMethods.ShellClipboardFormats.CFSTR_FILEDESCRIPTORW.Id)
            format.dwAspect = NativeMethods.DVASPECT_CONTENT
            format.lindex = -1
            format.ptd = New IntPtr(0)
            format.tymed = NativeMethods.TYMED_HGLOBAL

            result = dataObject.OleQueryGetData(format)
            If NativeMethods.Succeeded(result) Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception("Unable to query get file descriptor array.", ex)
        End Try
    End Function

End Class

Friend Class NativeMethods
    <StructLayout(LayoutKind.Sequential)> _
    Public NotInheritable Class FORMATETC
        Public cfFormat As UShort
        Public dummy As Short
        Public ptd As IntPtr
        Public dwAspect As Integer
        Public lindex As Integer
        Public tymed As Integer
    End Class

    <StructLayout(LayoutKind.Sequential)> _
    Public Class STGMEDIUM
        Public tymed As Integer
        Public unionmember As IntPtr
        Public pUnkForRelease As IntPtr
    End Class

    <StructLayout(LayoutKind.Sequential)> _
    Public NotInheritable Class FILEDESCRIPTOR
        Public dwFlags As UInteger
        Public clsid As Guid
        Public sizel As SIZEL
        Public pointl As POINTL
        Public dwFileAttributes As UInteger
        Public ftCreationTime As FILETIME
        Public ftLastAccessTime As FILETIME
        Public ftLastWriteTime As FILETIME
        Public nFileSizeHigh As UInteger
        Public nFileSizeLow As UInteger
        Public cFileName As String
    End Class

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)> _
    Public NotInheritable Class FILEDESCRIPTORA
        Public dwFlags As UInteger
        Public clsid As Guid
        Public sizel As SIZEL
        Public pointl As POINTL
        Public dwFileAttributes As UInteger
        Public ftCreationTime As FILETIME
        Public ftLastAccessTime As FILETIME
        Public ftLastWriteTime As FILETIME
        Public nFileSizeHigh As UInteger
        Public nFileSizeLow As UInteger
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)> _
       Public cFileName As String
    End Class

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)> _
    Public NotInheritable Class FILEDESCRIPTORW
        Public dwFlags As UInteger
        Public clsid As Guid
        Public sizel As SIZEL
        Public pointl As POINTL
        Public dwFileAttributes As UInteger
        Public ftCreationTime As FILETIME
        Public ftLastAccessTime As FILETIME
        Public ftLastWriteTime As FILETIME
        Public nFileSizeHigh As UInteger
        Public nFileSizeLow As UInteger
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)> _
       Public cFileName As String
    End Class

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)> _
    Public NotInheritable Class FILEGROUPDESCRIPTORA
        Public cItems As UInteger
        Public fgd As FILEDESCRIPTORA()
    End Class

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)> _
    Public NotInheritable Class FILEGROUPDESCRIPTORW
        Public cItems As UInteger
        Public fgd As FILEDESCRIPTORW()
    End Class

    Public Const CF_TEXT As Short = 1, CF_BITMAP As Short = 2, CF_METAFILEPICT As Short = 3, CF_SYLK As Short = 4, CF_DIF As Short = 5, CF_TIFF As Short = 6
    Public Const CF_OEMTEXT As Short = 7, CF_DIB As Short = 8, CF_PALETTE As Short = 9, CF_PENDATA As Short = 10, CF_RIFF As Short = 11, CF_WAVE As Short = 12
    Public Const CF_UNICODETEXT As Short = 13, CF_ENHMETAFILE As Short = 14, CF_HDROP As Short = 15, CF_LOCALE As Short = 16, CF_MAX As Short = 17, CF_OWNERDISPLAY As Short = 128
    Public Const CF_DSPTEXT As Short = 129, CF_DSPBITMAP As Short = 130, CF_DSPMETAFILEPICT As Short = 131, CF_DSPENHMETAFILE As Short = 142, CF_PRIVATEFIRST As Short = 512, CF_PRIVATELAST As Short = 767
    Public Const CF_GDIOBJFIRST As Short = 768, CF_GDIOBJLAST As Short = 1023

    Public Const DVASPECT_CONTENT As Integer = 1, DVASPECT_THUMBNAIL As Integer = 2, DVASPECT_ICON As Integer = 4, DVASPECT_DOCPRINT As Integer = 8

    Public Const TYMED_HGLOBAL As Integer = 1, TYMED_FILE As Integer = 2, TYMED_ISTREAM As Integer = 4, TYMED_ISTORAGE As Integer = 8, TYMED_GDI As Integer = 16, TYMED_MFPICT As Integer = 32
    Public Const TYMED_ENHMF As Integer = 64, TYMED_NULL As Integer = 0

    Public Const STGC_DEFAULT As Integer = 0, STGC_OVERWRITE As Integer = 1, STGC_ONLYIFCURRENT As Integer = 2, STGC_DANGEROUSLYCOMMITMERELYTODISKCACHE As Integer = 4, STGC_CONSOLIDATE As Integer = 8
    Public Const STATFLAG_DEFAULT As Integer = 0, STATFLAG_NONAME As Integer = 1, STATFLAG_NOOPEN As Integer = 2
    Public Const S_OK As Integer = 0, S_FALSE As Integer = 1, E_FAIL As Int64 = 2147500037 '(CInt(2147500037))

    Public Const STGM_DIRECT As UInteger = 0, STGM_TRANSACTED As UInteger = 65536, STGM_SIMPLE As UInteger = 134217728, STGM_READ As UInteger = 0, STGM_WRITE As UInteger = 1, STGM_READWRITE As UInteger = 2
    Public Const STGM_SHARE_DENY_NONE As UInteger = 64, STGM_SHARE_DENY_READ As UInteger = 48, STGM_SHARE_DENY_WRITE As UInteger = 32, STGM_SHARE_EXCLUSIVE As UInteger = 16, STGM_PRIORITY As UInteger = 262144, STGM_DELETEONRELEASE As UInteger = 67108864
    Public Const STGM_NOSCRATCH As UInteger = 1048576, STGM_CREATE As UInteger = 4096, STGM_CONVERT As UInteger = 131072, STGM_FAILIFTHERE As UInteger = 0, STGM_NOSNAPSHOT As UInteger = 2097152, STGM_DIRECT_SWMR As UInteger = 4194304

    Public Const FILE_ATTRIBUTE_READONLY As UInteger = 1, FILE_ATTRIBUTE_HIDDEN As UInteger = 2, FILE_ATTRIBUTE_SYSTEM As UInteger = 4, FILE_ATTRIBUTE_DIRECTORY As UInteger = 16, FILE_ATTRIBUTE_ARCHIVE As UInteger = 32, FILE_ATTRIBUTE_NORMAL As UInteger = 128
    Public Const FILE_ATTRIBUTE_TEMPORARY As UInteger = 256

    Public Shared Function Succeeded(ByVal hr As Integer) As Boolean
        Return (hr >= 0)
    End Function

    Public Shared Function Failed(ByVal hr As Int64) As Boolean
        Return (hr < 0)
    End Function

    Public Const DROPEFFECT_NONE As Integer = 0, DROPEFFECT_COPY As Integer = 1, DROPEFFECT_MOVE As Integer = 2, DROPEFFECT_LINK As Integer = 4, DROPEFFECT_SCROLL As Int64 = 2147483648

    <StructLayout(LayoutKind.Sequential)> _
    Public NotInheritable Class POINTL
        Public x As Integer
        Public y As Integer
    End Class

    <StructLayout(LayoutKind.Sequential)> _
    Public NotInheritable Class SIZEL
        Public cx As Integer
        Public cy As Integer
    End Class

    <StructLayout(LayoutKind.Sequential)> _
    Public NotInheritable Class FILETIME
        Public nFileTimeHigh As UInteger
        Public nFileTimeLow As UInteger
    End Class

    <StructLayout(LayoutKind.Sequential)> _
    Public Class STATSTG
        <MarshalAs(UnmanagedType.LPWStr)> _
        Public pwcsName As String
        Public type As Integer

        <MarshalAs(UnmanagedType.I8)> _
        Public cbSize As Long

        <MarshalAs(UnmanagedType.I8)> _
        Public mtime As Long

        <MarshalAs(UnmanagedType.I8)> _
        Public ctime As Long

        <MarshalAs(UnmanagedType.I8)> _
        Public atime As Long

        <MarshalAs(UnmanagedType.I4)> _
        Public grfMode As Integer

        <MarshalAs(UnmanagedType.I4)> _
        Public grfLocksSupported As Integer

        Public clsid_data1 As Integer

        <MarshalAs(UnmanagedType.I2)> _
        Public clsid_data2 As Short

        <MarshalAs(UnmanagedType.I2)> _
        Public clsid_data3 As Short

        <MarshalAs(UnmanagedType.U1)> _
        Public clsid_b0 As Byte

        <MarshalAs(UnmanagedType.U1)> _
        Public clsid_b1 As Byte

        <MarshalAs(UnmanagedType.U1)> _
        Public clsid_b2 As Byte

        <MarshalAs(UnmanagedType.U1)> _
        Public clsid_b3 As Byte

        <MarshalAs(UnmanagedType.U1)> _
        Public clsid_b4 As Byte

        <MarshalAs(UnmanagedType.U1)> _
        Public clsid_b5 As Byte

        <MarshalAs(UnmanagedType.U1)> _
        Public clsid_b6 As Byte

        <MarshalAs(UnmanagedType.U1)> _
        Public clsid_b7 As Byte

        <MarshalAs(UnmanagedType.I4)> _
        Public grfStateBits As Integer

        <MarshalAs(UnmanagedType.I4)> _
        Public reserved As Integer
    End Class

    <DllImport("USER32.DLL", CharSet:=CharSet.Auto, SetLastError:=True)> _
    Public Shared Function RegisterClipboardFormat(ByVal format As String) As Integer
    End Function

    <DllImport("KERNEL32.DLL", CharSet:=CharSet.None, SetLastError:=True)> _
    Public Shared Function GlobalLock(ByVal hGlobal As HandleRef) As IntPtr
    End Function

    <DllImport("KERNEL32.DLL", CharSet:=CharSet.None, SetLastError:=True)> _
    Public Shared Function GlobalUnlock(ByVal hGlobal As HandleRef) As Boolean
    End Function

    <DllImport("OLE32.DLL", ExactSpelling:=True, CharSet:=CharSet.Auto)> _
    Public Shared Function OleGetClipboard(<[In](), Out()> ByRef data As IOleDataObject) As Integer
    End Function

    <DllImport("OLE32.DLL", ExactSpelling:=True, CharSet:=CharSet.Auto, PreserveSig:=False)> _
    Public Shared Sub RegisterDragDrop(ByVal hwnd As HandleRef, ByVal target As IOleDropTarget)
    End Sub

    <DllImport("OLE32.DLL", CharSet:=CharSet.None)> _
    Public Shared Sub ReleaseStgMedium(ByVal pmedium As STGMEDIUM)
    End Sub

    <DllImport("OLE32.DLL", ExactSpelling:=True, CharSet:=CharSet.Auto, PreserveSig:=False)> _
    Public Shared Sub RevokeDragDrop(ByVal hwnd As HandleRef)
    End Sub

    <DllImport("OLE32.DLL", CharSet:=CharSet.Unicode, PreserveSig:=False)> _
    Public Shared Function StgCreateDocfile(ByVal pwcsName As String, ByVal grfMode As UInteger, ByVal reserved As UInteger) As IStorage
    End Function

    <DllImport("ole32.dll", PreserveSig:=False)> _
    Public Shared Function CreateILockBytesOnHGlobal(ByVal hGlobal As IntPtr, ByVal fDeleteOnRelease As Boolean) As ILockBytes
    End Function

    <DllImport("OLE32.DLL", CharSet:=CharSet.Auto, PreserveSig:=False)> _
    Public Shared Function GetHGlobalFromILockBytes(ByVal pLockBytes As ILockBytes) As IntPtr
    End Function

    <DllImport("OLE32.DLL", CharSet:=CharSet.Unicode, PreserveSig:=False)> _
    Public Shared Function StgCreateDocfileOnILockBytes(ByVal plkbyt As ILockBytes, ByVal grfMode As UInteger, ByVal reserved As UInteger) As IStorage
    End Function

#Region "IEnumFORMATETC interface"
    <ComImport(), Guid("00000103-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)> _
    Public Interface IEnumFORMATETC

        <PreserveSig()> _
        Function [Next](<[In](), MarshalAs(UnmanagedType.U4)> ByVal celt As Integer, <Out()> ByVal rgelt As FORMATETC, <[In](), Out(), MarshalAs(UnmanagedType.LPArray)> ByVal pceltFetched As Integer()) As Integer

        <PreserveSig()> _
        Function Skip(<[In](), MarshalAs(UnmanagedType.U4)> ByVal celt As Integer) As Integer

        <PreserveSig()> _
        Function Reset() As Integer

        <PreserveSig()> _
        Function Clone(<Out(), MarshalAs(UnmanagedType.LPArray)> ByVal ppenum As IEnumFORMATETC()) As Integer
    End Interface
#End Region

#Region "IDataObject interface"
    <ComImport(), Guid("0000010E-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown), System.Security.SuppressUnmanagedCodeSecurity()> _
    Public Interface IOleDataObject

        <PreserveSig()> _
        Function OleGetData(ByVal pFormatetc As FORMATETC, <Out()> ByVal pMedium As STGMEDIUM) As Integer

        <PreserveSig()> _
        Function OleGetDataHere(ByVal pFormatetc As FORMATETC, <[In](), Out()> ByVal pMedium As STGMEDIUM) As Integer

        <PreserveSig()> _
        Function OleQueryGetData(ByVal pFormatetc As FORMATETC) As Integer

        <PreserveSig()> _
        Function OleGetCanonicalFormatEtc(ByVal pformatectIn As FORMATETC, <Out()> ByVal pformatetcOut As FORMATETC) As Integer

        <PreserveSig()> _
        Function OleSetData(ByVal pFormatectIn As FORMATETC, ByVal pmedium As STGMEDIUM, ByVal fRelease As Integer) As Integer

        '<[return]: MarshalAs(UnmanagedType.[Interface])> _
        Function OleEnumFormatEtc(<[In](), MarshalAs(UnmanagedType.U4)> ByVal dwDirection As Integer) As IEnumFORMATETC

        <PreserveSig()> _
        Function OleDAdvise(ByVal pFormatetc As FORMATETC, <[In](), MarshalAs(UnmanagedType.U4)> ByVal advf As Integer, <[In](), MarshalAs(UnmanagedType.[Interface])> ByVal pAdvSink As Object, <Out(), MarshalAs(UnmanagedType.LPArray)> ByVal pdwConnection As Integer()) As Integer

        <PreserveSig()> _
        Function OleDUnadvise(<[In](), MarshalAs(UnmanagedType.U4)> ByVal dwConnection As Integer) As Integer

        <PreserveSig()> _
        Function OleEnumDAdvise(<Out(), MarshalAs(UnmanagedType.LPArray)> ByVal ppenumAdvise As Object()) As Integer
    End Interface
#End Region

#Region "IDataObject interface"
    <ComImport(), Guid("00000122-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)> _
    Public Interface IOleDropTarget
        <PreserveSig()> _
        Function OleDragEnter(<[In](), MarshalAs(UnmanagedType.[Interface])> ByVal pDataObj As Object, <[In](), MarshalAs(UnmanagedType.U4)> ByVal grfKeyState As Integer, <[In](), MarshalAs(UnmanagedType.U8)> ByVal pt As Long, <[In](), Out()> ByRef pdwEffect As Integer) As Integer

        <PreserveSig()> _
        Function OleDragOver(<[In](), MarshalAs(UnmanagedType.U4)> ByVal grfKeyState As Integer, <[In](), MarshalAs(UnmanagedType.U8)> ByVal pt As Long, <[In](), Out()> ByRef pdwEffect As Integer) As Integer

        <PreserveSig()> _
        Function OleDragLeave() As Integer

        <PreserveSig()> _
        Function OleDrop(<[In](), MarshalAs(UnmanagedType.[Interface])> ByVal pDataObj As Object, <[In](), MarshalAs(UnmanagedType.U4)> ByVal grfKeyState As Integer, <[In](), MarshalAs(UnmanagedType.U8)> ByVal pt As Long, <[In](), Out()> ByRef pdwEffect As Integer) As Integer
    End Interface
#End Region

#Region "ILockByte interface"
    <ComImport(), Guid("0000000A-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> _
    Public Interface ILockBytes
        Sub ReadAt(<[In](), MarshalAs(UnmanagedType.U8)> ByVal ulOffset As Long, <Out()> ByVal pv As IntPtr, <[In](), MarshalAs(UnmanagedType.U4)> ByVal cb As Integer, <Out(), MarshalAs(UnmanagedType.LPArray)> ByVal pcbRead As Integer())
        Sub WriteAt(<[In](), MarshalAs(UnmanagedType.U8)> ByVal ulOffset As Long, ByVal pv As IntPtr, <[In](), MarshalAs(UnmanagedType.U4)> ByVal cb As Integer, <Out(), MarshalAs(UnmanagedType.LPArray)> ByVal pcbWritten As Integer())
        Sub Flush()
        Sub SetSize(<[In](), MarshalAs(UnmanagedType.U8)> ByVal cb As Long)
        Sub LockRegion(<[In](), MarshalAs(UnmanagedType.U8)> ByVal libOffset As Long, <[In](), MarshalAs(UnmanagedType.U8)> ByVal cb As Long, <[In](), MarshalAs(UnmanagedType.U4)> ByVal dwLockType As Integer)
        Sub UnlockRegion(<[In](), MarshalAs(UnmanagedType.U8)> ByVal libOffset As Long, <[In](), MarshalAs(UnmanagedType.U8)> ByVal cb As Long, <[In](), MarshalAs(UnmanagedType.U4)> ByVal dwLockType As Integer)
        Sub Stat(<Out()> ByVal pstatstg As STATSTG, <[In](), MarshalAs(UnmanagedType.U4)> ByVal grfStatFlag As Integer)
    End Interface
#End Region

#Region "IStream interface"
    <ComImport(), Guid("0000000C-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)> _
    Public Interface IStream
        Function Read(ByVal buf As IntPtr, ByVal len As Integer) As Integer
        Function Write(ByVal buf As IntPtr, ByVal len As Integer) As Integer
        '<[return]: MarshalAs(UnmanagedType.I8)> _
        Function Seek(<[In](), MarshalAs(UnmanagedType.I8)> ByVal dlibMove As Long, ByVal dwOrigin As Integer) As Long
        Sub SetSize(<[In](), MarshalAs(UnmanagedType.I8)> ByVal libNewSize As Long)
        '<[return]: MarshalAs(UnmanagedType.I8)> _
        Function CopyTo(<[In](), MarshalAs(UnmanagedType.[Interface])> ByVal pstm As IStream, <[In](), MarshalAs(UnmanagedType.I8)> ByVal cb As Long, <Out(), MarshalAs(UnmanagedType.LPArray)> ByVal pcbRead As Long()) As Long
        Sub Commit(ByVal grfCommitFlags As Integer)
        Sub Revert()
        Sub LockRegion(<[In](), MarshalAs(UnmanagedType.I8)> ByVal libOffset As Long, <[In](), MarshalAs(UnmanagedType.I8)> ByVal cb As Long, ByVal dwLockType As Integer)
        Sub UnlockRegion(<[In](), MarshalAs(UnmanagedType.I8)> ByVal libOffset As Long, <[In](), MarshalAs(UnmanagedType.I8)> ByVal cb As Long, ByVal dwLockType As Integer)
        Sub Stat(<Out()> ByVal pStatstg As STATSTG, ByVal grfStatFlag As Integer)
        '<[return]: MarshalAs(UnmanagedType.[Interface])> _
        Function Clone() As IStream
    End Interface
#End Region

#Region "IStorage interface"
    <Flags()> _
    Public Enum STGM
        ' Fields
        STGM_CONVERT = 131072
        STGM_CREATE = 4096
        STGM_DELETEONRELEASE = 67108864
        STGM_DIRECT = 0
        STGM_DIRECT_SWMR = 4194304
        STGM_FAILIFTHERE = 0
        STGM_NOSCRATCH = 1048576
        STGM_NOSNAPSHOT = 2097152
        STGM_PRIORITY = 262144
        STGM_READ = 0
        STGM_READWRITE = 2
        STGM_SHARE_DENY_NONE = 64
        STGM_SHARE_DENY_READ = 48
        STGM_SHARE_DENY_WRITE = 32
        STGM_SHARE_EXCLUSIVE = 16
        STGM_SIMPLE = 134217728
        STGM_TRANSACTED = 65536
        STGM_WRITE = 1
    End Enum

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure tagRemSNB
        Public ulCntStr As UInteger
        Public ulCntChar As UInteger
        Public rgString As IntPtr
    End Structure

    <ComImport(), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("0000000B-0000-0000-C000-000000000046")> _
    Public Interface IStorage
        '<[return]: MarshalAs(UnmanagedType.[Interface])> _
        Function CreateStream(<[In](), MarshalAs(UnmanagedType.BStr)> ByVal pwcsName As String, <[In](), MarshalAs(UnmanagedType.U4)> ByVal grfMode As Integer, <[In](), MarshalAs(UnmanagedType.U4)> ByVal reserved1 As Integer, <[In](), MarshalAs(UnmanagedType.U4)> ByVal reserved2 As Integer) As IStream
        '<[return]: MarshalAs(UnmanagedType.[Interface])> _
        Function OpenStream(<[In](), MarshalAs(UnmanagedType.BStr)> ByVal pwcsName As String, ByVal reserved1 As IntPtr, <[In](), MarshalAs(UnmanagedType.U4)> ByVal grfMode As Integer, <[In](), MarshalAs(UnmanagedType.U4)> ByVal reserved2 As Integer) As IStream
        '<[return]: MarshalAs(UnmanagedType.[Interface])> _
        Function CreateStorage(<[In](), MarshalAs(UnmanagedType.BStr)> ByVal pwcsName As String, <[In](), MarshalAs(UnmanagedType.U4)> ByVal grfMode As Integer, <[In](), MarshalAs(UnmanagedType.U4)> ByVal reserved1 As Integer, <[In](), MarshalAs(UnmanagedType.U4)> ByVal reserved2 As Integer) As IStorage
        '<[return]: MarshalAs(UnmanagedType.[Interface])> _
        Function OpenStorage(<[In](), MarshalAs(UnmanagedType.BStr)> ByVal pwcsName As String, ByVal pstgPriority As IntPtr, <[In](), MarshalAs(UnmanagedType.U4)> ByVal grfMode As Integer, ByVal snbExclude As IntPtr, <[In](), MarshalAs(UnmanagedType.U4)> ByVal reserved As Integer) As IStorage
        Sub CopyTo(ByVal ciidExclude As Integer, <[In](), MarshalAs(UnmanagedType.LPArray)> ByVal pIIDExclude As Guid(), ByVal snbExclude As IntPtr, <[In](), MarshalAs(UnmanagedType.[Interface])> ByVal stgDest As IStorage)
        Sub MoveElementTo(<[In](), MarshalAs(UnmanagedType.BStr)> ByVal pwcsName As String, <[In](), MarshalAs(UnmanagedType.[Interface])> ByVal stgDest As IStorage, <[In](), MarshalAs(UnmanagedType.BStr)> ByVal pwcsNewName As String, <[In](), MarshalAs(UnmanagedType.U4)> ByVal grfFlags As Integer)
        Sub Commit(ByVal grfCommitFlags As Integer)
        Sub Revert()
        Sub EnumElements(<[In](), MarshalAs(UnmanagedType.U4)> ByVal reserved1 As Integer, ByVal reserved2 As IntPtr, <[In](), MarshalAs(UnmanagedType.U4)> ByVal reserved3 As Integer, <MarshalAs(UnmanagedType.[Interface])> ByRef ppVal As Object)
        Sub DestroyElement(<[In](), MarshalAs(UnmanagedType.BStr)> ByVal pwcsName As String)
        Sub RenameElement(<[In](), MarshalAs(UnmanagedType.BStr)> ByVal pwcsOldName As String, <[In](), MarshalAs(UnmanagedType.BStr)> ByVal pwcsNewName As String)
        Sub SetElementTimes(<[In](), MarshalAs(UnmanagedType.BStr)> ByVal pwcsName As String, <[In]()> ByVal pctime As FILETIME, <[In]()> ByVal patime As FILETIME, <[In]()> ByVal pmtime As FILETIME)
        Sub SetClass(<[In]()> ByRef clsid As Guid)
        Sub SetStateBits(ByVal grfStateBits As Integer, ByVal grfMask As Integer)
        Sub Stat(<Out()> ByVal pStatStg As STATSTG, ByVal grfStatFlag As Integer)
    End Interface

    <ComImport()> _
    <Guid("0000000d-0000-0000-C000-000000000046")> _
    <InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> _
    Public Interface IEnumSTATSTG
        ' The user needs to allocate an STATSTG array whose size is celt.
        <PreserveSig()> _
        Function [Next](ByVal celt As UInteger, <MarshalAs(UnmanagedType.LPArray), Out()> ByVal rgelt As STATSTG(), ByRef pceltFetched As UInteger) As UInteger
        Sub Skip(ByVal celt As UInteger)
        Sub Reset()
        '<[return]: MarshalAs(UnmanagedType.[Interface])> _
        Function Clone() As IEnumSTATSTG
    End Interface
#End Region

#Region "Shell Clipboard Formats"
    Public Class ShellClipboardFormats
        Public Shared ReadOnly Property CFSTR_SHELLIDLIST() As DataFormats.Format
            Get
                Try
                    Return DataFormats.GetFormat("Shell IDList Array")
                Catch ex As Exception
                    Throw New Exception("Unable to get the Shell IDList Array format.", ex)
                End Try
            End Get
        End Property

        Public Shared ReadOnly Property CFSTR_SHELLIDLISTOFFSET() As DataFormats.Format
            Get
                Try
                    Return DataFormats.GetFormat("Shell Objects Offsets")
                Catch ex As Exception
                    Throw New Exception("Unable to get the Shell Objects Offsets format.", ex)
                End Try
            End Get
        End Property

        Public Shared ReadOnly Property CFSTR_NETRESOURCES() As DataFormats.Format
            Get
                Try
                    Return DataFormats.GetFormat("Net Resource")
                Catch ex As Exception
                    Throw New Exception("Unable to get the Net Resource format.", ex)
                End Try
            End Get
        End Property

        Public Shared ReadOnly Property CFSTR_FILEDESCRIPTORA() As DataFormats.Format
            Get
                Try
                    Return DataFormats.GetFormat("FileGroupDescriptor")
                Catch ex As Exception
                    Throw New Exception("Unable to get the FileGroupDescriptor format.", ex)
                End Try
            End Get
        End Property

        Public Shared ReadOnly Property CFSTR_FILEDESCRIPTORW() As DataFormats.Format
            Get
                Try
                    Return DataFormats.GetFormat("FileGroupDescriptorW")
                Catch ex As Exception
                    Throw New Exception("Unable to get the FileGroupDescriptorW format.", ex)
                End Try
            End Get
        End Property

        Public Shared ReadOnly Property CFSTR_FILECONTENTS() As DataFormats.Format
            Get
                Try
                    Return DataFormats.GetFormat("FileContents")
                Catch ex As Exception
                    Throw New Exception("Unable to get the FileContents format.", ex)
                End Try
            End Get
        End Property

        Public Shared ReadOnly Property CFSTR_FILENAMEA() As DataFormats.Format
            Get
                Try
                    Return DataFormats.GetFormat("FileName")
                Catch ex As Exception
                    Throw New Exception("Unable to get the FileName format.", ex)
                End Try
            End Get
        End Property

        Public Shared ReadOnly Property CFSTR_FILENAMEW() As DataFormats.Format
            Get
                Try
                    Dim obj As DataFormats.Format = DataFormats.GetFormat("FileNameW")
                    Return obj
                Catch ex As Exception
                    Throw New Exception("Unable to get the FileNameW format.", ex)
                End Try
            End Get
        End Property

        Public Shared ReadOnly Property CFSTR_PRINTERGROUP() As DataFormats.Format
            Get
                Try
                    Return DataFormats.GetFormat("PrinterFriendlyName")
                Catch ex As Exception
                    Throw New Exception("Unable to get the PrinterFriendlyName format.", ex)
                End Try
            End Get
        End Property

        Public Shared ReadOnly Property CFSTR_FILENAMEMAPA() As DataFormats.Format
            Get
                Try
                    Return DataFormats.GetFormat("FileNameMap")
                Catch ex As Exception
                    Throw New Exception("Unable to get the FileNameMap format.", ex)
                End Try
            End Get
        End Property

        Public Shared ReadOnly Property CFSTR_FILENAMEMAPW() As DataFormats.Format
            Get
                Try
                    Return DataFormats.GetFormat("FileNameMapW")
                Catch ex As Exception
                    Throw New Exception("Unable to get the FileNameMapW format.", ex)
                End Try
            End Get
        End Property

        Public Shared ReadOnly Property CFSTR_SHELLURL() As DataFormats.Format
            Get
                Try
                    Return DataFormats.GetFormat("UniformResourceLocator")
                Catch ex As Exception
                    Throw New Exception("Unable to get the UniformResourceLocator format.", ex)
                End Try
            End Get
        End Property

        Public Shared ReadOnly Property CFSTR_INETURLA() As DataFormats.Format
            Get
                Try
                    Return CFSTR_SHELLURL
                Catch ex As Exception
                    Throw New Exception("Unable to get the CFSTR_SHELLURL format.", ex)
                End Try
            End Get
        End Property

        Public Shared ReadOnly Property CFSTR_INETURLW() As DataFormats.Format
            Get
                Try
                    Return DataFormats.GetFormat("UniformResourceLocatorW")
                Catch ex As Exception
                    Throw New Exception("Unable to get the UniformResourceLocatorW format.", ex)
                End Try
            End Get
        End Property

        Public Shared ReadOnly Property CFSTR_PREFERREDDROPEFFECT() As DataFormats.Format
            Get
                Try
                    Return DataFormats.GetFormat("Preferred DropEffect")
                Catch ex As Exception
                    Throw New Exception("Unable to get the Preferred DropEffect format.", ex)
                End Try
            End Get
        End Property

        Public Shared ReadOnly Property CFSTR_PERFORMEDDROPEFFECT() As DataFormats.Format
            Get
                Try
                    Return DataFormats.GetFormat("Performed DropEffect")
                Catch ex As Exception
                    Throw New Exception("Unable to get the Performed DropEffect format.", ex)
                End Try
            End Get
        End Property

        Public Shared ReadOnly Property CFSTR_PASTESUCCEEDED() As DataFormats.Format
            Get
                Try
                    Return DataFormats.GetFormat("Paste Succeeded")
                Catch ex As Exception
                    Throw New Exception("Unable to get the Paste Succeeded format.", ex)
                End Try
            End Get
        End Property

        Public Shared ReadOnly Property CFSTR_INDRAGLOOP() As DataFormats.Format
            Get
                Try
                    Return DataFormats.GetFormat("InShellDragLoop")
                Catch ex As Exception
                    Throw New Exception("Unable to get the InShellDragLoop format.", ex)
                End Try
            End Get
        End Property

        Public Shared ReadOnly Property CFSTR_DRAGCONTEXT() As DataFormats.Format
            Get
                Try
                    Return DataFormats.GetFormat("DragContext")
                Catch ex As Exception
                    Throw New Exception("Unable to get the DragContext format.", ex)
                End Try
            End Get
        End Property

        Public Shared ReadOnly Property CFSTR_MOUNTEDVOLUME() As DataFormats.Format
            Get
                Try
                    Return DataFormats.GetFormat("MountedVolume")
                Catch ex As Exception
                    Throw New Exception("Unable to get the MountedVolume format.", ex)
                End Try
            End Get
        End Property

        Public Shared ReadOnly Property CFSTR_PERSISTEDDATAOBJECT() As DataFormats.Format
            Get
                Try
                    Return DataFormats.GetFormat("PersistedDataObject")
                Catch ex As Exception
                    Throw New Exception("Unable to get the PersistedDataObject format.", ex)
                End Try
            End Get
        End Property

        Public Shared ReadOnly Property CFSTR_TARGETCLSID() As DataFormats.Format
            Get
                Try
                    Return DataFormats.GetFormat("TargetCLSID")
                Catch ex As Exception
                    Throw New Exception("Unable to get the TargetCLSID format.", ex)
                End Try
            End Get
        End Property

        Public Shared ReadOnly Property CFSTR_LOGICALPERFORMEDDROPEFFECT() As DataFormats.Format
            Get
                Try
                    Return DataFormats.GetFormat("Logical Performed DropEffect")
                Catch ex As Exception
                    Throw New Exception("Unable to get the Logical Performed DropEffect format.", ex)
                End Try
            End Get
        End Property

        Public Shared ReadOnly Property CFSTR_AUTOPLAY_SHELLIDLISTS() As DataFormats.Format
            Get
                Try
                    Return DataFormats.GetFormat("Autoplay Enumerated IDList Array")
                Catch ex As Exception
                    Throw New Exception("Unable to get the Autoplay Enumerated IDList Array format.", ex)
                End Try
            End Get
        End Property
    End Class
#End Region

End Class

Public Class DragDropProcess
    'Inherits UserControl
    Implements IDropTargetControl
    Private mfrmMain As System.Windows.Forms.Form
    Private mobjCaller As Object

    Public Event DroppedByteArrayAvailable As DroppedByteArrayAvailableHandler

    ''' <summary>
    ''' Procedure takes the object that is passed finds the form, stores both the form and control.
    ''' </summary>
    ''' <param name="sender">This is the object that the DragDrop will be captured for.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal sender As System.Object)
        Try
            mobjCaller = sender
            Dim ctrTemp As Object = sender
            Do Until TypeOf ctrTemp Is System.Windows.Forms.Form
                ctrTemp = DirectCast(ctrTemp, System.Windows.Forms.Control).Parent
            Loop
            If TypeOf ctrTemp Is System.Windows.Forms.Form Then
                mfrmMain = DirectCast(ctrTemp, System.Windows.Forms.Form)
            Else
                mfrmMain = Nothing
            End If

            ' TODO: Add any initialization after the InitializeComponent call
            If sender.Equals(mfrmMain) Then
                mfrmMain.AllowDrop = True
                AddHandler mfrmMain.VisibleChanged, AddressOf Control_VisibleChanged
            Else
                DirectCast(sender, Control).AllowDrop = True
                AddHandler DirectCast(sender, Control).VisibleChanged, AddressOf Control_VisibleChanged
            End If
            Application.OleRequired()
        Catch ex As Exception
            Throw New Exception("Unable to initialise object.", ex)
        End Try
    End Sub

    Private registeredForDragDrop As Boolean = False

    Private Sub RegisterForDragDrop()
        Try
            If Not (mfrmMain Is Nothing) Then 'mfrmMain.FindForm() IsNot Nothing Then
                Console.WriteLine("Become visible - registering: {0}", mfrmMain.Visible)
            End If

            Dim dt As New FileDropTarget(Me)
            AddHandler dt.StreamDropped, AddressOf dt_StreamDropped
            AddHandler dt.StorageDropped, AddressOf dt_StorageDropped
            AddHandler dt.FilenameDropped, AddressOf dt_FilenameDropped

            UnregisterForDragDrop()
            If mobjCaller.Equals(mfrmMain) Then
                NativeMethods.RegisterDragDrop(New HandleRef(Me, mfrmMain.Handle), dt)
            Else
                NativeMethods.RegisterDragDrop(New HandleRef(mobjCaller, DirectCast(mobjCaller, Control).Handle), dt)
            End If
            registeredForDragDrop = True
        Catch ex As Exception
            Throw New Exception("Unable to Register for Drag/Drop.", ex)
        End Try
    End Sub

    Private Sub UnregisterForDragDrop()
        Try
            Console.WriteLine("Become hidden - unregistering")

            If mobjCaller.Equals(mfrmMain) Then
                NativeMethods.RevokeDragDrop(New HandleRef(mobjCaller, mfrmMain.Handle))
            Else
                NativeMethods.RevokeDragDrop(New HandleRef(mobjCaller, DirectCast(mobjCaller, Control).Handle))
            End If
            registeredForDragDrop = False
        Catch ex As Exception
            Throw New Exception("Unable to Unregister for Drag/Drop.", ex)
        End Try
    End Sub

    Private Sub Control_VisibleChanged(ByVal sender As Object, ByVal e As EventArgs)
        'If Not System.ComponentModel.Component.DesignMode Then '
        Dim bolVisible As Boolean = False
        Try
            If sender.Equals(mfrmMain) Then
                bolVisible = mfrmMain.Visible
            Else
                bolVisible = DirectCast(mobjCaller, Control).Visible
            End If

            If bolVisible AndAlso Not registeredForDragDrop Then
                RegisterForDragDrop()
            Else
                UnregisterForDragDrop()
            End If
        Catch ex As Exception
            Throw New Exception("Problem processing the VisibleChanged event.", ex)
        End Try
    End Sub

    Private Sub dt_StreamDropped(ByVal sender As Object, ByVal filename As String, ByVal stream As NativeMethods.IStream)
        Try
            Dim stg As New NativeMethods.STATSTG()
            stream.Stat(stg, 0)

            Dim buf As IntPtr = Marshal.AllocHGlobal(CInt(stg.cbSize))
            Dim lockedBuf As IntPtr = NativeMethods.GlobalLock(New HandleRef(Me, buf))
            Dim x As Integer = stream.Read(buf, CInt(stg.cbSize))
            Dim bytes As Byte() = New Byte(CInt(stg.cbSize) - 1) {}

            Marshal.Copy(buf, bytes, 0, CInt(stg.cbSize))
            NativeMethods.GlobalUnlock(New HandleRef(Me, buf))
            Marshal.FreeHGlobal(buf)

            RaiseEvent DroppedByteArrayAvailable(Me, filename, bytes)
        Catch ex As Exception
            Console.WriteLine(ex)
            Throw New Exception("Unable to handle the Stream Dropped event.", ex)
        End Try
    End Sub

    Private Sub dt_StorageDropped(ByVal sender As Object, ByVal filename As String, ByVal storage As NativeMethods.IStorage)
        Try
            Dim grfFlags As UInteger = NativeMethods.STGM_CREATE Or NativeMethods.STGM_READWRITE Or NativeMethods.STGM_SHARE_EXCLUSIVE
            Dim lpBytes As NativeMethods.ILockBytes = NativeMethods.CreateILockBytesOnHGlobal(IntPtr.Zero, True)
            Dim lpDest As NativeMethods.IStorage = NativeMethods.StgCreateDocfileOnILockBytes(lpBytes, grfFlags, 0)

            storage.CopyTo(0, Nothing, IntPtr.Zero, lpDest)
            lpBytes.Flush()
            lpDest.Commit(NativeMethods.STGC_DEFAULT)

            Dim pStatstg As New NativeMethods.STATSTG()

            lpBytes.Stat(pStatstg, NativeMethods.STATFLAG_NONAME)

            Dim hGlobal As IntPtr = NativeMethods.GetHGlobalFromILockBytes(lpBytes)
            Dim hRef As New HandleRef(Me, hGlobal)
            Dim lpBuf As IntPtr = NativeMethods.GlobalLock(hRef)
            Dim bytes As Byte() = New Byte(CInt(pStatstg.cbSize) - 1) {}

            Marshal.Copy(lpBuf, bytes, 0, CInt(pStatstg.cbSize))
            RaiseEvent DroppedByteArrayAvailable(Me, filename, bytes)

            NativeMethods.GlobalUnlock(hRef)
            Marshal.ReleaseComObject(lpDest)
            Marshal.ReleaseComObject(lpBytes)
        Catch ex As Exception
            Console.WriteLine(ex)
            Throw New Exception("Unable to handle the Storage Dropped event.", ex)
        End Try

    End Sub

#Region "IFileDropTargetControl Members"
    Private Sub Activate() Implements IDropTargetControl.Activate
        'Dim f As Form = FindForm()
        Try
            If Not mfrmMain Is Nothing Then
                mfrmMain.Activate()
            End If
        Catch ex As Exception
            'Zygo.Client.Utility.Errors.ShowException(ex)
        End Try
    End Sub

    Private Sub DragDrop() Implements IDropTargetControl.DragDrop
        'pictureBox1.Image = Nothing
    End Sub

    Private Sub DragEnter(ByVal filename As String) Implements IDropTargetControl.DragEnter
        'pictureBox1.Image = GetImageForFilename(filename)
    End Sub

    Private Sub DragLeave() Implements IDropTargetControl.DragLeave
        'pictureBox1.Image = Nothing
    End Sub

    Private Sub DragOver() Implements IDropTargetControl.DragOver

    End Sub
#End Region

    Private Sub dt_FilenameDropped(ByVal sender As Object, ByVal filename As String)
        Try
            'If DroppedByteArrayAvailable IsNot Nothing Then
            Using stream As System.IO.FileStream = System.IO.File.OpenRead(filename)
                Dim length As Long = stream.Length
                Dim buffer As Byte() = New Byte(CInt(length) - 1) {}
                stream.Read(buffer, 0, CInt(length))

                Dim shortFilename As String = System.IO.Path.GetFileName(filename)
                RaiseEvent DroppedByteArrayAvailable(Me, shortFilename, buffer)

            End Using
            'End If
        Catch ex As Exception
            Throw New Exception("Unable to handle the Filename Dropped event.", ex)
        End Try
    End Sub
End Class
