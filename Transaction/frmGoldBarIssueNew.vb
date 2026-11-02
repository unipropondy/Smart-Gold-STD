Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmGoldBarIssueNew
    Inherits System.Windows.Forms.Form
    Dim objPurchase As New clsGoldBarPurchase
    Dim objTIS As New ClsGoldBarIssue
    Dim dABal As Double
    Dim dPaidCost As Double
    Dim sInvoicedate As String
    Dim bProceed As Boolean
    Dim dm As New DataManager
    Dim SInvoiceNo As String
    Dim sInventoryCode As String
    Dim sInvNo As String
    Dim sInvCode As String
    Dim IbLoad As Boolean
    Dim dWeight As Double
    Dim bEdited As Boolean
    Dim dAIssueBal As Double
    Dim dIssueWeight As Double
    Dim d24KIssueWeight As Double
    Dim sTDate As String
    Dim sGoldBarIssueNo As String
    Friend WithEvents grdInvoice As System.Windows.Forms.DataGridView
    Friend WithEvents cSel As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents cJobSheetNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CInvoiceNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSupplierCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCustomerCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cBalanceWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grdGoldIssue As System.Windows.Forms.DataGridView
    Friend WithEvents cIInvoiceNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cIInvoiceDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cIPurchaseBillNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cIClass As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cIPurchaseWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cIIssuedWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cIBalanceWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grdIssue1 As System.Windows.Forms.DataGridView
    Friend WithEvents cIs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cIIInventoryCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cIIDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cIIWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cIIQty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cIICostPerGram As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cIITotalCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cIIBarNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grdPCS1 As System.Windows.Forms.DataGridView
    Friend WithEvents cBSelect As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents cBBarNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtIssueNo1 As System.Windows.Forms.TextBox
    Friend WithEvents txtInvoiceNo1 As System.Windows.Forms.TextBox
    Friend WithEvents txtSupplierCode1 As System.Windows.Forms.TextBox
    Friend WithEvents txtIssued1 As System.Windows.Forms.TextBox
    Friend WithEvents txtNetIssue1 As System.Windows.Forms.TextBox
    Friend WithEvents txtIssued2 As System.Windows.Forms.TextBox
    Friend WithEvents btnInvoiceNo As System.Windows.Forms.Button
    Friend WithEvents btnSupplierCode As System.Windows.Forms.Button
    Friend WithEvents btnInventoryCode As System.Windows.Forms.Button
    Friend WithEvents txtInventoryCode1 As System.Windows.Forms.TextBox
    Dim sBarNo As String
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
    Friend WithEvents txtsupplierName As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtYYYY As System.Windows.Forms.TextBox
    Friend WithEvents txtMM As System.Windows.Forms.TextBox
    Friend WithEvents txtDD As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents txtRate As System.Windows.Forms.TextBox
    Friend WithEvents txtCurrencyName As System.Windows.Forms.TextBox
    Friend WithEvents txtCurrencyCode As AxEditLib.AxfpText
    'Friend WithEvents txtSupplierCode1 As System.Windows.Forms.TextBox
    Friend WithEvents txtIssueNo As AxEditLib.AxfpText
    Friend WithEvents grdUnpaidBill As AxFPSpread.AxvaSpread
    Friend WithEvents txtPayable As AxEditLib.AxfpDoubleSingle
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents txtClear As System.Windows.Forms.Button
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents lblPcs As System.Windows.Forms.Label
    Friend WithEvents TxtTotalWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtPcs As System.Windows.Forms.TextBox
    Friend WithEvents txtCostperGram As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalCost As System.Windows.Forms.TextBox
    Friend WithEvents txtInvoiceNo As AxEditLib.AxfpText
    Friend WithEvents pnlInvoice As System.Windows.Forms.Panel
    Friend WithEvents btnPanelClose As System.Windows.Forms.Button
    Friend WithEvents chkSelectAll As System.Windows.Forms.CheckBox
    Friend WithEvents btnPanelOK As System.Windows.Forms.Button
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents grdIssue As AxFPSpread.AxvaSpread
    Friend WithEvents txtInventoryCode As AxEditLib.AxfpText
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnIssueOk As System.Windows.Forms.Button
    Friend WithEvents txtInvInvoiceNo As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents btnPCS As System.Windows.Forms.Button
    Friend WithEvents pnlPCS As System.Windows.Forms.Panel
    Friend WithEvents grdPCS As AxFPSpread.AxvaSpread
    Friend WithEvents btnPanelPCS As System.Windows.Forms.Button
    Friend WithEvents btnPanePCSClose As System.Windows.Forms.Button
    Friend WithEvents chkPcsSelectAll As System.Windows.Forms.CheckBox
    Friend WithEvents btnRePrint As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGoldBarIssueNew))
        Dim DataGridViewCellStyle37 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle38 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle39 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle40 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle41 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle42 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle43 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle44 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle45 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle46 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle47 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle48 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnInvoiceNo = New System.Windows.Forms.Button()
        Me.btnSupplierCode = New System.Windows.Forms.Button()
        Me.txtIssued2 = New System.Windows.Forms.TextBox()
        Me.txtNetIssue1 = New System.Windows.Forms.TextBox()
        Me.txtIssueNo1 = New System.Windows.Forms.TextBox()
        Me.txtInvoiceNo1 = New System.Windows.Forms.TextBox()
        Me.txtSupplierCode1 = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtRate = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtYYYY = New System.Windows.Forms.TextBox()
        Me.txtMM = New System.Windows.Forms.TextBox()
        Me.txtDD = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtCurrencyName = New System.Windows.Forms.TextBox()
        Me.txtsupplierName = New System.Windows.Forms.TextBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.btnInventoryCode = New System.Windows.Forms.Button()
        Me.txtInventoryCode1 = New System.Windows.Forms.TextBox()
        Me.grdIssue1 = New System.Windows.Forms.DataGridView()
        Me.cIs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cIIInventoryCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cIIDescription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cIIWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cIIQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cIICostPerGram = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cIITotalCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cIIBarNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnPCS = New System.Windows.Forms.Button()
        Me.txtInvInvoiceNo = New System.Windows.Forms.TextBox()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.txtClear = New System.Windows.Forms.Button()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtPcs = New System.Windows.Forms.TextBox()
        Me.txtTotalCost = New System.Windows.Forms.TextBox()
        Me.txtCostperGram = New System.Windows.Forms.TextBox()
        Me.TxtTotalWeight = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.lblPcs = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.grdIssue = New AxFPSpread.AxvaSpread()
        Me.grdUnpaidBill = New AxFPSpread.AxvaSpread()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.pnlInvoice = New System.Windows.Forms.Panel()
        Me.grdInvoice = New System.Windows.Forms.DataGridView()
        Me.cSel = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.cJobSheetNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CInvoiceNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cSupplierCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cCustomerCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cBalanceWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnPanelClose = New System.Windows.Forms.Button()
        Me.chkSelectAll = New System.Windows.Forms.CheckBox()
        Me.btnPanelOK = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.grdGoldIssue = New System.Windows.Forms.DataGridView()
        Me.cIInvoiceNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cIInvoiceDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cIPurchaseBillNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cIClass = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cIPurchaseWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cIIssuedWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cIBalanceWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnIssueOk = New System.Windows.Forms.Button()
        Me.pnlPCS = New System.Windows.Forms.Panel()
        Me.grdPCS1 = New System.Windows.Forms.DataGridView()
        Me.cBSelect = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.cBBarNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnPanePCSClose = New System.Windows.Forms.Button()
        Me.chkPcsSelectAll = New System.Windows.Forms.CheckBox()
        Me.btnPanelPCS = New System.Windows.Forms.Button()
        Me.grdPCS = New AxFPSpread.AxvaSpread()
        Me.btnRePrint = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.grdIssue1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        CType(Me.grdIssue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdUnpaidBill, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlInvoice.SuspendLayout()
        CType(Me.grdInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.grdGoldIssue, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPCS.SuspendLayout()
        CType(Me.grdPCS1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdPCS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.btnInvoiceNo)
        Me.GroupBox1.Controls.Add(Me.btnSupplierCode)
        Me.GroupBox1.Controls.Add(Me.txtIssued2)
        Me.GroupBox1.Controls.Add(Me.txtNetIssue1)
        Me.GroupBox1.Controls.Add(Me.txtIssueNo1)
        Me.GroupBox1.Controls.Add(Me.txtInvoiceNo1)
        Me.GroupBox1.Controls.Add(Me.txtSupplierCode1)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.Label26)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtRate)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtYYYY)
        Me.GroupBox1.Controls.Add(Me.txtMM)
        Me.GroupBox1.Controls.Add(Me.txtDD)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtCurrencyName)
        Me.GroupBox1.Controls.Add(Me.txtsupplierName)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(16, 80)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(984, 120)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Entry Section"
        '
        'btnInvoiceNo
        '
        Me.btnInvoiceNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnInvoiceNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInvoiceNo.Image = CType(resources.GetObject("btnInvoiceNo.Image"), System.Drawing.Image)
        Me.btnInvoiceNo.Location = New System.Drawing.Point(353, 90)
        Me.btnInvoiceNo.Name = "btnInvoiceNo"
        Me.btnInvoiceNo.Size = New System.Drawing.Size(32, 23)
        Me.btnInvoiceNo.TabIndex = 140
        Me.btnInvoiceNo.Text = "..."
        Me.btnInvoiceNo.UseVisualStyleBackColor = False
        '
        'btnSupplierCode
        '
        Me.btnSupplierCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSupplierCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSupplierCode.Image = CType(resources.GetObject("btnSupplierCode.Image"), System.Drawing.Image)
        Me.btnSupplierCode.Location = New System.Drawing.Point(353, 56)
        Me.btnSupplierCode.Name = "btnSupplierCode"
        Me.btnSupplierCode.Size = New System.Drawing.Size(32, 23)
        Me.btnSupplierCode.TabIndex = 139
        Me.btnSupplierCode.Text = "..."
        Me.btnSupplierCode.UseVisualStyleBackColor = False
        '
        'txtIssued2
        '
        Me.txtIssued2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIssued2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIssued2.Location = New System.Drawing.Point(802, 91)
        Me.txtIssued2.MaxLength = 20
        Me.txtIssued2.Name = "txtIssued2"
        Me.txtIssued2.Size = New System.Drawing.Size(104, 20)
        Me.txtIssued2.TabIndex = 76
        Me.txtIssued2.Tag = "NE;"
        '
        'txtNetIssue1
        '
        Me.txtNetIssue1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNetIssue1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetIssue1.Location = New System.Drawing.Point(802, 58)
        Me.txtNetIssue1.MaxLength = 20
        Me.txtNetIssue1.Name = "txtNetIssue1"
        Me.txtNetIssue1.Size = New System.Drawing.Size(104, 20)
        Me.txtNetIssue1.TabIndex = 75
        Me.txtNetIssue1.Tag = "NE;"
        '
        'txtIssueNo1
        '
        Me.txtIssueNo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIssueNo1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIssueNo1.Location = New System.Drawing.Point(113, 29)
        Me.txtIssueNo1.MaxLength = 20
        Me.txtIssueNo1.Name = "txtIssueNo1"
        Me.txtIssueNo1.Size = New System.Drawing.Size(272, 20)
        Me.txtIssueNo1.TabIndex = 74
        Me.txtIssueNo1.Tag = "NE;"
        '
        'txtInvoiceNo1
        '
        Me.txtInvoiceNo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvoiceNo1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceNo1.Location = New System.Drawing.Point(113, 91)
        Me.txtInvoiceNo1.MaxLength = 20
        Me.txtInvoiceNo1.Name = "txtInvoiceNo1"
        Me.txtInvoiceNo1.Size = New System.Drawing.Size(272, 20)
        Me.txtInvoiceNo1.TabIndex = 73
        Me.txtInvoiceNo1.Tag = "NE;"
        '
        'txtSupplierCode1
        '
        Me.txtSupplierCode1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierCode1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierCode1.Location = New System.Drawing.Point(113, 58)
        Me.txtSupplierCode1.MaxLength = 20
        Me.txtSupplierCode1.Name = "txtSupplierCode1"
        Me.txtSupplierCode1.Size = New System.Drawing.Size(272, 20)
        Me.txtSupplierCode1.TabIndex = 72
        Me.txtSupplierCode1.Tag = "NE;"
        '
        'Label22
        '
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(392, 96)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(56, 16)
        Me.Label22.TabIndex = 71
        Me.Label22.Text = "Remarks"
        '
        'Label26
        '
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(912, 64)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(66, 16)
        Me.Label26.TabIndex = 70
        Me.Label26.Text = "(24K - 999)"
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(912, 72)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(66, 16)
        Me.Label15.TabIndex = 68
        Me.Label15.Text = "(22K - 916)"
        Me.Label15.Visible = False
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(912, 96)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(64, 16)
        Me.Label14.TabIndex = 67
        Me.Label14.Text = "(22K - 916)"
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(721, 64)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(104, 16)
        Me.Label11.TabIndex = 64
        Me.Label11.Text = "Net Issued Wt    "
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(721, 96)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(64, 16)
        Me.Label7.TabIndex = 61
        Me.Label7.Text = "Issued"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(696, 56)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(64, 19)
        Me.Label6.TabIndex = 60
        Me.Label6.Text = "Payable"
        Me.Label6.Visible = False
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(24, 97)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 16)
        Me.Label5.TabIndex = 58
        Me.Label5.Text = "Invoice No"
        '
        'txtRate
        '
        Me.txtRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRate.Location = New System.Drawing.Point(800, 24)
        Me.txtRate.MaxLength = 20
        Me.txtRate.Name = "txtRate"
        Me.txtRate.Size = New System.Drawing.Size(104, 20)
        Me.txtRate.TabIndex = 9
        Me.txtRate.Tag = "NE;"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(721, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 16)
        Me.Label4.TabIndex = 56
        Me.Label4.Text = "Rate"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(24, 96)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 16)
        Me.Label3.TabIndex = 55
        Me.Label3.Text = "Currency"
        Me.Label3.Visible = False
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(24, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 16)
        Me.Label2.TabIndex = 54
        Me.Label2.Text = "Supplier Code"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(24, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 16)
        Me.Label1.TabIndex = 53
        Me.Label1.Text = "Issue No"
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(608, 32)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(8, 16)
        Me.Label10.TabIndex = 52
        Me.Label10.Text = "/"
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(568, 32)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(8, 16)
        Me.Label9.TabIndex = 51
        Me.Label9.Text = "/"
        '
        'txtYYYY
        '
        Me.txtYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtYYYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtYYYY.Location = New System.Drawing.Point(616, 24)
        Me.txtYYYY.MaxLength = 20
        Me.txtYYYY.Name = "txtYYYY"
        Me.txtYYYY.Size = New System.Drawing.Size(48, 20)
        Me.txtYYYY.TabIndex = 4
        Me.txtYYYY.Tag = "NE;"
        '
        'txtMM
        '
        Me.txtMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMM.Location = New System.Drawing.Point(576, 24)
        Me.txtMM.MaxLength = 20
        Me.txtMM.Name = "txtMM"
        Me.txtMM.Size = New System.Drawing.Size(24, 20)
        Me.txtMM.TabIndex = 3
        Me.txtMM.Tag = "NE;"
        '
        'txtDD
        '
        Me.txtDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDD.Location = New System.Drawing.Point(536, 24)
        Me.txtDD.MaxLength = 20
        Me.txtDD.Name = "txtDD"
        Me.txtDD.Size = New System.Drawing.Size(25, 20)
        Me.txtDD.TabIndex = 2
        Me.txtDD.Tag = "NE;"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(433, 23)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(97, 24)
        Me.Label8.TabIndex = 50
        Me.Label8.Text = "Invoice Date"
        '
        'txtCurrencyName
        '
        Me.txtCurrencyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrencyName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencyName.Location = New System.Drawing.Point(456, 91)
        Me.txtCurrencyName.MaxLength = 20
        Me.txtCurrencyName.Name = "txtCurrencyName"
        Me.txtCurrencyName.Size = New System.Drawing.Size(256, 20)
        Me.txtCurrencyName.TabIndex = 8
        Me.txtCurrencyName.Tag = "NE;"
        '
        'txtsupplierName
        '
        Me.txtsupplierName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtsupplierName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsupplierName.Location = New System.Drawing.Point(400, 59)
        Me.txtsupplierName.MaxLength = 20
        Me.txtsupplierName.Name = "txtsupplierName"
        Me.txtsupplierName.Size = New System.Drawing.Size(272, 20)
        Me.txtsupplierName.TabIndex = 6
        Me.txtsupplierName.Tag = "NE;"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.btnInventoryCode)
        Me.GroupBox4.Controls.Add(Me.txtInventoryCode1)
        Me.GroupBox4.Controls.Add(Me.grdIssue1)
        Me.GroupBox4.Controls.Add(Me.btnPCS)
        Me.GroupBox4.Controls.Add(Me.txtInvInvoiceNo)
        Me.GroupBox4.Controls.Add(Me.txtDescription)
        Me.GroupBox4.Controls.Add(Me.Label12)
        Me.GroupBox4.Controls.Add(Me.Panel4)
        Me.GroupBox4.Controls.Add(Me.Label24)
        Me.GroupBox4.Controls.Add(Me.txtPcs)
        Me.GroupBox4.Controls.Add(Me.txtTotalCost)
        Me.GroupBox4.Controls.Add(Me.txtCostperGram)
        Me.GroupBox4.Controls.Add(Me.TxtTotalWeight)
        Me.GroupBox4.Controls.Add(Me.Label20)
        Me.GroupBox4.Controls.Add(Me.Label21)
        Me.GroupBox4.Controls.Add(Me.lblPcs)
        Me.GroupBox4.Controls.Add(Me.Label23)
        Me.GroupBox4.Controls.Add(Me.btnAdd)
        Me.GroupBox4.Controls.Add(Me.btnDelete)
        Me.GroupBox4.Location = New System.Drawing.Point(16, 370)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(984, 248)
        Me.GroupBox4.TabIndex = 68
        Me.GroupBox4.TabStop = False
        '
        'btnInventoryCode
        '
        Me.btnInventoryCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnInventoryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInventoryCode.Image = CType(resources.GetObject("btnInventoryCode.Image"), System.Drawing.Image)
        Me.btnInventoryCode.Location = New System.Drawing.Point(125, 191)
        Me.btnInventoryCode.Name = "btnInventoryCode"
        Me.btnInventoryCode.Size = New System.Drawing.Size(32, 23)
        Me.btnInventoryCode.TabIndex = 145
        Me.btnInventoryCode.Text = "..."
        Me.btnInventoryCode.UseVisualStyleBackColor = False
        '
        'txtInventoryCode1
        '
        Me.txtInventoryCode1.BackColor = System.Drawing.Color.White
        Me.txtInventoryCode1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInventoryCode1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInventoryCode1.ForeColor = System.Drawing.Color.Black
        Me.txtInventoryCode1.Location = New System.Drawing.Point(37, 192)
        Me.txtInventoryCode1.MaxLength = 20
        Me.txtInventoryCode1.Name = "txtInventoryCode1"
        Me.txtInventoryCode1.Size = New System.Drawing.Size(120, 20)
        Me.txtInventoryCode1.TabIndex = 144
        Me.txtInventoryCode1.Tag = ""
        '
        'grdIssue1
        '
        Me.grdIssue1.AllowUserToAddRows = False
        Me.grdIssue1.AllowUserToDeleteRows = False
        Me.grdIssue1.AllowUserToOrderColumns = True
        Me.grdIssue1.AllowUserToResizeColumns = False
        Me.grdIssue1.AllowUserToResizeRows = False
        Me.grdIssue1.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle37.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle37.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle37.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle37.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle37.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle37.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle37.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdIssue1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle37
        Me.grdIssue1.ColumnHeadersHeight = 40
        Me.grdIssue1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdIssue1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cIs, Me.cIIInventoryCode, Me.cIIDescription, Me.cIIWeight, Me.cIIQty, Me.cIICostPerGram, Me.cIITotalCost, Me.cIIBarNo})
        DataGridViewCellStyle38.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle38.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle38.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle38.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle38.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle38.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle38.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdIssue1.DefaultCellStyle = DataGridViewCellStyle38
        Me.grdIssue1.Location = New System.Drawing.Point(6, 19)
        Me.grdIssue1.Name = "grdIssue1"
        DataGridViewCellStyle39.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle39.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle39.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle39.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle39.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle39.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle39.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdIssue1.RowHeadersDefaultCellStyle = DataGridViewCellStyle39
        Me.grdIssue1.RowHeadersVisible = False
        Me.grdIssue1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.grdIssue1.Size = New System.Drawing.Size(882, 134)
        Me.grdIssue1.TabIndex = 143
        '
        'cIs
        '
        Me.cIs.HeaderText = "Invoice No"
        Me.cIs.Name = "cIs"
        '
        'cIIInventoryCode
        '
        Me.cIIInventoryCode.HeaderText = "Inventory Code"
        Me.cIIInventoryCode.Name = "cIIInventoryCode"
        '
        'cIIDescription
        '
        Me.cIIDescription.HeaderText = "Description"
        Me.cIIDescription.Name = "cIIDescription"
        '
        'cIIWeight
        '
        Me.cIIWeight.HeaderText = "Weight"
        Me.cIIWeight.Name = "cIIWeight"
        Me.cIIWeight.Visible = False
        Me.cIIWeight.Width = 80
        '
        'cIIQty
        '
        Me.cIIQty.HeaderText = "Qty"
        Me.cIIQty.Name = "cIIQty"
        '
        'cIICostPerGram
        '
        Me.cIICostPerGram.HeaderText = "CostPerGram"
        Me.cIICostPerGram.Name = "cIICostPerGram"
        Me.cIICostPerGram.Width = 80
        '
        'cIITotalCost
        '
        Me.cIITotalCost.HeaderText = "Total Cost"
        Me.cIITotalCost.Name = "cIITotalCost"
        Me.cIITotalCost.Visible = False
        '
        'cIIBarNo
        '
        Me.cIIBarNo.HeaderText = "BarNo"
        Me.cIIBarNo.Name = "cIIBarNo"
        '
        'btnPCS
        '
        Me.btnPCS.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPCS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPCS.Image = CType(resources.GetObject("btnPCS.Image"), System.Drawing.Image)
        Me.btnPCS.Location = New System.Drawing.Point(640, 192)
        Me.btnPCS.Name = "btnPCS"
        Me.btnPCS.Size = New System.Drawing.Size(32, 23)
        Me.btnPCS.TabIndex = 138
        Me.btnPCS.Text = "..."
        Me.btnPCS.UseVisualStyleBackColor = False
        '
        'txtInvInvoiceNo
        '
        Me.txtInvInvoiceNo.BackColor = System.Drawing.Color.White
        Me.txtInvInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvInvoiceNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvInvoiceNo.ForeColor = System.Drawing.Color.Black
        Me.txtInvInvoiceNo.Location = New System.Drawing.Point(328, 216)
        Me.txtInvInvoiceNo.MaxLength = 20
        Me.txtInvInvoiceNo.Name = "txtInvInvoiceNo"
        Me.txtInvInvoiceNo.Size = New System.Drawing.Size(237, 20)
        Me.txtInvInvoiceNo.TabIndex = 128
        Me.txtInvInvoiceNo.Tag = ""
        Me.txtInvInvoiceNo.Visible = False
        '
        'txtDescription
        '
        Me.txtDescription.BackColor = System.Drawing.Color.White
        Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.ForeColor = System.Drawing.Color.Black
        Me.txtDescription.Location = New System.Drawing.Point(163, 193)
        Me.txtDescription.MaxLength = 20
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(237, 20)
        Me.txtDescription.TabIndex = 13
        Me.txtDescription.Tag = ""
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.White
        Me.Label12.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label12.Location = New System.Drawing.Point(163, 161)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(181, 24)
        Me.Label12.TabIndex = 124
        Me.Label12.Text = "Description"
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Label17)
        Me.Panel4.Controls.Add(Me.Label16)
        Me.Panel4.Controls.Add(Me.Label13)
        Me.Panel4.Controls.Add(Me.Label18)
        Me.Panel4.Controls.Add(Me.Label19)
        Me.Panel4.Controls.Add(Me.TextBox1)
        Me.Panel4.Controls.Add(Me.txtClear)
        Me.Panel4.Controls.Add(Me.TextBox5)
        Me.Panel4.Controls.Add(Me.TextBox4)
        Me.Panel4.Controls.Add(Me.TextBox3)
        Me.Panel4.Controls.Add(Me.TextBox2)
        Me.Panel4.Location = New System.Drawing.Point(15, -80)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(611, 80)
        Me.Panel4.TabIndex = 105
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.Pink
        Me.Label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label17.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(489, 3)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(112, 24)
        Me.Label17.TabIndex = 118
        Me.Label17.Text = "Total Cost"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Pink
        Me.Label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label16.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(369, 3)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(120, 24)
        Me.Label16.TabIndex = 117
        Me.Label16.Text = "Cost Per Gram"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Pink
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(249, 3)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(120, 24)
        Me.Label13.TabIndex = 116
        Me.Label13.Text = "Gold Loss"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.Pink
        Me.Label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label18.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(129, 3)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(120, 24)
        Me.Label18.TabIndex = 115
        Me.Label18.Text = "Weight"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.Pink
        Me.Label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label19.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(6, 3)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(123, 24)
        Me.Label19.TabIndex = 114
        Me.Label19.Text = "Inventory Code"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.ForeColor = System.Drawing.Color.Black
        Me.TextBox1.Location = New System.Drawing.Point(249, 27)
        Me.TextBox1.MaxLength = 20
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(120, 20)
        Me.TextBox1.TabIndex = 113
        Me.TextBox1.Tag = ""
        '
        'txtClear
        '
        Me.txtClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClear.Image = CType(resources.GetObject("txtClear.Image"), System.Drawing.Image)
        Me.txtClear.Location = New System.Drawing.Point(526, 50)
        Me.txtClear.Name = "txtClear"
        Me.txtClear.Size = New System.Drawing.Size(75, 23)
        Me.txtClear.TabIndex = 111
        Me.txtClear.Text = "Clear"
        Me.txtClear.UseVisualStyleBackColor = False
        '
        'TextBox5
        '
        Me.TextBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox5.ForeColor = System.Drawing.Color.Black
        Me.TextBox5.Location = New System.Drawing.Point(489, 27)
        Me.TextBox5.MaxLength = 20
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(112, 20)
        Me.TextBox5.TabIndex = 106
        Me.TextBox5.Tag = ""
        '
        'TextBox4
        '
        Me.TextBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox4.ForeColor = System.Drawing.Color.Black
        Me.TextBox4.Location = New System.Drawing.Point(369, 27)
        Me.TextBox4.MaxLength = 20
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(120, 20)
        Me.TextBox4.TabIndex = 105
        Me.TextBox4.Tag = ""
        '
        'TextBox3
        '
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox3.ForeColor = System.Drawing.Color.Black
        Me.TextBox3.Location = New System.Drawing.Point(129, 27)
        Me.TextBox3.MaxLength = 20
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(120, 20)
        Me.TextBox3.TabIndex = 104
        Me.TextBox3.Tag = ""
        '
        'TextBox2
        '
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.ForeColor = System.Drawing.Color.Black
        Me.TextBox2.Location = New System.Drawing.Point(6, 27)
        Me.TextBox2.MaxLength = 20
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(123, 20)
        Me.TextBox2.TabIndex = 67
        Me.TextBox2.Tag = ""
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.White
        Me.Label24.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Black
        Me.Label24.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label24.Location = New System.Drawing.Point(40, 161)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(112, 24)
        Me.Label24.TabIndex = 114
        Me.Label24.Text = "Inventory Code"
        '
        'txtPcs
        '
        Me.txtPcs.BackColor = System.Drawing.Color.White
        Me.txtPcs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPcs.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPcs.ForeColor = System.Drawing.Color.Black
        Me.txtPcs.Location = New System.Drawing.Point(552, 193)
        Me.txtPcs.MaxLength = 20
        Me.txtPcs.Name = "txtPcs"
        Me.txtPcs.Size = New System.Drawing.Size(120, 20)
        Me.txtPcs.TabIndex = 15
        Me.txtPcs.Tag = ""
        '
        'txtTotalCost
        '
        Me.txtTotalCost.BackColor = System.Drawing.Color.White
        Me.txtTotalCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalCost.Enabled = False
        Me.txtTotalCost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalCost.ForeColor = System.Drawing.Color.Black
        Me.txtTotalCost.Location = New System.Drawing.Point(824, 193)
        Me.txtTotalCost.MaxLength = 20
        Me.txtTotalCost.Name = "txtTotalCost"
        Me.txtTotalCost.Size = New System.Drawing.Size(112, 20)
        Me.txtTotalCost.TabIndex = 17
        Me.txtTotalCost.Tag = ""
        '
        'txtCostperGram
        '
        Me.txtCostperGram.BackColor = System.Drawing.Color.White
        Me.txtCostperGram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCostperGram.Enabled = False
        Me.txtCostperGram.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostperGram.ForeColor = System.Drawing.Color.Black
        Me.txtCostperGram.Location = New System.Drawing.Point(688, 193)
        Me.txtCostperGram.MaxLength = 20
        Me.txtCostperGram.Name = "txtCostperGram"
        Me.txtCostperGram.Size = New System.Drawing.Size(120, 20)
        Me.txtCostperGram.TabIndex = 16
        Me.txtCostperGram.Tag = ""
        '
        'TxtTotalWeight
        '
        Me.TxtTotalWeight.BackColor = System.Drawing.Color.White
        Me.TxtTotalWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtTotalWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTotalWeight.ForeColor = System.Drawing.Color.Black
        Me.TxtTotalWeight.Location = New System.Drawing.Point(408, 193)
        Me.TxtTotalWeight.MaxLength = 20
        Me.TxtTotalWeight.Name = "TxtTotalWeight"
        Me.TxtTotalWeight.Size = New System.Drawing.Size(120, 20)
        Me.TxtTotalWeight.TabIndex = 14
        Me.TxtTotalWeight.Tag = ""
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.White
        Me.Label20.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label20.Location = New System.Drawing.Point(824, 161)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(112, 24)
        Me.Label20.TabIndex = 118
        Me.Label20.Text = "Total Cost"
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.White
        Me.Label21.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Black
        Me.Label21.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label21.Location = New System.Drawing.Point(688, 161)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(120, 24)
        Me.Label21.TabIndex = 117
        Me.Label21.Text = "Cost Per Gram"
        '
        'lblPcs
        '
        Me.lblPcs.BackColor = System.Drawing.Color.White
        Me.lblPcs.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPcs.ForeColor = System.Drawing.Color.Black
        Me.lblPcs.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblPcs.Location = New System.Drawing.Point(552, 161)
        Me.lblPcs.Name = "lblPcs"
        Me.lblPcs.Size = New System.Drawing.Size(120, 24)
        Me.lblPcs.TabIndex = 116
        Me.lblPcs.Text = "Qty"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.White
        Me.Label23.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.Black
        Me.Label23.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label23.Location = New System.Drawing.Point(408, 161)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(120, 24)
        Me.Label23.TabIndex = 115
        Me.Label23.Text = "Weight"
        '
        'btnAdd
        '
        Me.btnAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnAdd.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.Image = CType(resources.GetObject("btnAdd.Image"), System.Drawing.Image)
        Me.btnAdd.Location = New System.Drawing.Point(800, 216)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 18
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.Location = New System.Drawing.Point(880, 216)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 110
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'grdIssue
        '
        Me.grdIssue.Location = New System.Drawing.Point(15, 28)
        Me.grdIssue.Name = "grdIssue"
        Me.grdIssue.OcxState = CType(resources.GetObject("grdIssue.OcxState"), System.Windows.Forms.AxHost.State)
        Me.grdIssue.Size = New System.Drawing.Size(935, 114)
        Me.grdIssue.TabIndex = 125
        '
        'grdUnpaidBill
        '
        Me.grdUnpaidBill.Location = New System.Drawing.Point(16, 55)
        Me.grdUnpaidBill.Name = "grdUnpaidBill"
        Me.grdUnpaidBill.OcxState = CType(resources.GetObject("grdUnpaidBill.OcxState"), System.Windows.Forms.AxHost.State)
        Me.grdUnpaidBill.Size = New System.Drawing.Size(935, 114)
        Me.grdUnpaidBill.TabIndex = 18
        Me.grdUnpaidBill.Visible = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(888, 624)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 16
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.Location = New System.Drawing.Point(728, 624)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(76, 23)
        Me.btnSave.TabIndex = 14
        Me.btnSave.Text = "Save(F7)"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(808, 624)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(75, 23)
        Me.btnNew.TabIndex = 15
        Me.btnNew.Text = "Clear(F6)"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'pnlInvoice
        '
        Me.pnlInvoice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlInvoice.Controls.Add(Me.grdInvoice)
        Me.pnlInvoice.Controls.Add(Me.btnPanelClose)
        Me.pnlInvoice.Controls.Add(Me.chkSelectAll)
        Me.pnlInvoice.Controls.Add(Me.btnPanelOK)
        Me.pnlInvoice.Location = New System.Drawing.Point(240, 273)
        Me.pnlInvoice.Name = "pnlInvoice"
        Me.pnlInvoice.Size = New System.Drawing.Size(460, 195)
        Me.pnlInvoice.TabIndex = 136
        Me.pnlInvoice.Visible = False
        '
        'grdInvoice
        '
        Me.grdInvoice.AllowUserToAddRows = False
        Me.grdInvoice.AllowUserToDeleteRows = False
        Me.grdInvoice.AllowUserToOrderColumns = True
        Me.grdInvoice.AllowUserToResizeColumns = False
        Me.grdInvoice.AllowUserToResizeRows = False
        Me.grdInvoice.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle40.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle40.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle40.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle40.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle40.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle40.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle40.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdInvoice.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle40
        Me.grdInvoice.ColumnHeadersHeight = 40
        Me.grdInvoice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdInvoice.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cSel, Me.cJobSheetNo, Me.CInvoiceNo, Me.cSupplierCode, Me.cWeight, Me.cCustomerCode, Me.cBalanceWeight})
        DataGridViewCellStyle41.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle41.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle41.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle41.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle41.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle41.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle41.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdInvoice.DefaultCellStyle = DataGridViewCellStyle41
        Me.grdInvoice.Location = New System.Drawing.Point(8, 11)
        Me.grdInvoice.Name = "grdInvoice"
        DataGridViewCellStyle42.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle42.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle42.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle42.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle42.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle42.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle42.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdInvoice.RowHeadersDefaultCellStyle = DataGridViewCellStyle42
        Me.grdInvoice.RowHeadersVisible = False
        Me.grdInvoice.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.grdInvoice.Size = New System.Drawing.Size(431, 134)
        Me.grdInvoice.TabIndex = 141
        '
        'cSel
        '
        Me.cSel.HeaderText = "Sel"
        Me.cSel.Name = "cSel"
        Me.cSel.Width = 30
        '
        'cJobSheetNo
        '
        Me.cJobSheetNo.HeaderText = "JobSheet No"
        Me.cJobSheetNo.Name = "cJobSheetNo"
        '
        'CInvoiceNo
        '
        Me.CInvoiceNo.HeaderText = "Invoice No"
        Me.CInvoiceNo.Name = "CInvoiceNo"
        '
        'cSupplierCode
        '
        Me.cSupplierCode.HeaderText = "SupplierCode"
        Me.cSupplierCode.Name = "cSupplierCode"
        '
        'cWeight
        '
        Me.cWeight.HeaderText = "Weight"
        Me.cWeight.Name = "cWeight"
        Me.cWeight.Width = 80
        '
        'cCustomerCode
        '
        Me.cCustomerCode.HeaderText = "Customer Code"
        Me.cCustomerCode.Name = "cCustomerCode"
        Me.cCustomerCode.Visible = False
        '
        'cBalanceWeight
        '
        Me.cBalanceWeight.HeaderText = "Balance Weight"
        Me.cBalanceWeight.Name = "cBalanceWeight"
        Me.cBalanceWeight.Visible = False
        Me.cBalanceWeight.Width = 80
        '
        'btnPanelClose
        '
        Me.btnPanelClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPanelClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPanelClose.Image = CType(resources.GetObject("btnPanelClose.Image"), System.Drawing.Image)
        Me.btnPanelClose.Location = New System.Drawing.Point(344, 154)
        Me.btnPanelClose.Name = "btnPanelClose"
        Me.btnPanelClose.Size = New System.Drawing.Size(92, 23)
        Me.btnPanelClose.TabIndex = 139
        Me.btnPanelClose.Text = "Close"
        Me.btnPanelClose.UseVisualStyleBackColor = False
        '
        'chkSelectAll
        '
        Me.chkSelectAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSelectAll.Location = New System.Drawing.Point(8, 160)
        Me.chkSelectAll.Name = "chkSelectAll"
        Me.chkSelectAll.Size = New System.Drawing.Size(80, 24)
        Me.chkSelectAll.TabIndex = 138
        Me.chkSelectAll.Text = "Select All"
        '
        'btnPanelOK
        '
        Me.btnPanelOK.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPanelOK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPanelOK.Image = CType(resources.GetObject("btnPanelOK.Image"), System.Drawing.Image)
        Me.btnPanelOK.Location = New System.Drawing.Point(248, 154)
        Me.btnPanelOK.Name = "btnPanelOK"
        Me.btnPanelOK.Size = New System.Drawing.Size(92, 23)
        Me.btnPanelOK.TabIndex = 136
        Me.btnPanelOK.Text = "OK"
        Me.btnPanelOK.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.grdGoldIssue)
        Me.Panel1.Controls.Add(Me.btnIssueOk)
        Me.Panel1.Location = New System.Drawing.Point(16, 206)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(976, 168)
        Me.Panel1.TabIndex = 137
        '
        'grdGoldIssue
        '
        Me.grdGoldIssue.AllowUserToAddRows = False
        Me.grdGoldIssue.AllowUserToDeleteRows = False
        Me.grdGoldIssue.AllowUserToOrderColumns = True
        Me.grdGoldIssue.AllowUserToResizeColumns = False
        Me.grdGoldIssue.AllowUserToResizeRows = False
        Me.grdGoldIssue.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle43.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle43.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle43.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle43.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle43.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle43.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle43.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdGoldIssue.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle43
        Me.grdGoldIssue.ColumnHeadersHeight = 40
        Me.grdGoldIssue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdGoldIssue.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cIInvoiceNo, Me.cIInvoiceDate, Me.cIPurchaseBillNo, Me.cIClass, Me.cIPurchaseWeight, Me.cIIssuedWeight, Me.cIBalanceWeight})
        DataGridViewCellStyle44.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle44.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle44.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle44.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle44.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle44.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle44.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdGoldIssue.DefaultCellStyle = DataGridViewCellStyle44
        Me.grdGoldIssue.Location = New System.Drawing.Point(26, 16)
        Me.grdGoldIssue.Name = "grdGoldIssue"
        DataGridViewCellStyle45.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle45.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle45.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle45.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle45.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle45.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle45.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdGoldIssue.RowHeadersDefaultCellStyle = DataGridViewCellStyle45
        Me.grdGoldIssue.RowHeadersVisible = False
        Me.grdGoldIssue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.grdGoldIssue.Size = New System.Drawing.Size(602, 134)
        Me.grdGoldIssue.TabIndex = 142
        '
        'cIInvoiceNo
        '
        Me.cIInvoiceNo.HeaderText = "Invoice No"
        Me.cIInvoiceNo.Name = "cIInvoiceNo"
        '
        'cIInvoiceDate
        '
        Me.cIInvoiceDate.HeaderText = "InvoiceDate"
        Me.cIInvoiceDate.Name = "cIInvoiceDate"
        '
        'cIPurchaseBillNo
        '
        Me.cIPurchaseBillNo.HeaderText = "Purchase Bill No"
        Me.cIPurchaseBillNo.Name = "cIPurchaseBillNo"
        '
        'cIClass
        '
        Me.cIClass.HeaderText = "Class"
        Me.cIClass.Name = "cIClass"
        Me.cIClass.Visible = False
        Me.cIClass.Width = 80
        '
        'cIPurchaseWeight
        '
        Me.cIPurchaseWeight.HeaderText = "Purchase Weight"
        Me.cIPurchaseWeight.Name = "cIPurchaseWeight"
        '
        'cIIssuedWeight
        '
        Me.cIIssuedWeight.HeaderText = "Issued Weight"
        Me.cIIssuedWeight.Name = "cIIssuedWeight"
        Me.cIIssuedWeight.Width = 80
        '
        'cIBalanceWeight
        '
        Me.cIBalanceWeight.HeaderText = "Balance Weight"
        Me.cIBalanceWeight.Name = "cIBalanceWeight"
        Me.cIBalanceWeight.Visible = False
        '
        'btnIssueOk
        '
        Me.btnIssueOk.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnIssueOk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnIssueOk.Image = CType(resources.GetObject("btnIssueOk.Image"), System.Drawing.Image)
        Me.btnIssueOk.Location = New System.Drawing.Point(656, 40)
        Me.btnIssueOk.Name = "btnIssueOk"
        Me.btnIssueOk.Size = New System.Drawing.Size(92, 23)
        Me.btnIssueOk.TabIndex = 137
        Me.btnIssueOk.Text = "OK"
        Me.btnIssueOk.UseVisualStyleBackColor = False
        '
        'pnlPCS
        '
        Me.pnlPCS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlPCS.Controls.Add(Me.grdPCS1)
        Me.pnlPCS.Controls.Add(Me.btnPanePCSClose)
        Me.pnlPCS.Controls.Add(Me.chkPcsSelectAll)
        Me.pnlPCS.Controls.Add(Me.btnPanelPCS)
        Me.pnlPCS.Location = New System.Drawing.Point(752, 341)
        Me.pnlPCS.Name = "pnlPCS"
        Me.pnlPCS.Size = New System.Drawing.Size(242, 195)
        Me.pnlPCS.TabIndex = 138
        Me.pnlPCS.Visible = False
        '
        'grdPCS1
        '
        Me.grdPCS1.AllowUserToAddRows = False
        Me.grdPCS1.AllowUserToDeleteRows = False
        Me.grdPCS1.AllowUserToOrderColumns = True
        Me.grdPCS1.AllowUserToResizeColumns = False
        Me.grdPCS1.AllowUserToResizeRows = False
        Me.grdPCS1.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle46.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle46.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle46.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle46.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle46.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle46.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle46.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdPCS1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle46
        Me.grdPCS1.ColumnHeadersHeight = 40
        Me.grdPCS1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdPCS1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cBSelect, Me.cBBarNo})
        DataGridViewCellStyle47.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle47.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle47.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle47.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle47.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle47.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle47.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdPCS1.DefaultCellStyle = DataGridViewCellStyle47
        Me.grdPCS1.Location = New System.Drawing.Point(3, 10)
        Me.grdPCS1.Name = "grdPCS1"
        DataGridViewCellStyle48.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle48.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle48.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle48.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle48.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle48.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle48.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdPCS1.RowHeadersDefaultCellStyle = DataGridViewCellStyle48
        Me.grdPCS1.RowHeadersVisible = False
        Me.grdPCS1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.grdPCS1.Size = New System.Drawing.Size(222, 134)
        Me.grdPCS1.TabIndex = 142
        '
        'cBSelect
        '
        Me.cBSelect.HeaderText = "Sel"
        Me.cBSelect.Name = "cBSelect"
        Me.cBSelect.Width = 30
        '
        'cBBarNo
        '
        Me.cBBarNo.HeaderText = "Bar No"
        Me.cBBarNo.Name = "cBBarNo"
        '
        'btnPanePCSClose
        '
        Me.btnPanePCSClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPanePCSClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPanePCSClose.Image = CType(resources.GetObject("btnPanePCSClose.Image"), System.Drawing.Image)
        Me.btnPanePCSClose.Location = New System.Drawing.Point(168, 160)
        Me.btnPanePCSClose.Name = "btnPanePCSClose"
        Me.btnPanePCSClose.Size = New System.Drawing.Size(56, 23)
        Me.btnPanePCSClose.TabIndex = 139
        Me.btnPanePCSClose.Text = "Close"
        Me.btnPanePCSClose.UseVisualStyleBackColor = False
        '
        'chkPcsSelectAll
        '
        Me.chkPcsSelectAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPcsSelectAll.Location = New System.Drawing.Point(8, 160)
        Me.chkPcsSelectAll.Name = "chkPcsSelectAll"
        Me.chkPcsSelectAll.Size = New System.Drawing.Size(80, 24)
        Me.chkPcsSelectAll.TabIndex = 138
        Me.chkPcsSelectAll.Text = "Select All"
        '
        'btnPanelPCS
        '
        Me.btnPanelPCS.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPanelPCS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPanelPCS.Image = CType(resources.GetObject("btnPanelPCS.Image"), System.Drawing.Image)
        Me.btnPanelPCS.Location = New System.Drawing.Point(104, 160)
        Me.btnPanelPCS.Name = "btnPanelPCS"
        Me.btnPanelPCS.Size = New System.Drawing.Size(56, 23)
        Me.btnPanelPCS.TabIndex = 136
        Me.btnPanelPCS.Text = "OK"
        Me.btnPanelPCS.UseVisualStyleBackColor = False
        '
        'grdPCS
        '
        Me.grdPCS.Location = New System.Drawing.Point(8, 11)
        Me.grdPCS.Name = "grdPCS"
        Me.grdPCS.OcxState = CType(resources.GetObject("grdPCS.OcxState"), System.Windows.Forms.AxHost.State)
        Me.grdPCS.Size = New System.Drawing.Size(216, 133)
        Me.grdPCS.TabIndex = 135
        '
        'btnRePrint
        '
        Me.btnRePrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnRePrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRePrint.Image = CType(resources.GetObject("btnRePrint.Image"), System.Drawing.Image)
        Me.btnRePrint.Location = New System.Drawing.Point(632, 624)
        Me.btnRePrint.Name = "btnRePrint"
        Me.btnRePrint.Size = New System.Drawing.Size(80, 23)
        Me.btnRePrint.TabIndex = 139
        Me.btnRePrint.Text = "Re-Print(F9)"
        Me.btnRePrint.UseVisualStyleBackColor = False
        '
        'frmGoldBarIssueNew
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.HighlightText
        Me.ClientSize = New System.Drawing.Size(1042, 655)
        Me.Controls.Add(Me.pnlInvoice)
        Me.Controls.Add(Me.btnRePrint)
        Me.Controls.Add(Me.pnlPCS)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox4)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmGoldBarIssueNew"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Gold Bar IssueNew"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.grdIssue1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.grdIssue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdUnpaidBill, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlInvoice.ResumeLayout(False)
        CType(Me.grdInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.grdGoldIssue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPCS.ResumeLayout(False)
        CType(Me.grdPCS1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdPCS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmGoldBarIssueNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' subGridSetting(grdGoldIssue)
        ' subGridSetting(grdUnpaidBill)
        txtDD.Text = Format(Date.Now.Day, "00")
        txtMM.Text = Format(Date.Now.Month, "00")
        txtYYYY.Text = Date.Now.Year
        'subGetGoldBarDetail()
        clearall()
    End Sub
    Private Sub clearall()
        txtSupplierCode1.Text = ""
        txtsupplierName.Text = ""
        'txtCurrencyCode.Text = ""
        'txtCurrencyName.Text = ""
        txtRate.Text = ""
        txtInvoiceNo1.Text = ""
        ' txtPayable.Text = ""
        txtIssueNo1.Text = ""
        txtIssued2.Text = "0.00"
        txtNetIssue1.Text = "0.00"
        'txtSupplierCode1.ControlType = EditLib.ControlTypeConstants.ControlTypeNormal
        '  grdUnpaidBill.MaxRows = 0
        grdGoldIssue.RowCount = 0
        grdIssue1.RowCount = 0

        ' grdGoldIssue.MaxRows = 0
    End Sub
    'Private Sub subGetGoldBarDetail()
    '    Dim Ds As DataSet
    '    Dim i As Long
    '    Try
    '        Ds = objPurchase.GetGoldBarDetails
    '        If Ds.Tables("GoldBarPurchase").Rows.Count > 0 Then
    '            grdGoldIssue.MaxRows = 0
    '            For i = 0 To Ds.Tables("GoldBarPurchase").Rows.Count - 1
    '                grdGoldIssue.MaxRows += 1
    '                SetText(grdGoldIssue, grdGoldIssue.MaxRows, 1, Ds.Tables("GoldBarPurchase").Rows(i)("InvoiceNo"))
    '                SetText(grdGoldIssue, grdGoldIssue.MaxRows, 2, Ds.Tables("GoldBarPurchase").Rows(i)("InvoiceDate"))
    '                SetText(grdGoldIssue, grdGoldIssue.MaxRows, 3, "24K-999")
    '                SetText(grdGoldIssue, grdGoldIssue.MaxRows, 4, Ds.Tables("GoldBarPurchase").Rows(i)("TotalWeight"))
    '                SetText(grdGoldIssue, grdGoldIssue.MaxRows, 5, Ds.Tables("GoldBarPurchase").Rows(i)("IssuedWeight"))
    '                SetText(grdGoldIssue, grdGoldIssue.MaxRows, 6, Ds.Tables("GoldBarPurchase").Rows(i)("Balanceweight"))
    '            Next i
    '        End If
    '        Ds = Nothing
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '        Ds = Nothing
    '    End Try
    'End Sub
    Private Sub txtSupplierCode1_ButtonHit(ByVal sender As Object, ByVal e As AxEditLib._DfpText_ButtonHitEvent)
        Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSupplier", "L", 4, "SupplierName", "", "SupplierCode", "SupplierCode", 80, "SupplierName", "SupplierName", 180, "Address1", "Address1", 60, "Address2", "Address2", 80)
        frmHelpClass.ShowDialog()
        txtSupplierCode1.Text = frmHelpClass.Var1.Trim
        txtsupplierName.Text = frmHelpClass.Var2.Trim
        subGetpurchaseBill()
    End Sub

    Private Sub subGetpurchaseBill()
        Dim Ds As DataSet
        Dim i As Long
        Try
            Ds = objPurchase.GetPurchaseBill(txtSupplierCode1.Text.Trim)
            If Ds.Tables("PurchaseBill").Rows.Count > 0 Then
                grdUnpaidBill.MaxRows = 0
                For i = 0 To Ds.Tables("PurchaseBill").Rows.Count - 1
                    grdUnpaidBill.MaxRows += 1
                    SetText(grdUnpaidBill, grdUnpaidBill.MaxRows, 1, Ds.Tables("PurchaseBill").Rows(i)("InvoiceNo"))
                    SetText(grdUnpaidBill, grdUnpaidBill.MaxRows, 2, Ds.Tables("PurchaseBill").Rows(i)("InvoiceDate"))
                    SetText(grdUnpaidBill, grdUnpaidBill.MaxRows, 3, Ds.Tables("PurchaseBill").Rows(i)("PurchaseWeight"))
                    SetText(grdUnpaidBill, grdUnpaidBill.MaxRows, 4, Ds.Tables("PurchaseBill").Rows(i)("IssuedWeight"))
                    'SetText(grdUnpaidBill, grdUnpaidBill.MaxRows, 5, Ds.Tables("PurchaseBill").Rows(i)("BalanceWeight"))
                    txtPayable.Text = Format((Val(txtPayable.Text) + Val(GetText(grdUnpaidBill, grdUnpaidBill.MaxRows, 5))), "0.00")
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtSupplierCode1_KeyDownEvent(ByVal sender As Object, ByVal e As AxEditLib._DfpText_KeyDownEvent)
        If e.keyCode = Keys.F1 Then
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSupplier", "L", 4, "SupplierName", "", "SupplierCode", "SupplierCode", 80, "SupplierName", "SupplierName", 180, "Address1", "Address1", 60, "Address2", "Address2", 80)
            frmHelpClass.ShowDialog()
            txtSupplierCode1.Text = frmHelpClass.Var1.Trim
            txtsupplierName.Text = frmHelpClass.Var2.Trim
        End If
    End Sub
    'Private Sub GetCurrency()
    '    Dim objcurrecny As New clsCurrency
    '    Dim Ds As DataSet
    '    Try
    '        If txtCurrencyCode.Text.Trim <> "" Then
    '            Ds = objcurrecny.GetCurrency(txtCurrencyCode.Text.Trim)
    '            If Ds.Tables("Currency").Rows.Count > 0 Then
    '                txtRate.Text = Format(Ds.Tables("Currency").Rows(0)("CurrencyRate"), "0.00")
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    'End Sub
    'Private Sub txtCurrencyCode_ButtonHit(ByVal sender As Object, ByVal e As AxEditLib._DfpText_ButtonHitEvent)
    '    Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCurrency", "L", 4, "CurrencyName", "", "CurrencyCode", "CurrencyCode", 80, "CurrencyName", "CurrencyName", 180, "CurrencyRate", "CurrencyRate", 60, "Remarks", "Remarks", 80)
    '    frmHelpClass.ShowDialog()
    '    txtCurrencyCode.Text = frmHelpClass.Var1.Trim
    '    txtCurrencyName.Text = frmHelpClass.Var2.Trim
    '    'txtRate.Text = frmHelpClass.Var3.Trim
    '    GetCurrency()
    'End Sub
    'Private Sub txtCurrencyCode_KeyDownEvent(ByVal sender As Object, ByVal e As AxEditLib._DfpText_KeyDownEvent)
    '    If e.keyCode = Keys.F1 Then
    '        Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCurrency", "L", 4, "CurrencyName", "", "CurrencyCode", "CurrencyCode", 80, "CurrencyName", "CurrencyName", 180, "CurrencyRate", "CurrencyRate", 60, "Remarks", "Remarks", 80)
    '        frmHelpClass.ShowDialog()
    '        txtCurrencyCode.Text = frmHelpClass.Var1.Trim
    '        txtCurrencyName.Text = frmHelpClass.Var2.Trim
    '        'txtRate.Text = frmHelpClass.Var3.Trim
    '        GetCurrency()
    '    End If
    'End Sub

    Private Sub frmGoldBarIssueNew_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F6
                btnNew_Click(sender, e)
            Case Keys.F7
                btnSave_Click(sender, e)
            Case Keys.Enter
                SendKeys.Send("{Tab}")
            Case Keys.Up
                SendKeys.Send("+{Tab}")
            Case Keys.Down
                SendKeys.Send("{Tab}")
        End Select
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim Ds As DataSet
        Dim TotPurchaseweight As Double
        Dim TempPurchaseWeight As Double
        Dim i As Long
        Dim sExec As String
        Dim sDate As String
        Try

            '''      If Not fncValidation() Then Exit Sub
            bProceed = False

            If Not fncGoldBarIssueNo() Then
                Exit Sub
            End If
            If Val(txtNetIssue1.Text) <> Val(txtIssued2.Text) Then
                MessageBox.Show("Issued Weight and Invoice Weight Not Tally")
                Exit Sub
            End If


            sDate = Trim(txtDD.Text) & "/" & Trim(txtMM.Text) & "/" & Trim(txtYYYY.Text)

            If objPurchase.GoldBarIssueProceed(Trim(sGoldBarIssueNo), "", Trim(sDate), txtSupplierCode1.Text.Trim, 0, 0, Val(txtIssued2.Text), Val(txtNetIssue1.Text), "", "", "24K-999", 0, "G", sUserID) Then

            End If


            For i = 0 To grdGoldIssue.RowCount - 1
                If objPurchase.GoldBarIssueProceedInvoice(Trim(sGoldBarIssueNo), Trim(GetText(grdGoldIssue, i, 3)), Trim(GetText(grdGoldIssue, i, 2)), txtSupplierCode1.Text.Trim, Val(GetText(grdGoldIssue, i, 5)), Val(GetText(grdGoldIssue, i, 6)), sUserID) Then
                End If

                sExec = "UpDate tblPurchaseFix Set Issuedweight = IssuedWeight + "
                sExec = sExec & Val(GetText(grdGoldIssue, i, 6)) & ""
                sExec = sExec & " Where InvoiceNo =" & fncFormatSQLText(GetText(grdGoldIssue, i, 3))
                dm.ProjExecNonQry(sExec)

            Next

            For i = 1 To grdIssue.MaxRows - 1
                If objPurchase.GoldBarIssueProceedPurchase(Trim(sGoldBarIssueNo), Trim(GetText(grdIssue, i, 2)), Trim(GetText(grdIssue, i, 3)), Val(GetText(grdIssue, i, 4)), Val(GetText(grdIssue, i, 5)), Val(GetText(grdIssue, i, 6)), Val(GetText(grdIssue, i, 7)), Trim(GetText(grdIssue, i, 8)), sUserID) Then
                End If



                sExec = "UpDate tblinventory Set Pcs = Pcs - "
                sExec = sExec & Val(GetText(grdIssue, i, 5)) & ", QtyonHand = QtyonHand - "
                sExec = sExec & Val(GetText(grdIssue, i, 5))
                sExec = sExec & " Where Inventorycode =" & fncFormatSQLText(GetText(grdIssue, i, 2))
                dm.ProjExecNonQry(sExec)

                'sExec = "UpDate tblInventoryGoldBar Set Status='SA' "
                'sExec = sExec & " , GoldBarIssueNo = " & fncFormatSQLText(sGoldBarIssueNo)
                'sExec = sExec & " Where Inventorycode =" & fncFormatSQLText(GetText(grdIssue, i, 2))
                'sExec = sExec & " and BarNo in(" & Trim(GetText(grdIssue, i, 8)) & ")"
                'dm.ProjExecNonQry(sExec)
            Next

            ''sExec = "UpDate tblGoldBarPurchase Set Issuedweight = IssuedWeight + "
            ''sExec = sExec & Val(txtIssued2) & ", BalanceWeight = BalanceWeight - "
            ''sExec = sExec & Val(txtNetIssue11)
            ''sExec = sExec & " Where InvoiceNo =" & fncFormatSQLText(GetText(grdIssue, grdIssue.Row, 1))
            ''If dm.ProjExecNonQry(sExec) Then
            ''    MessageBox.Show("Issued Sucessfully")
            ''End If
            'subGetGoldBarDetail()
            ''' subGetpurchaseBill()
            txtPayable.Text = Format((GetText(grdUnpaidBill, grdUnpaidBill.Row, 5)), "0.00")
            txtInvoiceNo1.Text = ""
            txtIssued2.Text = ""
            txtNetIssue1.Text = ""
            '''End If
            MessageBox.Show("Issued Sucessfully")
            subPrint(Trim(sGoldBarIssueNo))
            subClear()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        '''Dim Ds As DataSet
        '''Dim TotPurchaseweight As Double
        '''Dim TempPurchaseWeight As Double
        '''Dim i As Long
        '''Dim sExec As String
        '''Try

        '''    If Not fncValidation() Then Exit Sub
        '''    bProceed = False
        '''    TotPurchaseweight = Val(txtIssued2.Text)
        '''    Ds = objPurchase.GetPurchaseBill(txtSupplierCode1.Text.Trim)
        '''    If Ds.Tables("PurchaseBill").Rows.Count > 0 Then
        '''        For i = 0 To Ds.Tables("PurchaseBill").Rows.Count - 1
        '''            If TotPurchaseweight >= Ds.Tables("PurchaseBill").Rows(i)("BalanceWeight") Then
        '''                TotPurchaseweight = TotPurchaseweight - Ds.Tables("PurchaseBill").Rows(i)("BalanceWeight")
        '''                TempPurchaseWeight = Ds.Tables("PurchaseBill").Rows(i)("BalanceWeight")
        '''            Else
        '''                TempPurchaseWeight = TotPurchaseweight
        '''                TotPurchaseweight = 0
        '''                bProceed = True
        '''            End If
        '''            If objPurchase.GoldBarIssueProceed(Ds.Tables("PurchaseBill").Rows(i)("InvoiceNo"), Ds.Tables("PurchaseBill").Rows(i)("InvoiceDate"), txtSupplierCode1.Text.Trim, Ds.Tables("PurchaseBill").Rows(i)("PurchaseWeight"), Ds.Tables("PurchaseBill").Rows(i)("PayableAmount"), TempPurchaseWeight * 0.92, TempPurchaseWeight, txtInvoiceNo1.Text.Trim, sInvoicedate, "24K-999", dPaidCost, "G", sUserID) Then
        '''            End If
        '''            If bProceed Then
        '''                Exit For
        '''            End If
        '''        Next
        '''        sExec = "UpDate tblGoldBarPurchase Set Issuedweight = IssuedWeight + "
        '''        sExec = sExec & Val(txtIssued2) & ", BalanceWeight = BalanceWeight - "
        '''        sExec = sExec & Val(txtNetIssue11)
        '''        sExec = sExec & " Where InvoiceNo =" & fncFormatSQLText(txtInvoiceNo1.Text.Trim)
        '''        If dm.ProjExecNonQry(sExec) Then
        '''            MessageBox.Show("Issued Sucessfully")
        '''        End If
        '''        'subGetGoldBarDetail()
        '''        subGetpurchaseBill()
        '''        txtPayable.Text = Format((GetText(grdUnpaidBill, grdUnpaidBill.Row, 5)), "0.00")
        '''        txtInvoiceNo1.Text = ""
        '''        txtIssued2.Text = ""
        '''        txtNetIssue11.Text = ""
        '''    End If

        '''Catch ex As Exception
        '''    MessageBox.Show(ex.Message)
        '''End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        clearall()
    End Sub
    Function fncValidation() As Boolean
        Try

            If fnDateValid(Val(txtDD.Text), Val(txtMM.Text), Val(txtYYYY.Text)) = False Then
                MsgBox("Pls Check the Date ")
                txtDD.Focus()
                Exit Function
            End If

            If Len(Trim$(txtSupplierCode1.Text)) = 0 Then
                MsgBox("Supplier cannot be blank!")
                txtSupplierCode1.Focus()
                Exit Function
            Else
                Dim objSupplier As New clsSuppliers
                Dim Ds As DataSet
                Ds = objSupplier.GetSupplierDetail(txtSupplierCode1.Text.Trim)
                If Ds.Tables(0).Rows.Count = 0 Then
                    MsgBox("Please Enter the Valid Supplier!")
                    Exit Function
                    Ds = Nothing
                End If
            End If

            If Len(Trim$(txtCurrencyCode.Text)) = 0 Then
                MsgBox("Currency cannot be blank!")
                txtCurrencyCode.Focus()
                Exit Function
            Else
                Dim objCurrency As New clsCurrency
                Dim Ds As DataSet
                Ds = objCurrency.GetCurrency(txtCurrencyCode.Text.Trim)
                If Ds.Tables("Currency").Rows.Count = 0 Then
                    MsgBox("Please Enter the Valid Currecncy!")
                    Exit Function
                    Ds = Nothing
                End If
            End If

            If Val(txtRate.Text) = 0 Then
                MsgBox("Rate must be greater than zero!")
                txtRate.Focus()
                Exit Function
            End If


            If Trim(txtInvoiceNo1.Text) = "" Then
                MsgBox("Invoice No. Cannot be Blank ....")
                'grdGoldIssue.Focus()
                Exit Function
            End If

            If Val(txtNetIssue1.Text) > dABal Then
                MsgBox("Gold Issued Cannot More Than Available Weight!!! ")
                txtIssued2.Focus()
                Exit Function
            End If

            If Val(txtPayable.Text) < Val(txtIssued2.Text) Then
                MsgBox("Gold Issued Cannot More Than Payable Weight!!! ")
                txtIssued2.Focus()
                Exit Function
            End If

            If Val(txtNetIssue1.Text) = 0 Then
                MsgBox("Gold Issued Actual Weight Should Not Be 0 !!! ")
                txtIssued2.Focus()
                Exit Function
            End If

            If Val(txtIssued2.Text) = 0 Then
                MsgBox("Gold Issued Converted  Weight Should Not Be 0 !!! ")
                txtIssued2.Focus()
                Exit Function
            End If
            fncValidation = True
            Exit Function

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function


    Private Sub txtInvoiceNo1_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Ds As DataSet
        Dim i As Long
        Try
            If txtInvoiceNo1.Text.Trim <> "" Then
                Ds = objPurchase.GetGoldBarInvoice(txtInvoiceNo1.Text.Trim)
                If Ds.Tables("GoldBarPurchase").Rows.Count > 0 Then
                    sInvoicedate = Ds.Tables("GoldBarPurchase").Rows(0)("InvoiceDate")
                    dABal = Ds.Tables("GoldBarPurchase").Rows(0)("Balanceweight")
                End If
                'For i = 1 To grdGoldIssue.MaxRows
                '    If txtInvoiceNo1.Text.Trim = GetText(grdGoldIssue, i, 1) Then
                '        grdGoldIssue.Row = i
                '        grdGoldIssue.Col = 1
                '        grdGoldIssue.Action = FPSpread.ActionConstants.ActionActiveCell
                '        Exit For
                '    End If
                'Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub txtIssued2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        '''  txtNetIssue11.Text = CDbl(txtIssued2.Text) * 0.92
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub subGetInventory(ByVal sInventoryCode As String)
        ', ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim sPMC As Long
        Dim ds As DataSet
        Dim ds1 As DataSet
        Dim objJobSheet As New clsGoldBarPurchase
        ds = objJobSheet.GetInventoryDetail(sInventoryCode.Trim)
        If ds.Tables(0).Rows.Count > 0 Then
            If Val(Trim(ds.Tables(0).Rows(0)("QtyOnHand") & "")) > 0 Then
                txtInventoryCode1.Text = Trim(ds.Tables(0).Rows(0)("Inventorycode") & "")
                txtDescription.Text = Trim(ds.Tables(0).Rows(0)("Description") & "")
                '''txtSupplierCode1.Text = Trim(ds.Tables(0).Rows(0)("SupplierCode") & "")
                TxtTotalWeight.Text = Val(Trim(ds.Tables(0).Rows(0)("Weight") & ""))
                txtCostperGram.Text = Val(Trim(ds.Tables(0).Rows(0)("CostperGram") & ""))
                txtPcs.Text = Val(Trim(ds.Tables(0).Rows(0)("Pcs") & ""))
                txtTotalCost.Text = Val(Trim(ds.Tables(0).Rows(0)("TotalCost") & ""))
            Else
                MsgBox("No Stock for this Inventory!", MsgBoxStyle.Information)
                txtInventoryCode1.Text = ""
                ' subClearItems()
                txtInventoryCode1.Focus()
            End If
        Else
            MsgBox("This Inventory Not Found... ", MsgBoxStyle.Exclamation)
            txtInventoryCode1.Text = ""
            ' subClearItems()
            txtInventoryCode1.Focus()
        End If


    End Sub


    Private Sub InvCode_ButtonHit(ByVal sender As System.Object, ByVal e As AxEditLib._DfpText_ButtonHitEvent)
        Try
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblInventory", "L", 4, "InventoryCode", "Classcode='999.9'", "InventoryCode", "InventoryCode", 80, "Description", "Description", 100, "CategoryCode", "CategoryCode", 80, "CatalogueCode", "CatalogueCode", 80)
            frmHelpClass.ShowDialog()
            txtInventoryCode1.Text = frmHelpClass.Var1.Trim
            subGetInventory(txtInventoryCode1.Text.Trim)
        Catch ex As Exception
            MessageBox.Show(Err.Description)
        End Try
    End Sub

    Private Sub txtPcs_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPcs.LostFocus
        '''Dim ds As DataSet
        '''Dim objJobSheet As New clsGoldBarPurchase
        '''ds = objJobSheet.GetInventoryDetail(txtInventoryCode1.Text.Trim)
        '''If ds.Tables(0).Rows.Count > 0 Then
        '''    If Val(txtPcs.Text) > Val(Trim(ds.Tables(0).Rows(0)("Pcs") & "")) Then
        '''        MsgBox("Can You Check the Actual Qty")
        '''        Exit Sub
        '''    Else
        '''txtIssued2.Text = Val(TxtTotalWeight.Text * txtPcs.Text)
        txtTotalCost.Text = Val(TxtTotalWeight.Text.Trim * txtCostperGram.Text.Trim * txtPcs.Text.Trim)
        '''    End If
        '''End If
    End Sub
    Private Sub subGetTradeInInvoices()
        Try
            Dim icnt As Long
            Dim sDate As Date
            Dim dsPur As New DataSet
            'Dim SBll As Purchase
            'SBll = New Purchase
            pnlInvoice.Visible = True
            dsPur = objTIS.GetTradeInInvoiceWholeSale(txtSupplierCode1.Text.Trim, SInvoiceNo)
            If dsPur.Tables(0).Rows.Count > 0 Then
                'grdInvoice.MaxRows = 0
                ''txtPayable.Text = ""
                'For icnt = 0 To dsPur.Tables(0).Rows.Count - 1
                '    grdInvoice.MaxRows = grdInvoice.MaxRows + 1
                '    grdInvoice.Row = grdInvoice.MaxRows
                '    Call SetText(grdInvoice, grdInvoice.Row, 1, 0)
                '    Call SetText(grdInvoice, grdInvoice.Row, 2, Trim(dsPur.Tables(0).Rows(icnt)("InvoiceDate") & ""))
                '    Call SetText(grdInvoice, grdInvoice.Row, 3, Trim(dsPur.Tables(0).Rows(icnt)("InvoiceNo") & ""))
                '    Call SetText(grdInvoice, grdInvoice.Row, 4, Trim(dsPur.Tables(0).Rows(icnt)("SupplierCode") & ""))
                '    Call SetText(grdInvoice, grdInvoice.Row, 5, Val(dsPur.Tables(0).Rows(icnt)("PurchaseWeight") & ""))
                '    Call SetText(grdInvoice, grdInvoice.Row, 6, Val(dsPur.Tables(0).Rows(icnt)("PurchaseWeight") & ""))
                'Next icnt
                'grdInvoice.Row = 1
                'grdInvoice.Col = 1
                'grdInvoice.Action = 0
                grdInvoice.RowCount = 0
                If Not IsNothing(dsPur) Then
                    If dsPur.Tables(0).Rows.Count > 0 Then
                        For icnt = 0 To dsPur.Tables(0).Rows.Count - 1
                            grdInvoice.Rows.Add()
                            Call fnSetdgvText(grdInvoice, icnt, 0, False)
                            fnSetdgvText(grdInvoice, icnt, 1, Trim(dsPur.Tables(0).Rows(icnt)("InvoiceDate") & ""))
                            fnSetdgvText(grdInvoice, icnt, 2, Trim(dsPur.Tables(0).Rows(icnt)("InvoiceNo") & ""))
                            fnSetdgvText(grdInvoice, icnt, 3, Trim(dsPur.Tables(0).Rows(icnt)("SupplierCode") & ""))
                            fnSetdgvText(grdInvoice, icnt, 4, Trim(dsPur.Tables(0).Rows(icnt)("PurchaseWeight") & ""))
                            fnSetdgvText(grdInvoice, icnt, 5, Trim(dsPur.Tables(0).Rows(icnt)("PurchaseWeight") & ""))
                        Next
                    End If

                End If
            Else
                '''grdUnpaidBill.MaxRows = 0
                '''MessageBox.Show("No Balance for this Supplier")
                '''txtSupplierCode1.Text = ""
                '''txtSupplierName.Text = ""
                '''txtSupplierCode1.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub txtInvoiceNo1_ButtonHit(ByVal sender As Object, ByVal e As AxEditLib._DfpText_ButtonHitEvent)
        If txtSupplierCode1.Text.Trim = "" Then
            MsgBox("Select the Supplier Code!")
            txtSupplierCode1.Focus()
            Exit Sub
        End If
        subGetTradeInInvoices()
    End Sub

    Private Sub btnPanelClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPanelClose.Click
        pnlInvoice.Visible = False
    End Sub
    Private Sub btnPanelOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPanelOK.Click
        Dim i As Long
        '  SInvoiceNo = ""
        ' sInvNo = ""
        ' sInvCode = ""

        For i = 0 To grdInvoice.RowCount - 1
            If Trim(fnGetdgvText(grdInvoice, i, 0)) = True Then
                ''SInvoiceNo = SInvoiceNo & fncFormatSQLText(GetText(grdInvoice, i, 2)) & ","
                SInvoiceNo = SInvoiceNo & "," & fncFormatSQLText(fnGetdgvText(grdInvoice, i, 2))
                sInvNo = sInvNo & "," & fncFormatSQLText(fnGetdgvText(grdInvoice, i, 2))
                sInventoryCode = sInventoryCode & "," & fncFormatSQLText(fnGetdgvText(grdInvoice, i, 2))
                sInvCode = sInvCode & "," & fncFormatSQLText(fnGetdgvText(grdInvoice, i, 2))
                dWeight = dWeight + Val(fnGetdgvText(grdInvoice, i, 4))
            End If
        Next
        'SInvoiceNo = Strings.Left(SInvoiceNo, Len(SInvoiceNo) - 1)
        If IbLoad Then
            SInvoiceNo = SInvoiceNo
            sInventoryCode = sInventoryCode
            sInvNo = sInvNo ''Strings.Right(sInvNo, Len(sInvNo) - 1)
            sInvCode = Strings.Right(sInvCode, Len(sInvCode) - 1)
        Else
            SInvoiceNo = Strings.Right(SInvoiceNo, Len(SInvoiceNo) - 1)
            sInventoryCode = Strings.Right(sInventoryCode, Len(sInventoryCode) - 1)
            sInvNo = Strings.Right(sInvNo, Len(sInvNo) - 1)
            sInvCode = Strings.Right(sInvCode, Len(sInvCode) - 1)
        End If
        subGetTradeBillNo()
        pnlInvoice.Visible = False
        ''' txtNetIssue11.Text = dWeight
    End Sub

    Private Sub chkSelectAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSelectAll.CheckedChanged
        Dim i As Long
        For i = 1 To grdInvoice.RowCount - 1
            'If Trim(GetText(grdInvoice, i, 1)) = 1 Then
            '    Call SetText(grdInvoice, i, 1, 0)
            'Else
            If chkSelectAll.Checked = True Then
                'Call SetText(grdInvoice, i, 1, 1)
                Call fnSetdgvText(grdInvoice, i, 0, True)
            Else
                'Call SetText(grdInvoice, i, 1, 0)
                Call fnSetdgvText(grdInvoice, i, 0, False)
            End If
            'End If
        Next
    End Sub
    Private Sub grdInvoice_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_ClickEvent)
        Dim i As Long
        ' If grdInvoice.Col = 1 Then
        'If Trim(GetText(grdInvoice, grdInvoice.ActiveRow, 1)) = 0 Or Trim(GetText(grdInvoice, grdInvoice.ActiveRow, 1)) = "" Then
        '    'If grdInvoice.ActiveRow = grdInvoice.MaxRows Then
        '    '    Call SetText(grdInvoice, grdInvoice.ActiveRow, 1, 0)
        '    'Else
        '    '    Call SetText(grdInvoice, grdInvoice.ActiveRow, 1, 1)
        '    'End If
        '    Call SetText(grdInvoice, grdInvoice.ActiveRow, 1, 1)

        'Else
        '    Call SetText(grdInvoice, grdInvoice.ActiveRow, 1, 0)

        'End If

        '  End If 

        If (fnGetdgvText(grdInvoice, grdInvoice.CurrentRow.Index, 0)) = False Then
            Call fnSetdgvText(grdInvoice, grdInvoice.CurrentRow.Index, 0, True)
        Else
            Call fnSetdgvText(grdInvoice, grdInvoice.CurrentRow.Index, 0, False)
        End If
    End Sub

    Private Sub GroupBox4_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox4.Enter

    End Sub


    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            subAddGrid(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Private Sub subAddGrid(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As Long
        Dim lQty As Long
        Dim lRow As Long
        Dim iRow As Long
        Try
            'If Not bEdited Then
            '    If grdIssue.MaxRows = 0 Then
            '        grdIssue.MaxRows = grdIssue.MaxRows + 1
            '    Else
            '        grdIssue.Row = grdIssue.MaxRows
            '        grdIssue.Action = 5
            '        grdIssue.MaxRows = grdIssue.MaxRows - 1
            '        grdIssue.MaxRows = grdIssue.MaxRows + 1
            '        'grdIssue.RowHeight(grdIssue.MaxRows) = GRDROWHT
            '    End If

            '    grdIssue.Row = grdIssue.MaxRows
            'Else
            '    grdIssue.Row = grdIssue.ActiveRow
            '    'grdIssue.RowHeight(grdIssue.ActiveRow) = GRDROWHT
            'End If
            lQty = 0
            lRow = 0
            iRow = grdIssue1.Rows.Add
            Call fnSetdgvText(grdIssue1, iRow, 0, txtInventoryCode1.Text.Trim)
            Call fnSetdgvText(grdIssue1, iRow, 1, txtDescription.Text.Trim)
            Call fnSetdgvText(grdIssue1, iRow, 2, Format(Val(TxtTotalWeight.Text.Trim), "#0.00"))
            Call fnSetdgvText(grdIssue1, iRow, 3, Format(Val(txtPcs.Text.Trim), "#0"))
            Call fnSetdgvText(grdIssue1, iRow, 4, Format(Val(txtCostperGram.Text.Trim), "#0.00"))
            Call fnSetdgvText(grdIssue1, iRow, 5, Format(Val(txtTotalCost.Text.Trim), "#0.00"))
            Call fnSetdgvText(grdIssue1, iRow, 6, Trim(sBarNo))
            d24KIssueWeight = d24KIssueWeight + Format(Val(TxtTotalWeight.Text.Trim) * Val(txtPcs.Text.Trim), "#0.00")

            'grdIssue.Row = grdIssue.MaxRows
            'grdIssue.Col = 1
            'grdIssue.Action = 0
            ' 'subTotal(sender, e)
            txtInventoryCode1.Tag = txtInventoryCode1
            txtNetIssue1.Text = d24KIssueWeight

            subClearDetail(True)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Private Sub subTotal(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Not bEdited Then
                grdIssue.MaxRows = grdIssue.MaxRows + 1
            End If

            Call SetText(grdIssue, grdIssue.MaxRows, 1, "Total:")

            grdIssue.ForeColor = Color.Red
            grdIssue.FontBold = True

            grdIssue.Col = 3
            grdIssue.Formula = "sum(c1:c" & grdIssue.MaxRows - 1 & ")"


            grdIssue.ForeColor = Color.Red
            grdIssue.FontBold = True

            grdIssue.Col = 4
            grdIssue.Formula = "sum(d1:d" & grdIssue.MaxRows - 1 & ")"

            grdIssue.ForeColor = Color.Red
            grdIssue.FontBold = True

            grdIssue.Col = 6
            grdIssue.Formula = "sum(f1:f" & grdIssue.MaxRows - 1 & ")"



        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Private Sub subClearDetail(ByVal bFocus As Boolean)
        Try
            txtInventoryCode1.Tag = ""
            bEdited = False
            txtInventoryCode1.Text = ""
            txtDescription.Text = ""
            TxtTotalWeight.Text = ""
            txtCostperGram.Text = ""
            txtPcs.Text = ""
            txtTotalCost.Text = ""

            'chkIPM.Checked = False
            If (bFocus And txtInventoryCode1.Enabled) Then txtInventoryCode1.Focus()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Private Sub subClear()
        Try
            grdGoldIssue.RowCount = 0
            grdIssue.MaxRows = 0
            txtSupplierCode1.Text = ""
            txtsupplierName.Text = ""
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Private Sub subGetTradeBillNo()
        Try
            Dim dsTIB As New DataSet
            Dim iCnt As Long

            dsTIB = objTIS.GetTradeInBalanceSales(Trim(sInvNo), Trim(sInvCode), Trim(txtSupplierCode1.Text))
            'If grdGoldIssue.MaxRows = 0 Then
            '    grdGoldIssue.MaxRows = 1
            'Else
            '    grdGoldIssue.MaxRows = grdGoldIssue.MaxRows + 1
            'End If

            'For iCnt = 1 To grdGoldIssue.MaxRows
            '    If Trim(GetText(grdGoldIssue, iCnt, 1)) = Trim(txtTradeBillNo.Text) Then
            '        MessageBox.Show("Item Already Scanned")
            '        grdGoldIssue.MaxRows = grdGoldIssue.MaxRows - 1
            '        txtTradeBillNo.Text = ""
            '        txtTradeBillNo.Focus()
            '        Exit Sub
            '    End If
            'Next
            ' dABal = 0
            'dAIssueBal = 0
            grdGoldIssue.RowCount = 0
            If dsTIB.Tables(0).Rows.Count > 0 Then
                'txtInvoiceNo1 = Trim(adoRs!InvoiceNo & "")
                sInvoicedate = Trim(dsTIB.Tables(0).Rows(0)("ModifyDate") & "")
                sTDate = Trim(dsTIB.Tables(0).Rows(0)("ModifyDate") & "")
                sTDate = DateValue(sTDate)
                For iCnt = 0 To dsTIB.Tables(0).Rows.Count - 1
                    'grdGoldIssue.RowCount = grdGoldIssue.RowCount + 1
                    'grdGoldIssue.Row = grdGoldIssue.RowCount
                    grdGoldIssue.Rows.Add()
                    Call fnSetdgvText(grdGoldIssue, iCnt, 0, Trim(dsTIB.Tables(0).Rows(iCnt)("JObsheetNo") & ""))
                    Call fnSetdgvText(grdGoldIssue, iCnt, 2, Trim(dsTIB.Tables(0).Rows(iCnt)("PurchaseBillNo") & ""))
                    Call fnSetdgvText(grdGoldIssue, iCnt, 1, sTDate)
                    Call fnSetdgvText(grdGoldIssue, iCnt, 3, "TRADE-IN")
                    Call fnSetdgvText(grdGoldIssue, iCnt, 4, Val(dsTIB.Tables(0).Rows(iCnt)("TotalWeight24K") & ""))
                    ''' Call SetText(grdGoldIssue, grdGoldIssue.Row, 5, Val(dsTIB.Tables(0).Rows(iCnt)("IssuedWeight") & ""))
                    Call fnSetdgvText(grdGoldIssue, iCnt, 5, Val(dsTIB.Tables(0).Rows(iCnt)("TotalWeight24K") * Val(1) & ""))
                    Call fnSetdgvText(grdGoldIssue, iCnt, 6, Val(dsTIB.Tables(0).Rows(iCnt)("TotalWeight24K") & ""))
                    dABal = dABal + Val(dsTIB.Tables(0).Rows(iCnt)("TotalWeight24K") & "")
                    dAIssueBal = dAIssueBal + Val(dsTIB.Tables(0).Rows(iCnt)("TotalWeight24K") * Val(1) & "")
                    '''If Val(dsTIB.Tables(0).Rows(iCnt)("TotalWeight24K")) > 0 Then
                    '''    dPaidCost = dPaidCost + Val(dsTIB.Tables(0).Rows(iCnt)("TotalCost") & "") / Val(dsTIB.Tables(0).Rows(iCnt)("TotalWeight24K") & "")
                    '''Else
                    dPaidCost = 0
                    '''End If
                Next iCnt

                'grdGoldIssue.Row = 1
                'grdGoldIssue.Col = 1
                'grdGoldIssue.Action = 0
            Else
                'grdGoldIssue.MaxRows = 0
                Exit Sub
            End If

            '''txtIssued2.Text = Val(dABal) + Val(txtIssued2.Text)
            txtIssued2.Text = Val(dABal)
            txtIssued2.Text = CDbl(txtIssued2.Text)
            '''txtTotalWt.Text = Val(txtIssued2.Text) * CDbl(txtPerc.Text)
            ''txtPayable.Text = Format(Val(dAIssueBal), "0.00")
            ''txtPayable.Text = Format(CDbl(txtPayable.Text), "0.00")
            '' txtTotalPrice.Text = Format((CDbl(txtTotalWt.Text) * CDbl(txtPricePerGram.Text)) + ((CDbl(txtTotalWt.Text) * CDbl(txtPricePerGram.Text)) * (Objuser.GstPercentage / 100)), "####00.00")
            ''  txtNoOfBills.Text = grdGoldIssue.MaxRows
            'txtNetIssue1.Text = Format(Str((CDbl(txtIssued2.Text) * 0.895) / 0.92), "#######.00")
            'lblTrade.Text = "TRADE"
            ''' sClass = "TRADE"
            IbLoad = True
            ''' txtTradeBillNo.Text = ""
            '''txtTradeBillNo.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub btnIssueOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIssueOk.Click
        Dim i As Long
        dIssueWeight = 0
        For i = 0 To grdGoldIssue.RowCount - 1
            dIssueWeight = dIssueWeight + Val(fnGetdgvText(grdGoldIssue, i, 5))
        Next
        txtIssued2.Text = Val(dIssueWeight)
        txtIssued2.Text = CDbl(txtIssued2.Text)
    End Sub
    Private Function fncGoldBarIssueNo() As Boolean
        Dim Ds As DataSet
        Dim objGoldBarIssue As New ClsGoldBarIssue
        Try
            Ds = objGoldBarIssue.GetUpdateGoldBarIssue
            If Ds.Tables("GoldBarIssue").Rows.Count <= 0 Then
                fncGoldBarIssueNo = False
                MsgBox("Error in generating JobSheet No!", MsgBoxStyle.Critical)
                Exit Function
            Else
                sGoldBarIssueNo = Trim(Ds.Tables("GoldBarIssue").Rows(0)("NextGoldBarIssueNo") & "")
                fncGoldBarIssueNo = True
            End If
            Ds = Nothing
            objGoldBarIssue = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        Finally

        End Try
    End Function


    Private Sub txtInventoryCode1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If txtInventoryCode1.Text.Trim <> "" Then
                subGetInventory(txtInventoryCode1.Text.Trim)
            End If
        Catch ex As Exception
            MessageBox.Show(Err.Description)
        End Try
    End Sub

    Private Sub txtInventoryCode1_KeyDownEvent(ByVal sender As Object, ByVal e As AxEditLib._DfpText_KeyDownEvent)
        Try
            Select Case e.keyCode
                Case Keys.F1

                    Dim frmHelpClass As New frmHelpSearch(0, 0, "tblInventory", "L", 3, "InventoryCode", "Classcode='GBAR'", "InventoryCode", "InventoryCode", 80, "Description", "Description", 150, "PurchaseBillNo", "PurchaseBillNo", 50)
                    frmHelpClass.ShowDialog()
                    txtInventoryCode1.Text = frmHelpClass.Var1.Trim
                    subGetInventory(txtInventoryCode1.Text.Trim)

            End Select
        Catch ex As Exception
            MessageBox.Show(Err.Description)
        End Try
    End Sub




    Private Sub btnPCS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPCS.Click
        Try
            subGetGoldBarNo()
        Catch ex As Exception
            MessageBox.Show(Err.Description)
        End Try
    End Sub
    Private Sub subGetGoldBarNo()
        Try
            Dim icnt As Long
            Dim sDate As Date
            Dim dsPur As New DataSet

            pnlPCS.Visible = True
            dsPur = objTIS.GetGoldBarNo(txtInventoryCode1.Text.Trim)
            If dsPur.Tables(0).Rows.Count > 0 Then
                grdPCS1.RowCount = 0

                For icnt = 0 To dsPur.Tables(0).Rows.Count - 1
                    'grdPCS.MaxRows = grdPCS.MaxRows + 1
                    'grdPCS.Row = grdPCS.MaxRows
                    grdPCS1.Rows.Add()
                    Call fnSetdgvText(grdPCS1, icnt, 0, 0)
                    Call fnSetdgvText(grdPCS1, icnt, 1, Trim(dsPur.Tables(0).Rows(icnt)("BarNo") & ""))

                Next icnt
                'grdPCS.Row = 1
                'grdPCS.Col = 1
                'grdPCS.Action = 0
            Else
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub grdPCS_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_ClickEvent) Handles grdPCS.ClickEvent
        Dim i As Long
        If Trim(GetText(grdPCS, grdPCS.ActiveRow, 1)) = 0 Or Trim(GetText(grdPCS, grdPCS.ActiveRow, 1)) = "" Then
            Call SetText(grdPCS, grdPCS.ActiveRow, 1, 1)
        Else
            Call SetText(grdPCS, grdPCS.ActiveRow, 1, 0)
        End If
    End Sub

    Private Sub btnPanelPCS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPanelPCS.Click
        Dim i As Long
        Dim j As Long
        sBarNo = ""
        j = 0
        For i = 1 To grdPCS.MaxRows
            If Trim(GetText(grdPCS, i, 1)) = 1 Then
                sBarNo = sBarNo & "," & fncFormatSQLText(GetText(grdPCS, i, 2))
                j = j + 1
            End If
        Next
        sBarNo = Strings.Right(sBarNo, Len(sBarNo) - 1)
        If Val(j) <> Val(txtPcs.Text) Then
            MsgBox("PCS and Selected BarNo Not Tally")
            Exit Sub
        End If
        pnlPCS.Visible = False
    End Sub


    Private Sub btnPanePCSClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPanePCSClose.Click
        pnlPCS.Visible = False
    End Sub

    Private Sub chkPcsSelectAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPcsSelectAll.CheckedChanged
        Dim i As Long
        For i = 1 To grdPCS.MaxRows

            If chkPcsSelectAll.Checked = True Then
                Call SetText(grdPCS, i, 1, 1)
            Else
                Call SetText(grdPCS, i, 1, 0)
            End If
        Next
    End Sub
    Private Sub subPrint(ByVal sTradeInIssueNo As String)
        Dim frmrep As New frmReports
        Dim CR As New ReportDocument
        Dim DS As New DataSet
        Dim objClsCompany As New clsCompany
        Dim sCompanyname, sAddress, sPhone, sFax, sReportTitle As String
        Try
            Dim ReportTitle As String
            Dim sSelection As String
            Dim sSalesType$
            Cursor = Cursors.WaitCursor
            DS = objClsCompany.getCompanyName
            If DS.Tables("GetCompanyName").Rows.Count > 0 Then
                sCompanyname = Trim(DS.Tables("GetCompanyName").Rows(0)("Name") & "")
                sAddress = Trim(DS.Tables("GetCompanyName").Rows(0)("Address1") & "") & " " & _
                             Trim(DS.Tables("GetCompanyName").Rows(0)("Address2") & "") & " " & _
                             Trim(DS.Tables("GetCompanyName").Rows(0)("Country") & "")
                sPhone = Trim(DS.Tables("GetCompanyName").Rows(0)("Phone") & "")
                sFax = Trim(DS.Tables("GetCompanyName").Rows(0)("Fax") & "")
            End If
            sReportTitle = "Trade In Issue Report For " & sTradeInIssueNo
            If UCase(sCompanyShortCode) = "HN" Then
                strpath = Application.StartupPath & "\Reports\rptGoldBarIssue_HN.rpt"
            Else
                strpath = Application.StartupPath & "\Reports\rptTradeInGoldIssue.rpt"
            End If
            CR.Load(strpath)

            '''CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(sCompanyname)
            '''CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(sAddress)
            '''CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText(sReportTitle)
            '''CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(sPhone)
            '''CR.DataDefinition.FormulaFields("CompAddress4").Text = fncFormatSQLText(sFax)
            ReportLogOnToServer(CR)
            CR.Refresh()
            frmrep.ReportViewer.ReportSource = CR
            sSelection = "{tblGoldBarIssued.IssueNo} = " & fncFormatSQLText(Trim(sTradeInIssueNo))
            frmrep.ReportViewer.SelectionFormula = sSelection
            frmrep.Show()
            frmrep.ReportViewer.Visible = True
            frmrep.ReportViewer.DisplayToolbar = True
            frmrep.ReportViewer.CloseView(False)
            frmrep.WindowState = FormWindowState.Maximized
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor = Cursors.Default
            frmrep = Nothing
            CR = Nothing
            DS = Nothing
            objClsCompany = Nothing
        End Try
    End Sub

    Private Sub txtIssueNo1_ButtonHit(ByVal sender As Object, ByVal e As AxEditLib._DfpText_ButtonHitEvent)
        Dim frmHelpClass As New frmHelpSearch(0, 0, "tblGoldBarIssued", "L", 3, "IssueNo", "", "IssueNo", "IssueNo", 80, "PDate", "IssueDate", 180, "SupplierCode", "SupplierCode", 60)
        frmHelpClass.ShowDialog()
        txtIssueNo1.Text = frmHelpClass.Var1.Trim
    End Sub

    Private Sub btnRePrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRePrint.Click
        If Trim(txtIssueNo1.Text) <> "" Then
            subPrint(txtIssueNo1.Text.Trim)
        End If
    End Sub

    Private Sub txtInvoiceNo1_Advance(sender As System.Object, e As AxEditLib._DfpText_AdvanceEvent)

    End Sub

    Private Sub btnSupplierCode_Click(sender As System.Object, e As System.EventArgs) Handles btnSupplierCode.Click
        Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSupplier", "L", 4, "SupplierName", "", "SupplierCode", "SupplierCode", 80, "SupplierName", "SupplierName", 180, "Address1", "Address1", 60, "Address2", "Address2", 80)
        frmHelpClass.ShowDialog()
        txtSupplierCode1.Text = frmHelpClass.Var1.Trim
        txtsupplierName.Text = frmHelpClass.Var2.Trim
    End Sub

    Private Sub txtSupplierCode1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtSupplierCode1.KeyDown
        If e.KeyCode = Keys.F1 Then
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSupplier", "L", 4, "SupplierName", "", "SupplierCode", "SupplierCode", 80, "SupplierName", "SupplierName", 180, "Address1", "Address1", 60, "Address2", "Address2", 80)
            frmHelpClass.ShowDialog()
            txtSupplierCode1.Text = frmHelpClass.Var1.Trim
            txtsupplierName.Text = frmHelpClass.Var2.Trim
        End If
    End Sub

    Private Sub btnInvoiceNo_Click(sender As System.Object, e As System.EventArgs) Handles btnInvoiceNo.Click
        If txtSupplierCode1.Text.Trim = "" Then
            MsgBox("Select the Supplier Code!")
            txtSupplierCode1.Focus()
            Exit Sub
        End If
        subGetTradeInInvoices()
    End Sub

    Private Sub txtInvoiceNo1_LostFocus1(sender As Object, e As System.EventArgs) Handles txtInvoiceNo1.LostFocus
        Dim Ds As DataSet
        Dim i As Long
        Try
            If txtInvoiceNo1.Text.Trim <> "" Then
                Ds = objPurchase.GetGoldBarInvoice(txtInvoiceNo1.Text.Trim)
                If Ds.Tables("GoldBarPurchase").Rows.Count > 0 Then
                    sInvoicedate = Ds.Tables("GoldBarPurchase").Rows(0)("InvoiceDate")
                    dABal = Ds.Tables("GoldBarPurchase").Rows(0)("Balanceweight")
                End If
                'For i = 1 To grdGoldIssue.MaxRows
                '    If txtInvoiceNo1.Text.Trim = GetText(grdGoldIssue, i, 1) Then
                '        grdGoldIssue.Row = i
                '        grdGoldIssue.Col = 1
                '        grdGoldIssue.Action = FPSpread.ActionConstants.ActionActiveCell
                '        Exit For
                '    End If
                'Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtInventoryCode1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtInventoryCode1.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.F1

                    Dim frmHelpClass As New frmHelpSearch(0, 0, "tblInventory", "L", 4, "InventoryCode", "ClassCode = '999.9'", "InventoryCode", "InventoryCode", 80, "Description", "Description", 150, "PurchaseBillNo", "PurchaseBillNo", 50, "ClassCode", "ClassCode", 50)
                    frmHelpClass.ShowDialog()
                    txtInventoryCode1.Text = frmHelpClass.Var1.Trim
                    subGetInventory(txtInventoryCode1.Text.Trim)

            End Select
        Catch ex As Exception
            MessageBox.Show(Err.Description)
        End Try
    End Sub

    Private Sub btnInventoryCode_Click(sender As System.Object, e As System.EventArgs) Handles btnInventoryCode.Click

        Dim frmHelpClass As New frmHelpSearch(0, 0, "tblInventory", "L", 4, "InventoryCode", "ClassCode = '999.9'", "InventoryCode", "InventoryCode", 80, "Description", "Description", 150, "PurchaseBillNo", "PurchaseBillNo", 50, "ClassCode", "ClassCode", 50)
        frmHelpClass.ShowDialog()
        txtInventoryCode1.Text = frmHelpClass.Var1.Trim
        subGetInventory(txtInventoryCode1.Text.Trim)
    End Sub

    Private Sub btnDelete_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete.Click

    End Sub
End Class
