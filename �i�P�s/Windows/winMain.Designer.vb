<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class winMain
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
        Me.components = New System.ComponentModel.Container
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.銷貨ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.庫存ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.進貨記錄ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.查詢庫存ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.商品項目GToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.dgSales = New System.Windows.Forms.DataGridView
        Me.cSalesLabel = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cTime = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cPersonnel = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Customer = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cDposit = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cPrice = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cmsEdit = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.銷貨AToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.修改CToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.刪除DToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.dtpEnd = New System.Windows.Forms.DateTimePicker
        Me.dtpStart = New System.Windows.Forms.DateTimePicker
        Me.rToday = New System.Windows.Forms.RadioButton
        Me.rUserTime = New System.Windows.Forms.RadioButton
        Me.r30Day = New System.Windows.Forms.RadioButton
        Me.其他資訊ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.供應商ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuStrip1.SuspendLayout()
        CType(Me.dgSales, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsEdit.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.銷貨ToolStripMenuItem, Me.庫存ToolStripMenuItem, Me.其他資訊ToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(795, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        '銷貨ToolStripMenuItem
        '
        Me.銷貨ToolStripMenuItem.Name = "銷貨ToolStripMenuItem"
        Me.銷貨ToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.銷貨ToolStripMenuItem.Text = "銷貨"
        '
        '庫存ToolStripMenuItem
        '
        Me.庫存ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.進貨記錄ToolStripMenuItem1, Me.查詢庫存ToolStripMenuItem, Me.商品項目GToolStripMenuItem})
        Me.庫存ToolStripMenuItem.Name = "庫存ToolStripMenuItem"
        Me.庫存ToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.庫存ToolStripMenuItem.Text = "庫存"
        '
        '進貨記錄ToolStripMenuItem1
        '
        Me.進貨記錄ToolStripMenuItem1.Name = "進貨記錄ToolStripMenuItem1"
        Me.進貨記錄ToolStripMenuItem1.Size = New System.Drawing.Size(141, 22)
        Me.進貨記錄ToolStripMenuItem1.Text = "進貨記錄(&I)"
        '
        '查詢庫存ToolStripMenuItem
        '
        Me.查詢庫存ToolStripMenuItem.Name = "查詢庫存ToolStripMenuItem"
        Me.查詢庫存ToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
        Me.查詢庫存ToolStripMenuItem.Text = "查詢庫存(&S)"
        '
        '商品項目GToolStripMenuItem
        '
        Me.商品項目GToolStripMenuItem.Name = "商品項目GToolStripMenuItem"
        Me.商品項目GToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
        Me.商品項目GToolStripMenuItem.Text = "商品項目(&G)"
        '
        'dgSales
        '
        Me.dgSales.AllowUserToAddRows = False
        Me.dgSales.AllowUserToDeleteRows = False
        Me.dgSales.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgSales.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgSales.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cSalesLabel, Me.cTime, Me.cPersonnel, Me.Customer, Me.cDposit, Me.cPrice})
        Me.dgSales.ContextMenuStrip = Me.cmsEdit
        Me.dgSales.Location = New System.Drawing.Point(12, 85)
        Me.dgSales.Name = "dgSales"
        Me.dgSales.ReadOnly = True
        Me.dgSales.RowTemplate.Height = 24
        Me.dgSales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgSales.Size = New System.Drawing.Size(771, 465)
        Me.dgSales.TabIndex = 1
        '
        'cSalesLabel
        '
        Me.cSalesLabel.HeaderText = "單號"
        Me.cSalesLabel.Name = "cSalesLabel"
        Me.cSalesLabel.ReadOnly = True
        Me.cSalesLabel.Width = 54
        '
        'cTime
        '
        Me.cTime.HeaderText = "時間"
        Me.cTime.Name = "cTime"
        Me.cTime.ReadOnly = True
        Me.cTime.Width = 54
        '
        'cPersonnel
        '
        Me.cPersonnel.HeaderText = "銷售人員"
        Me.cPersonnel.Name = "cPersonnel"
        Me.cPersonnel.ReadOnly = True
        Me.cPersonnel.Width = 78
        '
        'Customer
        '
        Me.Customer.HeaderText = "客戶"
        Me.Customer.Name = "Customer"
        Me.Customer.ReadOnly = True
        Me.Customer.Width = 54
        '
        'cDposit
        '
        Me.cDposit.HeaderText = "訂金"
        Me.cDposit.Name = "cDposit"
        Me.cDposit.ReadOnly = True
        Me.cDposit.Width = 54
        '
        'cPrice
        '
        Me.cPrice.HeaderText = "金額"
        Me.cPrice.Name = "cPrice"
        Me.cPrice.ReadOnly = True
        Me.cPrice.Width = 54
        '
        'cmsEdit
        '
        Me.cmsEdit.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.銷貨AToolStripMenuItem, Me.修改CToolStripMenuItem, Me.刪除DToolStripMenuItem})
        Me.cmsEdit.Name = "cmsEdit"
        Me.cmsEdit.Size = New System.Drawing.Size(118, 70)
        '
        '銷貨AToolStripMenuItem
        '
        Me.銷貨AToolStripMenuItem.Name = "銷貨AToolStripMenuItem"
        Me.銷貨AToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.銷貨AToolStripMenuItem.Text = "銷貨(&A)"
        '
        '修改CToolStripMenuItem
        '
        Me.修改CToolStripMenuItem.Name = "修改CToolStripMenuItem"
        Me.修改CToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.修改CToolStripMenuItem.Text = "修改(&C)"
        '
        '刪除DToolStripMenuItem
        '
        Me.刪除DToolStripMenuItem.Name = "刪除DToolStripMenuItem"
        Me.刪除DToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.刪除DToolStripMenuItem.Text = "刪除(&D)"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.dtpEnd)
        Me.GroupBox1.Controls.Add(Me.dtpStart)
        Me.GroupBox1.Controls.Add(Me.rToday)
        Me.GroupBox1.Controls.Add(Me.rUserTime)
        Me.GroupBox1.Controls.Add(Me.r30Day)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 27)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(771, 46)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "篩選時間"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(440, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(11, 12)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "~"
        '
        'dtpEnd
        '
        Me.dtpEnd.Location = New System.Drawing.Point(464, 15)
        Me.dtpEnd.Name = "dtpEnd"
        Me.dtpEnd.Size = New System.Drawing.Size(139, 22)
        Me.dtpEnd.TabIndex = 1
        '
        'dtpStart
        '
        Me.dtpStart.Location = New System.Drawing.Point(287, 15)
        Me.dtpStart.Name = "dtpStart"
        Me.dtpStart.Size = New System.Drawing.Size(139, 22)
        Me.dtpStart.TabIndex = 1
        '
        'rToday
        '
        Me.rToday.AutoSize = True
        Me.rToday.Location = New System.Drawing.Point(22, 21)
        Me.rToday.Name = "rToday"
        Me.rToday.Size = New System.Drawing.Size(47, 16)
        Me.rToday.TabIndex = 0
        Me.rToday.Text = "今日"
        Me.rToday.UseVisualStyleBackColor = True
        '
        'rUserTime
        '
        Me.rUserTime.AutoSize = True
        Me.rUserTime.Location = New System.Drawing.Point(210, 20)
        Me.rUserTime.Name = "rUserTime"
        Me.rUserTime.Size = New System.Drawing.Size(71, 16)
        Me.rUserTime.TabIndex = 0
        Me.rUserTime.Text = "指定時間"
        Me.rUserTime.UseVisualStyleBackColor = True
        '
        'r30Day
        '
        Me.r30Day.AutoSize = True
        Me.r30Day.Checked = True
        Me.r30Day.Location = New System.Drawing.Point(101, 21)
        Me.r30Day.Name = "r30Day"
        Me.r30Day.Size = New System.Drawing.Size(71, 16)
        Me.r30Day.TabIndex = 0
        Me.r30Day.TabStop = True
        Me.r30Day.Text = "30天以內"
        Me.r30Day.UseVisualStyleBackColor = True
        '
        '其他資訊ToolStripMenuItem
        '
        Me.其他資訊ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.供應商ToolStripMenuItem})
        Me.其他資訊ToolStripMenuItem.Name = "其他資訊ToolStripMenuItem"
        Me.其他資訊ToolStripMenuItem.Size = New System.Drawing.Size(68, 20)
        Me.其他資訊ToolStripMenuItem.Text = "其他資訊"
        '
        '供應商ToolStripMenuItem
        '
        Me.供應商ToolStripMenuItem.Name = "供應商ToolStripMenuItem"
        Me.供應商ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.供應商ToolStripMenuItem.Text = "供應商"
        '
        'winMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(795, 562)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgSales)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "winMain"
        Me.Text = "銷貨記錄"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.dgSales, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsEdit.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents 銷貨ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 庫存ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents dgSales As System.Windows.Forms.DataGridView
    Friend WithEvents cSalesLabel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cPersonnel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Customer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cDposit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 進貨記錄ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 查詢庫存ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents rToday As System.Windows.Forms.RadioButton
    Friend WithEvents rUserTime As System.Windows.Forms.RadioButton
    Friend WithEvents r30Day As System.Windows.Forms.RadioButton
    Friend WithEvents cmsEdit As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents 銷貨AToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 修改CToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 刪除DToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 商品項目GToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 其他資訊ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 供應商ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
