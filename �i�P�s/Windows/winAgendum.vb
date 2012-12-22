Public Class winAgendum
    WithEvents access As Database.Access

    Private Sub winAgendum_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        For Each box As AgendumBox In Panel1.Controls
            If box.Changed Then
                If box.Mode = AgendumBox.Type.Create Then
                    access.AddAgendum(box.Data)
                ElseIf box.Mode = AgendumBox.Type.Edit Then
                    access.ChangeAgendum(box.Data)
                End If
            End If
        Next
        access = Nothing
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
        Panel1.Controls.Clear()
        For i As Integer = 0 To dt.Rows.Count - 1
            AddAgendum(Database.Agendum.GetFrom(dt.Rows(i)))
            'Dim box As New AgendumBox
            'box.Data =
            'box.Anchor = AnchorStyles.Top + AnchorStyles.Left + Right
            'box.Top = box.Height * i + 2
            'box.Mode = AgendumBox.Type.Edit
            'box.Tag = i
            'Panel1.Controls.Add(box)
        Next

    End Sub

    Public Sub AddNewAgendum()
        Dim box As New AgendumBox
        box.Anchor = AnchorStyles.Top + AnchorStyles.Left + AnchorStyles.Right
        box.Top = box.Height * Panel1.Controls.Count + 2
        box.Mode = AgendumBox.Type.Create
        box.Tag = Panel1.Controls.Count
        box.Width = Panel1.Width - 4
        Panel1.Controls.Add(box)
    End Sub

    Public Sub AddAgendum(ByVal data As Database.Agendum)
        Dim box As New AgendumBox
        box.Anchor = AnchorStyles.Top + AnchorStyles.Left + AnchorStyles.Right
        box.Data = data
        box.Top = box.Height * Panel1.Controls.Count + 2
        box.Mode = AgendumBox.Type.Edit
        box.Tag = Panel1.Controls.Count
        box.Width = Panel1.Width - 4
        Panel1.Controls.Add(box)
    End Sub

    Public Sub ChangeAgendum(ByVal data As Database.Agendum)
        For i As Integer = 0 To Panel1.Controls.Count - 1
            If data.Label = CType(Panel1.Controls(i), AgendumBox).Data.Label Then
                CType(Panel1.Controls(i), AgendumBox).Data = data
            End If
        Next
    End Sub

    Public Sub DeleteAgendum(ByVal data As Database.Agendum)
        For i As Integer = 0 To Panel1.Controls.Count - 1
            If data.Label = CType(Panel1.Controls(i), AgendumBox).Data.Label Then
                CType(Panel1.Controls(i), AgendumBox).Data = data
                Panel1.Controls.RemoveAt(i)
                Relocation()

                Exit Sub
            End If
        Next
    End Sub

    Public Sub Relocation()
        For i As Integer = 0 To Panel1.Controls.Count - 1
            Panel1.Controls(i).Top = Panel1.Controls(i).Height * i + 2
        Next
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
        If Me.IsDisposed Then Exit Sub
        If Me.InvokeRequired Then
            Dim inv As New Action(Of Database.Agendum)(AddressOf ChangeAgendum)
            Me.Invoke(inv, Agendum)
        End If
    End Sub

    Private Sub access_CreatedAgendum(ByVal sender As Object, ByVal Agendum As Database.Agendum) Handles access.CreatedAgendum
        If Me.IsDisposed Then Exit Sub
        If Me.InvokeRequired Then
            Dim inv As New Action(Of Database.Agendum)(AddressOf AddAgendum)
            Me.Invoke(inv, Agendum)
        End If
    End Sub

    Private Sub access_DeletedAgendum(ByVal sender As Object, ByVal Agendum As Database.Agendum) Handles access.DeletedAgendum
        If Me.IsDisposed Then Exit Sub
        If Me.InvokeRequired Then
            Dim inv As New Action(Of Database.Agendum)(AddressOf DeleteAgendum)
            Me.Invoke(inv, Agendum)
        End If
    End Sub


    Private Sub btUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btUpdate.Click
        BeginUpdateList()
    End Sub

    Private Sub 新增CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 新增CToolStripMenuItem.Click
        AddNewAgendum()
    End Sub
End Class