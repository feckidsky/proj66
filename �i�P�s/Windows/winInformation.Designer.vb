<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class winInformation
    Inherits System.Windows.Forms.Form

    'Form 覆寫 Dispose 以清除元件清單。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    '為 Windows Form 設計工具的必要項
    Private components As System.ComponentModel.IContainer

    '注意: 以下為 Windows Form 設計工具所需的程序
    '可以使用 Windows Form 設計工具進行修改。
    '請不要使用程式碼編輯器進行修改。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.lbYesterdaySalesVolume = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lbYesterdayProfit = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.lbYesterdayCash = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lbYesterdayCard = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.lbUserCard = New System.Windows.Forms.Label
        Me.lbUserCash = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.lbUserProfit = New System.Windows.Forms.Label
        Me.dtpEnd = New System.Windows.Forms.DateTimePicker
        Me.lbUserSales = New System.Windows.Forms.Label
        Me.dtpStart = New System.Windows.Forms.DateTimePicker
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.lbTodayCard = New System.Windows.Forms.Label
        Me.lbTodayCash = New System.Windows.Forms.Label
        Me.lbTodayProfit = New System.Windows.Forms.Label
        Me.lbTodaySales = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.lbLastMonthCard = New System.Windows.Forms.Label
        Me.lbLastMonthCash = New System.Windows.Forms.Label
        Me.lbLastMonthProfit = New System.Windows.Forms.Label
        Me.lbLastMonthSales = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.lbTheMonthCard = New System.Windows.Forms.Label
        Me.lbTheMonthCash = New System.Windows.Forms.Label
        Me.lbTheMonthProfit = New System.Windows.Forms.Label
        Me.lbTheMonthSales = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.btClose = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label13 = New System.Windows.Forms.Label
        Me.lbYesterdayPrepay = New System.Windows.Forms.Label
        Me.lbTodayPrepay = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.lbLastMonthPrepay = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.lbTheMonthPrepay = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.lbUserPrepay = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(24, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "銷售額:"
        '
        'lbYesterdaySalesVolume
        '
        Me.lbYesterdaySalesVolume.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbYesterdaySalesVolume.Location = New System.Drawing.Point(77, 21)
        Me.lbYesterdaySalesVolume.Name = "lbYesterdaySalesVolume"
        Me.lbYesterdaySalesVolume.Size = New System.Drawing.Size(70, 12)
        Me.lbYesterdaySalesVolume.TabIndex = 1
        Me.lbYesterdaySalesVolume.Text = "0"
        Me.lbYesterdaySalesVolume.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 12)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "利潤:"
        '
        'lbYesterdayProfit
        '
        Me.lbYesterdayProfit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbYesterdayProfit.Location = New System.Drawing.Point(77, 69)
        Me.lbYesterdayProfit.Name = "lbYesterdayProfit"
        Me.lbYesterdayProfit.Size = New System.Drawing.Size(70, 12)
        Me.lbYesterdayProfit.TabIndex = 1
        Me.lbYesterdayProfit.Text = "0"
        Me.lbYesterdayProfit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(24, 98)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(32, 12)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "現金:"
        '
        'lbYesterdayCash
        '
        Me.lbYesterdayCash.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbYesterdayCash.Location = New System.Drawing.Point(77, 95)
        Me.lbYesterdayCash.Name = "lbYesterdayCash"
        Me.lbYesterdayCash.Size = New System.Drawing.Size(70, 12)
        Me.lbYesterdayCash.TabIndex = 1
        Me.lbYesterdayCash.Text = "0"
        Me.lbYesterdayCash.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lbYesterdayPrepay)
        Me.GroupBox1.Controls.Add(Me.lbYesterdayCard)
        Me.GroupBox1.Controls.Add(Me.lbYesterdayCash)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.lbYesterdayProfit)
        Me.GroupBox1.Controls.Add(Me.lbYesterdaySalesVolume)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(185, 141)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "昨日資訊"
        '
        'lbYesterdayCard
        '
        Me.lbYesterdayCard.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbYesterdayCard.Location = New System.Drawing.Point(77, 120)
        Me.lbYesterdayCard.Name = "lbYesterdayCard"
        Me.lbYesterdayCard.Size = New System.Drawing.Size(70, 12)
        Me.lbYesterdayCard.TabIndex = 1
        Me.lbYesterdayCard.Text = "0"
        Me.lbYesterdayCard.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(24, 123)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(32, 12)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "刷卡:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lbUserPrepay)
        Me.GroupBox2.Controls.Add(Me.Label26)
        Me.GroupBox2.Controls.Add(Me.lbUserCard)
        Me.GroupBox2.Controls.Add(Me.lbUserCash)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.lbUserProfit)
        Me.GroupBox2.Controls.Add(Me.dtpEnd)
        Me.GroupBox2.Controls.Add(Me.lbUserSales)
        Me.GroupBox2.Controls.Add(Me.dtpStart)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.Label25)
        Me.GroupBox2.Controls.Add(Me.Label24)
        Me.GroupBox2.Controls.Add(Me.Label23)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 332)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(397, 122)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "指定時間"
        '
        'lbUserCard
        '
        Me.lbUserCard.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbUserCard.Location = New System.Drawing.Point(290, 84)
        Me.lbUserCard.Name = "lbUserCard"
        Me.lbUserCard.Size = New System.Drawing.Size(70, 12)
        Me.lbUserCard.TabIndex = 1
        Me.lbUserCard.Text = "0"
        Me.lbUserCard.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbUserCash
        '
        Me.lbUserCash.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbUserCash.Location = New System.Drawing.Point(290, 63)
        Me.lbUserCash.Name = "lbUserCash"
        Me.lbUserCash.Size = New System.Drawing.Size(70, 12)
        Me.lbUserCash.TabIndex = 1
        Me.lbUserCash.Text = "0"
        Me.lbUserCash.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(174, 28)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(11, 12)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "~"
        '
        'lbUserProfit
        '
        Me.lbUserProfit.Location = New System.Drawing.Point(77, 104)
        Me.lbUserProfit.Name = "lbUserProfit"
        Me.lbUserProfit.Size = New System.Drawing.Size(70, 12)
        Me.lbUserProfit.TabIndex = 1
        Me.lbUserProfit.Text = "0"
        Me.lbUserProfit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpEnd
        '
        Me.dtpEnd.Location = New System.Drawing.Point(207, 21)
        Me.dtpEnd.Name = "dtpEnd"
        Me.dtpEnd.Size = New System.Drawing.Size(139, 22)
        Me.dtpEnd.TabIndex = 1
        '
        'lbUserSales
        '
        Me.lbUserSales.Location = New System.Drawing.Point(77, 63)
        Me.lbUserSales.Name = "lbUserSales"
        Me.lbUserSales.Size = New System.Drawing.Size(70, 12)
        Me.lbUserSales.TabIndex = 1
        Me.lbUserSales.Text = "0"
        Me.lbUserSales.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpStart
        '
        Me.dtpStart.Location = New System.Drawing.Point(21, 21)
        Me.dtpStart.Name = "dtpStart"
        Me.dtpStart.Size = New System.Drawing.Size(139, 22)
        Me.dtpStart.TabIndex = 1
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(210, 84)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(32, 12)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "刷卡:"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(210, 63)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(32, 12)
        Me.Label25.TabIndex = 0
        Me.Label25.Text = "現金:"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(24, 104)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(32, 12)
        Me.Label24.TabIndex = 0
        Me.Label24.Text = "利潤:"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(24, 63)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(44, 12)
        Me.Label23.TabIndex = 0
        Me.Label23.Text = "銷售額:"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lbTodayPrepay)
        Me.GroupBox3.Controls.Add(Me.Label18)
        Me.GroupBox3.Controls.Add(Me.lbTodayCard)
        Me.GroupBox3.Controls.Add(Me.lbTodayCash)
        Me.GroupBox3.Controls.Add(Me.lbTodayProfit)
        Me.GroupBox3.Controls.Add(Me.lbTodaySales)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Location = New System.Drawing.Point(224, 12)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(185, 141)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "今日資訊"
        '
        'lbTodayCard
        '
        Me.lbTodayCard.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbTodayCard.Location = New System.Drawing.Point(78, 120)
        Me.lbTodayCard.Name = "lbTodayCard"
        Me.lbTodayCard.Size = New System.Drawing.Size(70, 12)
        Me.lbTodayCard.TabIndex = 1
        Me.lbTodayCard.Text = "0"
        Me.lbTodayCard.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbTodayCash
        '
        Me.lbTodayCash.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbTodayCash.Location = New System.Drawing.Point(78, 95)
        Me.lbTodayCash.Name = "lbTodayCash"
        Me.lbTodayCash.Size = New System.Drawing.Size(70, 12)
        Me.lbTodayCash.TabIndex = 1
        Me.lbTodayCash.Text = "0"
        Me.lbTodayCash.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbTodayProfit
        '
        Me.lbTodayProfit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbTodayProfit.Location = New System.Drawing.Point(78, 69)
        Me.lbTodayProfit.Name = "lbTodayProfit"
        Me.lbTodayProfit.Size = New System.Drawing.Size(70, 12)
        Me.lbTodayProfit.TabIndex = 1
        Me.lbTodayProfit.Text = "0"
        Me.lbTodayProfit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbTodaySales
        '
        Me.lbTodaySales.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbTodaySales.Location = New System.Drawing.Point(78, 21)
        Me.lbTodaySales.Name = "lbTodaySales"
        Me.lbTodaySales.Size = New System.Drawing.Size(70, 12)
        Me.lbTodaySales.TabIndex = 1
        Me.lbTodaySales.Text = "0"
        Me.lbTodaySales.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(24, 120)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(32, 12)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "刷卡:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(24, 95)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(32, 12)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "現金:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(24, 69)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(32, 12)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "利潤:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(24, 21)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(44, 12)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "銷售額:"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.lbLastMonthPrepay)
        Me.GroupBox4.Controls.Add(Me.Label19)
        Me.GroupBox4.Controls.Add(Me.lbLastMonthCard)
        Me.GroupBox4.Controls.Add(Me.lbLastMonthCash)
        Me.GroupBox4.Controls.Add(Me.lbLastMonthProfit)
        Me.GroupBox4.Controls.Add(Me.lbLastMonthSales)
        Me.GroupBox4.Controls.Add(Me.Label7)
        Me.GroupBox4.Controls.Add(Me.Label14)
        Me.GroupBox4.Controls.Add(Me.Label15)
        Me.GroupBox4.Controls.Add(Me.Label16)
        Me.GroupBox4.Location = New System.Drawing.Point(12, 171)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(185, 147)
        Me.GroupBox4.TabIndex = 2
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "上月資訊"
        '
        'lbLastMonthCard
        '
        Me.lbLastMonthCard.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbLastMonthCard.Location = New System.Drawing.Point(77, 123)
        Me.lbLastMonthCard.Name = "lbLastMonthCard"
        Me.lbLastMonthCard.Size = New System.Drawing.Size(70, 12)
        Me.lbLastMonthCard.TabIndex = 1
        Me.lbLastMonthCard.Text = "0"
        Me.lbLastMonthCard.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbLastMonthCash
        '
        Me.lbLastMonthCash.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbLastMonthCash.Location = New System.Drawing.Point(77, 99)
        Me.lbLastMonthCash.Name = "lbLastMonthCash"
        Me.lbLastMonthCash.Size = New System.Drawing.Size(70, 12)
        Me.lbLastMonthCash.TabIndex = 1
        Me.lbLastMonthCash.Text = "0"
        Me.lbLastMonthCash.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbLastMonthProfit
        '
        Me.lbLastMonthProfit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbLastMonthProfit.Location = New System.Drawing.Point(77, 75)
        Me.lbLastMonthProfit.Name = "lbLastMonthProfit"
        Me.lbLastMonthProfit.Size = New System.Drawing.Size(70, 12)
        Me.lbLastMonthProfit.TabIndex = 1
        Me.lbLastMonthProfit.Text = "0"
        Me.lbLastMonthProfit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbLastMonthSales
        '
        Me.lbLastMonthSales.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbLastMonthSales.Location = New System.Drawing.Point(77, 27)
        Me.lbLastMonthSales.Name = "lbLastMonthSales"
        Me.lbLastMonthSales.Size = New System.Drawing.Size(70, 12)
        Me.lbLastMonthSales.TabIndex = 1
        Me.lbLastMonthSales.Text = "0"
        Me.lbLastMonthSales.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(24, 123)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(32, 12)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "刷卡:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(24, 99)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(32, 12)
        Me.Label14.TabIndex = 0
        Me.Label14.Text = "現金:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(24, 75)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(32, 12)
        Me.Label15.TabIndex = 0
        Me.Label15.Text = "利潤:"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(24, 27)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(44, 12)
        Me.Label16.TabIndex = 0
        Me.Label16.Text = "銷售額:"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.lbTheMonthPrepay)
        Me.GroupBox5.Controls.Add(Me.Label27)
        Me.GroupBox5.Controls.Add(Me.lbTheMonthCard)
        Me.GroupBox5.Controls.Add(Me.lbTheMonthCash)
        Me.GroupBox5.Controls.Add(Me.lbTheMonthProfit)
        Me.GroupBox5.Controls.Add(Me.lbTheMonthSales)
        Me.GroupBox5.Controls.Add(Me.Label11)
        Me.GroupBox5.Controls.Add(Me.Label20)
        Me.GroupBox5.Controls.Add(Me.Label21)
        Me.GroupBox5.Controls.Add(Me.Label22)
        Me.GroupBox5.Location = New System.Drawing.Point(224, 171)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(185, 147)
        Me.GroupBox5.TabIndex = 2
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "本月資訊"
        '
        'lbTheMonthCard
        '
        Me.lbTheMonthCard.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbTheMonthCard.Location = New System.Drawing.Point(78, 123)
        Me.lbTheMonthCard.Name = "lbTheMonthCard"
        Me.lbTheMonthCard.Size = New System.Drawing.Size(70, 12)
        Me.lbTheMonthCard.TabIndex = 1
        Me.lbTheMonthCard.Text = "0"
        Me.lbTheMonthCard.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbTheMonthCash
        '
        Me.lbTheMonthCash.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbTheMonthCash.Location = New System.Drawing.Point(78, 99)
        Me.lbTheMonthCash.Name = "lbTheMonthCash"
        Me.lbTheMonthCash.Size = New System.Drawing.Size(70, 12)
        Me.lbTheMonthCash.TabIndex = 1
        Me.lbTheMonthCash.Text = "0"
        Me.lbTheMonthCash.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbTheMonthProfit
        '
        Me.lbTheMonthProfit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbTheMonthProfit.Location = New System.Drawing.Point(78, 75)
        Me.lbTheMonthProfit.Name = "lbTheMonthProfit"
        Me.lbTheMonthProfit.Size = New System.Drawing.Size(70, 12)
        Me.lbTheMonthProfit.TabIndex = 1
        Me.lbTheMonthProfit.Text = "0"
        Me.lbTheMonthProfit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbTheMonthSales
        '
        Me.lbTheMonthSales.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbTheMonthSales.Location = New System.Drawing.Point(78, 27)
        Me.lbTheMonthSales.Name = "lbTheMonthSales"
        Me.lbTheMonthSales.Size = New System.Drawing.Size(70, 12)
        Me.lbTheMonthSales.TabIndex = 1
        Me.lbTheMonthSales.Text = "0"
        Me.lbTheMonthSales.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(24, 123)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(32, 12)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "刷卡:"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(24, 99)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(32, 12)
        Me.Label20.TabIndex = 0
        Me.Label20.Text = "現金:"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(24, 75)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(32, 12)
        Me.Label21.TabIndex = 0
        Me.Label21.Text = "利潤:"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(24, 27)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(44, 12)
        Me.Label22.TabIndex = 0
        Me.Label22.Text = "銷售額:"
        '
        'btClose
        '
        Me.btClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btClose.Location = New System.Drawing.Point(344, 468)
        Me.btClose.Name = "btClose"
        Me.btClose.Size = New System.Drawing.Size(75, 23)
        Me.btClose.TabIndex = 5
        Me.btClose.Text = "關閉"
        Me.btClose.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(12, 468)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "舊版算法"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(24, 46)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(44, 12)
        Me.Label13.TabIndex = 2
        Me.Label13.Text = "預付額:"
        '
        'lbYesterdayPrepay
        '
        Me.lbYesterdayPrepay.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbYesterdayPrepay.Location = New System.Drawing.Point(77, 46)
        Me.lbYesterdayPrepay.Name = "lbYesterdayPrepay"
        Me.lbYesterdayPrepay.Size = New System.Drawing.Size(70, 12)
        Me.lbYesterdayPrepay.TabIndex = 3
        Me.lbYesterdayPrepay.Text = "0"
        Me.lbYesterdayPrepay.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbTodayPrepay
        '
        Me.lbTodayPrepay.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbTodayPrepay.Location = New System.Drawing.Point(77, 46)
        Me.lbTodayPrepay.Name = "lbTodayPrepay"
        Me.lbTodayPrepay.Size = New System.Drawing.Size(70, 12)
        Me.lbTodayPrepay.TabIndex = 5
        Me.lbTodayPrepay.Text = "0"
        Me.lbTodayPrepay.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(24, 46)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(44, 12)
        Me.Label18.TabIndex = 4
        Me.Label18.Text = "預付額:"
        '
        'lbLastMonthPrepay
        '
        Me.lbLastMonthPrepay.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbLastMonthPrepay.Location = New System.Drawing.Point(77, 51)
        Me.lbLastMonthPrepay.Name = "lbLastMonthPrepay"
        Me.lbLastMonthPrepay.Size = New System.Drawing.Size(70, 12)
        Me.lbLastMonthPrepay.TabIndex = 5
        Me.lbLastMonthPrepay.Text = "0"
        Me.lbLastMonthPrepay.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(24, 51)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(44, 12)
        Me.Label19.TabIndex = 4
        Me.Label19.Text = "預付額:"
        '
        'lbTheMonthPrepay
        '
        Me.lbTheMonthPrepay.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbTheMonthPrepay.Location = New System.Drawing.Point(77, 51)
        Me.lbTheMonthPrepay.Name = "lbTheMonthPrepay"
        Me.lbTheMonthPrepay.Size = New System.Drawing.Size(70, 12)
        Me.lbTheMonthPrepay.TabIndex = 7
        Me.lbTheMonthPrepay.Text = "0"
        Me.lbTheMonthPrepay.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(24, 51)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(44, 12)
        Me.Label27.TabIndex = 6
        Me.Label27.Text = "預付額:"
        '
        'lbUserPrepay
        '
        Me.lbUserPrepay.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbUserPrepay.Location = New System.Drawing.Point(77, 84)
        Me.lbUserPrepay.Name = "lbUserPrepay"
        Me.lbUserPrepay.Size = New System.Drawing.Size(70, 12)
        Me.lbUserPrepay.TabIndex = 7
        Me.lbUserPrepay.Text = "0"
        Me.lbUserPrepay.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(24, 84)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(44, 12)
        Me.Label26.TabIndex = 6
        Me.Label26.Text = "預付額:"
        '
        'winInformation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(432, 503)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btClose)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "winInformation"
        Me.Text = "資訊"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lbYesterdaySalesVolume As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lbYesterdayProfit As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lbYesterdayCash As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lbTodayCash As System.Windows.Forms.Label
    Friend WithEvents lbTodayProfit As System.Windows.Forms.Label
    Friend WithEvents lbTodaySales As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lbUserCash As System.Windows.Forms.Label
    Friend WithEvents lbUserProfit As System.Windows.Forms.Label
    Friend WithEvents lbUserSales As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents lbLastMonthCash As System.Windows.Forms.Label
    Friend WithEvents lbLastMonthProfit As System.Windows.Forms.Label
    Friend WithEvents lbLastMonthSales As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents lbTheMonthCash As System.Windows.Forms.Label
    Friend WithEvents lbTheMonthProfit As System.Windows.Forms.Label
    Friend WithEvents lbTheMonthSales As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents btClose As System.Windows.Forms.Button
    Friend WithEvents lbYesterdayCard As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lbTodayCard As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lbLastMonthCard As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lbTheMonthCard As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lbUserCard As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents lbYesterdayPrepay As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lbUserPrepay As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents lbTodayPrepay As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents lbLastMonthPrepay As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lbTheMonthPrepay As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
End Class
