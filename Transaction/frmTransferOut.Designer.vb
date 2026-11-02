<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTransferOut
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTransferOut))
        Me.dgvTransfer = New System.Windows.Forms.DataGridView()
        Me.colInventoryCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDescription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colClassCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCategoryCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCatalogueCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSize = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCostPerGram = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPMC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSMC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTotalCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cboToLocationCode = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtFromLocationCode = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtTransferYear = New System.Windows.Forms.TextBox()
        Me.txtTransferDate = New System.Windows.Forms.TextBox()
        Me.txtTransferMonth = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtTransferNo = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.txtSMC = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtPMC = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtCostPerGram = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtInventoryCode = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.txtWeight = New System.Windows.Forms.TextBox()
        Me.txtSize = New System.Windows.Forms.TextBox()
        Me.txtTotalCost = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnTransferClear = New System.Windows.Forms.Button()
        Me.btnTransfer = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtTotalPCS = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtTotalSMC = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtTotalPMC = New System.Windows.Forms.TextBox()
        Me.txtTotalTotalCost = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBox7 = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtTotalWeight = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtPCS = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.btnScrapInventory = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.PnlSalesManPwd = New System.Windows.Forms.Panel()
        Me.txtSalesManPWD = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.btnUpdateTransfer = New System.Windows.Forms.Button()
        CType(Me.dgvTransfer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.PnlSalesManPwd.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvTransfer
        '
        Me.dgvTransfer.AllowUserToAddRows = False
        Me.dgvTransfer.AllowUserToDeleteRows = False
        Me.dgvTransfer.AllowUserToOrderColumns = True
        Me.dgvTransfer.AllowUserToResizeColumns = False
        Me.dgvTransfer.AllowUserToResizeRows = False
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvTransfer.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvTransfer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTransfer.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colInventoryCode, Me.colDescription, Me.colClassCode, Me.colCategoryCode, Me.colCatalogueCode, Me.colWeight, Me.colSize, Me.colQty, Me.colCostPerGram, Me.colPMC, Me.colSMC, Me.colTotalCost})
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvTransfer.DefaultCellStyle = DataGridViewCellStyle6
        Me.dgvTransfer.Location = New System.Drawing.Point(4, 239)
        Me.dgvTransfer.Name = "dgvTransfer"
        Me.dgvTransfer.ReadOnly = True
        Me.dgvTransfer.RowHeadersVisible = False
        Me.dgvTransfer.Size = New System.Drawing.Size(975, 222)
        Me.dgvTransfer.TabIndex = 3
        '
        'colInventoryCode
        '
        Me.colInventoryCode.HeaderText = "InventoryCode"
        Me.colInventoryCode.Name = "colInventoryCode"
        Me.colInventoryCode.ReadOnly = True
        Me.colInventoryCode.Width = 90
        '
        'colDescription
        '
        Me.colDescription.HeaderText = "Description"
        Me.colDescription.Name = "colDescription"
        Me.colDescription.ReadOnly = True
        Me.colDescription.Width = 185
        '
        'colClassCode
        '
        Me.colClassCode.HeaderText = "Class Code"
        Me.colClassCode.Name = "colClassCode"
        Me.colClassCode.ReadOnly = True
        Me.colClassCode.Width = 90
        '
        'colCategoryCode
        '
        Me.colCategoryCode.HeaderText = "Category Code"
        Me.colCategoryCode.Name = "colCategoryCode"
        Me.colCategoryCode.ReadOnly = True
        Me.colCategoryCode.Width = 90
        '
        'colCatalogueCode
        '
        Me.colCatalogueCode.HeaderText = "Catalogue Code"
        Me.colCatalogueCode.Name = "colCatalogueCode"
        Me.colCatalogueCode.ReadOnly = True
        Me.colCatalogueCode.Width = 90
        '
        'colWeight
        '
        Me.colWeight.HeaderText = "Weight"
        Me.colWeight.Name = "colWeight"
        Me.colWeight.ReadOnly = True
        Me.colWeight.Width = 50
        '
        'colSize
        '
        Me.colSize.HeaderText = "Size"
        Me.colSize.Name = "colSize"
        Me.colSize.ReadOnly = True
        Me.colSize.Width = 60
        '
        'colQty
        '
        Me.colQty.HeaderText = "Qty"
        Me.colQty.Name = "colQty"
        Me.colQty.ReadOnly = True
        Me.colQty.Width = 60
        '
        'colCostPerGram
        '
        Me.colCostPerGram.HeaderText = "Cost Per Gram"
        Me.colCostPerGram.Name = "colCostPerGram"
        Me.colCostPerGram.ReadOnly = True
        Me.colCostPerGram.Width = 70
        '
        'colPMC
        '
        Me.colPMC.HeaderText = "PMC"
        Me.colPMC.Name = "colPMC"
        Me.colPMC.ReadOnly = True
        Me.colPMC.Width = 50
        '
        'colSMC
        '
        Me.colSMC.HeaderText = "SMC"
        Me.colSMC.Name = "colSMC"
        Me.colSMC.ReadOnly = True
        Me.colSMC.Width = 60
        '
        'colTotalCost
        '
        Me.colTotalCost.HeaderText = "Total Cost"
        Me.colTotalCost.Name = "colTotalCost"
        Me.colTotalCost.ReadOnly = True
        Me.colTotalCost.Width = 120
        '
        'cboToLocationCode
        '
        Me.cboToLocationCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboToLocationCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboToLocationCode.Location = New System.Drawing.Point(307, 51)
        Me.cboToLocationCode.Name = "cboToLocationCode"
        Me.cboToLocationCode.Size = New System.Drawing.Size(117, 24)
        Me.cboToLocationCode.TabIndex = 6
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label15.Location = New System.Drawing.Point(222, 51)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(82, 13)
        Me.Label15.TabIndex = 506
        Me.Label15.Text = "To Location"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtRemarks)
        Me.GroupBox1.Controls.Add(Me.txtFromLocationCode)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtTransferYear)
        Me.GroupBox1.Controls.Add(Me.txtTransferDate)
        Me.GroupBox1.Controls.Add(Me.txtTransferMonth)
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtTransferNo)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.cboToLocationCode)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 112)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(496, 121)
        Me.GroupBox1.TabIndex = 510
        Me.GroupBox1.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label5.Location = New System.Drawing.Point(13, 81)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 13)
        Me.Label5.TabIndex = 513
        Me.Label5.Text = "Remarks"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRemarks
        '
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtRemarks.Location = New System.Drawing.Point(112, 81)
        Me.txtRemarks.MaxLength = 200
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(378, 21)
        Me.txtRemarks.TabIndex = 7
        '
        'txtFromLocationCode
        '
        Me.txtFromLocationCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtFromLocationCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFromLocationCode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromLocationCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtFromLocationCode.Location = New System.Drawing.Point(112, 51)
        Me.txtFromLocationCode.MaxLength = 50
        Me.txtFromLocationCode.Name = "txtFromLocationCode"
        Me.txtFromLocationCode.Size = New System.Drawing.Size(102, 21)
        Me.txtFromLocationCode.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label4.Location = New System.Drawing.Point(12, 54)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 13)
        Me.Label4.TabIndex = 511
        Me.Label4.Text = "From Location"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label1.Location = New System.Drawing.Point(799, 81)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 13)
        Me.Label1.TabIndex = 509
        Me.Label1.Text = "Invoice No"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label10.Location = New System.Drawing.Point(256, 19)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(97, 13)
        Me.Label10.TabIndex = 508
        Me.Label10.Text = "Transfer Date"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTransferYear
        '
        Me.txtTransferYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTransferYear.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransferYear.Location = New System.Drawing.Point(456, 19)
        Me.txtTransferYear.MaxLength = 4
        Me.txtTransferYear.Name = "txtTransferYear"
        Me.txtTransferYear.Size = New System.Drawing.Size(34, 21)
        Me.txtTransferYear.TabIndex = 4
        Me.txtTransferYear.Tag = "NE;"
        '
        'txtTransferDate
        '
        Me.txtTransferDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTransferDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransferDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTransferDate.Location = New System.Drawing.Point(376, 19)
        Me.txtTransferDate.MaxLength = 2
        Me.txtTransferDate.Name = "txtTransferDate"
        Me.txtTransferDate.Size = New System.Drawing.Size(21, 21)
        Me.txtTransferDate.TabIndex = 2
        Me.txtTransferDate.Tag = "NE;"
        '
        'txtTransferMonth
        '
        Me.txtTransferMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTransferMonth.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransferMonth.Location = New System.Drawing.Point(416, 19)
        Me.txtTransferMonth.MaxLength = 2
        Me.txtTransferMonth.Name = "txtTransferMonth"
        Me.txtTransferMonth.Size = New System.Drawing.Size(21, 21)
        Me.txtTransferMonth.TabIndex = 3
        Me.txtTransferMonth.Tag = "NE;"
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.TextBox1.Location = New System.Drawing.Point(887, 81)
        Me.TextBox1.MaxLength = 50
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(115, 21)
        Me.TextBox1.TabIndex = 510
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label3.Location = New System.Drawing.Point(13, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(84, 13)
        Me.Label3.TabIndex = 503
        Me.Label3.Text = "Transfer No"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTransferNo
        '
        Me.txtTransferNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtTransferNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTransferNo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransferNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtTransferNo.Location = New System.Drawing.Point(112, 19)
        Me.txtTransferNo.MaxLength = 50
        Me.txtTransferNo.Name = "txtTransferNo"
        Me.txtTransferNo.Size = New System.Drawing.Size(102, 21)
        Me.txtTransferNo.TabIndex = 1
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label12.Location = New System.Drawing.Point(113, 19)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(66, 13)
        Me.Label12.TabIndex = 503
        Me.Label12.Text = "Invoice No"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(12, 6)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 100)
        Me.pic.TabIndex = 511
        Me.pic.TabStop = False
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label31.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.Color.Black
        Me.Label31.Location = New System.Drawing.Point(436, 464)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(72, 24)
        Me.Label31.TabIndex = 531
        Me.Label31.Text = "Size"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtSMC
        '
        Me.txtSMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSMC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMC.ForeColor = System.Drawing.Color.Black
        Me.txtSMC.Location = New System.Drawing.Point(658, 488)
        Me.txtSMC.MaxLength = 20
        Me.txtSMC.Name = "txtSMC"
        Me.txtSMC.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtSMC.Size = New System.Drawing.Size(73, 20)
        Me.txtSMC.TabIndex = 14
        Me.txtSMC.Tag = ""
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.Color.Black
        Me.Label28.Location = New System.Drawing.Point(658, 464)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(73, 24)
        Me.Label28.TabIndex = 530
        Me.Label28.Text = "SMC"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtPMC
        '
        Me.txtPMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPMC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPMC.ForeColor = System.Drawing.Color.Black
        Me.txtPMC.Location = New System.Drawing.Point(586, 488)
        Me.txtPMC.MaxLength = 20
        Me.txtPMC.Name = "txtPMC"
        Me.txtPMC.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtPMC.Size = New System.Drawing.Size(73, 20)
        Me.txtPMC.TabIndex = 13
        Me.txtPMC.Tag = ""
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label29.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.Color.Black
        Me.Label29.Location = New System.Drawing.Point(586, 464)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(73, 24)
        Me.Label29.TabIndex = 529
        Me.Label29.Text = "PMC"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCostPerGram
        '
        Me.txtCostPerGram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCostPerGram.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostPerGram.ForeColor = System.Drawing.Color.Black
        Me.txtCostPerGram.Location = New System.Drawing.Point(507, 488)
        Me.txtCostPerGram.MaxLength = 20
        Me.txtCostPerGram.Name = "txtCostPerGram"
        Me.txtCostPerGram.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtCostPerGram.Size = New System.Drawing.Size(80, 20)
        Me.txtCostPerGram.TabIndex = 12
        Me.txtCostPerGram.Tag = ""
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Black
        Me.Label24.Location = New System.Drawing.Point(507, 464)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(80, 24)
        Me.Label24.TabIndex = 528
        Me.Label24.Text = "Cost Per Gram"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.Black
        Me.Label26.Location = New System.Drawing.Point(364, 464)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(72, 24)
        Me.Label26.TabIndex = 527
        Me.Label26.Text = "Weight"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.Black
        Me.Label23.Location = New System.Drawing.Point(124, 464)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(240, 24)
        Me.Label23.TabIndex = 526
        Me.Label23.Text = "Inventory Description"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtInventoryCode
        '
        Me.txtInventoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInventoryCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtInventoryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInventoryCode.ForeColor = System.Drawing.Color.Black
        Me.txtInventoryCode.Location = New System.Drawing.Point(4, 488)
        Me.txtInventoryCode.MaxLength = 20
        Me.txtInventoryCode.Name = "txtInventoryCode"
        Me.txtInventoryCode.Size = New System.Drawing.Size(120, 20)
        Me.txtInventoryCode.TabIndex = 8
        Me.txtInventoryCode.Tag = ""
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.Black
        Me.Label22.Location = New System.Drawing.Point(4, 464)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(120, 24)
        Me.Label22.TabIndex = 525
        Me.Label22.Text = "Inventory Code"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDescription
        '
        Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.ForeColor = System.Drawing.Color.Black
        Me.txtDescription.Location = New System.Drawing.Point(124, 488)
        Me.txtDescription.MaxLength = 20
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(241, 20)
        Me.txtDescription.TabIndex = 9
        Me.txtDescription.Tag = ""
        '
        'txtWeight
        '
        Me.txtWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeight.ForeColor = System.Drawing.Color.Black
        Me.txtWeight.Location = New System.Drawing.Point(364, 488)
        Me.txtWeight.MaxLength = 20
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtWeight.Size = New System.Drawing.Size(72, 20)
        Me.txtWeight.TabIndex = 10
        Me.txtWeight.Tag = ""
        '
        'txtSize
        '
        Me.txtSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSize.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSize.ForeColor = System.Drawing.Color.Black
        Me.txtSize.Location = New System.Drawing.Point(436, 488)
        Me.txtSize.MaxLength = 20
        Me.txtSize.Name = "txtSize"
        Me.txtSize.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtSize.Size = New System.Drawing.Size(72, 20)
        Me.txtSize.TabIndex = 11
        Me.txtSize.Tag = ""
        '
        'txtTotalCost
        '
        Me.txtTotalCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalCost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalCost.ForeColor = System.Drawing.Color.Black
        Me.txtTotalCost.Location = New System.Drawing.Point(813, 488)
        Me.txtTotalCost.MaxLength = 20
        Me.txtTotalCost.Name = "txtTotalCost"
        Me.txtTotalCost.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtTotalCost.Size = New System.Drawing.Size(81, 20)
        Me.txtTotalCost.TabIndex = 16
        Me.txtTotalCost.Tag = ""
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(813, 464)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 24)
        Me.Label2.TabIndex = 536
        Me.Label2.Text = "Total Cost"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnAdd
        '
        Me.btnAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnAdd.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.ForeColor = System.Drawing.Color.Black
        Me.btnAdd.Location = New System.Drawing.Point(738, 512)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 17
        Me.btnAdd.Text = "Add "
        Me.btnAdd.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(899, 512)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 19
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.Location = New System.Drawing.Point(819, 512)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 18
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'btnTransferClear
        '
        Me.btnTransferClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnTransferClear.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTransferClear.Location = New System.Drawing.Point(657, 552)
        Me.btnTransferClear.Name = "btnTransferClear"
        Me.btnTransferClear.Size = New System.Drawing.Size(100, 40)
        Me.btnTransferClear.TabIndex = 20
        Me.btnTransferClear.Text = "Clear (F6)"
        Me.btnTransferClear.UseVisualStyleBackColor = False
        '
        'btnTransfer
        '
        Me.btnTransfer.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnTransfer.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTransfer.Location = New System.Drawing.Point(763, 552)
        Me.btnTransfer.Name = "btnTransfer"
        Me.btnTransfer.Size = New System.Drawing.Size(110, 40)
        Me.btnTransfer.TabIndex = 21
        Me.btnTransfer.Text = "Transfer"
        Me.btnTransfer.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(879, 552)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 22
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtTotalPCS)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.txtTotalSMC)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.txtTotalPMC)
        Me.GroupBox2.Controls.Add(Me.txtTotalTotalCost)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.TextBox7)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.txtTotalWeight)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Location = New System.Drawing.Point(514, 112)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(465, 121)
        Me.GroupBox2.TabIndex = 543
        Me.GroupBox2.TabStop = False
        '
        'txtTotalPCS
        '
        Me.txtTotalPCS.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalPCS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalPCS.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalPCS.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtTotalPCS.Location = New System.Drawing.Point(108, 50)
        Me.txtTotalPCS.MaxLength = 50
        Me.txtTotalPCS.Name = "txtTotalPCS"
        Me.txtTotalPCS.Size = New System.Drawing.Size(105, 21)
        Me.txtTotalPCS.TabIndex = 518
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label14.Location = New System.Drawing.Point(12, 53)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(68, 13)
        Me.Label14.TabIndex = 517
        Me.Label14.Text = "Total PCS"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTotalSMC
        '
        Me.txtTotalSMC.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalSMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalSMC.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalSMC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtTotalSMC.Location = New System.Drawing.Point(300, 51)
        Me.txtTotalSMC.MaxLength = 50
        Me.txtTotalSMC.Name = "txtTotalSMC"
        Me.txtTotalSMC.Size = New System.Drawing.Size(115, 21)
        Me.txtTotalSMC.TabIndex = 516
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label6.Location = New System.Drawing.Point(214, 54)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 13)
        Me.Label6.TabIndex = 515
        Me.Label6.Text = "Total SMC"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label9.Location = New System.Drawing.Point(215, 19)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(70, 13)
        Me.Label9.TabIndex = 513
        Me.Label9.Text = "Total PMC"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTotalPMC
        '
        Me.txtTotalPMC.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalPMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalPMC.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalPMC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtTotalPMC.Location = New System.Drawing.Point(300, 19)
        Me.txtTotalPMC.MaxLength = 50
        Me.txtTotalPMC.Name = "txtTotalPMC"
        Me.txtTotalPMC.Size = New System.Drawing.Size(115, 21)
        Me.txtTotalPMC.TabIndex = 514
        '
        'txtTotalTotalCost
        '
        Me.txtTotalTotalCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalTotalCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalTotalCost.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalTotalCost.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtTotalTotalCost.Location = New System.Drawing.Point(108, 85)
        Me.txtTotalTotalCost.MaxLength = 50
        Me.txtTotalTotalCost.Name = "txtTotalTotalCost"
        Me.txtTotalTotalCost.Size = New System.Drawing.Size(105, 21)
        Me.txtTotalTotalCost.TabIndex = 512
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label7.Location = New System.Drawing.Point(12, 88)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 13)
        Me.Label7.TabIndex = 511
        Me.Label7.Text = "Total Cost"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label8.Location = New System.Drawing.Point(799, 81)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(66, 13)
        Me.Label8.TabIndex = 509
        Me.Label8.Text = "Invoice No"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TextBox7
        '
        Me.TextBox7.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.TextBox7.Location = New System.Drawing.Point(887, 81)
        Me.TextBox7.MaxLength = 50
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.Size = New System.Drawing.Size(115, 21)
        Me.TextBox7.TabIndex = 510
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label11.Location = New System.Drawing.Point(13, 19)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(89, 13)
        Me.Label11.TabIndex = 503
        Me.Label11.Text = "Total Weight"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTotalWeight
        '
        Me.txtTotalWeight.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalWeight.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalWeight.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtTotalWeight.Location = New System.Drawing.Point(108, 19)
        Me.txtTotalWeight.MaxLength = 50
        Me.txtTotalWeight.Name = "txtTotalWeight"
        Me.txtTotalWeight.Size = New System.Drawing.Size(105, 21)
        Me.txtTotalWeight.TabIndex = 504
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label13.Location = New System.Drawing.Point(113, 19)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(66, 13)
        Me.Label13.TabIndex = 503
        Me.Label13.Text = "Invoice No"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPCS
        '
        Me.txtPCS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPCS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPCS.ForeColor = System.Drawing.Color.Black
        Me.txtPCS.Location = New System.Drawing.Point(731, 488)
        Me.txtPCS.MaxLength = 20
        Me.txtPCS.Name = "txtPCS"
        Me.txtPCS.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtPCS.Size = New System.Drawing.Size(85, 20)
        Me.txtPCS.TabIndex = 15
        Me.txtPCS.Tag = ""
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(731, 464)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(85, 24)
        Me.Label16.TabIndex = 545
        Me.Label16.Text = "PCS"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnScrapInventory
        '
        Me.btnScrapInventory.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnScrapInventory.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnScrapInventory.Location = New System.Drawing.Point(382, 552)
        Me.btnScrapInventory.Name = "btnScrapInventory"
        Me.btnScrapInventory.Size = New System.Drawing.Size(126, 40)
        Me.btnScrapInventory.TabIndex = 546
        Me.btnScrapInventory.Text = "Scrap Inventory Transfer"
        Me.btnScrapInventory.UseVisualStyleBackColor = False
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPrint.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(551, 552)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(100, 40)
        Me.btnPrint.TabIndex = 554
        Me.btnPrint.Text = "Print (F8)"
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'PnlSalesManPwd
        '
        Me.PnlSalesManPwd.BackColor = System.Drawing.Color.Pink
        Me.PnlSalesManPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PnlSalesManPwd.Controls.Add(Me.txtSalesManPWD)
        Me.PnlSalesManPwd.Controls.Add(Me.Label21)
        Me.PnlSalesManPwd.Location = New System.Drawing.Point(351, 273)
        Me.PnlSalesManPwd.Name = "PnlSalesManPwd"
        Me.PnlSalesManPwd.Size = New System.Drawing.Size(199, 88)
        Me.PnlSalesManPwd.TabIndex = 555
        Me.PnlSalesManPwd.Visible = False
        '
        'txtSalesManPWD
        '
        Me.txtSalesManPWD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesManPWD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesManPWD.ForeColor = System.Drawing.Color.Black
        Me.txtSalesManPWD.Location = New System.Drawing.Point(48, 40)
        Me.txtSalesManPWD.MaxLength = 20
        Me.txtSalesManPWD.Name = "txtSalesManPWD"
        Me.txtSalesManPWD.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtSalesManPWD.Size = New System.Drawing.Size(136, 20)
        Me.txtSalesManPWD.TabIndex = 0
        Me.txtSalesManPWD.Tag = ""
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Black
        Me.Label21.Location = New System.Drawing.Point(48, 14)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(128, 15)
        Me.Label21.TabIndex = 74
        Me.Label21.Text = "SalesMan Password"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnUpdateTransfer
        '
        Me.btnUpdateTransfer.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnUpdateTransfer.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnUpdateTransfer.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdateTransfer.Location = New System.Drawing.Point(237, 552)
        Me.btnUpdateTransfer.Name = "btnUpdateTransfer"
        Me.btnUpdateTransfer.Size = New System.Drawing.Size(126, 40)
        Me.btnUpdateTransfer.TabIndex = 556
        Me.btnUpdateTransfer.Text = "Update Refresh Transfer"
        Me.btnUpdateTransfer.UseVisualStyleBackColor = False
        '
        'frmTransferOut
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(995, 604)
        Me.Controls.Add(Me.btnUpdateTransfer)
        Me.Controls.Add(Me.PnlSalesManPwd)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.btnScrapInventory)
        Me.Controls.Add(Me.txtPCS)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnTransferClear)
        Me.Controls.Add(Me.btnTransfer)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.txtTotalCost)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtSize)
        Me.Controls.Add(Me.txtWeight)
        Me.Controls.Add(Me.txtDescription)
        Me.Controls.Add(Me.Label31)
        Me.Controls.Add(Me.txtSMC)
        Me.Controls.Add(Me.Label28)
        Me.Controls.Add(Me.txtPMC)
        Me.Controls.Add(Me.Label29)
        Me.Controls.Add(Me.txtCostPerGram)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.txtInventoryCode)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.dgvTransfer)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmTransferOut"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Transfer Out"
        CType(Me.dgvTransfer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.PnlSalesManPwd.ResumeLayout(False)
        Me.PnlSalesManPwd.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout

End Sub
    Friend WithEvents dgvTransfer As System.Windows.Forms.DataGridView
    Friend WithEvents cboToLocationCode As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtTransferNo As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtFromLocationCode As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtTransferYear As System.Windows.Forms.TextBox
    Friend WithEvents txtTransferDate As System.Windows.Forms.TextBox
    Friend WithEvents txtTransferMonth As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txtSMC As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtPMC As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtCostPerGram As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtInventoryCode As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtSize As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalCost As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnTransferClear As System.Windows.Forms.Button
    Friend WithEvents btnTransfer As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents colInventoryCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colClassCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCategoryCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCatalogueCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSize As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colQty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCostPerGram As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPMC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSMC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTotalCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtTotalSMC As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtTotalPMC As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalTotalCost As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TextBox7 As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtTotalWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtTotalPCS As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtPCS As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents btnScrapInventory As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents PnlSalesManPwd As System.Windows.Forms.Panel
    Friend WithEvents txtSalesManPWD As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents btnUpdateTransfer As System.Windows.Forms.Button
End Class
