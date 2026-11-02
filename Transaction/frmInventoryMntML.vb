Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmInventoryMntML
    Inherits System.Windows.Forms.Form
    Dim objClsInventory As New clsInventory
    Dim oTconn As ADODB.Connection
    Dim oTds As DataSet
    Dim sPMCType As String
    Dim sSMCType As String
    Dim sOldValue As String
    Dim sNewValue As String
    Dim masPriceCode(9) As String

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
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents line As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdClear As System.Windows.Forms.Button
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdUpdate As System.Windows.Forms.Button
    Friend WithEvents txtDescription1 As System.Windows.Forms.TextBox
    Friend WithEvents txtSupplierCode As System.Windows.Forms.TextBox
    Friend WithEvents txtCostPerGram1 As System.Windows.Forms.TextBox
    Friend WithEvents txtPMC1 As System.Windows.Forms.TextBox
    Friend WithEvents txtGoldLoss1 As System.Windows.Forms.TextBox
    Friend WithEvents txtPurchaseBillNo As System.Windows.Forms.TextBox
    Friend WithEvents txtWeight1 As System.Windows.Forms.TextBox
    Friend WithEvents txtDiamondDescription1 As System.Windows.Forms.TextBox
    Friend WithEvents txtfrInventoryCode As System.Windows.Forms.TextBox
    Friend WithEvents txtToInventoryCode As System.Windows.Forms.TextBox
    Friend WithEvents txtSMC1 As System.Windows.Forms.TextBox
    Friend WithEvents txtPCS1 As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalCost1 As System.Windows.Forms.TextBox
    Friend WithEvents optPMCP As System.Windows.Forms.RadioButton
    Friend WithEvents optPMCW As System.Windows.Forms.RadioButton
    Friend WithEvents btnInventorySearchF As System.Windows.Forms.Button
    Friend WithEvents btnInventorySearchT As System.Windows.Forms.Button
    Friend WithEvents lblPMC As System.Windows.Forms.Label
    Friend WithEvents lblSMC As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtTotalCost As System.Windows.Forms.TextBox
    Friend WithEvents txtPCS As System.Windows.Forms.TextBox
    Friend WithEvents txtSMC As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtCostPerGram As System.Windows.Forms.TextBox
    Friend WithEvents txtPMC As System.Windows.Forms.TextBox
    Friend WithEvents txtGoldLoss As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtDiamondDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents ee As System.Windows.Forms.GroupBox
    Friend WithEvents optSMCP As System.Windows.Forms.RadioButton
    Friend WithEvents optSMCW As System.Windows.Forms.RadioButton
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtSize1 As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtSize As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmInventoryMntML))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.txtSize1 = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtTotalCost1 = New System.Windows.Forms.TextBox
        Me.txtPCS1 = New System.Windows.Forms.TextBox
        Me.txtSMC1 = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtDescription1 = New System.Windows.Forms.TextBox
        Me.txtCostPerGram1 = New System.Windows.Forms.TextBox
        Me.txtPMC1 = New System.Windows.Forms.TextBox
        Me.txtGoldLoss1 = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtWeight1 = New System.Windows.Forms.TextBox
        Me.txtDiamondDescription1 = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.line = New System.Windows.Forms.GroupBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.optPMCP = New System.Windows.Forms.RadioButton
        Me.optPMCW = New System.Windows.Forms.RadioButton
        Me.ee = New System.Windows.Forms.GroupBox
        Me.optSMCP = New System.Windows.Forms.RadioButton
        Me.optSMCW = New System.Windows.Forms.RadioButton
        Me.Label14 = New System.Windows.Forms.Label
        Me.lblSMC = New System.Windows.Forms.Label
        Me.GroupBox8 = New System.Windows.Forms.GroupBox
        Me.lblPMC = New System.Windows.Forms.Label
        Me.btnInventorySearchF = New System.Windows.Forms.Button
        Me.txtSupplierCode = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtPurchaseBillNo = New System.Windows.Forms.TextBox
        Me.txtfrInventoryCode = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtToInventoryCode = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.btnInventorySearchT = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.cmdClear = New System.Windows.Forms.Button
        Me.cmdClose = New System.Windows.Forms.Button
        Me.cmdUpdate = New System.Windows.Forms.Button
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtTotalCost = New System.Windows.Forms.TextBox
        Me.txtPCS = New System.Windows.Forms.TextBox
        Me.txtSMC = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtDescription = New System.Windows.Forms.TextBox
        Me.txtCostPerGram = New System.Windows.Forms.TextBox
        Me.txtPMC = New System.Windows.Forms.TextBox
        Me.txtGoldLoss = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.txtWeight = New System.Windows.Forms.TextBox
        Me.txtDiamondDescription = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.txtSize = New System.Windows.Forms.TextBox
        Me.pic = New System.Windows.Forms.PictureBox
        Me.GroupBox1.SuspendLayout()
        Me.line.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.ee.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.Controls.Add(Me.Label25)
        Me.GroupBox1.Controls.Add(Me.txtSize1)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.txtTotalCost1)
        Me.GroupBox1.Controls.Add(Me.txtPCS1)
        Me.GroupBox1.Controls.Add(Me.txtSMC1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtDescription1)
        Me.GroupBox1.Controls.Add(Me.txtCostPerGram1)
        Me.GroupBox1.Controls.Add(Me.txtPMC1)
        Me.GroupBox1.Controls.Add(Me.txtGoldLoss1)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtWeight1)
        Me.GroupBox1.Controls.Add(Me.txtDiamondDescription1)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.line)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Coral
        Me.GroupBox1.Location = New System.Drawing.Point(24, 168)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(408, 344)
        Me.GroupBox1.TabIndex = 329
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Old Data"
        '
        'Label25
        '
        Me.Label25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.Black
        Me.Label25.Location = New System.Drawing.Point(39, 80)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(96, 23)
        Me.Label25.TabIndex = 63
        Me.Label25.Text = "Length/Size"
        '
        'txtSize1
        '
        Me.txtSize1.BackColor = System.Drawing.Color.White
        Me.txtSize1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSize1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSize1.ForeColor = System.Drawing.Color.Navy
        Me.txtSize1.Location = New System.Drawing.Point(160, 80)
        Me.txtSize1.MaxLength = 21
        Me.txtSize1.Name = "txtSize1"
        Me.txtSize1.ReadOnly = True
        Me.txtSize1.Size = New System.Drawing.Size(176, 22)
        Me.txtSize1.TabIndex = 62
        Me.txtSize1.Text = ""
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(39, 304)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(96, 23)
        Me.Label16.TabIndex = 61
        Me.Label16.Text = "Total"
        '
        'txtTotalCost1
        '
        Me.txtTotalCost1.BackColor = System.Drawing.Color.White
        Me.txtTotalCost1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalCost1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalCost1.ForeColor = System.Drawing.Color.Navy
        Me.txtTotalCost1.Location = New System.Drawing.Point(160, 304)
        Me.txtTotalCost1.MaxLength = 21
        Me.txtTotalCost1.Name = "txtTotalCost1"
        Me.txtTotalCost1.ReadOnly = True
        Me.txtTotalCost1.Size = New System.Drawing.Size(180, 22)
        Me.txtTotalCost1.TabIndex = 60
        Me.txtTotalCost1.Text = ""
        '
        'txtPCS1
        '
        Me.txtPCS1.BackColor = System.Drawing.Color.White
        Me.txtPCS1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPCS1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPCS1.ForeColor = System.Drawing.Color.Navy
        Me.txtPCS1.Location = New System.Drawing.Point(160, 272)
        Me.txtPCS1.MaxLength = 21
        Me.txtPCS1.Name = "txtPCS1"
        Me.txtPCS1.ReadOnly = True
        Me.txtPCS1.Size = New System.Drawing.Size(180, 22)
        Me.txtPCS1.TabIndex = 59
        Me.txtPCS1.Text = ""
        '
        'txtSMC1
        '
        Me.txtSMC1.BackColor = System.Drawing.Color.White
        Me.txtSMC1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSMC1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMC1.ForeColor = System.Drawing.Color.Navy
        Me.txtSMC1.Location = New System.Drawing.Point(160, 240)
        Me.txtSMC1.MaxLength = 21
        Me.txtSMC1.Name = "txtSMC1"
        Me.txtSMC1.ReadOnly = True
        Me.txtSMC1.Size = New System.Drawing.Size(180, 22)
        Me.txtSMC1.TabIndex = 58
        Me.txtSMC1.Text = ""
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(39, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 23)
        Me.Label2.TabIndex = 57
        Me.Label2.Text = "Description"
        '
        'txtDescription1
        '
        Me.txtDescription1.BackColor = System.Drawing.Color.White
        Me.txtDescription1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescription1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription1.ForeColor = System.Drawing.Color.Navy
        Me.txtDescription1.Location = New System.Drawing.Point(160, 16)
        Me.txtDescription1.MaxLength = 50
        Me.txtDescription1.Name = "txtDescription1"
        Me.txtDescription1.ReadOnly = True
        Me.txtDescription1.Size = New System.Drawing.Size(240, 22)
        Me.txtDescription1.TabIndex = 2
        Me.txtDescription1.Text = ""
        '
        'txtCostPerGram1
        '
        Me.txtCostPerGram1.BackColor = System.Drawing.Color.White
        Me.txtCostPerGram1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCostPerGram1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostPerGram1.ForeColor = System.Drawing.Color.Navy
        Me.txtCostPerGram1.Location = New System.Drawing.Point(160, 176)
        Me.txtCostPerGram1.MaxLength = 21
        Me.txtCostPerGram1.Name = "txtCostPerGram1"
        Me.txtCostPerGram1.ReadOnly = True
        Me.txtCostPerGram1.Size = New System.Drawing.Size(180, 22)
        Me.txtCostPerGram1.TabIndex = 6
        Me.txtCostPerGram1.Text = ""
        '
        'txtPMC1
        '
        Me.txtPMC1.BackColor = System.Drawing.Color.White
        Me.txtPMC1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPMC1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPMC1.ForeColor = System.Drawing.Color.Navy
        Me.txtPMC1.Location = New System.Drawing.Point(160, 208)
        Me.txtPMC1.MaxLength = 21
        Me.txtPMC1.Name = "txtPMC1"
        Me.txtPMC1.ReadOnly = True
        Me.txtPMC1.Size = New System.Drawing.Size(180, 22)
        Me.txtPMC1.TabIndex = 7
        Me.txtPMC1.Text = ""
        '
        'txtGoldLoss1
        '
        Me.txtGoldLoss1.BackColor = System.Drawing.Color.White
        Me.txtGoldLoss1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldLoss1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoldLoss1.ForeColor = System.Drawing.Color.Navy
        Me.txtGoldLoss1.Location = New System.Drawing.Point(160, 144)
        Me.txtGoldLoss1.MaxLength = 21
        Me.txtGoldLoss1.Name = "txtGoldLoss1"
        Me.txtGoldLoss1.ReadOnly = True
        Me.txtGoldLoss1.Size = New System.Drawing.Size(180, 22)
        Me.txtGoldLoss1.TabIndex = 5
        Me.txtGoldLoss1.Text = ""
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(39, 176)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(96, 23)
        Me.Label15.TabIndex = 48
        Me.Label15.Text = "Cost/gm"
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(39, 240)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(96, 23)
        Me.Label12.TabIndex = 47
        Me.Label12.Text = "SMC"
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(39, 272)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(96, 23)
        Me.Label10.TabIndex = 42
        Me.Label10.Text = "PCS"
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(39, 208)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(96, 23)
        Me.Label11.TabIndex = 41
        Me.Label11.Text = "PMC"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(39, 136)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(96, 23)
        Me.Label8.TabIndex = 35
        Me.Label8.Text = "Gold Loss"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(39, 112)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(96, 23)
        Me.Label7.TabIndex = 31
        Me.Label7.Text = "Weight"
        '
        'txtWeight1
        '
        Me.txtWeight1.BackColor = System.Drawing.Color.White
        Me.txtWeight1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWeight1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeight1.ForeColor = System.Drawing.Color.Navy
        Me.txtWeight1.Location = New System.Drawing.Point(160, 112)
        Me.txtWeight1.MaxLength = 21
        Me.txtWeight1.Name = "txtWeight1"
        Me.txtWeight1.ReadOnly = True
        Me.txtWeight1.Size = New System.Drawing.Size(180, 22)
        Me.txtWeight1.TabIndex = 4
        Me.txtWeight1.Text = ""
        '
        'txtDiamondDescription1
        '
        Me.txtDiamondDescription1.BackColor = System.Drawing.Color.White
        Me.txtDiamondDescription1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDiamondDescription1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDiamondDescription1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiamondDescription1.ForeColor = System.Drawing.Color.Navy
        Me.txtDiamondDescription1.Location = New System.Drawing.Point(160, 48)
        Me.txtDiamondDescription1.MaxLength = 50
        Me.txtDiamondDescription1.Name = "txtDiamondDescription1"
        Me.txtDiamondDescription1.ReadOnly = True
        Me.txtDiamondDescription1.Size = New System.Drawing.Size(240, 22)
        Me.txtDiamondDescription1.TabIndex = 1
        Me.txtDiamondDescription1.Text = ""
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(39, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(136, 23)
        Me.Label3.TabIndex = 26
        Me.Label3.Text = "Diamond Description"
        '
        'line
        '
        Me.line.BackColor = System.Drawing.Color.Orange
        Me.line.Controls.Add(Me.GroupBox4)
        Me.line.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.line.ForeColor = System.Drawing.Color.Black
        Me.line.Location = New System.Drawing.Point(-8, -8)
        Me.line.Name = "line"
        Me.line.Size = New System.Drawing.Size(880, 8)
        Me.line.TabIndex = 31
        Me.line.TabStop = False
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.Orange
        Me.GroupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.GroupBox4.ForeColor = System.Drawing.Color.Black
        Me.GroupBox4.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(880, 8)
        Me.GroupBox4.TabIndex = 32
        Me.GroupBox4.TabStop = False
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.Label24)
        Me.GroupBox6.Controls.Add(Me.optPMCP)
        Me.GroupBox6.Controls.Add(Me.optPMCW)
        Me.GroupBox6.Controls.Add(Me.ee)
        Me.GroupBox6.Controls.Add(Me.Label14)
        Me.GroupBox6.Controls.Add(Me.lblSMC)
        Me.GroupBox6.Controls.Add(Me.GroupBox8)
        Me.GroupBox6.Controls.Add(Me.lblPMC)
        Me.GroupBox6.ForeColor = System.Drawing.Color.Black
        Me.GroupBox6.Location = New System.Drawing.Point(24, 512)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(840, 64)
        Me.GroupBox6.TabIndex = 81
        Me.GroupBox6.TabStop = False
        '
        'Label24
        '
        Me.Label24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Black
        Me.Label24.Location = New System.Drawing.Point(88, 23)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(64, 16)
        Me.Label24.TabIndex = 91
        Me.Label24.Text = "Purchase MC"
        '
        'optPMCP
        '
        Me.optPMCP.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPMCP.Location = New System.Drawing.Point(260, 23)
        Me.optPMCP.Name = "optPMCP"
        Me.optPMCP.Size = New System.Drawing.Size(70, 20)
        Me.optPMCP.TabIndex = 8
        Me.optPMCP.Text = "Per PCS"
        '
        'optPMCW
        '
        Me.optPMCW.Checked = True
        Me.optPMCW.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPMCW.Location = New System.Drawing.Point(182, 23)
        Me.optPMCW.Name = "optPMCW"
        Me.optPMCW.Size = New System.Drawing.Size(80, 20)
        Me.optPMCW.TabIndex = 7
        Me.optPMCW.TabStop = True
        Me.optPMCW.Text = "Per Gram"
        '
        'ee
        '
        Me.ee.Controls.Add(Me.optSMCP)
        Me.ee.Controls.Add(Me.optSMCW)
        Me.ee.ForeColor = System.Drawing.Color.Black
        Me.ee.Location = New System.Drawing.Point(584, 7)
        Me.ee.Name = "ee"
        Me.ee.Size = New System.Drawing.Size(180, 48)
        Me.ee.TabIndex = 600
        Me.ee.TabStop = False
        '
        'optSMCP
        '
        Me.optSMCP.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSMCP.Location = New System.Drawing.Point(96, 16)
        Me.optSMCP.Name = "optSMCP"
        Me.optSMCP.Size = New System.Drawing.Size(80, 24)
        Me.optSMCP.TabIndex = 10
        Me.optSMCP.Text = "Per PCS"
        '
        'optSMCW
        '
        Me.optSMCW.Checked = True
        Me.optSMCW.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSMCW.Location = New System.Drawing.Point(9, 16)
        Me.optSMCW.Name = "optSMCW"
        Me.optSMCW.Size = New System.Drawing.Size(80, 24)
        Me.optSMCW.TabIndex = 9
        Me.optSMCW.TabStop = True
        Me.optSMCW.Text = "Per Gram"
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(512, 25)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(64, 16)
        Me.Label14.TabIndex = 88
        Me.Label14.Text = "Sales MC"
        '
        'lblSMC
        '
        Me.lblSMC.Location = New System.Drawing.Point(432, 16)
        Me.lblSMC.Name = "lblSMC"
        Me.lblSMC.Size = New System.Drawing.Size(80, 24)
        Me.lblSMC.TabIndex = 67
        '
        'GroupBox8
        '
        Me.GroupBox8.Location = New System.Drawing.Point(160, 7)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(180, 48)
        Me.GroupBox8.TabIndex = 500
        Me.GroupBox8.TabStop = False
        '
        'lblPMC
        '
        Me.lblPMC.Location = New System.Drawing.Point(344, 24)
        Me.lblPMC.Name = "lblPMC"
        Me.lblPMC.Size = New System.Drawing.Size(64, 16)
        Me.lblPMC.TabIndex = 66
        '
        'btnInventorySearchF
        '
        Me.btnInventorySearchF.BackColor = System.Drawing.Color.Orange
        Me.btnInventorySearchF.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInventorySearchF.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInventorySearchF.Location = New System.Drawing.Point(338, 12)
        Me.btnInventorySearchF.Name = "btnInventorySearchF"
        Me.btnInventorySearchF.Size = New System.Drawing.Size(30, 22)
        Me.btnInventorySearchF.TabIndex = 57
        Me.btnInventorySearchF.Text = "..."
        Me.btnInventorySearchF.Visible = False
        '
        'txtSupplierCode
        '
        Me.txtSupplierCode.BackColor = System.Drawing.Color.White
        Me.txtSupplierCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierCode.ForeColor = System.Drawing.Color.Navy
        Me.txtSupplierCode.Location = New System.Drawing.Point(160, 12)
        Me.txtSupplierCode.MaxLength = 50
        Me.txtSupplierCode.Name = "txtSupplierCode"
        Me.txtSupplierCode.ReadOnly = True
        Me.txtSupplierCode.Size = New System.Drawing.Size(180, 22)
        Me.txtSupplierCode.TabIndex = 3
        Me.txtSupplierCode.Text = ""
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(450, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(112, 17)
        Me.Label1.TabIndex = 37
        Me.Label1.Text = "Purchase Bill No"
        '
        'txtPurchaseBillNo
        '
        Me.txtPurchaseBillNo.BackColor = System.Drawing.Color.White
        Me.txtPurchaseBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurchaseBillNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurchaseBillNo.ForeColor = System.Drawing.Color.Navy
        Me.txtPurchaseBillNo.Location = New System.Drawing.Point(587, 12)
        Me.txtPurchaseBillNo.MaxLength = 50
        Me.txtPurchaseBillNo.Name = "txtPurchaseBillNo"
        Me.txtPurchaseBillNo.ReadOnly = True
        Me.txtPurchaseBillNo.Size = New System.Drawing.Size(180, 22)
        Me.txtPurchaseBillNo.TabIndex = 10
        Me.txtPurchaseBillNo.Text = ""
        '
        'txtfrInventoryCode
        '
        Me.txtfrInventoryCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtfrInventoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtfrInventoryCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtfrInventoryCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtfrInventoryCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtfrInventoryCode.Location = New System.Drawing.Point(160, 12)
        Me.txtfrInventoryCode.MaxLength = 20
        Me.txtfrInventoryCode.Name = "txtfrInventoryCode"
        Me.txtfrInventoryCode.Size = New System.Drawing.Size(180, 22)
        Me.txtfrInventoryCode.TabIndex = 0
        Me.txtfrInventoryCode.Text = ""
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(39, 12)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 24)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "Supplier Code"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(39, 12)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 23)
        Me.Label5.TabIndex = 25
        Me.Label5.Text = "From Inventory"
        '
        'txtToInventoryCode
        '
        Me.txtToInventoryCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtToInventoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToInventoryCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtToInventoryCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToInventoryCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtToInventoryCode.Location = New System.Drawing.Point(587, 12)
        Me.txtToInventoryCode.MaxLength = 20
        Me.txtToInventoryCode.Name = "txtToInventoryCode"
        Me.txtToInventoryCode.Size = New System.Drawing.Size(180, 22)
        Me.txtToInventoryCode.TabIndex = 1
        Me.txtToInventoryCode.Text = ""
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(450, 12)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(96, 23)
        Me.Label6.TabIndex = 59
        Me.Label6.Text = "To  Inventory"
        '
        'btnInventorySearchT
        '
        Me.btnInventorySearchT.BackColor = System.Drawing.Color.Orange
        Me.btnInventorySearchT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInventorySearchT.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInventorySearchT.Location = New System.Drawing.Point(767, 12)
        Me.btnInventorySearchT.Name = "btnInventorySearchT"
        Me.btnInventorySearchT.Size = New System.Drawing.Size(30, 22)
        Me.btnInventorySearchT.TabIndex = 60
        Me.btnInventorySearchT.Text = "..."
        Me.btnInventorySearchT.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.GroupBox2.Controls.Add(Me.txtToInventoryCode)
        Me.GroupBox2.Controls.Add(Me.txtfrInventoryCode)
        Me.GroupBox2.Controls.Add(Me.btnInventorySearchF)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.btnInventorySearchT)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.ForeColor = System.Drawing.Color.Black
        Me.GroupBox2.Location = New System.Drawing.Point(20, 80)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(844, 40)
        Me.GroupBox2.TabIndex = 303
        Me.GroupBox2.TabStop = False
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.White
        Me.GroupBox5.Controls.Add(Me.Label1)
        Me.GroupBox5.Controls.Add(Me.txtPurchaseBillNo)
        Me.GroupBox5.Controls.Add(Me.Label4)
        Me.GroupBox5.Controls.Add(Me.txtSupplierCode)
        Me.GroupBox5.ForeColor = System.Drawing.Color.Black
        Me.GroupBox5.Location = New System.Drawing.Point(20, 120)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(844, 40)
        Me.GroupBox5.TabIndex = 313
        Me.GroupBox5.TabStop = False
        '
        'cmdClear
        '
        Me.cmdClear.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.cmdClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClear.Image = CType(resources.GetObject("cmdClear.Image"), System.Drawing.Image)
        Me.cmdClear.Location = New System.Drawing.Point(696, 584)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.Size = New System.Drawing.Size(80, 24)
        Me.cmdClear.TabIndex = 12
        Me.cmdClear.Text = "Clear (F6)"
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(784, 584)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(80, 24)
        Me.cmdClose.TabIndex = 13
        Me.cmdClose.Text = "Close (Esc)"
        '
        'cmdUpdate
        '
        Me.cmdUpdate.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.cmdUpdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUpdate.Image = CType(resources.GetObject("cmdUpdate.Image"), System.Drawing.Image)
        Me.cmdUpdate.Location = New System.Drawing.Point(608, 584)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.Size = New System.Drawing.Size(80, 24)
        Me.cmdUpdate.TabIndex = 11
        Me.cmdUpdate.Text = "Save (F7)"
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(32, 304)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(96, 23)
        Me.Label9.TabIndex = 99
        Me.Label9.Text = "Total"
        '
        'txtTotalCost
        '
        Me.txtTotalCost.BackColor = System.Drawing.Color.White
        Me.txtTotalCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalCost.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalCost.ForeColor = System.Drawing.Color.Navy
        Me.txtTotalCost.Location = New System.Drawing.Point(152, 304)
        Me.txtTotalCost.MaxLength = 21
        Me.txtTotalCost.Name = "txtTotalCost"
        Me.txtTotalCost.ReadOnly = True
        Me.txtTotalCost.Size = New System.Drawing.Size(180, 22)
        Me.txtTotalCost.TabIndex = 98
        Me.txtTotalCost.Text = ""
        '
        'txtPCS
        '
        Me.txtPCS.BackColor = System.Drawing.Color.White
        Me.txtPCS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPCS.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPCS.ForeColor = System.Drawing.Color.Navy
        Me.txtPCS.Location = New System.Drawing.Point(152, 272)
        Me.txtPCS.MaxLength = 21
        Me.txtPCS.Name = "txtPCS"
        Me.txtPCS.ReadOnly = True
        Me.txtPCS.Size = New System.Drawing.Size(180, 22)
        Me.txtPCS.TabIndex = 87
        Me.txtPCS.Text = ""
        '
        'txtSMC
        '
        Me.txtSMC.BackColor = System.Drawing.SystemColors.Window
        Me.txtSMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSMC.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtSMC.Location = New System.Drawing.Point(152, 240)
        Me.txtSMC.MaxLength = 21
        Me.txtSMC.Name = "txtSMC"
        Me.txtSMC.Size = New System.Drawing.Size(180, 22)
        Me.txtSMC.TabIndex = 6
        Me.txtSMC.Text = ""
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(32, 16)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(96, 23)
        Me.Label13.TabIndex = 97
        Me.Label13.Text = "Description"
        '
        'txtDescription
        '
        Me.txtDescription.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtDescription.Location = New System.Drawing.Point(152, 16)
        Me.txtDescription.MaxLength = 50
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(240, 22)
        Me.txtDescription.TabIndex = 2
        Me.txtDescription.Text = ""
        '
        'txtCostPerGram
        '
        Me.txtCostPerGram.BackColor = System.Drawing.Color.White
        Me.txtCostPerGram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCostPerGram.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostPerGram.ForeColor = System.Drawing.Color.Navy
        Me.txtCostPerGram.Location = New System.Drawing.Point(152, 176)
        Me.txtCostPerGram.MaxLength = 21
        Me.txtCostPerGram.Name = "txtCostPerGram"
        Me.txtCostPerGram.ReadOnly = True
        Me.txtCostPerGram.Size = New System.Drawing.Size(180, 22)
        Me.txtCostPerGram.TabIndex = 84
        Me.txtCostPerGram.Text = ""
        '
        'txtPMC
        '
        Me.txtPMC.BackColor = System.Drawing.SystemColors.Window
        Me.txtPMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPMC.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPMC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtPMC.Location = New System.Drawing.Point(152, 208)
        Me.txtPMC.MaxLength = 21
        Me.txtPMC.Name = "txtPMC"
        Me.txtPMC.Size = New System.Drawing.Size(180, 22)
        Me.txtPMC.TabIndex = 5
        Me.txtPMC.Text = ""
        '
        'txtGoldLoss
        '
        Me.txtGoldLoss.BackColor = System.Drawing.SystemColors.Window
        Me.txtGoldLoss.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldLoss.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoldLoss.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtGoldLoss.Location = New System.Drawing.Point(152, 144)
        Me.txtGoldLoss.MaxLength = 21
        Me.txtGoldLoss.Name = "txtGoldLoss"
        Me.txtGoldLoss.Size = New System.Drawing.Size(180, 22)
        Me.txtGoldLoss.TabIndex = 4
        Me.txtGoldLoss.Text = ""
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(32, 176)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(96, 23)
        Me.Label17.TabIndex = 96
        Me.Label17.Text = "Cost/gm"
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(32, 240)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(96, 23)
        Me.Label18.TabIndex = 95
        Me.Label18.Text = "SMC"
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(32, 272)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(96, 23)
        Me.Label19.TabIndex = 94
        Me.Label19.Text = "PCS"
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.Location = New System.Drawing.Point(32, 208)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(96, 23)
        Me.Label20.TabIndex = 93
        Me.Label20.Text = "PMC"
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Black
        Me.Label21.Location = New System.Drawing.Point(32, 144)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(96, 23)
        Me.Label21.TabIndex = 92
        Me.Label21.Text = "Gold Loss"
        '
        'Label22
        '
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.Black
        Me.Label22.Location = New System.Drawing.Point(32, 112)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(96, 23)
        Me.Label22.TabIndex = 91
        Me.Label22.Text = "Weight"
        '
        'txtWeight
        '
        Me.txtWeight.BackColor = System.Drawing.Color.White
        Me.txtWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWeight.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeight.ForeColor = System.Drawing.Color.Navy
        Me.txtWeight.Location = New System.Drawing.Point(152, 112)
        Me.txtWeight.MaxLength = 21
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.ReadOnly = True
        Me.txtWeight.Size = New System.Drawing.Size(180, 22)
        Me.txtWeight.TabIndex = 82
        Me.txtWeight.Text = ""
        '
        'txtDiamondDescription
        '
        Me.txtDiamondDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDiamondDescription.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDiamondDescription.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiamondDescription.Location = New System.Drawing.Point(152, 48)
        Me.txtDiamondDescription.MaxLength = 50
        Me.txtDiamondDescription.Name = "txtDiamondDescription"
        Me.txtDiamondDescription.Size = New System.Drawing.Size(240, 22)
        Me.txtDiamondDescription.TabIndex = 3
        Me.txtDiamondDescription.Text = ""
        '
        'Label23
        '
        Me.Label23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.Black
        Me.Label23.Location = New System.Drawing.Point(32, 48)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(136, 23)
        Me.Label23.TabIndex = 90
        Me.Label23.Text = "Diamond Description"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label26)
        Me.GroupBox3.Controls.Add(Me.txtSize)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.txtPCS)
        Me.GroupBox3.Controls.Add(Me.txtSMC)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.txtDescription)
        Me.GroupBox3.Controls.Add(Me.txtCostPerGram)
        Me.GroupBox3.Controls.Add(Me.txtPMC)
        Me.GroupBox3.Controls.Add(Me.txtGoldLoss)
        Me.GroupBox3.Controls.Add(Me.Label17)
        Me.GroupBox3.Controls.Add(Me.Label18)
        Me.GroupBox3.Controls.Add(Me.Label19)
        Me.GroupBox3.Controls.Add(Me.Label20)
        Me.GroupBox3.Controls.Add(Me.Label21)
        Me.GroupBox3.Controls.Add(Me.Label22)
        Me.GroupBox3.Controls.Add(Me.txtWeight)
        Me.GroupBox3.Controls.Add(Me.txtDiamondDescription)
        Me.GroupBox3.Controls.Add(Me.Label23)
        Me.GroupBox3.Controls.Add(Me.txtTotalCost)
        Me.GroupBox3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.Coral
        Me.GroupBox3.Location = New System.Drawing.Point(456, 168)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(408, 344)
        Me.GroupBox3.TabIndex = 400
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Editable"
        '
        'Label26
        '
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.Black
        Me.Label26.Location = New System.Drawing.Point(32, 80)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(96, 23)
        Me.Label26.TabIndex = 101
        Me.Label26.Text = "Length/Size"
        '
        'txtSize
        '
        Me.txtSize.BackColor = System.Drawing.Color.White
        Me.txtSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSize.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSize.ForeColor = System.Drawing.Color.Black
        Me.txtSize.Location = New System.Drawing.Point(152, 80)
        Me.txtSize.MaxLength = 21
        Me.txtSize.Name = "txtSize"
        Me.txtSize.Size = New System.Drawing.Size(180, 22)
        Me.txtSize.TabIndex = 100
        Me.txtSize.Text = ""
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
        'frmInventoryMntML
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(896, 623)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.cmdClear)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.cmdUpdate)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.GroupBox5)
        Me.ForeColor = System.Drawing.Color.Black
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmInventoryMntML"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Inventory Maintenance"
        Me.GroupBox1.ResumeLayout(False)
        Me.line.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.ee.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click

        Try
            Dim iCnt As Long
            Dim ds As DataSet
            Dim Mode As String
            Dim sErr As String
            Dim iMsg
            Dim barCodeFlag As Boolean
            Dim arrBarcode(100)

            If Len(lblPMC.Text) = 0 Then
                If optPMCW.Checked Then
                    lblPMC.Text = Format(Val(txtPMC.Text) * Val(txtWeight.Text), "#0.00")
                    If sCompanyShortCode = "ES" Then
                        lblPMC.Text = Format(fncGetNearestDollar(Val(lblPMC.Text)), "#0.00")
                    End If
                Else
                    lblPMC.Text = Format(Val(txtPMC.Text) * Val(txtPCS.Text), "#0.00")
                End If
                Call subTotal()
            End If

            If Len(lblSMC.Text) = 0 Then
                If optSMCW.Checked Then
                    lblSMC.Text = Format(Val(txtSMC.Text) * Val(txtWeight.Text), "#0.00")
                    If sCompanyShortCode = "ES" Then
                        lblSMC.Text = Format(fncGetNearestDollar(Val(lblSMC.Text)), "#0.00")
                    End If
                Else
                    lblSMC.Text = Format(Val(txtSMC.Text) * Val(txtPCS.Text), "#0.00")
                End If
                Call subTotal()
            End If

            barCodeFlag = False

            If Trim(txtfrInventoryCode.Text) > Trim(txtToInventoryCode.Text) Then
                txtToInventoryCode.Text = Trim(txtfrInventoryCode.Text)
            End If
            If Trim(txtfrInventoryCode.Text) = "" Then
                MsgBox("Invalid Inventory")
                Exit Sub
            End If
            If Trim(txtToInventoryCode.Text) = "" Then
                txtToInventoryCode.Text = Trim(txtfrInventoryCode.Text)
            End If

            'If Trim(txtDescription1.Text) = Trim(txtDescription.Text) And Trim(txtDiamondDescription1.Text) = Trim(txtDiamondDescription.Text) And Val(txtGoldLoss1.Text) = Val(txtGoldLoss.Text) And Val(txtPMC1.Text) = Val(lblPMC.Text) And Val(txtSMC1.Text) = Val(lblSMC.Text) Then
            'Msg'Box("No value has been modified")
            'Exit Sub
            'End If
            If Trim(txtDescription1.Text) = Trim(txtDescription.Text) And Trim(txtDiamondDescription1.Text) = Trim(txtDiamondDescription.Text) And Val(txtGoldLoss1.Text) = Val(txtGoldLoss.Text) And Val(txtSMC1.Text) = Val(lblSMC.Text) Then
                barCodeFlag = False
            Else
                barCodeFlag = True
            End If

            ds = objClsInventory.GetInventoryRange(Trim(txtfrInventoryCode.Text), Trim(txtToInventoryCode.Text), Trim(txtSupplierCode.Text), Trim(txtPurchaseBillNo.Text))
            Call subTotal()

            sPMCType = IIf(optPMCP.Checked, "P", "W")
            sSMCType = IIf(optSMCP.Checked, "P", "W")
            
            Dim i As Long
            i = 0
            If ds.Tables(0).Rows.Count > 0 Then
                iMsg = MsgBox("Would to like to Save all Inventories " + " ?", vbYesNo)
                If iMsg = vbYes Then
                    Mode = "I"
                    For iCnt = 0 To ds.Tables(0).Rows.Count - 1
                        If Val(ds.Tables(0).Rows(iCnt)("QtySold") & "") = 0 Then
                            If objClsInventory.InventoryMntML(Trim(ds.Tables(0).Rows(iCnt)("Inventorycode") & ""), Trim(txtDescription1.Text), Trim(txtDiamondDescription1.Text), Val(txtSize1.Text), Trim(sPMCType), Trim(sSMCType), Trim(txtPMC1.Text), Trim(txtSMC1.Text), Trim(txtTotalCost1.Text), Trim(txtGoldLoss1.Text), Trim(txtDescription.Text), Trim(txtDiamondDescription.Text), Val(txtSize.Text), Trim(sPMCType), Trim(sSMCType), Val(lblPMC.Text), Val(lblSMC.Text), Val(txtTotalCost.Text), Val(txtGoldLoss.Text), sUserID) Then

                                arrBarcode(i) = Trim(ds.Tables(0).Rows(iCnt)("Inventorycode") & "")
                                i = i + 1

                            Else
                                MsgBox("Transaction not saved")
                                'TransactionRollBack(oTconn)
                                Exit Sub
                            End If
                        ElseIf Val(ds.Tables(0).Rows(iCnt)("QtySold") & "") > 0 Then
                            If objClsInventory.InventoryMntML(Trim(ds.Tables(0).Rows(iCnt)("Inventorycode") & ""), Trim(txtDescription1.Text), Trim(txtDiamondDescription1.Text), Val(txtSize1.Text), Trim(sPMCType), Trim(sSMCType), Trim(txtPMC1.Text), Trim(txtSMC1.Text), Trim(txtTotalCost1.Text), Trim(txtGoldLoss1.Text), Trim(txtDescription.Text), Trim(txtDiamondDescription.Text), Val(txtSize.Text), Trim(sPMCType), Trim(sSMCType), Val(lblPMC.Text), Val(lblSMC.Text), Val(txtTotalCost.Text), Val(txtGoldLoss.Text), sUserID) Then
                                arrBarcode(i) = Trim(ds.Tables(0).Rows(iCnt)("Inventorycode") & "")
                                i = i + 1
                            Else
                                MsgBox("Transaction not saved", MsgBoxStyle.Information)
                                Exit Sub
                            End If
                        End If
                    Next
                    
                End If
            Else
                MsgBox("No Items Matching", MsgBoxStyle.Information)
                txtfrInventoryCode.Focus()
                Exit Sub
            End If
            Exit Sub
        Catch ex As Exception
            'Call TransactionRollBack(oTconn)
            MsgBox(Err.Description)
        Finally
            subClearItems()
            txtfrInventoryCode.Focus()
            Err.Clear()
        End Try
    End Sub

    Private Sub btnInventorySearchF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInventorySearchF.Click
        Try
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblInventory", "L", 4, "InventoryCode", "", "InventoryCode", "InventoryCode", 80, "Description", "Description", 150, "Weight ", "Weight", 50, "PurchaseBillNo", "PurchaseBillNo", 50)
            frmHelpClass.ShowDialog()
            txtfrInventoryCode.Text = frmHelpClass.Var1.Trim
            subGetInventory(txtfrInventoryCode.Text.Trim)
        Catch ex As Exception
            MessageBox.Show(Err.Description)
        End Try

    End Sub

    Private Sub btnInventorySearchT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInventorySearchT.Click
        Try
            If txtfrInventoryCode.Text.Trim <> "" Then
                Dim frmHelpClass As New frmHelpSearch(0, 0, "tblInventory", "L", 4, "InventoryCode", "", "InventoryCode", "InventoryCode", 80, "Description", "Description", 180, "Weight", "Weight", 60, "PurchaseBillNo", "PurchaseBillNo", 80)
                frmHelpClass.ShowDialog()
                txtToInventoryCode.Text = frmHelpClass.Var1.Trim
            Else
                MsgBox(" Fill the from inventory code for Range selection  !", MsgBoxStyle.Information)
                Exit Sub
                txtfrInventoryCode.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(Err.Description)
        End Try
    End Sub

    Private Sub subGetInventory(ByVal sInventoryCode As String)
        ', ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim sPMC As Long
        Dim ds As DataSet
        Dim ds1 As DataSet
        If sCompanyShortCode.Trim = "ES" Then
            ' txtGoldLoss.BackColor()
            txtGoldLoss.ReadOnly = False
        Else
            'txtGoldLoss.BackColor = &HE0E0E0
            txtGoldLoss.ReadOnly = True
        End If
        ds = objClsInventory.GetInventoryDetail(sInventoryCode.Trim)
        If ds.Tables(0).Rows.Count > 0 Then
            If Val(Trim(ds.Tables(0).Rows(0)("QtyOnHand") & "")) > 0 Then
                txtDescription.Text = Trim(ds.Tables(0).Rows(0)("Description") & "")
                txtDescription1.Text = Trim(ds.Tables(0).Rows(0)("Description") & "")
                txtDiamondDescription.Text = Trim(ds.Tables(0).Rows(0)("DiamondDescription") & "")
                txtDiamondDescription1.Text = Trim(ds.Tables(0).Rows(0)("DiamondDescription") & "")
                txtSupplierCode.Text = Trim(ds.Tables(0).Rows(0)("SupplierCode") & "")
                txtPurchaseBillNo.Text = Trim(ds.Tables(0).Rows(0)("PurchaseBillNo") & "")
                txtSize.Text = Val(Trim(ds.Tables(0).Rows(0)("Size") & ""))
                txtWeight.Text = Val(Trim(ds.Tables(0).Rows(0)("Weight") & ""))
                txtGoldLoss.Text = Val(Trim(ds.Tables(0).Rows(0)("GoldLoss") & ""))
                txtCostPerGram.Text = Val(Trim(ds.Tables(0).Rows(0)("CostperGram") & ""))

                If Trim(ds.Tables(0).Rows(0)("PMCType") & "") = "W" Then
                    txtPMC.Text = Format(Val(ds.Tables(0).Rows(0)("PMC") & "") / Val(ds.Tables(0).Rows(0)("Weight") & ""), "#0.0000")
                Else
                    txtPMC.Text = Format(Val(ds.Tables(0).Rows(0)("PMC") & "") / Val(ds.Tables(0).Rows(0)("Pcs") & ""), "#0.00")
                End If

                txtPCS.Text = CLng(Val(ds.Tables(0).Rows(0)("Pcs") & ""))
                txtTotalCost.Text = Format(Val(ds.Tables(0).Rows(0)("TotalCost") & ""), "0.00")
                txtSize1.Text = Val(Trim(ds.Tables(0).Rows(0)("Size") & ""))
                txtWeight1.Text = Val(ds.Tables(0).Rows(0)("Weight") & "")
                txtCostPerGram1.Text = Val(ds.Tables(0).Rows(0)("CostperGram") & "")
                txtPMC1.Text = Val(ds.Tables(0).Rows(0)("PMC") & "")

                If Trim(ds.Tables(0).Rows(0)("PMC") & "") = "W" Then
                    txtSMC.Text = Format(Val(ds.Tables(0).Rows(0)("sMc") & "") / Val(ds.Tables(0).Rows(0)("Weight") & ""), "#0.0000")
                Else
                    txtSMC.Text = Format(Val(ds.Tables(0).Rows(0)("sMc") & "") / Val(ds.Tables(0).Rows(0)("Pcs") & ""), "#0.0000")
                End If
                txtSMC1.Text = Val(ds.Tables(0).Rows(0)("sMc") & "")
                txtGoldLoss1.Text = Val(ds.Tables(0).Rows(0)("GoldLoss") & "")
                txtTotalCost1.Text = Val(ds.Tables(0).Rows(0)("TotalCost") & "")
                txtPCS1.Text = Val(ds.Tables(0).Rows(0)("Pcs") & "")

                If Trim(ds.Tables(0).Rows(0)("PMCType") & "") = "W" Then
                    optPMCW.Checked = True
                Else
                    optPMCP.Checked = True
                End If
                subGetPMC()
                'txtPMC_LostFocus(sender, e)
                If Trim(ds.Tables(0).Rows(0)("SMCType") & "") = "W" Then
                    optSMCW.Checked = True
                Else
                    optSMCP.Checked = True
                End If
                subGetSMC()
                'txtSMC_LostFocus(sender, e)
            Else
                MsgBox("No Stock for this Inventory!", MsgBoxStyle.Information)
                txtfrInventoryCode.Text = ""
                subClearItems()
                txtfrInventoryCode.Focus()
            End If
        Else
            MsgBox("This Inventory Not Found... ", MsgBoxStyle.Exclamation)
            txtfrInventoryCode.Text = ""
            subClearItems()
            txtfrInventoryCode.Focus()
        End If


    End Sub

    Private Sub optPMCP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optPMCP.Click
        Try
            If Val(txtPMC.Text) >= 0 Then
                lblPMC.Text = Format(Val(txtPMC.Text) * Val(txtPCS.Text), "#0.00")
            Else
                MsgBox("Invalid Making cost!", MsgBoxStyle.Information)
                txtPMC.Focus()
            End If
            Call subTotal()
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Sub

    Private Sub optPMCW_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optPMCW.Click
        Try
            If Val(txtPMC.Text) >= 0 Then
                lblPMC.Text = Format(Val(txtPMC.Text) * Val(txtWeight.Text), "#0.00")
            Else
                MsgBox("Invalid Making cost!")
                txtPMC.Focus()
            End If
            If sCompanyShortCode = "ES" Then
                lblPMC.Text = Format(fncGetNearestDollar(Val(lblPMC.Text)), "#0.00")
            End If
            Call subTotal()
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Sub

    Private Sub optSMCP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optSMCP.Click
        Try
            If Val(txtSMC.Text) >= 0 Then
                lblSMC.Text = Format(Val(txtSMC.Text) * Val(txtPCS.Text), "#0.00")
            Else
                MsgBox("Invalid sales making cost!")
                txtPMC.Focus()
            End If
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Sub

    Private Sub optSMCW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optSMCW.Click
        Try
            If Val(txtSMC.Text) >= 0 Then
                lblSMC.Text = Format(Val(txtSMC.Text) * Val(txtWeight.Text), "#0.00")
                If sCompanyShortCode = "ES" Then
                    lblSMC.Text = Format(fncGetNearestDollar(Val(lblSMC.Text)), "#0.00")
                End If
            Else
                MsgBox("Invalid sales making cost!")
                txtPMC.Focus()
            End If
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Sub

    Private Sub txtfrInventoryCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtfrInventoryCode.LostFocus
        If txtfrInventoryCode.Text.Trim <> "" Then
            subGetInventory(txtfrInventoryCode.Text.Trim)
        End If
    End Sub
    Private Sub subGetPMC()
        Try

            If Val(txtPMC.Text) >= 0 Then
                If optPMCW.Checked Then
                    lblPMC.Text = Format(Val(txtPMC.Text) * Val(txtWeight.Text), "#0.00")
                    If sCompanyShortCode = "ES" Then
                        lblPMC.Text = Format(fncGetNearestDollar(Val(lblPMC.Text)), "#0.00")
                    End If
                Else
                    lblPMC.Text = Format(Val(txtPMC.Text) * Val(txtPCS.Text), "#0.00")
                End If
                subTotal()
            Else
                MsgBox("Invalid Making cost!", MsgBoxStyle.Information)
                'txtPMC.Focus()
            End If

        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Sub

    Private Sub txtPMC_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If IsNumeric(txtPMC.Text) Then
            subGetPMC()
        Else
            MsgBox("Give the Numeric Values!", MsgBoxStyle.Information)
            txtPMC.Focus()
        End If

    End Sub
    Private Sub txtSMC_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If IsNumeric(txtSMC.Text) Then
            subGetSMC()
        Else
            MsgBox("Give the Numeric Values!")
            txtSMC.Focus()
        End If
    End Sub
    Private Sub subGetSMC()
        Try
            If optSMCW.Checked Then
                lblSMC.Text = Format(Val(txtSMC.Text) * Val(txtWeight.Text), "#0.00")
                If sCompanyShortCode = "ES" Then
                    lblSMC.Text = Format(fncGetNearestDollar(Val(lblSMC.Text)), "#0.00")
                End If
            Else
                lblSMC.Text = Format(Val(txtSMC.Text) * Val(txtPCS.Text), "#0.00")
            End If
            subTotal()

        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try

    End Sub

    Private Sub subTotal()
        Try
            If Trim(sPurchaseCostingGL) = "Y" Then
                txtTotalCost.Text = Format((Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram.Text) + IIf(optPMCW.Checked, Val(txtPMC.Text) * Val(txtWeight.Text), Val(txtPCS.Text) * Val(txtPMC.Text)), "0.00")
            Else
                txtTotalCost.Text = Format(Val(txtWeight.Text) * Val(txtCostPerGram.Text) + Val(lblPMC.Text), "0.00")
            End If
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Sub


    Private Sub frmInventoryMnt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                cmdClose_Click(sender, e)
            Case Keys.Enter
                SendKeys.Send("{tab}")
            Case Keys.F6
                cmdClear_Click(sender, e)
            Case Keys.F7
                cmdUpdate_Click(sender, e)
        End Select
    End Sub

    Private Sub subClearItems()
        txtfrInventoryCode.Text = ""
        txtDescription.Text = ""
        txtSize.Text = ""
        txtWeight.Text = ""
        txtGoldLoss.Text = ""
        txtPMC.Text = ""
        txtCostPerGram.Text = ""
        txtPCS.Text = ""
        txtTotalCost.Text = ""
        txtCostPerGram1.Text = ""
        txtDescription1.Text = ""
        txtDiamondDescription.Text = ""
        txtDiamondDescription1.Text = ""
        txtGoldLoss1.Text = ""
        txtPCS1.Text = ""
        txtPMC1.Text = ""
        txtPurchaseBillNo.Text = ""
        txtSMC.Text = ""
        txtSMC1.Text = ""
        txtSupplierCode.Text = ""
        txtTotalCost1.Text = ""
        txtSize1.Text = ""
        txtWeight1.Text = ""
        lblSMC.Text = ""
        lblPMC.Text = ""
        txtToInventoryCode.Text = ""

    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub cmdClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClear.Click
        subClearItems()
        txtfrInventoryCode.Focus()
    End Sub

    Private Sub frmInventoryMnt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'SetPosPinters("BARCODE")
    End Sub

    Private Sub printBarcode(ByVal sinventorycode As String)
        Try
            Dim dsInv As New DataSet
            Dim dsCls As New DataSet
            Dim dsMCC As New DataSet
            Dim sSelection As String
            Dim CR As New ReportDocument
            Dim frmrep As New frmReports
            Dim sClassName, sChar, sTemp As String
            Dim dNo As Double
            Dim objinv As New clsInventory
            Dim objCls As New ClsClass

            If sinventorycode = "Total" Or sinventorycode = "" Then Exit Sub

            dsInv = objinv.GetInventoryDetail(Trim(sinventorycode))

            If dsInv.Tables(0).Rows.Count = 0 Then
                MessageBox.Show("Inventory not found!")
                Exit Sub
            Else
                If Trim$(dsInv.Tables(0).Rows(0)("ClassCode") & "") <> "" Then
                    dsCls = objCls.GetClassDetail(Trim$(dsInv.Tables(0).Rows(0)("ClassCode") & ""))
                    If dsCls.Tables(0).Rows.Count > 0 Then sClassName = Trim$(dsCls.Tables(0).Rows(0)("ClassName") & "")
                End If
            End If
            dNo = Format$(CDbl(dsInv.Tables(0).Rows(0)("sMc") & ""), "###0")
            sChar = ""
            dsMCC = objinv.GetMcConvertion(dNo)
            If dsMCC.Tables(0).Rows.Count > 0 Then
                sChar = Mid(Trim(dsMCC.Tables(0).Rows(0)("Mc") & ""), 1, Len(Trim(dsMCC.Tables(0).Rows(0)("Mc") & "")) - 3) 'Remove the decimal part
            End If
            sTemp = Trim$(dsInv.Tables(0).Rows(0)("Description") & "")

            strpath = Application.StartupPath & "\Reports\rptBarcode.rpt"
            CR.Load(strpath)
            ReportLogOnToServer(CR)
            CR.Refresh()
            frmrep.ReportViewer.ReportSource = CR
            CR.DataDefinition.FormulaFields("Barcode").Text = fncFormatSQLText(Trim(dsInv.Tables(0).Rows(0)("Inventorycode") & ""))
            CR.DataDefinition.FormulaFields("Description").Text = fncFormatSQLText(Trim(dsInv.Tables(0).Rows(0)("Description") & ""))
            CR.DataDefinition.FormulaFields("TotalWeight").Text = fncFormatSQLText("NWT : " & Trim(Format(Val(dsInv.Tables(0).Rows(0)("Weight") & ""), "0.00")) & "gm")
            CR.DataDefinition.FormulaFields("MC").Text = fncFormatSQLText("MC :$ " & Trim(Format(Val(dsInv.Tables(0).Rows(0)("sMC") & ""), "0.00")))
            CR.DataDefinition.FormulaFields("GoldLoss").Text = fncFormatSQLText(Trim(dsInv.Tables(0).Rows(0)("SupplierCode") & "") & ": " & Format(dsInv.Tables(0).Rows(0)("PurchaseDate"), "MM/yyyy") & "(" & Trim(Format(dsInv.Tables(0).Rows(0)("GoldLoss") & "", "0.00")) & "gm")
            CR.DataDefinition.FormulaFields("Wt").Text = fncFormatSQLText("GWT :" & Trim(Format(dsInv.Tables(0).Rows(0)("Weight") & "", "0.00")) & "gm")
            CR.DataDefinition.FormulaFields("ClassCode").Text = fncFormatSQLText(Trim(dsInv.Tables(0).Rows(0)("ClassCode") & ""))
            CR.PrintToPrinter(1, True, 0, 0)
            frmrep.Show()
            frmrep.ReportViewer.Visible = True
            frmrep.ReportViewer.DisplayToolbar = True
            frmrep.ReportViewer.CloseView(False)
            frmrep.WindowState = FormWindowState.Maximized
            frmrep = Nothing
            frmrep = New frmReports

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Function fncBreakString(ByRef i As Integer, ByVal sMsg As String) As String
        Try
            Dim Y As Integer
            Dim X As Integer

            If Trim(sMsg) > 25 Then
                Y = Len(Trim(sMsg))
                Y = Y / 2 - 5
                For X = Y To Len(Trim(sMsg))
                    If Mid(sMsg, X, 1) = " " Then
                        fncBreakString = Mid(sMsg, 1, X)
                        i = X
                        Exit Function
                    End If

                Next X
            End If
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Function

    Private Function GetPriceCode(ByVal sPrice$) As String
        Try
            Dim nCnt%
            Dim sPriceCode$

            sPrice = Trim$(CLng(sPrice))
            If Trim$(sPrice) = "" Then
                GetPriceCode = ""
                Exit Function
            End If

            For nCnt = 1 To Len(sPrice)
                sPriceCode = sPriceCode & masPriceCode(Val(Mid$(sPrice, nCnt, 1)))
            Next

            GetPriceCode = sPriceCode
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try

    End Function

    Private Sub txtToInventoryCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtToInventoryCode.LostFocus
        Dim ds As DataSet
        If txtToInventoryCode.Text.Trim.Length <> 0 Then
            ds = objClsInventory.GetInventoryRange(Trim(txtfrInventoryCode.Text), Trim(txtToInventoryCode.Text), Trim(txtSupplierCode.Text), Trim(txtPurchaseBillNo.Text))
            If Not ds.Tables(0).Rows.Count > 0 Then
                txtToInventoryCode.Text = ""
                txtDescription.Focus()
            End If
        End If
    End Sub

    Private Sub optPMCW_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPMCW.CheckedChanged

    End Sub

    Private Sub optPMCW_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles optPMCW.Click

    End Sub



    Private Sub txtfrInventoryCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtfrInventoryCode.TextChanged

    End Sub
End Class
