Public Class winStockList
    WithEvents access As Database.Access = Program.DB

    Dim SelectMode As Boolean = False

    Private Sub winStock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
        DataGridView1.DataSource = DT

        'Dim Kinds As New List(Of String)

        'For Each e As Data.DataColumn In DT.Columns
        '    If Kinds.Exists ( Function ( s As String ) s=)
        'Next

    End Sub


    Private Sub 更新ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 更新ToolStripMenuItem.Click
        UpdateStockList()
    End Sub

    Public SelectedRow As DataGridViewRow = Nothing

    Public Function SelectGood() As DataGridViewRow
        SelectMode = True
        SelectedRow = Nothing
        MyBase.ShowDialog()

        Return SelectedRow 'DataGridView1.SelectedRows.Item(0)
    End Function

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If DataGridView1.SelectedCells(0).RowIndex >= DataGridView1.Rows.Count Then Exit Sub
        SelectedRow = DataGridView1.Rows(e.RowIndex)
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub


    Private Sub access_CreatedStock(ByVal stock As Database.StructureBase.Stock) Handles access.CreatedStock
        UpdateStockList()
    End Sub

    Private Sub 進貨ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 進貨ToolStripMenuItem.Click
        winStockIn.Create()
    End Sub
End Class