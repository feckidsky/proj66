﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DialogErrorFileList
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
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.btDownload = New System.Windows.Forms.Button
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.btDownloadAccess = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 12
        Me.ListBox1.Location = New System.Drawing.Point(12, 12)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(465, 232)
        Me.ListBox1.TabIndex = 0
        '
        'btDownload
        '
        Me.btDownload.Location = New System.Drawing.Point(377, 249)
        Me.btDownload.Name = "btDownload"
        Me.btDownload.Size = New System.Drawing.Size(75, 23)
        Me.btDownload.TabIndex = 1
        Me.btDownload.Text = "下載"
        Me.btDownload.UseVisualStyleBackColor = True
        '
        'btDownloadAccess
        '
        Me.btDownloadAccess.Location = New System.Drawing.Point(12, 249)
        Me.btDownloadAccess.Name = "btDownloadAccess"
        Me.btDownloadAccess.Size = New System.Drawing.Size(94, 23)
        Me.btDownloadAccess.TabIndex = 2
        Me.btDownloadAccess.Text = "下載資料庫"
        Me.btDownloadAccess.UseVisualStyleBackColor = True
        '
        'DialogErrorFileList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(489, 284)
        Me.Controls.Add(Me.btDownloadAccess)
        Me.Controls.Add(Me.btDownload)
        Me.Controls.Add(Me.ListBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "DialogErrorFileList"
        Me.Text = "錯誤記錄"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents btDownload As System.Windows.Forms.Button
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents btDownloadAccess As System.Windows.Forms.Button
End Class
