Imports 進銷存.Database

Public Class winStockMoveList
    WithEvents access As Database.Access
    Dim Filter As DataGridViewFilter


    Public Sub New()

        ' 此為 Windows Form 設計工具所需的呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。
        Filter = New DataGridViewFilter(dgList)

        Filter.AddTextFilter("庫存編號", "調貨編號", "品名", "種類", "廠牌", "供應商", "IMEI", "狀態")
        Filter.AddNumberFilter("數量", "進貨價")
    End Sub


    Public Overloads Sub Show(ByVal db As Database.Access)
        access = db

        MyBase.Show()
        MyBase.BringToFront()
        UpdateTitle()
        BeginUpdateStockMoveList()
    End Sub

    Private Sub rToday_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rToday.CheckedChanged, r30Day.CheckedChanged, rUserTime.CheckedChanged, dtpStart.ValueChanged, dtpEnd.ValueChanged
        If Me.Created Then BeginUpdateStockMoveList()
    End Sub

    Dim UpdateTitleHandler As New Action(AddressOf UpdateTitle)
    Public Sub UpdateTitle()
        If Not Me.Created Then Exit Sub
        If Me.InvokeRequired Then
            Me.Invoke(UpdateTitleHandler)
            Exit Sub
        End If


        Me.Text = "調貨記錄 - " & access.Name & " - " & IIf(access.Connected, "已連線", "斷線")
    End Sub

    Structure Period
        Dim StartTime As Date
        Dim EndTime As Date
        Sub New(ByVal Start As Date, ByVal [End] As Date)
            StartTime = Start : EndTime = [End]
        End Sub
    End Structure

    Delegate Function DelegateGetPeriod() As Period
    Dim GetPeriodHandler As New DelegateGetPeriod(AddressOf GetPeriod)
    Private Function GetPeriod() As Period
        If Me.InvokeRequired Then
            Return Me.Invoke(GetPeriodHandler)
        End If

        If r30Day.Checked Then
            Return New Period(Today.AddDays(1).AddSeconds(-1), Today.AddDays(-30))
        ElseIf rToday.Checked Then
            Return New Period(Today.AddDays(1).AddSeconds(-1), Today)
        Else
            Return New Period(dtpStart.Value.Date, dtpEnd.Value.Date.AddDays(1).AddSeconds(-1))
        End If
    End Function

    Dim dt As DataTable
    Public Sub BeginUpdateStockMoveList()

        Dim Period As Period = GetPeriod()


        Dim dialog As New ProgressDialog
        dialog.Thread = New Threading.Thread(New Threading.ParameterizedThreadStart(AddressOf UpdateStockMoveList))

        Dim args As UpdateStockMoveListArgs
        args.period = Period
        args.Progress = dialog.GetProgress("取得調貨資訊")
   
        dialog.Show()
        dialog.Thread.Start(args)
    End Sub

    Structure UpdateStockMoveListArgs
        Dim Progress As Database.Access.Progress
        Dim period As Period
    End Structure


    Private Sub UpdateStockMoveList(ByVal args As UpdateStockMoveListArgs)
        dt = access.GetStockMoveList(args.period.StartTime, args.period.EndTime, args.Progress)
        Me.Invoke(New Action(Of DataTable)(AddressOf UpdateStockMoveDataTable), dt)
        args.Progress.Finish()
    End Sub

    Private Sub UpdateStockMoveDataTable(ByVal dt As DataTable)
        If dgList.Columns.Count = 0 Then
            For i As Integer = 0 To dt.Columns.Count - 1
                dgList.Columns.Add(dt.Columns(i).ColumnName, dt.Columns(i).ColumnName)
            Next
        End If

        dgList.Rows.Clear()
        If Not IO.File.Exists(StockMoveVisiblePath) Then
            Code.SaveXml(DataGridViewVisibleDialog.GetVisibleColumns(dgList), StockMoveVisiblePath)
        Else
            DataGridViewVisibleDialog.SetVisible(dgList, Code.LoadXml(Of String())(StockMoveVisiblePath, New String() {}))
        End If

        If Filter IsNot Nothing Then Filter.ClearComboBoxItem()
        For i As Integer = 0 To dt.Rows.Count - 1
            Dim arr As String() = Array.ConvertAll(dt.Rows(i).ItemArray, Function(o As Object) o.ToString)
            AddRowInfo(arr)
        Next

    End Sub


    Dim AddRowInfoHandler As New Action(Of Object())(AddressOf AddRowInfo)
    Dim UpdateRowInfoHandler As New Action(Of Object())(AddressOf UpdateRowInfo)


    Private Sub AddRowInfo(ByVal arr As Object)
        If Not Me.Created Then Exit Sub
        If Me.InvokeRequired Then
            Me.Invoke(AddRowInfoHandler, arr)
            Exit Sub
        End If

        Dim idxAction As Integer = dgList.Columns("狀態").Index
        arr(idxAction) = StockMove.TypeText(arr(idxAction))
        Dim idx As Integer = dgList.Rows.Add(CType(arr, Object()))
        dgList.Sort(dgList.Columns(0), System.ComponentModel.ListSortDirection.Descending)
        Filter.AddComboBoxItem(dgList.Rows(idx))
        Filter.FilterRow(dgList.Rows(idx))
    End Sub

    Private Sub UpdateRowInfo(ByVal arr As Object)
        If Not Me.Created Then Exit Sub
        If Me.InvokeRequired Then
            Me.Invoke(UpdateRowInfoHandler, arr)
            Exit Sub
        End If
        Dim idxAction As Integer = dgList.Columns("狀態").Index
        Dim idxLabel As Integer = dgList.Columns("調貨編號").Index
        arr(idxAction) = StockMove.TypeText(arr(idxAction))

        For Each row As DataGridViewRow In dgList.Rows
            If row.Cells(idxLabel).Value = arr(idxLabel) Then
                For i As Integer = 0 To row.Cells.Count - 1
                    row.Cells(i).Value = arr(i)
                Next
                Filter.AddComboBoxItem(row)
                Filter.FilterRow(row)
                Exit Sub
            End If
        Next


    End Sub

    Private Sub dgList_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgList.SelectionChanged
        If dgList.SelectedRows.Count = 0 Then Exit Sub
        Dim idxAction As Integer = dgList.Columns("狀態").Index
        Dim row As DataGridViewRow = dgList.SelectedRows(0)
        Dim shop As ShopArgs = GetShopState(row)
        Dim text As String = row.Cells(idxAction).Value

        Dim cancelState As String() = {"申請", "調出(未送達)"}
        Dim InState As String() = {"調貨中", "取消"}
        Dim OutState As String() = {"申請", "取消"}
        Dim IsSource As Boolean = shop.Source Is CurrentAccess
        Dim IsDestine As Boolean = shop.Destine Is CurrentAccess

        tsCancel.Visible = Array.Exists(cancelState, Function(s As String) s = text) And (IsSource Or IsDestine)
        tsIn.Visible = Array.Exists(InState, Function(s As String) s = text) And IsDestine
        tsOut.Visible = Array.Exists(OutState, Function(s As String) s = text) And IsSource
    End Sub

    Private Sub access_ConnectStateChanged(ByVal Client As TCPTool.Client) Handles access.ConnectedFail, access.ConnectedSuccess
        UpdateTitle()
    End Sub



    Private Sub access_CreatedStockMove(ByVal sender As Object, ByVal data As Database.StockMove) Handles access.CreatedStockMove
        Dim arr() As Object = access.GetStockMoveRow(data.Label).ItemArray
        AddRowInfo(arr)
    End Sub

    Private Sub access_ChangedStockMove(ByVal sender As Object, ByVal data As Database.StockMove) Handles access.ChangedStockMove
        Dim arr() As Object = access.GetStockMoveRow(data.Label).ItemArray
        UpdateRowInfo(arr)
    End Sub

    Private Sub tsCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsCancel.Click
        If dgList.SelectedRows.Count = 0 Then
            MsgBox("您必須選擇一個項目")
            Exit Sub
        End If

        Dim row As DataGridViewRow = dgList.SelectedRows(0)

        Dim res As ShopArgs = GetShopState(row)
        If res.Connected Then
            Dim stockMove As StockMove = access.GetStockMove(row.Cells("調貨編號").Value)

            If stockMove.Action = Database.StockMove.Type.Sending Then
                Dim stock As Stock = res.Source.GetStock(stockMove.StockLabel)
                stock.Number += stockMove.Number
                res.Source.ChangeStock(stock)
            End If

            stockMove.Action = Database.StockMove.Type.Cancel
            res.Source.ChangeStockMove(stockMove)
            res.Destine.ChangeStockMove(stockMove)



        Else
            MsgBox(res.Message)
        End If


    End Sub

    Private Sub tsOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsOut.Click
        If dgList.SelectedRows.Count = 0 Then
            MsgBox("您必須選擇一個項目")
            Exit Sub
        End If

        Dim row As DataGridViewRow = dgList.SelectedRows(0)

        Dim res As ShopArgs = GetShopState(row)
        If res.Connected Then
            Dim stockMove As StockMove = access.GetStockMove(row.Cells("調貨編號").Value)
            stockMove.SourcePersonnel = CurrentUser.Label
            stockMove.Action = Database.StockMove.Type.Sending
            res.Source.ChangeStockMove(stockMove)
            stockMove.Action = Database.StockMove.Type.Receiving
            res.Destine.ChangeStockMove(stockMove)

            Dim stock As Stock = res.Source.GetStock(stockMove.StockLabel)
            stock.Number -= stockMove.Number
            res.Source.ChangeStock(stock)
        Else
            MsgBox(res.Message)
        End If
    End Sub

    Private Sub tsIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsIn.Click
        If dgList.SelectedRows.Count = 0 Then
            MsgBox("您必須選擇一個項目")
            Exit Sub
        End If

        Dim row As DataGridViewRow = dgList.SelectedRows(0)

        Dim res As ShopArgs = GetShopState(row)
        If res.Connected Then
            Dim stockMove As StockMove = access.GetStockMove(row.Cells("調貨編號").Value)
            stockMove.DestinePersonnel = CurrentUser.Label

            stockMove.Action = Database.StockMove.Type.Out
            res.Source.ChangeStockMove(stockMove)
            stockMove.Action = Database.StockMove.Type.In
            res.Destine.ChangeStockMove(stockMove)

            Dim stock As Stock = res.Destine.GetStock(stockMove.StockLabel)
            If stock.IsNull() Then
                stock = res.Source.GetStock(stockMove.StockLabel)
                stock.Number = stockMove.Number
                res.Destine.AddStock(stock)
            Else
                stock.Number += stockMove.Number
                res.Destine.ChangeStock(stock)
            End If

        Else
            MsgBox(res.Message)
        End If
    End Sub

    Structure ShopArgs
        Dim Source As Access
        Dim Destine As Access
        Dim SourceLabel As String
        Dim DestineLabel As String
        Dim Connected As Boolean
        Dim Message As String
    End Structure

    Private Function GetShopState(ByVal row As DataGridViewRow) As ShopArgs
        Dim result As ShopArgs
        result.SourceLabel = Strings.RTrim(row.Cells("來源").Value)
        result.DestineLabel = Strings.RTrim(row.Cells("目地").Value)

        result.Source = Client(result.SourceLabel)
        result.Destine = Client(result.DestineLabel)

        If result.Source Is Nothing Then
            result.Message = result.SourceLabel & "未設定連線"
            result.Connected = False
        ElseIf result.Destine Is Nothing Then
            result.Message = result.DestineLabel & "未設定連線"
            result.Connected = False
        ElseIf Not result.Source.Connected Then
            result.Message = result.SourceLabel & "尚未連線"
            result.Connected = False
        ElseIf Not result.Destine.Connected Then
            result.Message = result.DestineLabel & "尚未連線"
            result.Connected = False
        Else
            result.Message = "連線狀態ok"
            result.Connected = True
        End If
        Return result
    End Function





    Private Sub 欄位顯示ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 欄位顯示ToolStripMenuItem.Click
        If DataGridViewVisibleDialog.ShowDialog(dgList) Then Code.SaveXml(DataGridViewVisibleDialog.GetVisibleColumns(dgList), StockMoveVisiblePath)
    End Sub







End Class