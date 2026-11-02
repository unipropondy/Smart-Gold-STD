Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frmInvOrderEntry
    Inherits System.Windows.Forms.Form
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
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents btnCustomerCodeSearch As System.Windows.Forms.Button
    Friend WithEvents txtCustomerCode As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents optTodayRate As System.Windows.Forms.RadioButton
    Friend WithEvents optDeliveryDayRate As System.Windows.Forms.RadioButton
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents txtOYYYY As System.Windows.Forms.TextBox
    Friend WithEvents txtOMM As System.Windows.Forms.TextBox
    Friend WithEvents txtODD As System.Windows.Forms.TextBox
    Friend WithEvents txtCAddress3 As System.Windows.Forms.TextBox
    Friend WithEvents txtCAddress2 As System.Windows.Forms.TextBox
    Friend WithEvents txtCAddress1 As System.Windows.Forms.TextBox
    Friend WithEvents btnClassSearch As System.Windows.Forms.Button
    Friend WithEvents txtClass As System.Windows.Forms.TextBox
    Friend WithEvents txtEstimatedWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtEstimatedTotalCost As System.Windows.Forms.TextBox
    Friend WithEvents txtSupplierName As System.Windows.Forms.TextBox
    Friend WithEvents btnSupplierCodeSearch As System.Windows.Forms.Button
    Friend WithEvents txtSupplierCode As System.Windows.Forms.TextBox
    Friend WithEvents txtSAddress3 As System.Windows.Forms.TextBox
    Friend WithEvents txtSAddress2 As System.Windows.Forms.TextBox
    Friend WithEvents txtSAddress1 As System.Windows.Forms.TextBox
    Friend WithEvents txtAmountPaid As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtNRICNo As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtDYYYY As System.Windows.Forms.TextBox
    Friend WithEvents txtDMM As System.Windows.Forms.TextBox
    Friend WithEvents txtDDD As System.Windows.Forms.TextBox
    Friend WithEvents txtEYYYY As System.Windows.Forms.TextBox
    Friend WithEvents txtEMM As System.Windows.Forms.TextBox
    Friend WithEvents txtEDD As System.Windows.Forms.TextBox
    Friend WithEvents txtInventoryCode As System.Windows.Forms.TextBox
    Friend WithEvents txtActualCost As System.Windows.Forms.TextBox
    Friend WithEvents txtActualWeight As System.Windows.Forms.TextBox
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents btnOrderNoSearch As System.Windows.Forms.Button
    Friend WithEvents txtOrderNo As System.Windows.Forms.TextBox
    Friend WithEvents btnCustomer As System.Windows.Forms.Button
    Friend WithEvents txtDeliYY As System.Windows.Forms.TextBox
    Friend WithEvents txtDeliMM As System.Windows.Forms.TextBox
    Friend WithEvents txtDeliDD As System.Windows.Forms.TextBox
    Friend WithEvents txtEstiYY As System.Windows.Forms.TextBox
    Friend WithEvents txtEstiMM As System.Windows.Forms.TextBox
    Friend WithEvents txtEstiDD As System.Windows.Forms.TextBox
    Friend WithEvents txtAdvancePaid As System.Windows.Forms.TextBox
    Friend WithEvents txtEstiWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtActualTotalCost As System.Windows.Forms.TextBox
    Friend WithEvents txtWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtBalanceAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtInvCode As System.Windows.Forms.TextBox
    Friend WithEvents cboCatalogue As System.Windows.Forms.ComboBox
    Friend WithEvents cboCategory As System.Windows.Forms.ComboBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents txtEstiTotalCost As System.Windows.Forms.TextBox
    Friend WithEvents txtCurrCostPerGram As System.Windows.Forms.TextBox
    Friend WithEvents txtEstiGoldLoss As System.Windows.Forms.TextBox
    Friend WithEvents txtEstiMC As System.Windows.Forms.TextBox
    Friend WithEvents txtActualGoldLoss As System.Windows.Forms.TextBox
    Friend WithEvents txtSMC As System.Windows.Forms.TextBox
    Friend WithEvents txtPMC As System.Windows.Forms.TextBox
    Friend WithEvents txtActualCostPerGram As System.Windows.Forms.TextBox
    Friend WithEvents txtPurchaseBillNo As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents lblOrderStatus As System.Windows.Forms.Label
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents optGold As System.Windows.Forms.RadioButton
    Friend WithEvents optCash As System.Windows.Forms.RadioButton
    Friend WithEvents optBoth As System.Windows.Forms.RadioButton
    Friend WithEvents btnTradeIn As System.Windows.Forms.Button
    Friend WithEvents pnlAdminPwd As System.Windows.Forms.Panel
    Friend WithEvents btnAdminPwdCancel As System.Windows.Forms.Button
    Friend WithEvents btnAdminPwdOK As System.Windows.Forms.Button
    Friend WithEvents txtAdminPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents txtStoneWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtStoneDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents optRepair As System.Windows.Forms.RadioButton
    Friend WithEvents optOrder As System.Windows.Forms.RadioButton
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents lblNo As System.Windows.Forms.Label
    Friend WithEvents cmdPrint As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmInvOrderEntry))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.optRepair = New System.Windows.Forms.RadioButton
        Me.optOrder = New System.Windows.Forms.RadioButton
        Me.Label57 = New System.Windows.Forms.Label
        Me.txtStoneDescription = New System.Windows.Forms.TextBox
        Me.pnlAdminPwd = New System.Windows.Forms.Panel
        Me.btnAdminPwdCancel = New System.Windows.Forms.Button
        Me.btnAdminPwdOK = New System.Windows.Forms.Button
        Me.txtAdminPassword = New System.Windows.Forms.TextBox
        Me.Label45 = New System.Windows.Forms.Label
        Me.Label56 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.optBoth = New System.Windows.Forms.RadioButton
        Me.optCash = New System.Windows.Forms.RadioButton
        Me.optGold = New System.Windows.Forms.RadioButton
        Me.Label50 = New System.Windows.Forms.Label
        Me.txtEstiMC = New System.Windows.Forms.TextBox
        Me.Label49 = New System.Windows.Forms.Label
        Me.Label48 = New System.Windows.Forms.Label
        Me.Label47 = New System.Windows.Forms.Label
        Me.Label46 = New System.Windows.Forms.Label
        Me.cboCategory = New System.Windows.Forms.ComboBox
        Me.cboCatalogue = New System.Windows.Forms.ComboBox
        Me.btnCustomer = New System.Windows.Forms.Button
        Me.Label21 = New System.Windows.Forms.Label
        Me.txtNRICNo = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.txtSupplierName = New System.Windows.Forms.TextBox
        Me.btnSupplierCodeSearch = New System.Windows.Forms.Button
        Me.txtSupplierCode = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.txtSAddress3 = New System.Windows.Forms.TextBox
        Me.txtSAddress2 = New System.Windows.Forms.TextBox
        Me.txtSAddress1 = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.optDeliveryDayRate = New System.Windows.Forms.RadioButton
        Me.optTodayRate = New System.Windows.Forms.RadioButton
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnClassSearch = New System.Windows.Forms.Button
        Me.txtClass = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtCustomerName = New System.Windows.Forms.TextBox
        Me.btnCustomerCodeSearch = New System.Windows.Forms.Button
        Me.txtCustomerCode = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtCAddress3 = New System.Windows.Forms.TextBox
        Me.txtCAddress2 = New System.Windows.Forms.TextBox
        Me.txtCAddress1 = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtOYYYY = New System.Windows.Forms.TextBox
        Me.txtOMM = New System.Windows.Forms.TextBox
        Me.txtODD = New System.Windows.Forms.TextBox
        Me.lblDate = New System.Windows.Forms.Label
        Me.btnOrderNoSearch = New System.Windows.Forms.Button
        Me.txtOrderNo = New System.Windows.Forms.TextBox
        Me.lblNo = New System.Windows.Forms.Label
        Me.txtRemarks = New System.Windows.Forms.TextBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.Label31 = New System.Windows.Forms.Label
        Me.txtEstiYY = New System.Windows.Forms.TextBox
        Me.txtEstiMM = New System.Windows.Forms.TextBox
        Me.txtEstiDD = New System.Windows.Forms.TextBox
        Me.Label33 = New System.Windows.Forms.Label
        Me.txtAdvancePaid = New System.Windows.Forms.TextBox
        Me.txtBalanceAmount = New System.Windows.Forms.TextBox
        Me.Label36 = New System.Windows.Forms.Label
        Me.Label35 = New System.Windows.Forms.Label
        Me.txtEstiTotalCost = New System.Windows.Forms.TextBox
        Me.Label34 = New System.Windows.Forms.Label
        Me.txtCurrCostPerGram = New System.Windows.Forms.TextBox
        Me.txtEstiWeight = New System.Windows.Forms.TextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.txtEstiGoldLoss = New System.Windows.Forms.TextBox
        Me.TextBox3 = New System.Windows.Forms.TextBox
        Me.btnTradeIn = New System.Windows.Forms.Button
        Me.txtStoneWeight = New System.Windows.Forms.TextBox
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.pic = New System.Windows.Forms.PictureBox
        Me.lblStatus = New System.Windows.Forms.Label
        Me.txtEYYYY = New System.Windows.Forms.TextBox
        Me.txtInventoryCode = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.txtDYYYY = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.txtDMM = New System.Windows.Forms.TextBox
        Me.txtDDD = New System.Windows.Forms.TextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.txtActualCost = New System.Windows.Forms.TextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.txtActualWeight = New System.Windows.Forms.TextBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.txtAmountPaid = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtEMM = New System.Windows.Forms.TextBox
        Me.txtEDD = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtEstimatedTotalCost = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtEstimatedWeight = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.txtPurchaseBillNo = New System.Windows.Forms.TextBox
        Me.Label43 = New System.Windows.Forms.Label
        Me.Label38 = New System.Windows.Forms.Label
        Me.txtSMC = New System.Windows.Forms.TextBox
        Me.Label52 = New System.Windows.Forms.Label
        Me.txtPMC = New System.Windows.Forms.TextBox
        Me.Label53 = New System.Windows.Forms.Label
        Me.Label54 = New System.Windows.Forms.Label
        Me.Label51 = New System.Windows.Forms.Label
        Me.txtInvCode = New System.Windows.Forms.TextBox
        Me.Label37 = New System.Windows.Forms.Label
        Me.txtActualGoldLoss = New System.Windows.Forms.TextBox
        Me.txtActualTotalCost = New System.Windows.Forms.TextBox
        Me.Label39 = New System.Windows.Forms.Label
        Me.Label40 = New System.Windows.Forms.Label
        Me.Label41 = New System.Windows.Forms.Label
        Me.txtDeliYY = New System.Windows.Forms.TextBox
        Me.txtDeliMM = New System.Windows.Forms.TextBox
        Me.txtDeliDD = New System.Windows.Forms.TextBox
        Me.Label42 = New System.Windows.Forms.Label
        Me.txtWeight = New System.Windows.Forms.TextBox
        Me.Label44 = New System.Windows.Forms.Label
        Me.Label55 = New System.Windows.Forms.Label
        Me.lblOrderStatus = New System.Windows.Forms.Label
        Me.txtActualCostPerGram = New System.Windows.Forms.TextBox
        Me.btnDelete = New System.Windows.Forms.Button
        Me.cmdPrint = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.pnlAdminPwd.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Controls.Add(Me.Label57)
        Me.Panel1.Controls.Add(Me.txtStoneDescription)
        Me.Panel1.Controls.Add(Me.pnlAdminPwd)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.Label50)
        Me.Panel1.Controls.Add(Me.txtEstiMC)
        Me.Panel1.Controls.Add(Me.Label49)
        Me.Panel1.Controls.Add(Me.Label48)
        Me.Panel1.Controls.Add(Me.Label47)
        Me.Panel1.Controls.Add(Me.Label46)
        Me.Panel1.Controls.Add(Me.cboCategory)
        Me.Panel1.Controls.Add(Me.cboCatalogue)
        Me.Panel1.Controls.Add(Me.btnCustomer)
        Me.Panel1.Controls.Add(Me.Label21)
        Me.Panel1.Controls.Add(Me.txtNRICNo)
        Me.Panel1.Controls.Add(Me.Label20)
        Me.Panel1.Controls.Add(Me.txtSupplierName)
        Me.Panel1.Controls.Add(Me.btnSupplierCodeSearch)
        Me.Panel1.Controls.Add(Me.txtSupplierCode)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Controls.Add(Me.txtSAddress3)
        Me.Panel1.Controls.Add(Me.txtSAddress2)
        Me.Panel1.Controls.Add(Me.txtSAddress1)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.btnClassSearch)
        Me.Panel1.Controls.Add(Me.txtClass)
        Me.Panel1.Controls.Add(Me.Label3)
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
        Me.Panel1.Controls.Add(Me.txtRemarks)
        Me.Panel1.Controls.Add(Me.Label32)
        Me.Panel1.Controls.Add(Me.Label31)
        Me.Panel1.Controls.Add(Me.txtEstiYY)
        Me.Panel1.Controls.Add(Me.txtEstiMM)
        Me.Panel1.Controls.Add(Me.txtEstiDD)
        Me.Panel1.Controls.Add(Me.Label33)
        Me.Panel1.Controls.Add(Me.txtAdvancePaid)
        Me.Panel1.Controls.Add(Me.txtBalanceAmount)
        Me.Panel1.Controls.Add(Me.Label36)
        Me.Panel1.Controls.Add(Me.Label35)
        Me.Panel1.Controls.Add(Me.txtEstiTotalCost)
        Me.Panel1.Controls.Add(Me.Label34)
        Me.Panel1.Controls.Add(Me.txtCurrCostPerGram)
        Me.Panel1.Controls.Add(Me.txtEstiWeight)
        Me.Panel1.Controls.Add(Me.Label30)
        Me.Panel1.Controls.Add(Me.Label22)
        Me.Panel1.Controls.Add(Me.txtEstiGoldLoss)
        Me.Panel1.Controls.Add(Me.TextBox3)
        Me.Panel1.Controls.Add(Me.btnTradeIn)
        Me.Panel1.Controls.Add(Me.txtStoneWeight)
        Me.Panel1.Location = New System.Drawing.Point(32, 83)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(912, 405)
        Me.Panel1.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.optRepair)
        Me.GroupBox3.Controls.Add(Me.optOrder)
        Me.GroupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(328, 16)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(144, 32)
        Me.GroupBox3.TabIndex = 725
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
        'Label57
        '
        Me.Label57.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label57.ForeColor = System.Drawing.Color.Black
        Me.Label57.Location = New System.Drawing.Point(560, 384)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(96, 15)
        Me.Label57.TabIndex = 724
        Me.Label57.Text = "Stone Desc"
        '
        'txtStoneDescription
        '
        Me.txtStoneDescription.BackColor = System.Drawing.Color.White
        Me.txtStoneDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStoneDescription.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStoneDescription.ForeColor = System.Drawing.Color.Black
        Me.txtStoneDescription.Location = New System.Drawing.Point(680, 379)
        Me.txtStoneDescription.MaxLength = 20
        Me.txtStoneDescription.Name = "txtStoneDescription"
        Me.txtStoneDescription.Size = New System.Drawing.Size(216, 22)
        Me.txtStoneDescription.TabIndex = 23
        Me.txtStoneDescription.Tag = ""
        Me.txtStoneDescription.Text = ""
        '
        'pnlAdminPwd
        '
        Me.pnlAdminPwd.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(192, Byte))
        Me.pnlAdminPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlAdminPwd.Controls.Add(Me.btnAdminPwdCancel)
        Me.pnlAdminPwd.Controls.Add(Me.btnAdminPwdOK)
        Me.pnlAdminPwd.Controls.Add(Me.txtAdminPassword)
        Me.pnlAdminPwd.Controls.Add(Me.Label45)
        Me.pnlAdminPwd.Controls.Add(Me.Label56)
        Me.pnlAdminPwd.Location = New System.Drawing.Point(332, 149)
        Me.pnlAdminPwd.Name = "pnlAdminPwd"
        Me.pnlAdminPwd.Size = New System.Drawing.Size(246, 88)
        Me.pnlAdminPwd.TabIndex = 721
        Me.pnlAdminPwd.Visible = False
        '
        'btnAdminPwdCancel
        '
        Me.btnAdminPwdCancel.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnAdminPwdCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdminPwdCancel.Image = CType(resources.GetObject("btnAdminPwdCancel.Image"), System.Drawing.Image)
        Me.btnAdminPwdCancel.Location = New System.Drawing.Point(160, 56)
        Me.btnAdminPwdCancel.Name = "btnAdminPwdCancel"
        Me.btnAdminPwdCancel.Size = New System.Drawing.Size(64, 23)
        Me.btnAdminPwdCancel.TabIndex = 76
        Me.btnAdminPwdCancel.Text = "Cancel"
        '
        'btnAdminPwdOK
        '
        Me.btnAdminPwdOK.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnAdminPwdOK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdminPwdOK.Image = CType(resources.GetObject("btnAdminPwdOK.Image"), System.Drawing.Image)
        Me.btnAdminPwdOK.Location = New System.Drawing.Point(88, 56)
        Me.btnAdminPwdOK.Name = "btnAdminPwdOK"
        Me.btnAdminPwdOK.Size = New System.Drawing.Size(64, 23)
        Me.btnAdminPwdOK.TabIndex = 75
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
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.optBoth)
        Me.GroupBox2.Controls.Add(Me.optCash)
        Me.GroupBox2.Controls.Add(Me.optGold)
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(681, 256)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(200, 32)
        Me.GroupBox2.TabIndex = 719
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
        'Label50
        '
        Me.Label50.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.ForeColor = System.Drawing.Color.Black
        Me.Label50.Location = New System.Drawing.Point(32, 344)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(96, 15)
        Me.Label50.TabIndex = 208
        Me.Label50.Text = "Rate Fixing"
        '
        'txtEstiMC
        '
        Me.txtEstiMC.BackColor = System.Drawing.Color.White
        Me.txtEstiMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEstiMC.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEstiMC.ForeColor = System.Drawing.Color.Black
        Me.txtEstiMC.Location = New System.Drawing.Point(680, 176)
        Me.txtEstiMC.MaxLength = 20
        Me.txtEstiMC.Name = "txtEstiMC"
        Me.txtEstiMC.Size = New System.Drawing.Size(160, 22)
        Me.txtEstiMC.TabIndex = 14
        Me.txtEstiMC.Tag = ""
        Me.txtEstiMC.Text = ""
        Me.txtEstiMC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label49
        '
        Me.Label49.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.ForeColor = System.Drawing.Color.Black
        Me.Label49.Location = New System.Drawing.Point(560, 206)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(96, 15)
        Me.Label49.TabIndex = 207
        Me.Label49.Text = "Cost Per Gram"
        '
        'Label48
        '
        Me.Label48.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.ForeColor = System.Drawing.Color.Black
        Me.Label48.Location = New System.Drawing.Point(560, 151)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(104, 15)
        Me.Label48.TabIndex = 205
        Me.Label48.Text = "Gold Loss"
        '
        'Label47
        '
        Me.Label47.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.ForeColor = System.Drawing.Color.Black
        Me.Label47.Location = New System.Drawing.Point(559, 268)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(56, 15)
        Me.Label47.TabIndex = 203
        Me.Label47.Text = "Paymode "
        '
        'Label46
        '
        Me.Label46.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.Color.Black
        Me.Label46.Location = New System.Drawing.Point(32, 96)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(82, 15)
        Me.Label46.TabIndex = 202
        Me.Label46.Text = "Name"
        '
        'cboCategory
        '
        Me.cboCategory.BackColor = System.Drawing.Color.White
        Me.cboCategory.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCategory.Location = New System.Drawing.Point(680, 42)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.Size = New System.Drawing.Size(160, 24)
        Me.cboCategory.TabIndex = 7
        '
        'cboCatalogue
        '
        Me.cboCatalogue.BackColor = System.Drawing.Color.White
        Me.cboCatalogue.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCatalogue.Location = New System.Drawing.Point(680, 70)
        Me.cboCatalogue.Name = "cboCatalogue"
        Me.cboCatalogue.Size = New System.Drawing.Size(160, 24)
        Me.cboCatalogue.TabIndex = 8
        '
        'btnCustomer
        '
        Me.btnCustomer.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnCustomer.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomer.Image = CType(resources.GetObject("btnCustomer.Image"), System.Drawing.Image)
        Me.btnCustomer.Location = New System.Drawing.Point(314, 68)
        Me.btnCustomer.Name = "btnCustomer"
        Me.btnCustomer.Size = New System.Drawing.Size(48, 22)
        Me.btnCustomer.TabIndex = 200
        Me.btnCustomer.TabStop = False
        Me.btnCustomer.Text = "NEW"
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Black
        Me.Label21.Location = New System.Drawing.Point(560, 72)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(64, 15)
        Me.Label21.TabIndex = 176
        Me.Label21.Text = "Catalogue"
        '
        'txtNRICNo
        '
        Me.txtNRICNo.BackColor = System.Drawing.Color.White
        Me.txtNRICNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNRICNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNRICNo.ForeColor = System.Drawing.Color.Black
        Me.txtNRICNo.Location = New System.Drawing.Point(136, 198)
        Me.txtNRICNo.MaxLength = 20
        Me.txtNRICNo.Name = "txtNRICNo"
        Me.txtNRICNo.Size = New System.Drawing.Size(176, 22)
        Me.txtNRICNo.TabIndex = 704
        Me.txtNRICNo.Tag = ""
        Me.txtNRICNo.Text = ""
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.Location = New System.Drawing.Point(32, 200)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(56, 15)
        Me.Label20.TabIndex = 174
        Me.Label20.Text = "NRIC No."
        '
        'txtSupplierName
        '
        Me.txtSupplierName.BackColor = System.Drawing.Color.White
        Me.txtSupplierName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierName.ForeColor = System.Drawing.Color.Black
        Me.txtSupplierName.Location = New System.Drawing.Point(320, 224)
        Me.txtSupplierName.MaxLength = 20
        Me.txtSupplierName.Name = "txtSupplierName"
        Me.txtSupplierName.Size = New System.Drawing.Size(152, 22)
        Me.txtSupplierName.TabIndex = 705
        Me.txtSupplierName.Tag = ""
        Me.txtSupplierName.Text = ""
        '
        'btnSupplierCodeSearch
        '
        Me.btnSupplierCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnSupplierCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSupplierCodeSearch.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSupplierCodeSearch.Location = New System.Drawing.Point(288, 223)
        Me.btnSupplierCodeSearch.Name = "btnSupplierCodeSearch"
        Me.btnSupplierCodeSearch.Size = New System.Drawing.Size(24, 22)
        Me.btnSupplierCodeSearch.TabIndex = 270
        Me.btnSupplierCodeSearch.TabStop = False
        Me.btnSupplierCodeSearch.Text = "..."
        '
        'txtSupplierCode
        '
        Me.txtSupplierCode.BackColor = System.Drawing.Color.White
        Me.txtSupplierCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierCode.ForeColor = System.Drawing.Color.Black
        Me.txtSupplierCode.Location = New System.Drawing.Point(136, 223)
        Me.txtSupplierCode.MaxLength = 20
        Me.txtSupplierCode.Name = "txtSupplierCode"
        Me.txtSupplierCode.Size = New System.Drawing.Size(152, 22)
        Me.txtSupplierCode.TabIndex = 3
        Me.txtSupplierCode.Tag = ""
        Me.txtSupplierCode.Text = ""
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(32, 224)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(88, 15)
        Me.Label16.TabIndex = 167
        Me.Label16.Text = "Supplier Code"
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(32, 252)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(82, 15)
        Me.Label17.TabIndex = 164
        Me.Label17.Text = "Address"
        '
        'txtSAddress3
        '
        Me.txtSAddress3.BackColor = System.Drawing.Color.White
        Me.txtSAddress3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSAddress3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSAddress3.ForeColor = System.Drawing.Color.Black
        Me.txtSAddress3.Location = New System.Drawing.Point(136, 307)
        Me.txtSAddress3.MaxLength = 20
        Me.txtSAddress3.Name = "txtSAddress3"
        Me.txtSAddress3.Size = New System.Drawing.Size(336, 22)
        Me.txtSAddress3.TabIndex = 708
        Me.txtSAddress3.Tag = ""
        Me.txtSAddress3.Text = ""
        '
        'txtSAddress2
        '
        Me.txtSAddress2.BackColor = System.Drawing.Color.White
        Me.txtSAddress2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSAddress2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSAddress2.ForeColor = System.Drawing.Color.Black
        Me.txtSAddress2.Location = New System.Drawing.Point(136, 279)
        Me.txtSAddress2.MaxLength = 20
        Me.txtSAddress2.Name = "txtSAddress2"
        Me.txtSAddress2.Size = New System.Drawing.Size(336, 22)
        Me.txtSAddress2.TabIndex = 707
        Me.txtSAddress2.Tag = ""
        Me.txtSAddress2.Text = ""
        '
        'txtSAddress1
        '
        Me.txtSAddress1.BackColor = System.Drawing.Color.White
        Me.txtSAddress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSAddress1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSAddress1.ForeColor = System.Drawing.Color.Black
        Me.txtSAddress1.Location = New System.Drawing.Point(136, 251)
        Me.txtSAddress1.MaxLength = 20
        Me.txtSAddress1.Name = "txtSAddress1"
        Me.txtSAddress1.Size = New System.Drawing.Size(336, 22)
        Me.txtSAddress1.TabIndex = 706
        Me.txtSAddress1.Tag = ""
        Me.txtSAddress1.Text = ""
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.optDeliveryDayRate)
        Me.GroupBox1.Controls.Add(Me.optTodayRate)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(136, 336)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(224, 40)
        Me.GroupBox1.TabIndex = 154
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
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(560, 352)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 15)
        Me.Label5.TabIndex = 147
        Me.Label5.Text = "Remarks"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(560, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 15)
        Me.Label4.TabIndex = 143
        Me.Label4.Text = "Category"
        '
        'btnClassSearch
        '
        Me.btnClassSearch.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnClassSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClassSearch.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClassSearch.Location = New System.Drawing.Point(816, 16)
        Me.btnClassSearch.Name = "btnClassSearch"
        Me.btnClassSearch.Size = New System.Drawing.Size(24, 22)
        Me.btnClassSearch.TabIndex = 139
        Me.btnClassSearch.TabStop = False
        Me.btnClassSearch.Text = "..."
        '
        'txtClass
        '
        Me.txtClass.BackColor = System.Drawing.Color.White
        Me.txtClass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtClass.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClass.ForeColor = System.Drawing.Color.Black
        Me.txtClass.Location = New System.Drawing.Point(680, 16)
        Me.txtClass.MaxLength = 20
        Me.txtClass.Name = "txtClass"
        Me.txtClass.Size = New System.Drawing.Size(136, 22)
        Me.txtClass.TabIndex = 6
        Me.txtClass.Tag = ""
        Me.txtClass.Text = ""
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(560, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 15)
        Me.Label3.TabIndex = 140
        Me.Label3.Text = "Class"
        '
        'txtCustomerName
        '
        Me.txtCustomerName.BackColor = System.Drawing.Color.White
        Me.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerName.ForeColor = System.Drawing.Color.Black
        Me.txtCustomerName.Location = New System.Drawing.Point(136, 94)
        Me.txtCustomerName.MaxLength = 20
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(176, 22)
        Me.txtCustomerName.TabIndex = 700
        Me.txtCustomerName.Tag = ""
        Me.txtCustomerName.Text = ""
        '
        'btnCustomerCodeSearch
        '
        Me.btnCustomerCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnCustomerCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCustomerCodeSearch.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomerCodeSearch.Location = New System.Drawing.Point(288, 68)
        Me.btnCustomerCodeSearch.Name = "btnCustomerCodeSearch"
        Me.btnCustomerCodeSearch.Size = New System.Drawing.Size(24, 22)
        Me.btnCustomerCodeSearch.TabIndex = 600
        Me.btnCustomerCodeSearch.TabStop = False
        Me.btnCustomerCodeSearch.Text = "..."
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.BackColor = System.Drawing.Color.White
        Me.txtCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerCode.ForeColor = System.Drawing.Color.Black
        Me.txtCustomerCode.Location = New System.Drawing.Point(136, 68)
        Me.txtCustomerCode.MaxLength = 20
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.Size = New System.Drawing.Size(152, 22)
        Me.txtCustomerCode.TabIndex = 5
        Me.txtCustomerCode.Tag = ""
        Me.txtCustomerCode.Text = ""
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(32, 72)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 15)
        Me.Label1.TabIndex = 137
        Me.Label1.Text = "Customer Code"
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(32, 120)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(82, 15)
        Me.Label13.TabIndex = 134
        Me.Label13.Text = "Address"
        '
        'txtCAddress3
        '
        Me.txtCAddress3.BackColor = System.Drawing.Color.White
        Me.txtCAddress3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCAddress3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCAddress3.ForeColor = System.Drawing.Color.Black
        Me.txtCAddress3.Location = New System.Drawing.Point(136, 172)
        Me.txtCAddress3.MaxLength = 20
        Me.txtCAddress3.Name = "txtCAddress3"
        Me.txtCAddress3.Size = New System.Drawing.Size(176, 22)
        Me.txtCAddress3.TabIndex = 703
        Me.txtCAddress3.Tag = ""
        Me.txtCAddress3.Text = ""
        '
        'txtCAddress2
        '
        Me.txtCAddress2.BackColor = System.Drawing.Color.White
        Me.txtCAddress2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCAddress2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCAddress2.ForeColor = System.Drawing.Color.Black
        Me.txtCAddress2.Location = New System.Drawing.Point(136, 146)
        Me.txtCAddress2.MaxLength = 20
        Me.txtCAddress2.Name = "txtCAddress2"
        Me.txtCAddress2.Size = New System.Drawing.Size(336, 22)
        Me.txtCAddress2.TabIndex = 702
        Me.txtCAddress2.Tag = ""
        Me.txtCAddress2.Text = ""
        '
        'txtCAddress1
        '
        Me.txtCAddress1.BackColor = System.Drawing.Color.White
        Me.txtCAddress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCAddress1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCAddress1.ForeColor = System.Drawing.Color.Black
        Me.txtCAddress1.Location = New System.Drawing.Point(136, 120)
        Me.txtCAddress1.MaxLength = 20
        Me.txtCAddress1.Name = "txtCAddress1"
        Me.txtCAddress1.Size = New System.Drawing.Size(336, 22)
        Me.txtCAddress1.TabIndex = 701
        Me.txtCAddress1.Tag = ""
        Me.txtCAddress1.Text = ""
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(210, 42)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(8, 16)
        Me.Label6.TabIndex = 130
        Me.Label6.Text = "-"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(165, 42)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(8, 16)
        Me.Label8.TabIndex = 129
        Me.Label8.Text = "-"
        '
        'txtOYYYY
        '
        Me.txtOYYYY.BackColor = System.Drawing.Color.White
        Me.txtOYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOYYYY.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOYYYY.Location = New System.Drawing.Point(224, 42)
        Me.txtOYYYY.MaxLength = 4
        Me.txtOYYYY.Name = "txtOYYYY"
        Me.txtOYYYY.ReadOnly = True
        Me.txtOYYYY.Size = New System.Drawing.Size(32, 22)
        Me.txtOYYYY.TabIndex = 102
        Me.txtOYYYY.Tag = "ENN;"
        Me.txtOYYYY.Text = ""
        '
        'txtOMM
        '
        Me.txtOMM.BackColor = System.Drawing.Color.White
        Me.txtOMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOMM.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOMM.Location = New System.Drawing.Point(180, 42)
        Me.txtOMM.MaxLength = 2
        Me.txtOMM.Name = "txtOMM"
        Me.txtOMM.ReadOnly = True
        Me.txtOMM.Size = New System.Drawing.Size(24, 22)
        Me.txtOMM.TabIndex = 101
        Me.txtOMM.Tag = "ENN;"
        Me.txtOMM.Text = ""
        '
        'txtODD
        '
        Me.txtODD.BackColor = System.Drawing.Color.White
        Me.txtODD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtODD.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtODD.Location = New System.Drawing.Point(136, 42)
        Me.txtODD.MaxLength = 2
        Me.txtODD.Name = "txtODD"
        Me.txtODD.ReadOnly = True
        Me.txtODD.Size = New System.Drawing.Size(24, 22)
        Me.txtODD.TabIndex = 100
        Me.txtODD.Tag = "ENN;"
        Me.txtODD.Text = ""
        '
        'lblDate
        '
        Me.lblDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.ForeColor = System.Drawing.Color.Black
        Me.lblDate.Location = New System.Drawing.Point(32, 40)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(72, 20)
        Me.lblDate.TabIndex = 128
        Me.lblDate.Text = "Order Date"
        '
        'btnOrderNoSearch
        '
        Me.btnOrderNoSearch.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnOrderNoSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOrderNoSearch.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOrderNoSearch.Location = New System.Drawing.Point(288, 16)
        Me.btnOrderNoSearch.Name = "btnOrderNoSearch"
        Me.btnOrderNoSearch.Size = New System.Drawing.Size(24, 22)
        Me.btnOrderNoSearch.TabIndex = 200
        Me.btnOrderNoSearch.TabStop = False
        Me.btnOrderNoSearch.Text = "..."
        '
        'txtOrderNo
        '
        Me.txtOrderNo.BackColor = System.Drawing.Color.White
        Me.txtOrderNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOrderNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOrderNo.ForeColor = System.Drawing.Color.Black
        Me.txtOrderNo.Location = New System.Drawing.Point(136, 16)
        Me.txtOrderNo.MaxLength = 20
        Me.txtOrderNo.Name = "txtOrderNo"
        Me.txtOrderNo.Size = New System.Drawing.Size(152, 22)
        Me.txtOrderNo.TabIndex = 1
        Me.txtOrderNo.Tag = ""
        Me.txtOrderNo.Text = ""
        '
        'lblNo
        '
        Me.lblNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNo.ForeColor = System.Drawing.Color.Black
        Me.lblNo.Location = New System.Drawing.Point(32, 16)
        Me.lblNo.Name = "lblNo"
        Me.lblNo.Size = New System.Drawing.Size(64, 15)
        Me.lblNo.TabIndex = 110
        Me.lblNo.Text = "Order No"
        '
        'txtRemarks
        '
        Me.txtRemarks.BackColor = System.Drawing.Color.White
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.Black
        Me.txtRemarks.Location = New System.Drawing.Point(680, 352)
        Me.txtRemarks.MaxLength = 250
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRemarks.Size = New System.Drawing.Size(216, 22)
        Me.txtRemarks.TabIndex = 22
        Me.txtRemarks.Tag = ""
        Me.txtRemarks.Text = ""
        '
        'Label32
        '
        Me.Label32.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(712, 96)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(8, 22)
        Me.Label32.TabIndex = 165
        Me.Label32.Text = "-"
        '
        'Label31
        '
        Me.Label31.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(747, 96)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(8, 22)
        Me.Label31.TabIndex = 166
        Me.Label31.Text = "-"
        '
        'txtEstiYY
        '
        Me.txtEstiYY.BackColor = System.Drawing.Color.White
        Me.txtEstiYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEstiYY.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEstiYY.Location = New System.Drawing.Point(763, 98)
        Me.txtEstiYY.MaxLength = 4
        Me.txtEstiYY.Name = "txtEstiYY"
        Me.txtEstiYY.Size = New System.Drawing.Size(40, 22)
        Me.txtEstiYY.TabIndex = 11
        Me.txtEstiYY.Tag = "ENN;"
        Me.txtEstiYY.Text = ""
        '
        'txtEstiMM
        '
        Me.txtEstiMM.BackColor = System.Drawing.Color.White
        Me.txtEstiMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEstiMM.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEstiMM.Location = New System.Drawing.Point(720, 98)
        Me.txtEstiMM.MaxLength = 2
        Me.txtEstiMM.Name = "txtEstiMM"
        Me.txtEstiMM.Size = New System.Drawing.Size(24, 22)
        Me.txtEstiMM.TabIndex = 10
        Me.txtEstiMM.Tag = "ENN;"
        Me.txtEstiMM.Text = ""
        '
        'txtEstiDD
        '
        Me.txtEstiDD.BackColor = System.Drawing.Color.White
        Me.txtEstiDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEstiDD.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEstiDD.Location = New System.Drawing.Point(680, 98)
        Me.txtEstiDD.MaxLength = 2
        Me.txtEstiDD.Name = "txtEstiDD"
        Me.txtEstiDD.Size = New System.Drawing.Size(24, 22)
        Me.txtEstiDD.TabIndex = 9
        Me.txtEstiDD.Tag = "ENN;"
        Me.txtEstiDD.Text = ""
        '
        'Label33
        '
        Me.Label33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.Black
        Me.Label33.Location = New System.Drawing.Point(560, 101)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(96, 15)
        Me.Label33.TabIndex = 164
        Me.Label33.Text = "Date of Delivery"
        '
        'txtAdvancePaid
        '
        Me.txtAdvancePaid.BackColor = System.Drawing.Color.White
        Me.txtAdvancePaid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdvancePaid.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdvancePaid.ForeColor = System.Drawing.Color.Black
        Me.txtAdvancePaid.Location = New System.Drawing.Point(680, 295)
        Me.txtAdvancePaid.MaxLength = 20
        Me.txtAdvancePaid.Name = "txtAdvancePaid"
        Me.txtAdvancePaid.Size = New System.Drawing.Size(160, 22)
        Me.txtAdvancePaid.TabIndex = 20
        Me.txtAdvancePaid.Tag = ""
        Me.txtAdvancePaid.Text = ""
        Me.txtAdvancePaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBalanceAmount
        '
        Me.txtBalanceAmount.BackColor = System.Drawing.Color.White
        Me.txtBalanceAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBalanceAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBalanceAmount.ForeColor = System.Drawing.Color.Black
        Me.txtBalanceAmount.Location = New System.Drawing.Point(680, 324)
        Me.txtBalanceAmount.MaxLength = 20
        Me.txtBalanceAmount.Name = "txtBalanceAmount"
        Me.txtBalanceAmount.ReadOnly = True
        Me.txtBalanceAmount.Size = New System.Drawing.Size(160, 22)
        Me.txtBalanceAmount.TabIndex = 21
        Me.txtBalanceAmount.Tag = ""
        Me.txtBalanceAmount.Text = ""
        Me.txtBalanceAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label36
        '
        Me.Label36.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.Color.Black
        Me.Label36.Location = New System.Drawing.Point(560, 328)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(103, 15)
        Me.Label36.TabIndex = 174
        Me.Label36.Text = "Balance Amount"
        '
        'Label35
        '
        Me.Label35.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.Color.Black
        Me.Label35.Location = New System.Drawing.Point(560, 301)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(80, 15)
        Me.Label35.TabIndex = 172
        Me.Label35.Text = "Advance Paid"
        '
        'txtEstiTotalCost
        '
        Me.txtEstiTotalCost.BackColor = System.Drawing.Color.White
        Me.txtEstiTotalCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEstiTotalCost.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEstiTotalCost.ForeColor = System.Drawing.Color.Black
        Me.txtEstiTotalCost.Location = New System.Drawing.Point(680, 228)
        Me.txtEstiTotalCost.MaxLength = 20
        Me.txtEstiTotalCost.Name = "txtEstiTotalCost"
        Me.txtEstiTotalCost.ReadOnly = True
        Me.txtEstiTotalCost.Size = New System.Drawing.Size(160, 22)
        Me.txtEstiTotalCost.TabIndex = 16
        Me.txtEstiTotalCost.Tag = ""
        Me.txtEstiTotalCost.Text = ""
        Me.txtEstiTotalCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label34
        '
        Me.Label34.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.Color.Black
        Me.Label34.Location = New System.Drawing.Point(560, 232)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(103, 15)
        Me.Label34.TabIndex = 168
        Me.Label34.Text = "Estimated Cost"
        '
        'txtCurrCostPerGram
        '
        Me.txtCurrCostPerGram.BackColor = System.Drawing.Color.White
        Me.txtCurrCostPerGram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrCostPerGram.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrCostPerGram.ForeColor = System.Drawing.Color.Black
        Me.txtCurrCostPerGram.Location = New System.Drawing.Point(680, 202)
        Me.txtCurrCostPerGram.MaxLength = 20
        Me.txtCurrCostPerGram.Name = "txtCurrCostPerGram"
        Me.txtCurrCostPerGram.Size = New System.Drawing.Size(160, 22)
        Me.txtCurrCostPerGram.TabIndex = 15
        Me.txtCurrCostPerGram.Tag = ""
        Me.txtCurrCostPerGram.Text = ""
        Me.txtCurrCostPerGram.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtEstiWeight
        '
        Me.txtEstiWeight.BackColor = System.Drawing.Color.White
        Me.txtEstiWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEstiWeight.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEstiWeight.ForeColor = System.Drawing.Color.Black
        Me.txtEstiWeight.Location = New System.Drawing.Point(680, 124)
        Me.txtEstiWeight.MaxLength = 20
        Me.txtEstiWeight.Name = "txtEstiWeight"
        Me.txtEstiWeight.Size = New System.Drawing.Size(160, 22)
        Me.txtEstiWeight.TabIndex = 12
        Me.txtEstiWeight.Tag = ""
        Me.txtEstiWeight.Text = ""
        Me.txtEstiWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label30
        '
        Me.Label30.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.Color.Black
        Me.Label30.Location = New System.Drawing.Point(560, 181)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(104, 15)
        Me.Label30.TabIndex = 153
        Me.Label30.Text = "Mc"
        '
        'Label22
        '
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.Black
        Me.Label22.Location = New System.Drawing.Point(560, 128)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(103, 15)
        Me.Label22.TabIndex = 151
        Me.Label22.Text = "Estimated Weight"
        '
        'txtEstiGoldLoss
        '
        Me.txtEstiGoldLoss.BackColor = System.Drawing.Color.White
        Me.txtEstiGoldLoss.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEstiGoldLoss.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEstiGoldLoss.ForeColor = System.Drawing.Color.Black
        Me.txtEstiGoldLoss.Location = New System.Drawing.Point(680, 150)
        Me.txtEstiGoldLoss.MaxLength = 20
        Me.txtEstiGoldLoss.Name = "txtEstiGoldLoss"
        Me.txtEstiGoldLoss.Size = New System.Drawing.Size(160, 22)
        Me.txtEstiGoldLoss.TabIndex = 13
        Me.txtEstiGoldLoss.Tag = ""
        Me.txtEstiGoldLoss.Text = ""
        Me.txtEstiGoldLoss.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.Color.White
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox3.ForeColor = System.Drawing.Color.Black
        Me.TextBox3.Location = New System.Drawing.Point(136, 16)
        Me.TextBox3.MaxLength = 20
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(152, 22)
        Me.TextBox3.TabIndex = 0
        Me.TextBox3.Tag = ""
        Me.TextBox3.Text = ""
        '
        'btnTradeIn
        '
        Me.btnTradeIn.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnTradeIn.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTradeIn.Image = CType(resources.GetObject("btnTradeIn.Image"), System.Drawing.Image)
        Me.btnTradeIn.Location = New System.Drawing.Point(840, 296)
        Me.btnTradeIn.Name = "btnTradeIn"
        Me.btnTradeIn.Size = New System.Drawing.Size(64, 24)
        Me.btnTradeIn.TabIndex = 720
        Me.btnTradeIn.TabStop = False
        Me.btnTradeIn.Text = "TradeIn"
        Me.btnTradeIn.Visible = False
        '
        'txtStoneWeight
        '
        Me.txtStoneWeight.BackColor = System.Drawing.Color.White
        Me.txtStoneWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStoneWeight.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStoneWeight.ForeColor = System.Drawing.Color.Black
        Me.txtStoneWeight.Location = New System.Drawing.Point(336, 64)
        Me.txtStoneWeight.MaxLength = 20
        Me.txtStoneWeight.Name = "txtStoneWeight"
        Me.txtStoneWeight.Size = New System.Drawing.Size(8, 22)
        Me.txtStoneWeight.TabIndex = 723
        Me.txtStoneWeight.Tag = ""
        Me.txtStoneWeight.Text = ""
        Me.txtStoneWeight.Visible = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.Location = New System.Drawing.Point(544, 624)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(94, 23)
        Me.btnSave.TabIndex = 24
        Me.btnSave.Text = "Save (F7)"
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(849, 624)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(92, 23)
        Me.btnClose.TabIndex = 27
        Me.btnClose.Text = "Close(Esc)"
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(747, 624)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(95, 23)
        Me.btnClear.TabIndex = 25
        Me.btnClear.Text = "Clear (F6)"
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(0, 0)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(210, 75)
        Me.pic.TabIndex = 112
        Me.pic.TabStop = False
        '
        'lblStatus
        '
        Me.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblStatus.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.Color.Red
        Me.lblStatus.Location = New System.Drawing.Point(16, 336)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(216, 20)
        Me.lblStatus.TabIndex = 228
        Me.lblStatus.Text = "Status"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtEYYYY
        '
        Me.txtEYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEYYYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEYYYY.Location = New System.Drawing.Point(192, 16)
        Me.txtEYYYY.MaxLength = 4
        Me.txtEYYYY.Name = "txtEYYYY"
        Me.txtEYYYY.Size = New System.Drawing.Size(32, 20)
        Me.txtEYYYY.TabIndex = 203
        Me.txtEYYYY.Tag = "ENN;"
        Me.txtEYYYY.Text = ""
        '
        'txtInventoryCode
        '
        Me.txtInventoryCode.BackColor = System.Drawing.Color.White
        Me.txtInventoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInventoryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInventoryCode.ForeColor = System.Drawing.Color.Blue
        Me.txtInventoryCode.Location = New System.Drawing.Point(136, 304)
        Me.txtInventoryCode.MaxLength = 20
        Me.txtInventoryCode.Name = "txtInventoryCode"
        Me.txtInventoryCode.ReadOnly = True
        Me.txtInventoryCode.Size = New System.Drawing.Size(96, 20)
        Me.txtInventoryCode.TabIndex = 226
        Me.txtInventoryCode.Tag = ""
        Me.txtInventoryCode.Text = ""
        '
        'Label23
        '
        Me.Label23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.Black
        Me.Label23.Location = New System.Drawing.Point(16, 312)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(103, 15)
        Me.Label23.TabIndex = 227
        Me.Label23.Text = "Inventory Code"
        '
        'Label29
        '
        Me.Label29.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(192, 200)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(8, 16)
        Me.Label29.TabIndex = 225
        Me.Label29.Text = "-"
        '
        'txtDYYYY
        '
        Me.txtDYYYY.BackColor = System.Drawing.Color.White
        Me.txtDYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDYYYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDYYYY.ForeColor = System.Drawing.Color.Blue
        Me.txtDYYYY.Location = New System.Drawing.Point(200, 200)
        Me.txtDYYYY.MaxLength = 4
        Me.txtDYYYY.Name = "txtDYYYY"
        Me.txtDYYYY.ReadOnly = True
        Me.txtDYYYY.Size = New System.Drawing.Size(32, 20)
        Me.txtDYYYY.TabIndex = 224
        Me.txtDYYYY.Tag = "ENN;"
        Me.txtDYYYY.Text = ""
        '
        'Label24
        '
        Me.Label24.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(160, 200)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(8, 16)
        Me.Label24.TabIndex = 223
        Me.Label24.Text = "-"
        '
        'txtDMM
        '
        Me.txtDMM.BackColor = System.Drawing.Color.White
        Me.txtDMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDMM.ForeColor = System.Drawing.Color.Blue
        Me.txtDMM.Location = New System.Drawing.Point(168, 200)
        Me.txtDMM.MaxLength = 2
        Me.txtDMM.Name = "txtDMM"
        Me.txtDMM.ReadOnly = True
        Me.txtDMM.Size = New System.Drawing.Size(21, 20)
        Me.txtDMM.TabIndex = 218
        Me.txtDMM.Tag = "ENN;"
        Me.txtDMM.Text = ""
        '
        'txtDDD
        '
        Me.txtDDD.BackColor = System.Drawing.Color.White
        Me.txtDDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDDD.ForeColor = System.Drawing.Color.Blue
        Me.txtDDD.Location = New System.Drawing.Point(136, 200)
        Me.txtDDD.MaxLength = 2
        Me.txtDDD.Name = "txtDDD"
        Me.txtDDD.ReadOnly = True
        Me.txtDDD.Size = New System.Drawing.Size(21, 20)
        Me.txtDDD.TabIndex = 217
        Me.txtDDD.Tag = "ENN;"
        Me.txtDDD.Text = ""
        '
        'Label25
        '
        Me.Label25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.Black
        Me.Label25.Location = New System.Drawing.Point(16, 200)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(104, 24)
        Me.Label25.TabIndex = 222
        Me.Label25.Text = "Delivered Date"
        '
        'txtActualCost
        '
        Me.txtActualCost.BackColor = System.Drawing.Color.White
        Me.txtActualCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtActualCost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtActualCost.ForeColor = System.Drawing.Color.Blue
        Me.txtActualCost.Location = New System.Drawing.Point(136, 280)
        Me.txtActualCost.MaxLength = 20
        Me.txtActualCost.Name = "txtActualCost"
        Me.txtActualCost.ReadOnly = True
        Me.txtActualCost.Size = New System.Drawing.Size(96, 20)
        Me.txtActualCost.TabIndex = 216
        Me.txtActualCost.Tag = ""
        Me.txtActualCost.Text = ""
        '
        'Label26
        '
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.Black
        Me.Label26.Location = New System.Drawing.Point(16, 280)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(103, 15)
        Me.Label26.TabIndex = 221
        Me.Label26.Text = "Total Cost"
        '
        'Label27
        '
        Me.Label27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.Color.Black
        Me.Label27.Location = New System.Drawing.Point(16, 256)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(104, 13)
        Me.Label27.TabIndex = 220
        Me.Label27.Text = "Cost per Gram"
        '
        'txtActualWeight
        '
        Me.txtActualWeight.BackColor = System.Drawing.Color.White
        Me.txtActualWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtActualWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtActualWeight.ForeColor = System.Drawing.Color.Blue
        Me.txtActualWeight.Location = New System.Drawing.Point(136, 224)
        Me.txtActualWeight.MaxLength = 20
        Me.txtActualWeight.Name = "txtActualWeight"
        Me.txtActualWeight.ReadOnly = True
        Me.txtActualWeight.Size = New System.Drawing.Size(96, 20)
        Me.txtActualWeight.TabIndex = 214
        Me.txtActualWeight.Tag = ""
        Me.txtActualWeight.Text = ""
        '
        'Label28
        '
        Me.Label28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.Color.Black
        Me.Label28.Location = New System.Drawing.Point(16, 232)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(96, 15)
        Me.Label28.TabIndex = 219
        Me.Label28.Text = "Actual Weight"
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(16, 152)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(96, 15)
        Me.Label19.TabIndex = 213
        Me.Label19.Text = "Balance Amount"
        '
        'txtAmountPaid
        '
        Me.txtAmountPaid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmountPaid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmountPaid.ForeColor = System.Drawing.Color.Black
        Me.txtAmountPaid.Location = New System.Drawing.Point(128, 120)
        Me.txtAmountPaid.MaxLength = 20
        Me.txtAmountPaid.Name = "txtAmountPaid"
        Me.txtAmountPaid.Size = New System.Drawing.Size(96, 20)
        Me.txtAmountPaid.TabIndex = 204
        Me.txtAmountPaid.Tag = ""
        Me.txtAmountPaid.Text = ""
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(16, 128)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(80, 15)
        Me.Label18.TabIndex = 212
        Me.Label18.Text = "Amount Paid"
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(184, 16)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(8, 16)
        Me.Label12.TabIndex = 211
        Me.Label12.Text = "-"
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(152, 16)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(8, 16)
        Me.Label14.TabIndex = 210
        Me.Label14.Text = "-"
        '
        'txtEMM
        '
        Me.txtEMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEMM.Location = New System.Drawing.Point(160, 16)
        Me.txtEMM.MaxLength = 2
        Me.txtEMM.Name = "txtEMM"
        Me.txtEMM.Size = New System.Drawing.Size(21, 20)
        Me.txtEMM.TabIndex = 202
        Me.txtEMM.Tag = "ENN;"
        Me.txtEMM.Text = ""
        '
        'txtEDD
        '
        Me.txtEDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEDD.Location = New System.Drawing.Point(128, 16)
        Me.txtEDD.MaxLength = 2
        Me.txtEDD.Name = "txtEDD"
        Me.txtEDD.Size = New System.Drawing.Size(21, 20)
        Me.txtEDD.TabIndex = 201
        Me.txtEDD.Tag = "ENN;"
        Me.txtEDD.Text = ""
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(16, 16)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(96, 24)
        Me.Label15.TabIndex = 209
        Me.Label15.Text = "Estimated Date of Delivery"
        '
        'txtEstimatedTotalCost
        '
        Me.txtEstimatedTotalCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEstimatedTotalCost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEstimatedTotalCost.ForeColor = System.Drawing.Color.Black
        Me.txtEstimatedTotalCost.Location = New System.Drawing.Point(128, 96)
        Me.txtEstimatedTotalCost.MaxLength = 20
        Me.txtEstimatedTotalCost.Name = "txtEstimatedTotalCost"
        Me.txtEstimatedTotalCost.Size = New System.Drawing.Size(96, 20)
        Me.txtEstimatedTotalCost.TabIndex = 200
        Me.txtEstimatedTotalCost.Tag = ""
        Me.txtEstimatedTotalCost.Text = ""
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(16, 96)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(103, 15)
        Me.Label11.TabIndex = 208
        Me.Label11.Text = "Estimated Cost"
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(16, 72)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(88, 15)
        Me.Label10.TabIndex = 207
        Me.Label10.Text = "Cost per Gram"
        '
        'txtEstimatedWeight
        '
        Me.txtEstimatedWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEstimatedWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEstimatedWeight.ForeColor = System.Drawing.Color.Black
        Me.txtEstimatedWeight.Location = New System.Drawing.Point(128, 40)
        Me.txtEstimatedWeight.MaxLength = 20
        Me.txtEstimatedWeight.Name = "txtEstimatedWeight"
        Me.txtEstimatedWeight.Size = New System.Drawing.Size(96, 20)
        Me.txtEstimatedWeight.TabIndex = 198
        Me.txtEstimatedWeight.Tag = ""
        Me.txtEstimatedWeight.Text = ""
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(16, 40)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(103, 15)
        Me.Label7.TabIndex = 206
        Me.Label7.Text = "Estimated Weight"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.txtPurchaseBillNo)
        Me.Panel3.Controls.Add(Me.Label43)
        Me.Panel3.Controls.Add(Me.Label38)
        Me.Panel3.Controls.Add(Me.txtSMC)
        Me.Panel3.Controls.Add(Me.Label52)
        Me.Panel3.Controls.Add(Me.txtPMC)
        Me.Panel3.Controls.Add(Me.Label53)
        Me.Panel3.Controls.Add(Me.Label54)
        Me.Panel3.Controls.Add(Me.Label51)
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
        Me.Panel3.Controls.Add(Me.txtWeight)
        Me.Panel3.Controls.Add(Me.Label44)
        Me.Panel3.Controls.Add(Me.Label55)
        Me.Panel3.Controls.Add(Me.lblOrderStatus)
        Me.Panel3.Controls.Add(Me.txtActualCostPerGram)
        Me.Panel3.Location = New System.Drawing.Point(32, 488)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(912, 128)
        Me.Panel3.TabIndex = 151
        '
        'txtPurchaseBillNo
        '
        Me.txtPurchaseBillNo.BackColor = System.Drawing.Color.White
        Me.txtPurchaseBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurchaseBillNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurchaseBillNo.ForeColor = System.Drawing.Color.Black
        Me.txtPurchaseBillNo.Location = New System.Drawing.Point(136, 93)
        Me.txtPurchaseBillNo.MaxLength = 20
        Me.txtPurchaseBillNo.Name = "txtPurchaseBillNo"
        Me.txtPurchaseBillNo.ReadOnly = True
        Me.txtPurchaseBillNo.Size = New System.Drawing.Size(112, 22)
        Me.txtPurchaseBillNo.TabIndex = 32
        Me.txtPurchaseBillNo.Tag = ""
        Me.txtPurchaseBillNo.Text = ""
        Me.txtPurchaseBillNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label43
        '
        Me.Label43.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.Color.Black
        Me.Label43.Location = New System.Drawing.Point(32, 93)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(96, 15)
        Me.Label43.TabIndex = 201
        Me.Label43.Text = "Purchase BillNo"
        '
        'Label38
        '
        Me.Label38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.Color.Black
        Me.Label38.Location = New System.Drawing.Point(288, 93)
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
        Me.txtSMC.Location = New System.Drawing.Point(384, 93)
        Me.txtSMC.MaxLength = 20
        Me.txtSMC.Name = "txtSMC"
        Me.txtSMC.ReadOnly = True
        Me.txtSMC.Size = New System.Drawing.Size(112, 22)
        Me.txtSMC.TabIndex = 35
        Me.txtSMC.Tag = ""
        Me.txtSMC.Text = ""
        Me.txtSMC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label52
        '
        Me.Label52.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.ForeColor = System.Drawing.Color.Black
        Me.Label52.Location = New System.Drawing.Point(560, 61)
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
        Me.txtPMC.Location = New System.Drawing.Point(384, 61)
        Me.txtPMC.MaxLength = 20
        Me.txtPMC.Name = "txtPMC"
        Me.txtPMC.ReadOnly = True
        Me.txtPMC.Size = New System.Drawing.Size(112, 22)
        Me.txtPMC.TabIndex = 34
        Me.txtPMC.Tag = ""
        Me.txtPMC.Text = ""
        Me.txtPMC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label53
        '
        Me.Label53.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.ForeColor = System.Drawing.Color.Black
        Me.Label53.Location = New System.Drawing.Point(288, 61)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(64, 15)
        Me.Label53.TabIndex = 195
        Me.Label53.Text = "PMC"
        '
        'Label54
        '
        Me.Label54.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label54.ForeColor = System.Drawing.Color.Black
        Me.Label54.Location = New System.Drawing.Point(280, 29)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(93, 15)
        Me.Label54.TabIndex = 193
        Me.Label54.Text = " Cost Per Gram"
        '
        'Label51
        '
        Me.Label51.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.ForeColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.Label51.Location = New System.Drawing.Point(16, 5)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(200, 16)
        Me.Label51.TabIndex = 191
        Me.Label51.Text = "Actual Delivered Datas"
        '
        'txtInvCode
        '
        Me.txtInvCode.BackColor = System.Drawing.Color.White
        Me.txtInvCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvCode.ForeColor = System.Drawing.Color.Black
        Me.txtInvCode.Location = New System.Drawing.Point(136, 29)
        Me.txtInvCode.MaxLength = 20
        Me.txtInvCode.Name = "txtInvCode"
        Me.txtInvCode.ReadOnly = True
        Me.txtInvCode.Size = New System.Drawing.Size(112, 22)
        Me.txtInvCode.TabIndex = 28
        Me.txtInvCode.Tag = ""
        Me.txtInvCode.Text = ""
        Me.txtInvCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label37
        '
        Me.Label37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.Color.Black
        Me.Label37.Location = New System.Drawing.Point(32, 29)
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
        Me.txtActualGoldLoss.Location = New System.Drawing.Point(680, 61)
        Me.txtActualGoldLoss.MaxLength = 20
        Me.txtActualGoldLoss.Name = "txtActualGoldLoss"
        Me.txtActualGoldLoss.ReadOnly = True
        Me.txtActualGoldLoss.Size = New System.Drawing.Size(112, 22)
        Me.txtActualGoldLoss.TabIndex = 37
        Me.txtActualGoldLoss.Tag = ""
        Me.txtActualGoldLoss.Text = ""
        Me.txtActualGoldLoss.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtActualTotalCost
        '
        Me.txtActualTotalCost.BackColor = System.Drawing.Color.White
        Me.txtActualTotalCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtActualTotalCost.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtActualTotalCost.ForeColor = System.Drawing.Color.Black
        Me.txtActualTotalCost.Location = New System.Drawing.Point(680, 93)
        Me.txtActualTotalCost.MaxLength = 20
        Me.txtActualTotalCost.Name = "txtActualTotalCost"
        Me.txtActualTotalCost.ReadOnly = True
        Me.txtActualTotalCost.Size = New System.Drawing.Size(112, 22)
        Me.txtActualTotalCost.TabIndex = 38
        Me.txtActualTotalCost.Tag = ""
        Me.txtActualTotalCost.Text = ""
        Me.txtActualTotalCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label39
        '
        Me.Label39.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.Color.Black
        Me.Label39.Location = New System.Drawing.Point(560, 93)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(80, 15)
        Me.Label39.TabIndex = 186
        Me.Label39.Text = "Total Cost"
        '
        'Label40
        '
        Me.Label40.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(192, 61)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(8, 16)
        Me.Label40.TabIndex = 184
        Me.Label40.Text = "-"
        '
        'Label41
        '
        Me.Label41.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.Location = New System.Drawing.Point(160, 61)
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
        Me.txtDeliYY.Location = New System.Drawing.Point(200, 61)
        Me.txtDeliYY.MaxLength = 4
        Me.txtDeliYY.Name = "txtDeliYY"
        Me.txtDeliYY.ReadOnly = True
        Me.txtDeliYY.Size = New System.Drawing.Size(48, 22)
        Me.txtDeliYY.TabIndex = 31
        Me.txtDeliYY.Tag = "ENN;"
        Me.txtDeliYY.Text = ""
        '
        'txtDeliMM
        '
        Me.txtDeliMM.BackColor = System.Drawing.Color.White
        Me.txtDeliMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeliMM.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeliMM.Location = New System.Drawing.Point(168, 61)
        Me.txtDeliMM.MaxLength = 2
        Me.txtDeliMM.Name = "txtDeliMM"
        Me.txtDeliMM.ReadOnly = True
        Me.txtDeliMM.Size = New System.Drawing.Size(21, 22)
        Me.txtDeliMM.TabIndex = 30
        Me.txtDeliMM.Tag = "ENN;"
        Me.txtDeliMM.Text = ""
        '
        'txtDeliDD
        '
        Me.txtDeliDD.BackColor = System.Drawing.Color.White
        Me.txtDeliDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeliDD.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeliDD.Location = New System.Drawing.Point(136, 61)
        Me.txtDeliDD.MaxLength = 2
        Me.txtDeliDD.Name = "txtDeliDD"
        Me.txtDeliDD.ReadOnly = True
        Me.txtDeliDD.Size = New System.Drawing.Size(21, 22)
        Me.txtDeliDD.TabIndex = 29
        Me.txtDeliDD.Tag = "ENN;"
        Me.txtDeliDD.Text = ""
        '
        'Label42
        '
        Me.Label42.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.Color.Black
        Me.Label42.Location = New System.Drawing.Point(32, 61)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(97, 20)
        Me.Label42.TabIndex = 182
        Me.Label42.Text = "Delivered Date"
        '
        'txtWeight
        '
        Me.txtWeight.BackColor = System.Drawing.Color.White
        Me.txtWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWeight.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeight.ForeColor = System.Drawing.Color.Black
        Me.txtWeight.Location = New System.Drawing.Point(680, 29)
        Me.txtWeight.MaxLength = 20
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.ReadOnly = True
        Me.txtWeight.Size = New System.Drawing.Size(112, 22)
        Me.txtWeight.TabIndex = 36
        Me.txtWeight.Tag = ""
        Me.txtWeight.Text = ""
        Me.txtWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label44
        '
        Me.Label44.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.Color.Black
        Me.Label44.Location = New System.Drawing.Point(560, 29)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(103, 15)
        Me.Label44.TabIndex = 176
        Me.Label44.Text = "Weight"
        '
        'Label55
        '
        Me.Label55.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label55.ForeColor = System.Drawing.Color.Black
        Me.Label55.Location = New System.Drawing.Point(40, 29)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(96, 15)
        Me.Label55.TabIndex = 190
        Me.Label55.Text = "Inventory Code"
        '
        'lblOrderStatus
        '
        Me.lblOrderStatus.BackColor = System.Drawing.Color.Yellow
        Me.lblOrderStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOrderStatus.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOrderStatus.ForeColor = System.Drawing.Color.Red
        Me.lblOrderStatus.Location = New System.Drawing.Point(800, 29)
        Me.lblOrderStatus.Name = "lblOrderStatus"
        Me.lblOrderStatus.Size = New System.Drawing.Size(96, 79)
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
        Me.txtActualCostPerGram.Location = New System.Drawing.Point(384, 29)
        Me.txtActualCostPerGram.MaxLength = 20
        Me.txtActualCostPerGram.Name = "txtActualCostPerGram"
        Me.txtActualCostPerGram.ReadOnly = True
        Me.txtActualCostPerGram.Size = New System.Drawing.Size(112, 22)
        Me.txtActualCostPerGram.TabIndex = 33
        Me.txtActualCostPerGram.Tag = ""
        Me.txtActualCostPerGram.Text = ""
        Me.txtActualCostPerGram.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.Location = New System.Drawing.Point(645, 624)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(95, 23)
        Me.btnDelete.TabIndex = 26
        Me.btnDelete.Text = "Delete  (F8)"
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(440, 624)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.Size = New System.Drawing.Size(95, 23)
        Me.cmdPrint.TabIndex = 152
        Me.cmdPrint.Text = "Print"
        '
        'frmInvOrderEntry
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(962, 648)
        Me.Controls.Add(Me.cmdPrint)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmInvOrderEntry"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Order Entry"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.pnlAdminPwd.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

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
        txtClass.Clear()
        cboCategory.Text = ""
        cboCatalogue.Text = ""
        txtEstiDD.Clear()
        txtEstiMM.Clear()
        txtEstiYY.Clear()
        txtEstiGoldLoss.Text = 0.0
        txtEstiTotalCost.Text = 0.0
        txtEstiWeight.Text = 0.0
        txtEstiMC.Text = 0
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
        cboCategory.Text = ""
        cboCatalogue.Text = ""
        optCash.Checked = True
        btnTradeIn.Visible = False
        btnTradeIn.Enabled = True
        txtStoneDescription.Clear()
        btnSave.Enabled = True
        optOrder.Checked = True
        txtOrderNo.Focus()

    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmOrderEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        getCurrentDate()
        sStatus = "O"
        txtOrderNo.Focus()
        sAdminUserId = ""
        sAdminPassword = ""
        sTradeAmt = 0.0
        percentageAmt = 0.0
        subGetCompany()
        btnTradeIn.Visible = False
    End Sub

    Private Sub getCurrentDate()
        txtODD.Text = Format(ServerDate, "dd")
        txtOMM.Text = Format(ServerDate, "MM")
        txtOYYYY.Text = Format(ServerDate, "yyyy")
    End Sub

    Private Sub txtCustomerCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustomerCode.LostFocus
        If txtCustomerCode.Text.Trim <> "" Then
            LoadCustomerDetails(txtCustomerCode.Text.Trim)
        End If
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

    Private Sub btnCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomer.Click
        Dim frmCustomerO As New frmCustomer
        frmCustomerO.txtCustomercode.Text = txtCustomerCode.Text.Trim
        frmCustomerO.LoadCustomerDetails(txtCustomerCode.Text.Trim)
        frmCustomerO.btnNew.Visible = False
        frmCustomerO.btnDelete.Enabled = False
        frmCustomerO.ShowDialog()
        txtCustomerCode.Text = frmCustomerO.txtCustomercode.Text
        If txtCustomerCode.Text.Trim <> "" Then LoadCustomerDetails(txtCustomerCode.Text.Trim)
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

    Private Sub txtOrderNo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOrderNo.LostFocus
        If txtOrderNo.Text.Trim.Length <> 0 Then
            DispOrderDetails(txtOrderNo.Text.Trim)
        End If
    End Sub

    Private Sub txtClass_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtClass.LostFocus
        Dim ds As DataSet
        Dim dsClass As DataSet
        Dim icnt As Integer
        Try
            If txtClass.Text.Trim.Length > 0 Then
                ds = objClsOrder.GetCategory(txtClass.Text.Trim)
                If ds.Tables(0).Rows.Count > 0 Then
                    txtClass.Text = Trim(ds.Tables(0).Rows(0)("Classcode") & "")
                    cboCategory.Items.Clear()
                    For icnt = 0 To ds.Tables(0).Rows.Count - 1
                        cboCategory.Items.Add(Trim(ds.Tables(0).Rows(icnt)("categorycode") & ""))
                    Next
                    cboCategory.SelectedIndex = 0
                    cboCategory_LostFocus(sender, e)
                    ds = objClsOrder.GetClass(txtClass.Text.Trim)
                    '''Block By Durai
                    'If ds.Tables(0).Rows.Count > 0 Then
                    '    'txtCurrCostPerGram.Text = Format(Val(ds.Tables(0).Rows(0)("Rate") & ""), "0.00")
                    '    'txtCurrCostPerGram.ReadOnly = True
                    'End If
                    txtEstiDD.Focus()
                Else
                    MsgBox("No such ClassCode ! ", MsgBoxStyle.Exclamation)
                    txtClass.Clear()
                    txtClass.Focus()
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
    Public Sub DispOrderDetails(ByVal sOrderCode As String)
        Dim ds As DataSet
        Dim dsInv As DataSet
        Try

            ds = objClsOrder.GetOrderDetail(sOrderCode)
            If ds.Tables(0).Rows.Count > 0 Then
                txtOrderNo.Text = Trim(ds.Tables(0).Rows(0)("orderno") & "")
                txtODD.Text = Format(CDate(ds.Tables(0).Rows(0)("orderdate") & ""), "dd")
                txtOMM.Text = Format(CDate(ds.Tables(0).Rows(0)("orderdate") & ""), "MM")
                txtOYYYY.Text = Format(CDate(ds.Tables(0).Rows(0)("orderdate") & ""), "yyyy")
                txtCustomerCode.Text = Trim(ds.Tables(0).Rows(0)("CustomerCode") & "")
                txtClass.Text = Trim(ds.Tables(0).Rows(0)("ClassCode") & "")
                cboCategory.Text = Trim(ds.Tables(0).Rows(0)("CategoryCode") & "")
                cboCatalogue.Text = Trim(ds.Tables(0).Rows(0)("CatalogueCode") & "")
                txtSupplierCode.Text = Trim(ds.Tables(0).Rows(0)("SupplierCode") & "")
                txtEstiDD.Text = Format(CDate(ds.Tables(0).Rows(0)("DeliveryDate") & ""), "dd")
                txtEstiMM.Text = Format(CDate(ds.Tables(0).Rows(0)("DeliveryDate") & ""), "MM")
                txtEstiYY.Text = Format(CDate(ds.Tables(0).Rows(0)("DeliveryDate") & ""), "yyyy")
                txtEstiWeight.Text = Format(Val(ds.Tables(0).Rows(0)("WeightEstimated") & ""), "#0.00")
                txtEstiGoldLoss.Text = Format(Val(ds.Tables(0).Rows(0)("GoldLoss") & ""), "#0.00")
                txtEstiMC.Text = Format(Val(ds.Tables(0).Rows(0)("MC") & ""), "#0.00")
                txtEstiTotalCost.Text = Format(Val(ds.Tables(0).Rows(0)("TotalCostEstimated") & ""), "#0.00")
                txtCurrCostPerGram.Text = Format(Val(ds.Tables(0).Rows(0)("CostPerGram") & ""), "#0.00")
                txtAdvancePaid.Text = Format(Val(ds.Tables(0).Rows(0)("AdvancePaid") & ""), "#0.00")
                txtInvCode.Text = Trim(ds.Tables(0).Rows(0)("InventoryCode") & "")
                txtRemarks.Text = Trim(ds.Tables(0).Rows(0)("Remarks") & "")
                txtStoneDescription.Text = Trim(ds.Tables(0).Rows(0)("StoneDescription") & "")
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
                If Trim(ds.Tables(0).Rows(0)("InventoryCode") & "") <> "" Then
                    dsInv = objClsOrder.GetInventoryDetail(Trim(ds.Tables(0).Rows(0)("InventoryCode") & ""))
                    If dsInv.Tables(0).Rows.Count > 0 Then
                        txtInvCode.Text = Trim(dsInv.Tables(0).Rows(0)("InventoryCode") & "")
                        txtWeight.Text = Format(Val(dsInv.Tables(0).Rows(0)("Weight") & ""), "#0.00")
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
                        txtInvCode.Text = ""
                        txtWeight.Text = 0
                        txtActualTotalCost.Text = 0
                        txtActualCostPerGram.Text = 0
                        txtPMC.Text = 0
                        txtSMC.Text = 0
                        txtActualGoldLoss.Text = 0
                        txtPurchaseBillNo.Text = ""
                        txtDeliDD.Text = ""
                        txtDeliMM.Text = ""
                        txtDeliYY.Text = ""
                    End If
                Else
                    txtInvCode.Text = ""
                    txtWeight.Text = 0
                    txtActualTotalCost.Text = 0
                    txtActualCostPerGram.Text = 0
                    txtPMC.Text = 0
                    txtSMC.Text = 0
                    txtActualGoldLoss.Text = 0
                    txtPurchaseBillNo.Text = ""
                    txtDeliDD.Text = ""
                    txtDeliMM.Text = ""
                    txtDeliYY.Text = ""
                End If
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

    Private Sub frmOrderEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
            Case Keys.Down
                SendKeys.Send("{Tab}")
            Case Keys.Up
                SendKeys.Send("+{Tab}")
        End Select
    End Sub
    Private Function fncValidated() As Boolean
        fncValidated = False
        If Not IsDate(txtOYYYY.Text.Trim & "/" & txtOMM.Text.Trim & "/" & txtODD.Text.Trim) Then
            MsgBox(" Enter valid datas for Order Date !", MsgBoxStyle.Exclamation)
            txtODD.Clear()
            txtOMM.Clear()
            txtOYYYY.Clear()
            txtODD.Focus()
            Exit Function
        End If

        If txtCustomerCode.Text.Trim.Length = 0 Then
            MsgBox(" Customer Code is Empty !", MsgBoxStyle.Exclamation)
            txtCustomerCode.Focus()
            Exit Function
        End If

        If txtSupplierCode.Text.Trim.Length = 0 Then
            MsgBox(" Supplier Code is Empty !", MsgBoxStyle.Exclamation)
            txtSupplierCode.Focus()
            Exit Function
        End If

        If txtClass.Text.Trim.Length = 0 Then
            MsgBox(" Class Code is Empty !", MsgBoxStyle.Exclamation)
            txtClass.Focus()
            Exit Function
        End If

        If cboCategory.Text.Trim.Length = 0 Then
            MsgBox(" catagory is Empty !", MsgBoxStyle.Exclamation)
            cboCategory.Focus()
            Exit Function
        End If

        If cboCatalogue.Text.Trim.Length = 0 Then
            MsgBox(" Catalogue is Empty !", MsgBoxStyle.Exclamation)
            cboCatalogue.Focus()
            Exit Function
        End If

        If Val(txtEstiWeight.Text) = 0 Then
            MsgBox(" Estimated weight is not given !", MsgBoxStyle.Exclamation)
            txtEstiWeight.Focus()
            Exit Function
        End If

        '''If Val(txtEstiGoldLoss.Text) = 0 Then
        '''    MsgBox(" Estimated Gold Loss is not given !", MsgBoxStyle.Exclamation)
        '''    txtEstiGoldLoss.Focus()
        '''    Exit Function
        '''End If

        '''If Val(txtEstiMC.Text) = 0 Then
        '''    MsgBox(" Estimated Making Charge is not given !", MsgBoxStyle.Exclamation)
        '''    txtEstiMC.Focus()
        '''    Exit Function
        '''End If

        If Not IsDate(deliveryDate) Then
            MsgBox(" Enter valid datas for Delivery Date !", MsgBoxStyle.Exclamation)
            txtEstiDD.Clear()
            txtEstiMM.Clear()
            txtEstiYY.Clear()
            txtEstiDD.Focus()
            Exit Function
        End If

        If (txtEstiYY.Text.Trim & "/" & txtEstiMM.Text.Trim & "/" & txtEstiDD.Text.Trim) < (txtOYYYY.Text.Trim & "/" & txtOMM.Text.Trim & "/" & txtODD.Text.Trim) Then
            MsgBox(" Estimated Delivery Date is less than Current Date !", MsgBoxStyle.Exclamation)
            txtDeliDD.Focus()
            Exit Function
        End If

        percentageAmt = (30 / 100) * Val(txtEstiTotalCost.Text)
        If Val(txtAdvancePaid.Text) < percentageAmt Then
            If sAdminPassword.Trim = "" Then
                'MsgBox("Advance amount is less than 30% Estimated Amount  " & Format(Val(percentageAmt), "#0.00") & " is less than Market price: $" & Format(Val(dNetTotalMarketPrice), "#0.00") & "." & vbCrLf & "Do you want to proceed with negative profit? ", vbYesNo)
                MsgBox("Advance amount is less than 30% Estimated Amount  " & Format(Val(percentageAmt), "#0.00"), MsgBoxStyle.Exclamation)
                pnlAdminPwd.Visible = True
                txtAdminPassword.Clear()
                txtAdminPassword.Focus()
                Exit Function
            End If
        End If

        fncValidated = True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sRateFixed As String
        Dim sTradeInType As String
        Dim sInvType As String
        Try
            deliveryDate = txtEstiDD.Text.Trim & "/" & txtEstiMM.Text.Trim & "/" & txtEstiYY.Text.Trim
            orderedDate = txtODD.Text.Trim & "/" & txtOMM.Text.Trim & "/" & txtOYYYY.Text.Trim
            If fncValidated() Then

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
                subTotalCost()
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

                If objClsOrder.FncOrderSave(txtOrderNo.Text.Trim, orderedDate.Trim, txtCustomerCode.Text.Trim, txtClass.Text.Trim, _
                                            cboCategory.Text.Trim, cboCatalogue.Text.Trim, txtSupplierCode.Text.Trim, _
                                            deliveryDate.Trim, txtEstiWeight.Text.Trim, txtEstiGoldLoss.Text.Trim, _
                                            txtEstiMC.Text.Trim, txtCurrCostPerGram.Text.Trim, txtStoneDescription.Text.Trim, Val(txtStoneWeight.Text), tradeInRefNo, sTradeInType, sRateFixed, txtEstiTotalCost.Text.Trim, _
                                            txtAdvancePaid.Text.Trim, txtInvCode.Text.Trim, sStatus, txtRemarks.Text.Trim, sInvType, sAdminUserId.Trim) Then
                    If sInvType = "O" Then
                        MsgBox(" Order Entry Saved  ", MsgBoxStyle.Information)
                        If txtOrderNo.Text.Trim <> "" Then
                            subPrintBill(txtOrderNo.Text.Trim)
                        End If
                        ClearControls()
                    ElseIf sInvType = "R" Then
                        MsgBox(" Repair Entry Saved  ", MsgBoxStyle.Information)
                        If txtOrderNo.Text.Trim <> "" Then
                            subPrintBill(txtOrderNo.Text.Trim)
                        End If
                        ClearControls()
                    End If
                Else
                    MsgBox(" Error in Order Entry save !", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End If
            Exit Sub
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ClearControls()
    End Sub

    Private Sub cboCategory_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCategory.LostFocus
        Dim ds As DataSet
        Dim icnt As Integer
        Try
            If cboCategory.Text.Trim.Length > 0 Then
                ds = objClsOrder.GetCatelogue(cboCategory.Text.Trim)
                If ds.Tables(0).Rows.Count > 0 Then
                    cboCatalogue.Text = Trim(ds.Tables(0).Rows(0)("cataloguecode") & "")
                    cboCatalogue.Items.Clear()
                    For icnt = 0 To ds.Tables(0).Rows.Count - 1
                        cboCatalogue.Items.Add(Trim(ds.Tables(0).Rows(icnt)("cataloguecode") & ""))
                    Next
                    cboCatalogue.SelectedIndex = 0
                Else
                    MsgBox("No such CatalogueCode ! ", MsgBoxStyle.Exclamation)
                    cboCatalogue.Focus()
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

    Private Sub txtEstiWeight_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEstiWeight.TextChanged
        subTotalCost()
        Format(Val(txtEstiWeight.Text), "#0.00")
    End Sub

    Private Sub subTotalCost()
        txtEstiTotalCost.Text = Format((Val(txtEstiWeight.Text) + Val(txtEstiGoldLoss.Text)) * Val(txtCurrCostPerGram.Text) + Val(txtEstiMC.Text), "#0.00")
    End Sub

    Private Sub txtAdvancePaid_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAdvancePaid.TextChanged
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
    End Sub

    Private Sub txtEstiGoldLoss_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEstiGoldLoss.TextChanged
        subTotalCost()
        Format(Val(txtEstiGoldLoss.Text), "#0.00")
    End Sub

    Private Sub txtEstiMC_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEstiMC.TextChanged
        subTotalCost()
        Format(Val(txtEstiMC.Text), "#0.00")
    End Sub

    Private Sub txtSupplierCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSupplierCode.LostFocus
        If txtSupplierCode.Text.Trim.Length <> 0 Then
            DispSupplierDetails(txtSupplierCode.Text.Trim)
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim dr As DataRow()
        If txtOrderNo.Text.Trim <> "" Then
            If MsgBox("Do you want to Delete order no : " & txtOrderNo.Text.Trim & "? ", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If Not objClsOrder.OrderDelete(txtOrderNo.Text.Trim) Then
                    MsgBox("No such Order Number!", MsgBoxStyle.Exclamation)
                End If
            End If
            ClearControls()
        Else
            MsgBox("No Order Number to delete! ", MsgBoxStyle.Exclamation)
            txtOrderNo.Focus()
        End If
        Exit Sub
    End Sub

    Private Sub txtOrderNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtOrderNo.KeyDown
        If e.KeyCode = Keys.F1 Then
            btnOrderNoSearch_Click(sender, e)
        End If
    End Sub

    Private Sub txtSupplierCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSupplierCode.KeyDown
        If e.KeyCode = Keys.F1 Then
            btnSupplierCodeSearch_Click(sender, e)
        End If
    End Sub

    Private Sub txtClass_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtClass.KeyDown
        If e.KeyCode = Keys.F1 Then
            btnClassSearch_Click(sender, e)
        End If
    End Sub

    Private Sub txtCustomerCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCustomerCode.KeyDown
        If e.KeyCode = Keys.F1 Then
            btnCustomerCodeSearch_Click(sender, e)
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
        sAdminPassword = ""
        sAdminUserId = ""
        pnlAdminPwd.Visible = False
        txtAdvancePaid.Focus()
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


    Private Sub optGold_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optGold.CheckedChanged
        If optGold.Checked Then
            'txtAdvancePaid.Clear()
            btnTradeIn.Visible = True
            txtAdvancePaid.ReadOnly = True
        End If
    End Sub

    Private Sub optBoth_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optBoth.CheckedChanged
        If optBoth.Checked Then
            btnTradeIn.Visible = True
            txtAdvancePaid.ReadOnly = False
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

    Private Sub btnOrderNoSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOrderNoSearch.Click
        Try
            Dim frmHelpOrder As New frmHelpSearch(0, 0, "tblinventoryorder", "L", 4, "OrderNo", "", "OrderNo", "OrderNo", 80, "OrderDate", "OrderDate", 180, "CustomerCode", "CustomerCode", 60, "ClassCode", "ClassCode", 80)
            frmHelpOrder.ShowDialog()
            txtOrderNo.Text = frmHelpOrder.Var1.Trim
            If txtOrderNo.Text.Trim.Length <> 0 Then DispOrderDetails(txtOrderNo.Text.Trim)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
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

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub optCash_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optCash.CheckedChanged
        If optCash.Checked Then
            btnTradeIn.Visible = False
            txtAdvancePaid.ReadOnly = False
        End If
    End Sub
    Private Sub btnTradeIn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTradeIn.Click
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
    End Sub

    Private Sub txtAdvancePaid_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAdvancePaid.LostFocus
        If optBoth.Checked Then
            If Val(txtAdvancePaid.Text) < dTradeinAmount Then
                MsgBox("Advance Amount is less than Trade In amount $ " & Format(dTradeinAmount, "0.00") & " !!!", MsgBoxStyle.Exclamation)
                txtAdvancePaid.Focus()
                Exit Sub
            End If

        End If
    End Sub

    Private Sub txtAdminPassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAdminPassword.TextChanged

    End Sub

    Private Sub txtAdminPassword_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAdminPassword.LostFocus
        If txtAdminPassword.Text.Trim = "" Then
            pnlAdminPwd.Visible = False
        End If
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



    Private Sub txtCurrCostPerGram_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCurrCostPerGram.LostFocus
        subTotalCost()
    End Sub

    Private Sub subPrintBill(ByVal sInvoiceNo As String)
        Try
            Dim ReportTitle As String
            Dim sSelection As String
            Dim sSalesType$
            Dim frmrep As New frmReports
            Dim CR As New ReportDocument
            Cursor = Cursors.WaitCursor
            strpath = Application.StartupPath & "\Reports\rptOrderReceipt.rpt"
            CR.Load(strpath)
            If Not ReportLogOnToServer(CR) Then
                Exit Sub
            End If
            'ReportLogOnToServer(CR)
            CR.Refresh()
            frmrep.ReportViewer.ReportSource = CR
            sSelection = "{tblInventoryOrder.OrderNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
            frmrep.ReportViewer.SelectionFormula = sSelection
            frmrep.Show()
            'CR.PrintToPrinter(2, True, 0, 0)
            frmrep.ReportViewer.Visible = True
            frmrep.ReportViewer.DisplayToolbar = True
            frmrep.ReportViewer.CloseView(False)
            frmrep.WindowState = FormWindowState.Maximized
            frmrep = Nothing
            frmrep = New frmReports
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    
    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        subPrintBill(txtOrderNo.Text.Trim)
        ClearControls()
    End Sub

    Private Sub txtOrderNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOrderNo.TextChanged

    End Sub
End Class
