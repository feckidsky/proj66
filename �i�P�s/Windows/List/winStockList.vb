Public Class winStockList
    WithEvents access As Database.Access = Program.DB

    Dim SelectMode As Boolean = False
    Dim Filter As DataGridViewFilter
    Dim GoodsFilterText As String = ""


    Public Sub New()

        ' 此為 Windows Form 設計工具所需的呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。
        Filter = New DataGridViewFilter(dgItemList)
        Filter.AddTextFilter("商品編號", "庫存編號", "IMEI", "品名", "廠牌", "種類", "備註")
        Filter.AddNumberFilter("數量", "售價")
    End Sub

    Private Sub winStockList_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Filter.SetTextFilter("商品編號", GoodsFilterText)
        cbStock.Items.Clear()
        cbStock.Items.Add("本機庫存")
        cbStock.Items.AddRange(Client.GetNameList())

        cbStock.SelectedIndex = 0
        UpdateTitleText()
    End Sub

    Private Sub cbStock_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbStock.SelectedIndexChanged
        UpdateStockList()
    End Sub


    Public Overloads Sub Show()
        If Not CheckAuthority(1) Then
            Exit Sub
        End If
        MyBase.Show()
    End Sub

    Public Sub UpdateStockList()
        Dim DT As Data.DataTable
        If cbStock.SelectedIndex = 0 Then
            DT = DB.GetStockListWithHistoryPrice() 'DB.GetStockList()
        Else
            DT = Client(cbStock.Text).GetStockListWithHistoryPrice
        End If

        dgItemList.DataSource = DT
        dgItemList.Columns("商品編號").Visible = False

        If Filter IsNot Nothing Then Filter.UpdateComboBox()
    End Sub


    Private Sub 更新ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 更新ToolStripMenuItem.Click
        UpdateStockList()
    End Sub

    Public SelectedRow As DataGridViewRow = Nothing
    Public Function SelectStock() As DataGridViewRow
        SelectMode = True
        SelectedRow = Nothing
        MyBase.ShowDialog()
        If cbStock.SelectedIndex > 0 Then
            MsgBox("您無法選擇不在本店的庫存", MsgBoxStyle.Exclamation)
            SelectedRow = Nothing
        End If
        Return SelectedRow 'DataGridView1.SelectedRows.Item(0)
    End Function

    Public Function SelectStock(ByVal GoodsLabel As String) As DataGridViewRow
        SelectMode = True
        SelectedRow = Nothing
        GoodsFilterText = GoodsLabel
        MyBase.ShowDialog()
        If cbStock.SelectedIndex > 0 Then
            MsgBox("您無法選擇不在本店的庫存", MsgBoxStyle.Exclamation)
            SelectedRow = Nothing
        End If
        Return SelectedRow
    End Function

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgItemList.CellDoubleClick
        If e.RowIndex = -1 Then Exit Sub
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


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        UpdateTitleText()
    End Sub

    Private Sub UpdateTitleText()
        Dim connectState As String = ""
        If Client(cbStock.Text) IsNot Nothing Then
            connectState = IIf(Client(cbStock.Text).Client.Connected, "-已連線", "-斷線")

        End If
        Me.Text = "庫存查詢-" & cbStock.Text & connectState
    End Sub

End Class