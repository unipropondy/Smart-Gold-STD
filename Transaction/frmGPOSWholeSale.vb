Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmGPOSWholeSale
    Inherits System.Windows.Forms.Form
    Dim dm As New DataManager
    Public dWeightGrd, dGoldLossGrd, dStoneWeightGrd, dMCGrd, dPCSGrd, dTotalCostGrd, dTotalValueGrd As Double
    Friend WithEvents cItemCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cClassCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cDesc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cGoldLoss As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCostpergram As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cDSwt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSMC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cPCS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cTotalValue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cTotalCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSalesType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cPMC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grdTradeIN As System.Windows.Forms.DataGridView
    Friend WithEvents cItemNoTI As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cDescriptionTI As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cWeightTI As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cGoldLossTI As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCostTI As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cPMCTI As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cPCSTI As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cTotalCostTI As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cClassTI As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grdPaymode As System.Windows.Forms.DataGridView
    Friend WithEvents cPaymode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSlipNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pnlCostAdminPwd As System.Windows.Forms.Panel
    Friend WithEvents btnCostAdminPwdCancel As System.Windows.Forms.Button
    Friend WithEvents btnCostAdminPwdOK As System.Windows.Forms.Button
    Friend WithEvents txtCostAdminPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtPassFind As System.Windows.Forms.Label
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents chkExport As System.Windows.Forms.CheckBox
    Friend WithEvents cGSt As System.Windows.Forms.DataGridViewTextBoxColumn

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
    Friend WithEvents pnlAdminPwd As System.Windows.Forms.Panel
    Friend WithEvents btnAdminPwdCancel As System.Windows.Forms.Button
    Friend WithEvents btnAdminPwdOK As System.Windows.Forms.Button
    Friend WithEvents txtAdminPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents PnlReprint As System.Windows.Forms.Panel
    Friend WithEvents btnReprintCancel As System.Windows.Forms.Button
    Friend WithEvents btnReprintOK As System.Windows.Forms.Button
    Friend WithEvents txtRePrintBillNo As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtSlipNo8 As System.Windows.Forms.TextBox
    Friend WithEvents txtSlipNo7 As System.Windows.Forms.TextBox
    Friend WithEvents txtSlipNo6 As System.Windows.Forms.TextBox
    Friend WithEvents txtSlipNo5 As System.Windows.Forms.TextBox
    Friend WithEvents txtSlipNo4 As System.Windows.Forms.TextBox
    Friend WithEvents txtSlipNo3 As System.Windows.Forms.TextBox
    Friend WithEvents txtSlipNo2 As System.Windows.Forms.TextBox
    Friend WithEvents txtAmount6 As System.Windows.Forms.TextBox
    Friend WithEvents txtAmount8 As System.Windows.Forms.TextBox
    Friend WithEvents txtAmount7 As System.Windows.Forms.TextBox
    Friend WithEvents txtSlipNo1 As System.Windows.Forms.TextBox
    Friend WithEvents txtSlipNo0 As System.Windows.Forms.TextBox
    Friend WithEvents txtAmount5 As System.Windows.Forms.TextBox
    Friend WithEvents txtAmount4 As System.Windows.Forms.TextBox
    Friend WithEvents txtAmount3 As System.Windows.Forms.TextBox
    Friend WithEvents txtAmount2 As System.Windows.Forms.TextBox
    Friend WithEvents pnlTradeIn As System.Windows.Forms.Panel
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents txtTradeInCost As System.Windows.Forms.TextBox
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents txtTradeInGLPerc As System.Windows.Forms.TextBox
    Friend WithEvents btntradeInClose As System.Windows.Forms.Button
    Friend WithEvents btnTradeInUpdate As System.Windows.Forms.Button
    Friend WithEvents btnTradeinFClear As System.Windows.Forms.Button
    Friend WithEvents btntradeInClear As System.Windows.Forms.Button
    Friend WithEvents btnTradeInDelete As System.Windows.Forms.Button
    Friend WithEvents btnTradeInAdd As System.Windows.Forms.Button
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents opt24k As System.Windows.Forms.RadioButton
    Friend WithEvents opt22K As System.Windows.Forms.RadioButton
    Friend WithEvents txtTradeInTotalCost As System.Windows.Forms.TextBox
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents txtTradeinCostPerGram As System.Windows.Forms.TextBox
    Friend WithEvents txtTradeInGoldLoss As System.Windows.Forms.TextBox
    Friend WithEvents txtTradeInWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtTradeinDesc As System.Windows.Forms.TextBox
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents lblPaymode8 As System.Windows.Forms.Label
    Friend WithEvents lblPaymode7 As System.Windows.Forms.Label
    Friend WithEvents txtAmount1 As System.Windows.Forms.TextBox
    Friend WithEvents txtAmount0 As System.Windows.Forms.TextBox
    Friend WithEvents lblPaymode6 As System.Windows.Forms.Label
    Friend WithEvents lblPaymode5 As System.Windows.Forms.Label
    Friend WithEvents lblPaymode4 As System.Windows.Forms.Label
    Friend WithEvents lblPaymode3 As System.Windows.Forms.Label
    Friend WithEvents lblPaymode2 As System.Windows.Forms.Label
    Friend WithEvents lblPaymode1 As System.Windows.Forms.Label
    Friend WithEvents lblPaymode0 As System.Windows.Forms.Label
    Friend WithEvents pnlFindCost As System.Windows.Forms.Panel
    Friend WithEvents txtTael As System.Windows.Forms.TextBox
    Friend WithEvents Label78 As System.Windows.Forms.Label
    Friend WithEvents txtFindCostPerGram As System.Windows.Forms.TextBox
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents txtFindTotalCost As System.Windows.Forms.TextBox
    Friend WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents txtSupplierCode As System.Windows.Forms.TextBox
    Friend WithEvents Label75 As System.Windows.Forms.Label
    Friend WithEvents txtFindMC As System.Windows.Forms.TextBox
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents txtFindPurchaseDate As System.Windows.Forms.TextBox
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents txtFindPBillNo As System.Windows.Forms.TextBox
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents txtFindWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents txtFindDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents btnFindCostOK As System.Windows.Forms.Button
    Friend WithEvents txtFindInventory As System.Windows.Forms.TextBox
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents btnPayClear As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlBillCancel As System.Windows.Forms.Panel
    Friend WithEvents btnBillCancel As System.Windows.Forms.Button
    Friend WithEvents btnBillCancelOK As System.Windows.Forms.Button
    Friend WithEvents txtBillCancelBillNo As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents pnlBillCancelAdminPwd As System.Windows.Forms.Panel
    Friend WithEvents btnBCAdminPwdCancel As System.Windows.Forms.Button
    Friend WithEvents btnBCAdminPwdOK As System.Windows.Forms.Button
    Friend WithEvents txtBCAdminPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txtTotalValue As System.Windows.Forms.TextBox
    Friend WithEvents txtGST As System.Windows.Forms.TextBox
    Friend WithEvents txtSMC As System.Windows.Forms.TextBox
    Friend WithEvents txtPCS As System.Windows.Forms.TextBox
    Friend WithEvents txtCostPerGram As System.Windows.Forms.TextBox
    Friend WithEvents txtGoldLoss As System.Windows.Forms.TextBox
    Friend WithEvents txtWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtInventoryDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtInventoryCode As System.Windows.Forms.TextBox
    Friend WithEvents txtStoneWeight As System.Windows.Forms.Label
    Friend WithEvents lblTerminal As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents optInstallment As System.Windows.Forms.RadioButton
    Friend WithEvents optTradeIn As System.Windows.Forms.RadioButton
    Friend WithEvents optNormalSales As System.Windows.Forms.RadioButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents txtReturnChange As System.Windows.Forms.Label
    Friend WithEvents txtCommission As System.Windows.Forms.Label
    Friend WithEvents txtBalanceAmount As System.Windows.Forms.Label
    Friend WithEvents txtPaidAmount As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents txtTotalAmount As System.Windows.Forms.Label
    Friend WithEvents lblReturnAmount As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents lblSManTotal As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents lblNextInvoiceNo As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents lblLastBillAmount As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents lblLastBillNo As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents lblLastSalesman As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents lblLocation As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnCustomerCodeSearch As System.Windows.Forms.Button
    Friend WithEvents btnNewCustomer As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress3 As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress2 As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress1 As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerCode As System.Windows.Forms.TextBox
    Friend WithEvents txtSalesManName As System.Windows.Forms.TextBox
    Friend WithEvents txtSalesManPassword As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents optExclusive As System.Windows.Forms.RadioButton
    Friend WithEvents optInclusive As System.Windows.Forms.RadioButton
    Friend WithEvents optZeroRated As System.Windows.Forms.RadioButton
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lblPMC As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents pnlPMCType As System.Windows.Forms.Panel
    Friend WithEvents optPMCTypeValue As System.Windows.Forms.RadioButton
    Friend WithEvents optPMCTypePerc As System.Windows.Forms.RadioButton
    Friend WithEvents lblQty As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents lblSalesAmount As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents lblNetDiscount As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents lblNetValue As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents lblTax As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents txtNetAmount As System.Windows.Forms.TextBox
    Friend WithEvents lblNetTotal As System.Windows.Forms.Label
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents grdMarketRate As System.Windows.Forms.DataGridView
    Friend WithEvents cClass As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cRate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grdSales As System.Windows.Forms.DataGridView
    Friend WithEvents txtTealGST As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGPOSWholeSale))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlAdminPwd = New System.Windows.Forms.Panel()
        Me.btnAdminPwdCancel = New System.Windows.Forms.Button()
        Me.btnAdminPwdOK = New System.Windows.Forms.Button()
        Me.txtAdminPassword = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.PnlReprint = New System.Windows.Forms.Panel()
        Me.btnReprintCancel = New System.Windows.Forms.Button()
        Me.btnReprintOK = New System.Windows.Forms.Button()
        Me.txtRePrintBillNo = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtSlipNo8 = New System.Windows.Forms.TextBox()
        Me.txtSlipNo7 = New System.Windows.Forms.TextBox()
        Me.txtSlipNo6 = New System.Windows.Forms.TextBox()
        Me.txtSlipNo5 = New System.Windows.Forms.TextBox()
        Me.txtSlipNo4 = New System.Windows.Forms.TextBox()
        Me.txtSlipNo3 = New System.Windows.Forms.TextBox()
        Me.txtSlipNo2 = New System.Windows.Forms.TextBox()
        Me.txtAmount6 = New System.Windows.Forms.TextBox()
        Me.txtAmount8 = New System.Windows.Forms.TextBox()
        Me.txtAmount7 = New System.Windows.Forms.TextBox()
        Me.txtSlipNo1 = New System.Windows.Forms.TextBox()
        Me.txtSlipNo0 = New System.Windows.Forms.TextBox()
        Me.txtAmount5 = New System.Windows.Forms.TextBox()
        Me.txtAmount4 = New System.Windows.Forms.TextBox()
        Me.txtAmount3 = New System.Windows.Forms.TextBox()
        Me.txtAmount2 = New System.Windows.Forms.TextBox()
        Me.pnlTradeIn = New System.Windows.Forms.Panel()
        Me.grdTradeIN = New System.Windows.Forms.DataGridView()
        Me.cItemNoTI = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cDescriptionTI = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cWeightTI = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cGoldLossTI = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cCostTI = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cPMCTI = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cPCSTI = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cTotalCostTI = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cClassTI = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.txtTradeInCost = New System.Windows.Forms.TextBox()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.txtTradeInGLPerc = New System.Windows.Forms.TextBox()
        Me.btntradeInClose = New System.Windows.Forms.Button()
        Me.btnTradeInUpdate = New System.Windows.Forms.Button()
        Me.btnTradeinFClear = New System.Windows.Forms.Button()
        Me.btntradeInClear = New System.Windows.Forms.Button()
        Me.btnTradeInDelete = New System.Windows.Forms.Button()
        Me.btnTradeInAdd = New System.Windows.Forms.Button()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.opt24k = New System.Windows.Forms.RadioButton()
        Me.opt22K = New System.Windows.Forms.RadioButton()
        Me.txtTradeInTotalCost = New System.Windows.Forms.TextBox()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.txtTradeinCostPerGram = New System.Windows.Forms.TextBox()
        Me.txtTradeInGoldLoss = New System.Windows.Forms.TextBox()
        Me.txtTradeInWeight = New System.Windows.Forms.TextBox()
        Me.txtTradeinDesc = New System.Windows.Forms.TextBox()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.lblPaymode8 = New System.Windows.Forms.Label()
        Me.lblPaymode7 = New System.Windows.Forms.Label()
        Me.txtAmount1 = New System.Windows.Forms.TextBox()
        Me.txtAmount0 = New System.Windows.Forms.TextBox()
        Me.lblPaymode6 = New System.Windows.Forms.Label()
        Me.lblPaymode5 = New System.Windows.Forms.Label()
        Me.lblPaymode4 = New System.Windows.Forms.Label()
        Me.lblPaymode3 = New System.Windows.Forms.Label()
        Me.lblPaymode2 = New System.Windows.Forms.Label()
        Me.lblPaymode1 = New System.Windows.Forms.Label()
        Me.lblPaymode0 = New System.Windows.Forms.Label()
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
        Me.btnPayClear = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlBillCancel = New System.Windows.Forms.Panel()
        Me.btnBillCancel = New System.Windows.Forms.Button()
        Me.btnBillCancelOK = New System.Windows.Forms.Button()
        Me.txtBillCancelBillNo = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.pnlBillCancelAdminPwd = New System.Windows.Forms.Panel()
        Me.btnBCAdminPwdCancel = New System.Windows.Forms.Button()
        Me.btnBCAdminPwdOK = New System.Windows.Forms.Button()
        Me.txtBCAdminPassword = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.txtTotalValue = New System.Windows.Forms.TextBox()
        Me.txtGST = New System.Windows.Forms.TextBox()
        Me.txtSMC = New System.Windows.Forms.TextBox()
        Me.txtPCS = New System.Windows.Forms.TextBox()
        Me.txtCostPerGram = New System.Windows.Forms.TextBox()
        Me.txtGoldLoss = New System.Windows.Forms.TextBox()
        Me.txtWeight = New System.Windows.Forms.TextBox()
        Me.txtInventoryDescription = New System.Windows.Forms.TextBox()
        Me.txtInventoryCode = New System.Windows.Forms.TextBox()
        Me.txtStoneWeight = New System.Windows.Forms.Label()
        Me.lblTerminal = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.optInstallment = New System.Windows.Forms.RadioButton()
        Me.optTradeIn = New System.Windows.Forms.RadioButton()
        Me.optNormalSales = New System.Windows.Forms.RadioButton()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.txtReturnChange = New System.Windows.Forms.Label()
        Me.txtCommission = New System.Windows.Forms.Label()
        Me.txtBalanceAmount = New System.Windows.Forms.Label()
        Me.txtPaidAmount = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.txtTotalAmount = New System.Windows.Forms.Label()
        Me.lblReturnAmount = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.lblSManTotal = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.lblNextInvoiceNo = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.lblLastBillAmount = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.lblLastBillNo = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.lblLastSalesman = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.lblLocation = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.pnlPMCType = New System.Windows.Forms.Panel()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.optPMCTypeValue = New System.Windows.Forms.RadioButton()
        Me.optPMCTypePerc = New System.Windows.Forms.RadioButton()
        Me.lblPMC = New System.Windows.Forms.Label()
        Me.txtTealGST = New System.Windows.Forms.TextBox()
        Me.btnCustomerCodeSearch = New System.Windows.Forms.Button()
        Me.btnNewCustomer = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtAddress3 = New System.Windows.Forms.TextBox()
        Me.txtAddress2 = New System.Windows.Forms.TextBox()
        Me.txtAddress1 = New System.Windows.Forms.TextBox()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.txtCustomerCode = New System.Windows.Forms.TextBox()
        Me.txtSalesManName = New System.Windows.Forms.TextBox()
        Me.txtSalesManPassword = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.optExclusive = New System.Windows.Forms.RadioButton()
        Me.optInclusive = New System.Windows.Forms.RadioButton()
        Me.optZeroRated = New System.Windows.Forms.RadioButton()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblQty = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.lblSalesAmount = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.lblNetDiscount = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.lblNetValue = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.lblTax = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.txtNetAmount = New System.Windows.Forms.TextBox()
        Me.lblNetTotal = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.grdMarketRate = New System.Windows.Forms.DataGridView()
        Me.cClass = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cRate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grdSales = New System.Windows.Forms.DataGridView()
        Me.cItemCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cClassCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cDesc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cGoldLoss = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cCostpergram = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cDSwt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cSMC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cPCS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cTotalValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cTotalCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cSalesType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cPMC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cGSt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grdPaymode = New System.Windows.Forms.DataGridView()
        Me.cPaymode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cSlipNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pnlCostAdminPwd = New System.Windows.Forms.Panel()
        Me.btnCostAdminPwdCancel = New System.Windows.Forms.Button()
        Me.btnCostAdminPwdOK = New System.Windows.Forms.Button()
        Me.txtCostAdminPassword = New System.Windows.Forms.TextBox()
        Me.txtPassFind = New System.Windows.Forms.Label()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.chkExport = New System.Windows.Forms.CheckBox()
        Me.pnlAdminPwd.SuspendLayout()
        Me.PnlReprint.SuspendLayout()
        Me.pnlTradeIn.SuspendLayout()
        CType(Me.grdTradeIN, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        Me.pnlFindCost.SuspendLayout()
        Me.pnlBillCancel.SuspendLayout()
        Me.pnlBillCancelAdminPwd.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlPMCType.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.grdMarketRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSales, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdPaymode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCostAdminPwd.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlAdminPwd
        '
        Me.pnlAdminPwd.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlAdminPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlAdminPwd.Controls.Add(Me.btnAdminPwdCancel)
        Me.pnlAdminPwd.Controls.Add(Me.btnAdminPwdOK)
        Me.pnlAdminPwd.Controls.Add(Me.txtAdminPassword)
        Me.pnlAdminPwd.Controls.Add(Me.Label25)
        Me.pnlAdminPwd.Controls.Add(Me.Label27)
        Me.pnlAdminPwd.Location = New System.Drawing.Point(440, 279)
        Me.pnlAdminPwd.Name = "pnlAdminPwd"
        Me.pnlAdminPwd.Size = New System.Drawing.Size(246, 88)
        Me.pnlAdminPwd.TabIndex = 310
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
        'Label25
        '
        Me.Label25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.Black
        Me.Label25.Location = New System.Drawing.Point(16, 33)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(64, 15)
        Me.Label25.TabIndex = 72
        Me.Label25.Text = "Password"
        '
        'Label27
        '
        Me.Label27.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.Color.Black
        Me.Label27.Location = New System.Drawing.Point(8, 8)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(56, 15)
        Me.Label27.TabIndex = 72
        Me.Label27.Text = "Admin"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PnlReprint
        '
        Me.PnlReprint.BackColor = System.Drawing.Color.WhiteSmoke
        Me.PnlReprint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PnlReprint.Controls.Add(Me.btnReprintCancel)
        Me.PnlReprint.Controls.Add(Me.btnReprintOK)
        Me.PnlReprint.Controls.Add(Me.txtRePrintBillNo)
        Me.PnlReprint.Controls.Add(Me.Label21)
        Me.PnlReprint.Controls.Add(Me.Label23)
        Me.PnlReprint.Location = New System.Drawing.Point(439, 283)
        Me.PnlReprint.Name = "PnlReprint"
        Me.PnlReprint.Size = New System.Drawing.Size(248, 88)
        Me.PnlReprint.TabIndex = 309
        Me.PnlReprint.Visible = False
        '
        'btnReprintCancel
        '
        Me.btnReprintCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
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
        Me.btnReprintOK.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
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
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Black
        Me.Label21.Location = New System.Drawing.Point(32, 33)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(56, 15)
        Me.Label21.TabIndex = 74
        Me.Label21.Text = "Bill No"
        '
        'Label23
        '
        Me.Label23.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.Black
        Me.Label23.Location = New System.Drawing.Point(8, 8)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(82, 15)
        Me.Label23.TabIndex = 72
        Me.Label23.Text = "RePrint"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSlipNo8
        '
        Me.txtSlipNo8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNo8.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipNo8.ForeColor = System.Drawing.Color.Black
        Me.txtSlipNo8.Location = New System.Drawing.Point(1014, 325)
        Me.txtSlipNo8.MaxLength = 20
        Me.txtSlipNo8.Name = "txtSlipNo8"
        Me.txtSlipNo8.Size = New System.Drawing.Size(86, 30)
        Me.txtSlipNo8.TabIndex = 308
        Me.txtSlipNo8.Tag = ""
        Me.txtSlipNo8.Visible = False
        '
        'txtSlipNo7
        '
        Me.txtSlipNo7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNo7.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipNo7.ForeColor = System.Drawing.Color.Black
        Me.txtSlipNo7.Location = New System.Drawing.Point(1014, 292)
        Me.txtSlipNo7.MaxLength = 20
        Me.txtSlipNo7.Name = "txtSlipNo7"
        Me.txtSlipNo7.Size = New System.Drawing.Size(86, 30)
        Me.txtSlipNo7.TabIndex = 307
        Me.txtSlipNo7.Tag = ""
        Me.txtSlipNo7.Visible = False
        '
        'txtSlipNo6
        '
        Me.txtSlipNo6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNo6.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipNo6.ForeColor = System.Drawing.Color.Black
        Me.txtSlipNo6.Location = New System.Drawing.Point(1014, 259)
        Me.txtSlipNo6.MaxLength = 20
        Me.txtSlipNo6.Name = "txtSlipNo6"
        Me.txtSlipNo6.Size = New System.Drawing.Size(86, 30)
        Me.txtSlipNo6.TabIndex = 306
        Me.txtSlipNo6.Tag = ""
        Me.txtSlipNo6.Visible = False
        '
        'txtSlipNo5
        '
        Me.txtSlipNo5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNo5.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipNo5.ForeColor = System.Drawing.Color.Black
        Me.txtSlipNo5.Location = New System.Drawing.Point(1014, 227)
        Me.txtSlipNo5.MaxLength = 20
        Me.txtSlipNo5.Name = "txtSlipNo5"
        Me.txtSlipNo5.Size = New System.Drawing.Size(86, 30)
        Me.txtSlipNo5.TabIndex = 305
        Me.txtSlipNo5.Tag = ""
        Me.txtSlipNo5.Visible = False
        '
        'txtSlipNo4
        '
        Me.txtSlipNo4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNo4.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipNo4.ForeColor = System.Drawing.Color.Black
        Me.txtSlipNo4.Location = New System.Drawing.Point(1014, 196)
        Me.txtSlipNo4.MaxLength = 20
        Me.txtSlipNo4.Name = "txtSlipNo4"
        Me.txtSlipNo4.Size = New System.Drawing.Size(86, 30)
        Me.txtSlipNo4.TabIndex = 304
        Me.txtSlipNo4.Tag = ""
        Me.txtSlipNo4.Visible = False
        '
        'txtSlipNo3
        '
        Me.txtSlipNo3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNo3.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipNo3.ForeColor = System.Drawing.Color.Black
        Me.txtSlipNo3.Location = New System.Drawing.Point(1014, 165)
        Me.txtSlipNo3.MaxLength = 20
        Me.txtSlipNo3.Name = "txtSlipNo3"
        Me.txtSlipNo3.Size = New System.Drawing.Size(86, 30)
        Me.txtSlipNo3.TabIndex = 303
        Me.txtSlipNo3.Tag = ""
        Me.txtSlipNo3.Visible = False
        '
        'txtSlipNo2
        '
        Me.txtSlipNo2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNo2.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipNo2.ForeColor = System.Drawing.Color.Black
        Me.txtSlipNo2.Location = New System.Drawing.Point(1014, 134)
        Me.txtSlipNo2.MaxLength = 20
        Me.txtSlipNo2.Name = "txtSlipNo2"
        Me.txtSlipNo2.Size = New System.Drawing.Size(86, 30)
        Me.txtSlipNo2.TabIndex = 302
        Me.txtSlipNo2.Tag = ""
        Me.txtSlipNo2.Visible = False
        '
        'txtAmount6
        '
        Me.txtAmount6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmount6.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount6.ForeColor = System.Drawing.Color.Black
        Me.txtAmount6.Location = New System.Drawing.Point(899, 259)
        Me.txtAmount6.MaxLength = 20
        Me.txtAmount6.Name = "txtAmount6"
        Me.txtAmount6.Size = New System.Drawing.Size(112, 30)
        Me.txtAmount6.TabIndex = 297
        Me.txtAmount6.Tag = ""
        Me.txtAmount6.Visible = False
        '
        'txtAmount8
        '
        Me.txtAmount8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmount8.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount8.ForeColor = System.Drawing.Color.Black
        Me.txtAmount8.Location = New System.Drawing.Point(899, 325)
        Me.txtAmount8.MaxLength = 20
        Me.txtAmount8.Name = "txtAmount8"
        Me.txtAmount8.Size = New System.Drawing.Size(112, 30)
        Me.txtAmount8.TabIndex = 299
        Me.txtAmount8.Tag = ""
        Me.txtAmount8.Visible = False
        '
        'txtAmount7
        '
        Me.txtAmount7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmount7.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount7.ForeColor = System.Drawing.Color.Black
        Me.txtAmount7.Location = New System.Drawing.Point(899, 292)
        Me.txtAmount7.MaxLength = 20
        Me.txtAmount7.Name = "txtAmount7"
        Me.txtAmount7.Size = New System.Drawing.Size(112, 30)
        Me.txtAmount7.TabIndex = 298
        Me.txtAmount7.Tag = ""
        Me.txtAmount7.Visible = False
        '
        'txtSlipNo1
        '
        Me.txtSlipNo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNo1.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipNo1.ForeColor = System.Drawing.Color.Black
        Me.txtSlipNo1.Location = New System.Drawing.Point(1014, 104)
        Me.txtSlipNo1.MaxLength = 20
        Me.txtSlipNo1.Name = "txtSlipNo1"
        Me.txtSlipNo1.Size = New System.Drawing.Size(86, 30)
        Me.txtSlipNo1.TabIndex = 301
        Me.txtSlipNo1.Tag = ""
        Me.txtSlipNo1.Visible = False
        '
        'txtSlipNo0
        '
        Me.txtSlipNo0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNo0.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipNo0.ForeColor = System.Drawing.Color.Black
        Me.txtSlipNo0.Location = New System.Drawing.Point(1014, 77)
        Me.txtSlipNo0.MaxLength = 20
        Me.txtSlipNo0.Name = "txtSlipNo0"
        Me.txtSlipNo0.Size = New System.Drawing.Size(86, 30)
        Me.txtSlipNo0.TabIndex = 300
        Me.txtSlipNo0.Tag = ""
        Me.txtSlipNo0.Visible = False
        '
        'txtAmount5
        '
        Me.txtAmount5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmount5.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount5.ForeColor = System.Drawing.Color.Black
        Me.txtAmount5.Location = New System.Drawing.Point(899, 227)
        Me.txtAmount5.MaxLength = 20
        Me.txtAmount5.Name = "txtAmount5"
        Me.txtAmount5.Size = New System.Drawing.Size(112, 30)
        Me.txtAmount5.TabIndex = 296
        Me.txtAmount5.Tag = ""
        Me.txtAmount5.Visible = False
        '
        'txtAmount4
        '
        Me.txtAmount4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmount4.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount4.ForeColor = System.Drawing.Color.Black
        Me.txtAmount4.Location = New System.Drawing.Point(899, 196)
        Me.txtAmount4.MaxLength = 20
        Me.txtAmount4.Name = "txtAmount4"
        Me.txtAmount4.Size = New System.Drawing.Size(112, 30)
        Me.txtAmount4.TabIndex = 295
        Me.txtAmount4.Tag = ""
        Me.txtAmount4.Visible = False
        '
        'txtAmount3
        '
        Me.txtAmount3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmount3.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount3.ForeColor = System.Drawing.Color.Black
        Me.txtAmount3.Location = New System.Drawing.Point(899, 165)
        Me.txtAmount3.MaxLength = 20
        Me.txtAmount3.Name = "txtAmount3"
        Me.txtAmount3.Size = New System.Drawing.Size(112, 30)
        Me.txtAmount3.TabIndex = 294
        Me.txtAmount3.Tag = ""
        Me.txtAmount3.Visible = False
        '
        'txtAmount2
        '
        Me.txtAmount2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmount2.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount2.ForeColor = System.Drawing.Color.Black
        Me.txtAmount2.Location = New System.Drawing.Point(899, 134)
        Me.txtAmount2.MaxLength = 20
        Me.txtAmount2.Name = "txtAmount2"
        Me.txtAmount2.Size = New System.Drawing.Size(112, 30)
        Me.txtAmount2.TabIndex = 293
        Me.txtAmount2.Tag = ""
        Me.txtAmount2.Visible = False
        '
        'pnlTradeIn
        '
        Me.pnlTradeIn.BackColor = System.Drawing.Color.LightCoral
        Me.pnlTradeIn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTradeIn.Controls.Add(Me.grdTradeIN)
        Me.pnlTradeIn.Controls.Add(Me.Label41)
        Me.pnlTradeIn.Controls.Add(Me.txtTradeInCost)
        Me.pnlTradeIn.Controls.Add(Me.Label60)
        Me.pnlTradeIn.Controls.Add(Me.txtTradeInGLPerc)
        Me.pnlTradeIn.Controls.Add(Me.btntradeInClose)
        Me.pnlTradeIn.Controls.Add(Me.btnTradeInUpdate)
        Me.pnlTradeIn.Controls.Add(Me.btnTradeinFClear)
        Me.pnlTradeIn.Controls.Add(Me.btntradeInClear)
        Me.pnlTradeIn.Controls.Add(Me.btnTradeInDelete)
        Me.pnlTradeIn.Controls.Add(Me.btnTradeInAdd)
        Me.pnlTradeIn.Controls.Add(Me.Panel5)
        Me.pnlTradeIn.Controls.Add(Me.txtTradeInTotalCost)
        Me.pnlTradeIn.Controls.Add(Me.Label58)
        Me.pnlTradeIn.Controls.Add(Me.txtTradeinCostPerGram)
        Me.pnlTradeIn.Controls.Add(Me.txtTradeInGoldLoss)
        Me.pnlTradeIn.Controls.Add(Me.txtTradeInWeight)
        Me.pnlTradeIn.Controls.Add(Me.txtTradeinDesc)
        Me.pnlTradeIn.Controls.Add(Me.Label54)
        Me.pnlTradeIn.Controls.Add(Me.Label55)
        Me.pnlTradeIn.Controls.Add(Me.Label56)
        Me.pnlTradeIn.Controls.Add(Me.Label57)
        Me.pnlTradeIn.Location = New System.Drawing.Point(71, 301)
        Me.pnlTradeIn.Name = "pnlTradeIn"
        Me.pnlTradeIn.Size = New System.Drawing.Size(648, 312)
        Me.pnlTradeIn.TabIndex = 281
        Me.pnlTradeIn.Visible = False
        '
        'grdTradeIN
        '
        Me.grdTradeIN.AllowUserToAddRows = False
        Me.grdTradeIN.AllowUserToDeleteRows = False
        Me.grdTradeIN.AllowUserToResizeColumns = False
        Me.grdTradeIN.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdTradeIN.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdTradeIN.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cItemNoTI, Me.cDescriptionTI, Me.cWeightTI, Me.cGoldLossTI, Me.cCostTI, Me.cPMCTI, Me.cPCSTI, Me.cTotalCostTI, Me.cClassTI})
        Me.grdTradeIN.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdTradeIN.Location = New System.Drawing.Point(5, 42)
        Me.grdTradeIN.Name = "grdTradeIN"
        Me.grdTradeIN.RowHeadersVisible = False
        Me.grdTradeIN.Size = New System.Drawing.Size(636, 151)
        Me.grdTradeIN.TabIndex = 187
        '
        'cItemNoTI
        '
        Me.cItemNoTI.HeaderText = "ItemNO"
        Me.cItemNoTI.Name = "cItemNoTI"
        Me.cItemNoTI.Visible = False
        '
        'cDescriptionTI
        '
        Me.cDescriptionTI.HeaderText = "Description"
        Me.cDescriptionTI.Name = "cDescriptionTI"
        Me.cDescriptionTI.Width = 200
        '
        'cWeightTI
        '
        Me.cWeightTI.HeaderText = "Weight"
        Me.cWeightTI.Name = "cWeightTI"
        Me.cWeightTI.Width = 80
        '
        'cGoldLossTI
        '
        Me.cGoldLossTI.HeaderText = "Gold Loss"
        Me.cGoldLossTI.Name = "cGoldLossTI"
        '
        'cCostTI
        '
        Me.cCostTI.HeaderText = "Cost Per Gram"
        Me.cCostTI.Name = "cCostTI"
        Me.cCostTI.Width = 140
        '
        'cPMCTI
        '
        Me.cPMCTI.HeaderText = "PMC"
        Me.cPMCTI.Name = "cPMCTI"
        Me.cPMCTI.Visible = False
        '
        'cPCSTI
        '
        Me.cPCSTI.HeaderText = "PCS"
        Me.cPCSTI.Name = "cPCSTI"
        Me.cPCSTI.Visible = False
        '
        'cTotalCostTI
        '
        Me.cTotalCostTI.HeaderText = "Total Cost"
        Me.cTotalCostTI.Name = "cTotalCostTI"
        '
        'cClassTI
        '
        Me.cClassTI.HeaderText = "Class"
        Me.cClassTI.Name = "cClassTI"
        Me.cClassTI.Visible = False
        '
        'Label41
        '
        Me.Label41.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.Location = New System.Drawing.Point(224, 16)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(96, 16)
        Me.Label41.TabIndex = 186
        Me.Label41.Text = "Cost Per Gram"
        '
        'txtTradeInCost
        '
        Me.txtTradeInCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTradeInCost.Enabled = False
        Me.txtTradeInCost.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold)
        Me.txtTradeInCost.Location = New System.Drawing.Point(336, 8)
        Me.txtTradeInCost.Name = "txtTradeInCost"
        Me.txtTradeInCost.Size = New System.Drawing.Size(104, 22)
        Me.txtTradeInCost.TabIndex = 185
        '
        'Label60
        '
        Me.Label60.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label60.Location = New System.Drawing.Point(440, 16)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(80, 16)
        Me.Label60.TabIndex = 184
        Me.Label60.Text = "Gold Loss %"
        '
        'txtTradeInGLPerc
        '
        Me.txtTradeInGLPerc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTradeInGLPerc.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold)
        Me.txtTradeInGLPerc.Location = New System.Drawing.Point(536, 8)
        Me.txtTradeInGLPerc.Name = "txtTradeInGLPerc"
        Me.txtTradeInGLPerc.Size = New System.Drawing.Size(104, 22)
        Me.txtTradeInGLPerc.TabIndex = 132
        '
        'btntradeInClose
        '
        Me.btntradeInClose.BackColor = System.Drawing.Color.Orange
        Me.btntradeInClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntradeInClose.Location = New System.Drawing.Point(552, 282)
        Me.btntradeInClose.Name = "btntradeInClose"
        Me.btntradeInClose.Size = New System.Drawing.Size(88, 24)
        Me.btntradeInClose.TabIndex = 181
        Me.btntradeInClose.Text = "Close"
        Me.btntradeInClose.UseVisualStyleBackColor = False
        '
        'btnTradeInUpdate
        '
        Me.btnTradeInUpdate.BackColor = System.Drawing.Color.Orange
        Me.btnTradeInUpdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTradeInUpdate.Location = New System.Drawing.Point(462, 282)
        Me.btnTradeInUpdate.Name = "btnTradeInUpdate"
        Me.btnTradeInUpdate.Size = New System.Drawing.Size(88, 24)
        Me.btnTradeInUpdate.TabIndex = 180
        Me.btnTradeInUpdate.Text = "Update - (F9)"
        Me.btnTradeInUpdate.UseVisualStyleBackColor = False
        '
        'btnTradeinFClear
        '
        Me.btnTradeinFClear.BackColor = System.Drawing.Color.Orange
        Me.btnTradeinFClear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTradeinFClear.Location = New System.Drawing.Point(368, 282)
        Me.btnTradeinFClear.Name = "btnTradeinFClear"
        Me.btnTradeinFClear.Size = New System.Drawing.Size(88, 24)
        Me.btnTradeinFClear.TabIndex = 179
        Me.btnTradeinFClear.Text = "Clear"
        Me.btnTradeinFClear.UseVisualStyleBackColor = False
        '
        'btntradeInClear
        '
        Me.btntradeInClear.BackColor = System.Drawing.Color.Orange
        Me.btntradeInClear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntradeInClear.Location = New System.Drawing.Point(576, 256)
        Me.btntradeInClear.Name = "btntradeInClear"
        Me.btntradeInClear.Size = New System.Drawing.Size(64, 24)
        Me.btntradeInClear.TabIndex = 178
        Me.btntradeInClear.Text = "Clear"
        Me.btntradeInClear.UseVisualStyleBackColor = False
        '
        'btnTradeInDelete
        '
        Me.btnTradeInDelete.BackColor = System.Drawing.Color.Orange
        Me.btnTradeInDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTradeInDelete.Location = New System.Drawing.Point(504, 256)
        Me.btnTradeInDelete.Name = "btnTradeInDelete"
        Me.btnTradeInDelete.Size = New System.Drawing.Size(64, 24)
        Me.btnTradeInDelete.TabIndex = 177
        Me.btnTradeInDelete.Text = "Delete"
        Me.btnTradeInDelete.UseVisualStyleBackColor = False
        '
        'btnTradeInAdd
        '
        Me.btnTradeInAdd.BackColor = System.Drawing.Color.Orange
        Me.btnTradeInAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTradeInAdd.Location = New System.Drawing.Point(432, 256)
        Me.btnTradeInAdd.Name = "btnTradeInAdd"
        Me.btnTradeInAdd.Size = New System.Drawing.Size(64, 24)
        Me.btnTradeInAdd.TabIndex = 142
        Me.btnTradeInAdd.Text = "Add"
        Me.btnTradeInAdd.UseVisualStyleBackColor = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.LightCoral
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.Label59)
        Me.Panel5.Controls.Add(Me.opt24k)
        Me.Panel5.Controls.Add(Me.opt22K)
        Me.Panel5.Location = New System.Drawing.Point(8, 256)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(168, 40)
        Me.Panel5.TabIndex = 175
        '
        'Label59
        '
        Me.Label59.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label59.Location = New System.Drawing.Point(8, 16)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(40, 16)
        Me.Label59.TabIndex = 123
        Me.Label59.Text = "Class"
        '
        'opt24k
        '
        Me.opt24k.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.opt24k.Location = New System.Drawing.Point(112, 16)
        Me.opt24k.Name = "opt24k"
        Me.opt24k.Size = New System.Drawing.Size(48, 16)
        Me.opt24k.TabIndex = 122
        Me.opt24k.Text = "24K"
        '
        'opt22K
        '
        Me.opt22K.Checked = True
        Me.opt22K.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.opt22K.Location = New System.Drawing.Point(56, 16)
        Me.opt22K.Name = "opt22K"
        Me.opt22K.Size = New System.Drawing.Size(48, 16)
        Me.opt22K.TabIndex = 121
        Me.opt22K.TabStop = True
        Me.opt22K.Text = "22K"
        '
        'txtTradeInTotalCost
        '
        Me.txtTradeInTotalCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTradeInTotalCost.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold)
        Me.txtTradeInTotalCost.Location = New System.Drawing.Point(516, 224)
        Me.txtTradeInTotalCost.Name = "txtTradeInTotalCost"
        Me.txtTradeInTotalCost.Size = New System.Drawing.Size(124, 22)
        Me.txtTradeInTotalCost.TabIndex = 141
        '
        'Label58
        '
        Me.Label58.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label58.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label58.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label58.Location = New System.Drawing.Point(516, 200)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(124, 24)
        Me.Label58.TabIndex = 140
        Me.Label58.Text = "Total Value"
        Me.Label58.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTradeinCostPerGram
        '
        Me.txtTradeinCostPerGram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTradeinCostPerGram.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold)
        Me.txtTradeinCostPerGram.Location = New System.Drawing.Point(412, 224)
        Me.txtTradeinCostPerGram.Name = "txtTradeinCostPerGram"
        Me.txtTradeinCostPerGram.Size = New System.Drawing.Size(104, 22)
        Me.txtTradeinCostPerGram.TabIndex = 139
        '
        'txtTradeInGoldLoss
        '
        Me.txtTradeInGoldLoss.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTradeInGoldLoss.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold)
        Me.txtTradeInGoldLoss.Location = New System.Drawing.Point(308, 224)
        Me.txtTradeInGoldLoss.Name = "txtTradeInGoldLoss"
        Me.txtTradeInGoldLoss.Size = New System.Drawing.Size(104, 22)
        Me.txtTradeInGoldLoss.TabIndex = 137
        '
        'txtTradeInWeight
        '
        Me.txtTradeInWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTradeInWeight.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold)
        Me.txtTradeInWeight.Location = New System.Drawing.Point(204, 224)
        Me.txtTradeInWeight.Name = "txtTradeInWeight"
        Me.txtTradeInWeight.Size = New System.Drawing.Size(104, 22)
        Me.txtTradeInWeight.TabIndex = 135
        '
        'txtTradeinDesc
        '
        Me.txtTradeinDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTradeinDesc.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold)
        Me.txtTradeinDesc.Location = New System.Drawing.Point(4, 224)
        Me.txtTradeinDesc.Name = "txtTradeinDesc"
        Me.txtTradeinDesc.Size = New System.Drawing.Size(200, 22)
        Me.txtTradeinDesc.TabIndex = 133
        '
        'Label54
        '
        Me.Label54.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label54.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label54.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label54.Location = New System.Drawing.Point(412, 200)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(104, 24)
        Me.Label54.TabIndex = 138
        Me.Label54.Text = "Cost / Gram"
        Me.Label54.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label55
        '
        Me.Label55.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label55.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label55.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label55.Location = New System.Drawing.Point(308, 200)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(104, 24)
        Me.Label55.TabIndex = 136
        Me.Label55.Text = "Gold Loss"
        Me.Label55.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label56
        '
        Me.Label56.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label56.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label56.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label56.Location = New System.Drawing.Point(204, 200)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(104, 24)
        Me.Label56.TabIndex = 134
        Me.Label56.Text = "Weight"
        Me.Label56.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label57
        '
        Me.Label57.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label57.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label57.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label57.Location = New System.Drawing.Point(4, 200)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(200, 24)
        Me.Label57.TabIndex = 132
        Me.Label57.Text = "Inventory Description"
        Me.Label57.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPaymode8
        '
        Me.lblPaymode8.BackColor = System.Drawing.Color.White
        Me.lblPaymode8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPaymode8.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymode8.ForeColor = System.Drawing.Color.Black
        Me.lblPaymode8.Location = New System.Drawing.Point(747, 325)
        Me.lblPaymode8.Name = "lblPaymode8"
        Me.lblPaymode8.Size = New System.Drawing.Size(144, 24)
        Me.lblPaymode8.TabIndex = 290
        Me.lblPaymode8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblPaymode8.Visible = False
        '
        'lblPaymode7
        '
        Me.lblPaymode7.BackColor = System.Drawing.Color.White
        Me.lblPaymode7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPaymode7.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymode7.ForeColor = System.Drawing.Color.Black
        Me.lblPaymode7.Location = New System.Drawing.Point(747, 292)
        Me.lblPaymode7.Name = "lblPaymode7"
        Me.lblPaymode7.Size = New System.Drawing.Size(144, 24)
        Me.lblPaymode7.TabIndex = 289
        Me.lblPaymode7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblPaymode7.Visible = False
        '
        'txtAmount1
        '
        Me.txtAmount1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmount1.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount1.ForeColor = System.Drawing.Color.Black
        Me.txtAmount1.Location = New System.Drawing.Point(899, 104)
        Me.txtAmount1.MaxLength = 20
        Me.txtAmount1.Name = "txtAmount1"
        Me.txtAmount1.Size = New System.Drawing.Size(112, 30)
        Me.txtAmount1.TabIndex = 292
        Me.txtAmount1.Tag = ""
        Me.txtAmount1.Visible = False
        '
        'txtAmount0
        '
        Me.txtAmount0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmount0.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount0.ForeColor = System.Drawing.Color.Black
        Me.txtAmount0.Location = New System.Drawing.Point(899, 77)
        Me.txtAmount0.MaxLength = 20
        Me.txtAmount0.Name = "txtAmount0"
        Me.txtAmount0.Size = New System.Drawing.Size(112, 30)
        Me.txtAmount0.TabIndex = 291
        Me.txtAmount0.Tag = ""
        Me.txtAmount0.Visible = False
        '
        'lblPaymode6
        '
        Me.lblPaymode6.BackColor = System.Drawing.Color.White
        Me.lblPaymode6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPaymode6.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymode6.ForeColor = System.Drawing.Color.Black
        Me.lblPaymode6.Location = New System.Drawing.Point(747, 259)
        Me.lblPaymode6.Name = "lblPaymode6"
        Me.lblPaymode6.Size = New System.Drawing.Size(144, 24)
        Me.lblPaymode6.TabIndex = 288
        Me.lblPaymode6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblPaymode6.Visible = False
        '
        'lblPaymode5
        '
        Me.lblPaymode5.BackColor = System.Drawing.Color.White
        Me.lblPaymode5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPaymode5.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymode5.ForeColor = System.Drawing.Color.Black
        Me.lblPaymode5.Location = New System.Drawing.Point(747, 227)
        Me.lblPaymode5.Name = "lblPaymode5"
        Me.lblPaymode5.Size = New System.Drawing.Size(144, 24)
        Me.lblPaymode5.TabIndex = 287
        Me.lblPaymode5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblPaymode5.Visible = False
        '
        'lblPaymode4
        '
        Me.lblPaymode4.BackColor = System.Drawing.Color.White
        Me.lblPaymode4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPaymode4.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymode4.ForeColor = System.Drawing.Color.Black
        Me.lblPaymode4.Location = New System.Drawing.Point(747, 196)
        Me.lblPaymode4.Name = "lblPaymode4"
        Me.lblPaymode4.Size = New System.Drawing.Size(144, 24)
        Me.lblPaymode4.TabIndex = 286
        Me.lblPaymode4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblPaymode4.Visible = False
        '
        'lblPaymode3
        '
        Me.lblPaymode3.BackColor = System.Drawing.Color.White
        Me.lblPaymode3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPaymode3.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymode3.ForeColor = System.Drawing.Color.Black
        Me.lblPaymode3.Location = New System.Drawing.Point(747, 165)
        Me.lblPaymode3.Name = "lblPaymode3"
        Me.lblPaymode3.Size = New System.Drawing.Size(144, 24)
        Me.lblPaymode3.TabIndex = 285
        Me.lblPaymode3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblPaymode3.Visible = False
        '
        'lblPaymode2
        '
        Me.lblPaymode2.BackColor = System.Drawing.Color.White
        Me.lblPaymode2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPaymode2.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymode2.ForeColor = System.Drawing.Color.Black
        Me.lblPaymode2.Location = New System.Drawing.Point(747, 134)
        Me.lblPaymode2.Name = "lblPaymode2"
        Me.lblPaymode2.Size = New System.Drawing.Size(144, 24)
        Me.lblPaymode2.TabIndex = 284
        Me.lblPaymode2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblPaymode2.Visible = False
        '
        'lblPaymode1
        '
        Me.lblPaymode1.BackColor = System.Drawing.Color.White
        Me.lblPaymode1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPaymode1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymode1.ForeColor = System.Drawing.Color.Black
        Me.lblPaymode1.Location = New System.Drawing.Point(748, 104)
        Me.lblPaymode1.Name = "lblPaymode1"
        Me.lblPaymode1.Size = New System.Drawing.Size(144, 24)
        Me.lblPaymode1.TabIndex = 283
        Me.lblPaymode1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblPaymode1.Visible = False
        '
        'lblPaymode0
        '
        Me.lblPaymode0.BackColor = System.Drawing.Color.White
        Me.lblPaymode0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPaymode0.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymode0.ForeColor = System.Drawing.Color.Black
        Me.lblPaymode0.Location = New System.Drawing.Point(748, 77)
        Me.lblPaymode0.Name = "lblPaymode0"
        Me.lblPaymode0.Size = New System.Drawing.Size(144, 24)
        Me.lblPaymode0.TabIndex = 282
        Me.lblPaymode0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblPaymode0.Visible = False
        '
        'pnlFindCost
        '
        Me.pnlFindCost.BackColor = System.Drawing.Color.White
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
        Me.pnlFindCost.Location = New System.Drawing.Point(264, 287)
        Me.pnlFindCost.Name = "pnlFindCost"
        Me.pnlFindCost.Size = New System.Drawing.Size(456, 229)
        Me.pnlFindCost.TabIndex = 315
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
        'btnPayClear
        '
        Me.btnPayClear.BackColor = System.Drawing.Color.Orange
        Me.btnPayClear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPayClear.Location = New System.Drawing.Point(955, 309)
        Me.btnPayClear.Name = "btnPayClear"
        Me.btnPayClear.Size = New System.Drawing.Size(80, 32)
        Me.btnPayClear.TabIndex = 314
        Me.btnPayClear.Text = "Clear - (F8)"
        Me.btnPayClear.UseVisualStyleBackColor = False
        Me.btnPayClear.Visible = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.Orange
        Me.btnSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(827, 309)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(80, 32)
        Me.btnSave.TabIndex = 313
        Me.btnSave.Text = "Save - (F7)"
        Me.btnSave.UseVisualStyleBackColor = False
        Me.btnSave.Visible = False
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Orange
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(531, 5)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 40)
        Me.Label3.TabIndex = 217
        Me.Label3.Text = "BillCancel - (F3)"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Orange
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(635, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(112, 40)
        Me.Label2.TabIndex = 216
        Me.Label2.Text = "Net Total - (F5)"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Orange
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(219, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 40)
        Me.Label1.TabIndex = 215
        Me.Label1.Text = "Clear  -  (F2)"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlBillCancel
        '
        Me.pnlBillCancel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlBillCancel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlBillCancel.Controls.Add(Me.btnBillCancel)
        Me.pnlBillCancel.Controls.Add(Me.btnBillCancelOK)
        Me.pnlBillCancel.Controls.Add(Me.txtBillCancelBillNo)
        Me.pnlBillCancel.Controls.Add(Me.Label35)
        Me.pnlBillCancel.Controls.Add(Me.Label38)
        Me.pnlBillCancel.Location = New System.Drawing.Point(424, 282)
        Me.pnlBillCancel.Name = "pnlBillCancel"
        Me.pnlBillCancel.Size = New System.Drawing.Size(278, 88)
        Me.pnlBillCancel.TabIndex = 312
        Me.pnlBillCancel.Visible = False
        '
        'btnBillCancel
        '
        Me.btnBillCancel.BackColor = System.Drawing.Color.Orange
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
        Me.btnBillCancelOK.BackColor = System.Drawing.Color.Orange
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
        'Label35
        '
        Me.Label35.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.Color.Black
        Me.Label35.Location = New System.Drawing.Point(56, 34)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(56, 15)
        Me.Label35.TabIndex = 74
        Me.Label35.Text = "Bill No"
        '
        'Label38
        '
        Me.Label38.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.Color.Black
        Me.Label38.Location = New System.Drawing.Point(8, 8)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(72, 15)
        Me.Label38.TabIndex = 72
        Me.Label38.Text = "Bill Cancel"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlBillCancelAdminPwd
        '
        Me.pnlBillCancelAdminPwd.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlBillCancelAdminPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlBillCancelAdminPwd.Controls.Add(Me.btnBCAdminPwdCancel)
        Me.pnlBillCancelAdminPwd.Controls.Add(Me.btnBCAdminPwdOK)
        Me.pnlBillCancelAdminPwd.Controls.Add(Me.txtBCAdminPassword)
        Me.pnlBillCancelAdminPwd.Controls.Add(Me.Label29)
        Me.pnlBillCancelAdminPwd.Controls.Add(Me.Label31)
        Me.pnlBillCancelAdminPwd.Location = New System.Drawing.Point(440, 281)
        Me.pnlBillCancelAdminPwd.Name = "pnlBillCancelAdminPwd"
        Me.pnlBillCancelAdminPwd.Size = New System.Drawing.Size(246, 88)
        Me.pnlBillCancelAdminPwd.TabIndex = 311
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
        'Label29
        '
        Me.Label29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.Color.Black
        Me.Label29.Location = New System.Drawing.Point(16, 33)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(64, 15)
        Me.Label29.TabIndex = 72
        Me.Label29.Text = "Password"
        '
        'Label31
        '
        Me.Label31.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.Color.Black
        Me.Label31.Location = New System.Drawing.Point(8, 8)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(56, 15)
        Me.Label31.TabIndex = 72
        Me.Label31.Text = "Admin"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTotalValue
        '
        Me.txtTotalValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalValue.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalValue.Location = New System.Drawing.Point(987, 581)
        Me.txtTotalValue.Name = "txtTotalValue"
        Me.txtTotalValue.Size = New System.Drawing.Size(128, 21)
        Me.txtTotalValue.TabIndex = 241
        '
        'txtGST
        '
        Me.txtGST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGST.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGST.Location = New System.Drawing.Point(883, 581)
        Me.txtGST.Name = "txtGST"
        Me.txtGST.Size = New System.Drawing.Size(104, 21)
        Me.txtGST.TabIndex = 239
        '
        'txtSMC
        '
        Me.txtSMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSMC.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMC.Location = New System.Drawing.Point(779, 581)
        Me.txtSMC.Name = "txtSMC"
        Me.txtSMC.Size = New System.Drawing.Size(104, 21)
        Me.txtSMC.TabIndex = 237
        '
        'txtPCS
        '
        Me.txtPCS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPCS.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPCS.Location = New System.Drawing.Point(675, 581)
        Me.txtPCS.Name = "txtPCS"
        Me.txtPCS.Size = New System.Drawing.Size(104, 21)
        Me.txtPCS.TabIndex = 235
        '
        'txtCostPerGram
        '
        Me.txtCostPerGram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCostPerGram.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostPerGram.Location = New System.Drawing.Point(571, 581)
        Me.txtCostPerGram.Name = "txtCostPerGram"
        Me.txtCostPerGram.Size = New System.Drawing.Size(104, 21)
        Me.txtCostPerGram.TabIndex = 233
        '
        'txtGoldLoss
        '
        Me.txtGoldLoss.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldLoss.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoldLoss.Location = New System.Drawing.Point(467, 581)
        Me.txtGoldLoss.Name = "txtGoldLoss"
        Me.txtGoldLoss.Size = New System.Drawing.Size(104, 21)
        Me.txtGoldLoss.TabIndex = 231
        '
        'txtWeight
        '
        Me.txtWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWeight.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeight.Location = New System.Drawing.Point(363, 581)
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.Size = New System.Drawing.Size(104, 21)
        Me.txtWeight.TabIndex = 229
        '
        'txtInventoryDescription
        '
        Me.txtInventoryDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInventoryDescription.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInventoryDescription.Location = New System.Drawing.Point(107, 581)
        Me.txtInventoryDescription.Name = "txtInventoryDescription"
        Me.txtInventoryDescription.Size = New System.Drawing.Size(256, 21)
        Me.txtInventoryDescription.TabIndex = 227
        '
        'txtInventoryCode
        '
        Me.txtInventoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInventoryCode.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInventoryCode.Location = New System.Drawing.Point(3, 581)
        Me.txtInventoryCode.Name = "txtInventoryCode"
        Me.txtInventoryCode.Size = New System.Drawing.Size(104, 21)
        Me.txtInventoryCode.TabIndex = 225
        '
        'txtStoneWeight
        '
        Me.txtStoneWeight.BackColor = System.Drawing.Color.Navy
        Me.txtStoneWeight.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStoneWeight.ForeColor = System.Drawing.Color.Black
        Me.txtStoneWeight.Location = New System.Drawing.Point(771, 629)
        Me.txtStoneWeight.Name = "txtStoneWeight"
        Me.txtStoneWeight.Size = New System.Drawing.Size(56, 24)
        Me.txtStoneWeight.TabIndex = 278
        Me.txtStoneWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.txtStoneWeight.Visible = False
        '
        'lblTerminal
        '
        Me.lblTerminal.BackColor = System.Drawing.Color.Navy
        Me.lblTerminal.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTerminal.ForeColor = System.Drawing.Color.White
        Me.lblTerminal.Location = New System.Drawing.Point(123, 629)
        Me.lblTerminal.Name = "lblTerminal"
        Me.lblTerminal.Size = New System.Drawing.Size(56, 24)
        Me.lblTerminal.TabIndex = 277
        Me.lblTerminal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.optInstallment)
        Me.Panel4.Controls.Add(Me.optTradeIn)
        Me.Panel4.Controls.Add(Me.optNormalSales)
        Me.Panel4.Enabled = False
        Me.Panel4.Location = New System.Drawing.Point(843, 5)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(272, 40)
        Me.Panel4.TabIndex = 276
        '
        'optInstallment
        '
        Me.optInstallment.Enabled = False
        Me.optInstallment.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optInstallment.Location = New System.Drawing.Point(168, 8)
        Me.optInstallment.Name = "optInstallment"
        Me.optInstallment.Size = New System.Drawing.Size(80, 24)
        Me.optInstallment.TabIndex = 122
        Me.optInstallment.Text = "Installment"
        '
        'optTradeIn
        '
        Me.optTradeIn.Enabled = False
        Me.optTradeIn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTradeIn.Location = New System.Drawing.Point(88, 8)
        Me.optTradeIn.Name = "optTradeIn"
        Me.optTradeIn.Size = New System.Drawing.Size(72, 24)
        Me.optTradeIn.TabIndex = 121
        Me.optTradeIn.Text = "Trade In"
        '
        'optNormalSales
        '
        Me.optNormalSales.Checked = True
        Me.optNormalSales.Enabled = False
        Me.optNormalSales.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optNormalSales.Location = New System.Drawing.Point(8, 8)
        Me.optNormalSales.Name = "optNormalSales"
        Me.optNormalSales.Size = New System.Drawing.Size(72, 32)
        Me.optNormalSales.TabIndex = 120
        Me.optNormalSales.TabStop = True
        Me.optNormalSales.Text = "Normal Sales"
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.txtReturnChange)
        Me.Panel3.Controls.Add(Me.txtCommission)
        Me.Panel3.Controls.Add(Me.txtBalanceAmount)
        Me.Panel3.Controls.Add(Me.txtPaidAmount)
        Me.Panel3.Controls.Add(Me.Label47)
        Me.Panel3.Controls.Add(Me.Label50)
        Me.Panel3.Controls.Add(Me.Label51)
        Me.Panel3.Controls.Add(Me.Label52)
        Me.Panel3.Controls.Add(Me.Label53)
        Me.Panel3.Controls.Add(Me.txtTotalAmount)
        Me.Panel3.Location = New System.Drawing.Point(747, 357)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(368, 160)
        Me.Panel3.TabIndex = 274
        Me.Panel3.Visible = False
        '
        'txtReturnChange
        '
        Me.txtReturnChange.BackColor = System.Drawing.Color.Navy
        Me.txtReturnChange.Font = New System.Drawing.Font("Arial", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReturnChange.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.txtReturnChange.Location = New System.Drawing.Point(141, 126)
        Me.txtReturnChange.Name = "txtReturnChange"
        Me.txtReturnChange.Size = New System.Drawing.Size(219, 30)
        Me.txtReturnChange.TabIndex = 122
        Me.txtReturnChange.Text = "0.00"
        Me.txtReturnChange.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCommission
        '
        Me.txtCommission.BackColor = System.Drawing.Color.Navy
        Me.txtCommission.Font = New System.Drawing.Font("Arial", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCommission.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.txtCommission.Location = New System.Drawing.Point(141, 95)
        Me.txtCommission.Name = "txtCommission"
        Me.txtCommission.Size = New System.Drawing.Size(219, 30)
        Me.txtCommission.TabIndex = 121
        Me.txtCommission.Text = "0.00"
        Me.txtCommission.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBalanceAmount
        '
        Me.txtBalanceAmount.BackColor = System.Drawing.Color.Navy
        Me.txtBalanceAmount.Font = New System.Drawing.Font("Arial", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBalanceAmount.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.txtBalanceAmount.Location = New System.Drawing.Point(141, 64)
        Me.txtBalanceAmount.Name = "txtBalanceAmount"
        Me.txtBalanceAmount.Size = New System.Drawing.Size(219, 30)
        Me.txtBalanceAmount.TabIndex = 120
        Me.txtBalanceAmount.Text = "0.00"
        Me.txtBalanceAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPaidAmount
        '
        Me.txtPaidAmount.BackColor = System.Drawing.Color.Navy
        Me.txtPaidAmount.Font = New System.Drawing.Font("Arial", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaidAmount.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.txtPaidAmount.Location = New System.Drawing.Point(141, 32)
        Me.txtPaidAmount.Name = "txtPaidAmount"
        Me.txtPaidAmount.Size = New System.Drawing.Size(219, 30)
        Me.txtPaidAmount.TabIndex = 119
        Me.txtPaidAmount.Text = "0.00"
        Me.txtPaidAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label47
        '
        Me.Label47.BackColor = System.Drawing.Color.Yellow
        Me.Label47.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label47.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.ForeColor = System.Drawing.Color.Black
        Me.Label47.Location = New System.Drawing.Point(5, 126)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(133, 30)
        Me.Label47.TabIndex = 117
        Me.Label47.Text = "Return"
        Me.Label47.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label50
        '
        Me.Label50.BackColor = System.Drawing.Color.Yellow
        Me.Label50.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label50.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.ForeColor = System.Drawing.Color.Black
        Me.Label50.Location = New System.Drawing.Point(6, 95)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(132, 30)
        Me.Label50.TabIndex = 116
        Me.Label50.Text = "Commission"
        Me.Label50.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label51
        '
        Me.Label51.BackColor = System.Drawing.Color.Yellow
        Me.Label51.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label51.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.ForeColor = System.Drawing.Color.Black
        Me.Label51.Location = New System.Drawing.Point(6, 64)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(132, 30)
        Me.Label51.TabIndex = 115
        Me.Label51.Text = "Balance"
        Me.Label51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label52
        '
        Me.Label52.BackColor = System.Drawing.Color.Yellow
        Me.Label52.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label52.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.ForeColor = System.Drawing.Color.Black
        Me.Label52.Location = New System.Drawing.Point(5, 33)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(133, 30)
        Me.Label52.TabIndex = 114
        Me.Label52.Text = "Paid"
        Me.Label52.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label53
        '
        Me.Label53.BackColor = System.Drawing.Color.Yellow
        Me.Label53.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label53.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.ForeColor = System.Drawing.Color.Black
        Me.Label53.Location = New System.Drawing.Point(5, 2)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(133, 30)
        Me.Label53.TabIndex = 113
        Me.Label53.Text = "Total"
        Me.Label53.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTotalAmount
        '
        Me.txtTotalAmount.BackColor = System.Drawing.Color.Navy
        Me.txtTotalAmount.Font = New System.Drawing.Font("Arial", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalAmount.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.txtTotalAmount.Location = New System.Drawing.Point(141, 0)
        Me.txtTotalAmount.Name = "txtTotalAmount"
        Me.txtTotalAmount.Size = New System.Drawing.Size(219, 30)
        Me.txtTotalAmount.TabIndex = 118
        Me.txtTotalAmount.Text = "0.00"
        Me.txtTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblReturnAmount
        '
        Me.lblReturnAmount.BackColor = System.Drawing.Color.Navy
        Me.lblReturnAmount.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReturnAmount.ForeColor = System.Drawing.Color.White
        Me.lblReturnAmount.Location = New System.Drawing.Point(947, 629)
        Me.lblReturnAmount.Name = "lblReturnAmount"
        Me.lblReturnAmount.Size = New System.Drawing.Size(168, 24)
        Me.lblReturnAmount.TabIndex = 255
        Me.lblReturnAmount.Text = "0.00"
        Me.lblReturnAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label16
        '
        Me.Label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label16.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label16.Location = New System.Drawing.Point(675, 557)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(104, 24)
        Me.Label16.TabIndex = 234
        Me.Label16.Text = "PCS"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label15
        '
        Me.Label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label15.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label15.Location = New System.Drawing.Point(571, 557)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(104, 24)
        Me.Label15.TabIndex = 232
        Me.Label15.Text = "Cost / Gram"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label18
        '
        Me.Label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label18.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label18.Location = New System.Drawing.Point(883, 557)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(104, 24)
        Me.Label18.TabIndex = 238
        Me.Label18.Text = "GST"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label17
        '
        Me.Label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label17.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label17.Location = New System.Drawing.Point(779, 557)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(104, 24)
        Me.Label17.TabIndex = 236
        Me.Label17.Text = "SMC"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label14
        '
        Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label14.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label14.Location = New System.Drawing.Point(467, 557)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(104, 24)
        Me.Label14.TabIndex = 230
        Me.Label14.Text = "Gold Loss"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.Yellow
        Me.Label32.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label32.Location = New System.Drawing.Point(947, 605)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(168, 24)
        Me.Label32.TabIndex = 254
        Me.Label32.Text = "Return Amount"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSManTotal
        '
        Me.lblSManTotal.BackColor = System.Drawing.Color.Navy
        Me.lblSManTotal.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSManTotal.ForeColor = System.Drawing.Color.White
        Me.lblSManTotal.Location = New System.Drawing.Point(827, 629)
        Me.lblSManTotal.Name = "lblSManTotal"
        Me.lblSManTotal.Size = New System.Drawing.Size(120, 24)
        Me.lblSManTotal.TabIndex = 253
        Me.lblSManTotal.Text = "0.00"
        Me.lblSManTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.Yellow
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label30.Location = New System.Drawing.Point(827, 605)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(120, 24)
        Me.Label30.TabIndex = 252
        Me.Label30.Text = "Total Sales Amount"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblNextInvoiceNo
        '
        Me.lblNextInvoiceNo.BackColor = System.Drawing.Color.Navy
        Me.lblNextInvoiceNo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNextInvoiceNo.ForeColor = System.Drawing.Color.White
        Me.lblNextInvoiceNo.Location = New System.Drawing.Point(643, 629)
        Me.lblNextInvoiceNo.Name = "lblNextInvoiceNo"
        Me.lblNextInvoiceNo.Size = New System.Drawing.Size(184, 24)
        Me.lblNextInvoiceNo.TabIndex = 251
        Me.lblNextInvoiceNo.Text = "0"
        Me.lblNextInvoiceNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.Yellow
        Me.Label28.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label28.Location = New System.Drawing.Point(643, 605)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(184, 24)
        Me.Label28.TabIndex = 250
        Me.Label28.Text = "Next Invoice No"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLastBillAmount
        '
        Me.lblLastBillAmount.BackColor = System.Drawing.Color.Navy
        Me.lblLastBillAmount.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastBillAmount.ForeColor = System.Drawing.Color.White
        Me.lblLastBillAmount.Location = New System.Drawing.Point(467, 629)
        Me.lblLastBillAmount.Name = "lblLastBillAmount"
        Me.lblLastBillAmount.Size = New System.Drawing.Size(176, 24)
        Me.lblLastBillAmount.TabIndex = 249
        Me.lblLastBillAmount.Text = "0"
        Me.lblLastBillAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.Yellow
        Me.Label26.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label26.Location = New System.Drawing.Point(467, 605)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(176, 24)
        Me.Label26.TabIndex = 248
        Me.Label26.Text = "Last Bill Amount"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLastBillNo
        '
        Me.lblLastBillNo.BackColor = System.Drawing.Color.Navy
        Me.lblLastBillNo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastBillNo.ForeColor = System.Drawing.Color.White
        Me.lblLastBillNo.Location = New System.Drawing.Point(339, 629)
        Me.lblLastBillNo.Name = "lblLastBillNo"
        Me.lblLastBillNo.Size = New System.Drawing.Size(128, 24)
        Me.lblLastBillNo.TabIndex = 247
        Me.lblLastBillNo.Text = "0"
        Me.lblLastBillNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.Yellow
        Me.Label24.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label24.Location = New System.Drawing.Point(339, 605)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(128, 24)
        Me.Label24.TabIndex = 246
        Me.Label24.Text = "Last Bill No"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLastSalesman
        '
        Me.lblLastSalesman.BackColor = System.Drawing.Color.Navy
        Me.lblLastSalesman.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastSalesman.ForeColor = System.Drawing.Color.White
        Me.lblLastSalesman.Location = New System.Drawing.Point(147, 629)
        Me.lblLastSalesman.Name = "lblLastSalesman"
        Me.lblLastSalesman.Size = New System.Drawing.Size(200, 24)
        Me.lblLastSalesman.TabIndex = 245
        Me.lblLastSalesman.Text = "0"
        Me.lblLastSalesman.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.Yellow
        Me.Label22.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label22.Location = New System.Drawing.Point(139, 605)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(200, 24)
        Me.Label22.TabIndex = 244
        Me.Label22.Text = "Last Sales Man"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLocation
        '
        Me.lblLocation.BackColor = System.Drawing.Color.Navy
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.ForeColor = System.Drawing.Color.White
        Me.lblLocation.Location = New System.Drawing.Point(3, 629)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(136, 24)
        Me.lblLocation.TabIndex = 243
        Me.lblLocation.Text = "HQ"
        Me.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.Yellow
        Me.Label20.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label20.Location = New System.Drawing.Point(3, 605)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(136, 24)
        Me.Label20.TabIndex = 242
        Me.Label20.Text = "Location"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label19
        '
        Me.Label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label19.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label19.Location = New System.Drawing.Point(987, 557)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(128, 24)
        Me.Label19.TabIndex = 240
        Me.Label19.Text = "Total Value"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.chkExport)
        Me.Panel2.Controls.Add(Me.pnlPMCType)
        Me.Panel2.Controls.Add(Me.lblPMC)
        Me.Panel2.Controls.Add(Me.txtTealGST)
        Me.Panel2.Controls.Add(Me.btnCustomerCodeSearch)
        Me.Panel2.Controls.Add(Me.btnNewCustomer)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.txtRemarks)
        Me.Panel2.Controls.Add(Me.txtAddress3)
        Me.Panel2.Controls.Add(Me.txtAddress2)
        Me.Panel2.Controls.Add(Me.txtAddress1)
        Me.Panel2.Controls.Add(Me.txtCustomerName)
        Me.Panel2.Controls.Add(Me.txtCustomerCode)
        Me.Panel2.Controls.Add(Me.txtSalesManName)
        Me.Panel2.Controls.Add(Me.txtSalesManPassword)
        Me.Panel2.Location = New System.Drawing.Point(219, 45)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(653, 243)
        Me.Panel2.TabIndex = 223
        '
        'pnlPMCType
        '
        Me.pnlPMCType.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnlPMCType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlPMCType.Controls.Add(Me.Label44)
        Me.pnlPMCType.Controls.Add(Me.optPMCTypeValue)
        Me.pnlPMCType.Controls.Add(Me.optPMCTypePerc)
        Me.pnlPMCType.Location = New System.Drawing.Point(256, 208)
        Me.pnlPMCType.Name = "pnlPMCType"
        Me.pnlPMCType.Size = New System.Drawing.Size(192, 24)
        Me.pnlPMCType.TabIndex = 176
        Me.pnlPMCType.Visible = False
        '
        'Label44
        '
        Me.Label44.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(8, 0)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(40, 16)
        Me.Label44.TabIndex = 123
        Me.Label44.Text = "Type"
        '
        'optPMCTypeValue
        '
        Me.optPMCTypeValue.Checked = True
        Me.optPMCTypeValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPMCTypeValue.Location = New System.Drawing.Point(112, 0)
        Me.optPMCTypeValue.Name = "optPMCTypeValue"
        Me.optPMCTypeValue.Size = New System.Drawing.Size(56, 16)
        Me.optPMCTypeValue.TabIndex = 122
        Me.optPMCTypeValue.TabStop = True
        Me.optPMCTypeValue.Text = "Value"
        '
        'optPMCTypePerc
        '
        Me.optPMCTypePerc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPMCTypePerc.Location = New System.Drawing.Point(56, 0)
        Me.optPMCTypePerc.Name = "optPMCTypePerc"
        Me.optPMCTypePerc.Size = New System.Drawing.Size(48, 16)
        Me.optPMCTypePerc.TabIndex = 121
        Me.optPMCTypePerc.Text = "Perc"
        '
        'lblPMC
        '
        Me.lblPMC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPMC.Location = New System.Drawing.Point(8, 208)
        Me.lblPMC.Name = "lblPMC"
        Me.lblPMC.Size = New System.Drawing.Size(72, 16)
        Me.lblPMC.TabIndex = 120
        Me.lblPMC.Text = "Teal GST"
        Me.lblPMC.Visible = False
        '
        'txtTealGST
        '
        Me.txtTealGST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTealGST.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTealGST.Location = New System.Drawing.Point(112, 208)
        Me.txtTealGST.Name = "txtTealGST"
        Me.txtTealGST.Size = New System.Drawing.Size(136, 21)
        Me.txtTealGST.TabIndex = 119
        Me.txtTealGST.Visible = False
        '
        'btnCustomerCodeSearch
        '
        Me.btnCustomerCodeSearch.BackColor = System.Drawing.Color.Orange
        Me.btnCustomerCodeSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomerCodeSearch.Location = New System.Drawing.Point(200, 48)
        Me.btnCustomerCodeSearch.Name = "btnCustomerCodeSearch"
        Me.btnCustomerCodeSearch.Size = New System.Drawing.Size(32, 22)
        Me.btnCustomerCodeSearch.TabIndex = 118
        Me.btnCustomerCodeSearch.Text = "..."
        Me.btnCustomerCodeSearch.UseVisualStyleBackColor = False
        '
        'btnNewCustomer
        '
        Me.btnNewCustomer.BackColor = System.Drawing.Color.Orange
        Me.btnNewCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNewCustomer.Location = New System.Drawing.Point(8, 112)
        Me.btnNewCustomer.Name = "btnNewCustomer"
        Me.btnNewCustomer.Size = New System.Drawing.Size(80, 24)
        Me.btnNewCustomer.TabIndex = 117
        Me.btnNewCustomer.Text = "New"
        Me.btnNewCustomer.UseVisualStyleBackColor = False
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(8, 179)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(72, 16)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "Remarks"
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(8, 80)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(96, 24)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "Address"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(8, 48)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(96, 24)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "Customer "
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(8, 8)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(96, 24)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Sales Man"
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(112, 176)
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(537, 21)
        Me.txtRemarks.TabIndex = 7
        '
        'txtAddress3
        '
        Me.txtAddress3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress3.Location = New System.Drawing.Point(112, 144)
        Me.txtAddress3.Name = "txtAddress3"
        Me.txtAddress3.Size = New System.Drawing.Size(536, 21)
        Me.txtAddress3.TabIndex = 6
        '
        'txtAddress2
        '
        Me.txtAddress2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress2.Location = New System.Drawing.Point(112, 112)
        Me.txtAddress2.Name = "txtAddress2"
        Me.txtAddress2.Size = New System.Drawing.Size(536, 21)
        Me.txtAddress2.TabIndex = 5
        '
        'txtAddress1
        '
        Me.txtAddress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress1.Location = New System.Drawing.Point(112, 80)
        Me.txtAddress1.Name = "txtAddress1"
        Me.txtAddress1.Size = New System.Drawing.Size(536, 21)
        Me.txtAddress1.TabIndex = 4
        '
        'txtCustomerName
        '
        Me.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerName.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerName.Location = New System.Drawing.Point(243, 48)
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(405, 21)
        Me.txtCustomerName.TabIndex = 3
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerCode.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerCode.Location = New System.Drawing.Point(112, 48)
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.Size = New System.Drawing.Size(120, 21)
        Me.txtCustomerCode.TabIndex = 2
        '
        'txtSalesManName
        '
        Me.txtSalesManName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesManName.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesManName.Location = New System.Drawing.Point(243, 8)
        Me.txtSalesManName.Name = "txtSalesManName"
        Me.txtSalesManName.Size = New System.Drawing.Size(405, 21)
        Me.txtSalesManName.TabIndex = 1
        '
        'txtSalesManPassword
        '
        Me.txtSalesManPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesManPassword.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesManPassword.Location = New System.Drawing.Point(112, 8)
        Me.txtSalesManPassword.Name = "txtSalesManPassword"
        Me.txtSalesManPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtSalesManPassword.Size = New System.Drawing.Size(120, 21)
        Me.txtSalesManPassword.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.optExclusive)
        Me.Panel1.Controls.Add(Me.optInclusive)
        Me.Panel1.Controls.Add(Me.optZeroRated)
        Me.Panel1.Location = New System.Drawing.Point(3, 240)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(216, 48)
        Me.Panel1.TabIndex = 222
        '
        'optExclusive
        '
        Me.optExclusive.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optExclusive.Location = New System.Drawing.Point(144, 8)
        Me.optExclusive.Name = "optExclusive"
        Me.optExclusive.Size = New System.Drawing.Size(72, 24)
        Me.optExclusive.TabIndex = 122
        Me.optExclusive.Text = "Exclusive"
        '
        'optInclusive
        '
        Me.optInclusive.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optInclusive.Location = New System.Drawing.Point(72, 8)
        Me.optInclusive.Name = "optInclusive"
        Me.optInclusive.Size = New System.Drawing.Size(72, 24)
        Me.optInclusive.TabIndex = 121
        Me.optInclusive.Text = "Inclusive"
        '
        'optZeroRated
        '
        Me.optZeroRated.Checked = True
        Me.optZeroRated.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optZeroRated.Location = New System.Drawing.Point(8, 8)
        Me.optZeroRated.Name = "optZeroRated"
        Me.optZeroRated.Size = New System.Drawing.Size(56, 32)
        Me.optZeroRated.TabIndex = 120
        Me.optZeroRated.TabStop = True
        Me.optZeroRated.Text = "Zero Rated"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Orange
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(747, 5)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(96, 40)
        Me.Label6.TabIndex = 220
        Me.Label6.Text = "Close - (ESC)"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Orange
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(323, 5)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 40)
        Me.Label5.TabIndex = 219
        Me.Label5.Text = "Tender - (F4)"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Orange
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(419, 5)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(112, 40)
        Me.Label4.TabIndex = 218
        Me.Label4.Text = "Re Print - (F6)"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label12.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label12.Location = New System.Drawing.Point(107, 557)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(256, 24)
        Me.Label12.TabIndex = 226
        Me.Label12.Text = "Inventory Description"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label11.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label11.Location = New System.Drawing.Point(3, 557)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(104, 24)
        Me.Label11.TabIndex = 224
        Me.Label11.Text = "Inventory Code"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label13
        '
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label13.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label13.Location = New System.Drawing.Point(363, 557)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(104, 24)
        Me.Label13.TabIndex = 228
        Me.Label13.Text = "Weight"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblQty
        '
        Me.lblQty.BackColor = System.Drawing.Color.Yellow
        Me.lblQty.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQty.ForeColor = System.Drawing.Color.Navy
        Me.lblQty.Location = New System.Drawing.Point(1000, 56)
        Me.lblQty.Name = "lblQty"
        Me.lblQty.Size = New System.Drawing.Size(104, 25)
        Me.lblQty.TabIndex = 318
        Me.lblQty.Text = "0.00"
        Me.lblQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.Yellow
        Me.Label33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.Navy
        Me.Label33.Location = New System.Drawing.Point(876, 56)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(29, 25)
        Me.Label33.TabIndex = 317
        Me.Label33.Text = "Qty"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.Yellow
        Me.Label34.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label34.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.Color.Black
        Me.Label34.Location = New System.Drawing.Point(872, 48)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(240, 40)
        Me.Label34.TabIndex = 316
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSalesAmount
        '
        Me.lblSalesAmount.BackColor = System.Drawing.Color.Yellow
        Me.lblSalesAmount.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesAmount.ForeColor = System.Drawing.Color.Navy
        Me.lblSalesAmount.Location = New System.Drawing.Point(1000, 96)
        Me.lblSalesAmount.Name = "lblSalesAmount"
        Me.lblSalesAmount.Size = New System.Drawing.Size(104, 25)
        Me.lblSalesAmount.TabIndex = 321
        Me.lblSalesAmount.Text = "0.00"
        Me.lblSalesAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.Yellow
        Me.Label36.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.Color.Navy
        Me.Label36.Location = New System.Drawing.Point(876, 96)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(70, 25)
        Me.Label36.TabIndex = 320
        Me.Label36.Text = "Sales Amount"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.Yellow
        Me.Label37.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label37.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.Color.Black
        Me.Label37.Location = New System.Drawing.Point(872, 88)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(240, 40)
        Me.Label37.TabIndex = 319
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblNetDiscount
        '
        Me.lblNetDiscount.BackColor = System.Drawing.Color.Yellow
        Me.lblNetDiscount.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetDiscount.ForeColor = System.Drawing.Color.Navy
        Me.lblNetDiscount.Location = New System.Drawing.Point(1000, 136)
        Me.lblNetDiscount.Name = "lblNetDiscount"
        Me.lblNetDiscount.Size = New System.Drawing.Size(104, 25)
        Me.lblNetDiscount.TabIndex = 324
        Me.lblNetDiscount.Text = "0.00"
        Me.lblNetDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.Yellow
        Me.Label39.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.Color.Navy
        Me.Label39.Location = New System.Drawing.Point(879, 136)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(56, 25)
        Me.Label39.TabIndex = 323
        Me.Label39.Text = "Net Discount"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.Yellow
        Me.Label40.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label40.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.ForeColor = System.Drawing.Color.Black
        Me.Label40.Location = New System.Drawing.Point(872, 128)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(240, 40)
        Me.Label40.TabIndex = 322
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblNetValue
        '
        Me.lblNetValue.BackColor = System.Drawing.Color.Yellow
        Me.lblNetValue.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetValue.ForeColor = System.Drawing.Color.Navy
        Me.lblNetValue.Location = New System.Drawing.Point(1000, 176)
        Me.lblNetValue.Name = "lblNetValue"
        Me.lblNetValue.Size = New System.Drawing.Size(104, 25)
        Me.lblNetValue.TabIndex = 327
        Me.lblNetValue.Text = "0.00"
        Me.lblNetValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.Yellow
        Me.Label42.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.Color.Navy
        Me.Label42.Location = New System.Drawing.Point(876, 176)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(70, 25)
        Me.Label42.TabIndex = 326
        Me.Label42.Text = "Net Value"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.Yellow
        Me.Label43.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label43.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.Color.Black
        Me.Label43.Location = New System.Drawing.Point(872, 168)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(240, 40)
        Me.Label43.TabIndex = 325
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTax
        '
        Me.lblTax.BackColor = System.Drawing.Color.Yellow
        Me.lblTax.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTax.ForeColor = System.Drawing.Color.Navy
        Me.lblTax.Location = New System.Drawing.Point(1000, 216)
        Me.lblTax.Name = "lblTax"
        Me.lblTax.Size = New System.Drawing.Size(104, 25)
        Me.lblTax.TabIndex = 330
        Me.lblTax.Text = "0.00"
        Me.lblTax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.Yellow
        Me.Label45.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.Color.Navy
        Me.Label45.Location = New System.Drawing.Point(875, 216)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(24, 24)
        Me.Label45.TabIndex = 329
        Me.Label45.Text = "Tax"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.Yellow
        Me.Label46.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label46.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.Color.Black
        Me.Label46.Location = New System.Drawing.Point(872, 208)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(240, 40)
        Me.Label46.TabIndex = 328
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtNetAmount
        '
        Me.txtNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNetAmount.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.txtNetAmount.Location = New System.Drawing.Point(944, 256)
        Me.txtNetAmount.Name = "txtNetAmount"
        Me.txtNetAmount.Size = New System.Drawing.Size(80, 29)
        Me.txtNetAmount.TabIndex = 333
        Me.txtNetAmount.Visible = False
        '
        'lblNetTotal
        '
        Me.lblNetTotal.BackColor = System.Drawing.Color.Yellow
        Me.lblNetTotal.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetTotal.ForeColor = System.Drawing.Color.Navy
        Me.lblNetTotal.Location = New System.Drawing.Point(1000, 256)
        Me.lblNetTotal.Name = "lblNetTotal"
        Me.lblNetTotal.Size = New System.Drawing.Size(104, 25)
        Me.lblNetTotal.TabIndex = 334
        Me.lblNetTotal.Text = "0.00"
        Me.lblNetTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label48
        '
        Me.Label48.BackColor = System.Drawing.Color.Yellow
        Me.Label48.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.ForeColor = System.Drawing.Color.Navy
        Me.Label48.Location = New System.Drawing.Point(873, 256)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(70, 25)
        Me.Label48.TabIndex = 332
        Me.Label48.Text = "Net Amount"
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.Yellow
        Me.Label49.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label49.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.ForeColor = System.Drawing.Color.Black
        Me.Label49.Location = New System.Drawing.Point(872, 248)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(240, 40)
        Me.Label49.TabIndex = 331
        Me.Label49.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grdMarketRate
        '
        Me.grdMarketRate.AllowUserToAddRows = False
        Me.grdMarketRate.AllowUserToDeleteRows = False
        Me.grdMarketRate.AllowUserToOrderColumns = True
        Me.grdMarketRate.AllowUserToResizeColumns = False
        Me.grdMarketRate.AllowUserToResizeRows = False
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdMarketRate.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdMarketRate.ColumnHeadersHeight = 25
        Me.grdMarketRate.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cClass, Me.cRate})
        Me.grdMarketRate.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdMarketRate.Enabled = False
        Me.grdMarketRate.Location = New System.Drawing.Point(5, 6)
        Me.grdMarketRate.Name = "grdMarketRate"
        Me.grdMarketRate.RowHeadersVisible = False
        Me.grdMarketRate.RowHeadersWidth = 50
        Me.grdMarketRate.Size = New System.Drawing.Size(213, 236)
        Me.grdMarketRate.TabIndex = 335
        '
        'cClass
        '
        Me.cClass.HeaderText = "Class"
        Me.cClass.Name = "cClass"
        '
        'cRate
        '
        Me.cRate.HeaderText = "Rate"
        Me.cRate.Name = "cRate"
        '
        'grdSales
        '
        Me.grdSales.AllowUserToAddRows = False
        Me.grdSales.AllowUserToDeleteRows = False
        Me.grdSales.AllowUserToOrderColumns = True
        Me.grdSales.AllowUserToResizeColumns = False
        Me.grdSales.AllowUserToResizeRows = False
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdSales.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.grdSales.ColumnHeadersHeight = 25
        Me.grdSales.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cItemCode, Me.cClassCode, Me.cDesc, Me.cWeight, Me.cGoldLoss, Me.cCostpergram, Me.cDSwt, Me.cSMC, Me.cPCS, Me.cTotalValue, Me.cTotalCost, Me.cSalesType, Me.cPMC, Me.cGSt})
        Me.grdSales.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdSales.Location = New System.Drawing.Point(3, 294)
        Me.grdSales.Name = "grdSales"
        Me.grdSales.RowHeadersVisible = False
        Me.grdSales.RowHeadersWidth = 50
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSales.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.grdSales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdSales.Size = New System.Drawing.Size(1112, 260)
        Me.grdSales.TabIndex = 336
        '
        'cItemCode
        '
        Me.cItemCode.HeaderText = "Item Code"
        Me.cItemCode.Name = "cItemCode"
        '
        'cClassCode
        '
        Me.cClassCode.HeaderText = "Class"
        Me.cClassCode.Name = "cClassCode"
        Me.cClassCode.Visible = False
        '
        'cDesc
        '
        Me.cDesc.HeaderText = "Description"
        Me.cDesc.Name = "cDesc"
        Me.cDesc.Width = 270
        '
        'cWeight
        '
        Me.cWeight.HeaderText = "Weight"
        Me.cWeight.Name = "cWeight"
        Me.cWeight.Width = 80
        '
        'cGoldLoss
        '
        Me.cGoldLoss.HeaderText = "Gold Loss"
        Me.cGoldLoss.Name = "cGoldLoss"
        Me.cGoldLoss.Width = 90
        '
        'cCostpergram
        '
        Me.cCostpergram.HeaderText = "Cost Per Gram"
        Me.cCostpergram.Name = "cCostpergram"
        Me.cCostpergram.Width = 120
        '
        'cDSwt
        '
        Me.cDSwt.HeaderText = "D/S Wt"
        Me.cDSwt.Name = "cDSwt"
        Me.cDSwt.Width = 80
        '
        'cSMC
        '
        Me.cSMC.HeaderText = "SMC"
        Me.cSMC.Name = "cSMC"
        Me.cSMC.Width = 80
        '
        'cPCS
        '
        Me.cPCS.HeaderText = "PCS"
        Me.cPCS.Name = "cPCS"
        Me.cPCS.Width = 80
        '
        'cTotalValue
        '
        Me.cTotalValue.HeaderText = "Total Value"
        Me.cTotalValue.Name = "cTotalValue"
        '
        'cTotalCost
        '
        Me.cTotalCost.HeaderText = "Total Cost"
        Me.cTotalCost.Name = "cTotalCost"
        '
        'cSalesType
        '
        Me.cSalesType.HeaderText = "Sales Type"
        Me.cSalesType.Name = "cSalesType"
        '
        'cPMC
        '
        Me.cPMC.HeaderText = "PMC"
        Me.cPMC.Name = "cPMC"
        '
        'cGSt
        '
        Me.cGSt.HeaderText = "GST"
        Me.cGSt.Name = "cGSt"
        '
        'grdPaymode
        '
        Me.grdPaymode.AllowUserToAddRows = False
        Me.grdPaymode.AllowUserToDeleteRows = False
        Me.grdPaymode.AllowUserToResizeColumns = False
        Me.grdPaymode.AllowUserToResizeRows = False
        Me.grdPaymode.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cPaymode, Me.cAmount, Me.cSlipNO})
        Me.grdPaymode.Location = New System.Drawing.Point(22, 0)
        Me.grdPaymode.Name = "grdPaymode"
        Me.grdPaymode.RowHeadersVisible = False
        Me.grdPaymode.Size = New System.Drawing.Size(197, 221)
        Me.grdPaymode.TabIndex = 337
        Me.grdPaymode.Visible = False
        '
        'cPaymode
        '
        Me.cPaymode.HeaderText = "Paymode"
        Me.cPaymode.Name = "cPaymode"
        '
        'cAmount
        '
        Me.cAmount.HeaderText = "Amount"
        Me.cAmount.Name = "cAmount"
        '
        'cSlipNO
        '
        Me.cSlipNO.HeaderText = "Slip No"
        Me.cSlipNO.Name = "cSlipNO"
        '
        'pnlCostAdminPwd
        '
        Me.pnlCostAdminPwd.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlCostAdminPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCostAdminPwd.Controls.Add(Me.btnCostAdminPwdCancel)
        Me.pnlCostAdminPwd.Controls.Add(Me.btnCostAdminPwdOK)
        Me.pnlCostAdminPwd.Controls.Add(Me.txtCostAdminPassword)
        Me.pnlCostAdminPwd.Controls.Add(Me.txtPassFind)
        Me.pnlCostAdminPwd.Controls.Add(Me.Label61)
        Me.pnlCostAdminPwd.Location = New System.Drawing.Point(433, 285)
        Me.pnlCostAdminPwd.Name = "pnlCostAdminPwd"
        Me.pnlCostAdminPwd.Size = New System.Drawing.Size(246, 88)
        Me.pnlCostAdminPwd.TabIndex = 338
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
        'Label61
        '
        Me.Label61.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label61.ForeColor = System.Drawing.Color.Black
        Me.Label61.Location = New System.Drawing.Point(8, 8)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(56, 15)
        Me.Label61.TabIndex = 72
        Me.Label61.Text = "Admin"
        Me.Label61.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkExport
        '
        Me.chkExport.AutoSize = True
        Me.chkExport.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkExport.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkExport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkExport.Location = New System.Drawing.Point(528, 210)
        Me.chkExport.Name = "chkExport"
        Me.chkExport.Size = New System.Drawing.Size(108, 17)
        Me.chkExport.TabIndex = 556
        Me.chkExport.Text = "Export Invoice"
        Me.chkExport.UseVisualStyleBackColor = False
        '
        'frmGPOSWholeSale
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(1112, 659)
        Me.Controls.Add(Me.PnlReprint)
        Me.Controls.Add(Me.pnlBillCancel)
        Me.Controls.Add(Me.pnlCostAdminPwd)
        Me.Controls.Add(Me.grdPaymode)
        Me.Controls.Add(Me.pnlTradeIn)
        Me.Controls.Add(Me.grdSales)
        Me.Controls.Add(Me.grdMarketRate)
        Me.Controls.Add(Me.txtNetAmount)
        Me.Controls.Add(Me.txtTotalValue)
        Me.Controls.Add(Me.txtGST)
        Me.Controls.Add(Me.txtSMC)
        Me.Controls.Add(Me.txtPCS)
        Me.Controls.Add(Me.txtCostPerGram)
        Me.Controls.Add(Me.txtGoldLoss)
        Me.Controls.Add(Me.txtWeight)
        Me.Controls.Add(Me.txtInventoryDescription)
        Me.Controls.Add(Me.txtInventoryCode)
        Me.Controls.Add(Me.txtSlipNo8)
        Me.Controls.Add(Me.txtSlipNo7)
        Me.Controls.Add(Me.txtAmount8)
        Me.Controls.Add(Me.txtAmount7)
        Me.Controls.Add(Me.txtSlipNo6)
        Me.Controls.Add(Me.txtSlipNo5)
        Me.Controls.Add(Me.txtSlipNo4)
        Me.Controls.Add(Me.txtSlipNo3)
        Me.Controls.Add(Me.txtSlipNo2)
        Me.Controls.Add(Me.txtAmount6)
        Me.Controls.Add(Me.txtSlipNo1)
        Me.Controls.Add(Me.txtAmount5)
        Me.Controls.Add(Me.txtAmount4)
        Me.Controls.Add(Me.txtAmount3)
        Me.Controls.Add(Me.txtAmount2)
        Me.Controls.Add(Me.txtAmount1)
        Me.Controls.Add(Me.txtAmount0)
        Me.Controls.Add(Me.txtSlipNo0)
        Me.Controls.Add(Me.lblNetTotal)
        Me.Controls.Add(Me.Label48)
        Me.Controls.Add(Me.Label49)
        Me.Controls.Add(Me.lblTax)
        Me.Controls.Add(Me.Label45)
        Me.Controls.Add(Me.Label46)
        Me.Controls.Add(Me.lblNetValue)
        Me.Controls.Add(Me.Label42)
        Me.Controls.Add(Me.Label43)
        Me.Controls.Add(Me.lblNetDiscount)
        Me.Controls.Add(Me.Label39)
        Me.Controls.Add(Me.Label40)
        Me.Controls.Add(Me.lblSalesAmount)
        Me.Controls.Add(Me.Label36)
        Me.Controls.Add(Me.Label37)
        Me.Controls.Add(Me.lblQty)
        Me.Controls.Add(Me.Label33)
        Me.Controls.Add(Me.Label34)
        Me.Controls.Add(Me.pnlFindCost)
        Me.Controls.Add(Me.pnlAdminPwd)
        Me.Controls.Add(Me.btnPayClear)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.pnlBillCancelAdminPwd)
        Me.Controls.Add(Me.txtStoneWeight)
        Me.Controls.Add(Me.lblTerminal)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.lblReturnAmount)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label32)
        Me.Controls.Add(Me.lblSManTotal)
        Me.Controls.Add(Me.Label30)
        Me.Controls.Add(Me.lblNextInvoiceNo)
        Me.Controls.Add(Me.Label28)
        Me.Controls.Add(Me.lblLastBillAmount)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.lblLastBillNo)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.lblLastSalesman)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.lblLocation)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.lblPaymode8)
        Me.Controls.Add(Me.lblPaymode7)
        Me.Controls.Add(Me.lblPaymode6)
        Me.Controls.Add(Me.lblPaymode5)
        Me.Controls.Add(Me.lblPaymode4)
        Me.Controls.Add(Me.lblPaymode3)
        Me.Controls.Add(Me.lblPaymode2)
        Me.Controls.Add(Me.lblPaymode1)
        Me.Controls.Add(Me.lblPaymode0)
        Me.KeyPreview = True
        Me.Name = "frmGPOSWholeSale"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "WholeSale Jewel POS"
        Me.pnlAdminPwd.ResumeLayout(False)
        Me.pnlAdminPwd.PerformLayout()
        Me.PnlReprint.ResumeLayout(False)
        Me.PnlReprint.PerformLayout()
        Me.pnlTradeIn.ResumeLayout(False)
        Me.pnlTradeIn.PerformLayout()
        CType(Me.grdTradeIN, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.pnlFindCost.ResumeLayout(False)
        Me.pnlFindCost.PerformLayout()
        Me.pnlBillCancel.ResumeLayout(False)
        Me.pnlBillCancel.PerformLayout()
        Me.pnlBillCancelAdminPwd.ResumeLayout(False)
        Me.pnlBillCancelAdminPwd.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnlPMCType.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.grdMarketRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSales, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdPaymode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCostAdminPwd.ResumeLayout(False)
        Me.pnlCostAdminPwd.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Dim sCompanyCode As String
    Dim sTerminalCode As String
    Dim sLocationCode As String
    Dim sInvoiceNo As String
    Dim sSalesManCode As String
    Dim sGSTType As String
    Dim nAllowSalesReturn As Integer
    Dim CompName$, Address1$, Address2$, Country$, PhoneNo$, FaxNo$
    Dim dGSTPerc As Double
    Dim dDisPercentage As Double
    Dim sPrinterType$, dCustomerDisplayPort#, dCashDrawerInterface#, dCashDrawerPort#
    Dim bCustomerDisplay As Boolean, bPrinter As Boolean
    Dim bVoiceEnabled As Boolean
    Dim sClassCode As String
    Public msClsSalesType As String
    Dim mdCommission#, mdPurMC#, mdPMC#
    Dim dDailyClassRate As Double
    Dim dTotalCost As Double
    Dim bedited As Boolean
    Dim dTotalWeight, dTotalGoldLoss, dTotalSMC, dRate As Double
    Dim sSalesType As String
    Dim RecCount As Integer
    Dim iCnt As Long
    Dim sLoad As String
    Dim bAmountFlag As Boolean
    Dim dFindTotalMarketPrice As Double
    Dim dNetTotalMarketPrice As Double
    Dim sCustomerCode As String
    Dim sUserCode As String
    Dim sUserName As String
    Dim bPassword As Boolean
    Dim bBCPassword As Boolean
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
    Dim dActualSales#, dNetDiscount#, dNetAmount#
    Dim dGST As Double
    Dim sSDate As String
    Dim dCost As Double
    Dim dTotAmount As Double
    Dim sItemNo As Long
    Dim sCreditInvoiceNo As String
    Public tempTV As Double
    Public tempSMC As Double
    Public Diff As Double
    Public NTotVal As Double
    Public NSMC As Double
    Public dTotalOrgCost As Double
    'class objects
    Dim objComp As New clsCompany
    Dim objTerminal As New clsTerminal
    Dim objClass As New ClsClass
    Dim objClassRate As New ClsClass
    Dim objCust As New clsCustomer
    Dim objSales As New ClsSales
    Dim objPaymode As New clsPayMode
    Dim objInv As New clsInventory
    Dim objSupplier As New clsSuppliers
    Dim objIns As New ClsInstallment
    Dim objSalesReturn As New ClsSalesReturn
    Dim objUM As New clsUserMasters
    Dim objMember As New clsMember
    Dim objSM As New clsSalesMan
    Dim objPay As New clsPayMode

    'Constant Declaration

    Const MRClassCode = 1
    Const MRRate = 2

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

    Const LblPaymode = 1
    Const TxtAmount = 2
    Const TxtSlipNo = 3


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
        sDate = CStr(Format(CDate(ServerDate), "yyyy") + Format(CDate(ServerDate), "MM") + Format(CDate(ServerDate), "dd"))
        Try
            dsSMPWD = objSM.GetSalesManwithPassword(txtSalesManPassword.Text.Trim)
            If dsSMPWD.Tables(0).Rows.Count > 0 Then
                txtSalesManName.Text = Trim(dsSMPWD.Tables(0).Rows(0)("SalesManName") & "")
                sSalesManCode = Trim(dsSMPWD.Tables(0).Rows(0)("SalesmanCode") & "")
                sManCode = Trim(dsSMPWD.Tables(0).Rows(0)("SalesmanCode") & "")
                lblLastSalesman.Text = Trim(dsSMPWD.Tables(0).Rows(0)("SalesManName") & "")
                lblLastBillNo.Text = Trim(dsSMPWD.Tables(0).Rows(0)("InvoiceNo") & "")
                lblLastBillAmount.Text = Trim(dsSMPWD.Tables(0).Rows(0)("LastSalesAmount") & "")
                nAllowSalesReturn = dsSMPWD.Tables(0).Rows(0)("AllowSalesReturn") & ""
                If UCase(sCompanyShortCode) = "HN" Then
                    If UCase(txtCustomerCode.Text.Trim) = "" Then
                        txtCustomerCode.Text = "CASH"
                        txtCustomerName.Text = "CASH CUSTOMER"
                        ''If txtInventoryCode.Enabled = True Then txtInventoryCode.Focus()
                        If txtTealGST.Enabled Then txtTealGST.Focus()
                    End If
                Else
                    If UCase(txtCustomerCode.Text.Trim) = "" Then
                        txtCustomerCode.Text = "CASH"
                    End If
                End If
            Else
                MessageBox.Show("Invalid Password.")
                txtSalesManPassword.Clear()
                txtSalesManPassword.Focus()
            End If
            dsInvoiceNo = objSales.GetUpdateCreditInvoice()
            If dsInvoiceNo.Tables(0).Rows.Count > 0 Then
                lblNextInvoiceNo.Text = Trim(dsInvoiceNo.Tables(0).Rows(0)("NextCReditInvoice") & "")
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


    Private Sub frmGPOSNew_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If txtSalesManPassword.Enabled Then txtSalesManPassword.Focus()
        ' subGridSetting(grdMarketRate)
        ' subGridSetting(grdSales)
        ' subPayModeGridSettings(grdPaymode)
        ' subGridSetting(grdTradeIn)
        grdSales.RowCount = 0
        subGetCompany()
        subLoadTerminalParameter()
        subShowClassMarketRate()
        subLoadPayMode()
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
                ' lblCompanyName.Text = CompName
                '  lblCompanyAddress.Text = Address1 & ", " & Address2 & vbCrLf & " Phone : " & PhoneNo & " Fax : " & FaxNo
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
                            fnSetdgvText(grdMarketRate, grdMarketRate.RowCount - 1, 1, Val(Trim(dsClass.Tables("Class").Rows(iCnt)("Rate") & "")))
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtInventoryCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInventoryCode.LostFocus
        Try
            If Trim(txtInventoryCode.Text) <> "" Then
                'Blocked by Hafil for Abiramee
                'If txtInventoryCode.Text.Trim.ToUpper = "T" Then
                '    subShowTradein()
                '    Exit Sub
                '    'ElseIf txtInventoryCode.Text.Trim.ToUpper = "C" Then
                '    '    subShowCASH()
                '    '    Exit Sub
                '    'ElseIf txtInventoryCode.Text.Trim.ToUpper = "I" Then
                '    '    subShowInstallment()
                '    '    Exit Sub
                'End If
                ''' Durai changes 
                If Not fncCheckExisting(txtInventoryCode.Text) Then
                    If fncGetInventory(txtInventoryCode.Text) Then
                        If Trim(sClassCode) <> "" Then
                            If Val(txtCostPerGram.Text) <> 0 Then
                                If msClsSalesType <> "P" Then
                                    txtTotalValue.Text = Format((Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram.Text) + Val(txtSMC.Text), "0.00")
                                    ''''If optPMCTypePerc.Checked Then
                                    ''''    txtTotalValue.Text = Format((Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram.Text) + mdPurMC + (mdPurMC * Val(txtTealGST.Text) / 100), "0.00")
                                    ''''    mdPMC = Format(mdPurMC + (mdPurMC * Val(txtPMCperc.Text) / 100), "0.00")
                                    ''''ElseIf optPMCTypeValue.Checked Then
                                    ''''    txtTotalValue.Text = Format((Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram.Text) + mdPurMC + Val(txtPMCperc.Text), "0.00")
                                    ''''    mdPMC = Format(mdPurMC + Val(txtPMCperc.Text), "0.00")
                                    ''''End If
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
                                    '''txtWeight.Enabled = False
                                    'txtWeight.Focus()
                                    txtSMC.Enabled = True
                                    txtSMC.Focus()
                                End If

                                If optExclusive.Checked = True Then
                                    txtGST.Text = Format$(Val(txtTotalValue.Text) * (dGSTPerc / 100), "#0.00")
                                    'txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                                    txtTotalValue.Text = Format(Val(txtTotalValue.Text), "0.00")
                                ElseIf optInclusive.Checked = True Then
                                    txtGST.Text = Format$((Val(txtTotalValue.Text) * dGSTPerc) / (100), "#0.00")
                                    'txtGST.Text = Format$((Val(txtTotalValue.Text) * dGSTPerc) / (100 + dGSTPerc), "#0.00")
                                    txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                                ElseIf optZeroRated.Checked = True Then
                                    txtGST.Text = 0.0
                                    txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                                End If
                                dRate = Val(txtCostPerGram.Text)
                                'SHabeer 20151214
                                txtGoldLoss_LostFocus(sender, e)
                                txtPCS_LostFocus(sender, e)
                                txtSMC_LostFocus(sender, e)
                                'SHabeer 20151214
                                If sDetailDiscount = "1" Then
                                    If txtTotalValue.Enabled Then txtTotalValue.Focus()
                                Else
                                    If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
                                    '''If txtSMC.Enabled Then txtSMC.Focus()
                                End If


                                Exit Sub
                            End If
                            If Trim(sClassCode) <> "" Then
                                'txtGoldLoss.SetFocus
                            Else
                                If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
                                Exit Sub
                            End If
                        End If
                    End If
                Else
                    If bedited = False Then
                        MessageBox.Show("Item already scanned ...")
                        txtInventoryCode.Text = ""
                        If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
                        Exit Sub
                    End If
                End If
            Else
                If Trim(txtInventoryCode.Text) = "" Then
                    ' If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
                    Exit Sub
                End If

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
                If Trim(fnGetdgvText(grdSales, icnt, 0)) = Trim(sinventorycode) Then
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
        Try
            dsInv = objInv.GetInventoryDetail(sinventorycode)
            fncGetInventory = True
            If dsInv.Tables(0).Rows.Count > 0 Then
                lSalesBillNo = Val(dsInv.Tables(0).Rows(0)("SalesBillNo") & "")
                If ((Val(dsInv.Tables(0).Rows(0)("QtyOnHand") & "") = 0) And (Val(dsInv.Tables(0).Rows(0)("QtySold") & "") = 0)) Then
                    MsgBox("No Stock for this Inventory!")
                    txtInventoryCode.Text = ""
                    fncGetInventory = False
                    If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
                    Exit Function
                End If

                If (Val(dsInv.Tables(0).Rows(0)("QtyOnHand") & "") = 0) Then
                    MsgBox("Item Already Sold for Invoice " & Trim(dsInv.Tables(0).Rows(0)("SalesBillNo") & "") & " on " & Format(dsInv.Tables(0).Rows(0)("SalesDAte"), "dd/MM/yyyy"))
                    txtInventoryCode.Text = ""
                    fncGetInventory = False
                    If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
                    Exit Function
                End If
                If (Val(dsInv.Tables(0).Rows(0)("QtyOnHand") & "") < 1) Then
                    MsgBox("No Stock for this Inventory!")
                    fncGetInventory = False
                    txtInventoryCode.Text = ""
                    If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
                    Exit Function
                End If


                If Trim$(dsInv.Tables(0).Rows(0)("LocationCode") & "") <> gthisLocation Then
                    MsgBox("The selected Inventory belongs to the Location - " & Trim$(dsInv.Tables(0).Rows(0)("LocationCode") & "") & "!")
                    fncGetInventory = False
                    txtInventoryCode.Text = ""
                    If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
                    Exit Function
                End If

                sClassCode = Trim(dsInv.Tables(0).Rows(0)("ClassCode") & "")
                'load class details to get the rate and the salestype.
                subGetClass()
                txtInventoryDescription.Text = Trim(dsInv.Tables(0).Rows(0)("Description") & "")
                If msClsSalesType = "P" Or msClsSalesType = "B" Then
                    txtPCS.Enabled = True
                    txtStoneWeight.Text = Format(Val(dsInv.Tables(0).Rows(0)("StoneWeight") & ""), "0.00")

                    If UCase(sCompanyShortCode) = "ES" Or UCase(sCompanyShortCode) = "AS" Or UCase(sCompanyShortCode) = "HN" Then
                        txtPCS.Text = 1
                        'ElseIf UCase(sCompanyShortCode) = "HN" Then
                        '    If txtPCS.Enabled = True Then txtPCS.Focus()
                    Else
                        txtPCS.Text = Val(dsInv.Tables(0).Rows(0)("QtyOnHand") & "")
                    End If
                    If Val(dsInv.Tables(0).Rows(0)("Pcs")) > 0 Then
                        txtWeight.Text = Format(Val(dsInv.Tables(0).Rows(0)("Weight") & "") * Val(txtPCS.Text) / Val(dsInv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                        txtGoldLoss.Text = Format(Val(dsInv.Tables(0).Rows(0)("GoldLoss") & "") * Val(txtPCS.Text) / Val(dsInv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                        txtSMC.Text = Format(Val(dsInv.Tables(0).Rows(0)("sMc") & "") * Val(txtPCS.Text) / Val(dsInv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                        ' txtSMC.Text = Format(Val(dsInv.Tables(0).Rows(0)("PMC") & "") * Val(txtPCS.Text) / Val(dsInv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
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
                    ''HAJ jewel palace assorted item sales 23 jan 2008
                    If UCase(msClsSalesType) = "B" Then
                        txtSMC.Text = Format(Val(dsInv.Tables(0).Rows(0)("sMc") & "") / Val(dsInv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                        'txtSMC.Text = Format(Val(dsInv.Tables(0).Rows(0)("PMC") & "") / Val(dsInv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                    Else
                        txtSMC.Text = Format(Val(dsInv.Tables(0).Rows(0)("sMc") & ""), "0.00")
                        'txtSMC.Text = Format(Val(dsInv.Tables(0).Rows(0)("PMC") & ""), "0.00")
                    End If
                    mdPurMC = Val(dsInv.Tables(0).Rows(0)("PMC") & "")
                    dTotalCost = Val(dsInv.Tables(0).Rows(0)("TotalCost") & "")
                End If

            Else
                MsgBox("Inventory Not Found ...")
                fncGetInventory = False
                txtInventoryCode.Text = ""
                If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
                Exit Function
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function
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

    Private Sub txtGoldLoss_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGoldLoss.LostFocus
        Try
            If Trim(txtInventoryCode.Text) <> "" Then
                If msClsSalesType <> "P" Then
                    txtTotalValue.Text = Format((Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram.Text) + Val(txtSMC.Text), "0.00")
                    '''If optPMCTypePerc.Checked Then
                    '''    txtTotalValue.Text = Format((Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram.Text) + mdPurMC + (mdPurMC * Val(txtPMCperc.Text) / 100), "0.00")
                    '''    mdPMC = Format(mdPurMC + (mdPurMC * Val(txtPMCperc.Text) / 100), "0.00")
                    '''ElseIf optPMCTypeValue.Checked Then
                    '''    txtTotalValue.Text = Format((Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram.Text) + mdPurMC + Val(txtPMCperc.Text), "0.00")
                    '''    mdPMC = Format(mdPurMC + Val(txtPMCperc.Text), "0.00")
                    '''End If
                    ''HAJ AS PCS Changes on 29 Aug 2007
                ElseIf msClsSalesType = "P" And UCase(sCompanyShortCode) = "AS" Then
                    txtTotalValue.Text = Format((Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram.Text) + Val(txtSMC.Text), "0.00")
                Else
                    txtTotalValue.Text = Format(Val(txtSMC.Text), "0.00")
                End If
                If optExclusive.Checked = True Then
                    txtGST.Text = Format$(Val(txtTotalValue.Text) * (dGSTPerc / 100), "#0.00")
                    ' txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                    txtTotalValue.Text = Format(Val(txtTotalValue.Text), "0.00")
                ElseIf optInclusive.Checked = True Then
                    txtGST.Text = Format$((Val(txtTotalValue.Text) * dGSTPerc) / (100), "#0.00")
                    'txtGST.Text = Format$((Val(txtTotalValue.Text) * dGSTPerc) / (100 + dGSTPerc), "#0.00")
                    txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                ElseIf optZeroRated.Checked = True Then
                    txtGST.Text = 0.0
                    txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                End If

            Else
                If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtPCS_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPCS.LostFocus
        Dim dsinv As New DataSet
        Try
            ' dMarketCost = 20
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
                        If Not bedited Then
                            txtWeight.Text = Format(Val(dsinv.Tables(0).Rows(0)("Weight") & ""), "0.00")
                            txtGoldLoss.Text = Format(Val(dsinv.Tables(0).Rows(0)("GoldLoss") & "") * Val(txtPCS.Text) / Val(dsinv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                            ''' txtSMC.Text = Format(Val(dsinv.Tables(0).Rows(0)("sMc") & "") * Val(txtPCS.Text) / Val(dsinv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                            txtSMC.Text = Format(Val(dsinv.Tables(0).Rows(0)("PMC") & "") * Val(txtPCS.Text) / Val(dsinv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                            mdPurMC = Val(dsinv.Tables(0).Rows(0)("PMC") & "") * Val(txtPCS.Text) / Val(dsinv.Tables(0).Rows(0)("Pcs") & "")
                            dTotalCost = Val(dsinv.Tables(0).Rows(0)("TotalCost") & "") * Val(txtPCS.Text) / Val(dsinv.Tables(0).Rows(0)("Pcs") & "")
                            txtTotalValue.Text = Format((Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram.Text) + Val(txtSMC.Text), "0.00")
                            '''If optPMCTypePerc.Checked Then
                            '''    txtTotalValue.Text = Format((Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram.Text) + mdPurMC + (mdPurMC * Val(txtPMCperc.Text) / 100), "0.00")
                            '''    mdPMC = Format(mdPurMC + (mdPurMC * Val(txtPMCperc.Text) / 100), "0.00")
                            '''ElseIf optPMCTypeValue.Checked Then
                            '''    txtTotalValue.Text = Format((Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram.Text) + mdPurMC + Val(txtPMCperc.Text), "0.00")
                            '''    mdPMC = Format(mdPurMC + Val(txtPMCperc.Text), "0.00")
                            '''End If
                        End If

                        If optExclusive.Checked = True Then
                            txtGST.Text = Format$(Val(txtTotalValue.Text) * (dGSTPerc / 100), "#0.00")
                            ' txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                            txtTotalValue.Text = Format(Val(txtTotalValue.Text), "0.00")
                        ElseIf optInclusive.Checked = True Then
                            txtGST.Text = Format$((Val(txtTotalValue.Text) * dGSTPerc) / (100), "#0.00")
                            txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                        ElseIf optZeroRated.Checked = True Then
                            txtGST.Text = 0.0
                            txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                        End If
                    End If
                Else
                    txtPCS.Text = 1
                    txtPCS.Enabled = False
                End If
            Else
                If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
                Exit Sub
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
                ''' Hide By Durai 
                '''If Val(txtSMC.Text) < Val(mdPurMC) Then
                '''    MsgBox("Please enter the SMC greater than " & Val(mdPurMC))
                '''    txtSMC.Focus()
                '''    Exit Sub
                '''End If
                If msClsSalesType <> "P" Then
                    txtTotalValue.Text = Format((Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram.Text) + Val(txtSMC.Text), "0.00")
                    '''''If optPMCTypePerc.Checked Then
                    '''''    txtTotalValue.Text = Format((Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram.Text) + mdPurMC + (mdPurMC * Val(txtPMCperc.Text) / 100), "0.00")
                    '''''    mdPMC = Format(mdPurMC + (mdPurMC * Val(txtPMCperc.Text) / 100), "0.00")
                    '''''ElseIf optPMCTypeValue.Checked Then
                    '''''    txtTotalValue.Text = Format((Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram.Text) + mdPurMC + Val(txtPMCperc.Text), "0.00")
                    '''''    mdPMC = Format(mdPurMC + Val(txtPMCperc.Text), "0.00")
                    '''''End If
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

                If optExclusive.Checked = True Then
                    txtGST.Text = Format$(Val(txtTotalValue.Text) * (dGSTPerc / 100), "#0.00")
                    'txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                    txtTotalValue.Text = Format(Val(txtTotalValue.Text), "0.00")
                ElseIf optInclusive.Checked = True Then
                    txtGST.Text = Format$((Val(txtTotalValue.Text) * dGSTPerc) / (100), "#0.00")
                    'txtGST.Text = Format$((Val(txtTotalValue.Text) * dGSTPerc) / (100 + dGSTPerc), "#0.00")
                    txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                ElseIf optZeroRated.Checked = True Then
                    txtGST.Text = 0.0
                    txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                End If
                'txtGST.Text = Format$(Val(txtTotalValue.Text) * (dGSTPerc / 100), "#0.00")
                'txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                If sDetailDiscount = "1" Then
                    txtTotalValue.Focus()
                Else
                    subAddSales()
                End If

            Else
                If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
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
                'grdSales.FontBold = False
                'If Not bedited Then
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
                ''ADD BY HAFIL

                If Not bedited Then
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

                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(5).Style.ForeColor = Color.Black
                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(5).Style.Font = New Font(grdSales.Font.FontFamily, 10, FontStyle.Regular)

                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(6).Style.ForeColor = Color.Black
                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(6).Style.Font = New Font(grdSales.Font.FontFamily, 10, FontStyle.Regular)


                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(10).Style.ForeColor = Color.Black
                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(10).Style.Font = New Font(grdSales.Font.FontFamily, 10, FontStyle.Regular)
                ''''added by sarathy to detect the advance paid amount for the ordered items
                '''If dAdvancePaid <> 0 Then txtTotalValue.Text = Format(Val(txtTotalValue.Text) - Val(dAdvancePaid), "0.00")
                TotVal = Val(txtTotalValue.Text)
                sMc = Val(txtSMC.Text)

                'Call SetText(grdSales, grdSales.Row, ICode, Trim(txtInventoryCode.Text))
                'Call SetText(grdSales, grdSales.Row, IClass, Trim(sClassCode))
                'Call SetText(grdSales, grdSales.Row, IDescription, Trim(txtInventoryDescription.Text))
                'Call SetText(grdSales, grdSales.Row, IWeight, Val(txtWeight.Text))
                'Call SetText(grdSales, grdSales.Row, IGoldLoss, Val(txtGoldLoss.Text))
                'Call SetText(grdSales, grdSales.Row, ICostPerGram, Val(txtCostPerGram.Text))
                'Call SetText(grdSales, grdSales.Row, IDSWeight, Val(txtStoneWeight.Text))
                'Call SetText(grdSales, grdSales.Row, ISMC, Val(txtSMC.Text))
                ''Call SetText(grdSales, grdSales.Row, ISMC, Val(mdPMC))
                'Call SetText(grdSales, grdSales.Row, IPMC, Val(mdPurMC))
                'Call SetText(grdSales, grdSales.Row, IPCS, Val(txtPCS.Text))
                'Call SetText(grdSales, grdSales.Row, ITotalValue, Val(txtTotalValue.Text))
                'Call SetText(grdSales, grdSales.Row, ITotalCost, Val(dTotalCost))
                'Call SetText(grdSales, grdSales.Row, ISalesType, Trim$(msClsSalesType))
                'Call SetText(grdSales, grdSales.Row, IGST, Val(txtGST.Text))


                Call fnSetdgvText(grdSales, r, 0, Trim(txtInventoryCode.Text))
                Call fnSetdgvText(grdSales, r, 1, Trim(sClassCode))
                Call fnSetdgvText(grdSales, r, 2, Trim(txtInventoryDescription.Text))
                Call fnSetdgvText(grdSales, r, 3, Format(Val(txtWeight.Text), "0.00"))
                Call fnSetdgvText(grdSales, r, 4, Format(Val(txtGoldLoss.Text), "0.00"))
                Call fnSetdgvText(grdSales, r, 5, Format(Val(txtCostPerGram.Text), "0.00"))
                Call fnSetdgvText(grdSales, r, 6, Val(txtStoneWeight.Text))
                Call fnSetdgvText(grdSales, r, 7, Format(Val(txtSMC.Text), "0.00"))
                Call fnSetdgvText(grdSales, r, 8, Val(txtPCS.Text))
                Call fnSetdgvText(grdSales, r, 9, Format(Val(txtTotalValue.Text), "0.00"))
                Call fnSetdgvText(grdSales, r, 10, Val(dTotalCost))
                Call fnSetdgvText(grdSales, r, 11, Trim(msClsSalesType))
                Call fnSetdgvText(grdSales, r, 12, Val(mdPurMC))
                Call fnSetdgvText(grdSales, r, 13, Format(Val(txtGST.Text), "0.00"))



                'grdSales.Row = grdSales.MaxRows
                'grdSales.Col = 1
                'grdSales.Action = 0
                subTotalFromGrid()
                subTotal()
                subClearDetail()
                'If grdSales.MaxRows >= 2 Then
                '    optZeroRated.Enabled = False
                '    optInclusive.Enabled = False
                '    optExclusive.Enabled = False
                'End If
                ''haj 
                bedited = False
            End If
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
        '    If Not bedited Then
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
        '        subGetGSTAmount()
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
            If Not bedited Then
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

                '  If UCase(sSalesType) <> "TRADEIN" Then
                subGetGSTAmount()
                'Else
                '
                'End If
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
    Private Sub subClearDetail()
        Try
            bedited = False
            txtInventoryCode.Clear()
            txtInventoryDescription.Clear()
            txtWeight.Clear()
            txtGoldLoss.Clear()
            txtSMC.Clear()
            mdPurMC = 0
            mdPMC = 0
            txtCostPerGram.Clear()
            txtPCS.Clear()
            txtTotalValue.Clear()
            sClassCode = ""
            '  txtStoneWeight.Clear()
            dTotalCost = 0
            msClsSalesType = ""
            txtGST.Clear()
            If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subGetGSTAmount()
        Try
            Dim nGSTAmount As Double
            If optExclusive.Checked = True Then
                '  lblSalesType.Text = "EXCLUSIVE SALES"
                If dGSTPerc > 0 Then
                    nGSTAmount = Val(lblNetValue.Text) * dGSTPerc / 100
                Else
                    nGSTAmount = 0
                End If
                lblTax.Text = Format(nGSTAmount, "0.00")
                lblNetTotal.Text = Format(Val(lblNetValue.Text) + nGSTAmount, "0.00")
                'txtNetAmount.Text = Val(lblNetTotal.Text)
                txtNetAmount.Text = Val(lblNetValue.Text)
            ElseIf optInclusive.Checked = True Then
                '    lblSalesType.Text = "INCLUSIVE SALES"
                If dGSTPerc > 0 Then
                    nGSTAmount = (Val(lblNetValue.Text) * dGSTPerc) / (100 + dGSTPerc)
                Else
                    nGSTAmount = 0
                End If
                lblTax.Text = Format(nGSTAmount, "0.00")
                lblNetTotal.Text = Format(Val(lblNetValue.Text), "0.00")
                txtNetAmount.Text = Val(lblNetTotal.Text)
            ElseIf optZeroRated.Checked = True Then
                '    lblSalesType.Text = "ZERO RATED SALES"
                lblTax.Text = Format(0, "0.00")
                lblNetTotal.Text = Format(Val(lblNetValue.Text), "0.00")
                txtNetAmount.Text = Val(lblNetTotal.Text)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub txtInventoryCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtInventoryCode.KeyDown
        Try
            'If bedited = True And e.KeyCode = Keys.Delete Then
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

            Select Case e.KeyCode

                Case Keys.F1
                    pnlFindCost.Visible = True
                    If txtFindInventory.Enabled Then txtFindInventory.Focus()

                Case Keys.F6 'reprint

                    If Trim(txtSalesManPassword.Text) <> "" Then
                        subGetSalesManPassword()
                    Else
                        Exit Sub
                    End If
                    If UCase(sCompanyShortCode) = "HN" And (UCase(Objuser.CompanyName)) = "HANIFFA PTE LTD" Then
                        subGetAdminPasword()
                    Else
                        If Trim(txtSalesManName.Text) <> "" Then
                            subReprint()
                        End If
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

                    If UCase(sCompanyShortCode) = "HN" And (UCase(Objuser.CompanyName)) = "HANIFFA PTE LTD" Then
                        subGetBCAdminPasword()
                    Else
                        If Trim(txtSalesManName.Text) <> "" Then
                            subBillCancel()
                        End If
                    End If

                Case Keys.F4 'save or tender
                    If Trim(txtSalesManPassword.Text) <> "" Then
                        subGetSalesManPassword()
                    Else
                        Exit Sub
                    End If
                    If Trim(txtSalesManName.Text) <> "" Then
                        subTender()
                    End If
                Case Keys.F5 'Net Total
                    If Val(lblSalesAmount.Text) > 0 Then
                        subNetDiscount()
                    End If
                Case Keys.F7 'Net Total
                    If Val(txtPaidAmount.Text) > 0 Then
                        subSave()
                    End If
                Case Keys.F8 ' PayMode clear
                    If grdSales.RowCount > 0 Then
                        If (MsgBox("Do you still want to clear Paymode? ", vbYesNo)) = vbNo Then
                            Exit Sub
                        End If
                    End If
                    subPayClear()

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
    Private Sub subNetDiscount()
        Try
            If grdSales.RowCount > 1 Then
                ' PnlNetAmount.Visible = True
                'txtNetDiscountPercentage.SetFocus
                txtNetAmount.Visible = True
                txtNetAmount.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub subLoadPayMode()
        Dim dsPay As New DataSet

        Dim stest As String
        Try
            dsPay = objPay.GetPayMode()
            grdPaymode.RowCount = 0
            If dsPay.Tables(0).Rows.Count > 0 Then
                RecCount = dsPay.Tables(0).Rows.Count
                For iCnt = 0 To dsPay.Tables(0).Rows.Count - 1
                    grdPaymode.Rows.Add()
                    fnSetdgvText(grdPaymode, iCnt, LblPaymode - 1, Trim(dsPay.Tables(0).Rows(iCnt)("PayMode") & ""))

                    'Consider the commission 
                    arrpay(iCnt, 0) = Val(dsPay.Tables(0).Rows(iCnt)("Commission") & "")
                    arrpay(iCnt, 1) = 0
                    arrpay(iCnt, 2) = 0
                    arrpay(iCnt, 4) = ""
                    arrpay(iCnt, 3) = Trim(dsPay.Tables(0).Rows(iCnt)("PayMode") & "")
                    'Call SetObjImage(grdPaymode, iCnt, ImgPaymode, Trim(dsPay.Tables(0).Rows(iCnt)("Position") & ""))

                Next
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Public Sub subPayModeGridSettings(ByVal Grd As Object)
        Try
            Grd.BlockMode = False
            Grd.ShadowColor = System.Drawing.Color.Orange
            Grd.FontSize = 14
            Grd.FontName = "Arial"
            Grd.FontBold = True
            Grd.ShadowText = Color.Black
            Grd.SelBackColor = Color.BlanchedAlmond
            Grd.SelForeColor = Color.Navy
            Grd.GridColor = Color.Orange
            Grd.BorderStyle = FPSpread.BorderStyleConstants.BorderStyleFixedSingle
            Grd.GridShowHoriz = True
            Grd.GridShowVert = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Sub subGridSettings(ByVal Grd As Object)
        Try
            Grd.BlockMode = False
            Grd.ShadowColor = System.Drawing.Color.MediumAquamarine
            Grd.FontSize = 9
            Grd.FontName = "Arial"
            Grd.FontBold = True
            Grd.ShadowText = Color.Black
            Grd.SelBackColor = Color.BlanchedAlmond
            Grd.SelForeColor = Color.Navy
            Grd.GridColor = Color.Orange
            Grd.BorderStyle = FPSpread.BorderStyleConstants.BorderStyleFixedSingle
            Grd.GridShowHoriz = True
            Grd.GridShowVert = False

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    'Private Sub grdPaymode_KeyUpEvent(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_KeyUpEvent)
    '    Try
    '        If grdPaymode.ActiveCol = TxtAmount Then
    '            subTxtAmount_Keyup(grdPaymode.ActiveRow, e.keyCode)
    '        ElseIf grdPaymode.ActiveCol = TxtSlipNo Then
    '            txtSlipNo_KeyUp(grdPaymode.ActiveRow, e.keyCode)
    '        End If
    '        If sLoad = "1" Then
    '            SendKeys.Send("{TAB}")
    '            SendKeys.Send("{TAB}")
    '            sLoad = ""
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    'End Sub
    Private Sub subTxtAmount_Keyup(ByVal iIndex As Integer, ByVal KeyCode As Integer)
        Try
            Dim iCnt As Integer
            If (bTradeinFlag = False) Or (bTradeinFlag = True And dTradeinBalance > 0) Then


                Dim dComm As Double
                Dim sFormula As String
                If KeyCode = 13 Then



                    txtPaidAmount.Text = Format$(Val(txtPaidAmount.Text) - arrpay(iIndex - 1, 1), "0.00")
                    txtBalanceAmount.Text = Format$(Val(txtBalanceAmount.Text) + arrpay(iIndex - 1, 1), "0.00")
                    txtCommission.Text = Format$(Val(txtCommission.Text) - arrpay(iIndex - 1, 2), "0.00")

                    arrpay(iIndex - 1, 1) = Val(fnGetdgvText(grdPaymode, iIndex, TxtAmount)) / Val(1 + arrpay(iIndex - 1, 0) / 100)
                    arrpay(iIndex - 1, 2) = Val(fnGetdgvText(grdPaymode, iIndex, TxtAmount)) - arrpay(iIndex - 1, 1)

                    'new value
                    txtPaidAmount.Text = Format$(Val(txtPaidAmount.Text) + arrpay(iIndex - 1, 1), "0.00")
                    txtCommission.Text = Format$(Val(txtCommission.Text) + arrpay(iIndex - 1, 2), "0.00")
                    txtBalanceAmount.Text = Format$(IIf((Val(txtBalanceAmount.Text) - arrpay(iIndex - 1, 1)) < 0, 0, (Val(txtBalanceAmount.Text) - arrpay(iIndex - 1, 1))), "0.00")

                    If Val(txtPaidAmount.Text) >= Val(txtTotalAmount.Text) Then
                        txtReturnChange.Text = Format(System.Math.Abs(Val(txtPaidAmount.Text) - Val(txtTotalAmount.Text)), "#0.00")
                    End If
                    If Trim(arrpay(iIndex - 1, 3)) = "CASH" And arrpay(iIndex - 1, 1) >= Val(txtBalanceAmount.Text) Then
                        arrpay(iIndex - 1, 1) = Format(Val(arrpay(iIndex - 1, 1)) - Val(txtReturnChange.Text), "0.00")
                    End If

                    If Trim(fnGetdgvText(grdPaymode, iIndex, LblPaymode)) <> "CASH" And Trim(fnGetdgvText(grdPaymode, iIndex, LblPaymode)) <> "FOREIGN" And Val(fnGetdgvText(grdPaymode, iIndex, TxtAmount)) > 0 Then
                        'grdPaymode.Col = grdPaymode.ActiveCol + 1
                        'grdPaymode.Action = FPSpread.ActionConstants.ActionActiveCell

                    Else
                        If Val(txtPaidAmount.Text) < Val(txtTotalAmount.Text) Then
                            SendKeys.Send("{DOWN}")
                        Else
                            btnSave.Focus()
                        End If
                    End If
                ElseIf KeyCode = 40 Then
                    If arrpay(iIndex - 1, 1) = 0 Then
                        SetText(grdPaymode, iIndex, TxtAmount, "")
                    End If
                    If iIndex < RecCount Then
                        'grdPaymode.Row = grdPaymode.ActiveRow + 1
                        'grdPaymode.Col = TxtAmount
                        'grdPaymode.Action = FPSpread.ActionConstants.ActionActiveCell
                        'SendKeys.Send("{DOWN}")
                    Else
                        'grdPaymode.Col = TxtAmount
                        'grdPaymode.Row = 0

                    End If
                ElseIf KeyCode = 38 Then
                    If arrpay(iIndex - 1, 1) = 0 Then
                        SetText(grdPaymode, iIndex, TxtAmount, "")
                    End If
                    'If iIndex > 0 Then
                    '    'SendKeys.Send("{UP}")
                    '    grdPaymode.Row = grdPaymode.ActiveRow - 1
                    '    grdPaymode.Col = TxtAmount
                    '    grdPaymode.Action = FPSpread.ActionConstants.ActionActiveCell
                    'Else
                    '    grdPaymode.Row = RecCount
                    '    grdPaymode.Col = TxtAmount

                    'End If
                End If
                If Val(txtBalanceAmount.Text) = 0 And txtPaidAmount.Text >= txtTotalAmount.Text And Trim(GetText(grdPaymode, iIndex, LblPaymode)) = "CASH" And bAmountFlag = False Then
                    '        Me.KeyPreview = True
                    bAmountFlag = True
                    '        cmdSave.Enabled = True
                    'If Trim(arrpay(Index, 3)) = "CASH" And arrpay(Index, 1) > Val(txtTotalAmount) Then
                    '            arrpay(Index, 1) = Format(Val(txtTotalAmount), "0.00")
                    '
                    '         End If
                    '  btnSave.Focus()
                    '    Else
                    '    txtAmount(Index + 1).SetFocus
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    'Private Sub txtSlipNo_KeyUp(ByVal iIndex As Integer, ByVal KeyCode As Integer)
    '    If KeyCode = 13 Then
    '        If iIndex < RecCount Then
    '            grdPaymode.Row = grdPaymode.ActiveRow + 1
    '            grdPaymode.Col = TxtAmount
    '            grdPaymode.Action = FPSpread.ActionConstants.ActionActiveCell
    '        End If
    '    ElseIf KeyCode = 40 Then
    '        If iIndex < RecCount Then
    '            grdPaymode.Row = grdPaymode.ActiveRow + 1
    '            grdPaymode.Col = TxtSlipNo
    '            grdPaymode.Action = FPSpread.ActionConstants.ActionActiveCell
    '            'TxtSlipNo(Index + 1).SetFocus()
    '        Else
    '            grdPaymode.Row = 1
    '            grdPaymode.Col = TxtSlipNo
    '            grdPaymode.Action = FPSpread.ActionConstants.ActionActiveCell
    '            'TxtSlipNo(0).SetFocus()
    '        End If
    '    ElseIf KeyCode = 38 Then
    '        If iIndex > 0 Then
    '            grdPaymode.Row = grdPaymode.ActiveRow - 1
    '            grdPaymode.Col = TxtSlipNo
    '            grdPaymode.Action = FPSpread.ActionConstants.ActionActiveCell
    '            'TxtSlipNo(Index - 1).SetFocus()
    '        Else
    '            grdPaymode.Row = RecCount
    '            grdPaymode.Col = TxtSlipNo
    '            grdPaymode.Action = FPSpread.ActionConstants.ActionActiveCell
    '            'TxtSlipNo(RecCount).SetFocus()
    '        End If
    '    End If
    'End Sub
    Private Sub txtSlipNo_LostFocus(ByVal iIndex As Integer)
        Try
            arrpay(iIndex - 1, 4) = GetText(grdPaymode, iIndex, TxtSlipNo)
            If Val(txtPaidAmount.Text) >= Val(txtTotalAmount.Text) Then
                '   btnSave.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtSlipNo_Validate(ByVal iIndex As Integer)
        Try
            arrpay(iIndex - 1, 4) = Trim(GetText(grdPaymode, iIndex, TxtSlipNo))
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub grdPaymode_LeaveCell(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_LeaveCellEvent)
        Try

            If e.newCol <> -1 Then
                If e.row > 0 And e.col = TxtAmount Then
                    subTxtAmount_Lostfocus()
                End If
                'got focus    
                If e.newRow > 0 And e.newRow <> e.row And e.newCol = TxtAmount Then
                    subTxtAmount_Gotfocus(e.newRow)
                End If


                'Slip no Lostfocus
                If e.row > 0 And e.col = TxtSlipNo Then
                    txtSlipNo_LostFocus(e.row)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subTxtAmount_Lostfocus()
        Try
            Dim iX As Long
            Dim dAmt As Double
            Dim dComm As Double

            dAmt = 0

            For iX = 0 To RecCount - 1
                arrpay(iX, 1) = Val(GetText(grdPaymode, iX + 1, TxtAmount)) / Val(1 + arrpay(iX, 0) / 100)
                dAmt = dAmt + Val(arrpay(iX, 1))
                arrpay(iX, 2) = Val(GetText(grdPaymode, iX + 1, TxtAmount)) - arrpay(iX, 1)
                dComm = dComm + arrpay(iX, 2)
            Next iX

            txtPaidAmount.Text = Format$(dAmt, "0.00")
            txtBalanceAmount.Text = Format(Val(txtTotalAmount.Text) - Val(txtPaidAmount.Text), "#####0.00")
            If Val(txtBalanceAmount.Text) < 0 Then txtBalanceAmount.Text = "0.00"
            txtCommission.Text = Format$(dComm, "0.00")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subTxtAmount_Gotfocus(ByVal iIndex As Integer)
        Try

            If (bTradeinFlag = False) Or (bTradeinFlag = True And dTradeinBalance > 0) Then
                If arrpay(iIndex - 1, 1) + arrpay(iIndex - 1, 2) <= 0 Then

                    SetText(grdPaymode, iIndex, TxtAmount, Format(Val(txtBalanceAmount.Text) + (Val(txtBalanceAmount.Text) * arrpay(iIndex - 1, 0) / 100), "0.00"))
                Else

                    SetText(grdPaymode, iIndex, TxtAmount, Format(arrpay(iIndex - 1, 1) + arrpay(iIndex - 1, 2), "0.00"))
                End If


                If Trim(GetText(grdPaymode, iIndex, LblPaymode)) <> "CASH" And Trim(GetText(grdPaymode, iIndex, LblPaymode)) <> "FOREIGN" Then
                    'grdPaymode.Col = TxtAmount
                    'grdPaymode.Row = iIndex
                    'grdPaymode.Value()
                    'settext(grdpaymode,iTxtAmount(Index).MaxValue = Val(TxtAmount(Index))
                End If

            Else
                'TxtAmount(Index).Enabled = False
            End If
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
            If Val(lblSalesAmount.Text) = 0 And UCase(sSalesType) <> "TRADEIN" Then
                MsgBox("Enter items for sales! ", vbCritical)
                If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
                fncValidate = False
                Exit Function
            End If
            If grdSales.RowCount <= 0 Then
                MsgBox("Enter items for sales! ", vbCritical)
                If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
                fncValidate = False
                Exit Function
            End If
            If Val(lblSalesAmount.Text) < 0 Then
                MsgBox("Can not do negative sales. ", vbCritical)
                If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
                fncValidate = False
                Exit Function
            End If
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
                'dNetTotalMarketPrice = CalcMinimumPriceForML()
            Else
                dNetTotalMarketPrice = CalcMarketRate()
            End If

            If UCase(sCompanyShortCode) <> "HN" Then
                If dNetTotalMarketPrice <= 0 Then
                    CheckMarketCost = False
                    Exit Function
                End If
            End If
            If Val(lblNetTotal.Text) >= 0 Then
                '''Block By Durai\
                If UCase(sCompanyShortCode) = "HN" And sSalesType <> "TRADEIN" And optZeroRated.Checked = False Then
                    If (Val(lblNetTotal.Text) < dNetTotalMarketPrice) Then
                        sMsg = MsgBox("Bill total: $" & Format(Val(lblNetTotal.Text), "#0.00") & " is less than the Cost ", vbInformation)
                        If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
                        Exit Function
                    End If

                End If
                CheckMarketCost = True
                ''If grdPaymode.MaxRows > 0 Then
                ''    grdPaymode.Col = TxtAmount
                ''    grdPaymode.Row = 1
                ''    grdPaymode.Text = Format(Val(txtNetAmount.Text), "0.00")
                ''    grdPaymode.Action = FPSpread.ActionConstants.ActionActiveCell
                ''    grdPaymode.Col = 3
                ''    grdPaymode.Text = ""
                ''    grdPaymode.Lock = True
                ''End If

                ''txtTotalAmount.Text = Format(Val(txtNetAmount.Text), "0.00")
                ''txtBalanceAmount.Text = Format(Val(txtNetAmount.Text), "0.00")
                ''grdPaymode.Focus()
                subSave()
            ElseIf Val(lblNetTotal.Text) = 0 And sSalesType = "TRADEIN" Then
                CheckMarketCost = True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function
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
            sSalesDate = Format(CDate(ServerDate), "yyyy") + "/" + Format(CDate(ServerDate), "MM") + "/" + Format(CDate(ServerDate), "dd")
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

    Private Sub frmGPOSNew_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
                    SendKeys.Send("{TAB}")
                Case Keys.Enter
                    SendKeys.Send("{TAB}")

                Case Keys.F2
                    If Trim(UCase(sCompanyShortCode)) = "HN" Then
                        If grdSales.RowCount > 0 Then
                            subClear()
                        End If
                    End If
                Case Keys.F7 'Net Total
                    If Val(txtPaidAmount.Text) > 0 Then
                        subSave()
                    End If
                Case Keys.F8 ' PayMode clear
                    If grdSales.RowCount > 0 Then
                        If (MsgBox("Do you still want to clear Paymode? ", vbYesNo)) = vbNo Then
                            Exit Sub
                        End If
                    End If
                    subPayClear()
                Case Keys.F9 ' Update TradeIn
                    btnTradeInUpdate_Click(sender, e)

            End Select
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
    Private Sub subClear()
        Try

            '''optZeroRated.Enabled = False
            '''optInclusive.Enabled = False
            '''optExclusive.Enabled = False
            optExclusive.Checked = True
            ' txtCustomerCode.Text = "CASH"
            btnCustomerCodeSearch.Enabled = True
            txtCustomerName.Clear()
            '   subGetCustomerDetail()
            txtAddress1.Clear()
            txtAddress2.Clear()
            txtAddress3.Clear()
            txtRemarks.Clear()
            grdSales.RowCount = 0
            lblQty.Text = Format("0")
            lblSalesAmount.Text = Format("0.00")
            lblNetDiscount.Text = Format("0.00")
            lblNetValue.Text = Format("0.00")
            lblTax.Text = Format("0.00")
            lblNetTotal.Text = Format("0.00")
            lblSManTotal.Text = "0"
            lblNextInvoiceNo.Text = ""
            subClearDetail()
            subPayClear()
            txtNetAmount.Text = Format(0, "0.00")
            '    mdTotalCommission = 0
            dPayModeCommission = 0
            '  txtPMSurCharge.Text = "0"
            '   txtPayMode.Clear()
            txtSalesManPassword.Text = ""
            txtSalesManName.Text = ""
            txtCustomerCode.Text = ""
            txtTealGST.Text = ""
            sSalesType = "CASH"
            optNormalSales.Checked = True
            txtSalesManPassword.Focus()


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnCustomerCodeSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomerCodeSearch.Click
        Try
            Dim frmHelpCustomer As New frmHelpSearch(0, 0, "tblCustomer", "R", 4, "CustomerName", "", "CustomerCode", "CustomerCode", 80, "CustomerName", "CustomerName", 150, "Address1", "Address1", 90, "Address2", "Address2", 80)
            frmHelpCustomer.ShowDialog()
            txtCustomerCode.Text = frmHelpCustomer.Var1.Trim
            txtCustomerName.Text = frmHelpCustomer.Var2.Trim
            subGetCustomerDetail()
            sCustomerCode = Trim(txtCustomerCode.Text)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subGetCustomerDetail()
        Dim dsCust As New DataSet
        Try
            If txtCustomerCode.Text.Trim <> "" Then
                sCustomerCode = Trim(txtCustomerCode.Text.Trim)
                dsCust = objCust.GetCustomerwithCodeHQ(txtCustomerCode.Text.Trim)
                If dsCust.Tables(0).Rows.Count > 0 Then
                    txtCustomerName.Text = Trim(dsCust.Tables(0).Rows(0)("CustomerName") & "")
                    txtAddress1.Text = Trim(dsCust.Tables(0).Rows(0)("Address1") & "")
                    txtAddress2.Text = Trim(dsCust.Tables(0).Rows(0)("Address2") & "")
                    txtAddress3.Text = Trim(dsCust.Tables(0).Rows(0)("Address3") & "")
                    '''  If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
                    If txtTealGST.Enabled Then txtTealGST.Focus()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsCust = Nothing
        End Try
    End Sub

    Private Sub btnNewCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewCustomer.Click
        Dim frmCust As New frmCustomer
        frmCust.ShowDialog()
        frmCust = Nothing
    End Sub

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

    Private Sub subBillCancel()
        Try
            pnlBillCancel.Visible = True
            txtBillCancelBillNo.Clear()
            txtBillCancelBillNo.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
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
    Private Sub btnBCAdminPwdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBCAdminPwdCancel.Click
        Try
            txtBCAdminPassword.Clear()
            pnlBillCancelAdminPwd.Visible = False
            If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
            bPassword = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnAdminPwdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdminPwdCancel.Click
        Try
            txtAdminPassword.Clear()
            pnlAdminPwd.Visible = False
            If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
            bPassword = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnBillCancelOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBillCancelOK.Click
        Dim dsSal As New DataSet
        Dim dsIns As New DataSet
        Dim sMsg As String
        Try
            dsSal = objSalesReturn.GetCreditSalesHeaderByInvoiceNo(Trim(txtBillCancelBillNo.Text))
            If dsSal.Tables(0).Rows.Count > 0 Then
                If DateValue(dsSal.Tables(0).Rows(0)("BDate") & "") = DateValue(Trim(ServerDate)) Then
                    'If DateValue(DsHdr.Tables("Invoice").Rows(0)("InvoiceDate") & "") = DateValue(Date.Now) Then
                    'check whether paid installment
                    dsIns = objIns.GetInstallmentDetail(Trim(dsSal.Tables(0).Rows(0)("InsAgreementNo")))
                    If dsIns.Tables(0).Rows.Count > 0 Then
                        sMsg = MsgBox("Installment Paid for this Invoice..Do you want to CANCEL the bill No. " & Trim(txtBillCancelBillNo.Text) & " ? ", vbYesNo)
                    Else
                        sMsg = MsgBox("Do you want to CANCEL the bill No. " & Trim(txtBillCancelBillNo.Text) & " ? ", vbYesNo)
                    End If
                    If sMsg = vbYes Then
                        iRet = objSalesReturn.BillCancel(Trim(txtBillCancelBillNo.Text), Trim(sLocationCode), sUserID)
                        pnlBillCancel.Visible = False
                        If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
                        Exit Sub
                    End If
                Else
                    MessageBox.Show("You cannot cancel History Bills ....")
                    pnlBillCancel.Visible = False
                    If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
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
            If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub pnlBillCancelAdminPwd_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlBillCancelAdminPwd.Paint

    End Sub

    Private Sub btnReprintOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReprintOK.Click
        Dim sDate As Date
        Dim strDate As String
        Dim dsInvoice As New DataSet
        Try
            If Trim(txtRePrintBillNo.Text) <> "" Then
                bReprint = True

                dsInvoice = objSales.GetCreditSalesHeaderByInvoiceNo(Trim(txtRePrintBillNo.Text))
                If dsInvoice.Tables(0).Rows.Count > 0 Then
                    sDate = dsInvoice.Tables(0).Rows(0)("InvoiceDate") & ""
                    strDate = Format(CDate(sDate), "yyyy/MM/dd")
                    'sDate = dsInvoice.Tables(0).Rows(0)("InvoiceDate")
                    'strDate = Mid(sDate, 7, 4)
                    'strDate = strDate & Mid(sDate, 4, 2)
                    'strDate = strDate & Mid(sDate, 1, 2)
                    ' Shabeer 2012 05 17
                    If UCase(sCompanyShortCode) = "HN" And (UCase(Objuser.CompanyName)) = "HANIFFA PTE LTD" Then
                        'If ServerDate = CDate(Trim(strDate)) Then
                        Call subPrintBill(Trim(txtRePrintBillNo.Text))
                        bReprint = False
                        PnlReprint.Visible = False
                        subClear()
                        'Else
                        '    subGetAdminPasword()
                        'End If
                    Else
                        'If ServerDate = CDate(Trim(strDate)) Then
                        Call subPrintBill(Trim(txtRePrintBillNo.Text))
                        bReprint = False
                        PnlReprint.Visible = False
                        subClear()
                        '    Else
                        '    subGetAdminPasword()
                        'End If
                    End If
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
    Private Sub subPrintBill(ByVal sInvoiceNo As String)
        Dim ReportTitle As String
        Dim sSelection As String
        Dim sSalesType$
        Dim sReply As String
        Dim dsSales As New DataSet
        Dim frmrep As New frmReports
        Dim CR As New ReportDocument
        Dim j As Integer
        Try
            Cursor = Cursors.WaitCursor
            dsSales = objSales.GetCreditSalesHeaderByInvoiceNo(Trim(sInvoiceNo))

            If dsSales.Tables(0).Rows.Count > 0 Then sSalesType = Trim$(dsSales.Tables(0).Rows(0)("SalesType") & "")

            If UCase(sCompanyShortCode) = "HN" Then
                Select Case sSalesType
                    Case "TRADEIN"
                        strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_HN.rpt"
                        CR.Load(strpath)
                        ReportLogOnToServer(CR)
                        CR.Refresh()
                        subGetClassRate()
                        frmrep.ReportViewer.ReportSource = CR
                        sSelection = "{tblCreditSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
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
                        sSelection = "{tblCreditSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
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
                        ' strpath = Application.StartupPath & "\Reports\rptSalesInvoice_HN.rpt"
                        ' strpath = Application.StartupPath & "\Reports\rptCreditSalesInvoice_HN.rpt"


                        If UCase(sCompanyShortCode) = "MJ" Then
                            '   strpath = Application.StartupPath & "\Reports\rptSalesInvoice_HN.rpt"
                            strpath = Application.StartupPath & "\Reports\rptCreditSalesInvoice_MJ.rpt"
                        ElseIf UCase(sCompanyShortCode) = "MD" Then
                            '   strpath = Application.StartupPath & "\Reports\rptSalesInvoice_HN.rpt"
                            strpath = Application.StartupPath & "\Reports\rptCreditSalesInvoice_MD.rpt"
                        End If
                        CR.Load(strpath)
                        ReportLogOnToServer(CR)
                        CR.Refresh()
                        subGetClassRate()
                        frmrep.ReportViewer.ReportSource = CR
                        sSelection = "{tblCreditSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
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
                ' ''strpath = Application.StartupPath & "\Reports\rptCreditSalesInvoice_MJ.rpt"
                ' ''CR.Load(strpath)
                ' ''ReportLogOnToServer(CR)
                ' ''CR.Refresh()
                ' ''subGetClassRate()
                ' ''frmrep.ReportViewer.ReportSource = CR
                ' ''sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                ' ''frmrep.ReportViewer.SelectionFormula = sSelection
                ' ''CR.DataDefinition.RecordSelectionFormula = sSelection
                ' ''If UCase(sCompanyShortCode) = "MJ" And bReprint Then
                ' ''    CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                ' ''End If
                ' ''CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                ' ''CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                ' ''CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                ' ''CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                ' ''CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                ' ''CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                ' ''CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s24K999)
                ' ''CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                ' ''CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s20K850)
                ' ''CR.DataDefinition.FormulaFields("Rate4").Text = fncFormatSQLText(s22KSOV)
                ' '' ''CR.PrintOptions.PaperSize = PaperSize.DefaultPaperSize
                ' ''CR.PrintToPrinter(1, True, 0, 0)

                ' '' ''frmrep.ReportViewer.ReportSource = CR
                ' '' ''frmrep.ShowDialog()
                ' '' ''frmrep.ReportViewer.Visible = True
                ' '' ''frmrep.ReportViewer.CloseView(False)
                ' '' ''frmrep.WindowState = FormWindowState.Maximized
                ' ''frmrep = Nothing
                ' ''frmrep = New frmReports
                ''' End Select
                ''' 
                If UCase(sCompanyShortCode) = "MJ" Then
                    '   strpath = Application.StartupPath & "\Reports\rptSalesInvoice_HN.rpt"
                    If chkExport.Checked = True Then
                        strpath = Application.StartupPath & "\Reports\rptCreditSalesExportInvoice_MJ.rpt"
                    Else
                        strpath = Application.StartupPath & "\Reports\rptCreditSalesInvoice_MJ.rpt"
                    End If

                ElseIf UCase(sCompanyShortCode) = "MD" Then
                    '   strpath = Application.StartupPath & "\Reports\rptSalesInvoice_HN.rpt"
                    strpath = Application.StartupPath & "\Reports\rptCreditSalesInvoice_MD.rpt"
                End If
                CR.Load(strpath)
                ReportLogOnToServer(CR)
                CR.Refresh()
                subGetClassRate()
                frmrep.ReportViewer.ReportSource = CR
                sSelection = "{tblCreditSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
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
                        Case "TRADEIN"
                            If sCompanyShortCode = "ES" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_ESP.rpt"
                            ElseIf sCompanyShortCode = "AS" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_AS.rpt"
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
                            If sCompanyShortCode = "ES" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInDetailInvoice_ESP.rpt"
                            End If

                            If ((UCase(sCompanyShortCode) <> "HN") And (UCase(sCompanyShortCode) <> "AS") And (UCase(sCompanyShortCode) <> "ML")) Then
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
                                strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AS.rpt"
                            ElseIf sCompanyShortCode = "ML" Then
                                strpath = Application.StartupPath & "\Reports\rptSalesInvoice_ML.rpt"
                            ElseIf UCase(sCompanyShortCode) = "GMT" Then
                                strpath = Application.StartupPath & "\Reports\rptSalesInvoice_GMT.rpt"
                            ElseIf UCase(sCompanyShortCode) = "HN" Then
                                '   strpath = Application.StartupPath & "\Reports\rptSalesInvoice_HN.rpt"
                                strpath = Application.StartupPath & "\Reports\rptCreditSalesInvoice_HN.rpt"
                            ElseIf UCase(sCompanyShortCode) = "MD" Then
                                '   strpath = Application.StartupPath & "\Reports\rptSalesInvoice_HN.rpt"
                                strpath = Application.StartupPath & "\Reports\rptCreditSalesInvoice_MD.rpt"
                            End If
                            CR.Load(strpath)
                            ReportLogOnToServer(CR)
                            CR.Refresh()
                            subGetClassRate()
                            frmrep.ReportViewer.ReportSource = CR
                            sSelection = "{tblCreditSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
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
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_AS.rpt"
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
                                strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AS.rpt"
                            ElseIf sCompanyShortCode = "ML" Then
                                strpath = Application.StartupPath & "\Reports\rptSalesInvoice_ML.rpt"
                            ElseIf UCase(sCompanyShortCode) = "GMT" Then
                                strpath = Application.StartupPath & "\Reports\rptSalesInvoice_GMT.rpt"
                            ElseIf UCase(sCompanyShortCode) = "HN" Then
                                ' strpath = Application.StartupPath & "\Reports\rptSalesInvoice_HN.rpt"
                                strpath = Application.StartupPath & "\Reports\rptCreditSalesInvoice_HN.rpt"

                            ElseIf UCase(sCompanyShortCode) = "MD" Then
                                ' strpath = Application.StartupPath & "\Reports\rptSalesInvoice_HN.rpt"
                                strpath = Application.StartupPath & "\Reports\rptCreditSalesInvoice_MD.rpt"
                            End If
                            CR.Load(strpath)
                            ReportLogOnToServer(CR)
                            CR.Refresh()
                            frmrep.ReportViewer.ReportSource = CR
                            subGetClassRate()
                            sSelection = "{tblCreditSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
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
                        s24K999 = Format(Val(dsClass.Tables(0).Rows(i)("Rate") & ""), "#0.00")
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

    Private Sub btnPayClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPayClear.Click
        subPayClear()
    End Sub
    Private Sub subPayClear()
        Try
            subLoadPayMode()
            txtTotalAmount.Text = "0.00"
            txtPaidAmount.Text = "0.00"
            txtCommission.Text = "0.00"
            txtBalanceAmount.Text = "0.00"
            txtReturnChange.Text = "0.00"
            If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        subSave()
    End Sub
    Private Sub subSave()
        Dim bSave As Boolean
        Dim objPM As New frmPayingmode
        Try
            bSave = False
            If optNormalSales.Checked = True Then
                sSalesType = "CASH"
                dPaidAmount = Val(txtPaidAmount.Text)
                ReturnAmount = Val(txtReturnChange.Text)

                If dPaidAmount < Val(txtTotalAmount.Text) Then
                    MsgBox("Paid Amount Should be greater than equal to Total Sales Amount")
                    Exit Sub
                End If

                If dPaidAmount >= Val(txtTotalAmount.Text) And fnccheckPaid() Then

                    objSales.subOpenSalesConnection()
                    Try
                        objSales.subBeginSalesTransaction()
                        If fncGetInvoiceNo() = False Then
                            MessageBox.Show("Transaction failed!!!")
                            objSales.subRollBackSalesTransaction()
                            Exit Try
                        End If
                        
                        If Not objSales.fncUpdateNextCreditInvoiceNo() Then
                            MessageBox.Show("Transaction failed!!!")
                            objSales.subRollBackSalesTransaction()
                            Exit Try
                        End If
                        'If Not fncPaymodeDetailSave(sInvoiceNo) Then
                        '    MessageBox.Show("Transaction not saved")
                        '    objSales.subRollBackSalesTransaction()
                        '    objPM.Visible = False
                        '    Exit Try
                        'End If
                        'If Not fncSaveTradeInSales(sInvoiceNo) Then
                        '    MessageBox.Show("Transaction not saved")
                        '    objSales.subRollBackSalesTransaction()
                        '    objPM.Visible = False
                        '    Exit Try
                        'End If
                        If Not fncSales(sInvoiceNo) Then
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
                    objPM.Visible = False
                    Exit Sub
                End If
            ElseIf optTradeIn.Checked = True Then
                dPaidAmount = Val(txtPaidAmount.Text)
                sSalesType = "TRADEIN"
                objPM.SalesType = sSalesType
                '  If mbSalesTransaction = True Then

                ReturnAmount = Val(txtReturnChange.Text)

                If dPaidAmount < Val(txtTotalAmount.Text) Then
                    MsgBox("Paid Amount Should be greater than equal to Total Sales Amount")
                    Exit Sub
                End If

                If bTradeinFlag = True And fnccheckPaid() Then
                    objSales.subOpenSalesConnection()
                    Try
                        objSales.subBeginSalesTransaction()
                        ''If fncGetInvoiceNo() = False Then
                        ''    MessageBox.Show("Transaction failed!!!")
                        ''    objSales.subRollBackSalesTransaction()
                        ''    Exit Try
                        ''End If
                        ''If Not objSales.fncUpdateNextInvoiceNo() Then
                        ''    MessageBox.Show("Transaction failed!!!")
                        ''    objSales.subRollBackSalesTransaction()
                        ''    Exit Try
                        ''End If
                        ''If Not fncPaymodeDetailSave(sInvoiceNo) Then
                        ''    MsgBox("Transaction not saved")
                        ''    objSales.subRollBackSalesTransaction()
                        ''    objPM.Visible = False
                        ''    Exit Try
                        ''Else
                        ''    bTradeinFlag = False
                        ''    dTradeinBalance = 0
                        ''End If
                        ''If Not fncSales(sInvoiceNo) Then
                        ''    MessageBox.Show("Transaction not saved")
                        ''    objSales.subRollBackSalesTransaction()
                        ''    objPM.Visible = False
                        ''    Exit Try
                        ''End If
                        ''If Not fncUpdateTradeInRefNo() Then
                        ''    MessageBox.Show("Transaction not saved")
                        ''    objSales.subRollBackSalesTransaction()
                        ''    objPM.Visible = False
                        ''    Exit Try
                        ''End If
                        If fncGetInvoiceNo() = False Then
                            MessageBox.Show("Transaction failed!!!")
                            objSales.subRollBackSalesTransaction()
                            Exit Try
                        End If
                        If Not objSales.fncUpdateNextInvoiceNo() Then
                            MessageBox.Show("Transaction failed!!!")
                            objSales.subRollBackSalesTransaction()
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
                        If Not fncUpdateTradeInRefNo() Then
                            MessageBox.Show("Transaction not saved")
                            objSales.subRollBackSalesTransaction()
                            objPM.Visible = False
                            Exit Try
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
                    objPM.Visible = False
                End If
                'End If
            End If


            If bSave = True Then
                Call subPrintBill(Trim(sCreditInvoiceNo))
                If optInstallment.Checked = True Then
                    MessageBox.Show("Please take note the Sales No - " & sInvoiceNo & " and " & _
                                " Installment Agreement No - " & gsInsAgreementNo & ".")
                Else
                    ' MsgBox("Please take note the Sales no " + sInvoiceNo)
                End If
                objPM.SalesType = ""
                ' SubSayEnglish("THANK YOU PLEASE COME AGAIN ")
                subClear()
                txtSalesManPassword.Text = ""
                txtSalesManName.Text = ""
                If txtSalesManPassword.Enabled = True Then txtSalesManPassword.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Function fncGetInvoiceNo() As Boolean
        Dim dsComp As New DataSet
        Dim dsInv As New DataSet
        Dim dsCrInv As New DataSet
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
            'dsInv = objSales.GetUpdateInvoice()
            'If dsInv.Tables(0).Rows.Count > 0 Then
            '    sInvoiceNo = Trim(dsInv.Tables(0).Rows(0)("NextInvoice") & "")
            '    fncGetInvoiceNo = True
            'Else
            '    fncGetInvoiceNo = False
            'End If
            dsCrInv = objSales.GetUpdateCreditInvoice()
            If dsCrInv.Tables(0).Rows.Count > 0 Then
                sCreditInvoiceNo = Trim(dsCrInv.Tables(0).Rows(0)("NextCreditInvoice") & "")
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
    Private Function fncPaymodeDetailSave(ByVal sInvoiceNo As String) As Boolean
        Try
            Dim bPayModeFlag As Boolean
            Dim sDate As String
            Dim iCnt As Long

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
            Next iCnt

            If (bTradeinFlag = True And dTradeinBalance <= 0) And bPayModeFlag = False Then
                iCnt = 0
                iRet = objSales.PayModeDetailSave(sInvoiceNo, sDate, Trim(arrpay(iCnt, 3)), _
                                             Val(arrpay(iCnt, 1)), Val(arrpay(iCnt, 2)), _
                                             Trim(arrpay(iCnt, 4)), Trim(sSalesType))
                bPayModeFlag = True

                If Not iRet Then
                    fncPaymodeDetailSave = False
                    Exit Function
                End If

            End If

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
        Dim bCreditSalesDetail As Boolean
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
            '      txtPMSurCharge.Text = dPayModeCommission

            'dTotalWeight = GetText(grdSales, grdSales.MaxRows, IWeight)
            'dTotalGoldLoss = GetText(grdSales, grdSales.MaxRows, IGoldLoss)
            'dTotalStoneWeight = GetText(grdSales, grdSales.MaxRows, IDSWeight)
            'dTotalSMC = GetText(grdSales, grdSales.MaxRows, ISMC)
            'dTotalPCS = GetText(grdSales, grdSales.MaxRows, IPCS)
            'dTotalValue = GetText(grdSales, grdSales.MaxRows, ITotalValue)
            'dTotalCost = GetText(grdSales, grdSales.MaxRows, ITotalCost)

            dTotalWeight = fnGetdgvText(grdSales, grdSales.RowCount - 1, IWeight)
            dTotalGoldLoss = fnGetdgvText(grdSales, grdSales.RowCount - 1, IGoldLoss)
            dTotalStoneWeight = fnGetdgvText(grdSales, grdSales.RowCount - 1, IDSWeight)
            dTotalSMC = fnGetdgvText(grdSales, grdSales.RowCount - 1, ISMC)
            dTotalPCS = fnGetdgvText(grdSales, grdSales.RowCount - 1, IPCS)
            dTotalValue = fnGetdgvText(grdSales, grdSales.RowCount - 1, ITotalValue)
            dTotalCost = fnGetdgvText(grdSales, grdSales.RowCount - 1, ITotalCost)


            dGST = Format(Val(lblTax.Text), "0.00")
            nDetailDiscount = "0.00"
            dNetDiscount = Format(Val(lblNetDiscount.Text), "0.00")
            nDiscount = Format(Val(nDetailDiscount) + Val(dNetDiscount), "0.00")
            nNetTotal = Format(Val(lblNetTotal.Text), "0.00")
            dNetCommission = Val(txtCommission.Text)
            nNetTotal = nNetTotal + dNetCommission
            dPaidAmount = dPaidAmount + dNetCommission

            If optExclusive.Checked = True Then
                sGSTType = "E"
            ElseIf optInclusive.Checked = True Then
                sGSTType = "I"
            ElseIf optZeroRated.Checked = True Then
                sGSTType = "Z"
            End If

            sSDate = Format(CDate(ServerDate), "yyyy") + "/" + Format(CDate(ServerDate), "MM") + "/" + Format(CDate(ServerDate), "dd")



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
                    End If
                End If
            Next

            ''fncSales = objSales.SalesHeaderSave(Trim(sInvoiceNo), Trim(sSDate), Trim(sCompanyCode), Trim(sLocationCode), Trim(gTerminalCode), _
            ''                    Trim(txtCustomerCode.Text), "", Trim(txtCustomerName.Text), Trim(txtAddress1.Text), _
            ''                    Trim(txtAddress2.Text), Trim(txtAddress3.Text), Trim(sSalesManCode), Trim$(sSalesType), _
            ''                    Val(dTotalWeight) - Val(nTiWeight), Val(dTotalStoneWeight), Val(dTotalGoldLoss), _
            ''                    Val(dTotalSMC), Val(dTotalPCS), Val(dGST), Trim(sGSTType), Val(nDetailDiscount), _
            ''                    Val(dNetDiscount), Val(nDiscount), Val(nNetTotal), _
            ''                    Val(dPaidAmount), Val(ReturnAmount), _
            ''                    Val(dTotalValue), Val(dTotalCost), _
            ''                    -Val(nTiTotalAmt), Val(nTiWeight), _
            ''                    Trim$(gsInsAgreementNo), Val(dNetCommission), Trim(txtRemarks.Text))

            fncSales = objSales.CreditSalesHeaderSave(Trim(sCreditInvoiceNo), Trim(sSDate), Trim(sCompanyCode), Trim(sLocationCode), Trim(gTerminalCode), _
                       Trim(txtCustomerCode.Text), "", Trim(txtCustomerName.Text), Trim(txtAddress1.Text), _
                       Trim(txtAddress2.Text), Trim(txtAddress3.Text), Trim(sSalesManCode), Trim$(sSalesType), _
                       Val(dTotalWeight) - Val(nTiWeight), Val(dTotalStoneWeight), Val(dTotalGoldLoss), _
                       Val(dTotalSMC), Val(dTotalPCS), Val(dGST), Trim(sGSTType), Val(nDetailDiscount), _
                       Val(dNetDiscount), Val(nDiscount), Val(nNetTotal), _
                       Val(dPaidAmount), Val(ReturnAmount), _
                       Val(dTotalValue), Val(dTotalCost), _
                       -Val(nTiTotalAmt), Val(nTiWeight), _
                       Trim$(gsInsAgreementNo), Val(dNetCommission), Trim(txtRemarks.Text))

            If UCase(txtCustomerCode.Text.Trim) = "CASH" And sMemberModule = "1" Then
                fncSales = objSales.MemberTransactionSave("", Trim(sInvoiceNo), Trim(sSDate), Val(nDiscount), Val(dNetDiscount), Val(nNetTotal), Trim(sLocationCode))
            End If
            If UCase(sCompanyShortCode) <> "HN" Then
                Call CalcMarketRate()
                If bPassword Then
                    iRet = objSales.AllowMarketPriceSave(Trim(sInvoiceNo), Trim(sSDate), dDailyClassRate, dNetDiscount, dGST, Val(txtTotalValue.Text), dNetTotalMarketPrice)
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

                        dGST = Format("0.00")
                        dNetAmount = Format("0.00")
                        dActualSales = Format("0.00")
                        If optExclusive.Checked = True Then
                            dGST = Format((Val(fnGetdgvText(grdSales, iCnt, ITotalValue)) - dNetDiscount) * dGSTPerc / (100), "0.0000")
                            dNetAmount = Format((Val(fnGetdgvText(grdSales, iCnt, ITotalValue)) - dNetDiscount + dGST + dCommission), "0.00")
                            dActualSales = Format((Val(fnGetdgvText(grdSales, iCnt, ITotalValue)) - dNetDiscount + dCommission), "0.00")
                        ElseIf optInclusive.Checked = True Then
                            dGST = Format((Val(fnGetdgvText(grdSales, iCnt, ITotalValue)) - dNetDiscount) * dGSTPerc / (dGSTPerc + 100), "0.0000")
                            dNetAmount = Format((Val(fnGetdgvText(grdSales, iCnt, ITotalValue)) - dNetDiscount + dCommission), "0.00")
                            dActualSales = Format((Val(fnGetdgvText(grdSales, iCnt, ITotalValue)) - dNetDiscount - dGST + dCommission), "0.00")

                        ElseIf optZeroRated.Checked = True Then
                            dGST = Format("0.0000")
                            dNetAmount = Format((Val(fnGetdgvText(grdSales, iCnt, ITotalValue)) - dNetDiscount) + dCommission, "0.00")
                            dActualSales = Format((Val(fnGetdgvText(grdSales, iCnt, ITotalValue)) - dNetDiscount + dCommission), "0.00")
                        End If

                        nDetailDiscount = 0



                        sClassCode = Trim(fnGetdgvText(grdSales, iCnt, IClass))
                        sSalesDate = Format(CDate(ServerDate), "yyyy") + "/" + Format(CDate(ServerDate), "MM") + "/" + Format(CDate(ServerDate), "dd")
                        DsRate = objSales.GetMarketRate(sClassCode, sSalesDate)
                        If DsRate.Tables(0).Rows.Count > 0 Then
                            dRate = Val(DsRate.Tables(0).Rows(0)("Rate") & "")
                        End If
                        dDailyClassRate = dRate

                        ''bSalesDetail = objSales.SalesDetailSave(Trim(sInvoiceNo), _
                        ''                    Trim(GetText(grdSales, iCnt, ICode)), Trim(GetText(grdSales, iCnt, IDescription)), sLocationCode, _
                        ''                    Val(GetText(grdSales, iCnt, IWeight)), Val(GetText(grdSales, iCnt, IDSWeight)), _
                        ''                    Val(GetText(grdSales, iCnt, IGoldLoss)), Val(GetText(grdSales, iCnt, ICostPerGram)), _
                        ''                    Val(GetText(grdSales, iCnt, ISMC)), Val(GetText(grdSales, iCnt, IPCS)), _
                        ''                    Val(nDetailDiscount), Val(dNetDiscount), Val(dGST), _
                        ''                    Val(dNetAmount), Val(dActualSales), _
                        ''                    Val(GetText(grdSales, iCnt, ITotalValue)), Val(GetText(grdSales, iCnt, ITotalCost)), _
                        ''                    Val(dCommission), Trim(txtRemarks.Text), Val(dDailyClassRate), iCnt, Trim(GetText(grdSales, iCnt, ISalesType)))

                        ''If Not bSalesDetail Then
                        ''    'MsgBox(sError1)
                        ''    fncSales = False
                        ''    Exit Function
                        ''End If


                        bCreditSalesDetail = objSales.CreditSalesDetailSave(Trim(sCreditInvoiceNo), _
                                            Trim(fnGetdgvText(grdSales, iCnt, ICode)), Trim(fnGetdgvText(grdSales, iCnt, IDescription)), sLocationCode, _
                                            Val(fnGetdgvText(grdSales, iCnt, IWeight)), Val(fnGetdgvText(grdSales, iCnt, IDSWeight)), _
                                            Val(fnGetdgvText(grdSales, iCnt, IGoldLoss)), Val(fnGetdgvText(grdSales, iCnt, ICostPerGram)), _
                                            Val(fnGetdgvText(grdSales, iCnt, ISMC)), Val(fnGetdgvText(grdSales, iCnt, IPCS)), _
                                            Val(nDetailDiscount), Val(dNetDiscount), Val(dGST), _
                                            Val(dNetAmount), Val(dActualSales), _
                                            Val(fnGetdgvText(grdSales, iCnt, ITotalValue)), Val(fnGetdgvText(grdSales, iCnt, ITotalCost)), _
                                            Val(dCommission), Trim(txtRemarks.Text), Val(dDailyClassRate), iCnt, Trim(fnGetdgvText(grdSales, iCnt, ISalesType)))

                        If Not bCreditSalesDetail Then
                            'MsgBox(sError1)
                            fncSales = False
                            Exit Function
                        End If

                    End If
                    'bRFIDUpdate = objSales.RFIDItemStatusUpdate(Trim(GetText(grdSales, iCnt, ICode)))

                    'If Not bRFIDUpdate Then
                    '    'MsgBox(sError1)
                    '    fncSales = False
                    '    Exit Function
                    'End If

                Next iCnt

            End If
            bPassword = False

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

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
        Try
            bStatus = False
            bHaveTradeIn = False
            For iCnt = 0 To grdSales.RowCount - 1
                If fnGetdgvText(grdSales, iCnt, ISalesType).Trim = "TI" Then
                    nWeight = nWeight + Val(fnGetdgvText(grdSales, iCnt, IWeight))
                    nStoneWeight = nStoneWeight + 0
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
                sTIinventorycode = "TI-" & sInvoiceNo
                bStatus = objTI.TradeInDeleteSales(Trim(sTIinventorycode))
                If Not bStatus Then
                    Exit Function
                End If

                sSalesType = "TRADEIN"

                bStatus = objTI.TradeInHeaderSaveSales(Trim(sTIinventorycode), Trim$(sInvoiceNo), _
                          gthisLocation, UCase(Trim(txtCustomerCode.Text)), "", Trim(txtCustomerName.Text), Trim(txtAddress1.Text), _
                          Trim(txtAddress2.Text), Trim(txtAddress3.Text), _
                          Val(txtCostPerGram.Text), Val(nWeight), _
                          Val(nStoneWeight), Val(nSMC), Val(nPCS), _
                          Val(nGoldLoss), Val(nTotCost), _
                          -Val(nTotalAmt), "TRADEIN", "N", sUserID)
                If Not bStatus Then
                    Exit Function
                End If

                For iCnt = 0 To grdSales.RowCount - 1
                    If fnGetdgvText(grdSales, iCnt, ISalesType).Trim.ToUpper = "TI" Then
                        Call fnSetdgvText(grdSales, iCnt, 1, sTIinventorycode)
                        bStatus = objTI.TradeInDetailSaveSales(gthisLocation, Trim(sTIinventorycode), iCnt, Trim(fnGetdgvText(grdSales, iCnt, IDescription)), _
                                "", Val(fnGetdgvText(grdSales, iCnt, IWeight)), Val("0"), _
                                Val(fnGetdgvText(grdSales, iCnt, ICostPerGram)), Val(fnGetdgvText(grdSales, iCnt, ISMC)), Val(fnGetdgvText(grdSales, iCnt, IPCS)), _
                                Val(fnGetdgvText(grdSales, iCnt, IGoldLoss)), Val(fnGetdgvText(grdSales, iCnt, ITotalCost)), "TRADEIN", "TRADEIN", "TRADEIN", "TRADEIN", Trim(fnGetdgvText(grdSales, iCnt, IClass)), Trim$(sInvoiceNo), sUserID)
                        If Not bStatus Then
                            Exit Function
                        End If
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

    Private Sub frmGPOSNew_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        If txtSalesManPassword.Enabled Then txtSalesManPassword.Focus()
        If UCase(sCompanyShortCode) <> "MJ" Then
            If txtTradeInGLPerc.Enabled And txtTradeInGLPerc.Visible Then txtTradeInGLPerc.Focus()
        Else
            If txtTradeInGLPerc.Enabled And txtTradeInGLPerc.Visible Then txtTradeInGLPerc.Focus()
        End If
    End Sub

    Private Sub grdSales_DblClick(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_DblClickEvent)
        'Try
        '    If Trim(GetText(grdSales, grdSales.ActiveRow, 1)) <> "Total:" Then
        '        bedited = True
        '        txtInventoryCode.Text = GetText(grdSales, grdSales.ActiveRow, ICode)
        '        sClassCode = GetText(grdSales, grdSales.ActiveRow, IClass)
        '        txtInventoryDescription.Text = GetText(grdSales, grdSales.ActiveRow, IDescription)
        '        txtWeight.Text = GetText(grdSales, grdSales.ActiveRow, IWeight)
        '        txtGoldLoss.Text = GetText(grdSales, grdSales.ActiveRow, IGoldLoss)
        '        txtCostPerGram.Text = GetText(grdSales, grdSales.ActiveRow, ICostPerGram)
        '        txtStoneWeight.Text = GetText(grdSales, grdSales.ActiveRow, IDSWeight)
        '        txtSMC.Text = GetText(grdSales, grdSales.ActiveRow, ISMC)
        '        mdPMC = GetText(grdSales, grdSales.ActiveRow, ISMC)
        '        mdPurMC = GetText(grdSales, grdSales.ActiveRow, IPMC)
        '        txtPCS.Text = GetText(grdSales, grdSales.ActiveRow, IPCS)
        '        txtTotalValue.Text = GetText(grdSales, grdSales.ActiveRow, ITotalValue)
        '        'added by sarathy
        '        TotVal = txtTotalValue.Text.Trim
        '        dTotalCost = Val(GetText(grdSales, grdSales.ActiveRow, ITotalCost))
        '        msClsSalesType = GetText(grdSales, grdSales.ActiveRow, ISalesType)
        '        txtGST.Text = GetText(grdSales, grdSales.ActiveRow, IGST)
        '        If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
        '        Exit Sub
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
        Try
            If Trim(fnGetdgvText(grdSales, grdSales.CurrentRow.Index, 1)) <> "Total:" Then
                bedited = True
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
    Private Sub subShowTradein()
        Try
            lblNetDiscount.Text = Format(Val(lblNetDiscount.Text), "0.00")
            If Val(lblNetValue.Text) <> 0 Then
                If UCase(txtCustomerCode.Text.Trim) = "CASH" And sMemberModule = "0" And UCase(sCompanyShortCode) <> "GMT" Then
                    MessageBox.Show("TradeIn Sales Not For CASH Customer!!!")
                    txtInventoryCode.Text = ""
                    If txtCustomerCode.Enabled Then txtCustomerCode.Focus()
                    Exit Sub
                End If
                grdTradeIN.RowCount = 0
                subGetCostPerGram()
                subSetDataTable()
                optTradeIn.Checked = True
                pnlTradeIn.Visible = True
                If txtTradeInGLPerc.Enabled Then txtTradeInGLPerc.Focus()
            Else
                txtInventoryCode.Text = ""
                If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btntradeInClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntradeInClose.Click
        pnlTradeIn.Visible = False
    End Sub

    Private Sub txtTradeInWeight_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTradeInWeight.LostFocus
        If Val(txtTradeInWeight.Text) = 0 Then
            Exit Sub
        Else
            dCost = (Val(txtTradeInWeight.Text) + Val(txtTradeInGoldLoss.Text)) * Val(txtTradeinCostPerGram.Text) '+ Val(txtMC)

            If opt24k.Checked = True Then
                dTotAmount = dCost * 1.05
                txtTradeInTotalCost.Text = Format(dTotAmount, "0.00")
            Else
                txtTradeInTotalCost.Text = Format(dCost, "0.00")
            End If
        End If
    End Sub

    Private Sub pnlTradeIn_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlTradeIn.Paint

    End Sub

    Private Sub txtTradeInGLPerc_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTradeInGLPerc.TextChanged

        If Val(txtTradeInGLPerc.Text) And Val(txtTradeInGLPerc.Text) < 100 Then
            txtTradeinCostPerGram.Text = Val(txtTradeInCost.Text) - (Val(txtTradeInCost.Text) * Val(txtTradeInGLPerc.Text) / 100)
        Else
            MsgBox("Percentage is more than 100 % !!!!", MsgBoxStyle.Exclamation)
            txtTradeInGLPerc.Clear()
            txtTradeInGLPerc.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub txtTradeInGoldLoss_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTradeInGoldLoss.LostFocus
        If Val(txtTradeInGoldLoss.Text) <> 0 Then
            dCost = (Val(txtTradeInWeight.Text) + Val(txtTradeInGoldLoss.Text)) * Val(txtTradeinCostPerGram.Text) '+ Val(txtMC)
            If opt24k.Checked = True Then
                dTotAmount = dCost * 1.05
                txtTradeInTotalCost.Text = Format(dTotAmount, "0.00")
            Else
                txtTradeInTotalCost.Text = Format(dCost, "0.00")
            End If
        End If
    End Sub

    Private Sub txtTradeinCostPerGram_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTradeinCostPerGram.LostFocus
        Dim dTotAmount As Double
        dTotAmount = 0
        If Val(txtTradeinCostPerGram.Text) <> 0 Then
            dCost = (Val(txtTradeInWeight.Text) + Val(txtTradeInGoldLoss.Text)) * Val(txtTradeinCostPerGram.Text) '+ Val(txtMC)
            ''HAJ 24K changes
            If opt24k.Checked = True Then
                dTotAmount = dCost * 1.05
                txtTradeInTotalCost.Text = Format(dTotAmount, "0.00")
            Else
                txtTradeInTotalCost.Text = Format(dCost, "0.00")
            End If

        End If
    End Sub

    Private Sub txtCustomerCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCustomerCode.KeyDown
        If e.KeyCode = Keys.F1 Then btnCustomerCodeSearch_Click(sender, e)
    End Sub

    Private Sub txtCustomerCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustomerCode.LostFocus
        Try
            If UCase(txtCustomerCode.Text.Trim) = "CASH" And sMemberModule = "1" Then
                btnCustomerCodeSearch.Enabled = False
            Else
                btnCustomerCodeSearch.Enabled = True
                subGetCustomerDetail()
                If txtSalesManPassword.Text.Trim <> "" Then
                    ''If txtInventoryCode.Enabled = True Then txtInventoryCode.Focus()
                    If txtTealGST.Enabled Then txtTealGST.Focus()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub subGetCostPerGram()
        Dim adoRS As DataSet
        Dim ssql As String
        On Error GoTo ErrHandler
        ssql = "Select Rate from tblClass where ClassCode like '22%'"
        'GetRecordset(gsConn, ssql, adoRS)
        adoRS = dm.getDataSetWithSqlAdapter(ssql, "Table", "Table")
        If adoRS.Tables(0).Rows.Count = 0 Then
            ssql = "Select Rate from tblClass where ClassCode like '18K%'"
            'GetRecordset(gsConn, ssql, adoRS)
            adoRS = dm.getDataSetWithSqlAdapter(ssql, "Table", "Table")
            If adoRS.Tables(0).Rows.Count = 0 Then
                ssql = "Select Rate from tblClass"
                'GetRecordset(gsConn, ssql, adoRS)
                adoRS = dm.getDataSetWithSqlAdapter(ssql, "Table", "Table")
                If adoRS.Tables(0).Rows.Count > 0 Then
                    txtTradeInCost.Text = Val(adoRS.Tables(0).Rows(0)("Rate") & "")
                    txtTradeinCostPerGram.Text = Val(txtTradeInCost.Text) - (Val(txtTradeInCost.Text) * Val(txtTradeInGLPerc.Text) / 100)
                End If
            Else
                txtTradeInCost.Text = Val(adoRS.Tables(0).Rows(0)("Rate") & "")
                txtTradeinCostPerGram.Text = Val(txtTradeInCost.Text) - (Val(txtTradeInCost.Text) * Val(txtTradeInGLPerc.Text) / 100)
            End If
        Else
            txtTradeInCost.Text = Val(adoRS.Tables(0).Rows(0)("Rate") & "")
            txtTradeinCostPerGram.Text = Val(txtTradeInCost.Text) - (Val(txtTradeInCost.Text) * Val(txtTradeInGLPerc.Text) / 100)
        End If
        Exit Sub
ErrHandler:
        MsgBox(Err.Description)
        Err.Clear()
    End Sub

    Private Sub btnTradeInAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTradeInAdd.Click
        If UCase(sCompanyShortCode) = "SSJ" Then
            txtTradeInGLPerc.Text = 0
            If fncValidateTradeIn() Then
                subAddGrid()
                txtTradeinDesc.Focus()
            End If

        Else
            If Val(txtTradeInGLPerc.Text) = 0 Then
                MsgBox("Loss Percentage Cannot Be Zero.", MsgBoxStyle.Information)
                If txtTradeInGLPerc.Enabled And txtTradeInGLPerc.Visible Then txtTradeInGLPerc.Focus()
                Exit Sub
            End If
            If fncValidateTradeIn() Then
                subAddGrid()
                txtTradeinDesc.Focus()
            End If
        End If
    End Sub
    Private Function fncValidateTradeIn() As Boolean
        If UCase(sCompanyShortCode) <> "MJ" Then
            If Trim(txtTradeinDesc.Text) = "" Then
                MsgBox("Description Cannot be Blank ....")
                txtTradeinDesc.Focus()
                fncValidateTradeIn = False
                Exit Function
            End If
        End If

        If UCase(sCompanyShortCode) <> "MJ" Then
            If IsNumeric(txtTradeInWeight.Text) = False Then
                MsgBox("Weight is Invalid ....")
                fncValidateTradeIn = False
                txtTradeInWeight.Focus()
                Exit Function
            End If
        Else
            txtTradeInWeight.Text = 0
        End If
        If UCase(sCompanyShortCode) <> "MJ" Then
            If Val(txtTradeInWeight.Text) <= 0 Then
                MsgBox("Weight is Invalid ....")
                fncValidateTradeIn = False
                txtTradeInWeight.Focus()
                Exit Function
            End If
        End If

        'If Trim(txtStoneWeight.Text) <> "" Then
        '    If IsNumeric(txtStoneWeight.Text) = False Then
        '        MsgBox("Stone Weight is Invalid ....")
        '        fncValidateTradeIn = False
        '        txtStoneWeight.Focus()
        '        Exit Function
        '    End If

        '    If Val(txtStoneWeight.Text) < 0 Then
        '        MsgBox("Stone Weight is Invalid ....")
        '        fncValidateTradeIn = False
        '        txtStoneWeight.Focus()
        '        Exit Function
        '    End If
        'End If
        If Val(txtTradeInGoldLoss.Text) < 0 Then
            MsgBox("Gold Loss is Invalid ....")
            fncValidateTradeIn = False
            txtTradeInGoldLoss.Focus()
            Exit Function
        End If

        If IsNumeric(txtTradeinCostPerGram.Text) = False Then
            MsgBox("Cost/Gram is Invalid ....")
            fncValidateTradeIn = False
            txtTradeinCostPerGram.Focus()
            Exit Function
        End If

        If Val(txtTradeinCostPerGram.Text) <= 0 Then
            MsgBox("Cost Per Gram is Invalid ....")
            fncValidateTradeIn = False
            txtTradeinCostPerGram.Focus()
            Exit Function
        End If

        If Val(txtTradeInTotalCost.Text) > Val(txtNetAmount.Text) Then
            MsgBox("Trade In Amount Should not be Greater than Total Sales Amount")
            fncValidateTradeIn = False
            txtTradeInTotalCost.Focus()
            Exit Function
        End If
        fncValidateTradeIn = True

        Exit Function

    End Function

    Private Sub subAddGrid()
        Dim i As Long
        Dim dTotalCost As Double
        Dim dCost As Double
        Dim r As Long
        grdTradeIn.ForeColor = System.Drawing.Color.Black
        'grdTradeIn.FontBold = False


        'If Not bedited Then
        '    If grdTradeIn.MaxRows = 0 Then
        '        grdTradeIn.MaxRows = grdTradeIn.MaxRows + 1
        '    Else
        '        grdTradeIn.Row = grdTradeIn.MaxRows
        '        grdTradeIn.Action = 5
        '        grdTradeIn.MaxRows = grdTradeIn.MaxRows - 1
        '        grdTradeIn.MaxRows = grdTradeIn.MaxRows + 1
        '    End If

        '    grdTradeIn.Row = grdTradeIn.MaxRows
        'Else
        '    grdTradeIn.Row = grdTradeIn.ActiveRow
        'End If
        If Not bedited Then
            If grdTradeIN.RowCount = 0 Then
                grdTradeIN.Rows.Add()
            Else
                'grdJobSheet.Row = grdJobSheet.MaxRows
                'grdJobSheet.Action = 5
                'grdJobSheet.MaxRows = grdJobSheet.MaxRows - 1
                'grdJobSheet.MaxRows = grdJobSheet.MaxRows + 1
            End If
            r = grdTradeIN.RowCount - 1
        Else
            r = grdTradeIN.CurrentRow.Index
        End If

        Call fnSetdgvText(grdTradeIN, r, 0, r)
        Call fnSetdgvText(grdTradeIN, r, 1, Trim(txtTradeinDesc.Text))
        Call fnSetdgvText(grdTradeIN, r, 2, Val(txtTradeInWeight.Text))
        Call fnSetdgvText(grdTradeIN, r, 3, Val(txtTradeInGoldLoss.Text))
        Dim sTotalCost As String
        sTotalCost = Format(Val(txtTradeInTotalCost.Text), "#0.00")
        dCost = Mid(sTotalCost, Len(sTotalCost) - 2, 3)
        If dCost > 0 And dCost < 0.5 Then
            dTotalCost = Math.Round(Val(txtTradeInTotalCost.Text)) - 1
        ElseIf dCost >= 0.5 And dCost <= 0.99 Then
            dTotalCost = Math.Round(Val(txtTradeInTotalCost.Text))
        Else
            dTotalCost = Val(txtTradeInTotalCost.Text)
        End If

        'grdTradeIn.Col = 8
        Call SetText(grdTradeIN, r, 7, Val(dTotalCost))
        Call SetText(grdTradeIN, r, 4, Val(dTotalCost) / (Val(txtTradeInWeight.Text) + Val(txtTradeInGoldLoss.Text)))
        Call SetText(grdTradeIN, r, 8, IIf(opt22K.Checked = True, opt22K.Text, opt22K.Text))
        'grdTradeIn.Row = grdTradeIn.MaxRows
        'grdTradeIn.Col = 1
        'grdTradeIn.Action = 0

        subTradeInTotal()
        subTradeinClearDetail()
    End Sub
    Private Sub subTradeInTotal()

        'If Not bedited Then
        '    grdTradeIn.MaxRows = grdTradeIn.MaxRows + 1
        'End If

        'Call SetText(grdTradeIn, grdTradeIn.MaxRows, 1, "Total:")

        'grdTradeIn.ForeColor = System.Drawing.Color.Red
        'grdTradeIn.FontBold = True

        'grdTradeIn.Col = 3
        'grdTradeIn.Formula = "sum(c1:c" & grdTradeIn.MaxRows - 1 & ")"


        'grdTradeIn.ForeColor = System.Drawing.Color.Red
        'grdTradeIn.FontBold = True

        'grdTradeIn.Col = 4
        'grdTradeIn.Formula = "sum(d1:d" & grdTradeIn.MaxRows - 1 & ")"

        'grdTradeIn.ForeColor = System.Drawing.Color.Red
        'grdTradeIn.FontBold = True

        'grdTradeIn.Col = 6
        'grdTradeIn.Formula = "sum(f1:f" & grdTradeIn.MaxRows - 1 & ")"

        'grdTradeIn.ForeColor = System.Drawing.Color.Red
        'grdTradeIn.FontBold = True

        'grdTradeIn.Col = 7
        'grdTradeIn.Formula = "sum(g1:g" & grdTradeIn.MaxRows - 1 & ")"

        'grdTradeIn.ForeColor = System.Drawing.Color.Red
        'grdTradeIn.FontBold = True

        'grdTradeIn.Col = 8
        ''grdTradeIn.CellType = FPSpread.CellTypeConstants.CellTypeFloat
        'grdTradeIn.Formula = "sum(h1:h" & grdTradeIn.MaxRows - 1 & ")"

        'grdTradeIn.ForeColor = System.Drawing.Color.Red
        'grdTradeIn.FontBold = True

        'grdTradeIn.Col = 10
        'grdTradeIn.Formula = "sum(j1:j" & grdTradeIn.MaxRows - 1 & ")"

        'grdTradeIn.ForeColor = System.Drawing.Color.Red
        'grdTradeIn.FontBold = True

        'lblTradeInAmount.Text = Format(Val(GetText(grdTradeIn, grdTradeIn.MaxRows, 8)), "0.00")
        'lblBalanceAmount.Text = Format(Val(lblSalesAmount.Text) - Val(lblTradeInAmount.Text), "0.00")
    End Sub


    Private Sub subTradeinClearDetail()
        On Error GoTo ErrorSub
        bedited = False
        txtTradeinDesc.Text = ""
        txtTradeInWeight.Text = ""
        txtTradeInGoldLoss.Text = ""
        sItemNo = 0
        txtTradeInTotalCost.Text = ""
        If Val(txtTradeInCost.Text) <> 0 Then
            ' txtTradeinCostPerGram.Text = Val(txtTradeInCost.Text)
            If Val(txtTradeInGLPerc.Text) > 0 And Val(txtTradeInGLPerc.Text) < 100 Then
                txtTradeinCostPerGram.Text = Val(txtTradeInCost.Text) - (Val(txtTradeInCost.Text) * Val(txtTradeInGLPerc.Text) / 100)
            End If
        End If
        Exit Sub
ErrorSub:
        Err.Clear()
    End Sub

    Private Sub grdTradeIn_Advance(ByVal sender As System.Object, ByVal e As AxFPSpread._DSpreadEvents_AdvanceEvent)

    End Sub

    'Private Sub grdTradeIn_DblClick(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_DblClickEvent)
    '    If Trim(GetText(grdTradeIn, grdTradeIn.ActiveRow, 1)) <> "Total:" Then
    '        bedited = True
    '        sItemNo = grdTradeIn.ActiveRow
    '        txtTradeinDesc.Text = GetText(grdTradeIn, grdTradeIn.ActiveRow, 2)
    '        txtTradeInWeight.Text = GetText(grdTradeIn, grdTradeIn.ActiveRow, 3)
    '        txtTradeInGoldLoss.Text = GetText(grdTradeIn, grdTradeIn.ActiveRow, 4)
    '        txtTradeinCostPerGram.Text = GetText(grdTradeIn, grdTradeIn.ActiveRow, 5)
    '        txtTradeInTotalCost.Text = GetText(grdTradeIn, grdTradeIn.ActiveRow, 8)
    '        Exit Sub
    '    End If
    'End Sub

    Private Sub btnTradeInUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTradeInUpdate.Click
        Dim frmPayingmode As New frmPayingmode
        Dim frmSalesInvoice As New frmGPOS
        Dim objOrder As New frmInventoryOrderEntry
        bTradeinFlag = True
        mbSalesTransaction = False
        If fncValidate() Then
            '  dTradeinBalance = Val(lblBalanceAmount.Text)
            sSalesType = "TRADEIN"
            ' nTotalNet = Format(Val(lblBalanceAmount.Text), "0.00")
            fncAddTradeInSalesInPOS()
            pnlTradeIn.Visible = False
            subAddTradeInToGrid()
            If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
        End If
    End Sub
    Private Function fncAddTradeInSalesInPOS() As Boolean
        'Dim iCnt As Long
        'Dim bStatus As Boolean
        'Dim ItemNo, sDesc, sWeight, sGoldLoss, sCostPerGram, sPMC, sPCS, sTotalCost, sStoneDesc, sStoneWeight, sClass
        'Dim dr As DataRow
        'Try
        '    bStatus = False
        '    For iCnt = 1 To grdTradeIn.MaxRows - 1
        '        ItemNo = Trim(GetText(grdTradeIn, iCnt, 1))
        '        sDesc = Trim(GetText(grdTradeIn, iCnt, 2))
        '        sWeight = Trim(GetText(grdTradeIn, iCnt, 3))
        '        sGoldLoss = Trim(GetText(grdTradeIn, iCnt, 4))
        '        sCostPerGram = Trim(GetText(grdTradeIn, iCnt, 5))
        '        sPMC = Trim(GetText(grdTradeIn, iCnt, 6))
        '        'sPCS = Trim(GetText(grdTrade, iCnt, 7))
        '        sPCS = "1"
        '        sTotalCost = Trim(GetText(grdTradeIn, iCnt, 8))
        '        'sStoneDesc = Trim(GetText(grdTradeIn, iCnt, 9))
        '        'sStoneWeight = Trim(GetText(grdTradeIn, iCnt, 10))
        '        sClass = Trim(GetText(grdTradeIn, iCnt, 9))

        '        dr = dsTradeIn.Tables(0).NewRow()
        '        dr.Item("ItemNo") = Trim(ItemNo)
        '        dr.Item("sDesc") = Trim(sDesc)
        '        dr.Item("sWeight") = Trim(sWeight)
        '        dr.Item("sGoldLoss") = Trim(sGoldLoss)
        '        dr.Item("sCOstPerGram") = Trim(sCostPerGram)
        '        dr.Item("sPMC") = Trim(sPMC)
        '        dr.Item("sPCS") = Trim(sPCS)
        '        dr.Item("sTotalCost") = Trim(sTotalCost)
        '        'dr.Item("sStoneDesc") = Trim(sStoneDesc)
        '        'dr.Item("sStoneWeight") = Trim(sStoneWeight)
        '        dr.Item("sClass") = Trim(sClass)
        '        dsTradeIn.Tables(0).Rows.Add(dr)
        '    Next

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'Finally
        '    fncAddTradeInSalesInPOS = bStatus
        'End Try
    End Function
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
        Try

            For iRow = 0 To dsTradeIn.Tables(0).Rows.Count - 1
                grdSales.ForeColor = Color.Black
                'grdSales.FontBold = False
                'If Not bedited Then
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

                sTiInventoryCode = dsTradeIn.Tables(0).Rows(iRow).Item("ItemNo") & "".Trim
                sTiClassCode = dsTradeIn.Tables(0).Rows(iRow).Item("sClass") & "".Trim
                sTiDescription = dsTradeIn.Tables(0).Rows(iRow).Item("sDesc") & "".Trim
                sTiWeight = dsTradeIn.Tables(0).Rows(iRow).Item("sWeight") & "".Trim
                sTiGoldLoss = dsTradeIn.Tables(0).Rows(iRow).Item("sGoldLoss") & "".Trim
                sTiCostPerGram = dsTradeIn.Tables(0).Rows(iRow).Item("sCostPerGram") & "".Trim
                '  sTiStoneWeight = dsTradeIn.Tables(0).Rows(iRow).Item("sStoneWeight") & "".Trim
                sTiSmc = "0"
                sTiPurMC = dsTradeIn.Tables(0).Rows(iRow).Item("sPMC") & "".Trim
                sTiPCS = dsTradeIn.Tables(0).Rows(iRow).Item("sPCS") & "".Trim
                sTiTotalValue = dsTradeIn.Tables(0).Rows(iRow).Item("sTotalCost") & "".Trim
                sTiTotalCost = dsTradeIn.Tables(0).Rows(iRow).Item("sTotalCost") & "".Trim
                sTiSalesType = "TI"

                If optExclusive.Checked = True Then
                    sTiGST = Format$(Val(sTiTotalValue) * (dGSTPerc / 100), "#0.00")
                    sTiTotalValue = Format(Val(sTiTotalValue) + Val(sTiGST), "0.00")
                ElseIf optInclusive.Checked = True Then
                    If UCase(sCompanyShortCode) = "MJ" Or UCase(sCompanyShortCode) = "SSJ" Then
                        sTiTotalValue = Format(Val(sTiTotalValue) + 0, "0.00")
                    Else
                        sTiGST = Format$((Val(sTiTotalValue) * dGSTPerc) / (100), "#0.00")
                        sTiTotalValue = Format(Val(sTiTotalValue) + Val(sTiGST), "0.00")
                    End If
                ElseIf optZeroRated.Checked = True Then
                    sTiGST = 0.0
                    sTiTotalValue = Format(Val(sTiTotalValue) + Val(sTiGST), "0.00")
                End If

                Call SetText(grdSales, iRow, ICode, sTiInventoryCode)
                Call SetText(grdSales, iRow, IClass, sTiClassCode)
                Call SetText(grdSales, iRow, IDescription, sTiDescription)
                Call SetText(grdSales, iRow, IWeight, Val(sTiWeight))
                Call SetText(grdSales, iRow, IGoldLoss, Val(sTiGoldLoss))
                Call SetText(grdSales, iRow, ICostPerGram, Val(sTiCostPerGram))
                '       Call SetText(grdSales, grdSales.Row, IDSWeight, Val(sTiStoneWeight))
                Call SetText(grdSales, iRow, ISMC, Val(sTiSmc))
                Call SetText(grdSales, iRow, IPMC, Val(sTiPurMC))
                Call SetText(grdSales, iRow, IPCS, Val(sTiPCS))
                Call SetText(grdSales, iRow, ITotalValue, Val(-sTiTotalValue))
                Call SetText(grdSales, iRow, ITotalCost, Val(sTiTotalCost))
                Call SetText(grdSales, iRow, ISalesType, sTiSalesType)
                Call SetText(grdSales, iRow, IGST, Val(-sTiGST))

                'grdSales.Row = grdSales.MaxRows
                'grdSales.Col = 1
                'grdSales.Action = 0
                subTotalFromGrid()
                subTotal()
                subClearDetail()
                'If grdSales.MaxRows >= 2 Then
                '    optZeroRated.Enabled = False
                '    optInclusive.Enabled = False
                '    optExclusive.Enabled = False
                'End If
                bedited = False
            Next iRow

            dsTradeIn.Tables(0).Rows.Clear()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subSetDataTable()
        Dim dt As New DataTable
        dt = New DataTable
        dt.Columns.Add(New DataColumn("ItemNo", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("sDesc", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("sWeight", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("sGoldLoss", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("sCostPerGram", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("sPMC", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("sPCS", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("sTotalCost", Type.GetType("System.String")))
        'dt.Columns.Add(New DataColumn("sStoneDesc", Type.GetType("System.String")))
        'dt.Columns.Add(New DataColumn("sStoneWeight", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("sClass", Type.GetType("System.String")))
        dsTradeIn.Tables.Add(dt)
    End Sub

    Private Sub txtStoneWeight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtStoneWeight.Click

    End Sub

    Private Sub txtPaidAmount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPaidAmount.Click

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
                            'Changed by Shabeer 20120614
                            txtTael.Text = Format((((Val(txtFindCostPerGram.Text) / Val(GstTaelPercentage)) / 0.92) / 0.02646), "0")
                            'txtTael.Text = Format((((Val(txtFindCostPerGram.Text)) / 0.92) / 0.02646), "0")
                        Else
                            'txtTael.Text = Format((((Val(txtFindCostPerGram.Text)) / 0.92) / 0.02646), "0")
                            txtTael.Text = Format((((Val(txtFindCostPerGram.Text) / Val(GstTaelPercentage)) / 0.92) / 0.02646), "0")
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

    Private Sub btnTradeInAdd_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTradeInAdd.GotFocus
        txtTradeInTotalCost.Text = (Val(txtTradeInWeight.Text) + Val(txtTradeInGoldLoss.Text)) * Val(txtTradeinCostPerGram.Text)
    End Sub

    Private Sub btnTradeinFClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTradeinFClear.Click
        subTradeinClearDetail()
    End Sub

    Private Sub txtInventoryCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInventoryCode.TextChanged

    End Sub

    Private Sub txtTradeInTotalCost_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTradeInTotalCost.TextChanged

    End Sub

    Private Sub txtTradeInTotalCost_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTradeInTotalCost.Leave
        Dim dCostPerGram As Double
        If UCase(sCompanyShortCode) <> "MJ" Then
            If Val(txtTradeInTotalCost.Text) <> 0 Then
                dCost = Val(txtTradeInTotalCost.Text)
                dCostPerGram = dCost / (Val(txtTradeInWeight.Text) + Val(txtTradeInGoldLoss.Text))
                txtTradeinCostPerGram.Text = dCostPerGram
                txtTradeInTotalCost.Text = Format(dCost, "0.00")
            End If
        End If
    End Sub

    Private Sub btnReprintCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReprintCancel.Click
        Try
            txtRePrintBillNo.Text = ""
            PnlReprint.Visible = False
            If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function fnccheckPaid() As Boolean
        Try
            Dim dAmt As Double
            Dim dNoCash As Double
            Dim iX As Long
            fnccheckPaid = False
            For iX = 0 To RecCount - 1
                If Trim(arrpay(iX, 3)) <> "CASH" Then
                    dNoCash = dNoCash + arrpay(iX, 1)
                End If
            Next iX

            'txtReturnChange.Text = 0 - (Format(Val(txtBalanceAmount.Text), "#####0.00"))
            txtReturnChange.Text = Format(Val(txtBalanceAmount.Text), "#####0.00")
            For iX = 0 To RecCount - 1
                If Trim(arrpay(iX, 3)) = "CASH" And arrpay(iX, 1) >= Val(txtBalanceAmount.Text) Then
                    arrpay(iX, 1) = Format(Val(arrpay(iX, 1)) - Val(txtReturnChange.Text), "0.00")
                End If
                If Val(txtTotalAmount.Text) > 0 And Val(fnGetdgvText(grdPaymode, iX, TxtAmount)) < 0 Then
                    fnSetdgvText(grdPaymode, iX, TxtAmount, "0")
                    arrpay(iX, 1) = 0
                End If


                dAmt = dAmt + Val(fnGetdgvText(grdPaymode, iX, TxtAmount))
            Next iX
            If Math.Round(dAmt, 2) >= Val(txtTotalAmount.Text) Then
                fnccheckPaid = True
            Else
                txtPaidAmount.Text = dAmt
                fnSetdgvText(grdPaymode, iX, TxtAmount, dAmt)
                'grdPaymode.Col = TxtAmount
                'grdPaymode.Row = 0
                'TxtAmount(0).SetFocus()
                If Trim(sCompanyShortCode) = "HN" Then
                    fnccheckPaid = False
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Sub btntradeInClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntradeInClear.Click
        subTradeinClearDetail()
    End Sub

    Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub txtTealGST_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTealGST.LostFocus
        Try
            txtCostPerGram.Text = Format(((((Val(txtTealGST.Text) * 0.02646) * 0.92))), "#0.00")
            If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub


    Private Sub txtNetAmount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNetAmount.LostFocus
        Try
            If sSalesType = "TRADEIN" Then

                If Val(txtNetAmount.Text) >= 0 Then
                    lblNetDiscount.Text = Format(Val(lblSalesAmount.Text) - Val(txtNetAmount.Text), "0.00")
                    lblNetValue.Text = Format(Val(lblSalesAmount.Text) - Val(lblNetDiscount.Text), "0.00")
                    subGetGSTAmount()
                    txtNetAmount.Visible = False
                    If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
                    Exit Sub
                End If
            Else
                If Val(txtNetAmount.Text) <> 0 Then
                    lblNetDiscount.Text = Format(Val(lblSalesAmount.Text) - Val(txtNetAmount.Text), "0.00")
                    lblNetValue.Text = Format(Val(lblSalesAmount.Text) - Val(lblNetDiscount.Text), "0.00")
                    subGetGSTAmount()
                    txtNetAmount.Visible = False
                    If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtNetAmount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNetAmount.TextChanged

    End Sub

    Private Sub txtTael_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTael.LostFocus

    End Sub

    Private Sub txtTotalValue_LostFocus(sender As Object, e As System.EventArgs) Handles txtTotalValue.LostFocus
        Try
            'If msClsSalesType = "P" Or msClsSalesType = "B" Then
            '    If (Val(txtTotalValue.Text)) < (Val(mdPurMC)) Then
            '        'MsgBox("Cost Less than Purchase Cost $" & (Val(mdPurMC)) & ". Please Enter Super Admin Password to proceed.")
            '        MsgBox("Cost Less than Purchase Cost. Please Enter Super Admin Password to proceed.")
            '        'txtTotalValue.Focus()
            '        subGetAdminPassword()
            '        Exit Sub
            '    ElseIf (Val(txtTotalValue.Text)) < (Val(mdPurMC) + (Val(mdPurMC) * 0.4)) Then
            '        'MsgBox("Cost Less than Purchase Cost $" & (Val(mdPurMC) + (Val(mdPurMC) * 0.6)) & ". Please Enter Admin Password to proceed.")
            '        MsgBox("Cost Less than Purchase Cost. Please Enter Admin Password to proceed.")
            '        'txtTotalValue.Focus()
            '        subGetAdminPassword()
            '        Exit Sub
            '    End If
            'Else
            '    'If Val(sSalesPcs) = 0 Then
            '    If (Val(txtTotalValue.Text) - (Val(txtTotalValue.Text) * (7 / 107))) < ((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + Val(mdPurMC)) Then
            '        'MsgBox("Cost Less than Purchase Cost $" & ((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + Val(mdPurMC)) + (((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + Val(mdPurMC)) * 0.07) & ". Please Enter Super Admin Password to proceed.")
            '        MsgBox("Cost Less than Purchase Cost. Please Enter Super Admin Password to proceed.")
            '        'txtTotalValue.Focus()
            '        subGetAdminPassword()
            '        Exit Sub
            '    ElseIf (Val(txtTotalValue.Text) - (Val(txtTotalValue.Text) * (7 / 107))) < ((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + (Val(txtSMC.Text) - (Val(txtSMC.Text) * 0.4))) Then
            '        'MsgBox("Cost Less than Purchase Cost $" & ((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + (Val(txtSMC.Text) - (Val(txtSMC.Text) * 0.4))) + (((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + (Val(txtSMC.Text) - (Val(txtSMC.Text) * 0.4))) * 0.07) & ". Please Enter Admin Password to proceed.")
            '        MsgBox("Cost Less than Purchase Cost. Please Enter Admin Password to proceed.")
            '        'txtTotalValue.Focus()
            '        subGetAdminPassword()
            '        Exit Sub
            '    End If
            '    '    Else
            '    If (Val(txtTotalValue.Text) - (Val(txtTotalValue.Text) * (7 / 107))) < ((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + Val(mdPurMC)) Then
            '        'MsgBox("Cost Less than Purchase Cost $" & ((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + Val(mdPurMC)) + (((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + Val(mdPurMC)) * 0.07) & ". Please Enter Super Admin Password to proceed.")
            '        MsgBox("Cost Less than Purchase Cost. Please Enter Super Admin Password to proceed.")
            '        'txtTotalValue.Focus()
            '        subGetAdminPassword()
            '        Exit Sub
            '    ElseIf (Val(txtTotalValue.Text) - (Val(txtTotalValue.Text) * (7 / 107))) < ((Val(txtSMC.Text) - (Val(txtSMC.Text) * 0.4))) Then
            '        'MsgBox("Cost Less than Purchase Cost $" & ((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + (Val(txtSMC.Text) - (Val(txtSMC.Text) * 0.4))) + (((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + (Val(txtSMC.Text) - (Val(txtSMC.Text) * 0.4))) * 0.07) & ". Please Enter Admin Password to proceed.")
            '        MsgBox("Cost Less than Purchase Cost. Please Enter Admin Password to proceed.")
            '        'txtTotalValue.Focus()
            '        subGetAdminPassword()
            '        Exit Sub
            '    End If
            'End If
            'End If

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
    Private Sub txtTotalValue_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTotalValue.TextChanged

    End Sub

    Private Sub txtSMC_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtSMC.TextChanged

    End Sub

    Private Sub btnCostAdminPwdCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCostAdminPwdCancel.Click
        pnlCostAdminPwd.Visible = False
        txtCostAdminPassword.Focus()
    End Sub

    Private Sub btnCostAdminPwdOK_Click(sender As System.Object, e As System.EventArgs) Handles btnCostAdminPwdOK.Click
        Dim dsPWD As New DataSet
        Dim dsIns As New DataSet

        Try
            If Trim(txtCostAdminPassword.Text) <> "" Then
                dsPWD = objSM.GetSalesManwithPassword(txtCostAdminPassword.Text)
                If dsPWD.Tables(0).Rows.Count > 0 Then
                    If Trim(UCase(dsPWD.Tables(0).Rows(0)("SalesManGroup") & "")) = "ADMIN" Or Trim(UCase(dsPWD.Tables(0).Rows(0)("SalesManGroup") & "")) = "SUPER ADMIN" Then
                        If (Val(txtTotalValue.Text)) < (Val(mdPurMC)) And (msClsSalesType = "P" Or msClsSalesType = "B") Then
                            'MessageBox.Show("Please Check the Password")
                            MsgBox("SalesMan Has No Rights to do Change Cost!", MsgBoxStyle.Critical)
                            txtCostAdminPassword.Text = ""
                            If (txtCostAdminPassword.Enabled And txtCostAdminPassword.Visible) Then txtCostAdminPassword.Focus()
                            bBCPassword = False
                            Exit Sub
                        ElseIf (Val(txtTotalValue.Text) - (Val(txtTotalValue.Text) * (7 / 107))) < ((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + Val(mdPurMC)) And msClsSalesType = "W" And Val(sSalesType) = 0 And (UCase(dsPWD.Tables(0).Rows(0)("SalesManGroup") & "")) = "ADMIN" Then
                            MsgBox("SalesMan Has No Rights to do Change Cost!", MsgBoxStyle.Critical)
                            txtCostAdminPassword.Text = ""
                            If (txtCostAdminPassword.Enabled And txtCostAdminPassword.Visible) Then txtCostAdminPassword.Focus()
                            bBCPassword = False
                            Exit Sub
                        ElseIf (Val(txtTotalValue.Text) - (Val(txtTotalValue.Text) * (7 / 107))) < (Val(txtSMC.Text)) And msClsSalesType = "W" And Val(sSalesType) = 1 And (UCase(dsPWD.Tables(0).Rows(0)("SalesManGroup") & "")) = "ADMIN" Then
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
End Class
