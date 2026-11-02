Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frmConsignmentSales
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
    Friend WithEvents lblCustomerType As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents optInclusive As System.Windows.Forms.RadioButton
    Friend WithEvents optZeroRated As System.Windows.Forms.RadioButton
    Friend WithEvents optExclusive As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtSalesManName As System.Windows.Forms.TextBox
    Friend WithEvents txtSalesManPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnCustomerCodeSearch As System.Windows.Forms.Button
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress3 As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress2 As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress1 As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerCode As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtIYYYY As System.Windows.Forms.TextBox
    Friend WithEvents txtIMM As System.Windows.Forms.TextBox
    Friend WithEvents txtIDD As System.Windows.Forms.TextBox
    Friend WithEvents lblCompanyAddress As System.Windows.Forms.Label
    Friend WithEvents lblCompanyName As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents txtGST As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtTotalValue As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtPCS As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtSMC As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtCostPerGram As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtGoldLoss As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtInventoryCode As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtInventoryDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents lblReturnAmount As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents lblLastBillAmount As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents lblLastBillNo As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents lblLastSalesMan As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents lblTerminal As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents lblLocation As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtStoneWeight As System.Windows.Forms.TextBox
    Friend WithEvents grdSales As AxFPSpread.AxvaSpread
    Friend WithEvents grdMarketRate As AxFPSpread.AxvaSpread
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents lblTax As System.Windows.Forms.Label
    Friend WithEvents lblNetValue As System.Windows.Forms.Label
    Friend WithEvents lblNetDiscount As System.Windows.Forms.Label
    Friend WithEvents lblSalesAmount As System.Windows.Forms.Label
    Friend WithEvents lblQty As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblNetTotal As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents lblSalesType As System.Windows.Forms.Label
    Friend WithEvents PnlNetAmount As System.Windows.Forms.Panel
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents txtNetAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtNetDiscountPercentage As System.Windows.Forms.TextBox
    Friend WithEvents txtNetDiscount As System.Windows.Forms.TextBox
    Friend WithEvents PnlReprint As System.Windows.Forms.Panel
    Friend WithEvents btnReprintCancel As System.Windows.Forms.Button
    Friend WithEvents btnReprintOK As System.Windows.Forms.Button
    Friend WithEvents txtRePrintBillNo As System.Windows.Forms.TextBox
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents pnlAdminPwd As System.Windows.Forms.Panel
    Friend WithEvents btnAdminPwdCancel As System.Windows.Forms.Button
    Friend WithEvents btnAdminPwdOK As System.Windows.Forms.Button
    Friend WithEvents txtAdminPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents Label52 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmConsignmentSales))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lblCustomerType = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.optInclusive = New System.Windows.Forms.RadioButton
        Me.optZeroRated = New System.Windows.Forms.RadioButton
        Me.optExclusive = New System.Windows.Forms.RadioButton
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtSalesManName = New System.Windows.Forms.TextBox
        Me.txtSalesManPassword = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnCustomerCodeSearch = New System.Windows.Forms.Button
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtRemarks = New System.Windows.Forms.TextBox
        Me.txtAddress3 = New System.Windows.Forms.TextBox
        Me.txtAddress2 = New System.Windows.Forms.TextBox
        Me.txtAddress1 = New System.Windows.Forms.TextBox
        Me.txtCustomerName = New System.Windows.Forms.TextBox
        Me.txtCustomerCode = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtIYYYY = New System.Windows.Forms.TextBox
        Me.txtIMM = New System.Windows.Forms.TextBox
        Me.txtIDD = New System.Windows.Forms.TextBox
        Me.lblCompanyAddress = New System.Windows.Forms.Label
        Me.lblCompanyName = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.txtGST = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtTotalValue = New System.Windows.Forms.TextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.txtPCS = New System.Windows.Forms.TextBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.txtSMC = New System.Windows.Forms.TextBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.txtCostPerGram = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.txtGoldLoss = New System.Windows.Forms.TextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.txtWeight = New System.Windows.Forms.TextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.txtInventoryCode = New System.Windows.Forms.TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.txtInventoryDescription = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.lblReturnAmount = New System.Windows.Forms.Label
        Me.Label40 = New System.Windows.Forms.Label
        Me.Label37 = New System.Windows.Forms.Label
        Me.Label38 = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.Label35 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.lblLastBillAmount = New System.Windows.Forms.Label
        Me.Label36 = New System.Windows.Forms.Label
        Me.lblLastBillNo = New System.Windows.Forms.Label
        Me.Label34 = New System.Windows.Forms.Label
        Me.lblLastSalesMan = New System.Windows.Forms.Label
        Me.Label31 = New System.Windows.Forms.Label
        Me.lblTerminal = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.lblLocation = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.txtStoneWeight = New System.Windows.Forms.TextBox
        Me.grdSales = New AxFPSpread.AxvaSpread
        Me.grdMarketRate = New AxFPSpread.AxvaSpread
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Label32 = New System.Windows.Forms.Label
        Me.lblTax = New System.Windows.Forms.Label
        Me.lblNetValue = New System.Windows.Forms.Label
        Me.lblNetDiscount = New System.Windows.Forms.Label
        Me.lblSalesAmount = New System.Windows.Forms.Label
        Me.lblQty = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.lblNetTotal = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label65 = New System.Windows.Forms.Label
        Me.Label66 = New System.Windows.Forms.Label
        Me.Label67 = New System.Windows.Forms.Label
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.Label45 = New System.Windows.Forms.Label
        Me.Label44 = New System.Windows.Forms.Label
        Me.Label43 = New System.Windows.Forms.Label
        Me.Label42 = New System.Windows.Forms.Label
        Me.Label41 = New System.Windows.Forms.Label
        Me.Label39 = New System.Windows.Forms.Label
        Me.lblSalesType = New System.Windows.Forms.Label
        Me.PnlNetAmount = New System.Windows.Forms.Panel
        Me.Label46 = New System.Windows.Forms.Label
        Me.txtNetAmount = New System.Windows.Forms.TextBox
        Me.txtNetDiscountPercentage = New System.Windows.Forms.TextBox
        Me.txtNetDiscount = New System.Windows.Forms.TextBox
        Me.PnlReprint = New System.Windows.Forms.Panel
        Me.btnReprintCancel = New System.Windows.Forms.Button
        Me.btnReprintOK = New System.Windows.Forms.Button
        Me.txtRePrintBillNo = New System.Windows.Forms.TextBox
        Me.Label47 = New System.Windows.Forms.Label
        Me.Label48 = New System.Windows.Forms.Label
        Me.pnlAdminPwd = New System.Windows.Forms.Panel
        Me.btnAdminPwdCancel = New System.Windows.Forms.Button
        Me.btnAdminPwdOK = New System.Windows.Forms.Button
        Me.txtAdminPassword = New System.Windows.Forms.TextBox
        Me.Label51 = New System.Windows.Forms.Label
        Me.Label52 = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.grdSales, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdMarketRate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.PnlNetAmount.SuspendLayout()
        Me.PnlReprint.SuspendLayout()
        Me.pnlAdminPwd.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.lblCustomerType)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtSalesManName)
        Me.Panel1.Controls.Add(Me.txtSalesManPassword)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.btnCustomerCodeSearch)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtRemarks)
        Me.Panel1.Controls.Add(Me.txtAddress3)
        Me.Panel1.Controls.Add(Me.txtAddress2)
        Me.Panel1.Controls.Add(Me.txtAddress1)
        Me.Panel1.Controls.Add(Me.txtCustomerName)
        Me.Panel1.Controls.Add(Me.txtCustomerCode)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txtIYYYY)
        Me.Panel1.Controls.Add(Me.txtIMM)
        Me.Panel1.Controls.Add(Me.txtIDD)
        Me.Panel1.Location = New System.Drawing.Point(304, 64)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(536, 184)
        Me.Panel1.TabIndex = 1
        '
        'lblCustomerType
        '
        Me.lblCustomerType.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.lblCustomerType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustomerType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerType.ForeColor = System.Drawing.Color.Red
        Me.lblCustomerType.Location = New System.Drawing.Point(392, 58)
        Me.lblCustomerType.Name = "lblCustomerType"
        Me.lblCustomerType.Size = New System.Drawing.Size(136, 20)
        Me.lblCustomerType.TabIndex = 115
        Me.lblCustomerType.Text = "Customer type"
        Me.lblCustomerType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.optInclusive)
        Me.Panel2.Controls.Add(Me.optZeroRated)
        Me.Panel2.Controls.Add(Me.optExclusive)
        Me.Panel2.Location = New System.Drawing.Point(232, 30)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(296, 24)
        Me.Panel2.TabIndex = 5
        '
        'optInclusive
        '
        Me.optInclusive.Checked = True
        Me.optInclusive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optInclusive.ForeColor = System.Drawing.Color.Black
        Me.optInclusive.Location = New System.Drawing.Point(114, 0)
        Me.optInclusive.Name = "optInclusive"
        Me.optInclusive.Size = New System.Drawing.Size(75, 20)
        Me.optInclusive.TabIndex = 13
        Me.optInclusive.TabStop = True
        Me.optInclusive.Text = "Inclusive"
        '
        'optZeroRated
        '
        Me.optZeroRated.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optZeroRated.ForeColor = System.Drawing.Color.Black
        Me.optZeroRated.Location = New System.Drawing.Point(11, 0)
        Me.optZeroRated.Name = "optZeroRated"
        Me.optZeroRated.Size = New System.Drawing.Size(88, 20)
        Me.optZeroRated.TabIndex = 5
        Me.optZeroRated.Text = "Zero Rated"
        '
        'optExclusive
        '
        Me.optExclusive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optExclusive.ForeColor = System.Drawing.Color.Black
        Me.optExclusive.Location = New System.Drawing.Point(204, 0)
        Me.optExclusive.Name = "optExclusive"
        Me.optExclusive.Size = New System.Drawing.Size(78, 20)
        Me.optExclusive.TabIndex = 3
        Me.optExclusive.Text = "Exclusive"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(8, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 20)
        Me.Label2.TabIndex = 64
        Me.Label2.Text = "Invoice Date"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSalesManName
        '
        Me.txtSalesManName.BackColor = System.Drawing.Color.White
        Me.txtSalesManName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesManName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesManName.ForeColor = System.Drawing.Color.Black
        Me.txtSalesManName.Location = New System.Drawing.Point(232, 6)
        Me.txtSalesManName.MaxLength = 20
        Me.txtSalesManName.Name = "txtSalesManName"
        Me.txtSalesManName.ReadOnly = True
        Me.txtSalesManName.Size = New System.Drawing.Size(296, 20)
        Me.txtSalesManName.TabIndex = 1
        Me.txtSalesManName.Tag = ""
        Me.txtSalesManName.Text = ""
        '
        'txtSalesManPassword
        '
        Me.txtSalesManPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesManPassword.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesManPassword.ForeColor = System.Drawing.Color.Black
        Me.txtSalesManPassword.Location = New System.Drawing.Point(104, 5)
        Me.txtSalesManPassword.MaxLength = 20
        Me.txtSalesManPassword.Name = "txtSalesManPassword"
        Me.txtSalesManPassword.PasswordChar = Microsoft.VisualBasic.ChrW(42)
        Me.txtSalesManPassword.Size = New System.Drawing.Size(120, 20)
        Me.txtSalesManPassword.TabIndex = 0
        Me.txtSalesManPassword.Tag = ""
        Me.txtSalesManPassword.Text = ""
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(8, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 20)
        Me.Label1.TabIndex = 112
        Me.Label1.Text = "Sales Man"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnCustomerCodeSearch
        '
        Me.btnCustomerCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnCustomerCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCustomerCodeSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomerCodeSearch.Location = New System.Drawing.Point(200, 58)
        Me.btnCustomerCodeSearch.Name = "btnCustomerCodeSearch"
        Me.btnCustomerCodeSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnCustomerCodeSearch.TabIndex = 7
        Me.btnCustomerCodeSearch.Text = "..."
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(8, 82)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(82, 20)
        Me.Label13.TabIndex = 109
        Me.Label13.Text = "Address"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(8, 155)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(82, 20)
        Me.Label5.TabIndex = 108
        Me.Label5.Text = "Remarks"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.Black
        Me.txtRemarks.Location = New System.Drawing.Point(104, 155)
        Me.txtRemarks.MaxLength = 20
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(424, 20)
        Me.txtRemarks.TabIndex = 12
        Me.txtRemarks.Tag = ""
        Me.txtRemarks.Text = ""
        '
        'txtAddress3
        '
        Me.txtAddress3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress3.ForeColor = System.Drawing.Color.Black
        Me.txtAddress3.Location = New System.Drawing.Point(104, 131)
        Me.txtAddress3.MaxLength = 20
        Me.txtAddress3.Name = "txtAddress3"
        Me.txtAddress3.Size = New System.Drawing.Size(424, 20)
        Me.txtAddress3.TabIndex = 11
        Me.txtAddress3.Tag = ""
        Me.txtAddress3.Text = ""
        '
        'txtAddress2
        '
        Me.txtAddress2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress2.ForeColor = System.Drawing.Color.Black
        Me.txtAddress2.Location = New System.Drawing.Point(104, 106)
        Me.txtAddress2.MaxLength = 20
        Me.txtAddress2.Name = "txtAddress2"
        Me.txtAddress2.Size = New System.Drawing.Size(424, 20)
        Me.txtAddress2.TabIndex = 10
        Me.txtAddress2.Tag = ""
        Me.txtAddress2.Text = ""
        '
        'txtAddress1
        '
        Me.txtAddress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress1.ForeColor = System.Drawing.Color.Black
        Me.txtAddress1.Location = New System.Drawing.Point(104, 82)
        Me.txtAddress1.MaxLength = 20
        Me.txtAddress1.Name = "txtAddress1"
        Me.txtAddress1.Size = New System.Drawing.Size(424, 20)
        Me.txtAddress1.TabIndex = 9
        Me.txtAddress1.Tag = ""
        Me.txtAddress1.Text = ""
        '
        'txtCustomerName
        '
        Me.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerName.ForeColor = System.Drawing.Color.Black
        Me.txtCustomerName.Location = New System.Drawing.Point(232, 58)
        Me.txtCustomerName.MaxLength = 20
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(152, 20)
        Me.txtCustomerName.TabIndex = 8
        Me.txtCustomerName.Tag = ""
        Me.txtCustomerName.Text = ""
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerCode.ForeColor = System.Drawing.Color.Black
        Me.txtCustomerCode.Location = New System.Drawing.Point(104, 58)
        Me.txtCustomerCode.MaxLength = 20
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.Size = New System.Drawing.Size(96, 20)
        Me.txtCustomerCode.TabIndex = 6
        Me.txtCustomerCode.Tag = ""
        Me.txtCustomerCode.Text = ""
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(8, 58)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 20)
        Me.Label6.TabIndex = 102
        Me.Label6.Text = "Customer "
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(158, 33)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(8, 16)
        Me.Label3.TabIndex = 62
        Me.Label3.Text = "/"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(126, 33)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(8, 16)
        Me.Label4.TabIndex = 61
        Me.Label4.Text = "/"
        '
        'txtIYYYY
        '
        Me.txtIYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIYYYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIYYYY.Location = New System.Drawing.Point(168, 32)
        Me.txtIYYYY.MaxLength = 4
        Me.txtIYYYY.Name = "txtIYYYY"
        Me.txtIYYYY.Size = New System.Drawing.Size(32, 20)
        Me.txtIYYYY.TabIndex = 4
        Me.txtIYYYY.Tag = ""
        Me.txtIYYYY.Text = ""
        '
        'txtIMM
        '
        Me.txtIMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIMM.Location = New System.Drawing.Point(136, 32)
        Me.txtIMM.MaxLength = 2
        Me.txtIMM.Name = "txtIMM"
        Me.txtIMM.Size = New System.Drawing.Size(20, 20)
        Me.txtIMM.TabIndex = 3
        Me.txtIMM.Tag = ""
        Me.txtIMM.Text = ""
        '
        'txtIDD
        '
        Me.txtIDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIDD.Location = New System.Drawing.Point(104, 32)
        Me.txtIDD.MaxLength = 2
        Me.txtIDD.Name = "txtIDD"
        Me.txtIDD.Size = New System.Drawing.Size(20, 20)
        Me.txtIDD.TabIndex = 2
        Me.txtIDD.Tag = ""
        Me.txtIDD.Text = ""
        '
        'lblCompanyAddress
        '
        Me.lblCompanyAddress.BackColor = System.Drawing.Color.Yellow
        Me.lblCompanyAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCompanyAddress.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompanyAddress.ForeColor = System.Drawing.Color.Navy
        Me.lblCompanyAddress.Location = New System.Drawing.Point(96, 24)
        Me.lblCompanyAddress.Name = "lblCompanyAddress"
        Me.lblCompanyAddress.Size = New System.Drawing.Size(920, 40)
        Me.lblCompanyAddress.TabIndex = 150
        Me.lblCompanyAddress.Text = "Name and Address"
        Me.lblCompanyAddress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCompanyName
        '
        Me.lblCompanyName.BackColor = System.Drawing.Color.Yellow
        Me.lblCompanyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCompanyName.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompanyName.ForeColor = System.Drawing.Color.Navy
        Me.lblCompanyName.Location = New System.Drawing.Point(96, 0)
        Me.lblCompanyName.Name = "lblCompanyName"
        Me.lblCompanyName.Size = New System.Drawing.Size(920, 24)
        Me.lblCompanyName.TabIndex = 149
        Me.lblCompanyName.Text = "Name and Address"
        Me.lblCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureBox1
        '
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(96, 653)
        Me.PictureBox1.TabIndex = 151
        Me.PictureBox1.TabStop = False
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.txtGST)
        Me.Panel3.Controls.Add(Me.Label7)
        Me.Panel3.Controls.Add(Me.txtTotalValue)
        Me.Panel3.Controls.Add(Me.Label30)
        Me.Panel3.Controls.Add(Me.txtPCS)
        Me.Panel3.Controls.Add(Me.Label27)
        Me.Panel3.Controls.Add(Me.txtSMC)
        Me.Panel3.Controls.Add(Me.Label28)
        Me.Panel3.Controls.Add(Me.txtCostPerGram)
        Me.Panel3.Controls.Add(Me.Label24)
        Me.Panel3.Controls.Add(Me.txtGoldLoss)
        Me.Panel3.Controls.Add(Me.Label25)
        Me.Panel3.Controls.Add(Me.txtWeight)
        Me.Panel3.Controls.Add(Me.Label26)
        Me.Panel3.Controls.Add(Me.txtInventoryCode)
        Me.Panel3.Controls.Add(Me.Label22)
        Me.Panel3.Controls.Add(Me.txtInventoryDescription)
        Me.Panel3.Controls.Add(Me.Label23)
        Me.Panel3.Location = New System.Drawing.Point(96, 552)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(920, 51)
        Me.Panel3.TabIndex = 154
        '
        'txtGST
        '
        Me.txtGST.BackColor = System.Drawing.Color.White
        Me.txtGST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGST.Enabled = False
        Me.txtGST.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGST.ForeColor = System.Drawing.Color.Black
        Me.txtGST.Location = New System.Drawing.Point(744, 27)
        Me.txtGST.MaxLength = 20
        Me.txtGST.Name = "txtGST"
        Me.txtGST.Size = New System.Drawing.Size(72, 22)
        Me.txtGST.TabIndex = 17
        Me.txtGST.Tag = ""
        Me.txtGST.Text = ""
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.White
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Navy
        Me.Label7.Location = New System.Drawing.Point(744, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 27)
        Me.Label7.TabIndex = 158
        Me.Label7.Text = "GST"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTotalValue
        '
        Me.txtTotalValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalValue.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalValue.ForeColor = System.Drawing.Color.Black
        Me.txtTotalValue.Location = New System.Drawing.Point(816, 27)
        Me.txtTotalValue.MaxLength = 20
        Me.txtTotalValue.Name = "txtTotalValue"
        Me.txtTotalValue.Size = New System.Drawing.Size(102, 22)
        Me.txtTotalValue.TabIndex = 18
        Me.txtTotalValue.Tag = ""
        Me.txtTotalValue.Text = ""
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.White
        Me.Label30.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.Color.Navy
        Me.Label30.Location = New System.Drawing.Point(816, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(102, 27)
        Me.Label30.TabIndex = 156
        Me.Label30.Text = "Total Value"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtPCS
        '
        Me.txtPCS.BackColor = System.Drawing.Color.White
        Me.txtPCS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPCS.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPCS.ForeColor = System.Drawing.Color.Black
        Me.txtPCS.Location = New System.Drawing.Point(616, 27)
        Me.txtPCS.MaxLength = 20
        Me.txtPCS.Name = "txtPCS"
        Me.txtPCS.ReadOnly = True
        Me.txtPCS.Size = New System.Drawing.Size(56, 22)
        Me.txtPCS.TabIndex = 15
        Me.txtPCS.Tag = ""
        Me.txtPCS.Text = ""
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.White
        Me.Label27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label27.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.Color.Navy
        Me.Label27.Location = New System.Drawing.Point(616, 0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(56, 27)
        Me.Label27.TabIndex = 155
        Me.Label27.Text = "PCS"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtSMC
        '
        Me.txtSMC.BackColor = System.Drawing.Color.White
        Me.txtSMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSMC.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMC.ForeColor = System.Drawing.Color.Black
        Me.txtSMC.Location = New System.Drawing.Point(672, 27)
        Me.txtSMC.MaxLength = 20
        Me.txtSMC.Name = "txtSMC"
        Me.txtSMC.ReadOnly = True
        Me.txtSMC.Size = New System.Drawing.Size(72, 22)
        Me.txtSMC.TabIndex = 16
        Me.txtSMC.Tag = ""
        Me.txtSMC.Text = ""
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.White
        Me.Label28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label28.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.Color.Navy
        Me.Label28.Location = New System.Drawing.Point(672, 0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(72, 27)
        Me.Label28.TabIndex = 154
        Me.Label28.Text = "SMC"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCostPerGram
        '
        Me.txtCostPerGram.BackColor = System.Drawing.Color.White
        Me.txtCostPerGram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCostPerGram.Enabled = False
        Me.txtCostPerGram.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostPerGram.ForeColor = System.Drawing.Color.Black
        Me.txtCostPerGram.Location = New System.Drawing.Point(544, 27)
        Me.txtCostPerGram.MaxLength = 20
        Me.txtCostPerGram.Name = "txtCostPerGram"
        Me.txtCostPerGram.Size = New System.Drawing.Size(72, 22)
        Me.txtCostPerGram.TabIndex = 14
        Me.txtCostPerGram.Tag = ""
        Me.txtCostPerGram.Text = ""
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.White
        Me.Label24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label24.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Navy
        Me.Label24.Location = New System.Drawing.Point(544, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(72, 27)
        Me.Label24.TabIndex = 146
        Me.Label24.Text = "Cost/Gram"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtGoldLoss
        '
        Me.txtGoldLoss.BackColor = System.Drawing.Color.White
        Me.txtGoldLoss.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldLoss.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoldLoss.ForeColor = System.Drawing.Color.Black
        Me.txtGoldLoss.Location = New System.Drawing.Point(472, 27)
        Me.txtGoldLoss.MaxLength = 20
        Me.txtGoldLoss.Name = "txtGoldLoss"
        Me.txtGoldLoss.ReadOnly = True
        Me.txtGoldLoss.Size = New System.Drawing.Size(72, 22)
        Me.txtGoldLoss.TabIndex = 13
        Me.txtGoldLoss.Tag = ""
        Me.txtGoldLoss.Text = ""
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.White
        Me.Label25.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label25.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.Navy
        Me.Label25.Location = New System.Drawing.Point(472, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(72, 27)
        Me.Label25.TabIndex = 145
        Me.Label25.Text = "Gold Loss"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtWeight
        '
        Me.txtWeight.BackColor = System.Drawing.Color.White
        Me.txtWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWeight.Enabled = False
        Me.txtWeight.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeight.ForeColor = System.Drawing.Color.Black
        Me.txtWeight.Location = New System.Drawing.Point(400, 27)
        Me.txtWeight.MaxLength = 20
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.Size = New System.Drawing.Size(72, 22)
        Me.txtWeight.TabIndex = 12
        Me.txtWeight.Tag = ""
        Me.txtWeight.Text = ""
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.White
        Me.Label26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label26.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.Navy
        Me.Label26.Location = New System.Drawing.Point(400, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(72, 27)
        Me.Label26.TabIndex = 144
        Me.Label26.Text = "Weight"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtInventoryCode
        '
        Me.txtInventoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInventoryCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInventoryCode.ForeColor = System.Drawing.Color.Black
        Me.txtInventoryCode.Location = New System.Drawing.Point(0, 27)
        Me.txtInventoryCode.MaxLength = 20
        Me.txtInventoryCode.Name = "txtInventoryCode"
        Me.txtInventoryCode.Size = New System.Drawing.Size(104, 22)
        Me.txtInventoryCode.TabIndex = 10
        Me.txtInventoryCode.Tag = ""
        Me.txtInventoryCode.Text = ""
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.White
        Me.Label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label22.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.Navy
        Me.Label22.Location = New System.Drawing.Point(0, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(104, 27)
        Me.Label22.TabIndex = 138
        Me.Label22.Text = "Inventory Code"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtInventoryDescription
        '
        Me.txtInventoryDescription.BackColor = System.Drawing.Color.White
        Me.txtInventoryDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInventoryDescription.Enabled = False
        Me.txtInventoryDescription.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInventoryDescription.ForeColor = System.Drawing.Color.Black
        Me.txtInventoryDescription.Location = New System.Drawing.Point(104, 27)
        Me.txtInventoryDescription.MaxLength = 20
        Me.txtInventoryDescription.Name = "txtInventoryDescription"
        Me.txtInventoryDescription.Size = New System.Drawing.Size(296, 22)
        Me.txtInventoryDescription.TabIndex = 11
        Me.txtInventoryDescription.Tag = ""
        Me.txtInventoryDescription.Text = ""
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.White
        Me.Label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label23.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.Navy
        Me.Label23.Location = New System.Drawing.Point(104, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(296, 27)
        Me.Label23.TabIndex = 136
        Me.Label23.Text = "Inventory Description"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel5
        '
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.lblReturnAmount)
        Me.Panel5.Controls.Add(Me.Label40)
        Me.Panel5.Controls.Add(Me.Label37)
        Me.Panel5.Controls.Add(Me.Label38)
        Me.Panel5.Controls.Add(Me.Label33)
        Me.Panel5.Controls.Add(Me.Label35)
        Me.Panel5.Controls.Add(Me.Label19)
        Me.Panel5.Controls.Add(Me.Label29)
        Me.Panel5.Controls.Add(Me.lblLastBillAmount)
        Me.Panel5.Controls.Add(Me.Label36)
        Me.Panel5.Controls.Add(Me.lblLastBillNo)
        Me.Panel5.Controls.Add(Me.Label34)
        Me.Panel5.Controls.Add(Me.lblLastSalesMan)
        Me.Panel5.Controls.Add(Me.Label31)
        Me.Panel5.Controls.Add(Me.lblTerminal)
        Me.Panel5.Controls.Add(Me.Label21)
        Me.Panel5.Controls.Add(Me.lblLocation)
        Me.Panel5.Controls.Add(Me.Label20)
        Me.Panel5.Controls.Add(Me.txtStoneWeight)
        Me.Panel5.Location = New System.Drawing.Point(96, 603)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(920, 50)
        Me.Panel5.TabIndex = 155
        '
        'lblReturnAmount
        '
        Me.lblReturnAmount.BackColor = System.Drawing.Color.Navy
        Me.lblReturnAmount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReturnAmount.ForeColor = System.Drawing.Color.White
        Me.lblReturnAmount.Location = New System.Drawing.Point(768, 24)
        Me.lblReturnAmount.Name = "lblReturnAmount"
        Me.lblReturnAmount.Size = New System.Drawing.Size(152, 24)
        Me.lblReturnAmount.TabIndex = 137
        Me.lblReturnAmount.Text = "0.00"
        Me.lblReturnAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.Yellow
        Me.Label40.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.ForeColor = System.Drawing.Color.Black
        Me.Label40.Location = New System.Drawing.Point(768, 0)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(152, 24)
        Me.Label40.TabIndex = 136
        Me.Label40.Text = "Return Amount"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.Navy
        Me.Label37.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.Color.White
        Me.Label37.Location = New System.Drawing.Point(672, 24)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(96, 24)
        Me.Label37.TabIndex = 135
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.Yellow
        Me.Label38.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.Color.Black
        Me.Label38.Location = New System.Drawing.Point(672, 0)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(96, 24)
        Me.Label38.TabIndex = 134
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.Navy
        Me.Label33.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.White
        Me.Label33.Location = New System.Drawing.Point(576, 24)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(96, 24)
        Me.Label33.TabIndex = 133
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.Yellow
        Me.Label35.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.Color.Black
        Me.Label35.Location = New System.Drawing.Point(576, 0)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(96, 24)
        Me.Label35.TabIndex = 132
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.Navy
        Me.Label19.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.White
        Me.Label19.Location = New System.Drawing.Point(480, 24)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(96, 24)
        Me.Label19.TabIndex = 131
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.Yellow
        Me.Label29.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.Color.Black
        Me.Label29.Location = New System.Drawing.Point(480, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(96, 24)
        Me.Label29.TabIndex = 130
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLastBillAmount
        '
        Me.lblLastBillAmount.BackColor = System.Drawing.Color.Navy
        Me.lblLastBillAmount.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastBillAmount.ForeColor = System.Drawing.Color.White
        Me.lblLastBillAmount.Location = New System.Drawing.Point(384, 24)
        Me.lblLastBillAmount.Name = "lblLastBillAmount"
        Me.lblLastBillAmount.Size = New System.Drawing.Size(96, 24)
        Me.lblLastBillAmount.TabIndex = 129
        Me.lblLastBillAmount.Text = "0"
        Me.lblLastBillAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.Yellow
        Me.Label36.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.Color.Black
        Me.Label36.Location = New System.Drawing.Point(384, 0)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(96, 24)
        Me.Label36.TabIndex = 128
        Me.Label36.Text = "Last Bill Amt"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLastBillNo
        '
        Me.lblLastBillNo.BackColor = System.Drawing.Color.Navy
        Me.lblLastBillNo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastBillNo.ForeColor = System.Drawing.Color.White
        Me.lblLastBillNo.Location = New System.Drawing.Point(288, 24)
        Me.lblLastBillNo.Name = "lblLastBillNo"
        Me.lblLastBillNo.Size = New System.Drawing.Size(96, 24)
        Me.lblLastBillNo.TabIndex = 127
        Me.lblLastBillNo.Text = "0"
        Me.lblLastBillNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.Yellow
        Me.Label34.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.Color.Black
        Me.Label34.Location = New System.Drawing.Point(288, 0)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(96, 24)
        Me.Label34.TabIndex = 126
        Me.Label34.Text = "Last Bill No"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLastSalesMan
        '
        Me.lblLastSalesMan.BackColor = System.Drawing.Color.Navy
        Me.lblLastSalesMan.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastSalesMan.ForeColor = System.Drawing.Color.White
        Me.lblLastSalesMan.Location = New System.Drawing.Point(192, 24)
        Me.lblLastSalesMan.Name = "lblLastSalesMan"
        Me.lblLastSalesMan.Size = New System.Drawing.Size(96, 24)
        Me.lblLastSalesMan.TabIndex = 125
        Me.lblLastSalesMan.Text = "0"
        Me.lblLastSalesMan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.Yellow
        Me.Label31.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.Color.Black
        Me.Label31.Location = New System.Drawing.Point(192, 0)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(96, 24)
        Me.Label31.TabIndex = 124
        Me.Label31.Text = "Last SalesMan"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTerminal
        '
        Me.lblTerminal.BackColor = System.Drawing.Color.Navy
        Me.lblTerminal.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTerminal.ForeColor = System.Drawing.Color.White
        Me.lblTerminal.Location = New System.Drawing.Point(96, 24)
        Me.lblTerminal.Name = "lblTerminal"
        Me.lblTerminal.Size = New System.Drawing.Size(96, 24)
        Me.lblTerminal.TabIndex = 123
        Me.lblTerminal.Text = "0"
        Me.lblTerminal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.Yellow
        Me.Label21.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Black
        Me.Label21.Location = New System.Drawing.Point(96, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(96, 24)
        Me.Label21.TabIndex = 122
        Me.Label21.Text = "Terminal"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLocation
        '
        Me.lblLocation.BackColor = System.Drawing.Color.Navy
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.ForeColor = System.Drawing.Color.White
        Me.lblLocation.Location = New System.Drawing.Point(0, 24)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(96, 24)
        Me.lblLocation.TabIndex = 121
        Me.lblLocation.Text = "HQ"
        Me.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.Yellow
        Me.Label20.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.Location = New System.Drawing.Point(0, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(96, 24)
        Me.Label20.TabIndex = 120
        Me.Label20.Text = "Location"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtStoneWeight
        '
        Me.txtStoneWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStoneWeight.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStoneWeight.ForeColor = System.Drawing.Color.Black
        Me.txtStoneWeight.Location = New System.Drawing.Point(488, 24)
        Me.txtStoneWeight.MaxLength = 20
        Me.txtStoneWeight.Name = "txtStoneWeight"
        Me.txtStoneWeight.Size = New System.Drawing.Size(72, 22)
        Me.txtStoneWeight.TabIndex = 142
        Me.txtStoneWeight.Tag = ""
        Me.txtStoneWeight.Text = ""
        Me.txtStoneWeight.Visible = False
        '
        'grdSales
        '
        Me.grdSales.Location = New System.Drawing.Point(96, 248)
        Me.grdSales.Name = "grdSales"
        Me.grdSales.OcxState = CType(resources.GetObject("grdSales.OcxState"), System.Windows.Forms.AxHost.State)
        Me.grdSales.Size = New System.Drawing.Size(744, 304)
        Me.grdSales.TabIndex = 156
        '
        'grdMarketRate
        '
        Me.grdMarketRate.Location = New System.Drawing.Point(96, 64)
        Me.grdMarketRate.Name = "grdMarketRate"
        Me.grdMarketRate.OcxState = CType(resources.GetObject("grdMarketRate.OcxState"), System.Windows.Forms.AxHost.State)
        Me.grdMarketRate.Size = New System.Drawing.Size(208, 184)
        Me.grdMarketRate.TabIndex = 157
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Label32)
        Me.Panel4.Controls.Add(Me.lblTax)
        Me.Panel4.Controls.Add(Me.lblNetValue)
        Me.Panel4.Controls.Add(Me.lblNetDiscount)
        Me.Panel4.Controls.Add(Me.lblSalesAmount)
        Me.Panel4.Controls.Add(Me.lblQty)
        Me.Panel4.Controls.Add(Me.Label18)
        Me.Panel4.Controls.Add(Me.Label17)
        Me.Panel4.Controls.Add(Me.Label16)
        Me.Panel4.Controls.Add(Me.Label15)
        Me.Panel4.Controls.Add(Me.Label14)
        Me.Panel4.Controls.Add(Me.Label12)
        Me.Panel4.Controls.Add(Me.Label11)
        Me.Panel4.Controls.Add(Me.Label9)
        Me.Panel4.Controls.Add(Me.Label8)
        Me.Panel4.Controls.Add(Me.lblNetTotal)
        Me.Panel4.Controls.Add(Me.Label10)
        Me.Panel4.Controls.Add(Me.Label65)
        Me.Panel4.Controls.Add(Me.Label66)
        Me.Panel4.Controls.Add(Me.Label67)
        Me.Panel4.Location = New System.Drawing.Point(840, 248)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(176, 304)
        Me.Panel4.TabIndex = 158
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.Navy
        Me.Label32.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label32.Font = New System.Drawing.Font("Arial", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.Color.White
        Me.Label32.Location = New System.Drawing.Point(0, 200)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(174, 56)
        Me.Label32.TabIndex = 92
        Me.Label32.Text = "Net Total"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTax
        '
        Me.lblTax.BackColor = System.Drawing.Color.Yellow
        Me.lblTax.Font = New System.Drawing.Font("Arial", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTax.ForeColor = System.Drawing.Color.Navy
        Me.lblTax.Location = New System.Drawing.Point(80, 165)
        Me.lblTax.Name = "lblTax"
        Me.lblTax.Size = New System.Drawing.Size(88, 25)
        Me.lblTax.TabIndex = 91
        Me.lblTax.Text = "0.00"
        Me.lblTax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNetValue
        '
        Me.lblNetValue.BackColor = System.Drawing.Color.Yellow
        Me.lblNetValue.Font = New System.Drawing.Font("Arial", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetValue.ForeColor = System.Drawing.Color.Navy
        Me.lblNetValue.Location = New System.Drawing.Point(80, 127)
        Me.lblNetValue.Name = "lblNetValue"
        Me.lblNetValue.Size = New System.Drawing.Size(88, 25)
        Me.lblNetValue.TabIndex = 90
        Me.lblNetValue.Text = "0.00"
        Me.lblNetValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNetDiscount
        '
        Me.lblNetDiscount.BackColor = System.Drawing.Color.Yellow
        Me.lblNetDiscount.Font = New System.Drawing.Font("Arial", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetDiscount.ForeColor = System.Drawing.Color.Navy
        Me.lblNetDiscount.Location = New System.Drawing.Point(80, 88)
        Me.lblNetDiscount.Name = "lblNetDiscount"
        Me.lblNetDiscount.Size = New System.Drawing.Size(88, 25)
        Me.lblNetDiscount.TabIndex = 89
        Me.lblNetDiscount.Text = "0.00"
        Me.lblNetDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSalesAmount
        '
        Me.lblSalesAmount.BackColor = System.Drawing.Color.Yellow
        Me.lblSalesAmount.Font = New System.Drawing.Font("Arial", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesAmount.ForeColor = System.Drawing.Color.Navy
        Me.lblSalesAmount.Location = New System.Drawing.Point(80, 48)
        Me.lblSalesAmount.Name = "lblSalesAmount"
        Me.lblSalesAmount.Size = New System.Drawing.Size(88, 24)
        Me.lblSalesAmount.TabIndex = 88
        Me.lblSalesAmount.Text = "0.00"
        Me.lblSalesAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblQty
        '
        Me.lblQty.BackColor = System.Drawing.Color.Yellow
        Me.lblQty.Font = New System.Drawing.Font("Arial", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQty.ForeColor = System.Drawing.Color.Navy
        Me.lblQty.Location = New System.Drawing.Point(80, 8)
        Me.lblQty.Name = "lblQty"
        Me.lblQty.Size = New System.Drawing.Size(88, 25)
        Me.lblQty.TabIndex = 87
        Me.lblQty.Text = "0.00"
        Me.lblQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.Yellow
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Navy
        Me.Label18.Location = New System.Drawing.Point(2, 165)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(70, 25)
        Me.Label18.TabIndex = 86
        Me.Label18.Text = "Tax"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.Yellow
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Navy
        Me.Label17.Location = New System.Drawing.Point(2, 88)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(70, 25)
        Me.Label17.TabIndex = 85
        Me.Label17.Text = "Net Discount"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Yellow
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Navy
        Me.Label16.Location = New System.Drawing.Point(2, 127)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(70, 25)
        Me.Label16.TabIndex = 84
        Me.Label16.Text = "Net Value"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Yellow
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Navy
        Me.Label15.Location = New System.Drawing.Point(2, 48)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(70, 24)
        Me.Label15.TabIndex = 83
        Me.Label15.Text = "Sales Amount"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Yellow
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Navy
        Me.Label14.Location = New System.Drawing.Point(2, 8)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(70, 25)
        Me.Label14.TabIndex = 82
        Me.Label14.Text = "Qty"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Yellow
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label12.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(0, 160)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(174, 40)
        Me.Label12.TabIndex = 81
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Yellow
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(0, 120)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(174, 40)
        Me.Label11.TabIndex = 80
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Yellow
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(0, 40)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(174, 40)
        Me.Label9.TabIndex = 79
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Yellow
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(0, 80)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(174, 40)
        Me.Label8.TabIndex = 78
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblNetTotal
        '
        Me.lblNetTotal.BackColor = System.Drawing.Color.Yellow
        Me.lblNetTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNetTotal.Font = New System.Drawing.Font("Arial", 15.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetTotal.ForeColor = System.Drawing.Color.Red
        Me.lblNetTotal.Location = New System.Drawing.Point(0, 256)
        Me.lblNetTotal.Name = "lblNetTotal"
        Me.lblNetTotal.Size = New System.Drawing.Size(174, 48)
        Me.lblNetTotal.TabIndex = 77
        Me.lblNetTotal.Text = "0000000.00"
        Me.lblNetTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Yellow
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(0, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(174, 40)
        Me.Label10.TabIndex = 76
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label65
        '
        Me.Label65.BackColor = System.Drawing.Color.Yellow
        Me.Label65.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label65.ForeColor = System.Drawing.Color.White
        Me.Label65.Location = New System.Drawing.Point(16, 128)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(80, 25)
        Me.Label65.TabIndex = 84
        Me.Label65.Text = "Net Value"
        Me.Label65.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label66
        '
        Me.Label66.BackColor = System.Drawing.Color.Yellow
        Me.Label66.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label66.ForeColor = System.Drawing.Color.White
        Me.Label66.Location = New System.Drawing.Point(16, 48)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(104, 24)
        Me.Label66.TabIndex = 83
        Me.Label66.Text = "Sales Amount"
        Me.Label66.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label67
        '
        Me.Label67.BackColor = System.Drawing.Color.Yellow
        Me.Label67.Font = New System.Drawing.Font("Arial", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label67.ForeColor = System.Drawing.Color.White
        Me.Label67.Location = New System.Drawing.Point(96, 128)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(80, 25)
        Me.Label67.TabIndex = 90
        Me.Label67.Text = "0.00"
        Me.Label67.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel6
        '
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Controls.Add(Me.Label45)
        Me.Panel6.Controls.Add(Me.Label44)
        Me.Panel6.Controls.Add(Me.Label43)
        Me.Panel6.Controls.Add(Me.Label42)
        Me.Panel6.Controls.Add(Me.Label41)
        Me.Panel6.Controls.Add(Me.Label39)
        Me.Panel6.Controls.Add(Me.lblSalesType)
        Me.Panel6.Location = New System.Drawing.Point(840, 64)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(176, 184)
        Me.Panel6.TabIndex = 159
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.Label45.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label45.Font = New System.Drawing.Font("Arial", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.Color.White
        Me.Label45.Location = New System.Drawing.Point(0, 158)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(174, 24)
        Me.Label45.TabIndex = 127
        Me.Label45.Text = "Close (Esc)"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.Label44.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label44.Font = New System.Drawing.Font("Arial", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.Color.White
        Me.Label44.Location = New System.Drawing.Point(0, 134)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(174, 24)
        Me.Label44.TabIndex = 126
        Me.Label44.Text = "Net Total (F10)"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.Label43.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label43.Enabled = False
        Me.Label43.Font = New System.Drawing.Font("Arial", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.Color.White
        Me.Label43.Location = New System.Drawing.Point(0, 110)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(174, 24)
        Me.Label43.TabIndex = 125
        Me.Label43.Text = "Bill Cancel (F8)"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.Label42.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label42.Font = New System.Drawing.Font("Arial", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.Color.White
        Me.Label42.Location = New System.Drawing.Point(0, 86)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(174, 24)
        Me.Label42.TabIndex = 124
        Me.Label42.Text = "RePrint (F9)"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.Label41.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label41.Font = New System.Drawing.Font("Arial", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.ForeColor = System.Drawing.Color.White
        Me.Label41.Location = New System.Drawing.Point(0, 62)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(174, 24)
        Me.Label41.TabIndex = 123
        Me.Label41.Text = "Save (F7)"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.Label39.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label39.Font = New System.Drawing.Font("Arial", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.Color.White
        Me.Label39.Location = New System.Drawing.Point(0, 38)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(174, 24)
        Me.Label39.TabIndex = 122
        Me.Label39.Text = "Clear (F6)"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSalesType
        '
        Me.lblSalesType.BackColor = System.Drawing.Color.Yellow
        Me.lblSalesType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSalesType.Font = New System.Drawing.Font("Arial", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesType.ForeColor = System.Drawing.Color.Black
        Me.lblSalesType.Location = New System.Drawing.Point(0, 0)
        Me.lblSalesType.Name = "lblSalesType"
        Me.lblSalesType.Size = New System.Drawing.Size(174, 38)
        Me.lblSalesType.TabIndex = 121
        Me.lblSalesType.Text = "INCLUSIVE SALES"
        Me.lblSalesType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PnlNetAmount
        '
        Me.PnlNetAmount.BackColor = System.Drawing.Color.Yellow
        Me.PnlNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PnlNetAmount.Controls.Add(Me.Label46)
        Me.PnlNetAmount.Controls.Add(Me.txtNetAmount)
        Me.PnlNetAmount.Controls.Add(Me.txtNetDiscountPercentage)
        Me.PnlNetAmount.Controls.Add(Me.txtNetDiscount)
        Me.PnlNetAmount.ForeColor = System.Drawing.Color.White
        Me.PnlNetAmount.Location = New System.Drawing.Point(840, 449)
        Me.PnlNetAmount.Name = "PnlNetAmount"
        Me.PnlNetAmount.Size = New System.Drawing.Size(174, 103)
        Me.PnlNetAmount.TabIndex = 160
        Me.PnlNetAmount.Visible = False
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.Yellow
        Me.Label46.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.Color.Navy
        Me.Label46.Location = New System.Drawing.Point(8, 16)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(160, 25)
        Me.Label46.TabIndex = 103
        Me.Label46.Text = "Net Amount"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtNetAmount
        '
        Me.txtNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNetAmount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetAmount.ForeColor = System.Drawing.Color.Black
        Me.txtNetAmount.Location = New System.Drawing.Point(8, 48)
        Me.txtNetAmount.MaxLength = 20
        Me.txtNetAmount.Name = "txtNetAmount"
        Me.txtNetAmount.Size = New System.Drawing.Size(160, 25)
        Me.txtNetAmount.TabIndex = 102
        Me.txtNetAmount.Tag = ""
        Me.txtNetAmount.Text = ""
        '
        'txtNetDiscountPercentage
        '
        Me.txtNetDiscountPercentage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNetDiscountPercentage.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetDiscountPercentage.ForeColor = System.Drawing.Color.Black
        Me.txtNetDiscountPercentage.Location = New System.Drawing.Point(8, 48)
        Me.txtNetDiscountPercentage.MaxLength = 20
        Me.txtNetDiscountPercentage.Name = "txtNetDiscountPercentage"
        Me.txtNetDiscountPercentage.Size = New System.Drawing.Size(56, 25)
        Me.txtNetDiscountPercentage.TabIndex = 105
        Me.txtNetDiscountPercentage.Tag = ""
        Me.txtNetDiscountPercentage.Text = ""
        '
        'txtNetDiscount
        '
        Me.txtNetDiscount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNetDiscount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetDiscount.ForeColor = System.Drawing.Color.Black
        Me.txtNetDiscount.Location = New System.Drawing.Point(64, 48)
        Me.txtNetDiscount.MaxLength = 20
        Me.txtNetDiscount.Name = "txtNetDiscount"
        Me.txtNetDiscount.Size = New System.Drawing.Size(104, 25)
        Me.txtNetDiscount.TabIndex = 104
        Me.txtNetDiscount.Tag = ""
        Me.txtNetDiscount.Text = ""
        '
        'PnlReprint
        '
        Me.PnlReprint.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(192, Byte))
        Me.PnlReprint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PnlReprint.Controls.Add(Me.btnReprintCancel)
        Me.PnlReprint.Controls.Add(Me.btnReprintOK)
        Me.PnlReprint.Controls.Add(Me.txtRePrintBillNo)
        Me.PnlReprint.Controls.Add(Me.Label47)
        Me.PnlReprint.Controls.Add(Me.Label48)
        Me.PnlReprint.Location = New System.Drawing.Point(385, 288)
        Me.PnlReprint.Name = "PnlReprint"
        Me.PnlReprint.Size = New System.Drawing.Size(248, 88)
        Me.PnlReprint.TabIndex = 163
        Me.PnlReprint.Visible = False
        '
        'btnReprintCancel
        '
        Me.btnReprintCancel.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnReprintCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReprintCancel.Image = CType(resources.GetObject("btnReprintCancel.Image"), System.Drawing.Image)
        Me.btnReprintCancel.Location = New System.Drawing.Point(168, 56)
        Me.btnReprintCancel.Name = "btnReprintCancel"
        Me.btnReprintCancel.Size = New System.Drawing.Size(64, 23)
        Me.btnReprintCancel.TabIndex = 75
        Me.btnReprintCancel.Text = "Cancel"
        '
        'btnReprintOK
        '
        Me.btnReprintOK.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnReprintOK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReprintOK.Image = CType(resources.GetObject("btnReprintOK.Image"), System.Drawing.Image)
        Me.btnReprintOK.Location = New System.Drawing.Point(96, 56)
        Me.btnReprintOK.Name = "btnReprintOK"
        Me.btnReprintOK.Size = New System.Drawing.Size(64, 23)
        Me.btnReprintOK.TabIndex = 76
        Me.btnReprintOK.Text = "OK"
        '
        'txtRePrintBillNo
        '
        Me.txtRePrintBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRePrintBillNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRePrintBillNo.ForeColor = System.Drawing.Color.Black
        Me.txtRePrintBillNo.Location = New System.Drawing.Point(96, 33)
        Me.txtRePrintBillNo.MaxLength = 20
        Me.txtRePrintBillNo.Name = "txtRePrintBillNo"
        Me.txtRePrintBillNo.Size = New System.Drawing.Size(136, 20)
        Me.txtRePrintBillNo.TabIndex = 73
        Me.txtRePrintBillNo.Tag = ""
        Me.txtRePrintBillNo.Text = ""
        '
        'Label47
        '
        Me.Label47.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.ForeColor = System.Drawing.Color.Black
        Me.Label47.Location = New System.Drawing.Point(32, 33)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(56, 15)
        Me.Label47.TabIndex = 74
        Me.Label47.Text = "Bill No"
        '
        'Label48
        '
        Me.Label48.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.ForeColor = System.Drawing.Color.Black
        Me.Label48.Location = New System.Drawing.Point(8, 8)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(82, 15)
        Me.Label48.TabIndex = 72
        Me.Label48.Text = "RePrint"
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlAdminPwd
        '
        Me.pnlAdminPwd.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(192, Byte))
        Me.pnlAdminPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlAdminPwd.Controls.Add(Me.btnAdminPwdCancel)
        Me.pnlAdminPwd.Controls.Add(Me.btnAdminPwdOK)
        Me.pnlAdminPwd.Controls.Add(Me.txtAdminPassword)
        Me.pnlAdminPwd.Controls.Add(Me.Label51)
        Me.pnlAdminPwd.Controls.Add(Me.Label52)
        Me.pnlAdminPwd.Location = New System.Drawing.Point(386, 288)
        Me.pnlAdminPwd.Name = "pnlAdminPwd"
        Me.pnlAdminPwd.Size = New System.Drawing.Size(246, 88)
        Me.pnlAdminPwd.TabIndex = 165
        Me.pnlAdminPwd.Visible = False
        '
        'btnAdminPwdCancel
        '
        Me.btnAdminPwdCancel.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnAdminPwdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAdminPwdCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdminPwdCancel.Image = CType(resources.GetObject("btnAdminPwdCancel.Image"), System.Drawing.Image)
        Me.btnAdminPwdCancel.Location = New System.Drawing.Point(160, 56)
        Me.btnAdminPwdCancel.Name = "btnAdminPwdCancel"
        Me.btnAdminPwdCancel.Size = New System.Drawing.Size(64, 23)
        Me.btnAdminPwdCancel.TabIndex = 75
        Me.btnAdminPwdCancel.Text = "Cancel"
        '
        'btnAdminPwdOK
        '
        Me.btnAdminPwdOK.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnAdminPwdOK.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAdminPwdOK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdminPwdOK.Image = CType(resources.GetObject("btnAdminPwdOK.Image"), System.Drawing.Image)
        Me.btnAdminPwdOK.Location = New System.Drawing.Point(88, 56)
        Me.btnAdminPwdOK.Name = "btnAdminPwdOK"
        Me.btnAdminPwdOK.Size = New System.Drawing.Size(64, 23)
        Me.btnAdminPwdOK.TabIndex = 76
        Me.btnAdminPwdOK.Text = "OK"
        '
        'txtAdminPassword
        '
        Me.txtAdminPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdminPassword.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdminPassword.ForeColor = System.Drawing.Color.Black
        Me.txtAdminPassword.Location = New System.Drawing.Point(88, 32)
        Me.txtAdminPassword.MaxLength = 20
        Me.txtAdminPassword.Name = "txtAdminPassword"
        Me.txtAdminPassword.PasswordChar = Microsoft.VisualBasic.ChrW(42)
        Me.txtAdminPassword.Size = New System.Drawing.Size(136, 20)
        Me.txtAdminPassword.TabIndex = 73
        Me.txtAdminPassword.Tag = ""
        Me.txtAdminPassword.Text = ""
        '
        'Label51
        '
        Me.Label51.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.ForeColor = System.Drawing.Color.Black
        Me.Label51.Location = New System.Drawing.Point(16, 33)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(64, 15)
        Me.Label51.TabIndex = 74
        Me.Label51.Text = "Password"
        '
        'Label52
        '
        Me.Label52.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.ForeColor = System.Drawing.Color.Black
        Me.Label52.Location = New System.Drawing.Point(8, 8)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(56, 15)
        Me.Label52.TabIndex = 72
        Me.Label52.Text = "Admin"
        Me.Label52.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmConsignmentSales
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1018, 664)
        Me.Controls.Add(Me.PnlReprint)
        Me.Controls.Add(Me.pnlAdminPwd)
        Me.Controls.Add(Me.PnlNetAmount)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.grdMarketRate)
        Me.Controls.Add(Me.grdSales)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.lblCompanyAddress)
        Me.Controls.Add(Me.lblCompanyName)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmConsignmentSales"
        Me.Text = "Consignment Sales"
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        CType(Me.grdSales, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdMarketRate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.PnlNetAmount.ResumeLayout(False)
        Me.PnlReprint.ResumeLayout(False)
        Me.pnlAdminPwd.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim sClassCode As String
    'variables used for the inventory has the class salestype as Pcs.
    Public msClsSalesType As String
    Public tempTV As Double
    Public tempSMC As Double
    Public Diff As Double
    Public NTotVal As Double
    Public NSMC As Double
    Dim sCustomerCode As String
    Dim bEdited As Boolean
    Public sSalesType As String
    Dim sCompanyCode As String
    Dim sTerminalCode As String
    Dim sLocationCode As String
    Dim sInvoiceNo As String
    Dim sSalesManCode As String
    Dim sGSTType As String
    Dim dTotalCost As Double
    Dim dGSTPerc As Double
    Dim dDisPercentage As Double
    Dim sDiscountType As String
    Dim bReprint As Boolean
    Dim CompName$, Address1$, Address2$, Country$, PhoneNo$, FaxNo$
    Dim mdTradeInAmt As Double, mdDepositAmt As Double
    Dim msInsAgreementNo$
    Dim mdCommission#, mdPurMC#
    Dim dDailyClassRate As Double
    Dim sPrinterType$, dCustomerDisplayPort#, dCashDrawerInterface#, dCashDrawerPort#
    Dim bCustomerDisplay As Boolean, bPrinter As Boolean
    Const GRDROWHT = 17.708
    Dim bVoiceEnabled As Boolean
    Dim mdTotalCommission As Double
    Dim dMarketCost As Double
    Dim dTotalWeight, dTotalGoldLoss, dTotalSMC, dRate As Double
    Dim bPassword As Boolean
    Dim sSDate As String
    Dim dActualSales#, dNetDiscount#, dNetAmount#
    Dim dGst As Double
    Dim dFindTotalMarketPrice As Double
    Dim dNetTotalMarketPrice As Double
    Dim s24K As String
    Dim s22K As String
    Dim s20K As String
    Dim s22S As String

    Const MRClassCode = 1
    Const MRRate = 2
    'class objects
    Dim objComp As New clsCompany
    Dim objTerminal As New clsTerminal
    Dim objClass As New ClsClass
    Dim objSM As New clsSalesMan
    Dim objCust As New clsCustomer
    Dim objSales As New ClsConsignmentSales
    Dim objInv As New clsInventory
    Dim objSupplier As New clsSuppliers
    Dim objSalesReturn As New clsConsignSalesReturn
    Dim objUM As New clsUserMasters

    Dim dAdvancePaid As Double
    Dim sOrderRefNo As String

    Const ICode = 1
    Const IClass = 2
    Const IDescription = 3
    Const IWeight = 4
    Const IGoldLoss = 5
    Const ICostPerGram = 6
    Const IDSWeight = 7
    Const ISMC = 8
    Const IPCS = 9
    Const ITotalValue = 10
    Const ITotalCost = 11
    Const ISalesType = 12
    Const IPMC = 13
    Const IGST = 14


    Private Sub txtSalesManPassword_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSalesManPassword.LostFocus
        Try
            If txtSalesManPassword.Text.Trim <> "" Then
                subGetSalesManPassword()
            Else
                MessageBox.Show("Please enter Sales Man password.")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmConsignmentSales_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Escape
                    Me.Close()
                Case Keys.Return
                    SendKeys.Send("{TAB}")
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmConsignmentSales_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            biflag = False
            bTradeinFlag = False
            subGridSetting(grdMarketRate)
            subGridSetting(grdSales)
            grdSales.MaxRows = 0
            txtIDD.Text = Format(CDate(ServerDate), "dd")
            txtIMM.Text = Format(CDate(ServerDate), "MM")
            txtIYYYY.Text = Format(CDate(ServerDate), "yyyy")
            subGetCompany()
            subLoadTerminalParameter()
            'cboExpensesType.SelectedIndex = 0
            subShowClassMarketRate()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subGetSalesManPassword()
        Dim dsSMPWD As New DataSet
        Try
            dsSMPWD = objSM.GetSalesManwithPassword(txtSalesManPassword.Text.Trim)
            If dsSMPWD.Tables(0).Rows.Count > 0 Then
                txtSalesManName.Text = Trim(dsSMPWD.Tables(0).Rows(0)("SalesManName") & "")
                sSalesManCode = Trim(dsSMPWD.Tables(0).Rows(0)("SalesmanCode") & "")
                sManCode = Trim(dsSMPWD.Tables(0).Rows(0)("SalesmanCode") & "")
                lblLastSalesMan.Text = Trim(dsSMPWD.Tables(0).Rows(0)("SalesManName") & "")
                lblLastBillNo.Text = Trim(dsSMPWD.Tables(0).Rows(0)("InvoiceNo") & "")
                lblLastBillAmount.Text = Trim(dsSMPWD.Tables(0).Rows(0)("LastSalesAmount") & "")
                If Trim$(txtCustomerCode.Text.Trim) = "" Then
                    txtCustomerCode.Text = "CASH"
                    subGetCustomerDetail()
                End If
            Else
                MessageBox.Show("Invalid Password.")
                txtSalesManPassword.Clear()
                txtSalesManPassword.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsSMPWD = Nothing
        End Try
    End Sub

    Private Sub subGetCompany()
        Dim dsComp As New DataSet
        Try
            dsComp = objComp.GetCompanyList()
            If dsComp.Tables(0).Rows.Count > 0 Then
                sCompanyCode = Trim(dsComp.Tables(0).Rows(0)("CompanyID") & "")
                sLocationCode = gthisLocation
                sTerminalCode = ""
                lblLocation.Text = gthisLocation
                lblTerminal.Text = ""
                dGSTPerc = Val(dsComp.Tables(0).Rows(0)("GstPerc") & "")
                dDisPercentage = Val(Trim(dsComp.Tables(0).Rows(0)("MaxDiscountAllowed") & ""))
                'tradein limit
                dTradeinLimit = Val(Trim(dsComp.Tables(0).Rows(0)("tradeinLimit") & ""))
                'reqd for report.
                CompName = Trim(dsComp.Tables(0).Rows(0)("Name") & "")
                Address1 = Trim(dsComp.Tables(0).Rows(0)("Address1") & "")
                Address2 = Trim(dsComp.Tables(0).Rows(0)("Address2") & "")
                Country = Trim(dsComp.Tables(0).Rows(0)("Country") & "")
                PhoneNo = Trim(dsComp.Tables(0).Rows(0)("Phone") & "")
                FaxNo = Trim(dsComp.Tables(0).Rows(0)("Fax") & "")
                lblCompanyName.Text = CompName
                lblCompanyAddress.Text = Address1 & ", " & Address2 & vbCrLf & " Phone : " & PhoneNo & " Fax : " & FaxNo
            Else
                MessageBox.Show("Locked.")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsComp = Nothing
        End Try
    End Sub

    Private Sub subLoadTerminalParameter()
        Dim iCnt As Integer
        Dim dsTerminal As New DataSet
        Try
            dsTerminal = objTerminal.GetTerminal(gTerminalCode)
            If dsTerminal.Tables(0).Rows.Count > 0 Then
                sPrinterType = Trim(dsTerminal.Tables(0).Rows(0)("PrinterType") & "")
                dCustomerDisplayPort = Val(dsTerminal.Tables(0).Rows(0)("cusdisplayportno") & "")
                If Val(dsTerminal.Tables(0).Rows(0)("PrinterAttached") & "") = 1 Then
                    bPrinter = True
                Else
                    bPrinter = False
                End If
                If Val(dsTerminal.Tables(0).Rows(0)("VoiceEnabled") & "") = 1 Then
                    bVoiceEnabled = True
                Else
                    bVoiceEnabled = False
                End If
            Else
                MessageBox.Show("Your Computer is not Configured")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsTerminal = Nothing
        End Try
    End Sub

    Private Sub subShowClassMarketRate()
        Try
            Dim dsClass As New DataSet
            Dim iCnt As Long
            dsClass = objClass.GetClassDetail("")
            If Not IsNothing(dsClass) Then
                If dsClass.Tables("Class").Rows.Count > 0 Then
                    grdMarketRate.MaxRows = 0
                    For iCnt = 0 To dsClass.Tables("CLASS").Rows.Count - 1
                        If Trim(dsClass.Tables("Class").Rows(iCnt)("DisplayCost") & "") = "Y" Then
                            grdMarketRate.MaxRows = grdMarketRate.MaxRows + 1
                            SetText(grdMarketRate, grdMarketRate.MaxRows, MRClassCode, Trim(dsClass.Tables("Class").Rows(iCnt)("ClassCode") & ""))
                            SetText(grdMarketRate, grdMarketRate.MaxRows, MRRate, Val(Trim(dsClass.Tables("Class").Rows(iCnt)("Rate") & "")))
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subGetCustomerDetail()
        Dim dsCust As New DataSet
        Try
            If txtCustomerCode.Text.Trim <> "" Then
                sCustomerCode = Trim(txtCustomerCode.Text.Trim)
                dsCust = objCust.GetCustomerwithCode(txtCustomerCode.Text.Trim)
                If dsCust.Tables(0).Rows.Count > 0 Then
                    txtCustomerName.Text = Trim(dsCust.Tables(0).Rows(0)("CustomerName") & "")
                    lblCustomerType.Text = Trim(dsCust.Tables(0).Rows(0)("CustomerType") & "")
                    txtAddress1.Text = Trim(dsCust.Tables(0).Rows(0)("Address1") & "")
                    txtAddress2.Text = Trim(dsCust.Tables(0).Rows(0)("Address2") & "")
                    txtAddress3.Text = Trim(dsCust.Tables(0).Rows(0)("Address3") & "")
                    txtInventoryCode.Focus()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsCust = Nothing
        End Try
    End Sub

    Private Sub btnCustomerCodeSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomerCodeSearch.Click
        Try
            Dim frmHelpCustomer As New frmHelpSearch(0, 0, "tblCustomer", "L", 4, "CustomerName", "", "CustomerCode", "CustomerCode", 80, "CustomerName", "CustomerName", 150, "Address1", "Address1", 90, "Address2", "Address2", 80)
            frmHelpCustomer.ShowDialog()
            txtCustomerCode.Text = frmHelpCustomer.Var1.Trim
            txtCustomerName.Text = frmHelpCustomer.Var2.Trim
            subGetCustomerDetail()
            sCustomerCode = Trim(txtCustomerCode.Text)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtInventoryCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInventoryCode.TextChanged

    End Sub

    Private Sub txtInventoryCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInventoryCode.GotFocus
        Try
            txtInventoryCode.SelectAll()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtInventoryCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtInventoryCode.KeyDown
        Try
            If bEdited = True And e.KeyCode = Keys.Delete Then
                grdSales.Row = grdSales.ActiveRow
                grdSales.Action = 5
                grdSales.MaxRows = grdSales.MaxRows - 1
                If grdSales.MaxRows = 1 Then
                    grdSales.MaxRows = 0
                Else
                    subTotal()
                End If
                subClearDetail()
            End If

            Select Case e.KeyCode
                Case Keys.F1
                    'If Trim(sCompanyShortCode) <> "GV" Then
                    '    pnlFindCost.Visible = True
                    '    txtFindInventory.Focus()
                    'End If
                Case Keys.F9 'reprint
                    If Trim(txtSalesManPassword.Text) <> "" Then
                        subGetSalesManPassword()
                    Else
                        Exit Sub
                    End If
                    If Trim(txtSalesManName.Text) <> "" Then
                        subReprint()
                    End If
                Case Keys.F6 ' clear
                    If grdSales.MaxRows > 0 Then
                        If (MsgBox("There are some items in the list. Do you still want to clear? ", vbYesNo)) = vbNo Then
                            Exit Sub
                        End If
                    End If
                    subClear()
                    'Case Keys.F8 'Bill Cancel
                    '    If Trim(txtSalesManPassword.Text) <> "" Then
                    '        subGetSalesManPassword()
                    '    Else
                    '        Exit Sub
                    '    End If
                    '    If Trim(txtSalesManName.Text) <> "" Then
                    '        subBillCancel()
                    '    End If
                Case Keys.F7 'save or tender
                    If Trim(txtSalesManPassword.Text) <> "" Then
                        subGetSalesManPassword()
                    Else
                        Exit Sub
                    End If
                    If Trim(txtSalesManName.Text) <> "" Then
                        subTender()
                    End If
                Case Keys.F10 'Net Total
                    If Val(lblSalesAmount.Text) > 0 Then
                        subNetDiscount()
                    End If
                Case Keys.Escape
                    If grdSales.MaxRows > 0 Then
                        If (MsgBox("There are some items in the list. Do you still want to close? ", vbYesNo)) = vbNo Then
                            Exit Sub
                        End If
                    End If
                    subExit()
                Case Keys.Escape
                    If PnlNetAmount.Visible = True Then
                        PnlNetAmount.Visible = False
                        txtInventoryCode.Focus()
                        Exit Sub
                    End If
                Case Keys.PageDown
                    If Trim(txtSalesManPassword.Text) <> "" Then
                        subGetSalesManPassword()
                    Else
                        Exit Sub
                    End If
                    If Trim(txtSalesManName.Text) <> "" Then
                        subExpenses()
                    End If

            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subTotal()
        Try
            If Not bEdited Then
                grdSales.MaxRows = grdSales.MaxRows + 1
            End If

            'grdSales.RowHeight(grdSales.MaxRows) = GRDROWHT

            Call SetText(grdSales, grdSales.MaxRows, 1, "Total:")

            grdSales.ForeColor = Color.Red
            grdSales.FontBold = True

            grdSales.Col = 4
            grdSales.Formula = "sum(d1:d" & grdSales.MaxRows - 1 & ")"


            grdSales.ForeColor = Color.Red
            grdSales.FontBold = True

            grdSales.Col = 5
            grdSales.Formula = "sum(e1:e" & grdSales.MaxRows - 1 & ")"

            grdSales.ForeColor = Color.Red
            grdSales.FontBold = True

            grdSales.Col = 7
            grdSales.Formula = "sum(g1:g" & grdSales.MaxRows - 1 & ")"

            grdSales.ForeColor = Color.Red
            grdSales.FontBold = True

            grdSales.Col = 8
            grdSales.Formula = "sum(h1:h" & grdSales.MaxRows - 1 & ")"

            grdSales.ForeColor = Color.Red
            grdSales.FontBold = True

            grdSales.Col = 9
            grdSales.Formula = "sum(i1:i" & grdSales.MaxRows - 1 & ")"


            grdSales.ForeColor = Color.Red
            grdSales.FontBold = True

            grdSales.Col = 10
            grdSales.Formula = "sum(j1:j" & grdSales.MaxRows - 1 & ")"


            grdSales.ForeColor = Color.Blue
            grdSales.FontBold = True
            'grdSales.FontSize = 11

            grdSales.Col = 11
            grdSales.Formula = "sum(k1:k" & grdSales.MaxRows - 1 & ")"


            grdSales.ForeColor = Color.Red
            grdSales.FontBold = True

            'fill the bill label values.
            If grdSales.MaxRows > 1 Then
                dTotalWeight = Val(GetText(grdSales, grdSales.MaxRows, IWeight))
                dTotalGoldLoss = Val(GetText(grdSales, grdSales.MaxRows, IGoldLoss))
                dTotalSMC = Val(GetText(grdSales, grdSales.MaxRows, ISMC))
                lblQty.Text = Format(Val(GetText(grdSales, grdSales.MaxRows, IPCS)), "0")
                lblSalesAmount.Text = Format(Val(GetText(grdSales, grdSales.MaxRows, ITotalValue)), "0.00")
                If Trim(lblNetDiscount.Text.Trim) = "" Then
                    lblNetDiscount.Text = Format("0.00")
                Else
                    lblNetDiscount.Text = Format(Val(lblNetDiscount.Text), "0.00")
                End If
                lblNetValue.Text = Format(Val(lblSalesAmount.Text) - Val(lblNetDiscount.Text), "0.00")
                subGetGSTAmount()
            Else
                lblQty.Text = Format("0")
                lblSalesAmount.Text = Format("0.00")
                lblNetDiscount.Text = Format("0.00")
                lblNetValue.Text = Format("0.00")
                lblTax.Text = Format("0.00")
                lblNetTotal.Text = Format("0.00")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subClearDetail()
        Try
            bEdited = False
            txtInventoryCode.Clear()
            txtInventoryDescription.Clear()
            txtWeight.Clear()
            txtGoldLoss.Clear()
            txtSMC.Clear()
            mdPurMC = 0
            txtCostPerGram.Clear()
            txtPCS.Clear()
            txtTotalValue.Clear()
            sClassCode = ""
            txtStoneWeight.Clear()
            dTotalCost = 0
            msClsSalesType = ""
            txtGST.Clear()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub subClear()
        Try
            '    txtSalesManName = ""
            '    txtSalesManPassword = ""
            optZeroRated.Enabled = False
            optInclusive.Enabled = False
            optExclusive.Enabled = False
            optInclusive.Checked = True
            txtCustomerCode.Text = "CASH"
            txtCustomerName.Clear()
            subGetCustomerDetail() 'txtCustomerCode_LostFocus()
            txtAddress1.Clear()
            txtAddress2.Clear()
            txtAddress3.Clear()
            txtRemarks.Clear()
            '    txtTerms = ""
            grdSales.MaxRows = 0
            lblQty.Text = ""
            lblSalesAmount.Text = ""
            lblNetDiscount.Text = ""
            lblNetValue.Text = ""
            lblTax.Text = ""
            lblNetTotal.Text = ""
            subClearDetail()
            'hide the discount frame
            PnlNetAmount.Visible = False
            txtIDD.Text = Format(ServerDate, "dd")
            txtIMM.Text = Format(ServerDate, "MM")
            txtIYYYY.Text = Format(ServerDate, "yyyy")

            '''    txtNetDiscountPercentage = Format(0, "0.00")
            '''    txtNetDiscount = Format(0, "0.00")
            txtNetAmount.Text = Format(0, "0.00")
            mdTotalCommission = 0
            'txtPayMode.Clear()
            txtSalesManPassword.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subReprint()
        Try
            PnlReprint.Visible = True
            txtRePrintBillNo.Text = ""
            txtRePrintBillNo.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subGetGSTAmount()
        Try
            Dim nGSTAmount As Double
            If optExclusive.Checked = True Then
                lblSalesType.Text = "EXCLUSIVE SALES"
                If dGSTPerc > 0 Then
                    nGSTAmount = Val(lblNetValue.Text) * dGSTPerc / 100
                Else
                    nGSTAmount = 0
                End If
                lblTax.Text = Format(nGSTAmount, "0.00")
                lblNetTotal.Text = Format(Val(lblNetValue.Text) + nGSTAmount, "0.00")
            ElseIf optInclusive.Checked = True Then
                lblSalesType.Text = "INCLUSIVE SALES"
                If dGSTPerc > 0 Then
                    nGSTAmount = (Val(lblNetValue.Text) * dGSTPerc) / (100 + dGSTPerc)
                Else
                    nGSTAmount = 0
                End If
                lblTax.Text = Format(nGSTAmount, "0.00")
                lblNetTotal.Text = Format(Val(lblNetValue.Text), "0.00")
            ElseIf optZeroRated.Checked = True Then
                lblSalesType.Text = "ZERO RATED SALES"
                lblTax.Text = Format(0, "0.00")
                lblNetTotal.Text = Format(Val(lblNetValue.Text), "0.00")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    

    Private Sub subTender()
        Try
            If fncValidate() Then
                subCheckMarketRate()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subNetDiscount()
        Try
            If grdSales.MaxRows > 1 Then
                PnlNetAmount.Visible = True
                'txtNetDiscountPercentage.SetFocus
                txtNetAmount.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subExpenses()
        Try
            'pnlExpense.Enabled = True
            'pnlExpense.Visible = True
            'cboExpensesType.Enabled = True
            'cboExpensesType.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subExit()
        Try
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function fncValidate() As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            If Trim(txtCustomerCode.Text) = "" Then
                MsgBox("Please enter Customer! ", vbCritical)
                fncValidate = False
                txtCustomerCode.Focus()
                Exit Function
            End If
            If grdSales.MaxRows <= 0 Or Val(lblSalesAmount.Text) = 0 Then
                MsgBox("Enter items for sales! ", vbCritical)
                txtInventoryCode.Focus()
                fncValidate = False
                Exit Function
            End If
            If Not IsDate(Trim(txtIDD.Text) + "/" + Trim(txtIMM.Text) + "/" + Trim(txtIYYYY.Text)) Then
                MsgBox("INVOICE DATE is invalid...!")
                txtIDD.Focus()
                fncValidate = False
                Exit Function
            End If

            'If Trim(txtTerms) = "" Then
            '    MsgBox "Please enter terms of sale! ", vbCritical
            '    fncValidate = False
            '    Exit Function
            'End If
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncValidate = bStatus
        End Try
    End Function

    Private Sub subCheckMarketRate()
        Try
            If Not CheckMarketCost() Then Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Function CheckMarketCost() As Boolean
        Try
            Dim sMsg As String

            'If Trim(sCompanyShortCode) = "GV" Then
            dNetTotalMarketPrice = CalcMarketRate()
            If dNetTotalMarketPrice <= 0 Then
                CheckMarketCost = False
                Exit Function
            End If
            If Val(lblNetTotal.Text) <> 0 Then
                If (Val(lblNetTotal.Text) < dNetTotalMarketPrice) Then
                    sMsg = MsgBox("Bill total: $" & Format(Val(lblNetTotal.Text), "#0.00") & " is less than Market price: $" & Format(Val(dNetTotalMarketPrice), "#0.00") & "." & vbCrLf & "Do you want to proceed with negative profit? ", vbYesNo)
                    If sMsg = vbYes Then
                        subGetAdminPasword()
                    Else
                        pnlAdminPwd.Visible = False
                        Exit Function
                    End If
                Else
                    CheckMarketCost = True
                    subPaymentTypeOk()
                End If
            End If
            'Else
            'CheckMarketCost = True
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Function CalcMarketRate() As Double

        Dim dsMR As New DataSet
        Dim dTotalWeight As Double
        Dim sClassCode As String
        Dim dRate As Double
        Dim dMCDiscount As Double
        Dim dMarketRate As Double
        Dim dTempTotalMarketPrice As Double
        Dim dSMC As Double
        Dim iCnt As Long
        Dim sSalesDate As String
        Try
            sSalesDate = Trim(txtIDD.Text) + "/" + Trim(txtIMM.Text) + "/" + Trim(txtIYYYY.Text)
            CalcMarketRate = 0
            dFindTotalMarketPrice = 0
            dTempTotalMarketPrice = 0
            dDailyClassRate = 0
            For iCnt = 1 To grdSales.MaxRows - 1
                dSMC = Val(GetText(grdSales, iCnt, ISMC))
                sClassCode = Trim(GetText(grdSales, iCnt, 2))
                dTotalWeight = Val(GetText(grdSales, iCnt, IWeight)) + Val(GetText(grdSales, iCnt, IGoldLoss))

                dsMR = objSales.GetMarketRate(sClassCode, sSalesDate)
                If dsMR.Tables(0).Rows.Count = 0 Then
                    MessageBox.Show("Market Rate Not Found for this Class. Please set market rate!")
                    Exit Function
                Else
                    dRate = Val(dsMR.Tables(0).Rows(0)("Rate") & "")
                End If

                dMCDiscount = objSales.GetMCDiscoutFromClass(sClassCode)

                'dMarketRate = (dTotalWeight * dRate) + (dSMC / 2)
                'dMarketRate = dMarketRate + (dMarketRate * dGSTPerc / 100)

                'this was chaged as per the below formula given by durai 
                'MarketPrice = ( (Weight+GoldLoss)*(DailyClassRate))+ (SMC *(1-( MCDiscount  /100))

                dMarketRate = (dTotalWeight * dRate) + (dSMC * (1 - (dMCDiscount / 100)))
                dDailyClassRate = dRate
                dTempTotalMarketPrice = (dTotalWeight * dRate) + dSMC
                dTempTotalMarketPrice = dTempTotalMarketPrice + (dTempTotalMarketPrice * dGSTPerc / 100)
                CalcMarketRate = CalcMarketRate + dMarketRate
                dFindTotalMarketPrice = dFindTotalMarketPrice + dTempTotalMarketPrice
            Next iCnt
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsMR = Nothing
        End Try
    End Function

    Private Sub subGetAdminPasword()
        Try
            pnlAdminPwd.Visible = True
            txtAdminPassword.Clear()
            txtAdminPassword.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subPaymentTypeOk()
        Try
            objSales.OpenSalesConnection()
            objSales.gpBeginSalesTransaction()
            dPaidAmount = 0
            ReturnAmount = 0
            mdTradeInAmt = 0
            msInsAgreementNo = ""
            mdDepositAmt = 0
            mdCommission = 0

            If fncGetInvoiceNo() = False Then
                MessageBox.Show("Transaction failed!!!")
                objSales.gpRollBackSalesConnection()
                Exit Sub
            Else
                sSalesType = "CONSIGNMENT SALES"
                nTotalNet = Val(lblNetTotal.Text.Trim)
                If Not fncSales(sInvoiceNo) Then
                    MessageBox.Show("Transaction not saved")
                    objSales.gpRollBackSalesConnection()
                    Exit Sub
                End If
            End If
            objSales.gpCommitSalesTransaction()
            Call subPrintBill(Trim(sInvoiceNo))
            MsgBox("Please take note the Sales no " + sInvoiceNo)
            'SubSayEnglish("THANK YOU PLEASE COME AGAIN ")
            subClear()
        Catch ex As Exception
            objSales.gpRollBackSalesConnection()
            MessageBox.Show(ex.Message)
        Finally
            objSales.CloseSalesConnection()
        End Try
    End Sub

    Private Function fncGetInvoiceNo() As Boolean
        Dim dsComp As New DataSet
        Dim dsInv As New DataSet
        Try
            dsComp = objComp.GetCompanyList()
            If dsComp.Tables(0).Rows.Count > 0 Then
                sCompanyCode = Trim(dsComp.Tables(0).Rows(0)("CompanyID") & "")
                sLocationCode = gthisLocation
                sTerminalCode = ""
                sGSTType = Trim(dsComp.Tables(0).Rows(0)("GSTType") & "")
                dGSTPerc = Val(dsComp.Tables(0).Rows(0)("GstPerc") & "")
            Else
                MsgBox("Locked")
            End If

            dsInv = objSales.GetUpdateInvoice()
            If dsInv.Tables(0).Rows.Count > 0 Then
                sInvoiceNo = Trim(dsInv.Tables(0).Rows(0)("NextConsignmentSalesNo") & "")
                fncGetInvoiceNo = True
            Else
                fncGetInvoiceNo = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsComp = Nothing
            dsInv = Nothing
        End Try
    End Function

    Private Function fncSales(ByRef sSalesNo As String) As Boolean
        Dim DsRate As New DataSet
        Dim sPayType As String
        Dim bSalesDetail As Boolean
        Dim iCnt As Long
        Dim dRunningNetDiscount, dRunningCommission As Double
        Dim nDiscount, nNetTotal, nPaidAmt, nBalanceAmt, nDetailDiscount
        Dim dTotalWeight, dTotalStoneWeight, dTotalGoldLoss, dTotalSMC, dTotalPCS, dTotalValue, dTotalCost As Double
        Dim dNetCommission#, dCommission#
        Dim sSalesDate As String
        Try
            bSalesDetail = False
            'txtPMSurCharge.Text = dPayModeCommission
            dTotalWeight = GetText(grdSales, grdSales.MaxRows, 4)
            dTotalGoldLoss = GetText(grdSales, grdSales.MaxRows, 5)
            dTotalStoneWeight = GetText(grdSales, grdSales.MaxRows, 7)
            dTotalSMC = GetText(grdSales, grdSales.MaxRows, 8)
            dTotalPCS = GetText(grdSales, grdSales.MaxRows, 9)
            dTotalValue = GetText(grdSales, grdSales.MaxRows, 10)
            dTotalCost = GetText(grdSales, grdSales.MaxRows, 11)

            dGst = Format(Val(lblTax.Text), "0.00")
            nDetailDiscount = "0.00"
            dNetDiscount = Format(Val(lblNetDiscount.Text), "0.00")
            nDiscount = Format(Val(nDetailDiscount) + Val(dNetDiscount), "0.00")
            nNetTotal = Format(Val(lblNetTotal.Text), "0.00")
            'dNetCommission = Val(txtPMSurCharge.Text)
            nNetTotal = nNetTotal + dNetCommission

            If optExclusive.Checked = True Then
                sGSTType = "E"
            ElseIf optInclusive.Checked = True Then
                sGSTType = "I"
            ElseIf optZeroRated.Checked = True Then
                sGSTType = "Z"
            End If
            sSDate = Trim(txtIDD.Text) + "/" + Trim(txtIMM.Text) + "/" + Trim(txtIYYYY.Text)

            'Parameters added are nDetailDiscount, dNetDiscount, nDiscount, nNetTotal, nPaidAmt and nBalanceAmt
            fncSales = objSales.SalesHeaderSave(Trim(sInvoiceNo), Trim(sSDate), Trim(sCompanyCode), Trim(sLocationCode), Trim(gTerminalCode), _
                                Trim(txtCustomerCode.Text), Trim(txtCustomerName.Text), Trim(txtAddress1.Text), _
                                Trim(txtAddress2.Text), Trim(txtAddress3.Text), Trim(sSalesManCode), Trim$(sSalesType), _
                                Val(dTotalWeight), Val(dTotalStoneWeight), Val(dTotalGoldLoss), _
                                Val(dTotalSMC), Val(dTotalPCS), Val(dGst), Trim(sGSTType), Val(nDetailDiscount), _
                                Val(dNetDiscount), Val(nDiscount), Val(nNetTotal), _
                                Val(dPaidAmount), Val(ReturnAmount), _
                                Val(dTotalValue), Val(dTotalCost), _
                                Val(mdTradeInAmt), Val(mdDepositAmt), Trim$(msInsAgreementNo), _
                                Val(dNetCommission), Trim(txtRemarks.Text))
            Call CalcMarketRate()
            If bPassword Then
                iRet = objSales.AllowMarketPriceSave(Trim(sInvoiceNo), Trim(sSDate), dDailyClassRate, dNetDiscount, dGst, Val(txtTotalValue.Text), dNetTotalMarketPrice)
            End If

            Call objSales.FindTotalMarketPriceSave(Trim(sInvoiceNo), dFindTotalMarketPrice)

            If fncSales Then
                dRunningNetDiscount = 0
                dRunningCommission = 0
                For iCnt = 1 To grdSales.MaxRows - 1

                    dNetDiscount = Format("0.00")
                    dCommission = 0

                    If Val(lblSalesAmount.Text) <> 0 Then
                        If iCnt = grdSales.MaxRows - 1 Then
                            dNetDiscount = Val(lblNetDiscount.Text) - dRunningNetDiscount
                            If Val(dNetCommission) > 0 Then
                                dCommission = Val(dNetCommission) - dRunningCommission
                            End If
                        Else
                            dNetDiscount = Format((Val(GetText(grdSales, iCnt, 10)) * Val(lblNetDiscount.Text)) / Val(lblSalesAmount.Text), "0.00")
                            If Val(dNetCommission) > 0 Then
                                dCommission = Format((Val(GetText(grdSales, iCnt, 10)) * Val(dNetCommission)) / Val(lblSalesAmount.Text), "0.00")
                            End If
                        End If
                        dRunningNetDiscount = dRunningNetDiscount + dNetDiscount
                        dRunningCommission = dRunningCommission + dCommission
                    End If

                    dGst = Format("0.00")
                    dNetAmount = Format("0.00")
                    dActualSales = Format("0.00")
                    If optExclusive.Checked = True Then
                        dGst = Format((Val(GetText(grdSales, iCnt, 10)) - dNetDiscount) * dGSTPerc / (100), "0.0000")
                        dNetAmount = Format((Val(GetText(grdSales, iCnt, 10)) - dNetDiscount + dGst + dCommission), "0.00")
                        dActualSales = Format((Val(GetText(grdSales, iCnt, 10)) - dNetDiscount + dCommission), "0.00")
                    ElseIf optInclusive.Checked = True Then
                        dGst = Format((Val(GetText(grdSales, iCnt, 10)) - dNetDiscount) * dGSTPerc / (dGSTPerc + 100), "0.0000")
                        dNetAmount = Format((Val(GetText(grdSales, iCnt, 10)) - dNetDiscount + dCommission), "0.00")
                        dActualSales = Format((Val(GetText(grdSales, iCnt, 10)) - dNetDiscount - dGst + dCommission), "0.00")
                    ElseIf optZeroRated.Checked = True Then
                        dGst = Format("0.0000")
                        dNetAmount = Format((Val(GetText(grdSales, iCnt, 10)) - dNetDiscount) + dCommission, "0.00")
                        dActualSales = Format((Val(GetText(grdSales, iCnt, 10)) - dNetDiscount + dCommission), "0.00")
                    End If

                    nDetailDiscount = 0

                    'Modified by Durai on 06 Sep'2006

                    sClassCode = Trim(GetText(grdSales, iCnt, 2))
                    sSalesDate = Trim(txtIDD.Text) + "/" + Trim(txtIMM.Text) + "/" + Trim(txtIYYYY.Text)
                    DsRate = objSales.GetMarketRate(sClassCode, sSalesDate)
                    If DsRate.Tables(0).Rows.Count > 0 Then
                        dRate = Val(DsRate.Tables(0).Rows(0)("Rate") & "")
                    End If
                    dDailyClassRate = dRate

                    'Parameters added are sLocationCode, nDetailDiscount, dNetDiscount, dGst, dNetAmount and dActualSales
                    bSalesDetail = objSales.SalesDetailSave(Trim(sInvoiceNo), _
                                        Trim(GetText(grdSales, iCnt, 1)), Trim(GetText(grdSales, iCnt, 3)), sLocationCode, _
                                        Val(GetText(grdSales, iCnt, 4)), Val(GetText(grdSales, iCnt, 7)), _
                                        Val(GetText(grdSales, iCnt, 5)), Val(GetText(grdSales, iCnt, 6)), _
                                        Val(GetText(grdSales, iCnt, 8)), Val(GetText(grdSales, iCnt, 9)), _
                                        Val(nDetailDiscount), Val(dNetDiscount), Val(dGst), _
                                        Val(dNetAmount), Val(dActualSales), _
                                        Val(GetText(grdSales, iCnt, 10)), Val(GetText(grdSales, iCnt, 11)), _
                                        Val(dCommission), Trim(txtRemarks.Text), Val(dDailyClassRate), iCnt)

                    If Not bSalesDetail Then
                        'MsgBox(sError1)
                        fncSales = False
                        Exit Function
                    End If

                Next iCnt

            End If
            bPassword = False

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Sub subPrintBill(ByVal sInvoiceNo As String)
        Dim ReportTitle As String
        Dim sSelection As String
        Dim sSalesType$
        Dim sReply As String
        Dim dsSales As New DataSet
        Dim frmrep As New frmReports
        Dim CR As New ReportDocument
        Try
            Cursor = Cursors.WaitCursor
            dsSales = objSales.GetSalesHeaderByInvoiceNo(Trim(sInvoiceNo))
            If dsSales.Tables(0).Rows.Count > 0 Then sSalesType = Trim$(dsSales.Tables(0).Rows(0)("SalesType") & "")
            sReply = MsgBox("Do you want to print Net Bill?", vbYesNoCancel + vbQuestion, "Print Option")
            If sReply = vbYes Then
                strpath = Application.StartupPath & "\Reports\rptConsignmentSalesNet.rpt"
                CR.Load(strpath)
                ReportLogOnToServer(CR)
                CR.Refresh()
                frmrep.ReportViewer.ReportSource = CR
                sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                frmrep.ReportViewer.SelectionFormula = sSelection
                CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                CR.PrintToPrinter(1, True, 0, 0)
                frmrep.Show()
                frmrep.ReportViewer.Visible = True
                frmrep.ReportViewer.DisplayToolbar = True
                frmrep.ReportViewer.CloseView(False)
                frmrep.WindowState = FormWindowState.Maximized
                frmrep = Nothing
                frmrep = New frmReports
            ElseIf sReply = vbNo Then
                strpath = Application.StartupPath & "\Reports\rptConsignmentSales.rpt"
                CR.Load(strpath)
                ReportLogOnToServer(CR)
                CR.Refresh()
                frmrep.ReportViewer.ReportSource = CR
                sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                frmrep.ReportViewer.SelectionFormula = sSelection
                CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                frmrep.Show()
                frmrep.ReportViewer.Visible = True
                frmrep.ReportViewer.DisplayToolbar = True
                frmrep.ReportViewer.CloseView(False)
                frmrep.WindowState = FormWindowState.Maximized
                frmrep = Nothing
                frmrep = New frmReports
            ElseIf sReply = vbCancel Then
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub txtInventoryCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInventoryCode.LostFocus
        Try
            If Trim(txtInventoryCode.Text) <> "" Then
                If Not fncCheckExisting(txtInventoryCode.Text) Then
                    If fncGetInventory(txtInventoryCode.Text) Then
                        If Trim(sClassCode) <> "" Then
                            If Val(txtCostPerGram.Text) <> 0 Then
                                txtTotalValue.Text = Format((Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram.Text) + Val(txtSMC.Text), "0.00")
                                If optExclusive.Checked = True Then
                                    txtGST.Text = Format$(Val(txtTotalValue.Text) * (dGSTPerc / 100), "#0.00")
                                    txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                                ElseIf optInclusive.Checked = True Then
                                    txtGST.Text = Format$((Val(txtTotalValue.Text) * dGSTPerc) / (100 + dGSTPerc), "#0.00")
                                    txtTotalValue.Text = Format(Val(txtTotalValue.Text), "0.00")
                                ElseIf optZeroRated.Checked = True Then
                                    txtGST.Text = 0.0
                                    txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                                End If
                                dRate = Val(txtCostPerGram.Text)
                                txtGoldLoss_LostFocus(sender, e)
                                txtPCS_LostFocus(sender, e)
                                txtSMC_LostFocus(sender, e)
                                txtInventoryCode.Focus()
                                Exit Sub
                            End If
                            If Trim(sClassCode) <> "" Then
                                'txtGoldLoss.SetFocus
                            Else
                                txtInventoryCode.Focus()
                                Exit Sub
                            End If
                        End If
                    End If
                Else
                    If bEdited = False Then
                        MessageBox.Show("Item already scanned ...")
                        txtInventoryCode.Focus()
                        Exit Sub
                    End If
                End If
            Else
                If Trim(txtInventoryCode.Text) = "" Then
                    Exit Sub
                End If
                'txtInventoryCode.SetFocus
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function fncCheckExisting(ByVal sinventorycode As String) As Boolean
        Dim icnt As Long
        Try
            fncCheckExisting = False
            For icnt = 1 To grdSales.MaxRows
                If Trim(GetText(grdSales, icnt, 1)) = Trim(sinventorycode) Then
                    fncCheckExisting = True
                    Exit For
                End If
            Next icnt
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Function fncGetInventory(ByVal sinventorycode As String) As Boolean
        Dim lSalesBillNo&
        Dim dsInv As New DataSet
        Try
            dsInv = objInv.GetInventoryDetail(sinventorycode)
            fncGetInventory = True
            If dsInv.Tables(0).Rows.Count > 0 Then
                lSalesBillNo = Val(dsInv.Tables(0).Rows(0)("SalesBillNo") & "")
                'If Val(adoRS!SalesAmount & "") > 0 Then
                If Val(dsInv.Tables(0).Rows(0)("QtyOnHand") & "") = 0 Then
                    MsgBox("Item Already Sold for Invoice " & Trim(dsInv.Tables(0).Rows(0)("SalesBillNo") & "") & " on " & Format(dsInv.Tables(0).Rows(0)("SalesDAte"), "dd/MM/yyyy"))
                    fncGetInventory = False
                    txtInventoryCode.Focus()
                    Exit Function
                End If
                If Val(dsInv.Tables(0).Rows(0)("QtyOnHand") & "") < 1 Then
                    MsgBox("No Stock for this Inventory!")
                    fncGetInventory = False
                    txtInventoryCode.Focus()
                    Exit Function
                End If
                If Trim$(dsInv.Tables(0).Rows(0)("LocationCode") & "") <> gthisLocation Then
                    MsgBox("The selected Inventory belongs to the Location - " & Trim$(dsInv.Tables(0).Rows(0)("LocationCode") & "") & "!")
                    fncGetInventory = False
                    txtInventoryCode.Focus()
                    Exit Function
                End If
                'added by sarathiy to check the inventory is ordered for some other customer
                'dAdvancePaid = 0.0
                If Trim$(dsInv.Tables(0).Rows(0)("OrderNo") & "") <> "" Then
                    Dim dsOrder As New DataSet
                    Dim objOrder As New clsOrder
                    dsOrder = objOrder.GetOrderDetail(Trim$(dsInv.Tables(0).Rows(0)("OrderNo") & ""))
                    If dsOrder.Tables(0).Rows.Count > 0 Then
                        If txtCustomerCode.Text.Trim <> Trim(dsOrder.Tables(0).Rows(0)("CustomerCode")) Then
                            MessageBox.Show("This inventory is ordered for some other customer. you Cant sale this now.")
                            fncGetInventory = False
                            txtInventoryCode.Focus()
                            Exit Function
                        Else
                            'added by sarathy to calculate advace paid amount
                            dAdvancePaid = Val(dAdvancePaid) + Val(dsOrder.Tables(0).Rows(0)("AdvancePaid"))
                            sOrderRefNo = sOrderRefNo & " " & Trim$(dsInv.Tables(0).Rows(0)("OrderNo") & "")

                            sClassCode = Trim(dsInv.Tables(0).Rows(0)("ClassCode") & "")
                            'load class details to get the rate and the salestype.
                            subGetClass()
                            'chage the cost per gram if the as per ordered date
                            If dsOrder.Tables(0).Rows(0)("RateFixed") = "Y" Then
                                txtCostPerGram.Text = Format(Val(dsOrder.Tables(0).Rows(0)("CostPerGram")), "0.00")
                            End If
                            txtInventoryDescription.Text = Trim(dsInv.Tables(0).Rows(0)("Description") & "")
                            If msClsSalesType = "P" Or msClsSalesType = "B" Then
                                txtPCS.Enabled = True
                                txtStoneWeight.Text = Format(Val(dsInv.Tables(0).Rows(0)("StoneWeight") & ""), "0.00")
                                If Trim(sCompanyShortCode) = "ES" Then
                                    txtPCS.Text = 1
                                Else
                                    txtPCS.Text = Val(dsInv.Tables(0).Rows(0)("QtyOnHand") & "")
                                End If
                                If Val(dsInv.Tables(0).Rows(0)("Pcs")) > 0 Then
                                    txtWeight.Text = Format(Val(dsInv.Tables(0).Rows(0)("Weight") & "") * Val(txtPCS.Text) / Val(dsInv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                                    txtGoldLoss.Text = Format(Val(dsInv.Tables(0).Rows(0)("GoldLoss") & "") * Val(txtPCS.Text) / Val(dsInv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                                    txtSMC.Text = Format(Val(dsInv.Tables(0).Rows(0)("sMc") & "") * Val(txtPCS.Text) / Val(dsInv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                                    mdPurMC = Val(dsInv.Tables(0).Rows(0)("PMC") & "") * Val(txtPCS.Text) / Val(dsInv.Tables(0).Rows(0)("Pcs") & "")
                                    dTotalCost = Val(dsInv.Tables(0).Rows(0)("TotalCost") & "") * Val(txtPCS.Text) / Val(dsInv.Tables(0).Rows(0)("Pcs") & "")
                                Else
                                    txtWeight.Text = 0.0
                                    txtGoldLoss.Text = 0.0
                                    txtSMC.Text = 0.0
                                    mdPurMC = 0.0
                                    dTotalCost = 0.0
                                End If
                            Else
                                txtStoneWeight.Text = Format(Val(dsInv.Tables(0).Rows(0)("StoneWeight") & ""), "0.00")
                                txtPCS.Text = Val(dsInv.Tables(0).Rows(0)("Pcs") & "")
                                txtWeight.Text = Format(Val(dsInv.Tables(0).Rows(0)("Weight") & ""), "0.00")
                                txtGoldLoss.Text = Format(Val(dsInv.Tables(0).Rows(0)("GoldLoss") & ""), "0.00")
                                txtSMC.Text = Format(Val(dsInv.Tables(0).Rows(0)("sMc") & ""), "0.00")
                                mdPurMC = Val(dsInv.Tables(0).Rows(0)("PMC") & "")
                                dTotalCost = Val(dsInv.Tables(0).Rows(0)("TotalCost") & "")
                            End If
                        End If


                    End If
                Else
                    sClassCode = Trim(dsInv.Tables(0).Rows(0)("ClassCode") & "")
                    'load class details to get the rate and the salestype.
                    subGetClass()
                    txtInventoryDescription.Text = Trim(dsInv.Tables(0).Rows(0)("Description") & "")
                    If msClsSalesType = "P" Or msClsSalesType = "B" Then
                        txtPCS.Enabled = True
                        txtStoneWeight.Text = Format(Val(dsInv.Tables(0).Rows(0)("StoneWeight") & ""), "0.00")
                        If Trim(sCompanyShortCode) = "ES" Then
                            txtPCS.Text = 1
                        Else
                            txtPCS.Text = Val(dsInv.Tables(0).Rows(0)("QtyOnHand") & "")
                        End If
                        If Val(dsInv.Tables(0).Rows(0)("Pcs")) > 0 Then
                            txtWeight.Text = Format(Val(dsInv.Tables(0).Rows(0)("Weight") & "") * Val(txtPCS.Text) / Val(dsInv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                            txtGoldLoss.Text = Format(Val(dsInv.Tables(0).Rows(0)("GoldLoss") & "") * Val(txtPCS.Text) / Val(dsInv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                            txtSMC.Text = Format(Val(dsInv.Tables(0).Rows(0)("sMc") & "") * Val(txtPCS.Text) / Val(dsInv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                            mdPurMC = Val(dsInv.Tables(0).Rows(0)("PMC") & "") * Val(txtPCS.Text) / Val(dsInv.Tables(0).Rows(0)("Pcs") & "")
                            dTotalCost = Val(dsInv.Tables(0).Rows(0)("TotalCost") & "") * Val(txtPCS.Text) / Val(dsInv.Tables(0).Rows(0)("Pcs") & "")
                        Else
                            txtWeight.Text = 0.0
                            txtGoldLoss.Text = 0.0
                            txtSMC.Text = 0.0
                            mdPurMC = 0.0
                            dTotalCost = 0.0
                        End If
                    Else
                        txtStoneWeight.Text = Format(Val(dsInv.Tables(0).Rows(0)("StoneWeight") & ""), "0.00")
                        txtPCS.Text = Val(dsInv.Tables(0).Rows(0)("Pcs") & "")
                        txtWeight.Text = Format(Val(dsInv.Tables(0).Rows(0)("Weight") & ""), "0.00")
                        txtGoldLoss.Text = Format(Val(dsInv.Tables(0).Rows(0)("GoldLoss") & ""), "0.00")
                        txtSMC.Text = Format(Val(dsInv.Tables(0).Rows(0)("sMc") & ""), "0.00")
                        mdPurMC = Val(dsInv.Tables(0).Rows(0)("PMC") & "")
                        dTotalCost = Val(dsInv.Tables(0).Rows(0)("TotalCost") & "")
                    End If
                End If
            Else
                MsgBox("Inventory Not Found ...")
                fncGetInventory = False
                txtInventoryCode.Focus()
                Exit Function
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Sub txtGoldLoss_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGoldLoss.LostFocus
        Try
            If Trim(txtInventoryCode.Text) <> "" Then
                txtTotalValue.Text = Format((Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram.Text) + Val(txtSMC.Text), "0.00")
                If optExclusive.Checked = True Then
                    txtGST.Text = Format$(Val(txtTotalValue.Text) * (dGSTPerc / 100), "#0.00")
                    txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                ElseIf optInclusive.Checked = True Then
                    txtGST.Text = Format$((Val(txtTotalValue.Text) * dGSTPerc) / (100 + dGSTPerc), "#0.00")
                    txtTotalValue.Text = Format(Val(txtTotalValue.Text), "0.00")
                ElseIf optZeroRated.Checked = True Then
                    txtGST.Text = 0.0
                    txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                End If
                'txtGST.Text = Format$(Val(txtTotalValue.Text) * (dGSTPerc / 100), "#0.00")
                'txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
            Else
                'commented becoz press on F4 focus going to inventory code instead of cash option control.
                'txtInventoryCode.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtPCS_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPCS.LostFocus
        Dim dsinv As New DataSet
        Try
            dMarketCost = 20
            If Trim$(txtInventoryCode.Text) = "" Then Exit Sub
            If Val(txtPCS.Text) < 1 Then
                txtPCS.Text = ""
                MessageBox.Show("Invalid Pieces!")
                txtPCS.Focus()
                Exit Sub
            End If

            If Trim(txtInventoryCode.Text) <> "" Then
                If msClsSalesType = "P" Or msClsSalesType = "B" Then
                    dsinv = objInv.GetInventoryDetail(Trim$(txtInventoryCode.Text))
                    'get the details for the selected inv. code and fill again the value fields according to the
                    'entered pieces.
                    txtPCS.Text = CLng(Val(txtPCS.Text))
                    If dsinv.Tables(0).Rows.Count > 0 Then
                        If Val(txtPCS.Text) > Val(dsinv.Tables(0).Rows(0)("QtyOnHand") & "") Then
                            MessageBox.Show("The entered Pieces " & Val(txtPCS.Text) & " exceeds the quantity on hand " & Val(dsinv.Tables(0).Rows(0)("QtyOnHand") & ""))
                            txtPCS.Text = ""
                            txtPCS.Focus()
                            Exit Sub
                        End If
                        txtWeight.Text = Format(Val(dsinv.Tables(0).Rows(0)("Weight") & "") * Val(txtPCS.Text) / Val(dsinv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                        txtGoldLoss.Text = Format(Val(dsinv.Tables(0).Rows(0)("GoldLoss") & "") * Val(txtPCS.Text) / Val(dsinv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                        txtSMC.Text = Format(Val(dsinv.Tables(0).Rows(0)("sMc") & "") * Val(txtPCS.Text) / Val(dsinv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                        mdPurMC = Val(dsinv.Tables(0).Rows(0)("PMC") & "") * Val(txtPCS.Text) / Val(dsinv.Tables(0).Rows(0)("Pcs") & "")
                        dTotalCost = Val(dsinv.Tables(0).Rows(0)("TotalCost") & "") * Val(txtPCS.Text) / Val(dsinv.Tables(0).Rows(0)("Pcs") & "")
                        txtTotalValue.Text = Format((Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram.Text) + Val(txtSMC.Text), "0.00")
                        If optExclusive.Checked = True Then
                            txtGST.Text = Format$(Val(txtTotalValue.Text) * (dGSTPerc / 100), "#0.00")
                            txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                        ElseIf optInclusive.Checked = True Then
                            txtGST.Text = Format$((Val(txtTotalValue.Text) * dGSTPerc) / (100 + dGSTPerc), "#0.00")
                            txtTotalValue.Text = Format(Val(txtTotalValue.Text), "0.00")
                        ElseIf optZeroRated.Checked = True Then
                            txtGST.Text = 0.0
                            txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                        End If
                        'txtGST.Text = Format$(Val(txtTotalValue.Text) * (dGSTPerc / 100), "#0.00")
                        'txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                        'dNetTotalMarketPrice = Format((Val(txtWeight) + Val(txtGoldLoss) + dMarketCost) * Val(txtRate) + Val(txtSMC / 2), "0.00")
                    End If
                Else
                    txtPCS.Text = 1
                    txtPCS.Enabled = False
                End If
            Else
                txtInventoryCode.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtSMC_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSMC.LostFocus
        Try
            If Trim(txtInventoryCode.Text) <> "" Then
                If txtPCS.Enabled And Val(txtPCS.Text) < 1 Then
                    'txtPCS.SetFocus
                    Exit Sub
                End If
                If Val(txtPCS.Text) < 1 Then txtPCS.Text = 1
                'SMC should not be less than PMC
                'If Val(txtSMC) < Val(mdPurMC) Then
                '    MsgBox "Please enter the SMC greater than " & Val(mdPurMC)
                '    txtSMC.SetFocus
                '    Exit Sub
                'End If
                txtTotalValue.Text = Format((Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram.Text) + Val(txtSMC.Text), "0.00")
                If optExclusive.Checked = True Then
                    txtGST.Text = Format$(Val(txtTotalValue.Text) * (dGSTPerc / 100), "#0.00")
                    txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                ElseIf optInclusive.Checked = True Then
                    txtGST.Text = Format$((Val(txtTotalValue.Text) * dGSTPerc) / (100 + dGSTPerc), "#0.00")
                    txtTotalValue.Text = Format(Val(txtTotalValue.Text), "0.00")
                ElseIf optZeroRated.Checked = True Then
                    txtGST.Text = 0.0
                    txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                End If
                'txtGST.Text = Format$(Val(txtTotalValue.Text) * (dGSTPerc / 100), "#0.00")
                'txtTotalValue.Text = Format(Val(txtTotalValue.Text) + Val(txtGST.Text), "0.00")
                subAddSales()
            Else
                txtInventoryCode.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subAddSales()
        Try
            grdSales.ForeColor = Color.Black
            grdSales.FontBold = False
            If Not bEdited Then
                If grdSales.MaxRows = 0 Then
                    grdSales.MaxRows = grdSales.MaxRows + 1
                Else
                    grdSales.Row = grdSales.MaxRows
                    grdSales.Action = 5
                    grdSales.MaxRows = grdSales.MaxRows - 1
                    grdSales.MaxRows = grdSales.MaxRows + 1
                End If
                grdSales.Row = grdSales.MaxRows
            Else
                grdSales.Row = grdSales.ActiveRow
                'grdSales.RowHeight(grdSales.ActiveRow) = GRDROWHT
            End If
            ''''added by sarathy to detect the advance paid amount for the ordered items
            '''If dAdvancePaid <> 0 Then txtTotalValue.Text = Format(Val(txtTotalValue.Text) - Val(dAdvancePaid), "0.00")
            TotVal = Val(txtTotalValue.Text)
            sMc = Val(txtSMC.Text)

            Call SetText(grdSales, grdSales.Row, ICode, Trim(txtInventoryCode.Text))
            Call SetText(grdSales, grdSales.Row, IClass, Trim(sClassCode))
            Call SetText(grdSales, grdSales.Row, IDescription, Trim(txtInventoryDescription.Text))
            Call SetText(grdSales, grdSales.Row, IWeight, Val(txtWeight.Text))
            Call SetText(grdSales, grdSales.Row, IGoldLoss, Val(txtGoldLoss.Text))
            Call SetText(grdSales, grdSales.Row, ICostPerGram, Val(txtCostPerGram.Text))
            Call SetText(grdSales, grdSales.Row, IDSWeight, Val(txtStoneWeight.Text))
            Call SetText(grdSales, grdSales.Row, ISMC, Val(txtSMC.Text))
            Call SetText(grdSales, grdSales.Row, IPMC, Val(mdPurMC))
            Call SetText(grdSales, grdSales.Row, IPCS, Val(txtPCS.Text))
            Call SetText(grdSales, grdSales.Row, ITotalValue, Val(txtTotalValue.Text))
            Call SetText(grdSales, grdSales.Row, ITotalCost, Val(dTotalCost))
            Call SetText(grdSales, grdSales.Row, ISalesType, Trim$(msClsSalesType))
            Call SetText(grdSales, grdSales.Row, IGST, Val(txtGST.Text))

            grdSales.Row = grdSales.MaxRows
            grdSales.Col = 1
            grdSales.Action = 0
            subTotal()
            subClearDetail()
            If grdSales.MaxRows >= 2 Then
                optZeroRated.Enabled = False
                optInclusive.Enabled = False
                optExclusive.Enabled = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subGetClass()
        Dim dsCls As New DataSet
        Try
            dsCls = objClass.GetClassDetail(sClassCode)
            If dsCls.Tables(0).Rows.Count > 0 Then
                txtCostPerGram.Text = Val(dsCls.Tables(0).Rows(0)("Rate") & "")
                msClsSalesType = Trim$(dsCls.Tables(0).Rows(0)("SalesType") & "")
            Else
                MessageBox.Show("Class Not defined for this item .... ")
                Exit Sub
            End If
            dsCls = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtAdminPassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAdminPassword.TextChanged

    End Sub

    Private Sub btnAdminPwdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdminPwdOK.Click
        Dim dsPWD As New DataSet
        Try
            If Trim(txtAdminPassword.Text) <> "" Then
                dsPWD = objUM.GetAdminPassword(Trim(txtAdminPassword.Text))
                If dsPWD.Tables(0).Rows.Count > 0 Then
                    If Trim(UCase(dsPWD.Tables(0).Rows(0)("UserGroupCode") & "")) = "ADMIN" Then
                        bPassword = True
                        pnlAdminPwd.Visible = False
                        If PnlReprint.Visible = True Then
                            Call subPrintBill(Trim(txtRePrintBillNo.Text))
                            bReprint = False
                            PnlReprint.Visible = False
                            subClear()
                        Else
                            Call Payment()
                        End If
                    Else
                        MessageBox.Show("Please Check the Password")
                        txtAdminPassword.Focus()
                        bPassword = False
                        Exit Sub
                    End If
                Else
                    MessageBox.Show("Please Check the Password")
                    txtAdminPassword.Focus()
                    bPassword = False
                    Exit Sub
                End If
            Else
                MessageBox.Show("Enter the Password")
                txtAdminPassword.Focus()
                bPassword = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Payment()
        Try
            If Val(lblNetTotal.Text) <> 0 Then
                subPaymentTypeOk()
            Else
                txtInventoryCode.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnAdminPwdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdminPwdCancel.Click
        Try
            txtAdminPassword.Clear()
            pnlAdminPwd.Visible = False
            bPassword = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnReprintOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReprintOK.Click
        Dim sDate As Date
        Dim strDate As String
        Dim dsInvoice As New DataSet
        Try
            If Trim(txtRePrintBillNo.Text) <> "" Then
                bReprint = True

                dsInvoice = objSales.GetSalesHeaderByInvoiceNo(Trim(txtRePrintBillNo.Text))
                If dsInvoice.Tables(0).Rows.Count > 0 Then
                    sDate = dsInvoice.Tables(0).Rows(0)("InvoiceDate") & ""
                    strDate = Format(CDate(sDate), "yyyy/MM/dd")
                    'strDate = Mid(sDate, 7, 4) & "/" & Mid(sDate, 4, 2) & "/" & Mid(sDate, 1, 2)

                    If ServerDate = CDate(Trim(strDate)) Then
                        Call subPrintBill(Trim(txtRePrintBillNo.Text))
                        bReprint = False
                        PnlReprint.Visible = False
                        subClear()
                    Else
                        subGetAdminPasword()
                    End If
                Else
                    MessageBox.Show(" BillNo is not Correct!...")
                    bReprint = False
                    PnlReprint.Visible = False
                    subClear()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnReprintCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReprintCancel.Click
        Try
            txtRePrintBillNo.Text = ""
            PnlReprint.Visible = False
            txtInventoryCode.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub grdSales_DblClick(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_DblClickEvent) Handles grdSales.DblClick
        Try
            If Trim(GetText(grdSales, grdSales.ActiveRow, 1)) <> "Total:" Then
                bEdited = True
                txtInventoryCode.Text = GetText(grdSales, grdSales.ActiveRow, ICode)
                sClassCode = GetText(grdSales, grdSales.ActiveRow, IClass)
                txtInventoryDescription.Text = GetText(grdSales, grdSales.ActiveRow, IDescription)
                txtWeight.Text = GetText(grdSales, grdSales.ActiveRow, IWeight)
                txtGoldLoss.Text = GetText(grdSales, grdSales.ActiveRow, IGoldLoss)
                txtCostPerGram.Text = GetText(grdSales, grdSales.ActiveRow, ICostPerGram)
                txtStoneWeight.Text = GetText(grdSales, grdSales.ActiveRow, IDSWeight)
                txtSMC.Text = GetText(grdSales, grdSales.ActiveRow, ISMC)
                mdPurMC = GetText(grdSales, grdSales.ActiveRow, IPMC)
                txtPCS.Text = GetText(grdSales, grdSales.ActiveRow, IPCS)
                txtTotalValue.Text = GetText(grdSales, grdSales.ActiveRow, ITotalValue)
                'added by sarathy
                TotVal = txtTotalValue.Text.Trim
                dTotalCost = Val(GetText(grdSales, grdSales.ActiveRow, ITotalCost))
                msClsSalesType = GetText(grdSales, grdSales.ActiveRow, ISalesType)
                txtGST.Text = GetText(grdSales, grdSales.ActiveRow, IGST)
                txtInventoryCode.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
