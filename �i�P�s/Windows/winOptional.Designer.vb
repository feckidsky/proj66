<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class winOptional
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btSalesBackColor = New System.Windows.Forms.Button
        Me.btOrderBackColor = New System.Windows.Forms.Button
        Me.btOK = New System.Windows.Forms.Button
        Me.btCancel = New System.Windows.Forms.Button
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.dgShop = New System.Windows.Forms.DataGridView
        Me.cShop = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cIP = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cPort = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btRepairMDB = New System.Windows.Forms.Button
        Me.btMdbUpdate = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.cbMode = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.txtNetIndex = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtPort = New System.Windows.Forms.TextBox
        Me.txtServerName = New System.Windows.Forms.TextBox
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.btUseGmail = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtMailPort = New 進銷存.NumberBox
        Me.txtMailReceiverAddr = New System.Windows.Forms.TextBox
        Me.txtMailPassword = New System.Windows.Forms.TextBox
        Me.txtMailID = New System.Windows.Forms.TextBox
        Me.txtMailSenderAddr = New System.Windows.Forms.TextBox
        Me.txtBackupDir = New System.Windows.Forms.TextBox
        Me.txtMailServer = New System.Windows.Forms.TextBox
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtStoreCode = New System.Windows.Forms.TextBox
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgShop, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btSalesBackColor)
        Me.GroupBox1.Controls.Add(Me.btOrderBackColor)
        Me.GroupBox1.Location = New System.Drawing.Point(450, 200)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(176, 88)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "訂單/銷貨單背景"
        '
        'btSalesBackColor
        '
        Me.btSalesBackColor.Location = New System.Drawing.Point(17, 50)
        Me.btSalesBackColor.Name = "btSalesBackColor"
        Me.btSalesBackColor.Size = New System.Drawing.Size(142, 23)
        Me.btSalesBackColor.TabIndex = 1
        Me.btSalesBackColor.Text = "銷貨單背景"
        Me.btSalesBackColor.UseVisualStyleBackColor = True
        '
        'btOrderBackColor
        '
        Me.btOrderBackColor.Location = New System.Drawing.Point(17, 21)
        Me.btOrderBackColor.Name = "btOrderBackColor"
        Me.btOrderBackColor.Size = New System.Drawing.Size(142, 23)
        Me.btOrderBackColor.TabIndex = 1
        Me.btOrderBackColor.Text = "訂單背景"
        Me.btOrderBackColor.UseVisualStyleBackColor = True
        '
        'btOK
        '
        Me.btOK.Location = New System.Drawing.Point(741, 300)
        Me.btOK.Name = "btOK"
        Me.btOK.Size = New System.Drawing.Size(75, 23)
        Me.btOK.TabIndex = 1
        Me.btOK.Text = "確認"
        Me.btOK.UseVisualStyleBackColor = True
        '
        'btCancel
        '
        Me.btCancel.Location = New System.Drawing.Point(658, 300)
        Me.btCancel.Name = "btCancel"
        Me.btCancel.Size = New System.Drawing.Size(75, 23)
        Me.btCancel.TabIndex = 1
        Me.btCancel.Text = "取消"
        Me.btCancel.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dgShop)
        Me.GroupBox2.Location = New System.Drawing.Point(18, 103)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(420, 204)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "連線設定"
        '
        'dgShop
        '
        Me.dgShop.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgShop.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgShop.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgShop.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cShop, Me.cIP, Me.cPort})
        Me.dgShop.Location = New System.Drawing.Point(17, 21)
        Me.dgShop.Name = "dgShop"
        Me.dgShop.RowTemplate.Height = 24
        Me.dgShop.Size = New System.Drawing.Size(385, 164)
        Me.dgShop.TabIndex = 0
        '
        'cShop
        '
        Me.cShop.HeaderText = "店名"
        Me.cShop.Name = "cShop"
        '
        'cIP
        '
        Me.cIP.HeaderText = "IP"
        Me.cIP.Name = "cIP"
        '
        'cPort
        '
        Me.cPort.HeaderText = "Port"
        Me.cPort.Name = "cPort"
        '
        'btRepairMDB
        '
        Me.btRepairMDB.Location = New System.Drawing.Point(33, 21)
        Me.btRepairMDB.Name = "btRepairMDB"
        Me.btRepairMDB.Size = New System.Drawing.Size(113, 23)
        Me.btRepairMDB.TabIndex = 3
        Me.btRepairMDB.Text = "修復/壓縮資料庫"
        Me.btRepairMDB.UseVisualStyleBackColor = True
        '
        'btMdbUpdate
        '
        Me.btMdbUpdate.Location = New System.Drawing.Point(33, 50)
        Me.btMdbUpdate.Name = "btMdbUpdate"
        Me.btMdbUpdate.Size = New System.Drawing.Size(113, 23)
        Me.btMdbUpdate.TabIndex = 3
        Me.btMdbUpdate.Text = "更新資料庫結構"
        Me.btMdbUpdate.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btMdbUpdate)
        Me.GroupBox3.Controls.Add(Me.btRepairMDB)
        Me.GroupBox3.Location = New System.Drawing.Point(638, 200)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(178, 88)
        Me.GroupBox3.TabIndex = 4
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "資料庫"
        '
        'cbMode
        '
        Me.cbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMode.FormattingEnabled = True
        Me.cbMode.Items.AddRange(New Object() {"連線端", "伺服器"})
        Me.cbMode.Location = New System.Drawing.Point(16, 45)
        Me.cbMode.Name = "cbMode"
        Me.cbMode.Size = New System.Drawing.Size(100, 20)
        Me.cbMode.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 12)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "工作模式"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.txtNetIndex)
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Controls.Add(Me.Label11)
        Me.GroupBox4.Controls.Add(Me.Label2)
        Me.GroupBox4.Controls.Add(Me.txtPort)
        Me.GroupBox4.Controls.Add(Me.txtStoreCode)
        Me.GroupBox4.Controls.Add(Me.txtServerName)
        Me.GroupBox4.Controls.Add(Me.cbMode)
        Me.GroupBox4.Controls.Add(Me.Label1)
        Me.GroupBox4.Location = New System.Drawing.Point(19, 12)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(420, 85)
        Me.GroupBox4.TabIndex = 7
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "系統設定"
        '
        'txtNetIndex
        '
        Me.txtNetIndex.Location = New System.Drawing.Point(378, 45)
        Me.txtNetIndex.Name = "txtNetIndex"
        Me.txtNetIndex.Size = New System.Drawing.Size(22, 22)
        Me.txtNetIndex.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(322, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 12)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "通訊埠"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(132, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 12)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "伺服器名稱"
        '
        'txtPort
        '
        Me.txtPort.Location = New System.Drawing.Point(321, 45)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(51, 22)
        Me.txtPort.TabIndex = 7
        '
        'txtServerName
        '
        Me.txtServerName.Location = New System.Drawing.Point(131, 45)
        Me.txtServerName.Name = "txtServerName"
        Me.txtServerName.Size = New System.Drawing.Size(100, 22)
        Me.txtServerName.TabIndex = 7
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.btUseGmail)
        Me.GroupBox5.Controls.Add(Me.Button1)
        Me.GroupBox5.Controls.Add(Me.Label8)
        Me.GroupBox5.Controls.Add(Me.Label10)
        Me.GroupBox5.Controls.Add(Me.Label5)
        Me.GroupBox5.Controls.Add(Me.Label7)
        Me.GroupBox5.Controls.Add(Me.Label6)
        Me.GroupBox5.Controls.Add(Me.Label9)
        Me.GroupBox5.Controls.Add(Me.Label4)
        Me.GroupBox5.Controls.Add(Me.txtMailPort)
        Me.GroupBox5.Controls.Add(Me.txtMailReceiverAddr)
        Me.GroupBox5.Controls.Add(Me.txtMailPassword)
        Me.GroupBox5.Controls.Add(Me.txtMailID)
        Me.GroupBox5.Controls.Add(Me.txtMailSenderAddr)
        Me.GroupBox5.Controls.Add(Me.txtBackupDir)
        Me.GroupBox5.Controls.Add(Me.txtMailServer)
        Me.GroupBox5.Location = New System.Drawing.Point(445, 12)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(371, 182)
        Me.GroupBox5.TabIndex = 8
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "備份"
        '
        'btUseGmail
        '
        Me.btUseGmail.Location = New System.Drawing.Point(290, 17)
        Me.btUseGmail.Name = "btUseGmail"
        Me.btUseGmail.Size = New System.Drawing.Size(75, 23)
        Me.btUseGmail.TabIndex = 3
        Me.btUseGmail.Text = "使用Gmail"
        Me.btUseGmail.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(282, 121)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "選擇"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(16, 132)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 12)
        Me.Label8.TabIndex = 1
        Me.Label8.Text = "備份路徑"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(194, 53)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(29, 12)
        Me.Label10.TabIndex = 1
        Me.Label10.Text = "密碼"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(194, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 12)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "通訊埠"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(194, 85)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 12)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "收件者"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(18, 85)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 12)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "發送者"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(18, 52)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(29, 12)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "帳號"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(18, 21)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 12)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "伺服器"
        '
        'txtMailPort
        '
        Me.txtMailPort.Location = New System.Drawing.Point(241, 18)
        Me.txtMailPort.Name = "txtMailPort"
        Me.txtMailPort.Size = New System.Drawing.Size(43, 22)
        Me.txtMailPort.TabIndex = 0
        '
        'txtMailReceiverAddr
        '
        Me.txtMailReceiverAddr.Location = New System.Drawing.Point(241, 82)
        Me.txtMailReceiverAddr.Name = "txtMailReceiverAddr"
        Me.txtMailReceiverAddr.Size = New System.Drawing.Size(116, 22)
        Me.txtMailReceiverAddr.TabIndex = 0
        '
        'txtMailPassword
        '
        Me.txtMailPassword.Location = New System.Drawing.Point(241, 50)
        Me.txtMailPassword.Name = "txtMailPassword"
        Me.txtMailPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtMailPassword.Size = New System.Drawing.Size(116, 22)
        Me.txtMailPassword.TabIndex = 0
        '
        'txtMailID
        '
        Me.txtMailID.Location = New System.Drawing.Point(65, 50)
        Me.txtMailID.Name = "txtMailID"
        Me.txtMailID.Size = New System.Drawing.Size(116, 22)
        Me.txtMailID.TabIndex = 0
        '
        'txtMailSenderAddr
        '
        Me.txtMailSenderAddr.Location = New System.Drawing.Point(65, 82)
        Me.txtMailSenderAddr.Name = "txtMailSenderAddr"
        Me.txtMailSenderAddr.Size = New System.Drawing.Size(116, 22)
        Me.txtMailSenderAddr.TabIndex = 0
        '
        'txtBackupDir
        '
        Me.txtBackupDir.Location = New System.Drawing.Point(16, 147)
        Me.txtBackupDir.Name = "txtBackupDir"
        Me.txtBackupDir.Size = New System.Drawing.Size(341, 22)
        Me.txtBackupDir.TabIndex = 0
        '
        'txtMailServer
        '
        Me.txtMailServer.Location = New System.Drawing.Point(65, 18)
        Me.txtMailServer.Name = "txtMailServer"
        Me.txtMailServer.Size = New System.Drawing.Size(116, 22)
        Me.txtMailServer.TabIndex = 0
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(238, 24)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(53, 12)
        Me.Label11.TabIndex = 8
        Me.Label11.Text = "分店代號"
        '
        'txtStoreCode
        '
        Me.txtStoreCode.Location = New System.Drawing.Point(237, 45)
        Me.txtStoreCode.Name = "txtStoreCode"
        Me.txtStoreCode.Size = New System.Drawing.Size(66, 22)
        Me.txtStoreCode.TabIndex = 7
        '
        'winOptional
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(828, 335)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btCancel)
        Me.Controls.Add(Me.btOK)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "winOptional"
        Me.Text = "選項設定"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.dgShop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btSalesBackColor As System.Windows.Forms.Button
    Friend WithEvents btOrderBackColor As System.Windows.Forms.Button
    Friend WithEvents btOK As System.Windows.Forms.Button
    Friend WithEvents btCancel As System.Windows.Forms.Button
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents dgShop As System.Windows.Forms.DataGridView
    Friend WithEvents cShop As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cIP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cPort As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btRepairMDB As System.Windows.Forms.Button
    Friend WithEvents btMdbUpdate As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents cbMode As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPort As System.Windows.Forms.TextBox
    Friend WithEvents txtServerName As System.Windows.Forms.TextBox
    Friend WithEvents txtNetIndex As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtMailServer As System.Windows.Forms.TextBox
    Friend WithEvents txtMailPort As NumberBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtMailSenderAddr As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtMailReceiverAddr As System.Windows.Forms.TextBox
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtBackupDir As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtMailID As System.Windows.Forms.TextBox
    Friend WithEvents txtMailPassword As System.Windows.Forms.TextBox
    Friend WithEvents btUseGmail As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtStoreCode As System.Windows.Forms.TextBox
End Class
