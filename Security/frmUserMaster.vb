Public Class frmUserMaster
    Inherits System.Windows.Forms.Form
    Dim Dm As New DataManager
    Dim objClsUserMaster As New clsUserMasters
    Const USERCODE = 1
    Const USERNAME = 2
    Const USERGROUP = 3
    Const EXPDATE = 4
    Friend WithEvents grdUserMaster As System.Windows.Forms.DataGridView
    Friend WithEvents cUserMaster As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cUsermasterName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cD As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cE As System.Windows.Forms.DataGridViewTextBoxColumn
    Const NOOFDAYS = 5

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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdDelete As System.Windows.Forms.Button
    Friend WithEvents cmdUpdate As System.Windows.Forms.Button
    Friend WithEvents cmdNew As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtUserCode As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtUserMasterName As System.Windows.Forms.TextBox
    Friend WithEvents txtUserMasterCode As System.Windows.Forms.TextBox
    Friend WithEvents txtDaysOfWarning As System.Windows.Forms.TextBox
    Friend WithEvents cboExpiredDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents errP As System.Windows.Forms.ErrorProvider
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents cboUserGroupCode As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUserMaster))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cboExpiredDate = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtDaysOfWarning = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtUserMasterName = New System.Windows.Forms.TextBox()
        Me.txtUserMasterCode = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboUserGroupCode = New System.Windows.Forms.ComboBox()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdUpdate = New System.Windows.Forms.Button()
        Me.cmdNew = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtUserCode = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.errP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.grdUserMaster = New System.Windows.Forms.DataGridView()
        Me.cUserMaster = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cUsermasterName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.errP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdUserMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.Controls.Add(Me.cboExpiredDate)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtDaysOfWarning)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtUserMasterName)
        Me.GroupBox1.Controls.Add(Me.txtUserMasterCode)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.cboUserGroupCode)
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(380, 120)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(356, 176)
        Me.GroupBox1.TabIndex = 27
        Me.GroupBox1.TabStop = False
        '
        'cboExpiredDate
        '
        Me.cboExpiredDate.CustomFormat = ""
        Me.cboExpiredDate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboExpiredDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.cboExpiredDate.Location = New System.Drawing.Point(141, 105)
        Me.cboExpiredDate.Name = "cboExpiredDate"
        Me.cboExpiredDate.Size = New System.Drawing.Size(209, 21)
        Me.cboExpiredDate.TabIndex = 3
        Me.cboExpiredDate.Value = New Date(2007, 1, 10, 0, 0, 0, 0)
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(11, 109)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(112, 16)
        Me.Label7.TabIndex = 31
        Me.Label7.Text = "Expired Date"
        '
        'txtDaysOfWarning
        '
        Me.txtDaysOfWarning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDaysOfWarning.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDaysOfWarning.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDaysOfWarning.Location = New System.Drawing.Point(141, 134)
        Me.txtDaysOfWarning.MaxLength = 30
        Me.txtDaysOfWarning.Name = "txtDaysOfWarning"
        Me.txtDaysOfWarning.Size = New System.Drawing.Size(209, 21)
        Me.txtDaysOfWarning.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(11, 136)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(124, 16)
        Me.Label6.TabIndex = 29
        Me.Label6.Text = "Days Of Warning"
        '
        'txtUserMasterName
        '
        Me.txtUserMasterName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUserMasterName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtUserMasterName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserMasterName.Location = New System.Drawing.Point(141, 45)
        Me.txtUserMasterName.MaxLength = 50
        Me.txtUserMasterName.Name = "txtUserMasterName"
        Me.txtUserMasterName.Size = New System.Drawing.Size(209, 21)
        Me.txtUserMasterName.TabIndex = 1
        '
        'txtUserMasterCode
        '
        Me.txtUserMasterCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtUserMasterCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUserMasterCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtUserMasterCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserMasterCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtUserMasterCode.Location = New System.Drawing.Point(141, 16)
        Me.txtUserMasterCode.MaxLength = 10
        Me.txtUserMasterCode.Name = "txtUserMasterCode"
        Me.txtUserMasterCode.Size = New System.Drawing.Size(209, 21)
        Me.txtUserMasterCode.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(11, 78)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(112, 23)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "User Group Code"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(11, 47)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(112, 23)
        Me.Label3.TabIndex = 26
        Me.Label3.Text = "User Name"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(11, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(112, 23)
        Me.Label5.TabIndex = 25
        Me.Label5.Text = "User Code"
        '
        'cboUserGroupCode
        '
        Me.cboUserGroupCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboUserGroupCode.Location = New System.Drawing.Point(141, 74)
        Me.cboUserGroupCode.Name = "cboUserGroupCode"
        Me.cboUserGroupCode.Size = New System.Drawing.Size(209, 21)
        Me.cboUserGroupCode.TabIndex = 2
        Me.cboUserGroupCode.Text = "ComboBox1"
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.Location = New System.Drawing.Point(636, 319)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(100, 40)
        Me.cmdClose.TabIndex = 8
        Me.cmdClose.Text = "Close (Esc)"
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdDelete.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.Location = New System.Drawing.Point(529, 319)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(100, 40)
        Me.cmdDelete.TabIndex = 7
        Me.cmdDelete.Text = "Delete (F8)"
        Me.cmdDelete.UseVisualStyleBackColor = False
        '
        'cmdUpdate
        '
        Me.cmdUpdate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdUpdate.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUpdate.Location = New System.Drawing.Point(422, 319)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.Size = New System.Drawing.Size(100, 40)
        Me.cmdUpdate.TabIndex = 6
        Me.cmdUpdate.Text = "Update (F7)"
        Me.cmdUpdate.UseVisualStyleBackColor = False
        '
        'cmdNew
        '
        Me.cmdNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdNew.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdNew.Location = New System.Drawing.Point(315, 319)
        Me.cmdNew.Name = "cmdNew"
        Me.cmdNew.Size = New System.Drawing.Size(100, 40)
        Me.cmdNew.TabIndex = 5
        Me.cmdNew.Text = "New (F6)"
        Me.cmdNew.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.GroupBox2.Controls.Add(Me.txtUserName)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.txtUserCode)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox2.Location = New System.Drawing.Point(165, -28)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(264, 156)
        Me.GroupBox2.TabIndex = 27
        Me.GroupBox2.TabStop = False
        '
        'txtUserName
        '
        Me.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUserName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtUserName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtUserName.Location = New System.Drawing.Point(116, 96)
        Me.txtUserName.MaxLength = 50
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(120, 20)
        Me.txtUserName.TabIndex = 1
        Me.txtUserName.Tag = "NE"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(23, 96)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 14)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = " Group Name :"
        '
        'txtUserCode
        '
        Me.txtUserCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUserCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtUserCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtUserCode.Location = New System.Drawing.Point(114, 40)
        Me.txtUserCode.MaxLength = 50
        Me.txtUserCode.Name = "txtUserCode"
        Me.txtUserCode.Size = New System.Drawing.Size(120, 20)
        Me.txtUserCode.TabIndex = 0
        Me.txtUserCode.Tag = "NE"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(24, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 14)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = " Group Code :"
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.Location = New System.Drawing.Point(347, 160)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(80, 24)
        Me.Button1.TabIndex = 26
        Me.Button1.Text = "Close (Esc)"
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Image = CType(resources.GetObject("Button2.Image"), System.Drawing.Image)
        Me.Button2.Location = New System.Drawing.Point(251, 160)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(80, 24)
        Me.Button2.TabIndex = 25
        Me.Button2.Text = "Delete (F6)"
        '
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Image = CType(resources.GetObject("Button3.Image"), System.Drawing.Image)
        Me.Button3.Location = New System.Drawing.Point(163, 160)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(80, 24)
        Me.Button3.TabIndex = 24
        Me.Button3.Text = "Update (F5)"
        '
        'Button4
        '
        Me.Button4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.Image = CType(resources.GetObject("Button4.Image"), System.Drawing.Image)
        Me.Button4.Location = New System.Drawing.Point(67, 160)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(80, 24)
        Me.Button4.TabIndex = 23
        Me.Button4.Text = "New (F2)"
        '
        'errP
        '
        Me.errP.ContainerControl = Me
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(0, 0)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 100)
        Me.pic.TabIndex = 81
        Me.pic.TabStop = False
        '
        'grdUserMaster
        '
        Me.grdUserMaster.AllowUserToAddRows = False
        Me.grdUserMaster.AllowUserToDeleteRows = False
        Me.grdUserMaster.AllowUserToResizeColumns = False
        Me.grdUserMaster.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdUserMaster.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdUserMaster.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cUserMaster, Me.cUsermasterName, Me.cC, Me.cD, Me.cE})
        Me.grdUserMaster.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdUserMaster.Location = New System.Drawing.Point(0, 126)
        Me.grdUserMaster.Name = "grdUserMaster"
        Me.grdUserMaster.RowHeadersVisible = False
        Me.grdUserMaster.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdUserMaster.Size = New System.Drawing.Size(373, 169)
        Me.grdUserMaster.TabIndex = 82
        '
        'cUserMaster
        '
        Me.cUserMaster.HeaderText = "UserMaster"
        Me.cUserMaster.Name = "cUserMaster"
        Me.cUserMaster.Width = 150
        '
        'cUsermasterName
        '
        Me.cUsermasterName.HeaderText = "User Master Name"
        Me.cUsermasterName.Name = "cUsermasterName"
        Me.cUsermasterName.Width = 200
        '
        'cC
        '
        Me.cC.HeaderText = "C"
        Me.cC.Name = "cC"
        Me.cC.Visible = False
        '
        'cD
        '
        Me.cD.HeaderText = "D"
        Me.cD.Name = "cD"
        Me.cD.Visible = False
        '
        'cE
        '
        Me.cE.HeaderText = "E"
        Me.cE.Name = "cE"
        Me.cE.Visible = False
        '
        'frmUserMaster
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.cmdClose
        Me.ClientSize = New System.Drawing.Size(750, 382)
        Me.Controls.Add(Me.grdUserMaster)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.cmdDelete)
        Me.Controls.Add(Me.cmdUpdate)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.cmdNew)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmUserMaster"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "User Master"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.errP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdUserMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub cmdNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNew.Click
        clearall()
        txtUserMasterCode.ReadOnly = False
        txtUserMasterCode.Focus()
    End Sub

    Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click
        If fnScreenValidate() = True Then
            If objClsUserMaster.fncSaveUserMaster(txtUserMasterCode.Text.Trim, txtUserMasterName.Text.Trim, cboUserGroupCode.Text.Trim, cboExpiredDate.Text.Trim.ToString, Val(txtDaysOfWarning.Text.Trim), sUserID) Then
                clearall()
                DispUserDetails("")
                MsgBox("Saved Successfully!", MsgBoxStyle.Information)
            Else
                MsgBox("Error in Save/Update !", MsgBoxStyle.Exclamation)
            End If
        End If
        Exit Sub
    End Sub
    Private Sub clearall()
        txtUserMasterCode.Text = ""
        txtUserMasterName.Text = ""
        cboExpiredDate.Text = ""
        txtDaysOfWarning.Text = ""
        cboUserGroupCode.SelectedIndex = 0
    End Sub

    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        Dim dr As DataRow()
        If grdUserMaster.RowCount > 0 Then
            If MsgBox("Do you want to Delete ?" & Trim(fnGetdgvText(grdUserMaster, grdUserMaster.CurrentRow.Index, USERCODE)), MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If objClsUserMaster.UserMasterDelete(txtUserMasterCode.Text.Trim) Then
                    clearall()
                    DispUserDetails("")
                Else
                    MsgBox("No such user!", MsgBoxStyle.Exclamation)
                End If
            End If
        Else
            MsgBox(" No User master to delete ! ", MsgBoxStyle.Exclamation)
        End If
        Exit Sub
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Public Function fnScreenValidate() As Boolean
        fnScreenValidate = False
        If Len(Trim(txtUserMasterCode.Text)) = 0 Then
            MsgBox("Please enter UserGroupCode !", MsgBoxStyle.Critical)
            txtUserMasterCode.Focus()
            Exit Function
        End If

        If Len(Trim(txtUserMasterName.Text)) = 0 Then
            MsgBox("Please enter UserGroupName !", MsgBoxStyle.Critical)
            txtUserMasterName.Focus()
            Exit Function
        End If
        If Len(Trim(cboUserGroupCode.Text)) = 0 Then
            MsgBox("Please select UserGroup !", MsgBoxStyle.Critical)
            cboUserGroupCode.Focus()
            Exit Function
        End If
        If cboExpiredDate.Text.Trim < Now() Then
            MsgBox("Expired date is less than current Date !", MsgBoxStyle.Critical)
            cboExpiredDate.Focus()
            Exit Function
        End If
        If Len(Trim(txtDaysOfWarning.Text)) > 0 Then
            If Not IsNumeric(txtDaysOfWarning.Text.Trim) Then
                MsgBox("Please enter valid data to Days of Warning !", MsgBoxStyle.Critical)
                txtDaysOfWarning.Focus()
                Exit Function
            End If
        End If
        fnScreenValidate = True
        Exit Function
    End Function

    Private Sub frmUserMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                cmdClose_Click(sender, e)
            Case Keys.Enter
                SendKeys.Send("{tab}")
            Case Keys.F6
                cmdNew_Click(sender, e)
            Case Keys.F7
                cmdUpdate_Click(sender, e)
            Case Keys.F8
                cmdDelete_Click(sender, e)

        End Select
    End Sub
    Public Sub DispUserDetails(ByVal sCode As String)
        Dim ds As DataSet
        Dim iCnt As Integer
        ds = objClsUserMaster.GetUserMasterDetail(sCode)
        grdUserMaster.RowCount = 0
        If ds.Tables(0).Rows.Count > 0 Then
            For iCnt = 0 To ds.Tables(0).Rows.Count - 1
                grdUserMaster.Rows.Add()
                fnSetdgvText(grdUserMaster, iCnt, 0, ds.Tables(0).Rows(iCnt)("UserCode") & "")
                fnSetdgvText(grdUserMaster, iCnt, 1, ds.Tables(0).Rows(iCnt)("UserName") & "")
                fnSetdgvText(grdUserMaster, iCnt, 2, ds.Tables(0).Rows(iCnt)("UserGroupCode") & "")
                fnSetdgvText(grdUserMaster, iCnt, 3, ds.Tables(0).Rows(iCnt)("ExpiryDate") & "")
                fnSetdgvText(grdUserMaster, iCnt, 4, ds.Tables(0).Rows(iCnt)("DaysOfWarning") & "")
            Next
        End If
        txtUserMasterCode.Text = Trim(fnGetdgvText(grdUserMaster, 0, 0))
        txtUserMasterName.Text = Trim(fnGetdgvText(grdUserMaster, 0, 1))
        cboUserGroupCode.Text = Trim(fnGetdgvText(grdUserMaster, 0, 2))
        cboExpiredDate.Text = Trim(fnGetdgvText(grdUserMaster, 0, 3))
        txtDaysOfWarning.Text = Trim(fnGetdgvText(grdUserMaster, 0, 4))
        txtUserMasterCode.ReadOnly = True
        txtUserMasterCode.Focus()
    End Sub
    Public Sub AddItemstoCombo()
        Dm.FillDatasToComboListWithFilter(cboUserGroupCode, "tblUserGroup", "UserGroupCode", "UserGroupName")
    End Sub
    Private Sub frmUserGroup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'subGridSetting(grdUserMaster)
        Dm.SetMasterDataSet(" Select * from tblusergroup", "tblUserGroup")
        AddItemstoCombo()
        DispUserDetails("")
        txtUserMasterCode.ReadOnly = True
    End Sub
    Private Sub txtUserMasterCode_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim ds As New DataSet
        ds = objClsUserMaster.GetUserMasterDetail(txtUserMasterCode.Text.Trim)
        If ds.Tables(0).Rows.Count > 0 Then
            txtUserMasterCode.Text = Trim(ds.Tables(0).Rows(0)("UserCode") & "")
            txtUserMasterName.Text = Trim(ds.Tables(0).Rows(0)("UserName") & "")
            cboUserGroupCode.Text = Trim(ds.Tables(0).Rows(0)("UserGroupCode") & "")
            cboExpiredDate.Text = Trim(ds.Tables(0).Rows(0)("ExpiryDate") & "")
            txtDaysOfWarning.Text = Trim(ds.Tables(0).Rows(0)("DaysOfWarning") & "")
            txtUserMasterCode.ReadOnly = True
        End If

    End Sub

    Private Sub grdUserMaster_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_ClickEvent)
        'If e.row > 0 Then
        '    txtUserMasterCode.Text = Trim(fnGetText(grdUserMaster, e.row, USERCODE))
        '    txtUserMasterName.Text = Trim(fnGetText(grdUserMaster, e.row, USERNAME))
        '    cboUserGroupCode.Text = Trim(fnGetText(grdUserMaster, e.row, USERGROUP))
        '    cboExpiredDate.Text = Trim(fnGetText(grdUserMaster, e.row, EXPDATE))
        '    txtDaysOfWarning.Text = Trim(fnGetText(grdUserMaster, e.row, NOOFDAYS))
        '    txtUserMasterCode.ReadOnly = True
        'End If
    End Sub

    'Private Sub txtUserMasterCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtUserMasterCode.Validating
    '    If Len(Trim(txtUserMasterCode.Text)) = 0 Then
    '        errP.SetError(txtUserMasterCode, "Compulsary!")
    '        txtUserMasterCode.Focus()
    '    Else
    '        errP.SetError(txtUserMasterCode, "")
    '    End If
    'End Sub

    'Private Sub txtUserMasterName_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtUserMasterName.Validating
    '    If Len(Trim(txtUserMasterName.Text)) = 0 Then
    '        errP.SetError(txtUserMasterName, "Compulsary!")
    '        txtUserMasterName.Focus()
    '    Else
    '        errP.SetError(txtUserMasterName, "")
    '    End If
    'End Sub

    'Private Sub cboUserGroupCode_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
    '    If Len(Trim(cboUserGroupCode.Text)) = 0 Then
    '        errP.SetError(cboUserGroupCode, "Compulsary!")
    '        cboUserGroupCode.Focus()
    '    Else
    '        errP.SetError(cboUserGroupCode, "")
    '    End If
    'End Sub


    Private Sub grdUserMaster_Click(sender As Object, e As System.EventArgs) Handles grdUserMaster.Click
        txtUserMasterCode.Text = Trim(fnGetdgvText(grdUserMaster, grdUserMaster.CurrentRow.Index, 0))
        txtUserMasterName.Text = Trim(fnGetdgvText(grdUserMaster, grdUserMaster.CurrentRow.Index, 1))
        cboUserGroupCode.Text = Trim(fnGetdgvText(grdUserMaster, grdUserMaster.CurrentRow.Index, 2))
        cboExpiredDate.Text = Trim(fnGetdgvText(grdUserMaster, grdUserMaster.CurrentRow.Index, 3))
        txtDaysOfWarning.Text = Trim(fnGetdgvText(grdUserMaster, grdUserMaster.CurrentRow.Index, 4))
        txtUserMasterCode.ReadOnly = True
    End Sub

    Private Sub grdUserMaster_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdUserMaster.CellContentClick

    End Sub
End Class

