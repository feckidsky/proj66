Imports 進銷存.Database.StructureBase

Public Class winMain
    WithEvents access As Database.Access '= Program.DB


    Private Sub winMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UpdateTitle()
        UpdateSalesList()

    End Sub

    Dim UpdateTitleHandler As New Action(AddressOf UpdateTitle)
    Private Sub UpdateTitle()
        If Me.InvokeRequired Then
            Me.Invoke(UpdateTitleHandler)
            Exit Sub
        End If
        Dim connectState As String = IIf(access.GetType Is GetType(Database.AccessClient) And Not access.Connected, "斷線", "已連線")
        Me.Text = SystemTitle & " - " & access.Name & "(" & connectState & ") - " & CurrentUser.Name
    End Sub

    Dim Filter As DataGridViewFilter
    Public Sub New()

        InitialProgram()

        'If Config.Mode = Connect.Server Then
        '    access = Program.myDatabase
        '    'access = Client.Client(0)
        'Else
        '    If Client.Client.Count = 0 Then
        '        access = New Database.AccessClient()
        '    Else
        '        access = Client.Client(0)
        '    End If
        'End If

        ' 此為 Windows Form 設計工具所需的呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。
        AddHandler Program.Account_Logout, AddressOf Account_Login
        AddHandler Program.Account_LogIn, AddressOf Account_Login


        LogIn("kidsky", "3883", False)

        Filter = New DataGridViewFilter(dgSales)
        Filter.AddTextFilter("單號", "銷售人員", "客戶", "備註", "付款方式", "內容")
        Filter.AddNumberFilter("訂金", "金額", "利潤")
        dgSales.DefaultCellStyle.WrapMode = DataGridViewTriState.True

        cbClient.Items.Clear()
        cbClient.Items.AddRange(Client.GetNameList())
        If cbClient.Items.Count > 0 Then cbClient.SelectedIndex = 0
        cbForm.SelectedIndex = 2
    End Sub

    Private Sub cbForm_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbForm.SelectedIndexChanged
        FormIndex = cbForm.SelectedIndex
        If Me.Created Then UpdateSalesList()
    End Sub

    Private Sub cbClient_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbClient.SelectedIndexChanged
        access = Client(cbClient.Text)
        If Me.Created Then UpdateSalesList()
        UpdateTitle()
    End Sub


    Dim StartTime As Date
    Dim EndTime As Date
    Dim FormIndex As Integer

    Public Sub UpdateSalesList()
        If Not Me.Created Or access Is Nothing Then Exit Sub
        If access.GetType Is GetType(Database.AccessClient) AndAlso Not access.Connected Then dgSales.Rows.Clear()

        If rToday.Checked Then
            StartTime = Today.Date
            EndTime = Today.Date.AddDays(1)
        ElseIf r30Day.Checked Then
            StartTime = Today.Date.AddDays(-30)
            EndTime = Today.Date.AddDays(1)
        Else
            StartTime = dtpStart.Value.Date
            EndTime = dtpEnd.Value.Date.AddDays(1)
        End If

        'Dim dt As Data.DataTable = DB.GetSalesList(StartTime, EndTime, Me.cbForm.SelectedIndex)
        Dim dt As Data.DataTable = access.GetSalesListWithContract(StartTime, EndTime, Me.cbForm.SelectedIndex)


        dgSales.Columns.Clear()
        For i As Integer = 0 To dt.Columns.Count - 1
            dgSales.Columns.Add(dt.Columns(i).ColumnName, dt.Columns(i).ColumnName)
        Next
        dgSales.Columns.Add("內容", "內容")

        If Not IO.File.Exists(SalesVisiblePath) Then
            dgSales.Columns("單號").Visible = False
            Code.Save(DataGridViewVisibleDialog.GetVisibleColumns(dgSales), SalesVisiblePath)
        Else
            DataGridViewVisibleDialog.SetVisible(dgSales, Code.Load(Of String())(SalesVisiblePath, New String() {}))
        End If

        If Filter IsNot Nothing Then Filter.ClearComboBoxItem()
        For i As Integer = 0 To dt.Rows.Count - 1
            Dim arr As String() = Array.ConvertAll(dt.Rows(i).ItemArray, Function(o As Object) o.ToString)
            BeginAddRowInfo(arr)
            'Dim idx As Integer = dgSales.Rows.Add(arr)
            'Dim tip As String = access.GetSalesTip(dgSales.Rows(idx).Cells("單號").Value, dgSales.Rows(idx).Cells("付款方式").Value)
            'dgSales.Rows(idx).Cells("內容").Value = tip
            'dgSales.Rows.Item(i).Cells("銷貨時間").Value = IIf(dgSales.Rows(i).Cells("銷貨時間").Value = New Date(2001, 1, 1, 0, 0, 0), "", dgSales.Rows(i).Cells("銷貨時間").Value)
            'dgSales.Rows.Item(i).Cells("付款方式").Value = TypeOfPaymentsDescribe(dgSales.Rows(i).Cells("付款方式").Value)
        Next
        'UpdateListColor()
        'If Filter IsNot Nothing Then Filter.UpdateComboBox()
    End Sub

    Public Sub UpdateListColor()
        For i As Integer = 0 To dgSales.Rows.Count - 1
            UpdateRowColor(dgSales.Rows(i))
            'dgSales.Rows(i).DefaultCellStyle.BackColor = IIf(dgSales.Rows(i).Cells("付款方式").Value = "訂金", ToColor(Config.OrderBackcolor), ToColor(Config.SalesBackColor))
        Next
    End Sub

    Public Sub UpdateRowColor(ByVal row As DataGridViewRow)
        row.DefaultCellStyle.BackColor = IIf(row.Cells("付款方式").Value = "訂金", ToColor(Config.OrderBackcolor), ToColor(Config.SalesBackColor))
    End Sub


    Public Sub OpenSales()
        If dgSales.SelectedRows.Count <= 0 Then
            MsgBox("您至少必須選擇一個項目!")
            Exit Sub
        End If


        Dim row As DataGridViewRow = dgSales.SelectedRows(0)

        Dim SalesLabel As String = row.Cells("單號").Value
        winSales.Open(SalesLabel, access)

    End Sub




    Private Sub dgSales_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgSales.CellMouseDoubleClick

        OpenSales()

    End Sub



    Private Sub 進貨記錄ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 進貨記錄ToolStripMenuItem1.Click
        winStockInLog.Show(access)
    End Sub

    Private Sub 查詢庫存ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 查詢庫存ToolStripMenuItem.Click
        winStockList.Show(access)
    End Sub

    Private Sub rToday_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rToday.CheckedChanged, r30Day.CheckedChanged, rUserTime.CheckedChanged
        If Me.Created Then UpdateSalesList()
    End Sub

    Private Sub dtpStart_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpStart.ValueChanged, dtpEnd.ValueChanged
        If Me.Created Then UpdateSalesList()
    End Sub

    Private Sub 銷貨AToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 銷貨AToolStripMenuItem.Click, 銷貨ToolStripMenuItem.Click
        winSales.Create(access)
    End Sub

    Private Sub 修改CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 修改CToolStripMenuItem.Click
        OpenSales()
    End Sub


    Private Sub 刪除DToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 刪除DToolStripMenuItem.Click
        If dgSales.SelectedRows.Count <= 0 Then
            MsgBox("您至少必須選擇一個項目!")
            Exit Sub
        End If

        If MsgBox("刪除該筆銷貨單，您確定要這麼做？", MsgBoxStyle.OkCancel + MsgBoxStyle.Exclamation) = MsgBoxResult.Cancel Then
            Exit Sub
        End If

        Dim sales As New Sales
        sales.Label = dgSales.SelectedRows(0).Cells(0).Value
        access.DeleteSales(sales)
    End Sub

    Private Sub 商品項目GToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 商品項目GToolStripMenuItem.Click
        winGoodsList.Show(access)
    End Sub

    Private Sub 供應商ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 供應商ToolStripMenuItem.Click
        winSupplierList.Show(access)
    End Sub

    Private Sub 員工PToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 員工PToolStripMenuItem.Click
        winPersonnelList.Show(access)
    End Sub

    Private Sub 客戶CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 客戶CToolStripMenuItem.Click
        winCustomerList.Show(access)
    End Sub


    Private RealClose As Boolean = False
    Private Sub 關閉CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 關閉CToolStripMenuItem.Click, 關閉QToolStripMenuItem.Click
        RealClose = True
        Me.Close()
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.Visible = True
    End Sub

    Private Sub winMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = CloseReason.UserClosing And Not RealClose Then
            Me.Visible = False
            e.Cancel = True
        Else

            Program.FinishProgram()
            'Environment.Exit(Environment.ExitCode)
            'Application.Exit()
        End If

    End Sub


    Private Sub 開啟OToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 開啟OToolStripMenuItem.Click
        Me.Visible = True
    End Sub


    Private Sub 縮到工具列TToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 縮到工具列TToolStripMenuItem.Click
        Me.Visible = False
    End Sub

    Private Sub 登出OToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 登出OToolStripMenuItem.Click, 登出OToolStripMenuItem1.Click
        LogOut()
    End Sub


    Private Sub 登入IToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 登入IToolStripMenuItem.Click, 登入IToolStripMenuItem1.Click
        winLogIn.ShowDialog()
    End Sub


    Private Sub Account_Login(ByVal per As Database.Personnel, ByVal message As String)
        Me.Text = SystemTitle & " - " & per.Name
        MsgBox(message, MsgBoxStyle.Information, "帳號")
    End Sub


    Private Sub 修改密碼PToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 修改密碼PToolStripMenuItem.Click
        winChangePassword.ShowDialog(access)
    End Sub

    Private Sub 選項OToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 選項OToolStripMenuItem.Click
        winOptional.ShowDialog()
        UpdateListColor()
    End Sub

    Private Sub 合約CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 合約CToolStripMenuItem.Click
        winContractList.Show(access)
    End Sub

    Private Sub 結算ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 結算ToolStripMenuItem.Click
        winInformation.ShowDialog(access)
    End Sub

    Private Sub 列印PToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 列印PToolStripMenuItem.Click
        DataGridViewPrintDialog.ShowDialog(cbForm.Text, dgSales)
    End Sub

    Private Sub 欄位顯示VToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 欄位顯示VToolStripMenuItem.Click
        If DataGridViewVisibleDialog.ShowDialog(dgSales) Then Code.Save(DataGridViewVisibleDialog.GetVisibleColumns(dgSales), SalesVisiblePath)

    End Sub

    Delegate Sub DelegateObjArr(ByVal arr As Object())

    Dim AddRowHandler As New DelegateObjArr(AddressOf AddRow)
    Dim UpdateRowHandler As New DelegateObjArr(AddressOf UpdateRow)
    Dim DeleteRowHandler As New Action(Of Database.Sales)(AddressOf DeleteSales)

    Private Sub AddRow(ByVal arr As Object())
        Dim idx As Integer = dgSales.Rows.Add(arr)
        UpdateRowColor(dgSales.Rows(idx))
        Filter.FilterRow(dgSales.Rows(idx))
        Filter.AddComboBoxItem(dgSales.Rows(idx))
    End Sub

    Private Sub UpdateRow(ByVal arr As Object())

        For Each row As DataGridViewRow In dgSales.Rows
            If row.Cells(0).Value = arr(0) Then
                For i As Integer = 1 To arr.Length - 1
                    row.Cells(i).Value = arr(i)
                Next
                UpdateRowColor(row)
                Filter.FilterRow(row)
                Filter.AddComboBoxItem(row)
                Exit Sub
            End If
        Next
    End Sub

    Private Sub DeleteSales(ByVal sales As Sales)
        Dim delRows As New List(Of DataGridViewRow)
        For Each row As DataGridViewRow In dgSales.Rows
            If Strings.RTrim(row.Cells("單號").Value) = Strings.RTrim(sales.Label) Then delRows.Add(row)
        Next

        For Each row As DataGridViewRow In delRows
            dgSales.Rows.Remove(row)
        Next
    End Sub

    Private Sub BeginAddRowInfo(ByVal arr As Object())
        Dim thread As New Threading.Thread(New Threading.ParameterizedThreadStart(AddressOf ShowRowInfo))
        thread.Start(New Object() {arr, AddRowHandler})
    End Sub

    Private Sub ShowRowInfo(ByVal args As Object())
        Dim arr() As Object = args(0)
        Dim handler As DelegateObjArr = args(1)
        ShowRowInfo(arr, handler)
    End Sub

    Private Sub ShowRowInfo(ByVal arr As Object(), ByVal Handler As DelegateObjArr)
        Dim idxPayType As Integer = 5
        Dim idxLabel As Integer = 0

        '取得商品資訊
        Dim tip As String = access.GetSalesTip(arr(idxLabel), arr(idxPayType))
        If arr(idxPayType) = TypeOfPayment.Commission Then arr(2) = ""

        '將付款方式由數字改為中文描述
        arr(idxPayType) = TypeOfPaymentsDescribe(arr(idxPayType))

        Dim lst As New List(Of String)(arr)
        lst.Add(tip)

        Dim args As Object = lst.ToArray()
        '新增/修改顯示清單
        If Me.InvokeRequired Then
            Me.Invoke(Handler, args)
        Else
            Handler.Invoke(args)
        End If

    End Sub

    Private Sub access_ConnectedFail(ByVal Client As TCPTool.Client) Handles access.ConnectedFail
        UpdateTitle()
    End Sub

    Dim UpdateSalesListHandler As New Action(AddressOf UpdateSalesList)
    Private Sub access_ConnectedSuccess(ByVal Client As TCPTool.Client) Handles access.ConnectedSuccess
        If Me.InvokeRequired Then
            Me.Invoke(UpdateTitleHandler)
            Me.Invoke(UpdateSalesListHandler)
            Exit Sub
        End If
        UpdateTitleHandler()
        UpdateSalesListHandler()
    End Sub

    Private Sub access_CreatedSales(ByVal sender As Object, ByVal sales As Database.StructureBase.Sales, ByVal GoodsList() As Database.StructureBase.SalesGoods, ByVal OrderList() As Database.OrderGoods, ByVal SalesContracts() As SalesContract) Handles access.CreatedSales
        Dim dt As DataTable = access.GetSalesListWithContract(StartTime, EndTime, Me.cbForm.SelectedIndex, sales.Label)
        Dim arr As String() = (Array.ConvertAll(dt.Rows(0).ItemArray, Function(o As Object) o.ToString))
        ShowRowInfo(arr, AddRowHandler)
    End Sub


    Private Sub access_ChangedSales(ByVal sender As Object, ByVal sales As Database.StructureBase.Sales, ByVal GoodsList() As Database.StructureBase.SalesGoods, ByVal OrderList() As Database.OrderGoods, ByVal SalesContracts() As SalesContract) Handles access.ChangedSales
        Dim dt As DataTable = access.GetSalesListWithContract(StartTime, EndTime, FormIndex, sales.Label)
        Dim arr As String() = (Array.ConvertAll(dt.Rows(0).ItemArray, Function(o As Object) o.ToString))
        ShowRowInfo(arr, UpdateRowHandler)
    End Sub

    Private Sub access_DeletedSales(ByVal sender As Object, ByVal sales As Database.StructureBase.Sales) Handles access.DeletedSales
        'UpdateSalesList()
        If Me.InvokeRequired Then
            Me.Invoke(DeleteRowHandler, sales)
        Else
            DeleteRowHandler.Invoke(sales)
        End If
    End Sub




End Class