﻿
Public Class winGoodsList
    Dim Filter As DataGridViewFilter
    Enum Mode
        Normal = 0
        SelectItem = 1
    End Enum

    Dim work As Mode

    WithEvents access As Database.Access '= Program.DB
    Public Sub New()

        ' 此為 Windows Form 設計工具所需的呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。
        Filter = New DataGridViewFilter(dgGoodsList)
        Filter.AddTextFilter("編號", "品名", "種類", "廠牌", "備註")
    End Sub

    Private Sub winGoodsList_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        access = Nothing
    End Sub
    Private Sub winGoodsList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
      
        BeginUpdateGoodsList()
    End Sub

    Public Overloads Sub Show(ByVal DB As Database.Access)
        access = DB
        If Not CheckAuthority(1) Then Exit Sub
        work = Mode.Normal
        MyBase.Show()
        MyBase.BringToFront()
    End Sub


    Public Function SelectDialog(ByVal DB As Database.Access) As Database.Goods
        access = DB

        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        work = Mode.SelectItem
        If Me.Visible Then Me.Visible = False
        If MyBase.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Return GetSelectedGoods()
        Else
            Return Database.Goods.Null()
        End If

    End Function

    Private GoodsLoading As Boolean = False
    Private Sub BeginUpdateGoodsList()
        Dim dialog As New ProgressDialog
        dialog.Thread = New Threading.Thread(New Threading.ParameterizedThreadStart(AddressOf UpdateGoodsList))
        dialog.Start("讀取商品項目")
    End Sub

    Public Sub UpdateGoodsList(ByVal progress As Database.Access.Progress)



        Dim dt As DataTable = access.GetGoodsList(progress)
        progress.WaitFinish()

        If Me.InvokeRequired Then
            Try
                If Not Me.IsDisposed Then Me.Invoke(New Action(Of DataTable)(AddressOf UpdateGoodsDataTable), dt)
            Catch
            End Try

        Else
            UpdateGoodsDataTable(dt)
        End If

        progress.Finish()
    End Sub

    Public Sub UpdateGoodsDataTable(ByVal dt As DataTable)
        GoodsLoading = True
        dgGoodsList.DataSource = dt
        GoodsLoading = False
        UpdateTitle("Label", "編號")
        UpdateTitle("Name", "品名")
        UpdateTitle("Kind", "種類")
        UpdateTitle("Brand", "廠牌")
        UpdateTitle("Note", "備註")
        'dgGoodsList.Sort(dgGoodsList.Columns(0), System.ComponentModel.ListSortDirection.Descending)

        Try
            Filter.UpdateComboBox()
            dgGoodsList.Sort(dgGoodsList.Columns(0), System.ComponentModel.ListSortDirection.Descending)
        Catch

        End Try
        If dgGoodsList.Rows.Count > 0 Then
            dgGoodsList.Rows(0).Selected = True
        Else
            UpdateHistory()
        End If

    End Sub



    Private Sub UpdateTitle(ByVal Label As String, ByVal Text As String)
        dgGoodsList.Columns(Label).HeaderText = Text
    End Sub

    Private Sub 新增AToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 新增AToolStripMenuItem.Click, 新增CToolStripMenuItem1.Click
        winGoods.Create(GetNewGoods(), access)
    End Sub

    Private Sub 修改CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 修改CToolStripMenuItem.Click
        EditGoods()
    End Sub

    Private Sub EditGoods()
        Dim selected As Database.Goods = GetSelectedGoods()
        If selected.IsNull() Then
            MsgBox("您必須選擇一個項目")
            Exit Sub
        End If

        winGoods.Open(selected, access)
    End Sub

    Public Function GetSelectedGoods() As Database.Goods
        If Not Filter.HasSelectedItem Then
            Return Database.Goods.Null()
        End If

        Dim dt As DataTable = dgGoodsList.DataSource

        Dim label As String = dgGoodsList.SelectedRows(0).Cells(0).Value

        For Each r As Data.DataRow In dt.Rows
            If r.Item(0) = label Then
                Return Database.Goods.GetFrom(r)
            End If
        Next
        Return Database.Goods.Null()
        'Return Database.Goods.GetFrom(dt.Rows(dgGoodsList.SelectedRows(0).Index))
    End Function


    Private Sub 刪除DToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 刪除DToolStripMenuItem.Click
        If Not CheckAuthority(2) Then Exit Sub

        Dim selected As Database.Goods = GetSelectedGoods()
        If selected.IsNull() Then
            MsgBox("您必須選擇一個項目")
            Exit Sub
        End If

        'Dim SelectedGoods As Database.Goods = selected
        Dim count As Integer = access.GetStockLogByGoodsLabel(selected.Label).Rows.Count
        For Each c As Database.Access In ClientManager.Client
            If c.Connected Then count += c.GetStockLogByGoodsLabel(selected.Label).Rows.Count
        Next
        If count > 0 Then
            MsgBox("商品項目已經有進貨資料，無法刪除!")
            Exit Sub
        End If

        If MsgBox("這麼做將會刪除該商品項目，您確定要這麼做？", MsgBoxStyle.OkCancel + MsgBoxStyle.Question) = MsgBoxResult.Cancel Then
            Exit Sub
        End If



        access.DeleteGoods(selected)
        access.DeleteHistoryPriceList(selected.Label)
    End Sub

    Private Sub dgGoodsList_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgGoodsList.CellMouseDoubleClick
        If work = Mode.Normal Then
            EditGoods()
        Else
            Me.DialogResult = Windows.Forms.DialogResult.OK
        End If


    End Sub


    Dim invHistoryPrice As New ItemUpdate(AddressOf UpdateHistory)
    Private Sub access_ChangedHistoryPrice(ByVal sender As Object, ByVal hp As Database.HistoryPrice, ByVal source As Database.Access.Source) Handles access.ChangedHistoryPrice, access.CreatedHistoryPrice, access.DeletedHistoryPrice
        If Me.InvokeRequired Then
            Try
                If Not Me.IsDisposed Then Me.Invoke(invHistoryPrice)
            Catch
            End Try
        Else
            invHistoryPrice()
        End If
        'UpdateHistory()
    End Sub


    Delegate Sub ItemUpdate()
    Dim invGoods As New ItemUpdate(AddressOf BeginUpdateGoodsList)
    Private Sub access_ChangedGoods(ByVal sender As Object, ByVal goods As Database.Goods, ByVal source As Database.Access.Source) Handles access.ChangedGoods, access.CreatedGoods, access.DeletedGoods
        If Me.InvokeRequired Then
            Try
                If Not Me.IsDisposed Then Me.Invoke(invGoods)
            Catch
            End Try
        Else
            invGoods()
        End If
        'UpdateGoodsList()
    End Sub

    Private Sub dgGoodsList_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles dgGoodsList.RowsAdded
        Dim row As DataGridViewRow = CType(sender, DataGridView).Rows(e.RowIndex)
        Filter.FilterRow(row)
        Filter.AddComboBoxItem(row)
    End Sub


    Private Sub dgItemList_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgGoodsList.SelectionChanged
        If Me.IsHandleCreated Then UpdateHistory()
    End Sub

    Private Sub UpdateHistory()
        If GoodsLoading Then Exit Sub
        Dim goods As Database.Goods = GetSelectedGoods()
        If goods.IsNull Then Exit Sub
        gbHistory.Text = "歷史售價 - " & goods.Name & " - 讀取中..."
        Dim dt As Data.DataTable = access.GetHistoryPriceList(goods.Label)
        dgHistory.DataSource = dt
        If dt Is Nothing Then Exit Sub
        gbHistory.Text = "歷史售價 - " & goods.Name
        Try
            dgHistory.Columns(0).Visible = False
        Catch
        End Try
        Try
            dgHistory.Columns("Time").HeaderText = "時間"
            dgHistory.Columns("Cost").HeaderText = "進貨價"
            dgHistory.Columns("Price").HeaderText = "建議售價"
        Catch
        End Try
    End Sub


    Private Sub 新增歷史售價HToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 新增歷史售價HToolStripMenuItem.Click, tsAddHistoryPrice.Click
        AddHistoryPrice()
    End Sub


    Private Sub AddHistoryPrice()
        If Not CheckAuthority(2) Then Exit Sub
        Dim goods As Database.Goods = GetSelectedGoods()
        winHistoryPrice.Create(goods.Label, goods.Name, access)
    End Sub

    Private Function GetSelectedHistoryPrice() As Database.HistoryPrice
        If dgHistory.SelectedRows.Count = 0 Then
            Return Database.HistoryPrice.Null()
        End If

        Dim data As Database.HistoryPrice

        Dim selectedRow = dgHistory.SelectedRows(0)
        data.GoodsLabel = selectedRow.Cells("GoodsLabel").Value
        data.Time = selectedRow.Cells("Time").Value
        data.Price = selectedRow.Cells("Price").Value
        data.Cost = selectedRow.Cells("Cost").Value
        Return data

    End Function

    Private Sub tsEditHistoryPrice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsEditHistoryPrice.Click
        EditHIstoryPrice()
    End Sub

    Private Sub EditHistoryPrice()
        If Not CheckAuthority(2) Then Exit Sub
        Dim selectedHisPrice As Database.HistoryPrice = GetSelectedHistoryPrice()
        If selectedHisPrice.IsNull Then
            MsgBox("您至少必須選擇一個項目", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        winHistoryPrice.Open(selectedHisPrice, GetSelectedGoods().Name, access)
    End Sub


    Private Sub tsDeleteHistoryPrice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsDeleteHistoryPrice.Click
        If Not CheckAuthority(2) Then Exit Sub
        Dim selectedHisPrice As Database.HistoryPrice = GetSelectedHistoryPrice()
        If selectedHisPrice.IsNull Then
            MsgBox("您至少必須選擇一個項目", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If dgHistory.Rows.Count = 1 Then
            MsgBox("無法刪除，商品最少必須有一筆歷史售價!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If MsgBox("這麼做將會刪除該此歷史售價，您確定要這麼做？", MsgBoxStyle.OkCancel + MsgBoxStyle.Question) = MsgBoxResult.Cancel Then
            Exit Sub
        End If

        access.DeleteHistoryPrice(GetSelectedHistoryPrice())
    End Sub


    Private Sub dgHistory_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgHistory.MouseDoubleClick
        EditHistoryPrice()
    End Sub

   
End Class