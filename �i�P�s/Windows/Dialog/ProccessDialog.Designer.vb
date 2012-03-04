<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProgressDialog
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
        Me.TotalProgressBar = New System.Windows.Forms.ProgressBar
        Me.btCancel = New System.Windows.Forms.Button
        Me.lbPercent = New System.Windows.Forms.Label
        Me.lbMessage = New System.Windows.Forms.Label
        Me.PartProgressBar = New System.Windows.Forms.ProgressBar
        Me.lbPartMessage = New System.Windows.Forms.Label
        Me.lbPartPercent = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'TotalProgressBar
        '
        Me.TotalProgressBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TotalProgressBar.Location = New System.Drawing.Point(12, 25)
        Me.TotalProgressBar.Name = "TotalProgressBar"
        Me.TotalProgressBar.Size = New System.Drawing.Size(312, 23)
        Me.TotalProgressBar.TabIndex = 0
        '
        'btCancel
        '
        Me.btCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btCancel.Location = New System.Drawing.Point(237, 110)
        Me.btCancel.Name = "btCancel"
        Me.btCancel.Size = New System.Drawing.Size(75, 23)
        Me.btCancel.TabIndex = 1
        Me.btCancel.Text = "取消"
        Me.btCancel.UseVisualStyleBackColor = True
        '
        'lbPercent
        '
        Me.lbPercent.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbPercent.AutoSize = True
        Me.lbPercent.Location = New System.Drawing.Point(292, 9)
        Me.lbPercent.Name = "lbPercent"
        Me.lbPercent.Size = New System.Drawing.Size(20, 12)
        Me.lbPercent.TabIndex = 2
        Me.lbPercent.Text = "0%"
        '
        'lbMessage
        '
        Me.lbMessage.AutoSize = True
        Me.lbMessage.Location = New System.Drawing.Point(10, 9)
        Me.lbMessage.Name = "lbMessage"
        Me.lbMessage.Size = New System.Drawing.Size(14, 12)
        Me.lbMessage.TabIndex = 2
        Me.lbMessage.Text = "..."
        '
        'PartProgressBar
        '
        Me.PartProgressBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PartProgressBar.Location = New System.Drawing.Point(12, 75)
        Me.PartProgressBar.Name = "PartProgressBar"
        Me.PartProgressBar.Size = New System.Drawing.Size(312, 23)
        Me.PartProgressBar.TabIndex = 0
        '
        'lbPartMessage
        '
        Me.lbPartMessage.AutoSize = True
        Me.lbPartMessage.Location = New System.Drawing.Point(12, 60)
        Me.lbPartMessage.Name = "lbPartMessage"
        Me.lbPartMessage.Size = New System.Drawing.Size(14, 12)
        Me.lbPartMessage.TabIndex = 2
        Me.lbPartMessage.Text = "..."
        '
        'lbPartPercent
        '
        Me.lbPartPercent.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbPartPercent.AutoSize = True
        Me.lbPartPercent.Location = New System.Drawing.Point(292, 60)
        Me.lbPartPercent.Name = "lbPartPercent"
        Me.lbPartPercent.Size = New System.Drawing.Size(20, 12)
        Me.lbPartPercent.TabIndex = 2
        Me.lbPartPercent.Text = "0%"
        '
        'ProgressDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(341, 145)
        Me.Controls.Add(Me.lbPartMessage)
        Me.Controls.Add(Me.lbMessage)
        Me.Controls.Add(Me.lbPartPercent)
        Me.Controls.Add(Me.lbPercent)
        Me.Controls.Add(Me.btCancel)
        Me.Controls.Add(Me.PartProgressBar)
        Me.Controls.Add(Me.TotalProgressBar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "ProgressDialog"
        Me.Text = "進度"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TotalProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents btCancel As System.Windows.Forms.Button
    Friend WithEvents lbPercent As System.Windows.Forms.Label
    Friend WithEvents lbMessage As System.Windows.Forms.Label
    Friend WithEvents PartProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents lbPartMessage As System.Windows.Forms.Label
    Friend WithEvents lbPartPercent As System.Windows.Forms.Label
End Class
