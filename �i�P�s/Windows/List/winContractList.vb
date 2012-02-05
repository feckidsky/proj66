﻿Public Class winContractList

    Enum Mode
        Normal = 0
        SelectItem = 1
    End Enum

    Dim work As Mode

    WithEvents access As Database.Access '= Program.DB
    Dim Filter As DataGridViewFilter

    Dim FilterEffect As Boolean = False

    Private Sub winContractList_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Filter = New DataGridViewFilter(dgGoodsList)
        Filter.AddTextFilter("編號", "合約", "備註")
        Filter.AddNumberFilter("預付額", "佣金", "折扣")
        Filter.AddBoolFilter("有效")
        If FilterEffect Then Filter.SetBoolFilter("有效", True)
        UpdateList()
    End Sub


    Public Overloads Sub Show(ByVal DB As Database.Access)
        access = DB
        If Not CheckAuthority(2) Then Exit Sub
        work = Mode.Normal
        MyBase.Show()
    End Sub


    Public Function SelectDialog(ByVal DB As Database.Access) As Database.Contract
        access = DB
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        work = Mode.SelectItem
        If MyBase.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Return GetSelectedItem()
        Else
            Return Database.Contract.Null()
        End If
    End Function

    Public Function SelectEffectDialog(ByVal DB As Database.Access) As Database.Contract
        FilterEffect = True
        Return SelectDialog(DB)
    End Function

    Dim dt As DataTable
    Private Sub UpdateList()
        dt = access.GetContractList()
        dgGoodsList.DataSource = dt

        UpdateTitle("Label", "編號")
        UpdateTitle("Enable", "有效")
        UpdateTitle("Name", "合約")
        UpdateTitle("Commission", "佣金")
        UpdateTitle("Discount", "折扣")
        UpdateTitle("Prepay", "預付額")
        UpdateTitle("Note", "備註")
        If Filter IsNot Nothing Then Filter.UpdateComboBox()
    End Sub

    Private Sub UpdateTitle(ByVal Label As String, ByVal Text As String)
        dgGoodsList.Columns(Label).HeaderText = Text
    End Sub

    Private Sub 新增AToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 新增AToolStripMenuItem.Click, 新增CToolStripMenuItem1.Click
        winContract.Create(GetNewContract(), access)
    End Sub

    Private Sub 修改CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 修改CToolStripMenuItem.Click
        EditItem()
    End Sub

    Private Sub EditItem()

        If GetSelectedItem().IsNull Then
            MsgBox("您必須選擇一個項目")
            Exit Sub
        End If


        winContract.Open(GetSelectedItem(), access)
    End Sub

    Public Function GetSelectedItem() As Database.Contract
        If Not Filter.HasSelectedItem Then Return Database.Contract.Null()
        Dim dt As DataTable = dgGoodsList.DataSource

        Dim label As String = dgGoodsList.SelectedRows(0).Cells(0).Value

        For Each r As Data.DataRow In dt.Rows
            If r.Item(0) = label Then
                Return Database.Contract.GetFrom(r)
            End If
        Next
        Return Database.Contract.Null()
        'Return Database.Goods.GetFrom(dt.Rows(dgGoodsList.SelectedRows(0).Index))
    End Function


    Private Sub 刪除DToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 刪除DToolStripMenuItem.Click
        If Not CheckAuthority(2) Then Exit Sub

        Dim selectedItem As Database.Contract = GetSelectedItem()
        If selecteditem.IsNull() Then
            MsgBox("您必須選擇一個項目")
            Exit Sub
        End If


        Dim count As Integer = access.GetSalesListByContractLabel(selectedItem.Label).Rows.Count
        For Each c As Database.AccessClient In Client.Client
            If c.Connected Then count += c.GetSalesListByContractLabel(selectedItem.Label).Rows.Count
        Next

        If count > 0 Then
            MsgBox("商品項目已經有訂單資料，無法刪除!")
            Exit Sub
        End If

        If MsgBox("這麼做將會刪除該項合約項目，您確定要這麼做？", MsgBoxStyle.OkCancel + MsgBoxStyle.Question) = MsgBoxResult.Cancel Then
            Exit Sub
        End If



        access.DeleteContract(selectedItem)
    End Sub

    Private Sub dgGoodsList_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgGoodsList.CellMouseDoubleClick
        If work = Mode.Normal Then
            EditItem()
        Else
            Me.DialogResult = Windows.Forms.DialogResult.OK
        End If


    End Sub
    'Delegate Sub ItemUpdate()
    'Dim inv As New ItemUpdate(AddressOf UpdateList)
    'Private Sub access_ChangedItem(ByVal sender As Object, ByVal goods As Database.StructureBase.Contract) Handles access.ChangedContract, access.CreatedContract, access.DeletedContract
    '    Me.Invoke(inv)
    'End Sub

    Delegate Sub DelegateItem(ByVal sender As Object, ByVal sup As Database.Contract)
    Dim invCreate As New DelegateItem(AddressOf access_CreatedItem)
    Dim invDelete As New DelegateItem(AddressOf access_DeletedItem)
    Dim invChange As New DelegateItem(AddressOf access_ChangedItem)

    Private Sub access_CreatedItem(ByVal sender As Object, ByVal item As Database.StructureBase.Contract) Handles access.CreatedContract
        If Me.InvokeRequired Then
            Me.Invoke(invCreate, sender, item)
            Exit Sub
        End If
        dt.Rows.Add(item.ToObjects())
    End Sub

    Private Sub access_ChangedItem(ByVal sender As Object, ByVal item As Database.StructureBase.Contract) Handles access.ChangedContract
        If Me.InvokeRequired Then
            Me.Invoke(invChange, sender, item)
            Exit Sub
        End If

        For Each r As DataRow In dt.Rows
            If Strings.RTrim(r("Label")) = item.Label Then item.UpdateRow(r) 'r.ItemArray = sup.ToObjects()
        Next
    End Sub


    Private Sub access_DeletedItem(ByVal sender As Object, ByVal item As Database.StructureBase.Contract) Handles access.DeletedContract
        If Me.InvokeRequired Then
            Me.Invoke(invDelete, sender, item)
            Exit Sub
        End If

        Dim delRow As New List(Of DataRow)
        For Each r As DataRow In dt.Rows
            If Strings.RTrim(r("Label")) = item.Label Then delRow.Add(r)
        Next

        For Each r As DataRow In delRow
            dt.Rows.Remove(r)
        Next

    End Sub

End Class
