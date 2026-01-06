Option Explicit On

Imports Microsoft.Win32

Module UtilRegistry

    Public Sub CreateWinTexMainKey()

        Dim regKey As RegistryKey

        regKey = Registry.LocalMachine.OpenSubKey("Software\WinTex", True)

        If IsNothing(regKey) Then
            regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE", True)
            regKey.CreateSubKey("WinTex")
        End If

        regKey.Close()
    End Sub

    Public Sub AssignWinTexCameraKeys()

        Dim regKey As RegistryKey

        CreateWinTexMainKey()

        regKey = Registry.LocalMachine.OpenSubKey("Software\WinTex", True)

        regKey.SetValue("Camera_No", oCamera.nCameraNo, RegistryValueKind.DWord)
        regKey.SetValue("Camera_VideoCompressor", oCamera.nVideoCompressor, RegistryValueKind.DWord)
        regKey.SetValue("Camera_Width", oCamera.nWidth, RegistryValueKind.DWord)
        regKey.SetValue("Camera_Height", oCamera.nHeight, RegistryValueKind.DWord)
        regKey.SetValue("Camera_FrameRate", oCamera.nFrameRate, RegistryValueKind.DWord)
        regKey.SetValue("Camera_FileName", oCamera.cFileName, RegistryValueKind.String)

        regKey.Close()
    End Sub

    Public Sub RetrieveWinTexCameraKeys()

        Dim regKey As RegistryKey

        CreateWinTexMainKey()

        regKey = Registry.LocalMachine.OpenSubKey("Software\WinTex", True)

        oCamera.nCameraNo = regKey.GetValue("Camera_No", 0)
        oCamera.nVideoCompressor = regKey.GetValue("Camera_VideoCompressor", 0)
        oCamera.nWidth = regKey.GetValue("Camera_Width", 640)
        oCamera.nHeight = regKey.GetValue("Camera_Height", 480)
        oCamera.nFrameRate = regKey.GetValue("Camera_FrameRate", 30)
        oCamera.cFileName = regKey.GetValue("Camera_FileName", "c:\WinTexResim")

        regKey.Close()
    End Sub


End Module
