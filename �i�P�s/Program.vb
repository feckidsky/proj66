Imports 進銷存.Database.StructureBase

Public Module Program
    Public DB As New Database.Access

    Structure LoginResult
        Dim State As LoginState
        Dim msg As String

        Sub New(ByVal state As LoginState, ByVal msg As String)
            Me.State = state : Me.msg = msg
        End Sub
    End Structure

    Enum LoginState
        IdError = 0
        PasswordError = 1
        Success = 2
    End Enum

    Public CurrentUser As Database.Personnel = Database.Personnel.Guest

    Event Account_Logout(ByVal personnel As Database.Personnel, ByVal Message As String)
    Event Account_LogIn(ByVal personnel As Database.Personnel, ByVal Message As String)
    Public SystemTitle As String = "進銷存管理系統"
   
    Public Sub InitialProgram()
        'Dim d As OleDb.OleDbConnection = DB.File.ConnectBase(DB.File.BasePath)
        'DB.File.CreateTable(Sales.Table, Sales.ToColumns, d)
        'DB.File.CreateTable(SalesGoods.Table, SalesGoods.ToColumns, d)
        'DB.AddBase(Personnel.Administrator)

        LogOut(False)

        LogIn("kidsky1", "3883", False)
        'Dim admin As Personnel = DB.GetPersonnelByID("Administrator")
        'LogIn(admin.ID, admin.Password, False)
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

    Public Function LogIn(ByVal ID As String, ByVal Password As String, Optional ByVal TriggerEvent As Boolean = True) As LoginResult

        Dim result As LoginResult
        Dim user As Personnel = DB.GetPersonnelByID(ID)
        If user.IsNull() Then
            result = New LoginResult(LoginState.IdError, "帳號不存在!")
        ElseIf user.Password <> Password Then
            result = New LoginResult(LoginState.PasswordError, "密碼錯誤!")
        Else
            result = New LoginResult(LoginState.Success, "登入成功!")
            CurrentUser = user
        End If

        If TriggerEvent Then RaiseEvent Account_LogIn(user, result.msg)
        Return result
    End Function

    Public Sub LogOut(Optional ByVal TriggerEvent As Boolean = True)
        CurrentUser = Database.Personnel.Guest
        If TriggerEvent Then RaiseEvent Account_Logout(CurrentUser, "已經登出!")
    End Sub

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

    Public Function GetNewMobile() As Mobile
        Dim data As Mobile = Nothing
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

    Public Function GetNewOrder() As Order
        Dim data As Order = Nothing
        data.Label = "O" & Now.ToString("yyMMddHHmmss")
        data.Date = Now
        Return data
    End Function
End Module
