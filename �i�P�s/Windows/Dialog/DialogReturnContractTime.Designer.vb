<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DialogReturnContractTime
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
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.lstHour = New System.Windows.Forms.NumericUpDown
        Me.lstMinute = New System.Windows.Forms.NumericUpDown
        Me.lstSecond = New System.Windows.Forms.NumericUpDown
        Me.Label1 = New System.Windows.Forms.Label
        Me.dtpDate = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.ckEnable = New System.Windows.Forms.CheckBox
        CType(Me.lstHour, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lstMinute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lstSecond, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK_Button.Location = New System.Drawing.Point(73, 165)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(76, 32)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "確定"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(155, 165)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(76, 32)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "取消"
        '
        'lstHour
        '
        Me.lstHour.Location = New System.Drawing.Point(29, 26)
        Me.lstHour.Maximum = New Decimal(New Integer() {23, 0, 0, 0})
        Me.lstHour.Name = "lstHour"
        Me.lstHour.Size = New System.Drawing.Size(44, 27)
        Me.lstHour.TabIndex = 4
        '
        'lstMinute
        '
        Me.lstMinute.Location = New System.Drawing.Point(93, 26)
        Me.lstMinute.Maximum = New Decimal(New Integer() {59, 0, 0, 0})
        Me.lstMinute.Name = "lstMinute"
        Me.lstMinute.Size = New System.Drawing.Size(44, 27)
        Me.lstMinute.TabIndex = 4
        '
        'lstSecond
        '
        Me.lstSecond.Location = New System.Drawing.Point(157, 26)
        Me.lstSecond.Maximum = New Decimal(New Integer() {59, 0, 0, 0})
        Me.lstSecond.Name = "lstSecond"
        Me.lstSecond.Size = New System.Drawing.Size(44, 27)
        Me.lstSecond.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(79, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(12, 16)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = ":"
        '
        'dtpDate
        '
        Me.dtpDate.Location = New System.Drawing.Point(30, 26)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(142, 27)
        Me.dtpDate.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(143, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(12, 16)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = ":"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dtpDate)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 25)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(217, 61)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "日期"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.lstSecond)
        Me.GroupBox2.Controls.Add(Me.lstMinute)
        Me.GroupBox2.Controls.Add(Me.lstHour)
        Me.GroupBox2.Location = New System.Drawing.Point(15, 94)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(216, 62)
        Me.GroupBox2.TabIndex = 8
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "時間"
        '
        'ckEnable
        '
        Me.ckEnable.AutoSize = True
        Me.ckEnable.Location = New System.Drawing.Point(161, 6)
        Me.ckEnable.Name = "ckEnable"
        Me.ckEnable.Size = New System.Drawing.Size(59, 20)
        Me.ckEnable.TabIndex = 9
        Me.ckEnable.Text = "扣佣"
        Me.ckEnable.UseVisualStyleBackColor = True
        '
        'DialogReturnContractTime
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(243, 207)
        Me.Controls.Add(Me.ckEnable)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Cancel_Button)
        Me.Controls.Add(Me.OK_Button)
        Me.Font = New System.Drawing.Font("新細明體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DialogReturnContractTime"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "日期設定"
        CType(Me.lstHour, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lstMinute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lstSecond, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents lstHour As System.Windows.Forms.NumericUpDown
    Friend WithEvents lstMinute As System.Windows.Forms.NumericUpDown
    Friend WithEvents lstSecond As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents ckEnable As System.Windows.Forms.CheckBox

End Class
