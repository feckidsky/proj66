<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class winSalesGoodsList
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
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.欄位顯示CToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cbKeyWord = New 進銷存.FilterComboBox
        Me.btRead = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.ckTime = New System.Windows.Forms.CheckBox
        Me.dtpEnd = New System.Windows.Forms.DateTimePicker
        Me.dtpStart = New System.Windows.Forms.DateTimePicker
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.lbResult = New System.Windows.Forms.ToolStripStatusLabel
        Me.開啟銷貨SToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        CType(Me.dgItemList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
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
        Me.dgItemList.ContextMenuStrip = Me.ContextMenuStrip1
        Me.dgItemList.Location = New System.Drawing.Point(12, 99)
        Me.dgItemList.Name = "dgItemList"
        Me.dgItemList.ReadOnly = True
        Me.dgItemList.RowTemplate.Height = 24
        Me.dgItemList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgItemList.Size = New System.Drawing.Size(751, 344)
        Me.dgItemList.TabIndex = 1
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.欄位顯示CToolStripMenuItem, Me.開啟銷貨SToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(153, 70)
        '
        '欄位顯示CToolStripMenuItem
        '
        Me.欄位顯示CToolStripMenuItem.Name = "欄位顯示CToolStripMenuItem"
        Me.欄位顯示CToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.欄位顯示CToolStripMenuItem.Text = "欄位顯示(&C)"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.cbKeyWord)
        Me.GroupBox1.Controls.Add(Me.btRead)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.ckTime)
        Me.GroupBox1.Controls.Add(Me.dtpEnd)
        Me.GroupBox1.Controls.Add(Me.dtpStart)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(751, 90)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "查詢條件"
        '
        'cbKeyWord
        '
        Me.cbKeyWord.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbKeyWord.FormattingEnabled = True
        Me.cbKeyWord.Location = New System.Drawing.Point(69, 21)
        Me.cbKeyWord.Name = "cbKeyWord"
        Me.cbKeyWord.Size = New System.Drawing.Size(655, 20)
        Me.cbKeyWord.TabIndex = 9
        '
        'btRead
        '
        Me.btRead.Location = New System.Drawing.Point(649, 55)
        Me.btRead.Name = "btRead"
        Me.btRead.Size = New System.Drawing.Size(75, 23)
        Me.btRead.TabIndex = 8
        Me.btRead.Text = "查詢"
        Me.btRead.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(22, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 12)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "關鍵字"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(268, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(11, 12)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "~"
        '
        'ckTime
        '
        Me.ckTime.AutoSize = True
        Me.ckTime.Location = New System.Drawing.Point(69, 56)
        Me.ckTime.Name = "ckTime"
        Me.ckTime.Size = New System.Drawing.Size(48, 16)
        Me.ckTime.TabIndex = 4
        Me.ckTime.Text = "時間"
        Me.ckTime.UseVisualStyleBackColor = True
        '
        'dtpEnd
        '
        Me.dtpEnd.Location = New System.Drawing.Point(285, 53)
        Me.dtpEnd.Name = "dtpEnd"
        Me.dtpEnd.Size = New System.Drawing.Size(139, 22)
        Me.dtpEnd.TabIndex = 3
        '
        'dtpStart
        '
        Me.dtpStart.Location = New System.Drawing.Point(123, 53)
        Me.dtpStart.Name = "dtpStart"
        Me.dtpStart.Size = New System.Drawing.Size(139, 22)
        Me.dtpStart.TabIndex = 2
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lbResult})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 446)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(775, 22)
        Me.StatusStrip1.TabIndex = 3
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lbResult
        '
        Me.lbResult.Name = "lbResult"
        Me.lbResult.Size = New System.Drawing.Size(56, 17)
        Me.lbResult.Text = "查詢結果"
        '
        '開啟銷貨SToolStripMenuItem
        '
        Me.開啟銷貨SToolStripMenuItem.Name = "開啟銷貨SToolStripMenuItem"
        Me.開啟銷貨SToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.開啟銷貨SToolStripMenuItem.Text = "開啟銷貨單(&S)"
        '
        'winSalesGoodsList
        '
        Me.AcceptButton = Me.btRead
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(775, 468)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgItemList)
        Me.Name = "winSalesGoodsList"
        Me.Text = "銷貨商品清單"
        CType(Me.dgItemList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgItemList As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ckTime As System.Windows.Forms.CheckBox
    Friend WithEvents dtpEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btRead As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lbResult As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents cbKeyWord As 進銷存.FilterComboBox
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents 欄位顯示CToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 開啟銷貨SToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
