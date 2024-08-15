Module API
    Public Declare Function GetSystemMenu Lib "user32" (
        ByVal hwnd As Long,'菜单对应的窗口句柄
        ByVal bRevert As Long'标志位
        ) As Long '获取窗口菜单（最小化，最大化，关闭）
    Public Declare Function AppendMenu Lib "user32" Alias "AppendMenuA" (
        ByVal hMenu As Long,'菜单句柄
        ByVal wFlags As Long,'控制菜单外观与性能
        ByVal wIDNewItem As Long,'新菜单项标识符
        ByVal lpNewItem As String'新菜单项内容
        ) As Long '追加菜单
    Public Declare Function RemoveMenu Lib "user32" (
        ByVal hMenu As Long,'菜单句柄
        ByVal uPosition As Long,'标识符
        ByVal uFlags As Long'效果
        ) As Long '移除菜单
    Public Declare Function CheckMenuItem Lib "user32" (
        ByVal hMenu As Long,'菜单句柄
        ByVal uIDCheckItem As Long,'标识符
        ByVal uCheck As Long'标识符效果
        ) As Long '选中/清除选中菜单项
    Public Declare Function SetMenuItemBitmaps Lib "user32" (
        ByVal hMenu As Long,'菜单句柄
        ByVal uPosition As Long,'要更改的菜单项
        ByVal uFlags As Long,'标识符效果
        ByVal hBitmapUnchecked As Long,'未选择菜单项时显示的位图的句柄
        ByVal hBitmapChecked As Long'选择菜单项时显示的位图的句柄
        ) As Long '选中/清除选中菜单项
    Public Declare Function ChangeWindowMessageFilterEx Lib "user32.dll" (
        ByVal hwnd As Long,
        ByVal message As Long,
        ByVal action As Long,
        ByVal PCHANGEFILTERSTRUCT As IntPtr
        ) As Boolean '修改指定窗口 (UIPI) 消息筛选器的用户界面特权隔离。解除管理员模式下无法拖拽的问题
    Public Declare Function ChangeWindowMessageFilter Lib "user32.dll" (
        ByVal message As Long,
        ByVal dwFlag As Long
        ) As Boolean
    Public Declare Function DwmSetWindowAttribute Lib "DwmApi.dll" (
        ByVal hwnd As IntPtr,
        ByVal attr As DwmWindowAttribute,
        ByRef attrValue As Integer,
        ByVal attrSize As Integer
        ) As Long '修改标题栏颜色
    Public Declare Function FlushMenuThemes Lib "UxTheme.dll" (
        ) As Long '修改菜单颜色
    'Public Declare Function SetPreferredAppMode Lib "UxTheme.dll" (
    '    ByVal PreferredAppMode As PreferredAppMode
    '    ) As Long '修改菜单颜色
    'Public Declare Function SetWindowTheme Lib "UxTheme.dll" (
    '    ByVal hwnd As IntPtr,
    '    ByVal pszSubAppName As String,
    '    ByVal pszSubIdList As String
    '    ) As Long '使用主题
    'Public Enum PreferredAppMode
    '    _default
    '    AllowDark
    '    ForceDark
    '    ForceLight
    '    Max
    'End Enum
    Public Enum DwmWindowAttribute
        NCRenderingEnabled = 1
        NCRenderingPolicy
        TransitionsForceDisabled
        AllowNCPaint
        CaptionButtonBounds
        NonClientRtlLayout
        ForceIconicRepresentation
        Flip3DPolicy
        ExtendedFrameBounds
        HasIconicBitmap
        DisallowPeek
        ExcludedFromPeek
        Cloak
        Cloaked
        FreezeRepresentation
        PassiveUpdateMode
        UseHostBackdropBrush
        UseImmersiveDarkMode = 20
        WindowCornerPreference = 33
        BorderColor
        CaptionColor
        TextColor
        VisibleFrameBorderThickness
        SystemBackdropType
        Last
    End Enum
    '窗口拖拽文件消息常量
    Public Const WM_DROPFILES = &H233S
    Public Const WM_COPYGLOBALDATA = &H49S
    Public Const WM_COPYDATA = &H4AS
    Public Const MSGFLT_ALLOW = 1
    Public Const MSGFLT_ADD = 1
    Public Const MSGFLT_REMOVE = 2
    '窗口常量
    Public Const WM_SYSCOLORCHANGE = &H15S '当系统颜色改变时，发送此消息给所有顶级窗口
    Public Const WM_SETFOCUS = &H7S '窗体获得焦点
    Public Const WM_KILLFOCUS = &H8S '窗体失去焦点
    Public Const WM_COMMAND = &H111 '窗体选择菜单项
    Public Const WM_SYSCOMMAND = &H112 '窗体选择系统菜单项
    Public Const WM_DWMCOLORIZATIONCOLORCHANGED = &H320 '窗体主题色被更改（深色同样有效）
    '菜单常量
    Public Const MF_SEPARATOR = &H800 '分隔符
    Public Const MF_STRING = &H0 '字符串
    Public Const MF_BITMAP = &H4 '位图
    Public Const MF_GRAYED = &H1 '灰色菜单
    Public Const MF_ENABLED = &H0 '菜单可用
    Public Const MF_CHECKED = &H8 '勾选
    Public Const MF_UNCHECKED = &H0 '取消勾选
    Public Const MF_HILITE = &H80 '高亮
    Public Const MF_BYCOMMAND = &H0 '标识符
    Public Const MF_BYPOSITION = &H400 '位置
    Public Declare Function IsUserAnAdmin Lib "Shell32" Alias "IsUserAnAdmin" () As Integer  '获取当前权限
    Public Declare Function ShellExecute Lib "shell32.dll " Alias "ShellExecuteA" (
        ByVal hWnd As IntPtr,
        ByVal lpOperation As String,
        ByVal lpFile As String,
        ByVal lpParameters As String,
        ByVal lpDirectory As String,
        ByVal nShowCmd As Int32) As IntPtr '运行程序
    Public Declare Function EnableMenuItem Lib "user32" (
        ByVal hMenu As Long,
        ByVal uIDEnableItem As Long,
        ByVal uEnable As Long
        ) As Long '使菜单在有效与无效之间切换
End Module
