Imports 進銷存.Database.DatabaseType
Imports 進銷存.Database

Public Class winMain
    WithEvents m_access As Database.Access '= Program.DB

    Property access() As Database.Access
        Get
            Return m_access
        End Get
        Set(ByVal value As Database.Access)
            Dim Changed As Boolean = value IsNot m_access
            m_access = value
            If Changed Then
                If Me.Created Then UpdateSalesList()
                'UpdateLogList()
                UpdateTitle()
            End If
        End Set
    End Property

    Private Sub winMain_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        備份BToolStripMenuItem.Visible = Config.Mode = Connect.Server
    End Sub


    Private Sub winMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UpdateTitle()
        UpdateSalesList()
        'UpdateLogList()
    End Sub

    Dim UpdateTitleHandler As New Action(AddressOf UpdateTitle)
    Private Sub UpdateTitle()
        If Not Me.Created Then Exit Sub
        If Me.InvokeRequired Then
            If Not Me.IsDisposed Then Me.Invoke(UpdateTitleHandler)
            Exit Sub
        End If
        If access IsNot Nothing Then
            Dim connectState As String = IIf(access.GetType Is GetType(Database.AccessClient) And Not access.Connected, "斷線", "已連線")
            Me.Text = SystemTitle & " - " & ProgramVersion & " - " & access.Name & "(" & connectState & ") - " & CurrentUser.Name
        End If
        Button1.Visible = CurrentUser.ID = "Designer"
        偵錯工具ToolStripMenuItem.Visible = CurrentUser.ID = "Designer"
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
        dgSales.AutoGenerateColumns = False

        Filter = New DataGridViewFilter(dgSales)
        Filter.AddTextFilter("單號", "銷售人員", "客戶", "備註", "付款方式", "內容")
        Filter.AddNumberFilter("訂金", "金額", "利潤")
        dgSales.DefaultCellStyle.WrapMode = DataGridViewTriState.True

        cbClient.Items.Clear()
        cbClient.Items.AddRange(ClientManager.GetNameList())
        cbClient_UpdateState()
        'If cbClient.Items.Count > 0 Then cbClient.SelectedIndex = 0
        cbForm.SelectedIndex = 2

        '自動登入
        If LoginSetting.AutoLog Then
            Dim db As Access = ClientManager(LoginSetting.Shop)
            If db IsNot Nothing Then
                Me.access = db
                db.LogIn(LoginSetting.ID, LoginSetting.Password, True)
                cbClient.SelectedIndex = Array.FindIndex(Of Access)(ClientManager.Client, Function(a As Access) db.Name = a.Name)
            End If
        Else '沒有預設登入的資料庫，選擇第一個Client當預設值
            If cbClient.Items.Count > 0 Then cbClient.SelectedIndex = 0
        End If
    End Sub

    Private Sub cbForm_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbForm.SelectedIndexChanged
        FormIndex = cbForm.SelectedIndex
        If Me.Created Then UpdateSalesList()
    End Sub

    Private Sub cbClient_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbClient.DropDown
        cbClient_UpdateState()
    End Sub

    Private Sub cbClient_UpdateState()
        For i As Integer = 0 To cbClient.Items.Count - 1
            Dim client As Access = ClientManager(i)
            cbClient.Items(i) = client.Name & " - " & IIf(client.Connected, "已連線", "斷線")
        Next
    End Sub


    Private Sub cbClient_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbClient.SelectedIndexChanged
        'access = ClientManager(cbClient.Text)

        access = ClientManager(cbClient.SelectedIndex)
    End Sub


    Dim StartTime As Date
    Dim EndTime As Date
    Dim FormIndex As Integer




    Public Sub OpenSales()
        If dgSales.SelectedRows.Count <= 0 Then
            MsgBox("您至少必須選擇一個項目!")
            Exit Sub
        End If


        Dim row As DataGridViewRow = dgSales.SelectedRows(0)

        Dim SalesLabel As String = row.Cells("單號").Value

        'Dim win As New winSales
        'win.Open(SalesLabel, m_access)
        winSales.Open(SalesLabel, access)

    End Sub




    Private Sub dgSales_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgSales.CellMouseDoubleClick
        OpenSales()
    End Sub

