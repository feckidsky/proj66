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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(winMain))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.系統SToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.登入IToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.登出OToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.修改密碼PToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.關閉CToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.庫存ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.進貨記錄ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.查詢庫存ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.商品項目GToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.銷貨ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.其他資訊ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.供應商ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.客戶CToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.員工PToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
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
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.cmsSystem = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.登入IToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.登出OToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.開啟OToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.關閉QToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.縮到工具列TToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cbForm = New System.Windows.Forms.ComboBox
        Me.選項OToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuStrip1.SuspendLayout()
        CType(Me.dgSales, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsEdit.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.cmsSystem.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.系統SToolStripMenuItem, Me.庫存ToolStripMenuItem, Me.銷貨ToolStripMenuItem, Me.其他資訊ToolStripMenuItem, Me.選項OToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(795, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        '系統SToolStripMenuItem
        '
        Me.系統SToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.登入IToolStripMenuItem, Me.登出OToolStripMenuItem, Me.修改密碼PToolStripMenuItem, Me.ToolStripSeparator2, Me.關閉CToolStripMenuItem})
        Me.系統SToolStripMenuItem.Name = "系統SToolStripMenuItem"
        Me.系統SToolStripMenuItem.Size = New System.Drawing.Size(59, 20)
        Me.系統SToolStripMenuItem.Text = "系統(&S)"
        '
        '登入IToolStripMenuItem
        '
        Me.登入IToolStripMenuItem.Name = "登入IToolStripMenuItem"
        Me.登入IToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.登入IToolStripMenuItem.Text = "登入(&I)"
        '
        '登出OToolStripMenuItem
        '
        Me.登出OToolStripMenuItem.Name = "登出OToolStripMenuItem"
        Me.登出OToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.登出OToolStripMenuItem.Text = "登出(&O)"
        '
        '修改密碼PToolStripMenuItem
        '
        Me.修改密碼PToolStripMenuItem.Name = "修改密碼PToolStripMenuItem"
        Me.修改密碼PToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.修改密碼PToolStripMenuItem.Text = "修改密碼(&P)"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(136, 6)
        '
        '關閉CToolStripMenuItem
        '
        Me.關閉CToolStripMenuItem.Name = "關閉CToolStripMenuItem"
        Me.關閉CToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.關閉CToolStripMenuItem.Text = "關閉(&Q)"
        '
        '庫存ToolStripMenuItem
        '
        Me.庫存ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.進貨記錄ToolStripMenuItem1, Me.查詢庫存ToolStripMenuItem, Me.商品項目GToolStripMenuItem})
        Me.庫存ToolStripMenuItem.Name = "庫存ToolStripMenuItem"
        Me.庫存ToolStripMenuItem.Size = New System.Drawing.Size(59, 20)
        Me.庫存ToolStripMenuItem.Text = "庫存(&S)"
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
        '銷貨ToolStripMenuItem
        '
        Me.銷貨ToolStripMenuItem.Name = "銷貨ToolStripMenuItem"
        Me.銷貨ToolStripMenuItem.Size = New System.Drawing.Size(62, 20)
        Me.銷貨ToolStripMenuItem.Text = "訂單(&O)"
        '
        '其他資訊ToolStripMenuItem
        '
        Me.其他資訊ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.供應商ToolStripMenuItem, Me.客戶CToolStripMenuItem, Me.ToolStripSeparator3, Me.員工PToolStripMenuItem})
        Me.其他資訊ToolStripMenuItem.Name = "其他資訊ToolStripMenuItem"
        Me.其他資訊ToolStripMenuItem.Size = New System.Drawing.Size(83, 20)
        Me.其他資訊ToolStripMenuItem.Text = "其他資訊(&E)"
        '
        '供應商ToolStripMenuItem
        '
        Me.供應商ToolStripMenuItem.Name = "供應商ToolStripMenuItem"
        Me.供應商ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.供應商ToolStripMenuItem.Text = "供應商(&S)"
        '
        '客戶CToolStripMenuItem
        '
        Me.客戶CToolStripMenuItem.Name = "客戶CToolStripMenuItem"
        Me.客戶CToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.客戶CToolStripMenuItem.Text = "客戶(&C)"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(149, 6)
        '
        '員工PToolStripMenuItem
        '
        Me.員工PToolStripMenuItem.Name = "員工PToolStripMenuItem"
        Me.員工PToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.員工PToolStripMenuItem.Text = "員工(&P)"
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
        Me.dgSales.Location = New System.Drawing.Point(12, 79)
        Me.dgSales.Name = "dgSales"
        Me.dgSales.ReadOnly = True
        Me.dgSales.RowTemplate.Height = 24
        Me.dgSales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgSales.Size = New System.Drawing.Size(771, 471)
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
        Me.GroupBox1.Location = New System.Drawing.Point(222, 27)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(561, 46)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "篩選時間"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(386, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(11, 12)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "~"
        '
        'dtpEnd
        '
        Me.dtpEnd.Location = New System.Drawing.Point(410, 15)
        Me.dtpEnd.Name = "dtpEnd"
        Me.dtpEnd.Size = New System.Drawing.Size(139, 22)
        Me.dtpEnd.TabIndex = 1
        '
        'dtpStart
        '
        Me.dtpStart.Location = New System.Drawing.Point(233, 15)
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
        Me.rUserTime.Location = New System.Drawing.Point(156, 20)
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
        Me.r30Day.Location = New System.Drawing.Point(76, 21)
        Me.r30Day.Name = "r30Day"
        Me.r30Day.Size = New System.Drawing.Size(71, 16)
        Me.r30Day.TabIndex = 0
        Me.r30Day.TabStop = True
        Me.r30Day.Text = "30天以內"
        Me.r30Day.UseVisualStyleBackColor = True
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.NotifyIcon1.BalloonTipText = "文字"
        Me.NotifyIcon1.BalloonTipTitle = "標題"
        Me.NotifyIcon1.ContextMenuStrip = Me.cmsSystem
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "進銷存系統"
        Me.NotifyIcon1.Visible = True
        '
        'cmsSystem
        '
        Me.cmsSystem.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.登入IToolStripMenuItem1, Me.登出OToolStripMenuItem1, Me.開啟OToolStripMenuItem, Me.ToolStripSeparator1, Me.關閉QToolStripMenuItem, Me.縮到工具列TToolStripMenuItem})
        Me.cmsSystem.Name = "cmsSystem"
        Me.cmsSystem.Size = New System.Drawing.Size(137, 120)
        '
        '登入IToolStripMenuItem1
        '
        Me.登入IToolStripMenuItem1.Name = "登入IToolStripMenuItem1"
        Me.登入IToolStripMenuItem1.Size = New System.Drawing.Size(136, 22)
        Me.登入IToolStripMenuItem1.Text = "登入"
        '
        '登出OToolStripMenuItem1
        '
        Me.登出OToolStripMenuItem1.Name = "登出OToolStripMenuItem1"
        Me.登出OToolStripMenuItem1.Size = New System.Drawing.Size(136, 22)
        Me.登出OToolStripMenuItem1.Text = "登出"
        '
        '開啟OToolStripMenuItem
        '
        Me.開啟OToolStripMenuItem.Name = "開啟OToolStripMenuItem"
        Me.開啟OToolStripMenuItem.Size = New System.Drawing.Size(136, 22)
        Me.開啟OToolStripMenuItem.Text = "開啟"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(133, 6)
        '
        '關閉QToolStripMenuItem
        '
        Me.關閉QToolStripMenuItem.Name = "關閉QToolStripMenuItem"
        Me.關閉QToolStripMenuItem.Size = New System.Drawing.Size(136, 22)
        Me.關閉QToolStripMenuItem.Text = "關閉"
        '
        '縮到工具列TToolStripMenuItem
        '
        Me.縮到工具列TToolStripMenuItem.Name = "縮到工具列TToolStripMenuItem"
        Me.縮到工具列TToolStripMenuItem.Size = New System.Drawing.Size(136, 22)
        Me.縮到工具列TToolStripMenuItem.Text = "縮到工具列"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cbForm)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 27)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(204, 45)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "種類"
        '
        'cbForm
        '
        Me.cbForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbForm.FormattingEnabled = True
        Me.cbForm.Items.AddRange(New Object() {"訂單", "銷貨單", "訂單與銷貨單"})
        Me.cbForm.Location = New System.Drawing.Point(44, 15)
        Me.cbForm.Name = "cbForm"
        Me.cbForm.Size = New System.Drawing.Size(121, 20)
        Me.cbForm.TabIndex = 0
        '
        '選項OToolStripMenuItem
        '
        Me.選項OToolStripMenuItem.Name = "選項OToolStripMenuItem"
        Me.選項OToolStripMenuItem.Size = New System.Drawing.Size(62, 20)
        Me.選項OToolStripMenuItem.Text = "選項(&O)"
        '
        'winMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(795, 562)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgSales)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "winMain"
        Me.Text = "進銷存管理系統"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.dgSales, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsEdit.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.cmsSystem.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
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
    Friend WithEvents 員工PToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 系統SToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 登入IToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 登出OToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 修改密碼PToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 關閉CToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents cmsSystem As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents 登入IToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 登出OToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 關閉QToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 開啟OToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 縮到工具列TToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 客戶CToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cbForm As System.Windows.Forms.ComboBox
    Friend WithEvents 選項OToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
