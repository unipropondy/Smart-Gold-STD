Public Class frmTradeInGold
    Inherits System.Windows.Forms.Form
    Dim dm As New DataManager
    Dim objMember As New clsMember
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents optType750 As System.Windows.Forms.RadioButton
    Friend WithEvents grdTrade As System.Windows.Forms.DataGridView
    Friend WithEvents optType835 As System.Windows.Forms.RadioButton
    Friend WithEvents colItemNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colGoldLoss As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCostPerGram As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPMC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPCS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTotalCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStoneDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStoneWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colClass As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents optType22K As System.Windows.Forms.RadioButton
    Friend WithEvents optType24K As System.Windows.Forms.RadioButton
    Friend WithEvents optType18K As System.Windows.Forms.RadioButton
    Const dClass = 11
    Friend WithEvents pnlCostAdminPwd As System.Windows.Forms.Panel
    Friend WithEvents btnCostAdminPwdCancel As System.Windows.Forms.Button
    Friend WithEvents btnCostAdminPwdOK As System.Windows.Forms.Button
    Friend WithEvents txtCostAdminPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtPassFind As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents optType21K As System.Windows.Forms.RadioButton
    Friend WithEvents optType20K As System.Windows.Forms.RadioButton
    Dim dCostPerGram As Double

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
    Friend WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblCustomerType As System.Windows.Forms.Label
    Friend WithEvents txtLossPerc As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCostPerGram As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtInvoiceNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress3 As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress2 As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress1 As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerCode As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblSalesAmount As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents lblTradeInAmount As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblBalanceAmount As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblTradeInLimit As System.Windows.Forms.Label
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnFClear As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents txtTotalCost As System.Windows.Forms.TextBox
    Friend WithEvents txtCostPerGram1 As System.Windows.Forms.TextBox
    Friend WithEvents txtGoldLoss As System.Windows.Forms.TextBox
    Friend WithEvents txtStoneWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtDiamondDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblS As System.Windows.Forms.Label
    Friend WithEvents lbl As System.Windows.Forms.Label
    Friend WithEvents txtMemberCode As System.Windows.Forms.TextBox
    Friend WithEvents opt24K As System.Windows.Forms.RadioButton
    Friend WithEvents opt22K As System.Windows.Forms.RadioButton
    Friend WithEvents lblClass As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents optType875 As System.Windows.Forms.RadioButton
    Friend WithEvents optType916 As System.Windows.Forms.RadioButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTradeInGold))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblClass = New System.Windows.Forms.Label()
        Me.opt24K = New System.Windows.Forms.RadioButton()
        Me.opt22K = New System.Windows.Forms.RadioButton()
        Me.txtMemberCode = New System.Windows.Forms.TextBox()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblCustomerType = New System.Windows.Forms.Label()
        Me.txtLossPerc = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCostPerGram = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtAddress3 = New System.Windows.Forms.TextBox()
        Me.txtAddress2 = New System.Windows.Forms.TextBox()
        Me.txtAddress1 = New System.Windows.Forms.TextBox()
        Me.txtCustomerCode = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblSalesAmount = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblTradeInAmount = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblBalanceAmount = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblTradeInLimit = New System.Windows.Forms.Label()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnFClear = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.txtTotalCost = New System.Windows.Forms.TextBox()
        Me.txtCostPerGram1 = New System.Windows.Forms.TextBox()
        Me.txtGoldLoss = New System.Windows.Forms.TextBox()
        Me.txtStoneWeight = New System.Windows.Forms.TextBox()
        Me.txtWeight = New System.Windows.Forms.TextBox()
        Me.txtDiamondDescription = New System.Windows.Forms.TextBox()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblS = New System.Windows.Forms.Label()
        Me.lbl = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.optType22K = New System.Windows.Forms.RadioButton()
        Me.optType24K = New System.Windows.Forms.RadioButton()
        Me.optType18K = New System.Windows.Forms.RadioButton()
        Me.optType835 = New System.Windows.Forms.RadioButton()
        Me.optType750 = New System.Windows.Forms.RadioButton()
        Me.optType916 = New System.Windows.Forms.RadioButton()
        Me.optType875 = New System.Windows.Forms.RadioButton()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.grdTrade = New System.Windows.Forms.DataGridView()
        Me.colItemNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDescription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colGoldLoss = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCostPerGram = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPMC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPCS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTotalCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colStoneDescription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colStoneWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colClass = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pnlCostAdminPwd = New System.Windows.Forms.Panel()
        Me.btnCostAdminPwdCancel = New System.Windows.Forms.Button()
        Me.btnCostAdminPwdOK = New System.Windows.Forms.Button()
        Me.txtCostAdminPassword = New System.Windows.Forms.TextBox()
        Me.txtPassFind = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.optType20K = New System.Windows.Forms.RadioButton()
        Me.optType21K = New System.Windows.Forms.RadioButton()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdTrade, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCostAdminPwd.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.lblClass)
        Me.Panel1.Controls.Add(Me.opt24K)
        Me.Panel1.Controls.Add(Me.opt22K)
        Me.Panel1.Controls.Add(Me.txtMemberCode)
        Me.Panel1.Controls.Add(Me.txtCustomerName)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.lblCustomerType)
        Me.Panel1.Controls.Add(Me.txtLossPerc)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txtCostPerGram)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtInvoiceNo)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtRemarks)
        Me.Panel1.Controls.Add(Me.txtAddress3)
        Me.Panel1.Controls.Add(Me.txtAddress2)
        Me.Panel1.Controls.Add(Me.txtAddress1)
        Me.Panel1.Controls.Add(Me.txtCustomerCode)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Location = New System.Drawing.Point(20, 107)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(618, 216)
        Me.Panel1.TabIndex = 1
        '
        'lblClass
        '
        Me.lblClass.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblClass.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClass.ForeColor = System.Drawing.Color.Black
        Me.lblClass.Location = New System.Drawing.Point(6, 192)
        Me.lblClass.Name = "lblClass"
        Me.lblClass.Size = New System.Drawing.Size(48, 20)
        Me.lblClass.TabIndex = 170
        Me.lblClass.Text = "Class"
        Me.lblClass.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'opt24K
        '
        Me.opt24K.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.opt24K.Location = New System.Drawing.Point(128, 192)
        Me.opt24K.Name = "opt24K"
        Me.opt24K.Size = New System.Drawing.Size(56, 16)
        Me.opt24K.TabIndex = 169
        Me.opt24K.Text = "24K"
        '
        'opt22K
        '
        Me.opt22K.Checked = True
        Me.opt22K.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.opt22K.Location = New System.Drawing.Point(72, 192)
        Me.opt22K.Name = "opt22K"
        Me.opt22K.Size = New System.Drawing.Size(56, 16)
        Me.opt22K.TabIndex = 168
        Me.opt22K.TabStop = True
        Me.opt22K.Text = "22K"
        '
        'txtMemberCode
        '
        Me.txtMemberCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMemberCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMemberCode.ForeColor = System.Drawing.Color.Black
        Me.txtMemberCode.Location = New System.Drawing.Point(176, 35)
        Me.txtMemberCode.MaxLength = 20
        Me.txtMemberCode.Name = "txtMemberCode"
        Me.txtMemberCode.ReadOnly = True
        Me.txtMemberCode.Size = New System.Drawing.Size(136, 20)
        Me.txtMemberCode.TabIndex = 129
        Me.txtMemberCode.TabStop = False
        Me.txtMemberCode.Tag = ""
        '
        'txtCustomerName
        '
        Me.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerName.ForeColor = System.Drawing.Color.Black
        Me.txtCustomerName.Location = New System.Drawing.Point(72, 60)
        Me.txtCustomerName.MaxLength = 20
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.ReadOnly = True
        Me.txtCustomerName.Size = New System.Drawing.Size(535, 20)
        Me.txtCustomerName.TabIndex = 113
        Me.txtCustomerName.TabStop = False
        Me.txtCustomerName.Tag = ""
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(8, 60)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(66, 20)
        Me.Label11.TabIndex = 128
        Me.Label11.Text = "Name"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCustomerType
        '
        Me.lblCustomerType.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustomerType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustomerType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerType.ForeColor = System.Drawing.Color.Red
        Me.lblCustomerType.Location = New System.Drawing.Point(320, 35)
        Me.lblCustomerType.Name = "lblCustomerType"
        Me.lblCustomerType.Size = New System.Drawing.Size(128, 20)
        Me.lblCustomerType.TabIndex = 127
        Me.lblCustomerType.Text = "Customer Type"
        Me.lblCustomerType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtLossPerc
        '
        Me.txtLossPerc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLossPerc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLossPerc.ForeColor = System.Drawing.Color.Black
        Me.txtLossPerc.Location = New System.Drawing.Point(384, 8)
        Me.txtLossPerc.MaxLength = 20
        Me.txtLossPerc.Name = "txtLossPerc"
        Me.txtLossPerc.Size = New System.Drawing.Size(64, 20)
        Me.txtLossPerc.TabIndex = 149
        Me.txtLossPerc.TabStop = False
        Me.txtLossPerc.Tag = ""
        Me.txtLossPerc.Visible = False
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(328, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 20)
        Me.Label3.TabIndex = 126
        Me.Label3.Text = "Loss(%)"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label3.Visible = False
        '
        'txtCostPerGram
        '
        Me.txtCostPerGram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCostPerGram.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostPerGram.ForeColor = System.Drawing.Color.Black
        Me.txtCostPerGram.Location = New System.Drawing.Point(248, 8)
        Me.txtCostPerGram.MaxLength = 20
        Me.txtCostPerGram.Name = "txtCostPerGram"
        Me.txtCostPerGram.ReadOnly = True
        Me.txtCostPerGram.Size = New System.Drawing.Size(72, 20)
        Me.txtCostPerGram.TabIndex = 123
        Me.txtCostPerGram.TabStop = False
        Me.txtCostPerGram.Tag = ""
        Me.txtCostPerGram.Visible = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(176, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 20)
        Me.Label2.TabIndex = 124
        Me.Label2.Text = "Cost/ Gram"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label2.Visible = False
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvoiceNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceNo.ForeColor = System.Drawing.Color.Black
        Me.txtInvoiceNo.Location = New System.Drawing.Point(72, 8)
        Me.txtInvoiceNo.MaxLength = 20
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.ReadOnly = True
        Me.txtInvoiceNo.Size = New System.Drawing.Size(96, 20)
        Me.txtInvoiceNo.TabIndex = 121
        Me.txtInvoiceNo.TabStop = False
        Me.txtInvoiceNo.Tag = ""
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(6, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 20)
        Me.Label1.TabIndex = 122
        Me.Label1.Text = "Invoice No."
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(6, 86)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(66, 20)
        Me.Label13.TabIndex = 119
        Me.Label13.Text = "Address"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(6, 164)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 20)
        Me.Label5.TabIndex = 118
        Me.Label5.Text = "Remarks"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.Black
        Me.txtRemarks.Location = New System.Drawing.Point(72, 164)
        Me.txtRemarks.MaxLength = 20
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(535, 20)
        Me.txtRemarks.TabIndex = 150
        Me.txtRemarks.Tag = ""
        '
        'txtAddress3
        '
        Me.txtAddress3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress3.ForeColor = System.Drawing.Color.Black
        Me.txtAddress3.Location = New System.Drawing.Point(72, 138)
        Me.txtAddress3.MaxLength = 20
        Me.txtAddress3.Name = "txtAddress3"
        Me.txtAddress3.ReadOnly = True
        Me.txtAddress3.Size = New System.Drawing.Size(535, 20)
        Me.txtAddress3.TabIndex = 116
        Me.txtAddress3.TabStop = False
        Me.txtAddress3.Tag = ""
        '
        'txtAddress2
        '
        Me.txtAddress2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress2.ForeColor = System.Drawing.Color.Black
        Me.txtAddress2.Location = New System.Drawing.Point(72, 112)
        Me.txtAddress2.MaxLength = 20
        Me.txtAddress2.Name = "txtAddress2"
        Me.txtAddress2.ReadOnly = True
        Me.txtAddress2.Size = New System.Drawing.Size(535, 20)
        Me.txtAddress2.TabIndex = 115
        Me.txtAddress2.TabStop = False
        Me.txtAddress2.Tag = ""
        '
        'txtAddress1
        '
        Me.txtAddress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress1.ForeColor = System.Drawing.Color.Black
        Me.txtAddress1.Location = New System.Drawing.Point(72, 86)
        Me.txtAddress1.MaxLength = 20
        Me.txtAddress1.Name = "txtAddress1"
        Me.txtAddress1.ReadOnly = True
        Me.txtAddress1.Size = New System.Drawing.Size(535, 20)
        Me.txtAddress1.TabIndex = 114
        Me.txtAddress1.TabStop = False
        Me.txtAddress1.Tag = ""
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerCode.ForeColor = System.Drawing.Color.Black
        Me.txtCustomerCode.Location = New System.Drawing.Point(72, 35)
        Me.txtCustomerCode.MaxLength = 20
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.ReadOnly = True
        Me.txtCustomerCode.Size = New System.Drawing.Size(96, 20)
        Me.txtCustomerCode.TabIndex = 111
        Me.txtCustomerCode.TabStop = False
        Me.txtCustomerCode.Tag = ""
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(6, 34)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 20)
        Me.Label6.TabIndex = 112
        Me.Label6.Text = "Customer "
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Navy
        Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label14.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.White
        Me.Label14.Location = New System.Drawing.Point(644, 107)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(128, 24)
        Me.Label14.TabIndex = 145
        Me.Label14.Text = "Sales Amount"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSalesAmount
        '
        Me.lblSalesAmount.BackColor = System.Drawing.Color.Yellow
        Me.lblSalesAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSalesAmount.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesAmount.ForeColor = System.Drawing.Color.Black
        Me.lblSalesAmount.Location = New System.Drawing.Point(644, 131)
        Me.lblSalesAmount.Name = "lblSalesAmount"
        Me.lblSalesAmount.Size = New System.Drawing.Size(128, 24)
        Me.lblSalesAmount.TabIndex = 144
        Me.lblSalesAmount.Text = "0.00"
        Me.lblSalesAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Navy
        Me.Label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label16.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.White
        Me.Label16.Location = New System.Drawing.Point(644, 155)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(128, 24)
        Me.Label16.TabIndex = 143
        Me.Label16.Text = "Trade In Amount"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTradeInAmount
        '
        Me.lblTradeInAmount.BackColor = System.Drawing.Color.Yellow
        Me.lblTradeInAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTradeInAmount.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTradeInAmount.ForeColor = System.Drawing.Color.Black
        Me.lblTradeInAmount.Location = New System.Drawing.Point(644, 179)
        Me.lblTradeInAmount.Name = "lblTradeInAmount"
        Me.lblTradeInAmount.Size = New System.Drawing.Size(128, 24)
        Me.lblTradeInAmount.TabIndex = 142
        Me.lblTradeInAmount.Text = "0.00"
        Me.lblTradeInAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Navy
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label12.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.White
        Me.Label12.Location = New System.Drawing.Point(644, 203)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(128, 24)
        Me.Label12.TabIndex = 141
        Me.Label12.Text = "Balance Amount"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBalanceAmount
        '
        Me.lblBalanceAmount.BackColor = System.Drawing.Color.Yellow
        Me.lblBalanceAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBalanceAmount.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalanceAmount.ForeColor = System.Drawing.Color.Black
        Me.lblBalanceAmount.Location = New System.Drawing.Point(644, 227)
        Me.lblBalanceAmount.Name = "lblBalanceAmount"
        Me.lblBalanceAmount.Size = New System.Drawing.Size(128, 24)
        Me.lblBalanceAmount.TabIndex = 140
        Me.lblBalanceAmount.Text = "0.00"
        Me.lblBalanceAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Navy
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(644, 251)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(128, 24)
        Me.Label10.TabIndex = 139
        Me.Label10.Text = "Trade In Limit"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTradeInLimit
        '
        Me.lblTradeInLimit.BackColor = System.Drawing.Color.Yellow
        Me.lblTradeInLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTradeInLimit.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTradeInLimit.ForeColor = System.Drawing.Color.Black
        Me.lblTradeInLimit.Location = New System.Drawing.Point(644, 275)
        Me.lblTradeInLimit.Name = "lblTradeInLimit"
        Me.lblTradeInLimit.Size = New System.Drawing.Size(128, 48)
        Me.lblTradeInLimit.TabIndex = 138
        Me.lblTradeInLimit.Text = "0.00"
        Me.lblTradeInLimit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnUpdate
        '
        Me.btnUpdate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnUpdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdate.Location = New System.Drawing.Point(565, 548)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(100, 40)
        Me.btnUpdate.TabIndex = 161
        Me.btnUpdate.Text = "Update (F7)"
        Me.btnUpdate.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(672, 548)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 163
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnFClear
        '
        Me.btnFClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnFClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFClear.Location = New System.Drawing.Point(459, 549)
        Me.btnFClear.Name = "btnFClear"
        Me.btnFClear.Size = New System.Drawing.Size(100, 40)
        Me.btnFClear.TabIndex = 162
        Me.btnFClear.Text = "Clear (F6)"
        Me.btnFClear.UseVisualStyleBackColor = False
        Me.btnFClear.Visible = False
        '
        'btnAdd
        '
        Me.btnAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnAdd.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.Location = New System.Drawing.Point(609, 462)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 158
        Me.btnAdd.Text = "Add (+)"
        Me.btnAdd.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(690, 462)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 159
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.Location = New System.Drawing.Point(609, 487)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 160
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'txtTotalCost
        '
        Me.txtTotalCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalCost.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalCost.ForeColor = System.Drawing.Color.Black
        Me.txtTotalCost.Location = New System.Drawing.Point(504, 488)
        Me.txtTotalCost.MaxLength = 20
        Me.txtTotalCost.Name = "txtTotalCost"
        Me.txtTotalCost.Size = New System.Drawing.Size(99, 22)
        Me.txtTotalCost.TabIndex = 157
        Me.txtTotalCost.Tag = ""
        '
        'txtCostPerGram1
        '
        Me.txtCostPerGram1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCostPerGram1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostPerGram1.ForeColor = System.Drawing.Color.Black
        Me.txtCostPerGram1.Location = New System.Drawing.Point(416, 488)
        Me.txtCostPerGram1.MaxLength = 20
        Me.txtCostPerGram1.Name = "txtCostPerGram1"
        Me.txtCostPerGram1.Size = New System.Drawing.Size(88, 22)
        Me.txtCostPerGram1.TabIndex = 156
        Me.txtCostPerGram1.Tag = ""
        '
        'txtGoldLoss
        '
        Me.txtGoldLoss.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldLoss.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoldLoss.ForeColor = System.Drawing.Color.Black
        Me.txtGoldLoss.Location = New System.Drawing.Point(328, 488)
        Me.txtGoldLoss.MaxLength = 20
        Me.txtGoldLoss.Name = "txtGoldLoss"
        Me.txtGoldLoss.Size = New System.Drawing.Size(88, 22)
        Me.txtGoldLoss.TabIndex = 155
        Me.txtGoldLoss.Tag = ""
        '
        'txtStoneWeight
        '
        Me.txtStoneWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStoneWeight.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStoneWeight.ForeColor = System.Drawing.Color.Black
        Me.txtStoneWeight.Location = New System.Drawing.Point(240, 512)
        Me.txtStoneWeight.MaxLength = 20
        Me.txtStoneWeight.Name = "txtStoneWeight"
        Me.txtStoneWeight.Size = New System.Drawing.Size(88, 22)
        Me.txtStoneWeight.TabIndex = 154
        Me.txtStoneWeight.Tag = ""
        '
        'txtWeight
        '
        Me.txtWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWeight.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeight.ForeColor = System.Drawing.Color.Black
        Me.txtWeight.Location = New System.Drawing.Point(240, 488)
        Me.txtWeight.MaxLength = 20
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.Size = New System.Drawing.Size(88, 22)
        Me.txtWeight.TabIndex = 153
        Me.txtWeight.Tag = ""
        '
        'txtDiamondDescription
        '
        Me.txtDiamondDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDiamondDescription.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDiamondDescription.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiamondDescription.ForeColor = System.Drawing.Color.Black
        Me.txtDiamondDescription.Location = New System.Drawing.Point(16, 512)
        Me.txtDiamondDescription.MaxLength = 20
        Me.txtDiamondDescription.Name = "txtDiamondDescription"
        Me.txtDiamondDescription.Size = New System.Drawing.Size(224, 22)
        Me.txtDiamondDescription.TabIndex = 152
        Me.txtDiamondDescription.Tag = ""
        '
        'txtDescription
        '
        Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescription.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.ForeColor = System.Drawing.Color.Black
        Me.txtDescription.Location = New System.Drawing.Point(16, 488)
        Me.txtDescription.MaxLength = 20
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(224, 22)
        Me.txtDescription.TabIndex = 151
        Me.txtDescription.Tag = ""
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(504, 456)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(99, 32)
        Me.Label8.TabIndex = 150
        Me.Label8.Text = "Total Cost"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(416, 456)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(88, 32)
        Me.Label7.TabIndex = 149
        Me.Label7.Text = "Cost Per Gram"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(328, 456)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 32)
        Me.Label4.TabIndex = 148
        Me.Label4.Text = "Percentage"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblS
        '
        Me.lblS.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblS.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblS.ForeColor = System.Drawing.Color.Black
        Me.lblS.Location = New System.Drawing.Point(240, 456)
        Me.lblS.Name = "lblS"
        Me.lblS.Size = New System.Drawing.Size(88, 32)
        Me.lblS.TabIndex = 147
        Me.lblS.Text = "Weight"
        Me.lblS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl
        '
        Me.lbl.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl.ForeColor = System.Drawing.Color.Black
        Me.lbl.Location = New System.Drawing.Point(16, 456)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(224, 32)
        Me.lbl.TabIndex = 146
        Me.lbl.Text = "Description"
        Me.lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.optType21K)
        Me.GroupBox1.Controls.Add(Me.optType20K)
        Me.GroupBox1.Controls.Add(Me.optType22K)
        Me.GroupBox1.Controls.Add(Me.optType24K)
        Me.GroupBox1.Controls.Add(Me.optType18K)
        Me.GroupBox1.Controls.Add(Me.optType835)
        Me.GroupBox1.Controls.Add(Me.optType750)
        Me.GroupBox1.Controls.Add(Me.optType916)
        Me.GroupBox1.Controls.Add(Me.optType875)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox1.Location = New System.Drawing.Point(19, 540)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(413, 53)
        Me.GroupBox1.TabIndex = 166
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Class"
        '
        'optType22K
        '
        Me.optType22K.Checked = True
        Me.optType22K.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optType22K.Location = New System.Drawing.Point(227, 18)
        Me.optType22K.Name = "optType22K"
        Me.optType22K.Size = New System.Drawing.Size(53, 18)
        Me.optType22K.TabIndex = 175
        Me.optType22K.TabStop = True
        Me.optType22K.Text = "22K"
        '
        'optType24K
        '
        Me.optType24K.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optType24K.Location = New System.Drawing.Point(284, 18)
        Me.optType24K.Name = "optType24K"
        Me.optType24K.Size = New System.Drawing.Size(64, 16)
        Me.optType24K.TabIndex = 174
        Me.optType24K.Text = "24K"
        '
        'optType18K
        '
        Me.optType18K.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optType18K.Location = New System.Drawing.Point(351, 18)
        Me.optType18K.Name = "optType18K"
        Me.optType18K.Size = New System.Drawing.Size(56, 16)
        Me.optType18K.TabIndex = 173
        Me.optType18K.Text = "18K"
        '
        'optType835
        '
        Me.optType835.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optType835.Location = New System.Drawing.Point(52, 18)
        Me.optType835.Name = "optType835"
        Me.optType835.Size = New System.Drawing.Size(56, 16)
        Me.optType835.TabIndex = 172
        Me.optType835.Text = "835"
        Me.optType835.Visible = False
        '
        'optType750
        '
        Me.optType750.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optType750.ForeColor = System.Drawing.Color.DeepPink
        Me.optType750.Location = New System.Drawing.Point(6, 18)
        Me.optType750.Name = "optType750"
        Me.optType750.Size = New System.Drawing.Size(50, 16)
        Me.optType750.TabIndex = 171
        Me.optType750.Text = "750"
        Me.optType750.Visible = False
        '
        'optType916
        '
        Me.optType916.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optType916.Location = New System.Drawing.Point(171, 18)
        Me.optType916.Name = "optType916"
        Me.optType916.Size = New System.Drawing.Size(56, 16)
        Me.optType916.TabIndex = 170
        Me.optType916.Text = "916"
        Me.optType916.Visible = False
        '
        'optType875
        '
        Me.optType875.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optType875.ForeColor = System.Drawing.Color.Red
        Me.optType875.Location = New System.Drawing.Point(114, 18)
        Me.optType875.Name = "optType875"
        Me.optType875.Size = New System.Drawing.Size(56, 16)
        Me.optType875.TabIndex = 169
        Me.optType875.Text = "875"
        Me.optType875.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(20, 1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(270, 100)
        Me.PictureBox1.TabIndex = 167
        Me.PictureBox1.TabStop = False
        '
        'grdTrade
        '
        Me.grdTrade.AllowUserToAddRows = False
        Me.grdTrade.AllowUserToDeleteRows = False
        Me.grdTrade.AllowUserToOrderColumns = True
        Me.grdTrade.AllowUserToResizeColumns = False
        Me.grdTrade.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdTrade.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdTrade.ColumnHeadersHeight = 35
        Me.grdTrade.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colItemNo, Me.colDescription, Me.colWeight, Me.colGoldLoss, Me.colCostPerGram, Me.colPMC, Me.colPCS, Me.colTotalCost, Me.colStoneDescription, Me.colStoneWeight, Me.colClass})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdTrade.DefaultCellStyle = DataGridViewCellStyle2
        Me.grdTrade.Location = New System.Drawing.Point(19, 326)
        Me.grdTrade.Name = "grdTrade"
        Me.grdTrade.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdTrade.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.grdTrade.RowHeadersVisible = False
        Me.grdTrade.Size = New System.Drawing.Size(753, 127)
        Me.grdTrade.TabIndex = 515
        '
        'colItemNo
        '
        Me.colItemNo.HeaderText = "InventoryCode"
        Me.colItemNo.Name = "colItemNo"
        Me.colItemNo.ReadOnly = True
        Me.colItemNo.Width = 105
        '
        'colDescription
        '
        Me.colDescription.HeaderText = "Description"
        Me.colDescription.Name = "colDescription"
        Me.colDescription.ReadOnly = True
        Me.colDescription.Width = 200
        '
        'colWeight
        '
        Me.colWeight.HeaderText = "Weight"
        Me.colWeight.Name = "colWeight"
        Me.colWeight.ReadOnly = True
        Me.colWeight.Width = 60
        '
        'colGoldLoss
        '
        Me.colGoldLoss.HeaderText = "GoldLoss"
        Me.colGoldLoss.Name = "colGoldLoss"
        Me.colGoldLoss.ReadOnly = True
        Me.colGoldLoss.Visible = False
        Me.colGoldLoss.Width = 60
        '
        'colCostPerGram
        '
        Me.colCostPerGram.HeaderText = "Cost/Gm"
        Me.colCostPerGram.Name = "colCostPerGram"
        Me.colCostPerGram.ReadOnly = True
        Me.colCostPerGram.Width = 70
        '
        'colPMC
        '
        Me.colPMC.HeaderText = "PMC"
        Me.colPMC.Name = "colPMC"
        Me.colPMC.ReadOnly = True
        Me.colPMC.Visible = False
        '
        'colPCS
        '
        Me.colPCS.HeaderText = "PCS"
        Me.colPCS.Name = "colPCS"
        Me.colPCS.ReadOnly = True
        Me.colPCS.Visible = False
        '
        'colTotalCost
        '
        Me.colTotalCost.HeaderText = "Total Cost"
        Me.colTotalCost.Name = "colTotalCost"
        Me.colTotalCost.ReadOnly = True
        '
        'colStoneDescription
        '
        Me.colStoneDescription.HeaderText = "Stone Description"
        Me.colStoneDescription.Name = "colStoneDescription"
        Me.colStoneDescription.ReadOnly = True
        '
        'colStoneWeight
        '
        Me.colStoneWeight.HeaderText = "Stone Weight"
        Me.colStoneWeight.Name = "colStoneWeight"
        Me.colStoneWeight.ReadOnly = True
        Me.colStoneWeight.Width = 60
        '
        'colClass
        '
        Me.colClass.HeaderText = "Class"
        Me.colClass.Name = "colClass"
        Me.colClass.ReadOnly = True
        Me.colClass.Width = 50
        '
        'pnlCostAdminPwd
        '
        Me.pnlCostAdminPwd.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlCostAdminPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCostAdminPwd.Controls.Add(Me.btnCostAdminPwdCancel)
        Me.pnlCostAdminPwd.Controls.Add(Me.btnCostAdminPwdOK)
        Me.pnlCostAdminPwd.Controls.Add(Me.txtCostAdminPassword)
        Me.pnlCostAdminPwd.Controls.Add(Me.txtPassFind)
        Me.pnlCostAdminPwd.Controls.Add(Me.Label33)
        Me.pnlCostAdminPwd.Location = New System.Drawing.Point(268, 263)
        Me.pnlCostAdminPwd.Name = "pnlCostAdminPwd"
        Me.pnlCostAdminPwd.Size = New System.Drawing.Size(246, 88)
        Me.pnlCostAdminPwd.TabIndex = 516
        Me.pnlCostAdminPwd.Visible = False
        '
        'btnCostAdminPwdCancel
        '
        Me.btnCostAdminPwdCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCostAdminPwdCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCostAdminPwdCancel.Image = CType(resources.GetObject("btnCostAdminPwdCancel.Image"), System.Drawing.Image)
        Me.btnCostAdminPwdCancel.Location = New System.Drawing.Point(160, 56)
        Me.btnCostAdminPwdCancel.Name = "btnCostAdminPwdCancel"
        Me.btnCostAdminPwdCancel.Size = New System.Drawing.Size(64, 23)
        Me.btnCostAdminPwdCancel.TabIndex = 75
        Me.btnCostAdminPwdCancel.Text = "Cancel"
        Me.btnCostAdminPwdCancel.UseVisualStyleBackColor = False
        '
        'btnCostAdminPwdOK
        '
        Me.btnCostAdminPwdOK.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCostAdminPwdOK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCostAdminPwdOK.Image = CType(resources.GetObject("btnCostAdminPwdOK.Image"), System.Drawing.Image)
        Me.btnCostAdminPwdOK.Location = New System.Drawing.Point(88, 56)
        Me.btnCostAdminPwdOK.Name = "btnCostAdminPwdOK"
        Me.btnCostAdminPwdOK.Size = New System.Drawing.Size(64, 23)
        Me.btnCostAdminPwdOK.TabIndex = 74
        Me.btnCostAdminPwdOK.Text = "OK"
        Me.btnCostAdminPwdOK.UseVisualStyleBackColor = False
        '
        'txtCostAdminPassword
        '
        Me.txtCostAdminPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCostAdminPassword.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostAdminPassword.ForeColor = System.Drawing.Color.Black
        Me.txtCostAdminPassword.Location = New System.Drawing.Point(88, 32)
        Me.txtCostAdminPassword.MaxLength = 20
        Me.txtCostAdminPassword.Name = "txtCostAdminPassword"
        Me.txtCostAdminPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtCostAdminPassword.Size = New System.Drawing.Size(136, 20)
        Me.txtCostAdminPassword.TabIndex = 73
        Me.txtCostAdminPassword.Tag = ""
        '
        'txtPassFind
        '
        Me.txtPassFind.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassFind.ForeColor = System.Drawing.Color.Black
        Me.txtPassFind.Location = New System.Drawing.Point(16, 33)
        Me.txtPassFind.Name = "txtPassFind"
        Me.txtPassFind.Size = New System.Drawing.Size(64, 15)
        Me.txtPassFind.TabIndex = 72
        Me.txtPassFind.Text = "Password"
        '
        'Label33
        '
        Me.Label33.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.Black
        Me.Label33.Location = New System.Drawing.Point(8, 8)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(56, 15)
        Me.Label33.TabIndex = 72
        Me.Label33.Text = "Admin"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'optType20K
        '
        Me.optType20K.Checked = True
        Me.optType20K.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optType20K.Location = New System.Drawing.Point(171, 17)
        Me.optType20K.Name = "optType20K"
        Me.optType20K.Size = New System.Drawing.Size(53, 18)
        Me.optType20K.TabIndex = 176
        Me.optType20K.TabStop = True
        Me.optType20K.Text = "20K"
        '
        'optType21K
        '
        Me.optType21K.Checked = True
        Me.optType21K.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optType21K.Location = New System.Drawing.Point(110, 17)
        Me.optType21K.Name = "optType21K"
        Me.optType21K.Size = New System.Drawing.Size(53, 18)
        Me.optType21K.TabIndex = 177
        Me.optType21K.TabStop = True
        Me.optType21K.Text = "21K"
        '
        'frmTradeInGold
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(782, 614)
        Me.Controls.Add(Me.pnlCostAdminPwd)
        Me.Controls.Add(Me.grdTrade)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnFClear)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.txtTotalCost)
        Me.Controls.Add(Me.txtCostPerGram1)
        Me.Controls.Add(Me.txtGoldLoss)
        Me.Controls.Add(Me.txtStoneWeight)
        Me.Controls.Add(Me.txtWeight)
        Me.Controls.Add(Me.txtDiamondDescription)
        Me.Controls.Add(Me.txtDescription)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblS)
        Me.Controls.Add(Me.lbl)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.lblSalesAmount)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.lblTradeInAmount)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.lblBalanceAmount)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.lblTradeInLimit)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTradeInGold"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Trade In Sales"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdTrade, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCostAdminPwd.ResumeLayout(False)
        Me.pnlCostAdminPwd.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region
    Public sinventorycode As String
    Public sInvoiceDate As String
    Dim dCost As Double
    Dim sItemNo As Long
    Dim dLocalCost As Double
    Dim dForeignCost As Double
    Dim bEdited As Boolean
    Dim dTotalWeight, dTotalGoldLoss, dTotalMC, dTotalPCS, dTotalLocalCost, dTotalForeignCost As Double
    Dim msPurchaseType$
    Const GRDROWHT = 17.708
    Public sInvoiceNo As String
    Public sCustomerCode As String
    Public sMemberCode As String
    Public sSalesAmount As Double
    Dim sSalesType As String
    Dim msPayMode$
    Dim mbTransaction As Boolean
    Dim mdCommission As Double
    Dim mdTradeInAmt As Double
    Public sCalledFrom As String
    Public oTSalesConn As New ADODB.Connection
    Public oTSalesRs As New ADODB.Recordset
    Dim dTotAmount As Double = 0
    Public dWeightGrd, dGoldLossGrd, dMCGrd, dPCSGrd, dTotalCostGrd As Double
    Private Function fncValidate() As Boolean
        If UCase(txtCustomerCode.Text) = "CASH" And UCase(sCompanyShortCode) <> "GMT" Then
            If txtMemberCode.Text.Trim = "" Then
                MsgBox("MemberCode is NOT entered !", MsgBoxStyle.Critical)
                fncValidate = False
                txtCustomerCode.Focus()
                Exit Function
            End If
        End If
        If grdTrade.RowCount < 1 Then
            MsgBox("Enter trade in item details!!", vbCritical)
            fncValidate = False
            Exit Function
        End If
        If UCase(sCompanyShortCode) = "GMT" Then
            If Val(lblBalanceAmount.Text) + dTradeinLimit < 0 Then
                MsgBox("Balance Exceeds the Limit!!!", vbCritical)
                fncValidate = False
                Exit Function
            End If
        End If
        fncValidate = True
    End Function
    Private Function fncSaveTradeIn() As Boolean
        Dim SBll As New clsTradeIN
        Dim SBlm As New clsInventory
        Dim adoRS As ADODB.Recordset
        Dim frmSalesInvoice As New frmGPOS
        Dim sError As String
        Dim iRet As Boolean
        Dim frmPm As New clsPayMode
        Dim i As Long
        Dim sSql As String
        iRet = SBll.TradeInDelete(Trim(sinventorycode))
        If Trim(sError) = "" Then

            If UCase(Trim(sCalledFrom)) = "ORDERFORM" Then
                dPaidAmount = Val(fnGetdgvText(grdTrade, grdTrade.RowCount - 1, 8)) + (Val(fnGetdgvText(grdTrade, grdTrade.RowCount - 1, 8)) * 7) / 100
            End If

            'iRet = SBll.TradeInHeaderSave(oTSalesConn, oTSalesRs, Trim(sinventorycode), Trim$(sInvoiceNo), _
            '          gthisLocation, UCase(Trim(txtCustomerCode.Text)), Trim(txtMemberCode.Text), Trim(txtCustomerName.Text), Trim(txtAddress1.Text), _
            '          Trim(txtAddress2.Text), Trim(txtAddress3.Text), _
            '          Val(txtCostPerGram.Text), Val(fnGetdgvText(grdTrade, grdTrade.RowCount - 1, 3)), _
            '          Val(fnGetdgvText(grdTrade, grdTrade.RowCount - 1, 10)), _
            '          Val(fnGetdgvText(grdTrade, grdTrade.RowCount - 1, 6)), Val(fnGetdgvText(grdTrade, grdTrade.RowCount - 1, 7)), _
            '          Val(fnGetdgvText(grdTrade, grdTrade.RowCount - 1, 4)), Val(fnGetdgvText(grdTrade, grdTrade.RowCount - 1, 8)), _
            '          Val(dPaidAmount), "TRADEIN", "N", sUserID, sError)
            'If Trim(sError) <> "" Then
            '    MsgBox(sError, vbCritical)
            '    fncSaveTradeIn = False
            '    Exit Function
            'End If

            'For i = 1 To grdTrade.RowCount - 2
            '    iRet = SBll.TradeInDetailSave(oTSalesConn, oTSalesRs, gthisLocation, Trim(sinventorycode), i, Trim(fnGetdgvText(grdTrade, i, 2)), _
            '            Trim(fnGetdgvText(grdTrade, i, 9)), Val(fnGetdgvText(grdTrade, i, 3)), Val(fnGetdgvText(grdTrade, i, 10)), _
            '            Val(fnGetdgvText(grdTrade, i, 5)), Val(fnGetdgvText(grdTrade, i, 6)), Val(fnGetdgvText(grdTrade, i, 7)), _
            '            Val(fnGetdgvText(grdTrade, i, 4)), Val(fnGetdgvText(grdTrade, i, 8)), "TRADEIN", "TRADEIN", "TRADEIN", "TRADEIN", "", sUserID, sError)
            '    If Trim(sError) <> "" Then
            '        MsgBox(sError, vbCritical)
            '        fncSaveTradeIn = False
            '        Exit Function
            '    End If
            'Next
            'If UCase(Trim(sCalledFrom)) = "POS" Then
            '    'save paymodedetail
            '    frmPm.PayModeDetailSave(sInvoiceNo, sInvoiceDate, "TradeIn", Val(GetText(grdTrade, grdTrade.MaxRows, 8)), 0, "")
            'End If
            If UCase(Trim(sCalledFrom)) = "ORDERFORM" Then
                dPaidAmount = Val(fnGetdgvText(grdTrade, grdTrade.RowCount - 1, 7))
            End If
            'If UCase(Trim(sCalledFrom)) = "POS" Then
            '    frmSalesInvoice.TradeInAmt(Val(GetText(grdTrade, grdTrade.MaxRows, 8)))
            'Else
            '    frmSalesInvoice.TradeInAmt(Val(GetText(grdTrade, grdTrade.MaxRows, 8)))
            'End If
            mdTradeInAmt = Val(fnGetdgvText(grdTrade, grdTrade.RowCount - 1, 7))

            If Trim(sError) <> "" Then
                MsgBox(sError, vbCritical)
                fncSaveTradeIn = False
                Exit Function
            Else
                fncSaveTradeIn = True
                Exit Function
            End If
        Else
            MsgBox(sError, vbCritical)
            fncSaveTradeIn = False
            Exit Function
        End If
        frmPm = Nothing

    End Function
    Private Sub subGetTradeInDetails()
        Dim i As Long
        Dim adoRS As DataSet
        Dim SBll As New clsTradeIN

        adoRS = SBll.GetTradeInList(sinventorycode)

        grdTrade.RowCount = 0
        For i = 1 To adoRS.Tables(0).Rows.Count
            grdTrade.Rows.Add()
            Call fnSetdgvText(grdTrade, i, 0, i)
            Call fnSetdgvText(grdTrade, i, 1, Trim(adoRS.Tables(0).Rows(i)("Description") & ""))
            Call fnSetdgvText(grdTrade, i, 2, Trim(adoRS.Tables(0).Rows(i)("Weight") & ""))
            Call fnSetdgvText(grdTrade, i, 3, Trim(adoRS.Tables(0).Rows(i)("GoldLoss") & ""))
            Call fnSetdgvText(grdTrade, i, 4, Format(Val(adoRS.Tables(0).Rows(i)("CostperGram") & ""), "###0.000"))
            Call fnSetdgvText(grdTrade, i, 5, Format(Val(adoRS.Tables(0).Rows(i)("Mc")), "###0.000"))
            Call fnSetdgvText(grdTrade, i, 6, Val(adoRS.Tables(0).Rows(i)("Pcs") & ""))
            Call fnSetdgvText(grdTrade, i, 7, Val(adoRS.Tables(0).Rows(i)("TotalCost") & ""))
            Call fnSetdgvText(grdTrade, i, 8, Trim(adoRS.Tables(0).Rows(i)("DiamondDescription") & ""))
            Call fnSetdgvText(grdTrade, i, 9, Val(adoRS.Tables(0).Rows(i)("StoneWeight") & ""))
        Next i





        subTotal()
        adoRS = Nothing
        SBll = Nothing


    End Sub
    Private Sub subClear()
        sinventorycode = ""
        txtCustomerCode.Text = ""
        txtCustomerName.Text = ""
        txtAddress1.Text = ""
        txtAddress2.Text = ""
        txtAddress3.Text = ""
        txtRemarks.Text = ""
        grdTrade.RowCount = 0
    End Sub

    Private Sub subAddTradeInInventory()
        Dim i As Long
        Dim r As Long
        grdTrade.ForeColor = System.Drawing.Color.Black
        'grdTrade.FontBold = False

        If Not bEdited Then
            If grdTrade.RowCount = 0 Then
                grdTrade.Rows.Add()
            Else
                'grdJobSheet.Row = grdJobSheet.MaxRows
                'grdJobSheet.Action = 5
                'grdJobSheet.MaxRows = grdJobSheet.MaxRows - 1
                'grdJobSheet.MaxRows = grdJobSheet.MaxRows + 1
            End If
            r = grdTrade.RowCount - 1
        Else
            r = grdTrade.CurrentRow.Index
        End If

        Call SetText(grdTrade, r, 1, r)
        Call SetText(grdTrade, r, 2, Trim(txtDescription.Text))
        Call SetText(grdTrade, r, 9, Trim(txtDiamondDescription.Text))
        Call SetText(grdTrade, r, 3, Val(txtWeight.Text))
        Call SetText(grdTrade, r, 4, Val(txtGoldLoss.Text))
        Call SetText(grdTrade, r, 5, Format(Val(txtCostPerGram1.Text), "###0.000"))
        Call SetText(grdTrade, r, 8, Val(txtTotalCost.Text))
        Call SetText(grdTrade, r, 10, Trim(txtStoneWeight.Text))


        subTotal()
        subClearDetail()

    End Sub
    Private Sub subTotal()

        'If Not bEdited Then
        '    grdTrade.MaxRows = grdTrade.MaxRows + 1
        'End If

        'Call SetText(grdTrade, grdTrade.MaxRows, 1, "Total:")

        'grdTrade.ForeColor = System.Drawing.Color.Red
        'grdTrade.FontBold = True

        'grdTrade.Col = 3
        'grdTrade.Formula = "sum(c1:c" & grdTrade.MaxRows - 1 & ")"


        'grdTrade.ForeColor = System.Drawing.Color.Red
        'grdTrade.FontBold = True

        'grdTrade.Col = 4
        'grdTrade.Formula = "sum(d1:d" & grdTrade.MaxRows - 1 & ")"

        'grdTrade.ForeColor = System.Drawing.Color.Red
        'grdTrade.FontBold = True

        'grdTrade.Col = 6
        'grdTrade.Formula = "sum(f1:f" & grdTrade.MaxRows - 1 & ")"

        'grdTrade.ForeColor = System.Drawing.Color.Red
        'grdTrade.FontBold = True

        'grdTrade.Col = 7
        'grdTrade.Formula = "sum(g1:g" & grdTrade.MaxRows - 1 & ")"

        'grdTrade.ForeColor = System.Drawing.Color.Red
        'grdTrade.FontBold = True

        'grdTrade.Col = 8
        ''grdTrade.CellType = FPSpread.CellTypeConstants.CellTypeFloat
        'grdTrade.Formula = "sum(h1:h" & grdTrade.MaxRows - 1 & ")"

        'grdTrade.ForeColor = System.Drawing.Color.Red
        'grdTrade.FontBold = True

        'grdTrade.Col = 10
        'grdTrade.Formula = "sum(j1:j" & grdTrade.MaxRows - 1 & ")"

        'grdTrade.ForeColor = System.Drawing.Color.Red
        'grdTrade.FontBold = True

        Try
            If Not bEdited Then
                grdTrade.Rows.Add()
            End If

            Call fnSetdgvText(grdTrade, grdTrade.RowCount - 1, 0, "TOTAL:")
            Call fnSetdgvText(grdTrade, grdTrade.RowCount - 1, 2, Format(dWeightGrd, "0.00"))
            Call fnSetdgvText(grdTrade, grdTrade.RowCount - 1, 3, Format(dGoldLossGrd, "0.00"))
            Call fnSetdgvText(grdTrade, grdTrade.RowCount - 1, 5, Format(dMCGrd, "0.00"))
            Call fnSetdgvText(grdTrade, grdTrade.RowCount - 1, 6, dPCSGrd)
            Call fnSetdgvText(grdTrade, grdTrade.RowCount - 1, 7, Format(dTotalCostGrd, "0.00"))

            Me.grdTrade.Rows(grdTrade.RowCount - 1).Cells(0).Style.ForeColor = Color.Red
            Me.grdTrade.Rows(grdTrade.RowCount - 1).Cells(0).Style.Font = New Font(grdTrade.Font.FontFamily, 9, FontStyle.Bold)

            Me.grdTrade.Rows(grdTrade.RowCount - 1).Cells(2).Style.ForeColor = Color.Red
            Me.grdTrade.Rows(grdTrade.RowCount - 1).Cells(2).Style.Font = New Font(grdTrade.Font.FontFamily, 9, FontStyle.Bold)

            Me.grdTrade.Rows(grdTrade.RowCount - 1).Cells(3).Style.ForeColor = Color.Red
            Me.grdTrade.Rows(grdTrade.RowCount - 1).Cells(3).Style.Font = New Font(grdTrade.Font.FontFamily, 9, FontStyle.Bold)

            Me.grdTrade.Rows(grdTrade.RowCount - 1).Cells(5).Style.ForeColor = Color.Red
            Me.grdTrade.Rows(grdTrade.RowCount - 1).Cells(5).Style.Font = New Font(grdTrade.Font.FontFamily, 9, FontStyle.Bold)

            Me.grdTrade.Rows(grdTrade.RowCount - 1).Cells(6).Style.ForeColor = Color.Red
            Me.grdTrade.Rows(grdTrade.RowCount - 1).Cells(6).Style.Font = New Font(grdTrade.Font.FontFamily, 9, FontStyle.Bold)

            Me.grdTrade.Rows(grdTrade.RowCount - 1).Cells(7).Style.ForeColor = Color.Red
            Me.grdTrade.Rows(grdTrade.RowCount - 1).Cells(7).Style.Font = New Font(grdTrade.Font.FontFamily, 9, FontStyle.Bold)

            lblTradeInAmount.Text = Format(Val(fnGetdgvText(grdTrade, grdTrade.RowCount - 1, 7)), "0.00")
            lblBalanceAmount.Text = Format(Val(lblSalesAmount.Text) - Val(lblTradeInAmount.Text), "0.00")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Function subGetTotalForGrid()
        'dWeightGrd = Format(dWeightGrd + Val(txtWeight.Text), "0.00")
        'dGoldLossGrd = Format(dGoldLossGrd + Val(txtGoldLoss.Text), "0.00")
        'dMCGrd = Format(dMCGrd + Val(0), "0.00")
        'dPCSGrd = Format(dPCSGrd + Val(0), "0.00")
        'dTotalCostGrd = Format(dTotalCostGrd + Val(txtTotalCost.Text), "0.00")


        Dim icnt
        Dim sInventoryCode As String
        dWeightGrd = 0
        dGoldLossGrd = 0
        dMCGrd = 0
        dPCSGrd = 0
        dTotalCostGrd = 0
        For icnt = 0 To grdTrade.RowCount - 1
            sInventoryCode = Trim(fnGetdgvText(grdTrade, icnt, 0))
            If sInventoryCode <> "TOTAL:" Then
                dWeightGrd = Format(Val(dWeightGrd) + Val(fnGetdgvText(grdTrade, icnt, 2)), "0.00")
                dGoldLossGrd = Format(Val(dGoldLossGrd) + Val(fnGetdgvText(grdTrade, icnt, 3)), "0.00")
                dMCGrd = Format(Val(dMCGrd) + Val(fnGetdgvText(grdTrade, icnt, 5)), "0.00")
                dPCSGrd = Format(Val(dPCSGrd) + Val(fnGetdgvText(grdTrade, icnt, 6)), "0.00")
                dTotalCostGrd = Format(Val(dTotalCostGrd) + Val(fnGetdgvText(grdTrade, icnt, 7)), "0.00")
            End If
        Next
    End Function
    Private Sub subClearDetail()
        On Error GoTo ErrorSub
        bEdited = False
        txtDescription.Text = ""
        txtDiamondDescription.Text = ""
        txtWeight.Text = ""
        txtStoneWeight.Text = ""
        txtGoldLoss.Text = ""
        sItemNo = 0
        txtTotalCost.Text = ""
        If Val(txtCostPerGram.Text) <> 0 Then
            txtCostPerGram1.Text = Val(txtCostPerGram.Text)
        End If
        Exit Sub
