Imports 進銷存.Database.DatabaseType
Imports 進銷存.Database
Imports System.Runtime.CompilerServices

Public Module Program
#Region "Structure"

    Enum Connect
        Client = 0
        Server = 1
    End Enum

    Structure SystemOptional
        Dim OrderBackcolor As Integer
        Dim SalesBackColor As Integer
        Dim Mode As Connect
        Dim ServerName As String
        Dim ServerPort As Integer
        Dim ServerNetIndex As Integer
        Dim BackupDir As String
        Dim EmailBackupEnable As Boolean
        Dim DirBackupEnable As Boolean
        Shared ReadOnly Property DefaultConfig()
            Get
                Dim def As SystemOptional
                def.OrderBackcolor = Color.LightPink.ToArgb
                def.SalesBackColor = Color.LightGreen.ToArgb
                def.Mode = Connect.Client
                def.ServerName = My.Computer.Name
                def.ServerPort = 3600
                def.ServerNetIndex = 0
                def.BackupDir = My.Application.Info.DirectoryPath & "\Data\Backup\"
                def.EmailBackupEnable = False
                def.DirBackupEnable = False
                Return def
            End Get
        End Property

    End Structure



    Public Structure LoginInfo
        Dim Shop As String
        Dim ID As String
        Dim Password As String
        Dim AutoLog As Boolean
        Sub New(ByVal shop As String, ByVal id As String, ByVal password As String, ByVal autoLogIn As Boolean)
            Me.Shop = shop : Me.ID = id : Me.Password = password : Me.AutoLog = autoLogIn
        End Sub

        Shared ReadOnly Property Null() As LoginInfo
            Get
                Return New LoginInfo("", "", "", False)
            End Get
        End Property

        Public Shared Function Load(ByVal path As String) As LoginInfo
            Return Code.LoadXml(path, LoginInfo.Null, Code.ZipMode.ZIP)
        End Function

        Public Sub Save(ByVal path As String)
            Code.SaveXml(Me, path, Code.ZipMode.ZIP)
        End Sub


    End Structure

    Public Structure MailInformation
        Dim Server As String
        Dim Port As Integer
        Dim ID As String
        Dim Password As String
        Dim From As String
        Dim [To] As String

        Sub New(ByVal server As String, ByVal port As Integer, ByVal id As String, ByVal password As String, ByVal form As String, ByVal [to] As String)
            Me.Server = server : Me.Port = port : Me.ID = id : Me.Password = password : Me.From = form : Me.To = [to]
        End Sub
        Shared ReadOnly Property [Default]() As MailInformation
            Get
                ' , "feckidsky", "yqtaa3883", "title", "content", "就是我<feckidsky@gmail.com>", "kid.sky@yahoo.com.tw", files.ToArray))
                Return New MailInformation("smtp.gmail.com", 587, "", "", "", "")
            End Get
        End Property

        Public Shared Function Load(ByVal path As String) As MailInformation
            Return Code.LoadXml(path, [Default], Code.ZipMode.ZIP)
        End Function

        Public Sub Save(ByVal path As String)
            Code.SaveXml(Me, path, Code.ZipMode.ZIP)
        End Sub

    End Structure


