Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System
Imports System.IO
Public Class frmInventoryMnt
    Inherits System.Windows.Forms.Form
    Dim objClsInventory As New clsInventory
    Dim oTconn As ADODB.Connection
    Dim oTds As DataSet
    Dim sPMCType As String
    Dim sSMCType As String
    Dim sOldValue As String
    Dim sNewValue As String
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtSizeCode1 As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtSizeCode As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents btnSizeCode As System.Windows.Forms.Button
    Dim masPriceCode(9) As String
    Dim sSizeType As String
    Dim sOldSizeType As String
    Friend WithEvents grpAddlDescription As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents txtNewSellingPrice4 As System.Windows.Forms.TextBox
    Friend WithEvents txtNewSellingPrice3 As System.Windows.Forms.TextBox
    Friend WithEvents txtNewSellingPrice2 As System.Windows.Forms.TextBox
    Friend WithEvents txtNewSellingPrice1 As System.Windows.Forms.TextBox
    Friend WithEvents txtNewCost4 As System.Windows.Forms.TextBox
    Friend WithEvents txtNewCost3 As System.Windows.Forms.TextBox
    Friend WithEvents txtNewCost2 As System.Windows.Forms.TextBox
    Friend WithEvents txtNewCost1 As System.Windows.Forms.TextBox
    Friend WithEvents txtNewDesc4 As System.Windows.Forms.TextBox
    Friend WithEvents txtNewDesc3 As System.Windows.Forms.TextBox
    Friend WithEvents txtNewDesc2 As System.Windows.Forms.TextBox
    Friend WithEvents txtNewDesc1 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents txtSellingPrice4 As System.Windows.Forms.TextBox
    Friend WithEvents txtSellingPrice3 As System.Windows.Forms.TextBox
    Friend WithEvents txtSellingPrice2 As System.Windows.Forms.TextBox
    Friend WithEvents txtSellingPrice1 As System.Windows.Forms.TextBox
    Friend WithEvents txtCost4 As System.Windows.Forms.TextBox
    Friend WithEvents txtCost3 As System.Windows.Forms.TextBox
    Friend WithEvents txtCost2 As System.Windows.Forms.TextBox
    Friend WithEvents txtCost1 As System.Windows.Forms.TextBox
    Friend WithEvents txtDesc4 As System.Windows.Forms.TextBox
    Friend WithEvents txtDesc3 As System.Windows.Forms.TextBox
    Friend WithEvents txtDesc2 As System.Windows.Forms.TextBox
    Friend WithEvents txtDesc1 As System.Windows.Forms.TextBox
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents cmdAddlDescOk As System.Windows.Forms.Button
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents chkAdditionalDesc As System.Windows.Forms.CheckBox
    Friend WithEvents grpImage As System.Windows.Forms.GroupBox
    Friend WithEvents btnBrowseImage As System.Windows.Forms.Button
    Friend WithEvents picImage As System.Windows.Forms.PictureBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents chkImage As System.Windows.Forms.CheckBox
    Dim sCategoryCode As String
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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInventoryMnt))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtSizeCode1 = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtTotalCost1 = New System.Windows.Forms.TextBox()
        Me.txtPCS1 = New System.Windows.Forms.TextBox()
        Me.txtSMC1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtDescription1 = New System.Windows.Forms.TextBox()
        Me.txtCostPerGram1 = New System.Windows.Forms.TextBox()
        Me.txtPMC1 = New System.Windows.Forms.TextBox()
        Me.txtGoldLoss1 = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtWeight1 = New System.Windows.Forms.TextBox()
        Me.txtDiamondDescription1 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.line = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.optPMCP = New System.Windows.Forms.RadioButton()
        Me.optPMCW = New System.Windows.Forms.RadioButton()
        Me.ee = New System.Windows.Forms.GroupBox()
        Me.optSMCP = New System.Windows.Forms.RadioButton()
        Me.optSMCW = New System.Windows.Forms.RadioButton()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblSMC = New System.Windows.Forms.Label()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.lblPMC = New System.Windows.Forms.Label()
        Me.btnInventorySearchF = New System.Windows.Forms.Button()
        Me.txtSupplierCode = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPurchaseBillNo = New System.Windows.Forms.TextBox()
        Me.txtfrInventoryCode = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtToInventoryCode = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnInventorySearchT = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.cmdClear = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdUpdate = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtTotalCost = New System.Windows.Forms.TextBox()
        Me.txtPCS = New System.Windows.Forms.TextBox()
        Me.txtSMC = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.txtCostPerGram = New System.Windows.Forms.TextBox()
        Me.txtPMC = New System.Windows.Forms.TextBox()
        Me.txtGoldLoss = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtWeight = New System.Windows.Forms.TextBox()
        Me.txtDiamondDescription = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.txtSizeCode = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.btnSizeCode = New System.Windows.Forms.Button()
        Me.grpAddlDescription = New System.Windows.Forms.GroupBox()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.txtNewSellingPrice4 = New System.Windows.Forms.TextBox()
        Me.txtNewSellingPrice3 = New System.Windows.Forms.TextBox()
        Me.txtNewSellingPrice2 = New System.Windows.Forms.TextBox()
        Me.txtNewSellingPrice1 = New System.Windows.Forms.TextBox()
        Me.txtNewCost4 = New System.Windows.Forms.TextBox()
        Me.txtNewCost3 = New System.Windows.Forms.TextBox()
        Me.txtNewCost2 = New System.Windows.Forms.TextBox()
        Me.txtNewCost1 = New System.Windows.Forms.TextBox()
        Me.txtNewDesc4 = New System.Windows.Forms.TextBox()
        Me.txtNewDesc3 = New System.Windows.Forms.TextBox()
        Me.txtNewDesc2 = New System.Windows.Forms.TextBox()
        Me.txtNewDesc1 = New System.Windows.Forms.TextBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.txtSellingPrice4 = New System.Windows.Forms.TextBox()
        Me.txtSellingPrice3 = New System.Windows.Forms.TextBox()
        Me.txtSellingPrice2 = New System.Windows.Forms.TextBox()
        Me.txtSellingPrice1 = New System.Windows.Forms.TextBox()
        Me.txtCost4 = New System.Windows.Forms.TextBox()
        Me.txtCost3 = New System.Windows.Forms.TextBox()
        Me.txtCost2 = New System.Windows.Forms.TextBox()
        Me.txtCost1 = New System.Windows.Forms.TextBox()
        Me.txtDesc4 = New System.Windows.Forms.TextBox()
        Me.txtDesc3 = New System.Windows.Forms.TextBox()
        Me.txtDesc2 = New System.Windows.Forms.TextBox()
        Me.txtDesc1 = New System.Windows.Forms.TextBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.cmdAddlDescOk = New System.Windows.Forms.Button()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.chkAdditionalDesc = New System.Windows.Forms.CheckBox()
        Me.grpImage = New System.Windows.Forms.GroupBox()
        Me.btnBrowseImage = New System.Windows.Forms.Button()
        Me.picImage = New System.Windows.Forms.PictureBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.chkImage = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout()
        Me.line.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.ee.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpAddlDescription.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.grpImage.SuspendLayout()
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.Controls.Add(Me.Label26)
        Me.GroupBox1.Controls.Add(Me.txtSizeCode1)
        Me.GroupBox1.Controls.Add(Me.Label25)
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
        Me.GroupBox1.Location = New System.Drawing.Point(24, 197)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(408, 344)
        Me.GroupBox1.TabIndex = 329
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Old Data"
        '
        'Label26
        '
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.Black
        Me.Label26.Location = New System.Drawing.Point(463, 277)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(96, 23)
        Me.Label26.TabIndex = 64
        Me.Label26.Text = "Size"
        '
        'txtSizeCode1
        '
        Me.txtSizeCode1.BackColor = System.Drawing.Color.White
        Me.txtSizeCode1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSizeCode1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSizeCode1.ForeColor = System.Drawing.Color.Navy
        Me.txtSizeCode1.Location = New System.Drawing.Point(160, 276)
        Me.txtSizeCode1.MaxLength = 21
        Me.txtSizeCode1.Name = "txtSizeCode1"
        Me.txtSizeCode1.ReadOnly = True
        Me.txtSizeCode1.Size = New System.Drawing.Size(180, 22)
        Me.txtSizeCode1.TabIndex = 63
        '
        'Label25
        '
        Me.Label25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.Black
        Me.Label25.Location = New System.Drawing.Point(39, 276)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(96, 23)
        Me.Label25.TabIndex = 62
        Me.Label25.Text = "Size"
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
        '
        'txtPCS1
        '
        Me.txtPCS1.BackColor = System.Drawing.Color.White
        Me.txtPCS1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPCS1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPCS1.ForeColor = System.Drawing.Color.Navy
        Me.txtPCS1.Location = New System.Drawing.Point(160, 248)
        Me.txtPCS1.MaxLength = 21
        Me.txtPCS1.Name = "txtPCS1"
        Me.txtPCS1.ReadOnly = True
        Me.txtPCS1.Size = New System.Drawing.Size(180, 22)
        Me.txtPCS1.TabIndex = 59
        '
        'txtSMC1
        '
        Me.txtSMC1.BackColor = System.Drawing.Color.White
        Me.txtSMC1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSMC1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMC1.ForeColor = System.Drawing.Color.Navy
        Me.txtSMC1.Location = New System.Drawing.Point(160, 216)
        Me.txtSMC1.MaxLength = 21
        Me.txtSMC1.Name = "txtSMC1"
        Me.txtSMC1.ReadOnly = True
        Me.txtSMC1.Size = New System.Drawing.Size(180, 22)
        Me.txtSMC1.TabIndex = 58
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
        '
        'txtCostPerGram1
        '
        Me.txtCostPerGram1.BackColor = System.Drawing.Color.White
        Me.txtCostPerGram1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCostPerGram1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostPerGram1.ForeColor = System.Drawing.Color.Navy
        Me.txtCostPerGram1.Location = New System.Drawing.Point(160, 152)
        Me.txtCostPerGram1.MaxLength = 21
        Me.txtCostPerGram1.Name = "txtCostPerGram1"
        Me.txtCostPerGram1.ReadOnly = True
        Me.txtCostPerGram1.Size = New System.Drawing.Size(180, 22)
        Me.txtCostPerGram1.TabIndex = 6
        '
        'txtPMC1
        '
        Me.txtPMC1.BackColor = System.Drawing.Color.White
        Me.txtPMC1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPMC1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPMC1.ForeColor = System.Drawing.Color.Navy
        Me.txtPMC1.Location = New System.Drawing.Point(160, 184)
        Me.txtPMC1.MaxLength = 21
        Me.txtPMC1.Name = "txtPMC1"
        Me.txtPMC1.ReadOnly = True
        Me.txtPMC1.Size = New System.Drawing.Size(180, 22)
        Me.txtPMC1.TabIndex = 7
        '
        'txtGoldLoss1
        '
        Me.txtGoldLoss1.BackColor = System.Drawing.Color.White
        Me.txtGoldLoss1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldLoss1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoldLoss1.ForeColor = System.Drawing.Color.Navy
        Me.txtGoldLoss1.Location = New System.Drawing.Point(160, 112)
        Me.txtGoldLoss1.MaxLength = 21
        Me.txtGoldLoss1.Name = "txtGoldLoss1"
        Me.txtGoldLoss1.ReadOnly = True
        Me.txtGoldLoss1.Size = New System.Drawing.Size(180, 22)
        Me.txtGoldLoss1.TabIndex = 5
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(39, 152)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(96, 23)
        Me.Label15.TabIndex = 48
        Me.Label15.Text = "Cost/gm"
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(39, 216)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(96, 23)
        Me.Label12.TabIndex = 47
        Me.Label12.Text = "SMC"
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(39, 248)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(96, 23)
        Me.Label10.TabIndex = 42
        Me.Label10.Text = "PCS"
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(39, 184)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(96, 23)
        Me.Label11.TabIndex = 41
        Me.Label11.Text = "PMC"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(39, 112)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(96, 23)
        Me.Label8.TabIndex = 35
        Me.Label8.Text = "Gold Loss"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(39, 80)
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
        Me.txtWeight1.Location = New System.Drawing.Point(160, 80)
        Me.txtWeight1.MaxLength = 21
        Me.txtWeight1.Name = "txtWeight1"
        Me.txtWeight1.ReadOnly = True
        Me.txtWeight1.Size = New System.Drawing.Size(180, 22)
        Me.txtWeight1.TabIndex = 4
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
        Me.GroupBox6.Location = New System.Drawing.Point(24, 540)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(840, 64)
        Me.GroupBox6.TabIndex = 81
        Me.GroupBox6.TabStop = False
        '
        'Label24
        '
        Me.Label24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Black
        Me.Label24.Location = New System.Drawing.Point(145, 23)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(64, 16)
        Me.Label24.TabIndex = 91
        Me.Label24.Text = "Purchase MC"
        '
        'optPMCP
        '
        Me.optPMCP.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPMCP.Location = New System.Drawing.Point(317, 23)
        Me.optPMCP.Name = "optPMCP"
        Me.optPMCP.Size = New System.Drawing.Size(91, 20)
        Me.optPMCP.TabIndex = 8
        Me.optPMCP.Text = "Per PCS"
        '
        'optPMCW
        '
        Me.optPMCW.Checked = True
        Me.optPMCW.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPMCW.Location = New System.Drawing.Point(239, 23)
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
        Me.GroupBox8.Location = New System.Drawing.Point(217, 7)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(206, 48)
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
        Me.btnInventorySearchF.UseVisualStyleBackColor = False
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
        Me.btnInventorySearchT.UseVisualStyleBackColor = False
        Me.btnInventorySearchT.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox2.Controls.Add(Me.txtToInventoryCode)
        Me.GroupBox2.Controls.Add(Me.txtfrInventoryCode)
        Me.GroupBox2.Controls.Add(Me.btnInventorySearchF)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.btnInventorySearchT)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.ForeColor = System.Drawing.Color.Black
        Me.GroupBox2.Location = New System.Drawing.Point(20, 109)
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
        Me.GroupBox5.Location = New System.Drawing.Point(20, 143)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(844, 40)
        Me.GroupBox5.TabIndex = 313
        Me.GroupBox5.TabStop = False
        '
        'cmdClear
        '
        Me.cmdClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClear.Location = New System.Drawing.Point(658, 610)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.Size = New System.Drawing.Size(100, 40)
        Me.cmdClear.TabIndex = 12
        Me.cmdClear.Text = "Clear (F6)"
        Me.cmdClear.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.Location = New System.Drawing.Point(764, 610)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(100, 40)
        Me.cmdClose.TabIndex = 13
        Me.cmdClose.Text = "Close (Esc)"
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'cmdUpdate
        '
        Me.cmdUpdate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdUpdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUpdate.Location = New System.Drawing.Point(552, 610)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.Size = New System.Drawing.Size(100, 40)
        Me.cmdUpdate.TabIndex = 11
        Me.cmdUpdate.Text = "Save (F7)"
        Me.cmdUpdate.UseVisualStyleBackColor = False
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(32, 308)
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
        Me.txtTotalCost.Location = New System.Drawing.Point(152, 308)
        Me.txtTotalCost.MaxLength = 21
        Me.txtTotalCost.Name = "txtTotalCost"
        Me.txtTotalCost.ReadOnly = True
        Me.txtTotalCost.Size = New System.Drawing.Size(180, 22)
        Me.txtTotalCost.TabIndex = 98
        '
        'txtPCS
        '
        Me.txtPCS.BackColor = System.Drawing.Color.White
        Me.txtPCS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPCS.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPCS.ForeColor = System.Drawing.Color.Navy
        Me.txtPCS.Location = New System.Drawing.Point(152, 248)
        Me.txtPCS.MaxLength = 21
        Me.txtPCS.Name = "txtPCS"
        Me.txtPCS.ReadOnly = True
        Me.txtPCS.Size = New System.Drawing.Size(180, 22)
        Me.txtPCS.TabIndex = 87
        '
        'txtSMC
        '
        Me.txtSMC.BackColor = System.Drawing.SystemColors.Window
        Me.txtSMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSMC.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtSMC.Location = New System.Drawing.Point(152, 216)
        Me.txtSMC.MaxLength = 21
        Me.txtSMC.Name = "txtSMC"
        Me.txtSMC.Size = New System.Drawing.Size(180, 22)
        Me.txtSMC.TabIndex = 6
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
        '
        'txtCostPerGram
        '
        Me.txtCostPerGram.BackColor = System.Drawing.Color.White
        Me.txtCostPerGram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCostPerGram.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostPerGram.ForeColor = System.Drawing.Color.Navy
        Me.txtCostPerGram.Location = New System.Drawing.Point(152, 152)
        Me.txtCostPerGram.MaxLength = 21
        Me.txtCostPerGram.Name = "txtCostPerGram"
        Me.txtCostPerGram.ReadOnly = True
        Me.txtCostPerGram.Size = New System.Drawing.Size(180, 22)
        Me.txtCostPerGram.TabIndex = 84
        '
        'txtPMC
        '
        Me.txtPMC.BackColor = System.Drawing.SystemColors.Window
        Me.txtPMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPMC.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPMC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtPMC.Location = New System.Drawing.Point(152, 184)
        Me.txtPMC.MaxLength = 21
        Me.txtPMC.Name = "txtPMC"
        Me.txtPMC.Size = New System.Drawing.Size(180, 22)
        Me.txtPMC.TabIndex = 5
        '
        'txtGoldLoss
        '
        Me.txtGoldLoss.BackColor = System.Drawing.SystemColors.Window
        Me.txtGoldLoss.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldLoss.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoldLoss.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtGoldLoss.Location = New System.Drawing.Point(152, 112)
        Me.txtGoldLoss.MaxLength = 21
        Me.txtGoldLoss.Name = "txtGoldLoss"
        Me.txtGoldLoss.Size = New System.Drawing.Size(180, 22)
        Me.txtGoldLoss.TabIndex = 4
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(32, 152)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(96, 23)
        Me.Label17.TabIndex = 96
        Me.Label17.Text = "Cost/gm"
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(32, 216)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(96, 23)
        Me.Label18.TabIndex = 95
        Me.Label18.Text = "SMC"
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(32, 248)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(96, 23)
        Me.Label19.TabIndex = 94
        Me.Label19.Text = "PCS"
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.Location = New System.Drawing.Point(32, 184)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(96, 23)
        Me.Label20.TabIndex = 93
        Me.Label20.Text = "PMC"
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Black
        Me.Label21.Location = New System.Drawing.Point(32, 112)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(96, 23)
        Me.Label21.TabIndex = 92
        Me.Label21.Text = "Gold Loss"
        '
        'Label22
        '
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.Black
        Me.Label22.Location = New System.Drawing.Point(32, 80)
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
        Me.txtWeight.Location = New System.Drawing.Point(152, 80)
        Me.txtWeight.MaxLength = 21
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.ReadOnly = True
        Me.txtWeight.Size = New System.Drawing.Size(180, 22)
        Me.txtWeight.TabIndex = 82
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
        Me.GroupBox3.Location = New System.Drawing.Point(456, 197)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(408, 344)
        Me.GroupBox3.TabIndex = 400
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Editable"
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(20, 3)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 100)
        Me.pic.TabIndex = 401
        Me.pic.TabStop = False
        '
        'txtSizeCode
        '
        Me.txtSizeCode.BackColor = System.Drawing.Color.White
        Me.txtSizeCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSizeCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSizeCode.ForeColor = System.Drawing.Color.Navy
        Me.txtSizeCode.Location = New System.Drawing.Point(607, 473)
        Me.txtSizeCode.MaxLength = 21
        Me.txtSizeCode.Name = "txtSizeCode"
        Me.txtSizeCode.ReadOnly = True
        Me.txtSizeCode.Size = New System.Drawing.Size(180, 22)
        Me.txtSizeCode.TabIndex = 403
        '
        'Label27
        '
        Me.Label27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.Color.Black
        Me.Label27.Location = New System.Drawing.Point(486, 473)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(96, 23)
        Me.Label27.TabIndex = 402
        Me.Label27.Text = "Size"
        '
        'btnSizeCode
        '
        Me.btnSizeCode.BackColor = System.Drawing.Color.Orange
        Me.btnSizeCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSizeCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSizeCode.Location = New System.Drawing.Point(758, 473)
        Me.btnSizeCode.Name = "btnSizeCode"
        Me.btnSizeCode.Size = New System.Drawing.Size(30, 22)
        Me.btnSizeCode.TabIndex = 404
        Me.btnSizeCode.Text = "..."
        Me.btnSizeCode.UseVisualStyleBackColor = False
        '
        'grpAddlDescription
        '
        Me.grpAddlDescription.Controls.Add(Me.GroupBox9)
        Me.grpAddlDescription.Controls.Add(Me.GroupBox7)
        Me.grpAddlDescription.Controls.Add(Me.Label42)
        Me.grpAddlDescription.Controls.Add(Me.Label41)
        Me.grpAddlDescription.Controls.Add(Me.cmdAddlDescOk)
        Me.grpAddlDescription.Controls.Add(Me.Label37)
        Me.grpAddlDescription.Controls.Add(Me.Label40)
        Me.grpAddlDescription.Controls.Add(Me.Label39)
        Me.grpAddlDescription.Controls.Add(Me.Label38)
        Me.grpAddlDescription.Location = New System.Drawing.Point(226, 197)
        Me.grpAddlDescription.Name = "grpAddlDescription"
        Me.grpAddlDescription.Size = New System.Drawing.Size(445, 325)
        Me.grpAddlDescription.TabIndex = 405
        Me.grpAddlDescription.TabStop = False
        Me.grpAddlDescription.Visible = False
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.txtNewSellingPrice4)
        Me.GroupBox9.Controls.Add(Me.txtNewSellingPrice3)
        Me.GroupBox9.Controls.Add(Me.txtNewSellingPrice2)
        Me.GroupBox9.Controls.Add(Me.txtNewSellingPrice1)
        Me.GroupBox9.Controls.Add(Me.txtNewCost4)
        Me.GroupBox9.Controls.Add(Me.txtNewCost3)
        Me.GroupBox9.Controls.Add(Me.txtNewCost2)
        Me.GroupBox9.Controls.Add(Me.txtNewCost1)
        Me.GroupBox9.Controls.Add(Me.txtNewDesc4)
        Me.GroupBox9.Controls.Add(Me.txtNewDesc3)
        Me.GroupBox9.Controls.Add(Me.txtNewDesc2)
        Me.GroupBox9.Controls.Add(Me.txtNewDesc1)
        Me.GroupBox9.Location = New System.Drawing.Point(12, 165)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(417, 119)
        Me.GroupBox9.TabIndex = 531
        Me.GroupBox9.TabStop = False
        '
        'txtNewSellingPrice4
        '
        Me.txtNewSellingPrice4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewSellingPrice4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewSellingPrice4.ForeColor = System.Drawing.Color.Black
        Me.txtNewSellingPrice4.Location = New System.Drawing.Point(315, 89)
        Me.txtNewSellingPrice4.MaxLength = 50
        Me.txtNewSellingPrice4.Name = "txtNewSellingPrice4"
        Me.txtNewSellingPrice4.Size = New System.Drawing.Size(97, 20)
        Me.txtNewSellingPrice4.TabIndex = 56
        Me.txtNewSellingPrice4.Tag = ""
        '
        'txtNewSellingPrice3
        '
        Me.txtNewSellingPrice3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewSellingPrice3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewSellingPrice3.ForeColor = System.Drawing.Color.Black
        Me.txtNewSellingPrice3.Location = New System.Drawing.Point(315, 63)
        Me.txtNewSellingPrice3.MaxLength = 50
        Me.txtNewSellingPrice3.Name = "txtNewSellingPrice3"
        Me.txtNewSellingPrice3.Size = New System.Drawing.Size(98, 20)
        Me.txtNewSellingPrice3.TabIndex = 53
        Me.txtNewSellingPrice3.Tag = ""
        '
        'txtNewSellingPrice2
        '
        Me.txtNewSellingPrice2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewSellingPrice2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewSellingPrice2.ForeColor = System.Drawing.Color.Black
        Me.txtNewSellingPrice2.Location = New System.Drawing.Point(315, 37)
        Me.txtNewSellingPrice2.MaxLength = 50
        Me.txtNewSellingPrice2.Name = "txtNewSellingPrice2"
        Me.txtNewSellingPrice2.Size = New System.Drawing.Size(98, 20)
        Me.txtNewSellingPrice2.TabIndex = 50
        Me.txtNewSellingPrice2.Tag = ""
        '
        'txtNewSellingPrice1
        '
        Me.txtNewSellingPrice1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewSellingPrice1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewSellingPrice1.ForeColor = System.Drawing.Color.Black
        Me.txtNewSellingPrice1.Location = New System.Drawing.Point(315, 11)
        Me.txtNewSellingPrice1.MaxLength = 50
        Me.txtNewSellingPrice1.Name = "txtNewSellingPrice1"
        Me.txtNewSellingPrice1.Size = New System.Drawing.Size(98, 20)
        Me.txtNewSellingPrice1.TabIndex = 47
        Me.txtNewSellingPrice1.Tag = ""
        '
        'txtNewCost4
        '
        Me.txtNewCost4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewCost4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewCost4.ForeColor = System.Drawing.Color.Black
        Me.txtNewCost4.Location = New System.Drawing.Point(205, 89)
        Me.txtNewCost4.MaxLength = 50
        Me.txtNewCost4.Name = "txtNewCost4"
        Me.txtNewCost4.Size = New System.Drawing.Size(97, 20)
        Me.txtNewCost4.TabIndex = 55
        Me.txtNewCost4.Tag = ""
        '
        'txtNewCost3
        '
        Me.txtNewCost3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewCost3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewCost3.ForeColor = System.Drawing.Color.Black
        Me.txtNewCost3.Location = New System.Drawing.Point(205, 63)
        Me.txtNewCost3.MaxLength = 50
        Me.txtNewCost3.Name = "txtNewCost3"
        Me.txtNewCost3.Size = New System.Drawing.Size(98, 20)
        Me.txtNewCost3.TabIndex = 52
        Me.txtNewCost3.Tag = ""
        '
        'txtNewCost2
        '
        Me.txtNewCost2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewCost2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewCost2.ForeColor = System.Drawing.Color.Black
        Me.txtNewCost2.Location = New System.Drawing.Point(205, 37)
        Me.txtNewCost2.MaxLength = 50
        Me.txtNewCost2.Name = "txtNewCost2"
        Me.txtNewCost2.Size = New System.Drawing.Size(98, 20)
        Me.txtNewCost2.TabIndex = 49
        Me.txtNewCost2.Tag = ""
        '
        'txtNewCost1
        '
        Me.txtNewCost1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewCost1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewCost1.ForeColor = System.Drawing.Color.Black
        Me.txtNewCost1.Location = New System.Drawing.Point(205, 11)
        Me.txtNewCost1.MaxLength = 50
        Me.txtNewCost1.Name = "txtNewCost1"
        Me.txtNewCost1.Size = New System.Drawing.Size(98, 20)
        Me.txtNewCost1.TabIndex = 46
        Me.txtNewCost1.Tag = ""
        '
        'txtNewDesc4
        '
        Me.txtNewDesc4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewDesc4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewDesc4.ForeColor = System.Drawing.Color.Black
        Me.txtNewDesc4.Location = New System.Drawing.Point(3, 88)
        Me.txtNewDesc4.MaxLength = 50
        Me.txtNewDesc4.Name = "txtNewDesc4"
        Me.txtNewDesc4.Size = New System.Drawing.Size(187, 20)
        Me.txtNewDesc4.TabIndex = 54
        Me.txtNewDesc4.Tag = ""
        '
        'txtNewDesc3
        '
        Me.txtNewDesc3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewDesc3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewDesc3.ForeColor = System.Drawing.Color.Black
        Me.txtNewDesc3.Location = New System.Drawing.Point(3, 62)
        Me.txtNewDesc3.MaxLength = 50
        Me.txtNewDesc3.Name = "txtNewDesc3"
        Me.txtNewDesc3.Size = New System.Drawing.Size(188, 20)
        Me.txtNewDesc3.TabIndex = 51
        Me.txtNewDesc3.Tag = ""
        '
        'txtNewDesc2
        '
        Me.txtNewDesc2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewDesc2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewDesc2.ForeColor = System.Drawing.Color.Black
        Me.txtNewDesc2.Location = New System.Drawing.Point(3, 36)
        Me.txtNewDesc2.MaxLength = 50
        Me.txtNewDesc2.Name = "txtNewDesc2"
        Me.txtNewDesc2.Size = New System.Drawing.Size(188, 20)
        Me.txtNewDesc2.TabIndex = 48
        Me.txtNewDesc2.Tag = ""
        '
        'txtNewDesc1
        '
        Me.txtNewDesc1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewDesc1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewDesc1.ForeColor = System.Drawing.Color.Black
        Me.txtNewDesc1.Location = New System.Drawing.Point(3, 10)
        Me.txtNewDesc1.MaxLength = 50
        Me.txtNewDesc1.Name = "txtNewDesc1"
        Me.txtNewDesc1.Size = New System.Drawing.Size(188, 20)
        Me.txtNewDesc1.TabIndex = 45
        Me.txtNewDesc1.Tag = ""
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.txtSellingPrice4)
        Me.GroupBox7.Controls.Add(Me.txtSellingPrice3)
        Me.GroupBox7.Controls.Add(Me.txtSellingPrice2)
        Me.GroupBox7.Controls.Add(Me.txtSellingPrice1)
        Me.GroupBox7.Controls.Add(Me.txtCost4)
        Me.GroupBox7.Controls.Add(Me.txtCost3)
        Me.GroupBox7.Controls.Add(Me.txtCost2)
        Me.GroupBox7.Controls.Add(Me.txtCost1)
        Me.GroupBox7.Controls.Add(Me.txtDesc4)
        Me.GroupBox7.Controls.Add(Me.txtDesc3)
        Me.GroupBox7.Controls.Add(Me.txtDesc2)
        Me.GroupBox7.Controls.Add(Me.txtDesc1)
        Me.GroupBox7.Location = New System.Drawing.Point(12, 33)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(417, 119)
        Me.GroupBox7.TabIndex = 530
        Me.GroupBox7.TabStop = False
        '
        'txtSellingPrice4
        '
        Me.txtSellingPrice4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSellingPrice4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSellingPrice4.ForeColor = System.Drawing.Color.Black
        Me.txtSellingPrice4.Location = New System.Drawing.Point(315, 89)
        Me.txtSellingPrice4.MaxLength = 50
        Me.txtSellingPrice4.Name = "txtSellingPrice4"
        Me.txtSellingPrice4.ReadOnly = True
        Me.txtSellingPrice4.Size = New System.Drawing.Size(97, 20)
        Me.txtSellingPrice4.TabIndex = 56
        Me.txtSellingPrice4.Tag = ""
        '
        'txtSellingPrice3
        '
        Me.txtSellingPrice3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSellingPrice3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSellingPrice3.ForeColor = System.Drawing.Color.Black
        Me.txtSellingPrice3.Location = New System.Drawing.Point(315, 63)
        Me.txtSellingPrice3.MaxLength = 50
        Me.txtSellingPrice3.Name = "txtSellingPrice3"
        Me.txtSellingPrice3.ReadOnly = True
        Me.txtSellingPrice3.Size = New System.Drawing.Size(98, 20)
        Me.txtSellingPrice3.TabIndex = 53
        Me.txtSellingPrice3.Tag = ""
        '
        'txtSellingPrice2
        '
        Me.txtSellingPrice2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSellingPrice2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSellingPrice2.ForeColor = System.Drawing.Color.Black
        Me.txtSellingPrice2.Location = New System.Drawing.Point(315, 37)
        Me.txtSellingPrice2.MaxLength = 50
        Me.txtSellingPrice2.Name = "txtSellingPrice2"
        Me.txtSellingPrice2.ReadOnly = True
        Me.txtSellingPrice2.Size = New System.Drawing.Size(98, 20)
        Me.txtSellingPrice2.TabIndex = 50
        Me.txtSellingPrice2.Tag = ""
        '
        'txtSellingPrice1
        '
        Me.txtSellingPrice1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSellingPrice1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSellingPrice1.ForeColor = System.Drawing.Color.Black
        Me.txtSellingPrice1.Location = New System.Drawing.Point(315, 11)
        Me.txtSellingPrice1.MaxLength = 50
        Me.txtSellingPrice1.Name = "txtSellingPrice1"
        Me.txtSellingPrice1.ReadOnly = True
        Me.txtSellingPrice1.Size = New System.Drawing.Size(98, 20)
        Me.txtSellingPrice1.TabIndex = 47
        Me.txtSellingPrice1.Tag = ""
        '
        'txtCost4
        '
        Me.txtCost4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCost4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCost4.ForeColor = System.Drawing.Color.Black
        Me.txtCost4.Location = New System.Drawing.Point(205, 89)
        Me.txtCost4.MaxLength = 50
        Me.txtCost4.Name = "txtCost4"
        Me.txtCost4.ReadOnly = True
        Me.txtCost4.Size = New System.Drawing.Size(97, 20)
        Me.txtCost4.TabIndex = 55
        Me.txtCost4.Tag = ""
        '
        'txtCost3
        '
        Me.txtCost3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCost3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCost3.ForeColor = System.Drawing.Color.Black
        Me.txtCost3.Location = New System.Drawing.Point(205, 63)
        Me.txtCost3.MaxLength = 50
        Me.txtCost3.Name = "txtCost3"
        Me.txtCost3.ReadOnly = True
        Me.txtCost3.Size = New System.Drawing.Size(98, 20)
        Me.txtCost3.TabIndex = 52
        Me.txtCost3.Tag = ""
        '
        'txtCost2
        '
        Me.txtCost2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCost2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCost2.ForeColor = System.Drawing.Color.Black
        Me.txtCost2.Location = New System.Drawing.Point(205, 37)
        Me.txtCost2.MaxLength = 50
        Me.txtCost2.Name = "txtCost2"
        Me.txtCost2.ReadOnly = True
        Me.txtCost2.Size = New System.Drawing.Size(98, 20)
        Me.txtCost2.TabIndex = 49
        Me.txtCost2.Tag = ""
        '
        'txtCost1
        '
        Me.txtCost1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCost1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCost1.ForeColor = System.Drawing.Color.Black
        Me.txtCost1.Location = New System.Drawing.Point(205, 11)
        Me.txtCost1.MaxLength = 50
        Me.txtCost1.Name = "txtCost1"
        Me.txtCost1.ReadOnly = True
        Me.txtCost1.Size = New System.Drawing.Size(98, 20)
        Me.txtCost1.TabIndex = 46
        Me.txtCost1.Tag = ""
        '
        'txtDesc4
        '
        Me.txtDesc4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDesc4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc4.ForeColor = System.Drawing.Color.Black
        Me.txtDesc4.Location = New System.Drawing.Point(3, 88)
        Me.txtDesc4.MaxLength = 50
        Me.txtDesc4.Name = "txtDesc4"
        Me.txtDesc4.ReadOnly = True
        Me.txtDesc4.Size = New System.Drawing.Size(187, 20)
        Me.txtDesc4.TabIndex = 54
        Me.txtDesc4.Tag = ""
        '
        'txtDesc3
        '
        Me.txtDesc3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDesc3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc3.ForeColor = System.Drawing.Color.Black
        Me.txtDesc3.Location = New System.Drawing.Point(3, 62)
        Me.txtDesc3.MaxLength = 50
        Me.txtDesc3.Name = "txtDesc3"
        Me.txtDesc3.ReadOnly = True
        Me.txtDesc3.Size = New System.Drawing.Size(188, 20)
        Me.txtDesc3.TabIndex = 51
        Me.txtDesc3.Tag = ""
        '
        'txtDesc2
        '
        Me.txtDesc2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDesc2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc2.ForeColor = System.Drawing.Color.Black
        Me.txtDesc2.Location = New System.Drawing.Point(3, 36)
        Me.txtDesc2.MaxLength = 50
        Me.txtDesc2.Name = "txtDesc2"
        Me.txtDesc2.ReadOnly = True
        Me.txtDesc2.Size = New System.Drawing.Size(188, 20)
        Me.txtDesc2.TabIndex = 48
        Me.txtDesc2.Tag = ""
        '
        'txtDesc1
        '
        Me.txtDesc1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDesc1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc1.ForeColor = System.Drawing.Color.Black
        Me.txtDesc1.Location = New System.Drawing.Point(3, 10)
        Me.txtDesc1.MaxLength = 50
        Me.txtDesc1.Name = "txtDesc1"
        Me.txtDesc1.ReadOnly = True
        Me.txtDesc1.Size = New System.Drawing.Size(188, 20)
        Me.txtDesc1.TabIndex = 45
        Me.txtDesc1.Tag = ""
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label42.Location = New System.Drawing.Point(324, 17)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(75, 13)
        Me.Label42.TabIndex = 529
        Me.Label42.Text = "Selling Price"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label41.Location = New System.Drawing.Point(214, 17)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(32, 13)
        Me.Label41.TabIndex = 524
        Me.Label41.Text = "Cost"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdAddlDescOk
        '
        Me.cmdAddlDescOk.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdAddlDescOk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAddlDescOk.ForeColor = System.Drawing.Color.Black
        Me.cmdAddlDescOk.Location = New System.Drawing.Point(365, 286)
        Me.cmdAddlDescOk.Name = "cmdAddlDescOk"
        Me.cmdAddlDescOk.Size = New System.Drawing.Size(61, 23)
        Me.cmdAddlDescOk.TabIndex = 45
        Me.cmdAddlDescOk.Text = "OK"
        Me.cmdAddlDescOk.UseVisualStyleBackColor = False
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label37.Location = New System.Drawing.Point(12, 16)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(71, 13)
        Me.Label37.TabIndex = 515
        Me.Label37.Text = "Description"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label40.Location = New System.Drawing.Point(19, 99)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(78, 13)
        Me.Label40.TabIndex = 518
        Me.Label40.Text = "Description4"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label40.Visible = False
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label39.Location = New System.Drawing.Point(20, 73)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(78, 13)
        Me.Label39.TabIndex = 517
        Me.Label39.Text = "Description3"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label39.Visible = False
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label38.Location = New System.Drawing.Point(20, 47)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(78, 13)
        Me.Label38.TabIndex = 516
        Me.Label38.Text = "Description2"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label38.Visible = False
        '
        'chkAdditionalDesc
        '
        Me.chkAdditionalDesc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAdditionalDesc.ForeColor = System.Drawing.Color.Black
        Me.chkAdditionalDesc.Location = New System.Drawing.Point(38, 557)
        Me.chkAdditionalDesc.Name = "chkAdditionalDesc"
        Me.chkAdditionalDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAdditionalDesc.Size = New System.Drawing.Size(68, 31)
        Me.chkAdditionalDesc.TabIndex = 522
        Me.chkAdditionalDesc.Text = "Addl. Desc"
        '
        'grpImage
        '
        Me.grpImage.Controls.Add(Me.btnBrowseImage)
        Me.grpImage.Controls.Add(Me.picImage)
        Me.grpImage.Controls.Add(Me.Button1)
        Me.grpImage.Location = New System.Drawing.Point(112, 372)
        Me.grpImage.Name = "grpImage"
        Me.grpImage.Size = New System.Drawing.Size(266, 282)
        Me.grpImage.TabIndex = 476264
        Me.grpImage.TabStop = False
        Me.grpImage.Visible = False
        '
        'btnBrowseImage
        '
        Me.btnBrowseImage.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnBrowseImage.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowseImage.Location = New System.Drawing.Point(32, 237)
        Me.btnBrowseImage.Name = "btnBrowseImage"
        Me.btnBrowseImage.Size = New System.Drawing.Size(58, 25)
        Me.btnBrowseImage.TabIndex = 771
        Me.btnBrowseImage.Text = "Browse"
        Me.btnBrowseImage.UseVisualStyleBackColor = False
        '
        'picImage
        '
        Me.picImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picImage.Location = New System.Drawing.Point(32, 15)
        Me.picImage.Name = "picImage"
        Me.picImage.Size = New System.Drawing.Size(206, 217)
        Me.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picImage.TabIndex = 770
        Me.picImage.TabStop = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Button1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.Black
        Me.Button1.Location = New System.Drawing.Point(177, 237)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(61, 25)
        Me.Button1.TabIndex = 45
        Me.Button1.Text = "OK"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'chkImage
        '
        Me.chkImage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkImage.ForeColor = System.Drawing.Color.Black
        Me.chkImage.Location = New System.Drawing.Point(38, 621)
        Me.chkImage.Name = "chkImage"
        Me.chkImage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkImage.Size = New System.Drawing.Size(64, 18)
        Me.chkImage.TabIndex = 476263
        Me.chkImage.Text = "Image"
        '
        'frmInventoryMnt
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(896, 662)
        Me.Controls.Add(Me.chkImage)
        Me.Controls.Add(Me.grpImage)
        Me.Controls.Add(Me.chkAdditionalDesc)
        Me.Controls.Add(Me.grpAddlDescription)
        Me.Controls.Add(Me.btnSizeCode)
        Me.Controls.Add(Me.txtSizeCode)
        Me.Controls.Add(Me.Label27)
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
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmInventoryMnt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Inventory Maintenance"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.line.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.ee.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpAddlDescription.ResumeLayout(False)
        Me.grpAddlDescription.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.grpImage.ResumeLayout(False)
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

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
            Dim ByteArr() As Byte

            If chkAdditionalDesc.Checked Then
                If Val(txtCost1.Text) > 0 Then
                    txtPMC.Text = Val(txtPMC.Text) + Val(txtNewCost1.Text)
                End If
                If Val(txtCost2.Text) > 0 Then
                    txtPMC.Text = Val(txtPMC.Text) + Val(txtNewCost2.Text)
                End If
                If Val(txtCost3.Text) > 0 Then
                    txtPMC.Text = Val(txtPMC.Text) + Val(txtNewCost3.Text)
                End If
                If Val(txtCost4.Text) > 0 Then
                    txtPMC.Text = Val(txtPMC.Text) + Val(txtNewCost4.Text)
                End If

                'If Val(txtSellingPrice1.Text) > 0 Then
                '    txtSMCCost.Text = Val(txtSMCCost.Text) + Val(txtSellingPrice1.Text)
                'End If
                'If Val(txtSellingPrice2.Text) > 0 Then
                '    txtSMCCost.Text = Val(txtSMCCost.Text) + Val(txtSellingPrice2.Text)
                'End If
                'If Val(txtSellingPrice3.Text) > 0 Then
                '    txtSMCCost.Text = Val(txtSMCCost.Text) + Val(txtSellingPrice3.Text)
                'End If
                'If Val(txtSellingPrice4.Text) > 0 Then
                '    txtSMCCost.Text = Val(txtSMCCost.Text) + Val(txtSellingPrice4.Text)
                'End If
            End If

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
            '    MsgBox("No value has been modified")
            '    Exit Sub
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
            '    oTconn = New ADODB.Connection
            '   oTconn = TransactionOpen(gsConn)

            ReDim ByteArr(0)
            Dim ImageStream As New IO.MemoryStream
            If Not picImage.Image Is Nothing Then
                picImage.Image.Save(ImageStream, System.Drawing.Imaging.ImageFormat.Jpeg)
                ReDim ByteArr(CInt(ImageStream.Length - 1))
                ImageStream.Position = 0
                ImageStream.Read(ByteArr, 0, CInt(ImageStream.Length))
            Else
                ReDim ByteArr(0)
            End If

            Dim i As Long
            i = 0
            If ds.Tables(0).Rows.Count > 0 Then
                iMsg = MsgBox("Would to like to Save all Inventories " + " ?", vbYesNo)
                If iMsg = vbYes Then
                    Mode = "I"
                    For iCnt = 0 To ds.Tables(0).Rows.Count - 1
                        If Val(ds.Tables(0).Rows(iCnt)("QtySold") & "") = 0 Then
                            ''HAJ Inventory Modification on 01 Sep 2007
                            ' If objClsInventory.InventoryMnt(Trim(ds.Tables(0).Rows(iCnt)("Inventorycode") & ""), Trim(txtDescription1.Text), Trim(txtDiamondDescription1.Text), Trim(sPMCType), Trim(sSMCType), Trim(txtPMC1.Text), Trim(txtSMC1.Text), Trim(txtTotalCost1.Text), Trim(txtGoldLoss1.Text), Trim(txtDescription.Text), Trim(txtDiamondDescription.Text), Trim(sPMCType), Trim(sSMCType), Val(txtPMC.Text), Val(txtSMC.Text), Val(txtTotalCost.Text), Val(txtGoldLoss.Text), Val(txtSizeCode1.Text), Trim(sOldSizeType), Val(txtSizeCode.Text), Trim(sSizeType), sUserID) Then
                            If objClsInventory.InventoryMnt(Trim(ds.Tables(0).Rows(iCnt)("Inventorycode") & ""), Trim(txtDescription1.Text), Trim(txtDiamondDescription1.Text), Trim(sPMCType), Trim(sSMCType), Trim(txtPMC1.Text), Trim(txtSMC1.Text), _
                                                           Trim(txtTotalCost1.Text), Trim(txtGoldLoss1.Text), Trim(txtDescription.Text), Trim(txtDiamondDescription.Text), Trim(sPMCType), Trim(sSMCType), Val(txtPMC.Text), Val(txtSMC.Text), _
                                                           Val(txtTotalCost.Text), Val(txtGoldLoss.Text), Val(txtSizeCode1.Text), Trim(sOldSizeType), Val(txtSizeCode.Text), Trim(sSizeType), _
                                                           Trim(txtDesc1.Text), Trim(txtDesc2.Text), Trim(txtDesc3.Text), Trim(txtDesc4.Text), Val(txtCost1.Text), Val(txtCost2.Text), Val(txtCost3.Text), Val(txtCost4.Text), Val(txtSellingPrice1.Text), Val(txtSellingPrice2.Text), Val(txtSellingPrice3.Text), Val(txtSellingPrice4.Text), _
                                                           Trim(txtNewDesc1.Text), Trim(txtNewDesc2.Text), Trim(txtNewDesc3.Text), Trim(txtNewDesc4.Text), Val(txtNewCost1.Text), Val(txtNewCost2.Text), Val(txtNewCost3.Text), Val(txtNewCost4.Text), Val(txtNewSellingPrice1.Text), Val(txtNewSellingPrice2.Text), Val(txtNewSellingPrice3.Text), Val(txtNewSellingPrice4.Text), sUserID) Then
                                arrBarcode(i) = Trim(ds.Tables(0).Rows(iCnt)("Inventorycode") & "")
                                i = i + 1
                            Else
                                MsgBox("Transaction not saved")
                                'TransactionRollBack(oTconn)
                                Exit Sub
                            End If
                        ElseIf Val(ds.Tables(0).Rows(iCnt)("QtySold") & "") > 0 Then
                            ''HAJ Inventory Modification on 01 Sep 2007
                            If objClsInventory.InventoryMnt(Trim(ds.Tables(0).Rows(iCnt)("Inventorycode") & ""), Trim(txtDescription1.Text), Trim(txtDiamondDescription1.Text), Trim(sPMCType), Trim(sSMCType), Trim(txtPMC1.Text), Trim(txtSMC1.Text), _
                                                            Trim(txtTotalCost1.Text), Trim(txtGoldLoss1.Text), Trim(txtDescription.Text), Trim(txtDiamondDescription.Text), Trim(sPMCType), Trim(sSMCType), Val(txtPMC.Text), Val(txtSMC.Text), _
                                                            Val(txtTotalCost.Text), Val(txtGoldLoss.Text), Val(txtSizeCode1.Text), Trim(sOldSizeType), Val(txtSizeCode.Text), Trim(sSizeType), _
                                                            Trim(txtDesc1.Text), Trim(txtDesc2.Text), Trim(txtDesc3.Text), Trim(txtDesc4.Text), Val(txtCost1.Text), Val(txtCost2.Text), Val(txtCost3.Text), Val(txtCost4.Text), Val(txtSellingPrice1.Text), Val(txtSellingPrice2.Text), Val(txtSellingPrice3.Text), Val(txtSellingPrice4.Text), _
                                                            Trim(txtNewDesc1.Text), Trim(txtNewDesc2.Text), Trim(txtNewDesc3.Text), Trim(txtNewDesc4.Text), Val(txtNewCost1.Text), Val(txtNewCost2.Text), Val(txtNewCost3.Text), Val(txtNewCost4.Text), Val(txtNewSellingPrice1.Text), Val(txtNewSellingPrice2.Text), Val(txtNewSellingPrice3.Text), Val(txtNewSellingPrice4.Text), sUserID) Then
                                arrBarcode(i) = Trim(ds.Tables(0).Rows(iCnt)("Inventorycode") & "")
                                i = i + 1
                            Else
                                MsgBox("Transaction not saved", MsgBoxStyle.Information)
                                'TransactionRollBack(oTconn)
                                Exit Sub
                            End If
                        End If

                        gsConBatchPosting.ConnectionString = gsCon
                        gsConBatchPosting.Open()
                        gsTrans = gsConBatchPosting.BeginTransaction

                        If objClsInventory.InventoryImageSave(Trim(ds.Tables(0).Rows(iCnt)("Inventorycode") & ""), _
                       ByteArr, picImage.Name, sUserID) Then
                            gsTrans.Commit()
                        Else
                            MsgBox("Transaction not saved", MsgBoxStyle.Information)
                            'TransactionRollBack(oTconn)
                            gsTrans.Rollback()
                            Exit Sub
                        End If

                    Next
                    gsConBatchPosting.Close()
                    'TransactionCommit(oTconn)
                    '''Dim j As Long
                    '''If barCodeFlag = True Then
                    '''    For j = 0 To i
                    '''        printBarcode(arrBarcode(j))
                    '''    Next
                    '''End If
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
        Dim DsAdditional As DataSet
        If sCompanyShortCode.Trim = "ES" Or UCase(sCompanyShortCode.Trim) = "AS" Then
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
                txtWeight.Text = Val(Trim(ds.Tables(0).Rows(0)("Weight") & ""))
                txtGoldLoss.Text = Val(Trim(ds.Tables(0).Rows(0)("GoldLoss") & ""))
                txtCostPerGram.Text = Val(Trim(ds.Tables(0).Rows(0)("CostperGram") & ""))
                txtSizeCode.Text = Val(Trim(ds.Tables(0).Rows(0)("SizeCode") & ""))
                txtSizeCode1.Text = Val(Trim(ds.Tables(0).Rows(0)("SizeCode") & ""))
                sCategoryCode = Trim(ds.Tables(0).Rows(0)("CategoryCode") & "")
                sOldSizeType = Trim(ds.Tables(0).Rows(0)("SizeType") & "")
                sSizeType = Trim(ds.Tables(0).Rows(0)("SizeType") & "")
                'If Trim(ds.Tables(0).Rows(0)("PMCType") & "") = "W" Then
                '    txtPMC.Text = Format(Val(ds.Tables(0).Rows(0)("PMC") & "") / Val(ds.Tables(0).Rows(0)("Weight") & ""), "#0.0000")
                'Else
                '    txtPMC.Text = Format(Val(ds.Tables(0).Rows(0)("PMC") & "") / Val(ds.Tables(0).Rows(0)("Pcs") & ""), "#0.00")
                'End If
                txtPMC.Text = Format(Val(ds.Tables(0).Rows(0)("PMC") & ""), "#0.00")

                txtPCS.Text = CLng(Val(ds.Tables(0).Rows(0)("Pcs") & ""))
                txtTotalCost.Text = Format(Val(ds.Tables(0).Rows(0)("TotalCost") & ""), "0.00")

                txtWeight1.Text = Val(ds.Tables(0).Rows(0)("Weight") & "")
                txtCostPerGram1.Text = Val(ds.Tables(0).Rows(0)("CostperGram") & "")
                txtPMC1.Text = Val(ds.Tables(0).Rows(0)("PMC") & "")

                'If Trim(ds.Tables(0).Rows(0)("SMCType") & "") = "W" Then
                '    txtSMC.Text = Format(Val(ds.Tables(0).Rows(0)("sMc") & "") / Val(ds.Tables(0).Rows(0)("Weight") & ""), "#0.0000")
                'Else
                '    txtSMC.Text = Format(Val(ds.Tables(0).Rows(0)("sMc") & "") / Val(ds.Tables(0).Rows(0)("Pcs") & ""), "#0.0000")
                'End If
                txtSMC.Text = Format(Val(ds.Tables(0).Rows(0)("sMc") & ""), "#0.00")

                txtSMC1.Text = Val(ds.Tables(0).Rows(0)("sMc") & "")
                txtGoldLoss1.Text = Val(ds.Tables(0).Rows(0)("GoldLoss") & "")
                txtTotalCost1.Text = Val(ds.Tables(0).Rows(0)("TotalCost") & "")
                txtPCS1.Text = Val(ds.Tables(0).Rows(0)("Pcs") & "")

                If Trim(ds.Tables(0).Rows(0)("PMCType") & "") = "W" Then
                    optPMCW.Checked = True
                    optPMCP.Checked = False
                Else
                    optPMCP.Checked = True
                    optPMCW.Checked = False
                End If
                subGetPMC()
                'txtPMC_LostFocus(sender, e)
                If Trim(ds.Tables(0).Rows(0)("SMCType") & "") = "W" Then
                    optSMCW.Checked = True
                    optSMCP.Checked = False
                Else
                    optSMCP.Checked = True
                    optSMCW.Checked = False

                End If
                subGetSMC()
                'txtSMC_LostFocus(sender, e)

                Dim dsInventory As New DataSet
                Dim objClsInventory As New clsInventory
                dsInventory = objClsInventory.GetInventoryImage(Trim(sInventoryCode))
                If Not IsNothing(dsInventory) Then
                    If dsInventory.Tables("Inventory").Rows.Count > 0 Then
                        Byte2Image(picImage.Image, dsInventory.Tables("Inventory").Rows(0)("InventoryImage"))
                    End If
                End If

                DsAdditional = objClsInventory.GetAdditionalInventoryDetail(sInventoryCode.Trim)
                If DsAdditional.Tables("tblAddInventory").Rows.Count > 0 Then
                    txtDesc1.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Description1") & ""
                    txtDesc2.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Description2") & ""
                    txtDesc3.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Description3") & ""
                    txtDesc4.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Description4") & ""
                    txtCost1.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Cost1") & ""
                    txtCost2.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Cost2") & ""
                    txtCost3.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Cost3") & ""
                    txtCost4.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Cost4") & ""
                    txtSellingPrice1.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("SellingPrice1") & ""
                    txtSellingPrice2.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("SellingPrice2") & ""
                    txtSellingPrice3.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("SellingPrice3") & ""
                    txtSellingPrice4.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("SellingPrice4") & ""
                    txtNewDesc1.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Description1") & ""
                    txtNewDesc2.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Description2") & ""
                    txtNewDesc3.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Description3") & ""
                    txtNewDesc4.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Description4") & ""
                    txtNewCost1.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Cost1") & ""
                    txtNewCost2.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Cost2") & ""
                    txtNewCost3.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Cost3") & ""
                    txtNewCost4.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("Cost4") & ""
                    txtNewSellingPrice1.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("SellingPrice1") & ""
                    txtNewSellingPrice2.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("SellingPrice2") & ""
                    txtNewSellingPrice3.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("SellingPrice3") & ""
                    txtNewSellingPrice4.Text = DsAdditional.Tables("tblAddInventory").Rows(0)("SellingPrice4") & ""
                End If
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
    Public Sub Byte2Image(ByRef NewImage As Image, ByVal ByteArr() As Byte)
        Dim ImageStream As System.IO.MemoryStream
        Try
            If ByteArr.GetUpperBound(0) > 0 Then
                ImageStream = New MemoryStream(ByteArr)
                NewImage = System.Drawing.Image.FromStream(ImageStream)
            Else
                NewImage = Nothing
            End If
        Catch ex As Exception
            NewImage = Nothing
        End Try
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
                txtTotalCost.Text = Format(Val(txtWeight.Text) * Val(txtCostPerGram.Text) + Val(txtPMC.Text), "0.00")
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
        txtWeight1.Text = ""
        lblSMC.Text = ""
        lblPMC.Text = ""
        txtToInventoryCode.Text = ""
        txtSizeCode1.Text = ""
        txtSizeCode.Text = ""
        sSizeType = ""
        sOldSizeType = ""

        chkAdditionalDesc.Checked = False
        txtDesc1.Text = ""
        txtDesc2.Text = ""
        txtDesc3.Text = ""
        txtDesc4.Text = ""
        txtCost1.Text = ""
        txtCost2.Text = ""
        txtCost3.Text = ""
        txtCost4.Text = ""
        txtSellingPrice1.Text = ""
        txtSellingPrice2.Text = ""
        txtSellingPrice3.Text = ""
        txtSellingPrice4.Text = ""
        txtNewDesc1.Text = ""
        txtNewDesc2.Text = ""
        txtNewDesc3.Text = ""
        txtNewDesc4.Text = ""
        txtNewCost1.Text = ""
        txtNewCost2.Text = ""
        txtNewCost3.Text = ""
        txtNewCost4.Text = ""
        txtNewSellingPrice1.Text = ""
        txtNewSellingPrice2.Text = ""
        txtNewSellingPrice3.Text = ""
        txtNewSellingPrice4.Text = ""
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


    Private Sub txtfrInventoryCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtfrInventoryCode.TextChanged

    End Sub


    Private Sub txtPMC_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPMC.Leave
        If Trim(sCompanyShortCode) = "SSJ" Then
            lblPMC.Text = txtPMC.Text
            subTotal()
        End If
    End Sub

    Private Sub btnSizeCode_Click(sender As System.Object, e As System.EventArgs) Handles btnSizeCode.Click
        Try
            Dim e1 As New System.EventArgs
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSize", "L", 4, "SizeCode", "categorycode=" & fncFormatSQLText(sCategoryCode), "SizeCode", "SizeCode", 80, "SizeType", "SizeType", 100, "CategoryCode", "CategoryCode", 100, "Remarks", "Remarks", 100)
            frmHelpClass.ShowDialog()
            txtSizeCode.Text = frmHelpClass.Var1.Trim
            txtSizeCode_LostFocus(sender, e1)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtSizeCode_LostFocus(sender As Object, e As System.EventArgs) Handles txtSizeCode.LostFocus
        Dim Ds As DataSet
        Dim objSize As New ClsCatalogue
        Try
            If txtSizeCode.Text.Trim <> "" Then
                Ds = objSize.GetSizewithCode(txtSizeCode.Text.Trim, Trim(sCategoryCode))
                If Ds.Tables("Size").Rows.Count <= 0 Then
                    MsgBox("Invalid Size Code!", MsgBoxStyle.Critical)
                    If txtSizeCode.Enabled Then txtSizeCode.Focus()
                    Exit Sub
                Else
                    txtSizeCode.Text = Trim(Ds.Tables("Size").Rows(0)("SizeCode") & "")
                    sSizeType = Trim(Ds.Tables("Size").Rows(0)("SizeType") & "")
                    'If txtGoldLoss.Enabled Then txtGoldLoss.Focus()
                End If
            End If
            Ds = Nothing
            objSize = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub cmdAddlDescOk_Click(sender As System.Object, e As System.EventArgs) Handles cmdAddlDescOk.Click
        grpAddlDescription.Visible = False
    End Sub

    Private Sub chkAdditionalDesc_Click(sender As Object, e As System.EventArgs) Handles chkAdditionalDesc.Click
        If chkAdditionalDesc.Checked = True Then
            grpAddlDescription.Visible = True
        Else
            grpAddlDescription.Visible = False
        End If
    End Sub

    Private Sub txtSizeCode_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtSizeCode.TextChanged

    End Sub

    Private Sub chkImage_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkImage.CheckedChanged
        If chkImage.Checked = True Then
            grpImage.Visible = True
        Else
            grpImage.Visible = False
        End If
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        grpImage.Visible = False
    End Sub

    Private Sub btnBrowseImage_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowseImage.Click
        Dim OpenFileDialog1 As New OpenFileDialog


        With OpenFileDialog1
            .CheckFileExists = True
            .ShowReadOnly = False
            ' .Filter = "All Files|*.*|Bitmap Files (*)|*.bmp;*.gif;*.jpg"
            .Filter = "All Files|*.*|JPEG Files (*)|*.jpg"
            .FilterIndex = 2

            If .ShowDialog = DialogResult.OK Then
                ' Load the specified file into a PictureBox control.

                picImage.Image = Image.FromFile(.FileName)
                AutosizeImage(.FileName, picImage)
                picImage.Name = .SafeFileName
            End If
        End With
    End Sub

    Public Sub AutosizeImage(ByVal ImagePath As String, ByVal picBox As PictureBox, Optional ByVal pSizeMode As PictureBoxSizeMode = PictureBoxSizeMode.CenterImage)
        Try
            picBox.Image = Nothing
            picBox.SizeMode = pSizeMode
            If System.IO.File.Exists(ImagePath) Then
                Dim imgOrg As Bitmap
                Dim imgShow As Bitmap
                Dim g As Graphics
                Dim divideBy, divideByH, divideByW As Double
                imgOrg = DirectCast(Bitmap.FromFile(ImagePath), Bitmap)

                divideByW = imgOrg.Width / picBox.Width
                divideByH = imgOrg.Height / picBox.Height
                If divideByW > 1 Or divideByH > 1 Then
                    If divideByW > divideByH Then
                        divideBy = divideByW
                    Else
                        divideBy = divideByH
                    End If

                    imgShow = New Bitmap(CInt(CDbl(imgOrg.Width) / divideBy), CInt(CDbl(imgOrg.Height) / divideBy))
                    imgShow.SetResolution(imgOrg.HorizontalResolution, imgOrg.VerticalResolution)
                    g = Graphics.FromImage(imgShow)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    g.DrawImage(imgOrg, New Rectangle(0, 0, CInt(CDbl(imgOrg.Width) / divideBy), CInt(CDbl(imgOrg.Height) / divideBy)), 0, 0, imgOrg.Width, imgOrg.Height, GraphicsUnit.Pixel)
                    g.Dispose()
                Else
                    imgShow = New Bitmap(imgOrg.Width, imgOrg.Height)
                    imgShow.SetResolution(imgOrg.HorizontalResolution, imgOrg.VerticalResolution)
                    g = Graphics.FromImage(imgShow)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    g.DrawImage(imgOrg, New Rectangle(0, 0, imgOrg.Width, imgOrg.Height), 0, 0, imgOrg.Width, imgOrg.Height, GraphicsUnit.Pixel)
                    g.Dispose()
                End If
                imgOrg.Dispose()

                picBox.Image = imgShow

            Else
                picBox.Image = Nothing
            End If


        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try


    End Sub

    Private Sub chkAdditionalDesc_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkAdditionalDesc.CheckedChanged

    End Sub
End Class
