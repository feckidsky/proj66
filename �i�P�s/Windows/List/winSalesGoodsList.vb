Public Class winSalesGoodsList
    WithEvents access As Database.Access
    Dim Filter As DataGridViewFilter

    Dim SelectedRow As DataGridViewRow = Nothing


    Public Overloads Function ShowDialog(ByVal DB As Database.Access) As DataGridViewRow
        If Me.Visible Then Me.Visible = False
        access = DB
        SelectedRow = Nothing
        ReadStockGoodsList()

        MyBase.ShowDialog()
        Return SelectedRow
    End Function



    Public Overloads Sub Show(ByVal DB As Database.Access)
        access = DB
        MyBase.Show()
        MyBase.BringToFront()
        ReadStockGoodsList()
    End Sub
    Dim SalesGoodsListVisiblePath As String = My.Application.Info.DirectoryPath & "\SalesGoodsListVisible.xml"
    Dim SalesGoodsListFilterPatch As String = My.Application.Info.DirectoryPath & "\SalesGoodsListFilterHis.xml"
    Private Sub winSalesGoodsList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim lst As String() = Code.LoadXml(Of String())(SalesGoodsListFilterPatch, New String() {})
        cbKeyWord.Items.Clear()
        cbKeyWord.Items.AddRange(lst)
    End Sub

    Private Sub ReadStockGoodsList()
        If dtpStart.Value.Date > dtpEnd.Value.Date Then
            Dim tmp As Date = dtpStart.Value
            dtpStart.Value = dtpEnd.Value
            dtpEnd.Value = tmp
        End If

        Dim cnd As String = cbKeyWord.Text
        Dim st As Date = IIf(ckTime.Checked, dtpStart.Value.Date, Nothing)
        Dim ed As Date = IIf(ckTime.Checked, dtpEnd.Value.Date.AddDays(1).AddSeconds(-1), Nothing)
        'Dim table As DataTable = access.GetSalesGoodsLis(st, ed, cnd, cnd, cnd, cnd, cnd, cnd, cnd, cnd, 101)
        Dim table As DataTable = access.GetSalesGoodsLis(st, ed, cnd, 101)
        If table.Rows.Count = 101 Then
            lbResult.Text = "查詢筆數超出100筆，目前僅顯示100筆，若找不到資料請縮小篩選範圍"
            table.Rows.RemoveAt(100)
        Else
            lbResult.Text = "符合條件的筆數有 " & table.Rows.Count & " 筆"
        End If
        If table IsNot Nothing Then
            table.Columns("SalesLabel").ColumnName = "銷貨編號"
            table.Columns("SalesDate").ColumnName = "銷貨日期"
            table.Columns("Personnel").ColumnName = "銷貨人員"
            table.Columns("Customer").ColumnName = "客戶"
            table.Columns("StockLabel").ColumnName = "庫存編號"
            table.Columns("GoodsLabel").ColumnName = "商品編號"
            table.Columns("Kind").ColumnName = "種類"
            table.Columns("Brand").ColumnName = "廠牌"
            table.Columns("Name").ColumnName = "品名"
            table.Columns("Cost").ColumnName = "成本"
            table.Columns("SellingPrice").ColumnName = "賣價"
            table.Columns("Number").ColumnName = "數量"
            table.Columns("Note").ColumnName = "備註"
        End If

        dgItemList.DataSource = table

        If table IsNot Nothing Then
            DataGridViewVisibleDialog.SetVisible(dgItemList, Code.LoadXml(Of String())(SalesGoodsListVisiblePath, GetColumns(table)))
        End If
        Try
            Filter.UpdateComboBox()
        Catch
        End Try
    End Sub

    Public Function GetColumns(ByVal table As DataTable) As String()
        If table Is Nothing Then Return New String() {}
        Dim columns As New List(Of String)
        For i As Integer = 0 To table.Columns.Count - 1
            columns.Add(table.Columns(i).ColumnName)
        Next
        Return columns.ToArray
    End Function

    Public Sub New()

        ' 此為 Windows Form 設計工具所需的呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。
        Filter = New DataGridViewFilter(dgItemList)
        Filter.AddTextFilter("銷貨編號", "庫存編號", "商品編號", "銷貨人員", "客戶", "種類", "廠牌", "品名", "備註")
        Filter.AddNumberFilter("成本", "賣價", "數量")
    End Sub


    Private Sub btRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btRead.Click
        ReadStockGoodsList()
        Dim text As String = Strings.Trim(cbKeyWord.Text)
        If text <> "" AndAlso Not cbKeyWord.Items.Exists(Function(s As String) s = text) Then
            cbKeyWord.Items.Add(text)
            If cbKeyWord.Items.Count > 50 Then cbKeyWord.Items.RemoveRange(50, cbKeyWord.Items.Count - 50 + 1)
            Code.SaveXml(cbKeyWord.Items.ToArray, SalesGoodsListFilterPatch)
        End If


    End Sub



    Private Sub dgItemList_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgItemList.CellDoubleClick
        If e.RowIndex = -1 Then Exit Sub
        SelectedRow = dgItemList.Rows(e.RowIndex)
        Me.DialogResult = Windows.Forms.DialogResult.OK
        'Dim row As DataGridViewRow = dgItemList.Rows(e.RowIndex)
        'result.SalesDate = row.Cells("銷貨日期").Value
        'result.SalesLabel = row.Cells("銷貨編號").Value
        'result.SellingPrice = row.Cells("賣價").Value
        'result.Number = row.Cells("數量").Value
        'result.StockLabel = row.Cells("庫存編號").Value

        'Dim stock As New Database.Stock
        'stock.Cost = row.Cells("成本").Value

        'Dim returnGoods As Database.ReturnGoods
        'returnGoods.Number = row.Cells("數量").Value
        'returnGoods.ReturnDate = Now
        'returnGoods.ReturnPrice = row.Cells("賣價").Value
        'returnGoods.SalesLabel = row.Cells("銷貨編號").Value
        'returnGoods.StockLabel = row.Cells("庫存編號").Value

    End Sub


    Private Sub 欄位顯示CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 欄位顯示CToolStripMenuItem.Click
        If DataGridViewVisibleDialog.ShowDialog(dgItemList) Then Code.SaveXml(DataGridViewVisibleDialog.GetVisibleColumns(dgItemList), SalesGoodsListVisiblePath)
    End Sub

    Private Sub 開啟銷貨SToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 開啟銷貨SToolStripMenuItem.Click
        OpenSales()
    End Sub

    Private Sub OpenSales()
        If dgItemList.SelectedRows.Count <= 0 Then
            MsgBox("您至少必須選擇一個項目!")
            Exit Sub
        End If


        Dim row As DataGridViewRow = dgItemList.SelectedRows(0)

        Dim SalesLabel As String = row.Cells("銷貨編號").Value

        winSales.Open(SalesLabel, access)
        'Dim win As New winSales
        'win.Open(SalesLabel, access)
    End Sub
End Class