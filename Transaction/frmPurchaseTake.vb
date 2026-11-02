Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmPurchaseTake
    Inherits System.Windows.Forms.Form
    Dim dm As New DataManager
    Dim TotWt As Double
    Dim TotPCS As Double
    Dim dNetWt As Double
    Public dWeightGrd, dTotalCostGrd, dPCSGrd As Double
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents lblTotalPCS As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents lblTotalWeight As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents label16 As System.Windows.Forms.Label
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents lblUnScannedPCS As System.Windows.Forms.Label
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents lblUnscannedWeight As System.Windows.Forms.Label
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents grdStockTake As System.Windows.Forms.DataGridView
    Friend WithEvents cTrayID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cInventorycode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cDesc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cPCS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cTotalCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Dim dNetPcs As Double

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
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblScannedWeight As System.Windows.Forms.Label
    Friend WithEvents lblScannedPCS As System.Windows.Forms.Label
    Friend WithEvents cboCategoryCode As System.Windows.Forms.ComboBox
    Friend WithEvents txtSalesManPassword As System.Windows.Forms.TextBox
    Friend WithEvents cmdUpdateStockTake As System.Windows.Forms.Button
    Friend WithEvents txtTrayID As System.Windows.Forms.TextBox
    Friend WithEvents txtInventoryCode As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtSalesManName As System.Windows.Forms.TextBox
    Friend WithEvents btnList As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblLoadStatus As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPurchaseTake))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.cboCategoryCode = New System.Windows.Forms.ComboBox()
        Me.txtSalesManName = New System.Windows.Forms.TextBox()
        Me.txtSalesManPassword = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTrayID = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtInventoryCode = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmdUpdateStockTake = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnList = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lblScannedPCS = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.lblScannedWeight = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblLoadStatus = New System.Windows.Forms.Label()
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.lblTotalPCS = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.lblTotalWeight = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.label16 = New System.Windows.Forms.Label()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.lblUnScannedPCS = New System.Windows.Forms.Label()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.lblUnscannedWeight = New System.Windows.Forms.Label()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.grdStockTake = New System.Windows.Forms.DataGridView()
        Me.cTrayID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cInventorycode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cDesc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cPCS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cTotalCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel7.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.Panel13.SuspendLayout()
        Me.Panel14.SuspendLayout()
        CType(Me.grdStockTake, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnAdd
        '
        Me.btnAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnAdd.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.Image = CType(resources.GetObject("btnAdd.Image"), System.Drawing.Image)
        Me.btnAdd.Location = New System.Drawing.Point(16, 112)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(240, 72)
        Me.btnAdd.TabIndex = 1
        Me.btnAdd.Text = "Download Purchase Take"
        Me.btnAdd.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Controls.Add(Me.cboCategoryCode)
        Me.Panel1.Controls.Add(Me.txtSalesManName)
        Me.Panel1.Controls.Add(Me.txtSalesManPassword)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(264, 112)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(520, 72)
        Me.Panel1.TabIndex = 59
        Me.Panel1.Visible = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.Location = New System.Drawing.Point(288, 40)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(184, 23)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "Proceed Take"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'cboCategoryCode
        '
        Me.cboCategoryCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCategoryCode.Location = New System.Drawing.Point(112, 40)
        Me.cboCategoryCode.Name = "cboCategoryCode"
        Me.cboCategoryCode.Size = New System.Drawing.Size(168, 21)
        Me.cboCategoryCode.TabIndex = 4
        Me.cboCategoryCode.Text = "cboCategoryCode"
        '
        'txtSalesManName
        '
        Me.txtSalesManName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesManName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesManName.ForeColor = System.Drawing.Color.Black
        Me.txtSalesManName.Location = New System.Drawing.Point(240, 8)
        Me.txtSalesManName.MaxLength = 20
        Me.txtSalesManName.Name = "txtSalesManName"
        Me.txtSalesManName.Size = New System.Drawing.Size(232, 20)
        Me.txtSalesManName.TabIndex = 3
        Me.txtSalesManName.Tag = ""
        '
        'txtSalesManPassword
        '
        Me.txtSalesManPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesManPassword.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesManPassword.ForeColor = System.Drawing.Color.Black
        Me.txtSalesManPassword.Location = New System.Drawing.Point(112, 8)
        Me.txtSalesManPassword.MaxLength = 20
        Me.txtSalesManPassword.Name = "txtSalesManPassword"
        Me.txtSalesManPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtSalesManPassword.Size = New System.Drawing.Size(120, 20)
        Me.txtSalesManPassword.TabIndex = 2
        Me.txtSalesManPassword.Tag = ""
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(8, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 24)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "JobSheet No"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Password"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTrayID
        '
        Me.txtTrayID.BackColor = System.Drawing.Color.White
        Me.txtTrayID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTrayID.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTrayID.ForeColor = System.Drawing.Color.Black
        Me.txtTrayID.Location = New System.Drawing.Point(80, 439)
        Me.txtTrayID.MaxLength = 20
        Me.txtTrayID.Name = "txtTrayID"
        Me.txtTrayID.Size = New System.Drawing.Size(61, 20)
        Me.txtTrayID.TabIndex = 6
        Me.txtTrayID.Tag = ""
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.White
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(-8, 439)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 24)
        Me.Label3.TabIndex = 60
        Me.Label3.Text = "Tray ID"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtInventoryCode
        '
        Me.txtInventoryCode.BackColor = System.Drawing.Color.White
        Me.txtInventoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInventoryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInventoryCode.ForeColor = System.Drawing.Color.Black
        Me.txtInventoryCode.Location = New System.Drawing.Point(265, 439)
        Me.txtInventoryCode.MaxLength = 20
        Me.txtInventoryCode.Name = "txtInventoryCode"
        Me.txtInventoryCode.Size = New System.Drawing.Size(114, 20)
        Me.txtInventoryCode.TabIndex = 7
        Me.txtInventoryCode.Tag = ""
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.White
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(147, 439)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(112, 24)
        Me.Label4.TabIndex = 62
        Me.Label4.Text = "Inventory Code"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmdUpdateStockTake
        '
        Me.cmdUpdateStockTake.BackColor = System.Drawing.Color.Silver
        Me.cmdUpdateStockTake.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUpdateStockTake.Image = CType(resources.GetObject("cmdUpdateStockTake.Image"), System.Drawing.Image)
        Me.cmdUpdateStockTake.Location = New System.Drawing.Point(217, 627)
        Me.cmdUpdateStockTake.Name = "cmdUpdateStockTake"
        Me.cmdUpdateStockTake.Size = New System.Drawing.Size(120, 23)
        Me.cmdUpdateStockTake.TabIndex = 125
        Me.cmdUpdateStockTake.Text = "Update Stock Take"
        Me.cmdUpdateStockTake.UseVisualStyleBackColor = False
        Me.cmdUpdateStockTake.Visible = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.Silver
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(25, 627)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(80, 23)
        Me.btnClose.TabIndex = 9
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnList
        '
        Me.btnList.BackColor = System.Drawing.Color.Silver
        Me.btnList.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnList.Image = CType(resources.GetObject("btnList.Image"), System.Drawing.Image)
        Me.btnList.Location = New System.Drawing.Point(122, 627)
        Me.btnList.Name = "btnList"
        Me.btnList.Size = New System.Drawing.Size(80, 23)
        Me.btnList.TabIndex = 8
        Me.btnList.Text = "List"
        Me.btnList.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Location = New System.Drawing.Point(24, 656)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(760, 64)
        Me.Panel2.TabIndex = 128
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(544, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(152, 32)
        Me.Label7.TabIndex = 3
        Me.Label7.Text = "F8 :Exit"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(280, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(152, 32)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "F2 :Tray Input"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(16, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(152, 32)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "F1 :List"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.lblScannedPCS)
        Me.Panel3.Controls.Add(Me.Panel6)
        Me.Panel3.Controls.Add(Me.Panel5)
        Me.Panel3.Controls.Add(Me.Panel4)
        Me.Panel3.Location = New System.Drawing.Point(254, 510)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(203, 101)
        Me.Panel3.TabIndex = 129
        '
        'lblScannedPCS
        '
        Me.lblScannedPCS.BackColor = System.Drawing.Color.White
        Me.lblScannedPCS.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScannedPCS.ForeColor = System.Drawing.Color.Black
        Me.lblScannedPCS.Location = New System.Drawing.Point(112, 64)
        Me.lblScannedPCS.Name = "lblScannedPCS"
        Me.lblScannedPCS.Size = New System.Drawing.Size(88, 24)
        Me.lblScannedPCS.TabIndex = 134
        Me.lblScannedPCS.Text = "0"
        Me.lblScannedPCS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel6
        '
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Controls.Add(Me.lblScannedWeight)
        Me.Panel6.Location = New System.Drawing.Point(0, 48)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(104, 64)
        Me.Panel6.TabIndex = 133
        '
        'lblScannedWeight
        '
        Me.lblScannedWeight.BackColor = System.Drawing.Color.White
        Me.lblScannedWeight.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScannedWeight.ForeColor = System.Drawing.Color.Black
        Me.lblScannedWeight.Location = New System.Drawing.Point(8, 16)
        Me.lblScannedWeight.Name = "lblScannedWeight"
        Me.lblScannedWeight.Size = New System.Drawing.Size(88, 24)
        Me.lblScannedWeight.TabIndex = 2
        Me.lblScannedWeight.Text = "0.00"
        Me.lblScannedWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel5
        '
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.Label8)
        Me.Panel5.Location = New System.Drawing.Point(0, -1)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(104, 49)
        Me.Panel5.TabIndex = 132
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.White
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(7, 6)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(88, 34)
        Me.Label8.TabIndex = 1
        Me.Label8.Text = "Scanned Weight"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Label9)
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(238, 48)
        Me.Panel4.TabIndex = 131
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.White
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(112, 8)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(88, 24)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "Scanned PCS"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Red
        Me.Label12.Location = New System.Drawing.Point(16, 463)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(352, 32)
        Me.Label12.TabIndex = 131
        '
        'lblLoadStatus
        '
        Me.lblLoadStatus.AutoSize = True
        Me.lblLoadStatus.BackColor = System.Drawing.Color.White
        Me.lblLoadStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLoadStatus.ForeColor = System.Drawing.Color.Red
        Me.lblLoadStatus.Location = New System.Drawing.Point(536, 551)
        Me.lblLoadStatus.Name = "lblLoadStatus"
        Me.lblLoadStatus.Size = New System.Drawing.Size(0, 13)
        Me.lblLoadStatus.TabIndex = 132
        Me.lblLoadStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(16, 6)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 100)
        Me.pic.TabIndex = 133
        Me.pic.TabStop = False
        '
        'Panel7
        '
        Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel7.Controls.Add(Me.lblTotalPCS)
        Me.Panel7.Controls.Add(Me.Panel8)
        Me.Panel7.Controls.Add(Me.Panel9)
        Me.Panel7.Controls.Add(Me.Panel10)
        Me.Panel7.Location = New System.Drawing.Point(25, 509)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(198, 101)
        Me.Panel7.TabIndex = 134
        '
        'lblTotalPCS
        '
        Me.lblTotalPCS.BackColor = System.Drawing.Color.White
        Me.lblTotalPCS.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalPCS.ForeColor = System.Drawing.Color.Black
        Me.lblTotalPCS.Location = New System.Drawing.Point(112, 64)
        Me.lblTotalPCS.Name = "lblTotalPCS"
        Me.lblTotalPCS.Size = New System.Drawing.Size(88, 24)
        Me.lblTotalPCS.TabIndex = 134
        Me.lblTotalPCS.Text = "0"
        Me.lblTotalPCS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel8
        '
        Me.Panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel8.Controls.Add(Me.lblTotalWeight)
        Me.Panel8.Location = New System.Drawing.Point(0, 48)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(104, 64)
        Me.Panel8.TabIndex = 133
        '
        'lblTotalWeight
        '
        Me.lblTotalWeight.BackColor = System.Drawing.Color.White
        Me.lblTotalWeight.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalWeight.ForeColor = System.Drawing.Color.Black
        Me.lblTotalWeight.Location = New System.Drawing.Point(8, 16)
        Me.lblTotalWeight.Name = "lblTotalWeight"
        Me.lblTotalWeight.Size = New System.Drawing.Size(88, 24)
        Me.lblTotalWeight.TabIndex = 2
        Me.lblTotalWeight.Text = "0.00"
        Me.lblTotalWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel9
        '
        Me.Panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel9.Controls.Add(Me.Label15)
        Me.Panel9.Location = New System.Drawing.Point(0, -1)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(104, 49)
        Me.Panel9.TabIndex = 132
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.White
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(7, 11)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(88, 24)
        Me.Label15.TabIndex = 1
        Me.Label15.Text = "Total Weight"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel10
        '
        Me.Panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel10.Controls.Add(Me.label16)
        Me.Panel10.Location = New System.Drawing.Point(0, 0)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(238, 48)
        Me.Panel10.TabIndex = 131
        '
        'label16
        '
        Me.label16.BackColor = System.Drawing.Color.White
        Me.label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label16.ForeColor = System.Drawing.Color.Black
        Me.label16.Location = New System.Drawing.Point(112, 8)
        Me.label16.Name = "label16"
        Me.label16.Size = New System.Drawing.Size(88, 24)
        Me.label16.TabIndex = 1
        Me.label16.Text = "Total PCS"
        Me.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel11
        '
        Me.Panel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel11.Controls.Add(Me.lblUnScannedPCS)
        Me.Panel11.Controls.Add(Me.Panel12)
        Me.Panel11.Controls.Add(Me.Panel13)
        Me.Panel11.Controls.Add(Me.Panel14)
        Me.Panel11.Location = New System.Drawing.Point(505, 510)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(203, 101)
        Me.Panel11.TabIndex = 135
        '
        'lblUnScannedPCS
        '
        Me.lblUnScannedPCS.BackColor = System.Drawing.Color.White
        Me.lblUnScannedPCS.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnScannedPCS.ForeColor = System.Drawing.Color.Black
        Me.lblUnScannedPCS.Location = New System.Drawing.Point(112, 64)
        Me.lblUnScannedPCS.Name = "lblUnScannedPCS"
        Me.lblUnScannedPCS.Size = New System.Drawing.Size(88, 24)
        Me.lblUnScannedPCS.TabIndex = 134
        Me.lblUnScannedPCS.Text = "0"
        Me.lblUnScannedPCS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel12
        '
        Me.Panel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel12.Controls.Add(Me.lblUnscannedWeight)
        Me.Panel12.Location = New System.Drawing.Point(0, 48)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(104, 64)
        Me.Panel12.TabIndex = 133
        '
        'lblUnscannedWeight
        '
        Me.lblUnscannedWeight.BackColor = System.Drawing.Color.White
        Me.lblUnscannedWeight.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnscannedWeight.ForeColor = System.Drawing.Color.Black
        Me.lblUnscannedWeight.Location = New System.Drawing.Point(8, 16)
        Me.lblUnscannedWeight.Name = "lblUnscannedWeight"
        Me.lblUnscannedWeight.Size = New System.Drawing.Size(88, 24)
        Me.lblUnscannedWeight.TabIndex = 2
        Me.lblUnscannedWeight.Text = "0.00"
        Me.lblUnscannedWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel13
        '
        Me.Panel13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel13.Controls.Add(Me.Label17)
        Me.Panel13.Location = New System.Drawing.Point(0, -1)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(104, 49)
        Me.Panel13.TabIndex = 132
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.White
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(7, 6)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(88, 34)
        Me.Label17.TabIndex = 1
        Me.Label17.Text = "UnScanned Weight"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel14
        '
        Me.Panel14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel14.Controls.Add(Me.Label18)
        Me.Panel14.Location = New System.Drawing.Point(0, 0)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(238, 48)
        Me.Panel14.TabIndex = 131
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.White
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(112, 8)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(88, 31)
        Me.Label18.TabIndex = 1
        Me.Label18.Text = "UnScanned PCS"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grdStockTake
        '
        Me.grdStockTake.AllowUserToAddRows = False
        Me.grdStockTake.AllowUserToDeleteRows = False
        Me.grdStockTake.AllowUserToResizeColumns = False
        Me.grdStockTake.AllowUserToResizeRows = False
        Me.grdStockTake.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdStockTake.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdStockTake.ColumnHeadersHeight = 28
        Me.grdStockTake.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdStockTake.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cTrayID, Me.cInventorycode, Me.cDesc, Me.cWeight, Me.cCost, Me.cPCS, Me.cTotalCost})
        Me.grdStockTake.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdStockTake.Location = New System.Drawing.Point(16, 190)
        Me.grdStockTake.Name = "grdStockTake"
        Me.grdStockTake.RowHeadersVisible = False
        Me.grdStockTake.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdStockTake.Size = New System.Drawing.Size(768, 243)
        Me.grdStockTake.TabIndex = 136
        '
        'cTrayID
        '
        Me.cTrayID.HeaderText = "TrayID"
        Me.cTrayID.Name = "cTrayID"
        Me.cTrayID.Width = 70
        '
        'cInventorycode
        '
        Me.cInventorycode.HeaderText = "Inventory Code"
        Me.cInventorycode.Name = "cInventorycode"
        Me.cInventorycode.Width = 130
        '
        'cDesc
        '
        Me.cDesc.HeaderText = "Description"
        Me.cDesc.Name = "cDesc"
        Me.cDesc.Width = 200
        '
        'cWeight
        '
        Me.cWeight.HeaderText = "Weight"
        Me.cWeight.Name = "cWeight"
        Me.cWeight.Width = 70
        '
        'cCost
        '
        Me.cCost.HeaderText = "Cost Per Gram"
        Me.cCost.Name = "cCost"
        Me.cCost.Width = 130
        '
        'cPCS
        '
        Me.cPCS.HeaderText = "PCS"
        Me.cPCS.Name = "cPCS"
        Me.cPCS.Width = 50
        '
        'cTotalCost
        '
        Me.cTotalCost.HeaderText = "Total Cost"
        Me.cTotalCost.Name = "cTotalCost"
        '
        'frmPurchaseTake
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(792, 732)
        Me.Controls.Add(Me.grdStockTake)
        Me.Controls.Add(Me.Panel11)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.lblLoadStatus)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.btnList)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.cmdUpdateStockTake)
        Me.Controls.Add(Me.txtInventoryCode)
        Me.Controls.Add(Me.txtTrayID)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnAdd)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmPurchaseTake"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Purchase Take"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel7.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.Panel12.ResumeLayout(False)
        Me.Panel13.ResumeLayout(False)
        Me.Panel14.ResumeLayout(False)
        CType(Me.grdStockTake, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Panel1.Visible = True
        txtSalesManPassword.Focus()
        If cboCategoryCode.Items.Count > 0 Then cboCategoryCode.SelectedIndex = -1
        cboCategoryCode.Text = "ALL"
    End Sub
    Public Sub AddItemstoCombo()
        dm.SetMasterDataSet(" Select * from tblJobsheetHeader", "tblJobsheetHeader")
        dm.FillDatasToComboListWithFilter(cboCategoryCode, "tblJobsheetHeader", "JobsheetNo", "JobsheetDate")
    End Sub

    Private Sub frmStockTake_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AddItemstoCombo()
        'subGridSetting(grdStockTake)
        If cboCategoryCode.Items.Count > 0 Then cboCategoryCode.SelectedIndex = -1
        grdStockTake.RowCount = 0
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        ''Dim Ds As DataSet
        ''Dim Ds1 As DataSet
        ''Dim objInventory As New clsInventory
        ''Dim SQry As String
        ''Dim msg, style, Response
        ''If objInventory.UserLogSave(txtSalesManName.Text.Trim, Trim(cboCategoryCode.Text)) Then
        ''Else
        ''    MessageBox.Show("Error in Log Save")
        ''End If
        ' ''Ds = objInventory.CheckStock()
        ' ''If Ds.Tables("tblStock").Rows.Count > 0 Then
        ' ''    msg = "Stock take already Started ...Click Yes to Continue;Click No to start new stock take;Click cancel to Cancel To Transaction"
        ' ''    style = vbYesNoCancel + vbCritical + vbDefaultButton2
        ' ''    Response = MsgBox(msg, style)
        ' ''    'HAJ 
        ' ''    Me.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ' ''    grdStockTake.RowCount = 0
        ' ''    lblLoadStatus.Refresh()
        ' ''    lblLoadStatus.Text = "Loading... "
        ' ''    If Response = vbYes Then
        ' ''        grdStockTake.RowCount = 0
        ' ''        If cboCategoryCode.Text.Trim = "ALL" Then
        ' ''            objInventory.StockDeleteForTrayX()
        ' ''            subCreateStock()
        ' ''            txtTrayID.Focus()
        ' ''        Else
        ' ''            objInventory.StockDeleteForTrayX()
        ' ''            subCreateStock()
        ' ''            txtTrayID.Focus()
        ' ''        End If
        ' ''    ElseIf Response = vbNo Then
        ' ''        'HAJ 
        ' ''        lblLoadStatus.Refresh()
        ' ''        lblLoadStatus.Text = "Downloading Progress... "
        ' ''        grdStockTake.RowCount = 0
        ' ''        objInventory.StockDelete()
        ' ''        subCreateStock()
        ' ''        txtTrayID.Focus()
        ' ''    Else
        ' ''        Me.Close()
        ' ''    End If
        ' ''    subCalcTotalPCSandWeightTotal()
        ' ''Else
        ' ''    msg = "Do you want to Continue ?"
        ' ''    style = vbYesNo + vbCritical + vbDefaultButton2
        ' ''    Response = MsgBox(msg, style)
        ' ''    If Response = vbYes Then
        ' ''        objInventory.StockDelete()
        ' ''        subCreateStock()
        ' ''    End If
        ' ''End If
        ''objInventory.PurchaseStockDelete()
        ''subCreatePurchaseStock()
        ''subCalcTotalPCSandWeightTotalPurchase()
        ''lblLoadStatus.Text = ""
        ''Me.Cursor.Current = System.Windows.Forms.Cursors.Default

        Dim Ds As DataSet
        Dim Ds1 As DataSet
        Dim objInventory As New clsInventory
        Dim SQry As String
        Dim msg, style, Response
        If objInventory.UserLogSave(txtSalesManName.Text.Trim, Trim(cboCategoryCode.Text)) Then
        Else
            MessageBox.Show("Error in Log Save")
        End If
        Ds = objInventory.CheckPurchaseStock()
        If Ds.Tables("tblStock").Rows.Count > 0 Then
            msg = "Purchase Stock take already Started ...Click Yes to Continue;Click No to start new purchase stock take;Click cancel to Cancel To Transaction"
            style = vbYesNoCancel + vbCritical + vbDefaultButton2
            Response = MsgBox(msg, style)
            ''HAJ 
            Me.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            grdStockTake.RowCount = 0
            lblLoadStatus.Refresh()
            lblLoadStatus.Text = "Loading... "
            If Response = vbYes Then
                grdStockTake.RowCount = 0
                If cboCategoryCode.Text.Trim = "ALL" Then
                    'objInventory.StockDeleteForTrayX()
                    subCreatePurchaseStock()
                    txtTrayID.Focus()
                Else
                    '  objInventory.StockDeleteForTrayX()
                    subCreatePurchaseStock()
                    txtTrayID.Focus()
                End If
            ElseIf Response = vbNo Then
                ''HAJ 
                lblLoadStatus.Refresh()
                lblLoadStatus.Text = "Stock Downloading Progress... "
                grdStockTake.RowCount = 0
                objInventory.PurchaseStockDelete()
                subCreatePurchaseStock()
                txtTrayID.Focus()
            Else
                Me.Close()
            End If
            subCalcTotalPCSandWeightTotalPurchase()
        Else
            msg = "Do you want to Continue ?"
            style = vbYesNo + vbCritical + vbDefaultButton2
            Response = MsgBox(msg, style)
            If Response = vbYes Then
                objInventory.PurchaseStockDelete()
                subCreatePurchaseStock()
            End If
        End If
        subCalcTotalPCSandWeightTotalPurchase()
        lblLoadStatus.Text = ""
        Me.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub subCreateStock()
        Dim DS As DataSet
        Dim DS1 As DataSet
        Dim objInventory As New clsInventory
        If objInventory.fncStock(cboCategoryCode.Text.Trim, sUserID) Then
            If objInventory.UpdateModifyUser(cboCategoryCode.Text.Trim, sUserID) Then
            End If
        End If

    End Sub
    Private Sub subCreatePurchaseStock()
        Dim DS As DataSet
        Dim DS1 As DataSet
        Dim objInventory As New clsInventory
        If objInventory.fncPurchaseStock(cboCategoryCode.Text.Trim, sUserID) Then
            If objInventory.UpdatePurchaseModifyUser(cboCategoryCode.Text.Trim, sUserID) Then
            End If
        End If

    End Sub
    Private Sub cmdUpdateStockTake_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdateStockTake.Click
        Dim objinventory As New clsInventory
        If objinventory.UpdateStockTake() Then
            MsgBox("Stock Take Updated")
        End If
    End Sub
    Private Sub txtTrayID_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTrayID.LostFocus

    End Sub

    'Private Sub subTotal(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try

    '        grdStockTake.MaxRows = grdStockTake.MaxRows + 1


    '        Call SetText(grdStockTake, grdStockTake.MaxRows, 1, "Total:")

    '        grdStockTake.ForeColor = Color.Red
    '        grdStockTake.FontBold = True

    '        grdStockTake.Col = 4
    '        grdStockTake.Formula = "sum(d1:d" & grdStockTake.MaxRows - 1 & ")"

    '        grdStockTake.ForeColor = Color.Red
    '        grdStockTake.FontBold = True

    '        grdStockTake.Col = 5
    '        grdStockTake.Formula = "sum(e1:e" & grdStockTake.MaxRows - 1 & ")"

    '        grdStockTake.ForeColor = Color.Red
    '        grdStockTake.FontBold = True

    '        grdStockTake.Col = 6
    '        grdStockTake.Formula = "sum(f1:f" & grdStockTake.MaxRows - 1 & ")"

    '        grdStockTake.ForeColor = Color.Red
    '        grdStockTake.FontBold = True

    '        grdStockTake.Col = 7
    '        grdStockTake.Formula = "sum(g1:g" & grdStockTake.MaxRows - 1 & ")"


    '        grdStockTake.ForeColor = Color.Red
    '        grdStockTake.FontBold = True


    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "GoldPOS")
    '    End Try
    'End Sub
    Private Sub subTotal()
        Try
            If grdStockTake.RowCount = 0 Then
                Exit Sub
            End If
            grdStockTake.Rows.Add()
            Call fnSetdgvText(grdStockTake, grdStockTake.RowCount - 1, 0, "TOTAL:")
            Call fnSetdgvText(grdStockTake, grdStockTake.RowCount - 1, 3, Format(dWeightGrd, "0.00"))
            Call fnSetdgvText(grdStockTake, grdStockTake.RowCount - 1, 5, Format(dPCSGrd, "0.00"))
            Call fnSetdgvText(grdStockTake, grdStockTake.RowCount - 1, 6, Format(dTotalCostGrd, "0.00"))

            Me.grdStockTake.Rows(grdStockTake.RowCount - 1).Cells(0).Style.ForeColor = Color.Red
            Me.grdStockTake.Rows(grdStockTake.RowCount - 1).Cells(0).Style.Font = New Font(grdStockTake.Font.FontFamily, 9, FontStyle.Bold)

            Me.grdStockTake.Rows(grdStockTake.RowCount - 1).Cells(3).Style.ForeColor = Color.Red
            Me.grdStockTake.Rows(grdStockTake.RowCount - 1).Cells(3).Style.Font = New Font(grdStockTake.Font.FontFamily, 9, FontStyle.Bold)

            Me.grdStockTake.Rows(grdStockTake.RowCount - 1).Cells(5).Style.ForeColor = Color.Red
            Me.grdStockTake.Rows(grdStockTake.RowCount - 1).Cells(5).Style.Font = New Font(grdStockTake.Font.FontFamily, 9, FontStyle.Bold)

            Me.grdStockTake.Rows(grdStockTake.RowCount - 1).Cells(6).Style.ForeColor = Color.Red
            Me.grdStockTake.Rows(grdStockTake.RowCount - 1).Cells(6).Style.Font = New Font(grdStockTake.Font.FontFamily, 9, FontStyle.Bold)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        Finally

        End Try
    End Sub


    Private Sub subGetSalesManPassword()
        Dim objSalesman As New clsSalesMan
        DS = objSalesman.GetSalesManwithPassword(txtSalesManPassword.Text)
        If DS.Tables("Salesman").Rows.Count > 0 Then
            txtSalesManName.Text = Trim(DS.Tables("Salesman").Rows(0)("Salesmanname") & "")
        Else
            MsgBox("Password Incorrect ....Please Key in Password Again")
            txtSalesManPassword.Focus()
            ' Exit Sub
        End If

    End Sub


    Private Sub txtSalesManPassword_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSalesManPassword.LostFocus
        subGetSalesManPassword()
    End Sub

    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnList.Click
        Dim objclscompany As New clsCompany
        Dim sSql As String
        Dim sCompanyname, sAddress, sReportTitle, sPath, sPhone, sFax, sSelectFormula, sPrintFDate, sPrintTDate, sFromDate, sToDate As String
        Dim CR As New ReportDocument
        Dim frmRep As New frmReports
        Dim SDateFrom As String
        Dim SDateTo As String
        Dim sSelectFld As String
        Dim sExecSql As String
        Dim ReportTitle As String
        Try

            sReportTitle = "Inventory Stock Detail "
            sPath = Application.StartupPath & "\Reports\rptStockTake.rpt"
            sSql = " {tblStock.TrayID}=" & fncFormatSQLText(Microsoft.VisualBasic.UCase(txtTrayID.Text))
            ReportTitle = "Stock Tray List"
            DS = objclscompany.getCompanyName
            If DS.Tables("GetCompanyName").Rows.Count > 0 Then
                sCompanyname = Trim(DS.Tables("GetCompanyName").Rows(0)("Name") & "")
                sAddress = Trim(DS.Tables("GetCompanyName").Rows(0)("Address1") & "") & " " & _
                             Trim(DS.Tables("GetCompanyName").Rows(0)("Address2") & "") & " " & _
                             Trim(DS.Tables("GetCompanyName").Rows(0)("Country") & "")
                sPhone = Trim(DS.Tables("GetCompanyName").Rows(0)("Phone") & "")
                sFax = Trim(DS.Tables("GetCompanyName").Rows(0)("Fax") & "")
            End If
            CR.Load(sPath)
            If Not ReportLogOnToServer(CR) Then
                Exit Sub
            End If
            'ReportLogOnToServer(CR)
            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(sCompanyname)
            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(sAddress)
            CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText(ReportTitle)
            CR.DataDefinition.FormulaFields("RPTSELECTION").Text = fncFormatSQLText(sSelectFld)
            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(sPhone)
            CR.DataDefinition.FormulaFields("CompAddress4").Text = fncFormatSQLText(sFax)
            CR.DataDefinition.RecordSelectionFormula = sSql
            CR.Refresh()
            frmRep.ReportViewer.ReportSource = CR

            '==== Print/ Preview Option========


            frmRep.ShowDialog()
            frmRep.ReportViewer.Visible = True
            frmRep.ReportViewer.CloseView(False)
            frmRep.WindowState = FormWindowState.Maximized


        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
            CR.Close()
            CR = Nothing
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub frmStockTake_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                SendKeys.Send("{Tab}")
            Case Keys.Up
                SendKeys.Send("+{Tab}")
            Case Keys.Down
                SendKeys.Send("{Tab}")
            Case Keys.F1
                btnList_Click(sender, e)
            Case Keys.F2
                txtTrayID.Focus()
            Case Keys.F8
                btnClose_Click(sender, e)
        End Select
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    ''Calculating Weight from Database
    Private Sub subCalcTotalPCSandWeight()
        Dim Dr() As DataRow
        TotWt = 0
        TotPCS = 0
        Dr = dm.RowReturnProjExecNonQryWithDataRow("select Weight=sum(weight),CostPerGram=sum(costpergram),Totcost=sum(TotalCost),TotPcs=sum(PCS) from tblstock Where TrayId=" & fncFormatSQLText(Trim(txtTrayID.Text)) & "")
        If Dr.Length > 0 Then
            TotWt = Val(Dr(0)("Weight") & "")
            TotPCS = Val(Trim(Dr(0)("TotPcs") & ""))
            lblScannedWeight.Text = Format(TotWt, "###,##0.00")
            lblScannedPCS.Text = Format(TotPCS, "###,##0")
        End If
        lblUnscannedWeight.Text = Format((lblTotalWeight.Text) - (lblScannedWeight.Text), "###,##0.00")
        lblUnScannedPCS.Text = Format((lblTotalPCS.Text) - (lblScannedPCS.Text), "###,##0")
        Dr = Nothing
    End Sub
    Private Sub subCalcTotalPCSandWeightPurchase()
        Dim Dr() As DataRow
        TotWt = 0
        TotPCS = 0
        Dr = dm.RowReturnProjExecNonQryWithDataRow("select Weight=sum(weight),CostPerGram=sum(costpergram),Totcost=sum(TotalCost),TotPcs=sum(PCS) from tblPurchasestock Where TrayId=" & fncFormatSQLText(Trim(txtTrayID.Text)) & "")
        If Dr.Length > 0 Then
            TotWt = Val(Dr(0)("Weight") & "")
            TotPCS = Val(Trim(Dr(0)("TotPcs") & ""))
            lblScannedWeight.Text = Format(TotWt, "###,##0.00")
            lblScannedPCS.Text = Format(TotPCS, "###,##0")
        End If
        lblUnscannedWeight.Text = Format((lblTotalWeight.Text) - (lblScannedWeight.Text), "###,##0.00")
        lblUnScannedPCS.Text = Format((lblTotalPCS.Text) - (lblScannedPCS.Text), "###,##0")
        Dr = Nothing
    End Sub
    Private Sub subCalcTotalPCSandWeightTotal()
        Dim Dr() As DataRow
        TotWt = 0
        TotPCS = 0
        'Dr = dm.RowReturnProjExecNonQryWithDataRow("select Weight=sum(weight),CostPerGram=sum(costpergram),Totcost=sum(TotalCost),TotPcs=sum(PCS) from tblstock Where TrayId='X' and CategoryCode=" & fncFormatSQLText(Trim(cboCategoryCode.Text)) & "")
        Dr = dm.RowReturnProjExecNonQryWithDataRow("select Weight=sum(weight),CostPerGram=sum(costpergram),Totcost=sum(TotalCost),TotPcs=sum(PCS) from tblstock Where CategoryCode=" & fncFormatSQLText(Trim(cboCategoryCode.Text)) & "")
        If Dr.Length > 0 Then
            TotWt = Val(Dr(0)("Weight") & "")
            TotPCS = Val(Trim(Dr(0)("TotPcs") & ""))
            lblTotalWeight.Text = Format(TotWt, "###,##0.00")
            lblTotalPCS.Text = Format(TotPCS, "###,##0")
        End If
        Dr = Nothing
    End Sub
    Private Sub subCalcTotalPCSandWeightTotalPurchase()
        Dim Dr() As DataRow
        TotWt = 0
        TotPCS = 0
        'Dr = dm.RowReturnProjExecNonQryWithDataRow("select Weight=sum(weight),CostPerGram=sum(costpergram),Totcost=sum(TotalCost),TotPcs=sum(PCS) from tblstock Where TrayId='X' and CategoryCode=" & fncFormatSQLText(Trim(cboCategoryCode.Text)) & "")
        Dr = dm.RowReturnProjExecNonQryWithDataRow("select Weight=sum(weight),CostPerGram=sum(costpergram),Totcost=sum(TotalCost),TotPcs=sum(PCS) from tblPurchasestock Where jobsheetno=" & fncFormatSQLText(Trim(cboCategoryCode.Text)) & "")
        If Dr.Length > 0 Then
            TotWt = Val(Dr(0)("Weight") & "")
            TotPCS = Val(Trim(Dr(0)("TotPcs") & ""))
            lblTotalWeight.Text = Format(TotWt, "###,##0.00")
            lblTotalPCS.Text = Format(TotPCS, "###,##0")
        End If
        Dr = Nothing
    End Sub
    Private Sub txtInventoryCode_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInventoryCode.Leave
        Dim i As Long
        Dim j As Long
        Dim bEdited As Boolean
        Dim bStkError As Boolean
        Dim currow As Long
        Dim objInventory As New clsInventory

        If UCase(sCompanyShortCode) <> "ML" Then
            If Val(txtInventoryCode.Text.Trim) = 0 Then
                If (txtInventoryCode.Text.Trim) = "" Then
                    Exit Sub
                End If
                ' txtInventoryCode.Text = 999
            End If

        Else
            If Len(txtInventoryCode.Text.Trim) = 0 Then
                If (txtInventoryCode.Text.Trim) = "" Then
                    Exit Sub
                End If
                txtInventoryCode.Text = 999
            End If
        End If
        If (txtTrayID.Text) = "" Then
            MsgBox("Trayid Cannot be Empty")
            Exit Sub
        End If

        If Microsoft.VisualBasic.UCase(txtTrayID.Text.Trim) = "X" Then
            MsgBox("Items Cannot be Scanned for this Tray....")
            txtInventoryCode.Text = ""
            txtTrayID.Text = ""
            txtTrayID.Focus()
            Exit Sub
        End If


        DS = objInventory.GetInventoryFromStockPurchase(txtInventoryCode.Text.Trim)
        'grdStockTake.MaxRows = 0
        If DS.Tables("Inventory").Rows.Count > 0 Then
            Label12.Text = ""
            If Trim(DS.Tables("Inventory").Rows(i)("Trayid") & "") = "X" Then
                For i = 0 To DS.Tables("Inventory").Rows.Count - 1
                    '  grdStockTake.RowCount = 0
                    'grdStockTake.Row = grdStockTake.MaxRows
                    If grdStockTake.Rows.Count = 0 Then
                        grdStockTake.Rows.Add()
                    End If
                    Me.grdStockTake.Rows(grdStockTake.RowCount - 1).Cells(0).Style.ForeColor = Color.Black
                    Me.grdStockTake.Rows(grdStockTake.RowCount - 1).Cells(0).Style.Font = New Font(grdStockTake.Font.FontFamily, 8, FontStyle.Regular)

                    Me.grdStockTake.Rows(grdStockTake.RowCount - 1).Cells(3).Style.ForeColor = Color.Black
                    Me.grdStockTake.Rows(grdStockTake.RowCount - 1).Cells(3).Style.Font = New Font(grdStockTake.Font.FontFamily, 8, FontStyle.Regular)

                    Me.grdStockTake.Rows(grdStockTake.RowCount - 1).Cells(5).Style.ForeColor = Color.Black
                    Me.grdStockTake.Rows(grdStockTake.RowCount - 1).Cells(5).Style.Font = New Font(grdStockTake.Font.FontFamily, 8, FontStyle.Regular)

                    Me.grdStockTake.Rows(grdStockTake.RowCount - 1).Cells(6).Style.ForeColor = Color.Black
                    Me.grdStockTake.Rows(grdStockTake.RowCount - 1).Cells(6).Style.Font = New Font(grdStockTake.Font.FontFamily, 8, FontStyle.Regular)

                    Call fnSetdgvText(grdStockTake, grdStockTake.RowCount - 1, 0, Microsoft.VisualBasic.UCase(txtTrayID.Text.Trim))
                    Call fnSetdgvText(grdStockTake, grdStockTake.RowCount - 1, 1, Trim(DS.Tables("Inventory").Rows(i)("InventoryCode") & ""))
                    Call fnSetdgvText(grdStockTake, grdStockTake.RowCount - 1, 2, Trim(DS.Tables("Inventory").Rows(i)("Description") & ""))
                    Call fnSetdgvText(grdStockTake, grdStockTake.RowCount - 1, 3, Format(Val(DS.Tables("Inventory").Rows(i)("Weight") & ""), "#0.00"))
                    Call fnSetdgvText(grdStockTake, grdStockTake.RowCount - 1, 4, Format(Val(DS.Tables("Inventory").Rows(i)("CostPerGram") & ""), "#0.00"))
                    Call fnSetdgvText(grdStockTake, grdStockTake.RowCount - 1, 5, Format(Val(DS.Tables("Inventory").Rows(i)("PCS") & ""), "#0.00"))
                    Call fnSetdgvText(grdStockTake, grdStockTake.RowCount - 1, 6, Format(Val(DS.Tables("Inventory").Rows(i)("TotalCost") & ""), "#0.00"))
                Next i
                ''HAJ on 09 Oct 2007
                'grdStockTake.Row = grdStockTake.MaxRows
                'grdStockTake.Col = 1
                'grdStockTake.ForeColor = Color.Black

                'grdStockTake.Col = 4
                'grdStockTake.ForeColor = Color.Black

                'grdStockTake.Col = 5
                'grdStockTake.ForeColor = Color.Black

                'grdStockTake.Col = 6
                'grdStockTake.ForeColor = Color.Black

                'grdStockTake.Col = 7
                'grdStockTake.ForeColor = Color.Black

                'grdStockTake.Col = 8
                'grdStockTake.ForeColor = Color.Black

                ''grdStockTake.MaxRows = grdStockTake.MaxRows + 1

                'grdStockTake.Row = grdStockTake.MaxRows
                'grdStockTake.Col = 1
                'grdStockTake.Action = 0
                subTotalFromGrid()
                subTotal()
                ' subTotal(sender, e)
                'subTotal()
            ElseIf UCase(Trim(DS.Tables("Inventory").Rows(i)("Trayid") & "")) <> "X" Then
                Label12.Text = txtInventoryCode.Text & " Already Scanned"
                txtInventoryCode.Text = ""
                If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
                Exit Sub
            End If
            DS = objInventory.UpdatePurchaseStock(txtInventoryCode.Text, Microsoft.VisualBasic.UCase(txtTrayID.Text), Val(fnGetdgvText(grdStockTake, grdStockTake.RowCount - 1, 4)))
            ''HAJ on 18 Sep
        Else
            Label12.Text = txtInventoryCode.Text & " Not Found "
            '' Insert Missing Inventory to tblError
            bStkError = objInventory.StockError(txtInventoryCode.Text.Trim, Microsoft.VisualBasic.UCase(txtTrayID.Text))
            txtInventoryCode.Text = ""
            txtInventoryCode.Focus()
            Exit Sub
        End If
        'TotWt = 0
        'TotPCS = 0
        subCalcTotalPCSandWeightPurchase()
        txtInventoryCode.Text = ""
        txtInventoryCode.Focus()
    End Sub
    Public Function subTotalFromGrid()
        Dim icnt
        Dim sinventorycode As String
        dWeightGrd = 0
        dTotalCostGrd = 0
        dPCSGrd = 0
        For icnt = 0 To grdStockTake.RowCount - 1
            sinventorycode = Trim(fnGetdgvText(grdStockTake, icnt, 1))
            If sinventorycode <> "TOTAL:" Then
                dWeightGrd = Format(Val(dWeightGrd) + Val(fnGetdgvText(grdStockTake, icnt, 3)), "0.00")
                dPCSGrd = Format(Val(dPCSGrd) + Val(fnGetdgvText(grdStockTake, icnt, 5)), "0.00")
                dTotalCostGrd = Format(Val(dTotalCostGrd) + Val(fnGetdgvText(grdStockTake, icnt, 6)), "0.00")
            End If
        Next
    End Function
    Private Sub txtTrayID_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTrayID.Leave
        Dim objInventory As New clsInventory
        Dim objTray As New clsTray
        Dim i As Long
        Dim bRet As Boolean

        If txtTrayID.Text <> "" Then
            lblScannedWeight.Text = "000.00"
            lblScannedPCS.Text = "000.00"
        End If
        If txtTrayID.Text.Trim <> "" Then
            DS = objTray.GetTraywithCode(txtTrayID.Text.Trim)
            If DS.Tables("Tray").Rows.Count <= 0 Then
                MsgBox("Tray Not Found!", MsgBoxStyle.Critical)
                If txtTrayID.Enabled Then txtTrayID.Focus()
                Exit Sub
            End If
        End If

        Me.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        lblLoadStatus.Refresh()
        lblLoadStatus.Text = "Loading... Wait"
        DS = objInventory.GetPurchaseTray(txtTrayID.Text.Trim)
        ''Haj on 08 Oct 2007
        ''grdStockTake.MaxRows = 0
        If DS.Tables("Tray").Rows.Count > 0 Then
            grdStockTake.RowCount = 0
            For i = 0 To DS.Tables("Tray").Rows.Count - 1
                lblLoadStatus.Refresh()
                lblLoadStatus.Text = "Loading" & Space(5) & i & Space(5) & "of" & Space(5) & DS.Tables("Tray").Rows.Count
                'grdStockTake.MaxRows = grdStockTake.MaxRows + 1
                'grdStockTake.Row = grdStockTake.MaxRows
                grdStockTake.Rows.Add()
                Call fnSetdgvText(grdStockTake, i, 0, Microsoft.VisualBasic.UCase((DS.Tables("Tray").Rows(i)("TrayID") & "")))
                Call fnSetdgvText(grdStockTake, i, 1, Trim(DS.Tables("Tray").Rows(i)("InventoryCode") & ""))
                Call fnSetdgvText(grdStockTake, i, 2, Trim(DS.Tables("Tray").Rows(i)("Description") & ""))
                Call fnSetdgvText(grdStockTake, i, 3, Format(Val(DS.Tables("Tray").Rows(i)("Weight") & ""), "#0.00"))
                Call fnSetdgvText(grdStockTake, i, 4, Format(Val(DS.Tables("Tray").Rows(i)("CostPerGram") & ""), "#0.00"))
                Call fnSetdgvText(grdStockTake, i, 5, Format(Val(DS.Tables("Tray").Rows(i)("PCS") & ""), "#0.00"))
                Call fnSetdgvText(grdStockTake, i, 6, Format(Val(DS.Tables("Tray").Rows(i)("TotalCost") & ""), "#0.00"))
            Next i
            'grdStockTake.Row = grdStockTake.MaxRows
            'grdStockTake.Col = 1
            'grdStockTake.Action = 0
            'subTotal(sender, e)
            subTotalFromGrid()
            subTotal()
            subCalcTotalPCSandWeightPurchase()
            ''TotWt = 0
            ''TotPCS = 0
            ''lblLoadStatus.Refresh()
            ''lblLoadStatus.Text = ""
            ''lblLoadStatus.Text = "Calculation Progress... "
            ''For i = 1 To grdStockTake.MaxRows - 1
            ''    lblLoadStatus.Refresh()
            ''    lblLoadStatus.Text = "Calculation Progress... "
            ''    TotWt = TotWt + GetText(grdStockTake, i, 4)
            ''    TotPCS = TotPCS + 1
            ''    lblScannedWeight.Text = Format(TotWt, "###,##0.00gms")
            ''    Label11.Text = Format(TotPCS, "###,##0")
            ''Next i
        End If
        lblLoadStatus.Refresh()
        lblLoadStatus.Text = ""
        Me.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub txtInventoryCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInventoryCode.TextChanged

    End Sub

    Private Sub txtTrayID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTrayID.TextChanged

    End Sub

    Private Sub cboCategoryCode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCategoryCode.SelectedIndexChanged

    End Sub
End Class
