Public Class winCustomerList

    Enum Mode
        Normal = 0
        SelectItem = 1
    End Enum

    Dim work As Mode

    WithEvents access As Database.Access = Program.DB
    Dim Filter As DataGridViewFilter
    Private Sub winSupplierList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Filter = New DataGridViewFilter(dgList)
        Filter.AddTextFilter("編號", "名稱", "電話1", "電話2", "地址", "備註")
        UpdateList()
    End Sub

    Public Overloads Sub Show()
        ShowDialog()
    End Sub

    Public Overloads Sub ShowDialog()
        If Not CheckAuthority(1) Then Exit Sub
        work = Mode.Normal
        MyBase.ShowDialog()
    End Sub


    Public Function SelectDialog() As Database.Customer
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        work = Mode.SelectItem
        If MyBase.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Return GetSelectedCustomer()
        Else
            Return Database.Customer.Null()
        End If
    End Function


    Private Sub UpdateList()
        dgList.DataSource = DB.GetCustomerList()

        UpdateTitle("Label", "編號")
        UpdateTitle("Name", "名稱")
        UpdateTitle("Tel1", "電話1")
        UpdateTitle("Tel2", "電話2")
        UpdateTitle("Addr", "地址")
        UpdateTitle("Note", "備註")
        Filter.UpdateComboBox()
    End Sub

    Private Sub UpdateTitle(ByVal Label As String, ByVal Text As String)
        dgList.Columns(Label).HeaderText = Text
    End Sub

    Private Sub 新增AToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 新增AToolStripMenuItem.Click, 新增CToolStripMenuItem1.Click
        winCustomer.Create(GetNewCustomer())
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

        winCustomer.Open(item)
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


        Dim count As Integer = DB.GetSalesListByCustomer(item.Label).Rows.Count

        For Each c As Database.AccessClient In Client.Client
            If c.Connected Then count += c.GetSalesListByCustomer(item.Label).Rows.Count
        Next
        If count > 0 Then
            MsgBox("此客戶已有銷售記錄，無法刪除!")
            Exit Sub
        End If

        If MsgBox("這麼做將會刪除該客戶資訊，您確定要這麼做？", MsgBoxStyle.OkCancel + MsgBoxStyle.Question) = MsgBoxResult.Cancel Then
            Exit Sub
        End If

        DB.DeleteCustomer(item)
    End Sub

    Private Sub dgGoodsList_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgList.CellMouseDoubleClick
        If work = Mode.Normal Then
            EditGoods()
        Else
            Me.DialogResult = Windows.Forms.DialogResult.OK
        End If


    End Sub

    Private Sub access_CreatedCustomer(ByVal sender As Object, ByVal cus As Database.StructureBase.Customer) Handles access.CreatedCustomer, access.ChangedCustomer, access.DeletedCustomer
        UpdateList()
    End Sub



End Class