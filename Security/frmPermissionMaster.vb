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
    Friend WithEvents grdPermissionMaster As AxFPSpread.AxvaSpread
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents grdUserGroup As AxFPSpread.AxvaSpread
    Friend WithEvents cmdClear As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmPermissionMaster))
        Me.grdPermissionMaster = New AxFPSpread.AxvaSpread
        Me.grdUserGroup = New AxFPSpread.AxvaSpread
        Me.cmdClose = New System.Windows.Forms.Button
        Me.cmdClear = New System.Windows.Forms.Button
        Me.cmdSave = New System.Windows.Forms.Button
        Me.pic = New System.Windows.Forms.PictureBox
        CType(Me.grdPermissionMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdUserGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdPermissionMaster
        '
        Me.grdPermissionMaster.Location = New System.Drawing.Point(315, 80)
        Me.grdPermissionMaster.Name = "grdPermissionMaster"
        Me.grdPermissionMaster.OcxState = CType(resources.GetObject("grdPermissionMaster.OcxState"), System.Windows.Forms.AxHost.State)
        Me.grdPermissionMaster.Size = New System.Drawing.Size(674, 278)
        Me.grdPermissionMaster.TabIndex = 7
        '
        'grdUserGroup
        '
        Me.grdUserGroup.Location = New System.Drawing.Point(20, 80)
        Me.grdUserGroup.Name = "grdUserGroup"
        Me.grdUserGroup.OcxState = CType(resources.GetObject("grdUserGroup.OcxState"), System.Windows.Forms.AxHost.State)
        Me.grdUserGroup.Size = New System.Drawing.Size(288, 286)
        Me.grdUserGroup.TabIndex = 108
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(906, 386)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(80, 24)
        Me.cmdClose.TabIndex = 2
        Me.cmdClose.Text = "Close (Esc)"
        '
        'cmdClear
        '
        Me.cmdClear.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.cmdClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClear.Image = CType(resources.GetObject("cmdClear.Image"), System.Drawing.Image)
        Me.cmdClear.Location = New System.Drawing.Point(816, 387)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.Size = New System.Drawing.Size(80, 24)
        Me.cmdClear.TabIndex = 1
        Me.cmdClear.Text = "Clear (F6)"
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.cmdSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.Location = New System.Drawing.Point(726, 387)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(80, 24)
        Me.cmdSave.TabIndex = 0
        Me.cmdSave.Text = "Save (F7)"
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(0, 0)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(210, 75)
        Me.pic.TabIndex = 112
        Me.pic.TabStop = False
        '
        'frmPermissionMaster
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.cmdClose
        Me.ClientSize = New System.Drawing.Size(1014, 426)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.grdUserGroup)
        Me.Controls.Add(Me.grdPermissionMaster)
        Me.Controls.Add(Me.cmdClear)
        Me.Controls.Add(Me.cmdSave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmPermissionMaster"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Permission Settings"
        CType(Me.grdPermissionMaster, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdUserGroup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmPermissionMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        subGridSetting(grdPermissionMaster)
        subGridSetting(grdUserGroup)
        FunctionControl(Me, "SEUSPM")
        DispUserGroups()
        DispUserGroupCodeDetails(fnGetText(grdUserGroup, 1, 1))
      
    End Sub
    Public Sub DispUserGroupCodeDetails(ByVal UGCode As String)
        Dim DrUG As DataRow()
        Dim DrU As DataRow()
        Dim i As Integer
        DrUG = Dm.RowReturnProjExecNonQryWithDataRow("Select * from tblUserPermission where UserGroupCode=" & fncFormatSQLText(UGCode))
        If DrUG.Length > 0 Then
            fnGridClearText(grdPermissionMaster)
            grdPermissionMaster.MaxRows = 1
            For i = 0 To DrUG.Length - 1
                fnSetText(grdPermissionMaster, i + 1, 3, DrUG(i)("FormCode") & "")
                fnSetText(grdPermissionMaster, i + 1, 2, Dm.ValReturnProjExecNonQry("Select FormGroupCode from tblForms where FormCode=" & fncFormatSQLText(DrUG(i)("FormCode") & "")))
                fnSetText(grdPermissionMaster, i + 1, 4, Dm.ValReturnProjExecNonQry("Select FormDescription from tblForms where FormCode=" & fncFormatSQLText(DrUG(i)("FormCode") & "")))
                If Trim(DrUG(i)("AllowAdd") & "") = "A" And Trim(DrUG(i)("AllowUpdate") & "") = "U" And Trim(DrUG(i)("AllowDelete") & "") = "D" And Trim(DrUG(i)("AllowRead") & "") = "R" Then
                    fnSetText(grdPermissionMaster, i + 1, 1, 1)
                End If
                If Trim(DrUG(i)("AllowAdd") & "") = "A" Then
                    fnSetText(grdPermissionMaster, i + 1, 5, 1)
                ElseIf Trim(DrUG(i)("AllowAdd") & "") = "N" Then
                    fnSetText(grdPermissionMaster, i + 1, 5, 0)
                End If
                If Trim(DrUG(i)("AllowUpdate") & "") = "U" Then
                    fnSetText(grdPermissionMaster, i + 1, 6, 1)
                ElseIf Trim(DrUG(i)("AllowUpdate") & "") = "N" Then
                    fnSetText(grdPermissionMaster, i + 1, 6, 0)
                End If
                If Trim(DrUG(i)("AllowDelete") & "") = "D" Then
                    fnSetText(grdPermissionMaster, i + 1, 7, 1)
                ElseIf Trim(DrUG(i)("AllowDelete") & "") = "N" Then
                    fnSetText(grdPermissionMaster, i + 1, 7, 0)
                End If
                If Trim(DrUG(i)("AllowRead") & "") = "R" Then
                    fnSetText(grdPermissionMaster, i + 1, 8, 1)
                ElseIf Trim(DrUG(i)("AllowRead") & "") = "N" Then
                    fnSetText(grdPermissionMaster, i + 1, 8, 0)
                End If
                grdPermissionMaster.MaxRows += 1
            Next
            DrU = Dm.RowReturnProjExecNonQryWithDataRow("Select * from tblforms where formCode not in(Select FormCode from tblUserPermission where UserGroupCode=" & fncFormatSQLText(UGCode) & ")")
            If DrU.Length > 0 Then
                For i = 0 To DrU.Length - 1
                    fnSetText(grdPermissionMaster, grdPermissionMaster.MaxRows, 2, DrU(i)("FormGroupCode") & "")
                    fnSetText(grdPermissionMaster, grdPermissionMaster.MaxRows, 3, DrU(i)("FormCode") & "")
                    fnSetText(grdPermissionMaster, grdPermissionMaster.MaxRows, 4, DrU(i)("FormDescription") & "")
                    grdPermissionMaster.MaxRows += 1
                Next
            End If
            grdPermissionMaster.MaxRows -= 1
        Else
            FillGrdFormDetails()
        End If
    End Sub
    Public Sub FillGrdFormDetails()
        Dim DrF As DataRow()
        Dim i As Integer
        DrF = Dm.RowReturnProjExecNonQryWithDataRow("Select * from tblForms")
        If DrF.Length > 0 Then
            fnGridClearText(grdPermissionMaster)
            grdPermissionMaster.MaxRows = 1
            For i = 0 To DrF.Length - 1
                fnSetText(grdPermissionMaster, i + 1, 2, DrF(i)("FormGroupCode") & "")
                fnSetText(grdPermissionMaster, i + 1, 3, DrF(i)("FormCode") & "")
                fnSetText(grdPermissionMaster, i + 1, 4, DrF(i)("FormDescription") & "")
                grdPermissionMaster.MaxRows += 1
            Next
            grdPermissionMaster.MaxRows -= 1
        End If
    End Sub
    Private Sub cmbUserGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' DispUserGroupCodeDetails()
    End Sub
    Public Sub DispUserGroups()
        Dim DrU As DataRow()
        Dim R As Integer
        DrU = Dm.RowReturnProjExecNonQryWithDataRow("Select * from tblUserGroup")
        fnGridClearText(grdUserGroup)
        grdUserGroup.MaxRows = 1
        If DrU.Length > 0 Then
            For R = 0 To DrU.Length - 1
                fnSetText(grdUserGroup, R + 1, 1, DrU(R)("UserGroupCode") & "")
                fnSetText(grdUserGroup, R + 1, 2, DrU(R)("UserGroupName") & "")
                grdUserGroup.MaxRows += 1
            Next
            grdUserGroup.MaxRows -= 1
            ' SearchCol = 2
        End If
    End Sub
    Private Sub grdUserGroup_KeyDownEvent(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_KeyDownEvent) Handles grdUserGroup.KeyDownEvent
        Dim R As Integer
        If e.keyCode = Keys.Down Then
            R = grdUserGroup.ActiveRow + 1
            If R > grdUserGroup.MaxRows Then
                Exit Sub
            End If
            DispUserGroupCodeDetails(fnGetText(grdUserGroup, R, 1))
        ElseIf e.keyCode = Keys.Up Then
            R = grdUserGroup.ActiveRow - 1
            If R < 1 Then
                Exit Sub
            End If
            DispUserGroupCodeDetails(fnGetText(grdUserGroup, R, 1))
        End If
    End Sub
    Private Sub grdUserGroup_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_ClickEvent) Handles grdUserGroup.ClickEvent
        If e.row > 0 Then
            DispUserGroupCodeDetails(fnGetText(grdUserGroup, e.row, 1))
        End If
    End Sub
    Private Sub grdPermissionMaster_ButtonClicked(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_ButtonClickedEvent) Handles grdPermissionMaster.ButtonClicked
        If e.row > 0 Then
            If e.col = 1 Then
                If Val(fnGetText(grdPermissionMaster, e.row, 1)) = 1 Then
                    fnSetText(grdPermissionMaster, e.row, 5, 1)
                    fnSetText(grdPermissionMaster, e.row, 6, 1)
                    fnSetText(grdPermissionMaster, e.row, 7, 1)
                    fnSetText(grdPermissionMaster, e.row, 8, 1)
                ElseIf Val(fnGetText(grdPermissionMaster, e.row, 1)) = 0 Then
                    fnSetText(grdPermissionMaster, e.row, 5, 0)
                    fnSetText(grdPermissionMaster, e.row, 6, 0)
                    fnSetText(grdPermissionMaster, e.row, 7, 0)
                    fnSetText(grdPermissionMaster, e.row, 8, 0)
                End If
            End If
        End If
    End Sub
    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub
  
    Public Function SaveDetails() As Boolean
        Dim clsuP As New clsPermissionMaster
        Dim UserGroupCode, Add, Save, Delete, Read As String
        Dim i As Integer
        Try
            SaveDetails = False
            UserGroupCode = fnGetText(grdUserGroup, grdUserGroup.ActiveRow, 1)
            clsuP.OpenBatchPostinConnection()
            For i = 1 To grdPermissionMaster.MaxRows
                If Val(fnGetText(grdPermissionMaster, i, 5)) = 1 Then
                    Add = "A"
                Else
                    Add = "N"
                End If
                If Val(fnGetText(grdPermissionMaster, i, 6)) = 1 Then
                    Save = "U"
                Else
                    Save = "N"
                End If
                If Val(fnGetText(grdPermissionMaster, i, 7)) = 1 Then
                    Delete = "D"
                Else
                    Delete = "N"
                End If
                If Val(fnGetText(grdPermissionMaster, i, 8)) = 1 Then
                    Read = "R"
                Else
                    Read = "N"
                End If
                If clsuP.SavePermissionDetails(UserGroupCode, fnGetText(grdPermissionMaster, i, 3), Add, Save, Delete, Read, sUserID) = False Then
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
            DispUserGroupCodeDetails(fnGetText(grdUserGroup, 1, 1))
        End If
    End Sub

    Private Sub cmdClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClear.Click
        Dim i As Integer
        For i = 1 To grdPermissionMaster.MaxRows
            fnSetText(grdPermissionMaster, i, 1, 0)
            fnSetText(grdPermissionMaster, i, 5, 0)
            fnSetText(grdPermissionMaster, i, 6, 0)
            fnSetText(grdPermissionMaster, i, 7, 0)
            fnSetText(grdPermissionMaster, i, 8, 0)
        Next
    End Sub
End Class
