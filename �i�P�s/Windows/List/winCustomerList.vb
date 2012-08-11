Public Class winCustomerList

    Enum Mode
        Normal = 0
        SelectItem = 1
    End Enum

    Dim work As Mode

    WithEvents access As Database.Access '= Program.DB
    Dim Filter As DataGridViewFilter

    Private Sub winCustomerList_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Filter = New DataGridViewFilter(dgList)
        Filter.AddTextFilter("編號", "名稱", "電話1", "電話2", "地址", "備註")
    End Sub

    Private Sub winSupplierList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
     
        BeginUpdateList()
    End Sub

    Public Overloads Sub Show(ByVal db As Database.Access)
        ShowDialog(db)
    End Sub

    Public Overloads Sub ShowDialog(ByVal DB As Database.Access)
        Me.access = DB
        If Not CheckAuthority(1) Then Exit Sub
        work = Mode.Normal
        MyBase.ShowDialog()
    End Sub


    Public Function SelectDialog(ByVal db As Database.Access) As Database.Customer
        Me.access = db
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        work = Mode.SelectItem
        If MyBase.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Return GetSelectedCustomer()
        Else
            Return Database.Customer.Null()
        End If
    End Function

    Dim dt As DataTable



    Private Sub BeginUpdateList()
        Dim dialog As New ProgressDialog
        Dim progress As Database.Access.Progress = dialog.GetProgress("讀取客戶資料")
        dialog.Show()
        dialog.Thread = New Threading.Thread(New Threading.ParameterizedThreadStart(AddressOf UpdateList))
        dialog.Thread.Start(progress)
    End Sub

    Private Sub UpdateList(ByVal progress As Database.Access.Progress)
        dt = access.GetCustomerList(progress)
        Try
            If Not Me.IsDisposed Then Me.Invoke(New Action(Of DataTable)(AddressOf UpdateDataTable), dt)
        Catch
        End Try
        progress.Finish()
    End Sub

    Private Sub UpdateDataTable(ByVal newDT As DataTable)
        dgList.DataSource = newDT
        UpdateTitle("Label", "編號")
        UpdateTitle("Name", "名稱")
        UpdateTitle("Tel1", "電話1")
        UpdateTitle("Tel2", "電話2")
        UpdateTitle("Addr", "地址")
        UpdateTitle("Note", "備註")
        Try
            dgList.Sort(dgList.Columns(0), System.ComponentModel.ListSortDirection.Descending)
            Filter.UpdateComboBox()
        Catch

        End Try
    End Sub

    Private Sub UpdateTitle(ByVal Label As String, ByVal Text As String)
        dgList.Columns(Label).HeaderText = Text
    End Sub

    Private Sub 新增AToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 新增AToolStripMenuItem.Click, 新增CToolStripMenuItem1.Click
        winCustomer.Create(GetNewCustomer(), access)
    End Sub

    Private Sub 修改CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 修改CToolStripMenuItem.Click
        EditGoods()
    End Sub

    Private Sub EditGoods()
        Dim item As Database.Customer = GetSelectedCustomer()
        If item.IsNull() Then
            MsgBox("您必須選擇一個項目")
            Exit Sub
        End If

        winCustomer.Open(item, access)
    End Sub

    Public Function GetSelectedCustomer() As Database.Customer
        If Not Filter.HasSelectedItem Then Return Database.Customer.Null()
        Dim dt As DataTable = dgList.DataSource

        Dim label As String = dgList.SelectedRows(0).Cells(0).Value

        For Each r As DataRow In dt.Rows
            If r(0) = label Then
                Return Database.Customer.GetFrom(r)
            End If

        Next
        Return Database.Customer.Null()
    End Function


    Private Sub 刪除DToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 刪除DToolStripMenuItem.Click
        If Not CheckAuthority(2) Then Exit Sub

        Dim item As Database.Customer = GetSelectedCustomer()
        If item.IsNull() Then
            MsgBox("您必須選擇一個項目")
            Exit Sub
        End If


        Dim count As Integer = access.GetSalesListByCustomer(item.Label).Rows.Count

        For Each c As Database.Access In ClientManager.Client
            If c.Connected Then count += c.GetSalesListByCustomer(item.Label).Rows.Count
        Next
        If count > 0 Then
            MsgBox("此客戶已有銷售記錄，無法刪除!")
            Exit Sub
        End If

        If MsgBox("這麼做將會刪除該客戶資訊，您確定要這麼做？", MsgBoxStyle.OkCancel + MsgBoxStyle.Question) = MsgBoxResult.Cancel Then
            Exit Sub
        End If

        access.DeleteCustomer(item)
    End Sub

    Private Sub dgGoodsList_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgList.CellMouseDoubleClick
        If work = Mode.Normal Then
            EditGoods()
        Else
            Me.DialogResult = Windows.Forms.DialogResult.OK
        End If


    End Sub
    'Delegate Sub ItemUpdate()
    'Dim inv As New ItemUpdate(AddressOf UpdateList)
    'Private Sub access_CreatedCustomer(ByVal sender As Object, ByVal cus As Database.StructureBase.Customer) Handles access.CreatedCustomer, access.ChangedCustomer, access.DeletedCustomer
    '    Me.Invoke(inv)
    'End Sub


    Delegate Sub DelegateItem(ByVal sender As Object, ByVal sup As Database.Customer)
    Dim invCreate As New DelegateItem(AddressOf access_CreatedItem)
    Dim invDelete As New DelegateItem(AddressOf access_DeletedItem)
    Dim invChange As New DelegateItem(AddressOf access_ChangedItem)

    Private Sub access_CreatedItem(ByVal sender As Object, ByVal item As Database.Customer) Handles access.CreatedCustomer
        If Me.InvokeRequired Then
            Try
                If Not Me.IsDisposed Then Me.Invoke(invCreate, sender, item)
            Catch
            End Try
            Exit Sub
        End If
        With item
            dt.Rows.Add(.Label, .Name, .Tel1, .Tel2, .Note, .Addr, .Modify)
        End With
    End Sub

    Private Sub access_ChangedItem(ByVal sender As Object, ByVal item As Database.Customer) Handles access.ChangedCustomer
        If Me.InvokeRequired Then
            Try
                If Not Me.IsDisposed Then Me.Invoke(invChange, sender, item)
            Catch
            End Try
            Exit Sub
        End If

        For Each r As DataRow In dt.Rows
            If Strings.RTrim(r("Label")) = item.Label Then item.UpdateRow(r) 'r.ItemArray = sup.ToObjects()
        Next
    End Sub


    Private Sub access_DeletedItem(ByVal sender As Object, ByVal item As Database.Customer) Handles access.DeletedCustomer
        If Me.InvokeRequired Then
            Try
                If Not Me.IsDisposed Then Me.Invoke(invDelete, sender, item)
            Catch
            End Try
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

    Private Sub dgList_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles dgList.RowsAdded
        Dim row As DataGridViewRow = CType(sender, DataGridView).Rows(e.RowIndex)
        Filter.FilterRow(row)
        Filter.AddComboBoxItem(row)
    End Sub

  
End Class