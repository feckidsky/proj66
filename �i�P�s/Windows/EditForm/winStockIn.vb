Imports 進銷存.Database.DatabaseType
Imports 進銷存.Database

Public Class winStockIn

    Enum Mode
        Create = 0
        Open = 1
    End Enum
    Dim Work As Mode

    WithEvents access As Database.Access

    Private SelectedGoods As Database.Goods = Database.Goods.Null()
    Private SelectedSupplier As Database.Supplier = Database.Supplier.Null()


    Private Sub winStockIn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btOK.Text = IIf(Work = Mode.Create, "入庫", "修改")
        txtLabel.Enabled = Work = Mode.Create

        btSelectGoods.Text = SelectedGoods.ToString()
    End Sub


    Public Sub Create(ByVal DB As Database.Access)
        access = DB
        If Not CheckAuthority(2) Then Exit Sub
        Dim data As Stock = GetNewStock()
        Work = Mode.Create
        UpdateText(data)

        MyBase.ShowDialog()
    End Sub

    Public Function Create(ByVal DB As Database.Access, ByVal GoodsLabel As String) As DialogResult
        access = DB
        If Not CheckAuthority(2) Then Return Windows.Forms.DialogResult.Cancel
        Dim data As Stock = GetNewStock()
        data.GoodsLabel = GoodsLabel
        Work = Mode.Create
        UpdateText(data)

        Return MyBase.ShowDialog()
    End Function

    Public Sub Open(ByVal stock As Stock, ByVal DB As Database.Access)
        access = DB
        If Not CheckAuthority(2) Then Exit Sub
        Work = Mode.Open
        UpdateText(stock)

        MyBase.ShowDialog()
    End Sub

    Public Sub UpdateText(ByVal Data As Stock)
        txtLabel.Text = Data.Label
        'If Work = Mode.Open Then txtCost.Text = Data.Cost
        txtCost.Text = IIf(Work = Mode.Open, Data.Cost, "")
        txtIMEI.Text = Data.IMEI
        'txtPrice.Text = Data.Price
        txtDate.Text = Data.Date
        txtNote.Text = Data.Note
        txtNumber.Text = Data.Number
        SelectedGoods = access.GetGoods(Data.GoodsLabel)
        btSelectGoods.Text = SelectedGoods.ToString()
        SelectedSupplier = access.GetSupplier(Data.SupplierLabel)
        btSelectSupplier.Text = SelectedSupplier.ToString()
    End Sub

    Public Function GetData() As Stock
        Dim Data As Stock = Nothing
        Data.Label = txtLabel.Text
        Single.TryParse(txtCost.Text, Data.Cost)
        Data.IMEI = txtIMEI.Text
        'Data.Price = txtPrice.Text
        Data.Note = txtNote.Text
        Data.Date = txtDate.Text
        Data.Number = Val(txtNumber.Text)
        Data.GoodsLabel = SelectedGoods.Label
        Data.SupplierLabel = SelectedSupplier.Label

        Return Data
    End Function

    Private Sub btOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btOK.Click
        Dim newStock As Stock = GetData()

        If txtCost.Text = "" Then
            MsgBox("您未輸入進貨價!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If



        If newStock.GoodsLabel = "" Then
            MsgBox("尚未指定商品")
            Exit Sub
        End If

        If Work = Mode.Create Then
            access.AddStock(newStock)
        Else
            access.ChangeStock(newStock)
        End If

        ' Me.DialogResult = Windows.Forms.DialogResult.OK

    End Sub

    Private Sub btAddSupplier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSelectSupplier.Click
        Dim sel As Supplier = winSupplierList.SelectDialog(access)
        If Not sel.IsNull() Then
            SelectedSupplier = sel
            btSelectSupplier.Text = SelectedSupplier.ToString()
        End If
    End Sub

    Private Sub btAddGoods_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSelectGoods.Click
        Dim sel As Goods = winGoodsList.SelectDialog(access)
        If Not sel.IsNull() Then
            SelectedGoods = sel
            btSelectGoods.Text = SelectedGoods.ToString()

            Dim hp As HistoryPrice = access.GetListHistoryPrice(sel.Label)
            txtPrice.Text = hp.Price
            If txtCost.Text = "" Then txtCost.Text = hp.Cost
        End If
    End Sub



    Private Sub btResetGoods_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btResetGoods.Click
        SelectedGoods = Database.Goods.Null()
        btSelectGoods.Text = SelectedGoods.ToString()
    End Sub


    Private Sub btResetSupplier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btResetSupplier.Click
        SelectedSupplier = Database.Supplier.Null()
        btSelectSupplier.Text = SelectedSupplier.ToString()
    End Sub

    Private Sub btUpdateCostByHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btUpdateCostByHistory.Click
        txtCost.Text = access.GetListHistoryPrice(SelectedGoods.Label).Cost
    End Sub

    Public Sub New()

        ' 此為 Windows Form 設計工具所需的呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。
        Me.DefaultTextBoxImeMode()
    End Sub

    Private Sub access_CreatedStock(ByVal sender As Object, ByVal stock As Database.Stock) Handles access.CreatedStock, access.ChangedStock


        If Me.InvokeRequired Then
            Dim handler As New Action(Of Stock)(AddressOf ReturnDialogResult)
            Try
                If Not Me.IsDisposed Then Me.Invoke(handler, stock)
            Catch
            End Try
        Else
            ReturnDialogResult(stock)
        End If
    End Sub

    Private Sub ReturnDialogResult(ByVal stock As Stock)
        If stock.Label <> txtLabel.Text Then Exit Sub
        DialogResult = Windows.Forms.DialogResult.OK
    End Sub
End Class