
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmSplitInventory
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
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnInventoryCodeSearch As System.Windows.Forms.Button
    Friend WithEvents txtInventoryCode As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnSplit As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtOriginalInvCode As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtSplitWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtOriginalInvWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtSplitInvWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtSplitInvCode As System.Windows.Forms.TextBox
    Friend WithEvents txtNewDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents cboCategory As System.Windows.Forms.ComboBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents chkScrap As System.Windows.Forms.CheckBox
    Friend WithEvents cboOrgCategory As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cboOrgCatalogue As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cboCatalogue As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtorgDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents btnOrgSizeCode As System.Windows.Forms.Button
    Friend WithEvents txtOrgSizeCode As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents btnNewSizeCode As System.Windows.Forms.Button
    Friend WithEvents txtNewSizeCode As System.Windows.Forms.TextBox
    Friend WithEvents txtOriginalSMC As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtNewSMC As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents chkIPM As System.Windows.Forms.CheckBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtOriginalStWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtSplitStWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtSMC As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtPMC As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtStoneSplitWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtStoneWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents chkAdditionalDesc As System.Windows.Forms.CheckBox
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
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents btnBarcode As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSplitInventory))
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtStoneSplitWeight = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtStoneWeight = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtSMC = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtPMC = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.btnSplit = New System.Windows.Forms.Button()
        Me.txtSplitWeight = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtWeight = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtInventoryCode = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnInventoryCodeSearch = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtOriginalSMC = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.btnOrgSizeCode = New System.Windows.Forms.Button()
        Me.txtOrgSizeCode = New System.Windows.Forms.TextBox()
        Me.txtorgDescription = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cboOrgCatalogue = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cboOrgCategory = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtOriginalInvWeight = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtOriginalInvCode = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chkIPM = New System.Windows.Forms.CheckBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtNewSMC = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.btnNewSizeCode = New System.Windows.Forms.Button()
        Me.txtNewSizeCode = New System.Windows.Forms.TextBox()
        Me.cboCatalogue = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.chkScrap = New System.Windows.Forms.CheckBox()
        Me.cboCategory = New System.Windows.Forms.ComboBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtNewDescription = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtSplitInvWeight = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtSplitInvCode = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnBarcode = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.txtOriginalStWeight = New System.Windows.Forms.TextBox()
        Me.txtSplitStWeight = New System.Windows.Forms.TextBox()
        Me.chkAdditionalDesc = New System.Windows.Forms.CheckBox()
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
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpAddlDescription.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.Location = New System.Drawing.Point(364, 611)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(100, 40)
        Me.btnClear.TabIndex = 25
        Me.btnClear.Text = "Clear(F6)"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(470, 611)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 27
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txtStoneSplitWeight)
        Me.Panel1.Controls.Add(Me.Label22)
        Me.Panel1.Controls.Add(Me.txtStoneWeight)
        Me.Panel1.Controls.Add(Me.Label23)
        Me.Panel1.Controls.Add(Me.txtSMC)
        Me.Panel1.Controls.Add(Me.Label21)
        Me.Panel1.Controls.Add(Me.txtPMC)
        Me.Panel1.Controls.Add(Me.Label20)
        Me.Panel1.Controls.Add(Me.btnSplit)
        Me.Panel1.Controls.Add(Me.txtSplitWeight)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.txtWeight)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtDescription)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtInventoryCode)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.btnInventoryCodeSearch)
        Me.Panel1.Location = New System.Drawing.Point(10, 112)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(560, 157)
        Me.Panel1.TabIndex = 42
        '
        'txtStoneSplitWeight
        '
        Me.txtStoneSplitWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStoneSplitWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStoneSplitWeight.Location = New System.Drawing.Point(285, 112)
        Me.txtStoneSplitWeight.MaxLength = 20
        Me.txtStoneSplitWeight.Name = "txtStoneSplitWeight"
        Me.txtStoneSplitWeight.Size = New System.Drawing.Size(97, 20)
        Me.txtStoneSplitWeight.TabIndex = 62
        Me.txtStoneSplitWeight.Tag = "NE;"
        '
        'Label22
        '
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(213, 108)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(72, 33)
        Me.Label22.TabIndex = 64
        Me.Label22.Text = "Stone SplitWeight"
        '
        'txtStoneWeight
        '
        Me.txtStoneWeight.BackColor = System.Drawing.Color.White
        Me.txtStoneWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStoneWeight.Enabled = False
        Me.txtStoneWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStoneWeight.ForeColor = System.Drawing.Color.Black
        Me.txtStoneWeight.Location = New System.Drawing.Point(103, 112)
        Me.txtStoneWeight.MaxLength = 20
        Me.txtStoneWeight.Name = "txtStoneWeight"
        Me.txtStoneWeight.Size = New System.Drawing.Size(97, 20)
        Me.txtStoneWeight.TabIndex = 61
        Me.txtStoneWeight.Tag = "NE;"
        '
        'Label23
        '
        Me.Label23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(8, 112)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(64, 28)
        Me.Label23.TabIndex = 63
        Me.Label23.Text = "Stone Weight"
        '
        'txtSMC
        '
        Me.txtSMC.BackColor = System.Drawing.Color.White
        Me.txtSMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSMC.Enabled = False
        Me.txtSMC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMC.ForeColor = System.Drawing.Color.Black
        Me.txtSMC.Location = New System.Drawing.Point(285, 82)
        Me.txtSMC.MaxLength = 20
        Me.txtSMC.Name = "txtSMC"
        Me.txtSMC.Size = New System.Drawing.Size(97, 20)
        Me.txtSMC.TabIndex = 59
        Me.txtSMC.Tag = "NE;"
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(215, 86)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(64, 16)
        Me.Label21.TabIndex = 60
        Me.Label21.Text = "SMC"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPMC
        '
        Me.txtPMC.BackColor = System.Drawing.Color.White
        Me.txtPMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPMC.Enabled = False
        Me.txtPMC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPMC.ForeColor = System.Drawing.Color.Black
        Me.txtPMC.Location = New System.Drawing.Point(104, 82)
        Me.txtPMC.MaxLength = 20
        Me.txtPMC.Name = "txtPMC"
        Me.txtPMC.Size = New System.Drawing.Size(97, 20)
        Me.txtPMC.TabIndex = 57
        Me.txtPMC.Tag = "NE;"
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(9, 86)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(64, 16)
        Me.Label20.TabIndex = 58
        Me.Label20.Text = "PMC"
        '
        'btnSplit
        '
        Me.btnSplit.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSplit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSplit.Location = New System.Drawing.Point(407, 86)
        Me.btnSplit.Name = "btnSplit"
        Me.btnSplit.Size = New System.Drawing.Size(80, 43)
        Me.btnSplit.TabIndex = 5
        Me.btnSplit.Text = "Split"
        Me.btnSplit.UseVisualStyleBackColor = False
        '
        'txtSplitWeight
        '
        Me.txtSplitWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSplitWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSplitWeight.Location = New System.Drawing.Point(285, 56)
        Me.txtSplitWeight.MaxLength = 20
        Me.txtSplitWeight.Name = "txtSplitWeight"
        Me.txtSplitWeight.Size = New System.Drawing.Size(97, 20)
        Me.txtSplitWeight.TabIndex = 4
        Me.txtSplitWeight.Tag = "NE;"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(213, 56)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(72, 16)
        Me.Label8.TabIndex = 56
        Me.Label8.Text = "SplitWeight"
        '
        'txtWeight
        '
        Me.txtWeight.BackColor = System.Drawing.Color.White
        Me.txtWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWeight.Enabled = False
        Me.txtWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeight.ForeColor = System.Drawing.Color.Black
        Me.txtWeight.Location = New System.Drawing.Point(103, 56)
        Me.txtWeight.MaxLength = 20
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.Size = New System.Drawing.Size(97, 20)
        Me.txtWeight.TabIndex = 3
        Me.txtWeight.Tag = "NE;"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 60)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 16)
        Me.Label5.TabIndex = 52
        Me.Label5.Text = "Weight"
        '
        'txtDescription
        '
        Me.txtDescription.BackColor = System.Drawing.Color.White
        Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescription.Enabled = False
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.ForeColor = System.Drawing.Color.Black
        Me.txtDescription.Location = New System.Drawing.Point(285, 24)
        Me.txtDescription.MaxLength = 50
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(202, 20)
        Me.txtDescription.TabIndex = 2
        Me.txtDescription.Tag = "NE;"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(213, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 16)
        Me.Label2.TabIndex = 40
        Me.Label2.Text = "Description "
        '
        'txtInventoryCode
        '
        Me.txtInventoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInventoryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInventoryCode.Location = New System.Drawing.Point(103, 24)
        Me.txtInventoryCode.MaxLength = 20
        Me.txtInventoryCode.Name = "txtInventoryCode"
        Me.txtInventoryCode.Size = New System.Drawing.Size(96, 20)
        Me.txtInventoryCode.TabIndex = 0
        Me.txtInventoryCode.Tag = "NE;"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 16)
        Me.Label1.TabIndex = 38
        Me.Label1.Text = "Inventory Code"
        '
        'btnInventoryCodeSearch
        '
        Me.btnInventoryCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnInventoryCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInventoryCodeSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInventoryCodeSearch.Location = New System.Drawing.Point(175, 24)
        Me.btnInventoryCodeSearch.Name = "btnInventoryCodeSearch"
        Me.btnInventoryCodeSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnInventoryCodeSearch.TabIndex = 1
        Me.btnInventoryCodeSearch.Text = "..."
        Me.btnInventoryCodeSearch.UseVisualStyleBackColor = False
        Me.btnInventoryCodeSearch.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.txtOriginalSMC)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.btnOrgSizeCode)
        Me.GroupBox1.Controls.Add(Me.txtOrgSizeCode)
        Me.GroupBox1.Controls.Add(Me.txtorgDescription)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.cboOrgCatalogue)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.cboOrgCategory)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtOriginalInvWeight)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtOriginalInvCode)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Navy
        Me.GroupBox1.Location = New System.Drawing.Point(10, 275)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(270, 330)
        Me.GroupBox1.TabIndex = 44
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Original Item"
        '
        'txtOriginalSMC
        '
        Me.txtOriginalSMC.BackColor = System.Drawing.Color.White
        Me.txtOriginalSMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOriginalSMC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOriginalSMC.ForeColor = System.Drawing.Color.Black
        Me.txtOriginalSMC.Location = New System.Drawing.Point(11, 260)
        Me.txtOriginalSMC.MaxLength = 20
        Me.txtOriginalSMC.Name = "txtOriginalSMC"
        Me.txtOriginalSMC.Size = New System.Drawing.Size(104, 20)
        Me.txtOriginalSMC.TabIndex = 12
        Me.txtOriginalSMC.Tag = "NE;"
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(11, 241)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(89, 16)
        Me.Label16.TabIndex = 409
        Me.Label16.Text = "SMC"
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(11, 283)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(89, 16)
        Me.Label14.TabIndex = 407
        Me.Label14.Text = "Size"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(182, 109)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(82, 19)
        Me.Label18.TabIndex = 116
        Me.Label18.Text = "Stone Weight"
        '
        'btnOrgSizeCode
        '
        Me.btnOrgSizeCode.BackColor = System.Drawing.Color.Orange
        Me.btnOrgSizeCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOrgSizeCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOrgSizeCode.Location = New System.Drawing.Point(204, 302)
        Me.btnOrgSizeCode.Name = "btnOrgSizeCode"
        Me.btnOrgSizeCode.Size = New System.Drawing.Size(30, 22)
        Me.btnOrgSizeCode.TabIndex = 14
        Me.btnOrgSizeCode.Text = "..."
        Me.btnOrgSizeCode.UseVisualStyleBackColor = False
        '
        'txtOrgSizeCode
        '
        Me.txtOrgSizeCode.BackColor = System.Drawing.Color.White
        Me.txtOrgSizeCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOrgSizeCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOrgSizeCode.ForeColor = System.Drawing.Color.Navy
        Me.txtOrgSizeCode.Location = New System.Drawing.Point(11, 302)
        Me.txtOrgSizeCode.MaxLength = 21
        Me.txtOrgSizeCode.Name = "txtOrgSizeCode"
        Me.txtOrgSizeCode.ReadOnly = True
        Me.txtOrgSizeCode.Size = New System.Drawing.Size(222, 22)
        Me.txtOrgSizeCode.TabIndex = 13
        '
        'txtorgDescription
        '
        Me.txtorgDescription.BackColor = System.Drawing.Color.White
        Me.txtorgDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtorgDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtorgDescription.ForeColor = System.Drawing.Color.Black
        Me.txtorgDescription.Location = New System.Drawing.Point(10, 204)
        Me.txtorgDescription.MaxLength = 50
        Me.txtorgDescription.Name = "txtorgDescription"
        Me.txtorgDescription.Size = New System.Drawing.Size(224, 20)
        Me.txtorgDescription.TabIndex = 11
        Me.txtorgDescription.Tag = "NE;"
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(10, 185)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(120, 16)
        Me.Label13.TabIndex = 56
        Me.Label13.Text = "New Description"
        '
        'cboOrgCatalogue
        '
        Me.cboOrgCatalogue.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboOrgCatalogue.Location = New System.Drawing.Point(106, 66)
        Me.cboOrgCatalogue.Name = "cboOrgCatalogue"
        Me.cboOrgCatalogue.Size = New System.Drawing.Size(158, 22)
        Me.cboOrgCatalogue.TabIndex = 7
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(10, 66)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(89, 16)
        Me.Label11.TabIndex = 54
        Me.Label11.Text = "Catalogue"
        '
        'cboOrgCategory
        '
        Me.cboOrgCategory.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboOrgCategory.Location = New System.Drawing.Point(106, 20)
        Me.cboOrgCategory.Name = "cboOrgCategory"
        Me.cboOrgCategory.Size = New System.Drawing.Size(158, 23)
        Me.cboOrgCategory.TabIndex = 6
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(10, 20)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(89, 16)
        Me.Label10.TabIndex = 52
        Me.Label10.Text = "Category"
        '
        'txtOriginalInvWeight
        '
        Me.txtOriginalInvWeight.BackColor = System.Drawing.Color.White
        Me.txtOriginalInvWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOriginalInvWeight.Enabled = False
        Me.txtOriginalInvWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOriginalInvWeight.ForeColor = System.Drawing.Color.Black
        Me.txtOriginalInvWeight.Location = New System.Drawing.Point(105, 128)
        Me.txtOriginalInvWeight.MaxLength = 20
        Me.txtOriginalInvWeight.Name = "txtOriginalInvWeight"
        Me.txtOriginalInvWeight.Size = New System.Drawing.Size(71, 20)
        Me.txtOriginalInvWeight.TabIndex = 9
        Me.txtOriginalInvWeight.Tag = "NE;"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(105, 109)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 16)
        Me.Label4.TabIndex = 42
        Me.Label4.Text = "Weight"
        '
        'txtOriginalInvCode
        '
        Me.txtOriginalInvCode.BackColor = System.Drawing.Color.White
        Me.txtOriginalInvCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOriginalInvCode.Enabled = False
        Me.txtOriginalInvCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOriginalInvCode.ForeColor = System.Drawing.Color.Black
        Me.txtOriginalInvCode.Location = New System.Drawing.Point(10, 128)
        Me.txtOriginalInvCode.MaxLength = 20
        Me.txtOriginalInvCode.Name = "txtOriginalInvCode"
        Me.txtOriginalInvCode.Size = New System.Drawing.Size(85, 20)
        Me.txtOriginalInvCode.TabIndex = 8
        Me.txtOriginalInvCode.Tag = "NE;"
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(10, 109)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(89, 16)
        Me.Label9.TabIndex = 40
        Me.Label9.Text = "Inventory Code"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GroupBox2.Controls.Add(Me.chkIPM)
        Me.GroupBox2.Controls.Add(Me.Label19)
        Me.GroupBox2.Controls.Add(Me.txtNewSMC)
        Me.GroupBox2.Controls.Add(Me.Label17)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.btnNewSizeCode)
        Me.GroupBox2.Controls.Add(Me.txtNewSizeCode)
        Me.GroupBox2.Controls.Add(Me.cboCatalogue)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.chkScrap)
        Me.GroupBox2.Controls.Add(Me.cboCategory)
        Me.GroupBox2.Controls.Add(Me.Label26)
        Me.GroupBox2.Controls.Add(Me.txtNewDescription)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.txtSplitInvWeight)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.txtSplitInvCode)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.Navy
        Me.GroupBox2.Location = New System.Drawing.Point(287, 275)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(283, 330)
        Me.GroupBox2.TabIndex = 45
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Split Item"
        '
        'chkIPM
        '
        Me.chkIPM.ForeColor = System.Drawing.Color.Black
        Me.chkIPM.Location = New System.Drawing.Point(149, 260)
        Me.chkIPM.Name = "chkIPM"
        Me.chkIPM.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkIPM.Size = New System.Drawing.Size(62, 16)
        Me.chkIPM.TabIndex = 413
        Me.chkIPM.Text = "IPM"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(185, 106)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(94, 19)
        Me.Label19.TabIndex = 118
        Me.Label19.Text = "Stone Weight"
        '
        'txtNewSMC
        '
        Me.txtNewSMC.BackColor = System.Drawing.Color.White
        Me.txtNewSMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewSMC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewSMC.ForeColor = System.Drawing.Color.Black
        Me.txtNewSMC.Location = New System.Drawing.Point(13, 260)
        Me.txtNewSMC.MaxLength = 20
        Me.txtNewSMC.Name = "txtNewSMC"
        Me.txtNewSMC.Size = New System.Drawing.Size(104, 20)
        Me.txtNewSMC.TabIndex = 21
        Me.txtNewSMC.Tag = "NE;"
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(13, 241)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(89, 16)
        Me.Label17.TabIndex = 412
        Me.Label17.Text = "SMC"
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(11, 283)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(89, 16)
        Me.Label15.TabIndex = 410
        Me.Label15.Text = "Size"
        '
        'btnNewSizeCode
        '
        Me.btnNewSizeCode.BackColor = System.Drawing.Color.Orange
        Me.btnNewSizeCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNewSizeCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNewSizeCode.Location = New System.Drawing.Point(204, 302)
        Me.btnNewSizeCode.Name = "btnNewSizeCode"
        Me.btnNewSizeCode.Size = New System.Drawing.Size(30, 22)
        Me.btnNewSizeCode.TabIndex = 23
        Me.btnNewSizeCode.Text = "..."
        Me.btnNewSizeCode.UseVisualStyleBackColor = False
        '
        'txtNewSizeCode
        '
        Me.txtNewSizeCode.BackColor = System.Drawing.Color.White
        Me.txtNewSizeCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewSizeCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewSizeCode.ForeColor = System.Drawing.Color.Navy
        Me.txtNewSizeCode.Location = New System.Drawing.Point(11, 302)
        Me.txtNewSizeCode.MaxLength = 21
        Me.txtNewSizeCode.Name = "txtNewSizeCode"
        Me.txtNewSizeCode.ReadOnly = True
        Me.txtNewSizeCode.Size = New System.Drawing.Size(222, 22)
        Me.txtNewSizeCode.TabIndex = 22
        '
        'cboCatalogue
        '
        Me.cboCatalogue.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCatalogue.Location = New System.Drawing.Point(109, 66)
        Me.cboCatalogue.Name = "cboCatalogue"
        Me.cboCatalogue.Size = New System.Drawing.Size(168, 22)
        Me.cboCatalogue.TabIndex = 16
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(13, 66)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(89, 16)
        Me.Label12.TabIndex = 56
        Me.Label12.Text = "Catalogue"
        '
        'chkScrap
        '
        Me.chkScrap.ForeColor = System.Drawing.Color.Black
        Me.chkScrap.Location = New System.Drawing.Point(6, 39)
        Me.chkScrap.Name = "chkScrap"
        Me.chkScrap.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkScrap.Size = New System.Drawing.Size(62, 16)
        Me.chkScrap.TabIndex = 51
        Me.chkScrap.Text = "Scrap"
        '
        'cboCategory
        '
        Me.cboCategory.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCategory.Location = New System.Drawing.Point(109, 20)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.Size = New System.Drawing.Size(168, 23)
        Me.cboCategory.TabIndex = 15
        '
        'Label26
        '
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.Black
        Me.Label26.Location = New System.Drawing.Point(13, 20)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(89, 16)
        Me.Label26.TabIndex = 50
        Me.Label26.Text = "Category"
        '
        'txtNewDescription
        '
        Me.txtNewDescription.BackColor = System.Drawing.Color.White
        Me.txtNewDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewDescription.Enabled = False
        Me.txtNewDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewDescription.ForeColor = System.Drawing.Color.Black
        Me.txtNewDescription.Location = New System.Drawing.Point(13, 204)
        Me.txtNewDescription.MaxLength = 50
        Me.txtNewDescription.Name = "txtNewDescription"
        Me.txtNewDescription.Size = New System.Drawing.Size(224, 20)
        Me.txtNewDescription.TabIndex = 20
        Me.txtNewDescription.Tag = "NE;"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(13, 185)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(120, 16)
        Me.Label7.TabIndex = 48
        Me.Label7.Text = "New Description"
        '
        'txtSplitInvWeight
        '
        Me.txtSplitInvWeight.BackColor = System.Drawing.Color.White
        Me.txtSplitInvWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSplitInvWeight.Enabled = False
        Me.txtSplitInvWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSplitInvWeight.ForeColor = System.Drawing.Color.Black
        Me.txtSplitInvWeight.Location = New System.Drawing.Point(109, 128)
        Me.txtSplitInvWeight.MaxLength = 20
        Me.txtSplitInvWeight.Name = "txtSplitInvWeight"
        Me.txtSplitInvWeight.Size = New System.Drawing.Size(73, 20)
        Me.txtSplitInvWeight.TabIndex = 18
        Me.txtSplitInvWeight.Tag = "NE;"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(109, 106)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 19)
        Me.Label3.TabIndex = 46
        Me.Label3.Text = "Weight"
        '
        'txtSplitInvCode
        '
        Me.txtSplitInvCode.BackColor = System.Drawing.Color.White
        Me.txtSplitInvCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSplitInvCode.Enabled = False
        Me.txtSplitInvCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSplitInvCode.ForeColor = System.Drawing.Color.Black
        Me.txtSplitInvCode.Location = New System.Drawing.Point(13, 128)
        Me.txtSplitInvCode.MaxLength = 20
        Me.txtSplitInvCode.Name = "txtSplitInvCode"
        Me.txtSplitInvCode.Size = New System.Drawing.Size(87, 20)
        Me.txtSplitInvCode.TabIndex = 17
        Me.txtSplitInvCode.Tag = "NE;"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(13, 106)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(89, 16)
        Me.Label6.TabIndex = 44
        Me.Label6.Text = "Inventory Code"
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(258, 611)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 40)
        Me.btnSave.TabIndex = 24
        Me.btnSave.Text = "Update(F7)"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnBarcode
        '
        Me.btnBarcode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnBarcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBarcode.Location = New System.Drawing.Point(152, 611)
        Me.btnBarcode.Name = "btnBarcode"
        Me.btnBarcode.Size = New System.Drawing.Size(100, 40)
        Me.btnBarcode.TabIndex = 28
        Me.btnBarcode.Text = "Barcode"
        Me.btnBarcode.UseVisualStyleBackColor = False
        Me.btnBarcode.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(10, 6)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(270, 100)
        Me.PictureBox1.TabIndex = 114
        Me.PictureBox1.TabStop = False
        '
        'txtOriginalStWeight
        '
        Me.txtOriginalStWeight.BackColor = System.Drawing.Color.White
        Me.txtOriginalStWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOriginalStWeight.Enabled = False
        Me.txtOriginalStWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOriginalStWeight.ForeColor = System.Drawing.Color.Black
        Me.txtOriginalStWeight.Location = New System.Drawing.Point(195, 403)
        Me.txtOriginalStWeight.MaxLength = 20
        Me.txtOriginalStWeight.Name = "txtOriginalStWeight"
        Me.txtOriginalStWeight.Size = New System.Drawing.Size(79, 20)
        Me.txtOriginalStWeight.TabIndex = 10
        Me.txtOriginalStWeight.Tag = "NE;"
        '
        'txtSplitStWeight
        '
        Me.txtSplitStWeight.BackColor = System.Drawing.Color.White
        Me.txtSplitStWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSplitStWeight.Enabled = False
        Me.txtSplitStWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSplitStWeight.ForeColor = System.Drawing.Color.Black
        Me.txtSplitStWeight.Location = New System.Drawing.Point(475, 403)
        Me.txtSplitStWeight.MaxLength = 20
        Me.txtSplitStWeight.Name = "txtSplitStWeight"
        Me.txtSplitStWeight.Size = New System.Drawing.Size(89, 20)
        Me.txtSplitStWeight.TabIndex = 19
        Me.txtSplitStWeight.Tag = "NE;"
        '
        'chkAdditionalDesc
        '
        Me.chkAdditionalDesc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAdditionalDesc.ForeColor = System.Drawing.Color.Black
        Me.chkAdditionalDesc.Location = New System.Drawing.Point(12, 616)
        Me.chkAdditionalDesc.Name = "chkAdditionalDesc"
        Me.chkAdditionalDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAdditionalDesc.Size = New System.Drawing.Size(68, 31)
        Me.chkAdditionalDesc.TabIndex = 524
        Me.chkAdditionalDesc.Text = "Addl. Desc"
        '
        'grpAddlDescription
        '
        Me.grpAddlDescription.Controls.Add(Me.GroupBox9)
        Me.grpAddlDescription.Controls.Add(Me.GroupBox7)
        Me.grpAddlDescription.Controls.Add(Me.Label42)
        Me.grpAddlDescription.Controls.Add(Me.Label41)
        Me.grpAddlDescription.Controls.Add(Me.cmdAddlDescOk)
        Me.grpAddlDescription.Controls.Add(Me.Label40)
        Me.grpAddlDescription.Controls.Add(Me.Label43)
        Me.grpAddlDescription.Controls.Add(Me.Label44)
        Me.grpAddlDescription.Controls.Add(Me.Label45)
        Me.grpAddlDescription.Location = New System.Drawing.Point(-25, 460)
        Me.grpAddlDescription.Name = "grpAddlDescription"
        Me.grpAddlDescription.Size = New System.Drawing.Size(445, 325)
        Me.grpAddlDescription.TabIndex = 525
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
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label40.Location = New System.Drawing.Point(12, 16)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(71, 13)
        Me.Label40.TabIndex = 515
        Me.Label40.Text = "Description"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label43.Location = New System.Drawing.Point(19, 99)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(78, 13)
        Me.Label43.TabIndex = 518
        Me.Label43.Text = "Description4"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label43.Visible = False
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label44.Location = New System.Drawing.Point(20, 73)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(78, 13)
        Me.Label44.TabIndex = 517
        Me.Label44.Text = "Description3"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label44.Visible = False
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label45.Location = New System.Drawing.Point(20, 47)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(78, 13)
        Me.Label45.TabIndex = 516
        Me.Label45.Text = "Description2"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label45.Visible = False
        '
        'frmSplitInventory
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(582, 663)
        Me.ControlBox = False
        Me.Controls.Add(Me.grpAddlDescription)
        Me.Controls.Add(Me.chkAdditionalDesc)
        Me.Controls.Add(Me.txtSplitStWeight)
        Me.Controls.Add(Me.txtOriginalStWeight)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnBarcode)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmSplitInventory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Split & Return Inventory"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpAddlDescription.ResumeLayout(False)
        Me.grpAddlDescription.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region
    Dim objClsSplit As New ClsSliptInventory
    Dim objClsInvMerge As New clsMergeInventory
    Dim sClass As String
    Dim sMcCost As Double
    Dim sMcPrice As Double
    Dim sTotalCost As Double
    Dim MCTitle As String
    Dim dWeight1 As Double
    Dim dWeight2 As Double
    Dim dStoneWeight1 As Double
    Dim dStoneWeight2 As Double
    Dim dMcCost1 As Double
    Dim dMcCost2 As Double
    Dim dMcPrice1 As Double
    Dim dMcPrice2 As Double
    Dim dOriginalPMC As Double
    Dim dOriginalSMC As Double
    Dim dOriginalGoldLoss As Double
    Dim dOriginalTotalCost As Double
    Dim oTconn As ADODB.Connection
    Dim dsT As DataSet
    Dim dTotalCost1 As Double
    Dim dTotalcost2 As Double
    Dim sTrayId As String
    Dim sCatalogueCode As String
    Dim sCategoryCode As String
    Dim dCostPerGram As Double
    Dim dPMC As Double
    Dim dSMC As Double
    Dim dPCS As Double
    Dim dGoldLoss As Double
    Dim dStoneWeight As Double
    Dim sJobsheetNo As String
    Dim sPurchaseBillNo As String
    Dim dPurchaseDate As Date
    Dim dCreateDate As Date
    Dim sSupplierCode As String
    Dim sSalesBillNo As String
    Dim sDiamondDescription As String
    Dim dTotalCost As Double
    'Dim sCategoryCode As String
    Dim sClassCode As String
    'Dim sCatalogueCode As String
    Dim sPMCType As String
    Dim sSMCType As String
    Dim sRemarks As String
    Dim masPriceCode$(9)
    Dim objinv As New clsInventory
    Dim objCls As New ClsClass
    Dim bPrntBarcode As Boolean
    Dim sOrgSizeType As String
    Dim sNewSizeType As String
    Dim sSplitCategory As String

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmSplitInventory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtInventoryCode.Focus()
        bPrntBarcode = False
    End Sub

    Private Sub btnInventoryCodeSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInventoryCodeSearch.Click
        'subShowInventoryHelpWindow()
    End Sub
    Private Sub subShowInventoryHelpWindow()
        Try
            'Dim frmInvHelp As New frmHelpSearch(0, 0, "tblInventory", "L", 4, "Description", " QtyOnHand <> 0 ", "InventoryCode", "InventoryCode", 80, "Description", "Description", 150, "LocationCode", "LocationCode", 50, "Weight", "Weight", 80)
            'frmInvHelp.ShowDialog()
            'txtInventoryCode.Text = frmInvHelp.Var1.Trim
            'txtDescription.Text = frmInvHelp.Var2.Trim
            'txtWeight.Text = frmInvHelp.Var3.Trim
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subGetInventoryDetail()
        Dim DsAdditional As DataSet
        Dim objClsInventory As New clsInventory
        Dim dsInv As New DataSet
        Try
            If objClsSplit.IsInventory(txtInventoryCode.Text.Trim) Then
                dsInv = objClsSplit.GetInventoryDetail(txtInventoryCode.Text.Trim)
                If dsInv.Tables(0).Rows.Count > 0 Then
                    txtOriginalInvCode.Text = txtInventoryCode.Text.Trim
                    txtDescription.Text = Trim(dsInv.Tables(0).Rows(0)("Description") & "")
                    txtWeight.Text = Trim(dsInv.Tables(0).Rows(0)("Weight") & "")
                    txtStoneWeight.Text = Trim(dsInv.Tables(0).Rows(0)("StoneWeight") & "")
                    txtPMC.Text = Val(dsInv.Tables(0).Rows(0)("PMC"))
                    txtSMC.Text = Val(dsInv.Tables(0).Rows(0)("sMc"))
                    sTotalCost = Trim(dsInv.Tables(0).Rows(0)("TotalCost") & "")
                    dCostPerGram = Val(dsInv.Tables(0).Rows(0)("CostperGram"))
                    txtNewDescription.Text = Trim(dsInv.Tables(0).Rows(0)("Description") & "")
                    dOriginalPMC = Val(dsInv.Tables(0).Rows(0)("PMC"))
                    dOriginalSMC = Val(dsInv.Tables(0).Rows(0)("sMc"))
                    dOriginalGoldLoss = Val(dsInv.Tables(0).Rows(0)("GoldLoss"))
                    dOriginalTotalCost = Val(dsInv.Tables(0).Rows(0)("TotalCost"))
                    sJobsheetNo = Trim(dsInv.Tables(0).Rows(0)("JobSheetNo") & "")
                    sPurchaseBillNo = Trim(dsInv.Tables(0).Rows(0)("PurchaseBillNo") & "")
                    dPurchaseDate = dsInv.Tables(0).Rows(0)("PurchaseDate")
                    dCreateDate = Format(CDate(dsInv.Tables(0).Rows(0)("CreateDate")), "dd/MM/yy")
                    sSupplierCode = Trim(dsInv.Tables(0).Rows(0)("SupplierCode") & "")
                    sSalesBillNo = Trim(dsInv.Tables(0).Rows(0)("SalesBillNo") & "")
                    sDiamondDescription = Trim(dsInv.Tables(0).Rows(0)("DiamondDescription") & "")
                    dStoneWeight = dsInv.Tables(0).Rows(0)("StoneWeight")
                    dPurchaseDate = Format(CDate(dsInv.Tables(0).Rows(0)("PurchaseDate")), "dd/MM/yy")
                    sCategoryCode = Trim(dsInv.Tables(0).Rows(0)("CategoryCode"))
                    sSplitCategory = Trim(dsInv.Tables(0).Rows(0)("CategoryCode"))
                    sClassCode = Trim(dsInv.Tables(0).Rows(0)("ClassCode"))
                    sCatalogueCode = dsInv.Tables(0).Rows(0)("CatalogueCode")
                    sTrayId = Trim(dsInv.Tables(0).Rows(0)("TrayID"))
                    sPMCType = Trim(dsInv.Tables(0).Rows(0)("PMCType") & "")
                    sSMCType = Trim(dsInv.Tables(0).Rows(0)("SMCType") & "")
                    sRemarks = Trim(dsInv.Tables(0).Rows(0)("REMARKS") & "")



                    DsAdditional = objClsInventory.GetAdditionalInventoryDetail(txtInventoryCode.Text.Trim)
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
                    MessageBox.Show("This Item already sold. Cant split.")
                    txtInventoryCode.Focus()
                End If
            Else
                MessageBox.Show("Inventory does not exist.")
                txtInventoryCode.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtInventoryCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInventoryCode.LostFocus
        If txtInventoryCode.Text.Trim <> "" Then subGetInventoryDetail()
    End Sub

    Private Sub subClearMe()
        txtInventoryCode.Clear()
        txtDescription.Clear()
        txtWeight.Clear()
        txtSplitWeight.Clear()
        txtOriginalInvCode.Clear()
        txtOriginalInvWeight.Clear()
        txtSplitInvCode.Clear()
        txtSplitInvWeight.Clear()
        txtNewDescription.Clear()
        txtOrgSizeCode.Clear()
        txtorgDescription.Clear()
        txtOriginalSMC.Clear()
        txtNewSizeCode.Clear()
        txtNewSMC.Clear()
        txtInventoryCode.Focus()
        cboCategory.Enabled = True
        cboCatalogue.Enabled = True

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

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        subClearMe()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        subSave()

        If bPrntBarcode Then
            If UCase(sCompanyShortCode) <> "ES" Then
                Call btnBarcode_Click(sender, e)
            End If
            MessageBox.Show("Updated sucessfully")
            subClearMe()
            bPrntBarcode = False
        End If
    End Sub

    Private Sub subSave()
        Try
            If txtSplitInvCode.Text.Trim = "" Then
                MessageBox.Show("The split InventoryCode should not be blank.")
                txtSplitInvCode.Focus()
                Exit Sub
            End If
            If Val(txtOriginalInvWeight.Text.Trim) = 0 Or Val(txtSplitInvWeight.Text.Trim) = 0 Then
                MessageBox.Show("Invalid weight entered.")
                txtSplitInvWeight.Focus()
                Exit Sub
            End If
            If fncInventorySplitSave() Then
                bPrntBarcode = True
            Else
                bPrntBarcode = False
                MessageBox.Show("Error in save")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function fncInventorySplitSave() As Boolean
        Dim bSaveStatus As Boolean
        Dim bPurchase As Boolean
        bSaveStatus = False
        Try

            If Trim(sPurchaseCostingGL) = "Y" Then
                dTotalCost = (Val(txtSplitInvWeight.Text.Trim) + Val(dGoldLoss)) * Val(dCostPerGram) + dPMC
            Else
                dTotalCost = (Val(txtSplitInvWeight.Text.Trim) * dCostPerGram) + dPMC
            End If

            'If Not objClsSplit.UpdateSplitInventory(Trim(txtInventoryCode.Text.Trim), Val(txtOriginalInvWeight.Text.Trim), Trim(sCompanyShortCode)) Then
            '    Exit Function
            'End If

            'Shabeer 20150611
            '' ''If Not objClsSplit.UpdateSplitInventory(Trim(txtInventoryCode.Text.Trim), Val(txtOriginalInvWeight.Text.Trim), Trim(txtOriginalInvCode.Text), Trim(txtorgDescription.Text), _
            '' ''                                        Trim(cboOrgCategory.Text), Trim(cboOrgCatalogue.Text), Val(txtOrgSizeCode.Text), Trim(sOrgSizeType), Val(txtOriginalStWeight.Text), Val(txtOriginalSMC.Text), Trim(sCompanyShortCode)) Then
            '' ''    Exit Function
            '' ''End If


            If sRFIDModule = "1" Then
                objClsSplit.RFIDItemWtMCUpdate(Trim(txtInventoryCode.Text.Trim), Val(txtOriginalInvWeight.Text.Trim), Val(txtOriginalSMC.Text.Trim))
            End If


            If chkIPM.Checked Then
                If Not objClsSplit.fncSplitIPMInsert(txtInventoryCode.Text.Trim, txtSplitInvCode.Text.Trim) Then
                    Exit Function
                End If
            End If

            If Trim(gthisLocationType) <> "HQ" Then


       


                If Trim(txtOriginalInvCode.Text) <> Trim(txtInventoryCode.Text) Then

                    If Not objClsSplit.UpdateSplitInventoryDiff(Trim(txtInventoryCode.Text.Trim), Val(txtOriginalInvWeight.Text.Trim), Trim(txtOriginalInvCode.Text), Trim(txtorgDescription.Text), _
                         Trim(cboOrgCategory.Text), Trim(cboOrgCatalogue.Text), Val(txtOrgSizeCode.Text), Trim(sOrgSizeType), Val(txtOriginalStWeight.Text), Val(txtOriginalSMC.Text), Trim(sCompanyShortCode)) Then
                        Exit Function
                    End If

                    If Not objClsSplit.InventorySaveHQ(Trim(txtOriginalInvCode.Text), _
                       Trim(txtorgDescription.Text), Trim(sDiamondDescription), _
                       Val(txtOriginalInvWeight.Text.Trim), Val(txtOriginalStWeight.Text.Trim), Val(dCostPerGram), _
                       Val(dPMC), (Val(txtOriginalSMC.Text)), Val(dPCS), Val(dGoldLoss), Val(dTotalCost), _
                       Trim(sJobsheetNo), Trim(sPurchaseBillNo), Trim(dPurchaseDate), Trim(sSupplierCode), _
                       "", "", 0, 0, 0, Trim(sCategoryCode), Trim(sClassCode), Trim(sCatalogueCode), Trim(sTrayId), _
                       Trim$(sPMCType), Trim$(sSMCType), CLng(dPCS), 0, Trim(sRemarks), Val(txtOrgSizeCode.Text), Trim(sOrgSizeType)) Then
                        Exit Function
                    End If



                    



                    bPurchase = objClsInvMerge.fncInventoryMovementSaveHQ(Trim("HQ"), Trim(sCreateInventory), Trim(txtorgDescription.Text.Trim), "PU", "", "", _
                                    Val(1), Val(txtOriginalInvWeight.Text.Trim), Val(txtOrgSizeCode.Text), Val(dPMC), _
                                    Val(txtOriginalSMC.Text), 0, Val(dTotalCost), _
                                     "", sUserID)


                    bPurchase = objClsInvMerge.fncInventoryMovementSaveHQ(Trim(gthisLocation), Trim(sCreateInventory), Trim(txtorgDescription.Text.Trim), "TO", "", "", _
                                             Val(1), Val(txtOriginalInvWeight.Text.Trim), Val(txtOrgSizeCode.Text), Val(dPMC), _
                                             Val(txtOriginalSMC.Text), 0, Val(dTotalCost), _
                                              "", sUserID)


                    If Not objClsSplit.InventorySave(Trim(sCreateInventory), _
                    Trim(txtorgDescription.Text), Trim(sDiamondDescription), _
                    Val(txtOriginalInvWeight.Text.Trim), Val(txtOriginalStWeight.Text), Val(dCostPerGram), _
                    Val(dPMC), (Val(txtOriginalSMC.Text)), Val(dPCS), Val(dGoldLoss), Val(dTotalCost), _
                    Trim(sJobsheetNo), Trim(sPurchaseBillNo), Trim(dPurchaseDate), Trim(sSupplierCode), _
                    "", "", 0, 0, 0, Trim(sCategoryCode), Trim(sClassCode), Trim(sCatalogueCode), Trim(sTrayId), _
                    Trim$(sPMCType), Trim$(sSMCType), CLng(dPCS), 0, Trim(sRemarks), Val(txtOrgSizeCode.Text), Trim(sOrgSizeType)) Then
                        Exit Function
                    End If




                    bPurchase = objClsInvMerge.fncInventoryMovementSave(Trim(gthisLocation), Trim(sCreateInventory), Trim(txtNewDescription.Text.Trim), "TI", "", "", _
                                              Val(1), Val(txtSplitInvWeight.Text.Trim), Val(txtNewSizeCode.Text), Val(dPMC), _
                                              Val(txtNewSMC.Text), 0, Val(dTotalCost), _
                                               "", sUserID)

                    bPurchase = objClsInvMerge.fncInventoryMovementSave(Trim(gthisLocation), Trim(txtInventoryCode.Text), Trim(txtDescription.Text.Trim), "SP", "", "", _
                                              Val(1), Val(txtWeight.Text.Trim), Val(txtOrgSizeCode.Text), Val(txtPMC.Text), _
                                              Val(txtSMC.Text), 0, Val(dTotalCost), _
                                               "", sUserID)



                    If Not objClsSplit.InventorySaveHQ(Trim(txtSplitInvCode.Text), _
                       Trim(txtNewDescription.Text), Trim(sDiamondDescription), _
                       Val(txtSplitInvWeight.Text.Trim), Val(txtSplitStWeight.Text.Trim), Val(dCostPerGram), _
                       Val(dPMC), (Val(txtNewSMC.Text)), Val(dPCS), Val(dGoldLoss), Val(dTotalCost), _
                       Trim(sJobsheetNo), Trim(sPurchaseBillNo), Trim(dPurchaseDate), Trim(sSupplierCode), _
                       "", "", 0, 0, 0, Trim(sCategoryCode), Trim(sClassCode), Trim(sCatalogueCode), Trim(sTrayId), _
                       Trim$(sPMCType), Trim$(sSMCType), CLng(dPCS), 0, Trim(sRemarks), Val(txtNewSizeCode.Text), Trim(sNewSizeType)) Then
                        Exit Function
                    End If


                    If Not objClsSplit.SplitInventorySaveHQ(Trim(txtInventoryCode.Text), Trim(txtDescription.Text), _
                        Val(txtWeight.Text.Trim), Val(dOriginalPMC), Val(txtOriginalSMC.Text), _
                        Val(dOriginalGoldLoss), Val(dOriginalTotalCost), _
                        Val(txtOriginalInvWeight.Text), Trim(sCreateInventory), _
                        Trim(txtNewDescription.Text), Val(txtSplitInvWeight.Text), Val(dPMC), Val(dSMC), _
                        Val(dGoldLoss), Val(dTotalCost), Trim(txtOriginalInvCode.Text)) Then
                        Exit Function
                    End If

                    bPurchase = objClsInvMerge.fncInventoryMovementSaveHQ(Trim("HQ"), Trim(sCreateInventory), Trim(txtNewDescription.Text.Trim), "PU", "", "", _
                                Val(1), Val(txtSplitInvWeight.Text.Trim), Val(txtNewSizeCode.Text), Val(dPMC), _
                                Val(txtNewSMC.Text), 0, Val(dTotalCost), _
                                 "", sUserID)


                    bPurchase = objClsInvMerge.fncInventoryMovementSaveHQ(Trim(gthisLocation), Trim(sCreateInventory), Trim(txtNewDescription.Text.Trim), "TO", "", "", _
                                             Val(1), Val(txtSplitInvWeight.Text.Trim), Val(txtNewSizeCode.Text), Val(dPMC), _
                                             Val(txtNewSMC.Text), 0, Val(dTotalCost), _
                                              "", sUserID)


                    If Not objClsSplit.InventorySave(Trim(sCreateInventory), _
                    Trim(txtNewDescription.Text), Trim(sDiamondDescription), _
                    Val(txtSplitInvWeight.Text.Trim), Val(txtSplitStWeight.Text.Trim), Val(dCostPerGram), _
                    Val(dPMC), (Val(txtNewSMC.Text)), Val(dPCS), Val(dGoldLoss), Val(dTotalCost), _
                    Trim(sJobsheetNo), Trim(sPurchaseBillNo), Trim(dPurchaseDate), Trim(sSupplierCode), _
                    "", "", 0, 0, 0, Trim(sCategoryCode), Trim(sClassCode), Trim(sCatalogueCode), Trim(sTrayId), _
                    Trim$(sPMCType), Trim$(sSMCType), CLng(dPCS), 0, Trim(sRemarks), Val(txtNewSizeCode.Text), Trim(sNewSizeType)) Then
                        Exit Function
                    End If


                    If Not objClsSplit.SplitInventorySave(Trim(txtInventoryCode.Text), Trim(txtDescription.Text), _
                    Val(txtWeight.Text.Trim), Val(dOriginalPMC), Val(txtOriginalSMC.Text), _
                    Val(dOriginalGoldLoss), Val(dOriginalTotalCost), _
                    Val(txtOriginalInvWeight.Text), Trim(sCreateInventory), _
                    Trim(txtNewDescription.Text), Val(txtSplitInvWeight.Text), Val(dPMC), Val(dSMC), _
                    Val(dGoldLoss), Val(dTotalCost), Trim(txtOriginalInvCode.Text), Val(txtStoneWeight.Text), Val(txtOriginalStWeight.Text), Val(txtSplitStWeight.Text)) Then
                        Exit Function
                    End If

                    bPurchase = objClsInvMerge.fncInventoryMovementSave(Trim(gthisLocation), Trim(sCreateInventory), Trim(txtNewDescription.Text.Trim), "TI", "", "", _
                                              Val(1), Val(txtSplitInvWeight.Text.Trim), Val(txtNewSizeCode.Text), Val(dPMC), _
                                              Val(txtNewSMC.Text), 0, Val(dTotalCost), _
                                               "", sUserID)


                Else


                    If Not objClsSplit.UpdateSplitInventory(Trim(txtInventoryCode.Text.Trim), Val(txtOriginalInvWeight.Text.Trim), Trim(txtOriginalInvCode.Text), Trim(txtorgDescription.Text), _
                                      Trim(cboOrgCategory.Text), Trim(cboOrgCatalogue.Text), Val(txtOrgSizeCode.Text), Trim(sOrgSizeType), Val(txtOriginalStWeight.Text), Val(txtOriginalSMC.Text), Trim(sCompanyShortCode)) Then
                        Exit Function
                    End If


                    If Not objClsSplit.InventorySaveHQ(Trim(txtSplitInvCode.Text), _
                    Trim(txtNewDescription.Text), Trim(sDiamondDescription), _
                    Val(txtSplitInvWeight.Text.Trim), Val(dStoneWeight), Val(dCostPerGram), _
                    Val(dPMC), (Val(txtNewSMC.Text)), Val(dPCS), Val(dGoldLoss), Val(dTotalCost), _
                    Trim(sJobsheetNo), Trim(sPurchaseBillNo), Trim(dPurchaseDate), Trim(sSupplierCode), _
                    "", "", 0, 0, 0, Trim(sCategoryCode), Trim(sClassCode), Trim(sCatalogueCode), Trim(sTrayId), _
                    Trim$(sPMCType), Trim$(sSMCType), CLng(dPCS), 0, Trim(sRemarks), Val(txtNewSizeCode.Text), Trim(sNewSizeType)) Then
                        Exit Function
                    End If
                    If Not objClsSplit.SplitInventorySaveHQ(Trim(txtInventoryCode.Text), Trim(txtDescription.Text), _
                    Val(txtWeight.Text.Trim), Val(dOriginalPMC), Val(txtOriginalSMC.Text), _
                    Val(dOriginalGoldLoss), Val(dOriginalTotalCost), _
                    Val(txtOriginalInvWeight.Text), Trim(sCreateInventory), _
                    Trim(txtNewDescription.Text), Val(txtSplitInvWeight.Text), Val(dPMC), Val(dSMC), _
                    Val(dGoldLoss), Val(dTotalCost), Trim(txtOriginalInvCode.Text)) Then
                        Exit Function
                    End If




                    bPurchase = objClsInvMerge.fncInventoryMovementSaveHQ(Trim("HQ"), Trim(sCreateInventory), Trim(txtNewDescription.Text.Trim), "PU", "", "", _
                                    Val(1), Val(txtSplitInvWeight.Text.Trim), Val(txtNewSizeCode.Text), Val(dPMC), _
                                    Val(txtNewSMC.Text), 0, Val(dTotalCost), _
                                     "", sUserID)


                    bPurchase = objClsInvMerge.fncInventoryMovementSaveHQ(Trim(gthisLocation), Trim(sCreateInventory), Trim(txtNewDescription.Text.Trim), "TO", "", "", _
                                             Val(1), Val(txtSplitInvWeight.Text.Trim), Val(txtNewSizeCode.Text), Val(dPMC), _
                                             Val(txtNewSMC.Text), 0, Val(dTotalCost), _
                                              "", sUserID)

                    If Not objClsSplit.InventorySave(Trim(sCreateInventory), _
                    Trim(txtNewDescription.Text), Trim(sDiamondDescription), _
                    Val(txtSplitInvWeight.Text.Trim), Val(txtSplitStWeight.Text), Val(dCostPerGram), _
                    Val(dPMC), (Val(txtNewSMC.Text)), Val(dPCS), Val(dGoldLoss), Val(dTotalCost), _
                    Trim(sJobsheetNo), Trim(sPurchaseBillNo), Trim(dPurchaseDate), Trim(sSupplierCode), _
                    "", "", 0, 0, 0, Trim(sCategoryCode), Trim(sClassCode), Trim(sCatalogueCode), Trim(sTrayId), _
                    Trim$(sPMCType), Trim$(sSMCType), CLng(dPCS), 0, Trim(sRemarks), Val(txtNewSizeCode.Text), Trim(sNewSizeType)) Then
                        Exit Function
                    End If

                    If Not objClsSplit.SplitInventorySave(Trim(txtInventoryCode.Text), Trim(txtDescription.Text), _
                    Val(txtWeight.Text.Trim), Val(dOriginalPMC), Val(txtOriginalSMC.Text), _
                    Val(dOriginalGoldLoss), Val(dOriginalTotalCost), _
                    Val(txtOriginalInvWeight.Text), Trim(sCreateInventory), _
                    Trim(txtNewDescription.Text), Val(txtSplitInvWeight.Text), Val(dPMC), Val(dSMC), _
                    Val(dGoldLoss), Val(dTotalCost), Trim(txtOriginalInvCode.Text), Val(txtStoneWeight.Text), Val(txtOriginalStWeight.Text), Val(txtSplitStWeight.Text)) Then
                        Exit Function
                    End If



                    bPurchase = objClsInvMerge.fncInventoryMovementSave(Trim(gthisLocation), Trim(sCreateInventory), Trim(txtNewDescription.Text.Trim), "TI", "", "", _
                                              Val(1), Val(txtSplitInvWeight.Text.Trim), Val(txtNewSizeCode.Text), Val(dPMC), _
                                              Val(txtNewSMC.Text), 0, Val(dTotalCost), _
                                               "", sUserID)

                    bPurchase = objClsInvMerge.fncInventoryMovementSave(Trim(gthisLocation), Trim(txtOriginalInvCode.Text), Trim(txtorgDescription.Text.Trim), "SP", "", "", _
                                              Val(1), Val(txtOriginalInvWeight.Text.Trim), Val(txtOrgSizeCode.Text), Val(dPMC), _
                                              Val(txtOriginalSMC.Text), 0, Val(dTotalCost), _
                                               "", sUserID)


                End If


            Else

    

                If Trim(txtOriginalInvCode.Text) <> Trim(txtInventoryCode.Text) Then

                    If Not objClsSplit.UpdateSplitInventoryDiff(Trim(txtInventoryCode.Text.Trim), Val(txtOriginalInvWeight.Text.Trim), Trim(txtOriginalInvCode.Text), Trim(txtorgDescription.Text), _
                        Trim(cboOrgCategory.Text), Trim(cboOrgCatalogue.Text), Val(txtOrgSizeCode.Text), Trim(sOrgSizeType), Val(txtOriginalStWeight.Text), Val(txtOriginalSMC.Text), Trim(sCompanyShortCode)) Then
                        Exit Function
                    End If

                    If Not objClsSplit.InventorySave(Trim(txtOriginalInvCode.Text), _
                    Trim(txtorgDescription.Text), Trim(sDiamondDescription), _
                    Val(txtOriginalInvWeight.Text.Trim), Val(txtOriginalStWeight.Text), Val(dCostPerGram), _
                    Val(dPMC), (Val(txtOriginalSMC.Text)), Val(dPCS), Val(dGoldLoss), Val(dTotalCost), _
                    Trim(sJobsheetNo), Trim(sPurchaseBillNo), Trim(dPurchaseDate), Trim(sSupplierCode), _
                    "", "", 0, 0, 0, Trim(sCategoryCode), Trim(sClassCode), Trim(sCatalogueCode), Trim(sTrayId), _
                    Trim$(sPMCType), Trim$(sSMCType), CLng(dPCS), 0, Trim(sRemarks), Val(txtOrgSizeCode.Text), Trim(sOrgSizeType)) Then
                        Exit Function
                    End If

                    If Not objClsSplit.InventorySave(Trim(txtSplitInvCode.Text), _
                       Trim(txtNewDescription.Text), Trim(sDiamondDescription), _
                       Val(txtSplitInvWeight.Text.Trim), Val(txtSplitStWeight.Text), Val(dCostPerGram), _
                       Val(dPMC), (Val(txtNewSMC.Text)), Val(dPCS), Val(dGoldLoss), Val(dTotalCost), _
                       Trim(sJobsheetNo), Trim(sPurchaseBillNo), Trim(dPurchaseDate), Trim(sSupplierCode), _
                       "", "", 0, 0, 0, Trim(sCategoryCode), Trim(sClassCode), Trim(sCatalogueCode), Trim(sTrayId), _
                       Trim$(sPMCType), Trim$(sSMCType), CLng(dPCS), 0, Trim(sRemarks), Val(txtNewSizeCode.Text), Trim(sNewSizeType)) Then
                        Exit Function
                    End If

                    If Not objClsSplit.SplitInventorySave(Trim(txtInventoryCode.Text), Trim(txtDescription.Text), _
                    Val(txtWeight.Text.Trim), Val(dOriginalPMC), Val(txtOriginalSMC.Text), _
                    Val(dOriginalGoldLoss), Val(dOriginalTotalCost), _
                    Val(txtOriginalInvWeight.Text), Trim(sCreateInventory), _
                    Trim(txtNewDescription.Text), Val(txtSplitInvWeight.Text), Val(dPMC), Val(dSMC), _
                    Val(dGoldLoss), Val(dTotalCost), Trim(txtOriginalInvCode.Text), Val(txtStoneWeight.Text), Val(txtOriginalStWeight.Text), Val(txtSplitStWeight.Text)) Then
                        Exit Function
                    End If

                    bPurchase = objClsInvMerge.fncInventoryMovementSaveHQ(Trim("HQ"), Trim(txtOriginalInvCode.Text), Trim(txtorgDescription.Text.Trim), "PU", "", "", _
                                                      Val(1), Val(txtOriginalInvWeight.Text.Trim), Val(txtOrgSizeCode.Text), Val(dPMC), _
                                                      Val(txtOriginalSMC.Text), 0, Val(dTotalCost), _
                                                       "", sUserID)

                    bPurchase = objClsInvMerge.fncInventoryMovementSaveHQ(Trim("HQ"), Trim(txtSplitInvCode.Text), Trim(txtNewDescription.Text.Trim), "PU", "", "", _
                              Val(1), Val(txtSplitInvWeight.Text.Trim), Val(txtNewSizeCode.Text), Val(dPMC), _
                              Val(txtNewSMC.Text), 0, Val(dTotalCost), _
                               "", sUserID)

                    bPurchase = objClsInvMerge.fncInventoryMovementSave(Trim(gthisLocation), Trim(txtInventoryCode.Text), Trim(txtDescription.Text.Trim), "SP", "", "", _
                                             Val(1), Val(txtWeight.Text.Trim), Val(0), Val(txtPMC.Text), _
                                             Val(txtSMC.Text), 0, Val(dTotalCost), _
                                              "", sUserID)
                Else

                    If Not objClsSplit.UpdateSplitInventory(Trim(txtInventoryCode.Text.Trim), Val(txtOriginalInvWeight.Text.Trim), Trim(txtOriginalInvCode.Text), Trim(txtorgDescription.Text), _
                                      Trim(cboOrgCategory.Text), Trim(cboOrgCatalogue.Text), Val(txtOrgSizeCode.Text), Trim(sOrgSizeType), Val(txtOriginalStWeight.Text), Val(txtOriginalSMC.Text), Trim(sCompanyShortCode)) Then
                        Exit Function
                    End If

                    If Not objClsSplit.InventorySave(Trim(txtSplitInvCode.Text), _
                       Trim(txtNewDescription.Text), Trim(sDiamondDescription), _
                       Val(txtSplitInvWeight.Text.Trim), Val(txtSplitStWeight.Text), Val(dCostPerGram), _
                       Val(dPMC), (Val(txtNewSMC.Text)), Val(dPCS), Val(dGoldLoss), Val(dTotalCost), _
                       Trim(sJobsheetNo), Trim(sPurchaseBillNo), Trim(dPurchaseDate), Trim(sSupplierCode), _
                       "", "", 0, 0, 0, Trim(sCategoryCode), Trim(sClassCode), Trim(sCatalogueCode), Trim(sTrayId), _
                       Trim$(sPMCType), Trim$(sSMCType), CLng(dPCS), 0, Trim(sRemarks), Val(txtNewSizeCode.Text), Trim(sNewSizeType)) Then
                        Exit Function
                    End If

                    If Not objClsSplit.SplitInventorySave(Trim(txtInventoryCode.Text), Trim(txtDescription.Text), _
                    Val(txtWeight.Text.Trim), Val(dOriginalPMC), Val(txtOriginalSMC.Text), _
                    Val(dOriginalGoldLoss), Val(dOriginalTotalCost), _
                    Val(txtOriginalInvWeight.Text), Trim(sCreateInventory), _
                    Trim(txtNewDescription.Text), Val(txtSplitInvWeight.Text), Val(dPMC), Val(dSMC), _
                    Val(dGoldLoss), Val(dTotalCost), Trim(txtOriginalInvCode.Text), Val(txtStoneWeight.Text), Val(txtOriginalStWeight.Text), Val(txtSplitStWeight.Text)) Then
                        Exit Function
                    End If

                    bPurchase = objClsInvMerge.fncInventoryMovementSaveHQ(Trim("HQ"), Trim(txtSplitInvCode.Text), Trim(txtNewDescription.Text.Trim), "PU", "", "", _
               Val(1), Val(txtSplitInvWeight.Text.Trim), Val(txtNewSizeCode.Text), Val(dPMC), _
               Val(txtNewSMC.Text), 0, Val(dTotalCost), _
                "", sUserID)

                    bPurchase = objClsInvMerge.fncInventoryMovementSave(Trim(gthisLocation), Trim(txtInventoryCode.Text), Trim(txtDescription.Text.Trim), "SP", "", "", _
                                             Val(1), Val(txtWeight.Text.Trim), Val(0), Val(txtPMC.Text), _
                                             Val(txtSMC.Text), 0, Val(dTotalCost), _
                                              "", sUserID)

                End If

            End If


            Dim objInventory As New clsInventory

            If chkAdditionalDesc.Checked Then
                If objInventory.InventoryAddlDescSave(txtOriginalInvCode.Text.Trim, "", txtNewDesc1.Text.Trim, _
                    txtNewDesc2.Text.Trim, txtNewDesc3.Text.Trim, txtNewDesc4.Text.Trim, Val(txtNewCost1.Text.Trim), Val(txtNewCost2.Text.Trim), _
                    Val(txtNewCost3.Text.Trim), Val(txtNewCost4.Text.Trim), Val(txtNewSellingPrice1.Text.Trim), Val(txtNewSellingPrice2.Text.Trim), _
                    Val(txtNewSellingPrice3.Text.Trim), Val(txtNewSellingPrice4.Text.Trim), sUserID) Then
                Else
                    MsgBox("Error in Saving Inventory Additional Description", MsgBoxStyle.Critical)
                    bPurchase = False
                    Exit Function
                End If
            End If

            bPurchase = True


            'dPurchaseDate = Format(dPurchaseDate, "dd/MM/yy")
            'If Format(Now(), "dd/MM/yy") <> dCreateDate Then
            '    bPurchase = objClsSplit.PurchaseSave(Trim(sJobsheetNo), _
            '                Trim(sPurchaseBillNo), "", Trim(sSupplierCode), Trim(txtSplitInvCode.Text.Trim), _
            '                Trim(txtDescription.Text.Trim), Trim(sDiamondDescription), Val(dStoneWeight), _
            '                Val(dCostPerGram), Val(txtSplitInvWeight.Text.Trim), Val(dPMC), Val(dPCS), Val(dGoldLoss), Val(dTotalCost), _
            '                Trim(sCatalogueCode), Trim(sCategoryCode), Trim(sClassCode), Trim(sTrayId), "", "ST")
            'Else
            '    bPurchase = objClsSplit.PurchaseSave(Trim(sJobsheetNo), _
            '                Trim(sPurchaseBillNo), "", Trim(sSupplierCode), Trim(txtSplitInvCode.Text.Trim), _
            '                Trim(txtDescription.Text.Trim), Trim(sDiamondDescription), Val(dStoneWeight), _
            '                Val(dCostPerGram), Val(txtSplitInvWeight.Text.Trim), Val(dPMC), Val(dPCS), Val(dGoldLoss), Val(dTotalCost), _
            '                Trim(sCatalogueCode), Trim(sCategoryCode), Trim(sClassCode), Trim(sTrayId), "", Trim(sRemarks))
            'End If
            If bPurchase Then
                bSaveStatus = True
            Else
                Exit Function
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncInventorySplitSave = bSaveStatus
        End Try
    End Function

    Private Sub frmSplitInventory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F6
                btnClear_Click(sender, e)
            Case Keys.F7
                btnSave_Click(sender, e)
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

    Private Sub btnSplit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSplit.Click
        Try
            If Val(txtSplitWeight.Text.Trim) > 0 Then
                subSplitWeight()
            Else
                MessageBox.Show("Split weight shold be greater than Zero.")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subSplitWeight()
        Dim dsSInv As New DataSet
        Dim dTemp As Double
        Dim icnt As Integer
        Try
            If Not (Val(txtSplitWeight.Text.Trim) < Val(txtWeight.Text.Trim)) Then
                MessageBox.Show("Split Weight should be less then Inventory Weight.")
                Exit Sub
                txtSplitWeight.Focus()
            End If
            dsSInv = objClsSplit.GetSplitInventoryDetail(txtInventoryCode.Text.Trim, sCategoryCode)
            If dsSInv.Tables(0).Rows.Count > 0 Then
                dWeight1 = Val(txtWeight.Text.Trim) - Val(txtSplitWeight.Text.Trim)
                txtOriginalInvWeight.Text = dWeight1
                dWeight2 = Val(txtSplitWeight.Text.Trim)
                txtSplitInvWeight.Text = dWeight2
                dTemp = sMcCost / Val(txtWeight.Text.Trim)
                dMcCost1 = dTemp * dWeight1
                dTemp = sMcPrice / Val(txtWeight.Text.Trim)
                dMcPrice1 = dTemp * dWeight1
                dTemp = sTotalCost / Val(txtWeight.Text.Trim)
                dTotalCost1 = dTemp * dWeight1
                dMcCost2 = sMcCost - dMcCost1
                dMcPrice2 = sMcPrice - dMcPrice1
                dTotalcost2 = sTotalCost - dTotalCost1
                dWeight1 = Val(txtWeight.Text.Trim) - Val(txtSplitWeight.Text.Trim)
                dGoldLoss = (dWeight2 * dsSInv.Tables(0).Rows(0)("GoldLoss")) / Val(txtWeight.Text.Trim)
                dPMC = (dWeight2 * dsSInv.Tables(0).Rows(0)("PMC")) / Val(txtWeight.Text.Trim)
                dSMC = (dWeight2 * dsSInv.Tables(0).Rows(0)("sMc")) / Val(txtWeight.Text.Trim)
                dPCS = dsSInv.Tables(0).Rows(0)("Pcs")
                dStoneWeight1 = Val(txtStoneWeight.Text) - Val(txtStoneSplitWeight.Text)
                ' txtOriginalStWeight.Text = dsSInv.Tables(0).Rows(0)("StoneWeight")
                txtOriginalStWeight.Text = dStoneWeight1
                dStoneWeight2 = Val(txtStoneSplitWeight.Text)
                txtSplitStWeight.Text = dStoneWeight2
                txtNewDescription.Enabled = True

                If Trim$(dsSInv.Tables(0).Rows(0)("CatalogueCode")) <> "" Then
                    sCatalogueCode = Trim(dsSInv.Tables(0).Rows(0)("CatalogueCode"))
                    sCategoryCode = Trim(dsSInv.Tables(0).Rows(0)("CategoryCode"))
                    sClassCode = Trim(dsSInv.Tables(0).Rows(0)("ClassCode"))
                    '''  If dsSInv.Tables(1).Rows.Count > 0 Then txtSplitInvCode.Text = CStr(dsSInv.Tables(1).Rows(0)("NextInventoryCode"))
                    ''' 
                    DS = objClsInvMerge.GetCategory(sClassCode)
                    If DS.Tables(0).Rows.Count > 0 Then
                        For icnt = 0 To DS.Tables(0).Rows.Count - 1
                            cboCategory.Items.Add(Trim(DS.Tables(0).Rows(icnt)("categorycode") & ""))
                        Next
                    End If
                    cboCategory.SelectedIndex = 0

                    DS = objClsInvMerge.GetCategory(sClassCode)
                    If DS.Tables(0).Rows.Count > 0 Then
                        For icnt = 0 To DS.Tables(0).Rows.Count - 1
                            cboOrgCategory.Items.Add(Trim(DS.Tables(0).Rows(icnt)("categorycode") & ""))
                        Next
                    End If
                    cboOrgCategory.SelectedIndex = 0
                    cboOrgCategory.Text = Trim(dsSInv.Tables(0).Rows(0)("CategoryCode"))


                    DS = objClsInvMerge.GetCatalogue(sCategoryCode)
                    If DS.Tables(0).Rows.Count > 0 Then
                        For icnt = 0 To DS.Tables(0).Rows.Count - 1
                            cboOrgCatalogue.Items.Add(Trim(DS.Tables(0).Rows(icnt)("CatalogueCode") & ""))
                        Next
                    End If
                    cboOrgCatalogue.SelectedIndex = 0
                    cboOrgCatalogue.Text = Trim(dsSInv.Tables(0).Rows(0)("CatalogueCode"))

                End If
            Else
                MsgBox("Invalid Split Option for this inventory")
                txtInventoryCode.Text = ""
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnBarcode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBarcode.Click
        Dim sInventoryCode As String
        Dim i As Integer
        If chkScrap.Checked = False Then
            For i = 1 To 2
                sInventoryCode = ""
                If i = 1 Then
                    sInventoryCode = Trim(txtOriginalInvCode.Text)
                ElseIf i = 2 Then
                    sInventoryCode = Trim(txtSplitInvCode.Text)
                End If
                If UCase(sCompanyShortCode) = "HN" Or UCase(sCompanyShortCode) = "MJ" Or UCase(sCompanyShortCode) = "SSJ" Or UCase(sCompanyShortCode) = "RJ" Then
                    Dim objfrm As New frmBarcode
                    objfrm.txtInventoryCode.Text = Trim(sInventoryCode)
                    objfrm.txtInventoryCode_LostFocus(sender, e)
                    objfrm.btnPrint_Click(sender, e)
                ElseIf UCase(sCompanyShortCode) = "AJ" Then
                    Dim objfrm As New frmBarcode
                    objfrm.txtInventoryCode.Text = Trim(sInventoryCode)
                    objfrm.txtInventoryCode_LostFocus(sender, e)
                    objfrm.btnPrint_Click(sender, e)
                ElseIf UCase(sCompanyShortCode) = "AS" Then
                    Dim objfrm As New frmJewelBarcode
                    objfrm.txtInventoryCode.Text = Trim(sInventoryCode)
                    objfrm.txtInventoryCode_LostFocus(sender, e)
                    objfrm.btnPrint_Click(sender, e)
                End If
            Next
        Else

            sInventoryCode = Trim(txtOriginalInvCode.Text)

            If UCase(sCompanyShortCode) = "HN" Or UCase(sCompanyShortCode) = "MJ" Or UCase(sCompanyShortCode) = "SSJ" Or UCase(sCompanyShortCode) = "RJ" Then
                Dim objfrm As New frmBarcode
                objfrm.txtInventoryCode.Text = Trim(sInventoryCode)
                objfrm.txtInventoryCode_LostFocus(sender, e)
                objfrm.btnPrint_Click(sender, e)
            ElseIf UCase(sCompanyShortCode) = "AJ" Then
                Dim objfrm As New frmBarcode
                objfrm.txtInventoryCode.Text = Trim(sInventoryCode)
                objfrm.txtInventoryCode_LostFocus(sender, e)
                objfrm.btnPrint_Click(sender, e)
            ElseIf UCase(sCompanyShortCode) = "AS" Then
                Dim objfrm As New frmJewelBarcode
                objfrm.txtInventoryCode.Text = Trim(sInventoryCode)
                objfrm.txtInventoryCode_LostFocus(sender, e)
                objfrm.btnPrint_Click(sender, e)
            End If

        End If

    End Sub

    Private Sub cboCategory_LostFocus(sender As Object, e As System.EventArgs) Handles cboCategory.LostFocus
        Dim ds As DataSet
        Dim icnt As Long
        Try
            ds = objClsInvMerge.GetDetailForCategory(cboCategory.Text.Trim)
            If ds.Tables(0).Rows.Count > 0 Then
                txtSplitInvCode.Text = (ds.Tables(0).Rows(0)("nextInventoryCode"))
                txtSplitInvCode.ReadOnly = True
                sClassCode = Trim((ds.Tables(0).Rows(0)("ClassCode")))
                sCategoryCode = Trim((ds.Tables(0).Rows(0)("CategoryCode")))
                If txtNewDescription.Enabled Then txtNewDescription.Focus()

                If Trim(cboOrgCategory.Text) = Trim(cboCategory.Text) And Trim(cboCategory.Text) <> Trim(sSplitCategory) Then
                    txtSplitInvCode.Text = Microsoft.VisualBasic.Left((ds.Tables(0).Rows(0)("nextInventoryCode")), 1) & CStr(Microsoft.VisualBasic.Right(Trim(ds.Tables(0).Rows(0)("nextInventoryCode")), 6) + 1)
                End If
                cboCatalogue.Items.Clear()
                ds = objClsInvMerge.GetCatalogue(sCategoryCode)
                If ds.Tables(0).Rows.Count > 0 Then
                    For icnt = 0 To ds.Tables(0).Rows.Count - 1
                        cboCatalogue.Items.Add(Trim(ds.Tables(0).Rows(icnt)("CatalogueCode") & ""))
                    Next
                End If
                cboCatalogue.SelectedIndex = 0
                cboCatalogue_LostFocus(sender, e)
            End If
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
            ds.Dispose()
        End Try

    End Sub

    Private Sub chkScrap_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkScrap.CheckedChanged
        Dim ds As DataSet
        Try
            If chkScrap.Checked = True Then
                ds = objClsInvMerge.GetScrapInventory()
                If ds.Tables(0).Rows.Count > 0 Then
                    '   txtSplitInvCode.Text = "SC-" & Trim(gthisLocation) & (ds.Tables(0).Rows(0)("NextScrapInventoryNo"))
                    txtSplitInvCode.Text = "SC-" & objClsInvMerge.GetScrapInventoryNo()
                    txtSplitInvCode.ReadOnly = True
                    sClassCode = "SCRAP"
                    sCategoryCode = "SCRAP"
                    sCategoryCode = "SCRAP"
                    txtNewDescription.Text = "SCRAP INVENTORY"
                    cboCategory.Enabled = False
                    cboCatalogue.Enabled = False
                    cboCategory.Text = ""
                    cboCatalogue.Text = ""
                    If txtNewDescription.Enabled Then txtNewDescription.Focus()
                End If
            Else
                cboCategory.Enabled = True
                cboCatalogue.Enabled = True
            End If
        Catch ex As Exception
            MsgBox(Err.Description)
        End Try

    End Sub

    Private Sub cboOrgCategory_LostFocus(sender As Object, e As System.EventArgs) Handles cboOrgCategory.LostFocus
        Dim ds As DataSet
        Dim icnt As Long
        Try
            ds = objClsInvMerge.GetDetailForCategory(cboOrgCategory.Text.Trim)
            If ds.Tables(0).Rows.Count > 0 Then
                If Trim(cboOrgCategory.Text) <> Trim(sSplitCategory) Then
                    txtOriginalInvCode.Text = (ds.Tables(0).Rows(0)("nextInventoryCode"))
                    txtOriginalInvCode.ReadOnly = True
                    sClassCode = Trim((ds.Tables(0).Rows(0)("ClassCode")))
                    sCategoryCode = Trim((ds.Tables(0).Rows(0)("CategoryCode")))
                    If txtNewDescription.Enabled Then txtNewDescription.Focus()
                Else
                    txtOriginalInvCode.Text = txtInventoryCode.Text.Trim
                End If
                cboOrgCatalogue.Items.Clear()
                ds = objClsInvMerge.GetCatalogue(sCategoryCode)
                If ds.Tables(0).Rows.Count > 0 Then
                    For icnt = 0 To ds.Tables(0).Rows.Count - 1
                        cboOrgCatalogue.Items.Add(Trim(ds.Tables(0).Rows(icnt)("CatalogueCode") & ""))
                    Next
                End If
                cboOrgCatalogue.SelectedIndex = 0
                cboOrgCatalogue_LostFocus(sender, e)
            End If
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
            ds.Dispose()
        End Try
    End Sub

    Private Sub cboCatalogue_LostFocus(sender As Object, e As System.EventArgs) Handles cboCatalogue.LostFocus
        txtNewDescription.Text = Trim(cboCatalogue.Text)
    End Sub
    Private Sub cboOrgCatalogue_LostFocus(sender As Object, e As System.EventArgs) Handles cboOrgCatalogue.LostFocus
        txtorgDescription.Text = Trim(cboOrgCatalogue.Text)
    End Sub

    Private Sub btnOrgSizeCode_Click(sender As System.Object, e As System.EventArgs) Handles btnOrgSizeCode.Click
        Try
            Dim e1 As New System.EventArgs
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSize", "L", 4, "SizeCode", "categorycode=" & fncFormatSQLText(sCategoryCode), "SizeCode", "SizeCode", 80, "SizeType", "SizeType", 100, "CategoryCode", "CategoryCode", 100, "Remarks", "Remarks", 100)
            frmHelpClass.ShowDialog()
            txtOrgSizeCode.Text = frmHelpClass.Var1.Trim
            sOrgSizeType = frmHelpClass.Var2.Trim
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnNewSizeCode_Click(sender As System.Object, e As System.EventArgs) Handles btnNewSizeCode.Click
        Try
            Dim e1 As New System.EventArgs
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSize", "L", 4, "SizeCode", "categorycode=" & fncFormatSQLText(sCategoryCode), "SizeCode", "SizeCode", 80, "SizeType", "SizeType", 100, "CategoryCode", "CategoryCode", 100, "Remarks", "Remarks", 100)
            frmHelpClass.ShowDialog()
            txtNewSizeCode.Text = frmHelpClass.Var1.Trim
            sNewSizeType = frmHelpClass.Var2.Trim
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Label16_Click(sender As System.Object, e As System.EventArgs) Handles Label16.Click

    End Sub

    Private Sub chkAdditionalDesc_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkAdditionalDesc.CheckedChanged

    End Sub

    Private Sub chkAdditionalDesc_Click(sender As Object, e As System.EventArgs) Handles chkAdditionalDesc.Click
        If chkAdditionalDesc.Checked = True Then
            grpAddlDescription.Visible = True
        Else
            grpAddlDescription.Visible = False
        End If
    End Sub

    Private Sub cmdAddlDescOk_Click(sender As Object, e As System.EventArgs) Handles cmdAddlDescOk.Click
        grpAddlDescription.Visible = False
    End Sub

    Private Sub grpAddlDescription_Enter(sender As System.Object, e As System.EventArgs) Handles grpAddlDescription.Enter

    End Sub
End Class
