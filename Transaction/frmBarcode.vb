Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports MSCommLib
Imports System.Drawing.Printing
Imports System
Imports System.IO
Public Class frmBarcode
    Inherits System.Windows.Forms.Form
    Dim masPriceCode(9) As String
    Dim sSupplierCode As String
    Dim sPurchaseDate As String
    Dim dm As New DataManager
    Dim sPrinterName As String
    Dim sSize As String
    Dim sDiamondDes As String
    Dim sClassName As String
    Dim sPMC As String
    Dim sCategorycode As String
    Dim sCatalogueCode As String
    Dim sTrayGroup As String
    Dim sSizeCode As String
    Dim sSizeType As String
    Dim sSalesPCS As Boolean
    Dim sAddDescription1, sAddDescription2, sAddDescription3, sAddDescription4 As String
    Dim sAddCost1, sAddCost2, sAddCost3, sAddCost4 As String
    Dim sAddSellingPrice1, sAddSellingPrice2, sAddSellingPrice3, sAddSellingPrice4 As String
    Dim sStoneWeight, sSMCType As String
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents txtInventoryCode As System.Windows.Forms.TextBox
    Friend WithEvents btnInventoryCodeSearch As System.Windows.Forms.Button
    Friend WithEvents txtPrice As System.Windows.Forms.TextBox
    Dim objClsTray As New clsTray
    Dim sClassCode As String
    Dim objClass As New ClsClass
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnCatagorySearch As System.Windows.Forms.Button
    Friend WithEvents txtCategoryCode As System.Windows.Forms.TextBox
    Public msClsSalesType As String
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
    Friend WithEvents txtCompany As System.Windows.Forms.TextBox
    Friend WithEvents lblLocationCode As System.Windows.Forms.Label
    Friend WithEvents txtBarcode As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtGoldLoss As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtSMC As System.Windows.Forms.TextBox
    Friend WithEvents txtClass As System.Windows.Forms.TextBox
    Friend WithEvents txtNoofLabels As System.Windows.Forms.TextBox
    Friend WithEvents txtTrayID As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblPrice As System.Windows.Forms.Label
    Friend WithEvents btnTraySearch As System.Windows.Forms.Button
    Friend WithEvents txtNewTrayID As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBarcode))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnCatagorySearch = New System.Windows.Forms.Button()
        Me.txtCategoryCode = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtPrice = New System.Windows.Forms.TextBox()
        Me.txtInventoryCode = New System.Windows.Forms.TextBox()
        Me.btnTraySearch = New System.Windows.Forms.Button()
        Me.txtNewTrayID = New System.Windows.Forms.TextBox()
        Me.lblPrice = New System.Windows.Forms.Label()
        Me.txtTrayID = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtNoofLabels = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtClass = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtSMC = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtGoldLoss = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtWeight = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtBarcode = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCompany = New System.Windows.Forms.TextBox()
        Me.lblLocationCode = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnInventoryCodeSearch = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnCatagorySearch)
        Me.GroupBox1.Controls.Add(Me.txtCategoryCode)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtPrice)
        Me.GroupBox1.Controls.Add(Me.txtInventoryCode)
        Me.GroupBox1.Controls.Add(Me.btnTraySearch)
        Me.GroupBox1.Controls.Add(Me.txtNewTrayID)
        Me.GroupBox1.Controls.Add(Me.lblPrice)
        Me.GroupBox1.Controls.Add(Me.txtTrayID)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtNoofLabels)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtClass)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtSMC)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtGoldLoss)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtWeight)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtDescription)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtBarcode)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtCompany)
        Me.GroupBox1.Controls.Add(Me.lblLocationCode)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 100)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(416, 316)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'btnCatagorySearch
        '
        Me.btnCatagorySearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCatagorySearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCatagorySearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCatagorySearch.Location = New System.Drawing.Point(385, 140)
        Me.btnCatagorySearch.Name = "btnCatagorySearch"
        Me.btnCatagorySearch.Size = New System.Drawing.Size(23, 21)
        Me.btnCatagorySearch.TabIndex = 476210
        Me.btnCatagorySearch.Text = "..."
        Me.btnCatagorySearch.UseVisualStyleBackColor = False
        '
        'txtCategoryCode
        '
        Me.txtCategoryCode.BackColor = System.Drawing.Color.White
        Me.txtCategoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCategoryCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCategoryCode.Location = New System.Drawing.Point(279, 140)
        Me.txtCategoryCode.MaxLength = 20
        Me.txtCategoryCode.Name = "txtCategoryCode"
        Me.txtCategoryCode.Size = New System.Drawing.Size(110, 21)
        Me.txtCategoryCode.TabIndex = 476209
        Me.txtCategoryCode.Tag = "NE;"
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(211, 140)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(66, 18)
        Me.Label10.TabIndex = 476208
        Me.Label10.Text = "Category"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPrice
        '
        Me.txtPrice.AcceptsTab = True
        Me.txtPrice.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPrice.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPrice.Enabled = False
        Me.txtPrice.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrice.ForeColor = System.Drawing.Color.Black
        Me.txtPrice.Location = New System.Drawing.Point(144, 318)
        Me.txtPrice.MaxLength = 4
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.Size = New System.Drawing.Size(152, 20)
        Me.txtPrice.TabIndex = 476207
        Me.txtPrice.Tag = "NE"
        Me.txtPrice.Visible = False
        '
        'txtInventoryCode
        '
        Me.txtInventoryCode.AcceptsTab = True
        Me.txtInventoryCode.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtInventoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInventoryCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtInventoryCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInventoryCode.ForeColor = System.Drawing.Color.Black
        Me.txtInventoryCode.Location = New System.Drawing.Point(144, 83)
        Me.txtInventoryCode.MaxLength = 10
        Me.txtInventoryCode.Name = "txtInventoryCode"
        Me.txtInventoryCode.Size = New System.Drawing.Size(264, 20)
        Me.txtInventoryCode.TabIndex = 476206
        Me.txtInventoryCode.Tag = "NE"
        '
        'btnTraySearch
        '
        Me.btnTraySearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnTraySearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTraySearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTraySearch.Location = New System.Drawing.Point(384, 256)
        Me.btnTraySearch.Name = "btnTraySearch"
        Me.btnTraySearch.Size = New System.Drawing.Size(24, 20)
        Me.btnTraySearch.TabIndex = 476205
        Me.btnTraySearch.Text = "..."
        Me.btnTraySearch.UseVisualStyleBackColor = False
        '
        'txtNewTrayID
        '
        Me.txtNewTrayID.BackColor = System.Drawing.Color.White
        Me.txtNewTrayID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewTrayID.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewTrayID.Location = New System.Drawing.Point(307, 256)
        Me.txtNewTrayID.MaxLength = 20
        Me.txtNewTrayID.Name = "txtNewTrayID"
        Me.txtNewTrayID.Size = New System.Drawing.Size(82, 20)
        Me.txtNewTrayID.TabIndex = 476204
        Me.txtNewTrayID.Tag = "NE;"
        '
        'lblPrice
        '
        Me.lblPrice.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrice.ForeColor = System.Drawing.Color.Black
        Me.lblPrice.Location = New System.Drawing.Point(32, 320)
        Me.lblPrice.Name = "lblPrice"
        Me.lblPrice.Size = New System.Drawing.Size(88, 18)
        Me.lblPrice.TabIndex = 476202
        Me.lblPrice.Text = "Price"
        Me.lblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblPrice.Visible = False
        '
        'txtTrayID
        '
        Me.txtTrayID.AcceptsTab = True
        Me.txtTrayID.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtTrayID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTrayID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTrayID.Enabled = False
        Me.txtTrayID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTrayID.ForeColor = System.Drawing.Color.Black
        Me.txtTrayID.Location = New System.Drawing.Point(144, 256)
        Me.txtTrayID.MaxLength = 4
        Me.txtTrayID.Name = "txtTrayID"
        Me.txtTrayID.Size = New System.Drawing.Size(152, 20)
        Me.txtTrayID.TabIndex = 476199
        Me.txtTrayID.Tag = "NE"
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(32, 256)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(89, 18)
        Me.Label9.TabIndex = 476200
        Me.Label9.Text = "Tray ID"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtNoofLabels
        '
        Me.txtNoofLabels.AcceptsTab = True
        Me.txtNoofLabels.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtNoofLabels.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNoofLabels.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNoofLabels.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoofLabels.ForeColor = System.Drawing.Color.Black
        Me.txtNoofLabels.Location = New System.Drawing.Point(144, 288)
        Me.txtNoofLabels.MaxLength = 4
        Me.txtNoofLabels.Name = "txtNoofLabels"
        Me.txtNoofLabels.Size = New System.Drawing.Size(263, 20)
        Me.txtNoofLabels.TabIndex = 9
        Me.txtNoofLabels.Tag = "NE"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(32, 288)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(89, 18)
        Me.Label8.TabIndex = 476198
        Me.Label8.Text = "No of Labels"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtClass
        '
        Me.txtClass.AcceptsTab = True
        Me.txtClass.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtClass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtClass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtClass.Enabled = False
        Me.txtClass.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClass.ForeColor = System.Drawing.Color.Black
        Me.txtClass.Location = New System.Drawing.Point(144, 224)
        Me.txtClass.MaxLength = 4
        Me.txtClass.Name = "txtClass"
        Me.txtClass.Size = New System.Drawing.Size(263, 20)
        Me.txtClass.TabIndex = 8
        Me.txtClass.Tag = "NE"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(32, 227)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(89, 18)
        Me.Label7.TabIndex = 476196
        Me.Label7.Text = "Class"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSMC
        '
        Me.txtSMC.AcceptsTab = True
        Me.txtSMC.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtSMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSMC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSMC.Enabled = False
        Me.txtSMC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMC.ForeColor = System.Drawing.Color.Black
        Me.txtSMC.Location = New System.Drawing.Point(144, 196)
        Me.txtSMC.MaxLength = 4
        Me.txtSMC.Name = "txtSMC"
        Me.txtSMC.Size = New System.Drawing.Size(263, 20)
        Me.txtSMC.TabIndex = 7
        Me.txtSMC.Tag = "NE"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(32, 198)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(89, 18)
        Me.Label6.TabIndex = 476194
        Me.Label6.Text = "SMC"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtGoldLoss
        '
        Me.txtGoldLoss.AcceptsTab = True
        Me.txtGoldLoss.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtGoldLoss.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldLoss.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtGoldLoss.Enabled = False
        Me.txtGoldLoss.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoldLoss.ForeColor = System.Drawing.Color.Black
        Me.txtGoldLoss.Location = New System.Drawing.Point(144, 168)
        Me.txtGoldLoss.MaxLength = 4
        Me.txtGoldLoss.Name = "txtGoldLoss"
        Me.txtGoldLoss.Size = New System.Drawing.Size(263, 20)
        Me.txtGoldLoss.TabIndex = 6
        Me.txtGoldLoss.Tag = "NE"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(32, 171)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(89, 18)
        Me.Label5.TabIndex = 476192
        Me.Label5.Text = "Gold Loss"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtWeight
        '
        Me.txtWeight.AcceptsTab = True
        Me.txtWeight.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWeight.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtWeight.Enabled = False
        Me.txtWeight.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeight.ForeColor = System.Drawing.Color.Black
        Me.txtWeight.Location = New System.Drawing.Point(144, 140)
        Me.txtWeight.MaxLength = 4
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.Size = New System.Drawing.Size(61, 20)
        Me.txtWeight.TabIndex = 5
        Me.txtWeight.Tag = "NE"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(32, 143)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(89, 18)
        Me.Label4.TabIndex = 476190
        Me.Label4.Text = "Weight"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDescription
        '
        Me.txtDescription.AcceptsTab = True
        Me.txtDescription.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescription.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDescription.Enabled = False
        Me.txtDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.ForeColor = System.Drawing.Color.Black
        Me.txtDescription.Location = New System.Drawing.Point(144, 112)
        Me.txtDescription.MaxLength = 4
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(264, 20)
        Me.txtDescription.TabIndex = 4
        Me.txtDescription.Tag = "NE"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(32, 113)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 18)
        Me.Label3.TabIndex = 476188
        Me.Label3.Text = "Description"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(32, 83)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(124, 18)
        Me.Label2.TabIndex = 476185
        Me.Label2.Text = "Inventory Code"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBarcode
        '
        Me.txtBarcode.AcceptsTab = True
        Me.txtBarcode.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBarcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBarcode.Enabled = False
        Me.txtBarcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBarcode.ForeColor = System.Drawing.Color.Black
        Me.txtBarcode.Location = New System.Drawing.Point(144, 52)
        Me.txtBarcode.MaxLength = 4
        Me.txtBarcode.Name = "txtBarcode"
        Me.txtBarcode.Size = New System.Drawing.Size(263, 20)
        Me.txtBarcode.TabIndex = 2
        Me.txtBarcode.Tag = "NE"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(32, 55)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 18)
        Me.Label1.TabIndex = 476184
        Me.Label1.Text = "BarCode"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCompany
        '
        Me.txtCompany.AcceptsTab = True
        Me.txtCompany.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtCompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCompany.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCompany.Enabled = False
        Me.txtCompany.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompany.ForeColor = System.Drawing.Color.Black
        Me.txtCompany.Location = New System.Drawing.Point(144, 24)
        Me.txtCompany.MaxLength = 10
        Me.txtCompany.Name = "txtCompany"
        Me.txtCompany.Size = New System.Drawing.Size(263, 20)
        Me.txtCompany.TabIndex = 1
        Me.txtCompany.Tag = "NE"
        '
        'lblLocationCode
        '
        Me.lblLocationCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationCode.ForeColor = System.Drawing.Color.Black
        Me.lblLocationCode.Location = New System.Drawing.Point(32, 25)
        Me.lblLocationCode.Name = "lblLocationCode"
        Me.lblLocationCode.Size = New System.Drawing.Size(89, 18)
        Me.lblLocationCode.TabIndex = 476182
        Me.lblLocationCode.Text = "Company"
        Me.lblLocationCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(270, 100)
        Me.PictureBox1.TabIndex = 55
        Me.PictureBox1.TabStop = False
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnNew.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.ForeColor = System.Drawing.Color.Black
        Me.btnNew.Location = New System.Drawing.Point(205, 441)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(100, 40)
        Me.btnNew.TabIndex = 61
        Me.btnNew.Text = "Clear (F6)"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPrint.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btnPrint.FlatAppearance.BorderSize = 3
        Me.btnPrint.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.ForeColor = System.Drawing.Color.Black
        Me.btnPrint.Location = New System.Drawing.Point(87, 441)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(100, 40)
        Me.btnPrint.TabIndex = 60
        Me.btnPrint.Text = "Print (F9)"
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.Black
        Me.btnClose.Location = New System.Drawing.Point(316, 441)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 62
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnInventoryCodeSearch
        '
        Me.btnInventoryCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnInventoryCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInventoryCodeSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInventoryCodeSearch.Location = New System.Drawing.Point(384, 183)
        Me.btnInventoryCodeSearch.Name = "btnInventoryCodeSearch"
        Me.btnInventoryCodeSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnInventoryCodeSearch.TabIndex = 476206
        Me.btnInventoryCodeSearch.Text = "..."
        Me.btnInventoryCodeSearch.UseVisualStyleBackColor = False
        '
        'frmBarcode
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(429, 499)
        Me.Controls.Add(Me.btnInventoryCodeSearch)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmBarcode"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Barcode"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmBarcode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        gsBarCodeFilePath = Application.StartupPath & "\" & gsBarCodeFile & ".txt"
        If UCase(sCompanyShortCode) = "ES" Then
            txtCompany.Text = "SRI PINANG"
        ElseIf UCase(sCompanyShortCode) = "ML" Then
            txtCompany.Text = "MILLION"
        ElseIf UCase(sCompanyShortCode) = "AS" Then
            txtCompany.Text = "APPOLLO"
        ElseIf UCase(sCompanyShortCode) = "GMT" Then
            txtCompany.Text = "GMT JEWELLERS PTE LTD"
        ElseIf UCase(sCompanyShortCode) = "HN" And (UCase(Objuser.CompanyName)) = "HANIFFA PTE LTD" Then
            txtCompany.Text = "HANIFFA TEXTILES PTE LTD"
        ElseIf UCase(sCompanyShortCode) = "HN" And (UCase(Objuser.CompanyName)) = "JEWEL PALACE PTE LTD" Then
            txtCompany.Text = "JEWEL PALACE PTE LTD"
        ElseIf UCase(sCompanyShortCode) = "SSJ" Then
            txtCompany.Text = "SS JEWELLERS"
        ElseIf UCase(sCompanyShortCode) = "RJ" Then
            txtCompany.Text = "ROYAL JEWELS"
        End If
        txtNoofLabels.Text = "1"

    End Sub

    Private Sub subFillPriceCodeArr()
        Try
            Dim sQry As String
            Dim Ds As DataSet
            Dim i As Long
            sQry = "Select PriceCode, PriceValue from tblPriceCode order by PriceValue"
            Ds = dm.getDataSetWithSqlAdapter(sQry, "PriceCode", "PriceCode")
            If Ds.Tables("PriceCode").Rows.Count > 0 Then
                For i = 0 To Ds.Tables("PriceCode").Rows.Count - 1
                    masPriceCode(Val(Ds.Tables("PriceCode").Rows(i)("PriceValue") & "")) = Trim(Ds.Tables("PriceCode").Rows(i)("PriceCode") & "")
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub



    Private Sub subGetInventory()
        Dim DsInv As DataSet
        Dim DsTrade As DataSet
        Dim DsAdditional As DataSet
        Dim sender As System.Object
        Dim e As System.EventArgs
        Dim objInv As New clsInventory
        Dim objTrade As New clsTradeIN
        Dim Ds As DataSet
        Dim objTray As New clsTray
        Try
            DsInv = objInv.GetInventoryDetail(txtInventoryCode.Text.Trim)
            DsTrade = objTrade.getTradeIn(txtInventoryCode.Text.Trim)
            DsAdditional = objInv.GetAdditionalInventoryDetail(txtInventoryCode.Text.Trim)
            If DsInv.Tables("tblInventory").Rows.Count > 0 Then
                If DsInv.Tables("tblInventory").Rows(0)("QtyOnHand") & "" >= 0 Then
                    txtDescription.Text = DsInv.Tables("tblInventory").Rows(0)("Description") & ""
                    txtWeight.Text = DsInv.Tables("tblInventory").Rows(0)("Weight") & ""
                    sStoneWeight = DsInv.Tables("tblInventory").Rows(0)("StoneWeight") & ""
                    txtGoldLoss.Text = DsInv.Tables("tblInventory").Rows(0)("GoldLoss") & ""
                    sClassCode = Trim(DsInv.Tables(0).Rows(0)("ClassCode") & "")
                    'load class details to get the rate and the salestype.
                    subGetClass()
                    If Trim(DsInv.Tables(0).Rows(0)("SMCType") & "") = "P" Then
                        'txtSMC.Text = Val(DsInv.Tables("tblInventory").Rows(0)("SMc")) / DsInv.Tables("tblInventory").Rows(0)("PCS") & ""
                        If Val(DsInv.Tables(0).Rows(0)("Pcs") & "") > 0 Then
                            txtSMC.Text = Format(Val(DsInv.Tables("tblInventory").Rows(0)("sMc") & "") / Val(DsInv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                            sPMC = Format(Val(DsInv.Tables("tblInventory").Rows(0)("PMC") & "") / Val(DsInv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                        Else
                            txtSMC.Text = DsInv.Tables("tblInventory").Rows(0)("SMc") & ""
                            sPMC = DsInv.Tables("tblInventory").Rows(0)("PMC") & ""
                        End If
                        'txtSMC.Text = Format(Val(DsInv.Tables("tblInventory").Rows(0)("sMc") & "") / Val(DsInv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                        'sPMC = Format(Val(DsInv.Tables("tblInventory").Rows(0)("PMC") & "") / Val(DsInv.Tables(0).Rows(0)("Pcs") & ""), "0.00")
                    Else
                        txtSMC.Text = DsInv.Tables("tblInventory").Rows(0)("SMc") & ""
                        sPMC = DsInv.Tables("tblInventory").Rows(0)("PMC") & ""
                    End If

                    txtClass.Text = DsInv.Tables("tblInventory").Rows(0)("ClassCode") & ""
                    txtBarcode.Text = DsInv.Tables("tblInventory").Rows(0)("InventoryCode") & ""
                    sSupplierCode = DsInv.Tables("tblInventory").Rows(0)("SupplierCode") & ""
                    sPurchaseDate = DsInv.Tables("tblInventory").Rows(0)("PurchaseDate") & ""
                    txtTrayID.Text = DsInv.Tables("tblInventory").Rows(0)("TrayId") & ""
                    sSizeCode = DsInv.Tables("tblInventory").Rows(0)("SizeCode") & ""
                    sSizeType = DsInv.Tables("tblInventory").Rows(0)("SizeType") & ""
                    sSalesPCS = DsInv.Tables("tblInventory").Rows(0)("SalesPCS") & ""
                    sSMCType = DsInv.Tables("tblInventory").Rows(0)("SMCType") & ""
                    ' sPMC = DsInv.Tables("tblInventory").Rows(0)("PMC") & ""
                    Ds = objTray.GetTraywithCode(txtTrayID.Text.Trim)
                    If Ds.Tables("Tray").Rows.Count <= 0 Then
                        sTrayGroup = ""
                    Else
                        sTrayGroup = Trim(Ds.Tables("Tray").Rows(0)("TrayGroup") & "")
                    End If

                    ''HAJ Get size and supplier code
                    If sCompanyShortCode = "ML" Then
                        sSize = DsInv.Tables("tblInventory").Rows(0)("Size") & ""
                        sPMC = DsInv.Tables("tblInventory").Rows(0)("PMC") & ""
                    End If
                    sDiamondDes = DsInv.Tables("tblInventory").Rows(0)("DiamondDescription") & ""
                    ''HAJ getcategorycode For Haniffa Barcode on 03 Dec 2007
                    If UCase(sCompanyShortCode) = "HN" Or UCase(sCompanyShortCode) = "MJ" Or UCase(sCompanyShortCode) = "SSJ" Or UCase(sCompanyShortCode) = "RJ" Or UCase(sCompanyShortCode) = "AJ" Or UCase(sCompanyShortCode) = "MD" Then
                        sCategorycode = Trim(DsInv.Tables("tblInventory").Rows(0)("CategoryCode")) & ""
                        txtCategoryCode.Text = Trim(DsInv.Tables("tblInventory").Rows(0)("CategoryCode")) & ""
                        sCatalogueCode = Trim(DsInv.Tables("tblInventory").Rows(0)("CatalogueCode")) & ""
                    End If
                    If DsAdditional.Tables("tblAddInventory").Rows.Count > 0 Then
                        sAddDescription1 = DsAdditional.Tables("tblAddInventory").Rows(0)("Description1") & ""
                        sAddDescription2 = DsAdditional.Tables("tblAddInventory").Rows(0)("Description2") & ""
                        sAddDescription3 = DsAdditional.Tables("tblAddInventory").Rows(0)("Description3") & ""
                        sAddDescription4 = DsAdditional.Tables("tblAddInventory").Rows(0)("Description4") & ""
                        sAddCost1 = DsAdditional.Tables("tblAddInventory").Rows(0)("Cost1") & ""
                        sAddCost2 = DsAdditional.Tables("tblAddInventory").Rows(0)("Cost2") & ""
                        sAddCost3 = DsAdditional.Tables("tblAddInventory").Rows(0)("Cost3") & ""
                        sAddCost4 = DsAdditional.Tables("tblAddInventory").Rows(0)("Cost4") & ""
                        sAddSellingPrice1 = DsAdditional.Tables("tblAddInventory").Rows(0)("SellingPrice1") & ""
                        sAddSellingPrice2 = DsAdditional.Tables("tblAddInventory").Rows(0)("SellingPrice2") & ""
                        sAddSellingPrice3 = DsAdditional.Tables("tblAddInventory").Rows(0)("SellingPrice3") & ""
                        sAddSellingPrice4 = DsAdditional.Tables("tblAddInventory").Rows(0)("SellingPrice4") & ""
                    End If
                Else
                    MessageBox.Show("No Stock for this InventoryCode", "GoldPOS")
                    btnNew_Click(sender, e)
                End If
            ElseIf DsTrade.Tables("TradeIn").Rows.Count > 0 Then
                txtDescription.Text = "Trade in Items"
                txtWeight.Text = DsTrade.Tables("TradeIn").Rows(0)("TotalWeight") & ""
                txtGoldLoss.Text = DsTrade.Tables("TradeIn").Rows(0)("TotalGoldLoss") & ""
                txtSMC.Text = DsTrade.Tables("TradeIn").Rows(0)("TotalMC") & ""
                txtBarcode.Text = DsTrade.Tables("TradeIn").Rows(0)("InvoiceNo") & ""
                sPurchaseDate = DsTrade.Tables("TradeIn").Rows(0)("CreateDate") & ""
                txtNoofLabels.Focus()
            Else
                MessageBox.Show("InventoryCode Not Found", "GoldPOS")
                btnNew_Click(sender, e)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subGetClass()
        Dim dsCls As New DataSet
        Try
            dsCls = objClass.GetClassDetail(sClassCode)
            If dsCls.Tables(0).Rows.Count > 0 Then
                msClsSalesType = Trim$(dsCls.Tables(0).Rows(0)("SalesType") & "")
            Else
                MessageBox.Show("Class Not defined for this item .... ")
                Exit Sub
            End If
            dsCls = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subPrintBarCode()
        Dim sInvCode, sMC, sTrayID, sClass, sDesc, sWeight, sCompName, sBarcode As String, sSupplier As String
        Dim sNoOfLabels As String
        subDeleteBarCodeFile(gsBarCodeFilePath)

        '''''
        '''''Please Do not Change the Order of printing 14.02.2007 Kumar
        subBarCodeDetailsToFile(gsBarCodeFilePath, "I8,1,001") ''''1
        subBarCodeDetailsToFile(gsBarCodeFilePath, "q200") ''''2
        subBarCodeDetailsToFile(gsBarCodeFilePath, "S5") ''''3
        subBarCodeDetailsToFile(gsBarCodeFilePath, "H20") ''''4
        subBarCodeDetailsToFile(gsBarCodeFilePath, "ZB") ''''5
        subBarCodeDetailsToFile(gsBarCodeFilePath, "R0,0") ''''6
        subBarCodeDetailsToFile(gsBarCodeFilePath, "N") ''''7

        sInvCode = "B68,179,0,1,1,3,38,N," & """" & txtInventoryCode.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sInvCode) ''''8
        If Len(sInvCode.Trim) = 0 Then
            MsgBox("Please Enter InventoryCode", MsgBoxStyle.Information)
            If txtInventoryCode.Enabled = True Then txtInventoryCode.Focus()
            Exit Sub
        End If

        sMC = "A17,93,0,2,1,1,N," & """MC:" & txtSMC.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sMC) ''''9
        If Len(sMC.Trim) = 0 Then
            MsgBox("Please Enter SMC", MsgBoxStyle.Information)
            If txtSMC.Enabled = True Then txtSMC.Focus()
            Exit Sub
        End If

        sSupplier = "A17,114,0,2,1,1,N," & """Supp:" & sSupplierCode.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sSupplier) ''''9
        'If Len(sMC.Trim) = 0 Then
        '    MsgBox("Please Enter SMC", MsgBoxStyle.Information)
        '    If txtSMC.Enabled = True Then txtSMC.Focus()
        '    Exit Sub
        'End If

        sTrayID = "A17,72,0,2,1,1,N," & """" & txtTrayID.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sTrayID) ''''10
        If Len(sTrayID.Trim) = 0 Then
            MsgBox("Please Enter Tray ID", MsgBoxStyle.Information)
            If txtTrayID.Enabled = True Then txtTrayID.Focus()
            Exit Sub
        End If

        sClass = "A17,53,0,2,1,1,N," & """" & txtClass.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sClass) ''''11
        If Len(sClass.Trim) = 0 Then
            MsgBox("Please Enter Class", MsgBoxStyle.Information)
            If txtClass.Enabled = True Then txtClass.Focus()
            Exit Sub
        End If

        sDesc = "A17,33,0,2,1,1,N," & """" & txtDescription.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sDesc) ''''12
        If Len(sDesc.Trim) = 0 Then
            MsgBox("Please Enter Description", MsgBoxStyle.Information)
            If txtDescription.Enabled = True Then txtDescription.Focus()
            Exit Sub
        End If

        sWeight = "A17,14,0,2,1,1,N," & """Gm:" & Format(Val(txtWeight.Text), "##0.00") & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sWeight) ''''13
        If Len(sWeight) = 0 Then
            MsgBox("Please Enter Weight", MsgBoxStyle.Information)
            If txtWeight.Enabled = True Then txtWeight.Focus()
            Exit Sub
        End If
        txtCompany.Text = "SRI PINANG"
        sCompName = "A25,149,0,3,1,1,N," & """" & txtCompany.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sCompName) ''''14
        If Len(sCompName.Trim) = 0 Then
            MsgBox("Please Enter Company Name", MsgBoxStyle.Information)
            If txtCompany.Enabled = True Then txtCompany.Focus()
            Exit Sub
        End If

        sBarcode = "A57,225,0,3,1,1,N," & """" & txtBarcode.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sBarcode) ''''15
        If Len(sBarcode.Trim) = 0 Then
            MsgBox("Please Enter BarCode", MsgBoxStyle.Information)
            If txtBarcode.Enabled = True Then txtBarcode.Focus()
            Exit Sub
        End If
        If txtNoofLabels.Text.Trim = "" Then
            txtNoofLabels.Text = "1"
        End If
        sNoOfLabels = "W1," & txtNoofLabels.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sNoOfLabels) ''''16

        Dim cPrinter As New clsBarcode
        sPrinterName = "POSTEK G-2108"
        cPrinter.SendFileToPrinter(sPrinterName, gsBarCodeFilePath)
    End Sub

    

    Private Sub frmBarcode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F9
                btnPrint_Click(sender, e)
            Case Keys.F6
                btnNew_Click(sender, e)
            Case Keys.Escape
                btnClose_Click(sender, e)
            Case Keys.Up
                SendKeys.Send("+{Tab}")
            Case Keys.Down
                SendKeys.Send("{Tab}")
            Case Keys.Enter
                SendKeys.Send("{Tab}")
        End Select
    End Sub

   
    '' HAJ Million Goldsmit Barcode Function 28-05-2007
    Private Sub subPrintBarCodeML()
        Dim sInvCode, sMC, sTrayID, sDesc, sWeight, sCompName, sBarcode As String, sSupplier As String
        Dim sNoOfLabels As String
        Dim sSizeDesc As String
        Dim dGrossWeight As Double
        subDeleteBarCodeFile(gsBarCodeFilePath)


        '''''
        '''''Please Do not Change the Order of printing 14.02.2007 Kumar
        subBarCodeDetailsToFile(gsBarCodeFilePath, "I8,1,001") ''''1
        subBarCodeDetailsToFile(gsBarCodeFilePath, "q200") ''''2
        subBarCodeDetailsToFile(gsBarCodeFilePath, "S5") ''''3
        subBarCodeDetailsToFile(gsBarCodeFilePath, "H20") ''''4
        subBarCodeDetailsToFile(gsBarCodeFilePath, "ZB") ''''5
        subBarCodeDetailsToFile(gsBarCodeFilePath, "R0,0") ''''6
        subBarCodeDetailsToFile(gsBarCodeFilePath, "N") ''''7

        '' Barcode 
        sInvCode = "B38,195,0,1,1,3,37,N," & """" & txtInventoryCode.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sInvCode) ''''8
        If Len(sInvCode.Trim) = 0 Then
            MsgBox("Please Enter InventoryCode", MsgBoxStyle.Information)
            If txtInventoryCode.Enabled = True Then txtInventoryCode.Focus()
            Exit Sub
        End If

        'MC
        sMC = "A7,53,0,2,1,1,N," & """MC:" & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sMC) ''''9
        If Len(sMC.Trim) = 0 Then
            MsgBox("Please Enter SMC", MsgBoxStyle.Information)
            If txtSMC.Enabled = True Then txtSMC.Focus()
            Exit Sub
        End If

        'MC
        sMC = "A81,53,0,2,1,1,N," & """S$" & txtSMC.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sMC) ''''9
        If Len(sMC.Trim) = 0 Then
            MsgBox("Please Enter SMC", MsgBoxStyle.Information)
            If txtSMC.Enabled = True Then txtSMC.Focus()
            Exit Sub
        End If



        ' Tray 
        sTrayID = "A7,121,0,2,1,1,N," & """TRAY:" & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sTrayID) ''''10
        If Len(sTrayID.Trim) = 0 Then
            MsgBox("Please Enter Tray ID", MsgBoxStyle.Information)
            If txtTrayID.Enabled = True Then txtTrayID.Focus()
            Exit Sub
        End If

        ' Tray ID
        sTrayID = "A103,121,0,2,1,1,N," & """" & txtTrayID.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sTrayID) ''''10
        If Len(sTrayID.Trim) = 0 Then
            MsgBox("Please Enter Tray ID", MsgBoxStyle.Information)
            If txtTrayID.Enabled = True Then txtTrayID.Focus()
            Exit Sub
        End If


        'Description1
        sDesc = "A7,70,0,2,1,1,N," & """" & Mid(txtDescription.Text.Trim, 1, 16) & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sDesc) ''''12
        If Len(sDesc.Trim) = 0 Then
            MsgBox("Please Enter Description", MsgBoxStyle.Information)
            If txtDescription.Enabled = True Then txtDescription.Focus()
            Exit Sub
        End If

        ''''Description2
        ''sDesc = "A7,75,0,2,1,1,N," & """" & Mid(txtDescription.Text.Trim, 16, 32) & """"
        ''subBarCodeDetailsToFile(gsBarCodeFilePath, sDesc) ''''12
        ''If Len(sDesc.Trim) = 0 Then
        ''    MsgBox("Please Enter Description", MsgBoxStyle.Information)
        ''    If txtDescription.Enabled = True Then txtDescription.Focus()
        ''    Exit Sub
        ''End If

        ''supplier
        sSupplier = "A7,87,0,2,1,1,N," & """SUP:" & sSupplierCode.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sSupplier) ''''9


        ''SIZE
        sSizeDesc = "A7,104,0,2,1,1,N," & """SIZE:" & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sSizeDesc) ''''10
        ''If Len(sTrayID.Trim) = 0 Then
        ''    MsgBox("Please Enter Tray ID", MsgBoxStyle.Information)
        ''    If txtTrayID.Enabled = True Then txtTrayID.Focus()
        ''    Exit Sub
        ''End If

        ''SIZE
        sSize = "A77,104,0,2,1,1,N," & """" & sSize & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sSize) ''''12
        ''If Len(sDesc.Trim) = 0 Then
        ''    MsgBox("Please Enter Description", MsgBoxStyle.Information)
        ''    If txtDescription.Enabled = True Then txtDescription.Focus()
        ''    Exit Sub
        ''End If

        ''GR WT
        sWeight = "A7,20,0,2,1,1,N," & """GR WT:" & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sWeight) ''''13
        If Len(sWeight) = 0 Then
            MsgBox("Please Enter Weight", MsgBoxStyle.Information)
            If txtWeight.Enabled = True Then txtWeight.Focus()
            Exit Sub
        End If

        ''Weight
        '' 19 sep 2007
        dGrossWeight = Val(txtWeight.Text) + Val(txtGoldLoss.Text)
        sWeight = "A90,20,0,2,1,1,N," & """" & Format(dGrossWeight, "##0.00") & "gm" & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sWeight) ''''13
        If Len(sWeight) = 0 Then
            MsgBox("Please Enter Weight", MsgBoxStyle.Information)
            If txtWeight.Enabled = True Then txtWeight.Focus()
            Exit Sub
        End If

        ''GR Loss
        sWeight = "A7,36,0,2,1,1,N," & """GL LOSS:" & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sWeight) ''''13
        If Len(sWeight) = 0 Then
            MsgBox("Please Enter Gold Loss", MsgBoxStyle.Information)
            If txtGoldLoss.Enabled = True Then txtGoldLoss.Focus()
            Exit Sub
        End If

        ''Gold Loss
        sWeight = "A100,36,0,2,1,1,N," & """" & Format(Val(txtGoldLoss.Text), "##0.00") & "gm" & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sWeight) ''''13
        If Len(sWeight) = 0 Then
            MsgBox("Please Enter Gold Loss", MsgBoxStyle.Information)
            If txtGoldLoss.Enabled = True Then txtGoldLoss.Focus()
            Exit Sub
        End If

        ''Company
        txtCompany.Text = "MILLION"
        sCompName = "A44,158,0,4,1,1,N," & """" & txtCompany.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sCompName) ''''14
        If Len(sCompName.Trim) = 0 Then
            MsgBox("Please Enter Company Name", MsgBoxStyle.Information)
            If txtCompany.Enabled = True Then txtCompany.Focus()
            Exit Sub
        End If

        ''Text Under Barcode
        sBarcode = "A20,237,0,4,1,1,N," & """" & txtBarcode.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sBarcode) ''''15
        If Len(sBarcode.Trim) = 0 Then
            MsgBox("Please Enter BarCode", MsgBoxStyle.Information)
            If txtBarcode.Enabled = True Then txtBarcode.Focus()
            Exit Sub
        End If
        If txtNoofLabels.Text.Trim = "" Then
            txtNoofLabels.Text = "1"
        End If
        sNoOfLabels = "W1," & txtNoofLabels.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sNoOfLabels) ''''16

        Dim cPrinter As New clsBarcode
        sPrinterName = "POSTEK G-2108"
        cPrinter.SendFileToPrinter(sPrinterName, gsBarCodeFilePath)
    End Sub
    ' HAJ Million Goldsmit JewelBarcode Function 16-Aug-2007
    Private Sub subPrintJewelBarCodeML()
        Dim sInvCode, sTrayID, sClass, sDesc, sCompName, sBarcode As String, sSupplier As String, sPrice As String
        Dim sCost As String

        Dim sNoOfLabels As String
        Dim sSizeDesc As String
        subDeleteBarCodeFile(gsBarCodeFilePath)


        '
        'Please Do not Change the Order of printing 14.02.2007 Kumar
        subBarCodeDetailsToFile(gsBarCodeFilePath, "I8,1,001") ''''1
        subBarCodeDetailsToFile(gsBarCodeFilePath, "q200") ''''2
        subBarCodeDetailsToFile(gsBarCodeFilePath, "S5") ''''3
        subBarCodeDetailsToFile(gsBarCodeFilePath, "H10") ''''4
        subBarCodeDetailsToFile(gsBarCodeFilePath, "ZB") ''''5
        subBarCodeDetailsToFile(gsBarCodeFilePath, "R0,0") ''''6
        subBarCodeDetailsToFile(gsBarCodeFilePath, "N") ''''7

        '' Barcode 
        sInvCode = "B38,195,0,1,1,3,37,N," & """" & txtInventoryCode.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sInvCode) ''''8
        If Len(sInvCode.Trim) = 0 Then
            MsgBox("Please Enter InventoryCode", MsgBoxStyle.Information)
            If txtInventoryCode.Enabled = True Then txtInventoryCode.Focus()
            Exit Sub
        End If


        'Vendor
        sSupplier = "A7,20,0,2,1,1,N," & """" & sSupplierCode.Trim & ":" & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sSupplier) ''''10


        'Class Name (Type of Gold)
        sClass = "A90,20,0,2,1,1,N," & """" & Mid(sClassName.Trim, 1, 10) & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sClass) ''''11

        ''SIZE
        sSizeDesc = "A7,36,0,2,1,1,N," & """" & sSize & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sSizeDesc) ''''12

        ''TRAY
        sTrayID = "A100,36,0,2,1,1,N," & """" & "TRAY:" & Trim(txtTrayID.Text) & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sTrayID) ''''10

        ''Normal Description
        sDesc = "A7,53,0,2,1,1,N," & """" & Mid(txtDescription.Text.Trim, 1, 16) & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sDesc) ''''12

        ''Diamond Description1
        sDesc = "A7,70,0,2,1,1,N," & """" & Mid(sDiamondDes.Trim, 1, 16) & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sDesc) ''''12

        ''Diamond Description2
        sDesc = "A7,87,0,2,1,1,N," & """" & Mid(sDiamondDes.Trim, 16, 32) & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sDesc) ''''12

        ''Cost (PMC)
        sCost = FncGetMcConversion(Trim(sPMC))
        sCost = "A7,104,0,2,1,1,N," & """" & sCost & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sCost) ''''13

        ''Price (SMC)
        sPrice = "A7,121,0,3,1,1,N," & """S$" & Format(Val(txtSMC.Text), "##0.00") & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sPrice) ''''13

        ''Company
        txtCompany.Text = "MILLION"
        sCompName = "A44,158,0,4,1,1,N," & """" & txtCompany.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sCompName) ''''14
        If Len(sCompName.Trim) = 0 Then
            MsgBox("Please Enter Company Name", MsgBoxStyle.Information)
            If txtCompany.Enabled = True Then txtCompany.Focus()
            Exit Sub
        End If

        ''Text Under Barcode
        sBarcode = "A20,237,0,4,1,1,N," & """" & txtBarcode.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sBarcode) ''''15
        If Len(sBarcode.Trim) = 0 Then
            MsgBox("Please Enter BarCode", MsgBoxStyle.Information)
            If txtBarcode.Enabled = True Then txtBarcode.Focus()
            Exit Sub
        End If
        If txtNoofLabels.Text.Trim = "" Then
            txtNoofLabels.Text = "1"
        End If
        sNoOfLabels = "W1," & txtNoofLabels.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sNoOfLabels) ''''16

        Dim cPrinter As New clsBarcode
        sPrinterName = "POSTEK G-2108"
        cPrinter.SendFileToPrinter(sPrinterName, gsBarCodeFilePath)
    End Sub
    Private Sub subPrintBarCodeGMT()
        Dim sInvCode, sMC, sTrayID, sClass, sDesc, sWeight, sCompName, sBarcode As String, sSupplier As String
        Dim sQuote As String
        Dim sNoOfLabels As String
        subDeleteBarCodeFile(gsBarCodeFilePath)

        '''''
        '''''Please Do not Change the Order of printing 14.02.2007 Kumar
        subBarCodeDetailsToFile(gsBarCodeFilePath, "I8,1,001") ''''1
        subBarCodeDetailsToFile(gsBarCodeFilePath, "q200") ''''2
        subBarCodeDetailsToFile(gsBarCodeFilePath, "S5") ''''3
        subBarCodeDetailsToFile(gsBarCodeFilePath, "H20") ''''4
        subBarCodeDetailsToFile(gsBarCodeFilePath, "ZB") ''''5
        subBarCodeDetailsToFile(gsBarCodeFilePath, "R0,0") ''''6
        subBarCodeDetailsToFile(gsBarCodeFilePath, "N") ''''7

        'Barcode
        sInvCode = "B32,151,0,1,2,6,86,N," & """" & txtInventoryCode.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sInvCode) ''''8
        If Len(sInvCode.Trim) = 0 Then
            MsgBox("Please Enter InventoryCode", MsgBoxStyle.Information)
            If txtInventoryCode.Enabled = True Then txtInventoryCode.Focus()
            Exit Sub
        End If

        ''SMC
        sMC = "A13,94,0,3,1,1,N," & """C:$" & txtSMC.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sMC) ''''9
        If Len(sMC.Trim) = 0 Then
            MsgBox("Please Enter SMC", MsgBoxStyle.Information)
            If txtSMC.Enabled = True Then txtSMC.Focus()
            Exit Sub
        End If

        'Class Code
        sClass = "A13,55,0,3,1,1,N," & """" & txtClass.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sClass) ''''11
        If Len(sClass.Trim) = 0 Then
            MsgBox("Please Enter Class", MsgBoxStyle.Information)
            If txtClass.Enabled = True Then txtClass.Focus()
            Exit Sub
        End If

        sDesc = "A13,78,0,1,1,1,N," & """" & Mid(txtDescription.Text.Trim, 1, 18) & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sDesc) ''''12
        If Len(sDesc.Trim) = 0 Then
            MsgBox("Please Enter Description", MsgBoxStyle.Information)
            If txtDescription.Enabled = True Then txtDescription.Focus()
            Exit Sub
        End If
        'Weight

        sWeight = "A13,116,0,4,1,1,N," & """" & Format(Val(txtWeight.Text), "##0.00") & "gm" & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sWeight) ''''13
        If Len(sWeight) = 0 Then
            MsgBox("Please Enter Weight", MsgBoxStyle.Information)
            If txtWeight.Enabled = True Then txtWeight.Focus()
            Exit Sub
        End If

        'InventoryCode
        sBarcode = "A52,245,0,4,1,1,N," & """" & txtBarcode.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sBarcode) ''''15
        If Len(sBarcode.Trim) = 0 Then
            MsgBox("Please Enter BarCode", MsgBoxStyle.Information)
            If txtBarcode.Enabled = True Then txtBarcode.Focus()
            Exit Sub
        End If
        If txtNoofLabels.Text.Trim = "" Then
            txtNoofLabels.Text = "1"
        End If
        sNoOfLabels = "W1," & txtNoofLabels.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sNoOfLabels) ''''16

        Dim cPrinter As New clsBarcode
        sPrinterName = "POSTEK G-2108"
        cPrinter.SendFileToPrinter(sPrinterName, gsBarCodeFilePath)
    End Sub
    Private Sub subPrintBarCodeAB()
        Dim sInvCode, sMC, sTrayID, sClass, sDesc, sWeight, sCompName, sBarcode As String, sSupplier As String
        Dim sQuote As String
        Dim sNoOfLabels As String
        subDeleteBarCodeFile(gsBarCodeFilePath)

        subBarCodeDetailsToFile(gsBarCodeFilePath, "n") ''''1
        subBarCodeDetailsToFile(gsBarCodeFilePath, "M0500") ''''2
        subBarCodeDetailsToFile(gsBarCodeFilePath, "O0220") ''''3
        subBarCodeDetailsToFile(gsBarCodeFilePath, "V0") ''''4
        subBarCodeDetailsToFile(gsBarCodeFilePath, "f220") ''''5
        subBarCodeDetailsToFile(gsBarCodeFilePath, "L") ''''6
        subBarCodeDetailsToFile(gsBarCodeFilePath, "N") ''''7
        subBarCodeDetailsToFile(gsBarCodeFilePath, "D11") ''''6
        subBarCodeDetailsToFile(gsBarCodeFilePath, "A2") ''''7

        subBarCodeDetailsToFile(gsBarCodeFilePath, "3e2101801190065BE&D123456") ''''6
        subBarCodeDetailsToFile(gsBarCodeFilePath, "3911A0601310059E123456") ''''7
        subBarCodeDetailsToFile(gsBarCodeFilePath, "3911A0601020077LB65") ''''6
        subBarCodeDetailsToFile(gsBarCodeFilePath, "3911A0601020019RS") ''''7

        subBarCodeDetailsToFile(gsBarCodeFilePath, "3911A0600740088NV-260") ''''6
        subBarCodeDetailsToFile(gsBarCodeFilePath, "3911A080040008810.80g") ''''7
        subBarCodeDetailsToFile(gsBarCodeFilePath, "3911A080052008812.70g/1.900") ''''6
        subBarCodeDetailsToFile(gsBarCodeFilePath, "3911A0600650088LESS 1.90") ''''7
        subBarCodeDetailsToFile(gsBarCodeFilePath, "3911A06003900395.7 CM") ''''6
        subBarCodeDetailsToFile(gsBarCodeFilePath, "3911A0600920088D-0.13C-355") ''''7

        subBarCodeDetailsToFile(gsBarCodeFilePath, "Q0001") ''''6
        subBarCodeDetailsToFile(gsBarCodeFilePath, "E") ''''7
        MsgBox("Please Enter SMC", MsgBoxStyle.Information)

        subBarCodeDetailsToFile(gsBarCodeFilePath, sNoOfLabels) ''''16

        Dim cPrinter As New clsBarcode
        sPrinterName = "Argox OS-214 plus series PPLA"
        cPrinter.SendFileToPrinter(sPrinterName, gsBarCodeFilePath)
    End Sub
    Private Sub subPrintBarCodeABNEW()
        Dim sInvCode, sMC, sSMC, sTrayID, sClass, sDesc, sWeight, sCompName, sBarcode, sSizeCode As String, sSupplier As String
        Dim sQuote As String
        Dim sNoOfLabels As String
        Dim dsMCC As New DataSet
        Dim objinv As New clsInventory
        Dim sChar As String
        subDeleteBarCodeFile(gsBarCodeFilePath)

        subBarCodeDetailsToFile(gsBarCodeFilePath, "n") ''''1
        subBarCodeDetailsToFile(gsBarCodeFilePath, "M0500") ''''2
        subBarCodeDetailsToFile(gsBarCodeFilePath, "O0220") ''''3
        subBarCodeDetailsToFile(gsBarCodeFilePath, "V0") ''''4
        subBarCodeDetailsToFile(gsBarCodeFilePath, "f220") ''''5
        subBarCodeDetailsToFile(gsBarCodeFilePath, "L") ''''6
        subBarCodeDetailsToFile(gsBarCodeFilePath, "N") ''''7
        subBarCodeDetailsToFile(gsBarCodeFilePath, "D11") ''''6
        subBarCodeDetailsToFile(gsBarCodeFilePath, "A2") ''''7

        'Barcode
        sInvCode = "3e2101801190065" & """" & txtInventoryCode.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sInvCode) ''''8
        If Len(sInvCode.Trim) = 0 Then
            MsgBox("Please Enter InventoryCode", MsgBoxStyle.Information)
            If txtInventoryCode.Enabled = True Then txtInventoryCode.Focus()
            Exit Sub
        End If
        ''subBarCodeDetailsToFile(gsBarCodeFilePath, "3e2101801190065BE&D123456") ''''6

        'InventoryCode
        sBarcode = "3911A0601310059" & """" & txtBarcode.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sBarcode) ''''15
        If Len(sBarcode.Trim) = 0 Then
            MsgBox("Please Enter BarCode", MsgBoxStyle.Information)
            If txtBarcode.Enabled = True Then txtBarcode.Focus()
            Exit Sub
        End If

        ''subBarCodeDetailsToFile(gsBarCodeFilePath, "3911A0601310059E123456") ''''7

        ''SMC
        sMC = "3911A0601020077" & """LB" & txtSMC.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sMC) ''''9
        If Len(sMC.Trim) = 0 Then
            MsgBox("Please Enter SMC", MsgBoxStyle.Information)
            If txtSMC.Enabled = True Then txtSMC.Focus()
            Exit Sub
        End If

        ''' subBarCodeDetailsToFile(gsBarCodeFilePath, "3911A0601020077LB65") ''''6


        ' ''MC
        ''dsMCC = objinv.GetMcConvertion(sPMC)
        ''If dsMCC.Tables(0).Rows.Count > 0 Then
        ''    sChar = Mid(Trim(dsMCC.Tables(0).Rows(0)("Mc") & ""), 1, Len(Trim(dsMCC.Tables(0).Rows(0)("Mc") & "")) - 3) 'Remove the decimal part
        ''    sSMC = "3911A0601020019" & """" & sChar & """"
        ''    subBarCodeDetailsToFile(gsBarCodeFilePath, sSMC)
        ''End If
        subBarCodeDetailsToFile(gsBarCodeFilePath, "3911A0601020019RS") ''''7



        ''Description
        sDesc = "3911A0600740088" & """" & Mid(txtDescription.Text.Trim, 1, 15) & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sDesc) ''''12
        If Len(sDesc.Trim) = 0 Then
            MsgBox("Please Enter Description", MsgBoxStyle.Information)
            If txtDescription.Enabled = True Then txtDescription.Focus()
            Exit Sub
        End If

        ' subBarCodeDetailsToFile(gsBarCodeFilePath, "3911A0600740088NV-260") ''''6

        'Weight
        sWeight = "3911A0800400088" & """" & Format(Val(txtWeight.Text), "##0.00") & "g" & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sWeight) ''''13
        If Len(sWeight) = 0 Then
            MsgBox("Please Enter Weight", MsgBoxStyle.Information)
            If txtWeight.Enabled = True Then txtWeight.Focus()
            Exit Sub
        End If
        'subBarCodeDetailsToFile(gsBarCodeFilePath, "3911A080040008810.80g") ''''7


        subBarCodeDetailsToFile(gsBarCodeFilePath, "3911A080052008812.70g/1.900") ''''6
        subBarCodeDetailsToFile(gsBarCodeFilePath, "3911A0600650088LESS 1.90") ''''7

        'SizeCode
        sSizeCode = "3911A0601020077" & """" & Trim(sSizeCode) & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sSizeCode) ''''13
        ' subBarCodeDetailsToFile(gsBarCodeFilePath, "3911A06003900395.7 CM") ''''6

        subBarCodeDetailsToFile(gsBarCodeFilePath, "3911A0600920088D-0.13C-355") ''''7

        subBarCodeDetailsToFile(gsBarCodeFilePath, "Q0001") ''''6
        subBarCodeDetailsToFile(gsBarCodeFilePath, "E") ''''7
        MsgBox("Please Enter SMC", MsgBoxStyle.Information)

        subBarCodeDetailsToFile(gsBarCodeFilePath, sNoOfLabels) ''''16

        Dim cPrinter As New clsBarcode
        sPrinterName = "Argox OS-214 plus series PPLA"
        cPrinter.SendFileToPrinter(sPrinterName, gsBarCodeFilePath)
    End Sub
    Private Function FncGetMcConversion(ByVal sCost As String) As String
        Dim sMC As String
        Dim sSql As String
        sSql = "Exec sp_MCConversion " & Val(sCost) & "," & fncFormatSQLText("")
        sMC = dm.ValReturnProjExecNonQry(sSql)
        If Len(sMC) > 0 Then
            sMC = Mid(Trim(sMC & ""), 1, Len(Trim(sMC & "")) - 3) 'Remove the decimal part
        End If
        FncGetMcConversion = sMC
    End Function

    ''Haniffa Barcode on 03 Nov 2007 
    Private Sub subPrintBarCodeHaniffa()
        Dim sInvCode, sMC, sTrayID, sClass, sDesc, sWeight, sCompName, sBarcode As String, sSupplier As String
        Dim sQuote As String
        Dim sNoOfLabels As String
        Dim sender As System.Object
        Dim e As System.EventArgs
        subDeleteBarCodeFile(gsBarCodeFilePath)

        '''''
        '''''Please Do not Change the Order of printing 14.02.2007 Kumar
        subBarCodeDetailsToFile(gsBarCodeFilePath, "I8,1,001") ''''1
        subBarCodeDetailsToFile(gsBarCodeFilePath, "q200") ''''2
        subBarCodeDetailsToFile(gsBarCodeFilePath, "S5") ''''3
        subBarCodeDetailsToFile(gsBarCodeFilePath, "H20") ''''4
        subBarCodeDetailsToFile(gsBarCodeFilePath, "ZB") ''''5
        subBarCodeDetailsToFile(gsBarCodeFilePath, "R0,0") ''''6
        subBarCodeDetailsToFile(gsBarCodeFilePath, "N") ''''7

        'Barcode
        sInvCode = "B30,156,0,1,2,6,72,N," & """" & txtInventoryCode.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sInvCode) ''''8
        If Len(sInvCode.Trim) = 0 Then
            MsgBox("Please Enter InventoryCode", MsgBoxStyle.Information)
            If txtInventoryCode.Enabled = True Then txtInventoryCode.Focus()
            Exit Sub
        End If

        ''SMC
        sMC = "A11,70,0,3,1,1,N," & """MC:$" & txtSMC.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sMC) ''''9
        If Len(sMC.Trim) = 0 Then
            MsgBox("Please Enter SMC", MsgBoxStyle.Information)
            If txtSMC.Enabled = True Then txtSMC.Focus()
            Exit Sub
        End If

        ''CategoryCode - ClassCode
        sClass = "A11,129,0,2,1,1,N," & """" & sCategorycode & "-" & txtClass.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sClass) ''''11
        If Len(sClass.Trim) = 0 Then
            MsgBox("Please Enter Class", MsgBoxStyle.Information)
            If txtClass.Enabled = True Then txtClass.Focus()
            Exit Sub
        End If

        ''Description
        sDesc = "A11,42,0,3,1,1,N," & """" & Mid(txtDescription.Text.Trim, 1, 15) & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sDesc) ''''12
        If Len(sDesc.Trim) = 0 Then
            MsgBox("Please Enter Description", MsgBoxStyle.Information)
            If txtDescription.Enabled = True Then txtDescription.Focus()
            Exit Sub
        End If
        'Weight
        sWeight = "A11,94,0,4,1,1,N," & """" & Format(Val(txtWeight.Text), "##0.00") & "gm" & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sWeight) ''''13
        If Len(sWeight) = 0 Then
            MsgBox("Please Enter Weight", MsgBoxStyle.Information)
            If txtWeight.Enabled = True Then txtWeight.Focus()
            Exit Sub
        End If

        'InventoryCode
        sBarcode = "A50,236,0,4,1,1,N," & """" & txtBarcode.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sBarcode) ''''15
        If Len(sBarcode.Trim) = 0 Then
            MsgBox("Please Enter BarCode", MsgBoxStyle.Information)
            If txtBarcode.Enabled = True Then txtBarcode.Focus()
            Exit Sub
        End If
        If txtNoofLabels.Text.Trim = "" Then
            txtNoofLabels.Text = "1"
        End If
        sNoOfLabels = "W1," & txtNoofLabels.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sNoOfLabels) ''''16

        Dim cPrinter As New clsBarcode
        sPrinterName = "POSTEK G-2108"
        cPrinter.SendFileToPrinter(sPrinterName, gsBarCodeFilePath)
        btnNew_Click(sender, e)

    End Sub
    Private Sub subPrintBarCodeMeenaZebra()
        Dim sInventoryCode, sDescription, sGoldLoss, sWeight, sSMC, sSupplierCodeWithPurchaseDate, sClass, sCompany As String

        subDeleteBarCodeFile(gsBarCodeFilePath)



        subBarCodeDetailsToFile(gsBarCodeFilePath, "CT~~CD,~CC^~CT~")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "^XA")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "~TA000")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "~JSN")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "^LT0")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "^MNW")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "^MTT")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "^PON")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "^PMN")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "^LH0,0")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "^JMA")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "^PR4,4")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "~SD18")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "^JUS")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "^LRN")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "^CI27")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "^PA0,1,1,0")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "^XZ")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "^XA")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "^MMT")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "^PW719")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "^LL160")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "^LS0")

        'Company Name
        sCompany = "NEW GOLDEN TRADERS"
        '''sCompany = "TEXT 470,109," & Chr(34) & "ROMAN.TTF" & Chr(34) & ",180,5,9," & Chr(34) & sCompany & Chr(34)
        sCompany = "^FT27,51^A0N,17,18^FH\^CI28^FD" & sCompany & "^FS^CI27"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sCompany)


        'Description
        sDescription = Trim(sSupplierCode) & " " & Trim(sCatalogueCode) & "   " & Trim(sCategorycode) 'Mid(txtDescription.Text.Trim, 1, 13)
        ' sDescription = "TEXT 470,89," & Chr(34) & "ROMAN.TTF" & Chr(34) & ",180,7,5," & Chr(34) & sDescription & Chr(34)
        sDescription = "^FT27,72^A0N,17,18^FH\^CI28^FD" & sDescription & "^FS^CI27"""
        subBarCodeDetailsToFile(gsBarCodeFilePath, sDescription)



        'MC
        If Trim(gthisLocation) = "S2" Then
            sSMC = "MC:$ -"
        Else
            sSMC = IIf(txtSMC.Text.Trim <> "", "MC:$" & txtSMC.Text.Trim, "")
        End If

        '''sSMC = "TEXT 165,197," & Chr(34) & "0" & Chr(34) & ",180,8,7," & Chr(34) & sSMC & Chr(34)
        ''sSMC = "TEXT 470,71," & Chr(34) & "0" & Chr(34) & ",180,7,5," & Chr(34) & sSMC & Chr(34)
        'sSMC = "TEXT 421,70," & Chr(34) & "0" & Chr(34) & ",180,11,7," & Chr(34) & sSMC & Chr(34)
        sSMC = "^FT28,102^A0N,23,23^FH\^CI28^FD" & sSMC & "^FS^CI27"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sSMC)

        'Weight
        sWeight = IIf(txtWeight.Text.Trim <> "", txtWeight.Text.Trim & "gm", "")
        If sStoneWeight <> "0.00" Then
            sWeight = sWeight & " " & IIf(sStoneWeight <> "0.00", "NW" & (txtWeight.Text.Trim) - sStoneWeight, "")
        End If
        'sWeight = "TEXT 425,98," & Chr(34) & "0" & Chr(34) & ",180,14,9," & Chr(34) & sWeight & Chr(34)
        sWeight = "T^FT30,131^A0N,23,23^FH\^CI28^FD" & sWeight & "^FS^CI27"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sWeight)

        'Barcode
        '''sInventoryCode = "BARCODE 133,103," & Chr(34) & "128M" & Chr(34) & ",38,0,180,2,4," & Chr(34) & "!105" & txtInventoryCode.Text.Trim & Chr(34)
        ''' sInventoryCode = "BARCODE 165,103," & Chr(34) & "128M" & Chr(34) & ",45,0,180,1,3," & Chr(34) & "!105" & txtInventoryCode.Text.Trim & Chr(34)
        sInventoryCode = "^BY1,3,58^FT259,103^BCN,,N,N"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sInventoryCode)


        'Inventory
        '''sInventoryCode = "TEXT 110,49," & Chr(34) & "3" & Chr(34) & ",180,1,1," & Chr(34) & txtInventoryCode.Text.Trim & Chr(34)
        'sInventoryCode = "TEXT 238,76," & Chr(34) & "3" & Chr(34) & ",180,12,12," & Chr(34) & txtInventoryCode.Text.Trim & Chr(34)
        sInventoryCode = "^FH\^FD>;" & txtInventoryCode.Text.Trim & "^FS"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sInventoryCode)

        'Inventory
        '''sInventoryCode = "TEXT 110,49," & Chr(34) & "3" & Chr(34) & ",180,1,1," & Chr(34) & txtInventoryCode.Text.Trim & Chr(34)
        'sInventoryCode = "TEXT 238,76," & Chr(34) & "3" & Chr(34) & ",180,12,12," & Chr(34) & txtInventoryCode.Text.Trim & Chr(34)
        sInventoryCode = "^FT248,131^A0N,23,23^FH\^CI28^FD" & txtInventoryCode.Text.Trim & "^FS^CI27"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sInventoryCode)

        'No of lables
        'subBarCodeDetailsToFile(gsBarCodeFilePath, "PRINT 1," & IIf(txtNoofLabels.Text.Trim = "", 1, txtNoofLabels.Text.Trim))
        subBarCodeDetailsToFile(gsBarCodeFilePath, "^PQ1,0," & IIf(txtNoofLabels.Text.Trim = "", 1, txtNoofLabels.Text.Trim) & ",Y")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "^XZ")


        Dim cPrinter As New clsBarcode
        sPrinterName = "ZDesigner ZD220-203dpi ZPL"
        cPrinter.SendFileToPrinter(sPrinterName, gsBarCodeFilePath)

        '^FT27,51^A0N,17,18^FH\^CI28^FDNEW GOLDEN TRADERS^FS^CI27
        '^FT27,72^A0N,17,18^FH\^CI28^FDNSH SG NK 22K NK^FS^CI27
        '^FT28,102^A0N,23,23^FH\^CI28^FDMC:$108.00^FS^CI27
        '^FT30,131^A0N,23,23^FH\^CI28^FD7.22gm^FS^CI27
        '^BY1,3,58^FT259,103^BCN,,N,N
        '^FH\^FD>;1234567890^FS
        '^FT248,131^A0N,23,23^FH\^CI28^FD1234567890^FS^CI27
        '^PQ1,0,1,Y
        '^XZ


    End Sub
    ''Meena Barcode on 09 03 2011
    Private Sub subPrintBarCodeMeena()
        Dim sInventoryCode, sDescription, sGoldLoss, sWeight, sSMC, sSupplierCodeWithPurchaseDate, sClass, sCompany As String

        subDeleteBarCodeFile(gsBarCodeFilePath)
        'subBarCodeDetailsToFile(gsBarCodeFilePath, "SIZE 22.5 mm, 35 mm")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SIZE 87.5 mm, 15 mm")
        'subBarCodeDetailsToFile(gsBarCodeFilePath, "SIZE 90 mm, 15 mm")
        'subBarCodeDetailsToFile(gsBarCodeFilePath, "GAP 5 mm, 0 mm")
        'subBarCodeDetailsToFile(gsBarCodeFilePath, "SPEED 4")
        'subBarCodeDetailsToFile(gsBarCodeFilePath, "DENSITY 7")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "DIRECTION 0,0")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "REFERENCE 0,0")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "OFFSET 0 mm")
        'subBarCodeDetailsToFile(gsBarCodeFilePath, "SHIFT 0")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SET PEEL OFF")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SET CUTTER OFF")
        'subBarCodeDetailsToFile(gsBarCodeFilePath, "SET PARTIAL_CUTTER OFF")
        'subBarCodeDetailsToFile(gsBarCodeFilePath, "SET TEAR ON")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "CLS")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "CODEPAGE 1252")
        'subBarCodeDetailsToFile(gsBarCodeFilePath, "CODEPAGE 850")

        'Company Name
        sCompany = "NEW GOLDEN TRADERS"
        '''sCompany = "TEXT 470,109," & Chr(34) & "ROMAN.TTF" & Chr(34) & ",180,5,9," & Chr(34) & sCompany & Chr(34)
        sCompany = "TEXT 421,109," & Chr(34) & "0" & Chr(34) & ",180,5,5," & Chr(34) & sCompany & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sCompany)

        'Company Name
        ' sCompany = "TRADERS"
        ' sCompany = "TEXT 165,225," & Chr(34) & "ROMAN.TTF" & Chr(34) & ",180,5,9," & Chr(34) & sCompany & Chr(34)
        ' subBarCodeDetailsToFile(gsBarCodeFilePath, sCompany)

        'Description
        sDescription = Trim(sSupplierCode) & " " & Trim(sCatalogueCode) & "   " & Trim(sCategorycode) 'Mid(txtDescription.Text.Trim, 1, 13)
        ' sDescription = "TEXT 470,89," & Chr(34) & "ROMAN.TTF" & Chr(34) & ",180,7,5," & Chr(34) & sDescription & Chr(34)
        sDescription = "TEXT 421,89," & Chr(34) & "0" & Chr(34) & ",180,7,5," & Chr(34) & sDescription & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sDescription)


        'MC
        sSMC = IIf(txtSMC.Text.Trim <> "", "MC:$" & txtSMC.Text.Trim, "")
        '''sSMC = "TEXT 165,197," & Chr(34) & "0" & Chr(34) & ",180,8,7," & Chr(34) & sSMC & Chr(34)
        ''sSMC = "TEXT 470,71," & Chr(34) & "0" & Chr(34) & ",180,7,5," & Chr(34) & sSMC & Chr(34)
        'sSMC = "TEXT 421,70," & Chr(34) & "0" & Chr(34) & ",180,11,7," & Chr(34) & sSMC & Chr(34)
        sSMC = "TEXT 421,70," & Chr(34) & "0" & Chr(34) & ",180,9,7," & Chr(34) & sSMC & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sSMC)

        'Weight
        sWeight = IIf(txtWeight.Text.Trim <> "", txtWeight.Text.Trim & "gm", "")
        If sStoneWeight <> "0.00" Then
            sWeight = sWeight & " " & IIf(sStoneWeight <> "0.00", "NW" & (txtWeight.Text.Trim) - sStoneWeight, "")
        End If
        'sWeight = "TEXT 425,98," & Chr(34) & "0" & Chr(34) & ",180,14,9," & Chr(34) & sWeight & Chr(34)
        sWeight = "TEXT 421,47," & Chr(34) & "0" & Chr(34) & ",180,9,7," & Chr(34) & sWeight & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sWeight)

        'StoneWeight
        'sStoneWeight = sStoneWeight & "gm" ' IIf(txtWeight.Text.Trim <> "", txtWeight.Text.Trim & "gm", "")
        ''sWeight = "TEXT 425,98," & Chr(34) & "0" & Chr(34) & ",180,14,9," & Chr(34) & sWeight & Chr(34)
        'sStoneWeight = "TEXT 421,47," & Chr(34) & "0" & Chr(34) & ",180,11,7," & Chr(34) & sWeight & Chr(34)
        'subBarCodeDetailsToFile(gsBarCodeFilePath, sStoneWeight)


        'Category
        'sClass = "TEXT 208,170," & Chr(34) & "0" & Chr(34) & ",180,11,8," & Chr(34) & sCategorycode & Chr(34)
        'subBarCodeDetailsToFile(gsBarCodeFilePath, sClass)

        'Barcode
        '''sInventoryCode = "BARCODE 133,103," & Chr(34) & "128M" & Chr(34) & ",38,0,180,2,4," & Chr(34) & "!105" & txtInventoryCode.Text.Trim & Chr(34)
        ''' sInventoryCode = "BARCODE 165,103," & Chr(34) & "128M" & Chr(34) & ",45,0,180,1,3," & Chr(34) & "!105" & txtInventoryCode.Text.Trim & Chr(34)
        sInventoryCode = "BARCODE 225,87," & Chr(34) & "128M" & Chr(34) & ",44,0,180,2,4," & Chr(34) & "!105" & txtInventoryCode.Text.Trim & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sInventoryCode)


        'Inventory
        '''sInventoryCode = "TEXT 110,49," & Chr(34) & "3" & Chr(34) & ",180,1,1," & Chr(34) & txtInventoryCode.Text.Trim & Chr(34)
        'sInventoryCode = "TEXT 238,76," & Chr(34) & "3" & Chr(34) & ",180,12,12," & Chr(34) & txtInventoryCode.Text.Trim & Chr(34)
        sInventoryCode = "TEXT 215,37," & Chr(34) & "0" & Chr(34) & ",180,11,7," & Chr(34) & txtInventoryCode.Text.Trim & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sInventoryCode)

        'Description
        'sDescription = Mid(txtDescription.Text.Trim, 1, 13)
        'sDescription = "TEXT 165,201," & Chr(34) & "ROMAN.TTF" & Chr(34) & ",180,5,9," & Chr(34) & sDescription & Chr(34)
        'subBarCodeDetailsToFile(gsBarCodeFilePath, sDescription)

        ''Inv 
        'sInventoryCode = "TEXT 20,151," & Chr(34) & "ROMAN.TTF" & Chr(34) & ",90,11,8," & Chr(34) & txtInventoryCode.Text.Trim & Chr(34)
        'subBarCodeDetailsToFile(gsBarCodeFilePath, sInventoryCode)

        'No of lables
        subBarCodeDetailsToFile(gsBarCodeFilePath, "PRINT 1," & IIf(txtNoofLabels.Text.Trim = "", 1, txtNoofLabels.Text.Trim))


        Dim cPrinter As New clsBarcode
        sPrinterName = "TSC TTP-244 Plus"
        cPrinter.SendFileToPrinter(sPrinterName, gsBarCodeFilePath)
    End Sub


    Private Sub subPrintBarCodeMeenaold()
        Dim sInventoryCode, sDescription, sGoldLoss, sWeight, sSMC, sSupplierCodeWithPurchaseDate, sClass, sCompany As String

        subDeleteBarCodeFile(gsBarCodeFilePath)
        'subBarCodeDetailsToFile(gsBarCodeFilePath, "SIZE 22.5 mm, 35 mm")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SIZE 22.5 mm, 34 mm")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "GAP 3 mm, 0 mm")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SPEED 4")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "DENSITY 7")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "DIRECTION 0,0")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "REFERENCE 0,0")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "OFFSET 0 mm")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SHIFT 0")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SET PEEL OFF")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SET CUTTER OFF")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SET PARTIAL_CUTTER OFF")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SET TEAR ON")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "CLS")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "CODEPAGE 850")

        'Company Name
        sCompany = "NEW GOLDEN"
        sCompany = "TEXT 165,245," & Chr(34) & "ROMAN.TTF" & Chr(34) & ",180,5,9," & Chr(34) & sCompany & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sCompany)

        'Company Name
        sCompany = "TRADERS"
        sCompany = "TEXT 165,225," & Chr(34) & "ROMAN.TTF" & Chr(34) & ",180,5,9," & Chr(34) & sCompany & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sCompany)

        'MC
        sSMC = IIf(txtSMC.Text.Trim <> "", "MC:$" & txtSMC.Text.Trim, "")
        '''sSMC = "TEXT 165,197," & Chr(34) & "0" & Chr(34) & ",180,8,7," & Chr(34) & sSMC & Chr(34)
        sSMC = "TEXT 165,177," & Chr(34) & "0" & Chr(34) & ",180,9,8," & Chr(34) & sSMC & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sSMC)

        'Weight
        sWeight = IIf(txtWeight.Text.Trim <> "", txtWeight.Text.Trim & "gm", "")
        sWeight = "TEXT 165,158," & Chr(34) & "0" & Chr(34) & ",180,12,11," & Chr(34) & sWeight & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sWeight)


        'Category
        sClass = "TEXT 165,115," & Chr(34) & "0" & Chr(34) & ",180,8,7," & Chr(34) & sCategorycode & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sClass)

        'Barcode
        '''sInventoryCode = "BARCODE 133,103," & Chr(34) & "128M" & Chr(34) & ",38,0,180,2,4," & Chr(34) & "!105" & txtInventoryCode.Text.Trim & Chr(34)
        ''' sInventoryCode = "BARCODE 165,103," & Chr(34) & "128M" & Chr(34) & ",45,0,180,1,3," & Chr(34) & "!105" & txtInventoryCode.Text.Trim & Chr(34)
        sInventoryCode = "BARCODE 165,93," & Chr(34) & "128M" & Chr(34) & ",45,0,180,1,3," & Chr(34) & "!105" & txtInventoryCode.Text.Trim & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sInventoryCode)


        'Inventory
        '''sInventoryCode = "TEXT 110,49," & Chr(34) & "3" & Chr(34) & ",180,1,1," & Chr(34) & txtInventoryCode.Text.Trim & Chr(34)
        sInventoryCode = "TEXT 149,39," & Chr(34) & "3" & Chr(34) & ",180,1,1," & Chr(34) & txtInventoryCode.Text.Trim & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sInventoryCode)

        'Description
        sDescription = Mid(txtDescription.Text.Trim, 1, 13)
        sDescription = "TEXT 165,201," & Chr(34) & "ROMAN.TTF" & Chr(34) & ",180,5,9," & Chr(34) & sDescription & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sDescription)

        'Inv 
        sInventoryCode = "TEXT 20,151," & Chr(34) & "ROMAN.TTF" & Chr(34) & ",90,11,8," & Chr(34) & txtInventoryCode.Text.Trim & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sInventoryCode)

        'No of lables
        subBarCodeDetailsToFile(gsBarCodeFilePath, "PRINT 1," & IIf(txtNoofLabels.Text.Trim = "", 1, txtNoofLabels.Text.Trim))


        Dim cPrinter As New clsBarcode
        sPrinterName = "TSC TTP-244 Plus"
        cPrinter.SendFileToPrinter(sPrinterName, gsBarCodeFilePath)
    End Sub


    Private Sub subPrintBarCodeDiaMeena()
        Dim sInventoryCode, sDescription, sGoldLoss, sWeight, sSMC, sSupplierCodeWithPurchaseDate, sClass, sCompany As String

        subDeleteBarCodeFile(gsBarCodeFilePath)
        'subBarCodeDetailsToFile(gsBarCodeFilePath, "SIZE 22.5 mm, 35 mm")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SIZE 22.5 mm, 34 mm")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "GAP 3 mm, 0 mm")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SPEED 4")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "DENSITY 7")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "DIRECTION 0,0")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "REFERENCE 0,0")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "OFFSET 0 mm")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SHIFT 0")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SET PEEL OFF")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SET CUTTER OFF")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SET PARTIAL_CUTTER OFF")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SET TEAR ON")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "CLS")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "CODEPAGE 850")

        'Company Name
        sCompany = "Meena"
        sCompany = "TEXT 165,255," & Chr(34) & "ROMAN.TTF" & Chr(34) & ",180,5,9," & Chr(34) & sCompany & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sCompany)

        'Company Name
        sCompany = "Gold Jewellers"
        sCompany = "TEXT 165,235," & Chr(34) & "ROMAN.TTF" & Chr(34) & ",180,5,9," & Chr(34) & sCompany & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sCompany)



        'Shabeer 20250605
        ''Weight
        'sWeight = IIf(txtWeight.Text.Trim <> "", txtWeight.Text.Trim & "gm", "")
        'sWeight = "TEXT 165,168," & Chr(34) & "0" & Chr(34) & ",180,12,11," & Chr(34) & sWeight & Chr(34)
        'subBarCodeDetailsToFile(gsBarCodeFilePath, sWeight)


        'Category
        sClass = "TEXT 165,125," & Chr(34) & "0" & Chr(34) & ",180,8,7," & Chr(34) & sCategorycode & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sClass)

        'Barcode
        '''sInventoryCode = "BARCODE 133,103," & Chr(34) & "128M" & Chr(34) & ",38,0,180,2,4," & Chr(34) & "!105" & txtInventoryCode.Text.Trim & Chr(34)
        sInventoryCode = "BARCODE 165,103," & Chr(34) & "128M" & Chr(34) & ",45,0,180,1,3," & Chr(34) & "!105" & txtInventoryCode.Text.Trim & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sInventoryCode)


        'Inventory
        '''sInventoryCode = "TEXT 110,49," & Chr(34) & "3" & Chr(34) & ",180,1,1," & Chr(34) & txtInventoryCode.Text.Trim & Chr(34)
        sInventoryCode = "TEXT 149,49," & Chr(34) & "3" & Chr(34) & ",180,1,1," & Chr(34) & txtInventoryCode.Text.Trim & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sInventoryCode)

        'Description
        'shabeer sDescription = Mid(txtDescription.Text.Trim, 1, 13)
        sDescription = Mid(Trim(sAddDescription1), 1, 13)
        'shabeer sDescription = "TEXT 165,211," & Chr(34) & "ROMAN.TTF" & Chr(34) & ",180,5,9," & Chr(34) & sDescription & Chr(34)
        sDescription = "TEXT 165,211," & Chr(34) & "0" & Chr(34) & ",180,8,7," & Chr(34) & sDescription & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sDescription)

        'Description
        'shabeer sDescription = Mid(txtDescription.Text.Trim, 1, 13)
        sDescription = Mid(Trim(sAddDescription2), 1, 13)
        sDescription = "TEXT 165,187," & Chr(34) & "0" & Chr(34) & ",180,8,7," & Chr(34) & sDescription & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sDescription)


        'Description
        'shabeer sDescription = Mid(txtDescription.Text.Trim, 1, 13)
        sDescription = Mid(Trim(sAddDescription3), 1, 13)
        sDescription = "TEXT 165,163," & Chr(34) & "0" & Chr(34) & ",180,8,7," & Chr(34) & sDescription & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sDescription)

        'Description
        'shabeer sDescription = Mid(txtDescription.Text.Trim, 1, 13)
        sDescription = Mid(Trim(sAddDescription4), 1, 13)
        sDescription = "TEXT 165,139," & Chr(34) & "0" & Chr(34) & ",180,8,7," & Chr(34) & sDescription & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sDescription)

        'Inv 
        sInventoryCode = "TEXT 20,151," & Chr(34) & "ROMAN.TTF" & Chr(34) & ",90,11,8," & Chr(34) & txtInventoryCode.Text.Trim & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sInventoryCode)


        'MC
        'sSMC = IIf(txtSMC.Text.Trim <> "", "Price:$" & Microsoft.VisualBasic.Left(txtSMC.Text.Trim, Len(txtSMC.Text.Trim) - 3), "")
        sSMC = IIf(txtSMC.Text.Trim <> "", "$" & Microsoft.VisualBasic.Left(txtSMC.Text.Trim, Len(txtSMC.Text.Trim) - 3), "")
        '''sSMC = "TEXT 165,197," & Chr(34) & "0" & Chr(34) & ",180,8,7," & Chr(34) & sSMC & Chr(34)
        'shabeer sSMC = "TEXT 165,187," & Chr(34) & "0" & Chr(34) & ",180,9,8," & Chr(34) & sSMC & Chr(34)
        sSMC = "TEXT 25,20," & Chr(34) & "ROMAN.TTF" & Chr(34) & ",90,11,11," & Chr(34) & sSMC & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sSMC)

        'No of lables
        subBarCodeDetailsToFile(gsBarCodeFilePath, "PRINT 1," & IIf(txtNoofLabels.Text.Trim = "", 1, txtNoofLabels.Text.Trim))


        Dim cPrinter As New clsBarcode
        sPrinterName = "TSC TTP-244 Plus"
        cPrinter.SendFileToPrinter(sPrinterName, gsBarCodeFilePath)
    End Sub

    Private Sub subPrintBarCodeMadhura()
        Dim sInventoryCode, sDescription, sGoldLoss, sWeight, sSMC, sSupplierCodeWithPurchaseDate, sClass As String
        Dim sCompanyName1, sCompanyName2 As String

        subDeleteBarCodeFile(gsBarCodeFilePath)
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SIZE 22.5 mm, 35 mm")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "GAP 3 mm, 0 mm")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SPEED 4")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "DENSITY 7")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "DIRECTION 0,0")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "REFERENCE 0,0")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "OFFSET 0 mm")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SHIFT 0")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SET PEEL OFF")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SET CUTTER OFF")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SET PARTIAL_CUTTER OFF")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SET TEAR ON")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "CLS")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "CODEPAGE 850")


        'Description
        sCompanyName1 = "MADURAS"
        sCompanyName1 = "TEXT 165,260," & Chr(34) & "ROMAN.TTF" & Chr(34) & ",180,5,9," & Chr(34) & sDescription & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sCompanyName1)

        'Description
        sCompanyName2 = "JEWELLERY"
        sCompanyName2 = "TEXT 165,240," & Chr(34) & "ROMAN.TTF" & Chr(34) & ",180,5,9," & Chr(34) & sDescription & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sCompanyName2)

        'MC
        sSMC = IIf(txtSMC.Text.Trim <> "", "MC:$" & txtSMC.Text.Trim, "")
        '''sSMC = "TEXT 165,197," & Chr(34) & "0" & Chr(34) & ",180,8,7," & Chr(34) & sSMC & Chr(34)
        sSMC = "TEXT 165,197," & Chr(34) & "0" & Chr(34) & ",180,9,8," & Chr(34) & sSMC & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sSMC)

        'Weight
        sWeight = IIf(txtWeight.Text.Trim <> "", txtWeight.Text.Trim & "gm", "")
        sWeight = "TEXT 165,178," & Chr(34) & "0" & Chr(34) & ",180,12,11," & Chr(34) & sWeight & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sWeight)


        'Category
        sClass = "TEXT 165,125," & Chr(34) & "0" & Chr(34) & ",180,8,7," & Chr(34) & sCategorycode & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sClass)

        'Barcode
        '''sInventoryCode = "BARCODE 133,103," & Chr(34) & "128M" & Chr(34) & ",38,0,180,2,4," & Chr(34) & "!105" & txtInventoryCode.Text.Trim & Chr(34)
        sInventoryCode = "BARCODE 165,103," & Chr(34) & "128M" & Chr(34) & ",45,0,180,1,3," & Chr(34) & "!105" & txtInventoryCode.Text.Trim & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sInventoryCode)


        'Inventory
        '''sInventoryCode = "TEXT 110,49," & Chr(34) & "3" & Chr(34) & ",180,1,1," & Chr(34) & txtInventoryCode.Text.Trim & Chr(34)
        sInventoryCode = "TEXT 149,49," & Chr(34) & "3" & Chr(34) & ",180,1,1," & Chr(34) & txtInventoryCode.Text.Trim & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sInventoryCode)

        'Description
        sDescription = Mid(txtDescription.Text.Trim, 1, 13)
        sDescription = "TEXT 165,220," & Chr(34) & "ROMAN.TTF" & Chr(34) & ",180,5,9," & Chr(34) & sDescription & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sDescription)

        'Inv 
        sInventoryCode = "TEXT 20,151," & Chr(34) & "ROMAN.TTF" & Chr(34) & ",90,11,8," & Chr(34) & txtInventoryCode.Text.Trim & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sInventoryCode)

        'No of lables
        subBarCodeDetailsToFile(gsBarCodeFilePath, "PRINT 1," & IIf(txtNoofLabels.Text.Trim = "", 1, txtNoofLabels.Text.Trim))


        Dim cPrinter As New clsBarcode
        sPrinterName = "TSC TTP-244 Plus"
        cPrinter.SendFileToPrinter(sPrinterName, gsBarCodeFilePath)
    End Sub
    'Meena Barcode on 09 03 2011
    Private Sub subPrintBarCodeSSJewel()
        Dim sInventoryCode, sDescription, sGoldLoss, sWeight, sSMC, sSupplierCodeWithPurchaseDate, sClass As String
        Dim dNo As Double
        Dim sChar As String
        Dim dsMCC As New DataSet
        Dim objinv As New clsInventory
        subDeleteBarCodeFile(gsBarCodeFilePath)
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SIZE 22.5 mm, 35 mm")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "GAP 3 mm, 0 mm")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SPEED 4")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "DENSITY 7")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "DIRECTION 0,0")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "REFERENCE 0,0")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "OFFSET 0 mm")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SHIFT 0")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SET PEEL OFF")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SET CUTTER OFF")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SET PARTIAL_CUTTER OFF")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SET TEAR ON")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "CLS")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "CODEPAGE 850")

        'MC
        dsMCC = objinv.GetMcConvertion(txtSMC.Text.Trim)
        If dsMCC.Tables(0).Rows.Count > 0 Then
            sChar = Mid(Trim(dsMCC.Tables(0).Rows(0)("Mc") & ""), 1, Len(Trim(dsMCC.Tables(0).Rows(0)("Mc") & "")) - 3) 'Remove the decimal part
            sSMC = "TEXT 165,207," & Chr(34) & "0" & Chr(34) & ",180,9,8," & Chr(34) & sChar & Chr(34)
            subBarCodeDetailsToFile(gsBarCodeFilePath, sSMC)
        End If

        ' sSMC = IIf(txtSMC.Text.Trim <
        ' sSMC = IIf(txtSMC.Text.Trim <> "", "MC:$" & txtSMC.Text.Trim, "")
        '''sSMC = "TEXT 165,197," & Chr(34) & "0" & Chr(34) & ",180,8,7," & Chr(34) & sSMC & Chr(34)
        'sSMC = "TEXT 165,207," & Chr(34) & "0" & Chr(34) & ",180,9,8," & Chr(34) & sSMC & Chr(34)
        'subBarCodeDetailsToFile(gsBarCodeFilePath, sSMC)

        'Weight
        sWeight = IIf(txtWeight.Text.Trim <> "", txtWeight.Text.Trim & "gm", "")
        sWeight = "TEXT 165,188," & Chr(34) & "0" & Chr(34) & ",180,12,11," & Chr(34) & sWeight & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sWeight)


        'Category
        sClass = "TEXT 165,125," & Chr(34) & "0" & Chr(34) & ",180,8,7," & Chr(34) & sCategorycode & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sClass)

        'Barcode
        '''sInventoryCode = "BARCODE 133,103," & Chr(34) & "128M" & Chr(34) & ",38,0,180,2,4," & Chr(34) & "!105" & txtInventoryCode.Text.Trim & Chr(34)
        sInventoryCode = "BARCODE 158,103," & Chr(34) & "128M" & Chr(34) & ",45,0,180,1,3," & Chr(34) & "!105" & txtInventoryCode.Text.Trim & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sInventoryCode)


        'Inventory
        '''sInventoryCode = "TEXT 110,49," & Chr(34) & "3" & Chr(34) & ",180,1,1," & Chr(34) & txtInventoryCode.Text.Trim & Chr(34)
        sInventoryCode = "TEXT 155,49," & Chr(34) & "3" & Chr(34) & ",180,1,1," & Chr(34) & txtInventoryCode.Text.Trim & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sInventoryCode)

        'Description
        sDescription = Mid(txtDescription.Text.Trim, 1, 13)
        sDescription = "TEXT 165,230," & Chr(34) & "ROMAN.TTF" & Chr(34) & ",180,5,9," & Chr(34) & sDescription & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sDescription)

        'Inv 
        ''sInventoryCode = "TEXT 20,151," & Chr(34) & "ROMAN.TTF" & Chr(34) & ",90,11,8," & Chr(34) & txtInventoryCode.Text.Trim & Chr(34)
        ''subBarCodeDetailsToFile(gsBarCodeFilePath, sInventoryCode)

        'No of lables
        subBarCodeDetailsToFile(gsBarCodeFilePath, "PRINT 1," & IIf(txtNoofLabels.Text.Trim = "", 1, txtNoofLabels.Text.Trim))


        Dim cPrinter As New clsBarcode
        sPrinterName = "TSC TTP-244 Plus"
        cPrinter.SendFileToPrinter(sPrinterName, gsBarCodeFilePath)
    End Sub
    Private Sub subPrintBarCodeRJewel()
        Dim sInventoryCode, sDescription, sGoldLoss, sWeight, sSMC, sSupplierCodeWithPurchaseDate, sClass, sCompany As String
        Dim dNo As Double
        Dim sChar As String
        Dim dsMCC As New DataSet
        Dim objinv As New clsInventory
        subDeleteBarCodeFile(gsBarCodeFilePath)
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SIZE 22.5 mm, 35 mm")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "GAP 3 mm, 0 mm")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SPEED 4")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "DENSITY 7")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "DIRECTION 0,0")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "REFERENCE 0,0")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "OFFSET 0 mm")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SHIFT 0")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SET PEEL OFF")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SET CUTTER OFF")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SET PARTIAL_CUTTER OFF")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SET TEAR ON")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "CLS")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "CODEPAGE 850")

        'MC
        '''dsMCC = objinv.GetMcConvertion(txtSMC.Text.Trim)
        '''If dsMCC.Tables(0).Rows.Count > 0 Then
        '''    sChar = Mid(Trim(dsMCC.Tables(0).Rows(0)("Mc") & ""), 1, Len(Trim(dsMCC.Tables(0).Rows(0)("Mc") & "")) - 3) 'Remove the decimal part
        '''    sSMC = "TEXT 165,207," & Chr(34) & "0" & Chr(34) & ",180,9,8," & Chr(34) & sChar & Chr(34)
        '''    subBarCodeDetailsToFile(gsBarCodeFilePath, sSMC)
        '''End If


        'MC
        sSMC = IIf(txtSMC.Text.Trim <> "", "MC:$" & txtSMC.Text.Trim, "")
        '''sSMC = "TEXT 165,197," & Chr(34) & "0" & Chr(34) & ",180,8,7," & Chr(34) & sSMC & Chr(34)
        sSMC = "TEXT 165,207," & Chr(34) & "0" & Chr(34) & ",180,9,8," & Chr(34) & sSMC & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sSMC)

        ' sSMC = IIf(txtSMC.Text.Trim <
        ' sSMC = IIf(txtSMC.Text.Trim <> "", "MC:$" & txtSMC.Text.Trim, "")
        '''sSMC = "TEXT 165,197," & Chr(34) & "0" & Chr(34) & ",180,8,7," & Chr(34) & sSMC & Chr(34)
        'sSMC = "TEXT 165,207," & Chr(34) & "0" & Chr(34) & ",180,9,8," & Chr(34) & sSMC & Chr(34)
        'subBarCodeDetailsToFile(gsBarCodeFilePath, sSMC)

        'Weight
        sWeight = IIf(txtWeight.Text.Trim <> "", txtWeight.Text.Trim & "gm", "")
        sWeight = "TEXT 165,188," & Chr(34) & "0" & Chr(34) & ",180,12,11," & Chr(34) & sWeight & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sWeight)


        'Category
        sClass = "TEXT 165,125," & Chr(34) & "0" & Chr(34) & ",180,8,7," & Chr(34) & sCategorycode & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sClass)

        'Barcode
        '''sInventoryCode = "BARCODE 133,103," & Chr(34) & "128M" & Chr(34) & ",38,0,180,2,4," & Chr(34) & "!105" & txtInventoryCode.Text.Trim & Chr(34)
        sInventoryCode = "BARCODE 158,103," & Chr(34) & "128M" & Chr(34) & ",45,0,180,1,3," & Chr(34) & "!105" & txtInventoryCode.Text.Trim & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sInventoryCode)


        'Inventory
        '''sInventoryCode = "TEXT 110,49," & Chr(34) & "3" & Chr(34) & ",180,1,1," & Chr(34) & txtInventoryCode.Text.Trim & Chr(34)
        sInventoryCode = "TEXT 155,49," & Chr(34) & "3" & Chr(34) & ",180,1,1," & Chr(34) & txtInventoryCode.Text.Trim & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sInventoryCode)

        'Description
        sDescription = Mid(txtDescription.Text.Trim, 1, 13)
        sDescription = "TEXT 165,230," & Chr(34) & "ROMAN.TTF" & Chr(34) & ",180,5,9," & Chr(34) & sDescription & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sDescription)


        'Company Name
        sCompany = "ROYAL JEWELS"
        sCompany = "TEXT 165,255," & Chr(34) & "ROMAN.TTF" & Chr(34) & ",180,5,9," & Chr(34) & sCompany & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sCompany)



        'Inv 
        ''sInventoryCode = "TEXT 20,151," & Chr(34) & "ROMAN.TTF" & Chr(34) & ",90,11,8," & Chr(34) & txtInventoryCode.Text.Trim & Chr(34)
        ''subBarCodeDetailsToFile(gsBarCodeFilePath, sInventoryCode)

        'No of lables
        subBarCodeDetailsToFile(gsBarCodeFilePath, "PRINT 1," & IIf(txtNoofLabels.Text.Trim = "", 1, txtNoofLabels.Text.Trim))


        Dim cPrinter As New clsBarcode
        sPrinterName = "TSC TTP-244 Plus"
        cPrinter.SendFileToPrinter(sPrinterName, gsBarCodeFilePath)
    End Sub

    Private Sub btnTraySearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTraySearch.Click
        Try
            Dim frmHelpTray As New frmHelpSearch(0, 0, "tblTraymaster", "L", 3, "TrayName", "", "TrayId", "TrayId", 80, "TrayName", "TrayName", 180, "IsCustomerTray", "IsCustomerTray", 80)
            frmHelpTray.ShowDialog()
            txtNewTrayID.Text = frmHelpTray.Var1.Trim
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub btnInventoryCodeSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInventoryCodeSearch.Click
        Try
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblInventory", "L", 4, "Description", "", "InventoryCode", "InventoryCode", 80, "Description", "Description", 180, "SupplierCode", "SupplierCode", 60, "Weight", "Weight", 80)
            frmHelpClass.ShowDialog()
            txtInventoryCode.Text = frmHelpClass.Var1.Trim
            txtInventoryCode_LostFocus(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub txtInventoryCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtInventoryCode.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                Dim frmHelpClass As New frmHelpSearch(0, 0, "tblInventory", "L", 4, "Description", "", "InventoryCode", "InventoryCode", 80, "Description", "Description", 180, "SupplierCode", "SupplierCode", 60, "Weight", "Weight", 80)
                frmHelpClass.ShowDialog()
                txtInventoryCode.Text = frmHelpClass.Var1.Trim
                txtInventoryCode_LostFocus(sender, e)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Public Sub txtInventoryCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInventoryCode.LostFocus
        Try
            If txtInventoryCode.Text.Trim <> "" Then
                subGetInventory()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub


    Public Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim objClass As New ClsClass
        Dim objInventory As New clsInventory
        Dim dS As DataSet
        Dim sClass As String
        Dim sPurType As String
        If sRFIDModule = "0" Then
            gsBarCodeFilePath = Application.StartupPath & "\" & gsBarCodeFile & ".txt"
            If UCase(sCompanyShortCode) = "ES" Then
                subPrintBarCode()
            ElseIf UCase(sCompanyShortCode) = "ML" Then
                sPurType = ""
                sClassName = ""
                sClass = Trim(txtClass.Text.Trim)
                dS = objClass.GetClassDetail(Trim(sClass))
                If dS.Tables("Class").Rows.Count > 0 Then
                    sPurType = Trim(dS.Tables("Class").Rows(0)("PurchaseType") & "")
                    sClassName = Trim(dS.Tables("Class").Rows(0)("ClassName") & "")
                End If
                dS = Nothing
                objClass = Nothing

                If UCase(sPurType) = "P" Then
                    subPrintJewelBarCodeML()
                Else
                    subPrintBarCodeML()
                End If
            ElseIf UCase(sCompanyShortCode) = "GMT" Then
                subPrintBarCodeGMT()
            ElseIf UCase(sCompanyShortCode) = "HN" Then
                subPrintBarCodeHaniffa()
            ElseIf UCase(sCompanyShortCode) = "MJ" Then
                If Trim(sAddDescription1) <> "" Then
                    ' subPrintBarCodeDiaMeena()
                    subPrintBarCodeMeenaZebra()
                Else
                    subPrintBarCodeMeenaZebra()
                End If
            ElseIf UCase(sCompanyShortCode) = "SSJ" Then
                subPrintBarCodeSSJewel()
            ElseIf UCase(sCompanyShortCode) = "RJ" Then
                subPrintBarCodeRJewel()
            ElseIf UCase(sCompanyShortCode) = "AJ" Then
                ''subPrintBarCodeAB()
                'subBarcodeReportPrintAJ()
                subPrintBarcodeAbirammeLPT()
            ElseIf UCase(sCompanyShortCode) = "MD" Then
                ''subPrintBarCodeAB()
                'subBarcodeReportPrintAJ()
                'subPrintBarCodeMadhura()
                subBarcodeReportPrint_MDJ()
            End If

        Else

            If Trim(txtNewTrayID.Text) <> "" Then
                If objClsTray.TrayTransfer(txtNewTrayID.Text.Trim, txtInventoryCode.Text.Trim, txtTrayID.Text.Trim, "") Then
                Else
                    MessageBox.Show("Error in updation.")
                End If
            End If

            If Trim(txtCategoryCode.Text) <> "" Then
                If Trim(sCategorycode) <> Trim(txtCategoryCode.Text) Then
                    If objClsTray.CategoryUpdate(txtInventoryCode.Text.Trim, txtCategoryCode.Text.Trim) Then
                    Else
                        MessageBox.Show("Error in updation.")
                    End If
                End If
            End If

            Dim dsMCC As New DataSet
            Dim sChar As String


            If UCase(sCompanyShortCode) = "MJ" Then

                If objInventory.InventoryRFIDSave(txtInventoryCode.Text.Trim, _
                    txtDescription.Text.Trim, txtDescription.Text.Trim, _
                    Val(txtWeight.Text), _
                    Val(sPMC), Val(txtSMC.Text), Val(txtGoldLoss.Text), "", _
                    Trim(sCategorycode), Trim(txtClass.Text), Trim(sCategorycode), txtTrayID.Text.Trim, "") Then


                Else
                    MsgBox("Error in Saving RFID Inventory", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            Else
                dsMCC = objInventory.GetMcConvertion(txtSMC.Text.Trim)
                sChar = Mid(Trim(dsMCC.Tables(0).Rows(0)("Mc") & ""), 1, Len(Trim(dsMCC.Tables(0).Rows(0)("Mc") & "")) - 3)


                If objInventory.InventoryRFIDSave(txtInventoryCode.Text.Trim, _
                    txtDescription.Text.Trim, txtDescription.Text.Trim, _
                    Val(txtWeight.Text), _
                    Val(sPMC), Val(txtSMC.Text), Val(txtGoldLoss.Text), Trim(sSupplierCode), _
                    Trim(sCategorycode), Trim(txtClass.Text), Trim(txtCategoryCode.Text), txtTrayID.Text.Trim, sChar) Then


                Else
                    MsgBox("Error in Saving RFID Inventory", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End If


        End If
        btnNew_Click(sender, e)

    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click

        txtBarcode.Text = ""
        txtInventoryCode.Text = ""
        txtDescription.Text = ""
        txtBarcode.Text = ""
        txtWeight.Text = ""
        txtGoldLoss.Text = ""
        txtSMC.Text = ""
        txtClass.Text = ""
        txtPrice.Text = 0
        txtNoofLabels.Text = "1"
        sAddDescription1 = ""
        sAddDescription2 = ""
        sAddDescription3 = ""
        sAddDescription4 = ""
        sAddCost1 = 0
        sAddCost2 = 0
        sAddCost3 = 0
        sAddCost4 = 0
        sAddSellingPrice1 = 0
        sAddSellingPrice2 = 0
        sAddSellingPrice3 = 0
        sAddSellingPrice4 = 0
        txtCategoryCode.Text = ""
        txtInventoryCode.Focus()
    End Sub
    Private Sub subBarcodeReportPrintAJ()

        Dim sSql$
        Dim sTemp As String
        Dim sChar As String
        Dim strCompanyname, strAddress, strReportTitle, strpath, strselectformula, strDateFrom, strDateTo As String
        Dim strSubHead As String
        Dim CR As New ReportDocument
        Dim frmRep As New frmReports
        Dim objCompany As New clsCompany
        Dim dsMCC As New DataSet
        Dim objinv As New clsInventory
        Dim ds As DataSet
        Try
            strpath = Application.StartupPath & "\Reports\rptBarcode_AJ.rpt"
            Me.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            ds = objCompany.getCompanyName
            If ds.Tables("GetCompanyName").Rows.Count > 0 Then
                strCompanyname = Trim(ds.Tables("GetCompanyName").Rows(0)("Name") & "")
                strAddress = Trim(ds.Tables("GetCompanyName").Rows(0)("Address1") & "") & " " & _
                             Trim(ds.Tables("GetCompanyName").Rows(0)("Address2") & "") & " " & _
                             Trim(ds.Tables("GetCompanyName").Rows(0)("City") & "")
            End If
            frmRep.GetModule = "Barcode"
            frmRep.GetReportType = "Summary"
            frmRep.GetPrintOption = "Preview"

            dsMCC = objinv.GetMcConvertion(sPMC)
            sChar = Mid(Trim(dsMCC.Tables(0).Rows(0)("Mc") & ""), 1, Len(Trim(dsMCC.Tables(0).Rows(0)("Mc") & "")) - 3)

            CR.Load(strpath)
            ReportLogOnToServer(CR)
            CR.Refresh()
            frmRep.ReportViewer.ReportSource = CR
            strselectformula = "{tblInventory.InventoryCode}=" & fncFormatSQLText(Trim(txtInventoryCode.Text))

            CR.DataDefinition.FormulaFields("ClassCode").Text = fncFormatSQLText(txtClass.Text.Trim)
            CR.DataDefinition.FormulaFields("InventoryCode").Text = fncFormatSQLText(txtInventoryCode.Text.Trim)
            CR.DataDefinition.FormulaFields("Description").Text = fncFormatSQLText(txtDescription.Text.Trim)
            CR.DataDefinition.FormulaFields("Weight").Text = fncFormatSQLText(Format(Val(txtWeight.Text.Trim), "###0.00") + "gm")
            ' CR.DataDefinition.FormulaFields("GLOSS").Text = fncFormatSQLText(Format(GlOSS, "###0.00") + "gm")
            'CR.DataDefinition.FormulaFields("TotWeight").Text = fncFormatSQLText(Format(Val(txtWeight.Text), "###0.00") + "gm")
            'CR.DataDefinition.FormulaFields("sTemp").Text = fncFormatSQLText(sTemp)
            ' CR.DataDefinition.FormulaFields("SMC").Text = fncFormatSQLText("LB" + txtSMC.Text.Trim)
            CR.DataDefinition.FormulaFields("SMC").Text = fncFormatSQLText(txtSMC.Text.Trim)
            CR.DataDefinition.FormulaFields("PMC").Text = fncFormatSQLText(sChar)
            CR.DataDefinition.FormulaFields("SizeCode").Text = fncFormatSQLText(sSizeCode)
            CR.DataDefinition.FormulaFields("AddDesc1").Text = fncFormatSQLText(sAddDescription1)
            CR.DataDefinition.FormulaFields("AddDesc2").Text = fncFormatSQLText(sAddDescription2)
            CR.DataDefinition.FormulaFields("AddDesc3").Text = fncFormatSQLText(sAddDescription3)
            CR.DataDefinition.FormulaFields("AddDesc4").Text = fncFormatSQLText(sAddDescription4)
            CR.DataDefinition.FormulaFields("AddCost1").Text = fncFormatSQLText(sAddCost1)
            CR.DataDefinition.FormulaFields("AddCost2").Text = fncFormatSQLText(sAddCost2)
            CR.DataDefinition.FormulaFields("AddCost3").Text = fncFormatSQLText(sAddCost3)
            CR.DataDefinition.FormulaFields("AddCost4").Text = fncFormatSQLText(sAddCost4)
            CR.DataDefinition.FormulaFields("AddSellingPrice1").Text = fncFormatSQLText(sAddSellingPrice1)
            CR.DataDefinition.FormulaFields("AddSellingPrice2").Text = fncFormatSQLText(sAddSellingPrice2)
            CR.DataDefinition.FormulaFields("AddSellingPrice3").Text = fncFormatSQLText(sAddSellingPrice3)
            CR.DataDefinition.FormulaFields("AddSellingPrice4").Text = fncFormatSQLText(sAddSellingPrice4)
            CR.DataDefinition.FormulaFields("InventoryCode").Text = fncFormatSQLText(txtInventoryCode.Text.Trim)


            frmRep.ReportViewer.SelectionFormula = strselectformula
            CR.DataDefinition.RecordSelectionFormula = strselectformula
            ' ''frmRep.ShowDialog()
            ' ''frmRep.ReportViewer.Visible = True
            ' ''frmRep.ReportViewer.DisplayToolbar = True
            ' ''frmRep.ReportViewer.CloseView(False)
            ' ''frmRep.WindowState = FormWindowState.Maximized
            CR.PrintToPrinter(1, True, 0, 0)
            frmRep = Nothing
            frmRep = New frmReports
            Me.Cursor.Current = System.Windows.Forms.Cursors.Default


            CR.Close()
            CR.Dispose()
            CR = Nothing


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subPrintBarcodeAbirammeLPT()
        Dim sBarInvCode, sBarSMC, sBarPMC, sTrayID, sBarClass, sBarDesc, sBarWeight, sBarStoneWeight, sCompName, sBarcode, sBarSupplier, sBarSize As String
        Dim sBarAddDesc1, sBarAddDesc2, sBarAddDesc3, sChar As String
        Dim dsMCC As New DataSet
        Dim objinv As New clsInventory
        Dim sQuote As String
        Dim sNoOfLabels As String

        subDeleteBarCodeFile(gsBarCodeFilePath)

        'Dim objReader As StreamWriter
        ''Open("LPT1:" For Output As #1
        'FileOpen(1, "LPT1:", OpenMode.Output)

        'PrintLine(1, "n")
        'PrintLine(1, "M0500")
        'PrintLine(1, "O0220")
        'PrintLine(1, "V0")
        'PrintLine(1, "f220")
        'PrintLine(1, "L")
        'PrintLine(1, "N")
        'PrintLine(1, "D11")
        'PrintLine(1, "A2")

        'PrintLine(1, "3911A060035009125.60 g")

        'PrintLine(1, "3911A060035003628 INCH") ''''7
        'PrintLine(1, "3911A060044009125.60") ''''6
        'PrintLine(1, "33911A0600590091SQUARE ICE CREAM") ''''7

        'PrintLine(1, "3911A0600680091SQUARE ICE CREAM") ''''6
        'PrintLine(1, "3911A0601340091C100067") ''''7
        'PrintLine(1, "3911A0601340020MIK") ''''6
        'PrintLine(1, "3911A0600840087135.00") ''''7
        'PrintLine(1, "3911A0600840020YN") ''''6
        'PrintLine(1, "3e4203101160088BC&D100067") ''''7

        'PrintLine(1, "Q0001") ''''6
        'PrintLine(1, "E") ''''7

        'objReader.WriteLine("n")
        'objReader.WriteLine("M0500")
        'objReader.WriteLine("O0220")
        'objReader.WriteLine("V0")
        'objReader.WriteLine("f220")
        'objReader.WriteLine("L")
        'objReader.WriteLine("N")
        'objReader.WriteLine("D11")
        'objReader.WriteLine("A2")

        'objReader.WriteLine("3911A060035009125.60 g")

        'objReader.WriteLine("3911A060035003628 INCH") ''''7
        'objReader.WriteLine("3911A060044009125.60") ''''6
        'objReader.WriteLine("33911A0600590091SQUARE ICE CREAM") ''''7

        'objReader.WriteLine("3911A0600680091SQUARE ICE CREAM") ''''6
        'objReader.WriteLine("3911A0601340091C100067") ''''7
        'objReader.WriteLine("3911A0601340020MIK") ''''6
        'objReader.WriteLine("3911A0600840087135.00") ''''7
        'objReader.WriteLine("3911A0600840020YN") ''''6
        'objReader.WriteLine("3e4203101160088BC&D100067") ''''7

        'objReader.WriteLine("Q0001") ''''6
        'objReader.WriteLine("E") ''''7




        'DEFAULT SETTINGS

        subBarCodeDetailsToFile(gsBarCodeFilePath, Chr(2) & Chr(110)) ''''1
        subBarCodeDetailsToFile(gsBarCodeFilePath, Chr(2) & "M0500") ''''2
        subBarCodeDetailsToFile(gsBarCodeFilePath, Chr(2) & "O0220") ''''3
        subBarCodeDetailsToFile(gsBarCodeFilePath, Chr(2) & "V0") ''''4
        subBarCodeDetailsToFile(gsBarCodeFilePath, Chr(2) & "f220") ''''5
        subBarCodeDetailsToFile(gsBarCodeFilePath, Chr(1) & "D") ''''6
        subBarCodeDetailsToFile(gsBarCodeFilePath, Chr(2) & "L") ''''6
        subBarCodeDetailsToFile(gsBarCodeFilePath, "D11") ''''6
        subBarCodeDetailsToFile(gsBarCodeFilePath, "A2") ''''7

        'DEFAULT SETTINGS

        'Weight
        If Val(txtGoldLoss.Text) > 0 Then
            sBarWeight = Val(txtWeight.Text) + Val(txtGoldLoss.Text) & "g"
        ElseIf Val(txtWeight.Text) > 0 And Val(sStoneWeight) > 0 Then
            sBarWeight = Format((txtWeight.Text) - (sStoneWeight), "#0.00") & "g"
        ElseIf Val(txtWeight.Text) = 0 Then
            sBarWeight = "$" & (txtSMC.Text)
        ElseIf Trim(sClassCode) = "STONES" Then
            sBarWeight = Format((txtWeight.Text), "#0.00") & "CT"
        Else
            sBarWeight = (txtWeight.Text) & "g"
        End If

        sBarWeight = "3911A0800350091" & Trim(sBarWeight)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sBarWeight) ''''9

        'Size
        If Trim(sCategorycode) = "RING" And Trim(sSizeType) = "" Then
            sBarSize = ""
        ElseIf Trim(sSizeType) = "SIZE" And Microsoft.VisualBasic.Right(sSizeCode, 2) = "00" Then
            sBarSize = "SI-" & Microsoft.VisualBasic.Left(sSizeCode, Len(sSizeCode) - 3)
        ElseIf Trim(sSizeType) = "SIZE" And Microsoft.VisualBasic.Right(sSizeCode, 2) <> "00" Then
            sBarSize = "SI-" & Val(sSizeCode)
        ElseIf Trim(sSizeType) = "SET" Then
            sBarSize = "SET"
        ElseIf Trim(sClassCode) = "999.9" Or Trim(sClassCode) = "999" Then
            sBarSize = "24K"
        ElseIf Microsoft.VisualBasic.Right(sCategorycode, 11) = "SILVER IDOL" Then
            sBarSize = "999"
        ElseIf Trim(sSizeType) = "" Then
            sBarSize = ""
        ElseIf Microsoft.VisualBasic.Right(sSizeCode, 2) = "00" Then
            sBarSize = Microsoft.VisualBasic.Left(sSizeCode, Len(sSizeCode) - 3) & " " & Trim(sSizeType)
        ElseIf Microsoft.VisualBasic.Right(sSizeCode, 2) <> "00" Then
            sBarSize = Val(sSizeCode) & " " & Trim(sSizeType)
        ElseIf Trim(sCategorycode) = "OTHERS" And Trim(sCatalogueCode) = "RUDRAKSHA" And Microsoft.VisualBasic.Left(sSizeCode, 1) <> "9" Then
            sBarSize = Microsoft.VisualBasic.Left(sSizeCode, Len(sSizeCode) - 3) & " " & Trim(sSizeType)
        ElseIf Trim(sCategorycode) = "OTHERS" And Trim(sCatalogueCode) = "RUDRAKSHA" And Microsoft.VisualBasic.Left(sSizeCode, 1) = "9" Then
            sBarSize = Trim(sSizeType)
        ElseIf Trim(sSizeType) = "SET" Then
            sBarSize = Trim(sSizeType)
        Else
            sBarSize = Val(sSizeCode) & " " & Trim(sSizeType)
        End If

        sBarSize = "3911A0800350047" & Trim(sBarSize)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sBarSize) ''''9

        'StoneWeight
        If Trim(sClassCode) <> "STONES" And Val(txtWeight.Text) = 0 And Val(sStoneWeight) > 0 Then
            sBarStoneWeight = (sStoneWeight) & "g"
        ElseIf Trim(sClassCode) = "STONES" And Val(txtWeight.Text) = 0 And Val(sStoneWeight) > 0 Then
            sBarStoneWeight = (sStoneWeight) & "CT"
        ElseIf Trim(sAddDescription3) <> "" Then
            sBarStoneWeight = Trim(sAddDescription3)
        ElseIf Trim(sClassCode) = "STONES" Then
            sBarStoneWeight = (txtWeight.Text) & "CT"
        ElseIf Trim(sSMCType) = "P" And Trim(sClassCode) = "999.9" And Trim(sClassCode) = "999" Then
            sBarStoneWeight = (txtWeight.Text) & "g"
        ElseIf Trim(sSMCType) = "P" And Trim(sClassCode) <> "999.9" And Trim(sClassCode) <> "999" And Val(txtWeight.Text) = 0 Then
            sBarStoneWeight = (txtSMC.Text)
        Else
            sBarStoneWeight = (txtWeight.Text) & "g"
        End If

        sBarStoneWeight = "3911A0800460091" & Trim(sBarStoneWeight)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sBarStoneWeight) ''''9




        'AddDesc1

        sBarAddDesc1 = "3911A0600560091" & Trim(sAddDescription1)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sBarAddDesc1) ''''9

        'AddDesc2 & InventoryDescription

        'If Trim(sAddDescription2) <> "" Then
        '    sBarDesc = "3911A0600680091" & Trim(sAddDescription2)
        '    subBarCodeDetailsToFile(gsBarCodeFilePath, sBarDesc) ''''9
        'Else
        '    sBarDesc = "3911A0600680091" & Trim(txtDescription.Text)
        '    subBarCodeDetailsToFile(gsBarCodeFilePath, sBarDesc) ''''9
        'End If
        If Trim(sAddDescription2) <> "" Then
            sBarDesc = "3911A0600670091" & Trim(sAddDescription2)
            subBarCodeDetailsToFile(gsBarCodeFilePath, sBarDesc) ''''9
        Else
            sBarDesc = "3911A0600670091" & Trim(txtDescription.Text)
            subBarCodeDetailsToFile(gsBarCodeFilePath, sBarDesc) ''''9
        End If

        'InventoryCode
        sBarInvCode = "3911A0801350091" & Trim(txtInventoryCode.Text)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sBarInvCode) ''''9


        'SupplierCode
        sBarSupplier = "3911A0801350024" & StrReverse(Trim(sSupplierCode))
        subBarCodeDetailsToFile(gsBarCodeFilePath, sBarSupplier) ''''9

        'SMC
        'sBarSMC = "3911A0800860091" & (txtSMC.Text)
        'If Trim(sSMCType) = "P" Then
        '    sBarSMC = "3911A0800860091" & ("")

        If Val(txtWeight.Text) = 0 Then
            sBarSMC = "3911A0800860091" & ("")
        ElseIf sSalesPCS = True Then
            sBarSMC = "3911A0800860091" & ("")
        Else
            sBarSMC = "3911A0800860091" & (txtSMC.Text)
        End If

        subBarCodeDetailsToFile(gsBarCodeFilePath, sBarSMC) ''''9


        'PMC

        dsMCC = objinv.GetMcConvertion(sPMC)
        sChar = Mid(Trim(dsMCC.Tables(0).Rows(0)("Mc") & ""), 1, Len(Trim(dsMCC.Tables(0).Rows(0)("Mc") & "")) - 3)

        sBarPMC = "3911A0800860038" & Trim(sChar)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sBarPMC) ''''9


        'sBarPMC = "3911A0800860026" & Trim(sChar)
        'subBarCodeDetailsToFile(gsBarCodeFilePath, sBarPMC) ''''9

        'BARCODE
        'sBarcode = "3e4203501210088B" & Microsoft.VisualBasic.Left(txtInventoryCode.Text, 1) & "&D" & Microsoft.VisualBasic.Right(txtInventoryCode.Text, 6)
        sBarcode = "3e4202901180096B" & Microsoft.VisualBasic.Left(txtInventoryCode.Text, 1) & "&D" & Microsoft.VisualBasic.Right(txtInventoryCode.Text, 6)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sBarcode) ''''9

        'subBarCodeDetailsToFile(gsBarCodeFilePath, "3e4203101160088BC&D100067") ''''7

        'NO.OF LABELS
        sNoOfLabels = "Q000" & Val(txtNoofLabels.Text)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sNoOfLabels) ''''9

        subBarCodeDetailsToFile(gsBarCodeFilePath, "E") ''''7

        'subBarCodeDetailsToFile(gsBarCodeFilePath, "31110000033009125.60 g") ''''6
        'subBarCodeDetailsToFile(gsBarCodeFilePath, "31110000033003528 INCH") ''''7
        'subBarCodeDetailsToFile(gsBarCodeFilePath, "31110000043009125.60") ''''6
        'subBarCodeDetailsToFile(gsBarCodeFilePath, "311100000560075SQUARE ICE CREAM") ''''7

        'subBarCodeDetailsToFile(gsBarCodeFilePath, "311100000670075SQUARE ICE CREAM") ''''6
        'subBarCodeDetailsToFile(gsBarCodeFilePath, "311100001320091C100067") ''''7
        'subBarCodeDetailsToFile(gsBarCodeFilePath, "311100001320017MIK") ''''6
        'subBarCodeDetailsToFile(gsBarCodeFilePath, "311100000820087135.00") ''''7
        'subBarCodeDetailsToFile(gsBarCodeFilePath, "311100000820017YN") ''''6
        'subBarCodeDetailsToFile(gsBarCodeFilePath, "3e4203101160088BC&D100067") ''''7

        'subBarCodeDetailsToFile(gsBarCodeFilePath, "Q0001") ''''6
        'subBarCodeDetailsToFile(gsBarCodeFilePath, "E") ''''7

        'subBarCodeDetailsToFile(gsBarCodeFilePath, sNoOfLabels) ''''16 

        Dim cPrinter As New clsBarcode
        ' sPrinterName = "Argox"
        sPrinterName = "Argox OS-214 plus series PPLA"
        cPrinter.SendFileToPrinter(sPrinterName, gsBarCodeFilePath)
        ' MsgBox("Please Enter SMC", MsgBoxStyle.Information)
        'FileClose(1)
        Exit Sub
