Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frmTradeInIssue
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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtSupplierName As System.Windows.Forms.TextBox
    Friend WithEvents btnSupplierCodeSearch As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnIssueNoSearch As System.Windows.Forms.Button
    Friend WithEvents txtCurrencyName As System.Windows.Forms.TextBox
    Friend WithEvents btnCurrencySearch As System.Windows.Forms.Button
    Friend WithEvents txtCurrencyRate As System.Windows.Forms.TextBox
    Friend WithEvents txtGoldIssueNo As System.Windows.Forms.TextBox
    Friend WithEvents grdGoldIssue As AxFPSpread.AxvaSpread
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnBillClear As System.Windows.Forms.Button
    Friend WithEvents txtPayable As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents grdUnpaidBill As AxFPSpread.AxvaSpread
    Friend WithEvents btnProceed As System.Windows.Forms.Button
    Friend WithEvents txtsupplierCode As System.Windows.Forms.TextBox
    Friend WithEvents txtDD As System.Windows.Forms.TextBox
    Friend WithEvents txtCurrencyCode As System.Windows.Forms.TextBox
    Friend WithEvents txtNetIssue As System.Windows.Forms.TextBox
    Friend WithEvents txtIssued As System.Windows.Forms.TextBox
    Friend WithEvents txtTradeIssueNo As System.Windows.Forms.TextBox
    Friend WithEvents txtYY As System.Windows.Forms.TextBox
    Friend WithEvents txtMM As System.Windows.Forms.TextBox
    Friend WithEvents btnTradeBillNoSearch As System.Windows.Forms.Button
    Friend WithEvents txtTradeBillNo As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtNoOfBills As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtTotalPrice As System.Windows.Forms.TextBox
    Friend WithEvents txtPricePerGram As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTradeInIssue))
        Me.Panel1 = New System.Windows.Forms.Panel()
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
        Me.txtCurrencyName = New System.Windows.Forms.TextBox()
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
        Me.txtNetIssue = New System.Windows.Forms.TextBox()
        Me.txtPayable = New System.Windows.Forms.TextBox()
        Me.grdGoldIssue = New AxFPSpread.AxvaSpread()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.grdUnpaidBill = New AxFPSpread.AxvaSpread()
        Me.btnProceed = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnBillClear = New System.Windows.Forms.Button()
        Me.btnTradeBillNoSearch = New System.Windows.Forms.Button()
        Me.txtTradeBillNo = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        CType(Me.grdGoldIssue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdUnpaidBill, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
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
        Me.Panel1.Controls.Add(Me.txtCurrencyName)
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
        Me.Panel1.Location = New System.Drawing.Point(20, 80)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(612, 136)
        Me.Panel1.TabIndex = 2
        '
        'txtTotalPrice
        '
        Me.txtTotalPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalPrice.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalPrice.ForeColor = System.Drawing.Color.Black
        Me.txtTotalPrice.Location = New System.Drawing.Point(512, 104)
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
        Me.Label11.Location = New System.Drawing.Point(520, 64)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(64, 32)
        Me.Label11.TabIndex = 136
        Me.Label11.Text = "Total  Price"
        '
        'txtPricePerGram
        '
        Me.txtPricePerGram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPricePerGram.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPricePerGram.ForeColor = System.Drawing.Color.Black
        Me.txtPricePerGram.Location = New System.Drawing.Point(432, 104)
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
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(360, 104)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(64, 24)
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
        '
        'txtIssued
        '
        Me.txtIssued.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIssued.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIssued.ForeColor = System.Drawing.Color.BlueViolet
        Me.txtIssued.Location = New System.Drawing.Point(104, 104)
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
        Me.txtTradeIssueNo.Location = New System.Drawing.Point(104, 16)
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
        'txtCurrencyName
        '
        Me.txtCurrencyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrencyName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencyName.ForeColor = System.Drawing.Color.Black
        Me.txtCurrencyName.Location = New System.Drawing.Point(232, 70)
        Me.txtCurrencyName.MaxLength = 20
        Me.txtCurrencyName.Name = "txtCurrencyName"
        Me.txtCurrencyName.ReadOnly = True
        Me.txtCurrencyName.Size = New System.Drawing.Size(192, 20)
        Me.txtCurrencyName.TabIndex = 10
        Me.txtCurrencyName.Tag = ""
        '
        'btnCurrencySearch
        '
        Me.btnCurrencySearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCurrencySearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCurrencySearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCurrencySearch.Location = New System.Drawing.Point(200, 70)
        Me.btnCurrencySearch.Name = "btnCurrencySearch"
        Me.btnCurrencySearch.Size = New System.Drawing.Size(24, 20)
        Me.btnCurrencySearch.TabIndex = 700
        Me.btnCurrencySearch.Text = "..."
        Me.btnCurrencySearch.UseVisualStyleBackColor = False
        '
        'txtCurrencyCode
        '
        Me.txtCurrencyCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrencyCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencyCode.ForeColor = System.Drawing.Color.Black
        Me.txtCurrencyCode.Location = New System.Drawing.Point(104, 70)
        Me.txtCurrencyCode.MaxLength = 20
        Me.txtCurrencyCode.Name = "txtCurrencyCode"
        Me.txtCurrencyCode.Size = New System.Drawing.Size(96, 20)
        Me.txtCurrencyCode.TabIndex = 8
        Me.txtCurrencyCode.Tag = ""
        '
        'btnIssueNoSearch
        '
        Me.btnIssueNoSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
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
        Me.btnSupplierCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
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
        Me.txtsupplierCode.Location = New System.Drawing.Point(104, 43)
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
        Me.Label5.Text = "Supplier Code"
        '
        'txtGoldIssueNo
        '
        Me.txtGoldIssueNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldIssueNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoldIssueNo.ForeColor = System.Drawing.Color.Black
        Me.txtGoldIssueNo.Location = New System.Drawing.Point(104, 16)
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
        'txtNetIssue
        '
        Me.txtNetIssue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNetIssue.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetIssue.ForeColor = System.Drawing.Color.Black
        Me.txtNetIssue.Location = New System.Drawing.Point(536, 8)
        Me.txtNetIssue.MaxLength = 20
        Me.txtNetIssue.Name = "txtNetIssue"
        Me.txtNetIssue.Size = New System.Drawing.Size(72, 20)
        Me.txtNetIssue.TabIndex = 14
        Me.txtNetIssue.Tag = ""
        Me.txtNetIssue.Visible = False
        '
        'txtPayable
        '
        Me.txtPayable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPayable.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayable.ForeColor = System.Drawing.Color.Black
        Me.txtPayable.Location = New System.Drawing.Point(536, 48)
        Me.txtPayable.MaxLength = 20
        Me.txtPayable.Name = "txtPayable"
        Me.txtPayable.Size = New System.Drawing.Size(72, 20)
        Me.txtPayable.TabIndex = 12
        Me.txtPayable.Tag = ""
        Me.txtPayable.Visible = False
        '
        'grdGoldIssue
        '
        Me.grdGoldIssue.Location = New System.Drawing.Point(16, 256)
        Me.grdGoldIssue.Name = "grdGoldIssue"
        Me.grdGoldIssue.OcxState = CType(resources.GetObject("grdGoldIssue.OcxState"), System.Windows.Forms.AxHost.State)
        Me.grdGoldIssue.Size = New System.Drawing.Size(608, 192)
        Me.grdGoldIssue.TabIndex = 18
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(16, 239)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 15)
        Me.Label3.TabIndex = 114
        Me.Label3.Text = "Trade In Gold"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(32, 496)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 15)
        Me.Label4.TabIndex = 115
        Me.Label4.Text = "Unpaid Bill List"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label4.Visible = False
        '
        'grdUnpaidBill
        '
        Me.grdUnpaidBill.Location = New System.Drawing.Point(24, 488)
        Me.grdUnpaidBill.Name = "grdUnpaidBill"
        Me.grdUnpaidBill.OcxState = CType(resources.GetObject("grdUnpaidBill.OcxState"), System.Windows.Forms.AxHost.State)
        Me.grdUnpaidBill.Size = New System.Drawing.Size(608, 168)
        Me.grdUnpaidBill.TabIndex = 16
        Me.grdUnpaidBill.Visible = False
        '
        'btnProceed
        '
        Me.btnProceed.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnProceed.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnProceed.Image = CType(resources.GetObject("btnProceed.Image"), System.Drawing.Image)
        Me.btnProceed.Location = New System.Drawing.Point(440, 456)
        Me.btnProceed.Name = "btnProceed"
        Me.btnProceed.Size = New System.Drawing.Size(92, 23)
        Me.btnProceed.TabIndex = 19
        Me.btnProceed.Text = "Proceed (F7)"
        Me.btnProceed.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(536, 456)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(92, 23)
        Me.btnClose.TabIndex = 22
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnBillClear
        '
        Me.btnBillClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnBillClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBillClear.Image = CType(resources.GetObject("btnBillClear.Image"), System.Drawing.Image)
        Me.btnBillClear.Location = New System.Drawing.Point(344, 456)
        Me.btnBillClear.Name = "btnBillClear"
        Me.btnBillClear.Size = New System.Drawing.Size(92, 23)
        Me.btnBillClear.TabIndex = 20
        Me.btnBillClear.Text = "Clear (F6)"
        Me.btnBillClear.UseVisualStyleBackColor = False
        '
        'btnTradeBillNoSearch
        '
        Me.btnTradeBillNoSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnTradeBillNoSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTradeBillNoSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTradeBillNoSearch.Location = New System.Drawing.Point(360, 232)
        Me.btnTradeBillNoSearch.Name = "btnTradeBillNoSearch"
        Me.btnTradeBillNoSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnTradeBillNoSearch.TabIndex = 17
        Me.btnTradeBillNoSearch.Text = "..."
        Me.btnTradeBillNoSearch.UseVisualStyleBackColor = False
        '
        'txtTradeBillNo
        '
        Me.txtTradeBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTradeBillNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTradeBillNo.ForeColor = System.Drawing.Color.Black
        Me.txtTradeBillNo.Location = New System.Drawing.Point(264, 232)
        Me.txtTradeBillNo.MaxLength = 20
        Me.txtTradeBillNo.Name = "txtTradeBillNo"
        Me.txtTradeBillNo.Size = New System.Drawing.Size(96, 20)
        Me.txtTradeBillNo.TabIndex = 16
        Me.txtTradeBillNo.Tag = ""
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(160, 240)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(64, 15)
        Me.Label16.TabIndex = 118
        Me.Label16.Text = "Trade Bill No"
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.Location = New System.Drawing.Point(248, 456)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(92, 23)
        Me.btnPrint.TabIndex = 21
        Me.btnPrint.Text = "Print (F9)"
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'frmTradeInIssue
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(650, 487)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.btnTradeBillNoSearch)
        Me.Controls.Add(Me.txtTradeBillNo)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtNetIssue)
        Me.Controls.Add(Me.txtPayable)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.btnProceed)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnBillClear)
        Me.Controls.Add(Me.grdUnpaidBill)
        Me.Controls.Add(Me.grdGoldIssue)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmTradeInIssue"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Trade In Issue"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.grdGoldIssue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdUnpaidBill, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Dim bProceed As Boolean
    Dim dABal As Double
    Dim dPaidCost As Double
    Dim sClass As String
    Dim sInvoiceDate As String
    Dim sTDate As String
    Dim TotPurchaseWeight As Double

    Dim objTII As New ClsTradeInIssue
    Dim objSupp As New clsSuppliers
    Dim objCurr As New clsCurrency
    Dim objPur As New clsPurchase
    Dim objGBI As ClsGoldBarIssue
    Dim objTIS As New ClsTradeinSales
    Public oTSalesConn As New ADODB.Connection
    Public oTSalesRs As New ADODB.Recordset


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
        grdGoldIssue.MaxRows = 0
        grdUnpaidBill.MaxRows = 0
        subGridSetting(grdGoldIssue)
        subGridSetting(grdUnpaidBill)
        txtIssued.Text = "0.00"
        txtNoOfBills.Text = "0"
        txtPricePerGram.Text = "0.00"
        txtTotalPrice.Text = "0.00"
    End Sub

    Private Sub btnBillClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBillClear.Click
        subClearMe()
    End Sub

    Private Sub btnSupplierCodeSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSupplierCodeSearch.Click
        Try
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSupplier", "L", 4, "SupplierName", "", "SupplierCode", "SupplierCode", 80, "SupplierName", "SupplierName", 180, "Address1", "Address1", 60, "Address2", "Address2", 80)
            frmHelpClass.ShowDialog()
            txtsupplierCode.Text = frmHelpClass.Var1.Trim
            txtsupplierCode_LostFocus(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subClearMe()
        Try
            txtsupplierCode.Clear()
            txtSupplierName.Clear()
            txtCurrencyCode.Clear()
            txtCurrencyName.Clear()
            txtCurrencyRate.Clear()
            txtTradeBillNo.Clear()
            txtPayable.Clear()
            txtGoldIssueNo.Clear()
            txtIssued.Text = "0.00"
            txtNetIssue.Text = "0.00"
            grdUnpaidBill.MaxRows = 0
            grdGoldIssue.MaxRows = 0
            TotPurchaseWeight = 0
            txtPricePerGram.Text = "0.00"
            txtTotalPrice.Text = "0.00"
            txtNoOfBills.Text = "0"
            txtTradeIssueNo.Text = ""
            txtTradeBillNo.Enabled = True
            btnTradeBillNoSearch.Enabled = True
            btnProceed.Enabled = True
            txtTradeIssueNo.Text = objTII.GetNextTradeIssueNo()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub btnProceed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProceed.Click
        Try
            Dim TempPurchaseWeight As Double
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


            For i = 1 To grdGoldIssue.MaxRows
                dsTH = objTII.GetTradeInHeader(GetText(grdGoldIssue, i, 1))
                If dsTH.Tables(0).Rows.Count > 0 Then
                    If TotPurchaseWeight >= Val(dsTH.Tables(0).Rows(0)("Balanceweight")) Then
                        TotPurchaseWeight = TotPurchaseWeight - Val(dsTH.Tables(0).Rows(0)("Balanceweight"))
                        TempPurchaseWeight = Val(dsTH.Tables(0).Rows(0)("Balanceweight"))
                    Else
                        TempPurchaseWeight = TotPurchaseWeight
                        TotPurchaseWeight = 0
                        bProceed = True
                    End If


                    '    iRet = objTII.UpdateTradeInHeader(oTSalesConn, oTSalesRs, Trim(txtTradeIssueNo.Text), TempPurchaseWeight, Trim(dsTH.Tables(0).Rows(0)("InvoiceNo")), txtsupplierCode.Text.Trim, 0, sError)
                    If Trim(sError) <> "" Then
                        MessageBox.Show("Error in save")
                        Exit Sub
                    End If
                    dsTH = Nothing
                End If
            Next
            'iRet = objTII.UpdateTradeInIssueHistoryHeader(oTSalesConn, oTSalesRs, Trim(txtTradeIssueNo.Text), SIssueDate, txtsupplierCode.Text.Trim, _
            '                                              Val(txtNoOfBills.Text.Trim), Val(txtIssued.Text.Trim), Val(txtPricePerGram.Text.Trim), _
            '                                              Val(txtTotalPrice.Text.Trim), sUserID, sError)
            If Trim(sError) <> "" Then
                MessageBox.Show("Error in save")
                Exit Sub
            End If
            MessageBox.Show("Tradein Gold Issued Sucessfully")
            subPrint(txtTradeIssueNo.Text.Trim)
            objTII.UpdateTradeIssueNo()
            subClearMe()
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

            If grdGoldIssue.MaxRows = 1 Then
                If Len(Trim(GetText(grdGoldIssue, 1, 1))) = 0 Then
                    MessageBox.Show("Pls check the GoldBar Inventory!")
                    Exit Function
                End If
            End If
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncValidation = bStatus
        End Try
    End Function

    Private Sub subGetPurchaseBill()
        Try
            Dim icnt As Long
            Dim sDate As Date
            Dim dsPur As New DataSet
            'Dim SBll As Purchase
            'SBll = New Purchase
            dsPur = objPur.GetPurchaseBill(Trim(txtsupplierCode.Text))
            If dsPur.Tables(0).Rows.Count > 0 Then
                grdUnpaidBill.MaxRows = 0
                txtPayable.Text = ""
                For icnt = 0 To dsPur.Tables(0).Rows.Count - 1
                    grdUnpaidBill.MaxRows = grdUnpaidBill.MaxRows + 1
                    grdUnpaidBill.Row = grdUnpaidBill.MaxRows
                    sDate = CDate(Trim(dsPur.Tables(0).Rows(icnt)("InvoiceDate")))
                    Call SetText(grdUnpaidBill, grdUnpaidBill.Row, 1, Trim(dsPur.Tables(0).Rows(icnt)("InvoiceNo") & ""))
                    Call SetText(grdUnpaidBill, grdUnpaidBill.Row, 2, Trim(sDate & ""))
                    Call SetText(grdUnpaidBill, grdUnpaidBill.Row, 3, Val(dsPur.Tables(0).Rows(icnt)("PurchaseWeight") & ""))
                    Call SetText(grdUnpaidBill, grdUnpaidBill.Row, 4, Val(dsPur.Tables(0).Rows(icnt)("IssuedWeight") & ""))
                    Call SetText(grdUnpaidBill, grdUnpaidBill.Row, 5, Val(dsPur.Tables(0).Rows(icnt)("Balanceweight") & ""))
                    txtPayable.Text = Format((Val(txtPayable.Text) + GetText(grdUnpaidBill, grdUnpaidBill.Row, 5)), "0.00")
                Next icnt
                grdUnpaidBill.Row = 1
                grdUnpaidBill.Col = 1
                grdUnpaidBill.Action = 0
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
            Dim frmHelpBill As New frmHelpSearch(0, 0, "tblTradeInHeader", "L", 4, "InvoiceNo", " ((IssueFlag is Null) or (IssueFlag<> 'Y'))", "InvoiceNo", "InvoiceNo", 80, "InventoryCode", "InventoryCode", 80, "TotalWeight", "TotalWeight", 80, "CustomerCode", "CustomerCode", 60)
            frmHelpBill.ShowDialog()
            txtTradeBillNo.Text = frmHelpBill.Var1.Trim
            If txtTradeBillNo.Text.Trim <> "" Then
                subGetTradeBillNo()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subGetTradeBillNo()
        Try
            Dim dsTIB As New DataSet
            Dim iCnt As Long

            dsTIB = objTIS.GetTradeInBalance(Trim(txtTradeBillNo.Text))
            If grdGoldIssue.MaxRows = 0 Then
                grdGoldIssue.MaxRows = 1
            Else
                grdGoldIssue.MaxRows = grdGoldIssue.MaxRows + 1
            End If

            For iCnt = 1 To grdGoldIssue.MaxRows
                If Trim(GetText(grdGoldIssue, iCnt, 1)) = Trim(txtTradeBillNo.Text) Then
                    MessageBox.Show("Item Already Scanned")
                    grdGoldIssue.MaxRows = grdGoldIssue.MaxRows - 1
                    txtTradeBillNo.Text = ""
                    txtTradeBillNo.Focus()
                    Exit Sub
                End If
            Next


            If dsTIB.Tables(0).Rows.Count > 0 Then
                'txtInvoiceNo = Trim(adoRs!InvoiceNo & "")
                sInvoiceDate = Trim(dsTIB.Tables(0).Rows(0)("ModifyDate") & "")
                sTDate = Trim(dsTIB.Tables(0).Rows(0)("ModifyDate") & "")
                sTDate = DateValue(sTDate)
                Call SetText(grdGoldIssue, grdGoldIssue.MaxRows, 1, Trim(dsTIB.Tables(0).Rows(0)("InvoiceNo") & ""))
                Call SetText(grdGoldIssue, grdGoldIssue.MaxRows, 2, sTDate)
                Call SetText(grdGoldIssue, grdGoldIssue.MaxRows, 3, "TRADE-IN")
                Call SetText(grdGoldIssue, grdGoldIssue.MaxRows, 4, Val(dsTIB.Tables(0).Rows(0)("TotalWeight") & ""))
                Call SetText(grdGoldIssue, grdGoldIssue.MaxRows, 5, Val(dsTIB.Tables(0).Rows(0)("IssuedWeight") & ""))
                Call SetText(grdGoldIssue, grdGoldIssue.MaxRows, 6, Val(dsTIB.Tables(0).Rows(0)("Balanceweight") & ""))
                dABal = Val(dsTIB.Tables(0).Rows(0)("TotalWeight") & "")
                If Val(dsTIB.Tables(0).Rows(0)("Balanceweight")) > 0 Then
                    dPaidCost = Val(dsTIB.Tables(0).Rows(0)("Amount") & "") / Val(dsTIB.Tables(0).Rows(0)("Balanceweight") & "")
                Else
                    dPaidCost = 0
                End If
                grdGoldIssue.Row = 1
                grdGoldIssue.Col = 1
                grdGoldIssue.Action = 0
            Else
                grdGoldIssue.MaxRows = 0
                Exit Sub
            End If

            txtIssued.Text = Val(dABal) + Val(txtIssued.Text)
            txtIssued.Text = CDbl(txtIssued.Text)
            txtTotalPrice.Text = Format((CDbl(txtIssued.Text) * CDbl(txtPricePerGram.Text)) + ((CDbl(txtIssued.Text) * CDbl(txtPricePerGram.Text)) * (Objuser.GstPercentage / 100)), "####00.00")
            txtNoOfBills.Text = grdGoldIssue.MaxRows
            'txtNetIssue.Text = Format(Str((CDbl(txtIssued.Text) * 0.895) / 0.92), "#######.00")
            'lblTrade.Text = "TRADE"
            sClass = "TRADE"
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
            sReportTitle = "Trade In Issue Report For " & sTradeInIssueNo
            If UCase(sCompanyShortCode) = "HN" Then
                strpath = Application.StartupPath & "\Reports\rptTradeInGoldIssue_HN.rpt"
            Else
                strpath = Application.StartupPath & "\Reports\rptTradeInGoldIssue.rpt"
            End If
            CR.Load(strpath)

            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(sCompanyname)
            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(sAddress)
            CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText(sReportTitle)
            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(sPhone)
            CR.DataDefinition.FormulaFields("CompAddress4").Text = fncFormatSQLText(sFax)
            ReportLogOnToServer(CR)
            CR.Refresh()
            frmrep.ReportViewer.ReportSource = CR
            sSelection = "{tblTradeIssueHistory.TradeIssueNo} = " & fncFormatSQLText(Trim(sTradeInIssueNo))
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
                    grdGoldIssue.MaxRows = 0
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
            txtTotalPrice.Text = Format((CDbl(txtIssued.Text) * CDbl(txtPricePerGram.Text)) + ((CDbl(txtIssued.Text) * CDbl(txtPricePerGram.Text)) * (Objuser.GstPercentage / 100)), "####00.00")
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
End Class
