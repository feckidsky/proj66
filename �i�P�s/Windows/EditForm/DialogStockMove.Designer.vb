<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DialogStockMove
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtName = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtNumber = New System.Windows.Forms.TextBox
        Me.btOK = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtSource = New System.Windows.Forms.TextBox
        Me.btCancel = New System.Windows.Forms.Button
        Me.cbDestine = New System.Windows.Forms.ComboBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 54)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "品名"
        '
        'txtName
        '
        Me.txtName.BackColor = System.Drawing.SystemColors.Window
        Me.txtName.Location = New System.Drawing.Point(51, 51)
        Me.txtName.Name = "txtName"
        Me.txtName.ReadOnly = True
        Me.txtName.Size = New System.Drawing.Size(100, 22)
        Me.txtName.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(170, 54)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 12)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "數量"
        '
        'txtNumber
        '
        Me.txtNumber.Location = New System.Drawing.Point(205, 51)
        Me.txtNumber.Name = "txtNumber"
        Me.txtNumber.Size = New System.Drawing.Size(100, 22)
        Me.txtNumber.TabIndex = 1
        '
        'btOK
        '
        Me.btOK.Location = New System.Drawing.Point(242, 122)
        Me.btOK.Name = "btOK"
        Me.btOK.Size = New System.Drawing.Size(75, 23)
        Me.btOK.TabIndex = 2
        Me.btOK.Text = "確認"
        Me.btOK.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 12)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "來源"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(170, 21)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 12)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "目地"
        '
        'txtSource
        '
        Me.txtSource.BackColor = System.Drawing.SystemColors.Window
        Me.txtSource.Location = New System.Drawing.Point(51, 18)
        Me.txtSource.Name = "txtSource"
        Me.txtSource.ReadOnly = True
        Me.txtSource.Size = New System.Drawing.Size(100, 22)
        Me.txtSource.TabIndex = 1
        '
        'btCancel
        '
        Me.btCancel.Location = New System.Drawing.Point(152, 122)
        Me.btCancel.Name = "btCancel"
        Me.btCancel.Size = New System.Drawing.Size(75, 23)
        Me.btCancel.TabIndex = 2
        Me.btCancel.Text = "取消"
        Me.btCancel.UseVisualStyleBackColor = True
        '
        'cbDestine
        '
        Me.cbDestine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbDestine.FormattingEnabled = True
        Me.cbDestine.Location = New System.Drawing.Point(205, 18)
        Me.cbDestine.Name = "cbDestine"
        Me.cbDestine.Size = New System.Drawing.Size(121, 20)
        Me.cbDestine.TabIndex = 5
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cbDestine)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtSource)
        Me.GroupBox1.Controls.Add(Me.txtNumber)
        Me.GroupBox1.Controls.Add(Me.txtName)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(337, 92)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "內容"
        '
        'DialogStockMove
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(363, 160)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btCancel)
        Me.Controls.Add(Me.btOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "DialogStockMove"
        Me.Text = "調貨"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtNumber As System.Windows.Forms.TextBox
    Friend WithEvents btOK As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtSource As System.Windows.Forms.TextBox
    Friend WithEvents btCancel As System.Windows.Forms.Button
    Friend WithEvents cbDestine As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
End Class
