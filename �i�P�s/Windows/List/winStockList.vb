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

    Private Sub winStockList_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        access = Nothing
    End Sub

    Private Sub winStockList_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        Config()


    End Sub

    Private Sub cbStock_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbStock.SelectedIndexChanged
        access = ClientManager(cbStock.Text)
        BeginUpdateStockList()
    End Sub

    Private Sub Config()
        Filter.SetTextFilter("商品編號", GoodsFilterText)
        cbStock.Items.Clear()
        'cbStock.Items.Add("本機庫存")
        cbStock.Items.AddRange(ClientManager.GetNameList())

        cbStock.Text = access.Name ' = 0
        UpdateTitleText()
        'BeginUpdateStockList()
    End Sub

    Public Overloads Sub Show(ByVal DB As Database.Access)
        access = DB
        If Not CheckAuthority(1) Then
            Exit Sub
        End If

        MyBase.Show()
        MyBase.BringToFront()
        GoodsFilterText = ""
        'Config()
        'Filter.SetTextFilter("商品編號", "")
        'cbStock.SelectedIndex = 0 'UpdateStockList()
        'Filter.Filter()

    End Sub

    Dim UpdateStockListHandler As New Action(AddressOf BeginUpdateStockList)
    Dim DT As Data.DataTable = Nothing
    Public Sub BeginUpdateStockList()
        If Not Me.Created Then Exit Sub

        Dim dialog As New ProgressDialog
        dialog.Thread = New Threading.Thread(New Threading.ParameterizedThreadStart(AddressOf UpdateStockList))
        dialog.Start("讀取庫存資料")

    End Sub

    Public Sub UpdateStockList(ByVal Progress As Database.Access.Progress)
        DT = access.GetStockListWithHistoryPrice(, , Progress)
        Try
            If Not Me.IsDisposed Then Me.Invoke(New Action(Of DataTable)(AddressOf UpdateDataTable), DT)
        Catch
        End Try
        If Progress.Finished Then Progress.Finish()
    End Sub

    Public Sub UpdateDataTable(ByVal dt As DataTable)
        dgItemList.DataSource = dt

        If dt Is Nothing OrElse dt.Columns.Count = 0 Then Exit Sub

        'dgItemList.Columns("商品編號").Visible = False

        Try
            If Filter IsNot Nothing Then Filter.UpdateComboBox()
            dgItemList.Sort(dgItemList.Columns(0), System.ComponentModel.ListSortDirection.Descending)
        Catch

        End Try
        'Dim count As Integer = dgItemList.RowCount
        'For i As Integer = 0 To count - 1
        '    Dim cell As New DataGridViewRowHeaderCell()
        '    cell.Value = i
        '    'cell.
        '    dgItemList.Rows(i).HeaderCell = cell
        'Next

        'Dim cell As New DataGridViewRowHeaderCell()
        'AddHandler cell.
    End Sub

    'Class DataGridViewRowHandlerCellEx
    '    Inherits DataGridViewRowHeaderCell

    '    Protected Overrides Sub Paint(ByVal graphics As System.Drawing.Graphics, ByVal clipBounds As System.Drawing.Rectangle, ByVal cellBounds As System.Drawing.Rectangle, ByVal rowIndex As Integer, ByVal cellState As System.Windows.Forms.DataGridViewElementStates, ByVal value As Object, ByVal formattedValue As Object, ByVal errorText As String, ByVal cellStyle As System.Windows.Forms.DataGridViewCellStyle, ByVal advancedBorderStyle As System.Windows.Forms.DataGridViewAdvancedBorderStyle, ByVal paintParts As System.Windows.Forms.DataGridViewPaintParts)
    '        MyBase.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts)

    '    End Sub
    'End Class



    Private Sub 更新ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        BeginUpdateStockList()
    End Sub

    Public SelectedRow As DataGridViewRow = Nothing
    Public Function SelectStock(ByVal db As Database.Access) As DataGridViewRow
        access = db
        SelectMode = True
        SelectedRow = Nothing
        GoodsFilterText = ""
        If Me.Visible Then Me.Visible = False
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

        If Me.Visible Then Me.Visible = False
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

        If SelectMode Then
            SelectedRow = dgItemList.Rows(e.RowIndex)
            Me.Close()
        Else
            Modify()
        End If
        'Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub AddRow(ByVal arr() As Object)
        If DT IsNot Nothing Then DT.Rows.Add(arr)
    End Sub

    Private Sub RemoveRow(ByVal Label As String)
        Try
            If DT Is Nothing Then Exit Sub
            Dim rows() As DataRow = DT.Select("庫存編號 = '" & Label & "'")
            For Each r As DataRow In rows
                DT.Rows.Remove(r)
            Next
        Catch

        End Try
    End Sub

    Private Sub ChangeRow(ByVal arr() As Object)
        Try
            For i As Integer = 0 To DT.Rows.Count - 1
                If arr(1) = DT.Rows(i)("庫存編號") Then
                    DT.Rows(i).ItemArray = arr
                End If
            Next
        Catch

        End Try
    End Sub

    Private Sub access_ChangedStock(ByVal sender As Object, ByVal stock As Database.Stock) Handles access.ChangedStock
        Dim dt As DataTable = access.GetStockListWithHistoryPrice(stock.Label)
        If dt Is Nothing OrElse dt.Rows.Count = 0 Then Exit Sub
        Dim arr As Object = dt.Rows(0).ItemArray
        If Me.InvokeRequired Then
            Try
                If Not Me.IsDisposed Then Me.Invoke(New Action(Of Object)(AddressOf ChangeRow), arr)
            Catch
            End Try
        Else
            ChangeRow(arr)
        End If
    End Sub

    Private Sub access_DeletedStock(ByVal sender As Object, ByVal stock As Database.Stock) Handles access.DeletedStock
        If Me.InvokeRequired Then
            Try
                If Not Me.IsDisposed Then Me.Invoke(New Action(Of String)(AddressOf RemoveRow), stock.Label)
            Catch
            End Try
        Else
            RemoveRow(stock.Label)
        End If
    End Sub

    Private Sub access_CreatedStock(ByVal sender As Object, ByVal stock As Database.Stock) Handles access.CreatedStock
        Dim dt As DataTable = access.GetStockListWithHistoryPrice(stock.Label)
        If dt Is Nothing OrElse dt.Rows.Count = 0 Then Exit Sub
        Dim arr As Object = dt.Rows(0).ItemArray
        If Me.InvokeRequired Then
            Try
                If Not Me.IsDisposed Then Me.Invoke(New Action(Of Object)(AddressOf AddRow), arr)
            Catch
            End Try
        Else
            AddRow(arr)
        End If
    End Sub

    Private Sub 進貨ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 進貨ToolStripMenuItem.Click, 進貨ToolStripMenuItem1.Click
        winStockIn.Create(access)
    End Sub


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        UpdateTitleText()
    End Sub

    Private Sub UpdateTitleText()
        Dim connectState As String = ""
        If access IsNot Nothing AndAlso access.GetType Is GetType(Database.AccessClient) Then
            'If access.GetType Is GetType(Database.AccessClient) Then
            Dim c As Database.Access = access
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


    Private Sub 修改ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 修改ToolStripMenuItem.Click
        Modify()
    End Sub

    Public Sub Modify()
        If Not Filter.HasSelectedItem() Then
            MsgBox("您必須選取一個項目")
            Exit Sub
        End If

        Dim label As String = dgItemList.SelectedRows(0).Cells("庫存編號").Value
        Dim stock As Database.Stock = access.GetStock(label)
        winStockIn.Open(stock, access)

    End Sub

    Private Sub 刪除ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 刪除ToolStripMenuItem.Click
        If Not CheckAuthority(2) Then Exit Sub
        If Not Filter.HasSelectedItem() Then
            MsgBox("您必須選取一個項目")
            Exit Sub
        End If

        Dim stock As New Database.Stock
        stock.Label = dgItemList.SelectedRows(0).Cells("庫存編號").Value

        Dim Count As Integer = access.GetSalesListByStockLabel(stock.Label).Rows.Count
        For Each c As Database.Access In ClientManager.Client
            If c.Connected Then Count += c.GetSalesListByStockLabel(stock.Label).Rows.Count
        Next

        If Count > 0 Then
            MsgBox("無法刪除，該商品已經有銷貨記錄!")
            Exit Sub
        End If

        If MsgBox("您現在要刪除該筆進貨記錄，確定要這麼做？", MsgBoxStyle.OkCancel + MsgBoxStyle.Exclamation) = MsgBoxResult.Cancel Then Exit Sub
        stock.Label = dgItemList.SelectedRows(0).Cells(1).Value
        stock.GoodsLabel = dgItemList.SelectedRows(0).Cells(0).Value
        access.DeleteStock(stock)
    End Sub

    Private Sub dgItemList_RowPostPaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPostPaintEventArgs) Handles dgItemList.RowPostPaint
        Dim DataGridView As DataGridView = sender
        Dim solidBrush As SolidBrush = New SolidBrush(DataGridView.RowHeadersDefaultCellStyle.ForeColor)
        Dim xh As Integer = e.RowIndex + 1
        Dim StringWidth As Integer = e.Graphics.MeasureString(xh.ToString, DataGridView.Font).Width
        e.Graphics.DrawString(xh.ToString(), e.InheritedRowStyle.Font, solidBrush, DataGridView.Rows(e.RowIndex).HeaderCell.Size.Width - (StringWidth + 5), e.RowBounds.Location.Y + 4)
    End Sub
End Class