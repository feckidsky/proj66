Imports 進銷存.Database.StructureBase

Public Class winMain
    WithEvents access As Database.Access = Program.DB

    Private Sub winMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        'dgSales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells

    End Sub

    Public Sub New()
        InitialProgram()
        ' 此為 Windows Form 設計工具所需的呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。

        UpdateSalesList()
    End Sub



    Public Sub UpdateSalesList()

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

        Dim dt As Data.DataTable = DB.GetSalesList(StartTime, EndTime)



        dgSales.Columns.Clear()
        For i As Integer = 0 To dt.Columns.Count - 1
            dgSales.Columns.Add(dt.Columns(i).ColumnName, dt.Columns(i).ColumnName)
        Next
        'dgSales.DataSource = dt

        For i As Integer = 0 To dt.Rows.Count - 1
            Dim arr As String() = Array.ConvertAll(dt.Rows(i).ItemArray, Function(o As Object) o.ToString)
            dgSales.Rows.Add(arr)

            dgSales.Rows.Item(i).Cells("付款方式").Value = TypeOfPaymentsDescribe(dgSales.Rows(i).Cells("付款方式").Value)

        Next
    End Sub

    Public Sub OpenSales()
        If dgSales.SelectedRows.Count <= 0 Then
            MsgBox("您至少必須選擇一個項目!")
            Exit Sub
        End If


        Dim row As DataGridViewRow = dgSales.SelectedRows(0)

        Dim SalesLabel As String = row.Cells("單號").Value
        winSales.Open(SalesLabel)

    End Sub




    Private Sub dgSales_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgSales.CellMouseDoubleClick

        OpenSales()

    End Sub

    Private Sub access_ChangedSales(ByVal sales As Database.StructureBase.Sales, ByVal GoodsList() As Database.StructureBase.SalesGoods) Handles access.ChangedSales
        UpdateSalesList()
    End Sub

    Private Sub access_CreatedSales(ByVal sales As Database.StructureBase.Sales, ByVal GoodsList() As Database.StructureBase.SalesGoods) Handles access.CreatedSales
        UpdateSalesList()
    End Sub

    Private Sub access_DeletedSales(ByVal sales As Database.StructureBase.Sales) Handles access.DeletedSales
        UpdateSalesList()
    End Sub

    Private Sub 進貨記錄ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 進貨記錄ToolStripMenuItem1.Click
        winStockInLog.Show()
    End Sub

    Private Sub 查詢庫存ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 查詢庫存ToolStripMenuItem.Click
        winStockList.Show()
    End Sub

    Private Sub rToday_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rToday.CheckedChanged, r30Day.CheckedChanged, rUserTime.CheckedChanged
        UpdateSalesList()
    End Sub

    Private Sub dtpStart_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpStart.ValueChanged, dtpEnd.ValueChanged
        UpdateSalesList()
    End Sub

    Private Sub 銷貨AToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 銷貨AToolStripMenuItem.Click, 銷貨ToolStripMenuItem.Click
        winSales.Create()
    End Sub

    Private Sub 修改CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 修改CToolStripMenuItem.Click
        OpenSales()
    End Sub


    Private Sub 刪除DToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 刪除DToolStripMenuItem.Click
        If dgSales.SelectedRows.Count <= 0 Then
            MsgBox("您至少必須選擇一個項目!")
            Exit Sub
        End If

        If MsgBox("刪除該筆銷貨單，您確定要這麼做？", MsgBoxStyle.OkCancel + MsgBoxStyle.Exclamation) = MsgBoxResult.Cancel Then
            Exit Sub
        End If

        Dim sales As New Sales
        sales.Label = dgSales.SelectedRows(0).Cells(0).Value
        DB.DeleteSales(sales)
    End Sub

    Private Sub 商品項目GToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 商品項目GToolStripMenuItem.Click
        winGoodsList.Show()
    End Sub

    Private Sub 供應商ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 供應商ToolStripMenuItem.Click
        winSupplierList.Show()
    End Sub
End Class