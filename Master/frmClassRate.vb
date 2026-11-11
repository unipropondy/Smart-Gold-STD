Public Class frmClassRate
    Inherits System.Windows.Forms.Form

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
    Friend WithEvents ErrP As System.Windows.Forms.ErrorProvider
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnClassSearch As System.Windows.Forms.Button
    Friend WithEvents txtClassCode As System.Windows.Forms.TextBox
    Friend WithEvents txtClassName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtFDD As System.Windows.Forms.TextBox
    Friend WithEvents txtFMM As System.Windows.Forms.TextBox
    Friend WithEvents txtFYYYY As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtTYYYY As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtRate As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnClearDetails As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents txtTMM As System.Windows.Forms.TextBox
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents grdClassRate As System.Windows.Forms.DataGridView
    Friend WithEvents cFromdate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cToDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cRate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtTDD As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmClassRate))
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ErrP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnClearDetails = New System.Windows.Forms.Button()
        Me.txtRate = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtTYYYY = New System.Windows.Forms.TextBox()
        Me.txtTMM = New System.Windows.Forms.TextBox()
        Me.txtTDD = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtFYYYY = New System.Windows.Forms.TextBox()
        Me.txtFMM = New System.Windows.Forms.TextBox()
        Me.txtFDD = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnClassSearch = New System.Windows.Forms.Button()
        Me.txtClassCode = New System.Windows.Forms.TextBox()
        Me.txtClassName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.grdClassRate = New System.Windows.Forms.DataGridView()
        Me.cFromdate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cToDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cRate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.ErrP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdClassRate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ErrP
        '
        Me.ErrP.ContainerControl = Me
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.btnClearDetails)
        Me.Panel1.Controls.Add(Me.txtRate)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.txtTYYYY)
        Me.Panel1.Controls.Add(Me.txtTMM)
        Me.Panel1.Controls.Add(Me.txtTDD)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtFYYYY)
        Me.Panel1.Controls.Add(Me.txtFMM)
        Me.Panel1.Controls.Add(Me.txtFDD)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.btnClassSearch)
        Me.Panel1.Controls.Add(Me.txtClassCode)
        Me.Panel1.Controls.Add(Me.txtClassName)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Location = New System.Drawing.Point(442, 111)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(425, 262)
        Me.Panel1.TabIndex = 31
        '
        'btnClearDetails
        '
        Me.btnClearDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClearDetails.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearDetails.Location = New System.Drawing.Point(277, 202)
        Me.btnClearDetails.Name = "btnClearDetails"
        Me.btnClearDetails.Size = New System.Drawing.Size(126, 40)
        Me.btnClearDetails.TabIndex = 14
        Me.btnClearDetails.Text = "Clear Details(F5)"
        Me.btnClearDetails.UseVisualStyleBackColor = False
        '
        'txtRate
        '
        Me.txtRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRate.Location = New System.Drawing.Point(132, 164)
        Me.txtRate.MaxLength = 24
        Me.txtRate.Name = "txtRate"
        Me.txtRate.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtRate.Size = New System.Drawing.Size(100, 21)
        Me.txtRate.TabIndex = 9
        Me.txtRate.Tag = "ENN;"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(16, 157)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 23)
        Me.Label5.TabIndex = 52
        Me.Label5.Text = "Rate"
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(16, 124)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(88, 23)
        Me.Label11.TabIndex = 50
        Me.Label11.Text = "To Date"
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(190, 131)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(8, 16)
        Me.Label9.TabIndex = 49
        Me.Label9.Text = "-"
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(156, 131)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(8, 16)
        Me.Label10.TabIndex = 48
        Me.Label10.Text = "-"
        '
        'txtTYYYY
        '
        Me.txtTYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTYYYY.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTYYYY.Location = New System.Drawing.Point(200, 129)
        Me.txtTYYYY.MaxLength = 4
        Me.txtTYYYY.Name = "txtTYYYY"
        Me.txtTYYYY.Size = New System.Drawing.Size(32, 21)
        Me.txtTYYYY.TabIndex = 8
        Me.txtTYYYY.Tag = "ENN;"
        '
        'txtTMM
        '
        Me.txtTMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTMM.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTMM.Location = New System.Drawing.Point(166, 129)
        Me.txtTMM.MaxLength = 2
        Me.txtTMM.Name = "txtTMM"
        Me.txtTMM.Size = New System.Drawing.Size(20, 21)
        Me.txtTMM.TabIndex = 7
        Me.txtTMM.Tag = "ENN;"
        '
        'txtTDD
        '
        Me.txtTDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTDD.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTDD.Location = New System.Drawing.Point(132, 129)
        Me.txtTDD.MaxLength = 2
        Me.txtTDD.Name = "txtTDD"
        Me.txtTDD.Size = New System.Drawing.Size(20, 21)
        Me.txtTDD.TabIndex = 6
        Me.txtTDD.Tag = "ENN;"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(190, 95)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(8, 16)
        Me.Label2.TabIndex = 44
        Me.Label2.Text = "-"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(156, 95)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(8, 16)
        Me.Label1.TabIndex = 43
        Me.Label1.Text = "-"
        '
        'txtFYYYY
        '
        Me.txtFYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFYYYY.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFYYYY.Location = New System.Drawing.Point(200, 94)
        Me.txtFYYYY.MaxLength = 4
        Me.txtFYYYY.Name = "txtFYYYY"
        Me.txtFYYYY.Size = New System.Drawing.Size(32, 21)
        Me.txtFYYYY.TabIndex = 5
        Me.txtFYYYY.Tag = "ENN;"
        '
        'txtFMM
        '
        Me.txtFMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFMM.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFMM.Location = New System.Drawing.Point(166, 94)
        Me.txtFMM.MaxLength = 2
        Me.txtFMM.Name = "txtFMM"
        Me.txtFMM.Size = New System.Drawing.Size(20, 21)
        Me.txtFMM.TabIndex = 4
        Me.txtFMM.Tag = "ENN;"
        '
        'txtFDD
        '
        Me.txtFDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFDD.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFDD.Location = New System.Drawing.Point(132, 94)
        Me.txtFDD.MaxLength = 2
        Me.txtFDD.Name = "txtFDD"
        Me.txtFDD.Size = New System.Drawing.Size(20, 21)
        Me.txtFDD.TabIndex = 3
        Me.txtFDD.Tag = "ENN;"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(16, 91)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(88, 23)
        Me.Label7.TabIndex = 38
        Me.Label7.Text = "From Date"
        '
        'btnClassSearch
        '
        Me.btnClassSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClassSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClassSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClassSearch.Location = New System.Drawing.Point(316, 24)
        Me.btnClassSearch.Name = "btnClassSearch"
        Me.btnClassSearch.Size = New System.Drawing.Size(24, 21)
        Me.btnClassSearch.TabIndex = 1
        Me.btnClassSearch.Text = "..."
        Me.btnClassSearch.UseVisualStyleBackColor = False
        '
        'txtClassCode
        '
        Me.txtClassCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtClassCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClassCode.Location = New System.Drawing.Point(132, 24)
        Me.txtClassCode.MaxLength = 20
        Me.txtClassCode.Name = "txtClassCode"
        Me.txtClassCode.Size = New System.Drawing.Size(190, 21)
        Me.txtClassCode.TabIndex = 0
        Me.txtClassCode.Tag = "NE;"
        '
        'txtClassName
        '
        Me.txtClassName.BackColor = System.Drawing.Color.White
        Me.txtClassName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtClassName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClassName.Location = New System.Drawing.Point(132, 59)
        Me.txtClassName.MaxLength = 50
        Me.txtClassName.Name = "txtClassName"
        Me.txtClassName.ReadOnly = True
        Me.txtClassName.Size = New System.Drawing.Size(208, 21)
        Me.txtClassName.TabIndex = 2
        Me.txtClassName.Tag = ""
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(16, 57)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(96, 24)
        Me.Label4.TabIndex = 28
        Me.Label4.Text = "Class Name"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 23)
        Me.Label3.TabIndex = 27
        Me.Label3.Text = "Class Code"
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClear.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.Location = New System.Drawing.Point(440, 382)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(100, 40)
        Me.btnClear.TabIndex = 11
        Me.btnClear.Text = "Clear(F6)"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(767, 382)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 13
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(549, 382)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 40)
        Me.btnSave.TabIndex = 10
        Me.btnSave.Text = "Save(F7)"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDelete.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(658, 382)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(100, 40)
        Me.btnDelete.TabIndex = 12
        Me.btnDelete.Text = "Delete(F8)"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(-2, -3)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 100)
        Me.pic.TabIndex = 85
        Me.pic.TabStop = False
        '
        'grdClassRate
        '
        Me.grdClassRate.AllowUserToAddRows = False
        Me.grdClassRate.AllowUserToDeleteRows = False
        Me.grdClassRate.AllowUserToResizeColumns = False
        Me.grdClassRate.AllowUserToResizeRows = False
        Me.grdClassRate.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdClassRate.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdClassRate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdClassRate.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cFromdate, Me.cToDate, Me.cRate})
        Me.grdClassRate.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdClassRate.Location = New System.Drawing.Point(-2, 111)
        Me.grdClassRate.Name = "grdClassRate"
        Me.grdClassRate.RowHeadersVisible = False
        Me.grdClassRate.Size = New System.Drawing.Size(438, 262)
        Me.grdClassRate.TabIndex = 86
        '
        'cFromdate
        '
        Me.cFromdate.HeaderText = "From Date"
        Me.cFromdate.Name = "cFromdate"
        Me.cFromdate.Width = 150
        '
        'cToDate
        '
        Me.cToDate.HeaderText = "To Date"
        Me.cToDate.Name = "cToDate"
        Me.cToDate.Width = 150
        '
        'cRate
        '
        Me.cRate.HeaderText = "Rate"
        Me.cRate.Name = "cRate"
        Me.cRate.Width = 150
        '
        'frmClassRate
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(7, 14)
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(891, 436)
        Me.Controls.Add(Me.grdClassRate)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmClassRate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " "
        CType(Me.ErrP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdClassRate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim objclsClassRate As New clsClassRate
    Dim objClsClass As New ClsClass
    Const FromDt = 1
    Const ToDt = 2
    Const Rate = 3
    Const Insert = "I"
    Const Delete = "D"

    Private Sub btnClassSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClassSearch.Click
        Dim frmHelpClass As New frmHelpSearch(0, 0, "tblClass", "L", 4, "ClassCode", "", "ClassCode", "ClassCode", 80, "ClassName", "ClassName", 180, "Rate", "Rate", 60, "Remarks", "Remarks", 80)
        frmHelpClass.ShowDialog()
        txtClassCode.Text = frmHelpClass.Var1.Trim
        txtClassName.Text = frmHelpClass.Var2.Trim
        If txtClassCode.Text.Trim <> "" Then subGetClassRateDetails()
    End Sub

    Private Sub btnClearDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearDetails.Click
        subClearDetails()
        txtFDD.Focus()
    End Sub

    Private Sub subClearDetails()
        Try
            txtFDD.Text = Format(CDate(ServerDate), "dd")
            txtFMM.Text = Format(CDate(ServerDate), "MM")
            txtFYYYY.Text = Format(CDate(ServerDate), "yyyy")
            txtTDD.Clear()
            txtTMM.Clear()
            txtTYYYY.Clear()
            txtRate.Clear()
            btnDelete.Enabled = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        subClearMe()
    End Sub

    Private Sub subClearMe()
        Try
            subClearDetails()
            txtClassCode.Clear()
            txtClassName.Clear()
            grdClassRate.RowCount = 0
            txtClassCode.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtClassCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtClassCode.Validating
        If txtClassCode.Text.Trim = "" Then
            ErrP.SetError(txtClassCode, "Should not be empty.")
            'txtClassCode.Focus()
        Else
            ErrP.Dispose()
        End If
    End Sub

    Private Sub txtRate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtRate.Validating
        If txtRate.Text.Trim = "" Then
            ErrP.SetError(txtRate, "Should not be empty.")
            txtClassCode.Focus()
        ElseIf Not IsNumeric(txtRate.Text.Trim) Then
            ErrP.SetError(txtRate, "Should be Numeric.")
            txtRate.Clear()
            txtRate.Focus()
        Else
            ErrP.Dispose()
        End If
    End Sub

#Region "Validation"

    Private Function fncValidateMe() As Boolean
        Dim bStatus As Boolean
        Dim sFromDate, sTodate As String
        Try
            bStatus = False
            If txtClassCode.Text.Trim = "" Then
                MessageBox.Show("ClassCode should not be blank.")
                txtClassCode.Focus()
                Exit Function
            End If
            sFromDate = txtFYYYY.Text & "/" & txtFMM.Text & "/" & txtFDD.Text
            sTodate = txtTYYYY.Text & "/" & txtTMM.Text & "/" & txtTDD.Text
            If Not IsDate(sFromDate) Then
                MessageBox.Show("Please enter valid from date.")
                txtFDD.Focus()
                Exit Function
            End If
            If Not IsDate(sTodate) Then
                MessageBox.Show("Please enter valid to date.")
                txtTDD.Focus()
                Exit Function
            End If
            Dim k As Long
            If DateDiff(DateInterval.Day, CDate(sFromDate), CDate(sTodate)) < 0 Then
                MessageBox.Show("To date should be greater than From date.")
                txtTDD.Focus()
                Exit Function
            End If
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            bStatus = False
        Finally
            fncValidateMe = bStatus
        End Try
    End Function

#End Region

    Private Sub frmClassRate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F5
                btnClearDetails_Click(sender, e)
            Case Keys.F6
                btnClear_Click(sender, e)
            Case Keys.F7
                btnSave_Click(sender, e)
            Case Keys.F8
                If btnDelete.Enabled Then btnDelete_Click(sender, e)
            Case Keys.Escape
                btnClose_Click(sender, e)
            Case Keys.Return
                SendKeys.Send("{Tab}")
        End Select
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        subSave()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        subDelete()
    End Sub

    Private Sub frmClassRate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'subGridSetting(grdClassRate)
            If sDefaultClassCode.Trim <> "" Then
                txtClassCode.Text = sDefaultClassCode
                txtClassCode_LostFocus(sender, e)
                'subDisplayClassRate(grdClassRate.ActiveRow)
            Else
                txtFDD.Text = Format(CDate(ServerDate), "dd")
                txtFMM.Text = Format(CDate(ServerDate), "MM")
                txtFYYYY.Text = Format(CDate(ServerDate), "yyyy")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub subGetClassRateDetails()
        Dim dsClassRate As New DataSet
        Dim iCnt As Long
        Try
            dsClassRate = objclsClassRate.GetClassRateDetail(txtClassCode.Text.Trim)
            If Not IsNothing(dsClassRate) Then
                If dsClassRate.Tables("ClassRate").Rows.Count > 0 Then
                    grdClassRate.rowcount = 0
                    For iCnt = 0 To dsClassRate.Tables("ClassRate").Rows.Count - 1
                        grdClassRate.Rows.Add()
                        fnSetdgvText(grdClassRate, iCnt, 0, Trim(dsClassRate.Tables("ClassRate").Rows(iCnt)("FromDate") & ""))
                        fnSetdgvText(grdClassRate, iCnt, 1, Trim(dsClassRate.Tables("ClassRate").Rows(iCnt)("ToDate") & ""))
                        fnSetdgvText(grdClassRate, iCnt, 2, Trim(dsClassRate.Tables("ClassRate").Rows(iCnt)("Rate") & ""))
                    Next
                Else
                    grdClassRate.RowCount = 0
                End If
            Else
                grdClassRate.RowCount = 0
            End If
            subDisplayClassRate(grdClassRate.CurrentRow.Index)
            grdClassRate_Click(Nothing, Nothing)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsClassRate = Nothing
        End Try
    End Sub

    Private Sub txtClassCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtClassCode.KeyDown
        If e.KeyCode = Keys.F1 Then btnClassSearch_Click(sender, e)
    End Sub

    Private Sub txtClassCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtClassCode.LostFocus
        Dim sClassName As String
        Try
            If txtClassCode.Text.Trim <> "" Then
                sClassName = objClsClass.GetClassName(txtClassCode.Text.Trim)
                If Not IsNothing(sClassName.Trim) And sClassName.Trim.Length > 0 Then
                    txtClassName.Text = sClassName
                    subGetClassRateDetails()
                Else
                    txtClassName.Clear()
                    txtClassCode.Clear()
                    txtClassCode.Focus()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subDisplayClassRate(ByVal iRow As Integer)
        Try
            btnDelete.Enabled = True
            If iRow >= 0 And txtClassCode.Text.Trim <> "" Then
                txtFDD.Text = Format(CDate(fnGetdgvText(grdClassRate, iRow, 0)), "dd")
                txtFMM.Text = Format(CDate(fnGetdgvText(grdClassRate, iRow, 0)), "MM")
                txtFYYYY.Text = Format(CDate(fnGetdgvText(grdClassRate, iRow, 0)), "yyyy")
                txtTDD.Text = Format(CDate(fnGetdgvText(grdClassRate, iRow, 1)), "dd")
                txtTMM.Text = Format(CDate(fnGetdgvText(grdClassRate, iRow, 1)), "MM")
                txtTYYYY.Text = Format(CDate(fnGetdgvText(grdClassRate, iRow, 1)), "yyyy")
                txtRate.Text = fnGetdgvText(grdClassRate, iRow, 2)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnClassSearch_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClassSearch.GotFocus
        ErrP.Dispose()
    End Sub
    Private Sub subSave()
        Dim sFromDate, sToDate As String
        Try
            If fncValidateMe() Then
                sFromDate = txtFYYYY.Text & "/" & txtFMM.Text & "/" & txtFDD.Text
                sToDate = txtTYYYY.Text & "/" & txtTMM.Text & "/" & txtTDD.Text
                If objclsClassRate.ClassRateSave(txtClassCode.Text.Trim, txtClassName.Text.Trim, sFromDate, sToDate, txtRate.Text, Insert) Then
                    MessageBox.Show("Class Rate detail saved sucssfully.")
                    subClearDetails()
                    subGetClassRateDetails()
                    grdClassRate.Focus()
                Else
                    MessageBox.Show("Error in Save")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subDelete()
        Dim sFromDate, sToDate As String
        Try
            If txtClassCode.Text.Trim = "" Or txtFDD.Text.Trim = "" Or txtFMM.Text.Trim = "" Or txtFYYYY.Text.Trim = "" Or txtTDD.Text.Trim = "" Or txtTMM.Text.Trim = "" Or txtTYYYY.Text.Trim = "" Then
                MessageBox.Show("Please select the Class Rate to Delete?")
                Exit Sub
            End If
            sFromDate = txtFYYYY.Text & "/" & txtFMM.Text & "/" & txtFDD.Text
            sToDate = txtTYYYY.Text & "/" & txtTMM.Text & "/" & txtTDD.Text
            If objclsClassRate.ClassRateSave(txtClassCode.Text.Trim, txtClassName.Text.Trim, sFromDate, sToDate, txtRate.Text, Delete) Then
                MessageBox.Show("Class Rate Deleted.")
                subClearDetails()
                subGetClassRateDetails()
                grdClassRate.Focus()
            Else
                MessageBox.Show("Error in Delete")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtTYYYY_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTYYYY.LostFocus
        subGetClassRate()
    End Sub

    Private Sub subGetClassRate()
        Dim sFromDate, sToDate As String
        Dim dRate As Double
        Try
            If txtClassCode.Text.Trim = "" Or txtFDD.Text.Trim = "" Or txtFMM.Text.Trim = "" Or txtFYYYY.Text.Trim = "" Or txtTDD.Text.Trim = "" Or txtTMM.Text.Trim = "" Or txtTYYYY.Text.Trim = "" Then
                MessageBox.Show("Please select the Class Rate to Delete?")
                Exit Sub
            End If
            sFromDate = txtFYYYY.Text & "/" & txtFMM.Text & "/" & txtFDD.Text
            sToDate = txtTYYYY.Text & "/" & txtTMM.Text & "/" & txtTDD.Text
            dRate = objclsClassRate.GetClassRateDetail(txtClassCode.Text.Trim, sFromDate, sToDate)
            If Not IsNothing(dRate) Or Val(dRate) <> 0 Then
                txtRate.Text = dRate
            Else
                txtRate.Clear()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtClassCode_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtClassCode.TextChanged

    End Sub

    Private Sub grdClassRate_Click(sender As Object, e As System.EventArgs) Handles grdClassRate.Click
        subDisplayClassRate(grdClassRate.CurrentRow.Index)
    End Sub
End Class
