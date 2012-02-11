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
        CType(Me.dgList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsStockMove.SuspendLayout()
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
        Me.dgList.Location = New System.Drawing.Point(12, 27)
        Me.dgList.Name = "dgList"
        Me.dgList.ReadOnly = True
        Me.dgList.RowTemplate.Height = 24
        Me.dgList.Size = New System.Drawing.Size(711, 321)
        Me.dgList.TabIndex = 0
        '
        'cmsStockMove
        '
        Me.cmsStockMove.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.欄位顯示ToolStripMenuItem, Me.ToolStripSeparator1, Me.tsOut, Me.tsIn, Me.tsCancel})
        Me.cmsStockMove.Name = "cmsStockMove"
        Me.cmsStockMove.Size = New System.Drawing.Size(153, 120)
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
        Me.tsOut.Size = New System.Drawing.Size(152, 22)
        Me.tsOut.Text = "調出"
        '
        'tsIn
        '
        Me.tsIn.Name = "tsIn"
        Me.tsIn.Size = New System.Drawing.Size(152, 22)
        Me.tsIn.Text = "接收"
        '
        'tsCancel
        '
        Me.tsCancel.Name = "tsCancel"
        Me.tsCancel.Size = New System.Drawing.Size(124, 22)
        Me.tsCancel.Text = "取消"
        '
        'winStockMoveList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(735, 360)
        Me.Controls.Add(Me.dgList)
        Me.Name = "winStockMoveList"
        Me.Text = "調貨清單"
        CType(Me.dgList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsStockMove.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgList As System.Windows.Forms.DataGridView
    Friend WithEvents cmsStockMove As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsOut As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsIn As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsCancel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 欄位顯示ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
End Class