#End Region

    Public WithEvents myDatabase As New Database.Access("本機資料庫")

    Public Server As New Database.AccessServer
    Public WithEvents Client As New Database.AccessClientMenage()


    Public Config As SystemOptional
    Public MailInfo As MailInformation
    Public ConfigPath As String = My.Application.Info.DirectoryPath & "\Config.xml"
    Public ClientPath As String = My.Application.Info.DirectoryPath & "\Client.xml"
    Public SalesVisiblePath As String = My.Application.Info.DirectoryPath & "\SalesVisible.xml"
    Public StockMoveVisiblePath As String = My.Application.Info.DirectoryPath & "\StockVisible.xml"
    Public LoginInfoPath As String = My.Application.Info.DirectoryPath & "\Login.xml"
    Public MailInfoPath As String = My.Application.Info.DirectoryPath & "Mail.xml"

    Public CurrentUser As Database.Personnel = Database.Personnel.Guest
    Public LoginSetting As LoginInfo
    Public WithEvents CurrentAccess As Access

    Public SystemTitle As String = "進銷存管理系統"
    Public DatabaseVerson As String = "010200"

    <Extension()> _
    Public Sub DefaultTextBoxImeMode(ByVal Form As Form)
        With Form
            For Each ctl As Control In .Controls
                If ctl.GetType() Is GetType(TextBox) Then CType(ctl, TextBox).ImeMode = Windows.Forms.ImeMode.OnHalf
            Next
        End With
    End Sub

    Public Sub InitialProgram()

        ConfigLoad()

        If Config.Mode = Connect.Server Then
            myDatabase.Name = Config.ServerName
            Server.Access = myDatabase
            Server.Port = Config.ServerPort
            Server.Name = Config.ServerName
            Server.Open(Config.ServerNetIndex)
        End If


        Dim lstClient As New List(Of Database.Access)
        If Config.Mode = Connect.Server Then lstClient.Add(myDatabase)
        lstClient.AddRange(Database.AccessClientMenage.Load(ClientPath))
        Client.Client = lstClient.ToArray()
        Client.StartConnect()

        LoginSetting = LoginInfo.Load(LoginInfoPath)
        MailInfo = MailInformation.Load(MailInfoPath)

        'LogOut(False)
        'Dim admin As Personnel = DB.GetPersonnelByID("Administrator")
        'LogIn(admin.ID, admin.Password, False)
    End Sub

    Public Sub FinishProgram()

        Client.EndConnect()
        Try
            Server.Close()
        Catch
        End Try
    End Sub

    Private Sub UpdateDatabaseFirst()
        myDatabase.Command("UPDATE Personnel SET [Note]= 'DbVerson=" & DatabaseVerson & "' WHERE Label='Administrator' AND ([Note]='' OR IsNull([Note]))")

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
        Access.RepairAccess(myDatabase.BasePath)
    End Sub

    Public Sub UpdateDatabase()
        If Config.Mode = Connect.Client Then Exit Sub
        Dim info As Access.DbInfo = myDatabase.ReadDbInfo
        If info.IsNull Then UpdateDatabaseFirst()



        'myDatabase.AddColumn(Sales.Table, "DepositCardCharge", DBTypeSingle)
        'myDatabase.AddColumn(Sales.Table, "PayByCash", DBTypeSingle)
        'myDatabase.AddColumn(Sales.Table, "PayByCard", DBTypeSingle)
        'myDatabase.AddColumn(Sales.Table, "PayCardCharge", DBTypeSingle)


        'Database.Access.CreateTable(ReturnContract.Table, ReturnContract.ToColumns, d)
        '銷貨表補上銷貨日期
        'Dim SqlCommand As String = "UPDATE SalesGoods SET SalesDate=cdate(  mid(SalesLabel,3,2) +""/"" +mid(SalesLabel,5,2)  + ""/"" + mid(SalesLabel,7,2) ) WHERE IsNull(SalesDate);"
        'Dim count As Long = Database.Access.Command(SqlCommand, d)
        'SqlCommand = "UPDATE SalesContract SET SalesDate=cdate(  mid(SalesLabel,3,2) +""/"" +mid(SalesLabel,5,2)  + ""/"" + mid(SalesLabel,7,2) ) WHERE IsNull(SalesDate);"
        'count = Database.Access.Command(SqlCommand, d)


        'myDatabase.Command("UPDATE Sales SET DepositCardCharge=0.02 WHERE IsNull(DepositCardCharge);")
        'myDatabase.Command("UPDATE Sales SET PayCardCharge=0.02 WHERE IsNull(DepositCardCharge);")


        'Database.Access.DeleteTable("Mobile", d)
        'Database.Access.CreateTable(Contract.Table, Contract.ToColumns, d)
        'Database.Access.CreateTable(SalesContract.Table, SalesContract.ToColumns, d)
        'Database.Access.CreateTable(OrderGoods.Table, OrderGoods.ToColumns, d)
        'Database.Access.CreateTable(HistoryPrice.Table, HistoryPrice.ToColumns(), d)
        'Database.Access.CreateTable(StockMove.Table, StockMove.ToColumns(), d)
        'Database.Access.CreateTable(Log.Table, Log.ToColumns, d)
        'myDatabase.DeleteColumn(Stock.Table, "Price")
        'myDatabase.AddColumn(Supplier.Table, "Modify", Database.DBTypeDate)
        'myDatabase.AddColumn(Customer.Table, "Modify", Database.DBTypeDate)
        'myDatabase.AddColumn(Personnel.Table, "Modify", Database.DBTypeDate)
        'myDatabase.AddColumn(Goods.Table, "Modify", Database.DBTypeDate)
        'myDatabase.AddColumn(Contract.Table, "Modify", Database.DBTypeDate)
        'myDatabase.RenameColumn(Sales.Table, "Deposit", "DepositByCash", Database.DBTypeSingle)
        'myDatabase.AddColumn(Sales.Table, "DepositByCard", Database.DBTypeSingle)
        'myDatabase.AddColumn(SalesContract.Table, "Commission", Database.DBTypeSingle)



    End Sub

    Public Sub ConfigLoad()
        Config = Code.LoadXml(Of SystemOptional)(ConfigPath, SystemOptional.DefaultConfig)
    End Sub

    Public Sub ConfigSave()
        Code.SaveXml(Config, ConfigPath)
    End Sub


    Public Function CheckAuthority(ByVal level As Integer, Optional ByVal WithAdmin As Boolean = False) As Boolean
        If Not WithAdmin And CurrentUser.IsAdministrator Then
            MsgBox(CurrentUser.Name & "無法進行此動作", MsgBoxStyle.Exclamation)
            Return False
        End If

        If CurrentUser.Authority >= level Then
            Return True
        Else
            MsgBox(CurrentUser.Name & " - 權限等級[" & CurrentUser.Authority & "]不足, 執行此動作的權限等級為[" & level & "]", MsgBoxStyle.Exclamation)
            Return False
        End If
    End Function



    Public Function GetNewSupplier() As Supplier
        Dim data As Supplier = Nothing
        data.Label = "SU" & Now.ToString("yyMMddHHmmss")
        Return data
    End Function

    Public Function GetNewPersonnel() As Personnel
        Dim data As Personnel = Nothing
        data.Label = "P" & Now.ToString("yyMMddHHmmss")
        Return data
    End Function

    Public Function GetNewCustomer() As Customer
        Dim data As Customer = Nothing
        data.Label = "C" & Now.ToString("yyMMddHHmmss")
        Return data
    End Function

    Public Function GetNewGoods() As Goods
        Dim data As Goods = Nothing
        data.Label = "G" & Now.ToString("yyMMddHHmmss")
        Return data
    End Function

    Public Function GetNewMobile() As Contract
        Dim data As Contract = Nothing
        data.Label = "M" & Now.ToString("yyMMddHHmmss")
        Return data
    End Function

    Public Function GetNewStock() As Stock
        Dim data As Stock = Nothing
        data.Label = "ST" & Now.ToString("yyMMddHHmmss")
        data.Date = Now
        data.Number = 1
        Return data
    End Function

    Public Function GetNewSales() As Sales
        Dim data As Sales = Nothing
        data.Label = "SA" & Now.ToString("yyMMddHHmmss")
        data.OrderDate = Now
        data.PayCardCharge = 0.02
        data.DepositCardCharge = 0.02
        Return data
    End Function

    'Public Function GetNewOrder() As Order
    '    Dim data As Order = Nothing
    '    data.Label = "O" & Now.ToString("yyMMddHHmmss")
    '    data.Date = Now
    '    Return data
    'End Function

    Public Function GetNewContract() As Contract
        Dim data As New Contract
        data.Label = "C" & Now.ToString("yyMMddHHmmss")
        data.Enable = True
        Return data
    End Function

    Public Function ToColor(ByVal argb As Integer) As Color
        Return Color.FromArgb(argb)
    End Function


