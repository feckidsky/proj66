Public Class DialogErrorFileList

    Dim access As Database.Access

    Public Overloads Sub ShowDialog(ByVal db As Database.Access)
        access = db
        ListBox1.Items.Clear()
        ListBox1.Items.AddRange(access.GetErrorLogFileNames())
        MyBase.ShowDialog()
    End Sub


    Private Sub btDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btDownload.Click
        If ListBox1.SelectedItems.Count = 0 Then Exit Sub

        Dim File As String = ListBox1.SelectedItems(0)
        SaveFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
        SaveFileDialog1.FileName = IO.Path.GetFileName(File)
        If SaveFileDialog1.ShowDialog() <> Windows.Forms.DialogResult.OK Then Exit Sub
        access.Download(File, SaveFileDialog1.FileName)

        If MsgBox("下載完成，是否要立即開啟", MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then Exit Sub
        Try
            Process.Start(SaveFileDialog1.FileName)
        Catch
            MsgBox(Err.Description)
        End Try
    End Sub
End Class