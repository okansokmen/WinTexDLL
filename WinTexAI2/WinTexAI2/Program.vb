Imports System.Windows.Forms

Module Program
    Sub Main(args As String())
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        Dim oHTMain As New HTMain(args)
        Application.Run(oHTMain)
    End Sub
End Module