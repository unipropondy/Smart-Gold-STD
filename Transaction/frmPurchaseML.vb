Public Class frmPurchaseML
    Inherits System.Windows.Forms.Form
    Dim dm As New DataManager
    Dim masPriceCode(9) As String
    Dim sCurrencycode As String
    Dim CompName As String
    Dim Address1 As String
    Dim Address2 As String
    Dim Country As String
    Dim PhoneNo As String
    Dim FaxNo As String
    Dim sBillClose As String
    Dim sTranType As String
    Dim bEdited As Boolean
    Dim mspurchasetype As String
    Dim sClassCode As String
    Dim sCatalogueCode As String
    Dim sCategoryCode As String
    Dim sJobSheetCode As String
    Dim dCost As Double
    Dim dLocalCost As Double
    Dim dForeignCost As Double
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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnJobSheetNoSearch As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtIYYYY As System.Windows.Forms.TextBox
    Friend WithEvents txtIMM As System.Windows.Forms.TextBox
    Friend WithEvents txtIDD As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents optGold As System.Windows.Forms.RadioButton
    Friend WithEvents optCash As System.Windows.Forms.RadioButton
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents chkGST As System.Windows.Forms.CheckBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents txtJobsheetNo As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtInventoryCode As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents optSMCPCS As System.Windows.Forms.RadioButton
    Friend WithEvents optSMCGram As System.Windows.Forms.RadioButton
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents optPMCPCS As System.Windows.Forms.RadioButton
    Friend WithEvents optPMCGram As System.Windows.Forms.RadioButton
    Friend WithEvents txtInventoryPCS As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtInventoryWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtBillPCS As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtBillWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents btnTrayIdSearch As System.Windows.Forms.Button
    Friend WithEvents txtTrayId As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtCategoryCode As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents btnCatalogueCodeSearch As System.Windows.Forms.Button
    Friend WithEvents txtCatalogueCode As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnBarCode As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnBillClose As System.Windows.Forms.Button
    Friend WithEvents btnBillClear As System.Windows.Forms.Button
    Friend WithEvents txtInventoryDiamondDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtInventoryDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtTEAL As System.Windows.Forms.TextBox
    Friend WithEvents txtCostPerGram As System.Windows.Forms.TextBox
    Friend WithEvents txtCurrencyRate As System.Windows.Forms.TextBox
    Friend WithEvents txtCurrencyName As System.Windows.Forms.TextBox
    Friend WithEvents txtCurrencyCode As System.Windows.Forms.TextBox
    Friend WithEvents txtSupplierName As System.Windows.Forms.TextBox
    Friend WithEvents btnSupplierCodeSearch As System.Windows.Forms.Button
    Friend WithEvents txtSupplierCode As System.Windows.Forms.TextBox
    Friend WithEvents txtInvoiceNo As System.Windows.Forms.TextBox
    Friend WithEvents grdJobSheet As AxFPSpread.AxvaSpread
    Friend WithEvents grdPurchase As AxFPSpread.AxvaSpread
    Friend WithEvents txtSMCCost As System.Windows.Forms.TextBox
    Friend WithEvents txtPMCCost As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalCost As System.Windows.Forms.TextBox
    Friend WithEvents txtPCS As System.Windows.Forms.TextBox
    Friend WithEvents txtSMC As System.Windows.Forms.TextBox
    Friend WithEvents txtPMC As System.Windows.Forms.TextBox
    Friend WithEvents txtStoneWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtCostPerGramI As System.Windows.Forms.TextBox
    Friend WithEvents txtGoldLoss As System.Windows.Forms.TextBox
    Friend WithEvents txtWeight As System.Windows.Forms.TextBox
    Friend WithEvents lblAutoManual As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents chkAutoSMC As System.Windows.Forms.CheckBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txtSize As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmPurchaseML))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtRemarks = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.chkGST = New System.Windows.Forms.CheckBox
        Me.txtTEAL = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtCostPerGram = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.optGold = New System.Windows.Forms.RadioButton
        Me.optCash = New System.Windows.Forms.RadioButton
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtCurrencyName = New System.Windows.Forms.TextBox
        Me.txtCurrencyCode = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtSupplierName = New System.Windows.Forms.TextBox
        Me.btnSupplierCodeSearch = New System.Windows.Forms.Button
        Me.txtSupplierCode = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtIYYYY = New System.Windows.Forms.TextBox
        Me.txtIMM = New System.Windows.Forms.TextBox
        Me.txtIDD = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnJobSheetNoSearch = New System.Windows.Forms.Button
        Me.txtJobsheetNo = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtCurrencyRate = New System.Windows.Forms.TextBox
        Me.grdJobSheet = New AxFPSpread.AxvaSpread
        Me.grdPurchase = New AxFPSpread.AxvaSpread
        Me.btnClose = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtSize = New System.Windows.Forms.TextBox
        Me.Label31 = New System.Windows.Forms.Label
        Me.lblAutoManual = New System.Windows.Forms.Label
        Me.btnAdd = New System.Windows.Forms.Button
        Me.btnBarCode = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.txtSMCCost = New System.Windows.Forms.TextBox
        Me.txtPMCCost = New System.Windows.Forms.TextBox
        Me.txtTotalCost = New System.Windows.Forms.TextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.txtPCS = New System.Windows.Forms.TextBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.txtSMC = New System.Windows.Forms.TextBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.txtPMC = New System.Windows.Forms.TextBox
        Me.Label29 = New System.Windows.Forms.Label
        Me.txtStoneWeight = New System.Windows.Forms.TextBox
        Me.txtInventoryDiamondDescription = New System.Windows.Forms.TextBox
        Me.txtCostPerGramI = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.txtGoldLoss = New System.Windows.Forms.TextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.txtWeight = New System.Windows.Forms.TextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.txtInventoryDescription = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.txtInventoryCode = New System.Windows.Forms.TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.chkAutoSMC = New System.Windows.Forms.CheckBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.optSMCPCS = New System.Windows.Forms.RadioButton
        Me.optSMCGram = New System.Windows.Forms.RadioButton
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Label20 = New System.Windows.Forms.Label
        Me.optPMCPCS = New System.Windows.Forms.RadioButton
        Me.optPMCGram = New System.Windows.Forms.RadioButton
        Me.txtInventoryPCS = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.txtInventoryWeight = New System.Windows.Forms.TextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.txtBillPCS = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.txtBillWeight = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.btnTrayIdSearch = New System.Windows.Forms.Button
        Me.txtTrayId = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtCategoryCode = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.btnCatalogueCodeSearch = New System.Windows.Forms.Button
        Me.txtCatalogueCode = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.btnBillClose = New System.Windows.Forms.Button
        Me.btnBillClear = New System.Windows.Forms.Button
        Me.pic = New System.Windows.Forms.PictureBox
        Me.GroupBox1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.grdJobSheet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdPurchase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.GroupBox1.Controls.Add(Me.txtRemarks)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.chkGST)
        Me.GroupBox1.Controls.Add(Me.txtTEAL)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txtCostPerGram)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Panel2)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtCurrencyName)
        Me.GroupBox1.Controls.Add(Me.txtCurrencyCode)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtSupplierName)
        Me.GroupBox1.Controls.Add(Me.btnSupplierCodeSearch)
        Me.GroupBox1.Controls.Add(Me.txtSupplierCode)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtIYYYY)
        Me.GroupBox1.Controls.Add(Me.txtIMM)
        Me.GroupBox1.Controls.Add(Me.txtIDD)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtInvoiceNo)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.btnJobSheetNoSearch)
        Me.GroupBox1.Controls.Add(Me.txtJobsheetNo)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox1.Location = New System.Drawing.Point(5, 80)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(618, 168)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "JobSheet Details"
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.Black
        Me.txtRemarks.Location = New System.Drawing.Point(104, 133)
        Me.txtRemarks.MaxLength = 20
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(504, 20)
        Me.txtRemarks.TabIndex = 17
        Me.txtRemarks.Tag = ""
        Me.txtRemarks.Text = ""
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(8, 133)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(88, 15)
        Me.Label12.TabIndex = 73
        Me.Label12.Text = "Remarks"
        '
        'chkGST
        '
        Me.chkGST.ForeColor = System.Drawing.Color.Black
        Me.chkGST.Location = New System.Drawing.Point(240, 106)
        Me.chkGST.Name = "chkGST"
        Me.chkGST.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkGST.Size = New System.Drawing.Size(48, 16)
        Me.chkGST.TabIndex = 14
        Me.chkGST.Text = "GST"
        '
        'txtTEAL
        '
        Me.txtTEAL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTEAL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTEAL.ForeColor = System.Drawing.Color.Black
        Me.txtTEAL.Location = New System.Drawing.Point(340, 104)
        Me.txtTEAL.MaxLength = 20
        Me.txtTEAL.Name = "txtTEAL"
        Me.txtTEAL.Size = New System.Drawing.Size(72, 20)
        Me.txtTEAL.TabIndex = 15
        Me.txtTEAL.Tag = ""
        Me.txtTEAL.Text = ""
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(296, 107)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(40, 15)
        Me.Label11.TabIndex = 71
        Me.Label11.Text = "TEAL"
        '
        'txtCostPerGram
        '
        Me.txtCostPerGram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCostPerGram.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostPerGram.ForeColor = System.Drawing.Color.Black
        Me.txtCostPerGram.Location = New System.Drawing.Point(512, 104)
        Me.txtCostPerGram.MaxLength = 20
        Me.txtCostPerGram.Name = "txtCostPerGram"
        Me.txtCostPerGram.Size = New System.Drawing.Size(96, 20)
        Me.txtCostPerGram.TabIndex = 16
        Me.txtCostPerGram.Tag = ""
        Me.txtCostPerGram.Text = ""
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(418, 104)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(88, 15)
        Me.Label10.TabIndex = 69
        Me.Label10.Text = "Cost Per Gram"
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(8, 109)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(88, 15)
        Me.Label9.TabIndex = 67
        Me.Label9.Text = "Payable"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.optGold)
        Me.Panel2.Controls.Add(Me.optCash)
        Me.Panel2.Location = New System.Drawing.Point(104, 104)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(128, 24)
        Me.Panel2.TabIndex = 66
        '
        'optGold
        '
        Me.optGold.Checked = True
        Me.optGold.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optGold.ForeColor = System.Drawing.Color.Black
        Me.optGold.Location = New System.Drawing.Point(8, 0)
        Me.optGold.Name = "optGold"
        Me.optGold.Size = New System.Drawing.Size(60, 24)
        Me.optGold.TabIndex = 12
        Me.optGold.TabStop = True
        Me.optGold.Text = "Gold"
        '
        'optCash
        '
        Me.optCash.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCash.ForeColor = System.Drawing.Color.Black
        Me.optCash.Location = New System.Drawing.Point(80, 0)
        Me.optCash.Name = "optCash"
        Me.optCash.Size = New System.Drawing.Size(56, 24)
        Me.optCash.TabIndex = 13
        Me.optCash.Text = "Cash"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(464, 79)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(32, 15)
        Me.Label8.TabIndex = 65
        Me.Label8.Text = "Rate"
        '
        'txtCurrencyName
        '
        Me.txtCurrencyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrencyName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencyName.ForeColor = System.Drawing.Color.Black
        Me.txtCurrencyName.Location = New System.Drawing.Point(232, 79)
        Me.txtCurrencyName.MaxLength = 20
        Me.txtCurrencyName.Name = "txtCurrencyName"
        Me.txtCurrencyName.Size = New System.Drawing.Size(224, 20)
        Me.txtCurrencyName.TabIndex = 10
        Me.txtCurrencyName.Tag = ""
        Me.txtCurrencyName.Text = ""
        '
        'txtCurrencyCode
        '
        Me.txtCurrencyCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrencyCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencyCode.ForeColor = System.Drawing.Color.Black
        Me.txtCurrencyCode.Location = New System.Drawing.Point(104, 79)
        Me.txtCurrencyCode.MaxLength = 20
        Me.txtCurrencyCode.Name = "txtCurrencyCode"
        Me.txtCurrencyCode.Size = New System.Drawing.Size(120, 20)
        Me.txtCurrencyCode.TabIndex = 9
        Me.txtCurrencyCode.Tag = ""
        Me.txtCurrencyCode.Text = ""
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(8, 82)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 15)
        Me.Label6.TabIndex = 62
        Me.Label6.Text = "Currency"
        '
        'txtSupplierName
        '
        Me.txtSupplierName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierName.ForeColor = System.Drawing.Color.Black
        Me.txtSupplierName.Location = New System.Drawing.Point(232, 52)
        Me.txtSupplierName.MaxLength = 20
        Me.txtSupplierName.Name = "txtSupplierName"
        Me.txtSupplierName.Size = New System.Drawing.Size(376, 20)
        Me.txtSupplierName.TabIndex = 8
        Me.txtSupplierName.Tag = ""
        Me.txtSupplierName.Text = ""
        '
        'btnSupplierCodeSearch
        '
        Me.btnSupplierCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnSupplierCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSupplierCodeSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSupplierCodeSearch.Location = New System.Drawing.Point(200, 52)
        Me.btnSupplierCodeSearch.Name = "btnSupplierCodeSearch"
        Me.btnSupplierCodeSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnSupplierCodeSearch.TabIndex = 7
        Me.btnSupplierCodeSearch.Text = "..."
        '
        'txtSupplierCode
        '
        Me.txtSupplierCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierCode.ForeColor = System.Drawing.Color.Black
        Me.txtSupplierCode.Location = New System.Drawing.Point(104, 52)
        Me.txtSupplierCode.MaxLength = 20
        Me.txtSupplierCode.Name = "txtSupplierCode"
        Me.txtSupplierCode.Size = New System.Drawing.Size(96, 20)
        Me.txtSupplierCode.TabIndex = 6
        Me.txtSupplierCode.Tag = ""
        Me.txtSupplierCode.Text = ""
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(8, 55)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(88, 15)
        Me.Label5.TabIndex = 58
        Me.Label5.Text = "Supplier Code"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(566, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(8, 16)
        Me.Label3.TabIndex = 56
        Me.Label3.Text = "-"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(534, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(8, 16)
        Me.Label4.TabIndex = 55
        Me.Label4.Text = "-"
        '
        'txtIYYYY
        '
        Me.txtIYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIYYYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIYYYY.Location = New System.Drawing.Point(576, 24)
        Me.txtIYYYY.MaxLength = 4
        Me.txtIYYYY.Name = "txtIYYYY"
        Me.txtIYYYY.Size = New System.Drawing.Size(32, 20)
        Me.txtIYYYY.TabIndex = 5
        Me.txtIYYYY.Tag = "ENN;"
        Me.txtIYYYY.Text = ""
        '
        'txtIMM
        '
        Me.txtIMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIMM.Location = New System.Drawing.Point(542, 24)
        Me.txtIMM.MaxLength = 2
        Me.txtIMM.Name = "txtIMM"
        Me.txtIMM.Size = New System.Drawing.Size(20, 20)
        Me.txtIMM.TabIndex = 4
        Me.txtIMM.Tag = "ENN;"
        Me.txtIMM.Text = ""
        '
        'txtIDD
        '
        Me.txtIDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIDD.Location = New System.Drawing.Point(510, 24)
        Me.txtIDD.MaxLength = 2
        Me.txtIDD.Name = "txtIDD"
        Me.txtIDD.Size = New System.Drawing.Size(20, 20)
        Me.txtIDD.TabIndex = 3
        Me.txtIDD.Tag = "ENN;"
        Me.txtIDD.Text = ""
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(418, 24)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 23)
        Me.Label7.TabIndex = 54
        Me.Label7.Text = "Invoice Date"
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvoiceNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceNo.ForeColor = System.Drawing.Color.Black
        Me.txtInvoiceNo.Location = New System.Drawing.Point(298, 24)
        Me.txtInvoiceNo.MaxLength = 20
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(104, 20)
        Me.txtInvoiceNo.TabIndex = 2
        Me.txtInvoiceNo.Tag = ""
        Me.txtInvoiceNo.Text = ""
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(232, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 15)
        Me.Label2.TabIndex = 45
        Me.Label2.Text = "Invoice No"
        '
        'btnJobSheetNoSearch
        '
        Me.btnJobSheetNoSearch.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnJobSheetNoSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnJobSheetNoSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnJobSheetNoSearch.Location = New System.Drawing.Point(200, 24)
        Me.btnJobSheetNoSearch.Name = "btnJobSheetNoSearch"
        Me.btnJobSheetNoSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnJobSheetNoSearch.TabIndex = 1
        Me.btnJobSheetNoSearch.TabStop = False
        Me.btnJobSheetNoSearch.Text = "..."
        '
        'txtJobsheetNo
        '
        Me.txtJobsheetNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJobsheetNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJobsheetNo.ForeColor = System.Drawing.Color.Black
        Me.txtJobsheetNo.Location = New System.Drawing.Point(104, 24)
        Me.txtJobsheetNo.MaxLength = 20
        Me.txtJobsheetNo.Name = "txtJobsheetNo"
        Me.txtJobsheetNo.Size = New System.Drawing.Size(96, 20)
        Me.txtJobsheetNo.TabIndex = 1
        Me.txtJobsheetNo.Tag = ""
        Me.txtJobsheetNo.Text = ""
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(8, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 15)
        Me.Label1.TabIndex = 42
        Me.Label1.Text = "JobSheet No."
        '
        'txtCurrencyRate
        '
        Me.txtCurrencyRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrencyRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencyRate.ForeColor = System.Drawing.Color.Black
        Me.txtCurrencyRate.Location = New System.Drawing.Point(536, 256)
        Me.txtCurrencyRate.MaxLength = 20
        Me.txtCurrencyRate.Name = "txtCurrencyRate"
        Me.txtCurrencyRate.Size = New System.Drawing.Size(96, 20)
        Me.txtCurrencyRate.TabIndex = 11
        Me.txtCurrencyRate.Tag = ""
        Me.txtCurrencyRate.Text = ""
        '
        'grdJobSheet
        '
        Me.grdJobSheet.Location = New System.Drawing.Point(630, 80)
        Me.grdJobSheet.Name = "grdJobSheet"
        Me.grdJobSheet.OcxState = CType(resources.GetObject("grdJobSheet.OcxState"), System.Windows.Forms.AxHost.State)
        Me.grdJobSheet.Size = New System.Drawing.Size(346, 168)
        Me.grdJobSheet.TabIndex = 42
        '
        'grdPurchase
        '
        Me.grdPurchase.Location = New System.Drawing.Point(4, 256)
        Me.grdPurchase.Name = "grdPurchase"
        Me.grdPurchase.OcxState = CType(resources.GetObject("grdPurchase.OcxState"), System.Windows.Forms.AxHost.State)
        Me.grdPurchase.Size = New System.Drawing.Size(972, 168)
        Me.grdPurchase.TabIndex = 43
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(884, 592)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(92, 23)
        Me.btnClose.TabIndex = 51
        Me.btnClose.Text = "Close(Esc)"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txtSize)
        Me.Panel1.Controls.Add(Me.Label31)
        Me.Panel1.Controls.Add(Me.lblAutoManual)
        Me.Panel1.Controls.Add(Me.btnAdd)
        Me.Panel1.Controls.Add(Me.btnBarCode)
        Me.Panel1.Controls.Add(Me.btnDelete)
        Me.Panel1.Controls.Add(Me.btnClear)
        Me.Panel1.Controls.Add(Me.txtSMCCost)
        Me.Panel1.Controls.Add(Me.txtPMCCost)
        Me.Panel1.Controls.Add(Me.txtTotalCost)
        Me.Panel1.Controls.Add(Me.Label30)
        Me.Panel1.Controls.Add(Me.txtPCS)
        Me.Panel1.Controls.Add(Me.Label27)
        Me.Panel1.Controls.Add(Me.txtSMC)
        Me.Panel1.Controls.Add(Me.Label28)
        Me.Panel1.Controls.Add(Me.txtPMC)
        Me.Panel1.Controls.Add(Me.Label29)
        Me.Panel1.Controls.Add(Me.txtStoneWeight)
        Me.Panel1.Controls.Add(Me.txtInventoryDiamondDescription)
        Me.Panel1.Controls.Add(Me.txtCostPerGramI)
        Me.Panel1.Controls.Add(Me.Label24)
        Me.Panel1.Controls.Add(Me.txtGoldLoss)
        Me.Panel1.Controls.Add(Me.Label25)
        Me.Panel1.Controls.Add(Me.txtWeight)
        Me.Panel1.Controls.Add(Me.Label26)
        Me.Panel1.Controls.Add(Me.txtInventoryDescription)
        Me.Panel1.Controls.Add(Me.Label23)
        Me.Panel1.Controls.Add(Me.txtInventoryCode)
        Me.Panel1.Controls.Add(Me.Label22)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.txtInventoryPCS)
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Controls.Add(Me.txtInventoryWeight)
        Me.Panel1.Controls.Add(Me.Label19)
        Me.Panel1.Controls.Add(Me.txtBillPCS)
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Controls.Add(Me.txtBillWeight)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.btnTrayIdSearch)
        Me.Panel1.Controls.Add(Me.txtTrayId)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.txtCategoryCode)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.btnCatalogueCodeSearch)
        Me.Panel1.Controls.Add(Me.txtCatalogueCode)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Location = New System.Drawing.Point(4, 432)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(972, 152)
        Me.Panel1.TabIndex = 52
        '
        'txtSize
        '
        Me.txtSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSize.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSize.ForeColor = System.Drawing.Color.Black
        Me.txtSize.Location = New System.Drawing.Point(360, 72)
        Me.txtSize.MaxLength = 20
        Me.txtSize.Name = "txtSize"
        Me.txtSize.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtSize.Size = New System.Drawing.Size(72, 20)
        Me.txtSize.TabIndex = 33
        Me.txtSize.Tag = ""
        Me.txtSize.Text = ""
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(192, Byte), CType(128, Byte))
        Me.Label31.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.Color.Black
        Me.Label31.Location = New System.Drawing.Point(360, 48)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(72, 24)
        Me.Label31.TabIndex = 151
        Me.Label31.Text = "Length/Size"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAutoManual
        '
        Me.lblAutoManual.BackColor = System.Drawing.Color.Green
        Me.lblAutoManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAutoManual.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAutoManual.ForeColor = System.Drawing.Color.Black
        Me.lblAutoManual.Location = New System.Drawing.Point(544, 120)
        Me.lblAutoManual.Name = "lblAutoManual"
        Me.lblAutoManual.Size = New System.Drawing.Size(104, 23)
        Me.lblAutoManual.TabIndex = 48
        Me.lblAutoManual.Text = "MANUAL (F10)"
        Me.lblAutoManual.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnAdd
        '
        Me.btnAdd.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnAdd.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.Image = CType(resources.GetObject("btnAdd.Image"), System.Drawing.Image)
        Me.btnAdd.Location = New System.Drawing.Point(652, 120)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.TabIndex = 44
        Me.btnAdd.Text = "Add "
        '
        'btnBarCode
        '
        Me.btnBarCode.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnBarCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBarCode.Image = CType(resources.GetObject("btnBarCode.Image"), System.Drawing.Image)
        Me.btnBarCode.Location = New System.Drawing.Point(892, 120)
        Me.btnBarCode.Name = "btnBarCode"
        Me.btnBarCode.TabIndex = 47
        Me.btnBarCode.Text = "BarCode"
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.Location = New System.Drawing.Point(732, 120)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.TabIndex = 45
        Me.btnDelete.Text = "Delete"
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(812, 120)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.TabIndex = 46
        Me.btnClear.Text = "Clear"
        '
        'txtSMCCost
        '
        Me.txtSMCCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSMCCost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMCCost.ForeColor = System.Drawing.Color.Black
        Me.txtSMCCost.Location = New System.Drawing.Point(721, 92)
        Me.txtSMCCost.MaxLength = 20
        Me.txtSMCCost.Name = "txtSMCCost"
        Me.txtSMCCost.ReadOnly = True
        Me.txtSMCCost.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtSMCCost.Size = New System.Drawing.Size(72, 20)
        Me.txtSMCCost.TabIndex = 41
        Me.txtSMCCost.Tag = ""
        Me.txtSMCCost.Text = ""
        '
        'txtPMCCost
        '
        Me.txtPMCCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPMCCost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPMCCost.ForeColor = System.Drawing.Color.Black
        Me.txtPMCCost.Location = New System.Drawing.Point(649, 92)
        Me.txtPMCCost.MaxLength = 20
        Me.txtPMCCost.Name = "txtPMCCost"
        Me.txtPMCCost.ReadOnly = True
        Me.txtPMCCost.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtPMCCost.Size = New System.Drawing.Size(72, 20)
        Me.txtPMCCost.TabIndex = 39
        Me.txtPMCCost.Tag = ""
        Me.txtPMCCost.Text = ""
        '
        'txtTotalCost
        '
        Me.txtTotalCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalCost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalCost.ForeColor = System.Drawing.Color.Black
        Me.txtTotalCost.Location = New System.Drawing.Point(865, 72)
        Me.txtTotalCost.MaxLength = 20
        Me.txtTotalCost.Name = "txtTotalCost"
        Me.txtTotalCost.ReadOnly = True
        Me.txtTotalCost.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtTotalCost.Size = New System.Drawing.Size(104, 20)
        Me.txtTotalCost.TabIndex = 43
        Me.txtTotalCost.Tag = ""
        Me.txtTotalCost.Text = ""
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(192, Byte), CType(128, Byte))
        Me.Label30.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label30.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.Color.Black
        Me.Label30.Location = New System.Drawing.Point(865, 48)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(104, 24)
        Me.Label30.TabIndex = 150
        Me.Label30.Text = "Total Cost"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtPCS
        '
        Me.txtPCS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPCS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPCS.ForeColor = System.Drawing.Color.Black
        Me.txtPCS.Location = New System.Drawing.Point(793, 72)
        Me.txtPCS.MaxLength = 20
        Me.txtPCS.Name = "txtPCS"
        Me.txtPCS.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtPCS.Size = New System.Drawing.Size(72, 20)
        Me.txtPCS.TabIndex = 42
        Me.txtPCS.Tag = ""
        Me.txtPCS.Text = ""
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(192, Byte), CType(128, Byte))
        Me.Label27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.Color.Black
        Me.Label27.Location = New System.Drawing.Point(793, 48)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(72, 24)
        Me.Label27.TabIndex = 148
        Me.Label27.Text = "PCS"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtSMC
        '
        Me.txtSMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSMC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMC.ForeColor = System.Drawing.Color.Black
        Me.txtSMC.Location = New System.Drawing.Point(721, 72)
        Me.txtSMC.MaxLength = 20
        Me.txtSMC.Name = "txtSMC"
        Me.txtSMC.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtSMC.Size = New System.Drawing.Size(72, 20)
        Me.txtSMC.TabIndex = 40
        Me.txtSMC.Tag = ""
        Me.txtSMC.Text = ""
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(192, Byte), CType(128, Byte))
        Me.Label28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.Color.Black
        Me.Label28.Location = New System.Drawing.Point(721, 48)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(72, 24)
        Me.Label28.TabIndex = 146
        Me.Label28.Text = "SMC"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtPMC
        '
        Me.txtPMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPMC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPMC.ForeColor = System.Drawing.Color.Black
        Me.txtPMC.Location = New System.Drawing.Point(649, 72)
        Me.txtPMC.MaxLength = 20
        Me.txtPMC.Name = "txtPMC"
        Me.txtPMC.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtPMC.Size = New System.Drawing.Size(72, 20)
        Me.txtPMC.TabIndex = 38
        Me.txtPMC.Tag = ""
        Me.txtPMC.Text = ""
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(192, Byte), CType(128, Byte))
        Me.Label29.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.Color.Black
        Me.Label29.Location = New System.Drawing.Point(649, 48)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(72, 24)
        Me.Label29.TabIndex = 144
        Me.Label29.Text = "PMC"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtStoneWeight
        '
        Me.txtStoneWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStoneWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStoneWeight.ForeColor = System.Drawing.Color.Black
        Me.txtStoneWeight.Location = New System.Drawing.Point(433, 92)
        Me.txtStoneWeight.MaxLength = 20
        Me.txtStoneWeight.Name = "txtStoneWeight"
        Me.txtStoneWeight.Size = New System.Drawing.Size(72, 20)
        Me.txtStoneWeight.TabIndex = 35
        Me.txtStoneWeight.Tag = ""
        Me.txtStoneWeight.Text = ""
        '
        'txtInventoryDiamondDescription
        '
        Me.txtInventoryDiamondDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInventoryDiamondDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInventoryDiamondDescription.ForeColor = System.Drawing.Color.Black
        Me.txtInventoryDiamondDescription.Location = New System.Drawing.Point(121, 92)
        Me.txtInventoryDiamondDescription.MaxLength = 50
        Me.txtInventoryDiamondDescription.Name = "txtInventoryDiamondDescription"
        Me.txtInventoryDiamondDescription.Size = New System.Drawing.Size(312, 20)
        Me.txtInventoryDiamondDescription.TabIndex = 111
        Me.txtInventoryDiamondDescription.Tag = ""
        Me.txtInventoryDiamondDescription.Text = ""
        '
        'txtCostPerGramI
        '
        Me.txtCostPerGramI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCostPerGramI.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostPerGramI.ForeColor = System.Drawing.Color.Black
        Me.txtCostPerGramI.Location = New System.Drawing.Point(577, 72)
        Me.txtCostPerGramI.MaxLength = 20
        Me.txtCostPerGramI.Name = "txtCostPerGramI"
        Me.txtCostPerGramI.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtCostPerGramI.Size = New System.Drawing.Size(72, 20)
        Me.txtCostPerGramI.TabIndex = 37
        Me.txtCostPerGramI.Tag = ""
        Me.txtCostPerGramI.Text = ""
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(192, Byte), CType(128, Byte))
        Me.Label24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Black
        Me.Label24.Location = New System.Drawing.Point(577, 48)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(72, 24)
        Me.Label24.TabIndex = 140
        Me.Label24.Text = "Cost Per Gram"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtGoldLoss
        '
        Me.txtGoldLoss.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldLoss.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoldLoss.ForeColor = System.Drawing.Color.Black
        Me.txtGoldLoss.Location = New System.Drawing.Point(505, 72)
        Me.txtGoldLoss.MaxLength = 20
        Me.txtGoldLoss.Name = "txtGoldLoss"
        Me.txtGoldLoss.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtGoldLoss.Size = New System.Drawing.Size(72, 20)
        Me.txtGoldLoss.TabIndex = 36
        Me.txtGoldLoss.Tag = ""
        Me.txtGoldLoss.Text = ""
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(192, Byte), CType(128, Byte))
        Me.Label25.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.Black
        Me.Label25.Location = New System.Drawing.Point(505, 48)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(72, 24)
        Me.Label25.TabIndex = 138
        Me.Label25.Text = "Gold Loss"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtWeight
        '
        Me.txtWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeight.ForeColor = System.Drawing.Color.Black
        Me.txtWeight.Location = New System.Drawing.Point(433, 72)
        Me.txtWeight.MaxLength = 20
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtWeight.Size = New System.Drawing.Size(72, 20)
        Me.txtWeight.TabIndex = 34
        Me.txtWeight.Tag = ""
        Me.txtWeight.Text = ""
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(192, Byte), CType(128, Byte))
        Me.Label26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.Black
        Me.Label26.Location = New System.Drawing.Point(433, 48)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(72, 24)
        Me.Label26.TabIndex = 136
        Me.Label26.Text = "Weight"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtInventoryDescription
        '
        Me.txtInventoryDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInventoryDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInventoryDescription.ForeColor = System.Drawing.Color.Black
        Me.txtInventoryDescription.Location = New System.Drawing.Point(121, 72)
        Me.txtInventoryDescription.MaxLength = 50
        Me.txtInventoryDescription.Name = "txtInventoryDescription"
        Me.txtInventoryDescription.Size = New System.Drawing.Size(239, 20)
        Me.txtInventoryDescription.TabIndex = 32
        Me.txtInventoryDescription.Tag = ""
        Me.txtInventoryDescription.Text = ""
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(192, Byte), CType(128, Byte))
        Me.Label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.Black
        Me.Label23.Location = New System.Drawing.Point(121, 48)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(239, 24)
        Me.Label23.TabIndex = 134
        Me.Label23.Text = "Inventory Description"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtInventoryCode
        '
        Me.txtInventoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInventoryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInventoryCode.ForeColor = System.Drawing.Color.Black
        Me.txtInventoryCode.Location = New System.Drawing.Point(1, 72)
        Me.txtInventoryCode.MaxLength = 20
        Me.txtInventoryCode.Name = "txtInventoryCode"
        Me.txtInventoryCode.Size = New System.Drawing.Size(120, 20)
        Me.txtInventoryCode.TabIndex = 31
        Me.txtInventoryCode.Tag = ""
        Me.txtInventoryCode.Text = ""
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(192, Byte), CType(128, Byte))
        Me.Label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.Black
        Me.Label22.Location = New System.Drawing.Point(1, 48)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(120, 24)
        Me.Label22.TabIndex = 132
        Me.Label22.Text = "Inventory Code"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.chkAutoSMC)
        Me.Panel3.Controls.Add(Me.Label21)
        Me.Panel3.Controls.Add(Me.optSMCPCS)
        Me.Panel3.Controls.Add(Me.optSMCGram)
        Me.Panel3.Location = New System.Drawing.Point(809, 4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(160, 44)
        Me.Panel3.TabIndex = 131
        '
        'chkAutoSMC
        '
        Me.chkAutoSMC.Checked = True
        Me.chkAutoSMC.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAutoSMC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAutoSMC.ForeColor = System.Drawing.Color.Black
        Me.chkAutoSMC.Location = New System.Drawing.Point(80, 0)
        Me.chkAutoSMC.Name = "chkAutoSMC"
        Me.chkAutoSMC.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkAutoSMC.Size = New System.Drawing.Size(64, 16)
        Me.chkAutoSMC.TabIndex = 76
        Me.chkAutoSMC.Text = "AUTO"
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Black
        Me.Label21.Location = New System.Drawing.Point(0, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(88, 15)
        Me.Label21.TabIndex = 74
        Me.Label21.Text = "Sales MC"
        '
        'optSMCPCS
        '
        Me.optSMCPCS.Checked = True
        Me.optSMCPCS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSMCPCS.Location = New System.Drawing.Point(88, 16)
        Me.optSMCPCS.Name = "optSMCPCS"
        Me.optSMCPCS.Size = New System.Drawing.Size(69, 24)
        Me.optSMCPCS.TabIndex = 30
        Me.optSMCPCS.TabStop = True
        Me.optSMCPCS.Text = "Per PCS"
        '
        'optSMCGram
        '
        Me.optSMCGram.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSMCGram.Location = New System.Drawing.Point(8, 16)
        Me.optSMCGram.Name = "optSMCGram"
        Me.optSMCGram.Size = New System.Drawing.Size(80, 24)
        Me.optSMCGram.TabIndex = 29
        Me.optSMCGram.Text = "Per Gram"
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Label20)
        Me.Panel4.Controls.Add(Me.optPMCPCS)
        Me.Panel4.Controls.Add(Me.optPMCGram)
        Me.Panel4.Location = New System.Drawing.Point(649, 4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(160, 44)
        Me.Panel4.TabIndex = 130
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.Location = New System.Drawing.Point(0, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(88, 15)
        Me.Label20.TabIndex = 74
        Me.Label20.Text = "Purchase MC"
        '
        'optPMCPCS
        '
        Me.optPMCPCS.Checked = True
        Me.optPMCPCS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPMCPCS.Location = New System.Drawing.Point(88, 16)
        Me.optPMCPCS.Name = "optPMCPCS"
        Me.optPMCPCS.Size = New System.Drawing.Size(69, 24)
        Me.optPMCPCS.TabIndex = 28
        Me.optPMCPCS.TabStop = True
        Me.optPMCPCS.Text = "Per PCS"
        '
        'optPMCGram
        '
        Me.optPMCGram.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPMCGram.Location = New System.Drawing.Point(0, 16)
        Me.optPMCGram.Name = "optPMCGram"
        Me.optPMCGram.Size = New System.Drawing.Size(80, 24)
        Me.optPMCGram.TabIndex = 27
        Me.optPMCGram.Text = "Per Gram"
        '
        'txtInventoryPCS
        '
        Me.txtInventoryPCS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInventoryPCS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInventoryPCS.ForeColor = System.Drawing.Color.Black
        Me.txtInventoryPCS.Location = New System.Drawing.Point(577, 28)
        Me.txtInventoryPCS.MaxLength = 20
        Me.txtInventoryPCS.Name = "txtInventoryPCS"
        Me.txtInventoryPCS.ReadOnly = True
        Me.txtInventoryPCS.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtInventoryPCS.Size = New System.Drawing.Size(72, 20)
        Me.txtInventoryPCS.TabIndex = 26
        Me.txtInventoryPCS.Tag = ""
        Me.txtInventoryPCS.Text = ""
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(192, Byte), CType(128, Byte))
        Me.Label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(577, 4)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(72, 24)
        Me.Label18.TabIndex = 128
        Me.Label18.Text = "Inv. PCS"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtInventoryWeight
        '
        Me.txtInventoryWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInventoryWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInventoryWeight.ForeColor = System.Drawing.Color.Black
        Me.txtInventoryWeight.Location = New System.Drawing.Point(505, 28)
        Me.txtInventoryWeight.MaxLength = 20
        Me.txtInventoryWeight.Name = "txtInventoryWeight"
        Me.txtInventoryWeight.ReadOnly = True
        Me.txtInventoryWeight.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtInventoryWeight.Size = New System.Drawing.Size(72, 20)
        Me.txtInventoryWeight.TabIndex = 25
        Me.txtInventoryWeight.Tag = ""
        Me.txtInventoryWeight.Text = ""
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(192, Byte), CType(128, Byte))
        Me.Label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(505, 4)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(72, 24)
        Me.Label19.TabIndex = 126
        Me.Label19.Text = "Inv. Weight"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtBillPCS
        '
        Me.txtBillPCS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillPCS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillPCS.ForeColor = System.Drawing.Color.Black
        Me.txtBillPCS.Location = New System.Drawing.Point(433, 28)
        Me.txtBillPCS.MaxLength = 20
        Me.txtBillPCS.Name = "txtBillPCS"
        Me.txtBillPCS.ReadOnly = True
        Me.txtBillPCS.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtBillPCS.Size = New System.Drawing.Size(72, 20)
        Me.txtBillPCS.TabIndex = 24
        Me.txtBillPCS.Tag = ""
        Me.txtBillPCS.Text = ""
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(192, Byte), CType(128, Byte))
        Me.Label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(433, 4)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(72, 24)
        Me.Label17.TabIndex = 124
        Me.Label17.Text = "Bill PCS"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtBillWeight
        '
        Me.txtBillWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillWeight.ForeColor = System.Drawing.Color.Black
        Me.txtBillWeight.Location = New System.Drawing.Point(361, 28)
        Me.txtBillWeight.MaxLength = 20
        Me.txtBillWeight.Name = "txtBillWeight"
        Me.txtBillWeight.ReadOnly = True
        Me.txtBillWeight.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtBillWeight.Size = New System.Drawing.Size(72, 20)
        Me.txtBillWeight.TabIndex = 23
        Me.txtBillWeight.Tag = ""
        Me.txtBillWeight.Text = ""
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(192, Byte), CType(128, Byte))
        Me.Label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(361, 4)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(72, 24)
        Me.Label16.TabIndex = 122
        Me.Label16.Text = "Bill Weight"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnTrayIdSearch
        '
        Me.btnTrayIdSearch.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnTrayIdSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTrayIdSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTrayIdSearch.Location = New System.Drawing.Point(337, 28)
        Me.btnTrayIdSearch.Name = "btnTrayIdSearch"
        Me.btnTrayIdSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnTrayIdSearch.TabIndex = 22
        Me.btnTrayIdSearch.Text = "..."
        '
        'txtTrayId
        '
        Me.txtTrayId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTrayId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTrayId.ForeColor = System.Drawing.Color.Black
        Me.txtTrayId.Location = New System.Drawing.Point(241, 28)
        Me.txtTrayId.MaxLength = 20
        Me.txtTrayId.Name = "txtTrayId"
        Me.txtTrayId.Size = New System.Drawing.Size(96, 20)
        Me.txtTrayId.TabIndex = 21
        Me.txtTrayId.Tag = ""
        Me.txtTrayId.Text = ""
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(192, Byte), CType(128, Byte))
        Me.Label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(241, 4)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(120, 24)
        Me.Label15.TabIndex = 119
        Me.Label15.Text = "Tray Id"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCategoryCode
        '
        Me.txtCategoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCategoryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCategoryCode.ForeColor = System.Drawing.Color.Black
        Me.txtCategoryCode.Location = New System.Drawing.Point(121, 28)
        Me.txtCategoryCode.MaxLength = 20
        Me.txtCategoryCode.Name = "txtCategoryCode"
        Me.txtCategoryCode.ReadOnly = True
        Me.txtCategoryCode.Size = New System.Drawing.Size(120, 20)
        Me.txtCategoryCode.TabIndex = 20
        Me.txtCategoryCode.Tag = ""
        Me.txtCategoryCode.Text = ""
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(192, Byte), CType(128, Byte))
        Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(121, 4)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(120, 24)
        Me.Label14.TabIndex = 117
        Me.Label14.Text = "Category Code"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnCatalogueCodeSearch
        '
        Me.btnCatalogueCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnCatalogueCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCatalogueCodeSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCatalogueCodeSearch.Location = New System.Drawing.Point(97, 28)
        Me.btnCatalogueCodeSearch.Name = "btnCatalogueCodeSearch"
        Me.btnCatalogueCodeSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnCatalogueCodeSearch.TabIndex = 19
        Me.btnCatalogueCodeSearch.Text = "..."
        '
        'txtCatalogueCode
        '
        Me.txtCatalogueCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCatalogueCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCatalogueCode.ForeColor = System.Drawing.Color.Black
        Me.txtCatalogueCode.Location = New System.Drawing.Point(1, 28)
        Me.txtCatalogueCode.MaxLength = 20
        Me.txtCatalogueCode.Name = "txtCatalogueCode"
        Me.txtCatalogueCode.Size = New System.Drawing.Size(96, 20)
        Me.txtCatalogueCode.TabIndex = 18
        Me.txtCatalogueCode.Tag = ""
        Me.txtCatalogueCode.Text = ""
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(192, Byte), CType(128, Byte))
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(1, 4)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(120, 24)
        Me.Label13.TabIndex = 114
        Me.Label13.Text = "Catalogue Code"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnBillClose
        '
        Me.btnBillClose.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnBillClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBillClose.Image = CType(resources.GetObject("btnBillClose.Image"), System.Drawing.Image)
        Me.btnBillClose.Location = New System.Drawing.Point(790, 592)
        Me.btnBillClose.Name = "btnBillClose"
        Me.btnBillClose.Size = New System.Drawing.Size(92, 23)
        Me.btnBillClose.TabIndex = 49
        Me.btnBillClose.Text = "Bill Close (F7)"
        '
        'btnBillClear
        '
        Me.btnBillClear.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnBillClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBillClear.Image = CType(resources.GetObject("btnBillClear.Image"), System.Drawing.Image)
        Me.btnBillClear.Location = New System.Drawing.Point(696, 592)
        Me.btnBillClear.Name = "btnBillClear"
        Me.btnBillClear.Size = New System.Drawing.Size(92, 23)
        Me.btnBillClear.TabIndex = 50
        Me.btnBillClear.Text = "Clear (F6)"
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
        'frmPurchaseML
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(986, 624)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.btnBillClear)
        Me.Controls.Add(Me.btnBillClose)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.grdPurchase)
        Me.Controls.Add(Me.grdJobSheet)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtCurrencyRate)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmPurchaseML"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "JobSheet Purchase "
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.grdJobSheet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdPurchase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmPurchaseML_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        subGridSetting(grdJobSheet)
        subGridSetting(grdPurchase)

        subFillPriceCodeArr()
        subLockAllFlds(False)
        subClear()
        subClearDetail(False)
        subClearCatalogue()
        If UCase(sCompanyShortCode) = "ES" Then
            chkAutoSMC.Checked = True
        Else
            chkAutoSMC.Checked = False
        End If
        lblAutoManual.Text = "MANUAL (F10)"
        gsBarCodeFilePath = Application.StartupPath & "\" & gsBarCodeFile & ".txt"
        txtJobsheetNo.Focus()
    End Sub

    Private Sub subClearCatalogue()
        Try
            txtCatalogueCode.Clear()
            txtCategoryCode.Clear()
            txtTrayId.Clear()
            txtBillWeight.Clear()
            txtBillPCS.Clear()
            txtInventoryWeight.Clear()
            txtInventoryPCS.Clear()
            txtInventoryCode.Clear()
            txtInventoryDescription.Clear()
            txtCostPerGramI.Clear()
            txtPMC.Clear()
            txtSMC.Clear()
            If sCompanyShortCode <> "ML" Then
                optPMCGram.Checked = True
                optSMCGram.Checked = True
            Else
                optPMCPCS.Checked = True
                optSMCPCS.Checked = True
            End If
            txtPMCCost.Text = ""
            txtSMCCost.Text = ""
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try

    End Sub

    Private Sub subClearDetail(ByVal bFocus As Boolean)
        Try
            txtInventoryCode.Tag = ""
            bEdited = False
            txtInventoryDescription.Clear()
            txtInventoryDiamondDescription.Clear()
            txtWeight.Clear()
            txtStoneWeight.Clear()
            txtGoldLoss.Clear()

            txtPCS.Text = "1"
            mspurchasetype = ""
            txtTotalCost.Clear()
            sClassCode = ""
            If (bFocus And txtCatalogueCode.Enabled) Then txtCatalogueCode.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub subClear()
        Try
            txtJobsheetNo.Clear()
            txtInvoiceNo.Clear()
            txtIDD.Clear()
            txtIMM.Clear()
            txtIYYYY.Clear()
            txtSupplierCode.Clear()
            txtSupplierName.Clear()
            txtCurrencyCode.Clear()
            txtCurrencyName.Clear()
            txtCurrencyRate.Clear()
            optCash.Checked = False
            optGold.Checked = False
            chkGST.Checked = False
            txtTEAL.Clear()
            txtCostPerGram.Clear()
            txtRemarks.Clear()

            'txtGst.text = ""
            'txtManualCost.text = ""
            'txtManualMC.text = ""
            'txtManualWeight.text = ""
            'txtManualGoldLoss.text = ""

            grdPurchase.MaxRows = 0
            grdJobSheet.MaxRows = 0

            sTranType = ""
            sBillClose = ""

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Function subLockAllFlds(ByVal bLock As Boolean)
        Try
            txtJobsheetNo.ReadOnly = bLock
            txtInvoiceNo.ReadOnly = bLock
            txtSupplierCode.ReadOnly = bLock
            txtCatalogueCode.Enabled = Not bLock
            txtTrayId.Enabled = Not bLock
            txtInventoryDescription.ReadOnly = bLock
            txtInventoryDiamondDescription.ReadOnly = bLock
            txtWeight.ReadOnly = bLock
            txtStoneWeight.ReadOnly = bLock
            txtGoldLoss.ReadOnly = bLock
            txtPMC.ReadOnly = bLock
            txtSMC.ReadOnly = bLock
            txtPCS.ReadOnly = bLock

            optPMCGram.Enabled = Not bLock
            optPMCPCS.Enabled = Not bLock
            optSMCGram.Enabled = Not bLock
            optSMCPCS.Enabled = Not bLock
            btnAdd.Enabled = Not bLock
            btnDelete.Enabled = Not bLock
            btnClear.Enabled = Not bLock
            btnBillClose.Enabled = Not bLock
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Function

    Private Sub subFillPriceCodeArr()
        Dim sQry As String
        Dim Ds As DataSet
        Dim i As Integer
        Try
            sQry = "Select PriceCode, PriceValue from tblPriceCode order by PriceValue"
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Price", "Price")
            If Ds.Tables("price").Rows.Count > 0 Then
                For i = 0 To Ds.Tables("Price").Rows.Count - 1
                    masPriceCode(Val(Ds.Tables("price").Rows(i)("PriceValue") & "")) = Trim$(Ds.Tables("price").Rows(i)("PriceCode") & "")
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Private Sub btnJobSheetNoSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJobSheetNoSearch.Click
        Try
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblJobSheetHeader", "L", 4, "JobSheetNo", "", "JobSheetNo", "JobSheetNo", 80, "SupplierCode", "SupplierCode", 180, "PurchaseBillNo", "PurchaseBillNo", 60, "TotalWeight24k", "TotalWeight24k", 80)
            frmHelpClass.ShowDialog()
            txtJobsheetNo.Text = frmHelpClass.Var1.Trim
            'If txtJobsheetNo.Enabled = True Then txtJobsheetNo.Focus()
            txtJobsheetNo_LostFocus(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtJobsheetNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtJobsheetNo.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                Dim frmHelpClass As New frmHelpSearch(0, 0, "tblJobSheetHeader", "L", 4, "JobSheetNo", "", "JobSheetNo", "JobSheetNo", 80, "SupplierCode", "SupplierCode", 180, "PurchaseBillNo", "PurchaseBillNo", 60, "TotalWeight24k", "TotalWeight24k", 80)
                frmHelpClass.ShowDialog()
                txtJobsheetNo.Text = frmHelpClass.Var1.Trim
                txtJobsheetNo_LostFocus(sender, e)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnSupplierCodeSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSupplierCodeSearch.Click
        Try
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSupplier", "L", 4, "SupplierName", "", "SupplierCode", "SupplierCode", 80, "SupplierName", "SupplierName", 180, "Address1", "Address1", 60, "Address2", "Address2", 80)
            frmHelpClass.ShowDialog()
            txtSupplierCode.Text = frmHelpClass.Var1.Trim
            txtSupplierName.Text = frmHelpClass.Var2.Trim
            'If txtSupplierCode.Enabled Then txtSupplierCode.Focus()
            txtSupplierCode_LostFocus(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtSupplierCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSupplierCode.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSupplier", "L", 4, "SupplierName", "", "SupplierCode", "SupplierCode", 80, "SupplierName", "SupplierName", 180, "Address1", "Address1", 60, "Address2", "Address2", 80)
                frmHelpClass.ShowDialog()
                txtSupplierCode.Text = frmHelpClass.Var1.Trim
                txtSupplierName.Text = frmHelpClass.Var2.Trim
                txtSupplierCode_LostFocus(sender, e)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtJobsheetNo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtJobsheetNo.LostFocus
        Try
            If txtJobsheetNo.Text.Trim <> "" Then
                subGetJobSheet(sender, e)
                txtCatalogueCode.Focus()
            Else
                subLockAllFlds(False)
                subClear()
                subClearDetail(False)
                subClearCatalogue()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Private Sub subGetJobSheet(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim i As Long
        Dim Ds As DataSet
        Dim objJobSheet As New clsJobSheet

        Try
            Ds = objJobSheet.GetJobSheet(txtJobsheetNo.Text.Trim)
            If Ds.Tables("JobSheet").Rows.Count > 0 Then
                txtJobsheetNo.Text = Trim(Ds.Tables("JobSheet").Rows(0)("JobSheetNo") & "")
                sJobSheetCode = txtJobsheetNo.Text.Trim
                txtInvoiceNo.Text = Trim(Ds.Tables("JobSheet").Rows(0)("PurchaseBillNo") & "")
                txtIYYYY.Text = Mid(Ds.Tables("JobSheet").Rows(0)("Pdate") & "", 1, 4)
                txtIMM.Text = Mid(Ds.Tables("JobSheet").Rows(0)("Pdate") & "", 5, 2)
                txtIDD.Text = Mid(Ds.Tables("JobSheet").Rows(0)("Pdate") & "", 7, 2)
                txtSupplierCode.Text = Trim(Ds.Tables("JobSheet").Rows(0)("SupplierCode") & "")
                txtSupplierCode_LostFocus(sender, e)
                txtCurrencyCode.Text = Trim(Ds.Tables("JobSheet").Rows(0)("CurrencyCode") & " ")
                txtCurrencyCode_LostFocus(sender, e)
                txtCurrencyRate.Text = Val(Ds.Tables("JobSheet").Rows(0)("CurrencyRate") & "")

                txtCostPerGram.Text = Val(Ds.Tables("JobSheet").Rows(0)("CostperGram") & "")
                chkGST.Checked = IIf(UCase(Trim(Ds.Tables("JobSheet").Rows(0)("TaelGST") & "")) = "Y", 1, 0)
                If chkGST.Checked = True Then
                    txtTEAL.Text = Format((((Val(txtCostPerGram.Text) / Val(GstTaelPercentage)) / 0.92) / 0.02646), "0")
                Else
                    txtTEAL.Text = Format((((Val(txtCostPerGram.Text) / 0.92)) / 0.02646), "0")
                End If
                txtRemarks.Text = Trim(Ds.Tables("JobSheet").Rows(0)("REMARKS") & "")

                sTranType = UCase(Trim(Ds.Tables("JobSheet").Rows(0)("TranType") & ""))
                sBillClose = UCase(Trim$(Ds.Tables("JobSheet").Rows(0)("BillClose") & ""))

                'txtManualCost.text = Val(Ds.Tables("JobSheet").Rows(0)("ManualLocalCOST") & "")
                'txtManualMC.text = Val(Ds.Tables("JobSheet").Rows(0)("ManualMc") & "")
                'txtManualWeight.text = Val(Ds.Tables("JobSheet").Rows(0)("ManualWeight") & "")
                'txtManualGoldLoss.text = Val(Ds.Tables("JobSheet").Rows(0)("ManualGoldLoss") & "")
                'txtGst.text = Val(Ds.Tables("JobSheet").Rows(0)("Gst") & "")

                If Trim(Ds.Tables("JobSheet").Rows(0)("PaymentType") & "") = "G" Then
                    optGold.Checked = True
                Else
                    optCash.Checked = True
                End If


                'lock everything
                If sBillClose = "Y" Then
                    MsgBox("This bill is closed! Cannot modify!", MsgBoxStyle.Critical)
                    subLockAllFlds(True)
                ElseIf sTranType = "DP" Then
                    MsgBox("This is a direct purchase bill. Cannot modify!", MsgBoxStyle.Critical)
                    subLockAllFlds(True)
                Else
                    subLockAllFlds(False)
                End If
                txtJobsheetNo.ReadOnly = True
                txtSupplierCode.ReadOnly = True
                txtInvoiceNo.ReadOnly = True
                If txtCatalogueCode.Enabled Then txtCatalogueCode.Focus()
            Else
                MsgBox("Job sheet no. is invalid!", MsgBoxStyle.Critical)
                btnBillClear_Click(sender, e)
                Exit Sub
            End If

            subGetJobSheetDetails()
            subGetPurchaseDetails()
            txtCatalogueCode.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subGetPurchaseDetails()
        Dim i As Long
        Dim Ds As DataSet
        Dim objInventory As New clsInventory
        Try

            Ds = objInventory.GetInventorybyJobSheet(txtJobsheetNo.Text.Trim)

            grdPurchase.MaxRows = 0
            For i = 0 To Ds.Tables("tblInventory").Rows.Count - 1
                grdPurchase.MaxRows = grdPurchase.MaxRows + 1
                grdPurchase.Row = grdPurchase.MaxRows
                Call SetText(grdPurchase, grdPurchase.Row, 1, Trim(Ds.Tables("tblInventory").Rows(i)("Inventorycode") & ""))
                Call SetText(grdPurchase, grdPurchase.Row, 2, Trim(Ds.Tables("tblInventory").Rows(i)("ClassCode") & ""))
                Call SetText(grdPurchase, grdPurchase.Row, 3, Trim(Ds.Tables("tblInventory").Rows(i)("Description") & ""))
                Call SetText(grdPurchase, grdPurchase.Row, 4, Trim(Ds.Tables("tblInventory").Rows(i)("Size") & ""))
                Call SetText(grdPurchase, grdPurchase.Row, 5, Format(Val(Ds.Tables("tblInventory").Rows(i)("Weight") & ""), "#0.00"))
                Call SetText(grdPurchase, grdPurchase.Row, 6, Format(Val(Ds.Tables("tblInventory").Rows(i)("GoldLoss") & ""), "#0.00"))
                Call SetText(grdPurchase, grdPurchase.Row, 7, Format(Val(Ds.Tables("tblInventory").Rows(i)("CostperGram") & ""), "#0.00"))
                Call SetText(grdPurchase, grdPurchase.Row, 8, Format(Val(Ds.Tables("tblInventory").Rows(i)("PMC")), "#0.00"))
                Call SetText(grdPurchase, grdPurchase.Row, 9, Format(Val(Ds.Tables("tblInventory").Rows(i)("sMc")), "#0.00"))
                Call SetText(grdPurchase, grdPurchase.Row, 10, Format(Val(Ds.Tables("tblInventory").Rows(i)("Pcs") & ""), "#0"))
                Call SetText(grdPurchase, grdPurchase.Row, 11, Format(Val(Ds.Tables("tblInventory").Rows(i)("TotalCost") & ""), "#0.00"))
                Call SetText(grdPurchase, grdPurchase.Row, 12, Trim$(Ds.Tables("tblInventory").Rows(i)("CatalogueCode") & ""))
                Call SetText(grdPurchase, grdPurchase.Row, 13, Trim$(Ds.Tables("tblInventory").Rows(i)("CategoryCode") & ""))
                Call SetText(grdPurchase, grdPurchase.Row, 14, Trim$(Ds.Tables("tblInventory").Rows(i)("DiamondDescription") & ""))
                Call SetText(grdPurchase, grdPurchase.Row, 15, Trim$(Ds.Tables("tblInventory").Rows(i)("StoneWeight") & ""))
                Call SetText(grdPurchase, grdPurchase.Row, 16, Trim$(Ds.Tables("tblInventory").Rows(i)("PMCType") & ""))
                Call SetText(grdPurchase, grdPurchase.Row, 17, Trim$(Ds.Tables("tblInventory").Rows(i)("SMCType") & ""))
                Call SetText(grdPurchase, grdPurchase.Row, 18, Trim$(Ds.Tables("tblInventory").Rows(i)("TrayID") & ""))
            Next i

            grdPurchase.Row = grdPurchase.MaxRows
            grdPurchase.Col = 1
            grdPurchase.Action = 0
            subTotal()
            Ds = Nothing
            objInventory = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        Finally
        End Try
    End Sub
    Private Sub subTotal()
        Try
            If Not bEdited Then
                grdPurchase.MaxRows = grdPurchase.MaxRows + 1
            End If

            Call SetText(grdPurchase, grdPurchase.MaxRows, 1, "Total:")

            grdPurchase.ForeColor = Color.Red
            grdPurchase.FontBold = True

            grdPurchase.Col = 4
            grdPurchase.Formula = "sum(d1:d" & grdPurchase.MaxRows - 1 & ")"


            grdPurchase.ForeColor = Color.Red
            grdPurchase.FontBold = True

            grdPurchase.Col = 5
            grdPurchase.Formula = "sum(e1:e" & grdPurchase.MaxRows - 1 & ")"

            grdPurchase.ForeColor = Color.Red
            grdPurchase.FontBold = True

            grdPurchase.Col = 7
            grdPurchase.Formula = "sum(g1:g" & grdPurchase.MaxRows - 1 & ")"

            grdPurchase.ForeColor = Color.Red
            grdPurchase.FontBold = True

            grdPurchase.Col = 8
            grdPurchase.Formula = "sum(h1:h" & grdPurchase.MaxRows - 1 & ")"

            grdPurchase.ForeColor = Color.Red
            grdPurchase.FontBold = True

            grdPurchase.Col = 9
            grdPurchase.Formula = "sum(i1:i" & grdPurchase.MaxRows - 1 & ")"
            grdPurchase.ForeColor = Color.Red
            grdPurchase.FontBold = True

            grdPurchase.Col = 10
            grdPurchase.Formula = "sum(j1:j" & grdPurchase.MaxRows - 1 & ")"

            grdPurchase.ForeColor = Color.Red
            grdPurchase.FontBold = True

            subGetJobSheetDetails()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        Finally

        End Try
    End Sub

    Private Sub subGetJobSheetDetails()
        Dim objJobSheet As New clsJobSheet
        Dim objInventory As New clsInventory
        Dim DsJS As DataSet
        Dim DsIn As DataSet
        Dim i As Long
        Try
            DsJS = objJobSheet.GetJobSheetDetailWeight(txtJobsheetNo.Text.Trim)
            grdJobSheet.MaxRows = 0
            For i = 0 To DsJS.Tables("JobSheet").Rows.Count - 1
                grdJobSheet.MaxRows = grdJobSheet.MaxRows + 1
                grdJobSheet.Row = grdJobSheet.MaxRows
                Call SetText(grdJobSheet, grdJobSheet.Row, 1, Trim(DsJS.Tables("JobSheet").Rows(i)("CategoryCode") & ""))
                Call SetText(grdJobSheet, grdJobSheet.Row, 2, Format(Val(DsJS.Tables("JobSheet").Rows(i)("BillWT") & ""), "#0.00"))


                DsIn = objInventory.GetInventoryWeight(txtJobsheetNo.Text.Trim, Trim(DsJS.Tables("JobSheet").Rows(i)("CategoryCode") & ""))
                If DsIn.Tables("Inventory").Rows.Count > 0 Then
                    Call SetText(grdJobSheet, grdJobSheet.Row, 3, Format(Val(DsIn.Tables("Inventory").Rows(0)("BillWT") & ""), "#0.00"))
                    Call SetText(grdJobSheet, grdJobSheet.Row, 4, Format(Val((DsJS.Tables("JobSheet").Rows(i)("BillWT") & "") - Val(DsIn.Tables("Inventory").Rows(0)("BillWT") & "")), "#0.00"))
                Else
                    Call SetText(grdJobSheet, grdJobSheet.Row, 3, Format(Val("0"), "#0.00"))
                    Call SetText(grdJobSheet, grdJobSheet.Row, 4, Format(Val((DsJS.Tables("JobSheet").Rows(i)("BillWT") & "") - Val("0")), "#0.00"))
                End If
                DsIn = Nothing
            Next i

            grdJobSheet.Row = grdJobSheet.MaxRows
            grdPurchase.Col = 1
            grdPurchase.Action = 0
            DsJS = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try

    End Sub

    Private Sub btnBillClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBillClear.Click
        Try
            subLockAllFlds(False)
            subClear()
            subClearDetail(False)
            subClearCatalogue()
            txtJobsheetNo.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub txtSupplierCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSupplierCode.LostFocus
        Try
            If txtSupplierCode.Text.Trim <> "" Then If txtJobsheetNo.Text.Trim <> "" Then txtCatalogueCode.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            If sBillClose = "Y" Then Exit Sub
            If sTranType = "DP" Then Exit Sub

            If fncValidateInventory() Then
                Call FillTotalCost()
                'check the wt and the pcs.
                If fncCheckWtPCS() Then
                    If fncPurchaseSave() Then
                        subAddGrid(sender, e)
                        subGetSummary()
                        If txtWeight.Enabled Then txtWeight.Focus()
                    End If
                End If
                If txtWeight.Enabled Then txtWeight.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Private Sub subGetSummary()
        Dim Ds As DataSet
        Dim objJobsheet As New clsJobSheet
        Dim objInventory As New clsInventory
        Try
            Ds = objJobsheet.GetJobSheetWeight(txtJobsheetNo.Text.Trim, txtCategoryCode.Text.Trim)
            If Ds.Tables("JobSheet").Rows.Count > 0 Then
                txtBillWeight.Text = Format(Val(Ds.Tables("JobSheet").Rows(0)("BillWT") & ""), "#0.00")
                txtBillPCS.Text = Format(Val(Ds.Tables("JobSheet").Rows(0)("BillPcs") & ""), "#0")
            Else
                txtBillWeight.Text = "0.00"
                txtBillPCS.Text = "0"
            End If
            Ds = objInventory.GetInventoryWeight(txtJobsheetNo.Text.Trim, txtCategoryCode.Text.Trim)
            If Ds.Tables("Inventory").Rows.Count > 0 Then
                txtInventoryWeight.Text = Format(Val(Ds.Tables("Inventory").Rows(0)("BillWT") & ""), "#0.00")
                txtInventoryPCS.Text = Format(Val(Ds.Tables("Inventory").Rows(0)("BillPcs") & ""), "#0")
            Else
                txtInventoryWeight.Text = "0.00"
                txtInventoryPCS.Text = "0"
            End If
            Ds = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub subAddGrid(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As Long
        Try
            If Not bEdited Then
                If grdPurchase.MaxRows = 0 Then
                    grdPurchase.MaxRows = grdPurchase.MaxRows + 1
                Else
                    grdPurchase.Row = grdPurchase.MaxRows
                    grdPurchase.Action = 5
                    grdPurchase.MaxRows = grdPurchase.MaxRows - 1
                    grdPurchase.MaxRows = grdPurchase.MaxRows + 1
                End If

                grdPurchase.Row = grdPurchase.MaxRows
            Else
                grdPurchase.Row = grdPurchase.ActiveRow
            End If

            Call SetText(grdPurchase, grdPurchase.Row, 1, txtInventoryCode.Text.Trim)
            Call SetText(grdPurchase, grdPurchase.Row, 2, Trim(sClassCode))
            Call SetText(grdPurchase, grdPurchase.Row, 3, txtInventoryDescription.Text.Trim)
            Call SetText(grdPurchase, grdPurchase.Row, 4, txtSize.Text.Trim)
            Call SetText(grdPurchase, grdPurchase.Row, 5, Format(Val(txtWeight.Text), "#0.00"))
            Call SetText(grdPurchase, grdPurchase.Row, 6, Format(Val(txtGoldLoss.Text), "#0.00"))
            Call SetText(grdPurchase, grdPurchase.Row, 7, Format(Val(txtCostPerGramI.Text), "#0.00"))
            Call SetText(grdPurchase, grdPurchase.Row, 8, Format(Val(txtPMCCost.Text), "#0.00"))
            Call SetText(grdPurchase, grdPurchase.Row, 9, Format(Val(txtSMCCost.Text), "#0.00"))
            Call SetText(grdPurchase, grdPurchase.Row, 10, Format(Val(txtPCS.Text), "#0"))
            Call SetText(grdPurchase, grdPurchase.Row, 11, Format(Val(txtTotalCost.Text), "#0.00"))
            Call SetText(grdPurchase, grdPurchase.Row, 12, txtCatalogueCode.Text.Trim)
            'Call SetText(grdPurchase, grdPurchase.Row, 12, txtCategoryCode.Text.Trim)
            Call SetText(grdPurchase, grdPurchase.Row, 13, txtCategoryCode.Text.Trim)
            Call SetText(grdPurchase, grdPurchase.Row, 14, txtInventoryDiamondDescription.Text.Trim)
            Call SetText(grdPurchase, grdPurchase.Row, 15, Format(Val(txtStoneWeight.Text), "#0.00"))
            Call SetText(grdPurchase, grdPurchase.Row, 16, IIf(optPMCPCS.Checked, "P", "W"))
            Call SetText(grdPurchase, grdPurchase.Row, 17, IIf(optSMCPCS.Checked, "P", "W"))
            Call SetText(grdPurchase, grdPurchase.Row, 18, txtTrayId.Text.Trim)

            grdPurchase.Row = grdPurchase.MaxRows
            grdPurchase.Col = 1
            grdPurchase.Action = 0
            subTotal()
            txtInventoryCode.Tag = txtInventoryCode

            Dim objfrm As New frmBarcode
            objfrm.txtInventoryCode.Text = txtInventoryCode.Text.Trim
            objfrm.txtInventoryCode_LostFocus(sender, e)
            objfrm.btnPrint_Click(sender, e)
            '''If mspurchasetype = "W" Then

            '''ElseIf mspurchasetype = "P" Then
            '''    Dim objfrm As New frmJewelBarcode
            '''    objfrm.txtInventoryCode.Text = txtInventoryCode.Text.Trim
            '''    objfrm.txtInventoryCode_LostFocus(sender, e)
            '''    objfrm.btnPrint_Click(sender, e)
            '''End If
            subClearDetail(True)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Function fncPurchaseSave() As Boolean

        Dim SBll As Object
        Dim sinventorycode As String
        Try
            gsConBatchPosting.ConnectionString = gsCon
            gsConBatchPosting.Open()
            gsTrans = gsConBatchPosting.BeginTransaction

            If fncInventory(sinventorycode) Then
                gsTrans.Commit()
                fncPurchaseSave = True
            Else
                MsgBox("Transaction not saved", MsgBoxStyle.Critical)
                gsTrans.Rollback()
                fncPurchaseSave = False
            End If
            gsConBatchPosting.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            gsConBatchPosting.Close()
        End Try
    End Function
    Private Function fncInventory(ByRef sinventorycode As String) As Boolean

        Dim sPDate As String
        Dim Ds As DataSet
        Dim sError As String
        Dim bInventory As Boolean
        Dim bPurchase As Boolean
        Dim bPurchaseBill As Boolean
        Dim i As Long
        Dim sError1 As String
        Dim sError2 As String
        Dim sPMCType As String
        Dim sSMCType As String
        Dim sPayType As String
        Dim objInventory As New clsInventory
        Dim objPurchase As New clsPurchase
        Try
            bInventory = False
            sPMCType = IIf(optPMCPCS.Checked, "P", "W")
            sSMCType = IIf(optSMCPCS.Checked, "P", "W")

            If optGold.Checked Then
                sPayType = "G"
            Else
                sPayType = "C"
            End If

            If Val(txtPCS.Text) = 0 Then
                txtPCS.Text = "1"
            End If

            sPDate = txtIDD.Text.Trim + "/" + txtIMM.Text.Trim + "/" + txtIYYYY.Text.Trim
            If IsDate(sPDate) Then
                If objInventory.InventorySaveML(txtInventoryCode.Text.Trim, _
                    txtInventoryDescription.Text.Trim, txtInventoryDiamondDescription.Text.Trim, txtSize.Text.Trim, _
                    Val(txtWeight.Text), Val(txtStoneWeight.Text), Val(txtCostPerGramI.Text), _
                    Val(txtPMCCost.Text), Val(txtSMCCost.Text), Val(txtPCS.Text), Val(txtGoldLoss.Text), Val(txtTotalCost.Text), _
                    txtJobsheetNo.Text.Trim, txtInvoiceNo.Text.Trim, Trim(sPDate), txtSupplierCode.Text.Trim, _
                    "", "", 0, 0, 0, txtCategoryCode.Text.Trim, Trim(sClassCode), txtCatalogueCode.Text.Trim, txtTrayId.Text.Trim, _
                    Trim(sPMCType), Trim(sSMCType), CLng(txtPCS.Text), 0, txtRemarks.Text.Trim, sUserID) Then


                    sinventorycode = txtInventoryCode.Text.Trim
                Else
                    MsgBox("Error in Saving Inventory", MsgBoxStyle.Critical)
                    fncInventory = False
                    Exit Function
                End If
            Else
                MsgBox("Invoice Date is Invalid...!", MsgBoxStyle.Critical)
                Exit Function
            End If

            bPurchase = objPurchase.PurchaseSaveML(txtJobsheetNo.Text.Trim, _
                                txtInvoiceNo.Text.Trim, " ", txtSupplierCode.Text.Trim, Trim(sinventorycode), _
                                txtInventoryDescription.Text.Trim, txtInventoryDiamondDescription.Text.Trim, Val(txtStoneWeight.Text), _
                                Val(txtCostPerGramI.Text), Val(txtSize.Text.Trim), Val(txtWeight.Text), Val(txtPMCCost.Text), Val(txtPCS.Text), Val(txtGoldLoss.Text), Val(txtTotalCost.Text), _
                                txtCatalogueCode.Text.Trim, txtCategoryCode.Text.Trim, Trim(sClassCode), txtTrayId.Text.Trim, "JP", _
                                txtRemarks.Text.Trim, sUserID)
            If Not bPurchase Then
                MsgBox("Error in Saving Purchase", MsgBoxStyle.Critical)
                fncInventory = False
                Exit Function
            End If
            txtInventoryCode.Text = sinventorycode

            'Weight saved is the piece weight; purchase cost is without mc; purchase mc is the total mc

            bPurchaseBill = objPurchase.PurchaseBillSave(txtSupplierCode.Text.Trim, txtInvoiceNo.Text.Trim, Trim(sPDate), _
                        Trim(sPayType), Val(txtWeight.Text), Val(txtTotalCost.Text) - Val(txtPMCCost.Text), Val(txtPMCCost.Text), _
                        "JP", sUserID)
            If Not bPurchaseBill Then
                MsgBox("Error in Saving PurchaseBill", MsgBoxStyle.Critical)
                fncInventory = False
                Exit Function
            End If
            fncInventory = True
            txtInventoryCode.Text = sinventorycode

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Function

    Private Function fncCheckWtPCS() As Boolean
        Dim sCatalogueCode As String
        Dim lTotalWt As Double
        Dim lTotalPCS As Long
        Dim lMaxWt As Double
        Dim lAllMaxWt As Double
        Dim lMaxPCS As Long
        Dim sQry As String
        Dim Ds As DataSet
        Try
            sCatalogueCode = txtCatalogueCode.Text.Trim

            sQry = "Select SUM(Weight), SUM(PCS) from tblPurchase where CatalogueCode = " & _
                        fncFormatSQLText(sCatalogueCode) & " and JobSheetNo = " & fncFormatSQLText(txtJobsheetNo.Text.Trim)

            Ds = dm.getDataSetWithSqlAdapter(sQry, "Purchase", "Purchase")

            If Ds.Tables("Purchase").Rows.Count > 0 Then
                lTotalWt = Val(Ds.Tables("Purchase").Rows(0)(0) & "") + Val(txtWeight.Text)
                lTotalPCS = Val(Ds.Tables("Purchase").Rows(0)(1) & "") + Val(txtPCS.Text)
            End If

            If bEdited And grdPurchase.ActiveRow <> grdPurchase.MaxRows Then
                lTotalWt = lTotalWt - Val(GetText(grdPurchase, grdPurchase.ActiveRow, 4))
                lTotalPCS = lTotalPCS - Val(GetText(grdPurchase, grdPurchase.ActiveRow, 9))
            End If

            sQry = "Select Weight, PCS from tblJobSheetDetail where CatalogueCode = " & _
                        fncFormatSQLText(sCatalogueCode) & " and JobSheetNo = " & fncFormatSQLText(txtJobsheetNo.Text)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "JobSheet", "Jobsheet")

            If Ds.Tables("JobSheet").Rows.Count > 0 Then
                lMaxWt = Val(Ds.Tables("JobSheet").Rows(0)(0) & "")
                lMaxPCS = Val(Ds.Tables("JobSheet").Rows(0)(1) & "")
            End If

            'check the weight for each inventory irrespective of the PCS or Weight in the Class.

            lAllMaxWt = lMaxWt + ((dPurWtVarPerc * lMaxWt) / 100)
            If lTotalWt > lAllMaxWt Then
                MsgBox("Inventory Weight(" & Format$(lTotalWt, "#0.00") & " gm) exceeds the Catalogue Weight(" & _
                            Format$(lMaxWt, "#0.00") & " gm)!", MsgBoxStyle.Information)
                If txtWeight.Enabled Then txtWeight.Focus()
            ElseIf mspurchasetype = "P" Then
                If lTotalPCS > lMaxPCS Then
                    MsgBox("Inventory pieces exceed the total pieces of the Catalogue!", MsgBoxStyle.Critical)
                End If
                fncCheckWtPCS = True
            Else
                fncCheckWtPCS = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try

    End Function

    Private Sub FillTotalCost()
        Try
            If optPMCGram.Checked Then
                txtPMCCost.Text = Val(txtPMC.Text) * Val(txtWeight.Text)
            Else
                txtPMCCost.Text = Val(txtPMC.Text) * IIf(Val(txtPCS.Text) > 0, Val(txtPCS.Text), 1)
            End If
            txtPMCCost.Text = Format(Val(txtPMCCost.Text), "#0.00")

            If optSMCGram.Checked Then
                txtSMCCost.Text = Val(txtSMC.Text) * Val(txtWeight.Text)
            Else
                txtSMCCost.Text = Val(txtSMC.Text) * IIf(Val(txtPCS.Text) > 0, Val(txtPCS.Text), 1)
            End If
            txtSMCCost.Text = Format(Math.Round(Val(txtSMCCost.Text)), "#0.00")

            If Trim(sPurchaseCostingGL) = "Y" Then
                dCost = (Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGramI.Text) + Val(txtPMCCost.Text)
            Else
                dCost = (Val(txtWeight.Text)) * Val(txtCostPerGramI.Text) + Val(txtPMCCost.Text)
            End If
            dForeignCost = dCost

            If Val(txtCurrencyRate.Text) <> 0 Then
                dLocalCost = dForeignCost '* Val(txtCurrencyRate)
            Else
                dLocalCost = dCost
            End If

            txtTotalCost.Text = Format(dLocalCost, "#0.00")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Function fncValidateInventory() As Boolean

        Try
            If txtInventoryCode.Text.Trim = "" Then
                MsgBox("Inventory Code Cannot be Blank ....", MsgBoxStyle.Critical)
                If txtCatalogueCode.Enabled Then txtCatalogueCode.Focus()
                fncValidateInventory = False
                Exit Function
            End If

            If txtCatalogueCode.Text.Trim = "" Then
                MsgBox("Catalogue Code Cannot be Blank ....", MsgBoxStyle.Critical)
                If txtCatalogueCode.Enabled Then txtCatalogueCode.Focus()
                fncValidateInventory = False
                Exit Function
            End If

            If txtCategoryCode.Text.Trim = "" Then
                MsgBox("Category Code Cannot be Blank ....", MsgBoxStyle.Critical)
                If txtCatalogueCode.Enabled Then txtCatalogueCode.Focus()
                fncValidateInventory = False
                Exit Function
            End If


            If txtTrayId.Text.Trim = "" Then
                MsgBox("Tray Id Cannot be Blank ....", MsgBoxStyle.Critical)
                txtTrayId.Focus()
                fncValidateInventory = False
                Exit Function
            End If

            Dim Ds As DataSet
            Dim objtray As New clsTray

            If txtTrayId.Text.Trim <> "" Then
                Ds = objtray.GetTraywithCode(txtTrayId.Text.Trim)
                If Ds.Tables("Tray").Rows.Count = 0 Then
                    MsgBox("Invalid tray ID!", MsgBoxStyle.Critical)
                    txtTrayId.Focus()
                    Exit Function
                End If
            End If


            If txtInventoryDescription.Text.Trim = "" Then
                MsgBox("Description Cannot be Blank ....", MsgBoxStyle.Critical)
                fncValidateInventory = False
                Exit Function
            End If

            If mspurchasetype = "P" Or mspurchasetype = "B" Then
                If Val(txtWeight.Text) < 0 Then
                    MsgBox("Invalid weight!", MsgBoxStyle.Critical)
                    If txtWeight.Enabled Then txtWeight.Focus()
                    fncValidateInventory = False
                    Exit Function
                End If
            Else
                If Val(txtWeight.Text) <= 0 Then
                    MsgBox("Invalid weight!", MsgBoxStyle.Critical)
                    If txtWeight.Enabled Then txtWeight.Focus()
                    fncValidateInventory = False
                    Exit Function
                End If
            End If

            If Val(txtCostPerGramI.Text) <= 0 And mspurchasetype <> "P" Then
                MsgBox("Invalid cost per gram!", MsgBoxStyle.Critical)
                If txtCostPerGramI.Enabled Then txtCostPerGramI.Focus()
                fncValidateInventory = False
                Exit Function
            End If

            If txtInventoryDiamondDescription.Text.Trim = "" And mspurchasetype = "P" Then
                MsgBox("Description Cannot be Blank ....", MsgBoxStyle.Critical)
                If txtInventoryDiamondDescription.Enabled Then txtInventoryDiamondDescription.Focus()
                fncValidateInventory = False
                Exit Function
            End If

            If txtInventoryDiamondDescription.Text.Trim <> "" And mspurchasetype <> "P" Then
                If Val(txtStoneWeight.Text) = 0 Then
                    MsgBox("Please enter the Stone weight!", MsgBoxStyle.Critical)
                    If txtStoneWeight.Enabled Then txtStoneWeight.Focus()
                    fncValidateInventory = False
                    Exit Function
                End If
                'ElseIf Val(txtStoneWeight.Text) > 0 Then
                '    MsgBox("Please enter the Stone name!", MsgBoxStyle.Critical)
                '    If txtInventoryDiamondDescription.Enabled Then txtInventoryDiamondDescription.Focus()
                '    fncValidateInventory = False
                '    Exit Function
            End If

            If Val(txtPCS.Text) < 1 Then
                MsgBox("Invalid pcs!", MsgBoxStyle.Critical)
                If txtPCS.Enabled Then txtPCS.Focus()
                fncValidateInventory = False
                Exit Function
            End If

            fncValidateInventory = True

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Function

    Private Sub btnBarCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBarCode.Click
        Dim objClass As New ClsClass
        Dim dS As DataSet
        Dim sClass As String
        Dim sPurType As String

        Try
            sPurType = ""
            sClass = Trim(GetText(grdPurchase, grdPurchase.ActiveRow, 2))
            dS = objClass.GetClassDetail(Trim(sClass))
            If dS.Tables("Class").Rows.Count > 0 Then
                sPurType = Trim(dS.Tables("Class").Rows(0)("PurchaseType") & "")
            End If
            dS = Nothing
            objClass = Nothing
            If sPurType = "W" Then
                If sCompanyShortCode = "ES" Or sCompanyShortCode = "GMT" Or sCompanyShortCode = "ML" Then
                    Dim objfrm As New frmBarcode
                    objfrm.txtInventoryCode.Text = Trim(GetText(grdPurchase, grdPurchase.ActiveRow, 1))
                    objfrm.txtInventoryCode_LostFocus(sender, e)
                    objfrm.btnPrint_Click(sender, e)
                ElseIf sCompanyShortCode = "AS" Then
                    Dim objfrm As New frmJewelBarcode
                    objfrm.txtInventoryCode.Text = Trim(GetText(grdPurchase, grdPurchase.ActiveRow, 1))
                    objfrm.txtInventoryCode_LostFocus(sender, e)
                    objfrm.btnPrint_Click(sender, e)
                End If
            ElseIf sPurType = "P" Then
                If sCompanyShortCode = "GMT" Or sCompanyShortCode = "ML" Then
                    Dim objfrm As New frmBarcode
                    objfrm.txtInventoryCode.Text = Trim(GetText(grdPurchase, grdPurchase.ActiveRow, 1))
                    objfrm.txtInventoryCode_LostFocus(sender, e)
                    objfrm.btnPrint_Click(sender, e)
                Else
                    Dim objfrm As New frmJewelBarcode
                    objfrm.txtInventoryCode.Text = Trim(GetText(grdPurchase, grdPurchase.ActiveRow, 1))
                    objfrm.txtInventoryCode_LostFocus(sender, e)
                    objfrm.btnPrint_Click(sender, e)
                End If
            End If
            subClearDetail(True)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim sinventorycode As String
        Dim sSql As String
        Dim Ds As DataSet
        Dim Ds1 As DataSet
        Dim objPurchase As New clsPurchase
        Dim i As Long
        Dim bdelete As Boolean
        Try

            i = grdPurchase.ActiveRow
            If sBillClose = "Y" Then
                MsgBox("This bill is closed. Cannot delete!", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If sTranType = "DP" Then
                MsgBox("This is a direct purchase bill. Cannot delete!", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If grdPurchase.ActiveRow <> grdPurchase.MaxRows And grdPurchase.ActiveRow > 0 Then
                If MsgBox("Do you want to delete item no. " & Trim(GetText(grdPurchase, grdPurchase.ActiveRow, 1)) & "?", vbYesNo) = vbNo Then
                    Exit Sub
                End If
            Else
                MsgBox("Select item for deletion!", vbCritical)
                Exit Sub
            End If

            sinventorycode = Trim(GetText(grdPurchase, grdPurchase.ActiveRow, 1))

            If sinventorycode <> "Total:" And Trim(sinventorycode) <> "" Then
                'don't allow to change or delete the sold inventory.
                sSql = "Select SalesBillNo, CreateDate, QtyOnHand, PCS from tblInventory where InventoryCode = " & fncFormatSQLText(sinventorycode)

                Ds = dm.getDataSetWithSqlAdapter(sSql, "Inventory", "Inventory")

                If Ds.Tables("Inventory").Rows.Count > 0 Then
                    If Trim$(Ds.Tables("Inventory").Rows(0)("SalesBillNo") & "") <> "" Then
                        MsgBox("Item sold. Cannot delete!", MsgBoxStyle.Critical)
                        Exit Sub
                    ElseIf Val(Ds.Tables("Inventory").Rows(0)("QtyOnHand") & "") <> Val(Ds.Tables("Inventory").Rows(0)("Pcs") & "") Then 'To avoid deletion after purchase return
                        MsgBox("Qty on hand is not the same as no. of pieces purchased. Cannot delete!", MsgBoxStyle.Critical)
                        Exit Sub
                    Else
                        sSql = "Select EODDate from tblLocation where LocationCode = " & fncFormatSQLText(gthisLocation)
                        Ds1 = dm.getDataSetWithSqlAdapter(sSql, "Location", "Location")

                        If Ds1.Tables("Location").Rows.Count > 0 Then
                            If IsDate(Ds1.Tables("Location").Rows(0)("EODDate") & "") Then
                                If DateValue(Ds1.Tables("Location").Rows(0)("EODDate") & "") >= DateValue(Ds.Tables("Inventory").Rows(0)("CreateDate") & "") Then
                                    MsgBox("You cannot delete the Inventory after EOD!", MsgBoxStyle.Critical)
                                    Exit Sub
                                End If
                            End If
                        End If
                    End If

                    bEdited = True
                    'delete the inventory from the Purchase and inventory table

                    gsConBatchPosting.ConnectionString = gsCon
                    gsConBatchPosting.Open()
                    gsTrans = gsConBatchPosting.BeginTransaction
                    If objPurchase.PurchaseDelete(Trim(sinventorycode), txtInvoiceNo.Text.Trim, txtSupplierCode.Text.Trim) Then
                        gsTrans.Commit()

                        'remove the deleted row.
                        grdPurchase.Action = FPSpread.ActionConstants.ActionDeleteRow
                        grdPurchase.MaxRows = grdPurchase.MaxRows - 1
                        If grdPurchase.MaxRows = 1 Then
                            grdPurchase.MaxRows = 0
                            subGetJobSheetDetails()
                        Else
                            subTotal()
                        End If
                        subClearCatalogue()
                        subClearDetail(True)
                    Else
                        MsgBox("Error in Deletion", MsgBoxStyle.Critical)
                        gsTrans.Rollback()
                        Exit Sub
                    End If
                Else
                    MsgBox("Error in fetching inventory!", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        Finally
            If gsConBatchPosting.State = ConnectionState.Open Then gsConBatchPosting.Close()
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            subClearDetail(True)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub btnBillClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBillClose.Click
        Try
            Dim Ds As DataSet
            Dim DsP As DataSet
            Dim sSql As String
            Dim sJobsheetNo As String
            Dim objJobSheet As New clsJobSheet
            Dim dTotJobSheetWt As Double
            Dim dTotPurchaseWt As Double
            Dim dWtDiff As Double

            If sBillClose = "Y" Then
                MsgBox("This Job sheet bill is already closed!", MsgBoxStyle.Critical)
                Exit Sub
            End If
            If sTranType = "DP" Then
                MsgBox("This is a direct purchase bill. Use direct purchase screen to close this bill!", MsgBoxStyle.Critical)
                Exit Sub
            End If

            sJobsheetNo = txtJobsheetNo.Text.Trim
            If sJobsheetNo <> "" Then
                Ds = objJobSheet.GetJobSheet(sJobsheetNo)
                If Ds.Tables("JobSheet").Rows.Count > 0 Then
                    dTotJobSheetWt = Val(Ds.Tables("JobSheet").Rows(0)("TotalWeight24K") & "")
                    sSql = "Select SUM(Weight) from tblPurchase where JobSheetNo = " & fncFormatSQLText(sJobsheetNo)

                    DsP = dm.getDataSetWithSqlAdapter(sSql, "Purchase", "Purchase")
                    If DsP.Tables("Purchase").Rows.Count > 0 Then dTotPurchaseWt = Val(DsP.Tables("Purchase").Rows(0)(0) & "")
                    'allow .x% diff. (get from company) from jobsheet weight for the inventory weight


                    dWtDiff = Val(Format(Math.Abs(dTotJobSheetWt - dTotPurchaseWt), "#0.00"))
                    If dWtDiff > (dPurWtVarPerc * dTotJobSheetWt / 100) Then
                        MsgBox("Cannot close bill. The Inventory weight differs by more than " & Format(dPurWtVarPerc, "#0.00") & "% of the job sheet weight!", MsgBoxStyle.Critical)
                    Else
                        If MsgBox("You cannot make changes to closed bill. Would u like to close this job sheet bill?", vbOKCancel + vbQuestion) = vbOK Then
                            If objJobSheet.fncBillClose(sJobsheetNo) Then
                                MsgBox("Job sheet bill closed!", MsgBoxStyle.Information)
                                'lock all the fields.
                                subLockAllFlds(True)
                                sBillClose = "Y"
                                subClearDetail(False)
                                subClearCatalogue()
                            End If
                        End If
                    End If
                End If
            Else
                MsgBox("Please enter the job sheet no.!", MsgBoxStyle.Critical)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub txtCatalogueCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCatalogueCode.LostFocus
        Try
            If txtCatalogueCode.Text.Trim <> "" Then
                If Not fncValidCatalogueCode() Then
                    MsgBox("Please select the suitable catalogue for the selected job sheet!", MsgBoxStyle.Critical)
                    txtCatalogueCode.Text = ""
                    If txtCatalogueCode.Enabled Then Call txtCatalogueCode.Focus()
                    Exit Sub
                End If
            End If
            If Not bEdited Then subGetCatalogue()
            sCatalogueCode = txtCatalogueCode.Text.Trim
            'since catergory code is locked. call the subGetCategory fn. also.
            sCategoryCode = txtCategoryCode.Text.Trim
            If Not bEdited Then
                subGetCategory()
                subSetPurchaseType()
            End If
            If UCase(sCompanyShortCode) = "GMT" Then
                txtTrayId.Text = "DEF"
                txtInventoryDescription.Focus()
            Else
                txtTrayId.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Private Sub subSetPurchaseType()
        Dim objClass As New ClsClass
        Dim Ds As DataSet
        Try
            Ds = objClass.GetClassDetail(Trim(sClassCode))
            If Ds.Tables("Class").Rows.Count > 0 Then
                mspurchasetype = Trim(Ds.Tables("Class").Rows(0)("PurchaseType") & "")
                txtPCS.Enabled = Trim(Ds.Tables("Class").Rows(0)("PurchaseType" & "") = "P" Or Trim(Ds.Tables("Class").Rows(0)("PurchaseType") & "") = "B")
            End If
            Ds = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub subGetCategory()
        Dim i As Long
        Dim Ds As DataSet
        Dim DsJs As DataSet
        Dim sSql As String
        Dim objCategory As New ClsCategory
        Try
            If Trim(sCategoryCode) <> "" Then

                Ds = objCategory.GetCategorywithCode(Trim(sCategoryCode))

                If Ds.Tables("Category").Rows.Count > 0 Then
                    sClassCode = Trim(Ds.Tables("Category").Rows(0)("ClassCode") & "")
                    txtInventoryCode.Text = (Ds.Tables("Category").Rows(0)("NextInventoryCode") & "")
                    'get the jobsheet detail
                    sSql = "Select * from tblJobSheetDetail where JobSheetNo = " & fncFormatSQLText(sJobSheetCode) & _
                                " and CatalogueCode = " & fncFormatSQLText(Trim(sCatalogueCode))
                    DsJs = dm.getDataSetWithSqlAdapter(sSql, "JobSheet", "Jobsheet")


                    If DsJs.Tables("JobSheet").Rows.Count > 0 Then
                        txtCostPerGramI.Text = Val(DsJs.Tables("JobSheet").Rows(0)("CostperGram") & "")
                    End If
                Else
                    MsgBox("Error in fetching category!", MsgBoxStyle.Critical)
                End If
            End If
            Ds = Nothing
            DsJs = Nothing
            subGetSummary()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub subGetCatalogue()

        Dim i As Long
        Dim Ds As DataSet
        Dim dMCWtPCS As Double
        Dim dSMCWtPCS As Double
        Dim objCatalogue As New ClsCatalogue
        Try
            If txtCatalogueCode.Text.Trim <> "" Then

                Ds = objCatalogue.GetCatalogueCode(txtCatalogueCode.Text.Trim)
                If Ds.Tables("Catalogue").Rows.Count > 0 Then
                    txtCategoryCode.Text = Trim(Ds.Tables("Catalogue").Rows(0)("CategoryCode") & "")

                    'set PMCType and SMCType according to the previous row.
                    If grdPurchase.MaxRows > 1 Then
                        If Trim$(GetText(grdPurchase, grdPurchase.MaxRows - 1, 16)) = "P" Then
                            optPMCPCS.Checked = True
                            dMCWtPCS = Val(GetText(grdPurchase, grdPurchase.MaxRows - 1, 9))
                        Else
                            optPMCGram.Checked = True
                            dMCWtPCS = Val(GetText(grdPurchase, grdPurchase.MaxRows - 1, 4))
                        End If

                        If Trim$(GetText(grdPurchase, grdPurchase.MaxRows - 1, 17)) = "P" Then
                            optSMCPCS.Checked = True
                            dSMCWtPCS = Val(GetText(grdPurchase, grdPurchase.MaxRows - 1, 9))
                        Else
                            optSMCGram.Checked = True
                            dSMCWtPCS = Val(GetText(grdPurchase, grdPurchase.MaxRows - 1, 4))
                        End If
                    Else
                        If sCompanyShortCode = "ML" Then
                            optSMCPCS.Checked = True
                            optPMCPCS.Checked = True
                        Else
                            optPMCGram.Checked = True
                            optSMCGram.Checked = True
                        End If
                        dMCWtPCS = Val(GetText(grdPurchase, grdPurchase.MaxRows - 1, 4))
                        dSMCWtPCS = Val(GetText(grdPurchase, grdPurchase.MaxRows - 1, 4))
                    End If

                    'display the last desc
                    If txtCatalogueCode.Text.Trim <> sCatalogueCode Then
                        txtInventoryDescription.Text = Trim(Ds.Tables("Catalogue").Rows(0)("CatalogueName") & "")
                    ElseIf grdPurchase.MaxRows > 1 Then
                        txtInventoryDescription.Text = GetText(grdPurchase, grdPurchase.MaxRows - 1, 3)
                        'txtGoldLoss.Text = Val(GetText(grdPurchase, grdPurchase.MaxRows - 1, 5))
                        txtPCS.Text = Val(GetText(grdPurchase, grdPurchase.MaxRows - 1, 9))
                    Else
                        txtInventoryDescription.Text = Trim(Ds.Tables("catalogue").Rows(0)("CatalogueName") & "")
                    End If
                    txtPCS.Text = "1"
                End If
            Else
                subClearCatalogue()
                subClearDetail(False)
            End If
            Ds = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Function fncValidCatalogueCode() As Boolean
        Dim sSelect As String
        Dim i As Integer
        Dim sCatalogueCode As String
        Dim Ds As DataSet

        If txtJobsheetNo.Text.Trim = "" Then
            fncValidCatalogueCode = False
            Exit Function
        End If
        sCatalogueCode = txtCatalogueCode.Text.Trim
        sSelect = "Select CatalogueCode from tblCatalogue Where CatalogueCode in " & _
                "(Select DISTINCT(CatalogueCode) From tblJobsheetDetail Where " & _
                "JobSheetNo = " & fncFormatSQLText(Trim$(txtJobsheetNo.Text)) & ")"
        Ds = dm.getDataSetWithSqlAdapter(sSelect, "JobSheet", "JobSheet")
        For i = 0 To Ds.Tables("JobSheet").Rows.Count - 1
            If Trim(UCase(sCatalogueCode)) = UCase(Trim(Ds.Tables("JobSheet").Rows(i)("CatalogueCode") & "")) Then
                txtCatalogueCode.Text = Trim(Ds.Tables("JobSheet").Rows(i)("CatalogueCode") & "")
                sCatalogueCode = txtCatalogueCode.Text.Trim
                fncValidCatalogueCode = True
                Exit Function
            End If
        Next
    End Function

    Private Sub txtTrayId_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTrayId.LostFocus
        Dim Ds As DataSet
        Dim objTray As New clsTray
        Try
            If txtTrayId.Text.Trim <> "" Then
                If txtTrayId.Text.Trim <> "" Then
                    Ds = objTray.GetTraywithCode(txtTrayId.Text.Trim)
                    If Ds.Tables("Tray").Rows.Count <= 0 Then
                        MsgBox("Invalid tray ID!", MsgBoxStyle.Critical)
                        If txtTrayId.Enabled Then txtTrayId.Focus()
                        Exit Sub
                    Else
                        txtTrayId.Text = Trim(Ds.Tables("Tray").Rows(0)("TrayID") & "")
                        txtSize.Focus()
                        'txtWeight.Focus()
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub txtPMC_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPMC.LostFocus
        Try
            If Val(txtPMC.Text) >= 0 Then
                If optPMCGram.Checked Then
                    txtPMCCost.Text = Format(Val(txtPMC.Text) * Val(txtWeight.Text), "#0.00")
                Else
                    txtPMCCost.Text = Format(Val(txtPMC.Text) * Val(txtPCS.Text), "#0.00")
                End If
            Else
                txtPMCCost.Text = "0.00"
            End If
            If chkAutoSMC.Checked = True Then
                If optSMCGram.Checked Then
                    txtSMCCost.Text = Val(txtPMC.Text) * Val(txtWeight.Text) * 5
                Else
                    txtSMCCost.Text = Val(txtPMC.Text) * Val(txtPCS.Text) * 5
                End If
                txtSMC.Text = Format(Math.Round(Val(txtSMCCost.Text)), "#0.00")
                txtSMCCost.Text = Format(Math.Round(Val(txtSMCCost.Text)), "#0.00")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub txtSMC_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSMC.LostFocus
        Try
            If chkAutoSMC.Checked = True Then

                If optSMCGram.Checked Then
                    txtSMCCost.Text = Val(txtPMC.Text) * Val(txtWeight.Text) * 5
                Else
                    txtSMCCost.Text = Val(txtPMC.Text) * Val(txtPCS.Text) * 5
                End If
                txtSMC.Text = Format(Math.Round(Val(txtSMCCost.Text)), "#0.00")
                txtSMCCost.Text = Format(Math.Round(Val(txtSMCCost.Text)), "#0.00")
            Else
                If optSMCGram.Checked Then
                    txtSMCCost.Text = Val(txtSMC.Text) * Val(txtWeight.Text)
                Else
                    txtSMCCost.Text = Val(txtSMC.Text) * Val(txtPCS.Text)
                End If
                txtSMCCost.Text = Format(Math.Round(Val(txtSMCCost.Text)), "#0.00")

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub btnAdd_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.GotFocus
        Try
            If Trim(sPurchaseCostingGL) = "Y" Then
                txtTotalCost.Text = Format((Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGramI.Text) + _
                    IIf(optPMCGram.Checked, Val(txtPMC.Text) * Val(txtWeight.Text), Val(txtPCS.Text) * Val(txtPMC.Text)), "#0.00")
            Else
                txtTotalCost.Text = Format(Val(txtWeight.Text) * Val(txtCostPerGramI.Text) + _
                    IIf(optPMCGram.Checked, Val(txtPMC.Text) * Val(txtWeight.Text), Val(txtPCS.Text) * Val(txtPMC.Text)), "#0.00")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub btnTrayIdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTrayIdSearch.Click
        Try
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblTrayMaster", "L", 4, "TrayName", "", "TrayID", "TrayID", 80, "TrayName", "TrayName", 180, "SalesManID", "SalesManID", 60, "SalesManName", "SalesManName", 80)
            frmHelpClass.ShowDialog()
            txtTrayId.Text = frmHelpClass.Var1.Trim
            'If txtTrayId.Enabled Then txtTrayId.Focus()
            txtTrayId_LostFocus(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subAutoManual()
        Try
            If lblAutoManual.Text = "MANUAL (F10)" Then
                lblAutoManual.Text = "AUTO (F10)"
                lblAutoManual.BackColor = Color.Red
            ElseIf lblAutoManual.Text = "AUTO (F10)" Then
                lblAutoManual.Text = "MANUAL (F10)"
                lblAutoManual.BackColor = Color.Green
            Else
                lblAutoManual.Text = "MANUAL (F5)"
                lblAutoManual.BackColor = Color.Green
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try

    End Sub

    Private Sub btnCatalogueCodeSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCatalogueCodeSearch.Click
        Try
            Dim Search As String
            Search = " CatalogueCode in (Select DISTINCT(CatalogueCode) From tblJobsheetDetail " & _
                                        "Where JobSheetNo = " & fncFormatSQLText(txtJobsheetNo.Text.Trim) & " )"
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCatalogue", "L", 4, "CatalogueName", Search, "CatalogueCode", "CatalogueCode", 80, "CatalogueName", "CatalogueName", 180, "CategoryCode", "CategoryCode", 60, "GoldLoss", "GoldLoss", 80)
            frmHelpClass.ShowDialog()
            txtCatalogueCode.Text = frmHelpClass.Var1.Trim
            'If txtCatalogueCode.Enabled Then txtCatalogueCode.Focus()
            txtCatalogueCode_LostFocus(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            Err.Clear()
        End Try
    End Sub

    Private Sub txtWeight_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWeight.LostFocus
        Try
            Dim objPurchase As New clsPurchase
            Dim Ds As DataSet

            If Val(txtWeight.Text) = 0 And lblAutoManual.Text = "AUTO (F10)" Then
                If txtWeight.Enabled Then txtWeight.Focus()
                Exit Sub
            ElseIf lblAutoManual.Text = "MANUAL (F10)" Then
                'Get goldloss from table
                If Val(txtWeight.Text) > 0 Then
                    Ds = objPurchase.GetGoldLossByWeight(Val(txtWeight.Text))
                    If Ds.Tables("Purchase").Rows.Count > 0 Then
                        txtGoldLoss.Text = Format(Val(Ds.Tables("Purchase").Rows(0)("GoldLoss") & ""), "#0.00")
                    End If
                End If
                If txtGoldLoss.Enabled Then txtGoldLoss.Focus()
                Exit Sub
            Else
                If Val(txtWeight.Text) > 0 Then
                    Ds = objPurchase.GetGoldLossByWeight(Val(txtWeight.Text))
                    If Ds.Tables("Purchase").Rows.Count > 0 Then
                        txtGoldLoss.Text = Format(Val(Ds.Tables("Purchase").Rows(0)("GoldLoss") & ""), "#0.00")
                    End If
                End If

                If Trim(sPurchaseCostingGL) = "Y" Then
                    dCost = (Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGramI.Text) + Val(txtPMCCost.Text)
                Else
                    dCost = (Val(txtWeight.Text)) * Val(txtCostPerGramI.Text) + Val(txtPMCCost.Text)
                End If

                txtPMC_LostFocus(sender, e)
                txtSMC_LostFocus(sender, e)
                btnAdd_Click(sender, e)
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub frmPurchase_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                SendKeys.Send("{Tab}")
            Case Keys.Up
                SendKeys.Send("+{Tab}")
            Case Keys.Down
                SendKeys.Send("{Tab}")
            Case Keys.F6
                btnBillClear_Click(sender, e)
            Case Keys.F7
                btnBillClose_Click(sender, e)
            Case Keys.Escape
                btnClose_Click(sender, e)
            Case Keys.F10
                subAutoManual()
        End Select
    End Sub

    Private Sub txtCatalogueCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCatalogueCode.KeyDown
        If e.KeyCode = Keys.F1 Then
            Dim Search As String
            Search = " CatalogueCode in (Select DISTINCT(CatalogueCode) From tblJobsheetDetail " & _
                                        "Where JobSheetNo = " & fncFormatSQLText(txtJobsheetNo.Text.Trim) & " )"
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCatalogue", "L", 4, "CatalogueName", Search, "CatalogueCode", "CatalogueCode", 80, "CatalogueName", "CatalogueName", 180, "CategoryCode", "CategoryCode", 60, "GoldLoss", "GoldLoss", 80)
            frmHelpClass.ShowDialog()
            txtCatalogueCode.Text = frmHelpClass.Var1.Trim
        End If
    End Sub

    Private Sub txtTrayId_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTrayId.KeyDown
        If e.KeyCode = Keys.F1 Then
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblTrayMaster", "L", 4, "TrayName", "", "TrayID", "TrayID", 80, "TrayName", "TrayName", 180, "SalesManID", "SalesManID", 60, "SalesManName", "SalesManName", 80)
            frmHelpClass.ShowDialog()
            txtTrayId.Text = frmHelpClass.Var1.Trim
        End If
    End Sub
    Private Sub txtCurrencyCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCurrencyCode.LostFocus
        Try
            sCurrencycode = Trim(txtCurrencyCode.Text.Trim)
            subGetCurrency()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Private Sub subGetCurrency()
        Dim ObjCurrency As New clsCurrency
        Dim Ds As DataSet
        Try
            If sCurrencycode <> "" Then
                Ds = ObjCurrency.GetCurrency(sCurrencycode)
                If Ds.Tables("Currency").Rows.Count Then
                    txtCurrencyName.Text = Ds.Tables("Currency").Rows(0)("CurrencyName")
                    txtCurrencyRate.Text = Format(Ds.Tables("Currency").Rows(0)("CurrencyRate"), "0.00")
                Else
                    MessageBox.Show("Set Default Currency in Currency Master", "GoldPOS")
                    txtCurrencyCode.Clear()
                    txtCurrencyName.Clear()
                    txtCurrencyRate.Clear()
                    sCurrencycode = ""
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub txtJobsheetNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtJobsheetNo.TextChanged

    End Sub

    Private Sub frmPurchaseML_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        ' txtJobsheetNo.Focus()
    End Sub

    Private Sub txtWeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtWeight.TextChanged

    End Sub
End Class
