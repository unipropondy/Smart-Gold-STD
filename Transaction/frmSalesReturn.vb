Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frmSalesReturn
    Inherits System.Windows.Forms.Form
    Dim nUnits As Long
    Dim sFlag As Boolean
    Dim sDescription As String
    Dim nAverageCost As Double
    Dim nAllowExpireDate As Long
    Dim sCompany As String
    Dim nExpiredDate As Long
    Dim nMaximumQtyLevel As Long
    Dim sUpdateFlag As Boolean
    Dim sNextRetNo As String
    Dim nRecivedQty As Long
    Dim sLocationCode As String
    Dim dReturnQty As Double
    Dim dReturnAmount As Double
    Dim dTotalStoneWeight As Double
    Dim dTotalGoldLoss As Double
    Dim dTotalSMC As Double
    Dim dNetDiscount As Double
    Dim Discount As Double
    Dim dNetTotal As Double
    Dim dPaidAmt As Double
    Dim dBalanceAmt As Double
    Dim objMember As New clsMember
    Dim msSalesManCode As String
    Dim nAllowSalesReturn As Integer
    Dim objUM As New clsUserMasters
    Dim bBCPassword As Boolean
    Dim sUserCode As String
    Dim sUserName As String

    Dim nGST As Double
    Dim sGSTType As String
    Dim sReturnNo As String
    Dim msSelInvoiceNo$
    Dim dTotalCost As Double
    Dim dTotalValue As Double
    Dim dTotalWeight As Double

    Dim sinventorycode As String

    Dim bSubFormLoaded As Boolean
    Dim dEmpty As Integer
    Dim mlSelPCS As Integer
    Dim mnSelRow As Integer
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents grdSalesReturn As System.Windows.Forms.DataGridView
    Friend WithEvents InventoryCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Weight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PCS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ReturnAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvDiscount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NetDiscount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NetAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ActualSales As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GST As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SalesManId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DeptCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CategoryCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BrandCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AverageCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Compatible As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Dim Dm As New DataManager

    Dim dTradeINamt As Double

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
    Friend WithEvents txtAddress3 As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress2 As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress1 As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtBillNo As System.Windows.Forms.TextBox
    Friend WithEvents txtSalesMan As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents optTaxExclusive As System.Windows.Forms.RadioButton
    Friend WithEvents optZeroRated As System.Windows.Forms.RadioButton
    Friend WithEvents optTaxInclusive As System.Windows.Forms.RadioButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnCustomerCodeSearch As System.Windows.Forms.Button
    Friend WithEvents txtCustomerCode As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtRYYYY As System.Windows.Forms.TextBox
    Friend WithEvents txtRMM As System.Windows.Forms.TextBox
    Friend WithEvents txtRDD As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtBYYYY As System.Windows.Forms.TextBox
    Friend WithEvents txtBMM As System.Windows.Forms.TextBox
    Friend WithEvents txtBDD As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents txtClear As System.Windows.Forms.Button
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lblNetAmount As System.Windows.Forms.Label
    Friend WithEvents btnReport As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnBillClear As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents btnReturnprintCancel As System.Windows.Forms.Button
    Friend WithEvents btnReturnprintOK As System.Windows.Forms.Button
    Friend WithEvents txtReturnPrintNo As System.Windows.Forms.TextBox
    Friend WithEvents PnlSalesManPwd As System.Windows.Forms.Panel
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtSalesManPWD As System.Windows.Forms.TextBox
    Friend WithEvents PnlReturnprint As System.Windows.Forms.Panel
    Friend WithEvents txtRetAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtPCS As System.Windows.Forms.TextBox
    Friend WithEvents txtWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtInventoryCode As System.Windows.Forms.TextBox
    Friend WithEvents lblReturnTotal As System.Windows.Forms.Label
    Friend WithEvents lblGST As System.Windows.Forms.Label
    Friend WithEvents lblReturnQty As System.Windows.Forms.Label
    Friend WithEvents tbnInventorySearch As System.Windows.Forms.Button
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtMemberCode As System.Windows.Forms.TextBox
    Friend WithEvents lblCustomerType As System.Windows.Forms.Label
    Friend WithEvents pnlMember As System.Windows.Forms.Panel
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents txtMemberCodeView As System.Windows.Forms.TextBox
    Friend WithEvents Label79 As System.Windows.Forms.Label
    Friend WithEvents btnMemberCodeSearch As System.Windows.Forms.Button
    Friend WithEvents pnlBillCancelAdminPwd As System.Windows.Forms.Panel
    Friend WithEvents btnBCAdminPwdCancel As System.Windows.Forms.Button
    Friend WithEvents btnBCAdminPwdOK As System.Windows.Forms.Button
    Friend WithEvents txtBCAdminPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSalesReturn))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.lblCustomerType = New System.Windows.Forms.Label()
        Me.txtMemberCode = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtBYYYY = New System.Windows.Forms.TextBox()
        Me.txtBMM = New System.Windows.Forms.TextBox()
        Me.txtBDD = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtRYYYY = New System.Windows.Forms.TextBox()
        Me.txtRMM = New System.Windows.Forms.TextBox()
        Me.txtRDD = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.btnCustomerCodeSearch = New System.Windows.Forms.Button()
        Me.txtCustomerCode = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.optTaxInclusive = New System.Windows.Forms.RadioButton()
        Me.optTaxExclusive = New System.Windows.Forms.RadioButton()
        Me.optZeroRated = New System.Windows.Forms.RadioButton()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtAddress3 = New System.Windows.Forms.TextBox()
        Me.txtAddress2 = New System.Windows.Forms.TextBox()
        Me.txtAddress1 = New System.Windows.Forms.TextBox()
        Me.txtSalesMan = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtBillNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.txtClear = New System.Windows.Forms.Button()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtRetAmount = New System.Windows.Forms.TextBox()
        Me.txtPCS = New System.Windows.Forms.TextBox()
        Me.txtWeight = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.tbnInventorySearch = New System.Windows.Forms.Button()
        Me.txtInventoryCode = New System.Windows.Forms.TextBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.lblNetAmount = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblReturnTotal = New System.Windows.Forms.Label()
        Me.lblGST = New System.Windows.Forms.Label()
        Me.lblReturnQty = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.btnReport = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnBillClear = New System.Windows.Forms.Button()
        Me.PnlReturnprint = New System.Windows.Forms.Panel()
        Me.btnReturnprintCancel = New System.Windows.Forms.Button()
        Me.btnReturnprintOK = New System.Windows.Forms.Button()
        Me.txtReturnPrintNo = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.PnlSalesManPwd = New System.Windows.Forms.Panel()
        Me.txtSalesManPWD = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.pnlMember = New System.Windows.Forms.Panel()
        Me.btnMemberCodeSearch = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.txtMemberCodeView = New System.Windows.Forms.TextBox()
        Me.Label79 = New System.Windows.Forms.Label()
        Me.pnlBillCancelAdminPwd = New System.Windows.Forms.Panel()
        Me.btnBCAdminPwdCancel = New System.Windows.Forms.Button()
        Me.btnBCAdminPwdOK = New System.Windows.Forms.Button()
        Me.txtBCAdminPassword = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.grdSalesReturn = New System.Windows.Forms.DataGridView()
        Me.InventoryCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Description = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Weight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PCS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ReturnAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvDiscount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NetDiscount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NetAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ActualSales = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GST = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SalesManId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DeptCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CategoryCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BrandCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AverageCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Compatible = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.PnlReturnprint.SuspendLayout()
        Me.PnlSalesManPwd.SuspendLayout()
        Me.pnlMember.SuspendLayout()
        Me.pnlBillCancelAdminPwd.SuspendLayout()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSalesReturn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label24)
        Me.Panel1.Controls.Add(Me.txtRemarks)
        Me.Panel1.Controls.Add(Me.lblCustomerType)
        Me.Panel1.Controls.Add(Me.txtMemberCode)
        Me.Panel1.Controls.Add(Me.Label22)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.txtBYYYY)
        Me.Panel1.Controls.Add(Me.txtBMM)
        Me.Panel1.Controls.Add(Me.txtBDD)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txtRYYYY)
        Me.Panel1.Controls.Add(Me.txtRMM)
        Me.Panel1.Controls.Add(Me.txtRDD)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.txtCustomerName)
        Me.Panel1.Controls.Add(Me.btnCustomerCodeSearch)
        Me.Panel1.Controls.Add(Me.txtCustomerCode)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.txtAddress3)
        Me.Panel1.Controls.Add(Me.txtAddress2)
        Me.Panel1.Controls.Add(Me.txtAddress1)
        Me.Panel1.Controls.Add(Me.txtSalesMan)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtBillNo)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Location = New System.Drawing.Point(5, 103)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(658, 213)
        Me.Panel1.TabIndex = 1
        '
        'Label24
        '
        Me.Label24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Black
        Me.Label24.Location = New System.Drawing.Point(8, 187)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(82, 15)
        Me.Label24.TabIndex = 131
        Me.Label24.Text = "Remarks"
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.Black
        Me.txtRemarks.Location = New System.Drawing.Point(104, 183)
        Me.txtRemarks.MaxLength = 20
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(328, 20)
        Me.txtRemarks.TabIndex = 130
        Me.txtRemarks.Tag = ""
        '
        'lblCustomerType
        '
        Me.lblCustomerType.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblCustomerType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustomerType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerType.ForeColor = System.Drawing.Color.Red
        Me.lblCustomerType.Location = New System.Drawing.Point(440, 80)
        Me.lblCustomerType.Name = "lblCustomerType"
        Me.lblCustomerType.Size = New System.Drawing.Size(120, 20)
        Me.lblCustomerType.TabIndex = 129
        Me.lblCustomerType.Text = "Customer Type"
        Me.lblCustomerType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtMemberCode
        '
        Me.txtMemberCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMemberCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMemberCode.ForeColor = System.Drawing.Color.Black
        Me.txtMemberCode.Location = New System.Drawing.Point(232, 80)
        Me.txtMemberCode.MaxLength = 20
        Me.txtMemberCode.Name = "txtMemberCode"
        Me.txtMemberCode.Size = New System.Drawing.Size(200, 20)
        Me.txtMemberCode.TabIndex = 121
        Me.txtMemberCode.Tag = ""
        '
        'Label22
        '
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.Black
        Me.Label22.Location = New System.Drawing.Point(8, 104)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(82, 15)
        Me.Label22.TabIndex = 120
        Me.Label22.Text = "Name"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(584, 10)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(8, 16)
        Me.Label6.TabIndex = 118
        Me.Label6.Text = "-"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(552, 10)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(8, 16)
        Me.Label8.TabIndex = 117
        Me.Label8.Text = "-"
        '
        'txtBYYYY
        '
        Me.txtBYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBYYYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBYYYY.Location = New System.Drawing.Point(592, 8)
        Me.txtBYYYY.MaxLength = 4
        Me.txtBYYYY.Name = "txtBYYYY"
        Me.txtBYYYY.Size = New System.Drawing.Size(32, 20)
        Me.txtBYYYY.TabIndex = 5
        Me.txtBYYYY.Tag = "ENN;"
        '
        'txtBMM
        '
        Me.txtBMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBMM.Location = New System.Drawing.Point(560, 8)
        Me.txtBMM.MaxLength = 2
        Me.txtBMM.Name = "txtBMM"
        Me.txtBMM.Size = New System.Drawing.Size(21, 20)
        Me.txtBMM.TabIndex = 4
        Me.txtBMM.Tag = "ENN;"
        '
        'txtBDD
        '
        Me.txtBDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBDD.Location = New System.Drawing.Point(528, 8)
        Me.txtBDD.MaxLength = 2
        Me.txtBDD.Name = "txtBDD"
        Me.txtBDD.Size = New System.Drawing.Size(21, 20)
        Me.txtBDD.TabIndex = 3
        Me.txtBDD.Tag = "ENN;"
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(448, 8)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(72, 20)
        Me.Label9.TabIndex = 116
        Me.Label9.Text = "Bill Date"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(584, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(8, 16)
        Me.Label3.TabIndex = 112
        Me.Label3.Text = "-"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(552, 40)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(8, 16)
        Me.Label4.TabIndex = 111
        Me.Label4.Text = "-"
        '
        'txtRYYYY
        '
        Me.txtRYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRYYYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRYYYY.Location = New System.Drawing.Point(592, 40)
        Me.txtRYYYY.MaxLength = 4
        Me.txtRYYYY.Name = "txtRYYYY"
        Me.txtRYYYY.Size = New System.Drawing.Size(32, 20)
        Me.txtRYYYY.TabIndex = 11
        Me.txtRYYYY.Tag = "ENN;"
        '
        'txtRMM
        '
        Me.txtRMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRMM.Location = New System.Drawing.Point(560, 40)
        Me.txtRMM.MaxLength = 2
        Me.txtRMM.Name = "txtRMM"
        Me.txtRMM.Size = New System.Drawing.Size(21, 20)
        Me.txtRMM.TabIndex = 10
        Me.txtRMM.Tag = "ENN;"
        '
        'txtRDD
        '
        Me.txtRDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRDD.Location = New System.Drawing.Point(528, 40)
        Me.txtRDD.MaxLength = 2
        Me.txtRDD.Name = "txtRDD"
        Me.txtRDD.Size = New System.Drawing.Size(21, 20)
        Me.txtRDD.TabIndex = 9
        Me.txtRDD.Tag = "ENN;"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(448, 40)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 20)
        Me.Label7.TabIndex = 110
        Me.Label7.Text = "Return Date"
        '
        'txtCustomerName
        '
        Me.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerName.ForeColor = System.Drawing.Color.Black
        Me.txtCustomerName.Location = New System.Drawing.Point(104, 106)
        Me.txtCustomerName.MaxLength = 20
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(200, 20)
        Me.txtCustomerName.TabIndex = 14
        Me.txtCustomerName.Tag = ""
        '
        'btnCustomerCodeSearch
        '
        Me.btnCustomerCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCustomerCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCustomerCodeSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomerCodeSearch.Location = New System.Drawing.Point(200, 80)
        Me.btnCustomerCodeSearch.Name = "btnCustomerCodeSearch"
        Me.btnCustomerCodeSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnCustomerCodeSearch.TabIndex = 13
        Me.btnCustomerCodeSearch.Text = "..."
        Me.btnCustomerCodeSearch.UseVisualStyleBackColor = False
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerCode.ForeColor = System.Drawing.Color.Black
        Me.txtCustomerCode.Location = New System.Drawing.Point(104, 80)
        Me.txtCustomerCode.MaxLength = 20
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.Size = New System.Drawing.Size(96, 20)
        Me.txtCustomerCode.TabIndex = 12
        Me.txtCustomerCode.Tag = ""
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(8, 80)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 15)
        Me.Label1.TabIndex = 105
        Me.Label1.Text = "Customer Code"
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Location = New System.Drawing.Point(0, 72)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(656, 2)
        Me.Panel3.TabIndex = 102
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.optTaxInclusive)
        Me.Panel2.Controls.Add(Me.optTaxExclusive)
        Me.Panel2.Controls.Add(Me.optZeroRated)
        Me.Panel2.Location = New System.Drawing.Point(104, 36)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(328, 29)
        Me.Panel2.TabIndex = 101
        '
        'optTaxInclusive
        '
        Me.optTaxInclusive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTaxInclusive.Location = New System.Drawing.Point(110, 1)
        Me.optTaxInclusive.Name = "optTaxInclusive"
        Me.optTaxInclusive.Size = New System.Drawing.Size(96, 24)
        Me.optTaxInclusive.TabIndex = 7
        Me.optTaxInclusive.Text = "Tax Inclusive"
        '
        'optTaxExclusive
        '
        Me.optTaxExclusive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTaxExclusive.Location = New System.Drawing.Point(216, 1)
        Me.optTaxExclusive.Name = "optTaxExclusive"
        Me.optTaxExclusive.Size = New System.Drawing.Size(99, 24)
        Me.optTaxExclusive.TabIndex = 8
        Me.optTaxExclusive.Text = "Tax Exclusive"
        '
        'optZeroRated
        '
        Me.optZeroRated.Checked = True
        Me.optZeroRated.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optZeroRated.Location = New System.Drawing.Point(12, 1)
        Me.optZeroRated.Name = "optZeroRated"
        Me.optZeroRated.Size = New System.Drawing.Size(88, 24)
        Me.optZeroRated.TabIndex = 6
        Me.optZeroRated.TabStop = True
        Me.optZeroRated.Text = "Zero Rated"
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(8, 136)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(82, 15)
        Me.Label13.TabIndex = 100
        Me.Label13.Text = "Address"
        '
        'txtAddress3
        '
        Me.txtAddress3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress3.ForeColor = System.Drawing.Color.Black
        Me.txtAddress3.Location = New System.Drawing.Point(280, 158)
        Me.txtAddress3.MaxLength = 20
        Me.txtAddress3.Name = "txtAddress3"
        Me.txtAddress3.Size = New System.Drawing.Size(152, 20)
        Me.txtAddress3.TabIndex = 17
        Me.txtAddress3.Tag = ""
        '
        'txtAddress2
        '
        Me.txtAddress2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress2.ForeColor = System.Drawing.Color.Black
        Me.txtAddress2.Location = New System.Drawing.Point(104, 158)
        Me.txtAddress2.MaxLength = 20
        Me.txtAddress2.Name = "txtAddress2"
        Me.txtAddress2.Size = New System.Drawing.Size(168, 20)
        Me.txtAddress2.TabIndex = 16
        Me.txtAddress2.Tag = ""
        '
        'txtAddress1
        '
        Me.txtAddress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress1.ForeColor = System.Drawing.Color.Black
        Me.txtAddress1.Location = New System.Drawing.Point(104, 132)
        Me.txtAddress1.MaxLength = 20
        Me.txtAddress1.Name = "txtAddress1"
        Me.txtAddress1.Size = New System.Drawing.Size(328, 20)
        Me.txtAddress1.TabIndex = 15
        Me.txtAddress1.Tag = ""
        '
        'txtSalesMan
        '
        Me.txtSalesMan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesMan.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesMan.ForeColor = System.Drawing.Color.Black
        Me.txtSalesMan.Location = New System.Drawing.Point(336, 8)
        Me.txtSalesMan.MaxLength = 20
        Me.txtSalesMan.Name = "txtSalesMan"
        Me.txtSalesMan.Size = New System.Drawing.Size(96, 20)
        Me.txtSalesMan.TabIndex = 2
        Me.txtSalesMan.Tag = ""
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(256, 11)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 15)
        Me.Label5.TabIndex = 65
        Me.Label5.Text = "Sales Man"
        '
        'txtBillNo
        '
        Me.txtBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillNo.ForeColor = System.Drawing.Color.Black
        Me.txtBillNo.Location = New System.Drawing.Point(104, 8)
        Me.txtBillNo.MaxLength = 20
        Me.txtBillNo.Name = "txtBillNo"
        Me.txtBillNo.Size = New System.Drawing.Size(120, 20)
        Me.txtBillNo.TabIndex = 1
        Me.txtBillNo.Tag = ""
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(8, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 15)
        Me.Label2.TabIndex = 49
        Me.Label2.Text = "Bill No"
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.btnAdd)
        Me.Panel4.Controls.Add(Me.btnDelete)
        Me.Panel4.Controls.Add(Me.txtClear)
        Me.Panel4.Controls.Add(Me.Label15)
        Me.Panel4.Controls.Add(Me.Label14)
        Me.Panel4.Controls.Add(Me.txtRetAmount)
        Me.Panel4.Controls.Add(Me.txtPCS)
        Me.Panel4.Controls.Add(Me.txtWeight)
        Me.Panel4.Controls.Add(Me.Label12)
        Me.Panel4.Controls.Add(Me.Label11)
        Me.Panel4.Controls.Add(Me.Label10)
        Me.Panel4.Controls.Add(Me.txtDescription)
        Me.Panel4.Controls.Add(Me.tbnInventorySearch)
        Me.Panel4.Controls.Add(Me.txtInventoryCode)
        Me.Panel4.Location = New System.Drawing.Point(5, 460)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(658, 80)
        Me.Panel4.TabIndex = 103
        '
        'btnAdd
        '
        Me.btnAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnAdd.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.Location = New System.Drawing.Point(411, 50)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 24
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(491, 50)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 25
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'txtClear
        '
        Me.txtClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClear.Location = New System.Drawing.Point(571, 50)
        Me.txtClear.Name = "txtClear"
        Me.txtClear.Size = New System.Drawing.Size(75, 23)
        Me.txtClear.TabIndex = 26
        Me.txtClear.Text = "Clear"
        Me.txtClear.UseVisualStyleBackColor = False
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(536, 8)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(104, 15)
        Me.Label15.TabIndex = 108
        Me.Label15.Text = "Return Amount"
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(449, 8)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(82, 15)
        Me.Label14.TabIndex = 107
        Me.Label14.Text = "PCS"
        '
        'txtRetAmount
        '
        Me.txtRetAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRetAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRetAmount.ForeColor = System.Drawing.Color.Black
        Me.txtRetAmount.Location = New System.Drawing.Point(534, 24)
        Me.txtRetAmount.MaxLength = 20
        Me.txtRetAmount.Name = "txtRetAmount"
        Me.txtRetAmount.Size = New System.Drawing.Size(112, 20)
        Me.txtRetAmount.TabIndex = 23
        Me.txtRetAmount.Tag = ""
        '
        'txtPCS
        '
        Me.txtPCS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPCS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPCS.ForeColor = System.Drawing.Color.Black
        Me.txtPCS.Location = New System.Drawing.Point(448, 24)
        Me.txtPCS.MaxLength = 20
        Me.txtPCS.Name = "txtPCS"
        Me.txtPCS.Size = New System.Drawing.Size(83, 20)
        Me.txtPCS.TabIndex = 22
        Me.txtPCS.Tag = ""
        '
        'txtWeight
        '
        Me.txtWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeight.ForeColor = System.Drawing.Color.Black
        Me.txtWeight.Location = New System.Drawing.Point(358, 24)
        Me.txtWeight.MaxLength = 20
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.Size = New System.Drawing.Size(87, 20)
        Me.txtWeight.TabIndex = 21
        Me.txtWeight.Tag = ""
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(358, 8)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(82, 15)
        Me.Label12.TabIndex = 103
        Me.Label12.Text = "Weight"
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(139, 8)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(82, 15)
        Me.Label11.TabIndex = 102
        Me.Label11.Text = "Description"
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(8, 8)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(104, 15)
        Me.Label10.TabIndex = 101
        Me.Label10.Text = "Inventory Code"
        '
        'txtDescription
        '
        Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.ForeColor = System.Drawing.Color.Black
        Me.txtDescription.Location = New System.Drawing.Point(139, 24)
        Me.txtDescription.MaxLength = 20
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(216, 20)
        Me.txtDescription.TabIndex = 20
        Me.txtDescription.Tag = ""
        '
        'tbnInventorySearch
        '
        Me.tbnInventorySearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.tbnInventorySearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.tbnInventorySearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbnInventorySearch.Location = New System.Drawing.Point(112, 24)
        Me.tbnInventorySearch.Name = "tbnInventorySearch"
        Me.tbnInventorySearch.Size = New System.Drawing.Size(24, 20)
        Me.tbnInventorySearch.TabIndex = 19
        Me.tbnInventorySearch.Text = "..."
        Me.tbnInventorySearch.UseVisualStyleBackColor = False
        '
        'txtInventoryCode
        '
        Me.txtInventoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInventoryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInventoryCode.ForeColor = System.Drawing.Color.Black
        Me.txtInventoryCode.Location = New System.Drawing.Point(8, 24)
        Me.txtInventoryCode.MaxLength = 20
        Me.txtInventoryCode.Name = "txtInventoryCode"
        Me.txtInventoryCode.Size = New System.Drawing.Size(104, 20)
        Me.txtInventoryCode.TabIndex = 18
        Me.txtInventoryCode.Tag = ""
        '
        'Panel5
        '
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.lblNetAmount)
        Me.Panel5.Controls.Add(Me.Label16)
        Me.Panel5.Controls.Add(Me.lblReturnTotal)
        Me.Panel5.Controls.Add(Me.lblGST)
        Me.Panel5.Controls.Add(Me.lblReturnQty)
        Me.Panel5.Controls.Add(Me.Label17)
        Me.Panel5.Controls.Add(Me.Label18)
        Me.Panel5.Controls.Add(Me.Label19)
        Me.Panel5.Location = New System.Drawing.Point(5, 540)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(658, 66)
        Me.Panel5.TabIndex = 104
        '
        'lblNetAmount
        '
        Me.lblNetAmount.BackColor = System.Drawing.Color.LightCyan
        Me.lblNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNetAmount.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetAmount.ForeColor = System.Drawing.Color.Black
        Me.lblNetAmount.Location = New System.Drawing.Point(496, 32)
        Me.lblNetAmount.Name = "lblNetAmount"
        Me.lblNetAmount.Size = New System.Drawing.Size(160, 32)
        Me.lblNetAmount.TabIndex = 79
        Me.lblNetAmount.Text = "0.00"
        Me.lblNetAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Pink
        Me.Label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label16.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(496, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(160, 32)
        Me.Label16.TabIndex = 78
        Me.Label16.Text = "Net Amount"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblReturnTotal
        '
        Me.lblReturnTotal.BackColor = System.Drawing.Color.LightCyan
        Me.lblReturnTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblReturnTotal.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReturnTotal.ForeColor = System.Drawing.Color.Black
        Me.lblReturnTotal.Location = New System.Drawing.Point(168, 32)
        Me.lblReturnTotal.Name = "lblReturnTotal"
        Me.lblReturnTotal.Size = New System.Drawing.Size(168, 32)
        Me.lblReturnTotal.TabIndex = 77
        Me.lblReturnTotal.Text = "0.00"
        Me.lblReturnTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblGST
        '
        Me.lblGST.BackColor = System.Drawing.Color.LightCyan
        Me.lblGST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblGST.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGST.ForeColor = System.Drawing.Color.Black
        Me.lblGST.Location = New System.Drawing.Point(336, 32)
        Me.lblGST.Name = "lblGST"
        Me.lblGST.Size = New System.Drawing.Size(160, 32)
        Me.lblGST.TabIndex = 76
        Me.lblGST.Text = "0.00"
        Me.lblGST.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblReturnQty
        '
        Me.lblReturnQty.BackColor = System.Drawing.Color.LightCyan
        Me.lblReturnQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblReturnQty.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReturnQty.ForeColor = System.Drawing.Color.Black
        Me.lblReturnQty.Location = New System.Drawing.Point(0, 32)
        Me.lblReturnQty.Name = "lblReturnQty"
        Me.lblReturnQty.Size = New System.Drawing.Size(168, 32)
        Me.lblReturnQty.TabIndex = 75
        Me.lblReturnQty.Text = "0.00"
        Me.lblReturnQty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.Pink
        Me.Label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label17.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(168, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(168, 32)
        Me.Label17.TabIndex = 74
        Me.Label17.Text = "Total Amount"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.Pink
        Me.Label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label18.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(336, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(160, 32)
        Me.Label18.TabIndex = 73
        Me.Label18.Text = "GST Amount"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.Pink
        Me.Label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label19.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(0, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(168, 32)
        Me.Label19.TabIndex = 72
        Me.Label19.Text = "Total Qty"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnReport
        '
        Me.btnReport.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnReport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReport.Location = New System.Drawing.Point(457, 612)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(100, 40)
        Me.btnReport.TabIndex = 28
        Me.btnReport.Text = "Reprint (F9)"
        Me.btnReport.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(351, 612)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 40)
        Me.btnSave.TabIndex = 27
        Me.btnSave.Text = "Save (F7)"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(563, 612)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 30
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnBillClear
        '
        Me.btnBillClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnBillClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBillClear.Location = New System.Drawing.Point(242, 612)
        Me.btnBillClear.Name = "btnBillClear"
        Me.btnBillClear.Size = New System.Drawing.Size(100, 40)
        Me.btnBillClear.TabIndex = 29
        Me.btnBillClear.Text = "Clear (F6)"
        Me.btnBillClear.UseVisualStyleBackColor = False
        '
        'PnlReturnprint
        '
        Me.PnlReturnprint.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.PnlReturnprint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PnlReturnprint.Controls.Add(Me.btnReturnprintCancel)
        Me.PnlReturnprint.Controls.Add(Me.btnReturnprintOK)
        Me.PnlReturnprint.Controls.Add(Me.txtReturnPrintNo)
        Me.PnlReturnprint.Controls.Add(Me.Label20)
        Me.PnlReturnprint.Location = New System.Drawing.Point(304, 313)
        Me.PnlReturnprint.Name = "PnlReturnprint"
        Me.PnlReturnprint.Size = New System.Drawing.Size(248, 88)
        Me.PnlReturnprint.TabIndex = 109
        Me.PnlReturnprint.Visible = False
        '
        'btnReturnprintCancel
        '
        Me.btnReturnprintCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnReturnprintCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReturnprintCancel.Image = CType(resources.GetObject("btnReturnprintCancel.Image"), System.Drawing.Image)
        Me.btnReturnprintCancel.Location = New System.Drawing.Point(152, 48)
        Me.btnReturnprintCancel.Name = "btnReturnprintCancel"
        Me.btnReturnprintCancel.Size = New System.Drawing.Size(64, 23)
        Me.btnReturnprintCancel.TabIndex = 33
        Me.btnReturnprintCancel.Text = "Cancel"
        Me.btnReturnprintCancel.UseVisualStyleBackColor = False
        '
        'btnReturnprintOK
        '
        Me.btnReturnprintOK.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnReturnprintOK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReturnprintOK.Image = CType(resources.GetObject("btnReturnprintOK.Image"), System.Drawing.Image)
        Me.btnReturnprintOK.Location = New System.Drawing.Point(80, 48)
        Me.btnReturnprintOK.Name = "btnReturnprintOK"
        Me.btnReturnprintOK.Size = New System.Drawing.Size(64, 23)
        Me.btnReturnprintOK.TabIndex = 32
        Me.btnReturnprintOK.Text = "OK"
        Me.btnReturnprintOK.UseVisualStyleBackColor = False
        '
        'txtReturnPrintNo
        '
        Me.txtReturnPrintNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReturnPrintNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReturnPrintNo.ForeColor = System.Drawing.Color.Black
        Me.txtReturnPrintNo.Location = New System.Drawing.Point(80, 16)
        Me.txtReturnPrintNo.MaxLength = 20
        Me.txtReturnPrintNo.Name = "txtReturnPrintNo"
        Me.txtReturnPrintNo.Size = New System.Drawing.Size(136, 20)
        Me.txtReturnPrintNo.TabIndex = 31
        Me.txtReturnPrintNo.Tag = ""
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.Location = New System.Drawing.Point(8, 16)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(64, 15)
        Me.Label20.TabIndex = 74
        Me.Label20.Text = "Return No"
        '
        'PnlSalesManPwd
        '
        Me.PnlSalesManPwd.BackColor = System.Drawing.Color.Pink
        Me.PnlSalesManPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PnlSalesManPwd.Controls.Add(Me.txtSalesManPWD)
        Me.PnlSalesManPwd.Controls.Add(Me.Label21)
        Me.PnlSalesManPwd.Location = New System.Drawing.Point(230, 313)
        Me.PnlSalesManPwd.Name = "PnlSalesManPwd"
        Me.PnlSalesManPwd.Size = New System.Drawing.Size(199, 82)
        Me.PnlSalesManPwd.TabIndex = 0
        Me.PnlSalesManPwd.Visible = False
        '
        'txtSalesManPWD
        '
        Me.txtSalesManPWD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesManPWD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesManPWD.ForeColor = System.Drawing.Color.Black
        Me.txtSalesManPWD.Location = New System.Drawing.Point(48, 40)
        Me.txtSalesManPWD.MaxLength = 20
        Me.txtSalesManPWD.Name = "txtSalesManPWD"
        Me.txtSalesManPWD.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtSalesManPWD.Size = New System.Drawing.Size(136, 20)
        Me.txtSalesManPWD.TabIndex = 0
        Me.txtSalesManPWD.Tag = ""
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Black
        Me.Label21.Location = New System.Drawing.Point(48, 14)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(128, 15)
        Me.Label21.TabIndex = 74
        Me.Label21.Text = "SalesMan Password"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlMember
        '
        Me.pnlMember.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.pnlMember.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMember.Controls.Add(Me.btnMemberCodeSearch)
        Me.pnlMember.Controls.Add(Me.btnNew)
        Me.pnlMember.Controls.Add(Me.btnOK)
        Me.pnlMember.Controls.Add(Me.btnCancel)
        Me.pnlMember.Controls.Add(Me.txtMemberCodeView)
        Me.pnlMember.Controls.Add(Me.Label79)
        Me.pnlMember.Location = New System.Drawing.Point(168, 313)
        Me.pnlMember.Name = "pnlMember"
        Me.pnlMember.Size = New System.Drawing.Size(312, 104)
        Me.pnlMember.TabIndex = 150
        Me.pnlMember.Visible = False
        '
        'btnMemberCodeSearch
        '
        Me.btnMemberCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnMemberCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMemberCodeSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMemberCodeSearch.Location = New System.Drawing.Point(256, 24)
        Me.btnMemberCodeSearch.Name = "btnMemberCodeSearch"
        Me.btnMemberCodeSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnMemberCodeSearch.TabIndex = 14
        Me.btnMemberCodeSearch.Text = "..."
        Me.btnMemberCodeSearch.UseVisualStyleBackColor = False
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(72, 56)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(64, 23)
        Me.btnNew.TabIndex = 5
        Me.btnNew.Text = "New"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnOK
        '
        Me.btnOK.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnOK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.Image = CType(resources.GetObject("btnOK.Image"), System.Drawing.Image)
        Me.btnOK.Location = New System.Drawing.Point(144, 56)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(64, 23)
        Me.btnOK.TabIndex = 3
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.Location = New System.Drawing.Point(216, 56)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(64, 23)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'txtMemberCodeView
        '
        Me.txtMemberCodeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMemberCodeView.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMemberCodeView.Location = New System.Drawing.Point(120, 24)
        Me.txtMemberCodeView.Name = "txtMemberCodeView"
        Me.txtMemberCodeView.Size = New System.Drawing.Size(136, 20)
        Me.txtMemberCodeView.TabIndex = 0
        '
        'Label79
        '
        Me.Label79.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label79.Location = New System.Drawing.Point(8, 32)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(128, 24)
        Me.Label79.TabIndex = 0
        Me.Label79.Text = "MemberCode"
        '
        'pnlBillCancelAdminPwd
        '
        Me.pnlBillCancelAdminPwd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.pnlBillCancelAdminPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlBillCancelAdminPwd.Controls.Add(Me.btnBCAdminPwdCancel)
        Me.pnlBillCancelAdminPwd.Controls.Add(Me.btnBCAdminPwdOK)
        Me.pnlBillCancelAdminPwd.Controls.Add(Me.txtBCAdminPassword)
        Me.pnlBillCancelAdminPwd.Controls.Add(Me.Label23)
        Me.pnlBillCancelAdminPwd.Controls.Add(Me.Label29)
        Me.pnlBillCancelAdminPwd.Location = New System.Drawing.Point(210, 313)
        Me.pnlBillCancelAdminPwd.Name = "pnlBillCancelAdminPwd"
        Me.pnlBillCancelAdminPwd.Size = New System.Drawing.Size(246, 88)
        Me.pnlBillCancelAdminPwd.TabIndex = 151
        Me.pnlBillCancelAdminPwd.Visible = False
        '
        'btnBCAdminPwdCancel
        '
        Me.btnBCAdminPwdCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnBCAdminPwdCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBCAdminPwdCancel.Image = CType(resources.GetObject("btnBCAdminPwdCancel.Image"), System.Drawing.Image)
        Me.btnBCAdminPwdCancel.Location = New System.Drawing.Point(160, 56)
        Me.btnBCAdminPwdCancel.Name = "btnBCAdminPwdCancel"
        Me.btnBCAdminPwdCancel.Size = New System.Drawing.Size(64, 23)
        Me.btnBCAdminPwdCancel.TabIndex = 75
        Me.btnBCAdminPwdCancel.Text = "Cancel"
        Me.btnBCAdminPwdCancel.UseVisualStyleBackColor = False
        '
        'btnBCAdminPwdOK
        '
        Me.btnBCAdminPwdOK.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnBCAdminPwdOK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBCAdminPwdOK.Image = CType(resources.GetObject("btnBCAdminPwdOK.Image"), System.Drawing.Image)
        Me.btnBCAdminPwdOK.Location = New System.Drawing.Point(88, 56)
        Me.btnBCAdminPwdOK.Name = "btnBCAdminPwdOK"
        Me.btnBCAdminPwdOK.Size = New System.Drawing.Size(64, 23)
        Me.btnBCAdminPwdOK.TabIndex = 74
        Me.btnBCAdminPwdOK.Text = "OK"
        Me.btnBCAdminPwdOK.UseVisualStyleBackColor = False
        '
        'txtBCAdminPassword
        '
        Me.txtBCAdminPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBCAdminPassword.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBCAdminPassword.ForeColor = System.Drawing.Color.Black
        Me.txtBCAdminPassword.Location = New System.Drawing.Point(88, 32)
        Me.txtBCAdminPassword.MaxLength = 20
        Me.txtBCAdminPassword.Name = "txtBCAdminPassword"
        Me.txtBCAdminPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtBCAdminPassword.Size = New System.Drawing.Size(136, 20)
        Me.txtBCAdminPassword.TabIndex = 73
        Me.txtBCAdminPassword.Tag = ""
        '
        'Label23
        '
        Me.Label23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.Black
        Me.Label23.Location = New System.Drawing.Point(16, 33)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(64, 15)
        Me.Label23.TabIndex = 72
        Me.Label23.Text = "Password"
        '
        'Label29
        '
        Me.Label29.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.Color.Black
        Me.Label29.Location = New System.Drawing.Point(8, 8)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(56, 15)
        Me.Label29.TabIndex = 72
        Me.Label29.Text = "Admin"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(5, -3)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 100)
        Me.pic.TabIndex = 512
        Me.pic.TabStop = False
        '
        'grdSalesReturn
        '
        Me.grdSalesReturn.AllowUserToAddRows = False
        Me.grdSalesReturn.AllowUserToDeleteRows = False
        Me.grdSalesReturn.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdSalesReturn.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdSalesReturn.ColumnHeadersHeight = 30
        Me.grdSalesReturn.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.InventoryCode, Me.Description, Me.Weight, Me.PCS, Me.ReturnAmount, Me.dgvDiscount, Me.NetDiscount, Me.NetAmount, Me.ActualSales, Me.GST, Me.SalesManId, Me.DeptCode, Me.CategoryCode, Me.BrandCode, Me.AverageCost, Me.Compatible})
        Me.grdSalesReturn.Location = New System.Drawing.Point(5, 317)
        Me.grdSalesReturn.Name = "grdSalesReturn"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdSalesReturn.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdSalesReturn.Size = New System.Drawing.Size(658, 140)
        Me.grdSalesReturn.TabIndex = 513
        '
        'InventoryCode
        '
        Me.InventoryCode.HeaderText = "InventoryCode"
        Me.InventoryCode.Name = "InventoryCode"
        '
        'Description
        '
        Me.Description.HeaderText = "Description"
        Me.Description.Name = "Description"
        Me.Description.Width = 200
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
        '
        'ReturnAmount
        '
        Me.ReturnAmount.HeaderText = "Return Amount"
        Me.ReturnAmount.Name = "ReturnAmount"
        '
        'dgvDiscount
        '
        Me.dgvDiscount.HeaderText = "Discount"
        Me.dgvDiscount.Name = "dgvDiscount"
        Me.dgvDiscount.Visible = False
        '
        'NetDiscount
        '
        Me.NetDiscount.HeaderText = "Net Discount"
        Me.NetDiscount.Name = "NetDiscount"
        Me.NetDiscount.Visible = False
        '
        'NetAmount
        '
        Me.NetAmount.HeaderText = "Net Amount"
        Me.NetAmount.Name = "NetAmount"
        Me.NetAmount.Visible = False
        '
        'ActualSales
        '
        Me.ActualSales.HeaderText = "ActualSales"
        Me.ActualSales.Name = "ActualSales"
        Me.ActualSales.Visible = False
        '
        'GST
        '
        Me.GST.HeaderText = "GST"
        Me.GST.Name = "GST"
        Me.GST.Visible = False
        '
        'SalesManId
        '
        Me.SalesManId.HeaderText = "SalesManId"
        Me.SalesManId.Name = "SalesManId"
        Me.SalesManId.Visible = False
        '
        'DeptCode
        '
        Me.DeptCode.HeaderText = "Dept.Code"
        Me.DeptCode.Name = "DeptCode"
        Me.DeptCode.Visible = False
        '
        'CategoryCode
        '
        Me.CategoryCode.HeaderText = "Category Code"
        Me.CategoryCode.Name = "CategoryCode"
        Me.CategoryCode.Visible = False
        '
        'BrandCode
        '
        Me.BrandCode.HeaderText = "Brand Code"
        Me.BrandCode.Name = "BrandCode"
        Me.BrandCode.Visible = False
        '
        'AverageCost
        '
        Me.AverageCost.HeaderText = "Average Cost"
        Me.AverageCost.Name = "AverageCost"
        Me.AverageCost.Visible = False
        '
        'Compatible
        '
        Me.Compatible.HeaderText = "Compatible"
        Me.Compatible.Name = "Compatible"
        Me.Compatible.Visible = False
        '
        'frmSalesReturn
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(667, 664)
        Me.Controls.Add(Me.PnlSalesManPwd)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.pnlBillCancelAdminPwd)
        Me.Controls.Add(Me.pnlMember)
        Me.Controls.Add(Me.PnlReturnprint)
        Me.Controls.Add(Me.btnReport)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnBillClear)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.grdSalesReturn)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmSalesReturn"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sales Return"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.PnlReturnprint.ResumeLayout(False)
        Me.PnlReturnprint.PerformLayout()
        Me.PnlSalesManPwd.ResumeLayout(False)
        Me.PnlSalesManPwd.PerformLayout()
        Me.pnlMember.ResumeLayout(False)
        Me.pnlMember.PerformLayout()
        Me.pnlBillCancelAdminPwd.ResumeLayout(False)
        Me.pnlBillCancelAdminPwd.PerformLayout()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSalesReturn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmSalesReturn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ''subGridSetting(grdSalesReturn)
            subGetGST()
            PnlReturnprint.Visible = False
            PnlSalesManPwd.Visible = True
            If PnlSalesManPwd.Enabled Then txtSalesManPWD.Focus()
            grdSalesReturn.Rows.Clear()
            sFlag = False
            txtRDD.Text = Format(Now.Day, "00")
            txtRMM.Text = Format(Now.Month, "00")
            txtRYYYY.Text = Now.Year
            dEmpty = 0
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Private Sub subGetGST()
        Try
            Dim Ds As DataSet
            Dim objCompany As New clsCompany1
            Ds = objCompany.GetCompanyList()
            If Ds.Tables(0).Rows.Count > 0 Then

                nGST = Val(Ds.Tables(0).Rows(0)("GstPerc") & "")
            End If
            Ds = Nothing
            objCompany = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub frmSalesReturn_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
                btnSave_Click(sender, e)
            Case Keys.Escape
                btnClose_Click(sender, e)
            Case Keys.F9
                btnReport_Click(sender, e)
        End Select
    End Sub
    Private Sub txtSalesManPWD_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSalesManPWD.LostFocus
        Try
            If txtSalesManPWD.Text.Trim <> "" Then
                subGetPassword()
            Else
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Private Sub subGetPassword()
        Dim i As Long
        Dim Ds As DataSet
        Dim objSalesMan As New clsSalesMan
        Try

            Ds = objSalesMan.GetSalesManwithPassword(txtSalesManPWD.Text.Trim)
            If Ds.Tables("Salesman").Rows.Count > 0 Then
                msSalesManCode = Trim(Ds.Tables("Salesman").Rows(0)("SalesmanCode") & "")
                txtSalesMan.Text = Trim(Ds.Tables("Salesman").Rows(0)("SalesManName") & "")
                nAllowSalesReturn = Ds.Tables("Salesman").Rows(0)("AllowSalesReturn") & ""
            Else
                MsgBox("SalesMan Password is Incorrect!", MsgBoxStyle.Critical)
                txtSalesManPWD.Text = ""
                txtSalesManPWD.Focus()
                Exit Sub
            End If
            'If objSalesMan.SalesManLogSave(msSalesManCode, txtSalesMan.Text.Trim, "Sales Return", nAllowSalesReturn) Then
            'Else
            '    MessageBox.Show("Error in Log Save")
            'End If
            'If nAllowSalesReturn = 0 Then
            '    MsgBox("SalesMan Has No Rights to do Sales Return!", MsgBoxStyle.Critical)
            '    msSalesManCode = ""
            '    txtSalesMan.Text = ""
            '    Me.Close()
            '    Exit Sub
            'End If
            PnlSalesManPwd.Visible = False
            If UCase(sCompanyShortCode) = "HN" And (UCase(Objuser.CompanyName)) = "HANIFFA PTE LTD" Then
                pnlBillCancelAdminPwd.Visible = True
                txtBCAdminPassword.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub


    Private Sub txtBillNo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBillNo.LostFocus
        Dim objSalesRet As New ClsSalesReturn
        Dim Ds As DataSet
        Try
            If txtBillNo.Text.Trim <> "" Then


                Ds = objSalesRet.GetSalesReturnHeader(txtBillNo.Text.Trim)
                If Ds.Tables("SalesReturn").Rows.Count > 0 Then
                    MsgBox(UCase(txtBillNo.Text.Trim) + " Bill Already Returned ...", MsgBoxStyle.Critical)
                    Ds = Nothing
                    objSalesRet = Nothing
                    txtBillNo.Text = ""
                    txtBillNo.Focus()
                    btnAdd.Enabled = True
                    txtInventoryCode.Enabled = True
                    Exit Sub
                Else
                    subGetSalesDetail()
                    If txtBillNo.Text.Trim <> "" Then
                        btnAdd.Enabled = False
                        txtInventoryCode.Enabled = False
                    Else
                        btnAdd.Enabled = True
                        txtInventoryCode.Enabled = True
                    End If
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Public Sub subGetSalesDetail()

        Dim i As Long
        Dim j As Long
        Dim ODs As DataSet
        Dim tempInvCode As String
        Dim objSales As New ClsSalesReturn
        Dim objCustomer As New clsCustomer
        Dim Ds As DataSet
        Dim DsHdr As DataSet
        Dim DsCust As DataSet
        Dim DsMember As DataSet


        Try
            dReturnAmount = 0
            dReturnQty = 0
            DsHdr = objSales.GetSalesHeaderByInvoiceNo(txtBillNo.Text.Trim)
            If DsHdr.Tables("Invoice").Rows.Count > 0 Then
                If Not IsDate(DsHdr.Tables("Invoice").Rows(0)("InvoiceDate") & "") Then Exit Sub
                If DateValue(DsHdr.Tables("Invoice").Rows(0)("InvoiceDate") & "") = DateValue(Date.Now) Then
                    MsgBox("You cannot sales return the today's Bill. Please do the Bill Cancel!", MsgBoxStyle.Critical)
                    txtBillNo.Text = ""
                    txtBillNo.Focus()
                    Exit Sub
                End If

                If Trim(sCompanyShortCode) = "MD" Then
                    If Trim(txtSalesMan.Text) <> "BASRIA" Then
                        If DateDiff(DateInterval.Day, DsHdr.Tables("Invoice").Rows(0)("InvoiceDate") & "", Date.Now) > 7 Then
                            MsgBox("You cannot sales return Bills Older than 7 Days. Please Take as TradeIn!", MsgBoxStyle.Critical)
                            txtBillNo.Text = ""
                            If txtInventoryCode.Enabled Then txtInventoryCode.Text = ""
                            txtBillNo.Focus()
                            Exit Sub
                        End If
                    End If
                End If

                If Val(Trim(DsHdr.Tables("Invoice").Rows(0)("TradeInAmt") & "")) > 0 Then
                    MsgBox("This Sales has TRADEIN Items", MsgBoxStyle.Critical)
                End If
                dTradeINamt = Val(Trim(DsHdr.Tables("Invoice").Rows(0)("TradeInAmt") & ""))

                txtCustomerCode.Text = Trim(DsHdr.Tables("Invoice").Rows(0)("CustomerCode") & "")
                txtMemberCode.Text = Trim(DsHdr.Tables("Invoice").Rows(0)("MemberCode") & "")
                ''HAJ change for Date format, on 23 Oct 2007
                'txtBDD.Text = Mid(DsHdr.Tables("Invoice").Rows(0)("BDate"), 7, 2)
                'txtBMM.Text = Mid(DsHdr.Tables("Invoice").Rows(0)("BDate"), 5, 2)
                'txtBYYYY.Text = Mid(DsHdr.Tables("Invoice").Rows(0)("BDate"), 1, 4)
                txtBDD.Text = Mid(DsHdr.Tables("Invoice").Rows(0)("BDate"), 1, 2)
                txtBMM.Text = Mid(DsHdr.Tables("Invoice").Rows(0)("BDate"), 4, 2)
                txtBYYYY.Text = Mid(DsHdr.Tables("Invoice").Rows(0)("BDate"), 7, 4)
                sGSTType = Trim(DsHdr.Tables("Invoice").Rows(0)("GSTType") & "")
                If sGSTType = "I" Then
                    optTaxInclusive.Checked = True
                ElseIf sGSTType = "E" Then
                    optTaxExclusive.Checked = True
                Else
                    optZeroRated.Checked = True
                End If
                msSalesManCode = Trim(DsHdr.Tables("Invoice").Rows(0)("SalesManCode") & "")

                DsCust = objCustomer.GetCustomerwithCode(txtCustomerCode.Text.Trim)
                DsMember = objMember.GetMemberwithCode(txtMemberCode.Text.Trim)
                If DsCust.Tables("Customer").Rows.Count > 0 Then
                    txtCustomerName.Text = Trim(DsCust.Tables("Customer").Rows(0)("CustomerName") & "")
                    txtAddress1.Text = Trim(DsCust.Tables("Customer").Rows(0)("Address1") & "")
                    txtAddress2.Text = Trim(DsCust.Tables("Customer").Rows(0)("Address2") & "")
                    txtAddress3.Text = Trim(DsCust.Tables("Customer").Rows(0)("Address3") & "")
                    lblCustomerType.Text = Trim(DsCust.Tables("Customer").Rows(0)("CustomerType") & "")
                    'Else
                    '    txtCustomerName.Clear()
                    '    txtAddress1.Clear()
                    '    txtAddress2.Clear()
                    '    txtAddress3.Clear()
                    '    lblCustomerType.Text = "Type"
                End If
                If DsMember.Tables(0).Rows.Count > 0 And sMemberModule = "1" Then
                    txtCustomerName.Text = Trim(DsMember.Tables(0).Rows(0)("MemberName") & "")
                    txtAddress1.Text = Trim(DsMember.Tables(0).Rows(0)("Address1") & "")
                    txtAddress2.Text = Trim(DsMember.Tables(0).Rows(0)("Address2") & "")
                    txtAddress3.Text = Trim(DsMember.Tables(0).Rows(0)("Address3") & "")
                    lblCustomerType.Text = Trim(DsMember.Tables(0).Rows(0)("MemberType") & "")
                    'Else
                    '    txtCustomerName.Clear()
                    '    txtAddress1.Clear()
                    '    txtAddress2.Clear()
                    '    txtAddress3.Clear()
                    '    lblCustomerType.Text = "Type"
                End If
                If DsCust.Tables("Customer").Rows.Count = 0 And DsMember.Tables(0).Rows.Count = 0 Then
                    txtCustomerName.Clear()
                    txtAddress1.Clear()
                    txtAddress2.Clear()
                    txtAddress3.Clear()
                    lblCustomerType.Text = "Type"
                End If

            End If

            Ds = objSales.GetSalesDetailByInvoiceNo(txtBillNo.Text.Trim)
            If Ds.Tables("SalesDetail").Rows.Count > 0 Then
                grdSalesReturn.Rows.Clear()
                For i = 0 To Ds.Tables("SalesDetail").Rows.Count - 1
                    grdSalesReturn.Rows.Add()
                    grdSalesReturn.Rows(i).HeaderCell.Value = (i + 1).ToString()
                    fnSetdgvText(grdSalesReturn, i, 0, Trim(Ds.Tables("SalesDetail").Rows(i)("Inventorycode") & ""))
                    fnSetdgvText(grdSalesReturn, i, 1, Trim(Ds.Tables("SalesDetail").Rows(i)("Description") & ""))
                    fnSetdgvText(grdSalesReturn, i, 2, (Ds.Tables("SalesDetail").Rows(i)("Weight") & ""))
                    fnSetdgvText(grdSalesReturn, i, 3, Val(Ds.Tables("SalesDetail").Rows(i)("Pcs") & ""))
                    fnSetdgvText(grdSalesReturn, i, 4, Val(Ds.Tables("SalesDetail").Rows(i)("TotalValue") & "") - Val(Ds.Tables("SalesDetail").Rows(i)("NetDiscount") & ""))
                    fnSetdgvText(grdSalesReturn, i, 5, Val(Ds.Tables("SalesDetail").Rows(i)("DetailDiscount") & ""))
                    fnSetdgvText(grdSalesReturn, i, 6, Val(Ds.Tables("SalesDetail").Rows(i)("NetDiscount") & ""))
                    fnSetdgvText(grdSalesReturn, i, 7, Val(Ds.Tables("SalesDetail").Rows(i)("NetAmount") & ""))
                    fnSetdgvText(grdSalesReturn, i, 8, Val(Ds.Tables("SalesDetail").Rows(i)("ActualSales") & ""))
                    fnSetdgvText(grdSalesReturn, i, 9, Val(Ds.Tables("SalesDetail").Rows(i)("Gst") & ""))
                    fnSetdgvText(grdSalesReturn, i, 13, Val(Ds.Tables("SalesDetail").Rows(i)("TotalCost") & ""))
                    fnSetdgvText(grdSalesReturn, i, 14, Val(Ds.Tables("SalesDetail").Rows(i)("TotalValue") & ""))
                    fnSetdgvText(grdSalesReturn, i, 15, Val(Ds.Tables("SalesDetail").Rows(i)("Pcs") & ""))
                Next
            Else
                MsgBox("Invoice No is InCorrect!", MsgBoxStyle.Critical)
                txtBillNo.Text = ""
                txtBillNo.Focus()
            End If
            CalcTotal()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Private Sub CalcTotal()

        Dim i As Long
        Try
            lblReturnQty.Text = "0"
            lblReturnTotal.Text = "0"
            dTotalValue = 0
            dTotalWeight = 0
            dTotalCost = 0
            For i = 0 To grdSalesReturn.Rows.Count - 1
                dTotalWeight = Val(dTotalWeight) + Val(fnGetdgvText(grdSalesReturn, i, 2))
                lblReturnQty.Text = Val(lblReturnQty.Text) + Val(fnGetdgvText(grdSalesReturn, i, 3))
                lblReturnTotal.Text = Val(lblReturnTotal.Text) + Val(fnGetdgvText(grdSalesReturn, i, 4))
                dTotalCost = Val(dTotalCost) + Val(fnGetdgvText(grdSalesReturn, i, 13))
                dTotalValue = Val(dTotalValue) + Val(fnGetdgvText(grdSalesReturn, i, 14))
            Next i

            If Trim(sGSTType & "") = "E" Then
                If nGST > 0 Then
                    lblGST.Text = Val(lblReturnTotal.Text) * nGST / 100
                Else
                    lblGST.Text = "0"
                End If
                lblNetAmount.Text = Val(lblReturnTotal.Text) + Val(lblGST.Text)

            ElseIf Trim(sGSTType & "") = "I" Then
                If nGST > 0 Then
                    lblGST.Text = (Val(lblReturnTotal.Text) * nGST) / (100 + nGST)
                Else
                    lblGST.Text = "0"
                End If
                lblNetAmount.Text = Val(lblReturnTotal.Text)
            Else
                lblGST.Text = "0"
                lblNetAmount.Text = Val(lblReturnTotal.Text)
            End If

            lblReturnQty.Text = Format(Val(lblReturnQty.Text), "0")
            lblReturnTotal.Text = Format(Val(lblReturnTotal.Text), "0.00")
            ''HAj TradeIn GST For AS
            If UCase(sCompanyShortCode) = "AS" And UCase(sGSTType) = "I" Then
                subCalculateTradeInGST()
            Else
                lblGST.Text = Format(Val(lblGST.Text), "0.00")
            End If
            lblNetAmount.Text = Format(Val(lblNetAmount.Text), "0.00")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub txtInventoryCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInventoryCode.LostFocus
        Try
            If txtInventoryCode.Text.Trim <> "" Then
                subGetInventory(sender, e)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Private Sub subGetInventory(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim Ds As DataSet
        Dim ObjInventory As New clsInventory

        Try
            If txtBillNo.Text.Trim <> "" Then
                MsgBox("You cannot add item for the selected Bill No!", MsgBoxStyle.Critical)
                Exit Sub
            End If

            Ds = ObjInventory.GetInventoryDetail(txtInventoryCode.Text.Trim)

            If Ds.Tables("tblInventory").Rows.Count > 0 Then
                txtDescription.Text = Trim(Ds.Tables("tblInventory").Rows(0)("Description") & "")

                Call fncGetCustomerPrice(txtInventoryCode.Text.Trim, sender, e)

            Else
                MsgBox("This Inventory Not Found... ", MsgBoxStyle.Critical)
                txtInventoryCode.Clear()
                txtInventoryCode.Focus()
            End If
            Ds = Nothing
            ObjInventory = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Private Function fncGetCustomerPrice(ByVal sinventorycode As String, ByVal sender As Object, ByVal e As System.EventArgs) As Boolean
        Dim objSales As New ClsSalesReturn
        Dim DsSales As DataSet
        Dim DsExist As DataSet
        Dim sSql As String
        Try
            DsSales = objSales.GetCustomerPrice(sinventorycode)
            If DsSales.Tables("SalesDetail").Rows.Count > 1 Then
                msSelInvoiceNo = ""
                sSql = "Select InvoiceNo, InvoiceDate, CustomerCode, SalesManCode, TotalWeight, TotalPCS, NetTotal From tblSalesHeader Where InvoiceNo in " & _
                        "(Select InvoiceNo from tblSalesDetail where InventoryCode = " & fncFormatSQLText(sinventorycode) & _
                        " and InvoiceNo not like '" & Trim$(gthisLocation) & "02%') Order By InvoiceDate Desc"
                DsExist = Dm.getDataSetWithSqlAdapter(sSql, "SalesDetail", "SalesDetail")
                If DsExist.Tables("SalesDetail").Rows.Count > 0 Then
                    msSelInvoiceNo = Trim(DsExist.Tables("SalesDetail").Rows(0)("InvoiceNo") & "")
                End If
                'frmInvoiceSelect.SelInventoryCode = Trim$(sinventorycode)
                'frmInvoiceSelect.Show(vbModal)
                If msSelInvoiceNo <> "" Then
                    txtBillNo.Text = Trim(msSelInvoiceNo)
                    txtBillNo_LostFocus(sender, e)
                    fncGetCustomerPrice = True
                Else
                    fncGetCustomerPrice = False
                End If



            ElseIf DsSales.Tables("SalesDetail").Rows.Count = 1 Then
                txtBillNo.Text = Trim(DsSales.Tables("SalesDetail").Rows(0)("InvoiceNo") & "")
                txtBillNo_LostFocus(sender, e)
                fncGetCustomerPrice = True
            Else
                MsgBox("Please select the sold Inventory!", MsgBoxStyle.Critical)
                fncGetCustomerPrice = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            DsExist = Nothing
        End Try
    End Function

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        Dim i As Long
        Dim j As Long
        Dim z As Long

        Dim bisRowDeleted As Boolean
        Dim bisRowExist As Boolean
        Try

            If txtInventoryCode.Text.Trim <> "" And txtDescription.Text.Trim <> "" And Val(txtPCS.Text) > 0 Then

                If sFlag = True Then
                    ''grdSalesReturn.Row = grdSalesReturn.ActiveRow
                Else
                    bisRowExist = False
                    For i = 0 To grdSalesReturn.Rows.Count - 1
                        If txtInventoryCode.Text.Trim = Trim(fnGetdgvText(grdSalesReturn, i, 0)) And Val(txtPCS.Text) = Val(fnGetdgvText(grdSalesReturn, i, 3)) Then
                            bisRowExist = True
                            grdSalesReturn.RowCount = i
                            Exit For
                        End If
                    Next i
                    If Not bisRowExist Then
                        grdSalesReturn.RowCount = grdSalesReturn.Rows.Count + 1
                        grdSalesReturn.RowCount = grdSalesReturn.Rows.Count
                        i = grdSalesReturn.Rows.Count
                    End If
                End If

                grdSalesReturn.Item(0, grdSalesReturn.CurrentRow.Index).Value = txtInventoryCode.Text.Trim
                grdSalesReturn.Item(1, grdSalesReturn.CurrentRow.Index).Value = txtDescription.Text.Trim

                If bisRowExist Then
                    grdSalesReturn.Item(2, grdSalesReturn.CurrentRow.Index).Value = Val(grdSalesReturn.Text) + Val(txtWeight.Text)
                    grdSalesReturn.Item(3, grdSalesReturn.CurrentRow.Index).Value = txtPCS.Text.Trim
                    grdSalesReturn.Item(4, grdSalesReturn.CurrentRow.Index).Value = Val(grdSalesReturn.Text) + Val(txtRetAmount.Text)
                Else
                    If Val(txtPCS.Text) <> Val(fnGetdgvText(grdSalesReturn, grdSalesReturn.CurrentRow.Index, 3)) Then
                        
                        grdSalesReturn.Item(2, grdSalesReturn.CurrentRow.Index).Value = Val(fnGetdgvText(grdSalesReturn, grdSalesReturn.CurrentRow.Index, 2)) * Val(txtPCS.Text) / Val(fnGetdgvText(grdSalesReturn, grdSalesReturn.CurrentRow.Index, 3))
                        grdSalesReturn.Item(4, grdSalesReturn.CurrentRow.Index).Value = Val(fnGetdgvText(grdSalesReturn, grdSalesReturn.CurrentRow.Index, 4)) * Val(txtPCS.Text) / Val(fnGetdgvText(grdSalesReturn, grdSalesReturn.CurrentRow.Index, 3))
                        grdSalesReturn.Item(5, grdSalesReturn.CurrentRow.Index).Value = Val(fnGetdgvText(grdSalesReturn, grdSalesReturn.CurrentRow.Index, 5)) * Val(txtPCS.Text) / Val(fnGetdgvText(grdSalesReturn, grdSalesReturn.CurrentRow.Index, 3))
                        grdSalesReturn.Item(6, grdSalesReturn.CurrentRow.Index).Value = Val(fnGetdgvText(grdSalesReturn, grdSalesReturn.CurrentRow.Index, 6)) * Val(txtPCS.Text) / Val(fnGetdgvText(grdSalesReturn, grdSalesReturn.CurrentRow.Index, 3))
                        grdSalesReturn.Item(7, grdSalesReturn.CurrentRow.Index).Value = Val(fnGetdgvText(grdSalesReturn, grdSalesReturn.CurrentRow.Index, 7)) * Val(txtPCS.Text) / Val(fnGetdgvText(grdSalesReturn, grdSalesReturn.CurrentRow.Index, 3))
                        grdSalesReturn.Item(8, grdSalesReturn.CurrentRow.Index).Value = Val(fnGetdgvText(grdSalesReturn, grdSalesReturn.CurrentRow.Index, 8)) * Val(txtPCS.Text) / Val(fnGetdgvText(grdSalesReturn, grdSalesReturn.CurrentRow.Index, 3))
                        grdSalesReturn.Item(9, grdSalesReturn.CurrentRow.Index).Value = Val(fnGetdgvText(grdSalesReturn, grdSalesReturn.CurrentRow.Index, 9)) * Val(txtPCS.Text) / Val(fnGetdgvText(grdSalesReturn, grdSalesReturn.CurrentRow.Index, 3))
                        grdSalesReturn.Item(13, grdSalesReturn.CurrentRow.Index).Value = Val(fnGetdgvText(grdSalesReturn, grdSalesReturn.CurrentRow.Index, 13)) * Val(txtPCS.Text) / Val(fnGetdgvText(grdSalesReturn, grdSalesReturn.CurrentRow.Index, 3))
                        grdSalesReturn.Item(14, grdSalesReturn.CurrentRow.Index).Value = Val(fnGetdgvText(grdSalesReturn, grdSalesReturn.CurrentRow.Index, 14)) * Val(txtPCS.Text) / Val(fnGetdgvText(grdSalesReturn, grdSalesReturn.CurrentRow.Index, 3))

                        grdSalesReturn.Item(3, grdSalesReturn.CurrentRow.Index).Value = Trim(txtPCS.Text)
                    End If
                End If
                'grdSalesReturn.Col = 6
                'grdSalesReturn.Text = ""
            Else
                If txtInventoryCode.Enabled Then
                    MsgBox("Please enter the Inventory", MsgBoxStyle.Critical)
                Else
                    MsgBox("Please select the Inventory", MsgBoxStyle.Critical)
                End If

            End If
            CalcTotal()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim sInvCode As String
        Dim i As Long
        Dim j As Long
        Dim iRow As Integer
        Try
            If grdSalesReturn.Rows.Count > 0 Then
                'grdSalesReturn.Row = grdSalesReturn.ActiveRow
                'sInvCode = Trim(fnGetdgvText(grdSalesReturn, grdSalesReturn.ActiveRow, 1))

                'grdSalesReturn.Action = 5
                'grdSalesReturn.MaxRows = grdSalesReturn.MaxRows - 1
                iRow = grdSalesReturn.CurrentRow.Index
                sInvCode = Trim(fnGetdgvText(grdSalesReturn, iRow, 0))
                grdSalesReturn.Rows.RemoveAt(iRow)
                grdSalesReturn.RowCount = grdSalesReturn.Rows.Count
                subClear()
            End If
            CalcTotal()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Private Sub subClear()
        Try
            txtInventoryCode.Clear()
            txtDescription.Clear()
            txtWeight.Text = "0"
            txtPCS.Text = "0"
            txtRetAmount.Text = "0"
            txtRetAmount.Text = Format(Val(txtRetAmount.Text), "0.00")
            If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub txtClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtClear.Click
        Try
            subClear()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Dim i As Long
        Dim sDate As String
        Dim objSalesRet As New ClsSalesReturn
        Try
            If UCase(txtCustomerCode.Text) = "CASH" And sMemberModule = "1" Then
                If txtMemberCode.Text.Trim = "" Then
                    MsgBox("MemberCode is NOT entered !", MsgBoxStyle.Critical)
                    txtCustomerCode.Focus()
                    Exit Sub
                End If
            End If
            If grdSalesReturn.Rows.Count <= 0 Then
                MsgBox("No items for Sales Return!", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If fncUpdatesSalesReturn() Then
                MsgBox(" Please take note the sales return no " + sNextRetNo, MsgBoxStyle.Information)
                ''Block By Durai on Dec 26th 
                '''subPrintSalesReturn(sNextRetNo)
                If Trim(sCompanyShortCode) = "AJ" Then
                    subPrintSalesReturn(sNextRetNo)
                End If
                subClearHeader()
                Exit Sub
            Else
                MsgBox("Error in Saving SalesReturn", MsgBoxStyle.Critical)
                Exit Sub
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Private Sub subClearHeader()


        Try
            grdSalesReturn.Rows.Clear()
            sFlag = False
            txtRDD.Text = Format(ServerDate, "dd")
            txtRMM.Text = Format(ServerDate, "MM")
            txtRYYYY.Text = Format(ServerDate, "yyyy")
            dEmpty = 0
            txtCustomerCode.Clear()
            txtMemberCode.Text = ""
            lblCustomerType.Text = "Type"
            txtCustomerName.Clear()
            txtAddress1.Clear()
            txtAddress2.Clear()
            txtAddress3.Clear()
            txtBillNo.Clear()
            txtBDD.Clear()
            txtBMM.Clear()
            txtBYYYY.Clear()
            lblNetAmount.Text = "0.00"
            lblGST.Text = "0.00"
            lblReturnQty.Text = "0"
            lblReturnTotal.Text = "0.00"
            dTradeINamt = 0
            txtBillNo.Focus()
            txtInventoryCode.Enabled = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try

    End Sub

    Private Sub subPrintSalesReturn(ByVal sReturnNo As String)

        Dim strselectformula As String
        Dim frmRep As New frmReports
        Dim CR As New ReportDocument
        Dim objCompany As New clsCompany
        Dim Ds As DataSet
        Dim sTelephone As String
        Try
            If Trim(sReturnNo) <> "" Then

                Ds = objCompany.getCompanyName
                If Ds.Tables("GetCompanyName").Rows.Count > 0 Then
                    strCompanyname = Trim(Ds.Tables("GetCompanyName").Rows(0)("Name") & "")
                    strAddress = Trim(Ds.Tables("GetCompanyName").Rows(0)("Address1") & "") & " " & _
                                 Trim(Ds.Tables("GetCompanyName").Rows(0)("Address2") & "") & " " & _
                                 Trim(Ds.Tables("GetCompanyName").Rows(0)("City") & "")
                    sTelephone = Trim(Ds.Tables("GetCompanyName").Rows(0)("Phone") & "")
                End If


                strpath = Application.StartupPath & "\Reports\rptSalesReturn.rpt"
                CR.Load(strpath)
                If Not ReportLogOnToServer(CR) Then
                    Exit Sub
                End If
                'ReportLogOnToServer(CR)
                CR.Refresh()
                frmRep.ReportViewer.ReportSource = CR
                strselectformula = "{tblSalesReturnHeader.RefNo} = " & fncFormatSQLText(sReturnNo)



                CR.DataDefinition.FormulaFields("CustomerName").Text = fncFormatSQLText(txtCustomerName.Text)
                'CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(strAddress)
                'CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(sTelephone)


                frmRep.ReportViewer.SelectionFormula = strselectformula
                CR.DataDefinition.RecordSelectionFormula = strselectformula
                'frmRep.Show()
                'frmRep.ReportViewer.Visible = True
                'frmRep.ReportViewer.DisplayToolbar = True
                'frmRep.ReportViewer.CloseView(False)
                'frmRep.WindowState = FormWindowState.Maximized
                CR.PrintToPrinter(1, True, 0, 0)
                frmRep = Nothing
                frmRep = New frmReports

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Function fncUpdatesSalesReturn() As Boolean

        Dim Ds As DataSet
        Dim Ds1 As DataSet

        Dim iRows As Long
        Dim i As Long
        Dim objSR As New ClsSalesReturn
        Dim LitemCode As String
        Dim iRec As Long
        Dim sQry As String
        fncUpdatesSalesReturn = False
        Try
            gsConBatchPosting.ConnectionString = gsCon
            gsConBatchPosting.Open()
            gsTrans = gsConBatchPosting.BeginTransaction

            sQry = "sp_SalesReturnHeader  "
            sQry = sQry & fncFormatSQLText(gthisLocation & "") & ","
            sQry = sQry & fncFormatSQLText(txtBillNo.Text & "") & ","
            sQry = sQry & Val(dTotalWeight) & ","
            sQry = sQry & Val(lblReturnQty.Text) & ","
            sQry = sQry & Val(lblReturnTotal.Text) & ","
            sQry = sQry & Val(dTotalCost) & ","
            sQry = sQry & Val(lblGST.Text) & ","
            sQry = sQry & Val(lblNetAmount.Text) & ","
            sQry = sQry & fncFormatSQLText(msSalesManCode & "") & ","
            sQry = sQry & fncFormatSQLText(txtCustomerCode.Text & "") & ","
            sQry = sQry & fncFormatSQLText(txtMemberCode.Text & "") & ","
            sQry = sQry & fncFormatSQLText(txtCustomerName.Text & "") & ","
            sQry = sQry & fncFormatSQLText(sUserID & "") & ""
            Ds = Dm.getDataSetWithSqlAdapter(sQry, "SalesReturnHeader", "SalesReturnHeader")
            If Ds.Tables("SalesReturnHeader").Rows.Count > 0 Then
                sNextRetNo = Trim$(Ds.Tables("SalesReturnHeader").Rows(0)("ReturnNo") & "")
                For i = 0 To grdSalesReturn.Rows.Count - 1
                    sQry = "sp_SalesReturnDetail  "
                    sQry = sQry & fncFormatSQLText(sNextRetNo) & ","
                    sQry = sQry & fncFormatSQLText(Trim(fnGetdgvText(grdSalesReturn, i, 0))) & ","
                    sQry = sQry & fncFormatSQLText(Trim(fnGetdgvText(grdSalesReturn, i, 1))) & ","
                    sQry = sQry & Val(fnGetdgvText(grdSalesReturn, i, 2)) & ","
                    sQry = sQry & Val(fnGetdgvText(grdSalesReturn, i, 3)) & ","
                    sQry = sQry & Val(fnGetdgvText(grdSalesReturn, i, 13)) & ","
                    sQry = sQry & Val(fnGetdgvText(grdSalesReturn, i, 4)) & ","
                    sQry = sQry & Val(fnGetdgvText(grdSalesReturn, i, 5)) & ","
                    sQry = sQry & Val(fnGetdgvText(grdSalesReturn, i, 6)) & ","
                    sQry = sQry & Val(fnGetdgvText(grdSalesReturn, i, 7)) & ","
                    sQry = sQry & Val(fnGetdgvText(grdSalesReturn, i, 8)) & ","
                    sQry = sQry & Val(fnGetdgvText(grdSalesReturn, i, 9)) & ","
                    sQry = sQry & fncFormatSQLText(msSalesManCode) & ","
                    sQry = sQry & fncFormatSQLText(txtBillNo.Text) & ","
                    sQry = sQry & fncFormatSQLText(gthisLocation) & ","
                    sQry = sQry & fncFormatSQLText(sUserID) & ""
                    Ds = Dm.getDataSetWithSqlAdapter(sQry, "SalesRetDetail", "SalesRetDetail")

                    If sRFIDModule = "1" Then
                        objSR.RFIDItemStatusUpdate(Trim(fnGetdgvText(grdSalesReturn, i, 0)))
                    End If
                Next i
                ''HAJ SalesReturn Paymodedetail Capture in Negative Value on 26 Dec 2007
                If UCase(sCompanyShortCode) = "MJ" Then
                    sQry = "sp_SalesReturnPayModeDetail "
                    sQry = sQry & fncFormatSQLText(sNextRetNo) & ","
                    sQry = sQry & fncFormatSQLText(sUserID) & ""
                    Ds = Dm.getDataSetWithSqlAdapter(sQry, "PayModeDetail", "PayModeDetail")
                End If

                sQry = "sp_SalesReturnDeposit "
                sQry = sQry & fncFormatSQLText(sNextRetNo) & ","
                sQry = sQry & fncFormatSQLText(txtCustomerCode.Text & "") & ","
                sQry = sQry & Val(lblNetAmount.Text) - Val(dTradeINamt) & ","
                sQry = sQry & fncFormatSQLText("DEPOSIT") & ","
                sQry = sQry & fncFormatSQLText(sUserID) & ""
                Ds = Dm.getDataSetWithSqlAdapter(sQry, "PayModeDetail", "PayModeDetail")

                If Val(dTradeINamt) > 0 Then
                    sQry = "sp_SalesReturnDeposit "
                    sQry = sQry & fncFormatSQLText(sNextRetNo) & ","
                    sQry = sQry & fncFormatSQLText(txtCustomerCode.Text & "") & ","
                    sQry = sQry & Val(dTradeINamt) & ","
                    sQry = sQry & fncFormatSQLText("TRADEIN") & ","
                    sQry = sQry & fncFormatSQLText(sUserID) & ""
                    Ds = Dm.getDataSetWithSqlAdapter(sQry, "PayModeDetail", "PayModeDetail")
                End If

                sQry = "EXEC sp_SalesBillCancelCustomerUpdate " & fncFormatSQLText(txtBillNo.Text)

                Ds = Dm.getDataSetWithSqlAdapterAcc(sQry, "CustomerUpdate", "CustomerUpdate")


            Else
                MsgBox("ERROR IN DETAIL UPDATION ", MsgBoxStyle.Critical)
                gsTrans.Rollback()
                Exit Function
            End If
            gsTrans.Commit()
            gsConBatchPosting.Close()
            fncUpdatesSalesReturn = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            gsTrans.Rollback()
            gsConBatchPosting.Close()
        End Try
    End Function

    Private Sub btnBillClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBillClear.Click
        Try
            subClear()
            subClearHeader()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub btnReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReport.Click
        Try
            PnlReturnprint.Visible = True
            txtReturnPrintNo.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub btnReturnprintOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReturnprintOK.Click
        Try
            subGetSalesReturnPrint()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Private Sub subGetSalesReturnPrint()
        Try
            Dim Ds As DataSet
            Dim ObjSalesReturn As New ClsSalesReturn

            Ds = ObjSalesReturn.GetSalesReturnHeaderByRefNo(txtReturnPrintNo.Text.Trim)
            If Ds.Tables(0).Rows.Count > 0 Then
                sNextRetNo = txtReturnPrintNo.Text.Trim
                subPrintSalesReturn(sNextRetNo)
                txtReturnPrintNo.Text = ""
                PnlReturnprint.Visible = False
                txtInventoryCode.Focus()
            Else
                MsgBox("This Return Number Not Found... Please Give Correct Return No", MsgBoxStyle.Critical)
                txtReturnPrintNo.Clear()
                txtReturnPrintNo.Focus()
            End If
            Ds = Nothing
            ObjSalesReturn = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            Ds = Nothing
        End Try
    End Sub
    Private Sub tbnInventorySearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbnInventorySearch.Click
        Try
            Dim Search As String
            Search = " SalesAmount <> 0 "
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblInventory", "L", 4, "Description", Search, "InventoryCode", "InventoryCode", 80, "Description", "Description", 180, "JobSheetNo", "JobSheetNo", 60, "PurchaseBillNo", "PurchaseBillNo", 80)
            frmHelpClass.ShowDialog()
            txtInventoryCode.Text = frmHelpClass.Var1.Trim
            If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub btnReturnprintCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReturnprintCancel.Click
        Try
            sNextRetNo = ""
            PnlReturnprint.Visible = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnCustomerCodeSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomerCodeSearch.Click
        Try
            ' Dim frmHelpCustomer As New frmHelpSearch(0, 0, "tblCustomer", "L", 4, "CustomerName", "", "CustomerCode", "CustomerCode", 80, "CustomerName", "CustomerName", 150, "Address1", "Address1", 90, "Address2", "Address2", 80)
            Dim frmHelpCustomer As New frmHelpSearch(0, 0, "tblCustomer", "R", 4, "CustomerName", "", "CustomerCode", "CustomerCode", 80, "CustomerName", "CustomerName", 150, "NricNo", "NricNo", 90, "Address1", "Address1", 80)
            frmHelpCustomer.ShowDialog()
            txtCustomerCode.Text = frmHelpCustomer.Var1.Trim
            txtCustomerName.Text = frmHelpCustomer.Var2.Trim
            txtCustomerCode_LostFocus(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub



    Private Sub txtCustomerCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustomerCode.LostFocus
        Dim objCust As New clsCustomer
        Dim dsCustomer As New DataSet

        Try

            If UCase(txtCustomerCode.Text.Trim) = "CASH" And sMemberModule = "1" Then
                pnlMember.Visible = True
                btnCustomerCodeSearch.Enabled = False
                txtMemberCode.Visible = True
                txtMemberCode.Text = ""
                txtMemberCodeView.Clear()
                txtMemberCodeView.Focus()
            Else
                pnlMember.Visible = False
                btnCustomerCodeSearch.Enabled = True
                txtMemberCode.Visible = False
                txtMemberCode.Text = ""
                txtMemberCodeView.Clear()
                If Trim(txtCustomerCode.Text) <> "" Then
                    dsCustomer = objCust.GetCustomerwithCodeHQ(txtCustomerCode.Text)
                    If dsCustomer.Tables(0).Rows.Count > 0 Then
                        txtCustomerCode.Text = dsCustomer.Tables(0).Rows(0)("CustomerCode") & ""
                        txtCustomerName.Text = dsCustomer.Tables(0).Rows(0)("CustomerName") & ""
                        txtAddress1.Text = Trim(dsCustomer.Tables(0).Rows(0)("Address1") & "")
                        txtAddress2.Text = Trim(dsCustomer.Tables(0).Rows(0)("Address2") & "")
                        txtAddress3.Text = Trim(dsCustomer.Tables(0).Rows(0)("Address3") & "")
                        lblCustomerType.Text = Trim(dsCustomer.Tables(0).Rows(0)("CustomerType") & "")
                    Else
                        dEmpty = dEmpty + 1
                        If dEmpty = 3 Then
                            btnClose_Click(sender, e)
                            Exit Sub
                        End If
                        txtCustomerCode.Text = ""
                        txtCustomerCode.Focus()
                    End If
                Else
                    dEmpty = dEmpty + 1
                    If dEmpty = 3 Then
                        btnClose_Click(sender, e)
                        Exit Sub
                    End If
                    txtCustomerCode.Text = ""
                End If

            End If

        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

    End Sub

    Private Sub txtCustomerCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCustomerCode.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then btnCustomerCodeSearch_Click(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        sMemberCode = ""
        Dim frmMem As New frmMember
        frmMem.ShowDialog()
        txtMemberCodeView.Text = sMemberCode.Trim
        btnOK.Focus()
        frmMem = Nothing

    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim ds As New DataSet
        Try
            If txtMemberCodeView.Text.Trim <> "" Then
                ds = objMember.SubGetMemberCode(Trim(txtMemberCodeView.Text))
                If ds.Tables(0).Rows.Count > 0 Then
                    txtMemberCode.Text = Trim(ds.Tables(0).Rows(0)("MemberCode") & "")
                    txtCustomerName.Text = Trim(ds.Tables(0).Rows(0)("MemberName") & "")
                    lblCustomerType.Text = Trim(ds.Tables(0).Rows(0)("MemberType") & "")
                    txtAddress1.Text = Trim(ds.Tables(0).Rows(0)("Address1") & "")
                    txtAddress2.Text = Trim(ds.Tables(0).Rows(0)("Address2") & "")
                    txtAddress3.Text = Trim(ds.Tables(0).Rows(0)("Address3") & "")
                    pnlMember.Visible = False
                    btnCustomerCodeSearch.Enabled = False
                    txtCustomerName.Focus()
                Else
                    MsgBox("No such Member ! ", MsgBoxStyle.Critical)
                    txtMemberCode.Text = ""
                    txtCustomerName.Text = ""
                    lblCustomerType.Text = ""
                    txtAddress1.Text = ""
                    txtAddress2.Text = ""
                    txtAddress3.Text = ""
                    txtMemberCodeView.Text = ""
                    pnlMember.Visible = False
                    btnCustomerCodeSearch.Enabled = True
                    txtCustomerCode.Focus()

                End If
            Else
                pnlMember.Visible = False
                btnCustomerCodeSearch.Enabled = True
                txtCustomerCode.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            ds = Nothing
        End Try

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        pnlMember.Visible = False
        btnCustomerCodeSearch.Enabled = True
        txtCustomerCode.Focus()

    End Sub

    Private Sub btnMemberCodeSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMemberCodeSearch.Click
        Try
            Dim frmHelpCustomer As New frmHelpSearch(0, 0, "tblMember", "L", 4, "MemberName", "", "MemberCode", "MemberCode", 80, "MemberName", "MemberName", 150, "Address1", "Address1", 90, "Address2", "Address2", 80)
            frmHelpCustomer.ShowDialog()
            txtMemberCodeView.Text = frmHelpCustomer.Var1.Trim
            btnOK.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    ''HAj AS TradeIn GST Calculation
    Private Sub subCalculateTradeInGST()
        Dim dGstTradeIn As Double
        Dim i As Integer
        dGstTradeIn = 0
        If grdSalesReturn.Rows.Count = 0 Then Exit Sub
        For i = 0 To grdSalesReturn.Rows.Count - 1
            dGstTradeIn = dGstTradeIn + Val(fnGetdgvText(grdSalesReturn, i, 9))
        Next i
        lblGST.Text = Format(Val(dGstTradeIn), "0.0000")

    End Sub

    
    Private Sub txtBillNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBillNo.TextChanged

    End Sub
    Private Sub subGetBCAdminPasword()
        Try
            pnlBillCancelAdminPwd.Visible = True
            pnlBillCancelAdminPwd.BringToFront()
            txtBCAdminPassword.Clear()
            txtBCAdminPassword.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnBCAdminPwdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBCAdminPwdCancel.Click
        Try
            txtBCAdminPassword.Clear()
            pnlBillCancelAdminPwd.Visible = False
            bBCPassword = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnBCAdminPwdOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBCAdminPwdOK.Click
        Dim dsPWD As New DataSet
        Dim dsIns As New DataSet
        Dim sMsg As String
        Try
            If Trim(txtBCAdminPassword.Text) <> "" Then
                dsPWD = objUM.GetAdminPassword(Trim(txtBCAdminPassword.Text))
                If dsPWD.Tables(0).Rows.Count > 0 Then
                    If Trim(UCase(dsPWD.Tables(0).Rows(0)("UserGroupCode") & "")) = "ADMIN" Then
                        sUserCode = Trim(UCase(dsPWD.Tables(0).Rows(0)("UserCode") & ""))
                        sUserName = Trim(UCase(dsPWD.Tables(0).Rows(0)("UserName") & ""))
                        bBCPassword = True

                        'If PnlReprint.Visible = True Then
                        '    Call subPrintBill(Trim(txtRePrintBillNo.Text))
                        '    bReprint = False
                        '    PnlReprint.Visible = False
                        '    subClear()
                        'Else
                        '    Payment()
                        'End If
                        Dim objSalesMan As New clsSalesMan
                        If objSalesMan.SalesManLogSave(sUserCode, sUserName, "Sales Return", nAllowSalesReturn) Then
                        Else
                            MessageBox.Show("Error in Log Save")
                        End If
                        pnlBillCancelAdminPwd.Visible = False
                    Else
                        'MessageBox.Show("Please Check the Password")
                        MsgBox("SalesMan Has No Rights to do Sales Return!", MsgBoxStyle.Critical)
                        txtBCAdminPassword.Text = ""
                        If (txtBCAdminPassword.Enabled And txtBCAdminPassword.Visible) Then txtBCAdminPassword.Focus()
                        bBCPassword = False
                        Exit Sub
                    End If
                Else
                    ' MessageBox.Show("Please Check the Password")
                    MsgBox("SalesMan Has No Rights to do Sales Return!", MsgBoxStyle.Critical)
                    txtBCAdminPassword.Text = ""
                    If (txtBCAdminPassword.Enabled And txtBCAdminPassword.Visible) Then txtBCAdminPassword.Focus()
                    bBCPassword = False
                    Exit Sub
                End If
            Else
                MessageBox.Show("Enter the Password")
                txtBCAdminPassword.Focus()
                bBCPassword = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