ErrorSub:
        Err.Clear()
    End Sub
    Private Function fncValidateTradeIn() As Boolean
        If UCase(sCompanyShortCode) = "MJ" Then
            If Trim(txtDescription.Text) = "" Then
                MsgBox("Description Cannot be Blank ....")
                txtDescription.Focus()
                fncValidateTradeIn = False
                Exit Function
            End If
        End If

        If UCase(sCompanyShortCode) = "MJ" Then
            If IsNumeric(txtWeight.Text) = False Then
                MsgBox("Weight is Invalid ....")
                fncValidateTradeIn = False
                txtWeight.Focus()
                Exit Function
            End If
        Else
            txtWeight.Text = 0
        End If
        If UCase(sCompanyShortCode) = "MJ" Then
            If Val(txtWeight.Text) <= 0 Then
                MsgBox("Weight is Invalid ....")
                fncValidateTradeIn = False
                txtWeight.Focus()
                Exit Function
            End If
        End If

        If Trim(txtStoneWeight.Text) <> "" Then
            If IsNumeric(txtStoneWeight.Text) = False Then
                MsgBox("Stone Weight is Invalid ....")
                fncValidateTradeIn = False
                txtStoneWeight.Focus()
                Exit Function
            End If

            If Val(txtStoneWeight.Text) < 0 Then
                MsgBox("Stone Weight is Invalid ....")
                fncValidateTradeIn = False
                txtStoneWeight.Focus()
                Exit Function
            End If
        End If
        If Val(txtGoldLoss.Text) < 0 Then
            MsgBox("Gold Loss is Invalid ....")
            fncValidateTradeIn = False
            txtGoldLoss.Focus()
            Exit Function
        End If

        If IsNumeric(txtCostPerGram1.Text) = False Then
            MsgBox("Cost/Gram is Invalid ....")
            fncValidateTradeIn = False
            txtCostPerGram1.Focus()
            Exit Function
        End If

        If Val(txtCostPerGram1.Text) <= 0 Then
            MsgBox("Cost Per Gram is Invalid ....")
            fncValidateTradeIn = False
            txtCostPerGram1.Focus()
            Exit Function
        End If

        If Val(txtTotalCost.Text) > Val(sSalesAmount) Then
            MsgBox("Trade In Amount Should not be Greater than Total Sales Amount")
            fncValidateTradeIn = False
            txtTotalCost.Focus()
            Exit Function
        End If

        fncValidateTradeIn = True

        Exit Function

    End Function
    Private Sub subGetAdminPassword()
        Try
            pnlCostAdminPwd.Visible = True
            pnlCostAdminPwd.BringToFront()
            txtCostAdminPassword.Clear()
            txtCostAdminPassword.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subAddGrid()
        Dim i As Long
        Dim r As Integer
        Dim dTotalCost As Double
        Dim dCost As Double
        Dim sClassType As String
        grdTrade.ForeColor = System.Drawing.Color.Black
        '  grdTrade.FontBold = False

        If optType875.Checked = True Then
            sClassType = optType875.Text
        ElseIf optType750.Checked = True Then
            sClassType = optType750.Text
        ElseIf optType835.Checked = True Then
            sClassType = optType835.Text
        ElseIf optType916.Checked = True Then
            sClassType = optType916.Text
        ElseIf optType18K.Checked = True Then
            sClassType = optType18K.Text
        ElseIf optType24K.Checked = True Then
            sClassType = optType24K.Text
        ElseIf optType22K.Checked = True Then
            sClassType = optType22K.Text
        ElseIf optType20K.Checked = True Then
            sClassType = optType20K.Text
        ElseIf optType21K.Checked = True Then
            sClassType = optType21K.Text
        End If

        'If Not bEdited Then
        '    If grdTrade.MaxRows = 0 Then
        '        grdTrade.MaxRows = grdTrade.MaxRows + 1
        '    Else
        '        grdTrade.Row = grdTrade.MaxRows
        '        grdTrade.Action = 5
        '        grdTrade.MaxRows = grdTrade.MaxRows - 1
        '        grdTrade.MaxRows = grdTrade.MaxRows + 1
        '    End If

        '    grdTrade.Row = grdTrade.MaxRows
        'Else
        '    grdTrade.Row = grdTrade.ActiveRow
        '    'grdTrade.Height() = GRDROWHT
        '    'grdTrade.RowHeight(grdTrade.ActiveRow) = GRDROWHT
        'End If

        If Not bEdited Then
            If grdTrade.RowCount = 0 Then
                grdTrade.Rows.Add()
            Else
                'grdJobSheet.Row = grdJobSheet.MaxRows
                'grdJobSheet.Action = 5
                'grdJobSheet.MaxRows = grdJobSheet.MaxRows - 1
                'grdJobSheet.MaxRows = grdJobSheet.MaxRows + 1
            End If
            r = grdTrade.RowCount - 1
        Else
            r = grdTrade.CurrentRow.Index
        End If



        Me.grdTrade.Rows(grdTrade.RowCount - 1).Cells(0).Style.ForeColor = Color.Black
        Me.grdTrade.Rows(grdTrade.RowCount - 1).Cells(0).Style.Font = New Font(grdTrade.Font.FontFamily, 10, FontStyle.Regular)

        Me.grdTrade.Rows(grdTrade.RowCount - 1).Cells(2).Style.ForeColor = Color.Black
        Me.grdTrade.Rows(grdTrade.RowCount - 1).Cells(2).Style.Font = New Font(grdTrade.Font.FontFamily, 10, FontStyle.Regular)

        Me.grdTrade.Rows(grdTrade.RowCount - 1).Cells(3).Style.ForeColor = Color.Black
        Me.grdTrade.Rows(grdTrade.RowCount - 1).Cells(3).Style.Font = New Font(grdTrade.Font.FontFamily, 10, FontStyle.Regular)

        Me.grdTrade.Rows(grdTrade.RowCount - 1).Cells(5).Style.ForeColor = Color.Black
        Me.grdTrade.Rows(grdTrade.RowCount - 1).Cells(5).Style.Font = New Font(grdTrade.Font.FontFamily, 10, FontStyle.Regular)

        Me.grdTrade.Rows(grdTrade.RowCount - 1).Cells(6).Style.ForeColor = Color.Black
        Me.grdTrade.Rows(grdTrade.RowCount - 1).Cells(6).Style.Font = New Font(grdTrade.Font.FontFamily, 10, FontStyle.Regular)

        Me.grdTrade.Rows(grdTrade.RowCount - 1).Cells(7).Style.ForeColor = Color.Black
        Me.grdTrade.Rows(grdTrade.RowCount - 1).Cells(7).Style.Font = New Font(grdTrade.Font.FontFamily, 10, FontStyle.Regular)



        'Call SetText(grdTrade, grdTrade.Row, 1, grdTrade.Row)
        'Call SetText(grdTrade, grdTrade.Row, 2, Trim(txtDescription.Text))
        'Call SetText(grdTrade, grdTrade.Row, 3, Val(txtWeight.Text))
        'Call SetText(grdTrade, grdTrade.Row, 4, Val(txtGoldLoss.Text))
        ''        Call SetText(grdTrade, grdTrade.Row, 5, Val(txtCostPerGram1.Text))
        'Dim sTotalCost As String
        'sTotalCost = Format(Val(txtTotalCost.Text), "#0.00")
        'dCost = Mid(sTotalCost, Len(sTotalCost) - 2, 3)
        'If dCost > 0 And dCost < 0.5 Then
        '    dTotalCost = Math.Round(Val(txtTotalCost.Text)) - 1
        'ElseIf dCost >= 0.5 And dCost <= 0.99 Then
        '    dTotalCost = Math.Round(Val(txtTotalCost.Text))
        'Else
        '    dTotalCost = Val(txtTotalCost.Text)
        'End If

        'grdTrade.Col = 8
        ''grdTrade.CellType = FPSpread.CellTypeConstants.CellTypeFloat
        'Call SetText(grdTrade, grdTrade.Row, 8, Val(dTotalCost))
        'Call SetText(grdTrade, grdTrade.Row, 5, Val(dTotalCost) / (Val(txtWeight.Text) + Val(txtGoldLoss.Text)))
        'Call SetText(grdTrade, grdTrade.Row, 9, Trim(txtDiamondDescription.Text))
        'Call SetText(grdTrade, grdTrade.Row, 10, Val(txtStoneWeight.Text))
        ' ''HAJ Add Class code to Grid on 29 jan 2008
        ''  Call SetText(grdTrade, grdTrade.Row, dClass, IIf(optType22k.Checked = True, optType22k.Text, optType24k.Text))
        'Call SetText(grdTrade, grdTrade.Row, dClass, sClassType)


        Call fnSetdgvText(grdTrade, r, 0, r + 1)
        Call fnSetdgvText(grdTrade, r, 1, Trim(txtDescription.Text))
        Call fnSetdgvText(grdTrade, r, 2, Trim(txtWeight.Text))
        Call fnSetdgvText(grdTrade, r, 3, Format(Val(txtGoldLoss.Text), "#0.00"))
        Dim sTotalCost As String
        sTotalCost = Format(Val(txtTotalCost.Text), "#0.00")
        dCost = Mid(sTotalCost, Len(sTotalCost) - 2, 3)
        If dCost > 0 And dCost < 0.5 Then
            dTotalCost = Math.Round(Val(txtTotalCost.Text)) - 1
        ElseIf dCost >= 0.5 And dCost <= 0.99 Then
            dTotalCost = Math.Round(Val(txtTotalCost.Text))
        Else
            dTotalCost = Val(txtTotalCost.Text)
        End If
        'Shabeer20150212
        ' Call fnSetdgvText(grdTrade, r, 4, Format(Val(dTotalCost) / (Val(txtWeight.Text) + Val(txtGoldLoss.Text)), "#0.00"))
        Call fnSetdgvText(grdTrade, r, 4, Format(Val(dTotalCost) / (Val(txtWeight.Text)), "#0.00"))
        'Call fnSetdgvText(grdTrade, r, 5, Format(Val(txtCostPerGram1.Text), "#0.00"))
        Call fnSetdgvText(grdTrade, r, 8, Trim(txtDiamondDescription.Text))
        Call fnSetdgvText(grdTrade, r, 9, Val(txtStoneWeight.Text))
        Call fnSetdgvText(grdTrade, r, 7, Format(Val(dTotalCost), "#0.00"))
        Call fnSetdgvText(grdTrade, r, 10, sClassType)
        subGetTotalForGrid()
        subTotal()
        subClearDetail()
    End Sub

    Private Sub subGetMember()
        On Error GoTo ErrorSub
        Dim adoRS As DataSet
        adoRS = objMember.GetMemberwithCode(Trim(txtMemberCode.Text))
        If adoRS.Tables(0).Rows.Count > 0 Then
            txtCustomerName.Text = Trim(adoRS.Tables(0).Rows(0)("CustomerName") & "")
            txtAddress1.Text = Trim(adoRS.Tables(0).Rows(0)("Address1") & "")
            txtAddress2.Text = Trim(adoRS.Tables(0).Rows(0)("Address2") & "")
            txtAddress3.Text = Trim(adoRS.Tables(0).Rows(0)("Address3") & "")
        End If
        adoRS = Nothing
        Exit Sub
