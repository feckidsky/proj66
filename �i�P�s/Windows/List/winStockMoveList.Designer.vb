<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class winStockMoveList
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
        Me.dgList = New System.Windows.Forms.DataGridView
        Me.cmsStockMove = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.欄位顯示ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.tsOut = New System.Windows.Forms.ToolStripMenuItem
        Me.tsIn = New System.Windows.Forms.ToolStripMenuItem
        Me.tsCancel = New System.Windows.Forms.ToolStripMenuItem
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.dtpEnd = New System.Windows.Forms.DateTimePicker
        Me.dtpStart = New System.Windows.Forms.DateTimePicker
        Me.rToday = New System.Windows.Forms.RadioButton
        Me.rUserTime = New System.Windows.Forms.RadioButton
        Me.r30Day = New System.Windows.Forms.RadioButton
        CType(Me.dgList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsStockMove.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgList
        '
        Me.dgList.AllowUserToAddRows = False
        Me.dgList.AllowUserToDeleteRows = False
        Me.dgList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgList.ContextMenuStrip = Me.cmsStockMove
        Me.dgList.Location = New System.Drawing.Point(12, 76)
        Me.dgList.Name = "dgList"
        Me.dgList.ReadOnly = True
        Me.dgList.RowTemplate.Height = 24
        Me.dgList.Size = New System.Drawing.Size(772, 311)
        Me.dgList.TabIndex = 0
        '
        'cmsStockMove
        '
        Me.cmsStockMove.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.欄位顯示ToolStripMenuItem, Me.ToolStripSeparator1, Me.tsOut, Me.tsIn, Me.tsCancel})
        Me.cmsStockMove.Name = "cmsStockMove"
        Me.cmsStockMove.Size = New System.Drawing.Size(125, 98)
        '
        '欄位顯示ToolStripMenuItem
        '
        Me.欄位顯示ToolStripMenuItem.Name = "欄位顯示ToolStripMenuItem"
        Me.欄位顯示ToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.欄位顯示ToolStripMenuItem.Text = "欄位顯示"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(121, 6)
        '
        'tsOut
        '
        Me.tsOut.Name = "tsOut"
        Me.tsOut.Size = New System.Drawing.Size(124, 22)
        Me.tsOut.Text = "調出"
        '
        'tsIn
        '
        Me.tsIn.Name = "tsIn"
        Me.tsIn.Size = New System.Drawing.Size(124, 22)
        Me.tsIn.Text = "接收"
        '
        'tsCancel
        '
        Me.tsCancel.Name = "tsCancel"
        Me.tsCancel.Size = New System.Drawing.Size(124, 22)
        Me.tsCancel.Text = "取消"
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
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(772, 46)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "篩選時間"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(483, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(11, 12)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "~"
        '
        'dtpEnd
        '
        Me.dtpEnd.Location = New System.Drawing.Point(507, 16)
        Me.dtpEnd.Name = "dtpEnd"
        Me.dtpEnd.Size = New System.Drawing.Size(139, 22)
        Me.dtpEnd.TabIndex = 1
        '
        'dtpStart
        '
        Me.dtpStart.Location = New System.Drawing.Point(330, 16)
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
        Me.rUserTime.Location = New System.Drawing.Point(242, 21)
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
        Me.r30Day.Location = New System.Drawing.Point(114, 21)
        Me.r30Day.Name = "r30Day"
        Me.r30Day.Size = New System.Drawing.Size(71, 16)
        Me.r30Day.TabIndex = 0
        Me.r30Day.TabStop = True
        Me.r30Day.Text = "30天以內"
        Me.r30Day.UseVisualStyleBackColor = True
        '
        'winStockMoveList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(796, 399)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgList)
        Me.Name = "winStockMoveList"
        Me.Text = "調貨清單"
        CType(Me.dgList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsStockMove.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgList As System.Windows.Forms.DataGridView
    Friend WithEvents cmsStockMove As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsOut As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsIn As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsCancel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 欄位顯示ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents rToday As System.Windows.Forms.RadioButton
    Friend WithEvents rUserTime As System.Windows.Forms.RadioButton
    Friend WithEvents r30Day As System.Windows.Forms.RadioButton
End Class