#Region "Code - 序列化 / 壓縮"
    Public Class Code
        Public Shared Function Zip(ByVal Text As String, Optional ByVal ZipCount As Int16 = 1) As String
            If Text = "" Then Return ""
            If ZipCount > 0 Then
                Dim Data As Byte() = System.Text.Encoding.UTF8.GetBytes(Text)
                Return Convert.ToBase64String(Zip(Data, ZipCount))
            Else
                Return Text
            End If
        End Function

        Public Shared Function Unzip(ByVal Text As String, Optional ByVal ZipCount As Int16 = 1) As String
            If Text = "" Then Return ""
            If ZipCount > 0 Then
                Dim Data As Byte() = Convert.FromBase64String(Text)
                Return System.Text.Encoding.UTF8.GetString(Unzip(Data, ZipCount))
            Else
                Return Text
            End If
        End Function


        Public Shared Function Zip(ByVal Data() As Byte, Optional ByVal Count As Int16 = 1) As Byte()
            If Count = 0 Then Return Data
            Dim ms As IO.MemoryStream = New IO.MemoryStream()
            Dim compressedzipStream As IO.Compression.GZipStream = New IO.Compression.GZipStream(ms, IO.Compression.CompressionMode.Compress, True)
            compressedzipStream.Write(Data, 0, Data.Length)
            compressedzipStream.Close()
            compressedzipStream.Dispose()
            Return Zip(ms.ToArray, Count - 1)
            ms.Dispose()
        End Function

        Public Shared Function Unzip(ByVal Data() As Byte, Optional ByVal Count As Int16 = 1) As Byte()
            If Count = 0 Then Return Data
            Dim ms As IO.MemoryStream = New IO.MemoryStream(Data)
            Dim compressedzipStream As IO.Compression.GZipStream = New IO.Compression.GZipStream(ms, IO.Compression.CompressionMode.Decompress, True)

            Dim buff(4095) As Byte
            Dim read As Long = compressedzipStream.Read(buff, 0, buff.Length)
            Dim output As New IO.MemoryStream()
            output.Write(buff, 0, read)
            Do While (read > 0)
                read = compressedzipStream.Read(buff, 0, buff.Length)
                output.Write(buff, 0, read)
            Loop

            ms.Dispose()
            compressedzipStream.Close()
            compressedzipStream.Dispose()
            Return Unzip(output.ToArray, Count - 1)
            output.Dispose()
        End Function

        Public Shared Function XmlSerializeWithZIP(Of T)(ByVal obj As T) As String
            Return Zip(XmlSerialize(obj))
        End Function

        Public Shared Function XmlDeserializeWithUnzip(Of T)(ByVal ZipText As String, ByVal Type As Type) As T
            Return XmlDeserialize(Of T)(Unzip(ZipText))
        End Function

        Public Shared Function XmlDeserializeWithUnzip(Of T)(ByVal ZipText As String) As T
            Return XmlDeserialize(Of T)(Unzip(ZipText))
        End Function

        Public Shared Function XmlSerialize(Of T)(ByVal Obj As T) As String
            Try
                Dim ser As Xml.Serialization.XmlSerializer = New Xml.Serialization.XmlSerializer(GetType(T))
                Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
                Dim writer As IO.StringWriter = New IO.StringWriter(sb)
                ser.Serialize(writer, Obj)
                Return sb.ToString()
                writer.Dispose()
            Catch
                Return ""
            End Try
        End Function

        Public Shared Function XmlDeserialize(Of T)(ByVal Text As String) As T
            ''將取得的內容進行反序列化
            Dim mySerializer As Xml.Serialization.XmlSerializer = New Xml.Serialization.XmlSerializer(GetType(T)) 'GetType(SerializeData))
            Dim reader As New IO.StringReader(Text)
            Try
                Return mySerializer.Deserialize(reader)
            Catch
                Return Nothing
            Finally
                reader.Dispose()
            End Try
        End Function

        Public Shared Function BinSerailzie(Of T)(ByVal obj As T) As Byte()
            Dim sfFormatter As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
            Dim fStream As New IO.MemoryStream() 'New FileStream("1.dat", FileMode.Create)
            Try
                sfFormatter.Serialize(fStream, obj)
                Return fStream.ToArray
            Catch
                Return New Byte() {}
            Finally
                fStream.Dispose()
            End Try
        End Function

        Public Shared Function BinDeserialize(Of T)(ByVal data() As Byte) As T
            Dim sfFormatter As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter

            Dim fStream As New IO.MemoryStream(data)
            Try
                Return sfFormatter.Deserialize(fStream)
            Catch ex As Exception
                Return Nothing
            Finally
                fStream.Dispose()
            End Try
        End Function

        Public Enum ZipMode
            Normal = 0
            ZIP = 1
        End Enum

        Public Shared Sub SaveXml(Of T)(ByVal Data As T, ByVal FilePath As String, Optional ByVal Mode As ZipMode = ZipMode.Normal)
            Dim Dir As String = IO.Path.GetDirectoryName(FilePath)
            If Not IO.Directory.Exists(Dir) Then IO.Directory.CreateDirectory(Dir)
            Dim Text As String = Code.XmlSerialize(Data)
            If Mode = ZipMode.ZIP Then Text = Code.Zip(Text)
            My.Computer.FileSystem.WriteAllText(FilePath, Text, False, System.Text.Encoding.Unicode)
        End Sub

        Public Shared Function LoadXml(Of T)(ByVal FilePath As String, ByVal DefaultData As T, Optional ByVal Mode As ZipMode = ZipMode.Normal) As T
            If IO.File.Exists(FilePath) Then
                Dim Text As String = My.Computer.FileSystem.ReadAllText(FilePath, System.Text.Encoding.Unicode)
                If Mode = ZipMode.ZIP Then Text = Code.Unzip(Text)
                Return Code.XmlDeserialize(Of T)(Text)
            Else
                Return DefaultData
            End If
        End Function




    End Class
