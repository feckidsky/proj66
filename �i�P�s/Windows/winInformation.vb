Public Class winInformation

    Structure Info
        Dim Sales As Single
        Dim Profit As Single
        Dim Cash As Single
        Dim Card As Single
        Sub New(ByVal Sales As Single, ByVal Profit As Single, ByVal Cash As Single, ByVal Card As Single)
            Me.Sales = Sales : Me.Profit = Profit : Me.Cash = Cash : Me.Card = Card
        End Sub
    End Structure

    Private Sub winInformation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim infoYesterday As Info = GetInfo(Today.AddDays(-1), Today.AddSeconds(-1))
        lbYesterdayCash.Text = infoYesterday.Cash
        lbYesterdayProfit.Text = infoYesterday.Profit
        lbYesterdaySalesVolume.Text = infoYesterday.Sales
        lbYesterdayCard.Text = infoYesterday.Card

        Dim infoToday As Info = GetInfo(Today, Today.AddDays(1).AddSeconds(-1))
        lbTodayCash.Text = infoToday.Cash
        lbTodayProfit.Text = infoToday.Profit
        lbTodaySales.Text = infoToday.Sales
        lbTodayCard.Text = infoToday.Card

        Dim LastMonth As Date = New Date(Now.AddMonths(-1).Year, Now.AddMonths(-1).Month, 1)
        Dim infoLastMonth As Info = GetInfo(LastMonth, LastMonth.AddMonths(1).AddSeconds(-1))
        lbLastMonthCash.Text = infoLastMonth.Cash
        lbLastMonthProfit.Text = infoLastMonth.Profit
        lbLastMonthSales.Text = infoLastMonth.Sales
        lbLastMonthCard.Text = infoLastMonth.Card

        Dim Month As Date = New Date(Now.Year, Now.Month, 1)
        Dim infoMonth As Info = GetInfo(Month, Month.AddMonths(1).AddSeconds(-1))
        lbTheMonthCash.Text = infoMonth.Cash
        lbTheMonthProfit.Text = infoMonth.Profit
        lbTheMonthSales.Text = infoMonth.Sales
        lbTheMonthCard.Text = infoMonth.Card

        dtpStart.Value = Today
        dtpEnd.Value = Today
        UpdateUserDefInfo()
    End Sub

    Public Sub UpdateUserDefInfo()
        Dim info As Info = GetInfo(dtpStart.Value.Date, dtpEnd.Value.Date.AddDays(1).AddSeconds(-1))
        lbUserCash.Text = info.Cash
        lbUserProfit.Text = info.Profit
        lbUserSales.Text = info.Sales
        lbUserCard.Text = info.Card
    End Sub


    Public Function GetInfo(ByVal St As Date, ByVal Ed As Date) As Info

        Dim dt As DataTable = DB.GetOrderListWithContract(St, Ed)

        Dim Deposit As Single = 0
        For Each row As DataRow In dt.Rows
            Deposit += row.Item("訂金")
        Next

        dt = DB.GetSalesListWithContract(St, Ed, Database.Access.GetSalesListType.Sales)

        Dim Cash As Single = 0
        For Each row As DataRow In dt.Select("付款方式=" & Val(Database.TypeOfPayment.Cash))
            Cash += row.Item("金額") - row.Item("訂金")
        Next

        Dim Card As Single = 0
        For Each row As DataRow In dt.Select("付款方式=" & Val(Database.TypeOfPayment.Card))
            Card += row.Item("金額") - row.Item("訂金")
        Next


        Dim Profit As Single = 0
        Dim SalesVolume As Single = 0
        For Each row As DataRow In dt.Rows
            Profit += row.Item("利潤")
            SalesVolume += row.Item("金額")
        Next

        Return New Info(SalesVolume, Profit, Cash + Deposit, Card)
    End Function


    Private Sub dtpStart_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpStart.ValueChanged, dtpEnd.ValueChanged
        UpdateUserDefInfo()
    End Sub

    Private Sub btClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btClose.Click
        Me.Close()
    End Sub
End Class