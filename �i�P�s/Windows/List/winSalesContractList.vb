Public Class winSalesContractList
    WithEvents access As Database.Access
    Dim Filter As DataGridViewFilter
    Dim SalesContractVisiblePath As String = My.Application.Info.DirectoryPath & "\SalesConstractVisible.xml"
    Public Sub New()

        ' 此為 Windows Form 設計工具所需的呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。
        Filter = New DataGridViewFilter(dgItemList)
        Filter.AddTextFilter("銷貨編號", "合約編號", "門號", "合約名稱", "合約備註")
        Filter.AddNumberFilter("折扣", "佣金")
    End Sub

    Private Sub winSalesContractList_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        access = Nothing
    End Sub

    Private Sub winSalesContractList_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        dtpStart.Value = Today
        dtpEnd.Value = Today.AddDays(1).AddSeconds(-1)
    End Sub

    Private Sub btRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btRead.Click
        LoadList()
    End Sub

    Public Overloads Sub Show(ByVal db As Database.Access)
        Me.access = db
        MyBase.Show()
        LoadList()
    End Sub


    Private Sub LoadList()
        Dim dt As DataTable = access.GetSalesContractList(dtpStart.Value.Date, dtpEnd.Value.Date.AddDays(1).AddSeconds(-1))
        'dgItemList.DataSource = dt


        If dt IsNot Nothing Then
            dt.Columns("SalesLabel").ColumnName = "銷貨編號"
            dt.Columns("SalesDate").ColumnName = "銷貨日期"
            dt.Columns("ContractLabel").ColumnName = "合約編號"
            dt.Columns("Discount").ColumnName = "折扣"
            dt.Columns("Phone").ColumnName = "門號"
            dt.Columns("Commission").ColumnName = "佣金"
            dt.Columns("Name").ColumnName = "合約名稱"
            dt.Columns("Note").ColumnName = "合約備註"
            dt.Columns("ReturnDate").ColumnName = "扣佣日期"
        End If

        dgItemList.DataSource = dt

        If dt IsNot Nothing Then
            DataGridViewVisibleDialog.SetVisible(dgItemList, Code.LoadXml(Of String())(SalesContractVisiblePath, GetColumns(dt)))
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

    Private Sub 開啟銷貨SToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 開啟銷貨SToolStripMenuItem.Click
        OpenSales()
    End Sub

    Private Sub OpenSales()
        If dgItemList.SelectedRows.Count <= 0 Then
            MsgBox("您至少必須選擇一個項目!")
            Exit Sub
        End If


        Dim row As DataGridViewRow = dgItemList.SelectedRows(0)

        Dim SalesLabel As String = row.Cells(0).Value

        winSales.Open(SalesLabel, access)
        'Dim win As New winSales
        'win.Open(SalesLabel, access)
    End Sub


End Class