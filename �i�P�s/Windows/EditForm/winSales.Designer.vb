﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class winSales
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtNote = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtOrderDate = New System.Windows.Forms.TextBox
        Me.txtLabel = New System.Windows.Forms.TextBox
        Me.lbRealTotal = New System.Windows.Forms.Label
        Me.lbTotal = New System.Windows.Forms.Label
        Me.btOrder = New System.Windows.Forms.Button
        Me.cbPayMode = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtCustomer = New System.Windows.Forms.TextBox
        Me.txtPersonnel = New System.Windows.Forms.TextBox
        Me.btResetPersonnel = New System.Windows.Forms.Button
        Me.btResetCustomer = New System.Windows.Forms.Button
        Me.txtSalesDate = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btSales = New System.Windows.Forms.Button
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.tpOrder = New System.Windows.Forms.TabPage
        Me.btDeleteOrderItem = New System.Windows.Forms.Button
        Me.btAddOrderItem = New System.Windows.Forms.Button
        Me.dgOrderList = New System.Windows.Forms.DataGridView
        Me.cOLabel = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cOKind = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cOBrand = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cOName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cOCost = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cOPrice = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cOSellingPrice = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cONumber = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cOSubTotal = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.tpSales = New System.Windows.Forms.TabPage
        Me.btOrder2Sales = New System.Windows.Forms.Button
        Me.dgSalesList = New System.Windows.Forms.DataGridView
        Me.cSGoods = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cSLabel = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cSKind = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cSBrand = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cSName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cSCost = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cSPrice = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cSSellingPrice = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cSNumber = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cSSubTotal = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cSSalesDate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btDeleteSalesItem = New System.Windows.Forms.Button
        Me.btAddSalesItem = New System.Windows.Forms.Button
        Me.tpReturn = New System.Windows.Forms.TabPage
        Me.btDelReturnGoods = New System.Windows.Forms.Button
        Me.btAddReturnGoods = New System.Windows.Forms.Button
        Me.dgReturnList = New System.Windows.Forms.DataGridView
        Me.cRGoodsLabel = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cRSalesLabel = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cRStockLabel = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cRKind = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.crBrand = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cRName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cRCost = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cRSellingPrice = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cRReturnPrice = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cRNumber = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cRSubTotal = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.dgContract = New System.Windows.Forms.DataGridView
        Me.cCLabel = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cCName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cCPrepay = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cCCommission = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cCDiscount = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cCPhone = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cCReturnDate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btDeleteContract = New System.Windows.Forms.Button
        Me.btAddContract = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtPayCharge = New 進銷存.NumberBox
        Me.txtPayByCard = New 進銷存.NumberBox
        Me.txtDepositCharge = New 進銷存.NumberBox
        Me.txtDepositByCard = New 進銷存.NumberBox
        Me.txtPayByCash = New 進銷存.NumberBox
        Me.txtDeposit = New 進銷存.NumberBox
        Me.GroupBox2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.tpOrder.SuspendLayout()
        CType(Me.dgOrderList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpSales.SuspendLayout()
        CType(Me.dgSalesList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpReturn.SuspendLayout()
        CType(Me.dgReturnList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgContract, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(23, 512)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 12)
        Me.Label6.TabIndex = 38
        Me.Label6.Text = "備註"
        '
        'txtNote
        '
        Me.txtNote.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNote.Location = New System.Drawing.Point(21, 538)
        Me.txtNote.Multiline = True
        Me.txtNote.Name = "txtNote"
        Me.txtNote.Size = New System.Drawing.Size(336, 93)
        Me.txtNote.TabIndex = 37
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(23, 53)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 12)
        Me.Label4.TabIndex = 30
        Me.Label4.Text = "訂單日期"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(24, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "識別碼"
        '
        'txtOrderDate
        '
        Me.txtOrderDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtOrderDate.Location = New System.Drawing.Point(82, 50)
        Me.txtOrderDate.Name = "txtOrderDate"
        Me.txtOrderDate.ReadOnly = True
        Me.txtOrderDate.Size = New System.Drawing.Size(127, 22)
        Me.txtOrderDate.TabIndex = 25
        '
        'txtLabel
        '
        Me.txtLabel.Location = New System.Drawing.Point(82, 21)
        Me.txtLabel.Name = "txtLabel"
        Me.txtLabel.Size = New System.Drawing.Size(127, 22)
        Me.txtLabel.TabIndex = 24
        '
        'lbRealTotal
        '
        Me.lbRealTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbRealTotal.AutoSize = True
        Me.lbRealTotal.Location = New System.Drawing.Point(380, 603)
        Me.lbRealTotal.Name = "lbRealTotal"
        Me.lbRealTotal.Size = New System.Drawing.Size(53, 12)
        Me.lbRealTotal.TabIndex = 44
        Me.lbRealTotal.Text = "應付金額"
        '
        'lbTotal
        '
        Me.lbTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbTotal.AutoSize = True
        Me.lbTotal.Location = New System.Drawing.Point(380, 583)
        Me.lbTotal.Name = "lbTotal"
        Me.lbTotal.Size = New System.Drawing.Size(29, 12)
        Me.lbTotal.TabIndex = 45
        Me.lbTotal.Text = "合計"
        '
        'btOrder
        '
        Me.btOrder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btOrder.Location = New System.Drawing.Point(530, 628)
        Me.btOrder.Name = "btOrder"
        Me.btOrder.Size = New System.Drawing.Size(75, 35)
        Me.btOrder.TabIndex = 46
        Me.btOrder.Text = "修改訂單"
        Me.btOrder.UseVisualStyleBackColor = True
        '
        'cbPayMode
        '
        Me.cbPayMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbPayMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPayMode.Font = New System.Drawing.Font("新細明體", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.cbPayMode.FormattingEnabled = True
        Me.cbPayMode.Location = New System.Drawing.Point(417, 628)
        Me.cbPayMode.Name = "cbPayMode"
        Me.cbPayMode.Size = New System.Drawing.Size(92, 27)
        Me.cbPayMode.TabIndex = 47
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(382, 636)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 12)
        Me.Label3.TabIndex = 44
        Me.Label3.Text = "狀態"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(380, 528)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 12)
        Me.Label5.TabIndex = 44
        Me.Label5.Text = "訂金 =  現金"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(23, 89)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(53, 12)
        Me.Label7.TabIndex = 29
        Me.Label7.Text = "銷售人員"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(295, 89)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(29, 12)
        Me.Label8.TabIndex = 29
        Me.Label8.Text = "客戶"
        '
        'txtCustomer
        '
        Me.txtCustomer.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustomer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustomer.Location = New System.Drawing.Point(342, 84)
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.ReadOnly = True
        Me.txtCustomer.Size = New System.Drawing.Size(127, 22)
        Me.txtCustomer.TabIndex = 24
        '
        'txtPersonnel
        '
        Me.txtPersonnel.BackColor = System.Drawing.SystemColors.Window
        Me.txtPersonnel.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPersonnel.Location = New System.Drawing.Point(82, 84)
        Me.txtPersonnel.Name = "txtPersonnel"
        Me.txtPersonnel.ReadOnly = True
        Me.txtPersonnel.Size = New System.Drawing.Size(127, 22)
        Me.txtPersonnel.TabIndex = 24
        '
        'btResetPersonnel
        '
        Me.btResetPersonnel.Location = New System.Drawing.Point(215, 84)
        Me.btResetPersonnel.Name = "btResetPersonnel"
        Me.btResetPersonnel.Size = New System.Drawing.Size(38, 22)
        Me.btResetPersonnel.TabIndex = 49
        Me.btResetPersonnel.Text = "清除"
        Me.btResetPersonnel.UseVisualStyleBackColor = True
        '
        'btResetCustomer
        '
        Me.btResetCustomer.Location = New System.Drawing.Point(475, 84)
        Me.btResetCustomer.Name = "btResetCustomer"
        Me.btResetCustomer.Size = New System.Drawing.Size(38, 22)
        Me.btResetCustomer.TabIndex = 49
        Me.btResetCustomer.Text = "清除"
        Me.btResetCustomer.UseVisualStyleBackColor = True
        '
        'txtSalesDate
        '
        Me.txtSalesDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtSalesDate.Location = New System.Drawing.Point(342, 50)
        Me.txtSalesDate.Name = "txtSalesDate"
        Me.txtSalesDate.ReadOnly = True
        Me.txtSalesDate.Size = New System.Drawing.Size(127, 22)
        Me.txtSalesDate.TabIndex = 25
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(283, 53)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 12)
        Me.Label9.TabIndex = 30
        Me.Label9.Text = "銷貨日期"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.btResetCustomer)
        Me.GroupBox2.Controls.Add(Me.btResetPersonnel)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.txtSalesDate)
        Me.GroupBox2.Controls.Add(Me.txtOrderDate)
        Me.GroupBox2.Controls.Add(Me.txtCustomer)
        Me.GroupBox2.Controls.Add(Me.txtPersonnel)
        Me.GroupBox2.Controls.Add(Me.txtLabel)
        Me.GroupBox2.Location = New System.Drawing.Point(21, 6)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(670, 117)
        Me.GroupBox2.TabIndex = 50
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "資訊"
        '
        'btSales
        '
        Me.btSales.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btSales.Location = New System.Drawing.Point(611, 628)
        Me.btSales.Name = "btSales"
        Me.btSales.Size = New System.Drawing.Size(75, 35)
        Me.btSales.TabIndex = 46
        Me.btSales.Text = "銷貨"
        Me.btSales.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.tpOrder)
        Me.TabControl1.Controls.Add(Me.tpSales)
        Me.TabControl1.Controls.Add(Me.tpReturn)
        Me.TabControl1.Location = New System.Drawing.Point(21, 129)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(670, 239)
        Me.TabControl1.TabIndex = 51
        '
        'tpOrder
        '
        Me.tpOrder.Controls.Add(Me.btDeleteOrderItem)
        Me.tpOrder.Controls.Add(Me.btAddOrderItem)
        Me.tpOrder.Controls.Add(Me.dgOrderList)
        Me.tpOrder.Location = New System.Drawing.Point(4, 21)
        Me.tpOrder.Name = "tpOrder"
        Me.tpOrder.Padding = New System.Windows.Forms.Padding(3)
        Me.tpOrder.Size = New System.Drawing.Size(662, 214)
        Me.tpOrder.TabIndex = 0
        Me.tpOrder.Text = "訂單"
        Me.tpOrder.UseVisualStyleBackColor = True
        '
        'btDeleteOrderItem
        '
        Me.btDeleteOrderItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btDeleteOrderItem.Location = New System.Drawing.Point(571, 184)
        Me.btDeleteOrderItem.Name = "btDeleteOrderItem"
        Me.btDeleteOrderItem.Size = New System.Drawing.Size(75, 23)
        Me.btDeleteOrderItem.TabIndex = 49
        Me.btDeleteOrderItem.Text = "刪除"
        Me.btDeleteOrderItem.UseVisualStyleBackColor = True
        '
        'btAddOrderItem
        '
        Me.btAddOrderItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btAddOrderItem.Location = New System.Drawing.Point(490, 184)
        Me.btAddOrderItem.Name = "btAddOrderItem"
        Me.btAddOrderItem.Size = New System.Drawing.Size(75, 23)
        Me.btAddOrderItem.TabIndex = 48
        Me.btAddOrderItem.Text = "新增"
        Me.btAddOrderItem.UseVisualStyleBackColor = True
        '
        'dgOrderList
        '
        Me.dgOrderList.AllowUserToAddRows = False
        Me.dgOrderList.AllowUserToDeleteRows = False
        Me.dgOrderList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgOrderList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgOrderList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgOrderList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cOLabel, Me.cOKind, Me.cOBrand, Me.cOName, Me.cOCost, Me.cOPrice, Me.cOSellingPrice, Me.cONumber, Me.cOSubTotal})
        Me.dgOrderList.Location = New System.Drawing.Point(6, 6)
        Me.dgOrderList.Name = "dgOrderList"
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White
        Me.dgOrderList.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgOrderList.RowTemplate.Height = 24
        Me.dgOrderList.Size = New System.Drawing.Size(641, 172)
        Me.dgOrderList.TabIndex = 0
        '
        'cOLabel
        '
        Me.cOLabel.HeaderText = "商品編號"
        Me.cOLabel.Name = "cOLabel"
        Me.cOLabel.ReadOnly = True
        Me.cOLabel.Width = 78
        '
        'cOKind
        '
        Me.cOKind.HeaderText = "種類"
        Me.cOKind.Name = "cOKind"
        Me.cOKind.ReadOnly = True
        Me.cOKind.Width = 54
        '
        'cOBrand
        '
        Me.cOBrand.HeaderText = "廠牌"
        Me.cOBrand.Name = "cOBrand"
        Me.cOBrand.ReadOnly = True
        Me.cOBrand.Width = 54
        '
        'cOName
        '
        Me.cOName.HeaderText = "品名"
        Me.cOName.Name = "cOName"
        Me.cOName.ReadOnly = True
        Me.cOName.Width = 54
        '
        'cOCost
        '
        Me.cOCost.HeaderText = "進價"
        Me.cOCost.Name = "cOCost"
        Me.cOCost.ReadOnly = True
        Me.cOCost.Width = 54
        '
        'cOPrice
        '
        Me.cOPrice.HeaderText = "定價"
        Me.cOPrice.Name = "cOPrice"
        Me.cOPrice.ReadOnly = True
        Me.cOPrice.Width = 54
        '
        'cOSellingPrice
        '
        Me.cOSellingPrice.HeaderText = "賣價"
        Me.cOSellingPrice.Name = "cOSellingPrice"
        Me.cOSellingPrice.Width = 54
        '
        'cONumber
        '
        Me.cONumber.HeaderText = "數量"
        Me.cONumber.Name = "cONumber"
        Me.cONumber.Width = 54
        '
        'cOSubTotal
        '
        Me.cOSubTotal.HeaderText = "小計"
        Me.cOSubTotal.Name = "cOSubTotal"
        Me.cOSubTotal.ReadOnly = True
        Me.cOSubTotal.Width = 54
        '
        'tpSales
        '
        Me.tpSales.Controls.Add(Me.btOrder2Sales)
        Me.tpSales.Controls.Add(Me.dgSalesList)
        Me.tpSales.Controls.Add(Me.btDeleteSalesItem)
        Me.tpSales.Controls.Add(Me.btAddSalesItem)
        Me.tpSales.Location = New System.Drawing.Point(4, 21)
        Me.tpSales.Name = "tpSales"
        Me.tpSales.Padding = New System.Windows.Forms.Padding(3)
        Me.tpSales.Size = New System.Drawing.Size(662, 214)
        Me.tpSales.TabIndex = 1
        Me.tpSales.Text = "銷貨單"
        Me.tpSales.UseVisualStyleBackColor = True
        '
        'btOrder2Sales
        '
        Me.btOrder2Sales.Location = New System.Drawing.Point(22, 184)
        Me.btOrder2Sales.Name = "btOrder2Sales"
        Me.btOrder2Sales.Size = New System.Drawing.Size(98, 23)
        Me.btOrder2Sales.TabIndex = 51
        Me.btOrder2Sales.Text = "從訂單匯入>>"
        Me.btOrder2Sales.UseVisualStyleBackColor = True
        '
        'dgSalesList
        '
        Me.dgSalesList.AllowUserToAddRows = False
        Me.dgSalesList.AllowUserToDeleteRows = False
        Me.dgSalesList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgSalesList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgSalesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgSalesList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cSGoods, Me.cSLabel, Me.cSKind, Me.cSBrand, Me.cSName, Me.cSCost, Me.cSPrice, Me.cSSellingPrice, Me.cSNumber, Me.cSSubTotal, Me.cSSalesDate})
        Me.dgSalesList.Location = New System.Drawing.Point(6, 6)
        Me.dgSalesList.Name = "dgSalesList"
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White
        Me.dgSalesList.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgSalesList.RowTemplate.Height = 24
        Me.dgSalesList.Size = New System.Drawing.Size(641, 172)
        Me.dgSalesList.TabIndex = 48
        '
        'cSGoods
        '
        Me.cSGoods.HeaderText = "商品編號"
        Me.cSGoods.Name = "cSGoods"
        Me.cSGoods.ReadOnly = True
        Me.cSGoods.Visible = False
        Me.cSGoods.Width = 78
        '
        'cSLabel
        '
        Me.cSLabel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.cSLabel.HeaderText = "庫存編號"
        Me.cSLabel.Name = "cSLabel"
        Me.cSLabel.ReadOnly = True
        Me.cSLabel.Width = 78
        '
        'cSKind
        '
        Me.cSKind.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.cSKind.HeaderText = "種類"
        Me.cSKind.Name = "cSKind"
        Me.cSKind.ReadOnly = True
        Me.cSKind.Width = 54
        '
        'cSBrand
        '
        Me.cSBrand.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.cSBrand.HeaderText = "廠牌"
        Me.cSBrand.Name = "cSBrand"
        Me.cSBrand.ReadOnly = True
        Me.cSBrand.Width = 54
        '
        'cSName
        '
        Me.cSName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.cSName.HeaderText = "品名"
        Me.cSName.Name = "cSName"
        Me.cSName.ReadOnly = True
        Me.cSName.Width = 54
        '
        'cSCost
        '
        Me.cSCost.HeaderText = "進價"
        Me.cSCost.Name = "cSCost"
        Me.cSCost.ReadOnly = True
        Me.cSCost.Width = 54
        '
        'cSPrice
        '
        Me.cSPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.cSPrice.HeaderText = "定價"
        Me.cSPrice.Name = "cSPrice"
        Me.cSPrice.ReadOnly = True
        Me.cSPrice.Width = 54
        '
        'cSSellingPrice
        '
        Me.cSSellingPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.cSSellingPrice.HeaderText = "賣價"
        Me.cSSellingPrice.Name = "cSSellingPrice"
        Me.cSSellingPrice.Width = 54
        '
        'cSNumber
        '
        Me.cSNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.cSNumber.HeaderText = "數量"
        Me.cSNumber.Name = "cSNumber"
        Me.cSNumber.Width = 54
        '
        'cSSubTotal
        '
        Me.cSSubTotal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.cSSubTotal.HeaderText = "小計"
        Me.cSSubTotal.Name = "cSSubTotal"
        Me.cSSubTotal.ReadOnly = True
        Me.cSSubTotal.Width = 54
        '
        'cSSalesDate
        '
        DataGridViewCellStyle2.Format = "g"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.cSSalesDate.DefaultCellStyle = DataGridViewCellStyle2
        Me.cSSalesDate.HeaderText = "出貨日"
        Me.cSSalesDate.Name = "cSSalesDate"
        Me.cSSalesDate.ReadOnly = True
        Me.cSSalesDate.Visible = False
        Me.cSSalesDate.Width = 66
        '
        'btDeleteSalesItem
        '
        Me.btDeleteSalesItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btDeleteSalesItem.Location = New System.Drawing.Point(571, 184)
        Me.btDeleteSalesItem.Name = "btDeleteSalesItem"
        Me.btDeleteSalesItem.Size = New System.Drawing.Size(75, 23)
        Me.btDeleteSalesItem.TabIndex = 47
        Me.btDeleteSalesItem.Text = "刪除"
        Me.btDeleteSalesItem.UseVisualStyleBackColor = True
        '
        'btAddSalesItem
        '
        Me.btAddSalesItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btAddSalesItem.Location = New System.Drawing.Point(490, 184)
        Me.btAddSalesItem.Name = "btAddSalesItem"
        Me.btAddSalesItem.Size = New System.Drawing.Size(75, 23)
        Me.btAddSalesItem.TabIndex = 46
        Me.btAddSalesItem.Text = "新增"
        Me.btAddSalesItem.UseVisualStyleBackColor = True
        '
        'tpReturn
        '
        Me.tpReturn.Controls.Add(Me.btDelReturnGoods)
        Me.tpReturn.Controls.Add(Me.btAddReturnGoods)
        Me.tpReturn.Controls.Add(Me.dgReturnList)
        Me.tpReturn.Location = New System.Drawing.Point(4, 21)
        Me.tpReturn.Name = "tpReturn"
        Me.tpReturn.Padding = New System.Windows.Forms.Padding(3)
        Me.tpReturn.Size = New System.Drawing.Size(662, 214)
        Me.tpReturn.TabIndex = 2
        Me.tpReturn.Text = "退貨單"
        Me.tpReturn.UseVisualStyleBackColor = True
        '
        'btDelReturnGoods
        '
        Me.btDelReturnGoods.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btDelReturnGoods.Location = New System.Drawing.Point(571, 184)
        Me.btDelReturnGoods.Name = "btDelReturnGoods"
        Me.btDelReturnGoods.Size = New System.Drawing.Size(75, 23)
        Me.btDelReturnGoods.TabIndex = 51
        Me.btDelReturnGoods.Text = "刪除"
        Me.btDelReturnGoods.UseVisualStyleBackColor = True
        '
        'btAddReturnGoods
        '
        Me.btAddReturnGoods.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btAddReturnGoods.Location = New System.Drawing.Point(490, 184)
        Me.btAddReturnGoods.Name = "btAddReturnGoods"
        Me.btAddReturnGoods.Size = New System.Drawing.Size(75, 23)
        Me.btAddReturnGoods.TabIndex = 50
        Me.btAddReturnGoods.Text = "新增"
        Me.btAddReturnGoods.UseVisualStyleBackColor = True
        '
        'dgReturnList
        '
        Me.dgReturnList.AllowUserToAddRows = False
        Me.dgReturnList.AllowUserToDeleteRows = False
        Me.dgReturnList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgReturnList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgReturnList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgReturnList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cRGoodsLabel, Me.cRSalesLabel, Me.cRStockLabel, Me.cRKind, Me.crBrand, Me.cRName, Me.cRCost, Me.cRSellingPrice, Me.cRReturnPrice, Me.cRNumber, Me.cRSubTotal})
        Me.dgReturnList.Location = New System.Drawing.Point(6, 6)
        Me.dgReturnList.Name = "dgReturnList"
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White
        Me.dgReturnList.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgReturnList.RowTemplate.Height = 24
        Me.dgReturnList.Size = New System.Drawing.Size(641, 174)
        Me.dgReturnList.TabIndex = 49
        '
        'cRGoodsLabel
        '
        Me.cRGoodsLabel.HeaderText = "商品編號"
        Me.cRGoodsLabel.Name = "cRGoodsLabel"
        Me.cRGoodsLabel.ReadOnly = True
        Me.cRGoodsLabel.Visible = False
        Me.cRGoodsLabel.Width = 78
        '
        'cRSalesLabel
        '
        Me.cRSalesLabel.HeaderText = "銷貨編號"
        Me.cRSalesLabel.Name = "cRSalesLabel"
        Me.cRSalesLabel.ReadOnly = True
        Me.cRSalesLabel.Width = 78
        '
        'cRStockLabel
        '
        Me.cRStockLabel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.cRStockLabel.HeaderText = "庫存編號"
        Me.cRStockLabel.Name = "cRStockLabel"
        Me.cRStockLabel.ReadOnly = True
        Me.cRStockLabel.Width = 78
        '
        'cRKind
        '
        Me.cRKind.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.cRKind.HeaderText = "種類"
        Me.cRKind.Name = "cRKind"
        Me.cRKind.ReadOnly = True
        Me.cRKind.Width = 54
        '
        'crBrand
        '
        Me.crBrand.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.crBrand.HeaderText = "廠牌"
        Me.crBrand.Name = "crBrand"
        Me.crBrand.ReadOnly = True
        Me.crBrand.Width = 54
        '
        'cRName
        '
        Me.cRName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.cRName.HeaderText = "品名"
        Me.cRName.Name = "cRName"
        Me.cRName.ReadOnly = True
        Me.cRName.Width = 54
        '
        'cRCost
        '
        Me.cRCost.HeaderText = "進價"
        Me.cRCost.Name = "cRCost"
        Me.cRCost.ReadOnly = True
        Me.cRCost.Width = 54
        '
        'cRSellingPrice
        '
        Me.cRSellingPrice.HeaderText = "賣價"
        Me.cRSellingPrice.Name = "cRSellingPrice"
        Me.cRSellingPrice.ReadOnly = True
        Me.cRSellingPrice.Width = 54
        '
        'cRReturnPrice
        '
        Me.cRReturnPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.cRReturnPrice.HeaderText = "退價"
        Me.cRReturnPrice.Name = "cRReturnPrice"
        Me.cRReturnPrice.Width = 54
        '
        'cRNumber
        '
        Me.cRNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.cRNumber.HeaderText = "數量"
        Me.cRNumber.Name = "cRNumber"
        Me.cRNumber.Width = 54
        '
        'cRSubTotal
        '
        Me.cRSubTotal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.cRSubTotal.HeaderText = "小計"
        Me.cRSubTotal.Name = "cRSubTotal"
        Me.cRSubTotal.ReadOnly = True
        Me.cRSubTotal.Width = 54
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.dgContract)
        Me.GroupBox1.Controls.Add(Me.btDeleteContract)
        Me.GroupBox1.Controls.Add(Me.btAddContract)
        Me.GroupBox1.Location = New System.Drawing.Point(21, 374)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(670, 130)
        Me.GroupBox1.TabIndex = 52
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "合約"
        '
        'dgContract
        '
        Me.dgContract.AllowUserToAddRows = False
        Me.dgContract.AllowUserToDeleteRows = False
        Me.dgContract.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgContract.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgContract.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgContract.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cCLabel, Me.cCName, Me.cCPrepay, Me.cCCommission, Me.cCDiscount, Me.cCPhone, Me.cCReturnDate})
        Me.dgContract.Location = New System.Drawing.Point(10, 16)
        Me.dgContract.Name = "dgContract"
        Me.dgContract.RowTemplate.Height = 24
        Me.dgContract.Size = New System.Drawing.Size(650, 77)
        Me.dgContract.TabIndex = 48
        '
        'cCLabel
        '
        Me.cCLabel.HeaderText = "合約編號"
        Me.cCLabel.Name = "cCLabel"
        Me.cCLabel.ReadOnly = True
        Me.cCLabel.Width = 78
        '
        'cCName
        '
        Me.cCName.HeaderText = "名稱"
        Me.cCName.Name = "cCName"
        Me.cCName.ReadOnly = True
        Me.cCName.Width = 54
        '
        'cCPrepay
        '
        Me.cCPrepay.HeaderText = "預付額"
        Me.cCPrepay.Name = "cCPrepay"
        Me.cCPrepay.ReadOnly = True
        Me.cCPrepay.Width = 66
        '
        'cCCommission
        '
        Me.cCCommission.HeaderText = "佣金"
        Me.cCCommission.Name = "cCCommission"
        Me.cCCommission.Width = 54
        '
        'cCDiscount
        '
        Me.cCDiscount.HeaderText = "折扣"
        Me.cCDiscount.Name = "cCDiscount"
        Me.cCDiscount.Width = 54
        '
        'cCPhone
        '
        Me.cCPhone.HeaderText = "門號"
        Me.cCPhone.Name = "cCPhone"
        Me.cCPhone.Width = 54
        '
        'cCReturnDate
        '
        Me.cCReturnDate.HeaderText = "扣佣日期"
        Me.cCReturnDate.Name = "cCReturnDate"
        Me.cCReturnDate.ReadOnly = True
        Me.cCReturnDate.Width = 78
        '
        'btDeleteContract
        '
        Me.btDeleteContract.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btDeleteContract.Location = New System.Drawing.Point(576, 99)
        Me.btDeleteContract.Name = "btDeleteContract"
        Me.btDeleteContract.Size = New System.Drawing.Size(75, 23)
        Me.btDeleteContract.TabIndex = 47
        Me.btDeleteContract.Text = "刪除"
        Me.btDeleteContract.UseVisualStyleBackColor = True
        '
        'btAddContract
        '
        Me.btAddContract.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btAddContract.Location = New System.Drawing.Point(495, 99)
        Me.btAddContract.Name = "btAddContract"
        Me.btAddContract.Size = New System.Drawing.Size(75, 23)
        Me.btAddContract.TabIndex = 46
        Me.btAddContract.Text = "新增"
        Me.btAddContract.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(528, 528)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 12)
        Me.Label2.TabIndex = 44
        Me.Label2.Text = "+刷卡"
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(380, 556)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(68, 12)
        Me.Label10.TabIndex = 44
        Me.Label10.Text = "付款 =  現金"
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(528, 556)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(35, 12)
        Me.Label11.TabIndex = 44
        Me.Label11.Text = "+刷卡"
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(684, 526)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(14, 12)
        Me.Label12.TabIndex = 44
        Me.Label12.Text = "%"
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(684, 554)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(14, 12)
        Me.Label13.TabIndex = 44
        Me.Label13.Text = "%"
        '
        'txtPayCharge
        '
        Me.txtPayCharge.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPayCharge.Location = New System.Drawing.Point(652, 551)
        Me.txtPayCharge.Name = "txtPayCharge"
        Me.txtPayCharge.Size = New System.Drawing.Size(26, 22)
        Me.txtPayCharge.TabIndex = 48
        '
        'txtPayByCard
        '
        Me.txtPayByCard.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPayByCard.Location = New System.Drawing.Point(566, 551)
        Me.txtPayByCard.Name = "txtPayByCard"
        Me.txtPayByCard.Size = New System.Drawing.Size(74, 22)
        Me.txtPayByCard.TabIndex = 48
        '
        'txtDepositCharge
        '
        Me.txtDepositCharge.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDepositCharge.Location = New System.Drawing.Point(652, 523)
        Me.txtDepositCharge.Name = "txtDepositCharge"
        Me.txtDepositCharge.Size = New System.Drawing.Size(26, 22)
        Me.txtDepositCharge.TabIndex = 48
        '
        'txtDepositByCard
        '
        Me.txtDepositByCard.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDepositByCard.Location = New System.Drawing.Point(566, 523)
        Me.txtDepositByCard.Name = "txtDepositByCard"
        Me.txtDepositByCard.Size = New System.Drawing.Size(74, 22)
        Me.txtDepositByCard.TabIndex = 48
        '
        'txtPayByCash
        '
        Me.txtPayByCash.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPayByCash.Location = New System.Drawing.Point(449, 551)
        Me.txtPayByCash.Name = "txtPayByCash"
        Me.txtPayByCash.Size = New System.Drawing.Size(74, 22)
        Me.txtPayByCash.TabIndex = 48
        '
        'txtDeposit
        '
        Me.txtDeposit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDeposit.Location = New System.Drawing.Point(449, 523)
        Me.txtDeposit.Name = "txtDeposit"
        Me.txtDeposit.Size = New System.Drawing.Size(74, 22)
        Me.txtDeposit.TabIndex = 48
        '
        'winSales
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(707, 681)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.txtPayCharge)
        Me.Controls.Add(Me.txtPayByCard)
        Me.Controls.Add(Me.txtDepositCharge)
        Me.Controls.Add(Me.txtDepositByCard)
        Me.Controls.Add(Me.txtPayByCash)
        Me.Controls.Add(Me.txtDeposit)
        Me.Controls.Add(Me.cbPayMode)
        Me.Controls.Add(Me.btSales)
        Me.Controls.Add(Me.btOrder)
        Me.Controls.Add(Me.lbTotal)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lbRealTotal)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtNote)
        Me.Name = "winSales"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "銷貨單"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.tpOrder.ResumeLayout(False)
        CType(Me.dgOrderList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpSales.ResumeLayout(False)
        CType(Me.dgSalesList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpReturn.ResumeLayout(False)
        CType(Me.dgReturnList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.dgContract, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtNote As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtOrderDate As System.Windows.Forms.TextBox
    Friend WithEvents txtLabel As System.Windows.Forms.TextBox
    Friend WithEvents lbRealTotal As System.Windows.Forms.Label
    Friend WithEvents lbTotal As System.Windows.Forms.Label
    Friend WithEvents btOrder As System.Windows.Forms.Button
    Friend WithEvents cbPayMode As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtDeposit As NumberBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtCustomer As System.Windows.Forms.TextBox
    Friend WithEvents txtPersonnel As System.Windows.Forms.TextBox
    Friend WithEvents btResetPersonnel As System.Windows.Forms.Button
    Friend WithEvents btResetCustomer As System.Windows.Forms.Button
    Friend WithEvents txtSalesDate As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btSales As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tpOrder As System.Windows.Forms.TabPage
    Friend WithEvents tpSales As System.Windows.Forms.TabPage
    Friend WithEvents dgSalesList As System.Windows.Forms.DataGridView
    Friend WithEvents btDeleteSalesItem As System.Windows.Forms.Button
    Friend WithEvents btAddSalesItem As System.Windows.Forms.Button
    Friend WithEvents dgOrderList As System.Windows.Forms.DataGridView
    Friend WithEvents btDeleteOrderItem As System.Windows.Forms.Button
    Friend WithEvents btAddOrderItem As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btDeleteContract As System.Windows.Forms.Button
    Friend WithEvents btAddContract As System.Windows.Forms.Button
    Friend WithEvents dgContract As System.Windows.Forms.DataGridView
    Friend WithEvents btOrder2Sales As System.Windows.Forms.Button
    Friend WithEvents txtDepositByCard As NumberBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cOLabel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cOKind As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cOBrand As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cOName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cOCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cOPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cOSellingPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cONumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cOSubTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tpReturn As System.Windows.Forms.TabPage
    Friend WithEvents dgReturnList As System.Windows.Forms.DataGridView
    Friend WithEvents btDelReturnGoods As System.Windows.Forms.Button
    Friend WithEvents btAddReturnGoods As System.Windows.Forms.Button
    Friend WithEvents cRGoodsLabel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cRSalesLabel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cRStockLabel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cRKind As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents crBrand As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cRName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cRCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cRSellingPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cRReturnPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cRNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cRSubTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSGoods As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSLabel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSKind As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSBrand As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSSellingPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSSubTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSSalesDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtPayByCash As 進銷存.NumberBox
    Friend WithEvents txtPayByCard As 進銷存.NumberBox
    Friend WithEvents txtDepositCharge As 進銷存.NumberBox
    Friend WithEvents txtPayCharge As 進銷存.NumberBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cCLabel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCPrepay As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCCommission As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCDiscount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCPhone As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCReturnDate As System.Windows.Forms.DataGridViewTextBoxColumn
    'Friend WithEvents cSSalesDate As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
