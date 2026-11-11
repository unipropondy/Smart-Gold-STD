Public Class frmSupplierMnt
    Inherits System.Windows.Forms.Form
    Dim Dm As New DataManager
    Dim objClsSupplier As New clsSuppliers
    Const JobSheetNo = 1
    Const PurchaseBillNo = 2
    Const PurchaseBillDate = 3
    Const TotalWeight24K = 4
    Const TotalPCS = 5
    Friend WithEvents txtSupplierCode As System.Windows.Forms.TextBox
    Friend WithEvents btnSupplierCodeSearch As System.Windows.Forms.Button
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents grdPurchases As System.Windows.Forms.DataGridView
    Friend WithEvents cJobSheet As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cBillNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cPurchaseDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cPCS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grdPurchaseDetail As System.Windows.Forms.DataGridView
    Friend WithEvents cBillNoDetail As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cPurchaseDateDetail As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cItemCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cWeightDetail As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cPCSDetail As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cPriceDetail As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents optBoth As System.Windows.Forms.RadioButton
    Const TotalLocalCost = 6
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
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtSupplierName As System.Windows.Forms.TextBox
    Friend WithEvents txtMobile As System.Windows.Forms.TextBox
    Friend WithEvents txtPhone2 As System.Windows.Forms.TextBox
    Friend WithEvents txtUrl As System.Windows.Forms.TextBox
    Friend WithEvents txtTerms As System.Windows.Forms.TextBox
    Friend WithEvents txtFax As System.Windows.Forms.TextBox
    Friend WithEvents txtPhone1 As System.Windows.Forms.TextBox
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton3 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton4 As System.Windows.Forms.RadioButton
    Friend WithEvents ee As System.Windows.Forms.GroupBox
    Friend WithEvents optZero As System.Windows.Forms.RadioButton
    Friend WithEvents optInclusive As System.Windows.Forms.RadioButton
    Friend WithEvents optExclusive As System.Windows.Forms.RadioButton
    Friend WithEvents optGold As System.Windows.Forms.RadioButton
    Friend WithEvents optCash As System.Windows.Forms.RadioButton
    Friend WithEvents purchaseSummary As System.Windows.Forms.TabPage
    Friend WithEvents purchaseDetail As System.Windows.Forms.TabPage
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdDelete As System.Windows.Forms.Button
    Friend WithEvents cmdUpdate As System.Windows.Forms.Button
    Friend WithEvents txtAddress1 As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress2 As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress4 As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress3 As System.Windows.Forms.TextBox
    Friend WithEvents txtGLAccount2 As System.Windows.Forms.TextBox
    Friend WithEvents txtGLAccount1 As System.Windows.Forms.TextBox
    Friend WithEvents cmdClear As System.Windows.Forms.Button
    Friend WithEvents errP As System.Windows.Forms.ErrorProvider
    Friend WithEvents SSTabPurchase As System.Windows.Forms.TabControl
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtNricNo As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents OptFemale As System.Windows.Forms.RadioButton
    Friend WithEvents optMale As System.Windows.Forms.RadioButton
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtYYYY As System.Windows.Forms.TextBox
    Friend WithEvents txtMM As System.Windows.Forms.TextBox
    Friend WithEvents txtDD As System.Windows.Forms.TextBox
    Friend WithEvents txtPassportNo As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSupplierMnt))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtMobile = New System.Windows.Forms.TextBox()
        Me.txtPhone2 = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.OptFemale = New System.Windows.Forms.RadioButton()
        Me.optMale = New System.Windows.Forms.RadioButton()
        Me.txtNricNo = New System.Windows.Forms.TextBox()
        Me.txtAddress4 = New System.Windows.Forms.TextBox()
        Me.btnSupplierCodeSearch = New System.Windows.Forms.Button()
        Me.txtSupplierCode = New System.Windows.Forms.TextBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtYYYY = New System.Windows.Forms.TextBox()
        Me.txtMM = New System.Windows.Forms.TextBox()
        Me.txtDD = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtPassportNo = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.optExclusive = New System.Windows.Forms.RadioButton()
        Me.optInclusive = New System.Windows.Forms.RadioButton()
        Me.optZero = New System.Windows.Forms.RadioButton()
        Me.ee = New System.Windows.Forms.GroupBox()
        Me.optCash = New System.Windows.Forms.RadioButton()
        Me.optGold = New System.Windows.Forms.RadioButton()
        Me.txtAddress1 = New System.Windows.Forms.TextBox()
        Me.txtAddress2 = New System.Windows.Forms.TextBox()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtPhone1 = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtGLAccount2 = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtGLAccount1 = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtTerms = New System.Windows.Forms.TextBox()
        Me.txtFax = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtUrl = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtAddress3 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtSupplierName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.SSTabPurchase = New System.Windows.Forms.TabControl()
        Me.purchaseSummary = New System.Windows.Forms.TabPage()
        Me.grdPurchases = New System.Windows.Forms.DataGridView()
        Me.cJobSheet = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cBillNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cPurchaseDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cPCS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.purchaseDetail = New System.Windows.Forms.TabPage()
        Me.grdPurchaseDetail = New System.Windows.Forms.DataGridView()
        Me.cBillNoDetail = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cPurchaseDateDetail = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cItemCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cDescription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cWeightDetail = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cPCSDetail = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cPriceDetail = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmdClear = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdUpdate = New System.Windows.Forms.Button()
        Me.errP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.optBoth = New System.Windows.Forms.RadioButton()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.ee.SuspendLayout()
        Me.SSTabPurchase.SuspendLayout()
        Me.purchaseSummary.SuspendLayout()
        CType(Me.grdPurchases, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.purchaseDetail.SuspendLayout()
        CType(Me.grdPurchaseDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.errP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Window
        Me.GroupBox1.Controls.Add(Me.txtMobile)
        Me.GroupBox1.Controls.Add(Me.txtPhone2)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.txtNricNo)
        Me.GroupBox1.Controls.Add(Me.txtAddress4)
        Me.GroupBox1.Controls.Add(Me.btnSupplierCodeSearch)
        Me.GroupBox1.Controls.Add(Me.txtSupplierCode)
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.ee)
        Me.GroupBox1.Controls.Add(Me.txtAddress1)
        Me.GroupBox1.Controls.Add(Me.txtAddress2)
        Me.GroupBox1.Controls.Add(Me.txtEmail)
        Me.GroupBox1.Controls.Add(Me.txtRemarks)
        Me.GroupBox1.Controls.Add(Me.txtPhone1)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.txtGLAccount2)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.txtGLAccount1)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtTerms)
        Me.GroupBox1.Controls.Add(Me.txtFax)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtUrl)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtAddress3)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtSupplierName)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(6, 92)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(904, 288)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'txtMobile
        '
        Me.txtMobile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMobile.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMobile.Location = New System.Drawing.Point(400, 144)
        Me.txtMobile.MaxLength = 50
        Me.txtMobile.Name = "txtMobile"
        Me.txtMobile.Size = New System.Drawing.Size(182, 22)
        Me.txtMobile.TabIndex = 11
        '
        'txtPhone2
        '
        Me.txtPhone2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPhone2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPhone2.Location = New System.Drawing.Point(400, 112)
        Me.txtPhone2.MaxLength = 50
        Me.txtPhone2.Name = "txtPhone2"
        Me.txtPhone2.Size = New System.Drawing.Size(182, 22)
        Me.txtPhone2.TabIndex = 10
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.OptFemale)
        Me.GroupBox2.Controls.Add(Me.optMale)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.Black
        Me.GroupBox2.Location = New System.Drawing.Point(400, 200)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(184, 32)
        Me.GroupBox2.TabIndex = 13
        Me.GroupBox2.TabStop = False
        '
        'OptFemale
        '
        Me.OptFemale.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptFemale.Location = New System.Drawing.Point(72, 9)
        Me.OptFemale.Name = "OptFemale"
        Me.OptFemale.Size = New System.Drawing.Size(74, 20)
        Me.OptFemale.TabIndex = 15
        Me.OptFemale.Text = "Female"
        '
        'optMale
        '
        Me.optMale.Checked = True
        Me.optMale.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optMale.Location = New System.Drawing.Point(11, 10)
        Me.optMale.Name = "optMale"
        Me.optMale.Size = New System.Drawing.Size(141, 16)
        Me.optMale.TabIndex = 14
        Me.optMale.TabStop = True
        Me.optMale.Text = "Male"
        '
        'txtNricNo
        '
        Me.txtNricNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtNricNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNricNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNricNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtNricNo.Location = New System.Drawing.Point(126, 204)
        Me.txtNricNo.MaxLength = 20
        Me.txtNricNo.Name = "txtNricNo"
        Me.txtNricNo.Size = New System.Drawing.Size(178, 22)
        Me.txtNricNo.TabIndex = 6
        '
        'txtAddress4
        '
        Me.txtAddress4.BackColor = System.Drawing.SystemColors.Window
        Me.txtAddress4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtAddress4.Location = New System.Drawing.Point(126, 176)
        Me.txtAddress4.MaxLength = 50
        Me.txtAddress4.Name = "txtAddress4"
        Me.txtAddress4.Size = New System.Drawing.Size(178, 22)
        Me.txtAddress4.TabIndex = 5
        '
        'btnSupplierCodeSearch
        '
        Me.btnSupplierCodeSearch.BackColor = System.Drawing.Color.Orange
        Me.btnSupplierCodeSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSupplierCodeSearch.Location = New System.Drawing.Point(272, 15)
        Me.btnSupplierCodeSearch.Name = "btnSupplierCodeSearch"
        Me.btnSupplierCodeSearch.Size = New System.Drawing.Size(32, 22)
        Me.btnSupplierCodeSearch.TabIndex = 577
        Me.btnSupplierCodeSearch.Text = "..."
        Me.btnSupplierCodeSearch.UseVisualStyleBackColor = False
        '
        'txtSupplierCode
        '
        Me.txtSupplierCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSupplierCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierCode.Location = New System.Drawing.Point(126, 15)
        Me.txtSupplierCode.MaxLength = 50
        Me.txtSupplierCode.Name = "txtSupplierCode"
        Me.txtSupplierCode.Size = New System.Drawing.Size(148, 22)
        Me.txtSupplierCode.TabIndex = 576
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.White
        Me.GroupBox4.Controls.Add(Me.Label20)
        Me.GroupBox4.Controls.Add(Me.Label19)
        Me.GroupBox4.Controls.Add(Me.txtYYYY)
        Me.GroupBox4.Controls.Add(Me.txtMM)
        Me.GroupBox4.Controls.Add(Me.txtDD)
        Me.GroupBox4.Controls.Add(Me.Label18)
        Me.GroupBox4.Controls.Add(Me.Label17)
        Me.GroupBox4.Controls.Add(Me.txtPassportNo)
        Me.GroupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.GroupBox4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.GroupBox4.Location = New System.Drawing.Point(568, 232)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(328, 48)
        Me.GroupBox4.TabIndex = 575
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Foreigner"
        '
        'Label20
        '
        Me.Label20.Location = New System.Drawing.Point(223, 16)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(8, 16)
        Me.Label20.TabIndex = 476239
        Me.Label20.Text = "/"
        '
        'Label19
        '
        Me.Label19.Location = New System.Drawing.Point(261, 16)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(8, 16)
        Me.Label19.TabIndex = 476238
        Me.Label19.Text = "/"
        '
        'txtYYYY
        '
        Me.txtYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtYYYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtYYYY.Location = New System.Drawing.Point(272, 13)
        Me.txtYYYY.MaxLength = 4
        Me.txtYYYY.Name = "txtYYYY"
        Me.txtYYYY.Size = New System.Drawing.Size(48, 20)
        Me.txtYYYY.TabIndex = 27
        Me.txtYYYY.Tag = "NE;"
        '
        'txtMM
        '
        Me.txtMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMM.Location = New System.Drawing.Point(237, 13)
        Me.txtMM.MaxLength = 2
        Me.txtMM.Name = "txtMM"
        Me.txtMM.Size = New System.Drawing.Size(24, 20)
        Me.txtMM.TabIndex = 26
        Me.txtMM.Tag = "NE;"
        '
        'txtDD
        '
        Me.txtDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDD.Location = New System.Drawing.Point(198, 13)
        Me.txtDD.MaxLength = 2
        Me.txtDD.Name = "txtDD"
        Me.txtDD.Size = New System.Drawing.Size(25, 20)
        Me.txtDD.TabIndex = 25
        Me.txtDD.Tag = "NE;"
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(172, 16)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(32, 23)
        Me.Label18.TabIndex = 576
        Me.Label18.Text = "DOB"
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(3, 16)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(53, 24)
        Me.Label17.TabIndex = 575
        Me.Label17.Text = "Passport No :"
        '
        'txtPassportNo
        '
        Me.txtPassportNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPassportNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassportNo.Location = New System.Drawing.Point(62, 13)
        Me.txtPassportNo.MaxLength = 20
        Me.txtPassportNo.Name = "txtPassportNo"
        Me.txtPassportNo.Size = New System.Drawing.Size(106, 22)
        Me.txtPassportNo.TabIndex = 24
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(328, 208)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(66, 23)
        Me.Label16.TabIndex = 574
        Me.Label16.Text = "Gender"
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(24, 204)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(174, 23)
        Me.Label15.TabIndex = 572
        Me.Label15.Text = "BR No"
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(24, 176)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(174, 23)
        Me.Label14.TabIndex = 570
        Me.Label14.Text = "Nationality"
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.White
        Me.GroupBox3.Controls.Add(Me.optExclusive)
        Me.GroupBox3.Controls.Add(Me.optInclusive)
        Me.GroupBox3.Controls.Add(Me.optZero)
        Me.GroupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.GroupBox3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.Black
        Me.GroupBox3.Location = New System.Drawing.Point(600, 168)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(298, 56)
        Me.GroupBox3.TabIndex = 569
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Invoice Type"
        '
        'optExclusive
        '
        Me.optExclusive.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optExclusive.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optExclusive.Location = New System.Drawing.Point(208, 24)
        Me.optExclusive.Name = "optExclusive"
        Me.optExclusive.Size = New System.Drawing.Size(88, 26)
        Me.optExclusive.TabIndex = 23
        Me.optExclusive.Text = "Exclusive"
        '
        'optInclusive
        '
        Me.optInclusive.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optInclusive.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optInclusive.Location = New System.Drawing.Point(117, 24)
        Me.optInclusive.Name = "optInclusive"
        Me.optInclusive.Size = New System.Drawing.Size(87, 26)
        Me.optInclusive.TabIndex = 22
        Me.optInclusive.Text = "Inclusive"
        '
        'optZero
        '
        Me.optZero.Checked = True
        Me.optZero.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optZero.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optZero.Location = New System.Drawing.Point(17, 24)
        Me.optZero.Name = "optZero"
        Me.optZero.Size = New System.Drawing.Size(102, 26)
        Me.optZero.TabIndex = 21
        Me.optZero.TabStop = True
        Me.optZero.Text = "Zero Rated"
        '
        'ee
        '
        Me.ee.Controls.Add(Me.optBoth)
        Me.ee.Controls.Add(Me.optCash)
        Me.ee.Controls.Add(Me.optGold)
        Me.ee.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ee.ForeColor = System.Drawing.Color.Black
        Me.ee.Location = New System.Drawing.Point(600, 112)
        Me.ee.Name = "ee"
        Me.ee.Size = New System.Drawing.Size(296, 56)
        Me.ee.TabIndex = 505
        Me.ee.TabStop = False
        Me.ee.Text = "Account Type"
        '
        'optCash
        '
        Me.optCash.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCash.Location = New System.Drawing.Point(108, 30)
        Me.optCash.Name = "optCash"
        Me.optCash.Size = New System.Drawing.Size(64, 16)
        Me.optCash.TabIndex = 20
        Me.optCash.Text = "Cash"
        '
        'optGold
        '
        Me.optGold.Checked = True
        Me.optGold.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optGold.Location = New System.Drawing.Point(14, 30)
        Me.optGold.Name = "optGold"
        Me.optGold.Size = New System.Drawing.Size(64, 16)
        Me.optGold.TabIndex = 19
        Me.optGold.TabStop = True
        Me.optGold.Text = "Gold"
        '
        'txtAddress1
        '
        Me.txtAddress1.BackColor = System.Drawing.SystemColors.Window
        Me.txtAddress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtAddress1.Location = New System.Drawing.Point(126, 72)
        Me.txtAddress1.MaxLength = 50
        Me.txtAddress1.Name = "txtAddress1"
        Me.txtAddress1.Size = New System.Drawing.Size(178, 22)
        Me.txtAddress1.TabIndex = 2
        '
        'txtAddress2
        '
        Me.txtAddress2.BackColor = System.Drawing.SystemColors.Window
        Me.txtAddress2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtAddress2.Location = New System.Drawing.Point(126, 104)
        Me.txtAddress2.MaxLength = 50
        Me.txtAddress2.Name = "txtAddress2"
        Me.txtAddress2.Size = New System.Drawing.Size(178, 22)
        Me.txtAddress2.TabIndex = 3
        '
        'txtEmail
        '
        Me.txtEmail.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmail.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtEmail.Location = New System.Drawing.Point(400, 16)
        Me.txtEmail.MaxLength = 50
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(184, 22)
        Me.txtEmail.TabIndex = 7
        '
        'txtRemarks
        '
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtRemarks.Location = New System.Drawing.Point(126, 238)
        Me.txtRemarks.MaxLength = 50
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(426, 22)
        Me.txtRemarks.TabIndex = 24
        '
        'txtPhone1
        '
        Me.txtPhone1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPhone1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPhone1.Location = New System.Drawing.Point(400, 80)
        Me.txtPhone1.MaxLength = 50
        Me.txtPhone1.Name = "txtPhone1"
        Me.txtPhone1.Size = New System.Drawing.Size(182, 22)
        Me.txtPhone1.TabIndex = 9
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(600, 48)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(96, 23)
        Me.Label12.TabIndex = 47
        Me.Label12.Text = "GL Account 2"
        '
        'txtGLAccount2
        '
        Me.txtGLAccount2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGLAccount2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGLAccount2.Location = New System.Drawing.Point(696, 48)
        Me.txtGLAccount2.MaxLength = 20
        Me.txtGLAccount2.Name = "txtGLAccount2"
        Me.txtGLAccount2.Size = New System.Drawing.Size(200, 22)
        Me.txtGLAccount2.TabIndex = 17
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(600, 16)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(96, 23)
        Me.Label13.TabIndex = 46
        Me.Label13.Text = "GL Account 1"
        '
        'txtGLAccount1
        '
        Me.txtGLAccount1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGLAccount1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGLAccount1.Location = New System.Drawing.Point(696, 16)
        Me.txtGLAccount1.MaxLength = 20
        Me.txtGLAccount1.Name = "txtGLAccount1"
        Me.txtGLAccount1.Size = New System.Drawing.Size(200, 22)
        Me.txtGLAccount1.TabIndex = 16
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(600, 80)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(96, 23)
        Me.Label9.TabIndex = 43
        Me.Label9.Text = "Terms"
        '
        'txtTerms
        '
        Me.txtTerms.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTerms.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTerms.Location = New System.Drawing.Point(696, 80)
        Me.txtTerms.MaxLength = 50
        Me.txtTerms.Name = "txtTerms"
        Me.txtTerms.Size = New System.Drawing.Size(200, 22)
        Me.txtTerms.TabIndex = 18
        '
        'txtFax
        '
        Me.txtFax.BackColor = System.Drawing.SystemColors.Window
        Me.txtFax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFax.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFax.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtFax.Location = New System.Drawing.Point(400, 176)
        Me.txtFax.MaxLength = 50
        Me.txtFax.Name = "txtFax"
        Me.txtFax.Size = New System.Drawing.Size(182, 22)
        Me.txtFax.TabIndex = 12
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(328, 176)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(195, 23)
        Me.Label10.TabIndex = 42
        Me.Label10.Text = "Fax"
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(328, 144)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(147, 23)
        Me.Label11.TabIndex = 41
        Me.Label11.Text = "Mobile"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(328, 120)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(147, 23)
        Me.Label1.TabIndex = 37
        Me.Label1.Text = "Phone 2"
        '
        'txtUrl
        '
        Me.txtUrl.BackColor = System.Drawing.SystemColors.Window
        Me.txtUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUrl.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUrl.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtUrl.Location = New System.Drawing.Point(400, 48)
        Me.txtUrl.MaxLength = 50
        Me.txtUrl.Name = "txtUrl"
        Me.txtUrl.Size = New System.Drawing.Size(184, 22)
        Me.txtUrl.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(328, 80)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(195, 23)
        Me.Label2.TabIndex = 36
        Me.Label2.Text = "Phone 1"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(24, 238)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(174, 23)
        Me.Label8.TabIndex = 35
        Me.Label8.Text = "Remarks"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(328, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(147, 24)
        Me.Label7.TabIndex = 31
        Me.Label7.Text = "EMail"
        '
        'txtAddress3
        '
        Me.txtAddress3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress3.Location = New System.Drawing.Point(126, 144)
        Me.txtAddress3.MaxLength = 50
        Me.txtAddress3.Name = "txtAddress3"
        Me.txtAddress3.Size = New System.Drawing.Size(178, 22)
        Me.txtAddress3.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(328, 48)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(163, 23)
        Me.Label6.TabIndex = 29
        Me.Label6.Text = "Web Page"
        '
        'txtSupplierName
        '
        Me.txtSupplierName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSupplierName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierName.Location = New System.Drawing.Point(126, 44)
        Me.txtSupplierName.MaxLength = 50
        Me.txtSupplierName.Name = "txtSupplierName"
        Me.txtSupplierName.Size = New System.Drawing.Size(178, 22)
        Me.txtSupplierName.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(24, 71)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(174, 24)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "Address"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(24, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(174, 23)
        Me.Label3.TabIndex = 26
        Me.Label3.Text = "Supplier name"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(24, 19)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(174, 23)
        Me.Label5.TabIndex = 25
        Me.Label5.Text = "Supplier Code"
        '
        'SSTabPurchase
        '
        Me.SSTabPurchase.Controls.Add(Me.purchaseSummary)
        Me.SSTabPurchase.Controls.Add(Me.purchaseDetail)
        Me.SSTabPurchase.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSTabPurchase.Location = New System.Drawing.Point(6, 386)
        Me.SSTabPurchase.Name = "SSTabPurchase"
        Me.SSTabPurchase.SelectedIndex = 0
        Me.SSTabPurchase.Size = New System.Drawing.Size(904, 211)
        Me.SSTabPurchase.TabIndex = 29
        '
        'purchaseSummary
        '
        Me.purchaseSummary.BackColor = System.Drawing.SystemColors.Window
        Me.purchaseSummary.Controls.Add(Me.grdPurchases)
        Me.purchaseSummary.Location = New System.Drawing.Point(4, 23)
        Me.purchaseSummary.Name = "purchaseSummary"
        Me.purchaseSummary.Size = New System.Drawing.Size(896, 184)
        Me.purchaseSummary.TabIndex = 0
        Me.purchaseSummary.Text = "Purchase Summary"
        '
        'grdPurchases
        '
        Me.grdPurchases.AllowUserToAddRows = False
        Me.grdPurchases.AllowUserToDeleteRows = False
        Me.grdPurchases.AllowUserToResizeColumns = False
        Me.grdPurchases.AllowUserToResizeRows = False
        Me.grdPurchases.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdPurchases.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdPurchases.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdPurchases.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cJobSheet, Me.cBillNo, Me.cPurchaseDate, Me.cWeight, Me.cPCS, Me.cPrice})
        Me.grdPurchases.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdPurchases.Location = New System.Drawing.Point(3, 3)
        Me.grdPurchases.Name = "grdPurchases"
        Me.grdPurchases.RowHeadersVisible = False
        Me.grdPurchases.Size = New System.Drawing.Size(889, 178)
        Me.grdPurchases.TabIndex = 88
        '
        'cJobSheet
        '
        Me.cJobSheet.HeaderText = "Job Sheet"
        Me.cJobSheet.Name = "cJobSheet"
        Me.cJobSheet.Visible = False
        '
        'cBillNo
        '
        Me.cBillNo.HeaderText = "Bill No"
        Me.cBillNo.Name = "cBillNo"
        Me.cBillNo.Width = 150
        '
        'cPurchaseDate
        '
        Me.cPurchaseDate.HeaderText = "Purchase Date"
        Me.cPurchaseDate.Name = "cPurchaseDate"
        Me.cPurchaseDate.Width = 200
        '
        'cWeight
        '
        Me.cWeight.HeaderText = "Weight"
        Me.cWeight.Name = "cWeight"
        Me.cWeight.Width = 150
        '
        'cPCS
        '
        Me.cPCS.HeaderText = "PCS"
        Me.cPCS.Name = "cPCS"
        Me.cPCS.Width = 150
        '
        'cPrice
        '
        Me.cPrice.HeaderText = "Price"
        Me.cPrice.Name = "cPrice"
        Me.cPrice.Width = 200
        '
        'purchaseDetail
        '
        Me.purchaseDetail.BackColor = System.Drawing.SystemColors.Window
        Me.purchaseDetail.Controls.Add(Me.grdPurchaseDetail)
        Me.purchaseDetail.Location = New System.Drawing.Point(4, 23)
        Me.purchaseDetail.Name = "purchaseDetail"
        Me.purchaseDetail.Size = New System.Drawing.Size(896, 184)
        Me.purchaseDetail.TabIndex = 1
        Me.purchaseDetail.Text = "Purchase Detail"
        '
        'grdPurchaseDetail
        '
        Me.grdPurchaseDetail.AllowUserToAddRows = False
        Me.grdPurchaseDetail.AllowUserToDeleteRows = False
        Me.grdPurchaseDetail.AllowUserToResizeColumns = False
        Me.grdPurchaseDetail.AllowUserToResizeRows = False
        Me.grdPurchaseDetail.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdPurchaseDetail.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdPurchaseDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdPurchaseDetail.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cBillNoDetail, Me.cPurchaseDateDetail, Me.cItemCode, Me.cDescription, Me.cWeightDetail, Me.cPCSDetail, Me.cPriceDetail})
        Me.grdPurchaseDetail.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdPurchaseDetail.Location = New System.Drawing.Point(4, 3)
        Me.grdPurchaseDetail.Name = "grdPurchaseDetail"
        Me.grdPurchaseDetail.RowHeadersVisible = False
        Me.grdPurchaseDetail.Size = New System.Drawing.Size(889, 178)
        Me.grdPurchaseDetail.TabIndex = 89
        '
        'cBillNoDetail
        '
        Me.cBillNoDetail.HeaderText = "BillNo"
        Me.cBillNoDetail.Name = "cBillNoDetail"
        '
        'cPurchaseDateDetail
        '
        Me.cPurchaseDateDetail.HeaderText = "PurchaseDate"
        Me.cPurchaseDateDetail.Name = "cPurchaseDateDetail"
        Me.cPurchaseDateDetail.Width = 120
        '
        'cItemCode
        '
        Me.cItemCode.HeaderText = "Item Code"
        Me.cItemCode.Name = "cItemCode"
        '
        'cDescription
        '
        Me.cDescription.HeaderText = "Description"
        Me.cDescription.Name = "cDescription"
        Me.cDescription.Width = 230
        '
        'cWeightDetail
        '
        Me.cWeightDetail.HeaderText = "Weight"
        Me.cWeightDetail.Name = "cWeightDetail"
        Me.cWeightDetail.Width = 110
        '
        'cPCSDetail
        '
        Me.cPCSDetail.HeaderText = "Detail"
        Me.cPCSDetail.Name = "cPCSDetail"
        Me.cPCSDetail.Width = 110
        '
        'cPriceDetail
        '
        Me.cPriceDetail.HeaderText = "Price"
        Me.cPriceDetail.Name = "cPriceDetail"
        Me.cPriceDetail.Width = 110
        '
        'cmdClear
        '
        Me.cmdClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdClear.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClear.Location = New System.Drawing.Point(700, 606)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.Size = New System.Drawing.Size(100, 40)
        Me.cmdClear.TabIndex = 27
        Me.cmdClear.Text = "Clear (F6)"
        Me.cmdClear.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.Location = New System.Drawing.Point(806, 606)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(100, 40)
        Me.cmdClose.TabIndex = 28
        Me.cmdClose.Text = "Close (Esc)"
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdDelete.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.Location = New System.Drawing.Point(594, 605)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(100, 40)
        Me.cmdDelete.TabIndex = 26
        Me.cmdDelete.Text = "Delete (F8)"
        Me.cmdDelete.UseVisualStyleBackColor = False
        '
        'cmdUpdate
        '
        Me.cmdUpdate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdUpdate.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUpdate.Location = New System.Drawing.Point(488, 606)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.Size = New System.Drawing.Size(100, 40)
        Me.cmdUpdate.TabIndex = 25
        Me.cmdUpdate.Text = "Save (F7)"
        Me.cmdUpdate.UseVisualStyleBackColor = False
        '
        'errP
        '
        Me.errP.BlinkRate = 100
        Me.errP.ContainerControl = Me
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(4, -4)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 92)
        Me.pic.TabIndex = 202
        Me.pic.TabStop = False
        '
        'optBoth
        '
        Me.optBoth.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optBoth.Location = New System.Drawing.Point(194, 30)
        Me.optBoth.Name = "optBoth"
        Me.optBoth.Size = New System.Drawing.Size(56, 16)
        Me.optBoth.TabIndex = 476259
        Me.optBoth.Text = "Both"
        '
        'frmSupplierMnt
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.CancelButton = Me.cmdClose
        Me.ClientSize = New System.Drawing.Size(947, 648)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.cmdClear)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.cmdDelete)
        Me.Controls.Add(Me.cmdUpdate)
        Me.Controls.Add(Me.SSTabPurchase)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSupplierMnt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Supplier Management"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.ee.ResumeLayout(False)
        Me.SSTabPurchase.ResumeLayout(False)
        Me.purchaseSummary.ResumeLayout(False)
        CType(Me.grdPurchases, System.ComponentModel.ISupportInitialize).EndInit()
        Me.purchaseDetail.ResumeLayout(False)
        CType(Me.grdPurchaseDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.errP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmSupplierMnt_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'subGridSetting(grdPurchases)
        'subGridSetting(grdPurchaseDetail)
        If txtSupplierCode.Text.Trim.Length <> 0 Then
            DispSupplierDetails(txtSupplierCode.Text.Trim)
        End If

    End Sub
    Public Sub DispSupplierDetails(ByVal suplCode As String)
        Dim ds As DataSet
        Dim iCnt As Integer
        Try
            If suplCode.Length > 0 Then
                ds = objClsSupplier.GetSupplierDetail(suplCode)
                If ds.Tables(0).Rows.Count > 0 Then
                    txtSupplierCode.Text = Trim(ds.Tables(0).Rows(iCnt)("suppliercode") & "")
                    txtSupplierName.Text = Trim(ds.Tables(0).Rows(iCnt)("SupplierName") & "")
                    txtAddress1.Text = Trim(ds.Tables(0).Rows(iCnt)("Address1") & "")
                    txtAddress2.Text = Trim(ds.Tables(0).Rows(iCnt)("Address2") & "")
                    txtAddress3.Text = Trim(ds.Tables(0).Rows(iCnt)("Address3") & "")
                    ''Nationality
                    txtAddress4.Text = Trim(ds.Tables(0).Rows(iCnt)("Address4") & "")
                    txtNricNo.Text = Trim(ds.Tables(0).Rows(iCnt)("NRICNo") & "")
                    txtPassportNo.Text = Trim(ds.Tables(0).Rows(iCnt)("Passportno") & "")
                    txtDD.Text = Mid(Trim(ds.Tables(0).Rows(iCnt)("DOB") & ""), 1, 2)
                    txtMM.Text = Mid(Trim(ds.Tables(0).Rows(iCnt)("DOB") & ""), 4, 2)
                    txtYYYY.Text = Mid(Trim(ds.Tables(0).Rows(0)("DOB") & ""), 7, 4)

                    txtEmail.Text = Trim(ds.Tables(0).Rows(iCnt)("eMail") & "")
                    txtUrl.Text = Trim(ds.Tables(0).Rows(iCnt)("Url") & "")
                    txtPhone1.Text = Trim(ds.Tables(0).Rows(iCnt)("Phone1") & "")
                    txtPhone2.Text = Trim(ds.Tables(0).Rows(iCnt)("Phone2") & "")
                    txtMobile.Text = Trim(ds.Tables(0).Rows(iCnt)("Mobile") & "")
                    txtFax.Text = Trim(ds.Tables(0).Rows(iCnt)("Fax") & "")
                    txtTerms.Text = Trim(ds.Tables(0).Rows(iCnt)("Terms") & "")
                    txtGLAccount1.Text = Trim(ds.Tables(0).Rows(iCnt)("GLAccount1") & "")
                    txtGLAccount2.Text = Trim(ds.Tables(0).Rows(iCnt)("GLAccount2") & "")
                    txtRemarks.Text = Trim(ds.Tables(0).Rows(iCnt)("Remarks") & "")
                    If Trim(ds.Tables(0).Rows(iCnt)("AccountType") & "") = "GL" Then
                        optGold.Checked = True
                    ElseIf Trim(ds.Tables(0).Rows(iCnt)("AccountType") & "") = "C" Then
                        optCash.Checked = True
                    ElseIf Trim(ds.Tables(0).Rows(iCnt)("AccountType") & "") = "B" Then
                        optBoth.Checked = True
                    End If
                    If Trim(ds.Tables(0).Rows(iCnt)("GstType") & "") = "E" Then
                        optExclusive.Checked = True
                    ElseIf Trim(ds.Tables(0).Rows(iCnt)("GstType") & "") = "I" Then
                        optInclusive.Checked = True
                    Else
                        optZero.Checked = True
                    End If
                    ''HAJ 
                    If UCase(Trim(ds.Tables(0).Rows(iCnt)("Gender") & "")) = "M" Then
                        optMale.Checked = True
                    Else
                        OptFemale.Checked = True
                    End If
                    'txtSupplierCode.ReadOnly = True
                End If
                subLoadPurchases()
            End If

        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
            ds = Nothing

        End Try
    End Sub
    Private Sub subLoadPurchases()
        Try
            Dim ds As DataSet
            Dim iCnt As Integer
            ds = objClsSupplier.GetSupplierPurchaseDetail(txtSupplierCode.Text.Trim)
            grdPurchases.RowCount = 0
            If ds.Tables(0).Rows.Count > 0 Then
                For iCnt = 0 To ds.Tables(0).Rows.Count - 1
                    grdPurchases.Rows.Add()
                    fnSetdgvText(grdPurchases, iCnt, 0, ds.Tables(0).Rows(iCnt)("JobSheetNo") & "")
                    fnSetdgvText(grdPurchases, iCnt, 1, ds.Tables(0).Rows(iCnt)("PurchaseBillNo") & "")
                    fnSetdgvText(grdPurchases, iCnt, 2, ds.Tables(0).Rows(iCnt)("PurchaseBillDate") & "")
                    fnSetdgvText(grdPurchases, iCnt, 3, ds.Tables(0).Rows(iCnt)("TotalWeight24K") & "")
                    fnSetdgvText(grdPurchases, iCnt, 4, ds.Tables(0).Rows(iCnt)("TotalPCS") & "")
                    fnSetdgvText(grdPurchases, iCnt, 5, ds.Tables(0).Rows(iCnt)("TotalLocalCost") & "")

                Next
            End If
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            DS = Nothing
            Err.Clear()
        End Try
    End Sub

    Private Sub subLoadPurchasesDetails(ByVal jCode As String, ByVal bNo As String, ByVal sDate As String)
        Try
            Dim ds As DataSet
            Dim iCnt As Integer
            ds = objClsSupplier.GetPurchaseDetaillByJSNo(jCode)
            grdPurchaseDetail.RowCount = 0
            If ds.Tables(0).Rows.Count > 0 Then
                For iCnt = 0 To ds.Tables(0).Rows.Count - 1
                    grdPurchaseDetail.Rows.Add()
                    fnSetdgvText(grdPurchaseDetail, iCnt, 0, bNo.Trim)
                    fnSetdgvText(grdPurchaseDetail, iCnt, 1, sDate.Trim)
                    fnSetdgvText(grdPurchaseDetail, iCnt, 2, ds.Tables(0).Rows(iCnt)("Inventorycode") & "")
                    fnSetdgvText(grdPurchaseDetail, iCnt, 3, ds.Tables(0).Rows(iCnt)("Description") & "")
                    fnSetdgvText(grdPurchaseDetail, iCnt, 4, ds.Tables(0).Rows(iCnt)("Weight") & "")
                    fnSetdgvText(grdPurchaseDetail, iCnt, 5, ds.Tables(0).Rows(iCnt)("Pcs") & "")
                    fnSetdgvText(grdPurchaseDetail, iCnt, 6, ds.Tables(0).Rows(iCnt)("TotalCost") & "")

                Next
            End If
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            DS = Nothing
            Err.Clear()
        End Try
    End Sub


    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Dispose()
    End Sub

    Private Sub cmdClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClear.Click
        clearAll()
    End Sub
    Private Sub clearAll()
        txtSupplierCode.Text = ""
        txtSupplierName.Text = ""
        txtAddress1.Text = ""
        txtAddress2.Text = ""
        txtAddress3.Text = ""
        txtAddress4.Text = ""
        txtEmail.Text = ""
        txtUrl.Text = ""
        txtPhone1.Text = ""
        txtPhone2.Text = ""
        txtMobile.Text = ""
        txtFax.Text = ""
        txtTerms.Text = ""
        txtNricNo.Text = ""
        txtGLAccount1.Text = ""
        txtGLAccount2.Text = ""
        txtRemarks.Text = ""
        txtPassportNo.Text = ""
        txtDD.Text = ""
        txtMM.Text = ""
        txtYYYY.Text = ""
        optGold.Checked = True
        optZero.Checked = True
        grdPurchaseDetail.RowCount = 0
        grdPurchases.RowCount = 0
        SSTabPurchase.SelectedIndex = 0
        txtSupplierCode.Focus()
    End Sub

    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        Dim dr As DataRow()
        If txtSupplierCode.Text.Trim <> "" Then
            If fncGetSupplierTran(txtSupplierCode.Text.Trim) = True Then
                MsgBox("Can Not Delete Supplier , Beacuse This Supplier Is Having Some Transaction", MsgBoxStyle.Information)
                Exit Sub
            Else
                If MsgBox("Do you want to Delete ?" & txtSupplierCode.Text.Trim, MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                    If Not objClsSupplier.SupplierDelete(txtSupplierCode.Text.Trim) Then
                        MsgBox("No such user!", MsgBoxStyle.Exclamation)
                    End If
                End If
                clearAll()
            End If
        Else
            MsgBox("No Supplier to delete! ", MsgBoxStyle.Exclamation)
            txtSupplierCode.Focus()
        End If
        Exit Sub
    End Sub
    Private Function fncGetSupplierTran(ByVal sCode As String) As Boolean
        Dim DvCustomer() As DataRow
        Dim DM As New DataManager
        fncGetSupplierTran = False
        DvCustomer = DM.RowReturnProjExecNonQryWithDataRow("Select * from tblInventory where SupplierCode=" & fncFormatSQLText(sCode))
        If DvCustomer.Length > 0 Then
            fncGetSupplierTran = True
        Else
            fncGetSupplierTran = False
        End If
    End Function
    Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click
        Dim sGSTType As String
        Dim sAccountType As Char
        Dim sError As String
        Dim sGender As String
        Dim sDOB As String
        sError = ""
        If fnScreenValidate() = True Then

            If optExclusive.Checked = True Then
                sGSTType = "E"
            ElseIf optInclusive.Checked = True Then
                sGSTType = "I"
            Else
                sGSTType = ""
            End If
            If optGold.Checked = True Then
                sAccountType = "G"
            ElseIf optCash.Checked = True Then
                sAccountType = "C"
            Else
                sAccountType = "B"
            End If
            ''HAJ Gender of Supplier
            sDOB = txtDD.Text.Trim & "/" & txtMM.Text.Trim & "/" & txtYYYY.Text.Trim
            If optMale.Checked = True Then
                sGender = "M"
            Else
                sGender = "F"
            End If

            If (txtTerms.Text.Trim) <> "" Then

                If objClsSupplier.fncSaveSupplier(UCase(Trim(txtSupplierCode.Text)), UCase(Trim(txtSupplierName.Text)), _
                                        Trim(txtAddress1.Text), Trim(txtAddress2.Text), _
                                        Trim(txtAddress3.Text), Trim(txtAddress4.Text), _
                                        Trim(txtPhone1.Text), Trim(txtPhone2.Text), _
                                        Trim(txtMobile.Text), Trim(txtFax.Text), _
                                        Trim(txtEmail.Text), Trim(txtUrl.Text), Trim(txtTerms.Text), Trim(sAccountType), _
                                        Trim(txtGLAccount1.Text), Trim(txtGLAccount2.Text), Trim(sGSTType), Trim(txtRemarks.Text), Trim(txtNricNo.Text), sGender, txtPassportNo.Text.Trim, sDOB, sUserID, sError) Then


                    MsgBox("Saved Successfully!", MsgBoxStyle.Information)
                    clearAll()

                Else
                    MsgBox("Error in Save/Update !", MsgBoxStyle.Exclamation)
                End If
            Else
                MsgBox("Terms should not be blank", MsgBoxStyle.Information)
                txtTerms.Focus()
            End If
        End If
        Exit Sub
    End Sub
    Private Sub frmSupplierMnt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                cmdClose_Click(sender, e)
            Case Keys.Enter
                SendKeys.Send("{tab}")
            Case Keys.F6
                cmdClear_Click(sender, e)
            Case Keys.F7
                cmdUpdate_Click(sender, e)
            Case Keys.F8
                cmdDelete_Click(sender, e)
        End Select
    End Sub
    Public Function fnScreenValidate() As Boolean
        fnScreenValidate = False
        If Len(Trim(txtSupplierCode.Text)) = 0 Then
            MsgBox("Please enter Supplier Code !", MsgBoxStyle.Critical)
            txtSupplierCode.Focus()
            Exit Function
        End If

        If Len(Trim(txtSupplierName.Text)) = 0 Then
            MsgBox("Please enter Supplier Name !", MsgBoxStyle.Critical)
            txtSupplierName.Focus()
            Exit Function
        End If
        If Len(Trim(txtAddress1.Text)) = 0 Then
            MsgBox("Please enter Address !", MsgBoxStyle.Critical)
            txtAddress1.Focus()
            Exit Function
        End If
        ''HAJ Passportno 

        If Len(Trim(txtPassportNo.Text)) <> 0 Then
            If IsDate(txtDD.Text.Trim & "/" & txtMM.Text.Trim & "/" & txtYYYY.Text.Trim) = False Then
                MessageBox.Show("Enter valid Date of Birth")
                txtDD.Focus()
                Exit Function
            End If
        End If
        fnScreenValidate = True
        Exit Function
    End Function
    Private Sub txtSupplierCode_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
        If Len(Trim(txtSupplierCode.Text)) = 0 Then
            errP.SetError(txtSupplierCode, "Compulsary!")
            txtSupplierCode.Focus()
        Else
            errP.SetError(txtSupplierCode, "")
        End If
    End Sub

    Private Sub txtSupplierName_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtSupplierName.Validating
        If Len(Trim(txtSupplierName.Text)) = 0 Then
            errP.SetError(txtSupplierName, "Compulsary!")
            txtSupplierName.Focus()
        Else
            errP.SetError(txtSupplierName, "")
        End If
    End Sub

    Private Sub txtAddress1_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtAddress1.Validating
        If Len(Trim(txtAddress1.Text)) = 0 Then
            errP.SetError(txtAddress1, "Compulsary!")
            txtAddress1.Focus()
        Else
            errP.SetError(txtAddress1, "")
        End If
    End Sub



    Private Sub grdPurchases_DblClick(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_DblClickEvent)
        SSTabPurchase.SelectedIndex = 1
        subLoadPurchasesDetails(Trim(fnGetdgvText(grdPurchases, grdPurchases.CurrentRow.Index, 0)), Trim(fnGetdgvText(grdPurchases, grdPurchases.CurrentRow.Index, 1)), Trim(fnGetdgvText(grdPurchases, grdPurchases.CurrentRow.Index, 2)))

    End Sub


    Private Sub SSTabPurchase_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SSTabPurchase.SelectedIndexChanged
        If SSTabPurchase.SelectedIndex = 1 Then
            subLoadPurchasesDetails(Trim(fnGetdgvText(grdPurchases, grdPurchases.CurrentRow.Index, 0)), Trim(fnGetdgvText(grdPurchases, grdPurchases.CurrentRow.Index, 1)), Trim(fnGetdgvText(grdPurchases, grdPurchases.CurrentRow.Index, 2)))
        End If
    End Sub

    Private Sub txtSupplierCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtSupplierCode.KeyDown
        If e.KeyCode = Keys.F1 Then btnSupplierCodeSearch_Click(sender, e)
    End Sub

    Private Sub txtSupplierCode_LostFocus(sender As Object, e As System.EventArgs) Handles txtSupplierCode.LostFocus
        If txtSupplierCode.Text.Trim.Length <> 0 Then
            DispSupplierDetails(txtSupplierCode.Text.Trim)
        End If
    End Sub


    Private Sub btnSupplierCodeSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSupplierCodeSearch.Click
        Dim frmHelpClass As New frmHelpSearch(0, 0, "tblsupplier", "L", 4, "SupplierCode", "", "SupplierCode", "SupplierCode", 80, "SupplierName", "SupplierName", 180, "Address1", "Address1", 70, "Address2", "Address2", 70)
        frmHelpClass.ShowDialog()
        txtSupplierCode.Text = frmHelpClass.Var1.Trim
        txtSupplierName.Text = frmHelpClass.Var2.Trim
        DispSupplierDetails(txtSupplierCode.Text.Trim)

    End Sub

    Private Sub txtSupplierCode_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtSupplierCode.TextChanged

    End Sub
End Class
