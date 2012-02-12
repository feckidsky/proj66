<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class winStockIn
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
        Me.txtLabel = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtDate = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtIMEI = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtNote = New System.Windows.Forms.TextBox
        Me.txtCost = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtPrice = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtNumber = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.btOK = New System.Windows.Forms.Button
        Me.btSelectSupplier = New System.Windows.Forms.Button
        Me.btSelectGoods = New System.Windows.Forms.Button
        Me.btResetGoods = New System.Windows.Forms.Button
        Me.btResetSupplier = New System.Windows.Forms.Button
        Me.btUpdateCostByHistory = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(28, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "識別碼"
        '
        'txtLabel
        '
        Me.txtLabel.Location = New System.Drawing.Point(75, 29)
        Me.txtLabel.Name = "txtLabel"
        Me.txtLabel.Size = New System.Drawing.Size(127, 22)
        Me.txtLabel.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(28, 66)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 12)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "商品"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(28, 98)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 12)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "供應商"
        '
        'txtDate
        '
        Me.txtDate.Location = New System.Drawing.Point(284, 29)
        Me.txtDate.Name = "txtDate"
        Me.txtDate.Size = New System.Drawing.Size(127, 22)
        Me.txtDate.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(225, 32)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 12)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "進貨日期"
        '
        'txtIMEI
        '
        Me.txtIMEI.Location = New System.Drawing.Point(77, 128)
        Me.txtIMEI.Name = "txtIMEI"
        Me.txtIMEI.Size = New System.Drawing.Size(127, 22)
        Me.txtIMEI.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(30, 131)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(30, 12)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "IMEI"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(31, 213)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 12)
        Me.Label6.TabIndex = 18
        Me.Label6.Text = "備註"
        '
        'txtNote
        '
        Me.txtNote.Location = New System.Drawing.Point(32, 237)
        Me.txtNote.Multiline = True
        Me.txtNote.Name = "txtNote"
        Me.txtNote.Size = New System.Drawing.Size(394, 77)
        Me.txtNote.TabIndex = 11
        '
        'txtCost
        '
        Me.txtCost.Location = New System.Drawing.Point(77, 168)
        Me.txtCost.Name = "txtCost"
        Me.txtCost.Size = New System.Drawing.Size(127, 22)
        Me.txtCost.TabIndex = 8
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(30, 171)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 12)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "進貨價"
        '
        'txtPrice
        '
        Me.txtPrice.Enabled = False
        Me.txtPrice.Location = New System.Drawing.Point(286, 165)
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.Size = New System.Drawing.Size(127, 22)
        Me.txtPrice.TabIndex = 9
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(239, 168)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(29, 12)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "定價"
        '
        'txtNumber
        '
        Me.txtNumber.Location = New System.Drawing.Point(286, 128)
        Me.txtNumber.Name = "txtNumber"
        Me.txtNumber.Size = New System.Drawing.Size(127, 22)
        Me.txtNumber.TabIndex = 7
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(239, 131)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(29, 12)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "數量"
        '
        'btOK
        '
        Me.btOK.Location = New System.Drawing.Point(351, 337)
        Me.btOK.Name = "btOK"
        Me.btOK.Size = New System.Drawing.Size(75, 23)
        Me.btOK.TabIndex = 12
        Me.btOK.Text = "新增"
        Me.btOK.UseVisualStyleBackColor = True
        '
        'btSelectSupplier
        '
        Me.btSelectSupplier.Location = New System.Drawing.Point(77, 93)
        Me.btSelectSupplier.Name = "btSelectSupplier"
        Me.btSelectSupplier.Size = New System.Drawing.Size(284, 23)
        Me.btSelectSupplier.TabIndex = 4
        Me.btSelectSupplier.Text = "新增"
        Me.btSelectSupplier.UseVisualStyleBackColor = True
        '
        'btSelectGoods
        '
        Me.btSelectGoods.Location = New System.Drawing.Point(75, 61)
        Me.btSelectGoods.Name = "btSelectGoods"
        Me.btSelectGoods.Size = New System.Drawing.Size(286, 23)
        Me.btSelectGoods.TabIndex = 2
        Me.btSelectGoods.Text = "新增"
        Me.btSelectGoods.UseVisualStyleBackColor = True
        '
        'btResetGoods
        '
        Me.btResetGoods.Location = New System.Drawing.Point(367, 61)
        Me.btResetGoods.Name = "btResetGoods"
        Me.btResetGoods.Size = New System.Drawing.Size(46, 23)
        Me.btResetGoods.TabIndex = 3
        Me.btResetGoods.Text = "清除"
        Me.btResetGoods.UseVisualStyleBackColor = True
        '
        'btResetSupplier
        '
        Me.btResetSupplier.Location = New System.Drawing.Point(367, 93)
        Me.btResetSupplier.Name = "btResetSupplier"
        Me.btResetSupplier.Size = New System.Drawing.Size(46, 23)
        Me.btResetSupplier.TabIndex = 5
        Me.btResetSupplier.Text = "清除"
        Me.btResetSupplier.UseVisualStyleBackColor = True
        '
        'btUpdateCostByHistory
        '
        Me.btUpdateCostByHistory.Location = New System.Drawing.Point(107, 196)
        Me.btUpdateCostByHistory.Name = "btUpdateCostByHistory"
        Me.btUpdateCostByHistory.Size = New System.Drawing.Size(97, 23)
        Me.btUpdateCostByHistory.TabIndex = 10
        Me.btUpdateCostByHistory.Text = "與商品進價同步"
        Me.btUpdateCostByHistory.UseVisualStyleBackColor = True
        '
        'winStockIn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(440, 373)
        Me.Controls.Add(Me.btUpdateCostByHistory)
        Me.Controls.Add(Me.btResetSupplier)
        Me.Controls.Add(Me.btResetGoods)
        Me.Controls.Add(Me.btSelectGoods)
        Me.Controls.Add(Me.btSelectSupplier)
        Me.Controls.Add(Me.btOK)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtNote)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtNumber)
        Me.Controls.Add(Me.txtPrice)
        Me.Controls.Add(Me.txtCost)
        Me.Controls.Add(Me.txtIMEI)
        Me.Controls.Add(Me.txtDate)
        Me.Controls.Add(Me.txtLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.Name = "winStockIn"
        Me.Text = "進貨"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtLabel As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDate As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtIMEI As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtNote As System.Windows.Forms.TextBox
    Friend WithEvents txtCost As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtPrice As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btOK As System.Windows.Forms.Button
    Friend WithEvents btSelectSupplier As System.Windows.Forms.Button
    Friend WithEvents btSelectGoods As System.Windows.Forms.Button
    Friend WithEvents btResetGoods As System.Windows.Forms.Button
    Friend WithEvents btResetSupplier As System.Windows.Forms.Button
    Friend WithEvents btUpdateCostByHistory As System.Windows.Forms.Button
End Class
