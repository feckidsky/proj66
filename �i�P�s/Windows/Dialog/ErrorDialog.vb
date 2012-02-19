Public Class ErrorDialog


    Private Sub ErrorDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MsgBox("系統發生未預期的錯誤，程式即將關閉，請將紅色視窗內的訊息回報給設計人員!!", MsgBoxStyle.Exclamation)
    End Sub


    Dim Dir As String

    Public Overloads Sub ShowDialog(ByVal dir As String, ByVal msg As String)
        Me.TextBox1.Text = msg
        Me.TextBox1.SelectionStart = TextBox1.Text.Length
        Me.Dir = dir
        Me.ShowDialog()
        Application.Exit()
    End Sub

    Private Sub 開啟錯誤記錄資料夾ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 開啟錯誤記錄資料夾ToolStripMenuItem.Click
        Shell("Explorer.exe " & Dir, vbNormalFocus)
    End Sub


End Class