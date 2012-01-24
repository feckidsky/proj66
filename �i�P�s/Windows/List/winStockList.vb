Public Class winStockList
    WithEvents access As Database.Access = Program.DB

    Dim SelectMode As Boolean = False
    Dim Filter As DataGridViewFilter

    Private Sub winStock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Filter = New DataGridViewFilter(dgItemList)
        Filter.AddTextFilter("庫存編號", "IMEI", "品名", "廠牌", "種類", "備註")
        Filter.AddNumberFilter("數量", "售價")
        UpdateStockList()
    End Sub

    Public Overloads Sub Show()
        If Not CheckAuthority(1) Then
            Exit Sub
        End If
        MyBase.Show()
    End Sub

    Public Sub UpdateStockList()
        Dim DT As Data.DataTable = DB.GetStockList()
        dgItemList.DataSource = DT

        Filter.UpdateComboBox()
    End Sub


    Private Sub 更新ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 更新ToolStripMenuItem.Click
        UpdateStockList()
    End Sub

    Public SelectedRow As DataGridViewRow = Nothing
    Public Function SelectStock() As DataGridViewRow
        SelectMode = True
        SelectedRow = Nothing
        MyBase.ShowDialog()

        Return SelectedRow 'DataGridView1.SelectedRows.Item(0)
    End Function

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgItemList.CellDoubleClick
        If dgItemList.SelectedCells(0).RowIndex >= dgItemList.Rows.Count Then Exit Sub
        SelectedRow = dgItemList.Rows(e.RowIndex)
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub


    Private Sub access_CreatedStock(ByVal stock As Database.StructureBase.Stock) Handles access.CreatedStock
        UpdateStockList()
    End Sub

    Private Sub 進貨ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 進貨ToolStripMenuItem.Click
        winStockIn.Create()
    End Sub


End Class