<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class winStockInLog
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
        Me.dgStockLog = New System.Windows.Forms.DataGridView
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.進貨ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.dtpEnd = New System.Windows.Forms.DateTimePicker
        Me.dtpStart = New System.Windows.Forms.DateTimePicker
        Me.rToday = New System.Windows.Forms.RadioButton
        Me.rUserTime = New System.Windows.Forms.RadioButton
        Me.r30Day = New System.Windows.Forms.RadioButton
        Me.cmsEdit = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.進貨AToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.刪除DToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.修改CToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        CType(Me.dgStockLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.cmsEdit.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgStockLog
        '
        Me.dgStockLog.AllowUserToAddRows = False
        Me.dgStockLog.AllowUserToDeleteRows = False
        Me.dgStockLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgStockLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgStockLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgStockLog.ContextMenuStrip = Me.cmsEdit
        Me.dgStockLog.Location = New System.Drawing.Point(12, 91)
        Me.dgStockLog.Name = "dgStockLog"
        Me.dgStockLog.ReadOnly = True
        Me.dgStockLog.RowTemplate.Height = 24
        Me.dgStockLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgStockLog.Size = New System.Drawing.Size(785, 402)
        Me.dgStockLog.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.進貨ToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(809, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        '進貨ToolStripMenuItem
        '
        Me.進貨ToolStripMenuItem.Name = "進貨ToolStripMenuItem"
        Me.進貨ToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.進貨ToolStripMenuItem.Text = "進貨"
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
        Me.GroupBox1.Size = New System.Drawing.Size(785, 46)
        Me.GroupBox1.TabIndex = 2
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
        'cmsEdit
        '
        Me.cmsEdit.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.進貨AToolStripMenuItem, Me.修改CToolStripMenuItem, Me.刪除DToolStripMenuItem})
        Me.cmsEdit.Name = "cmsEdit"
        Me.cmsEdit.Size = New System.Drawing.Size(153, 92)
        '
        '進貨AToolStripMenuItem
        '
        Me.進貨AToolStripMenuItem.Name = "進貨AToolStripMenuItem"
        Me.進貨AToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.進貨AToolStripMenuItem.Text = "進貨(&A)"
        '
        '刪除DToolStripMenuItem
        '
        Me.刪除DToolStripMenuItem.Name = "刪除DToolStripMenuItem"
        Me.刪除DToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.刪除DToolStripMenuItem.Text = "刪除(&D)"
        '
        '修改CToolStripMenuItem
        '
        Me.修改CToolStripMenuItem.Name = "修改CToolStripMenuItem"
        Me.修改CToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.修改CToolStripMenuItem.Text = "修改(&C)"
        '
        'winStockInLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(809, 505)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgStockLog)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "winStockInLog"
        Me.Text = "進貨記錄"
        CType(Me.dgStockLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.cmsEdit.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgStockLog As System.Windows.Forms.DataGridView
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents 進貨ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rToday As System.Windows.Forms.RadioButton
    Friend WithEvents rUserTime As System.Windows.Forms.RadioButton
    Friend WithEvents r30Day As System.Windows.Forms.RadioButton
    Friend WithEvents dtpStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmsEdit As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents 進貨AToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 修改CToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 刪除DToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
