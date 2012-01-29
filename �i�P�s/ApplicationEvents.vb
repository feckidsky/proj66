
Namespace My

    ' MyApplication 可以使用下列事件:
    ' 
    ' Startup: 在應用程式啟動時，但尚未建立啟動表單之前引發。
    ' Shutdown: 在所有應用程式表單關閉之後引發。如果應用程式不正常終止，就不會引發此事件。
    ' UnhandledException: 在應用程式發生未處理的例外狀況時引發。
    ' StartupNextInstance: 在啟動單一執行個體應用程式且應用程式已於使用中時引發。
    ' NetworkAvailabilityChanged: 在連接或中斷網路連接時引發。
    Partial Friend Class MyApplication

        'Public Declare Function GetPixel Lib "gdi32" Alias "GetPixel" (ByVal hdc As IntPtr, ByVal X As Int32, ByVal Y As Int32) As Int32
        'Public Declare Sub ShowWindow Lib "user32" Alias "ShowWindows" (ByVal hwnd As IntPtr, ByVal Type As Int32)

        Public Enum CommandShow
            SW_HIDE = 0
            SW_SHOWNORMAL = 1
            SW_NORMAL = 1
            SW_SHOWMINIMIZED = 2
            SW_SHOWMAXIMIZED = 3
            SW_MAXIMIZE = 3
            SW_SHOWNOACTIVATE = 4
            SW_SHOW = 5
            SW_MINIMIZE = 6
            SW_SHOWMINNOACTIVE = 7
            SW_SHOWNA = 8
            SW_RESTORE = 9
            SW_SHOWDEFAULT = 10
            SW_FORCEMINIMIZE = 11
        End Enum

        Private Sub MyApplication_Shutdown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shutdown
            FinishProgram()
        End Sub

        Private Sub MyApplication_StartupNextInstance(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
            'Dim proc As Process() = Process.GetProcessesByName("進銷存")
            'ShowWindow(proc(0).MainWindowHandle.ToInt32(), CommandShow.SW_SHOWNORMAL)
            Dim app As winMain = My.Application.OpenForms(winMain.Name)
            app.Visible = True

        End Sub
    End Class

End Namespace

