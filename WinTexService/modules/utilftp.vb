Option Explicit On
Option Strict On

Imports System.Net

Module utilftp

    Public Function DownloadSingleFile(ftpAddress As String,
                                          ftpUser As String,
                                          ftpPassword As String,
                                          fileToDownload As String,
                                          downloadTargetFolder As String,
                                          deleteAfterDownload As Boolean) As Boolean

        DownloadSingleFile = False

        Try

            Dim sFtpFile As String = ftpAddress & fileToDownload

            Dim sTargetFileName = System.IO.Path.GetFileName(sFtpFile)
            sTargetFileName = sTargetFileName.Replace("/", "\")
            sTargetFileName = downloadTargetFolder & sTargetFileName

            My.Computer.Network.DownloadFile(sFtpFile, sTargetFileName, ftpUser, ftpPassword)

            If deleteAfterDownload Then
                Dim ftpRequest As FtpWebRequest = Nothing

                ftpRequest = CType(WebRequest.Create(sFtpFile), FtpWebRequest)

                With ftpRequest
                    .Credentials = New NetworkCredential(ftpUser, ftpPassword)
                    .Method = WebRequestMethods.Ftp.DeleteFile
                End With

                Dim response As FtpWebResponse = CType(ftpRequest.GetResponse(), FtpWebResponse)
                response.Close()

                ftpRequest = Nothing

                DownloadSingleFile = True

            End If

        Catch ex As Exception
            ErrDisp(ex, "DownloadSingleFile")
        End Try
    End Function
End Module
