<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReOrderLevelMaster
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReOrderLevelMaster))
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnToSizeCodeSearch = New System.Windows.Forms.Button()
        Me.txtToSizeCode = New System.Windows.Forms.TextBox()
        Me.cboLocationCode = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtToWeight = New System.Windows.Forms.TextBox()
        Me.txtReOrderLevel = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtFromWeight = New System.Windows.Forms.TextBox()
        Me.btnSizeCodeSearch = New System.Windows.Forms.Button()
        Me.btnCatalogueSearch = New System.Windows.Forms.Button()
        Me.txtSizeCode = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCatalogueCode = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnCatagorySearch = New System.Windows.Forms.Button()
        Me.txtCategoryCode = New System.Windows.Forms.TextBox()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.grdCatalogue = New System.Windows.Forms.DataGridView()
        Me.cLocationCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cCategoryCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cCatalogueCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cFromWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cToWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cSizeCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cSizeType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cToSizeCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cToSizeType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cReOrderLevel = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cRemarks = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdCatalogue, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(2, 3)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 100)
        Me.pic.TabIndex = 83
        Me.pic.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.btnToSizeCodeSearch)
        Me.GroupBox1.Controls.Add(Me.txtToSizeCode)
        Me.GroupBox1.Controls.Add(Me.cboLocationCode)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtToWeight)
        Me.GroupBox1.Controls.Add(Me.txtReOrderLevel)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtRemarks)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtFromWeight)
        Me.GroupBox1.Controls.Add(Me.btnSizeCodeSearch)
        Me.GroupBox1.Controls.Add(Me.btnCatalogueSearch)
        Me.GroupBox1.Controls.Add(Me.txtSizeCode)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtCatalogueCode)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnCatagorySearch)
        Me.GroupBox1.Controls.Add(Me.txtCategoryCode)
        Me.GroupBox1.Location = New System.Drawing.Point(479, 104)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(338, 408)
        Me.GroupBox1.TabIndex = 84
        Me.GroupBox1.TabStop = False
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(15, 291)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(122, 23)
        Me.Label8.TabIndex = 530
        Me.Label8.Text = "To Size"
        '
        'btnToSizeCodeSearch
        '
        Me.btnToSizeCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnToSizeCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnToSizeCodeSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnToSizeCodeSearch.Location = New System.Drawing.Point(301, 290)
        Me.btnToSizeCodeSearch.Name = "btnToSizeCodeSearch"
        Me.btnToSizeCodeSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnToSizeCodeSearch.TabIndex = 529
        Me.btnToSizeCodeSearch.Text = "..."
        Me.btnToSizeCodeSearch.UseVisualStyleBackColor = False
        '
        'txtToSizeCode
        '
        Me.txtToSizeCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToSizeCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToSizeCode.ForeColor = System.Drawing.Color.Black
        Me.txtToSizeCode.Location = New System.Drawing.Point(143, 291)
        Me.txtToSizeCode.MaxLength = 20
        Me.txtToSizeCode.Name = "txtToSizeCode"
        Me.txtToSizeCode.Size = New System.Drawing.Size(183, 20)
        Me.txtToSizeCode.TabIndex = 7
        Me.txtToSizeCode.Tag = ""
        '
        'cboLocationCode
        '
        Me.cboLocationCode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLocationCode.Items.AddRange(New Object() {"ALL"})
        Me.cboLocationCode.Location = New System.Drawing.Point(143, 19)
        Me.cboLocationCode.Name = "cboLocationCode"
        Me.cboLocationCode.Size = New System.Drawing.Size(99, 21)
        Me.cboLocationCode.TabIndex = 1
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label20.Location = New System.Drawing.Point(17, 22)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(86, 13)
        Me.Label20.TabIndex = 526
        Me.Label20.Text = "Location Code"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(15, 203)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(122, 23)
        Me.Label5.TabIndex = 525
        Me.Label5.Text = "To Weight"
        '
        'txtToWeight
        '
        Me.txtToWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToWeight.ForeColor = System.Drawing.Color.Black
        Me.txtToWeight.Location = New System.Drawing.Point(143, 203)
        Me.txtToWeight.MaxLength = 20
        Me.txtToWeight.Name = "txtToWeight"
        Me.txtToWeight.Size = New System.Drawing.Size(183, 20)
        Me.txtToWeight.TabIndex = 5
        Me.txtToWeight.Tag = ""
        '
        'txtReOrderLevel
        '
        Me.txtReOrderLevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReOrderLevel.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReOrderLevel.Location = New System.Drawing.Point(143, 329)
        Me.txtReOrderLevel.MaxLength = 100
        Me.txtReOrderLevel.Name = "txtReOrderLevel"
        Me.txtReOrderLevel.Size = New System.Drawing.Size(183, 21)
        Me.txtReOrderLevel.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(15, 331)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(103, 23)
        Me.Label4.TabIndex = 93
        Me.Label4.Text = "ReOrder level"
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(143, 371)
        Me.txtRemarks.MaxLength = 100
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(183, 21)
        Me.txtRemarks.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(15, 373)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(103, 23)
        Me.Label6.TabIndex = 91
        Me.Label6.Text = "Remarks"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(15, 150)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(122, 23)
        Me.Label3.TabIndex = 523
        Me.Label3.Text = "From Weight"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(17, 252)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(122, 23)
        Me.Label2.TabIndex = 521
        Me.Label2.Text = "From Size"
        '
        'txtFromWeight
        '
        Me.txtFromWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFromWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromWeight.ForeColor = System.Drawing.Color.Black
        Me.txtFromWeight.Location = New System.Drawing.Point(143, 150)
        Me.txtFromWeight.MaxLength = 20
        Me.txtFromWeight.Name = "txtFromWeight"
        Me.txtFromWeight.Size = New System.Drawing.Size(183, 20)
        Me.txtFromWeight.TabIndex = 4
        Me.txtFromWeight.Tag = ""
        '
        'btnSizeCodeSearch
        '
        Me.btnSizeCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSizeCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSizeCodeSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSizeCodeSearch.Location = New System.Drawing.Point(303, 251)
        Me.btnSizeCodeSearch.Name = "btnSizeCodeSearch"
        Me.btnSizeCodeSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnSizeCodeSearch.TabIndex = 520
        Me.btnSizeCodeSearch.Text = "..."
        Me.btnSizeCodeSearch.UseVisualStyleBackColor = False
        '
        'btnCatalogueSearch
        '
        Me.btnCatalogueSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCatalogueSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCatalogueSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCatalogueSearch.Location = New System.Drawing.Point(303, 99)
        Me.btnCatalogueSearch.Name = "btnCatalogueSearch"
        Me.btnCatalogueSearch.Size = New System.Drawing.Size(23, 21)
        Me.btnCatalogueSearch.TabIndex = 93
        Me.btnCatalogueSearch.Text = "..."
        Me.btnCatalogueSearch.UseVisualStyleBackColor = False
        '
        'txtSizeCode
        '
        Me.txtSizeCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSizeCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSizeCode.ForeColor = System.Drawing.Color.Black
        Me.txtSizeCode.Location = New System.Drawing.Point(145, 252)
        Me.txtSizeCode.MaxLength = 20
        Me.txtSizeCode.Name = "txtSizeCode"
        Me.txtSizeCode.Size = New System.Drawing.Size(183, 20)
        Me.txtSizeCode.TabIndex = 6
        Me.txtSizeCode.Tag = ""
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(15, 101)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(122, 23)
        Me.Label7.TabIndex = 90
        Me.Label7.Text = "Catalouge Code"
        '
        'txtCatalogueCode
        '
        Me.txtCatalogueCode.BackColor = System.Drawing.Color.White
        Me.txtCatalogueCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCatalogueCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCatalogueCode.Location = New System.Drawing.Point(143, 99)
        Me.txtCatalogueCode.MaxLength = 20
        Me.txtCatalogueCode.Name = "txtCatalogueCode"
        Me.txtCatalogueCode.Size = New System.Drawing.Size(162, 21)
        Me.txtCatalogueCode.TabIndex = 3
        Me.txtCatalogueCode.Tag = "NE;"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 54)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(122, 23)
        Me.Label1.TabIndex = 90
        Me.Label1.Text = "Category Code"
        '
        'btnCatagorySearch
        '
        Me.btnCatagorySearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCatagorySearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCatagorySearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCatagorySearch.Location = New System.Drawing.Point(303, 54)
        Me.btnCatagorySearch.Name = "btnCatagorySearch"
        Me.btnCatagorySearch.Size = New System.Drawing.Size(23, 21)
        Me.btnCatagorySearch.TabIndex = 91
        Me.btnCatagorySearch.Text = "..."
        Me.btnCatagorySearch.UseVisualStyleBackColor = False
        '
        'txtCategoryCode
        '
        Me.txtCategoryCode.BackColor = System.Drawing.Color.White
        Me.txtCategoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCategoryCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCategoryCode.Location = New System.Drawing.Point(143, 54)
        Me.txtCategoryCode.MaxLength = 20
        Me.txtCategoryCode.Name = "txtCategoryCode"
        Me.txtCategoryCode.Size = New System.Drawing.Size(162, 21)
        Me.txtCategoryCode.TabIndex = 2
        Me.txtCategoryCode.Tag = "NE;"
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnNew.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Location = New System.Drawing.Point(371, 518)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(100, 40)
        Me.btnNew.TabIndex = 86
        Me.btnNew.Text = "New(F6)"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(719, 518)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 88
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(490, 518)
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
        Me.btnDelete.Location = New System.Drawing.Point(609, 518)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(100, 40)
        Me.btnDelete.TabIndex = 87
        Me.btnDelete.Text = "Delete(F8)"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'grdCatalogue
        '
        Me.grdCatalogue.AllowUserToAddRows = False
        Me.grdCatalogue.AllowUserToDeleteRows = False
        Me.grdCatalogue.AllowUserToResizeColumns = False
        Me.grdCatalogue.AllowUserToResizeRows = False
        Me.grdCatalogue.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdCatalogue.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdCatalogue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdCatalogue.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cLocationCode, Me.cCategoryCode, Me.cCatalogueCode, Me.cFromWeight, Me.cToWeight, Me.cSizeCode, Me.cSizeType, Me.cToSizeCode, Me.cToSizeType, Me.cReOrderLevel, Me.cRemarks})
        Me.grdCatalogue.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdCatalogue.Location = New System.Drawing.Point(2, 109)
        Me.grdCatalogue.Name = "grdCatalogue"
        Me.grdCatalogue.RowHeadersVisible = False
        Me.grdCatalogue.Size = New System.Drawing.Size(471, 403)
        Me.grdCatalogue.TabIndex = 0
        '
        'cLocationCode
        '
        Me.cLocationCode.HeaderText = "Location Code"
        Me.cLocationCode.Name = "cLocationCode"
        '
        'cCategoryCode
        '
        Me.cCategoryCode.HeaderText = "Category Code"
        Me.cCategoryCode.Name = "cCategoryCode"
        Me.cCategoryCode.Width = 200
        '
        'cCatalogueCode
        '
        Me.cCatalogueCode.HeaderText = "Catalogue Code"
        Me.cCatalogueCode.Name = "cCatalogueCode"
        Me.cCatalogueCode.Width = 200
        '
        'cFromWeight
        '
        Me.cFromWeight.HeaderText = "From Weight"
        Me.cFromWeight.Name = "cFromWeight"
        '
        'cToWeight
        '
        Me.cToWeight.HeaderText = "To Weight"
        Me.cToWeight.Name = "cToWeight"
        '
        'cSizeCode
        '
        Me.cSizeCode.HeaderText = "SizeCode"
        Me.cSizeCode.Name = "cSizeCode"
        Me.cSizeCode.Width = 140
        '
        'cSizeType
        '
        Me.cSizeType.HeaderText = "SizeType"
        Me.cSizeType.Name = "cSizeType"
        Me.cSizeType.Visible = False
        '
        'cToSizeCode
        '
        Me.cToSizeCode.HeaderText = "ToSizeCode"
        Me.cToSizeCode.Name = "cToSizeCode"
        '
        'cToSizeType
        '
        Me.cToSizeType.HeaderText = "ToSizeType"
        Me.cToSizeType.Name = "cToSizeType"
        '
        'cReOrderLevel
        '
        Me.cReOrderLevel.HeaderText = "cReOrderLevel"
        Me.cReOrderLevel.Name = "cReOrderLevel"
        Me.cReOrderLevel.Visible = False
        '
        'cRemarks
        '
        Me.cRemarks.HeaderText = "Remarks"
        Me.cRemarks.Name = "cRemarks"
        Me.cRemarks.Visible = False
        '
        'frmReOrderLevelMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(831, 570)
        Me.Controls.Add(Me.grdCatalogue)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.pic)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmReOrderLevelMaster"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ReOrderLevel Master"
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdCatalogue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents grdCatalogue As System.Windows.Forms.DataGridView
    Friend WithEvents btnCatagorySearch As System.Windows.Forms.Button
    Friend WithEvents txtCategoryCode As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnCatalogueSearch As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtCatalogueCode As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtFromWeight As System.Windows.Forms.TextBox
    Friend WithEvents btnSizeCodeSearch As System.Windows.Forms.Button
    Friend WithEvents txtSizeCode As System.Windows.Forms.TextBox
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtReOrderLevel As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cSize As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtToWeight As System.Windows.Forms.TextBox
    Friend WithEvents cboLocationCode As System.Windows.Forms.ComboBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnToSizeCodeSearch As System.Windows.Forms.Button
    Friend WithEvents txtToSizeCode As System.Windows.Forms.TextBox
    Friend WithEvents cLocationCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCategoryCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCatalogueCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cFromWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cToWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSizeCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSizeType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cToSizeCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cToSizeType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cReOrderLevel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cRemarks As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