ErrorSub:
        Err.Clear()
    End Sub
    Private Sub subGetCustomer()
        On Error GoTo ErrorSub
        Dim i As Long
        Dim adoRS As DataSet
        Dim SBll = New clsCustomer
        adoRS = SBll.GetCustomer(Trim(txtCustomerCode.Text))
        If adoRS.Tables(0).Rows.Count > 0 Then
            txtCustomerName.Text = Trim(adoRS.Tables(0).Rows(0)("CustomerName") & "")
            txtAddress1.Text = Trim(adoRS.Tables(0).Rows(0)("Address1") & "")
            txtAddress2.Text = Trim(adoRS.Tables(0).Rows(0)("Address2") & "")
            txtAddress3.Text = Trim(adoRS.Tables(0).Rows(0)("Address3") & "")
        End If
        adoRS = Nothing
        SBll = Nothing
        Exit Sub
ErrorSub:
        Err.Clear()
    End Sub

    Private Sub subGetCostPerGram()
        Dim adoRS As DataSet
        Dim ssql As String
        On Error GoTo ErrHandler
        If sCompanyShortCode = "ES" Then
            txtLossPerc.Text = 18
        End If
        ssql = "Select Rate from tblClass where ClassCode like '22%'"
        'GetRecordset(gsConn, ssql, adoRS)
        adoRS = dm.getDataSetWithSqlAdapter(ssql, "Table", "Table")
        If adoRS.Tables(0).Rows.Count = 0 Then
            ssql = "Select Rate from tblClass where ClassCode like '18K%'"
            'GetRecordset(gsConn, ssql, adoRS)
            adoRS = dm.getDataSetWithSqlAdapter(ssql, "Table", "Table")
            If adoRS.Tables(0).Rows.Count = 0 Then
                ssql = "Select Rate from tblClass"
                'GetRecordset(gsConn, ssql, adoRS)
                adoRS = dm.getDataSetWithSqlAdapter(ssql, "Table", "Table")
                If adoRS.Tables(0).Rows.Count > 0 Then
                    txtCostPerGram.Text = Val(adoRS.Tables(0).Rows(0)("Rate") & "")
                    txtCostPerGram1.Text = Val(txtCostPerGram.Text) - (Val(txtCostPerGram.Text) * Val(txtLossPerc.Text) / 100)
                End If
            Else
                txtCostPerGram.Text = Val(adoRS.Tables(0).Rows(0)("Rate") & "")
                txtCostPerGram1.Text = Val(txtCostPerGram.Text) - (Val(txtCostPerGram.Text) * Val(txtLossPerc.Text) / 100)
            End If
        Else
            txtCostPerGram.Text = Val(adoRS.Tables(0).Rows(0)("Rate") & "")
            txtCostPerGram1.Text = Val(txtCostPerGram.Text) - (Val(txtCostPerGram.Text) * Val(txtLossPerc.Text) / 100)
        End If
        Exit Sub
