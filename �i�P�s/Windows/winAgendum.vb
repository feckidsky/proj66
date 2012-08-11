Public Class winAgendum
    WithEvents access As Database.Access
    Dim Filter As DataGridViewFilter

    Private Sub winAgendum_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Filter = New DataGridViewFilter(dgItem)
        Filter.AddTextFilter("編號", "種類", "內容")
        Filter.AddBoolFilter("已完成")

    End Sub

    Private Sub winAgendum_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BeginUpdateList()
    End Sub

    Public Overloads Sub Show(ByVal db As Database.Access)
        Me.access = db
        MyBase.Show()
        MyBase.BringToFront()

    End Sub

    Public Sub UpdateList(ByVal progress As Database.Access.Progress)
        Dim dt As DataTable = access.GetAgendumList(Finished, MaxCount, progress)
        Try
            If Not Me.IsDisposed Then Me.Invoke(New Action(Of DataTable)(AddressOf UpdateDataGridView), dt)
        Catch ex As Exception

        End Try
        progress.Finish()
    End Sub

    Public Sub UpdateDataGridView(ByVal dt As DataTable)
        dgItem.DataSource = dt
        UpdateTitle("Label", "編號")
        UpdateTitle("Kind", "種類")
        UpdateTitle("Message", "內容")
        UpdateTitle("Modify", "最後修改時間")
        UpdateTitle("Finished", "已完成")

        If Filter IsNot Nothing Then Filter.UpdateComboBox()
    End Sub

    Private Sub UpdateTitle(ByVal Label As String, ByVal Text As String)
        dgItem.Columns(Label).HeaderText = Text
    End Sub


    Dim Finished As Boolean
    Dim MaxCount As Integer
    Public Sub BeginUpdateList()
        Finished = ckFinished.Checked
        MaxCount = NumberBox1.Text
        Dim thread As New Threading.Thread(New Threading.ParameterizedThreadStart(AddressOf UpdateList))
        Dim dialog As New ProgressDialog
        dialog.Thread = thread
        Dim progress As New Database.Access.Progress(AddressOf dialog.UpdateProgress, "讀取待辦事項", AddressOf dialog.Finish)
        dialog.Show()
        thread.Start(progress)
    End Sub

    Private Sub access_ChangedAgendum(ByVal sender As Object, ByVal Agendum As Database.Agendum) Handles access.ChangedAgendum

    End Sub

    Private Sub access_CreatedAgendum(ByVal sender As Object, ByVal Agendum As Database.Agendum) Handles access.CreatedAgendum

    End Sub

    Private Sub access_DeletedAgendum(ByVal sender As Object, ByVal Agendum As Database.Agendum) Handles access.DeletedAgendum

    End Sub


    Private Sub btUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btUpdate.Click
        BeginUpdateList()
    End Sub


End Class