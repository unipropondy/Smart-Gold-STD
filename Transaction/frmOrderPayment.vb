Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmOrderPayment
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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents lblCustomerType As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress3 As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress2 As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress1 As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents btnCustomerCodeSearch As System.Windows.Forms.Button
    Friend WithEvents txtCustomerCode As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtOrderNo As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblBalanceAmount As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblEstimatedCost As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lblAdvancePaid As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPayAmount As System.Windows.Forms.TextBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents lblDepositAmount As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents pnlCancel As System.Windows.Forms.Panel
    Friend WithEvents btnBillCancel As System.Windows.Forms.Button
    Friend WithEvents btnBillCancelOK As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtOrderPaymentNo As System.Windows.Forms.TextBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnTradeIn As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents optBoth As System.Windows.Forms.RadioButton
    Friend WithEvents optCash As System.Windows.Forms.RadioButton
    Friend WithEvents optGold As System.Windows.Forms.RadioButton
    Friend WithEvents grdOrderPayment As System.Windows.Forms.DataGridView
    Friend WithEvents cNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cPaymentNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cRem As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cPaidAmt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents grdSales As System.Windows.Forms.DataGridView
    Friend WithEvents colInventoryCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colClass As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colGoldLoss As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCostPerGram As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColDSWt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSMC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTotalValue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTotalCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColSalesType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPMC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colGST As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkFinalPayment As System.Windows.Forms.CheckBox
    Friend WithEvents pnlCostAdminPwd As System.Windows.Forms.Panel
    Friend WithEvents btnCostAdminPwdCancel As System.Windows.Forms.Button
    Friend WithEvents btnCostAdminPwdOK As System.Windows.Forms.Button
    Friend WithEvents txtCostAdminPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtPassFind As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents opteTRSNo As System.Windows.Forms.RadioButton
    Friend WithEvents opteTrsTicketIssued As System.Windows.Forms.RadioButton
    Friend WithEvents opteTRSEligible As System.Windows.Forms.RadioButton
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents btnSubscriptionID As System.Windows.Forms.Button
    Friend WithEvents txtSubscriptionID As System.Windows.Forms.TextBox
    Friend WithEvents btnRePrint As System.Windows.Forms.Button
    Friend WithEvents PnlReprint As System.Windows.Forms.Panel
    Friend WithEvents btnReprintCancel As System.Windows.Forms.Button
    Friend WithEvents btnReprintOK As System.Windows.Forms.Button
    Friend WithEvents txtRePrintBillNo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnOrderPaymentNoSearch As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOrderPayment))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.btnSubscriptionID = New System.Windows.Forms.Button()
        Me.txtSubscriptionID = New System.Windows.Forms.TextBox()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.opteTRSNo = New System.Windows.Forms.RadioButton()
        Me.opteTrsTicketIssued = New System.Windows.Forms.RadioButton()
        Me.opteTRSEligible = New System.Windows.Forms.RadioButton()
        Me.chkFinalPayment = New System.Windows.Forms.CheckBox()
        Me.btnTradeIn = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.optBoth = New System.Windows.Forms.RadioButton()
        Me.optCash = New System.Windows.Forms.RadioButton()
        Me.optGold = New System.Windows.Forms.RadioButton()
        Me.txtPayAmount = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCustomerCode = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblCustomerType = New System.Windows.Forms.Label()
        Me.btnOrderPaymentNoSearch = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtAddress3 = New System.Windows.Forms.TextBox()
        Me.txtAddress2 = New System.Windows.Forms.TextBox()
        Me.txtAddress1 = New System.Windows.Forms.TextBox()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.btnCustomerCodeSearch = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtOrderNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblAdvancePaid = New System.Windows.Forms.Label()
        Me.lblEstimatedCost = New System.Windows.Forms.Label()
        Me.lblBalanceAmount = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.lblDepositAmount = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.pnlCancel = New System.Windows.Forms.Panel()
        Me.btnBillCancel = New System.Windows.Forms.Button()
        Me.btnBillCancelOK = New System.Windows.Forms.Button()
        Me.txtOrderPaymentNo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.grdOrderPayment = New System.Windows.Forms.DataGridView()
        Me.cNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cPaymentNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cRem = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cPaidAmt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.grdSales = New System.Windows.Forms.DataGridView()
        Me.colInventoryCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colClass = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDescription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colGoldLoss = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCostPerGram = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColDSWt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSMC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTotalValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTotalCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColSalesType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPMC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colGST = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pnlCostAdminPwd = New System.Windows.Forms.Panel()
        Me.btnCostAdminPwdCancel = New System.Windows.Forms.Button()
        Me.btnCostAdminPwdOK = New System.Windows.Forms.Button()
        Me.txtCostAdminPassword = New System.Windows.Forms.TextBox()
        Me.txtPassFind = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.btnRePrint = New System.Windows.Forms.Button()
        Me.PnlReprint = New System.Windows.Forms.Panel()
        Me.btnReprintCancel = New System.Windows.Forms.Button()
        Me.btnReprintOK = New System.Windows.Forms.Button()
        Me.txtRePrintBillNo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlCancel.SuspendLayout()
        CType(Me.grdOrderPayment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSales, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCostAdminPwd.SuspendLayout()
        Me.PnlReprint.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label35)
        Me.Panel1.Controls.Add(Me.btnSubscriptionID)
        Me.Panel1.Controls.Add(Me.txtSubscriptionID)
        Me.Panel1.Controls.Add(Me.Panel8)
        Me.Panel1.Controls.Add(Me.chkFinalPayment)
        Me.Panel1.Controls.Add(Me.btnTradeIn)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.txtPayAmount)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txtCustomerCode)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.lblCustomerType)
        Me.Panel1.Controls.Add(Me.btnOrderPaymentNoSearch)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtRemarks)
        Me.Panel1.Controls.Add(Me.txtAddress3)
        Me.Panel1.Controls.Add(Me.txtAddress2)
        Me.Panel1.Controls.Add(Me.txtAddress1)
        Me.Panel1.Controls.Add(Me.txtCustomerName)
        Me.Panel1.Controls.Add(Me.btnCustomerCodeSearch)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtOrderNo)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Location = New System.Drawing.Point(5, 103)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(658, 184)
        Me.Panel1.TabIndex = 2
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label35.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.Color.Black
        Me.Label35.Location = New System.Drawing.Point(233, 32)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(57, 22)
        Me.Label35.TabIndex = 774
        Me.Label35.Text = "Flexi ID"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnSubscriptionID
        '
        Me.btnSubscriptionID.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSubscriptionID.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSubscriptionID.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSubscriptionID.Location = New System.Drawing.Point(374, 34)
        Me.btnSubscriptionID.Name = "btnSubscriptionID"
        Me.btnSubscriptionID.Size = New System.Drawing.Size(24, 20)
        Me.btnSubscriptionID.TabIndex = 773
        Me.btnSubscriptionID.Text = "..."
        Me.btnSubscriptionID.UseVisualStyleBackColor = False
        '
        'txtSubscriptionID
        '
        Me.txtSubscriptionID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSubscriptionID.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubscriptionID.ForeColor = System.Drawing.Color.Black
        Me.txtSubscriptionID.Location = New System.Drawing.Point(294, 34)
        Me.txtSubscriptionID.MaxLength = 20
        Me.txtSubscriptionID.Name = "txtSubscriptionID"
        Me.txtSubscriptionID.Size = New System.Drawing.Size(80, 20)
        Me.txtSubscriptionID.TabIndex = 772
        Me.txtSubscriptionID.Tag = ""
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel8.Controls.Add(Me.opteTRSNo)
        Me.Panel8.Controls.Add(Me.opteTrsTicketIssued)
        Me.Panel8.Controls.Add(Me.opteTRSEligible)
        Me.Panel8.Location = New System.Drawing.Point(232, 4)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(331, 24)
        Me.Panel8.TabIndex = 771
        '
        'opteTRSNo
        '
        Me.opteTRSNo.Checked = True
        Me.opteTRSNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.opteTRSNo.ForeColor = System.Drawing.Color.Black
        Me.opteTRSNo.Location = New System.Drawing.Point(167, 0)
        Me.opteTRSNo.Name = "opteTRSNo"
        Me.opteTRSNo.Size = New System.Drawing.Size(162, 20)
        Me.opteTRSNo.TabIndex = 13
        Me.opteTRSNo.TabStop = True
        Me.opteTRSNo.Text = "eTRS Ticket Not Issued"
        '
        'opteTrsTicketIssued
        '
        Me.opteTrsTicketIssued.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.opteTrsTicketIssued.ForeColor = System.Drawing.Color.Black
        Me.opteTrsTicketIssued.Location = New System.Drawing.Point(11, 0)
        Me.opteTrsTicketIssued.Name = "opteTrsTicketIssued"
        Me.opteTrsTicketIssued.Size = New System.Drawing.Size(133, 20)
        Me.opteTrsTicketIssued.TabIndex = 5
        Me.opteTrsTicketIssued.Text = "eTRS Ticket Issued"
        '
        'opteTRSEligible
        '
        Me.opteTRSEligible.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.opteTRSEligible.ForeColor = System.Drawing.Color.Black
        Me.opteTRSEligible.Location = New System.Drawing.Point(283, 0)
        Me.opteTRSEligible.Name = "opteTRSEligible"
        Me.opteTRSEligible.Size = New System.Drawing.Size(103, 20)
        Me.opteTRSEligible.TabIndex = 3
        Me.opteTRSEligible.Text = "eTRS Eligible"
        Me.opteTRSEligible.Visible = False
        '
        'chkFinalPayment
        '
        Me.chkFinalPayment.AutoSize = True
        Me.chkFinalPayment.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkFinalPayment.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFinalPayment.Location = New System.Drawing.Point(548, 132)
        Me.chkFinalPayment.Name = "chkFinalPayment"
        Me.chkFinalPayment.Size = New System.Drawing.Size(105, 17)
        Me.chkFinalPayment.TabIndex = 770
        Me.chkFinalPayment.Text = "Final Payment"
        Me.chkFinalPayment.UseVisualStyleBackColor = True
        '
        'btnTradeIn
        '
        Me.btnTradeIn.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTradeIn.Location = New System.Drawing.Point(456, 127)
        Me.btnTradeIn.Name = "btnTradeIn"
        Me.btnTradeIn.Size = New System.Drawing.Size(86, 25)
        Me.btnTradeIn.TabIndex = 769
        Me.btnTradeIn.Text = "Trade IN"
        Me.btnTradeIn.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.optBoth)
        Me.GroupBox2.Controls.Add(Me.optCash)
        Me.GroupBox2.Controls.Add(Me.optGold)
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(228, 124)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(222, 26)
        Me.GroupBox2.TabIndex = 768
        Me.GroupBox2.TabStop = False
        '
        'optBoth
        '
        Me.optBoth.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.optBoth.Location = New System.Drawing.Point(164, 7)
        Me.optBoth.Name = "optBoth"
        Me.optBoth.Size = New System.Drawing.Size(51, 15)
        Me.optBoth.TabIndex = 19
        Me.optBoth.Text = "Both"
        '
        'optCash
        '
        Me.optCash.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.optCash.Checked = True
        Me.optCash.Location = New System.Drawing.Point(80, 7)
        Me.optCash.Name = "optCash"
        Me.optCash.Size = New System.Drawing.Size(56, 15)
        Me.optCash.TabIndex = 18
        Me.optCash.TabStop = True
        Me.optCash.Text = "Cash"
        '
        'optGold
        '
        Me.optGold.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.optGold.Location = New System.Drawing.Point(8, 7)
        Me.optGold.Name = "optGold"
        Me.optGold.Size = New System.Drawing.Size(56, 15)
        Me.optGold.TabIndex = 17
        Me.optGold.Text = "Gold"
        '
        'txtPayAmount
        '
        Me.txtPayAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPayAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayAmount.ForeColor = System.Drawing.Color.Black
        Me.txtPayAmount.Location = New System.Drawing.Point(104, 128)
        Me.txtPayAmount.MaxLength = 20
        Me.txtPayAmount.Name = "txtPayAmount"
        Me.txtPayAmount.Size = New System.Drawing.Size(96, 20)
        Me.txtPayAmount.TabIndex = 7
        Me.txtPayAmount.Tag = ""
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(8, 128)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 20)
        Me.Label3.TabIndex = 133
        Me.Label3.Text = "Pay Amount"
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerCode.Enabled = False
        Me.txtCustomerCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerCode.ForeColor = System.Drawing.Color.Black
        Me.txtCustomerCode.Location = New System.Drawing.Point(104, 32)
        Me.txtCustomerCode.MaxLength = 20
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.Size = New System.Drawing.Size(120, 20)
        Me.txtCustomerCode.TabIndex = 2
        Me.txtCustomerCode.Tag = ""
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(8, 56)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(96, 20)
        Me.Label16.TabIndex = 131
        Me.Label16.Text = "Name"
        '
        'lblCustomerType
        '
        Me.lblCustomerType.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblCustomerType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustomerType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerType.ForeColor = System.Drawing.Color.Red
        Me.lblCustomerType.Location = New System.Drawing.Point(452, 31)
        Me.lblCustomerType.Name = "lblCustomerType"
        Me.lblCustomerType.Size = New System.Drawing.Size(192, 20)
        Me.lblCustomerType.TabIndex = 129
        Me.lblCustomerType.Text = "Customer Type"
        Me.lblCustomerType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnOrderPaymentNoSearch
        '
        Me.btnOrderPaymentNoSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnOrderPaymentNoSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOrderPaymentNoSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOrderPaymentNoSearch.Location = New System.Drawing.Point(200, 8)
        Me.btnOrderPaymentNoSearch.Name = "btnOrderPaymentNoSearch"
        Me.btnOrderPaymentNoSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnOrderPaymentNoSearch.TabIndex = 1
        Me.btnOrderPaymentNoSearch.Text = "..."
        Me.btnOrderPaymentNoSearch.UseVisualStyleBackColor = False
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(8, 80)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(96, 20)
        Me.Label13.TabIndex = 100
        Me.Label13.Text = "Address"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(8, 152)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 20)
        Me.Label1.TabIndex = 71
        Me.Label1.Text = "Remarks"
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.Black
        Me.txtRemarks.Location = New System.Drawing.Point(104, 152)
        Me.txtRemarks.MaxLength = 100
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(320, 20)
        Me.txtRemarks.TabIndex = 8
        Me.txtRemarks.Tag = ""
        '
        'txtAddress3
        '
        Me.txtAddress3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress3.Enabled = False
        Me.txtAddress3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress3.ForeColor = System.Drawing.Color.Black
        Me.txtAddress3.Location = New System.Drawing.Point(288, 104)
        Me.txtAddress3.MaxLength = 20
        Me.txtAddress3.Name = "txtAddress3"
        Me.txtAddress3.Size = New System.Drawing.Size(136, 20)
        Me.txtAddress3.TabIndex = 6
        Me.txtAddress3.Tag = ""
        '
        'txtAddress2
        '
        Me.txtAddress2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress2.Enabled = False
        Me.txtAddress2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress2.ForeColor = System.Drawing.Color.Black
        Me.txtAddress2.Location = New System.Drawing.Point(104, 104)
        Me.txtAddress2.MaxLength = 20
        Me.txtAddress2.Name = "txtAddress2"
        Me.txtAddress2.Size = New System.Drawing.Size(176, 20)
        Me.txtAddress2.TabIndex = 5
        Me.txtAddress2.Tag = ""
        '
        'txtAddress1
        '
        Me.txtAddress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress1.Enabled = False
        Me.txtAddress1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress1.ForeColor = System.Drawing.Color.Black
        Me.txtAddress1.Location = New System.Drawing.Point(104, 80)
        Me.txtAddress1.MaxLength = 20
        Me.txtAddress1.Name = "txtAddress1"
        Me.txtAddress1.Size = New System.Drawing.Size(320, 20)
        Me.txtAddress1.TabIndex = 4
        Me.txtAddress1.Tag = ""
        '
        'txtCustomerName
        '
        Me.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerName.Enabled = False
        Me.txtCustomerName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerName.ForeColor = System.Drawing.Color.Black
        Me.txtCustomerName.Location = New System.Drawing.Point(104, 56)
        Me.txtCustomerName.MaxLength = 20
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(320, 20)
        Me.txtCustomerName.TabIndex = 3
        Me.txtCustomerName.Tag = ""
        '
        'btnCustomerCodeSearch
        '
        Me.btnCustomerCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCustomerCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCustomerCodeSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomerCodeSearch.Location = New System.Drawing.Point(200, 32)
        Me.btnCustomerCodeSearch.Name = "btnCustomerCodeSearch"
        Me.btnCustomerCodeSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnCustomerCodeSearch.TabIndex = 2
        Me.btnCustomerCodeSearch.Text = "..."
        Me.btnCustomerCodeSearch.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(8, 32)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 20)
        Me.Label5.TabIndex = 65
        Me.Label5.Text = "Customer Code"
        '
        'txtOrderNo
        '
        Me.txtOrderNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOrderNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOrderNo.ForeColor = System.Drawing.Color.Black
        Me.txtOrderNo.Location = New System.Drawing.Point(104, 8)
        Me.txtOrderNo.MaxLength = 20
        Me.txtOrderNo.Name = "txtOrderNo"
        Me.txtOrderNo.Size = New System.Drawing.Size(98, 20)
        Me.txtOrderNo.TabIndex = 0
        Me.txtOrderNo.Tag = ""
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(8, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 20)
        Me.Label2.TabIndex = 49
        Me.Label2.Text = "Order No"
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.lblAdvancePaid)
        Me.Panel2.Controls.Add(Me.lblEstimatedCost)
        Me.Panel2.Controls.Add(Me.lblBalanceAmount)
        Me.Panel2.Controls.Add(Me.Label15)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Location = New System.Drawing.Point(5, 421)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(658, 69)
        Me.Panel2.TabIndex = 114
        '
        'lblAdvancePaid
        '
        Me.lblAdvancePaid.BackColor = System.Drawing.Color.LightCyan
        Me.lblAdvancePaid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAdvancePaid.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAdvancePaid.ForeColor = System.Drawing.Color.Black
        Me.lblAdvancePaid.Location = New System.Drawing.Point(224, 35)
        Me.lblAdvancePaid.Name = "lblAdvancePaid"
        Me.lblAdvancePaid.Size = New System.Drawing.Size(216, 32)
        Me.lblAdvancePaid.TabIndex = 79
        Me.lblAdvancePaid.Text = "0.00"
        Me.lblAdvancePaid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblEstimatedCost
        '
        Me.lblEstimatedCost.BackColor = System.Drawing.Color.LightCyan
        Me.lblEstimatedCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblEstimatedCost.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEstimatedCost.ForeColor = System.Drawing.Color.Black
        Me.lblEstimatedCost.Location = New System.Drawing.Point(0, 35)
        Me.lblEstimatedCost.Name = "lblEstimatedCost"
        Me.lblEstimatedCost.Size = New System.Drawing.Size(224, 32)
        Me.lblEstimatedCost.TabIndex = 75
        Me.lblEstimatedCost.Text = "0.00"
        Me.lblEstimatedCost.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBalanceAmount
        '
        Me.lblBalanceAmount.BackColor = System.Drawing.Color.LightCyan
        Me.lblBalanceAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBalanceAmount.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalanceAmount.ForeColor = System.Drawing.Color.Black
        Me.lblBalanceAmount.Location = New System.Drawing.Point(440, 35)
        Me.lblBalanceAmount.Name = "lblBalanceAmount"
        Me.lblBalanceAmount.Size = New System.Drawing.Size(216, 32)
        Me.lblBalanceAmount.TabIndex = 76
        Me.lblBalanceAmount.Text = "0.00"
        Me.lblBalanceAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Pink
        Me.Label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(224, 4)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(216, 32)
        Me.Label15.TabIndex = 78
        Me.Label15.Text = "Advance Paid"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Pink
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(440, 4)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(216, 32)
        Me.Label11.TabIndex = 73
        Me.Label11.Text = "Balance Amount"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Pink
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(0, 4)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(224, 32)
        Me.Label10.TabIndex = 72
        Me.Label10.Text = "Estimated Cost"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(455, 496)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 40)
        Me.btnSave.TabIndex = 9
        Me.btnSave.Text = "Save (F7)"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(563, 496)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 11
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.Location = New System.Drawing.Point(349, 496)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(100, 40)
        Me.btnClear.TabIndex = 10
        Me.btnClear.Text = "Clear (F6)"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'lblDepositAmount
        '
        Me.lblDepositAmount.BackColor = System.Drawing.Color.LightCyan
        Me.lblDepositAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDepositAmount.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepositAmount.ForeColor = System.Drawing.Color.Black
        Me.lblDepositAmount.Location = New System.Drawing.Point(440, 176)
        Me.lblDepositAmount.Name = "lblDepositAmount"
        Me.lblDepositAmount.Size = New System.Drawing.Size(168, 32)
        Me.lblDepositAmount.TabIndex = 117
        Me.lblDepositAmount.Text = "0.00"
        Me.lblDepositAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblDepositAmount.Visible = False
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Pink
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label12.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(440, 144)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(168, 32)
        Me.Label12.TabIndex = 116
        Me.Label12.Text = "Deposit Amount"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label12.Visible = False
        '
        'pnlCancel
        '
        Me.pnlCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.pnlCancel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCancel.Controls.Add(Me.btnBillCancel)
        Me.pnlCancel.Controls.Add(Me.btnBillCancelOK)
        Me.pnlCancel.Controls.Add(Me.txtOrderPaymentNo)
        Me.pnlCancel.Controls.Add(Me.Label7)
        Me.pnlCancel.Controls.Add(Me.Label14)
        Me.pnlCancel.Location = New System.Drawing.Point(194, 203)
        Me.pnlCancel.Name = "pnlCancel"
        Me.pnlCancel.Size = New System.Drawing.Size(278, 88)
        Me.pnlCancel.TabIndex = 118
        Me.pnlCancel.Visible = False
        '
        'btnBillCancel
        '
        Me.btnBillCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnBillCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBillCancel.Image = CType(resources.GetObject("btnBillCancel.Image"), System.Drawing.Image)
        Me.btnBillCancel.Location = New System.Drawing.Point(200, 56)
        Me.btnBillCancel.Name = "btnBillCancel"
        Me.btnBillCancel.Size = New System.Drawing.Size(64, 23)
        Me.btnBillCancel.TabIndex = 75
        Me.btnBillCancel.Text = "Cancel"
        Me.btnBillCancel.UseVisualStyleBackColor = False
        '
        'btnBillCancelOK
        '
        Me.btnBillCancelOK.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnBillCancelOK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBillCancelOK.Image = CType(resources.GetObject("btnBillCancelOK.Image"), System.Drawing.Image)
        Me.btnBillCancelOK.Location = New System.Drawing.Point(128, 56)
        Me.btnBillCancelOK.Name = "btnBillCancelOK"
        Me.btnBillCancelOK.Size = New System.Drawing.Size(64, 23)
        Me.btnBillCancelOK.TabIndex = 74
        Me.btnBillCancelOK.Text = "OK"
        Me.btnBillCancelOK.UseVisualStyleBackColor = False
        '
        'txtOrderPaymentNo
        '
        Me.txtOrderPaymentNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOrderPaymentNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOrderPaymentNo.ForeColor = System.Drawing.Color.Black
        Me.txtOrderPaymentNo.Location = New System.Drawing.Point(128, 33)
        Me.txtOrderPaymentNo.MaxLength = 20
        Me.txtOrderPaymentNo.Name = "txtOrderPaymentNo"
        Me.txtOrderPaymentNo.Size = New System.Drawing.Size(136, 20)
        Me.txtOrderPaymentNo.TabIndex = 73
        Me.txtOrderPaymentNo.Tag = ""
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(8, 33)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(117, 15)
        Me.Label7.TabIndex = 72
        Me.Label7.Text = "Order Panyment No"
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Navy
        Me.Label14.Location = New System.Drawing.Point(8, 8)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(144, 15)
        Me.Label14.TabIndex = 72
        Me.Label14.Text = "Order Payment Cancel"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(242, 496)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(100, 40)
        Me.btnCancel.TabIndex = 119
        Me.btnCancel.Text = "Cancel (F8)"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'grdOrderPayment
        '
        Me.grdOrderPayment.AllowUserToAddRows = False
        Me.grdOrderPayment.AllowUserToDeleteRows = False
        Me.grdOrderPayment.AllowUserToResizeColumns = False
        Me.grdOrderPayment.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdOrderPayment.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdOrderPayment.ColumnHeadersHeight = 25
        Me.grdOrderPayment.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cNo, Me.cPaymentNo, Me.cRem, Me.cPaidAmt})
        Me.grdOrderPayment.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdOrderPayment.Location = New System.Drawing.Point(5, 290)
        Me.grdOrderPayment.Name = "grdOrderPayment"
        Me.grdOrderPayment.RowHeadersVisible = False
        Me.grdOrderPayment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdOrderPayment.Size = New System.Drawing.Size(657, 128)
        Me.grdOrderPayment.TabIndex = 121
        '
        'cNo
        '
        Me.cNo.HeaderText = "Order No"
        Me.cNo.Name = "cNo"
        '
        'cPaymentNo
        '
        Me.cPaymentNo.HeaderText = "Order Payment No"
        Me.cPaymentNo.Name = "cPaymentNo"
        Me.cPaymentNo.Width = 150
        '
        'cRem
        '
        Me.cRem.HeaderText = "Remarks"
        Me.cRem.Name = "cRem"
        Me.cRem.Width = 250
        '
        'cPaidAmt
        '
        Me.cPaidAmt.HeaderText = "Amount Paid"
        Me.cPaidAmt.Name = "cPaidAmt"
        Me.cPaidAmt.Width = 150
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(6, -3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(270, 100)
        Me.PictureBox1.TabIndex = 122
        Me.PictureBox1.TabStop = False
        '
        'grdSales
        '
        Me.grdSales.AllowUserToAddRows = False
        Me.grdSales.AllowUserToDeleteRows = False
        Me.grdSales.AllowUserToOrderColumns = True
        Me.grdSales.AllowUserToResizeColumns = False
        Me.grdSales.AllowUserToResizeRows = False
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdSales.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdSales.ColumnHeadersHeight = 45
        Me.grdSales.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colInventoryCode, Me.colClass, Me.colDescription, Me.colWeight, Me.colGoldLoss, Me.colCostPerGram, Me.ColDSWt, Me.colSMC, Me.colPcs, Me.colTotalValue, Me.colTotalCost, Me.ColSalesType, Me.colPMC, Me.colGST, Me.DataGridViewTextBoxColumn1})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdSales.DefaultCellStyle = DataGridViewCellStyle5
        Me.grdSales.Location = New System.Drawing.Point(4, 318)
        Me.grdSales.Name = "grdSales"
        Me.grdSales.ReadOnly = True
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdSales.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.grdSales.RowHeadersVisible = False
        Me.grdSales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdSales.Size = New System.Drawing.Size(658, 143)
        Me.grdSales.TabIndex = 556
        Me.grdSales.Visible = False
        '
        'colInventoryCode
        '
        Me.colInventoryCode.HeaderText = "Item Code"
        Me.colInventoryCode.Name = "colInventoryCode"
        Me.colInventoryCode.ReadOnly = True
        '
        'colClass
        '
        Me.colClass.HeaderText = "Class"
        Me.colClass.Name = "colClass"
        Me.colClass.ReadOnly = True
        Me.colClass.Width = 60
        '
        'colDescription
        '
        Me.colDescription.HeaderText = "Description"
        Me.colDescription.Name = "colDescription"
        Me.colDescription.ReadOnly = True
        Me.colDescription.Width = 180
        '
        'colWeight
        '
        Me.colWeight.HeaderText = "Weight"
        Me.colWeight.Name = "colWeight"
        Me.colWeight.ReadOnly = True
        Me.colWeight.Width = 60
        '
        'colGoldLoss
        '
        Me.colGoldLoss.HeaderText = "Gold Loss"
        Me.colGoldLoss.Name = "colGoldLoss"
        Me.colGoldLoss.ReadOnly = True
        Me.colGoldLoss.Width = 60
        '
        'colCostPerGram
        '
        Me.colCostPerGram.HeaderText = "Cost Per Gram"
        Me.colCostPerGram.Name = "colCostPerGram"
        Me.colCostPerGram.ReadOnly = True
        Me.colCostPerGram.Width = 60
        '
        'ColDSWt
        '
        Me.ColDSWt.HeaderText = "D/S Wt"
        Me.ColDSWt.Name = "ColDSWt"
        Me.ColDSWt.ReadOnly = True
        Me.ColDSWt.Visible = False
        Me.ColDSWt.Width = 80
        '
        'colSMC
        '
        DataGridViewCellStyle3.Format = "N2"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.colSMC.DefaultCellStyle = DataGridViewCellStyle3
        Me.colSMC.HeaderText = "SMC"
        Me.colSMC.Name = "colSMC"
        Me.colSMC.ReadOnly = True
        Me.colSMC.Width = 60
        '
        'colPcs
        '
        Me.colPcs.HeaderText = "PCS"
        Me.colPcs.Name = "colPcs"
        Me.colPcs.ReadOnly = True
        Me.colPcs.Width = 60
        '
        'colTotalValue
        '
        DataGridViewCellStyle4.Format = "N2"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.colTotalValue.DefaultCellStyle = DataGridViewCellStyle4
        Me.colTotalValue.HeaderText = "Total Value"
        Me.colTotalValue.Name = "colTotalValue"
        Me.colTotalValue.ReadOnly = True
        '
        'colTotalCost
        '
        Me.colTotalCost.HeaderText = "Total Cost"
        Me.colTotalCost.Name = "colTotalCost"
        Me.colTotalCost.ReadOnly = True
        Me.colTotalCost.Visible = False
        Me.colTotalCost.Width = 120
        '
        'ColSalesType
        '
        Me.ColSalesType.HeaderText = "SalesType"
        Me.ColSalesType.Name = "ColSalesType"
        Me.ColSalesType.ReadOnly = True
        Me.ColSalesType.Visible = False
        '
        'colPMC
        '
        Me.colPMC.HeaderText = "PMC"
        Me.colPMC.Name = "colPMC"
        Me.colPMC.ReadOnly = True
        Me.colPMC.Visible = False
        '
        'colGST
        '
        Me.colGST.HeaderText = "GST"
        Me.colGST.Name = "colGST"
        Me.colGST.ReadOnly = True
        Me.colGST.Visible = False
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "DetailDiscount"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Visible = False
        '
        'pnlCostAdminPwd
        '
        Me.pnlCostAdminPwd.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlCostAdminPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCostAdminPwd.Controls.Add(Me.btnCostAdminPwdCancel)
        Me.pnlCostAdminPwd.Controls.Add(Me.btnCostAdminPwdOK)
        Me.pnlCostAdminPwd.Controls.Add(Me.txtCostAdminPassword)
        Me.pnlCostAdminPwd.Controls.Add(Me.txtPassFind)
        Me.pnlCostAdminPwd.Controls.Add(Me.Label33)
        Me.pnlCostAdminPwd.Location = New System.Drawing.Point(210, 318)
        Me.pnlCostAdminPwd.Name = "pnlCostAdminPwd"
        Me.pnlCostAdminPwd.Size = New System.Drawing.Size(246, 88)
        Me.pnlCostAdminPwd.TabIndex = 557
        Me.pnlCostAdminPwd.Visible = False
        '
        'btnCostAdminPwdCancel
        '
        Me.btnCostAdminPwdCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCostAdminPwdCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCostAdminPwdCancel.Image = CType(resources.GetObject("btnCostAdminPwdCancel.Image"), System.Drawing.Image)
        Me.btnCostAdminPwdCancel.Location = New System.Drawing.Point(160, 56)
        Me.btnCostAdminPwdCancel.Name = "btnCostAdminPwdCancel"
        Me.btnCostAdminPwdCancel.Size = New System.Drawing.Size(64, 23)
        Me.btnCostAdminPwdCancel.TabIndex = 75
        Me.btnCostAdminPwdCancel.Text = "Cancel"
        Me.btnCostAdminPwdCancel.UseVisualStyleBackColor = False
        '
        'btnCostAdminPwdOK
        '
        Me.btnCostAdminPwdOK.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCostAdminPwdOK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCostAdminPwdOK.Image = CType(resources.GetObject("btnCostAdminPwdOK.Image"), System.Drawing.Image)
        Me.btnCostAdminPwdOK.Location = New System.Drawing.Point(88, 56)
        Me.btnCostAdminPwdOK.Name = "btnCostAdminPwdOK"
        Me.btnCostAdminPwdOK.Size = New System.Drawing.Size(64, 23)
        Me.btnCostAdminPwdOK.TabIndex = 74
        Me.btnCostAdminPwdOK.Text = "OK"
        Me.btnCostAdminPwdOK.UseVisualStyleBackColor = False
        '
        'txtCostAdminPassword
        '
        Me.txtCostAdminPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCostAdminPassword.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostAdminPassword.ForeColor = System.Drawing.Color.Black
        Me.txtCostAdminPassword.Location = New System.Drawing.Point(88, 32)
        Me.txtCostAdminPassword.MaxLength = 20
        Me.txtCostAdminPassword.Name = "txtCostAdminPassword"
        Me.txtCostAdminPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtCostAdminPassword.Size = New System.Drawing.Size(136, 20)
        Me.txtCostAdminPassword.TabIndex = 73
        Me.txtCostAdminPassword.Tag = ""
        '
        'txtPassFind
        '
        Me.txtPassFind.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassFind.ForeColor = System.Drawing.Color.Black
        Me.txtPassFind.Location = New System.Drawing.Point(16, 33)
        Me.txtPassFind.Name = "txtPassFind"
        Me.txtPassFind.Size = New System.Drawing.Size(64, 15)
        Me.txtPassFind.TabIndex = 72
        Me.txtPassFind.Text = "Password"
        '
        'Label33
        '
        Me.Label33.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.Black
        Me.Label33.Location = New System.Drawing.Point(8, 8)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(56, 15)
        Me.Label33.TabIndex = 72
        Me.Label33.Text = "Admin"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnRePrint
        '
        Me.btnRePrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnRePrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRePrint.Location = New System.Drawing.Point(136, 496)
        Me.btnRePrint.Name = "btnRePrint"
        Me.btnRePrint.Size = New System.Drawing.Size(100, 40)
        Me.btnRePrint.TabIndex = 558
        Me.btnRePrint.Text = "RePrint (F9)"
        Me.btnRePrint.UseVisualStyleBackColor = False
        '
        'PnlReprint
        '
        Me.PnlReprint.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.PnlReprint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PnlReprint.Controls.Add(Me.btnReprintCancel)
        Me.PnlReprint.Controls.Add(Me.btnReprintOK)
        Me.PnlReprint.Controls.Add(Me.txtRePrintBillNo)
        Me.PnlReprint.Controls.Add(Me.Label4)
        Me.PnlReprint.Controls.Add(Me.Label6)
        Me.PnlReprint.Location = New System.Drawing.Point(209, 310)
        Me.PnlReprint.Name = "PnlReprint"
        Me.PnlReprint.Size = New System.Drawing.Size(248, 88)
        Me.PnlReprint.TabIndex = 559
        Me.PnlReprint.Visible = False
        '
        'btnReprintCancel
        '
        Me.btnReprintCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnReprintCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReprintCancel.Image = CType(resources.GetObject("btnReprintCancel.Image"), System.Drawing.Image)
        Me.btnReprintCancel.Location = New System.Drawing.Point(168, 56)
        Me.btnReprintCancel.Name = "btnReprintCancel"
        Me.btnReprintCancel.Size = New System.Drawing.Size(64, 23)
        Me.btnReprintCancel.TabIndex = 75
        Me.btnReprintCancel.Text = "Cancel"
        Me.btnReprintCancel.UseVisualStyleBackColor = False
        '
        'btnReprintOK
        '
        Me.btnReprintOK.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnReprintOK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReprintOK.Image = CType(resources.GetObject("btnReprintOK.Image"), System.Drawing.Image)
        Me.btnReprintOK.Location = New System.Drawing.Point(96, 56)
        Me.btnReprintOK.Name = "btnReprintOK"
        Me.btnReprintOK.Size = New System.Drawing.Size(64, 23)
        Me.btnReprintOK.TabIndex = 76
        Me.btnReprintOK.Text = "OK"
        Me.btnReprintOK.UseVisualStyleBackColor = False
        '
        'txtRePrintBillNo
        '
        Me.txtRePrintBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRePrintBillNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRePrintBillNo.ForeColor = System.Drawing.Color.Black
        Me.txtRePrintBillNo.Location = New System.Drawing.Point(96, 33)
        Me.txtRePrintBillNo.MaxLength = 20
        Me.txtRePrintBillNo.Name = "txtRePrintBillNo"
        Me.txtRePrintBillNo.Size = New System.Drawing.Size(136, 20)
        Me.txtRePrintBillNo.TabIndex = 73
        Me.txtRePrintBillNo.Tag = ""
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(32, 33)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 15)
        Me.Label4.TabIndex = 74
        Me.Label4.Text = "Bill No"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(8, 8)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(82, 15)
        Me.Label6.TabIndex = 72
        Me.Label6.Text = "RePrint"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmOrderPayment
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(666, 541)
        Me.Controls.Add(Me.PnlReprint)
        Me.Controls.Add(Me.btnRePrint)
        Me.Controls.Add(Me.pnlCostAdminPwd)
        Me.Controls.Add(Me.grdSales)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.grdOrderPayment)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lblDepositAmount)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.pnlCancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmOrderPayment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Order Payment"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlCancel.ResumeLayout(False)
        Me.pnlCancel.PerformLayout()
        CType(Me.grdOrderPayment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSales, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCostAdminPwd.ResumeLayout(False)
        Me.pnlCostAdminPwd.PerformLayout()
        Me.PnlReprint.ResumeLayout(False)
        Me.PnlReprint.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim objCust As New clsCustomer
    Dim objOrder As New clsOrder
    Dim objSales As New ClsSales
    Dim dAmountPaid As Double
    Dim sInvoiceno As String
    Const cOrderNo = 1
    Const cOrderPaymentNo = 2
    Const cRemarks = 3
    Const cAmountPaid = 4
    Dim tradeInRefNo As String
    Dim msOrderpaymentNo$
    Dim dTradeInGST As Double
    Dim CompName$, Address1$, Address2$, Country$, PhoneNo$, FaxNo$
    Dim bReprint As Boolean
    Dim s24K As String
    Dim s22K As String
    Dim s20K As String
    Dim s22S As String
    Dim s999 As String
    Dim s916 As String
    Dim s835 As String
    Dim s24K999 As String
    Dim s20K850 As String
    Dim s22KSOV As String
    Dim sInvGstType As String
    Dim sInventoryCode As String
    Dim dTotInventoryAmt As Double

    Dim dGstPerc As Double
    Const ICode = 0
    Const IClass = 1
    Const IDescription = 2
    Const IWeight = 3
    Const IGoldLoss = 4
    Const ICostPerGram = 5
    Const IDSWeight = 6
    Const ISMC = 7
    Const IPCS = 8
    Const ITotalValue = 9
    Const ITotalCost = 10
    Const ISalesType = 11
    Const IPMC = 12
    Const IGST = 13
    Const IDiscount = 14
    Dim dRedeemedPoints As Integer
    Dim sBarLocationCode, sBarInvoiceNo, sBarInvoiceDate, sBarClassCode As String
    Dim dBarWeight, dBarStoneWeight, dBarTotalCost As Double
    Dim sType As String
    Dim dTotalCost As String
    Dim objClassRate As New ClsClass

    Private Sub btnCustomerCodeSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomerCodeSearch.Click
        Try
            Dim frmHelpCustomer As New frmHelpSearch(0, 0, "tblCustomer", "L", 4, "CustomerName", "", "CustomerCode", "CustomerCode", 80, "CustomerName", "CustomerName", 150, "Address1", "Address1", 90, "Address2", "Address2", 80)
            frmHelpCustomer.ShowDialog()
            txtCustomerCode.Text = frmHelpCustomer.Var1.Trim
            txtCustomerName.Text = frmHelpCustomer.Var2.Trim
            subGetCustomer()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subGetCustomer()
        Dim dsCust As New DataSet
        Try
            dsCust = objCust.GetCustomerwithCode(Trim(txtCustomerCode.Text))
            If dsCust.Tables(0).Rows.Count > 0 Then
                txtCustomerName.Text = Trim(dsCust.Tables(0).Rows(0)("CustomerName") & "")
                txtAddress1.Text = Trim(dsCust.Tables(0).Rows(0)("Address1") & "")
                txtAddress2.Text = Trim(dsCust.Tables(0).Rows(0)("Address2") & "")
                txtAddress3.Text = Trim(dsCust.Tables(0).Rows(0)("Address3") & "")
                lblCustomerType.Text = Trim(dsCust.Tables(0).Rows(0)("CustomerType") & "")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subGetOrderDetails()
        Dim dsOH As New DataSet
        Dim dsOD As New DataSet
        Dim dsOP As New DataSet
        Dim dsINV As New DataSet
        Dim iCnt As Integer
        Dim dPaymentAmount As Decimal
        Dim objInventory As New clsInventory
        Try
            grdOrderPayment.RowCount = 0
            dPaymentAmount = 0
            dTotalCost = 0
            dsOH = objOrder.GetOrderHeader(txtOrderNo.Text.Trim)
            If dsOH.Tables(0).Rows.Count > 0 Then
                txtCustomerCode.Text = Trim(dsOH.Tables(0).Rows(0)("CustomerCode") & "")
                If Not txtCustomerCode.Text.Trim = "" Then subGetCustomer()
                lblAdvancePaid.Text = Format(Val(dsOH.Tables(0).Rows(0)("AdvancePaid")), "#0.00")
                lblEstimatedCost.Text = Format(Val(dsOH.Tables(0).Rows(0)("TotalCostEstimated")), "#0.00")
                dsOD = objOrder.GetOrderDetail(txtOrderNo.Text.Trim)
                If dsOD.Tables(0).Rows.Count > 0 Then
                    For iCnt = 0 To dsOD.Tables(0).Rows.Count - 1
                        sInventoryCode = Trim(dsOD.Tables(0).Rows(0)("InventoryCode") & "")
                        dsINV = objInventory.GetInventoryDetail(sInventoryCode)
                        If dsINV.Tables(0).Rows.Count > 0 Then
                            dTotalCost = dTotalCost + Val(dsINV.Tables(0).Rows(0)("Totalcost"))
                        Else
                            sInventoryCode = ""
                        End If
                    Next
                End If
                dsOP = objOrder.GetOrderPaymentDetail(txtOrderNo.Text.Trim)
                If dsOP.Tables(0).Rows.Count > 0 Then
                    For iCnt = 0 To dsOP.Tables(0).Rows.Count - 1
                        grdOrderPayment.Rows.Add()
                        fnSetdgvText(grdOrderPayment, grdOrderPayment.RowCount - 1, 0, Trim(dsOP.Tables(0).Rows(iCnt)("OrderNo") & ""))
                        fnSetdgvText(grdOrderPayment, grdOrderPayment.RowCount - 1, 1, Trim(dsOP.Tables(0).Rows(iCnt)("OrderPaymentNo") & ""))
                        fnSetdgvText(grdOrderPayment, grdOrderPayment.RowCount - 1, 2, Trim(dsOP.Tables(0).Rows(iCnt)("Remarks") & ""))
                        fnSetdgvText(grdOrderPayment, grdOrderPayment.RowCount - 1, 3, Val(dsOP.Tables(0).Rows(iCnt)("AmountPaid")))
                        'dPaymentAmount = dPaymentAmount + Val(dsOP.Tables(0).Rows(iCnt)("AmountPaid"))
                    Next
                    subTotalFromGrid()
                    subTotal()
                Else
                    dPaymentAmount = 0
                End If
                lblBalanceAmount.Text = Format(Val(lblEstimatedCost.Text) - (Val(lblAdvancePaid.Text) + dPaymentAmount), "#0.00")
                txtPayAmount.Focus()
            Else
                MessageBox.Show("Could not find the OrderNo.")
                subClearMe()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsOH = Nothing
            dsOD = Nothing
        End Try
    End Sub
    Public Function subTotalFromGrid()
        Dim icnt
        Dim sinventorycode As String
        dAmountPaid = 0
        For icnt = 0 To grdOrderPayment.RowCount - 1
            sinventorycode = Trim(fnGetdgvText(grdOrderPayment, icnt, 0))
            If sinventorycode <> "TOTAL:" Then
                dAmountPaid = Format(Val(dAmountPaid) + Val(fnGetdgvText(grdOrderPayment, icnt, 3)), "0.00")
            End If
        Next
    End Function

    Private Sub subTotal()
        Try
            If grdOrderPayment.RowCount = 0 Then
                Exit Sub
            End If
            'If Not bEdited Then
            'grdPurchase.MaxRows = grdPurchase.MaxRows + 1
            grdOrderPayment.Rows.Add()
            'End If



            Call fnSetdgvText(grdOrderPayment, grdOrderPayment.RowCount - 1, 0, "TOTAL:")
            Call fnSetdgvText(grdOrderPayment, grdOrderPayment.RowCount - 1, 3, Format(dAmountPaid, "0.00"))



            Me.grdOrderPayment.Rows(grdOrderPayment.RowCount - 1).Cells(0).Style.ForeColor = Color.Red
            Me.grdOrderPayment.Rows(grdOrderPayment.RowCount - 1).Cells(0).Style.Font = New Font(grdOrderPayment.Font.FontFamily, 9, FontStyle.Bold)

            Me.grdOrderPayment.Rows(grdOrderPayment.RowCount - 1).Cells(3).Style.ForeColor = Color.Red
            Me.grdOrderPayment.Rows(grdOrderPayment.RowCount - 1).Cells(3).Style.Font = New Font(grdOrderPayment.Font.FontFamily, 9, FontStyle.Bold)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        Finally

        End Try
    End Sub


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub subClearMe()
        Try
            txtOrderNo.Clear()
            txtCustomerCode.Clear()
            txtCustomerName.Clear()
            txtAddress1.Clear()
            txtAddress2.Clear()
            txtAddress3.Clear()
            txtRemarks.Clear()
            lblCustomerType.Text = ""
            lblBalanceAmount.Text = "0.00"
            lblAdvancePaid.Text = "0.00"
            lblEstimatedCost.Text = "0.00"
            lblDepositAmount.Text = "0.00"
            txtPayAmount.Clear()
            grdOrderPayment.RowCount = 0
            dTradeInSales = 0
            dTradeInGST = 0
            btnTradeIn.Enabled = True
            optCash.Enabled = True
            txtPayAmount.Enabled = True
            sType = ""
            opteTRSNo.Checked = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmOrderPayment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'subGridSetting(grdOrderPayment)
    End Sub

    Private Sub frmOrderPayment_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Escape
                    If btnClose.Enabled Then btnClose_Click(sender, e)
                Case Keys.Enter
                    SendKeys.Send("{TAB}")
                Case Keys.F6
                    If btnClear.Enabled Then btnClear_Click(sender, e)
                Case Keys.F7
                    If btnSave.Enabled Then btnSave_Click(sender, e)
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        subClearMe()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Cursor.Current = Cursors.WaitCursor
        Dim frmPM As New frmPayingmode
        Try
            If fncValidateMe() Then
                objOrder.OpenOrderConnection()
                objOrder.gpBeginOrderTransaction()
                objSales.subOpenSalesConnection()
                objSales.subBeginSalesTransaction()
                msOrderpaymentNo = Trim("OP" & objOrder.fncGetNextOrderPaymentNo())
                dPaidAmount = 0
                ReturnAmount = 0
                ' nTotalNet = Val(txtPayAmount.Text)
                nTotalNet = Val(txtPayAmount.Text) - Val(dTradeInSales)
                frmPM.SalesType = ""
                frmPM.sCustomerCode = Trim(txtCustomerCode.Text)
                frmPM.sCustomerType = Trim(lblCustomerType.Text)
                frmPM.ShowDialog()
                If dPaidAmount > 0 Then
                    If Not fncPaymodeDetailSave(msOrderpaymentNo) Then
                        MessageBox.Show("Transaction not saved")
                        objOrder.gpRollBackOrderConnection()
                    ElseIf Not fncSaveOrderPaymentDetail() Then
                        MessageBox.Show("Transaction not saved")
                        objOrder.gpRollBackOrderConnection()
                    ElseIf Not subCheckAmount() Then
                        MessageBox.Show("Transaction not saved")
                        objOrder.gpRollBackOrderConnection()
                    ElseIf Not fncSaveTradeInSales(msOrderpaymentNo) Then
                        MessageBox.Show("Error in save Detail")
                        objSales.subRollBackSalesTransaction()
                    Else
                        objOrder.gpCommitOrderTransaction()
                        objSales.subCommitSalesTransaction()
                        MessageBox.Show("Order payment saved successfully.")
                        'Call subPrintBill(Trim(msInstallmentNo))
                        If Trim(sInvoiceno) <> "" Then
                            Call subPrintBill(Trim(sInvoiceno))
                        End If
                        Call subPrintOrderPayment(Trim(msOrderpaymentNo))

                        If Trim(sType) = "TRADEIN" And grdSales.RowCount > 0 Then
                            Call subGetBarInventory(msOrderpaymentNo)
                        End If

                        subClearMe()
                    End If

                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            frmPM = Nothing
            objOrder.CloseOrderConnection()
            objSales.subCloseSalesConnection()
            Cursor.Current = Cursors.Default
        End Try
    End Sub
    Private Sub subGetBarInventory(ByVal sInvoiceNo As String)
        Dim DsInv As DataSet
        Dim DsTrade As DataSet
        Dim DsAdditional As DataSet
        Dim sender As System.Object
        Dim e As System.EventArgs
        Dim objInv As New clsInventory
        Dim objTrade As New clsTradeIN
        Dim Ds As DataSet
        Dim objTray As New clsTray
        Dim icnt As Long

        Try
            '   DsInv = objInv.GetInventoryDetail(txtInventoryCode.Text.Trim)
            DsTrade = objTrade.getTradeInDetailForBarCode(sInvoiceNo)
            ' DsAdditional = objInv.GetAdditionalInventoryDetail(txtInventoryCode.Text.Trim)

            If DsTrade.Tables("TradeIn").Rows.Count > 0 Then
                For icnt = 0 To DsTrade.Tables("TradeIn").Rows.Count - 1
                    sBarLocationCode = Trim(gthisLocation)
                    sBarInvoiceNo = Trim(DsTrade.Tables("TradeIn").Rows(icnt)("InvoiceNo") & "")
                    sBarInvoiceDate = Trim(DsTrade.Tables("TradeIn").Rows(icnt)("InvoiceDate") & "")
                    sBarClassCode = Trim(DsTrade.Tables("TradeIn").Rows(icnt)("Class") & "")
                    dBarWeight = DsTrade.Tables("TradeIn").Rows(icnt)("Weight") & ""
                    dBarStoneWeight = DsTrade.Tables("TradeIn").Rows(icnt)("StoneWeight") & ""
                    dBarTotalCost = DsTrade.Tables("TradeIn").Rows(icnt)("TotalCost") & ""
                    subBarcodeReportPrintAJ()
                Next
            Else
                MessageBox.Show("InventoryCode Not Found", "GoldPOS")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subBarcodeReportPrintAJ()

        Dim sSql$
        Dim sTemp As String
        Dim sChar As String
        Dim strCompanyname, strAddress, strReportTitle, strpath, strselectformula, strDateFrom, strDateTo As String
        Dim strSubHead As String
        Dim CR As New ReportDocument
        Dim frmRep As New frmReports
        Dim objCompany As New clsCompany
        Dim dsMCC As New DataSet
        Dim objinv As New clsInventory
        Dim ds As DataSet
        Try
            strpath = Application.StartupPath & "\Reports\rptTradeInBarcode_AJ.rpt"
            Me.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            ds = objCompany.getCompanyName
            If ds.Tables("GetCompanyName").Rows.Count > 0 Then
                strCompanyname = Trim(ds.Tables("GetCompanyName").Rows(0)("Name") & "")
                strAddress = Trim(ds.Tables("GetCompanyName").Rows(0)("Address1") & "") & " " & _
                             Trim(ds.Tables("GetCompanyName").Rows(0)("Address2") & "") & " " & _
                             Trim(ds.Tables("GetCompanyName").Rows(0)("City") & "")
            End If
            frmRep.GetModule = "Barcode"
            frmRep.GetReportType = "Summary"
            frmRep.GetPrintOption = "Preview"

            CR.Load(strpath)
            ReportLogOnToServer(CR)
            CR.Refresh()
            frmRep.ReportViewer.ReportSource = CR
            strselectformula = "{tblTradeInDetail.InvoiceNo}=" & fncFormatSQLText(Trim(sBarInvoiceNo)) & " and "
            strselectformula = strselectformula & "{tblTradeInDetail.Class}=" & fncFormatSQLText(Trim(sBarClassCode))

            CR.DataDefinition.FormulaFields("LocationCode").Text = fncFormatSQLText(sBarLocationCode)
            CR.DataDefinition.FormulaFields("InvoiceNo").Text = fncFormatSQLText(sBarInvoiceNo)
            CR.DataDefinition.FormulaFields("InvoiceDate").Text = fncFormatSQLText(sBarInvoiceDate)
            CR.DataDefinition.FormulaFields("ClassCode").Text = fncFormatSQLText(sBarClassCode)
            CR.DataDefinition.FormulaFields("Weight").Text = fncFormatSQLText(Format((Val(dBarWeight) - Val(dBarStoneWeight)), "###0.00") + "gm")
            CR.DataDefinition.FormulaFields("StoneWeight").Text = fncFormatSQLText(Format(Val(dBarWeight), "###0.00") + "gm") 'fncFormatSQLText(Format((Val(dBarWeight) + Val(dBarStoneWeight)), "###0.00") + "gm")
            CR.DataDefinition.FormulaFields("TotalCost").Text = Format(Val(dBarTotalCost), "###0.00")
            CR.DataDefinition.FormulaFields("Salesman").Text = ""
            frmRep.ReportViewer.SelectionFormula = strselectformula
            CR.DataDefinition.RecordSelectionFormula = strselectformula
            ' ''frmRep.ShowDialog()
            ' ''frmRep.ReportViewer.Visible = True
            ' ''frmRep.ReportViewer.DisplayToolbar = True
            ' ''frmRep.ReportViewer.CloseView(False)
            ' ''frmRep.WindowState = FormWindowState.Maximized
            CR.PrintToPrinter(1, True, 0, 0)
            frmRep = Nothing
            frmRep = New frmReports
            Me.Cursor.Current = System.Windows.Forms.Cursors.Default


            CR.Close()
            CR.Dispose()
            CR = Nothing


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Function fncSaveTradeInSales(ByVal sInvoiceNo As String) As Boolean
        Dim objTI As New clsTradeIN
        Dim sTIinventorycode As String
        Dim sTIInvoiceNo As String
        Dim iCnt As Long
        Dim sSql As String
        Dim nWeight As Decimal
        Dim nStoneWeight As Decimal
        Dim nSMC As Decimal
        Dim nPCS As Decimal
        Dim nGoldLoss As Decimal
        Dim nTotCost As Decimal
        Dim nTotalAmt As Decimal
        Dim bStatus As Boolean
        Dim bHaveTradeIn As Boolean
        Dim i As Long
        Try
            bStatus = False
            bHaveTradeIn = False
            For iCnt = 0 To grdSales.RowCount - 1
                If fnGetdgvText(grdSales, iCnt, ISalesType).Trim = "TI" Then
                    nWeight = nWeight + Val(fnGetdgvText(grdSales, iCnt, IWeight))
                    nStoneWeight = nStoneWeight + Val(fnGetdgvText(grdSales, iCnt, IDSWeight))
                    nSMC = nSMC + Val(fnGetdgvText(grdSales, iCnt, ISMC))
                    nPCS = nPCS + Val(fnGetdgvText(grdSales, iCnt, IPCS))
                    nGoldLoss = nGoldLoss + Val(fnGetdgvText(grdSales, iCnt, IGoldLoss))
                    nTotCost = nTotCost + Val(fnGetdgvText(grdSales, iCnt, ITotalCost))
                    nTotalAmt = nTotalAmt + Val(fnGetdgvText(grdSales, iCnt, ITotalValue))
                    bHaveTradeIn = True
                End If
            Next

            If bHaveTradeIn Then
                'sTIInvoiceNo = objTI.fncGetNextTrandeInRefNo()
                'sTIinventorycode = "TI-HQ" & sTIInvoiceNo
                'bStatus = objTI.fncUpdateNextTradeInRefNo()
                'If Not bStatus Then
                '    Exit Function
                'End If
                ' sTIinventorycode = "TI-" & sInvoiceNo
                'bStatus = objTI.TradeInDeleteSales(Trim(sTIinventorycode))
                'If Not bStatus Then
                '    Exit Function
                'End If

                ' sSalesType = "TRADEIN"

                bStatus = objTI.TradeInHeaderSaveSales(Trim(sTIinventorycode), Trim$(sInvoiceNo), _
                          gthisLocation, UCase(Trim(txtCustomerCode.Text)), "", Trim(txtCustomerName.Text), Trim(txtAddress1.Text), _
                          Trim(txtAddress2.Text), Trim(txtAddress3.Text), _
                          Val(0), Val(nWeight), _
                          Val(nStoneWeight), Val(nSMC), Val(nPCS), _
                          Val(nGoldLoss), Val(nTotCost), _
                          -Val(nTotalAmt), "TRADEIN", "N", sUserID)
                If Not bStatus Then
                    Exit Function
                End If
                i = 0
                For iCnt = 0 To grdSales.RowCount - 1
                    If fnGetdgvText(grdSales, iCnt, ISalesType).Trim.ToUpper = "TI" Then
                        sTIinventorycode = "TI-" & sInvoiceNo & "-" & i + 1
                        Call fnSetdgvText(grdSales, iCnt, 0, sTIinventorycode)
                        bStatus = objTI.TradeInDetailSaveSales(gthisLocation, Trim(sTIinventorycode), iCnt, Trim(fnGetdgvText(grdSales, iCnt, IDescription)), _
                                "", Val(fnGetdgvText(grdSales, iCnt, IWeight)), Val(fnGetdgvText(grdSales, iCnt, IDSWeight)), _
                                Val(fnGetdgvText(grdSales, iCnt, ICostPerGram)), Val(fnGetdgvText(grdSales, iCnt, ISMC)), Val(fnGetdgvText(grdSales, iCnt, IPCS)), _
                                Val(fnGetdgvText(grdSales, iCnt, IGoldLoss)), Val(fnGetdgvText(grdSales, iCnt, ITotalCost)), "TRADEIN", "TRADEIN", "TRADEIN", "TRADEIN", Trim(fnGetdgvText(grdSales, iCnt, IClass)), Trim$(sInvoiceNo), sUserID)
                        If Not bStatus Then
                            Exit Function
                        End If
                        i = i + 1
                    End If
                Next
            Else
                bStatus = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncSaveTradeInSales = bStatus
        End Try
    End Function
    Private Sub subPrintOrderPayment(ByVal sInvoiceNo As String)
        Try
            Dim ReportTitle As String
            Dim sSelection As String
            Dim sSalesType$
            Dim frmrep As New frmReports
            Dim CR As New ReportDocument
            Dim j As Integer
            Cursor = Cursors.WaitCursor
            If Trim(sCompanyShortCode) = "AJ" Then
                strpath = Application.StartupPath & "\Reports\rptOrderPayment_AJ.rpt"
            ElseIf Trim(sCompanyShortCode) = "MD" Then
                strpath = Application.StartupPath & "\Reports\rptOrderPayment_MD.rpt"
            ElseIf Trim(sCompanyShortCode) = "MJ" Then
                strpath = Application.StartupPath & "\Reports\rptOrderPayment_MJ.rpt"
            End If
            CR.Load(strpath)
            If Not ReportLogOnToServer(CR) Then
                Exit Sub
            End If
            ReportLogOnToServer(CR)
            CR.Refresh()
            subGetClassRate()
            frmrep.ReportViewer.ReportSource = CR
            sSelection = "{tblOrderPaymentdetail.OrderPaymentNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
            frmrep.ReportViewer.SelectionFormula = sSelection
            CR.DataDefinition.RecordSelectionFormula = sSelection
            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
            CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
            CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
            CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
            CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
            CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
            CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
            ''frmrep.Show()
            '   If bPrinter Then CR.PrintToPrinter(1, True, 0, 0)
            ''frmrep.ReportViewer.Visible = True
            ''frmrep.ReportViewer.DisplayToolbar = True
            ''frmrep.ReportViewer.CloseView(False)
            ''frmrep.WindowState = FormWindowState.Maximized
            ''frmrep = Nothing
            ''frmrep = New frmReports
            If chkFinalPayment.Checked Then
                CR.PrintToPrinter(1, True, 0, 0)
            Else
                CR.PrintToPrinter(2, True, 0, 0)
            End If
            frmrep = Nothing
            frmrep = New frmReports

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub subPrintBill(ByVal sInvoiceNo As String)
        Dim ReportTitle As String
        Dim sSelection As String
        Dim sSalesType$
        Dim sReply As String
        Dim sOrderNo As String
        Dim dsSales As New DataSet
        Dim frmrep As New frmReports
        Dim CR As New ReportDocument
        Dim j As Integer
        Dim sInsAgreementNo As String
        Dim Dm As New DataManager
        Dim sPrintCustomer As String
        Dim sSql As String
        Try
            Cursor = Cursors.WaitCursor
            dsSales = objSales.GetSalesHeaderByInvoiceNo(Trim(sInvoiceNo))

            If dsSales.Tables(0).Rows.Count > 0 Then
                sSalesType = Trim$(dsSales.Tables(0).Rows(0)("SalesType") & "")
                sInsAgreementNo = Trim$(dsSales.Tables(0).Rows(0)("InsAgreementNo") & "")
                sPrintCustomer = Trim$(dsSales.Tables(0).Rows(0)("customerCode") & "")
            End If


            sSql = "If exists (Select * from dbo.sysobjects where id = object_id(N'[dbo].[vwMemberPrintAJ]') and OBJECTPROPERTY(id, N'IsView') = 1)"
            sSql = sSql & " Drop View vwMemberPrintAJ" & vbCrLf
            Dm.ProjExecNonQryAcc(sSql)


            sSql = " Create View vwMemberPrintAJ" & vbCrLf
            sSql = sSql & " AS" & vbCrLf
            sSql = sSql & " select top 1 * from vwMemberTransationaj "
            sSql = sSql & " Where CustomerCode = " & fncFormatSQLText(sPrintCustomer)
            sSql = sSql & " order by ExpiryDate"

            Dm.ProjExecNonQryAcc(sSql)

            sSql = "If exists (Select * from dbo.sysobjects where id = object_id(N'[dbo].[vwMemberTranAJ]') and OBJECTPROPERTY(id, N'IsView') = 1)"
            sSql = sSql & " Drop View vwMemberTranAJ" & vbCrLf
            Dm.ProjExecNonQryAcc(sSql)


            sSql = " Create View vwMemberTranAJ" & vbCrLf
            sSql = sSql & " AS" & vbCrLf
            sSql = sSql & " select  * from vwMemberTransationaj "
            sSql = sSql & " Where CustomerCode = " & fncFormatSQLText(sPrintCustomer)
            sSql = sSql & " and InvoiceNo = " & fncFormatSQLText(sInvoiceNo)


            Dm.ProjExecNonQryAcc(sSql)

            If UCase(sCompanyShortCode) = "HN" Then
                Select Case sSalesType
                    Case "TRADEIN"
                        strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_HN.rpt"
                        CR.Load(strpath)
                        ReportLogOnToServer(CR)
                        CR.Refresh()

                        frmrep.ReportViewer.ReportSource = CR
                        sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                        frmrep.ReportViewer.SelectionFormula = sSelection
                        CR.DataDefinition.RecordSelectionFormula = sSelection
                        If UCase(sCompanyShortCode) = "HN" And bReprint Then
                            CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                        End If
                        CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                        CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                        CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                        CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                        CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                        CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                        CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                        CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                        CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                        CR.PrintToPrinter(1, True, 0, 0)
                        frmrep = Nothing
                        frmrep = New frmReports


                        ''''' Durai Changes
                        '''If (UCase(Objuser.CompanyName)) = "HANIFFA PTE LTD" Then
                        strpath = Application.StartupPath & "\Reports\rptTradeInDetailInvoice_HN.rpt"
                        CR.Load(strpath)
                        ReportLogOnToServer(CR)
                        CR.Refresh()

                        frmrep.ReportViewer.ReportSource = CR
                        sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                        frmrep.ReportViewer.SelectionFormula = sSelection
                        CR.DataDefinition.RecordSelectionFormula = sSelection
                        CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                        CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                        CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                        CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                        CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                        CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                        CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                        CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                        CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                        CR.PrintToPrinter(1, True, 0, 0)
                        frmrep = Nothing
                        frmrep = New frmReports
                        '''End If

                    Case "CASH"
                        strpath = Application.StartupPath & "\Reports\rptSalesInvoice_HN.rpt"

                        CR.Load(strpath)
                        ReportLogOnToServer(CR)
                        CR.Refresh()

                        frmrep.ReportViewer.ReportSource = CR
                        sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                        frmrep.ReportViewer.SelectionFormula = sSelection
                        CR.DataDefinition.RecordSelectionFormula = sSelection
                        If UCase(sCompanyShortCode) = "HN" And bReprint Then
                            CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                        End If
                        CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                        CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                        CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                        CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                        CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                        CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                        CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                        CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                        CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                        CR.PrintToPrinter(1, True, 0, 0)
                        frmrep = Nothing
                        frmrep = New frmReports
                End Select
            ElseIf UCase(sCompanyShortCode) = "MJ" Then
                ''strpath = Application.StartupPath & "\Reports\rptSalesInvoice_MJ.rpt"
                If sInvGstType = "" Then
                    strpath = Application.StartupPath & "\Reports\rptOrderSalesInvoice_MJ.rpt"
                ElseIf sInvGstType = "Standard Rated Supply" Then
                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceStandard_MJ.rpt"
                ElseIf sInvGstType = "Exempt Supply" Or sInvGstType = "No GST" Then
                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceNoGST_MJ.rpt"
                End If
                CR.Load(strpath)
                ReportLogOnToServer(CR)
                CR.Refresh()

                frmrep.ReportViewer.ReportSource = CR
                sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                frmrep.ReportViewer.SelectionFormula = sSelection
                CR.DataDefinition.RecordSelectionFormula = sSelection
                If UCase(sCompanyShortCode) = "MJ" And bReprint Then
                    CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                End If
                CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s24K999)
                CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s20K850)
                CR.DataDefinition.FormulaFields("Rate4").Text = fncFormatSQLText(s22KSOV)
                ''CR.PrintOptions.PaperSize = PaperSize.DefaultPaperSize
                CR.PrintToPrinter(1, True, 0, 0)

                ''frmrep.ReportViewer.ReportSource = CR
                ''frmrep.ShowDialog()
                ''frmrep.ReportViewer.Visible = True
                ''frmrep.ReportViewer.CloseView(False)
                ''frmrep.WindowState = FormWindowState.Maximized
                frmrep = Nothing
                frmrep = New frmReports
                ''' End Select
            ElseIf UCase(sCompanyShortCode) = "SSJ" Then
                Select Case sSalesType
                    Case "INSTALLMENT"
                        strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_SSJ.rpt"

                        CR.Load(strpath)
                        ReportLogOnToServer(CR)
                        CR.Refresh()

                        frmrep.ReportViewer.ReportSource = CR
                        sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                        frmrep.ReportViewer.SelectionFormula = sSelection
                        CR.DataDefinition.RecordSelectionFormula = sSelection
                        If UCase(sCompanyShortCode) = "SSJ" And bReprint Then
                            CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                        End If
                        CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                        CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                        CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                        CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                        CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                        CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                        CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                        CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                        CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                        CR.PrintToPrinter(1, True, 0, 0)
                        frmrep = Nothing
                        frmrep = New frmReports
                    Case "TRADEIN"

                        strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_SSJ.rpt"
                        CR.Load(strpath)
                        ReportLogOnToServer(CR)
                        CR.Refresh()

                        frmrep.ReportViewer.ReportSource = CR
                        sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                        frmrep.ReportViewer.SelectionFormula = sSelection
                        CR.DataDefinition.RecordSelectionFormula = sSelection
                        If UCase(sCompanyShortCode) = "SSJ" And bReprint Then
                            CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                        End If
                        CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                        CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                        CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                        CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                        CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                        CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                        CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                        CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                        CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                        CR.PrintToPrinter(1, True, 0, 0)
                        frmrep = Nothing
                        frmrep = New frmReports
                    Case "CASH"
                        strpath = Application.StartupPath & "\Reports\rptSalesInvoice_SSJ.rpt"

                        CR.Load(strpath)
                        ReportLogOnToServer(CR)
                        CR.Refresh()

                        frmrep.ReportViewer.ReportSource = CR
                        sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                        frmrep.ReportViewer.SelectionFormula = sSelection
                        CR.DataDefinition.RecordSelectionFormula = sSelection
                        If UCase(sCompanyShortCode) = "SSJ" And bReprint Then
                            CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                        End If
                        CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                        CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                        CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                        CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                        CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                        CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                        CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                        CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                        CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                        CR.PrintToPrinter(1, True, 0, 0)
                        frmrep = Nothing
                        frmrep = New frmReports
                End Select
            ElseIf UCase(sCompanyShortCode) = "RJ" Then
                Select Case sSalesType
                    Case "INSTALLMENT"
                        j = 1
                        For j = 1 To 2
                            strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_RJ.rpt"

                            CR.Load(strpath)
                            ReportLogOnToServer(CR)
                            CR.Refresh()

                            frmrep.ReportViewer.ReportSource = CR
                            sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                            frmrep.ReportViewer.SelectionFormula = sSelection
                            CR.DataDefinition.RecordSelectionFormula = sSelection
                            If UCase(sCompanyShortCode) = "SSJ" And bReprint Then
                                CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                            End If
                            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                            CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                            CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                            CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                            CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                            CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                            CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                            CR.PrintToPrinter(1, True, 0, 0)
                            frmrep = Nothing
                            frmrep = New frmReports
                        Next
                        'frmrep.Show()
                        'frmrep.ReportViewer.Visible = True
                        'frmrep.ReportViewer.DisplayToolbar = True
                        'frmrep.ReportViewer.CloseView(False)
                        'frmrep.WindowState = FormWindowState.Maximized

                    Case "TRADEIN"
                        j = 1
                        For j = 1 To 2
                            strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_RJ.rpt"
                            CR.Load(strpath)
                            ReportLogOnToServer(CR)
                            CR.Refresh()

                            frmrep.ReportViewer.ReportSource = CR
                            sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                            frmrep.ReportViewer.SelectionFormula = sSelection
                            CR.DataDefinition.RecordSelectionFormula = sSelection
                            If UCase(sCompanyShortCode) = "SSJ" And bReprint Then
                                CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                            End If
                            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                            CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                            CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                            CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                            CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                            CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                            CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                            'frmrep.Show()
                            'frmrep.ReportViewer.Visible = True
                            'frmrep.ReportViewer.DisplayToolbar = True
                            'frmrep.ReportViewer.CloseView(False)
                            'frmrep.WindowState = FormWindowState.Maximized
                            CR.PrintToPrinter(1, True, 0, 0)
                            frmrep = Nothing
                            frmrep = New frmReports
                        Next
                    Case "CASH"
                        j = 1
                        For j = 1 To 2
                            strpath = Application.StartupPath & "\Reports\rptSalesInvoice_RJ.rpt"

                            CR.Load(strpath)
                            ReportLogOnToServer(CR)
                            CR.Refresh()

                            frmrep.ReportViewer.ReportSource = CR
                            sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                            frmrep.ReportViewer.SelectionFormula = sSelection
                            CR.DataDefinition.RecordSelectionFormula = sSelection
                            If UCase(sCompanyShortCode) = "SSJ" And bReprint Then
                                CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                            End If
                            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                            CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                            CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                            CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                            CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                            CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                            CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)


                            '  frmrep.ReportViewer.SelectionFormula = strselectformula
                            'frmrep.Show()
                            'frmrep.ReportViewer.Visible = True
                            'frmrep.ReportViewer.DisplayToolbar = True
                            'frmrep.ReportViewer.CloseView(False)
                            'frmrep.WindowState = FormWindowState.Maximized
                            ' frmrep = Nothing
                            '  CR.PrintOptions.PaperSize = PaperSize.DefaultPaperSize
                            CR.PrintToPrinter(1, True, 0, 0)
                            frmrep = Nothing
                            frmrep = New frmReports
                        Next
                End Select

            Else
                sReply = MsgBox("Do you want to print Net Bill?", vbYesNoCancel + vbQuestion, "Print Option")
                If sReply = vbYes Then
                    Select Case sSalesType
                        Case "INSTALLMENT"
                            If sCompanyShortCode = "ES" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_ESP.rpt"
                            ElseIf sCompanyShortCode = "AS" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_AS.rpt"
                            ElseIf sCompanyShortCode = "ML" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_ML.rpt"
                            ElseIf UCase(sCompanyShortCode) = "GMT" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_GMT.rpt"
                            End If

                            CR.Load(strpath)
                            ReportLogOnToServer(CR)
                            CR.Refresh()

                            frmrep.ReportViewer.ReportSource = CR
                            sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                            frmrep.ReportViewer.SelectionFormula = sSelection
                            CR.DataDefinition.RecordSelectionFormula = sSelection
                            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                            CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                            CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                            CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                            CR.PrintToPrinter(1, True, 0, 0)
                            frmrep = Nothing
                            frmrep = New frmReports
                        Case "ORDER"
                            Dim sCmd As String
                            Dim sExec As String
                            ' Dim Dm As New DataManager

                            sCmd = " Exec sp_OrderPayMode " & fncFormatSQLText(sInsAgreementNo)
                            Dm.ProjExecNonQry(sCmd)

                            sExec = " Exec sp_OrderTradeIn " & fncFormatSQLText(sInsAgreementNo)
                            Dm.ProjExecNonQry(sExec)


                            If UCase(sCompanyShortCode) = "AJ" Then
                                strpath = Application.StartupPath & "\Reports\rptOrderSalesInvoice_AJ.rpt"
                            ElseIf UCase(sCompanyShortCode) = "MD" Then
                                strpath = Application.StartupPath & "\Reports\rptOrderSalesInvoice_MD.rpt"
                            End If

                            CR.Load(strpath)
                            ReportLogOnToServer(CR)
                            CR.Refresh()
                            subGetClassRate()
                            frmrep.ReportViewer.ReportSource = CR
                            sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                            frmrep.ReportViewer.SelectionFormula = sSelection
                            CR.DataDefinition.RecordSelectionFormula = sSelection
                            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                            CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                            CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                            CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                            CR.PrintToPrinter(2, True, 0, 0)
                            frmrep = Nothing
                            frmrep = New frmReports
                        Case "TRADEIN"
                            If sCompanyShortCode = "ES" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_ESP.rpt"
                            ElseIf sCompanyShortCode = "AS" Then
                                '''strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_AS.rpt"
                                If sInvGstType = "" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_AS.rpt"
                                ElseIf sInvGstType = "Standard Rated Supply" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoiceStandard_AS.rpt"
                                ElseIf sInvGstType = "Exempt Supply" Or sInvGstType = "No GST" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoiceNoGST_AS.rpt"
                                End If
                            ElseIf sCompanyShortCode = "ML" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_ML.rpt"
                            ElseIf UCase(sCompanyShortCode) = "GMT" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_GMT.rpt"
                            ElseIf UCase(sCompanyShortCode) = "HN" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_HN.rpt"
                            End If
                            CR.Load(strpath)
                            ReportLogOnToServer(CR)
                            CR.Refresh()

                            frmrep.ReportViewer.ReportSource = CR
                            sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                            frmrep.ReportViewer.SelectionFormula = sSelection
                            CR.DataDefinition.RecordSelectionFormula = sSelection
                            If UCase(sCompanyShortCode) = "HN" And bReprint Then
                                CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                            End If
                            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                            CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                            CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                            CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                            CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                            CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                            CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                            CR.PrintToPrinter(1, True, 0, 0)
                            frmrep = Nothing
                            frmrep = New frmReports
                            If sCompanyShortCode = "ES" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInDetailInvoice_ESP.rpt"
                            End If

                            If ((UCase(sCompanyShortCode) <> "HN") And (UCase(sCompanyShortCode) <> "AS") And (UCase(sCompanyShortCode) <> "ML")) Then
                                CR.Load(strpath)
                                ReportLogOnToServer(CR)
                                CR.Refresh()

                                frmrep.ReportViewer.ReportSource = CR
                                sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                                frmrep.ReportViewer.SelectionFormula = sSelection
                                CR.DataDefinition.RecordSelectionFormula = sSelection
                                CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                                CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                                CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                                CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                                CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                                CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                                CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                                CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                                CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                                CR.PrintToPrinter(1, True, 0, 0)
                                frmrep = Nothing
                                frmrep = New frmReports
                            End If
                        Case Else
                            If UCase(sCompanyShortCode) = "ES" Then
                                strpath = Application.StartupPath & "\Reports\rptSalesInvoice_ESP.rpt"
                            ElseIf sCompanyShortCode = "AS" Then
                                '''strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AS.rpt"
                                If sInvGstType = "" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AS.rpt"
                                ElseIf sInvGstType = "Standard Rated Supply" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceStandard_AS.rpt"
                                ElseIf sInvGstType = "Exempt Supply" Or sInvGstType = "No GST" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceNoGST_AS.rpt"
                                End If
                            ElseIf sCompanyShortCode = "AJ" Then
                                '''strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AS.rpt"
                                If sInvGstType = "" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AJ.rpt"
                                ElseIf sInvGstType = "Standard Rated Supply" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceStandard_AJ.rpt"
                                ElseIf sInvGstType = "Exempt Supply" Or sInvGstType = "No GST" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceNoGST_AJ.rpt"
                                End If
                            ElseIf sCompanyShortCode = "ML" Then
                                strpath = Application.StartupPath & "\Reports\rptSalesInvoice_ML.rpt"
                            ElseIf UCase(sCompanyShortCode) = "GMT" Then
                                strpath = Application.StartupPath & "\Reports\rptSalesInvoice_GMT.rpt"
                            ElseIf UCase(sCompanyShortCode) = "HN" Then
                                strpath = Application.StartupPath & "\Reports\rptSalesInvoice_HN.rpt"
                            End If
                            CR.Load(strpath)
                            ReportLogOnToServer(CR)
                            CR.Refresh()

                            frmrep.ReportViewer.ReportSource = CR
                            sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                            frmrep.ReportViewer.SelectionFormula = sSelection
                            CR.DataDefinition.RecordSelectionFormula = sSelection
                            If UCase(sCompanyShortCode) = "HN" And bReprint Then
                                CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                            End If

                            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                            CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                            CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                            CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                            CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                            CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                            CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                            CR.PrintToPrinter(1, True, 0, 0)
                            frmrep = Nothing
                            frmrep = New frmReports
                    End Select
                End If

                If sReply = vbNo Then
                    Select Case sSalesType
                        Case "INSTALLMENT"
                            If sCompanyShortCode = "ES" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_ESP.rpt"
                            ElseIf sCompanyShortCode = "AS" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_AS.rpt"
                            ElseIf sCompanyShortCode = "ML" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_ML.rpt"
                            ElseIf UCase(sCompanyShortCode) = "GMT" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_GMT.rpt"
                            End If
                            CR.Load(strpath)
                            ReportLogOnToServer(CR)
                            CR.Refresh()

                            frmrep.ReportViewer.ReportSource = CR
                            sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                            frmrep.ReportViewer.SelectionFormula = sSelection
                            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                            CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                            CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                            CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)

                            frmrep.Show()
                            frmrep.ReportViewer.Visible = True
                            frmrep.ReportViewer.DisplayToolbar = True
                            frmrep.ReportViewer.CloseView(False)
                            frmrep.WindowState = FormWindowState.Maximized
                            frmrep = Nothing
                            frmrep = New frmReports

                        Case "TRADEIN"
                            If sCompanyShortCode = "ES" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_ESP.rpt"
                            ElseIf sCompanyShortCode = "AS" Then
                                '''  strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_AS.rpt"
                                If sInvGstType = "" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_AS.rpt"
                                ElseIf sInvGstType = "Standard Rated Supply" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoiceStandard_AS.rpt"
                                ElseIf sInvGstType = "Exempt Supply" Or sInvGstType = "No GST" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoiceNoGST_AS.rpt"
                                End If
                            ElseIf sCompanyShortCode = "AJ" Then
                                '''  strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_AS.rpt"
                                If sInvGstType = "" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_AJ.rpt"
                                ElseIf sInvGstType = "Standard Rated Supply" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoiceStandard_AJ.rpt"
                                ElseIf sInvGstType = "Exempt Supply" Or sInvGstType = "No GST" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoiceNoGST_AJ.rpt"
                                End If
                            ElseIf UCase(sCompanyShortCode) = "GMT" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_GMT.rpt"
                            ElseIf UCase(sCompanyShortCode) = "ML" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_ML.rpt"
                            ElseIf UCase(sCompanyShortCode) = "HN" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_HN.rpt"
                            End If
                            CR.Load(strpath)
                            ReportLogOnToServer(CR)
                            CR.Refresh()
                            frmrep.ReportViewer.ReportSource = CR

                            sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                            frmrep.ReportViewer.SelectionFormula = sSelection
                            If UCase(sCompanyShortCode) = "HN" And bReprint Then
                                CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                            End If
                            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                            CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                            CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                            CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                            CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                            CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                            CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)

                            frmrep.Show()
                            frmrep.ReportViewer.Visible = True
                            frmrep.ReportViewer.DisplayToolbar = True
                            frmrep.ReportViewer.CloseView(False)
                            frmrep.WindowState = FormWindowState.Maximized
                            frmrep = Nothing
                            frmrep = New frmReports

                            If sCompanyShortCode = "ES" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInDetailInvoice_ESP.rpt"
                            End If

                            If ((UCase(sCompanyShortCode) <> "HN") And (UCase(sCompanyShortCode) <> "AS") And (UCase(sCompanyShortCode) <> "ML")) Then
                                CR.Load(strpath)
                                ReportLogOnToServer(CR)
                                CR.Refresh()
                                frmrep.ReportViewer.ReportSource = CR

                                sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                                frmrep.ReportViewer.SelectionFormula = sSelection
                                CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                                CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                                CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                                CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                                CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                                CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                                CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                                CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                                CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)

                                frmrep.Show()
                                frmrep.ReportViewer.Visible = True
                                frmrep.ReportViewer.DisplayToolbar = True
                                frmrep.ReportViewer.CloseView(False)
                                frmrep.WindowState = FormWindowState.Maximized
                                frmrep = Nothing
                                frmrep = New frmReports
                            End If
                        Case Else
                            Dim sCmd As String
                            Dim sExec As String

                            ' Dim Dm As New DataManager

                            'sCmd = " Exec sp_OrderPayMode " & fncFormatSQLText(sOrderNo)
                            'Dm.ProjExecNonQry(sCmd)

                            'sExec = " Exec sp_OrderTradeIn " & fncFormatSQLText(sOrderNo)
                            'Dm.ProjExecNonQry(sExec)

                            If sCompanyShortCode = "ES" Then
                                strpath = Application.StartupPath & "\Reports\rptSalesInvoice_ESP.rpt"
                            ElseIf sCompanyShortCode = "AS" Then
                                '''strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AS.rpt"
                                If sInvGstType = "" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AS.rpt"
                                ElseIf sInvGstType = "Standard Rated Supply" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceStandard_AS.rpt"
                                ElseIf sInvGstType = "Exempt Supply" Or sInvGstType = "No GST" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceNoGST_AS.rpt"
                                End If
                            ElseIf sCompanyShortCode = "AJ" Then
                                '''strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AS.rpt"
                                If sInvGstType = "" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AJ.rpt"
                                ElseIf sInvGstType = "Standard Rated Supply" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceStandard_AJ.rpt"
                                ElseIf sInvGstType = "Exempt Supply" Or sInvGstType = "No GST" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceNoGST_AJ.rpt"
                                End If
                            ElseIf sCompanyShortCode = "ML" Then
                                strpath = Application.StartupPath & "\Reports\rptSalesInvoice_ML.rpt"
                            ElseIf UCase(sCompanyShortCode) = "GMT" Then
                                strpath = Application.StartupPath & "\Reports\rptSalesInvoice_GMT.rpt"
                            ElseIf UCase(sCompanyShortCode) = "HN" Then
                                strpath = Application.StartupPath & "\Reports\rptSalesInvoice_HN.rpt"
                            End If
                            CR.Load(strpath)
                            ReportLogOnToServer(CR)
                            CR.Refresh()
                            frmrep.ReportViewer.ReportSource = CR

                            sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                            frmrep.ReportViewer.SelectionFormula = sSelection
                            If UCase(sCompanyShortCode) = "HN" And bReprint Then
                                CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                            End If
                            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                            CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                            CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                            CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                            CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                            CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                            CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                            frmrep.Show()
                            frmrep.ReportViewer.Visible = True
                            frmrep.ReportViewer.DisplayToolbar = True
                            frmrep.ReportViewer.CloseView(False)
                            frmrep.WindowState = FormWindowState.Maximized
                            frmrep = Nothing
                            frmrep = New frmReports
                    End Select
                End If

                If sReply = vbCancel Then
                    Exit Sub
                End If
                If sReply = vbCancel Then
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub subGetClassRate()
        Dim dsClass As New DataSet
        Dim i As Integer
        Try
            dsClass = objClassRate.GetClassRate()
            If dsClass.Tables(0).Rows.Count > 0 Then
                For i = 0 To dsClass.Tables(0).Rows.Count - 1
                    If Trim(dsClass.Tables(0).Rows(i)("ClassCode") & "") = "22K" Then
                        s916 = Format(Val(dsClass.Tables(0).Rows(i)("Rate") & ""), "#0.00")
                    ElseIf Trim(dsClass.Tables(0).Rows(i)("ClassCode") & "") = "916" Then
                        s916 = Format(Val(dsClass.Tables(0).Rows(i)("Rate") & ""), "#0.00")
                    ElseIf Trim(dsClass.Tables(0).Rows(i)("ClassCode") & "") = "22K-916" Then
                        s916 = Format(Val(dsClass.Tables(0).Rows(i)("Rate") & ""), "#0.00")
                    ElseIf Trim(dsClass.Tables(0).Rows(i)("ClassCode") & "") = "999" Then
                        s999 = Format(Val(dsClass.Tables(0).Rows(i)("Rate") & ""), "#0.00")
                    ElseIf Trim(dsClass.Tables(0).Rows(i)("ClassCode") & "") = "20KJ" Then
                        s835 = Format(Val(dsClass.Tables(0).Rows(i)("Rate") & ""), "#0.00")
                    ElseIf Trim(dsClass.Tables(0).Rows(i)("ClassCode") & "") = "24K" Then
                        s999 = Format(Val(dsClass.Tables(0).Rows(i)("Rate") & ""), "#0.00")
                    ElseIf Trim(dsClass.Tables(0).Rows(i)("ClassCode") & "") = "24K-999" Then
                        s999 = Format(Val(dsClass.Tables(0).Rows(i)("Rate") & ""), "#0.00")
                    ElseIf Trim(dsClass.Tables(0).Rows(i)("ClassCode") & "") = "20K-850" Then
                        s20K850 = Format(Val(dsClass.Tables(0).Rows(i)("Rate") & ""), "#0.00")
                    ElseIf Trim(dsClass.Tables(0).Rows(i)("ClassCode") & "") = "22K-SOV" Then
                        s22KSOV = Format(Val(dsClass.Tables(0).Rows(i)("Rate") & ""), "#0.00")
                    End If
                Next i
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsClass = Nothing
        End Try
    End Sub
    Public Function subCheckAmount() As Boolean
        Dim dsOrdHdr As New DataSet
        Dim dTotalAdvancePaid As Double
        Dim dTotalCostEstimated As Double
        subCheckAmount = False

        Try
            If txtOrderNo.Text.Trim = "" Then Exit Function
            dsOrdHdr = objOrder.GetOrderBalance(Trim$(txtOrderNo.Text))
            If dsOrdHdr.Tables(0).Rows.Count > 0 Then
                dTotalAdvancePaid = Format(Val(dsOrdHdr.Tables(0).Rows(0)("AdvancePaid") & ""), "0.00")
                dTotalCostEstimated = Format(Val(dsOrdHdr.Tables(0).Rows(0)("TotalCostEstimated") & ""), "0.00")
                If Val(dTotalCostEstimated) <= Val(dTotalAdvancePaid) Or chkFinalPayment.Checked Then
                    SubAddHeaderIntoSales()
                    subAddDetailsIntoSales()
                    subAddPaymodedetails()


                End If
            End If
            subCheckAmount = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function
    Public Function SubAddHeaderIntoSales()
        Dim sCustomerCode, sName, sAddress1, sAddress2, sAddress3, sSalesManCode, sSalesType, sGstType As String
        Dim dTotalWeight, dTotalStoneWeight, dTotalGoldLoss, dTotalSMC, dGST, dDetailDiscont, dNetDiscount, dDiscount, dTotalCost, dTotalPCS, dTradeinAmt, dTradeInGST As Double
        Dim dr1 As DataRow()
        Dim dm As DataManager
        Dim dsInvoiceno As DataSet

        Dim sSql, sInvDate As String
        Dim iret As Boolean
        Dim seTRSType As String

        sInvDate = CStr(Format(CDate(ServerDate), "yyyy") & "/" & Format(CDate(ServerDate), "MM") & "/" & Format(CDate(ServerDate), "dd"))
        dsInvoiceno = objSales.GetUpdateInvoice()
        If dsInvoiceno.Tables(0).Rows.Count > 0 Then
            sInvoiceno = Trim(dsInvoiceno.Tables(0).Rows(0)("NextInvoice") & "")
        End If
        ''sSql = "select a.customerCode as CustomerCode,a.name as Name,a.address1 as Address1,a.address2 as Address2, "
        ''sSql = sSql & " a.address3 as Address3,a.salesman as SalesManCode,'INSTALLMENT' as SalesType,sum(b.weight) as TotalWeight, "
        ''sSql = sSql & " 0 as TotalStoneWeight,sum(b.goldloss)as TotalGoldLoss,sum(b.smc)as TotalSMC,'I' as GSTType, "
        ''sSql = sSql & " sum(b.gst)as GST,0 as DetailDiscount,0 as NetDiscount,0 as Discount,a.totalcost as TotalCost,(select Count(*) from tblinstallmententrydetail where insagreementno=" & fncFormatSQLText(txtOrderNo.Text) & ")as TotalPCS "
        ''sSql = sSql & " from tblinstallmententryheader a,tblinstallmententrydetail b "
        ''sSql = sSql & " where a.insagreementno=b.insagreementno and a.insagreementno=" & fncFormatSQLText(txtOrderNo.Text)
        ''sSql = sSql & " group by a.customerCode,a.name,a.address1,a.address2,a.address3,a.salesman,a.totalcost"



        sSql = "Select a.customerCode as CustomerCode,'' as Name,'' as Address1,'' as Address2,  '' as Address3,"
        sSql = sSql & " a.SalesManCode as SalesManCode,'ORDER' as SalesType,sum(b.WeightEstimated) as TotalWeight,  0 as TotalStoneWeight,"
        sSql = sSql & " 0 as TotalGoldLoss,0 as TotalSMC,'I' as GSTType, TotalGSTEstimated as GST,0 as DetailDiscount,"
        sSql = sSql & " 0 as NetDiscount,0 as Discount,a.AdvancePaid as TotalCost,"
        sSql = sSql & "(select Count(*) from tblinventoryorderdetail where OrderNo= " & fncFormatSQLText(txtOrderNo.Text) & " )as TotalPCS, a.TradeInAmt,isnull(a.TradeInGST,0)TradeInGST"
        sSql = sSql & " from tblInventoryOrderHeader a,tblInventoryOrderDetail b  "
        sSql = sSql & " where a.OrderNO=b.OrderNo  and  a.OrderNo=" & fncFormatSQLText(txtOrderNo.Text)
        sSql = sSql & " group by a.customerCode,a.AdvancePaid,TotalGSTEstimated,a.SalesManCode,a.TradeInAmt,a.TradeInGST"

        dr1 = DataManager.RowReturnProjExecNonQryWithDataRow(sSql)
        If dr1.Length > 0 Then
            sCustomerCode = Trim(dr1(0)("CustomerCode"))
            sName = Trim(dr1(0)("Name"))
            sAddress1 = Trim(dr1(0)("Address1"))
            sAddress2 = Trim(dr1(0)("Address2"))
            sAddress3 = Trim(dr1(0)("Address3"))
            sSalesManCode = Trim(dr1(0)("SalesManCode"))
            sSalesType = Trim(dr1(0)("SalesType"))
            dTotalWeight = Val(dr1(0)("TotalWeight"))
            dTotalStoneWeight = Val(dr1(0)("TotalStoneWeight"))

            dTotalGoldLoss = Val(dr1(0)("TotalGoldLoss"))

            dTotalSMC = Val(dr1(0)("TotalSMC"))
            dTotalPCS = Val(dr1(0)("TotalPCS"))
            sGstType = Trim(dr1(0)("GSTType"))

            dGST = Val(dr1(0)("GST")) + Val(dr1(0)("TradeInGST"))
            dDetailDiscont = Val(dr1(0)("DetailDiscount"))

            dDiscount = Val(dr1(0)("NetDiscount"))
            dTotalCost = Val(dr1(0)("TotalCost"))
            dTradeinAmt = Val(dr1(0)("TradeInAmt"))
            dTradeInGST = Val(dr1(0)("TradeInGST"))

            If opteTrsTicketIssued.Checked = True Then
                seTRSType = "Y"
            ElseIf opteTRSNo.Checked = True Then
                seTRSType = "N"
            ElseIf opteTRSEligible.Checked = True Then
                seTRSType = "E"
            End If
            iret = objOrder.UpdateDetailsintoSales(sInvoiceno, sInvDate, txtOrderNo.Text, sCustomerCode, sName, sAddress1, sAddress2, sAddress3, _
                                                 sSalesManCode, sSalesType, dTotalWeight, dTotalStoneWeight, dTotalGoldLoss, _
                                                 dTotalSMC, sGstType, dGST, dDetailDiscont, dDiscount, dTotalCost, dTotalPCS, seTRSType, sUserID, dTradeinAmt, dTradeInGST)


            Dim sDate As String
            sDate = Format(ServerDate, "dd/MM/yyyy")

            iret = objOrder.PosMemberTransationSave(Trim(gthisLocation), sInvoiceno, sDate, Trim(txtCustomerCode.Text), _
                                         dTotalCost, dTotalCost, Val(dRedeemedPoints))
            If Not iret Then
                MessageBox.Show("Error in Order Sales Save")
                SubAddHeaderIntoSales = False
                Exit Function
            End If

            Dim sFlexiLocation As String
            sFlexiLocation = Trim(gthisLocation)
            If Trim(sFlexiLocation) = "99" Then
                sFlexiLocation = "103"
            ElseIf Trim(sLocationCode) = "27" Then
                sFlexiLocation = "1"
            End If
            If Trim(txtSubscriptionID.Text) <> "" Then
                iret = objOrder.fncUpdatePayModeFlexiClosure(Trim(txtSubscriptionID.Text), Trim(sFlexiLocation), Trim(sUserID))
                If Not iret Then
                    MessageBox.Show("Error in FlexiSave")
                    SubAddHeaderIntoSales = False
                    Exit Function
                End If
            End If
        End If
    End Function

    Public Function subAddDetailsIntoSales()
        Dim InvoiceNo, InventoryCodeOrder, InventoryCode, Description, Locationcode, InsAgreementNo As String
        Dim Goldloss, Cost, PCS, SMC, GST, TotalValue, Weight, Rate, TotalCost As Double

        Dim ds1 As New DataSet
        Dim ds2 As New DataSet
        Dim ds3 As New DataSet
        Dim dm As New DataManager
        Dim sSql As String
        Dim iret As Boolean
        Dim icnt As Long
        ' sSql = "Select * from tblInstallmentEntryDetail where InsAgreementNo=" & fncFormatSQLText(txtOrderNo.Text)
        sSql = "Select * from tblInventoryOrderHeader where OrderNo=" & fncFormatSQLText(txtOrderNo.Text)
        ds3 = dm.getDataSetWithSqlAdapter(sSql, "tblInventoryOrderHeader", "tblInventoryOrderHeader")
        sSql = "Select * from tblInventoryOrderDetail where OrderNo=" & fncFormatSQLText(txtOrderNo.Text)
        ds1 = dm.getDataSetWithSqlAdapter(sSql, "tblInventoryOrderDetail", "tblInventoryOrderDetail")
        For icnt = 0 To ds1.Tables("tblInventoryOrderDetail").Rows.Count - 1
            InventoryCodeOrder = Trim(ds1.Tables("tblInventoryOrderDetail").Rows(icnt)("InventoryCode"))
            sSql = "Select * from tblInventory where InventoryCode=" & fncFormatSQLText(InventoryCodeOrder)
            ds2 = dm.getDataSetWithSqlAdapter(sSql, "tblInventory", "tblInventory")
            InventoryCode = Trim(ds2.Tables("tblInventory").Rows(0)("InventoryCode"))
            Description = Trim(ds2.Tables("tblInventory").Rows(0)("Description"))
            Locationcode = Trim(gthisLocation)
            Weight = Val(Trim(ds2.Tables("tblInventory").Rows(0)("Weight")))
            Rate = Val(Trim(ds1.Tables("tblInventoryOrderDetail").Rows(icnt)("CostPerGram")))
            InsAgreementNo = Trim(ds1.Tables("tblInventoryOrderDetail").Rows(icnt)("OrderNo"))
            Goldloss = Val(Trim(ds2.Tables("tblInventory").Rows(0)("GoldLoss")))
            Cost = Val(Trim(ds2.Tables("tblInventory").Rows(0)("TotalCost")))
            PCS = 1 'Val(Trim(ds1.Tables("tblInstallmentEntryDetail").Rows(icnt)("PCS")))
            SMC = Val(Trim(ds2.Tables("tblInventory").Rows(0)("SMC")))
            GST = Val(Trim(ds1.Tables("tblInventoryOrderDetail").Rows(icnt)("Estimatedgst")))
            TotalValue = Val(Trim(ds1.Tables("tblInventoryOrderDetail").Rows(icnt)("EstimatedCost")))
            TotalCost = Val(Trim(ds2.Tables("tblInventory").Rows(0)("TotalCost")))

            iret = objOrder.SalesDetailSave(sInvoiceno, InventoryCode, Description, Trim(sLocationCode), Weight, 0, Goldloss, Rate, SMC, PCS, 0, 0, GST, TotalValue, 0, TotalValue, TotalCost, 0, "", 0, 0, 0, "INSTALLMENT")

            If Not iret Then
                MessageBox.Show("Error in Order Sales Save")
                subAddDetailsIntoSales = False
                Exit Function
            End If

        Next

    End Function
    Public Function subAddPaymodedetails()
        Dim objPM As New clsPayMode
        Try
            Dim bPayModeFlag As Boolean
            Dim sDate As String
            Dim i As Long
            Dim dm As New DataManager
            Dim ds3 As New DataSet
            Dim ssql As String
            Dim dAmount As Double

            'ssql = "Select sum(Amount)Amount from tblPayModeDetail where InvoiceNo=" & fncFormatSQLText(txtOrderNo.Text)

            ssql = "select sum(amount)Amount from("
            ssql = ssql & " Select sum(Amount)Amount from tblPayModeDetail where InvoiceNo=" & fncFormatSQLText(txtOrderNo.Text) & ""
            ssql = ssql & " union all"
            ssql = ssql & " Select  sum(Amount)Amount from tblPayModeDetail where InvoiceNo in("
            ssql = ssql & " Select OrderPaymentNo from tblorderpaymentdetail where orderno=" & fncFormatSQLText(txtOrderNo.Text) & "))a "


            ds3 = dm.getDataSetWithSqlAdapter(ssql, "tblPayModeDetail", "tblPayModeDetail")
            dAmount = Val(Trim(ds3.Tables("tblPayModeDetail").Rows(0)("Amount")))
            bPayModeFlag = False
            sDate = Format(Now, "dd/MM/yyyy")
            If bPayModeFlag = False Then
                iRet = objPM.PayModeDetailSave(sInvoiceno, sDate, "ORDER", _
                                            Format(Val(dAmount), "0.00"), 0, _
                                            "MANUAL", "ORDER")



                If Not iRet Then
                    MessageBox.Show("Error in InstallmentSave")
                    subAddPaymodedetails = False
                    Exit Function
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        'Dim objPM As New clsPayMode
        'Try
        '    Dim bPayModeFlag As Boolean
        '    Dim sDate As String
        '    Dim i As Long
        '    bPayModeFlag = False
        '    sDate = Format(Now, "dd/MM/yyyy")
        '    For i = 0 To UBound(arrpay, 1)
        '        If arrpay(i, 1) > 0 Then
        '            iRet = objPM.PayModeDetailSave(sInvoiceno, sDate, Trim(arrpay(i, 3)), _
        '                                         Val(arrpay(i, 1)), Val(arrpay(i, 2)), _
        '                                         Trim(arrpay(i, 4)), "ORDER")
        '            bPayModeFlag = True
        '            If Not iRet Then
        '                subAddPaymodedetails = False
        '                Exit Function
        '            End If

        '        End If
        '    Next i
        '    If bPayModeFlag = False Then
        '        iRet = objPM.PayModeDetailSave(sInvoiceno, sDate, "CASH", _
        '                                    Val(txtPayAmount.Text), 0, _
        '                                    "MANUAL", "ORDER")
        '        If Not iRet Then
        '            subAddPaymodedetails = False
        '            Exit Function
        '        End If
        '    End If
        '    subAddPaymodedetails = True
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
    End Function
    Private Function fncValidateMe() As Boolean
        Dim bValid As Boolean
        Try
            bValid = False
            If txtOrderNo.Text.Trim = "" Then
                MessageBox.Show("Please enter OrderNo.")
                txtOrderNo.Focus()
                Exit Function
            End If
            If Val(txtPayAmount.Text.Trim) = 0 Then
                MessageBox.Show("Please enter pay amount.")
                txtPayAmount.Focus()
                Exit Function
            End If
            If Not IsNumeric(txtPayAmount.Text.Trim) Then
                MessageBox.Show("Please enter valid Pay Amount.")
                txtPayAmount.Clear()
                txtPayAmount.Focus()
                Exit Function
            End If
            If Val(txtPayAmount.Text.Trim) < 0 Then
                MessageBox.Show("Pay amount should not be greater than zero.")
                txtPayAmount.Clear()
                txtPayAmount.Focus()
                Exit Function
            End If
            If (Val(txtPayAmount.Text.Trim) >= Val(lblBalanceAmount.Text) Or chkFinalPayment.Checked) And Trim(sInventoryCode) = "" Then
                MessageBox.Show("Inventory Not Matched")
                'txtPayAmount.Text = Format(Val(lblBalanceAmount.Text), "#0.00")
                txtPayAmount.Clear()
                txtPayAmount.Focus()
                Exit Function
            End If
            bValid = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncValidateMe = bValid
        End Try
    End Function

    Private Sub txtOrderNo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOrderNo.LostFocus
        Try
            If txtOrderNo.Text.Trim <> "" Then
                subGetOrderDetails()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function fncPaymodeDetailSave(ByVal sOrderNo As String) As Boolean
        Dim objPM As New clsPayMode
        Try
            Dim bPayModeFlag As Boolean
            Dim sDate As String
            Dim i As Long
            bPayModeFlag = False
            sDate = Format(Now, "dd/MM/yyyy")
            For i = 0 To UBound(arrpay, 1)
                If arrpay(i, 1) > 0 Then
                    iRet = objPM.PayModeDetailSave(sOrderNo, sDate, Trim(arrpay(i, 3)), _
                                                 Val(arrpay(i, 1)), Val(arrpay(i, 2)), _
                                                 Trim(arrpay(i, 4)), "ORDER")
                    bPayModeFlag = True
                    If Not iRet Then
                        fncPaymodeDetailSave = False
                        Exit Function
                    End If

                End If

                If Trim(arrpay(i, 3)) = "REDEEMPOINTS" Then
                    If Trim(lblCustomerType.Text) = "RUBY" Then
                        dRedeemedPoints = Val(arrpay(i, 1)) / 5 * 1000
                    ElseIf Trim(lblCustomerType.Text) = "EMERALD" Then
                        dRedeemedPoints = Val(arrpay(i, 1)) / 7 * 1000
                    ElseIf Trim(lblCustomerType.Text) = "DIAMOND" Then
                        dRedeemedPoints = Val(arrpay(i, 1)) / 9 * 1000
                    End If
                End If
            Next i
            If bPayModeFlag = False Then
                iRet = objPM.PayModeDetailSave(sOrderNo, sDate, "CASH", _
                                            Val(txtPayAmount.Text), 0, _
                                            "MANUAL", "ORDER")
                If Not iRet Then
                    fncPaymodeDetailSave = False
                    Exit Function
                End If
            End If
            fncPaymodeDetailSave = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Function fncSaveOrderPaymentDetail() As Boolean
        Try
            Dim iRet As Boolean
            Dim i As Long
            Dim sRemarks$
            Dim sSql$
            iRet = objOrder.FncOrderPaymentSave(txtOrderNo.Text.Trim, msOrderpaymentNo.Trim, Val(txtPayAmount.Text.Trim), txtRemarks.Text.Trim, Val(dTradeInSales), Val(dTradeInGST))
            If Not iRet Then
                MessageBox.Show("Error in Order Payment save.")
                fncSaveOrderPaymentDetail = False
                Exit Function
            End If
            fncSaveOrderPaymentDetail = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        Try
            If txtOrderNo.Text.Trim = "" Then
                MessageBox.Show("Pleaes select order number.")
                txtOrderNo.Focus()
                Exit Sub
            End If

            If grdOrderPayment.RowCount - 1 > 0 And grdOrderPayment.CurrentRow.Index < grdOrderPayment.RowCount - 1 Then
                ' SmartJewel ToDo
                If (MessageBox.Show("Are you sure, do you want to cancel the order payment?", "GoldPOS", MessageBoxButtons.YesNo)) = DialogResult.Yes Then subCancelOrderPayment()
            End If
            subClearMe()
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try
    End Sub

    Private Sub subCancelOrderPayment()
        Dim objPM As New clsPayMode
        Try
            objOrder.OpenOrderConnection()
            objOrder.gpBeginOrderTransaction()
            If Not objOrder.fncOrderPaymentDetailDelete(txtOrderNo.Text.Trim, fnGetdgvText(grdOrderPayment, grdOrderPayment.CurrentRow.Index, cOrderPaymentNo), fnGetdgvText(grdOrderPayment, grdOrderPayment.CurrentRow.Index, cPaidAmt.Index)) Then
                objOrder.gpRollBackOrderConnection()
            Else
                If Not objPM.PayModeDetailSave(txtOrderNo.Text.Trim, Format(Now, "dd/MM/yyyy"), "CASH", -Val(fnGetdgvText(grdOrderPayment, grdOrderPayment.CurrentRow.Index, cAmountPaid)), Val(0), "OP CANCEL", "ORDER") Then
                    objOrder.gpRollBackOrderConnection()
                Else
                    MessageBox.Show("Order Payment Successful")
                    objOrder.gpCommitOrderTransaction()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            objOrder.CloseOrderConnection()
            objPM = Nothing
        End Try
    End Sub

    Private Sub txtOrderNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtOrderNo.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then btnOrderPaymentNoSearch_Click(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnOrderPaymentNoSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOrderPaymentNoSearch.Click
        Try
            'Dim frmHelpOrder As New frmHelpSearch(0, 0, "tblinventoryorderHeader", "L", 4, "OrderNo", "(Status = 'O' or Status = 'P') and AdvancePaid < TotalCostEstimated  and OrderNo in (Select Orderno from tblinventoryorderDetail where InventoryCode<>'')", "OrderNo", "OrderNo", 120, "OrderDate", "OrderDate", 180, "CustomerCode", "CustomerCode", 60, "SupplierCode", "SupplierCode", 80)
            Dim frmHelpOrder As New frmHelpSearch(0, 0, "tblinventoryorderHeader", "L", 4, "OrderNo", " AdvancePaid < TotalCostEstimated  ", "OrderNo", "OrderNo", 120, "OrderDate", "OrderDate", 180, "CustomerCode", "CustomerCode", 60, "SupplierCode", "SupplierCode", 80)
            frmHelpOrder.ShowDialog()
            txtOrderNo.Text = frmHelpOrder.Var1.Trim
            If txtOrderNo.Text.Trim <> "" Then subGetOrderDetails()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtPayAmount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPayAmount.LostFocus
        Try
            If IsNumeric(txtPayAmount.Text.Trim) Then
                txtPayAmount.Text = Format(Val(txtPayAmount.Text), "#0.00")
            Else
                txtPayAmount.Text = "0.00"
            End If
            If Val(lblAdvancePaid.Text) + Val(txtPayAmount.Text) < dTotalCost And chkFinalPayment.Checked Then
                MessageBox.Show("Paid Amount Lesser than Cost. Enter Super Admin Password")
                subGetAdminPassword()
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subGetAdminPassword()
        Try
            pnlCostAdminPwd.Visible = True
            pnlCostAdminPwd.BringToFront()
            txtCostAdminPassword.Clear()
            txtCostAdminPassword.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnTradeIn_Click(sender As System.Object, e As System.EventArgs) Handles btnTradeIn.Click
        Try
            Dim TradeIn As New frmTradeInGold
            'subGetCompany()
            grdSales.RowCount = 0
            TradeIn.sCalledFrom = "ORDERFORM"
            TradeIn.sInvoiceNo = Format(Val(txtOrderNo.Text), "00000000")
            TradeIn.txtCustomerCode.Text = txtCustomerCode.Text
            TradeIn.txtCustomerName.Text = txtCustomerName.Text
            TradeIn.lblCustomerType.Text = lblCustomerType.Text
            TradeIn.txtAddress1.Text = txtAddress1.Text
            TradeIn.txtAddress2.Text = txtAddress2.Text
            TradeIn.txtAddress3.Text = txtAddress3.Text
            TradeIn.lblTradeInLimit.Text = dTradeinLimit
            TradeIn.lblSalesAmount.Text = Val(lblBalanceAmount.Text)
            TradeIn.sSalesAmount = Val(lblBalanceAmount.Text)
            'TradeIn.txtCostPerGram.Text = Val(txtCurrCostPerGram.Text)
            TradeIn.ShowDialog()
            txtPayAmount.Text = dTradeinAmount
            tradeInRefNo = sTradeInRefNo
            dTradeInSales = dTradeinAmount
            dTradeInGST = Val(-Format$((Val(dTradeinAmount) * 9) / (109), "#0.00"))
            subAddTradeInToGrid()
            If ChkTradeIn Then
                btnTradeIn.Enabled = False
                optCash.Enabled = False
                If optBoth.Checked Then
                    txtPayAmount.Enabled = True
                Else
                    txtPayAmount.Enabled = False
                End If
            Else
                btnTradeIn.Enabled = True
                optCash.Enabled = True
                txtPayAmount.Enabled = True
            End If
            sType = "TRADEIN"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subAddTradeInToGrid()
        Dim sTiInventoryCode As String
        Dim sTiClassCode As String
        Dim sTiDescription As String
        Dim sTiWeight As String
        Dim sTiGoldLoss As String
        Dim sTiCostPerGram As String
        Dim sTiStoneWeight As String
        Dim sTiSmc As String
        Dim sTiPurMC As String
        Dim sTiPCS As String
        Dim sTiTotalValue As String
        Dim sTiTotalCost As String
        Dim sTiSalesType As String
        Dim sTiGST As String
        Dim iRow As Long
        Dim i As Long
        Dim bEdited As Boolean
        Try
            dTradeInSales = 0
            For iRow = 0 To dsTradeIn.Tables(0).Rows.Count - 1
                grdSales.ForeColor = Color.Black

                'If Not bEdited Then
                '    If grdSales.MaxRows = 0 Then
                '        grdSales.MaxRows = grdSales.MaxRows + 1
                '    Else
                '        grdSales.Row = grdSales.MaxRows
                '        grdSales.Action = 5
                '        grdSales.MaxRows = grdSales.MaxRows - 1
                '        grdSales.MaxRows = grdSales.MaxRows + 1
                '    End If
                '    grdSales.Row = grdSales.MaxRows
                'Else
                '    grdSales.Row = grdSales.ActiveRow
                'End If
                If Not bEdited Then
                    If grdSales.RowCount = 0 Then
                        grdSales.Rows.Add()
                    Else
                        'grdSales.Row = grdSales.MaxRows
                        'grdSales.Action = 5
                        'grdSales.MaxRows = grdSales.MaxRows - 1
                        'grdJobSheet.MaxRows = grdJobSheet.MaxRows + 1
                        grdSales.Rows.Add()
                    End If
                    i = grdSales.RowCount - 1
                Else
                    i = grdSales.CurrentRow.Index
                End If
                sTiInventoryCode = dsTradeIn.Tables(0).Rows(iRow).Item("ItemNo") & "".Trim
                sTiClassCode = dsTradeIn.Tables(0).Rows(iRow).Item("sClass") & "".Trim
                sTiDescription = dsTradeIn.Tables(0).Rows(iRow).Item("sDesc") & "".Trim
                sTiWeight = dsTradeIn.Tables(0).Rows(iRow).Item("sWeight") & "".Trim
                sTiGoldLoss = dsTradeIn.Tables(0).Rows(iRow).Item("sGoldLoss") & "".Trim
                sTiCostPerGram = dsTradeIn.Tables(0).Rows(iRow).Item("sCostPerGram") & "".Trim
                sTiStoneWeight = dsTradeIn.Tables(0).Rows(iRow).Item("sStoneWeight") & "".Trim
                sTiSmc = "0"
                sTiPurMC = dsTradeIn.Tables(0).Rows(iRow).Item("sPMC") & "".Trim
                sTiPCS = dsTradeIn.Tables(0).Rows(iRow).Item("sPCS") & "".Trim
                'sTiTotalValue = (Val(sTiWeight) + Val(sTiGoldLoss)) * Val(sTiCostPerGram)
                sTiTotalValue = dsTradeIn.Tables(0).Rows(iRow).Item("sTotalCost") & "".Trim
                'sTiTotalValue = dsTradeIn.Tables(0).Rows(iRow).Item("sTotalCost") & "".Trim
                sTiTotalCost = dsTradeIn.Tables(0).Rows(iRow).Item("sTotalCost") & "".Trim
                sTiSalesType = "TI"


                If UCase(sCompanyShortCode) = "MJ" Or UCase(sCompanyShortCode) = "SSJ" Or UCase(sCompanyShortCode) = "AJ" Or UCase(sCompanyShortCode) = "MD" Then
                    sTiTotalValue = Format(Val(sTiTotalValue) + 0, "0.00")
                    dTradeInGST = dTradeInGST + Val(-Format$((Val(sTiTotalValue) * dGstPerc) / (109), "#0.00"))
                    dTradeInSales = dTradeInSales + Format(Val(sTiTotalValue) + 0, "0.00")
                End If

                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(ICode).Style.ForeColor = Color.Red
                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(ICode).Style.Font = New Font(grdSales.Font.FontFamily, 9, FontStyle.Bold)

                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(IWeight).Style.ForeColor = Color.Red
                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(IWeight).Style.Font = New Font(grdSales.Font.FontFamily, 9, FontStyle.Bold)

                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(IGoldLoss).Style.ForeColor = Color.Red
                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(IGoldLoss).Style.Font = New Font(grdSales.Font.FontFamily, 9, FontStyle.Bold)

                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(ISMC).Style.ForeColor = Color.Red
                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(ISMC).Style.Font = New Font(grdSales.Font.FontFamily, 9, FontStyle.Bold)

                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(IPCS).Style.ForeColor = Color.Red
                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(IPCS).Style.Font = New Font(grdSales.Font.FontFamily, 9, FontStyle.Bold)

                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(ITotalValue).Style.ForeColor = Color.Red
                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(ITotalValue).Style.Font = New Font(grdSales.Font.FontFamily, 9, FontStyle.Bold)

                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(IDSWeight).Style.ForeColor = Color.Red
                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(IDSWeight).Style.Font = New Font(grdSales.Font.FontFamily, 9, FontStyle.Bold)

                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(ITotalCost).Style.ForeColor = Color.Red
                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(ITotalCost).Style.Font = New Font(grdSales.Font.FontFamily, 9, FontStyle.Bold)

                Call fnSetdgvText(grdSales, i, ICode, sTiInventoryCode)
                Call fnSetdgvText(grdSales, i, IClass, sTiClassCode)
                Call fnSetdgvText(grdSales, i, IDescription, sTiDescription)
                Call fnSetdgvText(grdSales, i, IWeight, Val(sTiWeight))
                Call fnSetdgvText(grdSales, i, IGoldLoss, Val(sTiGoldLoss))
                Call fnSetdgvText(grdSales, i, ICostPerGram, Val(sTiCostPerGram))
                Call fnSetdgvText(grdSales, i, IDSWeight, Val(sTiStoneWeight))
                Call fnSetdgvText(grdSales, i, ISMC, Val(sTiSmc))
                Call fnSetdgvText(grdSales, i, IPMC, Val(sTiPurMC))
                Call fnSetdgvText(grdSales, i, IPCS, Val(sTiPCS))
                Call fnSetdgvText(grdSales, i, ITotalValue, Val(-sTiTotalValue))
                Call fnSetdgvText(grdSales, i, ITotalCost, Val(sTiTotalCost))
                Call fnSetdgvText(grdSales, i, ISalesType, sTiSalesType)
                Call fnSetdgvText(grdSales, i, IGST, Val(-sTiGST))
                Call fnSetdgvText(grdSales, i, IDiscount, Val(0))


                'subTotalFromGrid()
                'subTotal()
                'subClearDetail()

                bEdited = False
            Next iRow

            dsTradeIn.Tables(0).Rows.Clear()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtOrderNo_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtOrderNo.TextChanged

    End Sub

    Private Sub txtOrderNo_StyleChanged(sender As Object, e As System.EventArgs) Handles txtOrderNo.StyleChanged

    End Sub

    Private Sub GroupBox2_Enter(sender As System.Object, e As System.EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub btnCostAdminPwdOK_Click(sender As Object, e As System.EventArgs) Handles btnCostAdminPwdOK.Click
        Dim dsPWD As New DataSet
        Dim dsIns As New DataSet
        Dim objSM As New clsSalesMan

        Try
            If Trim(txtCostAdminPassword.Text) <> "" Then
                dsPWD = objSM.GetSalesManwithPassword(txtCostAdminPassword.Text)
                If dsPWD.Tables(0).Rows.Count > 0 Then
                    If Trim(UCase(dsPWD.Tables(0).Rows(0)("SalesManGroup") & "")) = "SUPER ADMIN" Then
                        pnlCostAdminPwd.Visible = False
                        'subAddGrid()
                        'bEdited = False
                        'txtDescription.Focus()
                    Else
                        'MessageBox.Show("Please Check the Password")
                        MsgBox("SalesMan Has No Rights to do Change Cost!", MsgBoxStyle.Critical)
                        txtCostAdminPassword.Text = ""
                        If (txtCostAdminPassword.Enabled And txtCostAdminPassword.Visible) Then txtCostAdminPassword.Focus()

                        Exit Sub
                    End If
                Else
                    ' MessageBox.Show("Please Check the Password")
                    MsgBox("SalesMan Has No Rights to do Find Cost!", MsgBoxStyle.Critical)
                    txtCostAdminPassword.Text = ""
                    If (txtCostAdminPassword.Enabled And txtCostAdminPassword.Visible) Then txtCostAdminPassword.Focus()

                    Exit Sub
                End If
            Else
                MessageBox.Show("Enter the Password")
                txtCostAdminPassword.Focus()

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnCostAdminPwdCancel_Click(sender As Object, e As System.EventArgs) Handles btnCostAdminPwdCancel.Click
        pnlCostAdminPwd.Visible = False
        txtCostAdminPassword.Focus()
    End Sub

    Private Sub btnSubscriptionID_Click(sender As System.Object, e As System.EventArgs) Handles btnSubscriptionID.Click
        Try
            'Dim sFlexiLocation As String
            'sFlexiLocation = Trim(gthisLocation)
            'If Trim(sFlexiLocation) = "03" Then
            '    sFlexiLocation = "103"
            'ElseIf Trim(sLocationCode) = "27" Then
            '    sFlexiLocation = "1"
            'End If
            'Dim frmHelpCustomer As New frmHelpSearch(0, 0, "vwFlexiFinalClosure", "R", 4, "SubscriptionId", "isProcessed='N' and LocationCode =" & fncFormatSQLText(sFlexiLocation), "SubscriptionId", "SubscriptionId", 80, "CustomerName", "CustomerName", 90, "TotalAmount", "TotalAmount", 90, "TotalBonus", "TotalBonus", 80)
            'frmHelpCustomer.ShowDialog()
            'If frmHelpCustomer.Var1.Trim <> "" Then
            '    txtSubscriptionID.Text = frmHelpCustomer.Var1.Trim
            '    txtCustomerName.Text = frmHelpCustomer.Var2.Trim
            '    dFlexiFinalBonus = frmHelpCustomer.Var3.Trim
            '    dFlexiFinalAmount = frmHelpCustomer.Var4.Trim
            'End If
            subMultipleSearchClick(CSearchLocation, txtSubscriptionID.Text)
            subGetSubcription(txtSubscriptionID.Text)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subGetSubcription(ByVal sSubsriptionId As String)
        Dim dsCls As New DataSet
        Dim sFlexiLocation As String
        Dim objSales As New ClsSales
        Try


            sFlexiLocation = Trim(gthisLocation)
            If Trim(sFlexiLocation) = "99" Then
                sFlexiLocation = "103"
            ElseIf Trim(sLocationCode) = "27" Then
                sFlexiLocation = "1"
            End If
            dsCls = objSales.subGetSubcription(Trim(sSubsriptionId), Trim(sFlexiLocation))
            If dsCls.Tables(0).Rows.Count > 0 Then
                dFlexiFinalAmount = Val(dsCls.Tables(0).Rows(0)("TotalAmount") & "")
                dFlexiFinalBonus = Val(dsCls.Tables(0).Rows(0)("TotalBonus") & "")
                ' txtCustomerName.Text = Trim(dsCls.Tables(0).Rows(0)("customerName") & "")
            Else
                MessageBox.Show("Class Not defined for this item .... ")
                Exit Sub
            End If
            dsCls = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnRePrint_Click(sender As System.Object, e As System.EventArgs) Handles btnRePrint.Click
        Try
            PnlReprint.Visible = True
            txtRePrintBillNo.Clear()
            txtRePrintBillNo.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnReprintOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReprintOK.Click
        Try
            If Trim(txtRePrintBillNo.Text) <> "" Then
                bReprint = True
                'Call subPrintBill(Trim(txtRePrintBillNo.Text))
                Call subPrintOrderPayment(Trim(txtRePrintBillNo.Text))
                bReprint = False
                PnlReprint.Visible = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnReprintCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReprintCancel.Click
        Try
            txtRePrintBillNo.Clear()
            PnlReprint.Visible = False
            ' txtNoofInstallment.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
