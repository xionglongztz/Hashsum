Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Security.Cryptography
Imports System.Threading
Imports Microsoft.Win32
Public Class frmMain
    Dim calculateThread As Thread '定义新的线程
    Dim fileMD5value As String = ""
    Dim fileSHA1value As String = ""
    Dim fileSHA256value As String = ""
    Dim fileCRC32value As String = ""
    Dim fileSHA512value As String = "" '哈希值
    Dim _filePath As String = "" '文件路径
    Dim remindAfterFinished As Boolean '完成后提醒
    Dim isLetterCapital As Boolean '字母大写
    Delegate Sub Dg(v As Integer) '声明一个委托
    Dim HashUpdate As New Dg(AddressOf Hashshow) '实例化委托，指向哈希显示过程
    Dim firstWriteDate As Date '第一次打开文件时间
    Dim wndTitle As String '窗口标题
    Dim fileDelFlag As Boolean = False, fileEditFlag As Boolean = False '文件修改和删除标志位
    Public ThemeColor As Boolean
#Region "拖拽操作"
    Private Sub Form1_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop
        Try '支持拖放操作
            Dim filePath() As String = e.Data.GetData(DataFormats.FileDrop)
            For Each file As String In filePath
                If IO.Directory.Exists(file) Then '如果是目录
                    Exit Sub '退出
                End If
                _filePath = file
                HashCalc() '计算哈希
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error") '报错
        End Try
    End Sub
    Private Sub Form1_DragEnter(sender As Object, e As DragEventArgs) Handles Me.DragEnter
        Try
            If e.Data.GetDataPresent(DataFormats.FileDrop) Then
                e.Effect = DragDropEffects.Copy
            Else
                e.Effect = DragDropEffects.None
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error") '报错
        End Try
    End Sub '拖放操作
