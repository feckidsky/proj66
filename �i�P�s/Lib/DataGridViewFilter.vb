Public Class DataGridViewFilter

    Public WithEvents DataGrid As DataGridView
    Dim TextFilters As New List(Of TextFilter)
    Dim NumberFilters As New List(Of NumberFilter)
    Dim BoolFilters As New List(Of BoolFilter)
    Dim ContextMenuStrip As ContextMenuStrip

    Sub New(ByVal DataGridView As DataGridView)
        AddHandler DataGridView.ColumnHeaderMouseClick, AddressOf DataGrid_ColumnHeaderMouseClick
        Me.DataGrid = DataGridView
        ContextMenuStrip = DataGrid.ContextMenuStrip
        DataGrid.ContextMenuStrip = Nothing
        DataGrid.EnableHeadersVisualStyles = False
        DataGrid.RowsDefaultCellStyle.SelectionForeColor = Color.White
    End Sub

#Region "Filter Structure"
    Structure BoolFilter
        Dim HeaderName As String
        Dim cms As ContextMenuStrip
        Sub New(ByVal ContextMenuStrip As ContextMenuStrip, ByVal HeaderName As String)
            Me.HeaderName = HeaderName
            Me.cms = ContextMenuStrip
        End Sub

        Function Match(ByVal value As Boolean) As Boolean
            Dim selectedIndex As Integer = CType(cms.Items("cbText"), ToolStripComboBox).SelectedIndex
            If selectedIndex = 0 Then Return True
            If selectedIndex = 1 And value Then Return True
            If selectedIndex = 2 And Not value Then Return True
            Return False
        End Function

    End Structure

    Structure NumberFilter
        Dim HeaderName As String
        Dim cms As ContextMenuStrip

        Sub New(ByVal ContextMenuStrip As ContextMenuStrip, ByVal HeaderName As String)
            Me.cms = ContextMenuStrip
            Me.HeaderName = HeaderName
        End Sub

        Function Match(ByVal Text As String) As Boolean
            'Return True
            Dim max As Single = GetValue(cms.Items("txtMax").Text, Single.MaxValue)
            Dim min As Single = GetValue(cms.Items("txtMin").Text, Single.MinValue)
            Dim v As Single = GetValue(Text, 0)
            Return v >= min And v <= max
        End Function

        Function GetValue(ByVal obj As Object, ByVal DefaultValue As Single) As Single
            If obj Is Nothing OrElse obj = "" Then Return DefaultValue
            Try
                Return Val(obj)
            Catch ex As Exception
                Return DefaultValue
            End Try
        End Function
    End Structure

    Structure TextFilter
        Dim HeaderName As String
        Dim cms As ContextMenuStrip
        Sub New(ByVal cms As ContextMenuStrip, ByVal ColumnName As String)
            Me.HeaderName = ColumnName
            Me.cms = cms
        End Sub

        Function Match(ByVal Value As String) As Boolean
            Dim Text As String = cms.Items("cbText").Text
            If Text = "" Then Return True
            Return Strings.InStr(Value, Text) <> 0
        End Function
    End Structure
#End Region

