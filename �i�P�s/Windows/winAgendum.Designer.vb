<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class winAgendum
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
        Me.dgItem = New System.Windows.Forms.DataGridView
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.ckFinished = New System.Windows.Forms.CheckBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.NumberBox1 = New 進銷存.NumberBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.btUpdate = New System.Windows.Forms.Button
        CType(Me.dgItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.dgItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgItem.Location = New System.Drawing.Point(12, 73)
        Me.dgItem.Name = "DataGridView1"
        Me.dgItem.RowTemplate.Height = 24
        Me.dgItem.Size = New System.Drawing.Size(624, 258)
        Me.dgItem.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btUpdate)
        Me.GroupBox1.Controls.Add(Me.NumberBox1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.ckFinished)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(624, 45)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "選項"
        '
        'ckFinished
        '
        Me.ckFinished.AutoSize = True
        Me.ckFinished.Location = New System.Drawing.Point(18, 19)
        Me.ckFinished.Name = "ckFinished"
        Me.ckFinished.Size = New System.Drawing.Size(120, 16)
        Me.ckFinished.TabIndex = 0
        Me.ckFinished.Text = "顯示已完成的項目"
        Me.ckFinished.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(199, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 12)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "最大筆數"
        '
        'NumberBox1
        '
        Me.NumberBox1.Location = New System.Drawing.Point(258, 17)
        Me.NumberBox1.Name = "NumberBox1"
        Me.NumberBox1.Size = New System.Drawing.Size(35, 22)
        Me.NumberBox1.TabIndex = 2
        Me.NumberBox1.Text = "20"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(299, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(17, 12)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "筆"
        '
        'btUpdate
        '
        Me.btUpdate.Location = New System.Drawing.Point(342, 14)
        Me.btUpdate.Name = "btUpdate"
        Me.btUpdate.Size = New System.Drawing.Size(75, 23)
        Me.btUpdate.TabIndex = 3
        Me.btUpdate.Text = "查詢"
        Me.btUpdate.UseVisualStyleBackColor = True
        '
        'winAgendum
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(648, 343)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgItem)
        Me.Name = "winAgendum"
        Me.Text = "待辦事項"
        CType(Me.dgItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgItem As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btUpdate As System.Windows.Forms.Button
    Friend WithEvents NumberBox1 As 進銷存.NumberBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ckFinished As System.Windows.Forms.CheckBox
End Class
