Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmMergeInventoryML
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
    'Dim sCategoryCode As String
    Dim sClassCode As String
    'Dim sCatalogueCode As String
    Dim sPMCType As String
    Dim sSMCType As String
    Dim sRemarks As String
    Dim masPriceCode$(9)


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
    Friend WithEvents pic As System.Windows.Forms.PictureBox
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
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents txtSize1 As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents txtSize2 As System.Windows.Forms.TextBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents txtSize As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmMergeInventoryML))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtTotalCost1 = New System.Windows.Forms.TextBox
        Me.txtGoldLoss1 = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtPCS1 = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtSMC1 = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtPMC1 = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtCostPerGram1 = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtDiamondDescription1 = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.btnInventorySearch1 = New System.Windows.Forms.Button
        Me.txtStoneWeight1 = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtWeight1 = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtDescription1 = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtInventoryCode1 = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtTotalCost2 = New System.Windows.Forms.TextBox
        Me.txtGoldLoss2 = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtPCS2 = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtSMC2 = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtPMC2 = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtCostPerGram2 = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.txtDiamondDescription2 = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.btnInventorySearch2 = New System.Windows.Forms.Button
        Me.txtStoneWeight2 = New System.Windows.Forms.TextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.txtWeight2 = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.txtDescription2 = New System.Windows.Forms.TextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.txtInventoryCode2 = New System.Windows.Forms.TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.cboCategory = New System.Windows.Forms.ComboBox
        Me.txtDiamondDescription = New System.Windows.Forms.TextBox
        Me.txtDescription = New System.Windows.Forms.TextBox
        Me.Label34 = New System.Windows.Forms.Label
        Me.txtPCS = New System.Windows.Forms.TextBox
        Me.Label33 = New System.Windows.Forms.Label
        Me.txtStoneWeight = New System.Windows.Forms.TextBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.Label31 = New System.Windows.Forms.Label
        Me.txtTotalCost = New System.Windows.Forms.TextBox
        Me.txtSMC = New System.Windows.Forms.TextBox
        Me.Label29 = New System.Windows.Forms.Label
        Me.txtPMC = New System.Windows.Forms.TextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.txtGoldLoss = New System.Windows.Forms.TextBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.txtCostPerGram = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.txtWeight = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.txtInventoryCode = New System.Windows.Forms.TextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.pic = New System.Windows.Forms.PictureBox
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.Label35 = New System.Windows.Forms.Label
        Me.txtSize1 = New System.Windows.Forms.TextBox
        Me.Label36 = New System.Windows.Forms.Label
        Me.txtSize2 = New System.Windows.Forms.TextBox
        Me.Label37 = New System.Windows.Forms.Label
        Me.txtSize = New System.Windows.Forms.TextBox
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txtSize1)
        Me.Panel1.Controls.Add(Me.Label35)
        Me.Panel1.Controls.Add(Me.Label11)
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
        Me.Panel1.Location = New System.Drawing.Point(20, 80)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(398, 208)
        Me.Panel1.TabIndex = 143
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
        Me.txtTotalCost1.Text = ""
        '
        'txtGoldLoss1
        '
        Me.txtGoldLoss1.BackColor = System.Drawing.Color.White
        Me.txtGoldLoss1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldLoss1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoldLoss1.ForeColor = System.Drawing.Color.Black
        Me.txtGoldLoss1.Location = New System.Drawing.Point(104, 176)
        Me.txtGoldLoss1.MaxLength = 20
        Me.txtGoldLoss1.Name = "txtGoldLoss1"
        Me.txtGoldLoss1.ReadOnly = True
        Me.txtGoldLoss1.Size = New System.Drawing.Size(97, 22)
        Me.txtGoldLoss1.TabIndex = 116
        Me.txtGoldLoss1.Tag = "NE;"
        Me.txtGoldLoss1.Text = ""
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(8, 184)
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
        Me.txtPCS1.Text = ""
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
        Me.txtSMC1.Text = ""
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
        Me.txtPMC1.Location = New System.Drawing.Point(104, 152)
        Me.txtPMC1.MaxLength = 20
        Me.txtPMC1.Name = "txtPMC1"
        Me.txtPMC1.ReadOnly = True
        Me.txtPMC1.Size = New System.Drawing.Size(97, 22)
        Me.txtPMC1.TabIndex = 110
        Me.txtPMC1.Tag = "NE;"
        Me.txtPMC1.Text = ""
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(8, 160)
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
        Me.txtCostPerGram1.Location = New System.Drawing.Point(104, 128)
        Me.txtCostPerGram1.MaxLength = 20
        Me.txtCostPerGram1.Name = "txtCostPerGram1"
        Me.txtCostPerGram1.ReadOnly = True
        Me.txtCostPerGram1.Size = New System.Drawing.Size(97, 22)
        Me.txtCostPerGram1.TabIndex = 108
        Me.txtCostPerGram1.Tag = "NE;"
        Me.txtCostPerGram1.Text = ""
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(8, 136)
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
        Me.txtDiamondDescription1.Text = ""
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
        Me.btnInventorySearch1.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnInventorySearch1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInventorySearch1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInventorySearch1.Location = New System.Drawing.Point(199, 8)
        Me.btnInventorySearch1.Name = "btnInventorySearch1"
        Me.btnInventorySearch1.Size = New System.Drawing.Size(24, 22)
        Me.btnInventorySearch1.TabIndex = 105
        Me.btnInventorySearch1.Text = "..."
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
        Me.txtStoneWeight1.Text = ""
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
        Me.txtWeight1.Location = New System.Drawing.Point(104, 104)
        Me.txtWeight1.MaxLength = 20
        Me.txtWeight1.Name = "txtWeight1"
        Me.txtWeight1.ReadOnly = True
        Me.txtWeight1.Size = New System.Drawing.Size(97, 22)
        Me.txtWeight1.TabIndex = 3
        Me.txtWeight1.Tag = "NE;"
        Me.txtWeight1.Text = ""
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 112)
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
        Me.txtDescription1.Text = ""
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
        Me.txtInventoryCode1.Text = ""
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
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.txtSize2)
        Me.Panel2.Controls.Add(Me.Label36)
        Me.Panel2.Controls.Add(Me.Label12)
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
        Me.Panel2.Location = New System.Drawing.Point(418, 80)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(398, 208)
        Me.Panel2.TabIndex = 144
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
        Me.txtTotalCost2.Text = ""
        '
        'txtGoldLoss2
        '
        Me.txtGoldLoss2.BackColor = System.Drawing.Color.White
        Me.txtGoldLoss2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldLoss2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoldLoss2.ForeColor = System.Drawing.Color.Black
        Me.txtGoldLoss2.Location = New System.Drawing.Point(104, 176)
        Me.txtGoldLoss2.MaxLength = 20
        Me.txtGoldLoss2.Name = "txtGoldLoss2"
        Me.txtGoldLoss2.ReadOnly = True
        Me.txtGoldLoss2.Size = New System.Drawing.Size(97, 22)
        Me.txtGoldLoss2.TabIndex = 116
        Me.txtGoldLoss2.Tag = "NE;"
        Me.txtGoldLoss2.Text = ""
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(8, 184)
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
        Me.txtPCS2.Text = ""
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
        Me.txtSMC2.Text = ""
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
        Me.txtPMC2.Location = New System.Drawing.Point(104, 152)
        Me.txtPMC2.MaxLength = 20
        Me.txtPMC2.Name = "txtPMC2"
        Me.txtPMC2.ReadOnly = True
        Me.txtPMC2.Size = New System.Drawing.Size(97, 22)
        Me.txtPMC2.TabIndex = 110
        Me.txtPMC2.Tag = "NE;"
        Me.txtPMC2.Text = ""
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(8, 160)
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
        Me.txtCostPerGram2.Location = New System.Drawing.Point(104, 128)
        Me.txtCostPerGram2.MaxLength = 20
        Me.txtCostPerGram2.Name = "txtCostPerGram2"
        Me.txtCostPerGram2.ReadOnly = True
        Me.txtCostPerGram2.Size = New System.Drawing.Size(97, 22)
        Me.txtCostPerGram2.TabIndex = 108
        Me.txtCostPerGram2.Tag = "NE;"
        Me.txtCostPerGram2.Text = ""
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(8, 136)
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
        Me.txtDiamondDescription2.Text = ""
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
        Me.btnInventorySearch2.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnInventorySearch2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInventorySearch2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInventorySearch2.Location = New System.Drawing.Point(200, 8)
        Me.btnInventorySearch2.Name = "btnInventorySearch2"
        Me.btnInventorySearch2.Size = New System.Drawing.Size(24, 22)
        Me.btnInventorySearch2.TabIndex = 105
        Me.btnInventorySearch2.Text = "..."
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
        Me.txtStoneWeight2.Text = ""
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
        Me.txtWeight2.Location = New System.Drawing.Point(104, 104)
        Me.txtWeight2.MaxLength = 20
        Me.txtWeight2.Name = "txtWeight2"
        Me.txtWeight2.ReadOnly = True
        Me.txtWeight2.Size = New System.Drawing.Size(97, 22)
        Me.txtWeight2.TabIndex = 323
        Me.txtWeight2.Tag = "NE;"
        Me.txtWeight2.Text = ""
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(8, 112)
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
        Me.txtDescription2.Text = ""
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
        Me.txtInventoryCode2.Text = ""
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
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.txtSize)
        Me.Panel3.Controls.Add(Me.Label37)
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
        Me.Panel3.Location = New System.Drawing.Point(20, 296)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(796, 144)
        Me.Panel3.TabIndex = 145
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
        Me.txtDiamondDescription.Location = New System.Drawing.Point(104, 80)
        Me.txtDiamondDescription.Name = "txtDiamondDescription"
        Me.txtDiamondDescription.Size = New System.Drawing.Size(280, 22)
        Me.txtDiamondDescription.TabIndex = 5
        Me.txtDiamondDescription.Text = ""
        '
        'txtDescription
        '
        Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.Location = New System.Drawing.Point(104, 56)
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(280, 22)
        Me.txtDescription.TabIndex = 4
        Me.txtDescription.Text = ""
        '
        'Label34
        '
        Me.Label34.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(8, 32)
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
        Me.txtPCS.Text = ""
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
        Me.txtStoneWeight.Location = New System.Drawing.Point(504, 104)
        Me.txtStoneWeight.MaxLength = 20
        Me.txtStoneWeight.Name = "txtStoneWeight"
        Me.txtStoneWeight.ReadOnly = True
        Me.txtStoneWeight.Size = New System.Drawing.Size(97, 22)
        Me.txtStoneWeight.TabIndex = 128
        Me.txtStoneWeight.Tag = "NE;"
        Me.txtStoneWeight.Text = ""
        '
        'Label32
        '
        Me.Label32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(408, 104)
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
        Me.txtTotalCost.Text = ""
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
        Me.txtSMC.Text = ""
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
        Me.txtPMC.Text = ""
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
        Me.txtGoldLoss.Location = New System.Drawing.Point(504, 80)
        Me.txtGoldLoss.MaxLength = 20
        Me.txtGoldLoss.Name = "txtGoldLoss"
        Me.txtGoldLoss.ReadOnly = True
        Me.txtGoldLoss.Size = New System.Drawing.Size(97, 22)
        Me.txtGoldLoss.TabIndex = 119
        Me.txtGoldLoss.Tag = "NE;"
        Me.txtGoldLoss.Text = ""
        '
        'Label28
        '
        Me.Label28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(408, 80)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(64, 16)
        Me.Label28.TabIndex = 120
        Me.Label28.Text = "Gold Loss"
        '
        'Label27
        '
        Me.Label27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(8, 80)
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
        Me.txtCostPerGram.Location = New System.Drawing.Point(504, 56)
        Me.txtCostPerGram.MaxLength = 20
        Me.txtCostPerGram.Name = "txtCostPerGram"
        Me.txtCostPerGram.ReadOnly = True
        Me.txtCostPerGram.Size = New System.Drawing.Size(97, 22)
        Me.txtCostPerGram.TabIndex = 4
        Me.txtCostPerGram.Tag = "NE;"
        Me.txtCostPerGram.Text = ""
        '
        'Label23
        '
        Me.Label23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(408, 56)
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
        Me.txtWeight.Text = ""
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
        Me.Label25.Location = New System.Drawing.Point(8, 56)
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
        Me.txtInventoryCode.Location = New System.Drawing.Point(104, 33)
        Me.txtInventoryCode.MaxLength = 20
        Me.txtInventoryCode.Name = "txtInventoryCode"
        Me.txtInventoryCode.Size = New System.Drawing.Size(96, 22)
        Me.txtInventoryCode.TabIndex = 0
        Me.txtInventoryCode.Tag = "NE;"
        Me.txtInventoryCode.Text = ""
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
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(0, 0)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(210, 75)
        Me.pic.TabIndex = 113
        Me.pic.TabStop = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.Location = New System.Drawing.Point(584, 448)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.TabIndex = 7
        Me.btnSave.Text = "Merge (F7)"
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(664, 448)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.TabIndex = 8
        Me.btnClear.Text = "Clear (F6)"
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(744, 448)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.TabIndex = 9
        Me.btnClose.Text = "Close (Esc)"
        '
        'Label35
        '
        Me.Label35.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(8, 88)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(88, 16)
        Me.Label35.TabIndex = 120
        Me.Label35.Text = "Length/Size"
        '
        'txtSize1
        '
        Me.txtSize1.BackColor = System.Drawing.Color.White
        Me.txtSize1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSize1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSize1.ForeColor = System.Drawing.Color.Black
        Me.txtSize1.Location = New System.Drawing.Point(104, 80)
        Me.txtSize1.MaxLength = 20
        Me.txtSize1.Name = "txtSize1"
        Me.txtSize1.ReadOnly = True
        Me.txtSize1.Size = New System.Drawing.Size(97, 22)
        Me.txtSize1.TabIndex = 121
        Me.txtSize1.Tag = "NE;"
        Me.txtSize1.Text = ""
        '
        'Label36
        '
        Me.Label36.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.Location = New System.Drawing.Point(8, 88)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(88, 16)
        Me.Label36.TabIndex = 324
        Me.Label36.Text = "Length/Size"
        '
        'txtSize2
        '
        Me.txtSize2.BackColor = System.Drawing.Color.White
        Me.txtSize2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSize2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSize2.ForeColor = System.Drawing.Color.Black
        Me.txtSize2.Location = New System.Drawing.Point(104, 80)
        Me.txtSize2.MaxLength = 20
        Me.txtSize2.Name = "txtSize2"
        Me.txtSize2.ReadOnly = True
        Me.txtSize2.Size = New System.Drawing.Size(97, 22)
        Me.txtSize2.TabIndex = 325
        Me.txtSize2.Tag = "NE;"
        Me.txtSize2.Text = ""
        '
        'Label37
        '
        Me.Label37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(408, 32)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(72, 16)
        Me.Label37.TabIndex = 336
        Me.Label37.Text = "Length/Size"
        '
        'txtSize
        '
        Me.txtSize.BackColor = System.Drawing.Color.White
        Me.txtSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSize.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSize.ForeColor = System.Drawing.Color.Black
        Me.txtSize.Location = New System.Drawing.Point(504, 32)
        Me.txtSize.MaxLength = 20
        Me.txtSize.Name = "txtSize"
        Me.txtSize.ReadOnly = True
        Me.txtSize.Size = New System.Drawing.Size(97, 22)
        Me.txtSize.TabIndex = 337
        Me.txtSize.Tag = "NE;"
        Me.txtSize.Text = ""
        '
        'frmMergeInventoryML
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(834, 488)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmMergeInventoryML"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Merge Inventory"
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
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
            If txtInventoryCode1.Text.Trim <> txtInventoryCode2.Text.Trim Then
                subGetInventory2Detail()
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
        Try
            If objClsInvMerge.IsInventory(txtInventoryCode1.Text.Trim) Then
                dsInv = objClsInvMerge.GetInventoryDetail(txtInventoryCode1.Text.Trim)
                If dsInv.Tables(0).Rows.Count > 0 Then
                    txtInventoryCode1.Text = Trim(dsInv.Tables(0).Rows(0)("InventoryCode") & "")
                    txtDescription1.Text = Trim(dsInv.Tables(0).Rows(0)("Description") & "")
                    txtDiamondDescription1.Text = Trim(dsInv.Tables(0).Rows(0)("DiamondDescription") & "")
                    txtStoneWeight1.Text = Format(Val(dsInv.Tables(0).Rows(0)("StoneWeight") & ""), "0.00")
                    txtSize1.Text = Format(Val(dsInv.Tables(0).Rows(0)("Size") & ""), "0.00")
                    txtWeight1.Text = Format(Val(dsInv.Tables(0).Rows(0)("Weight") & ""), "0.00")
                    txtCostPerGram1.Text = Format(Val(dsInv.Tables(0).Rows(0)("CostperGram")), "0.00")
                    txtPMC1.Text = Format(Val(dsInv.Tables(0).Rows(0)("PMC")), "0.00")
                    txtSMC1.Text = Format(Val(dsInv.Tables(0).Rows(0)("sMc")), "0.00")
                    txtTotalCost1.Text = Format(Val(dsInv.Tables(0).Rows(0)("TotalCost") & ""), "0.00")
                    txtPCS1.Text = Val(dsInv.Tables(0).Rows(0)("PCS"))
                    txtStoneWeight1.Text = Format(Val(dsInv.Tables(0).Rows(0)("StoneWeight")), "0.00")
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
        Try

            If objClsInvMerge.IsInventory(txtInventoryCode2.Text.Trim) Then
                dsInv = objClsInvMerge.GetInventoryDetail(txtInventoryCode2.Text.Trim)
                If dsInv.Tables(0).Rows.Count > 0 Then
                    If sClassCode.Trim = Trim(dsInv.Tables(0).Rows(0)("ClassCode")) Then
                        txtInventoryCode2.Text = Trim(dsInv.Tables(0).Rows(0)("InventoryCode") & "")
                        txtDescription2.Text = Trim(dsInv.Tables(0).Rows(0)("Description") & "")
                        txtDiamondDescription2.Text = Trim(dsInv.Tables(0).Rows(0)("DiamondDescription") & "")
                        txtStoneWeight2.Text = Format(Val(dsInv.Tables(0).Rows(0)("StoneWeight") & ""), "0.00")
                        txtSize2.Text = Format(Val(dsInv.Tables(0).Rows(0)("Size") & ""), "0.00")
                        txtWeight2.Text = Format(Val(dsInv.Tables(0).Rows(0)("Weight") & ""), "0.00")
                        txtCostPerGram2.Text = Format(Val(dsInv.Tables(0).Rows(0)("CostperGram")), "0.00")
                        txtPMC2.Text = Format(Val(dsInv.Tables(0).Rows(0)("PMC")), "0.00")
                        txtSMC2.Text = Format(Val(dsInv.Tables(0).Rows(0)("sMc")), "0.00")
                        txtTotalCost2.Text = Format(Val(dsInv.Tables(0).Rows(0)("TotalCost") & ""), "0.00")
                        txtPCS2.Text = Val(dsInv.Tables(0).Rows(0)("PCS"))
                        txtStoneWeight2.Text = Format(Val(dsInv.Tables(0).Rows(0)("StoneWeight")), "0.00")
                        txtInventoryCode2.ReadOnly = True
                        subMergedDetail()
                        cboCategory.Focus()
                    Else
                        MsgBox("Class code is not the same to merge!", MsgBoxStyle.Critical)
                        txtInventoryCode2.Clear()
                        txtInventoryCode2.Focus()
                        Exit Sub
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
        txtWeight.Text = Format(Val(txtWeight1.Text) + Val(txtWeight2.Text), "0.00")
        ''aru
        txtSize.Text = Format(Val(txtSize1.Text) + Val(txtSize2.Text), "0.00")
        txtGoldLoss.Text = Format(Val(txtGoldLoss1.Text) + Val(txtGoldLoss2.Text), "0.00")
        txtTotalCost.Text = Format(Val(txtTotalCost1.Text) + Val(txtTotalCost2.Text), "0.00")
        txtStoneWeight.Text = Format(Val(txtStoneWeight1.Text) + Val(txtStoneWeight2.Text), "0.00")
        txtPCS.Text = Val(txtPCS1.Text)
        txtCostPerGram.Text = Format(Val(txtCostPerGram1.Text), "0.00")
        txtSMC.Text = Format(Val(txtSMC1.Text) + Val(txtSMC2.Text), "0.00")
        txtPMC.Text = Format(Val(txtPMC1.Text) + Val(txtPMC2.Text), "0.00")
        Try
            ds = objClsInvMerge.GetCategory(sClassCode)
            If ds.Tables(0).Rows.Count > 0 Then
                For icnt = 0 To ds.Tables(0).Rows.Count - 1
                    cboCategory.Items.Add(Trim(ds.Tables(0).Rows(icnt)("categorycode") & ""))
                Next
            End If
            ' cboCategory.SelectedIndex = 0
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
            ElseIf txtInventoryCode2.Text.Trim = "" Then
                MsgBox("Second InventoryCode is empty!", MsgBoxStyle.Exclamation)
                txtInventoryCode2.Focus()
                Exit Sub
            ElseIf txtInventoryCode.Text.Trim = "" Then
                MsgBox("New InventoryCode is empty!", MsgBoxStyle.Exclamation)
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
                '''subPrintBarcode(txtInventoryCode.Text.Trim)
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
        txtSize.Clear()
        txtSize1.Clear()
        txtSize2.Clear()
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
    End Sub
    Private Function fncInventory(ByRef sinventorycode As String) As Boolean
        Dim bUptationStatus As Boolean
        Dim bPurchase As Boolean
        bUptationStatus = False
        fncInventory = False
        Try
            dPCS = 1
            If Trim(sPurchaseCostingGL) = "Y" Then
                dTotalCost = (Val(txtWeight.Text.Trim) + Val(dGoldLoss)) * Val(dCostPerGram) + dPMC
            Else
                dTotalCost = (Val(txtWeight.Text.Trim) * dCostPerGram) + dPMC
            End If
            If Not objClsInvMerge.InventorySaveML("", _
                        Trim(txtDescription.Text), Trim(txtDiamondDescription.Text), _
                        Val(txtSize.Text.Trim), (txtWeight.Text.Trim), Val(txtStoneWeight.Text), Val(txtCostPerGram.Text), _
                        Val(txtPMC.Text), (Val(txtSMC.Text)), Val(txtPCS.Text), Val(txtGoldLoss.Text), Val(dTotalCost), _
                        Trim(sJobsheetNo), Trim(sPurchaseBillNo), Trim(dPurchaseDate), Trim(sSupplierCode), _
                        "", "", 0, 0, 0, Trim(sCategoryCode), Trim(sClassCode), Trim(sCatalogueCode), Trim(sTrayId), _
                        Trim$(sPMCType), Trim$(sSMCType), CLng(dPCS), 0, Trim(sRemarks)) Then
                fncInventory = False
                Exit Function
            End If
            dPurchaseDate = Format(dPurchaseDate, "dd/MM/yy")
            If Format(Now(), "dd/MM/yy") <> dCreateDate Then
                bPurchase = objClsInvMerge.PurchaseSaveML(Trim(sJobsheetNo), _
                            Trim(sPurchaseBillNo), "", Trim(sSupplierCode), Trim(txtInventoryCode.Text.Trim), _
                            Trim(txtDescription.Text.Trim), Trim(sDiamondDescription), Val(dStoneWeight), _
                            Val(dCostPerGram), Val(txtSize.Text.Trim), Val(txtWeight.Text.Trim), Val(dPMC), Val(dPCS), Val(dGoldLoss), Val(dTotalCost), _
                            Trim(sCatalogueCode), Trim(sCategoryCode), Trim(sClassCode), Trim(sTrayId), "", "ST")
            Else
                bPurchase = objClsInvMerge.PurchaseSaveML(Trim(sJobsheetNo), _
                            Trim(sPurchaseBillNo), "", Trim(sSupplierCode), Trim(txtInventoryCode.Text.Trim), _
                            Trim(txtDescription.Text.Trim), Trim(sDiamondDescription), Val(dStoneWeight), _
                            Val(dCostPerGram), Val(txtSize.Text.Trim), Val(txtWeight.Text.Trim), Val(dPMC), Val(dPCS), Val(dGoldLoss), Val(dTotalCost), _
                            Trim(sCatalogueCode), Trim(sCategoryCode), Trim(sClassCode), Trim(sTrayId), "", Trim(sRemarks))
            End If

            bUptationStatus = objClsInvMerge.InventoryMergedUptation(Trim(txtInventoryCode1.Text), Trim(txtInventoryCode.Text), 0, "MERGED INVENTORY")
            bUptationStatus = objClsInvMerge.InventoryMergedUptation(Trim(txtInventoryCode2.Text), Trim(txtInventoryCode.Text), 0, "MERGED INVENTORY")

            bUptationStatus = objClsInvMerge.MergeInvHistorySaveML(Trim(txtInventoryCode1.Text), _
                          Val(txtSize1.Text), Val(txtWeight1.Text.Trim), Val(txtPMC1.Text), (Val(txtSMC1.Text)), _
                           Val(txtGoldLoss1.Text), Val(txtTotalCost1.Text), _
                           Trim(txtInventoryCode2.Text), _
                           Val(txtSize2.Text), (txtWeight2.Text.Trim), Val(txtPMC2.Text), (Val(txtSMC2.Text)), _
                           Val(txtGoldLoss2.Text), Val(txtTotalCost2.Text), _
                           Trim(txtInventoryCode.Text), _
                           Val(txtSize.Text), (txtWeight.Text.Trim), Val(txtPMC.Text), (Val(txtSMC.Text)), _
                           Val(txtGoldLoss.Text), Val(txtTotalCost.Text), Trim(txtDescription.Text.Trim), _
                           Trim(sJobsheetNo), Trim(sClassCode))


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


    'Private Sub cboCategory_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCategory.LostFocus
    '    Dim ds As DataSet
    '    Try
    '        ds = objClsInvMerge.GetDetailForCategory(cboCategory.Text.Trim)
    '        If ds.Tables(0).Rows.Count > 0 Then
    '            txtInventoryCode.Text = Format(CInt(ds.Tables(0).Rows(0)("nextInventoryCode")), "000000")
    '            txtInventoryCode.ReadOnly = True
    '        End If
    '    Catch ex As Exception
    '        MsgBox(Err.Description)
    '    Finally
    '        Err.Clear()
    '        ds.Dispose()
    '    End Try

    'End Sub

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

    Private Sub subPrintBarcode(ByVal sinventorycode As String)
        Try
            Dim dsInv As New DataSet
            Dim dsCls As New DataSet
            Dim dsMCC As New DataSet
            Dim sSelection As String
            Dim CR As New ReportDocument
            Dim frmrep As New frmReports
            Dim sClassName, sChar, sTemp As String
            Dim dNo As Double
            Dim objinv As New clsInventory
            Dim objCls As New ClsClass

            If sinventorycode = "Total" Or sinventorycode = "" Then Exit Sub

            dsInv = objinv.GetInventoryDetail(Trim(sinventorycode))

            If dsInv.Tables(0).Rows.Count = 0 Then
                MessageBox.Show("Inventory not found!")
                Exit Sub
            Else
                If Trim$(dsInv.Tables(0).Rows(0)("ClassCode") & "") <> "" Then
                    dsCls = objCls.GetClassDetail(Trim$(dsInv.Tables(0).Rows(0)("ClassCode") & ""))
                    If dsCls.Tables(0).Rows.Count > 0 Then sClassName = Trim$(dsCls.Tables(0).Rows(0)("ClassName") & "")
                End If
            End If
            dNo = Format$(Val(dsInv.Tables(0).Rows(0)("sMc") & ""), "###0")
            sChar = ""
            dsMCC = objinv.GetMcConvertion(dNo)
            If dsMCC.Tables(0).Rows.Count > 0 Then
                sChar = Mid(Trim(dsMCC.Tables(0).Rows(0)("Mc") & ""), 1, Len(Trim(dsMCC.Tables(0).Rows(0)("Mc") & "")) - 3) 'Remove the decimal part
            End If
            sTemp = Trim$(dsInv.Tables(0).Rows(0)("Description") & "")

            strpath = Application.StartupPath & "\Reports\rptBarcode.rpt"
            CR.Load(strpath)
            ReportLogOnToServer(CR)
            CR.Refresh()
            frmrep.ReportViewer.ReportSource = CR
            CR.DataDefinition.FormulaFields("Barcode").Text = fncFormatSQLText(Trim(dsInv.Tables(0).Rows(0)("Inventorycode") & ""))
            CR.DataDefinition.FormulaFields("Description").Text = fncFormatSQLText(Trim(dsInv.Tables(0).Rows(0)("Description") & ""))
            CR.DataDefinition.FormulaFields("TotalWeight").Text = fncFormatSQLText("NWT : " & Trim(Format(Val(dsInv.Tables(0).Rows(0)("Weight") & ""), "0.00")) & "gm")
            CR.DataDefinition.FormulaFields("MC").Text = fncFormatSQLText("MC :$ " & Trim(Format(Val(dsInv.Tables(0).Rows(0)("sMC") & ""), "0.00")))
            CR.DataDefinition.FormulaFields("GoldLoss").Text = fncFormatSQLText(Trim(dsInv.Tables(0).Rows(0)("SupplierCode") & "") & ": " & Format(dsInv.Tables(0).Rows(0)("PurchaseDate"), "MM/yyyy") & "(" & Trim(Format(dsInv.Tables(0).Rows(0)("GoldLoss") & "", "0.00")) & "gm")
            CR.DataDefinition.FormulaFields("Wt").Text = fncFormatSQLText("GWT :" & Trim(Format(dsInv.Tables(0).Rows(0)("Weight") & "", "0.00")) & "gm")
            CR.DataDefinition.FormulaFields("ClassCode").Text = fncFormatSQLText(Trim(dsInv.Tables(0).Rows(0)("ClassCode") & ""))
            'CR.PrintToPrinter(1, True, 0, 0)
            frmrep.Show()
            frmrep.ReportViewer.Visible = True
            frmrep.ReportViewer.DisplayToolbar = True
            frmrep.ReportViewer.CloseView(False)
            frmrep.WindowState = FormWindowState.Maximized
            frmrep = Nothing
            frmrep = New frmReports

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub cboCategory_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCategory.LostFocus
        Dim ds As DataSet
        Try
            ds = objClsInvMerge.GetDetailForCategory(cboCategory.Text.Trim)
            If ds.Tables(0).Rows.Count > 0 Then
                txtInventoryCode.Text = (ds.Tables(0).Rows(0)("nextInventoryCode"))
                txtInventoryCode.ReadOnly = True
            End If
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
            ds.Dispose()
        End Try

    End Sub

End Class