#End Region
#Region "计算哈希线程"
    Private Function HashCalc()
        If IO.File.Exists(_filePath) = False Then
            MsgBox("文件不存在！", MsgBoxStyle.Critical, "Error")
            ClearWnd()
            Return 1
        End If
        fileEditFlag = False
        fileDelFlag = False
        EnableMenuItem(GetSystemMenu(Handle, False), 10, MF_ENABLED)
        EnableMenuItem(GetSystemMenu(Handle, False), 11, MF_ENABLED)
        EnableMenuItem(GetSystemMenu(Handle, False), 12, MF_ENABLED)
        EnableMenuItem(GetSystemMenu(Handle, False), 16, MF_ENABLED)
        EnableMenuItem(GetSystemMenu(Handle, False), 6, MF_ENABLED)
        EnableMenuItem(GetSystemMenu(Handle, False), 3, MF_ENABLED)
        firstWriteDate = IO.File.GetLastWriteTime(_filePath) '记录当前打开文件时间
        lMD5.Text = "MD5:"
        lSHA1.Text = "SHA1:"
        lSHA256.Text = "SHA256:"
        lCRC32.Text = "CRC32:"
        lSHA512.Text = "SHA512:"
        fileMD5value = ""
        fileSHA1value = ""
        fileSHA256value = ""
        fileCRC32value = ""
        fileSHA512value = ""
        calculateThread = New Thread(AddressOf Calculate) '线程实例化
        calculateThread.Start() '启动线程
        lFilename.Text = "File:" & _filePath
        PBFilewizard.Visible = False
        Try
            Dim FI As New FileInfo(_filePath)
            lFilename2.Text = "文件名称:" & FI.Name
            lFilePath.Text = "文件路径:" & FI.DirectoryName
            lFileSize.Text = "大小:" & ByteFormat(FI.Length) & "(" & FI.Length & "字节)"
            lCreateTime.Text = "创建时间:" & FI.CreationTime.ToString
            lModifyTime.Text = "修改时间:" & FI.LastWriteTime.ToString
            lCheckTime.Text = "访问时间:" & FI.LastAccessTime.ToString
            lProperties.Text = "属性:" & FI.Attributes.ToString
            lFileType.Text = "文件类型:" & FI.Extension.ToLower
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error") '报错
        End Try
        Text = wndTitle
        Return 0
    End Function
    Private Function ClearWnd() '清空窗口
        fileEditFlag = False
        fileDelFlag = False
        Text = wndTitle
        lFilename.Text = "File:"
        _filePath = ""
        lMD5.Text = "MD5:"
        lSHA1.Text = "SHA1:"
        lSHA256.Text = "SHA256:"
        lCRC32.Text = "CRC32:"
        lSHA512.Text = "SHA512:"
        PBFilewizard.Visible = True
        EnableMenuItem(GetSystemMenu(Handle, False), 10, MF_GRAYED)
        EnableMenuItem(GetSystemMenu(Handle, False), 11, MF_GRAYED)
        EnableMenuItem(GetSystemMenu(Handle, False), 12, MF_GRAYED)
        EnableMenuItem(GetSystemMenu(Handle, False), 16, MF_GRAYED)
        EnableMenuItem(GetSystemMenu(Handle, False), 6, MF_GRAYED)
        EnableMenuItem(GetSystemMenu(Handle, False), 3, MF_GRAYED)
        lFilename2.Text = "文件名称:"
        lFilePath.Text = "文件路径:"
        lFileSize.Text = "大小:"
        lCreateTime.Text = "创建时间:"
        lModifyTime.Text = "修改时间:"
        lCheckTime.Text = "访问时间:"
        lProperties.Text = "属性:"
        lFileType.Text = "文件类型:"
        Return 0
    End Function
    Private Sub Calculate() '计算哈希用线程
        Try
            fileMD5value = FileMD5(_filePath)
            BeginInvoke(HashUpdate, vbNullString)
            fileSHA1value = FileSHA1(_filePath)
            BeginInvoke(HashUpdate, vbNullString)
            fileSHA256value = FileSHA256(_filePath)
            BeginInvoke(HashUpdate, vbNullString)
            fileCRC32value = FileCRC32(_filePath)
            BeginInvoke(HashUpdate, vbNullString)
            fileSHA512value = FileSHA512(_filePath)
            BeginInvoke(HashUpdate, vbNullString) '执行委托并传递参数
            If isLetterCapital = False Then
                fileMD5value = fileMD5value.ToLower
                fileSHA1value = fileSHA1value.ToLower
                fileSHA256value = fileSHA256value.ToLower
                fileCRC32value = fileCRC32value.ToLower
                fileSHA512value = fileSHA512value.ToLower
            Else
                fileMD5value = fileMD5value.ToUpper
                fileSHA1value = fileSHA1value.ToUpper
                fileSHA256value = fileSHA256value.ToUpper
                fileCRC32value = fileCRC32value.ToUpper
                fileSHA512value = fileSHA512value.ToUpper
            End If
            BeginInvoke(HashUpdate, vbNullString)
            Hashshow()
            GC.Collect() '内存回收
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error") '报错
        End Try
        If remindAfterFinished = True Then
            MsgBox("文件" & _filePath & "的哈希已完成", MsgBoxStyle.Information, "tips")
        End If
        Hashshow()
        calculateThread.Abort() '停止线程
    End Sub
    Private Sub Hashshow()
        'Text = wndTitle & " - 正在读取哈希..."
        lMD5.Text = "MD5:" & fileMD5value
        lSHA1.Text = "SHA1:" & fileSHA1value
        lSHA256.Text = "SHA256:" & fileSHA256value
        lCRC32.Text = "CRC32:" & fileCRC32value
        lSHA512.Text = "SHA512:" & fileSHA512value
    End Sub