#Region "功能表"
    Private Sub 開啟OToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 開啟OToolStripMenuItem.Click
        Me.Visible = True
    End Sub


    Private Sub 縮到工具列TToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 縮到工具列TToolStripMenuItem.Click
        Me.Visible = False
    End Sub

    Private Sub 登出OToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 登出OToolStripMenuItem.Click, 登出OToolStripMenuItem1.Click
        If access IsNot Nothing Then access.LogOut()
    End Sub


    Private Sub 登入IToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 登入IToolStripMenuItem.Click, 登入IToolStripMenuItem1.Click
        Dim result As LoginResult = winLogIn.ShowDialog(access)
        If result.State = LoginState.Success Then access = result.Client
    End Sub


    'Private Sub Account_Login(ByVal per As Database.Personnel, ByVal message As String)
    '    Me.Text = SystemTitle & " - " & per.Name
    '    MsgBox(message, MsgBoxStyle.Information, "帳號")
    'End Sub


    Private Sub 修改密碼PToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 修改密碼PToolStripMenuItem.Click
        winChangePassword.ShowDialog(access)
    End Sub

    Private Sub 選項OToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 選項OToolStripMenuItem.Click
        winOptional.ShowDialog()
        UpdateListColor()
        UpdateCbClientList()
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
        If DataGridViewVisibleDialog.ShowDialog(dgSales) Then Code.SaveXml(DataGridViewVisibleDialog.GetVisibleColumns(dgSales), SalesVisiblePath)

    End Sub

    Private Sub 進貨記錄ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 進貨記錄ToolStripMenuItem1.Click
        winStockInLog.Show(access)
    End Sub

    Private Sub 調貨ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 調貨ToolStripMenuItem.Click
        winStockMoveList.Show(access)
    End Sub

    Private Sub 查詢庫存ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 查詢庫存ToolStripMenuItem.Click
        winStockList.Show(access)
    End Sub

    Private Sub rToday_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rToday.CheckedChanged, r30Day.CheckedChanged, rUserTime.CheckedChanged
        If Me.Created And CType(sender, RadioButton).Checked = True Then
            UpdateSalesList()
            'UpdateLogList()
        End If

    End Sub

    Private Sub dtpStart_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpStart.ValueChanged, dtpEnd.ValueChanged
        If Me.Created Then UpdateSalesList()
        'UpdateLogList()
    End Sub

    Private Sub 銷貨AToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 銷貨AToolStripMenuItem.Click, 銷貨ToolStripMenuItem.Click
        Dim win As New winSales
        win.Create(access)
        'winSales.Create(access)
    End Sub

    Private Sub 修改CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 修改CToolStripMenuItem.Click
        OpenSales()
    End Sub


    Private Sub 刪除DToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 刪除DToolStripMenuItem.Click
        If dgSales.SelectedRows.Count <= 0 Then
            MsgBox("您至少必須選擇一個項目!")
            Exit Sub
        End If

        'If MsgBox("刪除該筆銷貨單，您確定要這麼做？", MsgBoxStyle.OkCancel + MsgBoxStyle.Exclamation) = MsgBoxResult.Cancel Then
        '    Exit Sub
        'End If

        Dim sales As New Sales
        sales.Label = dgSales.SelectedRows(0).Cells(0).Value
        sales.OrderDate = dgSales.SelectedRows(0).Cells("訂單時間").Value
        If sales.OrderDate.Date = Today OrElse CheckAuthority(3) Then
            If winLogIn.ShowDialog(access, "輸入密碼以刪除此資料", CurrentUser.ID).State = LoginState.Success Then
                access.DeleteSales(sales)
            End If
        End If
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
#End Region


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

