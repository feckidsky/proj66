Imports 進銷存.Database

Public Class DialogStockMove

    Dim SourceShop As Access
    Dim DestineShop As Access
    Dim stock As Stock
    Dim Goods As Goods

    Enum Action
        Out = 0
        [In] = 1
    End Enum
    Dim mode As Action

    Private Sub DialogStockMove_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btOK.Text = IIf(mode = Action.Out, "調出", "申請")
        txtSource.Text = SourceShop.Name
        txtNumber.Text = 1
        Goods = SourceShop.GetGoods(stock.GoodsLabel)
        txtName.Text = Goods.Name

        cbDestine.Items.Clear()
        cbDestine.Items.AddRange(ClientManager.GetNameList())
        If DestineShop IsNot Nothing Then
            cbDestine.Text = DestineShop.Name
        Else
            btOK.Enabled = False
        End If

    End Sub

    Public Sub DialogStockOut(ByVal sourceShop As Access, ByVal StockLabel As String)
        Me.SourceShop = sourceShop
        Me.stock = Stock
        mode = Action.Out
        stock = sourceShop.GetStock(StockLabel)
        MyBase.ShowDialog()
    End Sub

    Public Sub DialogStockIn(ByVal sourceShop As Access, ByVal StockLabel As String)
        Me.SourceShop = sourceShop
        Me.DestineShop = CurrentAccess
        Me.stock = Stock
        mode = Action.In
        stock = sourceShop.GetStock(StockLabel)
        MyBase.ShowDialog()
    End Sub

    Private Sub cbDestine_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbDestine.SelectedIndexChanged
        DestineShop = ClientManager(cbDestine.Text)
        btOK.Enabled = DestineShop IsNot Nothing
    End Sub

    Private Sub btCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btCancel.Click
        DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub btOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btOK.Click

        Dim item As StockMove
        item.IMEI = stock.IMEI
        Try
            item.Number = Val(txtNumber.Text)
        Catch ex As Exception
            MsgBox(ex.Message)
            Exit Sub
        End Try
        If item.Number > stock.Number Then
            MsgBox("調貨數量不可大於庫存數量")
            Exit Sub
        End If
        item.Label = "SM" & Now.ToString("yyyyMMddHHmmss")
        item.SourceShop = SourceShop.Name
        item.DestineShop = DestineShop.Name
        item.Cost = stock.Cost
        item.Date = Now
        item.StockLabel = stock.Label
        item.SupplierLabel = stock.SupplierLabel
        item.GoodsLabel = stock.GoodsLabel

        If mode = Action.Out Then
            item.SourcePersonnel = CurrentUser.Label
            item.DestinePersonnel = ""
        Else
            item.DestinePersonnel = CurrentUser.Label
            item.SourcePersonnel = ""
        End If

        If SourceShop Is DestineShop Then
            MsgBox("來源與目地相同，動作取消!")
            Exit Sub
        ElseIf Not SourceShop.Connected Then
            MsgBox(SourceShop.Name & "未連線!")
            Exit Sub
        ElseIf Not DestineShop.Connected Then
            MsgBox(DestineShop.Name & "未連線!")
            Exit Sub
        End If

        If mode = Action.Out Then
            item.Action = StockMove.Type.Sending
            SourceShop.AddStockMove(item)
            item.Action = StockMove.Type.Receiving
            DestineShop.AddStockMove(item)
            stock.Number -= item.Number
            'SourceShop.ChangeStock(stock)
            SourceShop.StockMoveOut(stock, item.Number)
        Else
            item.Action = StockMove.Type.Request
            SourceShop.AddStockMove(item)
            item.Action = StockMove.Type.Request
            DestineShop.AddStockMove(item)
        End If

        DialogResult = Windows.Forms.DialogResult.OK
    End Sub
End Class