Public Class frmSearch
    Inherits System.Windows.Forms.Form
    Public Grid_Click As String

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
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents cmdSearch As System.Windows.Forms.Button
    Friend WithEvents dtgSearchDetails As System.Windows.Forms.DataGrid
    Friend WithEvents optStartsWith As System.Windows.Forms.RadioButton
    Friend WithEvents optEndsWith As System.Windows.Forms.RadioButton
    Friend WithEvents optContains As System.Windows.Forms.RadioButton
    Friend WithEvents optExact As System.Windows.Forms.RadioButton
    Friend WithEvents cmdSearchOK As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmSearch))
        Me.lblSearch = New System.Windows.Forms.Label
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.cmdSearch = New System.Windows.Forms.Button
        Me.dtgSearchDetails = New System.Windows.Forms.DataGrid
        Me.optStartsWith = New System.Windows.Forms.RadioButton
        Me.optEndsWith = New System.Windows.Forms.RadioButton
        Me.optContains = New System.Windows.Forms.RadioButton
        Me.optExact = New System.Windows.Forms.RadioButton
        Me.cmdSearchOK = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        CType(Me.dtgSearchDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblSearch
        '
        Me.lblSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.Location = New System.Drawing.Point(22, 42)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(60, 20)
        Me.lblSearch.TabIndex = 0
        Me.lblSearch.Text = "Search"
        '
        'txtSearch
        '
        Me.txtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSearch.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(104, 42)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(140, 22)
        Me.txtSearch.TabIndex = 1
        Me.txtSearch.Text = ""
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.cmdSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
        Me.cmdSearch.Location = New System.Drawing.Point(264, 44)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.Size = New System.Drawing.Size(72, 20)
        Me.cmdSearch.TabIndex = 2
        Me.cmdSearch.Text = "Search"
        '
        'dtgSearchDetails
        '
        Me.dtgSearchDetails.CaptionVisible = False
        Me.dtgSearchDetails.DataMember = ""
        Me.dtgSearchDetails.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(177, Byte))
        Me.dtgSearchDetails.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dtgSearchDetails.Location = New System.Drawing.Point(18, 120)
        Me.dtgSearchDetails.Name = "dtgSearchDetails"
        Me.dtgSearchDetails.ParentRowsBackColor = System.Drawing.SystemColors.ControlLight
        Me.dtgSearchDetails.PreferredColumnWidth = 150
        Me.dtgSearchDetails.ReadOnly = True
        Me.dtgSearchDetails.Size = New System.Drawing.Size(520, 214)
        Me.dtgSearchDetails.TabIndex = 3
        Me.dtgSearchDetails.TabStop = False
        '
        'optStartsWith
        '
        Me.optStartsWith.Checked = True
        Me.optStartsWith.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optStartsWith.Location = New System.Drawing.Point(418, 14)
        Me.optStartsWith.Name = "optStartsWith"
        Me.optStartsWith.Size = New System.Drawing.Size(110, 22)
        Me.optStartsWith.TabIndex = 4
        Me.optStartsWith.TabStop = True
        Me.optStartsWith.Text = "F1 Starts With"
        '
        'optEndsWith
        '
        Me.optEndsWith.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optEndsWith.Location = New System.Drawing.Point(418, 43)
        Me.optEndsWith.Name = "optEndsWith"
        Me.optEndsWith.Size = New System.Drawing.Size(110, 16)
        Me.optEndsWith.TabIndex = 5
        Me.optEndsWith.Text = "F2 Ends With"
        '
        'optContains
        '
        Me.optContains.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optContains.Location = New System.Drawing.Point(418, 66)
        Me.optContains.Name = "optContains"
        Me.optContains.Size = New System.Drawing.Size(110, 16)
        Me.optContains.TabIndex = 6
        Me.optContains.Text = "Contains"
        '
        'optExact
        '
        Me.optExact.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optExact.Location = New System.Drawing.Point(418, 89)
        Me.optExact.Name = "optExact"
        Me.optExact.Size = New System.Drawing.Size(110, 18)
        Me.optExact.TabIndex = 7
        Me.optExact.Text = "Exact"
        '
        'cmdSearchOK
        '
        Me.cmdSearchOK.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.cmdSearchOK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchOK.Image = CType(resources.GetObject("cmdSearchOK.Image"), System.Drawing.Image)
        Me.cmdSearchOK.Location = New System.Drawing.Point(352, 344)
        Me.cmdSearchOK.Name = "cmdSearchOK"
        Me.cmdSearchOK.Size = New System.Drawing.Size(72, 20)
        Me.cmdSearchOK.TabIndex = 8
        Me.cmdSearchOK.Text = "OK"
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Image = CType(resources.GetObject("cmdCancel.Image"), System.Drawing.Image)
        Me.cmdCancel.Location = New System.Drawing.Point(434, 344)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(72, 20)
        Me.cmdCancel.TabIndex = 9
        Me.cmdCancel.Text = "Cancel"
        '
        'frmSearch
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(548, 376)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdSearchOK)
        Me.Controls.Add(Me.optExact)
        Me.Controls.Add(Me.optContains)
        Me.Controls.Add(Me.optEndsWith)
        Me.Controls.Add(Me.optStartsWith)
        Me.Controls.Add(Me.dtgSearchDetails)
        Me.Controls.Add(Me.cmdSearch)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.lblSearch)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Location = New System.Drawing.Point(100, 100)
        Me.MaximizeBox = False
        Me.Name = "frmSearch"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Search"
        CType(Me.dtgSearchDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        On Error GoTo errorsub
        Call fncLoadSearchDetail()
        Exit Sub
errorsub:
        MsgBox(Err.Description)
        Err.Clear()
    End Sub
    Function fncLoadSearchDetail() As Integer
        Dim nCount As Long
        Dim Dm As New DataManager
        Dim adoRS As ADODB.Recordset
        Dim sSelect As String
        Dim sSearchString As String
        Dim nError As Integer
        Dim ds As New DataSet

        On Error GoTo errorsub
        fncLoadSearchDetail = True
        dtgSearchDetails.Enabled = True
        If txtSearch.Text <> "" Then
            sSearchString = ""
            If optStartsWith.Checked = True Then 'F1 Starts with
                sSearchString = " like " & fncFormatSQLText(Trim(txtSearch.Text) & "%")
            ElseIf optEndsWith.Checked = True Then 'F2 Ends With
                sSearchString = " like " & fncFormatSQLText("%" & Trim(txtSearch.Text))
            ElseIf optContains.Checked = True Then 'F3 Contains
                sSearchString = " like " & fncFormatSQLText("%" & Trim(txtSearch.Text) & "%")
            ElseIf optExact.Checked = True Then 'F4 Exact
                sSearchString = " = " & fncFormatSQLText(Trim(txtSearch.Text))
            End If
            sSelect = "Select AccountCode,Description from tblChartOfAccounts where Description " & Trim(sSearchString)
        Else
            sSelect = "Select AccountCode,Description from tblChartOfAccounts"
        End If
        ' MsgBox(sSelect)
        ds = Dm.getDataSetWithSqlAdapter(sSelect, "getSearch", "getSearch")

        ' If ds.Tables("getSearch").Rows.Count > 1000 Then
        'MsgBox("More than 1000 records")
        'Else
            dtgSearchDetails.DataSource = ds.Tables("getSearch").DefaultView
        'End If
        Exit Function
errorsub:
        fncLoadSearchDetail = True
        MsgBox(Err.Description)
        Err.Clear()
    End Function
    Private Sub frmSearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrorSub
        Grid_Click = "False"
        Me.Top = 20
        Me.Left = 0
        Call SetFormImage(Me, "Image1")
        Call UniproFAS1GridSetting(dtgSearchDetails, False)
        Call UniproFAS1GridSetting(dtgSearchDetails, False)
        dtgSearchDetails.CurrentCell = New DataGridCell(0, 0)
        Exit Sub
ErrorSub:
        MsgBox(Err.Description)
        Err.Clear()
    End Sub
    Private Sub dtgSearchDetails_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dtgSearchDetails.MouseUp
        On Error GoTo ErrorSub
        'MsgBox(e.X)
        Grid_Click = "True"
        Dim iRet As Boolean
        Dim pt As Point = New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = dtgSearchDetails.HitTest(pt)
        If hti.Type = DataGrid.HitTestType.Cell Or hti.Type = DataGrid.HitTestType.RowHeader Then
            dtgSearchDetails.CurrentCell = New DataGridCell(hti.Row, hti.Column)

            If hti.Row >= 0 Then
                dtgSearchDetails.Select(hti.Row)
                sDescription = dtgSearchDetails(hti.Row, 1)
                objGeneralSelect.objReferenceNo = dtgSearchDetails(hti.Row, 0)
                objGeneralSelect.objSelectionMade = True
            End If
        End If
        Exit Sub
ErrorSub:
        MsgBox(Err.Description)
        Err.Clear()
    End Sub
    Private Sub cmdSearchOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearchOK.Click
        On Error GoTo ErrorSub
        If Grid_Click = "True" Then
        ElseIf Grid_Click = "False" Then
            sDescription = dtgSearchDetails(0, 1)
            objGeneralSelect.objReferenceNo = dtgSearchDetails(0, 0)
            objGeneralSelect.objSelectionMade = True
        End If
        Me.Dispose(True)
        Exit Sub
ErrorSub:
        MsgBox(Err.Description)
        Err.Clear()
        Me.Dispose(True)
    End Sub
    Private Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        '        On Error GoTo ErrorSub
        '        Select Case e.KeyCode
        '            Case Keys.Enter
        '                cmdSearch_Click(sender, e)
        '            Case Keys.Down
        '                MsgBox("entered")
        '        End Select
        '        Exit Sub
        'ErrorSub:
        '        MsgBox(Err.Description)
        '        Err.Clear()
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        On Error GoTo ErrorSub
        objGeneralSelect.objSelectionMade = False
        Me.Dispose(True)
        Exit Sub
ErrorSub:
        MsgBox(Err.Description)
        Err.Clear()
        Me.Dispose(True)
    End Sub
    Private Sub txtSearch_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyUp
        On Error GoTo ErrorSub
        If e.KeyCode = Keys.Down Then
            dtgSearchDetails.Select(1)
            dtgSearchDetails.Focus()
        End If
        cmdSearch_Click(sender, e)
        Exit Sub
ErrorSub:
        MsgBox(Err.Description)
        Err.Clear()
    End Sub
End Class

