Public Class winSupplierList
    Dim Filter As DataGridViewFilter

    Enum Mode
        Normal = 0
        SelectItem = 1
    End Enum

    Dim work As Mode

    WithEvents access As Database.Access '= Program.DB

    Private Sub winSupplierList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Filter = New DataGridViewFilter(dgList)
        Filter.AddTextFilter("編號", "名稱", "電話1", "電話2", "地址", "備註")
        BeginUpdateList()
    End Sub

    Public Overloads Sub Show(ByVal DB As Database.Access)
        access = DB
        ShowDialog()
    End Sub

    Public Overloads Sub ShowDialog()
        If Not CheckAuthority(1) Then Exit Sub
        work = Mode.Normal
        MyBase.ShowDialog()
    End Sub


    Public Function SelectDialog(ByVal db As Database.Access) As Database.Supplier
        access = db
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        work = Mode.SelectItem
        If MyBase.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Return GetSelectedSupplier()
        Else
            Return Database.Supplier.Null()
        End If
    End Function

    Dim dt As DataTable
    Private Sub BeginUpdateList()
        Dim dialog As New ProgressDialog
        dialog.Thread = New Threading.Thread(New Threading.ParameterizedThreadStart(AddressOf UpdateList))
        dialog.Start("讀取供應商資料")
    End Sub

    Private Sub UpdateList(ByVal progress As Database.Access.Progress)
        dt = access.GetSupplierList(progress)
        Me.Invoke(New Action(Of DataTable)(AddressOf UpdateDataTable), dt)
        progress.Finish()
    End Sub

    Private Sub UpdateDataTable(ByVal dt As DataTable)
        dgList.DataSource = dt

        UpdateTitle("Label", "編號")
        UpdateTitle("Name", "名稱")
        UpdateTitle("Tel1", "電話1")
        UpdateTitle("Tel2", "電話2")
        UpdateTitle("Addr", "地址")
        UpdateTitle("Note", "備註")
        Try
            Filter.UpdateComboBox()
            dgList.Sort(dgList.Columns(0), System.ComponentModel.ListSortDirection.Descending)
        Catch

        End Try
    End Sub

    Private Sub UpdateTitle(ByVal Label As String, ByVal Text As String)
        dgList.Columns(Label).HeaderText = Text
    End Sub

    Private Sub 新增AToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 新增AToolStripMenuItem.Click, 新增CToolStripMenuItem1.Click
        winSupplier.Create(GetNewSupplier, access)
    End Sub

    Private Sub 修改CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 修改CToolStripMenuItem.Click
        EditGoods()
    End Sub

    Private Sub EditGoods()
        Dim selectedItem As Database.Supplier = GetSelectedSupplier()
        If selectedItem.IsNull() Then
            MsgBox("您必須選擇一個項目")
            Exit Sub
        End If

        winSupplier.Open(selectedItem, access)
    End Sub

    Public Function GetSelectedSupplier() As Database.Supplier
        If Not Filter.HasSelectedItem Then Return Database.Supplier.Null()
        Dim dt As DataTable = dgList.DataSource

        Dim label As String = dgList.SelectedRows(0).Cells(0).Value

        For Each r As DataRow In dt.Rows
            If r(0) = label Then
                Return Database.Supplier.GetFrom(r)

            End If

        Next
        Return Database.Supplier.Null() 'Database.Supplier.GetFrom(dt.Rows(dgList.SelectedRows(0).Index))
    End Function


    Private Sub 刪除DToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 刪除DToolStripMenuItem.Click
        If Not CheckAuthority(2) Then Exit Sub

        Dim SelectedSupplier As Database.Supplier = GetSelectedSupplier()
        If SelectedSupplier.IsNull() Then
            MsgBox("您必須選擇一個項目")
            Exit Sub
        End If


        Dim count As Integer = access.GetStockLogBySupplierLabel(SelectedSupplier.Label).Rows.Count
        For Each c As Database.Access In Client.Client
            If c.Connected Then count += c.GetStockLogBySupplierLabel(SelectedSupplier.Label).Rows.Count
        Next

        If count > 0 Then
            MsgBox("此供應商已經有進貨資料，無法刪除!")
            Exit Sub
        End If

        If MsgBox("這麼做將會刪除該此供應，您確定要這麼做？", MsgBoxStyle.OkCancel + MsgBoxStyle.Question) = MsgBoxResult.Cancel Then
            Exit Sub
        End If

        access.DeleteSupplier(SelectedSupplier)
    End Sub

    Private Sub dgGoodsList_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgList.CellMouseDoubleClick
        If work = Mode.Normal Then
            EditGoods()
        Else
            Me.DialogResult = Windows.Forms.DialogResult.OK
        End If


    End Sub


    Delegate Sub DelegateItem(sender As Object ,ByVal sup As Database.Supplier)
    Dim invCreate As New DelegateItem(AddressOf access_CreatedSupplier)
    Dim invDelete As New DelegateItem(AddressOf access_DeletedSupplier)
    Dim invChange As New DelegateItem(AddressOf access_ChangedSupplier)

    Private Sub access_CreatedSupplier(ByVal sender As Object, ByVal sup As Database.Supplier) Handles access.CreatedSupplier
        If Me.InvokeRequired Then
            Me.Invoke(invCreate, sender, sup)
            Exit Sub
        End If
        dt.Rows.Add(sup.Label, sup.Name, sup.Tel1, sup.Tel2, sup.Note, sup.Addr, sup.Modify)
    End Sub

    Private Sub access_ChangedSupplier(ByVal sender As Object, ByVal sup As Database.Supplier) Handles access.ChangedSupplier
        If Me.InvokeRequired Then
            Me.Invoke(invChange, sender, sup)
            Exit Sub
        End If

        For Each r As DataRow In dt.Rows
            If Strings.RTrim(r("Label")) = sup.Label Then sup.UpdateRow(r) 'r.ItemArray = sup.ToObjects()
        Next
    End Sub


    Private Sub access_DeletedSupplier(ByVal sender As Object, ByVal sup As Database.Supplier) Handles access.DeletedSupplier
        If Me.InvokeRequired Then
            Me.Invoke(invDelete, sender, sup)
            Exit Sub
        End If

        Dim delRow As New List(Of DataRow)
        For Each r As DataRow In dt.Rows
            If Strings.RTrim(r("Label")) = sup.Label Then delRow.Add(r)
        Next

        For Each r As DataRow In delRow
            dt.Rows.Remove(r)
        Next

    End Sub


    Private Sub dgList_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles dgList.RowsAdded
        Dim row As DataGridViewRow = CType(sender, DataGridView).Rows(e.RowIndex)
        Filter.FilterRow(row)
        Filter.AddComboBoxItem(row)
    End Sub
End Class