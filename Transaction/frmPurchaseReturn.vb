Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmPurchaseReturn
    Inherits System.Windows.Forms.Form
    Dim mlInvQtyOnHand&
    Dim mlPCS As Integer
    Dim msRefNo As String
    Dim sSupplierCode As String
    Dim sClassCode As String
    Dim msPurchaseType As String
    Dim iRet As Integer

    Dim DsPuRet As DataSet
    Dim Dm As New DataManager
    Const IN_CODE = 0
    Const IN_DESC = 1
    Const IN_WGHT = 2
    Const IN_GLOS = 3
    Const IN_MCHG = 4
    Const IN_CTGM = 5
    Const IN_PCES = 6
    Const IN_VALU = 7

    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents grdPurchaseReturn As System.Windows.Forms.DataGridView
    Friend WithEvents InventoryCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Weight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GoldLoss As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CostPerGram As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PCS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Value As System.Windows.Forms.DataGridViewTextBoxColumn



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
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnSupplierCodeSearch As System.Windows.Forms.Button
    Friend WithEvents txtSupplierCode As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtSupplierName As System.Windows.Forms.TextBox
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents chkHGST As System.Windows.Forms.CheckBox
    Friend WithEvents txtHGSTAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtHTotalMC As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtHTotalCost As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtRefNo As System.Windows.Forms.TextBox
    Friend WithEvents txtRYYYY As System.Windows.Forms.TextBox
    Friend WithEvents txtRMM As System.Windows.Forms.TextBox
    Friend WithEvents txtRDD As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtTotalWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalPCS As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnInventoryCodeSearch As System.Windows.Forms.Button
    Friend WithEvents txtInventoryCode As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents txtClear As System.Windows.Forms.Button
    Friend WithEvents btnReport As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnBillClear As System.Windows.Forms.Button
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtValue As System.Windows.Forms.TextBox
    Friend WithEvents txtPCS As System.Windows.Forms.TextBox
    Friend WithEvents txtCostperGram As System.Windows.Forms.TextBox
    Friend WithEvents txtMC As System.Windows.Forms.TextBox
    Friend WithEvents txtGoldLoss As System.Windows.Forms.TextBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPurchaseReturn))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtRYYYY = New System.Windows.Forms.TextBox()
        Me.txtRMM = New System.Windows.Forms.TextBox()
        Me.txtRDD = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chkHGST = New System.Windows.Forms.CheckBox()
        Me.txtHGSTAmount = New System.Windows.Forms.TextBox()
        Me.txtHTotalMC = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtHTotalCost = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtSupplierName = New System.Windows.Forms.TextBox()
        Me.btnSupplierCodeSearch = New System.Windows.Forms.Button()
        Me.txtSupplierCode = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtRefNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtTotalPCS = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtTotalWeight = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.txtClear = New System.Windows.Forms.Button()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtValue = New System.Windows.Forms.TextBox()
        Me.txtWeight = New System.Windows.Forms.TextBox()
        Me.txtPCS = New System.Windows.Forms.TextBox()
        Me.txtCostperGram = New System.Windows.Forms.TextBox()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.btnInventoryCodeSearch = New System.Windows.Forms.Button()
        Me.txtInventoryCode = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtMC = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtGoldLoss = New System.Windows.Forms.TextBox()
        Me.btnReport = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnBillClear = New System.Windows.Forms.Button()
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.grdPurchaseReturn = New System.Windows.Forms.DataGridView()
        Me.InventoryCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Description = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Weight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GoldLoss = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CostPerGram = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PCS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Value = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdPurchaseReturn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtRYYYY)
        Me.GroupBox1.Controls.Add(Me.txtRMM)
        Me.GroupBox1.Controls.Add(Me.txtRDD)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.chkHGST)
        Me.GroupBox1.Controls.Add(Me.txtHGSTAmount)
        Me.GroupBox1.Controls.Add(Me.txtHTotalMC)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.txtHTotalCost)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.txtRemarks)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.txtSupplierName)
        Me.GroupBox1.Controls.Add(Me.btnSupplierCodeSearch)
        Me.GroupBox1.Controls.Add(Me.txtSupplierCode)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtRefNo)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(5, 125)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(795, 112)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Header"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(376, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(8, 16)
        Me.Label3.TabIndex = 87
        Me.Label3.Text = "-"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(344, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(8, 16)
        Me.Label4.TabIndex = 86
        Me.Label4.Text = "-"
        '
        'txtRYYYY
        '
        Me.txtRYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRYYYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRYYYY.Location = New System.Drawing.Point(385, 24)
        Me.txtRYYYY.MaxLength = 4
        Me.txtRYYYY.Name = "txtRYYYY"
        Me.txtRYYYY.Size = New System.Drawing.Size(32, 20)
        Me.txtRYYYY.TabIndex = 4
        Me.txtRYYYY.Tag = "ENN;"
        '
        'txtRMM
        '
        Me.txtRMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRMM.Location = New System.Drawing.Point(352, 24)
        Me.txtRMM.MaxLength = 2
        Me.txtRMM.Name = "txtRMM"
        Me.txtRMM.Size = New System.Drawing.Size(20, 20)
        Me.txtRMM.TabIndex = 3
        Me.txtRMM.Tag = "ENN;"
        '
        'txtRDD
        '
        Me.txtRDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRDD.Location = New System.Drawing.Point(320, 24)
        Me.txtRDD.MaxLength = 2
        Me.txtRDD.Name = "txtRDD"
        Me.txtRDD.Size = New System.Drawing.Size(20, 20)
        Me.txtRDD.TabIndex = 2
        Me.txtRDD.Tag = "ENN;"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(232, 24)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 20)
        Me.Label7.TabIndex = 85
        Me.Label7.Text = "Return Date"
        '
        'chkHGST
        '
        Me.chkHGST.ForeColor = System.Drawing.Color.Black
        Me.chkHGST.Location = New System.Drawing.Point(554, 51)
        Me.chkHGST.Name = "chkHGST"
        Me.chkHGST.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkHGST.Size = New System.Drawing.Size(48, 16)
        Me.chkHGST.TabIndex = 81
        Me.chkHGST.Text = "GST"
        '
        'txtHGSTAmount
        '
        Me.txtHGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHGSTAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHGSTAmount.ForeColor = System.Drawing.Color.Black
        Me.txtHGSTAmount.Location = New System.Drawing.Point(634, 52)
        Me.txtHGSTAmount.MaxLength = 20
        Me.txtHGSTAmount.Name = "txtHGSTAmount"
        Me.txtHGSTAmount.ReadOnly = True
        Me.txtHGSTAmount.Size = New System.Drawing.Size(147, 20)
        Me.txtHGSTAmount.TabIndex = 26
        Me.txtHGSTAmount.Tag = ""
        '
        'txtHTotalMC
        '
        Me.txtHTotalMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHTotalMC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHTotalMC.ForeColor = System.Drawing.Color.Black
        Me.txtHTotalMC.Location = New System.Drawing.Point(634, 79)
        Me.txtHTotalMC.MaxLength = 20
        Me.txtHTotalMC.Name = "txtHTotalMC"
        Me.txtHTotalMC.ReadOnly = True
        Me.txtHTotalMC.Size = New System.Drawing.Size(147, 20)
        Me.txtHTotalMC.TabIndex = 27
        Me.txtHTotalMC.Tag = ""
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(554, 79)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(64, 15)
        Me.Label19.TabIndex = 79
        Me.Label19.Text = "Total MC"
        '
        'txtHTotalCost
        '
        Me.txtHTotalCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHTotalCost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHTotalCost.ForeColor = System.Drawing.Color.Black
        Me.txtHTotalCost.Location = New System.Drawing.Point(634, 24)
        Me.txtHTotalCost.MaxLength = 20
        Me.txtHTotalCost.Name = "txtHTotalCost"
        Me.txtHTotalCost.ReadOnly = True
        Me.txtHTotalCost.Size = New System.Drawing.Size(147, 20)
        Me.txtHTotalCost.TabIndex = 25
        Me.txtHTotalCost.Tag = ""
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(554, 27)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(64, 15)
        Me.Label17.TabIndex = 77
        Me.Label17.Text = "Total Cost"
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.Black
        Me.txtRemarks.Location = New System.Drawing.Point(96, 79)
        Me.txtRemarks.MaxLength = 20
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(448, 20)
        Me.txtRemarks.TabIndex = 8
        Me.txtRemarks.Tag = ""
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(6, 79)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(82, 15)
        Me.Label12.TabIndex = 75
        Me.Label12.Text = "Remarks"
        '
        'txtSupplierName
        '
        Me.txtSupplierName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierName.ForeColor = System.Drawing.Color.Black
        Me.txtSupplierName.Location = New System.Drawing.Point(224, 52)
        Me.txtSupplierName.MaxLength = 20
        Me.txtSupplierName.Name = "txtSupplierName"
        Me.txtSupplierName.Size = New System.Drawing.Size(320, 20)
        Me.txtSupplierName.TabIndex = 7
        Me.txtSupplierName.Tag = ""
        '
        'btnSupplierCodeSearch
        '
        Me.btnSupplierCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSupplierCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSupplierCodeSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSupplierCodeSearch.Location = New System.Drawing.Point(192, 52)
        Me.btnSupplierCodeSearch.Name = "btnSupplierCodeSearch"
        Me.btnSupplierCodeSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnSupplierCodeSearch.TabIndex = 6
        Me.btnSupplierCodeSearch.Text = "..."
        Me.btnSupplierCodeSearch.UseVisualStyleBackColor = False
        '
        'txtSupplierCode
        '
        Me.txtSupplierCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierCode.ForeColor = System.Drawing.Color.Black
        Me.txtSupplierCode.Location = New System.Drawing.Point(96, 52)
        Me.txtSupplierCode.MaxLength = 20
        Me.txtSupplierCode.Name = "txtSupplierCode"
        Me.txtSupplierCode.Size = New System.Drawing.Size(96, 20)
        Me.txtSupplierCode.TabIndex = 5
        Me.txtSupplierCode.Tag = ""
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(6, 52)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(82, 15)
        Me.Label5.TabIndex = 61
        Me.Label5.Text = "Supplier Code"
        '
        'txtRefNo
        '
        Me.txtRefNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRefNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefNo.ForeColor = System.Drawing.Color.Black
        Me.txtRefNo.Location = New System.Drawing.Point(96, 24)
        Me.txtRefNo.MaxLength = 20
        Me.txtRefNo.Name = "txtRefNo"
        Me.txtRefNo.Size = New System.Drawing.Size(120, 20)
        Me.txtRefNo.TabIndex = 1
        Me.txtRefNo.Tag = ""
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(6, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 15)
        Me.Label2.TabIndex = 47
        Me.Label2.Text = "Ref. No."
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txtTotalPCS)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.txtTotalWeight)
        Me.Panel1.Location = New System.Drawing.Point(5, 434)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(795, 32)
        Me.Panel1.TabIndex = 57
        '
        'txtTotalPCS
        '
        Me.txtTotalPCS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalPCS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalPCS.ForeColor = System.Drawing.Color.Black
        Me.txtTotalPCS.Location = New System.Drawing.Point(636, 5)
        Me.txtTotalPCS.MaxLength = 20
        Me.txtTotalPCS.Name = "txtTotalPCS"
        Me.txtTotalPCS.ReadOnly = True
        Me.txtTotalPCS.Size = New System.Drawing.Size(67, 20)
        Me.txtTotalPCS.TabIndex = 68
        Me.txtTotalPCS.Tag = ""
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(561, 7)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(64, 15)
        Me.Label8.TabIndex = 65
        Me.Label8.Text = "Total PCS"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(288, 8)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 15)
        Me.Label6.TabIndex = 64
        Me.Label6.Text = "Total Weight"
        '
        'txtTotalWeight
        '
        Me.txtTotalWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalWeight.ForeColor = System.Drawing.Color.Black
        Me.txtTotalWeight.Location = New System.Drawing.Point(373, 5)
        Me.txtTotalWeight.MaxLength = 20
        Me.txtTotalWeight.Name = "txtTotalWeight"
        Me.txtTotalWeight.ReadOnly = True
        Me.txtTotalWeight.Size = New System.Drawing.Size(67, 20)
        Me.txtTotalWeight.TabIndex = 62
        Me.txtTotalWeight.Tag = ""
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.btnAdd)
        Me.Panel2.Controls.Add(Me.btnDelete)
        Me.Panel2.Controls.Add(Me.txtClear)
        Me.Panel2.Controls.Add(Me.Label16)
        Me.Panel2.Controls.Add(Me.Label15)
        Me.Panel2.Controls.Add(Me.Label14)
        Me.Panel2.Controls.Add(Me.Label13)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.txtValue)
        Me.Panel2.Controls.Add(Me.txtWeight)
        Me.Panel2.Controls.Add(Me.txtPCS)
        Me.Panel2.Controls.Add(Me.txtCostperGram)
        Me.Panel2.Controls.Add(Me.txtDescription)
        Me.Panel2.Controls.Add(Me.btnInventoryCodeSearch)
        Me.Panel2.Controls.Add(Me.txtInventoryCode)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.txtMC)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.txtGoldLoss)
        Me.Panel2.Location = New System.Drawing.Point(5, 472)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(795, 80)
        Me.Panel2.TabIndex = 58
        '
        'btnAdd
        '
        Me.btnAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnAdd.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.Location = New System.Drawing.Point(548, 49)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 18
        Me.btnAdd.Text = "Add "
        Me.btnAdd.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(628, 49)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 19
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'txtClear
        '
        Me.txtClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClear.Location = New System.Drawing.Point(708, 49)
        Me.txtClear.Name = "txtClear"
        Me.txtClear.Size = New System.Drawing.Size(75, 23)
        Me.txtClear.TabIndex = 20
        Me.txtClear.Text = "Clear"
        Me.txtClear.UseVisualStyleBackColor = False
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(715, 8)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(48, 15)
        Me.Label16.TabIndex = 81
        Me.Label16.Text = "Value"
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(646, 8)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(40, 15)
        Me.Label15.TabIndex = 80
        Me.Label15.Text = "PCS"
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(577, 8)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(64, 15)
        Me.Label14.TabIndex = 79
        Me.Label14.Text = "Cost/Gram"
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(508, 8)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(48, 15)
        Me.Label13.TabIndex = 78
        Me.Label13.Text = "MC"
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(439, 8)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(64, 15)
        Me.Label11.TabIndex = 77
        Me.Label11.Text = "Gold Loss"
        '
        'txtValue
        '
        Me.txtValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtValue.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValue.ForeColor = System.Drawing.Color.Black
        Me.txtValue.Location = New System.Drawing.Point(715, 24)
        Me.txtValue.MaxLength = 20
        Me.txtValue.Name = "txtValue"
        Me.txtValue.Size = New System.Drawing.Size(69, 20)
        Me.txtValue.TabIndex = 17
        Me.txtValue.Tag = ""
        '
        'txtWeight
        '
        Me.txtWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeight.ForeColor = System.Drawing.Color.Black
        Me.txtWeight.Location = New System.Drawing.Point(370, 24)
        Me.txtWeight.MaxLength = 20
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.ReadOnly = True
        Me.txtWeight.Size = New System.Drawing.Size(67, 20)
        Me.txtWeight.TabIndex = 12
        Me.txtWeight.Tag = ""
        '
        'txtPCS
        '
        Me.txtPCS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPCS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPCS.ForeColor = System.Drawing.Color.Black
        Me.txtPCS.Location = New System.Drawing.Point(646, 24)
        Me.txtPCS.MaxLength = 20
        Me.txtPCS.Name = "txtPCS"
        Me.txtPCS.Size = New System.Drawing.Size(67, 20)
        Me.txtPCS.TabIndex = 16
        Me.txtPCS.Tag = ""
        '
        'txtCostperGram
        '
        Me.txtCostperGram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCostperGram.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostperGram.ForeColor = System.Drawing.Color.Black
        Me.txtCostperGram.Location = New System.Drawing.Point(577, 24)
        Me.txtCostperGram.MaxLength = 20
        Me.txtCostperGram.Name = "txtCostperGram"
        Me.txtCostperGram.ReadOnly = True
        Me.txtCostperGram.Size = New System.Drawing.Size(67, 20)
        Me.txtCostperGram.TabIndex = 15
        Me.txtCostperGram.Tag = ""
        '
        'txtDescription
        '
        Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.ForeColor = System.Drawing.Color.Black
        Me.txtDescription.Location = New System.Drawing.Point(120, 24)
        Me.txtDescription.MaxLength = 20
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReadOnly = True
        Me.txtDescription.Size = New System.Drawing.Size(248, 20)
        Me.txtDescription.TabIndex = 11
        Me.txtDescription.Tag = ""
        '
        'btnInventoryCodeSearch
        '
        Me.btnInventoryCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnInventoryCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInventoryCodeSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInventoryCodeSearch.Location = New System.Drawing.Point(94, 24)
        Me.btnInventoryCodeSearch.Name = "btnInventoryCodeSearch"
        Me.btnInventoryCodeSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnInventoryCodeSearch.TabIndex = 10
        Me.btnInventoryCodeSearch.Text = "..."
        Me.btnInventoryCodeSearch.UseVisualStyleBackColor = False
        '
        'txtInventoryCode
        '
        Me.txtInventoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInventoryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInventoryCode.ForeColor = System.Drawing.Color.Black
        Me.txtInventoryCode.Location = New System.Drawing.Point(6, 24)
        Me.txtInventoryCode.MaxLength = 20
        Me.txtInventoryCode.Name = "txtInventoryCode"
        Me.txtInventoryCode.Size = New System.Drawing.Size(88, 20)
        Me.txtInventoryCode.TabIndex = 9
        Me.txtInventoryCode.Tag = ""
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(6, 8)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(88, 15)
        Me.Label10.TabIndex = 71
        Me.Label10.Text = "Inventory Code"
        '
        'txtMC
        '
        Me.txtMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMC.ForeColor = System.Drawing.Color.Black
        Me.txtMC.Location = New System.Drawing.Point(508, 24)
        Me.txtMC.MaxLength = 20
        Me.txtMC.Name = "txtMC"
        Me.txtMC.ReadOnly = True
        Me.txtMC.Size = New System.Drawing.Size(67, 20)
        Me.txtMC.TabIndex = 14
        Me.txtMC.Tag = ""
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(370, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 15)
        Me.Label1.TabIndex = 65
        Me.Label1.Text = "Weight"
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(120, 8)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(72, 15)
        Me.Label9.TabIndex = 64
        Me.Label9.Text = "Description"
        '
        'txtGoldLoss
        '
        Me.txtGoldLoss.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldLoss.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoldLoss.ForeColor = System.Drawing.Color.Black
        Me.txtGoldLoss.Location = New System.Drawing.Point(439, 24)
        Me.txtGoldLoss.MaxLength = 20
        Me.txtGoldLoss.Name = "txtGoldLoss"
        Me.txtGoldLoss.ReadOnly = True
        Me.txtGoldLoss.Size = New System.Drawing.Size(67, 20)
        Me.txtGoldLoss.TabIndex = 13
        Me.txtGoldLoss.Tag = ""
        '
        'btnReport
        '
        Me.btnReport.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnReport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReport.Location = New System.Drawing.Point(591, 558)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(100, 40)
        Me.btnReport.TabIndex = 22
        Me.btnReport.Text = "Print (F9)"
        Me.btnReport.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(485, 558)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 40)
        Me.btnSave.TabIndex = 21
        Me.btnSave.Text = "Save (F7)"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(696, 558)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 24
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnBillClear
        '
        Me.btnBillClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnBillClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBillClear.Location = New System.Drawing.Point(379, 558)
        Me.btnBillClear.Name = "btnBillClear"
        Me.btnBillClear.Size = New System.Drawing.Size(100, 40)
        Me.btnBillClear.TabIndex = 23
        Me.btnBillClear.Text = "Clear (F6)"
        Me.btnBillClear.UseVisualStyleBackColor = False
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(5, 12)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 100)
        Me.pic.TabIndex = 122
        Me.pic.TabStop = False
        '
        'grdPurchaseReturn
        '
        Me.grdPurchaseReturn.AllowUserToAddRows = False
        Me.grdPurchaseReturn.AllowUserToDeleteRows = False
        Me.grdPurchaseReturn.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdPurchaseReturn.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdPurchaseReturn.ColumnHeadersHeight = 30
        Me.grdPurchaseReturn.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.InventoryCode, Me.Description, Me.Weight, Me.GoldLoss, Me.CostPerGram, Me.MC, Me.PCS, Me.Value})
        Me.grdPurchaseReturn.Location = New System.Drawing.Point(5, 237)
        Me.grdPurchaseReturn.Name = "grdPurchaseReturn"
        Me.grdPurchaseReturn.ReadOnly = True
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdPurchaseReturn.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdPurchaseReturn.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.grdPurchaseReturn.Size = New System.Drawing.Size(795, 195)
        Me.grdPurchaseReturn.TabIndex = 123
        '
        'InventoryCode
        '
        Me.InventoryCode.HeaderText = "InventoryCode"
        Me.InventoryCode.Name = "InventoryCode"
        Me.InventoryCode.ReadOnly = True
        '
        'Description
        '
        Me.Description.HeaderText = "Description"
        Me.Description.Name = "Description"
        Me.Description.ReadOnly = True
        Me.Description.Width = 200
        '
        'Weight
        '
        Me.Weight.HeaderText = "Weight"
        Me.Weight.Name = "Weight"
        Me.Weight.ReadOnly = True
        Me.Weight.Width = 70
        '
        'GoldLoss
        '
        Me.GoldLoss.HeaderText = "Gold Loss"
        Me.GoldLoss.Name = "GoldLoss"
        Me.GoldLoss.ReadOnly = True
        Me.GoldLoss.Width = 70
        '
        'CostPerGram
        '
        Me.CostPerGram.HeaderText = "Cost Per Gram"
        Me.CostPerGram.Name = "CostPerGram"
        Me.CostPerGram.ReadOnly = True
        Me.CostPerGram.Width = 70
        '
        'MC
        '
        Me.MC.HeaderText = "MC"
        Me.MC.Name = "MC"
        Me.MC.ReadOnly = True
        Me.MC.Width = 70
        '
        'PCS
        '
        Me.PCS.HeaderText = "PCS"
        Me.PCS.Name = "PCS"
        Me.PCS.ReadOnly = True
        Me.PCS.Width = 70
        '
        'Value
        '
        Me.Value.HeaderText = "Value"
        Me.Value.Name = "Value"
        Me.Value.ReadOnly = True
        '
        'frmPurchaseReturn
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(808, 619)
        Me.Controls.Add(Me.grdPurchaseReturn)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.btnReport)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnBillClear)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmPurchaseReturn"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Purchase Return"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdPurchaseReturn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmPurchaseReturn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''subGridSetting(grdPurchaseReturn)
        grdPurchaseReturn.Rows.Clear()
        txtRYYYY.Text = Now.Year
        txtRMM.Text = Format(Now.Month, "00")
        txtRDD.Text = Format(Now.Day, "00")
    End Sub

    Private Sub btnSupplierCodeSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSupplierCodeSearch.Click
        Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSupplier", "L", 4, "SupplierName", "", "SupplierCode", "SupplierCode", 80, "SupplierName", "SupplierName", 180, "Address1", "Address1", 60, "Address2", "Address2", 80)
        frmHelpClass.ShowDialog()
        txtSupplierCode.Text = frmHelpClass.Var1.Trim
        txtSupplierName.Text = frmHelpClass.Var2.Trim
        If txtSupplierCode.Enabled Then txtSupplierCode.Focus()
    End Sub

    Private Sub txtSupplierCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSupplierCode.TextChanged

    End Sub

    Private Sub txtSupplierCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSupplierCode.KeyDown
        If e.KeyCode = Keys.F1 Then
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSupplier", "L", 4, "SupplierName", "", "SupplierCode", "SupplierCode", 80, "SupplierName", "SupplierName", 180, "Address1", "Address1", 60, "Address2", "Address2", 80)
            frmHelpClass.ShowDialog()
            txtSupplierCode.Text = frmHelpClass.Var1.Trim
            txtSupplierName.Text = frmHelpClass.Var2.Trim
        End If
    End Sub

    Private Sub btnInventoryCodeSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInventoryCodeSearch.Click
        Try
            Dim Search As String
            If txtSupplierCode.Text.Trim <> "" Then
                Search = " QtyOnHand>=1 and SupplierCode = " & fncFormatSQLText(txtSupplierCode.Text.Trim)
            Else
                Search = " QtyOnHand>=1 "
            End If
            Dim frmHelpClass As New frmHelpSearch(0, 0, "vwInventoryStock", "L", 4, "Description", Search, "InventoryCode", "InventoryCode", 80, "Description", "Description", 180, "JobSheetNo", "JobSheetNo", 60, "PurchaseBillNo", "PurchaseBillNo", 80)
            frmHelpClass.ShowDialog()
            txtInventoryCode.Text = frmHelpClass.Var1.Trim
            If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub txtInventoryCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInventoryCode.TextChanged

    End Sub

    Private Sub txtInventoryCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInventoryCode.LostFocus
        Try
            If txtInventoryCode.Text.Trim <> "" Then
                subGetInventory()
                subGetClass()
                subSetPurchaseType()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub subGetInventory()

        Dim Ds As DataSet
        Dim Ds1 As DataSet
        Dim objInventory As New clsInventory
        Dim SQry As String
        Try

            Ds = objInventory.GetInventoryDetailTransfer(txtInventoryCode.Text.Trim)
            If Ds.Tables("tblInventory").Rows.Count > 0 Then
                If Val(Ds.Tables("tblInventory").Rows(0)("QtyOnHand") & "") > 0 Then
                    sSupplierCode = Trim(Ds.Tables("tblInventory").Rows(0)("SupplierCode") & "")
                    txtDescription.Text = Trim(Ds.Tables("tblInventory").Rows(0)("Description") & "")
                    txtWeight.Text = Val(Ds.Tables("tblInventory").Rows(0)("Weight") & "")
                    txtGoldLoss.Text = Val(Ds.Tables("tblInventory").Rows(0)("GoldLoss") & "")
                    txtCostperGram.Text = Val(Ds.Tables("tblInventory").Rows(0)("CostperGram") & "")
                    txtMC.Text = Val(Ds.Tables("tblInventory").Rows(0)("PMC") & "")
                    txtPCS.Text = CLng(Val(Ds.Tables("tblInventory").Rows(0)("Pcs") & ""))
                    txtValue.Text = Val(Ds.Tables("tblInventory").Rows(0)("TotalCost") & "")

                    mlInvQtyOnHand = Val(Ds.Tables("tblInventory").Rows(0)("QtyOnHand") & "")

                    'depends on the class lock the pcs.
                    SQry = "Select PurchaseType from tblClass where ClassCode = " & fncFormatSQLText(Trim$(Ds.Tables("tblInventory").Rows(0)("ClassCode") & ""))

                    Ds1 = Dm.getDataSetWithSqlAdapter(SQry, "Class", "Class")

                    If Ds1.Tables("Class").Rows.Count > 0 Then
                        If Trim(Ds1.Tables("Class").Rows(0)("PurchaseType") & "") = "W" Then
                            txtPCS.ReadOnly = True
                        Else
                            txtPCS.ReadOnly = False
                            txtPCS.Focus()
                        End If
                    Else
                        txtPCS.ReadOnly = True
                    End If
                Else
                    MsgBox("No Stock for this Inventory!", MsgBoxStyle.Critical)
                    txtInventoryCode.Text = ""
                    subClearFields()
                    txtInventoryCode.Focus()
                End If
            Else
                MsgBox("This Inventory Not Found... ", MsgBoxStyle.Critical)
                txtInventoryCode.Text = ""
                subClearFields()
                txtInventoryCode.Focus()
            End If

            Ds = Nothing
            Ds1 = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Sub subClearFields()
        Try
            txtInventoryCode.Clear()
            txtDescription.Clear()
            txtWeight.Clear()
            txtGoldLoss.Clear()
            txtMC.Clear()
            txtCostperGram.Clear()
            txtPCS.Clear()
            txtValue.Clear()
            txtInventoryCode.Focus()
        Catch ex As Exception
        End Try

    End Sub
    Private Sub subGetClass()
        Dim i As Long
        Dim Ds As DataSet
        Dim objClass As New ClsClass

        Try
            If txtInventoryCode.Text.Trim = "" Then Exit Sub
            Ds = objClass.GetClassForInventory(txtInventoryCode.Text.Trim)
            If Ds.Tables("Purchase").Rows.Count > 0 Then
                sClassCode = Trim(Ds.Tables("Purchase").Rows(0)("ClassCode") & "")
                Exit Sub
            End If
            Ds = Nothing
            objClass = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Private Sub subSetPurchaseType()
        Dim objCLass As New ClsClass
        Dim Ds As DataSet

        Try

            Ds = objCLass.GetClassDetail(Trim(sClassCode))

            If Ds.Tables("Class").Rows.Count > 0 Then
                msPurchaseType = Trim(Ds.Tables("Class").Rows(0)("PurchaseType") & "")
            End If

            Ds = Nothing
            objCLass = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim i As Long
            Dim lRow As Long
            Dim lPCS As Long
            Dim iRow As Integer
            Dim sMsg
            If txtInventoryCode.Text.Trim = "" Then
                MsgBox("Select InventoryCode!", MsgBoxStyle.Critical)
                txtInventoryCode.Focus()
                Exit Sub
                'ElseIf Val(txtWeight.Text) <= 0 And msPurchaseType = "W" Then
                '    MsgBox("Weight should be greater than zero!", MsgBoxStyle.Critical)
                '    Exit Sub
            ElseIf Val(txtPCS.Text) < 1 Then
                MsgBox("PCS should be greater than or equal to one!", MsgBoxStyle.Critical)
                txtPCS.Focus()
                Exit Sub
            ElseIf Val(txtPCS.Text) > Val(mlInvQtyOnHand) Then
                MsgBox("Maximum available PCS for this Inventory is " & Val(mlInvQtyOnHand), MsgBoxStyle.Critical)
                txtPCS.Focus()
                Exit Sub
            End If

            If fncInventoryCodeExist(lRow, lPCS) Then
                sMsg = MsgBox(Str(lPCS) + " PCS for this item found in Return list (Do you want to add " + Str(txtPCS.Text) + " Pieces )", vbYesNo)
                If sMsg = vbNo Then
                    Exit Sub
                End If
            End If

            'add the grid for new inv.
            If lPCS = 0 Then
                If Val(txtPCS.Text) <> 0 Then
                    iRow = grdPurchaseReturn.Rows.Add()
                    grdPurchaseReturn.Rows(iRow).HeaderCell.Value = (iRow + 1).ToString()
                    fnSetdgvText(grdPurchaseReturn, iRow, IN_CODE, txtInventoryCode.Text.Trim)
                    fnSetdgvText(grdPurchaseReturn, iRow, IN_DESC, txtDescription.Text.Trim)
                    fnSetdgvText(grdPurchaseReturn, iRow, IN_WGHT, txtWeight.Text.Trim)
                    fnSetdgvText(grdPurchaseReturn, iRow, IN_GLOS, txtGoldLoss.Text.Trim)
                    fnSetdgvText(grdPurchaseReturn, iRow, IN_MCHG, Val(txtMC.Text))
                    fnSetdgvText(grdPurchaseReturn, iRow, IN_CTGM, Val(txtCostperGram.Text))
                    fnSetdgvText(grdPurchaseReturn, iRow, IN_PCES, Val(txtPCS.Text))
                    fnSetdgvText(grdPurchaseReturn, iRow, IN_VALU, Val(txtValue.Text))
                    subTotal(sender, e)
                    subClearFields()
                Else
                    txtPCS.Focus()
                    Exit Sub
                End If
            ElseIf Val(txtPCS.Text) > (Val(mlInvQtyOnHand) - Val(lPCS)) Then
                MsgBox("Maximum available PCS for this Inventory is " & Val(mlInvQtyOnHand), MsgBoxStyle.Critical)
                txtPCS.Focus()
                Exit Sub
            Else
                fnSetdgvText(grdPurchaseReturn, lRow, IN_WGHT, _
                        Val(fnGetdgvText(grdPurchaseReturn, lRow, IN_WGHT)) + txtWeight.Text.Trim)
                fnSetdgvText(grdPurchaseReturn, lRow, IN_GLOS, _
                        Val(fnGetdgvText(grdPurchaseReturn, lRow, IN_GLOS)) + txtGoldLoss.Text.Trim)
                fnSetdgvText(grdPurchaseReturn, lRow, IN_MCHG, _
                        Val(fnGetdgvText(grdPurchaseReturn, lRow, IN_MCHG)) + Val(txtMC.Text))
                'take the new cost per gram and calculate the total value again
                fnSetdgvText(grdPurchaseReturn, lRow, IN_CTGM, Val(txtCostperGram.Text))
                fnSetdgvText(grdPurchaseReturn, lRow, IN_PCES, _
                        Val(fnGetdgvText(grdPurchaseReturn, lRow, IN_PCES)) + Val(txtPCS.Text))

                fnSetdgvText(grdPurchaseReturn, lRow, IN_VALU, _
                        (Val(fnGetdgvText(grdPurchaseReturn, lRow, IN_WGHT)) + Val(fnGetdgvText(grdPurchaseReturn, lRow, IN_GLOS))) _
                            * Val(fnGetdgvText(grdPurchaseReturn, lRow, IN_CTGM)) + Val(fnGetdgvText(grdPurchaseReturn, lRow, IN_MCHG)))
                ''grdPurchaseReturn.RowCount = lRow
                ''grdPurchaseReturn.ColumnCount = 1
                ''grdPurchaseReturn.Action = 0
                subTotal(sender, e)
                subClearFields()
            End If

            ''grdPurchaseReturn.RowCount = grdPurchaseReturn.Rows.Count
            ''grdPurchaseReturn.ColumnCount = 1
            ''grdPurchaseReturn.Action = 0

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Private Function fncInventoryCodeExist(ByRef lRow As Long, ByRef lPCS As Long) As Boolean

        Dim i As Long
        Try
            fncInventoryCodeExist = False
            For i = 0 To grdPurchaseReturn.Rows.Count - 1
                If Trim(fnGetdgvText(grdPurchaseReturn, i, IN_CODE)) = txtInventoryCode.Text.Trim Then
                    lPCS = Val(fnGetdgvText(grdPurchaseReturn, i, IN_PCES))
                    lRow = i
                    fncInventoryCodeExist = True
                    Exit Function
                End If
            Next i
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Function
    Private Sub subTotal(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim dTotal As Double
        Dim GstPer As Double
        Dim i As Long
        Dim dTotWt As Double
        Dim lTotPCS As Long
        Try
            For i = 0 To grdPurchaseReturn.Rows.Count - 1
                lTotPCS = lTotPCS + Val(fnGetdgvText(grdPurchaseReturn, i, IN_PCES))
                dTotWt = dTotWt + Val(fnGetdgvText(grdPurchaseReturn, i, IN_WGHT))
                dTotal = dTotal + Val(fnGetdgvText(grdPurchaseReturn, i, IN_VALU))
            Next i

            txtTotalPCS.Text = Format(lTotPCS, "0")
            txtTotalWeight.Text = Format(dTotWt, "0.00")

            If chkHGST.Checked And dTotal <> 0 Then
                GstPer = Val(Objuser.GstPercentage)
                txtHGSTAmount.Text = Format(GstPer * Val(Format(dTotal, "########0.00") / 100), "#####0.00")
            Else
                txtHGSTAmount.Text = ""
            End If
            txtHTotalCost.Text = Format(dTotal, "########0.00")
            txtHGSTAmount_LostFocus(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim iRow As Integer
        Try
            If grdPurchaseReturn.Rows.Count >= 0 Then
                ''grdPurchaseReturn.Row = grdPurchaseReturn.ActiveRow
                ''grdPurchaseReturn.Action = 5
                ''grdPurchaseReturn.MaxRows = grdPurchaseReturn.MaxRows - 1
                iRow = grdPurchaseReturn.CurrentRow.Index
                grdPurchaseReturn.Rows.RemoveAt(iRow)
                subTotal(sender, e)
                txtInventoryCode.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub txtClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtClear.Click
        Try
            subClear()
            txtInventoryCode.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Sub subClear()
        Try

            grdPurchaseReturn.Rows.Clear()
            'header
            txtRefNo.Clear()
            txtHTotalMC.Clear()
            txtHTotalCost.Clear()
            txtHGSTAmount.Clear()
            txtTotalPCS.Clear()
            txtTotalWeight.Clear()
            'detail
            txtInventoryCode.Clear()
            txtDescription.Clear()
            txtWeight.Clear()
            txtGoldLoss.Clear()
            txtMC.Clear()
            txtCostperGram.Clear()
            txtPCS.Clear()
            txtValue.Clear()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try

    End Sub

    Private Sub btnBillClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBillClear.Click
        Try
            subClear()
            txtSupplierCode.Clear()
            txtSupplierName.Clear()
            txtSupplierCode.ReadOnly = False
            txtSupplierCode.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub btnSAve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Dim i As Long
        Dim sMsg As String
        Dim sPurchaseReturnNo As String
        Dim dTotalWeight As Double
        Dim dTotalPCS As Double
        Dim dTotalValue As Double
        Dim dTotalGST As Double
        Dim dNetTotal As Double
        Dim Ds As DataSet
        Dim objPR As New clsPurchaseReturn
        Dim objInventory As New clsInventory
        Try

            If Not IsDate(txtRDD.Text.Trim & "-" & txtRMM.Text.Trim & "-" & txtRYYYY.Text.Trim) Then
                MsgBox("Please enter the valid Return Date!", MsgBoxStyle.Critical)
                txtRDD.Focus()
                Exit Sub
            ElseIf grdPurchaseReturn.Rows.Count = 0 Then
                MsgBox("There is no inventory in the Purchase Return List!", MsgBoxStyle.Critical)
                txtInventoryCode.Focus()
                Exit Sub
            ElseIf txtRefNo.Text.Trim <> "" Then
                MsgBox("You can take only the print out for old Purchase Return!", MsgBoxStyle.Critical)
                btnReport.Focus()
                Exit Sub
            End If


            dNetTotal = Val(txtHTotalMC.Text)
            dTotalWeight = Val(txtTotalWeight.Text)
            dTotalPCS = Val(txtTotalPCS.Text)
            dTotalValue = Val(txtHTotalCost.Text)
            dTotalGST = Val(txtHGSTAmount.Text)


            'open the transaction
            gsConBatchPosting.ConnectionString = gsCon
            gsConBatchPosting.Open()
            gsTrans = gsConBatchPosting.BeginTransaction

            'save header
            Ds = objPR.PurchaseReturnHeaderSave(txtSupplierCode.Text.Trim, txtRDD.Text.Trim & "/" & txtRMM.Text.Trim & "/" & txtRYYYY.Text.Trim, Trim(gthisLocation), _
                                Val(dTotalWeight), Val(dTotalPCS), Val(dTotalValue), Val(dTotalGST), Val(dNetTotal), _
                                txtRemarks.Text.Trim, sUserID)
            If Ds.Tables("PurChaseRet").Rows.Count > 0 Then
                sPurchaseReturnNo = Trim$(Ds.Tables("PurChaseRet").Rows(0)(0) & "")
            Else
                MsgBox("Error in receiving the Purchase Return Number!", MsgBoxStyle.Critical)
                gsTrans.Rollback()
                subClear()
                txtRefNo.Focus()
                Exit Sub
            End If


            'save details.
            For i = 0 To grdPurchaseReturn.Rows.Count - 1
                'Parameters added are sLocationCode, nDetailDiscount, dNetDiscount, dGst, dNetAmount and dActualSales
                If Not objPR.PurchaseReturnDetailSave(Trim(sPurchaseReturnNo), _
                                    Trim(fnGetdgvText(grdPurchaseReturn, i, IN_CODE)), Trim(fnGetdgvText(grdPurchaseReturn, i, IN_DESC)), _
                                    Trim(gthisLocation), Val(fnGetdgvText(grdPurchaseReturn, i, IN_WGHT)), Val(fnGetdgvText(grdPurchaseReturn, i, IN_GLOS)), _
                                    Val(fnGetdgvText(grdPurchaseReturn, i, IN_MCHG)), Val(fnGetdgvText(grdPurchaseReturn, i, IN_CTGM)), _
                                    Val(fnGetdgvText(grdPurchaseReturn, i, IN_PCES)), Val(fnGetdgvText(grdPurchaseReturn, i, IN_VALU)), _
                                    txtRemarks.Text.Trim, sUserID) Then


                    gsTrans.Rollback()
                    subClear()
                    Exit Sub
                End If

                If sRFIDModule = "1" Then
                    objPR.RFIDItemStatusUpdate(Trim(fnGetdgvText(grdPurchaseReturn, i, IN_CODE)))
                End If

                If objInventory.InventoryMovementSave(Trim(gthisLocation), Trim(fnGetdgvText(grdPurchaseReturn, i, IN_CODE)), Trim(fnGetdgvText(grdPurchaseReturn, i, IN_DESC)), "PR", Trim(sPurchaseReturnNo), txtRDD.Text.Trim & "-" & txtRMM.Text.Trim & "-" & txtRYYYY.Text.Trim, _
                         Val(fnGetdgvText(grdPurchaseReturn, i, IN_PCES)), Val(fnGetdgvText(grdPurchaseReturn, i, IN_WGHT)), 0, Val(fnGetdgvText(grdPurchaseReturn, i, IN_MCHG)), 0, Val(fnGetdgvText(grdPurchaseReturn, i, IN_CTGM)), Val(fnGetdgvText(grdPurchaseReturn, i, IN_VALU)), _
                         txtSupplierCode.Text.Trim, sUserID) Then
                Else
                    gsTrans.Rollback()
                    subClear()
                    Exit Sub
                End If

            Next i



            gsTrans.Commit()
            MsgBox("Please take Note Reference No : " & Trim(sPurchaseReturnNo))
            iRet = MsgBox("Do you want to print ? ", vbYesNo)
            If iRet = vbYes Then
                txtRefNo.Text = sPurchaseReturnNo
                btnReport_Click(sender, e)
            End If

            subClear()
            txtRefNo.Focus()
            gsConBatchPosting.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            gsConBatchPosting.Close()
            subClear()
            txtRefNo.Focus()
        End Try
    End Sub

    Private Sub btnReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReport.Click

        Dim strselectformula As String
        Dim frmRep As New frmReports
        Dim CR As New ReportDocument
        Dim objCompany As New clsCompany
        Dim Ds As DataSet
        Dim sTelephone As String
        Dim sFax As String
        Try
            If txtRefNo.Text.Trim <> "" Then

                Ds = objCompany.getCompanyName
                If Ds.Tables("GetCompanyName").Rows.Count > 0 Then
                    strCompanyname = Trim(Ds.Tables("GetCompanyName").Rows(0)("Name") & "")
                    strAddress = Trim(Ds.Tables("GetCompanyName").Rows(0)("Address1") & "") & " " & _
                                 Trim(Ds.Tables("GetCompanyName").Rows(0)("Address2") & "") & " " & _
                                 Trim(Ds.Tables("GetCompanyName").Rows(0)("City") & "")
                    sTelephone = Trim(Ds.Tables("GetCompanyName").Rows(0)("Phone") & "")
                    sFax = Trim(Ds.Tables("GetCompanyName").Rows(0)("Fax") & "")
                End If


                strpath = Application.StartupPath & "\Reports\rptPurchaseReturn.rpt"
                CR.Load(strpath)
                If Not ReportLogOnToServer(CR) Then
                    Exit Sub
                End If
                'ReportLogOnToServer(CR)
                CR.Refresh()
                frmRep.ReportViewer.ReportSource = CR
                strselectformula = "{tblPurchaseReturnHeader.ReturnNo} = " & fncFormatSQLText(txtRefNo.Text.Trim)



                CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(strCompanyname)
                CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(strAddress)
                CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(sTelephone)
                CR.DataDefinition.FormulaFields("CompAddress4").Text = fncFormatSQLText(sFax)


                frmRep.ReportViewer.SelectionFormula = strselectformula
                frmRep.Show()
                frmRep.ReportViewer.Visible = True
                frmRep.ReportViewer.DisplayToolbar = True
                frmRep.ReportViewer.CloseView(False)
                frmRep.WindowState = FormWindowState.Maximized
                frmRep = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub txtHGSTAmount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtHGSTAmount.LostFocus
        Try
            txtHTotalMC.Text = Format(Val(CDbl(txtHTotalCost.Text) + Val(txtHGSTAmount.Text)), "##0.00")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub txtInventoryCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtInventoryCode.KeyDown
        If e.KeyCode = Keys.F1 Then
            Dim Search As String
            If txtSupplierCode.Text.Trim <> "" Then
                Search = " QtyOnHand>=1 and SupplierCode = " & fncFormatSQLText(txtSupplierCode.Text.Trim)
            Else
                Search = " QtyOnHand>=1 "
            End If
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblInventory", "L", 4, "Description", Search, "InventoryCode", "InventoryCode", 80, "Description", "Description", 180, "JobSheetNo", "JobSheetNo", 60, "PurchaseBillNo", "PurchaseBillNo", 80)
            frmHelpClass.ShowDialog()
            txtInventoryCode.Text = frmHelpClass.Var1.Trim
        End If
    End Sub

    Private Sub frmPurchaseReturn_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
                btnSAve_Click(sender, e)
            Case Keys.Escape
                btnClose_Click(sender, e)
            Case Keys.F9
                btnReport_Click(sender, e)
        End Select
    End Sub
End Class
