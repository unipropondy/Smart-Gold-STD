Public Class frmOrderEntry
    Inherits System.Windows.Forms.Form
    Dim objCustomer As New clsCustomer
    Dim objClsSupplier As New clsSuppliers
    Dim objClsClass As New ClsClass
    Dim objClsCategory As New ClsCategory
    Dim objClscatalogue As New ClsCatalogue
    Dim objClsOrder As New clsOrder

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

    'NOTE: The followi ng procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents btnCustomerCodeSearch As System.Windows.Forms.Button
    Friend WithEvents txtCustomerCode As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
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
    Friend WithEvents txtItemDescription As System.Windows.Forms.TextBox
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
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
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
    Friend WithEvents txtEstiCost As System.Windows.Forms.TextBox
    Friend WithEvents txtEstiWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtActualTotalCost As System.Windows.Forms.TextBox
    Friend WithEvents txtWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtCostPerGram As System.Windows.Forms.TextBox
    Friend WithEvents txtBalanceAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtInvCode As System.Windows.Forms.TextBox
    Friend WithEvents txtBalAmt As System.Windows.Forms.TextBox
    Friend WithEvents cboCatalogue As System.Windows.Forms.ComboBox
    Friend WithEvents cboCategory As System.Windows.Forms.ComboBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents txtActualCostPerGram As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmOrderEntry))
        Me.Panel1 = New System.Windows.Forms.Panel
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
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.txtItemDescription = New System.Windows.Forms.TextBox
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
        Me.Label9 = New System.Windows.Forms.Label
        Me.btnOrderNoSearch = New System.Windows.Forms.Button
        Me.txtOrderNo = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
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
        Me.txtInvCode = New System.Windows.Forms.TextBox
        Me.Label37 = New System.Windows.Forms.Label
        Me.txtBalAmt = New System.Windows.Forms.TextBox
        Me.Label38 = New System.Windows.Forms.Label
        Me.txtActualTotalCost = New System.Windows.Forms.TextBox
        Me.Label39 = New System.Windows.Forms.Label
        Me.Label40 = New System.Windows.Forms.Label
        Me.Label41 = New System.Windows.Forms.Label
        Me.txtDeliYY = New System.Windows.Forms.TextBox
        Me.txtDeliMM = New System.Windows.Forms.TextBox
        Me.txtDeliDD = New System.Windows.Forms.TextBox
        Me.Label42 = New System.Windows.Forms.Label
        Me.txtActualCostPerGram = New System.Windows.Forms.TextBox
        Me.Label43 = New System.Windows.Forms.Label
        Me.txtWeight = New System.Windows.Forms.TextBox
        Me.Label44 = New System.Windows.Forms.Label
        Me.txtBalanceAmount = New System.Windows.Forms.TextBox
        Me.Label36 = New System.Windows.Forms.Label
        Me.txtAdvancePaid = New System.Windows.Forms.TextBox
        Me.Label35 = New System.Windows.Forms.Label
        Me.txtEstiCost = New System.Windows.Forms.TextBox
        Me.Label34 = New System.Windows.Forms.Label
        Me.Label31 = New System.Windows.Forms.Label
        Me.Label32 = New System.Windows.Forms.Label
        Me.txtEstiYY = New System.Windows.Forms.TextBox
        Me.txtEstiMM = New System.Windows.Forms.TextBox
        Me.txtEstiDD = New System.Windows.Forms.TextBox
        Me.Label33 = New System.Windows.Forms.Label
        Me.txtCostPerGram = New System.Windows.Forms.TextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.txtEstiWeight = New System.Windows.Forms.TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Label45 = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
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
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.txtItemDescription)
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
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.btnOrderNoSearch)
        Me.Panel1.Controls.Add(Me.txtOrderNo)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Location = New System.Drawing.Point(20, 80)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(468, 392)
        Me.Panel1.TabIndex = 0
        '
        'cboCategory
        '
        Me.cboCategory.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCategory.Location = New System.Drawing.Point(328, 152)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.Size = New System.Drawing.Size(121, 24)
        Me.cboCategory.TabIndex = 6
        '
        'cboCatalogue
        '
        Me.cboCatalogue.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCatalogue.Location = New System.Drawing.Point(112, 184)
        Me.cboCatalogue.Name = "cboCatalogue"
        Me.cboCatalogue.Size = New System.Drawing.Size(121, 24)
        Me.cboCatalogue.TabIndex = 179
        '
        'btnCustomer
        '
        Me.btnCustomer.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnCustomer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomer.Image = CType(resources.GetObject("btnCustomer.Image"), System.Drawing.Image)
        Me.btnCustomer.Location = New System.Drawing.Point(240, 43)
        Me.btnCustomer.Name = "btnCustomer"
        Me.btnCustomer.Size = New System.Drawing.Size(48, 20)
        Me.btnCustomer.TabIndex = 200
        Me.btnCustomer.Text = "NEW"
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Black
        Me.Label21.Location = New System.Drawing.Point(8, 184)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(64, 15)
        Me.Label21.TabIndex = 176
        Me.Label21.Text = "Catalogue"
        '
        'txtNRICNo
        '
        Me.txtNRICNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNRICNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNRICNo.ForeColor = System.Drawing.Color.Black
        Me.txtNRICNo.Location = New System.Drawing.Point(352, 128)
        Me.txtNRICNo.MaxLength = 20
        Me.txtNRICNo.Name = "txtNRICNo"
        Me.txtNRICNo.Size = New System.Drawing.Size(96, 22)
        Me.txtNRICNo.TabIndex = 11
        Me.txtNRICNo.Tag = ""
        Me.txtNRICNo.Text = ""
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.Location = New System.Drawing.Point(280, 128)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(56, 15)
        Me.Label20.TabIndex = 174
        Me.Label20.Text = "NRIC No."
        '
        'txtSupplierName
        '
        Me.txtSupplierName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierName.ForeColor = System.Drawing.Color.Black
        Me.txtSupplierName.Location = New System.Drawing.Point(240, 280)
        Me.txtSupplierName.MaxLength = 20
        Me.txtSupplierName.Name = "txtSupplierName"
        Me.txtSupplierName.Size = New System.Drawing.Size(208, 22)
        Me.txtSupplierName.TabIndex = 28
        Me.txtSupplierName.Tag = ""
        Me.txtSupplierName.Text = ""
        '
        'btnSupplierCodeSearch
        '
        Me.btnSupplierCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnSupplierCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSupplierCodeSearch.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSupplierCodeSearch.Location = New System.Drawing.Point(208, 280)
        Me.btnSupplierCodeSearch.Name = "btnSupplierCodeSearch"
        Me.btnSupplierCodeSearch.Size = New System.Drawing.Size(24, 22)
        Me.btnSupplierCodeSearch.TabIndex = 27
        Me.btnSupplierCodeSearch.Text = "..."
        '
        'txtSupplierCode
        '
        Me.txtSupplierCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierCode.ForeColor = System.Drawing.Color.Black
        Me.txtSupplierCode.Location = New System.Drawing.Point(112, 280)
        Me.txtSupplierCode.MaxLength = 20
        Me.txtSupplierCode.Name = "txtSupplierCode"
        Me.txtSupplierCode.Size = New System.Drawing.Size(96, 22)
        Me.txtSupplierCode.TabIndex = 26
        Me.txtSupplierCode.Tag = ""
        Me.txtSupplierCode.Text = ""
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(8, 280)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(96, 15)
        Me.Label16.TabIndex = 167
        Me.Label16.Text = "Supplier"
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(8, 312)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(82, 15)
        Me.Label17.TabIndex = 164
        Me.Label17.Text = "Address"
        '
        'txtSAddress3
        '
        Me.txtSAddress3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSAddress3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSAddress3.ForeColor = System.Drawing.Color.Black
        Me.txtSAddress3.Location = New System.Drawing.Point(112, 358)
        Me.txtSAddress3.MaxLength = 20
        Me.txtSAddress3.Name = "txtSAddress3"
        Me.txtSAddress3.Size = New System.Drawing.Size(336, 22)
        Me.txtSAddress3.TabIndex = 31
        Me.txtSAddress3.Tag = ""
        Me.txtSAddress3.Text = ""
        '
        'txtSAddress2
        '
        Me.txtSAddress2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSAddress2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSAddress2.ForeColor = System.Drawing.Color.Black
        Me.txtSAddress2.Location = New System.Drawing.Point(112, 332)
        Me.txtSAddress2.MaxLength = 20
        Me.txtSAddress2.Name = "txtSAddress2"
        Me.txtSAddress2.Size = New System.Drawing.Size(336, 22)
        Me.txtSAddress2.TabIndex = 30
        Me.txtSAddress2.Tag = ""
        Me.txtSAddress2.Text = ""
        '
        'txtSAddress1
        '
        Me.txtSAddress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSAddress1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSAddress1.ForeColor = System.Drawing.Color.Black
        Me.txtSAddress1.Location = New System.Drawing.Point(112, 306)
        Me.txtSAddress1.MaxLength = 20
        Me.txtSAddress1.Name = "txtSAddress1"
        Me.txtSAddress1.Size = New System.Drawing.Size(336, 22)
        Me.txtSAddress1.TabIndex = 29
        Me.txtSAddress1.Tag = ""
        Me.txtSAddress1.Text = ""
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.optDeliveryDayRate)
        Me.GroupBox1.Controls.Add(Me.optTodayRate)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(232, 177)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(216, 32)
        Me.GroupBox1.TabIndex = 154
        Me.GroupBox1.TabStop = False
        '
        'optDeliveryDayRate
        '
        Me.optDeliveryDayRate.Checked = True
        Me.optDeliveryDayRate.Location = New System.Drawing.Point(96, 11)
        Me.optDeliveryDayRate.Name = "optDeliveryDayRate"
        Me.optDeliveryDayRate.Size = New System.Drawing.Size(118, 15)
        Me.optDeliveryDayRate.TabIndex = 1
        Me.optDeliveryDayRate.TabStop = True
        Me.optDeliveryDayRate.Text = "Delivery Day Rate"
        '
        'optTodayRate
        '
        Me.optTodayRate.Location = New System.Drawing.Point(8, 11)
        Me.optTodayRate.Name = "optTodayRate"
        Me.optTodayRate.Size = New System.Drawing.Size(83, 15)
        Me.optTodayRate.TabIndex = 25
        Me.optTodayRate.Text = "Today Rate"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(8, 216)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 15)
        Me.Label5.TabIndex = 147
        Me.Label5.Text = "Item Description"
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Location = New System.Drawing.Point(0, 265)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(466, 2)
        Me.Panel2.TabIndex = 146
        '
        'txtItemDescription
        '
        Me.txtItemDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemDescription.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemDescription.ForeColor = System.Drawing.Color.Black
        Me.txtItemDescription.Location = New System.Drawing.Point(112, 216)
        Me.txtItemDescription.MaxLength = 20
        Me.txtItemDescription.Multiline = True
        Me.txtItemDescription.Name = "txtItemDescription"
        Me.txtItemDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtItemDescription.Size = New System.Drawing.Size(336, 40)
        Me.txtItemDescription.TabIndex = 16
        Me.txtItemDescription.Tag = ""
        Me.txtItemDescription.Text = ""
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(240, 160)
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
        Me.btnClassSearch.Location = New System.Drawing.Point(208, 156)
        Me.btnClassSearch.Name = "btnClassSearch"
        Me.btnClassSearch.Size = New System.Drawing.Size(24, 22)
        Me.btnClassSearch.TabIndex = 13
        Me.btnClassSearch.Text = "..."
        '
        'txtClass
        '
        Me.txtClass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtClass.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClass.ForeColor = System.Drawing.Color.Black
        Me.txtClass.Location = New System.Drawing.Point(112, 156)
        Me.txtClass.MaxLength = 20
        Me.txtClass.Name = "txtClass"
        Me.txtClass.Size = New System.Drawing.Size(96, 22)
        Me.txtClass.TabIndex = 5
        Me.txtClass.Tag = ""
        Me.txtClass.Text = ""
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(9, 160)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 15)
        Me.Label3.TabIndex = 140
        Me.Label3.Text = "Class"
        '
        'txtCustomerName
        '
        Me.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerName.ForeColor = System.Drawing.Color.Black
        Me.txtCustomerName.Location = New System.Drawing.Point(296, 44)
        Me.txtCustomerName.MaxLength = 20
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(152, 22)
        Me.txtCustomerName.TabIndex = 7
        Me.txtCustomerName.Tag = ""
        Me.txtCustomerName.Text = ""
        '
        'btnCustomerCodeSearch
        '
        Me.btnCustomerCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnCustomerCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCustomerCodeSearch.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomerCodeSearch.Location = New System.Drawing.Point(208, 44)
        Me.btnCustomerCodeSearch.Name = "btnCustomerCodeSearch"
        Me.btnCustomerCodeSearch.Size = New System.Drawing.Size(24, 22)
        Me.btnCustomerCodeSearch.TabIndex = 6
        Me.btnCustomerCodeSearch.Text = "..."
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerCode.ForeColor = System.Drawing.Color.Black
        Me.txtCustomerCode.Location = New System.Drawing.Point(112, 44)
        Me.txtCustomerCode.MaxLength = 20
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.Size = New System.Drawing.Size(96, 22)
        Me.txtCustomerCode.TabIndex = 4
        Me.txtCustomerCode.Tag = ""
        Me.txtCustomerCode.Text = ""
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(9, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 15)
        Me.Label1.TabIndex = 137
        Me.Label1.Text = "Customer Code"
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(9, 80)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(82, 15)
        Me.Label13.TabIndex = 134
        Me.Label13.Text = "Address"
        '
        'txtCAddress3
        '
        Me.txtCAddress3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCAddress3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCAddress3.ForeColor = System.Drawing.Color.Black
        Me.txtCAddress3.Location = New System.Drawing.Point(112, 128)
        Me.txtCAddress3.MaxLength = 20
        Me.txtCAddress3.Name = "txtCAddress3"
        Me.txtCAddress3.Size = New System.Drawing.Size(160, 22)
        Me.txtCAddress3.TabIndex = 10
        Me.txtCAddress3.Tag = ""
        Me.txtCAddress3.Text = ""
        '
        'txtCAddress2
        '
        Me.txtCAddress2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCAddress2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCAddress2.ForeColor = System.Drawing.Color.Black
        Me.txtCAddress2.Location = New System.Drawing.Point(112, 100)
        Me.txtCAddress2.MaxLength = 20
        Me.txtCAddress2.Name = "txtCAddress2"
        Me.txtCAddress2.Size = New System.Drawing.Size(336, 22)
        Me.txtCAddress2.TabIndex = 9
        Me.txtCAddress2.Tag = ""
        Me.txtCAddress2.Text = ""
        '
        'txtCAddress1
        '
        Me.txtCAddress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCAddress1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCAddress1.ForeColor = System.Drawing.Color.Black
        Me.txtCAddress1.Location = New System.Drawing.Point(112, 72)
        Me.txtCAddress1.MaxLength = 20
        Me.txtCAddress1.Name = "txtCAddress1"
        Me.txtCAddress1.Size = New System.Drawing.Size(336, 22)
        Me.txtCAddress1.TabIndex = 8
        Me.txtCAddress1.Tag = ""
        Me.txtCAddress1.Text = ""
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(408, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(8, 16)
        Me.Label6.TabIndex = 130
        Me.Label6.Text = "-"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(376, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(8, 16)
        Me.Label8.TabIndex = 129
        Me.Label8.Text = "-"
        '
        'txtOYYYY
        '
        Me.txtOYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOYYYY.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOYYYY.Location = New System.Drawing.Point(416, 16)
        Me.txtOYYYY.MaxLength = 4
        Me.txtOYYYY.Name = "txtOYYYY"
        Me.txtOYYYY.Size = New System.Drawing.Size(32, 22)
        Me.txtOYYYY.TabIndex = 3
        Me.txtOYYYY.Tag = "ENN;"
        Me.txtOYYYY.Text = ""
        '
        'txtOMM
        '
        Me.txtOMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOMM.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOMM.Location = New System.Drawing.Point(384, 16)
        Me.txtOMM.MaxLength = 2
        Me.txtOMM.Name = "txtOMM"
        Me.txtOMM.Size = New System.Drawing.Size(21, 22)
        Me.txtOMM.TabIndex = 2
        Me.txtOMM.Tag = "ENN;"
        Me.txtOMM.Text = ""
        '
        'txtODD
        '
        Me.txtODD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtODD.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtODD.Location = New System.Drawing.Point(352, 16)
        Me.txtODD.MaxLength = 2
        Me.txtODD.Name = "txtODD"
        Me.txtODD.Size = New System.Drawing.Size(21, 22)
        Me.txtODD.TabIndex = 1
        Me.txtODD.Tag = "ENN;"
        Me.txtODD.Text = ""
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(264, 16)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(72, 20)
        Me.Label9.TabIndex = 128
        Me.Label9.Text = "Order Date"
        '
        'btnOrderNoSearch
        '
        Me.btnOrderNoSearch.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnOrderNoSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOrderNoSearch.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOrderNoSearch.Location = New System.Drawing.Point(208, 16)
        Me.btnOrderNoSearch.Name = "btnOrderNoSearch"
        Me.btnOrderNoSearch.Size = New System.Drawing.Size(24, 22)
        Me.btnOrderNoSearch.TabIndex = 1
        Me.btnOrderNoSearch.Text = "..."
        '
        'txtOrderNo
        '
        Me.txtOrderNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOrderNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOrderNo.ForeColor = System.Drawing.Color.Black
        Me.txtOrderNo.Location = New System.Drawing.Point(112, 16)
        Me.txtOrderNo.MaxLength = 20
        Me.txtOrderNo.Name = "txtOrderNo"
        Me.txtOrderNo.Size = New System.Drawing.Size(96, 22)
        Me.txtOrderNo.TabIndex = 0
        Me.txtOrderNo.Tag = ""
        Me.txtOrderNo.Text = ""
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(9, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 15)
        Me.Label2.TabIndex = 110
        Me.Label2.Text = "Order No"
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.Location = New System.Drawing.Point(542, 480)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(94, 23)
        Me.btnSave.TabIndex = 32
        Me.btnSave.Text = "Save (F7)"
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(643, 480)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(92, 23)
        Me.btnClose.TabIndex = 34
        Me.btnClose.Text = "Close(Esc)"
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(440, 480)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(95, 23)
        Me.btnClear.TabIndex = 33
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
        Me.Panel3.Controls.Add(Me.txtInvCode)
        Me.Panel3.Controls.Add(Me.Label37)
        Me.Panel3.Controls.Add(Me.txtBalAmt)
        Me.Panel3.Controls.Add(Me.Label38)
        Me.Panel3.Controls.Add(Me.txtActualTotalCost)
        Me.Panel3.Controls.Add(Me.Label39)
        Me.Panel3.Controls.Add(Me.Label40)
        Me.Panel3.Controls.Add(Me.Label41)
        Me.Panel3.Controls.Add(Me.txtDeliYY)
        Me.Panel3.Controls.Add(Me.txtDeliMM)
        Me.Panel3.Controls.Add(Me.txtDeliDD)
        Me.Panel3.Controls.Add(Me.Label42)
        Me.Panel3.Controls.Add(Me.txtActualCostPerGram)
        Me.Panel3.Controls.Add(Me.Label43)
        Me.Panel3.Controls.Add(Me.txtWeight)
        Me.Panel3.Controls.Add(Me.Label44)
        Me.Panel3.Controls.Add(Me.txtBalanceAmount)
        Me.Panel3.Controls.Add(Me.Label36)
        Me.Panel3.Controls.Add(Me.txtAdvancePaid)
        Me.Panel3.Controls.Add(Me.Label35)
        Me.Panel3.Controls.Add(Me.txtEstiCost)
        Me.Panel3.Controls.Add(Me.Label34)
        Me.Panel3.Controls.Add(Me.Label31)
        Me.Panel3.Controls.Add(Me.Label32)
        Me.Panel3.Controls.Add(Me.txtEstiYY)
        Me.Panel3.Controls.Add(Me.txtEstiMM)
        Me.Panel3.Controls.Add(Me.txtEstiDD)
        Me.Panel3.Controls.Add(Me.Label33)
        Me.Panel3.Controls.Add(Me.txtCostPerGram)
        Me.Panel3.Controls.Add(Me.Label30)
        Me.Panel3.Controls.Add(Me.txtEstiWeight)
        Me.Panel3.Controls.Add(Me.Label22)
        Me.Panel3.Controls.Add(Me.Panel4)
        Me.Panel3.Location = New System.Drawing.Point(488, 80)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(248, 360)
        Me.Panel3.TabIndex = 113
        '
        'txtInvCode
        '
        Me.txtInvCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvCode.ForeColor = System.Drawing.Color.Black
        Me.txtInvCode.Location = New System.Drawing.Point(135, 333)
        Me.txtInvCode.MaxLength = 20
        Me.txtInvCode.Name = "txtInvCode"
        Me.txtInvCode.Size = New System.Drawing.Size(96, 22)
        Me.txtInvCode.TabIndex = 189
        Me.txtInvCode.Tag = ""
        Me.txtInvCode.Text = "0.00"
        Me.txtInvCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label37
        '
        Me.Label37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.Color.Black
        Me.Label37.Location = New System.Drawing.Point(8, 334)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(103, 15)
        Me.Label37.TabIndex = 190
        Me.Label37.Text = "Inventory Code"
        '
        'txtBalAmt
        '
        Me.txtBalAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBalAmt.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBalAmt.ForeColor = System.Drawing.Color.Black
        Me.txtBalAmt.Location = New System.Drawing.Point(135, 306)
        Me.txtBalAmt.MaxLength = 20
        Me.txtBalAmt.Name = "txtBalAmt"
        Me.txtBalAmt.Size = New System.Drawing.Size(96, 22)
        Me.txtBalAmt.TabIndex = 187
        Me.txtBalAmt.Tag = ""
        Me.txtBalAmt.Text = "0.00"
        Me.txtBalAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label38
        '
        Me.Label38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.Color.Black
        Me.Label38.Location = New System.Drawing.Point(8, 310)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(112, 15)
        Me.Label38.TabIndex = 188
        Me.Label38.Text = "Actual Bal. Amount"
        '
        'txtActualTotalCost
        '
        Me.txtActualTotalCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtActualTotalCost.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtActualTotalCost.ForeColor = System.Drawing.Color.Black
        Me.txtActualTotalCost.Location = New System.Drawing.Point(135, 279)
        Me.txtActualTotalCost.MaxLength = 20
        Me.txtActualTotalCost.Name = "txtActualTotalCost"
        Me.txtActualTotalCost.Size = New System.Drawing.Size(96, 22)
        Me.txtActualTotalCost.TabIndex = 185
        Me.txtActualTotalCost.Tag = ""
        Me.txtActualTotalCost.Text = "0.00"
        Me.txtActualTotalCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label39
        '
        Me.Label39.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.Color.Black
        Me.Label39.Location = New System.Drawing.Point(8, 286)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(103, 15)
        Me.Label39.TabIndex = 186
        Me.Label39.Text = "Actual Cost"
        '
        'Label40
        '
        Me.Label40.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(191, 198)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(8, 16)
        Me.Label40.TabIndex = 184
        Me.Label40.Text = "-"
        '
        'Label41
        '
        Me.Label41.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.Location = New System.Drawing.Point(159, 198)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(8, 16)
        Me.Label41.TabIndex = 183
        Me.Label41.Text = "-"
        '
        'txtDeliYY
        '
        Me.txtDeliYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeliYY.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeliYY.Location = New System.Drawing.Point(199, 198)
        Me.txtDeliYY.MaxLength = 4
        Me.txtDeliYY.Name = "txtDeliYY"
        Me.txtDeliYY.Size = New System.Drawing.Size(32, 22)
        Me.txtDeliYY.TabIndex = 181
        Me.txtDeliYY.Tag = "ENN;"
        Me.txtDeliYY.Text = ""
        '
        'txtDeliMM
        '
        Me.txtDeliMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeliMM.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeliMM.Location = New System.Drawing.Point(167, 198)
        Me.txtDeliMM.MaxLength = 2
        Me.txtDeliMM.Name = "txtDeliMM"
        Me.txtDeliMM.Size = New System.Drawing.Size(21, 22)
        Me.txtDeliMM.TabIndex = 180
        Me.txtDeliMM.Tag = "ENN;"
        Me.txtDeliMM.Text = ""
        '
        'txtDeliDD
        '
        Me.txtDeliDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeliDD.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeliDD.Location = New System.Drawing.Point(135, 198)
        Me.txtDeliDD.MaxLength = 2
        Me.txtDeliDD.Name = "txtDeliDD"
        Me.txtDeliDD.Size = New System.Drawing.Size(21, 22)
        Me.txtDeliDD.TabIndex = 179
        Me.txtDeliDD.Tag = "ENN;"
        Me.txtDeliDD.Text = ""
        '
        'Label42
        '
        Me.Label42.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.Color.Black
        Me.Label42.Location = New System.Drawing.Point(8, 198)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(97, 20)
        Me.Label42.TabIndex = 182
        Me.Label42.Text = "Delivered Date"
        '
        'txtActualCostPerGram
        '
        Me.txtActualCostPerGram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtActualCostPerGram.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtActualCostPerGram.ForeColor = System.Drawing.Color.Black
        Me.txtActualCostPerGram.Location = New System.Drawing.Point(135, 252)
        Me.txtActualCostPerGram.MaxLength = 20
        Me.txtActualCostPerGram.Name = "txtActualCostPerGram"
        Me.txtActualCostPerGram.Size = New System.Drawing.Size(96, 22)
        Me.txtActualCostPerGram.TabIndex = 177
        Me.txtActualCostPerGram.Tag = ""
        Me.txtActualCostPerGram.Text = "0.00"
        Me.txtActualCostPerGram.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label43
        '
        Me.Label43.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.Color.Black
        Me.Label43.Location = New System.Drawing.Point(8, 254)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(121, 15)
        Me.Label43.TabIndex = 178
        Me.Label43.Text = "Actual Cost Per Gram"
        '
        'txtWeight
        '
        Me.txtWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWeight.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeight.ForeColor = System.Drawing.Color.Black
        Me.txtWeight.Location = New System.Drawing.Point(167, 225)
        Me.txtWeight.MaxLength = 20
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.Size = New System.Drawing.Size(64, 22)
        Me.txtWeight.TabIndex = 175
        Me.txtWeight.Tag = ""
        Me.txtWeight.Text = "0.00"
        Me.txtWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label44
        '
        Me.Label44.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.Color.Black
        Me.Label44.Location = New System.Drawing.Point(8, 230)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(103, 15)
        Me.Label44.TabIndex = 176
        Me.Label44.Text = "Actual Weight"
        '
        'txtBalanceAmount
        '
        Me.txtBalanceAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBalanceAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBalanceAmount.ForeColor = System.Drawing.Color.Black
        Me.txtBalanceAmount.Location = New System.Drawing.Point(136, 151)
        Me.txtBalanceAmount.MaxLength = 20
        Me.txtBalanceAmount.Name = "txtBalanceAmount"
        Me.txtBalanceAmount.Size = New System.Drawing.Size(96, 22)
        Me.txtBalanceAmount.TabIndex = 173
        Me.txtBalanceAmount.Tag = ""
        Me.txtBalanceAmount.Text = "0.00"
        Me.txtBalanceAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label36
        '
        Me.Label36.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.Color.Black
        Me.Label36.Location = New System.Drawing.Point(8, 152)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(103, 15)
        Me.Label36.TabIndex = 174
        Me.Label36.Text = "Balance Amount"
        '
        'txtAdvancePaid
        '
        Me.txtAdvancePaid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdvancePaid.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdvancePaid.ForeColor = System.Drawing.Color.Black
        Me.txtAdvancePaid.Location = New System.Drawing.Point(136, 124)
        Me.txtAdvancePaid.MaxLength = 20
        Me.txtAdvancePaid.Name = "txtAdvancePaid"
        Me.txtAdvancePaid.Size = New System.Drawing.Size(96, 22)
        Me.txtAdvancePaid.TabIndex = 171
        Me.txtAdvancePaid.Tag = ""
        Me.txtAdvancePaid.Text = "0.00"
        Me.txtAdvancePaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label35
        '
        Me.Label35.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.Color.Black
        Me.Label35.Location = New System.Drawing.Point(8, 128)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(80, 15)
        Me.Label35.TabIndex = 172
        Me.Label35.Text = "Amount Paid"
        '
        'txtEstiCost
        '
        Me.txtEstiCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEstiCost.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEstiCost.ForeColor = System.Drawing.Color.Black
        Me.txtEstiCost.Location = New System.Drawing.Point(136, 97)
        Me.txtEstiCost.MaxLength = 20
        Me.txtEstiCost.Name = "txtEstiCost"
        Me.txtEstiCost.ReadOnly = True
        Me.txtEstiCost.Size = New System.Drawing.Size(96, 22)
        Me.txtEstiCost.TabIndex = 167
        Me.txtEstiCost.Tag = ""
        Me.txtEstiCost.Text = "0.00"
        Me.txtEstiCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label34
        '
        Me.Label34.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.Color.Black
        Me.Label34.Location = New System.Drawing.Point(8, 104)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(103, 15)
        Me.Label34.TabIndex = 168
        Me.Label34.Text = "Estimated Cost"
        '
        'Label31
        '
        Me.Label31.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(192, 16)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(8, 16)
        Me.Label31.TabIndex = 166
        Me.Label31.Text = "-"
        '
        'Label32
        '
        Me.Label32.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(160, 16)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(8, 16)
        Me.Label32.TabIndex = 165
        Me.Label32.Text = "-"
        '
        'txtEstiYY
        '
        Me.txtEstiYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEstiYY.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEstiYY.Location = New System.Drawing.Point(200, 16)
        Me.txtEstiYY.MaxLength = 4
        Me.txtEstiYY.Name = "txtEstiYY"
        Me.txtEstiYY.Size = New System.Drawing.Size(32, 22)
        Me.txtEstiYY.TabIndex = 163
        Me.txtEstiYY.Tag = "ENN;"
        Me.txtEstiYY.Text = ""
        '
        'txtEstiMM
        '
        Me.txtEstiMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEstiMM.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEstiMM.Location = New System.Drawing.Point(168, 16)
        Me.txtEstiMM.MaxLength = 2
        Me.txtEstiMM.Name = "txtEstiMM"
        Me.txtEstiMM.Size = New System.Drawing.Size(21, 22)
        Me.txtEstiMM.TabIndex = 162
        Me.txtEstiMM.Tag = "ENN;"
        Me.txtEstiMM.Text = ""
        '
        'txtEstiDD
        '
        Me.txtEstiDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEstiDD.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEstiDD.Location = New System.Drawing.Point(136, 16)
        Me.txtEstiDD.MaxLength = 2
        Me.txtEstiDD.Name = "txtEstiDD"
        Me.txtEstiDD.Size = New System.Drawing.Size(21, 22)
        Me.txtEstiDD.TabIndex = 161
        Me.txtEstiDD.Tag = "ENN;"
        Me.txtEstiDD.Text = ""
        '
        'Label33
        '
        Me.Label33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.Black
        Me.Label33.Location = New System.Drawing.Point(8, 19)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(120, 16)
        Me.Label33.TabIndex = 164
        Me.Label33.Text = "Date of Delivery"
        '
        'txtCostPerGram
        '
        Me.txtCostPerGram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCostPerGram.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostPerGram.ForeColor = System.Drawing.Color.Black
        Me.txtCostPerGram.Location = New System.Drawing.Point(136, 70)
        Me.txtCostPerGram.MaxLength = 20
        Me.txtCostPerGram.Name = "txtCostPerGram"
        Me.txtCostPerGram.Size = New System.Drawing.Size(96, 22)
        Me.txtCostPerGram.TabIndex = 152
        Me.txtCostPerGram.Tag = ""
        Me.txtCostPerGram.Text = "0.00"
        Me.txtCostPerGram.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label30
        '
        Me.Label30.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.Color.Black
        Me.Label30.Location = New System.Drawing.Point(8, 72)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(104, 15)
        Me.Label30.TabIndex = 153
        Me.Label30.Text = "Cost per Gram"
        '
        'txtEstiWeight
        '
        Me.txtEstiWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEstiWeight.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEstiWeight.ForeColor = System.Drawing.Color.Black
        Me.txtEstiWeight.Location = New System.Drawing.Point(168, 43)
        Me.txtEstiWeight.MaxLength = 20
        Me.txtEstiWeight.Name = "txtEstiWeight"
        Me.txtEstiWeight.Size = New System.Drawing.Size(64, 22)
        Me.txtEstiWeight.TabIndex = 150
        Me.txtEstiWeight.Tag = ""
        Me.txtEstiWeight.Text = "0.00"
        Me.txtEstiWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label22
        '
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.Black
        Me.Label22.Location = New System.Drawing.Point(8, 48)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(103, 15)
        Me.Label22.TabIndex = 151
        Me.Label22.Text = "Estimated Weight"
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Location = New System.Drawing.Point(0, 184)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(248, 2)
        Me.Panel4.TabIndex = 147
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Label45.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label45.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.Color.Red
        Me.Label45.Location = New System.Drawing.Point(488, 440)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(248, 32)
        Me.Label45.TabIndex = 191
        Me.Label45.Text = "Order Getting"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmOrderEntry
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(754, 512)
        Me.Controls.Add(Me.Label45)
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
        Me.Name = "frmOrderEntry"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Order Entry"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmOrderEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        getCurrentDate()
    End Sub
    Private Sub btnCustomerCodeSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomerCodeSearch.Click
        Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCustomer", "L", 4, "CustomerCode", "", "CustomerCode", "CustomerCode", 80, "CustomerName", "CustomerName", 150, "Phone2 ", "Phone2", 50, "Phone1", "Phone1", 50)
        frmHelpClass.ShowDialog()
        txtCustomerCode.Text = frmHelpClass.Var1.Trim
        LoadCustomerDetails(txtCustomerCode.Text.Trim)

    End Sub

    Private Sub btnClassSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClassSearch.Click
        Dim frmHelpClass As New frmHelpSearch(0, 0, "tblClass", "L", 4, "ClassCode", "", "ClassCode", "ClassCode", 80, "ClassName", "ClassName", 150, "SalesType ", "SalesType", 50, "PurchaseType", "PurchaseType", 50)
        frmHelpClass.ShowDialog()
        txtClass.Text = frmHelpClass.Var1.Trim
        txtClass_LostFocus(sender, e)
    End Sub
    Private Sub btnSupplierCodeSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSupplierCodeSearch.Click
        Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSupplier", "L", 4, "SupplierCode", "", "SupplierCode", "SupplierCode", 80, "SupplierName", "SupplierName", 150, "Phone1 ", "Phone1", 50, "Phone2", "Phone2", 50)
        frmHelpClass.ShowDialog()
        txtSupplierCode.Text = frmHelpClass.Var1.Trim
        DispSupplierDetails(txtSupplierCode.Text.Trim)
    End Sub
    Private Sub getCurrentDate()
        txtODD.Text = Format(ServerDate, "dd")
        txtOMM.Text = Format(ServerDate, "MM")
        txtOYYYY.Text = Format(ServerDate, "yyyy")
    End Sub

    Private Sub btnOrderNoSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOrderNoSearch.Click
        Dim frmHelpClass As New frmHelpSearch(0, 0, "tblInventoryOrder", "L", 4, "OrderNo", "", "OrderNo", "OrderNo", 80, "CustomerCode", "CustomerCode", 150, "SupplierCode ", "SupplierCode", 50, "OrderDate", "OrderDate", 50)
        frmHelpClass.ShowDialog()
        txtOrderNo.Text = frmHelpClass.Var1.Trim
        DispOrderDetails(txtOrderNo.Text.Trim)
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
                    txtNRICNo.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("NricNo") & "")
                    txtCustomerName.ReadOnly = True
                    txtCAddress1.ReadOnly = True
                    txtCAddress2.ReadOnly = True
                    txtCAddress3.ReadOnly = True
                    txtNRICNo.ReadOnly = True
                    txtClass.Focus()
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
        frmCustomerO.LoadCustomerDetails(frmCustomerO.txtCustomercode.Text)
        frmCustomerO.btnDelete.Enabled = False
        frmCustomerO.ShowDialog()
    End Sub

    Private Sub txtSupplierCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSupplierCode.LostFocus
        If txtSupplierCode.Text.Trim.Length <> 0 Then
            DispSupplierDetails(txtSupplierCode.Text.Trim)
        End If
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
                    txtEstiDD.Focus()
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
                    For icnt = 0 To ds.Tables(0).Rows.Count - 1
                        cboCategory.Items.Add(Trim(ds.Tables(0).Rows(icnt)("categorycode") & ""))
                    Next
                    ds = objClsOrder.GetClass(txtClass.Text.Trim)
                    If ds.Tables(0).Rows.Count > 0 Then
                        txtCostPerGram.Text = Format(Val(ds.Tables(0).Rows(0)("Rate") & ""), "0.00")
                        txtCostPerGram.ReadOnly = True
                    End If
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
        Try
            If sOrderCode.Length > 0 Then
                'ds = objClsOrder.GetOrderDetail(sOrderCode)
                If ds.Tables(0).Rows.Count > 0 Then

                Else
                    MsgBox("No such Order Number ! ", MsgBoxStyle.Exclamation)
                    txtOrderNo.Clear()
                    txtOrderNo.Focus()
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

    Private Sub frmOrderEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click

    End Sub

    Private Sub cboCategory_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCategory.LostFocus
        Dim ds As DataSet
        Dim icnt As Integer
        Try
            If cboCategory.Text.Trim.Length > 0 Then
                ds = objClsOrder.GetCatelogue(cboCategory.Text.Trim)
                If ds.Tables(0).Rows.Count > 0 Then
                    cboCatalogue.Text = Trim(ds.Tables(0).Rows(0)("cataloguecode") & "")
                    For icnt = 0 To ds.Tables(0).Rows.Count - 1
                        cboCatalogue.Items.Add(Trim(ds.Tables(0).Rows(icnt)("cataloguecode") & ""))
                    Next
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
    End Sub

    Private Sub subTotalCost()
        txtEstiCost.Text = Val(txtEstiWeight.Text) * Val(txtCostPerGram.Text)
    End Sub

    Private Sub txtAdvancePaid_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAdvancePaid.TextChanged
        txtBalanceAmount.Text = Val(txtEstiCost.Text) - Val(txtAdvancePaid.Text)
    End Sub

End Class