#End Region
#Region "哈希计算函数"
    Private Function FileMD5(Filename As String) As String '求MD5
        Dim fs As FileStream '新的文件流
        Dim md5Value = New MD5CryptoServiceProvider
        fs = New FileStream(Filename, FileMode.Open, FileAccess.Read) '读取文件
        Dim md5Result = BitConverter.ToString(md5Value.ComputeHash(fs))
        Dim Result = md5Result.Replace("-", "") '求MD5
        fs.Close() '关闭文件流
        Return Result
    End Function
    Private Function FileSHA1(Filename As String) As String '求SHA1
        Dim fs As FileStream '新的文件流
        Dim sha1Value = New SHA1CryptoServiceProvider
        fs = New FileStream(Filename, FileMode.Open, FileAccess.Read) '读取文件
        Dim sha1Result = BitConverter.ToString(sha1Value.ComputeHash(fs))
        Dim Result = sha1Result.Replace("-", "") '求SHA1
        fs.Close() '关闭文件流
        Return Result
    End Function
    Private Function FileSHA256(Filename As String) As String '求SHA256
        Dim fs As FileStream '新的文件流
        Dim sha256Value = New SHA256CryptoServiceProvider
        fs = New FileStream(Filename, FileMode.Open, FileAccess.Read) '读取文件
        Dim sha256Result = BitConverter.ToString(sha256Value.ComputeHash(fs))
        Dim Result = sha256Result.Replace("-", "") '求SHA256
        fs.Close() '关闭文件流
        Return Result
    End Function
    Private Function calcCRC32(ByVal data() As Byte) As UInt32 '求SHA1
        Static crc As UInt32, crctbl(255) As UInt32
        If data.Length = 0 Then Return 0
        If crc = 0 Then
            For i As Short = 0 To 255
                crc = i
                For j As Byte = 0 To 7
                    If crc And 1 Then crc = (crc >> 1) Xor &HEDB88320& Else crc >>= 1
                Next
                crctbl(i) = crc
            Next
            crc = 1
        End If
        Dim CRC32 = UInt32.MaxValue
        For Each b As Byte In data
            b = b Xor (CRC32 And &HFF)
            CRC32 >>= 8
            CRC32 = CRC32 Xor crctbl(b)
        Next
        Return Not CRC32
    End Function
    Public Function FileCRC32(Filename As String) '计算CRC32
        Dim fs As FileStream '新的文件流
        Dim br As BinaryReader '二进制值
        Dim sha1Value = New SHA1CryptoServiceProvider
        fs = New FileStream(Filename, FileMode.Open, FileAccess.Read) '读取文件
        br = New BinaryReader(fs)
        Dim result = calcCRC32(br.ReadBytes(fs.Length))
        br.Close()
        fs.Close()
        Return Hex(result)
    End Function
    Private Function FileSHA512(Filename As String) As String '求SHA512
        Dim fs As FileStream '新的文件流
        Dim sha512Value = New SHA512CryptoServiceProvider
        fs = New FileStream(Filename, FileMode.Open, FileAccess.Read) '读取文件
        Dim sha512Result = BitConverter.ToString(sha512Value.ComputeHash(fs))
        Dim Result = sha512Result.Replace("-", "") '求SHA512
        fs.Close() '关闭文件流
        Return Result
    End Function
