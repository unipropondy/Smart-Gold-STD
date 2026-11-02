Public Class frmPurchase
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
    Dim sSizeType As String
    Public dWeightGrd, dPMCGrd, dSMCGrd, dPCSGrd, dTotalCostGrd, dNetWeightGrd As Double

    Friend WithEvents btnSizeCodeSearch As System.Windows.Forms.Button
    Friend WithEvents txtSizeCode As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents cboTealPercent1 As System.Windows.Forms.ComboBox
    Friend WithEvents btnCategoryCodeSearch As System.Windows.Forms.Button
    Friend WithEvents chkAdditionalDesc As System.Windows.Forms.CheckBox
    Friend WithEvents grpAddlDescription As System.Windows.Forms.GroupBox
    Friend WithEvents cmdAddlDescOk As System.Windows.Forms.Button
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents txtDesc4 As System.Windows.Forms.TextBox
    Friend WithEvents txtDesc3 As System.Windows.Forms.TextBox
    Friend WithEvents txtDesc2 As System.Windows.Forms.TextBox
    Friend WithEvents txtDesc1 As System.Windows.Forms.TextBox
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents txtSellingPrice4 As System.Windows.Forms.TextBox
    Friend WithEvents txtSellingPrice3 As System.Windows.Forms.TextBox
    Friend WithEvents txtSellingPrice2 As System.Windows.Forms.TextBox
    Friend WithEvents txtSellingPrice1 As System.Windows.Forms.TextBox
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents txtCost4 As System.Windows.Forms.TextBox
    Friend WithEvents txtCost3 As System.Windows.Forms.TextBox
    Friend WithEvents txtCost2 As System.Windows.Forms.TextBox
    Friend WithEvents txtCost1 As System.Windows.Forms.TextBox
    Friend WithEvents grdJobSheet As System.Windows.Forms.DataGridView
    Friend WithEvents cCategoryJobsheet As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cBillWt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cInvWt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cBalance As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grdPurchase As System.Windows.Forms.DataGridView
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents txtBarNo As System.Windows.Forms.TextBox
    Friend WithEvents chkSalesPCS As System.Windows.Forms.CheckBox
    Friend WithEvents chkImage As System.Windows.Forms.CheckBox
    Friend WithEvents grpImage As System.Windows.Forms.GroupBox
    Friend WithEvents btnBrowseImage As System.Windows.Forms.Button
    Friend WithEvents picImage As System.Windows.Forms.PictureBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents cboTealPercent As System.Windows.Forms.TextBox
    Friend WithEvents cItemCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cClass As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cNetWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSize As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cGoldLoss As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCostPerGram As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cPMC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSMC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cPCS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cTotalCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCatalogueCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCategoryCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cStoneDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cStoneWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cMCType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSMCType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cTrayID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TxtPcsInventoryCode As System.Windows.Forms.TextBox
    Dim dForeignCost As Double
    Dim sSaveInventoryCode As String
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
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents chkAutoSMC As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents opt22K As System.Windows.Forms.RadioButton
    Friend WithEvents opt24K As System.Windows.Forms.RadioButton
    Friend WithEvents gbIPM As System.Windows.Forms.GroupBox
    Friend WithEvents cboBarCoin As System.Windows.Forms.ComboBox
    Friend WithEvents cboPurity As System.Windows.Forms.ComboBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents cboRefiner As System.Windows.Forms.ComboBox
    Friend WithEvents lblRefiner As System.Windows.Forms.Label
    Friend WithEvents cboCountry As System.Windows.Forms.ComboBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents cboType As System.Windows.Forms.ComboBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents cboGSTType As System.Windows.Forms.ComboBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents chkIPM As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPurchase))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cboTealPercent = New System.Windows.Forms.TextBox()
        Me.cboTealPercent1 = New System.Windows.Forms.ComboBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.opt24K = New System.Windows.Forms.RadioButton()
        Me.opt22K = New System.Windows.Forms.RadioButton()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.chkGST = New System.Windows.Forms.CheckBox()
        Me.txtTEAL = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtCostPerGram = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.optGold = New System.Windows.Forms.RadioButton()
        Me.optCash = New System.Windows.Forms.RadioButton()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtCurrencyName = New System.Windows.Forms.TextBox()
        Me.txtCurrencyCode = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtSupplierName = New System.Windows.Forms.TextBox()
        Me.btnSupplierCodeSearch = New System.Windows.Forms.Button()
        Me.txtSupplierCode = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtIYYYY = New System.Windows.Forms.TextBox()
        Me.txtIMM = New System.Windows.Forms.TextBox()
        Me.txtIDD = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnJobSheetNoSearch = New System.Windows.Forms.Button()
        Me.txtJobsheetNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCurrencyRate = New System.Windows.Forms.TextBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TxtPcsInventoryCode = New System.Windows.Forms.TextBox()
        Me.chkAdditionalDesc = New System.Windows.Forms.CheckBox()
        Me.btnCategoryCodeSearch = New System.Windows.Forms.Button()
        Me.btnSizeCodeSearch = New System.Windows.Forms.Button()
        Me.txtSizeCode = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.grpAddlDescription = New System.Windows.Forms.GroupBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.txtSellingPrice4 = New System.Windows.Forms.TextBox()
        Me.txtSellingPrice3 = New System.Windows.Forms.TextBox()
        Me.txtSellingPrice2 = New System.Windows.Forms.TextBox()
        Me.txtSellingPrice1 = New System.Windows.Forms.TextBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.txtCost4 = New System.Windows.Forms.TextBox()
        Me.txtCost3 = New System.Windows.Forms.TextBox()
        Me.txtCost2 = New System.Windows.Forms.TextBox()
        Me.txtCost1 = New System.Windows.Forms.TextBox()
        Me.cmdAddlDescOk = New System.Windows.Forms.Button()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.txtDesc4 = New System.Windows.Forms.TextBox()
        Me.txtDesc3 = New System.Windows.Forms.TextBox()
        Me.txtDesc2 = New System.Windows.Forms.TextBox()
        Me.txtDesc1 = New System.Windows.Forms.TextBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.gbIPM = New System.Windows.Forms.GroupBox()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.txtBarNo = New System.Windows.Forms.TextBox()
        Me.cboBarCoin = New System.Windows.Forms.ComboBox()
        Me.cboPurity = New System.Windows.Forms.ComboBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.cboRefiner = New System.Windows.Forms.ComboBox()
        Me.lblRefiner = New System.Windows.Forms.Label()
        Me.cboCountry = New System.Windows.Forms.ComboBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.cboType = New System.Windows.Forms.ComboBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.cboGSTType = New System.Windows.Forms.ComboBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.lblAutoManual = New System.Windows.Forms.Label()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnBarCode = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.txtSMCCost = New System.Windows.Forms.TextBox()
        Me.txtPMCCost = New System.Windows.Forms.TextBox()
        Me.txtTotalCost = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtPCS = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtSMC = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtPMC = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtStoneWeight = New System.Windows.Forms.TextBox()
        Me.txtInventoryDiamondDescription = New System.Windows.Forms.TextBox()
        Me.txtCostPerGramI = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtGoldLoss = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtWeight = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtInventoryDescription = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtInventoryCode = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.chkAutoSMC = New System.Windows.Forms.CheckBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.optSMCPCS = New System.Windows.Forms.RadioButton()
        Me.optSMCGram = New System.Windows.Forms.RadioButton()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.optPMCPCS = New System.Windows.Forms.RadioButton()
        Me.optPMCGram = New System.Windows.Forms.RadioButton()
        Me.txtInventoryPCS = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtInventoryWeight = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtBillPCS = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtBillWeight = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.btnTrayIdSearch = New System.Windows.Forms.Button()
        Me.txtTrayId = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtCategoryCode = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.btnCatalogueCodeSearch = New System.Windows.Forms.Button()
        Me.txtCatalogueCode = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.chkIPM = New System.Windows.Forms.CheckBox()
        Me.btnBillClose = New System.Windows.Forms.Button()
        Me.btnBillClear = New System.Windows.Forms.Button()
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.grdJobSheet = New System.Windows.Forms.DataGridView()
        Me.cCategoryJobsheet = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cBillWt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cInvWt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cBalance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grdPurchase = New System.Windows.Forms.DataGridView()
        Me.cItemCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cClass = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cDescription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cNetWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cSize = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cGoldLoss = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cCostPerGram = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cPMC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cSMC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cPCS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cTotalCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cCatalogueCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cCategoryCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cStoneDescription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cStoneWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cMCType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cSMCType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cTrayID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chkSalesPCS = New System.Windows.Forms.CheckBox()
        Me.chkImage = New System.Windows.Forms.CheckBox()
        Me.grpImage = New System.Windows.Forms.GroupBox()
        Me.btnBrowseImage = New System.Windows.Forms.Button()
        Me.picImage = New System.Windows.Forms.PictureBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.grpAddlDescription.SuspendLayout()
        Me.gbIPM.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdJobSheet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdPurchase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpImage.SuspendLayout()
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.cboTealPercent)
        Me.GroupBox1.Controls.Add(Me.cboTealPercent1)
        Me.GroupBox1.Controls.Add(Me.Label32)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
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
        Me.GroupBox1.Location = New System.Drawing.Point(5, 108)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(619, 168)
        Me.GroupBox1.TabIndex = 41
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "JobSheet Details"
        '
        'cboTealPercent
        '
        Me.cboTealPercent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboTealPercent.Enabled = False
        Me.cboTealPercent.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTealPercent.Location = New System.Drawing.Point(417, 111)
        Me.cboTealPercent.MaxLength = 20
        Me.cboTealPercent.Name = "cboTealPercent"
        Me.cboTealPercent.Size = New System.Drawing.Size(48, 20)
        Me.cboTealPercent.TabIndex = 476256
        Me.cboTealPercent.Tag = "NE;"
        '
        'cboTealPercent1
        '
        Me.cboTealPercent1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTealPercent1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTealPercent1.FormattingEnabled = True
        Me.cboTealPercent1.Items.AddRange(New Object() {".92", ".925", ".93", ".935", ".999"})
        Me.cboTealPercent1.Location = New System.Drawing.Point(479, 142)
        Me.cboTealPercent1.Name = "cboTealPercent1"
        Me.cboTealPercent1.Size = New System.Drawing.Size(51, 21)
        Me.cboTealPercent1.TabIndex = 78
        Me.cboTealPercent1.Visible = False
        '
        'Label32
        '
        Me.Label32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.Color.Black
        Me.Label32.Location = New System.Drawing.Point(367, 110)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(66, 29)
        Me.Label32.TabIndex = 77
        Me.Label32.Text = "TEAL Percent"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.opt24K)
        Me.GroupBox2.Controls.Add(Me.opt22K)
        Me.GroupBox2.Location = New System.Drawing.Point(276, 104)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(100, 24)
        Me.GroupBox2.TabIndex = 76
        Me.GroupBox2.TabStop = False
        '
        'opt24K
        '
        Me.opt24K.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.opt24K.ForeColor = System.Drawing.Color.Black
        Me.opt24K.Location = New System.Drawing.Point(41, 2)
        Me.opt24K.Name = "opt24K"
        Me.opt24K.Size = New System.Drawing.Size(63, 24)
        Me.opt24K.TabIndex = 76
        Me.opt24K.Text = "24K"
        '
        'opt22K
        '
        Me.opt22K.Checked = True
        Me.opt22K.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.opt22K.ForeColor = System.Drawing.Color.Black
        Me.opt22K.Location = New System.Drawing.Point(0, 2)
        Me.opt22K.Name = "opt22K"
        Me.opt22K.Size = New System.Drawing.Size(56, 24)
        Me.opt22K.TabIndex = 75
        Me.opt22K.TabStop = True
        Me.opt22K.Text = "22K"
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.Black
        Me.txtRemarks.Location = New System.Drawing.Point(104, 142)
        Me.txtRemarks.MaxLength = 20
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(361, 20)
        Me.txtRemarks.TabIndex = 17
        Me.txtRemarks.Tag = ""
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(8, 142)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(56, 15)
        Me.Label12.TabIndex = 73
        Me.Label12.Text = "Remarks"
        '
        'chkGST
        '
        Me.chkGST.ForeColor = System.Drawing.Color.Black
        Me.chkGST.Location = New System.Drawing.Point(224, 110)
        Me.chkGST.Name = "chkGST"
        Me.chkGST.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkGST.Size = New System.Drawing.Size(48, 16)
        Me.chkGST.TabIndex = 14
        Me.chkGST.Text = "GST"
        '
        'txtTEAL
        '
        Me.txtTEAL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTEAL.Enabled = False
        Me.txtTEAL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTEAL.ForeColor = System.Drawing.Color.Black
        Me.txtTEAL.Location = New System.Drawing.Point(548, 110)
        Me.txtTEAL.MaxLength = 20
        Me.txtTEAL.Name = "txtTEAL"
        Me.txtTEAL.Size = New System.Drawing.Size(60, 20)
        Me.txtTEAL.TabIndex = 15
        Me.txtTEAL.Tag = ""
        Me.txtTEAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(480, 113)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(40, 15)
        Me.Label11.TabIndex = 71
        Me.Label11.Text = "TEAL"
        '
        'txtCostPerGram
        '
        Me.txtCostPerGram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCostPerGram.Enabled = False
        Me.txtCostPerGram.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostPerGram.ForeColor = System.Drawing.Color.Black
        Me.txtCostPerGram.Location = New System.Drawing.Point(548, 133)
        Me.txtCostPerGram.MaxLength = 20
        Me.txtCostPerGram.Name = "txtCostPerGram"
        Me.txtCostPerGram.Size = New System.Drawing.Size(60, 20)
        Me.txtCostPerGram.TabIndex = 16
        Me.txtCostPerGram.Tag = ""
        Me.txtCostPerGram.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(480, 131)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(56, 26)
        Me.Label10.TabIndex = 69
        Me.Label10.Text = "Cost Per Gram"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(8, 109)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(56, 15)
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
        Me.optGold.Size = New System.Drawing.Size(62, 24)
        Me.optGold.TabIndex = 12
        Me.optGold.TabStop = True
        Me.optGold.Text = "Gold"
        '
        'optCash
        '
        Me.optCash.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCash.ForeColor = System.Drawing.Color.Black
        Me.optCash.Location = New System.Drawing.Point(74, 1)
        Me.optCash.Name = "optCash"
        Me.optCash.Size = New System.Drawing.Size(53, 24)
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
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(8, 82)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(64, 15)
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
        '
        'btnSupplierCodeSearch
        '
        Me.btnSupplierCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSupplierCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSupplierCodeSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSupplierCodeSearch.ForeColor = System.Drawing.Color.Black
        Me.btnSupplierCodeSearch.Location = New System.Drawing.Point(200, 52)
        Me.btnSupplierCodeSearch.Name = "btnSupplierCodeSearch"
        Me.btnSupplierCodeSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnSupplierCodeSearch.TabIndex = 7
        Me.btnSupplierCodeSearch.Text = "..."
        Me.btnSupplierCodeSearch.UseVisualStyleBackColor = False
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
        Me.btnJobSheetNoSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnJobSheetNoSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnJobSheetNoSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnJobSheetNoSearch.ForeColor = System.Drawing.Color.Black
        Me.btnJobSheetNoSearch.Location = New System.Drawing.Point(200, 24)
        Me.btnJobSheetNoSearch.Name = "btnJobSheetNoSearch"
        Me.btnJobSheetNoSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnJobSheetNoSearch.TabIndex = 1
        Me.btnJobSheetNoSearch.Text = "..."
        Me.btnJobSheetNoSearch.UseVisualStyleBackColor = False
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
        Me.txtJobsheetNo.TabIndex = 0
        Me.txtJobsheetNo.Tag = ""
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
        Me.txtCurrencyRate.Location = New System.Drawing.Point(512, 187)
        Me.txtCurrencyRate.MaxLength = 20
        Me.txtCurrencyRate.Name = "txtCurrencyRate"
        Me.txtCurrencyRate.Size = New System.Drawing.Size(101, 20)
        Me.txtCurrencyRate.TabIndex = 11
        Me.txtCurrencyRate.Tag = ""
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(876, 643)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 51
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.grpAddlDescription)
        Me.Panel1.Controls.Add(Me.TxtPcsInventoryCode)
        Me.Panel1.Controls.Add(Me.chkAdditionalDesc)
        Me.Panel1.Controls.Add(Me.btnCategoryCodeSearch)
        Me.Panel1.Controls.Add(Me.btnSizeCodeSearch)
        Me.Panel1.Controls.Add(Me.txtSizeCode)
        Me.Panel1.Controls.Add(Me.Label31)
        Me.Panel1.Controls.Add(Me.gbIPM)
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
        Me.Panel1.Controls.Add(Me.chkIPM)
        Me.Panel1.Location = New System.Drawing.Point(4, 432)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(972, 205)
        Me.Panel1.TabIndex = 52
        '
        'TxtPcsInventoryCode
        '
        Me.TxtPcsInventoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtPcsInventoryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPcsInventoryCode.ForeColor = System.Drawing.Color.Black
        Me.TxtPcsInventoryCode.Location = New System.Drawing.Point(1, 92)
        Me.TxtPcsInventoryCode.MaxLength = 20
        Me.TxtPcsInventoryCode.Name = "TxtPcsInventoryCode"
        Me.TxtPcsInventoryCode.Size = New System.Drawing.Size(120, 20)
        Me.TxtPcsInventoryCode.TabIndex = 522
        Me.TxtPcsInventoryCode.Tag = ""
        '
        'chkAdditionalDesc
        '
        Me.chkAdditionalDesc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAdditionalDesc.ForeColor = System.Drawing.Color.Black
        Me.chkAdditionalDesc.Location = New System.Drawing.Point(78, 122)
        Me.chkAdditionalDesc.Name = "chkAdditionalDesc"
        Me.chkAdditionalDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAdditionalDesc.Size = New System.Drawing.Size(112, 18)
        Me.chkAdditionalDesc.TabIndex = 521
        Me.chkAdditionalDesc.Text = "Addl. Desc"
        '
        'btnCategoryCodeSearch
        '
        Me.btnCategoryCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCategoryCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCategoryCodeSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCategoryCodeSearch.Location = New System.Drawing.Point(261, 28)
        Me.btnCategoryCodeSearch.Name = "btnCategoryCodeSearch"
        Me.btnCategoryCodeSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnCategoryCodeSearch.TabIndex = 520
        Me.btnCategoryCodeSearch.Text = "..."
        Me.btnCategoryCodeSearch.UseVisualStyleBackColor = False
        Me.btnCategoryCodeSearch.Visible = False
        '
        'btnSizeCodeSearch
        '
        Me.btnSizeCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSizeCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSizeCodeSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSizeCodeSearch.Location = New System.Drawing.Point(483, 72)
        Me.btnSizeCodeSearch.Name = "btnSizeCodeSearch"
        Me.btnSizeCodeSearch.Size = New System.Drawing.Size(22, 20)
        Me.btnSizeCodeSearch.TabIndex = 518
        Me.btnSizeCodeSearch.Text = "..."
        Me.btnSizeCodeSearch.UseVisualStyleBackColor = False
        '
        'txtSizeCode
        '
        Me.txtSizeCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSizeCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSizeCode.ForeColor = System.Drawing.Color.Black
        Me.txtSizeCode.Location = New System.Drawing.Point(433, 72)
        Me.txtSizeCode.MaxLength = 20
        Me.txtSizeCode.Name = "txtSizeCode"
        Me.txtSizeCode.Size = New System.Drawing.Size(63, 20)
        Me.txtSizeCode.TabIndex = 35
        Me.txtSizeCode.Tag = ""
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label31.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.Color.Black
        Me.Label31.Location = New System.Drawing.Point(433, 48)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(72, 24)
        Me.Label31.TabIndex = 519
        Me.Label31.Text = "Size"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grpAddlDescription
        '
        Me.grpAddlDescription.Controls.Add(Me.Label42)
        Me.grpAddlDescription.Controls.Add(Me.txtSellingPrice4)
        Me.grpAddlDescription.Controls.Add(Me.txtSellingPrice3)
        Me.grpAddlDescription.Controls.Add(Me.txtSellingPrice2)
        Me.grpAddlDescription.Controls.Add(Me.txtSellingPrice1)
        Me.grpAddlDescription.Controls.Add(Me.Label41)
        Me.grpAddlDescription.Controls.Add(Me.txtCost4)
        Me.grpAddlDescription.Controls.Add(Me.txtCost3)
        Me.grpAddlDescription.Controls.Add(Me.txtCost2)
        Me.grpAddlDescription.Controls.Add(Me.txtCost1)
        Me.grpAddlDescription.Controls.Add(Me.cmdAddlDescOk)
        Me.grpAddlDescription.Controls.Add(Me.Label37)
        Me.grpAddlDescription.Controls.Add(Me.txtDesc4)
        Me.grpAddlDescription.Controls.Add(Me.txtDesc3)
        Me.grpAddlDescription.Controls.Add(Me.txtDesc2)
        Me.grpAddlDescription.Controls.Add(Me.txtDesc1)
        Me.grpAddlDescription.Controls.Add(Me.Label40)
        Me.grpAddlDescription.Controls.Add(Me.Label39)
        Me.grpAddlDescription.Controls.Add(Me.Label38)
        Me.grpAddlDescription.Location = New System.Drawing.Point(184, 21)
        Me.grpAddlDescription.Name = "grpAddlDescription"
        Me.grpAddlDescription.Size = New System.Drawing.Size(445, 175)
        Me.grpAddlDescription.TabIndex = 85
        Me.grpAddlDescription.TabStop = False
        Me.grpAddlDescription.Visible = False
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
        'txtSellingPrice4
        '
        Me.txtSellingPrice4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSellingPrice4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSellingPrice4.ForeColor = System.Drawing.Color.Black
        Me.txtSellingPrice4.Location = New System.Drawing.Point(321, 117)
        Me.txtSellingPrice4.MaxLength = 50
        Me.txtSellingPrice4.Name = "txtSellingPrice4"
        Me.txtSellingPrice4.Size = New System.Drawing.Size(97, 20)
        Me.txtSellingPrice4.TabIndex = 44
        Me.txtSellingPrice4.Tag = ""
        '
        'txtSellingPrice3
        '
        Me.txtSellingPrice3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSellingPrice3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSellingPrice3.ForeColor = System.Drawing.Color.Black
        Me.txtSellingPrice3.Location = New System.Drawing.Point(321, 91)
        Me.txtSellingPrice3.MaxLength = 50
        Me.txtSellingPrice3.Name = "txtSellingPrice3"
        Me.txtSellingPrice3.Size = New System.Drawing.Size(98, 20)
        Me.txtSellingPrice3.TabIndex = 41
        Me.txtSellingPrice3.Tag = ""
        '
        'txtSellingPrice2
        '
        Me.txtSellingPrice2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSellingPrice2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSellingPrice2.ForeColor = System.Drawing.Color.Black
        Me.txtSellingPrice2.Location = New System.Drawing.Point(321, 65)
        Me.txtSellingPrice2.MaxLength = 50
        Me.txtSellingPrice2.Name = "txtSellingPrice2"
        Me.txtSellingPrice2.Size = New System.Drawing.Size(98, 20)
        Me.txtSellingPrice2.TabIndex = 38
        Me.txtSellingPrice2.Tag = ""
        '
        'txtSellingPrice1
        '
        Me.txtSellingPrice1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSellingPrice1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSellingPrice1.ForeColor = System.Drawing.Color.Black
        Me.txtSellingPrice1.Location = New System.Drawing.Point(321, 39)
        Me.txtSellingPrice1.MaxLength = 50
        Me.txtSellingPrice1.Name = "txtSellingPrice1"
        Me.txtSellingPrice1.Size = New System.Drawing.Size(98, 20)
        Me.txtSellingPrice1.TabIndex = 35
        Me.txtSellingPrice1.Tag = ""
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
        'txtCost4
        '
        Me.txtCost4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCost4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCost4.ForeColor = System.Drawing.Color.Black
        Me.txtCost4.Location = New System.Drawing.Point(211, 117)
        Me.txtCost4.MaxLength = 50
        Me.txtCost4.Name = "txtCost4"
        Me.txtCost4.Size = New System.Drawing.Size(97, 20)
        Me.txtCost4.TabIndex = 43
        Me.txtCost4.Tag = ""
        '
        'txtCost3
        '
        Me.txtCost3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCost3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCost3.ForeColor = System.Drawing.Color.Black
        Me.txtCost3.Location = New System.Drawing.Point(211, 91)
        Me.txtCost3.MaxLength = 50
        Me.txtCost3.Name = "txtCost3"
        Me.txtCost3.Size = New System.Drawing.Size(98, 20)
        Me.txtCost3.TabIndex = 40
        Me.txtCost3.Tag = ""
        '
        'txtCost2
        '
        Me.txtCost2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCost2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCost2.ForeColor = System.Drawing.Color.Black
        Me.txtCost2.Location = New System.Drawing.Point(211, 65)
        Me.txtCost2.MaxLength = 50
        Me.txtCost2.Name = "txtCost2"
        Me.txtCost2.Size = New System.Drawing.Size(98, 20)
        Me.txtCost2.TabIndex = 37
        Me.txtCost2.Tag = ""
        '
        'txtCost1
        '
        Me.txtCost1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCost1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCost1.ForeColor = System.Drawing.Color.Black
        Me.txtCost1.Location = New System.Drawing.Point(211, 39)
        Me.txtCost1.MaxLength = 50
        Me.txtCost1.Name = "txtCost1"
        Me.txtCost1.Size = New System.Drawing.Size(98, 20)
        Me.txtCost1.TabIndex = 34
        Me.txtCost1.Tag = ""
        '
        'cmdAddlDescOk
        '
        Me.cmdAddlDescOk.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdAddlDescOk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAddlDescOk.ForeColor = System.Drawing.Color.Black
        Me.cmdAddlDescOk.Location = New System.Drawing.Point(364, 140)
        Me.cmdAddlDescOk.Name = "cmdAddlDescOk"
        Me.cmdAddlDescOk.Size = New System.Drawing.Size(61, 23)
        Me.cmdAddlDescOk.TabIndex = 45
        Me.cmdAddlDescOk.Text = "OK"
        Me.cmdAddlDescOk.UseVisualStyleBackColor = False
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label37.Location = New System.Drawing.Point(12, 16)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(71, 13)
        Me.Label37.TabIndex = 515
        Me.Label37.Text = "Description"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDesc4
        '
        Me.txtDesc4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDesc4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc4.ForeColor = System.Drawing.Color.Black
        Me.txtDesc4.Location = New System.Drawing.Point(9, 116)
        Me.txtDesc4.MaxLength = 50
        Me.txtDesc4.Name = "txtDesc4"
        Me.txtDesc4.Size = New System.Drawing.Size(187, 20)
        Me.txtDesc4.TabIndex = 42
        Me.txtDesc4.Tag = ""
        '
        'txtDesc3
        '
        Me.txtDesc3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDesc3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc3.ForeColor = System.Drawing.Color.Black
        Me.txtDesc3.Location = New System.Drawing.Point(9, 90)
        Me.txtDesc3.MaxLength = 50
        Me.txtDesc3.Name = "txtDesc3"
        Me.txtDesc3.Size = New System.Drawing.Size(188, 20)
        Me.txtDesc3.TabIndex = 39
        Me.txtDesc3.Tag = ""
        '
        'txtDesc2
        '
        Me.txtDesc2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDesc2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc2.ForeColor = System.Drawing.Color.Black
        Me.txtDesc2.Location = New System.Drawing.Point(9, 64)
        Me.txtDesc2.MaxLength = 50
        Me.txtDesc2.Name = "txtDesc2"
        Me.txtDesc2.Size = New System.Drawing.Size(188, 20)
        Me.txtDesc2.TabIndex = 36
        Me.txtDesc2.Tag = ""
        '
        'txtDesc1
        '
        Me.txtDesc1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDesc1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc1.ForeColor = System.Drawing.Color.Black
        Me.txtDesc1.Location = New System.Drawing.Point(9, 38)
        Me.txtDesc1.MaxLength = 50
        Me.txtDesc1.Name = "txtDesc1"
        Me.txtDesc1.Size = New System.Drawing.Size(188, 20)
        Me.txtDesc1.TabIndex = 33
        Me.txtDesc1.Tag = ""
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label40.Location = New System.Drawing.Point(19, 99)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(78, 13)
        Me.Label40.TabIndex = 518
        Me.Label40.Text = "Description4"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label40.Visible = False
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label39.Location = New System.Drawing.Point(20, 73)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(78, 13)
        Me.Label39.TabIndex = 517
        Me.Label39.Text = "Description3"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label39.Visible = False
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label38.Location = New System.Drawing.Point(20, 47)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(78, 13)
        Me.Label38.TabIndex = 516
        Me.Label38.Text = "Description2"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label38.Visible = False
        '
        'gbIPM
        '
        Me.gbIPM.Controls.Add(Me.Label43)
        Me.gbIPM.Controls.Add(Me.txtBarNo)
        Me.gbIPM.Controls.Add(Me.cboBarCoin)
        Me.gbIPM.Controls.Add(Me.cboPurity)
        Me.gbIPM.Controls.Add(Me.Label33)
        Me.gbIPM.Controls.Add(Me.cboRefiner)
        Me.gbIPM.Controls.Add(Me.lblRefiner)
        Me.gbIPM.Controls.Add(Me.cboCountry)
        Me.gbIPM.Controls.Add(Me.Label34)
        Me.gbIPM.Controls.Add(Me.cboType)
        Me.gbIPM.Controls.Add(Me.Label35)
        Me.gbIPM.Controls.Add(Me.cboGSTType)
        Me.gbIPM.Controls.Add(Me.Label36)
        Me.gbIPM.Location = New System.Drawing.Point(6, 136)
        Me.gbIPM.Name = "gbIPM"
        Me.gbIPM.Size = New System.Drawing.Size(784, 64)
        Me.gbIPM.TabIndex = 179
        Me.gbIPM.TabStop = False
        Me.gbIPM.Visible = False
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label43.Location = New System.Drawing.Point(6, 40)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(40, 13)
        Me.Label43.TabIndex = 517
        Me.Label43.Text = "BarNo"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBarNo
        '
        Me.txtBarNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBarNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBarNo.ForeColor = System.Drawing.Color.Black
        Me.txtBarNo.Location = New System.Drawing.Point(48, 40)
        Me.txtBarNo.MaxLength = 20
        Me.txtBarNo.Name = "txtBarNo"
        Me.txtBarNo.Size = New System.Drawing.Size(120, 20)
        Me.txtBarNo.TabIndex = 516
        Me.txtBarNo.Tag = ""
        '
        'cboBarCoin
        '
        Me.cboBarCoin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBarCoin.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboBarCoin.Items.AddRange(New Object() {"Bar", "Coin"})
        Me.cboBarCoin.Location = New System.Drawing.Point(72, 10)
        Me.cboBarCoin.Name = "cboBarCoin"
        Me.cboBarCoin.Size = New System.Drawing.Size(96, 21)
        Me.cboBarCoin.TabIndex = 515
        Me.cboBarCoin.Visible = False
        '
        'cboPurity
        '
        Me.cboPurity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPurity.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPurity.Items.AddRange(New Object() {"99.9%", "99.5%", "99%"})
        Me.cboPurity.Location = New System.Drawing.Point(233, 8)
        Me.cboPurity.Name = "cboPurity"
        Me.cboPurity.Size = New System.Drawing.Size(104, 21)
        Me.cboPurity.TabIndex = 513
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label33.Location = New System.Drawing.Point(169, 8)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(41, 13)
        Me.Label33.TabIndex = 514
        Me.Label33.Text = "Purity"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboRefiner
        '
        Me.cboRefiner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRefiner.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRefiner.Location = New System.Drawing.Point(504, 8)
        Me.cboRefiner.Name = "cboRefiner"
        Me.cboRefiner.Size = New System.Drawing.Size(264, 21)
        Me.cboRefiner.TabIndex = 509
        '
        'lblRefiner
        '
        Me.lblRefiner.AutoSize = True
        Me.lblRefiner.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRefiner.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblRefiner.Location = New System.Drawing.Point(404, 8)
        Me.lblRefiner.Name = "lblRefiner"
        Me.lblRefiner.Size = New System.Drawing.Size(97, 13)
        Me.lblRefiner.TabIndex = 512
        Me.lblRefiner.Text = "Name of Refiner"
        Me.lblRefiner.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboCountry
        '
        Me.cboCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCountry.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCountry.Location = New System.Drawing.Point(504, 40)
        Me.cboCountry.Name = "cboCountry"
        Me.cboCountry.Size = New System.Drawing.Size(264, 21)
        Me.cboCountry.TabIndex = 510
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label34.Location = New System.Drawing.Point(404, 40)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(102, 13)
        Me.Label34.TabIndex = 511
        Me.Label34.Text = "Country of Origin"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboType
        '
        Me.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboType.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboType.Items.AddRange(New Object() {"Gold", "Silver", "Platinum", "Coin"})
        Me.cboType.Location = New System.Drawing.Point(47, 8)
        Me.cboType.Name = "cboType"
        Me.cboType.Size = New System.Drawing.Size(125, 21)
        Me.cboType.TabIndex = 505
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label35.Location = New System.Drawing.Point(7, 8)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(35, 13)
        Me.Label35.TabIndex = 508
        Me.Label35.Text = "Type"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboGSTType
        '
        Me.cboGSTType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboGSTType.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboGSTType.Items.AddRange(New Object() {"Exempt Supply", "Standard Rated Supply", "No GST"})
        Me.cboGSTType.Location = New System.Drawing.Point(233, 40)
        Me.cboGSTType.Name = "cboGSTType"
        Me.cboGSTType.Size = New System.Drawing.Size(168, 21)
        Me.cboGSTType.TabIndex = 506
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label36.Location = New System.Drawing.Point(169, 40)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(60, 13)
        Me.Label36.TabIndex = 507
        Me.Label36.Text = "GST Type"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblAutoManual
        '
        Me.lblAutoManual.BackColor = System.Drawing.Color.Green
        Me.lblAutoManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAutoManual.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAutoManual.ForeColor = System.Drawing.Color.Black
        Me.lblAutoManual.Location = New System.Drawing.Point(840, 152)
        Me.lblAutoManual.Name = "lblAutoManual"
        Me.lblAutoManual.Size = New System.Drawing.Size(104, 23)
        Me.lblAutoManual.TabIndex = 48
        Me.lblAutoManual.Text = "MANUAL (F10)"
        Me.lblAutoManual.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnAdd
        '
        Me.btnAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnAdd.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.ForeColor = System.Drawing.Color.Black
        Me.btnAdd.Location = New System.Drawing.Point(812, 96)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 44
        Me.btnAdd.Text = "Add "
        Me.btnAdd.UseVisualStyleBackColor = False
        '
        'btnBarCode
        '
        Me.btnBarCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnBarCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBarCode.Location = New System.Drawing.Point(892, 120)
        Me.btnBarCode.Name = "btnBarCode"
        Me.btnBarCode.Size = New System.Drawing.Size(75, 23)
        Me.btnBarCode.TabIndex = 47
        Me.btnBarCode.Text = "BarCode"
        Me.btnBarCode.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(892, 96)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 45
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.Location = New System.Drawing.Point(812, 120)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 46
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = False
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
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
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
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
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
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
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
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
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
        Me.txtStoneWeight.Location = New System.Drawing.Point(361, 92)
        Me.txtStoneWeight.MaxLength = 20
        Me.txtStoneWeight.Name = "txtStoneWeight"
        Me.txtStoneWeight.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtStoneWeight.Size = New System.Drawing.Size(72, 20)
        Me.txtStoneWeight.TabIndex = 355
        Me.txtStoneWeight.Tag = ""
        '
        'txtInventoryDiamondDescription
        '
        Me.txtInventoryDiamondDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInventoryDiamondDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInventoryDiamondDescription.ForeColor = System.Drawing.Color.Black
        Me.txtInventoryDiamondDescription.Location = New System.Drawing.Point(121, 92)
        Me.txtInventoryDiamondDescription.MaxLength = 50
        Me.txtInventoryDiamondDescription.Name = "txtInventoryDiamondDescription"
        Me.txtInventoryDiamondDescription.Size = New System.Drawing.Size(240, 20)
        Me.txtInventoryDiamondDescription.TabIndex = 33
        Me.txtInventoryDiamondDescription.Tag = ""
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
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
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
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
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
        Me.txtWeight.Location = New System.Drawing.Point(361, 72)
        Me.txtWeight.MaxLength = 20
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtWeight.Size = New System.Drawing.Size(72, 20)
        Me.txtWeight.TabIndex = 34
        Me.txtWeight.Tag = ""
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.Black
        Me.Label26.Location = New System.Drawing.Point(361, 48)
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
        Me.txtInventoryDescription.Size = New System.Drawing.Size(240, 20)
        Me.txtInventoryDescription.TabIndex = 32
        Me.txtInventoryDescription.Tag = ""
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.Black
        Me.Label23.Location = New System.Drawing.Point(121, 48)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(240, 24)
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
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
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
        Me.Panel3.Location = New System.Drawing.Point(786, 4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(183, 44)
        Me.Panel3.TabIndex = 131
        '
        'chkAutoSMC
        '
        Me.chkAutoSMC.Checked = True
        Me.chkAutoSMC.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAutoSMC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAutoSMC.ForeColor = System.Drawing.Color.Black
        Me.chkAutoSMC.Location = New System.Drawing.Point(55, 0)
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
        Me.Panel4.Size = New System.Drawing.Size(138, 44)
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
        Me.optPMCPCS.Location = New System.Drawing.Point(71, 18)
        Me.optPMCPCS.Name = "optPMCPCS"
        Me.optPMCPCS.Size = New System.Drawing.Size(72, 24)
        Me.optPMCPCS.TabIndex = 28
        Me.optPMCPCS.TabStop = True
        Me.optPMCPCS.Text = "Per PCS"
        '
        'optPMCGram
        '
        Me.optPMCGram.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPMCGram.Location = New System.Drawing.Point(0, 18)
        Me.optPMCGram.Name = "optPMCGram"
        Me.optPMCGram.Size = New System.Drawing.Size(82, 24)
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
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
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
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
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
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
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
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
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
        Me.btnTrayIdSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnTrayIdSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTrayIdSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTrayIdSearch.Location = New System.Drawing.Point(337, 28)
        Me.btnTrayIdSearch.Name = "btnTrayIdSearch"
        Me.btnTrayIdSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnTrayIdSearch.TabIndex = 22
        Me.btnTrayIdSearch.Text = "..."
        Me.btnTrayIdSearch.UseVisualStyleBackColor = False
        '
        'txtTrayId
        '
        Me.txtTrayId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTrayId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTrayId.ForeColor = System.Drawing.Color.Black
        Me.txtTrayId.Location = New System.Drawing.Point(284, 28)
        Me.txtTrayId.MaxLength = 20
        Me.txtTrayId.Name = "txtTrayId"
        Me.txtTrayId.Size = New System.Drawing.Size(53, 20)
        Me.txtTrayId.TabIndex = 21
        Me.txtTrayId.Tag = ""
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(284, 4)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(77, 24)
        Me.Label15.TabIndex = 119
        Me.Label15.Text = "Tray Id"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCategoryCode
        '
        Me.txtCategoryCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtCategoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCategoryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCategoryCode.ForeColor = System.Drawing.Color.Black
        Me.txtCategoryCode.Location = New System.Drawing.Point(121, 28)
        Me.txtCategoryCode.MaxLength = 20
        Me.txtCategoryCode.Name = "txtCategoryCode"
        Me.txtCategoryCode.ReadOnly = True
        Me.txtCategoryCode.Size = New System.Drawing.Size(164, 20)
        Me.txtCategoryCode.TabIndex = 18
        Me.txtCategoryCode.Tag = ""
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(121, 4)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(169, 24)
        Me.Label14.TabIndex = 117
        Me.Label14.Text = "Category Code"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnCatalogueCodeSearch
        '
        Me.btnCatalogueCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCatalogueCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCatalogueCodeSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCatalogueCodeSearch.Location = New System.Drawing.Point(97, 28)
        Me.btnCatalogueCodeSearch.Name = "btnCatalogueCodeSearch"
        Me.btnCatalogueCodeSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnCatalogueCodeSearch.TabIndex = 19
        Me.btnCatalogueCodeSearch.Text = "..."
        Me.btnCatalogueCodeSearch.UseVisualStyleBackColor = False
        '
        'txtCatalogueCode
        '
        Me.txtCatalogueCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCatalogueCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCatalogueCode.ForeColor = System.Drawing.Color.Black
        Me.txtCatalogueCode.Location = New System.Drawing.Point(1, 28)
        Me.txtCatalogueCode.MaxLength = 20
        Me.txtCatalogueCode.Name = "txtCatalogueCode"
        Me.txtCatalogueCode.Size = New System.Drawing.Size(120, 20)
        Me.txtCatalogueCode.TabIndex = 19
        Me.txtCatalogueCode.Tag = ""
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
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
        'chkIPM
        '
        Me.chkIPM.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIPM.ForeColor = System.Drawing.Color.Black
        Me.chkIPM.Location = New System.Drawing.Point(8, 124)
        Me.chkIPM.Name = "chkIPM"
        Me.chkIPM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkIPM.Size = New System.Drawing.Size(112, 14)
        Me.chkIPM.TabIndex = 516
        Me.chkIPM.Text = "IPM"
        '
        'btnBillClose
        '
        Me.btnBillClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnBillClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBillClose.Location = New System.Drawing.Point(760, 643)
        Me.btnBillClose.Name = "btnBillClose"
        Me.btnBillClose.Size = New System.Drawing.Size(110, 40)
        Me.btnBillClose.TabIndex = 49
        Me.btnBillClose.Text = "BillClose (F7)"
        Me.btnBillClose.UseVisualStyleBackColor = False
        '
        'btnBillClear
        '
        Me.btnBillClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnBillClear.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBillClear.Location = New System.Drawing.Point(654, 643)
        Me.btnBillClear.Name = "btnBillClear"
        Me.btnBillClear.Size = New System.Drawing.Size(100, 40)
        Me.btnBillClear.TabIndex = 50
        Me.btnBillClear.Text = "Clear (F6)"
        Me.btnBillClear.UseVisualStyleBackColor = False
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(5, 5)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 100)
        Me.pic.TabIndex = 84
        Me.pic.TabStop = False
        '
        'grdJobSheet
        '
        Me.grdJobSheet.AllowUserToAddRows = False
        Me.grdJobSheet.AllowUserToDeleteRows = False
        Me.grdJobSheet.AllowUserToResizeColumns = False
        Me.grdJobSheet.AllowUserToResizeRows = False
        Me.grdJobSheet.BackgroundColor = System.Drawing.Color.White
        Me.grdJobSheet.CausesValidation = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.NullValue = Nothing
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdJobSheet.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdJobSheet.ColumnHeadersHeight = 35
        Me.grdJobSheet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdJobSheet.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cCategoryJobsheet, Me.cBillWt, Me.cInvWt, Me.cBalance})
        Me.grdJobSheet.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdJobSheet.Location = New System.Drawing.Point(630, 108)
        Me.grdJobSheet.Name = "grdJobSheet"
        Me.grdJobSheet.RowHeadersVisible = False
        Me.grdJobSheet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdJobSheet.Size = New System.Drawing.Size(346, 167)
        Me.grdJobSheet.TabIndex = 86
        '
        'cCategoryJobsheet
        '
        Me.cCategoryJobsheet.HeaderText = "Category"
        Me.cCategoryJobsheet.Name = "cCategoryJobsheet"
        Me.cCategoryJobsheet.Width = 90
        '
        'cBillWt
        '
        Me.cBillWt.HeaderText = "Bill Weight"
        Me.cBillWt.Name = "cBillWt"
        Me.cBillWt.Width = 80
        '
        'cInvWt
        '
        Me.cInvWt.HeaderText = "Inventory Weight"
        Me.cInvWt.Name = "cInvWt"
        Me.cInvWt.Width = 80
        '
        'cBalance
        '
        Me.cBalance.HeaderText = "Balance"
        Me.cBalance.Name = "cBalance"
        Me.cBalance.Width = 80
        '
        'grdPurchase
        '
        Me.grdPurchase.AllowUserToAddRows = False
        Me.grdPurchase.AllowUserToDeleteRows = False
        Me.grdPurchase.AllowUserToResizeColumns = False
        Me.grdPurchase.AllowUserToResizeRows = False
        Me.grdPurchase.BackgroundColor = System.Drawing.Color.White
        Me.grdPurchase.CausesValidation = False
        Me.grdPurchase.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdPurchase.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdPurchase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdPurchase.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cItemCode, Me.cClass, Me.cDescription, Me.cWeight, Me.cNetWeight, Me.cSize, Me.cGoldLoss, Me.cCostPerGram, Me.cPMC, Me.cSMC, Me.cPCS, Me.cTotalCost, Me.cCatalogueCode, Me.cCategoryCode, Me.cStoneDescription, Me.cStoneWeight, Me.cMCType, Me.cSMCType, Me.cTrayID})
        Me.grdPurchase.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdPurchase.Location = New System.Drawing.Point(4, 281)
        Me.grdPurchase.Name = "grdPurchase"
        Me.grdPurchase.RowHeadersVisible = False
        Me.grdPurchase.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdPurchase.Size = New System.Drawing.Size(972, 150)
        Me.grdPurchase.TabIndex = 87
        '
        'cItemCode
        '
        Me.cItemCode.HeaderText = "ItemCode"
        Me.cItemCode.Name = "cItemCode"
        Me.cItemCode.Width = 80
        '
        'cClass
        '
        Me.cClass.HeaderText = "Class"
        Me.cClass.Name = "cClass"
        Me.cClass.Width = 80
        '
        'cDescription
        '
        Me.cDescription.HeaderText = "Description"
        Me.cDescription.Name = "cDescription"
        Me.cDescription.Width = 235
        '
        'cWeight
        '
        Me.cWeight.HeaderText = "Weight"
        Me.cWeight.Name = "cWeight"
        Me.cWeight.Width = 70
        '
        'cNetWeight
        '
        Me.cNetWeight.HeaderText = "Net weight"
        Me.cNetWeight.Name = "cNetWeight"
        Me.cNetWeight.Width = 70
        '
        'cSize
        '
        Me.cSize.HeaderText = "Size"
        Me.cSize.Name = "cSize"
        Me.cSize.Width = 70
        '
        'cGoldLoss
        '
        Me.cGoldLoss.HeaderText = "Gold Loss"
        Me.cGoldLoss.Name = "cGoldLoss"
        Me.cGoldLoss.Width = 70
        '
        'cCostPerGram
        '
        Me.cCostPerGram.HeaderText = "Cost Per Gram"
        Me.cCostPerGram.Name = "cCostPerGram"
        Me.cCostPerGram.Width = 70
        '
        'cPMC
        '
        Me.cPMC.HeaderText = "PMC"
        Me.cPMC.Name = "cPMC"
        Me.cPMC.Width = 70
        '
        'cSMC
        '
        Me.cSMC.HeaderText = "SMC"
        Me.cSMC.Name = "cSMC"
        Me.cSMC.Width = 70
        '
        'cPCS
        '
        Me.cPCS.HeaderText = "PCS"
        Me.cPCS.Name = "cPCS"
        Me.cPCS.Width = 70
        '
        'cTotalCost
        '
        Me.cTotalCost.HeaderText = "TotalCost"
        Me.cTotalCost.Name = "cTotalCost"
        Me.cTotalCost.Width = 80
        '
        'cCatalogueCode
        '
        Me.cCatalogueCode.HeaderText = "Catalogue Code"
        Me.cCatalogueCode.Name = "cCatalogueCode"
        Me.cCatalogueCode.Visible = False
        '
        'cCategoryCode
        '
        Me.cCategoryCode.HeaderText = "Category Code"
        Me.cCategoryCode.Name = "cCategoryCode"
        Me.cCategoryCode.Visible = False
        '
        'cStoneDescription
        '
        Me.cStoneDescription.HeaderText = "Stone Description"
        Me.cStoneDescription.Name = "cStoneDescription"
        Me.cStoneDescription.Visible = False
        '
        'cStoneWeight
        '
        Me.cStoneWeight.HeaderText = "Stone Weight"
        Me.cStoneWeight.Name = "cStoneWeight"
        Me.cStoneWeight.Visible = False
        '
        'cMCType
        '
        Me.cMCType.HeaderText = "MC Type"
        Me.cMCType.Name = "cMCType"
        Me.cMCType.Visible = False
        '
        'cSMCType
        '
        Me.cSMCType.HeaderText = "SMC Type"
        Me.cSMCType.Name = "cSMCType"
        Me.cSMCType.Visible = False
        '
        'cTrayID
        '
        Me.cTrayID.HeaderText = "Tray ID"
        Me.cTrayID.Name = "cTrayID"
        Me.cTrayID.Visible = False
        '
        'chkSalesPCS
        '
        Me.chkSalesPCS.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSalesPCS.ForeColor = System.Drawing.Color.Black
        Me.chkSalesPCS.Location = New System.Drawing.Point(917, 438)
        Me.chkSalesPCS.Name = "chkSalesPCS"
        Me.chkSalesPCS.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkSalesPCS.Size = New System.Drawing.Size(53, 16)
        Me.chkSalesPCS.TabIndex = 88
        Me.chkSalesPCS.Text = "PCS"
        '
        'chkImage
        '
        Me.chkImage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkImage.ForeColor = System.Drawing.Color.Black
        Me.chkImage.Location = New System.Drawing.Point(4, 655)
        Me.chkImage.Name = "chkImage"
        Me.chkImage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkImage.Size = New System.Drawing.Size(66, 18)
        Me.chkImage.TabIndex = 476262
        Me.chkImage.Text = "Image"
        '
        'grpImage
        '
        Me.grpImage.Controls.Add(Me.btnBrowseImage)
        Me.grpImage.Controls.Add(Me.picImage)
        Me.grpImage.Controls.Add(Me.Button1)
        Me.grpImage.Location = New System.Drawing.Point(146, 401)
        Me.grpImage.Name = "grpImage"
        Me.grpImage.Size = New System.Drawing.Size(266, 282)
        Me.grpImage.TabIndex = 476263
        Me.grpImage.TabStop = False
        Me.grpImage.Visible = False
        '
        'btnBrowseImage
        '
        Me.btnBrowseImage.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnBrowseImage.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowseImage.Location = New System.Drawing.Point(32, 237)
        Me.btnBrowseImage.Name = "btnBrowseImage"
        Me.btnBrowseImage.Size = New System.Drawing.Size(58, 25)
        Me.btnBrowseImage.TabIndex = 771
        Me.btnBrowseImage.Text = "Browse"
        Me.btnBrowseImage.UseVisualStyleBackColor = False
        '
        'picImage
        '
        Me.picImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picImage.Location = New System.Drawing.Point(32, 15)
        Me.picImage.Name = "picImage"
        Me.picImage.Size = New System.Drawing.Size(206, 217)
        Me.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picImage.TabIndex = 770
        Me.picImage.TabStop = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Button1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.Black
        Me.Button1.Location = New System.Drawing.Point(177, 237)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(61, 25)
        Me.Button1.TabIndex = 45
        Me.Button1.Text = "OK"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'frmPurchase
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(978, 695)
        Me.Controls.Add(Me.grpImage)
        Me.Controls.Add(Me.chkImage)
        Me.Controls.Add(Me.chkSalesPCS)
        Me.Controls.Add(Me.grdPurchase)
        Me.Controls.Add(Me.grdJobSheet)
        Me.Controls.Add(Me.txtCurrencyRate)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.btnBillClear)
        Me.Controls.Add(Me.btnBillClose)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmPurchase"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "JobSheet Purchase "
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.grpAddlDescription.ResumeLayout(False)
        Me.grpAddlDescription.PerformLayout()
        Me.gbIPM.ResumeLayout(False)
        Me.gbIPM.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdJobSheet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdPurchase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpImage.ResumeLayout(False)
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmPurchase_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'subGridSetting(grdJobSheet)
        'subGridSetting(grdPurchase)

        '''subFillPriceCodeArr()
        subLockAllFlds(False)
        subClear()
        subClearDetail(False)
        subClearCatalogue()
        If UCase(sCompanyShortCode) = "MJ" Then
            chkAutoSMC.Checked = False
        Else
            chkAutoSMC.Checked = False
        End If
        lblAutoManual.Text = "MANUAL (F10)"
        gsBarCodeFilePath = Application.StartupPath & "\" & gsBarCodeFile & ".txt"
        If Trim(sCompanyShortCode) = "AJ" Then
            txtTrayId.Text = "DEF"
            txtGoldLoss.Text = 0
        End If
        If cboTealPercent1.Items.Count > 0 Then cboTealPercent1.SelectedIndex = 1
    End Sub

    Private Sub subClearCatalogue()
        Try
            txtCatalogueCode.Clear()
            '''  txtCategoryCode.Clear()
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
            If sCompanyShortCode <> "ES" Then
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
            sSizeType = ""
            txtBarNo.Text = ""
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
            picImage.Name = ""
            picImage.Image = Nothing
            sCreateInventory = ""
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
            opt22K.Checked = True
            txtCostPerGram.Clear()
            txtRemarks.Clear()

            'txtGst.text = ""
            'txtManualCost.text = ""
            'txtManualMC.text = ""
            'txtManualWeight.text = ""
            'txtManualGoldLoss.text = ""

            grdPurchase.RowCount = 0
            grdJobSheet.RowCount = 0

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
                ' SubAddTotalRow()
                If txtCatalogueCode.CanFocus Then txtCatalogueCode.Focus()
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
    Public Function SubAddTotalRow()
        'If Not bEdited Then
        grdPurchase.Rows.Add()
        'End If

        Call fnSetdgvText(grdPurchase, grdPurchase.RowCount - 1, 0, "TOTAL:")

        Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(0).Style.ForeColor = Color.Red
        Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(0).Style.Font = New Font(grdPurchase.Font.FontFamily, 9, FontStyle.Bold)
    End Function
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

                ''HAJ TEAL 22k And 24K Calculation on 25 Sep 2007
                'If Val(cboTealPercent1.Text) <> 0 Then
                '    If chkGST.Checked = True And opt22K.Checked = True Then
                '        txtTEAL.Text = Format((((Val(txtCostPerGram.Text) / Val(GstTaelPercentage)) / Val(cboTealPercent1.Text)) / 0.02646), "0")
                '    ElseIf chkGST.Checked = True And opt24K.Checked = True Then
                '        txtTEAL.Text = Format((((Val(txtCostPerGram.Text) / Val(GstTaelPercentage))) / 0.02646), "0")
                '    ElseIf chkGST.Checked = False And opt24K.Checked = True Then
                '        txtTEAL.Text = Format((((Val(txtCostPerGram.Text))) / 0.02646), "0")
                '    Else
                '        txtTEAL.Text = Format((((Val(txtCostPerGram.Text) / Val(cboTealPercent1.Text))) / 0.02646), "0")
                '    End If
                'Else
                '    If chkGST.Checked = True And opt22K.Checked = True Then
                '        txtTEAL.Text = Format((((Val(txtCostPerGram.Text) / Val(GstTaelPercentage)) / 0.92) / 0.02646), "0")
                '    ElseIf chkGST.Checked = True And opt24K.Checked = True Then
                '        txtTEAL.Text = Format((((Val(txtCostPerGram.Text) / Val(GstTaelPercentage))) / 0.02646), "0")
                '    ElseIf chkGST.Checked = False And opt24K.Checked = True Then
                '        txtTEAL.Text = Format((((Val(txtCostPerGram.Text))) / 0.02646), "0")
                '    Else
                '        txtTEAL.Text = Format((((Val(txtCostPerGram.Text) / 0.92)) / 0.02646), "0")
                '    End If
                'End If

                txtTEAL.Text = Val(Ds.Tables("JobSheet").Rows(0)("TaelRate") & "")
                cboTealPercent.Text = Val(Ds.Tables("JobSheet").Rows(0)("TaelPercent") & "")

                If Trim(Ds.Tables("JobSheet").Rows(0)("ClassType") & "") = "22K" Then
                    opt22K.Checked = True
                ElseIf Trim(Ds.Tables("JobSheet").Rows(0)("ClassType") & "") = "24K" Then
                    opt24K.Checked = True
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
                If Trim(sCompanyShortCode) = "AJ" Then
                    txtTrayId.Text = "DEF"
                    txtGoldLoss.Text = 0
                End If
                '' If txtCatalogueCode.Enabled Then txtCatalogueCode.Focus()
                If txtCategoryCode.Enabled Then txtCategoryCode.Focus()
            Else
                MsgBox("Job sheet no. is invalid!", MsgBoxStyle.Critical)
                btnBillClear_Click(sender, e)
                Exit Sub
            End If

            subGetJobSheetDetails()
            subGetPurchaseDetails()
            ''txtCatalogueCode.Focus()
            If txtCategoryCode.Enabled Then txtCategoryCode.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subGetPurchaseDetails()
        Dim i As Long
        Dim Ds As DataSet
        Dim drPurchase() As DataRow
        Dim dm As New DataManager
        Dim objInventory As New clsInventory
        Try

            Ds = objInventory.GetInventorybyJobSheet(txtJobsheetNo.Text.Trim)

            grdPurchase.RowCount = 0
            For i = 0 To Ds.Tables("tblInventory").Rows.Count - 1
                grdPurchase.Rows.Add()
                ''grdPurchase.Row = grdPurchase.MaxRows
                Call fnSetdgvText(grdPurchase, i, 0, Trim(Ds.Tables("tblInventory").Rows(i)("Inventorycode") & ""))
                Call fnSetdgvText(grdPurchase, i, 1, Trim(Ds.Tables("tblInventory").Rows(i)("ClassCode") & ""))
                Call fnSetdgvText(grdPurchase, i, 2, Trim(Ds.Tables("tblInventory").Rows(i)("Description") & ""))
                Call fnSetdgvText(grdPurchase, i, 5, Trim(Ds.Tables("tblInventory").Rows(i)("SizeCode") & ""))
                If Trim(Ds.Tables("tblInventory").Rows(i)("SalesBillNo") & "") <> "" Then
                    drPurchase = dm.RowReturnProjExecNonQryWithDataRow("Select Weight from tblPurchase Where InventoryCode = " & fncFormatSQLText(Trim(Ds.Tables("tblInventory").Rows(i)("Inventorycode") & "") & " and JobSheetNo = " & fncFormatSQLText(txtJobsheetNo.Text.Trim)))
                    If drPurchase.Length > 0 Then
                        Call fnSetdgvText(grdPurchase, i, 3, Format(Val(drPurchase(0)("Weight") & ""), "#0.00"))
                    Else
                        Call fnSetdgvText(grdPurchase, i, 3, Format(Val(Ds.Tables("tblInventory").Rows(i)("Weight") & ""), "#0.00"))
                    End If
                Else
                    Call fnSetdgvText(grdPurchase, i, 3, Format(Val(Ds.Tables("tblInventory").Rows(i)("Weight") & ""), "#0.00"))
                End If
                drPurchase = Nothing
                Call fnSetdgvText(grdPurchase, i, 4, Format(Val(Ds.Tables("tblInventory").Rows(i)("Weight") & "") - Val(Ds.Tables("tblInventory").Rows(i)("StoneWeight") & ""), "#0.00"))
                Call fnSetdgvText(grdPurchase, i, 6, Format(Val(Ds.Tables("tblInventory").Rows(i)("GoldLoss") & ""), "#0.00"))

                Call fnSetdgvText(grdPurchase, i, 7, Format(Val(Ds.Tables("tblInventory").Rows(i)("CostperGram") & ""), "#0.00"))
                Call fnSetdgvText(grdPurchase, i, 8, Format(Val(Ds.Tables("tblInventory").Rows(i)("PMC")), "#0.00"))
                Call fnSetdgvText(grdPurchase, i, 9, Format(Val(Ds.Tables("tblInventory").Rows(i)("sMc")), "#0.00"))
                Call fnSetdgvText(grdPurchase, i, 10, Format(Val(Ds.Tables("tblInventory").Rows(i)("Pcs") & ""), "#0"))
                Call fnSetdgvText(grdPurchase, i, 11, Format(Val(Ds.Tables("tblInventory").Rows(i)("TotalCost") & ""), "#0.00"))
                Call fnSetdgvText(grdPurchase, i, 12, Trim$(Ds.Tables("tblInventory").Rows(i)("CatalogueCode") & ""))
                Call fnSetdgvText(grdPurchase, i, 13, Trim$(Ds.Tables("tblInventory").Rows(i)("CategoryCode") & ""))
                Call fnSetdgvText(grdPurchase, i, 14, Trim$(Ds.Tables("tblInventory").Rows(i)("DiamondDescription") & ""))
                Call fnSetdgvText(grdPurchase, i, 15, Trim$(Ds.Tables("tblInventory").Rows(i)("StoneWeight") & ""))
                Call fnSetdgvText(grdPurchase, i, 16, Trim$(Ds.Tables("tblInventory").Rows(i)("PMCType") & ""))
                Call fnSetdgvText(grdPurchase, i, 17, Trim$(Ds.Tables("tblInventory").Rows(i)("SMCType") & ""))
                Call fnSetdgvText(grdPurchase, i, 18, Trim$(Ds.Tables("tblInventory").Rows(i)("TrayID") & ""))
            Next i

            'grdPurchase.Row = grdPurchase.MaxRows
            'grdPurchase.Col = 1
            'grdPurchase.Action = 0
            subTotalFromGrid()
            subTotal()
            Ds = Nothing
            objInventory = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        Finally
            drPurchase = Nothing
            dm = Nothing
        End Try
    End Sub
    Private Sub subTotal()
        Try
            If grdPurchase.RowCount = 0 Then
                Exit Sub
            End If
            If Not bEdited Then
                'grdPurchase.MaxRows = grdPurchase.MaxRows + 1
                grdPurchase.Rows.Add()
            End If



            Call fnSetdgvText(grdPurchase, grdPurchase.RowCount - 1, 0, "TOTAL:")
            Call fnSetdgvText(grdPurchase, grdPurchase.RowCount - 1, 3, Format(dWeightGrd, "0.00"))
            Call fnSetdgvText(grdPurchase, grdPurchase.RowCount - 1, 4, Format(dNetWeightGrd, "0.00"))
            Call fnSetdgvText(grdPurchase, grdPurchase.RowCount - 1, 8, Format(dPMCGrd, "0.00"))
            Call fnSetdgvText(grdPurchase, grdPurchase.RowCount - 1, 9, Format(dSMCGrd, "0.00"))
            Call fnSetdgvText(grdPurchase, grdPurchase.RowCount - 1, 10, Format(dPCSGrd, "0.00"))
            Call fnSetdgvText(grdPurchase, grdPurchase.RowCount - 1, 11, Format(dTotalCostGrd, "0.00"))



            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(0).Style.ForeColor = Color.Red
            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(0).Style.Font = New Font(grdPurchase.Font.FontFamily, 9, FontStyle.Bold)

            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(3).Style.ForeColor = Color.Red
            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(3).Style.Font = New Font(grdPurchase.Font.FontFamily, 9, FontStyle.Bold)

            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(4).Style.ForeColor = Color.Red
            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(4).Style.Font = New Font(grdPurchase.Font.FontFamily, 9, FontStyle.Bold)

            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(6).Style.ForeColor = Color.Red
            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(6).Style.Font = New Font(grdPurchase.Font.FontFamily, 9, FontStyle.Bold)

            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(7).Style.ForeColor = Color.Red
            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(7).Style.Font = New Font(grdPurchase.Font.FontFamily, 9, FontStyle.Bold)

            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(8).Style.ForeColor = Color.Red
            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(8).Style.Font = New Font(grdPurchase.Font.FontFamily, 9, FontStyle.Bold)

            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(9).Style.ForeColor = Color.Red
            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(9).Style.Font = New Font(grdPurchase.Font.FontFamily, 9, FontStyle.Bold)

            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(10).Style.ForeColor = Color.Red
            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(10).Style.Font = New Font(grdPurchase.Font.FontFamily, 9, FontStyle.Bold)

            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(11).Style.ForeColor = Color.Red
            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(11).Style.Font = New Font(grdPurchase.Font.FontFamily, 9, FontStyle.Bold)

            'grdPurchase.ForeColor = Color.Red
            'grdPurchase.FontBold = True

            'grdPurchase.Col = 4
            'grdPurchase.Formula = "sum(d1:d" & grdPurchase.MaxRows - 1 & ")"


            'grdPurchase.ForeColor = Color.Red
            'grdPurchase.FontBold = True

            'grdPurchase.Col = 6
            'grdPurchase.Formula = "sum(f1:f" & grdPurchase.MaxRows - 1 & ")"

            'grdPurchase.ForeColor = Color.Red
            'grdPurchase.FontBold = True

            'grdPurchase.Col = 8
            'grdPurchase.Formula = "sum(h1:h" & grdPurchase.MaxRows - 1 & ")"

            'grdPurchase.ForeColor = Color.Red
            'grdPurchase.FontBold = True

            'grdPurchase.Col = 9
            'grdPurchase.Formula = "sum(i1:i" & grdPurchase.MaxRows - 1 & ")"

            'grdPurchase.ForeColor = Color.Red
            'grdPurchase.FontBold = True

            'grdPurchase.Col = 10
            'grdPurchase.Formula = "sum(j1:j" & grdPurchase.MaxRows - 1 & ")"
            'grdPurchase.ForeColor = Color.Red
            'grdPurchase.FontBold = True

            'grdPurchase.Col = 11
            'grdPurchase.Formula = "sum(k1:k" & grdPurchase.MaxRows - 1 & ")"

            'grdPurchase.ForeColor = Color.Red
            'grdPurchase.FontBold = True

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
            grdJobSheet.RowCount = 0
            For i = 0 To DsJS.Tables("JobSheet").Rows.Count - 1
                grdJobSheet.Rows.Add()
                Call fnSetdgvText(grdJobSheet, i, 0, Trim(DsJS.Tables("JobSheet").Rows(i)("CategoryCode") & ""))
                Call fnSetdgvText(grdJobSheet, i, 1, Format(Val(DsJS.Tables("JobSheet").Rows(i)("BillWT") & ""), "#0.00"))


                DsIn = objInventory.GetInventoryWeight(txtJobsheetNo.Text.Trim, Trim(DsJS.Tables("JobSheet").Rows(i)("CategoryCode") & ""))
                If DsIn.Tables("Inventory").Rows.Count > 0 Then
                    Call fnSetdgvText(grdJobSheet, i, 2, Format(Val(DsIn.Tables("Inventory").Rows(0)("BillWT") & ""), "#0.00"))
                    Call fnSetdgvText(grdJobSheet, i, 3, Format(Val((DsJS.Tables("JobSheet").Rows(i)("BillWT") & "") - Val(DsIn.Tables("Inventory").Rows(0)("BillWT") & "")), "#0.00"))
                Else
                    Call fnSetdgvText(grdJobSheet, i, 2, Format(Val("0"), "#0.00"))
                    Call fnSetdgvText(grdJobSheet, i, 3, Format(Val((DsJS.Tables("JobSheet").Rows(i)("BillWT") & "") - Val("0")), "#0.00"))
                End If
                DsIn = Nothing
            Next i

            'grdJobSheet.Row = grdJobSheet.MaxRows
            'grdPurchase.Col = 1
            'grdPurchase.Action = 0
            DsJS = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try

    End Sub
    Public Function subTotalFromGrid()
        Dim icnt
        Dim sinventorycode As String
        dWeightGrd = 0
        dNetWeightGrd = 0
        dPMCGrd = 0
        dSMCGrd = 0
        dPCSGrd = 0
        dTotalCostGrd = 0
        For icnt = 0 To grdPurchase.RowCount - 1
            sinventorycode = Trim(fnGetdgvText(grdPurchase, icnt, 0))
            If sinventorycode <> "TOTAL:" Then
                dWeightGrd = Format(Val(dWeightGrd) + Val(fnGetdgvText(grdPurchase, icnt, 3)), "0.00")
                dNetWeightGrd = Format(Val(dNetWeightGrd) + Val(fnGetdgvText(grdPurchase, icnt, 4)), "0.00")
                'dPMCGrd = Format(Val(dPMCGrd) + Val(fnGetdgvText(grdPurchase, icnt, 7)), "0.00")
                'dSMCGrd = Format(Val(dSMCGrd) + Val(fnGetdgvText(grdPurchase, icnt, 8)), "0.00")
                'dPCSGrd = Format(Val(dPCSGrd) + Val(fnGetdgvText(grdPurchase, icnt, 9)), "0.00")
                'dTotalCostGrd = Format(Val(dTotalCostGrd) + Val(fnGetdgvText(grdPurchase, icnt, 10)), "0.00")
                dPMCGrd = Format(Val(dPMCGrd) + Val(fnGetdgvText(grdPurchase, icnt, 8)), "0.00")
                dSMCGrd = Format(Val(dSMCGrd) + Val(fnGetdgvText(grdPurchase, icnt, 9)), "0.00")
                dPCSGrd = Format(Val(dPCSGrd) + Val(fnGetdgvText(grdPurchase, icnt, 10)), "0.00")
                dTotalCostGrd = Format(Val(dTotalCostGrd) + Val(fnGetdgvText(grdPurchase, icnt, 11)), "0.00")
            End If
        Next
    End Function

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
            '''   If txtSupplierCode.Text.Trim <> "" Then If txtJobsheetNo.Text.Trim <> "" Then txtCatalogueCode.Focus()
            If txtSupplierCode.Text.Trim <> "" Then If txtJobsheetNo.Text.Trim <> "" Then txtCategoryCode.Focus()
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
        Dim r As Integer
        Try
            If Not bEdited Then
                If grdPurchase.RowCount = 0 Then
                    grdPurchase.Rows.Add()
                Else
                    'grdJobSheet.Row = grdJobSheet.MaxRows
                    'grdJobSheet.Action = 5
                    'grdJobSheet.MaxRows = grdJobSheet.MaxRows - 1
                    'grdJobSheet.MaxRows = grdJobSheet.MaxRows + 1
                End If
                r = grdPurchase.RowCount - 1
            Else
                r = grdPurchase.CurrentRow.Index
            End If

            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(0).Style.ForeColor = Color.Black
            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(0).Style.Font = New Font(grdPurchase.Font.FontFamily, 8, FontStyle.Regular)

            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(3).Style.ForeColor = Color.Black
            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(3).Style.Font = New Font(grdPurchase.Font.FontFamily, 8, FontStyle.Regular)

            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(4).Style.ForeColor = Color.Black
            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(4).Style.Font = New Font(grdPurchase.Font.FontFamily, 8, FontStyle.Regular)

            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(6).Style.ForeColor = Color.Black
            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(6).Style.Font = New Font(grdPurchase.Font.FontFamily, 8, FontStyle.Regular)

            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(7).Style.ForeColor = Color.Black
            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(7).Style.Font = New Font(grdPurchase.Font.FontFamily, 8, FontStyle.Regular)

            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(8).Style.ForeColor = Color.Black
            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(8).Style.Font = New Font(grdPurchase.Font.FontFamily, 8, FontStyle.Regular)

            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(9).Style.ForeColor = Color.Black
            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(9).Style.Font = New Font(grdPurchase.Font.FontFamily, 8, FontStyle.Regular)

            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(10).Style.ForeColor = Color.Black
            Me.grdPurchase.Rows(grdPurchase.RowCount - 1).Cells(10).Style.Font = New Font(grdPurchase.Font.FontFamily, 8, FontStyle.Regular)



            Call fnSetdgvText(grdPurchase, r, 0, txtInventoryCode.Text.Trim)
            Call fnSetdgvText(grdPurchase, r, 1, Trim(sClassCode))
            Call fnSetdgvText(grdPurchase, r, 2, txtInventoryDescription.Text.Trim)
            Call fnSetdgvText(grdPurchase, r, 5, txtSizeCode.Text.Trim)
            Call fnSetdgvText(grdPurchase, r, 3, Format(Val(txtWeight.Text), "#0.00"))
            Call fnSetdgvText(grdPurchase, r, 4, Format(Val(txtWeight.Text.Trim) - Val(txtStoneWeight.Text.Trim), "#0.00"))
            Call fnSetdgvText(grdPurchase, r, 6, Format(Val(txtGoldLoss.Text), "#0.00"))
            Call fnSetdgvText(grdPurchase, r, 7, Format(Val(txtCostPerGramI.Text), "#0.00"))
            Call fnSetdgvText(grdPurchase, r, 8, Format(Val(txtPMCCost.Text), "#0.00"))
            Call fnSetdgvText(grdPurchase, r, 9, Format(Val(txtSMCCost.Text), "#0.00"))
            Call fnSetdgvText(grdPurchase, r, 10, Format(Val(txtPCS.Text), "#0"))
            Call fnSetdgvText(grdPurchase, r, 11, Format(Val(txtTotalCost.Text), "#0.00"))
            Call fnSetdgvText(grdPurchase, r, 12, txtCatalogueCode.Text.Trim)
            Call fnSetdgvText(grdPurchase, r, 13, txtCategoryCode.Text.Trim)
            Call fnSetdgvText(grdPurchase, r, 13, txtCategoryCode.Text.Trim)
            Call fnSetdgvText(grdPurchase, r, 14, txtInventoryDiamondDescription.Text.Trim)
            Call fnSetdgvText(grdPurchase, r, 15, Format(Val(txtStoneWeight.Text), "#0.00"))
            Call fnSetdgvText(grdPurchase, r, 16, IIf(optPMCPCS.Checked, "P", "W"))
            Call fnSetdgvText(grdPurchase, r, 17, IIf(optSMCPCS.Checked, "P", "W"))
            Call fnSetdgvText(grdPurchase, r, 18, txtTrayId.Text.Trim)

            'grdPurchase.Row = grdPurchase.MaxRows
            'grdPurchase.Col = 1
            'grdPurchase.Action = 0
            subTotalFromGrid()
            subTotal()
            txtInventoryCode.Tag = txtInventoryCode
            If sRFIDModule = "0" Then
                If UCase(sCompanyShortCode) = "AS" Then
                    Dim objfrm As New frmJewelBarcode
                    objfrm.txtInventoryCode.Text = txtInventoryCode.Text.Trim
                    objfrm.txtInventoryCode_LostFocus(sender, e)
                    objfrm.btnPrint_Click(sender, e)
                ElseIf UCase(sCompanyShortCode) = "AJ" Then
                    Dim objfrm As New frmBarcode
                    objfrm.txtInventoryCode.Text = txtInventoryCode.Text.Trim
                    objfrm.txtInventoryCode_LostFocus(sender, e)
                    objfrm.btnPrint_Click(sender, e)
                ElseIf UCase(sCompanyShortCode) = "HN" Or UCase(sCompanyShortCode) = "MJ" Or UCase(sCompanyShortCode) = "SSJ" Or UCase(sCompanyShortCode) = "RJ" Or UCase(sCompanyShortCode) = "MD" Then
                    Dim objfrm As New frmBarcode
                    objfrm.txtInventoryCode.Text = txtInventoryCode.Text.Trim
                    objfrm.txtInventoryCode_LostFocus(sender, e)
                    objfrm.btnPrint_Click(sender, e)
                ElseIf (UCase(sCompanyShortCode) = "ES") Or (UCase(sCompanyShortCode) = "ML") Then
                    If mspurchasetype = "W" Then
                        Dim objfrm As New frmBarcode
                        objfrm.txtInventoryCode.Text = txtInventoryCode.Text.Trim
                        objfrm.txtInventoryCode_LostFocus(sender, e)
                        objfrm.btnPrint_Click(sender, e)
                    ElseIf mspurchasetype = "P" Then
                        Dim objfrm As New frmJewelBarcode
                        objfrm.txtInventoryCode.Text = txtInventoryCode.Text.Trim
                        objfrm.txtInventoryCode_LostFocus(sender, e)
                        objfrm.btnPrint_Click(sender, e)
                    End If
                    subClearDetail(True)
                End If
            End If
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
        Dim sSalesPcs As Long
        Dim ByteArr() As Byte
        Dim objInventory As New clsInventory
        Dim objPurchase As New clsPurchase
        Try

            ReDim ByteArr(0)
            Dim ImageStream As New IO.MemoryStream
            If Not picImage.Image Is Nothing Then
                picImage.Image.Save(ImageStream, System.Drawing.Imaging.ImageFormat.Jpeg)
                ReDim ByteArr(CInt(ImageStream.Length - 1))
                ImageStream.Position = 0
                ImageStream.Read(ByteArr, 0, CInt(ImageStream.Length))
            Else
                ReDim ByteArr(0)
            End If

            bInventory = False
            sPMCType = IIf(optPMCPCS.Checked, "P", "W")
            sSMCType = IIf(optSMCPCS.Checked, "P", "W")

            If optGold.Checked Then
                sPayType = "G"
            Else
                sPayType = "C"
            End If

            If chkSalesPCS.Checked Then
                sSalesPcs = 1
            Else
                sSalesPcs = 0
            End If

            If Val(txtPCS.Text) = 0 Then
                txtPCS.Text = "1"
            End If

            If chkAdditionalDesc.Checked Then
                'SHabeer 20180312
                'If Val(txtCost1.Text) > 0 Then
                '    txtPMCCost.Text = Val(txtPMCCost.Text) + Val(txtCost1.Text)
                'End If
                'If Val(txtCost2.Text) > 0 Then
                '    txtPMCCost.Text = Val(txtPMCCost.Text) + Val(txtCost2.Text)
                'End If
                'If Val(txtCost3.Text) > 0 Then
                '    txtPMCCost.Text = Val(txtPMCCost.Text) + Val(txtCost3.Text)
                'End If
                'If Val(txtCost4.Text) > 0 Then
                '    txtPMCCost.Text = Val(txtPMCCost.Text) + Val(txtCost4.Text)
                'End If
                'SHabeer 20180312

                If Val(txtCost1.Text) > 0 Then
                    txtPMCCost.Text = Val(txtPMCCost.Text) + Val(txtCost1.Text)
                    txtTotalCost.Text = Val(txtTotalCost.Text) + Val(txtCost1.Text)
                End If
                If Val(txtCost2.Text) > 0 Then
                    txtPMCCost.Text = Val(txtPMCCost.Text) + Val(txtCost2.Text)
                    txtTotalCost.Text = Val(txtTotalCost.Text) + Val(txtCost2.Text)
                End If
                If Val(txtCost3.Text) > 0 Then
                    txtPMCCost.Text = Val(txtPMCCost.Text) + Val(txtCost3.Text)
                    txtTotalCost.Text = Val(txtTotalCost.Text) + Val(txtCost3.Text)
                End If



            
                'If Val(txtSellingPrice1.Text) > 0 Then
                '    txtSMCCost.Text = Val(txtSMCCost.Text) + Val(txtSellingPrice1.Text)
                'End If
                'If Val(txtSellingPrice2.Text) > 0 Then
                '    txtSMCCost.Text = Val(txtSMCCost.Text) + Val(txtSellingPrice2.Text)
                'End If
                'If Val(txtSellingPrice3.Text) > 0 Then
                '    txtSMCCost.Text = Val(txtSMCCost.Text) + Val(txtSellingPrice3.Text)
                'End If
                'If Val(txtSellingPrice4.Text) > 0 Then
                '    txtSMCCost.Text = Val(txtSMCCost.Text) + Val(txtSellingPrice4.Text)
                'End If
            End If


            sPDate = txtIDD.Text.Trim + "/" + txtIMM.Text.Trim + "/" + txtIYYYY.Text.Trim
            If IsDate(sPDate) Then


                If mspurchasetype = "B" And Trim(TxtPcsInventoryCode.Text) <> "" Then
                    sSaveInventoryCode = TxtPcsInventoryCode.Text.Trim
                Else
                    sSaveInventoryCode = txtInventoryCode.Text.Trim
                End If


                If mspurchasetype = "B" And Trim(TxtPcsInventoryCode.Text) <> "" Then
                    If objInventory.InventoryDPPCSSave(Trim(sSaveInventoryCode), _
                        txtInventoryDescription.Text.Trim, txtInventoryDiamondDescription.Text.Trim, _
                        Val(txtWeight.Text), Val(txtStoneWeight.Text), Val(txtCostPerGramI.Text), _
                        Val(txtPMCCost.Text), Val(txtSMCCost.Text), Val(txtPCS.Text), Val(txtGoldLoss.Text), Val(txtTotalCost.Text), _
                        txtJobsheetNo.Text.Trim, txtInvoiceNo.Text.Trim, Trim(sPDate), txtSupplierCode.Text.Trim, _
                        "", Trim(sPDate), 0, 0, 0, txtCategoryCode.Text.Trim, Trim(sClassCode), txtCatalogueCode.Text.Trim, txtTrayId.Text.Trim, _
                        Trim(sPMCType), Trim(sSMCType), CLng(txtPCS.Text), 0, txtRemarks.Text.Trim, Val(txtSizeCode.Text.Trim), Trim(sSizeType), ByteArr, picImage.Name, sUserID, Val(sSalesPcs)) Then

                        'sinventorycode = txtInventoryCode.Text.Trim

                        txtInventoryCode.Text = Trim(sCreateInventory)

                        If chkIPM.Checked Then
                            If objInventory.InventoryBarCoinSave(txtInventoryCode.Text.Trim, cboType.Text.Trim, _
                                cboGSTType.Text.Trim, cboPurity.Text.Trim, cboRefiner.Text.Trim, cboCountry.Text.Trim, txtBarNo.Text.Trim, sUserID) Then
                            Else
                                MsgBox("Error in Saving Inventory BarCoin Details", MsgBoxStyle.Critical)
                                fncInventory = False
                                Exit Function
                            End If
                        End If

                        If chkAdditionalDesc.Checked Then
                            If objInventory.InventoryAddlDescSave(Trim(sSaveInventoryCode), txtJobsheetNo.Text.Trim, txtDesc1.Text.Trim, _
                                txtDesc2.Text.Trim, txtDesc3.Text.Trim, txtDesc4.Text.Trim, Val(txtCost1.Text.Trim), Val(txtCost2.Text.Trim), _
                                Val(txtCost3.Text.Trim), Val(txtCost4.Text.Trim), Val(txtSellingPrice1.Text.Trim), Val(txtSellingPrice2.Text.Trim), _
                                Val(txtSellingPrice3.Text.Trim), Val(txtSellingPrice4.Text.Trim), sUserID) Then
                            Else
                                MsgBox("Error in Saving Inventory BarCoin Details", MsgBoxStyle.Critical)
                                fncInventory = False
                                Exit Function
                            End If
                        End If

                    Else
                        MsgBox("Error in Saving Inventory", MsgBoxStyle.Critical)
                        fncInventory = False
                        Exit Function
                    End If

                Else
                    If objInventory.InventoryDPSave(Trim(sSaveInventoryCode), _
                    txtInventoryDescription.Text.Trim, txtInventoryDiamondDescription.Text.Trim, _
                    Val(txtWeight.Text), Val(txtStoneWeight.Text), Val(txtCostPerGramI.Text), _
                    Val(txtPMCCost.Text), Val(txtSMCCost.Text), Val(txtPCS.Text), Val(txtGoldLoss.Text), Val(txtTotalCost.Text), _
                    txtJobsheetNo.Text.Trim, txtInvoiceNo.Text.Trim, Trim(sPDate), txtSupplierCode.Text.Trim, _
                    "", Trim(sPDate), 0, 0, 0, txtCategoryCode.Text.Trim, Trim(sClassCode), txtCatalogueCode.Text.Trim, txtTrayId.Text.Trim, _
                    Trim(sPMCType), Trim(sSMCType), CLng(txtPCS.Text), 0, txtRemarks.Text.Trim, Val(txtSizeCode.Text.Trim), Trim(sSizeType), ByteArr, picImage.Name, sUserID, Val(sSalesPcs)) Then

                        'sinventorycode = txtInventoryCode.Text.Trim

                        txtInventoryCode.Text = Trim(sCreateInventory)

                        If chkIPM.Checked Then
                            If objInventory.InventoryBarCoinSave(txtInventoryCode.Text.Trim, cboType.Text.Trim, _
                                cboGSTType.Text.Trim, cboPurity.Text.Trim, cboRefiner.Text.Trim, cboCountry.Text.Trim, txtBarNo.Text.Trim, sUserID) Then
                            Else
                                MsgBox("Error in Saving Inventory BarCoin Details", MsgBoxStyle.Critical)
                                fncInventory = False
                                Exit Function
                            End If
                        End If

                        If chkAdditionalDesc.Checked Then
                            If objInventory.InventoryAddlDescSave(txtInventoryCode.Text.Trim, txtJobsheetNo.Text.Trim, txtDesc1.Text.Trim, _
                                txtDesc2.Text.Trim, txtDesc3.Text.Trim, txtDesc4.Text.Trim, Val(txtCost1.Text.Trim), Val(txtCost2.Text.Trim), _
                                Val(txtCost3.Text.Trim), Val(txtCost4.Text.Trim), Val(txtSellingPrice1.Text.Trim), Val(txtSellingPrice2.Text.Trim), _
                                Val(txtSellingPrice3.Text.Trim), Val(txtSellingPrice4.Text.Trim), sUserID) Then
                            Else
                                MsgBox("Error in Saving Inventory Additional Description", MsgBoxStyle.Critical)
                                fncInventory = False
                                Exit Function
                            End If
                        End If

                    Else
                        MsgBox("Error in Saving Inventory", MsgBoxStyle.Critical)
                        fncInventory = False
                        Exit Function
                    End If
                End If


            Else
                MsgBox("Invoice Date is Invalid...!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Trim(txtInventoryCode.Text) <> "" Then
                bPurchase = objPurchase.PurchaseSave(txtJobsheetNo.Text.Trim, _
                                    txtInvoiceNo.Text.Trim, " ", txtSupplierCode.Text.Trim, txtInventoryCode.Text.Trim, _
                                    txtInventoryDescription.Text.Trim, txtInventoryDiamondDescription.Text.Trim, Val(txtStoneWeight.Text), _
                                    Val(txtCostPerGramI.Text), Val(txtWeight.Text), Val(txtPMCCost.Text), Val(txtPCS.Text), Val(txtGoldLoss.Text), Val(txtTotalCost.Text), _
                                    txtCatalogueCode.Text.Trim, txtCategoryCode.Text.Trim, Trim(sClassCode), txtTrayId.Text.Trim, "JP", _
                                    txtRemarks.Text.Trim, sUserID)
                If Not bPurchase Then
                    MsgBox("Error in Saving Purchase", MsgBoxStyle.Critical)
                    fncInventory = False
                    Exit Function
                End If

                If objInventory.InventoryMovementSave(Trim(gthisLocation), txtInventoryCode.Text.Trim, txtInventoryDescription.Text.Trim, "PU", txtInvoiceNo.Text.Trim, Trim(sPDate), _
                             Val(txtPCS.Text), Val(txtWeight.Text), Val(txtSizeCode.Text), Val(txtPMCCost.Text), Val(txtSMCCost.Text), Val(txtCostPerGramI.Text), Val(txtTotalCost.Text), _
                             txtSupplierCode.Text.Trim, sUserID) Then
                Else
                    MsgBox("Error in Saving Inventory Movement", MsgBoxStyle.Critical)
                    fncInventory = False
                    Exit Function
                End If
            End If

            ' txtInventoryCode.Text = sinventorycode

            'Weight saved is the piece weight; purchase cost is without mc; purchase mc is the total mc

            '' ''bPurchaseBill = objPurchase.PurchaseBillSave(txtSupplierCode.Text.Trim, txtInvoiceNo.Text.Trim, Trim(sPDate), _
            '' ''            Trim(sPayType), Val(txtWeight.Text), Val(txtTotalCost.Text) - Val(txtPMCCost.Text), Val(txtPMCCost.Text), _
            '' ''            "JP", sUserID)
            '' ''If Not bPurchaseBill Then
            '' ''    MsgBox("Error in Saving PurchaseBill", MsgBoxStyle.Critical)
            '' ''    fncInventory = False
            '' ''    Exit Function
            '' ''End If


            If sRFIDModule = "1" Then


                Dim dsMCC As New DataSet
                Dim sChar As String

                dsMCC = objInventory.GetMcConvertion(txtSMCCost.Text.Trim)
                sChar = Mid(Trim(dsMCC.Tables(0).Rows(0)("Mc") & ""), 1, Len(Trim(dsMCC.Tables(0).Rows(0)("Mc") & "")) - 3)


                If objInventory.InventoryRFIDSave(txtInventoryCode.Text.Trim, _
                    txtInventoryDescription.Text.Trim, txtInventoryDiamondDescription.Text.Trim, _
                    Val(txtWeight.Text), _
                    Val(txtPMCCost.Text), Val(txtSMCCost.Text), Val(txtGoldLoss.Text), txtSupplierCode.Text.Trim, _
                    txtCategoryCode.Text.Trim, Trim(sClassCode), txtCatalogueCode.Text.Trim, txtTrayId.Text.Trim, sChar) Then


                Else
                    MsgBox("Error in Saving RFID Inventory", MsgBoxStyle.Critical)
                    fncInventory = False
                    Exit Function
                End If

            End If
            fncInventory = True
            ' txtInventoryCode.Text = sinventorycode

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
        Dim sSplCategory As String
        Try
            sCatalogueCode = txtCatalogueCode.Text.Trim

            sQry = "Select SUM(Weight), SUM(PCS) from tblPurchase where CatalogueCode = " & _
                        fncFormatSQLText(sCatalogueCode) & " and JobSheetNo = " & fncFormatSQLText(txtJobsheetNo.Text.Trim)

            Ds = dm.getDataSetWithSqlAdapter(sQry, "Purchase", "Purchase")

            If Ds.Tables("Purchase").Rows.Count > 0 Then
                lTotalWt = Val(Ds.Tables("Purchase").Rows(0)(0) & "") + (Val(txtWeight.Text) - Val(txtStoneWeight.Text))
                lTotalPCS = Val(Ds.Tables("Purchase").Rows(0)(1) & "") + Val(txtPCS.Text)
            End If
            If grdPurchase.RowCount <> 0 Then
                If bEdited And grdPurchase.CurrentRow.Index <> grdPurchase.RowCount - 1 Then
                    lTotalWt = lTotalWt - Val(fnGetdgvText(grdPurchase, grdPurchase.CurrentRow.Index, 4))
                    lTotalPCS = lTotalPCS - Val(fnGetdgvText(grdPurchase, grdPurchase.CurrentRow.Index, 10))
                End If

            End If
            'If bEdited And grdPurchase.CurrentRow.Index <> grdPurchase.RowCount - 1 Then
            '    lTotalWt = lTotalWt - Val(GetText(grdPurchase, grdPurchase.CurrentRow.Index, 4))
            '    lTotalPCS = lTotalPCS - Val(GetText(grdPurchase, grdPurchase.CurrentRow.Index, 10))
            'End If

            sQry = "Select Weight, PCS from tblJobSheetDetail where CatalogueCode = " & _
                        fncFormatSQLText(sCatalogueCode) & " and JobSheetNo = " & fncFormatSQLText(txtJobsheetNo.Text)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "JobSheet", "Jobsheet")

            If Ds.Tables("JobSheet").Rows.Count > 0 Then
                lMaxWt = Val(Ds.Tables("JobSheet").Rows(0)(0) & "")
                lMaxPCS = Val(Ds.Tables("JobSheet").Rows(0)(1) & "")
            End If

            sQry = "Select SplCategory from tblJobSheetHeader where JobSheetNo = " & fncFormatSQLText(txtJobsheetNo.Text)

            Ds = dm.getDataSetWithSqlAdapter(sQry, "JobSheetHeader", "JobSheetHeader")

            If Ds.Tables("JobSheetHeader").Rows.Count > 0 Then
                sSplCategory = Trim(Ds.Tables("JobSheetHeader").Rows(0)(0) & "")
            End If

            'check the weight for each inventory irrespective of the PCS or Weight in the Class.

            If Trim(sSplCategory) = "Y" Then
                lAllMaxWt = lMaxWt + ((100 * lMaxWt) / 100)
            Else
                lAllMaxWt = lMaxWt + ((dPurWtVarPerc * lMaxWt) / 100)
            End If


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
                '  txtPMCCost.Text = Val(txtPMC.Text) * Val(txtWeight.Text)
                If Val(txtStoneWeight.Text) > 0 Then
                    txtPMCCost.Text = Val(txtPMC.Text) * (Val(txtWeight.Text) - Val(txtStoneWeight.Text))
                Else
                    txtPMCCost.Text = Val(txtPMC.Text) * Val(txtWeight.Text)
                End If
            Else
                txtPMCCost.Text = Val(txtPMC.Text) * IIf(Val(txtPCS.Text) > 0, Val(txtPCS.Text), 1)
            End If
            txtPMCCost.Text = Format(Val(txtPMCCost.Text), "#0.00")

            If optSMCGram.Checked Then
                If Val(txtStoneWeight.Text) > 0 Then
                    txtSMCCost.Text = Val(txtSMC.Text) * (Val(txtWeight.Text) - Val(txtStoneWeight.Text))
                Else
                    txtSMCCost.Text = Val(txtSMC.Text) * Val(txtWeight.Text)
                End If
            Else
                txtSMCCost.Text = Val(txtSMC.Text) * IIf(Val(txtPCS.Text) > 0, Val(txtPCS.Text), 1)
            End If
            txtSMCCost.Text = Format(Math.Round(Val(txtSMCCost.Text)), "#0.00")

            If Trim(sPurchaseCostingGL) = "Y" Then
                dCost = (Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGramI.Text) + Val(txtPMCCost.Text)
            Else
                ' dCost = (Val(txtWeight.Text)) * Val(txtCostPerGramI.Text) + Val(txtPMCCost.Text)
                If Val(txtStoneWeight.Text) > 0 Then
                    dCost = (Val(txtWeight.Text) - Val(txtStoneWeight.Text)) * Val(txtCostPerGramI.Text) + Val(txtPMCCost.Text)
                Else
                    dCost = (Val(txtWeight.Text)) * Val(txtCostPerGramI.Text) + Val(txtPMCCost.Text)
                End If
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


            Dim Dcs As DataSet
            Dim objCategory As New ClsCategory
            If txtSizeCode.Text.Trim = "" Then
                Dcs = objCategory.GetCategorywithSizeCode(txtCategoryCode.Text.Trim)
                If Dcs.Tables("Category").Rows.Count >= 0 Then
                    If Trim(Dcs.Tables("Category").Rows(0)("SizeFactor") & "") Then
                        MsgBox("SizeCode Cannot be Empty", MsgBoxStyle.Critical)
                        If txtSizeCode.Enabled Then txtSizeCode.Focus()
                        Exit Function
                    End If
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
                If chkSalesPCS.Checked Then
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

            End If
            If UCase(sCompanyShortCode) <> "MJ" Then
                If Val(txtCostPerGramI.Text) <= 0 And mspurchasetype <> "P" Then
                    MsgBox("Invalid cost per gram!", MsgBoxStyle.Critical)
                    If txtCostPerGramI.Enabled Then txtCostPerGramI.Focus()
                    fncValidateInventory = False
                    Exit Function
                End If
            End If
            ''HAJ on 24 sep 2007
            ''If txtInventoryDiamondDescription.Text.Trim = "" And mspurchasetype = "P" Then
            ''    MsgBox("Description Cannot be Blank ....", MsgBoxStyle.Critical)
            ''    If txtInventoryDiamondDescription.Enabled Then txtInventoryDiamondDescription.Focus()
            ''    fncValidateInventory = False
            ''    Exit Function
            ''End If

            If txtInventoryDiamondDescription.Text.Trim <> "" And mspurchasetype <> "P" Then
                ''' Hide By Durai on 29/12/2014
                'If Val(txtStoneWeight.Text) = 0 Then
                '    MsgBox("Please enter the Stone weight!", MsgBoxStyle.Critical)
                '    If txtStoneWeight.Enabled Then txtStoneWeight.Focus()
                '    fncValidateInventory = False
                '    Exit Function
                'End If

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

            If chkIPM.Checked Then
                If cboType.Text.Trim = "" Then
                    MsgBox("Type Cannot be Blank ....")
                    If cboType.Enabled Then cboType.Focus()
                    Exit Function
                End If


                If txtBarNo.Text.Trim = "" Then
                    MsgBox("BarNo Cannot be Blank ....")
                    If txtBarNo.Enabled Then txtBarNo.Focus()
                    Exit Function
                End If

                If cboPurity.Text.Trim = "" Then
                    MsgBox("Purity Cannot be Blank ....")
                    If cboPurity.Enabled Then cboPurity.Focus()
                    Exit Function
                End If

                If cboGSTType.Text.Trim = "" Then
                    MsgBox("GST Type Cannot be Blank ....")
                    If cboGSTType.Enabled Then cboGSTType.Focus()
                    Exit Function
                End If

                If cboRefiner.Text.Trim = "" Then
                    MsgBox("Refiner Cannot be Blank ....")
                    If cboRefiner.Enabled Then cboRefiner.Focus()
                    Exit Function
                End If

                If cboCountry.Text.Trim = "" Then
                    MsgBox("Country Cannot be Blank ....")
                    If cboCountry.Enabled Then cboCountry.Focus()
                    Exit Function
                End If

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
            sClass = Trim(fnGetdgvText(grdPurchase, grdPurchase.CurrentRow.Index, 2))
            dS = objClass.GetClassDetail(Trim(sClass))
            If dS.Tables("Class").Rows.Count > 0 Then
                sPurType = Trim(dS.Tables("Class").Rows(0)("PurchaseType") & "")
            End If
            dS = Nothing
            objClass = Nothing
            If sCompanyShortCode = "HN" Or UCase(sCompanyShortCode) = "MJ" Or UCase(sCompanyShortCode) = "SSJ" Or UCase(sCompanyShortCode) = "RJ" Or UCase(sCompanyShortCode) = "MD" Then
                Dim objfrm As New frmBarcode
                objfrm.txtInventoryCode.Text = Trim(fnGetdgvText(grdPurchase, grdPurchase.CurrentRow.Index, 0))
                objfrm.txtInventoryCode_LostFocus(sender, e)
                objfrm.btnPrint_Click(sender, e)
            ElseIf sCompanyShortCode = "AS" Then
                Dim objfrm As New frmJewelBarcode
                objfrm.txtInventoryCode.Text = Trim(fnGetdgvText(grdPurchase, grdPurchase.CurrentRow.Index, 0))
                objfrm.txtInventoryCode_LostFocus(sender, e)
                objfrm.btnPrint_Click(sender, e)
            ElseIf UCase(sCompanyShortCode) = "AJ" Then
                Dim objfrm As New frmBarcode
                objfrm.txtInventoryCode.Text = Trim(fnGetdgvText(grdPurchase, grdPurchase.CurrentRow.Index, 0))
                objfrm.txtInventoryCode_LostFocus(sender, e)
                objfrm.btnPrint_Click(sender, e)
            ElseIf sCompanyShortCode = "ES" Or sCompanyShortCode = "ML" Then
                If sPurType = "W" Then
                    Dim objfrm As New frmBarcode
                    objfrm.txtInventoryCode.Text = Trim(fnGetdgvText(grdPurchase, grdPurchase.CurrentRow.Index, 0))
                    objfrm.txtInventoryCode_LostFocus(sender, e)
                    objfrm.btnPrint_Click(sender, e)
                ElseIf sPurType = "P" Then
                    Dim objfrm As New frmJewelBarcode
                    objfrm.txtInventoryCode.Text = Trim(fnGetdgvText(grdPurchase, grdPurchase.CurrentRow.Index, 0))
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

            i = grdPurchase.CurrentRow.Index
            If sBillClose = "Y" Then
                MsgBox("This bill is closed. Cannot delete!", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If sTranType = "DP" Then
                MsgBox("This is a direct purchase bill. Cannot delete!", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If grdPurchase.CurrentRow.Index <> grdPurchase.RowCount - 1 And grdPurchase.CurrentRow.Index > -1 Then
                If MsgBox("Do you want to delete item no. " & Trim(fnGetdgvText(grdPurchase, grdPurchase.CurrentRow.Index, 0)) & "?", vbYesNo) = vbNo Then
                    Exit Sub
                End If
            Else
                MsgBox("Select item for deletion!", vbCritical)
                Exit Sub
            End If

            sinventorycode = Trim(fnGetdgvText(grdPurchase, grdPurchase.CurrentRow.Index, 0))

            If sinventorycode <> "TOTAL:" And Trim(sinventorycode) <> "" Then
                'don't allow to change or delete the sold inventory.
                sSql = "Select SalesBillNo, a.CreateDate, QtyOnHand, PCS from tblInventory a,tblInventoryStock b where a.InventoryCode=b.InventoryCode and "
                sSql = sSql & "a.LocationCode=b.LocationCode and "
                sSql = sSql & "a.InventoryCode = " & fncFormatSQLText(sinventorycode)

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
                        'grdPurchase.Action = FPSpread.ActionConstants.ActionDeleteRow
                        'grdPurchase.MaxRows = grdPurchase.MaxRows - 1
                        'If grdPurchase.MaxRows = 1 Then
                        '    grdPurchase.MaxRows = 0
                        '    subGetJobSheetDetails()
                        'Else
                        '    subTotal()
                        'End If
                        'grdJobSheet.Rows.RemoveAt(grdJobSheet.CurrentRow.Index)
                        'If grdJobSheet.RowCount = 1 Then
                        '    grdJobSheet.RowCount = 0
                        'Else
                        '    'subTotalFromGrid()
                        '    btnClear_Click(Nothing, Nothing)
                        'End If
                        grdPurchase.Rows.RemoveAt(grdPurchase.CurrentRow.Index)
                        If grdPurchase.RowCount = 1 Then
                            grdPurchase.RowCount = 0
                        Else
                            subTotalFromGrid()
                            subTotal()
                            'subGetJobSheetDetails()
                            btnClear_Click(Nothing, Nothing)
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
            If Trim(sCompanyShortCode) = "AJ" Then
                txtTrayId.Text = "DEF"
                txtGoldLoss.Text = 0
                txtInventoryDescription.Focus()
            End If
            If UCase(sCompanyShortCode) = "SSJ" Then
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
                    ''txtInventoryCode.Text = Format(Val(Ds.Tables("Category").Rows(0)("NextInventoryCode") & ""), "000000")
                    txtInventoryCode.Text = Trim(Ds.Tables("category").Rows(0)("NextInventoryCode") & "")
                    'get the jobsheet detail
                    sSql = "Select * from tblJobSheetDetail where JobSheetNo = " & fncFormatSQLText(sJobSheetCode) & _
                                " and CatalogueCode = " & fncFormatSQLText(Trim(sCatalogueCode))
                    DsJs = dm.getDataSetWithSqlAdapter(sSql, "JobSheet", "Jobsheet")


                    If DsJs.Tables("JobSheet").Rows.Count > 0 Then
                        txtCostPerGramI.Text = Val(DsJs.Tables("JobSheet").Rows(0)("CostperGram") & "")
                    End If
                    If UCase(sCompanyShortCode) = "SSJ" Then
                        txtCostPerGramI.Text = txtCostPerGram.Text.Trim
                    End If
                Else
                    'MsgBox("Error in fetching category!", MsgBoxStyle.Critical)
                    MsgBox("Error in fetching category! Check category range.", MsgBoxStyle.Critical)
                    txtCatalogueCode.Text = ""
                    txtInventoryCode.Text = ""
                    Exit Sub
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
                    If grdPurchase.RowCount - 1 > 1 Then
                        If Trim$(fnGetdgvText(grdPurchase, grdPurchase.RowCount - 2, 16)) = "P" Then
                            optPMCPCS.Checked = True
                            dMCWtPCS = Val(fnGetdgvText(grdPurchase, grdPurchase.RowCount - 2, 10))
                        Else
                            optPMCGram.Checked = True
                            dMCWtPCS = Val(fnGetdgvText(grdPurchase, grdPurchase.RowCount - 2, 3))
                        End If

                        If Trim$(fnGetdgvText(grdPurchase, grdPurchase.RowCount - 2, 17)) = "P" Then
                            optSMCPCS.Checked = True
                            dSMCWtPCS = Val(fnGetdgvText(grdPurchase, grdPurchase.RowCount - 2, 10))
                        Else
                            optSMCGram.Checked = True
                            dSMCWtPCS = Val(fnGetdgvText(grdPurchase, grdPurchase.RowCount - 2, 3))
                        End If
                    Else
                        If sCompanyShortCode = "ES" Then
                            optSMCPCS.Checked = True
                            optPMCPCS.Checked = True
                        Else
                            optPMCGram.Checked = True
                            optSMCGram.Checked = True
                        End If
                        If grdPurchase.RowCount = 0 Then

                        Else
                            dMCWtPCS = Val(fnGetdgvText(grdPurchase, grdPurchase.RowCount - 1, 3))
                            dSMCWtPCS = Val(fnGetdgvText(grdPurchase, grdPurchase.RowCount - 1, 3))
                        End If
                        'dMCWtPCS = Val(fnGetdgvText(grdPurchase, grdPurchase.RowCount - 1, 3))
                        'dSMCWtPCS = Val(fnGetdgvText(grdPurchase, grdPurchase.RowCount - 1, 3))
                    End If

                    'display the last desc
                    If txtCatalogueCode.Text.Trim <> sCatalogueCode Then
                        txtInventoryDescription.Text = Trim(Ds.Tables("Catalogue").Rows(0)("CatalogueName") & "")
                    ElseIf grdPurchase.RowCount > 1 Then
                        txtInventoryDescription.Text = fnGetdgvText(grdPurchase, grdPurchase.RowCount - 2, 2)
                        txtGoldLoss.Text = Val(fnGetdgvText(grdPurchase, grdPurchase.RowCount - 2, 6))
                        txtPCS.Text = Val(fnGetdgvText(grdPurchase, grdPurchase.RowCount - 2, 10))
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
    Private Function fncValidCategoryCode() As Boolean
        Dim sSelect As String
        Dim i As Integer
        Dim sCategoryCode As String
        Dim Ds As DataSet

        If txtJobsheetNo.Text.Trim = "" Then
            fncValidCategoryCode = False
            Exit Function
        End If
        sCategoryCode = txtCategoryCode.Text.Trim
        sSelect = "Select CategoryCode from tblCategory Where CategoryCode in " & _
                "(Select DISTINCT(CategoryCode) From tblJobsheetDetail Where " & _
                "JobSheetNo = " & fncFormatSQLText(Trim$(txtJobsheetNo.Text)) & ")"
        Ds = dm.getDataSetWithSqlAdapter(sSelect, "JobSheet", "JobSheet")
        For i = 0 To Ds.Tables("JobSheet").Rows.Count - 1
            If Trim(UCase(sCategoryCode)) = UCase(Trim(Ds.Tables("JobSheet").Rows(i)("CategoryCode") & "")) Then
                txtCategoryCode.Text = Trim(Ds.Tables("JobSheet").Rows(i)("CategoryCode") & "")
                sCategoryCode = txtCategoryCode.Text.Trim
                fncValidCategoryCode = True
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
                        txtWeight.Focus()
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
                    'txtPMCCost.Text = Format(Val(txtPMC.Text) * Val(txtWeight.Text), "#0.00")
                    If Val(txtStoneWeight.Text) > 0 Then
                        txtPMCCost.Text = Format(Val(txtPMC.Text) * (Val(txtWeight.Text) - Val(txtStoneWeight.Text)), "#0.00")
                    Else
                        txtPMCCost.Text = Format(Val(txtPMC.Text) * Val(txtWeight.Text), "#0.00")
                    End If
                Else
                    txtPMCCost.Text = Format(Val(txtPMC.Text) * Val(txtPCS.Text), "#0.00")
                End If
            Else
                txtPMCCost.Text = "0.00"
            End If
            If chkAutoSMC.Checked = True Then
                If optSMCGram.Checked Then
                    txtSMC.Text = Val(txtPMC.Text) * 4
                    txtSMCCost.Text = Val(txtPMC.Text) * Val(txtWeight.Text) * 4
                Else
                    txtSMCCost.Text = Val(txtPMC.Text) * 4
                    txtSMCCost.Text = Val(txtPMC.Text) * Val(txtPCS.Text) * 4
                End If
                txtSMC.Text = Format(Math.Round(Val(txtSMC.Text)), "#0.00")
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
                    txtSMCCost.Text = Val(txtPMC.Text) * Val(txtWeight.Text) * 4
                Else
                    txtSMCCost.Text = Val(txtPMC.Text) * Val(txtPCS.Text) * 4
                End If
                txtSMC.Text = Format(Math.Round(Val(txtSMCCost.Text)), "#0.00")
                txtSMCCost.Text = Format(Math.Round(Val(txtSMCCost.Text)), "#0.00")
            Else
                If optSMCGram.Checked Then
                    'txtSMCCost.Text = Val(txtSMC.Text) * Val(txtWeight.Text)
                    If Val(txtStoneWeight.Text) > 0 Then
                        txtSMCCost.Text = Val(txtSMC.Text) * (Val(txtWeight.Text) - Val(txtStoneWeight.Text))
                    Else
                        txtSMCCost.Text = Val(txtSMC.Text) * Val(txtWeight.Text)
                    End If
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
                '  txtTotalCost.Text = Format((Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGramI.Text) + _
                '     IIf(optPMCGram.Checked, Val(txtPMC.Text) * Val(txtWeight.Text), Val(txtPCS.Text) * Val(txtPMC.Text)), "#0.00")

                If Val(txtStoneWeight.Text) > 0 Then
                    txtTotalCost.Text = Format(((Val(txtWeight.Text) - Val(txtStoneWeight.Text)) + Val(txtGoldLoss.Text)) * Val(txtCostPerGramI.Text) + _
                        IIf(optPMCGram.Checked, Val(txtPMC.Text) * Val(txtWeight.Text), Val(txtPCS.Text) * Val(txtPMC.Text)), "#0.00")
                Else
                    txtTotalCost.Text = Format((Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGramI.Text) + _
                            IIf(optPMCGram.Checked, Val(txtPMC.Text) * Val(txtWeight.Text), Val(txtPCS.Text) * Val(txtPMC.Text)), "#0.00")
                End If
            Else
                ' txtTotalCost.Text = Format(Val(txtWeight.Text) * Val(txtCostPerGramI.Text) + _
                '    IIf(optPMCGram.Checked, Val(txtPMC.Text) * Val(txtWeight.Text), Val(txtPCS.Text) * Val(txtPMC.Text)), "#0.00")
                If Val(txtStoneWeight.Text) > 0 Then
                    txtTotalCost.Text = Format(IIf(optPMCGram.Checked, (Val(txtWeight.Text) - Val(txtStoneWeight.Text)) * Val(txtCostPerGramI.Text), 0) + _
                        IIf(optPMCGram.Checked, Val(txtPMC.Text) * (Val(txtWeight.Text) - Val(txtStoneWeight.Text)), Val(txtPCS.Text) * Val(txtPMC.Text)), "#0.00")
                Else
                    txtTotalCost.Text = Format(IIf(optPMCGram.Checked, Val(txtWeight.Text) * Val(txtCostPerGramI.Text), 0) + _
                             IIf(optPMCGram.Checked, Val(txtPMC.Text) * Val(txtWeight.Text), Val(txtPCS.Text) * Val(txtPMC.Text)), "#0.00")
                End If
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
                '''  If txtGoldLoss.Enabled Then txtGoldLoss.Focus()
                If txtSizeCode.Enabled Then txtSizeCode.Focus()
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
                    '            dCost = (Val(txtWeight.Text)) * Val(txtCostPerGramI.Text) + Val(txtPMCCost.Text)
                    If Val(txtStoneWeight.Text) > 0 Then
                        dCost = (Val(txtWeight.Text) - Val(txtStoneWeight.Text)) * Val(txtCostPerGramI.Text) + Val(txtPMCCost.Text)
                    Else
                        dCost = (Val(txtWeight.Text)) * Val(txtCostPerGramI.Text) + Val(txtPMCCost.Text)
                    End If
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



    Private Sub txtPMC_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPMC.TextChanged

    End Sub

    Private Sub chkIPM_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkIPM.CheckedChanged
        If chkIPM.Checked = True Then
            gbIPM.Visible = True
            cboType.Text = ""
            cboGSTType.Text = ""
            cboRefiner.Text = ""
            cboPurity.Text = ""
            cboCountry.Text = ""
            txtBarNo.Text = ""
        Else
            gbIPM.Visible = False
        End If
    End Sub
    Public Sub AddItemstoComboRefiner()
        dm.SetMasterDataSetLoc("Select * from tblRefiner where Type = " & fncFormatSQLText(cboType.Text.Trim), "tblRefiner")
        dm.FillDatasToComboListWithFilters(cboRefiner, "tblRefiner", "RefinerName", "RefinerCode")
    End Sub
    Public Sub AddItemstoComboCountry()
        dm.SetMasterDataSetLoc("Select * from tblCountry where Type = " & fncFormatSQLText(cboType.Text.Trim), "tblCountry")
        dm.FillDatasToComboListWithFilters(cboCountry, "tblCountry", "CountryName", "CountryCode")
    End Sub

    Private Sub cboType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboType.SelectedIndexChanged
        If Trim(cboType.Text) = "Coin" Then
            lblRefiner.Text = "Name of Coin"
        Else
            lblRefiner.Text = "Name of Refiner"
        End If
        AddItemstoComboRefiner()
        AddItemstoComboCountry()
    End Sub

    Private Sub btnSizeCodeSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSizeCodeSearch.Click
        Try
            Dim e1 As New System.EventArgs
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSize", "L", 4, "SizeCode", "categorycode=" & fncFormatSQLText(txtCategoryCode.Text), "SizeCode", "SizeCode", 80, "SizeType", "SizeType", 100, "CategoryCode", "CategoryCode", 100, "Remarks", "Remarks", 100)
            frmHelpClass.ShowDialog()
            txtSizeCode.Text = frmHelpClass.Var1.Trim
            sSizeType = frmHelpClass.Var2.Trim
            txtSizeCode_LostFocus(sender, e1)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtSizeCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtSizeCode.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                Dim e1 As New System.EventArgs
                Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSize", "L", 4, "SizeCode", "categorycode=" & fncFormatSQLText(sCategoryCode), "SizeCode", "SizeCode", 80, "SizeType", "SizeType", 100, "CategoryCode", "CategoryCode", 100, "Remarks", "Remarks", 30)
                frmHelpClass.ShowDialog()
                txtSizeCode.Text = frmHelpClass.Var1.Trim
                sSizeType = frmHelpClass.Var2.Trim
                txtSizeCode_LostFocus(sender, e1)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtSizeCode_LostFocus(sender As Object, e As System.EventArgs) Handles txtSizeCode.LostFocus
        Dim Ds As DataSet
        Dim objSize As New ClsCatalogue
        Try
            sSizeType = ""
            If txtSizeCode.Text.Trim <> "" Then
                Ds = objSize.GetSizewithCode(txtSizeCode.Text.Trim, txtCategoryCode.Text.Trim)
                If Ds.Tables("Size").Rows.Count <= 0 Then
                    MsgBox("Invalid Size Code!", MsgBoxStyle.Critical)
                    If txtSizeCode.Enabled Then txtSizeCode.Focus()
                    Exit Sub
                Else
                    txtSizeCode.Text = Trim(Ds.Tables("Size").Rows(0)("SizeCode") & "")
                    sSizeType = Trim(Ds.Tables("Size").Rows(0)("SizeType") & "")
                    If txtGoldLoss.Enabled Then txtGoldLoss.Focus()
                End If
            End If
            Ds = Nothing
            objSize = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub btnCategoryCodeSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnCategoryCodeSearch.Click
        Try
            Dim Search As String
            Search = " categorycode in (Select DISTINCT(Categorycode) From tblJobsheetDetail " & _
                                        "Where JobSheetNo = " & fncFormatSQLText(txtJobsheetNo.Text.Trim) & " )"
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCategory", "L", 3, "CategoryName", Search, "CategoryCode", "CategoryCode", 80, "CategoryName", "CategoryName", 180, "ClassCode", "ClassCode", 80)
            frmHelpClass.ShowDialog()
            txtCategoryCode.Text = frmHelpClass.Var1.Trim
            'If txtCatalogueCode.Enabled Then txtCatalogueCode.Focus()
            txtCategoryCode_LostFocus(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            Err.Clear()
        End Try
    End Sub

    Private Sub txtCategoryCode_LostFocus(sender As Object, e As System.EventArgs) Handles txtCategoryCode.LostFocus
        Try
            If txtCategoryCode.Text.Trim <> "" Then
                If Not fncValidCategoryCode() Then
                    MsgBox("Please select the suitable category for the selected job sheet!", MsgBoxStyle.Critical)
                    txtCategoryCode.Text = ""
                    If txtCategoryCode.Enabled Then Call txtCategoryCode.Focus()
                    Exit Sub
                End If
            End If
            '''If Not bEdited Then subGetCatalogue()
            ''' sCatalogueCode = txtCatalogueCode.Text.Trim
            'since catergory code is locked. call the subGetCategory fn. also.
            sCategoryCode = txtCategoryCode.Text.Trim
            ''If Not bEdited Then
            ''    subGetCategory()
            ''    subSetPurchaseType()
            ''End If
            ''If UCase(sCompanyShortCode) = "SSJ" Then
            ''    txtTrayId.Text = "DEF"
            ''    txtInventoryDescription.Focus()
            ''Else
            ''    '''txtTrayId.Focus()
            ''    If txtSizeCode.Enabled Then txtSizeCode.Focus()
            ''End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub cmdAddlDescOk_Click(sender As System.Object, e As System.EventArgs) Handles cmdAddlDescOk.Click
        grpAddlDescription.Visible = False
        ''chkAdditionalDesc.Checked = False
    End Sub

    Private Sub chkAdditionalDesc_Click(sender As Object, e As System.EventArgs) Handles chkAdditionalDesc.Click
        If chkAdditionalDesc.Checked = True Then
            grpAddlDescription.Visible = True
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
            txtDesc1.Focus()
        Else
            grpAddlDescription.Visible = False
        End If
    End Sub

    Private Sub txtSizeCode_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtSizeCode.TextChanged

    End Sub

    Private Sub txtJobsheetNo_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtJobsheetNo.TextChanged

    End Sub

    Private Sub grdJobSheet_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdJobSheet.CellContentClick

    End Sub


    Private Sub txtCatalogueCode_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtCatalogueCode.TextChanged

    End Sub

    Private Sub grdPurchase_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPurchase.CellContentClick

    End Sub

    Private Sub grdPurchase_DoubleClick(sender As Object, e As System.EventArgs) Handles grdPurchase.DoubleClick

    End Sub

    Private Sub chkAdditionalDesc_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkAdditionalDesc.CheckedChanged

    End Sub

    Private Sub btnBrowseImage_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowseImage.Click
        Dim OpenFileDialog1 As New OpenFileDialog


        With OpenFileDialog1
            .CheckFileExists = True
            .ShowReadOnly = False
            ' .Filter = "All Files|*.*|Bitmap Files (*)|*.bmp;*.gif;*.jpg"
            .Filter = "All Files|*.*|JPEG Files (*)|*.jpg"
            .FilterIndex = 2

            If .ShowDialog = DialogResult.OK Then
                ' Load the specified file into a PictureBox control.

                picImage.Image = Image.FromFile(.FileName)
                AutosizeImage(.FileName, picImage)
                picImage.Name = .SafeFileName
            End If
        End With
    End Sub
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        grpImage.Visible = False
    End Sub
    Private Sub chkImage_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkImage.CheckedChanged
        If chkImage.Checked = True Then
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

    Private Sub TxtPcsInventoryCode_TextChanged(sender As System.Object, e As System.EventArgs) Handles TxtPcsInventoryCode.TextChanged

    End Sub
End Class
