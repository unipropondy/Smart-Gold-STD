Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmTradeInWholeSales
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
    Friend WithEvents btnProceed As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnBillClear As System.Windows.Forms.Button
    Friend WithEvents btnTradeBillNoSearch As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtTradeBillNo As System.Windows.Forms.TextBox
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtTotalPrice As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtPricePerGram As System.Windows.Forms.TextBox
    Friend WithEvents txtNoOfBills As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtIssued As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtTradeIssueNo As System.Windows.Forms.TextBox
    Friend WithEvents txtCurrencyRate As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtYY As System.Windows.Forms.TextBox
    Friend WithEvents txtMM As System.Windows.Forms.TextBox
    Friend WithEvents txtDD As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtCurrencyName As System.Windows.Forms.TextBox
    Friend WithEvents btnCurrencySearch As System.Windows.Forms.Button
    Friend WithEvents txtCurrencyCode As System.Windows.Forms.TextBox
    Friend WithEvents btnIssueNoSearch As System.Windows.Forms.Button
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtSupplierName As System.Windows.Forms.TextBox
    Friend WithEvents btnSupplierCodeSearch As System.Windows.Forms.Button
    Friend WithEvents txtsupplierCode As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtGoldIssueNo As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPayable As System.Windows.Forms.TextBox
    Friend WithEvents txtNetIssue As System.Windows.Forms.TextBox
    Friend WithEvents pnlInvoice As System.Windows.Forms.Panel
    Friend WithEvents btnPanelOK As System.Windows.Forms.Button
    Friend WithEvents chkSelectAll As System.Windows.Forms.CheckBox
    Friend WithEvents txtTotalWt As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtPerc As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents grdInvoice As System.Windows.Forms.DataGridView
    Friend WithEvents grdGoldIssue As System.Windows.Forms.DataGridView
    Friend WithEvents cboTealPercent As System.Windows.Forms.ComboBox
    Friend WithEvents lblSelweight As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents cboPurity As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents lblSelCost As System.Windows.Forms.Label
    Friend WithEvents cSel As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents cInventoryCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cInvoice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IssuedWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cIssueWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cAvgCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents chkProduction As System.Windows.Forms.CheckBox
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cInvCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPweight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colIWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colClass As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkExport As System.Windows.Forms.CheckBox
    Friend WithEvents btnPanelClose As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTradeInWholeSales))
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btnProceed = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnBillClear = New System.Windows.Forms.Button()
        Me.btnTradeBillNoSearch = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtTradeBillNo = New System.Windows.Forms.TextBox()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkExport = New System.Windows.Forms.CheckBox()
        Me.chkProduction = New System.Windows.Forms.CheckBox()
        Me.cboPurity = New System.Windows.Forms.ComboBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.cboTealPercent = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtPerc = New System.Windows.Forms.TextBox()
        Me.txtTotalWt = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtTotalPrice = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtPricePerGram = New System.Windows.Forms.TextBox()
        Me.txtNoOfBills = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtIssued = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtTradeIssueNo = New System.Windows.Forms.TextBox()
        Me.txtCurrencyRate = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtYY = New System.Windows.Forms.TextBox()
        Me.txtMM = New System.Windows.Forms.TextBox()
        Me.txtDD = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnCurrencySearch = New System.Windows.Forms.Button()
        Me.txtCurrencyCode = New System.Windows.Forms.TextBox()
        Me.btnIssueNoSearch = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtSupplierName = New System.Windows.Forms.TextBox()
        Me.btnSupplierCodeSearch = New System.Windows.Forms.Button()
        Me.txtsupplierCode = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtGoldIssueNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCurrencyName = New System.Windows.Forms.TextBox()
        Me.txtPayable = New System.Windows.Forms.TextBox()
        Me.txtNetIssue = New System.Windows.Forms.TextBox()
        Me.pnlInvoice = New System.Windows.Forms.Panel()
        Me.chkSelectAll = New System.Windows.Forms.CheckBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblSelCost = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblSelweight = New System.Windows.Forms.Label()
        Me.grdInvoice = New System.Windows.Forms.DataGridView()
        Me.cSel = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.cInventoryCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cInvoice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IssuedWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cIssueWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cAvgCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnPanelClose = New System.Windows.Forms.Button()
        Me.btnPanelOK = New System.Windows.Forms.Button()
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.grdGoldIssue = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cInvCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPweight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colClass = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1.SuspendLayout()
        Me.pnlInvoice.SuspendLayout()
        CType(Me.grdInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdGoldIssue, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnProceed
        '
        Me.btnProceed.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnProceed.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnProceed.Location = New System.Drawing.Point(460, 542)
        Me.btnProceed.Name = "btnProceed"
        Me.btnProceed.Size = New System.Drawing.Size(92, 38)
        Me.btnProceed.TabIndex = 126
        Me.btnProceed.Text = "Proceed (F7)"
        Me.btnProceed.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(556, 542)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(92, 38)
        Me.btnClose.TabIndex = 129
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(52, 550)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 15)
        Me.Label4.TabIndex = 132
        Me.Label4.Text = "Unpaid Bill List"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label4.Visible = False
        '
        'btnBillClear
        '
        Me.btnBillClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnBillClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBillClear.Location = New System.Drawing.Point(364, 542)
        Me.btnBillClear.Name = "btnBillClear"
        Me.btnBillClear.Size = New System.Drawing.Size(92, 38)
        Me.btnBillClear.TabIndex = 127
        Me.btnBillClear.Text = "Clear (F6)"
        Me.btnBillClear.UseVisualStyleBackColor = False
        '
        'btnTradeBillNoSearch
        '
        Me.btnTradeBillNoSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnTradeBillNoSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTradeBillNoSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTradeBillNoSearch.Location = New System.Drawing.Point(380, 315)
        Me.btnTradeBillNoSearch.Name = "btnTradeBillNoSearch"
        Me.btnTradeBillNoSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnTradeBillNoSearch.TabIndex = 124
        Me.btnTradeBillNoSearch.Text = "..."
        Me.btnTradeBillNoSearch.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(45, 329)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 15)
        Me.Label3.TabIndex = 131
        Me.Label3.Text = "Trade In Gold"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTradeBillNo
        '
        Me.txtTradeBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTradeBillNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTradeBillNo.ForeColor = System.Drawing.Color.Black
        Me.txtTradeBillNo.Location = New System.Drawing.Point(284, 315)
        Me.txtTradeBillNo.MaxLength = 20
        Me.txtTradeBillNo.Name = "txtTradeBillNo"
        Me.txtTradeBillNo.Size = New System.Drawing.Size(96, 20)
        Me.txtTradeBillNo.TabIndex = 123
        Me.txtTradeBillNo.Tag = ""
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(268, 542)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(92, 38)
        Me.btnPrint.TabIndex = 128
        Me.btnPrint.Text = "Print (F9)"
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(180, 315)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(64, 15)
        Me.Label16.TabIndex = 133
        Me.Label16.Text = "Trade Bill No"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.chkExport)
        Me.Panel1.Controls.Add(Me.chkProduction)
        Me.Panel1.Controls.Add(Me.cboPurity)
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Controls.Add(Me.txtRemarks)
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Controls.Add(Me.cboTealPercent)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.txtPerc)
        Me.Panel1.Controls.Add(Me.txtTotalWt)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.txtTotalPrice)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.txtPricePerGram)
        Me.Panel1.Controls.Add(Me.txtNoOfBills)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtIssued)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.txtTradeIssueNo)
        Me.Panel1.Controls.Add(Me.txtCurrencyRate)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.txtYY)
        Me.Panel1.Controls.Add(Me.txtMM)
        Me.Panel1.Controls.Add(Me.txtDD)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.btnCurrencySearch)
        Me.Panel1.Controls.Add(Me.txtCurrencyCode)
        Me.Panel1.Controls.Add(Me.btnIssueNoSearch)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.txtSupplierName)
        Me.Panel1.Controls.Add(Me.btnSupplierCodeSearch)
        Me.Panel1.Controls.Add(Me.txtsupplierCode)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtGoldIssueNo)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Location = New System.Drawing.Point(40, 114)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(612, 198)
        Me.Panel1.TabIndex = 119
        '
        'chkExport
        '
        Me.chkExport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkExport.ForeColor = System.Drawing.Color.Black
        Me.chkExport.Location = New System.Drawing.Point(329, 65)
        Me.chkExport.Name = "chkExport"
        Me.chkExport.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkExport.Size = New System.Drawing.Size(91, 31)
        Me.chkExport.TabIndex = 3856
        Me.chkExport.Text = "Export"
        '
        'chkProduction
        '
        Me.chkProduction.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkProduction.ForeColor = System.Drawing.Color.Black
        Me.chkProduction.Location = New System.Drawing.Point(239, 65)
        Me.chkProduction.Name = "chkProduction"
        Me.chkProduction.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkProduction.Size = New System.Drawing.Size(128, 31)
        Me.chkProduction.TabIndex = 3855
        Me.chkProduction.Text = "Production"
        '
        'cboPurity
        '
        Me.cboPurity.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPurity.FormattingEnabled = True
        Me.cboPurity.Items.AddRange(New Object() {"750", "835", "875", "916", "999", "999.9", "22K"})
        Me.cboPurity.Location = New System.Drawing.Point(319, 137)
        Me.cboPurity.Name = "cboPurity"
        Me.cboPurity.Size = New System.Drawing.Size(64, 21)
        Me.cboPurity.TabIndex = 3854
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(233, 138)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(64, 16)
        Me.Label18.TabIndex = 3853
        Me.Label18.Text = "Purity"
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.Black
        Me.txtRemarks.Location = New System.Drawing.Point(107, 170)
        Me.txtRemarks.MaxLength = 100
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(477, 20)
        Me.txtRemarks.TabIndex = 3851
        Me.txtRemarks.Tag = ""
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(11, 170)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(96, 15)
        Me.Label17.TabIndex = 3852
        Me.Label17.Text = "Remarks"
        '
        'cboTealPercent
        '
        Me.cboTealPercent.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTealPercent.FormattingEnabled = True
        Me.cboTealPercent.Items.AddRange(New Object() {".750", ".850", ".895", ".897", ".900", ".999"})
        Me.cboTealPercent.Location = New System.Drawing.Point(296, 135)
        Me.cboTealPercent.Name = "cboTealPercent"
        Me.cboTealPercent.Size = New System.Drawing.Size(64, 21)
        Me.cboTealPercent.TabIndex = 3850
        Me.cboTealPercent.Visible = False
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(232, 136)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(64, 16)
        Me.Label14.TabIndex = 804
        Me.Label14.Text = "Cost Perc"
        Me.Label14.Visible = False
        '
        'txtPerc
        '
        Me.txtPerc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPerc.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPerc.ForeColor = System.Drawing.Color.Black
        Me.txtPerc.Location = New System.Drawing.Point(355, 136)
        Me.txtPerc.MaxLength = 8
        Me.txtPerc.Name = "txtPerc"
        Me.txtPerc.Size = New System.Drawing.Size(48, 21)
        Me.txtPerc.TabIndex = 803
        Me.txtPerc.Tag = ""
        Me.txtPerc.Visible = False
        '
        'txtTotalWt
        '
        Me.txtTotalWt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalWt.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalWt.ForeColor = System.Drawing.Color.BlueViolet
        Me.txtTotalWt.Location = New System.Drawing.Point(107, 136)
        Me.txtTotalWt.MaxLength = 20
        Me.txtTotalWt.Name = "txtTotalWt"
        Me.txtTotalWt.ReadOnly = True
        Me.txtTotalWt.Size = New System.Drawing.Size(120, 21)
        Me.txtTotalWt.TabIndex = 801
        Me.txtTotalWt.Tag = ""
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(8, 137)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(64, 19)
        Me.Label12.TabIndex = 802
        Me.Label12.Text = "Total Wt"
        '
        'txtTotalPrice
        '
        Me.txtTotalPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalPrice.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalPrice.ForeColor = System.Drawing.Color.Black
        Me.txtTotalPrice.Location = New System.Drawing.Point(319, 104)
        Me.txtTotalPrice.MaxLength = 8
        Me.txtTotalPrice.Name = "txtTotalPrice"
        Me.txtTotalPrice.Size = New System.Drawing.Size(96, 21)
        Me.txtTotalPrice.TabIndex = 15
        Me.txtTotalPrice.Tag = ""
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(233, 104)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(87, 21)
        Me.Label11.TabIndex = 136
        Me.Label11.Text = "Total  Price"
        '
        'txtPricePerGram
        '
        Me.txtPricePerGram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPricePerGram.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPricePerGram.ForeColor = System.Drawing.Color.Black
        Me.txtPricePerGram.Location = New System.Drawing.Point(501, 104)
        Me.txtPricePerGram.MaxLength = 8
        Me.txtPricePerGram.Name = "txtPricePerGram"
        Me.txtPricePerGram.Size = New System.Drawing.Size(72, 21)
        Me.txtPricePerGram.TabIndex = 14
        Me.txtPricePerGram.Tag = ""
        '
        'txtNoOfBills
        '
        Me.txtNoOfBills.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNoOfBills.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoOfBills.ForeColor = System.Drawing.Color.BlueViolet
        Me.txtNoOfBills.Location = New System.Drawing.Point(296, 104)
        Me.txtNoOfBills.MaxLength = 20
        Me.txtNoOfBills.Name = "txtNoOfBills"
        Me.txtNoOfBills.ReadOnly = True
        Me.txtNoOfBills.Size = New System.Drawing.Size(48, 21)
        Me.txtNoOfBills.TabIndex = 13
        Me.txtNoOfBills.Tag = ""
        Me.txtNoOfBills.Visible = False
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(429, 104)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(66, 24)
        Me.Label7.TabIndex = 133
        Me.Label7.Text = "Cost/Gram"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(232, 104)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 24)
        Me.Label1.TabIndex = 132
        Me.Label1.Text = "Total No.of Bills"
        Me.Label1.Visible = False
        '
        'txtIssued
        '
        Me.txtIssued.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIssued.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIssued.ForeColor = System.Drawing.Color.BlueViolet
        Me.txtIssued.Location = New System.Drawing.Point(107, 104)
        Me.txtIssued.MaxLength = 20
        Me.txtIssued.Name = "txtIssued"
        Me.txtIssued.ReadOnly = True
        Me.txtIssued.Size = New System.Drawing.Size(120, 21)
        Me.txtIssued.TabIndex = 12
        Me.txtIssued.Tag = ""
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(8, 96)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(64, 24)
        Me.Label10.TabIndex = 131
        Me.Label10.Text = "Total Issued Wt"
        '
        'txtTradeIssueNo
        '
        Me.txtTradeIssueNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTradeIssueNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTradeIssueNo.ForeColor = System.Drawing.Color.Black
        Me.txtTradeIssueNo.Location = New System.Drawing.Point(107, 16)
        Me.txtTradeIssueNo.MaxLength = 20
        Me.txtTradeIssueNo.Name = "txtTradeIssueNo"
        Me.txtTradeIssueNo.Size = New System.Drawing.Size(96, 20)
        Me.txtTradeIssueNo.TabIndex = 120
        Me.txtTradeIssueNo.Tag = ""
        '
        'txtCurrencyRate
        '
        Me.txtCurrencyRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrencyRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencyRate.ForeColor = System.Drawing.Color.Black
        Me.txtCurrencyRate.Location = New System.Drawing.Point(432, 70)
        Me.txtCurrencyRate.MaxLength = 20
        Me.txtCurrencyRate.Name = "txtCurrencyRate"
        Me.txtCurrencyRate.ReadOnly = True
        Me.txtCurrencyRate.Size = New System.Drawing.Size(72, 20)
        Me.txtCurrencyRate.TabIndex = 11
        Me.txtCurrencyRate.Tag = ""
        Me.txtCurrencyRate.Visible = False
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(384, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(8, 16)
        Me.Label6.TabIndex = 124
        Me.Label6.Text = "-"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(352, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(8, 16)
        Me.Label8.TabIndex = 123
        Me.Label8.Text = "-"
        '
        'txtYY
        '
        Me.txtYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtYY.Location = New System.Drawing.Point(392, 16)
        Me.txtYY.MaxLength = 4
        Me.txtYY.Name = "txtYY"
        Me.txtYY.ReadOnly = True
        Me.txtYY.Size = New System.Drawing.Size(32, 20)
        Me.txtYY.TabIndex = 4
        Me.txtYY.Tag = "ENN;"
        '
        'txtMM
        '
        Me.txtMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMM.Location = New System.Drawing.Point(360, 16)
        Me.txtMM.MaxLength = 2
        Me.txtMM.Name = "txtMM"
        Me.txtMM.ReadOnly = True
        Me.txtMM.Size = New System.Drawing.Size(21, 20)
        Me.txtMM.TabIndex = 3
        Me.txtMM.Tag = "ENN;"
        '
        'txtDD
        '
        Me.txtDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDD.Location = New System.Drawing.Point(328, 16)
        Me.txtDD.MaxLength = 2
        Me.txtDD.Name = "txtDD"
        Me.txtDD.ReadOnly = True
        Me.txtDD.Size = New System.Drawing.Size(21, 20)
        Me.txtDD.TabIndex = 2
        Me.txtDD.Tag = "ENN;"
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(248, 16)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(72, 20)
        Me.Label9.TabIndex = 122
        Me.Label9.Text = "Issue Date"
        '
        'btnCurrencySearch
        '
        Me.btnCurrencySearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCurrencySearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCurrencySearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCurrencySearch.Location = New System.Drawing.Point(200, 70)
        Me.btnCurrencySearch.Name = "btnCurrencySearch"
        Me.btnCurrencySearch.Size = New System.Drawing.Size(24, 20)
        Me.btnCurrencySearch.TabIndex = 700
        Me.btnCurrencySearch.Text = "..."
        Me.btnCurrencySearch.UseVisualStyleBackColor = False
        Me.btnCurrencySearch.Visible = False
        '
        'txtCurrencyCode
        '
        Me.txtCurrencyCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrencyCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencyCode.ForeColor = System.Drawing.Color.Black
        Me.txtCurrencyCode.Location = New System.Drawing.Point(107, 70)
        Me.txtCurrencyCode.MaxLength = 20
        Me.txtCurrencyCode.Name = "txtCurrencyCode"
        Me.txtCurrencyCode.Size = New System.Drawing.Size(96, 20)
        Me.txtCurrencyCode.TabIndex = 8
        Me.txtCurrencyCode.Tag = ""
        Me.txtCurrencyCode.Visible = False
        '
        'btnIssueNoSearch
        '
        Me.btnIssueNoSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnIssueNoSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnIssueNoSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnIssueNoSearch.Location = New System.Drawing.Point(200, 16)
        Me.btnIssueNoSearch.Name = "btnIssueNoSearch"
        Me.btnIssueNoSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnIssueNoSearch.TabIndex = 800
        Me.btnIssueNoSearch.Text = "..."
        Me.btnIssueNoSearch.UseVisualStyleBackColor = False
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(8, 72)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(82, 15)
        Me.Label13.TabIndex = 100
        Me.Label13.Text = "Currency"
        Me.Label13.Visible = False
        '
        'txtSupplierName
        '
        Me.txtSupplierName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierName.ForeColor = System.Drawing.Color.Black
        Me.txtSupplierName.Location = New System.Drawing.Point(232, 43)
        Me.txtSupplierName.MaxLength = 20
        Me.txtSupplierName.Name = "txtSupplierName"
        Me.txtSupplierName.ReadOnly = True
        Me.txtSupplierName.Size = New System.Drawing.Size(272, 20)
        Me.txtSupplierName.TabIndex = 7
        Me.txtSupplierName.Tag = ""
        '
        'btnSupplierCodeSearch
        '
        Me.btnSupplierCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSupplierCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSupplierCodeSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSupplierCodeSearch.Location = New System.Drawing.Point(200, 43)
        Me.btnSupplierCodeSearch.Name = "btnSupplierCodeSearch"
        Me.btnSupplierCodeSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnSupplierCodeSearch.TabIndex = 600
        Me.btnSupplierCodeSearch.Text = "..."
        Me.btnSupplierCodeSearch.UseVisualStyleBackColor = False
        '
        'txtsupplierCode
        '
        Me.txtsupplierCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtsupplierCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsupplierCode.ForeColor = System.Drawing.Color.Black
        Me.txtsupplierCode.Location = New System.Drawing.Point(107, 43)
        Me.txtsupplierCode.MaxLength = 20
        Me.txtsupplierCode.Name = "txtsupplierCode"
        Me.txtsupplierCode.Size = New System.Drawing.Size(96, 20)
        Me.txtsupplierCode.TabIndex = 5
        Me.txtsupplierCode.Tag = ""
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(8, 43)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 15)
        Me.Label5.TabIndex = 65
        Me.Label5.Text = "Supplier"
        '
        'txtGoldIssueNo
        '
        Me.txtGoldIssueNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldIssueNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoldIssueNo.ForeColor = System.Drawing.Color.Black
        Me.txtGoldIssueNo.Location = New System.Drawing.Point(107, 16)
        Me.txtGoldIssueNo.MaxLength = 20
        Me.txtGoldIssueNo.Name = "txtGoldIssueNo"
        Me.txtGoldIssueNo.Size = New System.Drawing.Size(96, 20)
        Me.txtGoldIssueNo.TabIndex = 0
        Me.txtGoldIssueNo.Tag = ""
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(8, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 15)
        Me.Label2.TabIndex = 49
        Me.Label2.Text = "Issue No"
        '
        'txtCurrencyName
        '
        Me.txtCurrencyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrencyName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencyName.ForeColor = System.Drawing.Color.Black
        Me.txtCurrencyName.Location = New System.Drawing.Point(251, 55)
        Me.txtCurrencyName.MaxLength = 20
        Me.txtCurrencyName.Name = "txtCurrencyName"
        Me.txtCurrencyName.ReadOnly = True
        Me.txtCurrencyName.Size = New System.Drawing.Size(192, 20)
        Me.txtCurrencyName.TabIndex = 10
        Me.txtCurrencyName.Tag = ""
        Me.txtCurrencyName.Visible = False
        '
        'txtPayable
        '
        Me.txtPayable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPayable.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayable.ForeColor = System.Drawing.Color.Black
        Me.txtPayable.Location = New System.Drawing.Point(556, 58)
        Me.txtPayable.MaxLength = 20
        Me.txtPayable.Name = "txtPayable"
        Me.txtPayable.Size = New System.Drawing.Size(72, 20)
        Me.txtPayable.TabIndex = 120
        Me.txtPayable.Tag = ""
        Me.txtPayable.Visible = False
        '
        'txtNetIssue
        '
        Me.txtNetIssue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNetIssue.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetIssue.ForeColor = System.Drawing.Color.Black
        Me.txtNetIssue.Location = New System.Drawing.Point(556, 18)
        Me.txtNetIssue.MaxLength = 20
        Me.txtNetIssue.Name = "txtNetIssue"
        Me.txtNetIssue.Size = New System.Drawing.Size(72, 20)
        Me.txtNetIssue.TabIndex = 121
        Me.txtNetIssue.Tag = ""
        Me.txtNetIssue.Visible = False
        '
        'pnlInvoice
        '
        Me.pnlInvoice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlInvoice.Controls.Add(Me.chkSelectAll)
        Me.pnlInvoice.Controls.Add(Me.Label19)
        Me.pnlInvoice.Controls.Add(Me.lblSelCost)
        Me.pnlInvoice.Controls.Add(Me.Label15)
        Me.pnlInvoice.Controls.Add(Me.lblSelweight)
        Me.pnlInvoice.Controls.Add(Me.grdInvoice)
        Me.pnlInvoice.Controls.Add(Me.btnPanelClose)
        Me.pnlInvoice.Controls.Add(Me.btnPanelOK)
        Me.pnlInvoice.Controls.Add(Me.txtCurrencyName)
        Me.pnlInvoice.Location = New System.Drawing.Point(40, 317)
        Me.pnlInvoice.Name = "pnlInvoice"
        Me.pnlInvoice.Size = New System.Drawing.Size(616, 221)
        Me.pnlInvoice.TabIndex = 135
        Me.pnlInvoice.Visible = False
        '
        'chkSelectAll
        '
        Me.chkSelectAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSelectAll.Location = New System.Drawing.Point(7, 189)
        Me.chkSelectAll.Name = "chkSelectAll"
        Me.chkSelectAll.Size = New System.Drawing.Size(80, 24)
        Me.chkSelectAll.TabIndex = 138
        Me.chkSelectAll.Text = "Select All"
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(118, 193)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(81, 14)
        Me.Label19.TabIndex = 147
        Me.Label19.Text = "Total Weight"
        '
        'lblSelCost
        '
        Me.lblSelCost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelCost.ForeColor = System.Drawing.Color.Black
        Me.lblSelCost.Location = New System.Drawing.Point(339, 224)
        Me.lblSelCost.Name = "lblSelCost"
        Me.lblSelCost.Size = New System.Drawing.Size(64, 15)
        Me.lblSelCost.TabIndex = 144
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(115, 230)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(85, 14)
        Me.Label15.TabIndex = 142
        Me.Label15.Text = "Total Weight"
        '
        'lblSelweight
        '
        Me.lblSelweight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelweight.ForeColor = System.Drawing.Color.Black
        Me.lblSelweight.Location = New System.Drawing.Point(209, 194)
        Me.lblSelweight.Name = "lblSelweight"
        Me.lblSelweight.Size = New System.Drawing.Size(64, 15)
        Me.lblSelweight.TabIndex = 141
        '
        'grdInvoice
        '
        Me.grdInvoice.AllowUserToAddRows = False
        Me.grdInvoice.AllowUserToDeleteRows = False
        Me.grdInvoice.AllowUserToOrderColumns = True
        Me.grdInvoice.AllowUserToResizeColumns = False
        Me.grdInvoice.AllowUserToResizeRows = False
        Me.grdInvoice.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdInvoice.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdInvoice.ColumnHeadersHeight = 40
        Me.grdInvoice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdInvoice.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cSel, Me.cInventoryCode, Me.cInvoice, Me.cWeight, Me.IssuedWeight, Me.cIssueWeight, Me.cAvgCost})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdInvoice.DefaultCellStyle = DataGridViewCellStyle3
        Me.grdInvoice.Location = New System.Drawing.Point(3, 13)
        Me.grdInvoice.Name = "grdInvoice"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdInvoice.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.grdInvoice.RowHeadersVisible = False
        Me.grdInvoice.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.grdInvoice.Size = New System.Drawing.Size(601, 170)
        Me.grdInvoice.TabIndex = 140
        '
        'cSel
        '
        Me.cSel.HeaderText = "Sel"
        Me.cSel.Name = "cSel"
        Me.cSel.Width = 30
        '
        'cInventoryCode
        '
        Me.cInventoryCode.HeaderText = "Inventory Code"
        Me.cInventoryCode.Name = "cInventoryCode"
        '
        'cInvoice
        '
        Me.cInvoice.HeaderText = "InvoiceDate"
        Me.cInvoice.Name = "cInvoice"
        '
        'cWeight
        '
        Me.cWeight.HeaderText = "Weight"
        Me.cWeight.Name = "cWeight"
        Me.cWeight.Width = 80
        '
        'IssuedWeight
        '
        Me.IssuedWeight.HeaderText = "Issued Weight"
        Me.IssuedWeight.Name = "IssuedWeight"
        '
        'cIssueWeight
        '
        Me.cIssueWeight.HeaderText = "Total Price"
        Me.cIssueWeight.Name = "cIssueWeight"
        '
        'cAvgCost
        '
        DataGridViewCellStyle2.Format = "N2"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.cAvgCost.DefaultCellStyle = DataGridViewCellStyle2
        Me.cAvgCost.HeaderText = "Average Cost"
        Me.cAvgCost.Name = "cAvgCost"
        Me.cAvgCost.Width = 80
        '
        'btnPanelClose
        '
        Me.btnPanelClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPanelClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPanelClose.Location = New System.Drawing.Point(515, 189)
        Me.btnPanelClose.Name = "btnPanelClose"
        Me.btnPanelClose.Size = New System.Drawing.Size(92, 23)
        Me.btnPanelClose.TabIndex = 139
        Me.btnPanelClose.Text = "Close"
        Me.btnPanelClose.UseVisualStyleBackColor = False
        '
        'btnPanelOK
        '
        Me.btnPanelOK.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPanelOK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPanelOK.Location = New System.Drawing.Point(419, 189)
        Me.btnPanelOK.Name = "btnPanelOK"
        Me.btnPanelOK.Size = New System.Drawing.Size(92, 23)
        Me.btnPanelOK.TabIndex = 136
        Me.btnPanelOK.Text = "OK"
        Me.btnPanelOK.UseVisualStyleBackColor = False
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(39, 8)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 100)
        Me.pic.TabIndex = 136
        Me.pic.TabStop = False
        '
        'grdGoldIssue
        '
        Me.grdGoldIssue.AllowUserToAddRows = False
        Me.grdGoldIssue.AllowUserToDeleteRows = False
        Me.grdGoldIssue.AllowUserToResizeColumns = False
        Me.grdGoldIssue.AllowUserToResizeRows = False
        Me.grdGoldIssue.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdGoldIssue.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.grdGoldIssue.ColumnHeadersHeight = 40
        Me.grdGoldIssue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdGoldIssue.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.colDate, Me.cInvCode, Me.colPweight, Me.colIWeight, Me.DataGridViewTextBoxColumn5, Me.colClass})
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdGoldIssue.DefaultCellStyle = DataGridViewCellStyle6
        Me.grdGoldIssue.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdGoldIssue.Location = New System.Drawing.Point(48, 350)
        Me.grdGoldIssue.Name = "grdGoldIssue"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdGoldIssue.RowHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.grdGoldIssue.RowHeadersVisible = False
        Me.grdGoldIssue.Size = New System.Drawing.Size(601, 176)
        Me.grdGoldIssue.TabIndex = 141
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Invoice No"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'colDate
        '
        Me.colDate.HeaderText = "Date"
        Me.colDate.Name = "colDate"
        '
        'cInvCode
        '
        Me.cInvCode.HeaderText = "Inventory Code"
        Me.cInvCode.Name = "cInvCode"
        '
        'colPweight
        '
        Me.colPweight.HeaderText = "Purchase Weight"
        Me.colPweight.Name = "colPweight"
        Me.colPweight.Width = 80
        '
        'colIWeight
        '
        Me.colIWeight.HeaderText = "Issued Weight"
        Me.colIWeight.Name = "colIWeight"
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "Balance Weight"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.Width = 80
        '
        'colClass
        '
        Me.colClass.HeaderText = "Class"
        Me.colClass.Name = "colClass"
        Me.colClass.Visible = False
        '
        'frmTradeInWholeSales
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(672, 616)
        Me.Controls.Add(Me.pnlInvoice)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.btnProceed)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtTradeBillNo)
        Me.Controls.Add(Me.txtPayable)
        Me.Controls.Add(Me.txtNetIssue)
        Me.Controls.Add(Me.btnBillClear)
        Me.Controls.Add(Me.btnTradeBillNoSearch)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.grdGoldIssue)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmTradeInWholeSales"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TradeIn Issue"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlInvoice.ResumeLayout(False)
        Me.pnlInvoice.PerformLayout()
        CType(Me.grdInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdGoldIssue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region
    Dim bProceed As Boolean
    Dim dABal As Double
    Dim dAIssueBal As Double
    Dim dPaidCost As Double
    Dim sClass As String
    Dim sInvoiceDate As String
    Dim sTDate As String
    Dim TotPurchaseWeight As Double
    Dim SInvoiceNo As String
    Dim sInvNo As String
    Dim sInventoryCode As String
    Dim sInvCode As String
    Dim IbLoad As Boolean
    Dim dSelectWeight As Double

    Dim objTII As New ClsTradeInIssue
    Dim objSupp As New clsSuppliers
    Dim objCurr As New clsCurrency
    Dim objPur As New clsPurchase
    Dim objGBI As ClsGoldBarIssue
    Dim objTIS As New ClsTradeinSales
    Public oTSalesConn As New ADODB.Connection
    Public oTSalesRs As New ADODB.Recordset
    Dim bEdited As Boolean

    Const cInvoiceNo = 1
    Const cInvoiceDate = 2
    Const cClass = 3
    Const cPurWeight = 4
    Const cIssuedWeight = 5
    Const cBalWeight = 6

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmTradeInIssue_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtDD.Text = Format(CDate(ServerDate), "dd")
        txtMM.Text = Format(CDate(ServerDate), "MM")
        txtYY.Text = Format(CDate(ServerDate), "yyyy")
        grdGoldIssue.RowCount = 0
        ' grdUnpaidBill.MaxRows = 0
        ' subGridSetting(grdGoldIssue)
        'subGridSetting(grdUnpaidBill)
        txtIssued.Text = "0.00"
        txtNoOfBills.Text = "0"
        txtPricePerGram.Text = "0.00"
        txtTotalPrice.Text = "0.00"
        txtTotalWt.Text = "0.00"
        SInvoiceNo = ""
        txtCurrencyCode.Text = "SGD"
        txtCurrencyName.Text = "SINGAPORE DOLLARS"
        txtCurrencyRate.Text = "1.00"
        dSelectWeight = "0.00"

        subSetDataTable()
        IbLoad = False
    End Sub

    Private Sub btnBillClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBillClear.Click
        subClearMe()
    End Sub

    Private Sub btnSupplierCodeSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSupplierCodeSearch.Click
        '''Try
        '''    Dim frmHelpCustomer As New frmHelpSearch(0, 0, "tblCustomer", "L", 4, "CustomerName", "", "CustomerCode", "CustomerCode", 80, "CustomerName", "CustomerName", 150, "Address1", "Address1", 90, "Address2", "Address2", 80)
        '''    frmHelpCustomer.ShowDialog()
        '''    txtsupplierCode.Text = frmHelpCustomer.Var1.Trim
        '''    txtsupplierCode_LostFocus(sender, e)
        '''Catch ex As Exception
        '''    MessageBox.Show(ex.Message)
        '''End Try
        Try
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSupplier", "L", 4, "SupplierName", "", "SupplierCode", "SupplierCode", 80, "SupplierName", "SupplierName", 180, "Address1", "Address1", 60, "Address2", "Address2", 80)
            frmHelpClass.ShowDialog()
            txtsupplierCode.Text = frmHelpClass.Var1.Trim
            txtSupplierName.Text = frmHelpClass.Var2.Trim
            txtsupplierCode_LostFocus(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subClearMe()
        Try
            txtsupplierCode.Clear()
            txtSupplierName.Clear()
            'txtCurrencyCode.Clear()
            'txtCurrencyName.Clear()
            'txtCurrencyRate.Clear()
            txtTradeBillNo.Clear()
            txtPayable.Clear()
            txtGoldIssueNo.Clear()
            txtIssued.Text = "0.00"
            txtNetIssue.Text = "0.00"
            txtTotalWt.Text = "0.00"
            'grdUnpaidBill.MaxRows = 0
            grdGoldIssue.RowCount = 0
            TotPurchaseWeight = 0
            txtPricePerGram.Text = "0.00"
            txtTotalPrice.Text = "0.00"
            txtNoOfBills.Text = "0"
            txtTradeIssueNo.Text = ""
            txtTradeBillNo.Enabled = True
            btnTradeBillNoSearch.Enabled = True
            btnProceed.Enabled = True
            txtTradeIssueNo.Text = objTII.GetNextTradeIssueNo()
            SInvoiceNo = ""
            dSelectWeight = "0.00"
            IbLoad = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub btnProceed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProceed.Click
        Try

            If Trim(sCompanyShortCode) = "MJ" Then
                Dim TempPurchaseWeight As Double
                Dim ActualIssuedWeight As Double
                Dim dsTH As New DataSet
                Dim dsPur As New DataSet
                Dim iCnt As Integer
                Dim sExec As String
                Dim i As Long
                Dim adoRS As ADODB.Recordset
                Dim sError As String
                Dim sIssueDate As String
                If Not fncValidation() Then Exit Sub
                Dim iRet As Boolean
                bProceed = False
                sIssueDate = txtDD.Text.Trim + "/" + txtMM.Text.Trim + "/" + txtYY.Text.Trim

                txtTradeIssueNo.Text = objTII.GetNextTradeIssueNo
                TotPurchaseWeight = Val(txtIssued.Text)


                For i = 0 To grdGoldIssue.RowCount - 1
                    dsTH = objTII.GetTradeInDetail(fnGetdgvText(grdGoldIssue, i, 0), fnGetdgvText(grdGoldIssue, i, 2))
                    If dsTH.Tables(0).Rows.Count > 0 Then
                        If TotPurchaseWeight >= Val(dsTH.Tables(0).Rows(0)("weight")) Then
                            TotPurchaseWeight = TotPurchaseWeight - Val(dsTH.Tables(0).Rows(0)("weight"))
                            TempPurchaseWeight = Val(dsTH.Tables(0).Rows(0)("weight"))
                        Else
                            TempPurchaseWeight = TotPurchaseWeight
                            TotPurchaseWeight = 0
                            bProceed = True
                        End If

                        ActualIssuedWeight = Val(fnGetdgvText(grdGoldIssue, i, 4))
                        TotPurchaseWeight = Val(fnGetdgvText(grdGoldIssue, i, 5))


                        iRet = objTII.UpdateTradeInHeaderMD(oTSalesConn, oTSalesRs, Trim(txtTradeIssueNo.Text), TotPurchaseWeight, Trim(dsTH.Tables(0).Rows(0)("InvoiceNo")), Trim(dsTH.Tables(0).Rows(0)("InventoryCode")), txtsupplierCode.Text.Trim, ActualIssuedWeight, txtRemarks.Text.Trim, sError)


                        If Trim(sError) <> "" Then
                            MessageBox.Show("Error in save")
                            Exit Sub
                        End If
                        dsTH = Nothing
                    End If
                Next
                iRet = objTII.UpdateTradeInIssueHistoryHeaderMD(oTSalesConn, oTSalesRs, Trim(txtTradeIssueNo.Text), sIssueDate, txtsupplierCode.Text.Trim, _
                                                            Val(txtNoOfBills.Text.Trim), Val(txtTotalWt.Text.Trim), Val(txtPricePerGram.Text.Trim), _
                                                            Val(txtTotalPrice.Text.Trim), Val(cboTealPercent.Text), sUserID, sError)
                If Trim(sError) <> "" Then
                    MessageBox.Show("Error in save")
                    Exit Sub
                End If
                MessageBox.Show("Tradein Gold Issued Sucessfully")
                subPrint(txtTradeIssueNo.Text.Trim)
                objTII.UpdateTradeIssueNo()
                subClearMe()
            Else
                Dim sError As String

                If Not fncValidation() Then Exit Sub
                Dim iRet As Boolean
                Dim dsTH As New DataSet
                Dim i As Long
                Dim sIssueDate As String

                sIssueDate = txtDD.Text.Trim + "/" + txtMM.Text.Trim + "/" + txtYY.Text.Trim

                txtTradeIssueNo.Text = objTII.GetNextTradeIssueNo
                TotPurchaseWeight = Val(txtIssued.Text)

                For i = 0 To grdInvoice.RowCount - 1
                    If fnGetdgvText(grdInvoice, i, 0) = True Then


                        iRet = objTII.UpdateTradeInMergeInventoryDetail(oTSalesConn, oTSalesRs, Trim(txtTradeIssueNo.Text), Trim(txtsupplierCode.Text), Trim(txtRemarks.Text), fnGetdgvText(grdInvoice, i, 1), fnGetdgvText(grdInvoice, i, 3), fnGetdgvText(grdInvoice, i, 4), fnGetdgvText(grdInvoice, i, 5), sError)


                        If Trim(sError) <> "" Then
                            MessageBox.Show("Error in save")
                            Exit Sub
                        End If
                        dsTH = Nothing
                    End If
                Next
                sIssueDate = txtDD.Text.Trim + "/" + txtMM.Text.Trim + "/" + txtYY.Text.Trim
                iRet = objTII.UpdateTradeInIssueHistoryHeader(oTSalesConn, oTSalesRs, Trim(txtTradeIssueNo.Text), sIssueDate, txtsupplierCode.Text.Trim, _
                                                             Val(txtNoOfBills.Text.Trim), Val(txtIssued.Text.Trim), Val(txtPricePerGram.Text.Trim), _
                                                              Val(txtTotalPrice.Text.Trim), Val(cboTealPercent.Text), sUserID, Trim(cboPurity.Text), sError)
                If Trim(sError) <> "" Then
                    MessageBox.Show("Error in save")
                    Exit Sub
                End If
                MessageBox.Show("Tradein Gold Issued Sucessfully")
                subPrint(txtTradeIssueNo.Text.Trim)
                '  subClearMe()
                objTII.UpdateTradeIssueNo()
                btnBillClear_Click(sender, e)
            End If
            'Dim TempPurchaseWeight As Double
            'Dim ActualIssuedWeight As Double
            'Dim dsTH As New DataSet
            'Dim dsPur As New DataSet
            'Dim iCnt As Integer
            'Dim sExec As String
            'Dim i As Long
            'Dim adoRS As ADODB.Recordset
            'Dim sError As String
            'Dim sIssueDate As String
            'If Not fncValidation() Then Exit Sub
            'Dim iRet As Boolean
            'bProceed = False
            'sIssueDate = txtDD.Text.Trim + "/" + txtMM.Text.Trim + "/" + txtYY.Text.Trim

            txtTradeIssueNo.Text = objTII.GetNextTradeIssueNo
            'TotPurchaseWeight = Val(txtIssued.Text)


            'For i = 0 To grdGoldIssue.RowCount - 1
            '    dsTH = objTII.GetTradeInDetail(fnGetdgvText(grdGoldIssue, i, 0), fnGetdgvText(grdGoldIssue, i, 2))
            '    If dsTH.Tables(0).Rows.Count > 0 Then
            '        If TotPurchaseWeight >= Val(dsTH.Tables(0).Rows(0)("Balanceweight")) Then
            '            TotPurchaseWeight = TotPurchaseWeight - Val(dsTH.Tables(0).Rows(0)("Balanceweight"))
            '            TempPurchaseWeight = Val(dsTH.Tables(0).Rows(0)("Balanceweight"))
            '        Else
            '            TempPurchaseWeight = TotPurchaseWeight
            '            TotPurchaseWeight = 0
            '            bProceed = True
            '        End If

            '        ActualIssuedWeight = Val(fnGetdgvText(grdGoldIssue, i, 4))
            '        TotPurchaseWeight = Val(fnGetdgvText(grdGoldIssue, i, 5))


            ' iRet = objTII.UpdateTradeInHeader(oTSalesConn, oTSalesRs, Trim(txtTradeIssueNo.Text), TotPurchaseWeight, Trim(dsTH.Tables(0).Rows(0)("InvoiceNo")), Trim(dsTH.Tables(0).Rows(0)("InventoryCode")), txtsupplierCode.Text.Trim, ActualIssuedWeight, txtRemarks.Text.Trim, sError)


            '        If Trim(sError) <> "" Then
            '            MessageBox.Show("Error in save")
            '            Exit Sub
            '        End If
            '        dsTH = Nothing
            '    End If
            'Next
            '  iRet = objTII.UpdateTradeInIssueHistoryHeader(oTSalesConn, oTSalesRs, Trim(txtTradeIssueNo.Text), sIssueDate, txtsupplierCode.Text.Trim, _
            '                                              Val(txtNoOfBills.Text.Trim), Val(txtTotalWt.Text.Trim), Val(txtPricePerGram.Text.Trim), _
            '                                              Val(txtTotalPrice.Text.Trim), Val(cboTealPercent.Text), sUserID, sError)
            'If Trim(sError) <> "" Then
            '    MessageBox.Show("Error in save")
            '    Exit Sub
            'End If
            'MessageBox.Show("Tradein Gold Issued Sucessfully")
            'subPrint(txtTradeIssueNo.Text.Trim)
            'objTII.UpdateTradeIssueNo()
            'subClearMe()

           
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        ''Block by Durai on April 8th

        ''''----------------- Trade issue ----------------
        '''txtNetIssue.Text = Format(Str((CDbl(txtIssued.Text) * 0.895) / 0.92), "#######.00")
        '''TotPurchaseWeight = txtNetIssue.Text
        '''bProceed = False

        '''dsPur = objPur.GetPurchaseBill(Trim(txtsupplierCode.Text))
        '''For iCnt = 0 To dsPur.Tables(0).Rows.Count
        '''    If TotPurchaseWeight >= Val(dsPur.Tables(0).Rows(iCnt)("Balanceweight")) Then
        '''        TotPurchaseWeight = TotPurchaseWeight - Val(dsPur.Tables(0).Rows(0)("Balanceweight"))
        '''        TempPurchaseWeight = Val(dsPur.Tables(0).Rows(iCnt)("Balanceweight") & "")
        '''    Else
        '''        TempPurchaseWeight = TotPurchaseWeight
        '''        TotPurchaseWeight = 0
        '''        bProceed = True
        '''    End If

        '''    If dsPur.Tables(0).Rows.Count > 0 Then
        '''        'Dim sbll1 As GoldBarIssue
        '''        Call objGBI.GoldBarIssueProceed(Trim(dsPur.Tables(0).Rows(iCnt)("InvoiceNo") & ""), CDate(Trim(dsPur.Tables(0).Rows(iCnt)("InvoiceDate") & "")), Trim(txtsupplierCode.Text), Val(dsPur.Tables(0).Rows(iCnt)("PurchaseWeight")), _
        '''                  Val(dsPur.Tables(0).Rows(iCnt)("PayableAmount")), Val(TempPurchaseWeight / 0.895) * 0.92, Val(TempPurchaseWeight), Trim(txtTradeIssueNo.Text), sInvoiceDate, "24K-999", Val(dPaidCost), "G")
        '''        If bProceed Then
        '''            Exit For
        '''        End If
        '''    End If
        '''Next
        '''dsPur = Nothing
        '''objGBI.UpdateGoldBarPurchase(txtIssued.Text.Trim, txtNetIssue.Text.Trim, txtTradeIssueNo.Text.Trim)


    End Sub

    Function fncValidation() As Boolean
        Dim bStatus As Boolean
        Dim dsSupp As New DataSet
        Try
            bStatus = False
            If fnDateValid(Val(txtDD.Text), Val(txtMM.Text), Val(txtYY.Text)) = False Then
                MessageBox.Show("Pls Check the Date ")
                txtDD.Focus()
                Exit Function
            End If
            If Len(Trim$(txtsupplierCode.Text)) = 0 Then
                MessageBox.Show("Supplier cannot be blank!")
                txtsupplierCode.Focus()
                Exit Function
                'Else
                '    If objSupp.IsSupplierCodeExist(Trim(txtsupplierCode.Text.Trim)) Then
                '        MessageBox.Show("Please Enter the Valid Supplier!")
                '        Exit Function
                '    End If
            End If
            If Len(Trim$(txtCurrencyCode.Text)) = 0 Then
                MessageBox.Show("Currency cannot be blank!")
                txtCurrencyCode.Focus()
                Exit Function
                'Else
                '    If objCurr.IsCurrencyExist(Trim(txtCurrencyCode.Text)) Then
                '        MessageBox.Show("Please Enter the Valid Currency Code!")
                '        Exit Function
                '    End If
            End If

            If Val(txtCurrencyRate.Text) = 0 Then
                MessageBox.Show("Rate must be greater than zero!")
                txtCurrencyRate.Focus()
                Exit Function
            End If

            If Val(txtIssued.Text) = 0 Then
                MessageBox.Show("Gold Issued Actual Weight Should Not Be 0 !!! ")
                Exit Function
            End If

            'If Val(txtNetIssue.Text) = 0 Then
            '    MessageBox.Show("Gold Issued Converted Weight Should Not Be 0 !!! ")
            '    Exit Function
            'End If

            'If grdGoldIssue.RowCount = 0 Then
            '    If Len(Trim(fnGetdgvText(grdGoldIssue, 1, 1))) = 0 Then
            '        MessageBox.Show("Pls check the GoldBar Inventory!")
            '        Exit Function
            '    End If
            'End If
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncValidation = bStatus
        End Try
    End Function

    'Private Sub subGetPurchaseBill()
    '    Try
    '        Dim icnt As Long
    '        Dim sDate As Date
    '        Dim dsPur As New DataSet
    '        'Dim SBll As Purchase
    '        'SBll = New Purchase
    '        dsPur = objPur.GetPurchaseBill(Trim(txtsupplierCode.Text))
    '        If dsPur.Tables(0).Rows.Count > 0 Then
    '            grdUnpaidBill.MaxRows = 0
    '            txtPayable.Text = ""
    '            For icnt = 0 To dsPur.Tables(0).Rows.Count - 1
    '                grdUnpaidBill.MaxRows = grdUnpaidBill.MaxRows + 1
    '                grdUnpaidBill.Row = grdUnpaidBill.MaxRows
    '                sDate = CDate(Trim(dsPur.Tables(0).Rows(icnt)("InvoiceDate")))
    '                Call SetText(grdUnpaidBill, grdUnpaidBill.Row, 1, Trim(dsPur.Tables(0).Rows(icnt)("InvoiceNo") & ""))
    '                Call SetText(grdUnpaidBill, grdUnpaidBill.Row, 2, Trim(sDate & ""))
    '                Call SetText(grdUnpaidBill, grdUnpaidBill.Row, 3, Val(dsPur.Tables(0).Rows(icnt)("PurchaseWeight") & ""))
    '                Call SetText(grdUnpaidBill, grdUnpaidBill.Row, 4, Val(dsPur.Tables(0).Rows(icnt)("IssuedWeight") & ""))
    '                Call SetText(grdUnpaidBill, grdUnpaidBill.Row, 5, Val(dsPur.Tables(0).Rows(icnt)("Balanceweight") & ""))
    '                txtPayable.Text = Format((Val(txtPayable.Text) + GetText(grdUnpaidBill, grdUnpaidBill.Row, 5)), "0.00")
    '            Next icnt
    '            grdUnpaidBill.Row = 1
    '            grdUnpaidBill.Col = 1
    '            grdUnpaidBill.Action = 0
    '        Else
    '            '''grdUnpaidBill.MaxRows = 0
    '            '''MessageBox.Show("No Balance for this Supplier")
    '            '''txtsupplierCode.Text = ""
    '            '''txtSupplierName.Text = ""
    '            '''txtsupplierCode.Focus()
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try

    'End Sub

    Private Sub subGetTradeInInvoices()
        Try
            Dim icnt As Long
            Dim sDate As Date
            Dim dsPur As New DataSet
            Dim sPurity As String
            'Dim SBll As Purchase
            'SBll = New Purchase
            pnlInvoice.Visible = True
            If cboPurity.Text = "999.9" Then
                sPurity = 1000
            ElseIf cboPurity.Text = "22K" Then
                sPurity = 22
            Else
                sPurity = Trim(cboPurity.Text)
            End If


            dsPur = objTIS.GetTradeInIssue(SInvoiceNo, Trim(sPurity), Trim(sDate))
            If dsPur.Tables(0).Rows.Count > 0 Then
                'grdInvoice.MaxRows = 0
                ''txtPayable.Text = ""
                'For icnt = 0 To dsPur.Tables(0).Rows.Count - 1
                '    grdInvoice.MaxRows = grdInvoice.MaxRows + 1
                '    grdInvoice.Row = grdInvoice.MaxRows
                '    Call SetText(grdInvoice, grdInvoice.Row, 1, 0)
                '    Call SetText(grdInvoice, grdInvoice.Row, 2, Trim(dsPur.Tables(0).Rows(icnt)("InvoiceNo") & ""))
                '    Call SetText(grdInvoice, grdInvoice.Row, 3, Trim(dsPur.Tables(0).Rows(icnt)("InventoryCode") & ""))
                '    Call SetText(grdInvoice, grdInvoice.Row, 4, Val(dsPur.Tables(0).Rows(icnt)("TotalWeight") & ""))
                '    Call SetText(grdInvoice, grdInvoice.Row, 5, Trim(dsPur.Tables(0).Rows(icnt)("CustomerCode") & ""))
                'Next icnt
                'grdInvoice.Row = 1
                'grdInvoice.Col = 1
                'grdInvoice.Action = 0
                grdInvoice.RowCount = 0
                If Trim(sCompanyShortCode) <> "MJ" Then
                    If Not IsNothing(dsPur) Then
                        If dsPur.Tables(0).Rows.Count > 0 Then
                            For icnt = 0 To dsPur.Tables(0).Rows.Count - 1
                                grdInvoice.Rows.Add()
                                Call fnSetdgvText(grdInvoice, icnt, 0, False)
                                'fnSetdgvText(grdInvoice, icnt, 1, Trim(dsPur.Tables(0).Rows(icnt)("InvoiceNo") & ""))
                                'fnSetdgvText(grdInvoice, icnt, 2, Trim(dsPur.Tables(0).Rows(icnt)("InvoiceDate") & ""))
                                'fnSetdgvText(grdInvoice, icnt, 3, Trim(dsPur.Tables(0).Rows(icnt)("InventoryCode") & ""))
                                'fnSetdgvText(grdInvoice, icnt, 4, Trim(dsPur.Tables(0).Rows(icnt)("Weight") & ""))
                                'fnSetdgvText(grdInvoice, icnt, 5, Trim(dsPur.Tables(0).Rows(icnt)("Weight") & ""))
                                'fnSetdgvText(grdInvoice, icnt, 6, Trim(dsPur.Tables(0).Rows(icnt)("CustomerCode") & ""))

                                fnSetdgvText(grdInvoice, icnt, 1, Trim(dsPur.Tables(0).Rows(icnt)("TradeinInventoryCode") & ""))
                                fnSetdgvText(grdInvoice, icnt, 2, Trim(dsPur.Tables(0).Rows(icnt)("InvoiceDate") & ""))
                                fnSetdgvText(grdInvoice, icnt, 3, Trim(dsPur.Tables(0).Rows(icnt)("TotalWeight") & ""))
                                fnSetdgvText(grdInvoice, icnt, 4, Trim(dsPur.Tables(0).Rows(icnt)("TotalWeight") & ""))
                                fnSetdgvText(grdInvoice, icnt, 5, Trim(dsPur.Tables(0).Rows(icnt)("TOtalPrice") & ""))
                                '   fnSetdgvText(grdInvoice, icnt, 6, Trim(dsPur.Tables(0).Rows(icnt)("AvgCost") & ""))
                                fnSetdgvText(grdInvoice, icnt, 6, Format(Val(dsPur.Tables(0).Rows(icnt)("AvgCost") & ""), "0.00"))

                            Next
                        End If

                    End If
                Else

                    If Not IsNothing(dsPur) Then
                        If dsPur.Tables(0).Rows.Count > 0 Then
                            For icnt = 0 To dsPur.Tables(0).Rows.Count - 1
                                grdInvoice.Rows.Add()
                                Call fnSetdgvText(grdInvoice, icnt, 0, False)
                                fnSetdgvText(grdInvoice, icnt, 1, Trim(dsPur.Tables(0).Rows(icnt)("InvoiceNo") & ""))
                                fnSetdgvText(grdInvoice, icnt, 2, Trim(dsPur.Tables(0).Rows(icnt)("CreateDate") & ""))
                                '   fnSetdgvText(grdInvoice, icnt, 3, Trim(dsPur.Tables(0).Rows(icnt)("InventoryCode") & ""))
                                fnSetdgvText(grdInvoice, icnt, 3, Trim(dsPur.Tables(0).Rows(icnt)("BalanceWeight") & ""))
                                fnSetdgvText(grdInvoice, icnt, 4, Trim(dsPur.Tables(0).Rows(icnt)("BalanceWeight") & ""))
                                fnSetdgvText(grdInvoice, icnt, 5, Trim(dsPur.Tables(0).Rows(icnt)("Totalcost") & ""))
                                fnSetdgvText(grdInvoice, icnt, 6, Val(0))

                            Next
                        End If

                    End If
                End If
            Else
                '''grdUnpaidBill.MaxRows = 0
                '''MessageBox.Show("No Balance for this Supplier")
                '''txtsupplierCode.Text = ""
                '''txtSupplierName.Text = ""
                '''txtsupplierCode.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub btnTradeBillNoSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTradeBillNoSearch.Click
        Try
            ''If Trim(sCompanyShortCode) = "MD" Then
            ''    Dim frmHelpBill As New frmHelpSearch(0, 0, "tblTradeInHeader", "L", 4, "InvoiceNo", " ((IssueFlag is Null) or (IssueFlag<> 'Y'))", "InvoiceNo", "InvoiceNo", 80, "InventoryCode", "InventoryCode", 80, "TotalWeight", "TotalWeight", 80, "CustomerCode", "CustomerCode", 60)
            ''    frmHelpBill.ShowDialog()
            ''    txtTradeBillNo.Text = frmHelpBill.Var1.Trim
            ''    If txtTradeBillNo.Text.Trim <> "" Then
            ''        subGetTradeBillNo()
            ''    End If
            ''    'If Val(cboTealPercent.Text) = 0 Then
            ''    '    MessageBox.Show("Cost Percentage Should not be Empty!")
            ''    '    cboTealPercent.Focus()
            ''    '    Exit Sub
            ''    'End If
            ''Else

            lblSelweight.Text = "0.00"
            subGetTradeInInvoices()
            'End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subGetTradeBillNo()
        Try
            Dim dsTIB As New DataSet
            Dim iCnt As Long

            dsTIB = objTIS.GetTradeInBalanceSales(Trim(SInvoiceNo), Trim(sInventoryCode))
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
            dABal = 0
            dAIssueBal = 0
            grdGoldIssue.RowCount = 0
            'grdGoldIssue.MaxRows = 0
            If dsTIB.Tables(0).Rows.Count > 0 Then
                'txtInvoiceNo = Trim(adoRs!InvoiceNo & "")
                sInvoiceDate = Trim(dsTIB.Tables(0).Rows(0)("ModifyDate") & "")
                sTDate = Trim(dsTIB.Tables(0).Rows(0)("ModifyDate") & "")
                sTDate = DateValue(sTDate)
                For iCnt = 0 To dsTIB.Tables(0).Rows.Count - 1
                    'grdGoldIssue.MaxRows = grdGoldIssue.MaxRows + 1
                    'grdGoldIssue.Row = grdGoldIssue.MaxRows
                    grdGoldIssue.Rows.Add()

                    'fnSetdgvText(grdGoldIssue, iCnt, 1, Trim(dsTIB.Tables(0).Rows(iCnt)("InvoiceNo") & ""))
                    'fnSetdgvText(grdGoldIssue, iCnt, 2, sTDate)
                    'fnSetdgvText(grdGoldIssue, iCnt, 3, "TRADE-IN")
                    'fnSetdgvText(grdGoldIssue, iCnt, 4, Val(dsTIB.Tables(0).Rows(iCnt)("TotalWeight") & ""))
                    'fnSetdgvText(grdGoldIssue, iCnt, 4, Val(dsTIB.Tables(0).Rows(iCnt)("TotalWeight") * Val(txtPerc.Text) & ""))
                    'fnSetdgvText(grdGoldIssue, iCnt, 4, Val(dsTIB.Tables(0).Rows(iCnt)("TotalWeight") & ""))
                    'fnSetdgvText(grdGoldIssue, iCnt, 4, Val(dsTIB.Tables(0).Rows(iCnt)("TotalWeight") & ""))
                    'fnSetdgvText(grdGoldIssue, iCnt, 4, Val(dsTIB.Tables(0).Rows(iCnt)("TotalWeight") & ""))

                    Call fnSetdgvText(grdGoldIssue, iCnt, 0, Trim(dsTIB.Tables(0).Rows(iCnt)("InvoiceNo") & ""))
                    Call fnSetdgvText(grdGoldIssue, iCnt, 1, sTDate)
                    Call fnSetdgvText(grdGoldIssue, iCnt, 2, "TRADE-IN")
                    Call fnSetdgvText(grdGoldIssue, iCnt, 3, Val(dsTIB.Tables(0).Rows(iCnt)("TotalWeight") & ""))
                    ''' Call SetText(grdGoldIssue, grdGoldIssue.Row, 5, Val(dsTIB.Tables(0).Rows(iCnt)("IssuedWeight") & ""))
                    Call fnSetdgvText(grdGoldIssue, iCnt, 4, Val(dsTIB.Tables(0).Rows(iCnt)("TotalWeight") * Val(cboTealPercent.Text) & ""))
                    Call fnSetdgvText(grdGoldIssue, iCnt, 5, Val(dsTIB.Tables(0).Rows(iCnt)("Balanceweight") & ""))
                    dABal = dABal + Val(dsTIB.Tables(0).Rows(iCnt)("TotalWeight") & "")
                    dAIssueBal = dAIssueBal + Val(dsTIB.Tables(0).Rows(iCnt)("TotalWeight") * Val(cboTealPercent.Text) & "")
                    If Val(dsTIB.Tables(0).Rows(iCnt)("Balanceweight")) > 0 Then
                        dPaidCost = dPaidCost + Val(dsTIB.Tables(0).Rows(iCnt)("Amount") & "") / Val(dsTIB.Tables(0).Rows(iCnt)("Balanceweight") & "")
                    Else
                        dPaidCost = 0
                    End If
                Next iCnt

                'grdGoldIssue.Row = 1
                'grdGoldIssue.Col = 1
                'grdGoldIssue.Action = 0
            Else
                'grdGoldIssue.MaxRows = 0
                Exit Sub
            End If

            '''txtIssued.Text = Val(dABal) + Val(txtIssued.Text)
            txtIssued.Text = Val(dABal)
            txtIssued.Text = CDbl(txtIssued.Text)
            '''txtTotalWt.Text = Val(txtIssued.Text) * CDbl(txtPerc.Text)
            txtTotalWt.Text = Format(Val(dAIssueBal), "0.00")
            txtTotalWt.Text = Format(CDbl(txtTotalWt.Text), "0.00")
            If chkExport.Checked = False Then
                txtTotalPrice.Text = Format((CDbl(txtTotalWt.Text) * CDbl(txtPricePerGram.Text)) + ((CDbl(txtTotalWt.Text) * CDbl(txtPricePerGram.Text)) * (Objuser.GstPercentage / 100)), "####00.00")
            Else
                txtTotalPrice.Text = Format((CDbl(txtTotalWt.Text) * CDbl(txtPricePerGram.Text)), "####00.00")
            End If
            txtNoOfBills.Text = grdGoldIssue.RowCount - 1
            'txtNetIssue.Text = Format(Str((CDbl(txtIssued.Text) * 0.895) / 0.92), "#######.00")
            'lblTrade.Text = "TRADE"
            sClass = "TRADE"
            IbLoad = True
            txtTradeBillNo.Text = ""
            txtTradeBillNo.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnTradeBillNoSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnTradeBillNoSearch.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then btnTradeBillNoSearch_Click(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtsupplierCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtsupplierCode.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then btnSupplierCodeSearch_Click(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtsupplierCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsupplierCode.LostFocus
        Try
            If Trim(txtsupplierCode.Text) <> "" Then
                subGetSupplier()
                'subGetPurchaseBill()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subGetSupplier()
        Try
            Dim dsSup As New DataSet
            dsSup = objSupp.GetSupplierDetail(Trim(txtsupplierCode.Text))
            '  dsSup = objSupp.GetCustomerwithCode(txtsupplierCode.Text.Trim)
            If dsSup.Tables(0).Rows.Count > 0 Then
                txtSupplierName.Text = Trim(dsSup.Tables(0).Rows(0)("SupplierName") & "")
                txtsupplierCode.ReadOnly = True
            Else
                txtsupplierCode.ReadOnly = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnCurrencySearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCurrencySearch.Click
        Try
            Dim frmHelpCur As New frmHelpSearch(0, 0, "tblcurrency", "L", 4, "CurrencyCode", "", "CurrencyCode", "CurrencyCode", 80, "CurrencyName", "CurrencyName", 180, "CurrencyRate", "CurrencyRate", 60, "Remarks", "Remarks", 80)
            frmHelpCur.ShowDialog()
            txtCurrencyCode.Text = frmHelpCur.Var1.Trim
            txtCurrencyCode_LostFocus(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtCurrencyCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCurrencyCode.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then btnCurrencySearch_Click(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtCurrencyCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCurrencyCode.LostFocus
        Try
            subGetCurrency()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subGetCurrency()
        Try
            Dim dsCur As New DataSet
            dsCur = objCurr.GetCurrencyDetail(Trim(txtCurrencyCode.Text))
            If dsCur.Tables(0).Rows.Count > 0 Then
                txtCurrencyName.Text = Trim(dsCur.Tables(0).Rows(0)("CurrencyName") & "")
                txtCurrencyRate.Text = Trim(dsCur.Tables(0).Rows(0)("CurrencyRate") & "")
                txtCurrencyCode.ReadOnly = True
            Else
                txtCurrencyCode.ReadOnly = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtIssued_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIssued.LostFocus
        Try
            If Val(txtPayable.Text) = 0 Then
                txtPayable.Focus()
                Exit Sub
            End If
            If Val(txtIssued.Text) = 0 Then
                txtIssued.Focus()
                Exit Sub
            End If
            txtNetIssue.Text = Format(Str((CDbl(txtIssued.Text) * 0.895) / 0.92), "#######.00")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnIssueNoSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIssueNoSearch.Click
        Try
            Dim frmHelp As New frmHelpSearch(0, 0, "tblTradeIssueHistory", "L", 4, "SupplierName", "", "TradeIssueNo", "TradeIssueNo", 80, "SupplierCode", "SupplierCode", 80, "CustomerCode", "CustomerCode", 80, "InvoiceNo", "InvoiceNo", 80)
            frmHelp.ShowDialog()
            txtTradeIssueNo.Text = frmHelp.Var1.Trim
            txtTradeIssueNo_LostFocus(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If txtTradeIssueNo.Text.Trim <> "" Then
                subPrint(txtTradeIssueNo.Text.Trim)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
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
            sReportTitle = "Trade In Issue "
            If UCase(sCompanyShortCode) = "MJ" Then
                If chkExport.Checked = True Then
                    strpath = Application.StartupPath & "\Reports\rptTradeInGoldIssueExport_HN.rpt"
                Else
                    strpath = Application.StartupPath & "\Reports\rptTradeInGoldIssue_HN.rpt"
                End If

            Else
                strpath = Application.StartupPath & "\Reports\rptTradeInGoldIssue.rpt"
            End If
            CR.Load(strpath)

            'CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(sCompanyname)
            'CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(sAddress)
            'CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText(sReportTitle)
            'CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(sPhone)
            'CR.DataDefinition.FormulaFields("CompAddress4").Text = fncFormatSQLText(sFax)
            ReportLogOnToServer(CR)
            CR.Refresh()
            frmrep.ReportViewer.ReportSource = CR
            If Trim(sCompanyShortCode) <> "MJ" Then
                sSelection = "{tblTradeIssueHistoryHeader.TradeIssueNo} = " & fncFormatSQLText(Trim(sTradeInIssueNo))
            Else
                sSelection = "{tblTradeIssueHistory.TradeIssueNo} = " & fncFormatSQLText(Trim(sTradeInIssueNo))
            End If
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

    Private Sub txtTradeIssueNo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTradeIssueNo.LostFocus
        Dim dsTII As New DataSet
        Dim objTII As New ClsTradeInIssue
        Dim iCnt As Integer
        Try
            If txtTradeIssueNo.Text.Trim <> "" Then
                dsTII = objTII.GetTradeIssueHistory(txtTradeIssueNo.Text.Trim)
                If dsTII.Tables(0).Rows.Count Then
                    txtsupplierCode.Text = dsTII.Tables(0).Rows(iCnt)("SupplierCode")
                    txtsupplierCode_LostFocus(sender, e)
                    txtTradeBillNo.Enabled = False
                    btnTradeBillNoSearch.Enabled = False
                    btnProceed.Enabled = False
                    grdGoldIssue.RowCount = 0
                    'For iCnt = 0 To dsTII.Tables(0).Rows.Count - 1
                    '    grdGoldIssue.MaxRows = grdGoldIssue.MaxRows + 1
                    '    SetText(grdGoldIssue, grdGoldIssue.MaxRows, cInvoiceNo, Trim(dsTII.Tables(0).Rows(iCnt)("InvoiceNo") & ""))

                    'Next
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtPricePerGram_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPricePerGram.Leave
        If Not IsNumeric(txtPricePerGram.Text.Trim) = True Then
            MessageBox.Show("Enter Correct Cost Per Gram ")
            txtPricePerGram.Text = "0.00"
            txtPricePerGram.Focus()
        ElseIf CDbl(txtPricePerGram.Text.Trim) > 0 Then
            ' txtTotalPrice.Text = Format((CDbl(txtTotalWt.Text) * CDbl(txtPricePerGram.Text)) + ((CDbl(txtTotalWt.Text) * CDbl(txtPricePerGram.Text)) * (Objuser.GstPercentage / 100)), "####00.00")
            If chkExport.Checked = False Then
                txtTotalPrice.Text = Format((CDbl(txtTotalWt.Text) * CDbl(txtPricePerGram.Text)) + ((CDbl(txtTotalWt.Text) * CDbl(txtPricePerGram.Text)) * (Objuser.GstPercentage / 100)), "####00.00")
            Else
                txtTotalPrice.Text = Format((CDbl(txtTotalWt.Text) * CDbl(txtPricePerGram.Text)), "####00.00")
            End If
        End If

    End Sub

    Private Sub txtTotalPrice_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotalPrice.Leave
        If Not IsNumeric(txtTotalPrice.Text.Trim) = True Then
            MessageBox.Show("Enter Correct Cost Per Gram ")
            txtTotalPrice.Text = "0.00"
            txtTotalPrice.Focus()
            'ElseIf CDbl(txtTotalPrice.Text.Trim) > 0 Then
            '    txtPricePerGram.Text = CDbl(txtTotalPrice.Text.Trim) / CDbl(txtIssued.Text.Trim)
        End If
    End Sub

    Private Sub frmTradeInIssue_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                SendKeys.Send("{Tab}")
            Case Keys.Up
                SendKeys.Send("+{Tab}")
            Case Keys.Down
                SendKeys.Send("{Tab}")
            Case Keys.Escape
                btnClose_Click(sender, e)
        End Select
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
        If Trim(fnGetdgvText(grdInvoice, grdInvoice.CurrentRow.Index, 0)) = False Then
            Call fnSetdgvText(grdInvoice, grdInvoice.CurrentRow.Index, 0, True)
        Else
            Call fnSetdgvText(grdInvoice, grdInvoice.CurrentRow.Index, 0, False)
        End If
        '  End If 
    End Sub

    Private Sub grdInvoice_MouseUpEvent(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_MouseUpEvent)

    End Sub


    Private Sub btnPanelOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPanelOK.Click
        Dim i As Long
        Dim j As Long
        ''  SInvoiceNo = ""
        'sInvNo = ""
        'sInvCode = ""
        Dim dWeight As Double
        Dim dIssuedWeight As Double
        Dim dTotalPrice As Double


        If Trim(sCompanyShortCode) = "MJ" Then
            For i = 0 To grdInvoice.RowCount - 1
                If Trim(fnGetdgvText(grdInvoice, i, 0)) = True Then
                    ''SInvoiceNo = SInvoiceNo & fncFormatSQLText(GetText(grdInvoice, i, 2)) & ","
                    SInvoiceNo = SInvoiceNo & "," & fncFormatSQLText(fnGetdgvText(grdInvoice, i, 1))
                    sInvNo = sInvNo & "," & fncFormatSQLText(fnGetdgvText(grdInvoice, i, 1))
                    sInventoryCode = sInventoryCode & "," & fncFormatSQLText(fnGetdgvText(grdInvoice, i, 2))
                    sInvCode = sInvCode & "," & fncFormatSQLText(fnGetdgvText(grdInvoice, i, 2))
                End If
            Next
            'SInvoiceNo = Strings.Left(SInvoiceNo, Len(SInvoiceNo) - 1)
            If IbLoad Then
                SInvoiceNo = SInvoiceNo
                sInvNo = Strings.Right(sInvNo, Len(sInvNo) - 1)
                sInventoryCode = sInventoryCode
                sInvCode = Strings.Right(sInvNo, Len(sInvCode) - 1)
            Else
                SInvoiceNo = Strings.Right(SInvoiceNo, Len(SInvoiceNo) - 1)
                sInvNo = Strings.Right(sInvNo, Len(sInvNo) - 1)
                sInventoryCode = Strings.Right(sInventoryCode, Len(sInventoryCode) - 1)
                sInvCode = Strings.Right(sInvCode, Len(sInvCode) - 1)
            End If
            'subGetTradeBillNo()
            fncAddInvoices()
            subAddTradeInToGrid()
            pnlInvoice.Visible = False
        Else
            j = 0

            dWeight = 0
            dIssuedWeight = 0
            dTotalPrice = 0
            For i = 0 To grdInvoice.RowCount - 1
                If Trim(fnGetdgvText(grdInvoice, i, 0)) = True Then
                    ''SInvoiceNo = SInvoiceNo & fncFormatSQLText(GetText(grdInvoice, i, 2)) & ","
                    dWeight = dWeight + Val(fnGetdgvText(grdInvoice, i, 3))
                    dIssuedWeight = dIssuedWeight + Val(fnGetdgvText(grdInvoice, i, 4))
                    dTotalPrice = dTotalPrice + Val(fnGetdgvText(grdInvoice, i, 6))
                    j = j + 1
                End If
            Next
            ''SInvoiceNo = Strings.Left(SInvoiceNo, Len(SInvoiceNo) - 1)
            'If IbLoad Then
            '    SInvoiceNo = SInvoiceNo
            '    sInvNo = Strings.Right(sInvNo, Len(sInvNo) - 1)
            '    sInventoryCode = sInventoryCode
            '    sInvCode = Strings.Right(sInvNo, Len(sInvCode) - 1)
            'Else
            '    SInvoiceNo = Strings.Right(SInvoiceNo, Len(SInvoiceNo) - 1)
            '    sInvNo = Strings.Right(sInvNo, Len(sInvNo) - 1)
            '    sInventoryCode = Strings.Right(sInventoryCode, Len(sInventoryCode) - 1)
            '    sInvCode = Strings.Right(sInvCode, Len(sInvCode) - 1)
            'End If
            ''subGetTradeBillNo()
            ''  fncAddInvoices()
            ''  subAddTradeInToGrid()
            dTotalPrice = Val(dTotalPrice) / j
            txtTotalWt.Text = Val(dWeight)
            txtIssued.Text = Val(dIssuedWeight)
            txtTotalPrice.Text = Val(dIssuedWeight) * Val(dTotalPrice)
            'pnlInvoice.Visible = False
        End If

    End Sub

    Private Sub chkSelectAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSelectAll.CheckedChanged
        Dim i As Long
        For i = 0 To grdInvoice.RowCount - 1
            'If Trim(GetText(grdInvoice, i, 1)) = 1 Then
            '    Call SetText(grdInvoice, i, 1, 0)
            'Else
            If chkSelectAll.Checked = True Then
                Call fnSetdgvText(grdInvoice, i, 0, True)
                dSelectWeight = dSelectWeight + Val(fnGetdgvText(grdInvoice, i, 3))
            Else
                Call fnSetdgvText(grdInvoice, i, 0, False)
                dSelectWeight = 0
            End If
            'End If
        Next
        lblSelweight.Text = Val(dSelectWeight)
    End Sub

    Private Sub btnPanelClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPanelClose.Click
        pnlInvoice.Visible = False
    End Sub

    Private Sub grdInvoice_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdInvoice.CellClick
        Dim i As Long
        dSelectWeight = 0
        If (fnGetdgvText(grdInvoice, grdInvoice.CurrentRow.Index, 0)) = False Then
            Call fnSetdgvText(grdInvoice, grdInvoice.CurrentRow.Index, 0, True)
            '    dSelectWeight = dSelectWeight + Val(fnGetdgvText(grdInvoice, grdInvoice.CurrentRow.Index, 5))
        Else
            Call fnSetdgvText(grdInvoice, grdInvoice.CurrentRow.Index, 0, False)
            '   dSelectWeight = dSelectWeight - Val(fnGetdgvText(grdInvoice, grdInvoice.CurrentRow.Index, 5))
        End If
        lblSelweight.Text = Val(dSelectWeight)

        For i = 0 To grdInvoice.RowCount - 1
            If (fnGetdgvText(grdInvoice, i, 0)) = True Then
                dSelectWeight = dSelectWeight + Val(fnGetdgvText(grdInvoice, i, 3))
            End If
        Next
        lblSelweight.Text = Val(dSelectWeight)
    End Sub

    Private Sub grdInvoice_CellContentClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdInvoice.CellContentClick
        'Dim i As Long
        'dSelectWeight = 0
        'If Trim(fnGetdgvText(grdInvoice, grdInvoice.CurrentRow.Index, 0)) = False Then
        '    Call fnSetdgvText(grdInvoice, grdInvoice.CurrentRow.Index, 0, True)
        '    '    dSelectWeight = dSelectWeight + Val(fnGetdgvText(grdInvoice, grdInvoice.CurrentRow.Index, 5))
        'Else
        '    Call fnSetdgvText(grdInvoice, grdInvoice.CurrentRow.Index, 0, False)
        '    '   dSelectWeight = dSelectWeight - Val(fnGetdgvText(grdInvoice, grdInvoice.CurrentRow.Index, 5))
        'End If
        'lblSelweight.Text = Val(dSelectWeight)

        'For i = 0 To grdInvoice.RowCount - 1
        '    If Trim(fnGetdgvText(grdInvoice, i, 0)) = True Then
        '        dSelectWeight = dSelectWeight + Val(fnGetdgvText(grdInvoice, i, 5))
        '    End If
        'Next
        'lblSelweight.Text = Val(dSelectWeight)
    End Sub

    Private Sub grdInvoice_CellStateChanged(sender As Object, e As System.Windows.Forms.DataGridViewCellStateChangedEventArgs) Handles grdInvoice.CellStateChanged
        'Dim i As Long
        'dSelectWeight = 0
        'If (fnGetdgvText(grdInvoice, grdInvoice.CurrentRow.Index, 0)) = 0 Then
        '    Call fnSetdgvText(grdInvoice, grdInvoice.CurrentRow.Index, 0, 1)
        '    '    dSelectWeight = dSelectWeight + Val(fnGetdgvText(grdInvoice, grdInvoice.CurrentRow.Index, 5))
        'Else
        '    Call fnSetdgvText(grdInvoice, grdInvoice.CurrentRow.Index, 0, 1)
        '    '   dSelectWeight = dSelectWeight - Val(fnGetdgvText(grdInvoice, grdInvoice.CurrentRow.Index, 5))
        'End If
        'lblSelweight.Text = Val(dSelectWeight)

        'For i = 0 To grdInvoice.RowCount - 1
        '    If (fnGetdgvText(grdInvoice, i, 0)) = 1 Then
        '        dSelectWeight = dSelectWeight + Val(fnGetdgvText(grdInvoice, i, 5))
        '    End If
        'Next
        'lblSelweight.Text = Val(dSelectWeight)
    End Sub

    Private Sub grdInvoice_Click(sender As Object, e As System.EventArgs) Handles grdInvoice.Click
        'Dim i As Long
        'dSelectWeight = 0
        'If Trim(fnGetdgvText(grdInvoice, grdInvoice.CurrentRow.Index, 0)) = False Then
        '    Call fnSetdgvText(grdInvoice, grdInvoice.CurrentRow.Index, 0, True)
        '    '    dSelectWeight = dSelectWeight + Val(fnGetdgvText(grdInvoice, grdInvoice.CurrentRow.Index, 5))
        'Else
        '    Call fnSetdgvText(grdInvoice, grdInvoice.CurrentRow.Index, 0, False)
        '    '   dSelectWeight = dSelectWeight - Val(fnGetdgvText(grdInvoice, grdInvoice.CurrentRow.Index, 5))
        'End If
        'lblSelweight.Text = Val(dSelectWeight)

        'For i = 0 To grdInvoice.RowCount - 1
        '    If Trim(fnGetdgvText(grdInvoice, i, 0)) = True Then
        '        dSelectWeight = dSelectWeight + Val(fnGetdgvText(grdInvoice, i, 5))
        '    End If
        'Next
        'lblSelweight.Text = Val(dSelectWeight)
    End Sub
    Private Function fncAddInvoices() As Boolean
        Dim iCnt As Long
        Dim bStatus As Boolean
        Dim sInvoiceNo, sInventoryCode, sWeight, sIssueWeight, sBalanceWeight, sInvoiceDate
        Dim dr As DataRow
        Try
            bStatus = False
            For iCnt = 0 To grdInvoice.RowCount - 1
                If Trim(fnGetdgvText(grdInvoice, iCnt, 0)) = True Then
                    sInvoiceNo = Trim(fnGetdgvText(grdInvoice, iCnt, 1))
                    sInvoiceDate = Trim(fnGetdgvText(grdInvoice, iCnt, 2))
                    sInventoryCode = Trim(fnGetdgvText(grdInvoice, iCnt, 3))
                    sWeight = Trim(fnGetdgvText(grdInvoice, iCnt, 4))
                    sIssueWeight = Trim(fnGetdgvText(grdInvoice, iCnt, 5))
                    sBalanceWeight = Trim(fnGetdgvText(grdInvoice, iCnt, 6))


                    dr = dsTradeIn.Tables(0).NewRow()
                    dr.Item("sInvoiceNo") = Trim(sInvoiceNo)
                    dr.Item("sInvoiceDate") = Trim(sInvoiceDate)
                    dr.Item("sInventoryCode") = Trim(sInventoryCode)
                    dr.Item("sWeight") = Trim(sWeight)
                    dr.Item("sIssueWeight") = Trim(sIssueWeight)
                    dr.Item("sBalanceWeight") = Trim(sBalanceWeight)
                    dsTradeIn.Tables(0).Rows.Add(dr)
                End If
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncAddInvoices = bStatus
        End Try
    End Function

    Private Sub subAddTradeInToGrid()
        Dim sInvoiceNo As String
        Dim sInventoryCode As String
        Dim sWeight As String
        Dim sIssueWeight As String
        Dim sBalanceWeight As String
        Dim sInvoiceDate As String
        Dim iRow As Long
        Dim i As Long

        Try
            dTradeInSales = 0
            dABal = 0
            dAIssueBal = 0
            For iRow = 0 To dsTradeIn.Tables(0).Rows.Count - 1
                grdGoldIssue.ForeColor = Color.Black

               
                If Not bEdited Then
                    If grdGoldIssue.RowCount = 0 Then
                        grdGoldIssue.Rows.Add()
                    Else
                        'grdSales.Row = grdSales.MaxRows
                        'grdSales.Action = 5
                        'grdSales.MaxRows = grdSales.MaxRows - 1
                        'grdJobSheet.MaxRows = grdJobSheet.MaxRows + 1
                        grdGoldIssue.Rows.Add()
                    End If
                    i = grdGoldIssue.RowCount - 1
                Else
                    i = grdGoldIssue.CurrentRow.Index
                End If
                sInvoiceNo = dsTradeIn.Tables(0).Rows(iRow).Item("sInvoiceNo") & "".Trim
                sInvoiceDate = dsTradeIn.Tables(0).Rows(iRow).Item("sInvoiceDate") & "".Trim
                sWeight = dsTradeIn.Tables(0).Rows(iRow).Item("sInventoryCode") & "".Trim
                'sIssueWeight = dsTradeIn.Tables(0).Rows(iRow).Item("sWeight") & "".Trim
                sIssueWeight = dsTradeIn.Tables(0).Rows(iRow).Item("sWeight") & "".Trim
                sBalanceWeight = sWeight - sIssueWeight
                ' sBalanceWeight = dsTradeIn.Tables(0).Rows(iRow).Item("sBalanceWeight") & "".Trim
                'sBalanceWeight = Val(sIssueWeight)
                ' sIssueWeight = Val(sIssueWeight) * Val(cboTealPercent.Text)
                dABal = dABal + Val(sIssueWeight)
                dAIssueBal = dAIssueBal + Val(sIssueWeight)
                'If Val(dsTIB.Tables(0).Rows(iCnt)("Balanceweight")) > 0 Then
                '    dPaidCost = dPaidCost + Val(dsTIB.Tables(0).Rows(iCnt)("Amount") & "") / Val(dsTIB.Tables(0).Rows(iCnt)("Balanceweight") & "")
                'Else
                '    dPaidCost = 0
                'End If

                Call fnSetdgvText(grdGoldIssue, i, 0, sInvoiceNo)
                Call fnSetdgvText(grdGoldIssue, i, 1, sInvoiceDate)
                Call fnSetdgvText(grdGoldIssue, i, 2, sInventoryCode)
                Call fnSetdgvText(grdGoldIssue, i, 3, Val(sWeight))
                Call fnSetdgvText(grdGoldIssue, i, 4, Val(sIssueWeight))
                Call fnSetdgvText(grdGoldIssue, i, 5, Val(sBalanceWeight))


                'subTotalFromGrid()
                'subTotal()
                'subClearDetail()

                bEdited = False
            Next iRow
            txtIssued.Text = Val(dABal)
            txtIssued.Text = CDbl(txtIssued.Text)
            If Trim(sCompanyShortCode) = "MJ" Then
                txtTotalWt.Text = Val(txtIssued.Text) '* CDbl(txtPerc.Text)
            Else
                txtTotalWt.Text = Format(Val(dAIssueBal), "0.00")
            End If
            'txtTotalWt.Text = Format(Val(dAIssueBal), "0.00")
            txtTotalWt.Text = Format(CDbl(txtTotalWt.Text), "0.00")
            ' txtTotalPrice.Text = Format((CDbl(txtTotalWt.Text) * CDbl(txtPricePerGram.Text)) + ((CDbl(txtTotalWt.Text) * CDbl(txtPricePerGram.Text)) * (Objuser.GstPercentage / 100)), "####00.00")
            If chkExport.Checked = False Then
                txtTotalPrice.Text = Format((CDbl(txtTotalWt.Text) * CDbl(txtPricePerGram.Text)) + ((CDbl(txtTotalWt.Text) * CDbl(txtPricePerGram.Text)) * (Objuser.GstPercentage / 100)), "####00.00")
            Else
                txtTotalPrice.Text = Format((CDbl(txtTotalWt.Text) * CDbl(txtPricePerGram.Text)), "####00.00")
            End If
            txtNoOfBills.Text = grdGoldIssue.RowCount - 1
            'txtNetIssue.Text = Format(Str((CDbl(txtIssued.Text) * 0.895) / 0.92), "#######.00")
            'lblTrade.Text = "TRADE"
            sClass = "TRADE"
            IbLoad = True
            txtTradeBillNo.Text = ""
            txtTradeBillNo.Focus()
            dsTradeIn.Tables(0).Rows.Clear()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subSetDataTable()
        Dim dt As New DataTable
        dt = New DataTable
        dt.Columns.Add(New DataColumn("sInvoiceNo", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("sInvoiceDate", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("sInventoryCode", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("sWeight", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("sIssueWeight", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("sBalanceWeight", Type.GetType("System.String")))
        dsTradeIn.Tables.Add(dt)
    End Sub

    
    Private Sub chkProduction_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkProduction.Click
        If chkProduction.Checked = True Then
            txtsupplierCode.Text = "NGT"
            txtsupplierCode.Enabled = False
            btnSupplierCodeSearch.Enabled = False
            txtsupplierCode_LostFocus(sender, e)
        Else
            txtsupplierCode.Text = ""
            txtsupplierCode.Enabled = True
            btnSupplierCodeSearch.Enabled = True
        End If

    End Sub

   
    Private Sub pnlInvoice_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlInvoice.Paint

    End Sub
End Class
