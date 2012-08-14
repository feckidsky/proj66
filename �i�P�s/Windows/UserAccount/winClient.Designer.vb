<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class winClient
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
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.txtReceive = New System.Windows.Forms.TextBox
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.txtError = New System.Windows.Forms.TextBox
        Me.ckSimpleMsg = New System.Windows.Forms.CheckBox
        Me.cbClient = New System.Windows.Forms.ComboBox
        Me.ckShowCheckMessage = New System.Windows.Forms.CheckBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(2, 43)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(840, 573)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.txtReceive)
        Me.TabPage1.Location = New System.Drawing.Point(4, 21)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(832, 548)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "接收內容"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'txtReceive
        '
        Me.txtReceive.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtReceive.Location = New System.Drawing.Point(4, 4)
        Me.txtReceive.Multiline = True
        Me.txtReceive.Name = "txtReceive"
        Me.txtReceive.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtReceive.Size = New System.Drawing.Size(822, 538)
        Me.txtReceive.TabIndex = 0
        Me.txtReceive.WordWrap = False
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.txtError)
        Me.TabPage2.Location = New System.Drawing.Point(4, 21)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(732, 475)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "錯誤記錄"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'txtError
        '
        Me.txtError.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtError.Location = New System.Drawing.Point(5, 4)
        Me.txtError.Multiline = True
        Me.txtError.Name = "txtError"
        Me.txtError.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtError.Size = New System.Drawing.Size(722, 468)
        Me.txtError.TabIndex = 1
        Me.txtError.WordWrap = False
        '
        'ckSimpleMsg
        '
        Me.ckSimpleMsg.AutoSize = True
        Me.ckSimpleMsg.Checked = True
        Me.ckSimpleMsg.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ckSimpleMsg.Location = New System.Drawing.Point(12, 12)
        Me.ckSimpleMsg.Name = "ckSimpleMsg"
        Me.ckSimpleMsg.Size = New System.Drawing.Size(72, 16)
        Me.ckSimpleMsg.TabIndex = 2
        Me.ckSimpleMsg.Text = "簡易訊息"
        Me.ckSimpleMsg.UseVisualStyleBackColor = True
        '
        'cbClient
        '
        Me.cbClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbClient.FormattingEnabled = True
        Me.cbClient.Location = New System.Drawing.Point(240, 8)
        Me.cbClient.Name = "cbClient"
        Me.cbClient.Size = New System.Drawing.Size(121, 20)
        Me.cbClient.TabIndex = 3
        '
        'ckShowCheckMessage
        '
        Me.ckShowCheckMessage.AutoSize = True
        Me.ckShowCheckMessage.Location = New System.Drawing.Point(102, 10)
        Me.ckShowCheckMessage.Name = "ckShowCheckMessage"
        Me.ckShowCheckMessage.Size = New System.Drawing.Size(120, 16)
        Me.ckShowCheckMessage.TabIndex = 4
        Me.ckShowCheckMessage.Text = "顯示連線測試訊息"
        Me.ckShowCheckMessage.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(423, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(125, 12)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "資料筆數：接收，發送"
        '
        'winClient
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(846, 628)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ckShowCheckMessage)
        Me.Controls.Add(Me.cbClient)
        Me.Controls.Add(Me.ckSimpleMsg)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "winClient"
        Me.Text = "winClient"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents txtReceive As System.Windows.Forms.TextBox
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents txtError As System.Windows.Forms.TextBox
    Friend WithEvents ckSimpleMsg As System.Windows.Forms.CheckBox
    Friend WithEvents cbClient As System.Windows.Forms.ComboBox
    Friend WithEvents ckShowCheckMessage As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
