<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.lMD5 = New System.Windows.Forms.Label()
        Me.lSHA1 = New System.Windows.Forms.Label()
        Me.lSHA256 = New System.Windows.Forms.Label()
        Me.lCRC32 = New System.Windows.Forms.Label()
        Me.lSHA512 = New System.Windows.Forms.Label()
        Me.lFilename = New System.Windows.Forms.Label()
        Me.lFilename2 = New System.Windows.Forms.Label()
        Me.lFilePath = New System.Windows.Forms.Label()
        Me.lFileSize = New System.Windows.Forms.Label()
        Me.lCreateTime = New System.Windows.Forms.Label()
        Me.lModifyTime = New System.Windows.Forms.Label()
        Me.lCheckTime = New System.Windows.Forms.Label()
        Me.lProperties = New System.Windows.Forms.Label()
        Me.PBFilewizard = New System.Windows.Forms.PictureBox()
        Me.lFileType = New System.Windows.Forms.Label()
        CType(Me.PBFilewizard, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lMD5
        '
        Me.lMD5.Location = New System.Drawing.Point(17, 45)
        Me.lMD5.Name = "lMD5"
        Me.lMD5.Size = New System.Drawing.Size(580, 15)
        Me.lMD5.TabIndex = 0
        Me.lMD5.Text = "MD5:"
        '
        'lSHA1
        '
        Me.lSHA1.Location = New System.Drawing.Point(17, 71)
        Me.lSHA1.Name = "lSHA1"
        Me.lSHA1.Size = New System.Drawing.Size(580, 15)
        Me.lSHA1.TabIndex = 1
        Me.lSHA1.Text = "SHA1:"
        '
        'lSHA256
        '
        Me.lSHA256.Location = New System.Drawing.Point(17, 97)
        Me.lSHA256.Name = "lSHA256"
        Me.lSHA256.Size = New System.Drawing.Size(580, 15)
        Me.lSHA256.TabIndex = 2
        Me.lSHA256.Text = "SHA256:"
        '
        'lCRC32
        '
        Me.lCRC32.Location = New System.Drawing.Point(17, 123)
        Me.lCRC32.Name = "lCRC32"
        Me.lCRC32.Size = New System.Drawing.Size(580, 15)
        Me.lCRC32.TabIndex = 3
        Me.lCRC32.Text = "CRC32:"
        '
        'lSHA512
        '
        Me.lSHA512.Location = New System.Drawing.Point(17, 149)
        Me.lSHA512.Name = "lSHA512"
        Me.lSHA512.Size = New System.Drawing.Size(580, 36)
        Me.lSHA512.TabIndex = 8
        Me.lSHA512.Text = "SHA512:"
        '
        'lFilename
        '
        Me.lFilename.Location = New System.Drawing.Point(17, 19)
        Me.lFilename.Name = "lFilename"
        Me.lFilename.Size = New System.Drawing.Size(580, 15)
        Me.lFilename.TabIndex = 9
        Me.lFilename.Text = "File:"
        '
        'lFilename2
        '
        Me.lFilename2.Location = New System.Drawing.Point(17, 191)
        Me.lFilename2.Name = "lFilename2"
        Me.lFilename2.Size = New System.Drawing.Size(580, 15)
        Me.lFilename2.TabIndex = 10
        Me.lFilename2.Text = "文件名称:"
        '
        'lFilePath
        '
        Me.lFilePath.Location = New System.Drawing.Point(17, 216)
        Me.lFilePath.Name = "lFilePath"
        Me.lFilePath.Size = New System.Drawing.Size(580, 15)
        Me.lFilePath.TabIndex = 11
        Me.lFilePath.Text = "文件路径:"
        '
        'lFileSize
        '
        Me.lFileSize.Location = New System.Drawing.Point(17, 241)
        Me.lFileSize.Name = "lFileSize"
        Me.lFileSize.Size = New System.Drawing.Size(580, 15)
        Me.lFileSize.TabIndex = 12
        Me.lFileSize.Text = "大小:"
        '
        'lCreateTime
        '
        Me.lCreateTime.Location = New System.Drawing.Point(17, 266)
        Me.lCreateTime.Name = "lCreateTime"
        Me.lCreateTime.Size = New System.Drawing.Size(580, 15)
        Me.lCreateTime.TabIndex = 13
        Me.lCreateTime.Text = "创建时间:"
        '
        'lModifyTime
        '
        Me.lModifyTime.Location = New System.Drawing.Point(17, 291)
        Me.lModifyTime.Name = "lModifyTime"
        Me.lModifyTime.Size = New System.Drawing.Size(580, 15)
        Me.lModifyTime.TabIndex = 14
        Me.lModifyTime.Text = "修改时间:"
        '
        'lCheckTime
        '
        Me.lCheckTime.Location = New System.Drawing.Point(17, 316)
        Me.lCheckTime.Name = "lCheckTime"
        Me.lCheckTime.Size = New System.Drawing.Size(580, 15)
        Me.lCheckTime.TabIndex = 15
        Me.lCheckTime.Text = "访问时间:"
        '
        'lProperties
        '
        Me.lProperties.Location = New System.Drawing.Point(17, 341)
        Me.lProperties.Name = "lProperties"
        Me.lProperties.Size = New System.Drawing.Size(580, 15)
        Me.lProperties.TabIndex = 16
        Me.lProperties.Text = "属性:"
        '
        'PBFilewizard
        '
        Me.PBFilewizard.Image = CType(resources.GetObject("PBFilewizard.Image"), System.Drawing.Image)
        Me.PBFilewizard.Location = New System.Drawing.Point(0, 5)
        Me.PBFilewizard.Name = "PBFilewizard"
        Me.PBFilewizard.Size = New System.Drawing.Size(612, 381)
        Me.PBFilewizard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PBFilewizard.TabIndex = 17
        Me.PBFilewizard.TabStop = False
        '
        'lFileType
        '
        Me.lFileType.Location = New System.Drawing.Point(16, 366)
        Me.lFileType.Name = "lFileType"
        Me.lFileType.Size = New System.Drawing.Size(580, 15)
        Me.lFileType.TabIndex = 18
        Me.lFileType.Text = "文件类型:"
        '
        'frmMain
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(612, 391)
        Me.Controls.Add(Me.PBFilewizard)
        Me.Controls.Add(Me.lFileType)
        Me.Controls.Add(Me.lProperties)
        Me.Controls.Add(Me.lCheckTime)
        Me.Controls.Add(Me.lModifyTime)
        Me.Controls.Add(Me.lCreateTime)
        Me.Controls.Add(Me.lFileSize)
        Me.Controls.Add(Me.lFilePath)
        Me.Controls.Add(Me.lFilename2)
        Me.Controls.Add(Me.lFilename)
        Me.Controls.Add(Me.lSHA512)
        Me.Controls.Add(Me.lCRC32)
        Me.Controls.Add(Me.lSHA256)
        Me.Controls.Add(Me.lSHA1)
        Me.Controls.Add(Me.lMD5)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.Text = "Hashsum"
        CType(Me.PBFilewizard, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lMD5 As Label
    Friend WithEvents lSHA1 As Label
    Friend WithEvents lSHA256 As Label
    Friend WithEvents lCRC32 As Label
    Friend WithEvents lSHA512 As Label
    Friend WithEvents lFilename As Label
    Friend WithEvents lFilename2 As Label
    Friend WithEvents lFilePath As Label
    Friend WithEvents lFileSize As Label
    Friend WithEvents lCreateTime As Label
    Friend WithEvents lModifyTime As Label
    Friend WithEvents lCheckTime As Label
    Friend WithEvents lProperties As Label
    Friend WithEvents PBFilewizard As PictureBox
    Friend WithEvents lFileType As Label
End Class
