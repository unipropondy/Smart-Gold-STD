<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSchemeTransactionDetail
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSchemeTransactionDetail))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtSchemeValue = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnSchemeCode = New System.Windows.Forms.Button()
        Me.txtSchemeCode = New System.Windows.Forms.TextBox()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.optGram = New System.Windows.Forms.RadioButton()
        Me.optValue = New System.Windows.Forms.RadioButton()
        Me.opteTRSEligible = New System.Windows.Forms.RadioButton()
        Me.Label82 = New System.Windows.Forms.Label()
        Me.btnNewCustomer = New System.Windows.Forms.Button()
        Me.lblCustomerType = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtSalesManName = New System.Windows.Forms.TextBox()
        Me.txtSalesManPassword = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnCustomerCodeSearch = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtAddress3 = New System.Windows.Forms.TextBox()
        Me.txtAddress2 = New System.Windows.Forms.TextBox()
        Me.txtAddress1 = New System.Windows.Forms.TextBox()
        Me.txtCustomerCode = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtIYYYY = New System.Windows.Forms.TextBox()
        Me.txtIMM = New System.Windows.Forms.TextBox()
        Me.txtIDD = New System.Windows.Forms.TextBox()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.grdInstallment = New System.Windows.Forms.DataGridView()
        Me.cInstallmentNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cInstallmentDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cNoofInstallment = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cInstallmentAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cAmountPaid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnRePrint = New System.Windows.Forms.Button()
        Me.btnSAve = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnBillClear = New System.Windows.Forms.Button()
        Me.btnSchemeNoSearch = New System.Windows.Forms.Button()
        Me.txtSchemeNo = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel8.SuspendLayout()
        CType(Me.grdInstallment, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(270, 100)
        Me.PictureBox1.TabIndex = 56
        Me.PictureBox1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.btnSchemeNoSearch)
        Me.Panel1.Controls.Add(Me.txtSchemeNo)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.txtSchemeValue)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.btnSchemeCode)
        Me.Panel1.Controls.Add(Me.txtSchemeCode)
        Me.Panel1.Controls.Add(Me.Panel8)
        Me.Panel1.Controls.Add(Me.Label82)
        Me.Panel1.Controls.Add(Me.btnNewCustomer)
        Me.Panel1.Controls.Add(Me.lblCustomerType)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtSalesManName)
        Me.Panel1.Controls.Add(Me.txtSalesManPassword)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.btnCustomerCodeSearch)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.txtAddress3)
        Me.Panel1.Controls.Add(Me.txtAddress2)
        Me.Panel1.Controls.Add(Me.txtAddress1)
        Me.Panel1.Controls.Add(Me.txtCustomerCode)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txtIYYYY)
        Me.Panel1.Controls.Add(Me.txtIMM)
        Me.Panel1.Controls.Add(Me.txtIDD)
        Me.Panel1.Controls.Add(Me.txtCustomerName)
        Me.Panel1.Location = New System.Drawing.Point(6, 125)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(662, 216)
        Me.Panel1.TabIndex = 57
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(230, 180)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(93, 20)
        Me.Label7.TabIndex = 524
        Me.Label7.Text = "Scheme Value"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSchemeValue
        '
        Me.txtSchemeValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSchemeValue.Enabled = False
        Me.txtSchemeValue.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSchemeValue.ForeColor = System.Drawing.Color.Black
        Me.txtSchemeValue.Location = New System.Drawing.Point(327, 181)
        Me.txtSchemeValue.MaxLength = 20
        Me.txtSchemeValue.Name = "txtSchemeValue"
        Me.txtSchemeValue.Size = New System.Drawing.Size(96, 20)
        Me.txtSchemeValue.TabIndex = 523
        Me.txtSchemeValue.Tag = ""
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(10, 180)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(85, 20)
        Me.Label5.TabIndex = 522
        Me.Label5.Text = "Scheme Code"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnSchemeCode
        '
        Me.btnSchemeCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSchemeCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSchemeCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSchemeCode.Location = New System.Drawing.Point(200, 180)
        Me.btnSchemeCode.Name = "btnSchemeCode"
        Me.btnSchemeCode.Size = New System.Drawing.Size(24, 20)
        Me.btnSchemeCode.TabIndex = 521
        Me.btnSchemeCode.Text = "..."
        Me.btnSchemeCode.UseVisualStyleBackColor = False
        '
        'txtSchemeCode
        '
        Me.txtSchemeCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSchemeCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSchemeCode.ForeColor = System.Drawing.Color.Black
        Me.txtSchemeCode.Location = New System.Drawing.Point(104, 180)
        Me.txtSchemeCode.MaxLength = 20
        Me.txtSchemeCode.Name = "txtSchemeCode"
        Me.txtSchemeCode.Size = New System.Drawing.Size(96, 20)
        Me.txtSchemeCode.TabIndex = 520
        Me.txtSchemeCode.Tag = ""
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel8.Controls.Add(Me.optGram)
        Me.Panel8.Controls.Add(Me.optValue)
        Me.Panel8.Controls.Add(Me.opteTRSEligible)
        Me.Panel8.Location = New System.Drawing.Point(441, 174)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(207, 31)
        Me.Panel8.TabIndex = 519
        '
        'optGram
        '
        Me.optGram.Checked = True
        Me.optGram.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optGram.ForeColor = System.Drawing.Color.Black
        Me.optGram.Location = New System.Drawing.Point(127, 0)
        Me.optGram.Name = "optGram"
        Me.optGram.Size = New System.Drawing.Size(117, 32)
        Me.optGram.TabIndex = 13
        Me.optGram.TabStop = True
        Me.optGram.Text = "By Gram"
        '
        'optValue
        '
        Me.optValue.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optValue.ForeColor = System.Drawing.Color.Black
        Me.optValue.Location = New System.Drawing.Point(21, 0)
        Me.optValue.Name = "optValue"
        Me.optValue.Size = New System.Drawing.Size(84, 34)
        Me.optValue.TabIndex = 5
        Me.optValue.Text = "By Value"
        '
        'opteTRSEligible
        '
        Me.opteTRSEligible.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.opteTRSEligible.ForeColor = System.Drawing.Color.Black
        Me.opteTRSEligible.Location = New System.Drawing.Point(283, 0)
        Me.opteTRSEligible.Name = "opteTRSEligible"
        Me.opteTRSEligible.Size = New System.Drawing.Size(103, 20)
        Me.opteTRSEligible.TabIndex = 3
        Me.opteTRSEligible.Text = "eTRS Eligible"
        Me.opteTRSEligible.Visible = False
        '
        'Label82
        '
        Me.Label82.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label82.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label82.ForeColor = System.Drawing.Color.Black
        Me.Label82.Location = New System.Drawing.Point(8, 78)
        Me.Label82.Name = "Label82"
        Me.Label82.Size = New System.Drawing.Size(80, 20)
        Me.Label82.TabIndex = 118
        Me.Label82.Text = "Name"
        Me.Label82.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnNewCustomer
        '
        Me.btnNewCustomer.BackColor = System.Drawing.Color.DarkOrange
        Me.btnNewCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNewCustomer.Location = New System.Drawing.Point(8, 131)
        Me.btnNewCustomer.Name = "btnNewCustomer"
        Me.btnNewCustomer.Size = New System.Drawing.Size(64, 24)
        Me.btnNewCustomer.TabIndex = 116
        Me.btnNewCustomer.Text = "New"
        Me.btnNewCustomer.UseVisualStyleBackColor = False
        '
        'lblCustomerType
        '
        Me.lblCustomerType.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblCustomerType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustomerType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerType.ForeColor = System.Drawing.Color.Red
        Me.lblCustomerType.Location = New System.Drawing.Point(446, 32)
        Me.lblCustomerType.Name = "lblCustomerType"
        Me.lblCustomerType.Size = New System.Drawing.Size(120, 17)
        Me.lblCustomerType.TabIndex = 115
        Me.lblCustomerType.Text = "Customer type"
        Me.lblCustomerType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(8, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 20)
        Me.Label2.TabIndex = 64
        Me.Label2.Text = "Invoice Date"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSalesManName
        '
        Me.txtSalesManName.BackColor = System.Drawing.Color.White
        Me.txtSalesManName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesManName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesManName.ForeColor = System.Drawing.Color.Black
        Me.txtSalesManName.Location = New System.Drawing.Point(232, 6)
        Me.txtSalesManName.MaxLength = 20
        Me.txtSalesManName.Name = "txtSalesManName"
        Me.txtSalesManName.ReadOnly = True
        Me.txtSalesManName.Size = New System.Drawing.Size(296, 20)
        Me.txtSalesManName.TabIndex = 1
        Me.txtSalesManName.TabStop = False
        Me.txtSalesManName.Tag = ""
        '
        'txtSalesManPassword
        '
        Me.txtSalesManPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesManPassword.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesManPassword.ForeColor = System.Drawing.Color.Black
        Me.txtSalesManPassword.Location = New System.Drawing.Point(104, 5)
        Me.txtSalesManPassword.MaxLength = 20
        Me.txtSalesManPassword.Name = "txtSalesManPassword"
        Me.txtSalesManPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtSalesManPassword.Size = New System.Drawing.Size(120, 20)
        Me.txtSalesManPassword.TabIndex = 0
        Me.txtSalesManPassword.Tag = ""
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(8, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 20)
        Me.Label1.TabIndex = 112
        Me.Label1.Text = "Sales Man"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnCustomerCodeSearch
        '
        Me.btnCustomerCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCustomerCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCustomerCodeSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomerCodeSearch.Location = New System.Drawing.Point(200, 58)
        Me.btnCustomerCodeSearch.Name = "btnCustomerCodeSearch"
        Me.btnCustomerCodeSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnCustomerCodeSearch.TabIndex = 118
        Me.btnCustomerCodeSearch.Text = "..."
        Me.btnCustomerCodeSearch.UseVisualStyleBackColor = False
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(8, 98)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(82, 20)
        Me.Label13.TabIndex = 109
        Me.Label13.Text = "Address"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtAddress3
        '
        Me.txtAddress3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress3.ForeColor = System.Drawing.Color.Black
        Me.txtAddress3.Location = New System.Drawing.Point(336, 131)
        Me.txtAddress3.MaxLength = 20
        Me.txtAddress3.Name = "txtAddress3"
        Me.txtAddress3.Size = New System.Drawing.Size(192, 20)
        Me.txtAddress3.TabIndex = 106
        Me.txtAddress3.Tag = ""
        '
        'txtAddress2
        '
        Me.txtAddress2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress2.ForeColor = System.Drawing.Color.Black
        Me.txtAddress2.Location = New System.Drawing.Point(104, 130)
        Me.txtAddress2.MaxLength = 20
        Me.txtAddress2.Name = "txtAddress2"
        Me.txtAddress2.Size = New System.Drawing.Size(224, 20)
        Me.txtAddress2.TabIndex = 105
        Me.txtAddress2.Tag = ""
        '
        'txtAddress1
        '
        Me.txtAddress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress1.ForeColor = System.Drawing.Color.Black
        Me.txtAddress1.Location = New System.Drawing.Point(104, 106)
        Me.txtAddress1.MaxLength = 20
        Me.txtAddress1.Name = "txtAddress1"
        Me.txtAddress1.Size = New System.Drawing.Size(291, 20)
        Me.txtAddress1.TabIndex = 104
        Me.txtAddress1.Tag = ""
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerCode.ForeColor = System.Drawing.Color.Black
        Me.txtCustomerCode.Location = New System.Drawing.Point(104, 58)
        Me.txtCustomerCode.MaxLength = 20
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.Size = New System.Drawing.Size(96, 20)
        Me.txtCustomerCode.TabIndex = 6
        Me.txtCustomerCode.Tag = ""
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(8, 58)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 20)
        Me.Label6.TabIndex = 102
        Me.Label6.Text = "Customer "
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(158, 33)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(8, 16)
        Me.Label3.TabIndex = 62
        Me.Label3.Text = "/"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(126, 33)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(8, 16)
        Me.Label4.TabIndex = 61
        Me.Label4.Text = "/"
        '
        'txtIYYYY
        '
        Me.txtIYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIYYYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIYYYY.Location = New System.Drawing.Point(168, 32)
        Me.txtIYYYY.MaxLength = 4
        Me.txtIYYYY.Name = "txtIYYYY"
        Me.txtIYYYY.ReadOnly = True
        Me.txtIYYYY.Size = New System.Drawing.Size(32, 20)
        Me.txtIYYYY.TabIndex = 4
        Me.txtIYYYY.TabStop = False
        Me.txtIYYYY.Tag = "ENN;"
        '
        'txtIMM
        '
        Me.txtIMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIMM.Location = New System.Drawing.Point(136, 32)
        Me.txtIMM.MaxLength = 2
        Me.txtIMM.Name = "txtIMM"
        Me.txtIMM.ReadOnly = True
        Me.txtIMM.Size = New System.Drawing.Size(20, 20)
        Me.txtIMM.TabIndex = 3
        Me.txtIMM.TabStop = False
        Me.txtIMM.Tag = "ENN;"
        '
        'txtIDD
        '
        Me.txtIDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIDD.Location = New System.Drawing.Point(104, 32)
        Me.txtIDD.MaxLength = 2
        Me.txtIDD.Name = "txtIDD"
        Me.txtIDD.ReadOnly = True
        Me.txtIDD.Size = New System.Drawing.Size(20, 20)
        Me.txtIDD.TabIndex = 2
        Me.txtIDD.TabStop = False
        Me.txtIDD.Tag = "ENN;"
        '
        'txtCustomerName
        '
        Me.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerName.ForeColor = System.Drawing.Color.Black
        Me.txtCustomerName.Location = New System.Drawing.Point(104, 82)
        Me.txtCustomerName.MaxLength = 20
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(288, 20)
        Me.txtCustomerName.TabIndex = 8
        Me.txtCustomerName.Tag = ""
        '
        'grdInstallment
        '
        Me.grdInstallment.AllowUserToAddRows = False
        Me.grdInstallment.AllowUserToDeleteRows = False
        Me.grdInstallment.AllowUserToResizeColumns = False
        Me.grdInstallment.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdInstallment.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdInstallment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdInstallment.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cInstallmentNo, Me.cInstallmentDate, Me.cNoofInstallment, Me.cInstallmentAmount, Me.cAmountPaid})
        Me.grdInstallment.Location = New System.Drawing.Point(8, 349)
        Me.grdInstallment.Name = "grdInstallment"
        Me.grdInstallment.RowHeadersVisible = False
        Me.grdInstallment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdInstallment.Size = New System.Drawing.Size(659, 242)
        Me.grdInstallment.TabIndex = 154
        '
        'cInstallmentNo
        '
        Me.cInstallmentNo.HeaderText = "Invoice No"
        Me.cInstallmentNo.Name = "cInstallmentNo"
        Me.cInstallmentNo.Width = 120
        '
        'cInstallmentDate
        '
        Me.cInstallmentDate.HeaderText = "Invoice Date"
        Me.cInstallmentDate.Name = "cInstallmentDate"
        Me.cInstallmentDate.Width = 130
        '
        'cNoofInstallment
        '
        Me.cNoofInstallment.HeaderText = "Scheme Amount"
        Me.cNoofInstallment.Name = "cNoofInstallment"
        Me.cNoofInstallment.Width = 125
        '
        'cInstallmentAmount
        '
        Me.cInstallmentAmount.HeaderText = "Daily Rate"
        Me.cInstallmentAmount.Name = "cInstallmentAmount"
        Me.cInstallmentAmount.Width = 135
        '
        'cAmountPaid
        '
        Me.cAmountPaid.HeaderText = "Weight Acurred"
        Me.cAmountPaid.Name = "cAmountPaid"
        Me.cAmountPaid.Width = 140
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(256, 634)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(100, 40)
        Me.btnCancel.TabIndex = 158
        Me.btnCancel.Text = "Cancel (F10)"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnRePrint
        '
        Me.btnRePrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnRePrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRePrint.Location = New System.Drawing.Point(152, 634)
        Me.btnRePrint.Name = "btnRePrint"
        Me.btnRePrint.Size = New System.Drawing.Size(100, 40)
        Me.btnRePrint.TabIndex = 159
        Me.btnRePrint.Text = "RePrint (F9)"
        Me.btnRePrint.UseVisualStyleBackColor = False
        '
        'btnSAve
        '
        Me.btnSAve.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSAve.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSAve.Location = New System.Drawing.Point(463, 634)
        Me.btnSAve.Name = "btnSAve"
        Me.btnSAve.Size = New System.Drawing.Size(100, 40)
        Me.btnSAve.TabIndex = 155
        Me.btnSAve.Text = "Save (F7)"
        Me.btnSAve.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(566, 634)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 157
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnBillClear
        '
        Me.btnBillClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnBillClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBillClear.Location = New System.Drawing.Point(359, 634)
        Me.btnBillClear.Name = "btnBillClear"
        Me.btnBillClear.Size = New System.Drawing.Size(100, 40)
        Me.btnBillClear.TabIndex = 156
        Me.btnBillClear.Text = "Clear (F6)"
        Me.btnBillClear.UseVisualStyleBackColor = False
        '
        'btnSchemeNoSearch
        '
        Me.btnSchemeNoSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSchemeNoSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSchemeNoSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSchemeNoSearch.Location = New System.Drawing.Point(399, 32)
        Me.btnSchemeNoSearch.Name = "btnSchemeNoSearch"
        Me.btnSchemeNoSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnSchemeNoSearch.TabIndex = 526
        Me.btnSchemeNoSearch.Text = "..."
        Me.btnSchemeNoSearch.UseVisualStyleBackColor = False
        '
        'txtSchemeNo
        '
        Me.txtSchemeNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSchemeNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSchemeNo.ForeColor = System.Drawing.Color.Black
        Me.txtSchemeNo.Location = New System.Drawing.Point(303, 32)
        Me.txtSchemeNo.MaxLength = 20
        Me.txtSchemeNo.Name = "txtSchemeNo"
        Me.txtSchemeNo.Size = New System.Drawing.Size(96, 20)
        Me.txtSchemeNo.TabIndex = 525
        Me.txtSchemeNo.Tag = ""
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(207, 32)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(96, 20)
        Me.Label8.TabIndex = 527
        Me.Label8.Text = "Scheme No"
        '
        'frmSchemeTransactionDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(678, 735)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnRePrint)
        Me.Controls.Add(Me.btnSAve)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnBillClear)
        Me.Controls.Add(Me.grdInstallment)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmSchemeTransactionDetail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "GoldScheme TransactionDetail"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        CType(Me.grdInstallment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label82 As System.Windows.Forms.Label
    Friend WithEvents btnNewCustomer As System.Windows.Forms.Button
    Friend WithEvents lblCustomerType As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtSalesManName As System.Windows.Forms.TextBox
    Friend WithEvents txtSalesManPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnCustomerCodeSearch As System.Windows.Forms.Button
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtAddress3 As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress2 As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress1 As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerCode As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtIYYYY As System.Windows.Forms.TextBox
    Friend WithEvents txtIMM As System.Windows.Forms.TextBox
    Friend WithEvents txtIDD As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents grdInstallment As System.Windows.Forms.DataGridView
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnRePrint As System.Windows.Forms.Button
    Friend WithEvents btnSAve As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnBillClear As System.Windows.Forms.Button
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents optGram As System.Windows.Forms.RadioButton
    Friend WithEvents optValue As System.Windows.Forms.RadioButton
    Friend WithEvents opteTRSEligible As System.Windows.Forms.RadioButton
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnSchemeCode As System.Windows.Forms.Button
    Friend WithEvents txtSchemeCode As System.Windows.Forms.TextBox
    Friend WithEvents cInstallmentNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cInstallmentDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cNoofInstallment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cInstallmentAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cAmountPaid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtSchemeValue As System.Windows.Forms.TextBox
    Friend WithEvents btnSchemeNoSearch As System.Windows.Forms.Button
    Friend WithEvents txtSchemeNo As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
End Class
