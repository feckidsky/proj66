Public Class winStockInLog
    WithEvents access As Database.Access = Program.DB

    Dim StartTime As Date
    Dim EndTime As Date

    Dim Filter As DataGridViewFilter

    Private Sub winStockInLog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpStart.Value = Today
        dtpEnd.Value = Today
        Filter = New DataGridViewFilter(dgStockLog)
        Filter.AddTextFilter("庫存編號", "供應商", "種類", "廠牌", "品名", "備註", "IMEI")
        Filter.AddNumberFilter("進貨價", "定價", "數量")
        UpdateStockInLog()
    End Sub

    Public Overloads Sub Show()
        If Not CheckAuthority(1) Then Exit Sub
        MyBase.Show()
    End Sub

    Public Sub UpdateStockInLog()

        Dim StartTime As Date
        Dim EndTime As Date
        If rToday.Checked Then
            StartTime = Today.Date
            EndTime = Today.Date.AddDays(1)
        ElseIf r30Day.Checked Then
            StartTime = Today.Date.AddDays(-30)
            EndTime = Today.Date.AddDays(1)
        Else
            StartTime = dtpStart.Value.Date
            EndTime = dtpEnd.Value.Date.AddDays(1)
        End If


        dgStockLog.DataSource = DB.GetStockLog(StartTime, EndTime)
        If Filter IsNot Nothing Then Filter.UpdateComboBox()
    End Sub



    '進貨事件/更新進貨清單/刪除進貨資料
    Private Sub access_CreatedStock(ByVal stock As Database.StructureBase.Stock) Handles access.CreatedStock, access.ChangedStock, access.DeletedStock
        UpdateStockInLog()
    End Sub



    '改變篩選方式
    Private Sub r_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rToday.CheckedChanged, r30Day.CheckedChanged, rUserTime.CheckedChanged
        UpdateStockInLog()
    End Sub

    '改變篩選時間
    Private Sub dtpStart_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpStart.ValueChanged, dtpEnd.ValueChanged
        If rUserTime.Checked Then UpdateStockInLog()
    End Sub

    Private Sub dgStockLog_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgStockLog.CellMouseDoubleClick
        If e.RowIndex = -1 Then Exit Sub
        Dim label As String = dgStockLog.Rows(e.RowIndex).Cells(0).Value
        Dim stock As Database.Stock = DB.GetStock(label)
        winStockIn.Open(stock)
    End Sub


    '快捷功能表
    Private Sub 進貨AToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 進貨AToolStripMenuItem.Click, 進貨ToolStripMenuItem.Click

        winStockIn.Create()
    End Sub


    Private Sub 修改CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 修改CToolStripMenuItem.Click

        If Not Filter.HasSelectedItem() Then
            MsgBox("您必須選取一個項目")
            Exit Sub
        End If

        Dim label As String = dgStockLog.SelectedRows(0).Cells(0).Value
        Dim stock As Database.Stock = DB.GetStock(label)
        winStockIn.Open(stock)
    End Sub

    Private Sub 刪除DToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 刪除DToolStripMenuItem.Click
        If Not CheckAuthority(2) Then Exit Sub
        If Not Filter.HasSelectedItem() Then
            MsgBox("您必須選取一個項目")
            Exit Sub
        End If

        Dim stock As New Database.Stock
        stock.Label = dgStockLog.SelectedRows(0).Cells(0).Value

        If DB.GetSalesListByStockLabel(stock.Label).Rows.Count > 0 Then
            MsgBox("無法刪除，該商品已經有銷貨記錄!")
            Exit Sub
        End If

        If MsgBox("您現在要刪除該筆進貨記錄，確定要這麼做？", MsgBoxStyle.OkCancel + MsgBoxStyle.Exclamation) = MsgBoxResult.Cancel Then Exit Sub
        stock.Label = dgStockLog.SelectedRows(0).Cells(0).Value
        DB.DeleteStock(stock)
    End Sub
End Class