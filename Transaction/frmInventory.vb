Imports System
Imports System.IO
Public Class frmInventory
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
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnCatalogueSearch As System.Windows.Forms.Button
    Friend WithEvents txtCatalogueCode As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnSupplierSearch As System.Windows.Forms.Button
    Friend WithEvents txtSupplier As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents optSold As System.Windows.Forms.RadioButton
    Friend WithEvents optStock As System.Windows.Forms.RadioButton
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtInvDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtinvCode As System.Windows.Forms.TextBox
    Friend WithEvents txtCategory As System.Windows.Forms.TextBox
    Friend WithEvents txtCatalogue As System.Windows.Forms.TextBox
    Friend WithEvents txtClass As System.Windows.Forms.TextBox
    Friend WithEvents txtLocation As System.Windows.Forms.TextBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtPMC As System.Windows.Forms.TextBox
    Friend WithEvents txtSMC As System.Windows.Forms.TextBox
    Friend WithEvents txtStoneWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtSalesDate As System.Windows.Forms.TextBox
    Friend WithEvents txtSalesBillNo As System.Windows.Forms.TextBox
    Friend WithEvents txtSalesSMC As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtSalesPCS As System.Windows.Forms.TextBox
    Friend WithEvents txtSalesCostPerGram As System.Windows.Forms.TextBox
    Friend WithEvents txtSalesMan As System.Windows.Forms.TextBox
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents txtSalesRetToal As System.Windows.Forms.TextBox
    Friend WithEvents txtSalesRetPCS As System.Windows.Forms.TextBox
    Friend WithEvents txtSalesRetCostperGram As System.Windows.Forms.TextBox
    Friend WithEvents txtSalesRetSMC As System.Windows.Forms.TextBox
    Friend WithEvents txtSalesRetDate As System.Windows.Forms.TextBox
    Friend WithEvents txtSalesRetRefNo As System.Windows.Forms.TextBox
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents txtJobSheetDate As System.Windows.Forms.TextBox
    Friend WithEvents txtJobSheetNo As System.Windows.Forms.TextBox
    Friend WithEvents txtPurchaseSupplier As System.Windows.Forms.TextBox
    Friend WithEvents txtPurchasePMC As System.Windows.Forms.TextBox
    Friend WithEvents txtPurchaseDate As System.Windows.Forms.TextBox
    Friend WithEvents txtPurchaseNo As System.Windows.Forms.TextBox
    Friend WithEvents txtPurchaseRetTotal As System.Windows.Forms.TextBox
    Friend WithEvents txtPurchaseRetPCS As System.Windows.Forms.TextBox
    Friend WithEvents txtPurchaseRetCostperGram As System.Windows.Forms.TextBox
    Friend WithEvents txtPurchaseRetPMC As System.Windows.Forms.TextBox
    Friend WithEvents txtPurchaseRetDate As System.Windows.Forms.TextBox
    Friend WithEvents txtPurchaseRetNo As System.Windows.Forms.TextBox
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents txtTotalSales As System.Windows.Forms.TextBox
    Friend WithEvents txtSearchWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtDiamondDescription As System.Windows.Forms.TextBox
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents txtTotalCost As System.Windows.Forms.TextBox
    Friend WithEvents txtQtyOnHand As System.Windows.Forms.TextBox
    Friend WithEvents txtCostPerGram As System.Windows.Forms.TextBox
    Friend WithEvents txtGoldLoss As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents txtSizeCode As System.Windows.Forms.TextBox
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents grdInventory As System.Windows.Forms.DataGridView
    Friend WithEvents cInvCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents txtCreatedBy As System.Windows.Forms.TextBox
    Friend WithEvents txtGrossWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents picImage As System.Windows.Forms.PictureBox
    Friend WithEvents pnlSplitMerge As System.Windows.Forms.Panel
    Friend WithEvents chkSplitMerge As System.Windows.Forms.CheckBox
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents txtMWeightN As System.Windows.Forms.TextBox
    Friend WithEvents txtMInventoryN As System.Windows.Forms.TextBox
    Friend WithEvents txtMWeight2 As System.Windows.Forms.TextBox
    Friend WithEvents txtMInventoryCode2 As System.Windows.Forms.TextBox
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents txtMWeight1 As System.Windows.Forms.TextBox
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents txtMInventoryCode1 As System.Windows.Forms.TextBox
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents TextBox19 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox20 As System.Windows.Forms.TextBox
    Friend WithEvents txtOriginalWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents txtWeight2 As System.Windows.Forms.TextBox
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents txtWeight1 As System.Windows.Forms.TextBox
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents txtDestInventoryCode As System.Windows.Forms.TextBox
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents TextBox25 As System.Windows.Forms.TextBox
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents grpAddlDescription As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents txtNewSellingPrice4 As System.Windows.Forms.TextBox
    Friend WithEvents txtNewSellingPrice3 As System.Windows.Forms.TextBox
    Friend WithEvents txtNewSellingPrice2 As System.Windows.Forms.TextBox
    Friend WithEvents txtNewSellingPrice1 As System.Windows.Forms.TextBox
    Friend WithEvents txtNewCost4 As System.Windows.Forms.TextBox
    Friend WithEvents txtNewCost3 As System.Windows.Forms.TextBox
    Friend WithEvents txtNewCost2 As System.Windows.Forms.TextBox
    Friend WithEvents txtNewCost1 As System.Windows.Forms.TextBox
    Friend WithEvents txtNewDesc4 As System.Windows.Forms.TextBox
    Friend WithEvents txtNewDesc3 As System.Windows.Forms.TextBox
    Friend WithEvents txtNewDesc2 As System.Windows.Forms.TextBox
    Friend WithEvents txtNewDesc1 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents txtSellingPrice4 As System.Windows.Forms.TextBox
    Friend WithEvents txtSellingPrice3 As System.Windows.Forms.TextBox
    Friend WithEvents txtSellingPrice2 As System.Windows.Forms.TextBox
    Friend WithEvents txtSellingPrice1 As System.Windows.Forms.TextBox
    Friend WithEvents txtCost4 As System.Windows.Forms.TextBox
    Friend WithEvents txtCost3 As System.Windows.Forms.TextBox
    Friend WithEvents txtCost2 As System.Windows.Forms.TextBox
    Friend WithEvents txtCost1 As System.Windows.Forms.TextBox
    Friend WithEvents txtDesc4 As System.Windows.Forms.TextBox
    Friend WithEvents txtDesc3 As System.Windows.Forms.TextBox
    Friend WithEvents txtDesc2 As System.Windows.Forms.TextBox
    Friend WithEvents txtDesc1 As System.Windows.Forms.TextBox
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents cmdAddlDescOk As System.Windows.Forms.Button
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents chkAdditionalDesc As System.Windows.Forms.CheckBox
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInventory))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.txtSearchWeight = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.optStock = New System.Windows.Forms.RadioButton()
        Me.optSold = New System.Windows.Forms.RadioButton()
        Me.btnSupplierSearch = New System.Windows.Forms.Button()
        Me.txtSupplier = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnCatalogueSearch = New System.Windows.Forms.Button()
        Me.txtCatalogueCode = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.picImage = New System.Windows.Forms.PictureBox()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtDiamondDescription = New System.Windows.Forms.TextBox()
        Me.txtLocation = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtClass = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtCatalogue = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtCategory = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtInvDescription = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtinvCode = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.txtGrossWeight = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.txtSMC = New System.Windows.Forms.TextBox()
        Me.txtPMC = New System.Windows.Forms.TextBox()
        Me.txtStoneWeight = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtWeight = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.txtTotalSales = New System.Windows.Forms.TextBox()
        Me.txtSalesPCS = New System.Windows.Forms.TextBox()
        Me.txtSalesCostPerGram = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtSalesSMC = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtSalesDate = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtSalesBillNo = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtSalesMan = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.txtSalesRetToal = New System.Windows.Forms.TextBox()
        Me.txtSalesRetPCS = New System.Windows.Forms.TextBox()
        Me.txtSalesRetCostperGram = New System.Windows.Forms.TextBox()
        Me.txtSalesRetSMC = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtSalesRetDate = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtSalesRetRefNo = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.txtJobSheetDate = New System.Windows.Forms.TextBox()
        Me.txtJobSheetNo = New System.Windows.Forms.TextBox()
        Me.txtPurchaseSupplier = New System.Windows.Forms.TextBox()
        Me.txtPurchasePMC = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtPurchaseDate = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.txtPurchaseNo = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.txtPurchaseRetTotal = New System.Windows.Forms.TextBox()
        Me.txtPurchaseRetPCS = New System.Windows.Forms.TextBox()
        Me.txtPurchaseRetCostperGram = New System.Windows.Forms.TextBox()
        Me.txtPurchaseRetPMC = New System.Windows.Forms.TextBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.txtPurchaseRetDate = New System.Windows.Forms.TextBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.txtPurchaseRetNo = New System.Windows.Forms.TextBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.lblMsg = New System.Windows.Forms.Label()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.txtSizeCode = New System.Windows.Forms.TextBox()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.txtTotalCost = New System.Windows.Forms.TextBox()
        Me.txtQtyOnHand = New System.Windows.Forms.TextBox()
        Me.txtCostPerGram = New System.Windows.Forms.TextBox()
        Me.txtGoldLoss = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.grdInventory = New System.Windows.Forms.DataGridView()
        Me.cInvCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cDescription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtCreatedBy = New System.Windows.Forms.TextBox()
        Me.pnlSplitMerge = New System.Windows.Forms.Panel()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.txtMWeightN = New System.Windows.Forms.TextBox()
        Me.txtMInventoryN = New System.Windows.Forms.TextBox()
        Me.txtMWeight2 = New System.Windows.Forms.TextBox()
        Me.txtMInventoryCode2 = New System.Windows.Forms.TextBox()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.txtMWeight1 = New System.Windows.Forms.TextBox()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.txtMInventoryCode1 = New System.Windows.Forms.TextBox()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.TextBox19 = New System.Windows.Forms.TextBox()
        Me.TextBox20 = New System.Windows.Forms.TextBox()
        Me.txtOriginalWeight = New System.Windows.Forms.TextBox()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.txtWeight2 = New System.Windows.Forms.TextBox()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.txtWeight1 = New System.Windows.Forms.TextBox()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.txtDestInventoryCode = New System.Windows.Forms.TextBox()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.TextBox25 = New System.Windows.Forms.TextBox()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.chkSplitMerge = New System.Windows.Forms.CheckBox()
        Me.grpAddlDescription = New System.Windows.Forms.GroupBox()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.txtNewSellingPrice4 = New System.Windows.Forms.TextBox()
        Me.txtNewSellingPrice3 = New System.Windows.Forms.TextBox()
        Me.txtNewSellingPrice2 = New System.Windows.Forms.TextBox()
        Me.txtNewSellingPrice1 = New System.Windows.Forms.TextBox()
        Me.txtNewCost4 = New System.Windows.Forms.TextBox()
        Me.txtNewCost3 = New System.Windows.Forms.TextBox()
        Me.txtNewCost2 = New System.Windows.Forms.TextBox()
        Me.txtNewCost1 = New System.Windows.Forms.TextBox()
        Me.txtNewDesc4 = New System.Windows.Forms.TextBox()
        Me.txtNewDesc3 = New System.Windows.Forms.TextBox()
        Me.txtNewDesc2 = New System.Windows.Forms.TextBox()
        Me.txtNewDesc1 = New System.Windows.Forms.TextBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.txtSellingPrice4 = New System.Windows.Forms.TextBox()
        Me.txtSellingPrice3 = New System.Windows.Forms.TextBox()
        Me.txtSellingPrice2 = New System.Windows.Forms.TextBox()
        Me.txtSellingPrice1 = New System.Windows.Forms.TextBox()
        Me.txtCost4 = New System.Windows.Forms.TextBox()
        Me.txtCost3 = New System.Windows.Forms.TextBox()
        Me.txtCost2 = New System.Windows.Forms.TextBox()
        Me.txtCost1 = New System.Windows.Forms.TextBox()
        Me.txtDesc4 = New System.Windows.Forms.TextBox()
        Me.txtDesc3 = New System.Windows.Forms.TextBox()
        Me.txtDesc2 = New System.Windows.Forms.TextBox()
        Me.txtDesc1 = New System.Windows.Forms.TextBox()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.cmdAddlDescOk = New System.Windows.Forms.Button()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.chkAdditionalDesc = New System.Windows.Forms.CheckBox()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel10.SuspendLayout()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSplitMerge.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.Panel14.SuspendLayout()
        Me.grpAddlDescription.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(859, 592)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(112, 23)
        Me.btnClose.TabIndex = 50
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.btnRefresh)
        Me.Panel1.Controls.Add(Me.txtSearchWeight)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.btnSupplierSearch)
        Me.Panel1.Controls.Add(Me.txtSupplier)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.btnCatalogueSearch)
        Me.Panel1.Controls.Add(Me.txtCatalogueCode)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtDescription)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Location = New System.Drawing.Point(304, 8)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(664, 72)
        Me.Panel1.TabIndex = 14
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnRefresh.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.Location = New System.Drawing.Point(418, 40)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(124, 20)
        Me.btnRefresh.TabIndex = 7
        Me.btnRefresh.Text = "Search(F5)"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'txtSearchWeight
        '
        Me.txtSearchWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearchWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchWeight.Location = New System.Drawing.Point(472, 8)
        Me.txtSearchWeight.MaxLength = 50
        Me.txtSearchWeight.Name = "txtSearchWeight"
        Me.txtSearchWeight.Size = New System.Drawing.Size(57, 20)
        Me.txtSearchWeight.TabIndex = 1
        Me.txtSearchWeight.Tag = "NE;"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(413, 12)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 16)
        Me.Label4.TabIndex = 40
        Me.Label4.Text = "Weight"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.optStock)
        Me.Panel2.Controls.Add(Me.optSold)
        Me.Panel2.Location = New System.Drawing.Point(532, -2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(125, 40)
        Me.Panel2.TabIndex = 38
        '
        'optStock
        '
        Me.optStock.Checked = True
        Me.optStock.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optStock.Location = New System.Drawing.Point(8, 8)
        Me.optStock.Name = "optStock"
        Me.optStock.Size = New System.Drawing.Size(60, 24)
        Me.optStock.TabIndex = 2
        Me.optStock.TabStop = True
        Me.optStock.Text = "Stock"
        '
        'optSold
        '
        Me.optSold.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSold.Location = New System.Drawing.Point(72, 8)
        Me.optSold.Name = "optSold"
        Me.optSold.Size = New System.Drawing.Size(58, 24)
        Me.optSold.TabIndex = 3
        Me.optSold.Text = "Sold"
        '
        'btnSupplierSearch
        '
        Me.btnSupplierSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSupplierSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSupplierSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSupplierSearch.Location = New System.Drawing.Point(382, 40)
        Me.btnSupplierSearch.Name = "btnSupplierSearch"
        Me.btnSupplierSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnSupplierSearch.TabIndex = 6
        Me.btnSupplierSearch.Text = "..."
        Me.btnSupplierSearch.UseVisualStyleBackColor = False
        '
        'txtSupplier
        '
        Me.txtSupplier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplier.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplier.Location = New System.Drawing.Point(284, 40)
        Me.txtSupplier.MaxLength = 20
        Me.txtSupplier.Name = "txtSupplier"
        Me.txtSupplier.Size = New System.Drawing.Size(98, 20)
        Me.txtSupplier.TabIndex = 5
        Me.txtSupplier.Tag = "NE;"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(230, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 16)
        Me.Label2.TabIndex = 35
        Me.Label2.Text = "Supplier"
        '
        'btnCatalogueSearch
        '
        Me.btnCatalogueSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCatalogueSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCatalogueSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCatalogueSearch.Location = New System.Drawing.Point(201, 40)
        Me.btnCatalogueSearch.Name = "btnCatalogueSearch"
        Me.btnCatalogueSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnCatalogueSearch.TabIndex = 4
        Me.btnCatalogueSearch.Text = "..."
        Me.btnCatalogueSearch.UseVisualStyleBackColor = False
        '
        'txtCatalogueCode
        '
        Me.txtCatalogueCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCatalogueCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCatalogueCode.Location = New System.Drawing.Point(103, 40)
        Me.txtCatalogueCode.MaxLength = 20
        Me.txtCatalogueCode.Name = "txtCatalogueCode"
        Me.txtCatalogueCode.Size = New System.Drawing.Size(98, 20)
        Me.txtCatalogueCode.TabIndex = 3
        Me.txtCatalogueCode.Tag = "NE;"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 43)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 16)
        Me.Label1.TabIndex = 32
        Me.Label1.Text = "Catalogue Code"
        '
        'txtDescription
        '
        Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.Location = New System.Drawing.Point(103, 8)
        Me.txtDescription.MaxLength = 50
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(305, 20)
        Me.txtDescription.TabIndex = 0
        Me.txtDescription.Tag = "NE;"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 16)
        Me.Label3.TabIndex = 31
        Me.Label3.Text = "Description"
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.picImage)
        Me.Panel3.Controls.Add(Me.Label47)
        Me.Panel3.Controls.Add(Me.Label20)
        Me.Panel3.Controls.Add(Me.txtRemarks)
        Me.Panel3.Controls.Add(Me.Label18)
        Me.Panel3.Controls.Add(Me.txtDiamondDescription)
        Me.Panel3.Controls.Add(Me.txtLocation)
        Me.Panel3.Controls.Add(Me.Label10)
        Me.Panel3.Controls.Add(Me.txtClass)
        Me.Panel3.Controls.Add(Me.Label9)
        Me.Panel3.Controls.Add(Me.txtCatalogue)
        Me.Panel3.Controls.Add(Me.Label8)
        Me.Panel3.Controls.Add(Me.txtCategory)
        Me.Panel3.Controls.Add(Me.Label7)
        Me.Panel3.Controls.Add(Me.txtInvDescription)
        Me.Panel3.Controls.Add(Me.Label6)
        Me.Panel3.Controls.Add(Me.txtinvCode)
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel3.Location = New System.Drawing.Point(376, 88)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(592, 176)
        Me.Panel3.TabIndex = 16
        '
        'picImage
        '
        Me.picImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picImage.Location = New System.Drawing.Point(405, 3)
        Me.picImage.Name = "picImage"
        Me.picImage.Size = New System.Drawing.Size(180, 122)
        Me.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picImage.TabIndex = 771
        Me.picImage.TabStop = False
        '
        'Label47
        '
        Me.Label47.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.Location = New System.Drawing.Point(269, 128)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(74, 16)
        Me.Label47.TabIndex = 88
        Me.Label47.Text = "Created By"
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(269, 150)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(56, 16)
        Me.Label20.TabIndex = 69
        Me.Label20.Text = "Remarks"
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(346, 150)
        Me.txtRemarks.MaxLength = 50
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(238, 20)
        Me.txtRemarks.TabIndex = 16
        Me.txtRemarks.Tag = "NE;"
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(5, 56)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(111, 16)
        Me.Label18.TabIndex = 67
        Me.Label18.Text = "Diamond Description"
        '
        'txtDiamondDescription
        '
        Me.txtDiamondDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDiamondDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiamondDescription.Location = New System.Drawing.Point(120, 54)
        Me.txtDiamondDescription.MaxLength = 50
        Me.txtDiamondDescription.Name = "txtDiamondDescription"
        Me.txtDiamondDescription.Size = New System.Drawing.Size(281, 20)
        Me.txtDiamondDescription.TabIndex = 11
        Me.txtDiamondDescription.Tag = "NE;"
        '
        'txtLocation
        '
        Me.txtLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.Location = New System.Drawing.Point(120, 150)
        Me.txtLocation.MaxLength = 50
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(136, 20)
        Me.txtLocation.TabIndex = 15
        Me.txtLocation.Tag = "NE;"
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(9, 150)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(53, 16)
        Me.Label10.TabIndex = 56
        Me.Label10.Text = "Location"
        '
        'txtClass
        '
        Me.txtClass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtClass.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClass.Location = New System.Drawing.Point(120, 126)
        Me.txtClass.MaxLength = 50
        Me.txtClass.Name = "txtClass"
        Me.txtClass.Size = New System.Drawing.Size(136, 20)
        Me.txtClass.TabIndex = 14
        Me.txtClass.Tag = "NE;"
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(5, 128)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(77, 16)
        Me.Label9.TabIndex = 54
        Me.Label9.Text = "Class"
        '
        'txtCatalogue
        '
        Me.txtCatalogue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCatalogue.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCatalogue.Location = New System.Drawing.Point(120, 102)
        Me.txtCatalogue.MaxLength = 50
        Me.txtCatalogue.Name = "txtCatalogue"
        Me.txtCatalogue.Size = New System.Drawing.Size(136, 20)
        Me.txtCatalogue.TabIndex = 13
        Me.txtCatalogue.Tag = "NE;"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(5, 104)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(77, 16)
        Me.Label8.TabIndex = 52
        Me.Label8.Text = "Catalogue"
        '
        'txtCategory
        '
        Me.txtCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCategory.Location = New System.Drawing.Point(120, 78)
        Me.txtCategory.MaxLength = 50
        Me.txtCategory.Name = "txtCategory"
        Me.txtCategory.Size = New System.Drawing.Size(136, 20)
        Me.txtCategory.TabIndex = 12
        Me.txtCategory.Tag = "NE;"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(5, 80)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(77, 16)
        Me.Label7.TabIndex = 50
        Me.Label7.Text = "Category"
        '
        'txtInvDescription
        '
        Me.txtInvDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvDescription.Location = New System.Drawing.Point(120, 30)
        Me.txtInvDescription.MaxLength = 50
        Me.txtInvDescription.Name = "txtInvDescription"
        Me.txtInvDescription.Size = New System.Drawing.Size(281, 20)
        Me.txtInvDescription.TabIndex = 10
        Me.txtInvDescription.Tag = "NE;"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(5, 32)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(77, 16)
        Me.Label6.TabIndex = 48
        Me.Label6.Text = "Description"
        '
        'txtinvCode
        '
        Me.txtinvCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtinvCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtinvCode.Location = New System.Drawing.Point(120, 6)
        Me.txtinvCode.MaxLength = 50
        Me.txtinvCode.Name = "txtinvCode"
        Me.txtinvCode.Size = New System.Drawing.Size(136, 20)
        Me.txtinvCode.TabIndex = 9
        Me.txtinvCode.Tag = "NE;"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(5, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(93, 16)
        Me.Label5.TabIndex = 46
        Me.Label5.Text = "Inventory Code"
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.txtGrossWeight)
        Me.Panel4.Controls.Add(Me.Label19)
        Me.Panel4.Controls.Add(Me.Label48)
        Me.Panel4.Controls.Add(Me.txtSMC)
        Me.Panel4.Controls.Add(Me.txtPMC)
        Me.Panel4.Controls.Add(Me.txtStoneWeight)
        Me.Panel4.Controls.Add(Me.Label13)
        Me.Panel4.Controls.Add(Me.txtWeight)
        Me.Panel4.Controls.Add(Me.Label14)
        Me.Panel4.Controls.Add(Me.Label16)
        Me.Panel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel4.Location = New System.Drawing.Point(376, 272)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(290, 82)
        Me.Panel4.TabIndex = 17
        '
        'txtGrossWeight
        '
        Me.txtGrossWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGrossWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGrossWeight.Location = New System.Drawing.Point(204, 17)
        Me.txtGrossWeight.MaxLength = 50
        Me.txtGrossWeight.Name = "txtGrossWeight"
        Me.txtGrossWeight.Size = New System.Drawing.Size(80, 20)
        Me.txtGrossWeight.TabIndex = 89
        Me.txtGrossWeight.Tag = "NE;"
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(107, -1)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(83, 12)
        Me.Label19.TabIndex = 66
        Me.Label19.Text = "Stone Weight"
        '
        'Label48
        '
        Me.Label48.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.Location = New System.Drawing.Point(204, -1)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(83, 19)
        Me.Label48.TabIndex = 90
        Me.Label48.Text = "Net Weight"
        '
        'txtSMC
        '
        Me.txtSMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSMC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMC.Location = New System.Drawing.Point(150, 55)
        Me.txtSMC.MaxLength = 50
        Me.txtSMC.Name = "txtSMC"
        Me.txtSMC.Size = New System.Drawing.Size(128, 20)
        Me.txtSMC.TabIndex = 20
        Me.txtSMC.Tag = "NE;"
        '
        'txtPMC
        '
        Me.txtPMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPMC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPMC.Location = New System.Drawing.Point(8, 54)
        Me.txtPMC.MaxLength = 50
        Me.txtPMC.Name = "txtPMC"
        Me.txtPMC.Size = New System.Drawing.Size(128, 20)
        Me.txtPMC.TabIndex = 19
        Me.txtPMC.Tag = "NE;"
        '
        'txtStoneWeight
        '
        Me.txtStoneWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStoneWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStoneWeight.Location = New System.Drawing.Point(110, 17)
        Me.txtStoneWeight.MaxLength = 50
        Me.txtStoneWeight.Name = "txtStoneWeight"
        Me.txtStoneWeight.Size = New System.Drawing.Size(88, 20)
        Me.txtStoneWeight.TabIndex = 18
        Me.txtStoneWeight.Tag = "NE;"
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(150, 40)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(48, 16)
        Me.Label13.TabIndex = 52
        Me.Label13.Text = "SMC"
        '
        'txtWeight
        '
        Me.txtWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeight.Location = New System.Drawing.Point(8, 18)
        Me.txtWeight.MaxLength = 50
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.Size = New System.Drawing.Size(96, 20)
        Me.txtWeight.TabIndex = 17
        Me.txtWeight.Tag = "NE;"
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(8, 39)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(56, 16)
        Me.Label14.TabIndex = 50
        Me.Label14.Text = "PMC"
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(8, -1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(48, 16)
        Me.Label16.TabIndex = 46
        Me.Label16.Text = "Weight"
        '
        'Panel5
        '
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.txtTotalSales)
        Me.Panel5.Controls.Add(Me.txtSalesPCS)
        Me.Panel5.Controls.Add(Me.txtSalesCostPerGram)
        Me.Panel5.Controls.Add(Me.Label27)
        Me.Panel5.Controls.Add(Me.txtSalesSMC)
        Me.Panel5.Controls.Add(Me.Label21)
        Me.Panel5.Controls.Add(Me.txtSalesDate)
        Me.Panel5.Controls.Add(Me.Label22)
        Me.Panel5.Controls.Add(Me.txtSalesBillNo)
        Me.Panel5.Controls.Add(Me.Label23)
        Me.Panel5.Controls.Add(Me.Label24)
        Me.Panel5.Controls.Add(Me.Label25)
        Me.Panel5.Controls.Add(Me.txtSalesMan)
        Me.Panel5.Controls.Add(Me.Label26)
        Me.Panel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel5.Location = New System.Drawing.Point(384, 368)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(280, 116)
        Me.Panel5.TabIndex = 18
        '
        'txtTotalSales
        '
        Me.txtTotalSales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalSales.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalSales.Location = New System.Drawing.Point(196, 63)
        Me.txtTotalSales.MaxLength = 50
        Me.txtTotalSales.Name = "txtTotalSales"
        Me.txtTotalSales.Size = New System.Drawing.Size(76, 20)
        Me.txtTotalSales.TabIndex = 30
        Me.txtTotalSales.Tag = "NE;"
        '
        'txtSalesPCS
        '
        Me.txtSalesPCS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesPCS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesPCS.Location = New System.Drawing.Point(112, 63)
        Me.txtSalesPCS.MaxLength = 50
        Me.txtSalesPCS.Name = "txtSalesPCS"
        Me.txtSalesPCS.Size = New System.Drawing.Size(80, 20)
        Me.txtSalesPCS.TabIndex = 29
        Me.txtSalesPCS.Tag = "NE;"
        '
        'txtSalesCostPerGram
        '
        Me.txtSalesCostPerGram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesCostPerGram.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesCostPerGram.Location = New System.Drawing.Point(8, 63)
        Me.txtSalesCostPerGram.MaxLength = 50
        Me.txtSalesCostPerGram.Name = "txtSalesCostPerGram"
        Me.txtSalesCostPerGram.Size = New System.Drawing.Size(96, 20)
        Me.txtSalesCostPerGram.TabIndex = 28
        Me.txtSalesCostPerGram.Tag = "NE;"
        '
        'Label27
        '
        Me.Label27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(8, 89)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(64, 16)
        Me.Label27.TabIndex = 57
        Me.Label27.Text = "Sales Man"
        '
        'txtSalesSMC
        '
        Me.txtSalesSMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesSMC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesSMC.Location = New System.Drawing.Point(196, 24)
        Me.txtSalesSMC.MaxLength = 50
        Me.txtSalesSMC.Name = "txtSalesSMC"
        Me.txtSalesSMC.Size = New System.Drawing.Size(77, 20)
        Me.txtSalesSMC.TabIndex = 27
        Me.txtSalesSMC.Tag = "NE;"
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(196, 47)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(64, 16)
        Me.Label21.TabIndex = 56
        Me.Label21.Text = "Total Sales"
        '
        'txtSalesDate
        '
        Me.txtSalesDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesDate.Location = New System.Drawing.Point(112, 24)
        Me.txtSalesDate.MaxLength = 50
        Me.txtSalesDate.Name = "txtSalesDate"
        Me.txtSalesDate.Size = New System.Drawing.Size(77, 20)
        Me.txtSalesDate.TabIndex = 26
        Me.txtSalesDate.Tag = "NE;"
        '
        'Label22
        '
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(112, 47)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(40, 16)
        Me.Label22.TabIndex = 54
        Me.Label22.Text = "PCS"
        '
        'txtSalesBillNo
        '
        Me.txtSalesBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesBillNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesBillNo.Location = New System.Drawing.Point(8, 24)
        Me.txtSalesBillNo.MaxLength = 50
        Me.txtSalesBillNo.Name = "txtSalesBillNo"
        Me.txtSalesBillNo.Size = New System.Drawing.Size(96, 20)
        Me.txtSalesBillNo.TabIndex = 25
        Me.txtSalesBillNo.Tag = "NE;"
        '
        'Label23
        '
        Me.Label23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(8, 47)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(72, 16)
        Me.Label23.TabIndex = 52
        Me.Label23.Text = "Cost/Gram"
        '
        'Label24
        '
        Me.Label24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(196, 5)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(32, 16)
        Me.Label24.TabIndex = 50
        Me.Label24.Text = "SMC"
        '
        'Label25
        '
        Me.Label25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(112, 5)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(32, 16)
        Me.Label25.TabIndex = 48
        Me.Label25.Text = "Date"
        '
        'txtSalesMan
        '
        Me.txtSalesMan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesMan.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesMan.Location = New System.Drawing.Point(113, 87)
        Me.txtSalesMan.MaxLength = 50
        Me.txtSalesMan.Name = "txtSalesMan"
        Me.txtSalesMan.Size = New System.Drawing.Size(159, 20)
        Me.txtSalesMan.TabIndex = 31
        Me.txtSalesMan.Tag = "NE;"
        '
        'Label26
        '
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(8, 6)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(64, 16)
        Me.Label26.TabIndex = 46
        Me.Label26.Text = "Sales Bill#"
        '
        'Panel6
        '
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Controls.Add(Me.txtSalesRetToal)
        Me.Panel6.Controls.Add(Me.txtSalesRetPCS)
        Me.Panel6.Controls.Add(Me.txtSalesRetCostperGram)
        Me.Panel6.Controls.Add(Me.txtSalesRetSMC)
        Me.Panel6.Controls.Add(Me.Label29)
        Me.Panel6.Controls.Add(Me.txtSalesRetDate)
        Me.Panel6.Controls.Add(Me.Label30)
        Me.Panel6.Controls.Add(Me.txtSalesRetRefNo)
        Me.Panel6.Controls.Add(Me.Label31)
        Me.Panel6.Controls.Add(Me.Label32)
        Me.Panel6.Controls.Add(Me.Label33)
        Me.Panel6.Controls.Add(Me.Label34)
        Me.Panel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel6.Location = New System.Drawing.Point(384, 488)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(280, 88)
        Me.Panel6.TabIndex = 19
        '
        'txtSalesRetToal
        '
        Me.txtSalesRetToal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesRetToal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesRetToal.Location = New System.Drawing.Point(195, 62)
        Me.txtSalesRetToal.MaxLength = 50
        Me.txtSalesRetToal.Name = "txtSalesRetToal"
        Me.txtSalesRetToal.Size = New System.Drawing.Size(81, 20)
        Me.txtSalesRetToal.TabIndex = 37
        Me.txtSalesRetToal.Tag = "NE;"
        '
        'txtSalesRetPCS
        '
        Me.txtSalesRetPCS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesRetPCS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesRetPCS.Location = New System.Drawing.Point(108, 62)
        Me.txtSalesRetPCS.MaxLength = 50
        Me.txtSalesRetPCS.Name = "txtSalesRetPCS"
        Me.txtSalesRetPCS.Size = New System.Drawing.Size(84, 20)
        Me.txtSalesRetPCS.TabIndex = 36
        Me.txtSalesRetPCS.Tag = "NE;"
        '
        'txtSalesRetCostperGram
        '
        Me.txtSalesRetCostperGram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesRetCostperGram.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesRetCostperGram.Location = New System.Drawing.Point(8, 62)
        Me.txtSalesRetCostperGram.MaxLength = 50
        Me.txtSalesRetCostperGram.Name = "txtSalesRetCostperGram"
        Me.txtSalesRetCostperGram.Size = New System.Drawing.Size(96, 20)
        Me.txtSalesRetCostperGram.TabIndex = 35
        Me.txtSalesRetCostperGram.Tag = "NE;"
        '
        'txtSalesRetSMC
        '
        Me.txtSalesRetSMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesRetSMC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesRetSMC.Location = New System.Drawing.Point(195, 23)
        Me.txtSalesRetSMC.MaxLength = 50
        Me.txtSalesRetSMC.Name = "txtSalesRetSMC"
        Me.txtSalesRetSMC.Size = New System.Drawing.Size(81, 20)
        Me.txtSalesRetSMC.TabIndex = 34
        Me.txtSalesRetSMC.Tag = "NE;"
        '
        'Label29
        '
        Me.Label29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(195, 46)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(64, 16)
        Me.Label29.TabIndex = 56
        Me.Label29.Text = "Total Sales"
        '
        'txtSalesRetDate
        '
        Me.txtSalesRetDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesRetDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesRetDate.Location = New System.Drawing.Point(108, 23)
        Me.txtSalesRetDate.MaxLength = 50
        Me.txtSalesRetDate.Name = "txtSalesRetDate"
        Me.txtSalesRetDate.Size = New System.Drawing.Size(84, 20)
        Me.txtSalesRetDate.TabIndex = 33
        Me.txtSalesRetDate.Tag = "NE;"
        '
        'Label30
        '
        Me.Label30.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(108, 46)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(40, 16)
        Me.Label30.TabIndex = 54
        Me.Label30.Text = "PCS"
        '
        'txtSalesRetRefNo
        '
        Me.txtSalesRetRefNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesRetRefNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesRetRefNo.Location = New System.Drawing.Point(8, 23)
        Me.txtSalesRetRefNo.MaxLength = 50
        Me.txtSalesRetRefNo.Name = "txtSalesRetRefNo"
        Me.txtSalesRetRefNo.Size = New System.Drawing.Size(96, 20)
        Me.txtSalesRetRefNo.TabIndex = 32
        Me.txtSalesRetRefNo.Tag = "NE;"
        '
        'Label31
        '
        Me.Label31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(8, 46)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(72, 16)
        Me.Label31.TabIndex = 52
        Me.Label31.Text = "Cost/Gram"
        '
        'Label32
        '
        Me.Label32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(195, 2)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(32, 16)
        Me.Label32.TabIndex = 50
        Me.Label32.Text = "SMC"
        '
        'Label33
        '
        Me.Label33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(108, 2)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(32, 16)
        Me.Label33.TabIndex = 48
        Me.Label33.Text = "Date"
        '
        'Label34
        '
        Me.Label34.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(8, 3)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(88, 16)
        Me.Label34.TabIndex = 46
        Me.Label34.Text = "Sales Ret Ref#"
        '
        'Panel7
        '
        Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel7.Controls.Add(Me.txtJobSheetDate)
        Me.Panel7.Controls.Add(Me.txtJobSheetNo)
        Me.Panel7.Controls.Add(Me.txtPurchaseSupplier)
        Me.Panel7.Controls.Add(Me.txtPurchasePMC)
        Me.Panel7.Controls.Add(Me.Label28)
        Me.Panel7.Controls.Add(Me.txtPurchaseDate)
        Me.Panel7.Controls.Add(Me.Label35)
        Me.Panel7.Controls.Add(Me.txtPurchaseNo)
        Me.Panel7.Controls.Add(Me.Label36)
        Me.Panel7.Controls.Add(Me.Label37)
        Me.Panel7.Controls.Add(Me.Label38)
        Me.Panel7.Controls.Add(Me.Label39)
        Me.Panel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel7.Location = New System.Drawing.Point(672, 368)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(288, 88)
        Me.Panel7.TabIndex = 20
        '
        'txtJobSheetDate
        '
        Me.txtJobSheetDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJobSheetDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJobSheetDate.Location = New System.Drawing.Point(200, 61)
        Me.txtJobSheetDate.MaxLength = 50
        Me.txtJobSheetDate.Name = "txtJobSheetDate"
        Me.txtJobSheetDate.Size = New System.Drawing.Size(80, 20)
        Me.txtJobSheetDate.TabIndex = 43
        Me.txtJobSheetDate.Tag = "NE;"
        '
        'txtJobSheetNo
        '
        Me.txtJobSheetNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJobSheetNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJobSheetNo.Location = New System.Drawing.Point(112, 61)
        Me.txtJobSheetNo.MaxLength = 50
        Me.txtJobSheetNo.Name = "txtJobSheetNo"
        Me.txtJobSheetNo.Size = New System.Drawing.Size(80, 20)
        Me.txtJobSheetNo.TabIndex = 42
        Me.txtJobSheetNo.Tag = "NE;"
        '
        'txtPurchaseSupplier
        '
        Me.txtPurchaseSupplier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurchaseSupplier.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurchaseSupplier.Location = New System.Drawing.Point(8, 61)
        Me.txtPurchaseSupplier.MaxLength = 50
        Me.txtPurchaseSupplier.Name = "txtPurchaseSupplier"
        Me.txtPurchaseSupplier.Size = New System.Drawing.Size(96, 20)
        Me.txtPurchaseSupplier.TabIndex = 41
        Me.txtPurchaseSupplier.Tag = "NE;"
        '
        'txtPurchasePMC
        '
        Me.txtPurchasePMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurchasePMC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurchasePMC.Location = New System.Drawing.Point(200, 19)
        Me.txtPurchasePMC.MaxLength = 50
        Me.txtPurchasePMC.Name = "txtPurchasePMC"
        Me.txtPurchasePMC.Size = New System.Drawing.Size(80, 20)
        Me.txtPurchasePMC.TabIndex = 40
        Me.txtPurchasePMC.Tag = "NE;"
        '
        'Label28
        '
        Me.Label28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(200, 44)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(64, 16)
        Me.Label28.TabIndex = 56
        Me.Label28.Text = "Date"
        '
        'txtPurchaseDate
        '
        Me.txtPurchaseDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurchaseDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurchaseDate.Location = New System.Drawing.Point(112, 19)
        Me.txtPurchaseDate.MaxLength = 50
        Me.txtPurchaseDate.Name = "txtPurchaseDate"
        Me.txtPurchaseDate.Size = New System.Drawing.Size(80, 20)
        Me.txtPurchaseDate.TabIndex = 39
        Me.txtPurchaseDate.Tag = "NE;"
        '
        'Label35
        '
        Me.Label35.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(112, 44)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(72, 16)
        Me.Label35.TabIndex = 54
        Me.Label35.Text = "Job Sheet#"
        '
        'txtPurchaseNo
        '
        Me.txtPurchaseNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurchaseNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurchaseNo.Location = New System.Drawing.Point(8, 19)
        Me.txtPurchaseNo.MaxLength = 50
        Me.txtPurchaseNo.Name = "txtPurchaseNo"
        Me.txtPurchaseNo.Size = New System.Drawing.Size(96, 20)
        Me.txtPurchaseNo.TabIndex = 38
        Me.txtPurchaseNo.Tag = "NE;"
        '
        'Label36
        '
        Me.Label36.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.Location = New System.Drawing.Point(200, 0)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(72, 16)
        Me.Label36.TabIndex = 52
        Me.Label36.Text = "PMC"
        '
        'Label37
        '
        Me.Label37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(112, 0)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(32, 16)
        Me.Label37.TabIndex = 50
        Me.Label37.Text = "Date"
        '
        'Label38
        '
        Me.Label38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(8, 0)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(64, 16)
        Me.Label38.TabIndex = 48
        Me.Label38.Text = "Purchase#"
        '
        'Label39
        '
        Me.Label39.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(8, 44)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(88, 16)
        Me.Label39.TabIndex = 46
        Me.Label39.Text = "Supplier"
        '
        'Panel8
        '
        Me.Panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel8.Controls.Add(Me.txtPurchaseRetTotal)
        Me.Panel8.Controls.Add(Me.txtPurchaseRetPCS)
        Me.Panel8.Controls.Add(Me.txtPurchaseRetCostperGram)
        Me.Panel8.Controls.Add(Me.txtPurchaseRetPMC)
        Me.Panel8.Controls.Add(Me.Label40)
        Me.Panel8.Controls.Add(Me.txtPurchaseRetDate)
        Me.Panel8.Controls.Add(Me.Label41)
        Me.Panel8.Controls.Add(Me.txtPurchaseRetNo)
        Me.Panel8.Controls.Add(Me.Label42)
        Me.Panel8.Controls.Add(Me.Label43)
        Me.Panel8.Controls.Add(Me.Label44)
        Me.Panel8.Controls.Add(Me.Label45)
        Me.Panel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel8.Location = New System.Drawing.Point(672, 488)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(288, 88)
        Me.Panel8.TabIndex = 21
        '
        'txtPurchaseRetTotal
        '
        Me.txtPurchaseRetTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurchaseRetTotal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurchaseRetTotal.Location = New System.Drawing.Point(200, 61)
        Me.txtPurchaseRetTotal.MaxLength = 50
        Me.txtPurchaseRetTotal.Name = "txtPurchaseRetTotal"
        Me.txtPurchaseRetTotal.Size = New System.Drawing.Size(80, 20)
        Me.txtPurchaseRetTotal.TabIndex = 49
        Me.txtPurchaseRetTotal.Tag = "NE;"
        '
        'txtPurchaseRetPCS
        '
        Me.txtPurchaseRetPCS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurchaseRetPCS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurchaseRetPCS.Location = New System.Drawing.Point(112, 61)
        Me.txtPurchaseRetPCS.MaxLength = 50
        Me.txtPurchaseRetPCS.Name = "txtPurchaseRetPCS"
        Me.txtPurchaseRetPCS.Size = New System.Drawing.Size(80, 20)
        Me.txtPurchaseRetPCS.TabIndex = 48
        Me.txtPurchaseRetPCS.Tag = "NE;"
        '
        'txtPurchaseRetCostperGram
        '
        Me.txtPurchaseRetCostperGram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurchaseRetCostperGram.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurchaseRetCostperGram.Location = New System.Drawing.Point(8, 61)
        Me.txtPurchaseRetCostperGram.MaxLength = 50
        Me.txtPurchaseRetCostperGram.Name = "txtPurchaseRetCostperGram"
        Me.txtPurchaseRetCostperGram.Size = New System.Drawing.Size(96, 20)
        Me.txtPurchaseRetCostperGram.TabIndex = 47
        Me.txtPurchaseRetCostperGram.Tag = "NE;"
        '
        'txtPurchaseRetPMC
        '
        Me.txtPurchaseRetPMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurchaseRetPMC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurchaseRetPMC.Location = New System.Drawing.Point(200, 19)
        Me.txtPurchaseRetPMC.MaxLength = 50
        Me.txtPurchaseRetPMC.Name = "txtPurchaseRetPMC"
        Me.txtPurchaseRetPMC.Size = New System.Drawing.Size(80, 20)
        Me.txtPurchaseRetPMC.TabIndex = 46
        Me.txtPurchaseRetPMC.Tag = "NE;"
        '
        'Label40
        '
        Me.Label40.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(200, 44)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(64, 16)
        Me.Label40.TabIndex = 56
        Me.Label40.Text = "Total"
        '
        'txtPurchaseRetDate
        '
        Me.txtPurchaseRetDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurchaseRetDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurchaseRetDate.Location = New System.Drawing.Point(112, 19)
        Me.txtPurchaseRetDate.MaxLength = 50
        Me.txtPurchaseRetDate.Name = "txtPurchaseRetDate"
        Me.txtPurchaseRetDate.Size = New System.Drawing.Size(80, 20)
        Me.txtPurchaseRetDate.TabIndex = 45
        Me.txtPurchaseRetDate.Tag = "NE;"
        '
        'Label41
        '
        Me.Label41.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.Location = New System.Drawing.Point(112, 44)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(72, 16)
        Me.Label41.TabIndex = 54
        Me.Label41.Text = "PCS"
        '
        'txtPurchaseRetNo
        '
        Me.txtPurchaseRetNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurchaseRetNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurchaseRetNo.Location = New System.Drawing.Point(8, 19)
        Me.txtPurchaseRetNo.MaxLength = 50
        Me.txtPurchaseRetNo.Name = "txtPurchaseRetNo"
        Me.txtPurchaseRetNo.Size = New System.Drawing.Size(96, 20)
        Me.txtPurchaseRetNo.TabIndex = 44
        Me.txtPurchaseRetNo.Tag = "NE;"
        '
        'Label42
        '
        Me.Label42.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.Location = New System.Drawing.Point(8, 44)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(72, 16)
        Me.Label42.TabIndex = 52
        Me.Label42.Text = "Cost/Gram"
        '
        'Label43
        '
        Me.Label43.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.Location = New System.Drawing.Point(200, 2)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(32, 16)
        Me.Label43.TabIndex = 50
        Me.Label43.Text = "PMC"
        '
        'Label44
        '
        Me.Label44.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(112, 2)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(64, 16)
        Me.Label44.TabIndex = 48
        Me.Label44.Text = "Date"
        '
        'Label45
        '
        Me.Label45.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.Location = New System.Drawing.Point(8, 2)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(88, 16)
        Me.Label45.TabIndex = 46
        Me.Label45.Text = "Purchase Ret.#"
        '
        'Panel9
        '
        Me.Panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel9.Location = New System.Drawing.Point(376, 360)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(592, 224)
        Me.Panel9.TabIndex = 22
        '
        'lblMsg
        '
        Me.lblMsg.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMsg.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMsg.ForeColor = System.Drawing.Color.Magenta
        Me.lblMsg.Location = New System.Drawing.Point(8, 561)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(360, 24)
        Me.lblMsg.TabIndex = 32
        Me.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel10
        '
        Me.Panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel10.Controls.Add(Me.txtSizeCode)
        Me.Panel10.Controls.Add(Me.Label46)
        Me.Panel10.Controls.Add(Me.txtTotalCost)
        Me.Panel10.Controls.Add(Me.txtQtyOnHand)
        Me.Panel10.Controls.Add(Me.txtCostPerGram)
        Me.Panel10.Controls.Add(Me.txtGoldLoss)
        Me.Panel10.Controls.Add(Me.Label17)
        Me.Panel10.Controls.Add(Me.Label11)
        Me.Panel10.Controls.Add(Me.Label12)
        Me.Panel10.Controls.Add(Me.Label15)
        Me.Panel10.Location = New System.Drawing.Point(678, 272)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(290, 82)
        Me.Panel10.TabIndex = 51
        '
        'txtSizeCode
        '
        Me.txtSizeCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSizeCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSizeCode.Location = New System.Drawing.Point(78, 19)
        Me.txtSizeCode.MaxLength = 50
        Me.txtSizeCode.Name = "txtSizeCode"
        Me.txtSizeCode.Size = New System.Drawing.Size(63, 20)
        Me.txtSizeCode.TabIndex = 66
        Me.txtSizeCode.Tag = "NE;"
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.White
        Me.Label46.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.Location = New System.Drawing.Point(78, 2)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(58, 16)
        Me.Label46.TabIndex = 67
        Me.Label46.Text = "Size"
        '
        'txtTotalCost
        '
        Me.txtTotalCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalCost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalCost.Location = New System.Drawing.Point(149, 55)
        Me.txtTotalCost.MaxLength = 50
        Me.txtTotalCost.Name = "txtTotalCost"
        Me.txtTotalCost.Size = New System.Drawing.Size(128, 20)
        Me.txtTotalCost.TabIndex = 24
        Me.txtTotalCost.Tag = "NE;"
        '
        'txtQtyOnHand
        '
        Me.txtQtyOnHand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtQtyOnHand.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQtyOnHand.Location = New System.Drawing.Point(8, 55)
        Me.txtQtyOnHand.MaxLength = 50
        Me.txtQtyOnHand.Name = "txtQtyOnHand"
        Me.txtQtyOnHand.Size = New System.Drawing.Size(128, 20)
        Me.txtQtyOnHand.TabIndex = 23
        Me.txtQtyOnHand.Tag = "NE;"
        '
        'txtCostPerGram
        '
        Me.txtCostPerGram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCostPerGram.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostPerGram.Location = New System.Drawing.Point(149, 19)
        Me.txtCostPerGram.MaxLength = 50
        Me.txtCostPerGram.Name = "txtCostPerGram"
        Me.txtCostPerGram.Size = New System.Drawing.Size(128, 20)
        Me.txtCostPerGram.TabIndex = 22
        Me.txtCostPerGram.Tag = "NE;"
        '
        'txtGoldLoss
        '
        Me.txtGoldLoss.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldLoss.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoldLoss.Location = New System.Drawing.Point(8, 19)
        Me.txtGoldLoss.MaxLength = 50
        Me.txtGoldLoss.Name = "txtGoldLoss"
        Me.txtGoldLoss.Size = New System.Drawing.Size(63, 20)
        Me.txtGoldLoss.TabIndex = 21
        Me.txtGoldLoss.Tag = "NE;"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.White
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(149, 40)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(80, 16)
        Me.Label17.TabIndex = 65
        Me.Label17.Text = "Total Cost"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.White
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(8, 40)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(80, 16)
        Me.Label11.TabIndex = 64
        Me.Label11.Text = "Qty On Hand"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.White
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(149, 2)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(72, 16)
        Me.Label12.TabIndex = 63
        Me.Label12.Text = "Cost/Gram"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.White
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(8, 2)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(58, 16)
        Me.Label15.TabIndex = 62
        Me.Label15.Text = "Gold Loss"
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(8, 7)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 100)
        Me.pic.TabIndex = 86
        Me.pic.TabStop = False
        '
        'grdInventory
        '
        Me.grdInventory.AllowUserToAddRows = False
        Me.grdInventory.AllowUserToDeleteRows = False
        Me.grdInventory.AllowUserToResizeColumns = False
        Me.grdInventory.AllowUserToResizeRows = False
        Me.grdInventory.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdInventory.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdInventory.ColumnHeadersHeight = 25
        Me.grdInventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdInventory.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cInvCode, Me.cDescription, Me.cWeight})
        Me.grdInventory.Location = New System.Drawing.Point(8, 113)
        Me.grdInventory.Name = "grdInventory"
        Me.grdInventory.RowHeadersVisible = False
        Me.grdInventory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdInventory.Size = New System.Drawing.Size(362, 445)
        Me.grdInventory.TabIndex = 87
        '
        'cInvCode
        '
        Me.cInvCode.HeaderText = "InventoryCode"
        Me.cInvCode.Name = "cInvCode"
        '
        'cDescription
        '
        Me.cDescription.HeaderText = "Description"
        Me.cDescription.Name = "cDescription"
        Me.cDescription.Width = 180
        '
        'cWeight
        '
        Me.cWeight.HeaderText = "Weight"
        Me.cWeight.Name = "cWeight"
        Me.cWeight.Width = 80
        '
        'txtCreatedBy
        '
        Me.txtCreatedBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCreatedBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCreatedBy.Location = New System.Drawing.Point(723, 215)
        Me.txtCreatedBy.MaxLength = 50
        Me.txtCreatedBy.Name = "txtCreatedBy"
        Me.txtCreatedBy.Size = New System.Drawing.Size(238, 20)
        Me.txtCreatedBy.TabIndex = 88
        Me.txtCreatedBy.Tag = "NE;"
        '
        'pnlSplitMerge
        '
        Me.pnlSplitMerge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSplitMerge.Controls.Add(Me.Panel12)
        Me.pnlSplitMerge.Controls.Add(Me.Panel14)
        Me.pnlSplitMerge.Location = New System.Drawing.Point(376, 456)
        Me.pnlSplitMerge.Name = "pnlSplitMerge"
        Me.pnlSplitMerge.Size = New System.Drawing.Size(600, 129)
        Me.pnlSplitMerge.TabIndex = 93
        Me.pnlSplitMerge.Visible = False
        '
        'Panel12
        '
        Me.Panel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel12.Controls.Add(Me.txtMWeightN)
        Me.Panel12.Controls.Add(Me.txtMInventoryN)
        Me.Panel12.Controls.Add(Me.txtMWeight2)
        Me.Panel12.Controls.Add(Me.txtMInventoryCode2)
        Me.Panel12.Controls.Add(Me.Label55)
        Me.Panel12.Controls.Add(Me.txtMWeight1)
        Me.Panel12.Controls.Add(Me.Label56)
        Me.Panel12.Controls.Add(Me.txtMInventoryCode1)
        Me.Panel12.Controls.Add(Me.Label57)
        Me.Panel12.Controls.Add(Me.Label58)
        Me.Panel12.Controls.Add(Me.Label59)
        Me.Panel12.Controls.Add(Me.Label60)
        Me.Panel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel12.Location = New System.Drawing.Point(302, 5)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(288, 88)
        Me.Panel12.TabIndex = 93
        '
        'txtMWeightN
        '
        Me.txtMWeightN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMWeightN.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMWeightN.Location = New System.Drawing.Point(200, 61)
        Me.txtMWeightN.MaxLength = 50
        Me.txtMWeightN.Name = "txtMWeightN"
        Me.txtMWeightN.Size = New System.Drawing.Size(80, 20)
        Me.txtMWeightN.TabIndex = 43
        Me.txtMWeightN.Tag = "NE;"
        '
        'txtMInventoryN
        '
        Me.txtMInventoryN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMInventoryN.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMInventoryN.Location = New System.Drawing.Point(112, 61)
        Me.txtMInventoryN.MaxLength = 50
        Me.txtMInventoryN.Name = "txtMInventoryN"
        Me.txtMInventoryN.Size = New System.Drawing.Size(80, 20)
        Me.txtMInventoryN.TabIndex = 42
        Me.txtMInventoryN.Tag = "NE;"
        '
        'txtMWeight2
        '
        Me.txtMWeight2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMWeight2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMWeight2.Location = New System.Drawing.Point(8, 61)
        Me.txtMWeight2.MaxLength = 50
        Me.txtMWeight2.Name = "txtMWeight2"
        Me.txtMWeight2.Size = New System.Drawing.Size(96, 20)
        Me.txtMWeight2.TabIndex = 41
        Me.txtMWeight2.Tag = "NE;"
        '
        'txtMInventoryCode2
        '
        Me.txtMInventoryCode2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMInventoryCode2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMInventoryCode2.Location = New System.Drawing.Point(200, 19)
        Me.txtMInventoryCode2.MaxLength = 50
        Me.txtMInventoryCode2.Name = "txtMInventoryCode2"
        Me.txtMInventoryCode2.Size = New System.Drawing.Size(80, 20)
        Me.txtMInventoryCode2.TabIndex = 40
        Me.txtMInventoryCode2.Tag = "NE;"
        '
        'Label55
        '
        Me.Label55.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label55.Location = New System.Drawing.Point(200, 44)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(64, 16)
        Me.Label55.TabIndex = 56
        Me.Label55.Text = "WeightN"
        '
        'txtMWeight1
        '
        Me.txtMWeight1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMWeight1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMWeight1.Location = New System.Drawing.Point(112, 19)
        Me.txtMWeight1.MaxLength = 50
        Me.txtMWeight1.Name = "txtMWeight1"
        Me.txtMWeight1.Size = New System.Drawing.Size(80, 20)
        Me.txtMWeight1.TabIndex = 39
        Me.txtMWeight1.Tag = "NE;"
        '
        'Label56
        '
        Me.Label56.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.Location = New System.Drawing.Point(112, 44)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(100, 18)
        Me.Label56.TabIndex = 54
        Me.Label56.Text = "InventoryCodeN"
        '
        'txtMInventoryCode1
        '
        Me.txtMInventoryCode1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMInventoryCode1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMInventoryCode1.Location = New System.Drawing.Point(8, 19)
        Me.txtMInventoryCode1.MaxLength = 50
        Me.txtMInventoryCode1.Name = "txtMInventoryCode1"
        Me.txtMInventoryCode1.Size = New System.Drawing.Size(96, 20)
        Me.txtMInventoryCode1.TabIndex = 38
        Me.txtMInventoryCode1.Tag = "NE;"
        '
        'Label57
        '
        Me.Label57.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label57.Location = New System.Drawing.Point(200, 0)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(84, 16)
        Me.Label57.TabIndex = 52
        Me.Label57.Text = "InventoryCode2"
        '
        'Label58
        '
        Me.Label58.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label58.Location = New System.Drawing.Point(112, 0)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(53, 21)
        Me.Label58.TabIndex = 50
        Me.Label58.Text = "Weight1"
        '
        'Label59
        '
        Me.Label59.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label59.Location = New System.Drawing.Point(8, 0)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(88, 22)
        Me.Label59.TabIndex = 48
        Me.Label59.Text = "InventoryCode1"
        '
        'Label60
        '
        Me.Label60.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label60.Location = New System.Drawing.Point(8, 44)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(88, 16)
        Me.Label60.TabIndex = 46
        Me.Label60.Text = "Weight2"
        '
        'Panel14
        '
        Me.Panel14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel14.Controls.Add(Me.TextBox19)
        Me.Panel14.Controls.Add(Me.TextBox20)
        Me.Panel14.Controls.Add(Me.txtOriginalWeight)
        Me.Panel14.Controls.Add(Me.Label67)
        Me.Panel14.Controls.Add(Me.txtWeight2)
        Me.Panel14.Controls.Add(Me.Label68)
        Me.Panel14.Controls.Add(Me.txtWeight1)
        Me.Panel14.Controls.Add(Me.Label69)
        Me.Panel14.Controls.Add(Me.txtDestInventoryCode)
        Me.Panel14.Controls.Add(Me.Label70)
        Me.Panel14.Controls.Add(Me.Label71)
        Me.Panel14.Controls.Add(Me.Label72)
        Me.Panel14.Controls.Add(Me.TextBox25)
        Me.Panel14.Controls.Add(Me.Label73)
        Me.Panel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel14.Location = New System.Drawing.Point(14, 5)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(280, 116)
        Me.Panel14.TabIndex = 92
        '
        'TextBox19
        '
        Me.TextBox19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox19.Location = New System.Drawing.Point(196, 63)
        Me.TextBox19.MaxLength = 50
        Me.TextBox19.Name = "TextBox19"
        Me.TextBox19.Size = New System.Drawing.Size(76, 20)
        Me.TextBox19.TabIndex = 30
        Me.TextBox19.Tag = "NE;"
        Me.TextBox19.Visible = False
        '
        'TextBox20
        '
        Me.TextBox20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox20.Location = New System.Drawing.Point(112, 63)
        Me.TextBox20.MaxLength = 50
        Me.TextBox20.Name = "TextBox20"
        Me.TextBox20.Size = New System.Drawing.Size(80, 20)
        Me.TextBox20.TabIndex = 29
        Me.TextBox20.Tag = "NE;"
        Me.TextBox20.Visible = False
        '
        'txtOriginalWeight
        '
        Me.txtOriginalWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOriginalWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOriginalWeight.Location = New System.Drawing.Point(8, 63)
        Me.txtOriginalWeight.MaxLength = 50
        Me.txtOriginalWeight.Name = "txtOriginalWeight"
        Me.txtOriginalWeight.Size = New System.Drawing.Size(96, 20)
        Me.txtOriginalWeight.TabIndex = 28
        Me.txtOriginalWeight.Tag = "NE;"
        '
        'Label67
        '
        Me.Label67.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label67.Location = New System.Drawing.Point(8, 89)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(64, 16)
        Me.Label67.TabIndex = 57
        Me.Label67.Text = "Sales Man"
        Me.Label67.Visible = False
        '
        'txtWeight2
        '
        Me.txtWeight2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWeight2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeight2.Location = New System.Drawing.Point(196, 24)
        Me.txtWeight2.MaxLength = 50
        Me.txtWeight2.Name = "txtWeight2"
        Me.txtWeight2.Size = New System.Drawing.Size(77, 20)
        Me.txtWeight2.TabIndex = 27
        Me.txtWeight2.Tag = "NE;"
        '
        'Label68
        '
        Me.Label68.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label68.Location = New System.Drawing.Point(196, 47)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(64, 16)
        Me.Label68.TabIndex = 56
        Me.Label68.Text = "Total Sales"
        Me.Label68.Visible = False
        '
        'txtWeight1
        '
        Me.txtWeight1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWeight1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeight1.Location = New System.Drawing.Point(112, 24)
        Me.txtWeight1.MaxLength = 50
        Me.txtWeight1.Name = "txtWeight1"
        Me.txtWeight1.Size = New System.Drawing.Size(77, 20)
        Me.txtWeight1.TabIndex = 26
        Me.txtWeight1.Tag = "NE;"
        '
        'Label69
        '
        Me.Label69.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label69.Location = New System.Drawing.Point(112, 47)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(40, 16)
        Me.Label69.TabIndex = 54
        Me.Label69.Text = "PCS"
        Me.Label69.Visible = False
        '
        'txtDestInventoryCode
        '
        Me.txtDestInventoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDestInventoryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDestInventoryCode.Location = New System.Drawing.Point(8, 24)
        Me.txtDestInventoryCode.MaxLength = 50
        Me.txtDestInventoryCode.Name = "txtDestInventoryCode"
        Me.txtDestInventoryCode.Size = New System.Drawing.Size(96, 20)
        Me.txtDestInventoryCode.TabIndex = 25
        Me.txtDestInventoryCode.Tag = "NE;"
        '
        'Label70
        '
        Me.Label70.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label70.Location = New System.Drawing.Point(8, 47)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(72, 16)
        Me.Label70.TabIndex = 52
        Me.Label70.Text = "Cost/Gram"
        '
        'Label71
        '
        Me.Label71.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label71.Location = New System.Drawing.Point(196, 5)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(59, 16)
        Me.Label71.TabIndex = 50
        Me.Label71.Text = "Weight2"
        '
        'Label72
        '
        Me.Label72.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label72.Location = New System.Drawing.Point(114, 5)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(54, 21)
        Me.Label72.TabIndex = 48
        Me.Label72.Text = "Weight1"
        '
        'TextBox25
        '
        Me.TextBox25.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox25.Location = New System.Drawing.Point(113, 87)
        Me.TextBox25.MaxLength = 50
        Me.TextBox25.Name = "TextBox25"
        Me.TextBox25.Size = New System.Drawing.Size(159, 20)
        Me.TextBox25.TabIndex = 31
        Me.TextBox25.Tag = "NE;"
        Me.TextBox25.Visible = False
        '
        'Label73
        '
        Me.Label73.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label73.Location = New System.Drawing.Point(8, 6)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(108, 16)
        Me.Label73.TabIndex = 46
        Me.Label73.Text = "Dest InventoryCode"
        '
        'chkSplitMerge
        '
        Me.chkSplitMerge.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSplitMerge.ForeColor = System.Drawing.Color.Black
        Me.chkSplitMerge.Location = New System.Drawing.Point(371, 590)
        Me.chkSplitMerge.Name = "chkSplitMerge"
        Me.chkSplitMerge.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSplitMerge.Size = New System.Drawing.Size(94, 31)
        Me.chkSplitMerge.TabIndex = 523
        Me.chkSplitMerge.Text = "Split Merge"
        '
        'grpAddlDescription
        '
        Me.grpAddlDescription.Controls.Add(Me.GroupBox9)
        Me.grpAddlDescription.Controls.Add(Me.GroupBox7)
        Me.grpAddlDescription.Controls.Add(Me.Label49)
        Me.grpAddlDescription.Controls.Add(Me.Label50)
        Me.grpAddlDescription.Controls.Add(Me.cmdAddlDescOk)
        Me.grpAddlDescription.Controls.Add(Me.Label51)
        Me.grpAddlDescription.Controls.Add(Me.Label52)
        Me.grpAddlDescription.Controls.Add(Me.Label53)
        Me.grpAddlDescription.Controls.Add(Me.Label54)
        Me.grpAddlDescription.Location = New System.Drawing.Point(519, 318)
        Me.grpAddlDescription.Name = "grpAddlDescription"
        Me.grpAddlDescription.Size = New System.Drawing.Size(445, 325)
        Me.grpAddlDescription.TabIndex = 524
        Me.grpAddlDescription.TabStop = False
        Me.grpAddlDescription.Visible = False
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.txtNewSellingPrice4)
        Me.GroupBox9.Controls.Add(Me.txtNewSellingPrice3)
        Me.GroupBox9.Controls.Add(Me.txtNewSellingPrice2)
        Me.GroupBox9.Controls.Add(Me.txtNewSellingPrice1)
        Me.GroupBox9.Controls.Add(Me.txtNewCost4)
        Me.GroupBox9.Controls.Add(Me.txtNewCost3)
        Me.GroupBox9.Controls.Add(Me.txtNewCost2)
        Me.GroupBox9.Controls.Add(Me.txtNewCost1)
        Me.GroupBox9.Controls.Add(Me.txtNewDesc4)
        Me.GroupBox9.Controls.Add(Me.txtNewDesc3)
        Me.GroupBox9.Controls.Add(Me.txtNewDesc2)
        Me.GroupBox9.Controls.Add(Me.txtNewDesc1)
        Me.GroupBox9.Location = New System.Drawing.Point(12, 165)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(417, 119)
        Me.GroupBox9.TabIndex = 531
        Me.GroupBox9.TabStop = False
        '
        'txtNewSellingPrice4
        '
        Me.txtNewSellingPrice4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewSellingPrice4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewSellingPrice4.ForeColor = System.Drawing.Color.Black
        Me.txtNewSellingPrice4.Location = New System.Drawing.Point(315, 89)
        Me.txtNewSellingPrice4.MaxLength = 50
        Me.txtNewSellingPrice4.Name = "txtNewSellingPrice4"
        Me.txtNewSellingPrice4.Size = New System.Drawing.Size(97, 20)
        Me.txtNewSellingPrice4.TabIndex = 56
        Me.txtNewSellingPrice4.Tag = ""
        '
        'txtNewSellingPrice3
        '
        Me.txtNewSellingPrice3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewSellingPrice3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewSellingPrice3.ForeColor = System.Drawing.Color.Black
        Me.txtNewSellingPrice3.Location = New System.Drawing.Point(315, 63)
        Me.txtNewSellingPrice3.MaxLength = 50
        Me.txtNewSellingPrice3.Name = "txtNewSellingPrice3"
        Me.txtNewSellingPrice3.Size = New System.Drawing.Size(98, 20)
        Me.txtNewSellingPrice3.TabIndex = 53
        Me.txtNewSellingPrice3.Tag = ""
        '
        'txtNewSellingPrice2
        '
        Me.txtNewSellingPrice2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewSellingPrice2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewSellingPrice2.ForeColor = System.Drawing.Color.Black
        Me.txtNewSellingPrice2.Location = New System.Drawing.Point(315, 37)
        Me.txtNewSellingPrice2.MaxLength = 50
        Me.txtNewSellingPrice2.Name = "txtNewSellingPrice2"
        Me.txtNewSellingPrice2.Size = New System.Drawing.Size(98, 20)
        Me.txtNewSellingPrice2.TabIndex = 50
        Me.txtNewSellingPrice2.Tag = ""
        '
        'txtNewSellingPrice1
        '
        Me.txtNewSellingPrice1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewSellingPrice1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewSellingPrice1.ForeColor = System.Drawing.Color.Black
        Me.txtNewSellingPrice1.Location = New System.Drawing.Point(315, 11)
        Me.txtNewSellingPrice1.MaxLength = 50
        Me.txtNewSellingPrice1.Name = "txtNewSellingPrice1"
        Me.txtNewSellingPrice1.Size = New System.Drawing.Size(98, 20)
        Me.txtNewSellingPrice1.TabIndex = 47
        Me.txtNewSellingPrice1.Tag = ""
        '
        'txtNewCost4
        '
        Me.txtNewCost4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewCost4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewCost4.ForeColor = System.Drawing.Color.Black
        Me.txtNewCost4.Location = New System.Drawing.Point(205, 89)
        Me.txtNewCost4.MaxLength = 50
        Me.txtNewCost4.Name = "txtNewCost4"
        Me.txtNewCost4.Size = New System.Drawing.Size(97, 20)
        Me.txtNewCost4.TabIndex = 55
        Me.txtNewCost4.Tag = ""
        '
        'txtNewCost3
        '
        Me.txtNewCost3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewCost3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewCost3.ForeColor = System.Drawing.Color.Black
        Me.txtNewCost3.Location = New System.Drawing.Point(205, 63)
        Me.txtNewCost3.MaxLength = 50
        Me.txtNewCost3.Name = "txtNewCost3"
        Me.txtNewCost3.Size = New System.Drawing.Size(98, 20)
        Me.txtNewCost3.TabIndex = 52
        Me.txtNewCost3.Tag = ""
        '
        'txtNewCost2
        '
        Me.txtNewCost2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewCost2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewCost2.ForeColor = System.Drawing.Color.Black
        Me.txtNewCost2.Location = New System.Drawing.Point(205, 37)
        Me.txtNewCost2.MaxLength = 50
        Me.txtNewCost2.Name = "txtNewCost2"
        Me.txtNewCost2.Size = New System.Drawing.Size(98, 20)
        Me.txtNewCost2.TabIndex = 49
        Me.txtNewCost2.Tag = ""
        '
        'txtNewCost1
        '
        Me.txtNewCost1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewCost1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewCost1.ForeColor = System.Drawing.Color.Black
        Me.txtNewCost1.Location = New System.Drawing.Point(205, 11)
        Me.txtNewCost1.MaxLength = 50
        Me.txtNewCost1.Name = "txtNewCost1"
        Me.txtNewCost1.Size = New System.Drawing.Size(98, 20)
        Me.txtNewCost1.TabIndex = 46
        Me.txtNewCost1.Tag = ""
        '
        'txtNewDesc4
        '
        Me.txtNewDesc4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewDesc4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewDesc4.ForeColor = System.Drawing.Color.Black
        Me.txtNewDesc4.Location = New System.Drawing.Point(3, 88)
        Me.txtNewDesc4.MaxLength = 50
        Me.txtNewDesc4.Name = "txtNewDesc4"
        Me.txtNewDesc4.Size = New System.Drawing.Size(187, 20)
        Me.txtNewDesc4.TabIndex = 54
        Me.txtNewDesc4.Tag = ""
        '
        'txtNewDesc3
        '
        Me.txtNewDesc3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewDesc3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewDesc3.ForeColor = System.Drawing.Color.Black
        Me.txtNewDesc3.Location = New System.Drawing.Point(3, 62)
        Me.txtNewDesc3.MaxLength = 50
        Me.txtNewDesc3.Name = "txtNewDesc3"
        Me.txtNewDesc3.Size = New System.Drawing.Size(188, 20)
        Me.txtNewDesc3.TabIndex = 51
        Me.txtNewDesc3.Tag = ""
        '
        'txtNewDesc2
        '
        Me.txtNewDesc2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewDesc2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewDesc2.ForeColor = System.Drawing.Color.Black
        Me.txtNewDesc2.Location = New System.Drawing.Point(3, 36)
        Me.txtNewDesc2.MaxLength = 50
        Me.txtNewDesc2.Name = "txtNewDesc2"
        Me.txtNewDesc2.Size = New System.Drawing.Size(188, 20)
        Me.txtNewDesc2.TabIndex = 48
        Me.txtNewDesc2.Tag = ""
        '
        'txtNewDesc1
        '
        Me.txtNewDesc1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewDesc1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewDesc1.ForeColor = System.Drawing.Color.Black
        Me.txtNewDesc1.Location = New System.Drawing.Point(3, 10)
        Me.txtNewDesc1.MaxLength = 50
        Me.txtNewDesc1.Name = "txtNewDesc1"
        Me.txtNewDesc1.Size = New System.Drawing.Size(188, 20)
        Me.txtNewDesc1.TabIndex = 45
        Me.txtNewDesc1.Tag = ""
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.txtSellingPrice4)
        Me.GroupBox7.Controls.Add(Me.txtSellingPrice3)
        Me.GroupBox7.Controls.Add(Me.txtSellingPrice2)
        Me.GroupBox7.Controls.Add(Me.txtSellingPrice1)
        Me.GroupBox7.Controls.Add(Me.txtCost4)
        Me.GroupBox7.Controls.Add(Me.txtCost3)
        Me.GroupBox7.Controls.Add(Me.txtCost2)
        Me.GroupBox7.Controls.Add(Me.txtCost1)
        Me.GroupBox7.Controls.Add(Me.txtDesc4)
        Me.GroupBox7.Controls.Add(Me.txtDesc3)
        Me.GroupBox7.Controls.Add(Me.txtDesc2)
        Me.GroupBox7.Controls.Add(Me.txtDesc1)
        Me.GroupBox7.Location = New System.Drawing.Point(12, 33)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(417, 119)
        Me.GroupBox7.TabIndex = 530
        Me.GroupBox7.TabStop = False
        '
        'txtSellingPrice4
        '
        Me.txtSellingPrice4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSellingPrice4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSellingPrice4.ForeColor = System.Drawing.Color.Black
        Me.txtSellingPrice4.Location = New System.Drawing.Point(315, 89)
        Me.txtSellingPrice4.MaxLength = 50
        Me.txtSellingPrice4.Name = "txtSellingPrice4"
        Me.txtSellingPrice4.ReadOnly = True
        Me.txtSellingPrice4.Size = New System.Drawing.Size(97, 20)
        Me.txtSellingPrice4.TabIndex = 56
        Me.txtSellingPrice4.Tag = ""
        '
        'txtSellingPrice3
        '
        Me.txtSellingPrice3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSellingPrice3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSellingPrice3.ForeColor = System.Drawing.Color.Black
        Me.txtSellingPrice3.Location = New System.Drawing.Point(315, 63)
        Me.txtSellingPrice3.MaxLength = 50
        Me.txtSellingPrice3.Name = "txtSellingPrice3"
        Me.txtSellingPrice3.ReadOnly = True
        Me.txtSellingPrice3.Size = New System.Drawing.Size(98, 20)
        Me.txtSellingPrice3.TabIndex = 53
        Me.txtSellingPrice3.Tag = ""
        '
        'txtSellingPrice2
        '
        Me.txtSellingPrice2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSellingPrice2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSellingPrice2.ForeColor = System.Drawing.Color.Black
        Me.txtSellingPrice2.Location = New System.Drawing.Point(315, 37)
        Me.txtSellingPrice2.MaxLength = 50
        Me.txtSellingPrice2.Name = "txtSellingPrice2"
        Me.txtSellingPrice2.ReadOnly = True
        Me.txtSellingPrice2.Size = New System.Drawing.Size(98, 20)
        Me.txtSellingPrice2.TabIndex = 50
        Me.txtSellingPrice2.Tag = ""
        '
        'txtSellingPrice1
        '
        Me.txtSellingPrice1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSellingPrice1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSellingPrice1.ForeColor = System.Drawing.Color.Black
        Me.txtSellingPrice1.Location = New System.Drawing.Point(315, 11)
        Me.txtSellingPrice1.MaxLength = 50
        Me.txtSellingPrice1.Name = "txtSellingPrice1"
        Me.txtSellingPrice1.ReadOnly = True
        Me.txtSellingPrice1.Size = New System.Drawing.Size(98, 20)
        Me.txtSellingPrice1.TabIndex = 47
        Me.txtSellingPrice1.Tag = ""
        '
        'txtCost4
        '
        Me.txtCost4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCost4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCost4.ForeColor = System.Drawing.Color.Black
        Me.txtCost4.Location = New System.Drawing.Point(205, 89)
        Me.txtCost4.MaxLength = 50
        Me.txtCost4.Name = "txtCost4"
        Me.txtCost4.ReadOnly = True
        Me.txtCost4.Size = New System.Drawing.Size(97, 20)
        Me.txtCost4.TabIndex = 55
        Me.txtCost4.Tag = ""
        '
        'txtCost3
        '
        Me.txtCost3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCost3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCost3.ForeColor = System.Drawing.Color.Black
        Me.txtCost3.Location = New System.Drawing.Point(205, 63)
        Me.txtCost3.MaxLength = 50
        Me.txtCost3.Name = "txtCost3"
        Me.txtCost3.ReadOnly = True
        Me.txtCost3.Size = New System.Drawing.Size(98, 20)
        Me.txtCost3.TabIndex = 52
        Me.txtCost3.Tag = ""
        '
        'txtCost2
        '
        Me.txtCost2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCost2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCost2.ForeColor = System.Drawing.Color.Black
        Me.txtCost2.Location = New System.Drawing.Point(205, 37)
        Me.txtCost2.MaxLength = 50
        Me.txtCost2.Name = "txtCost2"
        Me.txtCost2.ReadOnly = True
        Me.txtCost2.Size = New System.Drawing.Size(98, 20)
        Me.txtCost2.TabIndex = 49
        Me.txtCost2.Tag = ""
        '
        'txtCost1
        '
        Me.txtCost1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCost1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCost1.ForeColor = System.Drawing.Color.Black
        Me.txtCost1.Location = New System.Drawing.Point(205, 11)
        Me.txtCost1.MaxLength = 50
        Me.txtCost1.Name = "txtCost1"
        Me.txtCost1.ReadOnly = True
        Me.txtCost1.Size = New System.Drawing.Size(98, 20)
        Me.txtCost1.TabIndex = 46
        Me.txtCost1.Tag = ""
        '
        'txtDesc4
        '
        Me.txtDesc4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDesc4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc4.ForeColor = System.Drawing.Color.Black
        Me.txtDesc4.Location = New System.Drawing.Point(3, 88)
        Me.txtDesc4.MaxLength = 50
        Me.txtDesc4.Name = "txtDesc4"
        Me.txtDesc4.ReadOnly = True
        Me.txtDesc4.Size = New System.Drawing.Size(187, 20)
        Me.txtDesc4.TabIndex = 54
        Me.txtDesc4.Tag = ""
        '
        'txtDesc3
        '
        Me.txtDesc3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDesc3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc3.ForeColor = System.Drawing.Color.Black
        Me.txtDesc3.Location = New System.Drawing.Point(3, 62)
        Me.txtDesc3.MaxLength = 50
        Me.txtDesc3.Name = "txtDesc3"
        Me.txtDesc3.ReadOnly = True
        Me.txtDesc3.Size = New System.Drawing.Size(188, 20)
        Me.txtDesc3.TabIndex = 51
        Me.txtDesc3.Tag = ""
        '
        'txtDesc2
        '
        Me.txtDesc2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDesc2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc2.ForeColor = System.Drawing.Color.Black
        Me.txtDesc2.Location = New System.Drawing.Point(3, 36)
        Me.txtDesc2.MaxLength = 50
        Me.txtDesc2.Name = "txtDesc2"
        Me.txtDesc2.ReadOnly = True
        Me.txtDesc2.Size = New System.Drawing.Size(188, 20)
        Me.txtDesc2.TabIndex = 48
        Me.txtDesc2.Tag = ""
        '
        'txtDesc1
        '
        Me.txtDesc1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDesc1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc1.ForeColor = System.Drawing.Color.Black
        Me.txtDesc1.Location = New System.Drawing.Point(3, 10)
        Me.txtDesc1.MaxLength = 50
        Me.txtDesc1.Name = "txtDesc1"
        Me.txtDesc1.ReadOnly = True
        Me.txtDesc1.Size = New System.Drawing.Size(188, 20)
        Me.txtDesc1.TabIndex = 45
        Me.txtDesc1.Tag = ""
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label49.Location = New System.Drawing.Point(324, 17)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(75, 13)
        Me.Label49.TabIndex = 529
        Me.Label49.Text = "Selling Price"
        Me.Label49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label50.Location = New System.Drawing.Point(214, 17)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(32, 13)
        Me.Label50.TabIndex = 524
        Me.Label50.Text = "Cost"
        Me.Label50.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdAddlDescOk
        '
        Me.cmdAddlDescOk.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdAddlDescOk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAddlDescOk.ForeColor = System.Drawing.Color.Black
        Me.cmdAddlDescOk.Location = New System.Drawing.Point(365, 286)
        Me.cmdAddlDescOk.Name = "cmdAddlDescOk"
        Me.cmdAddlDescOk.Size = New System.Drawing.Size(61, 23)
        Me.cmdAddlDescOk.TabIndex = 45
        Me.cmdAddlDescOk.Text = "OK"
        Me.cmdAddlDescOk.UseVisualStyleBackColor = False
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label51.Location = New System.Drawing.Point(12, 16)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(71, 13)
        Me.Label51.TabIndex = 515
        Me.Label51.Text = "Description"
        Me.Label51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label52.Location = New System.Drawing.Point(19, 99)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(78, 13)
        Me.Label52.TabIndex = 518
        Me.Label52.Text = "Description4"
        Me.Label52.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label52.Visible = False
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label53.Location = New System.Drawing.Point(20, 73)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(78, 13)
        Me.Label53.TabIndex = 517
        Me.Label53.Text = "Description3"
        Me.Label53.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label53.Visible = False
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label54.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label54.Location = New System.Drawing.Point(20, 47)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(78, 13)
        Me.Label54.TabIndex = 516
        Me.Label54.Text = "Description2"
        Me.Label54.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label54.Visible = False
        '
        'chkAdditionalDesc
        '
        Me.chkAdditionalDesc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAdditionalDesc.ForeColor = System.Drawing.Color.Black
        Me.chkAdditionalDesc.Location = New System.Drawing.Point(471, 592)
        Me.chkAdditionalDesc.Name = "chkAdditionalDesc"
        Me.chkAdditionalDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAdditionalDesc.Size = New System.Drawing.Size(68, 31)
        Me.chkAdditionalDesc.TabIndex = 525
        Me.chkAdditionalDesc.Text = "Addl. Desc"
        '
        'frmInventory
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(978, 624)
        Me.Controls.Add(Me.chkAdditionalDesc)
        Me.Controls.Add(Me.grpAddlDescription)
        Me.Controls.Add(Me.chkSplitMerge)
        Me.Controls.Add(Me.pnlSplitMerge)
        Me.Controls.Add(Me.txtCreatedBy)
        Me.Controls.Add(Me.grdInventory)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel10)
        Me.Controls.Add(Me.lblMsg)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.Panel9)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmInventory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Inventory Search"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.Panel10.ResumeLayout(False)
        Me.Panel10.PerformLayout()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdInventory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSplitMerge.ResumeLayout(False)
        Me.Panel12.ResumeLayout(False)
        Me.Panel12.PerformLayout()
        Me.Panel14.ResumeLayout(False)
        Me.Panel14.PerformLayout()
        Me.grpAddlDescription.ResumeLayout(False)
        Me.grpAddlDescription.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Dim objClsInventorySearch As New ClsInventorySearch
    Const InvCode = 1
    Const InvDescription = 2
    Const InvWeight = 3

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmInventory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'subGridSetting(grdInventory)
        txtDescription.Focus()
    End Sub

    Private Sub btnCatalogueSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCatalogueSearch.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim frmHelpCatalogue As New frmHelpSearch(0, 0, "tblcatalogue", "L", 4, "CatalogueCode", "", "CatalogueCode", "CatalogueCode", 80, "CatalogueName", "CatalogueName", 180, "CategoryCode", "CategoryCode", 60, "Remarks", "Remarks", 80)
            frmHelpCatalogue.ShowDialog()
            txtCatalogueCode.Text = frmHelpCatalogue.Var1.Trim
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnSupplierSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSupplierSearch.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim frmHelpSupplier As New frmHelpSearch(0, 0, "tblsupplier", "L", 4, "SupplierCode", "", "SupplierCode", "SupplierCode", 80, "SupplierName", "SupplierName", 180, "Address1", "Address1", 70, "Address2", "Address2", 70)
            frmHelpSupplier.ShowDialog()
            txtSupplier.Text = frmHelpSupplier.Var1.Trim
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub subFetchInventoryDetails()
        Dim sQry As String
        Dim sCondition As String
        Dim dsInv As New DataSet
        Dim iCnt As Integer
        Try
            Cursor = Cursors.WaitCursor
            sCondition = ""
            sQry = " Select a.InventoryCode, a.Description, a.Weight from tblInventory a , tblInventoryStock b where a.InventoryCode=b.InventoryCode and a.LocationCode=b.LocationCode "
            sCondition = "a"
            sQry = sQry & " and a.Locationcode =  " & fncFormatSQLText(gthisLocation)
            'check for description 
            If txtDescription.Text.Trim <> "" Then
                'sQry = sQry & " and a.Description like " & fncFormatSQLText(txtDescription.Text.Trim & "%")
                sQry = sQry & " and a.Description like " & fncFormatSQLText("%" & txtDescription.Text.Trim & "%")
                sCondition = txtDescription.Text.Trim
            End If
            'check for weight
            If Val(txtSearchWeight.Text.Trim) <> 0 Then
                If Len(sCondition) > 0 Then
                    sQry = sQry & " and  a.Weight between " & Val(txtSearchWeight.Text.Trim) - 0.5 & " and " & Val(txtSearchWeight.Text.Trim) + 0.5
                Else
                    sQry = sQry & " Where  a.Weight between " & Val(txtSearchWeight.Text.Trim) - 0.5 & " and " & Val(txtSearchWeight.Text.Trim) + 0.5
                End If
                sCondition = sCondition & txtSearchWeight.Text.Trim
            End If
            'check for catalogue code
            If txtCatalogueCode.Text.Trim <> "" Then
                If Len(sCondition) > 0 Then
                    sQry = sQry & " and CatalogueCode = " & fncFormatSQLText(txtCatalogueCode.Text.Trim)
                Else
                    sQry = sQry & " Where CatalogueCode = " & fncFormatSQLText(txtCatalogueCode.Text.Trim)
                End If
                sCondition = sCondition & txtCatalogueCode.Text.Trim
            End If
            'check for supplier 
            If txtSupplier.Text.Trim <> "" Then
                If Len(sCondition) > 0 Then
                    sQry = sQry & " and SupplierCode = " & fncFormatSQLText(txtSupplier.Text.Trim)
                Else
                    sQry = sQry & " Where SupplierCode = " & fncFormatSQLText(txtSupplier.Text.Trim)
                End If
                sCondition = sCondition & txtSupplier.Text.Trim
            End If
            'check for stock or sold
            If optStock.Checked = True Then
                If Len(sCondition) > 0 Then
                    sQry = sQry & " and b.QtyOnHand > 0"
                Else
                    sQry = sQry & " where b.QtyOnHand > 0"
                End If
                lblMsg.Text = "Search by Stock in Hand."
            ElseIf optSold.Checked = True Then
                If Len(sCondition) > 0 Then
                    sQry = sQry & " and b.QtySold > 0"
                Else
                    sQry = sQry & " where b.QtySold > 0"
                End If
                lblMsg.Text = "Search by Stock Sold."
            End If
            sQry = sQry & " order by a.Description "
            dsInv = objClsInventorySearch.FetchInventoryDetails(sQry)
            grdInventory.RowCount = 0
            If dsInv.Tables("Inventory").Rows.Count > 0 Then
                For iCnt = 0 To dsInv.Tables("Inventory").Rows.Count - 1
                    'grdInventory.MaxRows = grdInventory.MaxRows + 1
                    grdInventory.Rows.Add()
                    fnSetdgvText(grdInventory, grdInventory.RowCount - 1, 0, dsInv.Tables("Inventory").Rows(iCnt)("InventoryCode"))
                    fnSetdgvText(grdInventory, grdInventory.RowCount - 1, 1, dsInv.Tables("Inventory").Rows(iCnt)("Description"))
                    fnSetdgvText(grdInventory, grdInventory.RowCount - 1, 2, dsInv.Tables("Inventory").Rows(iCnt)("Weight"))
                Next
                grdInventory.Focus()
                subGetInvDetails(grdInventory.CurrentRow.Index)
            Else
                txtDescription.Focus()
            End If
            dsInv = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        subFetchInventoryDetails()
    End Sub

    Private Sub frmInventory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F5
                If btnRefresh.Enabled Then btnRefresh_Click(sender, e)
            Case Keys.Escape
                btnClose_Click(sender, e)
            Case Keys.Return
                SendKeys.Send("{Tab}")
        End Select
    End Sub

    Private Sub txtCatalogueCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCatalogueCode.KeyDown
        If e.KeyCode = Keys.F1 Then btnCatalogueSearch_Click(sender, e)
    End Sub

    Private Sub txtSupplier_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSupplier.KeyDown
        If e.KeyCode = Keys.F1 Then btnSupplierSearch_Click(sender, e)
    End Sub



    Private Sub subGetInvDetails(ByVal iRow As Integer)
        Dim dsInvDetail As New DataSet
        Dim sFileName As String
        '  Dim FSO As New Scripting.FileSystemObject
        Try
            Cursor = Cursors.WaitCursor
            If iRow > 0 Then
                txtinvCode.Text = GetText(grdInventory, iRow, InvCode)
                If txtinvCode.Text.Trim <> "" Then
                    dsInvDetail = objClsInventorySearch.GetInventoryDetails(txtinvCode.Text.Trim)
                    If dsInvDetail.Tables.Count > 0 Then
                        If dsInvDetail.Tables(0).Rows.Count > 0 Then
                            txtInvDescription.Text = Trim(dsInvDetail.Tables(0).Rows(0)("Description") & "")
                            txtCategory.Text = Trim(dsInvDetail.Tables(0).Rows(0)("CategoryCode") & "")
                            txtCatalogue.Text = Trim(dsInvDetail.Tables(0).Rows(0)("CatalogueCode") & "")
                            txtClass.Text = Trim(dsInvDetail.Tables(0).Rows(0)("ClassCode") & "")
                            txtLocation.Text = Trim(dsInvDetail.Tables(0).Rows(0)("LocationCode") & "")
                            txtWeight.Text = Trim(dsInvDetail.Tables(0).Rows(0)("Weight") & "")
                            txtGoldLoss.Text = Trim(dsInvDetail.Tables(0).Rows(0)("GoldLoss") & "")
                            txtSizeCode.Text = Trim(dsInvDetail.Tables(0).Rows(0)("SizeCode") & "")
                            ' txtPMC.Text = Trim(dsInvDetail.Tables(0).Rows(0)("PMC") & "")
                            txtSMC.Text = Trim(dsInvDetail.Tables(0).Rows(0)("SMC") & "")
                            txtCostPerGram.Text = Trim(dsInvDetail.Tables(0).Rows(0)("CostPerGram") & "")
                            txtQtyOnHand.Text = Trim(dsInvDetail.Tables(0).Rows(0)("QtyOnHand") & "")
                            If Trim(sCompanyShortCode) = "MD" Then
                                txtPMC.Text = Trim(dsInvDetail.Tables(0).Rows(0)("PMC") & "") + (Trim(dsInvDetail.Tables(0).Rows(0)("PMC") & "") * 0.1)
                                txtTotalCost.Text = Trim(dsInvDetail.Tables(0).Rows(0)("TotalCost") & "") + (Trim(dsInvDetail.Tables(0).Rows(0)("TotalCost") & "") * 0.1)
                            Else
                                txtPMC.Text = Trim(dsInvDetail.Tables(0).Rows(0)("PMC") & "")
                                txtTotalCost.Text = Trim(dsInvDetail.Tables(0).Rows(0)("TotalCost") & "")
                            End If
                            'txtTotalCost.Text = Trim(dsInvDetail.Tables(0).Rows(0)("TotalCost") & "")
                            txtDiamondDescription.Text = Trim(dsInvDetail.Tables(0).Rows(0)("DiamondDescription") & "")
                            txtStoneWeight.Text = Trim(dsInvDetail.Tables(0).Rows(0)("StoneWeight") & "")
                            txtRemarks.Text = Trim(dsInvDetail.Tables(0).Rows(0)("Remarks") & "")
                            txtSalesBillNo.Text = Trim(dsInvDetail.Tables(0).Rows(0)("SalesBillNo") & "")
                            txtSalesDate.Text = Trim(dsInvDetail.Tables(0).Rows(0)("SalesDate") & "")
                            txtSalesSMC.Text = Trim(dsInvDetail.Tables(0).Rows(0)("SalesMc") & "")
                            txtSalesCostPerGram.Text = Trim(dsInvDetail.Tables(0).Rows(0)("SalesCostPerGram") & "")
                            txtSalesPCS.Text = Trim(dsInvDetail.Tables(0).Rows(0)("QtySold") & "")
                            txtPurchaseSupplier.Text = Trim(dsInvDetail.Tables(0).Rows(0)("SupplierCode") & "")
                            txtPurchaseNo.Text = Trim(dsInvDetail.Tables(0).Rows(0)("PurchaseBillNo") & "")
                            txtPurchaseDate.Text = Format(CDate(dsInvDetail.Tables(0).Rows(0)("PurchaseDate") & ""), "dd/MM/yyyy")
                            txtPMC.Text = Trim(dsInvDetail.Tables(0).Rows(0)("PMC") & "")
                            txtJobSheetNo.Text = Trim(dsInvDetail.Tables(0).Rows(0)("JobSheetNo") & "")
                            txtCreatedBy.Text = Trim(dsInvDetail.Tables(0).Rows(0)("CreateUser") & "")
                            txtGrossWeight.Text = Trim(dsInvDetail.Tables(0).Rows(0)("Weight") & "") - Trim(dsInvDetail.Tables(0).Rows(0)("StoneWeight") & "")
                        End If

                        If dsInvDetail.Tables(1).Rows.Count > 0 Then
                            txtJobSheetDate.Text = Format(CDate(dsInvDetail.Tables(1).Rows(0)("JobSheetDate") & ""), "dd/MM/yyyy")
                        Else
                            txtJobSheetDate.Clear()
                        End If
                        If dsInvDetail.Tables(2).Rows.Count > 0 Then
                            txtSalesMan.Text = Trim(dsInvDetail.Tables(2).Rows(0)("SalesManName") & "")
                        Else
                            txtSalesMan.Clear()
                        End If
                        If dsInvDetail.Tables(3).Rows.Count > 0 Then
                            txtTotalCost.Text = Format(Trim(dsInvDetail.Tables(3).Rows(0)("NetAmount") & ""), "0.00")
                        Else
                            txtTotalCost.Text = 0.0
                        End If
                        If dsInvDetail.Tables(4).Rows.Count > 0 Then
                            txtSalesRetRefNo.Text = Trim(dsInvDetail.Tables(4).Rows(0)("RefNo") & "")
                            txtSalesRetDate.Text = Trim(dsInvDetail.Tables(4).Rows(0)("ModifyDate") & "")
                            txtSalesRetSMC.Text = Trim(dsInvDetail.Tables(4).Rows(0)("Smc") & "")
                            txtSalesCostPerGram.Text = Trim(dsInvDetail.Tables(4).Rows(0)("Rate") & "")
                            txtTotalSales.Text = Trim(dsInvDetail.Tables(4).Rows(0)("NetAmount") & "")
                            txtSalesRetPCS.Text = Trim(dsInvDetail.Tables(4).Rows(0)("Pcs") & "")
                        Else
                            txtSalesRetRefNo.Clear()
                            txtSalesRetDate.Clear()
                            txtSalesRetSMC.Clear()
                            txtSalesCostPerGram.Clear()
                            txtTotalSales.Clear()
                            txtSalesRetPCS.Clear()
                        End If
                        If dsInvDetail.Tables(5).Rows.Count > 0 Then
                            txtPurchaseRetNo.Text = Trim(dsInvDetail.Tables(4).Rows(0)("ReturnNo") & "")
                            txtPurchaseRetDate.Text = Trim(dsInvDetail.Tables(4).Rows(0)("ModifyDate") & "")
                            txtPurchasePMC.Text = Trim(dsInvDetail.Tables(4).Rows(0)("Mc") & "")
                            txtPurchaseRetCostperGram.Text = Trim(dsInvDetail.Tables(4).Rows(0)("CostPerGram") & "")
                            txtPurchaseRetTotal.Text = Trim(dsInvDetail.Tables(4).Rows(0)("TotalValue") & "")
                            txtPurchaseRetPCS.Text = Trim(dsInvDetail.Tables(4).Rows(0)("Pcs") & "")
                        Else
                            txtPurchaseRetNo.Clear()
                            txtPurchaseRetDate.Clear()
                            txtPurchasePMC.Clear()
                            txtPurchaseRetCostperGram.Clear()
                            txtPurchaseRetTotal.Clear()
                            txtPurchaseRetPCS.Clear()
                        End If
                        If dsInvDetail.Tables(6).Rows.Count > 0 Then
                            txtDestInventoryCode.Text = Trim(dsInvDetail.Tables(5).Rows(0)("DestInventoryCode") & "")
                            txtOriginalWeight.Text = Trim(dsInvDetail.Tables(5).Rows(0)("OriginalWeight") & "")
                            txtWeight1.Text = Trim(dsInvDetail.Tables(5).Rows(0)("Weight1") & "")
                            txtWeight2.Text = Trim(dsInvDetail.Tables(5).Rows(0)("Weight2") & "")
                        Else
                            txtDestInventoryCode.Clear()
                            txtOriginalWeight.Clear()
                            txtWeight1.Clear()
                            txtWeight2.Clear()
                        End If
                        'If dsInvDetail.Tables(7).Rows.Count > 0 Then
                        '    txtDestInventoryCode.Text = Trim(dsInvDetail.Tables(6).Rows(0)("DestInventoryCode") & "")
                        '    txtOriginalWeight.Text = Trim(dsInvDetail.Tables(6).Rows(0)("OriginalWeight") & "")
                        '    txtWeight1.Text = Trim(dsInvDetail.Tables(6).Rows(0)("Weight1") & "")
                        '    txtWeight2.Text = Trim(dsInvDetail.Tables(6).Rows(0)("Weight2") & "")
                        'Else
                        '    txtDestInventoryCode.Clear()
                        '    txtOriginalWeight.Clear()
                        '    txtWeight1.Clear()
                        '    txtWeight2.Clear()
                        'End If
                    End If
                    'code here for diplaying image
                    'sFileName = Application.StartupPath & "\Images\" & Trim(txtCatalogue.Text) & ".gif"
                    'If FSO.FileExists(sFileName) Then
                    '    PicCatalouge.Visible = True
                    '    PicCatalouge.Image = Image.FromFile(sFileName)
                    'Else
                    '    PicCatalouge.Visible = False
                    'End If
                    'FSO = Nothing

                    Dim dsInventory As New DataSet
                    Dim objClsInventory As New clsInventory
                    dsInventory = objClsInventory.GetInventoryImage(Trim(txtinvCode.Text))
                    If Not IsNothing(dsInventory) Then
                        If dsInventory.Tables("Inventory").Rows.Count > 0 Then
                            Byte2Image(picImage.Image, dsInventory.Tables("Inventory").Rows(0)("InventoryImage"))
                        End If
                    End If
                    Dim DsAdditional As DataSet
                    DsAdditional = objClsInventory.GetAdditionalInventoryDetail(txtinvCode.Text.Trim)
                    If DsAdditional.Tables("tblAddInventory").Rows.Count > 0 Then
                        txtDesc1.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Description1") & ""
                        txtDesc2.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Description2") & ""
                        txtDesc3.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Description3") & ""
                        txtDesc4.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Description4") & ""
                        txtCost1.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Cost1") & ""
                        txtCost2.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Cost2") & ""
                        txtCost3.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Cost3") & ""
                        txtCost4.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Cost4") & ""
                        txtSellingPrice1.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("SellingPrice1") & ""
                        txtSellingPrice2.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("SellingPrice2") & ""
                        txtSellingPrice3.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("SellingPrice3") & ""
                        txtSellingPrice4.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("SellingPrice4") & ""
                        txtNewDesc1.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Description1") & ""
                        txtNewDesc2.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Description2") & ""
                        txtNewDesc3.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Description3") & ""
                        txtNewDesc4.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Description4") & ""
                        txtNewCost1.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Cost1") & ""
                        txtNewCost2.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Cost2") & ""
                        txtNewCost3.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Cost3") & ""
                        txtNewCost4.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Cost4") & ""
                        txtNewSellingPrice1.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("SellingPrice1") & ""
                        txtNewSellingPrice2.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("SellingPrice2") & ""
                        txtNewSellingPrice3.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("SellingPrice3") & ""
                        txtNewSellingPrice4.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("SellingPrice4") & ""
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
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
    Private Sub txtinvCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtinvCode.LostFocus
        If (txtinvCode.Text.Trim) <> "" Then
            subGetInventoryDetails(txtinvCode.Text.Trim)
        End If
    End Sub

    Private Sub subGetInventoryDetails(ByVal sInvCode As String)
        Dim dsInvDetail As New DataSet
        Dim sFileName As String
        'Dim FSO As New Scripting.FileSystemObject
        Try
            Cursor = Cursors.WaitCursor
            dsInvDetail = objClsInventorySearch.GetInventoryDetails(sInvCode)
            If dsInvDetail.Tables.Count > 0 Then
                If dsInvDetail.Tables(0).Rows.Count > 0 Then
                    txtInvDescription.Text = Trim(dsInvDetail.Tables(0).Rows(0)("Description") & "")
                    txtCategory.Text = Trim(dsInvDetail.Tables(0).Rows(0)("CategoryCode") & "")
                    txtCatalogue.Text = Trim(dsInvDetail.Tables(0).Rows(0)("CatalogueCode") & "")
                    txtClass.Text = Trim(dsInvDetail.Tables(0).Rows(0)("ClassCode") & "")
                    txtLocation.Text = Trim(dsInvDetail.Tables(0).Rows(0)("LocationCode") & "")
                    txtWeight.Text = Trim(dsInvDetail.Tables(0).Rows(0)("Weight") & "")
                    txtGoldLoss.Text = Trim(dsInvDetail.Tables(0).Rows(0)("GoldLoss") & "")
                    '  txtPMC.Text = Trim(dsInvDetail.Tables(0).Rows(0)("PMC") & "")
                    txtSizeCode.Text = Trim(dsInvDetail.Tables(0).Rows(0)("SizeCode") & "")

                    txtSMC.Text = Trim(dsInvDetail.Tables(0).Rows(0)("SMC") & "")
                    txtCostPerGram.Text = Trim(dsInvDetail.Tables(0).Rows(0)("CostPerGram") & "")
                    txtQtyOnHand.Text = Trim(dsInvDetail.Tables(0).Rows(0)("QtyOnHand") & "")
                    If Trim(sCompanyShortCode) = "MD" Then
                        txtPMC.Text = Format(Trim(dsInvDetail.Tables(0).Rows(0)("PMC") & "") + (Trim(dsInvDetail.Tables(0).Rows(0)("PMC") & "") * 0.1), "0.00")
                        txtTotalCost.Text = Format(Trim(dsInvDetail.Tables(0).Rows(0)("TotalCost") & "") + (Trim(dsInvDetail.Tables(0).Rows(0)("TotalCost") & "") * 0.1), "0.00")
                    Else
                        txtPMC.Text = Trim(dsInvDetail.Tables(0).Rows(0)("PMC") & "")
                        txtTotalCost.Text = Trim(dsInvDetail.Tables(0).Rows(0)("TotalCost") & "")
                    End If
                    ''  txtTotalCost.Text = Trim(dsInvDetail.Tables(0).Rows(0)("TotalCost") & "")
                    txtDiamondDescription.Text = Trim(dsInvDetail.Tables(0).Rows(0)("DiamondDescription") & "")
                    txtStoneWeight.Text = Trim(dsInvDetail.Tables(0).Rows(0)("StoneWeight") & "")
                    txtRemarks.Text = Trim(dsInvDetail.Tables(0).Rows(0)("Remarks") & "")
                    txtSalesBillNo.Text = Trim(dsInvDetail.Tables(0).Rows(0)("SalesBillNo") & "")
                    txtSalesDate.Text = Trim(dsInvDetail.Tables(0).Rows(0)("SalesDate") & "")
                    txtSalesSMC.Text = Trim(dsInvDetail.Tables(0).Rows(0)("SalesMc") & "")
                    txtSalesCostPerGram.Text = Trim(dsInvDetail.Tables(0).Rows(0)("SalesCostPerGram") & "")
                    txtSalesPCS.Text = Trim(dsInvDetail.Tables(0).Rows(0)("QtySold") & "")
                    txtPurchaseSupplier.Text = Trim(dsInvDetail.Tables(0).Rows(0)("SupplierCode") & "")
                    txtPurchaseNo.Text = Trim(dsInvDetail.Tables(0).Rows(0)("PurchaseBillNo") & "")
                    txtPurchaseDate.Text = Format(CDate(dsInvDetail.Tables(0).Rows(0)("PurchaseDate") & ""), "dd/MM/yyyy")
                    'txtPMC.Text = Trim(dsInvDetail.Tables(0).Rows(0)("PMC") & "")
                    txtJobSheetNo.Text = Trim(dsInvDetail.Tables(0).Rows(0)("JobSheetNo") & "")
                    txtCreatedBy.Text = Trim(dsInvDetail.Tables(0).Rows(0)("CreateUser") & "")
                    txtGrossWeight.Text = Trim(dsInvDetail.Tables(0).Rows(0)("Weight") & "") - Trim(dsInvDetail.Tables(0).Rows(0)("StoneWeight") & "")
                Else
                    MsgBox("Inventory Not Found", MsgBoxStyle.Information)
                    Exit Sub
                End If
                If dsInvDetail.Tables(1).Rows.Count > 0 Then
                    txtJobSheetDate.Text = Format(CDate(dsInvDetail.Tables(1).Rows(0)("JobSheetDate") & ""), "dd/MM/yyyy")
                Else
                    txtJobSheetDate.Clear()
                End If
                If dsInvDetail.Tables(2).Rows.Count > 0 Then
                    txtSalesMan.Text = Trim(dsInvDetail.Tables(2).Rows(0)("SalesManName") & "")
                Else
                    txtSalesMan.Clear()
                End If
                'If dsInvDetail.Tables(3).Rows.Count > 0 Then
                '    txtTotalCost.Text = Format(Trim(dsInvDetail.Tables(3).Rows(0)("NetAmount") & ""), "0.00")
                'Else
                '    txtTotalCost.Text = 0.0
                'End If
                If dsInvDetail.Tables(4).Rows.Count > 0 Then
                    txtSalesRetRefNo.Text = Trim(dsInvDetail.Tables(4).Rows(0)("RefNo") & "")
                    txtSalesRetDate.Text = Trim(dsInvDetail.Tables(4).Rows(0)("ModifyDate") & "")
                    txtSalesRetSMC.Text = Trim(dsInvDetail.Tables(4).Rows(0)("Smc") & "")
                    txtSalesCostPerGram.Text = Trim(dsInvDetail.Tables(4).Rows(0)("Rate") & "")
                    txtTotalSales.Text = Trim(dsInvDetail.Tables(4).Rows(0)("NetAmount") & "")
                    txtSalesRetPCS.Text = Trim(dsInvDetail.Tables(4).Rows(0)("Pcs") & "")
                Else
                    txtSalesRetRefNo.Clear()
                    txtSalesRetDate.Clear()
                    txtSalesRetSMC.Clear()
                    txtSalesCostPerGram.Clear()
                    txtTotalSales.Clear()
                    txtSalesRetPCS.Clear()
                End If
                If dsInvDetail.Tables(5).Rows.Count > 0 Then
                    txtPurchaseRetNo.Text = Trim(dsInvDetail.Tables(5).Rows(0)("ReturnNo") & "")
                    txtPurchaseRetDate.Text = Trim(dsInvDetail.Tables(5).Rows(0)("ModifyDate") & "")
                    txtPurchasePMC.Text = Trim(dsInvDetail.Tables(5).Rows(0)("Mc") & "")
                    txtPurchaseRetCostperGram.Text = Trim(dsInvDetail.Tables(5).Rows(0)("CostPerGram") & "")
                    txtPurchaseRetTotal.Text = Trim(dsInvDetail.Tables(5).Rows(0)("TotalValue") & "")
                    txtPurchaseRetPCS.Text = Trim(dsInvDetail.Tables(5).Rows(0)("Pcs") & "")
                Else
                    txtPurchaseRetNo.Clear()
                    txtPurchaseRetDate.Clear()
                    txtPurchasePMC.Clear()
                    txtPurchaseRetCostperGram.Clear()
                    txtPurchaseRetTotal.Clear()
                    txtPurchaseRetPCS.Clear()
                End If
                If dsInvDetail.Tables(6).Rows.Count > 0 Then
                    txtDestInventoryCode.Text = Trim(dsInvDetail.Tables(6).Rows(0)("DestInventoryCode") & "")
                    txtOriginalWeight.Text = Trim(dsInvDetail.Tables(6).Rows(0)("OriginalWeight") & "")
                    txtWeight1.Text = Trim(dsInvDetail.Tables(6).Rows(0)("Weight1") & "")
                    txtWeight2.Text = Trim(dsInvDetail.Tables(6).Rows(0)("Weight2") & "")
                Else
                    txtDestInventoryCode.Clear()
                    txtOriginalWeight.Clear()
                    txtWeight1.Clear()
                    txtWeight2.Clear()
                End If

                If dsInvDetail.Tables(7).Rows.Count > 0 Then
                    txtMInventoryCode1.Text = Trim(dsInvDetail.Tables(7).Rows(0)("InventoryCode1") & "")
                    txtMInventoryCode2.Text = Trim(dsInvDetail.Tables(7).Rows(0)("InventoryCode2") & "")
                    txtMInventoryN.Text = Trim(dsInvDetail.Tables(7).Rows(0)("InventoryCodeN") & "")
                    txtMWeight1.Text = Trim(dsInvDetail.Tables(7).Rows(0)("Weight1") & "")
                    txtMWeight2.Text = Trim(dsInvDetail.Tables(7).Rows(0)("Weight2") & "")
                    txtMWeightN.Text = Trim(dsInvDetail.Tables(7).Rows(0)("WeightN") & "")
                Else
                    txtMInventoryCode1.Clear()
                    txtMInventoryCode2.Clear()
                    txtMInventoryN.Clear()
                    txtMWeight1.Clear()
                    txtMWeight2.Clear()
                    txtMWeightN.Clear()
                End If
                'dsCatalogue = objClsCatalogue.GetCatalogue(txtinvCode.Text.Trim)
                'If Not IsNothing(dsCatalogue) Then
                '    If dsCatalogue.Tables("Catalogue").Rows.Count > 0 Then
                '        Byte2ImageNEW(picImage.Image, dsCatalogue.Tables("Catalogue").Rows(0)("CatalogueImage"))
                '    End If
                'End If
                Dim dsInventory As New DataSet
                Dim objClsInventory As New clsInventory
                dsInventory = objClsInventory.GetInventoryImage(Trim(txtinvCode.Text))
                If Not IsNothing(dsInventory) Then
                    If dsInventory.Tables("Inventory").Rows.Count > 0 Then
                        Byte2Image(picImage.Image, dsInventory.Tables("Inventory").Rows(0)("InventoryImage"))
                    End If
                End If
                Dim DsAdditional As DataSet
                DsAdditional = objClsInventory.GetAdditionalInventoryDetail(txtinvCode.Text.Trim)
                If DsAdditional.Tables("tblAddInventory").Rows.Count > 0 Then
                    txtDesc1.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Description1") & ""
                    txtDesc2.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Description2") & ""
                    txtDesc3.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Description3") & ""
                    txtDesc4.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Description4") & ""
                    txtCost1.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Cost1") & ""
                    txtCost2.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Cost2") & ""
                    txtCost3.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Cost3") & ""
                    txtCost4.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Cost4") & ""
                    txtSellingPrice1.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("SellingPrice1") & ""
                    txtSellingPrice2.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("SellingPrice2") & ""
                    txtSellingPrice3.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("SellingPrice3") & ""
                    txtSellingPrice4.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("SellingPrice4") & ""
                    txtNewDesc1.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Description1") & ""
                    txtNewDesc2.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Description2") & ""
                    txtNewDesc3.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Description3") & ""
                    txtNewDesc4.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Description4") & ""
                    txtNewCost1.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Cost1") & ""
                    txtNewCost2.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Cost2") & ""
                    txtNewCost3.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Cost3") & ""
                    txtNewCost4.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Cost4") & ""
                    txtNewSellingPrice1.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("SellingPrice1") & ""
                    txtNewSellingPrice2.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("SellingPrice2") & ""
                    txtNewSellingPrice3.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("SellingPrice3") & ""
                    txtNewSellingPrice4.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("SellingPrice4") & ""
                End If
            End If
            'Durai Hide code here for diplaying image

            ' ''sFileName = Application.StartupPath & "\Images\" & Trim(txtCatalogue.Text) & ".gif"
            ' ''If FSO.FileExists(sFileName) Then
            ' ''    PicCatalouge.Visible = True
            ' ''    PicCatalouge.Image = Image.FromFile(sFileName)
            ' ''Else
            ' ''    PicCatalouge.Visible = False
            ' ''End If
            ' ''FSO = Nothing


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub





    Private Sub grdInventory_Click(sender As Object, e As System.EventArgs) Handles grdInventory.Click
        Dim sInventoryCode As String
        sInventoryCode = fnGetdgvText(grdInventory, grdInventory.CurrentRow.Index, 0)
        txtinvCode.Text = sInventoryCode.Trim
        txtinvCode_LostFocus(Nothing, Nothing)
    End Sub



    Private Sub grdInventory_SelectionChanged(sender As Object, e As System.EventArgs) Handles grdInventory.SelectionChanged
        Dim sInventoryCode As String
        sInventoryCode = fnGetdgvText(grdInventory, grdInventory.CurrentRow.Index, 0)
        txtinvCode.Text = sInventoryCode.Trim
        txtinvCode_LostFocus(Nothing, Nothing)
    End Sub

    Private Sub txtinvCode_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtinvCode.TextChanged

    End Sub

    Private Sub chkSplitMerge_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkSplitMerge.CheckedChanged
        If chkSplitMerge.Checked = True Then
            pnlSplitMerge.Visible = True
        Else
            pnlSplitMerge.Visible = False
        End If
    End Sub

    Private Sub chkAdditionalDesc_Click(sender As Object, e As System.EventArgs) Handles chkAdditionalDesc.Click
        If chkAdditionalDesc.Checked = True Then
            grpAddlDescription.Visible = True
        Else
            grpAddlDescription.Visible = False
        End If
    End Sub

    Private Sub cmdAddlDescOk_Click(sender As System.Object, e As System.EventArgs) Handles cmdAddlDescOk.Click
        grpAddlDescription.Visible = False
    End Sub

    Private Sub chkAdditionalDesc_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkAdditionalDesc.CheckedChanged

    End Sub
End Class
