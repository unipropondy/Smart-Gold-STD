Public Class frmJobSheet
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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtCategoryCode As System.Windows.Forms.TextBox
    Friend WithEvents txtWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtGoldLoss As System.Windows.Forms.TextBox
    Friend WithEvents txtMC As System.Windows.Forms.TextBox
    Friend WithEvents txtPCS As System.Windows.Forms.TextBox
    Friend WithEvents txtDD As System.Windows.Forms.TextBox
    Friend WithEvents txtMM As System.Windows.Forms.TextBox
    Friend WithEvents txtYY As System.Windows.Forms.TextBox
    Friend WithEvents OptCash As System.Windows.Forms.RadioButton
    Friend WithEvents txtGST As System.Windows.Forms.TextBox
    Friend WithEvents txtInvoiceNo As System.Windows.Forms.TextBox
    Friend WithEvents txtCurrencyName As System.Windows.Forms.TextBox
    Friend WithEvents txtSupplierName As System.Windows.Forms.TextBox
    Friend WithEvents txtTAEL As System.Windows.Forms.TextBox
    Friend WithEvents txCatalogueName As System.Windows.Forms.TextBox
    Friend WithEvents txtCostPerGram As System.Windows.Forms.TextBox
    Friend WithEvents txtCostPerGram1 As System.Windows.Forms.TextBox
    Friend WithEvents txtCurrencyRate As System.Windows.Forms.TextBox
    Friend WithEvents txtManualGoldLoss As System.Windows.Forms.TextBox
    Friend WithEvents txtManualWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtManualMC As System.Windows.Forms.TextBox
    Friend WithEvents txtManualCost As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalCost As System.Windows.Forms.TextBox
    Friend WithEvents ChkGST As System.Windows.Forms.CheckBox
    Friend WithEvents ChkGST1 As System.Windows.Forms.CheckBox
    Friend WithEvents OptGoldAC As System.Windows.Forms.RadioButton
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents btnCatAdd As System.Windows.Forms.Button
    Friend WithEvents btnCatDelete As System.Windows.Forms.Button
    Friend WithEvents btnCatClear As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents opt22K As System.Windows.Forms.RadioButton
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cboTealPercent1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents txtCategoryName As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents optUnFix As System.Windows.Forms.RadioButton
    Friend WithEvents optFix As System.Windows.Forms.RadioButton
    Friend WithEvents txtCurrencyCode As System.Windows.Forms.TextBox
    Friend WithEvents txtSupplierCode As System.Windows.Forms.TextBox
    Friend WithEvents txtJobSheetNo As System.Windows.Forms.TextBox
    Friend WithEvents btnCurrencyCodeSearch As System.Windows.Forms.Button
    Friend WithEvents btnSupplierCodeSearch As System.Windows.Forms.Button
    Friend WithEvents btnJobSheetNoSearch As System.Windows.Forms.Button
    Friend WithEvents btnCatalogueCode As System.Windows.Forms.Button
    Friend WithEvents txtCatalogueCode As System.Windows.Forms.TextBox
    Friend WithEvents btnCCategoryCodeSearch As System.Windows.Forms.Button
    Friend WithEvents txtCCategoryCode As System.Windows.Forms.TextBox
    Friend WithEvents txtAdjustment As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtNetCost As System.Windows.Forms.TextBox
    Friend WithEvents labe24 As System.Windows.Forms.Label
    Friend WithEvents txtGoldValueMC As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents btnCatBDelete As System.Windows.Forms.Button
    Friend WithEvents grdJobSheet As System.Windows.Forms.DataGridView
    Friend WithEvents chkSplCategory As System.Windows.Forms.CheckBox
    Friend WithEvents chkAccessories As System.Windows.Forms.CheckBox
    Friend WithEvents grpMatchJobsheet As System.Windows.Forms.GroupBox
    Friend WithEvents txtMatchPCS As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtMatchWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents btnMatchJobsheetNo As System.Windows.Forms.Button
    Friend WithEvents txtMatchJobSheetNo As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents btnMatchCatalogue As System.Windows.Forms.Button
    Friend WithEvents txtMatchCatalogue As System.Windows.Forms.TextBox
    Friend WithEvents btnSalesOrderNo As System.Windows.Forms.Button
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txtSalesOrderNo As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents dgvHeader As System.Windows.Forms.DataGridView
    Friend WithEvents CatalogueCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Weight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PCS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkImage As System.Windows.Forms.CheckBox
    Friend WithEvents btnTaelPercent As System.Windows.Forms.Button
    Friend WithEvents cboTealPercent As System.Windows.Forms.TextBox
    Friend WithEvents optBoth As System.Windows.Forms.RadioButton
    Friend WithEvents txtPayableCash As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents btnGoldBarFix As System.Windows.Forms.Button
    Friend WithEvents btnGoldBarIssue As System.Windows.Forms.Button
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents txtDYY As System.Windows.Forms.TextBox
    Friend WithEvents txtDMM As System.Windows.Forms.TextBox
    Friend WithEvents txtDDD As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents txtDiaCost As System.Windows.Forms.TextBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents cCataCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCategCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cDesc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cWt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cGdLoss As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCostPerGm As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cMCJ As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cPCS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents opt24K As System.Windows.Forms.RadioButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmJobSheet))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.txtDYY = New System.Windows.Forms.TextBox()
        Me.txtDMM = New System.Windows.Forms.TextBox()
        Me.txtDDD = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.txtPayableCash = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.optBoth = New System.Windows.Forms.RadioButton()
        Me.btnTaelPercent = New System.Windows.Forms.Button()
        Me.cboTealPercent = New System.Windows.Forms.TextBox()
        Me.btnSalesOrderNo = New System.Windows.Forms.Button()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.txtSalesOrderNo = New System.Windows.Forms.TextBox()
        Me.btnCurrencyCodeSearch = New System.Windows.Forms.Button()
        Me.btnSupplierCodeSearch = New System.Windows.Forms.Button()
        Me.btnJobSheetNoSearch = New System.Windows.Forms.Button()
        Me.txtCurrencyCode = New System.Windows.Forms.TextBox()
        Me.txtSupplierCode = New System.Windows.Forms.TextBox()
        Me.txtJobSheetNo = New System.Windows.Forms.TextBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.optUnFix = New System.Windows.Forms.RadioButton()
        Me.optFix = New System.Windows.Forms.RadioButton()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cboTealPercent1 = New System.Windows.Forms.ComboBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.opt24K = New System.Windows.Forms.RadioButton()
        Me.opt22K = New System.Windows.Forms.RadioButton()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtTAEL = New System.Windows.Forms.TextBox()
        Me.txtCostPerGram = New System.Windows.Forms.TextBox()
        Me.txtCurrencyRate = New System.Windows.Forms.TextBox()
        Me.txtSupplierName = New System.Windows.Forms.TextBox()
        Me.txtCurrencyName = New System.Windows.Forms.TextBox()
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtGoldValueMC = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtAdjustment = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtNetCost = New System.Windows.Forms.TextBox()
        Me.labe24 = New System.Windows.Forms.Label()
        Me.txtManualGoldLoss = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtManualWeight = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.ChkGST = New System.Windows.Forms.CheckBox()
        Me.txtGST = New System.Windows.Forms.TextBox()
        Me.txtManualMC = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtManualCost = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.ChkGST1 = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.OptCash = New System.Windows.Forms.RadioButton()
        Me.OptGoldAC = New System.Windows.Forms.RadioButton()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtYY = New System.Windows.Forms.TextBox()
        Me.txtMM = New System.Windows.Forms.TextBox()
        Me.txtDD = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.txtDiaCost = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.btnCatBDelete = New System.Windows.Forms.Button()
        Me.btnCatalogueCode = New System.Windows.Forms.Button()
        Me.txtCatalogueCode = New System.Windows.Forms.TextBox()
        Me.btnCCategoryCodeSearch = New System.Windows.Forms.Button()
        Me.txtCCategoryCode = New System.Windows.Forms.TextBox()
        Me.txtCategoryName = New System.Windows.Forms.TextBox()
        Me.btnCatAdd = New System.Windows.Forms.Button()
        Me.btnCatDelete = New System.Windows.Forms.Button()
        Me.btnCatClear = New System.Windows.Forms.Button()
        Me.txtTotalCost = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtPCS = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtMC = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtCostPerGram1 = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtGoldLoss = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtWeight = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txCatalogueName = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtCategoryCode = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.grdJobSheet = New System.Windows.Forms.DataGridView()
        Me.cCataCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cCategCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cDesc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cWt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cGdLoss = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cCostPerGm = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cMCJ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cPCS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chkSplCategory = New System.Windows.Forms.CheckBox()
        Me.chkAccessories = New System.Windows.Forms.CheckBox()
        Me.grpMatchJobsheet = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.dgvHeader = New System.Windows.Forms.DataGridView()
        Me.CatalogueCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Weight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PCS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnMatchJobsheetNo = New System.Windows.Forms.Button()
        Me.txtMatchJobSheetNo = New System.Windows.Forms.TextBox()
        Me.btnMatchCatalogue = New System.Windows.Forms.Button()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtMatchCatalogue = New System.Windows.Forms.TextBox()
        Me.txtMatchPCS = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtMatchWeight = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.chkImage = New System.Windows.Forms.CheckBox()
        Me.btnGoldBarFix = New System.Windows.Forms.Button()
        Me.btnGoldBarIssue = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdJobSheet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpMatchJobsheet.SuspendLayout()
        CType(Me.dgvHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.Label34)
        Me.GroupBox1.Controls.Add(Me.Label35)
        Me.GroupBox1.Controls.Add(Me.txtDYY)
        Me.GroupBox1.Controls.Add(Me.txtDMM)
        Me.GroupBox1.Controls.Add(Me.txtDDD)
        Me.GroupBox1.Controls.Add(Me.Label36)
        Me.GroupBox1.Controls.Add(Me.txtPayableCash)
        Me.GroupBox1.Controls.Add(Me.Label33)
        Me.GroupBox1.Controls.Add(Me.optBoth)
        Me.GroupBox1.Controls.Add(Me.btnTaelPercent)
        Me.GroupBox1.Controls.Add(Me.cboTealPercent)
        Me.GroupBox1.Controls.Add(Me.btnSalesOrderNo)
        Me.GroupBox1.Controls.Add(Me.Label31)
        Me.GroupBox1.Controls.Add(Me.txtSalesOrderNo)
        Me.GroupBox1.Controls.Add(Me.btnCurrencyCodeSearch)
        Me.GroupBox1.Controls.Add(Me.btnSupplierCodeSearch)
        Me.GroupBox1.Controls.Add(Me.btnJobSheetNoSearch)
        Me.GroupBox1.Controls.Add(Me.txtCurrencyCode)
        Me.GroupBox1.Controls.Add(Me.txtSupplierCode)
        Me.GroupBox1.Controls.Add(Me.txtJobSheetNo)
        Me.GroupBox1.Controls.Add(Me.GroupBox6)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.cboTealPercent1)
        Me.GroupBox1.Controls.Add(Me.Label32)
        Me.GroupBox1.Controls.Add(Me.GroupBox5)
        Me.GroupBox1.Controls.Add(Me.Label27)
        Me.GroupBox1.Controls.Add(Me.Label26)
        Me.GroupBox1.Controls.Add(Me.txtRemarks)
        Me.GroupBox1.Controls.Add(Me.txtTAEL)
        Me.GroupBox1.Controls.Add(Me.txtCostPerGram)
        Me.GroupBox1.Controls.Add(Me.txtCurrencyRate)
        Me.GroupBox1.Controls.Add(Me.txtSupplierName)
        Me.GroupBox1.Controls.Add(Me.txtCurrencyName)
        Me.GroupBox1.Controls.Add(Me.txtInvoiceNo)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.ChkGST1)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.OptCash)
        Me.GroupBox1.Controls.Add(Me.OptGoldAC)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtYY)
        Me.GroupBox1.Controls.Add(Me.txtMM)
        Me.GroupBox1.Controls.Add(Me.txtDD)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 114)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(900, 207)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(711, 181)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(11, 14)
        Me.Label34.TabIndex = 476265
        Me.Label34.Text = "-"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(671, 182)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(11, 14)
        Me.Label35.TabIndex = 476264
        Me.Label35.Text = "-"
        '
        'txtDYY
        '
        Me.txtDYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDYY.Location = New System.Drawing.Point(719, 179)
        Me.txtDYY.MaxLength = 20
        Me.txtDYY.Name = "txtDYY"
        Me.txtDYY.Size = New System.Drawing.Size(32, 20)
        Me.txtDYY.TabIndex = 476262
        Me.txtDYY.Tag = "NE;"
        '
        'txtDMM
        '
        Me.txtDMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDMM.Location = New System.Drawing.Point(679, 179)
        Me.txtDMM.MaxLength = 20
        Me.txtDMM.Name = "txtDMM"
        Me.txtDMM.Size = New System.Drawing.Size(32, 20)
        Me.txtDMM.TabIndex = 476261
        Me.txtDMM.Tag = "NE;"
        '
        'txtDDD
        '
        Me.txtDDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDDD.Location = New System.Drawing.Point(639, 179)
        Me.txtDDD.MaxLength = 20
        Me.txtDDD.Name = "txtDDD"
        Me.txtDDD.Size = New System.Drawing.Size(32, 20)
        Me.txtDDD.TabIndex = 476260
        Me.txtDDD.Tag = "NE;"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.Location = New System.Drawing.Point(543, 181)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(66, 13)
        Me.Label36.TabIndex = 476263
        Me.Label36.Text = "Due Date"
        '
        'txtPayableCash
        '
        Me.txtPayableCash.BackColor = System.Drawing.SystemColors.Window
        Me.txtPayableCash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPayableCash.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayableCash.Location = New System.Drawing.Point(109, 179)
        Me.txtPayableCash.MaxLength = 20
        Me.txtPayableCash.Name = "txtPayableCash"
        Me.txtPayableCash.Size = New System.Drawing.Size(113, 20)
        Me.txtPayableCash.TabIndex = 476258
        Me.txtPayableCash.Tag = "NE;"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(9, 179)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(94, 13)
        Me.Label33.TabIndex = 476259
        Me.Label33.Text = "Payable Cash"
        '
        'optBoth
        '
        Me.optBoth.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optBoth.Location = New System.Drawing.Point(104, 156)
        Me.optBoth.Name = "optBoth"
        Me.optBoth.Size = New System.Drawing.Size(56, 16)
        Me.optBoth.TabIndex = 476257
        Me.optBoth.Text = "Both"
        '
        'btnTaelPercent
        '
        Me.btnTaelPercent.BackColor = System.Drawing.Color.Orange
        Me.btnTaelPercent.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTaelPercent.Location = New System.Drawing.Point(453, 110)
        Me.btnTaelPercent.Name = "btnTaelPercent"
        Me.btnTaelPercent.Size = New System.Drawing.Size(26, 20)
        Me.btnTaelPercent.TabIndex = 476256
        Me.btnTaelPercent.Text = "..."
        Me.btnTaelPercent.UseVisualStyleBackColor = False
        '
        'cboTealPercent
        '
        Me.cboTealPercent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboTealPercent.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTealPercent.Location = New System.Drawing.Point(415, 110)
        Me.cboTealPercent.MaxLength = 20
        Me.cboTealPercent.Name = "cboTealPercent"
        Me.cboTealPercent.Size = New System.Drawing.Size(48, 20)
        Me.cboTealPercent.TabIndex = 476255
        Me.cboTealPercent.Tag = "NE;"
        '
        'btnSalesOrderNo
        '
        Me.btnSalesOrderNo.BackColor = System.Drawing.Color.Orange
        Me.btnSalesOrderNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSalesOrderNo.Location = New System.Drawing.Point(633, 46)
        Me.btnSalesOrderNo.Name = "btnSalesOrderNo"
        Me.btnSalesOrderNo.Size = New System.Drawing.Size(28, 20)
        Me.btnSalesOrderNo.TabIndex = 476254
        Me.btnSalesOrderNo.Text = "..."
        Me.btnSalesOrderNo.UseVisualStyleBackColor = False
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(523, 50)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(45, 13)
        Me.Label31.TabIndex = 476253
        Me.Label31.Text = "SO No"
        '
        'txtSalesOrderNo
        '
        Me.txtSalesOrderNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesOrderNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesOrderNo.Location = New System.Drawing.Point(568, 46)
        Me.txtSalesOrderNo.MaxLength = 20
        Me.txtSalesOrderNo.Name = "txtSalesOrderNo"
        Me.txtSalesOrderNo.Size = New System.Drawing.Size(88, 20)
        Me.txtSalesOrderNo.TabIndex = 476253
        Me.txtSalesOrderNo.Tag = "NE;"
        '
        'btnCurrencyCodeSearch
        '
        Me.btnCurrencyCodeSearch.BackColor = System.Drawing.Color.Orange
        Me.btnCurrencyCodeSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCurrencyCodeSearch.Location = New System.Drawing.Point(194, 78)
        Me.btnCurrencyCodeSearch.Name = "btnCurrencyCodeSearch"
        Me.btnCurrencyCodeSearch.Size = New System.Drawing.Size(28, 20)
        Me.btnCurrencyCodeSearch.TabIndex = 476251
        Me.btnCurrencyCodeSearch.Text = "..."
        Me.btnCurrencyCodeSearch.UseVisualStyleBackColor = False
        '
        'btnSupplierCodeSearch
        '
        Me.btnSupplierCodeSearch.BackColor = System.Drawing.Color.Orange
        Me.btnSupplierCodeSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSupplierCodeSearch.Location = New System.Drawing.Point(194, 46)
        Me.btnSupplierCodeSearch.Name = "btnSupplierCodeSearch"
        Me.btnSupplierCodeSearch.Size = New System.Drawing.Size(28, 20)
        Me.btnSupplierCodeSearch.TabIndex = 476250
        Me.btnSupplierCodeSearch.Text = "..."
        Me.btnSupplierCodeSearch.UseVisualStyleBackColor = False
        '
        'btnJobSheetNoSearch
        '
        Me.btnJobSheetNoSearch.BackColor = System.Drawing.Color.Orange
        Me.btnJobSheetNoSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnJobSheetNoSearch.Location = New System.Drawing.Point(194, 16)
        Me.btnJobSheetNoSearch.Name = "btnJobSheetNoSearch"
        Me.btnJobSheetNoSearch.Size = New System.Drawing.Size(28, 20)
        Me.btnJobSheetNoSearch.TabIndex = 476249
        Me.btnJobSheetNoSearch.Text = "..."
        Me.btnJobSheetNoSearch.UseVisualStyleBackColor = False
        '
        'txtCurrencyCode
        '
        Me.txtCurrencyCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrencyCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencyCode.Location = New System.Drawing.Point(110, 78)
        Me.txtCurrencyCode.MaxLength = 20
        Me.txtCurrencyCode.Name = "txtCurrencyCode"
        Me.txtCurrencyCode.Size = New System.Drawing.Size(112, 20)
        Me.txtCurrencyCode.TabIndex = 7
        Me.txtCurrencyCode.Tag = "NE;"
        '
        'txtSupplierCode
        '
        Me.txtSupplierCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierCode.Location = New System.Drawing.Point(110, 46)
        Me.txtSupplierCode.MaxLength = 20
        Me.txtSupplierCode.Name = "txtSupplierCode"
        Me.txtSupplierCode.Size = New System.Drawing.Size(112, 20)
        Me.txtSupplierCode.TabIndex = 5
        Me.txtSupplierCode.Tag = "NE;"
        '
        'txtJobSheetNo
        '
        Me.txtJobSheetNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJobSheetNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJobSheetNo.Location = New System.Drawing.Point(110, 16)
        Me.txtJobSheetNo.MaxLength = 20
        Me.txtJobSheetNo.Name = "txtJobSheetNo"
        Me.txtJobSheetNo.Size = New System.Drawing.Size(112, 20)
        Me.txtJobSheetNo.TabIndex = 0
        Me.txtJobSheetNo.Tag = "NE;"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.optUnFix)
        Me.GroupBox6.Controls.Add(Me.optFix)
        Me.GroupBox6.Location = New System.Drawing.Point(500, 134)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(155, 36)
        Me.GroupBox6.TabIndex = 3853
        Me.GroupBox6.TabStop = False
        '
        'optUnFix
        '
        Me.optUnFix.Checked = True
        Me.optUnFix.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optUnFix.Location = New System.Drawing.Point(74, 13)
        Me.optUnFix.Name = "optUnFix"
        Me.optUnFix.Size = New System.Drawing.Size(72, 20)
        Me.optUnFix.TabIndex = 3853
        Me.optUnFix.TabStop = True
        Me.optUnFix.Text = "UnFix"
        '
        'optFix
        '
        Me.optFix.Enabled = False
        Me.optFix.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optFix.Location = New System.Drawing.Point(12, 13)
        Me.optFix.Name = "optFix"
        Me.optFix.Size = New System.Drawing.Size(56, 16)
        Me.optFix.TabIndex = 3851
        Me.optFix.Text = "Fix"
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(560, 108)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(55, 29)
        Me.Label10.TabIndex = 3850
        Me.Label10.Text = "Cost Per Gram"
        '
        'cboTealPercent1
        '
        Me.cboTealPercent1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTealPercent1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTealPercent1.FormattingEnabled = True
        Me.cboTealPercent1.Items.AddRange(New Object() {".92", ".925", ".93", ".935", ".999"})
        Me.cboTealPercent1.Location = New System.Drawing.Point(359, 140)
        Me.cboTealPercent1.Name = "cboTealPercent1"
        Me.cboTealPercent1.Size = New System.Drawing.Size(51, 21)
        Me.cboTealPercent1.TabIndex = 3849
        Me.cboTealPercent1.Visible = False
        '
        'Label32
        '
        Me.Label32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.Color.Black
        Me.Label32.Location = New System.Drawing.Point(366, 109)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(66, 29)
        Me.Label32.TabIndex = 3848
        Me.Label32.Text = "TEAL Percent"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.opt24K)
        Me.GroupBox5.Controls.Add(Me.opt22K)
        Me.GroupBox5.Location = New System.Drawing.Point(263, 104)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(98, 32)
        Me.GroupBox5.TabIndex = 3847
        Me.GroupBox5.TabStop = False
        '
        'opt24K
        '
        Me.opt24K.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.opt24K.Location = New System.Drawing.Point(49, 10)
        Me.opt24K.Name = "opt24K"
        Me.opt24K.Size = New System.Drawing.Size(41, 16)
        Me.opt24K.TabIndex = 12
        Me.opt24K.Text = "24K"
        '
        'opt22K
        '
        Me.opt22K.Checked = True
        Me.opt22K.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.opt22K.Location = New System.Drawing.Point(8, 10)
        Me.opt22K.Name = "opt22K"
        Me.opt22K.Size = New System.Drawing.Size(48, 16)
        Me.opt22K.TabIndex = 11
        Me.opt22K.TabStop = True
        Me.opt22K.Text = "22K"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(618, 18)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(11, 14)
        Me.Label27.TabIndex = 3846
        Me.Label27.Text = "-"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(578, 19)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(11, 14)
        Me.Label26.TabIndex = 3845
        Me.Label26.Text = "-"
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(299, 179)
        Me.txtRemarks.MaxLength = 50
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(210, 20)
        Me.txtRemarks.TabIndex = 15
        Me.txtRemarks.Tag = "NE;"
        '
        'txtTAEL
        '
        Me.txtTAEL.BackColor = System.Drawing.SystemColors.Window
        Me.txtTAEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTAEL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTAEL.Location = New System.Drawing.Point(516, 112)
        Me.txtTAEL.MaxLength = 20
        Me.txtTAEL.Name = "txtTAEL"
        Me.txtTAEL.Size = New System.Drawing.Size(43, 20)
        Me.txtTAEL.TabIndex = 13
        Me.txtTAEL.Tag = "NE;"
        Me.txtTAEL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCostPerGram
        '
        Me.txtCostPerGram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCostPerGram.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostPerGram.Location = New System.Drawing.Point(621, 112)
        Me.txtCostPerGram.MaxLength = 20
        Me.txtCostPerGram.Name = "txtCostPerGram"
        Me.txtCostPerGram.Size = New System.Drawing.Size(34, 20)
        Me.txtCostPerGram.TabIndex = 14
        Me.txtCostPerGram.Tag = "NE;"
        Me.txtCostPerGram.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCurrencyRate
        '
        Me.txtCurrencyRate.BackColor = System.Drawing.SystemColors.Window
        Me.txtCurrencyRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrencyRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencyRate.Location = New System.Drawing.Point(576, 78)
        Me.txtCurrencyRate.MaxLength = 20
        Me.txtCurrencyRate.Name = "txtCurrencyRate"
        Me.txtCurrencyRate.ReadOnly = True
        Me.txtCurrencyRate.Size = New System.Drawing.Size(80, 20)
        Me.txtCurrencyRate.TabIndex = 9
        Me.txtCurrencyRate.Tag = "NE;"
        '
        'txtSupplierName
        '
        Me.txtSupplierName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierName.Location = New System.Drawing.Point(232, 46)
        Me.txtSupplierName.MaxLength = 20
        Me.txtSupplierName.Name = "txtSupplierName"
        Me.txtSupplierName.Size = New System.Drawing.Size(288, 20)
        Me.txtSupplierName.TabIndex = 6
        Me.txtSupplierName.TabStop = False
        Me.txtSupplierName.Tag = "NE;"
        '
        'txtCurrencyName
        '
        Me.txtCurrencyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrencyName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencyName.Location = New System.Drawing.Point(232, 78)
        Me.txtCurrencyName.MaxLength = 20
        Me.txtCurrencyName.Name = "txtCurrencyName"
        Me.txtCurrencyName.Size = New System.Drawing.Size(288, 20)
        Me.txtCurrencyName.TabIndex = 8
        Me.txtCurrencyName.TabStop = False
        Me.txtCurrencyName.Tag = "NE;"
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvoiceNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceNo.Location = New System.Drawing.Point(306, 16)
        Me.txtInvoiceNo.MaxLength = 20
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(126, 20)
        Me.txtInvoiceNo.TabIndex = 1
        Me.txtInvoiceNo.Tag = "NE;"
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(680, 172)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(207, 4)
        Me.GroupBox3.TabIndex = 535
        Me.GroupBox3.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox2.Controls.Add(Me.txtGoldValueMC)
        Me.GroupBox2.Controls.Add(Me.Label25)
        Me.GroupBox2.Controls.Add(Me.txtAdjustment)
        Me.GroupBox2.Controls.Add(Me.Label24)
        Me.GroupBox2.Controls.Add(Me.txtNetCost)
        Me.GroupBox2.Controls.Add(Me.labe24)
        Me.GroupBox2.Controls.Add(Me.txtManualGoldLoss)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.txtManualWeight)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.ChkGST)
        Me.GroupBox2.Controls.Add(Me.txtGST)
        Me.GroupBox2.Controls.Add(Me.txtManualMC)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.txtManualCost)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Location = New System.Drawing.Point(680, 9)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(208, 167)
        Me.GroupBox2.TabIndex = 200
        Me.GroupBox2.TabStop = False
        '
        'txtGoldValueMC
        '
        Me.txtGoldValueMC.BackColor = System.Drawing.SystemColors.Control
        Me.txtGoldValueMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldValueMC.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoldValueMC.ForeColor = System.Drawing.Color.Black
        Me.txtGoldValueMC.Location = New System.Drawing.Point(104, 86)
        Me.txtGoldValueMC.MaxLength = 20
        Me.txtGoldValueMC.Name = "txtGoldValueMC"
        Me.txtGoldValueMC.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtGoldValueMC.Size = New System.Drawing.Size(96, 22)
        Me.txtGoldValueMC.TabIndex = 60
        Me.txtGoldValueMC.TabStop = False
        Me.txtGoldValueMC.Tag = "NE;"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(3, 88)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(99, 13)
        Me.Label25.TabIndex = 59
        Me.Label25.Text = "GoldValue+MC"
        '
        'txtAdjustment
        '
        Me.txtAdjustment.BackColor = System.Drawing.SystemColors.Control
        Me.txtAdjustment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdjustment.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdjustment.ForeColor = System.Drawing.Color.Black
        Me.txtAdjustment.Location = New System.Drawing.Point(104, 62)
        Me.txtAdjustment.MaxLength = 20
        Me.txtAdjustment.Name = "txtAdjustment"
        Me.txtAdjustment.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtAdjustment.Size = New System.Drawing.Size(96, 22)
        Me.txtAdjustment.TabIndex = 58
        Me.txtAdjustment.TabStop = False
        Me.txtAdjustment.Tag = "NE;"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(5, 64)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(82, 13)
        Me.Label24.TabIndex = 57
        Me.Label24.Text = "Adjustment"
        '
        'txtNetCost
        '
        Me.txtNetCost.BackColor = System.Drawing.SystemColors.Control
        Me.txtNetCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNetCost.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetCost.ForeColor = System.Drawing.Color.Black
        Me.txtNetCost.Location = New System.Drawing.Point(104, 10)
        Me.txtNetCost.MaxLength = 20
        Me.txtNetCost.Name = "txtNetCost"
        Me.txtNetCost.ReadOnly = True
        Me.txtNetCost.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtNetCost.Size = New System.Drawing.Size(96, 22)
        Me.txtNetCost.TabIndex = 56
        Me.txtNetCost.TabStop = False
        Me.txtNetCost.Tag = "NE;"
        '
        'labe24
        '
        Me.labe24.AutoSize = True
        Me.labe24.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labe24.Location = New System.Drawing.Point(6, 12)
        Me.labe24.Name = "labe24"
        Me.labe24.Size = New System.Drawing.Size(76, 13)
        Me.labe24.TabIndex = 55
        Me.labe24.Text = "Gold Value"
        '
        'txtManualGoldLoss
        '
        Me.txtManualGoldLoss.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtManualGoldLoss.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtManualGoldLoss.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtManualGoldLoss.Location = New System.Drawing.Point(120, 135)
        Me.txtManualGoldLoss.MaxLength = 20
        Me.txtManualGoldLoss.Name = "txtManualGoldLoss"
        Me.txtManualGoldLoss.ReadOnly = True
        Me.txtManualGoldLoss.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtManualGoldLoss.Size = New System.Drawing.Size(72, 22)
        Me.txtManualGoldLoss.TabIndex = 36
        Me.txtManualGoldLoss.Tag = "NE;"
        Me.txtManualGoldLoss.Visible = False
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(120, 111)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(65, 17)
        Me.Label14.TabIndex = 54
        Me.Label14.Text = "Gold Loss"
        Me.Label14.Visible = False
        '
        'txtManualWeight
        '
        Me.txtManualWeight.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtManualWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtManualWeight.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtManualWeight.Location = New System.Drawing.Point(42, 135)
        Me.txtManualWeight.MaxLength = 20
        Me.txtManualWeight.Name = "txtManualWeight"
        Me.txtManualWeight.ReadOnly = True
        Me.txtManualWeight.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtManualWeight.Size = New System.Drawing.Size(72, 22)
        Me.txtManualWeight.TabIndex = 35
        Me.txtManualWeight.Tag = "NE;"
        Me.txtManualWeight.Visible = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(8, 111)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(52, 13)
        Me.Label13.TabIndex = 52
        Me.Label13.Text = "Weight"
        Me.Label13.Visible = False
        '
        'ChkGST
        '
        Me.ChkGST.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ChkGST.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkGST.Location = New System.Drawing.Point(4, 111)
        Me.ChkGST.Name = "ChkGST"
        Me.ChkGST.Size = New System.Drawing.Size(56, 24)
        Me.ChkGST.TabIndex = 50
        Me.ChkGST.Text = " GST  "
        '
        'txtGST
        '
        Me.txtGST.BackColor = System.Drawing.SystemColors.Control
        Me.txtGST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGST.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGST.ForeColor = System.Drawing.Color.Black
        Me.txtGST.Location = New System.Drawing.Point(104, 114)
        Me.txtGST.MaxLength = 20
        Me.txtGST.Name = "txtGST"
        Me.txtGST.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtGST.Size = New System.Drawing.Size(96, 22)
        Me.txtGST.TabIndex = 34
        Me.txtGST.TabStop = False
        Me.txtGST.Tag = "NE;"
        '
        'txtManualMC
        '
        Me.txtManualMC.BackColor = System.Drawing.SystemColors.Control
        Me.txtManualMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtManualMC.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtManualMC.ForeColor = System.Drawing.Color.Black
        Me.txtManualMC.Location = New System.Drawing.Point(104, 37)
        Me.txtManualMC.MaxLength = 20
        Me.txtManualMC.Name = "txtManualMC"
        Me.txtManualMC.ReadOnly = True
        Me.txtManualMC.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtManualMC.Size = New System.Drawing.Size(96, 22)
        Me.txtManualMC.TabIndex = 33
        Me.txtManualMC.TabStop = False
        Me.txtManualMC.Tag = "NE;"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(6, 39)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(62, 13)
        Me.Label12.TabIndex = 31
        Me.Label12.Text = "Total MC"
        '
        'txtManualCost
        '
        Me.txtManualCost.BackColor = System.Drawing.SystemColors.Control
        Me.txtManualCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtManualCost.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtManualCost.ForeColor = System.Drawing.Color.Black
        Me.txtManualCost.Location = New System.Drawing.Point(104, 139)
        Me.txtManualCost.MaxLength = 20
        Me.txtManualCost.Name = "txtManualCost"
        Me.txtManualCost.ReadOnly = True
        Me.txtManualCost.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtManualCost.Size = New System.Drawing.Size(96, 22)
        Me.txtManualCost.TabIndex = 32
        Me.txtManualCost.TabStop = False
        Me.txtManualCost.Tag = "NE;"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(5, 142)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(72, 13)
        Me.Label11.TabIndex = 29
        Me.Label11.Text = "Total Cost"
        '
        'ChkGST1
        '
        Me.ChkGST1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ChkGST1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkGST1.Location = New System.Drawing.Point(208, 109)
        Me.ChkGST1.Name = "ChkGST1"
        Me.ChkGST1.Size = New System.Drawing.Size(56, 24)
        Me.ChkGST1.TabIndex = 12
        Me.ChkGST1.Text = "GST"
        Me.ChkGST1.UseVisualStyleBackColor = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(477, 115)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(39, 13)
        Me.Label9.TabIndex = 484
        Me.Label9.Text = "TAEL"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(229, 179)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(64, 13)
        Me.Label8.TabIndex = 464
        Me.Label8.Text = "Remarks"
        '
        'OptCash
        '
        Me.OptCash.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptCash.Location = New System.Drawing.Point(104, 134)
        Me.OptCash.Name = "OptCash"
        Me.OptCash.Size = New System.Drawing.Size(56, 16)
        Me.OptCash.TabIndex = 11
        Me.OptCash.Text = "Cash"
        '
        'OptGoldAC
        '
        Me.OptGoldAC.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptGoldAC.Location = New System.Drawing.Point(104, 112)
        Me.OptGoldAC.Name = "OptGoldAC"
        Me.OptGoldAC.Size = New System.Drawing.Size(56, 16)
        Me.OptGoldAC.TabIndex = 10
        Me.OptGoldAC.Text = "Gold"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(9, 112)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(59, 13)
        Me.Label7.TabIndex = 434
        Me.Label7.Text = "Payable"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(534, 80)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(36, 13)
        Me.Label6.TabIndex = 404
        Me.Label6.Text = "Rate"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(9, 80)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(102, 13)
        Me.Label5.TabIndex = 3844
        Me.Label5.Text = "Currency Code"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(9, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(97, 13)
        Me.Label4.TabIndex = 355
        Me.Label4.Text = "Supplier Code"
        '
        'txtYY
        '
        Me.txtYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtYY.Location = New System.Drawing.Point(626, 16)
        Me.txtYY.MaxLength = 20
        Me.txtYY.Name = "txtYY"
        Me.txtYY.Size = New System.Drawing.Size(32, 20)
        Me.txtYY.TabIndex = 4
        Me.txtYY.Tag = "NE;"
        '
        'txtMM
        '
        Me.txtMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMM.Location = New System.Drawing.Point(586, 16)
        Me.txtMM.MaxLength = 20
        Me.txtMM.Name = "txtMM"
        Me.txtMM.Size = New System.Drawing.Size(32, 20)
        Me.txtMM.TabIndex = 3
        Me.txtMM.Tag = "NE;"
        '
        'txtDD
        '
        Me.txtDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDD.Location = New System.Drawing.Point(546, 16)
        Me.txtDD.MaxLength = 20
        Me.txtDD.Name = "txtDD"
        Me.txtDD.Size = New System.Drawing.Size(32, 20)
        Me.txtDD.TabIndex = 2
        Me.txtDD.Tag = "NE;"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(450, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(90, 13)
        Me.Label3.TabIndex = 310
        Me.Label3.Text = "Invoice Date"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(232, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 13)
        Me.Label2.TabIndex = 2955
        Me.Label2.Text = "Invoice No"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 13)
        Me.Label1.TabIndex = 2564
        Me.Label1.Text = "JobSheet No"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.txtDiaCost)
        Me.GroupBox4.Controls.Add(Me.Label37)
        Me.GroupBox4.Controls.Add(Me.btnCatBDelete)
        Me.GroupBox4.Controls.Add(Me.btnCatalogueCode)
        Me.GroupBox4.Controls.Add(Me.txtCatalogueCode)
        Me.GroupBox4.Controls.Add(Me.btnCCategoryCodeSearch)
        Me.GroupBox4.Controls.Add(Me.txtCCategoryCode)
        Me.GroupBox4.Controls.Add(Me.txtCategoryName)
        Me.GroupBox4.Controls.Add(Me.btnCatAdd)
        Me.GroupBox4.Controls.Add(Me.btnCatDelete)
        Me.GroupBox4.Controls.Add(Me.btnCatClear)
        Me.GroupBox4.Controls.Add(Me.txtTotalCost)
        Me.GroupBox4.Controls.Add(Me.Label23)
        Me.GroupBox4.Controls.Add(Me.txtPCS)
        Me.GroupBox4.Controls.Add(Me.Label22)
        Me.GroupBox4.Controls.Add(Me.txtMC)
        Me.GroupBox4.Controls.Add(Me.Label21)
        Me.GroupBox4.Controls.Add(Me.txtCostPerGram1)
        Me.GroupBox4.Controls.Add(Me.Label20)
        Me.GroupBox4.Controls.Add(Me.txtGoldLoss)
        Me.GroupBox4.Controls.Add(Me.Label19)
        Me.GroupBox4.Controls.Add(Me.txtWeight)
        Me.GroupBox4.Controls.Add(Me.Label18)
        Me.GroupBox4.Controls.Add(Me.txCatalogueName)
        Me.GroupBox4.Controls.Add(Me.Label17)
        Me.GroupBox4.Controls.Add(Me.txtCategoryCode)
        Me.GroupBox4.Controls.Add(Me.Label16)
        Me.GroupBox4.Controls.Add(Me.Label15)
        Me.GroupBox4.Location = New System.Drawing.Point(5, 469)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(900, 95)
        Me.GroupBox4.TabIndex = 14
        Me.GroupBox4.TabStop = False
        '
        'txtDiaCost
        '
        Me.txtDiaCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDiaCost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiaCost.Location = New System.Drawing.Point(670, 35)
        Me.txtDiaCost.MaxLength = 20
        Me.txtDiaCost.Name = "txtDiaCost"
        Me.txtDiaCost.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtDiaCost.Size = New System.Drawing.Size(64, 20)
        Me.txtDiaCost.TabIndex = 476254
        Me.txtDiaCost.Tag = "NE;"
        Me.txtDiaCost.Visible = False
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label37.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label37.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.Color.Black
        Me.Label37.Location = New System.Drawing.Point(670, 15)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(64, 16)
        Me.Label37.TabIndex = 476255
        Me.Label37.Text = "Dia Cost"
        Me.Label37.Visible = False
        '
        'btnCatBDelete
        '
        Me.btnCatBDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCatBDelete.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCatBDelete.Location = New System.Drawing.Point(607, 63)
        Me.btnCatBDelete.Name = "btnCatBDelete"
        Me.btnCatBDelete.Size = New System.Drawing.Size(109, 23)
        Me.btnCatBDelete.TabIndex = 476253
        Me.btnCatBDelete.Text = "BeforeDelete"
        Me.btnCatBDelete.UseVisualStyleBackColor = False
        '
        'btnCatalogueCode
        '
        Me.btnCatalogueCode.BackColor = System.Drawing.Color.Orange
        Me.btnCatalogueCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCatalogueCode.Location = New System.Drawing.Point(83, 35)
        Me.btnCatalogueCode.Name = "btnCatalogueCode"
        Me.btnCatalogueCode.Size = New System.Drawing.Size(27, 20)
        Me.btnCatalogueCode.TabIndex = 476252
        Me.btnCatalogueCode.Text = "..."
        Me.btnCatalogueCode.UseVisualStyleBackColor = False
        '
        'txtCatalogueCode
        '
        Me.txtCatalogueCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCatalogueCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCatalogueCode.Location = New System.Drawing.Point(8, 35)
        Me.txtCatalogueCode.MaxLength = 20
        Me.txtCatalogueCode.Name = "txtCatalogueCode"
        Me.txtCatalogueCode.Size = New System.Drawing.Size(99, 20)
        Me.txtCatalogueCode.TabIndex = 476251
        Me.txtCatalogueCode.Tag = "NE;"
        '
        'btnCCategoryCodeSearch
        '
        Me.btnCCategoryCodeSearch.BackColor = System.Drawing.Color.Orange
        Me.btnCCategoryCodeSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCCategoryCodeSearch.Location = New System.Drawing.Point(264, 62)
        Me.btnCCategoryCodeSearch.Name = "btnCCategoryCodeSearch"
        Me.btnCCategoryCodeSearch.Size = New System.Drawing.Size(28, 20)
        Me.btnCCategoryCodeSearch.TabIndex = 476250
        Me.btnCCategoryCodeSearch.Text = "..."
        Me.btnCCategoryCodeSearch.UseVisualStyleBackColor = False
        Me.btnCCategoryCodeSearch.Visible = False
        '
        'txtCCategoryCode
        '
        Me.txtCCategoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCCategoryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCCategoryCode.Location = New System.Drawing.Point(166, 63)
        Me.txtCCategoryCode.MaxLength = 20
        Me.txtCCategoryCode.Name = "txtCCategoryCode"
        Me.txtCCategoryCode.Size = New System.Drawing.Size(124, 20)
        Me.txtCCategoryCode.TabIndex = 17
        Me.txtCCategoryCode.Tag = "NE;"
        Me.txtCCategoryCode.Visible = False
        '
        'txtCategoryName
        '
        Me.txtCategoryName.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCategoryName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCategoryName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCategoryName.Location = New System.Drawing.Point(152, 61)
        Me.txtCategoryName.MaxLength = 20
        Me.txtCategoryName.Name = "txtCategoryName"
        Me.txtCategoryName.ReadOnly = True
        Me.txtCategoryName.Size = New System.Drawing.Size(310, 20)
        Me.txtCategoryName.TabIndex = 18
        Me.txtCategoryName.Tag = "NE;"
        Me.txtCategoryName.Visible = False
        '
        'btnCatAdd
        '
        Me.btnCatAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCatAdd.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCatAdd.Location = New System.Drawing.Point(526, 63)
        Me.btnCatAdd.Name = "btnCatAdd"
        Me.btnCatAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnCatAdd.TabIndex = 25
        Me.btnCatAdd.Text = "Add "
        Me.btnCatAdd.UseVisualStyleBackColor = False
        '
        'btnCatDelete
        '
        Me.btnCatDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCatDelete.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCatDelete.Location = New System.Drawing.Point(722, 63)
        Me.btnCatDelete.Name = "btnCatDelete"
        Me.btnCatDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnCatDelete.TabIndex = 26
        Me.btnCatDelete.Text = "Delete"
        Me.btnCatDelete.UseVisualStyleBackColor = False
        '
        'btnCatClear
        '
        Me.btnCatClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCatClear.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCatClear.Location = New System.Drawing.Point(803, 63)
        Me.btnCatClear.Name = "btnCatClear"
        Me.btnCatClear.Size = New System.Drawing.Size(75, 23)
        Me.btnCatClear.TabIndex = 27
        Me.btnCatClear.Text = "Clear"
        Me.btnCatClear.UseVisualStyleBackColor = False
        '
        'txtTotalCost
        '
        Me.txtTotalCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalCost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalCost.Location = New System.Drawing.Point(803, 35)
        Me.txtTotalCost.MaxLength = 20
        Me.txtTotalCost.Name = "txtTotalCost"
        Me.txtTotalCost.ReadOnly = True
        Me.txtTotalCost.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtTotalCost.Size = New System.Drawing.Size(94, 20)
        Me.txtTotalCost.TabIndex = 24
        Me.txtTotalCost.Tag = "NE;"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label23.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.Black
        Me.Label23.Location = New System.Drawing.Point(803, 15)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(94, 16)
        Me.Label23.TabIndex = 44
        Me.Label23.Text = "Total Cost"
        '
        'txtPCS
        '
        Me.txtPCS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPCS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPCS.Location = New System.Drawing.Point(705, 35)
        Me.txtPCS.MaxLength = 20
        Me.txtPCS.Name = "txtPCS"
        Me.txtPCS.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtPCS.Size = New System.Drawing.Size(92, 20)
        Me.txtPCS.TabIndex = 23
        Me.txtPCS.Tag = "NE;"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label22.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.Black
        Me.Label22.Location = New System.Drawing.Point(705, 15)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(92, 16)
        Me.Label22.TabIndex = 42
        Me.Label22.Text = "PCS"
        '
        'txtMC
        '
        Me.txtMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMC.Location = New System.Drawing.Point(600, 35)
        Me.txtMC.MaxLength = 20
        Me.txtMC.Name = "txtMC"
        Me.txtMC.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtMC.Size = New System.Drawing.Size(82, 20)
        Me.txtMC.TabIndex = 22
        Me.txtMC.Tag = "NE;"
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label21.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Black
        Me.Label21.Location = New System.Drawing.Point(600, 15)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(82, 16)
        Me.Label21.TabIndex = 40
        Me.Label21.Text = "MC"
        '
        'txtCostPerGram1
        '
        Me.txtCostPerGram1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCostPerGram1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostPerGram1.Location = New System.Drawing.Point(527, 35)
        Me.txtCostPerGram1.MaxLength = 20
        Me.txtCostPerGram1.Name = "txtCostPerGram1"
        Me.txtCostPerGram1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtCostPerGram1.Size = New System.Drawing.Size(67, 20)
        Me.txtCostPerGram1.TabIndex = 21
        Me.txtCostPerGram1.Tag = "NE;"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label20.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.Location = New System.Drawing.Point(527, 15)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(67, 16)
        Me.Label20.TabIndex = 38
        Me.Label20.Text = "Cost/Gm"
        '
        'txtGoldLoss
        '
        Me.txtGoldLoss.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldLoss.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoldLoss.Location = New System.Drawing.Point(527, 35)
        Me.txtGoldLoss.MaxLength = 20
        Me.txtGoldLoss.Name = "txtGoldLoss"
        Me.txtGoldLoss.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtGoldLoss.Size = New System.Drawing.Size(59, 20)
        Me.txtGoldLoss.TabIndex = 20
        Me.txtGoldLoss.Tag = "NE;"
        Me.txtGoldLoss.Visible = False
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label19.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(527, 15)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(59, 16)
        Me.Label19.TabIndex = 36
        Me.Label19.Text = "Gold Loss"
        Me.Label19.Visible = False
        '
        'txtWeight
        '
        Me.txtWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeight.Location = New System.Drawing.Point(454, 35)
        Me.txtWeight.MaxLength = 20
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtWeight.Size = New System.Drawing.Size(64, 20)
        Me.txtWeight.TabIndex = 19
        Me.txtWeight.Tag = "NE;"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label18.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(454, 15)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(64, 16)
        Me.Label18.TabIndex = 34
        Me.Label18.Text = "Weight"
        '
        'txCatalogueName
        '
        Me.txCatalogueName.BackColor = System.Drawing.SystemColors.Window
        Me.txCatalogueName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txCatalogueName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txCatalogueName.Location = New System.Drawing.Point(229, 35)
        Me.txCatalogueName.MaxLength = 20
        Me.txCatalogueName.Name = "txCatalogueName"
        Me.txCatalogueName.ReadOnly = True
        Me.txCatalogueName.Size = New System.Drawing.Size(219, 20)
        Me.txCatalogueName.TabIndex = 18
        Me.txCatalogueName.Tag = "NE;"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label17.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(229, 15)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(219, 16)
        Me.Label17.TabIndex = 32
        Me.Label17.Text = "Description"
        '
        'txtCategoryCode
        '
        Me.txtCategoryCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtCategoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCategoryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCategoryCode.Location = New System.Drawing.Point(117, 35)
        Me.txtCategoryCode.MaxLength = 20
        Me.txtCategoryCode.Name = "txtCategoryCode"
        Me.txtCategoryCode.ReadOnly = True
        Me.txtCategoryCode.Size = New System.Drawing.Size(105, 20)
        Me.txtCategoryCode.TabIndex = 17
        Me.txtCategoryCode.Tag = "NE;"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label16.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(117, 15)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(105, 16)
        Me.Label16.TabIndex = 30
        Me.Label16.Text = "Category Code"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label15.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(8, 15)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(102, 16)
        Me.Label15.TabIndex = 28
        Me.Label15.Text = "Catalogue Code"
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClear.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.Location = New System.Drawing.Point(485, 570)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(100, 40)
        Me.btnClear.TabIndex = 30
        Me.btnClear.Text = "Clear(F6)"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(803, 570)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 31
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(591, 570)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 40)
        Me.btnSave.TabIndex = 28
        Me.btnSave.Text = "Save(F7)"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDelete.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(697, 570)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(100, 40)
        Me.btnDelete.TabIndex = 29
        Me.btnDelete.Text = "Delete(F8)"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(5, 8)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 100)
        Me.pic.TabIndex = 568
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
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdJobSheet.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdJobSheet.ColumnHeadersHeight = 35
        Me.grdJobSheet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdJobSheet.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cCataCode, Me.cCategCode, Me.cDesc, Me.cWt, Me.cGdLoss, Me.cCostPerGm, Me.cMCJ, Me.cPCS, Me.cTotal})
        Me.grdJobSheet.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdJobSheet.Location = New System.Drawing.Point(5, 319)
        Me.grdJobSheet.Name = "grdJobSheet"
        Me.grdJobSheet.RowHeadersVisible = False
        Me.grdJobSheet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdJobSheet.ShowCellErrors = False
        Me.grdJobSheet.ShowCellToolTips = False
        Me.grdJobSheet.ShowEditingIcon = False
        Me.grdJobSheet.ShowRowErrors = False
        Me.grdJobSheet.Size = New System.Drawing.Size(900, 138)
        Me.grdJobSheet.TabIndex = 569
        '
        'cCataCode
        '
        Me.cCataCode.HeaderText = "Catalogue Code"
        Me.cCataCode.Name = "cCataCode"
        '
        'cCategCode
        '
        Me.cCategCode.HeaderText = "Category Code"
        Me.cCategCode.Name = "cCategCode"
        '
        'cDesc
        '
        Me.cDesc.HeaderText = "Description"
        Me.cDesc.Name = "cDesc"
        Me.cDesc.Width = 280
        '
        'cWt
        '
        Me.cWt.HeaderText = "Weight (gm)"
        Me.cWt.Name = "cWt"
        Me.cWt.Width = 80
        '
        'cGdLoss
        '
        Me.cGdLoss.HeaderText = "Gold Loss"
        Me.cGdLoss.Name = "cGdLoss"
        Me.cGdLoss.Visible = False
        Me.cGdLoss.Width = 80
        '
        'cCostPerGm
        '
        Me.cCostPerGm.HeaderText = "Cost Per Gram"
        Me.cCostPerGm.Name = "cCostPerGm"
        Me.cCostPerGm.Width = 80
        '
        'cMCJ
        '
        Me.cMCJ.HeaderText = "MC"
        Me.cMCJ.Name = "cMCJ"
        Me.cMCJ.Width = 80
        '
        'cPCS
        '
        Me.cPCS.HeaderText = "PCS"
        Me.cPCS.Name = "cPCS"
        Me.cPCS.Width = 80
        '
        'cTotal
        '
        Me.cTotal.HeaderText = "Total Cost"
        Me.cTotal.Name = "cTotal"
        Me.cTotal.Width = 80
        '
        'chkSplCategory
        '
        Me.chkSplCategory.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkSplCategory.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSplCategory.Location = New System.Drawing.Point(413, 254)
        Me.chkSplCategory.Name = "chkSplCategory"
        Me.chkSplCategory.Size = New System.Drawing.Size(86, 33)
        Me.chkSplCategory.TabIndex = 570
        Me.chkSplCategory.Text = "Spl. Category"
        Me.chkSplCategory.UseVisualStyleBackColor = False
        '
        'chkAccessories
        '
        Me.chkAccessories.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkAccessories.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAccessories.Location = New System.Drawing.Point(281, 256)
        Me.chkAccessories.Name = "chkAccessories"
        Me.chkAccessories.Size = New System.Drawing.Size(111, 25)
        Me.chkAccessories.TabIndex = 571
        Me.chkAccessories.Text = "Accessories"
        Me.chkAccessories.UseVisualStyleBackColor = False
        '
        'grpMatchJobsheet
        '
        Me.grpMatchJobsheet.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpMatchJobsheet.Controls.Add(Me.Button1)
        Me.grpMatchJobsheet.Controls.Add(Me.dgvHeader)
        Me.grpMatchJobsheet.Controls.Add(Me.btnMatchJobsheetNo)
        Me.grpMatchJobsheet.Controls.Add(Me.txtMatchJobSheetNo)
        Me.grpMatchJobsheet.Controls.Add(Me.btnMatchCatalogue)
        Me.grpMatchJobsheet.Controls.Add(Me.Label28)
        Me.grpMatchJobsheet.Controls.Add(Me.txtMatchCatalogue)
        Me.grpMatchJobsheet.Controls.Add(Me.txtMatchPCS)
        Me.grpMatchJobsheet.Controls.Add(Me.Label30)
        Me.grpMatchJobsheet.Controls.Add(Me.txtMatchWeight)
        Me.grpMatchJobsheet.Controls.Add(Me.Label29)
        Me.grpMatchJobsheet.Location = New System.Drawing.Point(122, 351)
        Me.grpMatchJobsheet.Name = "grpMatchJobsheet"
        Me.grpMatchJobsheet.Size = New System.Drawing.Size(358, 201)
        Me.grpMatchJobsheet.TabIndex = 3854
        Me.grpMatchJobsheet.TabStop = False
        Me.grpMatchJobsheet.Visible = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Button1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(259, 143)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 476256
        Me.Button1.Text = "Add "
        Me.Button1.UseVisualStyleBackColor = False
        '
        'dgvHeader
        '
        Me.dgvHeader.AllowUserToAddRows = False
        Me.dgvHeader.AllowUserToDeleteRows = False
        Me.dgvHeader.AllowUserToResizeRows = False
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvHeader.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvHeader.ColumnHeadersHeight = 26
        Me.dgvHeader.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CatalogueCode, Me.Weight, Me.PCS})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvHeader.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvHeader.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvHeader.Location = New System.Drawing.Point(6, 54)
        Me.dgvHeader.Name = "dgvHeader"
        Me.dgvHeader.RowHeadersVisible = False
        Me.dgvHeader.Size = New System.Drawing.Size(306, 86)
        Me.dgvHeader.TabIndex = 476255
        '
        'CatalogueCode
        '
        Me.CatalogueCode.HeaderText = "Catalogue Code"
        Me.CatalogueCode.Name = "CatalogueCode"
        Me.CatalogueCode.Width = 120
        '
        'Weight
        '
        Me.Weight.HeaderText = "Weight"
        Me.Weight.Name = "Weight"
        '
        'PCS
        '
        Me.PCS.HeaderText = "PCS"
        Me.PCS.Name = "PCS"
        Me.PCS.Width = 80
        '
        'btnMatchJobsheetNo
        '
        Me.btnMatchJobsheetNo.BackColor = System.Drawing.Color.Orange
        Me.btnMatchJobsheetNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMatchJobsheetNo.Location = New System.Drawing.Point(200, 10)
        Me.btnMatchJobsheetNo.Name = "btnMatchJobsheetNo"
        Me.btnMatchJobsheetNo.Size = New System.Drawing.Size(28, 20)
        Me.btnMatchJobsheetNo.TabIndex = 476252
        Me.btnMatchJobsheetNo.Text = "..."
        Me.btnMatchJobsheetNo.UseVisualStyleBackColor = False
        '
        'txtMatchJobSheetNo
        '
        Me.txtMatchJobSheetNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMatchJobSheetNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMatchJobSheetNo.Location = New System.Drawing.Point(116, 10)
        Me.txtMatchJobSheetNo.MaxLength = 20
        Me.txtMatchJobSheetNo.Name = "txtMatchJobSheetNo"
        Me.txtMatchJobSheetNo.Size = New System.Drawing.Size(112, 20)
        Me.txtMatchJobSheetNo.TabIndex = 476250
        Me.txtMatchJobSheetNo.Tag = "NE;"
        '
        'btnMatchCatalogue
        '
        Me.btnMatchCatalogue.BackColor = System.Drawing.Color.Orange
        Me.btnMatchCatalogue.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMatchCatalogue.Location = New System.Drawing.Point(81, 146)
        Me.btnMatchCatalogue.Name = "btnMatchCatalogue"
        Me.btnMatchCatalogue.Size = New System.Drawing.Size(15, 20)
        Me.btnMatchCatalogue.TabIndex = 476254
        Me.btnMatchCatalogue.Text = "..."
        Me.btnMatchCatalogue.UseVisualStyleBackColor = False
        Me.btnMatchCatalogue.Visible = False
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label28.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(6, 12)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(108, 13)
        Me.Label28.TabIndex = 476251
        Me.Label28.Text = "Match JobSheet"
        '
        'txtMatchCatalogue
        '
        Me.txtMatchCatalogue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMatchCatalogue.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMatchCatalogue.Location = New System.Drawing.Point(18, 147)
        Me.txtMatchCatalogue.MaxLength = 20
        Me.txtMatchCatalogue.Name = "txtMatchCatalogue"
        Me.txtMatchCatalogue.Size = New System.Drawing.Size(78, 20)
        Me.txtMatchCatalogue.TabIndex = 476253
        Me.txtMatchCatalogue.Tag = "NE;"
        Me.txtMatchCatalogue.Visible = False
        '
        'txtMatchPCS
        '
        Me.txtMatchPCS.BackColor = System.Drawing.SystemColors.Window
        Me.txtMatchPCS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMatchPCS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMatchPCS.Location = New System.Drawing.Point(221, 147)
        Me.txtMatchPCS.MaxLength = 20
        Me.txtMatchPCS.Name = "txtMatchPCS"
        Me.txtMatchPCS.Size = New System.Drawing.Size(29, 20)
        Me.txtMatchPCS.TabIndex = 487
        Me.txtMatchPCS.Tag = "NE;"
        Me.txtMatchPCS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(189, 150)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(31, 13)
        Me.Label30.TabIndex = 488
        Me.Label30.Text = "PCS"
        '
        'txtMatchWeight
        '
        Me.txtMatchWeight.BackColor = System.Drawing.SystemColors.Window
        Me.txtMatchWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMatchWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMatchWeight.Location = New System.Drawing.Point(150, 146)
        Me.txtMatchWeight.MaxLength = 20
        Me.txtMatchWeight.Name = "txtMatchWeight"
        Me.txtMatchWeight.Size = New System.Drawing.Size(37, 20)
        Me.txtMatchWeight.TabIndex = 485
        Me.txtMatchWeight.Tag = "NE;"
        Me.txtMatchWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(99, 150)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(52, 13)
        Me.Label29.TabIndex = 486
        Me.Label29.Text = "Weight"
        '
        'chkImage
        '
        Me.chkImage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkImage.ForeColor = System.Drawing.Color.Black
        Me.chkImage.Location = New System.Drawing.Point(13, 570)
        Me.chkImage.Name = "chkImage"
        Me.chkImage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkImage.Size = New System.Drawing.Size(123, 18)
        Me.chkImage.TabIndex = 3855
        Me.chkImage.Text = "Match Jobsheet"
        '
        'btnGoldBarFix
        '
        Me.btnGoldBarFix.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnGoldBarFix.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGoldBarFix.Location = New System.Drawing.Point(379, 570)
        Me.btnGoldBarFix.Name = "btnGoldBarFix"
        Me.btnGoldBarFix.Size = New System.Drawing.Size(100, 40)
        Me.btnGoldBarFix.TabIndex = 3856
        Me.btnGoldBarFix.Text = "GoldBar Fix"
        Me.btnGoldBarFix.UseVisualStyleBackColor = False
        '
        'btnGoldBarIssue
        '
        Me.btnGoldBarIssue.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnGoldBarIssue.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGoldBarIssue.Location = New System.Drawing.Point(259, 570)
        Me.btnGoldBarIssue.Name = "btnGoldBarIssue"
        Me.btnGoldBarIssue.Size = New System.Drawing.Size(114, 40)
        Me.btnGoldBarIssue.TabIndex = 3857
        Me.btnGoldBarIssue.Text = "GoldBar Issue"
        Me.btnGoldBarIssue.UseVisualStyleBackColor = False
        '
        'frmJobSheet
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(920, 645)
        Me.Controls.Add(Me.btnGoldBarIssue)
        Me.Controls.Add(Me.btnGoldBarFix)
        Me.Controls.Add(Me.chkImage)
        Me.Controls.Add(Me.grpMatchJobsheet)
        Me.Controls.Add(Me.chkAccessories)
        Me.Controls.Add(Me.chkSplCategory)
        Me.Controls.Add(Me.grdJobSheet)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmJobSheet"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "JobSheet"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdJobSheet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpMatchJobsheet.ResumeLayout(False)
        Me.grpMatchJobsheet.PerformLayout()
        CType(Me.dgvHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim DM As New DataManager
    Dim sSupplierCode, sCurrencyCode, sJobSheetCode, sClassCode, sCategoryCode, sBillClose, sTranType As String
    Dim dCost, dLocalCost, dForeignCost As Double
    Dim bEdited As Boolean
    Dim msPurchaseType As String
    Dim dTotalWeight, dTotalGoldLoss, dTotalMC, dTotalPCS, dTotalLocalCost, dTotalForeignCost As Double
    Dim sDelCatalogList$

    Private Const CCATALOGUECODE As Integer = 1
    Private Const CCATEGORYCODE As Integer = 2
    Private Const CDESCRIUPTION As Integer = 3
    Private Const CWEIGHT As Integer = 4
    Private Const CGOLDLOSS As Integer = 5
    Private Const CCOSTPERGRAM As Integer = 6
    Private Const CMC As Integer = 7
    Private Const CPCES As Integer = 8
    Private Const CTOTALCOST As Integer = 9
    Private Const CCLASSCODE As Integer = 10
    Public dWeightGrd, dGoldLossGrd, dMCGrd, dPCSGrd, dTotalCostGrd, dTotalDiaCost As Double

    Private Sub frmJobSheet_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                SendKeys.Send("{tab}")
            Case Keys.F6
                btnClear_Click(sender, e)
            Case Keys.F7
                btnSave_Click(sender, e)
            Case Keys.Escape
                btnClose_Click(sender, e)
            Case Keys.F8
                btnDelete_Click(sender, e)
            Case Keys.Up
                SendKeys.Send("+{tab}")
            Case Keys.Down
                SendKeys.Send("{tab}")
        End Select
    End Sub


    Private Sub subSetPurchaseType()
        Dim objClass As New ClsClass
        Dim dS As DataSet

        Try

            dS = objClass.GetClassDetail(Trim(sClassCode))

            If dS.Tables("Class").Rows.Count > 0 Then
                msPurchaseType = Trim(dS.Tables("Class").Rows(0)("PurchaseType") & "")
                'txtPCS.Enabled = (Trim(dS.Tables("Class").Rows(0)("PurchaseType") & "") = "P" Or Trim(dS.Tables("Class").Rows(0)("PurchaseType") & "") = "B")
            End If

            dS = Nothing
            objClass = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try


    End Sub

    Private Sub subGetCatalogue(ByVal sCatalogueCode As String)
        Try
            Dim Dr As DataRow()
            Dim sSql As String
            sSql = "select a.Cataloguecode,a.CatalogueName,A.Categorycode,b.ClassCode from  tblcatalogue A ,tblcategory B"
            sSql = sSql & " where a.categorycode=b.Categorycode and a.Cataloguecode=" & fncFormatSQLText(sCatalogueCode)
            Dr = DataManager.RowReturnProjExecNonQryWithDataRow(sSql)
            If Dr.Length > 0 Then
                subCheckCatalogueExist()
                txtCategoryCode.Text = Trim(Dr(0)("CategoryCode") & "")
                txCatalogueName.Text = Trim(Dr(0)("CatalogueName") & "")
                sCategoryCode = Trim(Dr(0)("CategoryCode") & "")
                sClassCode = Trim(Dr(0)("ClassCode") & "")
                txtWeight.Focus()
            Else
                MsgBox("Invalid CatalogueCode", MsgBoxStyle.Critical)
                subClearDetail()
                txtCatalogueCode.Focus()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub subGetCategory(ByVal sCategoryCode As String)
        Try
            Dim Dr As DataRow()
            Dim sSql As String
            sSql = "select Categorycode,CategoryName,ClassCode from tblcategory "
            sSql = sSql & " where Categorycode=" & fncFormatSQLText(sCategoryCode)
            Dr = DataManager.RowReturnProjExecNonQryWithDataRow(sSql)
            If Dr.Length > 0 Then
                subCheckCategoryExist()
                txtCCategoryCode.Text = Trim(Dr(0)("CategoryCode") & "")
                txtCategoryName.Text = Trim(Dr(0)("CategoryName") & "")
                sCategoryCode = Trim(Dr(0)("CategoryCode") & "")
                sClassCode = Trim(Dr(0)("ClassCode") & "")
                txtWeight.Focus()
            Else
                MsgBox("Invalid CategoryCode", MsgBoxStyle.Critical)
                subClearDetail()
                txtCCategoryCode.Focus()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub subClearDetail()
        Try
            bEdited = False
            txtCatalogueCode.Text = ""
            txtCatalogueCode.Text = ""
            txtCategoryCode.Text = ""
            txCatalogueName.Text = ""
            txtCCategoryCode.Text = ""
            txtCategoryName.Text = ""
            txtWeight.Text = ""
            txtGoldLoss.Text = ""
            txtMC.Text = ""
            txtPCS.Text = ""
            txtTotalCost.Text = ""
            sClassCode = ""
            If Val(txtCostPerGram.Text) <> 0 Then
                txtCostPerGram1.Text = Val(txtCostPerGram.Text)
            End If
            'txtCostPerGram1.Locked = False
            txtCostPerGram1.TabStop = True
            Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtWeight_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWeight.LostFocus
        Try
            If Val(txtWeight.Text) <= 0 Then
                txtWeight.Text = "0.00"
            Else
                CalcTot()
                txtWeight.Text = Format(Val(txtWeight.Text), "#0.00")
            End If
            Exit Sub
        Catch ex As Exception

        End Try
    End Sub
    Private Sub CalcTot()
        dCost = (Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram1.Text) + Val(txtMC.Text)
    End Sub

    Private Sub txtGoldLoss_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGoldLoss.LostFocus
        Try
            If Val(txtGoldLoss.Text) <= 0 Then
                txtGoldLoss.Text = "0.00"
            Else
                CalcTot()
                txtGoldLoss.Text = Format(Val(txtGoldLoss.Text), "#0.00")
            End If
            Exit Sub
        Catch ex As Exception

        End Try
    End Sub
    Private Sub txtCostPerGram1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCostPerGram1.GotFocus

        'If Len(txtCostPerGram1.Text) > 0 Then
        '    subHighlightObject(txtCostPerGram.Text)
        'End If
    End Sub

    Private Sub txtCostPerGram1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCostPerGram1.LostFocus
        Try
            If Val(txtCostPerGram1.Text) <= 0 Then
                txtCostPerGram1.Text = "0.00"
            Else
                CalcTot()
                txtCostPerGram1.Text = Format(Val(txtCostPerGram1.Text), "#0.00")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtMC_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMC.TextChanged

    End Sub

    Private Sub txtMC_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMC.GotFocus

        'If Len(txtMC.Text) > 0 Then
        '    subHighlightObject(txtMC.Text)
        'End If
        Exit Sub
    End Sub

    Private Sub txtMC_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMC.LostFocus
        Try
            If Val(txtMC.Text) <= 0 Then
                txtMC.Text = "0.00"
            Else
                CalcTot()
                dCost = (Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram1.Text) + (Val(txtMC.Text) * Val(txtCurrencyRate.Text))
                txtMC.Text = Format(Val(txtMC.Text), "#0.00")
            End If
            Exit Sub
        Catch ex As Exception
        End Try
    End Sub
    Private Sub txtPCS_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPCS.GotFocus

        '        If Len(txtPCS.Text) > 0 Then
        '            subHighlightObject(txtPCS.Text)
        '        End If
        '        Exit Sub

    End Sub

    Private Sub txtPCS_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPCS.LostFocus
        Try
            txtPCS.Text = Format(Val(txtPCS.Text), "#0")
            txtTotalCost.Text = Format(dCost, "#0.00")
            Exit Sub
        Catch ex As Exception

        End Try
    End Sub
    Private Function fncValidateFields() As Boolean
        Try
            If Val(txtCurrencyRate.Text) <= 0 Then
                MsgBox("Please check the currency rate")
                fncValidateFields = False
                Exit Function
            End If

            ' ''If Trim(txtCatalogueCode.Text) = "" Or Trim(txtCategoryCode.Text) = "" Then
            ' ''    MsgBox("Enter valid catalogue!", vbCritical)
            ' ''    txtCatalogueCode.Focus()
            ' ''    fncValidateFields = False
            ' ''    Exit Function
            ' ''End If

            If Val(txtWeight.Text) <= 0 Then
                MsgBox("Enter valid weight!", vbCritical)
                txtWeight.Focus()
                fncValidateFields = False
                Exit Function
            End If

            If Val(txtGoldLoss.Text) < 0 Then
                MsgBox("Enter valid gold loss!", vbCritical)
                txtGoldLoss.Focus()
                fncValidateFields = False
                Exit Function
            End If
            If UCase(sCompanyShortCode) <> "MJ" Then
                If UCase(sCompanyShortCode) <> "AJ" Then
                    If Val(txtCostPerGram1.Text) <= 0 And msPurchaseType <> "P" Then
                        MsgBox("Enter valid cost per gram!", vbCritical)
                        txtCostPerGram1.Focus()
                        fncValidateFields = False
                        Exit Function
                    End If
                ElseIf UCase(sCompanyShortCode) = "AJ" Then
                    If optFix.Checked = True Then
                        If Val(txtCostPerGram1.Text) <= 0 And msPurchaseType <> "P" Then
                            MsgBox("Enter valid cost per gram!", vbCritical)
                            txtCostPerGram1.Focus()
                            fncValidateFields = False
                            Exit Function
                        End If
                    End If
                End If
            End If

            If Val(txtMC.Text) < 0 Then
                MsgBox("Enter valid making charges!", vbCritical)
                txtMC.Focus()
                fncValidateFields = False
                Exit Function
            End If

            If Val(txtPCS.Text) <= 0 Then
                MsgBox("Enter valid pcs!", vbCritical)
                txtPCS.Focus()
                fncValidateFields = False
                Exit Function
            End If

            fncValidateFields = True
        Catch ex As Exception

        End Try
    End Function
    Private Sub subAddJobSheet()
        Dim r As Integer
        Try
            If Not bEdited Then
                If grdJobSheet.RowCount = 0 Then
                    grdJobSheet.Rows.Add()
                Else
                    'grdJobSheet.Row = grdJobSheet.MaxRows
                    'grdJobSheet.Action = 5
                    'grdJobSheet.MaxRows = grdJobSheet.MaxRows - 1
                    'grdJobSheet.MaxRows = grdJobSheet.MaxRows + 1
                End If
                r = grdJobSheet.RowCount - 1
            Else
                r = grdJobSheet.CurrentRow.Index
            End If

            Me.grdJobSheet.Rows(grdJobSheet.RowCount - 1).Cells(0).Style.ForeColor = Color.Black
            Me.grdJobSheet.Rows(grdJobSheet.RowCount - 1).Cells(0).Style.Font = New Font(grdJobSheet.Font.FontFamily, 10, FontStyle.Regular)

            Me.grdJobSheet.Rows(grdJobSheet.RowCount - 1).Cells(3).Style.ForeColor = Color.Black
            Me.grdJobSheet.Rows(grdJobSheet.RowCount - 1).Cells(3).Style.Font = New Font(grdJobSheet.Font.FontFamily, 10, FontStyle.Regular)

            Me.grdJobSheet.Rows(grdJobSheet.RowCount - 1).Cells(4).Style.ForeColor = Color.Black
            Me.grdJobSheet.Rows(grdJobSheet.RowCount - 1).Cells(4).Style.Font = New Font(grdJobSheet.Font.FontFamily, 10, FontStyle.Regular)

            Me.grdJobSheet.Rows(grdJobSheet.RowCount - 1).Cells(6).Style.ForeColor = Color.Black
            Me.grdJobSheet.Rows(grdJobSheet.RowCount - 1).Cells(6).Style.Font = New Font(grdJobSheet.Font.FontFamily, 10, FontStyle.Regular)

            Me.grdJobSheet.Rows(grdJobSheet.RowCount - 1).Cells(7).Style.ForeColor = Color.Black
            Me.grdJobSheet.Rows(grdJobSheet.RowCount - 1).Cells(7).Style.Font = New Font(grdJobSheet.Font.FontFamily, 10, FontStyle.Regular)

            Me.grdJobSheet.Rows(grdJobSheet.RowCount - 1).Cells(8).Style.ForeColor = Color.Black
            Me.grdJobSheet.Rows(grdJobSheet.RowCount - 1).Cells(8).Style.Font = New Font(grdJobSheet.Font.FontFamily, 10, FontStyle.Regular)



            Call fnSetdgvText(grdJobSheet, r, 0, Trim(txtCatalogueCode.Text))
            Call fnSetdgvText(grdJobSheet, r, 1, Trim(txtCategoryCode.Text))
            Call fnSetdgvText(grdJobSheet, r, 2, Trim(txtCatalogueCode.Text))
            Call fnSetdgvText(grdJobSheet, r, 3, Format(Val(txtWeight.Text), "#0.00"))
            Call fnSetdgvText(grdJobSheet, r, 4, Format(Val(txtGoldLoss.Text), "#0.00"))
            Call fnSetdgvText(grdJobSheet, r, 5, Format(Val(txtCostPerGram1.Text), "#0.00"))
            Call fnSetdgvText(grdJobSheet, r, 6, Format(Val(txtMC.Text), "#0.00"))
            Call fnSetdgvText(grdJobSheet, r, 7, Format(Val(txtPCS.Text), "#0"))
            Call fnSetdgvText(grdJobSheet, r, 8, Format(Val(txtTotalCost.Text), "#0.00"))
            '  Call fnSetdgvText(grdJobSheet, r, 9, Format(Val(txtDiaCost.Text), "#0.00"))
            'Call SetText(grdJobSheet, grdJobSheet.MaxRows, CCLASSCODE, Trim(sClassCode))
            'grdJobSheet.Row = grdJobSheet.MaxRows
            'grdJobSheet.Col = 1
            'grdJobSheet.Action = 0
            'subGetTotalForGrid
            subTotalFromGrid()
            'subGetTotalForGrid()
            subTotal()
            subClearDetail()
            txtCatalogueCode.Focus()

            Exit Sub
        Catch ex As Exception
        End Try
    End Sub

    Public Function subGetTotalForGrid()
        dWeightGrd = Format(dWeightGrd + Val(txtWeight.Text), "0.00")
        dGoldLossGrd = Format(dGoldLossGrd + Val(txtGoldLoss.Text), "0.00")
        dMCGrd = Format(dMCGrd + Val(txtMC.Text), "0.00")
        dPCSGrd = Format(dPCSGrd + Val(txtPCS.Text), "0.00")
        dTotalCostGrd = Format(dTotalCostGrd + Val(txtTotalCost.Text), "0.00")
        '  dTotalDiaCost = Format(dTotalDiaCost + Val(txtDiaCost.Text), "0.00")
    End Function

    Private Sub subTotal()
        Try
            If Not bEdited Then
                grdJobSheet.Rows.Add()
            End If

            Call fnSetdgvText(grdJobSheet, grdJobSheet.RowCount - 1, 0, "TOTAL:")
            Call fnSetdgvText(grdJobSheet, grdJobSheet.RowCount - 1, 3, Format(dWeightGrd, "0.00"))
            Call fnSetdgvText(grdJobSheet, grdJobSheet.RowCount - 1, 4, Format(dGoldLossGrd, "0.00"))
            Call fnSetdgvText(grdJobSheet, grdJobSheet.RowCount - 1, 6, Format(dMCGrd, "0.00"))
            Call fnSetdgvText(grdJobSheet, grdJobSheet.RowCount - 1, 7, dPCSGrd)
            Call fnSetdgvText(grdJobSheet, grdJobSheet.RowCount - 1, 8, Format(dTotalCostGrd, "0.00"))
            '  Call fnSetdgvText(grdJobSheet, grdJobSheet.RowCount - 1, 9, Format(dTotalDiaCost, "0.00"))

            Me.grdJobSheet.Rows(grdJobSheet.RowCount - 1).Cells(0).Style.ForeColor = Color.Red
            Me.grdJobSheet.Rows(grdJobSheet.RowCount - 1).Cells(0).Style.Font = New Font(grdJobSheet.Font.FontFamily, 9, FontStyle.Bold)

            Me.grdJobSheet.Rows(grdJobSheet.RowCount - 1).Cells(3).Style.ForeColor = Color.Red
            Me.grdJobSheet.Rows(grdJobSheet.RowCount - 1).Cells(3).Style.Font = New Font(grdJobSheet.Font.FontFamily, 9, FontStyle.Bold)

            Me.grdJobSheet.Rows(grdJobSheet.RowCount - 1).Cells(4).Style.ForeColor = Color.Red
            Me.grdJobSheet.Rows(grdJobSheet.RowCount - 1).Cells(4).Style.Font = New Font(grdJobSheet.Font.FontFamily, 9, FontStyle.Bold)

            Me.grdJobSheet.Rows(grdJobSheet.RowCount - 1).Cells(6).Style.ForeColor = Color.Red
            Me.grdJobSheet.Rows(grdJobSheet.RowCount - 1).Cells(6).Style.Font = New Font(grdJobSheet.Font.FontFamily, 9, FontStyle.Bold)

            Me.grdJobSheet.Rows(grdJobSheet.RowCount - 1).Cells(7).Style.ForeColor = Color.Red
            Me.grdJobSheet.Rows(grdJobSheet.RowCount - 1).Cells(7).Style.Font = New Font(grdJobSheet.Font.FontFamily, 9, FontStyle.Bold)

            Me.grdJobSheet.Rows(grdJobSheet.RowCount - 1).Cells(8).Style.ForeColor = Color.Red
            Me.grdJobSheet.Rows(grdJobSheet.RowCount - 1).Cells(8).Style.Font = New Font(grdJobSheet.Font.FontFamily, 9, FontStyle.Bold)

            'Me.grdJobSheet.Rows(grdJobSheet.RowCount - 1).Cells(9).Style.ForeColor = Color.Red
            'Me.grdJobSheet.Rows(grdJobSheet.RowCount - 1).Cells(9).Style.Font = New Font(grdJobSheet.Font.FontFamily, 9, FontStyle.Bold)


            'grdJobSheet.Columns = 4
            'grdJobSheet.Formula = "sum(d1:d" & grdJobSheet.MaxRows - 1 & ")"
            'grdJobSheet.ForeColor = Color.Red
            'grdJobSheet.FontBold = True

            'grdJobSheet.Col = 5
            'grdJobSheet.Formula = "sum(e1:e" & grdJobSheet.MaxRows - 1 & ")"
            'grdJobSheet.ForeColor = Color.Red
            'grdJobSheet.FontBold = True

            'grdJobSheet.Col = 7
            'grdJobSheet.Formula = "sum(g1:g" & grdJobSheet.MaxRows - 1 & ")"
            'grdJobSheet.ForeColor = Color.Red
            'grdJobSheet.FontBold = True

            'grdJobSheet.Col = 8
            'grdJobSheet.Formula = "sum(h1:h" & grdJobSheet.MaxRows - 1 & ")"
            'grdJobSheet.ForeColor = Color.Red
            'grdJobSheet.FontBold = True

            'grdJobSheet.Col = 9
            'grdJobSheet.Formula = "sum(i1:i" & grdJobSheet.MaxRows - 1 & ")"
            'grdJobSheet.ForeColor = Color.Red
            'grdJobSheet.FontBold = True

            txtManualMC.Text = Format(dMCGrd, "0.00")
            txtManualCost.Text = Format(dTotalCostGrd, "0.00")
            txtManualWeight.Text = Format(dWeightGrd, "0.00")
            txtManualGoldLoss.Text = Format(dGoldLossGrd, "0.00")
            txtNetCost.Text = Format(Val(dTotalCostGrd) - Val(dMCGrd), "0.00")
            txtGoldValueMC.Text = Format(Val(txtNetCost.Text) + Val(txtManualMC.Text) + Val(txtAdjustment.Text), "0.00")
            subCalcGST()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If sBillClose = "Y" Or sTranType = "DP" Then
                subClearDetail()
                Exit Sub
            End If
            dForeignCost = dCost
            If Val(txtCurrencyRate.Text) <> 0 Then
                dLocalCost = dForeignCost * Val(txtCurrencyRate.Text)
            Else
                dLocalCost = dCost
            End If

            txtTotalCost.Text = Format(dLocalCost, "#0.00")

            If fncValidateFields() Then
                subAddJobSheet()
            End If
            Exit Sub
        Catch ex As Exception
        End Try
    End Sub
    Private Sub txtCostPerGram_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCostPerGram.LostFocus
        Try
            '' HAJ TEAL 22k and 24K Calculation on 25 sep 2007
            ''If ChkGST1.Checked = True And opt22K.Checked = True Then
            ''    'txtTAEL.Text = Format((((Val(txtCostPerGram.Text) / 1.05) / 0.92) / 0.02646), "0")
            ''    txtTAEL.Text = Format((((Val(txtCostPerGram.Text) / Val(GstTaelPercentage)) / 0.92) / 0.02646), "0")
            ''ElseIf ChkGST1.Checked = True And opt24K.Checked = True Then
            ''    txtTAEL.Text = Format((((Val(txtCostPerGram.Text) / Val(GstTaelPercentage))) / 0.02646), "0")
            ''ElseIf ChkGST1.Checked = False And opt24K.Checked = True Then
            ''    txtCostPerGram.Text = Format(((((Val(txtTAEL.Text) * 0.02646)))), "0.00")
            ''Else
            ''    txtTAEL.Text = Format((((Val(txtCostPerGram.Text)) / 0.92) / 0.02646), "0")
            ''End If


            If Val(cboTealPercent.Text) <> 0 Then
                If ChkGST1.Checked = True And opt22K.Checked = True Then
                    txtTAEL.Text = Format((((Val(txtCostPerGram.Text) / Val(GstTaelPercentage)) / Val(cboTealPercent.Text)) / 0.02646), "0")
                ElseIf ChkGST1.Checked = True And opt24K.Checked = True Then
                    txtTAEL.Text = Format((((Val(txtCostPerGram.Text) / Val(GstTaelPercentage))) / 0.02646), "0")
                ElseIf ChkGST1.Checked = False And opt24K.Checked = True Then
                    txtTAEL.Text = Format((((Val(txtCostPerGram.Text))) / 0.02646), "0")
                Else
                    txtTAEL.Text = Format((((Val(txtCostPerGram.Text) / Val(cboTealPercent.Text))) / 0.02646), "0")
                End If
            Else
                If ChkGST1.Checked = True And opt22K.Checked = True Then
                    txtTAEL.Text = Format((((Val(txtCostPerGram.Text) / Val(GstTaelPercentage)) / 0.92) / 0.02646), "0")
                ElseIf ChkGST1.Checked = True And opt24K.Checked = True Then
                    txtTAEL.Text = Format((((Val(txtCostPerGram.Text) / Val(GstTaelPercentage))) / 0.02646), "0")
                ElseIf ChkGST1.Checked = False And opt24K.Checked = True Then
                    txtTAEL.Text = Format((((Val(txtCostPerGram.Text))) / 0.02646), "0")
                Else
                    txtTAEL.Text = Format((((Val(txtCostPerGram.Text) / 0.92)) / 0.02646), "0")
                End If
            End If

            If Val(txtCostPerGram.Text) > 0 Then
                txtCostPerGram1.Text = Format(Val(txtCostPerGram.Text), "#0.00")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkGST.CheckedChanged

    End Sub
    Private Sub subCalcGST()
        Try
            'If ChkGST.Checked = True Then
            '    If OptCash.Checked = True Then
            '        txtGST.Text = Format((Val(Objuser.GstPercentage) * Val(txtManualCost.Text)) / 100, "#0.00")
            '        txtManualCost.Text = Format(Val(txtManualCost.Text.Trim) + Val(txtGST.Text.Trim), "#0.00")
            '    ElseIf OptGoldAC.Checked Then
            '        ''txtGST.Text = Format((Val(Objuser.GstPercentage) * Val(txtManualMC.Text)) / 100, "#0.00")
            '        txtGST.Text = Format((Val(Objuser.GstPercentage) * Val(txtManualCost.Text)) / 100, "#0.00")
            '        txtManualCost.Text = Format(Val(txtManualCost.Text.Trim) + Val(txtGST.Text.Trim), "#0.00")
            '    Else
            '        MsgBox("Set the payment type!", vbCritical)
            '        txtManualCost.Text = Format(Val(txtManualCost.Text.Trim) - Val(txtGST.Text.Trim), "#0.00")
            '        txtGST.Text = "0.00"
            '    End If
            'Else
            '    txtManualCost.Text = Format(Val(txtManualCost.Text.Trim) - Val(txtGST.Text.Trim), "#0.00")
            '    txtGST.Text = "0.00"
            'End If
            'If ChkGST.Checked = True Then
            '    If OptCash.Checked = True Then
            '        txtGST.Text = Format((Val(Objuser.GstPercentage) * (Val(txtManualCost.Text) + Val(txtAdjustment.Text))) / 100, "#0.00")
            '        txtManualCost.Text = Format(Val(txtManualCost.Text) + Val(txtGST.Text.Trim), "#0.00")
            '    ElseIf OptGoldAC.Checked Then
            '        ''txtGST.Text = Format((Val(Objuser.GstPercentage) * Val(txtManualMC.Text)) / 100, "#0.00")
            '        txtGST.Text = Format((Val(Objuser.GstPercentage) * (Val(txtManualCost.Text) + Val(txtAdjustment.Text))) / 100, "#0.00")
            '        txtManualCost.Text = Format(Val(txtManualCost.Text) + Val(txtGST.Text.Trim), "#0.00")
            '    Else
            '        MsgBox("Set the payment type!", vbCritical)
            '        txtManualCost.Text = Format(Val(txtManualCost.Text.Trim) - Val(txtGST.Text.Trim), "#0.00")
            '        txtGST.Text = "0.00"
            '    End If
            'Else
            '    txtManualCost.Text = Format(Val(txtManualCost.Text.Trim) - Val(txtGST.Text.Trim), "#0.00")
            '    txtGST.Text = "0.00"
            'End If
            If ChkGST.Checked = True Then
                If OptCash.Checked = True Then
                    txtGST.Text = Format((Val(Objuser.GstPercentage) * (Val(txtGoldValueMC.Text) - Val(txtPayableCash.Text))) / 100, "#0.00")
                    txtManualCost.Text = Format(Val(txtGoldValueMC.Text) + Val(txtGST.Text.Trim), "#0.00")
                ElseIf OptGoldAC.Checked Then
                    ''txtGST.Text = Format((Val(Objuser.GstPercentage) * Val(txtManualMC.Text)) / 100, "#0.00")
                    txtGST.Text = Format((Val(Objuser.GstPercentage) * (Val(txtGoldValueMC.Text) - Val(txtPayableCash.Text))) / 100, "#0.00")
                    txtManualCost.Text = Format(Val(txtGoldValueMC.Text) + Val(txtGST.Text.Trim), "#0.00")
                Else
                    '  MsgBox("Set the payment type!", vbCritical)
                    'txtManualCost.Text = Format(Val(txtManualCost.Text.Trim) - Val(txtGST.Text.Trim), "#0.00")
                    'txtGST.Text = "0.00"
                    txtGST.Text = Format((Val(Objuser.GstPercentage) * (Val(txtGoldValueMC.Text) - Val(txtPayableCash.Text))) / 100, "#0.00")
                    txtManualCost.Text = Format(Val(txtGoldValueMC.Text) + Val(txtGST.Text.Trim), "#0.00")
                End If
            Else
                txtManualCost.Text = Format(Val(txtManualCost.Text.Trim) - Val(txtGST.Text.Trim), "#0.00")
                txtGST.Text = "0.00"
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ChkGST_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkGST.Click
        Try
            subCalcGST()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subCheckCatalogueExist()
        Dim i As Long
        Try
            For i = 0 To grdJobSheet.RowCount
                If UCase(Trim(txtCatalogueCode.Text)) = UCase(Trim(GetText(grdJobSheet, i, 0))) Then

                    txtCategoryCode.Text = Trim(fnGetdgvText(grdJobSheet, i, 1))
                    txCatalogueName.Text = Trim(fnGetdgvText(grdJobSheet, i, 2))
                    txtWeight.Text = Format(Val(fnGetdgvText(grdJobSheet, i, 3)), "#0.00")
                    txtGoldLoss.Text = Format(Val(fnGetdgvText(grdJobSheet, i, 4)), "#0.00")
                    txtCostPerGram1.Text = Format(IIf(Val(txtCurrencyRate.Text) <> 1, Val(fnGetdgvText(grdJobSheet, i, 5)) / Val(txtCurrencyRate.Text), Val(fnGetdgvText(grdJobSheet, i, 5))), "#0.00")
                    txtMC.Text = Format(IIf(Val(txtCurrencyRate.Text) <> 1, Val(fnGetdgvText(grdJobSheet, i, 6)) / Val(txtCurrencyRate.Text), Val(fnGetdgvText(grdJobSheet, i, 6))), "#0.00")
                    'txtPCS = Format(Val(GetText(grdJobSheet, i, 8)), "#0")
                    'txtTotalCost = Format(IIf(Val(txtCurrencyRate) <> 1, Val(GetText(grdJobSheet, i, 9)) / Val(txtCurrencyRate), Val(GetText(grdJobSheet, i, 9))), "#0.00")

                    'grdJobSheet.Row = i
                    'grdJobSheet.Col = 1
                    'grdJobSheet.Action = 0
                    bEdited = True
                    Exit For
                End If
            Next i
            Exit Sub
        Catch ex As Exception
        End Try

    End Sub
    Private Sub subCheckCategoryExist()
        Dim i As Long
        Try
            For i = 0 To grdJobSheet.RowCount
                If UCase(Trim(txtCCategoryCode.Text)) = UCase(Trim(fnGetdgvText(grdJobSheet, i, 0))) Then

                    txtCategoryCode.Text = Trim(fnGetdgvText(grdJobSheet, i, 1))
                    txtCategoryName.Text = Trim(fnGetdgvText(grdJobSheet, i, 2))
                    txtWeight.Text = Format(Val(fnGetdgvText(grdJobSheet, i, 3)), "#0.00")
                    txtGoldLoss.Text = Format(Val(fnGetdgvText(grdJobSheet, i, 4)), "#0.00")
                    txtCostPerGram1.Text = Format(IIf(Val(txtCurrencyRate.Text) <> 1, Val(fnGetdgvText(grdJobSheet, i, 5)) / Val(txtCurrencyRate.Text), Val(fnGetdgvText(grdJobSheet, i, 5))), "#0.00")
                    txtMC.Text = Format(IIf(Val(txtCurrencyRate.Text) <> 1, Val(fnGetdgvText(grdJobSheet, i, 6)) / Val(txtCurrencyRate.Text), Val(fnGetdgvText(grdJobSheet, i, 6))), "#0.00")
                    'txtPCS = Format(Val(GetText(grdJobSheet, i, 8)), "#0")
                    'txtTotalCost = Format(IIf(Val(txtCurrencyRate) <> 1, Val(GetText(grdJobSheet, i, 9)) / Val(txtCurrencyRate), Val(GetText(grdJobSheet, i, 9))), "#0.00")

                    'grdJobSheet.Row = i
                    'grdJobSheet.Col = 1
                    'grdJobSheet.Action = 0
                    bEdited = True
                    Exit For
                End If
            Next i
            Exit Sub
        Catch ex As Exception
        End Try

    End Sub
    Private Sub subCheckCurrency()
        Try
            If Trim(sDefaultCurrency) = "" Then
                MessageBox.Show("Please set default currency in company master.", "GoldPOS")
            Else
                txtCurrencyCode.Text = sDefaultCurrency
                sCurrencyCode = txtCurrencyCode.Text
                subGetCurrency()
            End If
            Exit Sub
        Catch ex As Exception
        End Try
    End Sub

    Private Sub subGetCurrency()
        Try

            Dim DR As DataRow()
            Dim sSql As String
            If Trim(sCurrencyCode) <> "" Then
                sSql = "select * from tblCurrency where CurrencyCode=" & fncFormatSQLText(Trim(sCurrencyCode))
                DR = DataManager.RowReturnProjExecNonQryWithDataRow(sSql)
                If DR.Length > 0 Then
                    txtCurrencyCode.Text = Trim(sCurrencyCode)
                    txtCurrencyName.Text = Trim(DR(0)("CurrencyName") & "")
                    txtCurrencyRate.Text = Format(Val(DR(0)("CurrencyRate") & ""), "#0.0")
                Else
                    MessageBox.Show("Please set default currency in company master.", "GoldPOS")
                    txtCurrencyCode.Text = ""
                    txtCurrencyName.Text = ""
                    txtCurrencyRate.Text = ""
                    sCurrencyCode = ""
                End If

            Else
                txtCurrencyCode.Text = ""
                txtCurrencyName.Text = ""
                txtCurrencyRate.Text = ""
                sCurrencyCode = ""
            End If
            Exit Sub

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subCheckGSTPerc()

    End Sub


    Private Sub frmJobSheet_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ' subGridSetting(grdJobSheet)
            dWeightGrd = 0
            dGoldLossGrd = 0
            dMCGrd = 0
            dPCSGrd = 0
            dTotalCostGrd = 0
            dTotalDiaCost = 0
            subCheckCurrency()
            subCheckGSTPerc()
            subClear()
            Call SetFormImage(Me, "GP")
            If Trim(sCompanyShortCode) = "AJ" Then
                ChkGST.Checked = True
                ChkGST1.Checked = True
            End If
            Exit Sub
        Catch ex As Exception
        End Try
    End Sub
    Function subLockAllFlds(ByVal bLock As Boolean) As Boolean
        Try

            txtJobSheetNo.Enabled = Not bLock

            txtJobSheetNo.Enabled = Not bLock
            txtInvoiceNo.ReadOnly = bLock
            txtSupplierCode.Enabled = Not bLock

            txtDD.Enabled = Not bLock
            txtMM.Enabled = Not bLock
            txtYY.Enabled = Not bLock



            OptGoldAC.Enabled = Not bLock
            OptCash.Enabled = Not bLock

            ChkGST1.Enabled = Not bLock
            txtTAEL.ReadOnly = bLock
            txtCostPerGram.ReadOnly = bLock

            txtRemarks.ReadOnly = bLock
            ChkGST.Enabled = Not bLock

            txtCatalogueCode.Enabled = Not bLock
            txtWeight.ReadOnly = bLock
            txtGoldLoss.ReadOnly = bLock
            txtCostPerGram1.ReadOnly = bLock
            txtMC.ReadOnly = bLock
            txtPCS.ReadOnly = bLock
            grdJobSheet.Enabled = Not bLock
            btnDelete.Enabled = Not bLock
            btnCatDelete.Enabled = Not bLock
            btnSave.Enabled = Not bLock

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function
    Function subLockAllFldsLock(ByVal bLock As Boolean) As Boolean
        Try

            'txtJobSheetNo.Enabled = Not bLock

            'txtJobSheetNo.Enabled = Not bLock
            'txtInvoiceNo.ReadOnly = bLock
            'txtSupplierCode.Enabled = Not bLock

            'txtDD.Enabled = Not bLock
            'txtMM.Enabled = Not bLock
            'txtYY.Enabled = Not bLock

            'OptGoldAC.Enabled = Not bLock
            'OptCash.Enabled = Not bLock

            'ChkGST1.Enabled = Not bLock
            'txtTAEL.ReadOnly = bLock
            'txtCostPerGram.ReadOnly = bLock

            'txtRemarks.ReadOnly = bLock
            ChkGST.Enabled = Not bLock

            'txtCatalogueCode.Enabled = Not bLock
            'txtWeight.ReadOnly = bLock
            'txtGoldLoss.ReadOnly = bLock
            'txtCostPerGram1.ReadOnly = bLock
            'txtMC.ReadOnly = bLock
            'txtPCS.ReadOnly = bLock
            ' grdJobSheet.Enabled = Not bLock
            ' btnDelete.Enabled = Not bLock
            'btnCatDelete.Enabled = Not bLock
            btnSave.Enabled = Not bLock

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function
    Private Sub subClear()
        Dim sDate As String
        Try
            dWeightGrd = 0
            dGoldLossGrd = 0
            dMCGrd = 0
            dPCSGrd = 0
            dTotalCostGrd = 0
            dTotalDiaCost = 0
            subLockAllFlds(False)
            sJobSheetCode = ""
            txtJobSheetNo.Text = ""
            txtInvoiceNo.Text = ""
            sDate = Format(Now, "dd/MM/yyyy")
            txtDD.Text = Mid(Trim(sDate), 1, 2)
            txtMM.Text = Mid(Trim(sDate), 4, 2)
            txtYY.Text = Mid(Trim(sDate), 7, 4)

            txtDDD.Text = Mid(Trim(sDate), 1, 2)
            txtDMM.Text = Mid(Trim(sDate), 4, 2)
            txtDYY.Text = Mid(Trim(sDate), 7, 4)


            txtSupplierCode.Text = ""
            txtSupplierName.Text = ""
            OptCash.Checked = False
            OptGoldAC.Checked = False
            ChkGST1.Checked = 0
            opt22K.Checked = True
            txtTAEL.Text = ""
            txtCostPerGram.Text = ""
            txtRemarks.Text = ""

            txtManualCost.Text = ""
            txtManualMC.Text = ""
            ChkGST.Checked = 0
            txtGST.Text = ""
            txtManualWeight.Text = ""
            txtManualGoldLoss.Text = ""
            txtNetCost.Text = ""
            txtAdjustment.Text = ""
            chkAccessories.Checked = False
            grdJobSheet.RowCount = 0
            subClearDetail()
            txtCostPerGram1.Text = ""
            sTranType = ""
            sBillClose = "N"
            sDelCatalogList = ","
            txtMatchJobSheetNo.Text = ""
            txtMatchWeight.Text = ""
            txtMatchPCS.Text = ""
            btnSave.Enabled = True
            btnCatDelete.Enabled = True
            btnDelete.Enabled = True
            grpMatchJobsheet.Visible = False
            Exit Sub
        Catch ex As Exception
        End Try

    End Sub




    Private Sub subGetSupplier(ByVal sender As Object, ByVal e As System.EventArgs)
        Try


            Dim sSql As String
            Dim DR As DataRow()
            If Trim(sSupplierCode) <> "" Then
                sSql = "Select * from tblSupplier where SupplierCode=" & fncFormatSQLText(sSupplierCode)
                DR = DataManager.RowReturnProjExecNonQryWithDataRow(sSql)
                If DR.Length > 0 Then
                    txtSupplierName.Text = Trim(DR(0)("SupplierName") & "")
                    If Trim(DR(0)("GSTType") & "") = "E" Then
                        ChkGST1.Checked = True
                    Else
                        ChkGST1.Checked = False
                    End If

                    If Trim(DR(0)("AccountType") & "") = "GL" Then
                        OptGoldAC.Checked = True
                    ElseIf Trim(DR(0)("AccountType") & "") = "C" Then
                        OptCash.Checked = True
                    ElseIf Trim(DR(0)("AccountType") & "") = "B" Then
                        optBoth.Checked = True
                    End If

                    ChkGST1_Click(sender, e)
                Else
                    MsgBox("Invalid supplier code!", vbCritical)
                    txtSupplierCode.Text = ""
                    txtSupplierName.Text = ""
                    sSupplierCode = ""
                End If
            Else
                txtSupplierCode.Text = ""
                txtSupplierName.Text = ""
                sSupplierCode = ""
            End If

            Exit Sub
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ChkGST1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkGST1.Click
        Try
            ''HAJ TEAL Calculation on 25 Sep 2007
            ' ''If ChkGST1.Checked = True And opt22K.Checked = True Then
            ' ''    'txtCostPerGram.Text = Format(((((Val(txtTAEL.Text) * 0.02646) * 0.92) * 1.05)), "0.00")
            ' ''    txtCostPerGram.Text = Format(((((Val(txtTAEL.Text) * 0.02646) * 0.92) * Val(GstTaelPercentage))), "0.00")
            ' ''ElseIf ChkGST1.Checked = True And opt24K.Checked = True Then
            ' ''    txtCostPerGram.Text = Format(((((Val(txtTAEL.Text) * 0.02646)))), "0.00")
            ' ''ElseIf ChkGST1.Checked = False And opt24K.Checked = True Then
            ' ''    txtCostPerGram.Text = Format(((((Val(txtTAEL.Text) * 0.02646)))), "0.00")
            ' ''Else
            ' ''    txtCostPerGram.Text = Format(((((Val(txtTAEL.Text) * 0.02646) * 0.92))), "0.00")
            ' ''End If

            If Val(cboTealPercent.Text) <> 0 Then
                If ChkGST1.Checked = True And opt22K.Checked = True Then
                    'txtCostPerGram.Text = Format(((((Val(txtTAEL.Text) * 0.02646) * 0.92) * 1.05)), "0.00")
                    txtCostPerGram.Text = Format(((((Val(txtTAEL.Text) * 0.02646) * Val(cboTealPercent.Text)) * Val(GstTaelPercentage))), "0.00")
                ElseIf ChkGST1.Checked = True And opt24K.Checked = True Then
                    txtCostPerGram.Text = Format(((((Val(txtTAEL.Text) * 0.02646)))), "0.00")
                ElseIf ChkGST1.Checked = False And opt24K.Checked = True Then
                    txtCostPerGram.Text = Format(((((Val(txtTAEL.Text) * 0.02646)))), "0.00")
                Else
                    txtCostPerGram.Text = Format(((((Val(txtTAEL.Text) * 0.02646) * Val(cboTealPercent.Text)))), "0.00")
                End If
            Else
                If ChkGST1.Checked = True And opt22K.Checked = True Then
                    'txtCostPerGram.Text = Format(((((Val(txtTAEL.Text) * 0.02646) * 0.92) * 1.05)), "0.00")
                    txtCostPerGram.Text = Format(((((Val(txtTAEL.Text) * 0.02646) * 0.92) * Val(GstTaelPercentage))), "0.00")
                ElseIf ChkGST1.Checked = True And opt24K.Checked = True Then
                    txtCostPerGram.Text = Format(((((Val(txtTAEL.Text) * 0.02646)))), "0.00")
                ElseIf ChkGST1.Checked = False And opt24K.Checked = True Then
                    txtCostPerGram.Text = Format(((((Val(txtTAEL.Text) * 0.02646)))), "0.00")
                Else
                    txtCostPerGram.Text = Format(((((Val(txtTAEL.Text) * 0.02646) * 0.92))), "0.00")
                End If
            End If
            Exit Sub
        Catch ex As Exception
        End Try
    End Sub
    Private Sub subCheckDuplicateBill()
        Try
            Dim Dr As DataRow()

            Dim sSql As String
            sSql = "SELECT JobSheetNo FROM tblJobsheetHeader WHERE SupplierCode =" & fncFormatSQLText(sSupplierCode)
            sSql = sSql & " And PurchaseBillNo = " & fncFormatSQLText(Trim(txtInvoiceNo.Text))
            Dr = DataManager.RowReturnProjExecNonQryWithDataRow(sSql)
            If Dr.Length > 0 Then
                If Trim(sJobSheetCode) <> Trim(Dr(0)("JobSheetNo") & "") Then
                    MsgBox("Duplicate Invoice No. This invoice is already entered in JobsheetNo " & Trim(Dr(0)("JobSheetNo") & "") & ".")
                    txtInvoiceNo.Text = ""
                    txtInvoiceNo.Focus()
                End If
            End If
            Exit Sub
        Catch ex As Exception
        End Try
    End Sub
    Function fncGetJobSheet(ByVal sSupplier$, ByVal sInvoiceNo$) As Boolean
        Try

            Dim DR As DataRow()
            Dim SBll As New clsJobSheet
            DR = SBll.GetJobSheetByInvoice(sSupplier, sInvoiceNo)
            If DR.Length > 0 Then
                txtJobSheetNo.Text = Trim$(DR(0)("JobSheetNo") & "")
                ' txtJobSheetNo_LostFocus()
                fncGetJobSheet = True

            Else
                fncGetJobSheet = False
            End If
        Catch ex As Exception
        End Try
    End Function


    Function fncCheckValidity() As Boolean


        Try
            If Len(Trim$(txtInvoiceNo.Text)) = 0 Then
                MsgBox("Invoice no. cannot be empty!")
                txtInvoiceNo.Focus()
                Exit Function
            End If
            If Len(Trim$(txtSupplierCode.Text)) = 0 Then
                MsgBox("Supplier Code cannot be empty!")
                txtSupplierCode.Focus()
                Exit Function
            End If
            If Len(Trim$(txtCurrencyCode.Text)) = 0 Then
                MsgBox("Currency cannot be blank!")
                Exit Function
            End If
            If Val(txtCurrencyRate.Text) <= 0 Then
                MsgBox("Rate must be greater than zero!")
                Exit Function
            End If
            If Val(txtCostPerGram.Text) <= 0 And msPurchaseType <> "P" Then
                MsgBox("CostPerGram must be greater than zero!")
                txtCostPerGram.Focus()
                Exit Function
            End If
            If Not IsDate(txtDD.Text & "/" & txtMM.Text & "/" & txtYY.Text) Then
                txtDD.Focus()
                MsgBox("Invoice Date is Invalid!")
                Exit Function
            ElseIf CDate(txtDD.Text & "/" & txtMM.Text & "/" & txtYY.Text) > Now() Then
                MsgBox("Invoice Date should be less than today's date!")
                txtDD.Focus()
                Exit Function
            ElseIf Len(Trim(txtDD.Text)) <> 2 Or Len(Trim(txtMM.Text)) <> 2 Or Len(Trim(txtYY.Text)) <> 4 Then
                txtDD.Focus()
                MsgBox("Invoice Date is Invalid!")
                Exit Function
            End If
            If Not IsDate(txtDDD.Text & "/" & txtDMM.Text & "/" & txtDYY.Text) Then
                txtDD.Focus()
                MsgBox("Due Date is Invalid!")
                Exit Function
            ElseIf Len(Trim(txtDDD.Text)) <> 2 Or Len(Trim(txtDMM.Text)) <> 2 Or Len(Trim(txtDYY.Text)) <> 4 Then
                txtDD.Focus()
                MsgBox("Due Date is Invalid!")
                Exit Function
            End If
            If Not (OptGoldAC.Checked Or OptCash.Checked Or optBoth.Checked) Then
                MsgBox("Please select the payment type!")
                OptGoldAC.Focus()
                Exit Function
            End If

            If grdJobSheet.RowCount < 1 Then
                MsgBox("Please enter catalogue details!")
                txtCatalogueCode.Focus()
                Exit Function
            End If
            fncCheckValidity = True

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Function fncGetJobsheetNo() As Boolean
        Dim Ds As DataSet
        Dim objJobSheet As New clsJobSheet
        Try
            Ds = objJobSheet.GetUpdateJobSheetNo()
            If Ds.Tables("JobSheetNo").Rows.Count <= 0 Then
                fncGetJobsheetNo = False
                MessageBox.Show("Error in generating JobSheet No!", "GoldPOS")
                Exit Function
            Else
                sJobSheetCode = Trim(Ds.Tables("JobSheetNo").Rows(0)("NextJobSheetNo") & "")
                fncGetJobsheetNo = True
            End If
            Ds = Nothing
            objJobSheet = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim bNewJobSheet As Boolean
        If fncCheckValidity() Then
            If Trim$(sJobSheetCode) = "" Then
                bNewJobSheet = True
                If Not fncGetJobsheetNo() Then
                    Exit Sub
                End If
            End If

            If fncJobSheet(sJobSheetCode) Then
                If bNewJobSheet Then
                    MsgBox("Please take note the job sheet no " & sJobSheetCode)
                    Call btnClear_Click(sender, e)
                Else
                    Call subClearDetail()
                    MsgBox("Jobsheet Updated Successfully!")
                    txtCatalogueCode.Focus()
                End If
            Else
                MessageBox.Show("Transaction not saved", "GoldPOS")
            End If
        End If
    End Sub
    Private Function fncJobSheet(ByRef sJobsheetNo As String) As Boolean
        Dim sPayType As String
        Dim sPDate As String
        Dim sDDate As String
        Dim sFixStatus As String

        Dim bJobSheetDetail As Boolean
        Dim i As Long
        Dim sClassType As String

        Dim objJobSheet As New clsJobSheet
        Try

            bJobSheetDetail = False

            dTotalWeight = Val(fnGetdgvText(grdJobSheet, grdJobSheet.RowCount - 1, 3))
            dTotalGoldLoss = Val(fnGetdgvText(grdJobSheet, grdJobSheet.RowCount - 1, 4))
            dTotalMC = Val(fnGetdgvText(grdJobSheet, grdJobSheet.RowCount - 1, 6))
            dTotalPCS = Val(fnGetdgvText(grdJobSheet, grdJobSheet.RowCount - 1, 7))
            dTotalLocalCost = Val(fnGetdgvText(grdJobSheet, grdJobSheet.RowCount - 1, 8))
            dTotalForeignCost = Val(fnGetdgvText(grdJobSheet, grdJobSheet.RowCount - 1, 8))
            'dTotalDiaCost = Val(fnGetdgvText(grdJobSheet, grdJobSheet.RowCount - 1, 9))

            If OptGoldAC.Checked Then
                sPayType = "G"
            ElseIf OptCash.Checked Then
                sPayType = "C"
            ElseIf optBoth.Checked Then
                sPayType = "B"
            End If

            If optFix.Checked Then
                sFixStatus = "Y"
            Else
                sFixStatus = "N"
            End If
            If opt22K.Checked Then
                sClassType = "22K"
            ElseIf opt24K.Checked Then
                sClassType = "24K"
            End If
            sPDate = Trim(txtDD.Text) + "/" + Trim(txtMM.Text) + "/" + Trim(txtYY.Text)
            sDDate = Trim(txtDDD.Text) + "/" + Trim(txtDMM.Text) + "/" + Trim(txtDYY.Text)

            If IsDate(sPDate) Then
                objJobSheet.OpenBatchPostinConnection()
                objJobSheet.gpBeginTransaction()
                If Not objJobSheet.JobSheetHeaderSave(Trim(sJobsheetNo), _
                                                    txtInvoiceNo.Text.Trim, Trim(sPDate), Trim(sPayType), txtSupplierCode.Text.Trim, txtCurrencyCode.Text.Trim, Val(txtCurrencyRate.Text), _
                                                    Val(txtCostPerGram.Text), Val(dTotalWeight), Val(dTotalGoldLoss), Val(dTotalMC), Val(dTotalPCS), Val(dTotalLocalCost), Val(txtGST.Text), Val(dTotalForeignCost), _
                                                    Val(txtManualCost.Text), Val(txtManualMC.Text), Val(txtManualWeight.Text), Val(txtManualGoldLoss.Text), _
                                                    txtRemarks.Text.Trim, "JP", IIf(ChkGST1.Checked = True, "Y", "N"), Val(txtAdjustment.Text), sUserID, sFixStatus, IIf(chkSplCategory.Checked = True, "Y", "N"), _
                                                    IIf(chkAccessories.Checked = True, "Y", "N"), txtMatchJobSheetNo.Text.Trim, Val(txtMatchWeight.Text), Val(txtMatchPCS.Text), Trim(txtSalesOrderNo.Text), Val(cboTealPercent.Text), Val(txtTAEL.Text), Trim(sClassType), Trim(sDDate)) Then
                    MessageBox.Show("Error in Saving JobSheet Header", "GoldPOS")
                    objJobSheet.gpRollBackBatchConnection()
                    objJobSheet.CloseBatchConnection()
                    objJobSheet = Nothing
                    Exit Function
                End If


                'details save
                For i = 0 To grdJobSheet.RowCount - 2
                    If Not objJobSheet.JobSheetDetailSave(Trim(sJobsheetNo), _
                        Trim(fnGetdgvText(grdJobSheet, i, 0)), Trim(fnGetdgvText(grdJobSheet, i, 1)), _
                        Trim(fnGetdgvText(grdJobSheet, i, 2)), _
                        Val(fnGetdgvText(grdJobSheet, i, 5)), Val(fnGetdgvText(grdJobSheet, i, 3)), _
                        Val(fnGetdgvText(grdJobSheet, i, 6)), Val(fnGetdgvText(grdJobSheet, i, 7)), _
                        Val(fnGetdgvText(grdJobSheet, i, 4)), Val(fnGetdgvText(grdJobSheet, i, 8)), sUserID) Then
                        MessageBox.Show("Error in Saving JobSheet Detail", "GoldPOS")
                        objJobSheet.gpRollBackBatchConnection()
                        objJobSheet.CloseBatchConnection()
                        objJobSheet = Nothing
                        Exit Function
                    End If

                    If chkAccessories.Checked = True Then
                        If Not objJobSheet.AccessoriesSave(Trim(sJobsheetNo), _
                        Trim(fnGetdgvText(grdJobSheet, i, 0)), Trim(fnGetdgvText(grdJobSheet, i, 1)), _
                        Trim(fnGetdgvText(grdJobSheet, i, 2)), _
                        Val(fnGetdgvText(grdJobSheet, i, 5)), Val(fnGetdgvText(grdJobSheet, i, 3)), _
                        Val(fnGetdgvText(grdJobSheet, i, 6)), Val(fnGetdgvText(grdJobSheet, i, 7)), _
                        Val(fnGetdgvText(grdJobSheet, i, 4)), Val(fnGetdgvText(grdJobSheet, i, 8)), sUserID) Then
                            MessageBox.Show("Error in Saving Accessories Detail", "GoldPOS")
                            objJobSheet.gpRollBackBatchConnection()
                            objJobSheet.CloseBatchConnection()
                            objJobSheet = Nothing
                            Exit Function
                        End If
                    End If
                    If Trim(txtSalesOrderNo.Text) <> "" Then
                        If Not objJobSheet.SalesOrderHeaderSave(Trim(txtSalesOrderNo.Text),
                       Trim(fnGetdgvText(grdJobSheet, i, 0)), Trim(fnGetdgvText(grdJobSheet, i, 1)), Val(fnGetdgvText(grdJobSheet, i, 7)), _
                        sUserID) Then
                            MessageBox.Show("Error in Saving SalesOrder Header", "GoldPOS")
                            objJobSheet.gpRollBackBatchConnection()
                            objJobSheet.CloseBatchConnection()
                            objJobSheet = Nothing
                            Exit Function
                        End If
                    End If
                Next i
                fncJobSheet = True
            End If
            objJobSheet.gpCommitTransaction()
            objJobSheet.CloseBatchConnection()
            objJobSheet = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            objJobSheet.gpRollBackBatchConnection()
            objJobSheet.CloseBatchConnection()
            objJobSheet = Nothing
        End Try

    End Function


    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            subClear()
            txtJobSheetNo.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim objJobSheet As New clsJobSheet
        Dim iRet As String
        Try
            If txtJobSheetNo.Text.Trim <> "" Then
                iRet = MsgBox("Do you want to delete JobSheet - " & txtJobSheetNo.Text.Trim & " ?", vbYesNo)
                If iRet = vbYes Then
                    If Not objJobSheet.fncCheckJobSheetDelete(txtJobSheetNo.Text.Trim) Then
                        MsgBox("This job sheet has items. Cannot delete!", vbCritical)
                    Else
                        objJobSheet.OpenBatchPostinConnection()
                        objJobSheet.gpBeginTransaction()
                        If objJobSheet.UpdateModifyUser(txtJobSheetNo.Text.Trim, txtCatalogueCode.Text.Trim, Trim(sUserID), "J") Then
                            If objJobSheet.DeleteSave(txtJobSheetNo.Text.Trim, Trim(sUserID)) Then
                                If objJobSheet.JobSheetDelete(txtJobSheetNo.Text.Trim, Trim(sUserID)) Then
                                    objJobSheet.gpCommitTransaction()
                                    MsgBox("Jobsheet deleted successfully!", vbInformation)
                                    btnClear_Click(sender, e)
                                Else
                                    MsgBox("Error in Deletion", MsgBoxStyle.Critical)
                                    objJobSheet.gpRollBackBatchConnection()
                                    Exit Sub
                                End If
                            Else
                                objJobSheet.gpRollBackBatchConnection()
                                Exit Sub
                            End If
                        Else
                            objJobSheet.gpRollBackBatchConnection()
                            Exit Sub
                        End If
                    End If
                End If
            Else
                MsgBox("Select job sheet for deletion!", vbInformation)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub btnCatAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCatAdd.Click
        Try
            If sBillClose = "Y" Or sTranType = "DP" Then
                subClearDetail()
                Exit Sub
            End If

            dForeignCost = dCost
            If Val(txtCurrencyRate.Text) <> 0 Then
                dLocalCost = dForeignCost * Val(txtCurrencyRate.Text)
            Else
                dLocalCost = dCost
            End If

            txtTotalCost.Text = Format(dLocalCost, "#0.00")

            If fncValidateFields() Then
                subAddJobSheet()
                bEdited = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnCatDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCatDelete.Click
        Dim sPBillNo As String
        Dim sBillDate As String
        Dim sDueDate As String
        Dim sPayableType As String
        Dim sSupplierCode As String
        Dim sCurrencyCode As String
        Dim dCurrencyRate As Double
        Dim dCostPerGram As Double
        Dim dTotalWeight24k As Double
        Dim dTotalGoldLoss As Double
        Dim dTotalMC As Double
        Dim dTotalPCS As Double
        Dim dTotalCost As Double
        Dim dTotalLocalCost As Double
        Dim dGst As Double
        Dim dTotalForeignCost As Double
        Dim dManualLocalCost As Double
        Dim dManualMC As Double
        Dim dManualWeight As Double
        Dim dManualGoldLoss As Double
        Dim sRemarks As String
        Dim sFixStatus As String


        Dim objCatalogue As New ClsCatalogue
        Dim iRet As String
        Dim objjobsheet As New clsJobSheet
        Dim DsCat As DataSet
        Dim sClassType As String
        Try



            If txtCatalogueCode.Text.Trim <> "" Then
                iRet = MsgBox("Do you want to delete Catalogue - " & txtCatalogueCode.Text.Trim & " ?", MsgBoxStyle.YesNo)
                If iRet = vbYes Then
                    If objCatalogue.fncCheckCatalogueDelete1(txtCatalogueCode.Text.Trim, txtJobSheetNo.Text.Trim) Then
                        MsgBox("This catalogue has items. Cannot delete!", MsgBoxStyle.Critical)
                    Else
                        'get jobsheet header detail
                        Dim DsJH As DataSet
                        DsJH = objjobsheet.GetJobSheetHeader(txtJobSheetNo.Text.Trim)
                        If DsJH.Tables("JobSheet").Rows.Count > 0 Then
                            DsCat = objjobsheet.GetJobSheetDetailByCatCode(txtJobSheetNo.Text.Trim, txtCatalogueCode.Text.Trim)
                            If DsCat.Tables("JobSheet").Rows.Count > 0 Then
                                sPBillNo = Trim(DsJH.Tables("JobSheet").Rows(0)("PurchaseBillNo") & "")
                                sBillDate = Trim(DsJH.Tables("JobSheet").Rows(0)("JobSheetDate") & "")
                                sDueDate = Trim(DsJH.Tables("JobSheet").Rows(0)("DueDate") & "")
                                sSupplierCode = Trim(DsJH.Tables("JobSheet").Rows(0)("SupplierCode") & "")
                                dTotalWeight24k = Val(DsJH.Tables("JobSheet").Rows(0)("TotalWeight24K") & "") - Val(DsCat.Tables("JobSheet").Rows(0)("Weight") & "")
                                dTotalMC = Val(DsJH.Tables("JobSheet").Rows(0)("TotalMC") & "") - Val(DsCat.Tables("JobSheet").Rows(0)("Mc") & "")
                                dTotalPCS = Val(DsJH.Tables("JobSheet").Rows(0)("TotalPCS") & "") - Val(DsCat.Tables("JobSheet").Rows(0)("Pcs") & "")
                                dTotalGoldLoss = Val(DsJH.Tables("JobSheet").Rows(0)("TotalGoldLoss") & "") - Val(DsCat.Tables("JobSheet").Rows(0)("GoldLoss") & "")
                                dCostPerGram = Val(DsJH.Tables("JobSheet").Rows(0)("CostperGram") & "")
                                sCurrencyCode = Trim(DsJH.Tables("JobSheet").Rows(0)("CurrencyCode") & "")
                                dCurrencyRate = Val(DsJH.Tables("JobSheet").Rows(0)("CurrencyRate") & "")
                                sPayableType = Trim(DsJH.Tables("JobSheet").Rows(0)("PaymentType") & "")
                                dTotalLocalCost = Val(DsJH.Tables("JobSheet").Rows(0)("TotalLocalCost") & "") - Val(DsCat.Tables("JobSheet").Rows(0)("TotalCost") & "")
                                dTotalCost = Val(DsJH.Tables("JobSheet").Rows(0)("TotalCost") & "") - Val(DsCat.Tables("JobSheet").Rows(0)("TotalCost") & "")
                                dTotalForeignCost = Val(DsJH.Tables("JobSheet").Rows(0)("TotalForeignCost") & "") - Val(DsCat.Tables("JobSheet").Rows(0)("TotalCost") & "")
                                dManualLocalCost = Val(DsJH.Tables("JobSheet").Rows(0)("ManualLocalCOST") & "") - Val(DsCat.Tables("JobSheet").Rows(0)("TotalCost") & "")
                                dManualMC = Val(DsJH.Tables("JobSheet").Rows(0)("ManualMc") & "") - Val(DsCat.Tables("JobSheet").Rows(0)("Mc") & "")
                                dManualWeight = Val(DsJH.Tables("JobSheet").Rows(0)("ManualWeight") & "") - Val(DsCat.Tables("JobSheet").Rows(0)("Weight") & "")
                                dManualGoldLoss = Val(DsJH.Tables("JobSheet").Rows(0)("ManualGoldLoss") & "") - Val(DsCat.Tables("JobSheet").Rows(0)("GoldLoss") & "")

                                If UCase(Trim(DsJH.Tables("JobSheet").Rows(0)("PaymentType") & "")) = "C" Then
                                    dGst = (Val(Objuser.GstPercentage) * Val(dManualLocalCost)) / 100
                                Else
                                    dGst = (Val(Objuser.GstPercentage) * Val(dManualMC)) / 100
                                End If

                                sRemarks = Trim(DsJH.Tables("JobSheet").Rows(0)("REMARKS") & "")

                                If opt22K.Checked Then
                                    sClassType = "22K"
                                ElseIf opt24K.Checked Then
                                    sClassType = "24K"
                                End If
                            Else
                                MsgBox("Error in fetching Catalogue record", vbCritical)
                                Exit Sub
                            End If
                        Else
                            MsgBox("Error in fetching Jobsheet record", vbCritical)
                            Exit Sub
                        End If

                        '----------

                        objjobsheet.OpenBatchPostinConnection()
                        objjobsheet.gpBeginTransaction()
                        If objjobsheet.JobSheetHeaderSave(txtJobSheetNo.Text.Trim, _
                            Trim(sPBillNo), Trim(sBillDate), Trim(sPayableType), Trim(sSupplierCode), Trim(sCurrencyCode), Val(dCurrencyRate), _
                            Val(dCostPerGram), Val(dTotalWeight24k), Val(dTotalGoldLoss), Val(dTotalMC), Val(dTotalPCS), Val(dTotalLocalCost), Val(dGst), Val(dTotalForeignCost), _
                            Val(dManualLocalCost), Val(dManualMC), Val(dManualWeight), Val(dManualGoldLoss), _
                            Trim(sRemarks), "JP", IIf(ChkGST1.Checked = True, "Y", "N"), Val(txtAdjustment.Text), Objuser.UserId, sFixStatus, IIf(chkSplCategory.Checked = True, "Y", "N"), _
                            IIf(chkAccessories.Checked = True, "Y", "N"), txtMatchJobSheetNo.Text.Trim, Val(txtMatchWeight.Text), Val(txtMatchPCS.Text), Trim(txtSalesOrderNo.Text), Val(cboTealPercent.Text), Val(txtTAEL.Text), Trim(sClassType), Trim(sDueDate)) Then
                            If objjobsheet.UpdateModifyUser(txtJobSheetNo.Text.Trim, txtCatalogueCode.Text.Trim, Objuser.UserId, "C") Then
                                If objCatalogue.DeleteSave(txtJobSheetNo.Text.Trim, txtCatalogueCode.Text.Trim, Objuser.UserId) Then
                                    If objCatalogue.CatalogueDeleteFromJobSheet(txtCatalogueCode.Text.Trim, txtJobSheetNo.Text.Trim, Objuser.UserId) Then
                                        objjobsheet.gpCommitTransaction()
                                        objjobsheet.CloseBatchConnection()
                                        MsgBox("Catalogue deleted successfully!", vbInformation)
                                    Else
                                        objjobsheet.gpRollBackBatchConnection()
                                        Exit Sub
                                    End If
                                Else
                                    objjobsheet.gpRollBackBatchConnection()
                                    Exit Sub
                                End If
                            Else
                                objjobsheet.gpRollBackBatchConnection()
                                Exit Sub
                            End If
                        Else
                            objjobsheet.gpRollBackBatchConnection()
                            Exit Sub
                        End If

                        subClearDetail()

                        btnClear_Click(sender, e)
                        'subGetJobSheet(sender, e)
                        'txtCatalogueCode.Focus()
                    End If
                End If
            Else
                If grdJobSheet.RowCount > 0 Then
                    MsgBox("Select catalogue for deletion!", vbInformation)
                Else
                    MsgBox("No catalogue for deletion.", vbCritical)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnCatClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCatClear.Click
        Try
            subClearDetail()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub



    Private Sub txtTAEL_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTAEL.LostFocus
        Try
            ''HAJ on 25 sep 2007
            'If ChkGST1.Checked = True And opt22K.Checked = True Then
            '    'txtCostPerGram.Text = Format(((((Val(txtTAEL.Text) * 0.02646) * 0.92) * 1.05)), "#0.00")
            '    txtCostPerGram.Text = Format(((((Val(txtTAEL.Text) * 0.02646) * 0.92) * Val(GstTaelPercentage))), "#0.00")
            'ElseIf ChkGST1.Checked = True And opt24K.Checked = True Then
            '    txtCostPerGram.Text = Format(((((Val(txtTAEL.Text) * 0.02646)) * Val(GstTaelPercentage))), "#0.00")
            'ElseIf ChkGST1.Checked = False And opt24K.Checked = True Then
            '    txtCostPerGram.Text = Format(((((Val(txtTAEL.Text) * 0.02646)))), "0.00")
            'Else
            '    txtCostPerGram.Text = Format(((((Val(txtTAEL.Text) * 0.02646) * 0.92))), "#0.00")
            'End If

            If Val(cboTealPercent.Text) <> 0 Then
                If ChkGST1.Checked = True And opt22K.Checked = True Then
                    'txtCostPerGram.Text = Format(((((Val(txtTAEL.Text) * 0.02646) * 0.92) * 1.05)), "#0.00")
                    txtCostPerGram.Text = Format(((((Val(txtTAEL.Text) * 0.02646) * Val(cboTealPercent.Text)) * Val(GstTaelPercentage))), "#0.00")
                ElseIf ChkGST1.Checked = True And opt24K.Checked = True Then
                    txtCostPerGram.Text = Format(((((Val(txtTAEL.Text) * 0.02646)) * Val(GstTaelPercentage))), "#0.00")
                ElseIf ChkGST1.Checked = False And opt24K.Checked = True Then
                    txtCostPerGram.Text = Format(((((Val(txtTAEL.Text) * 0.02646)))), "0.00")
                Else
                    txtCostPerGram.Text = Format(((((Val(txtTAEL.Text) * 0.02646) * Val(cboTealPercent.Text)))), "#0.00")
                End If
            Else
                If ChkGST1.Checked = True And opt22K.Checked = True Then
                    'txtCostPerGram.Text = Format(((((Val(txtTAEL.Text) * 0.02646) * 0.92) * 1.05)), "#0.00")
                    txtCostPerGram.Text = Format(((((Val(txtTAEL.Text) * 0.02646) * 0.92) * Val(GstTaelPercentage))), "#0.00")
                ElseIf ChkGST1.Checked = True And opt24K.Checked = True Then
                    txtCostPerGram.Text = Format(((((Val(txtTAEL.Text) * 0.02646)) * Val(GstTaelPercentage))), "#0.00")
                ElseIf ChkGST1.Checked = False And opt24K.Checked = True Then
                    txtCostPerGram.Text = Format(((((Val(txtTAEL.Text) * 0.02646)))), "0.00")
                Else
                    txtCostPerGram.Text = Format(((((Val(txtTAEL.Text) * 0.02646) * 0.92))), "#0.00")
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subGetJobSheet(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim i As Long
            Dim Ds As DataSet
            Dim objjobsheet As New clsJobSheet
            dWeightGrd = 0
            dGoldLossGrd = 0
            dMCGrd = 0
            dPCSGrd = 0
            dTotalCostGrd = 0
            dTotalDiaCost = 0
            Ds = objjobsheet.GetJobSheet(txtJobSheetNo.Text.Trim)
            If Ds.Tables("JobSheet").Rows.Count > 0 Then
                txtJobSheetNo.ReadOnly = True
                txtJobSheetNo.Text = Trim(Ds.Tables("JobSheet").Rows(0)("JobSheetNo") & "")
                sJobSheetCode = txtJobSheetNo.Text.Trim
                txtInvoiceNo.Text = Trim(Ds.Tables("JobSheet").Rows(0)("PurchaseBillNo") & "")
                txtYY.Text = Mid(Ds.Tables("JobSheet").Rows(0)("Pdate") & "", 1, 4)
                txtMM.Text = Mid(Ds.Tables("JobSheet").Rows(0)("Pdate") & "", 5, 2)
                txtDD.Text = Mid(Ds.Tables("JobSheet").Rows(0)("Pdate") & "", 7, 2)

                txtDYY.Text = Mid(Ds.Tables("JobSheet").Rows(0)("DDate") & "", 1, 4)
                txtDMM.Text = Mid(Ds.Tables("JobSheet").Rows(0)("DDate") & "", 5, 2)
                txtDDD.Text = Mid(Ds.Tables("JobSheet").Rows(0)("DDate") & "", 7, 2)

                txtSupplierCode.Text = Trim(Ds.Tables("JobSheet").Rows(0)("SupplierCode") & "")
                txtSupplierCode_LostFocus(sender, e)
                txtCurrencyCode.Text = Trim(Ds.Tables("JobSheet").Rows(0)("CurrencyCode") & " ")
                txtCurrencyCode_LostFocus(sender, e)
                txtCurrencyRate.Text = Format(Val(Ds.Tables("JobSheet").Rows(0)("CurrencyRate") & ""), "#0.0000")
                If Trim(Ds.Tables("JobSheet").Rows(0)("PaymentType") & "") = "G" Then
                    OptGoldAC.Checked = True
                ElseIf Trim(Ds.Tables("JobSheet").Rows(0)("PaymentType") & "") = "C" Then
                    OptCash.Checked = True
                Else
                    optBoth.Checked = True
                End If
                If Trim(Ds.Tables("JobSheet").Rows(0)("FixStatus") & "") = "Y" Then
                    optFix.Checked = True
                Else
                    optUnFix.Checked = True
                End If
                txtCostPerGram.Text = Format(Val(Ds.Tables("JobSheet").Rows(0)("CostperGram") & ""), "#0.00")
                ChkGST1.Checked = IIf(UCase(Trim(Ds.Tables("JobSheet").Rows(0)("TaelGST") & "")) = "Y", 1, 0)
                txtCostPerGram.Text = Format(Val(Ds.Tables("JobSheet").Rows(0)("CostperGram") & ""), "#0.00")
                txtCostPerGram_LostFocus(sender, e)
                txtRemarks.Text = Trim(Ds.Tables("JobSheet").Rows(0)("REMARKS") & "")

                txtManualCost.Text = Format(Val(Ds.Tables("JobSheet").Rows(0)("ManualLocalCOST") & ""), "#0.00")
                txtManualMC.Text = Format(Val(Ds.Tables("JobSheet").Rows(0)("ManualMc") & ""), "#0.00")
                ChkGST.Checked = IIf(Val(Ds.Tables("JobSheet").Rows(0)("Gst") & "") > 0, 1, 0)
                txtGST.Text = Format(Val(Ds.Tables("JobSheet").Rows(0)("Gst") & ""), "#0.00")
                txtManualWeight.Text = Format(Val(Ds.Tables("JobSheet").Rows(0)("ManualWeight") & ""), "#0.00")
                txtManualGoldLoss.Text = Format(Val(Ds.Tables("JobSheet").Rows(0)("ManualGoldLoss") & ""), "#0.00")
                txtAdjustment.Text = Format(Val(Ds.Tables("JobSheet").Rows(0)("ManualAdjustment") & ""), "#0.00")

                sBillClose = Trim$(Ds.Tables("JobSheet").Rows(0)("BillClose") & "")
                sTranType = Trim(Ds.Tables("JobSheet").Rows(0)("TranType") & "")

                If Trim(Ds.Tables("JobSheet").Rows(0)("SplCategory") & "") = "Y" Then
                    chkSplCategory.Checked = True
                Else
                    chkSplCategory.Checked = False
                End If
                If Trim(Ds.Tables("JobSheet").Rows(0)("Accessories") & "") = "Y" Then
                    chkAccessories.Checked = True
                Else
                    chkAccessories.Checked = False
                End If
                'lblLastUpdated.Caption = Format(adoRS!ModifyDate, "DD/MM/YYYY")
                cboTealPercent1.Items.Add(".92")
                cboTealPercent1.Items.Add(".925")
                cboTealPercent1.Items.Add(".93")
                cboTealPercent1.Items.Add(".935")
                cboTealPercent1.Items.Add(".999")

                txtTAEL.Text = Val(Ds.Tables("JobSheet").Rows(0)("TaelRate") & "")
                cboTealPercent.Text = Val(Ds.Tables("JobSheet").Rows(0)("TaelPercent") & "")

                If Trim(Ds.Tables("JobSheet").Rows(0)("ClassType") & "") = "22K" Then
                    opt22K.Checked = True
                ElseIf Trim(Ds.Tables("JobSheet").Rows(0)("ClassType") & "") = "24K" Then
                    opt24K.Checked = True
                End If

                'lock everything
                If sBillClose = "Y" Then
                    MessageBox.Show("This bill is closed. Cannot modify!")
                    subLockAllFlds(False)
                    ' subLockAllFldsLock(False)
                ElseIf sTranType = "DP" Then
                    MessageBox.Show("This is a direct purchase bill. Cannot modify!")
                    subLockAllFlds(True)
                Else
                    subLockAllFlds(False)
                End If
                txtJobSheetNo.ReadOnly = True
                txtInvoiceNo.ReadOnly = True
                txtSupplierCode.ReadOnly = True
                cboTealPercent.Enabled = False
            Else
                MsgBox("JobSheetNo is Invalid!")
                subClear()
                txtJobSheetNo.Focus()
                Exit Sub
            End If
            Ds = Nothing
            Ds = New DataSet
            objjobsheet = New clsJobSheet
            Ds = objjobsheet.GetJobSheetDetail(txtJobSheetNo.Text.Trim)

            grdJobSheet.RowCount = 0
            For i = 0 To Ds.Tables("JobSheet").Rows.Count - 1
                grdJobSheet.Rows.Add()

                Call fnSetdgvText(grdJobSheet, i, 0, Trim(Ds.Tables("JobSheet").Rows(i)("CatalogueCode") & ""))
                Call fnSetdgvText(grdJobSheet, i, 1, Trim(Ds.Tables("JobSheet").Rows(i)("CategoryCode") & ""))
                Call fnSetdgvText(grdJobSheet, i, 2, Trim(Ds.Tables("JobSheet").Rows(i)("Description") & ""))
                Call fnSetdgvText(grdJobSheet, i, 3, Format(Val(Ds.Tables("JobSheet").Rows(i)("Weight") & ""), "#0.00"))
                Call fnSetdgvText(grdJobSheet, i, 4, Format(Val(Ds.Tables("JobSheet").Rows(i)("GoldLoss") & ""), "#0.00"))
                Call fnSetdgvText(grdJobSheet, i, 5, Format(Val(Ds.Tables("JobSheet").Rows(i)("CostperGram") & ""), "#0.00"))
                Call fnSetdgvText(grdJobSheet, i, 6, Format(Val(Ds.Tables("JobSheet").Rows(i)("Mc") & ""), "#0.00"))
                Call fnSetdgvText(grdJobSheet, i, 7, Format(Val(Ds.Tables("JobSheet").Rows(i)("Pcs") & ""), "#0"))
                Call fnSetdgvText(grdJobSheet, i, 8, Format(Val(Ds.Tables("JobSheet").Rows(i)("TotalCost") & ""), "#0.00"))
                '  Call fnSetdgvText(grdJobSheet, i, 9, Format(Val(Ds.Tables("JobSheet").Rows(i)("DiaCost") & ""), "#0.00"))
                dWeightGrd = Format(dWeightGrd + Val(Ds.Tables("JobSheet").Rows(i)("Weight") & ""), "#0.00")
                dGoldLossGrd = Format(dGoldLossGrd + Val(Ds.Tables("JobSheet").Rows(i)("GoldLoss") & ""), "0.00")
                dMCGrd = Format(dMCGrd + Val(Ds.Tables("JobSheet").Rows(i)("Mc") & ""), "0.00")
                dPCSGrd = Format(dPCSGrd + Val(Ds.Tables("JobSheet").Rows(i)("PCS") & ""), "0.00")
                dTotalCostGrd = Format(dTotalCostGrd + Val(Ds.Tables("JobSheet").Rows(i)("TotalCost") & ""), "0.00")
                'dTotalDiaCost = Format(dTotalDiaCost + Val(Ds.Tables("JobSheet").Rows(i)("DiaCost") & ""), "0.00")
            Next i

            'grdJobSheet.Row = grdJobSheet.MaxRows
            'grdJobSheet.Col = 1
            'grdJobSheet.Action = 0
            subTotal()

            'grdJobSheet.Row = grdJobSheet.MaxRows
            'grdJobSheet.Action = 0

            Ds = Nothing
            objjobsheet = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub



    Private Sub opt22K_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles opt22K.Click
        Call txtCostPerGram_LostFocus(sender, e)
    End Sub


    Private Sub opt24K_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles opt24K.Click
        Call txtCostPerGram_LostFocus(sender, e)
    End Sub



    Private Sub cboTealPercent_LostFocus(sender As Object, e As System.EventArgs) Handles cboTealPercent.LostFocus
        txtCostPerGram_LostFocus(sender, e)
    End Sub

    Private Sub txtCCategoryCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtCCategoryCode.KeyDown
        Try
            Dim e1 As New System.EventArgs
            If e.KeyCode = Keys.F1 Then
                Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCategory", "L", 3, "CategoryName", "", "CategoryCode", "CategoryCode", 80, "CategoryName", "CategoryName", 180, "ClassCode", "ClassCode", 60)
                frmHelpClass.ShowDialog()
                txtCCategoryCode.Text = frmHelpClass.Var1.Trim
                txtCCategoryCode_LostFocus(sender, e1)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtCCategoryCode_LostFocus(sender As Object, e As System.EventArgs) Handles txtCCategoryCode.LostFocus
        Try
            If txtCCategoryCode.Text.Trim <> "" Then
                subGetCategory(txtCCategoryCode.Text.Trim)
                subSetPurchaseType()
            End If

            If Not bEdited Then
                txtPCS.Text = "1"
                txtCostPerGram1.Text = txtCostPerGram.Text
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnCCategoryCodeSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnCCategoryCodeSearch.Click
        Try
            Dim e1 As New System.EventArgs
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCategory", "L", 3, "CategoryName", "", "CategoryCode", "CategoryCode", 80, "CategoryName", "CategoryName", 180, "ClassCode", "ClassCode", 60)
            frmHelpClass.ShowDialog()
            txtCCategoryCode.Text = frmHelpClass.Var1.Trim
            txtCCategoryCode_LostFocus(sender, e1)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtJobSheetNo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtJobSheetNo.KeyDown
        Try
            Dim e1 As New System.EventArgs
            If e.KeyCode = Keys.F1 Then
                Dim frmHelpClass As New frmHelpSearch(0, 0, "tblJobSheetHeader", "L", 4, "JobSheetNo", "", "JobSheetNo", "JobSheetNo", 80, "SupplierCode", "SupplierCode", 180, "JobSheetDate", "JobSheetDate", 60, "TotalCost", "TotalCost", 80)
                frmHelpClass.ShowDialog()
                txtJobSheetNo.Text = frmHelpClass.Var1.Trim
                SendKeys.Send("{TAB}")
                'txtJobSheetNo_LostFocus(sender, e1)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtJobSheetNo_LostFocus(sender As Object, e As System.EventArgs) Handles txtJobSheetNo.LostFocus
        Try
            If txtJobSheetNo.Text.Trim <> "" Then
                subGetJobSheet(sender, e)
                txtCatalogueCode.Focus()
            Else
                sJobSheetCode = ""
                sBillClose = "N"
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub btnJobSheetNoSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnJobSheetNoSearch.Click
        Try
            Dim e1 As New System.EventArgs
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblJobSheetHeader", "L", 4, "JobSheetNo", "", "JobSheetNo", "JobSheetNo", 80, "SupplierCode", "SupplierCode", 80, "PurchaseBillNo", "PurchaseBillNo", 80, "TotalCost", "TotalCost", 80)
            frmHelpClass.ShowDialog()
            txtJobSheetNo.Text = frmHelpClass.Var1.Trim
            SendKeys.Send("{TAB}")
            txtJobSheetNo_LostFocus(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub txtCurrencyCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtCurrencyCode.KeyDown
        Try
            Dim e1 As New System.EventArgs
            If e.KeyCode = Keys.F1 Then
                Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCurrency", "L", 4, "CurrencyName", "", "CurrencyCode", "CurrencyCode", 80, "CurrencyName", "CurrencyName", 180, "CurrencyRate", "CurrencyRate", 60, "Remarks", "Remarks", 80)
                frmHelpClass.ShowDialog()
                txtCurrencyCode.Text = frmHelpClass.Var1.Trim
                txtCurrencyName.Text = frmHelpClass.Var2.Trim
                txtCurrencyRate.Text = frmHelpClass.Var3.Trim
                txtCurrencyCode_LostFocus(sender, e1)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnCurrencyCodeSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnCurrencyCodeSearch.Click
        Try
            Dim e1 As New System.EventArgs
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCurrency", "L", 4, "CurrencyName", "", "CurrencyCode", "CurrencyCode", 80, "CurrencyName", "CurrencyName", 180, "CurrencyRate", "CurrencyRate", 60, "Remarks", "Remarks", 80)
            frmHelpClass.ShowDialog()
            txtCurrencyCode.Text = frmHelpClass.Var1.Trim
            txtCurrencyName.Text = frmHelpClass.Var2.Trim
            txtCurrencyRate.Text = frmHelpClass.Var3.Trim
            txtCurrencyCode_LostFocus(sender, e1)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtCurrencyCode_LostFocus(sender As Object, e As System.EventArgs) Handles txtCurrencyCode.LostFocus
        Try
            sCurrencyCode = txtCurrencyCode.Text.Trim
            subGetCurrency()
            OptGoldAC.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub txtSupplierCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtSupplierCode.KeyDown
        Try
            Dim e1 As New System.EventArgs
            If e.KeyCode = Keys.F1 Then
                Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSupplier", "L", 4, "SupplierName", "", "SupplierCode", "SupplierCode", 80, "SupplierName", "SupplierName", 180, "Address1", "Address1", 60, "Address2", "Address2", 80)
                frmHelpClass.ShowDialog()
                txtSupplierCode.Text = frmHelpClass.Var1.Trim
                txtSupplierName.Text = frmHelpClass.Var2.Trim
                txtSupplierCode_LostFocus(sender, e1)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtSupplierCode_LostFocus(sender As Object, e As System.EventArgs) Handles txtSupplierCode.LostFocus
        Try
            sSupplierCode = Trim(txtSupplierCode.Text)
            subGetSupplier(sender, e)
            If Trim(txtJobSheetNo.Text) = "" Then
                subCheckDuplicateBill()
                txtCurrencyCode.Focus()
            End If

            If Trim(txtJobSheetNo.Text) = "" And Trim$(txtSupplierCode.Text) <> "" And Trim$(txtInvoiceNo.Text) <> "" Then
                Call fncGetJobSheet(Trim$(txtSupplierCode.Text), Trim$(txtInvoiceNo.Text))
            End If
            Exit Sub
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnSupplierCodeSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSupplierCodeSearch.Click
        Try
            Dim e1 As New System.EventArgs
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSupplier", "L", 4, "SupplierName", "", "SupplierCode", "SupplierCode", 80, "SupplierName", "SupplierName", 180, "Address1", "Address1", 60, "Address2", "Address2", 80)
            frmHelpClass.ShowDialog()
            txtSupplierCode.Text = frmHelpClass.Var1.Trim
            txtSupplierName.Text = frmHelpClass.Var2.Trim
            txtSupplierCode_LostFocus(sender, e1)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtCatalogueCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtCatalogueCode.KeyDown
        Try
            Dim e1 As New System.EventArgs
            If e.KeyCode = Keys.F1 Then
                Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCatalogue", "L", 4, "CatalogueName", "GoldLoss='Y'", "CatalogueCode", "CatalogueCode", 80, "CatalogueName", "CatalogueName", 180, "CategoryCode", "CategoryCode", 60, "GoldLoss", "GoldLoss", 80)
                frmHelpClass.ShowDialog()
                txtCatalogueCode.Text = frmHelpClass.Var1.Trim
                txtCatalogueCode_LostFocus(sender, e1)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtCatalogueCode_LostFocus(sender As Object, e As System.EventArgs) Handles txtCatalogueCode.LostFocus
        Try
            If txtCatalogueCode.Text.Trim <> "" Then
                subGetCatalogue(txtCatalogueCode.Text.Trim)
                subSetPurchaseType()
            End If

            If Not bEdited Then
                txtPCS.Text = "1"
                txtCostPerGram1.Text = txtCostPerGram.Text
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnCatalogueCode_Click(sender As System.Object, e As System.EventArgs) Handles btnCatalogueCode.Click
        Try
            If Trim(txtSalesOrderNo.Text) <> "" Then
                Dim e1 As New System.EventArgs
                'Dim frmHelpClass As New frmHelpSearch(0, 0, "vwSalesOrder", "L", 4, "CatalogueName", "SalesOrderNo=" & fncFormatSQLText(txtSalesOrderNo.Text), "CatalogueCode", "CatalogueCode", 80, "CatalogueName", "CatalogueName", 180, "CategoryCode", "CategoryCode", 60, "SalesOrderNo", "SalesOrderNo", 80)
                Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSalesOrderHeader", "L", 4, "CatalogueCode", "SalesOrderNo=" & fncFormatSQLText(txtSalesOrderNo.Text) & " and SalesOrderQty > ReceivedQty", "CatalogueCode", "CatalogueCode", 80, "CategoryCode", "CategoryCode", 180, "SalesOrderNo", "SalesOrderNo", 60, "SalesOrderQty", "SalesOrderQty", 80)
                frmHelpClass.ShowDialog()
                txtCatalogueCode.Text = frmHelpClass.Var1.Trim
                txtCatalogueCode_LostFocus(sender, e1)
            Else
                Dim e1 As New System.EventArgs
                Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCatalogue", "L", 4, "CatalogueName", "GoldLoss='Y'", "CatalogueCode", "CatalogueCode", 80, "CatalogueName", "CatalogueName", 180, "CategoryCode", "CategoryCode", 60, "GoldLoss", "GoldLoss", 80)
                frmHelpClass.ShowDialog()
                txtCatalogueCode.Text = frmHelpClass.Var1.Trim
                txtCatalogueCode_LostFocus(sender, e1)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtAdjustment_Leave(sender As Object, e As System.EventArgs) Handles txtAdjustment.Leave
        txtGoldValueMC.Text = Format(Val(txtNetCost.Text.Trim) + Val(txtManualMC.Text.Trim) + Val(txtAdjustment.Text.Trim), "#0.00")
        subCalcGST()
    End Sub

    Private Sub txtJobSheetNo_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtJobSheetNo.TextChanged

    End Sub

    Private Sub btnCatBDelete_Click(sender As System.Object, e As System.EventArgs) Handles btnCatBDelete.Click
        Dim objCatalogue As New ClsCatalogue
        Try
            If txtCatalogueCode.Text.Trim <> "" Then
                iRet = MsgBox("Do you want to delete Catalogue - " & txtCatalogueCode.Text.Trim & " ?", MsgBoxStyle.YesNo)
                If iRet = vbYes Then
                    If objCatalogue.fncCheckCatalogueDeleteBefore(txtCatalogueCode.Text.Trim, txtJobSheetNo.Text.Trim) Then
                        MsgBox("This catalogue has items. Cannot delete!", MsgBoxStyle.Critical)
                    Else
                        'remove the deleted row.
                        'grdJobSheet.Action = FPSpread.ActionConstants.ActionDeleteRow
                        'grdJobSheet.MaxRows = grdJobSheet.MaxRows - 1
                        grdJobSheet.Rows.RemoveAt(grdJobSheet.CurrentRow.Index)
                        If grdJobSheet.RowCount = 1 Then
                            grdJobSheet.RowCount = 0
                        Else
                            subTotalFromGrid()
                            subTotal()
                            btnCatClear_Click(Nothing, Nothing)
                        End If
                    End If
                End If
                If grdJobSheet.RowCount >= 0 Then
                    MsgBox("Select catalogue for deletion!", vbInformation)
                Else
                    MsgBox("No catalogue for deletion.", vbCritical)
                End If
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
        dTotalDiaCost = 0
        For icnt = 0 To grdJobSheet.RowCount - 1
            sInventoryCode = Trim(fnGetdgvText(grdJobSheet, icnt, 0))
            If sInventoryCode <> "TOTAL:" Then
                dWeightGrd = Format(Val(dWeightGrd) + Val(fnGetdgvText(grdJobSheet, icnt, 3)), "0.00")
                dGoldLossGrd = Format(Val(dGoldLossGrd) + Val(fnGetdgvText(grdJobSheet, icnt, 4)), "0.00")
                dMCGrd = Format(Val(dMCGrd) + Val(fnGetdgvText(grdJobSheet, icnt, 6)), "0.00")
                dPCSGrd = Format(Val(dPCSGrd) + Val(fnGetdgvText(grdJobSheet, icnt, 7)), "0.00")
                dTotalCostGrd = Format(Val(dTotalCostGrd) + Val(fnGetdgvText(grdJobSheet, icnt, 8)), "0.00")
                '  dTotalDiaCost = Format(Val(dTotalDiaCost) + Val(fnGetdgvText(grdJobSheet, icnt, 9)), "0.00")
            End If
        Next
    End Function



    Private Sub grdJobSheet_Click(sender As Object, e As System.EventArgs) Handles grdJobSheet.Click
        If grdJobSheet.CurrentRow.Index = grdJobSheet.RowCount - 1 Then
            'do nothing
        Else
            bEdited = True
            GetDetailsFromGrid()
        End If
    End Sub
    Public Function GetDetailsFromGrid()
        txtCatalogueCode.Text = fnGetdgvText(grdJobSheet, grdJobSheet.CurrentRow.Index, 0)
        txtCategoryCode.Text = fnGetdgvText(grdJobSheet, grdJobSheet.CurrentRow.Index, 1)

        txCatalogueName.Text = fnGetdgvText(grdJobSheet, grdJobSheet.CurrentRow.Index, 2)
        txtWeight.Text = fnGetdgvText(grdJobSheet, grdJobSheet.CurrentRow.Index, 3)

        txtGoldLoss.Text = fnGetdgvText(grdJobSheet, grdJobSheet.CurrentRow.Index, 4)
        txtCostPerGram1.Text = fnGetdgvText(grdJobSheet, grdJobSheet.CurrentRow.Index, 5)

        txtMC.Text = fnGetdgvText(grdJobSheet, grdJobSheet.CurrentRow.Index, 6)

        txtPCS.Text = fnGetdgvText(grdJobSheet, grdJobSheet.CurrentRow.Index, 7)
        txtTotalCost.Text = fnGetdgvText(grdJobSheet, grdJobSheet.CurrentRow.Index, 8)


    End Function


    Private Sub btnMatchJobsheetNo_Click(sender As System.Object, e As System.EventArgs) Handles btnMatchJobsheetNo.Click
        Try
            Dim e1 As New System.EventArgs
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblJobSheetHeader", "L", 4, "JobSheetNo", "Accessories='Y'", "JobSheetNo", "JobSheetNo", 80, "SupplierCode", "SupplierCode", 180, "JobSheetDate", "JobSheetDate", 60, "TotalCost", "TotalCost", 80)
            frmHelpClass.ShowDialog()
            txtMatchJobSheetNo.Text = frmHelpClass.Var1.Trim
            SendKeys.Send("{TAB}")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnMatchCatalogue_Click(sender As System.Object, e As System.EventArgs) Handles btnMatchCatalogue.Click
        Try
            Dim Search As String
            Search = " CatalogueCode in (Select DISTINCT(CatalogueCode) From tblAccessoriesDetail " & _
                                        "Where JobSheetNo = " & fncFormatSQLText(txtMatchJobSheetNo.Text.Trim) & " )"
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCatalogue", "L", 4, "CatalogueName", Search, "CatalogueCode", "CatalogueCode", 80, "CatalogueName", "CatalogueName", 180, "CategoryCode", "CategoryCode", 60, "GoldLoss", "GoldLoss", 80)
            frmHelpClass.ShowDialog()
            txtMatchCatalogue.Text = frmHelpClass.Var1.Trim
            'If txtCatalogueCode.Enabled Then txtCatalogueCode.Focus()
            ' txtCatalogueCode_LostFocus(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            Err.Clear()
        End Try
    End Sub

    Private Sub btnSalesOrderNo_Click(sender As System.Object, e As System.EventArgs) Handles btnSalesOrderNo.Click
        Try
            Dim e1 As New System.EventArgs
            Dim frmHelpClass As New frmHelpSearch(0, 0, "vwSalesOrderHeader", "L", 4, "SalesOrderNo", "SupplierCode=" & fncFormatSQLText(txtSupplierCode.Text.Trim), "SalesOrderNo", "SalesOrderNo", 80, "SupplierCode", "SupplierCode", 180, "SalesOrderDate", "SalesOrderDate", 60, "DueDate", "DueDate", 80)
            frmHelpClass.ShowDialog()
            txtSalesOrderNo.Text = frmHelpClass.Var1.Trim
            SendKeys.Send("{TAB}")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub chkImage_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkImage.CheckedChanged
        If chkImage.Checked Then
            grpMatchJobsheet.Visible = True
        Else
            grpMatchJobsheet.Visible = False
        End If
    End Sub

    Private Sub btnTaelPercent_Click(sender As System.Object, e As System.EventArgs) Handles btnTaelPercent.Click
        Try
            Dim sdate As String
            Dim sPdate As String
            Dim e1 As New System.EventArgs
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblTaelPercent", "L", 4, "TaelPercent", "", "TaelPercent", "TaelPercent", "80", "TaelPercent1", "TaelPercent", "80", "TaelPercent2", "TaelPercent", "80", "TaelPercent3", "TaelPercent", "80")
            frmHelpClass.ShowDialog()
            cboTealPercent.Text = frmHelpClass.Var1.Trim
            sPdate = Trim(txtDD.Text) + "/" + Trim(txtMM.Text) + "/" + Trim(txtYY.Text)

            sdate = Format(DateAdd("d", Val(frmHelpClass.Var2.Trim), sPdate), "dd/MM/yyyy")


            txtDDD.Text = Mid(Trim(sDate), 1, 2)
            txtDMM.Text = Mid(Trim(sDate), 4, 2)
            txtDYY.Text = Mid(Trim(sDate), 7, 4)
            SendKeys.Send("{TAB}")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub txtGST_LostFocus(sender As Object, e As System.EventArgs) Handles txtGST.LostFocus
        txtManualCost.Text = Format(Val(txtGoldValueMC.Text) + Val(txtGST.Text.Trim), "#0.00")
    End Sub

    Private Sub grpMatchJobsheet_Enter(sender As System.Object, e As System.EventArgs) Handles grpMatchJobsheet.Enter

    End Sub

    Private Sub btnGoldBarFix_Click(sender As System.Object, e As System.EventArgs) Handles btnGoldBarFix.Click
        Dim objGoldBarFix As New frmGoldBarFix
        objGoldBarFix.Show()
        objGoldBarFix.txtJobSheetNo.Text = Trim(txtJobSheetNo.Text)
        objGoldBarFix.txtJobSheetNo_LostFocus(sender, e)
    End Sub

    Private Sub btnGoldBarIssue_Click(sender As System.Object, e As System.EventArgs) Handles btnGoldBarIssue.Click
        Dim objGoldBarIssue As New frmGoldBarIssue_AJ
        objGoldBarIssue.Show()
        objGoldBarIssue.txtSupplierCode1.Text = Trim(txtSupplierCode.Text)
        objGoldBarIssue.subGetSupplier(sender, e)
    End Sub
End Class
