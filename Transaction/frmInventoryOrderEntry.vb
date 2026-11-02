Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frmInventoryOrderEntry
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
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnOrderNoSearch As System.Windows.Forms.Button
    Friend WithEvents txtOrderNo As System.Windows.Forms.TextBox
    Friend WithEvents lblNo As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtOYYYY As System.Windows.Forms.TextBox
    Friend WithEvents txtOMM As System.Windows.Forms.TextBox
    Friend WithEvents txtODD As System.Windows.Forms.TextBox
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents btnCustomer As System.Windows.Forms.Button
    Friend WithEvents txtNRICNo As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents btnCustomerCodeSearch As System.Windows.Forms.Button
    Friend WithEvents txtCustomerCode As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtCAddress3 As System.Windows.Forms.TextBox
    Friend WithEvents txtCAddress2 As System.Windows.Forms.TextBox
    Friend WithEvents txtCAddress1 As System.Windows.Forms.TextBox
    Friend WithEvents txtSupplierName As System.Windows.Forms.TextBox
    Friend WithEvents btnSupplierCodeSearch As System.Windows.Forms.Button
    Friend WithEvents txtSupplierCode As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtSAddress3 As System.Windows.Forms.TextBox
    Friend WithEvents txtSAddress2 As System.Windows.Forms.TextBox
    Friend WithEvents txtSAddress1 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents optRepair As System.Windows.Forms.RadioButton
    Friend WithEvents optOrder As System.Windows.Forms.RadioButton
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents optDeliveryDayRate As System.Windows.Forms.RadioButton
    Friend WithEvents optTodayRate As System.Windows.Forms.RadioButton
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txtEstiYY As System.Windows.Forms.TextBox
    Friend WithEvents txtEstiMM As System.Windows.Forms.TextBox
    Friend WithEvents txtEstiDD As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents txtPurchaseBillNo As System.Windows.Forms.TextBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents txtSMC As System.Windows.Forms.TextBox
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents txtPMC As System.Windows.Forms.TextBox
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents txtInvCode As System.Windows.Forms.TextBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents txtActualGoldLoss As System.Windows.Forms.TextBox
    Friend WithEvents txtActualTotalCost As System.Windows.Forms.TextBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents txtDeliYY As System.Windows.Forms.TextBox
    Friend WithEvents txtDeliMM As System.Windows.Forms.TextBox
    Friend WithEvents txtDeliDD As System.Windows.Forms.TextBox
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents lblOrderStatus As System.Windows.Forms.Label
    Friend WithEvents txtActualCostPerGram As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents txtEstiWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents txtCurrCostPerGram As System.Windows.Forms.TextBox
    Friend WithEvents txtEstiTotalCost As System.Windows.Forms.TextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents optBoth As System.Windows.Forms.RadioButton
    Friend WithEvents optCash As System.Windows.Forms.RadioButton
    Friend WithEvents optGold As System.Windows.Forms.RadioButton
    Friend WithEvents txtAdvancePaid As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents btnTradeIn As System.Windows.Forms.Button
    Friend WithEvents txtBalanceAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnClassSearch As System.Windows.Forms.Button
    Friend WithEvents txtClass As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnCategorySearch As System.Windows.Forms.Button
    Friend WithEvents txtCategory As System.Windows.Forms.TextBox
    Friend WithEvents btnCalalogueSearch As System.Windows.Forms.Button
    Friend WithEvents txtCalalogue As System.Windows.Forms.TextBox
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtStoneDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtCost As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtMC As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtGoldLoss As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents grdOrderEntry As AxFPSpread.AxvaSpread
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtSupplierCost As System.Windows.Forms.TextBox
    Friend WithEvents txtActualWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalCost As System.Windows.Forms.TextBox
    Friend WithEvents txtStoneWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtWeight As System.Windows.Forms.TextBox
    Friend WithEvents cmdPrint As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents pnlAdminPwd As System.Windows.Forms.Panel
    Friend WithEvents btnAdminPwdCancel As System.Windows.Forms.Button
    Friend WithEvents btnAdminPwdOK As System.Windows.Forms.Button
    Friend WithEvents txtAdminPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents btnItemDelete As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInventoryOrderEntry))
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtSupplierCost = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtBalanceAmount = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.txtAdvancePaid = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.btnTradeIn = New System.Windows.Forms.Button()
        Me.txtEstiTotalCost = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.txtCurrCostPerGram = New System.Windows.Forms.TextBox()
        Me.txtEstiWeight = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.txtEstiYY = New System.Windows.Forms.TextBox()
        Me.txtEstiMM = New System.Windows.Forms.TextBox()
        Me.txtEstiDD = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.optDeliveryDayRate = New System.Windows.Forms.RadioButton()
        Me.optTodayRate = New System.Windows.Forms.RadioButton()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.optRepair = New System.Windows.Forms.RadioButton()
        Me.optOrder = New System.Windows.Forms.RadioButton()
        Me.txtSupplierName = New System.Windows.Forms.TextBox()
        Me.btnSupplierCodeSearch = New System.Windows.Forms.Button()
        Me.txtSupplierCode = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtSAddress3 = New System.Windows.Forms.TextBox()
        Me.txtSAddress2 = New System.Windows.Forms.TextBox()
        Me.txtSAddress1 = New System.Windows.Forms.TextBox()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.btnCustomer = New System.Windows.Forms.Button()
        Me.txtNRICNo = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.btnCustomerCodeSearch = New System.Windows.Forms.Button()
        Me.txtCustomerCode = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtCAddress3 = New System.Windows.Forms.TextBox()
        Me.txtCAddress2 = New System.Windows.Forms.TextBox()
        Me.txtCAddress1 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtOYYYY = New System.Windows.Forms.TextBox()
        Me.txtOMM = New System.Windows.Forms.TextBox()
        Me.txtODD = New System.Windows.Forms.TextBox()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.btnOrderNoSearch = New System.Windows.Forms.Button()
        Me.txtOrderNo = New System.Windows.Forms.TextBox()
        Me.lblNo = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.optBoth = New System.Windows.Forms.RadioButton()
        Me.optCash = New System.Windows.Forms.RadioButton()
        Me.optGold = New System.Windows.Forms.RadioButton()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.txtPurchaseBillNo = New System.Windows.Forms.TextBox()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.txtSMC = New System.Windows.Forms.TextBox()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.txtPMC = New System.Windows.Forms.TextBox()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.txtInvCode = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.txtActualGoldLoss = New System.Windows.Forms.TextBox()
        Me.txtActualTotalCost = New System.Windows.Forms.TextBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.txtDeliYY = New System.Windows.Forms.TextBox()
        Me.txtDeliMM = New System.Windows.Forms.TextBox()
        Me.txtDeliDD = New System.Windows.Forms.TextBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.txtActualWeight = New System.Windows.Forms.TextBox()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.lblOrderStatus = New System.Windows.Forms.Label()
        Me.txtActualCostPerGram = New System.Windows.Forms.TextBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnItemDelete = New System.Windows.Forms.Button()
        Me.txtStoneWeight = New System.Windows.Forms.TextBox()
        Me.txtWeight = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtGoldLoss = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtTotalCost = New System.Windows.Forms.TextBox()
        Me.txtMC = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtCost = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtStoneDescription = New System.Windows.Forms.TextBox()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.btnCalalogueSearch = New System.Windows.Forms.Button()
        Me.txtCalalogue = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnCategorySearch = New System.Windows.Forms.Button()
        Me.txtCategory = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnClassSearch = New System.Windows.Forms.Button()
        Me.txtClass = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.grdOrderEntry = New AxFPSpread.AxvaSpread()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.pnlAdminPwd = New System.Windows.Forms.Panel()
        Me.btnAdminPwdCancel = New System.Windows.Forms.Button()
        Me.btnAdminPwdOK = New System.Windows.Forms.Button()
        Me.txtAdminPassword = New System.Windows.Forms.TextBox()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.grdOrderEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlAdminPwd.SuspendLayout()
        Me.SuspendLayout()
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(0, 0)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(210, 75)
        Me.pic.TabIndex = 113
        Me.pic.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txtSupplierCost)
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtRemarks)
        Me.Panel1.Controls.Add(Me.txtBalanceAmount)
        Me.Panel1.Controls.Add(Me.Label36)
        Me.Panel1.Controls.Add(Me.txtAdvancePaid)
        Me.Panel1.Controls.Add(Me.Label35)
        Me.Panel1.Controls.Add(Me.btnTradeIn)
        Me.Panel1.Controls.Add(Me.txtEstiTotalCost)
        Me.Panel1.Controls.Add(Me.Label34)
        Me.Panel1.Controls.Add(Me.Label49)
        Me.Panel1.Controls.Add(Me.txtCurrCostPerGram)
        Me.Panel1.Controls.Add(Me.txtEstiWeight)
        Me.Panel1.Controls.Add(Me.Label22)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label32)
        Me.Panel1.Controls.Add(Me.Label31)
        Me.Panel1.Controls.Add(Me.txtEstiYY)
        Me.Panel1.Controls.Add(Me.txtEstiMM)
        Me.Panel1.Controls.Add(Me.txtEstiDD)
        Me.Panel1.Controls.Add(Me.Label33)
        Me.Panel1.Controls.Add(Me.Label50)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Controls.Add(Me.txtSupplierName)
        Me.Panel1.Controls.Add(Me.btnSupplierCodeSearch)
        Me.Panel1.Controls.Add(Me.txtSupplierCode)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Controls.Add(Me.txtSAddress3)
        Me.Panel1.Controls.Add(Me.txtSAddress2)
        Me.Panel1.Controls.Add(Me.txtSAddress1)
        Me.Panel1.Controls.Add(Me.Label46)
        Me.Panel1.Controls.Add(Me.btnCustomer)
        Me.Panel1.Controls.Add(Me.txtNRICNo)
        Me.Panel1.Controls.Add(Me.Label20)
        Me.Panel1.Controls.Add(Me.txtCustomerName)
        Me.Panel1.Controls.Add(Me.btnCustomerCodeSearch)
        Me.Panel1.Controls.Add(Me.txtCustomerCode)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.txtCAddress3)
        Me.Panel1.Controls.Add(Me.txtCAddress2)
        Me.Panel1.Controls.Add(Me.txtCAddress1)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.txtOYYYY)
        Me.Panel1.Controls.Add(Me.txtOMM)
        Me.Panel1.Controls.Add(Me.txtODD)
        Me.Panel1.Controls.Add(Me.lblDate)
        Me.Panel1.Controls.Add(Me.btnOrderNoSearch)
        Me.Panel1.Controls.Add(Me.txtOrderNo)
        Me.Panel1.Controls.Add(Me.lblNo)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Location = New System.Drawing.Point(9, 70)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(912, 296)
        Me.Panel1.TabIndex = 114
        '
        'txtSupplierCost
        '
        Me.txtSupplierCost.BackColor = System.Drawing.Color.White
        Me.txtSupplierCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierCost.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierCost.ForeColor = System.Drawing.Color.Black
        Me.txtSupplierCost.Location = New System.Drawing.Point(552, 176)
        Me.txtSupplierCost.MaxLength = 20
        Me.txtSupplierCost.Name = "txtSupplierCost"
        Me.txtSupplierCost.Size = New System.Drawing.Size(128, 22)
        Me.txtSupplierCost.TabIndex = 752
        Me.txtSupplierCost.Tag = ""
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(456, 184)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(80, 15)
        Me.Label18.TabIndex = 751
        Me.Label18.Text = "Supplier Cost"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(8, 268)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 15)
        Me.Label5.TabIndex = 750
        Me.Label5.Text = "Remarks"
        '
        'txtRemarks
        '
        Me.txtRemarks.BackColor = System.Drawing.Color.White
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.Black
        Me.txtRemarks.Location = New System.Drawing.Point(112, 264)
        Me.txtRemarks.MaxLength = 250
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRemarks.Size = New System.Drawing.Size(472, 22)
        Me.txtRemarks.TabIndex = 749
        Me.txtRemarks.Tag = ""
        '
        'txtBalanceAmount
        '
        Me.txtBalanceAmount.BackColor = System.Drawing.Color.White
        Me.txtBalanceAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBalanceAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBalanceAmount.ForeColor = System.Drawing.Color.Black
        Me.txtBalanceAmount.Location = New System.Drawing.Point(680, 240)
        Me.txtBalanceAmount.MaxLength = 20
        Me.txtBalanceAmount.Name = "txtBalanceAmount"
        Me.txtBalanceAmount.ReadOnly = True
        Me.txtBalanceAmount.Size = New System.Drawing.Size(160, 22)
        Me.txtBalanceAmount.TabIndex = 747
        Me.txtBalanceAmount.Tag = ""
        Me.txtBalanceAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label36
        '
        Me.Label36.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.Color.Black
        Me.Label36.Location = New System.Drawing.Point(584, 244)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(103, 15)
        Me.Label36.TabIndex = 748
        Me.Label36.Text = "Balance Amount"
        '
        'txtAdvancePaid
        '
        Me.txtAdvancePaid.BackColor = System.Drawing.Color.White
        Me.txtAdvancePaid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdvancePaid.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdvancePaid.ForeColor = System.Drawing.Color.Black
        Me.txtAdvancePaid.Location = New System.Drawing.Point(680, 216)
        Me.txtAdvancePaid.MaxLength = 20
        Me.txtAdvancePaid.Name = "txtAdvancePaid"
        Me.txtAdvancePaid.Size = New System.Drawing.Size(160, 22)
        Me.txtAdvancePaid.TabIndex = 744
        Me.txtAdvancePaid.Tag = ""
        Me.txtAdvancePaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label35
        '
        Me.Label35.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.Color.Black
        Me.Label35.Location = New System.Drawing.Point(584, 220)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(80, 15)
        Me.Label35.TabIndex = 745
        Me.Label35.Text = "Advance Paid"
        '
        'btnTradeIn
        '
        Me.btnTradeIn.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnTradeIn.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTradeIn.Image = CType(resources.GetObject("btnTradeIn.Image"), System.Drawing.Image)
        Me.btnTradeIn.Location = New System.Drawing.Point(840, 215)
        Me.btnTradeIn.Name = "btnTradeIn"
        Me.btnTradeIn.Size = New System.Drawing.Size(64, 24)
        Me.btnTradeIn.TabIndex = 746
        Me.btnTradeIn.TabStop = False
        Me.btnTradeIn.Text = "TradeIn"
        Me.btnTradeIn.UseVisualStyleBackColor = False
        Me.btnTradeIn.Visible = False
        '
        'txtEstiTotalCost
        '
        Me.txtEstiTotalCost.BackColor = System.Drawing.Color.White
        Me.txtEstiTotalCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEstiTotalCost.Enabled = False
        Me.txtEstiTotalCost.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEstiTotalCost.ForeColor = System.Drawing.Color.Black
        Me.txtEstiTotalCost.Location = New System.Drawing.Point(424, 240)
        Me.txtEstiTotalCost.MaxLength = 20
        Me.txtEstiTotalCost.Name = "txtEstiTotalCost"
        Me.txtEstiTotalCost.ReadOnly = True
        Me.txtEstiTotalCost.Size = New System.Drawing.Size(160, 22)
        Me.txtEstiTotalCost.TabIndex = 741
        Me.txtEstiTotalCost.Tag = ""
        Me.txtEstiTotalCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label34
        '
        Me.Label34.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.Color.Black
        Me.Label34.Location = New System.Drawing.Point(304, 244)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(103, 15)
        Me.Label34.TabIndex = 742
        Me.Label34.Text = "Estimated Cost"
        '
        'Label49
        '
        Me.Label49.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.ForeColor = System.Drawing.Color.Black
        Me.Label49.Location = New System.Drawing.Point(304, 220)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(96, 15)
        Me.Label49.TabIndex = 740
        Me.Label49.Text = "Cost Per Gram"
        '
        'txtCurrCostPerGram
        '
        Me.txtCurrCostPerGram.BackColor = System.Drawing.Color.White
        Me.txtCurrCostPerGram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrCostPerGram.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrCostPerGram.ForeColor = System.Drawing.Color.Black
        Me.txtCurrCostPerGram.Location = New System.Drawing.Point(424, 216)
        Me.txtCurrCostPerGram.MaxLength = 20
        Me.txtCurrCostPerGram.Name = "txtCurrCostPerGram"
        Me.txtCurrCostPerGram.Size = New System.Drawing.Size(160, 22)
        Me.txtCurrCostPerGram.TabIndex = 739
        Me.txtCurrCostPerGram.Tag = ""
        Me.txtCurrCostPerGram.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtEstiWeight
        '
        Me.txtEstiWeight.BackColor = System.Drawing.Color.White
        Me.txtEstiWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEstiWeight.Enabled = False
        Me.txtEstiWeight.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEstiWeight.ForeColor = System.Drawing.Color.Black
        Me.txtEstiWeight.Location = New System.Drawing.Point(112, 240)
        Me.txtEstiWeight.MaxLength = 20
        Me.txtEstiWeight.Name = "txtEstiWeight"
        Me.txtEstiWeight.Size = New System.Drawing.Size(160, 22)
        Me.txtEstiWeight.TabIndex = 737
        Me.txtEstiWeight.Tag = ""
        Me.txtEstiWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label22
        '
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.Black
        Me.Label22.Location = New System.Drawing.Point(8, 244)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(104, 15)
        Me.Label22.TabIndex = 738
        Me.Label22.Text = "Estimated Weight"
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Location = New System.Drawing.Point(0, 208)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(912, 2)
        Me.Panel2.TabIndex = 736
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(456, 84)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 15)
        Me.Label2.TabIndex = 735
        Me.Label2.Text = "Name"
        '
        'Label32
        '
        Me.Label32.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(136, 216)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(8, 22)
        Me.Label32.TabIndex = 733
        Me.Label32.Text = "-"
        '
        'Label31
        '
        Me.Label31.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(168, 216)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(8, 22)
        Me.Label31.TabIndex = 734
        Me.Label31.Text = "-"
        '
        'txtEstiYY
        '
        Me.txtEstiYY.BackColor = System.Drawing.Color.White
        Me.txtEstiYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEstiYY.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEstiYY.Location = New System.Drawing.Point(176, 216)
        Me.txtEstiYY.MaxLength = 4
        Me.txtEstiYY.Name = "txtEstiYY"
        Me.txtEstiYY.Size = New System.Drawing.Size(40, 22)
        Me.txtEstiYY.TabIndex = 731
        Me.txtEstiYY.Tag = "ENN;"
        '
        'txtEstiMM
        '
        Me.txtEstiMM.BackColor = System.Drawing.Color.White
        Me.txtEstiMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEstiMM.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEstiMM.Location = New System.Drawing.Point(144, 216)
        Me.txtEstiMM.MaxLength = 2
        Me.txtEstiMM.Name = "txtEstiMM"
        Me.txtEstiMM.Size = New System.Drawing.Size(24, 22)
        Me.txtEstiMM.TabIndex = 730
        Me.txtEstiMM.Tag = "ENN;"
        '
        'txtEstiDD
        '
        Me.txtEstiDD.BackColor = System.Drawing.Color.White
        Me.txtEstiDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEstiDD.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEstiDD.Location = New System.Drawing.Point(112, 216)
        Me.txtEstiDD.MaxLength = 2
        Me.txtEstiDD.Name = "txtEstiDD"
        Me.txtEstiDD.Size = New System.Drawing.Size(24, 22)
        Me.txtEstiDD.TabIndex = 729
        Me.txtEstiDD.Tag = "ENN;"
        '
        'Label33
        '
        Me.Label33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.Black
        Me.Label33.Location = New System.Drawing.Point(8, 220)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(96, 15)
        Me.Label33.TabIndex = 732
        Me.Label33.Text = "Date of Delivery"
        '
        'Label50
        '
        Me.Label50.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.ForeColor = System.Drawing.Color.Black
        Me.Label50.Location = New System.Drawing.Point(560, 16)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(96, 15)
        Me.Label50.TabIndex = 728
        Me.Label50.Text = "Rate Fixing"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.optDeliveryDayRate)
        Me.GroupBox1.Controls.Add(Me.optTodayRate)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(664, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(224, 40)
        Me.GroupBox1.TabIndex = 727
        Me.GroupBox1.TabStop = False
        '
        'optDeliveryDayRate
        '
        Me.optDeliveryDayRate.Checked = True
        Me.optDeliveryDayRate.Location = New System.Drawing.Point(96, 11)
        Me.optDeliveryDayRate.Name = "optDeliveryDayRate"
        Me.optDeliveryDayRate.Size = New System.Drawing.Size(118, 21)
        Me.optDeliveryDayRate.TabIndex = 5
        Me.optDeliveryDayRate.TabStop = True
        Me.optDeliveryDayRate.Text = "Delivery Day Rate"
        '
        'optTodayRate
        '
        Me.optTodayRate.Location = New System.Drawing.Point(8, 11)
        Me.optTodayRate.Name = "optTodayRate"
        Me.optTodayRate.Size = New System.Drawing.Size(83, 21)
        Me.optTodayRate.TabIndex = 4
        Me.optTodayRate.Text = "Today Rate"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.optRepair)
        Me.GroupBox3.Controls.Add(Me.optOrder)
        Me.GroupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(304, 8)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(144, 32)
        Me.GroupBox3.TabIndex = 726
        Me.GroupBox3.TabStop = False
        '
        'optRepair
        '
        Me.optRepair.Location = New System.Drawing.Point(72, 10)
        Me.optRepair.Name = "optRepair"
        Me.optRepair.Size = New System.Drawing.Size(64, 15)
        Me.optRepair.TabIndex = 19
        Me.optRepair.Text = "Repair"
        '
        'optOrder
        '
        Me.optOrder.Checked = True
        Me.optOrder.Location = New System.Drawing.Point(8, 10)
        Me.optOrder.Name = "optOrder"
        Me.optOrder.Size = New System.Drawing.Size(56, 15)
        Me.optOrder.TabIndex = 17
        Me.optOrder.TabStop = True
        Me.optOrder.Text = "Order"
        '
        'txtSupplierName
        '
        Me.txtSupplierName.BackColor = System.Drawing.Color.White
        Me.txtSupplierName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierName.ForeColor = System.Drawing.Color.Black
        Me.txtSupplierName.Location = New System.Drawing.Point(552, 80)
        Me.txtSupplierName.MaxLength = 20
        Me.txtSupplierName.Name = "txtSupplierName"
        Me.txtSupplierName.Size = New System.Drawing.Size(336, 22)
        Me.txtSupplierName.TabIndex = 721
        Me.txtSupplierName.Tag = ""
        '
        'btnSupplierCodeSearch
        '
        Me.btnSupplierCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSupplierCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSupplierCodeSearch.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSupplierCodeSearch.Location = New System.Drawing.Point(704, 56)
        Me.btnSupplierCodeSearch.Name = "btnSupplierCodeSearch"
        Me.btnSupplierCodeSearch.Size = New System.Drawing.Size(24, 22)
        Me.btnSupplierCodeSearch.TabIndex = 720
        Me.btnSupplierCodeSearch.TabStop = False
        Me.btnSupplierCodeSearch.Text = "..."
        Me.btnSupplierCodeSearch.UseVisualStyleBackColor = False
        '
        'txtSupplierCode
        '
        Me.txtSupplierCode.BackColor = System.Drawing.Color.White
        Me.txtSupplierCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierCode.ForeColor = System.Drawing.Color.Black
        Me.txtSupplierCode.Location = New System.Drawing.Point(552, 56)
        Me.txtSupplierCode.MaxLength = 20
        Me.txtSupplierCode.Name = "txtSupplierCode"
        Me.txtSupplierCode.Size = New System.Drawing.Size(152, 22)
        Me.txtSupplierCode.TabIndex = 717
        Me.txtSupplierCode.Tag = ""
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(456, 60)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(88, 15)
        Me.Label16.TabIndex = 719
        Me.Label16.Text = "Supplier Code"
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(456, 108)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(82, 15)
        Me.Label17.TabIndex = 718
        Me.Label17.Text = "Address"
        '
        'txtSAddress3
        '
        Me.txtSAddress3.BackColor = System.Drawing.Color.White
        Me.txtSAddress3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSAddress3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSAddress3.ForeColor = System.Drawing.Color.Black
        Me.txtSAddress3.Location = New System.Drawing.Point(552, 152)
        Me.txtSAddress3.MaxLength = 20
        Me.txtSAddress3.Name = "txtSAddress3"
        Me.txtSAddress3.Size = New System.Drawing.Size(336, 22)
        Me.txtSAddress3.TabIndex = 724
        Me.txtSAddress3.Tag = ""
        '
        'txtSAddress2
        '
        Me.txtSAddress2.BackColor = System.Drawing.Color.White
        Me.txtSAddress2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSAddress2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSAddress2.ForeColor = System.Drawing.Color.Black
        Me.txtSAddress2.Location = New System.Drawing.Point(552, 128)
        Me.txtSAddress2.MaxLength = 20
        Me.txtSAddress2.Name = "txtSAddress2"
        Me.txtSAddress2.Size = New System.Drawing.Size(336, 22)
        Me.txtSAddress2.TabIndex = 723
        Me.txtSAddress2.Tag = ""
        '
        'txtSAddress1
        '
        Me.txtSAddress1.BackColor = System.Drawing.Color.White
        Me.txtSAddress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSAddress1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSAddress1.ForeColor = System.Drawing.Color.Black
        Me.txtSAddress1.Location = New System.Drawing.Point(552, 104)
        Me.txtSAddress1.MaxLength = 20
        Me.txtSAddress1.Name = "txtSAddress1"
        Me.txtSAddress1.Size = New System.Drawing.Size(336, 22)
        Me.txtSAddress1.TabIndex = 722
        Me.txtSAddress1.Tag = ""
        '
        'Label46
        '
        Me.Label46.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.Color.Black
        Me.Label46.Location = New System.Drawing.Point(8, 84)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(82, 15)
        Me.Label46.TabIndex = 710
        Me.Label46.Text = "Name"
        '
        'btnCustomer
        '
        Me.btnCustomer.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCustomer.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomer.Image = CType(resources.GetObject("btnCustomer.Image"), System.Drawing.Image)
        Me.btnCustomer.Location = New System.Drawing.Point(288, 56)
        Me.btnCustomer.Name = "btnCustomer"
        Me.btnCustomer.Size = New System.Drawing.Size(48, 22)
        Me.btnCustomer.TabIndex = 709
        Me.btnCustomer.TabStop = False
        Me.btnCustomer.Text = "NEW"
        Me.btnCustomer.UseVisualStyleBackColor = False
        '
        'txtNRICNo
        '
        Me.txtNRICNo.BackColor = System.Drawing.Color.White
        Me.txtNRICNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNRICNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNRICNo.ForeColor = System.Drawing.Color.Black
        Me.txtNRICNo.Location = New System.Drawing.Point(112, 176)
        Me.txtNRICNo.MaxLength = 20
        Me.txtNRICNo.Name = "txtNRICNo"
        Me.txtNRICNo.Size = New System.Drawing.Size(176, 22)
        Me.txtNRICNo.TabIndex = 716
        Me.txtNRICNo.Tag = ""
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.Location = New System.Drawing.Point(8, 180)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(56, 15)
        Me.Label20.TabIndex = 708
        Me.Label20.Text = "NRIC No."
        '
        'txtCustomerName
        '
        Me.txtCustomerName.BackColor = System.Drawing.Color.White
        Me.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerName.ForeColor = System.Drawing.Color.Black
        Me.txtCustomerName.Location = New System.Drawing.Point(112, 80)
        Me.txtCustomerName.MaxLength = 20
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(336, 22)
        Me.txtCustomerName.TabIndex = 712
        Me.txtCustomerName.Tag = ""
        '
        'btnCustomerCodeSearch
        '
        Me.btnCustomerCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCustomerCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCustomerCodeSearch.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomerCodeSearch.Location = New System.Drawing.Point(264, 56)
        Me.btnCustomerCodeSearch.Name = "btnCustomerCodeSearch"
        Me.btnCustomerCodeSearch.Size = New System.Drawing.Size(24, 22)
        Me.btnCustomerCodeSearch.TabIndex = 711
        Me.btnCustomerCodeSearch.TabStop = False
        Me.btnCustomerCodeSearch.Text = "..."
        Me.btnCustomerCodeSearch.UseVisualStyleBackColor = False
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.BackColor = System.Drawing.Color.White
        Me.txtCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerCode.ForeColor = System.Drawing.Color.Black
        Me.txtCustomerCode.Location = New System.Drawing.Point(112, 56)
        Me.txtCustomerCode.MaxLength = 20
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.Size = New System.Drawing.Size(152, 22)
        Me.txtCustomerCode.TabIndex = 705
        Me.txtCustomerCode.Tag = ""
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(8, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 15)
        Me.Label1.TabIndex = 707
        Me.Label1.Text = "Customer Code"
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(8, 108)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(82, 15)
        Me.Label13.TabIndex = 706
        Me.Label13.Text = "Address"
        '
        'txtCAddress3
        '
        Me.txtCAddress3.BackColor = System.Drawing.Color.White
        Me.txtCAddress3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCAddress3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCAddress3.ForeColor = System.Drawing.Color.Black
        Me.txtCAddress3.Location = New System.Drawing.Point(112, 152)
        Me.txtCAddress3.MaxLength = 20
        Me.txtCAddress3.Name = "txtCAddress3"
        Me.txtCAddress3.Size = New System.Drawing.Size(336, 22)
        Me.txtCAddress3.TabIndex = 715
        Me.txtCAddress3.Tag = ""
        '
        'txtCAddress2
        '
        Me.txtCAddress2.BackColor = System.Drawing.Color.White
        Me.txtCAddress2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCAddress2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCAddress2.ForeColor = System.Drawing.Color.Black
        Me.txtCAddress2.Location = New System.Drawing.Point(112, 128)
        Me.txtCAddress2.MaxLength = 20
        Me.txtCAddress2.Name = "txtCAddress2"
        Me.txtCAddress2.Size = New System.Drawing.Size(336, 22)
        Me.txtCAddress2.TabIndex = 714
        Me.txtCAddress2.Tag = ""
        '
        'txtCAddress1
        '
        Me.txtCAddress1.BackColor = System.Drawing.Color.White
        Me.txtCAddress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCAddress1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCAddress1.ForeColor = System.Drawing.Color.Black
        Me.txtCAddress1.Location = New System.Drawing.Point(112, 104)
        Me.txtCAddress1.MaxLength = 20
        Me.txtCAddress1.Name = "txtCAddress1"
        Me.txtCAddress1.Size = New System.Drawing.Size(336, 22)
        Me.txtCAddress1.TabIndex = 713
        Me.txtCAddress1.Tag = ""
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(168, 32)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(8, 16)
        Me.Label6.TabIndex = 209
        Me.Label6.Text = "-"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(136, 32)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(8, 16)
        Me.Label8.TabIndex = 208
        Me.Label8.Text = "-"
        '
        'txtOYYYY
        '
        Me.txtOYYYY.BackColor = System.Drawing.Color.White
        Me.txtOYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOYYYY.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOYYYY.Location = New System.Drawing.Point(176, 32)
        Me.txtOYYYY.MaxLength = 4
        Me.txtOYYYY.Name = "txtOYYYY"
        Me.txtOYYYY.ReadOnly = True
        Me.txtOYYYY.Size = New System.Drawing.Size(32, 22)
        Me.txtOYYYY.TabIndex = 206
        Me.txtOYYYY.Tag = "ENN;"
        '
        'txtOMM
        '
        Me.txtOMM.BackColor = System.Drawing.Color.White
        Me.txtOMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOMM.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOMM.Location = New System.Drawing.Point(144, 32)
        Me.txtOMM.MaxLength = 2
        Me.txtOMM.Name = "txtOMM"
        Me.txtOMM.ReadOnly = True
        Me.txtOMM.Size = New System.Drawing.Size(24, 22)
        Me.txtOMM.TabIndex = 205
        Me.txtOMM.Tag = "ENN;"
        '
        'txtODD
        '
        Me.txtODD.BackColor = System.Drawing.Color.White
        Me.txtODD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtODD.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtODD.Location = New System.Drawing.Point(112, 32)
        Me.txtODD.MaxLength = 2
        Me.txtODD.Name = "txtODD"
        Me.txtODD.ReadOnly = True
        Me.txtODD.Size = New System.Drawing.Size(24, 22)
        Me.txtODD.TabIndex = 204
        Me.txtODD.Tag = "ENN;"
        '
        'lblDate
        '
        Me.lblDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.ForeColor = System.Drawing.Color.Black
        Me.lblDate.Location = New System.Drawing.Point(8, 33)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(72, 20)
        Me.lblDate.TabIndex = 207
        Me.lblDate.Text = "Order Date"
        '
        'btnOrderNoSearch
        '
        Me.btnOrderNoSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnOrderNoSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOrderNoSearch.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOrderNoSearch.Location = New System.Drawing.Point(264, 8)
        Me.btnOrderNoSearch.Name = "btnOrderNoSearch"
        Me.btnOrderNoSearch.Size = New System.Drawing.Size(24, 22)
        Me.btnOrderNoSearch.TabIndex = 203
        Me.btnOrderNoSearch.TabStop = False
        Me.btnOrderNoSearch.Text = "..."
        Me.btnOrderNoSearch.UseVisualStyleBackColor = False
        '
        'txtOrderNo
        '
        Me.txtOrderNo.BackColor = System.Drawing.Color.White
        Me.txtOrderNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOrderNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOrderNo.ForeColor = System.Drawing.Color.Black
        Me.txtOrderNo.Location = New System.Drawing.Point(112, 8)
        Me.txtOrderNo.MaxLength = 20
        Me.txtOrderNo.Name = "txtOrderNo"
        Me.txtOrderNo.Size = New System.Drawing.Size(152, 22)
        Me.txtOrderNo.TabIndex = 201
        Me.txtOrderNo.Tag = ""
        '
        'lblNo
        '
        Me.lblNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNo.ForeColor = System.Drawing.Color.Black
        Me.lblNo.Location = New System.Drawing.Point(8, 8)
        Me.lblNo.Name = "lblNo"
        Me.lblNo.Size = New System.Drawing.Size(64, 15)
        Me.lblNo.TabIndex = 202
        Me.lblNo.Text = "Order No"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.optBoth)
        Me.GroupBox2.Controls.Add(Me.optCash)
        Me.GroupBox2.Controls.Add(Me.optGold)
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(688, 172)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(200, 32)
        Me.GroupBox2.TabIndex = 743
        Me.GroupBox2.TabStop = False
        '
        'optBoth
        '
        Me.optBoth.Location = New System.Drawing.Point(144, 10)
        Me.optBoth.Name = "optBoth"
        Me.optBoth.Size = New System.Drawing.Size(48, 15)
        Me.optBoth.TabIndex = 19
        Me.optBoth.Text = "Both"
        '
        'optCash
        '
        Me.optCash.Checked = True
        Me.optCash.Location = New System.Drawing.Point(72, 10)
        Me.optCash.Name = "optCash"
        Me.optCash.Size = New System.Drawing.Size(56, 15)
        Me.optCash.TabIndex = 18
        Me.optCash.TabStop = True
        Me.optCash.Text = "Cash"
        '
        'optGold
        '
        Me.optGold.Location = New System.Drawing.Point(8, 10)
        Me.optGold.Name = "optGold"
        Me.optGold.Size = New System.Drawing.Size(56, 15)
        Me.optGold.TabIndex = 17
        Me.optGold.Text = "Gold"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.txtPurchaseBillNo)
        Me.Panel3.Controls.Add(Me.Label43)
        Me.Panel3.Controls.Add(Me.Label38)
        Me.Panel3.Controls.Add(Me.txtSMC)
        Me.Panel3.Controls.Add(Me.Label52)
        Me.Panel3.Controls.Add(Me.txtPMC)
        Me.Panel3.Controls.Add(Me.Label53)
        Me.Panel3.Controls.Add(Me.Label54)
        Me.Panel3.Controls.Add(Me.txtInvCode)
        Me.Panel3.Controls.Add(Me.Label37)
        Me.Panel3.Controls.Add(Me.txtActualGoldLoss)
        Me.Panel3.Controls.Add(Me.txtActualTotalCost)
        Me.Panel3.Controls.Add(Me.Label39)
        Me.Panel3.Controls.Add(Me.Label40)
        Me.Panel3.Controls.Add(Me.Label41)
        Me.Panel3.Controls.Add(Me.txtDeliYY)
        Me.Panel3.Controls.Add(Me.txtDeliMM)
        Me.Panel3.Controls.Add(Me.txtDeliDD)
        Me.Panel3.Controls.Add(Me.Label42)
        Me.Panel3.Controls.Add(Me.txtActualWeight)
        Me.Panel3.Controls.Add(Me.Label44)
        Me.Panel3.Controls.Add(Me.lblOrderStatus)
        Me.Panel3.Controls.Add(Me.txtActualCostPerGram)
        Me.Panel3.Location = New System.Drawing.Point(9, 606)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(912, 80)
        Me.Panel3.TabIndex = 723
        '
        'txtPurchaseBillNo
        '
        Me.txtPurchaseBillNo.BackColor = System.Drawing.Color.White
        Me.txtPurchaseBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurchaseBillNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurchaseBillNo.ForeColor = System.Drawing.Color.Black
        Me.txtPurchaseBillNo.Location = New System.Drawing.Point(112, 52)
        Me.txtPurchaseBillNo.MaxLength = 20
        Me.txtPurchaseBillNo.Name = "txtPurchaseBillNo"
        Me.txtPurchaseBillNo.ReadOnly = True
        Me.txtPurchaseBillNo.Size = New System.Drawing.Size(112, 22)
        Me.txtPurchaseBillNo.TabIndex = 32
        Me.txtPurchaseBillNo.Tag = ""
        Me.txtPurchaseBillNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label43
        '
        Me.Label43.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.Color.Black
        Me.Label43.Location = New System.Drawing.Point(8, 56)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(96, 15)
        Me.Label43.TabIndex = 201
        Me.Label43.Text = "Purchase BillNo"
        '
        'Label38
        '
        Me.Label38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.Color.Black
        Me.Label38.Location = New System.Drawing.Point(264, 56)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(64, 15)
        Me.Label38.TabIndex = 199
        Me.Label38.Text = "SMC"
        '
        'txtSMC
        '
        Me.txtSMC.BackColor = System.Drawing.Color.White
        Me.txtSMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSMC.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMC.ForeColor = System.Drawing.Color.Black
        Me.txtSMC.Location = New System.Drawing.Point(368, 52)
        Me.txtSMC.MaxLength = 20
        Me.txtSMC.Name = "txtSMC"
        Me.txtSMC.ReadOnly = True
        Me.txtSMC.Size = New System.Drawing.Size(112, 22)
        Me.txtSMC.TabIndex = 35
        Me.txtSMC.Tag = ""
        Me.txtSMC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label52
        '
        Me.Label52.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.ForeColor = System.Drawing.Color.Black
        Me.Label52.Location = New System.Drawing.Point(528, 32)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(80, 15)
        Me.Label52.TabIndex = 197
        Me.Label52.Text = "Gold Loss"
        '
        'txtPMC
        '
        Me.txtPMC.BackColor = System.Drawing.Color.White
        Me.txtPMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPMC.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPMC.ForeColor = System.Drawing.Color.Black
        Me.txtPMC.Location = New System.Drawing.Point(368, 28)
        Me.txtPMC.MaxLength = 20
        Me.txtPMC.Name = "txtPMC"
        Me.txtPMC.ReadOnly = True
        Me.txtPMC.Size = New System.Drawing.Size(112, 22)
        Me.txtPMC.TabIndex = 34
        Me.txtPMC.Tag = ""
        Me.txtPMC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label53
        '
        Me.Label53.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.ForeColor = System.Drawing.Color.Black
        Me.Label53.Location = New System.Drawing.Point(264, 32)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(64, 15)
        Me.Label53.TabIndex = 195
        Me.Label53.Text = "PMC"
        '
        'Label54
        '
        Me.Label54.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label54.ForeColor = System.Drawing.Color.Black
        Me.Label54.Location = New System.Drawing.Point(264, 8)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(93, 15)
        Me.Label54.TabIndex = 193
        Me.Label54.Text = " Cost Per Gram"
        '
        'txtInvCode
        '
        Me.txtInvCode.BackColor = System.Drawing.Color.White
        Me.txtInvCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvCode.ForeColor = System.Drawing.Color.Black
        Me.txtInvCode.Location = New System.Drawing.Point(112, 4)
        Me.txtInvCode.MaxLength = 20
        Me.txtInvCode.Name = "txtInvCode"
        Me.txtInvCode.ReadOnly = True
        Me.txtInvCode.Size = New System.Drawing.Size(112, 22)
        Me.txtInvCode.TabIndex = 28
        Me.txtInvCode.Tag = ""
        Me.txtInvCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label37
        '
        Me.Label37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.Color.Black
        Me.Label37.Location = New System.Drawing.Point(8, 8)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(96, 15)
        Me.Label37.TabIndex = 190
        Me.Label37.Text = "Inventory Code"
        '
        'txtActualGoldLoss
        '
        Me.txtActualGoldLoss.BackColor = System.Drawing.Color.White
        Me.txtActualGoldLoss.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtActualGoldLoss.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtActualGoldLoss.ForeColor = System.Drawing.Color.Black
        Me.txtActualGoldLoss.Location = New System.Drawing.Point(648, 28)
        Me.txtActualGoldLoss.MaxLength = 20
        Me.txtActualGoldLoss.Name = "txtActualGoldLoss"
        Me.txtActualGoldLoss.ReadOnly = True
        Me.txtActualGoldLoss.Size = New System.Drawing.Size(112, 22)
        Me.txtActualGoldLoss.TabIndex = 37
        Me.txtActualGoldLoss.Tag = ""
        Me.txtActualGoldLoss.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtActualTotalCost
        '
        Me.txtActualTotalCost.BackColor = System.Drawing.Color.White
        Me.txtActualTotalCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtActualTotalCost.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtActualTotalCost.ForeColor = System.Drawing.Color.Black
        Me.txtActualTotalCost.Location = New System.Drawing.Point(648, 52)
        Me.txtActualTotalCost.MaxLength = 20
        Me.txtActualTotalCost.Name = "txtActualTotalCost"
        Me.txtActualTotalCost.ReadOnly = True
        Me.txtActualTotalCost.Size = New System.Drawing.Size(112, 22)
        Me.txtActualTotalCost.TabIndex = 38
        Me.txtActualTotalCost.Tag = ""
        Me.txtActualTotalCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label39
        '
        Me.Label39.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.Color.Black
        Me.Label39.Location = New System.Drawing.Point(528, 56)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(80, 15)
        Me.Label39.TabIndex = 186
        Me.Label39.Text = "Total Cost"
        '
        'Label40
        '
        Me.Label40.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(168, 28)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(8, 16)
        Me.Label40.TabIndex = 184
        Me.Label40.Text = "-"
        '
        'Label41
        '
        Me.Label41.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.Location = New System.Drawing.Point(136, 28)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(8, 16)
        Me.Label41.TabIndex = 183
        Me.Label41.Text = "-"
        '
        'txtDeliYY
        '
        Me.txtDeliYY.BackColor = System.Drawing.Color.White
        Me.txtDeliYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeliYY.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeliYY.Location = New System.Drawing.Point(176, 28)
        Me.txtDeliYY.MaxLength = 4
        Me.txtDeliYY.Name = "txtDeliYY"
        Me.txtDeliYY.ReadOnly = True
        Me.txtDeliYY.Size = New System.Drawing.Size(48, 22)
        Me.txtDeliYY.TabIndex = 31
        Me.txtDeliYY.Tag = "ENN;"
        '
        'txtDeliMM
        '
        Me.txtDeliMM.BackColor = System.Drawing.Color.White
        Me.txtDeliMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeliMM.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeliMM.Location = New System.Drawing.Point(144, 28)
        Me.txtDeliMM.MaxLength = 2
        Me.txtDeliMM.Name = "txtDeliMM"
        Me.txtDeliMM.ReadOnly = True
        Me.txtDeliMM.Size = New System.Drawing.Size(21, 22)
        Me.txtDeliMM.TabIndex = 30
        Me.txtDeliMM.Tag = "ENN;"
        '
        'txtDeliDD
        '
        Me.txtDeliDD.BackColor = System.Drawing.Color.White
        Me.txtDeliDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeliDD.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeliDD.Location = New System.Drawing.Point(112, 28)
        Me.txtDeliDD.MaxLength = 2
        Me.txtDeliDD.Name = "txtDeliDD"
        Me.txtDeliDD.ReadOnly = True
        Me.txtDeliDD.Size = New System.Drawing.Size(21, 22)
        Me.txtDeliDD.TabIndex = 29
        Me.txtDeliDD.Tag = "ENN;"
        '
        'Label42
        '
        Me.Label42.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.Color.Black
        Me.Label42.Location = New System.Drawing.Point(8, 29)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(97, 20)
        Me.Label42.TabIndex = 182
        Me.Label42.Text = "Delivered Date"
        '
        'txtActualWeight
        '
        Me.txtActualWeight.BackColor = System.Drawing.Color.White
        Me.txtActualWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtActualWeight.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtActualWeight.ForeColor = System.Drawing.Color.Black
        Me.txtActualWeight.Location = New System.Drawing.Point(648, 4)
        Me.txtActualWeight.MaxLength = 20
        Me.txtActualWeight.Name = "txtActualWeight"
        Me.txtActualWeight.ReadOnly = True
        Me.txtActualWeight.Size = New System.Drawing.Size(112, 22)
        Me.txtActualWeight.TabIndex = 36
        Me.txtActualWeight.Tag = ""
        Me.txtActualWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label44
        '
        Me.Label44.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.Color.Black
        Me.Label44.Location = New System.Drawing.Point(528, 8)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(103, 15)
        Me.Label44.TabIndex = 176
        Me.Label44.Text = "Weight"
        '
        'lblOrderStatus
        '
        Me.lblOrderStatus.BackColor = System.Drawing.Color.Yellow
        Me.lblOrderStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblOrderStatus.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOrderStatus.ForeColor = System.Drawing.Color.Red
        Me.lblOrderStatus.Location = New System.Drawing.Point(768, 4)
        Me.lblOrderStatus.Name = "lblOrderStatus"
        Me.lblOrderStatus.Size = New System.Drawing.Size(136, 70)
        Me.lblOrderStatus.TabIndex = 191
        Me.lblOrderStatus.Text = "Order Getting ..."
        Me.lblOrderStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtActualCostPerGram
        '
        Me.txtActualCostPerGram.BackColor = System.Drawing.Color.White
        Me.txtActualCostPerGram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtActualCostPerGram.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtActualCostPerGram.ForeColor = System.Drawing.Color.Black
        Me.txtActualCostPerGram.Location = New System.Drawing.Point(368, 4)
        Me.txtActualCostPerGram.MaxLength = 20
        Me.txtActualCostPerGram.Name = "txtActualCostPerGram"
        Me.txtActualCostPerGram.ReadOnly = True
        Me.txtActualCostPerGram.Size = New System.Drawing.Size(112, 22)
        Me.txtActualCostPerGram.TabIndex = 33
        Me.txtActualCostPerGram.Tag = ""
        Me.txtActualCostPerGram.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.btnItemDelete)
        Me.Panel4.Controls.Add(Me.txtStoneWeight)
        Me.Panel4.Controls.Add(Me.txtWeight)
        Me.Panel4.Controls.Add(Me.Label15)
        Me.Panel4.Controls.Add(Me.Label14)
        Me.Panel4.Controls.Add(Me.Label12)
        Me.Panel4.Controls.Add(Me.txtGoldLoss)
        Me.Panel4.Controls.Add(Me.Label11)
        Me.Panel4.Controls.Add(Me.txtTotalCost)
        Me.Panel4.Controls.Add(Me.txtMC)
        Me.Panel4.Controls.Add(Me.Label10)
        Me.Panel4.Controls.Add(Me.txtCost)
        Me.Panel4.Controls.Add(Me.Label9)
        Me.Panel4.Controls.Add(Me.txtStoneDescription)
        Me.Panel4.Controls.Add(Me.txtDescription)
        Me.Panel4.Controls.Add(Me.Label23)
        Me.Panel4.Controls.Add(Me.btnCalalogueSearch)
        Me.Panel4.Controls.Add(Me.txtCalalogue)
        Me.Panel4.Controls.Add(Me.Label7)
        Me.Panel4.Controls.Add(Me.btnCategorySearch)
        Me.Panel4.Controls.Add(Me.txtCategory)
        Me.Panel4.Controls.Add(Me.Label4)
        Me.Panel4.Controls.Add(Me.btnClassSearch)
        Me.Panel4.Controls.Add(Me.txtClass)
        Me.Panel4.Controls.Add(Me.Label3)
        Me.Panel4.Location = New System.Drawing.Point(9, 534)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(912, 73)
        Me.Panel4.TabIndex = 724
        '
        'btnItemDelete
        '
        Me.btnItemDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnItemDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnItemDelete.Image = CType(resources.GetObject("btnItemDelete.Image"), System.Drawing.Image)
        Me.btnItemDelete.Location = New System.Drawing.Point(144, 48)
        Me.btnItemDelete.Name = "btnItemDelete"
        Me.btnItemDelete.Size = New System.Drawing.Size(95, 23)
        Me.btnItemDelete.TabIndex = 731
        Me.btnItemDelete.Text = "Delete"
        Me.btnItemDelete.UseVisualStyleBackColor = False
        '
        'txtStoneWeight
        '
        Me.txtStoneWeight.BackColor = System.Drawing.Color.White
        Me.txtStoneWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStoneWeight.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStoneWeight.ForeColor = System.Drawing.Color.Black
        Me.txtStoneWeight.Location = New System.Drawing.Point(656, 49)
        Me.txtStoneWeight.MaxLength = 20
        Me.txtStoneWeight.Name = "txtStoneWeight"
        Me.txtStoneWeight.Size = New System.Drawing.Size(72, 22)
        Me.txtStoneWeight.TabIndex = 151
        Me.txtStoneWeight.Tag = ""
        '
        'txtWeight
        '
        Me.txtWeight.BackColor = System.Drawing.Color.White
        Me.txtWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWeight.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeight.ForeColor = System.Drawing.Color.Black
        Me.txtWeight.Location = New System.Drawing.Point(656, 27)
        Me.txtWeight.MaxLength = 20
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.Size = New System.Drawing.Size(72, 22)
        Me.txtWeight.TabIndex = 150
        Me.txtWeight.Tag = ""
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.White
        Me.Label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Navy
        Me.Label15.Location = New System.Drawing.Point(656, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(72, 27)
        Me.Label15.TabIndex = 160
        Me.Label15.Text = "Weight"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.White
        Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label14.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Navy
        Me.Label14.Location = New System.Drawing.Point(240, 49)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(120, 22)
        Me.Label14.TabIndex = 159
        Me.Label14.Text = "Stone Description"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.White
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label12.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Navy
        Me.Label12.Location = New System.Drawing.Point(728, 49)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(66, 22)
        Me.Label12.TabIndex = 158
        Me.Label12.Text = "Total"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtGoldLoss
        '
        Me.txtGoldLoss.BackColor = System.Drawing.Color.White
        Me.txtGoldLoss.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldLoss.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoldLoss.ForeColor = System.Drawing.Color.Black
        Me.txtGoldLoss.Location = New System.Drawing.Point(852, 27)
        Me.txtGoldLoss.MaxLength = 20
        Me.txtGoldLoss.Name = "txtGoldLoss"
        Me.txtGoldLoss.Size = New System.Drawing.Size(58, 22)
        Me.txtGoldLoss.TabIndex = 154
        Me.txtGoldLoss.Tag = ""
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.White
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Navy
        Me.Label11.Location = New System.Drawing.Point(852, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(58, 27)
        Me.Label11.TabIndex = 156
        Me.Label11.Text = "Gold Loss"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTotalCost
        '
        Me.txtTotalCost.BackColor = System.Drawing.Color.White
        Me.txtTotalCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalCost.Enabled = False
        Me.txtTotalCost.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalCost.ForeColor = System.Drawing.Color.Black
        Me.txtTotalCost.Location = New System.Drawing.Point(794, 49)
        Me.txtTotalCost.MaxLength = 20
        Me.txtTotalCost.Name = "txtTotalCost"
        Me.txtTotalCost.Size = New System.Drawing.Size(116, 22)
        Me.txtTotalCost.TabIndex = 155
        Me.txtTotalCost.Tag = ""
        '
        'txtMC
        '
        Me.txtMC.BackColor = System.Drawing.Color.White
        Me.txtMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMC.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMC.ForeColor = System.Drawing.Color.Black
        Me.txtMC.Location = New System.Drawing.Point(794, 27)
        Me.txtMC.MaxLength = 20
        Me.txtMC.Name = "txtMC"
        Me.txtMC.Size = New System.Drawing.Size(58, 22)
        Me.txtMC.TabIndex = 153
        Me.txtMC.Tag = ""
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.White
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Navy
        Me.Label10.Location = New System.Drawing.Point(794, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(58, 27)
        Me.Label10.TabIndex = 153
        Me.Label10.Text = "MC"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCost
        '
        Me.txtCost.BackColor = System.Drawing.Color.White
        Me.txtCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCost.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCost.ForeColor = System.Drawing.Color.Black
        Me.txtCost.Location = New System.Drawing.Point(728, 27)
        Me.txtCost.MaxLength = 20
        Me.txtCost.Name = "txtCost"
        Me.txtCost.Size = New System.Drawing.Size(66, 22)
        Me.txtCost.TabIndex = 152
        Me.txtCost.Tag = ""
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.White
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Navy
        Me.Label9.Location = New System.Drawing.Point(728, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(66, 27)
        Me.Label9.TabIndex = 151
        Me.Label9.Text = "Cost"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtStoneDescription
        '
        Me.txtStoneDescription.BackColor = System.Drawing.Color.White
        Me.txtStoneDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStoneDescription.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStoneDescription.ForeColor = System.Drawing.Color.Black
        Me.txtStoneDescription.Location = New System.Drawing.Point(360, 49)
        Me.txtStoneDescription.MaxLength = 150
        Me.txtStoneDescription.Name = "txtStoneDescription"
        Me.txtStoneDescription.Size = New System.Drawing.Size(296, 22)
        Me.txtStoneDescription.TabIndex = 149
        Me.txtStoneDescription.Tag = ""
        '
        'txtDescription
        '
        Me.txtDescription.BackColor = System.Drawing.Color.White
        Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.ForeColor = System.Drawing.Color.Black
        Me.txtDescription.Location = New System.Drawing.Point(360, 27)
        Me.txtDescription.MaxLength = 150
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(296, 22)
        Me.txtDescription.TabIndex = 148
        Me.txtDescription.Tag = ""
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.White
        Me.Label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label23.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.Navy
        Me.Label23.Location = New System.Drawing.Point(360, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(296, 27)
        Me.Label23.TabIndex = 149
        Me.Label23.Text = "Description"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnCalalogueSearch
        '
        Me.btnCalalogueSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCalalogueSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCalalogueSearch.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCalalogueSearch.Location = New System.Drawing.Point(336, 27)
        Me.btnCalalogueSearch.Name = "btnCalalogueSearch"
        Me.btnCalalogueSearch.Size = New System.Drawing.Size(24, 22)
        Me.btnCalalogueSearch.TabIndex = 147
        Me.btnCalalogueSearch.TabStop = False
        Me.btnCalalogueSearch.Text = "..."
        Me.btnCalalogueSearch.UseVisualStyleBackColor = False
        '
        'txtCalalogue
        '
        Me.txtCalalogue.BackColor = System.Drawing.Color.White
        Me.txtCalalogue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalalogue.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCalalogue.ForeColor = System.Drawing.Color.Black
        Me.txtCalalogue.Location = New System.Drawing.Point(240, 27)
        Me.txtCalalogue.MaxLength = 20
        Me.txtCalalogue.Name = "txtCalalogue"
        Me.txtCalalogue.Size = New System.Drawing.Size(96, 22)
        Me.txtCalalogue.TabIndex = 146
        Me.txtCalalogue.Tag = ""
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.White
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Navy
        Me.Label7.Location = New System.Drawing.Point(240, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(120, 27)
        Me.Label7.TabIndex = 145
        Me.Label7.Text = "Catalogue Code"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnCategorySearch
        '
        Me.btnCategorySearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCategorySearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCategorySearch.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCategorySearch.Location = New System.Drawing.Point(216, 27)
        Me.btnCategorySearch.Name = "btnCategorySearch"
        Me.btnCategorySearch.Size = New System.Drawing.Size(24, 22)
        Me.btnCategorySearch.TabIndex = 144
        Me.btnCategorySearch.TabStop = False
        Me.btnCategorySearch.Text = "..."
        Me.btnCategorySearch.UseVisualStyleBackColor = False
        '
        'txtCategory
        '
        Me.txtCategory.BackColor = System.Drawing.Color.White
        Me.txtCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCategory.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCategory.ForeColor = System.Drawing.Color.Black
        Me.txtCategory.Location = New System.Drawing.Point(120, 27)
        Me.txtCategory.MaxLength = 20
        Me.txtCategory.Name = "txtCategory"
        Me.txtCategory.Size = New System.Drawing.Size(96, 22)
        Me.txtCategory.TabIndex = 143
        Me.txtCategory.Tag = ""
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.White
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Navy
        Me.Label4.Location = New System.Drawing.Point(120, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(120, 27)
        Me.Label4.TabIndex = 142
        Me.Label4.Text = "Category Code"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnClassSearch
        '
        Me.btnClassSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClassSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClassSearch.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClassSearch.Location = New System.Drawing.Point(96, 27)
        Me.btnClassSearch.Name = "btnClassSearch"
        Me.btnClassSearch.Size = New System.Drawing.Size(24, 22)
        Me.btnClassSearch.TabIndex = 141
        Me.btnClassSearch.TabStop = False
        Me.btnClassSearch.Text = "..."
        Me.btnClassSearch.UseVisualStyleBackColor = False
        '
        'txtClass
        '
        Me.txtClass.BackColor = System.Drawing.Color.White
        Me.txtClass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtClass.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClass.ForeColor = System.Drawing.Color.Black
        Me.txtClass.Location = New System.Drawing.Point(0, 27)
        Me.txtClass.MaxLength = 20
        Me.txtClass.Name = "txtClass"
        Me.txtClass.Size = New System.Drawing.Size(96, 22)
        Me.txtClass.TabIndex = 140
        Me.txtClass.Tag = ""
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.White
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Navy
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(120, 27)
        Me.Label3.TabIndex = 139
        Me.Label3.Text = "Class Code"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grdOrderEntry
        '
        Me.grdOrderEntry.Location = New System.Drawing.Point(9, 366)
        Me.grdOrderEntry.Name = "grdOrderEntry"
        Me.grdOrderEntry.OcxState = CType(resources.GetObject("grdOrderEntry.OcxState"), System.Windows.Forms.AxHost.State)
        Me.grdOrderEntry.Size = New System.Drawing.Size(912, 169)
        Me.grdOrderEntry.TabIndex = 725
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(446, 688)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.Size = New System.Drawing.Size(95, 23)
        Me.cmdPrint.TabIndex = 730
        Me.cmdPrint.Text = "Print"
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.Location = New System.Drawing.Point(637, 688)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(95, 23)
        Me.btnDelete.TabIndex = 728
        Me.btnDelete.Text = "Delete  (F8)"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.Location = New System.Drawing.Point(542, 688)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(94, 23)
        Me.btnSave.TabIndex = 726
        Me.btnSave.Text = "Save (F7)"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(829, 688)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(92, 23)
        Me.btnClose.TabIndex = 729
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(733, 688)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(95, 23)
        Me.btnClear.TabIndex = 727
        Me.btnClear.Text = "Clear (F6)"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'pnlAdminPwd
        '
        Me.pnlAdminPwd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.pnlAdminPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlAdminPwd.Controls.Add(Me.btnAdminPwdCancel)
        Me.pnlAdminPwd.Controls.Add(Me.btnAdminPwdOK)
        Me.pnlAdminPwd.Controls.Add(Me.txtAdminPassword)
        Me.pnlAdminPwd.Controls.Add(Me.Label45)
        Me.pnlAdminPwd.Controls.Add(Me.Label56)
        Me.pnlAdminPwd.Location = New System.Drawing.Point(342, 196)
        Me.pnlAdminPwd.Name = "pnlAdminPwd"
        Me.pnlAdminPwd.Size = New System.Drawing.Size(246, 88)
        Me.pnlAdminPwd.TabIndex = 731
        Me.pnlAdminPwd.Visible = False
        '
        'btnAdminPwdCancel
        '
        Me.btnAdminPwdCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnAdminPwdCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdminPwdCancel.Image = CType(resources.GetObject("btnAdminPwdCancel.Image"), System.Drawing.Image)
        Me.btnAdminPwdCancel.Location = New System.Drawing.Point(160, 56)
        Me.btnAdminPwdCancel.Name = "btnAdminPwdCancel"
        Me.btnAdminPwdCancel.Size = New System.Drawing.Size(64, 23)
        Me.btnAdminPwdCancel.TabIndex = 76
        Me.btnAdminPwdCancel.Text = "Cancel"
        Me.btnAdminPwdCancel.UseVisualStyleBackColor = False
        '
        'btnAdminPwdOK
        '
        Me.btnAdminPwdOK.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnAdminPwdOK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdminPwdOK.Image = CType(resources.GetObject("btnAdminPwdOK.Image"), System.Drawing.Image)
        Me.btnAdminPwdOK.Location = New System.Drawing.Point(88, 56)
        Me.btnAdminPwdOK.Name = "btnAdminPwdOK"
        Me.btnAdminPwdOK.Size = New System.Drawing.Size(64, 23)
        Me.btnAdminPwdOK.TabIndex = 75
        Me.btnAdminPwdOK.Text = "OK"
        Me.btnAdminPwdOK.UseVisualStyleBackColor = False
        '
        'txtAdminPassword
        '
        Me.txtAdminPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdminPassword.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdminPassword.ForeColor = System.Drawing.Color.Black
        Me.txtAdminPassword.Location = New System.Drawing.Point(88, 32)
        Me.txtAdminPassword.MaxLength = 20
        Me.txtAdminPassword.Name = "txtAdminPassword"
        Me.txtAdminPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtAdminPassword.Size = New System.Drawing.Size(136, 20)
        Me.txtAdminPassword.TabIndex = 73
        Me.txtAdminPassword.Tag = ""
        '
        'Label45
        '
        Me.Label45.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.Color.Black
        Me.Label45.Location = New System.Drawing.Point(16, 33)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(64, 15)
        Me.Label45.TabIndex = 74
        Me.Label45.Text = "Password"
        '
        'Label56
        '
        Me.Label56.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.ForeColor = System.Drawing.Color.Black
        Me.Label56.Location = New System.Drawing.Point(8, 8)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(56, 15)
        Me.Label56.TabIndex = 72
        Me.Label56.Text = "Admin"
        Me.Label56.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmInventoryOrderEntry
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(930, 711)
        Me.Controls.Add(Me.pnlAdminPwd)
        Me.Controls.Add(Me.cmdPrint)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.grdOrderEntry)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pic)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmInventoryOrderEntry"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Inventory Order Entry"
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.grdOrderEntry, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlAdminPwd.ResumeLayout(False)
        Me.pnlAdminPwd.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim objCustomer As New clsCustomer
    Dim objClsSupplier As New clsSuppliers
    Dim objClsOrder As New clsOrder
    Dim deliveryDate As String
    Dim orderedDate As String
    Dim sStatus As String
    Dim advAmountPay As Decimal
    Dim bPassword As Boolean
    Dim objUM As New clsUserMasters
    Dim sAdminUserId As String
    Dim sAdminPassword As String
    Dim percentageAmt As Decimal
    Dim sTradeAmt As Decimal
    Public tradeInRefNo As String
    Dim objComp As New clsCompany
    Dim sCustomerType As String
    Dim sNextOrderNo As String
    Dim sOrderType As String

    'grdOrderEntry
    Const cClassCode = 1
    Const cCategoryCode = 2
    Const cCatalogueCode = 3
    Const cDescription = 4
    Const cStoneDescription = 5
    Const cWeight = 6
    Const cStoneWeight = 7
    Const cGoldLoss = 8
    Const cCostPerGram = 9
    Const cMC = 10
    Const cTotalCost = 11
    Const cInventoryCode = 12
    Const cOrderInventoryCode = 13

    Private Sub btnOrderNoSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOrderNoSearch.Click
        Dim sInvType As String
        Try
            If optOrder.Checked Then
                sInvType = "O"
            Else
                sInvType = "R"
            End If
            Dim frmHelpOrder As New frmHelpSearch(0, 0, "tblinventoryorderHeader", "L", 4, "OrderNo", "InvType = " & fncFormatSQLText(sInvType), "OrderNo", "OrderNo", 80, "OrderDate", "OrderDate", 180, "CustomerCode", "CustomerCode", 60, "SupplierCode", "SupplierCode", 80)
            frmHelpOrder.ShowDialog()
            txtOrderNo.Text = frmHelpOrder.Var1.Trim
            If txtOrderNo.Text.Trim.Length <> 0 Then DispOrderDetails(txtOrderNo.Text.Trim)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub DispOrderDetails(ByVal sOrderCode As String)
        Dim ds As DataSet
        Dim dsOD As New DataSet
        Dim dsInv As DataSet
        Try

            ds = objClsOrder.GetInventoryOrderHeader(sOrderCode)
            If ds.Tables(0).Rows.Count > 0 Then
                txtOrderNo.Text = Trim(ds.Tables(0).Rows(0)("orderno") & "")
                txtODD.Text = Format(CDate(ds.Tables(0).Rows(0)("orderdate") & ""), "dd")
                txtOMM.Text = Format(CDate(ds.Tables(0).Rows(0)("orderdate") & ""), "MM")
                txtOYYYY.Text = Format(CDate(ds.Tables(0).Rows(0)("orderdate") & ""), "yyyy")
                txtCustomerCode.Text = Trim(ds.Tables(0).Rows(0)("CustomerCode") & "")
                txtSupplierCode.Text = Trim(ds.Tables(0).Rows(0)("SupplierCode") & "")
                txtEstiDD.Text = Format(CDate(ds.Tables(0).Rows(0)("DeliveryDate") & ""), "dd")
                txtEstiMM.Text = Format(CDate(ds.Tables(0).Rows(0)("DeliveryDate") & ""), "MM")
                txtEstiYY.Text = Format(CDate(ds.Tables(0).Rows(0)("DeliveryDate") & ""), "yyyy")
                txtEstiTotalCost.Text = Format(Val(ds.Tables(0).Rows(0)("TotalCostEstimated") & ""), "#0.00")
                txtCurrCostPerGram.Text = Format(Val(ds.Tables(0).Rows(0)("CostPerGram") & ""), "#0.00")
                txtAdvancePaid.Text = Format(Val(ds.Tables(0).Rows(0)("AdvancePaid") & ""), "#0.00")
                txtRemarks.Text = Trim(ds.Tables(0).Rows(0)("Remarks") & "")
                txtSupplierCost.Text = Trim(ds.Tables(0).Rows(0)("SupplierCost") & "")
                txtBalanceAmount.Text = Format(Val(txtEstiTotalCost.Text) - Val(txtAdvancePaid.Text), "#0.00")
                'If ds.Tables(1).Rows.Count > 0 Then txtBalanceAmount.Text = Format(Val(txtBalanceAmount.Text.Trim) - Val(ds.Tables(1).Rows(0)("AmountPaid")), "#0.00")
                sStatus = Trim(ds.Tables(0).Rows(0)("Status") & "")
                sOrderType = Trim(ds.Tables(0).Rows(0)("TradeInType") & "")
                If sOrderType = "C" Then
                    optCash.Checked = True
                ElseIf sOrderType = "G" Then
                    optGold.Checked = True
                Else
                    optBoth.Checked = True
                End If
                If Trim(ds.Tables(0).Rows(0)("RateFixed") & "") = "Y" Then
                    optTodayRate.Checked = True
                Else
                    optDeliveryDayRate.Checked = True
                End If

                If Trim(UCase(ds.Tables(0).Rows(0)("InvType") & "")) = "O" Then 'order
                    optOrder.Checked = True
                ElseIf Trim(UCase(ds.Tables(0).Rows(0)("InvType") & "")) = "R" Then
                    optRepair.Checked = True
                End If

                If sStatus = "O" And Trim(UCase(ds.Tables(0).Rows(0)("InvType") & "")) = "R" Then
                    lblOrderStatus.Text = "Repair"
                ElseIf sStatus = "O" And Trim(UCase(ds.Tables(0).Rows(0)("InvType") & "")) = "O" Then
                    lblOrderStatus.Text = "Ordered"
                ElseIf sStatus = "P" Then
                    lblOrderStatus.Text = "Pending"
                ElseIf sStatus = "S" Then
                    lblOrderStatus.Text = "Sold"
                End If

                DispSupplierDetails(txtSupplierCode.Text.Trim)
                LoadCustomerDetails(txtCustomerCode.Text.Trim)

                Dim iCnt As Integer
                dsOD = objClsOrder.GetInventoryOrderDetail(sOrderCode)
                grdOrderEntry.MaxRows = 0
                txtEstiWeight.Text = ""
                For iCnt = 0 To dsOD.Tables(0).Rows.Count - 1
                    grdOrderEntry.MaxRows = grdOrderEntry.MaxRows + 1
                    SetText(grdOrderEntry, grdOrderEntry.MaxRows, cClassCode, Trim(dsOD.Tables(0).Rows(iCnt)("ClassCode") & ""))
                    SetText(grdOrderEntry, grdOrderEntry.MaxRows, cCategoryCode, Trim(dsOD.Tables(0).Rows(iCnt)("CategoryCode") & ""))
                    SetText(grdOrderEntry, grdOrderEntry.MaxRows, cCatalogueCode, Trim(dsOD.Tables(0).Rows(iCnt)("CatalogueCode") & ""))
                    SetText(grdOrderEntry, grdOrderEntry.MaxRows, cDescription, Trim(dsOD.Tables(0).Rows(iCnt)("Description") & ""))
                    SetText(grdOrderEntry, grdOrderEntry.MaxRows, cStoneDescription, Trim(dsOD.Tables(0).Rows(iCnt)("StoneDescription") & ""))
                    SetText(grdOrderEntry, grdOrderEntry.MaxRows, cWeight, Trim(dsOD.Tables(0).Rows(iCnt)("WeightEstimated") & ""))
                    txtEstiWeight.Text = Val(txtEstiWeight.Text) + Val(dsOD.Tables(0).Rows(iCnt)("WeightEstimated"))
                    SetText(grdOrderEntry, grdOrderEntry.MaxRows, cStoneWeight, Trim(dsOD.Tables(0).Rows(iCnt)("StoneWeight") & ""))
                    SetText(grdOrderEntry, grdOrderEntry.MaxRows, cGoldLoss, Trim(dsOD.Tables(0).Rows(iCnt)("GoldLoss") & ""))
                    SetText(grdOrderEntry, grdOrderEntry.MaxRows, cCostPerGram, Trim(dsOD.Tables(0).Rows(iCnt)("CostPerGram") & ""))
                    SetText(grdOrderEntry, grdOrderEntry.MaxRows, cMC, Trim(dsOD.Tables(0).Rows(iCnt)("MC") & ""))
                    SetText(grdOrderEntry, grdOrderEntry.MaxRows, cTotalCost, Trim(dsOD.Tables(0).Rows(iCnt)("EstimatedCost") & ""))
                    SetText(grdOrderEntry, grdOrderEntry.MaxRows, cInventoryCode, Trim(dsOD.Tables(0).Rows(iCnt)("InventoryCode") & ""))
                    SetText(grdOrderEntry, grdOrderEntry.MaxRows, cOrderInventoryCode, Trim(dsOD.Tables(0).Rows(iCnt)("OrderInventoryCode") & ""))
                Next
                txtEstiWeight.Text = Format(Val(txtEstiWeight.Text), "0#.00")
                txtOrderNo.ReadOnly = True
                txtAdvancePaid.ReadOnly = True
            Else
                MsgBox("No such Order Number ! ", MsgBoxStyle.Exclamation)
                txtOrderNo.Clear()
                txtOrderNo.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
            ds = Nothing
            dsInv = Nothing
        End Try
    End Sub

    Private Sub subShowInventoryDetail(ByVal sInvCode As String)
        Dim dsInv As New DataSet
        Try
            dsInv = objClsOrder.GetInventoryDetail(sInvCode)
            If dsInv.Tables(0).Rows.Count > 0 Then
                txtInvCode.Text = Trim(dsInv.Tables(0).Rows(0)("InventoryCode") & "")
                txtActualWeight.Text = Format(Val(dsInv.Tables(0).Rows(0)("Weight") & ""), "#0.00")
                txtActualTotalCost.Text = Format(Val(dsInv.Tables(0).Rows(0)("TotalCost") & ""), "#0.00")
                txtActualCostPerGram.Text = Format(Val(dsInv.Tables(0).Rows(0)("CostperGram")), "#0.00")
                txtPMC.Text = Format(Val(dsInv.Tables(0).Rows(0)("PMC")), "#0.00")
                txtSMC.Text = Format(Val(dsInv.Tables(0).Rows(0)("sMc")), "#0.00")
                txtActualGoldLoss.Text = Format(Val(dsInv.Tables(0).Rows(0)("GoldLoss")), "#0.00")
                txtPurchaseBillNo.Text = Trim(dsInv.Tables(0).Rows(0)("PurchaseBillNo") & "")
                txtDeliDD.Text = Format(DateValue(Trim(dsInv.Tables(0).Rows(0)("PurchaseDate") & "")), "dd")
                txtDeliMM.Text = Format(DateValue(Trim(dsInv.Tables(0).Rows(0)("PurchaseDate") & "")), "MM")
                txtDeliYY.Text = Format(DateValue(Trim(dsInv.Tables(0).Rows(0)("PurchaseDate") & "")), "yyyy")
            Else
                subClearInventoryDetails()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsInv = Nothing
        End Try
    End Sub

    Private Sub subClearInventoryDetails()
        Try
            txtInvCode.Text = ""
            txtActualWeight.Text = 0
            txtActualTotalCost.Text = 0
            txtActualCostPerGram.Text = 0
            txtPMC.Text = 0
            txtSMC.Text = 0
            txtActualGoldLoss.Text = 0
            txtPurchaseBillNo.Text = ""
            txtDeliDD.Text = ""
            txtDeliMM.Text = ""
            txtDeliYY.Text = ""
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Sub DispSupplierDetails(ByVal suplCode As String)
        Dim ds As DataSet
        Try
            If suplCode.Length > 0 Then
                ds = objClsSupplier.GetSupplierDetail(suplCode)
                If ds.Tables(0).Rows.Count > 0 Then
                    txtSupplierCode.Text = Trim(ds.Tables(0).Rows(0)("suppliercode") & "")
                    txtSupplierName.Text = Trim(ds.Tables(0).Rows(0)("SupplierName") & "")
                    txtSAddress1.Text = Trim(ds.Tables(0).Rows(0)("Address1") & "")
                    txtSAddress2.Text = Trim(ds.Tables(0).Rows(0)("Address2") & "")
                    txtSAddress3.Text = Trim(ds.Tables(0).Rows(0)("Address3") & "")
                    txtSupplierName.ReadOnly = True
                    txtSAddress1.ReadOnly = True
                    txtSAddress2.ReadOnly = True
                    txtSAddress3.ReadOnly = True
                    'txtClass.Focus()
                Else
                    MsgBox("No such Supplier code ! ", MsgBoxStyle.Exclamation)
                    txtSupplierCode.Clear()
                    txtSupplierCode.Focus()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
            ds = Nothing
        End Try
    End Sub

    Private Sub LoadCustomerDetails(ByVal sCustCode As String)
        Dim dsCustomer As New DataSet
        Try
            dsCustomer = objCustomer.GetCustomerwithCode(sCustCode)
            If Not IsNothing(dsCustomer) Then
                If dsCustomer.Tables("Customer").Rows.Count > 0 Then
                    txtCustomerCode.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("CustomerCode") & "")
                    txtCustomerName.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("CustomerName") & "")
                    txtCAddress1.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("Address1") & "")
                    txtCAddress2.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("Address2") & "")
                    txtCAddress3.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("Address3") & "")
                    sCustomerType = Trim(dsCustomer.Tables("Customer").Rows(0)("CustomerType") & "")
                    txtNRICNo.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("NricNo") & "")
                    txtCustomerName.ReadOnly = True
                    txtCAddress1.ReadOnly = True
                    txtCAddress2.ReadOnly = True
                    txtCAddress3.ReadOnly = True
                    txtNRICNo.ReadOnly = True
                    txtSupplierCode.Focus()
                Else
                    MsgBox("No such Customer !", MsgBoxStyle.Exclamation)
                    txtCustomerCode.Clear()
                    txtCustomerCode.Focus()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Err.Clear()
            dsCustomer = Nothing
        End Try
    End Sub

    Private Sub grdOrderEntry_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_ClickEvent) Handles grdOrderEntry.ClickEvent
        Try
            If Trim(GetText(grdOrderEntry, e.row, cInventoryCode)) <> "" Then
                subShowInventoryDetail(Trim(GetText(grdOrderEntry, e.row, cInventoryCode)))
            Else
                subClearInventoryDetails()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtOrderNo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOrderNo.LostFocus
        If txtOrderNo.Text.Trim.Length <> 0 Then
            DispOrderDetails(txtOrderNo.Text.Trim)
        End If
    End Sub

    Private Sub txtOrderNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtOrderNo.KeyDown
        If e.KeyCode = Keys.F1 Then
            btnOrderNoSearch_Click(sender, e)
        End If
    End Sub

    Private Sub txtCustomerCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustomerCode.LostFocus
        If txtCustomerCode.Text.Trim <> "" Then
            LoadCustomerDetails(txtCustomerCode.Text.Trim)
        End If
    End Sub

    Private Sub txtCustomerCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCustomerCode.KeyDown
        If e.KeyCode = Keys.F1 Then
            btnCustomerCodeSearch_Click(sender, e)
        End If
    End Sub

    Private Sub btnCustomerCodeSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomerCodeSearch.Click
        Try
            Dim frmHelpCustomer As New frmHelpSearch(0, 0, "tblCustomer", "L", 4, "CustomerName", "", "CustomerCode", "CustomerCode", 80, "CustomerName", "CustomerName", 150, "Address1", "Address1", 90, "Address2", "Address2", 80)
            frmHelpCustomer.ShowDialog()
            txtCustomerCode.Text = frmHelpCustomer.Var1.Trim
            If txtCustomerCode.Text.Trim <> "" Then LoadCustomerDetails(txtCustomerCode.Text.Trim)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomer.Click
        Try
            Dim frmCustomerO As New frmCustomer
            frmCustomerO.txtCustomercode.Text = txtCustomerCode.Text.Trim
            frmCustomerO.LoadCustomerDetails(txtCustomerCode.Text.Trim)
            frmCustomerO.btnNew.Visible = False
            frmCustomerO.btnDelete.Enabled = False
            frmCustomerO.ShowDialog()
            txtCustomerCode.Text = frmCustomerO.txtCustomercode.Text
            If txtCustomerCode.Text.Trim <> "" Then LoadCustomerDetails(txtCustomerCode.Text.Trim)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtSupplierCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSupplierCode.TextChanged

    End Sub

    Private Sub txtSupplierCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSupplierCode.LostFocus
        If txtSupplierCode.Text.Trim.Length <> 0 Then
            DispSupplierDetails(txtSupplierCode.Text.Trim)
        End If
    End Sub

    Private Sub txtSupplierCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSupplierCode.KeyDown
        If e.KeyCode = Keys.F1 Then
            btnSupplierCodeSearch_Click(sender, e)
        End If
    End Sub

    Private Sub btnSupplierCodeSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSupplierCodeSearch.Click
        Try
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblsupplier", "L", 4, "SupplierCode", "", "SupplierCode", "SupplierCode", 80, "SupplierName", "SupplierName", 180, "Address1", "Address1", 70, "Address2", "Address2", 70)
            frmHelpClass.ShowDialog()
            txtSupplierCode.Text = frmHelpClass.Var1.Trim
            txtSupplierCode_LostFocus(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnTradeIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTradeIn.Click
        Try
            Dim TradeIn As New frmTradeInGold

            TradeIn.sCalledFrom = "ORDERFORM"
            TradeIn.sInvoiceNo = Format(Val(sNextOrderNo), "00000000")
            TradeIn.txtCustomerCode.Text = txtCustomerCode.Text
            TradeIn.txtCustomerName.Text = txtCustomerName.Text
            TradeIn.lblCustomerType.Text = sCustomerType
            TradeIn.txtAddress1.Text = txtCAddress1.Text
            TradeIn.txtAddress2.Text = txtCAddress2.Text
            TradeIn.txtAddress3.Text = txtCAddress3.Text
            TradeIn.lblTradeInLimit.Text = dTradeinLimit
            TradeIn.lblSalesAmount.Text = Val(txtEstiTotalCost.Text)
            TradeIn.sSalesAmount = Val(txtEstiTotalCost.Text)
            'TradeIn.txtCostPerGram.Text = Val(txtCurrCostPerGram.Text)
            TradeIn.ShowDialog()
            txtAdvancePaid.Text = dTradeinAmount
            tradeInRefNo = sTradeInRefNo
            If ChkTradeIn Then
                btnTradeIn.Enabled = False
                optCash.Enabled = False
            Else
                btnTradeIn.Enabled = True
                optCash.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub optOrder_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optOrder.CheckedChanged
        Try
            If optOrder.Checked = True Then
                lblNo.Text = "Order No"
                lblDate.Text = "Order Date"
            Else
                lblNo.Text = "Repair No"
                lblDate.Text = "Repair Date"
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub optRepair_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optRepair.CheckedChanged
        Try
            If optRepair.Checked = True Then
                lblNo.Text = "Repair No"
                lblDate.Text = "Repair Date"
            Else
                lblNo.Text = "Order No"
                lblDate.Text = "Order Date"
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub optGold_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optGold.CheckedChanged
        Try
            If optGold.Checked Then
                'txtAdvancePaid.Clear()
                btnTradeIn.Visible = True
                txtAdvancePaid.ReadOnly = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub optCash_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optCash.CheckedChanged
        Try
            If optCash.Checked Then
                btnTradeIn.Visible = False
                txtAdvancePaid.ReadOnly = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub optBoth_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optBoth.CheckedChanged
        Try
            If optBoth.Checked Then
                btnTradeIn.Visible = True
                txtAdvancePaid.ReadOnly = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmInventoryOrderEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            getCurrentDate()
            sStatus = "O"
            txtOrderNo.Focus()
            sAdminUserId = ""
            sAdminPassword = ""
            sTradeAmt = 0.0
            percentageAmt = 0.0
            subGetCompany()
            btnTradeIn.Visible = False
            subGridSetting(grdOrderEntry)
            grdOrderEntry.MaxRows = 0
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub getCurrentDate()
        txtODD.Text = Format(ServerDate, "dd")
        txtOMM.Text = Format(ServerDate, "MM")
        txtOYYYY.Text = Format(ServerDate, "yyyy")
    End Sub

    Private Sub subGetCompany()
        Dim dsComp As New DataSet
        Try
            dsComp = objComp.GetCompanyList()
            If dsComp.Tables(0).Rows.Count > 0 Then
                dTradeinLimit = Val(Trim(dsComp.Tables(0).Rows(0)("tradeinLimit") & ""))
                sNextOrderNo = Val(Trim(dsComp.Tables(0).Rows(0)("NextOrderEntryNo") & ""))
            Else
                MessageBox.Show("Locked.")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsComp = Nothing
        End Try
    End Sub

    Private Sub frmInventoryOrderEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.F6
                    btnClear_Click(sender, e)
                Case Keys.F7
                    btnSave_Click(sender, e)
                Case Keys.Escape
                    If pnlAdminPwd.Visible = True Then
                        pnlAdminPwd.Visible = False
                        txtAdvancePaid.Focus()
                    Else
                        btnClose_Click(sender, e)
                    End If
                Case Keys.Return
                    SendKeys.Send("{Tab}")
                    'Case Keys.Down
                    '    SendKeys.Send("{Tab}")
                    'Case Keys.Up
                    '    SendKeys.Send("+{Tab}")
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        If txtOrderNo.Text.Trim <> "" Then
            subPrintBill(txtOrderNo.Text.Trim)
            ClearControls()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sRateFixed As String
        Dim sTradeInType As String
        Dim sInvType As String
        Dim iCnt As Integer
        Dim objPM As New clsPayMode
        Dim frmPM As New frmPayingmode
        Try
            deliveryDate = txtEstiDD.Text.Trim & "/" & txtEstiMM.Text.Trim & "/" & txtEstiYY.Text.Trim
            orderedDate = txtODD.Text.Trim & "/" & txtOMM.Text.Trim & "/" & txtOYYYY.Text.Trim
            If fncValidateMe() Then
                If optTodayRate.Checked Then
                    sRateFixed = "Y"
                Else
                    sRateFixed = "N"
                End If
                If optCash.Checked Then
                    sTradeInType = "C"
                ElseIf optGold.Checked Then
                    sTradeInType = "G"
                ElseIf optBoth.Checked Then
                    sTradeInType = "B"
                End If
                sInvType = IIf(optOrder.Checked, "O", "R") ' O - Order; R -  Repair
                subCalcTotal()
                If optBoth.Checked Then
                    If sTradeAmt > Val(txtAdvancePaid.Text) Then
                        MsgBox("Trade In Amount is more than Advance Paid !", MsgBoxStyle.Exclamation)
                        txtAdvancePaid.Text = dTradeinAmount
                        txtAdvancePaid.Focus()
                        Exit Sub
                    End If
                    If tradeInRefNo.Trim = "" Then
                        MsgBox("TradeIn  is not updated! No trade in ref Number!", MsgBoxStyle.Exclamation)
                        btnTradeIn.Focus()
                        Exit Sub
                    End If
                End If
                If optGold.Checked Then
                    If tradeInRefNo.Trim = "" Then
                        MsgBox("TradeIn  is not updated! No trade in ref Number!", MsgBoxStyle.Exclamation)
                        btnTradeIn.Focus()
                        Exit Sub
                    End If
                End If

                objClsOrder.OpenOrderConnection()
                objClsOrder.gpBeginOrderTransaction()
                If txtOrderNo.Text.Trim = "" Then
                    txtOrderNo.Text = objClsOrder.GetNextOrderNo()
                    If txtOrderNo.Text.Trim = "" Then
                        objClsOrder.gpRollBackOrderConnection()
                        Exit Sub
                    End If
                    dPaidAmount = 0
                    ReturnAmount = 0
                    nTotalNet = Val(txtAdvancePaid.Text)
                    frmPM.SalesType = ""
                    frmPM.ShowDialog()
                    If dPaidAmount > 0 Then
                        'save paymode detail
                        If Not fncPaymodeDetailSave(txtOrderNo.Text.Trim) Then
                            MessageBox.Show("Error in save.")
                            objClsOrder.gpRollBackOrderConnection()
                        End If
                    Else
                        Exit Sub
                    End If
                End If

                'save order header
                If Not objClsOrder.FncOrderHeaderSave(txtOrderNo.Text.Trim, orderedDate.Trim, txtCustomerCode.Text.Trim, txtSupplierCode.Text.Trim, _
                                            deliveryDate.Trim, Val(txtCurrCostPerGram.Text.Trim), tradeInRefNo, sTradeInType, sRateFixed, Val(txtEstiTotalCost.Text.Trim), _
                                            Val(txtAdvancePaid.Text.Trim), sStatus, txtRemarks.Text.Trim, sAdminPassword, sInvType, Val(txtSupplierCost.Text.Trim), 0, "", "", 0, 0) Then
                    MsgBox(" Error in Order Entry save !", MsgBoxStyle.Critical)
                    objClsOrder.gpRollBackOrderConnection()
                    Exit Sub
                End If
                'save order detail
                For iCnt = 1 To grdOrderEntry.MaxRows
                    If Not objClsOrder.FncOrderDetailSave(txtOrderNo.Text.Trim, GetText(grdOrderEntry, iCnt, cDescription), GetText(grdOrderEntry, iCnt, cOrderInventoryCode), GetText(grdOrderEntry, iCnt, cStoneDescription), GetText(grdOrderEntry, iCnt, cClassCode), GetText(grdOrderEntry, iCnt, cCategoryCode), GetText(grdOrderEntry, iCnt, cCatalogueCode), Val(GetText(grdOrderEntry, iCnt, cWeight)), Val(GetText(grdOrderEntry, iCnt, cStoneWeight)), Val(GetText(grdOrderEntry, iCnt, cGoldLoss)), Val(GetText(grdOrderEntry, iCnt, cMC)), Val(GetText(grdOrderEntry, iCnt, cCostPerGram)), Val(GetText(grdOrderEntry, iCnt, cTotalCost)), sStatus, 0, 0, 0, "") Then
                        objClsOrder.gpRollBackOrderConnection()
                        Exit Sub
                    End If
                Next
                objClsOrder.gpCommitOrderTransaction()
                If sInvType = "O" Then
                    MsgBox(" Order Entry Saved  ", MsgBoxStyle.Information)
                ElseIf sInvType = "R" Then
                    MsgBox(" Repair Entry Saved  ", MsgBoxStyle.Information)
                End If
                If txtOrderNo.Text.Trim <> "" Then
                    subPrintBill(txtOrderNo.Text.Trim)
                End If
                ClearControls()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            objClsOrder.CloseOrderConnection()
            objPM = Nothing
            frmPM = Nothing
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim objOrder As New clsOrder
        Dim objPM As New clsPayMode
        Dim sOrderStatus As String
        Dim PURCHASED As String = "P"
        Dim ORDERED As String = "O"
        Dim REPAIR As String = "R"
        Dim dAmount As Decimal
        Try
            Cursor.Current = Cursors.WaitCursor
            If txtOrderNo.Text.Trim <> "" Then
                sOrderStatus = objOrder.GetOrderStatus(txtOrderNo.Text.Trim)
                If sOrderStatus.Trim = "" Then
                    MessageBox.Show("Order number not exists.")
                ElseIf UCase(sOrderStatus.Trim) = PURCHASED Then
                    MessageBox.Show("This ordered items are already purchased. You cant delete this order.")
                ElseIf UCase(sOrderStatus.Trim) = ORDERED Or UCase(sOrderStatus.Trim) = REPAIR Then
                    objOrder.OpenOrderConnection()
                    objOrder.gpBeginOrderTransaction()
                    If Not objOrder.OrderDelete(txtOrderNo.Text.Trim) Then
                        MessageBox.Show("Error in Delete.")
                        objOrder.gpRollBackOrderConnection()
                    End If
                    'update payment detail
                    dAmount = objOrder.GetOrderPaymentAmount(txtOrderNo.Text.Trim)
                    If Not objPM.PayModeDetailSave(txtOrderNo.Text.Trim, Format(Now, "dd/MM/yyyy"), "CASH", -dAmount, 0, "", "ORDER") Then
                        MessageBox.Show("Error in Delete.")
                        objOrder.gpRollBackOrderConnection()
                    End If
                    'delete from table orderpayment
                    If Not objOrder.fncOrderPaymentDetailDelete(txtOrderNo.Text.Trim, "", 0) Then
                        MessageBox.Show("Error in Delete.")
                        objOrder.gpRollBackOrderConnection()
                    End If
                    objOrder.gpCommitOrderTransaction()
                    MessageBox.Show("Order Deleted.")
                End If
                ClearControls()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            objOrder.CloseOrderConnection()
            objOrder = Nothing
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ClearControls()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtClass_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtClass.LostFocus
        Dim dsClass As DataSet
        Dim objClass As New ClsClass
        Try
            If txtClass.Text.Trim.Length > 0 Then
                dsClass = objClass.GetClassDetail(txtClass.Text.Trim)
                If dsClass.Tables(0).Rows.Count > 0 Then
                    txtClass.Text = Trim(dsClass.Tables(0).Rows(0)("Classcode") & "")
                    txtClass.Tag = Trim(dsClass.Tables(0).Rows(0)("PurchaseType") & "")
                    txtCategory.Focus()
                Else
                    MsgBox("No such ClassCode ! ", MsgBoxStyle.Exclamation)
                    txtClass.Clear()
                    txtClass.Focus()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            dsClass = Nothing
            objClass = Nothing
        End Try
    End Sub

    Private Sub txtClass_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtClass.KeyDown
        If e.KeyCode = Keys.F1 Then
            btnClassSearch_Click(sender, e)
        End If
    End Sub

    Private Sub btnClassSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClassSearch.Click
        Try
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblClass", "L", 4, "ClassCode", "", "ClassCode", "ClassCode", 80, "ClassName", "ClassName", 180, "Rate", "Rate", 60, "Remarks", "Remarks", 80)
            frmHelpClass.ShowDialog()
            txtClass.Text = frmHelpClass.Var1.Trim
            txtClass_LostFocus(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtCategory_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCategory.LostFocus
        Dim dsCategory As DataSet
        Dim objCategory As New ClsCategory
        Try
            If txtCategory.Text.Trim.Length > 0 Then
                dsCategory = objCategory.GetCategorywithCode(txtCategory.Text.Trim)
                If dsCategory.Tables(0).Rows.Count > 0 Then
                    txtCategory.Text = Trim(dsCategory.Tables(0).Rows(0)("Categorycode") & "")
                    txtCalalogue.Focus()
                Else
                    MsgBox("No such CategoryCode ! ", MsgBoxStyle.Exclamation)
                    txtCategory.Clear()
                    txtCategory.Focus()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            dsCategory = Nothing
            objCategory = Nothing
        End Try
    End Sub

    Private Sub txtCategory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCategory.KeyDown
        If e.KeyCode = Keys.F1 Then
            btnCategorySearch_Click(sender, e)
        End If
    End Sub

    Private Sub btnCategorySearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCategorySearch.Click
        Try
            Dim frmHelpCategory As New frmHelpSearch(0, 0, "tblCategory", "L", 4, "CategoryCode", " ClassCode = " & fncFormatSQLText(txtClass.Text.Trim), "CategoryCode", "CategoryCode", 80, "CategoryName", "CategoryName", 180, "ClassCode", "ClassCode", 60, "Remarks", "Remarks", 80)
            frmHelpCategory.ShowDialog()
            txtCategory.Text = frmHelpCategory.Var1.Trim
            txtCategory_LostFocus(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtCalalogue_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCalalogue.LostFocus
        Dim dsCatalogue As DataSet
        Dim objCatalogue As New ClsCatalogue
        Try
            If txtCalalogue.Text.Trim.Length > 0 Then
                dsCatalogue = objCatalogue.GetCatalogueCode(txtCalalogue.Text.Trim)
                If dsCatalogue.Tables(0).Rows.Count > 0 Then
                    txtCalalogue.Text = Trim(dsCatalogue.Tables(0).Rows(0)("Categorycode") & "")
                    txtDescription.Focus()
                Else
                    MsgBox("No such Catalogue! ", MsgBoxStyle.Exclamation)
                    txtCalalogue.Clear()
                    txtCalalogue.Focus()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            dsCatalogue = Nothing
            objCatalogue = Nothing
        End Try
    End Sub

    Private Sub txtCalalogue_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCalalogue.KeyDown
        If e.KeyCode = Keys.F1 Then
            btnCalalogueSearch_Click(sender, e)
        End If
    End Sub

    Private Sub btnCalalogueSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCalalogueSearch.Click
        Try
            Dim frmHelpCatalogue As New frmHelpSearch(0, 0, "tblCatalogue", "L", 4, "CatalogueName", " CategoryCode = " & fncFormatSQLText(txtCategory.Text.Trim), "CatalogueCode", "CatalogueCode", 80, "CategoryCode", "CategoryCode", 180, "CatalogueName", "CatalogueName", 60, "Remarks", "Remarks", 80)
            frmHelpCatalogue.ShowDialog()
            txtCalalogue.Text = frmHelpCatalogue.Var1.Trim
            txtCalalogue_LostFocus(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtWeight_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWeight.LostFocus
        Try
            If Trim(txtWeight.Text) = "" Then
                txtWeight.Text = "0.00"
            Else
                If Not IsNumeric(txtWeight.Text) Then
                    MessageBox.Show("Weight should be Numeric.")
                    txtWeight.Text = "0.00"
                    txtWeight.Focus()
                Else
                    txtWeight.Text = Format(Val(txtWeight.Text), "#0.00")
                    txtTotalCost.Text = Val(Val(txtCost.Text.Trim) * Val(txtWeight.Text.Trim)) + Val(txtMC.Text.Trim)
                    txtTotalCost.Text = Format(Val(txtTotalCost.Text), "#0.00")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtStoneWeight_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtStoneWeight.LostFocus
        Try
            If Trim(txtStoneWeight.Text) = "" Then
                txtStoneWeight.Text = "0.00"
            Else
                If Not IsNumeric(txtStoneWeight.Text) Then
                    MessageBox.Show("Stone Weight should be Numeric.")
                    txtStoneWeight.Text = "0.00"
                    txtStoneWeight.Focus()
                Else
                    txtStoneWeight.Text = Format(Val(txtStoneWeight.Text), "#0.00")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtCost_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCost.LostFocus
        Try
            If Trim(txtCost.Text) = "" Then
                txtCost.Text = "0.00"
            Else
                If Not IsNumeric(txtCost.Text) Then
                    MessageBox.Show("Cost should be Numeric.")
                    txtCost.Text = "0.00"
                    txtCost.Focus()
                Else
                    txtCost.Text = Format(Val(txtCost.Text), "#0.00")
                    txtTotalCost.Text = Val(Val(txtCost.Text.Trim) * Val(txtWeight.Text.Trim)) + Val(txtMC.Text.Trim)
                    txtTotalCost.Text = Format(Val(txtTotalCost.Text), "#0.00")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtGoldLoss_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGoldLoss.LostFocus
        Try
            If Trim(txtGoldLoss.Text) = "" Then
                txtGoldLoss.Text = "0.00"
                subAddToGrid()
            Else
                If Not IsNumeric(txtGoldLoss.Text) Then
                    MessageBox.Show("GoldLoss should be Numeric.")
                    txtGoldLoss.Text = "0.00"
                    txtGoldLoss.Focus()
                Else
                    txtGoldLoss.Text = Format(Val(txtGoldLoss.Text), "#0.00")
                    subAddToGrid()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subAddToGrid()
        Try
            If fncIsValidToAdd() Then
                grdOrderEntry.MaxRows = grdOrderEntry.MaxRows + 1
                SetText(grdOrderEntry, grdOrderEntry.MaxRows, cClassCode, txtClass.Text.Trim)
                SetText(grdOrderEntry, grdOrderEntry.MaxRows, cCategoryCode, txtCategory.Text.Trim)
                SetText(grdOrderEntry, grdOrderEntry.MaxRows, cCatalogueCode, txtCalalogue.Text.Trim)
                SetText(grdOrderEntry, grdOrderEntry.MaxRows, cDescription, txtDescription.Text.Trim)
                SetText(grdOrderEntry, grdOrderEntry.MaxRows, cStoneDescription, txtStoneDescription.Text.Trim)
                SetText(grdOrderEntry, grdOrderEntry.MaxRows, cWeight, txtWeight.Text.Trim)
                SetText(grdOrderEntry, grdOrderEntry.MaxRows, cStoneWeight, txtStoneWeight.Text.Trim)
                SetText(grdOrderEntry, grdOrderEntry.MaxRows, cGoldLoss, txtGoldLoss.Text.Trim)
                SetText(grdOrderEntry, grdOrderEntry.MaxRows, cCostPerGram, txtCost.Text.Trim)
                SetText(grdOrderEntry, grdOrderEntry.MaxRows, cMC, txtMC.Text.Trim)
                SetText(grdOrderEntry, grdOrderEntry.MaxRows, cTotalCost, txtTotalCost.Text.Trim)
                SetText(grdOrderEntry, grdOrderEntry.MaxRows, cInventoryCode, "")
                SetText(grdOrderEntry, grdOrderEntry.MaxRows, cOrderInventoryCode, "")
                subCalcTotal()
                subClearControls()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function fncIsValidToAdd() As Boolean
        Dim PURCHASETYPE_WEIGHT As String = "W"
        Dim bStatus As Boolean
        Try
            bStatus = False
            If txtClass.Text.Trim = "" Then
                MessageBox.Show("Please select class code.")
                txtClass.Focus()
                Exit Function
            End If
            If txtCategory.Text.Trim = "" Then
                MessageBox.Show("Please select Category code.")
                txtCategory.Focus()
                Exit Function
            End If
            If txtCalalogue.Text.Trim = "" Then
                MessageBox.Show("Please select catalogue code.")
                txtCalalogue.Focus()
                Exit Function
            End If
            If txtDescription.Text.Trim = "" Then
                MessageBox.Show("Please enter description for the item.")
                txtDescription.Focus()
                Exit Function
            End If
            If (txtWeight.Text.Trim = "" Or Val(txtWeight.Text.Trim) = 0) And txtClass.Tag = PURCHASETYPE_WEIGHT Then
                MessageBox.Show("Please enter weight.")
                txtWeight.Focus()
                Exit Function
            End If
            If Not optRepair.Checked Then
                If (txtCost.Text.Trim = "" Or Val(txtCost.Text.Trim) = 0) And txtClass.Tag = PURCHASETYPE_WEIGHT Then
                    MessageBox.Show("Please enter Cost per gram.")
                    txtCost.Focus()
                    Exit Function
                End If
            End If
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncIsValidToAdd = bStatus
        End Try
    End Function

    Private Sub subCalcTotal()
        Dim iCnt As Integer
        Try
            txtEstiWeight.Text = 0.0
            txtEstiTotalCost.Text = 0.0
            For iCnt = 1 To grdOrderEntry.MaxRows
                txtEstiWeight.Text = Val(txtEstiWeight.Text.Trim) + Val(GetText(grdOrderEntry, iCnt, cWeight))
                txtEstiTotalCost.Text = Val(txtEstiTotalCost.Text.Trim) + Val(GetText(grdOrderEntry, iCnt, cTotalCost))
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subClearControls()
        Try
            txtClass.Clear()
            txtCategory.Clear()
            txtCalalogue.Clear()
            txtDescription.Clear()
            txtStoneDescription.Clear()
            txtWeight.Clear()
            txtStoneWeight.Clear()
            txtCost.Clear()
            txtMC.Clear()
            txtGoldLoss.Clear()
            txtTotalCost.Clear()
            txtClass.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function fncValidateMe() As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            If Not IsDate(txtOYYYY.Text.Trim & "/" & txtOMM.Text.Trim & "/" & txtODD.Text.Trim) Then
                MsgBox(" Enter valid datas for Order Date !", MsgBoxStyle.Exclamation)
                txtODD.Clear()
                txtOMM.Clear()
                txtOYYYY.Clear()
                txtODD.Focus()
                Exit Function
            End If

            If txtCustomerCode.Text.Trim.Length = 0 Then
                MsgBox(" Customer code should not be empty !", MsgBoxStyle.Exclamation)
                txtCustomerCode.Focus()
                Exit Function
            End If

            If txtSupplierCode.Text.Trim.Length = 0 Then
                MsgBox(" Supplier Code is Empty !", MsgBoxStyle.Exclamation)
                txtSupplierCode.Focus()
                Exit Function
            End If

            If grdOrderEntry.MaxRows = 0 Then
                MessageBox.Show("No Item to save.")
                txtClass.Focus()
                Exit Function
            End If

            If Val(txtEstiWeight.Text) = 0 Then
                MsgBox("Estimated weight should not be zero!", MsgBoxStyle.Exclamation)
                txtClass.Focus()
                Exit Function
            End If

            If Not IsDate(deliveryDate) Then
                MsgBox(" Enter valid datas for Delivery Date !", MsgBoxStyle.Exclamation)
                txtEstiDD.Clear()
                txtEstiMM.Clear()
                txtEstiYY.Clear()
                txtEstiDD.Focus()
                Exit Function
            End If

            If (txtEstiYY.Text.Trim & "/" & txtEstiMM.Text.Trim & "/" & txtEstiDD.Text.Trim) < (txtOYYYY.Text.Trim & "/" & txtOMM.Text.Trim & "/" & txtODD.Text.Trim) Then
                MsgBox(" Estimated Delivery Date is less than Order Date !", MsgBoxStyle.Exclamation)
                txtDeliDD.Focus()
                Exit Function
            End If

            percentageAmt = (30 / 100) * Val(txtEstiTotalCost.Text)
            If Val(txtAdvancePaid.Text) < percentageAmt Then
                If Not (optRepair.Checked And Val(txtEstiTotalCost.Text) <= 50) Then 'should not ask admin password for repair and the estimated amount is less than or equal to 50
                    If sAdminPassword.Trim = "" Then
                        'MsgBox("Advance amount is less than 30% Estimated Amount  " & Format(Val(percentageAmt), "#0.00") & " is less than Market price: $" & Format(Val(dNetTotalMarketPrice), "#0.00") & "." & vbCrLf & "Do you want to proceed with negative profit? ", vbYesNo)
                        MsgBox("Advance amount is less than 30% Estimated Amount  " & Format(Val(percentageAmt), "#0.00"), MsgBoxStyle.Exclamation)
                        txtAdvancePaid.Enabled = True
                        pnlAdminPwd.Visible = True
                        txtAdminPassword.Clear()
                        txtAdminPassword.Focus()
                        Exit Function
                    End If
                End If
            End If

            If optRepair.Checked Then
                If txtSupplierCost.Text.Trim = "" Or Val(txtSupplierCost.Text) <= 0 Then
                    MessageBox.Show("Please enter Supplier cost.")
                    txtSupplierCost.Clear()
                    txtSupplierCost.Focus()
                    Exit Function
                End If
            End If

            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncValidateMe = bStatus
        End Try
    End Function

    Private Sub txtAdminPassword_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAdminPassword.LostFocus
        If txtAdminPassword.Text.Trim = "" Then
            pnlAdminPwd.Visible = False
        End If
    End Sub

    Private Sub btnAdminPwdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdminPwdOK.Click
        Dim dsPWD As New DataSet
        Try
            If Trim(txtAdminPassword.Text) <> "" Then
                dsPWD = objUM.GetAdminPassword(Trim(txtAdminPassword.Text))
                If dsPWD.Tables(0).Rows.Count > 0 Then
                    If Trim(UCase(dsPWD.Tables(0).Rows(0)("UserGroupCode") & "")) = "ADMIN" Then
                        sAdminPassword = Trim(UCase(dsPWD.Tables(0).Rows(0)("UserGroupCode") & ""))
                        sAdminUserId = Trim(UCase(dsPWD.Tables(0).Rows(0)("UserCode") & ""))
                        pnlAdminPwd.Visible = False
                        btnSave.Focus()
                    Else
                        MessageBox.Show("Please Check the Password")
                        txtAdminPassword.Focus()
                        txtAdminPassword.Clear()
                        sAdminPassword = ""
                        sAdminUserId = ""
                        Exit Sub
                    End If
                Else
                    MessageBox.Show("Please Check the Password")
                    txtAdminPassword.Focus()
                    txtAdminPassword.Clear()
                    sAdminPassword = ""
                    sAdminUserId = ""
                    Exit Sub
                End If
            Else
                MessageBox.Show("Enter the Password")
                txtAdminPassword.Focus()
                sAdminPassword = ""
                sAdminUserId = ""
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Err.Clear()
        End Try
    End Sub

    Private Sub btnAdminPwdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdminPwdCancel.Click
        Try
            sAdminPassword = ""
            sAdminUserId = ""
            pnlAdminPwd.Visible = False
            txtAdvancePaid.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtAdvancePaid_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAdvancePaid.TextChanged
        Try
            If txtAdvancePaid.Text.Trim = "" Then Exit Sub
            If Not IsNumeric(txtAdvancePaid.Text) Then
                MessageBox.Show("Enter Numeric Value..")
                txtAdvancePaid.Clear()
                txtAdvancePaid.Focus()
                Exit Sub
            End If
            If Val(txtAdvancePaid.Text) > 0 Then
                txtBalanceAmount.Text = Format(Val(txtEstiTotalCost.Text) - Val(txtAdvancePaid.Text), "#0.00")
                Format(Val(txtAdvancePaid.Text), "#0.00")
            Else
                txtAdvancePaid.Text = "0"
                txtBalanceAmount.Text = Format(Val(txtEstiTotalCost.Text) - Val(txtAdvancePaid.Text), "#0.00")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtAdvancePaid_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAdvancePaid.LostFocus
        Try
            If optBoth.Checked Then
                If Val(txtAdvancePaid.Text) < dTradeinAmount Then
                    MsgBox("Advance Amount is less than Trade In amount $ " & Format(dTradeinAmount, "0.00") & " !!!", MsgBoxStyle.Exclamation)
                    txtAdvancePaid.Focus()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subPrintBill(ByVal sInvoiceNo As String)
        Dim frmrep As New frmReports
        Dim CR As New ReportDocument
        Try
            Dim ReportTitle As String
            Dim sSelection As String
            Dim sSalesType$
            Cursor = Cursors.WaitCursor
            strpath = Application.StartupPath & "\Reports\rptOrderReceipt_New.rpt"
            CR.Load(strpath)
            ReportLogOnToServer(CR)
            CR.Refresh()
            frmrep.ReportViewer.ReportSource = CR
            sSelection = "{tblInventoryOrderHeader.OrderNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
            frmrep.ReportViewer.SelectionFormula = sSelection
            frmrep.Show()
            'CR.PrintToPrinter(2, True, 0, 0)
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
        End Try
    End Sub

    Private Sub ClearControls()
        getCurrentDate()
        txtOrderNo.Clear()
        txtCustomerCode.Clear()
        txtCustomerName.Clear()
        txtCAddress1.Clear()
        txtCAddress2.Clear()
        txtCAddress3.Clear()
        txtNRICNo.Clear()
        txtSupplierCode.Clear()
        txtSupplierName.Clear()
        txtSAddress1.Clear()
        txtSAddress2.Clear()
        txtSAddress3.Clear()
        txtEstiDD.Clear()
        txtEstiDD.Clear()
        txtEstiMM.Clear()
        txtEstiYY.Clear()
        txtEstiTotalCost.Text = 0.0
        txtEstiWeight.Text = 0.0
        txtCurrCostPerGram.Text = 0
        txtAdvancePaid.Text = 0
        txtBalanceAmount.Text = 0
        txtRemarks.Clear()
        txtInvCode.Clear()
        txtDeliDD.Clear()
        txtDeliMM.Clear()
        txtDeliYY.Clear()
        txtPurchaseBillNo.Clear()
        txtWeight.Clear()
        txtActualTotalCost.Clear()
        txtActualCostPerGram.Clear()
        txtActualGoldLoss.Clear()
        txtSMC.Clear()
        txtPMC.Clear()
        txtOrderNo.ReadOnly = False
        txtAdvancePaid.ReadOnly = False
        lblOrderStatus.Text = "Order Getting..."
        optCash.Checked = True
        btnTradeIn.Visible = False
        btnTradeIn.Enabled = True
        txtStoneDescription.Clear()
        btnSave.Enabled = True
        optOrder.Checked = True
        subClearControls()
        subClearInventoryDetails()
        grdOrderEntry.MaxRows = 0
        sStatus = "O"
        txtOrderNo.Focus()
    End Sub

    Private Sub txtMC_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMC.LostFocus
        Try
            If Trim(txtMC.Text) = "" Then
                txtMC.Text = "0.00"
            Else
                If Not IsNumeric(txtMC.Text) Then
                    MessageBox.Show("Cost should be Numeric.")
                    txtMC.Text = "0.00"
                    txtMC.Focus()
                Else
                    txtMC.Text = Format(Val(txtMC.Text), "#0.00")
                    txtTotalCost.Text = Val(Val(txtCost.Text.Trim) * Val(txtWeight.Text.Trim)) + Val(txtMC.Text.Trim)
                    txtTotalCost.Text = Format(Val(txtTotalCost.Text), "#0.00")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub grdOrderEntry_RightClick(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_RightClickEvent) Handles grdOrderEntry.RightClick
        Try


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub mnuDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            MessageBox.Show("Delete")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnItemDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnItemDelete.Click
        Dim objOrder As New clsOrder
        Try
            'If txtOrderNo.Text.Trim <> "" Then
            If grdOrderEntry.MaxRows > 0 Then
                If Not objOrder.fncDeleteOrderDetail(txtOrderNo.Text.Trim, GetText(grdOrderEntry, grdOrderEntry.ActiveRow, cOrderInventoryCode)) Then
                    MessageBox.Show("Error in delete.")
                Else
                    grdOrderEntry.Row = grdOrderEntry.ActiveRow
                    grdOrderEntry.Action = FPSpread.ActionConstants.ActionDeleteRow
                    grdOrderEntry.MaxRows = grdOrderEntry.MaxRows - 1
                    subCalcTotal()
                End If
            End If
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            objOrder = Nothing
        End Try
    End Sub

    Private Function fncPaymodeDetailSave(ByVal sOrderNo As String) As Boolean
        Dim objPM As New clsPayMode
        Try
            Dim bPayModeFlag As Boolean
            Dim sDate As String
            Dim i As Long
            bPayModeFlag = False
            sDate = Format(Now, "dd/MM/yyyy")
            For i = 0 To UBound(arrpay, 1)
                If arrpay(i, 1) > 0 Then
                    iRet = objPM.PayModeDetailSave(sOrderNo, sDate, Trim(arrpay(i, 3)), _
                                                 Val(arrpay(i, 1)), Val(arrpay(i, 2)), _
                                                 Trim(arrpay(i, 4)), "ORDER")
                    bPayModeFlag = True
                    If Not iRet Then
                        fncPaymodeDetailSave = False
                        Exit Function
                    End If

                End If
            Next i
            If bPayModeFlag = False Then
                iRet = objPM.PayModeDetailSave(sOrderNo, sDate, "CASH", _
                                            Val(txtAdvancePaid.Text), 0, _
                                            "MANUAL", "ORDER")
                If Not iRet Then
                    fncPaymodeDetailSave = False
                    Exit Function
                End If
            End If
            fncPaymodeDetailSave = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Sub grdOrderEntry_Advance(ByVal sender As System.Object, ByVal e As AxFPSpread._DSpreadEvents_AdvanceEvent) Handles grdOrderEntry.Advance

    End Sub

    Private Sub txtClass_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtClass.TextChanged

    End Sub

    Private Sub txtCategory_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtCategory.TextChanged

    End Sub

    Private Sub txtCategory_ParentChanged(sender As Object, e As System.EventArgs) Handles txtCategory.ParentChanged

    End Sub

    Private Sub txtCalalogue_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtCalalogue.TextChanged

    End Sub

    Private Sub txtTotalCost_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtTotalCost.TextChanged

    End Sub

    Private Sub txtWeight_Resize(sender As Object, e As System.EventArgs) Handles txtWeight.Resize

    End Sub

    Private Sub txtWeight_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtWeight.TextChanged

    End Sub

    Private Sub txtCost_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtCost.TextChanged

    End Sub

    Private Sub txtStoneWeight_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtStoneWeight.TextChanged

    End Sub

    Private Sub txtMC_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtMC.TextChanged

    End Sub

    Private Sub txtGoldLoss_StyleChanged(sender As Object, e As System.EventArgs) Handles txtGoldLoss.StyleChanged

    End Sub

    Private Sub txtGoldLoss_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtGoldLoss.TextChanged

    End Sub
End Class
