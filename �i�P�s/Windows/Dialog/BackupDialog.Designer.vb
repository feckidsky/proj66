<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BackupDialog
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
        Me.OK_Button = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btDir = New System.Windows.Forms.Button
        Me.ckDir = New System.Windows.Forms.CheckBox
        Me.ckEmail = New System.Windows.Forms.CheckBox
        Me.txtDir = New System.Windows.Forms.TextBox
        Me.txtEmail = New System.Windows.Forms.TextBox
        Me.btBackup = New System.Windows.Forms.Button
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.ProgressBar1 = New System.Windows.Forms.ToolStripProgressBar
        Me.lbStatu = New System.Windows.Forms.ToolStripStatusLabel
        Me.Button1 = New System.Windows.Forms.Button
        Me.OpenBackupFileDialog = New System.Windows.Forms.OpenFileDialog
        Me.GroupBox1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK_Button.Location = New System.Drawing.Point(242, 126)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(83, 33)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "關閉"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btDir)
        Me.GroupBox1.Controls.Add(Me.ckDir)
        Me.GroupBox1.Controls.Add(Me.ckEmail)
        Me.GroupBox1.Controls.Add(Me.txtDir)
        Me.GroupBox1.Controls.Add(Me.txtEmail)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(312, 109)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "備份選項"
        '
        'btDir
        '
        Me.btDir.Location = New System.Drawing.Point(223, 12)
        Me.btDir.Name = "btDir"
        Me.btDir.Size = New System.Drawing.Size(75, 23)
        Me.btDir.TabIndex = 3
        Me.btDir.Text = "路徑"
        Me.btDir.UseVisualStyleBackColor = True
        '
        'ckDir
        '
        Me.ckDir.AutoSize = True
        Me.ckDir.Location = New System.Drawing.Point(16, 43)
        Me.ckDir.Name = "ckDir"
        Me.ckDir.Size = New System.Drawing.Size(60, 16)
        Me.ckDir.TabIndex = 2
        Me.ckDir.Text = "資料夾"
        Me.ckDir.UseVisualStyleBackColor = True
        '
        'ckEmail
        '
        Me.ckEmail.AutoSize = True
        Me.ckEmail.Location = New System.Drawing.Point(16, 75)
        Me.ckEmail.Name = "ckEmail"
        Me.ckEmail.Size = New System.Drawing.Size(51, 16)
        Me.ckEmail.TabIndex = 2
        Me.ckEmail.Text = "Email"
        Me.ckEmail.UseVisualStyleBackColor = True
        '
        'txtDir
        '
        Me.txtDir.Location = New System.Drawing.Point(73, 41)
        Me.txtDir.Name = "txtDir"
        Me.txtDir.Size = New System.Drawing.Size(225, 22)
        Me.txtDir.TabIndex = 1
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(73, 69)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(225, 22)
        Me.txtEmail.TabIndex = 1
        '
        'btBackup
        '
        Me.btBackup.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btBackup.Location = New System.Drawing.Point(153, 126)
        Me.btBackup.Name = "btBackup"
        Me.btBackup.Size = New System.Drawing.Size(83, 33)
        Me.btBackup.TabIndex = 0
        Me.btBackup.Text = "備份"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ProgressBar1, Me.lbStatu})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 168)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(336, 22)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 2
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(200, 19)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.Visible = False
        '
        'lbStatu
        '
        Me.lbStatu.Name = "lbStatu"
        Me.lbStatu.Size = New System.Drawing.Size(321, 17)
        Me.lbStatu.Spring = True
        Me.lbStatu.Text = "狀態"
        Me.lbStatu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(69, 126)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(78, 33)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "還原"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'OpenBackupFileDialog
        '
        Me.OpenBackupFileDialog.Filter = "資料備份檔|*.bk"
        '
        'BackupDialog
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(336, 190)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btBackup)
        Me.Controls.Add(Me.OK_Button)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "BackupDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "備份"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents ckDir As System.Windows.Forms.CheckBox
    Friend WithEvents ckEmail As System.Windows.Forms.CheckBox
    Friend WithEvents txtDir As System.Windows.Forms.TextBox
    Friend WithEvents btBackup As System.Windows.Forms.Button
    Friend WithEvents btDir As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lbStatu As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents OpenBackupFileDialog As System.Windows.Forms.OpenFileDialog

End Class