#End Region

    WithEvents ccc As Database.AccessClient
    Private Sub Client_BeforeConnect(ByVal sender As Object, ByVal client As Database.AccessClient) Handles Client.BeforeConnect

        With client
            AddHandler .Account_LogIn, AddressOf ccc_Account_LogIn
            AddHandler .Account_Logout, AddressOf ccc_Account_LogIn

            AddHandler .CreatedContract, AddressOf CreatedContract
            AddHandler .CreatedCustomer, AddressOf CreatedCustomer
            AddHandler .CreatedSupplier, AddressOf CreatedSupplier
            AddHandler .CreatedPersonnel, AddressOf CreatedPersonnel
            AddHandler .CreatedGoods, AddressOf CreatedGoods
            AddHandler .CreatedHistoryPrice, AddressOf CreatedHistoryPrice

            AddHandler .ChangedContract, AddressOf ChangedContract
            AddHandler .ChangedCustomer, AddressOf ChangedCustomer
            AddHandler .ChangedSupplier, AddressOf ChangedSupplier
            AddHandler .ChangedPersonnel, AddressOf ChangedPersonnel
            AddHandler .ChangedGoods, AddressOf ChangedGoods
            AddHandler .ChangedHistoryPrice, AddressOf ChangedHistoryPrice

            AddHandler .DeletedContract, AddressOf DeletedContract
            AddHandler .DeletedCustomer, AddressOf DeletedCustomer
            AddHandler .DeletedSupplier, AddressOf DeletedSupplier
            AddHandler .DeletedPersonnel, AddressOf DeletedPersonnel
            AddHandler .DeletedGoods, AddressOf DeletedGoods
            AddHandler .DeletedHistoryPrice, AddressOf DeletedHistoryPrice
            AddHandler .DeletedHistoryPriceList, AddressOf DeletedHistoryPriceList
            AddHandler .ConnectedSuccess, AddressOf ConnectSuccess
            AddHandler .ReceiveServerName, AddressOf ccc_ReceiveServerName

        End With

    End Sub

    Private Sub Client_BeforeDisconnect(ByVal sender As Object, ByVal client As Database.AccessClient) Handles Client.BeforeDisconnect

        With client
            RemoveHandler .Account_LogIn, AddressOf ccc_Account_LogIn
            RemoveHandler .Account_Logout, AddressOf ccc_Account_LogIn

            RemoveHandler .CreatedContract, AddressOf CreatedContract
            RemoveHandler .CreatedCustomer, AddressOf CreatedCustomer
            RemoveHandler .CreatedSupplier, AddressOf CreatedSupplier
            RemoveHandler .CreatedPersonnel, AddressOf CreatedPersonnel
            RemoveHandler .CreatedGoods, AddressOf CreatedGoods
            RemoveHandler .CreatedHistoryPrice, AddressOf CreatedHistoryPrice

            RemoveHandler .ChangedContract, AddressOf ChangedContract
            RemoveHandler .ChangedCustomer, AddressOf ChangedCustomer
            RemoveHandler .ChangedSupplier, AddressOf ChangedSupplier
            RemoveHandler .ChangedPersonnel, AddressOf ChangedPersonnel
            RemoveHandler .ChangedGoods, AddressOf ChangedGoods
            RemoveHandler .ChangedHistoryPrice, AddressOf ChangedHistoryPrice

            RemoveHandler .DeletedContract, AddressOf DeletedContract
            RemoveHandler .DeletedCustomer, AddressOf DeletedCustomer
            RemoveHandler .DeletedSupplier, AddressOf DeletedSupplier
            RemoveHandler .DeletedPersonnel, AddressOf DeletedPersonnel
            RemoveHandler .DeletedGoods, AddressOf DeletedGoods
            RemoveHandler .DeletedHistoryPrice, AddressOf DeletedHistoryPrice
            RemoveHandler .DeletedHistoryPriceList, AddressOf DeletedHistoryPriceList
            RemoveHandler .ConnectedSuccess, AddressOf ConnectSuccess
            RemoveHandler .ReceiveServerName, AddressOf ccc_ReceiveServerName
        End With

    End Sub

    Dim UpdateDataLock As String = "UpdateDataLock"


    Private Sub SyncDatabase(ByVal client As Database.AccessClient)
        SyncLock UpdateDataLock
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

