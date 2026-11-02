Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frmInstallmentPayment
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
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtNoofInstallment As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtInstallmentAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
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
    Friend WithEvents btnAgreementNoSearch As System.Windows.Forms.Button
    Friend WithEvents txtTotalAmount As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblInstallmentAmount As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lblDepositAmount As System.Windows.Forms.Label
    Friend WithEvents lblBalanceAmount As System.Windows.Forms.Label
    Friend WithEvents lblSalesAmount As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnSAve As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnBillClear As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnRePrint As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents btnBillCancel As System.Windows.Forms.Button
    Friend WithEvents btnBillCancelOK As System.Windows.Forms.Button
    Friend WithEvents txtBillCancelBillNo As System.Windows.Forms.TextBox
    Friend WithEvents PnlReprint As System.Windows.Forms.Panel
    Friend WithEvents btnReprintCancel As System.Windows.Forms.Button
    Friend WithEvents btnReprintOK As System.Windows.Forms.Button
    Friend WithEvents txtRePrintBillNo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents pnlCancel As System.Windows.Forms.Panel
    Friend WithEvents txtAgreementNo As System.Windows.Forms.TextBox
    Friend WithEvents lblCustomerType As System.Windows.Forms.Label
    Friend WithEvents txtMemberCode As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents pnlMember As System.Windows.Forms.Panel
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents txtMemberCodeView As System.Windows.Forms.TextBox
    Friend WithEvents Label79 As System.Windows.Forms.Label
    Friend WithEvents btnMemberCodeSearch As System.Windows.Forms.Button
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents btnTradeIn As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents optBoth As System.Windows.Forms.RadioButton
    Friend WithEvents optCash As System.Windows.Forms.RadioButton
    Friend WithEvents optGold As System.Windows.Forms.RadioButton
    Friend WithEvents grdInstallment As System.Windows.Forms.DataGridView
    Friend WithEvents cInstallmentNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cInstallmentDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cNoofInstallment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cInstallmentAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cAmountPaid As System.Windows.Forms.DataGridViewTextBoxColumn
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
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents opteTRSNo As System.Windows.Forms.RadioButton
    Friend WithEvents opteTrsTicketIssued As System.Windows.Forms.RadioButton
    Friend WithEvents opteTRSEligible As System.Windows.Forms.RadioButton
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents btnSubscriptionID As System.Windows.Forms.Button
    Friend WithEvents txtSubscriptionID As System.Windows.Forms.TextBox
    Friend WithEvents btnCancelFme As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInstallmentPayment))
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
        Me.btnTradeIn = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.optBoth = New System.Windows.Forms.RadioButton()
        Me.optCash = New System.Windows.Forms.RadioButton()
        Me.optGold = New System.Windows.Forms.RadioButton()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtMemberCode = New System.Windows.Forms.TextBox()
        Me.lblCustomerType = New System.Windows.Forms.Label()
        Me.btnAgreementNoSearch = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtTotalAmount = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtNoofInstallment = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtInstallmentAmount = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtAddress3 = New System.Windows.Forms.TextBox()
        Me.txtAddress2 = New System.Windows.Forms.TextBox()
        Me.txtAddress1 = New System.Windows.Forms.TextBox()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.btnCustomerCodeSearch = New System.Windows.Forms.Button()
        Me.txtCustomerCode = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtAgreementNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblInstallmentAmount = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblDepositAmount = New System.Windows.Forms.Label()
        Me.lblBalanceAmount = New System.Windows.Forms.Label()
        Me.lblSalesAmount = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnSAve = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnBillClear = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnRePrint = New System.Windows.Forms.Button()
        Me.pnlCancel = New System.Windows.Forms.Panel()
        Me.btnBillCancel = New System.Windows.Forms.Button()
        Me.btnBillCancelOK = New System.Windows.Forms.Button()
        Me.txtBillCancelBillNo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.PnlReprint = New System.Windows.Forms.Panel()
        Me.btnReprintCancel = New System.Windows.Forms.Button()
        Me.btnReprintOK = New System.Windows.Forms.Button()
        Me.txtRePrintBillNo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.pnlMember = New System.Windows.Forms.Panel()
        Me.btnMemberCodeSearch = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancelFme = New System.Windows.Forms.Button()
        Me.txtMemberCodeView = New System.Windows.Forms.TextBox()
        Me.Label79 = New System.Windows.Forms.Label()
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.grdInstallment = New System.Windows.Forms.DataGridView()
        Me.cInstallmentNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cInstallmentDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cNoofInstallment = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cInstallmentAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cAmountPaid = New System.Windows.Forms.DataGridViewTextBoxColumn()
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
        Me.Panel1.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlCancel.SuspendLayout()
        Me.PnlReprint.SuspendLayout()
        Me.pnlMember.SuspendLayout()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdInstallment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSales, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Panel1.Controls.Add(Me.btnTradeIn)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.txtMemberCode)
        Me.Panel1.Controls.Add(Me.lblCustomerType)
        Me.Panel1.Controls.Add(Me.btnAgreementNoSearch)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.txtTotalAmount)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.txtNoofInstallment)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.txtInstallmentAmount)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtRemarks)
        Me.Panel1.Controls.Add(Me.txtAddress3)
        Me.Panel1.Controls.Add(Me.txtAddress2)
        Me.Panel1.Controls.Add(Me.txtAddress1)
        Me.Panel1.Controls.Add(Me.txtCustomerName)
        Me.Panel1.Controls.Add(Me.btnCustomerCodeSearch)
        Me.Panel1.Controls.Add(Me.txtCustomerCode)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtAgreementNo)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Location = New System.Drawing.Point(5, 112)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(658, 176)
        Me.Panel1.TabIndex = 1
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label35.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.Color.Black
        Me.Label35.Location = New System.Drawing.Point(230, 32)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(57, 22)
        Me.Label35.TabIndex = 775
        Me.Label35.Text = "Flexi ID"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnSubscriptionID
        '
        Me.btnSubscriptionID.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSubscriptionID.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSubscriptionID.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSubscriptionID.Location = New System.Drawing.Point(371, 34)
        Me.btnSubscriptionID.Name = "btnSubscriptionID"
        Me.btnSubscriptionID.Size = New System.Drawing.Size(24, 20)
        Me.btnSubscriptionID.TabIndex = 774
        Me.btnSubscriptionID.Text = "..."
        Me.btnSubscriptionID.UseVisualStyleBackColor = False
        '
        'txtSubscriptionID
        '
        Me.txtSubscriptionID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSubscriptionID.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubscriptionID.ForeColor = System.Drawing.Color.Black
        Me.txtSubscriptionID.Location = New System.Drawing.Point(291, 34)
        Me.txtSubscriptionID.MaxLength = 20
        Me.txtSubscriptionID.Name = "txtSubscriptionID"
        Me.txtSubscriptionID.Size = New System.Drawing.Size(80, 20)
        Me.txtSubscriptionID.TabIndex = 773
        Me.txtSubscriptionID.Tag = ""
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel8.Controls.Add(Me.opteTRSNo)
        Me.Panel8.Controls.Add(Me.opteTrsTicketIssued)
        Me.Panel8.Controls.Add(Me.opteTRSEligible)
        Me.Panel8.Location = New System.Drawing.Point(230, 8)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(327, 24)
        Me.Panel8.TabIndex = 772
        '
        'opteTRSNo
        '
        Me.opteTRSNo.Checked = True
        Me.opteTRSNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.opteTRSNo.ForeColor = System.Drawing.Color.Black
        Me.opteTRSNo.Location = New System.Drawing.Point(167, 0)
        Me.opteTRSNo.Name = "opteTRSNo"
        Me.opteTRSNo.Size = New System.Drawing.Size(165, 20)
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
        'btnTradeIn
        '
        Me.btnTradeIn.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTradeIn.Location = New System.Drawing.Point(549, 60)
        Me.btnTradeIn.Name = "btnTradeIn"
        Me.btnTradeIn.Size = New System.Drawing.Size(102, 25)
        Me.btnTradeIn.TabIndex = 767
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
        Me.GroupBox2.Location = New System.Drawing.Point(321, 53)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(222, 32)
        Me.GroupBox2.TabIndex = 766
        Me.GroupBox2.TabStop = False
        '
        'optBoth
        '
        Me.optBoth.Location = New System.Drawing.Point(164, 10)
        Me.optBoth.Name = "optBoth"
        Me.optBoth.Size = New System.Drawing.Size(51, 15)
        Me.optBoth.TabIndex = 19
        Me.optBoth.Text = "Both"
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
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(8, 60)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(82, 20)
        Me.Label16.TabIndex = 131
        Me.Label16.Text = "Name"
        '
        'txtMemberCode
        '
        Me.txtMemberCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMemberCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMemberCode.ForeColor = System.Drawing.Color.Black
        Me.txtMemberCode.Location = New System.Drawing.Point(232, 35)
        Me.txtMemberCode.MaxLength = 20
        Me.txtMemberCode.Name = "txtMemberCode"
        Me.txtMemberCode.Size = New System.Drawing.Size(192, 20)
        Me.txtMemberCode.TabIndex = 130
        Me.txtMemberCode.Tag = ""
        Me.txtMemberCode.Visible = False
        '
        'lblCustomerType
        '
        Me.lblCustomerType.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustomerType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustomerType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerType.ForeColor = System.Drawing.Color.Red
        Me.lblCustomerType.Location = New System.Drawing.Point(432, 35)
        Me.lblCustomerType.Name = "lblCustomerType"
        Me.lblCustomerType.Size = New System.Drawing.Size(216, 20)
        Me.lblCustomerType.TabIndex = 129
        Me.lblCustomerType.Text = "Customer Type"
        Me.lblCustomerType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblCustomerType.Visible = False
        '
        'btnAgreementNoSearch
        '
        Me.btnAgreementNoSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnAgreementNoSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAgreementNoSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAgreementNoSearch.Location = New System.Drawing.Point(200, 8)
        Me.btnAgreementNoSearch.Name = "btnAgreementNoSearch"
        Me.btnAgreementNoSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnAgreementNoSearch.TabIndex = 0
        Me.btnAgreementNoSearch.Text = "..."
        Me.btnAgreementNoSearch.UseVisualStyleBackColor = False
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(8, 86)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(82, 20)
        Me.Label13.TabIndex = 100
        Me.Label13.Text = "Address"
        '
        'txtTotalAmount
        '
        Me.txtTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalAmount.Enabled = False
        Me.txtTotalAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalAmount.ForeColor = System.Drawing.Color.Black
        Me.txtTotalAmount.Location = New System.Drawing.Point(547, 143)
        Me.txtTotalAmount.MaxLength = 20
        Me.txtTotalAmount.Name = "txtTotalAmount"
        Me.txtTotalAmount.Size = New System.Drawing.Size(101, 20)
        Me.txtTotalAmount.TabIndex = 9
        Me.txtTotalAmount.Tag = ""
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(435, 146)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(96, 15)
        Me.Label9.TabIndex = 99
        Me.Label9.Text = "Total Amount"
        '
        'txtNoofInstallment
        '
        Me.txtNoofInstallment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNoofInstallment.Enabled = False
        Me.txtNoofInstallment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoofInstallment.ForeColor = System.Drawing.Color.Black
        Me.txtNoofInstallment.Location = New System.Drawing.Point(547, 89)
        Me.txtNoofInstallment.MaxLength = 20
        Me.txtNoofInstallment.Name = "txtNoofInstallment"
        Me.txtNoofInstallment.Size = New System.Drawing.Size(101, 20)
        Me.txtNoofInstallment.TabIndex = 7
        Me.txtNoofInstallment.Tag = ""
        Me.txtNoofInstallment.Text = "1"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(435, 93)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(104, 15)
        Me.Label8.TabIndex = 97
        Me.Label8.Text = "No. of Installment"
        '
        'txtInstallmentAmount
        '
        Me.txtInstallmentAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInstallmentAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInstallmentAmount.ForeColor = System.Drawing.Color.Black
        Me.txtInstallmentAmount.Location = New System.Drawing.Point(547, 116)
        Me.txtInstallmentAmount.MaxLength = 20
        Me.txtInstallmentAmount.Name = "txtInstallmentAmount"
        Me.txtInstallmentAmount.Size = New System.Drawing.Size(101, 20)
        Me.txtInstallmentAmount.TabIndex = 8
        Me.txtInstallmentAmount.Tag = ""
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(435, 119)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(104, 15)
        Me.Label6.TabIndex = 95
        Me.Label6.Text = "Installment Amout"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(8, 148)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 20)
        Me.Label1.TabIndex = 71
        Me.Label1.Text = "Remarks"
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.Black
        Me.txtRemarks.Location = New System.Drawing.Point(104, 143)
        Me.txtRemarks.MaxLength = 20
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(320, 20)
        Me.txtRemarks.TabIndex = 6
        Me.txtRemarks.Tag = ""
        '
        'txtAddress3
        '
        Me.txtAddress3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress3.ForeColor = System.Drawing.Color.Black
        Me.txtAddress3.Location = New System.Drawing.Point(288, 116)
        Me.txtAddress3.MaxLength = 20
        Me.txtAddress3.Name = "txtAddress3"
        Me.txtAddress3.Size = New System.Drawing.Size(136, 20)
        Me.txtAddress3.TabIndex = 5
        Me.txtAddress3.Tag = ""
        '
        'txtAddress2
        '
        Me.txtAddress2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress2.ForeColor = System.Drawing.Color.Black
        Me.txtAddress2.Location = New System.Drawing.Point(104, 116)
        Me.txtAddress2.MaxLength = 20
        Me.txtAddress2.Name = "txtAddress2"
        Me.txtAddress2.Size = New System.Drawing.Size(176, 20)
        Me.txtAddress2.TabIndex = 4
        Me.txtAddress2.Tag = ""
        '
        'txtAddress1
        '
        Me.txtAddress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress1.ForeColor = System.Drawing.Color.Black
        Me.txtAddress1.Location = New System.Drawing.Point(104, 89)
        Me.txtAddress1.MaxLength = 20
        Me.txtAddress1.Name = "txtAddress1"
        Me.txtAddress1.Size = New System.Drawing.Size(320, 20)
        Me.txtAddress1.TabIndex = 3
        Me.txtAddress1.Tag = ""
        '
        'txtCustomerName
        '
        Me.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerName.ForeColor = System.Drawing.Color.Black
        Me.txtCustomerName.Location = New System.Drawing.Point(104, 62)
        Me.txtCustomerName.MaxLength = 20
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(192, 20)
        Me.txtCustomerName.TabIndex = 2
        Me.txtCustomerName.Tag = ""
        '
        'btnCustomerCodeSearch
        '
        Me.btnCustomerCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCustomerCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCustomerCodeSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomerCodeSearch.Location = New System.Drawing.Point(200, 35)
        Me.btnCustomerCodeSearch.Name = "btnCustomerCodeSearch"
        Me.btnCustomerCodeSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnCustomerCodeSearch.TabIndex = 2
        Me.btnCustomerCodeSearch.Text = "..."
        Me.btnCustomerCodeSearch.UseVisualStyleBackColor = False
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerCode.ForeColor = System.Drawing.Color.Black
        Me.txtCustomerCode.Location = New System.Drawing.Point(104, 35)
        Me.txtCustomerCode.MaxLength = 20
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.Size = New System.Drawing.Size(96, 20)
        Me.txtCustomerCode.TabIndex = 1
        Me.txtCustomerCode.Tag = ""
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(8, 34)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 20)
        Me.Label5.TabIndex = 65
        Me.Label5.Text = "Customer Code"
        '
        'txtAgreementNo
        '
        Me.txtAgreementNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAgreementNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAgreementNo.ForeColor = System.Drawing.Color.Black
        Me.txtAgreementNo.Location = New System.Drawing.Point(104, 8)
        Me.txtAgreementNo.MaxLength = 20
        Me.txtAgreementNo.Name = "txtAgreementNo"
        Me.txtAgreementNo.Size = New System.Drawing.Size(96, 20)
        Me.txtAgreementNo.TabIndex = 0
        Me.txtAgreementNo.Tag = ""
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(8, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 20)
        Me.Label2.TabIndex = 49
        Me.Label2.Text = "Agreement No"
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.lblInstallmentAmount)
        Me.Panel2.Controls.Add(Me.Label15)
        Me.Panel2.Controls.Add(Me.lblDepositAmount)
        Me.Panel2.Controls.Add(Me.lblBalanceAmount)
        Me.Panel2.Controls.Add(Me.lblSalesAmount)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Location = New System.Drawing.Point(5, 392)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(658, 66)
        Me.Panel2.TabIndex = 2
        '
        'lblInstallmentAmount
        '
        Me.lblInstallmentAmount.BackColor = System.Drawing.Color.LightCyan
        Me.lblInstallmentAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblInstallmentAmount.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInstallmentAmount.ForeColor = System.Drawing.Color.Black
        Me.lblInstallmentAmount.Location = New System.Drawing.Point(496, 32)
        Me.lblInstallmentAmount.Name = "lblInstallmentAmount"
        Me.lblInstallmentAmount.Size = New System.Drawing.Size(160, 32)
        Me.lblInstallmentAmount.TabIndex = 79
        Me.lblInstallmentAmount.Text = "0.00"
        Me.lblInstallmentAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Pink
        Me.Label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(496, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(160, 32)
        Me.Label15.TabIndex = 78
        Me.Label15.Text = "Installment Amount"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDepositAmount
        '
        Me.lblDepositAmount.BackColor = System.Drawing.Color.LightCyan
        Me.lblDepositAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDepositAmount.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepositAmount.ForeColor = System.Drawing.Color.Black
        Me.lblDepositAmount.Location = New System.Drawing.Point(168, 32)
        Me.lblDepositAmount.Name = "lblDepositAmount"
        Me.lblDepositAmount.Size = New System.Drawing.Size(168, 32)
        Me.lblDepositAmount.TabIndex = 77
        Me.lblDepositAmount.Text = "0.00"
        Me.lblDepositAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBalanceAmount
        '
        Me.lblBalanceAmount.BackColor = System.Drawing.Color.LightCyan
        Me.lblBalanceAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBalanceAmount.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalanceAmount.ForeColor = System.Drawing.Color.Black
        Me.lblBalanceAmount.Location = New System.Drawing.Point(336, 32)
        Me.lblBalanceAmount.Name = "lblBalanceAmount"
        Me.lblBalanceAmount.Size = New System.Drawing.Size(160, 32)
        Me.lblBalanceAmount.TabIndex = 76
        Me.lblBalanceAmount.Text = "0.00"
        Me.lblBalanceAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSalesAmount
        '
        Me.lblSalesAmount.BackColor = System.Drawing.Color.LightCyan
        Me.lblSalesAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSalesAmount.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesAmount.ForeColor = System.Drawing.Color.Black
        Me.lblSalesAmount.Location = New System.Drawing.Point(0, 32)
        Me.lblSalesAmount.Name = "lblSalesAmount"
        Me.lblSalesAmount.Size = New System.Drawing.Size(168, 32)
        Me.lblSalesAmount.TabIndex = 75
        Me.lblSalesAmount.Text = "0.00"
        Me.lblSalesAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Pink
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label12.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(168, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(168, 32)
        Me.Label12.TabIndex = 74
        Me.Label12.Text = "Deposit Amount"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Pink
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(336, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(160, 32)
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
        Me.Label10.Location = New System.Drawing.Point(0, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(168, 32)
        Me.Label10.TabIndex = 72
        Me.Label10.Text = "Sales Amount"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnSAve
        '
        Me.btnSAve.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSAve.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSAve.Location = New System.Drawing.Point(463, 484)
        Me.btnSAve.Name = "btnSAve"
        Me.btnSAve.Size = New System.Drawing.Size(100, 40)
        Me.btnSAve.TabIndex = 10
        Me.btnSAve.Text = "Save (F7)"
        Me.btnSAve.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(566, 484)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 13
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnBillClear
        '
        Me.btnBillClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnBillClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBillClear.Location = New System.Drawing.Point(359, 484)
        Me.btnBillClear.Name = "btnBillClear"
        Me.btnBillClear.Size = New System.Drawing.Size(100, 40)
        Me.btnBillClear.TabIndex = 12
        Me.btnBillClear.Text = "Clear (F6)"
        Me.btnBillClear.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(256, 484)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(100, 40)
        Me.btnCancel.TabIndex = 14
        Me.btnCancel.Text = "Cancel (F10)"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnRePrint
        '
        Me.btnRePrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnRePrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRePrint.Location = New System.Drawing.Point(152, 484)
        Me.btnRePrint.Name = "btnRePrint"
        Me.btnRePrint.Size = New System.Drawing.Size(100, 40)
        Me.btnRePrint.TabIndex = 15
        Me.btnRePrint.Text = "RePrint (F9)"
        Me.btnRePrint.UseVisualStyleBackColor = False
        '
        'pnlCancel
        '
        Me.pnlCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.pnlCancel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCancel.Controls.Add(Me.btnBillCancel)
        Me.pnlCancel.Controls.Add(Me.btnBillCancelOK)
        Me.pnlCancel.Controls.Add(Me.txtBillCancelBillNo)
        Me.pnlCancel.Controls.Add(Me.Label7)
        Me.pnlCancel.Controls.Add(Me.Label14)
        Me.pnlCancel.Location = New System.Drawing.Point(210, 304)
        Me.pnlCancel.Name = "pnlCancel"
        Me.pnlCancel.Size = New System.Drawing.Size(278, 88)
        Me.pnlCancel.TabIndex = 73
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
        'txtBillCancelBillNo
        '
        Me.txtBillCancelBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillCancelBillNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillCancelBillNo.ForeColor = System.Drawing.Color.Black
        Me.txtBillCancelBillNo.Location = New System.Drawing.Point(128, 33)
        Me.txtBillCancelBillNo.MaxLength = 20
        Me.txtBillCancelBillNo.Name = "txtBillCancelBillNo"
        Me.txtBillCancelBillNo.Size = New System.Drawing.Size(136, 20)
        Me.txtBillCancelBillNo.TabIndex = 73
        Me.txtBillCancelBillNo.Tag = ""
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(16, 33)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(104, 15)
        Me.Label7.TabIndex = 72
        Me.Label7.Text = "Installment Bill No"
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(8, 8)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(128, 15)
        Me.Label14.TabIndex = 72
        Me.Label14.Text = "Installment Cancel"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PnlReprint
        '
        Me.PnlReprint.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.PnlReprint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PnlReprint.Controls.Add(Me.btnReprintCancel)
        Me.PnlReprint.Controls.Add(Me.btnReprintOK)
        Me.PnlReprint.Controls.Add(Me.txtRePrintBillNo)
        Me.PnlReprint.Controls.Add(Me.Label4)
        Me.PnlReprint.Controls.Add(Me.Label3)
        Me.PnlReprint.Location = New System.Drawing.Point(210, 300)
        Me.PnlReprint.Name = "PnlReprint"
        Me.PnlReprint.Size = New System.Drawing.Size(248, 88)
        Me.PnlReprint.TabIndex = 74
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
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(8, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 15)
        Me.Label3.TabIndex = 72
        Me.Label3.Text = "RePrint"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlMember
        '
        Me.pnlMember.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.pnlMember.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMember.Controls.Add(Me.btnMemberCodeSearch)
        Me.pnlMember.Controls.Add(Me.btnNew)
        Me.pnlMember.Controls.Add(Me.btnOK)
        Me.pnlMember.Controls.Add(Me.btnCancelFme)
        Me.pnlMember.Controls.Add(Me.txtMemberCodeView)
        Me.pnlMember.Controls.Add(Me.Label79)
        Me.pnlMember.Location = New System.Drawing.Point(178, 353)
        Me.pnlMember.Name = "pnlMember"
        Me.pnlMember.Size = New System.Drawing.Size(312, 104)
        Me.pnlMember.TabIndex = 151
        Me.pnlMember.Visible = False
        '
        'btnMemberCodeSearch
        '
        Me.btnMemberCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnMemberCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMemberCodeSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMemberCodeSearch.Location = New System.Drawing.Point(256, 24)
        Me.btnMemberCodeSearch.Name = "btnMemberCodeSearch"
        Me.btnMemberCodeSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnMemberCodeSearch.TabIndex = 120
        Me.btnMemberCodeSearch.Text = "..."
        Me.btnMemberCodeSearch.UseVisualStyleBackColor = False
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(72, 56)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(64, 23)
        Me.btnNew.TabIndex = 5
        Me.btnNew.Text = "New"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnOK
        '
        Me.btnOK.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnOK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.Image = CType(resources.GetObject("btnOK.Image"), System.Drawing.Image)
        Me.btnOK.Location = New System.Drawing.Point(144, 56)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(64, 23)
        Me.btnOK.TabIndex = 3
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'btnCancelFme
        '
        Me.btnCancelFme.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCancelFme.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelFme.Image = CType(resources.GetObject("btnCancelFme.Image"), System.Drawing.Image)
        Me.btnCancelFme.Location = New System.Drawing.Point(216, 56)
        Me.btnCancelFme.Name = "btnCancelFme"
        Me.btnCancelFme.Size = New System.Drawing.Size(64, 23)
        Me.btnCancelFme.TabIndex = 4
        Me.btnCancelFme.Text = "Cancel"
        Me.btnCancelFme.UseVisualStyleBackColor = False
        '
        'txtMemberCodeView
        '
        Me.txtMemberCodeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMemberCodeView.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMemberCodeView.Location = New System.Drawing.Point(120, 24)
        Me.txtMemberCodeView.Name = "txtMemberCodeView"
        Me.txtMemberCodeView.Size = New System.Drawing.Size(136, 20)
        Me.txtMemberCodeView.TabIndex = 0
        '
        'Label79
        '
        Me.Label79.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label79.Location = New System.Drawing.Point(8, 32)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(128, 24)
        Me.Label79.TabIndex = 0
        Me.Label79.Text = "MemberCode"
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(9, 6)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 100)
        Me.pic.TabIndex = 152
        Me.pic.TabStop = False
        '
        'grdInstallment
        '
        Me.grdInstallment.AllowUserToAddRows = False
        Me.grdInstallment.AllowUserToDeleteRows = False
        Me.grdInstallment.AllowUserToResizeColumns = False
        Me.grdInstallment.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdInstallment.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdInstallment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdInstallment.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cInstallmentNo, Me.cInstallmentDate, Me.cNoofInstallment, Me.cInstallmentAmount, Me.cAmountPaid})
        Me.grdInstallment.Location = New System.Drawing.Point(5, 289)
        Me.grdInstallment.Name = "grdInstallment"
        Me.grdInstallment.RowHeadersVisible = False
        Me.grdInstallment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdInstallment.Size = New System.Drawing.Size(657, 101)
        Me.grdInstallment.TabIndex = 153
        '
        'cInstallmentNo
        '
        Me.cInstallmentNo.HeaderText = "Installment No"
        Me.cInstallmentNo.Name = "cInstallmentNo"
        Me.cInstallmentNo.Width = 120
        '
        'cInstallmentDate
        '
        Me.cInstallmentDate.HeaderText = "Installment Date"
        Me.cInstallmentDate.Name = "cInstallmentDate"
        Me.cInstallmentDate.Width = 130
        '
        'cNoofInstallment
        '
        Me.cNoofInstallment.HeaderText = "No Of Installment"
        Me.cNoofInstallment.Name = "cNoofInstallment"
        Me.cNoofInstallment.Width = 125
        '
        'cInstallmentAmount
        '
        Me.cInstallmentAmount.HeaderText = "Installment Amount"
        Me.cInstallmentAmount.Name = "cInstallmentAmount"
        Me.cInstallmentAmount.Width = 135
        '
        'cAmountPaid
        '
        Me.cAmountPaid.HeaderText = "Amount Paid"
        Me.cAmountPaid.Name = "cAmountPaid"
        Me.cAmountPaid.Width = 140
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
        Me.grdSales.Location = New System.Drawing.Point(6, 316)
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
        Me.grdSales.Size = New System.Drawing.Size(658, 181)
        Me.grdSales.TabIndex = 555
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
        'frmInstallmentPayment
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(669, 536)
        Me.Controls.Add(Me.grdSales)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.pnlMember)
        Me.Controls.Add(Me.pnlCancel)
        Me.Controls.Add(Me.PnlReprint)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnRePrint)
        Me.Controls.Add(Me.btnSAve)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnBillClear)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.grdInstallment)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmInstallmentPayment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Installment Payment"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlCancel.ResumeLayout(False)
        Me.pnlCancel.PerformLayout()
        Me.PnlReprint.ResumeLayout(False)
        Me.PnlReprint.PerformLayout()
        Me.pnlMember.ResumeLayout(False)
        Me.pnlMember.PerformLayout()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdInstallment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSales, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region
    Dim mnPendingIns%
    Dim msInsAgreementNo$
    Dim mdInstallmentAmt#
    Dim mdInstallmentAmtPaid#
    Dim msInvoiceNo$
    Dim msInstallmentNo$
    Dim bReprint As Boolean
    Dim dNoofInstallment, dInstallmentAmount, dAmountPaid As Double
    Dim sInvoiceno As String
    Dim sInsInvoiceNo As String
    Dim CompName$, Address1$, Address2$, Country$, PhoneNo$, FaxNo$
    Dim sPrinterType$, dCustomerDisplayPort#, dCashDrawerInterface#, dCashDrawerPort#
    Dim bCustomerDisplay As Boolean, bPrinter As Boolean
    Public bInsMsg As Boolean
    Public dbalAmt As Double
    Dim bVoiceEnabled As Boolean
    Const GRDROWHT = 17.708

    Dim objMember As New clsMember
    Dim objTer As New clsTerminal
    Dim objComp As New clsCompany
    Dim objIns As New ClsInstallment
    Dim objSales As New ClsSales
    Dim objCust As New clsCustomer
    Dim tradeInRefNo As String
    Const InstallmentNo = 1
    Const InstallmentDate = 2
    Const NoOfInstallment = 3
    Const InstallmentAmount = 4
    Const AmountPaid = 5
    Dim dTradeInGST As Double
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
    Dim objClassRate As New ClsClass

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmInstallmentPayment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            bInsMsg = False
            'subGridSetting(grdInstallmentPayment)
            bInstallmentFlag = True
            grdInstallment.RowCount = 0
            subLoadTerminalParameter()
            subgetcompany()
            'SetPosPinters("BILL")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub subLoadTerminalParameter()
        Dim dsTer As New DataSet
        Try
            dsTer = objTer.GetTerminal(Trim(gTerminalCode))
            If dsTer.Tables(0).Rows.Count Then
                sPrinterType = Trim(dsTer.Tables(0).Rows(0)("PrinterType") & "")
                dCustomerDisplayPort = Val(dsTer.Tables(0).Rows(0)("cusdisplayportno") & "")

                If Val(dsTer.Tables(0).Rows(0)("PrinterAttached") & "") = 1 Then
                    bPrinter = True
                Else
                    bPrinter = False
                End If
                If Val(dsTer.Tables(0).Rows(0)("VoiceEnabled") & "") = 1 Then
                    bVoiceEnabled = True
                Else
                    bVoiceEnabled = False
                End If
            Else
                MsgBox("Your Computer is not Configured")

                Exit Sub
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subgetcompany()
        Dim dsComp As New DataSet
        Try
            dsComp = objComp.GetCompanyList()
            If dsComp.Tables(0).Rows.Count Then
                CompName = Trim(dsComp.Tables(0).Rows(0)("Name") & "")
                Address1 = Trim(dsComp.Tables(0).Rows(0)("Address1") & "")
                Address2 = Trim(dsComp.Tables(0).Rows(0)("Address2") & "")
                Country = Trim(dsComp.Tables(0).Rows(0)("Country") & "")
                PhoneNo = Trim(dsComp.Tables(0).Rows(0)("Phone") & "")
                FaxNo = Trim(dsComp.Tables(0).Rows(0)("Fax") & "")
            Else
                MsgBox("Locked")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmInstallmentPayment_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.VisibleChanged
        Try
            bInstallmentFlag = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmInstallmentPayment_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        bInstallmentFlag = False
    End Sub

    Private Sub btnAgreementNoSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgreementNoSearch.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim frmHelpAgreement As New frmHelpSearch(0, 0, "tblInstallmentEntryHeader", "L", 4, "InsAgreementNo", "DepositAmt+AmtPaid<>TotalCost", "InsAgreementNo", "InsAgreementNo", 100, "LocationCode", "LocationCode", 60, "CustomerCode", "CustomerCode", 80, "Name", "Name", 80)
            frmHelpAgreement.ShowDialog()
            txtAgreementNo.Text = frmHelpAgreement.Var1.Trim
            subGetAgreementDetail()
            txtNoofInstallment_Leave(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub subGetAgreementDetail()
        Dim dsInsHdr As New DataSet
        Try
            If txtAgreementNo.Text.Trim = "" Then Exit Sub
            dsInsHdr = objIns.GetInstallmentHdr(Trim$(txtAgreementNo.Text))
            If dsInsHdr.Tables(0).Rows.Count > 0 Then
                If Val(dsInsHdr.Tables(0).Rows(0)("AmtPaid") & "") >= Val(dsInsHdr.Tables(0).Rows(0)("TotalCost") & "") Then
                    MessageBox.Show("Installment Amount was paid!")
                Else
                    txtCustomerCode.Text = Trim(dsInsHdr.Tables(0).Rows(0)("CustomerCode") & "")
                    txtMemberCode.Text = Trim(dsInsHdr.Tables(0).Rows(0)("MemberCode") & "")
                    txtCustomerName.Text = Trim(dsInsHdr.Tables(0).Rows(0)("Name") & "")
                    'lblCustomerType.Text = Trim(dsInsHdr.Tables(0).Rows(0)("CustomerType") & "")
                    txtAddress1.Text = Trim(dsInsHdr.Tables(0).Rows(0)("Address1") & "")
                    txtAddress2.Text = Trim(dsInsHdr.Tables(0).Rows(0)("Address2") & "")
                    txtAddress3.Text = Trim(dsInsHdr.Tables(0).Rows(0)("Address3") & "")
                    msInsAgreementNo = Trim$(dsInsHdr.Tables(0).Rows(0)("InsAgreementNo") & "")
                    txtAgreementNo.Text = msInsAgreementNo
                    mnPendingIns = Val(dsInsHdr.Tables(0).Rows(0)("InsTotal") & "") - Val(dsInsHdr.Tables(0).Rows(0)("InsPaid") & "")
                    mdInstallmentAmt = Val(dsInsHdr.Tables(0).Rows(0)("InstallmentAmt") & "")
                    txtInstallmentAmount.Text = Format(mdInstallmentAmt, "0.00")
                    lblSalesAmount.Text = Format(Val(dsInsHdr.Tables(0).Rows(0)("TotalCost")), "0.00")
                    lblDepositAmount.Text = Format(Val(dsInsHdr.Tables(0).Rows(0)("DepositAmt")), "0.00")
                    mdInstallmentAmtPaid = Val(dsInsHdr.Tables(0).Rows(0)("AmtPaid") & "")
                    lblInstallmentAmount.Text = Format(mdInstallmentAmtPaid, "0.00")
                    lblBalanceAmount.Text = Format(Val(lblSalesAmount.Text) - Val(lblDepositAmount.Text) - Val(lblInstallmentAmount.Text), "0.00")
                    dbalAmt = Val(lblBalanceAmount.Text)
                    'if the balance amt is less than the installment amt then set the installment amt as the balance amt.
                    If (Val(mdInstallmentAmt) > Val(lblBalanceAmount.Text)) Then
                        mdInstallmentAmt = Val(lblBalanceAmount.Text)
                        txtInstallmentAmount.Text = Format(mdInstallmentAmt, "0.00")
                    ElseIf (Val(mdInstallmentAmt) < Val(lblBalanceAmount.Text)) And mnPendingIns = 1 Then
                        bInsMsg = True
                        mdInstallmentAmt = Val(lblBalanceAmount.Text)
                        txtInstallmentAmount.Text = Format(mdInstallmentAmt, "0.00")
                    End If
                    'subGetCustomer()
                    subShowInsDetail()
                    subShowBalance()
                    '''txtInstallmentAmount.ReadOnly = True
                    '''txtInstallmentAmount.ReadOnly = True
                    If Trim(sCompanyShortCode) = "HN" Then
                        txtNoofInstallment.Text = 6
                        txtInstallmentAmount.Text = Format(Val(lblBalanceAmount.Text) / 6, "0.00")
                        txtTotalAmount.Text = Val(txtInstallmentAmount.Text)
                        txtInstallmentAmount.Focus()
                    Else
                        'txtNoofInstallment.Focus()
                        txtInstallmentAmount.Focus()
                    End If

                End If
            Else
                MsgBox("This Installment Agreement No Not Found... Please Give Correct Installment Agreement No")
                txtAgreementNo.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subShowInsDetail()
        Dim dsSalesHdr As New DataSet
        Try
            dsSalesHdr = objSales.GetSalesHeaderByInsAgreementNo(msInsAgreementNo)
            If dsSalesHdr.Tables(0).Rows.Count > 0 Then
                If Trim(txtCustomerName.Text) = "CASH CUSTOMER" Then
                    txtCustomerCode.Text = Trim$(dsSalesHdr.Tables(0).Rows(0)("CustomerCode") & "")
                    subGetCustomer()
                End If
            End If
            'fill the data for the entered agreement no.
            Call subGetInstallmentDetails()
            Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subShowBalance()
        Dim dsInsHdr As New DataSet
        Try
            If txtAgreementNo.Text.Trim = "" Then Exit Sub
            dsInsHdr = objIns.GetInstallmentBalance(Trim$(txtAgreementNo.Text))
            If dsInsHdr.Tables(0).Rows.Count > 0 Then
                lblBalanceAmount.Text = Format(Val(lblSalesAmount.Text) - Val(dsInsHdr.Tables(0).Rows(0)("AmountPaid") & "") - Val(lblDepositAmount.Text), "0.00")
            End If
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

    Private Sub subGetInstallmentDetails()
        Dim dsIns As New DataSet
        Dim iCnt As Integer
        Try
            grdInstallment.RowCount = 0
            dsIns = objIns.GetInstallmentDetail(msInsAgreementNo)
            If dsIns.Tables(0).Rows.Count > 0 Then
                For iCnt = 0 To dsIns.Tables(0).Rows.Count - 1
                    grdInstallment.Rows.Add()
                    Call fnSetdgvText(grdInstallment, grdInstallment.RowCount - 1, 0, Trim(dsIns.Tables(0).Rows(iCnt)("InstallmentNo") & ""))
                    Call fnSetdgvText(grdInstallment, grdInstallment.RowCount - 1, 1, Format(CDate(dsIns.Tables(0).Rows(iCnt)("CreateDate") & ""), "dd-MM-yyyy"))
                    Call fnSetdgvText(grdInstallment, grdInstallment.RowCount - 1, 2, Val(Trim(dsIns.Tables(0).Rows(iCnt)("InsPaid") & "")))
                    Call fnSetdgvText(grdInstallment, grdInstallment.RowCount - 1, 3, Format(Val(dsIns.Tables(0).Rows(iCnt)("InstallmentAmt") & "")))
                    Call fnSetdgvText(grdInstallment, grdInstallment.RowCount - 1, 4, Format(Val(dsIns.Tables(0).Rows(iCnt)("AmtPaid") & ""), "###0.00"))
                Next iCnt
            End If
            'grdInstallmentPayment.Row = grdInstallmentPayment.MaxRows + 1
            'grdInstallmentPayment.Col = 1
            'grdInstallmentPayment.Action = 0
            subTotalFromGrid()
            subTotal()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Function subTotalFromGrid()
        Dim icnt
        Dim sinventorycode As String
        dNoofInstallment = 0
        dInstallmentAmount = 0
        dAmountPaid = 0
        For icnt = 0 To grdInstallment.RowCount - 1
            sinventorycode = Trim(fnGetdgvText(grdInstallment, icnt, 0))
            If sinventorycode <> "TOTAL:" Then
                dNoofInstallment = Format(Val(dNoofInstallment) + Val(fnGetdgvText(grdInstallment, icnt, 2)), "0.00")
                dInstallmentAmount = Format(Val(dInstallmentAmount) + Val(fnGetdgvText(grdInstallment, icnt, 3)), "0.00")
                dAmountPaid = Format(Val(dAmountPaid) + Val(fnGetdgvText(grdInstallment, icnt, 4)), "0.00")
            End If
        Next
    End Function
    Private Sub subTotal()
        Try
            If grdInstallment.RowCount = 0 Then
                Exit Sub
            End If
            'If Not bEdited Then
            'grdPurchase.MaxRows = grdPurchase.MaxRows + 1
            grdInstallment.Rows.Add()
            'End If



            Call fnSetdgvText(grdInstallment, grdInstallment.RowCount - 1, 0, "TOTAL:")
            Call fnSetdgvText(grdInstallment, grdInstallment.RowCount - 1, 2, Format(dNoofInstallment, "0.00"))
            Call fnSetdgvText(grdInstallment, grdInstallment.RowCount - 1, 3, Format(dInstallmentAmount, "0.00"))
            Call fnSetdgvText(grdInstallment, grdInstallment.RowCount - 1, 4, Format(dAmountPaid, "0.00"))



            Me.grdInstallment.Rows(grdInstallment.RowCount - 1).Cells(0).Style.ForeColor = Color.Red
            Me.grdInstallment.Rows(grdInstallment.RowCount - 1).Cells(0).Style.Font = New Font(grdInstallment.Font.FontFamily, 9, FontStyle.Bold)

            Me.grdInstallment.Rows(grdInstallment.RowCount - 1).Cells(2).Style.ForeColor = Color.Red
            Me.grdInstallment.Rows(grdInstallment.RowCount - 1).Cells(2).Style.Font = New Font(grdInstallment.Font.FontFamily, 9, FontStyle.Bold)

            Me.grdInstallment.Rows(grdInstallment.RowCount - 1).Cells(3).Style.ForeColor = Color.Red
            Me.grdInstallment.Rows(grdInstallment.RowCount - 1).Cells(3).Style.Font = New Font(grdInstallment.Font.FontFamily, 9, FontStyle.Bold)

            Me.grdInstallment.Rows(grdInstallment.RowCount - 1).Cells(4).Style.ForeColor = Color.Red
            Me.grdInstallment.Rows(grdInstallment.RowCount - 1).Cells(4).Style.Font = New Font(grdInstallment.Font.FontFamily, 9, FontStyle.Bold)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        Finally

        End Try
    End Sub

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

    Private Sub txtAgreementNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAgreementNo.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then btnAgreementNoSearch_Click(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtAgreementNo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAgreementNo.LostFocus
        Try
            subGetAgreementDetail()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtInstallmentAmount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInstallmentAmount.TextChanged
        Try
            If Val(txtNoofInstallment.Text) > 0 And Val(txtInstallmentAmount.Text) > 0 Then
                If Trim(sCompanyShortCode) = "HN" Then
                    txtTotalAmount.Text = Format$(Val(txtInstallmentAmount.Text), "##.00")
                Else
                    txtTotalAmount.Text = Format$(Val(txtNoofInstallment.Text) * Val(txtInstallmentAmount.Text), "##.00")
                End If
                mdInstallmentAmt = Val(txtInstallmentAmount.Text)
                lblInstallmentAmount.Text = Format(mdInstallmentAmtPaid + Val(txtTotalAmount.Text), "0.00")
                lblBalanceAmount.Text = Format(Val(lblSalesAmount.Text) - Val(lblDepositAmount.Text) - Val(lblInstallmentAmount.Text), "0.00")
            ElseIf Val(txtNoofInstallment.Text) > 0 And Val(txtInstallmentAmount.Text) = 0 Then
                txtTotalAmount.Text = 0
                mdInstallmentAmt = Val(txtInstallmentAmount.Text)
                lblInstallmentAmount.Text = Format(mdInstallmentAmtPaid + Val(txtTotalAmount.Text), "0.00")
                lblBalanceAmount.Text = Format(Val(lblSalesAmount.Text) - Val(lblDepositAmount.Text) - Val(lblInstallmentAmount.Text), "0.00")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtNoofInstallment_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNoofInstallment.Leave
        Try
            If Val(txtNoofInstallment.Text) > 0 And Val(txtInstallmentAmount.Text) > 0 Then
                If Trim(sCompanyShortCode) = "HN" Then
                    txtTotalAmount.Text = Format$(Val(txtInstallmentAmount.Text), "##.00")
                Else
                    txtTotalAmount.Text = Format$(Val(txtNoofInstallment.Text) * Val(txtInstallmentAmount.Text), "##.00")
                End If
                mdInstallmentAmt = Val(txtInstallmentAmount.Text)
                lblInstallmentAmount.Text = Format(mdInstallmentAmtPaid + Val(txtTotalAmount.Text), "0.00")
                lblBalanceAmount.Text = Format(Val(lblSalesAmount.Text) - Val(lblDepositAmount.Text) - Val(lblInstallmentAmount.Text), "0.00")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnRePrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRePrint.Click
        Try
            PnlReprint.Visible = True
            txtRePrintBillNo.Clear()
            txtRePrintBillNo.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            If Trim$(txtAgreementNo.Text) = "" Then
                MessageBox.Show("Please enter the Installment Agreement No!")
                Exit Sub
            End If
            pnlCancel.Visible = True
            txtBillCancelBillNo.Clear()
            txtBillCancelBillNo.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnBillClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBillClear.Click
        Try
            txtAgreementNo.Clear()
            txtCustomerCode.Clear()
            txtMemberCode.Text = ""
            lblCustomerType.Text = "Type"
            txtCustomerName.Clear()
            lblCustomerType.Text = ""
            txtAddress1.Clear()
            txtAddress2.Clear()
            txtAddress3.Clear()
            txtRemarks.Clear()
            txtNoofInstallment.Clear()
            txtInstallmentAmount.Clear()
            txtTotalAmount.Clear()
            grdInstallment.RowCount = 0
            grdSales.RowCount = 0
            txtRePrintBillNo.Clear()
            txtBillCancelBillNo.Clear()
            lblSalesAmount.Text = ""
            lblBalanceAmount.Text = ""
            lblDepositAmount.Text = ""
            lblSalesAmount.Text = ""
            lblInstallmentAmount.Text = ""
            dTradeInGST = 0
            dTradeInSales = 0
            sInvoiceno = ""
            sInsInvoiceNo = ""
            btnTradeIn.Enabled = True
            optCash.Enabled = True
            opteTRSNo.Checked = True
            txtTotalAmount.Enabled = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnSAve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSAve.Click
        Dim frmPM As New frmPayingmode
        Try
            If bInsMsg = True Then
                If Val(txtInstallmentAmount.Text) < Val(dbalAmt) Then
                    MsgBox("Please give full amount..!")
                    lblBalanceAmount.Text = Format(dbalAmt, "0.00")
                    txtInstallmentAmount.Text = Format(dbalAmt, "0.00")
                    insFocus()
                    Exit Sub
                End If
            End If

            If Trim$(txtAgreementNo.Text) = "" Then
                MsgBox("Please enter the Installment Agreement No!")
                Exit Sub
            End If

            If fncCheckValidity() Then
                objIns.OpenConnection()
                objIns.BeginTransaction()
                objSales.subOpenSalesConnection()
                objSales.subBeginSalesTransaction()
                dPaidAmount = 0
                ReturnAmount = 0

                nTotalNet = Val(txtTotalAmount.Text) - Val(dTradeInSales)
                'get the next Installment Number.
                If Not fncGetInstallmentNo() Then Exit Sub
                frmPM.SalesType = ""
                frmPM.sCustomerType = Trim(lblCustomerType.Text)
                frmPM.sCustomerCode = Trim(txtCustomerCode.Text)
                frmPM.ShowDialog()
                If dPaidAmount > 0 Then
                    If Not fncPaymodeDetailSave(msInstallmentNo) Then
                        MessageBox.Show("Transaction not saved")
                        objIns.RollBackTransaction()
                        'save the Installment Details.
                    ElseIf Not fncSaveInstallmentDet() Then
                        MessageBox.Show("Transaction not saved")
                        objIns.RollBackTransaction()
                    ElseIf Not subChecktheAmount() Then
                        MessageBox.Show("Transaction not saved")
                        objIns.RollBackTransaction()
                    ElseIf Not fncSaveTradeInSales(msInstallmentNo) Then
                        MessageBox.Show("Error in save Detail")
                        objSales.subRollBackSalesTransaction()
                    Else

                        objIns.CommitTransaction()
                        objSales.subCommitSalesTransaction()
                        ' Call subPrintBill(Trim(msInstallmentNo))
                        If Trim(sInvoiceno) <> "" Then
                            Call subPrintBill(Trim(sInvoiceno))
                        End If
                        Call subPrintInstallment(Trim(msInstallmentNo))
                        If Trim(sType) = "TRADEIN" And grdSales.RowCount > 0 Then
                            Call subGetBarInventory(Trim(msInstallmentNo))
                        End If
                    End If

                End If

                btnBillClear_Click(sender, e)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            objIns.CloseConnection()
            objSales.subCloseSalesConnection()
            frmPM = Nothing
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
    Public Function subChecktheAmount() As Boolean
        Dim dsInsHdr As New DataSet
        Dim dTotalAmountPaid As Double
        subChecktheAmount = False

        Try
            If txtAgreementNo.Text.Trim = "" Then Exit Function
            dsInsHdr = objIns.GetInstallmentBalance(Trim$(txtAgreementNo.Text))
            If dsInsHdr.Tables(0).Rows.Count > 0 Then
                dTotalAmountPaid = Format(Val(dsInsHdr.Tables(0).Rows(0)("AmountPaid") & "") + Val(lblDepositAmount.Text), "0.00")
                If Val(dTotalAmountPaid) = Val(lblSalesAmount.Text) Then
                    SubAddHeaderIntoSales()
                    subAddDetailsIntoSales()
                    subAddPaymodedetails()
                End If
            End If
            subChecktheAmount = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function
    Public Function subAddPaymodedetails()
        Dim objPM As New clsPayMode
        Try
            Dim bPayModeFlag As Boolean
            Dim sDate As String
            Dim i As Long
            bPayModeFlag = False
            sDate = Format(Now, "dd/MM/yyyy")
            If bPayModeFlag = False Then
                iRet = objPM.PayModeDetailSave(sInvoiceno, sDate, "INSTALLMENT", _
                                            Format(Val(lblSalesAmount.Text), "0.00"), 0, _
                                            "MANUAL", "INSTALLMENT")
                If Not iRet Then
                    MessageBox.Show("Error in InstallmentSave")
                    subAddPaymodedetails = False
                    Exit Function
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function
    Public Function subAddDetailsIntoSales()
        Dim InvoiceNo, InventoryCode, Description, Locationcode, InsAgreementNo As String
        Dim Goldloss, Cost, PCS, SMC, GST, TotalValue, nDetailDiscount, dWeight As Double
        Dim ds1 As New DataSet
        Dim dm As New DataManager
        Dim sSql As String
        Dim iret As Boolean
        Dim icnt As Long
        sSql = "Select * from tblInstallmentEntryDetail where InsAgreementNo=" & fncFormatSQLText(txtAgreementNo.Text)
        ds1 = dm.getDataSetWithSqlAdapter(sSql, "tblInstallmentEntryDetail", "tblInstallmentEntryDetail")
        For icnt = 0 To ds1.Tables("tblInstallmentEntryDetail").Rows.Count - 1
            InventoryCode = Trim(ds1.Tables("tblInstallmentEntryDetail").Rows(icnt)("InventoryCode"))
            Description = Trim(ds1.Tables("tblInstallmentEntryDetail").Rows(icnt)("Description"))
            Locationcode = Trim(ds1.Tables("tblInstallmentEntryDetail").Rows(icnt)("Locationcode"))
            InsAgreementNo = Trim(ds1.Tables("tblInstallmentEntryDetail").Rows(icnt)("InsAgreementNo"))
            dWeight = Val(Trim(ds1.Tables("tblInstallmentEntryDetail").Rows(icnt)("Weight")))
            Goldloss = Val(Trim(ds1.Tables("tblInstallmentEntryDetail").Rows(icnt)("Goldloss")))
            Cost = Val(Trim(ds1.Tables("tblInstallmentEntryDetail").Rows(icnt)("Cost")))
            PCS = Val(Trim(ds1.Tables("tblInstallmentEntryDetail").Rows(icnt)("PCS")))
            SMC = Val(Trim(ds1.Tables("tblInstallmentEntryDetail").Rows(icnt)("SMC")))
            GST = Val(Trim(ds1.Tables("tblInstallmentEntryDetail").Rows(icnt)("GST")))
            TotalValue = Val(Trim(ds1.Tables("tblInstallmentEntryDetail").Rows(icnt)("TotalValue")))
            nDetailDiscount = Val(Trim(ds1.Tables("tblInstallmentEntryDetail").Rows(icnt)("DetailDiscount")))

            iret = objIns.SalesDetailSave(sInvoiceno, InventoryCode, Description, Trim(gthisLocation), dWeight, 0, Goldloss, Cost, SMC, PCS, nDetailDiscount, 0, GST, TotalValue, 0, TotalValue, TotalValue, 0, "", 0, icnt, Val(dTradeInGST), "INSTALLMENT")

            If Not iret Then
                MessageBox.Show("Error in InstallmentSave")
                subAddDetailsIntoSales = False
                Exit Function
            End If

        Next

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
        '    sInvDate = CStr(Format(CDate(ServerDate), "yyyy") & "/" & Format(CDate(ServerDate), "MM") & "/" & Format(CDate(ServerDate), "dd"))
        sInvDate = Format(Now, "dd/MM/yyyy")
        dsInvoiceno = objSales.GetUpdateInvoice()
        If dsInvoiceno.Tables(0).Rows.Count > 0 Then
            sInvoiceno = Trim(dsInvoiceno.Tables(0).Rows(0)("NextInvoice") & "")
            sInsInvoiceNo = Trim(dsInvoiceno.Tables(0).Rows(0)("NextInvoice") & "")
        End If
        sSql = "select a.customerCode as CustomerCode,a.name as Name,a.address1 as Address1,a.address2 as Address2, "
        sSql = sSql & " a.address3 as Address3,a.salesman as SalesManCode,'INSTALLMENT' as SalesType,sum(b.weight) as TotalWeight, "
        sSql = sSql & " 0 as TotalStoneWeight,sum(b.goldloss)as TotalGoldLoss,sum(b.smc)as TotalSMC,'I' as GSTType, "
        sSql = sSql & " sum(b.gst)as GST,sum(b.DetailDiscount) as DetailDiscount,0 as NetDiscount,0 as Discount,a.totalcost as TotalCost,"
        sSql = sSql & " (select Count(*) from tblinstallmententrydetail where insagreementno=" & fncFormatSQLText(txtAgreementNo.Text) & ")as TotalPCS, "
        sSql = sSql & " a.TradeInAmt,isnull(a.TradeInGST,0)TradeInGST "
        sSql = sSql & " from tblinstallmententryheader a,tblinstallmententrydetail b "
        sSql = sSql & " where a.insagreementno=b.insagreementno and a.insagreementno=" & fncFormatSQLText(txtAgreementNo.Text)
        sSql = sSql & " group by a.customerCode,a.name,a.address1,a.address2,a.address3,a.salesman,a.totalcost,a.TradeInAmt,a.TradeInGST"
        dr1 = dm.RowReturnProjExecNonQryWithDataRow(sSql)
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
            iret = objIns.UpdateDetailsintoSales(sInvoiceno, sInvDate, txtAgreementNo.Text.Trim, sCustomerCode, sName, sAddress1, sAddress2, sAddress3, _
                                                 sSalesManCode, sSalesType, dTotalWeight, dTotalStoneWeight, dTotalGoldLoss, _
                                                 dTotalSMC, sGstType, dGST, dDetailDiscont, dDiscount, dTotalCost, dTotalPCS, dTradeinAmt, dTradeInGST, seTRSType, sUserID)


            Dim sDate As String
            sDate = Format(ServerDate, "dd/MM/yyyy")

            iret = objIns.PosMemberTransationSave(Trim(gthisLocation), sInvoiceno, sDate, Trim(txtCustomerCode.Text), _
                                         dTotalCost, dTotalCost, Val(dRedeemedPoints))
            If Not iret Then
                MessageBox.Show("Error in InstallmentSave")
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
                iret = objIns.fncUpdatePayModeFlexiClosure(Trim(txtSubscriptionID.Text), Trim(sFlexiLocation), Trim(sUserID))
                If Not iret Then
                    MessageBox.Show("Error in FlexiSave")
                    SubAddHeaderIntoSales = False
                    Exit Function
                End If
            End If

        End If
    End Function
    Private Sub insFocus()
        Try
            txtInstallmentAmount.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Function fncCheckValidity() As Boolean
        Try
            fncCheckValidity = True
            If UCase(txtCustomerCode.Text) = "CASH" Then
                If txtMemberCode.Text.Trim = "" Then
                    MsgBox("MemberCode is NOT entered !", MsgBoxStyle.Critical)
                    fncCheckValidity = False
                    txtCustomerCode.Focus()
                    Exit Function
                End If
            End If
            If Not IsNumeric(txtNoofInstallment.Text) Then
                MessageBox.Show("No. of Installment should be numeric!")
                fncCheckValidity = False
                txtNoofInstallment.Focus()
                Exit Function
            ElseIf CLng(Val(txtNoofInstallment.Text)) <= 0 Then
                MessageBox.Show("No. of Installment should be greater than zero!")
                fncCheckValidity = False
                txtNoofInstallment.Focus()
                Exit Function
            ElseIf Not IsNumeric(txtInstallmentAmount.Text) Then
                MessageBox.Show("Installment Amount should be numeric!")
                fncCheckValidity = False
                txtInstallmentAmount.Focus()
                Exit Function
            ElseIf CLng(Val(txtInstallmentAmount.Text)) <= 0 Then
                MessageBox.Show("Installment Amount should be greater than zero!")
                fncCheckValidity = False
                txtInstallmentAmount.Focus()
                Exit Function
            ElseIf Val(lblBalanceAmount.Text) < 0 Then
                MessageBox.Show("Total Installment Amount exceeds the balance Amount!")
                fncCheckValidity = False
                txtNoofInstallment.Focus()
                Exit Function
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Function fncGetInstallmentNo() As Boolean
        Try
            Dim dsInsNo As New DataSet

            dsInsNo = objIns.GetUpdateInstallmentNo()
            If dsInsNo.Tables(0).Rows.Count > 0 Then
                msInstallmentNo = "IN-" & Trim(dsInsNo.Tables(0).Rows(0)("NextInstallmentNo") & "")
                fncGetInstallmentNo = True
            Else
                fncGetInstallmentNo = False
                MessageBox.Show("Error in generating Installment No!")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Function fncPaymodeDetailSave(ByVal sInstallmentNo As String) As Boolean
        Dim objPM As New clsPayMode
        Try
            Dim bPayModeFlag As Boolean
            Dim sDate As String
            Dim i As Long
            bPayModeFlag = False
            sDate = Format(Now, "dd/MM/yyyy")
            For i = 0 To UBound(arrpay, 1)
                If arrpay(i, 1) > 0 Then
                    iRet = objPM.PayModeDetailSave(sInstallmentNo, sDate, Trim(arrpay(i, 3)), _
                                                 Val(arrpay(i, 1)), Val(arrpay(i, 2)), _
                                                 Trim(arrpay(i, 4)), "INSTALLMENT")
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
                iRet = objPM.PayModeDetailSave(nNextInvoice, sDate, "CASH", _
                                            Val(txtTotalAmount.Text), 0, _
                                            "MANUAL", "INSTALLMENT")
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

    Private Function fncSaveInstallmentDet() As Boolean
        Try
            Dim iRet As Boolean
            Dim i As Long
            Dim sRemarks$
            Dim sSql$

            iRet = objIns.InstallmentDetDelete(Trim(msInsAgreementNo), Trim(msInstallmentNo))
            If Not iRet Then
                MessageBox.Show("Error in InstallmentSave")
                fncSaveInstallmentDet = False
                Exit Function
            End If
            iRet = objIns.InstallmentHeaderUpdate(Trim(msInsAgreementNo), _
                                    Val(txtNoofInstallment.Text), Val(txtInstallmentAmount.Text), Val(txtTotalAmount.Text), Val(dTradeInSales), Val(dTradeInGST))

            If Not iRet Then
                MessageBox.Show("Error in InstallmentSave")
                fncSaveInstallmentDet = False
                Exit Function
            End If

            iRet = objIns.InstallmentDetailSave(Trim(msInsAgreementNo), Trim(msInstallmentNo), _
                                    Val(txtNoofInstallment.Text), Val(txtInstallmentAmount.Text), Val(txtTotalAmount.Text), sRemarks)
            If Not iRet Then
                MessageBox.Show("Error in Save")
                fncSaveInstallmentDet = False
                Exit Function
            End If
            fncSaveInstallmentDet = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function
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
    Private Sub subPrintInstallment(ByVal sInvoiceNo As String)
        Try
            Dim ReportTitle As String
            Dim sSelection As String
            Dim sSalesType$
            Dim frmrep As New frmReports
            Dim CR As New ReportDocument
            Dim j As Integer
            Cursor = Cursors.WaitCursor
            If Trim(sCompanyShortCode) = "AJ" Then
                strpath = Application.StartupPath & "\Reports\rptInstallmentPayment_AJ.rpt"
            ElseIf Trim(sCompanyShortCode) = "MD" Then
                strpath = Application.StartupPath & "\Reports\rptInstallmentPayment_MD.rpt"
            ElseIf Trim(sCompanyShortCode) = "MJ" Then
                strpath = Application.StartupPath & "\Reports\rptInstallmentPayment_MJ.rpt"
            End If

            CR.Load(strpath)
            If Not ReportLogOnToServer(CR) Then
                Exit Sub
            End If
            ReportLogOnToServer(CR)
            CR.Refresh()
            subGetClassRate()
            frmrep.ReportViewer.ReportSource = CR
            sSelection = "{tblInstallmentDetail.InstallmentNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
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
            ' frmrep.Show()
            '   If bPrinter Then CR.PrintToPrinter(1, True, 0, 0)
            ''frmrep.ReportViewer.Visible = True
            ''frmrep.ReportViewer.DisplayToolbar = True
            ''frmrep.ReportViewer.CloseView(False)
            ''frmrep.WindowState = FormWindowState.Maximized
            ''frmrep = Nothing
            ''frmrep = New frmReports
            'If Trim(sInsInvoiceNo) <> "" Then
            '    CR.PrintToPrinter(1, True, 0, 0)
            'Else
            CR.PrintToPrinter(2, True, 0, 0)
            'End If
            frmrep = Nothing
            frmrep = New frmReports

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub subPrintBillold(ByVal sInstallmentNo As String)
        Try
            Dim ReportTitle As String
            Dim sSelection As String
            Dim sSalesType$
            Dim frmrep As New frmReports
            Dim CR As New ReportDocument
            Dim j As Integer
            Cursor = Cursors.WaitCursor
            If UCase(sCompanyShortCode) = "RJ" Then
                j = 1
                For j = 1 To 2
                    strpath = Application.StartupPath & "\Reports\rptInstallment.rpt"
                    CR.Load(strpath)
                    'If Not ReportLogOnToServer(CR) Then
                    '    Exit Sub
                    'End If
                    ReportLogOnToServer(CR)
                    CR.Refresh()
                    frmrep.ReportViewer.ReportSource = CR
                    If bReprint = True Then
                        sSelection = "{tblInstallmentDetail.InstallmentNo} = " & fncFormatSQLText(Trim(sInstallmentNo))
                    Else
                        sSelection = "{tblInstallmentHeader.InsAgreementNo} = " & fncFormatSQLText(Trim(msInsAgreementNo)) & _
                                    " And {tblInstallmentDetail.InstallmentNo} = " & fncFormatSQLText(Trim(sInstallmentNo))
                    End If
                    frmrep.ReportViewer.SelectionFormula = sSelection
                    CR.DataDefinition.RecordSelectionFormula = sSelection
                    CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                    CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                    CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                    CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                    CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                    CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                    'frmrep.Show()
                    'If bPrinter Then CR.PrintToPrinter(1, True, 0, 0)
                    'frmrep.ReportViewer.Visible = True
                    'frmrep.ReportViewer.DisplayToolbar = True
                    'frmrep.ReportViewer.CloseView(False)
                    'frmrep.WindowState = FormWindowState.Maximized
                    'frmrep = Nothing
                    'frmrep = New frmReports
                    CR.PrintToPrinter(1, True, 0, 0)
                    frmrep = Nothing
                    frmrep = New frmReports
                Next
            ElseIf UCase(sCompanyShortCode) = "HN" Then
                strpath = Application.StartupPath & "\Reports\rptInstallment_HN.rpt"
                CR.Load(strpath)
                If Not ReportLogOnToServer(CR) Then
                    Exit Sub
                End If
                ReportLogOnToServer(CR)
                CR.Refresh()
                frmrep.ReportViewer.ReportSource = CR
                If bReprint = True Then
                    sSelection = "{tblInstallmentDetail.InstallmentNo} = " & fncFormatSQLText(Trim(sInstallmentNo))
                Else
                    sSelection = "{tblInstallmentHeader.InsAgreementNo} = " & fncFormatSQLText(Trim(msInsAgreementNo)) & _
                                " And {tblInstallmentDetail.InstallmentNo} = " & fncFormatSQLText(Trim(sInstallmentNo))
                End If
                frmrep.ReportViewer.SelectionFormula = sSelection
                CR.DataDefinition.RecordSelectionFormula = sSelection
                CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                frmrep.Show()
                If bPrinter Then CR.PrintToPrinter(1, True, 0, 0)
                frmrep.ReportViewer.Visible = True
                frmrep.ReportViewer.DisplayToolbar = True
                frmrep.ReportViewer.CloseView(False)
                frmrep.WindowState = FormWindowState.Maximized
                frmrep = Nothing
                frmrep = New frmReports
                '''CR.PrintToPrinter(1, True, 0, 0)
                '''frmrep = Nothing
                '''frmrep = New frmReports
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnReprintOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReprintOK.Click
        Try
            If Trim(txtRePrintBillNo.Text) <> "" Then
                bReprint = True
                'Call subPrintBill(Trim(txtRePrintBillNo.Text))
                Call subPrintInstallment(Trim(txtRePrintBillNo.Text))
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
            txtNoofInstallment.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnBillCancelOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBillCancelOK.Click
        Try
            Dim dsIns As New DataSet
            Dim sLocationCode$
            dsIns = objIns.GetInsDetlByInsNo(Trim(txtBillCancelBillNo.Text))
            If dsIns.Tables(0).Rows.Count > 0 Then
                If DateValue(dsIns.Tables(0).Rows(0)("CreateDate") & "") = DateValue(Trim(ServerDate)) Then

                    If MsgBox("Do you want to CANCEL the Installment Bill No. " & Trim(txtBillCancelBillNo.Text) & " ? ", vbYesNo) = vbYes Then
                        sLocationCode = gthisLocation
                        iRet = objIns.InsBillCancel(Trim(txtBillCancelBillNo.Text), Trim(sLocationCode))
                        pnlCancel.Visible = False
                        'refresh the data and the grid
                        txtAgreementNo.Text = msInsAgreementNo
                        txtAgreementNo_LostFocus(sender, e)
                        txtNoofInstallment.Focus()
                    Else
                        pnlCancel.Visible = False
                        txtNoofInstallment.Focus()
                    End If
                Else
                    MessageBox.Show("You cannot cancel History Inventory Bills ....")
                    pnlCancel.Visible = False
                    txtNoofInstallment.Focus()
                End If
            Else
                MessageBox.Show("This Installment Bill Not Found... Please Give Correct Installment Bill No")
                txtBillCancelBillNo.Clear()
                txtBillCancelBillNo.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnBillCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBillCancel.Click
        Try
            txtBillCancelBillNo.Clear()
            pnlCancel.Visible = False
            txtNoofInstallment.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub frmInstallmentPayment_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Escape
                    Me.Close()
                Case Keys.Enter
                    SendKeys.Send("{TAB}")
                Case Keys.F6
                    btnBillClear_Click(sender, e)
                Case Keys.F7
                    btnSAve_Click(sender, e)
                Case Keys.F10
                    btnCancel_Click(sender, e)
                Case Keys.F9
                    btnRePrint_Click(sender, e)
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtCustomerCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustomerCode.LostFocus
        Try

            If UCase(txtCustomerCode.Text.Trim) = "CASH" And sMemberModule = "1" Then
                pnlMember.Visible = True
                btnCustomerCodeSearch.Enabled = False
                txtMemberCode.Visible = True
                txtMemberCode.Text = ""
                txtMemberCodeView.Clear()
                txtMemberCodeView.Focus()
            Else
                pnlMember.Visible = False
                btnCustomerCodeSearch.Enabled = True
                txtMemberCode.Visible = False
                txtMemberCode.Text = ""
                txtMemberCodeView.Clear()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click

        sMemberCode = ""
        Dim frmMem As New frmMember
        frmMem.ShowDialog()
        txtMemberCodeView.Text = sMemberCode.Trim
        btnOK.Focus()
        frmMem = Nothing


    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim ds As New DataSet
        Try
            If txtMemberCodeView.Text.Trim <> "" Then
                ds = objMember.SubGetMemberCode(Trim(txtMemberCodeView.Text))
                If ds.Tables(0).Rows.Count > 0 Then
                    txtMemberCode.Text = Trim(ds.Tables(0).Rows(0)("MemberCode") & "")
                    txtCustomerName.Text = Trim(ds.Tables(0).Rows(0)("MemberName") & "")
                    lblCustomerType.Text = Trim(ds.Tables(0).Rows(0)("MemberType") & "")
                    txtAddress1.Text = Trim(ds.Tables(0).Rows(0)("Address1") & "")
                    txtAddress2.Text = Trim(ds.Tables(0).Rows(0)("Address2") & "")
                    txtAddress3.Text = Trim(ds.Tables(0).Rows(0)("Address3") & "")
                    pnlMember.Visible = False
                    btnCustomerCodeSearch.Enabled = False
                    txtCustomerName.Focus()
                Else
                    MsgBox("No such Member ! ", MsgBoxStyle.Critical)
                    txtMemberCode.Text = ""
                    txtCustomerName.Text = ""
                    lblCustomerType.Text = ""
                    txtAddress1.Text = ""
                    txtAddress2.Text = ""
                    txtAddress3.Text = ""
                    txtMemberCodeView.Text = ""
                    pnlMember.Visible = False
                    btnCustomerCodeSearch.Enabled = True
                    txtCustomerCode.Focus()

                End If
            Else
                pnlMember.Visible = False
                btnCustomerCodeSearch.Enabled = True
                txtCustomerCode.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            ds = Nothing
        End Try
    End Sub

    Private Sub btnMemberCodeSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMemberCodeSearch.Click
        Try
            Dim frmHelpCustomer As New frmHelpSearch(0, 0, "tblMember", "L", 4, "MemberName", "", "MemberCode", "MemberCode", 80, "MemberName", "MemberName", 150, "Address1", "Address1", 90, "Address2", "Address2", 80)
            frmHelpCustomer.ShowDialog()
            txtMemberCodeView.Text = frmHelpCustomer.Var1.Trim
            btnOK.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub btnCancelFme_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelFme.Click
        pnlMember.Visible = False
        btnCustomerCodeSearch.Enabled = True
        txtCustomerCode.Focus()
    End Sub



    Private Sub txtInstallmentAmount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInstallmentAmount.LostFocus
        Try
            If Trim(sCompanyShortCode) = "HN" Then
                If Val(txtInstallmentAmount.Text) > 0 Then
                    If Val(txtInstallmentAmount.Text) < 100 And (Val(lblBalanceAmount.Text) / 6) > 100 Then
                        MessageBox.Show("Installment Should not Less than 100")
                        txtInstallmentAmount.Text = ""
                        txtTotalAmount.Text = ""
                        txtInstallmentAmount.Focus()
                        Exit Sub
                    ElseIf (Val(lblBalanceAmount.Text) / 6) < 100 And Val(txtInstallmentAmount.Text) < Format(dbalAmt / 6, "0.00") Then
                        MessageBox.Show("Installment Should not Less than " & Format(dbalAmt / 6, "0.00"))
                        txtInstallmentAmount.Text = ""
                        txtTotalAmount.Text = ""
                        txtInstallmentAmount.Focus()
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtAgreementNo_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtAgreementNo.TextChanged

    End Sub

    Private Sub txtCustomerCode_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtCustomerCode.TextChanged

    End Sub

    Private Sub btnTradeIn_Click(sender As System.Object, e As System.EventArgs) Handles btnTradeIn.Click
        Try
            Dim TradeIn As New frmTradeInGold
            'subGetCompany()
            grdSales.RowCount = 0
            TradeIn.sCalledFrom = "ORDERFORM"
            TradeIn.sInvoiceNo = Format(Val(txtAgreementNo.Text), "00000000")
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
            txtNoofInstallment.Text = 1
            txtInstallmentAmount.Text = dTradeinAmount
            txtTotalAmount.Text = dTradeinAmount
            tradeInRefNo = sTradeInRefNo
            dTradeInSales = dTradeinAmount
            dTradeInGST = Val(-Format$((Val(dTradeinAmount) * 9) / (109), "#0.00"))
            subAddTradeInToGrid()
            If ChkTradeIn Then
                btnTradeIn.Enabled = False
                optCash.Enabled = False
                txtTotalAmount.Enabled = False
            Else
                btnTradeIn.Enabled = True
                optCash.Enabled = True
                txtTotalAmount.Enabled = True
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
    Private Sub optGold_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optGold.CheckedChanged
        If optGold.Checked = True Then
            btnTradeIn.Visible = True
        Else
            btnTradeIn.Visible = False
        End If
    End Sub

    Private Sub optCash_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optCash.CheckedChanged
        If optCash.Checked = True Then
            btnTradeIn.Visible = False
        Else
            btnTradeIn.Visible = True
        End If
    End Sub

    Private Sub optBoth_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optBoth.CheckedChanged
        If optBoth.Checked = True Then
            btnTradeIn.Visible = True
        Else
            btnTradeIn.Visible = False
        End If
    End Sub
    Private Sub subPrintBill(ByVal sInvoiceNo As String)
        Dim ReportTitle As String
        Dim sSelection As String
        Dim sSalesType$
        Dim sReply As String
        Dim dsSales As New DataSet
        Dim frmrep As New frmReports
        Dim CR As New ReportDocument
        Dim j As Integer
        Dim sCmd As String
        Dim sExec As String
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


                sCmd = " Exec sp_InstallmentPayMode " & fncFormatSQLText(sInsAgreementNo)
                Dm.ProjExecNonQry(sCmd)

                ''strpath = Application.StartupPath & "\Reports\rptSalesInvoice_MJ.rpt"
                If sInvGstType = "" Then
                    strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_MJ.rpt"
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
                CR.PrintToPrinter(2, True, 0, 0)

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

                            sCmd = " Exec sp_InstallmentPayMode " & fncFormatSQLText(sInsAgreementNo)
                            Dm.ProjExecNonQry(sCmd)

                            sExec = " Exec sp_InstallmentTradeIn " & fncFormatSQLText(sInsAgreementNo)
                            Dm.ProjExecNonQry(sExec)

                            If sCompanyShortCode = "ES" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_ESP.rpt"
                            ElseIf sCompanyShortCode = "AS" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_AS.rpt"
                            ElseIf sCompanyShortCode = "ML" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_ML.rpt"
                            ElseIf UCase(sCompanyShortCode) = "GMT" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_GMT.rpt"
                            ElseIf UCase(sCompanyShortCode) = "AJ" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_AJ.rpt"
                            ElseIf UCase(sCompanyShortCode) = "MD" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_MD.rpt"
                            ElseIf UCase(sCompanyShortCode) = "MJ" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_MJ.rpt"
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

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

    End Sub
End Class
