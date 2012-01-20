
Public Class winGoodsList

    Enum Mode
        Normal = 0
        SelectItem = 1
    End Enum

    Dim work As Mode

    WithEvents access As Database.Access = Program.DB

    Private Sub winGoodsList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UpdateList()
    End Sub

    Public Overloads Sub Show()
        If Not CheckAuthority(1) Then Exit Sub
        work = Mode.Normal
        MyBase.Show()
    End Sub


    Public Function SelectDialog() As Database.Goods
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        work = Mode.SelectItem
        If MyBase.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Return GetSelectedGoods()
        Else
            Return Database.Goods.Null()
        End If
    End Function


    Private Sub UpdateList()
        dgItemList.DataSource = DB.GetGoodsList()

        UpdateTitle("Label", "編號")
        UpdateTitle("Name", "品名")
        UpdateTitle("Kind", "種類")
        UpdateTitle("Brand", "廠牌")
        UpdateTitle("Note", "備註")

    End Sub

    Private Sub UpdateTitle(ByVal Label As String, ByVal Text As String)
        dgItemList.Columns(Label).HeaderText = Text
    End Sub

    Private Sub 新增AToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 新增AToolStripMenuItem.Click, 新增CToolStripMenuItem1.Click
        winGoods.Create(GetNewGoods())
    End Sub

    Private Sub 修改CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 修改CToolStripMenuItem.Click
        EditGoods()
    End Sub

    Private Sub EditGoods()

        If dgItemList.SelectedRows.Count <= 0 Then
            MsgBox("您必須選擇一個項目")
            Exit Sub
        End If


        winGoods.Open(GetSelectedGoods())
    End Sub

    Public Function GetSelectedGoods() As Database.Goods
        Dim dt As DataTable = dgItemList.DataSource

        Dim label As String = dgItemList.SelectedRows(0).Cells(0).Value

        For Each r As Data.DataRow In dt.Rows
            If r.Item(0) = label Then
                Return Database.Goods.GetFrom(r)
            End If
        Next
        Return Database.Goods.Null()
        'Return Database.Goods.GetFrom(dt.Rows(dgGoodsList.SelectedRows(0).Index))
    End Function


    Private Sub 刪除DToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 刪除DToolStripMenuItem.Click
        If Not CheckAuthority(2) Then Exit Sub

        If dgItemList.SelectedRows.Count <= 0 Then
            MsgBox("您必須選擇一個項目")
            Exit Sub
        End If

        Dim SelectedGoods As Database.Goods = GetSelectedGoods()
        Dim count As Integer = DB.GetStockLogByGoodsLabel(SelectedGoods.Label).Rows.Count
        If count > 0 Then
            MsgBox("商品項目已經有進貨資料，無法刪除!")
            Exit Sub
        End If

        If MsgBox("這麼做將會刪除該商品項目，您確定要這麼做？", MsgBoxStyle.OkCancel + MsgBoxStyle.Question) = MsgBoxResult.Cancel Then
            Exit Sub
        End If



        DB.DeleteGoods(SelectedGoods)
    End Sub

    Private Sub dgGoodsList_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgItemList.CellMouseDoubleClick
        If work = Mode.Normal Then
            EditGoods()
        Else
            Me.DialogResult = Windows.Forms.DialogResult.OK
        End If


    End Sub

    Private Sub access_ChangedGoods(ByVal goods As Database.StructureBase.Goods) Handles access.ChangedGoods, access.CreatedGoods, access.DeletedGoods
        UpdateList()
    End Sub


End Class