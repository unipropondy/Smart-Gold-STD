<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSupplierPayment
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSupplierPayment))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkCustomerCopy = New System.Windows.Forms.CheckBox()
        Me.chkFinalPayment = New System.Windows.Forms.CheckBox()
        Me.txtHeadInvoiceNo = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
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
        Me.txtSupplierName = New System.Windows.Forms.TextBox()
        Me.btnSupplierCodeSearch = New System.Windows.Forms.Button()
        Me.txtSupplierCode = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtPaymentNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgvSupplier = New System.Windows.Forms.DataGridView()
        Me.cJobSheetNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cINvoiceNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cInvoiceDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cTotalAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cPaidAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cBalanceAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cRemarks = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.btnIssueNo = New System.Windows.Forms.Button()
        Me.txtIssueNo = New System.Windows.Forms.TextBox()
        Me.lblIssueNo = New System.Windows.Forms.Label()
        Me.lblBank = New System.Windows.Forms.Label()
        Me.cboBank = New System.Windows.Forms.ComboBox()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lblRemarks = New System.Windows.Forms.Label()
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
        Me.txtJobSheetNo = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.cmdUpdate = New System.Windows.Forms.Button()
        Me.btnReprint = New System.Windows.Forms.Button()
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
        Me.GroupBox1.Controls.Add(Me.chkCustomerCopy)
        Me.GroupBox1.Controls.Add(Me.chkFinalPayment)
        Me.GroupBox1.Controls.Add(Me.txtHeadInvoiceNo)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.cmdFetch)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.Label27)
        Me.GroupBox1.Controls.Add(Me.Label26)
        Me.GroupBox1.Controls.Add(Me.txtPYY)
        Me.GroupBox1.Controls.Add(Me.txtPMM)
        Me.GroupBox1.Controls.Add(Me.txtPDD)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtSupplierName)
        Me.GroupBox1.Controls.Add(Me.btnSupplierCodeSearch)
        Me.GroupBox1.Controls.Add(Me.txtSupplierCode)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtPaymentNo)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 118)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(845, 154)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'chkCustomerCopy
        '
        Me.chkCustomerCopy.AutoSize = True
        Me.chkCustomerCopy.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkCustomerCopy.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCustomerCopy.Location = New System.Drawing.Point(712, 128)
        Me.chkCustomerCopy.Name = "chkCustomerCopy"
        Me.chkCustomerCopy.Size = New System.Drawing.Size(110, 17)
        Me.chkCustomerCopy.TabIndex = 476266
        Me.chkCustomerCopy.Text = "Customer Copy"
        Me.chkCustomerCopy.UseVisualStyleBackColor = True
        '
        'chkFinalPayment
        '
        Me.chkFinalPayment.AutoSize = True
        Me.chkFinalPayment.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkFinalPayment.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFinalPayment.Location = New System.Drawing.Point(587, 128)
        Me.chkFinalPayment.Name = "chkFinalPayment"
        Me.chkFinalPayment.Size = New System.Drawing.Size(105, 17)
        Me.chkFinalPayment.TabIndex = 476265
        Me.chkFinalPayment.Text = "Final Payment"
        Me.chkFinalPayment.UseVisualStyleBackColor = True
        Me.chkFinalPayment.Visible = False
        '
        'txtHeadInvoiceNo
        '
        Me.txtHeadInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHeadInvoiceNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHeadInvoiceNo.Location = New System.Drawing.Point(129, 66)
        Me.txtHeadInvoiceNo.MaxLength = 20
        Me.txtHeadInvoiceNo.Name = "txtHeadInvoiceNo"
        Me.txtHeadInvoiceNo.Size = New System.Drawing.Size(126, 20)
        Me.txtHeadInvoiceNo.TabIndex = 476264
        Me.txtHeadInvoiceNo.Tag = "NE;"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(6, 68)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(77, 13)
        Me.Label17.TabIndex = 476263
        Me.Label17.Text = "Invoice No"
        '
        'cmdFetch
        '
        Me.cmdFetch.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFetch.Location = New System.Drawing.Point(699, 20)
        Me.cmdFetch.Name = "cmdFetch"
        Me.cmdFetch.Size = New System.Drawing.Size(132, 52)
        Me.cmdFetch.TabIndex = 7
        Me.cmdFetch.Text = "Fetch"
        Me.cmdFetch.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.optAll)
        Me.GroupBox3.Controls.Add(Me.optUnPaid)
        Me.GroupBox3.Controls.Add(Me.optPaid)
        Me.GroupBox3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(587, 14)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(103, 108)
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
        Me.GroupBox2.Location = New System.Drawing.Point(9, 89)
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
        Me.txtTYY.TabIndex = 12
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
        Me.txtTMM.TabIndex = 11
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
        Me.txtTDD.TabIndex = 10
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
        Me.Label5.TabIndex = 1
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
        Me.txtFYY.TabIndex = 9
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
        Me.txtFMM.TabIndex = 8
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
        Me.txtFDD.TabIndex = 7
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
        Me.txtPYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPYY.Location = New System.Drawing.Point(467, 14)
        Me.txtPYY.MaxLength = 20
        Me.txtPYY.Name = "txtPYY"
        Me.txtPYY.Size = New System.Drawing.Size(32, 20)
        Me.txtPYY.TabIndex = 3
        Me.txtPYY.Tag = "NE;"
        '
        'txtPMM
        '
        Me.txtPMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPMM.Location = New System.Drawing.Point(420, 14)
        Me.txtPMM.MaxLength = 20
        Me.txtPMM.Name = "txtPMM"
        Me.txtPMM.Size = New System.Drawing.Size(32, 20)
        Me.txtPMM.TabIndex = 2
        Me.txtPMM.Tag = "NE;"
        '
        'txtPDD
        '
        Me.txtPDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
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
        'txtSupplierName
        '
        Me.txtSupplierName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierName.Location = New System.Drawing.Point(271, 40)
        Me.txtSupplierName.MaxLength = 20
        Me.txtSupplierName.Name = "txtSupplierName"
        Me.txtSupplierName.Size = New System.Drawing.Size(288, 20)
        Me.txtSupplierName.TabIndex = 6
        Me.txtSupplierName.TabStop = False
        Me.txtSupplierName.Tag = "NE;"
        '
        'btnSupplierCodeSearch
        '
        Me.btnSupplierCodeSearch.BackColor = System.Drawing.Color.Orange
        Me.btnSupplierCodeSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSupplierCodeSearch.Location = New System.Drawing.Point(227, 40)
        Me.btnSupplierCodeSearch.Name = "btnSupplierCodeSearch"
        Me.btnSupplierCodeSearch.Size = New System.Drawing.Size(28, 20)
        Me.btnSupplierCodeSearch.TabIndex = 5
        Me.btnSupplierCodeSearch.Text = "..."
        Me.btnSupplierCodeSearch.UseVisualStyleBackColor = False
        '
        'txtSupplierCode
        '
        Me.txtSupplierCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierCode.Location = New System.Drawing.Point(129, 40)
        Me.txtSupplierCode.MaxLength = 20
        Me.txtSupplierCode.Name = "txtSupplierCode"
        Me.txtSupplierCode.Size = New System.Drawing.Size(125, 20)
        Me.txtSupplierCode.TabIndex = 4
        Me.txtSupplierCode.Tag = "NE;"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 42)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(97, 13)
        Me.Label4.TabIndex = 476252
        Me.Label4.Text = "Supplier Code"
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
        Me.dgvSupplier.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cJobSheetNo, Me.cINvoiceNo, Me.cInvoiceDate, Me.cTotalAmount, Me.cPaidAmount, Me.cBalanceAmount, Me.cStatus, Me.cRemarks})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvSupplier.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvSupplier.Location = New System.Drawing.Point(12, 278)
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
        Me.dgvSupplier.Size = New System.Drawing.Size(845, 190)
        Me.dgvSupplier.TabIndex = 13
        '
        'cJobSheetNo
        '
        Me.cJobSheetNo.HeaderText = "Job Sheet No"
        Me.cJobSheetNo.Name = "cJobSheetNo"
        Me.cJobSheetNo.ReadOnly = True
        Me.cJobSheetNo.Width = 120
        '
        'cINvoiceNo
        '
        Me.cINvoiceNo.HeaderText = "Invoice No"
        Me.cINvoiceNo.Name = "cINvoiceNo"
        Me.cINvoiceNo.ReadOnly = True
        Me.cINvoiceNo.Width = 120
        '
        'cInvoiceDate
        '
        Me.cInvoiceDate.HeaderText = "Invoice Date"
        Me.cInvoiceDate.Name = "cInvoiceDate"
        Me.cInvoiceDate.ReadOnly = True
        Me.cInvoiceDate.Width = 80
        '
        'cTotalAmount
        '
        Me.cTotalAmount.HeaderText = "Total Amount"
        Me.cTotalAmount.Name = "cTotalAmount"
        Me.cTotalAmount.ReadOnly = True
        Me.cTotalAmount.Width = 80
        '
        'cPaidAmount
        '
        Me.cPaidAmount.HeaderText = "Paid Amount"
        Me.cPaidAmount.Name = "cPaidAmount"
        Me.cPaidAmount.ReadOnly = True
        Me.cPaidAmount.Width = 80
        '
        'cBalanceAmount
        '
        Me.cBalanceAmount.HeaderText = "Balance Amount"
        Me.cBalanceAmount.Name = "cBalanceAmount"
        Me.cBalanceAmount.ReadOnly = True
        Me.cBalanceAmount.Width = 80
        '
        'cStatus
        '
        Me.cStatus.HeaderText = "Status"
        Me.cStatus.Name = "cStatus"
        Me.cStatus.ReadOnly = True
        Me.cStatus.Width = 70
        '
        'cRemarks
        '
        Me.cRemarks.HeaderText = "Remarks"
        Me.cRemarks.Name = "cRemarks"
        Me.cRemarks.ReadOnly = True
        Me.cRemarks.Width = 200
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.btnIssueNo)
        Me.GroupBox4.Controls.Add(Me.txtIssueNo)
        Me.GroupBox4.Controls.Add(Me.lblIssueNo)
        Me.GroupBox4.Controls.Add(Me.lblBank)
        Me.GroupBox4.Controls.Add(Me.cboBank)
        Me.GroupBox4.Controls.Add(Me.txtAmount)
        Me.GroupBox4.Controls.Add(Me.Label18)
        Me.GroupBox4.Controls.Add(Me.lblRemarks)
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
        Me.GroupBox4.Controls.Add(Me.txtJobSheetNo)
        Me.GroupBox4.Controls.Add(Me.Label10)
        Me.GroupBox4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(12, 474)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(845, 105)
        Me.GroupBox4.TabIndex = 476263
        Me.GroupBox4.TabStop = False
        '
        'btnIssueNo
        '
        Me.btnIssueNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnIssueNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnIssueNo.Image = CType(resources.GetObject("btnIssueNo.Image"), System.Drawing.Image)
        Me.btnIssueNo.Location = New System.Drawing.Point(574, 71)
        Me.btnIssueNo.Name = "btnIssueNo"
        Me.btnIssueNo.Size = New System.Drawing.Size(32, 21)
        Me.btnIssueNo.TabIndex = 3859
        Me.btnIssueNo.Text = "..."
        Me.btnIssueNo.UseVisualStyleBackColor = False
        Me.btnIssueNo.Visible = False
        '
        'txtIssueNo
        '
        Me.txtIssueNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIssueNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIssueNo.Location = New System.Drawing.Point(497, 71)
        Me.txtIssueNo.MaxLength = 20
        Me.txtIssueNo.Name = "txtIssueNo"
        Me.txtIssueNo.Size = New System.Drawing.Size(79, 20)
        Me.txtIssueNo.TabIndex = 3858
        Me.txtIssueNo.Tag = "NE;"
        Me.txtIssueNo.Visible = False
        '
        'lblIssueNo
        '
        Me.lblIssueNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIssueNo.Location = New System.Drawing.Point(388, 71)
        Me.lblIssueNo.Name = "lblIssueNo"
        Me.lblIssueNo.Size = New System.Drawing.Size(103, 27)
        Me.lblIssueNo.TabIndex = 3857
        Me.lblIssueNo.Text = "GoldBar Issue No"
        Me.lblIssueNo.Visible = False
        '
        'lblBank
        '
        Me.lblBank.AutoSize = True
        Me.lblBank.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBank.Location = New System.Drawing.Point(437, 70)
        Me.lblBank.Name = "lblBank"
        Me.lblBank.Size = New System.Drawing.Size(39, 13)
        Me.lblBank.TabIndex = 3856
        Me.lblBank.Text = "Bank"
        '
        'cboBank
        '
        Me.cboBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBank.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboBank.FormattingEnabled = True
        Me.cboBank.ItemHeight = 13
        Me.cboBank.Items.AddRange(New Object() {"OCBC", "IB", "DBS", "UOB", "STD"})
        Me.cboBank.Location = New System.Drawing.Point(507, 70)
        Me.cboBank.Name = "cboBank"
        Me.cboBank.Size = New System.Drawing.Size(92, 21)
        Me.cboBank.TabIndex = 16
        '
        'txtAmount
        '
        Me.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount.Location = New System.Drawing.Point(288, 67)
        Me.txtAmount.MaxLength = 20
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(97, 20)
        Me.txtAmount.TabIndex = 15
        Me.txtAmount.Tag = "NE;"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(225, 67)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(57, 13)
        Me.Label18.TabIndex = 3854
        Me.Label18.Text = "Amount"
        '
        'lblRemarks
        '
        Me.lblRemarks.AutoSize = True
        Me.lblRemarks.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemarks.Location = New System.Drawing.Point(613, 70)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(64, 13)
        Me.lblRemarks.TabIndex = 3853
        Me.lblRemarks.Text = "Remarks"
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(693, 70)
        Me.txtRemarks.MaxLength = 20
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(124, 20)
        Me.txtRemarks.TabIndex = 17
        Me.txtRemarks.Tag = "NE;"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(6, 70)
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
        Me.cboPaymode.Items.AddRange(New Object() {"CASH", "GOLD BAR", "CHEQUE", "TRADE-IN", "BANK TRANSFER", "TT"})
        Me.cboPaymode.Location = New System.Drawing.Point(91, 67)
        Me.cboPaymode.Name = "cboPaymode"
        Me.cboPaymode.Size = New System.Drawing.Size(126, 21)
        Me.cboPaymode.TabIndex = 14
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
        Me.txtInvoiceDate.Location = New System.Drawing.Point(278, 33)
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
        Me.Label12.Location = New System.Drawing.Point(275, 17)
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
        Me.txtInvoiceNo.Location = New System.Drawing.Point(143, 33)
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
        Me.Label11.Location = New System.Drawing.Point(140, 17)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(77, 13)
        Me.Label11.TabIndex = 2569
        Me.Label11.Text = "Invoice No"
        '
        'txtJobSheetNo
        '
        Me.txtJobSheetNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJobSheetNo.Enabled = False
        Me.txtJobSheetNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJobSheetNo.Location = New System.Drawing.Point(8, 33)
        Me.txtJobSheetNo.MaxLength = 20
        Me.txtJobSheetNo.Name = "txtJobSheetNo"
        Me.txtJobSheetNo.Size = New System.Drawing.Size(126, 20)
        Me.txtJobSheetNo.TabIndex = 2568
        Me.txtJobSheetNo.Tag = "NE;"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(5, 17)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(91, 13)
        Me.Label10.TabIndex = 2567
        Me.Label10.Text = "Job Sheet No"
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(757, 587)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 20
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
        Me.btnClear.TabIndex = 19
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
        Me.cmdUpdate.TabIndex = 18
        Me.cmdUpdate.Text = "Update"
        Me.cmdUpdate.UseVisualStyleBackColor = False
        '
        'btnReprint
        '
        Me.btnReprint.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnReprint.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnReprint.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReprint.Location = New System.Drawing.Point(436, 587)
        Me.btnReprint.Name = "btnReprint"
        Me.btnReprint.Size = New System.Drawing.Size(100, 40)
        Me.btnReprint.TabIndex = 476264
        Me.btnReprint.Text = "RePrint"
        Me.btnReprint.UseVisualStyleBackColor = False
        '
        'frmSupplierPayment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(869, 639)
        Me.Controls.Add(Me.btnReprint)
        Me.Controls.Add(Me.cmdUpdate)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.dgvSupplier)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSupplierPayment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Supplier Payment"
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
    Friend WithEvents btnSupplierCodeSearch As System.Windows.Forms.Button
    Friend WithEvents txtSupplierCode As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtSupplierName As System.Windows.Forms.TextBox
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
    Friend WithEvents cJobSheetNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cINvoiceNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cInvoiceDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cTotalAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cPaidAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cBalanceAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cStatus As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cRemarks As System.Windows.Forms.DataGridViewTextBoxColumn
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
    Friend WithEvents txtJobSheetNo As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents lblRemarks As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cboPaymode As System.Windows.Forms.ComboBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents cmdUpdate As System.Windows.Forms.Button
    Friend WithEvents lblBank As System.Windows.Forms.Label
    Friend WithEvents cboBank As System.Windows.Forms.ComboBox
    Friend WithEvents btnReprint As System.Windows.Forms.Button
    Friend WithEvents txtHeadInvoiceNo As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents btnIssueNo As System.Windows.Forms.Button
    Friend WithEvents txtIssueNo As System.Windows.Forms.TextBox
    Friend WithEvents lblIssueNo As System.Windows.Forms.Label
    Friend WithEvents chkFinalPayment As System.Windows.Forms.CheckBox
    Friend WithEvents chkCustomerCopy As System.Windows.Forms.CheckBox
End Class
