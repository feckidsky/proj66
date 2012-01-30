<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
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
        Me.txtDeposit = New System.Windows.Forms.TextBox
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
        Me.cOPrice = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cOSellingPrice = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cONumber = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cOSubTotal = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.tpSales = New System.Windows.Forms.TabPage
        Me.dgSalesList = New System.Windows.Forms.DataGridView
        Me.cSGoods = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cSLabel = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cSKind = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cSBrand = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cSName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cSPrice = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cSSellingPrice = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cSNumber = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cSSubTotal = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btDeleteSalesItem = New System.Windows.Forms.Button
        Me.btAddSalesItem = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.dgContract = New System.Windows.Forms.DataGridView
        Me.cCLabel = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cCName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cCPrepay = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cCDiscount = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cCPhone = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btDeleteContract = New System.Windows.Forms.Button
        Me.btAddContract = New System.Windows.Forms.Button
        Me.GroupBox2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.tpOrder.SuspendLayout()
        CType(Me.dgOrderList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpSales.SuspendLayout()
        CType(Me.dgSalesList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgContract, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(19, 547)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 12)
        Me.Label6.TabIndex = 38
        Me.Label6.Text = "備註"
        '
        'txtNote
        '
        Me.txtNote.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNote.Location = New System.Drawing.Point(21, 562)
        Me.txtNote.Multiline = True
        Me.txtNote.Name = "txtNote"
        Me.txtNote.Size = New System.Drawing.Size(306, 93)
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
        Me.txtOrderDate.Enabled = False
        Me.txtOrderDate.Location = New System.Drawing.Point(82, 50)
        Me.txtOrderDate.Name = "txtOrderDate"
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
        Me.lbRealTotal.Location = New System.Drawing.Point(345, 606)
        Me.lbRealTotal.Name = "lbRealTotal"
        Me.lbRealTotal.Size = New System.Drawing.Size(53, 12)
        Me.lbRealTotal.TabIndex = 44
        Me.lbRealTotal.Text = "應付金額"
        '
        'lbTotal
        '
        Me.lbTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbTotal.AutoSize = True
        Me.lbTotal.Location = New System.Drawing.Point(345, 578)
        Me.lbTotal.Name = "lbTotal"
        Me.lbTotal.Size = New System.Drawing.Size(29, 12)
        Me.lbTotal.TabIndex = 45
        Me.lbTotal.Text = "合計"
        '
        'btOrder
        '
        Me.btOrder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btOrder.Location = New System.Drawing.Point(462, 636)
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
        Me.cbPayMode.FormattingEnabled = True
        Me.cbPayMode.Location = New System.Drawing.Point(526, 544)
        Me.cbPayMode.Name = "cbPayMode"
        Me.cbPayMode.Size = New System.Drawing.Size(79, 20)
        Me.cbPayMode.TabIndex = 47
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(465, 547)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 12)
        Me.Label3.TabIndex = 44
        Me.Label3.Text = "付款方式"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(342, 547)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(29, 12)
        Me.Label5.TabIndex = 44
        Me.Label5.Text = "訂金"
        '
        'txtDeposit
        '
        Me.txtDeposit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDeposit.Location = New System.Drawing.Point(377, 544)
        Me.txtDeposit.Name = "txtDeposit"
        Me.txtDeposit.Size = New System.Drawing.Size(74, 22)
        Me.txtDeposit.TabIndex = 48
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
        Me.txtSalesDate.Enabled = False
        Me.txtSalesDate.Location = New System.Drawing.Point(342, 50)
        Me.txtSalesDate.Name = "txtSalesDate"
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
        Me.GroupBox2.Size = New System.Drawing.Size(619, 117)
        Me.GroupBox2.TabIndex = 50
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "資訊"
        '
        'btSales
        '
        Me.btSales.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btSales.Location = New System.Drawing.Point(543, 636)
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
        Me.TabControl1.Location = New System.Drawing.Point(21, 129)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(619, 268)
        Me.TabControl1.TabIndex = 51
        '
        'tpOrder
        '
        Me.tpOrder.Controls.Add(Me.btDeleteOrderItem)
        Me.tpOrder.Controls.Add(Me.btAddOrderItem)
        Me.tpOrder.Controls.Add(Me.dgOrderList)
        Me.tpOrder.Location = New System.Drawing.Point(4, 22)
        Me.tpOrder.Name = "tpOrder"
        Me.tpOrder.Padding = New System.Windows.Forms.Padding(3)
        Me.tpOrder.Size = New System.Drawing.Size(611, 242)
        Me.tpOrder.TabIndex = 0
        Me.tpOrder.Text = "訂單"
        Me.tpOrder.UseVisualStyleBackColor = True
        '
        'btDeleteOrderItem
        '
        Me.btDeleteOrderItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btDeleteOrderItem.Location = New System.Drawing.Point(521, 213)
        Me.btDeleteOrderItem.Name = "btDeleteOrderItem"
        Me.btDeleteOrderItem.Size = New System.Drawing.Size(75, 23)
        Me.btDeleteOrderItem.TabIndex = 49
        Me.btDeleteOrderItem.Text = "刪除"
        Me.btDeleteOrderItem.UseVisualStyleBackColor = True
        '
        'btAddOrderItem
        '
        Me.btAddOrderItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btAddOrderItem.Location = New System.Drawing.Point(440, 213)
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
        Me.dgOrderList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cOLabel, Me.cOKind, Me.cOBrand, Me.cOName, Me.cOPrice, Me.cOSellingPrice, Me.cONumber, Me.cOSubTotal})
        Me.dgOrderList.Location = New System.Drawing.Point(6, 9)
        Me.dgOrderList.Name = "dgOrderList"
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.White
        Me.dgOrderList.RowsDefaultCellStyle = DataGridViewCellStyle11
        Me.dgOrderList.RowTemplate.Height = 24
        Me.dgOrderList.Size = New System.Drawing.Size(599, 198)
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
        Me.tpSales.Controls.Add(Me.dgSalesList)
        Me.tpSales.Controls.Add(Me.btDeleteSalesItem)
        Me.tpSales.Controls.Add(Me.btAddSalesItem)
        Me.tpSales.Location = New System.Drawing.Point(4, 22)
        Me.tpSales.Name = "tpSales"
        Me.tpSales.Padding = New System.Windows.Forms.Padding(3)
        Me.tpSales.Size = New System.Drawing.Size(611, 242)
        Me.tpSales.TabIndex = 1
        Me.tpSales.Text = "銷貨單"
        Me.tpSales.UseVisualStyleBackColor = True
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
        Me.dgSalesList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cSGoods, Me.cSLabel, Me.cSKind, Me.cSBrand, Me.cSName, Me.cSPrice, Me.cSSellingPrice, Me.cSNumber, Me.cSSubTotal})
        Me.dgSalesList.Location = New System.Drawing.Point(6, 9)
        Me.dgSalesList.Name = "dgSalesList"
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.White
        Me.dgSalesList.RowsDefaultCellStyle = DataGridViewCellStyle12
        Me.dgSalesList.RowTemplate.Height = 24
        Me.dgSalesList.Size = New System.Drawing.Size(599, 194)
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
        'btDeleteSalesItem
        '
        Me.btDeleteSalesItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btDeleteSalesItem.Location = New System.Drawing.Point(521, 209)
        Me.btDeleteSalesItem.Name = "btDeleteSalesItem"
        Me.btDeleteSalesItem.Size = New System.Drawing.Size(75, 23)
        Me.btDeleteSalesItem.TabIndex = 47
        Me.btDeleteSalesItem.Text = "刪除"
        Me.btDeleteSalesItem.UseVisualStyleBackColor = True
        '
        'btAddSalesItem
        '
        Me.btAddSalesItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btAddSalesItem.Location = New System.Drawing.Point(440, 209)
        Me.btAddSalesItem.Name = "btAddSalesItem"
        Me.btAddSalesItem.Size = New System.Drawing.Size(75, 23)
        Me.btAddSalesItem.TabIndex = 46
        Me.btAddSalesItem.Text = "新增"
        Me.btAddSalesItem.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.dgContract)
        Me.GroupBox1.Controls.Add(Me.btDeleteContract)
        Me.GroupBox1.Controls.Add(Me.btAddContract)
        Me.GroupBox1.Location = New System.Drawing.Point(21, 403)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(619, 130)
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
        Me.dgContract.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cCLabel, Me.cCName, Me.cCPrepay, Me.cCDiscount, Me.cCPhone})
        Me.dgContract.Location = New System.Drawing.Point(10, 16)
        Me.dgContract.Name = "dgContract"
        Me.dgContract.RowTemplate.Height = 24
        Me.dgContract.Size = New System.Drawing.Size(599, 77)
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
        Me.cCPrepay.Width = 66
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
        'btDeleteContract
        '
        Me.btDeleteContract.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btDeleteContract.Location = New System.Drawing.Point(525, 99)
        Me.btDeleteContract.Name = "btDeleteContract"
        Me.btDeleteContract.Size = New System.Drawing.Size(75, 23)
        Me.btDeleteContract.TabIndex = 47
        Me.btDeleteContract.Text = "刪除"
        Me.btDeleteContract.UseVisualStyleBackColor = True
        '
        'btAddContract
        '
        Me.btAddContract.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btAddContract.Location = New System.Drawing.Point(444, 99)
        Me.btAddContract.Name = "btAddContract"
        Me.btAddContract.Size = New System.Drawing.Size(75, 23)
        Me.btAddContract.TabIndex = 46
        Me.btAddContract.Text = "新增"
        Me.btAddContract.UseVisualStyleBackColor = True
        '
        'winSales
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(656, 683)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.txtDeposit)
        Me.Controls.Add(Me.cbPayMode)
        Me.Controls.Add(Me.btSales)
        Me.Controls.Add(Me.btOrder)
        Me.Controls.Add(Me.lbTotal)
        Me.Controls.Add(Me.Label3)
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
    Friend WithEvents txtDeposit As System.Windows.Forms.TextBox
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
    Friend WithEvents cOLabel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cOKind As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cOBrand As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cOName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cOPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cOSellingPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cONumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cOSubTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCLabel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCPrepay As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCDiscount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCPhone As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSGoods As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSLabel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSKind As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSBrand As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSSellingPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSSubTotal As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
