Public Class frmGoldBarPurchase
    Inherits System.Windows.Forms.Form
    Dim dm As New DataManager
    Dim sPurchaseNo As String
    Dim sCurrencyCode As String
    Dim sSupplierCode As String
    Dim objCurrency As New clsCurrency
    Dim objPurchase As New clsGoldBarPurchase
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
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents grdGoldBar As AxFPSpread.AxvaSpread
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents txtCurrencyName As System.Windows.Forms.TextBox
    Friend WithEvents txtsupplierName As System.Windows.Forms.TextBox
    Friend WithEvents txtInvoiceNo As System.Windows.Forms.TextBox
    Friend WithEvents txtCurrencyCode As AxEditLib.AxfpText
    Friend WithEvents txtsupplierCode As AxEditLib.AxfpText
    Friend WithEvents txtPurchaseNo As AxEditLib.AxfpText
    Friend WithEvents txtGST As AxEditLib.AxfpDoubleSingle
    Friend WithEvents txtTotalCost As AxEditLib.AxfpDoubleSingle
    Friend WithEvents txtTotalweight As AxEditLib.AxfpDoubleSingle
    Friend WithEvents txtQty As AxEditLib.AxfpDoubleSingle
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtClassCode As System.Windows.Forms.TextBox
    Friend WithEvents txtCurrencyRate As System.Windows.Forms.TextBox
    Friend WithEvents txtYYYY As System.Windows.Forms.TextBox
    Friend WithEvents txtMM As System.Windows.Forms.TextBox
    Friend WithEvents txtDD As System.Windows.Forms.TextBox
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmGoldBarPurchase))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtCurrencyRate = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtYYYY = New System.Windows.Forms.TextBox
        Me.txtMM = New System.Windows.Forms.TextBox
        Me.txtDD = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtRemarks = New System.Windows.Forms.TextBox
        Me.txtCurrencyName = New System.Windows.Forms.TextBox
        Me.txtsupplierName = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtCurrencyCode = New AxEditLib.AxfpText
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtsupplierCode = New AxEditLib.AxfpText
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtPurchaseNo = New AxEditLib.AxfpText
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.grdGoldBar = New AxFPSpread.AxvaSpread
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.txtGST = New AxEditLib.AxfpDoubleSingle
        Me.Label17 = New System.Windows.Forms.Label
        Me.txtTotalCost = New AxEditLib.AxfpDoubleSingle
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtTotalweight = New AxEditLib.AxfpDoubleSingle
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtQty = New AxEditLib.AxfpDoubleSingle
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtDescription = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtClassCode = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.btnNew = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.pic = New System.Windows.Forms.PictureBox
        Me.GroupBox1.SuspendLayout()
        CType(Me.txtCurrencyCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtsupplierCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPurchaseNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdGoldBar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.txtGST, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalweight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQty, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtCurrencyRate)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtYYYY)
        Me.GroupBox1.Controls.Add(Me.txtMM)
        Me.GroupBox1.Controls.Add(Me.txtDD)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtRemarks)
        Me.GroupBox1.Controls.Add(Me.txtCurrencyName)
        Me.GroupBox1.Controls.Add(Me.txtsupplierName)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtInvoiceNo)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtCurrencyCode)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtsupplierCode)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtPurchaseNo)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 80)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(768, 160)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'txtCurrencyRate
        '
        Me.txtCurrencyRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrencyRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencyRate.Location = New System.Drawing.Point(680, 84)
        Me.txtCurrencyRate.MaxLength = 20
        Me.txtCurrencyRate.Name = "txtCurrencyRate"
        Me.txtCurrencyRate.Size = New System.Drawing.Size(72, 20)
        Me.txtCurrencyRate.TabIndex = 10
        Me.txtCurrencyRate.Tag = "NE;"
        Me.txtCurrencyRate.Text = ""
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(635, 86)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(32, 16)
        Me.Label11.TabIndex = 47
        Me.Label11.Text = "Rate"
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(692, 22)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(8, 16)
        Me.Label10.TabIndex = 46
        Me.Label10.Text = "/"
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(653, 22)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(8, 16)
        Me.Label9.TabIndex = 45
        Me.Label9.Text = "/"
        '
        'txtYYYY
        '
        Me.txtYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtYYYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtYYYY.Location = New System.Drawing.Point(704, 20)
        Me.txtYYYY.MaxLength = 20
        Me.txtYYYY.Name = "txtYYYY"
        Me.txtYYYY.Size = New System.Drawing.Size(48, 20)
        Me.txtYYYY.TabIndex = 5
        Me.txtYYYY.Tag = "NE;"
        Me.txtYYYY.Text = ""
        '
        'txtMM
        '
        Me.txtMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMM.Location = New System.Drawing.Point(664, 20)
        Me.txtMM.MaxLength = 20
        Me.txtMM.Name = "txtMM"
        Me.txtMM.Size = New System.Drawing.Size(24, 20)
        Me.txtMM.TabIndex = 4
        Me.txtMM.Tag = "NE;"
        Me.txtMM.Text = ""
        '
        'txtDD
        '
        Me.txtDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDD.Location = New System.Drawing.Point(624, 20)
        Me.txtDD.MaxLength = 20
        Me.txtDD.Name = "txtDD"
        Me.txtDD.Size = New System.Drawing.Size(25, 20)
        Me.txtDD.TabIndex = 3
        Me.txtDD.Tag = "NE;"
        Me.txtDD.Text = ""
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(544, 20)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(72, 16)
        Me.Label8.TabIndex = 41
        Me.Label8.Text = "Invoice Date"
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(128, 124)
        Me.txtRemarks.MaxLength = 20
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(624, 20)
        Me.txtRemarks.TabIndex = 11
        Me.txtRemarks.Tag = "NE;"
        Me.txtRemarks.Text = ""
        '
        'txtCurrencyName
        '
        Me.txtCurrencyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrencyName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencyName.Location = New System.Drawing.Point(416, 84)
        Me.txtCurrencyName.MaxLength = 20
        Me.txtCurrencyName.Name = "txtCurrencyName"
        Me.txtCurrencyName.Size = New System.Drawing.Size(208, 20)
        Me.txtCurrencyName.TabIndex = 9
        Me.txtCurrencyName.Tag = "NE;"
        Me.txtCurrencyName.Text = ""
        '
        'txtsupplierName
        '
        Me.txtsupplierName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtsupplierName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsupplierName.Location = New System.Drawing.Point(416, 52)
        Me.txtsupplierName.MaxLength = 20
        Me.txtsupplierName.Name = "txtsupplierName"
        Me.txtsupplierName.Size = New System.Drawing.Size(336, 20)
        Me.txtsupplierName.TabIndex = 7
        Me.txtsupplierName.Tag = "NE;"
        Me.txtsupplierName.Text = ""
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(304, 88)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(88, 16)
        Me.Label7.TabIndex = 37
        Me.Label7.Text = "Currency Name"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(304, 55)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 16)
        Me.Label6.TabIndex = 36
        Me.Label6.Text = "Supplier Name"
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvoiceNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceNo.Location = New System.Drawing.Point(416, 20)
        Me.txtInvoiceNo.MaxLength = 20
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(112, 20)
        Me.txtInvoiceNo.TabIndex = 2
        Me.txtInvoiceNo.Tag = "NE;"
        Me.txtInvoiceNo.Text = ""
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(304, 20)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 16)
        Me.Label5.TabIndex = 34
        Me.Label5.Text = "Invoice No"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(16, 128)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 16)
        Me.Label4.TabIndex = 33
        Me.Label4.Text = "Remarks"
        '
        'txtCurrencyCode
        '
        Me.txtCurrencyCode.ContainingControl = Me
        Me.txtCurrencyCode.Location = New System.Drawing.Point(128, 84)
        Me.txtCurrencyCode.Name = "txtCurrencyCode"
        Me.txtCurrencyCode.OcxState = CType(resources.GetObject("txtCurrencyCode.OcxState"), System.Windows.Forms.AxHost.State)
        Me.txtCurrencyCode.Size = New System.Drawing.Size(152, 24)
        Me.txtCurrencyCode.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 90)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 16)
        Me.Label3.TabIndex = 31
        Me.Label3.Text = "Currency Code"
        '
        'txtsupplierCode
        '
        Me.txtsupplierCode.ContainingControl = Me
        Me.txtsupplierCode.Location = New System.Drawing.Point(128, 52)
        Me.txtsupplierCode.Name = "txtsupplierCode"
        Me.txtsupplierCode.OcxState = CType(resources.GetObject("txtsupplierCode.OcxState"), System.Windows.Forms.AxHost.State)
        Me.txtsupplierCode.Size = New System.Drawing.Size(152, 24)
        Me.txtsupplierCode.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 16)
        Me.Label2.TabIndex = 29
        Me.Label2.Text = "Supplier Code"
        '
        'txtPurchaseNo
        '
        Me.txtPurchaseNo.ContainingControl = Me
        Me.txtPurchaseNo.Location = New System.Drawing.Point(128, 17)
        Me.txtPurchaseNo.Name = "txtPurchaseNo"
        Me.txtPurchaseNo.OcxState = CType(resources.GetObject("txtPurchaseNo.OcxState"), System.Windows.Forms.AxHost.State)
        Me.txtPurchaseNo.Size = New System.Drawing.Size(152, 24)
        Me.txtPurchaseNo.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 16)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Bar Purchase No"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.grdGoldBar)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(24, 400)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(760, 176)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "GoldBar Purchase Details"
        Me.GroupBox2.Visible = False
        '
        'grdGoldBar
        '
        Me.grdGoldBar.ContainingControl = Me
        Me.grdGoldBar.Location = New System.Drawing.Point(24, 19)
        Me.grdGoldBar.Name = "grdGoldBar"
        Me.grdGoldBar.OcxState = CType(resources.GetObject("grdGoldBar.OcxState"), System.Windows.Forms.AxHost.State)
        Me.grdGoldBar.Size = New System.Drawing.Size(712, 148)
        Me.grdGoldBar.TabIndex = 36
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtGST)
        Me.GroupBox3.Controls.Add(Me.Label17)
        Me.GroupBox3.Controls.Add(Me.txtTotalCost)
        Me.GroupBox3.Controls.Add(Me.Label16)
        Me.GroupBox3.Controls.Add(Me.txtTotalweight)
        Me.GroupBox3.Controls.Add(Me.Label15)
        Me.GroupBox3.Controls.Add(Me.txtQty)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Controls.Add(Me.txtDescription)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.txtClassCode)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Location = New System.Drawing.Point(8, 243)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(768, 72)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        '
        'txtGST
        '
        Me.txtGST.ContainingControl = Me
        Me.txtGST.Location = New System.Drawing.Point(672, 37)
        Me.txtGST.Name = "txtGST"
        Me.txtGST.OcxState = CType(resources.GetObject("txtGST.OcxState"), System.Windows.Forms.AxHost.State)
        Me.txtGST.Size = New System.Drawing.Size(72, 24)
        Me.txtGST.TabIndex = 17
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(680, 16)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(58, 16)
        Me.Label17.TabIndex = 46
        Me.Label17.Text = "Total GST"
        '
        'txtTotalCost
        '
        Me.txtTotalCost.ContainingControl = Me
        Me.txtTotalCost.Location = New System.Drawing.Point(576, 37)
        Me.txtTotalCost.Name = "txtTotalCost"
        Me.txtTotalCost.OcxState = CType(resources.GetObject("txtTotalCost.OcxState"), System.Windows.Forms.AxHost.State)
        Me.txtTotalCost.Size = New System.Drawing.Size(72, 24)
        Me.txtTotalCost.TabIndex = 16
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(584, 16)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(67, 16)
        Me.Label16.TabIndex = 44
        Me.Label16.Text = "Total Cost"
        '
        'txtTotalweight
        '
        Me.txtTotalweight.ContainingControl = Me
        Me.txtTotalweight.Location = New System.Drawing.Point(496, 38)
        Me.txtTotalweight.Name = "txtTotalweight"
        Me.txtTotalweight.OcxState = CType(resources.GetObject("txtTotalweight.OcxState"), System.Windows.Forms.AxHost.State)
        Me.txtTotalweight.Size = New System.Drawing.Size(72, 23)
        Me.txtTotalweight.TabIndex = 15
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(496, 16)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(74, 16)
        Me.Label15.TabIndex = 42
        Me.Label15.Text = "Total Weight"
        '
        'txtQty
        '
        Me.txtQty.ContainingControl = Me
        Me.txtQty.Location = New System.Drawing.Point(424, 37)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.OcxState = CType(resources.GetObject("txtQty.OcxState"), System.Windows.Forms.AxHost.State)
        Me.txtQty.Size = New System.Drawing.Size(52, 24)
        Me.txtQty.TabIndex = 14
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(440, 16)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(23, 16)
        Me.Label14.TabIndex = 40
        Me.Label14.Text = "Qty"
        '
        'txtDescription
        '
        Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.Location = New System.Drawing.Point(152, 39)
        Me.txtDescription.MaxLength = 20
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(256, 20)
        Me.txtDescription.TabIndex = 13
        Me.txtDescription.Tag = "NE;"
        Me.txtDescription.Text = ""
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(256, 16)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(68, 16)
        Me.Label13.TabIndex = 38
        Me.Label13.Text = "Description"
        '
        'txtClassCode
        '
        Me.txtClassCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtClassCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClassCode.Location = New System.Drawing.Point(32, 39)
        Me.txtClassCode.MaxLength = 20
        Me.txtClassCode.Name = "txtClassCode"
        Me.txtClassCode.Size = New System.Drawing.Size(112, 20)
        Me.txtClassCode.TabIndex = 12
        Me.txtClassCode.Tag = "NE;"
        Me.txtClassCode.Text = ""
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(48, 16)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(68, 16)
        Me.Label12.TabIndex = 36
        Me.Label12.Text = "Class Code"
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(460, 320)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.TabIndex = 20
        Me.btnNew.Text = "New(F6)"
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(700, 320)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.TabIndex = 21
        Me.btnClose.Text = "Close(Esc)"
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.Location = New System.Drawing.Point(540, 320)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(76, 23)
        Me.btnSave.TabIndex = 18
        Me.btnSave.Text = "Save(F7)"
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.Location = New System.Drawing.Point(620, 320)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.TabIndex = 19
        Me.btnDelete.Text = "Delete(F8)"
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(8, 3)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(210, 75)
        Me.pic.TabIndex = 54
        Me.pic.TabStop = False
        '
        'frmGoldBarPurchase
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ClientSize = New System.Drawing.Size(784, 352)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Name = "frmGoldBarPurchase"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "GoldBar Purchase"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.txtCurrencyCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtsupplierCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPurchaseNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.grdGoldBar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.txtGST, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalweight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQty, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private Sub frmGoldBarPurchase_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtDD.Text = Format(Date.Now.Day, "00")
        txtMM.Text = Format(Date.Now.Month, "00")
        txtYYYY.Text = Date.Now.Year
        subCheckCurrency()
        subgetclasscode()
    End Sub
    Private Sub subgetclasscode()
        Dim ds As DataSet
        Try
            ds = objCurrency.GetClassCodeForGoldBar
            If ds.Tables("Currency").Rows.Count = 0 Then
                MsgBox("Please set the GoldBarCode for ClassCode in class master.", MsgBoxStyle.Information)
            ElseIf ds.Tables("Currency").Rows.Count > 1 Then
                MsgBox("Please set the GoldBarCode for One ClassCode in class master.", MsgBoxStyle.Information)
            ElseIf ds.Tables("Currency").Rows.Count > 0 Then
                txtClassCode.Text = ds.Tables("Currency").Rows(0)("ClassCode")
                txtDescription.Text = ds.Tables("Currency").Rows(0)("ClassName")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub subCheckCurrency()
        Try
            If Trim(sDefaultCurrency) = "" Then
                MsgBox("Please set default currency in company master.", MsgBoxStyle.Critical)
            Else
                txtCurrencyCode.Text = sDefaultCurrency
                sCurrencyCode = txtCurrencyCode.Text
                subgetCurrency()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub subgetCurrency()
        Dim ds As DataSet
        Try
            ds = objCurrency.GetCurrency(sCurrencyCode)
            If Not IsNothing(ds) Then
                If ds.Tables("Currency").Rows.Count > 0 Then
                    txtCurrencyCode.Text = ds.Tables("Currency").Rows(0)("CurrencyCode")
                    txtCurrencyName.Text = ds.Tables("Currency").Rows(0)("CurrencyName")
                    txtCurrencyRate.Text = Format(Val(ds.Tables("Currency").Rows(0)("CurrencyRate")), "#0.00")
                Else
                    MsgBox("Please set default currency in company master.", MsgBoxStyle.Critical)
                    txtCurrencyCode.Text = ""
                    txtCurrencyName.Clear()
                    txtCurrencyRate.Clear()
                    sCurrencyCode = ""
                End If
            End If
            ds = Nothing
        Catch ex As Exception
            MsgBox(ex.Message)
            ds = Nothing
        End Try
    End Sub
    Private Sub subgetsupplierName()
        Dim ds As DataSet
        Try
            ds = objPurchase.GetSupplier(sSupplierCode)
            If Not IsNothing(ds) Then
                If ds.Tables("Supplier").Rows.Count > 0 Then
                    txtCurrencyCode.Text = ds.Tables("Supplier").Rows(0)("SupplierCode")
                    txtCurrencyName.Text = ds.Tables("Supplier").Rows(0)("SupplierName")
                Else
                    txtsupplierCode.Text = ""
                    txtsupplierName.Clear()
                    sSupplierCode = ""
                End If
            End If
            ds = Nothing
        Catch ex As Exception
            MsgBox(ex.Message)
            ds = Nothing
        End Try
    End Sub

    Private Sub txtCurrencyCode_ButtonHit(ByVal sender As Object, ByVal e As AxEditLib._DfpText_ButtonHitEvent) Handles txtCurrencyCode.ButtonHit
        Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCurrency", "L", 4, "CurrencyName", "", "CurrencyCode", "CurrencyCode", 80, "CurrencyName", "CurrencyName", 180, "CurrencyRate", "CurrencyRate", 60, "Remarks", "Remarks", 80)
        frmHelpClass.ShowDialog()
        txtCurrencyCode.Text = frmHelpClass.Var1.Trim
        txtCurrencyName.Text = frmHelpClass.Var2.Trim
        txtCurrencyRate.Text = frmHelpClass.Var3.Trim
    End Sub
    Private Sub txtsupplierCode_ButtonHit(ByVal sender As Object, ByVal e As AxEditLib._DfpText_ButtonHitEvent) Handles txtsupplierCode.ButtonHit
        Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSupplier", "L", 4, "SupplierName", "", "SupplierCode", "SupplierCode", 80, "SupplierName", "SupplierName", 180, "Address1", "Address1", 60, "Address2", "Address2", 80)
        frmHelpClass.ShowDialog()
        txtsupplierCode.Text = frmHelpClass.Var1.Trim
        txtsupplierName.Text = frmHelpClass.Var2.Trim
    End Sub
    Private Sub txtPurchaseNo_ButtonHit(ByVal sender As Object, ByVal e As AxEditLib._DfpText_ButtonHitEvent) Handles txtPurchaseNo.ButtonHit
        Dim frmHelpClass As New frmHelpSearch(0, 0, "tblGoldBarPurchase", "L", 4, "PurchaseNo", "", "PurchaseNo", "PurchaseNo", 80, "SupplierCode", "SupplierCode", 180, "InvoiceNo", "InvoiceNo", 60, "ClassCode", "ClassCode", 80)
        frmHelpClass.ShowDialog()
        txtPurchaseNo.Text = frmHelpClass.Var1.Trim
    End Sub
    Private Sub txtTotalCost_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotalCost.LostFocus
        Try
            txtGST.Text = Val(txtTotalCost.Text) * (Val(Objuser.GstPercentage) / 100)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub txtGST_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGST.LostFocus
        Try
            txtTotalCost.Text = Val(txtTotalCost.Text) + Val(txtGST.Text)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim sDate As String
            If Not fncValidate() Then Exit Sub
            If Not subCheckDuplicateBill() Then Exit Sub
            sDate = txtDD.Text.Trim & "/" & txtMM.Text.Trim & "/" & txtYYYY.Text.Trim
            If objPurchase.GoldBarPurchaseSave(txtInvoiceNo.Text, sDate, txtsupplierCode.Text.Trim, txtCurrencyCode.Text.Trim, _
                                                Val(txtCurrencyRate.Text.Trim), txtClassCode.Text.Trim, txtDescription.Text.Trim, Val(txtTotalweight.Text.Trim), Val(txtQty.Text.Trim), (txtQty.Text.Trim), (txtQty.Text.Trim), Val(txtTotalCost.Text.Trim), Val(txtGST.Text.Trim), txtRemarks.Text.Trim, sUserID) Then
                MessageBox.Show(" GoldBar Purchase Detail saved Successfully")
                ClearAll()
                txtPurchaseNo.Focus()
            Else
                MessageBox.Show("Error in Save")
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Function fncPurchaseValidation() As Boolean
        fncPurchaseValidation = False

        If txtInvoiceNo.Text.Trim = "" Then
            MsgBox("InvoiceNo cannot be blank!")
            txtInvoiceNo.Focus()
            Exit Function
        End If

        If Val(txtQty.Text) < 1 Then
            MsgBox("Quantity should be greater than or equal to one!")
            txtQty.Focus()
            Exit Function
        End If

        If Val(txtTotalweight.Text) <= 0 Then
            MsgBox("Weight should be greater than zero!")
            txtTotalweight.Focus()
            Exit Function
        End If
        If Val(txtTotalCost.Text) <= 0 Then
            MsgBox("Total Cost Must be greater than zero!")
            txtTotalCost.Focus()
            Exit Function
        End If
        fncPurchaseValidation = True
    End Function

    Private Function fncValidate() As Boolean
        Try
            fncValidate = False
            If txtInvoiceNo.Text.Trim = "" Then
                MsgBox("Invoice No. Cannot be Blank ....")
                txtInvoiceNo.Focus()
                Exit Function
            End If

            If txtDescription.Text.Trim = "" Then
                MsgBox("Description Cannot be Blank ....")
                txtDescription.Focus()
                Exit Function
            End If

            If fnDateValid(Val(txtDD.Text.Trim), Val(txtMM.Text.Trim), Val(txtYYYY.Text.Trim)) = False Then
                MsgBox("Pls Check the Purchase Date ")
                txtDD.Focus()
                Exit Function
            End If

            If txtsupplierCode.Text.Trim = "" Then
                MsgBox("Supplier cannot be blank!")
                txtsupplierCode.Focus()
                Exit Function
            End If

            If txtCurrencyCode.Text.Trim = "" Then
                MsgBox("Currency cannot be blank!")
                txtCurrencyCode.Focus()
                Exit Function
            End If

            If Val(txtCurrencyRate.Text) = 0 Then
                MsgBox("Rate must be greater than zero!")
                txtCurrencyRate.Focus()
                Exit Function
            End If
            If Val(txtQty.Text) < 1 Then
                MsgBox("Quantity should be greater than or equal to one!")
                txtQty.Focus()
                Exit Function
            End If

            If Val(txtTotalweight.Text) <= 0 Then
                MsgBox("Weight should be greater than zero!")
                txtTotalweight.Focus()
                Exit Function
            End If
            If Val(txtTotalCost.Text) <= 0 Then
                MsgBox("Total Cost Must be greater than zero!")
                txtTotalCost.Focus()
                Exit Function
            End If
            fncValidate = True
            Exit Function
        Catch ex As Exception
            MsgBox(ex.Message)
            fncValidate = False
        End Try

    End Function
    Private Function subCheckDuplicateBill() As Boolean
        Try
            Dim ds As DataSet
            ds = objPurchase.GetGoldBarPurchaseSupplierBill(txtsupplierCode.Text.Trim, txtInvoiceNo.Text.Trim)
            If ds.Tables("Purchase").Rows.Count > 0 Then
                If sPurchaseNo <> ds.Tables("Purchase").Rows(0)("PurchaseNo") Then
                    MsgBox("Duplicate Invoice No.. This Bill is Already entered - " & ds.Tables("Purchase").Rows(0)("InvoiceNo"))
                    txtInvoiceNo.Clear()
                    txtInvoiceNo.Focus()
                    subCheckDuplicateBill = False
                    Exit Function
                End If
            Else
                subCheckDuplicateBill = True
            End If
        Catch ex As Exception
            MessageBox.Show("Error in Checking Duplicate Bill")
        End Try
    End Function

    Private Sub frmGoldBarPurchase_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F6
                btnNew_Click(sender, e)
            Case Keys.F7
                btnSave_Click(sender, e)
            Case Keys.F8
                btnDelete_Click(sender, e)
            Case Keys.Escape
                btnClose_Click(sender, e)
            Case Keys.Enter
                SendKeys.Send("{Tab}")
            Case Keys.Up
                SendKeys.Send("+{Tab}")
            Case Keys.Down
                SendKeys.Send("{Tab}")

        End Select
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        ClearAll()
        subCheckCurrency()
        subgetclasscode()
        btnSave.Enabled = True
        txtPurchaseNo.Focus()
    End Sub
    Private Sub ClearAll()
        txtPurchaseNo.Text = ""
        txtInvoiceNo.Clear()
        txtsupplierCode.Text = ""
        txtsupplierName.Text = ""
        txtCurrencyCode.Text = ""
        txtCurrencyName.Text = ""
        txtCurrencyRate.Text = ""
        txtRemarks.Clear()
        txtClassCode.Clear()
        txtDescription.Clear()
        txtQty.Text = ""
        txtTotalweight.Text = ""
        txtTotalCost.Text = ""
        txtGST.Text = ""
        grdGoldBar.MaxRows = 0
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If txtPurchaseNo.Text.Trim <> "" Then
                If objPurchase.DeleteGoldBarPurchase(txtPurchaseNo.Text.Trim) Then
                    MessageBox.Show("Goldbar Purchase detail deleted.")
                    ClearAll()
                    txtPurchaseNo.Focus()
                Else
                    MessageBox.Show("Error in Deleting GoldBar Purchase")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtPurchaseNo_Advance(ByVal sender As System.Object, ByVal e As AxEditLib._DfpText_AdvanceEvent) Handles txtPurchaseNo.Advance

    End Sub

    Private Sub txtPurchaseNo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPurchaseNo.LostFocus
        Dim Ds As New DataSet
        Try
            If txtPurchaseNo.Text.Trim <> "" Then
                Ds = objPurchase.GetGoldBarPurchaseWithPurchaseNo(txtPurchaseNo.Text.Trim)
                If Ds.Tables("GoldBarPurchase").Rows.Count > 0 Then
                    grdGoldBar.MaxRows = 0
                    btnSave.Enabled = False
                    txtPurchaseNo.Text = Ds.Tables("GoldBarPurchase").Rows(0)("PurchaseNo")
                    txtInvoiceNo.Text = Ds.Tables("GoldBarPurchase").Rows(0)("InvoiceNo")
                    txtYYYY.Text = Mid(Ds.Tables("GoldBarPurchase").Rows(0)("InvoiceDate"), 7, 4)
                    txtMM.Text = Format(Val(Mid(Ds.Tables("GoldBarPurchase").Rows(0)("InvoiceDate"), 4, 2)), "00")
                    txtDD.Text = Format(Val(Mid(Ds.Tables("GoldBarPurchase").Rows(0)("InvoiceDate"), 1, 2)), "00")
                    txtsupplierCode.Text = Ds.Tables("GoldBarPurchase").Rows(0)("SupplierCode")
                    sSupplierCode = Ds.Tables("GoldBarPurchase").Rows(0)("SupplierCode")
                    subgetsupplierName()
                    txtCurrencyCode.Text = Ds.Tables("GoldBarPurchase").Rows(0)("CurrencyCode")
                    sCurrencyCode = Ds.Tables("GoldBarPurchase").Rows(0)("CurrencyCode")
                    subgetCurrency()
                    txtCurrencyRate.Text = Val(Ds.Tables("GoldbarPurchase").Rows(0)("CurrencyRate"))
                    txtRemarks.Text = Ds.Tables("goldBarPurchase").Rows(0)("Remarks")

                    ' included newly
                    txtClassCode.Text = Ds.Tables("goldBarPurchase").Rows(0)("ClassCode") & ""
                    txtDescription.Text = Ds.Tables("goldBarPurchase").Rows(0)("Description") & ""
                    txtQty.Text = Val(Ds.Tables("GoldBarPurchase").Rows(0)("TotalQty"))
                    txtTotalweight.Text = Val(Ds.Tables("GoldBarPurchase").Rows(0)("TotalWeight"))
                    txtTotalCost.Text = Val(Ds.Tables("GoldBarPurchase").Rows(0)("TotalCost"))
                    txtGST.Text = Val(Ds.Tables("GoldBarPurchase").Rows(0)("GST"))

                    'fill the grid
                    'grdGoldBar.MaxRows = grdGoldBar.MaxRows + 1
                    'grdGoldBar.Row = grdGoldBar.MaxRows
                    'grdGoldBar.Col = 1
                    'grdGoldBar.Text = Ds.Tables("GoldBarPurchase").Rows(0)("PurchaseNo")
                    'grdGoldBar.Row = grdGoldBar.MaxRows
                    'grdGoldBar.Col = 2
                    'grdGoldBar.Text = Ds.Tables("GoldBarPurchase").Rows(0)("InvoiceNo")
                    'grdGoldBar.Row = grdGoldBar.MaxRows
                    'grdGoldBar.Col = 3
                    'grdGoldBar.Text = Ds.Tables("GoldBarPurchase").Rows(0)("Classcode")
                    'grdGoldBar.Row = grdGoldBar.MaxRows
                    'grdGoldBar.Col = 4
                    'grdGoldBar.Text = Ds.Tables("GoldBarPurchase").Rows(0)("TotalWeight")
                    'grdGoldBar.Row = grdGoldBar.MaxRows
                    'grdGoldBar.Col = 5
                    'grdGoldBar.Text = Ds.Tables("GoldBarPurchase").Rows(0)("IssuedWeight")
                End If
            End If
            Ds = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        End Try
    End Sub

    Private Sub txtsupplierCode_Advance(ByVal sender As System.Object, ByVal e As AxEditLib._DfpText_AdvanceEvent) Handles txtsupplierCode.Advance

    End Sub

    Private Sub txtsupplierCode_KeyDownEvent(ByVal sender As Object, ByVal e As AxEditLib._DfpText_KeyDownEvent) Handles txtsupplierCode.KeyDownEvent
        If e.keyCode = Keys.F1 Then
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSupplier", "L", 4, "SupplierName", "", "SupplierCode", "SupplierCode", 80, "SupplierName", "SupplierName", 180, "Address1", "Address1", 60, "Address2", "Address2", 80)
            frmHelpClass.ShowDialog()
            txtsupplierCode.Text = frmHelpClass.Var1.Trim
            txtsupplierName.Text = frmHelpClass.Var2.Trim
        End If
    End Sub

    Private Sub txtPurchaseNo_KeyDownEvent(ByVal sender As Object, ByVal e As AxEditLib._DfpText_KeyDownEvent) Handles txtPurchaseNo.KeyDownEvent
        If e.keyCode = Keys.F1 Then
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblGoldBarPurchase", "L", 4, "PurchaseNo", "", "PurchaseNo", "PurchaseNo", 80, "SupplierCode", "SupplierCode", 180, "InvoiceNo", "InvoiceNo", 60, "ClassCode", "ClassCode", 80)
            frmHelpClass.ShowDialog()
            txtPurchaseNo.Text = frmHelpClass.Var1.Trim
        End If
    End Sub
    Private Sub txtCurrencyCode_KeyDownEvent(ByVal sender As Object, ByVal e As AxEditLib._DfpText_KeyDownEvent) Handles txtCurrencyCode.KeyDownEvent
        If e.keyCode = Keys.F1 Then
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCurrency", "L", 4, "CurrencyName", "", "CurrencyCode", "CurrencyCode", 80, "CurrencyName", "CurrencyName", 180, "CurrencyRate", "CurrencyRate", 60, "Remarks", "Remarks", 80)
            frmHelpClass.ShowDialog()
            txtCurrencyCode.Text = frmHelpClass.Var1.Trim
            txtCurrencyName.Text = frmHelpClass.Var2.Trim
            txtCurrencyRate.Text = frmHelpClass.Var3.Trim
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