ErrHandler:
        MsgBox(Err.Description)
        Err.Clear()
    End Sub
    Private Sub subGetClassCostPerGram(ByVal sClasssCode As String)
        Dim adoRS As DataSet
        Dim ssql As String
        On Error GoTo ErrHandler

        ssql = "Select Rate from tblClass where classcode= " & fncFormatSQLText(sClasssCode)
        adoRS = dm.getDataSetWithSqlAdapter(ssql, "Table", "Table")
        If adoRS.Tables(0).Rows.Count > 0 Then
            dCostPerGram = Val(adoRS.Tables(0).Rows(0)("Rate") & "")
        End If
        Exit Sub
ErrHandler:
        MsgBox(Err.Description)
        Err.Clear()
    End Sub


    Public Function GetDetailsFromGrid()
        sItemNo = grdTrade.CurrentRow.Index
        txtDescription.Text = fnGetdgvText(grdTrade, grdTrade.CurrentRow.Index, 1)
        txtDiamondDescription.Text = fnGetdgvText(grdTrade, grdTrade.CurrentRow.Index, 8)

        txtWeight.Text = fnGetdgvText(grdTrade, grdTrade.CurrentRow.Index, 2)
        txtStoneWeight.Text = fnGetdgvText(grdTrade, grdTrade.CurrentRow.Index, 9)

        txtGoldLoss.Text = fnGetdgvText(grdTrade, grdTrade.CurrentRow.Index, 3)
        txtCostPerGram1.Text = fnGetdgvText(grdTrade, grdTrade.CurrentRow.Index, 4)

        txtTotalCost.Text = fnGetdgvText(grdTrade, grdTrade.CurrentRow.Index, 7)
    End Function
    '''Private Sub txtCustomerCode_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustomerCode.Leave

    '''    Try
    '''        If UCase(txtCustomerCode.Text.Trim) = "CASH" And sMemberModule = "1" Then
    '''            subGetMember()
    '''        ElseIf UCase(txtCustomerCode.Text.Trim) = "CASH" And sMemberModule = "0" Then
    '''            MessageBox.Show("TradeIn Sales Not For CASH Customer!!!")
    '''            Me.Close()
    '''        Else
    '''            sCustomerCode = Trim(txtCustomerCode.Text)
    '''            subGetCustomer()
    '''        End If

    '''    Catch ex As Exception
    '''        MsgBox(ex.Message)

    '''    End Try

    '''End Sub

    Private Sub txtWeight_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWeight.Leave
        If Val(txtWeight.Text) = 0 Then
            ' txtWeight.SetFocus
            Exit Sub
        Else
            'Shabeer20150212
            '  dCost = (Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram1.Text) '+ Val(txtMC)
            dCost = (Val(txtWeight.Text)) * Val(txtCostPerGram1.Text) '+ Val(txtMC)
            ''HAJ 
            If opt24K.Checked = True Then
                dTotAmount = dCost * 1.05
                txtTotalCost.Text = Format(dTotAmount, "0.00")
            Else
                txtTotalCost.Text = Format(dCost, "0.00")
            End If
        End If
    End Sub

    Private Sub txtLossPerc_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLossPerc.TextChanged
        If UCase(sCompanyShortCode) = "SSJ" Then
            txtLossPerc.Text = 0
        Else
            txtLossPerc.Text = 0
            'If Val(txtLossPerc.Text) And Val(txtLossPerc.Text) < 100 Then
            '    txtCostPerGram1.Text = Val(txtCostPerGram.Text) - (Val(txtCostPerGram.Text) * Val(txtLossPerc.Text) / 100)
            'Else
            '    MsgBox("Percentage is more than 100 % !!!!", MsgBoxStyle.Exclamation)
            '    txtLossPerc.Clear()
            '    txtLossPerc.Focus()
            '    Exit Sub
            'End If
        End If
    End Sub

    Private Sub txtGoldLoss_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGoldLoss.Leave
        'If Val(txtGoldLoss.Text) <> 0 Then
        '    'Shabeer20150212
        '    ' dCost = (Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram1.Text) '+ Val(txtMC)
        '    dCost = (Val(txtWeight.Text)) * Val(txtCostPerGram1.Text) '+ Val(txtMC)
        '    If opt24K.Checked = True Then
        '        dTotAmount = dCost * 1.05
        '        txtTotalCost.Text = Format(dTotAmount, "0.00")
        '    Else
        '        txtTotalCost.Text = Format(dCost, "0.00")
        '    End If
        'End If
    End Sub

    Private Sub txtCostPerGram1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCostPerGram1.Leave
        Dim dTotAmount As Double
        dTotAmount = 0
        If Val(txtCostPerGram1.Text) <> 0 Then
            'Shabeer20150212
            ' dCost = (Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram1.Text) '+ Val(txtMC)
            dCost = (Val(txtWeight.Text)) * Val(txtCostPerGram1.Text) '+ Val(txtMC)
            ''HAJ 24K changes
            If opt24K.Checked = True Then
                dTotAmount = dCost * 1.05
                txtTotalCost.Text = Format(dTotAmount, "0.00")
            Else
                txtTotalCost.Text = Format(dCost, "0.00")
            End If

        End If
    End Sub

    Private Sub txtCostPerGram_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCostPerGram.Leave
        If Val(txtCostPerGram.Text) <> 0 Then
            txtCostPerGram1.Text = Val(txtCostPerGram.Text)
        End If
    End Sub

    Private Sub frmTradeInGold_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        Dim frmSalesInvoice As New frmGPOS

        If Not mbTransaction Then
            If oTSalesConn.State = 1 Then oTSalesConn.RollbackTrans()
            'If oTSalesConn.State = 1 Then TransactionRollBack(oTSalesConn)
        End If
        If UCase(Trim(sCalledFrom)) = "POS" Then
            If Len(frmSalesInvoice.txtCustomerName.Text) = 0 And Len(txtCustomerName.Text) <> 0 Then
                frmSalesInvoice.txtCustomerName.Text = Trim(txtCustomerName.Text)
            End If
            If Len(frmSalesInvoice.txtAddress1.Text) = 0 And Len(txtAddress1.Text) <> 0 Then
                frmSalesInvoice.txtAddress1.Text = Trim(txtAddress1.Text)
            End If
            If Len(frmSalesInvoice.txtAddress2.Text) = 0 And Len(txtAddress2.Text) <> 0 Then
                frmSalesInvoice.txtAddress2.Text = Trim(txtAddress2.Text)
            End If
            If Len(frmSalesInvoice.txtAddress3.Text) = 0 And Len(txtAddress3.Text) <> 0 Then
                frmSalesInvoice.txtAddress3.Text = Trim(txtAddress3.Text)
            End If
        End If
        sCalledFrom = ""
    End Sub

    Private Sub frmTradeInGold_HelpButtonClicked(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.HelpButtonClicked

    End Sub

    Private Sub frmTradeInGold_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim frmSalesInvoice As New frmGPOS
        dNegBalance = False
        grdTrade.RowCount = 0
        sTradeInRefNo = ""
        dTradeinAmount = 0.0
        ChkTradeIn = False
        lblTradeInLimit.Text = Format(dTradeinLimit, "0.00")
        '  subGridSetting(grdTrade)
        bEdited = False
        '''If UCase(Trim(sCalledFrom)) <> "ORDERFORM" Then
        '''    txtCustomerCode.Text = Trim(sCustomerCode)
        '''    txtCustomerCode.Focus()
        '''End If
        If sCalledFrom <> "POS" Then
            txtInvoiceNo.Text = Trim(sInvoiceNo)
            sinventorycode = "TI-" & Trim(sInvoiceNo)
        End If
        lblSalesAmount.Text = Format(Val(sSalesAmount), "0.00")
        If UCase(Trim(sCalledFrom)) = "POS" Then
            subGetCostPerGram()
        ElseIf UCase(Trim(sCalledFrom)) = "ORDERFORM" Then
            txtLossPerc.Focus()
        End If
        subGetCostPerGram()
        If Len(txtCustomerCode.Text) > 0 Then
            If UCase(txtCustomerCode.Text.Trim) = "CASH" And sMemberModule = "1" Then
                subGetMember()
            ElseIf UCase(txtCustomerCode.Text.Trim) = "CASH" And sMemberModule = "0" And UCase(sCompanyShortCode) <> "GMT" Then
                MessageBox.Show("TradeIn Sales Not For CASH Customer!!!")
                Me.Close()
            Else
                sCustomerCode = Trim(txtCustomerCode.Text)
                subGetCustomer()
            End If
        End If
        ''dm.SetPosPinters("BILL")
        '' HAJ Hide the option Buttons on  24 oct 2007
        If UCase(sCompanyShortCode) = "AS" Then
            lblClass.Visible = True
            opt22K.Visible = True
            opt24K.Visible = True
        Else
            lblClass.Visible = False
            opt22K.Visible = False
            opt24K.Visible = False
        End If

        If UCase(sCompanyShortCode) = "MJ" Then
            txtLossPerc.Text = "10"
            txtLossPerc.ReadOnly = True
        End If
        If UCase(sCompanyShortCode) = "MD" Then
            txtLossPerc.Visible = True
            Label3.Visible = True
            txtLossPerc.Text = "3"
        End If

        subSetDataTable()

    End Sub

    Private Sub btnFClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFClear.Click
        subClearDetail()
    End Sub

    Private Sub btnAdd_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.GotFocus
        If UCase(sCompanyShortCode) = "MJ" Then
            'Shabeer20150212
            ' txtTotalCost.Text = (Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram1.Text) '+ Val(txtMC)
            txtTotalCost.Text = (Val(txtWeight.Text)) * Val(txtCostPerGram1.Text) '+ Val(txtMC)
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        mbSalesTransaction = False
        Me.Close()
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Dim frmPayingmode As New frmPayingmode
        Dim frmSalesInvoice As New frmGPOS
        Dim objOrder As New frmInstallment_AJ
        bTradeinFlag = True
        mbSalesTransaction = False
        If fncValidate() Then
            If sCalledFrom = "POS" Then
                dTradeinBalance = Val(lblBalanceAmount.Text)
                sSalesType = "TradeIn"
                nTotalNet = Format(Val(lblBalanceAmount.Text), "0.00")
                fncAddTradeInSalesInPOS()
                Me.Close()
                'frmPayingmode.ShowDialog()
                'If gPayCancel = False Then
                '    'If Not fncSaveTradeInSales() Then
                '    '    MsgBox("Transaction not saved")
                '    '    mbSalesTransaction = False
                '    'Else
                '    '    mbSalesTransaction = True
                '    'End If
                '    'Me.Close()
                'ElseIf gPayCancel = True Thenduraiups
                '    'Exit Sub
                'End If
            ElseIf sCalledFrom = "ORDERFORM" Then
                sSalesType = "TradeIn"
                If Not fncSaveTradeIn() Then
                    MsgBox("Transaction not saved")
                    oTSalesConn.RollbackTrans()
                Else
                    mbTransaction = True
                End If
                If UCase(Trim(sCalledFrom)) = "POS" Then
                    frmSalesInvoice.lblReturnAmount.Text = 0
                ElseIf UCase(Trim(sCalledFrom)) = "ORDERFORM" Then
                    dTradeinAmount = dPaidAmount
                    sTradeInRefNo = sinventorycode
                    ChkTradeIn = True
                End If
                fncAddTradeInSalesInPOS()
                Me.Close()
            End If
        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If UCase(sCompanyShortCode) = "SSJ" Then
            txtLossPerc.Text = 0
            If fncValidateTradeIn() Then
                subAddGrid()
                txtDescription.Focus()
            End If

        Else
            'If Val(txtLossPerc.Text) = 0 Then
            '    MsgBox("Loss Percentage Cannot Be Zero.", MsgBoxStyle.Information)
            '    If txtLossPerc.Enabled And txtLossPerc.Visible Then txtLossPerc.Focus()
            '    Exit Sub
            'End If
            Dim sClass As String
            If Trim(sCompanyShortCode) = "AJ" Then
                If optType22K.Checked Then
                    sClass = "916"
                ElseIf optType750.Checked Then
                    sClass = "916"
                ElseIf optType835.Checked Then
                    sClass = "916"
                ElseIf optType875.Checked Then
                    sClass = "916"
                ElseIf optType916.Checked Then
                    sClass = "916"
                ElseIf optType18K.Checked Then
                    sClass = "999"
                ElseIf optType24K.Checked Then
                    sClass = "999.9"
                End If
            Else
                If optType22K.Checked Then
                    sClass = "22K"
                ElseIf optType750.Checked Then
                    sClass = "22K-916"
                ElseIf optType835.Checked Then
                    sClass = "22K-916"
                ElseIf optType875.Checked Then
                    sClass = "22K-916"
                ElseIf optType916.Checked Then
                    sClass = "22K-916"
                ElseIf optType18K.Checked Then
                    sClass = "18K"
                ElseIf optType24K.Checked Then
                    sClass = "24K"
                ElseIf optType20K.Checked Then
                    sClass = "22K"
                ElseIf optType21K.Checked Then
                    sClass = "22K"
                End If
            End If
            subGetClassCostPerGram(sClass)


            txtLossPerc.Text = 0
            If fncValidateTradeIn() Then
                If Val(txtCostPerGram1.Text) > Val(dCostPerGram) Then
                    MessageBox.Show("Cost PerGram more than market value. Enter Super Admin Password to Continue")
                    subGetAdminPassword()
                    Exit Sub
                Else
                    subAddGrid()
                    bEdited = False
                    txtDescription.Focus()
                End If

            End If
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        subClearDetail()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        'If bEdited Then
        '    grdTrade.Action = FPSpread.ActionConstants.ActionDeleteRow
        '    grdTrade.MaxRows = grdTrade.MaxRows - 1
        '    If grdTrade.MaxRows = 1 Then
        '        grdTrade.MaxRows = 0
        '    Else
        '        subTotal()
        '    End If
        '    subClearDetail()
        'End If
        grdTrade.Rows.RemoveAt(grdTrade.CurrentRow.Index)
        If grdTrade.RowCount = 1 Then
            grdTrade.RowCount = 0
        Else
            subTotal()
            subClearDetail()
        End If

    End Sub

    Private Sub frmTradeInGold_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F6
                btnFClear_Click(sender, e)
            Case Keys.F7
                btnUpdate_Click(sender, e)
            Case Keys.Escape
                btnClose_Click(sender, e)
            Case Keys.Return
                SendKeys.Send("{Tab}")
            Case Keys.Down
                SendKeys.Send("{Tab}")
            Case Keys.Up
                SendKeys.Send("+{Tab}")
        End Select

    End Sub

    Private Sub btnMemberCodeSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim frmHelpCustomer As New frmHelpSearch(0, 0, "tblMember", "L", 4, "MemberName", "", "MemberCode", "MemberCode", 80, "MemberName", "MemberName", 150, "Address1", "Address1", 90, "Address2", "Address2", 80)
            frmHelpCustomer.ShowDialog()
            txtMemberCode.Text = frmHelpCustomer.Var1.Trim
            txtCustomerName.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub txtTotalCost_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotalCost.Leave
        Dim dCostPerGram1 As Double
        If UCase(sCompanyShortCode) <> "MD" Then
            If Val(txtTotalCost.Text) <> 0 Then
                dCost = Val(txtTotalCost.Text)
                'Shabeer 20150212
                'dCostPerGram = dCost / (Val(txtWeight.Text) + Val(txtGoldLoss.Text))
                dCostPerGram1 = dCost / (Val(txtWeight.Text))
                txtCostPerGram1.Text = dCostPerGram1
                txtTotalCost.Text = Format(dCost, "0.00")
            End If
        End If
    End Sub

    Private Function fncSaveTradeInSales() As Boolean
        Dim objTI As New clsTradeIN
        Dim iCnt As Long
        Dim sSql As String
        Dim bStatus As Boolean
        Dim sError As String
        Dim i As Long
        objTI.subOpenTIConnection()
        Try
            bStatus = False
            sInvoiceNo = objTI.fncGetNextTrandeInRefNo()
            txtInvoiceNo.Text = sInvoiceNo
            sinventorycode = "TI-" & sInvoiceNo
            bStatus = objTI.fncUpdateNextTradeInRefNo()
            If Not bStatus Then
                objTI.subRollBackTITransaction()
                Exit Function
            End If
            bStatus = objTI.TradeInDeleteSales(Trim(sinventorycode))
            If Not bStatus Then
                objTI.subRollBackTITransaction()
                Exit Function
            End If


            bStatus = objTI.TradeInHeaderSaveSales(Trim(sinventorycode), Trim$(sInvoiceNo), _
                      gthisLocation, UCase(Trim(txtCustomerCode.Text)), Trim(txtMemberCode.Text), Trim(txtCustomerName.Text), Trim(txtAddress1.Text), _
                      Trim(txtAddress2.Text), Trim(txtAddress3.Text), _
                      Val(txtCostPerGram.Text), Val(GetText(grdTrade, grdTrade.RowCount - 1, 3)), _
                      Val(GetText(grdTrade, grdTrade.RowCount - 1, 10)), _
                      Val(GetText(grdTrade, grdTrade.RowCount - 1, 6)), Val(GetText(grdTrade, grdTrade.RowCount - 1, 7)), _
                      Val(GetText(grdTrade, grdTrade.RowCount - 1, 4)), Val(GetText(grdTrade, grdTrade.RowCount - 1, 8)), _
                      Val(dPaidAmount), "TRADEIN", "N", sUserID)
            If Not bStatus Then
                objTI.subRollBackTITransaction()
                Exit Function
            End If

            For i = 1 To grdTrade.RowCount - 2
                bStatus = objTI.TradeInDetailSaveSales(gthisLocation, Trim(sinventorycode), i, Trim(fnGetdgvText(grdTrade, i, 2)), _
                        Trim(fnGetdgvText(grdTrade, i, 9)), Val(fnGetdgvText(grdTrade, i, 3)), Val(fnGetdgvText(grdTrade, i, 10)), _
                        Val(fnGetdgvText(grdTrade, i, 5)), Val(fnGetdgvText(grdTrade, i, 6)), Val(fnGetdgvText(grdTrade, i, 7)), _
                        Val(fnGetdgvText(grdTrade, i, 4)), Val(fnGetdgvText(grdTrade, i, 8)), "TRADEIN", "TRADEIN", "TRADEIN", "TRADEIN", "", Trim(sInvoiceNo), sUserID)
                If Not bStatus Then
                    objTI.subRollBackTITransaction()
                    Exit Function
                End If
            Next
            'End If
            mdTradeInAmt = Val(GetText(grdTrade, grdTrade.RowCount - 1, 8))
            msTradeInRefNo = sInvoiceNo
        Catch ex As Exception
            objTI.subRollBackTITransaction()
            MessageBox.Show(ex.Message)
        Finally
            objTI.subCloseTIConnection()
            fncSaveTradeInSales = bStatus
        End Try
    End Function

    Private Function fncAddTradeInSalesInPOS() As Boolean
        Dim iCnt As Long
        Dim bStatus As Boolean
        Dim ItemNo, sDesc, sWeight, sGoldLoss, sCostPerGram, sPMC, sPCS, sTotalCost, sStoneDesc, sStoneWeight, sClass
        Dim dr As DataRow
        Try
            bStatus = False
            For iCnt = 0 To grdTrade.RowCount - 2
                ItemNo = Trim(fnGetdgvText(grdTrade, iCnt, 0))
                sDesc = Trim(fnGetdgvText(grdTrade, iCnt, 1))
                sWeight = Trim(fnGetdgvText(grdTrade, iCnt, 2))
                sGoldLoss = Trim(fnGetdgvText(grdTrade, iCnt, 3))
                sCostPerGram = Trim(fnGetdgvText(grdTrade, iCnt, 4))
                sPMC = Trim(fnGetdgvText(grdTrade, iCnt, 5))
                'sPCS = Trim(GetText(grdTrade, iCnt, 7))
                sPCS = "1"
                sTotalCost = Trim(fnGetdgvText(grdTrade, iCnt, 7))
                sStoneDesc = Trim(fnGetdgvText(grdTrade, iCnt, 8))
                sStoneWeight = Trim(fnGetdgvText(grdTrade, iCnt, 9))
                sClass = Trim(fnGetdgvText(grdTrade, iCnt, 10))

                dr = dsTradeIn.Tables(0).NewRow()
                dr.Item("ItemNo") = Trim(ItemNo)
                dr.Item("sDesc") = Trim(sDesc)
                dr.Item("sWeight") = Trim(sWeight)
                dr.Item("sGoldLoss") = Trim(sGoldLoss)
                dr.Item("sCOstPerGram") = Trim(sCostPerGram)
                dr.Item("sPMC") = Trim(sPMC)
                dr.Item("sPCS") = Trim(sPCS)
                dr.Item("sTotalCost") = Trim(sTotalCost)
                dr.Item("sStoneDesc") = Trim(sStoneDesc)
                dr.Item("sStoneWeight") = Trim(sStoneWeight)
                dr.Item("sClass") = Trim(sClass)
                dsTradeIn.Tables(0).Rows.Add(dr)
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncAddTradeInSalesInPOS = bStatus
        End Try
    End Function
    Private Sub subSetDataTable()
        Dim dt As New DataTable
        dt = New DataTable
        dt.Columns.Add(New DataColumn("ItemNo", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("sDesc", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("sWeight", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("sGoldLoss", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("sCostPerGram", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("sPMC", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("sPCS", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("sTotalCost", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("sStoneDesc", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("sStoneWeight", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("sClass", Type.GetType("System.String")))
        dsTradeIn.Tables.Add(dt)
    End Sub

    Private Sub opt22K_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles opt22K.Click
        Call txtCostPerGram1_Leave(sender, e)
    End Sub

    Private Sub opt24K_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles opt24K.Click
        Call txtCostPerGram1_Leave(sender, e)
    End Sub

    Private Sub frmTradeInGold_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        If UCase(sCompanyShortCode) = "MJ" Then
            If txtLossPerc.Enabled And txtLossPerc.Visible Then txtLossPerc.Focus()
        Else
            If txtTotalCost.Enabled And txtTotalCost.Visible Then txtTotalCost.Focus()
        End If
    End Sub



    Private Sub grdTrade_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdTrade.DoubleClick
        If grdTrade.CurrentRow.Index = grdTrade.RowCount - 1 Then
            'do nothing
        Else
            bEdited = True
            GetDetailsFromGrid()
        End If
    End Sub

    Private Sub optType835_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optType835.CheckedChanged
        If optType835.Checked = True Then
            txtDescription.Text = "835 ITEMS"
        Else
            txtDescription.Text = ""
        End If
    End Sub

    Private Sub optType875_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optType875.CheckedChanged
        If optType875.Checked = True Then
            txtDescription.Text = "875 ITEMS"
        Else
            txtDescription.Text = ""
        End If
    End Sub

    Private Sub optType916_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optType916.CheckedChanged
        If optType916.Checked = True Then
            txtDescription.Text = "916 ITEMS"
        Else
            txtDescription.Text = ""
        End If
    End Sub

    'Private Sub optType999_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optType18K.CheckedChanged
    '    If optTyp.Checked = True Then
    '        txtDescription.Text = "999 ITEMS"
    '    Else
    '        txtDescription.Text = ""
    '    End If
    'End Sub

    Private Sub optType9999_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optType24K.CheckedChanged
        If optType24K.Checked = True Then
            txtDescription.Text = "999.9 ITEMS"
        Else
            txtDescription.Text = ""
        End If
    End Sub

    Private Sub optType750_CheckedChanged(sender As Object, e As System.EventArgs) Handles optType750.CheckedChanged
        If optType750.Checked = True Then
            txtDescription.Text = "750 ITEMS"
        Else
            txtDescription.Text = ""
        End If
    End Sub

    Private Sub optType22K_CheckedChanged(sender As Object, e As System.EventArgs) Handles optType22K.CheckedChanged
        If optType22K.Checked = True Then
            txtDescription.Text = "22K ITEMS"
        Else
            txtDescription.Text = ""
        End If
    End Sub

    Private Sub optType18K_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optType22K.CheckedChanged
        If optType18K.Checked = True Then
            txtDescription.Text = "18K ITEMS"
        Else
            txtDescription.Text = ""
        End If
    End Sub

    Private Sub optType20K_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optType22K.CheckedChanged
        If optType20K.Checked = True Then
            txtDescription.Text = "20K ITEMS"
        Else
            txtDescription.Text = ""
        End If
    End Sub

    Private Sub optType21K_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optType22K.CheckedChanged
        If optType21K.Checked = True Then
            txtDescription.Text = "21K ITEMS"
        Else
            txtDescription.Text = ""
        End If
    End Sub

    Private Sub btnCostAdminPwdOK_Click(sender As Object, e As System.EventArgs) Handles btnCostAdminPwdOK.Click
        Dim dsPWD As New DataSet
        Dim dsIns As New DataSet
        Dim objSM As New clsSalesMan

        Try
            If Trim(txtCostAdminPassword.Text) <> "" Then
                dsPWD = objSM.GetSalesManwithPassword(txtCostAdminPassword.Text)
                If dsPWD.Tables(0).Rows.Count > 0 Then
                    If Trim(UCase(dsPWD.Tables(0).Rows(0)("SalesManGroup") & "")) = "SUPER ADMIN" Then
                        pnlCostAdminPwd.Visible = False
                        subAddGrid()
                        bEdited = False
                        txtDescription.Focus()
                    Else
                        'MessageBox.Show("Please Check the Password")
                        MsgBox("SalesMan Has No Rights to do Change Cost!", MsgBoxStyle.Critical)
                        txtCostAdminPassword.Text = ""
                        If (txtCostAdminPassword.Enabled And txtCostAdminPassword.Visible) Then txtCostAdminPassword.Focus()

                        Exit Sub
                    End If
                Else
                    ' MessageBox.Show("Please Check the Password")
                    MsgBox("SalesMan Has No Rights to do Find Cost!", MsgBoxStyle.Critical)
                    txtCostAdminPassword.Text = ""
                    If (txtCostAdminPassword.Enabled And txtCostAdminPassword.Visible) Then txtCostAdminPassword.Focus()

                    Exit Sub
                End If
            Else
                MessageBox.Show("Enter the Password")
                txtCostAdminPassword.Focus()

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnCostAdminPwdCancel_Click(sender As Object, e As System.EventArgs) Handles btnCostAdminPwdCancel.Click
        pnlCostAdminPwd.Visible = False
        txtCostAdminPassword.Focus()
    End Sub

End Class
