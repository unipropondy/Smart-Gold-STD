Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System
Imports System.IO

Public Class frmGPOS
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
    Friend WithEvents txtSalesManPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnCustomerCodeSearch As System.Windows.Forms.Button
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress3 As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress2 As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress1 As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerCode As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtIYYYY As System.Windows.Forms.TextBox
    Friend WithEvents txtIMM As System.Windows.Forms.TextBox
    Friend WithEvents txtIDD As System.Windows.Forms.TextBox

    Friend WithEvents txtSalesManName As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents optZeroRated As System.Windows.Forms.RadioButton
    Friend WithEvents optExclusive As System.Windows.Forms.RadioButton
    Friend WithEvents optInclusive As System.Windows.Forms.RadioButton

    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents txtInventoryDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtInventoryCode As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtGoldLoss As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtPCS As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtSMC As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents txtGST As System.Windows.Forms.TextBox
    Friend WithEvents txtCostPerGram As System.Windows.Forms.TextBox
    Friend WithEvents lblTax As System.Windows.Forms.Label
    Friend WithEvents lblNetValue As System.Windows.Forms.Label
    Friend WithEvents lblNetDiscount As System.Windows.Forms.Label
    Friend WithEvents lblSalesAmount As System.Windows.Forms.Label
    Friend WithEvents lblQty As System.Windows.Forms.Label
    Friend WithEvents lblNetTotal As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents lblLocation As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents lblTerminal As System.Windows.Forms.Label
    Friend WithEvents lblLastSalesMan As System.Windows.Forms.Label
    Friend WithEvents lblLastBillNo As System.Windows.Forms.Label
    Friend WithEvents lblLastBillAmount As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents lblReturnAmount As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents lblSalesType As System.Windows.Forms.Label
    Friend WithEvents PnlReprint As System.Windows.Forms.Panel
    Friend WithEvents btnReprintCancel As System.Windows.Forms.Button
    Friend WithEvents btnReprintOK As System.Windows.Forms.Button
    Friend WithEvents txtRePrintBillNo As System.Windows.Forms.TextBox
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents btnBillCancel As System.Windows.Forms.Button
    Friend WithEvents btnBillCancelOK As System.Windows.Forms.Button
    Friend WithEvents txtBillCancelBillNo As System.Windows.Forms.TextBox
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents pnlBillCancel As System.Windows.Forms.Panel
    Friend WithEvents btnAdminPwdCancel As System.Windows.Forms.Button
    Friend WithEvents btnAdminPwdOK As System.Windows.Forms.Button
    Friend WithEvents txtAdminPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents pnlAdminPwd As System.Windows.Forms.Panel
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents txtCashSales As System.Windows.Forms.TextBox
    Friend WithEvents txtTradeIn As System.Windows.Forms.TextBox
    Friend WithEvents txtInstalment As System.Windows.Forms.TextBox
    Friend WithEvents pnlPaymentType As System.Windows.Forms.Panel
    Friend WithEvents btnInstalmentCancel As System.Windows.Forms.Button
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents Type As System.Windows.Forms.Label
    Friend WithEvents txtExpenseDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents txtExpenseAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents btnExpenseClose As System.Windows.Forms.Button
    Friend WithEvents btnExpenseSave As System.Windows.Forms.Button
    Friend WithEvents pnlExpense As System.Windows.Forms.Panel
    Friend WithEvents cboExpensesType As System.Windows.Forms.ComboBox
    Friend WithEvents pnlTotal As System.Windows.Forms.Panel
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents txtPMGST As System.Windows.Forms.TextBox
    Friend WithEvents txtGrandTotal As System.Windows.Forms.TextBox
    Friend WithEvents btnPMClear As System.Windows.Forms.Button
    Friend WithEvents btnPMPayment As System.Windows.Forms.Button
    Friend WithEvents txtPayMode As System.Windows.Forms.TextBox
    Friend WithEvents btnPaymodeSearch As System.Windows.Forms.Button
    Friend WithEvents txtPMNetTotal As System.Windows.Forms.TextBox
    Friend WithEvents txtPMDiscount As System.Windows.Forms.TextBox
    Friend WithEvents txtPMSurCharge As System.Windows.Forms.TextBox
    Friend WithEvents txtStoneWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalValue As System.Windows.Forms.TextBox
    Friend WithEvents btnPaymentTypeOK As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents PnlNetAmount As System.Windows.Forms.Panel
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents txtNetAmount As System.Windows.Forms.TextBox
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents optInstallment As System.Windows.Forms.RadioButton
    Friend WithEvents optCashSales As System.Windows.Forms.RadioButton
    Friend WithEvents optTradeIn As System.Windows.Forms.RadioButton
    Friend WithEvents lblCustomerType As System.Windows.Forms.Label
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents pnlFindCost As System.Windows.Forms.Panel
    Friend WithEvents btnFindCostOK As System.Windows.Forms.Button
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents Label75 As System.Windows.Forms.Label
    Friend WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents Label78 As System.Windows.Forms.Label
    Friend WithEvents txtFindInventory As System.Windows.Forms.TextBox
    Friend WithEvents txtFindDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtFindWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtFindPBillNo As System.Windows.Forms.TextBox
    Friend WithEvents txtFindPurchaseDate As System.Windows.Forms.TextBox
    Friend WithEvents txtFindMC As System.Windows.Forms.TextBox
    Friend WithEvents txtSupplierCode As System.Windows.Forms.TextBox
    Friend WithEvents txtFindTotalCost As System.Windows.Forms.TextBox
    Friend WithEvents txtFindCostPerGram As System.Windows.Forms.TextBox
    Friend WithEvents txtTael As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalFinal As System.Windows.Forms.TextBox
    Friend WithEvents txtNetDiscount As System.Windows.Forms.TextBox
    Friend WithEvents txtNetDiscountPercentage As System.Windows.Forms.TextBox
    Friend WithEvents lblCompanyName As System.Windows.Forms.Label
    Friend WithEvents lblCompanyAddress As System.Windows.Forms.Label
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents txtLossPer As System.Windows.Forms.TextBox
    Friend WithEvents btnNewCustomer As System.Windows.Forms.Button
    Friend WithEvents Label79 As System.Windows.Forms.Label
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents pnlMember As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents txtMemberCode As System.Windows.Forms.TextBox
    Friend WithEvents txtMemberCodeView As System.Windows.Forms.TextBox
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents Label82 As System.Windows.Forms.Label
    Friend WithEvents btnMemberCodeSearch As System.Windows.Forms.Button
    Friend WithEvents grpType As System.Windows.Forms.GroupBox
    Friend WithEvents lblCash As System.Windows.Forms.Label
    Friend WithEvents lblInstalment As System.Windows.Forms.Label
    Friend WithEvents lbltradeIN As System.Windows.Forms.Label
    Friend WithEvents lblSManTotal As System.Windows.Forms.Label
    Friend WithEvents lblSManTotalCap As System.Windows.Forms.Label
    Friend WithEvents lblNextInvoiceNo As System.Windows.Forms.Label
    Friend WithEvents lblNextInvoiceNoCap As System.Windows.Forms.Label
    Friend WithEvents pnlBillCancelAdminPwd As System.Windows.Forms.Panel
    Friend WithEvents txtBCAdminPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents btnBCAdminPwdCancel As System.Windows.Forms.Button
    Friend WithEvents pnlCostAdminPwd As System.Windows.Forms.Panel
    Friend WithEvents btnCostAdminPwdCancel As System.Windows.Forms.Button
    Friend WithEvents btnCostAdminPwdOK As System.Windows.Forms.Button
    Friend WithEvents txtCostAdminPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtPassFind As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents grdSales As System.Windows.Forms.DataGridView
    Friend WithEvents grdMarketRate As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colRate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkeTRS As System.Windows.Forms.CheckBox
    Friend WithEvents chkPrintMC As System.Windows.Forms.CheckBox
    Friend WithEvents chkPrintImage As System.Windows.Forms.CheckBox
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents opteTRSNo As System.Windows.Forms.RadioButton
    Friend WithEvents opteTrsTicketIssued As System.Windows.Forms.RadioButton
    Friend WithEvents opteTRSEligible As System.Windows.Forms.RadioButton
    Friend WithEvents lblMemberPoints As System.Windows.Forms.Label
    Friend WithEvents chkShowImage As System.Windows.Forms.CheckBox
    Friend WithEvents grpImage As System.Windows.Forms.GroupBox
    Friend WithEvents picImage As System.Windows.Forms.PictureBox
    Friend WithEvents cmdAddlDescOk As System.Windows.Forms.Button
    Friend WithEvents btnSubscriptionID As System.Windows.Forms.Button
    Friend WithEvents txtSubscriptionID As System.Windows.Forms.TextBox
    Friend WithEvents lblId As System.Windows.Forms.Label
    Friend WithEvents Label80 As System.Windows.Forms.Label
    Friend WithEvents btnAdvDeposit As System.Windows.Forms.Button
    Friend WithEvents txtAdvDeposit As System.Windows.Forms.TextBox
    Friend WithEvents Label83 As System.Windows.Forms.Label
    Friend WithEvents Label81 As System.Windows.Forms.Label
    Friend WithEvents txtFlexiBonus As System.Windows.Forms.TextBox
    Friend WithEvents txtFlexiAmount As System.Windows.Forms.TextBox
    Friend WithEvents chkTradeIn As System.Windows.Forms.CheckBox
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
    Friend WithEvents colDetailDiscount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnBCAdminPwdOK As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGPOS))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label83 = New System.Windows.Forms.Label()
        Me.Label81 = New System.Windows.Forms.Label()
        Me.txtFlexiBonus = New System.Windows.Forms.TextBox()
        Me.txtFlexiAmount = New System.Windows.Forms.TextBox()
        Me.Label80 = New System.Windows.Forms.Label()
        Me.btnAdvDeposit = New System.Windows.Forms.Button()
        Me.txtAdvDeposit = New System.Windows.Forms.TextBox()
        Me.lblId = New System.Windows.Forms.Label()
        Me.btnSubscriptionID = New System.Windows.Forms.Button()
        Me.txtSubscriptionID = New System.Windows.Forms.TextBox()
        Me.chkPrintImage = New System.Windows.Forms.CheckBox()
        Me.chkPrintMC = New System.Windows.Forms.CheckBox()
        Me.Label82 = New System.Windows.Forms.Label()
        Me.btnNewCustomer = New System.Windows.Forms.Button()
        Me.lblCustomerType = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.optInclusive = New System.Windows.Forms.RadioButton()
        Me.optZeroRated = New System.Windows.Forms.RadioButton()
        Me.optExclusive = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtSalesManName = New System.Windows.Forms.TextBox()
        Me.txtSalesManPassword = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnCustomerCodeSearch = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtAddress3 = New System.Windows.Forms.TextBox()
        Me.txtAddress2 = New System.Windows.Forms.TextBox()
        Me.txtAddress1 = New System.Windows.Forms.TextBox()
        Me.txtCustomerCode = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtIYYYY = New System.Windows.Forms.TextBox()
        Me.txtIMM = New System.Windows.Forms.TextBox()
        Me.txtIDD = New System.Windows.Forms.TextBox()
        Me.txtMemberCode = New System.Windows.Forms.TextBox()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.txtGST = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtTotalValue = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtPCS = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtSMC = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtCostPerGram = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtGoldLoss = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtWeight = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtInventoryCode = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtInventoryDescription = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.lblTax = New System.Windows.Forms.Label()
        Me.lblNetValue = New System.Windows.Forms.Label()
        Me.lblNetDiscount = New System.Windows.Forms.Label()
        Me.lblSalesAmount = New System.Windows.Forms.Label()
        Me.lblQty = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblNetTotal = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.lblTerminal = New System.Windows.Forms.Label()
        Me.lblReturnAmount = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.lblSManTotal = New System.Windows.Forms.Label()
        Me.lblSManTotalCap = New System.Windows.Forms.Label()
        Me.lblNextInvoiceNo = New System.Windows.Forms.Label()
        Me.lblNextInvoiceNoCap = New System.Windows.Forms.Label()
        Me.lblLastBillAmount = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.lblLastBillNo = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.lblLastSalesMan = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.lblLocation = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtStoneWeight = New System.Windows.Forms.TextBox()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.lblSalesType = New System.Windows.Forms.Label()
        Me.PnlReprint = New System.Windows.Forms.Panel()
        Me.btnReprintCancel = New System.Windows.Forms.Button()
        Me.btnReprintOK = New System.Windows.Forms.Button()
        Me.txtRePrintBillNo = New System.Windows.Forms.TextBox()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.pnlBillCancel = New System.Windows.Forms.Panel()
        Me.btnBillCancel = New System.Windows.Forms.Button()
        Me.btnBillCancelOK = New System.Windows.Forms.Button()
        Me.txtBillCancelBillNo = New System.Windows.Forms.TextBox()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.pnlAdminPwd = New System.Windows.Forms.Panel()
        Me.btnAdminPwdCancel = New System.Windows.Forms.Button()
        Me.btnAdminPwdOK = New System.Windows.Forms.Button()
        Me.txtAdminPassword = New System.Windows.Forms.TextBox()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.pnlPaymentType = New System.Windows.Forms.Panel()
        Me.btnPaymentTypeOK = New System.Windows.Forms.Button()
        Me.txtInstalment = New System.Windows.Forms.TextBox()
        Me.txtTradeIn = New System.Windows.Forms.TextBox()
        Me.btnInstalmentCancel = New System.Windows.Forms.Button()
        Me.txtCashSales = New System.Windows.Forms.TextBox()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.optInstallment = New System.Windows.Forms.RadioButton()
        Me.optCashSales = New System.Windows.Forms.RadioButton()
        Me.optTradeIn = New System.Windows.Forms.RadioButton()
        Me.pnlExpense = New System.Windows.Forms.Panel()
        Me.cboExpensesType = New System.Windows.Forms.ComboBox()
        Me.txtExpenseAmount = New System.Windows.Forms.TextBox()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.txtExpenseDescription = New System.Windows.Forms.TextBox()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.btnExpenseClose = New System.Windows.Forms.Button()
        Me.btnExpenseSave = New System.Windows.Forms.Button()
        Me.Type = New System.Windows.Forms.Label()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.pnlTotal = New System.Windows.Forms.Panel()
        Me.pnlBillCancelAdminPwd = New System.Windows.Forms.Panel()
        Me.btnBCAdminPwdCancel = New System.Windows.Forms.Button()
        Me.btnBCAdminPwdOK = New System.Windows.Forms.Button()
        Me.txtBCAdminPassword = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.grpType = New System.Windows.Forms.GroupBox()
        Me.lbltradeIN = New System.Windows.Forms.Label()
        Me.lblInstalment = New System.Windows.Forms.Label()
        Me.lblCash = New System.Windows.Forms.Label()
        Me.txtPayMode = New System.Windows.Forms.TextBox()
        Me.txtLossPer = New System.Windows.Forms.TextBox()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.txtTotalFinal = New System.Windows.Forms.TextBox()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.txtPMSurCharge = New System.Windows.Forms.TextBox()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.txtPMNetTotal = New System.Windows.Forms.TextBox()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.txtPMDiscount = New System.Windows.Forms.TextBox()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.btnPaymodeSearch = New System.Windows.Forms.Button()
        Me.txtPMGST = New System.Windows.Forms.TextBox()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.txtGrandTotal = New System.Windows.Forms.TextBox()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.btnPMClear = New System.Windows.Forms.Button()
        Me.btnPMPayment = New System.Windows.Forms.Button()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PnlNetAmount = New System.Windows.Forms.Panel()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.txtNetAmount = New System.Windows.Forms.TextBox()
        Me.txtNetDiscountPercentage = New System.Windows.Forms.TextBox()
        Me.txtNetDiscount = New System.Windows.Forms.TextBox()
        Me.lblCompanyName = New System.Windows.Forms.Label()
        Me.pnlFindCost = New System.Windows.Forms.Panel()
        Me.txtTael = New System.Windows.Forms.TextBox()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.txtFindCostPerGram = New System.Windows.Forms.TextBox()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.txtFindTotalCost = New System.Windows.Forms.TextBox()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.txtSupplierCode = New System.Windows.Forms.TextBox()
        Me.Label75 = New System.Windows.Forms.Label()
        Me.txtFindMC = New System.Windows.Forms.TextBox()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.txtFindPurchaseDate = New System.Windows.Forms.TextBox()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.txtFindPBillNo = New System.Windows.Forms.TextBox()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.txtFindWeight = New System.Windows.Forms.TextBox()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.txtFindDescription = New System.Windows.Forms.TextBox()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.btnFindCostOK = New System.Windows.Forms.Button()
        Me.txtFindInventory = New System.Windows.Forms.TextBox()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.lblCompanyAddress = New System.Windows.Forms.Label()
        Me.pnlMember = New System.Windows.Forms.Panel()
        Me.btnMemberCodeSearch = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.txtMemberCodeView = New System.Windows.Forms.TextBox()
        Me.Label79 = New System.Windows.Forms.Label()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.pnlCostAdminPwd = New System.Windows.Forms.Panel()
        Me.btnCostAdminPwdCancel = New System.Windows.Forms.Button()
        Me.btnCostAdminPwdOK = New System.Windows.Forms.Button()
        Me.txtCostAdminPassword = New System.Windows.Forms.TextBox()
        Me.txtPassFind = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.grdSales = New System.Windows.Forms.DataGridView()
        Me.grdMarketRate = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colRate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chkeTRS = New System.Windows.Forms.CheckBox()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.opteTRSNo = New System.Windows.Forms.RadioButton()
        Me.opteTrsTicketIssued = New System.Windows.Forms.RadioButton()
        Me.opteTRSEligible = New System.Windows.Forms.RadioButton()
        Me.lblMemberPoints = New System.Windows.Forms.Label()
        Me.chkShowImage = New System.Windows.Forms.CheckBox()
        Me.grpImage = New System.Windows.Forms.GroupBox()
        Me.picImage = New System.Windows.Forms.PictureBox()
        Me.cmdAddlDescOk = New System.Windows.Forms.Button()
        Me.chkTradeIn = New System.Windows.Forms.CheckBox()
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
        Me.colDetailDiscount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.PnlReprint.SuspendLayout()
        Me.pnlBillCancel.SuspendLayout()
        Me.pnlAdminPwd.SuspendLayout()
        Me.pnlPaymentType.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.pnlExpense.SuspendLayout()
        Me.pnlTotal.SuspendLayout()
        Me.pnlBillCancelAdminPwd.SuspendLayout()
        Me.grpType.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlNetAmount.SuspendLayout()
        Me.pnlFindCost.SuspendLayout()
        Me.pnlMember.SuspendLayout()
        Me.pnlCostAdminPwd.SuspendLayout()
        CType(Me.grdSales, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdMarketRate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel8.SuspendLayout()
        Me.grpImage.SuspendLayout()
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label83)
        Me.Panel1.Controls.Add(Me.Label81)
        Me.Panel1.Controls.Add(Me.txtFlexiBonus)
        Me.Panel1.Controls.Add(Me.txtFlexiAmount)
        Me.Panel1.Controls.Add(Me.Label80)
        Me.Panel1.Controls.Add(Me.btnAdvDeposit)
        Me.Panel1.Controls.Add(Me.txtAdvDeposit)
        Me.Panel1.Controls.Add(Me.lblId)
        Me.Panel1.Controls.Add(Me.btnSubscriptionID)
        Me.Panel1.Controls.Add(Me.txtSubscriptionID)
        Me.Panel1.Controls.Add(Me.chkPrintImage)
        Me.Panel1.Controls.Add(Me.chkPrintMC)
        Me.Panel1.Controls.Add(Me.Label82)
        Me.Panel1.Controls.Add(Me.btnNewCustomer)
        Me.Panel1.Controls.Add(Me.lblCustomerType)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtSalesManName)
        Me.Panel1.Controls.Add(Me.txtSalesManPassword)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.btnCustomerCodeSearch)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtRemarks)
        Me.Panel1.Controls.Add(Me.txtAddress3)
        Me.Panel1.Controls.Add(Me.txtAddress2)
        Me.Panel1.Controls.Add(Me.txtAddress1)
        Me.Panel1.Controls.Add(Me.txtCustomerCode)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txtIYYYY)
        Me.Panel1.Controls.Add(Me.txtIMM)
        Me.Panel1.Controls.Add(Me.txtIDD)
        Me.Panel1.Controls.Add(Me.txtMemberCode)
        Me.Panel1.Controls.Add(Me.txtCustomerName)
        Me.Panel1.Location = New System.Drawing.Point(304, 72)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(536, 216)
        Me.Panel1.TabIndex = 0
        '
        'Label83
        '
        Me.Label83.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label83.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label83.ForeColor = System.Drawing.Color.Black
        Me.Label83.Location = New System.Drawing.Point(338, 177)
        Me.Label83.Name = "Label83"
        Me.Label83.Size = New System.Drawing.Size(80, 14)
        Me.Label83.TabIndex = 529
        Me.Label83.Text = "Flexi Bonus"
        Me.Label83.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label81
        '
        Me.Label81.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label81.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label81.ForeColor = System.Drawing.Color.Black
        Me.Label81.Location = New System.Drawing.Point(256, 177)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(80, 14)
        Me.Label81.TabIndex = 528
        Me.Label81.Text = "Flexi Amount"
        Me.Label81.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtFlexiBonus
        '
        Me.txtFlexiBonus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFlexiBonus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFlexiBonus.ForeColor = System.Drawing.Color.Black
        Me.txtFlexiBonus.Location = New System.Drawing.Point(341, 189)
        Me.txtFlexiBonus.MaxLength = 50
        Me.txtFlexiBonus.Name = "txtFlexiBonus"
        Me.txtFlexiBonus.Size = New System.Drawing.Size(81, 20)
        Me.txtFlexiBonus.TabIndex = 527
        Me.txtFlexiBonus.Tag = ""
        '
        'txtFlexiAmount
        '
        Me.txtFlexiAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFlexiAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFlexiAmount.ForeColor = System.Drawing.Color.Black
        Me.txtFlexiAmount.Location = New System.Drawing.Point(256, 189)
        Me.txtFlexiAmount.MaxLength = 50
        Me.txtFlexiAmount.Name = "txtFlexiAmount"
        Me.txtFlexiAmount.Size = New System.Drawing.Size(81, 20)
        Me.txtFlexiAmount.TabIndex = 526
        Me.txtFlexiAmount.Tag = ""
        '
        'Label80
        '
        Me.Label80.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label80.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label80.ForeColor = System.Drawing.Color.Black
        Me.Label80.Location = New System.Drawing.Point(385, 57)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(55, 22)
        Me.Label80.TabIndex = 525
        Me.Label80.Text = "Advance"
        Me.Label80.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnAdvDeposit
        '
        Me.btnAdvDeposit.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnAdvDeposit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdvDeposit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdvDeposit.Location = New System.Drawing.Point(506, 59)
        Me.btnAdvDeposit.Name = "btnAdvDeposit"
        Me.btnAdvDeposit.Size = New System.Drawing.Size(24, 20)
        Me.btnAdvDeposit.TabIndex = 524
        Me.btnAdvDeposit.Text = "..."
        Me.btnAdvDeposit.UseVisualStyleBackColor = False
        '
        'txtAdvDeposit
        '
        Me.txtAdvDeposit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdvDeposit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdvDeposit.ForeColor = System.Drawing.Color.Black
        Me.txtAdvDeposit.Location = New System.Drawing.Point(437, 59)
        Me.txtAdvDeposit.MaxLength = 20
        Me.txtAdvDeposit.Name = "txtAdvDeposit"
        Me.txtAdvDeposit.Size = New System.Drawing.Size(69, 20)
        Me.txtAdvDeposit.TabIndex = 523
        Me.txtAdvDeposit.Tag = ""
        '
        'lblId
        '
        Me.lblId.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblId.ForeColor = System.Drawing.Color.Black
        Me.lblId.Location = New System.Drawing.Point(227, 57)
        Me.lblId.Name = "lblId"
        Me.lblId.Size = New System.Drawing.Size(47, 22)
        Me.lblId.TabIndex = 522
        Me.lblId.Text = "Flexi ID"
        Me.lblId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnSubscriptionID
        '
        Me.btnSubscriptionID.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSubscriptionID.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSubscriptionID.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSubscriptionID.Location = New System.Drawing.Point(360, 59)
        Me.btnSubscriptionID.Name = "btnSubscriptionID"
        Me.btnSubscriptionID.Size = New System.Drawing.Size(24, 20)
        Me.btnSubscriptionID.TabIndex = 521
        Me.btnSubscriptionID.Text = "..."
        Me.btnSubscriptionID.UseVisualStyleBackColor = False
        '
        'txtSubscriptionID
        '
        Me.txtSubscriptionID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSubscriptionID.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubscriptionID.ForeColor = System.Drawing.Color.Black
        Me.txtSubscriptionID.Location = New System.Drawing.Point(275, 59)
        Me.txtSubscriptionID.MaxLength = 20
        Me.txtSubscriptionID.Name = "txtSubscriptionID"
        Me.txtSubscriptionID.Size = New System.Drawing.Size(99, 20)
        Me.txtSubscriptionID.TabIndex = 520
        Me.txtSubscriptionID.Tag = ""
        '
        'chkPrintImage
        '
        Me.chkPrintImage.AutoSize = True
        Me.chkPrintImage.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkPrintImage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPrintImage.Location = New System.Drawing.Point(430, 154)
        Me.chkPrintImage.Name = "chkPrintImage"
        Me.chkPrintImage.Size = New System.Drawing.Size(90, 17)
        Me.chkPrintImage.TabIndex = 519
        Me.chkPrintImage.Text = "Print Image"
        Me.chkPrintImage.UseVisualStyleBackColor = True
        '
        'chkPrintMC
        '
        Me.chkPrintMC.AutoSize = True
        Me.chkPrintMC.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkPrintMC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPrintMC.Location = New System.Drawing.Point(336, 155)
        Me.chkPrintMC.Name = "chkPrintMC"
        Me.chkPrintMC.Size = New System.Drawing.Size(74, 17)
        Me.chkPrintMC.TabIndex = 518
        Me.chkPrintMC.Text = "Print MC"
        Me.chkPrintMC.UseVisualStyleBackColor = True
        '
        'Label82
        '
        Me.Label82.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label82.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label82.ForeColor = System.Drawing.Color.Black
        Me.Label82.Location = New System.Drawing.Point(8, 78)
        Me.Label82.Name = "Label82"
        Me.Label82.Size = New System.Drawing.Size(80, 20)
        Me.Label82.TabIndex = 118
        Me.Label82.Text = "Name"
        Me.Label82.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnNewCustomer
        '
        Me.btnNewCustomer.BackColor = System.Drawing.Color.DarkOrange
        Me.btnNewCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNewCustomer.Location = New System.Drawing.Point(8, 131)
        Me.btnNewCustomer.Name = "btnNewCustomer"
        Me.btnNewCustomer.Size = New System.Drawing.Size(64, 24)
        Me.btnNewCustomer.TabIndex = 116
        Me.btnNewCustomer.Text = "New"
        Me.btnNewCustomer.UseVisualStyleBackColor = False
        '
        'lblCustomerType
        '
        Me.lblCustomerType.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblCustomerType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustomerType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerType.ForeColor = System.Drawing.Color.Red
        Me.lblCustomerType.Location = New System.Drawing.Point(408, 80)
        Me.lblCustomerType.Name = "lblCustomerType"
        Me.lblCustomerType.Size = New System.Drawing.Size(120, 20)
        Me.lblCustomerType.TabIndex = 115
        Me.lblCustomerType.Text = "Customer type"
        Me.lblCustomerType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.optInclusive)
        Me.Panel2.Controls.Add(Me.optZeroRated)
        Me.Panel2.Controls.Add(Me.optExclusive)
        Me.Panel2.Location = New System.Drawing.Point(232, 30)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(296, 24)
        Me.Panel2.TabIndex = 114
        '
        'optInclusive
        '
        Me.optInclusive.Checked = True
        Me.optInclusive.Enabled = False
        Me.optInclusive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optInclusive.ForeColor = System.Drawing.Color.Black
        Me.optInclusive.Location = New System.Drawing.Point(114, 0)
        Me.optInclusive.Name = "optInclusive"
        Me.optInclusive.Size = New System.Drawing.Size(75, 20)
        Me.optInclusive.TabIndex = 13
        Me.optInclusive.TabStop = True
        Me.optInclusive.Text = "Inclusive"
        '
        'optZeroRated
        '
        Me.optZeroRated.Enabled = False
        Me.optZeroRated.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optZeroRated.ForeColor = System.Drawing.Color.Black
        Me.optZeroRated.Location = New System.Drawing.Point(11, 0)
        Me.optZeroRated.Name = "optZeroRated"
        Me.optZeroRated.Size = New System.Drawing.Size(88, 20)
        Me.optZeroRated.TabIndex = 5
        Me.optZeroRated.Text = "Zero Rated"
        '
        'optExclusive
        '
        Me.optExclusive.Enabled = False
        Me.optExclusive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optExclusive.ForeColor = System.Drawing.Color.Black
        Me.optExclusive.Location = New System.Drawing.Point(204, 0)
        Me.optExclusive.Name = "optExclusive"
        Me.optExclusive.Size = New System.Drawing.Size(78, 20)
        Me.optExclusive.TabIndex = 3
        Me.optExclusive.Text = "Exclusive"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(8, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 20)
        Me.Label2.TabIndex = 64
        Me.Label2.Text = "Invoice Date"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSalesManName
        '
        Me.txtSalesManName.BackColor = System.Drawing.Color.White
        Me.txtSalesManName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesManName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesManName.ForeColor = System.Drawing.Color.Black
        Me.txtSalesManName.Location = New System.Drawing.Point(232, 6)
        Me.txtSalesManName.MaxLength = 20
        Me.txtSalesManName.Name = "txtSalesManName"
        Me.txtSalesManName.ReadOnly = True
        Me.txtSalesManName.Size = New System.Drawing.Size(296, 20)
        Me.txtSalesManName.TabIndex = 1
        Me.txtSalesManName.TabStop = False
        Me.txtSalesManName.Tag = ""
        '
        'txtSalesManPassword
        '
        Me.txtSalesManPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesManPassword.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesManPassword.ForeColor = System.Drawing.Color.Black
        Me.txtSalesManPassword.Location = New System.Drawing.Point(104, 5)
        Me.txtSalesManPassword.MaxLength = 20
        Me.txtSalesManPassword.Name = "txtSalesManPassword"
        Me.txtSalesManPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtSalesManPassword.Size = New System.Drawing.Size(120, 20)
        Me.txtSalesManPassword.TabIndex = 0
        Me.txtSalesManPassword.Tag = ""
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(8, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 20)
        Me.Label1.TabIndex = 112
        Me.Label1.Text = "Sales Man"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnCustomerCodeSearch
        '
        Me.btnCustomerCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCustomerCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCustomerCodeSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomerCodeSearch.Location = New System.Drawing.Point(200, 58)
        Me.btnCustomerCodeSearch.Name = "btnCustomerCodeSearch"
        Me.btnCustomerCodeSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnCustomerCodeSearch.TabIndex = 118
        Me.btnCustomerCodeSearch.Text = "..."
        Me.btnCustomerCodeSearch.UseVisualStyleBackColor = False
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(8, 98)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(82, 20)
        Me.Label13.TabIndex = 109
        Me.Label13.Text = "Address"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(8, 155)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(82, 20)
        Me.Label5.TabIndex = 108
        Me.Label5.Text = "PassPort No"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.Black
        Me.txtRemarks.Location = New System.Drawing.Point(104, 154)
        Me.txtRemarks.MaxLength = 50
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(223, 20)
        Me.txtRemarks.TabIndex = 9
        Me.txtRemarks.Tag = ""
        '
        'txtAddress3
        '
        Me.txtAddress3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress3.ForeColor = System.Drawing.Color.Black
        Me.txtAddress3.Location = New System.Drawing.Point(336, 131)
        Me.txtAddress3.MaxLength = 20
        Me.txtAddress3.Name = "txtAddress3"
        Me.txtAddress3.Size = New System.Drawing.Size(192, 20)
        Me.txtAddress3.TabIndex = 106
        Me.txtAddress3.Tag = ""
        '
        'txtAddress2
        '
        Me.txtAddress2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress2.ForeColor = System.Drawing.Color.Black
        Me.txtAddress2.Location = New System.Drawing.Point(104, 130)
        Me.txtAddress2.MaxLength = 20
        Me.txtAddress2.Name = "txtAddress2"
        Me.txtAddress2.Size = New System.Drawing.Size(224, 20)
        Me.txtAddress2.TabIndex = 105
        Me.txtAddress2.Tag = ""
        '
        'txtAddress1
        '
        Me.txtAddress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress1.ForeColor = System.Drawing.Color.Black
        Me.txtAddress1.Location = New System.Drawing.Point(104, 106)
        Me.txtAddress1.MaxLength = 20
        Me.txtAddress1.Name = "txtAddress1"
        Me.txtAddress1.Size = New System.Drawing.Size(291, 20)
        Me.txtAddress1.TabIndex = 104
        Me.txtAddress1.Tag = ""
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerCode.ForeColor = System.Drawing.Color.Black
        Me.txtCustomerCode.Location = New System.Drawing.Point(104, 58)
        Me.txtCustomerCode.MaxLength = 20
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.Size = New System.Drawing.Size(96, 20)
        Me.txtCustomerCode.TabIndex = 6
        Me.txtCustomerCode.Tag = ""
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(8, 58)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 20)
        Me.Label6.TabIndex = 102
        Me.Label6.Text = "Customer "
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(158, 33)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(8, 16)
        Me.Label3.TabIndex = 62
        Me.Label3.Text = "/"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(126, 33)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(8, 16)
        Me.Label4.TabIndex = 61
        Me.Label4.Text = "/"
        '
        'txtIYYYY
        '
        Me.txtIYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIYYYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIYYYY.Location = New System.Drawing.Point(168, 32)
        Me.txtIYYYY.MaxLength = 4
        Me.txtIYYYY.Name = "txtIYYYY"
        Me.txtIYYYY.ReadOnly = True
        Me.txtIYYYY.Size = New System.Drawing.Size(32, 20)
        Me.txtIYYYY.TabIndex = 4
        Me.txtIYYYY.TabStop = False
        Me.txtIYYYY.Tag = "ENN;"
        '
        'txtIMM
        '
        Me.txtIMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIMM.Location = New System.Drawing.Point(136, 32)
        Me.txtIMM.MaxLength = 2
        Me.txtIMM.Name = "txtIMM"
        Me.txtIMM.ReadOnly = True
        Me.txtIMM.Size = New System.Drawing.Size(20, 20)
        Me.txtIMM.TabIndex = 3
        Me.txtIMM.TabStop = False
        Me.txtIMM.Tag = "ENN;"
        '
        'txtIDD
        '
        Me.txtIDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIDD.Location = New System.Drawing.Point(104, 32)
        Me.txtIDD.MaxLength = 2
        Me.txtIDD.Name = "txtIDD"
        Me.txtIDD.ReadOnly = True
        Me.txtIDD.Size = New System.Drawing.Size(20, 20)
        Me.txtIDD.TabIndex = 2
        Me.txtIDD.TabStop = False
        Me.txtIDD.Tag = "ENN;"
        '
        'txtMemberCode
        '
        Me.txtMemberCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMemberCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMemberCode.Location = New System.Drawing.Point(232, 56)
        Me.txtMemberCode.Name = "txtMemberCode"
        Me.txtMemberCode.Size = New System.Drawing.Size(160, 20)
        Me.txtMemberCode.TabIndex = 117
        Me.txtMemberCode.Visible = False
        '
        'txtCustomerName
        '
        Me.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerName.ForeColor = System.Drawing.Color.Black
        Me.txtCustomerName.Location = New System.Drawing.Point(104, 82)
        Me.txtCustomerName.MaxLength = 20
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(288, 20)
        Me.txtCustomerName.TabIndex = 8
        Me.txtCustomerName.Tag = ""
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.txtGST)
        Me.Panel3.Controls.Add(Me.Label7)
        Me.Panel3.Controls.Add(Me.txtTotalValue)
        Me.Panel3.Controls.Add(Me.Label30)
        Me.Panel3.Controls.Add(Me.txtPCS)
        Me.Panel3.Controls.Add(Me.Label27)
        Me.Panel3.Controls.Add(Me.txtSMC)
        Me.Panel3.Controls.Add(Me.Label28)
        Me.Panel3.Controls.Add(Me.txtCostPerGram)
        Me.Panel3.Controls.Add(Me.Label24)
        Me.Panel3.Controls.Add(Me.txtGoldLoss)
        Me.Panel3.Controls.Add(Me.Label25)
        Me.Panel3.Controls.Add(Me.txtWeight)
        Me.Panel3.Controls.Add(Me.Label26)
        Me.Panel3.Controls.Add(Me.txtInventoryCode)
        Me.Panel3.Controls.Add(Me.Label22)
        Me.Panel3.Controls.Add(Me.txtInventoryDescription)
        Me.Panel3.Controls.Add(Me.Label23)
        Me.Panel3.Location = New System.Drawing.Point(96, 568)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(920, 51)
        Me.Panel3.TabIndex = 115
        '
        'txtGST
        '
        Me.txtGST.BackColor = System.Drawing.Color.White
        Me.txtGST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGST.Enabled = False
        Me.txtGST.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGST.ForeColor = System.Drawing.Color.Black
        Me.txtGST.Location = New System.Drawing.Point(744, 27)
        Me.txtGST.MaxLength = 20
        Me.txtGST.Name = "txtGST"
        Me.txtGST.Size = New System.Drawing.Size(72, 22)
        Me.txtGST.TabIndex = 17
        Me.txtGST.TabStop = False
        Me.txtGST.Tag = ""
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.White
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Navy
        Me.Label7.Location = New System.Drawing.Point(744, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 27)
        Me.Label7.TabIndex = 158
        Me.Label7.Text = "GST"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTotalValue
        '
        Me.txtTotalValue.BackColor = System.Drawing.Color.White
        Me.txtTotalValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalValue.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalValue.ForeColor = System.Drawing.Color.Black
        Me.txtTotalValue.HideSelection = False
        Me.txtTotalValue.Location = New System.Drawing.Point(816, 27)
        Me.txtTotalValue.MaxLength = 20
        Me.txtTotalValue.Name = "txtTotalValue"
        Me.txtTotalValue.Size = New System.Drawing.Size(102, 22)
        Me.txtTotalValue.TabIndex = 18
        Me.txtTotalValue.TabStop = False
        Me.txtTotalValue.Tag = ""
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.White
        Me.Label30.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.Color.Navy
        Me.Label30.Location = New System.Drawing.Point(816, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(102, 27)
        Me.Label30.TabIndex = 156
        Me.Label30.Text = "Total Value"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtPCS
        '
        Me.txtPCS.BackColor = System.Drawing.Color.White
        Me.txtPCS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPCS.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPCS.ForeColor = System.Drawing.Color.Black
        Me.txtPCS.Location = New System.Drawing.Point(616, 27)
        Me.txtPCS.MaxLength = 20
        Me.txtPCS.Name = "txtPCS"
        Me.txtPCS.Size = New System.Drawing.Size(56, 22)
        Me.txtPCS.TabIndex = 15
        Me.txtPCS.Tag = ""
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.White
        Me.Label27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label27.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.Color.Navy
        Me.Label27.Location = New System.Drawing.Point(616, 0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(56, 27)
        Me.Label27.TabIndex = 155
        Me.Label27.Text = "PCS"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtSMC
        '
        Me.txtSMC.BackColor = System.Drawing.Color.White
        Me.txtSMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSMC.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMC.ForeColor = System.Drawing.Color.Black
        Me.txtSMC.Location = New System.Drawing.Point(672, 27)
        Me.txtSMC.MaxLength = 20
        Me.txtSMC.Name = "txtSMC"
        Me.txtSMC.ReadOnly = True
        Me.txtSMC.Size = New System.Drawing.Size(72, 22)
        Me.txtSMC.TabIndex = 16
        Me.txtSMC.Tag = ""
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.White
        Me.Label28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label28.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.Color.Navy
        Me.Label28.Location = New System.Drawing.Point(672, 0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(72, 27)
        Me.Label28.TabIndex = 154
        Me.Label28.Text = "SMC"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCostPerGram
        '
        Me.txtCostPerGram.BackColor = System.Drawing.Color.White
        Me.txtCostPerGram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCostPerGram.Enabled = False
        Me.txtCostPerGram.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostPerGram.ForeColor = System.Drawing.Color.Black
        Me.txtCostPerGram.Location = New System.Drawing.Point(544, 27)
        Me.txtCostPerGram.MaxLength = 20
        Me.txtCostPerGram.Name = "txtCostPerGram"
        Me.txtCostPerGram.Size = New System.Drawing.Size(72, 22)
        Me.txtCostPerGram.TabIndex = 14
        Me.txtCostPerGram.Tag = ""
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.White
        Me.Label24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label24.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Navy
        Me.Label24.Location = New System.Drawing.Point(544, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(72, 27)
        Me.Label24.TabIndex = 146
        Me.Label24.Text = "Cost/Gram"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtGoldLoss
        '
        Me.txtGoldLoss.BackColor = System.Drawing.Color.White
        Me.txtGoldLoss.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldLoss.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoldLoss.ForeColor = System.Drawing.Color.Black
        Me.txtGoldLoss.Location = New System.Drawing.Point(472, 27)
        Me.txtGoldLoss.MaxLength = 20
        Me.txtGoldLoss.Name = "txtGoldLoss"
        Me.txtGoldLoss.ReadOnly = True
        Me.txtGoldLoss.Size = New System.Drawing.Size(72, 22)
        Me.txtGoldLoss.TabIndex = 13
        Me.txtGoldLoss.Tag = ""
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.White
        Me.Label25.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label25.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.Navy
        Me.Label25.Location = New System.Drawing.Point(472, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(72, 27)
        Me.Label25.TabIndex = 145
        Me.Label25.Text = "Gold Loss"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtWeight
        '
        Me.txtWeight.BackColor = System.Drawing.Color.White
        Me.txtWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWeight.Enabled = False
        Me.txtWeight.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeight.ForeColor = System.Drawing.Color.Black
        Me.txtWeight.Location = New System.Drawing.Point(400, 27)
        Me.txtWeight.MaxLength = 20
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.Size = New System.Drawing.Size(72, 22)
        Me.txtWeight.TabIndex = 12
        Me.txtWeight.Tag = ""
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.White
        Me.Label26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label26.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.Navy
        Me.Label26.Location = New System.Drawing.Point(400, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(72, 27)
        Me.Label26.TabIndex = 144
        Me.Label26.Text = "Weight"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtInventoryCode
        '
        Me.txtInventoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInventoryCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtInventoryCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInventoryCode.ForeColor = System.Drawing.Color.Black
        Me.txtInventoryCode.Location = New System.Drawing.Point(0, 27)
        Me.txtInventoryCode.MaxLength = 20
        Me.txtInventoryCode.Name = "txtInventoryCode"
        Me.txtInventoryCode.Size = New System.Drawing.Size(104, 22)
        Me.txtInventoryCode.TabIndex = 10
        Me.txtInventoryCode.Tag = ""
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.White
        Me.Label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label22.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.Navy
        Me.Label22.Location = New System.Drawing.Point(0, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(104, 27)
        Me.Label22.TabIndex = 138
        Me.Label22.Text = "Inventory Code"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtInventoryDescription
        '
        Me.txtInventoryDescription.BackColor = System.Drawing.Color.White
        Me.txtInventoryDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInventoryDescription.Enabled = False
        Me.txtInventoryDescription.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInventoryDescription.ForeColor = System.Drawing.Color.Black
        Me.txtInventoryDescription.Location = New System.Drawing.Point(104, 27)
        Me.txtInventoryDescription.MaxLength = 20
        Me.txtInventoryDescription.Name = "txtInventoryDescription"
        Me.txtInventoryDescription.Size = New System.Drawing.Size(296, 22)
        Me.txtInventoryDescription.TabIndex = 11
        Me.txtInventoryDescription.Tag = ""
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.White
        Me.Label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label23.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.Navy
        Me.Label23.Location = New System.Drawing.Point(104, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(296, 27)
        Me.Label23.TabIndex = 136
        Me.Label23.Text = "Inventory Description"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Label32)
        Me.Panel4.Controls.Add(Me.lblTax)
        Me.Panel4.Controls.Add(Me.lblNetValue)
        Me.Panel4.Controls.Add(Me.lblNetDiscount)
        Me.Panel4.Controls.Add(Me.lblSalesAmount)
        Me.Panel4.Controls.Add(Me.lblQty)
        Me.Panel4.Controls.Add(Me.Label18)
        Me.Panel4.Controls.Add(Me.Label17)
        Me.Panel4.Controls.Add(Me.Label16)
        Me.Panel4.Controls.Add(Me.Label15)
        Me.Panel4.Controls.Add(Me.Label14)
        Me.Panel4.Controls.Add(Me.Label12)
        Me.Panel4.Controls.Add(Me.Label11)
        Me.Panel4.Controls.Add(Me.Label9)
        Me.Panel4.Controls.Add(Me.Label8)
        Me.Panel4.Controls.Add(Me.lblNetTotal)
        Me.Panel4.Controls.Add(Me.Label10)
        Me.Panel4.Controls.Add(Me.Label65)
        Me.Panel4.Controls.Add(Me.Label66)
        Me.Panel4.Controls.Add(Me.Label67)
        Me.Panel4.Location = New System.Drawing.Point(840, 256)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(176, 304)
        Me.Panel4.TabIndex = 116
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.Navy
        Me.Label32.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label32.Font = New System.Drawing.Font("Arial", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.Color.White
        Me.Label32.Location = New System.Drawing.Point(0, 200)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(174, 56)
        Me.Label32.TabIndex = 92
        Me.Label32.Text = "Net Total"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTax
        '
        Me.lblTax.BackColor = System.Drawing.Color.Yellow
        Me.lblTax.Font = New System.Drawing.Font("Arial", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTax.ForeColor = System.Drawing.Color.Navy
        Me.lblTax.Location = New System.Drawing.Point(80, 165)
        Me.lblTax.Name = "lblTax"
        Me.lblTax.Size = New System.Drawing.Size(88, 25)
        Me.lblTax.TabIndex = 91
        Me.lblTax.Text = "0.00"
        Me.lblTax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNetValue
        '
        Me.lblNetValue.BackColor = System.Drawing.Color.Yellow
        Me.lblNetValue.Font = New System.Drawing.Font("Arial", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetValue.ForeColor = System.Drawing.Color.Navy
        Me.lblNetValue.Location = New System.Drawing.Point(80, 127)
        Me.lblNetValue.Name = "lblNetValue"
        Me.lblNetValue.Size = New System.Drawing.Size(88, 25)
        Me.lblNetValue.TabIndex = 90
        Me.lblNetValue.Text = "0.00"
        Me.lblNetValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNetDiscount
        '
        Me.lblNetDiscount.BackColor = System.Drawing.Color.Yellow
        Me.lblNetDiscount.Font = New System.Drawing.Font("Arial", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetDiscount.ForeColor = System.Drawing.Color.Navy
        Me.lblNetDiscount.Location = New System.Drawing.Point(80, 88)
        Me.lblNetDiscount.Name = "lblNetDiscount"
        Me.lblNetDiscount.Size = New System.Drawing.Size(88, 25)
        Me.lblNetDiscount.TabIndex = 89
        Me.lblNetDiscount.Text = "0.00"
        Me.lblNetDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSalesAmount
        '
        Me.lblSalesAmount.BackColor = System.Drawing.Color.Yellow
        Me.lblSalesAmount.Font = New System.Drawing.Font("Arial", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesAmount.ForeColor = System.Drawing.Color.Navy
        Me.lblSalesAmount.Location = New System.Drawing.Point(80, 48)
        Me.lblSalesAmount.Name = "lblSalesAmount"
        Me.lblSalesAmount.Size = New System.Drawing.Size(88, 24)
        Me.lblSalesAmount.TabIndex = 88
        Me.lblSalesAmount.Text = "0.00"
        Me.lblSalesAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblQty
        '
        Me.lblQty.BackColor = System.Drawing.Color.Yellow
        Me.lblQty.Font = New System.Drawing.Font("Arial", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQty.ForeColor = System.Drawing.Color.Navy
        Me.lblQty.Location = New System.Drawing.Point(80, 8)
        Me.lblQty.Name = "lblQty"
        Me.lblQty.Size = New System.Drawing.Size(88, 25)
        Me.lblQty.TabIndex = 87
        Me.lblQty.Text = "0.00"
        Me.lblQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.Yellow
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Navy
        Me.Label18.Location = New System.Drawing.Point(2, 165)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(70, 25)
        Me.Label18.TabIndex = 86
        Me.Label18.Text = "Tax"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.Yellow
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Navy
        Me.Label17.Location = New System.Drawing.Point(2, 88)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(70, 25)
        Me.Label17.TabIndex = 85
        Me.Label17.Text = "Net Discount"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Yellow
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Navy
        Me.Label16.Location = New System.Drawing.Point(2, 127)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(70, 25)
        Me.Label16.TabIndex = 84
        Me.Label16.Text = "Net Value"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Yellow
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Navy
        Me.Label15.Location = New System.Drawing.Point(2, 48)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(70, 24)
        Me.Label15.TabIndex = 83
        Me.Label15.Text = "Sales Amount"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Yellow
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Navy
        Me.Label14.Location = New System.Drawing.Point(2, 8)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(70, 25)
        Me.Label14.TabIndex = 82
        Me.Label14.Text = "Qty"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Yellow
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label12.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(0, 160)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(174, 40)
        Me.Label12.TabIndex = 81
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Yellow
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(0, 120)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(174, 40)
        Me.Label11.TabIndex = 80
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Yellow
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(0, 40)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(174, 40)
        Me.Label9.TabIndex = 79
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Yellow
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(0, 80)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(174, 40)
        Me.Label8.TabIndex = 78
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblNetTotal
        '
        Me.lblNetTotal.BackColor = System.Drawing.Color.Yellow
        Me.lblNetTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNetTotal.Font = New System.Drawing.Font("Arial", 15.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetTotal.ForeColor = System.Drawing.Color.Red
        Me.lblNetTotal.Location = New System.Drawing.Point(0, 256)
        Me.lblNetTotal.Name = "lblNetTotal"
        Me.lblNetTotal.Size = New System.Drawing.Size(174, 48)
        Me.lblNetTotal.TabIndex = 77
        Me.lblNetTotal.Text = "0000000.00"
        Me.lblNetTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Yellow
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(0, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(174, 40)
        Me.Label10.TabIndex = 76
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label65
        '
        Me.Label65.BackColor = System.Drawing.Color.Yellow
        Me.Label65.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label65.ForeColor = System.Drawing.Color.White
        Me.Label65.Location = New System.Drawing.Point(16, 128)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(80, 25)
        Me.Label65.TabIndex = 84
        Me.Label65.Text = "Net Value"
        Me.Label65.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label66
        '
        Me.Label66.BackColor = System.Drawing.Color.Yellow
        Me.Label66.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label66.ForeColor = System.Drawing.Color.White
        Me.Label66.Location = New System.Drawing.Point(16, 48)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(104, 24)
        Me.Label66.TabIndex = 83
        Me.Label66.Text = "Sales Amount"
        Me.Label66.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label67
        '
        Me.Label67.BackColor = System.Drawing.Color.Yellow
        Me.Label67.Font = New System.Drawing.Font("Arial", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label67.ForeColor = System.Drawing.Color.White
        Me.Label67.Location = New System.Drawing.Point(96, 128)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(80, 25)
        Me.Label67.TabIndex = 90
        Me.Label67.Text = "0.00"
        Me.Label67.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel5
        '
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.lblTerminal)
        Me.Panel5.Controls.Add(Me.lblReturnAmount)
        Me.Panel5.Controls.Add(Me.Label40)
        Me.Panel5.Controls.Add(Me.Label37)
        Me.Panel5.Controls.Add(Me.Label38)
        Me.Panel5.Controls.Add(Me.lblSManTotal)
        Me.Panel5.Controls.Add(Me.lblSManTotalCap)
        Me.Panel5.Controls.Add(Me.lblNextInvoiceNo)
        Me.Panel5.Controls.Add(Me.lblNextInvoiceNoCap)
        Me.Panel5.Controls.Add(Me.lblLastBillAmount)
        Me.Panel5.Controls.Add(Me.Label36)
        Me.Panel5.Controls.Add(Me.lblLastBillNo)
        Me.Panel5.Controls.Add(Me.Label34)
        Me.Panel5.Controls.Add(Me.lblLastSalesMan)
        Me.Panel5.Controls.Add(Me.Label31)
        Me.Panel5.Controls.Add(Me.Label21)
        Me.Panel5.Controls.Add(Me.lblLocation)
        Me.Panel5.Controls.Add(Me.Label20)
        Me.Panel5.Controls.Add(Me.txtStoneWeight)
        Me.Panel5.Location = New System.Drawing.Point(96, 616)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(920, 50)
        Me.Panel5.TabIndex = 120
        '
        'lblTerminal
        '
        Me.lblTerminal.BackColor = System.Drawing.Color.Navy
        Me.lblTerminal.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTerminal.ForeColor = System.Drawing.Color.White
        Me.lblTerminal.Location = New System.Drawing.Point(96, 24)
        Me.lblTerminal.Name = "lblTerminal"
        Me.lblTerminal.Size = New System.Drawing.Size(64, 24)
        Me.lblTerminal.TabIndex = 123
        Me.lblTerminal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblReturnAmount
        '
        Me.lblReturnAmount.BackColor = System.Drawing.Color.Navy
        Me.lblReturnAmount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReturnAmount.ForeColor = System.Drawing.Color.White
        Me.lblReturnAmount.Location = New System.Drawing.Point(768, 24)
        Me.lblReturnAmount.Name = "lblReturnAmount"
        Me.lblReturnAmount.Size = New System.Drawing.Size(152, 24)
        Me.lblReturnAmount.TabIndex = 137
        Me.lblReturnAmount.Text = "0.00"
        Me.lblReturnAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.Yellow
        Me.Label40.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.ForeColor = System.Drawing.Color.Black
        Me.Label40.Location = New System.Drawing.Point(768, 0)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(152, 24)
        Me.Label40.TabIndex = 136
        Me.Label40.Text = "Return Amount"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.Navy
        Me.Label37.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.Color.White
        Me.Label37.Location = New System.Drawing.Point(728, 24)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(40, 24)
        Me.Label37.TabIndex = 135
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.Yellow
        Me.Label38.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.Color.Black
        Me.Label38.Location = New System.Drawing.Point(728, 0)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(40, 24)
        Me.Label38.TabIndex = 134
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSManTotal
        '
        Me.lblSManTotal.BackColor = System.Drawing.Color.Navy
        Me.lblSManTotal.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSManTotal.ForeColor = System.Drawing.Color.White
        Me.lblSManTotal.Location = New System.Drawing.Point(608, 24)
        Me.lblSManTotal.Name = "lblSManTotal"
        Me.lblSManTotal.Size = New System.Drawing.Size(120, 24)
        Me.lblSManTotal.TabIndex = 133
        Me.lblSManTotal.Text = "0.00"
        Me.lblSManTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSManTotalCap
        '
        Me.lblSManTotalCap.BackColor = System.Drawing.Color.Yellow
        Me.lblSManTotalCap.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSManTotalCap.ForeColor = System.Drawing.Color.Black
        Me.lblSManTotalCap.Location = New System.Drawing.Point(608, 0)
        Me.lblSManTotalCap.Name = "lblSManTotalCap"
        Me.lblSManTotalCap.Size = New System.Drawing.Size(120, 24)
        Me.lblSManTotalCap.TabIndex = 132
        Me.lblSManTotalCap.Text = "Total Sales Amt"
        Me.lblSManTotalCap.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblNextInvoiceNo
        '
        Me.lblNextInvoiceNo.BackColor = System.Drawing.Color.Navy
        Me.lblNextInvoiceNo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNextInvoiceNo.ForeColor = System.Drawing.Color.White
        Me.lblNextInvoiceNo.Location = New System.Drawing.Point(480, 24)
        Me.lblNextInvoiceNo.Name = "lblNextInvoiceNo"
        Me.lblNextInvoiceNo.Size = New System.Drawing.Size(128, 24)
        Me.lblNextInvoiceNo.TabIndex = 131
        Me.lblNextInvoiceNo.Text = "0"
        Me.lblNextInvoiceNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblNextInvoiceNoCap
        '
        Me.lblNextInvoiceNoCap.BackColor = System.Drawing.Color.Yellow
        Me.lblNextInvoiceNoCap.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNextInvoiceNoCap.ForeColor = System.Drawing.Color.Black
        Me.lblNextInvoiceNoCap.Location = New System.Drawing.Point(480, 0)
        Me.lblNextInvoiceNoCap.Name = "lblNextInvoiceNoCap"
        Me.lblNextInvoiceNoCap.Size = New System.Drawing.Size(128, 24)
        Me.lblNextInvoiceNoCap.TabIndex = 130
        Me.lblNextInvoiceNoCap.Text = "Next Invoice No"
        Me.lblNextInvoiceNoCap.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLastBillAmount
        '
        Me.lblLastBillAmount.BackColor = System.Drawing.Color.Navy
        Me.lblLastBillAmount.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastBillAmount.ForeColor = System.Drawing.Color.White
        Me.lblLastBillAmount.Location = New System.Drawing.Point(384, 24)
        Me.lblLastBillAmount.Name = "lblLastBillAmount"
        Me.lblLastBillAmount.Size = New System.Drawing.Size(96, 24)
        Me.lblLastBillAmount.TabIndex = 129
        Me.lblLastBillAmount.Text = "0"
        Me.lblLastBillAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.Yellow
        Me.Label36.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.Color.Black
        Me.Label36.Location = New System.Drawing.Point(384, 0)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(96, 24)
        Me.Label36.TabIndex = 128
        Me.Label36.Text = "Last Bill Amt"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLastBillNo
        '
        Me.lblLastBillNo.BackColor = System.Drawing.Color.Navy
        Me.lblLastBillNo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastBillNo.ForeColor = System.Drawing.Color.White
        Me.lblLastBillNo.Location = New System.Drawing.Point(288, 24)
        Me.lblLastBillNo.Name = "lblLastBillNo"
        Me.lblLastBillNo.Size = New System.Drawing.Size(96, 24)
        Me.lblLastBillNo.TabIndex = 127
        Me.lblLastBillNo.Text = "0"
        Me.lblLastBillNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.Yellow
        Me.Label34.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.Color.Black
        Me.Label34.Location = New System.Drawing.Point(288, 0)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(96, 24)
        Me.Label34.TabIndex = 126
        Me.Label34.Text = "Last Bill No"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLastSalesMan
        '
        Me.lblLastSalesMan.BackColor = System.Drawing.Color.Navy
        Me.lblLastSalesMan.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastSalesMan.ForeColor = System.Drawing.Color.White
        Me.lblLastSalesMan.Location = New System.Drawing.Point(160, 24)
        Me.lblLastSalesMan.Name = "lblLastSalesMan"
        Me.lblLastSalesMan.Size = New System.Drawing.Size(152, 24)
        Me.lblLastSalesMan.TabIndex = 125
        Me.lblLastSalesMan.Text = "0"
        Me.lblLastSalesMan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.Yellow
        Me.Label31.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.Color.Black
        Me.Label31.Location = New System.Drawing.Point(176, 0)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(112, 24)
        Me.Label31.TabIndex = 124
        Me.Label31.Text = "Last SalesMan"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.Yellow
        Me.Label21.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Black
        Me.Label21.Location = New System.Drawing.Point(80, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(96, 24)
        Me.Label21.TabIndex = 122
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLocation
        '
        Me.lblLocation.BackColor = System.Drawing.Color.Navy
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.ForeColor = System.Drawing.Color.White
        Me.lblLocation.Location = New System.Drawing.Point(0, 24)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(96, 24)
        Me.lblLocation.TabIndex = 121
        Me.lblLocation.Text = "HQ"
        Me.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.Yellow
        Me.Label20.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.Location = New System.Drawing.Point(0, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(96, 24)
        Me.Label20.TabIndex = 120
        Me.Label20.Text = "Location"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtStoneWeight
        '
        Me.txtStoneWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStoneWeight.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStoneWeight.ForeColor = System.Drawing.Color.Black
        Me.txtStoneWeight.Location = New System.Drawing.Point(488, 24)
        Me.txtStoneWeight.MaxLength = 20
        Me.txtStoneWeight.Name = "txtStoneWeight"
        Me.txtStoneWeight.Size = New System.Drawing.Size(72, 22)
        Me.txtStoneWeight.TabIndex = 142
        Me.txtStoneWeight.Tag = ""
        Me.txtStoneWeight.Visible = False
        '
        'Panel6
        '
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Controls.Add(Me.Label45)
        Me.Panel6.Controls.Add(Me.Label44)
        Me.Panel6.Controls.Add(Me.Label43)
        Me.Panel6.Controls.Add(Me.Label42)
        Me.Panel6.Controls.Add(Me.Label41)
        Me.Panel6.Controls.Add(Me.Label39)
        Me.Panel6.Controls.Add(Me.lblSalesType)
        Me.Panel6.Location = New System.Drawing.Point(840, 72)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(176, 184)
        Me.Panel6.TabIndex = 121
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label45.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label45.Font = New System.Drawing.Font("Arial", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.Color.White
        Me.Label45.Location = New System.Drawing.Point(0, 158)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(174, 24)
        Me.Label45.TabIndex = 127
        Me.Label45.Text = "Close (Esc)"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label44.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label44.Font = New System.Drawing.Font("Arial", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.Color.White
        Me.Label44.Location = New System.Drawing.Point(0, 134)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(174, 24)
        Me.Label44.TabIndex = 126
        Me.Label44.Text = "Net Total (F5)"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label43.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label43.Font = New System.Drawing.Font("Arial", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.Color.White
        Me.Label43.Location = New System.Drawing.Point(0, 110)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(174, 24)
        Me.Label43.TabIndex = 125
        Me.Label43.Text = "Bill Cancel (F3)"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label42.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label42.Font = New System.Drawing.Font("Arial", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.Color.White
        Me.Label42.Location = New System.Drawing.Point(0, 86)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(174, 24)
        Me.Label42.TabIndex = 124
        Me.Label42.Text = "RePrint (F6)"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label41.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label41.Font = New System.Drawing.Font("Arial", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.ForeColor = System.Drawing.Color.White
        Me.Label41.Location = New System.Drawing.Point(0, 62)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(174, 24)
        Me.Label41.TabIndex = 123
        Me.Label41.Text = "Save (F4)"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label39.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label39.Font = New System.Drawing.Font("Arial", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.Color.White
        Me.Label39.Location = New System.Drawing.Point(0, 38)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(174, 24)
        Me.Label39.TabIndex = 122
        Me.Label39.Text = "Clear (F2)"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSalesType
        '
        Me.lblSalesType.BackColor = System.Drawing.Color.Yellow
        Me.lblSalesType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSalesType.Font = New System.Drawing.Font("Arial", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesType.ForeColor = System.Drawing.Color.Black
        Me.lblSalesType.Location = New System.Drawing.Point(0, 0)
        Me.lblSalesType.Name = "lblSalesType"
        Me.lblSalesType.Size = New System.Drawing.Size(174, 38)
        Me.lblSalesType.TabIndex = 121
        Me.lblSalesType.Text = "INCLUSIVE SALES"
        Me.lblSalesType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PnlReprint
        '
        Me.PnlReprint.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.PnlReprint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PnlReprint.Controls.Add(Me.btnReprintCancel)
        Me.PnlReprint.Controls.Add(Me.btnReprintOK)
        Me.PnlReprint.Controls.Add(Me.txtRePrintBillNo)
        Me.PnlReprint.Controls.Add(Me.Label47)
        Me.PnlReprint.Controls.Add(Me.Label48)
        Me.PnlReprint.Location = New System.Drawing.Point(432, 341)
        Me.PnlReprint.Name = "PnlReprint"
        Me.PnlReprint.Size = New System.Drawing.Size(248, 88)
        Me.PnlReprint.TabIndex = 123
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
        Me.btnReprintCancel.TabIndex = 76
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
        Me.btnReprintOK.TabIndex = 75
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
        'Label47
        '
        Me.Label47.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.ForeColor = System.Drawing.Color.Black
        Me.Label47.Location = New System.Drawing.Point(32, 33)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(56, 15)
        Me.Label47.TabIndex = 74
        Me.Label47.Text = "Bill No"
        '
        'Label48
        '
        Me.Label48.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.ForeColor = System.Drawing.Color.Black
        Me.Label48.Location = New System.Drawing.Point(8, 8)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(82, 15)
        Me.Label48.TabIndex = 72
        Me.Label48.Text = "RePrint"
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlBillCancel
        '
        Me.pnlBillCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.pnlBillCancel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlBillCancel.Controls.Add(Me.btnBillCancel)
        Me.pnlBillCancel.Controls.Add(Me.btnBillCancelOK)
        Me.pnlBillCancel.Controls.Add(Me.txtBillCancelBillNo)
        Me.pnlBillCancel.Controls.Add(Me.Label49)
        Me.pnlBillCancel.Controls.Add(Me.Label50)
        Me.pnlBillCancel.Location = New System.Drawing.Point(416, 296)
        Me.pnlBillCancel.Name = "pnlBillCancel"
        Me.pnlBillCancel.Size = New System.Drawing.Size(278, 88)
        Me.pnlBillCancel.TabIndex = 124
        Me.pnlBillCancel.Visible = False
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
        'Label49
        '
        Me.Label49.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.ForeColor = System.Drawing.Color.Black
        Me.Label49.Location = New System.Drawing.Point(56, 34)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(56, 15)
        Me.Label49.TabIndex = 74
        Me.Label49.Text = "Bill No"
        '
        'Label50
        '
        Me.Label50.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.ForeColor = System.Drawing.Color.Black
        Me.Label50.Location = New System.Drawing.Point(8, 8)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(72, 15)
        Me.Label50.TabIndex = 72
        Me.Label50.Text = "Bill Cancel"
        Me.Label50.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlAdminPwd
        '
        Me.pnlAdminPwd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.pnlAdminPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlAdminPwd.Controls.Add(Me.btnAdminPwdCancel)
        Me.pnlAdminPwd.Controls.Add(Me.btnAdminPwdOK)
        Me.pnlAdminPwd.Controls.Add(Me.txtAdminPassword)
        Me.pnlAdminPwd.Controls.Add(Me.Label51)
        Me.pnlAdminPwd.Controls.Add(Me.Label52)
        Me.pnlAdminPwd.Location = New System.Drawing.Point(424, 351)
        Me.pnlAdminPwd.Name = "pnlAdminPwd"
        Me.pnlAdminPwd.Size = New System.Drawing.Size(246, 88)
        Me.pnlAdminPwd.TabIndex = 125
        Me.pnlAdminPwd.Visible = False
        '
        'btnAdminPwdCancel
        '
        Me.btnAdminPwdCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnAdminPwdCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdminPwdCancel.Image = CType(resources.GetObject("btnAdminPwdCancel.Image"), System.Drawing.Image)
        Me.btnAdminPwdCancel.Location = New System.Drawing.Point(160, 56)
        Me.btnAdminPwdCancel.Name = "btnAdminPwdCancel"
        Me.btnAdminPwdCancel.Size = New System.Drawing.Size(64, 23)
        Me.btnAdminPwdCancel.TabIndex = 75
        Me.btnAdminPwdCancel.Text = "Cancel"
        Me.btnAdminPwdCancel.UseVisualStyleBackColor = False
        '
        'btnAdminPwdOK
        '
        Me.btnAdminPwdOK.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnAdminPwdOK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdminPwdOK.Image = CType(resources.GetObject("btnAdminPwdOK.Image"), System.Drawing.Image)
        Me.btnAdminPwdOK.Location = New System.Drawing.Point(88, 56)
        Me.btnAdminPwdOK.Name = "btnAdminPwdOK"
        Me.btnAdminPwdOK.Size = New System.Drawing.Size(64, 23)
        Me.btnAdminPwdOK.TabIndex = 74
        Me.btnAdminPwdOK.Text = "OK"
        Me.btnAdminPwdOK.UseVisualStyleBackColor = False
        '
        'txtAdminPassword
        '
        Me.txtAdminPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdminPassword.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdminPassword.ForeColor = System.Drawing.Color.Black
        Me.txtAdminPassword.Location = New System.Drawing.Point(88, 32)
        Me.txtAdminPassword.MaxLength = 20
        Me.txtAdminPassword.Name = "txtAdminPassword"
        Me.txtAdminPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtAdminPassword.Size = New System.Drawing.Size(136, 20)
        Me.txtAdminPassword.TabIndex = 73
        Me.txtAdminPassword.Tag = ""
        '
        'Label51
        '
        Me.Label51.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.ForeColor = System.Drawing.Color.Black
        Me.Label51.Location = New System.Drawing.Point(16, 33)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(64, 15)
        Me.Label51.TabIndex = 72
        Me.Label51.Text = "Password"
        '
        'Label52
        '
        Me.Label52.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.ForeColor = System.Drawing.Color.Black
        Me.Label52.Location = New System.Drawing.Point(8, 8)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(56, 15)
        Me.Label52.TabIndex = 72
        Me.Label52.Text = "Admin"
        Me.Label52.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlPaymentType
        '
        Me.pnlPaymentType.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.pnlPaymentType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlPaymentType.Controls.Add(Me.btnPaymentTypeOK)
        Me.pnlPaymentType.Controls.Add(Me.txtInstalment)
        Me.pnlPaymentType.Controls.Add(Me.txtTradeIn)
        Me.pnlPaymentType.Controls.Add(Me.btnInstalmentCancel)
        Me.pnlPaymentType.Controls.Add(Me.txtCashSales)
        Me.pnlPaymentType.Controls.Add(Me.Label54)
        Me.pnlPaymentType.Controls.Add(Me.Panel7)
        Me.pnlPaymentType.Location = New System.Drawing.Point(424, 304)
        Me.pnlPaymentType.Name = "pnlPaymentType"
        Me.pnlPaymentType.Size = New System.Drawing.Size(158, 160)
        Me.pnlPaymentType.TabIndex = 126
        Me.pnlPaymentType.Visible = False
        '
        'btnPaymentTypeOK
        '
        Me.btnPaymentTypeOK.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPaymentTypeOK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPaymentTypeOK.Image = CType(resources.GetObject("btnPaymentTypeOK.Image"), System.Drawing.Image)
        Me.btnPaymentTypeOK.Location = New System.Drawing.Point(8, 128)
        Me.btnPaymentTypeOK.Name = "btnPaymentTypeOK"
        Me.btnPaymentTypeOK.Size = New System.Drawing.Size(56, 23)
        Me.btnPaymentTypeOK.TabIndex = 79
        Me.btnPaymentTypeOK.Text = "OK"
        Me.btnPaymentTypeOK.UseVisualStyleBackColor = False
        '
        'txtInstalment
        '
        Me.txtInstalment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInstalment.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInstalment.ForeColor = System.Drawing.Color.Black
        Me.txtInstalment.Location = New System.Drawing.Point(8, 96)
        Me.txtInstalment.MaxLength = 20
        Me.txtInstalment.Name = "txtInstalment"
        Me.txtInstalment.Size = New System.Drawing.Size(136, 25)
        Me.txtInstalment.TabIndex = 78
        Me.txtInstalment.Tag = ""
        Me.txtInstalment.Text = "Instalment"
        Me.txtInstalment.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtTradeIn
        '
        Me.txtTradeIn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTradeIn.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTradeIn.ForeColor = System.Drawing.Color.Black
        Me.txtTradeIn.Location = New System.Drawing.Point(8, 64)
        Me.txtTradeIn.MaxLength = 20
        Me.txtTradeIn.Name = "txtTradeIn"
        Me.txtTradeIn.Size = New System.Drawing.Size(136, 25)
        Me.txtTradeIn.TabIndex = 77
        Me.txtTradeIn.Tag = ""
        Me.txtTradeIn.Text = "Trade In"
        Me.txtTradeIn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnInstalmentCancel
        '
        Me.btnInstalmentCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnInstalmentCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInstalmentCancel.Image = CType(resources.GetObject("btnInstalmentCancel.Image"), System.Drawing.Image)
        Me.btnInstalmentCancel.Location = New System.Drawing.Point(80, 128)
        Me.btnInstalmentCancel.Name = "btnInstalmentCancel"
        Me.btnInstalmentCancel.Size = New System.Drawing.Size(64, 23)
        Me.btnInstalmentCancel.TabIndex = 75
        Me.btnInstalmentCancel.Text = "Cancel"
        Me.btnInstalmentCancel.UseVisualStyleBackColor = False
        '
        'txtCashSales
        '
        Me.txtCashSales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCashSales.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCashSales.ForeColor = System.Drawing.Color.Black
        Me.txtCashSales.Location = New System.Drawing.Point(8, 32)
        Me.txtCashSales.MaxLength = 20
        Me.txtCashSales.Name = "txtCashSales"
        Me.txtCashSales.Size = New System.Drawing.Size(136, 25)
        Me.txtCashSales.TabIndex = 73
        Me.txtCashSales.Tag = ""
        Me.txtCashSales.Text = "Cash Sales"
        Me.txtCashSales.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label54
        '
        Me.Label54.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label54.ForeColor = System.Drawing.Color.Blue
        Me.Label54.Location = New System.Drawing.Point(8, 8)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(128, 24)
        Me.Label54.TabIndex = 72
        Me.Label54.Text = "Payment Type"
        Me.Label54.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Panel7
        '
        Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel7.Controls.Add(Me.optInstallment)
        Me.Panel7.Controls.Add(Me.optCashSales)
        Me.Panel7.Controls.Add(Me.optTradeIn)
        Me.Panel7.Location = New System.Drawing.Point(8, 64)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(136, 69)
        Me.Panel7.TabIndex = 115
        '
        'optInstallment
        '
        Me.optInstallment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optInstallment.ForeColor = System.Drawing.Color.Black
        Me.optInstallment.Location = New System.Drawing.Point(8, 20)
        Me.optInstallment.Name = "optInstallment"
        Me.optInstallment.Size = New System.Drawing.Size(96, 20)
        Me.optInstallment.TabIndex = 13
        Me.optInstallment.Text = "Instalment"
        '
        'optCashSales
        '
        Me.optCashSales.Checked = True
        Me.optCashSales.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCashSales.ForeColor = System.Drawing.Color.Black
        Me.optCashSales.Location = New System.Drawing.Point(8, 0)
        Me.optCashSales.Name = "optCashSales"
        Me.optCashSales.Size = New System.Drawing.Size(96, 20)
        Me.optCashSales.TabIndex = 12
        Me.optCashSales.TabStop = True
        Me.optCashSales.Text = "Cash Sales"
        '
        'optTradeIn
        '
        Me.optTradeIn.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTradeIn.ForeColor = System.Drawing.Color.Black
        Me.optTradeIn.Location = New System.Drawing.Point(8, 40)
        Me.optTradeIn.Name = "optTradeIn"
        Me.optTradeIn.Size = New System.Drawing.Size(96, 20)
        Me.optTradeIn.TabIndex = 3
        Me.optTradeIn.Text = "Trade In"
        '
        'pnlExpense
        '
        Me.pnlExpense.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.pnlExpense.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlExpense.Controls.Add(Me.cboExpensesType)
        Me.pnlExpense.Controls.Add(Me.txtExpenseAmount)
        Me.pnlExpense.Controls.Add(Me.Label57)
        Me.pnlExpense.Controls.Add(Me.txtExpenseDescription)
        Me.pnlExpense.Controls.Add(Me.Label56)
        Me.pnlExpense.Controls.Add(Me.btnExpenseClose)
        Me.pnlExpense.Controls.Add(Me.btnExpenseSave)
        Me.pnlExpense.Controls.Add(Me.Type)
        Me.pnlExpense.Controls.Add(Me.Label55)
        Me.pnlExpense.Location = New System.Drawing.Point(424, 304)
        Me.pnlExpense.Name = "pnlExpense"
        Me.pnlExpense.Size = New System.Drawing.Size(278, 152)
        Me.pnlExpense.TabIndex = 127
        Me.pnlExpense.Visible = False
        '
        'cboExpensesType
        '
        Me.cboExpensesType.Items.AddRange(New Object() {"CASH PURCHASE", "PAID OUT", "SUNDAY", "OTHER EXPENSES"})
        Me.cboExpensesType.Location = New System.Drawing.Point(128, 32)
        Me.cboExpensesType.Name = "cboExpensesType"
        Me.cboExpensesType.Size = New System.Drawing.Size(136, 21)
        Me.cboExpensesType.TabIndex = 81
        '
        'txtExpenseAmount
        '
        Me.txtExpenseAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExpenseAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExpenseAmount.ForeColor = System.Drawing.Color.Black
        Me.txtExpenseAmount.Location = New System.Drawing.Point(128, 80)
        Me.txtExpenseAmount.MaxLength = 20
        Me.txtExpenseAmount.Name = "txtExpenseAmount"
        Me.txtExpenseAmount.Size = New System.Drawing.Size(88, 20)
        Me.txtExpenseAmount.TabIndex = 79
        Me.txtExpenseAmount.Tag = ""
        '
        'Label57
        '
        Me.Label57.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label57.ForeColor = System.Drawing.Color.Black
        Me.Label57.Location = New System.Drawing.Point(32, 80)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(80, 15)
        Me.Label57.TabIndex = 80
        Me.Label57.Text = "Amount"
        '
        'txtExpenseDescription
        '
        Me.txtExpenseDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExpenseDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExpenseDescription.ForeColor = System.Drawing.Color.Black
        Me.txtExpenseDescription.Location = New System.Drawing.Point(128, 56)
        Me.txtExpenseDescription.MaxLength = 20
        Me.txtExpenseDescription.Name = "txtExpenseDescription"
        Me.txtExpenseDescription.Size = New System.Drawing.Size(136, 20)
        Me.txtExpenseDescription.TabIndex = 77
        Me.txtExpenseDescription.Tag = ""
        '
        'Label56
        '
        Me.Label56.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.ForeColor = System.Drawing.Color.Black
        Me.Label56.Location = New System.Drawing.Point(32, 56)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(80, 15)
        Me.Label56.TabIndex = 78
        Me.Label56.Text = "Description"
        '
        'btnExpenseClose
        '
        Me.btnExpenseClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnExpenseClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExpenseClose.Image = CType(resources.GetObject("btnExpenseClose.Image"), System.Drawing.Image)
        Me.btnExpenseClose.Location = New System.Drawing.Point(136, 112)
        Me.btnExpenseClose.Name = "btnExpenseClose"
        Me.btnExpenseClose.Size = New System.Drawing.Size(64, 23)
        Me.btnExpenseClose.TabIndex = 75
        Me.btnExpenseClose.Text = "Close"
        Me.btnExpenseClose.UseVisualStyleBackColor = False
        '
        'btnExpenseSave
        '
        Me.btnExpenseSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnExpenseSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExpenseSave.Image = CType(resources.GetObject("btnExpenseSave.Image"), System.Drawing.Image)
        Me.btnExpenseSave.Location = New System.Drawing.Point(64, 112)
        Me.btnExpenseSave.Name = "btnExpenseSave"
        Me.btnExpenseSave.Size = New System.Drawing.Size(64, 23)
        Me.btnExpenseSave.TabIndex = 76
        Me.btnExpenseSave.Text = "Save"
        Me.btnExpenseSave.UseVisualStyleBackColor = False
        '
        'Type
        '
        Me.Type.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Type.ForeColor = System.Drawing.Color.Black
        Me.Type.Location = New System.Drawing.Point(32, 34)
        Me.Type.Name = "Type"
        Me.Type.Size = New System.Drawing.Size(80, 15)
        Me.Type.TabIndex = 74
        Me.Type.Text = "Bill No"
        '
        'Label55
        '
        Me.Label55.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label55.ForeColor = System.Drawing.Color.Blue
        Me.Label55.Location = New System.Drawing.Point(8, 8)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(264, 15)
        Me.Label55.TabIndex = 72
        Me.Label55.Text = "Expenses"
        Me.Label55.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlTotal
        '
        Me.pnlTotal.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.pnlTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTotal.Controls.Add(Me.pnlBillCancelAdminPwd)
        Me.pnlTotal.Controls.Add(Me.grpType)
        Me.pnlTotal.Controls.Add(Me.txtPayMode)
        Me.pnlTotal.Controls.Add(Me.txtLossPer)
        Me.pnlTotal.Controls.Add(Me.Label64)
        Me.pnlTotal.Controls.Add(Me.txtTotalFinal)
        Me.pnlTotal.Controls.Add(Me.Label62)
        Me.pnlTotal.Controls.Add(Me.txtPMSurCharge)
        Me.pnlTotal.Controls.Add(Me.Label63)
        Me.pnlTotal.Controls.Add(Me.txtPMNetTotal)
        Me.pnlTotal.Controls.Add(Me.Label60)
        Me.pnlTotal.Controls.Add(Me.txtPMDiscount)
        Me.pnlTotal.Controls.Add(Me.Label61)
        Me.pnlTotal.Controls.Add(Me.btnPaymodeSearch)
        Me.pnlTotal.Controls.Add(Me.txtPMGST)
        Me.pnlTotal.Controls.Add(Me.Label53)
        Me.pnlTotal.Controls.Add(Me.txtGrandTotal)
        Me.pnlTotal.Controls.Add(Me.Label58)
        Me.pnlTotal.Controls.Add(Me.btnPMClear)
        Me.pnlTotal.Controls.Add(Me.btnPMPayment)
        Me.pnlTotal.Controls.Add(Me.Label59)
        Me.pnlTotal.Location = New System.Drawing.Point(403, 291)
        Me.pnlTotal.Name = "pnlTotal"
        Me.pnlTotal.Size = New System.Drawing.Size(376, 248)
        Me.pnlTotal.TabIndex = 128
        Me.pnlTotal.Visible = False
        '
        'pnlBillCancelAdminPwd
        '
        Me.pnlBillCancelAdminPwd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.pnlBillCancelAdminPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlBillCancelAdminPwd.Controls.Add(Me.btnBCAdminPwdCancel)
        Me.pnlBillCancelAdminPwd.Controls.Add(Me.btnBCAdminPwdOK)
        Me.pnlBillCancelAdminPwd.Controls.Add(Me.txtBCAdminPassword)
        Me.pnlBillCancelAdminPwd.Controls.Add(Me.Label19)
        Me.pnlBillCancelAdminPwd.Controls.Add(Me.Label29)
        Me.pnlBillCancelAdminPwd.Location = New System.Drawing.Point(32, 40)
        Me.pnlBillCancelAdminPwd.Name = "pnlBillCancelAdminPwd"
        Me.pnlBillCancelAdminPwd.Size = New System.Drawing.Size(246, 96)
        Me.pnlBillCancelAdminPwd.TabIndex = 150
        Me.pnlBillCancelAdminPwd.Visible = False
        '
        'btnBCAdminPwdCancel
        '
        Me.btnBCAdminPwdCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnBCAdminPwdCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBCAdminPwdCancel.Image = CType(resources.GetObject("btnBCAdminPwdCancel.Image"), System.Drawing.Image)
        Me.btnBCAdminPwdCancel.Location = New System.Drawing.Point(160, 56)
        Me.btnBCAdminPwdCancel.Name = "btnBCAdminPwdCancel"
        Me.btnBCAdminPwdCancel.Size = New System.Drawing.Size(64, 23)
        Me.btnBCAdminPwdCancel.TabIndex = 75
        Me.btnBCAdminPwdCancel.Text = "Cancel"
        Me.btnBCAdminPwdCancel.UseVisualStyleBackColor = False
        '
        'btnBCAdminPwdOK
        '
        Me.btnBCAdminPwdOK.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnBCAdminPwdOK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBCAdminPwdOK.Image = CType(resources.GetObject("btnBCAdminPwdOK.Image"), System.Drawing.Image)
        Me.btnBCAdminPwdOK.Location = New System.Drawing.Point(88, 56)
        Me.btnBCAdminPwdOK.Name = "btnBCAdminPwdOK"
        Me.btnBCAdminPwdOK.Size = New System.Drawing.Size(64, 23)
        Me.btnBCAdminPwdOK.TabIndex = 74
        Me.btnBCAdminPwdOK.Text = "OK"
        Me.btnBCAdminPwdOK.UseVisualStyleBackColor = False
        '
        'txtBCAdminPassword
        '
        Me.txtBCAdminPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBCAdminPassword.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBCAdminPassword.ForeColor = System.Drawing.Color.Black
        Me.txtBCAdminPassword.Location = New System.Drawing.Point(88, 32)
        Me.txtBCAdminPassword.MaxLength = 20
        Me.txtBCAdminPassword.Name = "txtBCAdminPassword"
        Me.txtBCAdminPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtBCAdminPassword.Size = New System.Drawing.Size(136, 20)
        Me.txtBCAdminPassword.TabIndex = 73
        Me.txtBCAdminPassword.Tag = ""
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(16, 33)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(64, 15)
        Me.Label19.TabIndex = 72
        Me.Label19.Text = "Password"
        '
        'Label29
        '
        Me.Label29.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.Color.Black
        Me.Label29.Location = New System.Drawing.Point(8, 8)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(56, 15)
        Me.Label29.TabIndex = 72
        Me.Label29.Text = "Admin"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'grpType
        '
        Me.grpType.Controls.Add(Me.lbltradeIN)
        Me.grpType.Controls.Add(Me.lblInstalment)
        Me.grpType.Controls.Add(Me.lblCash)
        Me.grpType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpType.ForeColor = System.Drawing.Color.Blue
        Me.grpType.Location = New System.Drawing.Point(261, 88)
        Me.grpType.Name = "grpType"
        Me.grpType.Size = New System.Drawing.Size(112, 96)
        Me.grpType.TabIndex = 121
        Me.grpType.TabStop = False
        '
        'lbltradeIN
        '
        Me.lbltradeIN.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltradeIN.ForeColor = System.Drawing.Color.Red
        Me.lbltradeIN.Location = New System.Drawing.Point(8, 64)
        Me.lbltradeIN.Name = "lbltradeIN"
        Me.lbltradeIN.Size = New System.Drawing.Size(96, 16)
        Me.lbltradeIN.TabIndex = 2
        Me.lbltradeIN.Text = "T - TRADE-IN"
        '
        'lblInstalment
        '
        Me.lblInstalment.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInstalment.ForeColor = System.Drawing.Color.Red
        Me.lblInstalment.Location = New System.Drawing.Point(8, 40)
        Me.lblInstalment.Name = "lblInstalment"
        Me.lblInstalment.Size = New System.Drawing.Size(96, 16)
        Me.lblInstalment.TabIndex = 1
        Me.lblInstalment.Text = "I - INSTALMENT"
        '
        'lblCash
        '
        Me.lblCash.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCash.ForeColor = System.Drawing.Color.Red
        Me.lblCash.Location = New System.Drawing.Point(8, 16)
        Me.lblCash.Name = "lblCash"
        Me.lblCash.Size = New System.Drawing.Size(72, 16)
        Me.lblCash.TabIndex = 0
        Me.lblCash.Text = "C - CASH"
        '
        'txtPayMode
        '
        Me.txtPayMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPayMode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayMode.ForeColor = System.Drawing.Color.Black
        Me.txtPayMode.Location = New System.Drawing.Point(112, 32)
        Me.txtPayMode.MaxLength = 20
        Me.txtPayMode.Name = "txtPayMode"
        Me.txtPayMode.Size = New System.Drawing.Size(136, 20)
        Me.txtPayMode.TabIndex = 81
        Me.txtPayMode.Tag = ""
        '
        'txtLossPer
        '
        Me.txtLossPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLossPer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLossPer.ForeColor = System.Drawing.Color.Black
        Me.txtLossPer.Location = New System.Drawing.Point(312, 32)
        Me.txtLossPer.MaxLength = 20
        Me.txtLossPer.Name = "txtLossPer"
        Me.txtLossPer.Size = New System.Drawing.Size(56, 20)
        Me.txtLossPer.TabIndex = 88
        Me.txtLossPer.Tag = ""
        Me.txtLossPer.Visible = False
        '
        'Label64
        '
        Me.Label64.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label64.ForeColor = System.Drawing.Color.Black
        Me.Label64.Location = New System.Drawing.Point(256, 32)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(48, 15)
        Me.Label64.TabIndex = 120
        Me.Label64.Text = "Loss %"
        Me.Label64.Visible = False
        '
        'txtTotalFinal
        '
        Me.txtTotalFinal.BackColor = System.Drawing.Color.White
        Me.txtTotalFinal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalFinal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalFinal.ForeColor = System.Drawing.Color.Black
        Me.txtTotalFinal.Location = New System.Drawing.Point(112, 176)
        Me.txtTotalFinal.MaxLength = 20
        Me.txtTotalFinal.Name = "txtTotalFinal"
        Me.txtTotalFinal.ReadOnly = True
        Me.txtTotalFinal.Size = New System.Drawing.Size(136, 20)
        Me.txtTotalFinal.TabIndex = 87
        Me.txtTotalFinal.Tag = ""
        '
        'Label62
        '
        Me.Label62.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label62.ForeColor = System.Drawing.Color.Black
        Me.Label62.Location = New System.Drawing.Point(16, 176)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(80, 15)
        Me.Label62.TabIndex = 119
        Me.Label62.Text = "Total"
        '
        'txtPMSurCharge
        '
        Me.txtPMSurCharge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPMSurCharge.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPMSurCharge.ForeColor = System.Drawing.Color.Black
        Me.txtPMSurCharge.Location = New System.Drawing.Point(112, 152)
        Me.txtPMSurCharge.MaxLength = 20
        Me.txtPMSurCharge.Name = "txtPMSurCharge"
        Me.txtPMSurCharge.ReadOnly = True
        Me.txtPMSurCharge.Size = New System.Drawing.Size(136, 20)
        Me.txtPMSurCharge.TabIndex = 86
        Me.txtPMSurCharge.Tag = ""
        Me.txtPMSurCharge.Text = "0.00"
        '
        'Label63
        '
        Me.Label63.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label63.ForeColor = System.Drawing.Color.Black
        Me.Label63.Location = New System.Drawing.Point(16, 152)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(80, 15)
        Me.Label63.TabIndex = 117
        Me.Label63.Text = "Sur Charge"
        '
        'txtPMNetTotal
        '
        Me.txtPMNetTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPMNetTotal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPMNetTotal.ForeColor = System.Drawing.Color.Black
        Me.txtPMNetTotal.Location = New System.Drawing.Point(112, 128)
        Me.txtPMNetTotal.MaxLength = 20
        Me.txtPMNetTotal.Name = "txtPMNetTotal"
        Me.txtPMNetTotal.ReadOnly = True
        Me.txtPMNetTotal.Size = New System.Drawing.Size(136, 20)
        Me.txtPMNetTotal.TabIndex = 85
        Me.txtPMNetTotal.Tag = ""
        '
        'Label60
        '
        Me.Label60.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label60.ForeColor = System.Drawing.Color.Black
        Me.Label60.Location = New System.Drawing.Point(16, 128)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(80, 15)
        Me.Label60.TabIndex = 115
        Me.Label60.Text = "Net Total"
        '
        'txtPMDiscount
        '
        Me.txtPMDiscount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPMDiscount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPMDiscount.ForeColor = System.Drawing.Color.Black
        Me.txtPMDiscount.Location = New System.Drawing.Point(112, 104)
        Me.txtPMDiscount.MaxLength = 20
        Me.txtPMDiscount.Name = "txtPMDiscount"
        Me.txtPMDiscount.ReadOnly = True
        Me.txtPMDiscount.Size = New System.Drawing.Size(136, 20)
        Me.txtPMDiscount.TabIndex = 84
        Me.txtPMDiscount.Tag = ""
        '
        'Label61
        '
        Me.Label61.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label61.ForeColor = System.Drawing.Color.Black
        Me.Label61.Location = New System.Drawing.Point(16, 104)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(80, 15)
        Me.Label61.TabIndex = 113
        Me.Label61.Text = "Discount"
        '
        'btnPaymodeSearch
        '
        Me.btnPaymodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPaymodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPaymodeSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPaymodeSearch.Location = New System.Drawing.Point(224, 32)
        Me.btnPaymodeSearch.Name = "btnPaymodeSearch"
        Me.btnPaymodeSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnPaymodeSearch.TabIndex = 111
        Me.btnPaymodeSearch.Text = "..."
        Me.btnPaymodeSearch.UseVisualStyleBackColor = False
        Me.btnPaymodeSearch.Visible = False
        '
        'txtPMGST
        '
        Me.txtPMGST.BackColor = System.Drawing.Color.White
        Me.txtPMGST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPMGST.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPMGST.ForeColor = System.Drawing.Color.Black
        Me.txtPMGST.Location = New System.Drawing.Point(112, 80)
        Me.txtPMGST.MaxLength = 20
        Me.txtPMGST.Name = "txtPMGST"
        Me.txtPMGST.ReadOnly = True
        Me.txtPMGST.Size = New System.Drawing.Size(136, 20)
        Me.txtPMGST.TabIndex = 83
        Me.txtPMGST.Tag = ""
        '
        'Label53
        '
        Me.Label53.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.ForeColor = System.Drawing.Color.Black
        Me.Label53.Location = New System.Drawing.Point(16, 80)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(80, 15)
        Me.Label53.TabIndex = 80
        Me.Label53.Text = "GST"
        '
        'txtGrandTotal
        '
        Me.txtGrandTotal.BackColor = System.Drawing.Color.White
        Me.txtGrandTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGrandTotal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGrandTotal.ForeColor = System.Drawing.Color.Black
        Me.txtGrandTotal.Location = New System.Drawing.Point(112, 56)
        Me.txtGrandTotal.MaxLength = 20
        Me.txtGrandTotal.Name = "txtGrandTotal"
        Me.txtGrandTotal.ReadOnly = True
        Me.txtGrandTotal.Size = New System.Drawing.Size(136, 20)
        Me.txtGrandTotal.TabIndex = 82
        Me.txtGrandTotal.Tag = ""
        '
        'Label58
        '
        Me.Label58.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label58.ForeColor = System.Drawing.Color.Black
        Me.Label58.Location = New System.Drawing.Point(16, 56)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(80, 15)
        Me.Label58.TabIndex = 78
        Me.Label58.Text = "Grand Total"
        '
        'btnPMClear
        '
        Me.btnPMClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPMClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPMClear.Image = CType(resources.GetObject("btnPMClear.Image"), System.Drawing.Image)
        Me.btnPMClear.Location = New System.Drawing.Point(205, 208)
        Me.btnPMClear.Name = "btnPMClear"
        Me.btnPMClear.Size = New System.Drawing.Size(64, 23)
        Me.btnPMClear.TabIndex = 90
        Me.btnPMClear.Text = "Clear(F1)"
        Me.btnPMClear.UseVisualStyleBackColor = False
        '
        'btnPMPayment
        '
        Me.btnPMPayment.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPMPayment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPMPayment.Image = CType(resources.GetObject("btnPMPayment.Image"), System.Drawing.Image)
        Me.btnPMPayment.Location = New System.Drawing.Point(133, 208)
        Me.btnPMPayment.Name = "btnPMPayment"
        Me.btnPMPayment.Size = New System.Drawing.Size(64, 23)
        Me.btnPMPayment.TabIndex = 89
        Me.btnPMPayment.Text = "Payment"
        Me.btnPMPayment.UseVisualStyleBackColor = False
        '
        'Label59
        '
        Me.Label59.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label59.ForeColor = System.Drawing.Color.Black
        Me.Label59.Location = New System.Drawing.Point(16, 34)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(80, 15)
        Me.Label59.TabIndex = 74
        Me.Label59.Text = "PayMode"
        '
        'PictureBox1
        '
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(0, 6)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(96, 658)
        Me.PictureBox1.TabIndex = 143
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(120, 72)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(168, 72)
        Me.PictureBox2.TabIndex = 144
        Me.PictureBox2.TabStop = False
        '
        'PnlNetAmount
        '
        Me.PnlNetAmount.BackColor = System.Drawing.Color.Yellow
        Me.PnlNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PnlNetAmount.Controls.Add(Me.Label46)
        Me.PnlNetAmount.Controls.Add(Me.txtNetAmount)
        Me.PnlNetAmount.Controls.Add(Me.txtNetDiscountPercentage)
        Me.PnlNetAmount.Controls.Add(Me.txtNetDiscount)
        Me.PnlNetAmount.ForeColor = System.Drawing.Color.White
        Me.PnlNetAmount.Location = New System.Drawing.Point(841, 457)
        Me.PnlNetAmount.Name = "PnlNetAmount"
        Me.PnlNetAmount.Size = New System.Drawing.Size(174, 103)
        Me.PnlNetAmount.TabIndex = 145
        Me.PnlNetAmount.Visible = False
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.Yellow
        Me.Label46.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.Color.Navy
        Me.Label46.Location = New System.Drawing.Point(8, 16)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(160, 25)
        Me.Label46.TabIndex = 103
        Me.Label46.Text = "Net Amount"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtNetAmount
        '
        Me.txtNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNetAmount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetAmount.ForeColor = System.Drawing.Color.Black
        Me.txtNetAmount.Location = New System.Drawing.Point(8, 72)
        Me.txtNetAmount.MaxLength = 20
        Me.txtNetAmount.Name = "txtNetAmount"
        Me.txtNetAmount.Size = New System.Drawing.Size(160, 25)
        Me.txtNetAmount.TabIndex = 102
        Me.txtNetAmount.Tag = ""
        '
        'txtNetDiscountPercentage
        '
        Me.txtNetDiscountPercentage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNetDiscountPercentage.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetDiscountPercentage.ForeColor = System.Drawing.Color.Black
        Me.txtNetDiscountPercentage.Location = New System.Drawing.Point(8, 48)
        Me.txtNetDiscountPercentage.MaxLength = 20
        Me.txtNetDiscountPercentage.Name = "txtNetDiscountPercentage"
        Me.txtNetDiscountPercentage.Size = New System.Drawing.Size(56, 25)
        Me.txtNetDiscountPercentage.TabIndex = 105
        Me.txtNetDiscountPercentage.Tag = ""
        Me.txtNetDiscountPercentage.Visible = False
        '
        'txtNetDiscount
        '
        Me.txtNetDiscount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNetDiscount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetDiscount.ForeColor = System.Drawing.Color.Black
        Me.txtNetDiscount.Location = New System.Drawing.Point(64, 48)
        Me.txtNetDiscount.MaxLength = 20
        Me.txtNetDiscount.Name = "txtNetDiscount"
        Me.txtNetDiscount.Size = New System.Drawing.Size(104, 25)
        Me.txtNetDiscount.TabIndex = 104
        Me.txtNetDiscount.Tag = ""
        Me.txtNetDiscount.Visible = False
        '
        'lblCompanyName
        '
        Me.lblCompanyName.BackColor = System.Drawing.Color.Yellow
        Me.lblCompanyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCompanyName.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompanyName.ForeColor = System.Drawing.Color.Navy
        Me.lblCompanyName.Location = New System.Drawing.Point(96, 8)
        Me.lblCompanyName.Name = "lblCompanyName"
        Me.lblCompanyName.Size = New System.Drawing.Size(920, 24)
        Me.lblCompanyName.TabIndex = 146
        Me.lblCompanyName.Text = "Name and Address"
        Me.lblCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlFindCost
        '
        Me.pnlFindCost.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.pnlFindCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFindCost.Controls.Add(Me.txtTael)
        Me.pnlFindCost.Controls.Add(Me.Label78)
        Me.pnlFindCost.Controls.Add(Me.txtFindCostPerGram)
        Me.pnlFindCost.Controls.Add(Me.Label77)
        Me.pnlFindCost.Controls.Add(Me.txtFindTotalCost)
        Me.pnlFindCost.Controls.Add(Me.Label76)
        Me.pnlFindCost.Controls.Add(Me.txtSupplierCode)
        Me.pnlFindCost.Controls.Add(Me.Label75)
        Me.pnlFindCost.Controls.Add(Me.txtFindMC)
        Me.pnlFindCost.Controls.Add(Me.Label74)
        Me.pnlFindCost.Controls.Add(Me.txtFindPurchaseDate)
        Me.pnlFindCost.Controls.Add(Me.Label73)
        Me.pnlFindCost.Controls.Add(Me.txtFindPBillNo)
        Me.pnlFindCost.Controls.Add(Me.Label72)
        Me.pnlFindCost.Controls.Add(Me.txtFindWeight)
        Me.pnlFindCost.Controls.Add(Me.Label71)
        Me.pnlFindCost.Controls.Add(Me.txtFindDescription)
        Me.pnlFindCost.Controls.Add(Me.Label70)
        Me.pnlFindCost.Controls.Add(Me.btnFindCostOK)
        Me.pnlFindCost.Controls.Add(Me.txtFindInventory)
        Me.pnlFindCost.Controls.Add(Me.Label68)
        Me.pnlFindCost.Controls.Add(Me.Label69)
        Me.pnlFindCost.Location = New System.Drawing.Point(240, 296)
        Me.pnlFindCost.Name = "pnlFindCost"
        Me.pnlFindCost.Size = New System.Drawing.Size(456, 229)
        Me.pnlFindCost.TabIndex = 147
        Me.pnlFindCost.Visible = False
        '
        'txtTael
        '
        Me.txtTael.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTael.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTael.ForeColor = System.Drawing.Color.Black
        Me.txtTael.Location = New System.Drawing.Point(352, 152)
        Me.txtTael.MaxLength = 20
        Me.txtTael.Name = "txtTael"
        Me.txtTael.Size = New System.Drawing.Size(80, 20)
        Me.txtTael.TabIndex = 93
        Me.txtTael.Tag = ""
        '
        'Label78
        '
        Me.Label78.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label78.ForeColor = System.Drawing.Color.Black
        Me.Label78.Location = New System.Drawing.Point(264, 152)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(64, 15)
        Me.Label78.TabIndex = 94
        Me.Label78.Text = "TAEL"
        '
        'txtFindCostPerGram
        '
        Me.txtFindCostPerGram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFindCostPerGram.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFindCostPerGram.ForeColor = System.Drawing.Color.Black
        Me.txtFindCostPerGram.Location = New System.Drawing.Point(168, 152)
        Me.txtFindCostPerGram.MaxLength = 20
        Me.txtFindCostPerGram.Name = "txtFindCostPerGram"
        Me.txtFindCostPerGram.Size = New System.Drawing.Size(80, 20)
        Me.txtFindCostPerGram.TabIndex = 91
        Me.txtFindCostPerGram.Tag = ""
        '
        'Label77
        '
        Me.Label77.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label77.ForeColor = System.Drawing.Color.Black
        Me.Label77.Location = New System.Drawing.Point(24, 152)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(96, 15)
        Me.Label77.TabIndex = 92
        Me.Label77.Text = "Cost Per Gram"
        '
        'txtFindTotalCost
        '
        Me.txtFindTotalCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFindTotalCost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFindTotalCost.ForeColor = System.Drawing.Color.Black
        Me.txtFindTotalCost.Location = New System.Drawing.Point(352, 128)
        Me.txtFindTotalCost.MaxLength = 20
        Me.txtFindTotalCost.Name = "txtFindTotalCost"
        Me.txtFindTotalCost.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtFindTotalCost.Size = New System.Drawing.Size(80, 20)
        Me.txtFindTotalCost.TabIndex = 89
        Me.txtFindTotalCost.Tag = ""
        Me.txtFindTotalCost.Text = "0.00"
        '
        'Label76
        '
        Me.Label76.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label76.ForeColor = System.Drawing.Color.Black
        Me.Label76.Location = New System.Drawing.Point(264, 128)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(64, 15)
        Me.Label76.TabIndex = 90
        Me.Label76.Text = "Total Cost"
        '
        'txtSupplierCode
        '
        Me.txtSupplierCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierCode.ForeColor = System.Drawing.Color.Black
        Me.txtSupplierCode.Location = New System.Drawing.Point(352, 104)
        Me.txtSupplierCode.MaxLength = 20
        Me.txtSupplierCode.Name = "txtSupplierCode"
        Me.txtSupplierCode.Size = New System.Drawing.Size(80, 20)
        Me.txtSupplierCode.TabIndex = 87
        Me.txtSupplierCode.Tag = ""
        '
        'Label75
        '
        Me.Label75.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label75.ForeColor = System.Drawing.Color.Black
        Me.Label75.Location = New System.Drawing.Point(264, 104)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(88, 15)
        Me.Label75.TabIndex = 88
        Me.Label75.Text = "Supplier Code"
        '
        'txtFindMC
        '
        Me.txtFindMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFindMC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFindMC.ForeColor = System.Drawing.Color.Black
        Me.txtFindMC.Location = New System.Drawing.Point(352, 80)
        Me.txtFindMC.MaxLength = 20
        Me.txtFindMC.Name = "txtFindMC"
        Me.txtFindMC.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtFindMC.Size = New System.Drawing.Size(80, 20)
        Me.txtFindMC.TabIndex = 85
        Me.txtFindMC.Tag = ""
        Me.txtFindMC.Text = "0.00"
        '
        'Label74
        '
        Me.Label74.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label74.ForeColor = System.Drawing.Color.Black
        Me.Label74.Location = New System.Drawing.Point(264, 80)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(88, 15)
        Me.Label74.TabIndex = 86
        Me.Label74.Text = "Supplier MC"
        '
        'txtFindPurchaseDate
        '
        Me.txtFindPurchaseDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFindPurchaseDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFindPurchaseDate.ForeColor = System.Drawing.Color.Black
        Me.txtFindPurchaseDate.Location = New System.Drawing.Point(168, 128)
        Me.txtFindPurchaseDate.MaxLength = 20
        Me.txtFindPurchaseDate.Name = "txtFindPurchaseDate"
        Me.txtFindPurchaseDate.Size = New System.Drawing.Size(80, 20)
        Me.txtFindPurchaseDate.TabIndex = 83
        Me.txtFindPurchaseDate.Tag = ""
        '
        'Label73
        '
        Me.Label73.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label73.ForeColor = System.Drawing.Color.Black
        Me.Label73.Location = New System.Drawing.Point(24, 128)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(112, 15)
        Me.Label73.TabIndex = 84
        Me.Label73.Text = "Purchase Date"
        '
        'txtFindPBillNo
        '
        Me.txtFindPBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFindPBillNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFindPBillNo.ForeColor = System.Drawing.Color.Black
        Me.txtFindPBillNo.Location = New System.Drawing.Point(168, 104)
        Me.txtFindPBillNo.MaxLength = 20
        Me.txtFindPBillNo.Name = "txtFindPBillNo"
        Me.txtFindPBillNo.Size = New System.Drawing.Size(80, 20)
        Me.txtFindPBillNo.TabIndex = 81
        Me.txtFindPBillNo.Tag = ""
        '
        'Label72
        '
        Me.Label72.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label72.ForeColor = System.Drawing.Color.Black
        Me.Label72.Location = New System.Drawing.Point(24, 104)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(120, 15)
        Me.Label72.TabIndex = 82
        Me.Label72.Text = "Purchase Bill NO"
        '
        'txtFindWeight
        '
        Me.txtFindWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFindWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFindWeight.ForeColor = System.Drawing.Color.Black
        Me.txtFindWeight.Location = New System.Drawing.Point(168, 80)
        Me.txtFindWeight.MaxLength = 20
        Me.txtFindWeight.Name = "txtFindWeight"
        Me.txtFindWeight.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtFindWeight.Size = New System.Drawing.Size(80, 20)
        Me.txtFindWeight.TabIndex = 79
        Me.txtFindWeight.Tag = ""
        Me.txtFindWeight.Text = "0.00"
        '
        'Label71
        '
        Me.Label71.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label71.ForeColor = System.Drawing.Color.Black
        Me.Label71.Location = New System.Drawing.Point(24, 80)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(64, 15)
        Me.Label71.TabIndex = 80
        Me.Label71.Text = "Weight"
        '
        'txtFindDescription
        '
        Me.txtFindDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFindDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFindDescription.ForeColor = System.Drawing.Color.Black
        Me.txtFindDescription.Location = New System.Drawing.Point(168, 56)
        Me.txtFindDescription.MaxLength = 20
        Me.txtFindDescription.Name = "txtFindDescription"
        Me.txtFindDescription.Size = New System.Drawing.Size(264, 20)
        Me.txtFindDescription.TabIndex = 78
        Me.txtFindDescription.Tag = ""
        '
        'Label70
        '
        Me.Label70.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label70.ForeColor = System.Drawing.Color.Black
        Me.Label70.Location = New System.Drawing.Point(24, 56)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(88, 15)
        Me.Label70.TabIndex = 77
        Me.Label70.Text = "Description"
        '
        'btnFindCostOK
        '
        Me.btnFindCostOK.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnFindCostOK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFindCostOK.Image = CType(resources.GetObject("btnFindCostOK.Image"), System.Drawing.Image)
        Me.btnFindCostOK.Location = New System.Drawing.Point(168, 184)
        Me.btnFindCostOK.Name = "btnFindCostOK"
        Me.btnFindCostOK.Size = New System.Drawing.Size(80, 23)
        Me.btnFindCostOK.TabIndex = 76
        Me.btnFindCostOK.Text = "OK"
        Me.btnFindCostOK.UseVisualStyleBackColor = False
        '
        'txtFindInventory
        '
        Me.txtFindInventory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFindInventory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFindInventory.ForeColor = System.Drawing.Color.Black
        Me.txtFindInventory.Location = New System.Drawing.Point(168, 32)
        Me.txtFindInventory.MaxLength = 20
        Me.txtFindInventory.Name = "txtFindInventory"
        Me.txtFindInventory.Size = New System.Drawing.Size(136, 20)
        Me.txtFindInventory.TabIndex = 73
        Me.txtFindInventory.Tag = ""
        '
        'Label68
        '
        Me.Label68.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label68.ForeColor = System.Drawing.Color.Black
        Me.Label68.Location = New System.Drawing.Point(24, 32)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(136, 15)
        Me.Label68.TabIndex = 74
        Me.Label68.Text = "Inventory Code/Barcode"
        '
        'Label69
        '
        Me.Label69.Font = New System.Drawing.Font("Arial", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label69.ForeColor = System.Drawing.Color.Red
        Me.Label69.Location = New System.Drawing.Point(8, 8)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(82, 16)
        Me.Label69.TabIndex = 72
        Me.Label69.Text = "Find Cost"
        Me.Label69.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCompanyAddress
        '
        Me.lblCompanyAddress.BackColor = System.Drawing.Color.Yellow
        Me.lblCompanyAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCompanyAddress.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompanyAddress.ForeColor = System.Drawing.Color.Navy
        Me.lblCompanyAddress.Location = New System.Drawing.Point(96, 32)
        Me.lblCompanyAddress.Name = "lblCompanyAddress"
        Me.lblCompanyAddress.Size = New System.Drawing.Size(920, 40)
        Me.lblCompanyAddress.TabIndex = 148
        Me.lblCompanyAddress.Text = "Name and Address"
        Me.lblCompanyAddress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlMember
        '
        Me.pnlMember.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.pnlMember.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMember.Controls.Add(Me.btnMemberCodeSearch)
        Me.pnlMember.Controls.Add(Me.btnNew)
        Me.pnlMember.Controls.Add(Me.btnOK)
        Me.pnlMember.Controls.Add(Me.btnCancel)
        Me.pnlMember.Controls.Add(Me.txtMemberCodeView)
        Me.pnlMember.Controls.Add(Me.Label79)
        Me.pnlMember.Location = New System.Drawing.Point(368, 346)
        Me.pnlMember.Name = "pnlMember"
        Me.pnlMember.Size = New System.Drawing.Size(312, 104)
        Me.pnlMember.TabIndex = 149
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
        Me.btnMemberCodeSearch.TabIndex = 119
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
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.Location = New System.Drawing.Point(216, 56)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(64, 23)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
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
        'pnlCostAdminPwd
        '
        Me.pnlCostAdminPwd.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlCostAdminPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCostAdminPwd.Controls.Add(Me.btnCostAdminPwdCancel)
        Me.pnlCostAdminPwd.Controls.Add(Me.btnCostAdminPwdOK)
        Me.pnlCostAdminPwd.Controls.Add(Me.txtCostAdminPassword)
        Me.pnlCostAdminPwd.Controls.Add(Me.txtPassFind)
        Me.pnlCostAdminPwd.Controls.Add(Me.Label33)
        Me.pnlCostAdminPwd.Location = New System.Drawing.Point(386, 292)
        Me.pnlCostAdminPwd.Name = "pnlCostAdminPwd"
        Me.pnlCostAdminPwd.Size = New System.Drawing.Size(246, 88)
        Me.pnlCostAdminPwd.TabIndex = 218
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
        'grdSales
        '
        Me.grdSales.AllowUserToAddRows = False
        Me.grdSales.AllowUserToDeleteRows = False
        Me.grdSales.AllowUserToOrderColumns = True
        Me.grdSales.AllowUserToResizeColumns = False
        Me.grdSales.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdSales.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdSales.ColumnHeadersHeight = 45
        Me.grdSales.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colInventoryCode, Me.colClass, Me.colDescription, Me.colWeight, Me.colGoldLoss, Me.colCostPerGram, Me.ColDSWt, Me.colSMC, Me.colPcs, Me.colTotalValue, Me.colTotalCost, Me.ColSalesType, Me.colPMC, Me.colGST, Me.colDetailDiscount})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdSales.DefaultCellStyle = DataGridViewCellStyle4
        Me.grdSales.Location = New System.Drawing.Point(96, 291)
        Me.grdSales.Name = "grdSales"
        Me.grdSales.ReadOnly = True
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdSales.RowHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.grdSales.RowHeadersVisible = False
        Me.grdSales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdSales.Size = New System.Drawing.Size(741, 271)
        Me.grdSales.TabIndex = 515
        '
        'grdMarketRate
        '
        Me.grdMarketRate.AllowUserToAddRows = False
        Me.grdMarketRate.AllowUserToDeleteRows = False
        Me.grdMarketRate.AllowUserToOrderColumns = True
        Me.grdMarketRate.AllowUserToResizeColumns = False
        Me.grdMarketRate.AllowUserToResizeRows = False
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdMarketRate.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.grdMarketRate.ColumnHeadersHeight = 42
        Me.grdMarketRate.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn2, Me.colRate})
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdMarketRate.DefaultCellStyle = DataGridViewCellStyle7
        Me.grdMarketRate.Location = New System.Drawing.Point(97, 72)
        Me.grdMarketRate.Name = "grdMarketRate"
        Me.grdMarketRate.ReadOnly = True
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdMarketRate.RowHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.grdMarketRate.RowHeadersVisible = False
        Me.grdMarketRate.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdMarketRate.Size = New System.Drawing.Size(203, 216)
        Me.grdMarketRate.TabIndex = 516
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Class"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'colRate
        '
        Me.colRate.HeaderText = "Rate"
        Me.colRate.Name = "colRate"
        Me.colRate.ReadOnly = True
        '
        'chkeTRS
        '
        Me.chkeTRS.AutoSize = True
        Me.chkeTRS.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkeTRS.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkeTRS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkeTRS.Location = New System.Drawing.Point(701, 153)
        Me.chkeTRS.Name = "chkeTRS"
        Me.chkeTRS.Size = New System.Drawing.Size(132, 18)
        Me.chkeTRS.TabIndex = 517
        Me.chkeTRS.Text = "eTRS Ticket Issued"
        Me.chkeTRS.UseVisualStyleBackColor = False
        Me.chkeTRS.Visible = False
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel8.Controls.Add(Me.opteTRSNo)
        Me.Panel8.Controls.Add(Me.opteTrsTicketIssued)
        Me.Panel8.Controls.Add(Me.opteTRSEligible)
        Me.Panel8.Location = New System.Drawing.Point(313, 251)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(239, 31)
        Me.Panel8.TabIndex = 518
        '
        'opteTRSNo
        '
        Me.opteTRSNo.Checked = True
        Me.opteTRSNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.opteTRSNo.ForeColor = System.Drawing.Color.Black
        Me.opteTRSNo.Location = New System.Drawing.Point(118, 0)
        Me.opteTRSNo.Name = "opteTRSNo"
        Me.opteTRSNo.Size = New System.Drawing.Size(117, 32)
        Me.opteTRSNo.TabIndex = 13
        Me.opteTRSNo.TabStop = True
        Me.opteTRSNo.Text = "Not For eTRS"
        '
        'opteTrsTicketIssued
        '
        Me.opteTrsTicketIssued.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.opteTrsTicketIssued.ForeColor = System.Drawing.Color.Black
        Me.opteTrsTicketIssued.Location = New System.Drawing.Point(11, 0)
        Me.opteTrsTicketIssued.Name = "opteTrsTicketIssued"
        Me.opteTrsTicketIssued.Size = New System.Drawing.Size(129, 34)
        Me.opteTrsTicketIssued.TabIndex = 5
        Me.opteTrsTicketIssued.Text = "For eTRS"
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
        'lblMemberPoints
        '
        Me.lblMemberPoints.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblMemberPoints.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMemberPoints.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMemberPoints.ForeColor = System.Drawing.Color.Red
        Me.lblMemberPoints.Location = New System.Drawing.Point(713, 179)
        Me.lblMemberPoints.Name = "lblMemberPoints"
        Me.lblMemberPoints.Size = New System.Drawing.Size(120, 20)
        Me.lblMemberPoints.TabIndex = 519
        Me.lblMemberPoints.Text = "Customer type"
        Me.lblMemberPoints.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkShowImage
        '
        Me.chkShowImage.AutoSize = True
        Me.chkShowImage.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkShowImage.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkShowImage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShowImage.Location = New System.Drawing.Point(718, 260)
        Me.chkShowImage.Name = "chkShowImage"
        Me.chkShowImage.Size = New System.Drawing.Size(95, 17)
        Me.chkShowImage.TabIndex = 520
        Me.chkShowImage.Text = "Show Image"
        Me.chkShowImage.UseVisualStyleBackColor = False
        Me.chkShowImage.Visible = False
        '
        'grpImage
        '
        Me.grpImage.Controls.Add(Me.picImage)
        Me.grpImage.Controls.Add(Me.cmdAddlDescOk)
        Me.grpImage.Location = New System.Drawing.Point(100, 75)
        Me.grpImage.Name = "grpImage"
        Me.grpImage.Size = New System.Drawing.Size(191, 215)
        Me.grpImage.TabIndex = 554
        Me.grpImage.TabStop = False
        Me.grpImage.Visible = False
        '
        'picImage
        '
        Me.picImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picImage.Location = New System.Drawing.Point(10, 15)
        Me.picImage.Name = "picImage"
        Me.picImage.Size = New System.Drawing.Size(165, 155)
        Me.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picImage.TabIndex = 770
        Me.picImage.TabStop = False
        '
        'cmdAddlDescOk
        '
        Me.cmdAddlDescOk.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdAddlDescOk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAddlDescOk.ForeColor = System.Drawing.Color.Black
        Me.cmdAddlDescOk.Location = New System.Drawing.Point(114, 184)
        Me.cmdAddlDescOk.Name = "cmdAddlDescOk"
        Me.cmdAddlDescOk.Size = New System.Drawing.Size(61, 23)
        Me.cmdAddlDescOk.TabIndex = 45
        Me.cmdAddlDescOk.Text = "OK"
        Me.cmdAddlDescOk.UseVisualStyleBackColor = False
        '
        'chkTradeIn
        '
        Me.chkTradeIn.AutoSize = True
        Me.chkTradeIn.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkTradeIn.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkTradeIn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTradeIn.Location = New System.Drawing.Point(749, 260)
        Me.chkTradeIn.Name = "chkTradeIn"
        Me.chkTradeIn.Size = New System.Drawing.Size(74, 17)
        Me.chkTradeIn.TabIndex = 555
        Me.chkTradeIn.Text = "Trade In"
        Me.chkTradeIn.UseVisualStyleBackColor = False
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
        DataGridViewCellStyle2.Format = "N2"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.colSMC.DefaultCellStyle = DataGridViewCellStyle2
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
        DataGridViewCellStyle3.Format = "N2"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.colTotalValue.DefaultCellStyle = DataGridViewCellStyle3
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
        'colDetailDiscount
        '
        Me.colDetailDiscount.HeaderText = "DetailDiscount"
        Me.colDetailDiscount.Name = "colDetailDiscount"
        Me.colDetailDiscount.ReadOnly = True
        Me.colDetailDiscount.Visible = False
        '
        'frmGPOS
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1018, 672)
        Me.Controls.Add(Me.chkTradeIn)
        Me.Controls.Add(Me.grpImage)
        Me.Controls.Add(Me.chkShowImage)
        Me.Controls.Add(Me.lblMemberPoints)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.chkeTRS)
        Me.Controls.Add(Me.grdMarketRate)
        Me.Controls.Add(Me.pnlCostAdminPwd)
        Me.Controls.Add(Me.pnlBillCancel)
        Me.Controls.Add(Me.pnlMember)
        Me.Controls.Add(Me.PnlReprint)
        Me.Controls.Add(Me.pnlAdminPwd)
        Me.Controls.Add(Me.pnlTotal)
        Me.Controls.Add(Me.lblCompanyAddress)
        Me.Controls.Add(Me.pnlPaymentType)
        Me.Controls.Add(Me.pnlExpense)
        Me.Controls.Add(Me.pnlFindCost)
        Me.Controls.Add(Me.lblCompanyName)
        Me.Controls.Add(Me.PnlNetAmount)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.grdSales)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Menu = Me.MainMenu1
        Me.MinimizeBox = False
        Me.Name = "frmGPOS"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "POS"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.PnlReprint.ResumeLayout(False)
        Me.PnlReprint.PerformLayout()
        Me.pnlBillCancel.ResumeLayout(False)
        Me.pnlBillCancel.PerformLayout()
        Me.pnlAdminPwd.ResumeLayout(False)
        Me.pnlAdminPwd.PerformLayout()
        Me.pnlPaymentType.ResumeLayout(False)
        Me.pnlPaymentType.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.pnlExpense.ResumeLayout(False)
        Me.pnlExpense.PerformLayout()
        Me.pnlTotal.ResumeLayout(False)
        Me.pnlTotal.PerformLayout()
        Me.pnlBillCancelAdminPwd.ResumeLayout(False)
        Me.pnlBillCancelAdminPwd.PerformLayout()
        Me.grpType.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlNetAmount.ResumeLayout(False)
        Me.PnlNetAmount.PerformLayout()
        Me.pnlFindCost.ResumeLayout(False)
        Me.pnlFindCost.PerformLayout()
        Me.pnlMember.ResumeLayout(False)
        Me.pnlMember.PerformLayout()
        Me.pnlCostAdminPwd.ResumeLayout(False)
        Me.pnlCostAdminPwd.PerformLayout()
        CType(Me.grdSales, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdMarketRate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel8.ResumeLayout(False)
        Me.grpImage.ResumeLayout(False)
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region


    Dim sClassCode As String
    Public msClsSalesType As String
    Public tempTV As Double
    Public tempSMC As Double
    Public Diff As Double
    Public NTotVal As Double
    Public NSMC As Double
    Dim sCustomerCode As String
    Dim bEdited As Boolean
    Public sSalesType As String
    Dim sCompanyCode As String
    Dim sTerminalCode As String
    Dim sLocationCode As String
    Dim sInvoiceNo As String
    Dim sSalesManCode As String
    Dim sGSTType As String
    Dim dTotalCost As Double
    Dim dGSTPerc As Double
    Dim dDisPercentage As Double
    Dim sDiscountType As String
    Dim bReprint As Boolean
    Dim CompName$, Address1$, Address2$, Country$, PhoneNo$, FaxNo$
    Dim mdTradeInAmt As Double, mdDepositAmt As Double
    Dim nAllowSalesReturn As Integer
    Dim sUserCode As String
    Dim sUserName As String

    Dim msInsAgreementNo$
    Dim mdCommission#, mdPurMC#
    Dim dDailyClassRate As Double
    Dim sPrinterType$, dCustomerDisplayPort#, dCashDrawerInterface#, dCashDrawerPort#
    Dim bCustomerDisplay As Boolean, bPrinter As Boolean
    Const GRDROWHT = 17.708
    Dim bVoiceEnabled As Boolean
    Dim mdTotalCommission As Double
    Dim dMarketCost As Double
    Dim dTotalWeight, dTotalGoldLoss, dTotalSMC, dRate As Double
    Dim bPassword As Boolean
    Dim bBCPassword As Boolean
    Dim sSDate As String
    Dim dActualSales#, dNetDiscount#, dNetAmount#
    Dim dGst As Double
    Dim dLossPer As Double
    Dim dFindTotalMarketPrice As Double
    Dim dNetTotalMarketPrice As Double
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

    Const MRClassCode = 1
    Const MRRate = 2
    'class objects
    Dim objComp As New clsCompany
    Dim objTerminal As New clsTerminal
    Dim objClass As New ClsClass
    Dim objClassRate As New ClsClass
    Dim objSM As New clsSalesMan
    Dim objCust As New clsCustomer
    Dim objSales As New ClsSales
    Dim objPaymode As New clsPayMode
    Dim objInv As New clsInventory
    Dim objSupplier As New clsSuppliers
    Dim objIns As New ClsInstallment
    Dim objSalesReturn As New ClsSalesReturn
    Dim objUM As New clsUserMasters
    Dim objMember As New clsMember
    Dim sInvGSTType As String
    Dim sSalesPcs As Boolean

    Dim dAdvancePaid As Double
    Dim sOrderRefNo As String
    Dim dTradeInGST As Double
    Dim dTotalOrgCost As Double

    'Const ICode = 1
    'Const IClass = 2
    'Const IDescription = 3
    'Const IWeight = 4
    'Const IGoldLoss = 5
    'Const ICostPerGram = 6
    'Const IDSWeight = 7
    'Const ISMC = 8
    'Const IPCS = 9
    'Const ITotalValue = 10
    'Const ITotalCost = 11
    'Const ISalesType = 12
    'Const IPMC = 13
    'Const IGST = 14
    'Const IDiscount = 15

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
    Public dWeightGrd, dGoldLossGrd, dStoneWeightGrd, dMCGrd, dPCSGrd, dTotalCostGrd, dTotalValueGrd As Double
    Dim seTRSType As String
    Dim sBarLocationCode, sBarInvoiceNo, sBarInvoiceDate, sBarClassCode, sBarInventoryCode As String
    Dim dBarWeight, dBarStoneWeight, dBarTotalCost As Double
    Dim dRedeemedPoints As Integer
    Dim sCataloguecode As String
    Dim sCategorycode As String
    Dim sCostChangeSalesman As String
    Dim dCataloguediscount As Integer
    Dim dSellingPrice As Integer
    Dim sPassportNo As String
    'Public Property TradeInAmt(ByVal dTradeInAmt As Double)
    '    Get
    '        mdTradeInAmt = dTradeInAmt
    '    End Get
    '    Set(ByVal Value)

    '    End Set
    'End Property
    Public WriteOnly Property InsAgreementNo() As String
        Set(ByVal Value As String)
            msInsAgreementNo = Value
        End Set
    End Property

    Public WriteOnly Property DepositAmt() As String
        Set(ByVal Value As String)
            mdDepositAmt = Value
        End Set
    End Property


    Public Sub TradeInAmt(ByVal dTradeInAmt As Double)
        mdTradeInAmt = dTradeInAmt
    End Sub

    Private Sub frmGPOS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            biflag = False
            bTradeinFlag = False
            grdSales.RowCount = 0
            txtIDD.Text = Format(CDate(ServerDate), "dd")
            txtIMM.Text = Format(CDate(ServerDate), "MM")
            txtIYYYY.Text = Format(CDate(ServerDate), "yyyy")
            subGetCompany()
            subLoadTerminalParameter()
            cboExpensesType.SelectedIndex = 0
            subShowClassMarketRate()
            lblCash.Visible = False
            lblInstalment.Visible = False
            lbltradeIN.Visible = False
            dFlexiFinalAmount = 0
            dFlexiFinalBonus = 0
            If Trim(Objuser.CompanyName) = "ABIRAAME JEWELLERS" Then
                Panel8.Visible = False
            Else
                Panel8.Visible = True
            End If
            If Trim(sCompanyShortCode) = "MJ" Then
                lblId.Text = "Inv.No"
            End If
            lblSManTotal.Visible = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subGetCompany()
        Dim dsComp As New DataSet
        Try
            dsComp = objComp.GetCompanyList()
            If dsComp.Tables(0).Rows.Count > 0 Then
                sCompanyCode = Trim(dsComp.Tables(0).Rows(0)("CompanyID") & "")
                sLocationCode = gthisLocation
                sTerminalCode = ""
                lblLocation.Text = gthisLocation
                lblTerminal.Text = ""
                dGSTPerc = Val(dsComp.Tables(0).Rows(0)("GstPerc") & "")
                dDisPercentage = Val(Trim(dsComp.Tables(0).Rows(0)("MaxDiscountAllowed") & ""))
                'tradein limit
                dTradeinLimit = Val(Trim(dsComp.Tables(0).Rows(0)("tradeinLimit") & ""))
                'reqd for report.
                CompName = Trim(dsComp.Tables(0).Rows(0)("Name") & "")
                Address1 = Trim(dsComp.Tables(0).Rows(0)("Address1") & "")
                Address2 = Trim(dsComp.Tables(0).Rows(0)("Address2") & "")
                Country = Trim(dsComp.Tables(0).Rows(0)("Country") & "")
                PhoneNo = Trim(dsComp.Tables(0).Rows(0)("Phone") & "")
                FaxNo = Trim(dsComp.Tables(0).Rows(0)("Fax") & "")
                lblCompanyName.Text = CompName
                lblCompanyAddress.Text = Address1 & ", " & Address2 & vbCrLf & " Phone : " & PhoneNo & " Fax : " & FaxNo
            Else
                MessageBox.Show("Locked.")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsComp = Nothing
        End Try
    End Sub

    Private Sub subLoadTerminalParameter()
        Dim iCnt As Integer
        Dim dsTerminal As New DataSet
        Try
            dsTerminal = objTerminal.GetTerminal(gTerminalCode)
            If dsTerminal.Tables(0).Rows.Count > 0 Then
                sPrinterType = Trim(dsTerminal.Tables(0).Rows(0)("PrinterType") & "")
                dCustomerDisplayPort = Val(dsTerminal.Tables(0).Rows(0)("cusdisplayportno") & "")
                If Val(dsTerminal.Tables(0).Rows(0)("PrinterAttached") & "") = 1 Then
                    bPrinter = True
                Else
                    bPrinter = False
                End If
                If Val(dsTerminal.Tables(0).Rows(0)("VoiceEnabled") & "") = 1 Then
                    bVoiceEnabled = True
                Else
                    bVoiceEnabled = False
                End If
            Else
                MessageBox.Show("Your Computer is not Configured")
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsTerminal = Nothing
        End Try
    End Sub

    Private Sub subShowClassMarketRate()
        Try
            Dim dsClass As New DataSet
            Dim iCnt As Long
            dsClass = objClass.GetClassDetail("")
            If Not IsNothing(dsClass) Then
                If dsClass.Tables("Class").Rows.Count > 0 Then
                    grdMarketRate.RowCount = 0
                    For iCnt = 0 To dsClass.Tables("CLASS").Rows.Count - 1
                        If Trim(dsClass.Tables("Class").Rows(iCnt)("DisplayCost") & "") = "Y" Then
                            grdMarketRate.Rows.Add()
                            fnSetdgvText(grdMarketRate, grdMarketRate.RowCount - 1, 0, Trim(dsClass.Tables("Class").Rows(iCnt)("ClassCode") & ""))
                            fnSetdgvText(grdMarketRate, grdMarketRate.RowCount - 1, 1, Format(Val(Trim(dsClass.Tables("Class").Rows(iCnt)("Rate") & "")), "0.00"))
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmGPOS_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Escape
                    If grdSales.RowCount > 0 Then
                        If (MsgBox("There are some items in the list. Do you still want to close? ", vbYesNo)) = vbNo Then
                            Exit Sub
                        Else
                            subExit()
                        End If
                    Else
                        subExit()
                    End If
                Case Keys.Return
                    If pnlPaymentType.Visible = False Then SendKeys.Send("{TAB}")
                Case Keys.F8
                    If txtInventoryCode.Enabled = True Then txtInventoryCode.Focus()
                Case Keys.F2
                    If Trim(UCase(sCompanyShortCode)) = "HN" Then
                        If grdSales.RowCount > 0 Then
                            subClear()
                        End If
                    End If

            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnPMClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPMClear.Click
        Try
            pnlTotal.Visible = False
            txtInventoryCode.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtSalesManPassword_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSalesManPassword.Leave
        Try
            If txtSalesManPassword.Text.Trim <> "" Then
                subGetSalesManPassword()
            Else
                MessageBox.Show("Please enter Sales Man password.")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subGetSalesManPassword()
        Dim dsSMPWD As New DataSet
        Dim dsInvoiceNo As New DataSet
        Dim dsTotalSalesAmt As New DataSet
        Dim sDate As String
        Dim sSalesManGroup As String
        sDate = CStr(Format(CDate(ServerDate), "yyyy") + Format(CDate(ServerDate), "MM") + Format(CDate(ServerDate), "dd"))
        Try
            dsSMPWD = objSM.GetSalesManwithPassword(txtSalesManPassword.Text.Trim)
            If dsSMPWD.Tables(0).Rows.Count > 0 Then
                txtSalesManName.Text = Trim(dsSMPWD.Tables(0).Rows(0)("SalesManName") & "")
                sSalesManCode = Trim(dsSMPWD.Tables(0).Rows(0)("SalesmanCode") & "")
                sManCode = Trim(dsSMPWD.Tables(0).Rows(0)("SalesmanCode") & "")
                lblLastSalesMan.Text = Trim(dsSMPWD.Tables(0).Rows(0)("SalesManName") & "")
                lblLastBillNo.Text = Trim(dsSMPWD.Tables(0).Rows(0)("InvoiceNo") & "")
                lblLastBillAmount.Text = Trim(dsSMPWD.Tables(0).Rows(0)("LastSalesAmount") & "")
                nAllowSalesReturn = dsSMPWD.Tables(0).Rows(0)("AllowSalesReturn") & ""
                sSalesManGroup = Trim(dsSMPWD.Tables(0).Rows(0)("SalesManGroup") & "")
                If UCase(sCompanyShortCode) = "HN" Then
                    If UCase(txtCustomerCode.Text.Trim) = "" Then
                        txtCustomerCode.Text = "CASH"
                        txtCustomerName.Text = "CASH CUSTOMER"
                        If txtInventoryCode.Enabled = True Then txtInventoryCode.Focus()
                    End If
                ElseIf UCase(sCompanyShortCode) = "MJ" Then
                    If UCase(txtCustomerCode.Text.Trim) = "" Then
                        txtCustomerCode.Text = "CASH"
                    End If
                Else
                    If UCase(txtCustomerCode.Text.Trim) = "" Then
                        txtCustomerCode.Text = "CASH"
                        txtCustomerName.Text = "CASH CUSTOMER"
                        If txtInventoryCode.Enabled = True Then txtInventoryCode.Focus()
                    End If
                End If
                'Shabeer 20150226
                If Trim(sSalesManGroup) = "ADMIN" Or Trim(sSalesManGroup) = "SUPER ADMIN" Then
                    optZeroRated.Enabled = True
                    optInclusive.Enabled = True
                    optExclusive.Enabled = False
                Else
                    optZeroRated.Enabled = False
                    optInclusive.Enabled = False
                    optExclusive.Enabled = False
                End If
                'Shabeer 20150226
            Else
                MessageBox.Show("Invalid Password.")
                txtSalesManPassword.Clear()
                txtSalesManPassword.Focus()
            End If
            dsInvoiceNo = objSales.GetUpdateInvoice()
            If dsInvoiceNo.Tables(0).Rows.Count > 0 Then
                lblNextInvoiceNo.Text = Trim(dsInvoiceNo.Tables(0).Rows(0)("NextInvoice") & "")
            Else
                lblNextInvoiceNo.Text = ""
            End If
            dsTotalSalesAmt = objSM.GetSalesManSalesManTotalAmt(sSalesManCode, sDate)
            If dsTotalSalesAmt.Tables(0).Rows.Count > 0 Then
                lblSManTotal.Text = Trim(dsTotalSalesAmt.Tables(0).Rows(0)("TotalAmount") & "0")
            Else
                lblSManTotal.Text = "0"
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsSMPWD = Nothing
            dsInvoiceNo = Nothing
        End Try
    End Sub

    Private Sub txtCustomerCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustomerCode.LostFocus

        Try

            If txtCustomerCode.Text.Trim = "" Then txtCustomerCode.Text = "CASH"

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
                subGetCustomerDetail()
                If txtSalesManPassword.Text.Trim <> "" Then
                    If txtInventoryCode.Enabled = True Then txtInventoryCode.Focus()
                End If
            End If
            If opteTrsTicketIssued.Checked Then txtRemarks.Text = Trim(txtCustomerCode.Text)
            subGetCustomerDetail()
            sCustomerCode = Trim(txtCustomerCode.Text)
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

    End Sub

    Private Sub subGetCustomerDetail()
        Dim dsCust As New DataSet
        Dim dsCustPoints As New DataSet
        Dim sEmail As String

        Try
            If txtCustomerCode.Text.Trim <> "" Then
                sCustomerCode = Trim(txtCustomerCode.Text.Trim)
                dsCust = objCust.GetCustomerwithCodeHQ(txtCustomerCode.Text.Trim)
                If dsCust.Tables(0).Rows.Count > 0 Then
                    txtCustomerName.Text = Trim(dsCust.Tables(0).Rows(0)("CustomerName") & "")
                    lblCustomerType.Text = Trim(dsCust.Tables(0).Rows(0)("CustomerType") & "")
                    txtAddress1.Text = Trim(dsCust.Tables(0).Rows(0)("Address1") & "")
                    txtAddress2.Text = Trim(dsCust.Tables(0).Rows(0)("Address2") & "")
                    txtAddress3.Text = Trim(dsCust.Tables(0).Rows(0)("Address3") & "")
                    sPassportNo = Trim(dsCust.Tables(0).Rows(0)("NricNo") & "")
                    'sEmail = Trim(dsCust.Tables(0).Rows(0)("eMail") & "")
                    'lblMemberPoints.Text = Val(dsCust.Tables(0).Rows(0)("TotalRewardPoints") & "") - Val(dsCust.Tables(0).Rows(0)("TotalRedeemedPoints") & "")

                    'If Trim(sEmail) = "" Then

                    'End If

                    dsCustPoints = objCust.GetCustomerPointswithCodeHQ(txtCustomerCode.Text.Trim)
                    If dsCustPoints.Tables(0).Rows.Count > 0 Then
                        lblMemberPoints.Text = Val(dsCustPoints.Tables(0).Rows(0)("RewardPoints") & "") - Val(dsCustPoints.Tables(0).Rows(0)("Redeemedpoints") & "")
                    End If
                    txtInventoryCode.Focus()
                    Exit Sub
                Else
                    txtCustomerCode.Text = "CASH"
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsCust = Nothing
        End Try
    End Sub
    'Private Sub subGetMemberDetail()
    '    Dim dsCust As New DataSet
    '    Try
    '        If txtMemberCode.Text.Trim <> "" Then
    '            sCustomerCode = Trim(txtCustomerCode.Text.Trim)
    '            dsCust = objCust.GetCustomerwithCode(txtCustomerCode.Text.Trim)
    '            If dsCust.Tables(0).Rows.Count > 0 Then
    '                txtCustomerName.Text = Trim(dsCust.Tables(0).Rows(0)("CustomerName") & "")
    '                lblCustomerType.Text = Trim(dsCust.Tables(0).Rows(0)("CustomerType") & "")
    '                txtAddress1.Text = Trim(dsCust.Tables(0).Rows(0)("Address1") & "")
    '                txtAddress2.Text = Trim(dsCust.Tables(0).Rows(0)("Address2") & "")
    '                txtAddress3.Text = Trim(dsCust.Tables(0).Rows(0)("Address3") & "")
    '                txtInventoryCode.Focus()
    '                Exit Sub
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    Finally
    '        dsCust = Nothing
    '    End Try
    'End Sub
    Private Sub txtInventoryCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInventoryCode.GotFocus
        txtInventoryCode.SelectAll()
    End Sub

    Private Sub txtInventoryCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtInventoryCode.KeyDown
        Dim iRow As Long
        Try
            'If bEdited = True And e.KeyCode = Keys.Delete Then
            '    grdSales.Row = grdSales.ActiveRow
            '    grdSales.Action = 5
            '    grdSales.MaxRows = grdSales.MaxRows - 1
            '    If grdSales.MaxRows = 1 Then
            '        grdSales.MaxRows = 0
            '    Else
            '        subTotal()
            '    End If
            '    subClearDetail()
            'End If

            'If bEdited = True And e.KeyCode = Keys.Delete Then
            '    grdSales.Row = grdSales.CurrentRow.Index
            '    grdSales.Action = 5
            '    grdSales.RowCount = grdSales.RowCount - 1
            '    If grdSales.RowCount = 1 Then
            '        grdSales.RowCount = 0
            '    Else
            '        subTotal()
            '    End If
            '    subClearDetail()
            'End If

            Select Case e.KeyCode
                Case Keys.F1
                    'If UCase(sCompanyShortCode) <> "ES" Then
                    If UCase(sCompanyShortCode) <> "MD" Then
                        pnlFindCost.Visible = True
                        txtFindInventory.Focus()
                    End If

                Case Keys.F6 'reprint
                    If Trim(txtSalesManPassword.Text) <> "" Then
                        subGetSalesManPassword()
                    Else
                        Exit Sub
                    End If
                    '''subGetAdminPasword()
                    If Trim(txtSalesManName.Text) <> "" Then
                        subReprint()
                    End If
                Case Keys.F2 ' clear
                    If grdSales.RowCount > 0 Then
                        If (MsgBox("There are some items in the list. Do you still want to clear? ", vbYesNo)) = vbNo Then
                            Exit Sub
                        End If
                    End If
                    subClear()
                Case Keys.F3 'Bill Cancel
                    If Trim(txtSalesManPassword.Text) <> "" Then
                        subGetSalesManPassword()
                    Else
                        Exit Sub
                    End If
                    '''subGetBCAdminPasword()
                    ''' subBillCancel()
                    Dim objSalesMan As New clsSalesMan
                    If objSalesMan.SalesManLogSave(sSalesManCode, txtSalesManName.Text.Trim, "Bill Cancel", nAllowSalesReturn) Then
                    Else
                        MessageBox.Show("Error in Log Save")
                    End If
                    If Trim(txtSalesManName.Text) <> "" And nAllowSalesReturn = 1 Then
                        subBillCancel()
                    Else
                        MsgBox("SalesMan Has No Rights to do Bill Cancel!", MsgBoxStyle.Critical)
                        txtSalesManName.Text = ""
                        txtSalesManPassword.Text = ""
                        If txtSalesManPassword.Enabled Then txtSalesManPassword.Focus()
                    End If
                    'If Trim(txtSalesManName.Text) <> "" Then
                    '    subBillCancel()
                    'End If
                Case Keys.F4 'save or tender
                    If Trim(txtSalesManPassword.Text) <> "" Then
                        subGetSalesManPassword()
                    Else
                        Exit Sub
                    End If
                    If (UCase(Objuser.CompanyName)) = "APJ JUWELLER PTE LTD" Then
                        If Val(lblSalesAmount.Text) >= 300 Then
                            If Trim(txtCustomerCode.Text) = "CASH" Then
                                MsgBox("Enter Mobile Number to Proceed", MsgBoxStyle.Critical)
                                If txtCustomerCode.Enabled Then txtCustomerCode.Focus()
                                Exit Sub
                            End If
                        End If
                    End If

                    If (UCase(Objuser.CompanyName)) = "ABIRAAME POPULAR JEWELLERS (S) PTE LTD" Then
                        If Trim(sPassportNo) <> Trim(txtRemarks.Text) And opteTrsTicketIssued.Checked = True Then
                            MsgBox("Passport No Mismatch! Enter Correct Passport No", MsgBoxStyle.Critical)
                            If txtRemarks.Enabled Then txtRemarks.Focus()
                            Exit Sub
                        End If
                    End If
                    If Trim(txtSalesManName.Text) <> "" Then
                        If (UCase(Objuser.CompanyName)) = "ABIRAAME POPULAR JEWELLERS (S) PTE LTD" Then
                            If grdSales.RowCount > 0 And Trim(txtRemarks.Text) = "" Or (opteTrsTicketIssued.Checked = False And Trim(txtRemarks.Text) <> "") Then
                                If (MsgBox("Is this Customer tourist? Do You want to enter eTRS? ", vbYesNo)) = vbYes Then
                                    If txtRemarks.Enabled Then txtRemarks.Focus()
                                    Exit Sub
                                End If
                                If (MsgBox("Do you want to Proceed Payment? ", vbYesNo)) = vbNo Then
                                    Exit Sub
                                End If
                            End If
                        ElseIf (UCase(Objuser.CompanyName)) = "ABIRAAME JEWELLERS" Then
                            If (MsgBox("Do you want to Proceed Payment? ", vbYesNo)) = vbNo Then
                                Exit Sub
                            End If
                        End If
                        subTender()
                    End If
                Case Keys.F5 'Net Total
                        If Val(lblSalesAmount.Text) > 0 Then
                            subNetDiscount()
                        End If
                Case Keys.Delete
                        iRow = grdSales.CurrentRow.Index
                        grdSales.Rows.RemoveAt(iRow)
                        bEdited = True
                        subTotalFromGrid()
                        subTotal()
                        bEdited = False
                Case Keys.Escape
                        If grdSales.RowCount > 0 Then
                            If (MsgBox("There are some items in the list. Do you still want to close? ", vbYesNo)) = vbNo Then
                                Exit Sub
                            Else
                                subExit()
                            End If
                        Else
                            subExit()
                        End If
                        'Case Keys.Escape
                        '    If PnlNetAmount.Visible = True Then
                        '        PnlNetAmount.Visible = False
                        '        txtInventoryCode.Focus()
                        '        Exit Sub
                        'End If
                Case Keys.PageDown
                        If Trim(txtSalesManPassword.Text) <> "" Then
                            subGetSalesManPassword()
                        Else
                            Exit Sub
                        End If
                        If Trim(txtSalesManName.Text) <> "" Then
                            subExpenses()
                        End If
                        '        Case vbKeyF7:
                        '            If Trim(txtSalesManPassword) <> "" Then
                        '                subGetSalesManPassword
                        '            Else
                        '                Exit Sub
                        '            End If
                        '            If Trim(txtSalesManName) <> "" Then
                        '                subInstallment
                        '            End If
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subReprint()
        Try
            PnlReprint.Visible = True
            txtRePrintBillNo.Text = ""
            txtRePrintBillNo.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Function subTotalFromGrid()
        Dim icnt
        Dim sInventoryCode As String
        dWeightGrd = 0
        dGoldLossGrd = 0
        dMCGrd = 0
        dPCSGrd = 0
        dTotalCostGrd = 0
        dStoneWeightGrd = 0
        dTotalValueGrd = 0
        For icnt = 0 To grdSales.RowCount - 1
            sInventoryCode = Trim(fnGetdgvText(grdSales, icnt, ICode))
            If sInventoryCode <> "TOTAL:" Then
                dWeightGrd = Format(Val(dWeightGrd) + Val(fnGetdgvText(grdSales, icnt, IWeight)), "0.00")
                dGoldLossGrd = Format(Val(dGoldLossGrd) + Val(fnGetdgvText(grdSales, icnt, IGoldLoss)), "0.00")
                dMCGrd = Format(Val(dMCGrd) + Val(fnGetdgvText(grdSales, icnt, ISMC)), "0.00")
                dPCSGrd = Format(Val(dPCSGrd) + Val(fnGetdgvText(grdSales, icnt, IPCS)), "0.00")
                dTotalCostGrd = Format(Val(dTotalCostGrd) + Val(fnGetdgvText(grdSales, icnt, ITotalValue)), "0.00")
                dStoneWeightGrd = Format(Val(dStoneWeightGrd) + Val(fnGetdgvText(grdSales, icnt, IDSWeight)), "0.00")
                dTotalValueGrd = Format(Val(dTotalValueGrd) + Val(fnGetdgvText(grdSales, icnt, ITotalCost)), "0.00")
            End If
        Next
    End Function
    Private Sub subTotal()
        'Try
        '    If Not bEdited Then
        '        grdSales.MaxRows = grdSales.MaxRows + 1
        '    End If

        '    'grdSales.RowHeight(grdSales.MaxRows) = GRDROWHT

        '    Call SetText(grdSales, grdSales.MaxRows, ICode, "Total:")

        '    grdSales.ForeColor = Color.Red
        '    grdSales.FontBold = True

        '    grdSales.Col = IWeight
        '    grdSales.Formula = "sum(d1:d" & grdSales.MaxRows - 1 & ")"


        '    grdSales.ForeColor = Color.Red
        '    grdSales.FontBold = True

        '    grdSales.Col = IGoldLoss
        '    grdSales.Formula = "sum(e1:e" & grdSales.MaxRows - 1 & ")"

        '    grdSales.ForeColor = Color.Red
        '    grdSales.FontBold = True

        '    grdSales.Col = IDSWeight
        '    grdSales.Formula = "sum(g1:g" & grdSales.MaxRows - 1 & ")"

        '    grdSales.ForeColor = Color.Red
        '    grdSales.FontBold = True

        '    grdSales.Col = ISMC
        '    grdSales.Formula = "sum(h1:h" & grdSales.MaxRows - 1 & ")"

        '    grdSales.ForeColor = Color.Red
        '    grdSales.FontBold = True

        '    grdSales.Col = IPCS
        '    grdSales.Formula = "sum(i1:i" & grdSales.MaxRows - 1 & ")"


        '    grdSales.ForeColor = Color.Red
        '    grdSales.FontBold = True

        '    grdSales.Col = ITotalValue
        '    grdSales.Formula = "sum(j1:j" & grdSales.MaxRows - 1 & ")"


        '    grdSales.ForeColor = Color.Blue
        '    grdSales.FontBold = True
        '    'grdSales.FontSize = 11

        '    grdSales.Col = ITotalCost
        '    grdSales.Formula = "sum(k1:k" & grdSales.MaxRows - 1 & ")"


        '    grdSales.ForeColor = Color.Red
        '    grdSales.FontBold = True

        '    'fill the bill label values.
        '    If grdSales.MaxRows > 1 Then
        '        dTotalWeight = Val(GetText(grdSales, grdSales.MaxRows, IWeight))
        '        dTotalGoldLoss = Val(GetText(grdSales, grdSales.MaxRows, IGoldLoss))
        '        dTotalSMC = Val(GetText(grdSales, grdSales.MaxRows, ISMC))
        '        lblQty.Text = Format(Val(GetText(grdSales, grdSales.MaxRows, IPCS)), "0")
        '        lblSalesAmount.Text = Format(Val(GetText(grdSales, grdSales.MaxRows, ITotalValue)), "0.00")
        '        If Trim(lblNetDiscount.Text.Trim) = "" Then
        '            lblNetDiscount.Text = Format("0.00")
        '        Else
        '            lblNetDiscount.Text = Format(Val(lblNetDiscount.Text), "0.00")
        '        End If
        '        lblNetValue.Text = Format(Val(lblSalesAmount.Text) - Val(lblNetDiscount.Text), "0.00")

        '        '  If UCase(sSalesType) <> "TRADEIN" Then
        '        subGetGSTAmount()
        '        'Else
        '        '
        '        'End If
        '    Else
        '        lblQty.Text = Format("0")
        '        lblSalesAmount.Text = Format("0.00")
        '        lblNetDiscount.Text = Format("0.00")
        '        lblNetValue.Text = Format("0.00")
        '        lblTax.Text = Format("0.00")
        '        lblNetTotal.Text = Format("0.00")
        '    End If

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
        Try
            If Not bEdited Then
                grdSales.Rows.Add()
            End If

            Call fnSetdgvText(grdSales, grdSales.RowCount - 1, ICode, "TOTAL:")
            Call fnSetdgvText(grdSales, grdSales.RowCount - 1, IWeight, Format(dWeightGrd, "0.00"))
            Call fnSetdgvText(grdSales, grdSales.RowCount - 1, IGoldLoss, Format(dGoldLossGrd, "0.00"))
            Call fnSetdgvText(grdSales, grdSales.RowCount - 1, ISMC, Format(dMCGrd, "0.00"))
            Call fnSetdgvText(grdSales, grdSales.RowCount - 1, IPCS, dPCSGrd)
            Call fnSetdgvText(grdSales, grdSales.RowCount - 1, ITotalValue, Format(dTotalCostGrd, "0.00"))
            Call fnSetdgvText(grdSales, grdSales.RowCount - 1, IDSWeight, Format(dStoneWeightGrd, "0.00"))
            Call fnSetdgvText(grdSales, grdSales.RowCount - 1, ITotalCost, Format(dTotalValueGrd, "0.00"))

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


            'fill the bill label values.
            If grdSales.RowCount > 1 Then
                dTotalWeight = Val(fnGetdgvText(grdSales, grdSales.RowCount - 1, IWeight))
                dTotalGoldLoss = Val(fnGetdgvText(grdSales, grdSales.RowCount - 1, IGoldLoss))
                dTotalSMC = Val(fnGetdgvText(grdSales, grdSales.RowCount - 1, ISMC))
                lblQty.Text = Format(Val(fnGetdgvText(grdSales, grdSales.RowCount - 1, IPCS)), "0")
                lblSalesAmount.Text = Format(Val(fnGetdgvText(grdSales, grdSales.RowCount - 1, ITotalValue)), "0.00")
                If Trim(lblNetDiscount.Text.Trim) = "" Then
                    lblNetDiscount.Text = Format("0.00")
                Else
                    lblNetDiscount.Text = Format(Val(lblNetDiscount.Text), "0.00")
                End If
                lblNetValue.Text = Format(Val(lblSalesAmount.Text) - Val(lblNetDiscount.Text), "0.00")

                'Shabeer 20250916
                If UCase(sSalesType) <> "TRADEIN" Then
                    subGetGSTAmount()
                Else
                    sInvGSTType = ""
                    optExclusive.Checked = True
                End If
                'Shabeer 20250916
            Else
                lblQty.Text = Format("0")
                lblSalesAmount.Text = Format("0.00")
                lblNetDiscount.Text = Format("0.00")
                lblNetValue.Text = Format("0.00")
                lblTax.Text = Format("0.00")
                lblNetTotal.Text = Format("0.00")
            End If
            subGetGSTAmount()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Function subGetTotalForGrid()
        dWeightGrd = Format(dWeightGrd + Val(txtWeight.Text), "0.00")
        dGoldLossGrd = Format(dGoldLossGrd + Val(txtGoldLoss.Text), "0.00")
        dMCGrd = Format(dMCGrd + Val(txtSMC.Text), "0.00")
        dPCSGrd = Format(dPCSGrd + Val(txtPCS.Text), "0.00")
        dTotalCostGrd = Format(dTotalCostGrd + Val(txtTotalValue.Text), "0.00")
    End Function
    Private Sub subClearDetail()
        Try
            bEdited = False
            txtInventoryCode.Clear()
            txtInventoryDescription.Clear()
            txtWeight.Clear()
            txtGoldLoss.Clear()
            txtSMC.Clear()
            mdPurMC = 0
            txtCostPerGram.Clear()
            txtPCS.Clear()
            txtTotalValue.Clear()
            sClassCode = ""
            txtStoneWeight.Clear()
            dTotalCost = 0
            msClsSalesType = ""
            txtGST.Clear()
            NTotVal = 0
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub subClear()
        Try
            'SHabeer 20140911
            ''optZeroRated.Enabled = True
            ''optInclusive.Enabled = True
            ''optExclusive.Enabled = True
            'SHabeer 20140911
            optZeroRated.Enabled = False
            optInclusive.Enabled = False
            optExclusive.Enabled = False
            optInclusive.Checked = True
            txtCustomerCode.Text = "CASH"
            txtMemberCode.Text = ""
            lblCustomerType.Text = "Type"
            lblMemberPoints.Text = "Points"
            btnCustomerCodeSearch.Enabled = True
            txtCustomerName.Clear()
            subGetCustomerDetail()
            txtAddress1.Clear()
            txtAddress2.Clear()
            txtAddress3.Clear()
            txtRemarks.Clear()
            grdSales.RowCount = 0
            lblQty.Text = ""
            lblSalesAmount.Text = ""
            lblNetDiscount.Text = ""
            lblNetValue.Text = ""
            lblTax.Text = ""
            lblNetTotal.Text = ""
            lblSManTotal.Text = "0"
            lblNextInvoiceNo.Text = ""
            subClearDetail()
            PnlNetAmount.Visible = False
            txtIDD.Text = Format(ServerDate, "dd")
            txtIMM.Text = Format(ServerDate, "MM")
            txtIYYYY.Text = Format(ServerDate, "yyyy")
            txtNetAmount.Text = Format(0, "0.00")
            mdTotalCommission = 0
            dPayModeCommission = 0
            txtPMSurCharge.Text = "0"
            dTradeInGST = 0
            dTradeInSales = 0
            chkeTRS.Checked = False
            opteTRSNo.Checked = True
            chkPrintMC.Checked = False
            chkPrintImage.Checked = False
            txtPayMode.Clear()
            dRedeemedPoints = 0
            dFlexiFinalAmount = 0
            dFlexiFinalBonus = 0
            txtFlexiAmount.Text = 0
            txtFlexiBonus.Text = 0
            txtSubscriptionID.Text = ""
            sOutCustomerCode = ""
            sOutCustomerName = ""
            sCostChangeSalesman = ""
            sPassportNo = ""
            chkTradeIn.Checked = False
            sSalesType = ""
            txtSalesManPassword.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subBillCancel()
        Try
            pnlBillCancel.Visible = True
            txtBillCancelBillNo.Clear()
            txtBillCancelBillNo.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subTender()
        Try
            If fncValidate() Then
                subCheckMarketRate()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function fncValidate() As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False

            If Trim(txtCustomerCode.Text) = "" Then
                MsgBox("Please enter Customer! ", vbCritical)
                fncValidate = False
                txtCustomerCode.Focus()
                Exit Function
            End If
            If UCase(txtCustomerCode.Text) = "CASH" And sMemberModule = "1" Then
                If txtMemberCode.Text.Trim = "" Then
                    MsgBox("MemberCode is NOT entered !", MsgBoxStyle.Critical)
                    fncValidate = False
                    txtCustomerCode.Focus()
                    Exit Function
                End If
            End If

            If Trim(txtRemarks.Text) = "" And opteTrsTicketIssued.Checked Then
                MsgBox("Please Enter PassportNo! ", vbCritical)
                fncValidate = False
                txtRemarks.Focus()
                Exit Function
            End If
            If Val(lblSalesAmount.Text) = 0 And UCase(sSalesType) <> "TRADEIN" Then
                MsgBox("Enter items for sales! ", vbCritical)
                txtInventoryCode.Focus()
                fncValidate = False
                Exit Function
            End If
            If grdSales.RowCount <= 0 Then
                MsgBox("Enter items for sales! ", vbCritical)
                txtInventoryCode.Focus()
                fncValidate = False
                Exit Function
            End If
            If Val(lblSalesAmount.Text) < 0 Then
                MsgBox("Can not do negative sales. ", vbCritical)
                txtInventoryCode.Focus()
                fncValidate = False
                Exit Function
            End If
            If Not IsDate(Trim(txtIDD.Text) + "/" + Trim(txtIMM.Text) + "/" + Trim(txtIYYYY.Text)) Then
                MsgBox("INVOICE DATE is invalid...!")
                txtIDD.Focus()
                fncValidate = False
                Exit Function
            End If

            'If Trim(txtTerms) = "" Then
            '    MsgBox "Please enter terms of sale! ", vbCritical
            '    fncValidate = False
            '    Exit Function
            'End If
            bStatus = True

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncValidate = bStatus
        End Try
    End Function

    Private Sub subCheckMarketRate()
        Try
            If Not CheckMarketCost() Then Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Function CheckMarketCost() As Boolean
        Try
            Dim sMsg As String
            If UCase(sCompanyShortCode) = "ML" Then
                dNetTotalMarketPrice = CalcMinimumPriceForML()
            Else
                dNetTotalMarketPrice = CalcMarketRate()
            End If

            If UCase(sCompanyShortCode) <> "HN" And UCase(sCompanyShortCode) <> "AJ" Then
                If dNetTotalMarketPrice <= 0 Then
                    CheckMarketCost = False
                    Exit Function
                End If
            End If
            If Val(lblNetTotal.Text) > 0 Then
                '''Block By Durai
                If UCase(sCompanyShortCode) = "HN" And sSalesType <> "TRADEIN" Then
                    If (Val(lblNetTotal.Text) < dNetTotalMarketPrice) Then
                        sMsg = MsgBox("Bill total: $" & Format(Val(lblNetTotal.Text), "#0.00") & " is less than the Cost ", vbInformation)
                        Exit Function
                    End If
                End If
                CheckMarketCost = True
                optCashSales.Checked = True
                '''pnlTotal.Visible = True
                txtPayMode.Text = "CASH"
                txtGrandTotal.Text = Format(Val(lblNetTotal.Text), "0.00") 'Format(Val(lblSalesAmount.Text), "0.00")
                txtPMGST.Text = Format(Val(lblTax.Text), "0.00")
                txtPMDiscount.Text = Format(Val(lblNetDiscount.Text), "0.00")
                txtPMNetTotal.Text = Format(Val(lblNetTotal.Text), "0.00") ' Format(Val(lblNetValue.Text), "0.00")
                txtTotalFinal.Text = Format((Val(txtPMNetTotal.Text) + Val(txtPMSurCharge.Text)), "0.00")
                subPaymentTypeOk()
                'txtPayMode.Clear()
                'txtPayMode.Focus()
                '''If btnPMPayment.Enabled And btnPMPayment.Visible Then btnPMPayment.Focus()
            ElseIf Val(lblNetTotal.Text) = 0 And sSalesType = "TRADEIN" Then
                CheckMarketCost = True
                optCashSales.Checked = True
                pnlTotal.Visible = True
                txtPayMode.Text = "CASH"
                txtGrandTotal.Text = Format(Val(lblSalesAmount.Text), "0.00")
                txtPMGST.Text = Format(Val(lblTax.Text), "0.00")
                txtPMDiscount.Text = Format(Val(lblNetDiscount.Text), "0.00")
                txtPMNetTotal.Text = Format(Val(lblNetValue.Text), "0.00")
                txtTotalFinal.Text = Format((Val(txtPMNetTotal.Text) + Val(txtPMSurCharge.Text)), "0.00")
                If btnPMPayment.Enabled And btnPMPayment.Visible Then btnPMPayment.Focus()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Sub subNetDiscount()
        Try
            If grdSales.RowCount > 1 Then
                PnlNetAmount.Visible = True
                'txtNetDiscountPercentage.SetFocus
                txtNetAmount.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subExit()
        Try
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subExpenses()
        Try
            pnlExpense.Enabled = True
            pnlExpense.Visible = True
            cboExpensesType.Enabled = True
            cboExpensesType.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function CalcMarketRate() As Double

        Dim dsMR As New DataSet
        Dim dTotalWeight As Double
        Dim sClassCode As String
        Dim dRate As Double
        Dim dMCDiscount As Double
        Dim dMarketRate As Double
        Dim dTempTotalMarketPrice As Double
        Dim dSMC As Double
        Dim iCnt As Long
        Dim sSalesDate As String
        Dim dTradeinAmount As Double
        Dim sInventoryCode As String

        Try
            sSalesDate = Trim(txtIDD.Text) + "/" + Trim(txtIMM.Text) + "/" + Trim(txtIYYYY.Text)
            CalcMarketRate = 0
            dFindTotalMarketPrice = 0
            dTempTotalMarketPrice = 0
            dDailyClassRate = 0
            For iCnt = 0 To grdSales.RowCount - 1
                '''If fnGetText(grdSales, iCnt, ISalesType).Trim.ToUpper = "TI" Then
                '''    dTotalWeight = Val(GetText(grdSales, iCnt, IWeight)) + Val(GetText(grdSales, iCnt, IGoldLoss))
                '''    dTradeinAmount = dTotalWeight * Val(fnGetText(grdSales, iCnt, ICostPerGram))
                '''    CalcMarketRate = CalcMarketRate - dTradeinAmount
                '''Else
                If fnGetdgvText(grdSales, iCnt, ISalesType).Trim.ToUpper <> "TI" Then
                    sInventoryCode = Trim(fnGetdgvText(grdSales, iCnt, 0))
                    If sInventoryCode <> "TOTAL:" Then
                        dSMC = Val(fnGetdgvText(grdSales, iCnt, ISMC))
                        sClassCode = Trim(fnGetdgvText(grdSales, iCnt, IClass))
                        dTotalWeight = Val(fnGetdgvText(grdSales, iCnt, IWeight)) + Val(fnGetdgvText(grdSales, iCnt, IGoldLoss))

                        dsMR = objSales.GetMarketRate(sClassCode, sSalesDate)
                        subGetClassSalesType(sClassCode)
                        If dsMR.Tables(0).Rows.Count = 0 Then
                            MessageBox.Show("Market Rate Not Found for this Class. Please set market rate!")
                            Exit Function
                        Else
                            dRate = Val(dsMR.Tables(0).Rows(0)("Rate") & "")
                        End If

                        dMCDiscount = objSales.GetMCDiscoutFromClass(sClassCode)

                        If sCompanyShortCode = "HN" Then
                            If msClsSalesType = "W" Then
                                dMarketRate = (dTotalWeight * dRate) + (dSMC * (1 - (dMCDiscount / 100)))
                            ElseIf msClsSalesType = "B" And UCase(sCompanyShortCode) = "HN" Then 'sarathy 2009-05-07
                                dMarketRate = dSMC
                            ElseIf msClsSalesType = "P" And UCase(sCompanyShortCode) = "HN" Then
                                dMarketRate = dSMC
                            Else
                                dMarketRate = (dSMC * (1 - (dMCDiscount / 100)))
                            End If
                        Else
                            If msClsSalesType <> "P" Then
                                dMarketRate = (dTotalWeight * dRate) + (dSMC * (1 - (dMCDiscount / 100)))
                                '' HAJ AS Pcs Sales 
                            ElseIf msClsSalesType = "P" And UCase(sCompanyShortCode) = "AS" Then
                                dMarketRate = (dTotalWeight * dRate) + (dSMC * (1 - (dMCDiscount / 100)))
                            Else
                                dMarketRate = (dSMC * (1 - (dMCDiscount / 100)))
                            End If
                        End If

                        dDailyClassRate = dRate
                        If msClsSalesType <> "P" Then
                            dTempTotalMarketPrice = (dTotalWeight * dRate) + dSMC
                            '' HAJ AS Pcs Sales 
                        ElseIf msClsSalesType = "P" And UCase(sCompanyShortCode) = "AS" Then
                            dTempTotalMarketPrice = (dTotalWeight * dRate) + dSMC
                        Else
                            dTempTotalMarketPrice = dSMC
                        End If

                        dTempTotalMarketPrice = dTempTotalMarketPrice + (dTempTotalMarketPrice * dGSTPerc / 100)
                        CalcMarketRate = CalcMarketRate + dMarketRate
                        dFindTotalMarketPrice = dFindTotalMarketPrice + dTempTotalMarketPrice
                    End If
                End If
            Next iCnt
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            msClsSalesType = ""
            dsMR = Nothing
        End Try
    End Function

    Private Sub subGetAdminPasword()
        Try
            pnlAdminPwd.Visible = True
            pnlAdminPwd.BringToFront()
            txtAdminPassword.Clear()
            txtAdminPassword.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subGetBCAdminPasword()
        Try
            pnlBillCancelAdminPwd.Visible = True
            pnlBillCancelAdminPwd.BringToFront()
            txtBCAdminPassword.Clear()
            txtBCAdminPassword.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnPaymentTypeOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPaymentTypeOK.Click
        '''subPaymentTypeOk()
    End Sub

    Private Sub subPaymentTypeOk()
        Dim objPM As New frmPayingmode
        Dim objI As New frmInstallment
        Dim objTI As New frmTradeInGold
        Dim bSave As Boolean
        Dim bResult As Boolean
        Dim sFlexiLocation As String
        Try
            dPaidAmount = 0
            ReturnAmount = 0
            mdTradeInAmt = 0
            msInsAgreementNo = ""
            mdDepositAmt = 0
            mdCommission = 0
            bSave = False
            sSubscriptionId = ""
            sAdvDepositNo = ""
            gsInsAgreementNo = ""
            gdDepositAmt = 0
            getServerDate()
            If txtIDD.Text <> Format(CDate(ServerDate), "dd") Then
                txtIDD.Text = Format(CDate(ServerDate), "dd")
                txtIMM.Text = Format(CDate(ServerDate), "MM")
                txtIYYYY.Text = Format(CDate(ServerDate), "yyyy")
            End If
            sSubscriptionId = Trim(txtSubscriptionID.Text)
            sAdvDepositNo = Trim(txtAdvDeposit.Text)

            dFlexiFinalAmount = Val(txtFlexiAmount.Text)
            dFlexiFinalBonus = Val(txtFlexiBonus.Text)

            If optCashSales.Checked = True Then
                sSalesType = "CASH"
                'nTotalNet = Val(lblNetTotal.Text.Trim)
                If Trim(sCompanyShortCode) = "AJ" Then
                    nTotalNet = Val(txtTotalFinal.Text)
                Else
                    nTotalNet = Val(txtTotalFinal.Text)
                End If

                objPM.SalesType = sSalesType
                objPM.lblStart.Text = "Y"
                objPM.dAdvancePaidAmount = dAdvancePaid
                objPM.sOrderRefNo = sOrderRefNo
                objPM.sCustomerType = Trim(lblCustomerType.Text)
                objPM.sCustomerCode = Trim(txtCustomerCode.Text)
                objPM.sRedeemSalesType = Trim(sSalesType)
                objPM.ShowDialog()
                If gPayCancel = True Then
                    If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
                    Exit Sub
                End If
                mdCommission = objPM.Commission
                If dPaidAmount >= 0 Then
                    objSales.subOpenSalesConnection()
                    objSales.subOpenSalesConnectionHQ()
                    If Trim(sCompanyShortCode) = "AJ" Then
                        objSales.subOpenSalesConnectionFlexi()
                    End If

                    Try
                        objSales.subBeginSalesTransaction()
                        objSales.subBeginSalesTransactionHQ()
                        If Trim(sCompanyShortCode) = "AJ" Then
                            objSales.subBeginSalesTransactionFlexi()
                        End If


                        If fncGetInvoiceNo() = False Then
                            MessageBox.Show("Transaction failed!!!")
                            objSales.subRollBackSalesTransaction()
                            pnlPaymentType.Visible = False
                            Exit Try
                        End If
                        If Not objSales.fncUpdateNextInvoiceNo() Then
                            MessageBox.Show("Transaction failed!!!")
                            objSales.subRollBackSalesTransaction()
                            pnlPaymentType.Visible = False
                            Exit Try
                        End If
                        If Not fncPaymodeDetailSave(sInvoiceNo) Then
                            MessageBox.Show("Transaction not saved")
                            objSales.subRollBackSalesTransaction()
                            objPM.Visible = False
                            Exit Try
                        End If
                        If Not fncSaveTradeInSales(sInvoiceNo) Then
                            MessageBox.Show("Transaction not saved")
                            objSales.subRollBackSalesTransaction()
                            objPM.Visible = False
                            Exit Try
                        End If
                        If Not fncSales(sInvoiceNo) Then
                            MessageBox.Show("Transaction not saved")
                            objSales.subRollBackSalesTransaction()
                            objPM.Visible = False
                            Exit Try
                        End If
                        'If Val(dOfferday) <> 1 Then
                        'If Trim(txtSubscriptionID.Text) = "" Then
                        If Not fncPosMemberTransactionSave(sInvoiceNo) Then
                            MessageBox.Show("Transaction not saved")
                            objSales.subRollBackSalesTransactionHQ()
                            objPM.Visible = False
                            Exit Try
                        End If
                        ' End If

                        'End If
                        If Not objSales.fncUpdateDeposit(sDepositNo, sInvoiceNo, Trim(txtIDD.Text) + "/" + Trim(txtIMM.Text) + "/" + Trim(txtIYYYY.Text)) Then
                            MessageBox.Show("Transaction not saved!!!")
                            objSales.subRollBackSalesTransaction()
                            pnlPaymentType.Visible = False
                            Exit Try
                        End If
                        If Not objSales.fncUpdateAdvDeposit(sAdvDepositNo, sInvoiceNo, Trim(txtIDD.Text) + "/" + Trim(txtIMM.Text) + "/" + Trim(txtIYYYY.Text)) Then
                            MessageBox.Show("Transaction not saved!!!")
                            objSales.subRollBackSalesTransaction()
                            pnlPaymentType.Visible = False
                            Exit Try
                        End If
                        If Trim(sCostChangeSalesman) <> "" Then
                            If Not objSales.fncUpdateCostChangeLogSave(sInvoiceNo, Trim(txtIDD.Text) + "/" + Trim(txtIMM.Text) + "/" + Trim(txtIYYYY.Text), Trim(txtSalesManName.Text), Trim(sCostChangeSalesman)) Then
                                MessageBox.Show("Transaction not saved!!!")
                                objSales.subRollBackSalesTransaction()
                                Exit Try
                            End If
                        End If
                        If Trim(sCompanyShortCode) = "AJ" Then
                            sFlexiLocation = Trim(gthisLocation)
                            If Trim(sFlexiLocation) = "99" Then
                                sFlexiLocation = "103"
                            ElseIf Trim(sLocationCode) = "27" Then
                                sFlexiLocation = "1"
                            End If
                            If Trim(txtSubscriptionID.Text) <> "" Then
                                If Not objSales.fncUpdatePayModeFlexiClosure(Trim(txtSubscriptionID.Text), Trim(sFlexiLocation), Trim(sUserID)) Then
                                    MessageBox.Show("Transaction not saved!!!")
                                    objSales.subRollBackSalesTransactionFlexi()
                                    Exit Try
                                End If
                            End If
                        End If




                        objSales.subCommitSalesTransaction()
                        objSales.subCommitSalesTransactionHQ()
                        If Trim(sCompanyShortCode) = "AJ" Then
                            objSales.subCommitSalesTransactionFlexi()
                        End If

                        bSave = True
                    Catch ex As Exception
                        objSales.subRollBackSalesTransaction()
                        objSales.subRollBackSalesTransactionHQ()
                        If Trim(sCompanyShortCode) = "AJ" Then
                            objSales.subRollBackSalesTransactionFlexi()
                        End If

                    Finally
                        objSales.subCloseSalesConnection()
                        objSales.subCloseSalesConnectionHQ()
                        If Trim(sCompanyShortCode) = "AJ" Then
                            objSales.subCloseSalesConnectionFlexi()
                        End If

                    End Try
                    If Not bSave Then Exit Sub
                Else
                    objPM.Visible = False
                    Exit Sub
                End If

            ElseIf optInstallment.Checked = True Then
                If sSalesType = "TRADEIN" And optInstallment.Checked = True Then
                    MessageBox.Show("TradeIn Sales Not Allowed In Installment")
                    Exit Sub
                End If
                sSalesType = "INSTALLMENT"
                objI.sCalledFrom = "POS"
                objI.sInvoiceNo = "" 'Trim(sInvoiceNo)
                'objI.sSalesAmount = Val(txtTotalFinal.Text)
                objI.sSalesAmount = Val(lblNetTotal.Text)
                objPM.dAdvancePaidAmount = dAdvancePaid
                objI.txtCustomerCode.Text = Trim(txtCustomerCode.Text)
                objI.txtMemberCode.Text = Trim(txtMemberCode.Text)
                objI.lblCustomerType.Text = Trim(lblCustomerType.Text)
                objI.sInvoiceDate = Trim(Trim(txtIDD.Text) + "/" + Trim(txtIMM.Text) + "/" + Trim(txtIYYYY.Text))
                objI.txtDepositAmount.Focus()
                objI.ShowDialog()
                If dPaidAmount > 0 And mbSalesTransaction Then
                    objSales.subOpenSalesConnection()
                    Try
                        objSales.subBeginSalesTransaction()
                        If fncGetInvoiceNo() = False Then
                            MessageBox.Show("Transaction failed!!!")
                            objSales.subRollBackSalesTransaction()
                            pnlPaymentType.Visible = False
                            Exit Try
                        End If
                        If Not objSales.fncUpdateNextInvoiceNo() Then
                            MessageBox.Show("Transaction failed!!!")
                            objSales.subRollBackSalesTransaction()
                            pnlPaymentType.Visible = False
                            Exit Try
                        End If
                        If Not fncPaymodeDetailSave(gsInsAgreementNo) Then
                            MessageBox.Show("Transaction not saved")
                            objSales.subRollBackSalesTransaction()
                            pnlPaymentType.Visible = False
                            Exit Try
                        End If
                        If Not fncSales(sInvoiceNo) Then
                            MessageBox.Show("Transaction not saved")
                            objSales.subRollBackSalesTransaction()
                            pnlPaymentType.Visible = False
                            Exit Try
                        End If
                        If Not fncUpdateInstallmentRefNo() Then
                            MessageBox.Show("Transaction not saved")
                            objSales.subRollBackSalesTransaction()
                            objPM.Visible = False
                            Exit Try
                        End If
                        objSales.subCommitSalesTransaction()
                        bSave = True
                    Catch ex As Exception
                        objSales.subRollBackSalesTransaction()
                    Finally
                        objSales.subCloseSalesConnection()
                    End Try
                    If Not bSave Then Exit Sub
                Else
                    objI.Visible = False
                    Exit Sub
                End If
            ElseIf optTradeIn.Checked = True Then
                'If sSalesType = "TRADEIN" And Trim(txtCustomerCode.Text) = "CASH" Then
                '    MessageBox.Show("TradeIn Sales Not For CASH Customer!!!")
                '    Exit Sub
                'End If
                sSalesType = "TRADEIN"
                objTI.sCalledFrom = "POS"
                objTI.sInvoiceNo = "" 'Trim(sInvoiceNo)
                objTI.sSalesAmount = Val(lblNetTotal.Text)
                objTI.txtCustomerCode.Text = Trim(txtCustomerCode.Text)
                objTI.txtMemberCode.Text = Trim(txtMemberCode.Text)
                objTI.txtCustomerName.Text = Trim(txtCustomerName.Text)
                objTI.lblCustomerType.Text = Trim(lblCustomerType.Text)
                objTI.txtAddress1.Text = Trim(txtAddress1.Text)
                objTI.txtAddress2.Text = Trim(txtAddress2.Text)
                objTI.txtAddress3.Text = Trim(txtAddress3.Text)
                objTI.sInvoiceDate = Trim(Trim(txtIDD.Text) + "/" + Trim(txtIMM.Text) + "/" + Trim(txtIYYYY.Text))
                'objTI.txtLossPerc.Text = Val(txtLossPer.Text)
                If UCase(sCompanyShortCode) = "MJ" Then
                    objTI.txtDescription.Text = "Trade In Gold - 22K"
                End If
                objPM.SalesType = sSalesType
                objTI.ShowDialog()
                If txtInventoryCode.Enabled And txtInventoryCode.Visible Then txtInventoryCode.Focus()
                mdCommission = objPM.Commission
                If (UCase(sCompanyShortCode) = "ES") Then
                    If mbSalesTransaction Then
                        If dPaidAmount > 0 Or (bTradeinFlag = True And dTradeinBalance < 0) Then
                            objSales.subOpenSalesConnection()
                            objSales.subOpenSalesConnectionHQ()
                            objSales.subOpenSalesConnectionFlexi()
                            Try
                                objSales.subBeginSalesTransaction()
                                objSales.subBeginSalesTransactionHQ()
                                objSales.subBeginSalesTransactionFlexi()
                                If fncGetInvoiceNo() = False Then
                                    MessageBox.Show("Transaction failed!!!")
                                    objSales.subRollBackSalesTransaction()
                                    pnlPaymentType.Visible = False
                                    Exit Try
                                End If
                                If Not objSales.fncUpdateNextInvoiceNo() Then
                                    MessageBox.Show("Transaction failed!!!")
                                    objSales.subRollBackSalesTransaction()
                                    pnlPaymentType.Visible = False
                                    Exit Try
                                End If
                                If Not fncPaymodeDetailSave(sInvoiceNo) Then
                                    MsgBox("Transaction not saved")
                                    objSales.subRollBackSalesTransaction()
                                    objPM.Visible = False
                                    Exit Try
                                Else
                                    bTradeinFlag = False
                                    dTradeinBalance = 0
                                End If
                                If Not fncSales(sInvoiceNo) Then
                                    MessageBox.Show("Transaction not saved")
                                    objSales.subRollBackSalesTransaction()
                                    objPM.Visible = False
                                    Exit Try
                                End If
                                If Not fncUpdateTradeInRefNo() Then
                                    MessageBox.Show("Transaction not saved")
                                    objSales.subRollBackSalesTransaction()
                                    objPM.Visible = False
                                    Exit Try
                                End If
                                'If Val(dOfferday) <> 1 Then
                                'If Trim(txtSubscriptionID.Text) = "" Then
                                If Not fncPosMemberTransactionSave(sInvoiceNo) Then
                                    MessageBox.Show("Transaction not saved")
                                    objSales.subRollBackSalesTransaction()
                                    objPM.Visible = False
                                    Exit Try
                                End If
                                'End If

                                'End If
                                If Not objSales.fncUpdateDeposit(sDepositNo, sInvoiceNo, Trim(txtIDD.Text) + "/" + Trim(txtIMM.Text) + "/" + Trim(txtIYYYY.Text)) Then
                                    MessageBox.Show("Transaction not saved!!!")
                                    objSales.subRollBackSalesTransaction()
                                    pnlPaymentType.Visible = False
                                    Exit Try
                                End If
                                If Not objSales.fncUpdateAdvDeposit(sAdvDepositNo, sInvoiceNo, Trim(txtIDD.Text) + "/" + Trim(txtIMM.Text) + "/" + Trim(txtIYYYY.Text)) Then
                                    MessageBox.Show("Transaction not saved!!!")
                                    objSales.subRollBackSalesTransaction()
                                    pnlPaymentType.Visible = False
                                    Exit Try
                                End If
                                If Trim(sCostChangeSalesman) <> "" Then
                                    If Not objSales.fncUpdateCostChangeLogSave(sInvoiceNo, Trim(txtIDD.Text) + "/" + Trim(txtIMM.Text) + "/" + Trim(txtIYYYY.Text), Trim(txtSalesManName.Text), Trim(sCostChangeSalesman)) Then
                                        MessageBox.Show("Transaction not saved!!!")
                                        objSales.subRollBackSalesTransaction()
                                        Exit Try
                                    End If
                                End If
                                OpenConnectionFlexi()
                                BeginTransactionFlexi()

                                sFlexiLocation = Trim(gthisLocation)
                                If Trim(sFlexiLocation) = "99" Then
                                    sFlexiLocation = "103"
                                ElseIf Trim(sLocationCode) = "27" Then
                                    sFlexiLocation = "1"
                                End If
                                If Trim(txtSubscriptionID.Text) <> "" Then
                                    bResult = objSales.fncUpdatePayModeFlexiClosure(Trim(txtSubscriptionID.Text), Trim(sFlexiLocation), Trim(sUserID))
                                    If bResult = False Then
                                        RollBackTransactionFlexi()
                                        Exit Sub
                                    End If
                                End If

                                If bResult = False Then
                                    RollBackTransactionFlexi()
                                Else
                                    CommitTransactionFlexi()
                                End If
                                msTradeInRefNo = ""
                                objSales.subCommitSalesTransaction()
                                bSave = True
                            Catch ex As Exception
            objSales.subRollBackSalesTransaction()
        Finally
            objSales.subCloseSalesConnection()
        End Try
                            If Not bSave Then Exit Sub
                        Else
                            objTI.Visible = False
                            Exit Sub
                        End If
                    End If
                    objPM.Visible = False
                Else
                    If mbSalesTransaction = True Then
                        If bTradeinFlag = True Then
                            objSales.subOpenSalesConnection()
                            objSales.subOpenSalesConnectionHQ()
                            If Trim(sCompanyShortCode) = "AJ" Then
                                objSales.subOpenSalesConnectionFlexi()
                            End If

                            Try
                                objSales.subBeginSalesTransaction()
                                objSales.subBeginSalesTransactionHQ()
                                If Trim(sCompanyShortCode) = "AJ" Then
                                    objSales.subBeginSalesTransactionFlexi()
                                End If

                                If fncGetInvoiceNo() = False Then
                                    MessageBox.Show("Transaction failed!!!")
                                    objSales.subRollBackSalesTransaction()
                                    pnlPaymentType.Visible = False
                                    Exit Try
                                End If
                                If Not objSales.fncUpdateNextInvoiceNo() Then
                                    MessageBox.Show("Transaction failed!!!")
                                    objSales.subRollBackSalesTransaction()
                                    pnlPaymentType.Visible = False
                                    Exit Try
                                End If
                                If Not fncPaymodeDetailSave(sInvoiceNo) Then
                                    MsgBox("Transaction not saved")
                                    objSales.subRollBackSalesTransaction()
                                    objPM.Visible = False
                                    Exit Try
                                Else
                                    bTradeinFlag = False
                                    dTradeinBalance = 0
                                End If
                                If Not fncSales(sInvoiceNo) Then
                                    MessageBox.Show("Transaction not saved")
                                    objSales.subRollBackSalesTransaction()
                                    objPM.Visible = False
                                    Exit Try
                                End If
                                If Not fncUpdateTradeInRefNo() Then
                                    MessageBox.Show("Transaction not saved")
                                    objSales.subRollBackSalesTransaction()
                                    objPM.Visible = False
                                    Exit Try
                                End If
                                ' If Val(dOfferday) <> 1 Then
                                '  If Trim(txtSubscriptionID.Text) = "" Then
                                If Not fncPosMemberTransactionSave(sInvoiceNo) Then
                                    MessageBox.Show("Transaction not saved")
                                    objSales.subRollBackSalesTransactionHQ()
                                    objPM.Visible = False
                                    Exit Try
                                End If
                                ' End If

                                'End If
                                If Not objSales.fncUpdateDeposit(sDepositNo, sInvoiceNo, Trim(txtIDD.Text) + "/" + Trim(txtIMM.Text) + "/" + Trim(txtIYYYY.Text)) Then
                                    MessageBox.Show("Transaction not saved!!!")
                                    objSales.subRollBackSalesTransaction()
                                    pnlPaymentType.Visible = False
                                    Exit Try
                                End If
                                If Not objSales.fncUpdateAdvDeposit(sAdvDepositNo, sInvoiceNo, Trim(txtIDD.Text) + "/" + Trim(txtIMM.Text) + "/" + Trim(txtIYYYY.Text)) Then
                                    MessageBox.Show("Transaction not saved!!!")
                                    objSales.subRollBackSalesTransaction()
                                    pnlPaymentType.Visible = False
                                    Exit Try
                                End If
                                If Trim(sCostChangeSalesman) <> "" Then
                                    If Not objSales.fncUpdateCostChangeLogSave(sInvoiceNo, Trim(txtIDD.Text) + "/" + Trim(txtIMM.Text) + "/" + Trim(txtIYYYY.Text), Trim(txtSalesManName.Text), Trim(sCostChangeSalesman)) Then
                                        MessageBox.Show("Transaction not saved!!!")
                                        objSales.subRollBackSalesTransaction()
                                        Exit Try
                                    End If
                                End If
                                If Trim(sCompanyShortCode) = "AJ" Then
                                    sFlexiLocation = Trim(gthisLocation)
                                    If Trim(sFlexiLocation) = "99" Then
                                        sFlexiLocation = "103"
                                    ElseIf Trim(sLocationCode) = "27" Then
                                        sFlexiLocation = "1"
                                    End If
                                    If Trim(txtSubscriptionID.Text) <> "" Then
                                        bResult = objSales.fncUpdatePayModeFlexiClosure(Trim(txtSubscriptionID.Text), Trim(sFlexiLocation), Trim(sUserID))
                                        If bResult = False Then
                                            objSales.subRollBackSalesTransactionFlexi()
                                            Exit Sub
                                        End If
                                    End If
                                End If


                                msTradeInRefNo = ""
                                objSales.subCommitSalesTransaction()
                                objSales.subCommitSalesTransactionHQ()
                                If Trim(sCompanyShortCode) = "AJ" Then
                                    objSales.subCommitSalesTransactionFlexi()
                                End If

                                bSave = True
                            Catch ex As Exception
            objSales.subRollBackSalesTransaction()
            objSales.subRollBackSalesTransactionHQ()
            If Trim(sCompanyShortCode) = "AJ" Then
                objSales.subRollBackSalesTransactionFlexi()
            End If

        Finally
            objSales.subCloseSalesConnection()
            objSales.subCloseSalesConnectionHQ()
            If Trim(sCompanyShortCode) = "AJ" Then
                objSales.subCloseSalesConnectionFlexi()
            End If

        End Try
                            If Not bSave Then Exit Sub
                            objPM.Visible = False
                        End If
                    End If
                End If
            End If
            If bSave = True Then
                Call subPrintBill(Trim(sInvoiceNo))
                If Trim(sSalesType) = "TRADEIN" Then
                    If Trim(sCompanyShortCode) = "AJ" Then
                        Call subGetBarInventory()
                    End If
                End If
                If UCase(sCompanyShortCode) <> "HN" Then
                    If optInstallment.Checked = True Then
                        MessageBox.Show("Please take note the Sales No - " & sInvoiceNo & " and " & _
                                    " Installment Agreement No - " & gsInsAgreementNo & ".")
                    Else
                        MsgBox("Please take note the Sales no " + sInvoiceNo)
                    End If
                End If
                objPM.SalesType = ""
                SubSayEnglish("THANK YOU PLEASE COME AGAIN ")
                subClear()
                txtSalesManPassword.Text = ""
                txtSalesManName.Text = ""
                If txtSalesManPassword.Enabled = True Then txtSalesManPassword.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            objPM = Nothing
            objI = Nothing
            objTI = Nothing
        End Try
    End Sub

    Private Sub subGetBarInventory()
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
                    sBarInventoryCode = Trim(DsTrade.Tables("TradeIn").Rows(icnt)("InventoryCode") & "")
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
            strselectformula = strselectformula & "{tblTradeInDetail.Class}=" & fncFormatSQLText(Trim(sBarClassCode)) & " and "
            strselectformula = strselectformula & "{tblTradeInDetail.Inventorycode}=" & fncFormatSQLText(Trim(sBarInventoryCode))


            CR.DataDefinition.FormulaFields("LocationCode").Text = fncFormatSQLText(sBarLocationCode)
            CR.DataDefinition.FormulaFields("InvoiceNo").Text = fncFormatSQLText(sBarInvoiceNo)
            CR.DataDefinition.FormulaFields("InvoiceDate").Text = fncFormatSQLText(sBarInvoiceDate)
            CR.DataDefinition.FormulaFields("ClassCode").Text = fncFormatSQLText(sBarClassCode)
            CR.DataDefinition.FormulaFields("Weight").Text = fncFormatSQLText(Format((Val(dBarWeight) - Val(dBarStoneWeight)), "###0.00") + "gm")
            CR.DataDefinition.FormulaFields("StoneWeight").Text = fncFormatSQLText(Format(Val(dBarWeight), "###0.00") + "gm") 'fncFormatSQLText(Format((Val(dBarWeight) + Val(dBarStoneWeight)), "###0.00") + "gm")
            CR.DataDefinition.FormulaFields("TotalCost").Text = Format(Val(dBarTotalCost), "###0.00")
            CR.DataDefinition.FormulaFields("Salesman").Text = fncFormatSQLText(sSalesManCode)
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
    Private Function fncGetInvoiceNo() As Boolean
        Dim dsComp As New DataSet
        Dim dsInv As New DataSet
        Try
            dsComp = objComp.GetCompanyList()
            If dsComp.Tables(0).Rows.Count > 0 Then
                sCompanyCode = Trim(dsComp.Tables(0).Rows(0)("CompanyID") & "")
                sLocationCode = gthisLocation
                sTerminalCode = ""
                sGSTType = Trim(dsComp.Tables(0).Rows(0)("GSTType") & "")
                dGSTPerc = Val(dsComp.Tables(0).Rows(0)("GstPerc") & "")
            Else
                MsgBox("Locked")
            End If
            dsInv = objSales.GetUpdateInvoice()
            If dsInv.Tables(0).Rows.Count > 0 Then
                sInvoiceNo = Trim(dsInv.Tables(0).Rows(0)("NextInvoice") & "")
                fncGetInvoiceNo = True
            Else
                fncGetInvoiceNo = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsComp = Nothing
            dsInv = Nothing
        End Try
    End Function

    Public Function fncPaymodeDetailSave(ByVal sInvoiceNo As String) As Boolean
        Try
            Dim bPayModeFlag As Boolean
            Dim sDate As String
            Dim iCnt As Long

            Dim sPayMode As String
            Dim nPayAmount As Decimal
            Dim sSlipNo As String
            Dim PayMode As New frmPayingmode

            bPayModeFlag = False
            sDate = Format(ServerDate, "dd/MM/yyyy")

            For iCnt = 0 To UBound(arrpay, 1)
                If arrpay(iCnt, 1) > 0 Then
                    iRet = objSales.PayModeDetailSave(sInvoiceNo, sDate, Trim(arrpay(iCnt, 3)), _
                                                 Val(arrpay(iCnt, 1)), Val(arrpay(iCnt, 2)), _
                                                 Trim(arrpay(iCnt, 4)), Trim(sSalesType))
                    bPayModeFlag = True

                    If Not iRet Then
                        fncPaymodeDetailSave = False
                        Exit Function
                    End If
                End If
                If Trim(arrpay(iCnt, 3)) = "REDEEMPOINTS" Then
                    If Trim(lblCustomerType.Text) = "RUBY" Then
                        dRedeemedPoints = Val(arrpay(iCnt, 1)) / 5 * 1000
                    ElseIf Trim(lblCustomerType.Text) = "EMERALD" Then
                        dRedeemedPoints = Val(arrpay(iCnt, 1)) / 7 * 1000
                    ElseIf Trim(lblCustomerType.Text) = "DIAMOND" Then
                        dRedeemedPoints = Val(arrpay(iCnt, 1)) / 9 * 1000
                    End If
                End If
            Next iCnt

            'For iCnt = 0 To PayMode.grdPaymode.RowCount - 1
            '    If Val(fnGetdgvText(PayMode.grdPaymode, iCnt, PayMode.cTxtAmount.Index)) <> 0 Then
            '        sPayMode = Convert.ToString(fnGetdgvText(PayMode.grdPaymode, iCnt, PayMode.cLblPaymode.Index)).Trim
            '        nPayAmount = Val(fnGetdgvText(frmPayingmode.grdPaymode, iCnt, frmPayingmode.cTxtAmount.Index))
            '        sSlipNo = Convert.ToString(fnGetdgvText(frmPayingmode.grdPaymode, iCnt, frmPayingmode.cTxtSlipNo.Index)).Trim
            '        If Val(nPayAmount) > 0 Then
            '            iRet = objSales.PayModeDetailSave(sInvoiceNo, sDate, sPayMode, nPayAmount, 0, sSlipNo)
            '        End If

            '        bPayModeFlag = True

            '        If Not iRet Then
            '            fncPaymodeDetailSave = False
            '            Exit Function
            '        End If
            '    End If

            'Next iCnt


            If (bTradeinFlag = True And dTradeinBalance <= 0) Then
                iCnt = 0
                iRet = objSales.PayModeDetailSave(sInvoiceNo, sDate, Trim(arrpay(iCnt, 3)), _
                                             Val(arrpay(iCnt, 1)), Val(arrpay(iCnt, 2)), _
                                             Trim(arrpay(iCnt, 4)), Trim(sSalesType))
                bPayModeFlag = True

                If Not iRet Then
                    fncPaymodeDetailSave = False
                    Exit Function
                End If
                'sPayMode = Convert.ToString(fnGetdgvText(frmPayingmode.grdPaymode, iCnt, frmPayingmode.cLblPaymode.Index)).Trim
                'nPayAmount = Val(fnGetdgvText(frmPayingmode.grdPaymode, iCnt, frmPayingmode.cTxtAmount.Index))
                'sSlipNo = Convert.ToString(fnGetdgvText(frmPayingmode.grdPaymode, iCnt, frmPayingmode.cTxtSlipNo.Index)).Trim
                'If Val(nPayAmount) > 0 And sPayMode = "TRADEIN" Then
                '    iRet = objSales.PayModeDetailSave(sInvoiceNo, sDate, sPayMode, nPayAmount, 0, sSlipNo)
                'End If

                'bPayModeFlag = True

                'If Not iRet Then
                '    fncPaymodeDetailSave = False
                '    Exit Function
                'End If
            End If


            ''' Block By Durai (Fix the Issue) 
            '''If bPayModeFlag = False Then
            '''    iRet = objSales.PayModeDetailSave(nNextInvoice, sDate, "CASH", _
            '''                                Val(lblNetTotal.Text), 0, _
            '''                                "MANUAL")
            '''    If Not iRet Then

            '''        fncPaymodeDetailSave = False
            '''        Exit Function
            '''    End If

            '''End If
            If Trim(sCompanyShortCode) = "HN" Then
                If bPayModeFlag = False Then
                    fncPaymodeDetailSave = False
                Else
                    fncPaymodeDetailSave = True
                End If
            Else
                fncPaymodeDetailSave = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Function fncSales(ByRef sSalesNo As String) As Boolean
        Dim DsRate As New DataSet
        Dim sPayType As String
        Dim bSalesDetail As Boolean
        Dim bRFIDUpdate As Boolean
        Dim iCnt As Long
        Dim dRunningNetDiscount, dRunningCommission As Double
        Dim nTiWeight, nTiTotalAmt, nTiGst As Double
        Dim nDiscount, nNetTotal, nPaidAmt, nBalanceAmt, nDetailDiscount
        Dim dTotalWeight, dTotalStoneWeight, dTotalGoldLoss, dTotalSMC, dTotalPCS, dTotalValue, dTotalCost As Double
        Dim dNetCommission#, dCommission#
        Dim sSalesDate As String
        Dim sInventoryCode As String
        Try

            bSalesDetail = False
            txtPMSurCharge.Text = dPayModeCommission

            dTotalWeight = fnGetdgvText(grdSales, grdSales.RowCount - 1, IWeight)
            dTotalGoldLoss = fnGetdgvText(grdSales, grdSales.RowCount - 1, IGoldLoss)
            dTotalStoneWeight = fnGetdgvText(grdSales, grdSales.RowCount - 1, IDSWeight)
            dTotalSMC = fnGetdgvText(grdSales, grdSales.RowCount - 1, ISMC)
            dTotalPCS = fnGetdgvText(grdSales, grdSales.RowCount - 1, IPCS)
            dTotalValue = fnGetdgvText(grdSales, grdSales.RowCount - 1, ITotalValue)
            dTotalCost = fnGetdgvText(grdSales, grdSales.RowCount - 1, ITotalCost)


            'If Trim(sGSTType) = "iCnt" Then
            '   dGst = Format(dTotalValue * dGSTPerc / (dGSTPerc + 100), "0.0000")
            'Else
            '   dGst = Format(dTotalValue * dGSTPerc / 100, "0.0000")
            'End If
            dGst = Format(Val(lblTax.Text), "0.00")
            nDetailDiscount = "0.00"        'Format(Val(GetText(grdSales, grdSales.MaxRows, 7)), "0.00")
            dNetDiscount = Format(Val(lblNetDiscount.Text), "0.00")
            nDiscount = Format(Val(nDetailDiscount) + Val(dNetDiscount), "0.00")
            nNetTotal = Format(Val(lblNetTotal.Text), "0.00")
            dNetCommission = Val(txtPMSurCharge.Text)
            nNetTotal = nNetTotal + dNetCommission

            If optExclusive.Checked = True Then
                sGSTType = "E"
            ElseIf optInclusive.Checked = True Then
                sGSTType = "I"
            ElseIf optZeroRated.Checked = True Then
                sGSTType = "Z"
            End If

            If opteTrsTicketIssued.Checked = True Then
                seTRSType = "Y"
            ElseIf opteTRSNo.Checked = True Then
                seTRSType = "N"
            ElseIf opteTRSEligible.Checked = True Then
                seTRSType = "E"
            End If

            sSDate = Trim(txtIDD.Text) + "/" + Trim(txtIMM.Text) + "/" + Trim(txtIYYYY.Text)

            '' GST for Only Paid Amount - TradeIn Sales Only 
            '''If ((UCase(sCompanyShortCode) = "AS") Or (UCase(sCompanyShortCode) = "HN") Or (UCase(sCompanyShortCode) = "ML")) Then

            '''    ''If UCase(sCompanyShortCode) = "ML" Then
            '''    If UCase(sGSTType) = "I" And UCase(sSalesType) = "TRADEIN" Then
            '''        If dGSTPerc > 0 Then
            '''            dGst = ((Val(dPaidAmount) - dNetCommission) * dGSTPerc) / (100 + dGSTPerc)
            '''        End If
            '''    End If
            '''End If

            nTiWeight = "0"
            nTiTotalAmt = "0"
            nTiGst = "0"
            For iCnt = 0 To grdSales.RowCount - 1
                sInventoryCode = Trim(fnGetdgvText(grdSales, iCnt, 0))
                If sInventoryCode <> "TOTAL:" Then
                    If fnGetdgvText(grdSales, iCnt, ISalesType).Trim.ToUpper = "TI" Then
                        nTiWeight = nTiWeight + Val(fnGetdgvText(grdSales, iCnt, IWeight))
                        nTiTotalAmt = nTiTotalAmt + Val(fnGetdgvText(grdSales, iCnt, ITotalValue)) - Val(fnGetdgvText(grdSales, iCnt, IGST))
                        nTiGst = nTiGst + Val(fnGetdgvText(grdSales, iCnt, IGST))
                    Else
                        nDetailDiscount = nDetailDiscount + Val(fnGetdgvText(grdSales, iCnt, IDiscount))
                    End If
                End If
            Next

            If Val(dTradeInSales) > 0 And bTradeInSR = True Then
                dTradeInGST = Val(-Format$((Val(dTradeInSales) * dGSTPerc) / (107), "#0.00"))
                dTradeInSales = Format(Val(dTradeInSales) + 0, "0.00")
                dTotalValue = dTotalValue - dTradeInSales
                nNetTotal = nNetTotal - dTradeInSales
                nTiTotalAmt = Val(dTradeInSales)
                dGst = dGst + dTradeInGST
            End If

            fncSales = objSales.SalesHeaderSave(Trim(sInvoiceNo), Trim(sSDate), Trim(sCompanyCode), Trim(sLocationCode), Trim(gTerminalCode), _
                                Trim(txtCustomerCode.Text), Trim(txtSubscriptionID.Text), Trim(txtCustomerName.Text), Trim(txtAddress1.Text), _
                                Trim(txtAddress2.Text), Trim(txtAddress3.Text), Trim(sSalesManCode), Trim$(sSalesType), _
                                Val(dTotalWeight) - Val(nTiWeight), Val(dTotalStoneWeight), Val(dTotalGoldLoss), _
                                Val(dTotalSMC), Val(dTotalPCS), Val(dGst), Trim(sGSTType), Val(nDetailDiscount), _
                                Val(dNetDiscount), Val(nDiscount), Val(nNetTotal), _
                                Val(dPaidAmount), Val(ReturnAmount), _
                                Val(dTotalValue), Val(dTotalCost), _
                                -Val(nTiTotalAmt), Val(nTiWeight), _
                                Trim$(gsInsAgreementNo), Val(dNetCommission), Trim(txtRemarks.Text), Val(dTradeInGST), Trim(seTRSType))

            If UCase(txtCustomerCode.Text.Trim) = "CASH" And sMemberModule = "1" Then
                fncSales = objSales.MemberTransactionSave(Trim(txtMemberCode.Text), Trim(sInvoiceNo), Trim(sSDate), Val(nDiscount), Val(dNetDiscount), Val(nNetTotal), Trim(sLocationCode))
            End If
            If UCase(sCompanyShortCode) <> "HN" Then
                Call CalcMarketRate()
                If bPassword Then
                    iRet = objSales.AllowMarketPriceSave(Trim(sInvoiceNo), Trim(sSDate), dDailyClassRate, dNetDiscount, dGst, Val(txtTotalValue.Text), dNetTotalMarketPrice)
                End If

                Call objSales.FindTotalMarketPriceSave(Trim(sInvoiceNo), dFindTotalMarketPrice)
            End If

            If fncSales Then
                dRunningNetDiscount = 0
                dRunningCommission = 0
                For iCnt = 0 To grdSales.RowCount - 1
                    sInventoryCode = Trim(fnGetdgvText(grdSales, iCnt, 0))
                    If sInventoryCode <> "TOTAL:" Then
                        dNetDiscount = Format("0.00")
                        dCommission = 0

                        If Val(lblSalesAmount.Text) <> 0 Then
                            If fnGetdgvText(grdSales, iCnt, ISalesType).Trim.ToUpper <> "TI" Then
                                If iCnt = grdSales.RowCount - 1 Then
                                    dNetDiscount = Val(lblNetDiscount.Text) - dRunningNetDiscount
                                    If Val(dNetCommission) > 0 Then
                                        dCommission = Val(dNetCommission) - dRunningCommission
                                    End If
                                Else
                                    dNetDiscount = Format((Val(fnGetdgvText(grdSales, iCnt, ITotalValue)) * Val(lblNetDiscount.Text)) / (Val(lblSalesAmount.Text) + Val(-nTiTotalAmt) + Val(-nTiGst)), "0.00")
                                    If Val(dNetCommission) > 0 Then
                                        dCommission = Format((Val(fnGetdgvText(grdSales, iCnt, ITotalValue)) * Val(dNetCommission)) / (Val(lblSalesAmount.Text) + Val(-nTiTotalAmt) + Val(-nTiGst)), "0.00")
                                    End If
                                End If
                            End If
                            dRunningNetDiscount = dRunningNetDiscount + dNetDiscount
                            dRunningCommission = dRunningCommission + dCommission
                        End If

                        dGst = Format("0.00")
                        dNetAmount = Format("0.00")
                        dActualSales = Format("0.00")

                        If sInvGSTType = "" Or sInvGSTType = "Standard Rated Supply" Then
                            If optExclusive.Checked = True Then
                                dGst = Format((Val(fnGetdgvText(grdSales, iCnt, ITotalValue)) - dNetDiscount) * dGSTPerc / (100), "0.0000")
                                dNetAmount = Format((Val(fnGetdgvText(grdSales, iCnt, ITotalValue)) - dNetDiscount + dGst + dCommission), "0.00")
                                dActualSales = Format((Val(fnGetdgvText(grdSales, iCnt, ITotalValue)) - dNetDiscount + dCommission), "0.00")
                            ElseIf optInclusive.Checked = True Then
                                dGst = Format((Val(fnGetdgvText(grdSales, iCnt, ITotalValue)) - dNetDiscount) * dGSTPerc / (dGSTPerc + 100), "0.0000")
                                dNetAmount = Format((Val(fnGetdgvText(grdSales, iCnt, ITotalValue)) - dNetDiscount + dCommission), "0.00")
                                dActualSales = Format((Val(fnGetdgvText(grdSales, iCnt, ITotalValue)) - dNetDiscount - dGst + dCommission), "0.00")
                            ElseIf optZeroRated.Checked = True Then
                                dGst = Format("0.0000")
                                dNetAmount = Format((Val(fnGetdgvText(grdSales, iCnt, ITotalValue)) - dNetDiscount) + dCommission, "0.00")
                                dActualSales = Format((Val(fnGetdgvText(grdSales, iCnt, ITotalValue)) - dNetDiscount + dCommission), "0.00")
                            End If
                        ElseIf sInvGSTType = "Exempt Supply" Or sInvGSTType = "No GST" Then
                            dGst = Format("0.0000")
                            dNetAmount = Format((Val(fnGetdgvText(grdSales, iCnt, ITotalValue)) - dNetDiscount) + dCommission, "0.00")
                            dActualSales = Format((Val(fnGetdgvText(grdSales, iCnt, ITotalValue)) - dNetDiscount + dCommission), "0.00")
                        End If

                        '''If optExclusive.Checked = True Then
                        '''    dGst = Format((Val(GetText(grdSales, iCnt, ITotalValue)) - dNetDiscount) * dGSTPerc / (100), "0.0000")
                        '''    dNetAmount = Format((Val(GetText(grdSales, iCnt, ITotalValue)) - dNetDiscount + dGst + dCommission), "0.00")
                        '''    dActualSales = Format((Val(GetText(grdSales, iCnt, ITotalValue)) - dNetDiscount + dCommission), "0.00")
                        '''ElseIf optInclusive.Checked = True Then
                        '''    dGst = Format((Val(GetText(grdSales, iCnt, ITotalValue)) - dNetDiscount) * dGSTPerc / (dGSTPerc + 100), "0.0000")
                        '''    dNetAmount = Format((Val(GetText(grdSales, iCnt, ITotalValue)) - dNetDiscount + dCommission), "0.00")
                        '''    dActualSales = Format((Val(GetText(grdSales, iCnt, ITotalValue)) - dNetDiscount - dGst + dCommission), "0.00")

                        '''ElseIf optZeroRated.Checked = True Then
                        '''    dGst = Format("0.0000")
                        '''    dNetAmount = Format((Val(GetText(grdSales, iCnt, ITotalValue)) - dNetDiscount) + dCommission, "0.00")
                        '''    dActualSales = Format((Val(GetText(grdSales, iCnt, ITotalValue)) - dNetDiscount + dCommission), "0.00")
                        '''End If

                        ''  nDetailDiscount = 0

                        'Modified by Durai on 06 Sep'2006

                        sClassCode = Trim(fnGetdgvText(grdSales, iCnt, IClass))
                        sSalesDate = Trim(txtIDD.Text) + "/" + Trim(txtIMM.Text) + "/" + Trim(txtIYYYY.Text)
                        DsRate = objSales.GetMarketRate(sClassCode, sSalesDate)
                        If DsRate.Tables(0).Rows.Count > 0 Then
                            dRate = Val(DsRate.Tables(0).Rows(0)("Rate") & "")
                        End If
                        dDailyClassRate = dRate

                        'Parameters added are sLocationCode, nDetailDiscount, dNetDiscount, dGst, dNetAmount and dActualSales
                        bSalesDetail = objSales.SalesDetailSave(Trim(sInvoiceNo), _
                                            Trim(fnGetdgvText(grdSales, iCnt, ICode)), Trim(fnGetdgvText(grdSales, iCnt, IDescription)), sLocationCode, _
                                            Val(fnGetdgvText(grdSales, iCnt, IWeight)), Val(fnGetdgvText(grdSales, iCnt, IDSWeight)), _
                                            Val(fnGetdgvText(grdSales, iCnt, IGoldLoss)), Val(fnGetdgvText(grdSales, iCnt, ICostPerGram)), _
                                            Val(fnGetdgvText(grdSales, iCnt, ISMC)), Val(fnGetdgvText(grdSales, iCnt, IPCS)), _
                                           Val(fnGetdgvText(grdSales, iCnt, IDiscount)), Val(dNetDiscount), Val(dGst), _
                                            Val(dNetAmount), Val(dActualSales), _
                                            Val(fnGetdgvText(grdSales, iCnt, ITotalValue)), Val(fnGetdgvText(grdSales, iCnt, ITotalCost)), _
                                            Val(dCommission), Trim(txtRemarks.Text), Val(dDailyClassRate), iCnt, Trim(fnGetdgvText(grdSales, iCnt, ISalesType)), Val(dTradeInGST))

                        If Not bSalesDetail Then
                            'MsgBox(sError1)
                            fncSales = False
                            Exit Function
                        End If
                    End If

                    If sRFIDModule = "1" Then
                        bRFIDUpdate = objSales.RFIDItemStatusUpdate(Trim(fnGetdgvText(grdSales, iCnt, ICode)))

                        If Not bRFIDUpdate Then
                            'MsgBox(sError1)
                            fncSales = False
                            Exit Function
                        End If
                    End If

                Next iCnt
                '' AS TradeIn GST updated in SalesDetail on 27 Aug 2007
                '''If ((UCase(sCompanyShortCode) = "AS") Or (UCase(sCompanyShortCode) = "HN") Or (UCase(sCompanyShortCode) = "ML")) Then
                '''    ''If (UCase(sCompanyShortCode) = "ML") Then
                '''    If UCase(sGSTType) = "I" And UCase(sSalesType) = "TRADEIN" Then
                '''        bSalesDetail = objSales.SalesDetailSaveTradeInGST(sInvoiceNo)
                '''    End If
                '''End If
            End If
            bPassword = False

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Sub SubSayEnglish(ByVal sText As String)
        'If bVoiceEnabled Then
        '    Voice.Speak sText, SVSFlagsAsync
        'End If
    End Sub
    Public Function fncPosMemberTransactionSave(ByVal sInvoiceNo As String) As Boolean
        Try
            Dim bPayModeFlag As Boolean
            Dim sDate As String
            Dim iCnt As Long

            Dim sPayMode As String
            Dim nPayAmount As Decimal
            Dim sSlipNo As String
            Dim PayMode As New frmPayingmode

            bPayModeFlag = False
            sDate = Format(ServerDate, "dd/MM/yyyy")

            If Trim(txtSubscriptionID.Text) = "" Then
                iRet = objSales.PosMemberTransationSave(Trim(gthisLocation), sInvoiceNo, sDate, Trim(txtCustomerCode.Text), _
                             Val(lblNetTotal.Text), Val(lblNetTotal.Text), Val(dRedeemedPoints))
            Else
                iRet = objSales.PosMemberTransationSave(Trim(gthisLocation), sInvoiceNo, sDate, Trim(txtCustomerCode.Text), _
                        Val(lblNetTotal.Text), Val(0), Val(dRedeemedPoints))
            End If

            bPayModeFlag = True

            If Not iRet Then
                fncPosMemberTransactionSave = False
                Exit Function
            End If

            fncPosMemberTransactionSave = True

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function
    Private Sub subPrintBill(ByVal sInvoiceNo As String)
        Dim ReportTitle As String
        Dim sSelection As String
        Dim sSalesType$
        Dim sReply As String
        Dim dsSales As New DataSet
        Dim frmrep As New frmReports
        Dim CR As New ReportDocument
        Dim j As Integer
        Dim sPrintMC As String
        Dim sPrintImage As String
        Dim Dm As New DataManager
        Dim sCmd As String
        Dim sExec As String
        Dim sInsAgreementNo As String
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
                        subGetClassRate()
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
                        subGetClassRate()
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
                        subGetClassRate()
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
                If sInvGSTType = "" Then
                    strpath = Application.StartupPath & "\Reports\rptSalesInvoice_MJ.rpt"
                ElseIf sInvGSTType = "Standard Rated Supply" Then
                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceStandard_MJ.rpt"
                ElseIf sInvGSTType = "Exempt Supply" Or sInvGSTType = "No GST" Then
                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceNoGST_MJ.rpt"
                End If
                CR.Load(strpath)
                ReportLogOnToServer(CR)
                CR.Refresh()
                subGetClassRate()
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
            ElseIf UCase(sCompanyShortCode) = "FH" Then
                ''strpath = Application.StartupPath & "\Reports\rptSalesInvoice_MJ.rpt"
                If sInvGSTType = "" Then
                    strpath = Application.StartupPath & "\Reports\rptSalesInvoice_FH.rpt"
                ElseIf sInvGSTType = "Standard Rated Supply" Then
                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceStandard_FH.rpt"
                ElseIf sInvGSTType = "Exempt Supply" Or sInvGSTType = "No GST" Then
                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceNoGST_FH.rpt"
                End If
                CR.Load(strpath)
                ReportLogOnToServer(CR)
                CR.Refresh()
                subGetClassRate()
                frmrep.ReportViewer.ReportSource = CR
                sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                frmrep.ReportViewer.SelectionFormula = sSelection
                CR.DataDefinition.RecordSelectionFormula = sSelection
                If UCase(sCompanyShortCode) = "FH" And bReprint Then
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
                        subGetClassRate()
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
                        subGetClassRate()
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
                        subGetClassRate()
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
                            subGetClassRate()
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
                            subGetClassRate()
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
                            subGetClassRate()
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
                            CR.DataDefinition.FormulaFields("PrintMC").Text = fncFormatSQLText(sPrintMC)
                            CR.DataDefinition.FormulaFields("PrintImage").Text = fncFormatSQLText(sPrintImage)
                            If bReprint Then
                                CR.PrintToPrinter(1, True, 0, 0)
                            Else
                                CR.PrintToPrinter(2, True, 0, 0)
                            End If
                            frmrep = Nothing
                            frmrep = New frmReports
                        Case "ORDER"

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
                            CR.DataDefinition.FormulaFields("PrintMC").Text = fncFormatSQLText(sPrintMC)
                            CR.DataDefinition.FormulaFields("PrintImage").Text = fncFormatSQLText(sPrintImage)
                            If bReprint Then
                                CR.PrintToPrinter(1, True, 0, 0)
                            Else
                                CR.PrintToPrinter(2, True, 0, 0)
                            End If
                            frmrep = Nothing
                            frmrep = New frmReports
                        Case "TRADEIN"
                            If sCompanyShortCode = "ES" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_ESP.rpt"
                            ElseIf sCompanyShortCode = "AS" Then
                                '''strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_AS.rpt"
                                If sInvGSTType = "" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_AS.rpt"
                                ElseIf sInvGSTType = "Standard Rated Supply" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoiceStandard_AS.rpt"
                                ElseIf sInvGSTType = "Exempt Supply" Or sInvGSTType = "No GST" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoiceNoGST_AS.rpt"
                                End If
                            ElseIf sCompanyShortCode = "ML" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_ML.rpt"
                            ElseIf UCase(sCompanyShortCode) = "GMT" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_GMT.rpt"
                            ElseIf UCase(sCompanyShortCode) = "HN" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_HN.rpt"
                            ElseIf UCase(sCompanyShortCode) = "AJ" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_AJ.rpt"
                            ElseIf UCase(sCompanyShortCode) = "MD" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_MD.rpt"
                            ElseIf UCase(sCompanyShortCode) = "FH" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_FH.rpt"
                            End If
                            CR.Load(strpath)
                            ReportLogOnToServer(CR)
                            CR.Refresh()
                            subGetClassRate()
                            frmrep.ReportViewer.ReportSource = CR
                            sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                            frmrep.ReportViewer.SelectionFormula = sSelection
                            CR.DataDefinition.RecordSelectionFormula = sSelection
                            If UCase(sCompanyShortCode) = "HN" And bReprint Then
                                CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                            End If

                            If chkPrintMC.Checked Then
                                sPrintMC = "Y"
                            Else
                                sPrintMC = "N"
                            End If
                            If chkPrintImage.Checked Then
                                sPrintImage = "Y"
                            Else
                                sPrintImage = "N"
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
                            CR.DataDefinition.FormulaFields("PrintMC").Text = fncFormatSQLText(sPrintMC)
                            CR.DataDefinition.FormulaFields("PrintImage").Text = fncFormatSQLText(sPrintImage)
                            If bReprint Then
                                CR.PrintToPrinter(1, True, 0, 0)
                            Else
                                If sCompanyShortCode = "AJ" Then
                                    CR.PrintToPrinter(1, True, 0, 0)
                                Else
                                    CR.PrintToPrinter(2, True, 0, 0)
                                End If
                            End If
                            frmrep = Nothing
                            frmrep = New frmReports
                            If sCompanyShortCode = "ES" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInDetailInvoice_ESP.rpt"
                            End If

                            If ((UCase(sCompanyShortCode) <> "HN") And (UCase(sCompanyShortCode) <> "AS") And (UCase(sCompanyShortCode) <> "ML") And (UCase(sCompanyShortCode) <> "AJ")) Then
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
                                If sInvGSTType = "" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AS.rpt"
                                ElseIf sInvGSTType = "Standard Rated Supply" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceStandard_AS.rpt"
                                ElseIf sInvGSTType = "Exempt Supply" Or sInvGSTType = "No GST" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceNoGST_AS.rpt"
                                End If
                            ElseIf sCompanyShortCode = "AJ" Then
                                '''strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AS.rpt"
                                If sInvGSTType = "" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AJ.rpt"
                                ElseIf sInvGSTType = "Standard Rated Supply" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceStandard_AJ.rpt"
                                ElseIf sInvGSTType = "Exempt Supply" Or sInvGSTType = "No GST" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceNoGST_AJ.rpt"
                                End If
                            ElseIf sCompanyShortCode = "MD" Then
                                '''strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AS.rpt"
                                If sInvGSTType = "" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoice_MD.rpt"
                                ElseIf sInvGSTType = "Standard Rated Supply" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceStandard_MD.rpt"
                                ElseIf sInvGSTType = "Exempt Supply" Or sInvGSTType = "No GST" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceNoGST_MD.rpt"
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
                            subGetClassRate()
                            frmrep.ReportViewer.ReportSource = CR
                            sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                            frmrep.ReportViewer.SelectionFormula = sSelection
                            CR.DataDefinition.RecordSelectionFormula = sSelection
                            If UCase(sCompanyShortCode) = "HN" And bReprint Then
                                CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                            End If
                            If chkPrintMC.Checked Then
                                sPrintMC = "Y"
                            Else
                                sPrintMC = "N"
                            End If
                            If chkPrintImage.Checked Then
                                sPrintImage = "Y"
                            Else
                                sPrintImage = "N"
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
                            CR.DataDefinition.FormulaFields("PrintMC").Text = fncFormatSQLText(sPrintMC)
                            CR.DataDefinition.FormulaFields("PrintImage").Text = fncFormatSQLText(sPrintImage)
                            If bReprint Then
                                CR.PrintToPrinter(1, True, 0, 0)
                            Else
                                If sCompanyShortCode = "AJ" Then
                                    CR.PrintToPrinter(1, True, 0, 0)
                                Else
                                    CR.PrintToPrinter(2, True, 0, 0)
                                End If
                            End If
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
                            subGetClassRate()
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
                                If sInvGSTType = "" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_AS.rpt"
                                ElseIf sInvGSTType = "Standard Rated Supply" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoiceStandard_AS.rpt"
                                ElseIf sInvGSTType = "Exempt Supply" Or sInvGSTType = "No GST" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoiceNoGST_AS.rpt"
                                End If
                            ElseIf sCompanyShortCode = "AJ" Then
                                '''  strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_AS.rpt"
                                If sInvGSTType = "" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_AJ.rpt"
                                ElseIf sInvGSTType = "Standard Rated Supply" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoiceStandard_AJ.rpt"
                                ElseIf sInvGSTType = "Exempt Supply" Or sInvGSTType = "No GST" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoiceNoGST_AJ.rpt"
                                End If
                            ElseIf sCompanyShortCode = "MD" Then
                                '''  strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_AS.rpt"
                                If sInvGSTType = "" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_MD.rpt"
                                ElseIf sInvGSTType = "Standard Rated Supply" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoiceStandard_MD.rpt"
                                ElseIf sInvGSTType = "Exempt Supply" Or sInvGSTType = "No GST" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoiceNoGST_MD.rpt"
                                End If
                            ElseIf sCompanyShortCode = "MD" Then
                                '''  strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_AS.rpt"
                                If sInvGSTType = "" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_FH.rpt"
                                ElseIf sInvGSTType = "Standard Rated Supply" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoiceStandard_FH.rpt"
                                ElseIf sInvGSTType = "Exempt Supply" Or sInvGSTType = "No GST" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoiceNoGST_FH.rpt"
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
                            subGetClassRate()
                            sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                            frmrep.ReportViewer.SelectionFormula = sSelection
                            If UCase(sCompanyShortCode) = "HN" And bReprint Then
                                CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                            End If
                            If chkPrintMC.Checked Then
                                sPrintMC = "Y"
                            Else
                                sPrintMC = "N"
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
                            CR.DataDefinition.FormulaFields("PrintMC").Text = fncFormatSQLText(sPrintMC)

                            frmrep.Show()
                            frmrep.ReportViewer.Visible = True
                            frmrep.ReportViewer.DisplayToolbar = True
                            frmrep.ReportViewer.CloseView(False)
                            frmrep.WindowState = FormWindowState.Maximized
                            If bReprint Then
                                CR.PrintToPrinter(1, True, 0, 0)
                            Else
                                If sCompanyShortCode = "AJ" Then
                                    CR.PrintToPrinter(1, True, 0, 0)
                                Else
                                    CR.PrintToPrinter(2, True, 0, 0)
                                End If
                            End If
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
                                subGetClassRate()
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
                                If sInvGSTType = "" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AS.rpt"
                                ElseIf sInvGSTType = "Standard Rated Supply" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceStandard_AS.rpt"
                                ElseIf sInvGSTType = "Exempt Supply" Or sInvGSTType = "No GST" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceNoGST_AS.rpt"
                                End If
                            ElseIf sCompanyShortCode = "AJ" Then
                                '''strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AS.rpt"
                                If sInvGSTType = "" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AJ.rpt"
                                ElseIf sInvGSTType = "Standard Rated Supply" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceStandard_AJ.rpt"
                                ElseIf sInvGSTType = "Exempt Supply" Or sInvGSTType = "No GST" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceNoGST_AJ.rpt"
                                End If
                            ElseIf sCompanyShortCode = "MD" Then
                                '''strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AS.rpt"
                                If sInvGSTType = "" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoice_MD.rpt"
                                ElseIf sInvGSTType = "Standard Rated Supply" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceStandard_MD.rpt"
                                ElseIf sInvGSTType = "Exempt Supply" Or sInvGSTType = "No GST" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceNoGST_MD.rpt"
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
                            subGetClassRate()
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
                            If bReprint Then
                                CR.PrintToPrinter(1, True, 0, 0)
                            Else
                                If sCompanyShortCode = "AJ" Then
                                    CR.PrintToPrinter(1, True, 0, 0)
                                Else
                                    CR.PrintToPrinter(2, True, 0, 0)
                                End If
                            End If
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

    Private Sub btnCustomerCodeSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomerCodeSearch.Click
        Try
            Dim frmHelpCustomer As New frmHelpSearch(0, 0, "tblCustomer", "R", 4, "CustomerName", "", "CustomerCode", "CustomerCode", 80, "CustomerName", "CustomerName", 150, "NricNo", "NricNo", 90, "Address1", "Address1", 80)
            frmHelpCustomer.ShowDialog()
            txtCustomerCode.Text = frmHelpCustomer.Var1.Trim
            txtCustomerName.Text = frmHelpCustomer.Var2.Trim
            subGetCustomerDetail()
            sCustomerCode = Trim(txtCustomerCode.Text)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subGetInventoryBarCoin(ByVal sInventoryCode As String)
        Dim dsCls As New DataSet
        Try
            dsCls = objInv.GetInventoryBarCoin(sInventoryCode)
            If dsCls.Tables(0).Rows.Count > 0 Then
                sInvGSTType = Trim$(dsCls.Tables(0).Rows(0)("GSTType") & "")
            Else
                sInvGSTType = ""
            End If
            dsCls = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtInventoryCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInventoryCode.LostFocus
        Try
            If Trim(txtInventoryCode.Text) <> "" Then
                If txtInventoryCode.Text.Trim.ToUpper = "T" Then
                    subShowTradein()
                    Exit Sub
                ElseIf txtInventoryCode.Text.Trim.ToUpper = "C" Then
                    subShowCASH()
                    Exit Sub
                ElseIf txtInventoryCode.Text.Trim.ToUpper = "I" Then
                    subShowInstallment()
                    Exit Sub
                End If

                sInvGSTType = ""
                subGetInventoryBarCoin(txtInventoryCode.Text)
                '''If UCase(txtCustomerCode.Text.Trim) = "CASH" And sInvGSTType <> "" Then
                '''    MessageBox.Show("IPM Not For CASH Customer!!!")
                '''    txtInventoryCode.Text = ""
                '''    If txtCustomerCode.Enabled Then txtCustomerCode.Focus()
                '''    Exit Sub
                '''End If
                ''' 
                'If (UCase(Objuser.CompanyName)) = "ABIRAAME POPULAR JEWELLERS (S) PTE LTD" Then
                '    If UCase(txtCustomerCode.Text.Trim) = "CASH" And Trim(txtSubscriptionID.Text) = "" And Trim(gthisLocation) <> "29" Then
                '        MessageBox.Show("Sales Not Allowed For CASH Customer!!!")
                '        txtInventoryCode.Text = ""
                '        If txtCustomerCode.Enabled Then txtCustomerCode.Focus()
                '        Exit Sub
                '    End If
                'End If
          
                ''' Durai changes 
                If Not fncCheckExisting(txtInventoryCode.Text) Then
                    If fncGetInventory(txtInventoryCode.Text) Then
                        If Trim(sClassCode) <> "" Then
                            If Val(txtCostPerGram.Text) <> 0 Then
                                If msClsSalesType <> "P" Then
                                    If Val(sSalesPcs) = 0 Then
                                        txtTotalValue.Text = Format((Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram.Text) + Val(txtSMC.Text), "0.00")
                                    Else
                                        txtTotalValue.Text = Format(Val(txtSMC.Text), "0.00")
                                    End If
                                    ''HAJ AS PCS Sales of MUKUTTHI 
                                ElseIf msClsSalesType = "P" And UCase(sCompanyShortCode) = "AS" Then
                                    txtTotalValue.Text = Format((Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram.Text) + Val(txtSMC.Text), "0.00")
                                Else
                                    txtTotalValue.Text = Format(Val(txtSMC.Text), "0.00")
                                End If

                                '''HAJ JEWELPALACE ASSORTED ITEM CHANGES ON 23 JAN 2008
                                If UCase(msClsSalesType) = "B" Then
                                    txtWeight.Enabled = True
                                    txtWeight.Focus()
                                Else
                                    txtWeight.Enabled = False
                                    'txtWeight.Focus()
                                End If

                                If chkTradeIn.Checked = True Then
                                    txtGST.Text = 0.0
                                    txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                                    sInvGSTType = "T"
                                End If

                                If sInvGSTType = "" Or sInvGSTType = "Standard Rated Supply" Then
                                    If optExclusive.Checked = True Then
                                        txtGST.Text = Format$(Val(txtTotalValue.Text) * (dGSTPerc / 100), "#0.00")
                                        txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                                    ElseIf optInclusive.Checked = True Then
                                        txtGST.Text = Format$((Val(txtTotalValue.Text) * dGSTPerc) / (100), "#0.00")
                                        'txtGST.Text = Format$((Val(txtTotalValue.Text) * dGSTPerc) / (100 + dGSTPerc), "#0.00")
                                        txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                                    ElseIf optZeroRated.Checked = True Then
                                        txtGST.Text = 0.0
                                        txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                                    End If
                                ElseIf sInvGSTType = "Exempt Supply" Or sInvGSTType = "No GST" Then
                                    txtGST.Text = 0.0
                                    txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                                End If
                                
                                dTotalOrgCost = Format(Val(txtTotalValue.Text), "0.00")
                                '''If optExclusive.Checked = True Then
                                '''    txtGST.Text = Format$(Val(txtTotalValue.Text) * (dGSTPerc / 100), "#0.00")
                                '''    txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                                '''ElseIf optInclusive.Checked = True Then
                                '''    txtGST.Text = Format$((Val(txtTotalValue.Text) * dGSTPerc) / (100), "#0.00")
                                '''    'txtGST.Text = Format$((Val(txtTotalValue.Text) * dGSTPerc) / (100 + dGSTPerc), "#0.00")
                                '''    txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                                '''ElseIf optZeroRated.Checked = True Then
                                '''    txtGST.Text = 0.0
                                '''    txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                                '''End If
                                dRate = Val(txtCostPerGram.Text)
                                txtGoldLoss_LostFocus(sender, e)
                                txtPCS_Leave(sender, e)
                                txtSMC_LostFocus(sender, e)
                                If sDetailDiscount = "1" Then
                                    If txtTotalValue.Enabled Then txtTotalValue.Focus()
                                Else
                                    txtInventoryCode.Focus()
                                End If


                                Exit Sub
                            End If
                            If Trim(sClassCode) <> "" Then
                                'txtGoldLoss.SetFocus
                            Else
                                txtInventoryCode.Focus()
                                Exit Sub
                            End If
                        End If
                    End If
                Else
                    If bEdited = False Then
                        MessageBox.Show("Item already scanned ...")
                        txtInventoryCode.Focus()
                        Exit Sub
                    End If
                End If
            Else
                If Trim(txtInventoryCode.Text) = "" Then
                    Exit Sub
                End If
                'txtInventoryCode.SetFocus
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function fncCheckExisting(ByVal sinventorycode As String) As Boolean
        Dim icnt As Long
        Try
            fncCheckExisting = False
            For icnt = 0 To grdSales.RowCount - 1
                If Trim(fnGetdgvText(grdSales, icnt, ICode)) = Trim(sinventorycode) Then
                    fncCheckExisting = True
                    Exit For
                End If
            Next icnt
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Function fncGetInventory(ByVal sinventorycode As String) As Boolean
        Dim lSalesBillNo&
        Dim dsInv As New DataSet
        Dim dsIns As New DataSet
        Try
            dsInv = objInv.GetInventoryDetail(sinventorycode)
            fncGetInventory = True
            If dsInv.Tables(0).Rows.Count > 0 Then
                lSalesBillNo = Val(dsInv.Tables(0).Rows(0)("SalesBillNo") & "")
                'If Val(adoRS!SalesAmount & "") > 0 Then
                If UCase(sCompanyShortCode = "GMT") Then
                    If (Val(dsInv.Tables(0).Rows(0)("QtyOnHand") & "") = 0) And (Trim(dsInv.Tables(0).Rows(0)("CategoryCode")) <> "22KASR") Then
                        MsgBox("Item Already Sold for Invoice " & Trim(dsInv.Tables(0).Rows(0)("SalesBillNo") & "") & " on " & Format(dsInv.Tables(0).Rows(0)("SalesDAte"), "dd/MM/yyyy"))
                        fncGetInventory = False
                        txtInventoryCode.Focus()
                        Exit Function
                    End If
                    If (Val(dsInv.Tables(0).Rows(0)("QtyOnHand") & "") < 1) And (Trim(dsInv.Tables(0).Rows(0)("CategoryCode")) <> "22KASR") Then
                        MsgBox("No Stock for this Inventory!")
                        fncGetInventory = False
                        txtInventoryCode.Focus()
                        Exit Function
                    End If
                Else
                    If ((Val(dsInv.Tables(0).Rows(0)("QtyOnHand") & "") = 0) And (Val(dsInv.Tables(0).Rows(0)("QtySold") & "") = 0)) Then
                        MsgBox("No Stock for this Inventory!")
                        fncGetInventory = False
                        txtInventoryCode.Focus()
                        Exit Function
                    End If

                    If (Val(dsInv.Tables(0).Rows(0)("QtyOnHand") & "") = 0) Then
                        MsgBox("Item Already Sold for Invoice " & Trim(dsInv.Tables(0).Rows(0)("SalesBillNo") & "") & " on " & Format(dsInv.Tables(0).Rows(0)("SalesDAte"), "dd/MM/yyyy"))
                        fncGetInventory = False
                        txtInventoryCode.Focus()
                        Exit Function
                    End If
                    If (Val(dsInv.Tables(0).Rows(0)("QtyOnHand") & "") < 1) Then
                        MsgBox("No Stock for this Inventory!")
                        fncGetInventory = False
                        txtInventoryCode.Focus()
                        Exit Function
                    End If

                End If

                If Trim$(dsInv.Tables(0).Rows(0)("LocationCode") & "") <> gthisLocation Then
                    MsgBox("The selected Inventory belongs to the Location - " & Trim$(dsInv.Tables(0).Rows(0)("LocationCode") & "") & "!")
                    fncGetInventory = False
                    txtInventoryCode.Focus()
                    Exit Function
                End If

                If Trim$(dsInv.Tables(0).Rows(0)("Status") & "") = "IN" Then
                    dsIns = objInv.GetInstallmentDetail(sinventorycode)
                    If dsIns.Tables(0).Rows.Count > 0 Then
                        MsgBox("This Inventory is in Installment Status! " & Trim$(dsIns.Tables(0).Rows(0)("InsAgreementNo") & ""))
                        fncGetInventory = False
                        txtInventoryCode.Focus()
                    End If
                    Exit Function
                End If

                If Trim$(dsInv.Tables(0).Rows(0)("Status") & "") = "ON" Then
                    MsgBox("This Inventory is in Online Status!")
                    'fncGetInventory = False
                    'txtInventoryCode.Focus()
                    'Exit Function
                End If

                'added by sarathiy to check the inventory is ordered for some other customer
                'dAdvancePaid = 0.0
                If Trim$(dsInv.Tables(0).Rows(0)("OrderNo") & "") <> "" Then
                    Dim dsOrder As New DataSet
                    Dim objOrder As New clsOrder
                    '''dsOrder = objOrder.GetOrderDetail(Trim$(dsInv.Tables(0).Rows(0)("OrderNo") & ""))
                    dsOrder = objOrder.GetOrderHeader(Trim$(dsInv.Tables(0).Rows(0)("OrderNo") & ""))
                    If dsOrder.Tables(0).Rows.Count > 0 Then
                        If txtCustomerCode.Text.Trim <> Trim(dsOrder.Tables(0).Rows(0)("CustomerCode")) Then
                            MessageBox.Show("This inventory is ordered for some other customer. you Cant sale this now.")
                            fncGetInventory = False
                            txtInventoryCode.Focus()
                            Exit Function
                        Else
                            'added by sarathy to calculate advace paid amount
                            dAdvancePaid = Val(dsOrder.Tables(0).Rows(0)("AdvancePaid"))
                            sOrderRefNo = Trim$(dsInv.Tables(0).Rows(0)("OrderNo") & "")
                            sClassCode = Trim(dsInv.Tables(0).Rows(0)("ClassCode") & "")
                            'load class details to get the rate and the salestype.
                            subGetClass()
                            subGetCatalogue()
                            'chage the cost per gram if the as per ordered date
                            If dsOrder.Tables(0).Rows(0)("RateFixed") = "Y" Then
                                txtCostPerGram.Text = Format(Val(dsOrder.Tables(0).Rows(0)("CostPerGram")), "0.00")
                            End If
                            txtInventoryDescription.Text = Trim(dsInv.Tables(0).Rows(0)("Description") & "")
                            If msClsSalesType = "P" Or msClsSalesType = "B" Then
                                txtPCS.Enabled = True
                                txtStoneWeight.Text = Format(Val(dsInv.Tables(0).Rows(0)("StoneWeight") & ""), "0.00")
                                ''HAJ AS MUKKUTHI Sales by PCS on 29 Aug 2007
                                If UCase(sCompanyShortCode) = "ES" Or UCase(sCompanyShortCode) = "AS" Or UCase(sCompanyShortCode) = "HN" Then
                                    txtPCS.Text = 1
                                    'ElseIf UCase(sCompanyShortCode) = "HN" Then
                                    '    If txtPCS.Enabled = True Then txtPCS.Focus()
                                Else
                                    txtPCS.Text = 1 'Val(dsInv.Tables(0).Rows(0)("QtyOnHand") & "")
                                End If
                                If Val(dsInv.Tables(0).Rows(0)("Pcs")) > 0 Then
                                    txtWeight.Text = Format(Val(dsInv.Tables(0).Rows(0)("Weight") & "") * Val(txtPCS.Text) / Val(dsInv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                                    txtGoldLoss.Text = Format(Val(dsInv.Tables(0).Rows(0)("GoldLoss") & "") * Val(txtPCS.Text) / Val(dsInv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                                    txtSMC.Text = Format(Val(dsInv.Tables(0).Rows(0)("sMc") & "") * Val(txtPCS.Text) / Val(dsInv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                                    mdPurMC = Val(dsInv.Tables(0).Rows(0)("PMC") & "") * Val(txtPCS.Text) / Val(dsInv.Tables(0).Rows(0)("Pcs") & "")
                                    dTotalCost = Val(dsInv.Tables(0).Rows(0)("TotalCost") & "") * Val(txtPCS.Text) / Val(dsInv.Tables(0).Rows(0)("Pcs") & "")
                                Else
                                    txtWeight.Text = 0.0
                                    txtGoldLoss.Text = 0.0
                                    txtSMC.Text = 0.0
                                    mdPurMC = 0.0
                                    dTotalCost = 0.0
                                End If
                            Else
                                txtStoneWeight.Text = Format(Val(dsInv.Tables(0).Rows(0)("StoneWeight") & ""), "0.00")
                                txtPCS.Text = Val(dsInv.Tables(0).Rows(0)("Pcs") & "")
                                txtWeight.Text = Format(Val(dsInv.Tables(0).Rows(0)("Weight") & ""), "0.00")
                                txtGoldLoss.Text = Format(Val(dsInv.Tables(0).Rows(0)("GoldLoss") & ""), "0.00")
                                txtSMC.Text = Format(Val(dsInv.Tables(0).Rows(0)("sMc") & ""), "0.00")
                                mdPurMC = Val(dsInv.Tables(0).Rows(0)("PMC") & "")
                                dTotalCost = Val(dsInv.Tables(0).Rows(0)("TotalCost") & "")
                            End If
                        End If


                    End If
                Else
                    sClassCode = Trim(dsInv.Tables(0).Rows(0)("ClassCode") & "")
                    sCataloguecode = Trim(dsInv.Tables(0).Rows(0)("CatalogueCode") & "")
                    sCategorycode = Trim(dsInv.Tables(0).Rows(0)("CategoryCode") & "")
                    If chkShowImage.Checked Then
                        'Dim dsCatalogue As New DataSet
                        'Dim objClsCatalogue As New ClsCatalogue
                        'dsCatalogue = objClsCatalogue.GetCatalogueImage(Trim(sCataloguecode))
                        'If Not IsNothing(dsCatalogue) Then
                        '    If dsCatalogue.Tables("Catalogue").Rows.Count > 0 Then
                        '        Byte2Image(picImage.Image, dsCatalogue.Tables("Catalogue").Rows(0)("CatalogueImage"))
                        '    End If
                        'End If

                        Dim dsInventory As New DataSet
                        Dim objClsInventory As New clsInventory
                        dsInventory = objClsInventory.GetInventoryImage(Trim(sinventorycode))
                        If Not IsNothing(dsInventory) Then
                            If dsInventory.Tables("Inventory").Rows.Count > 0 Then
                                Byte2Image(picImage.Image, dsInventory.Tables("Inventory").Rows(0)("InventoryImage"))
                            End If
                        End If
                    End If
                    'load class details to get the rate and the salestype.
                    subGetClass()
                    subGetCatalogue()
                    subGetAddlDescription()
                    txtInventoryDescription.Text = Trim(dsInv.Tables(0).Rows(0)("Description") & "")
                    sSalesPcs = (dsInv.Tables(0).Rows(0)("SalesPCS") & "")
                    If msClsSalesType = "P" Or msClsSalesType = "B" Then
                        txtPCS.Enabled = True
                        txtStoneWeight.Text = Format(Val(dsInv.Tables(0).Rows(0)("StoneWeight") & ""), "0.00")
                        ''HAJ AS MUKKUTHI Sales by PCS on 29 Aug 2007
                        If UCase(sCompanyShortCode) = "ES" Or UCase(sCompanyShortCode) = "AS" Or UCase(sCompanyShortCode) = "HN" Then
                            txtPCS.Text = 1
                            'ElseIf UCase(sCompanyShortCode) = "HN" Then
                            '    If txtPCS.Enabled = True Then txtPCS.Focus()
                        Else
                            txtPCS.Text = 1 'Val(dsInv.Tables(0).Rows(0)("QtyOnHand") & "")
                        End If
                        If Val(dsInv.Tables(0).Rows(0)("Pcs")) > 0 Then
                            txtWeight.Text = Format(Val(dsInv.Tables(0).Rows(0)("Weight") & "") * Val(txtPCS.Text) / Val(dsInv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                            txtGoldLoss.Text = Format(Val(dsInv.Tables(0).Rows(0)("GoldLoss") & "") * Val(txtPCS.Text) / Val(dsInv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                            txtSMC.Text = Format(Val(dsInv.Tables(0).Rows(0)("sMc") & "") * Val(txtPCS.Text) / Val(dsInv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                            mdPurMC = Val(dsInv.Tables(0).Rows(0)("PMC") & "") * Val(txtPCS.Text) / Val(dsInv.Tables(0).Rows(0)("Pcs") & "")
                            dTotalCost = Val(dsInv.Tables(0).Rows(0)("TotalCost") & "") * Val(txtPCS.Text) / Val(dsInv.Tables(0).Rows(0)("Pcs") & "")
                        Else
                            txtWeight.Text = 0.0
                            txtGoldLoss.Text = 0.0
                            txtSMC.Text = 0.0
                            mdPurMC = 0.0
                            dTotalCost = 0.0
                        End If
                    Else
                        txtStoneWeight.Text = Format(Val(dsInv.Tables(0).Rows(0)("StoneWeight") & ""), "0.00")
                        txtPCS.Text = Val(dsInv.Tables(0).Rows(0)("Pcs") & "")

                        If Val(dsInv.Tables(0).Rows(0)("StoneWeight") & "") > 0 Then
                            If Trim(sCompanyShortCode) = "MJ" Then
                                txtWeight.Text = Format(Val(dsInv.Tables(0).Rows(0)("Weight") & "") - Val(dsInv.Tables(0).Rows(0)("StoneWeight") & ""), "0.00")
                            Else
                                txtWeight.Text = Format(Val(dsInv.Tables(0).Rows(0)("Weight") & "") - Val(dsInv.Tables(0).Rows(0)("StoneWeight") & ""), "0.00")
                            End If

                        Else
                            txtWeight.Text = Format(Val(dsInv.Tables(0).Rows(0)("Weight") & ""), "0.00")
                        End If

                        txtGoldLoss.Text = Format(Val(dsInv.Tables(0).Rows(0)("GoldLoss") & ""), "0.00")
                        ''HAJ jewel palace assorted item sales 23 jan 2008
                        If UCase(msClsSalesType) = "B" Then
                            txtSMC.Text = Format(Val(dsInv.Tables(0).Rows(0)("sMc") & "") / Val(dsInv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                        Else
                            txtSMC.Text = Format(Val(dsInv.Tables(0).Rows(0)("sMc") & ""), "0.00")
                        End If
                        mdPurMC = Val(dsInv.Tables(0).Rows(0)("PMC") & "")
                        dTotalCost = Val(dsInv.Tables(0).Rows(0)("TotalCost") & "")
                    End If
                End If
            Else
                MsgBox("Inventory Not Found ...")
                fncGetInventory = False
                txtInventoryCode.Focus()
                Exit Function
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function
    Public Sub Byte2Image(ByRef NewImage As Image, ByVal ByteArr() As Byte)
        Dim ImageStream As System.IO.MemoryStream
        Try
            If ByteArr.GetUpperBound(0) > 0 Then
                ImageStream = New MemoryStream(ByteArr)
                NewImage = System.Drawing.Image.FromStream(ImageStream)
            Else
                NewImage = Nothing
            End If
        Catch ex As Exception
            NewImage = Nothing
        End Try
    End Sub
    Private Sub subGetClass()
        Dim dsCls As New DataSet
        Try
            dsCls = objClass.GetClassDetail(sClassCode)
            If dsCls.Tables(0).Rows.Count > 0 Then
                txtCostPerGram.Text = Val(dsCls.Tables(0).Rows(0)("Rate") & "")
                msClsSalesType = Trim$(dsCls.Tables(0).Rows(0)("SalesType") & "")
            Else
                MessageBox.Show("Class Not defined for this item .... ")
                Exit Sub
            End If
            dsCls = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subGetCatalogue()
        Dim objCat As New ClsCatalogue
        Dim dsCls As New DataSet
        Try
            dsCls = objCat.GetCataloguePOSCode(sCataloguecode)
            If dsCls.Tables(0).Rows.Count > 0 Then
                dCataloguediscount = Val(dsCls.Tables(0).Rows(0)("Reorderlevel") & "")
            Else
                MessageBox.Show("Catalogue Not defined for this item .... ")
                Exit Sub
            End If
            dsCls = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subGetAddlDescription()
        Dim objCat As New clsInventory
        Dim dsCls As New DataSet
        Try
            dsCls = objCat.GetAdditionalInventoryDetail(txtInventoryCode.Text)
            If dsCls.Tables(0).Rows.Count > 0 Then
                dSellingPrice = Val(dsCls.Tables(0).Rows(0)("SellingPrice1") & "") + Val(dsCls.Tables(0).Rows(0)("SellingPrice2") & "") + Val(dsCls.Tables(0).Rows(0)("SellingPrice3") & "")
            End If
            dsCls = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtGoldLoss_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGoldLoss.LostFocus
        Try
            If Trim(txtInventoryCode.Text) <> "" Then
                If msClsSalesType <> "P" Then
                    ''txtTotalValue.Text = Format((Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram.Text) + Val(txtSMC.Text), "0.00")
                    If Val(sSalesPcs) = 0 Then
                        txtTotalValue.Text = Format((Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram.Text) + Val(txtSMC.Text), "0.00")
                    Else
                        txtTotalValue.Text = Format(Val(txtSMC.Text), "0.00")
                    End If
                    ''HAJ AS PCS Changes on 29 Aug 2007
                ElseIf msClsSalesType = "P" And UCase(sCompanyShortCode) = "AS" Then
                    txtTotalValue.Text = Format((Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram.Text) + Val(txtSMC.Text), "0.00")
                Else
                    txtTotalValue.Text = Format(Val(txtSMC.Text), "0.00")
                End If


                If chkTradeIn.Checked = True Then
                    txtGST.Text = 0.0
                    txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                    sInvGSTType = "T"
                End If
                If sInvGSTType = "" Or sInvGSTType = "Standard Rated Supply" Then
                    If optExclusive.Checked = True Then
                        txtGST.Text = Format$(Val(txtTotalValue.Text) * (dGSTPerc / 100), "#0.00")
                        txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                    ElseIf optInclusive.Checked = True Then
                        txtGST.Text = Format$((Val(txtTotalValue.Text) * dGSTPerc) / (100), "#0.00")
                        'txtGST.Text = Format$((Val(txtTotalValue.Text) * dGSTPerc) / (100 + dGSTPerc), "#0.00")
                        txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                    ElseIf optZeroRated.Checked = True Then
                        txtGST.Text = 0.0
                        txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                    End If
                ElseIf sInvGSTType = "Exempt Supply" Or sInvGSTType = "No GST" Then
                    txtGST.Text = 0.0
                    txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                End If
                
                '''If optExclusive.Checked = True Then
                '''    txtGST.Text = Format$(Val(txtTotalValue.Text) * (dGSTPerc / 100), "#0.00")
                '''    txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                '''ElseIf optInclusive.Checked = True Then
                '''    txtGST.Text = Format$((Val(txtTotalValue.Text) * dGSTPerc) / (100), "#0.00")
                '''    'txtGST.Text = Format$((Val(txtTotalValue.Text) * dGSTPerc) / (100 + dGSTPerc), "#0.00")
                '''    txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                '''ElseIf optZeroRated.Checked = True Then
                '''    txtGST.Text = 0.0
                '''    txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                '''End If

            Else
                'commented becoz press on F4 focus going to inventory code instead of cash option control.
                'txtInventoryCode.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtSMC_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSMC.LostFocus
        Try
            If Trim(txtInventoryCode.Text) <> "" Then
                If txtPCS.Enabled And Val(txtPCS.Text) < 1 Then
                    'txtPCS.SetFocus
                    Exit Sub
                End If
                If Val(txtPCS.Text) < 1 Then txtPCS.Text = 1
                'SMC should not be less than PMC
                'If Val(txtSMC) < Val(mdPurMC) Then
                '    MsgBox "Please enter the SMC greater than " & Val(mdPurMC)
                '    txtSMC.SetFocus
                '    Exit Sub
                'End If
                If msClsSalesType <> "P" Then
                    ' txtTotalValue.Text = Format((Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram.Text) + Val(txtSMC.Text), "0.00")
                    If Val(sSalesPcs) = 0 Then
                        txtTotalValue.Text = Format((Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram.Text) + Val(txtSMC.Text), "0.00")
                    Else
                        txtTotalValue.Text = Format(Val(txtSMC.Text), "0.00")
                    End If
                    ''HAJ AS PCS Changes on 29 Aug 2007
                ElseIf msClsSalesType = "P" And UCase(sCompanyShortCode) = "AS" Then
                    txtTotalValue.Text = Format((Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram.Text) + Val(txtSMC.Text), "0.00")
                Else
                    txtTotalValue.Text = Format(Val(txtSMC.Text), "0.00") * Val(txtPCS.Text)
                End If

                ''HAJ jewel Palace Assorted items sales on 23 Jan 2008
                If msClsSalesType = "B" Then
                    txtTotalValue.Text = Format((Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram.Text) + Val(txtSMC.Text), "0.00")
                End If

                If chkTradeIn.Checked = True Then
                    txtGST.Text = 0.0
                    txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                    sInvGSTType = "T"
                End If

                If sInvGSTType = "" Or sInvGSTType = "Standard Rated Supply" Then
                    If optExclusive.Checked = True Then
                        txtGST.Text = Format$(Val(txtTotalValue.Text) * (dGSTPerc / 100), "#0.00")
                        txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                    ElseIf optInclusive.Checked = True Then
                        txtGST.Text = Format$((Val(txtTotalValue.Text) * dGSTPerc) / (100), "#0.00")
                        'txtGST.Text = Format$((Val(txtTotalValue.Text) * dGSTPerc) / (100 + dGSTPerc), "#0.00")
                        txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                    ElseIf optZeroRated.Checked = True Then
                        txtGST.Text = 0.0
                        txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                    End If
                ElseIf sInvGSTType = "Exempt Supply" Or sInvGSTType = "No GST" Then
                    txtGST.Text = 0.0
                    txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                End If
                
                '''If optExclusive.Checked = True Then
                '''    txtGST.Text = Format$(Val(txtTotalValue.Text) * (dGSTPerc / 100), "#0.00")
                '''    txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                '''ElseIf optInclusive.Checked = True Then
                '''    txtGST.Text = Format$((Val(txtTotalValue.Text) * dGSTPerc) / (100), "#0.00")
                '''    'txtGST.Text = Format$((Val(txtTotalValue.Text) * dGSTPerc) / (100 + dGSTPerc), "#0.00")
                '''    txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                '''ElseIf optZeroRated.Checked = True Then
                '''    txtGST.Text = 0.0
                '''    txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                '''End If
                'txtGST.Text = Format$(Val(txtTotalValue.Text) * (dGSTPerc / 100), "#0.00")
                'txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                If sDetailDiscount = "1" Then
                    txtTotalValue.Focus()
                Else
                    subAddSales()
                End If

            Else
                txtInventoryCode.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subAddSales()
        Dim r As Long
        Try
            If txtInventoryCode.Text.Trim <> "" Then
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
                '    'grdSales.RowHeight(grdSales.ActiveRow) = GRDROWHT
                'End If

                If Not bEdited Then
                    If grdSales.RowCount = 0 Then
                        grdSales.Rows.Add()
                    Else
                        'grdJobSheet.Row = grdJobSheet.MaxRows
                        'grdJobSheet.Action = 5
                        'grdJobSheet.MaxRows = grdJobSheet.MaxRows - 1
                        'grdJobSheet.MaxRows = grdJobSheet.MaxRows + 1
                    End If
                    r = grdSales.RowCount - 1
                Else
                    r = grdSales.CurrentRow.Index
                End If



                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(ICode).Style.ForeColor = Color.Black
                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(ICode).Style.Font = New Font(grdSales.Font.FontFamily, 10, FontStyle.Regular)

                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(IWeight).Style.ForeColor = Color.Black
                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(IWeight).Style.Font = New Font(grdSales.Font.FontFamily, 10, FontStyle.Regular)

                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(IGoldLoss).Style.ForeColor = Color.Black
                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(IGoldLoss).Style.Font = New Font(grdSales.Font.FontFamily, 10, FontStyle.Regular)

                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(IPMC).Style.ForeColor = Color.Black
                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(IPMC).Style.Font = New Font(grdSales.Font.FontFamily, 10, FontStyle.Regular)

                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(ISMC).Style.ForeColor = Color.Black
                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(ISMC).Style.Font = New Font(grdSales.Font.FontFamily, 10, FontStyle.Regular)

                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(IPCS).Style.ForeColor = Color.Black
                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(IPCS).Style.Font = New Font(grdSales.Font.FontFamily, 10, FontStyle.Regular)

                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(ITotalValue).Style.ForeColor = Color.Black
                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(ITotalValue).Style.Font = New Font(grdSales.Font.FontFamily, 10, FontStyle.Regular)




                ''''added by sarathy to detect the advance paid amount for the ordered items
                '''If dAdvancePaid <> 0 Then txtTotalValue.Text = Format(Val(txtTotalValue.Text) - Val(dAdvancePaid), "0.00")
                TotVal = Val(txtTotalValue.Text)
                sMc = Val(txtSMC.Text)

                Call fnSetdgvText(grdSales, r, ICode, Trim(txtInventoryCode.Text))
                Call fnSetdgvText(grdSales, r, IClass, Trim(sClassCode))
                Call fnSetdgvText(grdSales, r, IDescription, Trim(txtInventoryDescription.Text))
                Call fnSetdgvText(grdSales, r, IWeight, Format(Val(txtWeight.Text), "0.00"))
                Call fnSetdgvText(grdSales, r, IGoldLoss, Format(Val(txtGoldLoss.Text), "0.00"))
                Call fnSetdgvText(grdSales, r, ICostPerGram, Format(Val(txtCostPerGram.Text), "0.00"))
                Call fnSetdgvText(grdSales, r, IDSWeight, Val(txtStoneWeight.Text))
                Call fnSetdgvText(grdSales, r, ISMC, Format(Val(txtSMC.Text), "0.00"))
                Call fnSetdgvText(grdSales, r, IPMC, Val(mdPurMC))
                Call fnSetdgvText(grdSales, r, IPCS, Val(txtPCS.Text))
                Call fnSetdgvText(grdSales, r, ITotalValue, Format(Val(txtTotalValue.Text), "0.00"))
                Call fnSetdgvText(grdSales, r, ITotalCost, Val(dTotalCost))
                Call fnSetdgvText(grdSales, r, ISalesType, Trim$(msClsSalesType))
                Call fnSetdgvText(grdSales, r, IGST, Val(txtGST.Text))
                Call fnSetdgvText(grdSales, r, IDiscount, Val(NTotVal))

                'grdSales.Row = grdSales.MaxRows
                'grdSales.Col = 1
                'grdSales.Action = 0
                subTotalFromGrid()
                subTotal()
                subClearDetail()
                'SHabeer 20150226
                If grdSales.RowCount >= 1 Then
                    optZeroRated.Enabled = False
                    optInclusive.Enabled = False
                    optExclusive.Enabled = False
                End If
                'SHabeer 20150226
                ''haj 
                bEdited = False
            End If
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

                If optExclusive.Checked = True Then
                    sTiGST = Format$(Val(sTiTotalValue) * (dGSTPerc / 100), "#0.00")
                    sTiTotalValue = Format(Val(sTiTotalValue) + Val(sTiGST), "0.00")
                ElseIf optInclusive.Checked = True Then
                    If UCase(sCompanyShortCode) = "MJ" Or UCase(sCompanyShortCode) = "SSJ" Or UCase(sCompanyShortCode) = "AJ" Or UCase(sCompanyShortCode) = "MD" Then
                        sTiTotalValue = Format(Val(sTiTotalValue) + 0, "0.00")
                        dTradeInGST = dTradeInGST + Val(-Format$((Val(sTiTotalValue) * dGSTPerc) / (109), "#0.00"))
                        dTradeInSales = dTradeInSales + Format(Val(sTiTotalValue) + 0, "0.00")
                    Else
                        sTiGST = Format$((Val(sTiTotalValue) * dGSTPerc) / (100), "#0.00")
                        sTiTotalValue = Format(Val(sTiTotalValue) + Val(sTiGST), "0.00")
                    End If
                ElseIf optZeroRated.Checked = True Then
                    sTiGST = 0.0
                    sTiTotalValue = Format(Val(sTiTotalValue) + Val(sTiGST), "0.00")
                    dTradeInGST = dTradeInGST + Val(-Format$((Val(sTiTotalValue) * dGSTPerc) / (109), "#0.00"))
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


                subTotalFromGrid()
                subTotal()
                subClearDetail()

                bEdited = False
            Next iRow

            dsTradeIn.Tables(0).Rows.Clear()
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

                sSalesType = "TRADEIN"

                bStatus = objTI.TradeInHeaderSaveSales(Trim(sTIinventorycode), Trim$(sInvoiceNo), _
                          gthisLocation, UCase(Trim(txtCustomerCode.Text)), Trim(txtMemberCode.Text), Trim(txtCustomerName.Text), Trim(txtAddress1.Text), _
                          Trim(txtAddress2.Text), Trim(txtAddress3.Text), _
                          Val(txtCostPerGram.Text), Val(nWeight), _
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

    Private Sub btnCustomerCodeSearch_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCustomerCodeSearch.LostFocus
        Try
            subGetCustomerDetail()
            sCustomerCode = Trim(txtCustomerCode.Text)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnCustomerCodeSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnCustomerCodeSearch.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then btnCustomerCodeSearch_Click(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subGetGSTAmount()
        Try
            Dim nGSTAmount As Double
            If sInvGSTType = "T" Then
                lblSalesType.Text = "ZERO RATED SALES"
                lblTax.Text = Format(0, "0.00")
                lblNetTotal.Text = Format(Val(lblNetValue.Text), "0.00")
                txtNetAmount.Text = Val(lblNetTotal.Text)
            End If
            If sInvGSTType = "" Or sInvGSTType = "Standard Rated Supply" Then
                If optExclusive.Checked = True Then
                    lblSalesType.Text = "EXCLUSIVE SALES"
                    If dGSTPerc > 0 Then
                        nGSTAmount = Val(lblNetValue.Text) * dGSTPerc / 100
                    Else
                        nGSTAmount = 0
                    End If
                    lblTax.Text = Format(nGSTAmount, "0.00")
                    lblNetTotal.Text = Format(Val(lblNetValue.Text) + nGSTAmount, "0.00")
                    txtNetAmount.Text = Val(lblNetTotal.Text)
                ElseIf optInclusive.Checked = True Then
                    lblSalesType.Text = "INCLUSIVE SALES"
                    If dGSTPerc > 0 Then
                        nGSTAmount = (Val(lblNetValue.Text) * dGSTPerc) / (100 + dGSTPerc)
                    Else
                        nGSTAmount = 0
                    End If
                    lblTax.Text = Format(nGSTAmount, "0.00")
                    lblNetTotal.Text = Format(Val(lblNetValue.Text), "0.00")
                    txtNetAmount.Text = Val(lblNetTotal.Text)
                ElseIf optZeroRated.Checked = True Then
                    lblSalesType.Text = "ZERO RATED SALES"
                    lblTax.Text = Format(0, "0.00")
                    lblNetTotal.Text = Format(Val(lblNetValue.Text), "0.00")
                    txtNetAmount.Text = Val(lblNetTotal.Text)
                End If
            ElseIf sInvGSTType = "Exempt Supply" Or sInvGSTType = "No GST" Then
                lblSalesType.Text = "ZERO RATED SALES"
                optZeroRated.Checked = True
                optInclusive.Checked = False
                optExclusive.Checked = False
                lblTax.Text = Format(0, "0.00")
                lblNetTotal.Text = Format(Val(lblNetValue.Text), "0.00")
                txtNetAmount.Text = Val(lblNetTotal.Text)
            End If
            '''If optExclusive.Checked = True Then
            '''    lblSalesType.Text = "EXCLUSIVE SALES"
            '''    If dGSTPerc > 0 Then
            '''        nGSTAmount = Val(lblNetValue.Text) * dGSTPerc / 100
            '''    Else
            '''        nGSTAmount = 0
            '''    End If
            '''    lblTax.Text = Format(nGSTAmount, "0.00")
            '''    lblNetTotal.Text = Format(Val(lblNetValue.Text) + nGSTAmount, "0.00")
            '''ElseIf optInclusive.Checked = True Then
            '''    lblSalesType.Text = "INCLUSIVE SALES"
            '''    If dGSTPerc > 0 Then
            '''        nGSTAmount = (Val(lblNetValue.Text) * dGSTPerc) / (100 + dGSTPerc)
            '''    Else
            '''        nGSTAmount = 0
            '''    End If
            '''    lblTax.Text = Format(nGSTAmount, "0.00")
            '''    lblNetTotal.Text = Format(Val(lblNetValue.Text), "0.00")
            '''ElseIf optZeroRated.Checked = True Then
            '''    lblSalesType.Text = "ZERO RATED SALES"
            '''    lblTax.Text = Format(0, "0.00")
            '''    lblNetTotal.Text = Format(Val(lblNetValue.Text), "0.00")
            '''End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub optExclusive_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optExclusive.Click
        subGetGSTAmount()
    End Sub

    Private Sub optInclusive_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optInclusive.Click
        subGetGSTAmount()
    End Sub

    Private Sub optZeroRated_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optZeroRated.Click
        subGetGSTAmount()
    End Sub



    Private Sub txtTotalValue_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTotalValue.KeyDown
        'Try
        '    If e.KeyCode = Keys.Return Then
        '        tempTV = Val(txtTotalValue.Text)
        '        '''        tempSMC = Val(txtSMC.Text)
        '        If TotVal > tempTV Then
        '            Diff = TotVal - tempTV
        '            NTotVal = tempTV
        '            '''            If sMc - Diff >= 0 Then
        '            '''                NSMC = sMc - Diff
        '            '''                subAddSales1()
        '            '''            Else
        '            '''                MsgBox("More Reduction in Total Value")
        '            '''                txtTotalValue.Text = TotVal
        '            '''                txtTotalValue.Focus()
        '            '''                Exit Sub
        '            '''            End If
        '        End If
        '        '''        If TotVal <= tempTV Then
        '        '''            Diff = tempTV - TotVal
        '        '''            NTotVal = tempTV
        '        '''            NSMC = sMc + Diff
        '        '''            subAddSales1()
        '        '''        End If
        '        '''        grdSales.Focus()
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
    End Sub
    Private Sub subAddSales1()
        'Try
        '    grdSales.ForeColor = Color.Black
        '    grdSales.FontBold = False

        '    If Not bEdited Then
        '        If grdSales.MaxRows = 0 Then
        '            grdSales.MaxRows = grdSales.MaxRows + 1
        '        Else
        '            grdSales.Row = grdSales.MaxRows
        '            grdSales.Action = 5
        '            grdSales.MaxRows = grdSales.MaxRows - 1
        '            grdSales.MaxRows = grdSales.MaxRows + 1
        '        End If

        '        grdSales.Row = grdSales.MaxRows
        '    Else
        '        grdSales.Row = grdSales.ActiveRow
        '        'grdSales.RowHeight(grdSales.ActiveRow) = GRDROWHT
        '    End If
        '    Call fnSetdgvText(grdSales, grdSales.Row, ICode, Trim(txtInventoryCode.Text))
        '    Call fnSetdgvText(grdSales, grdSales.Row, IClass, Trim(sClassCode))
        '    Call SetText(grdSales, grdSales.Row, IDescription, Trim(txtInventoryDescription.Text))
        '    Call SetText(grdSales, grdSales.Row, IWeight, Val(txtWeight.Text))
        '    Call SetText(grdSales, grdSales.Row, IGoldLoss, Val(txtGoldLoss.Text))
        '    Call SetText(grdSales, grdSales.Row, ICostPerGram, Val(txtCostPerGram.Text))
        '    Call SetText(grdSales, grdSales.Row, IDSWeight, Val(txtStoneWeight.Text))
        '    Call SetText(grdSales, grdSales.Row, ISMC, Val(NSMC))
        '    Call SetText(grdSales, grdSales.Row, IPMC, Val(mdPurMC))
        '    Call SetText(grdSales, grdSales.Row, IPCS, Val(txtPCS.Text))
        '    Call SetText(grdSales, grdSales.Row, ITotalValue, Val(NTotVal))
        '    Call SetText(grdSales, grdSales.Row, ITotalCost, Val(dTotalCost))
        '    Call SetText(grdSales, grdSales.Row, ISalesType, Trim$(msClsSalesType))
        '    Call SetText(grdSales, grdSales.Row, IGST, Val(txtGST.Text))
        '    grdSales.Row = grdSales.MaxRows - 1
        '    grdSales.Col = 1
        '    grdSales.Action = 0
        '    subTotal()
        '    subClearDetail()
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
    End Sub

    Private Sub btnReprintOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReprintOK.Click
        Dim sDate As Date
        Dim strDate As String
        Dim dsInvoice As New DataSet
        Try
            If Trim(txtRePrintBillNo.Text) <> "" Then
                bReprint = True

                dsInvoice = objSales.GetSalesHeaderByInvoiceNo(Trim(txtRePrintBillNo.Text))
                If dsInvoice.Tables(0).Rows.Count > 0 Then
                    sDate = dsInvoice.Tables(0).Rows(0)("InvoiceDate") & ""
                    strDate = Format(CDate(sDate), "yyyy/MM/dd")
                    'sDate = dsInvoice.Tables(0).Rows(0)("InvoiceDate")
                    'strDate = Mid(sDate, 7, 4)
                    'strDate = strDate & Mid(sDate, 4, 2)
                    'strDate = strDate & Mid(sDate, 1, 2)
                    ' Shabeer 2012 05 17
                    'If ServerDate = CDate(Trim(strDate)) Then
                    Call subPrintBill(Trim(txtRePrintBillNo.Text))
                    bReprint = False
                    PnlReprint.Visible = False
                    subClear()
                    'Else
                    '    subGetAdminPasword()
                    'End If
                Else
                    MessageBox.Show(" BillNo is not Correct!...")
                    bReprint = False
                    PnlReprint.Visible = False
                    subClear()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnReprintCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReprintCancel.Click
        Try
            txtRePrintBillNo.Text = ""
            PnlReprint.Visible = False
            txtInventoryCode.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnFindCostOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFindCostOK.Click
        Try
            pnlFindCost.Visible = False
            txtFindCostPerGram.Clear()
            txtFindDescription.Clear()
            txtFindInventory.Clear()
            txtFindMC.Clear()
            txtFindPBillNo.Clear()
            txtFindPurchaseDate.Clear()
            txtFindTotalCost.Clear()
            txtFindWeight.Clear()
            txtTael.Clear()
            txtInventoryCode.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtFindInventory_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFindInventory.LostFocus
        Dim lSalesBillNo&
        Dim sSuppllierNo As String
        Dim dsInv As New DataSet
        Dim dsSup As New DataSet
        Try
            If Trim(txtFindInventory.Text) <> "" Then
                dsInv = objInv.GetInventoryDetail(txtFindInventory.Text)
                If dsInv.Tables(0).Rows.Count > 0 Then
                    txtFindDescription.Text = Trim(dsInv.Tables(0).Rows(0)("Description") & "")
                    txtFindCostPerGram.Text = Val(dsInv.Tables(0).Rows(0)("CostperGram") & "")
                    txtFindMC.Text = Val(dsInv.Tables(0).Rows(0)("PMC") & "")
                    txtFindPBillNo.Text = Trim(dsInv.Tables(0).Rows(0)("PurchaseBillNo") & "")
                    txtFindPurchaseDate.Text = Trim(dsInv.Tables(0).Rows(0)("PurchaseDate") & "")
                    txtFindTotalCost.Text = Val(dsInv.Tables(0).Rows(0)("TotalCost") & "")
                    txtFindWeight.Text = Val(dsInv.Tables(0).Rows(0)("Weight") & "")
                    sSuppllierNo = Trim(dsInv.Tables(0).Rows(0)("SupplierCode") & "")
                    txtSupplierCode.Text = Trim(sSuppllierNo)
                    dsSup = objSupplier.GetSupplierDetail(sSuppllierNo)
                    If dsSup.Tables(0).Rows.Count > 0 Then
                        If Trim(dsSup.Tables(0).Rows(0)("GSTType") & "") = "E" Then
                            txtTael.Text = Format((((Val(txtFindCostPerGram.Text) / Val(GstTaelPercentage)) / 0.92) / 0.02646), "0")
                        Else
                            txtTael.Text = Format((((Val(txtFindCostPerGram.Text)) / 0.92) / 0.02646), "0")
                        End If
                    End If
                Else
                    MessageBox.Show("Inventory / Barcode Not Found ....")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnInstalmentCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInstalmentCancel.Click
        Try
            optCashSales.Checked = True
            pnlPaymentType.Visible = False
            txtInventoryCode.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnPaymodeSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPaymodeSearch.Click
        Try
            Dim frmHelpPayMode As New frmHelpSearch(0, 0, "tblPaymode", "L", 4, "Position", " Active  = 1 ", "Paymode", "Paymode", 80, "Description", "Description", 150, "Commission", "Commission", 90, "Position", "Position", 80)
            frmHelpPayMode.ShowDialog()
            txtPayMode.Text = frmHelpPayMode.Var1.Trim

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtPayMode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPayMode.LostFocus
        Dim dsPM As New DataSet
        Try
            If Trim(txtPayMode.Text) <> "" Then
                dsPM = objPaymode.GetPayModeDetail(txtPayMode.Text)
                If dsPM.Tables(0).Rows.Count > 0 Then
                    mdTotalCommission = Val(dsPM.Tables(0).Rows(0)("Commission") & "")
                    txtPMSurCharge.Text = (mdTotalCommission / 100) * Val(lblNetTotal.Text)
                End If
                dsPM = Nothing
            Else
                txtPMSurCharge.Text = 0
                mdTotalCommission = 0
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        ''HAJ
        grpType.Visible = False
    End Sub

    Private Sub txtPayMode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPayMode.TextChanged
        Try
            If Len(txtPayMode.Text.Trim) = 1 Then

                Select Case Mid(txtPayMode.Text, 1, 1)
                    Case "C", "c", "N", "n", "V", "v", "A", "a", "M", "m", "D", "d"
                        txtPayMode.Text = "CASH"
                    Case "T", "t"
                        'txtPayMode.Text = "TRADE-IN"
                    Case "I", "i"
                        txtPayMode.Text = "INSTALMENT"
                        If sSalesType = "TRADEIN" And txtPayMode.Text = "INSTALMENT" Then
                            MessageBox.Show("TradeIn Sales Not Allowed In Installment")
                            Exit Sub
                        End If
                    Case Else
                        txtPayMode.Text = "CASH"
                End Select
            End If
            If Trim(txtPayMode.Text) <> "" Then
                If (txtLossPer.Enabled = True And txtLossPer.Text.Trim = "" And txtPayMode.Text = "TRADE-IN") Then
                    txtLossPer.Focus()
                Else
                    btnPMPayment.Focus()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtPMDiscount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPMDiscount.LostFocus
        Try
            'If Val(txtPMDiscount.Text) <> 0 Then
            txtPMNetTotal.Text = Format(Val(txtGrandTotal.Text) - Val(txtPMDiscount.Text), "0.00")
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtPMSurCharge_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPMSurCharge.LostFocus
        Try
            txtTotalFinal.Text = Val(txtPMNetTotal.Text) + Val(txtPMSurCharge.Text)
            If Val(txtPMNetTotal.Text) > 0 Then
                txtPMDiscount.Text = Val(txtGrandTotal.Text) - Val(txtPMNetTotal.Text)
            End If
            txtPMDiscount.Text = Val(txtPMDiscount.Text)
            txtPMDiscount_LostFocus(sender, e)
            btnPaymentTypeOK.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtNetDiscount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNetDiscount.LostFocus
        Try
            Dim dTempDisc As Double
            dTempDisc = Val(lblSalesAmount.Text) * Val(dDisPercentage) / 100
            If Val(txtNetDiscount.Text) > dTempDisc Then
                MessageBox.Show("More Discount cannot allowed !!!" + vbLf + "Max Discount Amount : $" + Format(dTempDisc, "0.00") + vbLf + "Max Discount : " + Format(dDisPercentage, "0.00") + "%")
                txtNetDiscount.Text = ""
                txtNetDiscount.Focus()
                Exit Sub
            End If
            If Val(txtNetDiscount.Text) > 0 Then
                lblNetDiscount.Text = Format(Val(txtNetDiscount.Text), "0.00")
                lblNetValue.Text = Format(Val(lblSalesAmount.Text) - Val(lblNetDiscount.Text), "0.00")
                subGetGSTAmount()
                PnlNetAmount.Visible = False
                txtInventoryCode.Focus()
                Exit Sub
            Else
                lblNetDiscount.Text = Format(Val(txtNetDiscount.Text), "0.00")
                lblNetValue.Text = Format(Val(lblSalesAmount.Text) - Val(lblNetDiscount.Text), "0.00")
                subGetGSTAmount()
                PnlNetAmount.Visible = False
                txtInventoryCode.Focus()
                Exit Sub
            End If
            If Val(txtNetDiscountPercentage.Text) > 0 Then
                sDiscountType = "Percentage"
            ElseIf Val(txtNetDiscount.Text) > 0 Then
                sDiscountType = "Amount"
            Else
                sDiscountType = "Nill"
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnBillCancelOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBillCancelOK.Click
        Dim dsSal As New DataSet
        Dim dsIns As New DataSet
        Dim sMsg As String
        Dim sCustMemCode As String
        Dim dCustNetTotal As Double
        Try
            dsSal = objSalesReturn.GetSalesHeaderByInvoiceNo(Trim(txtBillCancelBillNo.Text))
            If dsSal.Tables(0).Rows.Count > 0 Then
                If DateValue(dsSal.Tables(0).Rows(0)("BDate") & "") = DateValue(Trim(ServerDate)) Then
                    'If DateValue(DsHdr.Tables("Invoice").Rows(0)("InvoiceDate") & "") = DateValue(Date.Now) Then
                    'check whether paid installment
                    sCustMemCode = Trim(dsSal.Tables(0).Rows(0)("CustomerCode"))
                    dCustNetTotal = Val(dsSal.Tables(0).Rows(0)("NetTotal"))
                    dsIns = objIns.GetInstallmentDetail(Trim(dsSal.Tables(0).Rows(0)("InsAgreementNo")))
                    If dsIns.Tables(0).Rows.Count > 0 Then
                        sMsg = MsgBox("Installment Paid for this Invoice..Do you want to CANCEL the bill No. " & Trim(txtBillCancelBillNo.Text) & " ? ", vbYesNo)
                    Else
                        sMsg = MsgBox("Do you want to CANCEL the bill No. " & Trim(txtBillCancelBillNo.Text) & " ? ", vbYesNo)
                    End If
                    If sMsg = vbYes Then


                        iRet = objSalesReturn.BillCancel(Trim(txtBillCancelBillNo.Text), Trim(sLocationCode), sUserID)
                        pnlBillCancel.Visible = False
                        If Trim(sCompanyShortCode) = "AJ" Then
                            subPrintBillCancel(Trim(txtBillCancelBillNo.Text))
                        End If
                        txtInventoryCode.Focus()
                        Exit Sub
                    End If
                Else
                    MessageBox.Show("You cannot cancel History Bills ....")
                    pnlBillCancel.Visible = False
                    txtInventoryCode.Focus()
                    Exit Sub
                End If
            Else
                MessageBox.Show("This Bill Not Found... Please Give Correct Bill No")
                txtBillCancelBillNo.Clear()
                txtBillCancelBillNo.Focus()
            End If
            dsIns = Nothing
            dsSal = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnBillCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBillCancel.Click
        Try
            txtBillCancelBillNo.Text = ""
            pnlBillCancel.Visible = False
            txtInventoryCode.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub



    Private Sub btnExpenseClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExpenseClose.Click
        pnlExpense.Visible = False
    End Sub

    Private Sub btnAdminPwdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdminPwdOK.Click
        Dim dsPWD As New DataSet
        Try
            If Trim(txtAdminPassword.Text) <> "" Then
                dsPWD = objUM.GetAdminPassword(Trim(txtAdminPassword.Text))
                If dsPWD.Tables(0).Rows.Count > 0 Then
                    If Trim(UCase(dsPWD.Tables(0).Rows(0)("UserGroupCode") & "")) = "ADMIN" Then
                        sUserCode = Trim(UCase(dsPWD.Tables(0).Rows(0)("UserCode") & ""))
                        sUserName = Trim(UCase(dsPWD.Tables(0).Rows(0)("UserName") & ""))
                        bPassword = True
                        pnlAdminPwd.Visible = False
                        Dim objSalesMan As New clsSalesMan
                        If objSalesMan.SalesManLogSave(sUserCode, sUserName, "Re-Print", nAllowSalesReturn) Then
                        Else
                            MessageBox.Show("Error in Log Save")
                        End If
                        'If PnlReprint.Visible = True Then
                        '    Call subPrintBill(Trim(txtRePrintBillNo.Text))
                        '    bReprint = False
                        '    PnlReprint.Visible = False
                        '    subClear()
                        'Else
                        '    Payment()
                        'End If
                        PnlReprint.Visible = True
                        txtRePrintBillNo.Text = ""
                        txtRePrintBillNo.Focus()
                    Else
                        '  MessageBox.Show("Please Check the Password")
                        MsgBox("SalesMan Has No Rights to do RePrint!", MsgBoxStyle.Critical)
                        txtAdminPassword.Text = ""
                        If (txtAdminPassword.Enabled And txtAdminPassword.Visible) Then txtAdminPassword.Focus()
                        bPassword = False
                        Exit Sub
                    End If
                Else
                    'MessageBox.Show("Please Check the Password")
                    MsgBox("SalesMan Has No Rights to do RePrint!", MsgBoxStyle.Critical)
                    txtAdminPassword.Text = ""
                    If (txtAdminPassword.Enabled And txtAdminPassword.Visible) Then txtAdminPassword.Focus()
                    bPassword = False
                    Exit Sub
                End If
            Else
                MessageBox.Show("Enter the Password")
                txtAdminPassword.Focus()
                bPassword = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Payment()
        Try
            If Val(lblNetTotal.Text) <> 0 Then
                optCashSales.Checked = True
                pnlTotal.Visible = True
                txtPayMode.Text = "CASH"
                txtGrandTotal.Text = Format(Val(lblSalesAmount.Text), "0.00")
                txtPMGST.Text = Format(Val(lblTax.Text), "0.00")
                txtPMDiscount.Text = Format(Val(lblNetDiscount.Text), "0.00")
                txtPMNetTotal.Text = Format(Val(lblNetValue.Text), "0.00")
                txtTotalFinal.Text = Format((Val(txtPMNetTotal.Text) + Val(txtPMSurCharge.Text)), "0.00")
                'txtPayMode.Clear()
                txtPayMode.Focus()
                'subPaymentTypeOk()
            Else
                txtInventoryCode.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnAdminPwdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdminPwdCancel.Click
        Try
            txtAdminPassword.Clear()
            pnlAdminPwd.Visible = False
            bPassword = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtNetDiscountPercentage_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNetDiscountPercentage.TextChanged
        Dim dTot As Double
        Try
            dTot = Val(txtNetDiscountPercentage.Text) / 100
            txtNetDiscount.Text = Val(lblSalesAmount.Text) * dTot
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtNetAmount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNetAmount.LostFocus
        Try

            'If Val(lblSalesAmount.Text) - Val(txtNetAmount.Text) > Val((Val(dTotalWeight)) * 5) Then
            'MessageBox.Show("NetAmount Should be Greater than or equal to " & Format((Val(lblSalesAmount.Text) - Val((Val(dTotalWeight)) * 5)), "0.00"))
            'txtNetAmount.Text = Format(Val(lblSalesAmount.Text), "0.00")
            'txtNetAmount.Focus()
            'Exit Sub
            'End If
            If sSalesType = "TRADEIN" Then
                If Val(txtNetAmount.Text) >= 0 Then
                    lblNetDiscount.Text = Format(Val(lblSalesAmount.Text) - Val(txtNetAmount.Text), "0.00")
                    lblNetValue.Text = Format(Val(lblSalesAmount.Text) - Val(lblNetDiscount.Text), "0.00")
                    subGetGSTAmount()
                    PnlNetAmount.Visible = False
                    txtInventoryCode.Focus()
                    Exit Sub
                End If
            Else
                If Val(txtNetAmount.Text) <> 0 Then
                    lblNetDiscount.Text = Format(Val(lblSalesAmount.Text) - Val(txtNetAmount.Text), "0.00")
                    lblNetValue.Text = Format(Val(lblSalesAmount.Text) - Val(lblNetDiscount.Text), "0.00")
                    subGetGSTAmount()
                    PnlNetAmount.Visible = False
                    txtInventoryCode.Focus()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtCashSales_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCashSales.GotFocus
        Try
            txtCashSales.BackColor = Color.Red
            txtCashSales.ForeColor = Color.Green
            txtTradeIn.BackColor = Color.White
            txtTradeIn.ForeColor = Color.Black
            txtInstalment.BackColor = Color.White
            txtInstalment.ForeColor = Color.Black
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtTradeIn_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTradeIn.GotFocus
        Try
            txtTradeIn.BackColor = Color.Red
            txtTradeIn.ForeColor = Color.Green
            txtCashSales.BackColor = Color.White
            txtCashSales.ForeColor = Color.Black
            txtInstalment.BackColor = Color.White
            txtInstalment.ForeColor = Color.Black
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtInstalment_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInstalment.GotFocus
        Try
            txtInstalment.BackColor = Color.Red
            txtInstalment.ForeColor = Color.Green
            txtTradeIn.BackColor = Color.White
            txtTradeIn.ForeColor = Color.Black
            txtCashSales.BackColor = Color.White
            txtCashSales.ForeColor = Color.Black
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub



    Private Sub txtPayMode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPayMode.GotFocus
        Try
            ''HAJ 
            grpType.Visible = True
            txtPayMode.SelectAll()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnPMPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPMPayment.Click
        Try
            If Trim(txtPayMode.Text) = "TRADE-IN" And Val(txtLossPer.Text) = "0.00" Then
                MsgBox("Enter Loss Percent", vbInformation)
                txtLossPer.Focus()
                Exit Sub
            Else
                dLossPer = Val(txtLossPer.Text)
            End If
            'lblNetDiscount.Text = Format(Val(lblNetDiscount.Text) + Val(txtPMDiscount.Text), "0.00")
            lblNetDiscount.Text = Format(Val(lblNetDiscount.Text), "0.00")
            If Val(txtPMNetTotal.Text) >= 0 Then
                pnlTotal.Visible = False
                If Trim(txtPayMode.Text) = "CASH" Then
                    optCashSales.Checked = True
                ElseIf Trim(txtPayMode.Text) = "TRADE-IN" Then
                    optTradeIn.Checked = True
                ElseIf Trim(txtPayMode.Text) = "INSTALMENT" Then
                    optInstallment.Checked = True
                End If
                subPaymentTypeOk()
            Else
                pnlTotal.Visible = False
                txtInventoryCode.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subShowTradein()
        Try
            txtPayMode.Text = "TRADE-IN"
            lblNetDiscount.Text = Format(Val(lblNetDiscount.Text), "0.00")
            If Val(lblNetValue.Text) <> 0 Then
                If Trim(txtPayMode.Text) = "CASH" Then
                    optCashSales.Checked = True
                ElseIf Trim(txtPayMode.Text) = "TRADE-IN" Then
                    optTradeIn.Checked = True
                ElseIf Trim(txtPayMode.Text) = "INSTALMENT" Then
                    optInstallment.Checked = True
                End If
                txtInventoryCode.Text = ""
                subPaymentTypeOk()
                subAddTradeInToGrid()
            Else
                txtInventoryCode.Text = ""
                txtInventoryCode.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    'Shabeer 20120207
    Private Sub subShowInstallment()
        Try
            txtPayMode.Text = "INSTALMENT"
            lblNetDiscount.Text = Format(Val(lblNetDiscount.Text), "0.00")
            If Val(lblNetValue.Text) <> 0 Then
                If Trim(txtPayMode.Text) = "CASH" Then
                    optCashSales.Checked = True
                ElseIf Trim(txtPayMode.Text) = "TRADE-IN" Then
                    optTradeIn.Checked = True
                ElseIf Trim(txtPayMode.Text) = "INSTALMENT" Then
                    optInstallment.Checked = True
                End If
                txtInventoryCode.Text = ""
                subPaymentTypeOk()
            Else
                txtInventoryCode.Text = ""
                txtInventoryCode.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    'Shabeer 20120207
    Private Sub txtPMNetTotal_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPMNetTotal.LostFocus
        Try
            If Val(txtPMNetTotal.Text) <> 0 Then
                txtPMDiscount.Text = Val(txtGrandTotal.Text) - Val(txtPMNetTotal.Text)
            End If
            txtNetDiscount.Text = Val(txtPMDiscount.Text)
            txtPMDiscount_LostFocus(sender, e)
            If mdTotalCommission > 0 Then
                txtPMSurCharge.Text = (mdTotalCommission / 100) * Val(lblNetTotal.Text)
                txtTotalFinal.Text = Val(txtPMNetTotal.Text) + Val(txtPMSurCharge.Text)
                txtPMSurCharge.Focus()
                Exit Sub
            End If
            btnPaymentTypeOK.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtCustomerCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCustomerCode.KeyDown
        If e.KeyCode = Keys.F1 Then btnCustomerCodeSearch_Click(sender, e)
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
                        'ElseIf Trim(dsClass.Tables(0).Rows(i)("ClassCode") & "") = "916" Then
                        '    s916 = Format(Val(dsClass.Tables(0).Rows(i)("Rate") & ""), "#0.00")
                        'ElseIf Trim(dsClass.Tables(0).Rows(i)("ClassCode") & "") = "22K-916" Then
                        '    s916 = Format(Val(dsClass.Tables(0).Rows(i)("Rate") & ""), "#0.00")
                    ElseIf Trim(dsClass.Tables(0).Rows(i)("ClassCode") & "") = "999" Then
                        s999 = Format(Val(dsClass.Tables(0).Rows(i)("Rate") & ""), "#0.00")
                    ElseIf Trim(dsClass.Tables(0).Rows(i)("ClassCode") & "") = "SILVER" Then
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

    Private Sub btnNewCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewCustomer.Click
        IsCalledFrom = "POS"
        If Trim(sCompanyShortCode) = "AJ" Then
            Dim frmCust As New frmOutletCustomers
            frmCust.ShowDialog()
            frmCust = Nothing
        ElseIf Trim(sCompanyShortCode) = "MJ" Then
            Dim frmCust As New frmCustomer
            frmCust.ShowDialog()
            frmCust = Nothing
        Else
            Dim frmCust As New frmOutletCustomersMJ
            frmCust.ShowDialog()
            frmCust = Nothing
        End If
   
        txtCustomerCode.Text = Trim(sOutCustomerCode)
        txtCustomerName.Text = Trim(sOutCustomerName)
    End Sub

    Private Sub subGetClassSalesType(ByVal sclassCode)
        Dim dsCls As New DataSet
        Try
            dsCls = objClass.GetClassDetail(sclassCode)
            If dsCls.Tables(0).Rows.Count > 0 Then
                txtCostPerGram.Text = Val(dsCls.Tables(0).Rows(0)("Rate") & "")
                msClsSalesType = Trim$(dsCls.Tables(0).Rows(0)("SalesType") & "")
            Else
                MessageBox.Show("Class Not defined for this item .... ")
                Exit Sub
            End If
            dsCls = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subGetSubcription(ByVal sSubsriptionId As String)
        Dim dsCls As New DataSet
        Dim sFlexiLocation As String
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
                txtFlexiAmount.Text = Val(dFlexiFinalAmount)
                txtFlexiBonus.Text = Val(dFlexiFinalBonus)
            Else
                MessageBox.Show("Class Not defined for this item .....")
                Exit Sub
            End If
            dsCls = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        pnlMember.Visible = False
        btnCustomerCodeSearch.Enabled = True
        txtCustomerCode.Focus()
    End Sub


    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        sMemberCode = ""
        Dim frmMem As New frmMember
        frmMem.ShowDialog()
        txtMemberCodeView.Text = sMemberCode.Trim
        btnOK.Focus()
        frmMem = Nothing
    End Sub


    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
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
                    txtInventoryCode.Focus()
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
                    'txtMemberName.Text = ""
                    'txtNricNo.Text = ""
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

    Private Function fncUpdateInvoiceNo() As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = objSales.UpdateNextInvoice()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateInvoiceNo = bStatus
        End Try
    End Function

    Private Function fncUpdateTradeInRefNo() As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = objSales.fncUpdateTradeInRefNo(sInvoiceNo, msTradeInRefNo)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateTradeInRefNo = bStatus
        End Try
    End Function

    Private Function fncUpdateInstallmentRefNo() As Boolean
        Dim bStatus As Boolean
        Try
            'bStatus = objSales.fncUpdateInstallmentRefNo(sInvoiceNo, msInstallmentRefNo)
            bStatus = objSales.fncUpdateInstallmentRefNo(sInvoiceNo, gsInsAgreementNo)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateInstallmentRefNo = bStatus
        End Try
    End Function
    Private Function CalcMinimumPriceForML() As Double

        Dim dsMR As New DataSet
        Dim dTotalWeight As Double
        Dim sClassCode As String
        Dim dRate As Double
        Dim dMCDiscount As Double
        Dim dMarketRate As Double
        Dim dTempTotalMarketPrice As Double
        Dim dSMC As Double
        Dim dPMC As Double
        Dim iCnt As Long
        Dim sSalesDate As String
        Try
            sSalesDate = Trim(txtIDD.Text) + "/" + Trim(txtIMM.Text) + "/" + Trim(txtIYYYY.Text)
            CalcMinimumPriceForML = 0
            dFindTotalMarketPrice = 0
            dTempTotalMarketPrice = 0
            dDailyClassRate = 0
            For iCnt = 0 To grdSales.RowCount - 1
                dSMC = Val(fnGetdgvText(grdSales, iCnt, ISMC))
                dPMC = Val(fnGetdgvText(grdSales, iCnt, IPMC))
                sClassCode = Trim(fnGetdgvText(grdSales, iCnt, IClass))
                dTotalWeight = Val(fnGetdgvText(grdSales, iCnt, IWeight)) + Val(fnGetdgvText(grdSales, iCnt, IGoldLoss))

                dsMR = objSales.GetMarketRate(sClassCode, sSalesDate)
                subGetClassSalesType(sClassCode)
                If dsMR.Tables(0).Rows.Count = 0 Then
                    MessageBox.Show("Market Rate Not Found for this Class. Please set market rate!")
                    Exit Function
                Else
                    dRate = Val(dsMR.Tables(0).Rows(0)("Rate") & "")
                End If



                If msClsSalesType <> "P" Then
                    dMarketRate = ((dTotalWeight * dRate) + 1)
                Else
                    dMarketRate = dPMC
                End If
                dDailyClassRate = dRate
                If msClsSalesType <> "P" Then
                    dTempTotalMarketPrice = dMarketRate
                Else
                    dTempTotalMarketPrice = dMarketRate
                End If

                dTempTotalMarketPrice = dTempTotalMarketPrice + (dTempTotalMarketPrice * dGSTPerc / 100)
                CalcMinimumPriceForML = CalcMinimumPriceForML + dTempTotalMarketPrice
                dFindTotalMarketPrice = dFindTotalMarketPrice + dTempTotalMarketPrice
            Next iCnt
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            msClsSalesType = ""
            dsMR = Nothing
        End Try
    End Function

    Private Sub txtWeight_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWeight.Leave
        Call txtPCS_Leave(sender, e)
    End Sub

    Private Sub txtPCS_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPCS.Leave
        Dim dsinv As New DataSet
        Try
            dMarketCost = 20
            If Trim$(txtInventoryCode.Text) = "" Then Exit Sub
            '            If UCase(sCompanyShortCode) <> "HN" Then
            If Val(txtPCS.Text) < 1 Then
                txtPCS.Text = ""
                MessageBox.Show("Invalid Pieces!")
                txtPCS.Focus()
                Exit Sub
            End If
            '   End If

            If Trim(txtInventoryCode.Text) <> "" Then
                If msClsSalesType = "P" Or msClsSalesType = "B" Then
                    dsinv = objInv.GetInventoryDetail(Trim$(txtInventoryCode.Text))
                    'get the details for the selected inv. code and fill again the value fields according to the
                    'entered pieces.
                    txtPCS.Text = CLng(Val(txtPCS.Text))
                    If dsinv.Tables(0).Rows.Count > 0 Then
                        If UCase(sCompanyShortCode) = "HN" Then
                            If Val(txtPCS.Text) > Val(dsinv.Tables(0).Rows(0)("QtyOnHand") & "") Then
                                MessageBox.Show("The entered Pieces " & Val(txtPCS.Text) & " exceeds the quantity on hand " & Val(dsinv.Tables(0).Rows(0)("QtyOnHand") & ""))
                                txtPCS.Text = ""
                                txtPCS.Focus()
                                Exit Sub
                            End If
                        Else
                            If Val(txtPCS.Text) > Val(dsinv.Tables(0).Rows(0)("PCS") & "") Then
                                MessageBox.Show("The Entered Pieces " & Val(txtPCS.Text) & " Exceeds the quantity on hand " & Val(dsinv.Tables(0).Rows(0)("QtyOnHand") & ""))
                                txtPCS.Text = ""
                                txtPCS.Focus()
                                Exit Sub
                            End If
                        End If
                        ''Weight Check
                        If Val(Trim(txtWeight.Text)) > Val(Trim((dsinv.Tables(0).Rows(0)("Weight") & ""))) Then
                            MessageBox.Show("The Entered Weight " & Val(txtWeight.Text) & " Exceeds the Total Weight " & Val(dsinv.Tables(0).Rows(0)("Weight") & ""))
                            'txtPCS.Text = ""
                            txtWeight.Focus()
                            Exit Sub
                        End If

                        '' HAJ JewelPlace on 23 Jan 2008
                        If Not bEdited Then
                            'If Trim(sCompanyShortCode) = "MD" Then
                            '    txtWeight.Text = Format(Val(dsinv.Tables(0).Rows(0)("Weight") & "") * Val(txtPCS.Text) / Val(dsinv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                            'Else
                            txtWeight.Text = Format(Val(dsinv.Tables(0).Rows(0)("Weight") & ""), "0.00")
                            'End If

                            txtGoldLoss.Text = Format(Val(dsinv.Tables(0).Rows(0)("GoldLoss") & "") * Val(txtPCS.Text) / Val(dsinv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                            txtSMC.Text = Format(Val(dsinv.Tables(0).Rows(0)("sMc") & "") * Val(txtPCS.Text) / Val(dsinv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                            mdPurMC = Val(dsinv.Tables(0).Rows(0)("PMC") & "") * Val(txtPCS.Text) / Val(dsinv.Tables(0).Rows(0)("Pcs") & "")
                            dTotalCost = Val(dsinv.Tables(0).Rows(0)("TotalCost") & "") * Val(txtPCS.Text) / Val(dsinv.Tables(0).Rows(0)("Pcs") & "")
                            txtTotalValue.Text = Format((Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram.Text) + Val(txtSMC.Text), "0.00")
                        End If


                        If chkTradeIn.Checked = True Then
                            txtGST.Text = 0.0
                            txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                            sInvGSTType = "T"
                        End If

                        If sInvGSTType = "" Or sInvGSTType = "Standard Rated Supply" Then
                            If optExclusive.Checked = True Then
                                txtGST.Text = Format$(Val(txtTotalValue.Text) * (dGSTPerc / 100), "#0.00")
                                txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                            ElseIf optInclusive.Checked = True Then
                                txtGST.Text = Format$((Val(txtTotalValue.Text) * dGSTPerc) / (100), "#0.00")
                                'txtGST.Text = Format$((Val(txtTotalValue.Text) * dGSTPerc) / (100 + dGSTPerc), "#0.00")
                                txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                            ElseIf optZeroRated.Checked = True Then
                                txtGST.Text = 0.0
                                txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                            End If
                        ElseIf sInvGSTType = "Exempt Supply" Or sInvGSTType = "No GST" Then
                            txtGST.Text = 0.0
                            txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                        End If
                        
                        '''If optExclusive.Checked = True Then
                        '''    txtGST.Text = Format$(Val(txtTotalValue.Text) * (dGSTPerc / 100), "#0.00")
                        '''    txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                        '''ElseIf optInclusive.Checked = True Then
                        '''    txtGST.Text = Format$((Val(txtTotalValue.Text) * dGSTPerc) / (100), "#0.00")
                        '''    'txtGST.Text = Format$((Val(txtTotalValue.Text) * dGSTPerc) / (100 + dGSTPerc), "#0.00")
                        '''    txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                        '''ElseIf optZeroRated.Checked = True Then
                        '''    txtGST.Text = 0.0
                        '''    txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                        '''End If
                        'txtGST.Text = Format$(Val(txtTotalValue.Text) * (dGSTPerc / 100), "#0.00")
                        'txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                        'dNetTotalMarketPrice = Format((Val(txtWeight) + Val(txtGoldLoss) + dMarketCost) * Val(txtRate) + Val(txtSMC / 2), "0.00")
                    End If
                Else
                    txtPCS.Text = 1
                    txtPCS.Enabled = False
                End If
            Else
                txtInventoryCode.Focus()
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
    Private Sub txtTotalValue_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotalValue.Leave

        'Try
        '    If msClsSalesType = "P" Then
        '        If (Val(txtTotalValue.Text)) < (Val(mdPurMC)) Then
        '            MsgBox("Cost Less than Purchase Cost " & (Val(mdPurMC)) & " Please Enter Super Admin Password to proceed.")
        '            'txtTotalValue.Focus()
        '            subGetAdminPassword()
        '            Exit Sub
        '        ElseIf (Val(txtTotalValue.Text)) < (Val(mdPurMC) + (Val(mdPurMC) * 0.6)) Then
        '            MsgBox("Cost Less than Purchase Cost " & (Val(mdPurMC) + (Val(mdPurMC) * 0.6)) & " Please Enter Admin Password to proceed.")
        '            'txtTotalValue.Focus()
        '            subGetAdminPassword()
        '            Exit Sub
        '        End If

        '    End If

        '        tempTV = Val(txtTotalValue.Text)
        '        If dTotalOrgCost > tempTV Then
        '            Diff = dTotalOrgCost - tempTV
        '            NTotVal = Diff
        '        End If

        '        subAddSales()
        '        txtInventoryCode.Focus()
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try

    End Sub

    Private Sub subShowCASH()
        Try
            txtPayMode.Text = "CASH"
            lblNetDiscount.Text = Format(Val(lblNetDiscount.Text), "0.00")
            If Val(lblNetValue.Text) <> 0 Then
                If Trim(txtPayMode.Text) = "CASH" Then
                    optCashSales.Checked = True
                ElseIf Trim(txtPayMode.Text) = "TRADE-IN" Then
                    optTradeIn.Checked = True
                ElseIf Trim(txtPayMode.Text) = "INSTALMENT" Then
                    optInstallment.Checked = True
                End If
                txtInventoryCode.Text = ""
                subTender()

            Else
                txtInventoryCode.Text = ""
                txtInventoryCode.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub pnlBillCancelAdminPwd_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlBillCancelAdminPwd.Paint

    End Sub

    Private Sub btnBCAdminPwdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBCAdminPwdCancel.Click
        Try
            txtBCAdminPassword.Clear()
            pnlBillCancelAdminPwd.Visible = False
            bPassword = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnBCAdminPwdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBCAdminPwdOK.Click
        Dim dsPWD As New DataSet
        Dim dsIns As New DataSet
        Dim sMsg As String
        Try
            If Trim(txtBCAdminPassword.Text) <> "" Then
                dsPWD = objUM.GetAdminPassword(Trim(txtBCAdminPassword.Text))
                If dsPWD.Tables(0).Rows.Count > 0 Then
                    If Trim(UCase(dsPWD.Tables(0).Rows(0)("UserGroupCode") & "")) = "ADMIN" Then
                        sUserCode = Trim(UCase(dsPWD.Tables(0).Rows(0)("UserCode") & ""))
                        sUserName = Trim(UCase(dsPWD.Tables(0).Rows(0)("UserName") & ""))
                        bBCPassword = True
                        pnlBillCancelAdminPwd.Visible = False
                        'If PnlReprint.Visible = True Then
                        '    Call subPrintBill(Trim(txtRePrintBillNo.Text))
                        '    bReprint = False
                        '    PnlReprint.Visible = False
                        '    subClear()
                        'Else
                        '    Payment()
                        'End If
                        Dim objSalesMan As New clsSalesMan
                        If objSalesMan.SalesManLogSave(sUserCode, sUserName, "Bill Cancel", nAllowSalesReturn) Then
                        Else
                            MessageBox.Show("Error in Log Save")
                        End If
                        subBillCancel()
                    Else
                        'MessageBox.Show("Please Check the Password")
                        MsgBox("SalesMan Has No Rights to do Bill Cancel!", MsgBoxStyle.Critical)
                        txtBCAdminPassword.Text = ""
                        If (txtBCAdminPassword.Enabled And txtBCAdminPassword.Visible) Then txtBCAdminPassword.Focus()
                        bBCPassword = False
                        Exit Sub
                    End If
                Else
                    ' MessageBox.Show("Please Check the Password")
                    MsgBox("SalesMan Has No Rights to do Bill Cancel!", MsgBoxStyle.Critical)
                    txtBCAdminPassword.Text = ""
                    If (txtBCAdminPassword.Enabled And txtBCAdminPassword.Visible) Then txtBCAdminPassword.Focus()
                    bBCPassword = False
                    Exit Sub
                End If
            Else
                MessageBox.Show("Enter the Password")
                txtBCAdminPassword.Focus()
                bBCPassword = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnCostAdminPwdOK_Click(sender As System.Object, e As System.EventArgs) Handles btnCostAdminPwdOK.Click
        Dim dsPWD As New DataSet
        Dim dsIns As New DataSet

        Try
            If Trim(txtCostAdminPassword.Text) <> "" Then
                dsPWD = objSM.GetSalesManwithPassword(txtCostAdminPassword.Text)
                If dsPWD.Tables(0).Rows.Count > 0 Then

                    If Trim(UCase(dsPWD.Tables(0).Rows(0)("SalesManGroup") & "")) = "ADMIN" Or Trim(UCase(dsPWD.Tables(0).Rows(0)("SalesManGroup") & "")) = "SUPER ADMIN" Then
                        If (Val(txtTotalValue.Text)) < (Val(mdPurMC)) And (msClsSalesType = "P") And Trim(sCategorycode) <> "GIFT" Then
                            'MessageBox.Show("Please Check the Password")
                            MsgBox("SalesMan Has No Rights to do Change Cost!", MsgBoxStyle.Critical)
                            txtCostAdminPassword.Text = ""
                            If (txtCostAdminPassword.Enabled And txtCostAdminPassword.Visible) Then txtCostAdminPassword.Focus()
                            bBCPassword = False
                            Exit Sub
                        ElseIf ((Val(txtTotalValue.Text) - (Val(txtTotalValue.Text) * (9 / 109))) / Val(txtWeight.Text)) < (Val(txtCostPerGram.Text) - (Val(txtCostPerGram.Text) * 0.09)) And (msClsSalesType = "B") And Val(txtWeight.Text) > 0 And (UCase(dsPWD.Tables(0).Rows(0)("SalesManGroup") & "")) = "ADMIN" Then
                            'MessageBox.Show("Please Check the Password")
                            MsgBox("SalesMan Has No Rights to do Change Cost!", MsgBoxStyle.Critical)
                            txtCostAdminPassword.Text = ""
                            If (txtCostAdminPassword.Enabled And txtCostAdminPassword.Visible) Then txtCostAdminPassword.Focus()
                            bBCPassword = False
                            Exit Sub
                        ElseIf (Val(txtTotalValue.Text)) < (Val(mdPurMC)) And (msClsSalesType = "B") And Val(txtWeight.Text) = 0 And (UCase(dsPWD.Tables(0).Rows(0)("SalesManGroup") & "")) = "ADMIN" Then
                            'MessageBox.Show("Please Check the Password")
                            MsgBox("SalesMan Has No Rights to do Change Cost!", MsgBoxStyle.Critical)
                            txtCostAdminPassword.Text = ""
                            If (txtCostAdminPassword.Enabled And txtCostAdminPassword.Visible) Then txtCostAdminPassword.Focus()
                            bBCPassword = False
                            Exit Sub
                        ElseIf (Val(txtTotalValue.Text) - (Val(txtTotalValue.Text) * (9 / 109))) < ((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + Val(mdPurMC)) And msClsSalesType = "W" And Val(sSalesType) = 0 And (UCase(dsPWD.Tables(0).Rows(0)("SalesManGroup") & "")) = "ADMIN" Then
                            MsgBox("SalesMan Has No Rights to do Change Cost!", MsgBoxStyle.Critical)
                            txtCostAdminPassword.Text = ""
                            If (txtCostAdminPassword.Enabled And txtCostAdminPassword.Visible) Then txtCostAdminPassword.Focus()
                            bBCPassword = False
                            Exit Sub
                        ElseIf (Val(txtTotalValue.Text) - (Val(txtTotalValue.Text) * (9 / 109))) < (Val(txtSMC.Text)) And msClsSalesType = "W" And Val(sSalesType) = 1 And (UCase(dsPWD.Tables(0).Rows(0)("SalesManGroup") & "")) = "ADMIN" Then
                            MsgBox("SalesMan Has No Rights to do Change Cost!", MsgBoxStyle.Critical)
                            txtCostAdminPassword.Text = ""
                            If (txtCostAdminPassword.Enabled And txtCostAdminPassword.Visible) Then txtCostAdminPassword.Focus()
                            bBCPassword = False
                            Exit Sub
                        Else
                            tempTV = Val(txtTotalValue.Text)
                            If dTotalOrgCost > tempTV Then
                                Diff = dTotalOrgCost - tempTV
                                NTotVal = Diff
                            End If
                            pnlCostAdminPwd.Visible = False
                            sCostChangeSalesman = Trim(UCase(dsPWD.Tables(0).Rows(0)("SalesManName") & ""))
                            subAddSales()
                            txtInventoryCode.Focus()
                        End If
                    ElseIf Trim(UCase(dsPWD.Tables(0).Rows(0)("SalesManGroup") & "")) = "SUPER ADMIN" Then
                        tempTV = Val(txtTotalValue.Text)
                        If dTotalOrgCost > tempTV Then
                            Diff = dTotalOrgCost - tempTV
                            NTotVal = Diff
                        End If
                        pnlCostAdminPwd.Visible = False
                        sCostChangeSalesman = Trim(UCase(dsPWD.Tables(0).Rows(0)("SalesManName") & ""))
                        subAddSales()
                        txtInventoryCode.Focus()
                    Else
                        'MessageBox.Show("Please Check the Password")
                        MsgBox("SalesMan Has No Rights to do Change Cost!", MsgBoxStyle.Critical)
                        txtCostAdminPassword.Text = ""
                        If (txtCostAdminPassword.Enabled And txtCostAdminPassword.Visible) Then txtCostAdminPassword.Focus()
                        bBCPassword = False
                        Exit Sub
                    End If
                Else
                    ' MessageBox.Show("Please Check the Password")
                    MsgBox("SalesMan Has No Rights to do Find Cost!", MsgBoxStyle.Critical)
                    txtCostAdminPassword.Text = ""
                    If (txtCostAdminPassword.Enabled And txtCostAdminPassword.Visible) Then txtCostAdminPassword.Focus()
                    bBCPassword = False
                    Exit Sub
                End If
            Else
                MessageBox.Show("Enter the Password")
                txtCostAdminPassword.Focus()
                bBCPassword = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnCostAdminPwdCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCostAdminPwdCancel.Click
        pnlCostAdminPwd.Visible = False
        txtCostAdminPassword.Focus()
    End Sub

    Private Sub txtTotalValue_LostFocus(sender As Object, e As System.EventArgs) Handles txtTotalValue.LostFocus
        Try
            If Trim(sCompanyShortCode) = "AJ" Then

                If msClsSalesType = "P" Then
                    If Trim(sCategorycode) <> "GIFT" Then
                        If (Val(txtTotalValue.Text)) < (Val(mdPurMC)) Then
                            'MsgBox("Cost Less than Purchase Cost $" & (Val(mdPurMC)) & ". Please Enter Super Admin Password to proceed.")
                            MsgBox("Cost Less than Purchase Cost. Please Enter Super Admin Password to proceed.")
                            'txtTotalValue.Focus()
                            subGetAdminPassword()
                            Exit Sub
                        ElseIf (Val(txtTotalValue.Text)) < (Val(mdPurMC) + (Val(mdPurMC) * 0.6)) Then
                            'MsgBox("Cost Less than Purchase Cost $" & (Val(mdPurMC) + (Val(mdPurMC) * 0.6)) & ". Please Enter Admin Password to proceed.")
                            MsgBox("Cost Less than Purchase Cost. Please Enter Admin Password to proceed.")
                            'txtTotalValue.Focus()
                            subGetAdminPassword()
                            Exit Sub
                        End If
                    End If
                    
                ElseIf msClsSalesType = "B" Then
                    If Val(txtWeight.Text) > 0 Then
                        If ((Val(txtTotalValue.Text) - (Val(txtTotalValue.Text) * (9 / 109))) / Val(txtWeight.Text)) < (Val(txtCostPerGram.Text) - (Val(txtCostPerGram.Text) * 0.09)) Then
                            'MsgBox("Cost Less than Purchase Cost $" & (Val(mdPurMC)) & ". Please Enter Super Admin Password to proceed.")
                            MsgBox("Cost Less than Purchase Cost. Please Enter Super Admin Password to proceed.")
                            'txtTotalValue.Focus()
                            subGetAdminPassword()
                            Exit Sub
                        End If
                    Else
                        If (Val(txtTotalValue.Text)) < (Val(mdPurMC)) Then
                            'MsgBox("Cost Less than Purchase Cost $" & (Val(mdPurMC)) & ". Please Enter Super Admin Password to proceed.")
                            MsgBox("Cost Less than Purchase Cost. Please Enter Super Admin Password to proceed.")
                            'txtTotalValue.Focus()
                            subGetAdminPassword()
                            Exit Sub
                        ElseIf (Val(txtTotalValue.Text)) < (Val(mdPurMC) + (Val(mdPurMC) * 0.6)) Then
                            'MsgBox("Cost Less than Purchase Cost $" & (Val(mdPurMC) + (Val(mdPurMC) * 0.6)) & ". Please Enter Admin Password to proceed.")
                            MsgBox("Cost Less than Purchase Cost. Please Enter Admin Password to proceed.")
                            'txtTotalValue.Focus()
                            subGetAdminPassword()
                            Exit Sub
                        End If
                    End If
                ElseIf Val(txtWeight.Text) >= 10 And Val(dCataloguediscount) > 0 Then
                    If (Val(txtTotalValue.Text) - (Val(txtTotalValue.Text) * (9 / 109))) < ((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + (Val(txtWeight.Text) * Val(dCataloguediscount))) Then
                        'MsgBox("Cost Less than Purchase Cost $" & (Val(mdPurMC) + (Val(mdPurMC) * 0.6)) & ". Please Enter Admin Password to proceed.")
                        MsgBox("Cost Less than Purchase Cost. Please Enter Super Admin Password to proceed.")
                        'txtTotalValue.Focus()
                        subGetAdminPassword()
                        Exit Sub
                    End If
                Else
                    If Trim(sCategorycode) = "CHAIN" Then
                        If Val(sSalesPcs) = 0 Then
                            If (Val(txtTotalValue.Text) - (Val(txtTotalValue.Text) * (9 / 109))) < ((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + Val(mdPurMC)) Then
                                'MsgBox("Cost Less than Purchase Cost $" & ((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + Val(mdPurMC)) + (((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + Val(mdPurMC)) * 0.07) & ". Please Enter Super Admin Password to proceed.")
                                MsgBox("Cost Less than Purchase Cost. Please Enter Super Admin Password to proceed.")
                                'txtTotalValue.Focus()
                                subGetAdminPassword()
                                Exit Sub
                            ElseIf (Val(txtTotalValue.Text) - (Val(txtTotalValue.Text) * (9 / 109))) < ((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + (Val(txtSMC.Text) - (Val(txtSMC.Text) * 0.4))) Then
                                'MsgBox("Cost Less than Purchase Cost $" & ((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + (Val(txtSMC.Text) - (Val(txtSMC.Text) * 0.4))) + (((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + (Val(txtSMC.Text) - (Val(txtSMC.Text) * 0.4))) * 0.07) & ". Please Enter Admin Password to proceed.")
                                MsgBox("Cost Less than Purchase Cost. Please Enter Admin Password to proceed.")
                                'txtTotalValue.Focus()
                                subGetAdminPassword()
                                Exit Sub
                            End If
                        Else
                            If (Val(txtTotalValue.Text) - (Val(txtTotalValue.Text) * (9 / 109))) < ((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + Val(mdPurMC)) Then
                                'MsgBox("Cost Less than Purchase Cost $" & ((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + Val(mdPurMC)) + (((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + Val(mdPurMC)) * 0.07) & ". Please Enter Super Admin Password to proceed.")
                                MsgBox("Cost Less than Purchase Cost. Please Enter Super Admin Password to proceed.")
                                'txtTotalValue.Focus()
                                subGetAdminPassword()
                                Exit Sub
                            ElseIf (Val(txtTotalValue.Text) - (Val(txtTotalValue.Text) * (9 / 109))) < ((Val(txtSMC.Text) - (Val(txtSMC.Text) * 0.4))) Then
                                'MsgBox("Cost Less than Purchase Cost $" & ((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + (Val(txtSMC.Text) - (Val(txtSMC.Text) * 0.4))) + (((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + (Val(txtSMC.Text) - (Val(txtSMC.Text) * 0.4))) * 0.07) & ". Please Enter Admin Password to proceed.")
                                MsgBox("Cost Less than Purchase Cost. Please Enter Admin Password to proceed.")
                                'txtTotalValue.Focus()
                                subGetAdminPassword()
                                Exit Sub
                            End If
                        End If
                    Else
                        If Val(sSalesPcs) = 0 Then
                            If (Val(txtTotalValue.Text) - (Val(txtTotalValue.Text) * (9 / 109))) < ((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + Val(mdPurMC)) Then
                                'MsgBox("Cost Less than Purchase Cost $" & ((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + Val(mdPurMC)) + (((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + Val(mdPurMC)) * 0.07) & ". Please Enter Super Admin Password to proceed.")
                                MsgBox("Cost Less than Purchase Cost. Please Enter Super Admin Password to proceed.")
                                'txtTotalValue.Focus()
                                subGetAdminPassword()
                                Exit Sub
                            ElseIf (Val(txtTotalValue.Text) - (Val(txtTotalValue.Text) * (9 / 109))) < ((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + (Val(txtSMC.Text) - (Val(txtSMC.Text) * 0.4))) Then
                                'MsgBox("Cost Less than Purchase Cost $" & ((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + (Val(txtSMC.Text) - (Val(txtSMC.Text) * 0.4))) + (((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + (Val(txtSMC.Text) - (Val(txtSMC.Text) * 0.4))) * 0.07) & ". Please Enter Admin Password to proceed.")
                                MsgBox("Cost Less than Purchase Cost. Please Enter Admin Password to proceed.")
                                'txtTotalValue.Focus()
                                subGetAdminPassword()
                                Exit Sub
                            End If
                        Else
                            If (Val(txtTotalValue.Text) - (Val(txtTotalValue.Text) * (9 / 109))) < ((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + Val(mdPurMC)) Then
                                'MsgBox("Cost Less than Purchase Cost $" & ((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + Val(mdPurMC)) + (((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + Val(mdPurMC)) * 0.07) & ". Please Enter Super Admin Password to proceed.")
                                MsgBox("Cost Less than Purchase Cost. Please Enter Super Admin Password to proceed.")
                                'txtTotalValue.Focus()
                                subGetAdminPassword()
                                Exit Sub
                            ElseIf (Val(txtTotalValue.Text) - (Val(txtTotalValue.Text) * (9 / 109))) < ((Val(txtSMC.Text) - (Val(txtSMC.Text) * 0.4))) Then
                                'MsgBox("Cost Less than Purchase Cost $" & ((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + (Val(txtSMC.Text) - (Val(txtSMC.Text) * 0.4))) + (((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + (Val(txtSMC.Text) - (Val(txtSMC.Text) * 0.4))) * 0.07) & ". Please Enter Admin Password to proceed.")
                                MsgBox("Cost Less than Purchase Cost. Please Enter Admin Password to proceed.")
                                'txtTotalValue.Focus()
                                subGetAdminPassword()
                                Exit Sub
                            End If
                        End If
                    End If

                End If
            ElseIf Trim(sCompanyShortCode) = "MJ" Then
                If Val(txtGST.Text) <> 0 Then
                    If (Val(txtTotalValue.Text) - (Val(txtTotalValue.Text) * (9 / 109))) < ((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + Val(mdPurMC)) Then
                        'MsgBox("Cost Less than Purchase Cost $" & ((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + Val(mdPurMC)) + (((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + Val(mdPurMC)) * 0.07) & ". Please Enter Super Admin Password to proceed.")
                        MsgBox("Cost Less than Purchase Cost. Please Enter Super Admin Password to proceed.")
                        'txtTotalValue.Focus()
                        subGetAdminPassword()
                        Exit Sub
                        'ElseIf (Val(txtTotalValue.Text) - (Val(txtTotalValue.Text) * (7 / 107))) < ((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + (Val(txtSMC.Text) - (Val(txtSMC.Text) * 0.15))) Then
                        '    'MsgBox("Cost Less than Purchase Cost $" & ((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + (Val(txtSMC.Text) - (Val(txtSMC.Text) * 0.4))) + (((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + (Val(txtSMC.Text) - (Val(txtSMC.Text) * 0.4))) * 0.07) & ". Please Enter Admin Password to proceed.")
                        '    MsgBox("Cost Less than Purchase Cost. Please Enter Admin Password to proceed.")
                        '    'txtTotalValue.Focus()
                        '    subGetAdminPassword()
                        '    Exit Sub
                    End If
                End If
                End If
                tempTV = Val(txtTotalValue.Text)
                If dTotalOrgCost > tempTV Then
                    Diff = dTotalOrgCost - tempTV
                    NTotVal = Diff
                End If

                subAddSales()
                txtInventoryCode.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub grdSales_DoubleClick(sender As Object, e As System.EventArgs) Handles grdSales.DoubleClick
        Try
            If Trim(fnGetdgvText(grdSales, grdSales.CurrentRow.Index, 1)) <> "Total:" Then
                bEdited = True
                txtInventoryCode.Text = fnGetdgvText(grdSales, grdSales.CurrentRow.Index, ICode)
                sClassCode = fnGetdgvText(grdSales, grdSales.CurrentRow.Index, IClass)
                txtInventoryDescription.Text = fnGetdgvText(grdSales, grdSales.CurrentRow.Index, IDescription)
                txtWeight.Text = fnGetdgvText(grdSales, grdSales.CurrentRow.Index, IWeight)
                txtGoldLoss.Text = fnGetdgvText(grdSales, grdSales.CurrentRow.Index, IGoldLoss)
                txtCostPerGram.Text = fnGetdgvText(grdSales, grdSales.CurrentRow.Index, ICostPerGram)
                txtStoneWeight.Text = fnGetdgvText(grdSales, grdSales.CurrentRow.Index, IDSWeight)
                txtSMC.Text = fnGetdgvText(grdSales, grdSales.CurrentRow.Index, ISMC)
                mdPurMC = fnGetdgvText(grdSales, grdSales.CurrentRow.Index, IPMC)
                txtPCS.Text = fnGetdgvText(grdSales, grdSales.CurrentRow.Index, IPCS)
                txtTotalValue.Text = fnGetdgvText(grdSales, grdSales.CurrentRow.Index, ITotalValue)
                'added by sarathy
                TotVal = txtTotalValue.Text.Trim
                dTotalOrgCost = txtTotalValue.Text.Trim + Val(fnGetdgvText(grdSales, grdSales.CurrentRow.Index, IDiscount))
                dTotalCost = Val(fnGetdgvText(grdSales, grdSales.CurrentRow.Index, ITotalCost))
                msClsSalesType = fnGetdgvText(grdSales, grdSales.CurrentRow.Index, ISalesType)
                txtGST.Text = fnGetdgvText(grdSales, grdSales.CurrentRow.Index, IGST)
                txtInventoryCode.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtInventoryCode_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtInventoryCode.TextChanged

    End Sub

    Private Sub chkShowImage_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkShowImage.CheckedChanged
        If chkShowImage.Checked Then
            grpImage.Visible = True
        Else
            grpImage.Visible = False
        End If
    End Sub
    Public Sub AutosizeImage(ByVal ImagePath As String, ByVal picBox As PictureBox, Optional ByVal pSizeMode As PictureBoxSizeMode = PictureBoxSizeMode.CenterImage)
        Try
            picBox.Image = Nothing
            picBox.SizeMode = pSizeMode
            If System.IO.File.Exists(ImagePath) Then
                Dim imgOrg As Bitmap
                Dim imgShow As Bitmap
                Dim g As Graphics
                Dim divideBy, divideByH, divideByW As Double
                imgOrg = DirectCast(Bitmap.FromFile(ImagePath), Bitmap)

                divideByW = imgOrg.Width / picBox.Width
                divideByH = imgOrg.Height / picBox.Height
                If divideByW > 1 Or divideByH > 1 Then
                    If divideByW > divideByH Then
                        divideBy = divideByW
                    Else
                        divideBy = divideByH
                    End If

                    imgShow = New Bitmap(CInt(CDbl(imgOrg.Width) / divideBy), CInt(CDbl(imgOrg.Height) / divideBy))
                    imgShow.SetResolution(imgOrg.HorizontalResolution, imgOrg.VerticalResolution)
                    g = Graphics.FromImage(imgShow)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    g.DrawImage(imgOrg, New Rectangle(0, 0, CInt(CDbl(imgOrg.Width) / divideBy), CInt(CDbl(imgOrg.Height) / divideBy)), 0, 0, imgOrg.Width, imgOrg.Height, GraphicsUnit.Pixel)
                    g.Dispose()
                Else
                    imgShow = New Bitmap(imgOrg.Width, imgOrg.Height)
                    imgShow.SetResolution(imgOrg.HorizontalResolution, imgOrg.VerticalResolution)
                    g = Graphics.FromImage(imgShow)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    g.DrawImage(imgOrg, New Rectangle(0, 0, imgOrg.Width, imgOrg.Height), 0, 0, imgOrg.Width, imgOrg.Height, GraphicsUnit.Pixel)
                    g.Dispose()
                End If
                imgOrg.Dispose()

                picBox.Image = imgShow

            Else
                picBox.Image = Nothing
            End If


        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try


    End Sub
    Private Sub subPrintBillCancel(ByVal sInvoiceNo As String)
        Dim ReportTitle As String
        Dim sSelection As String
        Dim sSalesType$
        Dim sReply As String
        Dim dsSales As New DataSet
        Dim frmrep As New frmReports
        Dim CR As New ReportDocument
        Dim j As Integer
        Dim sPrintMC As String
        Dim sPrintImage As String
        Try
            Cursor = Cursors.WaitCursor
            
            
            sReply = MsgBox("Do you want to print Cancel Bill?", vbYesNoCancel + vbQuestion, "Print Option")
                If sReply = vbYes Then
                    
                       
                If sCompanyShortCode = "AJ" Then
                    strpath = Application.StartupPath & "\Reports\rptBillCancelInvoice_AJ.rpt"
                End If
                CR.Load(strpath)
                ReportLogOnToServer(CR)
                CR.Refresh()
                subGetClassRate()
                frmrep.ReportViewer.ReportSource = CR
                sSelection = "{tblSalesReturnHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                frmrep.ReportViewer.SelectionFormula = sSelection
                CR.DataDefinition.RecordSelectionFormula = sSelection
                If UCase(sCompanyShortCode) = "HN" And bReprint Then
                    CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                End If
                If chkPrintMC.Checked Then
                    sPrintMC = "Y"
                Else
                    sPrintMC = "N"
                End If
                If chkPrintImage.Checked Then
                    sPrintImage = "Y"
                Else
                    sPrintImage = "N"
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
                CR.DataDefinition.FormulaFields("PrintMC").Text = fncFormatSQLText(sPrintMC)
                CR.DataDefinition.FormulaFields("PrintImage").Text = fncFormatSQLText(sPrintImage)
                If bReprint Then
                    CR.PrintToPrinter(1, True, 0, 0)
                Else
                    CR.PrintToPrinter(1, True, 0, 0)
                End If
                frmrep = Nothing
                frmrep = New frmReports

            End If

                If sReply = vbCancel Then
                    Exit Sub
                End If
                If sReply = vbCancel Then
                    Exit Sub
                End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub cmdAddlDescOk_Click(sender As System.Object, e As System.EventArgs) Handles cmdAddlDescOk.Click
        grpImage.Visible = False
    End Sub

    Private Sub btnSubscriptionID_Click(sender As System.Object, e As System.EventArgs) Handles btnSubscriptionID.Click

        If Trim(sCompanyShortCode) = "MJ" Then
            Dim frmHelpCustomer As New frmHelpSearch(0, 0, "tblDeposit", "L", 4, "SalesInvoiceNo", "DepositUsed='N' and LocationCode =" & fncFormatSQLText(gthisLocation), "SalesReturnNo", "SalesReturnNo", 80, "SalesInvoiceNo", "SalesInvoiceNo", 90, "SalesInvoiceDate", "SalesInvoiceDate", 90, "Amount", "Amount", 80)
            frmHelpCustomer.ShowDialog()
            If frmHelpCustomer.Var1.Trim <> "" Then
                'txtCustomerCode.Text = frmHelpCustomer.Var1.Trim
                'txtCustomerName.Text = frmHelpCustomer.Var2.Trim
                '  dAdvDepositAmount = frmHelpCustomer.Var3.Trim
                dAdvDepositAmount = frmHelpCustomer.Var4.Trim
            End If
        Else
            subMultipleSearchClick(CSearchLocation, txtSubscriptionID.Text)
            subGetSubcription(txtSubscriptionID.Text)
        End If
        ' ''Try
        ' ''    Dim sFlexiLocation As String
        ' ''    sFlexiLocation = Trim(gthisLocation)
        ' ''    If Trim(sFlexiLocation) = "03" Then
        ' ''        sFlexiLocation = "103"
        ' ''    ElseIf Trim(sLocationCode) = "27" Then
        ' ''        sFlexiLocation = "1"
        ' ''    End If
        ' ''    Dim frmHelpCustomer As New frmHelpSearch(0, 0, "vwFlexiFinalClosure", "R", 4, "SubscriptionId", "isProcessed='N' and LocationCode =" & fncFormatSQLText(sFlexiLocation), "SubscriptionId", "SubscriptionId", 80, "CustomerName", "CustomerName", 90, "TotalAmount", "TotalAmount", 90, "TotalBonus", "TotalBonus", 80)
        ' ''    frmHelpCustomer.ShowDialog()
        ' ''    If frmHelpCustomer.Var1.Trim <> "" Then
        ' ''        txtSubscriptionID.Text = frmHelpCustomer.Var1.Trim
        ' ''        txtCustomerName.Text = frmHelpCustomer.Var2.Trim
        ' ''        dFlexiFinalBonus = frmHelpCustomer.Var3.Trim
        ' ''        dFlexiFinalAmount = frmHelpCustomer.Var4.Trim
        ' ''    End If
        ' ''Catch ex As Exception
        ' ''    MessageBox.Show(ex.Message)
        ' ''End Try
       

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
    End Sub

    Private Sub btnAdvDeposit_Click(sender As System.Object, e As System.EventArgs) Handles btnAdvDeposit.Click
        Dim frmHelpCustomer As New frmHelpSearch(0, 0, "tblCustomerDeposit", "L", 4, "CustomerCode", "DepositUsed='N' and LocationCode =" & fncFormatSQLText(gthisLocation), "CustomerCode", "CustomerCode", 80, "CustomerName", "CustomerName", 90, "DepositNo", "DepositNo", 90, "Amount", "Amount", 80)
        frmHelpCustomer.ShowDialog()
        If frmHelpCustomer.Var1.Trim <> "" Then
            txtCustomerCode.Text = frmHelpCustomer.Var1.Trim
            txtCustomerName.Text = frmHelpCustomer.Var2.Trim
            dAdvDepositAmount = frmHelpCustomer.Var3.Trim
            txtAdvDeposit.Text = frmHelpCustomer.Var4.Trim
        End If
    End Sub

    Private Sub opteTrsTicketIssued_CheckedChanged(sender As Object, e As System.EventArgs) Handles opteTrsTicketIssued.CheckedChanged
        If opteTrsTicketIssued.Checked = True Then
            txtRemarks.Text = Trim(txtCustomerCode.Text)
        Else
            txtRemarks.Text = ""
        End If

    End Sub

    Private Sub txtTotalValue_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTotalValue.TextChanged

    End Sub

    Private Sub txtSalesManPassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSalesManPassword.TextChanged

    End Sub
End Class