#Region "銷貨/訂單顯示更新"
    Public Sub UpdateSalesList()
        If Not Me.Created Or access Is Nothing Then Exit Sub
        If access.GetType Is GetType(Database.AccessClient) AndAlso Not access.Connected Then dgSales.Rows.Clear()

        If Me.InvokeRequired Then
            If Not Me.IsDisposed Then Me.Invoke(New Action(AddressOf UpdateSalesList))
            Exit Sub
        End If

        If rToday.Checked Then
            StartTime = Today.Date
            EndTime = Today.Date.AddDays(1)
        ElseIf r30Day.Checked Then
            StartTime = Today.Date.AddDays(-2)
            EndTime = Today.Date.AddDays(1)
        Else
            StartTime = dtpStart.Value.Date
            EndTime = dtpEnd.Value.Date.AddDays(1)
        End If


        Dim winProgress As New ProgressDialog
        winProgress.AutoClose = False
        Dim Progress As New Access.Progress(AddressOf winProgress.UpdateProgress, "讀取訂單/銷貨單", Nothing)
        Dim thread As New Threading.Thread(New Threading.ParameterizedThreadStart(AddressOf UpdateSalesList))
        Dim args As UpdateSalesListArgs
        args.StartTime = StartTime
        args.EndTime = EndTime
        args.ListType = Me.cbForm.SelectedIndex
        args.progress = Progress
        args.Dialog = winProgress
        winProgress.Thread = thread
        winProgress.Show()
        thread.Start(args)
    End Sub

    Structure UpdateSalesListArgs
        Dim StartTime As Date
        Dim EndTime As Date
        Dim ListType As Access.GetSalesListType
        Dim progress As Access.Progress
        Dim Dialog As ProgressDialog
    End Structure

    Public Sub UpdateColumn(ByVal dt As DataTable)
        If dgSales.Rows.Count = 0 Then
            dgSales.Columns.Clear()
            For i As Integer = 0 To dt.Columns.Count - 1
                dgSales.Columns.Add(dt.Columns(i).ColumnName, dt.Columns(i).ColumnName)
            Next
            dgSales.Columns.Add("內容", "內容")
            dgSales.Columns("內容").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End If

        dgSales.Rows.Clear()
        If dt Is Nothing Then
            txtDataCount.Text = "查詢資料時發生錯誤!" ' dt.Rows.Count
        Else
            txtDataCount.Text = "資料筆數:" & dt.Rows.Count
        End If

        If Not IO.File.Exists(SalesVisiblePath) Then
            If dgSales.Columns("單號") Is Nothing Then Exit Sub
            dgSales.Columns("單號").Visible = False
            Code.SaveXml(DataGridViewVisibleDialog.GetVisibleColumns(dgSales), SalesVisiblePath)
        Else
            DataGridViewVisibleDialog.SetVisible(dgSales, Code.LoadXml(Of String())(SalesVisiblePath, New String() {}))
        End If

        If Filter IsNot Nothing Then Filter.ClearComboBoxItem()
    End Sub

    Dim UpdateColumnHandler As New Action(Of DataTable)(AddressOf UpdateColumn)
    Public Sub UpdateSalesList(ByVal args As UpdateSalesListArgs)
        args.progress.Reset("讀取訂單/銷貨單資料", 0, 50)
        ' Dim dt As Data.DataTable = access.GetSalesListWithContract(args.StartTime, args.EndTime, args.ListType, , , args.progress)
        Dim dt As Data.DataTable = access.GetSalesListInfo(args.StartTime, args.EndTime, args.ListType, , True, args.progress)
        If dt Is Nothing Then
            args.Dialog.Close()
            Exit Sub
        End If

        Try
            If Not Me.IsDisposed Then Me.Invoke(UpdateColumnHandler, dt)
        Catch

        End Try

        If dt.Rows.Count = 0 Then
            args.Dialog.Close()
            Exit Sub
        End If

        If Not Me.IsDisposed Then Me.Invoke(New Action(AddressOf StopUpdate))
        args.progress.Reset("取得訂單/銷貨單內容", 50, 99)
        For i As Integer = 0 To dt.Rows.Count - 1
            args.progress.Report((i + 1) / dt.Rows.Count * 100)
            Dim arr As String() = Array.ConvertAll(dt.Rows(i).ItemArray, Function(o As Object) o.ToString)
            'BeginAddRowInfo(arr)
            ShowRowInfo(arr, AddRowHandler)
        Next
        If Not Me.IsDisposed Then Me.Invoke(New Action(AddressOf ResetUpdate))
        UpdateLogList()
        args.Dialog.Close()
    End Sub

    Public Sub StopUpdate()
        dgSales.SuspendLayout()
        'dgSales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
        'dgSales.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None
        'dgSales.Visible = False

    End Sub

    Public Sub ResetUpdate()
        'dgSales.Visible = True
        'dgSales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
        'dgSales.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
        dgSales.ResumeLayout(False)
    End Sub

    Public Sub UpdateListColor()
        For i As Integer = 0 To dgSales.Rows.Count - 1
            UpdateRowColor(dgSales.Rows(i))
            'dgSales.Rows(i).DefaultCellStyle.BackColor = IIf(dgSales.Rows(i).Cells("付款方式").Value = "訂金", ToColor(Config.OrderBackcolor), ToColor(Config.SalesBackColor))
        Next
    End Sub

    Public Sub UpdateRowColor(ByVal row As DataGridViewRow)
        If row.Cells("付款方式").Value = "退訂" Then
            row.DefaultCellStyle.BackColor = Color.Yellow
            'row.InheritedStyle.BackColor = Color.Yellow
        Else
            row.DefaultCellStyle.BackColor = IIf(row.Cells("付款方式").Value = Database.Sales.PaymentDescribe(Database.Payment.Deposit), ToColor(Config.OrderBackcolor), ToColor(Config.SalesBackColor))
            'row.InheritedStyle.BackColor = IIf(row.Cells("付款方式").Value = "訂金", ToColor(Config.OrderBackcolor), ToColor(Config.SalesBackColor))
        End If

    End Sub

    Delegate Sub DelegateObjArr(ByVal arr As Object())

    Dim AddRowHandler As New DelegateObjArr(AddressOf AddRow)
    Dim UpdateRowHandler As New DelegateObjArr(AddressOf UpdateRow)
    Dim DeleteRowHandler As New Action(Of Database.Sales)(AddressOf DeleteSales)

    Private Sub AddRow(ByVal arr As Object())
        Me.SuspendLayout()
        Dim idx As Integer

        Try
            idx = dgSales.Rows.Add(arr)
        Catch
            Exit Sub
        End Try

        UpdateRowColor(dgSales.Rows(idx))
        dgSales.Sort(dgSales.Columns(0), System.ComponentModel.ListSortDirection.Descending)
        Filter.FilterRow(dgSales.Rows(idx))
        Filter.AddComboBoxItem(dgSales.Rows(idx))
        Me.ResumeLayout(False)
        Me.PerformLayout()
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
        If arr(idxPayType) = Payment.Deposit Then arr(2) = ""

        '將付款方式由數字改為中文描述
        arr(idxPayType) = Sales.PaymentDescribe(arr(idxPayType))

        Dim lst As New List(Of String)(arr)
        lst.Add(tip)

        Dim args As Object = lst.ToArray()
        '新增/修改顯示清單
        If Me.InvokeRequired Then
            If Not Me.IsDisposed Then Me.Invoke(Handler, args)
        Else
            Handler.Invoke(args)
        End If

    End Sub
