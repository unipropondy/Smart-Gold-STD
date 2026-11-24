Public Class frmPermissionMaster
    Inherits System.Windows.Forms.Form
    Dim Dm As New DataManager
    'Dim SearchCol As String

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
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdClear As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents grdUserGroup As System.Windows.Forms.DataGridView
    Friend WithEvents cGroupCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cGroupName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grdPermissionMaster As System.Windows.Forms.DataGridView
    Friend WithEvents cChkAll As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents cFormGroup As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cFormCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cFormName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cAdd As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents cSave As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents cDelete As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents cRead As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPermissionMaster))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdClear = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.grdUserGroup = New System.Windows.Forms.DataGridView()
        Me.cGroupCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cGroupName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grdPermissionMaster = New System.Windows.Forms.DataGridView()
        Me.cChkAll = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.cFormGroup = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cFormCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cFormName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cAdd = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.cSave = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.cDelete = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.cRead = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdUserGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdPermissionMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.Location = New System.Drawing.Point(737, 401)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(100, 40)
        Me.cmdClose.TabIndex = 2
        Me.cmdClose.Text = "Close (Esc)"
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'cmdClear
        '
        Me.cmdClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdClear.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClear.Location = New System.Drawing.Point(612, 401)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.Size = New System.Drawing.Size(100, 40)
        Me.cmdClear.TabIndex = 1
        Me.cmdClear.Text = "Clear (F6)"
        Me.cmdClear.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdSave.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.Location = New System.Drawing.Point(493, 401)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(100, 40)
        Me.cmdSave.TabIndex = 0
        Me.cmdSave.Text = "Save (F7)"
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(0, 0)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 100)
        Me.pic.TabIndex = 112
        Me.pic.TabStop = False
        '
        'grdUserGroup
        '
        Me.grdUserGroup.AllowUserToAddRows = False
        Me.grdUserGroup.AllowUserToDeleteRows = False
        Me.grdUserGroup.AllowUserToResizeColumns = False
        Me.grdUserGroup.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdUserGroup.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdUserGroup.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cGroupCode, Me.cGroupName})
        Me.grdUserGroup.Location = New System.Drawing.Point(0, 106)
        Me.grdUserGroup.Name = "grdUserGroup"
        Me.grdUserGroup.RowHeadersVisible = False
        Me.grdUserGroup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdUserGroup.Size = New System.Drawing.Size(303, 289)
        Me.grdUserGroup.TabIndex = 114
        '
        'cGroupCode
        '
        Me.cGroupCode.HeaderText = "Group Code"
        Me.cGroupCode.Name = "cGroupCode"
        Me.cGroupCode.Width = 150
        '
        'cGroupName
        '
        Me.cGroupName.HeaderText = "Group Name"
        Me.cGroupName.Name = "cGroupName"
        Me.cGroupName.Width = 150
        '
        'grdPermissionMaster
        '
        Me.grdPermissionMaster.AllowUserToAddRows = False
        Me.grdPermissionMaster.AllowUserToDeleteRows = False
        Me.grdPermissionMaster.AllowUserToResizeColumns = False
        Me.grdPermissionMaster.AllowUserToResizeRows = False
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdPermissionMaster.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdPermissionMaster.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cChkAll, Me.cFormGroup, Me.cFormCode, Me.cFormName, Me.cAdd, Me.cSave, Me.cDelete, Me.cRead})
        Me.grdPermissionMaster.Location = New System.Drawing.Point(309, 106)
        Me.grdPermissionMaster.Name = "grdPermissionMaster"
        Me.grdPermissionMaster.RowHeadersVisible = False
        Me.grdPermissionMaster.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.grdPermissionMaster.Size = New System.Drawing.Size(526, 289)
        Me.grdPermissionMaster.TabIndex = 115
        '
        'cChkAll
        '
        Me.cChkAll.HeaderText = "Chk All"
        Me.cChkAll.Name = "cChkAll"
        Me.cChkAll.Width = 60
        '
        'cFormGroup
        '
        Me.cFormGroup.HeaderText = "FormGroup"
        Me.cFormGroup.Name = "cFormGroup"
        '
        'cFormCode
        '
        Me.cFormCode.HeaderText = "Form Code"
        Me.cFormCode.Name = "cFormCode"
        Me.cFormCode.Width = 120
        '
        'cFormName
        '
        Me.cFormName.HeaderText = "Form Name"
        Me.cFormName.Name = "cFormName"
        Me.cFormName.Width = 240
        '
        'cAdd
        '
        Me.cAdd.HeaderText = "Add"
        Me.cAdd.Name = "cAdd"
        Me.cAdd.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cAdd.Visible = False
        Me.cAdd.Width = 70
        '
        'cSave
        '
        Me.cSave.HeaderText = "Save"
        Me.cSave.Name = "cSave"
        Me.cSave.Visible = False
        Me.cSave.Width = 70
        '
        'cDelete
        '
        Me.cDelete.HeaderText = "Delete"
        Me.cDelete.Name = "cDelete"
        Me.cDelete.Visible = False
        Me.cDelete.Width = 70
        '
        'cRead
        '
        Me.cRead.HeaderText = "Read"
        Me.cRead.Name = "cRead"
        Me.cRead.Visible = False
        Me.cRead.Width = 70
        '
        'frmPermissionMaster
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.cmdClose
        Me.ClientSize = New System.Drawing.Size(863, 458)
        Me.Controls.Add(Me.grdPermissionMaster)
        Me.Controls.Add(Me.grdUserGroup)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.cmdClear)
        Me.Controls.Add(Me.cmdSave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmPermissionMaster"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Permission Settings"
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdUserGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdPermissionMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmPermissionMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'subGridSetting(grdPermissionMaster)
        'subGridSetting(grdUserGroup)
        FunctionControl(Me, "SEUSPM")
        DispUserGroups()
        DispUserGroupCodeDetails(fnGetdgvText(grdUserGroup, 0, 0))

    End Sub
    Public Sub DispUserGroupCodeDetails(ByVal UGCode As String)
        Dim DrUG As DataRow()
        Dim DrU As DataRow()
        Dim i As Integer
        DrUG = Dm.RowReturnProjExecNonQryWithDataRow("Select * from tblUserPermission where UserGroupCode=" & fncFormatSQLText(UGCode))
        If DrUG.Length > 0 Then
            'fnGridClearText(grdPermissionMaster)
            grdPermissionMaster.RowCount = 0
            For i = 0 To DrUG.Length - 1
                grdPermissionMaster.Rows.Add()
                If Trim(DrUG(i)("AllowAdd") & "") = "A" And Trim(DrUG(i)("AllowUpdate") & "") = "U" And Trim(DrUG(i)("AllowDelete") & "") = "D" And Trim(DrUG(i)("AllowRead") & "") = "R" Then
                    'fnSetdgvText(grdPermissionMaster, i, cChkAll.Index, 1)
                    fnSetdgvText(grdPermissionMaster, i, 0, True)
                    fnSetdgvText(grdPermissionMaster, i, 4, True)
                    fnSetdgvText(grdPermissionMaster, i, 5, True)
                    fnSetdgvText(grdPermissionMaster, i, 6, True)
                    fnSetdgvText(grdPermissionMaster, i, 7, True)
                Else
                    fnSetdgvText(grdPermissionMaster, i, 0, False)
                    fnSetdgvText(grdPermissionMaster, i, 4, False)
                    fnSetdgvText(grdPermissionMaster, i, 5, False)
                    fnSetdgvText(grdPermissionMaster, i, 6, False)
                    fnSetdgvText(grdPermissionMaster, i, 7, False)
                End If
                fnSetdgvText(grdPermissionMaster, i, 2, DrUG(i)("FormCode") & "")
                fnSetdgvText(grdPermissionMaster, i, 1, Dm.ValReturnProjExecNonQry("Select FormGroupCode from tblForms where FormCode=" & fncFormatSQLText(DrUG(i)("FormCode") & "")))
                fnSetdgvText(grdPermissionMaster, i, 3, Dm.ValReturnProjExecNonQry("Select FormDescription from tblForms where FormCode=" & fncFormatSQLText(DrUG(i)("FormCode") & "")))
                
                If Trim(DrUG(i)("AllowAdd") & "") = "A" Then
                    fnSetdgvText(grdPermissionMaster, i, 4, True)
                ElseIf Trim(DrUG(i)("AllowAdd") & "") = "N" Then
                    fnSetdgvText(grdPermissionMaster, i, 4, False)
                End If
                If Trim(DrUG(i)("AllowUpdate") & "") = "U" Then
                    fnSetdgvText(grdPermissionMaster, i, 5, True)
                ElseIf Trim(DrUG(i)("AllowUpdate") & "") = "N" Then
                    fnSetdgvText(grdPermissionMaster, i, 5, False)
                End If
                If Trim(DrUG(i)("AllowDelete") & "") = "D" Then
                    fnSetdgvText(grdPermissionMaster, i, 6, True)
                ElseIf Trim(DrUG(i)("AllowDelete") & "") = "N" Then
                    fnSetdgvText(grdPermissionMaster, i, 6, False)
                End If
                If Trim(DrUG(i)("AllowRead") & "") = "R" Then
                    fnSetdgvText(grdPermissionMaster, i, 7, True)
                ElseIf Trim(DrUG(i)("AllowRead") & "") = "N" Then
                    fnSetdgvText(grdPermissionMaster, i, 7, False)
                End If
                ';grdPermissionMaster.MaxRows += 1
            Next
            DrU = Dm.RowReturnProjExecNonQryWithDataRow("Select * from tblforms where formCode not in(Select FormCode from tblUserPermission where UserGroupCode=" & fncFormatSQLText(UGCode) & ")")
            If DrU.Length > 0 Then
                For i = 0 To DrU.Length - 1
                    grdPermissionMaster.Rows.Add()
                    fnSetdgvText(grdPermissionMaster, grdPermissionMaster.RowCount - 1, 0, False)
                    fnSetdgvText(grdPermissionMaster, grdPermissionMaster.RowCount - 1, 1, DrU(i)("FormGroupCode") & "")
                    fnSetdgvText(grdPermissionMaster, grdPermissionMaster.RowCount - 1, 2, DrU(i)("FormCode") & "")
                    fnSetdgvText(grdPermissionMaster, grdPermissionMaster.RowCount - 1, 3, DrU(i)("FormDescription") & "")
                    'grdPermissionMaster.MaxRows += 1
                Next
            End If
            'grdPermissionMaster.MaxRows -= 1
        Else
            FillGrdFormDetails()
        End If
    End Sub
    Public Sub FillGrdFormDetails()
        Dim DrF As DataRow()
        Dim i As Integer
        DrF = Dm.RowReturnProjExecNonQryWithDataRow("Select * from tblForms")
        If DrF.Length > 0 Then
            ' fnGridClearText(grdPermissionMaster)
            grdPermissionMaster.RowCount = 0
            For i = 0 To DrF.Length - 1
                grdPermissionMaster.Rows.Add()
                fnSetdgvText(grdPermissionMaster, i, 1, DrF(i)("FormGroupCode") & "")
                fnSetdgvText(grdPermissionMaster, i, 2, DrF(i)("FormCode") & "")
                fnSetdgvText(grdPermissionMaster, i, 3, DrF(i)("FormDescription") & "")

            Next

        End If
    End Sub
    Private Sub cmbUserGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' DispUserGroupCodeDetails()
    End Sub
    Public Sub DispUserGroups()
        'Dim DrU As DataRow()
        'Dim R As Integer
        'DrU = Dm.RowReturnProjExecNonQryWithDataRow("Select * from tblUserGroup")
        ''fnGridClearText(grdUserGroup)
        'grdUserGroup.RowCount = 0
        'If DrU.Length > 0 Then
        '    For R = 0 To DrU.Length - 1
        '        grdUserGroup.Rows.Add()
        '        fnSetdgvText(grdUserGroup, R, 0, DrU(R)("UserGroupCode") & "")
        '        fnSetdgvText(grdUserGroup, R, 1, DrU(R)("UserGroupName") & "")
        '        'grdUserGroup.MaxRows += 1
        '    Next
        '    'grdUserGroup.MaxRows -= 1
        '    ' SearchCol = 2
        'End If

        Dim DrU As DataRow()
        Dim R As Integer
        DrU = Dm.RowReturnProjExecNonQryWithDataRow("Select * from tbluserMaster")
        'fnGridClearText(grdUserGroup)
        grdUserGroup.RowCount = 0
        If DrU.Length > 0 Then
            For R = 0 To DrU.Length - 1
                grdUserGroup.Rows.Add()
                fnSetdgvText(grdUserGroup, R, 0, DrU(R)("UserCode") & "")
                fnSetdgvText(grdUserGroup, R, 1, DrU(R)("UserName") & "")
                'grdUserGroup.MaxRows += 1
            Next
            'grdUserGroup.MaxRows -= 1
            ' SearchCol = 2
        End If
    End Sub
    'Private Sub grdUserGroup_KeyDownEvent(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_KeyDownEvent)
    '    'Dim R As Integer
    '    'If e.keyCode = Keys.Down Then
    '    '    R = grdUserGroup.ActiveRow + 1
    '    '    If R > grdUserGroup.MaxRows Then
    '    '        Exit Sub
    '    '    End If
    '    '    DispUserGroupCodeDetails(fnGetText(grdUserGroup, R, 1))
    '    'ElseIf e.keyCode = Keys.Up Then
    '    '    R = grdUserGroup.ActiveRow - 1
    '    '    If R < 1 Then
    '    '        Exit Sub
    '    '    End If
    '    '    DispUserGroupCodeDetails(fnGetText(grdUserGroup, R, 1))
    '    'End If
    'End Sub
    'Private Sub grdUserGroup_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_ClickEvent)
    '    'If e.row > 0 Then
    '    '    DispUserGroupCodeDetails(fnGetText(grdUserGroup, e.row, 1))
    '    'End If
    'End Sub
    'Private Sub grdPermissionMaster_ButtonClicked(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_ButtonClickedEvent)
    '    If e.row > 0 Then
    '        If e.col = 1 Then
    '            If Val(fnGetText(grdPermissionMaster, e.row, 1)) = 1 Then
    '                fnSetText(grdPermissionMaster, e.row, 5, 1)
    '                fnSetText(grdPermissionMaster, e.row, 6, 1)
    '                fnSetText(grdPermissionMaster, e.row, 7, 1)
    '                fnSetText(grdPermissionMaster, e.row, 8, 1)
    '            ElseIf Val(fnGetText(grdPermissionMaster, e.row, 1)) = 0 Then
    '                fnSetText(grdPermissionMaster, e.row, 5, 0)
    '                fnSetText(grdPermissionMaster, e.row, 6, 0)
    '                fnSetText(grdPermissionMaster, e.row, 7, 0)
    '                fnSetText(grdPermissionMaster, e.row, 8, 0)
    '            End If
    '        End If
    '    End If
    'End Sub
    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub
  
    Public Function SaveDetails() As Boolean
        Dim clsuP As New clsPermissionMaster
        Dim UserGroupCode, Add, Save, Delete, Read As String
        Dim i As Integer
        Try
            SaveDetails = False
            UserGroupCode = fnGetdgvText(grdUserGroup, grdUserGroup.CurrentRow.Index, 0)
            clsuP.OpenBatchPostinConnection()
            For i = 0 To grdPermissionMaster.RowCount - 1
                If (fnGetdgvText(grdPermissionMaster, i, cChkAll.Index)) = True Then

                    Add = "A"
                    Save = "U"
                    Delete = "D"
                    Read = "R"
                Else
                    Add = "N"
                    Save = "N"
                    Delete = "N"
                    Read = "N"
                End If
                'If (fnGetdgvText(grdPermissionMaster, i, cSave.Index)) = True Then
                '    Save = "U"
                'Else
                '    Save = "N"
                'End If
                'If (fnGetdgvText(grdPermissionMaster, i, cDelete.Index)) = True Then
                '    Delete = "D"
                'Else
                '    Delete = "N"
                'End If
                'If (fnGetdgvText(grdPermissionMaster, i, cRead.Index)) = True Then
                '    Read = "R"
                'Else
                '    Read = "N"
                'End If
                If clsuP.SavePermissionDetails(UserGroupCode, fnGetdgvText(grdPermissionMaster, i, 2), Add, Save, Delete, Read, sUserID) = False Then
                    clsuP.gpRollBackBatchConnection()
                    SaveDetails = False
                    Exit Function
                End If

            Next
            clsuP.gpCommitTransaction()
            clsuP.CloseBatchConnection()
            SaveDetails = True
        Catch ex As Exception
            MsgBox("Error in Saving permission Details   " & Err.Description)
            clsuP.gpRollBackBatchConnection()
            clsuP.CloseBatchConnection()
            SaveDetails = False
            Exit Function
        Finally
            Err.Clear()
        End Try
    End Function
  
    Private Sub frmPermissionMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                SendKeys.Send("{tab}")
            Case Keys.Escape
                cmdClose_Click(sender, e)
            Case Keys.F6
                cmdClear_Click(sender, e)
            Case Keys.F7
                cmdSave_Click(sender, e)
        End Select
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If SaveDetails() = True Then
            MsgBox("Details Successfully saved", MsgBoxStyle.Information)
            DispUserGroups()
            DispUserGroupCodeDetails(fnGetdgvText(grdUserGroup, 0, 0))
        End If
    End Sub

    Private Sub cmdClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClear.Click
        Dim i As Integer
        For i = 0 To grdPermissionMaster.RowCount - 1
            fnSetdgvText(grdPermissionMaster, i, 0, 0)
            fnSetdgvText(grdPermissionMaster, i, 4, 0)
            fnSetdgvText(grdPermissionMaster, i, 5, 0)
            fnSetdgvText(grdPermissionMaster, i, 6, 0)
            fnSetdgvText(grdPermissionMaster, i, 7, 0)
        Next
    End Sub
    Private Sub grdPermissionMaster_CellValueChanged(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPermissionMaster.CellValueChanged
        Try
            If grdPermissionMaster.RowCount = 0 Then Exit Sub
            'If e.ColumnIndex = cChkAll.Index Then
            '    If fnGetdgvText(grdPermissionMaster, grdPermissionMaster.CurrentRow.Index, cChkAll.Index) = True Then
            '        fnSetdgvText(grdPermissionMaster, grdPermissionMaster.CurrentRow.Index, cAdd.Index, True)
            '        fnSetdgvText(grdPermissionMaster, grdPermissionMaster.CurrentRow.Index, cSave.Index, True)
            '        fnSetdgvText(grdPermissionMaster, grdPermissionMaster.CurrentRow.Index, cDelete.Index, True)
            '        fnSetdgvText(grdPermissionMaster, grdPermissionMaster.CurrentRow.Index, cRead.Index, True)
            '    Else
            '        fnSetdgvText(grdPermissionMaster, grdPermissionMaster.CurrentRow.Index, cAdd.Index, False)
            '        fnSetdgvText(grdPermissionMaster, grdPermissionMaster.CurrentRow.Index, cSave.Index, False)
            '        fnSetdgvText(grdPermissionMaster, grdPermissionMaster.CurrentRow.Index, cDelete.Index, False)
            '        fnSetdgvText(grdPermissionMaster, grdPermissionMaster.CurrentRow.Index, cRead.Index, False)
            '    End If
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub grdUserGroup_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdUserGroup.CellClick
        DispUserGroupCodeDetails(fnGetdgvText(grdUserGroup, e.RowIndex, 0))
    End Sub
End Class
