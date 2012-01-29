'Imports System
'Imports System.Text
'Imports System.Diagnostics
'Imports System.Runtime.InteropServices

Public Class ErrorLog
    Public Shared Dir As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & My.Application.Info.Title & " ErrorLog\"

    Shared Event ErrorOccur()

    Public Shared Sub Enable()
        '委派錯誤訊息
        AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf OnUnhandledException
    End Sub

    Public Shared Sub Enable(ByVal Dir As String)
        ErrorLog.Dir = Dir
        '委派錯誤訊息
        AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf OnUnhandledException
    End Sub

    Public Shared Sub Disable()
        RemoveHandler AppDomain.CurrentDomain.UnhandledException, AddressOf OnUnhandledException
    End Sub

    Public Shared Sub OnErrorOccur()
        RaiseEvent ErrorOccur()
    End Sub


#Region "記錄錯誤訊息"
    Public Shared Sub OnUnhandledException(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs)
        WriteErrorLog(e.ExceptionObject)
        RaiseEvent ErrorOccur()
    End Sub

    Public Shared Sub WriteErrorLog(ByVal e As Exception)
        Dim Msg As String = GetErrorMessage(e)
        WriteErrorLog(Msg)
    End Sub

    Public Shared Function GetErrorMessage(ByVal e As Exception) As String
        Dim TPM As ULong = My.Computer.Info.TotalPhysicalMemory
        Dim TVM As ULong = My.Computer.Info.TotalVirtualMemory
        Dim APM As ULong = My.Computer.Info.AvailablePhysicalMemory
        Dim AVM As ULong = My.Computer.Info.AvailableVirtualMemory
        Dim Msg As String = ""
        Msg &= "===================================================" & vbCrLf
        Msg &= Now.ToString & vbCrLf
        Msg &= "---------------------------------------------------" & vbCrLf
        Msg &= "實體記憶體：" & Math.Round((TPM - APM) / 1024 / 1024, 2) & " MB -> " & Math.Round((TPM - APM) / TPM * 100, 2) & "%" & vbCrLf
        Msg &= "虛擬記憶體：" & Math.Round((TVM - AVM) / 1024 / 1024, 2) & " MB -> " & Math.Round((TVM - AVM) / TVM * 100, 2) & "%" & vbCrLf
        Msg &= "---------------------------------------------------" & vbCrLf
        Msg &= e.TargetSite.ToString & ":" & vbCrLf
        Msg &= e.Message & vbCrLf
        Msg &= "---------------------------------------------------" & vbCrLf
        Msg &= e.StackTrace & vbCrLf
        Return Msg
    End Function

    Public Shared Sub WriteErrorLog(ByVal Msg As String)
        ' Dim Dir As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\JNC ErrorLog\"
        Dim LogFile As String = Dir & Today.ToString("yyyyMMdd") & ".txt"
        If Not IO.Directory.Exists(Dir) Then IO.Directory.CreateDirectory(Dir)
        My.Computer.FileSystem.WriteAllText(LogFile, Msg, True)
    End Sub
#End Region



    Public Enum Mode
        LogOff = 0
        ForcedLogOff = 4
        Shutdown = 1
        ForcedShutdown = 5
        Reboot = 2
        ForcedReboot = 6
        PowerOff = 8
        ForcedPowerOff = 12
    End Enum

    Public Shared Function ExitWindows(ByVal Mode As Mode)
        AdjustToken()
        Return ExitWindowsEx(Mode, 0)
    End Function

    Public Declare Function ExitWindowsEx Lib "user32" _
                        Alias "ExitWindowsEx" ( _
                                        ByVal uFlags As Integer, _
                                        ByVal dwReserved As Integer) As Integer

    Public Declare Function GetCurrentProcess Lib "kernel32" _
                            Alias "GetCurrentProcess" () As IntPtr

    Public Declare Function OpenProcessToken Lib "advapi32.dll" ( _
                                            ByVal ProcessHandle As IntPtr, _
                                            ByVal DesiredAccess As Integer, _
                                            ByRef TokenHandle As IntPtr) As Integer

    Public Declare Function LookupPrivilegeValue Lib "advapi32.dll" _
                            Alias "LookupPrivilegeValueA" ( _
                                            ByVal lpSystemName As String, _
                                            ByVal lpName As String, _
                                            ByRef lpLuid As LUID) As Integer

    Public Declare Function AdjustTokenPrivileges Lib "advapi32.dll" _
                            Alias "AdjustTokenPrivileges" ( _
                                            ByVal TokenHandle As IntPtr, _
                                            ByVal DisableAllPrivileges As Integer, _
                                            ByRef NewState As TOKEN_PRIVILEGES, _
                                            ByVal BufferLength As Integer, _
                                            ByRef PreviousState As TOKEN_PRIVILEGES, _
                                            ByRef ReturnLength As Integer) As Integer

    Public Declare Function ShShutDownDialog Lib "shell32" _
                            Alias "#60" (ByVal YourGuess As Integer) As Integer

    Public Const TOKEN_ADJUST_PRIVILEGES = &H20
    Public Const TOKEN_QUERY = &H8
    Public Const SE_PRIVILEGE_ENABLED = &H2
    Public Const SPI_SCREENSAVERRUNNING = 97
    Public Const EWX_FORCE = 4
    Public Const EWX_LOGOFF = 0
    Public Const EWX_REBOOT = 2
    Public Const EWX_SHUTDOWN = 1

    Public Structure LUID
        Dim UsedPart As Integer
        Dim IgnoredForNowHigh32BitPart As Integer
    End Structure

    Public Structure TOKEN_PRIVILEGES
        Dim PrivilegeCount As Integer
        Dim TheLuid As LUID
        Dim Attributes As Integer
    End Structure

    Public Shared Sub AdjustToken()
        Dim hdlProcessHandle As IntPtr
        Dim hdlTokenHandle As IntPtr
        Dim tmpLuid As LUID
        Dim tkp As TOKEN_PRIVILEGES
        Dim tkpNewButIgnored As TOKEN_PRIVILEGES
        Dim lBufferNeeded As Integer

        hdlProcessHandle = GetCurrentProcess()
        OpenProcessToken(hdlProcessHandle, (TOKEN_ADJUST_PRIVILEGES Or _
        TOKEN_QUERY), hdlTokenHandle)
        LookupPrivilegeValue("", "SeShutdownPrivilege", tmpLuid)
        tkp.PrivilegeCount = 1
        tkp.TheLuid = tmpLuid
        tkp.Attributes = SE_PRIVILEGE_ENABLED
        AdjustTokenPrivileges(hdlTokenHandle, False, tkp, _
        Len(tkpNewButIgnored), tkpNewButIgnored, lBufferNeeded)
    End Sub

End Class


