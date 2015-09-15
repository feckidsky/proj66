Imports 進銷存.Database.DatabaseType
Imports 進銷存.Database
'Imports System.Runtime.CompilerServices

Public Module UpdateBatabase

    ''' <summary>資料庫板本編號</summary>
    Public DatabaseVerson As String = "010203"

#Region "同步資料庫"


    Dim UpdateDataLock As String = "UpdateDataLock"
    Public syncClient As AccessClient = Nothing

    Public Sub SyncDatabase(ByVal client As Database.AccessClient)
        'Exit Sub
        If client.SyncWorking Then Exit Sub
        SyncLock UpdateDataLock
            If Not client.Connected Then Exit Sub
            syncClient = client
            client.SyncWorking = True
            Dim SyncDialog As New ProgressDialog
            SyncDialog.AutoClose = False
            SyncDialog.Title = "同步資料庫"
            SyncDialog.SubBarVisible = True
            SyncDialog.Thread = Threading.Thread.CurrentThread
            Dim totProgress As New Access.Progress(AddressOf SyncDialog.UpdateProgress, "同步資料庫", Nothing, 0, 10)
            Dim partProgress As New Access.Progress(AddressOf SyncDialog.UpdatePartProgress, "", Nothing)
            totProgress.SubProgress = partProgress
            'SyncDialog.CancelHandler = totProgress.CancelHandler
            Dim SyncDialogShowHandler As New Action(AddressOf SyncDialog.Show)


            Dim main As winMain = My.Application.OpenForms("winMain")

            While main Is Nothing
                main = My.Application.OpenForms("winMain")
            End While
            main.SyncThread = Threading.Thread.CurrentThread
            totProgress.ProgressCallback = [Delegate].Combine(totProgress.ProgressCallback, main.SyncMainReportHandler)
            partProgress.ProgressCallback = [Delegate].Combine(partProgress.ProgressCallback, main.SyncSubReportHandler)
            'totProgress.FinishCallback = [Delegate].Combine(totProgress.FinishCallback, main.SyncFinishSyncHandler)
            main.SyncStartSyncHandler.Invoke()
OpenDialog:
            Try
                '停用進度顯示視窗
                'My.Application.OpenForms(0).Invoke(SyncDialogShowHandler)
            Catch
                GoTo OpenDialog
            End Try


            Try


                'GoTo SyncPrice '測試用-直接跳到同步歷史售價的動作
                totProgress.Reset(0, 10)
                partProgress.Text = "讀取" & client.Name & "商品項目"
                Dim sDT As DataTable = Nothing  '= client.GetGoodsList(totProgress)

                Do While sDT Is Nothing
                    sDT = client.GetGoodsList(totProgress)
                    totProgress.WaitFinish()
                Loop

                totProgress.Reset(10, 15)
                partProgress.Text = "讀取本機商品項目"
                Dim myDT As DataTable = myDatabase.GetGoodsList(totProgress)

                totProgress.Reset(15, 20)
                partProgress.Text = ("同步" & client.Name & "商品項目")
                For i As Integer = 0 To sDT.Rows.Count - 1 '  As DataRow In sDT.Rows
                    totProgress.Report((i + 1) / sDT.Rows.Count * 100)
                    Select Case CompareModify(myDT, sDT.Rows(i)("Label"), GetTime(sDT.Rows(i)("Modify")))
                        Case Compare.MoreNew : myDatabase.ChangeGoods(Goods.GetFrom(sDT.Rows(i)), Access.Source.Far)
                        Case Compare.NoExist : myDatabase.AddGoods(Goods.GetFrom(sDT.Rows(i)), Access.Source.Far)
                    End Select
                Next

                totProgress.Reset(20, 25)
                partProgress.Text = "讀取" & client.Name & "員工資料"
                sDT = Nothing  'lient.GetPersonnelList(totProgress)
                Do While sDT Is Nothing
                    sDT = client.GetPersonnelList(totProgress)
                    totProgress.WaitFinish()
                Loop
                totProgress.Reset(25, 35)
                partProgress.Text = "讀取本機員工資料"
                myDT = myDatabase.GetPersonnelList(totProgress)


                totProgress.Reset(35, 40)
                partProgress.Text = "同步" & client.Name & "員工資料"
                For i As Integer = 0 To sDT.Rows.Count - 1 '  As DataRow In sDT.Rows
                    totProgress.Report((i + 1) / sDT.Rows.Count * 100)
                    Select Case CompareModify(myDT, sDT.Rows(i)("Label"), GetTime(sDT.Rows(i)("Modify")))
                        Case Compare.MoreNew : myDatabase.ChangePersonnel(Personnel.GetFrom(sDT.Rows(i)), Access.Source.Far)
                        Case Compare.NoExist : myDatabase.AddPersonnel(Personnel.GetFrom(sDT.Rows(i)), Access.Source.Far)
                    End Select
                Next

                totProgress.Reset(40, 45)
                partProgress.Text = "讀取" & client.Name & " 客戶資料"
                sDT = Nothing  'client.GetCustomerList(totProgress)
                Do While sDT Is Nothing
                    sDT = client.GetCustomerList(totProgress)
                    totProgress.WaitFinish()
                Loop
                totProgress.Reset(45, 50)
                partProgress.Text = "讀取本機員工資料"
                myDT = myDatabase.GetCustomerList(totProgress)
                totProgress.Reset(50, 55)
                partProgress.Text = "同步" & client.Name & "客戶資料"
                For i As Integer = 0 To sDT.Rows.Count - 1 '  As DataRow In sDT.Rows
                    totProgress.Report((i + 1) / sDT.Rows.Count * 100)
                    Select Case CompareModify(myDT, sDT.Rows(i)("Label"), GetTime(sDT.Rows(i)("Modify")))
                        Case Compare.MoreNew : myDatabase.ChangeCustomer(Customer.GetFrom(sDT.Rows(i)), Access.Source.Far)
                        Case Compare.NoExist : myDatabase.AddCustomer(Customer.GetFrom(sDT.Rows(i)), Access.Source.Far)
                    End Select
                Next

                totProgress.Reset(55, 60)
                partProgress.Text = "讀取" & client.Name & "供應商資料"
                sDT = Nothing  'client.GetSupplierList(totProgress)
                Do While sDT Is Nothing
                    sDT = client.GetSupplierList(totProgress)
                    totProgress.WaitFinish()
                Loop
                totProgress.Reset(60, 65)
                partProgress.Text = "讀取本機供應商資料"
                myDT = myDatabase.GetSupplierList(totProgress)
                totProgress.Reset(65, 70)
                partProgress.Text = "同步" & client.Name & "供應商資料"
                For i As Integer = 0 To sDT.Rows.Count - 1 '  As DataRow In sDT.Rows
                    totProgress.Report((i + 1) / sDT.Rows.Count * 100)
                    Select Case CompareModify(myDT, sDT.Rows(i)("Label"), GetTime(sDT.Rows(i)("Modify")))
                        Case Compare.MoreNew : myDatabase.ChangeSupplier(Supplier.GetFrom(sDT.Rows(i)), Access.Source.Far)
                        Case Compare.NoExist : myDatabase.AddSupplier(Supplier.GetFrom(sDT.Rows(i)), Access.Source.Far)
                    End Select
                Next

                totProgress.Reset(70, 75)
                partProgress.Text = "讀取" & client.Name & "合約項目"
                sDT = Nothing 'client.GetContractList(totProgress)
                Do While sDT Is Nothing
                    sDT = client.GetContractList(totProgress)
                    totProgress.WaitFinish()
                Loop
                totProgress.Reset(75, 80)
                partProgress.Text = "讀取本機合約項目"
                myDT = myDatabase.GetContractList(totProgress)
                totProgress.Reset(80, 85)
                partProgress.Text = "同步" & client.Name & "合約項目"
                For i As Integer = 0 To sDT.Rows.Count - 1 '  As DataRow In sDT.Rows
                    totProgress.Report((i + 1) / sDT.Rows.Count * 100)
                    Select Case CompareModify(myDT, sDT.Rows(i)("Label"), GetTime(sDT.Rows(i)("Modify")))
                        Case Compare.MoreNew : myDatabase.ChangeContract(Contract.GetFrom(sDT.Rows(i)), Access.Source.Far)
                        Case Compare.NoExist : myDatabase.AddContract(Contract.GetFrom(sDT.Rows(i)), Access.Source.Far)
                    End Select
                Next

SyncPrice:

                totProgress.Reset(85, 90)
                partProgress.Text = "讀取" & client.Name & "歷史售價"
                sDT = Nothing ' client.GetHistoryPriceList(totProgress)
                Do While sDT Is Nothing
                    sDT = client.GetHistoryPriceList(totProgress)
                    totProgress.WaitFinish()
                Loop
                totProgress.Reset(90, 95)
                partProgress.Text = "讀取本機歷史售價"
                myDT = myDatabase.GetHistoryPriceList(totProgress)
                totProgress.Reset(95, 99)
                partProgress.Text = "同步" & client.Name & "歷史售價"
                For i As Integer = 0 To sDT.Rows.Count - 1 '  As DataRow In sDT.Rows
                    totProgress.Report((i + 1) / sDT.Rows.Count * 100)
                    If CompareHistoryPrice(myDT, sDT.Rows(i)("GoodsLabel"), GetTime(sDT.Rows(i)("Time"))) = Compare.NoExist Then
                        myDatabase.AddHistoryPrice(HistoryPrice.GetFrom(sDT.Rows(i)), Access.Source.Far)
                    End If
                Next
            Catch

            End Try
            main.SyncFinishSyncHandler()
            SyncDialog.Close()
            client.SyncWorking = False
        End SyncLock
    End Sub
#End Region

#Region "更新資料庫結構"

    Private Sub UpdateDatabaseFirst()
        myDatabase.Command("UPDATE Personnel SET [Note]= 'DbVerson=010200' WHERE Label='Administrator' AND ([Note]='' OR IsNull([Note]))")

        myDatabase.ChangeTypeColumn(Stock.Table, "Note", Database.DBTypeNote)
        myDatabase.ChangeTypeColumn(Personnel.Table, "Note", Database.DBTypeNote)
        myDatabase.ChangeTypeColumn(Sales.Table, "Note", Database.DBTypeNote)
        myDatabase.ChangeTypeColumn(Customer.Table, "Note", Database.DBTypeNote)
        myDatabase.ChangeTypeColumn(Supplier.Table, "Note", Database.DBTypeNote)
        myDatabase.ChangeTypeColumn(Contract.Table, "Note", Database.DBTypeNote)
        myDatabase.ChangeTypeColumn(Goods.Table, "Note", Database.DBTypeNote)
        myDatabase.AddColumn(SalesContract.Table, "ReturnDate", Database.DBTypeDate)

        Dim d As OleDb.OleDbConnection = Database.Access.ConnectBase(myDatabase.BasePath)
        '新增退貨表
        Database.Access.CreateTable(ReturnGoods.Table, ReturnGoods.ToColumns, d)
        '新增暫存銷貨表
        Database.Access.CreateTable("tmpSales", Sales.ToColumns, d)
        d.Close()

        Dim SalesColumnsText As String = Join(Array.ConvertAll(Sales.ToColumns, Function(c As Column) "[" & c.Name & "]"), ",")
        Dim newSAles As String = "SELECT Sales.Label AS 單號, Sales.OrderDate AS 訂單時間, Sales.SalesDate AS 銷貨時間, Personnel.Label, Customer.Label, Sales.Deposit, Sales.DepositByCard, 0.02 AS Expr1, Sum(IIf(IsNull([SalesGoods].[SalesLabel]) Or [Sales].[TypeOfPayment]<>0,0,[SellingPrice]*[SalesGoods].[Number]))+IIf(IsNull([Price]) Or [Sales].[TypeOfPayment]<>0,0,[Price])-[Deposit] AS 現金, Sum(IIf(IsNull([SalesGoods].[SalesLabel]) Or [Sales].[TypeOfPayment]<>1,0,[SellingPrice]*[SalesGoods].[Number]))+IIf(IsNull([Price]) Or [Sales].[TypeOfPayment]<>1,0,[Price])-[DepositByCard] AS 刷卡, 0.02 AS Expr2, IIf([Sales].[TypeOfPayment]=0,1,Sales.TypeOfPayment) AS 付款方式, Sales.Note AS 備註 " & _
        " FROM (((((Sales LEFT JOIN (SalesGoods LEFT JOIN Stock ON SalesGoods.StockLabel = Stock.Label) ON Sales.Label = SalesGoods.SalesLabel) LEFT JOIN Customer ON Sales.CustomerLabel = Customer.Label) LEFT JOIN Personnel ON Sales.PersonnelLabel = Personnel.Label) LEFT JOIN (SELECT SalesLabel, sum(Contract.Prepay)-sum(SalesContract.Discount) AS Price, sum(SalesContract.commission)-sum(SalesContract.Discount) AS profit FROM SalesContract LEFT JOIN Contract ON SalesContract.ContractLabel=Contract.Label GROUP BY SalesLabel)  AS ContractInfo ON Sales.Label = ContractInfo.SalesLabel) LEFT JOIN ReturnGoods ON Sales.Label = ReturnGoods.ReturnLabel) LEFT JOIN Stock AS Stock_1 ON ReturnGoods.StockLabel = Stock_1.Label " & _
        " GROUP BY Sales.Label, Sales.OrderDate, Sales.SalesDate, Personnel.Label, Customer.Label, Sales.Deposit, Sales.DepositByCard, 0.02, 0.02, [Sales].[TypeOfPayment], Sales.Note, ContractInfo.Price, ContractInfo.profit"
        myDatabase.Command("INSERT INTO tmpSales (" & SalesColumnsText & ") " & newSAles)

        myDatabase.Command("DROP TABLE Sales")

        d = Database.Access.ConnectBase(myDatabase.BasePath)
        Access.CreateTable(Sales.Table, Sales.ToColumns, d)
        d.Close()

        myDatabase.Command("INSERT INTO Sales (" & SalesColumnsText & ") SELECT " & SalesColumnsText & " FROM tmpSales")
        myDatabase.Command("DROP TABLE tmpSales")
        'Access.RepairAccess(myDatabase.BasePath)
    End Sub

    Public Sub UpdateDatabase010200()
        myDatabase.Command("UPDATE Personnel SET [Note]= 'DbVerson=010201' WHERE Label='Administrator' AND ([Note]='DbVerson=010200')")
        myDatabase.Command("DELETE FROM HistoryPrice WHERE GoodsLabel='';")
    End Sub

    Public Sub UpdateDatabase010201()
        myDatabase.Command("UPDATE Personnel SET [Note]= 'DbVerson=010202' WHERE Label='Administrator' AND ([Note]='DbVerson=010201')")
        If Not IO.File.Exists(myDatabase.MsgPath) Then Access.CreateBulletinFile(myDatabase.MsgPath)
    End Sub

    Public Sub UpdateDatabase010202()
        myDatabase.Command("UPDATE Personnel SET [Note]= 'DbVerson=010203' WHERE Label='Administrator' AND ([Note]='DbVerson=010202')")
        myDatabase.AddColumn(StockMove.Table, "TransferPrice", Database.DBTypeInteger)
        myDatabase.Command("UPDATE " & StockMove.Table & " SET [TransferPrice]= Cost WHERE isnull(TransferPrice)")
    End Sub

    Public Function UpdateDatabase() As Boolean
        If Config.Mode = Connect.Client Then Return False
        Dim info As Access.DbInfo = myDatabase.ReadDbInfo
        Dim changed As Boolean = info.DbVerson <> DatabaseVerson

        If changed Then
            MsgBox("即將進行資料庫更新動作，建議先備份目前的資料庫!", MsgBoxStyle.Information, vbOKOnly)
            If MsgBox("是否進行備份資料庫？", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                BackupDialog.ShowDialog()
            End If
        End If


        If info.IsNull Then UpdateDatabaseFirst()
        If info.DbVerson <= 10200 Then UpdateDatabase010200()
        If info.DbVerson <= 10201 Then UpdateDatabase010201()
        If info.DbVerson <= 10202 Then UpdateDatabase010202()

        '壓縮/修復資料庫
        If changed Then Access.RepairAccess(myDatabase.BasePath)
        Return changed
    End Function
#End Region

    Enum Compare
        Normal = 0
        NoExist = 1
        MoreNew = 2
    End Enum

    Public Function CompareHistoryPrice(ByVal DT As DataTable, ByVal Label As String, ByVal Time As Date) As Compare
        For Each r As DataRow In DT.Rows
            If r("GoodsLabel") = Label And GetTime(r("Time")) = Time Then Return Compare.Normal
            If Label = "" Then Return Compare.Normal
        Next
        Return Compare.NoExist
    End Function

    Public Function CompareModify(ByVal DT As DataTable, ByVal Label As String, ByVal Modify As Date) As Compare
        For Each r As DataRow In DT.Rows
            If r("Label") = Label Then
                If GetTime(r("Modify")) < Modify Then
                    Return Compare.MoreNew
                Else
                    Return Compare.Normal
                End If
            End If
        Next
        Return Compare.NoExist
    End Function
End Module
