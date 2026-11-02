Public Class frmInventoryML
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
    Friend WithEvents grdInventory As AxFPSpread.AxvaSpread
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents PicCatalouge As System.Windows.Forms.PictureBox
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
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents txtSize As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmInventoryML))
        Me.btnClose = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnRefresh = New System.Windows.Forms.Button
        Me.txtSearchWeight = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.optStock = New System.Windows.Forms.RadioButton
        Me.optSold = New System.Windows.Forms.RadioButton
        Me.btnSupplierSearch = New System.Windows.Forms.Button
        Me.txtSupplier = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnCatalogueSearch = New System.Windows.Forms.Button
        Me.txtCatalogueCode = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtDescription = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.grdInventory = New AxFPSpread.AxvaSpread
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.txtSize = New System.Windows.Forms.TextBox
        Me.Label46 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.txtRemarks = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.txtDiamondDescription = New System.Windows.Forms.TextBox
        Me.txtLocation = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtClass = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtCatalogue = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtCategory = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtInvDescription = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtinvCode = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.PicCatalouge = New System.Windows.Forms.PictureBox
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Label19 = New System.Windows.Forms.Label
        Me.txtSMC = New System.Windows.Forms.TextBox
        Me.txtPMC = New System.Windows.Forms.TextBox
        Me.txtStoneWeight = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtWeight = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.txtTotalSales = New System.Windows.Forms.TextBox
        Me.txtSalesPCS = New System.Windows.Forms.TextBox
        Me.txtSalesCostPerGram = New System.Windows.Forms.TextBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.txtSalesSMC = New System.Windows.Forms.TextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.txtSalesDate = New System.Windows.Forms.TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.txtSalesBillNo = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.txtSalesMan = New System.Windows.Forms.TextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.txtSalesRetToal = New System.Windows.Forms.TextBox
        Me.txtSalesRetPCS = New System.Windows.Forms.TextBox
        Me.txtSalesRetCostperGram = New System.Windows.Forms.TextBox
        Me.txtSalesRetSMC = New System.Windows.Forms.TextBox
        Me.Label29 = New System.Windows.Forms.Label
        Me.txtSalesRetDate = New System.Windows.Forms.TextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.txtSalesRetRefNo = New System.Windows.Forms.TextBox
        Me.Label31 = New System.Windows.Forms.Label
        Me.Label32 = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.Label34 = New System.Windows.Forms.Label
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.txtJobSheetDate = New System.Windows.Forms.TextBox
        Me.txtJobSheetNo = New System.Windows.Forms.TextBox
        Me.txtPurchaseSupplier = New System.Windows.Forms.TextBox
        Me.txtPurchasePMC = New System.Windows.Forms.TextBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.txtPurchaseDate = New System.Windows.Forms.TextBox
        Me.Label35 = New System.Windows.Forms.Label
        Me.txtPurchaseNo = New System.Windows.Forms.TextBox
        Me.Label36 = New System.Windows.Forms.Label
        Me.Label37 = New System.Windows.Forms.Label
        Me.Label38 = New System.Windows.Forms.Label
        Me.Label39 = New System.Windows.Forms.Label
        Me.Panel8 = New System.Windows.Forms.Panel
        Me.txtPurchaseRetTotal = New System.Windows.Forms.TextBox
        Me.txtPurchaseRetPCS = New System.Windows.Forms.TextBox
        Me.txtPurchaseRetCostperGram = New System.Windows.Forms.TextBox
        Me.txtPurchaseRetPMC = New System.Windows.Forms.TextBox
        Me.Label40 = New System.Windows.Forms.Label
        Me.txtPurchaseRetDate = New System.Windows.Forms.TextBox
        Me.Label41 = New System.Windows.Forms.Label
        Me.txtPurchaseRetNo = New System.Windows.Forms.TextBox
        Me.Label42 = New System.Windows.Forms.Label
        Me.Label43 = New System.Windows.Forms.Label
        Me.Label44 = New System.Windows.Forms.Label
        Me.Label45 = New System.Windows.Forms.Label
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.lblMsg = New System.Windows.Forms.Label
        Me.Panel10 = New System.Windows.Forms.Panel
        Me.txtTotalCost = New System.Windows.Forms.TextBox
        Me.txtQtyOnHand = New System.Windows.Forms.TextBox
        Me.txtCostPerGram = New System.Windows.Forms.TextBox
        Me.txtGoldLoss = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.pic = New System.Windows.Forms.PictureBox
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.grdInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(896, 592)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.TabIndex = 50
        Me.btnClose.Text = "Close(Esc)"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
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
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnRefresh.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.Image = CType(resources.GetObject("btnRefresh.Image"), System.Drawing.Image)
        Me.btnRefresh.Location = New System.Drawing.Point(418, 40)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(124, 20)
        Me.btnRefresh.TabIndex = 7
        Me.btnRefresh.Text = "Search(F5)"
        '
        'txtSearchWeight
        '
        Me.txtSearchWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearchWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchWeight.Location = New System.Drawing.Point(472, 8)
        Me.txtSearchWeight.MaxLength = 50
        Me.txtSearchWeight.Name = "txtSearchWeight"
        Me.txtSearchWeight.Size = New System.Drawing.Size(69, 20)
        Me.txtSearchWeight.TabIndex = 1
        Me.txtSearchWeight.Tag = "NE;"
        Me.txtSearchWeight.Text = ""
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
        Me.Panel2.Location = New System.Drawing.Point(546, -2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(123, 40)
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
        Me.optSold.Size = New System.Drawing.Size(48, 24)
        Me.optSold.TabIndex = 3
        Me.optSold.Text = "Sold"
        '
        'btnSupplierSearch
        '
        Me.btnSupplierSearch.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnSupplierSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSupplierSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSupplierSearch.Location = New System.Drawing.Point(382, 40)
        Me.btnSupplierSearch.Name = "btnSupplierSearch"
        Me.btnSupplierSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnSupplierSearch.TabIndex = 6
        Me.btnSupplierSearch.Text = "..."
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
        Me.txtSupplier.Text = ""
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
        Me.btnCatalogueSearch.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnCatalogueSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCatalogueSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCatalogueSearch.Location = New System.Drawing.Point(201, 40)
        Me.btnCatalogueSearch.Name = "btnCatalogueSearch"
        Me.btnCatalogueSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnCatalogueSearch.TabIndex = 4
        Me.btnCatalogueSearch.Text = "..."
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
        Me.txtCatalogueCode.Text = ""
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
        Me.txtDescription.Text = ""
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
        'grdInventory
        '
        Me.grdInventory.Location = New System.Drawing.Point(8, 88)
        Me.grdInventory.Name = "grdInventory"
        Me.grdInventory.OcxState = CType(resources.GetObject("grdInventory.OcxState"), System.Windows.Forms.AxHost.State)
        Me.grdInventory.Size = New System.Drawing.Size(360, 472)
        Me.grdInventory.TabIndex = 8
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.txtSize)
        Me.Panel3.Controls.Add(Me.Label46)
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
        Me.Panel3.Controls.Add(Me.PicCatalouge)
        Me.Panel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel3.Location = New System.Drawing.Point(376, 88)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(592, 176)
        Me.Panel3.TabIndex = 16
        '
        'txtSize
        '
        Me.txtSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSize.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSize.Location = New System.Drawing.Point(272, 152)
        Me.txtSize.MaxLength = 50
        Me.txtSize.Name = "txtSize"
        Me.txtSize.Size = New System.Drawing.Size(80, 20)
        Me.txtSize.TabIndex = 71
        Me.txtSize.Tag = "NE;"
        Me.txtSize.Text = ""
        '
        'Label46
        '
        Me.Label46.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.Location = New System.Drawing.Point(208, 152)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(64, 16)
        Me.Label46.TabIndex = 70
        Me.Label46.Text = "Length/Size"
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(368, 152)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(56, 16)
        Me.Label20.TabIndex = 69
        Me.Label20.Text = "Remarks"
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(432, 150)
        Me.txtRemarks.MaxLength = 50
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(152, 20)
        Me.txtRemarks.TabIndex = 16
        Me.txtRemarks.Tag = "NE;"
        Me.txtRemarks.Text = ""
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(4, 56)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(116, 16)
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
        Me.txtDiamondDescription.Size = New System.Drawing.Size(296, 20)
        Me.txtDiamondDescription.TabIndex = 11
        Me.txtDiamondDescription.Tag = "NE;"
        Me.txtDiamondDescription.Text = ""
        '
        'txtLocation
        '
        Me.txtLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.Location = New System.Drawing.Point(120, 150)
        Me.txtLocation.MaxLength = 50
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(80, 20)
        Me.txtLocation.TabIndex = 15
        Me.txtLocation.Tag = "NE;"
        Me.txtLocation.Text = ""
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(8, 150)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(48, 16)
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
        Me.txtClass.Text = ""
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(4, 128)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(72, 16)
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
        Me.txtCatalogue.Text = ""
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(4, 104)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(72, 16)
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
        Me.txtCategory.Text = ""
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(4, 80)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 16)
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
        Me.txtInvDescription.Size = New System.Drawing.Size(296, 20)
        Me.txtInvDescription.TabIndex = 10
        Me.txtInvDescription.Tag = "NE;"
        Me.txtInvDescription.Text = ""
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(4, 32)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 16)
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
        Me.txtinvCode.Text = ""
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(4, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(88, 16)
        Me.Label5.TabIndex = 46
        Me.Label5.Text = "Inventory Code"
        '
        'PicCatalouge
        '
        Me.PicCatalouge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PicCatalouge.Location = New System.Drawing.Point(430, 0)
        Me.PicCatalouge.Name = "PicCatalouge"
        Me.PicCatalouge.Size = New System.Drawing.Size(160, 104)
        Me.PicCatalouge.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PicCatalouge.TabIndex = 44
        Me.PicCatalouge.TabStop = False
        Me.PicCatalouge.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Label19)
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
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(150, 2)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(72, 16)
        Me.Label19.TabIndex = 66
        Me.Label19.Text = "Stone Weight"
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
        Me.txtSMC.Text = ""
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
        Me.txtPMC.Text = ""
        '
        'txtStoneWeight
        '
        Me.txtStoneWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStoneWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStoneWeight.Location = New System.Drawing.Point(150, 19)
        Me.txtStoneWeight.MaxLength = 50
        Me.txtStoneWeight.Name = "txtStoneWeight"
        Me.txtStoneWeight.Size = New System.Drawing.Size(128, 20)
        Me.txtStoneWeight.TabIndex = 18
        Me.txtStoneWeight.Tag = "NE;"
        Me.txtStoneWeight.Text = ""
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.txtWeight.Size = New System.Drawing.Size(128, 20)
        Me.txtWeight.TabIndex = 17
        Me.txtWeight.Tag = "NE;"
        Me.txtWeight.Text = ""
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(8, 39)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(56, 16)
        Me.Label14.TabIndex = 50
        Me.Label14.Text = "PMC"
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(8, 2)
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
        Me.txtTotalSales.Text = ""
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
        Me.txtSalesPCS.Text = ""
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
        Me.txtSalesCostPerGram.Text = ""
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
        Me.txtSalesSMC.Text = ""
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
        Me.txtSalesDate.Text = ""
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
        Me.txtSalesBillNo.Text = ""
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
        Me.txtSalesMan.Text = ""
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
        Me.txtSalesRetToal.Text = ""
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
        Me.txtSalesRetPCS.Text = ""
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
        Me.txtSalesRetCostperGram.Text = ""
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
        Me.txtSalesRetSMC.Text = ""
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
        Me.txtSalesRetDate.Text = ""
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
        Me.txtSalesRetRefNo.Text = ""
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
        Me.txtJobSheetDate.Text = ""
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
        Me.txtJobSheetNo.Text = ""
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
        Me.txtPurchaseSupplier.Text = ""
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
        Me.txtPurchasePMC.Text = ""
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
        Me.txtPurchaseDate.Text = ""
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
        Me.txtPurchaseNo.Text = ""
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
        Me.txtPurchaseRetTotal.Text = ""
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
        Me.txtPurchaseRetPCS.Text = ""
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
        Me.txtPurchaseRetCostperGram.Text = ""
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
        Me.txtPurchaseRetPMC.Text = ""
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
        Me.txtPurchaseRetDate.Text = ""
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
        Me.txtPurchaseRetNo.Text = ""
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
        Me.txtTotalCost.Text = ""
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
        Me.txtQtyOnHand.Text = ""
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
        Me.txtCostPerGram.Text = ""
        '
        'txtGoldLoss
        '
        Me.txtGoldLoss.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldLoss.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoldLoss.Location = New System.Drawing.Point(8, 19)
        Me.txtGoldLoss.MaxLength = 50
        Me.txtGoldLoss.Name = "txtGoldLoss"
        Me.txtGoldLoss.Size = New System.Drawing.Size(128, 20)
        Me.txtGoldLoss.TabIndex = 21
        Me.txtGoldLoss.Tag = "NE;"
        Me.txtGoldLoss.Text = ""
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
        Me.Label15.Size = New System.Drawing.Size(64, 16)
        Me.Label15.TabIndex = 62
        Me.Label15.Text = "Gold Loss"
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(0, 0)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(210, 75)
        Me.pic.TabIndex = 112
        Me.pic.TabStop = False
        '
        'frmInventoryML
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(978, 624)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel10)
        Me.Controls.Add(Me.lblMsg)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.grdInventory)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.Panel9)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmInventoryML"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Inventory Search"
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.grdInventory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.ResumeLayout(False)

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
        subGridSetting(grdInventory)
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
            sQry = " Select InventoryCode, Description, Weight from tblInventory "
            'check for description 
            If txtDescription.Text.Trim <> "" Then
                sQry = sQry & " Where Description like " & fncFormatSQLText(txtDescription.Text.Trim & "%")
                sCondition = txtDescription.Text.Trim
            End If
            'check for weight
            If Val(txtSearchWeight.Text.Trim) <> 0 Then
                If Len(sCondition) > 0 Then
                    sQry = sQry & " and  Weight between " & Val(txtSearchWeight.Text.Trim) - 0.5 & " and " & Val(txtSearchWeight.Text.Trim) + 0.5
                Else
                    sQry = sQry & " Where  Weight between " & Val(txtSearchWeight.Text.Trim) - 0.5 & " and " & Val(txtSearchWeight.Text.Trim) + 0.5
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
                    sQry = sQry & " and QtyOnHand > 0"
                Else
                    sQry = sQry & " where QtyOnHand > 0"
                End If
                lblMsg.Text = "Searh by Stock in Hand."
            ElseIf optSold.Checked = True Then
                If Len(sCondition) > 0 Then
                    sQry = sQry & " and QtySold > 0"
                Else
                    sQry = sQry & " where QtySold > 0"
                End If
                lblMsg.Text = "Searh by Stock Sold."
            End If
            sQry = sQry & " order by Description "
            dsInv = objClsInventorySearch.FetchInventoryDetails(sQry)
            grdInventory.MaxRows = 0
            If dsInv.Tables("Inventory").Rows.Count > 0 Then
                For iCnt = 0 To dsInv.Tables("Inventory").Rows.Count - 1
                    grdInventory.MaxRows = grdInventory.MaxRows + 1
                    SetText(grdInventory, iCnt + 1, InvCode, dsInv.Tables("Inventory").Rows(iCnt)("InventoryCode"))
                    SetText(grdInventory, iCnt + 1, InvDescription, dsInv.Tables("Inventory").Rows(iCnt)("Description"))
                    SetText(grdInventory, iCnt + 1, InvWeight, dsInv.Tables("Inventory").Rows(iCnt)("Weight"))
                Next
                grdInventory.Focus()
                subGetInvDetails(grdInventory.ActiveRow)
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

    Private Sub grdInventory_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_ClickEvent) Handles grdInventory.ClickEvent
        subGetInvDetails(e.row)
    End Sub

    Private Sub subGetInvDetails(ByVal iRow As Integer)
        Dim dsInvDetail As New DataSet
        Dim sFileName As String
        Dim FSO As New Scripting.FileSystemObject
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
                            txtSize.Text = Trim(dsInvDetail.Tables(0).Rows(0)("Size") & "")
                            txtWeight.Text = Trim(dsInvDetail.Tables(0).Rows(0)("Weight") & "")
                            txtGoldLoss.Text = Trim(dsInvDetail.Tables(0).Rows(0)("GoldLoss") & "")
                            txtPMC.Text = Trim(dsInvDetail.Tables(0).Rows(0)("PMC") & "")
                            txtSMC.Text = Trim(dsInvDetail.Tables(0).Rows(0)("SMC") & "")
                            txtCostPerGram.Text = Trim(dsInvDetail.Tables(0).Rows(0)("CostPerGram") & "")
                            txtQtyOnHand.Text = Trim(dsInvDetail.Tables(0).Rows(0)("QtyOnHand") & "")
                            txtTotalCost.Text = Trim(dsInvDetail.Tables(0).Rows(0)("TotalCost") & "")
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
                    End If
                    'code here for diplaying image
                    sFileName = Application.StartupPath & "\Images\" & Trim(txtCatalogue.Text) & ".gif"
                    If FSO.FileExists(sFileName) Then
                        PicCatalouge.Visible = True
                        PicCatalouge.Image = Image.FromFile(sFileName)
                    Else
                        PicCatalouge.Visible = False
                    End If
                    FSO = Nothing
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdInventory_LeaveRow(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_LeaveRowEvent) Handles grdInventory.LeaveRow
        subGetInvDetails(e.newRow)
    End Sub

    Private Sub txtinvCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtinvCode.LostFocus
        If (txtinvCode.Text.Trim) <> "" Then
            subGetInventoryDetails(txtinvCode.Text.Trim)
        End If
    End Sub

    Private Sub subGetInventoryDetails(ByVal sInvCode As String)
        Dim dsInvDetail As New DataSet
        Dim sFileName As String
        Dim FSO As New Scripting.FileSystemObject
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
                    txtPMC.Text = Trim(dsInvDetail.Tables(0).Rows(0)("PMC") & "")
                    txtSMC.Text = Trim(dsInvDetail.Tables(0).Rows(0)("SMC") & "")
                    txtCostPerGram.Text = Trim(dsInvDetail.Tables(0).Rows(0)("CostPerGram") & "")
                    txtQtyOnHand.Text = Trim(dsInvDetail.Tables(0).Rows(0)("QtyOnHand") & "")
                    txtTotalCost.Text = Trim(dsInvDetail.Tables(0).Rows(0)("TotalCost") & "")
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
            End If
            'code here for diplaying image
            sFileName = Application.StartupPath & "\Images\" & Trim(txtCatalogue.Text) & ".gif"
            If FSO.FileExists(sFileName) Then
                PicCatalouge.Visible = True
                PicCatalouge.Image = Image.FromFile(sFileName)
            Else
                PicCatalouge.Visible = False
            End If
            FSO = Nothing


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub



    Private Sub grdInventory_Advance(ByVal sender As System.Object, ByVal e As AxFPSpread._DSpreadEvents_AdvanceEvent) Handles grdInventory.Advance

    End Sub

    Private Sub grdInventory_ColWidthChange(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_ColWidthChangeEvent) Handles grdInventory.ColWidthChange

    End Sub
End Class
