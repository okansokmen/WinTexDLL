Public NotInheritable Class AboutBoxWinTex


    Private Sub AboutBoxWinTex_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Set the title of the form.
        Dim ApplicationTitle As String = "WinTexDLL"
        'If My.Application.Info.Title <> "" Then
        '    ApplicationTitle = My.Application.Info.Title
        'Else
        '    ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        'End If
        Me.Text = String.Format("About {0}", ApplicationTitle)
        ' Initialize all of the text displayed on the About Box.
        Me.LabelProductName.Text = "WinTexDLL.DLL" ' My.Application.Info.ProductName
        Me.LabelVersion.Text = "Version " + HTMain.cWinTexDLLVersion ' String.Format("Version {0}", My.Application.Info.Version.ToString)
        Me.LabelCopyright.Text = My.Application.Info.Copyright
        Me.LabelCompanyName.Text = My.Application.Info.CompanyName
        Me.TextBoxDescription.Text = "WinTex programının, kullanıcı tanımlı raporlama, PDF gösterme, PDF çevirme, e-mail çekme (Google üzerinden), metin düzenleme (Microsoft Word dökümanlarını düzeltebilirsiniz), tablo düzenleme (Microsoft Excel dökümanlarını düzeltebilirsiniz), 40 yabancı dilde mütercim tercüman, resim birleştirme, resim gösterme, fotoğraf ve film çekme, vs.. fonksiyonlarını tamamlayan modüldür." ' My.Application.Info.Description
    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub

    Private Sub TableLayoutPanel_Paint(sender As Object, e As PaintEventArgs) Handles TableLayoutPanel.Paint

    End Sub
End Class
