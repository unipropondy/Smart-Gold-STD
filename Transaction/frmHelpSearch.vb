Public Class frmHelpSearch
    Inherits System.Windows.Forms.Form
    Dim DV As New DataView
    Dim sSearchTableName As String
    Dim lPeriod As Double
    Dim oSearchDS As New DataSet
    Dim sWhereCol As String
    Public Var1 As String
    Public Var2 As String
    Public Var3 As String
    Public Var4 As String
    Dim X1 As Decimal
    Dim Y1 As Decimal
    Dim TblName As String
    Dim SearchBy As String
    Dim NoofCol As Integer
    Dim SortCol As String, MyArray() As String
    Dim sConnFocus As String

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub
    Public Sub New(ByVal P1 As Decimal, ByVal P2 As Decimal, ByVal TName As String, ByVal ConnFocusOn As String, ByVal NCol As Integer, ByVal SCol As String, ByVal WhereCol As String, ByVal ParamArray MyArr() As String)
        MyBase.New()
        InitializeComponent()
        X1 = 580
        Y1 = 55
        NoofCol = NCol
        TblName = TName
        sSearchTableName = TName
        lPeriod = nPeriods
        sConnFocus = ConnFocusOn
        sWhereCol = WhereCol
        SortCol = SCol
        ReDim Preserve MyArray(MyArr.Length - 1)
        Array.Copy(MyArr, MyArray, MyArr.Length)
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSearchBy As System.Windows.Forms.TextBox
    Friend WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents cmdSelect As System.Windows.Forms.Button
    Friend WithEvents dgHelpSearch As System.Windows.Forms.DataGrid
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents lblNoofRecords As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboSearchFields As System.Windows.Forms.ComboBox
    Friend WithEvents lblDisp As System.Windows.Forms.Label
    Friend WithEvents optStart As System.Windows.Forms.RadioButton
    Friend WithEvents optEnd As System.Windows.Forms.RadioButton
    Friend WithEvents optAny As System.Windows.Forms.RadioButton
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmHelpSearch))
        Me.dgHelpSearch = New System.Windows.Forms.DataGrid
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtSearchBy = New System.Windows.Forms.TextBox
        Me.cmdExit = New System.Windows.Forms.Button
        Me.cmdSelect = New System.Windows.Forms.Button
        Me.lblSearch = New System.Windows.Forms.Label
        Me.lblNoofRecords = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.cboSearchFields = New System.Windows.Forms.ComboBox
        Me.lblDisp = New System.Windows.Forms.Label
        Me.optStart = New System.Windows.Forms.RadioButton
        Me.optEnd = New System.Windows.Forms.RadioButton
        Me.optAny = New System.Windows.Forms.RadioButton
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        CType(Me.dgHelpSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgHelpSearch
        '
        Me.dgHelpSearch.BackgroundColor = System.Drawing.Color.White
        Me.dgHelpSearch.CaptionBackColor = System.Drawing.Color.Yellow
        Me.dgHelpSearch.CaptionForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.dgHelpSearch.DataMember = ""
        Me.dgHelpSearch.GridLineColor = System.Drawing.Color.Black
        Me.dgHelpSearch.HeaderBackColor = System.Drawing.Color.Yellow
        Me.dgHelpSearch.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgHelpSearch.Location = New System.Drawing.Point(2, 95)
        Me.dgHelpSearch.Name = "dgHelpSearch"
        Me.dgHelpSearch.ParentRowsBackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.dgHelpSearch.ReadOnly = True
        Me.dgHelpSearch.Size = New System.Drawing.Size(429, 550)
        Me.dgHelpSearch.TabIndex = 13
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(10, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 17)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Search By..."
        '
        'txtSearchBy
        '
        Me.txtSearchBy.Location = New System.Drawing.Point(175, 75)
        Me.txtSearchBy.Name = "txtSearchBy"
        Me.txtSearchBy.Size = New System.Drawing.Size(259, 20)
        Me.txtSearchBy.TabIndex = 15
        Me.txtSearchBy.Text = ""
        '
        'cmdExit
        '
        Me.cmdExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdExit.BackColor = System.Drawing.Color.AliceBlue
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.cmdExit.ForeColor = System.Drawing.Color.RoyalBlue
        Me.cmdExit.Image = CType(resources.GetObject("cmdExit.Image"), System.Drawing.Image)
        Me.cmdExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdExit.Location = New System.Drawing.Point(311, 651)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(119, 21)
        Me.cmdExit.TabIndex = 17
        Me.cmdExit.Text = "E&xit  ( Esc )"
        Me.cmdExit.Visible = False
        '
        'cmdSelect
        '
        Me.cmdSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSelect.BackColor = System.Drawing.Color.AliceBlue
        Me.cmdSelect.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdSelect.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.cmdSelect.ForeColor = System.Drawing.Color.RoyalBlue
        Me.cmdSelect.Image = CType(resources.GetObject("cmdSelect.Image"), System.Drawing.Image)
        Me.cmdSelect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSelect.Location = New System.Drawing.Point(183, 651)
        Me.cmdSelect.Name = "cmdSelect"
        Me.cmdSelect.Size = New System.Drawing.Size(119, 21)
        Me.cmdSelect.TabIndex = 16
        Me.cmdSelect.Text = "Select ( F2 ) "
        Me.cmdSelect.Visible = False
        '
        'lblSearch
        '
        Me.lblSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.ForeColor = System.Drawing.Color.Blue
        Me.lblSearch.Location = New System.Drawing.Point(5, 75)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(166, 20)
        Me.lblSearch.TabIndex = 18
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNoofRecords
        '
        Me.lblNoofRecords.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNoofRecords.ForeColor = System.Drawing.Color.Red
        Me.lblNoofRecords.Location = New System.Drawing.Point(260, 5)
        Me.lblNoofRecords.Name = "lblNoofRecords"
        Me.lblNoofRecords.Size = New System.Drawing.Size(74, 18)
        Me.lblNoofRecords.TabIndex = 459
        Me.lblNoofRecords.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.White
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Blue
        Me.Label5.Location = New System.Drawing.Point(340, 5)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(94, 18)
        Me.Label5.TabIndex = 458
        Me.Label5.Text = "Records Found"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(180, 25)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 18)
        Me.Label3.TabIndex = 461
        Me.Label3.Text = "Search Field"
        '
        'cboSearchFields
        '
        Me.cboSearchFields.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSearchFields.Location = New System.Drawing.Point(260, 25)
        Me.cboSearchFields.Name = "cboSearchFields"
        Me.cboSearchFields.Size = New System.Drawing.Size(170, 21)
        Me.cboSearchFields.TabIndex = 460
        '
        'lblDisp
        '
        Me.lblDisp.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblDisp.Location = New System.Drawing.Point(185, 46)
        Me.lblDisp.Name = "lblDisp"
        Me.lblDisp.Size = New System.Drawing.Size(76, 17)
        Me.lblDisp.TabIndex = 466
        '
        'optStart
        '
        Me.optStart.Location = New System.Drawing.Point(265, 50)
        Me.optStart.Name = "optStart"
        Me.optStart.Size = New System.Drawing.Size(51, 13)
        Me.optStart.TabIndex = 463
        Me.optStart.Text = "Start"
        Me.optStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'optEnd
        '
        Me.optEnd.Location = New System.Drawing.Point(320, 50)
        Me.optEnd.Name = "optEnd"
        Me.optEnd.Size = New System.Drawing.Size(45, 13)
        Me.optEnd.TabIndex = 464
        Me.optEnd.Text = "End"
        Me.optEnd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'optAny
        '
        Me.optAny.Location = New System.Drawing.Point(370, 50)
        Me.optAny.Name = "optAny"
        Me.optAny.Size = New System.Drawing.Size(48, 16)
        Me.optAny.TabIndex = 465
        Me.optAny.Text = "Any"
        Me.optAny.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ComboBox1
        '
        Me.ComboBox1.ItemHeight = 13
        Me.ComboBox1.Location = New System.Drawing.Point(5, 40)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(80, 21)
        Me.ComboBox1.TabIndex = 467
        Me.ComboBox1.Text = "ComboBox1"
        Me.ComboBox1.Visible = False
        '
        'frmHelpSearch
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(432, 676)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.lblDisp)
        Me.Controls.Add(Me.optStart)
        Me.Controls.Add(Me.optEnd)
        Me.Controls.Add(Me.optAny)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cboSearchFields)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdSelect)
        Me.Controls.Add(Me.dgHelpSearch)
        Me.Controls.Add(Me.txtSearchBy)
        Me.Controls.Add(Me.lblSearch)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblNoofRecords)
        Me.Controls.Add(Me.Label5)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmHelpSearch"
        Me.Text = "Help Search"
        CType(Me.dgHelpSearch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmHelpSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Location = New Point(X1, Y1)
        Dim TabSty As New DataGridTableStyle
        AssignDatSet()
        dgHelpSearch.TableStyles.Add(fnSetGridStyle())

        DV.Table = oSearchDS.Tables("Search")
        lblNoofRecords.Text = DV.Count
        dgHelpSearch.DataSource = DV
        SearchBy = MyArray(3)
        cboSearchFields.Text = MyArray(3)
        ComboBox1.SelectedIndex = cboSearchFields.SelectedIndex
        lblSearch.Text = MyArray(4)
    End Sub
    Private Sub txtSearchBy_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchBy.TextChanged
        On Error GoTo ErrTrap
        'DV = GetRecordsFromMasterDataSet("" & TblName & "", "" & SortCol & "")
        DV.Table = oSearchDS.Tables("Search")

        If Len(Trim(SearchBy)) = 0 Then
            SearchBy = MyArray(0)
            lblSearch.Text = MyArray(1)
            cboSearchFields.Text = MyArray(1)
            ComboBox1.SelectedIndex = cboSearchFields.SelectedIndex
        End If
        If optAny.Visible = True And optEnd.Visible = True And optStart.Visible = True Then
            If optAny.Checked = True Then
                DV.RowFilter = SearchBy & " Like '%" & txtSearchBy.Text & "%'"
            ElseIf optStart.Checked = True Then
                DV.RowFilter = SearchBy & " Like '" & txtSearchBy.Text & "%'"
            ElseIf optEnd.Checked = True Then
                DV.RowFilter = SearchBy & " Like '%" & txtSearchBy.Text & "'"
            End If
        Else
            DV.RowFilter = SearchBy & " Like '" & txtSearchBy.Text & "%'"
        End If
        lblNoofRecords.Text = DV.Count
        dgHelpSearch.DataSource = DV
        Exit Sub
ErrTrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Sub
    Private Sub frmHelpSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Close()
        End Select
    End Sub
    Private Sub cmdSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelect.Click
        Var1 = dgHelpSearch.Item(dgHelpSearch.CurrentRowIndex, 0)
        Var2 = dgHelpSearch.Item(dgHelpSearch.CurrentRowIndex, 1)
        Var3 = dgHelpSearch.Item(dgHelpSearch.CurrentRowIndex, 3)
        Var4 = dgHelpSearch.Item(dgHelpSearch.CurrentRowIndex, 2)
    End Sub
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub
    Public Function fnSetGridStyle() As DataGridTableStyle
        Dim TabStlye As New DataGridTableStyle
        Dim I As Int32
        Dim J As Int32
        If NoofCol * 3 <> MyArray.Length Then
            MsgBox("Please Send Currect Parameters", MsgBoxStyle.Critical)
            Exit Function
        End If
        TabStlye.MappingName = "Search"
        Dim Column As DataGridColumnStyle
        J = 0
        For I = 0 To NoofCol - 1
            Column = New DataGridTextBoxColumn
            Column.MappingName = MyArray(J)
            J += 1
            Column.HeaderText = MyArray(J)
            J += 1
            Column.Width = Val(MyArray(J))
            TabStlye.GridColumnStyles.Add(Column)
            J += 1
        Next
        Return TabStlye
    End Function
    Private Sub dgHelpSearch_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgHelpSearch.CurrentCellChanged
        Var1 = dgHelpSearch.Item(dgHelpSearch.CurrentRowIndex, 0)
        Var2 = dgHelpSearch.Item(dgHelpSearch.CurrentRowIndex, 1)
        Var3 = IIf(IsDBNull(dgHelpSearch.Item(dgHelpSearch.CurrentRowIndex, 2)) = True, "", dgHelpSearch.Item(dgHelpSearch.CurrentRowIndex, 2))
        Var4 = IIf(IsDBNull(dgHelpSearch.Item(dgHelpSearch.CurrentRowIndex, 3)) = True, "", dgHelpSearch.Item(dgHelpSearch.CurrentRowIndex, 3))
    End Sub
    Private Sub dgHelpSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgHelpSearch.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Close()
        End Select
    End Sub
    Private Sub dgHelpSearch_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgHelpSearch.DoubleClick
        dgHelpSearch_CurrentCellChanged(sender, e)
        cmdExit_Click(sender, e)
    End Sub
    Private Sub dgHelpSearch_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgHelpSearch.MouseUp
        On Error GoTo err
        Dim Pt = New Point(e.X, e.Y)
        Dim HTI As DataGrid.HitTestInfo = dgHelpSearch.HitTest(Pt)
        If HTI.Type = DataGrid.HitTestType.Cell Then
            dgHelpSearch.CurrentCell = New DataGridCell(HTI.Row, HTI.Column)
            If HTI.Row >= 0 Then
                dgHelpSearch.Select(HTI.Row)
                Var1 = dgHelpSearch(HTI.Row, 0)
                Var2 = dgHelpSearch(HTI.Row, 1)
                Var3 = dgHelpSearch(HTI.Row, 3)
                Var4 = dgHelpSearch(HTI.Row, 2)
            End If
        ElseIf HTI.Type = DataGrid.HitTestType.ColumnHeader Then
            SearchBy = MyArray(HTI.Column * 3)
            cboSearchFields.Text = MyArray(HTI.Column * 3)
            ComboBox1.SelectedIndex = cboSearchFields.SelectedIndex
            lblSearch.Text = MyArray((HTI.Column * 3) + 1)
        End If
err:
    End Sub
    Private Sub txtSearchBy_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearchBy.KeyDown
        If e.KeyCode = Keys.Enter Then
            Var1 = dgHelpSearch.Item(0, 0)
            Var2 = dgHelpSearch.Item(0, 1)
            Var3 = dgHelpSearch.Item(0, 3)
            Var4 = dgHelpSearch.Item(0, 2)
            cmdExit_Click(sender, e)
        End If
    End Sub

    Private Sub frmHelpSearch_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        txtSearchBy.Focus()
    End Sub
    Private Function AssignDatSet() As Boolean
        Dim Cls As New DataManager
        Dim I As Int32
        Dim J As Int32
        On Error GoTo ErrTrap
        AssignDatSet = False
        If sConnFocus = "L" Then
            If Len(Trim(sWhereCol)) = 0 Then
                If UCase(sSearchTableName) = UCase("tblChartofAccounts") Then
                    oSearchDS = Cls.SetTabelsToDataSet("Select * from " & sSearchTableName & " where Active='Y'", "Search")
                Else
                    oSearchDS = Cls.SetTabelsToDataSet("Select * from " & sSearchTableName, "Search")
                End If
            Else
                If UCase(sSearchTableName) = UCase("tblChartofAccounts") Then
                    oSearchDS = Cls.SetTabelsToDataSet("Select * from " & sSearchTableName & " where " & sWhereCol & " and Active='Y'", "Search")
                Else
                    oSearchDS = Cls.SetTabelsToDataSet("Select * from " & sSearchTableName & " where " & sWhereCol, "Search")
                End If
            End If
        Else
            If Len(Trim(sWhereCol)) = 0 Then
                oSearchDS = Cls.SetTabelsToDataSetAcc("Select * from " & sSearchTableName, "Search")
            Else
                oSearchDS = Cls.SetTabelsToDataSetAcc("Select * from " & sSearchTableName & " where " & sWhereCol, "Search")
            End If

        End If

        DV.Table = oSearchDS.Tables("Search")
        cboSearchFields.Items.Clear()
        J = 0
        For I = 0 To DV.Table.Columns.Count - 1
            If UCase(Trim(MyArray(J))) = UCase(Trim(DV.Table.Columns.Item(I).ToString)) Then
                cboSearchFields.Items.Add(DV.Table.Columns.Item(I))
                J = J + 3
                If MyArray.Length <= J Then Exit For
            End If

        Next
        J = 0
        For I = 0 To DV.Table.Columns.Count - 1
            If UCase(Trim(MyArray(J))) = UCase(Trim(DV.Table.Columns.Item(I).ToString)) Then
                ComboBox1.Items.Add(DV.Table.Columns(I).DataType)
                J = J + 3
                If MyArray.Length <= J Then Exit For
            End If
        Next

        AssignDatSet = True
        Exit Function
ErrTrap:
        AssignDatSet = False
    End Function


    Private Sub cboSearchFields_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSearchFields.SelectedIndexChanged
        ComboBox1.SelectedIndex = cboSearchFields.SelectedIndex
        lblDisp.Text = Mid(ComboBox1.Text, InStr(ComboBox1.Text, ".") + 1)
        Select Case lblDisp.Text
            Case "String"
                optAny.Visible = True
                optStart.Visible = True
                optEnd.Visible = True
                optStart.Checked = True
            Case "DateTime"
                optAny.Visible = False
                optStart.Visible = False
                optEnd.Visible = False
            Case "Decimal"
                optAny.Visible = False
                optStart.Visible = False
                optEnd.Visible = False
            Case Else
                optAny.Visible = False
                optStart.Visible = False
                optEnd.Visible = False
        End Select
    End Sub
    Private Sub optAny_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optAny.Click
        txtSearchBy.Focus()
    End Sub
    Private Sub optEnd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optEnd.Click
        txtSearchBy.Focus()
    End Sub
    Private Sub optStart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optStart.Click
        txtSearchBy.Focus()
    End Sub

    Private Sub frmHelpSearch_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If Var1 = "" Then
            Var1 = "" 'dgHelpSearch.Item(0, 0)
            Var2 = "" 'dgHelpSearch.Item(0, 1)
            Var3 = "" 'dgHelpSearch.Item(0, 3)"
        End If
    End Sub
End Class