ErrTrap:
        MsgBox(Err.Description)
        Err.Clear()
        'Close #1
    End Sub

    Private Sub subBarcodeReportPrint_MDJ()

        Dim sSql$
        Dim sTemp As String
        Dim sChar As String
        Dim strCompanyname, strAddress, strReportTitle, strpath, strselectformula, strDateFrom, strDateTo As String
        Dim strSubHead As String
        Dim CR As New ReportDocument
        Dim frmRep As New frmReports
        Dim objCompany As New clsCompany
        Dim ds As DataSet
        Dim dsMCC As New DataSet
        Dim objinv As New clsInventory
        Try
            strpath = Application.StartupPath & "\Reports\rptBarCode_MDJ.rpt"
            Me.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            ds = objCompany.getCompanyName
            If ds.Tables("GetCompanyName").Rows.Count > 0 Then
                strCompanyname = Trim(ds.Tables("GetCompanyName").Rows(0)("Name") & "")
                strAddress = Trim(ds.Tables("GetCompanyName").Rows(0)("Address1") & "") & " " & _
                             Trim(ds.Tables("GetCompanyName").Rows(0)("Address2") & "") & " " & _
                             Trim(ds.Tables("GetCompanyName").Rows(0)("City") & "")
            End If
            frmRep.GetModule = "Barcode"
            frmRep.GetReportType = "Summary"
            frmRep.GetPrintOption = "Preview"

            dsMCC = objinv.GetMcConvertion(txtSMC.Text.Trim)
            sChar = Mid(Trim(dsMCC.Tables(0).Rows(0)("Mc") & ""), 1, Len(Trim(dsMCC.Tables(0).Rows(0)("Mc") & "")) - 3)


            CR.Load(strpath)
            ReportLogOnToServer(CR)
            CR.Refresh()
            frmRep.ReportViewer.ReportSource = CR
            strselectformula = "{tblInventory.InventoryCode}=" & fncFormatSQLText(Trim(txtInventoryCode.Text))

            CR.DataDefinition.FormulaFields("InventoryCode").Text = fncFormatSQLText(txtInventoryCode.Text.Trim)
            'CR.DataDefinition.FormulaFields("Weight").Text = fncFormatSQLText(Format(Val(txtWeight.Text.Trim), "###0.00") + "gm")
            ''CR.DataDefinition.FormulaFields("GLOSS").Text = fncFormatSQLText(Format(GlOSS, "###0.00") + "gm")
            '' CR.DataDefinition.FormulaFields("TotWeight").Text = fncFormatSQLText(Format(Val(txtWeight.Text) + Val(GlOSS), "###0.00") + "gm")
            'CR.DataDefinition.FormulaFields("sTemp").Text = fncFormatSQLText(sTemp)
            'CR.DataDefinition.FormulaFields("MC").Text = fncFormatSQLText(Trim("MC:") + "$" + Trim(txtSMC.Text.Trim))
            'CR.DataDefinition.FormulaFields("GOLDEN VILLA").Text = fncFormatSQLText("MADURAS JEWELLERY")
            CR.DataDefinition.FormulaFields("InventoryCode").Text = fncFormatSQLText(txtInventoryCode.Text.Trim)
            CR.DataDefinition.FormulaFields("MC").Text = fncFormatSQLText(Trim(sChar))

            frmRep.ReportViewer.SelectionFormula = strselectformula
            CR.DataDefinition.RecordSelectionFormula = strselectformula
            'frmRep.ShowDialog()
            'frmRep.ReportViewer.Visible = True
            'frmRep.ReportViewer.DisplayToolbar = True
            'frmRep.ReportViewer.CloseView(False)
            'frmRep.WindowState = FormWindowState.Maximized

            CR.PrintToPrinter(1, True, 0, 0)
            Me.Cursor.Current = System.Windows.Forms.Cursors.Default
            CR.Close()
            CR.Dispose()
            CR = Nothing


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subPrintBarCode_Abiramee()
        Dim pd As New PrintDocument
        AddHandler pd.PrintPage, AddressOf subPrintBarCode_Abiramee
        pd.PrinterSettings.PrinterName = ""
        If pd.PrinterSettings.IsValid Then
            pd.Print()
        Else
            MsgBox("Printer Error. Press OK to continue Or Press Cancel to terminate the application", MsgBoxStyle.Critical)
        End If

        Exit Sub
ErrTrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub btnCatagorySearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCatagorySearch.Click
        Dim frmHelpCategory As New frmHelpSearch(0, 0, "tblCategory", "L", 4, "CategoryCode", "", "CategoryCode", "CategoryCode", 80, "CategoryName", "CategoryName", 180, "ClassCode", "ClassCode", 60, "Remarks", "Remarks", 80)
        frmHelpCategory.ShowDialog()
        txtCategoryCode.Text = frmHelpCategory.Var1.Trim
    End Sub
End Class
