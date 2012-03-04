Public Class winInformation

    Dim access As Database.Access

    Structure Info
        Dim Sales As Single
        Dim Profit As Single
        Dim Cash As Single
        Dim Card As Single
        Sub New(ByVal Sales As Single, ByVal Profit As Single, ByVal Cash As Single, ByVal Card As Single)
            Me.Sales = Sales : Me.Profit = Profit : Me.Cash = Cash : Me.Card = Card
        End Sub
    End Structure

    Public Overloads Sub ShowDialog(ByVal DB As Database.Access)
        access = DB
        MyBase.ShowDialog()
    End Sub
    Dim infoYesterday As Database.Access.SalesInformation
    Dim infoToday As Database.Access.SalesInformation
    Dim infoLastMonth As Database.Access.SalesInformation
    Dim infoMonth As Database.Access.SalesInformation
    Dim infoUser As Database.Access.SalesInformation

    Public Sub UpdateYesterday()
        lbYesterdayCash.Text = infoYesterday.Cash
        lbYesterdayProfit.Text = infoYesterday.Profit
        lbYesterdaySalesVolume.Text = infoYesterday.Sales
        lbYesterdayCard.Text = infoYesterday.Card
    End Sub

    Public Sub UpdateToday()
        lbTodayCash.Text = infoToday.Cash
        lbTodayProfit.Text = infoToday.Profit
        lbTodaySales.Text = infoToday.Sales
        lbTodayCard.Text = infoToday.Card
    End Sub

    Public Sub UpdateLastMonth()
        lbLastMonthCash.Text = infoLastMonth.Cash
        lbLastMonthProfit.Text = infoLastMonth.Profit
        lbLastMonthSales.Text = infoLastMonth.Sales
        lbLastMonthCard.Text = infoLastMonth.Card
    End Sub

    Public Sub UpdateTheMonth()
        lbTheMonthCash.Text = infoMonth.Cash
        lbTheMonthProfit.Text = infoMonth.Profit
        lbTheMonthSales.Text = infoMonth.Sales
        lbTheMonthCard.Text = infoMonth.Card
    End Sub

    Public Sub UpdateUserInfo()
        lbUserCash.Text = infoUser.Cash
        lbUserProfit.Text = infoUser.Profit
        lbUserSales.Text = infoUser.Sales
        lbUserCard.Text = infoUser.Card
    End Sub

    Private Sub winInformation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim dialog As New ProgressDialog
        dialog.Thread = New Threading.Thread(New Threading.ParameterizedThreadStart(AddressOf UpdateSalesInformation))
        dialog.Start("讀取銷售資訊")
     
    End Sub

    Public Sub UpdateSalesInformation(ByVal progress As Database.Access.Progress)

        progress.Report("讀取昨日資訊", 15)
        infoYesterday = access.GetSalesInformation(Today.AddDays(-1), Today.AddSeconds(-1))
        Me.Invoke(New Action(AddressOf UpdateYesterday))

        progress.Report("讀取今日資訊", 30)
        infoToday = access.GetSalesInformation(Today, Today.AddDays(1).AddSeconds(-1))
        Me.Invoke(New Action(AddressOf UpdateToday))

        progress.Report("讀取上個資訊", 45)
        Dim LastMonth As Date = New Date(Now.AddMonths(-1).Year, Now.AddMonths(-1).Month, 1)
        infoLastMonth = access.GetSalesInformation(LastMonth, LastMonth.AddMonths(1).AddSeconds(-1))
        Me.Invoke(New Action(AddressOf UpdateLastMonth))

        progress.Report("讀取本月資訊", 60)
        Dim Month As Date = New Date(Now.Year, Now.Month, 1)
        infoMonth = access.GetSalesInformation(Month, Month.AddMonths(1).AddSeconds(-1))
        Me.Invoke(New Action(AddressOf UpdateTheMonth))

        progress.Report("讀取自訂日期資訊", 75)
        Dim st As Date = Me.Invoke(New Func(Of Date)(AddressOf GetStartTime))
        Dim ed As Date = Me.Invoke(New Func(Of Date)(AddressOf GetEndTime))
        infoUser = access.GetSalesInformation(st, ed.AddDays(1).AddSeconds(-1))
        Me.Invoke(New Action(AddressOf UpdateUserInfo))
        progress.Finish()
    End Sub

    Function GetStartTime() As Date
        Return dtpStart.Value.Date
    End Function

    Public Function GetEndTime() As Date
        Return dtpEnd.Value.Date
    End Function


    Public Sub BeginUpdateUserDefInfo()
        Dim dialog As New ProgressDialog
        dialog.Thread = New Threading.Thread(New Threading.ParameterizedThreadStart(AddressOf UpdateUserDefInfo))
        dialog.Start("取得使用者自訂資訊")

    End Sub

    Public Sub UpdateUserDefInfo(ByVal progress As Database.Access.Progress)

        Dim st As Date = Me.Invoke(New Func(Of Date)(AddressOf GetStartTime))
        Dim ed As Date = Me.Invoke(New Func(Of Date)(AddressOf GetEndTime))
        infoUser = access.GetSalesInformation(dtpStart.Value.Date, dtpEnd.Value.Date.AddDays(1).AddSeconds(-1))

        Me.Invoke(New Action(AddressOf UpdateUserInfo))
        progress.Finish()
    End Sub



    Private Sub dtpStart_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpStart.ValueChanged, dtpEnd.ValueChanged
        BeginUpdateUserDefInfo()
    End Sub

    Private Sub btClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btClose.Click
        Me.Close()
    End Sub
End Class