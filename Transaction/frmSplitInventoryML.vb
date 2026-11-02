
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmSplitInventoryML
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
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtLength As System.Windows.Forms.TextBox
    Friend WithEvents txtNewDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtSplitInvLength As System.Windows.Forms.TextBox
    Friend WithEvents txtSplitLength As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtOriginalInvLength As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmSplitInventoryML))
        Me.btnClear = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtSplitLength = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtLength = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.btnSplit = New System.Windows.Forms.Button
        Me.txtSplitWeight = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtWeight = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtDescription = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtInventoryCode = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnInventoryCodeSearch = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtOriginalInvLength = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtOriginalInvWeight = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtOriginalInvCode = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtSplitInvLength = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtNewDescription = New System.Windows.Forms.TextBox
        Me.txtSplitInvWeight = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtSplitInvCode = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.btnSave = New System.Windows.Forms.Button
        Me.pic = New System.Windows.Forms.PictureBox
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(352, 376)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.TabIndex = 12
        Me.btnClear.Text = "Clear(F6)"
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(432, 376)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.TabIndex = 13
        Me.btnClose.Text = "Close(Esc)"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txtSplitLength)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.txtLength)
        Me.Panel1.Controls.Add(Me.Label10)
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
        Me.Panel1.Location = New System.Drawing.Point(10, 80)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(500, 120)
        Me.Panel1.TabIndex = 42
        '
        'txtSplitLength
        '
        Me.txtSplitLength.BackColor = System.Drawing.Color.White
        Me.txtSplitLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSplitLength.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSplitLength.ForeColor = System.Drawing.Color.Black
        Me.txtSplitLength.Location = New System.Drawing.Point(285, 88)
        Me.txtSplitLength.MaxLength = 20
        Me.txtSplitLength.Name = "txtSplitLength"
        Me.txtSplitLength.Size = New System.Drawing.Size(97, 20)
        Me.txtSplitLength.TabIndex = 5
        Me.txtSplitLength.Tag = "NE;"
        Me.txtSplitLength.Text = ""
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(208, 88)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(72, 16)
        Me.Label12.TabIndex = 60
        Me.Label12.Text = "Length/Size"
        '
        'txtLength
        '
        Me.txtLength.BackColor = System.Drawing.Color.White
        Me.txtLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLength.Enabled = False
        Me.txtLength.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLength.ForeColor = System.Drawing.Color.Black
        Me.txtLength.Location = New System.Drawing.Point(104, 88)
        Me.txtLength.MaxLength = 20
        Me.txtLength.Name = "txtLength"
        Me.txtLength.Size = New System.Drawing.Size(97, 20)
        Me.txtLength.TabIndex = 57
        Me.txtLength.Tag = "NE;"
        Me.txtLength.Text = ""
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(8, 96)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(72, 16)
        Me.Label10.TabIndex = 58
        Me.Label10.Text = "Length/Size"
        '
        'btnSplit
        '
        Me.btnSplit.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnSplit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSplit.Image = CType(resources.GetObject("btnSplit.Image"), System.Drawing.Image)
        Me.btnSplit.Location = New System.Drawing.Point(407, 56)
        Me.btnSplit.Name = "btnSplit"
        Me.btnSplit.Size = New System.Drawing.Size(80, 20)
        Me.btnSplit.TabIndex = 6
        Me.btnSplit.Text = "Split"
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
        Me.txtSplitWeight.Text = ""
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
        Me.txtWeight.Text = ""
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 64)
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
        Me.txtDescription.Text = ""
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
        Me.txtInventoryCode.Text = ""
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
        Me.btnInventoryCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(192, Byte), CType(255, Byte))
        Me.btnInventoryCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInventoryCodeSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInventoryCodeSearch.Location = New System.Drawing.Point(175, 24)
        Me.btnInventoryCodeSearch.Name = "btnInventoryCodeSearch"
        Me.btnInventoryCodeSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnInventoryCodeSearch.TabIndex = 1
        Me.btnInventoryCodeSearch.Text = "..."
        Me.btnInventoryCodeSearch.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.GroupBox1.Controls.Add(Me.txtOriginalInvLength)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.txtOriginalInvWeight)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtOriginalInvCode)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Navy
        Me.GroupBox1.Location = New System.Drawing.Point(8, 208)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(245, 160)
        Me.GroupBox1.TabIndex = 44
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Original Item"
        '
        'txtOriginalInvLength
        '
        Me.txtOriginalInvLength.BackColor = System.Drawing.Color.White
        Me.txtOriginalInvLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOriginalInvLength.Enabled = False
        Me.txtOriginalInvLength.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOriginalInvLength.ForeColor = System.Drawing.Color.Black
        Me.txtOriginalInvLength.Location = New System.Drawing.Point(128, 88)
        Me.txtOriginalInvLength.MaxLength = 20
        Me.txtOriginalInvLength.Name = "txtOriginalInvLength"
        Me.txtOriginalInvLength.Size = New System.Drawing.Size(104, 20)
        Me.txtOriginalInvLength.TabIndex = 61
        Me.txtOriginalInvLength.Tag = "NE;"
        Me.txtOriginalInvLength.Text = ""
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(128, 72)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(72, 16)
        Me.Label13.TabIndex = 62
        Me.Label13.Text = "Length/Size"
        '
        'txtOriginalInvWeight
        '
        Me.txtOriginalInvWeight.BackColor = System.Drawing.Color.White
        Me.txtOriginalInvWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOriginalInvWeight.Enabled = False
        Me.txtOriginalInvWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOriginalInvWeight.ForeColor = System.Drawing.Color.Black
        Me.txtOriginalInvWeight.Location = New System.Drawing.Point(127, 42)
        Me.txtOriginalInvWeight.MaxLength = 20
        Me.txtOriginalInvWeight.Name = "txtOriginalInvWeight"
        Me.txtOriginalInvWeight.Size = New System.Drawing.Size(104, 20)
        Me.txtOriginalInvWeight.TabIndex = 7
        Me.txtOriginalInvWeight.Tag = "NE;"
        Me.txtOriginalInvWeight.Text = ""
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(127, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(89, 16)
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
        Me.txtOriginalInvCode.Location = New System.Drawing.Point(8, 42)
        Me.txtOriginalInvCode.MaxLength = 20
        Me.txtOriginalInvCode.Name = "txtOriginalInvCode"
        Me.txtOriginalInvCode.Size = New System.Drawing.Size(104, 20)
        Me.txtOriginalInvCode.TabIndex = 6
        Me.txtOriginalInvCode.Tag = "NE;"
        Me.txtOriginalInvCode.Text = ""
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(8, 20)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(89, 16)
        Me.Label9.TabIndex = 40
        Me.Label9.Text = "Inventory Code"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.GroupBox2.Controls.Add(Me.txtSplitInvLength)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.txtNewDescription)
        Me.GroupBox2.Controls.Add(Me.txtSplitInvWeight)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.txtSplitInvCode)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.Navy
        Me.GroupBox2.Location = New System.Drawing.Point(264, 208)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(245, 160)
        Me.GroupBox2.TabIndex = 45
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Split Item"
        '
        'txtSplitInvLength
        '
        Me.txtSplitInvLength.BackColor = System.Drawing.Color.White
        Me.txtSplitInvLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSplitInvLength.Enabled = False
        Me.txtSplitInvLength.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSplitInvLength.ForeColor = System.Drawing.Color.Black
        Me.txtSplitInvLength.Location = New System.Drawing.Point(128, 80)
        Me.txtSplitInvLength.MaxLength = 20
        Me.txtSplitInvLength.Name = "txtSplitInvLength"
        Me.txtSplitInvLength.Size = New System.Drawing.Size(104, 20)
        Me.txtSplitInvLength.TabIndex = 59
        Me.txtSplitInvLength.Tag = "NE;"
        Me.txtSplitInvLength.Text = ""
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(128, 64)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(72, 16)
        Me.Label11.TabIndex = 60
        Me.Label11.Text = "Length/Size"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(8, 96)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(152, 16)
        Me.Label7.TabIndex = 48
        Me.Label7.Text = "New Description"
        '
        'txtNewDescription
        '
        Me.txtNewDescription.BackColor = System.Drawing.Color.White
        Me.txtNewDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewDescription.Enabled = False
        Me.txtNewDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewDescription.ForeColor = System.Drawing.Color.Black
        Me.txtNewDescription.Location = New System.Drawing.Point(8, 120)
        Me.txtNewDescription.MaxLength = 50
        Me.txtNewDescription.Name = "txtNewDescription"
        Me.txtNewDescription.Size = New System.Drawing.Size(192, 20)
        Me.txtNewDescription.TabIndex = 47
        Me.txtNewDescription.Tag = "NE;"
        Me.txtNewDescription.Text = ""
        '
        'txtSplitInvWeight
        '
        Me.txtSplitInvWeight.BackColor = System.Drawing.Color.White
        Me.txtSplitInvWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSplitInvWeight.Enabled = False
        Me.txtSplitInvWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSplitInvWeight.ForeColor = System.Drawing.Color.Black
        Me.txtSplitInvWeight.Location = New System.Drawing.Point(128, 40)
        Me.txtSplitInvWeight.MaxLength = 20
        Me.txtSplitInvWeight.Name = "txtSplitInvWeight"
        Me.txtSplitInvWeight.Size = New System.Drawing.Size(104, 20)
        Me.txtSplitInvWeight.TabIndex = 9
        Me.txtSplitInvWeight.Tag = "NE;"
        Me.txtSplitInvWeight.Text = ""
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(128, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 16)
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
        Me.txtSplitInvCode.Location = New System.Drawing.Point(8, 40)
        Me.txtSplitInvCode.MaxLength = 20
        Me.txtSplitInvCode.Name = "txtSplitInvCode"
        Me.txtSplitInvCode.Size = New System.Drawing.Size(104, 20)
        Me.txtSplitInvCode.TabIndex = 8
        Me.txtSplitInvCode.Tag = "NE;"
        Me.txtSplitInvCode.Text = ""
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(8, 18)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(89, 16)
        Me.Label6.TabIndex = 44
        Me.Label6.Text = "Inventory Code"
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.Location = New System.Drawing.Point(272, 376)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.TabIndex = 46
        Me.btnSave.Text = "Update(F7)"
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
        'frmSplitInventoryML
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(522, 400)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnClose)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmSplitInventoryML"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Split & Retrun Inventory"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Dim objClsSplit As New ClsSliptInventory
    Dim sClass As String
    Dim sMcCost As Double
    Dim sMcPrice As Double
    Dim sTotalCost As Double
    Dim MCTitle As String
    Dim dWeight1 As Double
    Dim dWeight2 As Double
    Dim dLength1 As Double
    Dim dLength2 As Double
    Dim dMcCost1 As Double
    Dim dMcCost2 As Double
    Dim dMcCost3 As Double
    Dim dMcPrice1 As Double
    Dim dMcPrice2 As Double
    Dim dMcPrice3 As Double
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

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmSplitInventory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtInventoryCode.Focus()
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
        Dim dsInv As New DataSet
        Try
            If objClsSplit.IsInventory(txtInventoryCode.Text.Trim) Then
                dsInv = objClsSplit.GetInventoryDetail(txtInventoryCode.Text.Trim)
                If dsInv.Tables(0).Rows.Count > 0 Then
                    txtOriginalInvCode.Text = txtInventoryCode.Text.Trim
                    txtDescription.Text = Trim(dsInv.Tables(0).Rows(0)("Description") & "")
                    txtLength.Text = Trim(dsInv.Tables(0).Rows(0)("Size") & "")
                    txtWeight.Text = Trim(dsInv.Tables(0).Rows(0)("Weight") & "")
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
                    sClassCode = Trim(dsInv.Tables(0).Rows(0)("ClassCode"))
                    sCatalogueCode = dsInv.Tables(0).Rows(0)("CatalogueCode")
                    sTrayId = Trim(dsInv.Tables(0).Rows(0)("TrayID"))
                    sPMCType = Trim(dsInv.Tables(0).Rows(0)("PMCType") & "")
                    sSMCType = Trim(dsInv.Tables(0).Rows(0)("SMCType") & "")
                    sRemarks = Trim(dsInv.Tables(0).Rows(0)("REMARKS") & "")
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
        txtLength.Clear()
        txtWeight.Clear()
        txtSplitLength.Clear()
        txtSplitWeight.Clear()
        txtOriginalInvLength.Clear()
        txtOriginalInvCode.Clear()
        txtOriginalInvWeight.Clear()
        txtSplitInvCode.Clear()
        txtSplitInvLength.Clear()
        txtSplitInvWeight.Clear()
        txtNewDescription.Clear()
        txtInventoryCode.Focus()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        subClearMe()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        subSave()
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
                MessageBox.Show("Updated sucessfully")
                subClearMe()
            Else
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
            If Not objClsSplit.InventorySaveML(Trim(txtSplitInvCode.Text), _
            Trim(txtNewDescription.Text), Trim(sDiamondDescription), _
            Val(txtSplitInvLength.Text.Trim), (txtSplitInvWeight.Text.Trim), Val(dStoneWeight), Val(dCostPerGram), _
            Val(dPMC), (Val(dSMC)), Val(dPCS), Val(dGoldLoss), Val(dTotalCost), _
            Trim(sJobsheetNo), Trim(sPurchaseBillNo), Trim(dPurchaseDate), Trim(sSupplierCode), _
            "", "", 0, 0, 0, Trim(sCategoryCode), Trim(sClassCode), Trim(sCatalogueCode), Trim(sTrayId), _
            Trim$(sPMCType), Trim$(sSMCType), CLng(dPCS), 0, Trim(sRemarks)) Then
                Exit Function
            End If
            If Not objClsSplit.SplitInventorySaveML(Trim(txtInventoryCode.Text), Trim(txtDescription.Text), _
            Val(txtLength.Text.Trim), (txtWeight.Text.Trim), Val(dOriginalPMC), Val(dOriginalSMC), _
            Val(dOriginalGoldLoss), Val(dOriginalTotalCost), _
            Val(txtOriginalInvLength.Text.Trim), (txtOriginalInvWeight.Text), Trim(txtSplitInvCode.Text), _
            Trim(txtNewDescription.Text), Val(txtSplitInvLength.Text.Trim), Val(txtSplitInvWeight.Text), Val(dPMC), Val(dSMC), _
            Val(dGoldLoss), Val(dTotalCost)) Then
                Exit Function
            End If

            dPurchaseDate = Format(dPurchaseDate, "dd/MM/yy")
            If Format(Now(), "dd/MM/yy") <> dCreateDate Then
                bPurchase = objClsSplit.PurchaseSave(Trim(sJobsheetNo), _
                            Trim(sPurchaseBillNo), "", Trim(sSupplierCode), Trim(txtSplitInvCode.Text.Trim), _
                            Trim(txtDescription.Text.Trim), Trim(sDiamondDescription), Val(dStoneWeight), _
                            Val(dCostPerGram), Val(txtSplitInvWeight.Text.Trim), Val(dPMC), Val(dPCS), Val(dGoldLoss), Val(dTotalCost), _
                            Trim(sCatalogueCode), Trim(sCategoryCode), Trim(sClassCode), Trim(sTrayId), "", "ST")
            Else
                bPurchase = objClsSplit.PurchaseSave(Trim(sJobsheetNo), _
                            Trim(sPurchaseBillNo), "", Trim(sSupplierCode), Trim(txtSplitInvCode.Text.Trim), _
                            Trim(txtDescription.Text.Trim), Trim(sDiamondDescription), Val(dStoneWeight), _
                            Val(dCostPerGram), Val(txtSplitInvWeight.Text.Trim), Val(dPMC), Val(dPCS), Val(dGoldLoss), Val(dTotalCost), _
                            Trim(sCatalogueCode), Trim(sCategoryCode), Trim(sClassCode), Trim(sTrayId), "", Trim(sRemarks))
            End If
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
        Dim dTemp, dTemp1 As Double
        Try
            If Not (Val(txtSplitWeight.Text.Trim) < Val(txtWeight.Text.Trim)) Then
                MessageBox.Show("Split Weight should be less then Inventory Weight.")
                Exit Sub
                txtSplitWeight.Focus()
            End If
            
            dsSInv = objClsSplit.GetSplitInventoryDetail(txtInventoryCode.Text.Trim, sCategoryCode)
            If dsSInv.Tables(0).Rows.Count > 0 Then
                dWeight1 = Val(txtWeight.Text.Trim) - Val(txtSplitWeight.Text.Trim)
                dLength1 = Val(txtLength.Text.Trim)
                txtOriginalInvWeight.Text = dWeight1
                txtOriginalInvLength.Text = dLength1

                dWeight2 = Val(txtSplitWeight.Text.Trim)
                dLength2 = Val(txtSplitLength.Text.Trim)
                txtSplitInvCode.Text = txtInventoryCode.Text
                txtSplitInvLength.Text = txtSplitLength.Text
                txtSplitInvWeight.Text = dWeight2
                txtSplitInvLength.Text = dLength2
                ''''aru


                dTemp = sMcCost / Val(txtWeight.Text.Trim)


                dMcCost1 = dTemp * dWeight1
                '''aru
                dMcCost1 = dTemp1 * dLength1
                dTemp = sMcPrice / Val(txtWeight.Text.Trim)
                ''''aru


                dMcPrice1 = dTemp * dWeight1
                '''
                dMcPrice2 = dTemp1 * dLength1
                dTemp = sTotalCost / Val(txtWeight.Text.Trim)
                '''

                dTotalCost1 = dTemp * dWeight1


                dMcCost2 = sMcCost - dMcCost1
                dMcPrice2 = sMcPrice - dMcPrice1
                dTotalcost2 = sTotalCost - dTotalCost1
                '''
                dMcCost3 = sMcCost - dMcCost2
                dMcPrice3 = sMcPrice - dMcPrice2

                dWeight1 = Val(txtWeight.Text.Trim) - Val(txtSplitWeight.Text.Trim)
                dLength1 = Val(txtLength.Text.Trim)

                dGoldLoss = (dWeight2 * dsSInv.Tables(0).Rows(0)("GoldLoss")) / Val(txtWeight.Text.Trim)
                dPMC = (dWeight2 * dsSInv.Tables(0).Rows(0)("PMC")) / Val(txtWeight.Text.Trim)
                dSMC = (dWeight2 * dsSInv.Tables(0).Rows(0)("sMc")) / Val(txtWeight.Text.Trim)
                dPCS = dsSInv.Tables(0).Rows(0)("Pcs")
                txtNewDescription.Enabled = True

                If Trim$(dsSInv.Tables(0).Rows(0)("CatalogueCode")) <> "" Then
                    sCatalogueCode = Trim(dsSInv.Tables(0).Rows(0)("CatalogueCode"))
                    sCategoryCode = Trim(dsSInv.Tables(0).Rows(0)("CategoryCode"))
                    If dsSInv.Tables(1).Rows.Count > 0 Then txtSplitInvCode.Text = (dsSInv.Tables(1).Rows(0)("NextInventoryCode"))
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

    Private Sub subPrintBarcode(ByVal sinventorycode As String)
        Try
            Dim dsInv As New DataSet
            Dim dsCls As New DataSet
            Dim dsMCC As New DataSet
            Dim sSelection As String
            Dim CR As New ReportDocument
            Dim frmrep As New frmReports
            Dim sClassName, sChar, sTemp As String
            Dim dNo As Double

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
            If Not ReportLogOnToServer(CR) Then
                Exit Sub
            End If
            'ReportLogOnToServer(CR)
            CR.Refresh()
            frmrep.ReportViewer.ReportSource = CR
            CR.DataDefinition.FormulaFields("Barcode").Text = fncFormatSQLText(Trim(dsInv.Tables(0).Rows(0)("Inventorycode") & ""))
            CR.DataDefinition.FormulaFields("Description").Text = fncFormatSQLText(Trim(dsInv.Tables(0).Rows(0)("Description") & ""))
            CR.DataDefinition.FormulaFields("TotalWeight").Text = fncFormatSQLText("NWT : " & Trim(Format(Val(dsInv.Tables(0).Rows(0)("Weight") & ""), "0.00")) & "gm")
            CR.DataDefinition.FormulaFields("MC").Text = fncFormatSQLText("MC :$ " & Trim(Format(Val(dsInv.Tables(0).Rows(0)("sMC") & ""), "0.00")))
            CR.DataDefinition.FormulaFields("GoldLoss").Text = fncFormatSQLText(Trim(dsInv.Tables(0).Rows(0)("SupplierCode") & "") & ": " & Format(dsInv.Tables(0).Rows(0)("PurchaseDate"), "MM/yyyy") & "(" & Trim(Format(dsInv.Tables(0).Rows(0)("GoldLoss") & "", "0.00")) & "gm")
            CR.DataDefinition.FormulaFields("Wt").Text = fncFormatSQLText("GWT :" & Trim(Format(dsInv.Tables(0).Rows(0)("Weight") & "", "0.00")) & "gm")
            CR.DataDefinition.FormulaFields("ClassCode").Text = fncFormatSQLText(Trim(dsInv.Tables(0).Rows(0)("ClassCode") & ""))
            'CR.PrintToPrinter(1, True, 0, 0)
            frmrep.ShowDialog()
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

    '''arumugam

    Private Sub txtInventoryCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInventoryCode.TextChanged

    End Sub
End Class
