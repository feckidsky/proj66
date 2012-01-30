Public Class DataGridViewPrintDialog

    Dim DataGrid As DataGridView
    Dim printFont As Font = New Font("新細明體", 12)

    Dim Title As String = ""


    Private Sub DataGridViewPrintDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btFont.Font = printFont

        PrintDocument1.PrinterSettings.DefaultPageSettings.Margins.Top = 15
        PrintDocument1.PrinterSettings.DefaultPageSettings.Margins.Bottom = 15
        PrintDocument1.PrinterSettings.DefaultPageSettings.Margins.Left = 15
        PrintDocument1.PrinterSettings.DefaultPageSettings.Margins.Right = 15
    End Sub

    Public Overloads Sub ShowDialog(ByVal Title As String, ByVal DataGrid As DataGridView)
        Me.DataGrid = DataGrid
        ckList.Items.Clear()
        Me.Title = Title
        For Each c As DataGridViewColumn In DataGrid.Columns
            ckList.Items.Add(c.HeaderText, True)
        Next
        MyBase.ShowDialog()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btPreview.Click

        PrintPreviewDialog1.Document = PrintDocument1
        PrintPreviewDialog1.ShowDialog()
    End Sub

    Private Sub btFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btFont.Click
        Me.FontDialog1.Font = printFont
        If FontDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then printFont = FontDialog1.Font
        btFont.Font = printFont
    End Sub

    Private Sub btCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btCancel.Click
        Me.Close()
    End Sub

    Private Sub btSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSetting.Click
        PrintDialog1.PrinterSettings = PrintDocument1.PrinterSettings
        PrintDialog1.Document = PrintDocument1
        Dim result As DialogResult = PrintDialog1.ShowDialog()
        If result = Windows.Forms.DialogResult.OK Then PrintDocument1.PrinterSettings = PrintDialog1.PrinterSettings
    End Sub

    Private Sub btPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btPrint.Click
        PrintDocument1.Print()
    End Sub

    Public Function GetString(ByVal obj As Object) As String
        If obj Is DBNull.Value Then Return ""
        Return obj.ToString
    End Function

    Public Function GetValue(ByVal row As DataGridViewRow, ByVal ColumnName As String) As String
        Dim idx As Integer = -1

        For i As Integer = 0 To DataGrid.Columns.Count - 1
            If DataGrid.Columns(i).HeaderText = ColumnName Then
                idx = i
                Exit For
            End If
        Next

        If idx = -1 Then Return ""
        Return Strings.Trim(row.Cells(idx).Value.ToString)
    End Function

    Public Function GetValues(ByVal row As DataGridViewRow) As String()
        Dim lst As New List(Of String)

        For i As Integer = 0 To ckList.CheckedItems.Count - 1
            lst.Add(GetValue(row, ckList.CheckedItems(i)))
        Next
        Return lst.ToArray
    End Function

    Public Function GetColumnNames() As String()
        Dim lst As New List(Of String)

        For i As Integer = 0 To ckList.CheckedItems.Count - 1
            lst.Add(ckList.CheckedItems(i))
        Next
        Return lst.ToArray

    End Function

    Private Function GetRows(ByVal idx As Integer, ByVal count As Integer) As DataGridViewRow()
        Dim rows As New List(Of DataGridViewRow)

        Do While count > 0 And idx < DataGrid.Rows.Count
            If DataGrid.Rows(idx).Visible Then
                rows.Add(DataGrid.Rows(idx))
                count -= 1
            End If
            idx += 1
        Loop

        Return rows.ToArray()
    End Function


    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Static idxRow As Integer = 0
        Static page As Integer = 1

        Dim g As System.Drawing.Graphics = e.Graphics

        '計算欄位高度
        Dim RowHeight As Integer = g.MeasureString("國", printFont).Height * 1.5

        '指定筆刷顏色
        Dim brush As New Drawing.SolidBrush(Color.Black)

        '計算最大行數
        Dim PageLine As Integer = e.MarginBounds.Height / RowHeight

        '取得本頁欄位
        Dim rows As DataGridViewRow() = GetRows(idxRow, PageLine)

        '取得欄位名稱
        Dim Columns As String() = GetColumnNames()

        '取得各欄寬度
        Dim CellWidth() As Single = Array.ConvertAll(Columns, Function(s As String) g.MeasureString(s, printFont).Width)

        '計算欄寬依最大欄位為基準
        For i As Integer = 0 To rows.Count - 1
            Dim values() As String = GetValues(rows(i))
            For j As Integer = 0 To values.Length - 1
                Dim w As Integer = g.MeasureString(values(j), printFont).Width
                If CellWidth(j) < w Then CellWidth(j) = w
            Next
        Next

        '總欄寬小於版面寬度，依比例放大欄寬
        Dim totCellWidth As Single = CellWidth.Sum() + CellWidth.Count * 3
        If totCellWidth < e.MarginBounds.Width Then
            Dim gain(CellWidth.Count - 1) As Integer
            For i As Integer = 0 To CellWidth.Count - 1
                CellWidth(i) = CellWidth(i) / (totCellWidth - CellWidth.Count * 3) * e.MarginBounds.Width
            Next
        End If

        '列印標題
        g.DrawString(Title, printFont, brush, e.MarginBounds.Left + (e.MarginBounds.Width - g.MeasureString(Title, printFont).Width) / 2, e.MarginBounds.Top - RowHeight * 1.5)

        Dim tF As Font = New Font(printFont.FontFamily, 12)
        Dim tS As SizeF = g.MeasureString(Now.ToString, tF)
        g.DrawString(Now.ToString, tF, brush, e.PageBounds.Width - tS.Width - 10, e.PageBounds.Height - tS.Height - 10)

        Dim txtPage As String = (Int((idxRow + 1) / PageLine) + 1) & "/" & (Int(DataGrid.Rows.Count / PageLine) + 1)
        tS = g.MeasureString(txtPage, tF)
        g.DrawString(txtPage, tF, brush, e.MarginBounds.Left + (e.MarginBounds.Width + tS.Width) / 2, e.PageBounds.Height - tS.Height - 10)

        '列印欄位名稱
        Dim cx As Integer = e.MarginBounds.Left
        For i As Integer = 0 To Columns.Length - 1
            g.DrawString(Columns(i), printFont, brush, cx, e.MarginBounds.Top)
            cx += CellWidth(i) + 3
        Next

        '印出資料內容
        For i As Integer = 0 To rows.Count - 1
            Dim values() As String = GetValues(rows(i))
            Dim x As Integer = e.MarginBounds.Left
            For j As Integer = 0 To values.Length - 1
                g.DrawString(values(j), printFont, brush, x, e.MarginBounds.Top + (i + 1) * RowHeight)
                x += CellWidth(j) + 3
            Next
        Next

        '取得最後一列索引值
        idxRow += rows.Count
        page += 1

        '判斷是否分頁
        e.HasMorePages = idxRow < DataGrid.Rows.Count

        '不分頁重置列數索引值
        If Not e.HasMorePages Then
            idxRow = 0
            page = 1
        End If


    End Sub



End Class