#End Region
#Region "窗体消息处理"
    Protected Overrides Sub WndProc(ByRef m As Message) '窗体消息处理函数
        If m.Msg = WM_SYSCOMMAND Then '窗体响应菜单
            Dim hMenu = GetSystemMenu(Handle, False)
            Select Case m.WParam.ToInt32'对应菜单标号
                Case 1 '窗口置顶
                    If TopMost = False Then
                        TopMost = True
                        CheckMenuItem(hMenu, 1, MF_CHECKED) '窗口置顶
                    Else
                        TopMost = False
                        CheckMenuItem(hMenu, 1, MF_UNCHECKED) '取消置顶
                    End If
                Case 2 '完成后提醒
                    If remindAfterFinished = True Then
                        remindAfterFinished = False
                        CheckMenuItem(hMenu, 2, MF_UNCHECKED)
                    Else
                        remindAfterFinished = True
                        CheckMenuItem(hMenu, 2, MF_CHECKED)
                    End If
                Case 3 '字母大写
                    If isLetterCapital = True Then
                        isLetterCapital = False
                        CheckMenuItem(GetSystemMenu(Handle, False), 3, MF_UNCHECKED)
                        fileMD5value = fileMD5value.ToLower
                        fileSHA1value = fileSHA1value.ToLower
                        fileSHA256value = fileSHA256value.ToLower
                        fileCRC32value = fileCRC32value.ToLower
                        fileSHA512value = fileSHA512value.ToLower
                    Else
                        isLetterCapital = True
                        CheckMenuItem(GetSystemMenu(Handle, False), 3, MF_CHECKED)
                        fileMD5value = fileMD5value.ToUpper
                        fileSHA1value = fileSHA1value.ToUpper
                        fileSHA256value = fileSHA256value.ToUpper
                        fileCRC32value = fileCRC32value.ToUpper
                        fileSHA512value = fileSHA512value.ToUpper
                    End If
                    Hashshow()
                Case 5
                    Dim FileSelect As New OpenFileDialog '定义"打开"对话框
                    FileSelect.Title = "选择文件" '设置标题
                    FileSelect.Filter = "所有文件(*.*)|*.*" '设置过滤器
                    FileSelect.Multiselect = False '不可以多选
                    'FileSelect.InitialDirectory = Application.StartupPath '程序路径
                    FileSelect.ShowDialog() '显示对话框
                    If FileSelect.FileName = "" Then
                        Exit Sub
                    Else
                        _filePath = FileSelect.FileName
                        HashCalc()
                    End If
                Case 6 '复制实例
                    Dim TempFilePath As String
                    TempFilePath = _filePath
                    If IsUserAnAdmin <> 0 Then
                        ShellExecute(Handle, "runas", Application.ExecutablePath, TempFilePath, vbNullString, 10) '运行自身
                    Else
                        ShellExecute(Handle, "open", Application.ExecutablePath, TempFilePath, vbNullString, 10) '运行自身
                    End If
                Case 7 '新建空窗口
                    ShellExecute(Handle, "open", Application.ExecutablePath, vbNullString, vbNullString, 10) '运行自身
                Case 8 '以管理员身份重启
                    runAsElevated()
                Case 10 '从剪贴板校验
                    If Clipboard.ContainsText = False Then
                        MsgBox("剪贴板为空", vbInformation, "tips")
                    Else
                        Dim checkResult = "校验结果：" & vbCrLf
                        If InStr(1, Clipboard.GetText, fileMD5value.ToLower) > 0 Or InStr(1, Clipboard.GetText, fileMD5value.ToUpper) > 0 Then
                            checkResult &= "MD5：√"
                        Else
                            checkResult &= "MD5：×"
                        End If
                        checkResult &= vbCrLf
                        If InStr(1, Clipboard.GetText, fileSHA1value.ToLower) > 0 Or InStr(1, Clipboard.GetText, fileSHA1value.ToUpper) > 0 Then
                            checkResult &= "SHA1：√"
                        Else
                            checkResult &= "SHA1：×"
                        End If
                        checkResult &= vbCrLf
                        If InStr(1, Clipboard.GetText, fileSHA256value.ToLower) > 0 Or InStr(1, Clipboard.GetText, fileSHA256value.ToUpper) > 0 Then
                            checkResult &= "SHA256：√"
                        Else
                            checkResult &= "SHA256：×"
                        End If
                        checkResult &= vbCrLf
                        If InStr(1, Clipboard.GetText, fileCRC32value.ToLower) > 0 Or InStr(1, Clipboard.GetText, fileCRC32value.ToUpper) > 0 Then
                            checkResult &= "CRC32：√"
                        Else
                            checkResult &= "CRC32：×"
                        End If
                        checkResult &= vbCrLf
                        If InStr(1, Clipboard.GetText, fileSHA512value.ToLower) > 0 Or InStr(1, Clipboard.GetText, fileSHA512value.ToUpper) > 0 Then
                            checkResult &= "SHA512：√"
                        Else
                            checkResult &= "SHA512：×"
                        End If
                        checkResult &= vbCrLf
                        checkResult &= "校验已完成。"
                        MsgBox(checkResult, vbInformation, "tips")
                    End If
                Case 11 '打开文件位置
                    If IO.File.Exists(_filePath) = True Then '检测目录是否存在
                        Shell("explorer /select," & """" & _filePath & """", 1)  '打开文件夹
                    End If
                Case 12 '刷新
                    HashCalc()
                Case 14 '关于
                    MsgBox("前面的区域，以后再来探索吧")
                Case 15 '添加注册表
                    If Microsoft.Win32.Registry.ClassesRoot.OpenSubKey("*\shell\Hashsum") Is Nothing Then
                        Dim logoRegistry = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey("*\shell\Hashsum")
                        logoRegistry.SetValue("Icon", Application.ExecutablePath & ",0") '添加图标
                        Dim menuRegistry = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey("*\shell\Hashsum\command")
                        menuRegistry.SetValue("", Application.ExecutablePath & " " & Chr(34) & "%1" & Chr(34)) '快捷方式
                        CheckMenuItem(hMenu, 15, MF_CHECKED)
                    Else
                        Microsoft.Win32.Registry.ClassesRoot.DeleteSubKey("*\shell\Hashsum\command")
                        Microsoft.Win32.Registry.ClassesRoot.DeleteSubKey("*\shell\Hashsum")
                        CheckMenuItem(hMenu, 15, MF_UNCHECKED)
                    End If
                Case 16
                    ClearWnd()
            End Select
        End If
        If m.Msg = WM_DWMCOLORIZATIONCOLORCHANGED Then
            systemThemeChange() '系统主题发生变化
        End If
        MyBase.WndProc(m) '循环监听消息
    End Sub '处理窗口信息
#End Region
#Region "辅助函数"
    Private Function updateUpper()
        If isLetterCapital = True Then
            fileMD5value = fileMD5value.ToLower
            fileSHA1value = fileSHA1value.ToLower
            fileSHA256value = fileSHA256value.ToLower
            fileCRC32value = fileCRC32value.ToLower
            fileSHA512value = fileSHA512value.ToLower
        Else
            fileMD5value = fileMD5value.ToUpper
            fileSHA1value = fileSHA1value.ToUpper
            fileSHA256value = fileSHA256value.ToUpper
            fileCRC32value = fileCRC32value.ToUpper
            fileSHA512value = fileSHA512value.ToUpper
        End If
        Hashshow()
        Return 0
    End Function '更新大小写
    Private Function runAsElevated()
        Dim a As IntPtr
        Dim TempFilePath As String
        TempFilePath = _filePath
        a = ShellExecute(Handle, "runas", Application.ExecutablePath, TempFilePath, vbNullString, 10) '管理员模式运行自身
        If a.ToInt32 > 32 Then '如果成功运行
            Dispose() '关闭程序
        ElseIf a.ToInt32 = 5 Then
            MsgBox("权限提升失败。" & vbCrLf & "操作被用户取消。", vbInformation, "tips") '弹出提示
        End If
        Return 0
    End Function '管理员模式运行
    Private Function systemThemeChange()
        Dim dKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", True).GetValue("AppsUseLightTheme", "0") '判断是否为深色主题
        If dKey = 0 Then '如果是深色模式
            BackColor = Color.FromArgb(32, 33, 36)
            For Each Ctls In Me.Controls '获取控件集合
                Ctls.ForeColor = Color.FromArgb(218, 220, 224)
                Ctls.BackColor = Color.FromArgb(32, 33, 36)
            Next
            ThemeColor = True
        Else
            BackColor = Color.FromArgb(255, 255, 255)
            For Each Ctls In Me.Controls '获取控件集合
                Ctls.ForeColor = Color.FromArgb(0, 0, 0)
                Ctls.BackColor = Color.FromArgb(255, 255, 255)
            Next
            ThemeColor = False
        End If
        DwmSetWindowAttribute(Handle, DwmWindowAttribute.UseImmersiveDarkMode, ThemeColor, Marshal.SizeOf(Of Integer))
        'SetWindowTheme(Handle, L"Explorer", vbNull)
        'FlushMenuThemes()
        SetPreferredAppMode(PreferredAppMode.AllowDark)
        FlushMenuThemes()
        Return 0
    End Function '系统主题发生改变
    Private Function ByteFormat(Bytesize As Long) As String
        Dim result As String
        If Bytesize <= Math.Pow(1024, 1) Then
            result = Bytesize.ToString & "Byte"
        ElseIf Bytesize > Math.Pow(1024, 1) And Bytesize <= Math.Pow(1024, 2) Then
            result = Math.Round(Bytesize / Math.Pow(1024, 1), 1).ToString & "KB"
        ElseIf Bytesize > Math.Pow(1024, 2) And Bytesize <= Math.Pow(1024, 3) Then
            result = Math.Round(Bytesize / Math.Pow(1024, 2), 1).ToString & "MB"
        ElseIf Bytesize > Math.Pow(1024, 3) And Bytesize <= Math.Pow(1024, 4) Then
            result = Math.Round(Bytesize / Math.Pow(1024, 3), 1).ToString & "GB"
        ElseIf Bytesize > Math.Pow(1024, 4) Then
            result = Math.Round(Bytesize / Math.Pow(1024, 4), 1).ToString & "TB"
        Else
            result = Bytesize.ToString & "Byte"
        End If
        Return result
    End Function '文件大小格式进位
#End Region
#Region "点击复制"
    Private Sub lFilename_Click(sender As Object, e As EventArgs) Handles lFilename.Click
        Clipboard.SetDataObject(_filePath)
        MsgBox("文件路径已复制到剪贴板。", vbInformation, "tips")
    End Sub
    Private Sub lMD5_Click(sender As Object, e As EventArgs) Handles lMD5.Click
        Clipboard.SetDataObject(fileMD5value)
        MsgBox("文件" & _filePath & "的MD5已复制到剪贴板。", vbInformation, "tips")
    End Sub
    Private Sub lSHA1_Click(sender As Object, e As EventArgs) Handles lSHA1.Click
        Clipboard.SetDataObject(fileSHA1value)
        MsgBox("文件" & _filePath & "的SHA1已复制到剪贴板。", vbInformation, "tips")
    End Sub
    Private Sub lSHA256_Click(sender As Object, e As EventArgs) Handles lSHA256.Click
        Clipboard.SetDataObject(fileSHA256value)
        MsgBox("文件" & _filePath & "的SHA256已复制到剪贴板。", vbInformation, "tips")
    End Sub
    Private Sub lCRC32_Click(sender As Object, e As EventArgs) Handles lCRC32.Click
        Clipboard.SetDataObject(fileCRC32value)
        MsgBox("文件" & _filePath & "的CRC32已复制到剪贴板。", vbInformation, "tips")
    End Sub
    Private Sub lSHA512_Click(sender As Object, e As EventArgs) Handles lSHA512.Click
        Clipboard.SetDataObject(fileSHA512value)
        MsgBox("文件" & _filePath & "的SHA512已复制到剪贴板。", vbInformation, "tips")
    End Sub
#End Region
#Region "窗体激活和初始化"
    Private Sub frmMain_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If _filePath = "" Then
            Exit Sub
        End If
        If IO.File.Exists(_filePath) = False And fileDelFlag = False Then
            Text = "[X] " & wndTitle
            If MsgBox("当前文件可能被删除，是否清空当前哈希值？", vbYesNo + vbExclamation, "tips") = vbYes Then
                ClearWnd()
            Else
                fileEditFlag = True
                fileDelFlag = True
            End If
            Exit Sub
        End If
        If IO.File.GetLastWriteTime(_filePath) <> firstWriteDate And fileEditFlag = False Then
            Text = "[@] " & wndTitle
            If MsgBox("当前文件可能被修改，是否重新计算哈希值？", vbYesNo + vbExclamation, "tips") = vbYes Then
                HashCalc()
            Else
                fileEditFlag = True
            End If
            Exit Sub
        End If
    End Sub
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim MnuHandle = GetSystemMenu(Handle, False) '获取菜单句柄
        AppendMenu(MnuHandle, MF_SEPARATOR, 0, vbNullString) '添加分隔符
        AppendMenu(MnuHandle, MF_STRING, 15, "文件右键菜单(&G)")
        AppendMenu(MnuHandle, MF_STRING, 1, "窗口置顶(&T)")
        AppendMenu(MnuHandle, MF_STRING, 2, "完成后提醒(&E)")
        AppendMenu(MnuHandle, MF_STRING, 3, "字母大写(&L)")
        AppendMenu(MnuHandle, MF_SEPARATOR, 4, vbNullString)
        AppendMenu(MnuHandle, MF_STRING, 5, "打开文件(&O)...")
        AppendMenu(MnuHandle, MF_STRING, 6, "复制当前实例(&D)")
        AppendMenu(MnuHandle, MF_STRING, 7, "新建空窗口(&N)")
        AppendMenu(MnuHandle, MF_STRING, 8, "以管理员身份重启(&R)")
        AppendMenu(MnuHandle, MF_SEPARATOR, 9, vbNullString)
        AppendMenu(MnuHandle, MF_STRING, 10, "从剪贴板校验(&H)...")
        AppendMenu(MnuHandle, MF_STRING, 11, "打开文件位置(&F)")
        AppendMenu(MnuHandle, MF_STRING, 12, "刷新(&S)")
        AppendMenu(MnuHandle, MF_STRING, 16, "关闭当前文件(&C)")
        AppendMenu(MnuHandle, MF_SEPARATOR, 13, vbNullString)
        AppendMenu(MnuHandle, MF_STRING, 14, "关于Hashsum(&A)...") '添加菜单
        'RemoveMenu(MnuHandle, 61728, MF_BYCOMMAND) '去除“还原”菜单
        'RemoveMenu(MnuHandle, 61488, MF_BYCOMMAND) '去除“最大化”菜单
        'RemoveMenu(MnuHandle, 61440, MF_BYCOMMAND) '去除“大小”菜单
        'Dim mIcon As IntPtr, mHandle As IntPtr
        'ExtractIconEx("shell32.dll", 252, mHandle, mHandle, 1)
        'MsgBox(GetIconInfo(mHandle, mIcon))
        SetMenuItemBitmaps(MnuHandle, 14, MF_BYCOMMAND, 1, vbNullString)
        If Microsoft.Win32.Registry.ClassesRoot.OpenSubKey("*\shell\Hashsum\command") Is Nothing Then
            CheckMenuItem(MnuHandle, 15, MF_UNCHECKED)
        Else
            CheckMenuItem(MnuHandle, 15, MF_CHECKED)
        End If '判断文件菜单是否存在
        If IsUserAnAdmin <> 0 Then '如果以管理员模式运行
            wndTitle = "Hashsum [管理员权限]"
            EnableMenuItem(GetSystemMenu(Handle, False), 8, MF_GRAYED) '菜单无效
            'MsgBox(ChangeWindowMessageFilterEx(Handle, WM_DROPFILES, MSGFLT_ALLOW, vbNull))
            'MsgBox(ChangeWindowMessageFilterEx(Handle, WM_COPYDATA, MSGFLT_ALLOW, vbNull))
            'MsgBox(ChangeWindowMessageFilter(WM_DROPFILES, MSGFLT_ADD))
            'MsgBox(ChangeWindowMessageFilter(WM_COPYDATA, MSGFLT_ADD))
        Else
            wndTitle = "Hashsum"
            EnableMenuItem(GetSystemMenu(Handle, False), 15, MF_GRAYED) '不能修改右键菜单
        End If
        Text = wndTitle
        systemThemeChange() '检测系统主题
        remindAfterFinished = False
        CheckMenuItem(MnuHandle, 2, MF_UNCHECKED) '默认勾选
        isLetterCapital = True
        CheckMenuItem(MnuHandle, 3, MF_CHECKED) '默认勾选
        ClearWnd()
        Dim cmdLine = Replace(Microsoft.VisualBasic.Command, """", "")
        If IO.File.Exists(cmdLine) = True Then
            _filePath = cmdLine
            HashCalc()
            Exit Sub
        End If
        If cmdLine <> "" Then
            MsgBox("文件路径或参数错误！", MsgBoxStyle.Critical, "Error") '报错
        End If
        SetPreferredAppMode(PreferredAppMode.AllowDark)
        FlushMenuThemes()
    End Sub
#End Region
#Region "函数定义"
    <DllImport("uxtheme.dll", EntryPoint:="#135", SetLastError:=True, CharSet:=CharSet.Unicode)>
    Public Shared Function SetPreferredAppMode(ByVal PreferredAppMode As PreferredAppMode) As Long
        '修改菜单颜色
    End Function
    <DllImport("uxtheme.dll", EntryPoint:="#135", SetLastError:=True, CharSet:=CharSet.Unicode)>
    Public Shared Function SetWindowTheme(ByVal hwnd As IntPtr, ByVal pszSubAppName As String, ByVal pszSubIdList As String) As Long
        '使用主题
    End Function
    <DllImport("uxtheme.dll", EntryPoint:="#136", SetLastError:=True, CharSet:=CharSet.Unicode)>
    Public Shared Function FlushMenuThemes() As Long
        '修改菜单颜色
    End Function
    Public Enum PreferredAppMode
        _default
        AllowDark
        ForceDark
        ForceLight
        Max
    End Enum
#End Region
End Class
