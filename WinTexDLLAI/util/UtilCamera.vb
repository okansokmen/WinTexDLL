Option Strict Off
Option Explicit On 

Imports DirectX.Capture
Imports System
Imports System.IO
Imports System.Drawing

Module UtilCamera

     Public Sub ConfParamCam()

        Dim size As Size

        CaptureInformation.Camera = Dispositivos.VideoInputDevices(oCamera.nCameraNo)
        CaptureInformation.CaptureInfo = New DirectX.Capture.Capture(CaptureInformation.Camera, Nothing)

        CaptureInformation.CaptureInfo.Stop()
        ' Change the compressor
        CaptureInformation.CaptureInfo.VideoCompressor = Dispositivos.VideoCompressors(oCamera.nVideoCompressor)
        ' Change the image size
        size = New Size(oCamera.nWidth, oCamera.nHeight)
        CaptureInformation.CaptureInfo.FrameSize = size
        ' Change the number of frames per second
        CaptureInformation.CaptureInfo.FrameRate = oCamera.nFrameRate
        ' capture file
        CaptureInformation.PathVideo = oCamera.cFileName + ".avi"
        CaptureInformation.CaptureInfo.Filename = oCamera.cFileName + ".avi"
    End Sub
End Module