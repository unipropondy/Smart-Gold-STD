<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDeposit
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDeposit))
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.grpMain = New System.Windows.Forms.GroupBox()
        Me.txtInventoryCode2 = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtInventoryCode1 = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtInventoryCode = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtBalanceAmount = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtAmountPaid = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtTotalAmount = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtOrderNo = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.optCash = New System.Windows.Forms.RadioButton()
        Me.optGold = New System.Windows.Forms.RadioButton()
        Me.pnlStatus = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.txtIssYY = New System.Windows.Forms.TextBox()
        Me.txtIssMM = New System.Windows.Forms.TextBox()
        Me.txtIssDD = New System.Windows.Forms.TextBox()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.txtOYYYY = New System.Windows.Forms.TextBox()
        Me.txtOMM = New System.Windows.Forms.TextBox()
        Me.txtODD = New System.Windows.Forms.TextBox()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.txtEstiYY = New System.Windows.Forms.TextBox()
        Me.txtEstiMM = New System.Windows.Forms.TextBox()
        Me.txtEstiDD = New System.Windows.Forms.TextBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnSupplierCodeSearch = New System.Windows.Forms.Button()
        Me.txtSupplierCode = New System.Windows.Forms.TextBox()
        Me.btnOrderPaymentNoSearch = New System.Windows.Forms.Button()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtTotalCost = New System.Windows.Forms.TextBox()
        Me.btnCustomerCodeSearch = New System.Windows.Forms.Button()
        Me.btnNewCustomer = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblCustomerType = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtDepositAmount = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtNoofInstallment = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtInstallmentAmount = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtIYYYY = New System.Windows.Forms.TextBox()
        Me.txtIMM = New System.Windows.Forms.TextBox()
        Me.txtIDD = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtAddress3 = New System.Windows.Forms.TextBox()
        Me.txtAddress2 = New System.Windows.Forms.TextBox()
        Me.txtAddress1 = New System.Windows.Forms.TextBox()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.txtCustomerCode = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.PnlSalesManPwd = New System.Windows.Forms.Panel()
        Me.txtSalesManPWD = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpMain.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.pnlStatus.SuspendLayout()
        Me.PnlSalesManPwd.SuspendLayout()
        Me.SuspendLayout()
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(1, 12)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 100)
        Me.pic.TabIndex = 84
        Me.pic.TabStop = False
        '
        'grpMain
        '
        Me.grpMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpMain.Controls.Add(Me.Label23)
        Me.grpMain.Controls.Add(Me.txtInventoryCode2)
        Me.grpMain.Controls.Add(Me.Label22)
        Me.grpMain.Controls.Add(Me.txtInventoryCode1)
        Me.grpMain.Controls.Add(Me.Label20)
        Me.grpMain.Controls.Add(Me.txtInventoryCode)
        Me.grpMain.Controls.Add(Me.Label19)
        Me.grpMain.Controls.Add(Me.txtBalanceAmount)
        Me.grpMain.Controls.Add(Me.Label18)
        Me.grpMain.Controls.Add(Me.txtAmountPaid)
        Me.grpMain.Controls.Add(Me.Label17)
        Me.grpMain.Controls.Add(Me.txtTotalAmount)
        Me.grpMain.Controls.Add(Me.Label16)
        Me.grpMain.Controls.Add(Me.txtOrderNo)
        Me.grpMain.Controls.Add(Me.Label15)
        Me.grpMain.Controls.Add(Me.Label12)
        Me.grpMain.Controls.Add(Me.GroupBox2)
        Me.grpMain.Controls.Add(Me.pnlStatus)
        Me.grpMain.Controls.Add(Me.Label51)
        Me.grpMain.Controls.Add(Me.Label52)
        Me.grpMain.Controls.Add(Me.txtIssYY)
        Me.grpMain.Controls.Add(Me.txtIssMM)
        Me.grpMain.Controls.Add(Me.txtIssDD)
        Me.grpMain.Controls.Add(Me.Label53)
        Me.grpMain.Controls.Add(Me.Label43)
        Me.grpMain.Controls.Add(Me.Label44)
        Me.grpMain.Controls.Add(Me.txtOYYYY)
        Me.grpMain.Controls.Add(Me.txtOMM)
        Me.grpMain.Controls.Add(Me.txtODD)
        Me.grpMain.Controls.Add(Me.lblDate)
        Me.grpMain.Controls.Add(Me.Label37)
        Me.grpMain.Controls.Add(Me.Label38)
        Me.grpMain.Controls.Add(Me.txtEstiYY)
        Me.grpMain.Controls.Add(Me.txtEstiMM)
        Me.grpMain.Controls.Add(Me.txtEstiDD)
        Me.grpMain.Controls.Add(Me.Label39)
        Me.grpMain.Controls.Add(Me.Label10)
        Me.grpMain.Controls.Add(Me.btnSupplierCodeSearch)
        Me.grpMain.Controls.Add(Me.txtSupplierCode)
        Me.grpMain.Controls.Add(Me.btnOrderPaymentNoSearch)
        Me.grpMain.Controls.Add(Me.Label29)
        Me.grpMain.Controls.Add(Me.txtTotalCost)
        Me.grpMain.Controls.Add(Me.btnCustomerCodeSearch)
        Me.grpMain.Controls.Add(Me.btnNewCustomer)
        Me.grpMain.Controls.Add(Me.Label14)
        Me.grpMain.Controls.Add(Me.lblCustomerType)
        Me.grpMain.Controls.Add(Me.Label13)
        Me.grpMain.Controls.Add(Me.txtDepositAmount)
        Me.grpMain.Controls.Add(Me.Label9)
        Me.grpMain.Controls.Add(Me.txtNoofInstallment)
        Me.grpMain.Controls.Add(Me.Label8)
        Me.grpMain.Controls.Add(Me.txtInstallmentAmount)
        Me.grpMain.Controls.Add(Me.Label6)
        Me.grpMain.Controls.Add(Me.Label3)
        Me.grpMain.Controls.Add(Me.Label4)
        Me.grpMain.Controls.Add(Me.txtIYYYY)
        Me.grpMain.Controls.Add(Me.txtIMM)
        Me.grpMain.Controls.Add(Me.txtIDD)
        Me.grpMain.Controls.Add(Me.Label7)
        Me.grpMain.Controls.Add(Me.Label1)
        Me.grpMain.Controls.Add(Me.txtRemarks)
        Me.grpMain.Controls.Add(Me.txtAddress3)
        Me.grpMain.Controls.Add(Me.txtAddress2)
        Me.grpMain.Controls.Add(Me.txtAddress1)
        Me.grpMain.Controls.Add(Me.txtCustomerName)
        Me.grpMain.Controls.Add(Me.txtCustomerCode)
        Me.grpMain.Controls.Add(Me.Label5)
        Me.grpMain.Controls.Add(Me.txtInvoiceNo)
        Me.grpMain.Controls.Add(Me.Label2)
        Me.grpMain.Location = New System.Drawing.Point(1, 118)
        Me.grpMain.Name = "grpMain"
        Me.grpMain.Size = New System.Drawing.Size(625, 246)
        Me.grpMain.TabIndex = 86
        Me.grpMain.TabStop = False
        Me.grpMain.Text = "Header"
        '
        'txtInventoryCode2
        '
        Me.txtInventoryCode2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInventoryCode2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInventoryCode2.ForeColor = System.Drawing.Color.Black
        Me.txtInventoryCode2.Location = New System.Drawing.Point(335, 138)
        Me.txtInventoryCode2.MaxLength = 20
        Me.txtInventoryCode2.Name = "txtInventoryCode2"
        Me.txtInventoryCode2.Size = New System.Drawing.Size(75, 20)
        Me.txtInventoryCode2.TabIndex = 476289
        Me.txtInventoryCode2.Tag = ""
        '
        'Label22
        '
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.Black
        Me.Label22.Location = New System.Drawing.Point(271, 136)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(62, 29)
        Me.Label22.TabIndex = 476290
        Me.Label22.Text = "Inventory Code"
        '
        'txtInventoryCode1
        '
        Me.txtInventoryCode1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInventoryCode1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInventoryCode1.ForeColor = System.Drawing.Color.Black
        Me.txtInventoryCode1.Location = New System.Drawing.Point(335, 111)
        Me.txtInventoryCode1.MaxLength = 20
        Me.txtInventoryCode1.Name = "txtInventoryCode1"
        Me.txtInventoryCode1.Size = New System.Drawing.Size(75, 20)
        Me.txtInventoryCode1.TabIndex = 476287
        Me.txtInventoryCode1.Tag = ""
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.Location = New System.Drawing.Point(271, 109)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(62, 29)
        Me.Label20.TabIndex = 476288
        Me.Label20.Text = "Inventory Code"
        '
        'txtInventoryCode
        '
        Me.txtInventoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInventoryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInventoryCode.ForeColor = System.Drawing.Color.Black
        Me.txtInventoryCode.Location = New System.Drawing.Point(335, 85)
        Me.txtInventoryCode.MaxLength = 20
        Me.txtInventoryCode.Name = "txtInventoryCode"
        Me.txtInventoryCode.Size = New System.Drawing.Size(75, 20)
        Me.txtInventoryCode.TabIndex = 2
        Me.txtInventoryCode.Tag = ""
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(271, 83)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(62, 29)
        Me.Label19.TabIndex = 476286
        Me.Label19.Text = "Inventory Code"
        '
        'txtBalanceAmount
        '
        Me.txtBalanceAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBalanceAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBalanceAmount.ForeColor = System.Drawing.Color.Black
        Me.txtBalanceAmount.Location = New System.Drawing.Point(105, 195)
        Me.txtBalanceAmount.MaxLength = 20
        Me.txtBalanceAmount.Name = "txtBalanceAmount"
        Me.txtBalanceAmount.ReadOnly = True
        Me.txtBalanceAmount.Size = New System.Drawing.Size(110, 20)
        Me.txtBalanceAmount.TabIndex = 476284
        Me.txtBalanceAmount.Tag = ""
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(9, 195)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(99, 15)
        Me.Label18.TabIndex = 476285
        Me.Label18.Text = "Balance Amount"
        '
        'txtAmountPaid
        '
        Me.txtAmountPaid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmountPaid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmountPaid.ForeColor = System.Drawing.Color.Black
        Me.txtAmountPaid.Location = New System.Drawing.Point(105, 169)
        Me.txtAmountPaid.MaxLength = 20
        Me.txtAmountPaid.Name = "txtAmountPaid"
        Me.txtAmountPaid.Size = New System.Drawing.Size(110, 20)
        Me.txtAmountPaid.TabIndex = 5
        Me.txtAmountPaid.Tag = ""
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(9, 169)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(99, 15)
        Me.Label17.TabIndex = 476283
        Me.Label17.Text = "Amount Paid"
        '
        'txtTotalAmount
        '
        Me.txtTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalAmount.ForeColor = System.Drawing.Color.Black
        Me.txtTotalAmount.Location = New System.Drawing.Point(105, 120)
        Me.txtTotalAmount.MaxLength = 20
        Me.txtTotalAmount.Name = "txtTotalAmount"
        Me.txtTotalAmount.Size = New System.Drawing.Size(110, 20)
        Me.txtTotalAmount.TabIndex = 3
        Me.txtTotalAmount.Tag = ""
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(9, 120)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(99, 15)
        Me.Label16.TabIndex = 476281
        Me.Label16.Text = "Total Amount"
        '
        'txtOrderNo
        '
        Me.txtOrderNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOrderNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOrderNo.ForeColor = System.Drawing.Color.Black
        Me.txtOrderNo.Location = New System.Drawing.Point(303, 21)
        Me.txtOrderNo.MaxLength = 20
        Me.txtOrderNo.Name = "txtOrderNo"
        Me.txtOrderNo.Size = New System.Drawing.Size(85, 20)
        Me.txtOrderNo.TabIndex = 476278
        Me.txtOrderNo.Tag = ""
        Me.txtOrderNo.Visible = False
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(231, 21)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(68, 15)
        Me.Label15.TabIndex = 476279
        Me.Label15.Text = "Order No"
        Me.Label15.Visible = False
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(9, 339)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(82, 15)
        Me.Label12.TabIndex = 476277
        Me.Label12.Text = "Payable"
        Me.Label12.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.optCash)
        Me.GroupBox2.Controls.Add(Me.optGold)
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(105, 329)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(140, 32)
        Me.GroupBox2.TabIndex = 476276
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Visible = False
        '
        'optCash
        '
        Me.optCash.Checked = True
        Me.optCash.Location = New System.Drawing.Point(80, 10)
        Me.optCash.Name = "optCash"
        Me.optCash.Size = New System.Drawing.Size(56, 15)
        Me.optCash.TabIndex = 18
        Me.optCash.TabStop = True
        Me.optCash.Text = "Cash"
        '
        'optGold
        '
        Me.optGold.Location = New System.Drawing.Point(8, 10)
        Me.optGold.Name = "optGold"
        Me.optGold.Size = New System.Drawing.Size(56, 15)
        Me.optGold.TabIndex = 17
        Me.optGold.Text = "Gold"
        '
        'pnlStatus
        '
        Me.pnlStatus.Controls.Add(Me.Label11)
        Me.pnlStatus.Controls.Add(Me.cboStatus)
        Me.pnlStatus.Location = New System.Drawing.Point(425, 329)
        Me.pnlStatus.Name = "pnlStatus"
        Me.pnlStatus.Size = New System.Drawing.Size(184, 32)
        Me.pnlStatus.TabIndex = 476275
        Me.pnlStatus.Visible = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label11.Location = New System.Drawing.Point(8, 8)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(42, 14)
        Me.Label11.TabIndex = 500
        Me.Label11.Text = "Status"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.Location = New System.Drawing.Point(80, 4)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(96, 21)
        Me.cboStatus.TabIndex = 499
        '
        'Label51
        '
        Me.Label51.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.Location = New System.Drawing.Point(549, 82)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(8, 16)
        Me.Label51.TabIndex = 476274
        Me.Label51.Text = "-"
        Me.Label51.Visible = False
        '
        'Label52
        '
        Me.Label52.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.Location = New System.Drawing.Point(517, 82)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(8, 16)
        Me.Label52.TabIndex = 476273
        Me.Label52.Text = "-"
        Me.Label52.Visible = False
        '
        'txtIssYY
        '
        Me.txtIssYY.BackColor = System.Drawing.Color.White
        Me.txtIssYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIssYY.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIssYY.Location = New System.Drawing.Point(559, 82)
        Me.txtIssYY.MaxLength = 4
        Me.txtIssYY.Name = "txtIssYY"
        Me.txtIssYY.Size = New System.Drawing.Size(38, 22)
        Me.txtIssYY.TabIndex = 476272
        Me.txtIssYY.Tag = "ENN;"
        Me.txtIssYY.Visible = False
        '
        'txtIssMM
        '
        Me.txtIssMM.BackColor = System.Drawing.Color.White
        Me.txtIssMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIssMM.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIssMM.Location = New System.Drawing.Point(525, 82)
        Me.txtIssMM.MaxLength = 2
        Me.txtIssMM.Name = "txtIssMM"
        Me.txtIssMM.Size = New System.Drawing.Size(24, 22)
        Me.txtIssMM.TabIndex = 476271
        Me.txtIssMM.Tag = "ENN;"
        Me.txtIssMM.Visible = False
        '
        'txtIssDD
        '
        Me.txtIssDD.BackColor = System.Drawing.Color.White
        Me.txtIssDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIssDD.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIssDD.Location = New System.Drawing.Point(493, 82)
        Me.txtIssDD.MaxLength = 2
        Me.txtIssDD.Name = "txtIssDD"
        Me.txtIssDD.Size = New System.Drawing.Size(24, 22)
        Me.txtIssDD.TabIndex = 476270
        Me.txtIssDD.Tag = "ENN;"
        Me.txtIssDD.Visible = False
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.Location = New System.Drawing.Point(393, 82)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(65, 14)
        Me.Label53.TabIndex = 476269
        Me.Label53.Text = "Issue Date"
        Me.Label53.Visible = False
        '
        'Label43
        '
        Me.Label43.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.Location = New System.Drawing.Point(551, 48)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(8, 16)
        Me.Label43.TabIndex = 772
        Me.Label43.Text = "-"
        '
        'Label44
        '
        Me.Label44.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(519, 48)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(8, 16)
        Me.Label44.TabIndex = 771
        Me.Label44.Text = "-"
        '
        'txtOYYYY
        '
        Me.txtOYYYY.BackColor = System.Drawing.Color.White
        Me.txtOYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOYYYY.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOYYYY.Location = New System.Drawing.Point(561, 52)
        Me.txtOYYYY.MaxLength = 4
        Me.txtOYYYY.Name = "txtOYYYY"
        Me.txtOYYYY.Size = New System.Drawing.Size(38, 22)
        Me.txtOYYYY.TabIndex = 769
        Me.txtOYYYY.Tag = "ENN;"
        '
        'txtOMM
        '
        Me.txtOMM.BackColor = System.Drawing.Color.White
        Me.txtOMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOMM.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOMM.Location = New System.Drawing.Point(527, 52)
        Me.txtOMM.MaxLength = 2
        Me.txtOMM.Name = "txtOMM"
        Me.txtOMM.Size = New System.Drawing.Size(24, 22)
        Me.txtOMM.TabIndex = 768
        Me.txtOMM.Tag = "ENN;"
        '
        'txtODD
        '
        Me.txtODD.BackColor = System.Drawing.Color.White
        Me.txtODD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtODD.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtODD.Location = New System.Drawing.Point(493, 52)
        Me.txtODD.MaxLength = 2
        Me.txtODD.Name = "txtODD"
        Me.txtODD.Size = New System.Drawing.Size(24, 22)
        Me.txtODD.TabIndex = 767
        Me.txtODD.Tag = "ENN;"
        '
        'lblDate
        '
        Me.lblDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.ForeColor = System.Drawing.Color.Black
        Me.lblDate.Location = New System.Drawing.Point(391, 54)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(84, 20)
        Me.lblDate.TabIndex = 770
        Me.lblDate.Text = "Deposit Date"
        '
        'Label37
        '
        Me.Label37.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(361, 167)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(8, 22)
        Me.Label37.TabIndex = 765
        Me.Label37.Text = "-"
        '
        'Label38
        '
        Me.Label38.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(393, 167)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(8, 22)
        Me.Label38.TabIndex = 766
        Me.Label38.Text = "-"
        '
        'txtEstiYY
        '
        Me.txtEstiYY.BackColor = System.Drawing.Color.White
        Me.txtEstiYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEstiYY.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEstiYY.Location = New System.Drawing.Point(401, 167)
        Me.txtEstiYY.MaxLength = 4
        Me.txtEstiYY.Name = "txtEstiYY"
        Me.txtEstiYY.Size = New System.Drawing.Size(40, 22)
        Me.txtEstiYY.TabIndex = 763
        Me.txtEstiYY.Tag = "ENN;"
        '
        'txtEstiMM
        '
        Me.txtEstiMM.BackColor = System.Drawing.Color.White
        Me.txtEstiMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEstiMM.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEstiMM.Location = New System.Drawing.Point(369, 167)
        Me.txtEstiMM.MaxLength = 2
        Me.txtEstiMM.Name = "txtEstiMM"
        Me.txtEstiMM.Size = New System.Drawing.Size(24, 22)
        Me.txtEstiMM.TabIndex = 762
        Me.txtEstiMM.Tag = "ENN;"
        '
        'txtEstiDD
        '
        Me.txtEstiDD.BackColor = System.Drawing.Color.White
        Me.txtEstiDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEstiDD.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEstiDD.Location = New System.Drawing.Point(335, 167)
        Me.txtEstiDD.MaxLength = 2
        Me.txtEstiDD.Name = "txtEstiDD"
        Me.txtEstiDD.Size = New System.Drawing.Size(24, 22)
        Me.txtEstiDD.TabIndex = 761
        Me.txtEstiDD.Tag = "ENN;"
        '
        'Label39
        '
        Me.Label39.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.Color.Black
        Me.Label39.Location = New System.Drawing.Point(391, 25)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(96, 15)
        Me.Label39.TabIndex = 764
        Me.Label39.Text = "Date of Delivery"
        Me.Label39.Visible = False
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(257, 334)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(64, 28)
        Me.Label10.TabIndex = 555
        Me.Label10.Text = "Supplier Code"
        Me.Label10.Visible = False
        '
        'btnSupplierCodeSearch
        '
        Me.btnSupplierCodeSearch.BackColor = System.Drawing.Color.Orange
        Me.btnSupplierCodeSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSupplierCodeSearch.Location = New System.Drawing.Point(391, 334)
        Me.btnSupplierCodeSearch.Name = "btnSupplierCodeSearch"
        Me.btnSupplierCodeSearch.Size = New System.Drawing.Size(28, 20)
        Me.btnSupplierCodeSearch.TabIndex = 554
        Me.btnSupplierCodeSearch.Text = "..."
        Me.btnSupplierCodeSearch.UseVisualStyleBackColor = False
        Me.btnSupplierCodeSearch.Visible = False
        '
        'txtSupplierCode
        '
        Me.txtSupplierCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierCode.Location = New System.Drawing.Point(327, 334)
        Me.txtSupplierCode.MaxLength = 20
        Me.txtSupplierCode.Name = "txtSupplierCode"
        Me.txtSupplierCode.Size = New System.Drawing.Size(92, 20)
        Me.txtSupplierCode.TabIndex = 553
        Me.txtSupplierCode.Tag = "NE;"
        Me.txtSupplierCode.Visible = False
        '
        'btnOrderPaymentNoSearch
        '
        Me.btnOrderPaymentNoSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnOrderPaymentNoSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOrderPaymentNoSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOrderPaymentNoSearch.Location = New System.Drawing.Point(203, 19)
        Me.btnOrderPaymentNoSearch.Name = "btnOrderPaymentNoSearch"
        Me.btnOrderPaymentNoSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnOrderPaymentNoSearch.TabIndex = 552
        Me.btnOrderPaymentNoSearch.Text = "..."
        Me.btnOrderPaymentNoSearch.UseVisualStyleBackColor = False
        '
        'Label29
        '
        Me.Label29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.Color.Black
        Me.Label29.Location = New System.Drawing.Point(541, 56)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(99, 15)
        Me.Label29.TabIndex = 162
        Me.Label29.Text = "Total Amount"
        Me.Label29.Visible = False
        '
        'txtTotalCost
        '
        Me.txtTotalCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalCost.Enabled = False
        Me.txtTotalCost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalCost.ForeColor = System.Drawing.Color.Black
        Me.txtTotalCost.Location = New System.Drawing.Point(653, 56)
        Me.txtTotalCost.MaxLength = 20
        Me.txtTotalCost.Name = "txtTotalCost"
        Me.txtTotalCost.Size = New System.Drawing.Size(110, 20)
        Me.txtTotalCost.TabIndex = 161
        Me.txtTotalCost.Tag = ""
        Me.txtTotalCost.Visible = False
        '
        'btnCustomerCodeSearch
        '
        Me.btnCustomerCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCustomerCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCustomerCodeSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomerCodeSearch.Location = New System.Drawing.Point(201, 51)
        Me.btnCustomerCodeSearch.Name = "btnCustomerCodeSearch"
        Me.btnCustomerCodeSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnCustomerCodeSearch.TabIndex = 2
        Me.btnCustomerCodeSearch.Text = "..."
        Me.btnCustomerCodeSearch.UseVisualStyleBackColor = False
        '
        'btnNewCustomer
        '
        Me.btnNewCustomer.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnNewCustomer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNewCustomer.Image = CType(resources.GetObject("btnNewCustomer.Image"), System.Drawing.Image)
        Me.btnNewCustomer.Location = New System.Drawing.Point(233, 48)
        Me.btnNewCustomer.Name = "btnNewCustomer"
        Me.btnNewCustomer.Size = New System.Drawing.Size(64, 23)
        Me.btnNewCustomer.TabIndex = 158
        Me.btnNewCustomer.Text = "New"
        Me.btnNewCustomer.UseVisualStyleBackColor = False
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(9, 83)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(82, 15)
        Me.Label14.TabIndex = 156
        Me.Label14.Text = "Name"
        '
        'lblCustomerType
        '
        Me.lblCustomerType.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblCustomerType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustomerType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerType.ForeColor = System.Drawing.Color.Red
        Me.lblCustomerType.Location = New System.Drawing.Point(303, 49)
        Me.lblCustomerType.Name = "lblCustomerType"
        Me.lblCustomerType.Size = New System.Drawing.Size(122, 22)
        Me.lblCustomerType.TabIndex = 155
        Me.lblCustomerType.Text = "Customer Type"
        Me.lblCustomerType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblCustomerType.Visible = False
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(9, 343)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(82, 15)
        Me.Label13.TabIndex = 154
        Me.Label13.Text = "Address"
        Me.Label13.Visible = False
        '
        'txtDepositAmount
        '
        Me.txtDepositAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDepositAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepositAmount.ForeColor = System.Drawing.Color.Black
        Me.txtDepositAmount.Location = New System.Drawing.Point(105, 143)
        Me.txtDepositAmount.MaxLength = 20
        Me.txtDepositAmount.Name = "txtDepositAmount"
        Me.txtDepositAmount.Size = New System.Drawing.Size(110, 20)
        Me.txtDepositAmount.TabIndex = 4
        Me.txtDepositAmount.Tag = ""
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(9, 143)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(99, 15)
        Me.Label9.TabIndex = 153
        Me.Label9.Text = "Deposit Amount"
        '
        'txtNoofInstallment
        '
        Me.txtNoofInstallment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNoofInstallment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoofInstallment.ForeColor = System.Drawing.Color.Black
        Me.txtNoofInstallment.Location = New System.Drawing.Point(663, 83)
        Me.txtNoofInstallment.MaxLength = 20
        Me.txtNoofInstallment.Name = "txtNoofInstallment"
        Me.txtNoofInstallment.Size = New System.Drawing.Size(110, 20)
        Me.txtNoofInstallment.TabIndex = 140
        Me.txtNoofInstallment.Tag = ""
        Me.txtNoofInstallment.Text = "1"
        Me.txtNoofInstallment.Visible = False
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(551, 83)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(107, 15)
        Me.Label8.TabIndex = 152
        Me.Label8.Text = "No. of Installment"
        Me.Label8.Visible = False
        '
        'txtInstallmentAmount
        '
        Me.txtInstallmentAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInstallmentAmount.Enabled = False
        Me.txtInstallmentAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInstallmentAmount.ForeColor = System.Drawing.Color.Black
        Me.txtInstallmentAmount.Location = New System.Drawing.Point(653, 120)
        Me.txtInstallmentAmount.MaxLength = 20
        Me.txtInstallmentAmount.Name = "txtInstallmentAmount"
        Me.txtInstallmentAmount.Size = New System.Drawing.Size(110, 20)
        Me.txtInstallmentAmount.TabIndex = 141
        Me.txtInstallmentAmount.Tag = ""
        Me.txtInstallmentAmount.Visible = False
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(541, 120)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(107, 15)
        Me.Label6.TabIndex = 151
        Me.Label6.Text = "Installment Amout"
        Me.Label6.Visible = False
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(714, 155)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(11, 16)
        Me.Label3.TabIndex = 150
        Me.Label3.Text = "-"
        Me.Label3.Visible = False
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(679, 155)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(11, 16)
        Me.Label4.TabIndex = 149
        Me.Label4.Text = "-"
        Me.Label4.Visible = False
        '
        'txtIYYYY
        '
        Me.txtIYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIYYYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIYYYY.Location = New System.Drawing.Point(727, 155)
        Me.txtIYYYY.MaxLength = 4
        Me.txtIYYYY.Name = "txtIYYYY"
        Me.txtIYYYY.Size = New System.Drawing.Size(35, 20)
        Me.txtIYYYY.TabIndex = 144
        Me.txtIYYYY.Tag = "ENN;"
        Me.txtIYYYY.Visible = False
        '
        'txtIMM
        '
        Me.txtIMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIMM.Location = New System.Drawing.Point(690, 155)
        Me.txtIMM.MaxLength = 2
        Me.txtIMM.Name = "txtIMM"
        Me.txtIMM.Size = New System.Drawing.Size(24, 20)
        Me.txtIMM.TabIndex = 143
        Me.txtIMM.Tag = "ENN;"
        Me.txtIMM.Visible = False
        '
        'txtIDD
        '
        Me.txtIDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIDD.Location = New System.Drawing.Point(653, 155)
        Me.txtIDD.MaxLength = 2
        Me.txtIDD.Name = "txtIDD"
        Me.txtIDD.Size = New System.Drawing.Size(24, 20)
        Me.txtIDD.TabIndex = 142
        Me.txtIDD.Tag = "ENN;"
        Me.txtIDD.Visible = False
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(541, 155)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(75, 20)
        Me.Label7.TabIndex = 148
        Me.Label7.Text = "Due Date"
        Me.Label7.Visible = False
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(247, 200)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 15)
        Me.Label1.TabIndex = 147
        Me.Label1.Text = "Remarks"
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.Black
        Me.txtRemarks.Location = New System.Drawing.Point(330, 201)
        Me.txtRemarks.MaxLength = 200
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(267, 37)
        Me.txtRemarks.TabIndex = 6
        Me.txtRemarks.Tag = ""
        '
        'txtAddress3
        '
        Me.txtAddress3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress3.ForeColor = System.Drawing.Color.Black
        Me.txtAddress3.Location = New System.Drawing.Point(289, 375)
        Me.txtAddress3.MaxLength = 20
        Me.txtAddress3.Name = "txtAddress3"
        Me.txtAddress3.Size = New System.Drawing.Size(136, 20)
        Me.txtAddress3.TabIndex = 137
        Me.txtAddress3.Tag = ""
        Me.txtAddress3.Visible = False
        '
        'txtAddress2
        '
        Me.txtAddress2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress2.ForeColor = System.Drawing.Color.Black
        Me.txtAddress2.Location = New System.Drawing.Point(105, 375)
        Me.txtAddress2.MaxLength = 20
        Me.txtAddress2.Name = "txtAddress2"
        Me.txtAddress2.ReadOnly = True
        Me.txtAddress2.Size = New System.Drawing.Size(176, 20)
        Me.txtAddress2.TabIndex = 136
        Me.txtAddress2.Tag = ""
        Me.txtAddress2.Visible = False
        '
        'txtAddress1
        '
        Me.txtAddress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress1.ForeColor = System.Drawing.Color.Black
        Me.txtAddress1.Location = New System.Drawing.Point(105, 343)
        Me.txtAddress1.MaxLength = 20
        Me.txtAddress1.Name = "txtAddress1"
        Me.txtAddress1.ReadOnly = True
        Me.txtAddress1.Size = New System.Drawing.Size(320, 20)
        Me.txtAddress1.TabIndex = 135
        Me.txtAddress1.Tag = ""
        Me.txtAddress1.Visible = False
        '
        'txtCustomerName
        '
        Me.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerName.ForeColor = System.Drawing.Color.Black
        Me.txtCustomerName.Location = New System.Drawing.Point(105, 83)
        Me.txtCustomerName.MaxLength = 20
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(152, 20)
        Me.txtCustomerName.TabIndex = 134
        Me.txtCustomerName.Tag = ""
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerCode.ForeColor = System.Drawing.Color.Black
        Me.txtCustomerCode.Location = New System.Drawing.Point(105, 51)
        Me.txtCustomerCode.MaxLength = 20
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.Size = New System.Drawing.Size(120, 20)
        Me.txtCustomerCode.TabIndex = 1
        Me.txtCustomerCode.Tag = ""
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(9, 51)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 15)
        Me.Label5.TabIndex = 146
        Me.Label5.Text = "Customer Code"
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvoiceNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceNo.ForeColor = System.Drawing.Color.Black
        Me.txtInvoiceNo.Location = New System.Drawing.Point(105, 19)
        Me.txtInvoiceNo.MaxLength = 20
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(120, 20)
        Me.txtInvoiceNo.TabIndex = 132
        Me.txtInvoiceNo.Tag = ""
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(9, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 15)
        Me.Label2.TabIndex = 145
        Me.Label2.Text = "Deposit No"
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPrint.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(92, 375)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(100, 40)
        Me.btnPrint.TabIndex = 558
        Me.btnPrint.Text = "Print (F8)"
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDelete.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(198, 376)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(100, 40)
        Me.btnDelete.TabIndex = 557
        Me.btnDelete.Text = "Delete (F3)"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClear.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.Location = New System.Drawing.Point(304, 375)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(100, 40)
        Me.btnClear.TabIndex = 555
        Me.btnClear.Text = "Clear (F6)"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(410, 375)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(110, 40)
        Me.btnSave.TabIndex = 7
        Me.btnSave.Text = "Save (F7)"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(526, 375)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 556
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'PnlSalesManPwd
        '
        Me.PnlSalesManPwd.BackColor = System.Drawing.Color.Pink
        Me.PnlSalesManPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PnlSalesManPwd.Controls.Add(Me.txtSalesManPWD)
        Me.PnlSalesManPwd.Controls.Add(Me.Label21)
        Me.PnlSalesManPwd.Location = New System.Drawing.Point(623, 286)
        Me.PnlSalesManPwd.Name = "PnlSalesManPwd"
        Me.PnlSalesManPwd.Size = New System.Drawing.Size(199, 88)
        Me.PnlSalesManPwd.TabIndex = 559
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
        'Label23
        '
        Me.Label23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.Black
        Me.Label23.Location = New System.Drawing.Point(233, 171)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(88, 15)
        Me.Label23.TabIndex = 476291
        Me.Label23.Text = "Payment Date"
        '
        'frmDeposit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(644, 432)
        Me.Controls.Add(Me.PnlSalesManPwd)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.grpMain)
        Me.Controls.Add(Me.pic)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmDeposit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Deposit"
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpMain.ResumeLayout(False)
        Me.grpMain.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.pnlStatus.ResumeLayout(False)
        Me.pnlStatus.PerformLayout()
        Me.PnlSalesManPwd.ResumeLayout(False)
        Me.PnlSalesManPwd.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents grpMain As System.Windows.Forms.GroupBox
    Friend WithEvents btnOrderPaymentNoSearch As System.Windows.Forms.Button
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtTotalCost As System.Windows.Forms.TextBox
    Friend WithEvents btnCustomerCodeSearch As System.Windows.Forms.Button
    Friend WithEvents btnNewCustomer As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblCustomerType As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtDepositAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtNoofInstallment As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtInstallmentAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtIYYYY As System.Windows.Forms.TextBox
    Friend WithEvents txtIMM As System.Windows.Forms.TextBox
    Friend WithEvents txtIDD As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress3 As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress2 As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress1 As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerCode As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtInvoiceNo As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents PnlSalesManPwd As System.Windows.Forms.Panel
    Friend WithEvents txtSalesManPWD As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnSupplierCodeSearch As System.Windows.Forms.Button
    Friend WithEvents txtSupplierCode As System.Windows.Forms.TextBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents txtOYYYY As System.Windows.Forms.TextBox
    Friend WithEvents txtOMM As System.Windows.Forms.TextBox
    Friend WithEvents txtODD As System.Windows.Forms.TextBox
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents txtEstiYY As System.Windows.Forms.TextBox
    Friend WithEvents txtEstiMM As System.Windows.Forms.TextBox
    Friend WithEvents txtEstiDD As System.Windows.Forms.TextBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents txtIssYY As System.Windows.Forms.TextBox
    Friend WithEvents txtIssMM As System.Windows.Forms.TextBox
    Friend WithEvents txtIssDD As System.Windows.Forms.TextBox
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents pnlStatus As System.Windows.Forms.Panel
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents optCash As System.Windows.Forms.RadioButton
    Friend WithEvents optGold As System.Windows.Forms.RadioButton
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtOrderNo As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtTotalAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtBalanceAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtAmountPaid As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtInventoryCode As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtInventoryCode2 As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtInventoryCode1 As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
End Class
