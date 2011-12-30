<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class winSales
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
        Me.btSelectStock = New System.Windows.Forms.Button
        Me.btAddSupplier = New System.Windows.Forms.Button
        Me.btAdd = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtNote = New System.Windows.Forms.TextBox
        Me.cbSupplier = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cbGoods = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtDate = New System.Windows.Forms.TextBox
        Me.txtLabel = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'btSelectStock
        '
        Me.btSelectStock.Location = New System.Drawing.Point(226, 70)
        Me.btSelectStock.Name = "btSelectStock"
        Me.btSelectStock.Size = New System.Drawing.Size(53, 23)
        Me.btSelectStock.TabIndex = 40
        Me.btSelectStock.Text = "選擇"
        Me.btSelectStock.UseVisualStyleBackColor = True
        '
        'btAddSupplier
        '
        Me.btAddSupplier.Location = New System.Drawing.Point(226, 114)
        Me.btAddSupplier.Name = "btAddSupplier"
        Me.btAddSupplier.Size = New System.Drawing.Size(53, 23)
        Me.btAddSupplier.TabIndex = 41
        Me.btAddSupplier.Text = "新增"
        Me.btAddSupplier.UseVisualStyleBackColor = True
        '
        'btAdd
        '
        Me.btAdd.Location = New System.Drawing.Point(354, 320)
        Me.btAdd.Name = "btAdd"
        Me.btAdd.Size = New System.Drawing.Size(75, 23)
        Me.btAdd.TabIndex = 39
        Me.btAdd.Text = "新增"
        Me.btAdd.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(34, 196)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 12)
        Me.Label6.TabIndex = 38
        Me.Label6.Text = "備註"
        '
        'txtNote
        '
        Me.txtNote.Location = New System.Drawing.Point(35, 220)
        Me.txtNote.Multiline = True
        Me.txtNote.Name = "txtNote"
        Me.txtNote.Size = New System.Drawing.Size(394, 77)
        Me.txtNote.TabIndex = 37
        '
        'cbSupplier
        '
        Me.cbSupplier.FormattingEnabled = True
        Me.cbSupplier.Location = New System.Drawing.Point(80, 116)
        Me.cbSupplier.Name = "cbSupplier"
        Me.cbSupplier.Size = New System.Drawing.Size(127, 20)
        Me.cbSupplier.TabIndex = 35
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(33, 119)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 12)
        Me.Label3.TabIndex = 32
        Me.Label3.Text = "供應商"
        '
        'cbGoods
        '
        Me.cbGoods.FormattingEnabled = True
        Me.cbGoods.Location = New System.Drawing.Point(80, 72)
        Me.cbGoods.Name = "cbGoods"
        Me.cbGoods.Size = New System.Drawing.Size(127, 20)
        Me.cbGoods.TabIndex = 36
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(33, 75)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 12)
        Me.Label2.TabIndex = 33
        Me.Label2.Text = "商品"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(265, 27)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 12)
        Me.Label4.TabIndex = 30
        Me.Label4.Text = "出貨日期"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(33, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "識別碼"
        '
        'txtDate
        '
        Me.txtDate.Location = New System.Drawing.Point(324, 24)
        Me.txtDate.Name = "txtDate"
        Me.txtDate.Size = New System.Drawing.Size(127, 22)
        Me.txtDate.TabIndex = 25
        '
        'txtLabel
        '
        Me.txtLabel.Location = New System.Drawing.Point(80, 24)
        Me.txtLabel.Name = "txtLabel"
        Me.txtLabel.Size = New System.Drawing.Size(127, 22)
        Me.txtLabel.TabIndex = 24
        '
        'winSales
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(644, 405)
        Me.Controls.Add(Me.btSelectStock)
        Me.Controls.Add(Me.btAddSupplier)
        Me.Controls.Add(Me.btAdd)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtNote)
        Me.Controls.Add(Me.cbSupplier)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cbGoods)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtDate)
        Me.Controls.Add(Me.txtLabel)
        Me.Name = "winSales"
        Me.Text = "銷貨單"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btSelectStock As System.Windows.Forms.Button
    Friend WithEvents btAddSupplier As System.Windows.Forms.Button
    Friend WithEvents btAdd As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtNote As System.Windows.Forms.TextBox
    Friend WithEvents cbSupplier As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cbGoods As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtDate As System.Windows.Forms.TextBox
    Friend WithEvents txtLabel As System.Windows.Forms.TextBox
End Class
