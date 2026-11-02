Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmMergeInventory
    Inherits System.Windows.Forms.Form
    Dim objClsInvMerge As New clsMergeInventory
    Dim sClass As String
    Dim sMcCost As Double
    Dim sMcPrice As Double
    Dim sTotalCost As Double
    Dim MCTitle As String
    Dim dWeight1 As Double
    Dim dWeight2 As Double
    Dim dMcCost1 As Double
    Dim dMcCost2 As Double
    Dim dMcPrice1 As Double
    Dim dMcPrice2 As Double
    Dim dOriginalPMC As Double
    Dim dOriginalSMC As Double
    Dim dOriginalGoldLoss As Double
    Dim dOriginalTotalCost As Double
    Dim oTconn As ADODB.Connection
    Dim dsT As DataSet
    Dim dTotalCost1 As Double
    Dim dTotalcost2 As Double
    Dim sTrayId As String
    Dim sCatalogueCode As String
    Dim sCategoryCode As String
    Dim dCostPerGram As Double
    Dim dPMC As Double
    Dim dSMC As Double
    Dim dPCS As Double
    Dim dGoldLoss As Double
    Dim dStoneWeight As Double

    Dim sJobsheetNo As String
    Dim sPurchaseBillNo As String
    Dim dPurchaseDate As Date
    Dim dCreateDate As Date
    Dim sSupplierCode As String
    Dim sSalesBillNo As String
    Dim sDiamondDescription As String
    Dim dTotalCost As Double
    Dim sClassCode As String
    Dim sPMCType As String
    Dim sSMCType As String
    Dim sRemarks As String
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents cboCatalogue As System.Windows.Forms.ComboBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents btnSizeCode As System.Windows.Forms.Button
    Friend WithEvents txtSizeCode As System.Windows.Forms.TextBox
    Dim masPriceCode$(9)
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents txtSizeCode1 As System.Windows.Forms.TextBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents txtSizeCode2 As System.Windows.Forms.TextBox
    Friend WithEvents txtMergeWeight As System.Windows.Forms.TextBox
    Friend WithEvents chkMerge As System.Windows.Forms.CheckBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents chkAdditionalDesc As System.Windows.Forms.CheckBox
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
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents cmdAddlDescOk As System.Windows.Forms.Button
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Dim sSizeType As String


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
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtWeight1 As System.Windows.Forms.TextBox
    Friend WithEvents txtDescription1 As System.Windows.Forms.TextBox
    Friend WithEvents txtInventoryCode1 As System.Windows.Forms.TextBox
    Friend WithEvents btnInventorySearch1 As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtInventoryCode As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtStoneWeight1 As System.Windows.Forms.TextBox
    Friend WithEvents txtStoneWeight2 As System.Windows.Forms.TextBox
    Friend WithEvents txtWeight2 As System.Windows.Forms.TextBox
    Friend WithEvents txtInventoryCode2 As System.Windows.Forms.TextBox
    Friend WithEvents btnInventorySearch2 As System.Windows.Forms.Button
    Friend WithEvents txtDiamondDescription1 As System.Windows.Forms.TextBox
    Friend WithEvents txtCostPerGram1 As System.Windows.Forms.TextBox
    Friend WithEvents txtSMC1 As System.Windows.Forms.TextBox
    Friend WithEvents txtPMC1 As System.Windows.Forms.TextBox
    Friend WithEvents txtPCS1 As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalCost1 As System.Windows.Forms.TextBox
    Friend WithEvents txtGoldLoss1 As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalCost2 As System.Windows.Forms.TextBox
    Friend WithEvents txtGoldLoss2 As System.Windows.Forms.TextBox
    Friend WithEvents txtPCS2 As System.Windows.Forms.TextBox
    Friend WithEvents txtSMC2 As System.Windows.Forms.TextBox
    Friend WithEvents txtPMC2 As System.Windows.Forms.TextBox
    Friend WithEvents txtCostPerGram2 As System.Windows.Forms.TextBox
    Friend WithEvents txtDiamondDescription2 As System.Windows.Forms.TextBox
    Friend WithEvents txtDescription2 As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents txtSMC As System.Windows.Forms.TextBox
    Friend WithEvents txtPMC As System.Windows.Forms.TextBox
    Friend WithEvents txtGoldLoss As System.Windows.Forms.TextBox
    Friend WithEvents txtCostPerGram As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalCost As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents txtStoneWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtPCS As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtDiamondDescription As System.Windows.Forms.TextBox
    Friend WithEvents cboCategory As System.Windows.Forms.ComboBox
    Friend WithEvents btnBarcode As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMergeInventory))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtSizeCode1 = New System.Windows.Forms.TextBox()
        Me.txtTotalCost1 = New System.Windows.Forms.TextBox()
        Me.txtGoldLoss1 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtPCS1 = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtSMC1 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtPMC1 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCostPerGram1 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtDiamondDescription1 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnInventorySearch1 = New System.Windows.Forms.Button()
        Me.txtStoneWeight1 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtWeight1 = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtDescription1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtInventoryCode1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtMergeWeight = New System.Windows.Forms.TextBox()
        Me.chkMerge = New System.Windows.Forms.CheckBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtSizeCode2 = New System.Windows.Forms.TextBox()
        Me.txtTotalCost2 = New System.Windows.Forms.TextBox()
        Me.txtGoldLoss2 = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtPCS2 = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtSMC2 = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtPMC2 = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtCostPerGram2 = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtDiamondDescription2 = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.btnInventorySearch2 = New System.Windows.Forms.Button()
        Me.txtStoneWeight2 = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtWeight2 = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtDescription2 = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtInventoryCode2 = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.btnSizeCode = New System.Windows.Forms.Button()
        Me.txtSizeCode = New System.Windows.Forms.TextBox()
        Me.cboCatalogue = New System.Windows.Forms.ComboBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.cboCategory = New System.Windows.Forms.ComboBox()
        Me.txtDiamondDescription = New System.Windows.Forms.TextBox()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.txtPCS = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.txtStoneWeight = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.txtTotalCost = New System.Windows.Forms.TextBox()
        Me.txtSMC = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtPMC = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtGoldLoss = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtCostPerGram = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtWeight = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtInventoryCode = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnBarcode = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.chkAdditionalDesc = New System.Windows.Forms.CheckBox()
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
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.cmdAddlDescOk = New System.Windows.Forms.Button()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpAddlDescription.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label37)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.txtSizeCode1)
        Me.Panel1.Controls.Add(Me.txtTotalCost1)
        Me.Panel1.Controls.Add(Me.txtGoldLoss1)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.txtPCS1)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.txtSMC1)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.txtPMC1)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.txtCostPerGram1)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txtDiamondDescription1)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.btnInventorySearch1)
        Me.Panel1.Controls.Add(Me.txtStoneWeight1)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.txtWeight1)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtDescription1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtInventoryCode1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(20, 109)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(398, 208)
        Me.Panel1.TabIndex = 143
        '
        'Label37
        '
        Me.Label37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.Color.Black
        Me.Label37.Location = New System.Drawing.Point(9, 182)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(89, 16)
        Me.Label37.TabIndex = 412
        Me.Label37.Text = "Size"
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(208, 152)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(80, 16)
        Me.Label11.TabIndex = 119
        Me.Label11.Text = "Total Cost"
        '
        'txtSizeCode1
        '
        Me.txtSizeCode1.BackColor = System.Drawing.Color.White
        Me.txtSizeCode1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSizeCode1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSizeCode1.ForeColor = System.Drawing.Color.Navy
        Me.txtSizeCode1.Location = New System.Drawing.Point(104, 176)
        Me.txtSizeCode1.MaxLength = 21
        Me.txtSizeCode1.Name = "txtSizeCode1"
        Me.txtSizeCode1.ReadOnly = True
        Me.txtSizeCode1.Size = New System.Drawing.Size(97, 22)
        Me.txtSizeCode1.TabIndex = 411
        '
        'txtTotalCost1
        '
        Me.txtTotalCost1.BackColor = System.Drawing.Color.White
        Me.txtTotalCost1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalCost1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalCost1.Location = New System.Drawing.Point(288, 152)
        Me.txtTotalCost1.MaxLength = 20
        Me.txtTotalCost1.Name = "txtTotalCost1"
        Me.txtTotalCost1.ReadOnly = True
        Me.txtTotalCost1.Size = New System.Drawing.Size(97, 22)
        Me.txtTotalCost1.TabIndex = 117
        Me.txtTotalCost1.Tag = "NE;"
        '
        'txtGoldLoss1
        '
        Me.txtGoldLoss1.BackColor = System.Drawing.Color.White
        Me.txtGoldLoss1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldLoss1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoldLoss1.ForeColor = System.Drawing.Color.Black
        Me.txtGoldLoss1.Location = New System.Drawing.Point(104, 152)
        Me.txtGoldLoss1.MaxLength = 20
        Me.txtGoldLoss1.Name = "txtGoldLoss1"
        Me.txtGoldLoss1.ReadOnly = True
        Me.txtGoldLoss1.Size = New System.Drawing.Size(97, 22)
        Me.txtGoldLoss1.TabIndex = 116
        Me.txtGoldLoss1.Tag = "NE;"
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(8, 160)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(64, 16)
        Me.Label10.TabIndex = 118
        Me.Label10.Text = "Gold Loss"
        '
        'txtPCS1
        '
        Me.txtPCS1.BackColor = System.Drawing.Color.White
        Me.txtPCS1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPCS1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPCS1.Location = New System.Drawing.Point(288, 104)
        Me.txtPCS1.MaxLength = 20
        Me.txtPCS1.Name = "txtPCS1"
        Me.txtPCS1.ReadOnly = True
        Me.txtPCS1.Size = New System.Drawing.Size(97, 22)
        Me.txtPCS1.TabIndex = 114
        Me.txtPCS1.Tag = "NE;"
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(208, 104)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(80, 16)
        Me.Label9.TabIndex = 115
        Me.Label9.Text = "PCS"
        '
        'txtSMC1
        '
        Me.txtSMC1.BackColor = System.Drawing.Color.White
        Me.txtSMC1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSMC1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMC1.Location = New System.Drawing.Point(288, 128)
        Me.txtSMC1.MaxLength = 20
        Me.txtSMC1.Name = "txtSMC1"
        Me.txtSMC1.ReadOnly = True
        Me.txtSMC1.Size = New System.Drawing.Size(97, 22)
        Me.txtSMC1.TabIndex = 111
        Me.txtSMC1.Tag = "NE;"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(208, 128)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 16)
        Me.Label6.TabIndex = 113
        Me.Label6.Text = "SMC"
        '
        'txtPMC1
        '
        Me.txtPMC1.BackColor = System.Drawing.Color.White
        Me.txtPMC1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPMC1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPMC1.ForeColor = System.Drawing.Color.Black
        Me.txtPMC1.Location = New System.Drawing.Point(104, 128)
        Me.txtPMC1.MaxLength = 20
        Me.txtPMC1.Name = "txtPMC1"
        Me.txtPMC1.ReadOnly = True
        Me.txtPMC1.Size = New System.Drawing.Size(97, 22)
        Me.txtPMC1.TabIndex = 110
        Me.txtPMC1.Tag = "NE;"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(8, 136)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(64, 16)
        Me.Label7.TabIndex = 112
        Me.Label7.Text = "PMC"
        '
        'txtCostPerGram1
        '
        Me.txtCostPerGram1.BackColor = System.Drawing.Color.White
        Me.txtCostPerGram1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCostPerGram1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostPerGram1.ForeColor = System.Drawing.Color.Black
        Me.txtCostPerGram1.Location = New System.Drawing.Point(104, 104)
        Me.txtCostPerGram1.MaxLength = 20
        Me.txtCostPerGram1.Name = "txtCostPerGram1"
        Me.txtCostPerGram1.ReadOnly = True
        Me.txtCostPerGram1.Size = New System.Drawing.Size(97, 22)
        Me.txtCostPerGram1.TabIndex = 108
        Me.txtCostPerGram1.Tag = "NE;"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(8, 112)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 16)
        Me.Label4.TabIndex = 109
        Me.Label4.Text = "Cost Per Gram"
        '
        'txtDiamondDescription1
        '
        Me.txtDiamondDescription1.BackColor = System.Drawing.Color.White
        Me.txtDiamondDescription1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDiamondDescription1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiamondDescription1.ForeColor = System.Drawing.Color.Black
        Me.txtDiamondDescription1.Location = New System.Drawing.Point(104, 56)
        Me.txtDiamondDescription1.MaxLength = 20
        Me.txtDiamondDescription1.Name = "txtDiamondDescription1"
        Me.txtDiamondDescription1.ReadOnly = True
        Me.txtDiamondDescription1.Size = New System.Drawing.Size(280, 22)
        Me.txtDiamondDescription1.TabIndex = 106
        Me.txtDiamondDescription1.Tag = "NE;"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 16)
        Me.Label3.TabIndex = 107
        Me.Label3.Text = "Diamond Desc."
        '
        'btnInventorySearch1
        '
        Me.btnInventorySearch1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnInventorySearch1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInventorySearch1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInventorySearch1.Location = New System.Drawing.Point(199, 8)
        Me.btnInventorySearch1.Name = "btnInventorySearch1"
        Me.btnInventorySearch1.Size = New System.Drawing.Size(24, 22)
        Me.btnInventorySearch1.TabIndex = 105
        Me.btnInventorySearch1.Text = "..."
        Me.btnInventorySearch1.UseVisualStyleBackColor = False
        Me.btnInventorySearch1.Visible = False
        '
        'txtStoneWeight1
        '
        Me.txtStoneWeight1.BackColor = System.Drawing.Color.White
        Me.txtStoneWeight1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStoneWeight1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStoneWeight1.Location = New System.Drawing.Point(288, 80)
        Me.txtStoneWeight1.MaxLength = 20
        Me.txtStoneWeight1.Name = "txtStoneWeight1"
        Me.txtStoneWeight1.ReadOnly = True
        Me.txtStoneWeight1.Size = New System.Drawing.Size(97, 22)
        Me.txtStoneWeight1.TabIndex = 4
        Me.txtStoneWeight1.Tag = "NE;"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(208, 80)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(80, 16)
        Me.Label8.TabIndex = 56
        Me.Label8.Text = "Stone Weight"
        '
        'txtWeight1
        '
        Me.txtWeight1.BackColor = System.Drawing.Color.White
        Me.txtWeight1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWeight1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeight1.ForeColor = System.Drawing.Color.Black
        Me.txtWeight1.Location = New System.Drawing.Point(104, 80)
        Me.txtWeight1.MaxLength = 20
        Me.txtWeight1.Name = "txtWeight1"
        Me.txtWeight1.ReadOnly = True
        Me.txtWeight1.Size = New System.Drawing.Size(97, 22)
        Me.txtWeight1.TabIndex = 3
        Me.txtWeight1.Tag = "NE;"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 88)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 16)
        Me.Label5.TabIndex = 52
        Me.Label5.Text = "Weight"
        '
        'txtDescription1
        '
        Me.txtDescription1.BackColor = System.Drawing.Color.White
        Me.txtDescription1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescription1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription1.ForeColor = System.Drawing.Color.Black
        Me.txtDescription1.Location = New System.Drawing.Point(104, 32)
        Me.txtDescription1.MaxLength = 20
        Me.txtDescription1.Name = "txtDescription1"
        Me.txtDescription1.ReadOnly = True
        Me.txtDescription1.Size = New System.Drawing.Size(280, 22)
        Me.txtDescription1.TabIndex = 2
        Me.txtDescription1.Tag = "NE;"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 16)
        Me.Label2.TabIndex = 40
        Me.Label2.Text = "Description "
        '
        'txtInventoryCode1
        '
        Me.txtInventoryCode1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInventoryCode1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInventoryCode1.Location = New System.Drawing.Point(103, 8)
        Me.txtInventoryCode1.MaxLength = 20
        Me.txtInventoryCode1.Name = "txtInventoryCode1"
        Me.txtInventoryCode1.Size = New System.Drawing.Size(97, 22)
        Me.txtInventoryCode1.TabIndex = 0
        Me.txtInventoryCode1.Tag = "NE;"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 16)
        Me.Label1.TabIndex = 38
        Me.Label1.Text = "Inventory Code"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.txtMergeWeight)
        Me.Panel2.Controls.Add(Me.chkMerge)
        Me.Panel2.Controls.Add(Me.Label38)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Controls.Add(Me.txtSizeCode2)
        Me.Panel2.Controls.Add(Me.txtTotalCost2)
        Me.Panel2.Controls.Add(Me.txtGoldLoss2)
        Me.Panel2.Controls.Add(Me.Label13)
        Me.Panel2.Controls.Add(Me.txtPCS2)
        Me.Panel2.Controls.Add(Me.Label14)
        Me.Panel2.Controls.Add(Me.txtSMC2)
        Me.Panel2.Controls.Add(Me.Label15)
        Me.Panel2.Controls.Add(Me.txtPMC2)
        Me.Panel2.Controls.Add(Me.Label16)
        Me.Panel2.Controls.Add(Me.txtCostPerGram2)
        Me.Panel2.Controls.Add(Me.Label17)
        Me.Panel2.Controls.Add(Me.txtDiamondDescription2)
        Me.Panel2.Controls.Add(Me.Label18)
        Me.Panel2.Controls.Add(Me.btnInventorySearch2)
        Me.Panel2.Controls.Add(Me.txtStoneWeight2)
        Me.Panel2.Controls.Add(Me.Label19)
        Me.Panel2.Controls.Add(Me.txtWeight2)
        Me.Panel2.Controls.Add(Me.Label20)
        Me.Panel2.Controls.Add(Me.txtDescription2)
        Me.Panel2.Controls.Add(Me.Label21)
        Me.Panel2.Controls.Add(Me.txtInventoryCode2)
        Me.Panel2.Controls.Add(Me.Label22)
        Me.Panel2.Location = New System.Drawing.Point(418, 109)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(398, 208)
        Me.Panel2.TabIndex = 144
        '
        'txtMergeWeight
        '
        Me.txtMergeWeight.BackColor = System.Drawing.Color.White
        Me.txtMergeWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMergeWeight.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMergeWeight.ForeColor = System.Drawing.Color.Black
        Me.txtMergeWeight.Location = New System.Drawing.Point(288, 176)
        Me.txtMergeWeight.MaxLength = 20
        Me.txtMergeWeight.Name = "txtMergeWeight"
        Me.txtMergeWeight.Size = New System.Drawing.Size(97, 22)
        Me.txtMergeWeight.TabIndex = 414
        Me.txtMergeWeight.Tag = "NE;"
        '
        'chkMerge
        '
        Me.chkMerge.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMerge.ForeColor = System.Drawing.Color.Black
        Me.chkMerge.Location = New System.Drawing.Point(175, 173)
        Me.chkMerge.Name = "chkMerge"
        Me.chkMerge.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkMerge.Size = New System.Drawing.Size(107, 25)
        Me.chkMerge.TabIndex = 413
        Me.chkMerge.Text = "Merge Weight"
        '
        'Label38
        '
        Me.Label38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.Color.Black
        Me.Label38.Location = New System.Drawing.Point(8, 180)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(89, 16)
        Me.Label38.TabIndex = 412
        Me.Label38.Text = "Size"
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(208, 152)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(80, 16)
        Me.Label12.TabIndex = 119
        Me.Label12.Text = "Total Cost"
        '
        'txtSizeCode2
        '
        Me.txtSizeCode2.BackColor = System.Drawing.Color.White
        Me.txtSizeCode2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSizeCode2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSizeCode2.ForeColor = System.Drawing.Color.Navy
        Me.txtSizeCode2.Location = New System.Drawing.Point(104, 176)
        Me.txtSizeCode2.MaxLength = 21
        Me.txtSizeCode2.Name = "txtSizeCode2"
        Me.txtSizeCode2.ReadOnly = True
        Me.txtSizeCode2.Size = New System.Drawing.Size(70, 22)
        Me.txtSizeCode2.TabIndex = 411
        '
        'txtTotalCost2
        '
        Me.txtTotalCost2.BackColor = System.Drawing.Color.White
        Me.txtTotalCost2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalCost2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalCost2.Location = New System.Drawing.Point(288, 152)
        Me.txtTotalCost2.MaxLength = 20
        Me.txtTotalCost2.Name = "txtTotalCost2"
        Me.txtTotalCost2.ReadOnly = True
        Me.txtTotalCost2.Size = New System.Drawing.Size(97, 22)
        Me.txtTotalCost2.TabIndex = 117
        Me.txtTotalCost2.Tag = "NE;"
        '
        'txtGoldLoss2
        '
        Me.txtGoldLoss2.BackColor = System.Drawing.Color.White
        Me.txtGoldLoss2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldLoss2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoldLoss2.ForeColor = System.Drawing.Color.Black
        Me.txtGoldLoss2.Location = New System.Drawing.Point(104, 152)
        Me.txtGoldLoss2.MaxLength = 20
        Me.txtGoldLoss2.Name = "txtGoldLoss2"
        Me.txtGoldLoss2.ReadOnly = True
        Me.txtGoldLoss2.Size = New System.Drawing.Size(97, 22)
        Me.txtGoldLoss2.TabIndex = 116
        Me.txtGoldLoss2.Tag = "NE;"
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(8, 160)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(64, 16)
        Me.Label13.TabIndex = 118
        Me.Label13.Text = "Gold Loss"
        '
        'txtPCS2
        '
        Me.txtPCS2.BackColor = System.Drawing.Color.White
        Me.txtPCS2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPCS2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPCS2.Location = New System.Drawing.Point(288, 104)
        Me.txtPCS2.MaxLength = 20
        Me.txtPCS2.Name = "txtPCS2"
        Me.txtPCS2.ReadOnly = True
        Me.txtPCS2.Size = New System.Drawing.Size(97, 22)
        Me.txtPCS2.TabIndex = 114
        Me.txtPCS2.Tag = "NE;"
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(208, 104)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(80, 16)
        Me.Label14.TabIndex = 115
        Me.Label14.Text = "PCS"
        '
        'txtSMC2
        '
        Me.txtSMC2.BackColor = System.Drawing.Color.White
        Me.txtSMC2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSMC2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMC2.Location = New System.Drawing.Point(288, 128)
        Me.txtSMC2.MaxLength = 20
        Me.txtSMC2.Name = "txtSMC2"
        Me.txtSMC2.ReadOnly = True
        Me.txtSMC2.Size = New System.Drawing.Size(97, 22)
        Me.txtSMC2.TabIndex = 111
        Me.txtSMC2.Tag = "NE;"
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(208, 128)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(80, 16)
        Me.Label15.TabIndex = 113
        Me.Label15.Text = "SMC"
        '
        'txtPMC2
        '
        Me.txtPMC2.BackColor = System.Drawing.Color.White
        Me.txtPMC2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPMC2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPMC2.ForeColor = System.Drawing.Color.Black
        Me.txtPMC2.Location = New System.Drawing.Point(104, 128)
        Me.txtPMC2.MaxLength = 20
        Me.txtPMC2.Name = "txtPMC2"
        Me.txtPMC2.ReadOnly = True
        Me.txtPMC2.Size = New System.Drawing.Size(97, 22)
        Me.txtPMC2.TabIndex = 110
        Me.txtPMC2.Tag = "NE;"
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(8, 136)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(64, 16)
        Me.Label16.TabIndex = 112
        Me.Label16.Text = "PMC"
        '
        'txtCostPerGram2
        '
        Me.txtCostPerGram2.BackColor = System.Drawing.Color.White
        Me.txtCostPerGram2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCostPerGram2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostPerGram2.ForeColor = System.Drawing.Color.Black
        Me.txtCostPerGram2.Location = New System.Drawing.Point(104, 104)
        Me.txtCostPerGram2.MaxLength = 20
        Me.txtCostPerGram2.Name = "txtCostPerGram2"
        Me.txtCostPerGram2.ReadOnly = True
        Me.txtCostPerGram2.Size = New System.Drawing.Size(97, 22)
        Me.txtCostPerGram2.TabIndex = 108
        Me.txtCostPerGram2.Tag = "NE;"
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(8, 112)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(88, 16)
        Me.Label17.TabIndex = 109
        Me.Label17.Text = "Cost Per Gram"
        '
        'txtDiamondDescription2
        '
        Me.txtDiamondDescription2.BackColor = System.Drawing.Color.White
        Me.txtDiamondDescription2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDiamondDescription2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiamondDescription2.ForeColor = System.Drawing.Color.Black
        Me.txtDiamondDescription2.Location = New System.Drawing.Point(104, 56)
        Me.txtDiamondDescription2.MaxLength = 20
        Me.txtDiamondDescription2.Name = "txtDiamondDescription2"
        Me.txtDiamondDescription2.ReadOnly = True
        Me.txtDiamondDescription2.Size = New System.Drawing.Size(280, 22)
        Me.txtDiamondDescription2.TabIndex = 106
        Me.txtDiamondDescription2.Tag = "NE;"
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(8, 64)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(88, 16)
        Me.Label18.TabIndex = 107
        Me.Label18.Text = "Diamond Desc"
        '
        'btnInventorySearch2
        '
        Me.btnInventorySearch2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnInventorySearch2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInventorySearch2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInventorySearch2.Location = New System.Drawing.Point(200, 8)
        Me.btnInventorySearch2.Name = "btnInventorySearch2"
        Me.btnInventorySearch2.Size = New System.Drawing.Size(24, 22)
        Me.btnInventorySearch2.TabIndex = 105
        Me.btnInventorySearch2.Text = "..."
        Me.btnInventorySearch2.UseVisualStyleBackColor = False
        Me.btnInventorySearch2.Visible = False
        '
        'txtStoneWeight2
        '
        Me.txtStoneWeight2.BackColor = System.Drawing.Color.White
        Me.txtStoneWeight2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStoneWeight2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStoneWeight2.Location = New System.Drawing.Point(288, 80)
        Me.txtStoneWeight2.MaxLength = 20
        Me.txtStoneWeight2.Name = "txtStoneWeight2"
        Me.txtStoneWeight2.ReadOnly = True
        Me.txtStoneWeight2.Size = New System.Drawing.Size(97, 22)
        Me.txtStoneWeight2.TabIndex = 4
        Me.txtStoneWeight2.Tag = "NE;"
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(208, 80)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(80, 16)
        Me.Label19.TabIndex = 56
        Me.Label19.Text = "Stone Weight"
        '
        'txtWeight2
        '
        Me.txtWeight2.BackColor = System.Drawing.Color.White
        Me.txtWeight2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWeight2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeight2.ForeColor = System.Drawing.Color.Black
        Me.txtWeight2.Location = New System.Drawing.Point(104, 80)
        Me.txtWeight2.MaxLength = 20
        Me.txtWeight2.Name = "txtWeight2"
        Me.txtWeight2.ReadOnly = True
        Me.txtWeight2.Size = New System.Drawing.Size(97, 22)
        Me.txtWeight2.TabIndex = 323
        Me.txtWeight2.Tag = "NE;"
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(8, 88)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(64, 16)
        Me.Label20.TabIndex = 52
        Me.Label20.Text = "Weight"
        '
        'txtDescription2
        '
        Me.txtDescription2.BackColor = System.Drawing.Color.White
        Me.txtDescription2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescription2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription2.ForeColor = System.Drawing.Color.Black
        Me.txtDescription2.Location = New System.Drawing.Point(104, 32)
        Me.txtDescription2.MaxLength = 20
        Me.txtDescription2.Name = "txtDescription2"
        Me.txtDescription2.ReadOnly = True
        Me.txtDescription2.Size = New System.Drawing.Size(280, 22)
        Me.txtDescription2.TabIndex = 2
        Me.txtDescription2.Tag = "NE;"
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(8, 40)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(72, 16)
        Me.Label21.TabIndex = 40
        Me.Label21.Text = "Description "
        '
        'txtInventoryCode2
        '
        Me.txtInventoryCode2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInventoryCode2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInventoryCode2.Location = New System.Drawing.Point(104, 8)
        Me.txtInventoryCode2.MaxLength = 20
        Me.txtInventoryCode2.Name = "txtInventoryCode2"
        Me.txtInventoryCode2.Size = New System.Drawing.Size(96, 22)
        Me.txtInventoryCode2.TabIndex = 2
        Me.txtInventoryCode2.Tag = "NE;"
        '
        'Label22
        '
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(8, 8)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(89, 16)
        Me.Label22.TabIndex = 38
        Me.Label22.Text = "Inventory Code"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.TextBox1)
        Me.Panel3.Controls.Add(Me.Label39)
        Me.Panel3.Controls.Add(Me.Label36)
        Me.Panel3.Controls.Add(Me.btnSizeCode)
        Me.Panel3.Controls.Add(Me.txtSizeCode)
        Me.Panel3.Controls.Add(Me.cboCatalogue)
        Me.Panel3.Controls.Add(Me.Label35)
        Me.Panel3.Controls.Add(Me.cboCategory)
        Me.Panel3.Controls.Add(Me.txtDiamondDescription)
        Me.Panel3.Controls.Add(Me.txtDescription)
        Me.Panel3.Controls.Add(Me.Label34)
        Me.Panel3.Controls.Add(Me.txtPCS)
        Me.Panel3.Controls.Add(Me.Label33)
        Me.Panel3.Controls.Add(Me.txtStoneWeight)
        Me.Panel3.Controls.Add(Me.Label32)
        Me.Panel3.Controls.Add(Me.Label31)
        Me.Panel3.Controls.Add(Me.txtTotalCost)
        Me.Panel3.Controls.Add(Me.txtSMC)
        Me.Panel3.Controls.Add(Me.Label29)
        Me.Panel3.Controls.Add(Me.txtPMC)
        Me.Panel3.Controls.Add(Me.Label30)
        Me.Panel3.Controls.Add(Me.txtGoldLoss)
        Me.Panel3.Controls.Add(Me.Label28)
        Me.Panel3.Controls.Add(Me.Label27)
        Me.Panel3.Controls.Add(Me.txtCostPerGram)
        Me.Panel3.Controls.Add(Me.Label23)
        Me.Panel3.Controls.Add(Me.txtWeight)
        Me.Panel3.Controls.Add(Me.Label24)
        Me.Panel3.Controls.Add(Me.Label25)
        Me.Panel3.Controls.Add(Me.txtInventoryCode)
        Me.Panel3.Controls.Add(Me.Label26)
        Me.Panel3.Location = New System.Drawing.Point(20, 348)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(796, 172)
        Me.Panel3.TabIndex = 145
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(504, 115)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(281, 22)
        Me.TextBox1.TabIndex = 412
        '
        'Label39
        '
        Me.Label39.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(408, 115)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(88, 16)
        Me.Label39.TabIndex = 413
        Me.Label39.Text = "Remarks"
        '
        'Label36
        '
        Me.Label36.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.Color.Black
        Me.Label36.Location = New System.Drawing.Point(7, 147)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(89, 16)
        Me.Label36.TabIndex = 410
        Me.Label36.Text = "Size"
        '
        'btnSizeCode
        '
        Me.btnSizeCode.BackColor = System.Drawing.Color.Orange
        Me.btnSizeCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSizeCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSizeCode.Location = New System.Drawing.Point(296, 143)
        Me.btnSizeCode.Name = "btnSizeCode"
        Me.btnSizeCode.Size = New System.Drawing.Size(30, 22)
        Me.btnSizeCode.TabIndex = 409
        Me.btnSizeCode.Text = "..."
        Me.btnSizeCode.UseVisualStyleBackColor = False
        '
        'txtSizeCode
        '
        Me.txtSizeCode.BackColor = System.Drawing.Color.White
        Me.txtSizeCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSizeCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSizeCode.ForeColor = System.Drawing.Color.Navy
        Me.txtSizeCode.Location = New System.Drawing.Point(103, 143)
        Me.txtSizeCode.MaxLength = 21
        Me.txtSizeCode.Name = "txtSizeCode"
        Me.txtSizeCode.ReadOnly = True
        Me.txtSizeCode.Size = New System.Drawing.Size(222, 22)
        Me.txtSizeCode.TabIndex = 408
        '
        'cboCatalogue
        '
        Me.cboCatalogue.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCatalogue.Location = New System.Drawing.Point(103, 38)
        Me.cboCatalogue.Name = "cboCatalogue"
        Me.cboCatalogue.Size = New System.Drawing.Size(120, 24)
        Me.cboCatalogue.TabIndex = 336
        '
        'Label35
        '
        Me.Label35.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.Color.Black
        Me.Label35.Location = New System.Drawing.Point(7, 38)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(89, 16)
        Me.Label35.TabIndex = 337
        Me.Label35.Text = "Catalogue"
        '
        'cboCategory
        '
        Me.cboCategory.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCategory.Location = New System.Drawing.Point(104, 8)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.Size = New System.Drawing.Size(121, 24)
        Me.cboCategory.TabIndex = 3
        '
        'txtDiamondDescription
        '
        Me.txtDiamondDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDiamondDescription.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiamondDescription.Location = New System.Drawing.Point(103, 115)
        Me.txtDiamondDescription.Name = "txtDiamondDescription"
        Me.txtDiamondDescription.Size = New System.Drawing.Size(280, 22)
        Me.txtDiamondDescription.TabIndex = 5
        '
        'txtDescription
        '
        Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.Location = New System.Drawing.Point(103, 91)
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(280, 22)
        Me.txtDescription.TabIndex = 4
        '
        'Label34
        '
        Me.Label34.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(7, 67)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(89, 16)
        Me.Label34.TabIndex = 131
        Me.Label34.Text = "Inventory Code"
        '
        'txtPCS
        '
        Me.txtPCS.BackColor = System.Drawing.Color.White
        Me.txtPCS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPCS.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPCS.Location = New System.Drawing.Point(688, 8)
        Me.txtPCS.MaxLength = 20
        Me.txtPCS.Name = "txtPCS"
        Me.txtPCS.ReadOnly = True
        Me.txtPCS.Size = New System.Drawing.Size(97, 22)
        Me.txtPCS.TabIndex = 129
        Me.txtPCS.Tag = "NE;"
        '
        'Label33
        '
        Me.Label33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(608, 8)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(80, 16)
        Me.Label33.TabIndex = 130
        Me.Label33.Text = "PCS"
        '
        'txtStoneWeight
        '
        Me.txtStoneWeight.BackColor = System.Drawing.Color.White
        Me.txtStoneWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStoneWeight.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStoneWeight.ForeColor = System.Drawing.Color.Black
        Me.txtStoneWeight.Location = New System.Drawing.Point(504, 80)
        Me.txtStoneWeight.MaxLength = 20
        Me.txtStoneWeight.Name = "txtStoneWeight"
        Me.txtStoneWeight.ReadOnly = True
        Me.txtStoneWeight.Size = New System.Drawing.Size(97, 22)
        Me.txtStoneWeight.TabIndex = 128
        Me.txtStoneWeight.Tag = "NE;"
        '
        'Label32
        '
        Me.Label32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(408, 80)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(72, 16)
        Me.Label32.TabIndex = 127
        Me.Label32.Text = "Stone Wt."
        '
        'Label31
        '
        Me.Label31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(608, 80)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(64, 16)
        Me.Label31.TabIndex = 126
        Me.Label31.Text = "Total Cost"
        '
        'txtTotalCost
        '
        Me.txtTotalCost.BackColor = System.Drawing.Color.White
        Me.txtTotalCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalCost.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalCost.Location = New System.Drawing.Point(688, 80)
        Me.txtTotalCost.MaxLength = 20
        Me.txtTotalCost.Name = "txtTotalCost"
        Me.txtTotalCost.ReadOnly = True
        Me.txtTotalCost.Size = New System.Drawing.Size(97, 22)
        Me.txtTotalCost.TabIndex = 125
        Me.txtTotalCost.Tag = "NE;"
        '
        'txtSMC
        '
        Me.txtSMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSMC.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMC.Location = New System.Drawing.Point(688, 56)
        Me.txtSMC.MaxLength = 20
        Me.txtSMC.Name = "txtSMC"
        Me.txtSMC.Size = New System.Drawing.Size(97, 22)
        Me.txtSMC.TabIndex = 6
        Me.txtSMC.Tag = "NE;"
        '
        'Label29
        '
        Me.Label29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(608, 56)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(64, 16)
        Me.Label29.TabIndex = 124
        Me.Label29.Text = "SMC"
        '
        'txtPMC
        '
        Me.txtPMC.BackColor = System.Drawing.Color.White
        Me.txtPMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPMC.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPMC.ForeColor = System.Drawing.Color.Black
        Me.txtPMC.Location = New System.Drawing.Point(688, 32)
        Me.txtPMC.MaxLength = 20
        Me.txtPMC.Name = "txtPMC"
        Me.txtPMC.ReadOnly = True
        Me.txtPMC.Size = New System.Drawing.Size(97, 22)
        Me.txtPMC.TabIndex = 121
        Me.txtPMC.Tag = "NE;"
        '
        'Label30
        '
        Me.Label30.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(608, 32)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(48, 16)
        Me.Label30.TabIndex = 123
        Me.Label30.Text = "PMC"
        '
        'txtGoldLoss
        '
        Me.txtGoldLoss.BackColor = System.Drawing.Color.White
        Me.txtGoldLoss.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldLoss.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoldLoss.ForeColor = System.Drawing.Color.Black
        Me.txtGoldLoss.Location = New System.Drawing.Point(504, 56)
        Me.txtGoldLoss.MaxLength = 20
        Me.txtGoldLoss.Name = "txtGoldLoss"
        Me.txtGoldLoss.ReadOnly = True
        Me.txtGoldLoss.Size = New System.Drawing.Size(97, 22)
        Me.txtGoldLoss.TabIndex = 119
        Me.txtGoldLoss.Tag = "NE;"
        '
        'Label28
        '
        Me.Label28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(408, 56)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(64, 16)
        Me.Label28.TabIndex = 120
        Me.Label28.Text = "Gold Loss"
        '
        'Label27
        '
        Me.Label27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(7, 115)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(88, 16)
        Me.Label27.TabIndex = 58
        Me.Label27.Text = "Diamond Desc."
        '
        'txtCostPerGram
        '
        Me.txtCostPerGram.BackColor = System.Drawing.Color.White
        Me.txtCostPerGram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCostPerGram.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostPerGram.Location = New System.Drawing.Point(504, 32)
        Me.txtCostPerGram.MaxLength = 20
        Me.txtCostPerGram.Name = "txtCostPerGram"
        Me.txtCostPerGram.ReadOnly = True
        Me.txtCostPerGram.Size = New System.Drawing.Size(97, 22)
        Me.txtCostPerGram.TabIndex = 4
        Me.txtCostPerGram.Tag = "NE;"
        '
        'Label23
        '
        Me.Label23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(408, 32)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(83, 16)
        Me.Label23.TabIndex = 56
        Me.Label23.Text = "Cost Per Gram"
        '
        'txtWeight
        '
        Me.txtWeight.BackColor = System.Drawing.Color.White
        Me.txtWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWeight.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeight.ForeColor = System.Drawing.Color.Black
        Me.txtWeight.Location = New System.Drawing.Point(504, 8)
        Me.txtWeight.MaxLength = 20
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.ReadOnly = True
        Me.txtWeight.Size = New System.Drawing.Size(97, 22)
        Me.txtWeight.TabIndex = 335
        Me.txtWeight.Tag = "NE;"
        '
        'Label24
        '
        Me.Label24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(408, 8)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(64, 16)
        Me.Label24.TabIndex = 52
        Me.Label24.Text = "Weight"
        '
        'Label25
        '
        Me.Label25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(7, 91)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(72, 16)
        Me.Label25.TabIndex = 40
        Me.Label25.Text = "Description "
        '
        'txtInventoryCode
        '
        Me.txtInventoryCode.BackColor = System.Drawing.Color.White
        Me.txtInventoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInventoryCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInventoryCode.Location = New System.Drawing.Point(103, 68)
        Me.txtInventoryCode.MaxLength = 20
        Me.txtInventoryCode.Name = "txtInventoryCode"
        Me.txtInventoryCode.Size = New System.Drawing.Size(96, 22)
        Me.txtInventoryCode.TabIndex = 0
        Me.txtInventoryCode.Tag = "NE;"
        '
        'Label26
        '
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(8, 8)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(89, 16)
        Me.Label26.TabIndex = 38
        Me.Label26.Text = "Category"
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(504, 526)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 40)
        Me.btnSave.TabIndex = 7
        Me.btnSave.Text = "Merge (F7)"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.Location = New System.Drawing.Point(610, 526)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(100, 40)
        Me.btnClear.TabIndex = 8
        Me.btnClear.Text = "Clear (F6)"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(716, 526)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 9
        Me.btnClose.Text = "Close (Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnBarcode
        '
        Me.btnBarcode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnBarcode.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnBarcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBarcode.Location = New System.Drawing.Point(398, 526)
        Me.btnBarcode.Name = "btnBarcode"
        Me.btnBarcode.Size = New System.Drawing.Size(100, 40)
        Me.btnBarcode.TabIndex = 146
        Me.btnBarcode.Text = "Barcode"
        Me.btnBarcode.UseVisualStyleBackColor = False
        Me.btnBarcode.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(20, 1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(270, 100)
        Me.PictureBox1.TabIndex = 147
        Me.PictureBox1.TabStop = False
        '
        'chkAdditionalDesc
        '
        Me.chkAdditionalDesc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAdditionalDesc.ForeColor = System.Drawing.Color.Black
        Me.chkAdditionalDesc.Location = New System.Drawing.Point(20, 535)
        Me.chkAdditionalDesc.Name = "chkAdditionalDesc"
        Me.chkAdditionalDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAdditionalDesc.Size = New System.Drawing.Size(68, 31)
        Me.chkAdditionalDesc.TabIndex = 523
        Me.chkAdditionalDesc.Text = "Addl. Desc"
        '
        'grpAddlDescription
        '
        Me.grpAddlDescription.Controls.Add(Me.GroupBox9)
        Me.grpAddlDescription.Controls.Add(Me.GroupBox7)
        Me.grpAddlDescription.Controls.Add(Me.Label42)
        Me.grpAddlDescription.Controls.Add(Me.Label41)
        Me.grpAddlDescription.Controls.Add(Me.cmdAddlDescOk)
        Me.grpAddlDescription.Controls.Add(Me.Label40)
        Me.grpAddlDescription.Controls.Add(Me.Label43)
        Me.grpAddlDescription.Controls.Add(Me.Label44)
        Me.grpAddlDescription.Controls.Add(Me.Label45)
        Me.grpAddlDescription.Location = New System.Drawing.Point(195, 86)
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
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label42.Location = New System.Drawing.Point(324, 17)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(75, 13)
        Me.Label42.TabIndex = 529
        Me.Label42.Text = "Selling Price"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label41.Location = New System.Drawing.Point(214, 17)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(32, 13)
        Me.Label41.TabIndex = 524
        Me.Label41.Text = "Cost"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label40.Location = New System.Drawing.Point(12, 16)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(71, 13)
        Me.Label40.TabIndex = 515
        Me.Label40.Text = "Description"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label43.Location = New System.Drawing.Point(19, 99)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(78, 13)
        Me.Label43.TabIndex = 518
        Me.Label43.Text = "Description4"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label43.Visible = False
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label44.Location = New System.Drawing.Point(20, 73)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(78, 13)
        Me.Label44.TabIndex = 517
        Me.Label44.Text = "Description3"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label44.Visible = False
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label45.Location = New System.Drawing.Point(20, 47)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(78, 13)
        Me.Label45.TabIndex = 516
        Me.Label45.Text = "Description2"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label45.Visible = False
        '
        'frmMergeInventory
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(834, 578)
        Me.ControlBox = False
        Me.Controls.Add(Me.grpAddlDescription)
        Me.Controls.Add(Me.chkAdditionalDesc)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnBarcode)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmMergeInventory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Merge Inventory"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpAddlDescription.ResumeLayout(False)
        Me.grpAddlDescription.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub txtInventoryCode1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInventoryCode1.LostFocus
        If txtInventoryCode1.Text.Trim <> "" Then
            subGetInventory1Detail()
            Exit Sub
        End If
    End Sub
    Private Sub txtInventoryCode2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInventoryCode2.LostFocus
        If txtInventoryCode2.Text.Trim <> "" Then
            If UCase(txtInventoryCode1.Text.Trim) <> UCase(txtInventoryCode2.Text.Trim) Then
                subGetInventory2Detail()
                chkMerge.Enabled = False
                txtMergeWeight.Enabled = False
                Exit Sub
            Else
                MsgBox("Same inventory can not be merged!", MsgBoxStyle.Exclamation)
                txtInventoryCode2.Clear()
                Exit Sub
            End If

        End If
    End Sub
    Private Sub subGetInventory1Detail()
        Dim dsInv As New DataSet
        Dim DsAdditional As DataSet
        Dim objClsInventory As New clsInventory

        Try
            If objClsInvMerge.IsInventory(txtInventoryCode1.Text.Trim) Then
                dsInv = objClsInvMerge.GetInventoryDetail(txtInventoryCode1.Text.Trim)
                If dsInv.Tables(0).Rows.Count > 0 Then
                    txtInventoryCode1.Text = Trim(dsInv.Tables(0).Rows(0)("InventoryCode") & "")
                    txtDescription1.Text = Trim(dsInv.Tables(0).Rows(0)("Description") & "")
                    txtDiamondDescription1.Text = Trim(dsInv.Tables(0).Rows(0)("DiamondDescription") & "")
                    txtStoneWeight1.Text = Format(Val(dsInv.Tables(0).Rows(0)("StoneWeight") & ""), "0.00")
                    txtWeight1.Text = Format(Val(dsInv.Tables(0).Rows(0)("Weight") & ""), "0.00")
                    txtCostPerGram1.Text = Format(Val(dsInv.Tables(0).Rows(0)("CostperGram")), "0.00")
                    txtPMC1.Text = Format(Val(dsInv.Tables(0).Rows(0)("PMC")), "0.00")
                    txtSMC1.Text = Format(Val(dsInv.Tables(0).Rows(0)("sMc")), "0.00")
                    txtTotalCost1.Text = Format(Val(dsInv.Tables(0).Rows(0)("TotalCost") & ""), "0.00")
                    txtPCS1.Text = Val(dsInv.Tables(0).Rows(0)("PCS"))
                    txtStoneWeight1.Text = Format(Val(dsInv.Tables(0).Rows(0)("StoneWeight")), "0.00")
                    txtSizeCode1.Text = Format(Val(dsInv.Tables(0).Rows(0)("SizeCode")), "0.00")

                    sJobsheetNo = Trim(dsInv.Tables(0).Rows(0)("JobSheetNo") & "")
                    sPurchaseBillNo = Trim(dsInv.Tables(0).Rows(0)("PurchaseBillNo") & "")
                    dPurchaseDate = dsInv.Tables(0).Rows(0)("PurchaseDate")
                    dCreateDate = Format(CDate(dsInv.Tables(0).Rows(0)("CreateDate")), "dd/MM/yy")
                    sSupplierCode = Trim(dsInv.Tables(0).Rows(0)("SupplierCode") & "")
                    sSalesBillNo = Trim(dsInv.Tables(0).Rows(0)("SalesBillNo") & "")
                    dPurchaseDate = Format(CDate(dsInv.Tables(0).Rows(0)("PurchaseDate")), "dd/MM/yy")
                    sCategoryCode = Trim(dsInv.Tables(0).Rows(0)("CategoryCode"))
                    sClassCode = Trim(dsInv.Tables(0).Rows(0)("ClassCode"))
                    sCatalogueCode = dsInv.Tables(0).Rows(0)("CatalogueCode")
                    sTrayId = Trim(dsInv.Tables(0).Rows(0)("TrayID"))
                    sPMCType = Trim(dsInv.Tables(0).Rows(0)("PMCType") & "")
                    sSMCType = Trim(dsInv.Tables(0).Rows(0)("SMCType") & "")
                    sRemarks = Trim(dsInv.Tables(0).Rows(0)("REMARKS") & "")
                    txtInventoryCode1.ReadOnly = True
                    txtInventoryCode2.Focus()


                    DsAdditional = objClsInventory.GetAdditionalInventoryDetail(txtInventoryCode1.Text.Trim)
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
                Else
                    MsgBox("This Item already sold. Can not merge!", MsgBoxStyle.Exclamation)
                    txtInventoryCode1.Clear()
                    txtInventoryCode1.Focus()
                    Exit Sub
                End If
            Else
                MsgBox("Inventory does not exist!", MsgBoxStyle.Exclamation)
                txtInventoryCode1.Clear()
                txtInventoryCode1.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
            dsInv.Dispose()
        End Try
    End Sub
    Private Sub subGetInventory2Detail()
        Dim dsInv As New DataSet
        Dim DsAdditional As DataSet
        Dim objClsInventory As New clsInventory
        Try

            If objClsInvMerge.IsInventory(txtInventoryCode2.Text.Trim) Then
                dsInv = objClsInvMerge.GetInventoryDetail(txtInventoryCode2.Text.Trim)
                If dsInv.Tables(0).Rows.Count > 0 Then
                    'If sClassCode.Trim = Trim(dsInv.Tables(0).Rows(0)("ClassCode")) Then
                    txtInventoryCode2.Text = Trim(dsInv.Tables(0).Rows(0)("InventoryCode") & "")
                    txtDescription2.Text = Trim(dsInv.Tables(0).Rows(0)("Description") & "")
                    txtDiamondDescription2.Text = Trim(dsInv.Tables(0).Rows(0)("DiamondDescription") & "")
                    txtStoneWeight2.Text = Format(Val(dsInv.Tables(0).Rows(0)("StoneWeight") & ""), "0.00")
                    txtWeight2.Text = Format(Val(dsInv.Tables(0).Rows(0)("Weight") & ""), "0.00")
                    txtCostPerGram2.Text = Format(Val(dsInv.Tables(0).Rows(0)("CostperGram")), "0.00")
                    txtPMC2.Text = Format(Val(dsInv.Tables(0).Rows(0)("PMC")), "0.00")
                    txtSMC2.Text = Format(Val(dsInv.Tables(0).Rows(0)("sMc")), "0.00")
                    txtTotalCost2.Text = Format(Val(dsInv.Tables(0).Rows(0)("TotalCost") & ""), "0.00")
                    txtPCS2.Text = Val(dsInv.Tables(0).Rows(0)("PCS"))
                    txtStoneWeight2.Text = Format(Val(dsInv.Tables(0).Rows(0)("StoneWeight")), "0.00")
                    txtSizeCode2.Text = Format(Val(dsInv.Tables(0).Rows(0)("SizeCode")), "0.00")
                    txtInventoryCode2.ReadOnly = True
                    subMergedDetail()
                    cboCategory.Focus()
                    'Else
                    '    MsgBox("Class code is not the same to merge!", MsgBoxStyle.Critical)
                    '    txtInventoryCode2.Clear()
                    '    txtInventoryCode2.Focus()
                    '    Exit Sub
                    'End If
                    If Trim(txtDesc1.Text) = "" Then
                        DsAdditional = objClsInventory.GetAdditionalInventoryDetail(txtInventoryCode2.Text.Trim)
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
                Else
                    MsgBox("This Item already sold. Can not merge!", MsgBoxStyle.Exclamation)
                    txtInventoryCode2.Clear()
                    txtInventoryCode2.Focus()
                    Exit Sub
                End If
            Else
                MsgBox("Inventory does not exist!", MsgBoxStyle.Exclamation)
                txtInventoryCode2.Clear()
                txtInventoryCode2.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
            dsInv.Dispose()
        End Try
    End Sub
    Private Sub subMergedDetail()
        Dim ds As DataSet
        Dim icnt As Integer
        If chkMerge.Checked = False Then
            txtWeight.Text = Format(Val(txtWeight1.Text) + Val(txtWeight2.Text), "0.00")
            txtGoldLoss.Text = Format(Val(txtGoldLoss1.Text) + Val(txtGoldLoss2.Text), "0.00")
            txtTotalCost.Text = Format(Val(txtTotalCost1.Text) + Val(txtTotalCost2.Text), "0.00")
            txtStoneWeight.Text = Format(Val(txtStoneWeight1.Text) + Val(txtStoneWeight2.Text), "0.00")
            txtPCS.Text = Val(txtPCS1.Text)
            txtCostPerGram.Text = Format(Val(txtCostPerGram1.Text), "0.00")
            txtSMC.Text = Format(Val(txtSMC1.Text) + Val(txtSMC2.Text), "0.00")
            txtPMC.Text = Format(Val(txtPMC1.Text) + Val(txtPMC2.Text), "0.00")
        Else
            txtWeight.Text = Format(Val(txtWeight1.Text) + Val(txtMergeWeight.Text), "0.00")
            txtGoldLoss.Text = Format(Val(txtGoldLoss1.Text), "0.00")
            txtTotalCost.Text = Format(Val(txtTotalCost1.Text), "0.00")
            txtStoneWeight.Text = Format(Val(txtStoneWeight1.Text), "0.00")
            txtPCS.Text = Val(txtPCS1.Text)
            txtCostPerGram.Text = Format(Val(txtCostPerGram1.Text), "0.00")
            txtSMC.Text = Format(Val(txtSMC1.Text), "0.00")
            txtPMC.Text = Format(Val(txtPMC1.Text), "0.00")
        End If
        Try
            ds = objClsInvMerge.GetMergeCategory(sClassCode)
            If ds.Tables(0).Rows.Count > 0 Then
                For icnt = 0 To ds.Tables(0).Rows.Count - 1
                    cboCategory.Items.Add(Trim(ds.Tables(0).Rows(icnt)("categorycode") & ""))
                Next
            End If
            cboCategory.SelectedIndex = 0
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If txtInventoryCode1.Text.Trim = "" Then
                MsgBox("First InventoryCode is empty!", MsgBoxStyle.Exclamation)
                txtInventoryCode1.Focus()
                Exit Sub
            ElseIf txtInventoryCode2.Text.Trim = "" And chkMerge.Checked = False Then
                MsgBox("Second InventoryCode is empty!", MsgBoxStyle.Exclamation)
                txtInventoryCode2.Focus()
                Exit Sub
            ElseIf txtInventoryCode.Text.Trim = "" Then
                MsgBox("New InventoryCode is empty!", MsgBoxStyle.Exclamation)
                txtInventoryCode1.Focus()
                Exit Sub
            ElseIf txtInventoryCode1.Text.Trim = txtInventoryCode2.Text.Trim Then
                MsgBox("Both InventoryCode are Same!", MsgBoxStyle.Exclamation)
                txtInventoryCode1.Focus()
                Exit Sub
            End If
            If cboCategory.Text.Trim = "" Then
                MsgBox("Select category!", MsgBoxStyle.Exclamation)
                cboCategory.Focus()
                Exit Sub
            End If
            If txtSMC.Text.Trim = "" Then
                MsgBox("Enter S M C value!", MsgBoxStyle.Exclamation)
                txtSMC.Focus()
                Exit Sub
            End If
            If fncInventory(txtInventoryCode.Text.Trim) Then
                If sCompanyShortCode <> "ES" Then
                    Call btnBarcode_Click(sender, e)
                End If
                MsgBox("Inventory Merged  Successfully..!", MsgBoxStyle.Information)
                ClearFormControls()
                Exit Sub
            Else
                MsgBox("Error in saving the Merged Inventory!", MsgBoxStyle.Critical)
                Exit Sub
            End If

        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ClearFormControls()
    End Sub
    Private Sub ClearFormControls()
        txtInventoryCode.Clear()
        txtInventoryCode1.Clear()
        txtInventoryCode2.Clear()
        txtDescription.Clear()
        txtDescription1.Clear()
        txtDescription2.Clear()
        txtDiamondDescription.Clear()
        txtDiamondDescription1.Clear()
        txtDiamondDescription2.Clear()
        txtStoneWeight.Clear()
        txtStoneWeight1.Clear()
        txtStoneWeight2.Clear()
        txtWeight.Clear()
        txtWeight1.Clear()
        txtWeight2.Clear()
        txtGoldLoss.Clear()
        txtGoldLoss1.Clear()
        txtGoldLoss2.Clear()
        txtCostPerGram.Clear()
        txtCostPerGram1.Clear()
        txtCostPerGram2.Clear()
        txtPMC.Clear()
        txtPMC1.Clear()
        txtPMC2.Clear()
        txtSMC.Clear()
        txtSMC1.Clear()
        txtSMC2.Clear()
        txtPCS.Clear()
        txtPCS1.Clear()
        txtPCS2.Clear()
        txtTotalCost.Clear()
        txtTotalCost1.Clear()
        txtTotalCost2.Clear()
        cboCategory.Text = ""
        txtInventoryCode.ReadOnly = False
        txtInventoryCode1.ReadOnly = False
        txtInventoryCode2.ReadOnly = False
        txtInventoryCode1.Focus()


        chkAdditionalDesc.Checked = False
        txtDesc1.Text = ""
        txtDesc2.Text = ""
        txtDesc3.Text = ""
        txtDesc4.Text = ""
        txtCost1.Text = ""
        txtCost2.Text = ""
        txtCost3.Text = ""
        txtCost4.Text = ""
        txtSellingPrice1.Text = ""
        txtSellingPrice2.Text = ""
        txtSellingPrice3.Text = ""
        txtSellingPrice4.Text = ""
        txtNewDesc1.Text = ""
        txtNewDesc2.Text = ""
        txtNewDesc3.Text = ""
        txtNewDesc4.Text = ""
        txtNewCost1.Text = ""
        txtNewCost2.Text = ""
        txtNewCost3.Text = ""
        txtNewCost4.Text = ""
        txtNewSellingPrice1.Text = ""
        txtNewSellingPrice2.Text = ""
        txtNewSellingPrice3.Text = ""
        txtNewSellingPrice4.Text = ""
    End Sub
    Private Function fncInventory(ByRef sinventorycode As String) As Boolean
        Dim bUptationStatus As Boolean
        Dim bPurchase As Boolean
        Dim objInventory As New clsInventory
        bUptationStatus = False
        fncInventory = False
        Try
            dPCS = 1
            sCategoryCode = cboCategory.Text.Trim
            dGoldLoss = Val(txtGoldLoss.Text.Trim)
            dCostPerGram = Val(txtCostPerGram.Text.Trim)
            dTotalCost = (Val(txtWeight.Text.Trim) + Val(dGoldLoss)) * Val(dCostPerGram) + dPMC

            If Not objClsInvMerge.InventorySave(Trim(txtInventoryCode.Text), _
                        Trim(txtDescription.Text), Trim(txtDiamondDescription.Text), _
                        Val(txtWeight.Text.Trim), Val(txtStoneWeight.Text), Val(txtCostPerGram.Text), _
                        Val(txtPMC.Text), (Val(txtSMC.Text)), Val(txtPCS.Text), Val(txtGoldLoss.Text), Val(dTotalCost), _
                        Trim(sJobsheetNo), Trim(sPurchaseBillNo), Trim(dPurchaseDate), Trim(sSupplierCode), _
                        "", "", 0, 0, 0, Trim(sCategoryCode), Trim(sClassCode), Trim(sCatalogueCode), Trim(sTrayId), _
                        Trim$(sPMCType), Trim$(sSMCType), CLng(dPCS), 0, Trim(sRemarks), Val(txtSizeCode.Text), Trim(sSizeType)) Then
                fncInventory = False
                Exit Function
            End If
            If Trim(gthisLocationType) <> "HQ" Then
                If Not objClsInvMerge.InventorySaveHQ(Trim(txtInventoryCode.Text), _
                            Trim(txtDescription.Text), Trim(txtDiamondDescription.Text), _
                            Val(txtWeight.Text.Trim), Val(txtStoneWeight.Text), Val(txtCostPerGram.Text), _
                            Val(txtPMC.Text), (Val(txtSMC.Text)), Val(txtPCS.Text), Val(txtGoldLoss.Text), Val(dTotalCost), _
                            Trim(sJobsheetNo), Trim(sPurchaseBillNo), Trim(dPurchaseDate), Trim(sSupplierCode), _
                            "", "", 0, 0, 0, Trim(sCategoryCode), Trim(sClassCode), Trim(sCatalogueCode), Trim(sTrayId), _
                            Trim$(sPMCType), Trim$(sSMCType), CLng(dPCS), 0, Trim(sRemarks), Val(txtSizeCode.Text), Trim(sSizeType)) Then
                    fncInventory = False
                    Exit Function
                End If
            End If
            'dPurchaseDate = Format(dPurchaseDate, "dd/MM/yy")
            'If Format(Now(), "dd/MM/yy") <> dCreateDate Then
            '    bPurchase = objClsInvMerge.PurchaseSave(Trim(sJobsheetNo), _
            '                Trim(sPurchaseBillNo), "", Trim(sSupplierCode), Trim(txtInventoryCode.Text.Trim), _
            '                Trim(txtDescription.Text.Trim), Trim(sDiamondDescription), Val(dStoneWeight), _
            '                Val(dCostPerGram), Val(txtWeight.Text.Trim), Val(dPMC), Val(dPCS), Val(dGoldLoss), Val(dTotalCost), _
            '                Trim(sCatalogueCode), Trim(sCategoryCode), Trim(sClassCode), Trim(sTrayId), "", "ST")
            'Else
            '    bPurchase = objClsInvMerge.PurchaseSave(Trim(sJobsheetNo), _
            '                Trim(sPurchaseBillNo), "", Trim(sSupplierCode), Trim(txtInventoryCode.Text.Trim), _
            '                Trim(txtDescription.Text.Trim), Trim(sDiamondDescription), Val(dStoneWeight), _
            '                Val(dCostPerGram), Val(txtWeight.Text.Trim), Val(dPMC), Val(dPCS), Val(dGoldLoss), Val(dTotalCost), _
            '                Trim(sCatalogueCode), Trim(sCategoryCode), Trim(sClassCode), Trim(sTrayId), "", Trim(sRemarks))
            'End If
            bPurchase = True
            bUptationStatus = objClsInvMerge.InventoryMergedUptation(Trim(txtInventoryCode1.Text), Trim(txtInventoryCode.Text), 0, "MERGED INVENTORY")
            bUptationStatus = objClsInvMerge.InventoryMergedUptation(Trim(txtInventoryCode2.Text), Trim(txtInventoryCode.Text), 0, "MERGED INVENTORY")

            If chkMerge.Checked = False Then
                bUptationStatus = objClsInvMerge.MergeInvHistorySave(Trim(txtInventoryCode1.Text), _
                               Val(txtWeight1.Text.Trim), Val(txtPMC1.Text), (Val(txtSMC1.Text)), _
                               Val(txtGoldLoss1.Text), Val(txtTotalCost1.Text), _
                               Trim(txtInventoryCode2.Text), _
                               Val(txtWeight2.Text.Trim), Val(txtPMC2.Text), (Val(txtSMC2.Text)), _
                               Val(txtGoldLoss2.Text), Val(txtTotalCost2.Text), _
                               Trim(txtInventoryCode.Text), _
                               Val(txtWeight.Text.Trim), Val(txtPMC.Text), (Val(txtSMC.Text)), _
                               Val(txtGoldLoss.Text), Val(txtTotalCost.Text), Trim(txtDescription.Text.Trim), _
                               Trim(sJobsheetNo), Trim(sClassCode))
            Else
                bUptationStatus = objClsInvMerge.MergeInvHistorySave(Trim(txtInventoryCode1.Text), _
                      Val(txtWeight1.Text.Trim), Val(txtPMC1.Text), (Val(txtSMC1.Text)), _
                      Val(txtGoldLoss1.Text), Val(txtTotalCost1.Text), _
                      "MERGEITEM", _
                      Val(txtMergeWeight.Text.Trim), Val(0), (Val(0)), _
                      Val(0), Val(0), _
                      Trim(txtInventoryCode.Text), _
                      Val(txtWeight.Text.Trim), Val(txtPMC.Text), (Val(txtSMC.Text)), _
                      Val(txtGoldLoss.Text), Val(txtTotalCost.Text), Trim(txtDescription.Text.Trim), _
                      Trim(sJobsheetNo), Trim(sClassCode))
            End If


            If Trim(gthisLocationType) <> "HQ" Then
                bUptationStatus = objClsInvMerge.fncInventoryMovementSaveHQ(Trim("HQ"), Trim(txtInventoryCode.Text), Trim(txtDescription.Text.Trim), "PU", "", "", _
                                          Val(1), Val(txtWeight.Text.Trim), Val(txtSizeCode.Text), Val(txtPMC.Text.Trim), _
                                          Val(txtSMC.Text.Trim), 0, Val(txtTotalCost.Text.Trim), _
                                           "", sUserID)


                bUptationStatus = objClsInvMerge.fncInventoryMovementSaveHQ(Trim("HQ"), Trim(txtInventoryCode.Text), Trim(txtDescription.Text.Trim), "TO", "", "", _
                                          Val(1), Val(txtWeight.Text.Trim), Val(txtSizeCode.Text), Val(txtPMC.Text.Trim), _
                                          Val(txtSMC.Text.Trim), 0, Val(txtTotalCost.Text.Trim), _
                                           "", sUserID)

            End If
            bUptationStatus = objClsInvMerge.fncInventoryMovementSave(Trim(gthisLocation), Trim(txtInventoryCode.Text), Trim(txtDescription.Text.Trim), "TI", "", "", _
                                      Val(1), Val(txtWeight.Text.Trim), Val(txtSizeCode.Text), Val(txtPMC.Text.Trim), _
                                      Val(txtSMC.Text.Trim), 0, Val(txtTotalCost.Text.Trim), _
                                       "", sUserID)


            bUptationStatus = objClsInvMerge.fncInventoryMovementSave(Trim(gthisLocation), Trim(txtInventoryCode1.Text), Trim(txtDescription1.Text.Trim), "ME", "", "", _
                                      Val(1), Val(txtWeight1.Text.Trim), 0, Val(txtPMC1.Text.Trim), _
                                      Val(txtSMC1.Text.Trim), 0, Val(txtTotalCost1.Text.Trim), _
                                       "", sUserID)

            bUptationStatus = objClsInvMerge.fncInventoryMovementSave(Trim(gthisLocation), Trim(txtInventoryCode2.Text), Trim(txtDescription2.Text.Trim), "ME", "", "", _
                                      Val(1), Val(txtWeight2.Text.Trim), 0, Val(txtPMC2.Text.Trim), _
                                      Val(txtSMC2.Text.Trim), 0, Val(txtTotalCost2.Text.Trim), _
                                       "", sUserID)



            If chkAdditionalDesc.Checked Then
                If objInventory.InventoryAddlDescSave(txtInventoryCode.Text.Trim, "", txtNewDesc1.Text.Trim, _
                    txtNewDesc2.Text.Trim, txtNewDesc3.Text.Trim, txtNewDesc4.Text.Trim, Val(txtNewCost1.Text.Trim), Val(txtNewCost2.Text.Trim), _
                    Val(txtNewCost3.Text.Trim), Val(txtNewCost4.Text.Trim), Val(txtNewSellingPrice1.Text.Trim), Val(txtNewSellingPrice2.Text.Trim), _
                    Val(txtNewSellingPrice3.Text.Trim), Val(txtNewSellingPrice4.Text.Trim), sUserID) Then
                Else
                    MsgBox("Error in Saving Inventory Additional Description", MsgBoxStyle.Critical)
                    fncInventory = False
                    Exit Function
                End If
            End If

            If bPurchase And bUptationStatus Then
                fncInventory = True
            Else
                fncInventory = False
                Exit Function
            End If


        Catch ex As Exception
            MsgBox(Err.Description)
            fncInventory = False
        Finally
            Err.Clear()
        End Try
    End Function


    Private Sub cboCategory_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCategory.LostFocus
        Dim ds As DataSet
        Dim icnt As Long
        Try
            ds = objClsInvMerge.GetDetailForCategory(cboCategory.Text.Trim)
            If ds.Tables(0).Rows.Count > 0 Then
                txtInventoryCode.Text = (ds.Tables(0).Rows(0)("nextInventoryCode")) 'Format(CInt(ds.Tables(0).Rows(0)("nextInventoryCode")), "000000")
                txtInventoryCode.ReadOnly = True
            End If
            cboCatalogue.Items.Clear()
            ds = objClsInvMerge.GetCatalogue(cboCategory.Text.Trim)
            If ds.Tables(0).Rows.Count > 0 Then
                For icnt = 0 To ds.Tables(0).Rows.Count - 1
                    cboCatalogue.Items.Add(Trim(ds.Tables(0).Rows(icnt)("CatalogueCode") & ""))
                Next
            End If
            cboCatalogue.SelectedIndex = 0
            'cboCatalogue_LostFocus(sender, e)
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
            ds.Dispose()
        End Try

    End Sub
    Private Sub cboCatalogue_LostFocus(sender As Object, e As System.EventArgs) Handles cboCatalogue.LostFocus
        txtDescription.Text = Trim(cboCatalogue.Text)
    End Sub
    Private Sub txtSMC_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSMC.LostFocus
        If Not IsNumeric(txtSMC.Text.Trim) Then
            MsgBox("Enter numeric Value !", MsgBoxStyle.Exclamation)
            txtSMC.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub frmMergeInventory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtInventoryCode1.Focus()
    End Sub

    Private Sub frmMergeInventory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F6
                btnClear_Click(sender, e)
            Case Keys.F7
                btnSave_Click(sender, e)
            Case Keys.Escape
                btnClose_Click(sender, e)
            Case Keys.Return
                SendKeys.Send("{Tab}")
            Case Keys.Down
                SendKeys.Send("{Tab}")
            Case Keys.Up
                SendKeys.Send("+{Tab}")
        End Select
    End Sub
    Private Sub btnBarcode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBarcode.Click
        If UCase(sCompanyShortCode) = "HN" Or UCase(sCompanyShortCode) = "MJ" Or UCase(sCompanyShortCode) = "SSJ" Or UCase(sCompanyShortCode) = "RJ" Then
            Dim objfrm As New frmBarcode
            objfrm.txtInventoryCode.Text = Trim(txtInventoryCode.Text)
            objfrm.txtInventoryCode_LostFocus(sender, e)
            objfrm.btnPrint_Click(sender, e)
        ElseIf UCase(sCompanyShortCode) = "AJ" Then
            Dim objfrm As New frmBarcode
            objfrm.txtInventoryCode.Text = Trim(txtInventoryCode.Text)
            objfrm.txtInventoryCode_LostFocus(sender, e)
            objfrm.btnPrint_Click(sender, e)
        ElseIf UCase(sCompanyShortCode) = "AS" Then
            Dim objfrm As New frmJewelBarcode
            objfrm.txtInventoryCode.Text = Trim(txtInventoryCode.Text)
            objfrm.txtInventoryCode_LostFocus(sender, e)
            objfrm.btnPrint_Click(sender, e)
        End If
    End Sub


    Private Sub btnSizeCode_Click(sender As System.Object, e As System.EventArgs) Handles btnSizeCode.Click
        Try
            Dim e1 As New System.EventArgs
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSize", "L", 4, "SizeCode", "categorycode=" & fncFormatSQLText(cboCategory.Text.Trim), "SizeCode", "SizeCode", 80, "SizeType", "SizeType", 100, "CategoryCode", "CategoryCode", 100, "Remarks", "Remarks", 100)
            frmHelpClass.ShowDialog()
            txtSizeCode.Text = frmHelpClass.Var1.Trim
            sSizeType = frmHelpClass.Var2.Trim
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub chkMerge_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkMerge.CheckedChanged
        If chkMerge.Checked Then
            txtInventoryCode2.Clear()
            txtDescription2.Clear()
            txtDiamondDescription2.Clear()
            txtStoneWeight2.Clear()
            txtWeight2.Clear()
            txtGoldLoss2.Clear()
            txtCostPerGram2.Clear()
            txtPMC2.Clear()
            txtSMC2.Clear()
            txtPCS2.Clear()
            txtTotalCost2.Clear()
        End If
    End Sub

    Private Sub txtMergeWeight_LostFocus(sender As Object, e As System.EventArgs) Handles txtMergeWeight.LostFocus
        subMergedDetail()
    End Sub

    Private Sub btnInventorySearch2_Click(sender As System.Object, e As System.EventArgs) Handles btnInventorySearch2.Click

    End Sub

    Private Sub btnInventorySearch1_Click(sender As System.Object, e As System.EventArgs) Handles btnInventorySearch1.Click

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

End Class
