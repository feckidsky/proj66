Public Class DataGridViewVisibleDialog

    Dim DataGrid As DataGridView
    Dim Changed As Boolean = False
    Dim Adding As Boolean = False
    Public Overloads Function ShowDialog(ByVal DataGrid As DataGridView) As Boolean
        Me.DataGrid = DataGrid
        ckList.Items.Clear()

        Adding = True
        For Each c As DataGridViewColumn In DataGrid.Columns

            ckList.Items.Add(c.HeaderText, c.Visible)
        Next
        Adding = False
        Changed = False
        MyBase.ShowDialog()
        SetVisible(DataGrid, GetStrings())
        Return Changed
    End Function

    Private Sub btCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btCancel.Click
        Me.Close()
    End Sub

    Public Function GetColumnNames() As String()
        Dim lst As New List(Of String)

        For i As Integer = 0 To ckList.CheckedItems.Count - 1

            lst.Add(ckList.CheckedItems(i))
        Next
        Return lst.ToArray

    End Function

    Private Shared Function Checked(ByVal CheckedItem() As String, ByVal Name As String) As Boolean
        For i As Integer = 0 To CheckedItem.Length - 1
            If CheckedItem(i) = Name Then Return True
        Next
        Return False
    End Function

    Public Function GetStrings() As String()
        Dim lst As New List(Of String)
        For i As Integer = 0 To ckList.CheckedItems.Count - 1
            lst.Add(ckList.CheckedItems(i))
        Next
        Return lst.ToArray
    End Function

    Private Sub ckList_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles ckList.ItemCheck
        If Adding Then Exit Sub
        'SetVisible(DataGrid, GetStrings())
        Changed = True
    End Sub

    Private Sub ckList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckList.SelectedIndexChanged

    End Sub

    Public Shared Function GetVisibleColumns(ByVal datagrid As DataGridView) As String()
        Dim lst As New List(Of String)
        For Each c As DataGridViewColumn In datagrid.Columns
            If c.Visible Then lst.Add(c.HeaderText)
        Next
        Return lst.ToArray
    End Function

    Public Shared Sub SetVisible(ByVal DataGrid As DataGridView, ByVal SelectedItem() As String)
        For Each c As DataGridViewColumn In DataGrid.Columns
            c.Visible = Checked(SelectedItem, c.HeaderText)
        Next
    End Sub


End Class