OpenDialog:
            Try
                My.Application.OpenForms(0).Invoke(SyncDialogShowHandler)

            Catch
                GoTo OpenDialog
            End Try


            Try

                totProgress.Reset(0, 10)
                partProgress.Text = "讀取" & client.Name & "商品項目"
                Dim sDT As DataTable = client.GetGoodsList(totProgress)

                Do While sDT Is Nothing
                    sDT = client.GetGoodsList(totProgress)
                Loop

                totProgress.Reset(10, 15)
                partProgress.Text = "讀取本機商品項目"
                Dim myDT As DataTable = myDatabase.GetGoodsList(totProgress)

                totProgress.Reset(15, 20)
                partProgress.Text = ("同步" & client.Name & "商品項目")
                For i As Integer = 0 To sDT.Rows.Count - 1 '  As DataRow In sDT.Rows
                    totProgress.Report((i + 1) / sDT.Rows.Count * 100)
                    Select Case CompareModify(myDT, sDT.Rows(i)("Label"), GetTime(sDT.Rows(i)("Modify")))
                        Case Compare.MoreNew : myDatabase.ChangeGoods(Goods.GetFrom(sDT.Rows(i)), False)
                        Case Compare.NoExist : myDatabase.AddGoods(Goods.GetFrom(sDT.Rows(i)), False)
                    End Select
                Next

                totProgress.Reset(20, 25)
                partProgress.Text = "讀取" & client.Name & "員工資料"
                sDT = client.GetPersonnelList(totProgress)
                Do While sDT Is Nothing
                    sDT = client.GetPersonnelList(totProgress)
                Loop
                totProgress.Reset(25, 35)
                partProgress.Text = "讀取本機員工資料"
                myDT = myDatabase.GetPersonnelList(totProgress)


                totProgress.Reset(35, 40)
                partProgress.Text = "同步" & client.Name & "員工資料"
                For i As Integer = 0 To sDT.Rows.Count - 1 '  As DataRow In sDT.Rows
                    totProgress.Report((i + 1) / sDT.Rows.Count * 100)
                    Select Case CompareModify(myDT, sDT.Rows(i)("Label"), GetTime(sDT.Rows(i)("Modify")))
                        Case Compare.MoreNew : myDatabase.ChangePersonnel(Personnel.GetFrom(sDT.Rows(i)), False)
                        Case Compare.NoExist : myDatabase.AddPersonnel(Personnel.GetFrom(sDT.Rows(i)), False)
                    End Select
                Next

                totProgress.Reset(40, 45)
                partProgress.Text = "讀取" & client.Name & " 客戶資料"
                sDT = client.GetCustomerList(totProgress)
                Do While sDT Is Nothing
                    sDT = client.GetCustomerList(totProgress)
                Loop
                totProgress.Reset(45, 50)
                partProgress.Text = "讀取本機員工資料"
                myDT = myDatabase.GetCustomerList(totProgress)
                totProgress.Reset(50, 55)
                partProgress.Text = "同步" & client.Name & "客戶資料"
                For i As Integer = 0 To sDT.Rows.Count - 1 '  As DataRow In sDT.Rows
                    totProgress.Report((i + 1) / sDT.Rows.Count * 100)
                    Select Case CompareModify(myDT, sDT.Rows(i)("Label"), GetTime(sDT.Rows(i)("Modify")))
                        Case Compare.MoreNew : myDatabase.ChangeCustomer(Customer.GetFrom(sDT.Rows(i)), False)
                        Case Compare.NoExist : myDatabase.AddCustomer(Customer.GetFrom(sDT.Rows(i)), False)
                    End Select
                Next

                totProgress.Reset(55, 60)
                partProgress.Text = "讀取" & client.Name & "供應商資料"
                sDT = client.GetSupplierList(totProgress)
                Do While sDT Is Nothing
                    sDT = client.GetSupplierList(totProgress)
                Loop
                totProgress.Reset(60, 65)
                partProgress.Text = "讀取本機供應商資料"
                myDT = myDatabase.GetSupplierList(totProgress)
                totProgress.Reset(65, 70)
                partProgress.Text = "同步" & client.Name & "供應商資料"
                For i As Integer = 0 To sDT.Rows.Count - 1 '  As DataRow In sDT.Rows
                    totProgress.Report((i + 1) / sDT.Rows.Count * 100)
                    Select Case CompareModify(myDT, sDT.Rows(i)("Label"), GetTime(sDT.Rows(i)("Modify")))
                        Case Compare.MoreNew : myDatabase.ChangeSupplier(Supplier.GetFrom(sDT.Rows(i)), False)
                        Case Compare.NoExist : myDatabase.AddSupplier(Supplier.GetFrom(sDT.Rows(i)), False)
                    End Select
                Next

                totProgress.Reset(70, 75)
                partProgress.Text = "讀取" & client.Name & "合約項目"
                sDT = client.GetContractList(totProgress)
                Do While sDT Is Nothing
                    sDT = client.GetContractList(totProgress)
                Loop
                totProgress.Reset(75, 80)
                partProgress.Text = "讀取本機合約項目"
                myDT = myDatabase.GetContractList(totProgress)
                totProgress.Reset(80, 85)
                partProgress.Text = "同步" & client.Name & "合約項目"
                For i As Integer = 0 To sDT.Rows.Count - 1 '  As DataRow In sDT.Rows
                    totProgress.Report((i + 1) / sDT.Rows.Count * 100)
                    Select Case CompareModify(myDT, sDT.Rows(i)("Label"), GetTime(sDT.Rows(i)("Modify")))
                        Case Compare.MoreNew : myDatabase.ChangeContract(Contract.GetFrom(sDT.Rows(i)), False)
                        Case Compare.NoExist : myDatabase.AddContract(Contract.GetFrom(sDT.Rows(i)), False)
                    End Select
                Next

                totProgress.Reset(85, 90)
                partProgress.Text = "讀取" & client.Name & "歷史售價"
                sDT = client.GetHistoryPriceList(totProgress)
                Do While sDT Is Nothing
                    sDT = client.GetContractList(totProgress)
                Loop
                totProgress.Reset(90, 95)
                partProgress.Text = "讀取本機歷史售價"
                myDT = myDatabase.GetHistoryPriceList(totProgress)
                totProgress.Reset(95, 99)
                partProgress.Text = "同步" & client.Name & "歷史售價"
                For i As Integer = 0 To sDT.Rows.Count - 1 '  As DataRow In sDT.Rows
                    totProgress.Report((i + 1) / sDT.Rows.Count * 100)
                    If CompareHistoryPrice(myDT, sDT.Rows(i)("GoodsLabel"), GetTime(sDT.Rows(i)("Time"))) = Compare.NoExist Then
                        myDatabase.AddHistoryPrice(HistoryPrice.GetFrom(sDT.Rows(i)), False)
                    End If
                Next
            Catch

            End Try

            SyncDialog.Close()
        End SyncLock
    End Sub

    Private Sub ConnectSuccess(ByVal sender As Object) Handles ccc.ConnectedSuccess
        If Config.Mode = Connect.Client Then Exit Sub
        SyncDatabase(sender)
    End Sub

    Public Function GetTime(ByVal obj As Object) As Date
        If obj Is DBNull.Value Then Return New Date(0)
        Return CType(obj, Date)
    End Function

    Enum Compare
        Normal = 0
        NoExist = 1
        MoreNew = 2
    End Enum

    Public Function CompareHistoryPrice(ByVal DT As DataTable, ByVal Label As String, ByVal Time As Date) As Compare
        For Each r As DataRow In DT.Rows
            If r("GoodsLabel") = Label And GetTime(r("Time")) = Time Then Return Compare.Normal
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

    Private Sub ChangedContract(ByVal sender As Object, ByVal con As Database.Contract) Handles ccc.ChangedContract
        If Config.Mode = Connect.Server Then myDatabase.ChangeContract(con, False)
    End Sub

    Private Sub ChangedCustomer(ByVal sender As Object, ByVal cus As Database.Customer) Handles ccc.ChangedCustomer
        If Config.Mode = Connect.Server Then myDatabase.ChangeCustomer(cus, False)
    End Sub

    Private Sub ChangedGoods(ByVal sender As Object, ByVal goods As Database.Goods) Handles ccc.ChangedGoods
        If Config.Mode = Connect.Server Then myDatabase.ChangeGoods(goods, False)
    End Sub

    Private Sub ChangedHistoryPrice(ByVal sender As Object, ByVal hp As Database.HistoryPrice) Handles ccc.ChangedHistoryPrice
        If Config.Mode = Connect.Server Then myDatabase.ChangeHistoryPrice(hp, False)
    End Sub

    Private Sub ChangedPersonnel(ByVal sender As Object, ByVal per As Database.Personnel) Handles ccc.ChangedPersonnel
        If Config.Mode = Connect.Server Then myDatabase.ChangePersonnel(per, False)
    End Sub


    Private Sub ChangedSupplier(ByVal sender As Object, ByVal sup As Database.Supplier) Handles ccc.ChangedSupplier
        If Config.Mode = Connect.Server Then myDatabase.ChangeSupplier(sup, False)
    End Sub

    Private Sub CreatedContract(ByVal sender As Object, ByVal con As Database.Contract) Handles ccc.CreatedContract
        If Config.Mode = Connect.Server Then myDatabase.AddContract(con, False)
    End Sub

    Private Sub CreatedCustomer(ByVal sender As Object, ByVal cus As Database.Customer) Handles ccc.CreatedCustomer
        If Config.Mode = Connect.Server Then myDatabase.AddCustomer(cus, False)
    End Sub

    Private Sub CreatedGoods(ByVal sender As Object, ByVal goods As Database.Goods) Handles ccc.CreatedGoods
        If Config.Mode = Connect.Server Then myDatabase.AddGoods(goods, False)
    End Sub

    Private Sub CreatedHistoryPrice(ByVal sender As Object, ByVal hp As Database.HistoryPrice) Handles ccc.CreatedHistoryPrice
        If Config.Mode = Connect.Server Then myDatabase.AddHistoryPrice(hp, False)
    End Sub

    Private Sub CreatedPersonnel(ByVal sender As Object, ByVal per As Database.Personnel) Handles ccc.CreatedPersonnel
        If Config.Mode = Connect.Server Then myDatabase.AddPersonnel(per, False)
    End Sub

    Private Sub CreatedSupplier(ByVal sender As Object, ByVal sup As Database.Supplier) Handles ccc.CreatedSupplier
        If Config.Mode = Connect.Server Then myDatabase.AddSupplier(sup, False)
    End Sub

    Private Sub DeletedContract(ByVal sender As Object, ByVal con As Database.Contract) Handles ccc.DeletedContract
        If Config.Mode = Connect.Server Then myDatabase.DeleteContract(con, False)
    End Sub

    Private Sub DeletedCustomer(ByVal sender As Object, ByVal cus As Database.Customer) Handles ccc.DeletedCustomer
        If Config.Mode = Connect.Server Then myDatabase.DeleteCustomer(cus, False)
    End Sub

    Private Sub DeletedGoods(ByVal sender As Object, ByVal goods As Database.Goods) Handles ccc.DeletedGoods
        If Config.Mode = Connect.Server Then myDatabase.DeleteGoods(goods, False)
    End Sub

    Private Sub DeletedHistoryPrice(ByVal sender As Object, ByVal hp As Database.HistoryPrice) Handles ccc.DeletedHistoryPrice
        If Config.Mode = Connect.Server Then myDatabase.DeleteHistoryPrice(hp, False)
    End Sub

    Private Sub DeletedHistoryPriceList(ByVal sender As Object, ByVal hp As Database.HistoryPrice) Handles ccc.DeletedHistoryPriceList
        If Config.Mode = Connect.Server Then myDatabase.DeleteHistoryPriceList(hp.GoodsLabel, False)
    End Sub

    Private Sub DeletedPersonnel(ByVal sender As Object, ByVal per As Database.Personnel) Handles ccc.DeletedPersonnel
        If Config.Mode = Connect.Server Then myDatabase.DeletePersonnel(per, False)
    End Sub

    Private Sub DeletedSupplier(ByVal sender As Object, ByVal sup As Database.Supplier) Handles ccc.DeletedSupplier
        If Config.Mode = Connect.Server Then myDatabase.DeleteSupplier(sup, False)
    End Sub

    Private Sub ccc_ReceiveServerName(ByVal sender As Object, ByVal Name As String) Handles ccc.ReceiveServerName
        Client.Save(ClientPath)
    End Sub

    Private Sub ccc_Account_LogIn(ByVal sender As Object, ByVal result As Database.LoginResult) Handles myDatabase.Account_Logout, myDatabase.Account_LogIn
        CurrentAccess = sender
        CurrentUser = result.User
    End Sub

    Private Sub CurrentAccess_ChangedStockMove(ByVal sender As Object, ByVal data As Database.StockMove) Handles CurrentAccess.ChangedStockMove
        Dim goods As Goods = Nothing
        If data.Action = StockMove.Type.Receiving And data.DestineShop = CurrentAccess.Name Then
            Try
                goods = Client(data.SourceShop).GetGoods(data.GoodsLabel)
            Catch
            End Try
            ShowNotify(data.SourceShop & " 已調出商品" & vbCrLf & goods.Name & " x " & data.Number)
        End If

    End Sub

    Public Function GetMain() As Form
        Return My.Application.OpenForms("winMain")
        'Return My.Forms.winMain ' (winMain.Name)(0)
    End Function

    Private Sub CurrentAccess_CreatedStockMove(ByVal sender As Object, ByVal data As Database.StockMove) Handles CurrentAccess.CreatedStockMove

        Dim goods As Goods = Nothing
        If data.Action = StockMove.Type.Request And data.SourceShop = CurrentAccess.Name Then
            goods = CurrentAccess.GetGoods(data.GoodsLabel)
            ShowNotify(data.DestineShop & " 申請調貨" & vbCrLf & goods.Name & " x " & data.Number)
        ElseIf data.Action = StockMove.Type.Receiving And data.DestineShop = CurrentAccess.Name Then
            Try
                goods = Client(data.SourceShop).GetGoods(data.GoodsLabel)
            Catch
            End Try
            ShowNotify(data.SourceShop & " 已調出商品" & vbCrLf & goods.Name & " x " & data.Number)
        End If
    End Sub


    Dim ShowNotifyHandler As New Action(Of String)(AddressOf ShowNotify)
    Private Sub ShowNotify(ByVal msg As String)
        If GetMain().InvokeRequired Then
            GetMain().Invoke(ShowNotifyHandler, msg)
            Exit Sub
        End If
        winNotify.Show(msg)
    End Sub

End Module
