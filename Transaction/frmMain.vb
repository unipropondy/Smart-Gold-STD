Public Class frmMain
    Inherits System.Windows.Forms.Form
    Dim sClassCode As String
    Dim StartTime As DateTime
    Friend WithEvents txtGST As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents mnuGoldSchemeEntries As System.Windows.Forms.MenuItem
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnMainOutletBarCode As System.Windows.Forms.Button
    Dim dClassRate As Double
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
    Public WithEvents ImageList As System.Windows.Forms.ImageList
    Friend WithEvents MainMenu2 As System.Windows.Forms.MainMenu
    Friend WithEvents mnuMaster As System.Windows.Forms.MenuItem
    Friend WithEvents mnuExit As System.Windows.Forms.MenuItem
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents btnQuit As System.Windows.Forms.ToolBarButton
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel3 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents conMenu As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem44 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem47 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem55 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem57 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem59 As System.Windows.Forms.MenuItem
    Friend WithEvents ConMenuAr As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem61 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem62 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem63 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem64 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem65 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSecurity As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem101 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuChangePassword As System.Windows.Forms.MenuItem
    Friend WithEvents mnuUserPermission As System.Windows.Forms.MenuItem
    Friend WithEvents mnuUserGroup As System.Windows.Forms.MenuItem
    Friend WithEvents mnuUserMaster As System.Windows.Forms.MenuItem
    Friend WithEvents mnuHeadUtility As System.Windows.Forms.MenuItem
    Friend WithEvents mnuChangePrinter As System.Windows.Forms.MenuItem
    Friend WithEvents mnuDailyProcedure As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSettlement As System.Windows.Forms.MenuItem
    Friend WithEvents mnuDayEnd As System.Windows.Forms.MenuItem
    Friend WithEvents mnuClass As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCategory As System.Windows.Forms.MenuItem
    Friend WithEvents mnuClassRate As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCatalogue As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPurchaseHeader As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCompany As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLocation As System.Windows.Forms.MenuItem
    Friend WithEvents mnuTerminal As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSupplier As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCustomer As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSalesman As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuTrayMaster As System.Windows.Forms.MenuItem
    Friend WithEvents mnuTrayTransfer As System.Windows.Forms.MenuItem
    Friend WithEvents mnuJobSheet As System.Windows.Forms.MenuItem
    Friend WithEvents mnuDirectPurchase As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPurchase As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem7 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuBarCode As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSpecialBarcode As System.Windows.Forms.MenuItem
    Friend WithEvents mnuJewellaryBarcode As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFixCostJobSheet As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem10 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuInventorySearch As System.Windows.Forms.MenuItem
    Friend WithEvents mnuInventoryMaintenance As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSplitInventory As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPurchaseReturn As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem9 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuHeadSales As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPOS As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem8 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuInstallment As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSalesReturn As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem11 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuTradeInSales As System.Windows.Forms.MenuItem
    Friend WithEvents mnuHeadTransaction As System.Windows.Forms.MenuItem
    Friend WithEvents mnuMiscellaneousEntry As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOrderEntry As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPaymentEntry As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem12 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuGoldBarPurchase As System.Windows.Forms.MenuItem
    Friend WithEvents mnuGoldBarIssue As System.Windows.Forms.MenuItem
    Friend WithEvents mnuTradeInGoldIssue As System.Windows.Forms.MenuItem
    Friend WithEvents mnuGSTRefund As System.Windows.Forms.MenuItem
    Friend WithEvents mnuHeadReports As System.Windows.Forms.MenuItem
    Friend WithEvents mnuMasterReports As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPurchaseReports As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSalesReports As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem13 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuInventoryReports As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSettlementReports As System.Windows.Forms.MenuItem
    Friend WithEvents mnuEODReports As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem14 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuManagementReports As System.Windows.Forms.MenuItem
    Friend WithEvents mnuGoldbarReports As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem16 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuTransactionReports As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAuditingReports As System.Windows.Forms.MenuItem
    Friend WithEvents mnuHeadAccounts As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAccountMaster As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAccountControl As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAccountType As System.Windows.Forms.MenuItem
    Friend WithEvents mnuBankMaster As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem15 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuGroup As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCOAGroup As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAgingDaysSettings As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPaymodeLedgerSettings As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAPSystemSettings As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAPControlLedgerSettings As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem17 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAccountPayable As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPaymenttoOthers As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPaymenttoVendor As System.Windows.Forms.MenuItem
    Friend WithEvents mnuDebitNote As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCreditNote As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem18 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem20 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuGroupPosting As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPurchaseInvoicePosting As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPurchaseReturnPosting As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPaymentPosting As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem21 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPaymentOthersPosting As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem22 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuDebitNotePosting As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCreditNotePosting As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem24 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAPReports As System.Windows.Forms.MenuItem
    Friend WithEvents mnuDebitNoteReport As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCreditNoteReport As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem25 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAgingReport As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPaymentReport As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem27 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuBalanceReport As System.Windows.Forms.MenuItem
    Friend WithEvents mnuGSTReport As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem26 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuGeneralLedger As System.Windows.Forms.MenuItem
    Friend WithEvents mnuJournalEntry As System.Windows.Forms.MenuItem
    Friend WithEvents mnuJournalTemplate As System.Windows.Forms.MenuItem
    Friend WithEvents mnuJournalListing As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem28 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFinacialReports As System.Windows.Forms.MenuItem
    Friend WithEvents mnuJournalReport As System.Windows.Forms.MenuItem
    Friend WithEvents mnuJournalList As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem29 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLedgerReports As System.Windows.Forms.MenuItem
    Friend WithEvents mnuMultipleLedgerReports As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem30 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuTrialBalance As System.Windows.Forms.MenuItem
    Friend WithEvents mnuGeneralLedgerReports As System.Windows.Forms.MenuItem
    Friend WithEvents mnuProfitandLoss As System.Windows.Forms.MenuItem
    Friend WithEvents mnuBalanceSheet As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem31 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem32 As System.Windows.Forms.MenuItem
    Friend WithEvents btnClass As System.Windows.Forms.ToolBarButton
    Friend WithEvents Sep1 As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCategory As System.Windows.Forms.ToolBarButton
    Friend WithEvents sep2 As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCatalogue As System.Windows.Forms.ToolBarButton
    Friend WithEvents Sep3 As System.Windows.Forms.ToolBarButton
    Friend WithEvents Sep4 As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnSearch As System.Windows.Forms.ToolBarButton
    Friend WithEvents sep5 As System.Windows.Forms.ToolBarButton
    Friend WithEvents sep6 As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnSales As System.Windows.Forms.ToolBarButton
    Friend WithEvents sep7 As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnBarcode As System.Windows.Forms.ToolBarButton
    Friend WithEvents sep8 As System.Windows.Forms.ToolBarButton
    Friend WithEvents MenuItem33 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem34 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPaymode As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCurrency As System.Windows.Forms.MenuItem
    Friend WithEvents mnuMergeInventory As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem36 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem35 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem37 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuReceiptsToCustomer As System.Windows.Forms.MenuItem
    Friend WithEvents mnuReceiptsToOther As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem38 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuARDebitNote As System.Windows.Forms.MenuItem
    Friend WithEvents mnuARCreditNote As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem39 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem40 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCreditSalesPostGroup As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCreditSalesReturnPostGroup As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem41 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuReceiptCustomerPostGroup As System.Windows.Forms.MenuItem
    Friend WithEvents mnuReceiptOthersPostGroup As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem42 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCreditNotePostGroup As System.Windows.Forms.MenuItem
    Friend WithEvents mnuDebitNotePostGroup As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem43 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPOSSettlementPost As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem45 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem46 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSalesLedgerReport As System.Windows.Forms.MenuItem
    Friend WithEvents mnuReceiptReport As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem48 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuARCreditNoteReport As System.Windows.Forms.MenuItem
    Friend WithEvents mnuARDebitNoteReport As System.Windows.Forms.MenuItem
    Friend WithEvents mnuARAgingReport As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem49 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuARSatementReport As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem50 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuARBalanceReport As System.Windows.Forms.MenuItem
    Friend WithEvents ObjImageList As System.Windows.Forms.ImageList
    Friend WithEvents mnuPayOutMaster As System.Windows.Forms.MenuItem
    Friend WithEvents sbGPos As System.Windows.Forms.StatusBar
    Friend WithEvents mnuARControlLedgerSettings As System.Windows.Forms.MenuItem
    Friend WithEvents mnuJournalPosting As System.Windows.Forms.MenuItem
    Friend WithEvents ImageList2 As System.Windows.Forms.ImageList
    Friend WithEvents btnJobSheet As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnOrder As System.Windows.Forms.ToolBarButton
    Friend WithEvents mnuConsignmentSales As System.Windows.Forms.MenuItem
    Friend WithEvents mnuConSales As System.Windows.Forms.MenuItem
    Friend WithEvents mnuConSalesRet As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem19 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem23 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuDailySales As System.Windows.Forms.MenuItem
    Friend WithEvents mnuStockReports As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCOA As System.Windows.Forms.MenuItem
    Friend WithEvents mnuMemberModule As System.Windows.Forms.MenuItem
    Friend WithEvents mnuMemberType As System.Windows.Forms.MenuItem
    Friend WithEvents mnuNricType As System.Windows.Forms.MenuItem
    Friend WithEvents mnuMember As System.Windows.Forms.MenuItem
    Friend WithEvents mnuMemberReports As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem52 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem53 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem54 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAnalysisReports As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem56 As System.Windows.Forms.MenuItem
    Friend WithEvents MnuAccMasterReports As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOrderPayment As System.Windows.Forms.MenuItem
    Friend WithEvents sep9 As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnTodaySales As System.Windows.Forms.ToolBarButton
    Friend WithEvents sep10 As System.Windows.Forms.ToolBarButton
    Friend WithEvents mnuOrderPayment1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem58 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem60 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem66 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPOSSettlementReport As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPurchaseReturnPost As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSalesReturnPost As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem67 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAbout As System.Windows.Forms.MenuItem
    Friend WithEvents UGPSMenu As System.Windows.Forms.ToolBar
    Friend WithEvents sep11 As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnClassCostDisp As System.Windows.Forms.ToolBarButton
    Friend WithEvents MenuItem68 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem69 As System.Windows.Forms.MenuItem
    Friend WithEvents Manageforotheres As System.Windows.Forms.MenuItem
    Friend WithEvents mnuWholeSalePOS As System.Windows.Forms.MenuItem
    Friend WithEvents mnuTradeInWholeSales As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem70 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCountry As System.Windows.Forms.MenuItem
    Friend WithEvents mnuRefiner As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem71 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuGoldBarFix As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSize As System.Windows.Forms.MenuItem
    Friend WithEvents grpMainHQ As System.Windows.Forms.GroupBox
    Friend WithEvents btnMainExit As System.Windows.Forms.Button
    Friend WithEvents btnMainBarCode As System.Windows.Forms.Button
    Friend WithEvents btnMainTransferOut As System.Windows.Forms.Button
    Friend WithEvents btnMainTransferIn As System.Windows.Forms.Button
    Friend WithEvents btnMainPurchase As System.Windows.Forms.Button
    Friend WithEvents btnMainJobsheet As System.Windows.Forms.Button
    Friend WithEvents btnMainSearch As System.Windows.Forms.Button
    Friend WithEvents btnMainCategory As System.Windows.Forms.Button
    Friend WithEvents btnMainCatalogue As System.Windows.Forms.Button
    Friend WithEvents btnMainClass As System.Windows.Forms.Button
    Friend WithEvents btnMainInventory As System.Windows.Forms.Button
    Friend WithEvents mnuMasterCompany As System.Windows.Forms.MenuItem
    Friend WithEvents mnuMasterInventory As System.Windows.Forms.MenuItem
    Friend WithEvents mnuMasterProcrument As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem75 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuMasterIPM As System.Windows.Forms.MenuItem
    Friend WithEvents mnuHeadTransfer As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem78 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuTransferOut As System.Windows.Forms.MenuItem
    Friend WithEvents mnuTransferIn As System.Windows.Forms.MenuItem
    Friend WithEvents mnuTransferStatus As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem79 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem80 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuTransferReports As System.Windows.Forms.MenuItem
    Friend WithEvents mnuInventoryMovement As System.Windows.Forms.MenuItem
    Friend WithEvents grpMainOutlet As System.Windows.Forms.GroupBox
    Friend WithEvents btnMainOutletExit As System.Windows.Forms.Button
    Friend WithEvents btnMainOutletDayEnd As System.Windows.Forms.Button
    Friend WithEvents btnMainOutletUpdateHQ As System.Windows.Forms.Button
    Friend WithEvents btnMainOutletSettlement As System.Windows.Forms.Button
    Friend WithEvents btnMainOutletTransferOut As System.Windows.Forms.Button
    Friend WithEvents btnMainOutletTransferIn As System.Windows.Forms.Button
    Friend WithEvents btnMainOutletOrderEntry As System.Windows.Forms.Button
    Friend WithEvents btnMainOutletInstallment As System.Windows.Forms.Button
    Friend WithEvents btnMainOutletInventory As System.Windows.Forms.Button
    Friend WithEvents btnMainOutletSearch As System.Windows.Forms.Button
    Friend WithEvents btnMainOutletPos As System.Windows.Forms.Button
    Friend WithEvents btnMainOutletPaymentEntry As System.Windows.Forms.Button
    Friend WithEvents mnuInstallmentEntry As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOrderInventory As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem81 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuRepairEntry As System.Windows.Forms.MenuItem
    Friend WithEvents mnuRepairPayment As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem82 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem84 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem83 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem85 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPaymentEntries As System.Windows.Forms.MenuItem
    Friend WithEvents dgvOrder As System.Windows.Forms.DataGridView
    Friend WithEvents mnuPaymodeModification As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPayOut As System.Windows.Forms.MenuItem
    Friend WithEvents dgvInstallment As System.Windows.Forms.DataGridView
    Friend WithEvents mnuOutletCustomers As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCustomerReports As System.Windows.Forms.MenuItem
    Friend WithEvents dgvunfixJobsheet As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grpReOrder As System.Windows.Forms.GroupBox
    Friend WithEvents dgvReOrder As System.Windows.Forms.DataGridView
    Friend WithEvents cLocationCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents mnuReOrderLevelMaster As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSalesOrder As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOnlineTransfer As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCustomerPointAdjustment As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCustomerTypeChange As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPaymentEntryAksayaTritiya As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCatalogueSearch As System.Windows.Forms.MenuItem
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cLocation As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Balance As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Value As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents mnueTRSTicket As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSupplierMC As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPurchaseTally As System.Windows.Forms.MenuItem
    Friend WithEvents mnuTradeInMerge As System.Windows.Forms.MenuItem
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuMaster2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChangePasswordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSalesHistory As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSupplierPayment As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCreditSalesPayment As System.Windows.Forms.MenuItem
    Friend WithEvents colOrderNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coLocationCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colOrderDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DeliveryDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCustomerCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colMobile As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cValue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSupplier As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents mnuTradeInSplit As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCustomerDeposit As System.Windows.Forms.MenuItem
    Friend WithEvents mnuTradeInTransfer As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSTockTradeInTransfer As System.Windows.Forms.MenuItem
    Friend WithEvents mnuTradeInReports As System.Windows.Forms.MenuItem
    Friend WithEvents mnueTRSTicketVoid As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSupplierPaymentAmendment As System.Windows.Forms.MenuItem
    Friend WithEvents btnMainHQPos As System.Windows.Forms.Button
    Friend WithEvents btnMainCreditSales As System.Windows.Forms.Button
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPurchaseTake As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPurchaseTakeReports As System.Windows.Forms.MenuItem
    Friend WithEvents mnuDeposit As System.Windows.Forms.MenuItem
    Friend WithEvents mnuDailySalesEntry As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPurchaseEntry As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSchemeMaster As System.Windows.Forms.MenuItem
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtCostPerGram As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtInventoryCode As System.Windows.Forms.TextBox
    Friend WithEvents txtSMC As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnInventoryCodeSearch As System.Windows.Forms.Button
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtTotalValue As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents mnuRFIDStocktake As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.MainMenu2 = New System.Windows.Forms.MainMenu(Me.components)
        Me.mnuMaster = New System.Windows.Forms.MenuItem()
        Me.mnuChangePassword = New System.Windows.Forms.MenuItem()
        Me.MenuItem101 = New System.Windows.Forms.MenuItem()
        Me.mnuExit = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.mnuMasterCompany = New System.Windows.Forms.MenuItem()
        Me.mnuCompany = New System.Windows.Forms.MenuItem()
        Me.mnuLocation = New System.Windows.Forms.MenuItem()
        Me.mnuTerminal = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.mnuMasterInventory = New System.Windows.Forms.MenuItem()
        Me.mnuClass = New System.Windows.Forms.MenuItem()
        Me.mnuCategory = New System.Windows.Forms.MenuItem()
        Me.mnuCatalogue = New System.Windows.Forms.MenuItem()
        Me.mnuClassRate = New System.Windows.Forms.MenuItem()
        Me.mnuTrayMaster = New System.Windows.Forms.MenuItem()
        Me.mnuSize = New System.Windows.Forms.MenuItem()
        Me.MenuItem4 = New System.Windows.Forms.MenuItem()
        Me.mnuMasterProcrument = New System.Windows.Forms.MenuItem()
        Me.mnuSupplier = New System.Windows.Forms.MenuItem()
        Me.MenuItem75 = New System.Windows.Forms.MenuItem()
        Me.mnuSalesman = New System.Windows.Forms.MenuItem()
        Me.mnuPaymode = New System.Windows.Forms.MenuItem()
        Me.mnuCustomer = New System.Windows.Forms.MenuItem()
        Me.mnuOutletCustomers = New System.Windows.Forms.MenuItem()
        Me.mnuCustomerPointAdjustment = New System.Windows.Forms.MenuItem()
        Me.mnuCustomerTypeChange = New System.Windows.Forms.MenuItem()
        Me.MenuItem34 = New System.Windows.Forms.MenuItem()
        Me.mnuMasterIPM = New System.Windows.Forms.MenuItem()
        Me.mnuRefiner = New System.Windows.Forms.MenuItem()
        Me.mnuCountry = New System.Windows.Forms.MenuItem()
        Me.mnuCurrency = New System.Windows.Forms.MenuItem()
        Me.MenuItem6 = New System.Windows.Forms.MenuItem()
        Me.mnuPayOutMaster = New System.Windows.Forms.MenuItem()
        Me.MenuItem70 = New System.Windows.Forms.MenuItem()
        Me.mnuReOrderLevelMaster = New System.Windows.Forms.MenuItem()
        Me.mnuSchemeMaster = New System.Windows.Forms.MenuItem()
        Me.mnuMemberModule = New System.Windows.Forms.MenuItem()
        Me.mnuMemberType = New System.Windows.Forms.MenuItem()
        Me.MenuItem52 = New System.Windows.Forms.MenuItem()
        Me.mnuNricType = New System.Windows.Forms.MenuItem()
        Me.MenuItem53 = New System.Windows.Forms.MenuItem()
        Me.mnuMember = New System.Windows.Forms.MenuItem()
        Me.MenuItem54 = New System.Windows.Forms.MenuItem()
        Me.mnuMemberReports = New System.Windows.Forms.MenuItem()
        Me.mnuPurchaseHeader = New System.Windows.Forms.MenuItem()
        Me.mnuJobSheet = New System.Windows.Forms.MenuItem()
        Me.mnuPurchase = New System.Windows.Forms.MenuItem()
        Me.mnuDirectPurchase = New System.Windows.Forms.MenuItem()
        Me.mnuFixCostJobSheet = New System.Windows.Forms.MenuItem()
        Me.MenuItem7 = New System.Windows.Forms.MenuItem()
        Me.mnuBarCode = New System.Windows.Forms.MenuItem()
        Me.mnuJewellaryBarcode = New System.Windows.Forms.MenuItem()
        Me.mnuSpecialBarcode = New System.Windows.Forms.MenuItem()
        Me.MenuItem10 = New System.Windows.Forms.MenuItem()
        Me.mnuInventorySearch = New System.Windows.Forms.MenuItem()
        Me.mnuInventoryMaintenance = New System.Windows.Forms.MenuItem()
        Me.mnuInventoryMovement = New System.Windows.Forms.MenuItem()
        Me.mnuCatalogueSearch = New System.Windows.Forms.MenuItem()
        Me.MenuItem36 = New System.Windows.Forms.MenuItem()
        Me.mnuSplitInventory = New System.Windows.Forms.MenuItem()
        Me.mnuMergeInventory = New System.Windows.Forms.MenuItem()
        Me.MenuItem9 = New System.Windows.Forms.MenuItem()
        Me.mnuPurchaseReturn = New System.Windows.Forms.MenuItem()
        Me.MenuItem71 = New System.Windows.Forms.MenuItem()
        Me.mnuSalesOrder = New System.Windows.Forms.MenuItem()
        Me.mnuGoldBarFix = New System.Windows.Forms.MenuItem()
        Me.mnuSupplierMC = New System.Windows.Forms.MenuItem()
        Me.mnuPurchaseTally = New System.Windows.Forms.MenuItem()
        Me.mnuSupplierPayment = New System.Windows.Forms.MenuItem()
        Me.mnuSupplierPaymentAmendment = New System.Windows.Forms.MenuItem()
        Me.mnuPurchaseEntry = New System.Windows.Forms.MenuItem()
        Me.mnuHeadSales = New System.Windows.Forms.MenuItem()
        Me.mnuPOS = New System.Windows.Forms.MenuItem()
        Me.mnuPaymentEntries = New System.Windows.Forms.MenuItem()
        Me.mnuPaymodeModification = New System.Windows.Forms.MenuItem()
        Me.mnuWholeSalePOS = New System.Windows.Forms.MenuItem()
        Me.MenuItem8 = New System.Windows.Forms.MenuItem()
        Me.MenuItem11 = New System.Windows.Forms.MenuItem()
        Me.mnuTradeInSales = New System.Windows.Forms.MenuItem()
        Me.MenuItem19 = New System.Windows.Forms.MenuItem()
        Me.mnuConsignmentSales = New System.Windows.Forms.MenuItem()
        Me.mnuConSales = New System.Windows.Forms.MenuItem()
        Me.MenuItem23 = New System.Windows.Forms.MenuItem()
        Me.mnuConSalesRet = New System.Windows.Forms.MenuItem()
        Me.MenuItem69 = New System.Windows.Forms.MenuItem()
        Me.MenuItem68 = New System.Windows.Forms.MenuItem()
        Me.MenuItem81 = New System.Windows.Forms.MenuItem()
        Me.MenuItem82 = New System.Windows.Forms.MenuItem()
        Me.mnuSalesReturn = New System.Windows.Forms.MenuItem()
        Me.mnuPayOut = New System.Windows.Forms.MenuItem()
        Me.mnuPaymentEntryAksayaTritiya = New System.Windows.Forms.MenuItem()
        Me.mnuSalesHistory = New System.Windows.Forms.MenuItem()
        Me.mnuCreditSalesPayment = New System.Windows.Forms.MenuItem()
        Me.mnuDeposit = New System.Windows.Forms.MenuItem()
        Me.mnuDailySalesEntry = New System.Windows.Forms.MenuItem()
        Me.mnuHeadTransaction = New System.Windows.Forms.MenuItem()
        Me.mnuMiscellaneousEntry = New System.Windows.Forms.MenuItem()
        Me.mnuInstallmentEntry = New System.Windows.Forms.MenuItem()
        Me.mnuInstallment = New System.Windows.Forms.MenuItem()
        Me.MenuItem84 = New System.Windows.Forms.MenuItem()
        Me.mnuOrderInventory = New System.Windows.Forms.MenuItem()
        Me.mnuOrderPayment1 = New System.Windows.Forms.MenuItem()
        Me.MenuItem83 = New System.Windows.Forms.MenuItem()
        Me.mnuOrderEntry = New System.Windows.Forms.MenuItem()
        Me.mnuRepairEntry = New System.Windows.Forms.MenuItem()
        Me.mnuRepairPayment = New System.Windows.Forms.MenuItem()
        Me.MenuItem85 = New System.Windows.Forms.MenuItem()
        Me.mnuOrderPayment = New System.Windows.Forms.MenuItem()
        Me.mnuTrayTransfer = New System.Windows.Forms.MenuItem()
        Me.mnuPaymentEntry = New System.Windows.Forms.MenuItem()
        Me.MenuItem12 = New System.Windows.Forms.MenuItem()
        Me.mnuGoldBarPurchase = New System.Windows.Forms.MenuItem()
        Me.mnuGoldBarIssue = New System.Windows.Forms.MenuItem()
        Me.mnuTradeInGoldIssue = New System.Windows.Forms.MenuItem()
        Me.MenuItem31 = New System.Windows.Forms.MenuItem()
        Me.mnuGSTRefund = New System.Windows.Forms.MenuItem()
        Me.mnuTradeInMerge = New System.Windows.Forms.MenuItem()
        Me.mnuTradeInTransfer = New System.Windows.Forms.MenuItem()
        Me.mnuTradeInSplit = New System.Windows.Forms.MenuItem()
        Me.mnuTradeInWholeSales = New System.Windows.Forms.MenuItem()
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.mnuCustomerDeposit = New System.Windows.Forms.MenuItem()
        Me.mnuHeadTransfer = New System.Windows.Forms.MenuItem()
        Me.MenuItem78 = New System.Windows.Forms.MenuItem()
        Me.mnuTransferOut = New System.Windows.Forms.MenuItem()
        Me.mnuTransferIn = New System.Windows.Forms.MenuItem()
        Me.mnuTransferStatus = New System.Windows.Forms.MenuItem()
        Me.mnuOnlineTransfer = New System.Windows.Forms.MenuItem()
        Me.mnuSTockTradeInTransfer = New System.Windows.Forms.MenuItem()
        Me.MenuItem79 = New System.Windows.Forms.MenuItem()
        Me.MenuItem80 = New System.Windows.Forms.MenuItem()
        Me.mnuTransferReports = New System.Windows.Forms.MenuItem()
        Me.mnuHeadReports = New System.Windows.Forms.MenuItem()
        Me.mnuMasterReports = New System.Windows.Forms.MenuItem()
        Me.mnuCustomerReports = New System.Windows.Forms.MenuItem()
        Me.mnuInventoryReports = New System.Windows.Forms.MenuItem()
        Me.mnuPurchaseReports = New System.Windows.Forms.MenuItem()
        Me.mnuStockReports = New System.Windows.Forms.MenuItem()
        Me.mnuSalesReports = New System.Windows.Forms.MenuItem()
        Me.mnuTransactionReports = New System.Windows.Forms.MenuItem()
        Me.mnuGoldbarReports = New System.Windows.Forms.MenuItem()
        Me.mnuDailySales = New System.Windows.Forms.MenuItem()
        Me.MenuItem13 = New System.Windows.Forms.MenuItem()
        Me.mnuSettlementReports = New System.Windows.Forms.MenuItem()
        Me.mnuEODReports = New System.Windows.Forms.MenuItem()
        Me.MenuItem14 = New System.Windows.Forms.MenuItem()
        Me.mnuManagementReports = New System.Windows.Forms.MenuItem()
        Me.mnuAnalysisReports = New System.Windows.Forms.MenuItem()
        Me.MenuItem16 = New System.Windows.Forms.MenuItem()
        Me.mnuAuditingReports = New System.Windows.Forms.MenuItem()
        Me.Manageforotheres = New System.Windows.Forms.MenuItem()
        Me.mnuTradeInReports = New System.Windows.Forms.MenuItem()
        Me.mnuHeadAccounts = New System.Windows.Forms.MenuItem()
        Me.mnuAccountMaster = New System.Windows.Forms.MenuItem()
        Me.mnuAccountControl = New System.Windows.Forms.MenuItem()
        Me.mnuAccountType = New System.Windows.Forms.MenuItem()
        Me.mnuBankMaster = New System.Windows.Forms.MenuItem()
        Me.MenuItem15 = New System.Windows.Forms.MenuItem()
        Me.mnuGroup = New System.Windows.Forms.MenuItem()
        Me.mnuCOAGroup = New System.Windows.Forms.MenuItem()
        Me.mnuCOA = New System.Windows.Forms.MenuItem()
        Me.MenuItem32 = New System.Windows.Forms.MenuItem()
        Me.mnuAgingDaysSettings = New System.Windows.Forms.MenuItem()
        Me.mnuPaymodeLedgerSettings = New System.Windows.Forms.MenuItem()
        Me.MenuItem56 = New System.Windows.Forms.MenuItem()
        Me.MnuAccMasterReports = New System.Windows.Forms.MenuItem()
        Me.mnuAPSystemSettings = New System.Windows.Forms.MenuItem()
        Me.mnuAPControlLedgerSettings = New System.Windows.Forms.MenuItem()
        Me.mnuARControlLedgerSettings = New System.Windows.Forms.MenuItem()
        Me.MenuItem17 = New System.Windows.Forms.MenuItem()
        Me.mnuAccountPayable = New System.Windows.Forms.MenuItem()
        Me.mnuPaymenttoOthers = New System.Windows.Forms.MenuItem()
        Me.mnuPaymenttoVendor = New System.Windows.Forms.MenuItem()
        Me.MenuItem18 = New System.Windows.Forms.MenuItem()
        Me.mnuDebitNote = New System.Windows.Forms.MenuItem()
        Me.mnuCreditNote = New System.Windows.Forms.MenuItem()
        Me.MenuItem20 = New System.Windows.Forms.MenuItem()
        Me.mnuGroupPosting = New System.Windows.Forms.MenuItem()
        Me.mnuPurchaseInvoicePosting = New System.Windows.Forms.MenuItem()
        Me.mnuPurchaseReturnPosting = New System.Windows.Forms.MenuItem()
        Me.MenuItem21 = New System.Windows.Forms.MenuItem()
        Me.mnuPaymentPosting = New System.Windows.Forms.MenuItem()
        Me.mnuPaymentOthersPosting = New System.Windows.Forms.MenuItem()
        Me.MenuItem22 = New System.Windows.Forms.MenuItem()
        Me.mnuDebitNotePosting = New System.Windows.Forms.MenuItem()
        Me.mnuCreditNotePosting = New System.Windows.Forms.MenuItem()
        Me.MenuItem24 = New System.Windows.Forms.MenuItem()
        Me.mnuAPReports = New System.Windows.Forms.MenuItem()
        Me.mnuPaymentReport = New System.Windows.Forms.MenuItem()
        Me.MenuItem27 = New System.Windows.Forms.MenuItem()
        Me.mnuDebitNoteReport = New System.Windows.Forms.MenuItem()
        Me.mnuCreditNoteReport = New System.Windows.Forms.MenuItem()
        Me.MenuItem25 = New System.Windows.Forms.MenuItem()
        Me.mnuAgingReport = New System.Windows.Forms.MenuItem()
        Me.mnuBalanceReport = New System.Windows.Forms.MenuItem()
        Me.MenuItem33 = New System.Windows.Forms.MenuItem()
        Me.mnuGSTReport = New System.Windows.Forms.MenuItem()
        Me.mnuPurchaseReturnPost = New System.Windows.Forms.MenuItem()
        Me.MenuItem26 = New System.Windows.Forms.MenuItem()
        Me.MenuItem35 = New System.Windows.Forms.MenuItem()
        Me.mnuReceiptsToCustomer = New System.Windows.Forms.MenuItem()
        Me.mnuReceiptsToOther = New System.Windows.Forms.MenuItem()
        Me.MenuItem38 = New System.Windows.Forms.MenuItem()
        Me.mnuARCreditNote = New System.Windows.Forms.MenuItem()
        Me.mnuARDebitNote = New System.Windows.Forms.MenuItem()
        Me.MenuItem40 = New System.Windows.Forms.MenuItem()
        Me.MenuItem39 = New System.Windows.Forms.MenuItem()
        Me.mnuCreditSalesPostGroup = New System.Windows.Forms.MenuItem()
        Me.mnuCreditSalesReturnPostGroup = New System.Windows.Forms.MenuItem()
        Me.MenuItem41 = New System.Windows.Forms.MenuItem()
        Me.mnuReceiptCustomerPostGroup = New System.Windows.Forms.MenuItem()
        Me.mnuReceiptOthersPostGroup = New System.Windows.Forms.MenuItem()
        Me.MenuItem42 = New System.Windows.Forms.MenuItem()
        Me.mnuCreditNotePostGroup = New System.Windows.Forms.MenuItem()
        Me.mnuDebitNotePostGroup = New System.Windows.Forms.MenuItem()
        Me.MenuItem43 = New System.Windows.Forms.MenuItem()
        Me.mnuPOSSettlementPost = New System.Windows.Forms.MenuItem()
        Me.MenuItem45 = New System.Windows.Forms.MenuItem()
        Me.MenuItem46 = New System.Windows.Forms.MenuItem()
        Me.mnuSalesLedgerReport = New System.Windows.Forms.MenuItem()
        Me.mnuReceiptReport = New System.Windows.Forms.MenuItem()
        Me.MenuItem48 = New System.Windows.Forms.MenuItem()
        Me.mnuARCreditNoteReport = New System.Windows.Forms.MenuItem()
        Me.mnuARDebitNoteReport = New System.Windows.Forms.MenuItem()
        Me.MenuItem49 = New System.Windows.Forms.MenuItem()
        Me.mnuARAgingReport = New System.Windows.Forms.MenuItem()
        Me.mnuARSatementReport = New System.Windows.Forms.MenuItem()
        Me.MenuItem50 = New System.Windows.Forms.MenuItem()
        Me.mnuARBalanceReport = New System.Windows.Forms.MenuItem()
        Me.mnuPOSSettlementReport = New System.Windows.Forms.MenuItem()
        Me.mnuSalesReturnPost = New System.Windows.Forms.MenuItem()
        Me.MenuItem37 = New System.Windows.Forms.MenuItem()
        Me.mnuGeneralLedger = New System.Windows.Forms.MenuItem()
        Me.mnuJournalEntry = New System.Windows.Forms.MenuItem()
        Me.mnuJournalTemplate = New System.Windows.Forms.MenuItem()
        Me.mnuJournalListing = New System.Windows.Forms.MenuItem()
        Me.mnuJournalPosting = New System.Windows.Forms.MenuItem()
        Me.MenuItem28 = New System.Windows.Forms.MenuItem()
        Me.mnuFinacialReports = New System.Windows.Forms.MenuItem()
        Me.mnuJournalReport = New System.Windows.Forms.MenuItem()
        Me.mnuJournalList = New System.Windows.Forms.MenuItem()
        Me.MenuItem29 = New System.Windows.Forms.MenuItem()
        Me.mnuLedgerReports = New System.Windows.Forms.MenuItem()
        Me.mnuMultipleLedgerReports = New System.Windows.Forms.MenuItem()
        Me.mnuGeneralLedgerReports = New System.Windows.Forms.MenuItem()
        Me.MenuItem30 = New System.Windows.Forms.MenuItem()
        Me.mnuTrialBalance = New System.Windows.Forms.MenuItem()
        Me.mnuProfitandLoss = New System.Windows.Forms.MenuItem()
        Me.mnuBalanceSheet = New System.Windows.Forms.MenuItem()
        Me.mnuDailyProcedure = New System.Windows.Forms.MenuItem()
        Me.mnuSettlement = New System.Windows.Forms.MenuItem()
        Me.mnuDayEnd = New System.Windows.Forms.MenuItem()
        Me.mnuSecurity = New System.Windows.Forms.MenuItem()
        Me.mnuUserGroup = New System.Windows.Forms.MenuItem()
        Me.mnuUserMaster = New System.Windows.Forms.MenuItem()
        Me.mnuUserPermission = New System.Windows.Forms.MenuItem()
        Me.mnuHeadUtility = New System.Windows.Forms.MenuItem()
        Me.MenuItem58 = New System.Windows.Forms.MenuItem()
        Me.MenuItem60 = New System.Windows.Forms.MenuItem()
        Me.MenuItem66 = New System.Windows.Forms.MenuItem()
        Me.mnuRFIDStocktake = New System.Windows.Forms.MenuItem()
        Me.mnuChangePrinter = New System.Windows.Forms.MenuItem()
        Me.mnueTRSTicket = New System.Windows.Forms.MenuItem()
        Me.mnueTRSTicketVoid = New System.Windows.Forms.MenuItem()
        Me.MenuItem5 = New System.Windows.Forms.MenuItem()
        Me.mnuPurchaseTake = New System.Windows.Forms.MenuItem()
        Me.mnuPurchaseTakeReports = New System.Windows.Forms.MenuItem()
        Me.mnuGoldSchemeEntries = New System.Windows.Forms.MenuItem()
        Me.MenuItem67 = New System.Windows.Forms.MenuItem()
        Me.mnuAbout = New System.Windows.Forms.MenuItem()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.UGPSMenu = New System.Windows.Forms.ToolBar()
        Me.sep10 = New System.Windows.Forms.ToolBarButton()
        Me.btnTodaySales = New System.Windows.Forms.ToolBarButton()
        Me.sep8 = New System.Windows.Forms.ToolBarButton()
        Me.btnClass = New System.Windows.Forms.ToolBarButton()
        Me.Sep1 = New System.Windows.Forms.ToolBarButton()
        Me.btnClassCostDisp = New System.Windows.Forms.ToolBarButton()
        Me.sep11 = New System.Windows.Forms.ToolBarButton()
        Me.btnCategory = New System.Windows.Forms.ToolBarButton()
        Me.sep2 = New System.Windows.Forms.ToolBarButton()
        Me.btnCatalogue = New System.Windows.Forms.ToolBarButton()
        Me.Sep3 = New System.Windows.Forms.ToolBarButton()
        Me.btnJobSheet = New System.Windows.Forms.ToolBarButton()
        Me.Sep4 = New System.Windows.Forms.ToolBarButton()
        Me.btnSearch = New System.Windows.Forms.ToolBarButton()
        Me.sep5 = New System.Windows.Forms.ToolBarButton()
        Me.btnOrder = New System.Windows.Forms.ToolBarButton()
        Me.sep6 = New System.Windows.Forms.ToolBarButton()
        Me.btnSales = New System.Windows.Forms.ToolBarButton()
        Me.sep7 = New System.Windows.Forms.ToolBarButton()
        Me.btnBarcode = New System.Windows.Forms.ToolBarButton()
        Me.sep9 = New System.Windows.Forms.ToolBarButton()
        Me.btnQuit = New System.Windows.Forms.ToolBarButton()
        Me.ImageList2 = New System.Windows.Forms.ImageList(Me.components)
        Me.conMenu = New System.Windows.Forms.ContextMenu()
        Me.MenuItem44 = New System.Windows.Forms.MenuItem()
        Me.MenuItem47 = New System.Windows.Forms.MenuItem()
        Me.MenuItem55 = New System.Windows.Forms.MenuItem()
        Me.MenuItem57 = New System.Windows.Forms.MenuItem()
        Me.MenuItem59 = New System.Windows.Forms.MenuItem()
        Me.ConMenuAr = New System.Windows.Forms.ContextMenu()
        Me.MenuItem61 = New System.Windows.Forms.MenuItem()
        Me.MenuItem62 = New System.Windows.Forms.MenuItem()
        Me.MenuItem63 = New System.Windows.Forms.MenuItem()
        Me.MenuItem64 = New System.Windows.Forms.MenuItem()
        Me.MenuItem65 = New System.Windows.Forms.MenuItem()
        Me.sbGPos = New System.Windows.Forms.StatusBar()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel3 = New System.Windows.Forms.StatusBarPanel()
        Me.ObjImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.grpMainHQ = New System.Windows.Forms.GroupBox()
        Me.btnMainHQPos = New System.Windows.Forms.Button()
        Me.btnMainExit = New System.Windows.Forms.Button()
        Me.btnMainBarCode = New System.Windows.Forms.Button()
        Me.btnMainTransferOut = New System.Windows.Forms.Button()
        Me.btnMainTransferIn = New System.Windows.Forms.Button()
        Me.btnMainCreditSales = New System.Windows.Forms.Button()
        Me.btnMainPurchase = New System.Windows.Forms.Button()
        Me.btnMainJobsheet = New System.Windows.Forms.Button()
        Me.btnMainSearch = New System.Windows.Forms.Button()
        Me.btnMainCategory = New System.Windows.Forms.Button()
        Me.btnMainCatalogue = New System.Windows.Forms.Button()
        Me.btnMainClass = New System.Windows.Forms.Button()
        Me.btnMainInventory = New System.Windows.Forms.Button()
        Me.grpMainOutlet = New System.Windows.Forms.GroupBox()
        Me.btnMainOutletBarCode = New System.Windows.Forms.Button()
        Me.btnMainOutletExit = New System.Windows.Forms.Button()
        Me.btnMainOutletDayEnd = New System.Windows.Forms.Button()
        Me.btnMainOutletUpdateHQ = New System.Windows.Forms.Button()
        Me.btnMainOutletSettlement = New System.Windows.Forms.Button()
        Me.btnMainOutletTransferOut = New System.Windows.Forms.Button()
        Me.btnMainOutletTransferIn = New System.Windows.Forms.Button()
        Me.btnMainOutletOrderEntry = New System.Windows.Forms.Button()
        Me.btnMainOutletInstallment = New System.Windows.Forms.Button()
        Me.btnMainOutletInventory = New System.Windows.Forms.Button()
        Me.btnMainOutletSearch = New System.Windows.Forms.Button()
        Me.btnMainOutletPos = New System.Windows.Forms.Button()
        Me.btnMainOutletPaymentEntry = New System.Windows.Forms.Button()
        Me.dgvOrder = New System.Windows.Forms.DataGridView()
        Me.colOrderNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coLocationCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colOrderDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DeliveryDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCustomerCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMobile = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cSupplier = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvInstallment = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cLocation = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Balance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Value = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvunfixJobsheet = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grpReOrder = New System.Windows.Forms.GroupBox()
        Me.dgvReOrder = New System.Windows.Forms.DataGridView()
        Me.cLocationCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.mnuMaster2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChangePasswordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.txtGST = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtTotalValue = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtCostPerGram = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtInventoryCode = New System.Windows.Forms.TextBox()
        Me.txtSMC = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtWeight = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnInventoryCodeSearch = New System.Windows.Forms.Button()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpMainHQ.SuspendLayout()
        Me.grpMainOutlet.SuspendLayout()
        CType(Me.dgvOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvInstallment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvunfixJobsheet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpReOrder.SuspendLayout()
        CType(Me.dgvReOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ImageList
        '
        Me.ImageList.ImageStream = CType(resources.GetObject("ImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList.Images.SetKeyName(0, "")
        Me.ImageList.Images.SetKeyName(1, "")
        '
        'MainMenu2
        '
        Me.MainMenu2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuMaster, Me.MenuItem3, Me.mnuMemberModule, Me.mnuPurchaseHeader, Me.mnuHeadSales, Me.mnuHeadTransaction, Me.mnuHeadTransfer, Me.mnuHeadReports, Me.mnuHeadAccounts, Me.mnuDailyProcedure, Me.mnuSecurity, Me.mnuHeadUtility, Me.MenuItem67})
        '
        'mnuMaster
        '
        Me.mnuMaster.Index = 0
        Me.mnuMaster.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuChangePassword, Me.MenuItem101, Me.mnuExit})
        Me.mnuMaster.Text = "File"
        '
        'mnuChangePassword
        '
        Me.mnuChangePassword.Index = 0
        Me.mnuChangePassword.Text = "Change Password"
        '
        'MenuItem101
        '
        Me.MenuItem101.Index = 1
        Me.MenuItem101.Text = "-"
        '
        'mnuExit
        '
        Me.mnuExit.Index = 2
        Me.mnuExit.Text = "Exit"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 1
        Me.MenuItem3.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuMasterCompany, Me.MenuItem2, Me.mnuMasterInventory, Me.MenuItem4, Me.mnuMasterProcrument, Me.MenuItem75, Me.MenuItem34, Me.mnuMasterIPM, Me.mnuCurrency, Me.MenuItem6, Me.mnuPayOutMaster, Me.MenuItem70, Me.mnuReOrderLevelMaster, Me.mnuSchemeMaster})
        Me.MenuItem3.Text = "Master"
        '
        'mnuMasterCompany
        '
        Me.mnuMasterCompany.Index = 0
        Me.mnuMasterCompany.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuCompany, Me.mnuLocation, Me.mnuTerminal})
        Me.mnuMasterCompany.Text = "Company"
        '
        'mnuCompany
        '
        Me.mnuCompany.Index = 0
        Me.mnuCompany.Text = "Company"
        '
        'mnuLocation
        '
        Me.mnuLocation.Index = 1
        Me.mnuLocation.Text = "Location"
        '
        'mnuTerminal
        '
        Me.mnuTerminal.Index = 2
        Me.mnuTerminal.Text = "Terminal"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 1
        Me.MenuItem2.Text = "-"
        '
        'mnuMasterInventory
        '
        Me.mnuMasterInventory.Index = 2
        Me.mnuMasterInventory.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuClass, Me.mnuCategory, Me.mnuCatalogue, Me.mnuClassRate, Me.mnuTrayMaster, Me.mnuSize})
        Me.mnuMasterInventory.Text = "Inventory"
        '
        'mnuClass
        '
        Me.mnuClass.Index = 0
        Me.mnuClass.Text = "Class"
        '
        'mnuCategory
        '
        Me.mnuCategory.Index = 1
        Me.mnuCategory.Text = "Category"
        '
        'mnuCatalogue
        '
        Me.mnuCatalogue.Index = 2
        Me.mnuCatalogue.Text = "Catalogue"
        '
        'mnuClassRate
        '
        Me.mnuClassRate.Index = 3
        Me.mnuClassRate.Text = "Daily Class Rate"
        '
        'mnuTrayMaster
        '
        Me.mnuTrayMaster.Index = 4
        Me.mnuTrayMaster.Text = "Tray Master"
        '
        'mnuSize
        '
        Me.mnuSize.Index = 5
        Me.mnuSize.Text = "Size"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 3
        Me.MenuItem4.Text = "-"
        '
        'mnuMasterProcrument
        '
        Me.mnuMasterProcrument.Index = 4
        Me.mnuMasterProcrument.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuSupplier})
        Me.mnuMasterProcrument.Text = "Procrument"
        '
        'mnuSupplier
        '
        Me.mnuSupplier.Index = 0
        Me.mnuSupplier.Text = "Supplier"
        '
        'MenuItem75
        '
        Me.MenuItem75.Index = 5
        Me.MenuItem75.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuSalesman, Me.mnuPaymode, Me.mnuCustomer, Me.mnuOutletCustomers, Me.mnuCustomerPointAdjustment, Me.mnuCustomerTypeChange})
        Me.MenuItem75.Text = "Sales"
        '
        'mnuSalesman
        '
        Me.mnuSalesman.Index = 0
        Me.mnuSalesman.Text = "SalesMan"
        '
        'mnuPaymode
        '
        Me.mnuPaymode.Index = 1
        Me.mnuPaymode.Text = "Paymode"
        '
        'mnuCustomer
        '
        Me.mnuCustomer.Index = 2
        Me.mnuCustomer.Text = "Customer"
        '
        'mnuOutletCustomers
        '
        Me.mnuOutletCustomers.Index = 3
        Me.mnuOutletCustomers.Text = "Outlet Customers"
        '
        'mnuCustomerPointAdjustment
        '
        Me.mnuCustomerPointAdjustment.Index = 4
        Me.mnuCustomerPointAdjustment.Text = "Customer Points Adjustment"
        '
        'mnuCustomerTypeChange
        '
        Me.mnuCustomerTypeChange.Index = 5
        Me.mnuCustomerTypeChange.Text = "Customer Type Change"
        '
        'MenuItem34
        '
        Me.MenuItem34.Index = 6
        Me.MenuItem34.Text = "-"
        '
        'mnuMasterIPM
        '
        Me.mnuMasterIPM.Index = 7
        Me.mnuMasterIPM.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuRefiner, Me.mnuCountry})
        Me.mnuMasterIPM.Text = "IPM"
        '
        'mnuRefiner
        '
        Me.mnuRefiner.Index = 0
        Me.mnuRefiner.Text = "Refiner"
        '
        'mnuCountry
        '
        Me.mnuCountry.Index = 1
        Me.mnuCountry.Text = "Country"
        '
        'mnuCurrency
        '
        Me.mnuCurrency.Index = 8
        Me.mnuCurrency.Text = "Currency"
        Me.mnuCurrency.Visible = False
        '
        'MenuItem6
        '
        Me.MenuItem6.Index = 9
        Me.MenuItem6.Text = "-"
        Me.MenuItem6.Visible = False
        '
        'mnuPayOutMaster
        '
        Me.mnuPayOutMaster.Index = 10
        Me.mnuPayOutMaster.Text = "PayOut Master"
        '
        'MenuItem70
        '
        Me.MenuItem70.Index = 11
        Me.MenuItem70.Text = "-"
        '
        'mnuReOrderLevelMaster
        '
        Me.mnuReOrderLevelMaster.Index = 12
        Me.mnuReOrderLevelMaster.Text = "ReOrder Level"
        '
        'mnuSchemeMaster
        '
        Me.mnuSchemeMaster.Index = 13
        Me.mnuSchemeMaster.Text = "Scheme Master"
        '
        'mnuMemberModule
        '
        Me.mnuMemberModule.Index = 2
        Me.mnuMemberModule.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuMemberType, Me.MenuItem52, Me.mnuNricType, Me.MenuItem53, Me.mnuMember, Me.MenuItem54, Me.mnuMemberReports})
        Me.mnuMemberModule.Text = "Member"
        '
        'mnuMemberType
        '
        Me.mnuMemberType.Index = 0
        Me.mnuMemberType.Text = "Member Type"
        '
        'MenuItem52
        '
        Me.MenuItem52.Index = 1
        Me.MenuItem52.Text = "-"
        '
        'mnuNricType
        '
        Me.mnuNricType.Index = 2
        Me.mnuNricType.Text = "NRIC Type"
        '
        'MenuItem53
        '
        Me.MenuItem53.Index = 3
        Me.MenuItem53.Text = "-"
        '
        'mnuMember
        '
        Me.mnuMember.Index = 4
        Me.mnuMember.Text = "Member "
        '
        'MenuItem54
        '
        Me.MenuItem54.Index = 5
        Me.MenuItem54.Text = "-"
        '
        'mnuMemberReports
        '
        Me.mnuMemberReports.Index = 6
        Me.mnuMemberReports.Text = "Member Reports"
        '
        'mnuPurchaseHeader
        '
        Me.mnuPurchaseHeader.Index = 3
        Me.mnuPurchaseHeader.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuJobSheet, Me.mnuPurchase, Me.mnuDirectPurchase, Me.mnuFixCostJobSheet, Me.MenuItem7, Me.mnuBarCode, Me.mnuJewellaryBarcode, Me.mnuSpecialBarcode, Me.MenuItem10, Me.mnuInventorySearch, Me.mnuInventoryMaintenance, Me.mnuInventoryMovement, Me.mnuCatalogueSearch, Me.MenuItem36, Me.mnuSplitInventory, Me.mnuMergeInventory, Me.MenuItem9, Me.mnuPurchaseReturn, Me.MenuItem71, Me.mnuSalesOrder, Me.mnuGoldBarFix, Me.mnuSupplierMC, Me.mnuPurchaseTally, Me.mnuSupplierPayment, Me.mnuSupplierPaymentAmendment, Me.mnuPurchaseEntry})
        Me.mnuPurchaseHeader.Text = "Purchase"
        '
        'mnuJobSheet
        '
        Me.mnuJobSheet.Index = 0
        Me.mnuJobSheet.Text = "JobSheet"
        '
        'mnuPurchase
        '
        Me.mnuPurchase.Index = 1
        Me.mnuPurchase.Text = "Purchase"
        '
        'mnuDirectPurchase
        '
        Me.mnuDirectPurchase.Index = 2
        Me.mnuDirectPurchase.Text = "Direct Purchase"
        '
        'mnuFixCostJobSheet
        '
        Me.mnuFixCostJobSheet.Index = 3
        Me.mnuFixCostJobSheet.Text = "JobSheet Costing"
        '
        'MenuItem7
        '
        Me.MenuItem7.Index = 4
        Me.MenuItem7.Text = "-"
        '
        'mnuBarCode
        '
        Me.mnuBarCode.Index = 5
        Me.mnuBarCode.Text = "Barcode"
        '
        'mnuJewellaryBarcode
        '
        Me.mnuJewellaryBarcode.Index = 6
        Me.mnuJewellaryBarcode.Text = "Jewellary Barcode"
        '
        'mnuSpecialBarcode
        '
        Me.mnuSpecialBarcode.Index = 7
        Me.mnuSpecialBarcode.Text = "Special Barcode"
        '
        'MenuItem10
        '
        Me.MenuItem10.Index = 8
        Me.MenuItem10.Text = "-"
        '
        'mnuInventorySearch
        '
        Me.mnuInventorySearch.Index = 9
        Me.mnuInventorySearch.Text = "Inventory Search"
        '
        'mnuInventoryMaintenance
        '
        Me.mnuInventoryMaintenance.Index = 10
        Me.mnuInventoryMaintenance.Text = "Inventory Maintenance"
        '
        'mnuInventoryMovement
        '
        Me.mnuInventoryMovement.Index = 11
        Me.mnuInventoryMovement.Text = "Inventory Movement"
        '
        'mnuCatalogueSearch
        '
        Me.mnuCatalogueSearch.Index = 12
        Me.mnuCatalogueSearch.Text = "Catalogue Search"
        '
        'MenuItem36
        '
        Me.MenuItem36.Index = 13
        Me.MenuItem36.Text = "-"
        '
        'mnuSplitInventory
        '
        Me.mnuSplitInventory.Index = 14
        Me.mnuSplitInventory.Text = "Split Inventory"
        '
        'mnuMergeInventory
        '
        Me.mnuMergeInventory.Index = 15
        Me.mnuMergeInventory.Text = "Merge Inventory"
        '
        'MenuItem9
        '
        Me.MenuItem9.Index = 16
        Me.MenuItem9.Text = "-"
        '
        'mnuPurchaseReturn
        '
        Me.mnuPurchaseReturn.Index = 17
        Me.mnuPurchaseReturn.Text = "Purchase Return"
        '
        'MenuItem71
        '
        Me.MenuItem71.Index = 18
        Me.MenuItem71.Text = "-"
        '
        'mnuSalesOrder
        '
        Me.mnuSalesOrder.Index = 19
        Me.mnuSalesOrder.Text = "Sales Order"
        '
        'mnuGoldBarFix
        '
        Me.mnuGoldBarFix.Index = 20
        Me.mnuGoldBarFix.Text = "GoldBar Fix"
        '
        'mnuSupplierMC
        '
        Me.mnuSupplierMC.Index = 21
        Me.mnuSupplierMC.Text = "Suppler MC"
        '
        'mnuPurchaseTally
        '
        Me.mnuPurchaseTally.Index = 22
        Me.mnuPurchaseTally.Text = "Purchase Tally"
        '
        'mnuSupplierPayment
        '
        Me.mnuSupplierPayment.Index = 23
        Me.mnuSupplierPayment.Text = "Supplier Payment"
        '
        'mnuSupplierPaymentAmendment
        '
        Me.mnuSupplierPaymentAmendment.Index = 24
        Me.mnuSupplierPaymentAmendment.Text = "Supplier Payment Amendment"
        '
        'mnuPurchaseEntry
        '
        Me.mnuPurchaseEntry.Index = 25
        Me.mnuPurchaseEntry.Text = "Purchase Entry"
        '
        'mnuHeadSales
        '
        Me.mnuHeadSales.Index = 4
        Me.mnuHeadSales.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuPOS, Me.mnuPaymentEntries, Me.mnuPaymodeModification, Me.mnuWholeSalePOS, Me.MenuItem8, Me.MenuItem11, Me.mnuTradeInSales, Me.MenuItem19, Me.mnuConsignmentSales, Me.MenuItem69, Me.MenuItem68, Me.MenuItem81, Me.MenuItem82, Me.mnuSalesReturn, Me.mnuPayOut, Me.mnuPaymentEntryAksayaTritiya, Me.mnuSalesHistory, Me.mnuCreditSalesPayment, Me.mnuDeposit, Me.mnuDailySalesEntry})
        Me.mnuHeadSales.Text = "Sales"
        '
        'mnuPOS
        '
        Me.mnuPOS.Index = 0
        Me.mnuPOS.Text = "POS"
        '
        'mnuPaymentEntries
        '
        Me.mnuPaymentEntries.Index = 1
        Me.mnuPaymentEntries.Text = "Payment Entry"
        '
        'mnuPaymodeModification
        '
        Me.mnuPaymodeModification.Index = 2
        Me.mnuPaymodeModification.Text = "Paymode Modification"
        '
        'mnuWholeSalePOS
        '
        Me.mnuWholeSalePOS.Index = 3
        Me.mnuWholeSalePOS.Text = "WholeSale POS"
        '
        'MenuItem8
        '
        Me.MenuItem8.Index = 4
        Me.MenuItem8.Text = "-"
        Me.MenuItem8.Visible = False
        '
        'MenuItem11
        '
        Me.MenuItem11.Index = 5
        Me.MenuItem11.Text = "-"
        Me.MenuItem11.Visible = False
        '
        'mnuTradeInSales
        '
        Me.mnuTradeInSales.Index = 6
        Me.mnuTradeInSales.Text = "Trade In Sales"
        '
        'MenuItem19
        '
        Me.MenuItem19.Index = 7
        Me.MenuItem19.Text = "-"
        Me.MenuItem19.Visible = False
        '
        'mnuConsignmentSales
        '
        Me.mnuConsignmentSales.Index = 8
        Me.mnuConsignmentSales.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuConSales, Me.MenuItem23, Me.mnuConSalesRet})
        Me.mnuConsignmentSales.Text = "Consignment Sales"
        Me.mnuConsignmentSales.Visible = False
        '
        'mnuConSales
        '
        Me.mnuConSales.Index = 0
        Me.mnuConSales.Text = "Consignment Sales "
        '
        'MenuItem23
        '
        Me.MenuItem23.Index = 1
        Me.MenuItem23.Text = "-"
        '
        'mnuConSalesRet
        '
        Me.mnuConSalesRet.Index = 2
        Me.mnuConSalesRet.Text = "Consignment Sales Return"
        '
        'MenuItem69
        '
        Me.MenuItem69.Index = 9
        Me.MenuItem69.Text = "-"
        Me.MenuItem69.Visible = False
        '
        'MenuItem68
        '
        Me.MenuItem68.Index = 10
        Me.MenuItem68.Text = "Misc"
        '
        'MenuItem81
        '
        Me.MenuItem81.Index = 11
        Me.MenuItem81.Text = "-"
        Me.MenuItem81.Visible = False
        '
        'MenuItem82
        '
        Me.MenuItem82.Index = 12
        Me.MenuItem82.Text = "-"
        '
        'mnuSalesReturn
        '
        Me.mnuSalesReturn.Index = 13
        Me.mnuSalesReturn.Text = "Sales Return"
        '
        'mnuPayOut
        '
        Me.mnuPayOut.Index = 14
        Me.mnuPayOut.Text = "Pay Out"
        '
        'mnuPaymentEntryAksayaTritiya
        '
        Me.mnuPaymentEntryAksayaTritiya.Index = 15
        Me.mnuPaymentEntryAksayaTritiya.Text = "Payment Entry AksayaTritiya"
        '
        'mnuSalesHistory
        '
        Me.mnuSalesHistory.Index = 16
        Me.mnuSalesHistory.Text = "Sales History"
        '
        'mnuCreditSalesPayment
        '
        Me.mnuCreditSalesPayment.Index = 17
        Me.mnuCreditSalesPayment.Text = "Credit Sales Payment"
        '
        'mnuDeposit
        '
        Me.mnuDeposit.Index = 18
        Me.mnuDeposit.Text = "Deposit"
        '
        'mnuDailySalesEntry
        '
        Me.mnuDailySalesEntry.Index = 19
        Me.mnuDailySalesEntry.Text = "Daily Sales Entry"
        '
        'mnuHeadTransaction
        '
        Me.mnuHeadTransaction.Index = 5
        Me.mnuHeadTransaction.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuMiscellaneousEntry, Me.mnuInstallmentEntry, Me.mnuInstallment, Me.MenuItem84, Me.mnuOrderInventory, Me.mnuOrderPayment1, Me.MenuItem83, Me.mnuOrderEntry, Me.mnuRepairEntry, Me.mnuRepairPayment, Me.MenuItem85, Me.mnuOrderPayment, Me.mnuTrayTransfer, Me.mnuPaymentEntry, Me.MenuItem12, Me.mnuGoldBarPurchase, Me.mnuGoldBarIssue, Me.mnuTradeInGoldIssue, Me.MenuItem31, Me.mnuGSTRefund, Me.mnuTradeInMerge, Me.mnuTradeInTransfer, Me.mnuTradeInSplit, Me.mnuTradeInWholeSales, Me.MenuItem1, Me.mnuCustomerDeposit})
        Me.mnuHeadTransaction.Text = "Transaction"
        '
        'mnuMiscellaneousEntry
        '
        Me.mnuMiscellaneousEntry.Enabled = False
        Me.mnuMiscellaneousEntry.Index = 0
        Me.mnuMiscellaneousEntry.Text = "Miscellaneous Entry"
        Me.mnuMiscellaneousEntry.Visible = False
        '
        'mnuInstallmentEntry
        '
        Me.mnuInstallmentEntry.Index = 1
        Me.mnuInstallmentEntry.Text = "Installment/Order Entry"
        '
        'mnuInstallment
        '
        Me.mnuInstallment.Index = 2
        Me.mnuInstallment.Text = "Installment Payment"
        '
        'MenuItem84
        '
        Me.MenuItem84.Index = 3
        Me.MenuItem84.Text = "-"
        '
        'mnuOrderInventory
        '
        Me.mnuOrderInventory.Index = 4
        Me.mnuOrderInventory.Text = "Order Inventory"
        '
        'mnuOrderPayment1
        '
        Me.mnuOrderPayment1.Index = 5
        Me.mnuOrderPayment1.Text = "Order Payment"
        '
        'MenuItem83
        '
        Me.MenuItem83.Index = 6
        Me.MenuItem83.Text = "-"
        '
        'mnuOrderEntry
        '
        Me.mnuOrderEntry.Index = 7
        Me.mnuOrderEntry.Text = "Order Entry"
        Me.mnuOrderEntry.Visible = False
        '
        'mnuRepairEntry
        '
        Me.mnuRepairEntry.Index = 8
        Me.mnuRepairEntry.Text = "Repair Entry"
        '
        'mnuRepairPayment
        '
        Me.mnuRepairPayment.Index = 9
        Me.mnuRepairPayment.Text = "Repair Payment"
        '
        'MenuItem85
        '
        Me.MenuItem85.Index = 10
        Me.MenuItem85.Text = "-"
        '
        'mnuOrderPayment
        '
        Me.mnuOrderPayment.Index = 11
        Me.mnuOrderPayment.Text = "Order Payment"
        Me.mnuOrderPayment.Visible = False
        '
        'mnuTrayTransfer
        '
        Me.mnuTrayTransfer.Index = 12
        Me.mnuTrayTransfer.Text = "Tray Transfer"
        '
        'mnuPaymentEntry
        '
        Me.mnuPaymentEntry.Enabled = False
        Me.mnuPaymentEntry.Index = 13
        Me.mnuPaymentEntry.Text = "Payment Entry"
        Me.mnuPaymentEntry.Visible = False
        '
        'MenuItem12
        '
        Me.MenuItem12.Index = 14
        Me.MenuItem12.Text = "-"
        '
        'mnuGoldBarPurchase
        '
        Me.mnuGoldBarPurchase.Index = 15
        Me.mnuGoldBarPurchase.Text = "GoldBar Purchase"
        Me.mnuGoldBarPurchase.Visible = False
        '
        'mnuGoldBarIssue
        '
        Me.mnuGoldBarIssue.Index = 16
        Me.mnuGoldBarIssue.Text = "GoldBar Issue"
        Me.mnuGoldBarIssue.Visible = False
        '
        'mnuTradeInGoldIssue
        '
        Me.mnuTradeInGoldIssue.Index = 17
        Me.mnuTradeInGoldIssue.Text = "Trade In Gold Issue"
        '
        'MenuItem31
        '
        Me.MenuItem31.Index = 18
        Me.MenuItem31.Text = "-"
        Me.MenuItem31.Visible = False
        '
        'mnuGSTRefund
        '
        Me.mnuGSTRefund.Enabled = False
        Me.mnuGSTRefund.Index = 19
        Me.mnuGSTRefund.Text = "GST Refund"
        Me.mnuGSTRefund.Visible = False
        '
        'mnuTradeInMerge
        '
        Me.mnuTradeInMerge.Index = 20
        Me.mnuTradeInMerge.Text = "Trade In Merge"
        Me.mnuTradeInMerge.Visible = False
        '
        'mnuTradeInTransfer
        '
        Me.mnuTradeInTransfer.Index = 21
        Me.mnuTradeInTransfer.Text = "Trade In Transfer"
        Me.mnuTradeInTransfer.Visible = False
        '
        'mnuTradeInSplit
        '
        Me.mnuTradeInSplit.Index = 22
        Me.mnuTradeInSplit.Text = "Trade In Split"
        Me.mnuTradeInSplit.Visible = False
        '
        'mnuTradeInWholeSales
        '
        Me.mnuTradeInWholeSales.Index = 23
        Me.mnuTradeInWholeSales.Text = "Trade In Issue"
        Me.mnuTradeInWholeSales.Visible = False
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 24
        Me.MenuItem1.Text = "-"
        '
        'mnuCustomerDeposit
        '
        Me.mnuCustomerDeposit.Index = 25
        Me.mnuCustomerDeposit.Text = "Customer Deposit"
        Me.mnuCustomerDeposit.Visible = False
        '
        'mnuHeadTransfer
        '
        Me.mnuHeadTransfer.Index = 6
        Me.mnuHeadTransfer.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem78, Me.MenuItem79, Me.MenuItem80})
        Me.mnuHeadTransfer.Text = "Transfer"
        '
        'MenuItem78
        '
        Me.MenuItem78.Index = 0
        Me.MenuItem78.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuTransferOut, Me.mnuTransferIn, Me.mnuTransferStatus, Me.mnuOnlineTransfer, Me.mnuSTockTradeInTransfer})
        Me.MenuItem78.Text = "Transaction"
        '
        'mnuTransferOut
        '
        Me.mnuTransferOut.Index = 0
        Me.mnuTransferOut.Text = "Transfer Out"
        '
        'mnuTransferIn
        '
        Me.mnuTransferIn.Index = 1
        Me.mnuTransferIn.Text = "Transfer In"
        '
        'mnuTransferStatus
        '
        Me.mnuTransferStatus.Index = 2
        Me.mnuTransferStatus.Text = "Transfer Status"
        '
        'mnuOnlineTransfer
        '
        Me.mnuOnlineTransfer.Index = 3
        Me.mnuOnlineTransfer.Text = "Online Transfer"
        '
        'mnuSTockTradeInTransfer
        '
        Me.mnuSTockTradeInTransfer.Index = 4
        Me.mnuSTockTradeInTransfer.Text = "Stock TradeIn Transfer"
        '
        'MenuItem79
        '
        Me.MenuItem79.Index = 1
        Me.MenuItem79.Text = "-"
        '
        'MenuItem80
        '
        Me.MenuItem80.Index = 2
        Me.MenuItem80.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuTransferReports})
        Me.MenuItem80.Text = "Reports"
        '
        'mnuTransferReports
        '
        Me.mnuTransferReports.Index = 0
        Me.mnuTransferReports.Text = "Transfer Reports"
        '
        'mnuHeadReports
        '
        Me.mnuHeadReports.Index = 7
        Me.mnuHeadReports.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuMasterReports, Me.mnuCustomerReports, Me.mnuInventoryReports, Me.mnuPurchaseReports, Me.mnuStockReports, Me.mnuSalesReports, Me.mnuTransactionReports, Me.mnuGoldbarReports, Me.mnuDailySales, Me.MenuItem13, Me.mnuSettlementReports, Me.mnuEODReports, Me.MenuItem14, Me.mnuManagementReports, Me.mnuAnalysisReports, Me.MenuItem16, Me.mnuAuditingReports, Me.Manageforotheres, Me.mnuTradeInReports})
        Me.mnuHeadReports.Text = "Reports"
        '
        'mnuMasterReports
        '
        Me.mnuMasterReports.Index = 0
        Me.mnuMasterReports.Text = "Master Reports"
        '
        'mnuCustomerReports
        '
        Me.mnuCustomerReports.Index = 1
        Me.mnuCustomerReports.Text = "Customer Reports"
        '
        'mnuInventoryReports
        '
        Me.mnuInventoryReports.Index = 2
        Me.mnuInventoryReports.Text = "Inventory Reports"
        '
        'mnuPurchaseReports
        '
        Me.mnuPurchaseReports.Index = 3
        Me.mnuPurchaseReports.Text = "Purchase Reports"
        '
        'mnuStockReports
        '
        Me.mnuStockReports.Enabled = False
        Me.mnuStockReports.Index = 4
        Me.mnuStockReports.Text = "Stock Reports"
        Me.mnuStockReports.Visible = False
        '
        'mnuSalesReports
        '
        Me.mnuSalesReports.Index = 5
        Me.mnuSalesReports.Text = "Sales Reports"
        '
        'mnuTransactionReports
        '
        Me.mnuTransactionReports.Index = 6
        Me.mnuTransactionReports.Text = "Transaction Reports"
        '
        'mnuGoldbarReports
        '
        Me.mnuGoldbarReports.Index = 7
        Me.mnuGoldbarReports.Text = "Goldbar Reports"
        '
        'mnuDailySales
        '
        Me.mnuDailySales.Enabled = False
        Me.mnuDailySales.Index = 8
        Me.mnuDailySales.Text = "Daily Sales Reports"
        Me.mnuDailySales.Visible = False
        '
        'MenuItem13
        '
        Me.MenuItem13.Index = 9
        Me.MenuItem13.Text = "-"
        '
        'mnuSettlementReports
        '
        Me.mnuSettlementReports.Index = 10
        Me.mnuSettlementReports.Text = "Settlement Reports"
        Me.mnuSettlementReports.Visible = False
        '
        'mnuEODReports
        '
        Me.mnuEODReports.Index = 11
        Me.mnuEODReports.Text = "EOD History Reports"
        '
        'MenuItem14
        '
        Me.MenuItem14.Index = 12
        Me.MenuItem14.Text = "-"
        '
        'mnuManagementReports
        '
        Me.mnuManagementReports.Index = 13
        Me.mnuManagementReports.Text = "Management Reports"
        '
        'mnuAnalysisReports
        '
        Me.mnuAnalysisReports.Index = 14
        Me.mnuAnalysisReports.Text = "Analysis Reports"
        '
        'MenuItem16
        '
        Me.MenuItem16.Enabled = False
        Me.MenuItem16.Index = 15
        Me.MenuItem16.Text = "-"
        Me.MenuItem16.Visible = False
        '
        'mnuAuditingReports
        '
        Me.mnuAuditingReports.Enabled = False
        Me.mnuAuditingReports.Index = 16
        Me.mnuAuditingReports.Text = "Auditing Reports"
        Me.mnuAuditingReports.Visible = False
        '
        'Manageforotheres
        '
        Me.Manageforotheres.Index = 17
        Me.Manageforotheres.Text = "Management Reports For Others"
        Me.Manageforotheres.Visible = False
        '
        'mnuTradeInReports
        '
        Me.mnuTradeInReports.Index = 18
        Me.mnuTradeInReports.Text = "TradeIn Reports"
        '
        'mnuHeadAccounts
        '
        Me.mnuHeadAccounts.Index = 8
        Me.mnuHeadAccounts.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuAccountMaster, Me.mnuAPSystemSettings, Me.MenuItem17, Me.mnuAccountPayable, Me.MenuItem26, Me.MenuItem35, Me.MenuItem37, Me.mnuGeneralLedger})
        Me.mnuHeadAccounts.Text = "Accounts"
        '
        'mnuAccountMaster
        '
        Me.mnuAccountMaster.Index = 0
        Me.mnuAccountMaster.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuAccountControl, Me.mnuAccountType, Me.mnuBankMaster, Me.MenuItem15, Me.mnuGroup, Me.mnuCOAGroup, Me.mnuCOA, Me.MenuItem32, Me.mnuAgingDaysSettings, Me.mnuPaymodeLedgerSettings, Me.MenuItem56, Me.MnuAccMasterReports})
        Me.mnuAccountMaster.Text = "Account Master"
        '
        'mnuAccountControl
        '
        Me.mnuAccountControl.Index = 0
        Me.mnuAccountControl.Text = "Account Control"
        '
        'mnuAccountType
        '
        Me.mnuAccountType.Index = 1
        Me.mnuAccountType.Text = "Account Type"
        '
        'mnuBankMaster
        '
        Me.mnuBankMaster.Index = 2
        Me.mnuBankMaster.Text = "Bank Master"
        '
        'MenuItem15
        '
        Me.MenuItem15.Index = 3
        Me.MenuItem15.Text = "-"
        '
        'mnuGroup
        '
        Me.mnuGroup.Index = 4
        Me.mnuGroup.Text = "Group"
        '
        'mnuCOAGroup
        '
        Me.mnuCOAGroup.Index = 5
        Me.mnuCOAGroup.Text = "COA Group"
        '
        'mnuCOA
        '
        Me.mnuCOA.Index = 6
        Me.mnuCOA.Text = "COA"
        '
        'MenuItem32
        '
        Me.MenuItem32.Index = 7
        Me.MenuItem32.Text = "-"
        '
        'mnuAgingDaysSettings
        '
        Me.mnuAgingDaysSettings.Index = 8
        Me.mnuAgingDaysSettings.Text = "Aging Days Settings"
        '
        'mnuPaymodeLedgerSettings
        '
        Me.mnuPaymodeLedgerSettings.Index = 9
        Me.mnuPaymodeLedgerSettings.Text = "Paymode Ledger Settings"
        '
        'MenuItem56
        '
        Me.MenuItem56.Index = 10
        Me.MenuItem56.Text = "-"
        '
        'MnuAccMasterReports
        '
        Me.MnuAccMasterReports.Index = 11
        Me.MnuAccMasterReports.Text = "Reports"
        '
        'mnuAPSystemSettings
        '
        Me.mnuAPSystemSettings.Index = 1
        Me.mnuAPSystemSettings.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuAPControlLedgerSettings, Me.mnuARControlLedgerSettings})
        Me.mnuAPSystemSettings.Text = "Control Ledger Settings"
        '
        'mnuAPControlLedgerSettings
        '
        Me.mnuAPControlLedgerSettings.Index = 0
        Me.mnuAPControlLedgerSettings.Text = "AP Control Ledger Settings"
        '
        'mnuARControlLedgerSettings
        '
        Me.mnuARControlLedgerSettings.Index = 1
        Me.mnuARControlLedgerSettings.Text = "AR Control Ledger Settings"
        '
        'MenuItem17
        '
        Me.MenuItem17.Index = 2
        Me.MenuItem17.Text = "-"
        '
        'mnuAccountPayable
        '
        Me.mnuAccountPayable.Index = 3
        Me.mnuAccountPayable.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuPaymenttoOthers, Me.mnuPaymenttoVendor, Me.MenuItem18, Me.mnuDebitNote, Me.mnuCreditNote, Me.MenuItem20, Me.mnuGroupPosting, Me.MenuItem24, Me.mnuAPReports})
        Me.mnuAccountPayable.Text = "Account Payable"
        '
        'mnuPaymenttoOthers
        '
        Me.mnuPaymenttoOthers.Index = 0
        Me.mnuPaymenttoOthers.Text = "Payment to Others"
        '
        'mnuPaymenttoVendor
        '
        Me.mnuPaymenttoVendor.Index = 1
        Me.mnuPaymenttoVendor.Text = "Payment to Vendor"
        '
        'MenuItem18
        '
        Me.MenuItem18.Index = 2
        Me.MenuItem18.Text = "-"
        '
        'mnuDebitNote
        '
        Me.mnuDebitNote.Index = 3
        Me.mnuDebitNote.Text = "Debit Note"
        '
        'mnuCreditNote
        '
        Me.mnuCreditNote.Index = 4
        Me.mnuCreditNote.Text = "Credit Note"
        '
        'MenuItem20
        '
        Me.MenuItem20.Index = 5
        Me.MenuItem20.Text = "-"
        '
        'mnuGroupPosting
        '
        Me.mnuGroupPosting.Index = 6
        Me.mnuGroupPosting.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuPurchaseInvoicePosting, Me.mnuPurchaseReturnPosting, Me.MenuItem21, Me.mnuPaymentPosting, Me.mnuPaymentOthersPosting, Me.MenuItem22, Me.mnuDebitNotePosting, Me.mnuCreditNotePosting})
        Me.mnuGroupPosting.Text = "Group Posting"
        '
        'mnuPurchaseInvoicePosting
        '
        Me.mnuPurchaseInvoicePosting.Index = 0
        Me.mnuPurchaseInvoicePosting.Text = "Purchase Invoice Posting (Group)"
        '
        'mnuPurchaseReturnPosting
        '
        Me.mnuPurchaseReturnPosting.Index = 1
        Me.mnuPurchaseReturnPosting.Text = "Purchase Return Posting (Group)"
        '
        'MenuItem21
        '
        Me.MenuItem21.Index = 2
        Me.MenuItem21.Text = "-"
        '
        'mnuPaymentPosting
        '
        Me.mnuPaymentPosting.Index = 3
        Me.mnuPaymentPosting.Text = "Payment Posting (Group)"
        '
        'mnuPaymentOthersPosting
        '
        Me.mnuPaymentOthersPosting.Index = 4
        Me.mnuPaymentOthersPosting.Text = "Payment Others Posting (Group)"
        '
        'MenuItem22
        '
        Me.MenuItem22.Index = 5
        Me.MenuItem22.Text = "-"
        '
        'mnuDebitNotePosting
        '
        Me.mnuDebitNotePosting.Index = 6
        Me.mnuDebitNotePosting.Text = "Debit Note Posting (Group)"
        '
        'mnuCreditNotePosting
        '
        Me.mnuCreditNotePosting.Index = 7
        Me.mnuCreditNotePosting.Text = "Credit Note Posting (Group)"
        '
        'MenuItem24
        '
        Me.MenuItem24.Index = 7
        Me.MenuItem24.Text = "-"
        '
        'mnuAPReports
        '
        Me.mnuAPReports.Index = 8
        Me.mnuAPReports.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuPaymentReport, Me.MenuItem27, Me.mnuDebitNoteReport, Me.mnuCreditNoteReport, Me.MenuItem25, Me.mnuAgingReport, Me.mnuBalanceReport, Me.MenuItem33, Me.mnuGSTReport, Me.mnuPurchaseReturnPost})
        Me.mnuAPReports.Text = "Reports"
        '
        'mnuPaymentReport
        '
        Me.mnuPaymentReport.Index = 0
        Me.mnuPaymentReport.Text = "Payment Report"
        '
        'MenuItem27
        '
        Me.MenuItem27.Index = 1
        Me.MenuItem27.Text = "-"
        '
        'mnuDebitNoteReport
        '
        Me.mnuDebitNoteReport.Index = 2
        Me.mnuDebitNoteReport.Text = "Debit Note Report"
        '
        'mnuCreditNoteReport
        '
        Me.mnuCreditNoteReport.Index = 3
        Me.mnuCreditNoteReport.Text = "Credit Note Report"
        '
        'MenuItem25
        '
        Me.MenuItem25.Index = 4
        Me.MenuItem25.Text = "-"
        '
        'mnuAgingReport
        '
        Me.mnuAgingReport.Index = 5
        Me.mnuAgingReport.Text = "Aging Report"
        '
        'mnuBalanceReport
        '
        Me.mnuBalanceReport.Index = 6
        Me.mnuBalanceReport.Text = "Balance Report"
        '
        'MenuItem33
        '
        Me.MenuItem33.Index = 7
        Me.MenuItem33.Text = "-"
        '
        'mnuGSTReport
        '
        Me.mnuGSTReport.Index = 8
        Me.mnuGSTReport.Text = "GST Report"
        '
        'mnuPurchaseReturnPost
        '
        Me.mnuPurchaseReturnPost.Index = 9
        Me.mnuPurchaseReturnPost.Text = "Purchase Return Post"
        '
        'MenuItem26
        '
        Me.MenuItem26.Index = 4
        Me.MenuItem26.Text = "-"
        '
        'MenuItem35
        '
        Me.MenuItem35.Index = 5
        Me.MenuItem35.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuReceiptsToCustomer, Me.mnuReceiptsToOther, Me.MenuItem38, Me.mnuARCreditNote, Me.mnuARDebitNote, Me.MenuItem40, Me.MenuItem39, Me.MenuItem45, Me.MenuItem46})
        Me.MenuItem35.Text = "Account Receivable"
        '
        'mnuReceiptsToCustomer
        '
        Me.mnuReceiptsToCustomer.Index = 0
        Me.mnuReceiptsToCustomer.Text = "Receipts (Customers)"
        '
        'mnuReceiptsToOther
        '
        Me.mnuReceiptsToOther.Index = 1
        Me.mnuReceiptsToOther.Text = "Receipts (Others)"
        '
        'MenuItem38
        '
        Me.MenuItem38.Index = 2
        Me.MenuItem38.Text = "-"
        '
        'mnuARCreditNote
        '
        Me.mnuARCreditNote.Index = 3
        Me.mnuARCreditNote.Text = "AR Credit Note"
        '
        'mnuARDebitNote
        '
        Me.mnuARDebitNote.Index = 4
        Me.mnuARDebitNote.Text = "AR Debit Note"
        '
        'MenuItem40
        '
        Me.MenuItem40.Index = 5
        Me.MenuItem40.Text = "-"
        '
        'MenuItem39
        '
        Me.MenuItem39.Index = 6
        Me.MenuItem39.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuCreditSalesPostGroup, Me.mnuCreditSalesReturnPostGroup, Me.MenuItem41, Me.mnuReceiptCustomerPostGroup, Me.mnuReceiptOthersPostGroup, Me.MenuItem42, Me.mnuCreditNotePostGroup, Me.mnuDebitNotePostGroup, Me.MenuItem43, Me.mnuPOSSettlementPost})
        Me.MenuItem39.Text = "Group Posting"
        '
        'mnuCreditSalesPostGroup
        '
        Me.mnuCreditSalesPostGroup.Index = 0
        Me.mnuCreditSalesPostGroup.Text = "Sales Post (Group)"
        '
        'mnuCreditSalesReturnPostGroup
        '
        Me.mnuCreditSalesReturnPostGroup.Index = 1
        Me.mnuCreditSalesReturnPostGroup.Text = "Sales Return Post (Group)"
        '
        'MenuItem41
        '
        Me.MenuItem41.Index = 2
        Me.MenuItem41.Text = "-"
        '
        'mnuReceiptCustomerPostGroup
        '
        Me.mnuReceiptCustomerPostGroup.Index = 3
        Me.mnuReceiptCustomerPostGroup.Text = "Receipt Customer Post (Group)"
        '
        'mnuReceiptOthersPostGroup
        '
        Me.mnuReceiptOthersPostGroup.Index = 4
        Me.mnuReceiptOthersPostGroup.Text = "Receipt Others Post (Group)"
        '
        'MenuItem42
        '
        Me.MenuItem42.Index = 5
        Me.MenuItem42.Text = "-"
        '
        'mnuCreditNotePostGroup
        '
        Me.mnuCreditNotePostGroup.Index = 6
        Me.mnuCreditNotePostGroup.Text = "Credit Note Post (Group)"
        '
        'mnuDebitNotePostGroup
        '
        Me.mnuDebitNotePostGroup.Index = 7
        Me.mnuDebitNotePostGroup.Text = "Debit Note Post (Group)"
        '
        'MenuItem43
        '
        Me.MenuItem43.Index = 8
        Me.MenuItem43.Text = "-"
        '
        'mnuPOSSettlementPost
        '
        Me.mnuPOSSettlementPost.Index = 9
        Me.mnuPOSSettlementPost.Text = "POS Settlement Post (Group)"
        '
        'MenuItem45
        '
        Me.MenuItem45.Index = 7
        Me.MenuItem45.Text = "-"
        '
        'MenuItem46
        '
        Me.MenuItem46.Index = 8
        Me.MenuItem46.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuSalesLedgerReport, Me.mnuReceiptReport, Me.MenuItem48, Me.mnuARCreditNoteReport, Me.mnuARDebitNoteReport, Me.MenuItem49, Me.mnuARAgingReport, Me.mnuARSatementReport, Me.MenuItem50, Me.mnuARBalanceReport, Me.mnuPOSSettlementReport, Me.mnuSalesReturnPost})
        Me.MenuItem46.Text = "Reports"
        '
        'mnuSalesLedgerReport
        '
        Me.mnuSalesLedgerReport.Index = 0
        Me.mnuSalesLedgerReport.Text = "Sales Ledger Report"
        '
        'mnuReceiptReport
        '
        Me.mnuReceiptReport.Index = 1
        Me.mnuReceiptReport.Text = "Receipt Report"
        '
        'MenuItem48
        '
        Me.MenuItem48.Index = 2
        Me.MenuItem48.Text = "-"
        '
        'mnuARCreditNoteReport
        '
        Me.mnuARCreditNoteReport.Index = 3
        Me.mnuARCreditNoteReport.Text = "Credit Note Report"
        '
        'mnuARDebitNoteReport
        '
        Me.mnuARDebitNoteReport.Index = 4
        Me.mnuARDebitNoteReport.Text = "Debit Note Report"
        '
        'MenuItem49
        '
        Me.MenuItem49.Index = 5
        Me.MenuItem49.Text = "-"
        '
        'mnuARAgingReport
        '
        Me.mnuARAgingReport.Index = 6
        Me.mnuARAgingReport.Text = "AR Aging Report"
        '
        'mnuARSatementReport
        '
        Me.mnuARSatementReport.Index = 7
        Me.mnuARSatementReport.Text = "AR Satement Report"
        '
        'MenuItem50
        '
        Me.MenuItem50.Index = 8
        Me.MenuItem50.Text = "-"
        '
        'mnuARBalanceReport
        '
        Me.mnuARBalanceReport.Index = 9
        Me.mnuARBalanceReport.Text = "Balance Report"
        '
        'mnuPOSSettlementReport
        '
        Me.mnuPOSSettlementReport.Index = 10
        Me.mnuPOSSettlementReport.Text = "POS Settlement Report"
        '
        'mnuSalesReturnPost
        '
        Me.mnuSalesReturnPost.Index = 11
        Me.mnuSalesReturnPost.Text = "Sales Return Post Report"
        '
        'MenuItem37
        '
        Me.MenuItem37.Index = 6
        Me.MenuItem37.Text = "-"
        '
        'mnuGeneralLedger
        '
        Me.mnuGeneralLedger.Index = 7
        Me.mnuGeneralLedger.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuJournalEntry, Me.mnuJournalTemplate, Me.mnuJournalListing, Me.mnuJournalPosting, Me.MenuItem28, Me.mnuFinacialReports})
        Me.mnuGeneralLedger.Text = "General Ledger"
        '
        'mnuJournalEntry
        '
        Me.mnuJournalEntry.Index = 0
        Me.mnuJournalEntry.Text = "Journal Entry"
        '
        'mnuJournalTemplate
        '
        Me.mnuJournalTemplate.Index = 1
        Me.mnuJournalTemplate.Text = "Journal Template"
        '
        'mnuJournalListing
        '
        Me.mnuJournalListing.Index = 2
        Me.mnuJournalListing.Text = "Journal Listing"
        '
        'mnuJournalPosting
        '
        Me.mnuJournalPosting.Index = 3
        Me.mnuJournalPosting.Text = "Journal Posting"
        '
        'MenuItem28
        '
        Me.MenuItem28.Index = 4
        Me.MenuItem28.Text = "-"
        '
        'mnuFinacialReports
        '
        Me.mnuFinacialReports.Index = 5
        Me.mnuFinacialReports.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuJournalReport, Me.mnuJournalList, Me.MenuItem29, Me.mnuLedgerReports, Me.mnuMultipleLedgerReports, Me.mnuGeneralLedgerReports, Me.MenuItem30, Me.mnuTrialBalance, Me.mnuProfitandLoss, Me.mnuBalanceSheet})
        Me.mnuFinacialReports.Text = "Financial Reports"
        '
        'mnuJournalReport
        '
        Me.mnuJournalReport.Index = 0
        Me.mnuJournalReport.Text = "Journal Report"
        '
        'mnuJournalList
        '
        Me.mnuJournalList.Index = 1
        Me.mnuJournalList.Text = "Journal List"
        '
        'MenuItem29
        '
        Me.MenuItem29.Index = 2
        Me.MenuItem29.Text = "-"
        '
        'mnuLedgerReports
        '
        Me.mnuLedgerReports.Index = 3
        Me.mnuLedgerReports.Text = "Ledger Reports"
        '
        'mnuMultipleLedgerReports
        '
        Me.mnuMultipleLedgerReports.Index = 4
        Me.mnuMultipleLedgerReports.Text = "Multiple Ledger Reports"
        '
        'mnuGeneralLedgerReports
        '
        Me.mnuGeneralLedgerReports.Index = 5
        Me.mnuGeneralLedgerReports.Text = "General Ledger Reports"
        '
        'MenuItem30
        '
        Me.MenuItem30.Index = 6
        Me.MenuItem30.Text = "-"
        '
        'mnuTrialBalance
        '
        Me.mnuTrialBalance.Index = 7
        Me.mnuTrialBalance.Text = "Trial Balance Report"
        '
        'mnuProfitandLoss
        '
        Me.mnuProfitandLoss.Index = 8
        Me.mnuProfitandLoss.Text = "Profit and Loss Report"
        '
        'mnuBalanceSheet
        '
        Me.mnuBalanceSheet.Index = 9
        Me.mnuBalanceSheet.Text = "Balance Sheet Report"
        '
        'mnuDailyProcedure
        '
        Me.mnuDailyProcedure.Index = 9
        Me.mnuDailyProcedure.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuSettlement, Me.mnuDayEnd})
        Me.mnuDailyProcedure.Text = "Daily Procedure"
        '
        'mnuSettlement
        '
        Me.mnuSettlement.Index = 0
        Me.mnuSettlement.Text = "Settlement"
        '
        'mnuDayEnd
        '
        Me.mnuDayEnd.Index = 1
        Me.mnuDayEnd.Text = "Day End"
        '
        'mnuSecurity
        '
        Me.mnuSecurity.Index = 10
        Me.mnuSecurity.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuUserGroup, Me.mnuUserMaster, Me.mnuUserPermission})
        Me.mnuSecurity.Text = "&Security"
        '
        'mnuUserGroup
        '
        Me.mnuUserGroup.Index = 0
        Me.mnuUserGroup.Text = "User Group"
        '
        'mnuUserMaster
        '
        Me.mnuUserMaster.Index = 1
        Me.mnuUserMaster.Text = "User Master"
        '
        'mnuUserPermission
        '
        Me.mnuUserPermission.Index = 2
        Me.mnuUserPermission.Text = "User Permission"
        '
        'mnuHeadUtility
        '
        Me.mnuHeadUtility.Index = 11
        Me.mnuHeadUtility.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem58, Me.mnuChangePrinter, Me.mnueTRSTicket, Me.mnueTRSTicketVoid, Me.MenuItem5, Me.mnuGoldSchemeEntries})
        Me.mnuHeadUtility.Text = "Utility"
        '
        'MenuItem58
        '
        Me.MenuItem58.Index = 0
        Me.MenuItem58.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem60, Me.MenuItem66, Me.mnuRFIDStocktake})
        Me.MenuItem58.Text = "Stock Take"
        '
        'MenuItem60
        '
        Me.MenuItem60.Index = 0
        Me.MenuItem60.Text = "Stock Take"
        '
        'MenuItem66
        '
        Me.MenuItem66.Index = 1
        Me.MenuItem66.Text = "StockTake Reports"
        '
        'mnuRFIDStocktake
        '
        Me.mnuRFIDStocktake.Index = 2
        Me.mnuRFIDStocktake.Text = "RFID Stock Take Reports"
        Me.mnuRFIDStocktake.Visible = False
        '
        'mnuChangePrinter
        '
        Me.mnuChangePrinter.Index = 1
        Me.mnuChangePrinter.Text = "Change Printer"
        '
        'mnueTRSTicket
        '
        Me.mnueTRSTicket.Index = 2
        Me.mnueTRSTicket.Text = "eTRS Ticket"
        '
        'mnueTRSTicketVoid
        '
        Me.mnueTRSTicketVoid.Index = 3
        Me.mnueTRSTicketVoid.Text = "eTRS Ticket Void"
        '
        'MenuItem5
        '
        Me.MenuItem5.Index = 4
        Me.MenuItem5.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuPurchaseTake, Me.mnuPurchaseTakeReports})
        Me.MenuItem5.Text = "Purchase Take "
        '
        'mnuPurchaseTake
        '
        Me.mnuPurchaseTake.Index = 0
        Me.mnuPurchaseTake.Text = "Purchase Take"
        '
        'mnuPurchaseTakeReports
        '
        Me.mnuPurchaseTakeReports.Index = 1
        Me.mnuPurchaseTakeReports.Text = "Purchase Take Reports"
        '
        'mnuGoldSchemeEntries
        '
        Me.mnuGoldSchemeEntries.Index = 5
        Me.mnuGoldSchemeEntries.Text = "Gold Scheme Transaction"
        '
        'MenuItem67
        '
        Me.MenuItem67.Index = 12
        Me.MenuItem67.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuAbout})
        Me.MenuItem67.Text = "Help"
        '
        'mnuAbout
        '
        Me.mnuAbout.Index = 0
        Me.mnuAbout.Text = "About"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "")
        Me.ImageList1.Images.SetKeyName(1, "")
        Me.ImageList1.Images.SetKeyName(2, "")
        Me.ImageList1.Images.SetKeyName(3, "")
        Me.ImageList1.Images.SetKeyName(4, "")
        Me.ImageList1.Images.SetKeyName(5, "")
        Me.ImageList1.Images.SetKeyName(6, "")
        Me.ImageList1.Images.SetKeyName(7, "")
        Me.ImageList1.Images.SetKeyName(8, "")
        Me.ImageList1.Images.SetKeyName(9, "")
        Me.ImageList1.Images.SetKeyName(10, "")
        Me.ImageList1.Images.SetKeyName(11, "")
        Me.ImageList1.Images.SetKeyName(12, "")
        Me.ImageList1.Images.SetKeyName(13, "")
        Me.ImageList1.Images.SetKeyName(14, "")
        Me.ImageList1.Images.SetKeyName(15, "")
        Me.ImageList1.Images.SetKeyName(16, "")
        Me.ImageList1.Images.SetKeyName(17, "")
        Me.ImageList1.Images.SetKeyName(18, "")
        Me.ImageList1.Images.SetKeyName(19, "")
        Me.ImageList1.Images.SetKeyName(20, "")
        Me.ImageList1.Images.SetKeyName(21, "")
        '
        'UGPSMenu
        '
        Me.UGPSMenu.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.sep10, Me.btnTodaySales, Me.sep8, Me.btnClass, Me.Sep1, Me.btnClassCostDisp, Me.sep11, Me.btnCategory, Me.sep2, Me.btnCatalogue, Me.Sep3, Me.btnJobSheet, Me.Sep4, Me.btnSearch, Me.sep5, Me.btnOrder, Me.sep6, Me.btnSales, Me.sep7, Me.btnBarcode, Me.sep9, Me.btnQuit})
        Me.UGPSMenu.ButtonSize = New System.Drawing.Size(75, 75)
        Me.UGPSMenu.DropDownArrows = True
        Me.UGPSMenu.ImageList = Me.ImageList2
        Me.UGPSMenu.Location = New System.Drawing.Point(0, 0)
        Me.UGPSMenu.Name = "UGPSMenu"
        Me.UGPSMenu.ShowToolTips = True
        Me.UGPSMenu.Size = New System.Drawing.Size(1733, 66)
        Me.UGPSMenu.TabIndex = 1
        Me.UGPSMenu.Visible = False
        '
        'sep10
        '
        Me.sep10.Name = "sep10"
        Me.sep10.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'btnTodaySales
        '
        Me.btnTodaySales.ImageIndex = 4
        Me.btnTodaySales.Name = "btnTodaySales"
        Me.btnTodaySales.Text = "Terminal Sales"
        '
        'sep8
        '
        Me.sep8.Name = "sep8"
        Me.sep8.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'btnClass
        '
        Me.btnClass.ImageIndex = 6
        Me.btnClass.Name = "btnClass"
        Me.btnClass.Text = "Class"
        '
        'Sep1
        '
        Me.Sep1.Name = "Sep1"
        Me.Sep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'btnClassCostDisp
        '
        Me.btnClassCostDisp.ImageIndex = 10
        Me.btnClassCostDisp.Name = "btnClassCostDisp"
        Me.btnClassCostDisp.Text = "Stock Level"
        '
        'sep11
        '
        Me.sep11.Name = "sep11"
        Me.sep11.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'btnCategory
        '
        Me.btnCategory.ImageIndex = 1
        Me.btnCategory.Name = "btnCategory"
        Me.btnCategory.Text = "Category"
        '
        'sep2
        '
        Me.sep2.Name = "sep2"
        Me.sep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'btnCatalogue
        '
        Me.btnCatalogue.ImageIndex = 2
        Me.btnCatalogue.Name = "btnCatalogue"
        Me.btnCatalogue.Text = "Catalogue"
        '
        'Sep3
        '
        Me.Sep3.Name = "Sep3"
        Me.Sep3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'btnJobSheet
        '
        Me.btnJobSheet.ImageIndex = 4
        Me.btnJobSheet.Name = "btnJobSheet"
        Me.btnJobSheet.Text = "JobSheet"
        '
        'Sep4
        '
        Me.Sep4.Name = "Sep4"
        Me.Sep4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'btnSearch
        '
        Me.btnSearch.ImageIndex = 3
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Text = "Search"
        '
        'sep5
        '
        Me.sep5.Name = "sep5"
        Me.sep5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'btnOrder
        '
        Me.btnOrder.ImageIndex = 0
        Me.btnOrder.Name = "btnOrder"
        Me.btnOrder.Text = "OrderEntry"
        '
        'sep6
        '
        Me.sep6.Name = "sep6"
        Me.sep6.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'btnSales
        '
        Me.btnSales.ImageIndex = 8
        Me.btnSales.Name = "btnSales"
        Me.btnSales.Text = "Sales"
        '
        'sep7
        '
        Me.sep7.Name = "sep7"
        Me.sep7.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'btnBarcode
        '
        Me.btnBarcode.ImageIndex = 7
        Me.btnBarcode.Name = "btnBarcode"
        Me.btnBarcode.Text = "BarCode"
        '
        'sep9
        '
        Me.sep9.Name = "sep9"
        Me.sep9.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'btnQuit
        '
        Me.btnQuit.ImageIndex = 5
        Me.btnQuit.Name = "btnQuit"
        Me.btnQuit.Text = "Quit"
        '
        'ImageList2
        '
        Me.ImageList2.ImageStream = CType(resources.GetObject("ImageList2.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList2.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList2.Images.SetKeyName(0, "")
        Me.ImageList2.Images.SetKeyName(1, "")
        Me.ImageList2.Images.SetKeyName(2, "")
        Me.ImageList2.Images.SetKeyName(3, "")
        Me.ImageList2.Images.SetKeyName(4, "")
        Me.ImageList2.Images.SetKeyName(5, "")
        Me.ImageList2.Images.SetKeyName(6, "")
        Me.ImageList2.Images.SetKeyName(7, "")
        Me.ImageList2.Images.SetKeyName(8, "")
        Me.ImageList2.Images.SetKeyName(9, "")
        Me.ImageList2.Images.SetKeyName(10, "")
        Me.ImageList2.Images.SetKeyName(11, "icon-100.ico")
        Me.ImageList2.Images.SetKeyName(12, "exit.jpg")
        '
        'conMenu
        '
        Me.conMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem44, Me.MenuItem47, Me.MenuItem55, Me.MenuItem57, Me.MenuItem59})
        '
        'MenuItem44
        '
        Me.MenuItem44.Enabled = False
        Me.MenuItem44.Index = 0
        Me.MenuItem44.Text = "Purchase Invoice Posting (Group)"
        '
        'MenuItem47
        '
        Me.MenuItem47.Enabled = False
        Me.MenuItem47.Index = 1
        Me.MenuItem47.Text = "Purchase Return Posting (Group)"
        '
        'MenuItem55
        '
        Me.MenuItem55.Enabled = False
        Me.MenuItem55.Index = 2
        Me.MenuItem55.Text = "Debit Note Posting (Group )"
        '
        'MenuItem57
        '
        Me.MenuItem57.Enabled = False
        Me.MenuItem57.Index = 3
        Me.MenuItem57.Text = "Payment  Posting ( Group )"
        '
        'MenuItem59
        '
        Me.MenuItem59.Enabled = False
        Me.MenuItem59.Index = 4
        Me.MenuItem59.Text = "Payment Others Posting ( Group )"
        '
        'ConMenuAr
        '
        Me.ConMenuAr.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem61, Me.MenuItem62, Me.MenuItem63, Me.MenuItem64, Me.MenuItem65})
        '
        'MenuItem61
        '
        Me.MenuItem61.Enabled = False
        Me.MenuItem61.Index = 0
        Me.MenuItem61.Text = "Credit Sales Post ( Group )"
        '
        'MenuItem62
        '
        Me.MenuItem62.Enabled = False
        Me.MenuItem62.Index = 1
        Me.MenuItem62.Text = "Credit Sales Return Post ( Group )"
        '
        'MenuItem63
        '
        Me.MenuItem63.Enabled = False
        Me.MenuItem63.Index = 2
        Me.MenuItem63.Text = "Receipt Customer Post ( Group )"
        '
        'MenuItem64
        '
        Me.MenuItem64.Enabled = False
        Me.MenuItem64.Index = 3
        Me.MenuItem64.Text = "Receipt Others Post ( Group )"
        '
        'MenuItem65
        '
        Me.MenuItem65.Enabled = False
        Me.MenuItem65.Index = 4
        Me.MenuItem65.Text = "Credit Note Post ( Group )"
        '
        'sbGPos
        '
        Me.sbGPos.Location = New System.Drawing.Point(0, 685)
        Me.sbGPos.Name = "sbGPos"
        Me.sbGPos.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanel2, Me.StatusBarPanel3})
        Me.sbGPos.ShowPanels = True
        Me.sbGPos.Size = New System.Drawing.Size(1733, 16)
        Me.sbGPos.TabIndex = 12
        '
        'StatusBarPanel1
        '
        Me.StatusBarPanel1.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.Raised
        Me.StatusBarPanel1.Name = "StatusBarPanel1"
        Me.StatusBarPanel1.Style = System.Windows.Forms.StatusBarPanelStyle.OwnerDraw
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        '
        'StatusBarPanel3
        '
        Me.StatusBarPanel3.Name = "StatusBarPanel3"
        '
        'ObjImageList
        '
        Me.ObjImageList.ImageStream = CType(resources.GetObject("ObjImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ObjImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ObjImageList.Images.SetKeyName(0, "")
        Me.ObjImageList.Images.SetKeyName(1, "")
        Me.ObjImageList.Images.SetKeyName(2, "")
        Me.ObjImageList.Images.SetKeyName(3, "")
        Me.ObjImageList.Images.SetKeyName(4, "")
        Me.ObjImageList.Images.SetKeyName(5, "")
        Me.ObjImageList.Images.SetKeyName(6, "")
        Me.ObjImageList.Images.SetKeyName(7, "")
        Me.ObjImageList.Images.SetKeyName(8, "")
        '
        'grpMainHQ
        '
        Me.grpMainHQ.Controls.Add(Me.btnMainHQPos)
        Me.grpMainHQ.Controls.Add(Me.btnMainExit)
        Me.grpMainHQ.Controls.Add(Me.btnMainBarCode)
        Me.grpMainHQ.Controls.Add(Me.btnMainTransferOut)
        Me.grpMainHQ.Controls.Add(Me.btnMainTransferIn)
        Me.grpMainHQ.Controls.Add(Me.btnMainCreditSales)
        Me.grpMainHQ.Controls.Add(Me.btnMainPurchase)
        Me.grpMainHQ.Controls.Add(Me.btnMainJobsheet)
        Me.grpMainHQ.Controls.Add(Me.btnMainSearch)
        Me.grpMainHQ.Controls.Add(Me.btnMainCategory)
        Me.grpMainHQ.Controls.Add(Me.btnMainCatalogue)
        Me.grpMainHQ.Controls.Add(Me.btnMainClass)
        Me.grpMainHQ.Controls.Add(Me.btnMainInventory)
        Me.grpMainHQ.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.grpMainHQ.Location = New System.Drawing.Point(1, 0)
        Me.grpMainHQ.Name = "grpMainHQ"
        Me.grpMainHQ.Size = New System.Drawing.Size(89, 782)
        Me.grpMainHQ.TabIndex = 13
        Me.grpMainHQ.TabStop = False
        Me.grpMainHQ.Visible = False
        '
        'btnMainHQPos
        '
        Me.btnMainHQPos.Image = CType(resources.GetObject("btnMainHQPos.Image"), System.Drawing.Image)
        Me.btnMainHQPos.Location = New System.Drawing.Point(6, 527)
        Me.btnMainHQPos.Name = "btnMainHQPos"
        Me.btnMainHQPos.Size = New System.Drawing.Size(75, 75)
        Me.btnMainHQPos.TabIndex = 132
        Me.btnMainHQPos.UseVisualStyleBackColor = True
        '
        'btnMainExit
        '
        Me.btnMainExit.Image = CType(resources.GetObject("btnMainExit.Image"), System.Drawing.Image)
        Me.btnMainExit.Location = New System.Drawing.Point(6, 676)
        Me.btnMainExit.Name = "btnMainExit"
        Me.btnMainExit.Size = New System.Drawing.Size(75, 75)
        Me.btnMainExit.TabIndex = 131
        Me.btnMainExit.UseVisualStyleBackColor = True
        '
        'btnMainBarCode
        '
        Me.btnMainBarCode.Image = CType(resources.GetObject("btnMainBarCode.Image"), System.Drawing.Image)
        Me.btnMainBarCode.Location = New System.Drawing.Point(6, 601)
        Me.btnMainBarCode.Name = "btnMainBarCode"
        Me.btnMainBarCode.Size = New System.Drawing.Size(75, 75)
        Me.btnMainBarCode.TabIndex = 130
        Me.btnMainBarCode.UseVisualStyleBackColor = True
        '
        'btnMainTransferOut
        '
        Me.btnMainTransferOut.Image = CType(resources.GetObject("btnMainTransferOut.Image"), System.Drawing.Image)
        Me.btnMainTransferOut.Location = New System.Drawing.Point(735, 7)
        Me.btnMainTransferOut.Name = "btnMainTransferOut"
        Me.btnMainTransferOut.Size = New System.Drawing.Size(75, 75)
        Me.btnMainTransferOut.TabIndex = 129
        Me.btnMainTransferOut.UseVisualStyleBackColor = True
        '
        'btnMainTransferIn
        '
        Me.btnMainTransferIn.Image = CType(resources.GetObject("btnMainTransferIn.Image"), System.Drawing.Image)
        Me.btnMainTransferIn.Location = New System.Drawing.Point(654, 7)
        Me.btnMainTransferIn.Name = "btnMainTransferIn"
        Me.btnMainTransferIn.Size = New System.Drawing.Size(75, 75)
        Me.btnMainTransferIn.TabIndex = 124
        Me.btnMainTransferIn.UseVisualStyleBackColor = True
        '
        'btnMainCreditSales
        '
        Me.btnMainCreditSales.Image = CType(resources.GetObject("btnMainCreditSales.Image"), System.Drawing.Image)
        Me.btnMainCreditSales.Location = New System.Drawing.Point(573, 7)
        Me.btnMainCreditSales.Name = "btnMainCreditSales"
        Me.btnMainCreditSales.Size = New System.Drawing.Size(75, 75)
        Me.btnMainCreditSales.TabIndex = 124
        Me.btnMainCreditSales.UseVisualStyleBackColor = True
        Me.btnMainCreditSales.Visible = False
        '
        'btnMainPurchase
        '
        Me.btnMainPurchase.Image = CType(resources.GetObject("btnMainPurchase.Image"), System.Drawing.Image)
        Me.btnMainPurchase.Location = New System.Drawing.Point(6, 453)
        Me.btnMainPurchase.Name = "btnMainPurchase"
        Me.btnMainPurchase.Size = New System.Drawing.Size(75, 75)
        Me.btnMainPurchase.TabIndex = 128
        Me.btnMainPurchase.UseVisualStyleBackColor = True
        '
        'btnMainJobsheet
        '
        Me.btnMainJobsheet.Image = CType(resources.GetObject("btnMainJobsheet.Image"), System.Drawing.Image)
        Me.btnMainJobsheet.Location = New System.Drawing.Point(6, 379)
        Me.btnMainJobsheet.Name = "btnMainJobsheet"
        Me.btnMainJobsheet.Size = New System.Drawing.Size(75, 75)
        Me.btnMainJobsheet.TabIndex = 127
        Me.btnMainJobsheet.UseVisualStyleBackColor = True
        '
        'btnMainSearch
        '
        Me.btnMainSearch.Image = CType(resources.GetObject("btnMainSearch.Image"), System.Drawing.Image)
        Me.btnMainSearch.Location = New System.Drawing.Point(6, 305)
        Me.btnMainSearch.Name = "btnMainSearch"
        Me.btnMainSearch.Size = New System.Drawing.Size(75, 75)
        Me.btnMainSearch.TabIndex = 124
        Me.btnMainSearch.UseVisualStyleBackColor = True
        '
        'btnMainCategory
        '
        Me.btnMainCategory.Image = CType(resources.GetObject("btnMainCategory.Image"), System.Drawing.Image)
        Me.btnMainCategory.Location = New System.Drawing.Point(6, 83)
        Me.btnMainCategory.Name = "btnMainCategory"
        Me.btnMainCategory.Size = New System.Drawing.Size(75, 75)
        Me.btnMainCategory.TabIndex = 126
        Me.btnMainCategory.UseVisualStyleBackColor = True
        '
        'btnMainCatalogue
        '
        Me.btnMainCatalogue.Image = CType(resources.GetObject("btnMainCatalogue.Image"), System.Drawing.Image)
        Me.btnMainCatalogue.Location = New System.Drawing.Point(6, 157)
        Me.btnMainCatalogue.Name = "btnMainCatalogue"
        Me.btnMainCatalogue.Size = New System.Drawing.Size(75, 75)
        Me.btnMainCatalogue.TabIndex = 125
        Me.btnMainCatalogue.UseVisualStyleBackColor = True
        '
        'btnMainClass
        '
        Me.btnMainClass.Image = CType(resources.GetObject("btnMainClass.Image"), System.Drawing.Image)
        Me.btnMainClass.Location = New System.Drawing.Point(6, 7)
        Me.btnMainClass.Name = "btnMainClass"
        Me.btnMainClass.Size = New System.Drawing.Size(75, 75)
        Me.btnMainClass.TabIndex = 124
        Me.btnMainClass.UseVisualStyleBackColor = True
        '
        'btnMainInventory
        '
        Me.btnMainInventory.Image = CType(resources.GetObject("btnMainInventory.Image"), System.Drawing.Image)
        Me.btnMainInventory.Location = New System.Drawing.Point(6, 231)
        Me.btnMainInventory.Name = "btnMainInventory"
        Me.btnMainInventory.Size = New System.Drawing.Size(75, 75)
        Me.btnMainInventory.TabIndex = 123
        Me.btnMainInventory.UseVisualStyleBackColor = True
        '
        'grpMainOutlet
        '
        Me.grpMainOutlet.Controls.Add(Me.btnMainOutletBarCode)
        Me.grpMainOutlet.Controls.Add(Me.btnMainOutletExit)
        Me.grpMainOutlet.Controls.Add(Me.btnMainOutletDayEnd)
        Me.grpMainOutlet.Controls.Add(Me.btnMainOutletUpdateHQ)
        Me.grpMainOutlet.Controls.Add(Me.btnMainOutletSettlement)
        Me.grpMainOutlet.Controls.Add(Me.btnMainOutletTransferOut)
        Me.grpMainOutlet.Controls.Add(Me.btnMainOutletTransferIn)
        Me.grpMainOutlet.Controls.Add(Me.btnMainOutletOrderEntry)
        Me.grpMainOutlet.Controls.Add(Me.btnMainOutletInstallment)
        Me.grpMainOutlet.Controls.Add(Me.btnMainOutletInventory)
        Me.grpMainOutlet.Controls.Add(Me.btnMainOutletSearch)
        Me.grpMainOutlet.Controls.Add(Me.btnMainOutletPos)
        Me.grpMainOutlet.Controls.Add(Me.btnMainOutletPaymentEntry)
        Me.grpMainOutlet.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.grpMainOutlet.Location = New System.Drawing.Point(0, 0)
        Me.grpMainOutlet.Name = "grpMainOutlet"
        Me.grpMainOutlet.Size = New System.Drawing.Size(1014, 88)
        Me.grpMainOutlet.TabIndex = 14
        Me.grpMainOutlet.TabStop = False
        Me.grpMainOutlet.Visible = False
        '
        'btnMainOutletBarCode
        '
        Me.btnMainOutletBarCode.Image = CType(resources.GetObject("btnMainOutletBarCode.Image"), System.Drawing.Image)
        Me.btnMainOutletBarCode.Location = New System.Drawing.Point(655, 7)
        Me.btnMainOutletBarCode.Name = "btnMainOutletBarCode"
        Me.btnMainOutletBarCode.Size = New System.Drawing.Size(75, 75)
        Me.btnMainOutletBarCode.TabIndex = 132
        Me.btnMainOutletBarCode.UseVisualStyleBackColor = True
        '
        'btnMainOutletExit
        '
        Me.btnMainOutletExit.Image = CType(resources.GetObject("btnMainOutletExit.Image"), System.Drawing.Image)
        Me.btnMainOutletExit.Location = New System.Drawing.Point(897, 7)
        Me.btnMainOutletExit.Name = "btnMainOutletExit"
        Me.btnMainOutletExit.Size = New System.Drawing.Size(75, 75)
        Me.btnMainOutletExit.TabIndex = 131
        Me.btnMainOutletExit.UseVisualStyleBackColor = True
        '
        'btnMainOutletDayEnd
        '
        Me.btnMainOutletDayEnd.Image = CType(resources.GetObject("btnMainOutletDayEnd.Image"), System.Drawing.Image)
        Me.btnMainOutletDayEnd.Location = New System.Drawing.Point(816, 7)
        Me.btnMainOutletDayEnd.Name = "btnMainOutletDayEnd"
        Me.btnMainOutletDayEnd.Size = New System.Drawing.Size(75, 75)
        Me.btnMainOutletDayEnd.TabIndex = 130
        Me.btnMainOutletDayEnd.UseVisualStyleBackColor = True
        '
        'btnMainOutletUpdateHQ
        '
        Me.btnMainOutletUpdateHQ.Image = CType(resources.GetObject("btnMainOutletUpdateHQ.Image"), System.Drawing.Image)
        Me.btnMainOutletUpdateHQ.Location = New System.Drawing.Point(735, 7)
        Me.btnMainOutletUpdateHQ.Name = "btnMainOutletUpdateHQ"
        Me.btnMainOutletUpdateHQ.Size = New System.Drawing.Size(75, 75)
        Me.btnMainOutletUpdateHQ.TabIndex = 129
        Me.btnMainOutletUpdateHQ.UseVisualStyleBackColor = True
        '
        'btnMainOutletSettlement
        '
        Me.btnMainOutletSettlement.Image = CType(resources.GetObject("btnMainOutletSettlement.Image"), System.Drawing.Image)
        Me.btnMainOutletSettlement.Location = New System.Drawing.Point(654, 7)
        Me.btnMainOutletSettlement.Name = "btnMainOutletSettlement"
        Me.btnMainOutletSettlement.Size = New System.Drawing.Size(75, 75)
        Me.btnMainOutletSettlement.TabIndex = 124
        Me.btnMainOutletSettlement.UseVisualStyleBackColor = True
        Me.btnMainOutletSettlement.Visible = False
        '
        'btnMainOutletTransferOut
        '
        Me.btnMainOutletTransferOut.Image = CType(resources.GetObject("btnMainOutletTransferOut.Image"), System.Drawing.Image)
        Me.btnMainOutletTransferOut.Location = New System.Drawing.Point(573, 7)
        Me.btnMainOutletTransferOut.Name = "btnMainOutletTransferOut"
        Me.btnMainOutletTransferOut.Size = New System.Drawing.Size(75, 75)
        Me.btnMainOutletTransferOut.TabIndex = 124
        Me.btnMainOutletTransferOut.UseVisualStyleBackColor = True
        '
        'btnMainOutletTransferIn
        '
        Me.btnMainOutletTransferIn.Image = CType(resources.GetObject("btnMainOutletTransferIn.Image"), System.Drawing.Image)
        Me.btnMainOutletTransferIn.Location = New System.Drawing.Point(492, 7)
        Me.btnMainOutletTransferIn.Name = "btnMainOutletTransferIn"
        Me.btnMainOutletTransferIn.Size = New System.Drawing.Size(75, 75)
        Me.btnMainOutletTransferIn.TabIndex = 128
        Me.btnMainOutletTransferIn.UseVisualStyleBackColor = True
        '
        'btnMainOutletOrderEntry
        '
        Me.btnMainOutletOrderEntry.Image = CType(resources.GetObject("btnMainOutletOrderEntry.Image"), System.Drawing.Image)
        Me.btnMainOutletOrderEntry.Location = New System.Drawing.Point(411, 7)
        Me.btnMainOutletOrderEntry.Name = "btnMainOutletOrderEntry"
        Me.btnMainOutletOrderEntry.Size = New System.Drawing.Size(75, 75)
        Me.btnMainOutletOrderEntry.TabIndex = 127
        Me.btnMainOutletOrderEntry.UseVisualStyleBackColor = True
        '
        'btnMainOutletInstallment
        '
        Me.btnMainOutletInstallment.Image = CType(resources.GetObject("btnMainOutletInstallment.Image"), System.Drawing.Image)
        Me.btnMainOutletInstallment.Location = New System.Drawing.Point(330, 7)
        Me.btnMainOutletInstallment.Name = "btnMainOutletInstallment"
        Me.btnMainOutletInstallment.Size = New System.Drawing.Size(75, 75)
        Me.btnMainOutletInstallment.TabIndex = 124
        Me.btnMainOutletInstallment.UseVisualStyleBackColor = True
        '
        'btnMainOutletInventory
        '
        Me.btnMainOutletInventory.Image = CType(resources.GetObject("btnMainOutletInventory.Image"), System.Drawing.Image)
        Me.btnMainOutletInventory.Location = New System.Drawing.Point(87, 7)
        Me.btnMainOutletInventory.Name = "btnMainOutletInventory"
        Me.btnMainOutletInventory.Size = New System.Drawing.Size(75, 75)
        Me.btnMainOutletInventory.TabIndex = 126
        Me.btnMainOutletInventory.UseVisualStyleBackColor = True
        '
        'btnMainOutletSearch
        '
        Me.btnMainOutletSearch.Image = CType(resources.GetObject("btnMainOutletSearch.Image"), System.Drawing.Image)
        Me.btnMainOutletSearch.Location = New System.Drawing.Point(168, 7)
        Me.btnMainOutletSearch.Name = "btnMainOutletSearch"
        Me.btnMainOutletSearch.Size = New System.Drawing.Size(75, 75)
        Me.btnMainOutletSearch.TabIndex = 125
        Me.btnMainOutletSearch.UseVisualStyleBackColor = True
        '
        'btnMainOutletPos
        '
        Me.btnMainOutletPos.Image = CType(resources.GetObject("btnMainOutletPos.Image"), System.Drawing.Image)
        Me.btnMainOutletPos.Location = New System.Drawing.Point(6, 7)
        Me.btnMainOutletPos.Name = "btnMainOutletPos"
        Me.btnMainOutletPos.Size = New System.Drawing.Size(75, 75)
        Me.btnMainOutletPos.TabIndex = 124
        Me.btnMainOutletPos.UseVisualStyleBackColor = True
        '
        'btnMainOutletPaymentEntry
        '
        Me.btnMainOutletPaymentEntry.Image = CType(resources.GetObject("btnMainOutletPaymentEntry.Image"), System.Drawing.Image)
        Me.btnMainOutletPaymentEntry.Location = New System.Drawing.Point(249, 7)
        Me.btnMainOutletPaymentEntry.Name = "btnMainOutletPaymentEntry"
        Me.btnMainOutletPaymentEntry.Size = New System.Drawing.Size(75, 75)
        Me.btnMainOutletPaymentEntry.TabIndex = 123
        Me.btnMainOutletPaymentEntry.UseVisualStyleBackColor = True
        '
        'dgvOrder
        '
        Me.dgvOrder.AllowUserToAddRows = False
        Me.dgvOrder.AllowUserToDeleteRows = False
        Me.dgvOrder.AllowUserToOrderColumns = True
        Me.dgvOrder.AllowUserToResizeColumns = False
        Me.dgvOrder.AllowUserToResizeRows = False
        Me.dgvOrder.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvOrder.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvOrder.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colOrderNo, Me.coLocationCode, Me.colOrderDate, Me.DeliveryDate, Me.colCustomerCode, Me.colMobile, Me.cValue, Me.cSupplier})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvOrder.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvOrder.Location = New System.Drawing.Point(1008, 88)
        Me.dgvOrder.Name = "dgvOrder"
        Me.dgvOrder.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvOrder.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvOrder.RowHeadersVisible = False
        Me.dgvOrder.Size = New System.Drawing.Size(707, 164)
        Me.dgvOrder.TabIndex = 15
        Me.dgvOrder.Visible = False
        '
        'colOrderNo
        '
        Me.colOrderNo.HeaderText = "Order No"
        Me.colOrderNo.Name = "colOrderNo"
        Me.colOrderNo.ReadOnly = True
        Me.colOrderNo.Width = 110
        '
        'coLocationCode
        '
        Me.coLocationCode.HeaderText = "Location Code"
        Me.coLocationCode.Name = "coLocationCode"
        Me.coLocationCode.ReadOnly = True
        Me.coLocationCode.Width = 70
        '
        'colOrderDate
        '
        Me.colOrderDate.HeaderText = "Order Date"
        Me.colOrderDate.Name = "colOrderDate"
        Me.colOrderDate.ReadOnly = True
        Me.colOrderDate.Width = 110
        '
        'DeliveryDate
        '
        Me.DeliveryDate.HeaderText = "Due Date"
        Me.DeliveryDate.Name = "DeliveryDate"
        Me.DeliveryDate.ReadOnly = True
        Me.DeliveryDate.Width = 110
        '
        'colCustomerCode
        '
        Me.colCustomerCode.HeaderText = "Customer Code"
        Me.colCustomerCode.Name = "colCustomerCode"
        Me.colCustomerCode.ReadOnly = True
        Me.colCustomerCode.Width = 110
        '
        'colMobile
        '
        Me.colMobile.HeaderText = "Mobile"
        Me.colMobile.Name = "colMobile"
        Me.colMobile.ReadOnly = True
        Me.colMobile.Width = 90
        '
        'cValue
        '
        Me.cValue.HeaderText = "Value"
        Me.cValue.Name = "cValue"
        Me.cValue.ReadOnly = True
        Me.cValue.Visible = False
        '
        'cSupplier
        '
        Me.cSupplier.HeaderText = "Supplier"
        Me.cSupplier.Name = "cSupplier"
        Me.cSupplier.ReadOnly = True
        '
        'dgvInstallment
        '
        Me.dgvInstallment.AllowUserToAddRows = False
        Me.dgvInstallment.AllowUserToDeleteRows = False
        Me.dgvInstallment.AllowUserToOrderColumns = True
        Me.dgvInstallment.AllowUserToResizeColumns = False
        Me.dgvInstallment.AllowUserToResizeRows = False
        Me.dgvInstallment.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvInstallment.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvInstallment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvInstallment.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.cLocation, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5, Me.Balance, Me.Value})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvInstallment.DefaultCellStyle = DataGridViewCellStyle5
        Me.dgvInstallment.Location = New System.Drawing.Point(654, 468)
        Me.dgvInstallment.Name = "dgvInstallment"
        Me.dgvInstallment.ReadOnly = True
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvInstallment.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgvInstallment.RowHeadersVisible = False
        Me.dgvInstallment.Size = New System.Drawing.Size(628, 164)
        Me.dgvInstallment.TabIndex = 16
        Me.dgvInstallment.Visible = False
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Ins. Agreement No"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 130
        '
        'cLocation
        '
        Me.cLocation.HeaderText = "Location Code"
        Me.cLocation.Name = "cLocation"
        Me.cLocation.ReadOnly = True
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Ins. Date"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 90
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Due Date"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 110
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "Customer Code"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Visible = False
        Me.DataGridViewTextBoxColumn4.Width = 110
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "Mobile"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Width = 90
        '
        'Balance
        '
        Me.Balance.HeaderText = "Balance"
        Me.Balance.Name = "Balance"
        Me.Balance.ReadOnly = True
        '
        'Value
        '
        Me.Value.HeaderText = "cValue"
        Me.Value.Name = "Value"
        Me.Value.ReadOnly = True
        Me.Value.Visible = False
        '
        'dgvunfixJobsheet
        '
        Me.dgvunfixJobsheet.AllowUserToAddRows = False
        Me.dgvunfixJobsheet.AllowUserToDeleteRows = False
        Me.dgvunfixJobsheet.AllowUserToOrderColumns = True
        Me.dgvunfixJobsheet.AllowUserToResizeColumns = False
        Me.dgvunfixJobsheet.AllowUserToResizeRows = False
        Me.dgvunfixJobsheet.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvunfixJobsheet.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.dgvunfixJobsheet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvunfixJobsheet.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn6, Me.DataGridViewTextBoxColumn7, Me.DataGridViewTextBoxColumn8, Me.DataGridViewTextBoxColumn9})
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvunfixJobsheet.DefaultCellStyle = DataGridViewCellStyle8
        Me.dgvunfixJobsheet.Location = New System.Drawing.Point(99, 258)
        Me.dgvunfixJobsheet.Name = "dgvunfixJobsheet"
        Me.dgvunfixJobsheet.ReadOnly = True
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvunfixJobsheet.RowHeadersDefaultCellStyle = DataGridViewCellStyle9
        Me.dgvunfixJobsheet.RowHeadersVisible = False
        Me.dgvunfixJobsheet.Size = New System.Drawing.Size(453, 164)
        Me.dgvunfixJobsheet.TabIndex = 17
        Me.dgvunfixJobsheet.Visible = False
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.HeaderText = "JobSheet No"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Width = 110
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.HeaderText = "JobSheet Date"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Width = 110
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.HeaderText = "Supplier Code"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.Width = 110
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.HeaderText = "Invoice No"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.Width = 110
        '
        'grpReOrder
        '
        Me.grpReOrder.Controls.Add(Me.dgvReOrder)
        Me.grpReOrder.Location = New System.Drawing.Point(549, 323)
        Me.grpReOrder.Name = "grpReOrder"
        Me.grpReOrder.Size = New System.Drawing.Size(537, 164)
        Me.grpReOrder.TabIndex = 18
        Me.grpReOrder.TabStop = False
        Me.grpReOrder.Visible = False
        '
        'dgvReOrder
        '
        Me.dgvReOrder.AllowUserToAddRows = False
        Me.dgvReOrder.AllowUserToDeleteRows = False
        Me.dgvReOrder.AllowUserToOrderColumns = True
        Me.dgvReOrder.AllowUserToResizeColumns = False
        Me.dgvReOrder.AllowUserToResizeRows = False
        Me.dgvReOrder.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvReOrder.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle10
        Me.dgvReOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvReOrder.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cLocationCode, Me.DataGridViewTextBoxColumn10, Me.DataGridViewTextBoxColumn11, Me.DataGridViewTextBoxColumn12, Me.DataGridViewTextBoxColumn13})
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvReOrder.DefaultCellStyle = DataGridViewCellStyle11
        Me.dgvReOrder.Location = New System.Drawing.Point(8, 11)
        Me.dgvReOrder.Name = "dgvReOrder"
        Me.dgvReOrder.ReadOnly = True
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvReOrder.RowHeadersDefaultCellStyle = DataGridViewCellStyle12
        Me.dgvReOrder.RowHeadersVisible = False
        Me.dgvReOrder.Size = New System.Drawing.Size(529, 168)
        Me.dgvReOrder.TabIndex = 18
        '
        'cLocationCode
        '
        Me.cLocationCode.HeaderText = "Location Code"
        Me.cLocationCode.Name = "cLocationCode"
        Me.cLocationCode.ReadOnly = True
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.HeaderText = "Catalogue Code"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        Me.DataGridViewTextBoxColumn10.Width = 160
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.HeaderText = "Category Code"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.HeaderText = "ReOrder Level"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = True
        Me.DataGridViewTextBoxColumn12.Width = 80
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.HeaderText = "QtyonHand"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = True
        Me.DataGridViewTextBoxColumn13.Width = 80
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 10000
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMaster2})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1014, 24)
        Me.MenuStrip1.TabIndex = 19
        Me.MenuStrip1.Text = "MenuStrip1"
        Me.MenuStrip1.Visible = False
        '
        'mnuMaster2
        '
        Me.mnuMaster2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ChangePasswordToolStripMenuItem, Me.ToolStripMenuItem1, Me.ExitToolStripMenuItem})
        Me.mnuMaster2.Name = "mnuMaster2"
        Me.mnuMaster2.Size = New System.Drawing.Size(35, 20)
        Me.mnuMaster2.Text = "File"
        '
        'ChangePasswordToolStripMenuItem
        '
        Me.ChangePasswordToolStripMenuItem.Name = "ChangePasswordToolStripMenuItem"
        Me.ChangePasswordToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.ChangePasswordToolStripMenuItem.Text = "Change Password"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(168, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.btnNew)
        Me.GroupBox1.Controls.Add(Me.txtGST)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtTotalValue)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtCostPerGram)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtInventoryCode)
        Me.GroupBox1.Controls.Add(Me.txtSMC)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtWeight)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.btnInventoryCodeSearch)
        Me.GroupBox1.Controls.Add(Me.txtDescription)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(1424, 7)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(297, 480)
        Me.GroupBox1.TabIndex = 20
        Me.GroupBox1.TabStop = False
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(58, 28)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(173, 25)
        Me.Label8.TabIndex = 476223
        Me.Label8.Text = "Price Checker"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnNew.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.ForeColor = System.Drawing.Color.Black
        Me.btnNew.Location = New System.Drawing.Point(89, 420)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(100, 40)
        Me.btnNew.TabIndex = 476222
        Me.btnNew.Text = "OK"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'txtGST
        '
        Me.txtGST.AcceptsTab = True
        Me.txtGST.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtGST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGST.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtGST.Enabled = False
        Me.txtGST.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGST.ForeColor = System.Drawing.Color.Black
        Me.txtGST.Location = New System.Drawing.Point(15, 320)
        Me.txtGST.MaxLength = 4
        Me.txtGST.Name = "txtGST"
        Me.txtGST.Size = New System.Drawing.Size(261, 20)
        Me.txtGST.TabIndex = 476220
        Me.txtGST.Tag = "NE"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(9, 299)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(89, 18)
        Me.Label7.TabIndex = 476221
        Me.Label7.Text = "GST"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTotalValue
        '
        Me.txtTotalValue.AcceptsTab = True
        Me.txtTotalValue.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtTotalValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalValue.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTotalValue.Enabled = False
        Me.txtTotalValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalValue.ForeColor = System.Drawing.Color.Black
        Me.txtTotalValue.Location = New System.Drawing.Point(13, 364)
        Me.txtTotalValue.MaxLength = 0
        Me.txtTotalValue.Multiline = True
        Me.txtTotalValue.Name = "txtTotalValue"
        Me.txtTotalValue.Size = New System.Drawing.Size(261, 49)
        Me.txtTotalValue.TabIndex = 476218
        Me.txtTotalValue.Tag = "NE"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(9, 343)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(94, 18)
        Me.Label5.TabIndex = 476219
        Me.Label5.Text = "Total Value"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCostPerGram
        '
        Me.txtCostPerGram.AcceptsTab = True
        Me.txtCostPerGram.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtCostPerGram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCostPerGram.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCostPerGram.Enabled = False
        Me.txtCostPerGram.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostPerGram.ForeColor = System.Drawing.Color.Black
        Me.txtCostPerGram.Location = New System.Drawing.Point(12, 234)
        Me.txtCostPerGram.MaxLength = 4
        Me.txtCostPerGram.Name = "txtCostPerGram"
        Me.txtCostPerGram.Size = New System.Drawing.Size(261, 20)
        Me.txtCostPerGram.TabIndex = 476216
        Me.txtCostPerGram.Tag = "NE"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(8, 213)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 18)
        Me.Label1.TabIndex = 476217
        Me.Label1.Text = "CostPerGram"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtInventoryCode
        '
        Me.txtInventoryCode.AcceptsTab = True
        Me.txtInventoryCode.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtInventoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInventoryCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtInventoryCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInventoryCode.ForeColor = System.Drawing.Color.Black
        Me.txtInventoryCode.Location = New System.Drawing.Point(12, 102)
        Me.txtInventoryCode.MaxLength = 10
        Me.txtInventoryCode.Name = "txtInventoryCode"
        Me.txtInventoryCode.Size = New System.Drawing.Size(264, 20)
        Me.txtInventoryCode.TabIndex = 476214
        Me.txtInventoryCode.Tag = "NE"
        '
        'txtSMC
        '
        Me.txtSMC.AcceptsTab = True
        Me.txtSMC.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtSMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSMC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSMC.Enabled = False
        Me.txtSMC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMC.ForeColor = System.Drawing.Color.Black
        Me.txtSMC.Location = New System.Drawing.Point(12, 276)
        Me.txtSMC.MaxLength = 4
        Me.txtSMC.Name = "txtSMC"
        Me.txtSMC.Size = New System.Drawing.Size(261, 20)
        Me.txtSMC.TabIndex = 476209
        Me.txtSMC.Tag = "NE"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(6, 255)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(89, 18)
        Me.Label6.TabIndex = 476213
        Me.Label6.Text = "SMC"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtWeight
        '
        Me.txtWeight.AcceptsTab = True
        Me.txtWeight.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWeight.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtWeight.Enabled = False
        Me.txtWeight.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeight.ForeColor = System.Drawing.Color.Black
        Me.txtWeight.Location = New System.Drawing.Point(12, 190)
        Me.txtWeight.MaxLength = 4
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.Size = New System.Drawing.Size(262, 20)
        Me.txtWeight.TabIndex = 476208
        Me.txtWeight.Tag = "NE"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(6, 169)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(89, 18)
        Me.Label4.TabIndex = 476212
        Me.Label4.Text = "Weight"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnInventoryCodeSearch
        '
        Me.btnInventoryCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnInventoryCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInventoryCodeSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInventoryCodeSearch.Location = New System.Drawing.Point(312, 183)
        Me.btnInventoryCodeSearch.Name = "btnInventoryCodeSearch"
        Me.btnInventoryCodeSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnInventoryCodeSearch.TabIndex = 476215
        Me.btnInventoryCodeSearch.Text = "..."
        Me.btnInventoryCodeSearch.UseVisualStyleBackColor = False
        '
        'txtDescription
        '
        Me.txtDescription.AcceptsTab = True
        Me.txtDescription.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescription.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDescription.Enabled = False
        Me.txtDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.ForeColor = System.Drawing.Color.Black
        Me.txtDescription.Location = New System.Drawing.Point(12, 146)
        Me.txtDescription.MaxLength = 4
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(264, 20)
        Me.txtDescription.TabIndex = 476207
        Me.txtDescription.Tag = "NE"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(6, 125)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 18)
        Me.Label3.TabIndex = 476211
        Me.Label3.Text = "Description"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(6, 81)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(124, 18)
        Me.Label2.TabIndex = 476210
        Me.Label2.Text = "Inventory Code"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmMain
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.AutoSize = True
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(1733, 701)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grpReOrder)
        Me.Controls.Add(Me.dgvunfixJobsheet)
        Me.Controls.Add(Me.dgvInstallment)
        Me.Controls.Add(Me.dgvOrder)
        Me.Controls.Add(Me.grpMainOutlet)
        Me.Controls.Add(Me.grpMainHQ)
        Me.Controls.Add(Me.sbGPos)
        Me.Controls.Add(Me.UGPSMenu)
        Me.Controls.Add(Me.MenuStrip1)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Menu = Me.MainMenu2
        Me.Name = "frmMain"
        Me.Text = "Unipro Smart Gold"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpMainHQ.ResumeLayout(False)
        Me.grpMainOutlet.ResumeLayout(False)
        CType(Me.dgvOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvInstallment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvunfixJobsheet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpReOrder.ResumeLayout(False)
        CType(Me.dgvReOrder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub mnuExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExit.Click
        If MsgBox("Do you want to quit?", MsgBoxStyle.Critical + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then End
    End Sub

    Private Sub UFAMENU_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles UGPSMenu.ButtonClick
        If e.Button.Text = "Class" Then
            If ((sUserGroup = "SALES") Or (sUserGroup = "PURCH")) Then
                MsgBox(" You don't have rights to access !!!", MsgBoxStyle.Information)
                Exit Sub
            Else
                Dim frm As New frmClass
                frm.Show()
                frm = Nothing
            End If
        End If
        If e.Button.Text = "Stock Level" Then
            Dim frm As New frmClassDisplay
            frm.Show()
            frm = Nothing
        End If
        If e.Button.Text = "Category" Then
            If ((sUserGroup = "SALES") Or (sUserGroup = "PURCH")) Then
                MsgBox(" You don't have rights to access !!!", MsgBoxStyle.Information)
                Exit Sub
            Else
                If (sCompanyShortCode = "ML") Then
                    Dim frm As New frmCategoryML
                    frm.Show()
                    frm = Nothing
                Else
                    Dim frm As New frmCategory
                    frm.Show()
                    frm = Nothing
                End If
            End If
        End If

        If e.Button.Text = "Catalogue" Then
            If ((sUserGroup = "SALES") Or (sUserGroup = "PURCH")) Then
                MsgBox(" You don't have rights to access !!!", MsgBoxStyle.Information)
                Exit Sub
            Else
                Dim frm As New frmCatalouge
                frm.Show()
                frm = Nothing
            End If
        End If
        If e.Button.Text = "OrderEntry" Then
            e.Button.Text = "OrderEntry"
            If (sUserGroup = "PURCH") Then
                MsgBox(" You don't have rights to access !!!", MsgBoxStyle.Information)
                Exit Sub
            Else
                Dim frm As New frmInventoryOrderEntry
                frm.Show()
                frm = Nothing
            End If
        End If
        If e.Button.Text = "Search" Then
            Dim frm As New frmInventory
            frm.Show()
            frm = Nothing
        End If
        If e.Button.Text = "JobSheet" Then
            If (sUserGroup = "SALES") Then
                MsgBox(" You don't have rights to access !!!", MsgBoxStyle.Information)
                Exit Sub
            Else
                Dim frm As New frmJobSheet
                frm.Show()
                frm = Nothing
            End If
        End If
        If e.Button.Text = "Sales" Then
            If (sUserGroup = "PURCH") Then
                MsgBox(" You don't have rights to access !!!", MsgBoxStyle.Information)
                Exit Sub
            Else
                If Trim(sCompanyShortCode) = "HN" Then
                    Dim frm As New frmGPOSNew
                    frm.Show()
                    frm = Nothing
                Else
                    Dim frm As New frmGPOS
                    frm.Show()
                    frm = Nothing
                End If
            End If
        End If
        If e.Button.Text = "BarCode" Then
            If (sUserGroup = "SALES") Then
                MsgBox(" You don't have rights to access !!!", MsgBoxStyle.Information)
                Exit Sub
            Else
                'If (sCompanyShortCode = "ES") Or (sCompanyShortCode = "ML") Or (sCompanyShortCode = "HN") Or (sCompanyShortCode = "MJ") Or (sCompanyShortCode = "SSJ") Or (sCompanyShortCode = "RJ") Then
                '    Dim frm As New frmBarcode
                '    frm.show()
                '    frm = Nothing
                'ElseIf sCompanyShortCode = "AS" Then
                '    Dim frm As New frmJewelBarcode
                '    frm.show()
                '    frm = Nothing
                'End If
                'Changed by Shabeer 20111019
                If sCompanyShortCode = "AS" Then
                    Dim objfrm As New frmJewelBarcode
                    objfrm.Show()
                    objfrm = Nothing
                Else
                    Dim objfrm As New frmBarcode
                    objfrm.Show()
                    objfrm = Nothing
                End If
            End If
        End If
        If e.Button.Text = "Terminal Sales" Then
            If (sUserGroup = "PURCH") Then
                MsgBox(" You don't have rights to access !!!", MsgBoxStyle.Information)
                Exit Sub
            Else
                Dim frm As New frmTodaySales
                frm.Show()
                frm = Nothing
            End If
        End If

        If e.Button.Text = "Quit" Then
            mnuExit_Click(sender, e)
        End If
    End Sub

    Private Sub frmMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            'Case Keys.Up
            '    SendKeys.Send("+{Tab}")
            'Case Keys.Down
            '    SendKeys.Send("{Tab}")
            Case Keys.Enter
                SendKeys.Send("{Tab}")
        End Select
    End Sub

    Private Sub frmMain_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        StartTime = Now()
    End Sub
    'Public Sub subSetMenu(ByVal sGroupCode As String)
    '    Dim dsMenu As New DataSet
    '    Dim objUser As New clsUser
    '    Dim mMenu As String
    '    Try
    '        SetAllMenuItems(MenuStrip, True)
    '        dsMenu = objUser.fncGetUserMenuByGroupCode(sGroupCode)
    '        For i = 0 To dsMenu.Tables(0).Rows.Count - 1
    '            mMenu = dsMenu.Tables(0).Rows(i).Item("MenuName") & ""
    '            SetAllMenuItems(MenuStrip, mMenu)
    '        Next i
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical)
    '    Finally
    '        dsMenu = Nothing
    '    End Try
    'End Sub

    Private Sub SetAllMenuItems(ByRef menuStrip As MenuStrip, ByVal sMenuName As String)
        Dim c As ToolStripItem
        Dim t As ToolStripMenuItem
        For Each c In menuStrip.Items
            'c.Enabled = enable
            If c.GetType Is GetType(ToolStripMenuItem) Then
                t = c
                SetAllMenuItems(t.DropDownItems, sMenuName)
            End If
        Next
    End Sub
    Private Sub SetAllMenuItems(ByRef menus As ToolStripItemCollection, ByVal sMenuName As String)
        Dim c As ToolStripItem
        Dim t As ToolStripMenuItem
        For Each c In menus
            'c.Enabled = enable
            If c.GetType Is GetType(ToolStripMenuItem) Then
                If sMenuName = c.Name Then
                    c.Enabled = True
                End If
                t = c
                SetAllMenuItems(t.DropDownItems, sMenuName)
            End If
        Next
    End Sub

    Public Sub DispUserGroupCodeDetails()
        Dim DrUG As DataRow()
        Dim DrU As DataRow()
        Dim i As Integer
        Dim dm As New DataManager

        If Trim(Objuser.CompanyName) = "ABIRAAME JEWELLERS" Then
            mnueTRSTicket.Text = "Discount Ticket"
        End If

        mnuCompany.Enabled = False
        mnuLocation.Enabled = False
        mnuTerminal.Enabled = False
        mnuClass.Enabled = False
        mnuCategory.Enabled = False
        mnuCatalogue.Enabled = False
        mnuSupplier.Enabled = False
        mnuCustomer.Enabled = False
        mnuOutletCustomers.Enabled = False
        mnuCustomerPointAdjustment.Enabled = False
        mnuCustomerTypeChange.Enabled = False
        mnuSalesman.Enabled = False
        mnuCurrency.Enabled = False
        mnuPaymode.Enabled = False
        mnuTrayMaster.Enabled = False
        mnuTrayTransfer.Enabled = False
        mnuSize.Enabled = False
        mnuRefiner.Enabled = False
        mnuCountry.Enabled = False
        mnuPayOutMaster.Enabled = False
        mnuReOrderLevelMaster.Enabled = False
        mnuJobSheet.Enabled = False
        mnuPurchase.Enabled = False
        mnuDirectPurchase.Enabled = False
        mnuFixCostJobSheet.Enabled = False
        mnuBarCode.Enabled = False
        mnuInventorySearch.Enabled = False
        mnuInventoryMaintenance.Enabled = False
        mnuInventoryMovement.Enabled = False
        mnuCatalogueSearch.Enabled = False
        mnuSplitInventory.Enabled = False
        mnuMergeInventory.Enabled = False
        mnuPurchaseReturn.Enabled = False
        mnuSalesOrder.Enabled = False
        mnuPOS.Enabled = False
        mnuPaymentEntries.Enabled = False
        mnuPaymodeModification.Enabled = False
        mnuSalesReturn.Enabled = False
        mnuPayOut.Enabled = False
        mnuInstallmentEntry.Enabled = False
        mnuInstallment.Enabled = False
        mnuOrderInventory.Enabled = False
        mnuOrderPayment1.Enabled = False
        mnuRepairEntry.Enabled = False
        mnuRepairPayment.Enabled = False
        mnuGoldBarIssue.Enabled = False
        mnuTradeInGoldIssue.Enabled = False
        mnuTransferOut.Enabled = False
        mnuTransferIn.Enabled = False
        mnuTransferStatus.Enabled = False
        mnuMasterReports.Enabled = False
        mnuCustomerReports.Enabled = False
        mnuPurchaseReports.Enabled = False
        mnuInventoryReports.Enabled = False
        mnuSalesReports.Enabled = False
        mnuManagementReports.Enabled = False
        mnuDayEnd.Enabled = False
        mnuSettlement.Enabled = False
        mnuUserGroup.Enabled = False
        mnuUserMaster.Enabled = False
        mnuUserPermission.Enabled = False
        mnuChangePassword.Enabled = False
        MenuItem60.Enabled = False
        mnuTradeInWholeSales.Enabled = False
        mnuGoldBarFix.Enabled = False
        mnuSupplierMC.Enabled = False
        mnuPurchaseTally.Enabled = False
        mnuWholeSalePOS.Enabled = False
        mnuSupplierPayment.Enabled = False
        mnuPaymentEntryAksayaTritiya.Enabled = False
        mnuSalesHistory.Enabled = False
        mnuCreditSalesPayment.Enabled = False
        mnuGoldBarPurchase.Enabled = False
        mnuTradeInMerge.Enabled = False
        mnuOnlineTransfer.Enabled = False
        mnuTransferReports.Enabled = False
        mnuTransactionReports.Enabled = False
        MenuItem66.Enabled = False
        mnueTRSTicket.Enabled = False
        mnuEODReports.Enabled = True
        mnuAnalysisReports.Enabled = False
        btnMainBarCode.Enabled = False
        btnMainCatalogue.Enabled = False
        btnMainCategory.Enabled = False
        btnMainClass.Enabled = False
        btnMainCreditSales.Enabled = False
        btnMainInventory.Enabled = False
        btnMainJobsheet.Enabled = False
        btnMainOutletDayEnd.Enabled = False
        btnMainOutletInstallment.Enabled = False
        btnMainOutletInventory.Enabled = True
        btnMainOutletOrderEntry.Enabled = False
        btnMainOutletPaymentEntry.Enabled = False
        btnMainOutletPos.Enabled = False
        btnMainOutletSearch.Enabled = False
        btnMainOutletSettlement.Enabled = False
        btnMainOutletTransferIn.Enabled = False
        btnMainOutletTransferOut.Enabled = False
        btnMainOutletUpdateHQ.Enabled = False
        btnMainPurchase.Enabled = False
        btnMainSearch.Enabled = False
        btnMainTransferIn.Enabled = False
        btnMainTransferOut.Enabled = False
        mnuTradeInSplit.Enabled = False
        mnuCustomerDeposit.Enabled = False
        mnuSTockTradeInTransfer.Enabled = True
        DrUG = dm.RowReturnProjExecNonQryWithDataRow("Select * from tblUserPermission a,tblForms b where a.FormCode=b.FormCode and UserGroupCode=" & fncFormatSQLText(sUserID))
        If DrUG.Length > 0 Then
            For i = 0 To DrUG.Length - 1
                If Trim(DrUG(i)("AllowAdd") & "") = "A" Then
                    'If Trim(DrUG(i)("MenuName") & "") = "mnuCompany" Then
                    '    mnuCompany.Enabled = True
                    'End If
                    Select Case Trim(DrUG(i)("MenuName") & "")
                        Case "mnuCompany"
                            mnuCompany.Enabled = True
                        Case "mnuLocation"
                            mnuLocation.Enabled = True
                        Case "mnuTerminal"
                            mnuTerminal.Enabled = True
                        Case "mnuClass"
                            mnuClass.Enabled = True
                            btnMainClass.Enabled = True
                        Case "mnuCategory"
                            mnuCategory.Enabled = True
                            btnMainCategory.Enabled = True
                        Case "mnuCatalogue"
                            mnuCatalogue.Enabled = True
                            btnMainCatalogue.Enabled = True
                            mnuTradeInWholeSales.Enabled = True
                            mnuTradeInSplit.Enabled = True
                            mnuCustomerDeposit.Enabled = True
                        Case "mnuSupplier"
                            mnuSupplier.Enabled = True
                        Case "mnuCustomer"
                            mnuCustomer.Enabled = True
                        Case "mnuOutletCustomers"
                            mnuOutletCustomers.Enabled = True
                        Case "mnuCustomerPointAdjustment"
                            mnuCustomerPointAdjustment.Enabled = True
                        Case "mnuCustomerTypeChange"
                            mnuCustomerTypeChange.Enabled = True
                        Case "mnuSalesman"
                            mnuSalesman.Enabled = True
                        Case "mnuCurrency"
                            mnuCurrency.Enabled = True
                        Case "mnuPaymode"
                            mnuPaymode.Enabled = True
                        Case "mnuTrayMaster"
                            mnuTrayMaster.Enabled = True
                        Case "mnuTrayTransfer"
                            mnuTrayTransfer.Enabled = True
                        Case "mnuSize"
                            mnuSize.Enabled = True
                        Case "mnuRefiner"
                            mnuRefiner.Enabled = True
                        Case "mnuCountry"
                            mnuCountry.Enabled = True
                        Case "mnuPayOutMaster"
                            mnuPayOutMaster.Enabled = True
                        Case "mnuReOrderLevelMaster"
                            mnuReOrderLevelMaster.Enabled = True
                        Case "mnuJobSheet"
                            mnuJobSheet.Enabled = True
                            btnMainJobsheet.Enabled = True
                        Case "mnuPurchase"
                            mnuPurchase.Enabled = True
                            btnMainPurchase.Enabled = True
                        Case "mnudirectPurchase"
                            mnuDirectPurchase.Enabled = True
                        Case "mnuFixCostJobSheet"
                            mnuFixCostJobSheet.Enabled = True
                        Case "mnuBarCode"
                            mnuBarCode.Enabled = True
                            btnMainBarCode.Enabled = True
                        Case "mnuInventorySearch"
                            mnuInventorySearch.Enabled = True
                            btnMainOutletSearch.Enabled = True
                            btnMainSearch.Enabled = True
                        Case "mnuInventoryMaintenance"
                            mnuInventoryMaintenance.Enabled = True
                            btnMainInventory.Enabled = True
                        Case "mnuInventoryMovement"
                            mnuInventoryMovement.Enabled = True
                        Case "mnuCatalogueSearch"
                            mnuCatalogueSearch.Enabled = True
                        Case "mnuSplitInventory"
                            mnuSplitInventory.Enabled = True
                        Case "mnuMergeInventory"
                            mnuMergeInventory.Enabled = True
                        Case "mnuPurchaseReturn"
                            mnuPurchaseReturn.Enabled = True
                        Case "mnuSalesOrder"
                            mnuSalesOrder.Enabled = True
                        Case "mnuPOS"
                            mnuPOS.Enabled = True
                            btnMainOutletPos.Enabled = True
                        Case "mnuPaymentEntries"
                            mnuPaymentEntries.Enabled = True
                            btnMainOutletPaymentEntry.Enabled = True
                        Case "mnuPaymodeModification"
                            mnuPaymodeModification.Enabled = True
                        Case "mnuSalesReturn"
                            mnuSalesReturn.Enabled = True
                        Case "mnuPayOut"
                            mnuPayOut.Enabled = True
                        Case "mnuInstallmentEntry"
                            mnuInstallmentEntry.Enabled = True
                            btnMainOutletOrderEntry.Enabled = True
                        Case "mnuInstallment"
                            mnuInstallment.Enabled = True
                            btnMainOutletInstallment.Enabled = True
                        Case "mnuOrderInventory"
                            mnuOrderInventory.Enabled = True
                        Case "mnuOrderPayment1"
                            mnuOrderPayment1.Enabled = True
                        Case "mnuRepairEntry"
                            mnuRepairEntry.Enabled = True
                        Case "mnuRepairPayment"
                            mnuRepairPayment.Enabled = True
                        Case "mnuGoldBarIssue"
                            mnuGoldBarIssue.Enabled = True
                        Case "mnuTradeInGoldIssue"
                            mnuTradeInGoldIssue.Enabled = True
                        Case "mnuTransferOut"
                            mnuTransferOut.Enabled = True
                            btnMainOutletTransferOut.Enabled = True
                            btnMainTransferOut.Enabled = True
                        Case "mnuTransferIn"
                            mnuTransferIn.Enabled = True
                            btnMainOutletTransferIn.Enabled = True
                            btnMainTransferIn.Enabled = True
                        Case "mnuTransferStatus"
                            mnuTransferStatus.Enabled = True
                        Case "mnuMasterReports"
                            mnuMasterReports.Enabled = True
                        Case "mnuCustomerReports"
                            mnuCustomerReports.Enabled = True
                        Case "mnuPurchaseReports"
                            mnuPurchaseReports.Enabled = True
                        Case "mnuInventoryReports"
                            mnuInventoryReports.Enabled = True
                        Case "mnuSalesReports"
                            mnuSalesReports.Enabled = True
                        Case "mnuManagementReports"
                            mnuManagementReports.Enabled = True
                        Case "mnuDayEnd"
                            mnuDayEnd.Enabled = True
                            btnMainOutletDayEnd.Enabled = True
                            btnMainOutletUpdateHQ.Enabled = True
                        Case "mnuSettlement"
                            mnuSettlement.Enabled = True
                            btnMainOutletSettlement.Enabled = True
                        Case "mnuUserGroup"
                            mnuUserGroup.Enabled = True
                        Case "mnuUserMaster"
                            mnuUserMaster.Enabled = True
                        Case "mnuUserPermission"
                            mnuUserPermission.Enabled = True
                        Case "mnuChangePassword"
                            mnuChangePassword.Enabled = True
                        Case "MenuItem60"
                            MenuItem60.Enabled = True

                        Case "mnuGoldBarFix"
                            mnuGoldBarFix.Enabled = True
                        Case "mnuSupplierMC"
                            mnuSupplierMC.Enabled = True
                        Case "mnuPurchaseTally"
                            mnuPurchaseTally.Enabled = True
                        Case "mnuWholeSalePOS"
                            mnuWholeSalePOS.Enabled = True
                            btnMainCreditSales.Enabled = True
                        Case "mnuSupplierPayment"
                            mnuSupplierPayment.Enabled = True
                        Case "mnuPaymentEntryAksayaTritiya"
                            mnuPaymentEntryAksayaTritiya.Enabled = True

                        Case "mnuSalesHistory"
                            mnuSalesHistory.Enabled = True
                        Case "mnuCreditSalesPayment"
                            mnuCreditSalesPayment.Enabled = True
                        Case "mnuGoldBarPurchase"
                            mnuGoldBarPurchase.Enabled = True
                        Case "mnuTradeInMerge"
                            mnuTradeInMerge.Enabled = True
                        Case "mnuOnlineTransfer"
                            mnuOnlineTransfer.Enabled = True
                        Case "mnuTransferReports"
                            mnuTransferReports.Enabled = True
                        Case "mnuTransactionReports"
                            mnuTransactionReports.Enabled = True
                        Case "MenuItem66"
                            MenuItem66.Enabled = True
                        Case "mnueTRSTicket"
                            mnueTRSTicket.Enabled = True
                    End Select



                End If
            Next
        End If
    End Sub
    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        getServerDate()
        DispUserGroupCodeDetails()
        ' subSetMenu(sUserGroup)
        'If sUserGroup = "SALES" Then
        '    mnuMasterCompany.Visible = False
        '    mnuMasterInventory.Visible = False
        '    mnuMasterIPM.Visible = False
        '    mnuMasterProcrument.Visible = False
        '    mnuCurrency.Visible = False
        '    mnuSalesman.Visible = False
        '    mnuPaymode.Visible = False
        '    MenuItem2.Visible = False
        '    MenuItem4.Visible = False
        '    MenuItem6.Visible = False
        '    MenuItem70.Visible = False
        '    mnuPayOutMaster.Visible = False
        '    mnuReOrderLevelMaster.Visible = False
        '    mnuPurchaseHeader.Enabled = False
        '    mnuHeadTransaction.Enabled = False
        '    mnuHeadAccounts.Enabled = False
        '    mnuDailyProcedure.Enabled = False
        '    mnuSecurity.Enabled = False
        '    mnuMasterReports.Enabled = False
        '    mnuInventoryReports.Enabled = False
        '    mnuPurchaseReports.Enabled = False
        '    mnuSalesReports.Enabled = False
        '    mnuSettlementReports.Enabled = False
        '    mnuEODReports.Enabled = False
        '    mnuManagementReports.Enabled = False
        '    mnuGoldbarReports.Enabled = False
        '    mnuAuditingReports.Enabled = False
        '    mnuTransactionReports.Enabled = False
        '    mnuDailySales.Enabled = False
        '    mnuAnalysisReports.Enabled = False
        '    mnuPaymentEntries.Visible = False
        '    mnuPaymodeModification.Visible = False
        '    mnuSalesReturn.Visible = False
        '    mnuPayOut.Visible = False
        '    mnuPaymentEntryAksayaTritiya.Visible = False
        '    mnuHeadReports.Enabled = False
        '    mnuTradeInWholeSales.Visible = False
        '    mnuOrderInventory.Enabled = False
        '    mnuHeadUtility.Enabled = False
        '    btnMainOutletPaymentEntry.Enabled = False
        '    btnMainOutletTransferIn.Enabled = False
        '    btnMainOutletTransferOut.Enabled = False
        '    btnMainOutletSettlement.Enabled = False
        '    btnMainOutletDayEnd.Enabled = False
        '    btnMainOutletUpdateHQ.Enabled = False

        '    'MenuItem3.Enabled = False
        '    'mnuPurchaseHeader.Enabled = False
        '    'mnuHeadTransaction.Enabled = False
        '    'mnuHeadAccounts.Enabled = False
        '    'mnuDailyProcedure.Enabled = False
        '    'mnuSecurity.Enabled = False
        '    'mnuMasterReports.Enabled = False
        '    'mnuInventoryReports.Enabled = False
        '    'mnuPurchaseReports.Enabled = False
        '    'mnuSalesReports.Enabled = True
        '    'mnuSettlementReports.Enabled = False
        '    'mnuEODReports.Enabled = False
        '    'mnuManagementReports.Enabled = False
        '    'mnuGoldbarReports.Enabled = False
        '    'mnuAuditingReports.Enabled = False
        '    'mnuTransactionReports.Enabled = True
        '    'mnuAuditingReports.Enabled = False
        '    'mnuDailySales.Enabled = False
        '    'mnuAnalysisReports.Enabled = False
        '    'mnuTransactionReports.Enabled = False
        '    'mnuHeadTransaction.Enabled = True
        '    'mnuHeadTransfer.Enabled = False
        '    'mnuPaymentEntries.Visible = False
        '    'mnuPaymodeModification.Visible = False
        '    'mnuSalesReturn.Visible = False
        '    'mnuPayOut.Visible = False
        '    'mnuPaymentEntryAksayaTritiya.Visible = False
        '    'mnuHeadReports.Enabled = False
        '    'mnuTradeInWholeSales.Visible = False
        '    'mnuOrderInventory.Enabled = False
        '    'mnuHeadUtility.Enabled = False
        '    'btnMainOutletPaymentEntry.Enabled = False
        '    'btnMainOutletTransferIn.Enabled = False
        '    'btnMainOutletTransferOut.Enabled = False
        '    'btnMainOutletSettlement.Enabled = False
        '    'btnMainOutletDayEnd.Enabled = False
        '    'btnMainOutletUpdateHQ.Enabled = False
        'ElseIf sUserGroup = "CASH" Then
        '    mnuMasterCompany.Visible = False
        '    mnuMasterInventory.Visible = False
        '    mnuMasterIPM.Visible = False
        '    mnuMasterProcrument.Visible = False
        '    mnuCurrency.Visible = False
        '    mnuSalesman.Visible = False
        '    mnuPaymode.Visible = False
        '    MenuItem2.Visible = False
        '    MenuItem4.Visible = False
        '    MenuItem6.Visible = False
        '    MenuItem70.Visible = False
        '    mnuPayOutMaster.Visible = False
        '    mnuReOrderLevelMaster.Visible = False
        '    mnuPurchaseHeader.Enabled = False
        '    mnuHeadTransaction.Enabled = False
        '    mnuHeadAccounts.Enabled = False
        '    mnuDailyProcedure.Enabled = True
        '    mnuSecurity.Enabled = False
        '    mnuMasterReports.Enabled = False
        '    mnuCustomerReports.Enabled = False
        '    mnuInventoryReports.Enabled = False
        '    mnuPurchaseReports.Enabled = False
        '    mnuSalesReports.Enabled = True
        '    mnuSettlementReports.Enabled = False
        '    mnuEODReports.Enabled = False
        '    mnuManagementReports.Enabled = False
        '    mnuGoldbarReports.Enabled = False
        '    mnuAuditingReports.Enabled = False
        '    mnuTransactionReports.Enabled = True
        '    mnuAuditingReports.Enabled = False
        '    mnuDailySales.Enabled = False
        '    mnuAnalysisReports.Enabled = False
        '    mnuTransactionReports.Enabled = False
        '    mnuHeadTransaction.Enabled = True
        '    mnuHeadTransfer.Enabled = False
        '    mnuPaymentEntries.Visible = True
        '    mnuPaymodeModification.Visible = True
        '    mnuSalesReturn.Visible = False
        '    mnuPayOut.Visible = True
        '    mnuPaymentEntryAksayaTritiya.Visible = False
        '    mnuHeadReports.Enabled = True
        '    mnuTradeInWholeSales.Visible = False
        '    mnuOrderInventory.Enabled = False
        '    btnMainOutletPaymentEntry.Enabled = True
        '    btnMainOutletTransferIn.Enabled = False
        '    btnMainOutletTransferOut.Enabled = False
        '    btnMainOutletSettlement.Enabled = True
        '    btnMainOutletDayEnd.Enabled = True
        '    btnMainOutletUpdateHQ.Enabled = True

        '    'MenuItem3.Enabled = False
        '    'mnuPurchaseHeader.Enabled = False
        '    'mnuHeadTransaction.Enabled = False
        '    'mnuHeadAccounts.Enabled = False
        '    'mnuDailyProcedure.Enabled = True
        '    'mnuSecurity.Enabled = False
        '    'mnuMasterReports.Enabled = False
        '    'mnuCustomerReports.Enabled = False
        '    'mnuInventoryReports.Enabled = False
        '    'mnuPurchaseReports.Enabled = False
        '    'mnuSalesReports.Enabled = True
        '    'mnuSettlementReports.Enabled = False
        '    'mnuEODReports.Enabled = False
        '    'mnuManagementReports.Enabled = False
        '    'mnuGoldbarReports.Enabled = False
        '    'mnuAuditingReports.Enabled = False
        '    'mnuTransactionReports.Enabled = True
        '    'mnuAuditingReports.Enabled = False
        '    'mnuDailySales.Enabled = False
        '    'mnuAnalysisReports.Enabled = False
        '    'mnuTransactionReports.Enabled = False
        '    'mnuHeadTransaction.Enabled = True
        '    'mnuHeadTransfer.Enabled = False
        '    'mnuPaymentEntries.Visible = True
        '    'mnuPaymodeModification.Visible = True
        '    'mnuSalesReturn.Visible = False
        '    'mnuPayOut.Visible = True
        '    'mnuPaymentEntryAksayaTritiya.Visible = False
        '    'mnuHeadReports.Enabled = True
        '    'mnuTradeInWholeSales.Visible = False
        '    'mnuOrderInventory.Enabled = False
        '    'btnMainOutletPaymentEntry.Enabled = True
        '    'btnMainOutletTransferIn.Enabled = False
        '    'btnMainOutletTransferOut.Enabled = False
        '    'btnMainOutletSettlement.Enabled = True
        '    'btnMainOutletDayEnd.Enabled = True
        '    'btnMainOutletUpdateHQ.Enabled = True
        'ElseIf sUserGroup = "PURCH" Then
        '    MenuItem3.Enabled = True
        '    mnuHeadSales.Enabled = True
        '    mnuHeadTransaction.Enabled = True
        '    mnuHeadAccounts.Enabled = False
        '    mnuDailyProcedure.Enabled = True
        '    mnuSecurity.Enabled = False
        '    mnuMasterReports.Enabled = False
        '    mnuInventoryReports.Enabled = True
        '    mnuPurchaseReports.Enabled = True
        '    mnuSalesReports.Enabled = False
        '    mnuSettlementReports.Enabled = False
        '    mnuEODReports.Enabled = True
        '    mnuManagementReports.Enabled = False
        '    mnuGoldbarReports.Enabled = False
        '    mnuAuditingReports.Enabled = False
        '    mnuTransactionReports.Enabled = False
        '    mnuAuditingReports.Enabled = False
        '    mnuDailySales.Enabled = False
        '    mnuAnalysisReports.Enabled = False
        '    mnuTransactionReports.Enabled = False
        'ElseIf sUserGroup = "PURCH" Then
        '    MenuItem3.Enabled = True
        '    mnuHeadSales.Enabled = True
        '    mnuHeadTransaction.Enabled = True
        '    mnuHeadAccounts.Enabled = False
        '    mnuDailyProcedure.Enabled = True
        '    mnuSecurity.Enabled = False
        '    mnuMasterReports.Enabled = False
        '    mnuInventoryReports.Enabled = True
        '    mnuPurchaseReports.Enabled = True
        '    mnuSalesReports.Enabled = False
        '    mnuSettlementReports.Enabled = False
        '    mnuEODReports.Enabled = True
        '    mnuManagementReports.Enabled = False
        '    mnuGoldbarReports.Enabled = False
        '    mnuAuditingReports.Enabled = False
        '    mnuTransactionReports.Enabled = False
        '    mnuAuditingReports.Enabled = False
        '    mnuDailySales.Enabled = False
        '    mnuAnalysisReports.Enabled = False
        '    mnuTransactionReports.Enabled = False
        'End If
        If UCase(sCompanyShortCode) <> "ES" Then
            mnuJewellaryBarcode.Visible = False
            mnuSpecialBarcode.Visible = False
        End If
        If sRFIDModule = "1" Then
            mnuRFIDStocktake.Visible = True
        End If
        If Trim(gthisLocationType) = "HQ" Then
            grpMainHQ.Visible = True
        Else
            grpMainOutlet.Visible = True
        End If
        If Trim(gthisLocationType) = "HQ" Then
            mnuMasterCompany.Visible = True
            mnuMasterInventory.Visible = True
            mnuMasterIPM.Visible = True
            mnuMasterProcrument.Visible = True
            mnuCurrency.Visible = True
            mnuSalesman.Visible = True
            mnuPaymode.Visible = True
            MenuItem2.Visible = True
            MenuItem4.Visible = True
            MenuItem6.Visible = True
            MenuItem70.Visible = True
            mnuPayOutMaster.Visible = True
            mnuReOrderLevelMaster.Visible = True
            mnuJobSheet.Visible = True
            mnuPurchase.Visible = True
            mnuDirectPurchase.Visible = True
            mnuFixCostJobSheet.Visible = True
            MenuItem7.Visible = True
            mnuPurchaseReturn.Visible = True
            mnuSalesOrder.Visible = True
            MenuItem71.Visible = True
            mnuOrderInventory.Visible = True
            mnuSecurity.Visible = True
            mnuMasterReports.Visible = True
            mnuPurchaseReports.Visible = True
            mnuGoldbarReports.Visible = False
            mnuWholeSalePOS.Visible = True
            mnuTrayTransfer.Visible = True
            MenuItem85.Visible = True
            mnuCustomer.Visible = True
            MenuItem58.Visible = True
            mnuCustomerPointAdjustment.Visible = True
            mnuCustomerTypeChange.Visible = True
            mnuCatalogueSearch.Visible = True
            If Trim(sCompanyShortCode) = "AJ" Then
                mnuPurchaseTally.Visible = True
                mnuSupplierMC.Visible = True
                mnuPaymentEntryAksayaTritiya.Visible = True
                mnuGoldBarFix.Visible = True
            Else
                mnuPurchaseTally.Visible = False
                mnuSupplierMC.Visible = False
                mnuPaymentEntryAksayaTritiya.Visible = False
                mnuGoldBarFix.Visible = True
            End If
            If Trim(sCompanyShortCode) = "MJ" Then
                MenuItem68.Visible = True
            Else
                MenuItem68.Visible = False
            End If
            mnuTradeInWholeSales.Visible = True
            mnuTradeInSplit.Visible = True
            'Shabeer 20190917
            mnuCustomerDeposit.Visible = False
        Else
            mnuMasterCompany.Visible = False
            mnuMasterInventory.Visible = True
            mnuClass.Visible = False
            mnuCategory.Visible = False
            mnuCatalogue.Visible = False
            mnuTrayMaster.Visible = False
            mnuSize.Visible = False
            mnuMasterIPM.Visible = False
            mnuMasterProcrument.Visible = False
            mnuCurrency.Visible = False
            mnuSalesman.Visible = False
            mnuPaymode.Visible = False
            MenuItem2.Visible = False
            MenuItem4.Visible = False
            MenuItem6.Visible = False
            MenuItem70.Visible = False
            mnuPayOutMaster.Visible = False
            mnuReOrderLevelMaster.Visible = False
            mnuJobSheet.Visible = True
            mnuPurchase.Visible = True
            mnuDirectPurchase.Visible = True
            mnuFixCostJobSheet.Visible = False
            MenuItem7.Visible = False
            mnuPurchaseReturn.Visible = True
            mnuSalesOrder.Visible = False
            MenuItem71.Visible = False
            mnuOrderInventory.Visible = True
            mnuSecurity.Visible = False
            mnuMasterReports.Visible = False
            mnuPurchaseReports.Visible = False
            mnuGoldbarReports.Visible = False
            mnuWholeSalePOS.Visible = True
            mnuTrayTransfer.Visible = False
            MenuItem85.Visible = False
            '  mnuCustomer.Visible = True
            MenuItem58.Visible = True
            'mnuCustomerPointAdjustment.Visible = True
            'mnuCustomerTypeChange.Visible = True
            mnuCatalogueSearch.Visible = True
            mnuManagementReports.Visible = False
            mnuPurchaseTally.Visible = False
            mnuSupplierMC.Visible = False
            mnuManagementReports.Visible = True
            mnuClassRate.Visible = True
        End If

        'If sUserGroup = "SALES" Then
        '    mnuMasterCompany.Visible = False
        '    mnuMasterInventory.Visible = False
        '    mnuMasterIPM.Visible = False
        '    mnuMasterProcrument.Visible = False
        '    mnuCurrency.Visible = False
        '    mnuSalesman.Visible = False
        '    mnuPaymode.Visible = False
        '    MenuItem2.Visible = False
        '    MenuItem4.Visible = False
        '    MenuItem6.Visible = False
        '    MenuItem70.Visible = False
        '    mnuPayOutMaster.Visible = False
        '    mnuReOrderLevelMaster.Visible = False
        '    mnuPurchaseHeader.Enabled = False
        '    mnuHeadTransaction.Enabled = True
        '    mnuHeadTransfer.Enabled = False
        '    mnuHeadAccounts.Enabled = False
        '    mnuDailyProcedure.Enabled = False
        '    mnuSecurity.Enabled = False
        '    mnuMasterReports.Enabled = False
        '    mnuInventoryReports.Enabled = False
        '    mnuPurchaseReports.Enabled = False
        '    mnuSalesReports.Enabled = False
        '    mnuSettlementReports.Enabled = False
        '    mnuEODReports.Enabled = False
        '    mnuManagementReports.Enabled = False
        '    mnuGoldbarReports.Enabled = False
        '    mnuAuditingReports.Enabled = False
        '    mnuTransactionReports.Enabled = False
        '    mnuDailySales.Enabled = False
        '    mnuAnalysisReports.Enabled = False
        '    mnuPaymentEntries.Visible = False
        '    mnuPaymodeModification.Visible = False
        '    mnuSalesReturn.Visible = False
        '    mnuPayOut.Visible = False
        '    mnuPaymentEntryAksayaTritiya.Visible = False
        '    mnuHeadReports.Enabled = False
        '    mnuTradeInWholeSales.Visible = False
        '    mnuOrderInventory.Enabled = False
        '    mnuHeadUtility.Enabled = False
        '    mnuTrayTransfer.Visible = False
        '    mnuGoldBarIssue.Visible = False
        '    mnuGoldBarPurchase.Visible = False
        '    mnuTradeInMerge.Visible = False
        '    MenuItem31.Visible = False
        '    MenuItem12.Visible = False
        '    btnMainOutletPaymentEntry.Enabled = False
        '    btnMainOutletTransferIn.Enabled = False
        '    btnMainOutletTransferOut.Enabled = False
        '    btnMainOutletSettlement.Enabled = False
        '    btnMainOutletDayEnd.Enabled = False
        '    btnMainOutletUpdateHQ.Enabled = False

        '    'MenuItem3.Enabled = False
        '    'mnuPurchaseHeader.Enabled = False
        '    'mnuHeadTransaction.Enabled = False
        '    'mnuHeadAccounts.Enabled = False
        '    'mnuDailyProcedure.Enabled = False
        '    'mnuSecurity.Enabled = False
        '    'mnuMasterReports.Enabled = False
        '    'mnuInventoryReports.Enabled = False
        '    'mnuPurchaseReports.Enabled = False
        '    'mnuSalesReports.Enabled = True
        '    'mnuSettlementReports.Enabled = False
        '    'mnuEODReports.Enabled = False
        '    'mnuManagementReports.Enabled = False
        '    'mnuGoldbarReports.Enabled = False
        '    'mnuAuditingReports.Enabled = False
        '    'mnuTransactionReports.Enabled = True
        '    'mnuAuditingReports.Enabled = False
        '    'mnuDailySales.Enabled = False
        '    'mnuAnalysisReports.Enabled = False
        '    'mnuTransactionReports.Enabled = False
        '    'mnuHeadTransaction.Enabled = True
        '    'mnuHeadTransfer.Enabled = False
        '    'mnuPaymentEntries.Visible = False
        '    'mnuPaymodeModification.Visible = False
        '    'mnuSalesReturn.Visible = False
        '    'mnuPayOut.Visible = False
        '    'mnuPaymentEntryAksayaTritiya.Visible = False
        '    'mnuHeadReports.Enabled = False
        '    'mnuTradeInWholeSales.Visible = False
        '    'mnuOrderInventory.Enabled = False
        '    'mnuHeadUtility.Enabled = False
        '    'btnMainOutletPaymentEntry.Enabled = False
        '    'btnMainOutletTransferIn.Enabled = False
        '    'btnMainOutletTransferOut.Enabled = False
        '    'btnMainOutletSettlement.Enabled = False
        '    'btnMainOutletDayEnd.Enabled = False
        '    'btnMainOutletUpdateHQ.Enabled = False
        'ElseIf sUserGroup = "ADMIN" Then
        '    mnuMasterCompany.Enabled = False
        '    mnuLocation.Enabled = False
        '    mnuTerminal.Enabled = False
        '    mnuCurrency.Enabled = False
        '    mnuPayOut.Enabled = False

        'ElseIf sUserGroup = "CASH" Then
        '    mnuMasterCompany.Visible = False
        '    mnuMasterInventory.Visible = False
        '    mnuMasterIPM.Visible = False
        '    mnuMasterProcrument.Visible = False
        '    mnuCurrency.Visible = False
        '    mnuSalesman.Visible = False
        '    mnuPaymode.Visible = False
        '    MenuItem2.Visible = False
        '    MenuItem4.Visible = False
        '    MenuItem6.Visible = False
        '    MenuItem70.Visible = False
        '    mnuPayOutMaster.Visible = False
        '    mnuReOrderLevelMaster.Visible = False
        '    mnuPurchaseHeader.Enabled = False
        '    mnuHeadTransaction.Enabled = False
        '    mnuHeadAccounts.Enabled = False
        '    mnuDailyProcedure.Enabled = True
        '    mnuSecurity.Enabled = False
        '    mnuMasterReports.Enabled = False
        '    mnuCustomerReports.Enabled = False
        '    mnuInventoryReports.Enabled = False
        '    mnuPurchaseReports.Enabled = False
        '    mnuSalesReports.Enabled = True
        '    mnuSettlementReports.Enabled = False
        '    mnuEODReports.Enabled = False
        '    mnuManagementReports.Enabled = False
        '    mnuGoldbarReports.Enabled = False
        '    mnuAuditingReports.Enabled = False
        '    mnuTransactionReports.Enabled = True
        '    mnuAuditingReports.Enabled = False
        '    mnuDailySales.Enabled = False
        '    mnuAnalysisReports.Enabled = False
        '    mnuTransactionReports.Enabled = False
        '    mnuHeadTransaction.Enabled = True
        '    mnuHeadTransfer.Enabled = False
        '    mnuPaymentEntries.Visible = True
        '    mnuPaymodeModification.Visible = True
        '    mnuSalesReturn.Visible = False
        '    mnuPayOut.Visible = True
        '    mnuPaymentEntryAksayaTritiya.Visible = False
        '    mnuHeadReports.Enabled = True
        '    mnuTradeInWholeSales.Visible = False
        '    mnuOrderInventory.Enabled = False
        '    btnMainOutletPaymentEntry.Enabled = True
        '    btnMainOutletTransferIn.Enabled = False
        '    btnMainOutletTransferOut.Enabled = False
        '    btnMainOutletSettlement.Enabled = True
        '    btnMainOutletDayEnd.Enabled = True
        '    btnMainOutletUpdateHQ.Enabled = True

        '    'MenuItem3.Enabled = False
        '    'mnuPurchaseHeader.Enabled = False
        '    'mnuHeadTransaction.Enabled = False
        '    'mnuHeadAccounts.Enabled = False
        '    'mnuDailyProcedure.Enabled = True
        '    'mnuSecurity.Enabled = False
        '    'mnuMasterReports.Enabled = False
        '    'mnuCustomerReports.Enabled = False
        '    'mnuInventoryReports.Enabled = False
        '    'mnuPurchaseReports.Enabled = False
        '    'mnuSalesReports.Enabled = True
        '    'mnuSettlementReports.Enabled = False
        '    'mnuEODReports.Enabled = False
        '    'mnuManagementReports.Enabled = False
        '    'mnuGoldbarReports.Enabled = False
        '    'mnuAuditingReports.Enabled = False
        '    'mnuTransactionReports.Enabled = True
        '    'mnuAuditingReports.Enabled = False
        '    'mnuDailySales.Enabled = False
        '    'mnuAnalysisReports.Enabled = False
        '    'mnuTransactionReports.Enabled = False
        '    'mnuHeadTransaction.Enabled = True
        '    'mnuHeadTransfer.Enabled = False
        '    'mnuPaymentEntries.Visible = True
        '    'mnuPaymodeModification.Visible = True
        '    'mnuSalesReturn.Visible = False
        '    'mnuPayOut.Visible = True
        '    'mnuPaymentEntryAksayaTritiya.Visible = False
        '    'mnuHeadReports.Enabled = True
        '    'mnuTradeInWholeSales.Visible = False
        '    'mnuOrderInventory.Enabled = False
        '    'btnMainOutletPaymentEntry.Enabled = True
        '    'btnMainOutletTransferIn.Enabled = False
        '    'btnMainOutletTransferOut.Enabled = False
        '    'btnMainOutletSettlement.Enabled = True
        '    'btnMainOutletDayEnd.Enabled = True
        '    'btnMainOutletUpdateHQ.Enabled = True
        'ElseIf sUserGroup = "PURCH" Then
        '    MenuItem3.Enabled = True
        '    mnuHeadSales.Enabled = True
        '    mnuHeadTransaction.Enabled = True
        '    mnuHeadAccounts.Enabled = False
        '    mnuDailyProcedure.Enabled = True
        '    mnuSecurity.Enabled = False
        '    mnuMasterReports.Enabled = False
        '    mnuInventoryReports.Enabled = True
        '    mnuPurchaseReports.Enabled = True
        '    mnuSalesReports.Enabled = False
        '    mnuSettlementReports.Enabled = False
        '    mnuEODReports.Enabled = True
        '    mnuManagementReports.Enabled = False
        '    mnuGoldbarReports.Enabled = False
        '    mnuAuditingReports.Enabled = False
        '    mnuTransactionReports.Enabled = False
        '    mnuAuditingReports.Enabled = False
        '    mnuDailySales.Enabled = False
        '    mnuAnalysisReports.Enabled = False
        '    mnuTransactionReports.Enabled = False
        'ElseIf sUserGroup = "PURCH" Then
        '    MenuItem3.Enabled = True
        '    mnuHeadSales.Enabled = True
        '    mnuHeadTransaction.Enabled = True
        '    mnuHeadAccounts.Enabled = False
        '    mnuDailyProcedure.Enabled = True
        '    mnuSecurity.Enabled = False
        '    mnuMasterReports.Enabled = False
        '    mnuInventoryReports.Enabled = True
        '    mnuPurchaseReports.Enabled = True
        '    mnuSalesReports.Enabled = False
        '    mnuSettlementReports.Enabled = False
        '    mnuEODReports.Enabled = True
        '    mnuManagementReports.Enabled = False
        '    mnuGoldbarReports.Enabled = False
        '    mnuAuditingReports.Enabled = False
        '    mnuTransactionReports.Enabled = False
        '    mnuAuditingReports.Enabled = False
        '    mnuDailySales.Enabled = False
        '    mnuAnalysisReports.Enabled = False
        '    mnuTransactionReports.Enabled = False
        '    mnuManagementReports.Enabled = False
        'End If


        Me.Text = "Unipro SmartGold UserID : " & Trim(sUserID)
        StartTime = Now()
        Timer1.Start()
        Timer1.Interval = 10000

    End Sub

    Private Sub mnuLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        sLogInFlag = "S"
        Dim Form1 As New frmSignon
        Form1.Show()
        Form1.Dispose()
    End Sub

    Private Sub frmMain_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated

        If sMemberModule = "1" Then
            mnuMemberModule.Visible = True
            mnuMemberModule.Enabled = True
            mnuAnalysisReports.Visible = True
            mnuAnalysisReports.Enabled = True
        Else
            mnuMemberModule.Visible = False
            mnuMemberModule.Enabled = False
            mnuAnalysisReports.Visible = False
            mnuAnalysisReports.Enabled = False
        End If
        If sAccountsModule = "1" Then
            mnuHeadAccounts.Visible = True
            mnuHeadAccounts.Enabled = True
        Else
            mnuHeadAccounts.Visible = False
            mnuHeadAccounts.Enabled = False
        End If
        dgvOrder.Visible = False
        'subLoadOrderStatus()
        'subLoadUnfixJobSheet()
        If Trim(gthisLocationType) <> "HQ" Then
            subLoadInstallmentStatus()
            dgvInstallment.Visible = False
        ElseIf Trim(gthisLocationType) = "HQ" Then
            ' dgvInstallment.Visible = False
            subLoadInstallmentStatus()
            dgvInstallment.Visible = False
        End If
        ' subReOrderLevel()
        If sLogInFlag <> "S" Then Exit Sub
        sLogInFlag = ""

    End Sub
    Private Sub subLoadUnfixJobSheet()
        Dim sSql, sFromLocStatus As String
        Dim iRows, iCount As Integer
        Dim DM As New DataManager
        Dim drH As DataRow()

        'sSql &= "Select * from tblInventoryTransferHeader Where FromLocation = " & fncFormatSQLText(sLocation)
        'sSql &= " And TransferDate = " & fncFormatSQLDate(sTransferDate)

        sSql = "SELECT JobSheetNo,JobSheetDate,SupplierCode,PurchaseBillNo FROM TblJobSheetHeader  "
        sSql = sSql & " where isnull(FixStatus,'N')='N'"

        drH = DM.RowReturnProjExecNonQryWithDataRow(sSql)
        dgvunfixJobsheet.RowCount = 0
        If drH.Length > 0 Then
            dgvunfixJobsheet.Visible = True
            For iRows = 0 To drH.Length - 1
                dgvunfixJobsheet.Rows.Add()
                fnSetdgvText(dgvunfixJobsheet, iRows, 0, Trim(drH(iRows)("JobSheetNo") & ""))
                fnSetdgvText(dgvunfixJobsheet, iRows, 1, Trim(drH(iRows)("JobSheetDate") & ""))
                fnSetdgvText(dgvunfixJobsheet, iRows, 2, Trim(drH(iRows)("SupplierCode") & ""))
                fnSetdgvText(dgvunfixJobsheet, iRows, 3, Trim(drH(iRows)("PurchaseBillNo") & ""))
            Next
        End If
        drH = Nothing
        sSql = Nothing
    End Sub
    Private Sub subReOrderLevel()
        Dim sSql, sFromLocStatus As String
        Dim iRows, iCount As Integer
        Dim DM As New DataManager
        Dim drH As DataRow()

        'sSql &= "Select * from tblInventoryTransferHeader Where FromLocation = " & fncFormatSQLText(sLocation)
        ''sSql &= " And TransferDate = " & fncFormatSQLDate(sTransferDate)
        'sSql = "Select c.Cataloguecode,c.CategoryCode,c.ReOrderLevel,sum(Qtyonhand)Qty from tblInventory a,"
        'sSql = sSql & " tblInventoryStock b,tblCatalogue c where a.Cataloguecode = c.Cataloguecode And a.InventoryCode = b.InventoryCode"
        'sSql = sSql & " and isnull(c.Reorderlevel,0)>Qtyonhand Group by c.Cataloguecode,c.CategoryCode,ReOrderLevel"
        sSql = "Select b.Locationcode,c.Cataloguecode,c.CategoryCode,c.ReOrderLevel,sum(Qtyonhand)QTy from tblInventory a,"
        sSql = sSql & " tblInventoryStock b,tblCatalogue c where a.Cataloguecode = c.Cataloguecode And a.InventoryCode = b.InventoryCode"
        sSql = sSql & " and b.locationcode<>'HQ'"
        sSql = sSql & " Group by  b.Locationcode,c.Cataloguecode,c.CategoryCode,ReOrderLevel"
        sSql = sSql & " having(isnull(c.Reorderlevel, 0) > sum(Qtyonhand))"

        drH = DM.RowReturnProjExecNonQryWithDataRow(sSql)
        dgvReOrder.RowCount = 0
        If drH.Length > 0 Then
            grpReOrder.Visible = True
            For iRows = 0 To drH.Length - 1
                dgvReOrder.Rows.Add()
                fnSetdgvText(dgvReOrder, iRows, 0, Trim(drH(iRows)("Locationcode") & ""))
                fnSetdgvText(dgvReOrder, iRows, 1, Trim(drH(iRows)("Cataloguecode") & ""))
                fnSetdgvText(dgvReOrder, iRows, 2, Trim(drH(iRows)("CategoryCode") & ""))
                fnSetdgvText(dgvReOrder, iRows, 3, Trim(drH(iRows)("ReOrderLevel") & ""))
                fnSetdgvText(dgvReOrder, iRows, 4, Trim(drH(iRows)("Qty") & ""))
            Next
        End If
        drH = Nothing
        sSql = Nothing
    End Sub
    Private Sub subLoadOrderStatus()
        Dim sSql, sFromLocStatus As String
        Dim iRows, iCount As Integer
        Dim DM As New DataManager
        Dim drH As DataRow()

        'sSql &= "Select * from tblInventoryTransferHeader Where FromLocation = " & fncFormatSQLText(sLocation)
        'sSql &= " And TransferDate = " & fncFormatSQLDate(sTransferDate)

        'sSql = "SELECT OrderNo,OrderDate,DeliveryDate,a.CustomerCode,Mobile FROM TBLINVENTORYORDERHEADER a,tblCustomer b WHERE "
        'sSql = sSql & " a.CustomerCode = b.CustomerCode And DateDiff(Day, getdate(), deliverydate) <= 5 and InvType='O'"
        'sSql = sSql & " and a.Locationcode =" & fncFormatSQLText(sLocationCode)
        If Trim(gthisLocationType) <> "HQ" Then
            sSql = "SELECT distinct a.OrderNo,Locationcode,OrderDate,DeliveryDate,a.CustomerCode,Mobile,"
            sSql = sSql & " case when datediff(dd,getdate(),DeliveryDate)<0 then 'A' when datediff(dd,getdate(),DeliveryDate)<7 and datediff(dd,getdate(),DeliveryDate)>0 "
            sSql = sSql & " then 'B' when  datediff(dd,getdate(),DeliveryDate)>7 and isnull(a.suppliercode,'')<>'' then 'D' else 'C' end Value,SupplierCode Supplier "
            sSql = sSql & " FROM tblinventoryorderdetail c,TBLINVENTORYORDERHEADER a left join tblCustomer b on a.CustomerCode = b.CustomerCode WHERE "
            sSql = sSql & " a.orderno=c.orderno and inventorycode=''"
            sSql = sSql & " and a.Locationcode =" & fncFormatSQLText(sLocationCode)
        Else
            sSql = "SELECT distinct a.OrderNo,locationcode,OrderDate,DeliveryDate,a.CustomerCode,Mobile,case when datediff(dd,getdate(),DeliveryDate)<0 then 'A' when datediff(dd,getdate(),DeliveryDate)<7 and datediff(dd,getdate(),DeliveryDate)>0 then 'B' when  datediff(dd,getdate(),DeliveryDate)>7 and isnull(a.suppliercode,'')<>'' then 'D'  else 'C' end Value,SupplierCode Supplier "
            sSql = sSql & "FROM tblinventoryorderdetail c,TBLINVENTORYORDERHEADER a left join tblCustomer b on a.CustomerCode = b.CustomerCode WHERE "
            sSql = sSql & " a.orderno=c.orderno and inventorycode=''"
        End If


        drH = DM.RowReturnProjExecNonQryWithDataRow(sSql)
        dgvOrder.RowCount = 0
        If drH.Length > 0 Then
            dgvOrder.Visible = True
            For iRows = 0 To drH.Length - 1
                dgvOrder.Rows.Add()
                fnSetdgvText(dgvOrder, iRows, 0, Trim(drH(iRows)("OrderNo") & ""))
                fnSetdgvText(dgvOrder, iRows, 1, Trim(drH(iRows)("LocationCode") & ""))
                fnSetdgvText(dgvOrder, iRows, 2, Trim(drH(iRows)("OrderDate") & ""))
                fnSetdgvText(dgvOrder, iRows, 3, Trim(drH(iRows)("DeliveryDate") & ""))
                fnSetdgvText(dgvOrder, iRows, 4, Trim(drH(iRows)("CustomerCode") & ""))
                fnSetdgvText(dgvOrder, iRows, 5, Trim(drH(iRows)("Mobile") & ""))
                fnSetdgvText(dgvOrder, iRows, 6, Trim(drH(iRows)("Value") & ""))
                fnSetdgvText(dgvOrder, iRows, 7, Trim(drH(iRows)("Supplier") & ""))
            Next
        End If
        drH = Nothing
        sSql = Nothing
    End Sub
    Private Sub subLoadInstallmentStatus()
        Dim sSql, sFromLocStatus As String
        Dim iRows, iCount As Integer
        Dim DM As New DataManager
        Dim drH As DataRow()

        'sSql &= "Select * from tblInventoryTransferHeader Where FromLocation = " & fncFormatSQLText(sLocation)
        'sSql &= " And TransferDate = " & fncFormatSQLDate(sTransferDate)

        'sSql = "SELECT OrderNo,OrderDate,DeliveryDate,a.CustomerCode,Mobile FROM TBLINVENTORYORDERHEADER a,tblCustomer b WHERE "
        'sSql = sSql & " a.CustomerCode = b.CustomerCode And DateDiff(Day, getdate(), deliverydate) <= 5 and InvType='O'"
        'sSql = sSql & " and a.Locationcode =" & fncFormatSQLText(sLocationCode)
        If Trim(gthisLocationType) <> "HQ" Then
            sSql = "SELECT InsAgreementNo,LocationCode,InvoiceDate,LastDate,a.CustomerCode,Mobile,totalcost-(depositamt+AmtPaid) Balance,"
            sSql = sSql & " case when datediff(dd,getdate(),LastDate)<0 then 'A' else 'B' end Value FROM tblInstallmentEntryHeader a left join tblCustomer b on a.CustomerCode=b.CustomerCode  "
            sSql = sSql & " where  InsAgreementNo not  in(select InsAgreementNo"
            sSql = sSql & " from tblSalesHeader where InsAgreementNo<>'') and totalcost-(depositamt+AmtPaid) > 0 "
            sSql = sSql & " and a.Locationcode =" & fncFormatSQLText(sLocationCode)
            sSql = sSql & " order by InsAgreementNo"
        Else
            sSql = "SELECT InsAgreementNo,LocationCode,InvoiceDate,LastDate,a.CustomerCode,Mobile,totalcost-(depositamt+AmtPaid) Balance,"
            sSql = sSql & " case when datediff(dd,getdate(),LastDate)<0 then 'A' else 'B' end Value FROM tblInstallmentEntryHeader a left join tblCustomer b on a.CustomerCode=b.CustomerCode  "
            sSql = sSql & " where InsAgreementNo not  in(select InsAgreementNo"
            sSql = sSql & " from tblSalesHeader where InsAgreementNo<>'') and totalcost-(depositamt+AmtPaid) > 0 "
            sSql = sSql & " order by InsAgreementNo"
        End If

        drH = DM.RowReturnProjExecNonQryWithDataRow(sSql)
        dgvInstallment.RowCount = 0
        If drH.Length > 0 Then
            dgvInstallment.Visible = True
            For iRows = 0 To drH.Length - 1
                dgvInstallment.Rows.Add()
                fnSetdgvText(dgvInstallment, iRows, 0, Trim(drH(iRows)("InsAgreementNo") & ""))
                fnSetdgvText(dgvInstallment, iRows, 1, Trim(drH(iRows)("LocationCode") & ""))
                fnSetdgvText(dgvInstallment, iRows, 2, Trim(drH(iRows)("InvoiceDate") & ""))
                fnSetdgvText(dgvInstallment, iRows, 3, Trim(drH(iRows)("LastDate") & ""))
                fnSetdgvText(dgvInstallment, iRows, 4, Trim(drH(iRows)("CustomerCode") & ""))
                fnSetdgvText(dgvInstallment, iRows, 5, Trim(drH(iRows)("Mobile") & ""))
                fnSetNumeric(dgvInstallment, iRows, 6, Val(drH(iRows)("Balance")))
                fnSetdgvText(dgvInstallment, iRows, 7, Trim(drH(iRows)("Value") & ""))
            Next
        End If
        drH = Nothing
        sSql = Nothing
    End Sub
    Private Sub mnuClass_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuClass.Click
        Dim objFrm As New frmClass
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCategory.Click
        If ((sCompanyShortCode = "ML")) Then
            Dim objfrm As New frmCategoryML
            objfrm.Show()
            objfrm = Nothing
        Else
            Dim objfrm As New frmCategory
            objfrm.Show()
            objfrm = Nothing
        End If
    End Sub

    Private Sub mnuClassRate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuClassRate.Click
        Dim objFrm As New frmClassRate
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuCatalogue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCatalogue.Click
        Dim objFrm As New frmCatalouge
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuSplitInventory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSplitInventory.Click
        If sCompanyShortCode = "ML" Then
            Dim objFrm As New frmSplitInventoryML
            objFrm.Show()
            objFrm = Nothing
        Else
            Dim objFrm As New frmSplitInventory
            objFrm.Show()
            objFrm = Nothing
        End If
    End Sub

    Private Sub mnuFixCostJobSheet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFixCostJobSheet.Click
        Dim objFrm As New frmFixCostJobSheet
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuDirectPurchase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDirectPurchase.Click
        If sCompanyShortCode = "ML" Then
            Dim objFrm As New frmDirectPurchaseML
            objFrm.Show()
            objFrm = Nothing
        Else
            Dim objFrm As New frmDirectPurchase
            objFrm.Show()
            objFrm = Nothing
        End If

    End Sub

    Private Sub mnuPurchase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPurchase.Click
        If sCompanyShortCode = "ML" Then
            Dim objFrm As New frmPurchaseML
            objFrm.Show()
            objFrm = Nothing
        Else
            Dim objFrm As New frmPurchase
            objFrm.Show()
            objFrm = Nothing
        End If

    End Sub

    Private Sub mnuJobSheet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuJobSheet.Click
        Dim objFrm As New frmJobSheet
        objFrm.Show()
        objFrm = Nothing

    End Sub

    Private Sub mnuBarCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBarCode.Click
        'If ((sCompanyShortCode = "ES") Or (sCompanyShortCode = "HN") Or (sCompanyShortCode = "ML")) Or (sCompanyShortCode = "MJ") Or (sCompanyShortCode = "SSJ") Or (sCompanyShortCode = "RJ") Then
        '    Dim objfrm As New frmBarcode
        '    objfrm.show()
        '    objfrm = Nothing
        'ElseIf sCompanyShortCode = "AS" Then
        '    Dim objfrm As New frmJewelBarcode
        '    objfrm.show()
        '    objfrm = Nothing
        'End If
        'Changed by Shabeer 20111019
        If sCompanyShortCode = "AS" Then
            Dim objfrm As New frmJewelBarcode
            objfrm.Show()
            objfrm = Nothing
        Else
            Dim objfrm As New frmBarcode
            objfrm.Show()
            objfrm = Nothing
        End If
    End Sub

    Private Sub mnuJewellaryBarcode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuJewellaryBarcode.Click
        If sCompanyShortCode = "ES" Then
            Dim objFrm As New frmJewelBarcode
            objFrm.Show()
            objFrm = Nothing
        End If
    End Sub

    Private Sub mnuSpecialBarcode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSpecialBarcode.Click
        If sCompanyShortCode = "ES" Then
            Dim objFrm As New frmBarcodeLabelprinting
            objFrm.Show()
            objFrm = Nothing
        End If
    End Sub

    Private Sub mnuInventorySearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInventorySearch.Click
        If sCompanyShortCode = "ML" Then
            Dim objFrm As New frmInventoryML
            objFrm.Show()
            objFrm = Nothing
        Else
            Dim objFrm As New frmInventory
            objFrm.Show()
            objFrm = Nothing
        End If

    End Sub

    Private Sub mnuInventoryMaintenance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInventoryMaintenance.Click
        If sCompanyShortCode = "ML" Then
            Dim objFrm As New frmInventoryMntML
            objFrm.Show()
            objFrm = Nothing
        Else
            Dim objFrm As New frmInventoryMnt
            objFrm.Show()
            objFrm = Nothing
        End If

    End Sub

    Private Sub mnuPOS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPOS.Click
        '''Dim objFrm As New frmGPOSNew
        '''objFrm.show()
        ''''objFrm = Nothing
        If Trim(sCompanyShortCode) = "HN" Then
            Dim frm As New frmGPOS
            frm.Show()
            frm = Nothing
        Else
            Dim frm As New frmGPOS
            frm.Show()
            frm = Nothing
        End If
    End Sub

    Private Sub mnuInstallment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInstallment.Click
        Dim objFrm As New frmInstallmentPayment
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuSalesReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSalesReturn.Click
        Dim objFrm As New frmSalesReturn
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuTradeInSales_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTradeInSales.Click
        Dim objFrm As New frmTradeInOnly
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuOrderEntry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOrderEntry.Click
        Dim objFrm As New frmInventoryOrderEntry
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuGoldBarPurchase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGoldBarPurchase.Click
        Dim objFrm As New frmGoldBarPurchaseNew
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuGoldBarIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGoldBarIssue.Click
        'Dim objFrm As New frmGoldBarIssue
        If Trim(sCompanyShortCode) = "AJ" Then
            Dim objFrm As New frmGoldBarIssue_AJ
            objFrm.Show()
            objFrm = Nothing
        Else
            Dim objFrm As New frmGoldBarIssueNew
            objFrm.Show()
            objFrm = Nothing
        End If

    End Sub

    Private Sub mnuTradeInGoldIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTradeInGoldIssue.Click
        Dim objFrm As New frmTradeInIssue
        objFrm.Show()
        objFrm = Nothing
    End Sub


    Private Sub mnuMasterReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMasterReports.Click
        Dim objFrm As New frmMasterReports
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuSettlement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSettlement.Click
        Dim objFrm As New frmSettlementAJ
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuInventoryReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInventoryReports.Click
        If sCompanyShortCode = "ML" Then
            Dim objFrm As New frmInventoryReports
            objFrm.Show()
            objFrm.optInventoryMovementBySize.Visible = True
            objFrm = Nothing
        Else
            Dim objFrm As New frmInventoryReports
            objFrm.Show()
            objFrm.optInventoryMovementBySize.Visible = False
            objFrm = Nothing
        End If

    End Sub

    Private Sub mnuPurchaseReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPurchaseReports.Click
        Dim objFrm As New frmPurchaseReports
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuSalesReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSalesReports.Click
        If UCase(sCompanyShortCode) = "ES" And (sUserGroup = "SALES") Then
            Dim objFrm As New frmReport
            objFrm.Show()
            objFrm = Nothing
        Else
            Dim objFrm As New frmSalesReports
            objFrm.Show()
            objFrm = Nothing
        End If
    End Sub

    Private Sub mnuSettlementReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSettlementReports.Click
        Dim objFrm As New frmSettlementReport
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuEODHistoryReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEODReports.Click
        Dim objFrm As New FrmEODHistory
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuManagementReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuManagementReports.Click

        Dim objFrm As New frmManagementReports
        objFrm.Show()
        objFrm = Nothing

    End Sub

    Private Sub mnuGoldbarReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGoldbarReports.Click
        Dim objFrm As New frmGoldBarReports
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuTransactionReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTransactionReports.Click
        Dim objFrm As New frmTransactionReports
        objFrm.Show()
        objFrm = Nothing
    End Sub



    Private Sub mnuDayEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDayEnd.Click
        If Trim(sCompanyShortCode) = "MJ" Then
            Dim objFrm As New frmEOD
            objFrm.Show()
            objFrm = Nothing
        ElseIf Trim(sCompanyShortCode) = "MD" Then
            Dim objFrm As New frmEOD
            objFrm.Show()
            objFrm = Nothing
        Else
            Dim objFrm As New frmDayEndtoHQ
            objFrm.Show()
            objFrm = Nothing
        End If

    End Sub

    Private Sub mnuUserGroup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUserGroup.Click
        Dim objFrm As New frmUserGroup
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuUserMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUserMaster.Click
        Dim objFrm As New frmUserMaster
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuUserPermission_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUserPermission.Click
        Dim objFrm As New frmPermissionMaster
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuChangePrinter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuChangePrinter.Click
        Dim objFrm As New frmChangePrinter
        objFrm.Show()
        objFrm = Nothing
    End Sub




    Private Sub mnuStockAdjustment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim objFrm As New frmStockAdjustment
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuPurchaseReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPurchaseReturn.Click
        Dim objFrm As New frmPurchaseReturn
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuSupplier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSupplier.Click
        Dim objFrm As New frmSupplierMnt
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub MenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSalesman.Click
        Dim objFrm As New frmSalesMan
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuCurrency_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCurrency.Click
        Dim objFrm As New frmCurrency
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuChangePassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuChangePassword.Click
        Dim objFrm As New frmChangePassword
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuTrayMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTrayMaster.Click
        Dim objFrm As New frmTray
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuTrayTransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTrayTransfer.Click
        Dim objFrm As New frmTrayTransfer
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuPaymode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPaymode.Click
        Dim objFrm As New frmPayMode
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuCompany_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCompany.Click
        Dim objFrm As New frmCompany
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuLocation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLocation.Click
        Dim objFrm As New frmLocation
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCustomer.Click
        Dim objFrm As New frmCustomer
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuTerminal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTerminal.Click
        Dim objFrm As New frmTerminal
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuMergeInventory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMergeInventory.Click
        If sCompanyShortCode = "ML" Then
            Dim objFrm As New frmMergeInventoryML
            objFrm.Show()
            objFrm = Nothing
        Else
            Dim objFrm As New frmMergeInventory
            objFrm.Show()
            objFrm = Nothing
        End If

    End Sub


    Private Sub mnuGSTReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGSTReport.Click
        Dim objFrm As New frmGSTReports
        objFrm.Show()
        objFrm = Nothing
    End Sub



    Private Sub mnuDailySales_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDailySales.Click
        Dim objFrm As New frmDailySalesReport
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuStockReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuStockReports.Click
        Dim objFrm As New frmStockReports
        objFrm.Show()
        objFrm = Nothing

    End Sub



    Private Sub mnuMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMember.Click
        Dim objFrm As New frmMember
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuMemberType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMemberType.Click
        Dim objFrm As New frmMemberType
        objFrm.Show()
        objFrm = Nothing
    End Sub


    Private Sub mnuAnalysisReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAnalysisReports.Click
        Dim objFrm As New frmSalesAnalysisReport
        objFrm.Show()
        objFrm = Nothing
    End Sub


    Private Sub mnuNricType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNricType.Click
        Dim objFrm As New frmNricType
        objFrm.Show()
        objFrm = Nothing
    End Sub

    Private Sub mnuMemberReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMemberReports.Click
        Dim objFrm As New frmMemberReport
        objFrm.Show()
        objFrm = Nothing
    End Sub


    Private Sub mnuOrderPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOrderPayment.Click
        Dim objFrm As New frmOrderPayment
        Try
            objFrm.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            objFrm = Nothing
        End Try
    End Sub

    Private Sub mnuOrderPayment1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOrderPayment1.Click
        Dim objFrm As New frmOrderPayment
        Try
            objFrm.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            objFrm = Nothing
        End Try

    End Sub

    Private Sub MenuItem60_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem60.Click
        Dim frm As New frmStockTake
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub MenuItem66_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem66.Click
        Dim frm As New frmStockTakeReport
        frm.Show()
        frm = Nothing
    End Sub




    Private Sub mnuPurchaseReturnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPurchaseReturnPost.Click
        Dim frm As New frmPurchaseReturnReport
        frm.Show()
        frm = Nothing
    End Sub


    Private Sub mnuAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAbout.Click
        Dim frm As New frmAbout
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub MenuItem68_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem68.Click
        Dim frm As New frmMisc
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub MenuItem70_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Manageforotheres.Click
        Dim frm As New frmManagementForOthers
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuWholeSalePOS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuWholeSalePOS.Click
        Dim frm As New frmGPOSWholeSale
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuTradeInWholeSales_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTradeInWholeSales.Click
        Dim frm As New frmTradeInWholeSales
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuCountry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCountry.Click
        Dim frm As New frmCountry
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuRefiner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRefiner.Click
        Dim frm As New frmRefiner
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuGoldBarFix_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGoldBarFix.Click
        Dim frm As New frmGoldBarFix
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuRFIDStocktake_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRFIDStocktake.Click
        Dim frm As New frmRFIDStockTakeReports
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuSize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSize.Click
        Dim frm As New frmSize
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub btnMainBarCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMainBarCode.Click
        Dim frm As New frmBarcode
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub btnMainClass_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMainClass.Click
        Dim frm As New frmClass
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub btnMainJobsheet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMainJobsheet.Click
        Dim frm As New frmJobSheet
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub btnMainPurchase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMainPurchase.Click
        Dim frm As New frmPurchase
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub btnMainExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMainExit.Click
        Me.Dispose()
    End Sub

    Private Sub btnMainCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMainCategory.Click
        Dim frm As New frmCategory
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub btnMainCatalogue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMainCatalogue.Click
        Dim frm As New frmCatalouge
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub btnMainSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMainSearch.Click
        Dim frm As New frmInventory
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub btnMainTransferOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMainTransferOut.Click
        Dim frm As New frmTransferOut
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub btnMainTransferIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMainTransferIn.Click
        Dim frm As New frmTransferIn
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuTransferOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTransferOut.Click
        Dim frm As New frmTransferOut
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuTransferIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTransferIn.Click
        Dim frm As New frmTransferIn
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuTransferStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTransferStatus.Click
        Dim frm As New frmTransferList
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuInventoryMovement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInventoryMovement.Click
        Dim frm As New frmInventoryMovement
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub btnMainOutletPos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMainOutletPos.Click
        Dim frm As New frmGPOS
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub btnMainOutletInventory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMainOutletInventory.Click
        Dim frm As New frmInventoryAJ
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub btnMainOutletSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMainOutletSearch.Click
        Dim frm As New frmInventory
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub btnMainOutletTradeIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMainOutletPaymentEntry.Click
        Dim frm As New frmPaymentEntry
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub btnMainOutletInstallment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMainOutletInstallment.Click
        Dim frm As New frmInstallmentPayment
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub btnMainOutletOrderEntry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMainOutletOrderEntry.Click
        Dim frm As New frmInstallment_AJ
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub btnMainOutletTransferIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMainOutletTransferIn.Click
        Dim frm As New frmTransferIn
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub btnMainOutletTransferOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMainOutletTransferOut.Click
        Dim frm As New frmTransferOut
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub btnMainOutletSettlement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMainOutletSettlement.Click
        Dim frm As New frmSettlementAJ
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub btnMainOutletUpdateHQ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMainOutletUpdateHQ.Click
        Dim frm As New frmUpdatetoHQ
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub btnMainOutletDayEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMainOutletDayEnd.Click
        Dim frm As New frmDayEndtoHQ
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub btnMainOutletExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMainOutletExit.Click
        Me.Dispose()
    End Sub


    Private Sub mnuInstallmentEntry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInstallmentEntry.Click
        Dim frm As New frmInstallment_AJ
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub btnMainInventory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMainInventory.Click
        Dim frm As New frmInventoryAJ
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuTransferReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTransferReports.Click
        Dim frm As New frmTransferReports
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuOrderInventory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOrderInventory.Click
        Dim frm As New frmOrderInventory
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuRepairEntry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRepairEntry.Click
        Dim frm As New frmRepairEntry
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuRepairPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRepairPayment.Click
        Dim frm As New frmRepairPayment
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub btnMainCreditSales_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMainCreditSales.Click
        Dim frm As New frmPaymentEntry
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuPaymentEntries_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPaymentEntries.Click
        Dim frm As New frmPaymentEntry
        frm.Show()
        frm = Nothing
    End Sub


    Private Sub mnuPaymodeModification_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPaymodeModification.Click
        Dim frm As New frmPaymodeModification
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuPayOutMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPayOutMaster.Click
        Dim frm As New frmPayOutMaster
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuPayOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPayOut.Click
        Dim frm As New frmPayOut
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub dgvOrder_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvOrder.CellDoubleClick
        Dim objOrder As New frmInstallment_AJ

        objOrder.txtInvoiceNo.Text = fnGetdgvText(dgvOrder, dgvOrder.CurrentRow.Index, 0).Trim & ""
        sMainOrderNo = fnGetdgvText(dgvOrder, dgvOrder.CurrentRow.Index, 0).Trim & ""
        objOrder.optInstallment.Checked = False
        objOrder.optOrder.Checked = True
        'objOrder.txtInvoiceNo_LostFocus(sender, e)
        objOrder.sMainFrom = "MAINORDER"
        objOrder.ShowDialog()
    End Sub


    Private Sub dgvInstallment_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvInstallment.CellDoubleClick
        Dim objOrder As New frmInstallment_AJ

        objOrder.txtInvoiceNo.Text = fnGetdgvText(dgvInstallment, dgvInstallment.CurrentRow.Index, 0).Trim & ""
        sMainOrderNo = fnGetdgvText(dgvInstallment, dgvInstallment.CurrentRow.Index, 0).Trim & ""
        objOrder.optInstallment.Checked = True
        objOrder.optOrder.Checked = False
        'objOrder.txtInvoiceNo_LostFocus(sender, e)
        objOrder.sMainFrom = "MAININS"
        objOrder.ShowDialog()
    End Sub

    Private Sub mnuOutletCustomers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOutletCustomers.Click
        Dim frm As New frmOutletCustomers
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuCustomerReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCustomerReports.Click
        Dim frm As New frmCustomerReports
        frm.Show()
        frm = Nothing
    End Sub


    Private Sub dgvunfixJobsheet_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvunfixJobsheet.CellDoubleClick
        Dim objFix As New frmFixCostJobSheet

        objFix.txtJobSheetNo.Text = fnGetdgvText(dgvunfixJobsheet, dgvunfixJobsheet.CurrentRow.Index, 0).Trim & ""
        'objOrder.optInstallment.Checked = False
        'objOrder.optOrder.Checked = True
        objFix.txtJobSheetNo_LostFocus(sender, e)
        objFix.ShowDialog()
    End Sub

    Private Sub mnuReOrderLevelMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReOrderLevelMaster.Click
        Dim frm As New frmReOrderLevelMaster
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuSalesOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSalesOrder.Click
        Dim frm As New frmSalesOrder
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuOnlineTransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOnlineTransfer.Click
        Dim frm As New frmOnlineTransfer
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuCustomerPointAdjustment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCustomerPointAdjustment.Click
        Dim frm As New frmCustomerPointsAdjustment
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuCustomerTypeChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCustomerTypeChange.Click
        Dim frm As New frmCustomerTypeChange
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuPaymentEntryAksayaTritiya_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPaymentEntryAksayaTritiya.Click
        Dim frm As New FrmPaymentEntryAksayaTritiya
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuCatalogueSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCatalogueSearch.Click
        Dim frm As New frmInventorySearch
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub dgvOrder_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgvOrder.CellFormatting
        Dim i As Integer
        For i = 0 To Me.dgvOrder.Rows.Count - 1
            If Me.dgvOrder.Rows(i).Cells(6).Value = "A" Then
                Me.dgvOrder.Rows(i).DefaultCellStyle.BackColor = Color.Blue
            ElseIf Me.dgvOrder.Rows(i).Cells(6).Value = "B" Then
                Me.dgvOrder.Rows(i).DefaultCellStyle.BackColor = Color.Green
            ElseIf Me.dgvOrder.Rows(i).Cells(6).Value = "D" Then
                Me.dgvOrder.Rows(i).DefaultCellStyle.BackColor = Color.Yellow
            End If
        Next
    End Sub

    'Private Sub Timer1_Tick(sender As Object, e As System.EventArgs) Handles Timer1.Tick
    '    'If Trim(sUserGroup) = "ADMIN" Then
    '    '    Me.Close()
    '    'End If
    '    'Dim checktime As DateTime = Now()
    '    'Dim span As TimeSpan = checktime.Subtract(StartTime)
    '    'If span.Minutes >= 1 Then
    '    '    If Trim(sUserGroup) = "ADMIN" Then
    '    '        Me.Close()
    '    '    End If
    '    'End If

    'End Sub

    Private Sub frmMain_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        StartTime = Now()
    End Sub

    Private Sub dgvInstallment_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgvInstallment.CellFormatting
        Dim i As Integer
        For i = 0 To Me.dgvInstallment.Rows.Count - 1
            If Me.dgvInstallment.Rows(i).Cells(7).Value = "A" Then
                Me.dgvInstallment.Rows(i).DefaultCellStyle.BackColor = Color.Blue
            End If
        Next
    End Sub

    Private Sub mnueTRSTicket_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnueTRSTicket.Click
        If Trim(Objuser.CompanyName) = "ABIRAAME JEWELLERS" Then
            Dim frm As New frmDiscountStatement
            frm.Show()
            frm = Nothing
        Else
            Dim frm As New frmeTRSStatement
            frm.Show()
            frm = Nothing
        End If

    End Sub

    Private Sub mnuSupplierMC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSupplierMC.Click
        Dim frm As New frmSupplierMCPerc
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuPurchaseTally_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPurchaseTally.Click
        Dim frm As New frmPurchaseTally
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuTradeInMerge_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTradeInMerge.Click
        Dim frm As New frmTradeInMerge
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub conMenu_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles conMenu.Popup

    End Sub

    Private Sub mnuSalesHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSalesHistory.Click
        Dim frm As New frmSalesHistory
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuSupplierPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSupplierPayment.Click
        Dim frm As New frmSupplierPayment
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuCreditSalesPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCreditSalesPayment.Click
        Dim frm As New frmCreditSalespayment
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuTradeInSplit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTradeInSplit.Click
        Dim frm As New frmTradeInSplit
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuCustomerDeposit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCustomerDeposit.Click
        Dim frm As New frmCustomerDeposit
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuTradeInTransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTradeInTransfer.Click
        Dim frm As New frmTradeInTransfer
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuSTockTradeInTransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSTockTradeInTransfer.Click
        Dim frm As New frmStockTradeInTransfer
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuTradeInReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTradeInReports.Click
        Dim frm As New frmTradeInReports
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnueTRSTicketVoid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnueTRSTicketVoid.Click
        Dim frm As New frmeTRSVoidStatement
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuSupplierPaymentAmendment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSupplierPaymentAmendment.Click
        Dim frm As New frmSupplierPaymentAmendment
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub btnMainHQPos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMainHQPos.Click
        Dim frm As New frmGPOS
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub grpMainOutlet_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grpMainOutlet.Enter

    End Sub

    Private Sub mnuPurchaseTake_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPurchaseTake.Click
        Dim frm As New frmPurchaseTake
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuPurchaseTakeReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPurchaseTakeReports.Click
        Dim frm As New frmRFIDStockTakeReports
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuDeposit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDeposit.Click
        Dim frm As New frmDeposit
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuDailySalesEntry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDailySalesEntry.Click
        Dim frm As New frmDailySales
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuPurchaseEntry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPurchaseEntry.Click
        Dim frm As New frmPurchaseEntry
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub mnuSchemeMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSchemeMaster.Click
        Dim frm As New frmSchemeMaster
        frm.Show()
        frm = Nothing
    End Sub

    Private Sub txtInventoryCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInventoryCode.Leave
        Try
            If txtInventoryCode.Text.Trim <> "" Then
                subGetInventory()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub


    Private Sub subGetInventory()
        Dim DsInv As DataSet
        Dim DsTrade As DataSet
        Dim DsAdditional As DataSet
        Dim sender As System.Object
        Dim e As System.EventArgs
        Dim objInv As New clsInventory
        Dim objTrade As New clsTradeIN
        Dim Ds As DataSet
        Dim objTray As New clsTray

        Try
            DsInv = objInv.GetInventoryDetail(txtInventoryCode.Text.Trim)
            If DsInv.Tables("tblInventory").Rows.Count > 0 Then
                If DsInv.Tables("tblInventory").Rows(0)("QtyOnHand") & "" >= 0 Then
                    txtDescription.Text = DsInv.Tables("tblInventory").Rows(0)("Description") & ""
                    txtWeight.Text = DsInv.Tables("tblInventory").Rows(0)("Weight") & ""
                    sClassCode = Trim(DsInv.Tables(0).Rows(0)("ClassCode") & "")
                    'load class details to get the rate and the salestype.
                    subGetClass()
                    If Trim(DsInv.Tables(0).Rows(0)("SMCType") & "") = "P" Then
                        'txtSMC.Text = Val(DsInv.Tables("tblInventory").Rows(0)("SMc")) / DsInv.Tables("tblInventory").Rows(0)("PCS") & ""
                        If Val(DsInv.Tables(0).Rows(0)("Pcs") & "") > 0 Then
                            txtSMC.Text = Format(Val(DsInv.Tables("tblInventory").Rows(0)("sMc") & "") / Val(DsInv.Tables(0).Rows(0)("Pcs") & ""), "0.00")

                        Else
                            txtSMC.Text = DsInv.Tables("tblInventory").Rows(0)("SMc") & ""

                        End If
                        'txtSMC.Text = Format(Val(DsInv.Tables("tblInventory").Rows(0)("sMc") & "") / Val(DsInv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                        'sPMC = Format(Val(DsInv.Tables("tblInventory").Rows(0)("PMC") & "") / Val(DsInv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                    Else
                        txtSMC.Text = DsInv.Tables("tblInventory").Rows(0)("SMc") & ""

                    End If
                    txtGST.Text = Format(((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + Val(txtSMC.Text)) * 0.09, "0.00")
                    txtTotalValue.Text = Format(((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + Val(txtSMC.Text)) + ((Val(txtWeight.Text) * Val(txtCostPerGram.Text)) + Val(txtSMC.Text)) * 0.09, "0.00")

                    ' sPMC = DsInv.Tables("tblInventory").Rows(0)("PMC") & ""


                    ''HAJ Get size and supplier code

                Else
                    MessageBox.Show("No Stock for this InventoryCode", "GoldPOS")

                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subGetClass()
        Dim dsCls As New DataSet
        Dim objClass As New ClsClass
        Try
            dsCls = objClass.GetClassDetail(sClassCode)
            If dsCls.Tables(0).Rows.Count > 0 Then
                txtCostPerGram.Text = Val(dsCls.Tables(0).Rows(0)("Rate") & "")
            Else
                MessageBox.Show("Class Not defined for this item .... ")
                Exit Sub
            End If
            dsCls = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        txtInventoryCode.Text = ""
        txtDescription.Text = ""
        txtWeight.Text = ""
        txtCostPerGram.Text = ""
        txtSMC.Text = ""
        txtGST.Text = ""
        txtTotalValue.Text = ""
        txtInventoryCode.Focus()
    End Sub

    Private Sub txtInventoryCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtGST.KeyDown
        txtInventoryCode_LostFocus(sender, e)
    End Sub

    Private Sub mnuGoldSchemeEntries_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGoldSchemeEntries.Click
        Dim frm As New frmSchemeTransactionDetail
        frm.Show()
        frm = Nothing
    End Sub

  
    Private Sub btnMainOutletBarCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMainOutletBarCode.Click
        Dim frm As New frmBarcode
        frm.Show()
        frm = Nothing
    End Sub
End Class
