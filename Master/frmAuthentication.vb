Public Class frmAuthentication
    Inherits System.Windows.Forms.Form
    Public Valid As String

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdSignin As System.Windows.Forms.Button
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtUserID As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdSignin = New System.Windows.Forms.Button
        Me.txtPassword = New System.Windows.Forms.TextBox
        Me.txtUserID = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.Color.Yellow
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.Location = New System.Drawing.Point(176, 105)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(84, 22)
        Me.cmdCancel.TabIndex = 3
        Me.cmdCancel.Text = "Cancel (Esc)"
        '
        'cmdSignin
        '
        Me.cmdSignin.BackColor = System.Drawing.Color.Yellow
        Me.cmdSignin.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdSignin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSignin.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSignin.Location = New System.Drawing.Point(89, 105)
        Me.cmdSignin.Name = "cmdSignin"
        Me.cmdSignin.Size = New System.Drawing.Size(75, 22)
        Me.cmdSignin.TabIndex = 2
        Me.cmdSignin.Text = "OK (F4)"
        '
        'txtPassword
        '
        Me.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPassword.Location = New System.Drawing.Point(160, 60)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(106, 20)
        Me.txtPassword.TabIndex = 1
        Me.txtPassword.Text = ""
        '
        'txtUserID
        '
        Me.txtUserID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUserID.Location = New System.Drawing.Point(160, 30)
        Me.txtUserID.Name = "txtUserID"
        Me.txtUserID.Size = New System.Drawing.Size(106, 20)
        Me.txtUserID.TabIndex = 0
        Me.txtUserID.Text = ""
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(75, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 16)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Password :"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(75, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 20)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "User ID :"
        '
        'frmAuthentication
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(349, 163)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdSignin)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.txtUserID)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAuthentication"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Authentication"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub cmdSignin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSignin.Click
        Dim sUid As String
        Dim sPwd As String
        sUid = Trim(txtUserID.Text)
        sPwd = Trim(txtPassword.Text)
        If sUid <> "" And sPwd <> "" Then
            If LCase(sUid) = "unipro" And LCase(sPwd) = "ups" Then
                Valid = "ok"
                Me.Dispose(True)
            Else
                Valid = "cancel"
                MsgBox("Invalid UserID or Password", MsgBoxStyle.Critical)
                txtUserID.Focus()
                Exit Sub
            End If
        Else
            Valid = "cancel"
            MsgBox("Type Some valid UserID and Password", MsgBoxStyle.Critical)
            txtUserID.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Valid = "cancel"
        Me.Dispose(True)
    End Sub

    Private Sub frmAuthentication_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        On Error GoTo ErrorSub
        Select Case e.KeyCode
            Case Keys.Enter : SendKeys.Send("{TAB}")
            Case Keys.F4
                cmdSignin_Click(sender, e)
            Case Keys.Escape
                cmdCancel_Click(sender, e)
            Case Keys.Up : SendKeys.Send("+{Tab}")
            Case Keys.Down : SendKeys.Send("{Tab}")
        End Select
        Exit Sub
ErrorSub:
        MsgBox(Err.Description)
        Err.Clear()
    End Sub
    End Class