#Region "AddFilter"
    Public Sub AddTextFilter(ByVal ParamArray ColumnNames() As String)
        For Each c As String In ColumnNames
            AddTextFilter(c)
        Next
    End Sub

    '加入篩選條件
    Public Sub AddTextFilter(ByVal ColumnName As String)
        Dim cms As New ContextMenuStrip()
        cms.BackColor = Color.LightGray
        Dim lbCancel As New ToolStripMenuItem("取消篩選", Nothing)

        lbCancel.Name = "lbCancel"
        lbCancel.Enabled = False
        Dim cbText As New ToolStripComboBox("cbText")
        'cbText.Fl
        cms.Text = ColumnName
        cms.Items.Add(lbCancel)
        cms.Items.Add(cbText)

        AddHandler cbText.KeyPress, AddressOf Combo_KeyPress
        AddHandler cbText.TextUpdate, AddressOf Combo_BoxTextUpdate
        AddHandler cbText.SelectedIndexChanged, AddressOf Combo_SelectedIndexChanged
        AddHandler lbCancel.Click, AddressOf TextFilterCancel_Click
        TextFilters.Add(New TextFilter(cms, ColumnName))
    End Sub

    Public Sub AddBoolFilter(ByVal ParamArray ColumnNames() As String)
        For Each c As String In ColumnNames
            AddBoolFilter(c)
        Next
    End Sub

    Public Sub AddBoolFilter(ByVal ColumnName As String)
        Dim cms As New ContextMenuStrip()
        Dim cbText As New ToolStripComboBox("cbText")
        cbText.Items.AddRange(New String() {"全選", "Yes", "No"})
        cbText.DropDownStyle = ComboBoxStyle.DropDownList
        cbText.SelectedIndex = 0
        cms.Items.Add(cbText)
        cms.Text = ColumnName
        cms.BackColor = Color.LightGray
        AddHandler cbText.SelectedIndexChanged, AddressOf Combo_SelectedIndexChanged
        BoolFilters.Add(New BoolFilter(cms, ColumnName))
    End Sub


    Public Sub AddNumberFilter(ByVal ParamArray ColumnName() As String)
        For Each c As String In ColumnName
            AddNumberFilter(c)
        Next
    End Sub
    Public Sub AddNumberFilter(ByVal ColumnName As String)

        Dim cms As New ContextMenuStrip()
        cms.BackColor = Color.LightGray
        Dim lbCancel As New ToolStripMenuItem("取消篩選", Nothing)
        Dim lbMax As New ToolStripMenuItem("範圍-高", Nothing)
        Dim lbMin As New ToolStripMenuItem("範圍-低", Nothing)

        lbCancel.Name = "lbCancel"
        lbCancel.Enabled = False
        Dim txtMax As New ToolStripTextBox("txtMax")
        Dim txtMin As New ToolStripTextBox("txtMin")
        txtMax.BorderStyle = BorderStyle.FixedSingle
        txtMin.BorderStyle = BorderStyle.FixedSingle
        txtMax.ToolTipText = "最大值"
        txtMin.ToolTipText = "最小值"
        cms.Text = ColumnName
        cms.Items.Add(lbCancel)
        cms.Items.Add(lbMax)
        cms.Items.Add(txtMax)
        cms.Items.Add(lbMin)
        cms.Items.Add(txtMin)

        AddHandler lbCancel.Click, AddressOf NumberFilterCancel_Click
        AddHandler txtMax.KeyPress, AddressOf Number_KeyPress
        AddHandler txtMin.KeyPress, AddressOf Number_KeyPress

        NumberFilters.Add(New NumberFilter(cms, ColumnName))
    End Sub
