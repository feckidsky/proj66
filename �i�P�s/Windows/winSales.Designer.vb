﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
        Me.btAddGood = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtNote = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtOrderDate = New System.Windows.Forms.TextBox
        Me.txtLabel = New System.Windows.Forms.TextBox
        Me.dgList = New System.Windows.Forms.DataGridView
        Me.cLabel = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cKind = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cBrand = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cPrice = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cSPrice = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cNumber = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cSubTotal = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btDelete = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.lbTotal = New System.Windows.Forms.Label
        Me.btOrder = New System.Windows.Forms.Button
        Me.cbPayMode = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtDeposit = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtCustomer = New System.Windows.Forms.TextBox
        Me.txtPersonnel = New System.Windows.Forms.TextBox
        Me.btResetPersonnel = New System.Windows.Forms.Button
        Me.btResetCustomer = New System.Windows.Forms.Button
        Me.txtSalesDate = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btSales = New System.Windows.Forms.Button
        CType(Me.dgList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btAddGood
        '
        Me.btAddGood.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btAddGood.Location = New System.Drawing.Point(407, 311)
        Me.btAddGood.Name = "btAddGood"
        Me.btAddGood.Size = New System.Drawing.Size(75, 23)
        Me.btAddGood.TabIndex = 39
        Me.btAddGood.Text = "新增"
        Me.btAddGood.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(19, 475)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 12)
        Me.Label6.TabIndex = 38
        Me.Label6.Text = "備註"
        '
        'txtNote
        '
        Me.txtNote.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNote.Location = New System.Drawing.Point(21, 490)
        Me.txtNote.Multiline = True
        Me.txtNote.Name = "txtNote"
        Me.txtNote.Size = New System.Drawing.Size(358, 93)
        Me.txtNote.TabIndex = 37
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(23, 53)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 12)
        Me.Label4.TabIndex = 30
        Me.Label4.Text = "訂單日期"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(24, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "識別碼"
        '
        'txtOrderDate
        '
        Me.txtOrderDate.Enabled = False
        Me.txtOrderDate.Location = New System.Drawing.Point(82, 50)
        Me.txtOrderDate.Name = "txtOrderDate"
        Me.txtOrderDate.Size = New System.Drawing.Size(127, 22)
        Me.txtOrderDate.TabIndex = 25
        '
        'txtLabel
        '
        Me.txtLabel.Location = New System.Drawing.Point(82, 21)
        Me.txtLabel.Name = "txtLabel"
        Me.txtLabel.Size = New System.Drawing.Size(127, 22)
        Me.txtLabel.TabIndex = 24
        '
        'dgList
        '
        Me.dgList.AllowUserToAddRows = False
        Me.dgList.AllowUserToDeleteRows = False
        Me.dgList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cLabel, Me.cKind, Me.cBrand, Me.cName, Me.cPrice, Me.cSPrice, Me.cNumber, Me.cSubTotal})
        Me.dgList.Location = New System.Drawing.Point(17, 26)
        Me.dgList.Name = "dgList"
        Me.dgList.RowTemplate.Height = 24
        Me.dgList.Size = New System.Drawing.Size(546, 279)
        Me.dgList.TabIndex = 42
        '
        'cLabel
        '
        Me.cLabel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.cLabel.HeaderText = "商品編號"
        Me.cLabel.Name = "cLabel"
        Me.cLabel.ReadOnly = True
        Me.cLabel.Width = 78
        '
        'cKind
        '
        Me.cKind.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.cKind.HeaderText = "種類"
        Me.cKind.Name = "cKind"
        Me.cKind.Width = 54
        '
        'cBrand
        '
        Me.cBrand.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.cBrand.HeaderText = "廠牌"
        Me.cBrand.Name = "cBrand"
        Me.cBrand.Width = 54
        '
        'cName
        '
        Me.cName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.cName.HeaderText = "品名"
        Me.cName.Name = "cName"
        Me.cName.Width = 54
        '
        'cPrice
        '
        Me.cPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.cPrice.HeaderText = "定價"
        Me.cPrice.Name = "cPrice"
        Me.cPrice.Width = 54
        '
        'cSPrice
        '
        Me.cSPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.cSPrice.HeaderText = "賣價"
        Me.cSPrice.Name = "cSPrice"
        Me.cSPrice.Width = 54
        '
        'cNumber
        '
        Me.cNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.cNumber.HeaderText = "數量"
        Me.cNumber.Name = "cNumber"
        Me.cNumber.Width = 54
        '
        'cSubTotal
        '
        Me.cSubTotal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.cSubTotal.HeaderText = "小計"
        Me.cSubTotal.Name = "cSubTotal"
        Me.cSubTotal.ReadOnly = True
        Me.cSubTotal.Width = 54
        '
        'btDelete
        '
        Me.btDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btDelete.Location = New System.Drawing.Point(488, 311)
        Me.btDelete.Name = "btDelete"
        Me.btDelete.Size = New System.Drawing.Size(75, 23)
        Me.btDelete.TabIndex = 39
        Me.btDelete.Text = "刪除"
        Me.btDelete.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.dgList)
        Me.GroupBox1.Controls.Add(Me.btDelete)
        Me.GroupBox1.Controls.Add(Me.btAddGood)
        Me.GroupBox1.Location = New System.Drawing.Point(21, 129)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(578, 343)
        Me.GroupBox1.TabIndex = 43
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "商品清單"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(413, 483)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 12)
        Me.Label2.TabIndex = 44
        Me.Label2.Text = "合計"
        '
        'lbTotal
        '
        Me.lbTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbTotal.AutoSize = True
        Me.lbTotal.Location = New System.Drawing.Point(470, 483)
        Me.lbTotal.Name = "lbTotal"
        Me.lbTotal.Size = New System.Drawing.Size(11, 12)
        Me.lbTotal.TabIndex = 45
        Me.lbTotal.Text = "0"
        '
        'btOrder
        '
        Me.btOrder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btOrder.Location = New System.Drawing.Point(428, 564)
        Me.btOrder.Name = "btOrder"
        Me.btOrder.Size = New System.Drawing.Size(75, 35)
        Me.btOrder.TabIndex = 46
        Me.btOrder.Text = "修改訂單"
        Me.btOrder.UseVisualStyleBackColor = True
        '
        'cbPayMode
        '
        Me.cbPayMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbPayMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPayMode.FormattingEnabled = True
        Me.cbPayMode.Location = New System.Drawing.Point(472, 531)
        Me.cbPayMode.Name = "cbPayMode"
        Me.cbPayMode.Size = New System.Drawing.Size(100, 20)
        Me.cbPayMode.TabIndex = 47
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(413, 534)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 12)
        Me.Label3.TabIndex = 44
        Me.Label3.Text = "付款方式"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(413, 506)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(29, 12)
        Me.Label5.TabIndex = 44
        Me.Label5.Text = "訂金"
        '
        'txtDeposit
        '
        Me.txtDeposit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDeposit.Location = New System.Drawing.Point(472, 503)
        Me.txtDeposit.Name = "txtDeposit"
        Me.txtDeposit.Size = New System.Drawing.Size(100, 22)
        Me.txtDeposit.TabIndex = 48
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(23, 89)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(53, 12)
        Me.Label7.TabIndex = 29
        Me.Label7.Text = "銷售人員"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(295, 89)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(29, 12)
        Me.Label8.TabIndex = 29
        Me.Label8.Text = "客戶"
        '
        'txtCustomer
        '
        Me.txtCustomer.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustomer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustomer.Location = New System.Drawing.Point(342, 84)
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.ReadOnly = True
        Me.txtCustomer.Size = New System.Drawing.Size(127, 22)
        Me.txtCustomer.TabIndex = 24
        '
        'txtPersonnel
        '
        Me.txtPersonnel.BackColor = System.Drawing.SystemColors.Window
        Me.txtPersonnel.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPersonnel.Location = New System.Drawing.Point(82, 84)
        Me.txtPersonnel.Name = "txtPersonnel"
        Me.txtPersonnel.ReadOnly = True
        Me.txtPersonnel.Size = New System.Drawing.Size(127, 22)
        Me.txtPersonnel.TabIndex = 24
        '
        'btResetPersonnel
        '
        Me.btResetPersonnel.Location = New System.Drawing.Point(215, 84)
        Me.btResetPersonnel.Name = "btResetPersonnel"
        Me.btResetPersonnel.Size = New System.Drawing.Size(38, 22)
        Me.btResetPersonnel.TabIndex = 49
        Me.btResetPersonnel.Text = "清除"
        Me.btResetPersonnel.UseVisualStyleBackColor = True
        '
        'btResetCustomer
        '
        Me.btResetCustomer.Location = New System.Drawing.Point(475, 84)
        Me.btResetCustomer.Name = "btResetCustomer"
        Me.btResetCustomer.Size = New System.Drawing.Size(38, 22)
        Me.btResetCustomer.TabIndex = 49
        Me.btResetCustomer.Text = "清除"
        Me.btResetCustomer.UseVisualStyleBackColor = True
        '
        'txtSalesDate
        '
        Me.txtSalesDate.Enabled = False
        Me.txtSalesDate.Location = New System.Drawing.Point(342, 50)
        Me.txtSalesDate.Name = "txtSalesDate"
        Me.txtSalesDate.Size = New System.Drawing.Size(127, 22)
        Me.txtSalesDate.TabIndex = 25
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(283, 53)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 12)
        Me.Label9.TabIndex = 30
        Me.Label9.Text = "銷貨日期"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btResetCustomer)
        Me.GroupBox2.Controls.Add(Me.btResetPersonnel)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.txtSalesDate)
        Me.GroupBox2.Controls.Add(Me.txtOrderDate)
        Me.GroupBox2.Controls.Add(Me.txtCustomer)
        Me.GroupBox2.Controls.Add(Me.txtPersonnel)
        Me.GroupBox2.Controls.Add(Me.txtLabel)
        Me.GroupBox2.Location = New System.Drawing.Point(21, 6)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(578, 117)
        Me.GroupBox2.TabIndex = 50
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "資訊"
        '
        'btSales
        '
        Me.btSales.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btSales.Location = New System.Drawing.Point(509, 564)
        Me.btSales.Name = "btSales"
        Me.btSales.Size = New System.Drawing.Size(75, 35)
        Me.btSales.TabIndex = 46
        Me.btSales.Text = "銷貨"
        Me.btSales.UseVisualStyleBackColor = True
        '
        'winSales
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(622, 611)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.txtDeposit)
        Me.Controls.Add(Me.cbPayMode)
        Me.Controls.Add(Me.btSales)
        Me.Controls.Add(Me.btOrder)
        Me.Controls.Add(Me.lbTotal)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtNote)
        Me.Name = "winSales"
        Me.Text = "銷貨單"
        CType(Me.dgList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btAddGood As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtNote As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtOrderDate As System.Windows.Forms.TextBox
    Friend WithEvents txtLabel As System.Windows.Forms.TextBox
    Friend WithEvents dgList As System.Windows.Forms.DataGridView
    Friend WithEvents btDelete As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lbTotal As System.Windows.Forms.Label
    Friend WithEvents btOrder As System.Windows.Forms.Button
    Friend WithEvents cbPayMode As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cLabel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cKind As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cBrand As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSubTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtDeposit As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtCustomer As System.Windows.Forms.TextBox
    Friend WithEvents txtPersonnel As System.Windows.Forms.TextBox
    Friend WithEvents btResetPersonnel As System.Windows.Forms.Button
    Friend WithEvents btResetCustomer As System.Windows.Forms.Button
    Friend WithEvents txtSalesDate As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btSales As System.Windows.Forms.Button
End Class
