Public Class frmCompany
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtCompanyCode As System.Windows.Forms.TextBox
    Friend WithEvents txtCompanyName As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress1 As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress2 As System.Windows.Forms.TextBox
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents txtState As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtUrl As System.Windows.Forms.TextBox
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtFax As System.Windows.Forms.TextBox
    Friend WithEvents txtphone As System.Windows.Forms.TextBox
    Friend WithEvents txtPostalCode As System.Windows.Forms.TextBox
    Friend WithEvents grbCompany As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtDateFormat As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtTimeFormat As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtDefaultCurrency As System.Windows.Forms.TextBox
    Friend WithEvents txtCurrencySign As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtRegNo As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    'Friend WithEvents txtGST As AxEditLib.AxfpDoubleSingle
    Friend WithEvents txtDisplayMessage2 As System.Windows.Forms.TextBox
    Friend WithEvents txtDisplayMessage1 As System.Windows.Forms.TextBox
    Friend WithEvents txtBillMessage2 As System.Windows.Forms.TextBox
    Friend WithEvents txtBillMessage1 As System.Windows.Forms.TextBox
    Friend WithEvents txtcountry As System.Windows.Forms.TextBox
    Friend WithEvents lblTransSetting As System.Windows.Forms.LinkLabel
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents grpTransaction As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents txtPaymentNo As System.Windows.Forms.TextBox
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents txtReturnNo As System.Windows.Forms.TextBox
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents cboBillPrint As System.Windows.Forms.CheckBox
    Friend WithEvents cboTaxTaken As System.Windows.Forms.CheckBox
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents txtTradeSalesNo As System.Windows.Forms.TextBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents txtTaxIn As System.Windows.Forms.TextBox
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents txtExpiredTime As System.Windows.Forms.TextBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents txtPoNo As System.Windows.Forms.TextBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents txtCredit As System.Windows.Forms.TextBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents txtAvgCost As System.Windows.Forms.TextBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents txtPriceChange As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents txtMaxDiscount As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents txtNextReceipt As System.Windows.Forms.TextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents txtNextInvoice As System.Windows.Forms.TextBox
    Friend WithEvents txtGST As System.Windows.Forms.TextBox
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents Label33 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCompany))
        Me.grbCompany = New System.Windows.Forms.GroupBox()
        Me.txtcountry = New System.Windows.Forms.TextBox()
        Me.txtUrl = New System.Windows.Forms.TextBox()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtFax = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtphone = New System.Windows.Forms.TextBox()
        Me.txtPostalCode = New System.Windows.Forms.TextBox()
        Me.txtState = New System.Windows.Forms.TextBox()
        Me.txtCity = New System.Windows.Forms.TextBox()
        Me.txtAddress2 = New System.Windows.Forms.TextBox()
        Me.txtAddress1 = New System.Windows.Forms.TextBox()
        Me.txtCompanyName = New System.Windows.Forms.TextBox()
        Me.txtCompanyCode = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtGST = New System.Windows.Forms.TextBox()
        Me.txtDisplayMessage2 = New System.Windows.Forms.TextBox()
        Me.txtDisplayMessage1 = New System.Windows.Forms.TextBox()
        Me.txtBillMessage2 = New System.Windows.Forms.TextBox()
        Me.txtBillMessage1 = New System.Windows.Forms.TextBox()
        Me.txtRegNo = New System.Windows.Forms.TextBox()
        Me.txtDefaultCurrency = New System.Windows.Forms.TextBox()
        Me.txtCurrencySign = New System.Windows.Forms.TextBox()
        Me.txtTimeFormat = New System.Windows.Forms.TextBox()
        Me.txtDateFormat = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.lblTransSetting = New System.Windows.Forms.LinkLabel()
        Me.grpTransaction = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.txtPaymentNo = New System.Windows.Forms.TextBox()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.txtReturnNo = New System.Windows.Forms.TextBox()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cboBillPrint = New System.Windows.Forms.CheckBox()
        Me.cboTaxTaken = New System.Windows.Forms.CheckBox()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.txtTradeSalesNo = New System.Windows.Forms.TextBox()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.txtTaxIn = New System.Windows.Forms.TextBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.txtExpiredTime = New System.Windows.Forms.TextBox()
        Me.txtPoNo = New System.Windows.Forms.TextBox()
        Me.txtCredit = New System.Windows.Forms.TextBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.txtAvgCost = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.txtPriceChange = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.txtMaxDiscount = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.txtNextReceipt = New System.Windows.Forms.TextBox()
        Me.txtNextInvoice = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.grbCompany.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.grpTransaction.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grbCompany
        '
        Me.grbCompany.BackColor = System.Drawing.SystemColors.Window
        Me.grbCompany.Controls.Add(Me.txtcountry)
        Me.grbCompany.Controls.Add(Me.txtUrl)
        Me.grbCompany.Controls.Add(Me.txtEmail)
        Me.grbCompany.Controls.Add(Me.Label13)
        Me.grbCompany.Controls.Add(Me.txtFax)
        Me.grbCompany.Controls.Add(Me.Label11)
        Me.grbCompany.Controls.Add(Me.Label10)
        Me.grbCompany.Controls.Add(Me.Label9)
        Me.grbCompany.Controls.Add(Me.Label8)
        Me.grbCompany.Controls.Add(Me.txtphone)
        Me.grbCompany.Controls.Add(Me.txtPostalCode)
        Me.grbCompany.Controls.Add(Me.txtState)
        Me.grbCompany.Controls.Add(Me.txtCity)
        Me.grbCompany.Controls.Add(Me.txtAddress2)
        Me.grbCompany.Controls.Add(Me.txtAddress1)
        Me.grbCompany.Controls.Add(Me.txtCompanyName)
        Me.grbCompany.Controls.Add(Me.txtCompanyCode)
        Me.grbCompany.Controls.Add(Me.Label7)
        Me.grbCompany.Controls.Add(Me.Label6)
        Me.grbCompany.Controls.Add(Me.Label5)
        Me.grbCompany.Controls.Add(Me.Label4)
        Me.grbCompany.Controls.Add(Me.Label2)
        Me.grbCompany.Controls.Add(Me.Label1)
        Me.grbCompany.Controls.Add(Me.Label22)
        Me.grbCompany.Controls.Add(Me.Label24)
        Me.grbCompany.Controls.Add(Me.Label25)
        Me.grbCompany.Controls.Add(Me.Label26)
        Me.grbCompany.Controls.Add(Me.Label28)
        Me.grbCompany.Controls.Add(Me.Label29)
        Me.grbCompany.Controls.Add(Me.Label30)
        Me.grbCompany.Controls.Add(Me.Label31)
        Me.grbCompany.Controls.Add(Me.Label32)
        Me.grbCompany.Controls.Add(Me.Label12)
        Me.grbCompany.Location = New System.Drawing.Point(3, 97)
        Me.grbCompany.Name = "grbCompany"
        Me.grbCompany.Size = New System.Drawing.Size(567, 316)
        Me.grbCompany.TabIndex = 0
        Me.grbCompany.TabStop = False
        '
        'txtcountry
        '
        Me.txtcountry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtcountry.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcountry.Location = New System.Drawing.Point(135, 184)
        Me.txtcountry.MaxLength = 50
        Me.txtcountry.Name = "txtcountry"
        Me.txtcountry.Size = New System.Drawing.Size(136, 21)
        Me.txtcountry.TabIndex = 7
        '
        'txtUrl
        '
        Me.txtUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUrl.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUrl.Location = New System.Drawing.Point(135, 280)
        Me.txtUrl.MaxLength = 50
        Me.txtUrl.Name = "txtUrl"
        Me.txtUrl.Size = New System.Drawing.Size(386, 21)
        Me.txtUrl.TabIndex = 12
        '
        'txtEmail
        '
        Me.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmail.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmail.Location = New System.Drawing.Point(135, 248)
        Me.txtEmail.MaxLength = 50
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(386, 21)
        Me.txtEmail.TabIndex = 11
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(8, 280)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(94, 20)
        Me.Label13.TabIndex = 22
        Me.Label13.Text = "URL "
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtFax
        '
        Me.txtFax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFax.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFax.Location = New System.Drawing.Point(374, 216)
        Me.txtFax.MaxLength = 15
        Me.txtFax.Name = "txtFax"
        Me.txtFax.Size = New System.Drawing.Size(147, 21)
        Me.txtFax.TabIndex = 10
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(8, 184)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(94, 20)
        Me.Label11.TabIndex = 19
        Me.Label11.Text = "Country"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(287, 216)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(78, 20)
        Me.Label10.TabIndex = 18
        Me.Label10.Text = "Fax"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(8, 216)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(94, 20)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Phone"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(8, 248)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(94, 20)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "E-mail"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtphone
        '
        Me.txtphone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtphone.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtphone.Location = New System.Drawing.Point(135, 216)
        Me.txtphone.MaxLength = 35
        Me.txtphone.Name = "txtphone"
        Me.txtphone.Size = New System.Drawing.Size(136, 21)
        Me.txtphone.TabIndex = 9
        '
        'txtPostalCode
        '
        Me.txtPostalCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPostalCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPostalCode.Location = New System.Drawing.Point(374, 184)
        Me.txtPostalCode.MaxLength = 10
        Me.txtPostalCode.Name = "txtPostalCode"
        Me.txtPostalCode.Size = New System.Drawing.Size(147, 21)
        Me.txtPostalCode.TabIndex = 8
        '
        'txtState
        '
        Me.txtState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtState.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtState.Location = New System.Drawing.Point(374, 152)
        Me.txtState.MaxLength = 50
        Me.txtState.Name = "txtState"
        Me.txtState.Size = New System.Drawing.Size(147, 21)
        Me.txtState.TabIndex = 6
        '
        'txtCity
        '
        Me.txtCity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCity.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCity.Location = New System.Drawing.Point(135, 152)
        Me.txtCity.MaxLength = 50
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(136, 21)
        Me.txtCity.TabIndex = 5
        '
        'txtAddress2
        '
        Me.txtAddress2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress2.Location = New System.Drawing.Point(135, 120)
        Me.txtAddress2.MaxLength = 50
        Me.txtAddress2.Name = "txtAddress2"
        Me.txtAddress2.Size = New System.Drawing.Size(384, 21)
        Me.txtAddress2.TabIndex = 4
        '
        'txtAddress1
        '
        Me.txtAddress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress1.Location = New System.Drawing.Point(135, 88)
        Me.txtAddress1.MaxLength = 50
        Me.txtAddress1.Name = "txtAddress1"
        Me.txtAddress1.Size = New System.Drawing.Size(384, 21)
        Me.txtAddress1.TabIndex = 3
        '
        'txtCompanyName
        '
        Me.txtCompanyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCompanyName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyName.Location = New System.Drawing.Point(135, 56)
        Me.txtCompanyName.MaxLength = 50
        Me.txtCompanyName.Name = "txtCompanyName"
        Me.txtCompanyName.Size = New System.Drawing.Size(384, 21)
        Me.txtCompanyName.TabIndex = 2
        '
        'txtCompanyCode
        '
        Me.txtCompanyCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtCompanyCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCompanyCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyCode.Location = New System.Drawing.Point(135, 24)
        Me.txtCompanyCode.MaxLength = 15
        Me.txtCompanyCode.Name = "txtCompanyCode"
        Me.txtCompanyCode.ReadOnly = True
        Me.txtCompanyCode.Size = New System.Drawing.Size(160, 21)
        Me.txtCompanyCode.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(287, 152)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(78, 20)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "State"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(9, 152)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(94, 20)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "City"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 120)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(103, 20)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Address2"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(8, 88)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(111, 20)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Address1"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(135, 20)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Company Name"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Company Code"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label22
        '
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(8, 184)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(56, 20)
        Me.Label22.TabIndex = 19
        Me.Label22.Text = "Country"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label24
        '
        Me.Label24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(8, 216)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(56, 20)
        Me.Label24.TabIndex = 17
        Me.Label24.Text = "Phone"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label25
        '
        Me.Label25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(8, 248)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(56, 20)
        Me.Label25.TabIndex = 16
        Me.Label25.Text = "E-mail"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label26
        '
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(8, 280)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(56, 20)
        Me.Label26.TabIndex = 22
        Me.Label26.Text = "URL "
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label28
        '
        Me.Label28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(8, 152)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(56, 20)
        Me.Label28.TabIndex = 5
        Me.Label28.Text = "City"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label29
        '
        Me.Label29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(8, 120)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(64, 20)
        Me.Label29.TabIndex = 4
        Me.Label29.Text = "Address2"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label30
        '
        Me.Label30.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(8, 88)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(72, 20)
        Me.Label30.TabIndex = 3
        Me.Label30.Text = "Address1"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label31
        '
        Me.Label31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(8, 56)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(96, 20)
        Me.Label31.TabIndex = 1
        Me.Label31.Text = "Company Name"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label32
        '
        Me.Label32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(8, 24)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(104, 20)
        Me.Label32.TabIndex = 0
        Me.Label32.Text = "Company Code"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(287, 184)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(94, 20)
        Me.Label12.TabIndex = 20
        Me.Label12.Text = "Postal Code"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cboBillPrint)
        Me.GroupBox1.Controls.Add(Me.Label44)
        Me.GroupBox1.Controls.Add(Me.txtGST)
        Me.GroupBox1.Controls.Add(Me.txtDisplayMessage2)
        Me.GroupBox1.Controls.Add(Me.txtDisplayMessage1)
        Me.GroupBox1.Controls.Add(Me.txtBillMessage2)
        Me.GroupBox1.Controls.Add(Me.txtBillMessage1)
        Me.GroupBox1.Controls.Add(Me.txtRegNo)
        Me.GroupBox1.Controls.Add(Me.txtDefaultCurrency)
        Me.GroupBox1.Controls.Add(Me.txtCurrencySign)
        Me.GroupBox1.Controls.Add(Me.txtTimeFormat)
        Me.GroupBox1.Controls.Add(Me.txtDateFormat)
        Me.GroupBox1.Controls.Add(Me.Label23)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Location = New System.Drawing.Point(576, 97)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(397, 316)
        Me.GroupBox1.TabIndex = 49
        Me.GroupBox1.TabStop = False
        '
        'txtGST
        '
        Me.txtGST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGST.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGST.Location = New System.Drawing.Point(170, 289)
        Me.txtGST.MaxLength = 50
        Me.txtGST.Name = "txtGST"
        Me.txtGST.Size = New System.Drawing.Size(111, 21)
        Me.txtGST.TabIndex = 72
        '
        'txtDisplayMessage2
        '
        Me.txtDisplayMessage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDisplayMessage2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDisplayMessage2.Location = New System.Drawing.Point(171, 256)
        Me.txtDisplayMessage2.MaxLength = 50
        Me.txtDisplayMessage2.Name = "txtDisplayMessage2"
        Me.txtDisplayMessage2.Size = New System.Drawing.Size(201, 21)
        Me.txtDisplayMessage2.TabIndex = 22
        '
        'txtDisplayMessage1
        '
        Me.txtDisplayMessage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDisplayMessage1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDisplayMessage1.Location = New System.Drawing.Point(171, 226)
        Me.txtDisplayMessage1.MaxLength = 50
        Me.txtDisplayMessage1.Name = "txtDisplayMessage1"
        Me.txtDisplayMessage1.Size = New System.Drawing.Size(201, 21)
        Me.txtDisplayMessage1.TabIndex = 21
        '
        'txtBillMessage2
        '
        Me.txtBillMessage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillMessage2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillMessage2.Location = New System.Drawing.Point(171, 196)
        Me.txtBillMessage2.MaxLength = 50
        Me.txtBillMessage2.Name = "txtBillMessage2"
        Me.txtBillMessage2.Size = New System.Drawing.Size(201, 21)
        Me.txtBillMessage2.TabIndex = 20
        '
        'txtBillMessage1
        '
        Me.txtBillMessage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillMessage1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillMessage1.Location = New System.Drawing.Point(171, 166)
        Me.txtBillMessage1.MaxLength = 50
        Me.txtBillMessage1.Name = "txtBillMessage1"
        Me.txtBillMessage1.Size = New System.Drawing.Size(201, 21)
        Me.txtBillMessage1.TabIndex = 19
        '
        'txtRegNo
        '
        Me.txtRegNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRegNo.Location = New System.Drawing.Point(171, 136)
        Me.txtRegNo.MaxLength = 50
        Me.txtRegNo.Name = "txtRegNo"
        Me.txtRegNo.Size = New System.Drawing.Size(201, 21)
        Me.txtRegNo.TabIndex = 18
        '
        'txtDefaultCurrency
        '
        Me.txtDefaultCurrency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDefaultCurrency.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDefaultCurrency.Location = New System.Drawing.Point(171, 106)
        Me.txtDefaultCurrency.MaxLength = 3
        Me.txtDefaultCurrency.Name = "txtDefaultCurrency"
        Me.txtDefaultCurrency.Size = New System.Drawing.Size(201, 21)
        Me.txtDefaultCurrency.TabIndex = 17
        '
        'txtCurrencySign
        '
        Me.txtCurrencySign.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrencySign.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencySign.Location = New System.Drawing.Point(171, 76)
        Me.txtCurrencySign.MaxLength = 3
        Me.txtCurrencySign.Name = "txtCurrencySign"
        Me.txtCurrencySign.Size = New System.Drawing.Size(201, 21)
        Me.txtCurrencySign.TabIndex = 16
        '
        'txtTimeFormat
        '
        Me.txtTimeFormat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTimeFormat.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTimeFormat.Location = New System.Drawing.Point(171, 46)
        Me.txtTimeFormat.MaxLength = 25
        Me.txtTimeFormat.Name = "txtTimeFormat"
        Me.txtTimeFormat.Size = New System.Drawing.Size(201, 21)
        Me.txtTimeFormat.TabIndex = 15
        '
        'txtDateFormat
        '
        Me.txtDateFormat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDateFormat.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFormat.Location = New System.Drawing.Point(171, 16)
        Me.txtDateFormat.MaxLength = 25
        Me.txtDateFormat.Name = "txtDateFormat"
        Me.txtDateFormat.Size = New System.Drawing.Size(201, 21)
        Me.txtDateFormat.TabIndex = 14
        '
        'Label23
        '
        Me.Label23.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(15, 288)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(154, 20)
        Me.Label23.TabIndex = 70
        Me.Label23.Text = "GST Percentage"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(15, 256)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(154, 20)
        Me.Label17.TabIndex = 68
        Me.Label17.Text = "Display Message 2:"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(15, 232)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(154, 16)
        Me.Label15.TabIndex = 67
        Me.Label15.Text = "Display Message 1:"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(15, 201)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(146, 16)
        Me.Label14.TabIndex = 66
        Me.Label14.Text = "Bill Message 2:"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(15, 171)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(146, 16)
        Me.Label3.TabIndex = 62
        Me.Label3.Text = "Bill Message 1:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(15, 140)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(146, 16)
        Me.Label16.TabIndex = 61
        Me.Label16.Text = "Business Reg. No"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(15, 78)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(138, 18)
        Me.Label19.TabIndex = 59
        Me.Label19.Text = "Currency Sign"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(16, 111)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(138, 16)
        Me.Label18.TabIndex = 58
        Me.Label18.Text = "Default Currency"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(15, 48)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(122, 20)
        Me.Label20.TabIndex = 55
        Me.Label20.Text = "Time Format"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(15, 16)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(114, 16)
        Me.Label21.TabIndex = 53
        Me.Label21.Text = "Date Format"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTransSetting
        '
        Me.lblTransSetting.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTransSetting.Location = New System.Drawing.Point(0, 562)
        Me.lblTransSetting.Name = "lblTransSetting"
        Me.lblTransSetting.Size = New System.Drawing.Size(201, 24)
        Me.lblTransSetting.TabIndex = 50
        Me.lblTransSetting.TabStop = True
        Me.lblTransSetting.Text = "Transaction Settings"
        Me.lblTransSetting.Visible = False
        '
        'grpTransaction
        '
        Me.grpTransaction.Controls.Add(Me.GroupBox4)
        Me.grpTransaction.Controls.Add(Me.GroupBox3)
        Me.grpTransaction.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpTransaction.Location = New System.Drawing.Point(3, 419)
        Me.grpTransaction.Name = "grpTransaction"
        Me.grpTransaction.Size = New System.Drawing.Size(970, 140)
        Me.grpTransaction.TabIndex = 52
        Me.grpTransaction.TabStop = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.txtPaymentNo)
        Me.GroupBox4.Controls.Add(Me.Label46)
        Me.GroupBox4.Controls.Add(Me.txtReturnNo)
        Me.GroupBox4.Controls.Add(Me.Label45)
        Me.GroupBox4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(706, 15)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(257, 119)
        Me.GroupBox4.TabIndex = 1
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Purchase"
        '
        'txtPaymentNo
        '
        Me.txtPaymentNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPaymentNo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaymentNo.Location = New System.Drawing.Point(132, 43)
        Me.txtPaymentNo.MaxLength = 25
        Me.txtPaymentNo.Name = "txtPaymentNo"
        Me.txtPaymentNo.Size = New System.Drawing.Size(106, 21)
        Me.txtPaymentNo.TabIndex = 39
        '
        'Label46
        '
        Me.Label46.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.Location = New System.Drawing.Point(14, 43)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(141, 20)
        Me.Label46.TabIndex = 65
        Me.Label46.Text = "Next PaymentNo"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtReturnNo
        '
        Me.txtReturnNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReturnNo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReturnNo.Location = New System.Drawing.Point(132, 17)
        Me.txtReturnNo.MaxLength = 25
        Me.txtReturnNo.Name = "txtReturnNo"
        Me.txtReturnNo.Size = New System.Drawing.Size(106, 21)
        Me.txtReturnNo.TabIndex = 38
        '
        'Label45
        '
        Me.Label45.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.Location = New System.Drawing.Point(14, 17)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(127, 20)
        Me.Label45.TabIndex = 63
        Me.Label45.Text = "Next ReturnNo"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cboTaxTaken)
        Me.GroupBox3.Controls.Add(Me.txtTradeSalesNo)
        Me.GroupBox3.Controls.Add(Me.Label43)
        Me.GroupBox3.Controls.Add(Me.txtTaxIn)
        Me.GroupBox3.Controls.Add(Me.Label42)
        Me.GroupBox3.Controls.Add(Me.txtExpiredTime)
        Me.GroupBox3.Controls.Add(Me.txtPoNo)
        Me.GroupBox3.Controls.Add(Me.txtCredit)
        Me.GroupBox3.Controls.Add(Me.Label38)
        Me.GroupBox3.Controls.Add(Me.txtAvgCost)
        Me.GroupBox3.Controls.Add(Me.Label37)
        Me.GroupBox3.Controls.Add(Me.txtPriceChange)
        Me.GroupBox3.Controls.Add(Me.Label36)
        Me.GroupBox3.Controls.Add(Me.txtMaxDiscount)
        Me.GroupBox3.Controls.Add(Me.Label35)
        Me.GroupBox3.Controls.Add(Me.txtNextReceipt)
        Me.GroupBox3.Controls.Add(Me.txtNextInvoice)
        Me.GroupBox3.Controls.Add(Me.Label33)
        Me.GroupBox3.Controls.Add(Me.Label41)
        Me.GroupBox3.Controls.Add(Me.Label40)
        Me.GroupBox3.Controls.Add(Me.Label39)
        Me.GroupBox3.Controls.Add(Me.Label34)
        Me.GroupBox3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(10, 16)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(690, 120)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Sales"
        '
        'cboBillPrint
        '
        Me.cboBillPrint.Location = New System.Drawing.Point(362, 291)
        Me.cboBillPrint.Name = "cboBillPrint"
        Me.cboBillPrint.Size = New System.Drawing.Size(28, 16)
        Me.cboBillPrint.TabIndex = 81
        '
        'cboTaxTaken
        '
        Me.cboTaxTaken.Location = New System.Drawing.Point(360, 96)
        Me.cboTaxTaken.Name = "cboTaxTaken"
        Me.cboTaxTaken.Size = New System.Drawing.Size(103, 16)
        Me.cboTaxTaken.TabIndex = 80
        '
        'Label44
        '
        Me.Label44.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(287, 286)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(69, 21)
        Me.Label44.TabIndex = 79
        Me.Label44.Text = "Offer day"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTradeSalesNo
        '
        Me.txtTradeSalesNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTradeSalesNo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTradeSalesNo.Location = New System.Drawing.Point(590, 93)
        Me.txtTradeSalesNo.MaxLength = 25
        Me.txtTradeSalesNo.Name = "txtTradeSalesNo"
        Me.txtTradeSalesNo.Size = New System.Drawing.Size(94, 21)
        Me.txtTradeSalesNo.TabIndex = 37
        '
        'Label43
        '
        Me.Label43.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.Location = New System.Drawing.Point(474, 94)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(176, 20)
        Me.Label43.TabIndex = 77
        Me.Label43.Text = "Trade in SalesNo"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTaxIn
        '
        Me.txtTaxIn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTaxIn.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTaxIn.Location = New System.Drawing.Point(590, 41)
        Me.txtTaxIn.MaxLength = 25
        Me.txtTaxIn.Name = "txtTaxIn"
        Me.txtTaxIn.Size = New System.Drawing.Size(94, 21)
        Me.txtTaxIn.TabIndex = 31
        '
        'Label42
        '
        Me.Label42.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.Location = New System.Drawing.Point(474, 44)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(157, 20)
        Me.Label42.TabIndex = 75
        Me.Label42.Text = "Tax IN"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtExpiredTime
        '
        Me.txtExpiredTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExpiredTime.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExpiredTime.Location = New System.Drawing.Point(359, 68)
        Me.txtExpiredTime.MaxLength = 25
        Me.txtExpiredTime.Name = "txtExpiredTime"
        Me.txtExpiredTime.Size = New System.Drawing.Size(109, 21)
        Me.txtExpiredTime.TabIndex = 33
        '
        'txtPoNo
        '
        Me.txtPoNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPoNo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPoNo.Location = New System.Drawing.Point(359, 42)
        Me.txtPoNo.MaxLength = 25
        Me.txtPoNo.Name = "txtPoNo"
        Me.txtPoNo.ReadOnly = True
        Me.txtPoNo.Size = New System.Drawing.Size(109, 21)
        Me.txtPoNo.TabIndex = 30
        '
        'txtCredit
        '
        Me.txtCredit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCredit.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCredit.Location = New System.Drawing.Point(125, 94)
        Me.txtCredit.MaxLength = 25
        Me.txtCredit.Name = "txtCredit"
        Me.txtCredit.Size = New System.Drawing.Size(117, 21)
        Me.txtCredit.TabIndex = 35
        '
        'Label38
        '
        Me.Label38.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(5, 95)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(129, 20)
        Me.Label38.TabIndex = 67
        Me.Label38.Text = "Next CreditNo"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtAvgCost
        '
        Me.txtAvgCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAvgCost.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAvgCost.Location = New System.Drawing.Point(125, 68)
        Me.txtAvgCost.MaxLength = 25
        Me.txtAvgCost.Name = "txtAvgCost"
        Me.txtAvgCost.Size = New System.Drawing.Size(117, 21)
        Me.txtAvgCost.TabIndex = 32
        '
        'Label37
        '
        Me.Label37.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(5, 68)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(137, 20)
        Me.Label37.TabIndex = 65
        Me.Label37.Text = "NextAvgCost%"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPriceChange
        '
        Me.txtPriceChange.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPriceChange.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPriceChange.Location = New System.Drawing.Point(125, 42)
        Me.txtPriceChange.MaxLength = 3
        Me.txtPriceChange.Name = "txtPriceChange"
        Me.txtPriceChange.Size = New System.Drawing.Size(117, 21)
        Me.txtPriceChange.TabIndex = 29
        '
        'Label36
        '
        Me.Label36.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.Location = New System.Drawing.Point(5, 43)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(153, 18)
        Me.Label36.TabIndex = 63
        Me.Label36.Text = "Max PriceChange"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtMaxDiscount
        '
        Me.txtMaxDiscount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMaxDiscount.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMaxDiscount.Location = New System.Drawing.Point(590, 15)
        Me.txtMaxDiscount.MaxLength = 3
        Me.txtMaxDiscount.Name = "txtMaxDiscount"
        Me.txtMaxDiscount.Size = New System.Drawing.Size(94, 21)
        Me.txtMaxDiscount.TabIndex = 28
        '
        'Label35
        '
        Me.Label35.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(474, 16)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(157, 20)
        Me.Label35.TabIndex = 61
        Me.Label35.Text = "Max Discount"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtNextReceipt
        '
        Me.txtNextReceipt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNextReceipt.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNextReceipt.Location = New System.Drawing.Point(359, 16)
        Me.txtNextReceipt.MaxLength = 25
        Me.txtNextReceipt.Name = "txtNextReceipt"
        Me.txtNextReceipt.ReadOnly = True
        Me.txtNextReceipt.Size = New System.Drawing.Size(109, 21)
        Me.txtNextReceipt.TabIndex = 27
        '
        'txtNextInvoice
        '
        Me.txtNextInvoice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNextInvoice.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNextInvoice.Location = New System.Drawing.Point(125, 16)
        Me.txtNextInvoice.MaxLength = 25
        Me.txtNextInvoice.Name = "txtNextInvoice"
        Me.txtNextInvoice.ReadOnly = True
        Me.txtNextInvoice.Size = New System.Drawing.Size(117, 21)
        Me.txtNextInvoice.TabIndex = 26
        '
        'Label33
        '
        Me.Label33.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(5, 16)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(126, 20)
        Me.Label33.TabIndex = 57
        Me.Label33.Text = "Next Invoice"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label41
        '
        Me.Label41.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.Location = New System.Drawing.Point(248, 94)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(165, 20)
        Me.Label41.TabIndex = 73
        Me.Label41.Text = "Tax Taken"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label40
        '
        Me.Label40.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(248, 68)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(165, 20)
        Me.Label40.TabIndex = 71
        Me.Label40.Text = "Expired Time"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label39
        '
        Me.Label39.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(248, 42)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(165, 20)
        Me.Label39.TabIndex = 69
        Me.Label39.Text = "Next PONo"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label34
        '
        Me.Label34.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(248, 16)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(165, 20)
        Me.Label34.TabIndex = 59
        Me.Label34.Text = "Next ReceiptNo"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.InitialImage = Nothing
        Me.pic.Location = New System.Drawing.Point(-1, -2)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 100)
        Me.pic.TabIndex = 476202
        Me.pic.TabStop = False
        '
        'cmdSave
        '
        Me.cmdSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdSave.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.ForeColor = System.Drawing.Color.Black
        Me.cmdSave.Location = New System.Drawing.Point(764, 562)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(100, 40)
        Me.cmdSave.TabIndex = 24
        Me.cmdSave.Tag = "U"
        Me.cmdSave.Text = "Save (F7)"
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.Color.Black
        Me.cmdClose.Location = New System.Drawing.Point(873, 562)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(100, 40)
        Me.cmdClose.TabIndex = 25
        Me.cmdClose.Text = "Close ( Esc )"
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'frmCompany
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(983, 610)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.grpTransaction)
        Me.Controls.Add(Me.lblTransSetting)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.grbCompany)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Menu = Me.MainMenu1
        Me.MinimizeBox = False
        Me.Name = "frmCompany"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Company Master"
        Me.grbCompany.ResumeLayout(False)
        Me.grbCompany.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpTransaction.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmCompany_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        FunctionControl(Me, "MACOMP")
        'getcountry()
        DispRecords()
        grpTransaction.Visible = True
    End Sub
    'Private Sub getcountry()
    '    Dim Dm As New DataManager
    '    Dm.FillDatasToComboListWithFilter(cboCountry, "tblCountryMaster", "CountryName", "SlNo")
    'End Sub
    Private Sub DispRecords()
        Dim dm As DataManager
        Dim Dr As DataRow()
        Try
            Dr = dm.RowReturnProjExecNonQryWithDataRow("Select * from tblCompany ")
            If Dr.Length > 0 Then
                txtCompanyCode.Text = Trim(Dr(0)("CompanyID") & "")
                txtCompanyName.Text = Trim(Dr(0)("Name") & "")
                txtAddress1.Text = Trim(Dr(0)("Address1") & "")
                txtAddress2.Text = Trim(Dr(0)("Address2") & "")
                txtCity.Text = Trim(Dr(0)("City") & "")
                txtState.Text = Trim(Dr(0)("State") & "")
                txtcountry.Text = Trim(Dr(0)("Country") & "")
                txtPostalCode.Text = Trim(Dr(0)("PostalCode") & "")
                txtphone.Text = Trim(Dr(0)("Phone") & "")
                txtFax.Text = Trim(Dr(0)("Fax") & "")
                txtEmail.Text = Trim(Dr(0)("Email") & "")
                txtUrl.Text = Trim(Dr(0)("URL") & "")
                txtRegNo.Text = Trim(Dr(0)("GSTRegNo") & "")
                txtCurrencySign.Text = Trim(Dr(0)("CurrencySign") & "")
                txtDefaultCurrency.Text = Trim(Dr(0)("DefaultCurrency") & "")
                txtDateFormat.Text = Trim(Dr(0)("DateFormat") & "")
                txtTimeFormat.Text = Trim(Dr(0)("TimeFormat") & "")
                txtBillMessage1.Text = Trim(Dr(0)("Message1") & "")
                txtBillMessage2.Text = Trim(Dr(0)("Message2") & "")
                txtDisplayMessage1.Text = Trim(Dr(0)("DisplayMessage1") & "")
                txtDisplayMessage2.Text = Trim(Dr(0)("DisplayMessage2") & "")
                txtGST.Text = Val(Dr(0)("GSTPerc") & "")

                'Transaction Information
                'sales
                txtNextInvoice.Text = Val(Dr(0)("NextInvoice") & "")
                txtNextReceipt.Text = Val(Dr(0)("NextReciptNo") & "")
                txtMaxDiscount.Text = Val(Dr(0)("MaxDiscountAllowed") & "")
                txtPriceChange.Text = Val(Dr(0)("MaxPriceChangeAllowed") & "")
                txtPoNo.Text = Val(Dr(0)("NextPoNo") & "")
                txtExpiredTime.Text = Val(Dr(0)("ExpiredTime") & "")
                txtAvgCost.Text = Val(Dr(0)("NextAverageCostPercentage") & "")
                txtTaxIn.Text = Val(Dr(0)("TaxIn") & "")
                txtTradeSalesNo.Text = Val(Dr(0)("NextTradeinSalesNo") & "")
                txtCredit.Text = Val(Dr(0)("NextPosCreditNo") & "")
                If Trim(Dr(0)("BillPrint")) = "1" Then
                    cboBillPrint.Checked = True
                Else
                    cboBillPrint.Checked = False
                End If
                If Trim(Dr(0)("TaxTaken")) = "1" Then
                    cboTaxTaken.Checked = True
                Else
                    cboTaxTaken.Checked = False
                End If
                'Purchase
                txtReturnNo.Text = Val(Dr(0)("NextPurReNo") & "")
                txtPaymentNo.Text = Val(Dr(0)("NextPaymentVoucherNo") & "")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        'If MsgBox("Do you want to Exit?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        Me.Close()
        'End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim oCls As New clsCompany
        Dim ExpiryDate As String
        Dim sTaxTaken As String
        Dim sBillPrint As String

        sBillPrint = IIf(cboBillPrint.Checked = True, "1", "0")
        sTaxTaken = IIf(cboTaxTaken.Checked = True, "1", "0")

        If Trim(txtCompanyName.Text) = "" Then
            MsgBox("Please Enter the Company Name", MsgBoxStyle.Information)
            Exit Sub
        End If

        If oCls.CompanySave(txtCompanyCode.Text.Trim, txtCompanyName.Text.Trim, txtAddress1.Text.Trim, txtAddress2.Text.Trim, _
                        txtCity.Text.Trim, txtState.Text.Trim, txtcountry.Text.Trim, txtPostalCode.Text.Trim, txtphone.Text.Trim, txtFax.Text.Trim, txtEmail.Text.Trim, _
                        txtUrl.Text.Trim, txtDateFormat.Text.Trim, txtTimeFormat.Text.Trim, txtCurrencySign.Text.Trim, txtDefaultCurrency.Text.Trim, _
                        txtRegNo.Text.Trim, txtBillMessage1.Text.Trim, txtBillMessage2.Text.Trim, _
                        txtDisplayMessage1.Text.Trim, txtDisplayMessage2.Text.Trim, _
                        CDbl(txtGST.Text.Trim), Val(txtNextInvoice.Text), Val(txtNextReceipt.Text), Val(txtMaxDiscount.Text), Val(txtPriceChange.Text), _
                         Val(txtPoNo.Text), txtTaxIn.Text.Trim, Val(txtAvgCost.Text), Val(txtExpiredTime.Text), sBillPrint, Val(txtCredit.Text), _
                          sTaxTaken, Val(txtTradeSalesNo.Text), Val(txtReturnNo.Text), Val(txtPaymentNo.Text)) = False Then
            MsgBox("Error in Save Record")
        Else
            MsgBox("Sucessfully Updated", MsgBoxStyle.Information)
            DispRecords()
        End If
    End Sub

    Private Sub frmCompany_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Up Then
            SendKeys.Send("+{Tab}")
        ElseIf e.KeyCode = Keys.Down Then
            SendKeys.Send("{Tab}")
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.KeyCode = Keys.F7 Then
            If cmdSave.Enabled = True Then cmdSave_Click(sender, e)
        ElseIf e.KeyCode = Keys.Escape Then
            If grpTransaction.Visible = False Then
                cmdClose_Click(sender, e)
            Else
                grpTransaction.Visible = False
            End If
        End If
    End Sub

    Private Sub lblTransSetting_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblTransSetting.LinkClicked
        grpTransaction.Visible = True
    End Sub
End Class