#End Region

    Sub SetTextFilter(ByVal ColumnText As String, ByVal Text As String)
        Dim idx As Integer = TextFilters.FindIndex(Function(f As TextFilter) f.HeaderName = ColumnText)
        If idx >= 0 Then
            TextFilters(idx).cms.Items("cbText").Text = Text
        End If
        'Filter()
    End Sub

    Sub SetBoolFilter(ByVal ColumnText As String, ByVal value As Boolean)
        Dim idx As Integer = BoolFilters.FindIndex(Function(f As BoolFilter) f.HeaderName = ColumnText)
        If idx >= 0 Then
            CType(BoolFilters(idx).cms.Items("cbText"), ToolStripComboBox).SelectedIndex = IIf(value, 1, 2)
        End If
    End Sub

    Sub CancelBoolFilter(ByVal ColumnText As String)
        Dim idx As Integer = BoolFilters.FindIndex(Function(f As BoolFilter) f.HeaderName = ColumnText)
        If idx >= 0 Then
            CType(BoolFilters(idx).cms.Items("cbText"), ToolStripComboBox).SelectedIndex = 0
        End If
    End Sub

    '取消篩選
    Sub TextFilterCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lb As ToolStripMenuItem = sender
        lb.Owner.Items("cbText").Text = ""
        Filter()
    End Sub

    '篩選內容變更
    Sub Combo_BoxTextUpdate(ByVal sender As Object, ByVal e As System.EventArgs)
        Filter()
    End Sub

    '直接選取篩選內容
    Sub Combo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim cb As ToolStripComboBox = sender
        Filter()
        cb.Owner.Hide()
    End Sub
    '關閉文字篩選功能表
    Sub Combo_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        Dim cb As ToolStripComboBox = sender
        If e.KeyChar = Chr(13) Then cb.Owner.Hide()
    End Sub

    Sub NumberFilterCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim cb As ToolStripMenuItem = sender
        cb.Owner.Items("txtMax").Text = ""
        cb.Owner.Items("txtMin").Text = ""
        Filter()
    End Sub

    Sub Number_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        Dim cb As ToolStripTextBox = sender
        If e.KeyChar = Chr(13) Then
            Filter()
            cb.Owner.Hide()
        End If
    End Sub

    Private Function GetColumn(ByVal HeaderText As String) As DataGridViewColumn
        For Each c As DataGridViewColumn In DataGrid.Columns
            If c.HeaderText = HeaderText Then Return c
        Next
        Return Nothing
    End Function

    Private Function GetCellValue(ByVal Row As DataGridViewRow, ByVal HeaderText As String) As Object
        For Each c As DataGridViewCell In Row.Cells
            If DataGrid.Columns(c.ColumnIndex).HeaderText = HeaderText Then Return GetValue(c.Value)
        Next
        Return Nothing
    End Function

    '更新篩選功能表的外觀
    Private Sub UpdateToolStripState()
        Dim f As Font = DataGrid.DefaultCellStyle.Font
        Dim FilterFont As New Font(f.FontFamily, f.Size, FontStyle.Bold + FontStyle.Italic, f.Unit)

        Dim bc As Color = DataGrid.RowHeadersDefaultCellStyle.BackColor
        Dim FilterColor As Color = Color.Red

        If DataGrid.Rows.Count = 0 Then Exit Sub
        For Each c As TextFilter In TextFilters
            Dim enable As Boolean = Not (c.cms.Items("cbText").Text = "")
            Dim column As DataGridViewColumn = GetColumn(c.HeaderName)
            column.DefaultCellStyle.Font = IIf(enable, FilterFont, f)
            column.HeaderCell.Style.BackColor = IIf(enable, FilterColor, bc)
            c.cms.Items("lbCancel").Enabled = enable
        Next

        For Each c As NumberFilter In NumberFilters
            Dim enable As Boolean = c.cms.Items("txtMax").Text <> "" Or c.cms.Items("txtMin").Text <> ""
            Dim column As DataGridViewColumn = GetColumn(c.HeaderName)
            If column Is Nothing Then Continue For
            column.DefaultCellStyle.Font = IIf(enable, FilterFont, f)
            column.HeaderCell.Style.BackColor = IIf(enable, FilterColor, bc)
            c.cms.Items("lbCancel").Enabled = enable
        Next

        For Each c As BoolFilter In BoolFilters
            Dim enable As Boolean = CType(c.cms.Items("cbText"), ToolStripComboBox).SelectedIndex <> 0
            Dim column As DataGridViewColumn = GetColumn(c.HeaderName)
            If column Is Nothing Then Continue For
            column.HeaderCell.Style.BackColor = IIf(enable, FilterColor, bc)
        Next
    End Sub

    '執行篩選動作
    Public Sub Filter()
        UpdateToolStripState()

        DataGrid.CurrentCell = Nothing

        ' Try
        For i As Integer = 0 To DataGrid.Rows.Count - 1
            FilterRow(DataGrid.Rows(i))
            'Dim Match As Boolean = True
            'For Each c As TextFilter In TextFilters
            '    Match = Match And c.Match(GetCellValue(DataGrid.Rows(i), c.HeaderName))
            'Next

            'For Each c As NumberFilter In NumberFilters

            '    Match = Match And c.Match(GetCellValue(DataGrid.Rows(i), c.HeaderName))
            'Next

            'For Each c As BoolFilter In BoolFilters
            '    Match = Match And c.Match(GetCellValue(DataGrid.Rows(i), c.HeaderName))
            'Next

            'DataGrid.Rows(i).Visible = Match
        Next
        'Catch
        '    MsgBox(Err.Description)
        'End Try
    End Sub

    Public Sub FilterRow(ByVal row As DataGridViewRow)
        Dim Match As Boolean = True
        For Each c As TextFilter In TextFilters
            Match = Match And c.Match(GetCellValue(row, c.HeaderName))
        Next

        For Each c As NumberFilter In NumberFilters

            Match = Match And c.Match(GetCellValue(row, c.HeaderName))
        Next

        For Each c As BoolFilter In BoolFilters
            Match = Match And c.Match(GetCellValue(row, c.HeaderName))
        Next

        row.Visible = Match
    End Sub

    '當使用滑鼠右鍵時，將該行選起來
    Private Sub DataGrid_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGrid.CellMouseClick
        If e.Button <> MouseButtons.Right Or e.RowIndex = -1 Then Exit Sub
        For Each row As DataGridViewRow In DataGrid.SelectedRows
            row.Selected = False
        Next

        DataGrid.Rows(e.RowIndex).Selected = True
    End Sub


    '顯示篩選功能表
    Private Sub DataGrid_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGrid.ColumnHeaderMouseClick

        If e.Button <> Windows.Forms.MouseButtons.Right Then Exit Sub
        Dim rec As Rectangle = DataGrid.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, False)
        Dim loc As New Point(rec.X, rec.Y + rec.Height)
        Dim HeaderText As String = DataGrid.Columns(e.ColumnIndex).HeaderText
        For i As Integer = 0 To TextFilters.Count - 1
            If TextFilters(i).HeaderName = HeaderText Then
                TextFilters(i).cms.Show(sender, loc)
                Exit Sub
            End If
        Next


        For i As Integer = 0 To NumberFilters.Count - 1
            If NumberFilters(i).HeaderName = HeaderText Then
                NumberFilters(i).cms.Show(sender, loc)
                Exit Sub
            End If

        Next

        For i As Integer = 0 To BoolFilters.Count - 1
            If BoolFilters(i).HeaderName = HeaderText Then
                BoolFilters(i).cms.Show(sender, loc)
                Exit Sub
            End If
        Next
    End Sub


    '顯示預設的快捷功能表
    Private Sub DataGrid_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGrid.MouseClick
        If e.Button = MouseButtons.Right And ContextMenuStrip IsNot Nothing Then ContextMenuStrip.Show(sender, e.X, e.Y)
    End Sub

    Public Sub AddComboBoxItem(ByVal row As DataGridViewRow)
        For Each c As TextFilter In TextFilters
            Dim cb As ToolStripComboBox = c.cms.Items("cbText")
            Dim text As String = GetCellValue(row, c.HeaderName)
            If text = Nothing Then Continue For
            If cb.Items.IndexOf(text) = -1 Then cb.Items.Add(text)
        Next
    End Sub

    Public Sub ClearComboBoxItem()
        For Each c As TextFilter In TextFilters
            CType(c.cms.Items("cbText"), ToolStripComboBox).Items.Clear()
        Next
    End Sub

    Public Sub UpdateComboBox()
        ClearComboBoxItem()


        For Each row As DataGridViewRow In DataGrid.Rows
            AddComboBoxItem(row)
            'For Each c As TextFilter In TextFilters
            '    Dim cb As ToolStripComboBox = c.cms.Items("cbText")
            '    Dim text As String = GetCellValue(row, c.HeaderName)
            '    If cb.Items.IndexOf(text) = -1 Then cb.Items.Add(text)
            'Next
        Next
        Filter()
    End Sub

    Private Function GetValue(ByVal obj As Object) As String
        If obj Is DBNull.Value Then Return ""
        Return obj.ToString
    End Function

    Public Function HasSelectedItem() As Boolean
        Return DataGrid.SelectedRows.Count <> 0 AndAlso DataGrid.SelectedRows(0).Visible
    End Function


    Private Sub DataGrid_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid.Sorted
        Filter()
    End Sub
End Class
