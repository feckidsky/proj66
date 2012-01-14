Public Class winGoodsList

    Private Sub winGoodsList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UpdateList()
    End Sub

    Private Sub UpdateList()
        dgGoodsList.DataSource = DB.GetGoodsList()

    End Sub

    Private Sub 新增AToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 新增AToolStripMenuItem.Click
        winGoods.Add(GetNewGoods())
    End Sub
End Class