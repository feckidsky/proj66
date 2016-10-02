Imports System.Net.Mail

Module Mail

    Public Sub sendTest()
        Dim files As New List(Of String)
        files.Add("C:\Documents and Settings\Administrator\桌面\新增文字文件.txt")
        files.Add("C:\Documents and Settings\Administrator\桌面\base.rar")

        MsgBox(SendMail("smtp.gmail.com", 587, "id", "password", "title", "content", "就是我<feckidsky@gmail.com>", "kid.sky@yahoo.com.tw", files.ToArray))
    End Sub

    Public Class MailSender
        Inherits System.Net.Mail.SmtpClient

        Public Function BeginSend(ByVal Server As String, ByVal port As Integer, ByVal id As String, ByVal password As String, ByVal title As String, ByVal content As String, ByVal from As String, ByVal receiver As String, Optional ByVal files() As String = Nothing, Optional ByVal obj As Object = Nothing) As Boolean
            Dim message As MailMessage = New MailMessage(from, receiver, title, content)

            If files IsNot Nothing Then
                For Each file As String In files
                    ' Create  the file attachment for this e-mail message.
                    Dim data As Attachment = New Attachment(file, Net.Mime.MediaTypeNames.Application.Octet)
                    ' Add time stamp information for the file.
                    'ContentDisposition disposition = data.ContentDisposition
                    data.ContentDisposition.CreationDate = System.IO.File.GetCreationTime(file)
                    data.ContentDisposition.ModificationDate = System.IO.File.GetLastWriteTime(file)
                    data.ContentDisposition.ReadDate = System.IO.File.GetLastAccessTime(file)
                    ' Add the file attachment to this e-mail message.
                    message.Attachments.Add(data)
                Next
            End If
            Me.Host = Server
            Me.Port = port

            '//設定你的帳號密碼 
            Credentials = New System.Net.NetworkCredential(id, password)
            '//Gmial 的 smtp 必需要使用 SSL 
            EnableSsl = True
            Try
                SendAsync(message, obj)
            Catch
                MsgBox(Err.Description)
                Return False
            End Try
            Return True
        End Function


    End Class

    Public Function SendMail(ByVal Server As String, ByVal port As Integer, ByVal id As String, ByVal password As String, ByVal title As String, ByVal content As String, ByVal from As String, ByVal receiver As String, Optional ByVal files() As String = Nothing) As String

        ' Create a message and set up the recipients.
        Dim message As MailMessage = New MailMessage(from, receiver, title, content)

        If files IsNot Nothing Then
            For Each file As String In files
                ' Create  the file attachment for this e-mail message.
                Dim data As Attachment = New Attachment(file, Net.Mime.MediaTypeNames.Application.Octet)
                ' Add time stamp information for the file.
                'ContentDisposition disposition = data.ContentDisposition
                data.ContentDisposition.CreationDate = System.IO.File.GetCreationTime(file)
                data.ContentDisposition.ModificationDate = System.IO.File.GetLastWriteTime(file)
                data.ContentDisposition.ReadDate = System.IO.File.GetLastAccessTime(file)
                ' Add the file attachment to this e-mail message.
                message.Attachments.Add(data)
            Next
        End If


        Dim MySmtp As System.Net.Mail.SmtpClient = New System.Net.Mail.SmtpClient(Server, port)
        '//設定你的帳號密碼 
        MySmtp.Credentials = New System.Net.NetworkCredential(id, password)
        '//Gmial 的 smtp 必需要使用 SSL 
        MySmtp.EnableSsl = True
        '//發送Email 
        Try
            MySmtp.Send(message)
            Return "發送成功!"
        Catch e As SmtpException
            Return e.Message
        End Try
    End Function


End Module
