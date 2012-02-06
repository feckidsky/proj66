Imports 進銷存.Database.DatabaseType
Imports 進銷存.Database

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
        Shared ReadOnly Property DefaultConfig()
            Get
                Dim def As SystemOptional
                def.OrderBackcolor = Color.LightPink.ToArgb
                def.SalesBackColor = Color.LightGreen.ToArgb
                def.Mode = Connect.Client
                def.ServerName = My.Computer.Name
                def.ServerPort = 3600
                Return def
            End Get
        End Property

    End Structure

#End Region

    Public myDatabase As New Database.Access("本機資料庫")

    Public Server As New Database.AccessServer
    Public WithEvents Client As New Database.AccessClientMenage()


    Public Config As SystemOptional
    Public ConfigPath As String = My.Application.Info.DirectoryPath & "\Config.xml"
    Public ClientPath As String = My.Application.Info.DirectoryPath & "\Client.xml"
    Public SalesVisiblePath As String = My.Application.Info.DirectoryPath & "\SalesVisible.xml"

    Public CurrentUser As Database.Personnel = Database.Personnel.Guest


    Public SystemTitle As String = "進銷存管理系統"

    Public Sub InitialProgram()
        ErrorLog.Enable()

        ConfigLoad()

        If Config.Mode = Connect.Server Then
            Server.Access = myDatabase
            Server.Port = Config.ServerPort
            Server.Name = Config.ServerName
            Server.Open()
        End If


        Dim lstClient As New List(Of Database.Access)
        If Config.Mode = Connect.Server Then lstClient.Add(myDatabase)
        lstClient.AddRange(Database.AccessClientMenage.Load(ClientPath))
        Client.Client = lstClient.ToArray()
        Client.StartConnect()


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


    Public Sub UpdateDatabase()
        If Config.Mode = Connect.Client Then Exit Sub
        Dim d As OleDb.OleDbConnection = Database.Access.ConnectBase(myDatabase.BasePath)
        Database.Access.DeleteTable("Mobile", d)
        Database.Access.CreateTable(Contract.Table, Contract.ToColumns, d)
        Database.Access.CreateTable(SalesContract.Table, SalesContract.ToColumns, d)
        Database.Access.CreateTable(OrderGoods.Table, OrderGoods.ToColumns, d)
        Database.Access.CreateTable(HistoryPrice.Table, HistoryPrice.ToColumns(), d)
        myDatabase.DeleteColumn(Stock.Table, "Price")
        myDatabase.AddColumn(Supplier.Table, "Modify", Database.DBTypeDate)
        myDatabase.AddColumn(Customer.Table, "Modify", Database.DBTypeDate)
        myDatabase.AddColumn(Personnel.Table, "Modify", Database.DBTypeDate)
        myDatabase.AddColumn(Goods.Table, "Modify", Database.DBTypeDate)
        myDatabase.AddColumn(Contract.Table, "Modify", Database.DBTypeDate)
    End Sub

    Public Sub ConfigLoad()
        Config = Code.Load(Of SystemOptional)(ConfigPath, SystemOptional.DefaultConfig)
    End Sub

    Public Sub ConfigSave()
        Code.Save(Config, ConfigPath)
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

        Public Shared Function SerializeWithZIP(ByVal obj As Object) As String
            Return Zip(Serialize(obj))
        End Function

        Public Shared Function DeserializeWithUnzip(Of T)(ByVal ZipText As String, ByVal Type As Type) As T
            Return Deserialize(Of T)(Unzip(ZipText))
        End Function

        Public Shared Function DeserializeWithUnzip(Of T)(ByVal ZipText As String) As T
            Return Deserialize(Of T)(Unzip(ZipText))
        End Function

        Public Shared Function Serialize(ByVal Obj As Object) As String
            Dim ser As Xml.Serialization.XmlSerializer = New Xml.Serialization.XmlSerializer(Obj.GetType)
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim writer As IO.StringWriter = New IO.StringWriter(sb)
            ser.Serialize(writer, Obj)
            Return sb.ToString()
            writer.Dispose()
        End Function

        Public Shared Function Deserialize(Of T)(ByVal Text As String) As T
            ''將取得的內容進行反序列化
            Dim mySerializer As Xml.Serialization.XmlSerializer = New Xml.Serialization.XmlSerializer(GetType(T)) 'GetType(SerializeData))
            Dim reader As New IO.StringReader(Text)
            Return mySerializer.Deserialize(reader)
            reader.Dispose()
        End Function

        Public Enum ZipMode
            Normal = 0
            ZIP = 1
        End Enum

        Public Shared Sub Save(Of T)(ByVal Data As T, ByVal FilePath As String, Optional ByVal Mode As ZipMode = ZipMode.Normal)
            Dim Dir As String = IO.Path.GetDirectoryName(FilePath)
            If Not IO.Directory.Exists(Dir) Then IO.Directory.CreateDirectory(Dir)
            Dim Text As String = Code.Serialize(Data)
            If Mode = ZipMode.ZIP Then Text = Code.Zip(Text)
            My.Computer.FileSystem.WriteAllText(FilePath, Text, False, System.Text.Encoding.Unicode)
        End Sub

        Public Shared Function Load(Of T)(ByVal FilePath As String, ByVal DefaultData As T, Optional ByVal Mode As ZipMode = ZipMode.Normal) As T
            If IO.File.Exists(FilePath) Then
                Dim Text As String = My.Computer.FileSystem.ReadAllText(FilePath, System.Text.Encoding.Unicode)
                If Mode = ZipMode.ZIP Then Text = Code.Unzip(Text)
                Return Code.Deserialize(Of T)(Text)
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



    Private Sub ConnectSuccess(ByVal sender As Object) Handles ccc.ConnectedSuccess
        If Config.Mode = Connect.Client Then Exit Sub
        SyncLock UpdateDataLock
            Try
                Dim client As Database.AccessClient = sender

                Dim sDT As DataTable = client.GetGoodsList()
                Dim myDT As DataTable = myDatabase.GetGoodsList()

                For Each r As DataRow In sDT.Rows
                    Select Case CompareModify(myDT, r("Label"), GetTime(r("Modify")))
                        Case Compare.MoreNew : myDatabase.ChangeGoods(Goods.GetFrom(r), False)
                        Case Compare.NoExist : myDatabase.AddGoods(Goods.GetFrom(r), False)
                    End Select
                Next

                sDT = client.GetPersonnelList
                myDT = myDatabase.GetPersonnelList
                For Each r As DataRow In sDT.Rows
                    Select Case CompareModify(myDT, r("Label"), GetTime(r("Modify")))
                        Case Compare.MoreNew : myDatabase.ChangePersonnel(Personnel.GetFrom(r), False)
                        Case Compare.NoExist : myDatabase.AddPersonnel(Personnel.GetFrom(r), False)
                    End Select
                Next

                sDT = client.GetCustomerList
                myDT = myDatabase.GetCustomerList
                For Each r As DataRow In sDT.Rows
                    Select Case CompareModify(myDT, r("Label"), GetTime(r("Modify")))
                        Case Compare.MoreNew : myDatabase.ChangeCustomer(Customer.GetFrom(r), False)
                        Case Compare.NoExist : myDatabase.AddCustomer(Customer.GetFrom(r), False)
                    End Select
                Next

                sDT = client.GetSupplierList
                myDT = myDatabase.GetSupplierList
                For Each r As DataRow In sDT.Rows
                    Select Case CompareModify(myDT, r("Label"), GetTime(r("Modify")))
                        Case Compare.MoreNew : myDatabase.ChangeSupplier(Supplier.GetFrom(r), False)
                        Case Compare.NoExist : myDatabase.AddSupplier(Supplier.GetFrom(r), False)
                    End Select
                Next

                sDT = client.GetContractList()
                myDT = myDatabase.GetContractList()
                For Each r As DataRow In sDT.Rows
                    Select Case CompareModify(myDT, r("Label"), GetTime(r("Modify")))
                        Case Compare.MoreNew : myDatabase.ChangeContract(Contract.GetFrom(r), False)
                        Case Compare.NoExist : myDatabase.AddContract(Contract.GetFrom(r), False)
                    End Select
                Next

                sDT = client.GetHistoryPriceList()
                myDT = myDatabase.GetHistoryPriceList()
                For Each r As DataRow In sDT.Rows
                    If CompareHistoryPrice(myDT, r("GoodsLabel"), GetTime(r("Time"))) = Compare.NoExist Then
                        myDatabase.AddHistoryPrice(HistoryPrice.GetFrom(r), False)
                    End If
                Next
            Catch

            End Try

        End SyncLock
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

    Private Sub ccc_Account_LogIn(ByVal sender As Object, ByVal result As Database.LoginResult) Handles ccc.Account_LogIn, ccc.Account_Logout
        CurrentUser = result.User
    End Sub

End Module
