Public Class DialogErrorFileList

    Dim access As Database.Access

    WithEvents downloader As TCPTool.Client.Receiver

    Public Overloads Sub ShowDialog(ByVal db As Database.Access)
        access = db
        ListBox1.Items.Clear()
        ListBox1.Items.AddRange(access.GetErrorLogFileNames())
        MyBase.ShowDialog()
    End Sub

    Dim DownloadDialog As ProgressDialog

    Private Sub btDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btDownload.Click
        If ListBox1.SelectedItems.Count = 0 Then Exit Sub

        Dim File As String = ListBox1.SelectedItems(0)
        SaveFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
        SaveFileDialog1.FileName = IO.Path.GetFileName(File)
        If SaveFileDialog1.ShowDialog() <> Windows.Forms.DialogResult.OK Then Exit Sub
        downloader = access.Download(File, SaveFileDialog1.FileName)
        DeleteHandler = Nothing
        DownloadDialog = New ProgressDialog
        DownloadDialog.Text = "下載檔案 - " & IO.Path.GetFileName(downloader.destFile)
        DownloadDialog.Show()

    End Sub

    Private Sub downloader_Downloaded(ByVal sender As Object, ByVal stream As IO.Stream) Handles downloader.Received
        DownloadDialog.Finish()
        If MsgBox("下載完成，是否要立即開啟", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Try
                Process.Start(SaveFileDialog1.FileName)
            Catch
                MsgBox(Err.Description)
            End Try
        End If
        If DeleteHandler IsNot Nothing Then DeleteHandler(CType(sender, TCPTool.Client.Receiver).sourceFile)
    End Sub

    Private Sub downloader_Progress(ByVal sender As Object, ByVal percent As Integer) Handles downloader.Progress
        DownloadDialog.UpdateProgress(percent, "下載中...")
    End Sub

    Dim DeleteHandler As Action(Of String)
    Private Sub btDownloadAccess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btDownloadAccess.Click
        Dim source As String = access.GetCloneBasePath()
        SaveFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
        SaveFileDialog1.FileName = IO.Path.GetFileName("base.mdb")
        If SaveFileDialog1.ShowDialog() <> Windows.Forms.DialogResult.OK Then Exit Sub
        downloader = access.Download(source, SaveFileDialog1.FileName)
        DeleteHandler = New Action(Of String)(AddressOf access.DeleteFile)
        DownloadDialog = New ProgressDialog
        DownloadDialog.Text = "下載檔案 - " & IO.Path.GetFileName(downloader.destFile)
        DownloadDialog.Show()

    End Sub
End Class