<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class winStockList
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
        Me.dgItemList = New System.Windows.Forms.DataGridView
        Me.cmsStock = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.調貨ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.進貨ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.調貨ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.cbStock = New System.Windows.Forms.ToolStripComboBox
        Me.列印PToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.dgItemList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsStock.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgItemList
        '
        Me.dgItemList.AllowUserToAddRows = False
        Me.dgItemList.AllowUserToDeleteRows = False
        Me.dgItemList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgItemList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgItemList.ContextMenuStrip = Me.cmsStock
        Me.dgItemList.Location = New System.Drawing.Point(10, 28)
        Me.dgItemList.Name = "dgItemList"
        Me.dgItemList.ReadOnly = True
        Me.dgItemList.RowTemplate.Height = 24
        Me.dgItemList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgItemList.Size = New System.Drawing.Size(620, 348)
        Me.dgItemList.TabIndex = 0
        '
        'cmsStock
        '
        Me.cmsStock.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.調貨ToolStripMenuItem})
        Me.cmsStock.Name = "cmsStock"
        Me.cmsStock.Size = New System.Drawing.Size(101, 26)
        '
        '調貨ToolStripMenuItem
        '
        Me.調貨ToolStripMenuItem.Name = "調貨ToolStripMenuItem"
        Me.調貨ToolStripMenuItem.Size = New System.Drawing.Size(100, 22)
        Me.調貨ToolStripMenuItem.Text = "調貨"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.進貨ToolStripMenuItem, Me.調貨ToolStripMenuItem1, Me.cbStock, Me.列印PToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(640, 28)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        '進貨ToolStripMenuItem
        '
        Me.進貨ToolStripMenuItem.Name = "進貨ToolStripMenuItem"
        Me.進貨ToolStripMenuItem.Size = New System.Drawing.Size(44, 24)
        Me.進貨ToolStripMenuItem.Text = "進貨"
        '
        '調貨ToolStripMenuItem1
        '
        Me.調貨ToolStripMenuItem1.Name = "調貨ToolStripMenuItem1"
        Me.調貨ToolStripMenuItem1.Size = New System.Drawing.Size(44, 24)
        Me.調貨ToolStripMenuItem1.Text = "調貨"
        '
        'cbStock
        '
        Me.cbStock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbStock.Name = "cbStock"
        Me.cbStock.Size = New System.Drawing.Size(121, 24)
        '
        '列印PToolStripMenuItem
        '
        Me.列印PToolStripMenuItem.Name = "列印PToolStripMenuItem"
        Me.列印PToolStripMenuItem.Size = New System.Drawing.Size(59, 24)
        Me.列印PToolStripMenuItem.Text = "列印(&P)"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'winStockList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(640, 387)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.dgItemList)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "winStockList"
        Me.Text = "庫存查詢"
        CType(Me.dgItemList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsStock.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgItemList As System.Windows.Forms.DataGridView
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents 進貨ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cbStock As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents 列印PToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsStock As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents 調貨ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 調貨ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
End Class
