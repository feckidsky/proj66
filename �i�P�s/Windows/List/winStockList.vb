Public Class winStockList
    WithEvents access As Database.Access '= Program.DB

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

        Config()


    End Sub

    Private Sub cbStock_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbStock.SelectedIndexChanged
        access = Client(cbStock.Text)
        UpdateStockList()
    End Sub

    Private Sub Config()
        Filter.SetTextFilter("商品編號", GoodsFilterText)
        cbStock.Items.Clear()
        'cbStock.Items.Add("本機庫存")
        cbStock.Items.AddRange(Client.GetNameList())

        cbStock.Text = access.Name ' = 0
        UpdateTitleText()
        UpdateStockList()
    End Sub

    Public Overloads Sub Show(ByVal DB As Database.Access)
        access = DB
        If Not CheckAuthority(1) Then
            Exit Sub
        End If

        MyBase.Show()
        MyBase.BringToFront()
        GoodsFilterText = ""
        Config()
        'Filter.SetTextFilter("商品編號", "")
        'cbStock.SelectedIndex = 0 'UpdateStockList()
        'Filter.Filter()

    End Sub

    Dim UpdateStockListHandler As New Action(AddressOf UpdateStockList)
    Public Sub UpdateStockList()
        If Not Me.Created Then Exit Sub
        If Me.InvokeRequired Then
            Me.Invoke(UpdateStockListHandler)
            Exit Sub
        End If
        Dim DT As Data.DataTable = access.GetStockListWithHistoryPrice()
        'If cbStock.SelectedIndex = 0 Then

        '    DT = Program.myDatabase.GetStockListWithHistoryPrice() 'DB.GetStockList()
        'Else
        '    DT = access.GetStockListWithHistoryPrice
        'End If

        dgItemList.DataSource = DT

        If DT.Columns.Count = 0 Then Exit Sub

        'dgItemList.Columns("商品編號").Visible = False

        If Filter IsNot Nothing Then Filter.UpdateComboBox()
    End Sub


    Private Sub 更新ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        UpdateStockList()
    End Sub

    Public SelectedRow As DataGridViewRow = Nothing
    Public Function SelectStock(ByVal db As Database.Access) As DataGridViewRow
        access = db
        SelectMode = True
        SelectedRow = Nothing
        GoodsFilterText = ""
        MyBase.ShowDialog()

        If db.Name <> cbStock.Text Then
            MsgBox("您無法選擇不在本店的庫存", MsgBoxStyle.Exclamation)
            SelectedRow = Nothing
        End If
        Return SelectedRow 'DataGridView1.SelectedRows.Item(0)
    End Function

    Public Function SelectStock(ByVal GoodsLabel As String, ByVal DB As Database.Access) As DataGridViewRow
        access = DB
        SelectMode = True
        SelectedRow = Nothing
        GoodsFilterText = GoodsLabel
        MyBase.ShowDialog()
        If SelectedRow IsNot Nothing And DB.Name <> cbStock.Text Then
            MsgBox("您無法選擇不在本店的庫存", MsgBoxStyle.Exclamation)
            SelectedRow = Nothing
        End If
        Return SelectedRow
    End Function

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgItemList.CellDoubleClick
        If e.RowIndex = -1 Then Exit Sub
        If dgItemList.SelectedCells(0).RowIndex >= dgItemList.Rows.Count Then Exit Sub
        SelectedRow = dgItemList.Rows(e.RowIndex)
        Me.Close()
        'Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub access_ChangedStock(ByVal sender As Object, ByVal stock As Database.Stock) Handles access.ChangedStock
        UpdateStockList()
    End Sub

    Private Sub access_DeletedStock(ByVal sender As Object, ByVal stock As Database.Stock) Handles access.DeletedStock
        UpdateStockList()
    End Sub


    Private Sub access_CreatedStock(ByVal sender As Object, ByVal stock As Database.Stock) Handles access.CreatedStock
        UpdateStockList()
    End Sub

    Private Sub 進貨ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 進貨ToolStripMenuItem.Click
        winStockIn.Create(access)
    End Sub


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        UpdateTitleText()
    End Sub

    Private Sub UpdateTitleText()
        Dim connectState As String = ""
        If access.GetType Is GetType(Database.AccessClient) Then
            Dim c As Database.AccessClient = access
            connectState = IIf(c.Connected, "-已連線", "-斷線")
        End If
        Me.Text = "庫存查詢-" & cbStock.Text & connectState
    End Sub


    Private Sub 列印PToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 列印PToolStripMenuItem.Click
        DataGridViewPrintDialog.ShowDialog("庫存清單", dgItemList)
    End Sub

    Private Sub cbStock_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbStock.TextChanged

    End Sub

    Private Sub 調貨ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 調貨ToolStripMenuItem.Click, 調貨ToolStripMenuItem1.Click
        If dgItemList.SelectedRows.Count = 0 Then
            MsgBox("您尚未選任何項目")
            Exit Sub
        End If

        If Not access.Connected Then
            MsgBox(access.Name & "已經斷線")
            Exit Sub
        End If

        If CurrentAccess Is access Then
            DialogStockMove.DialogStockOut(access, dgItemList.SelectedRows(0).Cells("庫存編號").Value)
        Else
            DialogStockMove.DialogStockIn(access, dgItemList.SelectedRows(0).Cells("庫存編號").Value)

        End If

    End Sub


End Class