﻿Public Class DataGridViewPrintDialog

    Dim DataGrid As DataGridView
    Dim printGrid As DataTable
    Dim printFont As Font = New Font("新細明體", 12)

    Dim Title As String = ""


    Private Sub DataGridViewPrintDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Dim setting As New Printing.PageSettings()
        'setting.
        'PrintDocument1.PrinterSettings.DefaultPageSettings.Margins.Top = 10
        'PrintDocument1.PrinterSettings.DefaultPageSettings.Margins.Bottom = 10
        'PrintDocument1.PrinterSettings.DefaultPageSettings.Margins.Left = 10
        'PrintDocument1.PrinterSettings.DefaultPageSettings.Margins.Right = 10
    End Sub

    Public Overloads Sub ShowDialog(ByVal Title As String, ByVal DataGrid As DataGridView)
        Me.DataGrid = DataGrid
        ckList.Items.Clear()

        Me.Title = Title
        For Each c As DataGridViewColumn In DataGrid.Columns
            ckList.Items.Add(c.HeaderText, c.Visible)
        Next

        btFont.Font = printFont



        MyBase.ShowDialog()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btPreview.Click

        PrintPreviewDialog1.Document = PrintDocument1
        focusPage = 1
        startPage = 1
        endPage = PrintDocument1.PrinterSettings.MaximumPage

        printGrid = GetPrintGrid()
        PrintPreviewDialog1.ShowDialog()
    End Sub

    Private Function GetPrintGrid() As DataTable
        Dim grid As New DataTable
        'For i As Integer = 0 To DataGrid.Columns.Count - 1

        '    grid.Columns.Add(DataGrid.Columns(i).HeaderText)
        'Next
        grid.Columns.AddRange(Array.ConvertAll(GetColumnNames(), Function(s As String) New DataColumn(s)))

        For i As Integer = 0 To DataGrid.Rows.Count - 1
            If DataGrid.Rows(i).Visible Then
                'Dim items As New List(Of Object)
                'For Each item As DataGridViewCell In DataGrid.Rows(i).Cells
                '    items.Add(item.Value)
                'Next

                Dim items As String() = GetValues(DataGrid.Rows(i))
                ' If ckNumber.Checked Then items(0) = grid.Rows.Count + 1
                grid.Rows.Add(items)

            End If
        Next
        Return grid
    End Function


    Private Sub btFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btFont.Click
        Me.FontDialog1.Font = printFont
        If FontDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then printFont = FontDialog1.Font
        btFont.Font = printFont
    End Sub

    Private Sub btCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btCancel.Click
        Me.Close()
    End Sub

    Private Sub btSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSetting.Click
        'PrintDialog1.PrinterSettings = PrintDocument1.PrinterSettings
        PrintDialog1.Document = PrintDocument1
        Dim result As DialogResult = PrintDialog1.ShowDialog()
        PrintDocument1.PrinterSettings = PrintDialog1.PrinterSettings
        If result = Windows.Forms.DialogResult.OK Then
            printGrid = GetPrintGrid()
            SetPageRange()
            PrintDocument1.Print()
        End If

    End Sub

    Dim focusPage As Integer
    Dim startPage As Integer
    Dim endPage As Integer
    Dim LineInterval As Integer = 5

    Private Sub SetPageRange()
        Dim setting As Printing.PrinterSettings = PrintDocument1.PrinterSettings
        Select Case setting.PrintRange
            Case Printing.PrintRange.AllPages
                focusPage = 1
                endPage = setting.MaximumPage
            Case Printing.PrintRange.SomePages
                focusPage = setting.FromPage
                endPage = setting.ToPage
        End Select
        startPage = focusPage
    End Sub

    Private Sub btPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btPrint.Click
        printGrid = GetPrintGrid()
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
        If ckNumber.Checked Then lst.Add((row.Index + 1).ToString)
        For i As Integer = 0 To ckList.CheckedItems.Count - 1
            lst.Add(GetValue(row, ckList.CheckedItems(i)))
        Next
        Return lst.ToArray
    End Function

    Public Function GetColumnNames() As String()
        Dim lst As New List(Of String)
        If ckNumber.Checked Then lst.Add("項次")
        For i As Integer = 0 To ckList.CheckedItems.Count - 1
            lst.Add(ckList.CheckedItems(i))
        Next
        Return lst.ToArray

    End Function

    'Private Function GetRows(ByVal idx As Integer, ByVal count As Integer) As DataGridViewRow()
    '    Dim rows As New List(Of DataGridViewRow)

    '    Do While count > 0 And idx < DataGrid.Rows.Count
    '        If DataGrid.Rows(idx).Visible Then
    '            rows.Add(DataGrid.Rows(idx))
    '            count -= 1
    '        End If
    '        idx += 1
    '    Loop

    '    Return rows.ToArray()
    'End Function

    Public Function GetTotalPage(ByVal e As System.Drawing.Printing.PrintPageEventArgs) As Integer()
        Dim lstRowCount As New List(Of Integer)
        Dim pageSize As Size = e.MarginBounds.Size
        Dim totPage As Integer = 1

        Dim currentHeight As Integer = e.Graphics.MeasureString(Join(GetColumnNames(), ""), printFont).Height + LineInterval
        Dim text As String = ""
        Dim cntRow As Integer = 0
        For i As Integer = 0 To printGrid.Rows.Count - 1
            '取得行高
            Dim lineHeight As Integer = GetCellHeight(printGrid.Rows(i), e) + LineInterval

            If currentHeight + lineHeight > pageSize.Height Then
                lstRowCount.Add(cntRow)
                totPage += 1
                currentHeight = e.Graphics.MeasureString(Join(GetColumnNames(), ""), printFont).Height + LineInterval
                cntRow = 1
            Else
                cntRow += 1
                currentHeight += lineHeight
            End If

        Next
        lstRowCount.Add(cntRow)
        Return lstRowCount.ToArray
    End Function

    Public Function GetCellHeight(ByVal Row As DataRow, ByVal e As System.Drawing.Printing.PrintPageEventArgs) As Integer
        Dim lineHeight As Integer = 0
        'For c As Integer = 0 To Row.Cells.Count - 1
        '    Dim text As String = Row.Cells(c).Value.ToString
        '    lineHeight = Math.Max(e.Graphics.MeasureString(text, printFont).Height, lineHeight)
        'Next

        For Each o As Object In Row.ItemArray
            Dim text As String = o.ToString
            lineHeight = Math.Max(e.Graphics.MeasureString(text, printFont).Height, lineHeight)
        Next
        Return lineHeight
    End Function



    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Static idxRow As Integer = 0

        Static totPage As Integer() = New Integer() {}

        Me.Text = ""


        If totPage.Length = 0 Then
            totPage = GetTotalPage(e)
        End If

        If focusPage = 0 Then
            focusPage = 1
            startPage = 1
        End If


        If startPage > totPage.Length Then
            e.Cancel = True
            MsgBox("頁數 " & startPage & " 頁數超出範圍，該文件只有 " & totPage.Length & " 頁")
        End If

        Dim g As System.Drawing.Graphics = e.Graphics

        '指定筆刷顏色
        Dim brush As New Drawing.SolidBrush(Color.Black)

        '取得欄位名稱
        Dim Columns As String() = GetColumnNames()

        '取得各欄寬度
        Static CellWidth() As Single

        '計算欄寬依最大欄位為基準
        'For i As Integer = 0 To totPage(page - 1) - 1
        'For i As Integer = idxRow To idxRow + totPage(page - 1) - 1
        If focusPage = startPage Then
            CellWidth = Array.ConvertAll(Columns, Function(s As String) g.MeasureString(s, printFont).Width)
            For i As Integer = 0 To printGrid.Rows.Count - 1 'DataGrid.RowCount - 1
                Dim values() As Object = printGrid.Rows(i).ItemArray()  'GetValues(DataGrid.Rows(i))
                For j As Integer = 0 To values.Length - 1
                    Dim w As Integer = g.MeasureString(values(j).ToString, printFont).Width
                    If CellWidth(j) < w Then CellWidth(j) = w
                Next
            Next

            '取得起始欄
            idxRow = 0
            For i As Integer = 0 To startPage - 1 - 1
                idxRow += totPage(i)
            Next

        End If

        '若總欄寬小於版面寬度，依比例放大欄寬
        Dim totCellWidth As Single = CellWidth.Sum() + CellWidth.Count * 3
        If totCellWidth < e.MarginBounds.Width Then
            Dim gain(CellWidth.Count - 1) As Integer
            For i As Integer = 0 To CellWidth.Count - 1
                CellWidth(i) = CellWidth(i) / (totCellWidth - CellWidth.Count * 3) * e.MarginBounds.Width
            Next
        End If

        '列印標題
        g.DrawString(Title, printFont, brush, e.MarginBounds.Left + (e.MarginBounds.Width - g.MeasureString(Title, printFont).Width) / 2, e.MarginBounds.Top - g.MeasureString(Title, printFont).Height - 5)

        '列印時間
        Dim tF As Font = New Font(printFont.FontFamily, 12)
        Dim tS As SizeF = g.MeasureString(Now.ToString, tF)

        g.DrawString(Now.ToString, tF, brush, e.MarginBounds.Left + e.MarginBounds.Width - tS.Width, e.MarginBounds.Top + e.MarginBounds.Height + 10)

        '列印頁數
        Dim txtPage As String = focusPage & "/" & totPage.Length
        tS = g.MeasureString(txtPage, tF)
        g.DrawString(txtPage, tF, brush, e.MarginBounds.Left + (e.MarginBounds.Width + tS.Width) / 2, e.MarginBounds.Top + e.MarginBounds.Height + 10)

        '列印欄位名稱
        Dim cx As Integer = e.MarginBounds.Left
        Dim cy As Integer = e.MarginBounds.Top
        Dim TitleLineHeight As Integer = 0
        For i As Integer = 0 To Columns.Length - 1
            TitleLineHeight = Math.Max(TitleLineHeight, g.MeasureString(Columns(i), printFont).Height)
            g.DrawString(Columns(i), printFont, brush, cx, cy)
            cx += CellWidth(i) + 3
        Next

        Dim pen As New Pen(Color.Black)

        cy += TitleLineHeight + LineInterval
        g.DrawLine(pen, e.MarginBounds.Left, cy - 5, e.MarginBounds.Right, cy - 5)


        '印出資料內容
        For i As Integer = idxRow To idxRow + totPage(focusPage - 1) - 1
            Dim values() As Object = printGrid.Rows(i).ItemArray  'GetValues(DataGrid.Rows(i))
            Dim x As Integer = e.MarginBounds.Left
            For j As Integer = 0 To values.Length - 1
                g.DrawString(values(j).ToString, printFont, brush, x, cy)
                x += CellWidth(j) + 3
            Next
            cy += GetCellHeight(printGrid.Rows(i), e) + LineInterval 'GetCellHeight(DataGrid.Rows(i), e) + LineInterval
            g.DrawLine(pen, e.MarginBounds.Left, cy - LineInterval, e.MarginBounds.Right, cy - LineInterval)
        Next

        '取得最後一列索引值
        idxRow += totPage(focusPage - 1)
        focusPage += 1

        '判斷是否分頁
        e.HasMorePages = focusPage <= IIf(endPage > totPage.Length, totPage.Length, endPage)   'totPage.Length  'idxRow < DataGrid.Rows.Count

        '不分頁重置列數索引值
        If Not e.HasMorePages Then
            idxRow = 0
            focusPage = 1
            totPage = New Integer() {}
        End If


    End Sub



    Private Sub btPageSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btPageSetup.Click
        PageSetupDialog1.Document = PrintDocument1
        If PageSetupDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            PrintDocument1 = PageSetupDialog1.Document
        End If

    End Sub

    Private Sub PrintPreviewDialog1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintPreviewDialog1.Load

    End Sub
End Class