#End Region
#Region "訂單/銷貨單 動作"


    Private Sub access_CreatedSales(ByVal sender As Object, ByVal sales As Database.Sales, ByVal GoodsList() As Database.SalesGoods, ByVal OrderList() As Database.OrderGoods, ByVal ReturnList() As Database.ReturnGoods, ByVal SalesContracts() As SalesContract) Handles m_access.CreatedSales
        Dim dt As DataTable = access.GetSalesListInfo(StartTime, EndTime, FormIndex, sales.Label, True)
        If dt.Rows.Count = 0 Then Exit Sub
        Dim arr As String() = (Array.ConvertAll(dt.Rows(0).ItemArray, Function(o As Object) o.ToString))
        ShowRowInfo(arr, AddRowHandler)
    End Sub


    Private Sub access_ChangedSales(ByVal sender As Object, ByVal sales As Database.Sales, ByVal GoodsList() As Database.SalesGoods, ByVal OrderList() As Database.OrderGoods, ByVal ReturnList() As Database.ReturnGoods, ByVal SalesContracts() As SalesContract) Handles m_access.ChangedSales
        Dim dt As DataTable = access.GetSalesListInfo(StartTime, EndTime, FormIndex, sales.Label, True)
        If dt Is Nothing OrElse dt.Rows.Count = 0 Then Exit Sub
        Dim arr As String() = (Array.ConvertAll(dt.Rows(0).ItemArray, Function(o As Object) o.ToString))
        ShowRowInfo(arr, UpdateRowHandler)
    End Sub

    Private Sub access_DeletedSales(ByVal sender As Object, ByVal sales As Database.Sales) Handles m_access.DeletedSales
        'UpdateSalesList()
        If Me.InvokeRequired Then
            If Not Me.IsDisposed Then Me.Invoke(DeleteRowHandler, sales)
        Else
            DeleteRowHandler.Invoke(sales)
        End If
    End Sub
