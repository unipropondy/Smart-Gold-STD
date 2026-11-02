<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSalesOrder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSalesOrder))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.grpSearch = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnSupplierCodeSearch = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtSupplierCode = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtIYYYY = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtIMM = New System.Windows.Forms.TextBox()
        Me.btnFetch = New System.Windows.Forms.Button()
        Me.txtIDD = New System.Windows.Forms.TextBox()
        Me.txtSONo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboLocationCode = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtCatalogue = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtCategory = New System.Windows.Forms.TextBox()
        Me.btnCatagorySearch = New System.Windows.Forms.Button()
        Me.btnCatalogueSearch = New System.Windows.Forms.Button()
        Me.grdCatalogue = New System.Windows.Forms.DataGridView()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnRePrint = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.cSel = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.cCategoryCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cCatalogueCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cToWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cSizeCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cSizeType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cToSizeCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cToSizeType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cQtyonHand = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cReOrderLevel = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cSOQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpSearch.SuspendLayout()
        CType(Me.grdCatalogue, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(3, 2)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 100)
        Me.pic.TabIndex = 84
        Me.pic.TabStop = False
        '
        'grpSearch
        '
        Me.grpSearch.Controls.Add(Me.Label2)
        Me.grpSearch.Controls.Add(Me.Label1)
        Me.grpSearch.Controls.Add(Me.btnSupplierCodeSearch)
        Me.grpSearch.Controls.Add(Me.Label4)
        Me.grpSearch.Controls.Add(Me.txtSupplierCode)
        Me.grpSearch.Controls.Add(Me.Label5)
        Me.grpSearch.Controls.Add(Me.txtRemarks)
        Me.grpSearch.Controls.Add(Me.txtIYYYY)
        Me.grpSearch.Controls.Add(Me.Label3)
        Me.grpSearch.Controls.Add(Me.txtIMM)
        Me.grpSearch.Controls.Add(Me.btnFetch)
        Me.grpSearch.Controls.Add(Me.txtIDD)
        Me.grpSearch.Controls.Add(Me.txtSONo)
        Me.grpSearch.Controls.Add(Me.Label7)
        Me.grpSearch.Controls.Add(Me.cboLocationCode)
        Me.grpSearch.Controls.Add(Me.Label20)
        Me.grpSearch.Controls.Add(Me.Label12)
        Me.grpSearch.Controls.Add(Me.txtCatalogue)
        Me.grpSearch.Controls.Add(Me.Label11)
        Me.grpSearch.Controls.Add(Me.txtCategory)
        Me.grpSearch.Controls.Add(Me.btnCatagorySearch)
        Me.grpSearch.Controls.Add(Me.btnCatalogueSearch)
        Me.grpSearch.Location = New System.Drawing.Point(12, 108)
        Me.grpSearch.Name = "grpSearch"
        Me.grpSearch.Size = New System.Drawing.Size(773, 132)
        Me.grpSearch.TabIndex = 493
        Me.grpSearch.TabStop = False
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(657, 59)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(11, 16)
        Me.Label2.TabIndex = 507
        Me.Label2.Text = "-"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label1.Location = New System.Drawing.Point(6, 107)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 13)
        Me.Label1.TabIndex = 506
        Me.Label1.Text = "Remarks"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnSupplierCodeSearch
        '
        Me.btnSupplierCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSupplierCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSupplierCodeSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSupplierCodeSearch.ForeColor = System.Drawing.Color.Black
        Me.btnSupplierCodeSearch.Location = New System.Drawing.Point(692, 17)
        Me.btnSupplierCodeSearch.Name = "btnSupplierCodeSearch"
        Me.btnSupplierCodeSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnSupplierCodeSearch.TabIndex = 499
        Me.btnSupplierCodeSearch.Text = "..."
        Me.btnSupplierCodeSearch.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(622, 59)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(11, 16)
        Me.Label4.TabIndex = 506
        Me.Label4.Text = "-"
        '
        'txtSupplierCode
        '
        Me.txtSupplierCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierCode.ForeColor = System.Drawing.Color.Black
        Me.txtSupplierCode.Location = New System.Drawing.Point(596, 17)
        Me.txtSupplierCode.MaxLength = 20
        Me.txtSupplierCode.Name = "txtSupplierCode"
        Me.txtSupplierCode.Size = New System.Drawing.Size(96, 20)
        Me.txtSupplierCode.TabIndex = 498
        Me.txtSupplierCode.Tag = ""
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(500, 20)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(88, 15)
        Me.Label5.TabIndex = 500
        Me.Label5.Text = "Supplier Code"
        '
        'txtRemarks
        '
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtRemarks.Location = New System.Drawing.Point(75, 105)
        Me.txtRemarks.MaxLength = 50
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(556, 21)
        Me.txtRemarks.TabIndex = 515
        '
        'txtIYYYY
        '
        Me.txtIYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIYYYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIYYYY.Location = New System.Drawing.Point(670, 59)
        Me.txtIYYYY.MaxLength = 4
        Me.txtIYYYY.Name = "txtIYYYY"
        Me.txtIYYYY.Size = New System.Drawing.Size(46, 20)
        Me.txtIYYYY.TabIndex = 504
        Me.txtIYYYY.Tag = "ENN;"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label3.Location = New System.Drawing.Point(6, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 13)
        Me.Label3.TabIndex = 505
        Me.Label3.Text = "SO No"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtIMM
        '
        Me.txtIMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIMM.Location = New System.Drawing.Point(633, 59)
        Me.txtIMM.MaxLength = 2
        Me.txtIMM.Name = "txtIMM"
        Me.txtIMM.Size = New System.Drawing.Size(24, 20)
        Me.txtIMM.TabIndex = 503
        Me.txtIMM.Tag = "ENN;"
        '
        'btnFetch
        '
        Me.btnFetch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnFetch.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFetch.Location = New System.Drawing.Point(647, 93)
        Me.btnFetch.Name = "btnFetch"
        Me.btnFetch.Size = New System.Drawing.Size(75, 33)
        Me.btnFetch.TabIndex = 494
        Me.btnFetch.Text = "Fetch"
        Me.btnFetch.UseVisualStyleBackColor = False
        '
        'txtIDD
        '
        Me.txtIDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIDD.Location = New System.Drawing.Point(596, 59)
        Me.txtIDD.MaxLength = 2
        Me.txtIDD.Name = "txtIDD"
        Me.txtIDD.Size = New System.Drawing.Size(24, 20)
        Me.txtIDD.TabIndex = 502
        Me.txtIDD.Tag = "ENN;"
        '
        'txtSONo
        '
        Me.txtSONo.BackColor = System.Drawing.SystemColors.Window
        Me.txtSONo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSONo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSONo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtSONo.Location = New System.Drawing.Point(75, 13)
        Me.txtSONo.MaxLength = 50
        Me.txtSONo.Name = "txtSONo"
        Me.txtSONo.Size = New System.Drawing.Size(155, 21)
        Me.txtSONo.TabIndex = 504
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(500, 58)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(75, 20)
        Me.Label7.TabIndex = 505
        Me.Label7.Text = "Due Date"
        '
        'cboLocationCode
        '
        Me.cboLocationCode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLocationCode.Items.AddRange(New Object() {"ALL"})
        Me.cboLocationCode.Location = New System.Drawing.Point(337, 19)
        Me.cboLocationCode.Name = "cboLocationCode"
        Me.cboLocationCode.Size = New System.Drawing.Size(99, 21)
        Me.cboLocationCode.TabIndex = 509
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label20.Location = New System.Drawing.Point(245, 21)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(86, 13)
        Me.Label20.TabIndex = 508
        Me.Label20.Text = "Location Code"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label12.Location = New System.Drawing.Point(245, 59)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(64, 13)
        Me.Label12.TabIndex = 509
        Me.Label12.Text = "Catalogue"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCatalogue
        '
        Me.txtCatalogue.BackColor = System.Drawing.SystemColors.Window
        Me.txtCatalogue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCatalogue.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCatalogue.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCatalogue.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtCatalogue.Location = New System.Drawing.Point(337, 56)
        Me.txtCatalogue.MaxLength = 20
        Me.txtCatalogue.Name = "txtCatalogue"
        Me.txtCatalogue.Size = New System.Drawing.Size(130, 21)
        Me.txtCatalogue.TabIndex = 510
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label11.Location = New System.Drawing.Point(6, 57)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(59, 13)
        Me.Label11.TabIndex = 511
        Me.Label11.Text = "Catagory"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCategory
        '
        Me.txtCategory.BackColor = System.Drawing.SystemColors.Window
        Me.txtCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCategory.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtCategory.Location = New System.Drawing.Point(75, 55)
        Me.txtCategory.MaxLength = 20
        Me.txtCategory.Name = "txtCategory"
        Me.txtCategory.Size = New System.Drawing.Size(132, 21)
        Me.txtCategory.TabIndex = 512
        '
        'btnCatagorySearch
        '
        Me.btnCatagorySearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCatagorySearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCatagorySearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCatagorySearch.ForeColor = System.Drawing.Color.Black
        Me.btnCatagorySearch.Location = New System.Drawing.Point(204, 55)
        Me.btnCatagorySearch.Name = "btnCatagorySearch"
        Me.btnCatagorySearch.Size = New System.Drawing.Size(26, 21)
        Me.btnCatagorySearch.TabIndex = 513
        Me.btnCatagorySearch.Text = "..."
        Me.btnCatagorySearch.UseVisualStyleBackColor = False
        '
        'btnCatalogueSearch
        '
        Me.btnCatalogueSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCatalogueSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCatalogueSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCatalogueSearch.ForeColor = System.Drawing.Color.Black
        Me.btnCatalogueSearch.Location = New System.Drawing.Point(466, 56)
        Me.btnCatalogueSearch.Name = "btnCatalogueSearch"
        Me.btnCatalogueSearch.Size = New System.Drawing.Size(24, 21)
        Me.btnCatalogueSearch.TabIndex = 514
        Me.btnCatalogueSearch.Text = "..."
        Me.btnCatalogueSearch.UseVisualStyleBackColor = False
        '
        'grdCatalogue
        '
        Me.grdCatalogue.AllowUserToAddRows = False
        Me.grdCatalogue.AllowUserToDeleteRows = False
        Me.grdCatalogue.AllowUserToResizeColumns = False
        Me.grdCatalogue.AllowUserToResizeRows = False
        Me.grdCatalogue.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdCatalogue.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdCatalogue.ColumnHeadersHeight = 30
        Me.grdCatalogue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdCatalogue.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cSel, Me.cCategoryCode, Me.cCatalogueCode, Me.cWeight, Me.cToWeight, Me.cSizeCode, Me.cSizeType, Me.cToSizeCode, Me.cToSizeType, Me.cQtyonHand, Me.cReOrderLevel, Me.cSOQty})
        Me.grdCatalogue.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.grdCatalogue.Location = New System.Drawing.Point(12, 246)
        Me.grdCatalogue.Name = "grdCatalogue"
        Me.grdCatalogue.RowHeadersVisible = False
        Me.grdCatalogue.Size = New System.Drawing.Size(773, 248)
        Me.grdCatalogue.TabIndex = 494
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnNew.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Location = New System.Drawing.Point(370, 511)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(100, 40)
        Me.btnNew.TabIndex = 496
        Me.btnNew.Text = "New(F6)"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(688, 511)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 497
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(476, 511)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 40)
        Me.btnSave.TabIndex = 495
        Me.btnSave.Text = "Save(F7)"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnRePrint
        '
        Me.btnRePrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnRePrint.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRePrint.Location = New System.Drawing.Point(264, 511)
        Me.btnRePrint.Name = "btnRePrint"
        Me.btnRePrint.Size = New System.Drawing.Size(100, 40)
        Me.btnRePrint.TabIndex = 501
        Me.btnRePrint.Text = "Re Print"
        Me.btnRePrint.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDelete.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(582, 511)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(100, 40)
        Me.btnDelete.TabIndex = 502
        Me.btnDelete.Text = "Delete(F8)"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'cSel
        '
        Me.cSel.HeaderText = "Sel"
        Me.cSel.Name = "cSel"
        Me.cSel.Width = 30
        '
        'cCategoryCode
        '
        Me.cCategoryCode.HeaderText = "Category Code"
        Me.cCategoryCode.Name = "cCategoryCode"
        Me.cCategoryCode.Width = 110
        '
        'cCatalogueCode
        '
        Me.cCatalogueCode.HeaderText = "Catalogue Code"
        Me.cCatalogueCode.Name = "cCatalogueCode"
        Me.cCatalogueCode.Width = 200
        '
        'cWeight
        '
        Me.cWeight.HeaderText = "From Weight"
        Me.cWeight.Name = "cWeight"
        Me.cWeight.Width = 80
        '
        'cToWeight
        '
        Me.cToWeight.HeaderText = "To Weight"
        Me.cToWeight.Name = "cToWeight"
        '
        'cSizeCode
        '
        Me.cSizeCode.HeaderText = "From SizeCode"
        Me.cSizeCode.Name = "cSizeCode"
        Me.cSizeCode.Width = 80
        '
        'cSizeType
        '
        Me.cSizeType.HeaderText = "SizeType"
        Me.cSizeType.Name = "cSizeType"
        Me.cSizeType.Visible = False
        '
        'cToSizeCode
        '
        Me.cToSizeCode.HeaderText = "To SizeCode"
        Me.cToSizeCode.Name = "cToSizeCode"
        '
        'cToSizeType
        '
        Me.cToSizeType.HeaderText = "To SizeType"
        Me.cToSizeType.Name = "cToSizeType"
        Me.cToSizeType.Visible = False
        '
        'cQtyonHand
        '
        Me.cQtyonHand.HeaderText = "QtyonHand"
        Me.cQtyonHand.Name = "cQtyonHand"
        Me.cQtyonHand.Width = 80
        '
        'cReOrderLevel
        '
        Me.cReOrderLevel.HeaderText = "ReOrder Qty"
        Me.cReOrderLevel.Name = "cReOrderLevel"
        '
        'cSOQty
        '
        Me.cSOQty.HeaderText = "SO Qty"
        Me.cSOQty.Name = "cSOQty"
        Me.cSOQty.Width = 80
        '
        'frmSalesOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(797, 554)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnRePrint)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.grdCatalogue)
        Me.Controls.Add(Me.grpSearch)
        Me.Controls.Add(Me.pic)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmSalesOrder"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SalesOrder"
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpSearch.ResumeLayout(False)
        Me.grpSearch.PerformLayout()
        CType(Me.grdCatalogue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents grpSearch As System.Windows.Forms.GroupBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtCatalogue As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtCategory As System.Windows.Forms.TextBox
    Friend WithEvents btnCatagorySearch As System.Windows.Forms.Button
    Friend WithEvents btnCatalogueSearch As System.Windows.Forms.Button
    Friend WithEvents cboLocationCode As System.Windows.Forms.ComboBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents btnFetch As System.Windows.Forms.Button
    Friend WithEvents grdCatalogue As System.Windows.Forms.DataGridView
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtSONo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents btnSupplierCodeSearch As System.Windows.Forms.Button
    Friend WithEvents txtSupplierCode As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnRePrint As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtIYYYY As System.Windows.Forms.TextBox
    Friend WithEvents txtIMM As System.Windows.Forms.TextBox
    Friend WithEvents txtIDD As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents cSel As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents cCategoryCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCatalogueCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cToWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSizeCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSizeType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cToSizeCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cToSizeType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cQtyonHand As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cReOrderLevel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSOQty As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
