Imports System

Public Module PlatformUtils
    ' Returns "64-bit" if the current process is 64-bit, otherwise "32-bit"
    Public Function GetProcessBitness() As String
        Return If(Environment.Is64BitProcess, "64-bit", "32-bit")
    End Function

    ' Convenience: True if the current process is 64-bit
    Public Function IsProcess64Bit() As Boolean
        Return Environment.Is64BitProcess
    End Function
End Module