#End Region

    Private Sub access_Account_LogIn(ByVal sender As Object, ByVal result As Database.LoginResult) Handles m_access.Account_LogIn, m_access.Account_Logout

        If result.State <> Database.LoginState.Success Then
            MsgBox(result.msg, MsgBoxStyle.Information)
        Else

        End If

        dgLog.ContextMenuStrip = IIf(result.User.Authority >= 3, cmsLog, Nothing)

        If Not Me.Created Then Exit Sub
        UpdateTitle()
    End Sub

    Private Sub access_ConnectedFail(ByVal Client As TCPTool.Client) Handles m_access.ConnectedFail
        UpdateTitle()
    End Sub

    Dim UpdateSalesListHandler As New Action(AddressOf UpdateSalesList)
    Private Sub access_ConnectedSuccess(ByVal Client As TCPTool.Client) Handles m_access.ConnectedSuccess
        If Me.InvokeRequired Then
            If Not Me.IsDisposed Then Me.Invoke(UpdateTitleHandler)
            If Not Me.IsDisposed Then Me.Invoke(UpdateSalesListHandler)
            Exit Sub
        End If
        If LoginSetting.AutoLog And m_access.User.IsGuest() Then access.LogIn(LoginSetting.ID, LoginSetting.Password)

        UpdateTitleHandler()
        UpdateSalesListHandler()
        'UpdateLogList()
    End Sub

    Dim dtLog As DataTable
    Dim UpdateLogListHandler As New Action(AddressOf UpdateLogList)
    Private Sub UpdateLogList()
        If Not Me.Created Or access Is Nothing Then Exit Sub

        If Me.InvokeRequired Then
            If Not Me.IsDisposed Then Me.Invoke(UpdateLogListHandler)
            Exit Sub
        End If


        dtLog = access.GetLogList(StartTime, EndTime, "")

        dgLog.DataSource = dtLog

        If dtLog Is Nothing OrElse dgLog.Columns.Count = 0 Then Exit Sub

        dgLog.Columns("員工編號").Visible = False
        dgLog.Columns("內容").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        dgLog.Sort(dgLog.Columns(0), System.ComponentModel.ListSortDirection.Descending)
    End Sub

    Dim CreateLogHandler As New Action(Of Object())(AddressOf CreateLogRow)

    Private Sub CreateLogRow(ByVal arr As Object)

        If dtLog Is Nothing OrElse dgLog.Columns.Count = 0 Then Exit Sub
        For Each row As DataRow In dtLog.Rows
            Try
                If row(0) = arr(0) And row(1) = arr(1) Then
                    Exit Sub
                End If
            Catch
                Exit Sub
            End Try
        Next

        Try
            dtLog.Rows.Add(CType(arr, Object()))
            dgLog.Sort(dgLog.Columns(0), System.ComponentModel.ListSortDirection.Descending)
        Catch

        End Try
    End Sub


    Dim DeleteLogHandler As New Action(Of Log)(AddressOf DeleteLogRow)
    Private Sub DeleteLogRow(ByVal log As Log)
        If dtLog Is Nothing OrElse dgLog.Columns.Count = 0 Then Exit Sub
        For Each row As DataRow In dtLog.Rows
            Try
                If row(0) = log.Date And row(1) = log.Personnel Then
                    dtLog.Rows.Remove(row)
                    Exit Sub
                End If
            Catch
            End Try
        Next
    End Sub

    Private Sub m_access_ChangedLog(ByVal sender As Object, ByVal log As Database.Log) Handles m_access.ChangedLog
        'UpdateLogList()
        'Dim row As Object = CType(sender, Access).GetLogRow(log.Date, log.Personnel).ItemArray()
    End Sub

    Private Sub m_access_CreatedLog(ByVal sender As Object, ByVal log As Database.Log) Handles m_access.CreatedLog

        Dim row As DataRow = CType(sender, Access).GetLogRow(log.Date, log.Personnel)

        If row Is Nothing Then Exit Sub
        Dim arr As Object = row.ItemArray()

        If arr(0) < StartTime Or EndTime < arr(0) Then Exit Sub

        If Me.InvokeRequired Then
            If Not Me.IsDisposed Then Me.Invoke(CreateLogHandler, arr)
        Else
            CreateLogHandler(arr)
        End If

    End Sub

    Private Sub m_access_DeletedLog(ByVal sender As Object, ByVal log As Database.Log) Handles m_access.DeletedLog
        'Dim row As Object = CType(sender, Access).GetLogRow(log.Date, log.Personnel).ItemArray()
        If Me.InvokeRequired Then
            If Not Me.IsDisposed Then Me.Invoke(DeleteLogHandler, log)
        Else
            DeleteLogHandler(log)
        End If

    End Sub

    Dim DeleteAllLogHandler As New Action(Of Object)(AddressOf m_access_DeletedAllLog)
    Private Sub m_access_DeletedAllLog(ByVal sender As Object) Handles m_access.DeletedAllLog
        If Me.InvokeRequired Then
            If Not Me.IsDisposed Then Me.Invoke(DeleteAllLogHandler, sender)
            Exit Sub
        End If
        'dgLog.CurrentCell = Nothing
        If dtLog Is Nothing OrElse dgLog.Columns.Count = 0 Then Exit Sub
        dtLog.Rows.Clear()
    End Sub

    Private Sub 刪除ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 刪除ToolStripMenuItem.Click
        If dgLog.SelectedRows.Count = 0 Then
            MsgBox("您至少必須選擇一項記錄")
            Exit Sub
        End If

        If MsgBox("您確定要這些刪除記錄？", MsgBoxStyle.Question + MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then Exit Sub

        If winLogIn.ShowDialog(access, "密碼確認", CurrentUser.ID).State <> LoginState.Success Then Exit Sub

        Dim logs As New List(Of Log)

        For Each row As DataGridViewRow In dgLog.SelectedRows
            logs.Add(New Log(row.Cells("員工編號").Value, row.Cells("日期").Value, row.Cells("內容").Value))
        Next

        For Each Log As Log In logs
            access.DeleteLog(Log)
        Next

    End Sub

    Private Sub 全部刪除ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 全部刪除ToolStripMenuItem.Click
        If MsgBox("您確定要刪除全部記錄？", MsgBoxStyle.Question + MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then Exit Sub

        If winLogIn.ShowDialog(access, "密碼確認", CurrentUser.ID).State <> LoginState.Success Then Exit Sub

        access.DeleteAllLog()
    End Sub


    Private Sub 系統SToolStripMenuItem_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles 系統SToolStripMenuItem.DropDownItemClicked

    End Sub

    Private Sub 系統SToolStripMenuItem_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles 系統SToolStripMenuItem.DropDownOpening
        Me.登入IToolStripMenuItem.Visible = CurrentUser.IsGuest()
        Me.登出OToolStripMenuItem.Visible = Not CurrentUser.IsGuest()
        Me.修改密碼PToolStripMenuItem.Visible = Not CurrentUser.IsGuest()
    End Sub

    Private Sub 還原ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 還原ToolStripMenuItem.Click
        'Me.Visible = True
        Me.Show()
    End Sub

    Private Sub cmsSystem_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmsSystem.Opening
        還原ToolStripMenuItem.Visible = Not Me.Visible
        縮到工具列TToolStripMenuItem.Visible = Me.Visible
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Dim a() As Integer = New Integer() {}
        'a(0) = 100
        'Dim lst As New List(Of String)
        'For i = 0 To 10
        '    Dim s As System.Guid = System.Guid.NewGuid
        '    lst.Add(Join(Array.ConvertAll(s.ToByteArray, Function(b As Byte) Hex(b)), "") & vbTab & s.ToString)
        'Next

        'MsgBox(Join(lst.ToArray, vbCrLf))
        'MsgBox(Join(access.GetErrorLogFileNames(), vbCrLf))
        MsgBox(Join(Array.ConvertAll(TCPTool.GetIPv4s(), AddressOf GetString), vbCrLf))
    End Sub

    Public Function GetString(ByVal ip As Net.IPAddress) As String
        Return ip.ToString
    End Function


    Private Sub 銷貨清單OToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 銷貨清單OToolStripMenuItem.Click, 銷貨清單OToolStripMenuItem1.Click
        winSalesGoodsList.Show(access)
    End Sub

    Private Sub 銷售合約ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 銷售合約ToolStripMenuItem.Click, 銷售合約CToolStripMenuItem.Click
        winSalesContractList.Show(access)
    End Sub

    Private Sub 備份BToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 備份BToolStripMenuItem.Click
        BackupDialog.Show()
    End Sub

#Region "資料庫同步進度顯示"
    Public SyncMainReportHandler As New Access.Progress.ProgressAction(AddressOf SyncMainReport)
    Public SyncSubReportHandler As New Access.Progress.ProgressAction(AddressOf SyncSubReport)
    Public SyncStartSyncHandler As New Action(AddressOf SyncStart)
    Public SyncFinishSyncHandler As New Action(AddressOf SyncFinish)
    Public SyncThread As Threading.Thread

    Private Sub SyncStart()
        If Me.InvokeRequired Then
            If Not Me.IsDisposed Then Me.Invoke(SyncStartSyncHandler)
            Exit Sub
        End If
        lbSyncTitle.Visible = True
        lbSyncInfo.Visible = True
        progSync.Visible = True
        btSyncCancel.Visible = True
    End Sub

    Private Sub SyncFinish()
        If Me.InvokeRequired Then
            If Not Me.IsDisposed Then Me.Invoke(SyncFinishSyncHandler)
            Exit Sub
        End If
        lbSyncTitle.Visible = False
        lbSyncInfo.Visible = False
        progSync.Visible = False
        btSyncCancel.Visible = False
    End Sub

    Private Sub SyncMainReport(ByVal msg As String, ByVal percent As Integer)
        If Me.InvokeRequired Then
            If Not Me.IsDisposed Then Me.Invoke(SyncMainReportHandler, msg, percent)
            Exit Sub
        End If

        progSync.Value = percent
        lbSyncTitle.Text = msg
    End Sub

    Private Sub SyncSubReport(ByVal msg As String, ByVal percent As Integer)
        If Me.InvokeRequired Then
            If Not Me.IsDisposed Then Me.Invoke(SyncSubReportHandler, msg, percent)
            Exit Sub
        End If
        lbSyncInfo.Text = msg & " - " & percent & "%"
    End Sub
#End Region


    Private Sub btSyncCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSyncCancel.Click
        Try
            SyncThread.Abort()
            SyncFinish()
            syncClient.SyncWorking = False
        Catch ex As Exception

        End Try
    End Sub

    Dim UpdateCbClinetListHandler As New Action(AddressOf UpdateCbClientList)
    Dim UpdateCbClientLock As String = "UpdateCbClientLock"
    Public Sub UpdateCbClientList()
        If Me.InvokeRequired Then
            If Not Me.IsDisposed Then Me.Invoke(UpdateCbClinetListHandler)
            Exit Sub
        End If

        SyncLock UpdateCbClientLock
            For i As Integer = cbClient.Items.Count - 1 To ClientManager.Client.Length Step -1
                cbClient.Items.RemoveAt(i)
            Next

            For i As Integer = cbClient.Items.Count To ClientManager.Client.Length - 1
                cbClient.Items.Add(ClientManager(i).Name)
            Next

            For i As Integer = 0 To cbClient.Items.Count - 1
                If cbClient.Items(i) <> ClientManager(i).Name Then
                    cbClient.Items(i) = ClientManager(i).Name
                End If
            Next
        End SyncLock
        cbClient_UpdateState()
        'cbClient.Items.Clear()
        'cbClient.Items.AddRange(ClientManager.GetNameList())
    End Sub

    Private Sub 待辦事項ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 待辦事項ToolStripMenuItem.Click
        winAgendum.Show(access)
    End Sub



    Private Sub 錯誤記錄ToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 錯誤記錄ToolStripMenuItem.Click
        DialogErrorFileList.ShowDialog(access)
    End Sub

    Private Sub 連線內容ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 連線內容ToolStripMenuItem.Click
        winClient.Show(access)
    End Sub
End Class