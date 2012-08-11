Imports System.Windows.Forms

Public Class BackupDialog
    WithEvents MailSender As New MailSender

    Private Sub BackupDialog_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Config.DirBackupEnable = ckDir.Checked
        Config.EmailBackupEnable = ckEmail.Checked
        ConfigSave()
    End Sub

    Private Sub BackupDialog_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not btBackup.Enabled Then
            If MsgBox("目前正在備份中，確定要離開？", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                Try
                    MailSender.SendAsyncCancel()

                Catch ex As Exception

                End Try
            Else
                e.Cancel = True
            End If

        End If

    End Sub

    Private Sub BackupDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtEmail.Text = MailInfo.To
        txtDir.Text = Config.BackupDir
        ckDir.Checked = Config.DirBackupEnable
        ckEmail.Checked = Config.EmailBackupEnable
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        'Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub



    Private Sub btDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btDir.Click
        FolderBrowserDialog1.SelectedPath = txtDir.Text
        If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtDir.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub btBackup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btBackup.Click
        If Not ckEmail.Checked And Not ckDir.Checked Then
            lbStatu.Text = "未選擇備份方式"
            Exit Sub
        End If

        ' 檢查路徑是否可使用
        If ckDir.Checked Then
            If txtDir.Text = "" Then
                lbStatu.Text = "路徑不能是空字串"
                Exit Sub
            Else
                Try
                    Dim dir As String = IO.Path.GetDirectoryName(txtDir.Text)
                    If Not IO.Directory.Exists(dir) Then IO.Directory.CreateDirectory(dir)
                Catch
                    lbStatu.Text = Err.Description
                    Exit Sub
                End Try
            End If
        End If

        ProgressBar1.Visible = True
        btBackup.Enabled = False

        Dim name As String = Now.ToString("yyyyMMddHHmmss")
        Dim tmpDir As String = My.Application.Info.DirectoryPath & "\Data\tmp\"
        Dim tmpMdb As String = tmpDir & name & ".mdb"
        Dim tmpZip As String = tmpDir & name & ".bk"
        If Not IO.Directory.Exists(tmpDir) Then IO.Directory.CreateDirectory(tmpDir)
        Dim delFiles As String() = IO.Directory.GetFiles(tmpDir)
        For Each f As String In delFiles
            Try
                IO.File.Delete(f)
            Catch ex As Exception

            End Try
        Next

        lbStatu.Text = "壓縮資料"
        Database.Access.RepairAccess(myDatabase.BasePath, tmpDir & name & ".mdb")
        Zip(tmpMdb, tmpZip)
        IO.File.Delete(tmpMdb)

        If ckDir.Checked Then
            lbStatu.Text = "備份檔案到指定路徑"
            If Not IO.Directory.Exists(txtDir.Text) Then IO.Directory.CreateDirectory(txtDir.Text)
            IO.File.Copy(tmpZip, IO.Path.Combine(txtDir.Text, name & ".bk"))
        End If

        If ckEmail.Checked Then
            lbStatu.Text = "發送郵件"
            Dim result As Boolean = MailSender.BeginSend(MailInfo.Server, MailInfo.Port, MailInfo.ID, MailInfo.Password, "[" & Config.ServerName & "]進銷存系統資料庫備份-" & Now.ToString("yyyy/MM/dd HH:mm:ss"), "", Config.ServerName & "<" & MailInfo.From & ">", txtEmail.Text, New String() {tmpZip}, tmpZip)
            ProgressBar1.Visible = result
            btBackup.Enabled = Not result
            Try
                If Not result Then IO.File.Delete(tmpZip)
            Catch
            End Try
        Else
            lbStatu.Text = "備份完成"
            MsgBox("備份結束")
            btBackup.Enabled = True
            IO.File.Delete(tmpZip)
        End If


    End Sub

    Dim SendCompletedHander As New Action(Of Object, System.ComponentModel.AsyncCompletedEventArgs)(AddressOf MailSender_SendCompleted)
    Private Sub MailSender_SendCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs) Handles MailSender.SendCompleted
        If Me.InvokeRequired Then
            If Not Me.IsDisposed Then Me.Invoke(SendCompletedHander, sender, e)
            Exit Sub
        End If

        '刪除暫存檔
        Try
            IO.File.Delete(e.UserState)
        Catch ex As Exception

        End Try

        Try
            If e.Cancelled Then
                lbStatu.Text = "備份作業已取消"
                MsgBox("備份作業已取消")
            ElseIf e.Error IsNot Nothing Then
                lbStatu.Text = e.Error.Message
                MsgBox(e.Error.Message)
            Else
                lbStatu.Text = "備份結束"
                MsgBox("備份結束")
            End If

            ProgressBar1.Visible = False
            btBackup.Enabled = True
        Catch
        End Try
    End Sub


    Public Shared Sub Unzip(ByVal source As String, ByVal des As String)
        Dim ms As New IO.FileStream(des, IO.FileMode.Create)
        Dim ss As New IO.FileStream(source, IO.FileMode.Open)
        'Dim ms As IO.MemoryStream = New IO.MemoryStream(Data)

        Dim compressedzipStream As IO.Compression.GZipStream = New IO.Compression.GZipStream(ss, IO.Compression.CompressionMode.Decompress, True)
        Dim buff(4095) As Byte
        Dim read As Long = compressedzipStream.Read(buff, 0, buff.Length)
        'Dim output As New IO.MemoryStream()
        ms.Write(buff, 0, read)
        Do While (read > 0)
            read = compressedzipStream.Read(buff, 0, buff.Length)
            ms.Write(buff, 0, read)
        Loop

        ms.Close()
        ms.Dispose()
        compressedzipStream.Close()
        compressedzipStream.Dispose()
        ss.Close()
        ss.Dispose()
    End Sub

    Public Shared Sub Zip(ByVal source As String, ByVal des As String)
        Dim ms As New IO.FileStream(des, IO.FileMode.Create)
        Dim ss As New IO.FileStream(source, IO.FileMode.Open)
        Dim compressedzipStream As IO.Compression.GZipStream = New IO.Compression.GZipStream(ms, IO.Compression.CompressionMode.Compress, True)

        Dim ReadToEnd As Boolean = False
        Dim BufferSize As Long = 4096
        Dim data(BufferSize - 1) As Byte

        Do
            Try
                If ss.Length - ss.Position < BufferSize Then  '如果剩下位傳送的位元組不到BufferSize
                    System.Array.Resize(data, ss.Length - ss.Position)
                End If
            Catch

            End Try

            Try
                ReadToEnd = ss.Length - ss.Position <= BufferSize
            Catch
                ReadToEnd = True
            End Try

            Try
                ss.Read(data, 0, data.Length)
                ' ms.Write(data, 0, data.Length)
                compressedzipStream.Write(data, 0, data.Length)
            Catch
            End Try
        Loop Until ReadToEnd

        compressedzipStream.Close()
        compressedzipStream.Dispose()

        ms.Close()
        ms.Dispose()
        ss.Close()
        ss.Dispose()

    End Sub

    Private Sub RestoreDatabase(ByVal zipFile As String, ByVal destineFile As String)
        If IO.File.Exists(destineFile) Then
            Try
                IO.File.Delete(destineFile)
            Catch ex As Exception
                MsgBox(ex.Message)
                Exit Sub
            End Try
        End If
        Unzip(zipFile, destineFile)
        MsgBox("還原成功!")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        OpenBackupFileDialog.Multiselect = False
        OpenBackupFileDialog.InitialDirectory = txtDir.Text
        If OpenBackupFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then

            If MsgBox("執行還原後，目前資料庫的資料將被取代，確定要繼續？", MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                Exit Sub
            End If

            RestoreDatabase(OpenBackupFileDialog.FileName, myDatabase.BasePath)
        End If
    End Sub


End Class
