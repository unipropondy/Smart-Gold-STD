Public Class frmHelpSearchNew
    Inherits System.Windows.Forms.Form
    Dim DV As New DataView
    Dim sSearchTableName As String
    Dim dPeriodDateFiled As String
    Dim iFinYear As Int32
    Dim lPeriod As Int32
    Dim iMonth As Int32
    Dim iYear As Int32
    Dim oSearchDS As New DataSet
    Public NewVar1 As String
    Public NewVar2 As String
    Public NewVar3 As String
    Dim X1 As Decimal
    Dim Y1 As Decimal
    Dim TblName As String
    Dim SearchBy As String
    Dim NoofCol As Integer
    Dim SortCol As String, MyArray() As String
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
    End Sub
    Public Sub New(ByVal P1 As Decimal, ByVal P2 As Decimal, ByVal TName As String, ByVal PeriodWiseSearchFieldName As String, ByVal NCol As Integer, ByVal SCol As String, ByVal ParamArray MyArr() As String)
        MyBase.New()
        InitializeComponent()
        X1 = 580
        Y1 = 55
        NoofCol = NCol
        TblName = TName
        sSearchTableName = TName
        dPeriodDateFiled = PeriodWiseSearchFieldName
        lPeriod = nPeriods
        iFinYear = Val(FinYear)
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
    Friend WithEvents chkFilteringType As System.Windows.Forms.CheckBox
    Friend WithEvents cboPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents grpBox As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnScanRecords As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblNoofRecords As System.Windows.Forms.Label
    Friend WithEvents cboSearchFields As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearchText As System.Windows.Forms.TextBox
    Friend WithEvents chkInPeriod As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents optStart As System.Windows.Forms.RadioButton
    Friend WithEvents optEnd As System.Windows.Forms.RadioButton
    Friend WithEvents optAny As System.Windows.Forms.RadioButton
    Friend WithEvents lblDisp As System.Windows.Forms.Label
    Friend WithEvents cmdClearFilters As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmHelpSearchNew))
        Me.dgHelpSearch = New System.Windows.Forms.DataGrid
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtSearchBy = New System.Windows.Forms.TextBox
        Me.cmdExit = New System.Windows.Forms.Button
        Me.cmdSelect = New System.Windows.Forms.Button
        Me.lblSearch = New System.Windows.Forms.Label
        Me.chkFilteringType = New System.Windows.Forms.CheckBox
        Me.cboPeriod = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.grpBox = New System.Windows.Forms.GroupBox
        Me.lblDisp = New System.Windows.Forms.Label
        Me.optStart = New System.Windows.Forms.RadioButton
        Me.btnScanRecords = New System.Windows.Forms.Button
        Me.chkInPeriod = New System.Windows.Forms.CheckBox
        Me.txtSearchText = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cboSearchFields = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.optEnd = New System.Windows.Forms.RadioButton
        Me.optAny = New System.Windows.Forms.RadioButton
        Me.cmdClearFilters = New System.Windows.Forms.Button
        Me.lblNoofRecords = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        CType(Me.dgHelpSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgHelpSearch
        '
        Me.dgHelpSearch.BackgroundColor = System.Drawing.Color.White
        Me.dgHelpSearch.CaptionBackColor = System.Drawing.Color.Yellow
        Me.dgHelpSearch.CaptionForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.dgHelpSearch.DataMember = ""
        Me.dgHelpSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgHelpSearch.GridLineColor = System.Drawing.Color.Black
        Me.dgHelpSearch.HeaderBackColor = System.Drawing.Color.Orange
        Me.dgHelpSearch.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgHelpSearch.Location = New System.Drawing.Point(2, 135)
        Me.dgHelpSearch.Name = "dgHelpSearch"
        Me.dgHelpSearch.ParentRowsBackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.dgHelpSearch.ReadOnly = True
        Me.dgHelpSearch.Size = New System.Drawing.Size(429, 510)
        Me.dgHelpSearch.TabIndex = 130
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 95)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 17)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Search By..."
        '
        'txtSearchBy
        '
        Me.txtSearchBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchBy.Location = New System.Drawing.Point(170, 115)
        Me.txtSearchBy.Name = "txtSearchBy"
        Me.txtSearchBy.Size = New System.Drawing.Size(259, 20)
        Me.txtSearchBy.TabIndex = 9
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
        Me.lblSearch.Location = New System.Drawing.Point(5, 115)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(166, 20)
        Me.lblSearch.TabIndex = 18
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkFilteringType
        '
        Me.chkFilteringType.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkFilteringType.BackColor = System.Drawing.Color.Honeydew
        Me.chkFilteringType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFilteringType.ForeColor = System.Drawing.Color.Black
        Me.chkFilteringType.Image = CType(resources.GetObject("chkFilteringType.Image"), System.Drawing.Image)
        Me.chkFilteringType.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.chkFilteringType.Location = New System.Drawing.Point(0, 0)
        Me.chkFilteringType.Name = "chkFilteringType"
        Me.chkFilteringType.Size = New System.Drawing.Size(200, 25)
        Me.chkFilteringType.TabIndex = 19
        Me.chkFilteringType.Text = "Filtering Type"
        Me.chkFilteringType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboPeriod
        '
        Me.cboPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPeriod.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPeriod.Items.AddRange(New Object() {"", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25"})
        Me.cboPeriod.Location = New System.Drawing.Point(345, 0)
        Me.cboPeriod.Name = "cboPeriod"
        Me.cboPeriod.Size = New System.Drawing.Size(85, 22)
        Me.cboPeriod.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(300, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 18)
        Me.Label2.TabIndex = 452
        Me.Label2.Text = "Period"
        '
        'grpBox
        '
        Me.grpBox.Controls.Add(Me.lblDisp)
        Me.grpBox.Controls.Add(Me.optStart)
        Me.grpBox.Controls.Add(Me.btnScanRecords)
        Me.grpBox.Controls.Add(Me.chkInPeriod)
        Me.grpBox.Controls.Add(Me.txtSearchText)
        Me.grpBox.Controls.Add(Me.Label3)
        Me.grpBox.Controls.Add(Me.cboSearchFields)
        Me.grpBox.Controls.Add(Me.Label4)
        Me.grpBox.Controls.Add(Me.optEnd)
        Me.grpBox.Controls.Add(Me.optAny)
        Me.grpBox.Controls.Add(Me.cmdClearFilters)
        Me.grpBox.Location = New System.Drawing.Point(0, 20)
        Me.grpBox.Name = "grpBox"
        Me.grpBox.Size = New System.Drawing.Size(440, 75)
        Me.grpBox.TabIndex = 454
        Me.grpBox.TabStop = False
        '
        'lblDisp
        '
        Me.lblDisp.Location = New System.Drawing.Point(9, 53)
        Me.lblDisp.Name = "lblDisp"
        Me.lblDisp.Size = New System.Drawing.Size(76, 17)
        Me.lblDisp.TabIndex = 461
        '
        'optStart
        '
        Me.optStart.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optStart.Location = New System.Drawing.Point(88, 54)
        Me.optStart.Name = "optStart"
        Me.optStart.Size = New System.Drawing.Size(51, 13)
        Me.optStart.TabIndex = 3
        Me.optStart.Text = "Start"
        Me.optStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnScanRecords
        '
        Me.btnScanRecords.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnScanRecords.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnScanRecords.ForeColor = System.Drawing.Color.Black
        Me.btnScanRecords.Image = CType(resources.GetObject("btnScanRecords.Image"), System.Drawing.Image)
        Me.btnScanRecords.Location = New System.Drawing.Point(305, 31)
        Me.btnScanRecords.Name = "btnScanRecords"
        Me.btnScanRecords.Size = New System.Drawing.Size(124, 19)
        Me.btnScanRecords.TabIndex = 9
        Me.btnScanRecords.Text = "Scan Records (F9)"
        Me.btnScanRecords.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkInPeriod
        '
        Me.chkInPeriod.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkInPeriod.BackColor = System.Drawing.Color.Honeydew
        Me.chkInPeriod.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInPeriod.Image = CType(resources.GetObject("chkInPeriod.Image"), System.Drawing.Image)
        Me.chkInPeriod.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.chkInPeriod.Location = New System.Drawing.Point(249, 7)
        Me.chkInPeriod.Name = "chkInPeriod"
        Me.chkInPeriod.Size = New System.Drawing.Size(178, 23)
        Me.chkInPeriod.TabIndex = 6
        Me.chkInPeriod.Text = "In Period"
        Me.chkInPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtSearchText
        '
        Me.txtSearchText.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchText.Location = New System.Drawing.Point(86, 30)
        Me.txtSearchText.Name = "txtSearchText"
        Me.txtSearchText.Size = New System.Drawing.Size(160, 20)
        Me.txtSearchText.TabIndex = 2
        Me.txtSearchText.Text = ""
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(10, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 18)
        Me.Label3.TabIndex = 453
        Me.Label3.Text = "Search Field"
        '
        'cboSearchFields
        '
        Me.cboSearchFields.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSearchFields.Location = New System.Drawing.Point(86, 8)
        Me.cboSearchFields.Name = "cboSearchFields"
        Me.cboSearchFields.Size = New System.Drawing.Size(161, 22)
        Me.cboSearchFields.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Location = New System.Drawing.Point(10, 31)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 18)
        Me.Label4.TabIndex = 453
        Me.Label4.Text = "Search Text"
        '
        'optEnd
        '
        Me.optEnd.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optEnd.Location = New System.Drawing.Point(144, 54)
        Me.optEnd.Name = "optEnd"
        Me.optEnd.Size = New System.Drawing.Size(45, 13)
        Me.optEnd.TabIndex = 4
        Me.optEnd.Text = "End"
        Me.optEnd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'optAny
        '
        Me.optAny.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optAny.Location = New System.Drawing.Point(194, 54)
        Me.optAny.Name = "optAny"
        Me.optAny.Size = New System.Drawing.Size(48, 16)
        Me.optAny.TabIndex = 5
        Me.optAny.Text = "Any"
        Me.optAny.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmdClearFilters
        '
        Me.cmdClearFilters.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.cmdClearFilters.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClearFilters.ForeColor = System.Drawing.Color.Black
        Me.cmdClearFilters.Image = CType(resources.GetObject("cmdClearFilters.Image"), System.Drawing.Image)
        Me.cmdClearFilters.Location = New System.Drawing.Point(305, 52)
        Me.cmdClearFilters.Name = "cmdClearFilters"
        Me.cmdClearFilters.Size = New System.Drawing.Size(124, 19)
        Me.cmdClearFilters.TabIndex = 8
        Me.cmdClearFilters.Text = "Clear Allfilters (F8)"
        Me.cmdClearFilters.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNoofRecords
        '
        Me.lblNoofRecords.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNoofRecords.ForeColor = System.Drawing.Color.Red
        Me.lblNoofRecords.Location = New System.Drawing.Point(220, 95)
        Me.lblNoofRecords.Name = "lblNoofRecords"
        Me.lblNoofRecords.Size = New System.Drawing.Size(105, 18)
        Me.lblNoofRecords.TabIndex = 459
        Me.lblNoofRecords.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Blue
        Me.Label5.Location = New System.Drawing.Point(330, 95)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(94, 18)
        Me.Label5.TabIndex = 458
        Me.Label5.Text = "Records Found"
        '
        'ComboBox1
        '
        Me.ComboBox1.ItemHeight = 13
        Me.ComboBox1.Location = New System.Drawing.Point(110, 0)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(80, 21)
        Me.ComboBox1.TabIndex = 455
        Me.ComboBox1.Text = "ComboBox1"
        Me.ComboBox1.Visible = False
        '
        'frmHelpSearchNew
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(432, 676)
        Me.Controls.Add(Me.cboPeriod)
        Me.Controls.Add(Me.chkFilteringType)
        Me.Controls.Add(Me.grpBox)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdSelect)
        Me.Controls.Add(Me.dgHelpSearch)
        Me.Controls.Add(Me.txtSearchBy)
        Me.Controls.Add(Me.lblSearch)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblNoofRecords)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ComboBox1)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmHelpSearchNew"
        Me.Text = "Help Search"
        CType(Me.dgHelpSearch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpBox.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private Sub frmHelpSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Location = New Point(X1, Y1)
        Dim TabSty As New DataGridTableStyle
        dgHelpSearch.TableStyles.Add(fnSetGridStyle())
        cboPeriod.Text = nPeriods
        grpBox.Visible = False
        chkFilteringType.Text = "By Period"
        AssignDatSet()
        SearchBy = MyArray(3)
        lblSearch.Text = MyArray(4)

    End Sub
    Private Sub txtSearchBy_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchBy.TextChanged
        On Error GoTo ErrTrap
        'DV = GetRecordsFromMasterDataSet("" & TblName & "", "" & SortCol & "")
        DV.Table = oSearchDS.Tables(sSearchTableName)

        If Len(Trim(SearchBy)) = 0 Then
            SearchBy = MyArray(0)
            lblSearch.Text = MyArray(1)
        End If
        DV.RowFilter = SearchBy & " Like '" & txtSearchBy.Text & "%'"
        dgHelpSearch.DataSource = DV

        lblNoofRecords.Text = "(" & DV.Count & ")"

        Exit Sub
ErrTrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Sub
    Private Sub frmHelpSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Close()
            Case Keys.F9
                btnScanRecords_Click(sender, e)
            Case Keys.F8
                cmdClearFilters_Click(sender, e)
        End Select
    End Sub
    Private Sub cmdSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelect.Click
        NewVar1 = dgHelpSearch.Item(dgHelpSearch.CurrentRowIndex, 0)
        NewVar2 = dgHelpSearch.Item(dgHelpSearch.CurrentRowIndex, 1)
        NewVar3 = dgHelpSearch.Item(dgHelpSearch.CurrentRowIndex, 3)
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
        TabStlye.MappingName = sSearchTableName
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
        NewVar1 = dgHelpSearch.Item(dgHelpSearch.CurrentRowIndex, 0)
        NewVar2 = dgHelpSearch.Item(dgHelpSearch.CurrentRowIndex, 1)
        NewVar3 = IIf(IsDBNull(dgHelpSearch.Item(dgHelpSearch.CurrentRowIndex, 3)) = True, "", dgHelpSearch.Item(dgHelpSearch.CurrentRowIndex, 3))
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
                NewVar1 = dgHelpSearch(HTI.Row, 0)
                NewVar2 = dgHelpSearch(HTI.Row, 1)
                NewVar3 = dgHelpSearch(HTI.Row, 3)
            End If
        ElseIf HTI.Type = DataGrid.HitTestType.ColumnHeader Then
            SearchBy = MyArray(HTI.Column * 3)
            lblSearch.Text = MyArray((HTI.Column * 3) + 1)
        End If
err:
    End Sub
    Private Sub txtSearchBy_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearchBy.KeyDown
        If e.KeyCode = Keys.Enter Then
            NewVar1 = dgHelpSearch.Item(0, 0)
            NewVar2 = dgHelpSearch.Item(0, 1)
            NewVar3 = dgHelpSearch.Item(0, 3)
            cmdExit_Click(sender, e)
        End If
    End Sub


    Private Sub frmHelpSearch_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        txtSearchBy.Focus()
    End Sub
    Private Sub chkFilteringType_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilteringType.CheckedChanged
        If chkFilteringType.Checked = False Then
            grpBox.Visible = False
            chkFilteringType.Text = "Search"
            AssignDatSet()
        Else
            grpBox.Visible = True
            chkFilteringType.Text = "Advance Search"
            AssignDatSet()
        End If
    End Sub
    Private Function AssignDatSet() As Boolean
        Dim Cls As New DataManager
        On Error GoTo ErrTrap
        Dim I As Int32
        AssignDatSet = False
        FnFindMonthAndYear(lPeriod)
        If sSearchTableName = "tblGidHeader" Then
            'oSearchDS = Cls.SetMasterDataSetNewMasterDB("Select * from " & sSearchTableName & " where month(" & dPeriodDateFiled & ")=" & iMonth & " and  year(" & dPeriodDateFiled & ")=" & iYear & " and Accflag <> 'Y' and Cancelled=0", sSearchTableName)
            oSearchDS = Cls.SetMasterDataSetAcc("Select * from " & sSearchTableName & " where month(" & dPeriodDateFiled & ")=" & iMonth & " and  year(" & dPeriodDateFiled & ")=" & iYear & " and Accflag <> 'Y' and Cancelled=0", sSearchTableName)
        Else
            oSearchDS = Cls.SetTabelsToDataSetAcc("Select * from " & sSearchTableName & " where month(" & dPeriodDateFiled & ")=" & iMonth & " and  year(" & dPeriodDateFiled & ")=" & iYear, sSearchTableName)
        End If

        DV.Table = oSearchDS.Tables(sSearchTableName)
        cboSearchFields.Items.Clear()
        For I = 0 To DV.Table.Columns.Count - 1
            cboSearchFields.Items.Add(DV.Table.Columns.Item(I))
        Next
        For I = 0 To DV.Table.Columns.Count - 1
            ComboBox1.Items.Add(DV.Table.Columns(I).DataType)
        Next
        dgHelpSearch.DataSource = DV
        lblNoofRecords.Text = "(" & DV.Count & ")"
        AssignDatSet = True
        Exit Function
ErrTrap:
        MsgBox(Err.Description)
        AssignDatSet = False
    End Function

    Private Sub cboPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPeriod.SelectedIndexChanged
        'chkFilteringType.Checked = False
        'grpBox.Visible = False
        lPeriod = Val(cboPeriod.Text)
        AssignDatSet()
    End Sub
    Private Sub FnFindMonthAndYear(ByVal Period As Integer)
        Dim Ds As New DataSet
        Dim Cls As New DataManager
        Dim tCurDate As Date
        Dim DR() As DataRow
        If Period = 0 Then Period = nPeriods
        Ds = Cls.SetTabelsToDataSetAcc("Select * from tblAccountControl", "AC")
        DR = Ds.Tables("AC").Select
        If DR.Length > 0 Then
            tCurDate = CDate(DR(0)("StartDate" & Val(Period)) & "")
            iMonth = tCurDate.Month
            iYear = tCurDate.Year
        End If
        If iMonth <= 0 Or iMonth >= 13 Then
            MsgBox("Invalid Period")
            iMonth = ServerDate.Month
            iYear = ServerDate.Year
        End If
    End Sub
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

    Private Sub txtSearchText_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchText.TextChanged

    End Sub

    Private Sub txtSearchText_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchText.LostFocus
        Dim St As String
        If Len(Trim(cboSearchFields.Text)) = 0 Then
            Exit Sub
        End If

        Select Case lblDisp.Text
            Case "String"
                If optAny.Checked = True Then
                    St = cboSearchFields.Text & "  like '%" & txtSearchText.Text & "%'"
                ElseIf optStart.Checked = True Then
                    St = cboSearchFields.Text & " like '" & txtSearchText.Text & "%'"
                ElseIf optEnd.Checked = True Then
                    St = cboSearchFields.Text & " like '%" & txtSearchText.Text & "'"
                End If
                AssignDatSetFromTextFilter(St)
            Case "DateTime"
                If IsDate(txtSearchText.Text) = False Then
                    MsgBox("Invalid Dates")
                    St = cboSearchFields.Text & " = " & fncFormatSQLDate(txtSearchText.Text)
                Else
                    St = cboSearchFields.Text & " = " & fncFormatSQLDate(Format(txtSearchText.Text, "yyyy") & "/" & Format(txtSearchText.Text, "MM") & "/" & Format(txtSearchText.Text, "dd"))
                End If
                optAny.Visible = False
                optStart.Visible = False
                optEnd.Visible = False
                AssignDatSetFromTextFilter(St)
            Case "Decimal"
                If Val(txtSearchText.Text) <> 0 Then
                    St = cboSearchFields.Text & " = " & Val(txtSearchText.Text)
                Else
                    St = ""
                End If
                optAny.Visible = False
                optStart.Visible = False
                optEnd.Visible = False
                AssignDatSetFromTextFilter(St)
            Case Else
                If Val(txtSearchText.Text) <> 0 Then
                    St = cboSearchFields.Text & " = " & Val(txtSearchText.Text)
                Else
                    St = ""
                End If
                optAny.Visible = False
                optStart.Visible = False
                optEnd.Visible = False
                AssignDatSetFromTextFilter(St)
        End Select
    End Sub
    Private Function AssignDatSetFromTextFilter(ByVal WhereCon As String) As Boolean
        Dim Cls As New DataManager
        On Error GoTo ErrTrap
        Dim I As Int32
        AssignDatSetFromTextFilter = False
        FnFindMonthAndYear(lPeriod)
        If Len(Trim(WhereCon)) <> 0 Then
            If chkInPeriod.Checked = False Then
                If sSearchTableName = "tblGidHeader" Then
                    'oSearchDS = Cls.SetTabelsToDataSetFromMasterDB("Select * from " & sSearchTableName & " where month(" & dPeriodDateFiled & ")=" & iMonth & " and  year(" & dPeriodDateFiled & ")=" & iYear & "  and " & WhereCon, sSearchTableName)
                    oSearchDS = Cls.SetTabelsToDataSetAcc("Select * from " & sSearchTableName & " where month(" & dPeriodDateFiled & ")=" & iMonth & " and  year(" & dPeriodDateFiled & ")=" & iYear & "  and " & WhereCon, sSearchTableName)
                Else
                    oSearchDS = Cls.SetTabelsToDataSetAcc("Select * from " & sSearchTableName & " where month(" & dPeriodDateFiled & ")=" & iMonth & " and  year(" & dPeriodDateFiled & ")=" & iYear & "  and " & WhereCon, sSearchTableName)
                End If
            Else
                If sSearchTableName = "tblGidHeader" Then
                    'oSearchDS = Cls.SetTabelsToDataSetFromMasterDB("Select * from " & sSearchTableName & " where   " & WhereCon, sSearchTableName)
                    oSearchDS = Cls.SetTabelsToDataSetAcc("Select * from " & sSearchTableName & " where   " & WhereCon, sSearchTableName)
                Else
                    oSearchDS = Cls.SetTabelsToDataSetAcc("Select * from " & sSearchTableName & " where   " & WhereCon, sSearchTableName)
                End If
            End If
        Else
            If sSearchTableName = "tblGidHeader" Then
                'oSearchDS = Cls.SetTabelsToDataSetFromMasterDB("Select * from " & sSearchTableName & " where month(" & dPeriodDateFiled & ")=" & iMonth & " and  year(" & dPeriodDateFiled & ")=" & iYear, sSearchTableName)
                oSearchDS = Cls.SetTabelsToDataSetAcc("Select * from " & sSearchTableName & " where month(" & dPeriodDateFiled & ")=" & iMonth & " and  year(" & dPeriodDateFiled & ")=" & iYear, sSearchTableName)
            Else
                oSearchDS = Cls.SetTabelsToDataSetAcc("Select * from " & sSearchTableName & " where month(" & dPeriodDateFiled & ")=" & iMonth & " and  year(" & dPeriodDateFiled & ")=" & iYear, sSearchTableName)
            End If
        End If


        DV.Table = oSearchDS.Tables(sSearchTableName)
        cboSearchFields.Items.Clear()
        For I = 0 To DV.Table.Columns.Count - 1
            cboSearchFields.Items.Add(DV.Table.Columns.Item(I))
        Next

        For I = 0 To DV.Table.Columns.Count - 1
            ComboBox1.Items.Add(DV.Table.Columns(I).DataType)
        Next

        lblNoofRecords.Text = "(" & DV.Count & ")"
        dgHelpSearch.DataSource = DV
        AssignDatSetFromTextFilter = True
        Exit Function
ErrTrap:
        MsgBox(Err.Description)
        AssignDatSetFromTextFilter = False
    End Function
    Private Sub btnScanRecords_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanRecords.Click
        txtSearchText_LostFocus(sender, e)
    End Sub
    Private Sub chkInPeriod_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkInPeriod.CheckedChanged
        If chkInPeriod.Checked = False Then
            chkInPeriod.Text = "In-Period"
            txtSearchText_LostFocus(sender, e)
        Else
            chkInPeriod.Text = "All-Period"
            txtSearchText_LostFocus(sender, e)
        End If
    End Sub
    Private Sub cmdClearFilters_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClearFilters.Click
        txtSearchText.Text = ""
        cboSearchFields.Text = ""
        grpBox.Visible = False
        chkFilteringType.Checked = False
        chkFilteringType.Text = "Search"
        cboPeriod.Text = nPeriods
        lPeriod = Val(cboPeriod.Text)
        AssignDatSet()
    End Sub

    Private Sub dgHelpSearch_Navigate(ByVal sender As System.Object, ByVal ne As System.Windows.Forms.NavigateEventArgs) Handles dgHelpSearch.Navigate

    End Sub

    Private Sub optAny_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optAny.CheckedChanged

    End Sub

    Private Sub grpBox_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grpBox.Enter

    End Sub
End Class
