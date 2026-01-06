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

End Module
