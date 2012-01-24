Public Class winContractList

    Enum Mode
        Normal = 0
        SelectItem = 1
    End Enum

    Dim work As Mode

    WithEvents access As Database.Access = Program.DB
    Dim Filter As DataGridViewFilter

    Private Sub winContactList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Filter = New DataGridViewFilter(dgGoodsList)
        Filter.AddTextFilter("編號", "合約", "備註")
        Filter.AddNumberFilter("預付額", "佣金", "折扣")
        Filter.AddBoolFilter("有效")
        UpdateList()
    End Sub

    Public Overloads Sub Show()
        If Not CheckAuthority(2) Then Exit Sub
        work = Mode.Normal
        MyBase.Show()
    End Sub


    Public Function SelectDialog() As Database.Contract
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        work = Mode.SelectItem
        If MyBase.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Return GetSelectedItem()
        Else
            Return Database.Contract.Null()
        End If
    End Function


    Private Sub UpdateList()
        dgGoodsList.DataSource = DB.GetContractList()

        UpdateTitle("Label", "編號")
        UpdateTitle("Enable", "有效")
        UpdateTitle("Name", "合約")
        UpdateTitle("Commission", "佣金")
        UpdateTitle("Discount", "折扣")
        UpdateTitle("Prepay", "預付額")
        UpdateTitle("Note", "備註")
        If Filter IsNot Nothing Then Filter.UpdateComboBox()
    End Sub

    Private Sub UpdateTitle(ByVal Label As String, ByVal Text As String)
        dgGoodsList.Columns(Label).HeaderText = Text
    End Sub

    Private Sub 新增AToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 新增AToolStripMenuItem.Click, 新增CToolStripMenuItem1.Click
        winContract.Create(GetNewContract())
    End Sub

    Private Sub 修改CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 修改CToolStripMenuItem.Click
        EditItem()
    End Sub

    Private Sub EditItem()

        If GetSelectedItem().IsNull Then
            MsgBox("您必須選擇一個項目")
            Exit Sub
        End If


        winContract.Open(GetSelectedItem())
    End Sub

    Public Function GetSelectedItem() As Database.Contract
        If Not Filter.HasSelectedItem Then Return Database.Contract.Null()
        Dim dt As DataTable = dgGoodsList.DataSource

        Dim label As String = dgGoodsList.SelectedRows(0).Cells(0).Value

        For Each r As Data.DataRow In dt.Rows
            If r.Item(0) = label Then
                Return Database.Contract.GetFrom(r)
            End If
        Next
        Return Database.Contract.Null()
        'Return Database.Goods.GetFrom(dt.Rows(dgGoodsList.SelectedRows(0).Index))
    End Function


    Private Sub 刪除DToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 刪除DToolStripMenuItem.Click
        If Not CheckAuthority(2) Then Exit Sub

        Dim selectedItem As Database.Contract = GetSelectedItem()
        If selecteditem.IsNull() Then
            MsgBox("您必須選擇一個項目")
            Exit Sub
        End If


        Dim count As Integer = DB.GetSalesListByContractLabel(SelectedItem.Label).Rows.Count
        If count > 0 Then
            MsgBox("商品項目已經有訂單資料，無法刪除!")
            Exit Sub
        End If

        If MsgBox("這麼做將會刪除該項合約項目，您確定要這麼做？", MsgBoxStyle.OkCancel + MsgBoxStyle.Question) = MsgBoxResult.Cancel Then
            Exit Sub
        End If



        DB.DeleteContract(SelectedItem)
    End Sub

    Private Sub dgGoodsList_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgGoodsList.CellMouseDoubleClick
        If work = Mode.Normal Then
            EditItem()
        Else
            Me.DialogResult = Windows.Forms.DialogResult.OK
        End If


    End Sub

    Private Sub access_ChangedItem(ByVal goods As Database.StructureBase.Contract) Handles access.ChangedContract, access.CreatedContract, access.DeletedContract
        UpdateList()
    End Sub


    Private Sub 刪除DToolStripMenuItem_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles 刪除DToolStripMenuItem.Disposed

    End Sub
End Class
