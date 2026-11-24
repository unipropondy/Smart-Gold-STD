Public Class frmUserGroup
    Inherits System.Windows.Forms.Form
    Dim Dm As DataManager
    Dim OClsUserGroup As New clsUserGroup
    Const USERCODE = 1
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdDelete As System.Windows.Forms.Button
    Friend WithEvents cmdUpdate As System.Windows.Forms.Button
    Friend WithEvents cmdNew As System.Windows.Forms.Button
    Friend WithEvents grdUserGroup As System.Windows.Forms.DataGridView
    Friend WithEvents cGroupCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cGroupName As System.Windows.Forms.DataGridViewTextBoxColumn
    Const USERNAME = 2

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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtUserCode As System.Windows.Forms.TextBox
    Friend WithEvents errP As System.Windows.Forms.ErrorProvider
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUserGroup))
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtUserCode = New System.Windows.Forms.TextBox()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.errP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdUpdate = New System.Windows.Forms.Button()
        Me.cmdNew = New System.Windows.Forms.Button()
        Me.grdUserGroup = New System.Windows.Forms.DataGridView()
        Me.cGroupCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cGroupName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1.SuspendLayout()
        CType(Me.errP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdUserGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.Controls.Add(Me.txtUserCode)
        Me.GroupBox1.Controls.Add(Me.txtUserName)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(342, 110)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(294, 176)
        Me.GroupBox1.TabIndex = 19
        Me.GroupBox1.TabStop = False
        '
        'txtUserCode
        '
        Me.txtUserCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtUserCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUserCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtUserCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtUserCode.Location = New System.Drawing.Point(117, 37)
        Me.txtUserCode.MaxLength = 6
        Me.txtUserCode.Name = "txtUserCode"
        Me.txtUserCode.Size = New System.Drawing.Size(171, 21)
        Me.txtUserCode.TabIndex = 0
        '
        'txtUserName
        '
        Me.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUserName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtUserName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserName.Location = New System.Drawing.Point(117, 97)
        Me.txtUserName.MaxLength = 50
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(171, 21)
        Me.txtUserName.TabIndex = 1
        Me.txtUserName.Tag = "NE"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(5, 97)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = " Group Name :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(6, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = " Group Code :"
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
        Me.pic.TabIndex = 23
        Me.pic.TabStop = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.Location = New System.Drawing.Point(536, 315)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(100, 40)
        Me.cmdClose.TabIndex = 9
        Me.cmdClose.Text = "Close (Esc)"
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdDelete.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.Location = New System.Drawing.Point(423, 315)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(100, 40)
        Me.cmdDelete.TabIndex = 8
        Me.cmdDelete.Text = "Delete (F8)"
        Me.cmdDelete.UseVisualStyleBackColor = False
        '
        'cmdUpdate
        '
        Me.cmdUpdate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdUpdate.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUpdate.Location = New System.Drawing.Point(310, 315)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.Size = New System.Drawing.Size(100, 40)
        Me.cmdUpdate.TabIndex = 7
        Me.cmdUpdate.Text = "Update (F7)"
        Me.cmdUpdate.UseVisualStyleBackColor = False
        '
        'cmdNew
        '
        Me.cmdNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdNew.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdNew.Location = New System.Drawing.Point(197, 315)
        Me.cmdNew.Name = "cmdNew"
        Me.cmdNew.Size = New System.Drawing.Size(100, 40)
        Me.cmdNew.TabIndex = 6
        Me.cmdNew.Text = "New (F6)"
        Me.cmdNew.UseVisualStyleBackColor = False
        '
        'grdUserGroup
        '
        Me.grdUserGroup.AllowUserToAddRows = False
        Me.grdUserGroup.AllowUserToDeleteRows = False
        Me.grdUserGroup.AllowUserToResizeColumns = False
        Me.grdUserGroup.AllowUserToResizeRows = False
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdUserGroup.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdUserGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdUserGroup.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cGroupCode, Me.cGroupName})
        Me.grdUserGroup.Location = New System.Drawing.Point(0, 114)
        Me.grdUserGroup.Name = "grdUserGroup"
        Me.grdUserGroup.RowHeadersVisible = False
        Me.grdUserGroup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdUserGroup.Size = New System.Drawing.Size(336, 172)
        Me.grdUserGroup.TabIndex = 24
        '
        'cGroupCode
        '
        Me.cGroupCode.HeaderText = "Group Code"
        Me.cGroupCode.Name = "cGroupCode"
        Me.cGroupCode.Width = 170
        '
        'cGroupName
        '
        Me.cGroupName.HeaderText = "Group Name"
        Me.cGroupName.Name = "cGroupName"
        Me.cGroupName.Width = 150
        '
        'frmUserGroup
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(654, 377)
        Me.Controls.Add(Me.grdUserGroup)
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
        Me.Name = "frmUserGroup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "User Group"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.errP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdUserGroup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region




    Public Function fnScreenValidate() As Boolean
        fnScreenValidate = False
        If Len(Trim(txtUserCode.Text)) = 0 Then
            MsgBox("Please enter UserGroup Code !", MsgBoxStyle.Critical)
            txtUserCode.Focus()
            Exit Function
        End If

        If Len(Trim(txtUserName.Text)) = 0 Then
            MsgBox("Please enter UserGroup Name !", MsgBoxStyle.Critical)
            txtUserName.Focus()
            Exit Function
        End If
        fnScreenValidate = True
        Exit Function
    End Function
    Private Sub clearall()
        txtUserCode.Text = ""
        txtUserName.Text = ""
    End Sub

    Private Sub frmUserGroup_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
        ds = OClsUserGroup.GetUserGroupDetail(sCode)
        grdUserGroup.RowCount = 0
        If ds.Tables(0).Rows.Count > 0 Then
            For iCnt = 0 To ds.Tables(0).Rows.Count - 1
                grdUserGroup.Rows.Add()
                fnSetdgvText(grdUserGroup, iCnt, 0, ds.Tables(0).Rows(iCnt)("UserGroupCode") & "")
                fnSetdgvText(grdUserGroup, iCnt, 1, ds.Tables(0).Rows(iCnt)("UserGroupName") & "")
            Next
        End If
        ' grdUserGroup.MaxRows -= 1
        txtUserCode.Text = Trim(fnGetdgvText(grdUserGroup, 0, 0))
        txtUserName.Text = Trim(fnGetdgvText(grdUserGroup, 0, 1))
        txtUserCode.Focus()
    End Sub

    Private Sub frmUserGroup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'subGridSetting(grdUserGroup)
        DispUserDetails("")
        txtUserCode.ReadOnly = True
    End Sub
    Private Sub txtUserCode_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim ds As New DataSet

        ds = OClsUserGroup.GetUserGroupDetail(txtUserCode.Text.Trim)
        If ds.Tables(0).Rows.Count > 0 Then
            txtUserCode.Text = Trim(ds.Tables(0).Rows(0)("UserGroupCode") & "")
            txtUserName.Text = Trim(ds.Tables(0).Rows(0)("UserGroupName") & "")
            txtUserCode.ReadOnly = True
        End If

    End Sub

    'Private Sub grdUserGroup_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_ClickEvent)
    '    If e.row > 0 Then
    '        txtUserCode.Text = Trim(fnGetText(grdUserGroup, e.row, USERCODE))
    '        txtUserName.Text = Trim(fnGetText(grdUserGroup, e.row, USERNAME))
    '        txtUserCode.ReadOnly = True
    '    End If
    'End Sub


    Private Sub txtUserCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtUserCode.Validating
        If Len(Trim(txtUserCode.Text)) = 0 Then
            errP.SetError(txtUserCode, "Compulsary!")
            txtUserCode.Focus()
        Else
            errP.SetError(txtUserCode, "")
        End If
    End Sub

    Private Sub txtUserName_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtUserName.Validating
        If Len(Trim(txtUserName.Text)) = 0 Then
            errP.SetError(txtUserName, "Compulsary!")
            txtUserName.Focus()
        Else
            errP.SetError(txtUserName, "")
        End If
    End Sub


    Private Sub cmdClose_Click(sender As System.Object, e As System.EventArgs) Handles cmdClose.Click
        Me.Dispose()
    End Sub

    Private Sub cmdNew_Click(sender As System.Object, e As System.EventArgs) Handles cmdNew.Click
        clearall()
        txtUserCode.ReadOnly = False
        txtUserCode.Focus()
    End Sub

    Private Sub cmdUpdate_Click(sender As System.Object, e As System.EventArgs) Handles cmdUpdate.Click
        If fnScreenValidate() = True Then
            If OClsUserGroup.fncSaveUserGroup(txtUserCode.Text.Trim, txtUserName.Text.Trim, sUserID) Then
                clearall()
                DispUserDetails("")
                MsgBox("Saved Successfully!", MsgBoxStyle.Information)
            Else
                MsgBox("Error in Save/Update !", MsgBoxStyle.Exclamation)
            End If
        End If
        Exit Sub
    End Sub

    Private Sub cmdDelete_Click(sender As System.Object, e As System.EventArgs) Handles cmdDelete.Click
        Dim dr As DataRow()
        If grdUserGroup.RowCount > 0 Then
            If MsgBox("Do you want to Delete ?" & Trim(fnGetdgvText(grdUserGroup, grdUserGroup.CurrentRow.Index, 0)), MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If OClsUserGroup.UserGroupDelete(txtUserCode.Text.Trim) Then
                    clearall()
                    DispUserDetails("")
                Else
                    MsgBox("No such user!", MsgBoxStyle.Exclamation)
                End If
            End If
        Else
            MsgBox("No User Group  to delete ! ", MsgBoxStyle.Exclamation)
        End If
        Exit Sub
    End Sub

    Private Sub grdUserGroup_Advance(sender As System.Object, e As AxFPSpread._DSpreadEvents_AdvanceEvent)

    End Sub

    Private Sub grdUserGroup_Click(sender As Object, e As System.EventArgs) Handles grdUserGroup.Click
        txtUserCode.Text = fnGetdgvText(grdUserGroup, grdUserGroup.CurrentRow.Index, 0)
        txtUserName.Text = fnGetdgvText(grdUserGroup, grdUserGroup.CurrentRow.Index, 1)
    End Sub

    Private Sub grdUserGroup_MouseLeave(sender As Object, e As System.EventArgs) Handles grdUserGroup.MouseLeave
        'txtUserCode.Text = fnGetdgvText(grdUserGroup, grdUserGroup.CurrentRow.Index, 0)
        'txtUserName.Text = fnGetdgvText(grdUserGroup, grdUserGroup.CurrentRow.Index, 1)
    End Sub
End Class
