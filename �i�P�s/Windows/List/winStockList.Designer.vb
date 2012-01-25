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
        Me.dgItemList = New System.Windows.Forms.DataGridView
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.更新ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.進貨ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        CType(Me.dgItemList, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.dgItemList.Location = New System.Drawing.Point(10, 28)
        Me.dgItemList.Name = "dgItemList"
        Me.dgItemList.ReadOnly = True
        Me.dgItemList.RowTemplate.Height = 24
        Me.dgItemList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgItemList.Size = New System.Drawing.Size(620, 322)
        Me.dgItemList.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.更新ToolStripMenuItem, Me.進貨ToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(640, 24)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        '更新ToolStripMenuItem
        '
        Me.更新ToolStripMenuItem.Name = "更新ToolStripMenuItem"
        Me.更新ToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.更新ToolStripMenuItem.Text = "更新"
        Me.更新ToolStripMenuItem.Visible = False
        '
        '進貨ToolStripMenuItem
        '
        Me.進貨ToolStripMenuItem.Name = "進貨ToolStripMenuItem"
        Me.進貨ToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.進貨ToolStripMenuItem.Text = "進貨"
        '
        'winStockList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(640, 361)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.dgItemList)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "winStockList"
        Me.Text = "庫存查詢"
        CType(Me.dgItemList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgItemList As System.Windows.Forms.DataGridView
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents 更新ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 進貨ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
