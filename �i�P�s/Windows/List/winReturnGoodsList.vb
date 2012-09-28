Public Class winReturnGoodsList
    WithEvents access As Database.Access
    Dim Filter As DataGridViewFilter

    Dim SelectedRow As DataGridViewRow = Nothing


    Public Overloads Function ShowDialog(ByVal DB As Database.Access) As DataGridViewRow
        If Me.Visible Then Me.Visible = False
        access = DB
        SelectedRow = Nothing
        ReadReturnGoodsList()

        MyBase.ShowDialog()
        Return SelectedRow
    End Function



    Public Overloads Sub Show(ByVal DB As Database.Access)
        access = DB
        MyBase.Show()
        MyBase.BringToFront()
        ReadReturnGoodsList()
    End Sub
    Dim SalesGoodsListVisiblePath As String = My.Application.Info.DirectoryPath & "\ReturnGoodsListVisible.xml"
    Dim SalesGoodsListFilterPatch As String = My.Application.Info.DirectoryPath & "\ReturnGoodsListFilterHis.xml"
    Private Sub winSalesGoodsList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim lst As String() = Code.LoadXml(Of String())(SalesGoodsListFilterPatch, New String() {})
        cbKeyWord.Items.Clear()
        cbKeyWord.Items.AddRange(lst)
    End Sub

    Private Sub ReadReturnGoodsList()
        If dtpStart.Value.Date > dtpEnd.Value.Date Then
            Dim tmp As Date = dtpStart.Value
            dtpStart.Value = dtpEnd.Value
            dtpEnd.Value = tmp
        End If

        Dim cnd As String = cbKeyWord.Text
        Dim st As Date = IIf(ckTime.Checked, dtpStart.Value.Date, Nothing)
        Dim ed As Date = IIf(ckTime.Checked, dtpEnd.Value.Date.AddDays(1).AddSeconds(-1), Nothing)
        'Dim table As DataTable = access.GetSalesGoodsLis(st, ed, cnd, cnd, cnd, cnd, cnd, cnd, cnd, cnd, 101)
        Dim table As DataTable = access.GetReturnGoodsLis(st, ed, cnd, 101)
        If table.Rows.Count > 100 Then
            lbResult.Text = "查詢筆數超出100筆，目前僅顯示100筆，若找不到資料請縮小篩選範圍"
            Dim lst As Integer = table.Rows.Count - 1
            For i As Integer = lst To 100 Step -1
                table.Rows.RemoveAt(i)
            Next
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
            table.Columns("SalesGoods.Number").ColumnName = "銷售數量"
            table.Columns("Note").ColumnName = "備註"
            table.Columns("ReturnLabel").ColumnName = "退貨單號"
            table.Columns("ReturnPrice").ColumnName = "退價"
            table.Columns("ReturnGoods.Number").ColumnName = "退貨數量"
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
        Filter.AddTextFilter("銷貨編號", "庫存編號", "商品編號", "銷貨人員", "客戶", "種類", "廠牌", "品名", "備註", "退貨單號")
        Filter.AddNumberFilter("成本", "賣價", "銷售數量", "退價", "退貨數量")
    End Sub


    Private Sub btRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btRead.Click
        ReadReturnGoodsList()
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
    End Sub


    Private Sub 欄位顯示CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 欄位顯示CToolStripMenuItem.Click
        If DataGridViewVisibleDialog.ShowDialog(dgItemList) Then Code.SaveXml(DataGridViewVisibleDialog.GetVisibleColumns(dgItemList), SalesGoodsListVisiblePath)
    End Sub

    Private Sub 開啟銷貨SToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 開啟銷貨SToolStripMenuItem.Click
        OpenSales()
    End Sub

    Private Sub 開啟退貨單RToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 開啟退貨單RToolStripMenuItem.Click
        OpenReturnSales()
    End Sub

    Private Sub OpenSales()
        If dgItemList.SelectedRows.Count <= 0 Then
            MsgBox("您至少必須選擇一個項目!")
            Exit Sub
        End If


        Dim row As DataGridViewRow = dgItemList.SelectedRows(0)

        Dim SalesLabel As String = row.Cells("銷貨編號").Value

        winSales.Open(SalesLabel, access)
    End Sub

    Private Sub OpenReturnSales()
        If dgItemList.SelectedRows.Count <= 0 Then
            MsgBox("您至少必須選擇一個項目!")
            Exit Sub
        End If


        Dim row As DataGridViewRow = dgItemList.SelectedRows(0)

        Dim SalesLabel As String = row.Cells("退貨單號").Value

        winSales.Open(SalesLabel, access, winSales.Form.Return)
    End Sub


End Class