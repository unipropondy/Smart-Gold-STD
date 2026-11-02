Public Class frmGoldBarIssue
    Inherits System.Windows.Forms.Form
    Dim objPurchase As New clsGoldBarPurchase
    Dim dABal As Double
    Dim dPaidCost As Double
    Dim sInvoicedate As String
    Dim bProceed As Boolean
    Dim dm As New DataManager

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
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents AxvaSpread1 As AxFPSpread.AxvaSpread
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents txtInvoiceNo As System.Windows.Forms.TextBox
    Friend WithEvents txtRate As System.Windows.Forms.TextBox
    Friend WithEvents txtCurrencyName As System.Windows.Forms.TextBox
    Friend WithEvents txtCurrencyCode As AxEditLib.AxfpText
    Friend WithEvents txtSupplierCode As AxEditLib.AxfpText
    Friend WithEvents txtIssueNo As AxEditLib.AxfpText
    Friend WithEvents grdGoldIssue As AxFPSpread.AxvaSpread
    Friend WithEvents grdUnpaidBill As AxFPSpread.AxvaSpread
    Friend WithEvents txtIssued As AxEditLib.AxfpDoubleSingle
    Friend WithEvents txtPayable As AxEditLib.AxfpDoubleSingle
    Friend WithEvents txtNetIssue As AxEditLib.AxfpDoubleSingle
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmGoldBarIssue))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtNetIssue = New AxEditLib.AxfpDoubleSingle
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtIssued = New AxEditLib.AxfpDoubleSingle
        Me.txtPayable = New AxEditLib.AxfpDoubleSingle
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtRate = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtYYYY = New System.Windows.Forms.TextBox
        Me.txtMM = New System.Windows.Forms.TextBox
        Me.txtDD = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtCurrencyName = New System.Windows.Forms.TextBox
        Me.txtsupplierName = New System.Windows.Forms.TextBox
        Me.txtCurrencyCode = New AxEditLib.AxfpText
        Me.txtSupplierCode = New AxEditLib.AxfpText
        Me.txtIssueNo = New AxEditLib.AxfpText
        Me.Label12 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.grdGoldIssue = New AxFPSpread.AxvaSpread
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.AxvaSpread1 = New AxFPSpread.AxvaSpread
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.grdUnpaidBill = New AxFPSpread.AxvaSpread
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnNew = New System.Windows.Forms.Button
        Me.pic = New System.Windows.Forms.PictureBox
        Me.GroupBox1.SuspendLayout()
        CType(Me.txtNetIssue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIssued, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPayable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCurrencyCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSupplierCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIssueNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdGoldIssue, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.AxvaSpread1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        CType(Me.grdUnpaidBill, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.txtNetIssue)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txtIssued)
        Me.GroupBox1.Controls.Add(Me.txtPayable)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtInvoiceNo)
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
        Me.GroupBox1.Controls.Add(Me.txtCurrencyCode)
        Me.GroupBox1.Controls.Add(Me.txtSupplierCode)
        Me.GroupBox1.Controls.Add(Me.txtIssueNo)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(16, 80)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(640, 184)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Entry Section"
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(486, 125)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(66, 16)
        Me.Label15.TabIndex = 68
        Me.Label15.Text = "(22K - 916)"
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(488, 156)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(64, 16)
        Me.Label14.TabIndex = 67
        Me.Label14.Text = "(22K - 916)"
        '
        'txtNetIssue
        '
        Me.txtNetIssue.ContainingControl = Me
        Me.txtNetIssue.Location = New System.Drawing.Point(554, 152)
        Me.txtNetIssue.Name = "txtNetIssue"
        Me.txtNetIssue.OcxState = CType(resources.GetObject("txtNetIssue.OcxState"), System.Windows.Forms.AxHost.State)
        Me.txtNetIssue.Size = New System.Drawing.Size(76, 24)
        Me.txtNetIssue.TabIndex = 13
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(550, 112)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(82, 32)
        Me.Label11.TabIndex = 64
        Me.Label11.Text = "Net Issued Wt       (24K - 999)"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtIssued
        '
        Me.txtIssued.ContainingControl = Me
        Me.txtIssued.Location = New System.Drawing.Point(376, 152)
        Me.txtIssued.Name = "txtIssued"
        Me.txtIssued.OcxState = CType(resources.GetObject("txtIssued.OcxState"), System.Windows.Forms.AxHost.State)
        Me.txtIssued.Size = New System.Drawing.Size(104, 24)
        Me.txtIssued.TabIndex = 12
        '
        'txtPayable
        '
        Me.txtPayable.ContainingControl = Me
        Me.txtPayable.Location = New System.Drawing.Point(376, 120)
        Me.txtPayable.Name = "txtPayable"
        Me.txtPayable.OcxState = CType(resources.GetObject("txtPayable.OcxState"), System.Windows.Forms.AxHost.State)
        Me.txtPayable.Size = New System.Drawing.Size(104, 24)
        Me.txtPayable.TabIndex = 11
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(312, 156)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(48, 16)
        Me.Label7.TabIndex = 61
        Me.Label7.Text = "Issued"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(312, 125)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(48, 19)
        Me.Label6.TabIndex = 60
        Me.Label6.Text = "Payable"
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvoiceNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceNo.Location = New System.Drawing.Point(144, 123)
        Me.txtInvoiceNo.MaxLength = 20
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(152, 20)
        Me.txtInvoiceNo.TabIndex = 10
        Me.txtInvoiceNo.Tag = "NE;"
        Me.txtInvoiceNo.Text = ""
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(24, 126)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 16)
        Me.Label5.TabIndex = 58
        Me.Label5.Text = "Invoice No"
        '
        'txtRate
        '
        Me.txtRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRate.Location = New System.Drawing.Point(552, 88)
        Me.txtRate.MaxLength = 20
        Me.txtRate.Name = "txtRate"
        Me.txtRate.Size = New System.Drawing.Size(80, 20)
        Me.txtRate.TabIndex = 9
        Me.txtRate.Tag = "NE;"
        Me.txtRate.Text = ""
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(488, 93)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(32, 16)
        Me.Label4.TabIndex = 56
        Me.Label4.Text = "Rate"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(24, 96)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 16)
        Me.Label3.TabIndex = 55
        Me.Label3.Text = "Currency"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(24, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 16)
        Me.Label2.TabIndex = 54
        Me.Label2.Text = "Supplier Code"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(24, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 16)
        Me.Label1.TabIndex = 53
        Me.Label1.Text = "Issue No"
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(568, 28)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(8, 16)
        Me.Label10.TabIndex = 52
        Me.Label10.Text = "/"
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(532, 29)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(8, 16)
        Me.Label9.TabIndex = 51
        Me.Label9.Text = "/"
        '
        'txtYYYY
        '
        Me.txtYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtYYYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtYYYY.Location = New System.Drawing.Point(582, 26)
        Me.txtYYYY.MaxLength = 20
        Me.txtYYYY.Name = "txtYYYY"
        Me.txtYYYY.Size = New System.Drawing.Size(48, 20)
        Me.txtYYYY.TabIndex = 4
        Me.txtYYYY.Tag = "NE;"
        Me.txtYYYY.Text = ""
        '
        'txtMM
        '
        Me.txtMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMM.Location = New System.Drawing.Point(542, 26)
        Me.txtMM.MaxLength = 20
        Me.txtMM.Name = "txtMM"
        Me.txtMM.Size = New System.Drawing.Size(24, 20)
        Me.txtMM.TabIndex = 3
        Me.txtMM.Tag = "NE;"
        Me.txtMM.Text = ""
        '
        'txtDD
        '
        Me.txtDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDD.Location = New System.Drawing.Point(502, 26)
        Me.txtDD.MaxLength = 20
        Me.txtDD.Name = "txtDD"
        Me.txtDD.Size = New System.Drawing.Size(25, 20)
        Me.txtDD.TabIndex = 2
        Me.txtDD.Tag = "NE;"
        Me.txtDD.Text = ""
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(422, 26)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(72, 16)
        Me.Label8.TabIndex = 50
        Me.Label8.Text = "Invoice Date"
        '
        'txtCurrencyName
        '
        Me.txtCurrencyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrencyName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencyName.Location = New System.Drawing.Point(304, 91)
        Me.txtCurrencyName.MaxLength = 20
        Me.txtCurrencyName.Name = "txtCurrencyName"
        Me.txtCurrencyName.Size = New System.Drawing.Size(176, 20)
        Me.txtCurrencyName.TabIndex = 8
        Me.txtCurrencyName.Tag = "NE;"
        Me.txtCurrencyName.Text = ""
        '
        'txtsupplierName
        '
        Me.txtsupplierName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtsupplierName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsupplierName.Location = New System.Drawing.Point(304, 59)
        Me.txtsupplierName.MaxLength = 20
        Me.txtsupplierName.Name = "txtsupplierName"
        Me.txtsupplierName.Size = New System.Drawing.Size(328, 20)
        Me.txtsupplierName.TabIndex = 6
        Me.txtsupplierName.Tag = "NE;"
        Me.txtsupplierName.Text = ""
        '
        'txtCurrencyCode
        '
        Me.txtCurrencyCode.ContainingControl = Me
        Me.txtCurrencyCode.Location = New System.Drawing.Point(144, 90)
        Me.txtCurrencyCode.Name = "txtCurrencyCode"
        Me.txtCurrencyCode.OcxState = CType(resources.GetObject("txtCurrencyCode.OcxState"), System.Windows.Forms.AxHost.State)
        Me.txtCurrencyCode.Size = New System.Drawing.Size(152, 24)
        Me.txtCurrencyCode.TabIndex = 7
        '
        'txtSupplierCode
        '
        Me.txtSupplierCode.ContainingControl = Me
        Me.txtSupplierCode.Location = New System.Drawing.Point(144, 57)
        Me.txtSupplierCode.Name = "txtSupplierCode"
        Me.txtSupplierCode.OcxState = CType(resources.GetObject("txtSupplierCode.OcxState"), System.Windows.Forms.AxHost.State)
        Me.txtSupplierCode.Size = New System.Drawing.Size(152, 24)
        Me.txtSupplierCode.TabIndex = 5
        '
        'txtIssueNo
        '
        Me.txtIssueNo.ContainingControl = Me
        Me.txtIssueNo.Location = New System.Drawing.Point(144, 24)
        Me.txtIssueNo.Name = "txtIssueNo"
        Me.txtIssueNo.OcxState = CType(resources.GetObject("txtIssueNo.OcxState"), System.Windows.Forms.AxHost.State)
        Me.txtIssueNo.Size = New System.Drawing.Size(152, 24)
        Me.txtIssueNo.TabIndex = 1
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(600, 176)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(48, 8)
        Me.Label12.TabIndex = 66
        Me.Label12.Text = "Net Issued Wt"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.grdGoldIssue)
        Me.GroupBox2.Controls.Add(Me.GroupBox3)
        Me.GroupBox2.Location = New System.Drawing.Point(16, 270)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(640, 176)
        Me.GroupBox2.TabIndex = 67
        Me.GroupBox2.TabStop = False
        '
        'grdGoldIssue
        '
        Me.grdGoldIssue.ContainingControl = Me
        Me.grdGoldIssue.Location = New System.Drawing.Point(8, 16)
        Me.grdGoldIssue.Name = "grdGoldIssue"
        Me.grdGoldIssue.OcxState = CType(resources.GetObject("grdGoldIssue.OcxState"), System.Windows.Forms.AxHost.State)
        Me.grdGoldIssue.Size = New System.Drawing.Size(623, 152)
        Me.grdGoldIssue.TabIndex = 17
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.AxvaSpread1)
        Me.GroupBox3.Location = New System.Drawing.Point(0, 176)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(640, 176)
        Me.GroupBox3.TabIndex = 68
        Me.GroupBox3.TabStop = False
        '
        'AxvaSpread1
        '
        Me.AxvaSpread1.ContainingControl = Me
        Me.AxvaSpread1.Location = New System.Drawing.Point(8, 16)
        Me.AxvaSpread1.Name = "AxvaSpread1"
        Me.AxvaSpread1.OcxState = CType(resources.GetObject("AxvaSpread1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxvaSpread1.Size = New System.Drawing.Size(623, 148)
        Me.AxvaSpread1.TabIndex = 37
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.grdUnpaidBill)
        Me.GroupBox4.Location = New System.Drawing.Point(16, 448)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(640, 136)
        Me.GroupBox4.TabIndex = 68
        Me.GroupBox4.TabStop = False
        '
        'grdUnpaidBill
        '
        Me.grdUnpaidBill.ContainingControl = Me
        Me.grdUnpaidBill.Location = New System.Drawing.Point(9, 14)
        Me.grdUnpaidBill.Name = "grdUnpaidBill"
        Me.grdUnpaidBill.OcxState = CType(resources.GetObject("grdUnpaidBill.OcxState"), System.Windows.Forms.AxHost.State)
        Me.grdUnpaidBill.Size = New System.Drawing.Size(623, 114)
        Me.grdUnpaidBill.TabIndex = 18
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(581, 586)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.TabIndex = 16
        Me.btnClose.Text = "Close(Esc)"
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.Location = New System.Drawing.Point(421, 586)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(76, 23)
        Me.btnSave.TabIndex = 14
        Me.btnSave.Text = "Save(F7)"
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(501, 586)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.TabIndex = 15
        Me.btnNew.Text = "Clear(F6)"
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(16, 0)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(210, 75)
        Me.pic.TabIndex = 69
        Me.pic.TabStop = False
        '
        'frmGoldBarIssue
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.HighlightText
        Me.ClientSize = New System.Drawing.Size(664, 611)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label12)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmGoldBarIssue"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Gold Bar Issue"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.txtNetIssue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIssued, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPayable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCurrencyCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSupplierCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIssueNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.grdGoldIssue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.AxvaSpread1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.grdUnpaidBill, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmGoldBarIssue_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        subGridSetting(grdGoldIssue)
        subGridSetting(grdUnpaidBill)
        txtDD.Text = Format(Date.Now.Day, "00")
        txtMM.Text = Format(Date.Now.Month, "00")
        txtYYYY.Text = Date.Now.Year
        subGetGoldBarDetail()
        clearall()
    End Sub
    Private Sub clearall()
        txtSupplierCode.Text = ""
        txtsupplierName.Text = ""
        txtCurrencyCode.Text = ""
        txtCurrencyName.Text = ""
        txtRate.Text = ""
        txtInvoiceNo.Text = ""
        txtPayable.text = ""
        txtIssueNo.Text = ""
        txtIssued.text = "0.00"
        txtNetIssue.text = "0.00"
        'txtSupplierCode.ControlType = EditLib.ControlTypeConstants.ControlTypeNormal
        grdUnpaidBill.MaxRows = 0
        grdGoldIssue.MaxRows = 0
    End Sub
    Private Sub subGetGoldBarDetail()
        Dim Ds As DataSet
        Dim i As Long
        Try
            Ds = objPurchase.GetGoldBarDetails
            If Ds.Tables("GoldBarPurchase").Rows.Count > 0 Then
                grdGoldIssue.MaxRows = 0
                For i = 0 To Ds.Tables("GoldBarPurchase").Rows.Count - 1
                    grdGoldIssue.MaxRows += 1
                    SetText(grdGoldIssue, grdGoldIssue.MaxRows, 1, Ds.Tables("GoldBarPurchase").Rows(i)("InvoiceNo"))
                    SetText(grdGoldIssue, grdGoldIssue.MaxRows, 2, Ds.Tables("GoldBarPurchase").Rows(i)("InvoiceDate"))
                    SetText(grdGoldIssue, grdGoldIssue.MaxRows, 3, "24K-999")
                    SetText(grdGoldIssue, grdGoldIssue.MaxRows, 4, Ds.Tables("GoldBarPurchase").Rows(i)("TotalWeight"))
                    SetText(grdGoldIssue, grdGoldIssue.MaxRows, 5, Ds.Tables("GoldBarPurchase").Rows(i)("IssuedWeight"))
                    SetText(grdGoldIssue, grdGoldIssue.MaxRows, 6, Ds.Tables("GoldBarPurchase").Rows(i)("Balanceweight"))
                Next i
            End If
            Ds = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        End Try
    End Sub
    Private Sub txtSupplierCode_ButtonHit(ByVal sender As Object, ByVal e As AxEditLib._DfpText_ButtonHitEvent) Handles txtSupplierCode.ButtonHit
        Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSupplier", "L", 4, "SupplierName", "", "SupplierCode", "SupplierCode", 80, "SupplierName", "SupplierName", 180, "Address1", "Address1", 60, "Address2", "Address2", 80)
        frmHelpClass.ShowDialog()
        txtSupplierCode.Text = frmHelpClass.Var1.Trim
        txtsupplierName.Text = frmHelpClass.Var2.Trim
        subGetPurchaseBill()
    End Sub
    Private Sub subGetpurchaseBill()
        Dim Ds As DataSet
        Dim i As Long
        Try
            Ds = objPurchase.GetPurchaseBill(txtSupplierCode.Text.Trim)
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

    Private Sub txtSupplierCode_KeyDownEvent(ByVal sender As Object, ByVal e As AxEditLib._DfpText_KeyDownEvent) Handles txtSupplierCode.KeyDownEvent
        If e.keyCode = Keys.F1 Then
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSupplier", "L", 4, "SupplierName", "", "SupplierCode", "SupplierCode", 80, "SupplierName", "SupplierName", 180, "Address1", "Address1", 60, "Address2", "Address2", 80)
            frmHelpClass.ShowDialog()
            txtSupplierCode.Text = frmHelpClass.Var1.Trim
            txtsupplierName.Text = frmHelpClass.Var2.Trim
            subGetpurchaseBill()

        End If
    End Sub
    Private Sub GetCurrency()
        Dim objcurrecny As New clsCurrency
        Dim Ds As DataSet
        Try
            If txtCurrencyCode.Text.Trim <> "" Then
                Ds = objcurrecny.GetCurrency(txtCurrencyCode.Text.Trim)
                If Ds.Tables("Currency").Rows.Count > 0 Then
                    txtRate.Text = Format(Ds.Tables("Currency").Rows(0)("CurrencyRate"), "0.00")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtCurrencyCode_ButtonHit(ByVal sender As Object, ByVal e As AxEditLib._DfpText_ButtonHitEvent) Handles txtCurrencyCode.ButtonHit
        Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCurrency", "L", 4, "CurrencyName", "", "CurrencyCode", "CurrencyCode", 80, "CurrencyName", "CurrencyName", 180, "CurrencyRate", "CurrencyRate", 60, "Remarks", "Remarks", 80)
        frmHelpClass.ShowDialog()
        txtCurrencyCode.Text = frmHelpClass.Var1.Trim
        txtCurrencyName.Text = frmHelpClass.Var2.Trim
        'txtRate.Text = frmHelpClass.Var3.Trim
        GetCurrency()
    End Sub
    Private Sub txtCurrencyCode_KeyDownEvent(ByVal sender As Object, ByVal e As AxEditLib._DfpText_KeyDownEvent) Handles txtCurrencyCode.KeyDownEvent
        If e.keyCode = Keys.F1 Then
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCurrency", "L", 4, "CurrencyName", "", "CurrencyCode", "CurrencyCode", 80, "CurrencyName", "CurrencyName", 180, "CurrencyRate", "CurrencyRate", 60, "Remarks", "Remarks", 80)
            frmHelpClass.ShowDialog()
            txtCurrencyCode.Text = frmHelpClass.Var1.Trim
            txtCurrencyName.Text = frmHelpClass.Var2.Trim
            'txtRate.Text = frmHelpClass.Var3.Trim
            GetCurrency()
        End If
    End Sub

    Private Sub frmGoldBarIssue_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
        Try

            If Not fncValidation() Then Exit Sub
            bProceed = False
            ''TotPurchaseweight = Val(txtIssued.Text)
            ''Ds = objPurchase.GetPurchaseBill(txtSupplierCode.Text.Trim)
            If Ds.Tables("PurchaseBill").Rows.Count > 0 Then
                For i = 0 To Ds.Tables("PurchaseBill").Rows.Count - 1
                    If TotPurchaseweight >= Ds.Tables("PurchaseBill").Rows(i)("BalanceWeight") Then
                        TotPurchaseweight = TotPurchaseweight - Ds.Tables("PurchaseBill").Rows(i)("BalanceWeight")
                        TempPurchaseWeight = Ds.Tables("PurchaseBill").Rows(i)("BalanceWeight")
                    Else
                        TempPurchaseWeight = TotPurchaseweight
                        TotPurchaseweight = 0
                        bProceed = True
                    End If
                    If objPurchase.GoldBarIssueProceed("", Ds.Tables("PurchaseBill").Rows(i)("InvoiceNo"), Ds.Tables("PurchaseBill").Rows(i)("InvoiceDate"), txtSupplierCode.Text.Trim, Ds.Tables("PurchaseBill").Rows(i)("PurchaseWeight"), Ds.Tables("PurchaseBill").Rows(i)("PayableAmount"), TempPurchaseWeight * 0.92, TempPurchaseWeight, txtInvoiceNo.Text.Trim, sInvoicedate, "24K-999", dPaidCost, "G", sUserID) Then
                    End If
                    If bProceed Then
                        Exit For
                    End If
                Next
                sExec = "UpDate tblGoldBarPurchase Set Issuedweight = IssuedWeight + "
                sExec = sExec & Val(txtIssued) & ", BalanceWeight = BalanceWeight - "
                sExec = sExec & Val(txtNetIssue)
                sExec = sExec & " Where InvoiceNo =" & fncFormatSQLText(txtInvoiceNo.Text.Trim)
                If dm.ProjExecNonQry(sExec) Then
                    MessageBox.Show("Issued Sucessfully")
                End If
                subGetGoldBarDetail()
                subGetpurchaseBill()
                txtPayable.Text = Format((GetText(grdUnpaidBill, grdUnpaidBill.Row, 5)), "0.00")
                txtInvoiceNo.Text = ""
                txtIssued.Text = ""
                txtNetIssue.Text = ""
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
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

            If Len(Trim$(txtSupplierCode.Text)) = 0 Then
                MsgBox("Supplier cannot be blank!")
                txtSupplierCode.Focus()
                Exit Function
            Else
                Dim objSupplier As New clsSuppliers
                Dim Ds As DataSet
                Ds = objSupplier.GetSupplierDetail(txtSupplierCode.Text.Trim)
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


            If Trim(txtInvoiceNo.Text) = "" Then
                MsgBox("Invoice No. Cannot be Blank ....")
                grdGoldIssue.Focus()
                Exit Function
            End If

            If Val(txtNetIssue.Text) > dABal Then
                MsgBox("Gold Issued Cannot More Than Available Weight!!! ")
                txtIssued.Focus()
                Exit Function
            End If

            If Val(txtPayable.Text) < Val(txtIssued.Text) Then
                MsgBox("Gold Issued Cannot More Than Payable Weight!!! ")
                txtIssued.Focus()
                Exit Function
            End If

            If Val(txtNetIssue.Text) = 0 Then
                MsgBox("Gold Issued Actual Weight Should Not Be 0 !!! ")
                txtIssued.Focus()
                Exit Function
            End If

            If Val(txtIssued.Text) = 0 Then
                MsgBox("Gold Issued Converted  Weight Should Not Be 0 !!! ")
                txtIssued.Focus()
                Exit Function
            End If
            fncValidation = True
            Exit Function

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Sub grdGoldIssue_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_ClickEvent) Handles grdGoldIssue.ClickEvent
        txtInvoiceNo.Text = GetText(grdGoldIssue, grdGoldIssue.ActiveRow, 1)
        txtInvoiceNo.Focus()
    End Sub
    Private Sub txtInvoiceNo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInvoiceNo.LostFocus
        Dim Ds As DataSet
        Dim i As Long
        Try
            If txtInvoiceNo.Text.Trim <> "" Then
                Ds = objPurchase.GetGoldBarInvoice(txtInvoiceNo.Text.Trim)
                If Ds.Tables("GoldBarPurchase").Rows.Count > 0 Then
                    sInvoicedate = Ds.Tables("GoldBarPurchase").Rows(0)("InvoiceDate")
                    dABal = Ds.Tables("GoldBarPurchase").Rows(0)("Balanceweight")
                End If
                For i = 1 To grdGoldIssue.MaxRows
                    If txtInvoiceNo.Text.Trim = GetText(grdGoldIssue, i, 1) Then
                        grdGoldIssue.Row = i
                        grdGoldIssue.Col = 1
                        grdGoldIssue.Action = FPSpread.ActionConstants.ActionActiveCell
                        Exit For
                    End If
                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub grdGoldIssue_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdGoldIssue.LostFocus

    End Sub

    Private Sub txtIssued_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIssued.LostFocus
        txtNetIssue.Text = CDbl(txtIssued.Text) * 0.92
    End Sub

    Private Sub txtInvoiceNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInvoiceNo.TextChanged

    End Sub

    Private Sub txtSupplierCode_Advance(ByVal sender As System.Object, ByVal e As AxEditLib._DfpText_AdvanceEvent) Handles txtSupplierCode.Advance

    End Sub

    Private Sub txtCurrencyCode_Advance(ByVal sender As System.Object, ByVal e As AxEditLib._DfpText_AdvanceEvent) Handles txtCurrencyCode.Advance

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
