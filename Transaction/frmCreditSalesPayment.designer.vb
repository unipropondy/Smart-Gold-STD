<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCreditSalespayment
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCreditSalespayment))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmdFetch = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.optAll = New System.Windows.Forms.RadioButton()
        Me.optUnPaid = New System.Windows.Forms.RadioButton()
        Me.optPaid = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtTYY = New System.Windows.Forms.TextBox()
        Me.txtTMM = New System.Windows.Forms.TextBox()
        Me.txtTDD = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtFYY = New System.Windows.Forms.TextBox()
        Me.txtFMM = New System.Windows.Forms.TextBox()
        Me.txtFDD = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtPYY = New System.Windows.Forms.TextBox()
        Me.txtPMM = New System.Windows.Forms.TextBox()
        Me.txtPDD = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtPaymentNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgvSupplier = New System.Windows.Forms.DataGridView()
        Me.cINvoiceNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cInvoiceDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cCustomerCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cCustomerName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cTotalAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cPaidAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cBalanceAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cRemarks = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cboPaymode = New System.Windows.Forms.ComboBox()
        Me.txtBalanceAmount = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtPaidAmount = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtTotalAmount = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtInvoiceDate = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtCustCode = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.cmdUpdate = New System.Windows.Forms.Button()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.btnCustomerCodeSearch = New System.Windows.Forms.Button()
        Me.txtCustomerCode = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgvSupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(270, 100)
        Me.PictureBox1.TabIndex = 533
        Me.PictureBox1.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.txtCustomerName)
        Me.GroupBox1.Controls.Add(Me.btnCustomerCodeSearch)
        Me.GroupBox1.Controls.Add(Me.txtCustomerCode)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cmdFetch)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.Label27)
        Me.GroupBox1.Controls.Add(Me.Label26)
        Me.GroupBox1.Controls.Add(Me.txtPYY)
        Me.GroupBox1.Controls.Add(Me.txtPMM)
        Me.GroupBox1.Controls.Add(Me.txtPDD)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtPaymentNo)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 118)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(845, 146)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'cmdFetch
        '
        Me.cmdFetch.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFetch.Location = New System.Drawing.Point(699, 20)
        Me.cmdFetch.Name = "cmdFetch"
        Me.cmdFetch.Size = New System.Drawing.Size(132, 52)
        Me.cmdFetch.TabIndex = 4
        Me.cmdFetch.Text = "Fetch"
        Me.cmdFetch.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.optAll)
        Me.GroupBox3.Controls.Add(Me.optUnPaid)
        Me.GroupBox3.Controls.Add(Me.optPaid)
        Me.GroupBox3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(587, 8)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(103, 97)
        Me.GroupBox3.TabIndex = 476262
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Paid Status"
        '
        'optAll
        '
        Me.optAll.AutoSize = True
        Me.optAll.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optAll.Location = New System.Drawing.Point(16, 75)
        Me.optAll.Name = "optAll"
        Me.optAll.Size = New System.Drawing.Size(42, 17)
        Me.optAll.TabIndex = 2
        Me.optAll.Text = "All"
        Me.optAll.UseVisualStyleBackColor = True
        '
        'optUnPaid
        '
        Me.optUnPaid.AutoSize = True
        Me.optUnPaid.Checked = True
        Me.optUnPaid.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optUnPaid.Location = New System.Drawing.Point(16, 18)
        Me.optUnPaid.Name = "optUnPaid"
        Me.optUnPaid.Size = New System.Drawing.Size(70, 17)
        Me.optUnPaid.TabIndex = 0
        Me.optUnPaid.TabStop = True
        Me.optUnPaid.Text = "UnPaid"
        Me.optUnPaid.UseVisualStyleBackColor = True
        '
        'optPaid
        '
        Me.optPaid.AutoSize = True
        Me.optPaid.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPaid.Location = New System.Drawing.Point(16, 46)
        Me.optPaid.Name = "optPaid"
        Me.optPaid.Size = New System.Drawing.Size(53, 17)
        Me.optPaid.TabIndex = 1
        Me.optPaid.Text = "Paid"
        Me.optPaid.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.txtTYY)
        Me.GroupBox2.Controls.Add(Me.txtTMM)
        Me.GroupBox2.Controls.Add(Me.txtTDD)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.txtFYY)
        Me.GroupBox2.Controls.Add(Me.txtFMM)
        Me.GroupBox2.Controls.Add(Me.txtFDD)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(8, 84)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(551, 56)
        Me.GroupBox2.TabIndex = 476261
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Date Selection"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(449, 25)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(11, 14)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "-"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(403, 26)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(11, 14)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "-"
        '
        'txtTYY
        '
        Me.txtTYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTYY.Location = New System.Drawing.Point(462, 23)
        Me.txtTYY.MaxLength = 20
        Me.txtTYY.Name = "txtTYY"
        Me.txtTYY.Size = New System.Drawing.Size(32, 20)
        Me.txtTYY.TabIndex = 476276
        Me.txtTYY.Tag = "NE;"
        '
        'txtTMM
        '
        Me.txtTMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTMM.Location = New System.Drawing.Point(415, 23)
        Me.txtTMM.MaxLength = 20
        Me.txtTMM.Name = "txtTMM"
        Me.txtTMM.Size = New System.Drawing.Size(32, 20)
        Me.txtTMM.TabIndex = 476275
        Me.txtTMM.Tag = "NE;"
        '
        'txtTDD
        '
        Me.txtTDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTDD.Location = New System.Drawing.Point(369, 23)
        Me.txtTDD.MaxLength = 20
        Me.txtTDD.Name = "txtTDD"
        Me.txtTDD.Size = New System.Drawing.Size(32, 20)
        Me.txtTDD.TabIndex = 3
        Me.txtTDD.Tag = "NE;"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(266, 25)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(57, 13)
        Me.Label9.TabIndex = 476277
        Me.Label9.Text = "To Date"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(189, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(11, 14)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "-"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(143, 26)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(11, 14)
        Me.Label5.TabIndex = 476272
        Me.Label5.Text = "-"
        '
        'txtFYY
        '
        Me.txtFYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFYY.Location = New System.Drawing.Point(202, 23)
        Me.txtFYY.MaxLength = 20
        Me.txtFYY.Name = "txtFYY"
        Me.txtFYY.Size = New System.Drawing.Size(32, 20)
        Me.txtFYY.TabIndex = 476270
        Me.txtFYY.Tag = "NE;"
        '
        'txtFMM
        '
        Me.txtFMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFMM.Location = New System.Drawing.Point(155, 23)
        Me.txtFMM.MaxLength = 20
        Me.txtFMM.Name = "txtFMM"
        Me.txtFMM.Size = New System.Drawing.Size(32, 20)
        Me.txtFMM.TabIndex = 1
        Me.txtFMM.Tag = "NE;"
        '
        'txtFDD
        '
        Me.txtFDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFDD.Location = New System.Drawing.Point(109, 23)
        Me.txtFDD.MaxLength = 20
        Me.txtFDD.Name = "txtFDD"
        Me.txtFDD.Size = New System.Drawing.Size(32, 20)
        Me.txtFDD.TabIndex = 0
        Me.txtFDD.Tag = "NE;"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(6, 25)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(75, 13)
        Me.Label6.TabIndex = 476271
        Me.Label6.Text = "From Date"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(454, 16)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(11, 14)
        Me.Label27.TabIndex = 3
        Me.Label27.Text = "-"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(408, 17)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(11, 14)
        Me.Label26.TabIndex = 2
        Me.Label26.Text = "-"
        '
        'txtPYY
        '
        Me.txtPYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPYY.Enabled = False
        Me.txtPYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPYY.Location = New System.Drawing.Point(467, 14)
        Me.txtPYY.MaxLength = 20
        Me.txtPYY.Name = "txtPYY"
        Me.txtPYY.Size = New System.Drawing.Size(32, 20)
        Me.txtPYY.TabIndex = 476257
        Me.txtPYY.Tag = "NE;"
        '
        'txtPMM
        '
        Me.txtPMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPMM.Enabled = False
        Me.txtPMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPMM.Location = New System.Drawing.Point(420, 14)
        Me.txtPMM.MaxLength = 20
        Me.txtPMM.Name = "txtPMM"
        Me.txtPMM.Size = New System.Drawing.Size(32, 20)
        Me.txtPMM.TabIndex = 476256
        Me.txtPMM.Tag = "NE;"
        '
        'txtPDD
        '
        Me.txtPDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPDD.Enabled = False
        Me.txtPDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPDD.Location = New System.Drawing.Point(374, 14)
        Me.txtPDD.MaxLength = 20
        Me.txtPDD.Name = "txtPDD"
        Me.txtPDD.Size = New System.Drawing.Size(32, 20)
        Me.txtPDD.TabIndex = 1
        Me.txtPDD.Tag = "NE;"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(271, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(98, 13)
        Me.Label3.TabIndex = 476258
        Me.Label3.Text = "Payment Date"
        '
        'txtPaymentNo
        '
        Me.txtPaymentNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPaymentNo.Enabled = False
        Me.txtPaymentNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaymentNo.Location = New System.Drawing.Point(129, 14)
        Me.txtPaymentNo.MaxLength = 20
        Me.txtPaymentNo.Name = "txtPaymentNo"
        Me.txtPaymentNo.Size = New System.Drawing.Size(126, 20)
        Me.txtPaymentNo.TabIndex = 0
        Me.txtPaymentNo.Tag = "NE;"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 13)
        Me.Label1.TabIndex = 2565
        Me.Label1.Text = "Payment No"
        '
        'dgvSupplier
        '
        Me.dgvSupplier.AllowUserToAddRows = False
        Me.dgvSupplier.AllowUserToDeleteRows = False
        Me.dgvSupplier.AllowUserToResizeColumns = False
        Me.dgvSupplier.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSupplier.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvSupplier.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSupplier.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cINvoiceNo, Me.cInvoiceDate, Me.cCustomerCode, Me.cCustomerName, Me.cTotalAmount, Me.cPaidAmount, Me.cBalanceAmount, Me.cStatus, Me.cRemarks})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvSupplier.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvSupplier.Location = New System.Drawing.Point(12, 270)
        Me.dgvSupplier.Name = "dgvSupplier"
        Me.dgvSupplier.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSupplier.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvSupplier.RowHeadersVisible = False
        Me.dgvSupplier.RowTemplate.Height = 30
        Me.dgvSupplier.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvSupplier.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSupplier.Size = New System.Drawing.Size(845, 185)
        Me.dgvSupplier.TabIndex = 537
        '
        'cINvoiceNo
        '
        Me.cINvoiceNo.HeaderText = "Invoice No"
        Me.cINvoiceNo.Name = "cINvoiceNo"
        Me.cINvoiceNo.ReadOnly = True
        Me.cINvoiceNo.Width = 80
        '
        'cInvoiceDate
        '
        Me.cInvoiceDate.HeaderText = "Invoice Date"
        Me.cInvoiceDate.Name = "cInvoiceDate"
        Me.cInvoiceDate.ReadOnly = True
        Me.cInvoiceDate.Width = 120
        '
        'cCustomerCode
        '
        Me.cCustomerCode.HeaderText = "Customer Code"
        Me.cCustomerCode.Name = "cCustomerCode"
        Me.cCustomerCode.ReadOnly = True
        Me.cCustomerCode.Width = 80
        '
        'cCustomerName
        '
        Me.cCustomerName.HeaderText = "Customer Name"
        Me.cCustomerName.Name = "cCustomerName"
        Me.cCustomerName.ReadOnly = True
        Me.cCustomerName.Width = 200
        '
        'cTotalAmount
        '
        Me.cTotalAmount.HeaderText = "Total Amount"
        Me.cTotalAmount.Name = "cTotalAmount"
        Me.cTotalAmount.ReadOnly = True
        Me.cTotalAmount.Width = 60
        '
        'cPaidAmount
        '
        Me.cPaidAmount.HeaderText = "Paid Amount"
        Me.cPaidAmount.Name = "cPaidAmount"
        Me.cPaidAmount.ReadOnly = True
        Me.cPaidAmount.Width = 60
        '
        'cBalanceAmount
        '
        Me.cBalanceAmount.HeaderText = "Balance Amount"
        Me.cBalanceAmount.Name = "cBalanceAmount"
        Me.cBalanceAmount.ReadOnly = True
        Me.cBalanceAmount.Width = 60
        '
        'cStatus
        '
        Me.cStatus.HeaderText = "Status"
        Me.cStatus.Name = "cStatus"
        Me.cStatus.ReadOnly = True
        Me.cStatus.Width = 60
        '
        'cRemarks
        '
        Me.cRemarks.HeaderText = "Remarks"
        Me.cRemarks.Name = "cRemarks"
        Me.cRemarks.ReadOnly = True
        Me.cRemarks.Width = 120
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.txtAmount)
        Me.GroupBox4.Controls.Add(Me.Label18)
        Me.GroupBox4.Controls.Add(Me.Label17)
        Me.GroupBox4.Controls.Add(Me.txtRemarks)
        Me.GroupBox4.Controls.Add(Me.Label16)
        Me.GroupBox4.Controls.Add(Me.cboPaymode)
        Me.GroupBox4.Controls.Add(Me.txtBalanceAmount)
        Me.GroupBox4.Controls.Add(Me.Label15)
        Me.GroupBox4.Controls.Add(Me.txtPaidAmount)
        Me.GroupBox4.Controls.Add(Me.Label14)
        Me.GroupBox4.Controls.Add(Me.txtTotalAmount)
        Me.GroupBox4.Controls.Add(Me.Label13)
        Me.GroupBox4.Controls.Add(Me.txtInvoiceDate)
        Me.GroupBox4.Controls.Add(Me.Label12)
        Me.GroupBox4.Controls.Add(Me.txtInvoiceNo)
        Me.GroupBox4.Controls.Add(Me.Label11)
        Me.GroupBox4.Controls.Add(Me.txtCustCode)
        Me.GroupBox4.Controls.Add(Me.Label10)
        Me.GroupBox4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(12, 474)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(845, 105)
        Me.GroupBox4.TabIndex = 476263
        Me.GroupBox4.TabStop = False
        '
        'txtAmount
        '
        Me.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount.Location = New System.Drawing.Point(372, 68)
        Me.txtAmount.MaxLength = 20
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(97, 20)
        Me.txtAmount.TabIndex = 1
        Me.txtAmount.Tag = "NE;"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(275, 71)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(57, 13)
        Me.Label18.TabIndex = 3854
        Me.Label18.Text = "Amount"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(484, 71)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(64, 13)
        Me.Label17.TabIndex = 3853
        Me.Label17.Text = "Remarks"
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(554, 69)
        Me.txtRemarks.MaxLength = 20
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(265, 20)
        Me.txtRemarks.TabIndex = 2
        Me.txtRemarks.Tag = "NE;"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(6, 71)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(67, 13)
        Me.Label16.TabIndex = 3851
        Me.Label16.Text = "Paymode"
        '
        'cboPaymode
        '
        Me.cboPaymode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPaymode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPaymode.FormattingEnabled = True
        Me.cboPaymode.Items.AddRange(New Object() {"CASH", "GOLD BAR", "CHEQUE", "TRADE-IN"})
        Me.cboPaymode.Location = New System.Drawing.Point(143, 68)
        Me.cboPaymode.Name = "cboPaymode"
        Me.cboPaymode.Size = New System.Drawing.Size(126, 21)
        Me.cboPaymode.TabIndex = 0
        '
        'txtBalanceAmount
        '
        Me.txtBalanceAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBalanceAmount.Enabled = False
        Me.txtBalanceAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBalanceAmount.Location = New System.Drawing.Point(693, 33)
        Me.txtBalanceAmount.MaxLength = 20
        Me.txtBalanceAmount.Name = "txtBalanceAmount"
        Me.txtBalanceAmount.Size = New System.Drawing.Size(126, 20)
        Me.txtBalanceAmount.TabIndex = 2578
        Me.txtBalanceAmount.Tag = "NE;"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(690, 17)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(149, 13)
        Me.Label15.TabIndex = 2577
        Me.Label15.Text = "Total Balance Amount"
        '
        'txtPaidAmount
        '
        Me.txtPaidAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPaidAmount.Enabled = False
        Me.txtPaidAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaidAmount.Location = New System.Drawing.Point(554, 33)
        Me.txtPaidAmount.MaxLength = 20
        Me.txtPaidAmount.Name = "txtPaidAmount"
        Me.txtPaidAmount.Size = New System.Drawing.Size(126, 20)
        Me.txtPaidAmount.TabIndex = 2576
        Me.txtPaidAmount.Tag = "NE;"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(551, 17)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(126, 13)
        Me.Label14.TabIndex = 2575
        Me.Label14.Text = "Total Paid Amount"
        '
        'txtTotalAmount
        '
        Me.txtTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalAmount.Enabled = False
        Me.txtTotalAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalAmount.Location = New System.Drawing.Point(415, 33)
        Me.txtTotalAmount.MaxLength = 20
        Me.txtTotalAmount.Name = "txtTotalAmount"
        Me.txtTotalAmount.Size = New System.Drawing.Size(126, 20)
        Me.txtTotalAmount.TabIndex = 2574
        Me.txtTotalAmount.Tag = "NE;"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(412, 17)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(94, 13)
        Me.Label13.TabIndex = 2573
        Me.Label13.Text = "Total Amount"
        '
        'txtInvoiceDate
        '
        Me.txtInvoiceDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvoiceDate.Enabled = False
        Me.txtInvoiceDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceDate.Location = New System.Drawing.Point(155, 33)
        Me.txtInvoiceDate.MaxLength = 20
        Me.txtInvoiceDate.Name = "txtInvoiceDate"
        Me.txtInvoiceDate.Size = New System.Drawing.Size(126, 20)
        Me.txtInvoiceDate.TabIndex = 2572
        Me.txtInvoiceDate.Tag = "NE;"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(152, 17)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(90, 13)
        Me.Label12.TabIndex = 2571
        Me.Label12.Text = "Invoice Date"
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvoiceNo.Enabled = False
        Me.txtInvoiceNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceNo.Location = New System.Drawing.Point(14, 33)
        Me.txtInvoiceNo.MaxLength = 20
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(126, 20)
        Me.txtInvoiceNo.TabIndex = 2570
        Me.txtInvoiceNo.Tag = "NE;"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(11, 17)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(77, 13)
        Me.Label11.TabIndex = 2569
        Me.Label11.Text = "Invoice No"
        '
        'txtCustCode
        '
        Me.txtCustCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustCode.Enabled = False
        Me.txtCustCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustCode.Location = New System.Drawing.Point(290, 33)
        Me.txtCustCode.MaxLength = 20
        Me.txtCustCode.Name = "txtCustCode"
        Me.txtCustCode.Size = New System.Drawing.Size(110, 20)
        Me.txtCustCode.TabIndex = 2568
        Me.txtCustCode.Tag = "NE;"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(287, 17)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(71, 13)
        Me.Label10.TabIndex = 2567
        Me.Label10.Text = "Cust Code"
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(757, 587)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClear.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.Location = New System.Drawing.Point(651, 587)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(100, 40)
        Me.btnClear.TabIndex = 2
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'cmdUpdate
        '
        Me.cmdUpdate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdUpdate.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdUpdate.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUpdate.Location = New System.Drawing.Point(545, 587)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.Size = New System.Drawing.Size(100, 40)
        Me.cmdUpdate.TabIndex = 1
        Me.cmdUpdate.Text = "Update"
        Me.cmdUpdate.UseVisualStyleBackColor = False
        '
        'txtCustomerName
        '
        Me.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerName.Location = New System.Drawing.Point(271, 52)
        Me.txtCustomerName.MaxLength = 20
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(288, 20)
        Me.txtCustomerName.TabIndex = 476265
        Me.txtCustomerName.TabStop = False
        Me.txtCustomerName.Tag = "NE;"
        '
        'btnCustomerCodeSearch
        '
        Me.btnCustomerCodeSearch.BackColor = System.Drawing.Color.Orange
        Me.btnCustomerCodeSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomerCodeSearch.Location = New System.Drawing.Point(227, 52)
        Me.btnCustomerCodeSearch.Name = "btnCustomerCodeSearch"
        Me.btnCustomerCodeSearch.Size = New System.Drawing.Size(28, 20)
        Me.btnCustomerCodeSearch.TabIndex = 476264
        Me.btnCustomerCodeSearch.Text = "..."
        Me.btnCustomerCodeSearch.UseVisualStyleBackColor = False
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerCode.Location = New System.Drawing.Point(129, 52)
        Me.txtCustomerCode.MaxLength = 20
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.Size = New System.Drawing.Size(125, 20)
        Me.txtCustomerCode.TabIndex = 476263
        Me.txtCustomerCode.Tag = "NE;"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 54)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(105, 13)
        Me.Label4.TabIndex = 476266
        Me.Label4.Text = "Customer Code"
        '
        'frmCreditSalespayment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(869, 639)
        Me.Controls.Add(Me.cmdUpdate)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.dgvSupplier)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCreditSalespayment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Credit Sales Payment"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.dgvSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtPaymentNo As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtPYY As System.Windows.Forms.TextBox
    Friend WithEvents txtPMM As System.Windows.Forms.TextBox
    Friend WithEvents txtPDD As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmdFetch As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents optAll As System.Windows.Forms.RadioButton
    Friend WithEvents optUnPaid As System.Windows.Forms.RadioButton
    Friend WithEvents optPaid As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtTYY As System.Windows.Forms.TextBox
    Friend WithEvents txtTMM As System.Windows.Forms.TextBox
    Friend WithEvents txtTDD As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtFYY As System.Windows.Forms.TextBox
    Friend WithEvents txtFMM As System.Windows.Forms.TextBox
    Friend WithEvents txtFDD As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dgvSupplier As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents txtBalanceAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtPaidAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtTotalAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtInvoiceDate As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtInvoiceNo As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtCustCode As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cboPaymode As System.Windows.Forms.ComboBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents cmdUpdate As System.Windows.Forms.Button
    Friend WithEvents cINvoiceNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cInvoiceDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCustomerCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCustomerName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cTotalAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cPaidAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cBalanceAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cStatus As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cRemarks As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents btnCustomerCodeSearch As System.Windows.Forms.Button
    Friend WithEvents txtCustomerCode As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
