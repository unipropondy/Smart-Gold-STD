Public Class frmCategory
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
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents ErrP As System.Windows.Forms.ErrorProvider
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCategoryCode As System.Windows.Forms.TextBox
    Friend WithEvents txtCategoryName As System.Windows.Forms.TextBox
    Friend WithEvents txtClassCode As System.Windows.Forms.TextBox
    Friend WithEvents btnClassSearch As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtClassName As System.Windows.Forms.TextBox
    Friend WithEvents txtToInventoryCode As System.Windows.Forms.TextBox
    Friend WithEvents txtFromInventoryCode As System.Windows.Forms.TextBox
    Friend WithEvents txtNextInventoryCode As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents txtInventoryPrefix As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents grdCategory As System.Windows.Forms.DataGridView
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents chkShowInReceipt As System.Windows.Forms.CheckBox
    Friend WithEvents cCategoryCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCategoryName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cClassCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cFromInv As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cToInv As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cNextInv As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cRemarks As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSize As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cInvPrefix As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cShowInReceipt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkSize As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCategory))
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.ErrP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.chkShowInReceipt = New System.Windows.Forms.CheckBox()
        Me.txtInventoryPrefix = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.chkSize = New System.Windows.Forms.CheckBox()
        Me.txtToInventoryCode = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtFromInventoryCode = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnClassSearch = New System.Windows.Forms.Button()
        Me.txtClassCode = New System.Windows.Forms.TextBox()
        Me.txtClassName = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtNextInventoryCode = New System.Windows.Forms.TextBox()
        Me.txtCategoryCode = New System.Windows.Forms.TextBox()
        Me.txtCategoryName = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.grdCategory = New System.Windows.Forms.DataGridView()
        Me.cCategoryCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cCategoryName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cClassCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cFromInv = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cToInv = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cNextInv = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cRemarks = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cSize = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cInvPrefix = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cShowInReceipt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.ErrP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDelete.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(862, 491)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(100, 40)
        Me.btnDelete.TabIndex = 13
        Me.btnDelete.Text = "Delete(F8)"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnNew.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Location = New System.Drawing.Point(642, 491)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(100, 40)
        Me.btnNew.TabIndex = 12
        Me.btnNew.Text = "New(F6)"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(972, 491)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 14
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(752, 491)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 40)
        Me.btnSave.TabIndex = 11
        Me.btnSave.Text = "Save(F7)"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'ErrP
        '
        Me.ErrP.ContainerControl = Me
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.chkShowInReceipt)
        Me.Panel1.Controls.Add(Me.txtInventoryPrefix)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.chkSize)
        Me.Panel1.Controls.Add(Me.txtToInventoryCode)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.txtFromInventoryCode)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.btnClassSearch)
        Me.Panel1.Controls.Add(Me.txtClassCode)
        Me.Panel1.Controls.Add(Me.txtClassName)
        Me.Panel1.Controls.Add(Me.txtRemarks)
        Me.Panel1.Controls.Add(Me.txtNextInventoryCode)
        Me.Panel1.Controls.Add(Me.txtCategoryCode)
        Me.Panel1.Controls.Add(Me.txtCategoryName)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(638, 114)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(434, 354)
        Me.Panel1.TabIndex = 30
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(16, 317)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(110, 24)
        Me.Label11.TabIndex = 46
        Me.Label11.Text = "Show in Receipt"
        '
        'chkShowInReceipt
        '
        Me.chkShowInReceipt.AutoSize = True
        Me.chkShowInReceipt.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkShowInReceipt.Location = New System.Drawing.Point(188, 316)
        Me.chkShowInReceipt.Name = "chkShowInReceipt"
        Me.chkShowInReceipt.Size = New System.Drawing.Size(15, 14)
        Me.chkShowInReceipt.TabIndex = 45
        Me.chkShowInReceipt.UseVisualStyleBackColor = True
        '
        'txtInventoryPrefix
        '
        Me.txtInventoryPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInventoryPrefix.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInventoryPrefix.Location = New System.Drawing.Point(188, 252)
        Me.txtInventoryPrefix.MaxLength = 2
        Me.txtInventoryPrefix.Name = "txtInventoryPrefix"
        Me.txtInventoryPrefix.Size = New System.Drawing.Size(132, 21)
        Me.txtInventoryPrefix.TabIndex = 9
        Me.txtInventoryPrefix.Tag = "ENN;"
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(16, 255)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(224, 23)
        Me.Label10.TabIndex = 44
        Me.Label10.Text = "Inventory Prefix"
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(16, 133)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(110, 24)
        Me.Label9.TabIndex = 42
        Me.Label9.Text = "Size Factor"
        '
        'chkSize
        '
        Me.chkSize.AutoSize = True
        Me.chkSize.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkSize.Location = New System.Drawing.Point(188, 132)
        Me.chkSize.Name = "chkSize"
        Me.chkSize.Size = New System.Drawing.Size(15, 14)
        Me.chkSize.TabIndex = 41
        Me.chkSize.UseVisualStyleBackColor = True
        '
        'txtToInventoryCode
        '
        Me.txtToInventoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToInventoryCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToInventoryCode.Location = New System.Drawing.Point(188, 192)
        Me.txtToInventoryCode.MaxLength = 20
        Me.txtToInventoryCode.Name = "txtToInventoryCode"
        Me.txtToInventoryCode.Size = New System.Drawing.Size(132, 21)
        Me.txtToInventoryCode.TabIndex = 7
        Me.txtToInventoryCode.Tag = "ENN;"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(16, 195)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(216, 23)
        Me.Label8.TabIndex = 40
        Me.Label8.Text = "To Inventory Code"
        '
        'txtFromInventoryCode
        '
        Me.txtFromInventoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFromInventoryCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromInventoryCode.Location = New System.Drawing.Point(188, 161)
        Me.txtFromInventoryCode.MaxLength = 20
        Me.txtFromInventoryCode.Name = "txtFromInventoryCode"
        Me.txtFromInventoryCode.Size = New System.Drawing.Size(132, 21)
        Me.txtFromInventoryCode.TabIndex = 6
        Me.txtFromInventoryCode.Tag = "ENN;"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(16, 164)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(224, 23)
        Me.Label7.TabIndex = 38
        Me.Label7.Text = "From Inventory Code"
        '
        'btnClassSearch
        '
        Me.btnClassSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClassSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClassSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClassSearch.Location = New System.Drawing.Point(311, 70)
        Me.btnClassSearch.Name = "btnClassSearch"
        Me.btnClassSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnClassSearch.TabIndex = 4
        Me.btnClassSearch.Text = "..."
        Me.btnClassSearch.UseVisualStyleBackColor = False
        '
        'txtClassCode
        '
        Me.txtClassCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtClassCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClassCode.Location = New System.Drawing.Point(188, 70)
        Me.txtClassCode.MaxLength = 20
        Me.txtClassCode.Name = "txtClassCode"
        Me.txtClassCode.Size = New System.Drawing.Size(148, 21)
        Me.txtClassCode.TabIndex = 3
        Me.txtClassCode.Tag = "NE;"
        '
        'txtClassName
        '
        Me.txtClassName.BackColor = System.Drawing.Color.White
        Me.txtClassName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtClassName.Enabled = False
        Me.txtClassName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClassName.Location = New System.Drawing.Point(188, 101)
        Me.txtClassName.MaxLength = 50
        Me.txtClassName.Name = "txtClassName"
        Me.txtClassName.ReadOnly = True
        Me.txtClassName.Size = New System.Drawing.Size(238, 21)
        Me.txtClassName.TabIndex = 5
        Me.txtClassName.Tag = ""
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(188, 281)
        Me.txtRemarks.MaxLength = 100
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(238, 21)
        Me.txtRemarks.TabIndex = 10
        '
        'txtNextInventoryCode
        '
        Me.txtNextInventoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNextInventoryCode.Enabled = False
        Me.txtNextInventoryCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNextInventoryCode.Location = New System.Drawing.Point(188, 223)
        Me.txtNextInventoryCode.MaxLength = 20
        Me.txtNextInventoryCode.Name = "txtNextInventoryCode"
        Me.txtNextInventoryCode.Size = New System.Drawing.Size(132, 21)
        Me.txtNextInventoryCode.TabIndex = 8
        Me.txtNextInventoryCode.Tag = "ENN;"
        '
        'txtCategoryCode
        '
        Me.txtCategoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCategoryCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCategoryCode.Location = New System.Drawing.Point(188, 8)
        Me.txtCategoryCode.MaxLength = 20
        Me.txtCategoryCode.Name = "txtCategoryCode"
        Me.txtCategoryCode.Size = New System.Drawing.Size(142, 21)
        Me.txtCategoryCode.TabIndex = 0
        Me.txtCategoryCode.Tag = "NE;"
        '
        'txtCategoryName
        '
        Me.txtCategoryName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCategoryName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCategoryName.Location = New System.Drawing.Point(188, 39)
        Me.txtCategoryName.MaxLength = 50
        Me.txtCategoryName.Name = "txtCategoryName"
        Me.txtCategoryName.Size = New System.Drawing.Size(238, 21)
        Me.txtCategoryName.TabIndex = 1
        Me.txtCategoryName.Tag = "NE;"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(16, 284)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(208, 23)
        Me.Label6.TabIndex = 33
        Me.Label6.Text = "Remarks"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(16, 226)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(224, 23)
        Me.Label5.TabIndex = 32
        Me.Label5.Text = "Next Inventory Code"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(16, 103)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(110, 24)
        Me.Label4.TabIndex = 28
        Me.Label4.Text = "Class Name"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 72)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(110, 23)
        Me.Label3.TabIndex = 27
        Me.Label3.Text = "Class Code"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(208, 23)
        Me.Label2.TabIndex = 26
        Me.Label2.Text = "Category Name"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(208, 23)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "Category Code"
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(6, 2)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 100)
        Me.pic.TabIndex = 85
        Me.pic.TabStop = False
        '
        'grdCategory
        '
        Me.grdCategory.AllowUserToAddRows = False
        Me.grdCategory.AllowUserToDeleteRows = False
        Me.grdCategory.AllowUserToResizeColumns = False
        Me.grdCategory.AllowUserToResizeRows = False
        Me.grdCategory.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdCategory.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdCategory.ColumnHeadersHeight = 30
        Me.grdCategory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdCategory.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cCategoryCode, Me.cCategoryName, Me.cClassCode, Me.cFromInv, Me.cToInv, Me.cNextInv, Me.cRemarks, Me.cSize, Me.cInvPrefix, Me.cShowInReceipt})
        Me.grdCategory.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdCategory.Location = New System.Drawing.Point(6, 114)
        Me.grdCategory.Name = "grdCategory"
        Me.grdCategory.RowHeadersVisible = False
        Me.grdCategory.Size = New System.Drawing.Size(626, 354)
        Me.grdCategory.TabIndex = 86
        '
        'cCategoryCode
        '
        Me.cCategoryCode.HeaderText = "Category Code"
        Me.cCategoryCode.Name = "cCategoryCode"
        Me.cCategoryCode.Width = 130
        '
        'cCategoryName
        '
        Me.cCategoryName.HeaderText = "Category Name"
        Me.cCategoryName.Name = "cCategoryName"
        Me.cCategoryName.Width = 200
        '
        'cClassCode
        '
        Me.cClassCode.HeaderText = "ClassCode"
        Me.cClassCode.Name = "cClassCode"
        Me.cClassCode.Visible = False
        '
        'cFromInv
        '
        Me.cFromInv.HeaderText = "From Inventory"
        Me.cFromInv.Name = "cFromInv"
        Me.cFromInv.Width = 150
        '
        'cToInv
        '
        Me.cToInv.HeaderText = "To Inventory"
        Me.cToInv.Name = "cToInv"
        Me.cToInv.Width = 150
        '
        'cNextInv
        '
        Me.cNextInv.HeaderText = "Next Inventory"
        Me.cNextInv.Name = "cNextInv"
        Me.cNextInv.Visible = False
        '
        'cRemarks
        '
        Me.cRemarks.HeaderText = "Remarks"
        Me.cRemarks.Name = "cRemarks"
        Me.cRemarks.Visible = False
        '
        'cSize
        '
        Me.cSize.HeaderText = "Size"
        Me.cSize.Name = "cSize"
        Me.cSize.Visible = False
        '
        'cInvPrefix
        '
        Me.cInvPrefix.HeaderText = "Inventory Prefix"
        Me.cInvPrefix.Name = "cInvPrefix"
        Me.cInvPrefix.Visible = False
        '
        'cShowInReceipt
        '
        Me.cShowInReceipt.HeaderText = "ShowInReport"
        Me.cShowInReceipt.Name = "cShowInReceipt"
        Me.cShowInReceipt.Visible = False
        '
        'frmCategory
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(7, 14)
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(1091, 543)
        Me.Controls.Add(Me.grdCategory)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnDelete)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCategory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Category"
        CType(Me.ErrP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCategory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Obrevations"
    ' string variables starts with "s"
    ' dobule and money variables starts with "d"
    ' boolean variables starts with "b"
    ' counter variables ends with "Cnt"
    ' dataset variables starts with "ds"
#End Region

    Const CategoryCode = 1
    Const CategoryName = 2
    Const ClassCode = 3
    Const FromInventoryCode = 4
    Const ToInventoryCode = 5
    Const NextInventoryCode = 6
    Const Remarks = 7
    Const Size = 8
    Const InventoryPrefix = 9
    Const ShowinReceipt = 10
    Dim objClsCategory As New ClsCategory
    Dim objClsClass As New ClsClass

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnClassSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClassSearch.Click
        Try
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblClass", "L", 4, "ClassCode", "", "ClassCode", "ClassCode", 80, "ClassName", "ClassName", 180, "Rate", "Rate", 60, "Remarks", "Remarks", 80)
            frmHelpClass.ShowDialog()
            txtClassCode.Text = frmHelpClass.Var1.Trim
            txtClassName.Text = frmHelpClass.Var2.Trim
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmCategory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'subGridSetting(grdCategory)
        GetCategoryDetails()
        'ShowCategoryDetail(grdCategory.ActiveRow)
        grdCategory.Focus()
        chkSize.Enabled = False
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        ClearMe()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sSize, sShowinReport As String
        Dim sFInventoryCode, sTInventorycode, sNInventoryCode As String
        If chkSize.Checked Then
            sSize = "1"
        Else
            sSize = "0"
        End If
        If chkShowInReceipt.Checked Then
            sShowinReport = "1"
        Else
            sShowinReport = "0"
        End If
        sFInventoryCode = txtInventoryPrefix.Text + txtFromInventoryCode.Text.Trim
        sTInventorycode = txtInventoryPrefix.Text + txtToInventoryCode.Text.Trim
        sNInventoryCode = txtInventoryPrefix.Text + txtNextInventoryCode.Text.Trim

        Try
            If ValidateMe() Then
                'If objClsCategory.CategorySave(txtCategoryCode.Text.Trim, txtCategoryName.Text.Trim, txtClassCode.Text.Trim, Val(txtFromInventoryCode.Text.Trim), Val(txtToInventoryCode.Text.Trim), Val(txtNextInventoryCode.Text.Trim), txtRemarks.Text.Trim, sSize) Then
                If objClsCategory.CategorySave(txtCategoryCode.Text.Trim, txtCategoryName.Text.Trim, txtClassCode.Text.Trim, Trim(sFInventoryCode), Trim(sTInventorycode), Trim(sNInventoryCode), txtRemarks.Text.Trim, sSize, Trim(txtInventoryPrefix.Text.Trim), Trim(sShowinReport)) Then
                    MessageBox.Show("Category detail saved sucssfully.")
                    ClearMe()
                    GetCategoryDetails()
                    grdCategory.Focus()
                    'ShowCategoryDetail(grdCategory.ActiveRow)
                Else
                    MessageBox.Show("Error in Save")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If grdCategory.RowCount > 0 And grdCategory.CurrentRow.Index > 0 Then
                Dim sCategoryCode As String
                sCategoryCode = fnGetdgvText(grdCategory, grdCategory.CurrentRow.Index, CategoryCode)
                If fncGetCategoryTran(sCategoryCode) = True Then
                    MsgBox("Can Not Delete Category , Beacuse This Category Is Having Some Inventory", MsgBoxStyle.Information)
                    Exit Sub
                Else
                    If MessageBox.Show("Do you want to delete " & fnGetdgvText(grdCategory, grdCategory.CurrentRow.Index, CategoryCode) & " Category?", "GoldPOS", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        If objClsCategory.DeleteCategroy(sCategoryCode) Then
                            MessageBox.Show("Category detail deleted.")
                            ClearMe()
                            GetCategoryDetails()
                            grdCategory.Focus()
                            'ShowCategoryDetail(grdCategory.ActiveRow)
                        Else
                            MessageBox.Show("Error in deleting Category detail.")
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Function fncGetCategoryTran(ByVal sCode As String) As Boolean
        Dim DvCustomer() As DataRow
        Dim DM As New DataManager
        fncGetCategoryTran = False
        DvCustomer = DM.RowReturnProjExecNonQryWithDataRow("Select * from tblInventory where CategoryCode=" & fncFormatSQLText(sCode))
        If DvCustomer.Length > 0 Then
            fncGetCategoryTran = True
        Else
            fncGetCategoryTran = False
        End If
    End Function

    Private Sub frmCategory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F6
                btnNew_Click(sender, e)
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

    Private Sub txtCategoryCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtCategoryCode.Validating
        If txtCategoryCode.Text.Trim = "" Then
            ErrP.SetError(txtCategoryCode, "Should not be empty.")
            txtCategoryCode.Focus()
        Else
            ErrP.Dispose()
        End If
    End Sub

    Private Sub txtCategoryName_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtCategoryName.Validating
        If txtCategoryName.Text.Trim = "" Then
            ErrP.SetError(txtCategoryName, "Should not be empty.")
            txtCategoryName.Focus()
        Else
            ErrP.Dispose()
        End If
    End Sub

    Private Sub txtClassCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtClassCode.Validating
        If txtClassCode.Text.Trim = "" Then
            ErrP.SetError(txtClassCode, "Should not be empty.")
            txtClassCode.Focus()
        Else
            ErrP.Dispose()
        End If
    End Sub

    Private Sub txtFromInventoryCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtFromInventoryCode.Validating
        If txtFromInventoryCode.Text.Trim = "" Then
            ErrP.SetError(txtFromInventoryCode, "Should not be empty.")
            txtFromInventoryCode.Focus()
            'ElseIf Not IsNumeric(txtFromInventoryCode.Text.Trim) Then
            '    ErrP.SetError(txtFromInventoryCode, "Should be Numeric.")
            '    txtFromInventoryCode.Focus()
        Else
            ErrP.Dispose()
        End If
    End Sub

    Private Sub txtToInventoryCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtToInventoryCode.Validating
        If txtToInventoryCode.Text.Trim = "" Then
            ErrP.SetError(txtToInventoryCode, "Should not be empty.")
            txtToInventoryCode.Focus()
            'ElseIf Not IsNumeric(txtToInventoryCode.Text.Trim) Then
            '    ErrP.SetError(txtToInventoryCode, "Should be Numeric.")
            '    txtToInventoryCode.Focus()
        Else
            ErrP.Dispose()
        End If
    End Sub

    Private Sub txtNextInventoryCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtNextInventoryCode.Validating
        If txtNextInventoryCode.Text.Trim = "" Then
            ErrP.SetError(txtNextInventoryCode, "Should not be empty.")
            txtNextInventoryCode.Focus()
            'ElseIf Not IsNumeric(txtNextInventoryCode.Text.Trim) Then
            '    ErrP.SetError(txtNextInventoryCode, "Should be Numeric.")
            '    txtNextInventoryCode.Focus()
        Else
            ErrP.Dispose()
        End If
    End Sub

    Private Sub txtClassCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtClassCode.LostFocus
        Dim sClassName As String
        Try
            If txtClassCode.Text.Trim <> "" Then
                sClassName = objClsClass.GetClassName(txtClassCode.Text.Trim)
                If Not IsNothing(sClassName.Trim) Or sClassName.Trim.Length > 0 Then
                    txtClassName.Text = sClassName
                Else
                    txtClassCode.Clear()
                    txtClassName.Clear()
                    txtClassCode.Focus()
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub GetCategoryDetails()
        Try
            Dim dsClass As New DataSet
            Dim iCnt As Long
            dsClass = objClsCategory.GetCategory
            If Not IsNothing(dsClass) Then
                If dsClass.Tables("Category").Rows.Count > 0 Then
                    grdCategory.RowCount = 0
                    For iCnt = 0 To dsClass.Tables("Category").Rows.Count - 1
                        grdCategory.Rows.Add()
                        fnSetdgvText(grdCategory, iCnt, 0, Trim(dsClass.Tables("Category").Rows(iCnt)("CategoryCode") & ""))
                        fnSetdgvText(grdCategory, iCnt, 1, Trim(dsClass.Tables("Category").Rows(iCnt)("CategoryName") & ""))

                        fnSetdgvText(grdCategory, iCnt, 2, Trim(dsClass.Tables("Category").Rows(iCnt)("ClassCode") & ""))
                        fnSetdgvText(grdCategory, iCnt, 3, Trim(dsClass.Tables("Category").Rows(iCnt)("FromInventoryCode") & ""))

                        fnSetdgvText(grdCategory, iCnt, 4, Trim(dsClass.Tables("Category").Rows(iCnt)("ToInventoryCode") & ""))
                        fnSetdgvText(grdCategory, iCnt, 5, Trim(dsClass.Tables("Category").Rows(iCnt)("NextInventoryCode") & ""))


                        fnSetdgvText(grdCategory, iCnt, 6, Trim(dsClass.Tables("Category").Rows(iCnt)("Remarks") & ""))
                        fnSetdgvText(grdCategory, iCnt, 7, Trim(dsClass.Tables("Category").Rows(iCnt)("SizeFactor") & ""))


                        fnSetdgvText(grdCategory, iCnt, 8, Trim(dsClass.Tables("Category").Rows(iCnt)("InventoryPrefix") & ""))
                        fnSetdgvText(grdCategory, iCnt, 9, Trim(dsClass.Tables("Category").Rows(iCnt)("ShowinReceipt") & ""))
                    Next
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Function GetDetailsFromGrid()
        Dim sCategoryCode, sCategoryName, sClassCode, sFromInventory, sToInventory, sNextInventory, sSizeFactor, sInventoryPrefix, sRemarks, sShowinReceipt As String

        btnDelete.Enabled = True
        sCategoryCode = fnGetdgvText(grdCategory, grdCategory.CurrentRow.Index, cCategoryCode.Index)
        sCategoryName = fnGetdgvText(grdCategory, grdCategory.CurrentRow.Index, cCategoryName.Index)

        sFromInventory = fnGetdgvText(grdCategory, grdCategory.CurrentRow.Index, cFromInv.Index)
        sToInventory = fnGetdgvText(grdCategory, grdCategory.CurrentRow.Index, cToInv.Index)

        sNextInventory = fnGetdgvText(grdCategory, grdCategory.CurrentRow.Index, cNextInv.Index)

        sSizeFactor = fnGetdgvText(grdCategory, grdCategory.CurrentRow.Index, cSize.Index)
        sInventoryPrefix = fnGetdgvText(grdCategory, grdCategory.CurrentRow.Index, cInvPrefix.Index)
        sClassCode = fnGetdgvText(grdCategory, grdCategory.CurrentRow.Index, cClassCode.Index)

        sRemarks = fnGetdgvText(grdCategory, grdCategory.CurrentRow.Index, cRemarks.Index)

        sShowinReceipt = fnGetdgvText(grdCategory, grdCategory.CurrentRow.Index, cShowInReceipt.Index)

        txtCategoryCode.Text = sCategoryCode
        txtCategoryName.Text = sCategoryName
        txtFromInventoryCode.Text = sFromInventory
        txtToInventoryCode.Text = sToInventory
        txtNextInventoryCode.Text = sNextInventory
        txtClassCode.Text = sClassCode
        txtInventoryPrefix.Text = sInventoryPrefix
        txtRemarks.Text = sRemarks
        If sSizeFactor = "1" Then
            chkSize.Checked = True
        Else
            chkSize.Checked = False
        End If
        If sShowinReceipt = "1" Then
            chkShowInReceipt.Checked = True
        Else
            chkShowInReceipt.Checked = False
        End If
        txtClassCode_LostFocus(Nothing, Nothing)

    End Function

    Private Sub ShowCategoryDetail(ByVal Row As Integer)
        Try
            btnDelete.Enabled = True
            If Row > 0 Then
                txtCategoryCode.Text = GetText(grdCategory, Row, CategoryCode)
                txtCategoryCode.ReadOnly = True
                txtCategoryName.Text = GetText(grdCategory, Row, CategoryName)
                txtClassCode.Text = GetText(grdCategory, Row, ClassCode)
                txtClassName.Text = objClsClass.GetClassName(txtClassCode.Text.Trim).Trim
                txtFromInventoryCode.Text = GetText(grdCategory, Row, FromInventoryCode)
                txtToInventoryCode.Text = GetText(grdCategory, Row, ToInventoryCode)
                txtNextInventoryCode.Text = GetText(grdCategory, Row, NextInventoryCode)
                txtRemarks.Text = GetText(grdCategory, Row, Remarks)
                chkSize.Checked = GetText(grdCategory, Row, Size)
                txtInventoryPrefix.Text = GetText(grdCategory, Row, InventoryPrefix)
                chkShowInReceipt.Checked = GetText(grdCategory, Row, ShowinReceipt)
                chkSize.Enabled = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtClassCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtClassCode.KeyDown
        If e.KeyCode = Keys.F1 Then btnClassSearch_Click(sender, e)
    End Sub

    Private Sub grdCategory_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_ClickEvent)
        ShowCategoryDetail(e.row)
    End Sub

    Private Sub grdCategory_LeaveRow(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_LeaveRowEvent)
        ShowCategoryDetail(e.newRow)
    End Sub
    Private Sub ClearMe()
        txtCategoryCode.Clear()
        txtCategoryName.Clear()
        txtClassCode.Clear()
        txtClassName.Clear()
        txtFromInventoryCode.Clear()
        txtToInventoryCode.Clear()
        txtNextInventoryCode.Clear()
        txtRemarks.Clear()
        btnDelete.Enabled = False
        txtCategoryCode.ReadOnly = False
        txtCategoryCode.Focus()
        chkSize.Checked = False
        chkSize.Enabled = True
        txtInventoryPrefix.Clear()
        txtCategoryCode.Focus()
        chkShowInReceipt.Checked = False
        chkShowInReceipt.Enabled = True
    End Sub

#Region "Validation"

    Private Function ValidateMe() As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            If txtCategoryCode.Text.Trim = "" Then
                MessageBox.Show("CategoryCode should not be blank.")
                txtCategoryCode.Focus()
                Exit Function
            End If
            If txtCategoryName.Text.Trim = "" Then
                MessageBox.Show("CategoryName should not be blank.")
                txtCategoryName.Focus()
                Exit Function
            End If
            If txtClassCode.Text.Trim = "" Then
                MessageBox.Show("ClassCode should not be blank.")
                txtClassCode.Focus()
                Exit Function
            End If
            'check valid classcode
            If IsNothing(objClsClass.GetClassName(txtClassCode.Text.Trim)) Then
                MessageBox.Show("Pleae enter valid ClassCode.")
                txtClassCode.Clear()
                txtClassCode.Focus()
                Exit Function
            End If
            If txtFromInventoryCode.Text.Trim = "" Then
                MessageBox.Show("From Inventory code should not be blank.")
                txtFromInventoryCode.Focus()
                Exit Function
                'ElseIf Not IsNumeric(txtFromInventoryCode.Text.Trim) Then
                '    MessageBox.Show("From Inventory code should be Numeric.")
                '    txtFromInventoryCode.Focus()
                '    Exit Function
            End If
            If txtToInventoryCode.Text.Trim = "" Then
                MessageBox.Show("To Inventory code should not be blank.")
                txtToInventoryCode.Focus()
                Exit Function
                'ElseIf Not IsNumeric(txtToInventoryCode.Text.Trim) Then
                '    MessageBox.Show("To Inventory code should be Numeric.")
                '    txtToInventoryCode.Focus()
                '    Exit Function
            End If
            If Val(txtToInventoryCode.Text) < Val(txtFromInventoryCode.Text) Then
                MessageBox.Show("ToInventoryCode should be greater than FromInventoryCode.")
                txtToInventoryCode.Focus()
                Exit Function
            End If
            If objClsCategory.GetInventoryRange(txtFromInventoryCode.Text.Trim, txtInventoryPrefix.Text.Trim) Or objClsCategory.GetInventoryRange(txtToInventoryCode.Text.Trim, txtInventoryPrefix.Text.Trim) Then
                MessageBox.Show("This Inventory code range already exist.")
                txtFromInventoryCode.Focus()
                Exit Function
            End If
            '''If txtNextInventoryCode.Text.Trim = "" Then
            '''    MessageBox.Show("Next Inventory code should not be blank.")
            '''    txtNextInventoryCode.Focus()
            '''    Exit Function
            '''ElseIf Not IsNumeric(txtNextInventoryCode.Text.Trim) Then
            '''    MessageBox.Show("Next Inventory code should be Numeric.")
            '''    txtNextInventoryCode.Focus()
            '''    Exit Function
            '''ElseIf Not (Val(txtNextInventoryCode.Text) >= Val(txtFromInventoryCode.Text) And Val(txtNextInventoryCode.Text) <= Val(txtToInventoryCode.Text)) Then
            '''    MessageBox.Show("Next inventorycode should be greaterthan From InventoryCode and Less than To InventoryCode.")
            '''    txtNextInventoryCode.Focus()
            '''    Exit Function
            '''End If
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            bStatus = False
        Finally
            ValidateMe = bStatus
        End Try

    End Function

#End Region

    Private Sub txtClassCode_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtClassCode.TextChanged

    End Sub

    Private Sub grdCategory_Click(sender As Object, e As System.EventArgs) Handles grdCategory.Click
        GetDetailsFromGrid()
    End Sub

    Private Sub grdCategory_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCategory.CellContentClick

    End Sub

    Private Sub txtFromInventoryCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFromInventoryCode.TextChanged

    End Sub
End Class
