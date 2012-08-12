<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AgendumBox
    Inherits System.Windows.Forms.UserControl

    'UserControl 覆寫 Dispose 以清除元件清單。
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
        Me.txtMessage = New System.Windows.Forms.TextBox
        Me.txtKind = New System.Windows.Forms.TextBox
        Me.dtpStart = New System.Windows.Forms.DateTimePicker
        Me.ckFinished = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'txtMessage
        '
        Me.txtMessage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMessage.Location = New System.Drawing.Point(3, 31)
        Me.txtMessage.Multiline = True
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtMessage.Size = New System.Drawing.Size(436, 200)
        Me.txtMessage.TabIndex = 0
        '
        'txtKind
        '
        Me.txtKind.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtKind.Location = New System.Drawing.Point(3, 3)
        Me.txtKind.Name = "txtKind"
        Me.txtKind.Size = New System.Drawing.Size(237, 22)
        Me.txtKind.TabIndex = 1
        '
        'dtpStart
        '
        Me.dtpStart.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpStart.Location = New System.Drawing.Point(246, 3)
        Me.dtpStart.Name = "dtpStart"
        Me.dtpStart.Size = New System.Drawing.Size(139, 22)
        Me.dtpStart.TabIndex = 2
        '
        'ckFinished
        '
        Me.ckFinished.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ckFinished.AutoSize = True
        Me.ckFinished.Location = New System.Drawing.Point(391, 9)
        Me.ckFinished.Name = "ckFinished"
        Me.ckFinished.Size = New System.Drawing.Size(48, 16)
        Me.ckFinished.TabIndex = 3
        Me.ckFinished.Text = "完成"
        Me.ckFinished.UseVisualStyleBackColor = True
        '
        'AgendumBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.ckFinished)
        Me.Controls.Add(Me.dtpStart)
        Me.Controls.Add(Me.txtKind)
        Me.Controls.Add(Me.txtMessage)
        Me.Name = "AgendumBox"
        Me.Size = New System.Drawing.Size(442, 234)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtMessage As System.Windows.Forms.TextBox
    Friend WithEvents txtKind As System.Windows.Forms.TextBox
    Friend WithEvents dtpStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents ckFinished As System.Windows.Forms.CheckBox

End Class
