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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.進貨ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.銷貨ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.庫存ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.dgSales = New System.Windows.Forms.DataGridView
        Me.cSalesLabel = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cTime = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cPersonnel = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Customer = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cDposit = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cPrice = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.MenuStrip1.SuspendLayout()
        CType(Me.dgSales, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.進貨ToolStripMenuItem, Me.銷貨ToolStripMenuItem, Me.庫存ToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(784, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        '進貨ToolStripMenuItem
        '
        Me.進貨ToolStripMenuItem.Name = "進貨ToolStripMenuItem"
        Me.進貨ToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.進貨ToolStripMenuItem.Text = "進貨"
        '
        '銷貨ToolStripMenuItem
        '
        Me.銷貨ToolStripMenuItem.Name = "銷貨ToolStripMenuItem"
        Me.銷貨ToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.銷貨ToolStripMenuItem.Text = "銷貨"
        '
        '庫存ToolStripMenuItem
        '
        Me.庫存ToolStripMenuItem.Name = "庫存ToolStripMenuItem"
        Me.庫存ToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.庫存ToolStripMenuItem.Text = "庫存"
        '
        'dgSales
        '
        Me.dgSales.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgSales.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cSalesLabel, Me.cTime, Me.cPersonnel, Me.Customer, Me.cDposit, Me.cPrice})
        Me.dgSales.Location = New System.Drawing.Point(12, 27)
        Me.dgSales.Name = "dgSales"
        Me.dgSales.RowTemplate.Height = 24
        Me.dgSales.Size = New System.Drawing.Size(760, 523)
        Me.dgSales.TabIndex = 1
        '
        'cSalesLabel
        '
        Me.cSalesLabel.HeaderText = "單號"
        Me.cSalesLabel.Name = "cSalesLabel"
        Me.cSalesLabel.ReadOnly = True
        '
        'cTime
        '
        Me.cTime.HeaderText = "時間"
        Me.cTime.Name = "cTime"
        Me.cTime.ReadOnly = True
        '
        'cPersonnel
        '
        Me.cPersonnel.HeaderText = "銷售人員"
        Me.cPersonnel.Name = "cPersonnel"
        Me.cPersonnel.ReadOnly = True
        '
        'Customer
        '
        Me.Customer.HeaderText = "客戶"
        Me.Customer.Name = "Customer"
        Me.Customer.ReadOnly = True
        '
        'cDposit
        '
        Me.cDposit.HeaderText = "訂金"
        Me.cDposit.Name = "cDposit"
        Me.cDposit.ReadOnly = True
        '
        'cPrice
        '
        Me.cPrice.HeaderText = "金額"
        Me.cPrice.Name = "cPrice"
        Me.cPrice.ReadOnly = True
        '
        'winMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 562)
        Me.Controls.Add(Me.dgSales)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "winMain"
        Me.Text = "winMain"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.dgSales, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents 進貨ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 銷貨ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 庫存ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents dgSales As System.Windows.Forms.DataGridView
    Friend WithEvents cSalesLabel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cPersonnel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Customer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cDposit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cPrice As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
