<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DataGridViewPrintDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DataGridViewPrintDialog))
        Me.ckList = New System.Windows.Forms.CheckedListBox
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog
        Me.btPreview = New System.Windows.Forms.Button
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument
        Me.btFont = New System.Windows.Forms.Button
        Me.FontDialog1 = New System.Windows.Forms.FontDialog
        Me.btCancel = New System.Windows.Forms.Button
        Me.btPrint = New System.Windows.Forms.Button
        Me.btSetting = New System.Windows.Forms.Button
        Me.ckNumber = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'ckList
        '
        Me.ckList.FormattingEnabled = True
        Me.ckList.Location = New System.Drawing.Point(12, 10)
        Me.ckList.Name = "ckList"
        Me.ckList.Size = New System.Drawing.Size(172, 208)
        Me.ckList.TabIndex = 0
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'btPreview
        '
        Me.btPreview.Location = New System.Drawing.Point(206, 106)
        Me.btPreview.Name = "btPreview"
        Me.btPreview.Size = New System.Drawing.Size(100, 30)
        Me.btPreview.TabIndex = 2
        Me.btPreview.Text = "預覽列印"
        Me.btPreview.UseVisualStyleBackColor = True
        '
        'PrintDocument1
        '
        '
        'btFont
        '
        Me.btFont.Location = New System.Drawing.Point(206, 34)
        Me.btFont.Name = "btFont"
        Me.btFont.Size = New System.Drawing.Size(100, 30)
        Me.btFont.TabIndex = 3
        Me.btFont.Text = "字體"
        Me.btFont.UseVisualStyleBackColor = True
        '
        'btCancel
        '
        Me.btCancel.Location = New System.Drawing.Point(206, 188)
        Me.btCancel.Name = "btCancel"
        Me.btCancel.Size = New System.Drawing.Size(100, 30)
        Me.btCancel.TabIndex = 4
        Me.btCancel.Text = "關閉"
        Me.btCancel.UseVisualStyleBackColor = True
        '
        'btPrint
        '
        Me.btPrint.Location = New System.Drawing.Point(206, 142)
        Me.btPrint.Name = "btPrint"
        Me.btPrint.Size = New System.Drawing.Size(100, 30)
        Me.btPrint.TabIndex = 5
        Me.btPrint.Text = "列印"
        Me.btPrint.UseVisualStyleBackColor = True
        '
        'btSetting
        '
        Me.btSetting.Location = New System.Drawing.Point(206, 70)
        Me.btSetting.Name = "btSetting"
        Me.btSetting.Size = New System.Drawing.Size(100, 30)
        Me.btSetting.TabIndex = 6
        Me.btSetting.Text = "印表機設定"
        Me.btSetting.UseVisualStyleBackColor = True
        '
        'ckNumber
        '
        Me.ckNumber.AutoSize = True
        Me.ckNumber.Location = New System.Drawing.Point(206, 12)
        Me.ckNumber.Name = "ckNumber"
        Me.ckNumber.Size = New System.Drawing.Size(96, 16)
        Me.ckNumber.TabIndex = 7
        Me.ckNumber.Text = "印出項目編號"
        Me.ckNumber.UseVisualStyleBackColor = True
        '
        'DataGridViewPrintDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(328, 236)
        Me.Controls.Add(Me.ckNumber)
        Me.Controls.Add(Me.btSetting)
        Me.Controls.Add(Me.btPrint)
        Me.Controls.Add(Me.btCancel)
        Me.Controls.Add(Me.btFont)
        Me.Controls.Add(Me.btPreview)
        Me.Controls.Add(Me.ckList)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "DataGridViewPrintDialog"
        Me.Text = "列印"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ckList As System.Windows.Forms.CheckedListBox
    Friend WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents btPreview As System.Windows.Forms.Button
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents btFont As System.Windows.Forms.Button
    Friend WithEvents FontDialog1 As System.Windows.Forms.FontDialog
    Friend WithEvents btCancel As System.Windows.Forms.Button
    Friend WithEvents btPrint As System.Windows.Forms.Button
    Friend WithEvents btSetting As System.Windows.Forms.Button
    Friend WithEvents ckNumber As System.Windows.Forms.CheckBox
End Class
