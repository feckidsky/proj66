﻿Public Class winPersonnelList
    Dim Filter As DataGridViewFilter
    Enum Mode
        Normal = 0
        SelectItem = 1
    End Enum

    Dim work As Mode

    WithEvents access As Database.Access '= Program.DB

    Private Sub winPersonnelList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Filter = New DataGridViewFilter(dgList)
        Filter.AddTextFilter("編號", "名稱", "電話1", "電話2", "地址", "備註", "帳號")
        Filter.AddNumberFilter("權限")

        UpdateList()
    End Sub

    Public Overloads Sub Show(ByVal DB As Database.Access)
        access = DB
        ShowDialog()
    End Sub

    Public Overloads Sub ShowDialog(ByVal DB As Database.Access)
        access = DB
        If Not CheckAuthority(1, WithAdmin:=True) Then Exit Sub
        work = Mode.Normal
        MyBase.ShowDialog()
    End Sub


    Public Function SelectDialog(ByVal DB As Database.Access) As Database.Personnel
        access = DB
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        work = Mode.SelectItem
        If MyBase.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Return GetSelectedItem()
        Else
            Return Database.Personnel.Null()
        End If
    End Function

    Dim dt As DataTable
    Private Sub UpdateList()
        dt = access.GetPersonnelList()
        Dim table As DataTable = dt

        Dim rowAdmin As Data.DataRow = table.Rows(0)
        For Each row As Data.DataRow In table.Rows
            If Trim(row("ID")) = Database.Personnel.Administrator.Label Then rowAdmin = row
        Next
        table.Rows.Remove(rowAdmin)

        dgList.DataSource = table
        dgList.Columns("Password").Visible = False

        UpdateTitle("Label", "編號")
        UpdateTitle("Name", "名稱")
        UpdateTitle("Tel1", "電話1")
        UpdateTitle("Tel2", "電話2")
        UpdateTitle("Addr", "地址")
        UpdateTitle("Note", "備註")
        UpdateTitle("Authority", "權限")
        UpdateTitle("ID", "帳號")
        Filter.UpdateComboBox()
    End Sub

    Private Sub UpdateTitle(ByVal Label As String, ByVal Text As String)
        dgList.Columns(Label).HeaderText = Text
    End Sub

    Private Sub 新增AToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 新增AToolStripMenuItem.Click, 新增CToolStripMenuItem1.Click
        winPeople.Create(GetNewPersonnel(), access)
    End Sub

    Private Sub 修改CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 修改CToolStripMenuItem.Click
        EditGoods()
    End Sub

    Private Sub EditGoods()
        Dim selectedItem As Database.Personnel = GetSelectedItem()
        If selectedItem.IsNull() Then
            MsgBox("您必須選擇一個項目")
            Exit Sub
        End If

        winPeople.Open(selectedItem, access)
    End Sub

    Public Function GetSelectedItem() As Database.Personnel
        If Not Filter.HasSelectedItem Then Return Database.Personnel.Null()
        Dim dt As DataTable = dgList.DataSource

        Dim label As String = dgList.SelectedRows(0).Cells(0).Value

        For Each r As DataRow In dt.Rows
            If r(0) = label Then
                Return Database.Personnel.GetFrom(r)

            End If

        Next
        Return Database.Personnel.Null() 'Database.Supplier.GetFrom(dt.Rows(dgList.SelectedRows(0).Index))
    End Function


    Private Sub 刪除DToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 刪除DToolStripMenuItem.Click

        If Not CheckAuthority(3, WithAdmin:=True) Then Exit Sub

        Dim SelectedItem As Database.Personnel = GetSelectedItem()
        If winLogIn.ShowDialog("請輸入使用者密碼", CurrentUser.ID).State <> LoginState.Success Then Exit Sub
        If SelectedItem.IsNull() Then
            MsgBox("您必須選擇一個項目")
            Exit Sub
        End If


        Dim count As Integer = access.GetSalesListByPersonnel(SelectedItem.Label).Rows.Count
        For Each c As Database.AccessClient In Client.Client
            If c.Connected Then count += c.GetSalesListByPersonnel(SelectedItem.Label).Rows.Count
        Next
        If count > 0 Then
            MsgBox("此員工已有銷售記錄，無法刪除!")
            Exit Sub
        End If

        If MsgBox("這麼做將會刪除該此員工的資料，您確定要這麼做？", MsgBoxStyle.OkCancel + MsgBoxStyle.Question) = MsgBoxResult.Cancel Then
            Exit Sub
        End If

        access.DeletePersonnel(SelectedItem)
    End Sub

    Private Sub dgGoodsList_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgList.CellMouseDoubleClick
        If work = Mode.Normal Then
            EditGoods()
        Else
            Me.DialogResult = Windows.Forms.DialogResult.OK
        End If


    End Sub

    'Private Sub access_CreatedPersonnel(ByVal sender As Object, ByVal per As Database.StructureBase.Personnel) Handles access.CreatedPersonnel, access.ChangedPersonnel, access.DeletedPersonnel
    '    UpdateList()
    'End Sub


    Delegate Sub DelegateItem(ByVal sender As Object, ByVal sup As Database.Personnel)
    Dim invCreate As New DelegateItem(AddressOf access_CreatedItem)
    Dim invDelete As New DelegateItem(AddressOf access_DeletedItem)
    Dim invChange As New DelegateItem(AddressOf access_ChangedItem)

    Private Sub access_CreatedItem(ByVal sender As Object, ByVal item As Database.StructureBase.Personnel) Handles access.CreatedPersonnel
        If Me.InvokeRequired Then
            Me.Invoke(invCreate, sender, item)
            Exit Sub
        End If
        With item
            dt.Rows.Add(.Label, .Name, .Tel1, .Tel2, .Note, .ID, .Password, .Authority, .Addr, .Modify)
        End With
    End Sub

    Private Sub access_ChangedItem(ByVal sender As Object, ByVal item As Database.StructureBase.Personnel) Handles access.ChangedPersonnel
        If Me.InvokeRequired Then
            Me.Invoke(invChange, sender, item)
            Exit Sub
        End If

        For Each r As DataRow In dt.Rows
            If Strings.RTrim(r("Label")) = item.Label Then item.UpdateRow(r) 'r.ItemArray = sup.ToObjects()
        Next
    End Sub


    Private Sub access_DeletedItem(ByVal sender As Object, ByVal item As Database.StructureBase.Personnel) Handles access.DeletedPersonnel
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