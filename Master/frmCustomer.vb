Imports System
Imports System.IO
Imports System.Text.RegularExpressions
Public Class frmCustomer
    Inherits System.Windows.Forms.Form
    Dim objCustomer As New clsCustomer
    Const CustomerCode = 1
    Const CustomerName = 2
    Const Phone = 3
    Const Fax = 4
    Friend WithEvents txtCustomerCode As System.Windows.Forms.TextBox
    Friend WithEvents btnCustomerCodeSearch As System.Windows.Forms.Button
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents grdSalesSummary As System.Windows.Forms.DataGridView
    Friend WithEvents cInvoiceNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cInvoiceDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cPCS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grdSalesDetail As System.Windows.Forms.DataGridView
    Friend WithEvents cInventoryCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cWeightDetail As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cPCSDetail As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtAYYYY As System.Windows.Forms.TextBox
    Friend WithEvents txtAMM As System.Windows.Forms.TextBox
    Friend WithEvents txtADD As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents lblBalancePoints As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents grdCustomerPoints As System.Windows.Forms.DataGridView
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Protected WithEvents cmbNationality As System.Windows.Forms.ComboBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents lblTotalPoints As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents lblRedeemedPoints As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents optNone As System.Windows.Forms.RadioButton
    Friend WithEvents optPhone As System.Windows.Forms.RadioButton
    Friend WithEvents optSMS As System.Windows.Forms.RadioButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cExpiryDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cRemarks As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cRedeemed As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cInvoiceRedeem As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents grpImage As System.Windows.Forms.GroupBox
    Friend WithEvents btnBrowseImage As System.Windows.Forms.Button
    Friend WithEvents picImage As System.Windows.Forms.PictureBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents chkKYC As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowKYC As System.Windows.Forms.CheckBox
    Friend WithEvents grpImage2 As System.Windows.Forms.GroupBox
    Friend WithEvents PicImage2 As System.Windows.Forms.PictureBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Const Remarks = 5


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
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtURL As System.Windows.Forms.TextBox
    Friend WithEvents lblAddress2 As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtMobile As System.Windows.Forms.TextBox
    Friend WithEvents txtFax As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtAddress3 As System.Windows.Forms.TextBox
    Friend WithEvents txtPhone2 As System.Windows.Forms.TextBox
    Friend WithEvents lblPersonInCharge As System.Windows.Forms.Label
    Friend WithEvents txtPhone1 As System.Windows.Forms.TextBox
    Friend WithEvents lblFax As System.Windows.Forms.Label
    Friend WithEvents txtAddress4 As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress2 As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress1 As System.Windows.Forms.TextBox
    Friend WithEvents lblAddress1 As System.Windows.Forms.Label
    Friend WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents lblLocationCode As System.Windows.Forms.Label
    Friend WithEvents lblLocationName As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents txtNricNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtYYYY As System.Windows.Forms.TextBox
    Friend WithEvents txtMM As System.Windows.Forms.TextBox
    Friend WithEvents txtDD As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtPassportNo As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Protected WithEvents cboCustomerType As System.Windows.Forms.ComboBox
    Protected WithEvents cboDateType As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents optMale As System.Windows.Forms.RadioButton
    Friend WithEvents optFemale As System.Windows.Forms.RadioButton
    Friend WithEvents Label14 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCustomer))
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.optNone = New System.Windows.Forms.RadioButton()
        Me.optPhone = New System.Windows.Forms.RadioButton()
        Me.optSMS = New System.Windows.Forms.RadioButton()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.btnCustomerCodeSearch = New System.Windows.Forms.Button()
        Me.txtCustomerCode = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.optFemale = New System.Windows.Forms.RadioButton()
        Me.optMale = New System.Windows.Forms.RadioButton()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cboDateType = New System.Windows.Forms.ComboBox()
        Me.cboCustomerType = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtPassportNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtYYYY = New System.Windows.Forms.TextBox()
        Me.txtMM = New System.Windows.Forms.TextBox()
        Me.txtDD = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtNricNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtURL = New System.Windows.Forms.TextBox()
        Me.lblAddress2 = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtMobile = New System.Windows.Forms.TextBox()
        Me.txtFax = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtAddress3 = New System.Windows.Forms.TextBox()
        Me.txtPhone2 = New System.Windows.Forms.TextBox()
        Me.lblPersonInCharge = New System.Windows.Forms.Label()
        Me.txtPhone1 = New System.Windows.Forms.TextBox()
        Me.lblFax = New System.Windows.Forms.Label()
        Me.txtAddress4 = New System.Windows.Forms.TextBox()
        Me.txtAddress2 = New System.Windows.Forms.TextBox()
        Me.txtAddress1 = New System.Windows.Forms.TextBox()
        Me.lblAddress1 = New System.Windows.Forms.Label()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.lblLocationCode = New System.Windows.Forms.Label()
        Me.lblLocationName = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.grdSalesSummary = New System.Windows.Forms.DataGridView()
        Me.cInvoiceNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cInvoiceDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cPCS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.grdSalesDetail = New System.Windows.Forms.DataGridView()
        Me.cInventoryCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cDescription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cWeightDetail = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cPCSDetail = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.grdCustomerPoints = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cExpiryDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cRemarks = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cRedeemed = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cInvoiceRedeem = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtAYYYY = New System.Windows.Forms.TextBox()
        Me.txtAMM = New System.Windows.Forms.TextBox()
        Me.txtADD = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblBalancePoints = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.cmbNationality = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.lblTotalPoints = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.lblRedeemedPoints = New System.Windows.Forms.Label()
        Me.chkActive = New System.Windows.Forms.CheckBox()
        Me.grpImage = New System.Windows.Forms.GroupBox()
        Me.btnBrowseImage = New System.Windows.Forms.Button()
        Me.picImage = New System.Windows.Forms.PictureBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.chkKYC = New System.Windows.Forms.CheckBox()
        Me.chkShowKYC = New System.Windows.Forms.CheckBox()
        Me.grpImage2 = New System.Windows.Forms.GroupBox()
        Me.PicImage2 = New System.Windows.Forms.PictureBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.grdSalesSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.grdSalesDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.grdCustomerPoints, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpImage.SuspendLayout()
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpImage2.SuspendLayout()
        CType(Me.PicImage2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnNew.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Location = New System.Drawing.Point(345, 655)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(100, 40)
        Me.btnNew.TabIndex = 26
        Me.btnNew.Text = "New(F6)"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(690, 655)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 25
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(460, 655)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 40)
        Me.btnSave.TabIndex = 23
        Me.btnSave.Text = "Save(F7)"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDelete.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(575, 655)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(100, 40)
        Me.btnDelete.TabIndex = 24
        Me.btnDelete.Text = "Delete(F8)"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.btnCustomerCodeSearch)
        Me.GroupBox1.Controls.Add(Me.txtCustomerCode)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.optFemale)
        Me.GroupBox1.Controls.Add(Me.optMale)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.cboDateType)
        Me.GroupBox1.Controls.Add(Me.cboCustomerType)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtPassportNo)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtYYYY)
        Me.GroupBox1.Controls.Add(Me.txtMM)
        Me.GroupBox1.Controls.Add(Me.txtDD)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtNricNo)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtRemarks)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtURL)
        Me.GroupBox1.Controls.Add(Me.lblAddress2)
        Me.GroupBox1.Controls.Add(Me.txtEmail)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtMobile)
        Me.GroupBox1.Controls.Add(Me.txtFax)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtAddress3)
        Me.GroupBox1.Controls.Add(Me.txtPhone2)
        Me.GroupBox1.Controls.Add(Me.lblPersonInCharge)
        Me.GroupBox1.Controls.Add(Me.txtPhone1)
        Me.GroupBox1.Controls.Add(Me.lblFax)
        Me.GroupBox1.Controls.Add(Me.txtAddress4)
        Me.GroupBox1.Controls.Add(Me.txtAddress2)
        Me.GroupBox1.Controls.Add(Me.txtAddress1)
        Me.GroupBox1.Controls.Add(Me.lblAddress1)
        Me.GroupBox1.Controls.Add(Me.txtCustomerName)
        Me.GroupBox1.Controls.Add(Me.lblLocationCode)
        Me.GroupBox1.Controls.Add(Me.lblLocationName)
        Me.GroupBox1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(8, 97)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(782, 341)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Customer Details"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.optNone)
        Me.Panel1.Controls.Add(Me.optPhone)
        Me.Panel1.Controls.Add(Me.optSMS)
        Me.Panel1.Location = New System.Drawing.Point(520, 297)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(256, 42)
        Me.Panel1.TabIndex = 476254
        '
        'optNone
        '
        Me.optNone.AutoSize = True
        Me.optNone.Checked = True
        Me.optNone.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optNone.Location = New System.Drawing.Point(174, 12)
        Me.optNone.Name = "optNone"
        Me.optNone.Size = New System.Drawing.Size(58, 17)
        Me.optNone.TabIndex = 476252
        Me.optNone.TabStop = True
        Me.optNone.Text = "None"
        Me.optNone.UseVisualStyleBackColor = True
        '
        'optPhone
        '
        Me.optPhone.AutoSize = True
        Me.optPhone.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPhone.Location = New System.Drawing.Point(96, 12)
        Me.optPhone.Name = "optPhone"
        Me.optPhone.Size = New System.Drawing.Size(65, 17)
        Me.optPhone.TabIndex = 476251
        Me.optPhone.Text = "Phone"
        Me.optPhone.UseVisualStyleBackColor = True
        '
        'optSMS
        '
        Me.optSMS.AutoSize = True
        Me.optSMS.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSMS.Location = New System.Drawing.Point(20, 12)
        Me.optSMS.Name = "optSMS"
        Me.optSMS.Size = New System.Drawing.Size(51, 17)
        Me.optSMS.TabIndex = 476250
        Me.optSMS.Text = "SMS"
        Me.optSMS.UseVisualStyleBackColor = True
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Black
        Me.Label21.Location = New System.Drawing.Point(400, 301)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(98, 26)
        Me.Label21.TabIndex = 476253
        Me.Label21.Text = "Contact"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(48, 144)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(98, 24)
        Me.Label18.TabIndex = 476249
        Me.Label18.Text = "Country"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnCustomerCodeSearch
        '
        Me.btnCustomerCodeSearch.BackColor = System.Drawing.Color.Orange
        Me.btnCustomerCodeSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomerCodeSearch.Location = New System.Drawing.Point(335, 15)
        Me.btnCustomerCodeSearch.Name = "btnCustomerCodeSearch"
        Me.btnCustomerCodeSearch.Size = New System.Drawing.Size(32, 22)
        Me.btnCustomerCodeSearch.TabIndex = 476248
        Me.btnCustomerCodeSearch.Text = "..."
        Me.btnCustomerCodeSearch.UseVisualStyleBackColor = False
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCustomerCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerCode.Location = New System.Drawing.Point(165, 17)
        Me.txtCustomerCode.MaxLength = 50
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.Size = New System.Drawing.Size(203, 21)
        Me.txtCustomerCode.TabIndex = 1
        Me.txtCustomerCode.Tag = "NE"
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(48, 176)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(98, 24)
        Me.Label14.TabIndex = 476246
        Me.Label14.Text = "Postal Code"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'optFemale
        '
        Me.optFemale.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optFemale.Location = New System.Drawing.Point(231, 243)
        Me.optFemale.Name = "optFemale"
        Me.optFemale.Size = New System.Drawing.Size(80, 16)
        Me.optFemale.TabIndex = 10
        Me.optFemale.Text = "Female"
        '
        'optMale
        '
        Me.optMale.Checked = True
        Me.optMale.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optMale.Location = New System.Drawing.Point(166, 242)
        Me.optMale.Name = "optMale"
        Me.optMale.Size = New System.Drawing.Size(80, 16)
        Me.optMale.TabIndex = 9
        Me.optMale.TabStop = True
        Me.optMale.Text = "Male"
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(48, 233)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(98, 26)
        Me.Label13.TabIndex = 476245
        Me.Label13.Text = "Gender"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(48, 208)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(106, 21)
        Me.Label12.TabIndex = 476244
        Me.Label12.Text = "Nationality"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(400, 208)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(124, 26)
        Me.Label11.TabIndex = 476243
        Me.Label11.Text = "Religion"
        '
        'cboDateType
        '
        Me.cboDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDateType.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDateType.Location = New System.Drawing.Point(538, 208)
        Me.cboDateType.Name = "cboDateType"
        Me.cboDateType.Size = New System.Drawing.Size(219, 21)
        Me.cboDateType.Sorted = True
        Me.cboDateType.TabIndex = 21
        '
        'cboCustomerType
        '
        Me.cboCustomerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustomerType.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCustomerType.Items.AddRange(New Object() {"DIAMOND", "EMERALD", "RUBY"})
        Me.cboCustomerType.Location = New System.Drawing.Point(165, 46)
        Me.cboCustomerType.Name = "cboCustomerType"
        Me.cboCustomerType.Size = New System.Drawing.Size(203, 21)
        Me.cboCustomerType.Sorted = True
        Me.cboCustomerType.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(48, 46)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(135, 16)
        Me.Label3.TabIndex = 476241
        Me.Label3.Text = "Customer Type"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPassportNo
        '
        Me.txtPassportNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPassportNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPassportNo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassportNo.Location = New System.Drawing.Point(538, 17)
        Me.txtPassportNo.MaxLength = 50
        Me.txtPassportNo.Name = "txtPassportNo"
        Me.txtPassportNo.Size = New System.Drawing.Size(219, 21)
        Me.txtPassportNo.TabIndex = 11
        Me.txtPassportNo.Tag = "NE"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(400, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(146, 22)
        Me.Label2.TabIndex = 476240
        Me.Label2.Text = "Passport Number"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(611, 244)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(8, 16)
        Me.Label10.TabIndex = 476238
        Me.Label10.Text = "/"
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(564, 245)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(8, 16)
        Me.Label9.TabIndex = 476237
        Me.Label9.Text = "/"
        '
        'txtYYYY
        '
        Me.txtYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtYYYY.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtYYYY.Location = New System.Drawing.Point(631, 241)
        Me.txtYYYY.MaxLength = 20
        Me.txtYYYY.Name = "txtYYYY"
        Me.txtYYYY.Size = New System.Drawing.Size(61, 21)
        Me.txtYYYY.TabIndex = 20
        Me.txtYYYY.Tag = "NE;"
        '
        'txtMM
        '
        Me.txtMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMM.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMM.Location = New System.Drawing.Point(585, 240)
        Me.txtMM.MaxLength = 20
        Me.txtMM.Name = "txtMM"
        Me.txtMM.Size = New System.Drawing.Size(24, 21)
        Me.txtMM.TabIndex = 19
        Me.txtMM.Tag = "NE;"
        '
        'txtDD
        '
        Me.txtDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDD.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDD.Location = New System.Drawing.Point(538, 240)
        Me.txtDD.MaxLength = 20
        Me.txtDD.Name = "txtDD"
        Me.txtDD.Size = New System.Drawing.Size(25, 21)
        Me.txtDD.TabIndex = 18
        Me.txtDD.Tag = "NE;"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(400, 238)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(144, 20)
        Me.Label8.TabIndex = 476236
        Me.Label8.Text = "Date Of Birth"
        '
        'txtNricNo
        '
        Me.txtNricNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNricNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNricNo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNricNo.Location = New System.Drawing.Point(166, 271)
        Me.txtNricNo.MaxLength = 20
        Me.txtNricNo.Name = "txtNricNo"
        Me.txtNricNo.Size = New System.Drawing.Size(203, 21)
        Me.txtNricNo.TabIndex = 8
        Me.txtNricNo.Tag = "NE"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(49, 271)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(135, 19)
        Me.Label1.TabIndex = 476232
        Me.Label1.Text = "Nric Number"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsTab = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRemarks.Location = New System.Drawing.Point(165, 306)
        Me.txtRemarks.MaxLength = 50
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(203, 20)
        Me.txtRemarks.TabIndex = 22
        Me.txtRemarks.Tag = "NE"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(48, 301)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(111, 26)
        Me.Label6.TabIndex = 476230
        Me.Label6.Text = "Remarks"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtURL
        '
        Me.txtURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtURL.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtURL.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtURL.Location = New System.Drawing.Point(538, 176)
        Me.txtURL.MaxLength = 50
        Me.txtURL.Name = "txtURL"
        Me.txtURL.Size = New System.Drawing.Size(219, 21)
        Me.txtURL.TabIndex = 17
        Me.txtURL.Tag = "NE"
        Me.txtURL.Visible = False
        '
        'lblAddress2
        '
        Me.lblAddress2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddress2.ForeColor = System.Drawing.Color.Black
        Me.lblAddress2.Location = New System.Drawing.Point(400, 176)
        Me.lblAddress2.Name = "lblAddress2"
        Me.lblAddress2.Size = New System.Drawing.Size(138, 21)
        Me.lblAddress2.TabIndex = 476223
        Me.lblAddress2.Text = "Web Page"
        Me.lblAddress2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblAddress2.Visible = False
        '
        'txtEmail
        '
        Me.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmail.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtEmail.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmail.Location = New System.Drawing.Point(538, 176)
        Me.txtEmail.MaxLength = 50
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(219, 21)
        Me.txtEmail.TabIndex = 16
        Me.txtEmail.Tag = "NE"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(400, 176)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(138, 21)
        Me.Label4.TabIndex = 476222
        Me.Label4.Text = "Email"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtMobile
        '
        Me.txtMobile.AcceptsTab = True
        Me.txtMobile.BackColor = System.Drawing.SystemColors.HighlightText
        Me.txtMobile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMobile.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMobile.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMobile.ForeColor = System.Drawing.Color.Black
        Me.txtMobile.Location = New System.Drawing.Point(538, 110)
        Me.txtMobile.MaxLength = 50
        Me.txtMobile.Name = "txtMobile"
        Me.txtMobile.Size = New System.Drawing.Size(219, 21)
        Me.txtMobile.TabIndex = 14
        Me.txtMobile.Tag = "NE"
        '
        'txtFax
        '
        Me.txtFax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFax.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFax.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFax.Location = New System.Drawing.Point(538, 144)
        Me.txtFax.MaxLength = 50
        Me.txtFax.Name = "txtFax"
        Me.txtFax.Size = New System.Drawing.Size(219, 21)
        Me.txtFax.TabIndex = 15
        Me.txtFax.Tag = "NE"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(400, 110)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(138, 24)
        Me.Label5.TabIndex = 476220
        Me.Label5.Text = "Mobile"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(400, 144)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(138, 18)
        Me.Label7.TabIndex = 476221
        Me.Label7.Text = "Fax"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtAddress3
        '
        Me.txtAddress3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAddress3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress3.Location = New System.Drawing.Point(165, 176)
        Me.txtAddress3.MaxLength = 50
        Me.txtAddress3.Name = "txtAddress3"
        Me.txtAddress3.Size = New System.Drawing.Size(203, 21)
        Me.txtAddress3.TabIndex = 6
        Me.txtAddress3.Tag = "NE"
        '
        'txtPhone2
        '
        Me.txtPhone2.AcceptsTab = True
        Me.txtPhone2.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtPhone2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPhone2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPhone2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPhone2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPhone2.Location = New System.Drawing.Point(538, 80)
        Me.txtPhone2.MaxLength = 50
        Me.txtPhone2.Name = "txtPhone2"
        Me.txtPhone2.Size = New System.Drawing.Size(219, 21)
        Me.txtPhone2.TabIndex = 13
        Me.txtPhone2.Tag = "NE"
        '
        'lblPersonInCharge
        '
        Me.lblPersonInCharge.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPersonInCharge.ForeColor = System.Drawing.Color.Black
        Me.lblPersonInCharge.Location = New System.Drawing.Point(400, 80)
        Me.lblPersonInCharge.Name = "lblPersonInCharge"
        Me.lblPersonInCharge.Size = New System.Drawing.Size(138, 19)
        Me.lblPersonInCharge.TabIndex = 476211
        Me.lblPersonInCharge.Text = "Phone2"
        Me.lblPersonInCharge.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPhone1
        '
        Me.txtPhone1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPhone1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPhone1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPhone1.Location = New System.Drawing.Point(538, 46)
        Me.txtPhone1.MaxLength = 50
        Me.txtPhone1.Name = "txtPhone1"
        Me.txtPhone1.Size = New System.Drawing.Size(219, 21)
        Me.txtPhone1.TabIndex = 12
        Me.txtPhone1.Tag = "NE"
        '
        'lblFax
        '
        Me.lblFax.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFax.ForeColor = System.Drawing.Color.Black
        Me.lblFax.Location = New System.Drawing.Point(400, 46)
        Me.lblFax.Name = "lblFax"
        Me.lblFax.Size = New System.Drawing.Size(138, 25)
        Me.lblFax.TabIndex = 476210
        Me.lblFax.Text = "Phone1"
        Me.lblFax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtAddress4
        '
        Me.txtAddress4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAddress4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress4.Location = New System.Drawing.Point(165, 208)
        Me.txtAddress4.MaxLength = 50
        Me.txtAddress4.Name = "txtAddress4"
        Me.txtAddress4.Size = New System.Drawing.Size(203, 21)
        Me.txtAddress4.TabIndex = 7
        Me.txtAddress4.Tag = "NE"
        Me.txtAddress4.Visible = False
        '
        'txtAddress2
        '
        Me.txtAddress2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAddress2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress2.Location = New System.Drawing.Point(165, 144)
        Me.txtAddress2.MaxLength = 50
        Me.txtAddress2.Name = "txtAddress2"
        Me.txtAddress2.Size = New System.Drawing.Size(203, 21)
        Me.txtAddress2.TabIndex = 5
        Me.txtAddress2.Tag = "NE"
        '
        'txtAddress1
        '
        Me.txtAddress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAddress1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress1.Location = New System.Drawing.Point(165, 110)
        Me.txtAddress1.MaxLength = 50
        Me.txtAddress1.Name = "txtAddress1"
        Me.txtAddress1.Size = New System.Drawing.Size(203, 21)
        Me.txtAddress1.TabIndex = 4
        Me.txtAddress1.Tag = "NE"
        '
        'lblAddress1
        '
        Me.lblAddress1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddress1.ForeColor = System.Drawing.Color.Black
        Me.lblAddress1.Location = New System.Drawing.Point(48, 110)
        Me.lblAddress1.Name = "lblAddress1"
        Me.lblAddress1.Size = New System.Drawing.Size(111, 23)
        Me.lblAddress1.TabIndex = 476206
        Me.lblAddress1.Text = "Address"
        Me.lblAddress1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCustomerName
        '
        Me.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCustomerName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerName.Location = New System.Drawing.Point(165, 80)
        Me.txtCustomerName.MaxLength = 50
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(203, 21)
        Me.txtCustomerName.TabIndex = 3
        Me.txtCustomerName.Tag = "NE"
        '
        'lblLocationCode
        '
        Me.lblLocationCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationCode.ForeColor = System.Drawing.Color.Black
        Me.lblLocationCode.Location = New System.Drawing.Point(48, 17)
        Me.lblLocationCode.Name = "lblLocationCode"
        Me.lblLocationCode.Size = New System.Drawing.Size(135, 18)
        Me.lblLocationCode.TabIndex = 476204
        Me.lblLocationCode.Text = "Customer Code"
        Me.lblLocationCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLocationName
        '
        Me.lblLocationName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationName.ForeColor = System.Drawing.Color.Black
        Me.lblLocationName.Location = New System.Drawing.Point(48, 80)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(135, 22)
        Me.lblLocationName.TabIndex = 476205
        Me.lblLocationName.Text = "Customer Name"
        Me.lblLocationName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(8, 444)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(782, 205)
        Me.TabControl1.TabIndex = 11
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.HighlightText
        Me.TabPage1.Controls.Add(Me.grdSalesSummary)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(774, 179)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Sales Summary                                   "
        '
        'grdSalesSummary
        '
        Me.grdSalesSummary.AllowUserToAddRows = False
        Me.grdSalesSummary.AllowUserToDeleteRows = False
        Me.grdSalesSummary.AllowUserToResizeColumns = False
        Me.grdSalesSummary.AllowUserToResizeRows = False
        Me.grdSalesSummary.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdSalesSummary.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.grdSalesSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdSalesSummary.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cInvoiceNo, Me.cInvoiceDate, Me.cWeight, Me.cPCS, Me.cPrice})
        Me.grdSalesSummary.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdSalesSummary.Location = New System.Drawing.Point(4, 5)
        Me.grdSalesSummary.Name = "grdSalesSummary"
        Me.grdSalesSummary.RowHeadersVisible = False
        Me.grdSalesSummary.Size = New System.Drawing.Size(766, 171)
        Me.grdSalesSummary.TabIndex = 89
        '
        'cInvoiceNo
        '
        Me.cInvoiceNo.HeaderText = "InvoiceNo"
        Me.cInvoiceNo.Name = "cInvoiceNo"
        Me.cInvoiceNo.Width = 130
        '
        'cInvoiceDate
        '
        Me.cInvoiceDate.HeaderText = "InvoiceDate"
        Me.cInvoiceDate.Name = "cInvoiceDate"
        '
        'cWeight
        '
        Me.cWeight.HeaderText = "Weight"
        Me.cWeight.Name = "cWeight"
        Me.cWeight.Width = 150
        '
        'cPCS
        '
        Me.cPCS.HeaderText = "PCS"
        Me.cPCS.Name = "cPCS"
        Me.cPCS.Width = 150
        '
        'cPrice
        '
        Me.cPrice.HeaderText = "Price"
        Me.cPrice.Name = "cPrice"
        Me.cPrice.Width = 150
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.SystemColors.HighlightText
        Me.TabPage2.Controls.Add(Me.grdSalesDetail)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(774, 179)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Sales Detail                                                                     " & _
            ""
        Me.TabPage2.Visible = False
        '
        'grdSalesDetail
        '
        Me.grdSalesDetail.AllowUserToAddRows = False
        Me.grdSalesDetail.AllowUserToDeleteRows = False
        Me.grdSalesDetail.AllowUserToResizeColumns = False
        Me.grdSalesDetail.AllowUserToResizeRows = False
        Me.grdSalesDetail.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdSalesDetail.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.grdSalesDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdSalesDetail.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cInventoryCode, Me.cDescription, Me.cWeightDetail, Me.cPCSDetail, Me.cAmount})
        Me.grdSalesDetail.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdSalesDetail.Location = New System.Drawing.Point(4, 4)
        Me.grdSalesDetail.Name = "grdSalesDetail"
        Me.grdSalesDetail.RowHeadersVisible = False
        Me.grdSalesDetail.Size = New System.Drawing.Size(766, 172)
        Me.grdSalesDetail.TabIndex = 90
        '
        'cInventoryCode
        '
        Me.cInventoryCode.HeaderText = "InventoryCode"
        Me.cInventoryCode.Name = "cInventoryCode"
        Me.cInventoryCode.Width = 120
        '
        'cDescription
        '
        Me.cDescription.HeaderText = "Description"
        Me.cDescription.Name = "cDescription"
        Me.cDescription.Width = 300
        '
        'cWeightDetail
        '
        Me.cWeightDetail.HeaderText = "Weight"
        Me.cWeightDetail.Name = "cWeightDetail"
        '
        'cPCSDetail
        '
        Me.cPCSDetail.HeaderText = "PCS"
        Me.cPCSDetail.Name = "cPCSDetail"
        '
        'cAmount
        '
        Me.cAmount.HeaderText = "Amount"
        Me.cAmount.Name = "cAmount"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.grdCustomerPoints)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(774, 179)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Customer Points"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'grdCustomerPoints
        '
        Me.grdCustomerPoints.AllowUserToAddRows = False
        Me.grdCustomerPoints.AllowUserToDeleteRows = False
        Me.grdCustomerPoints.AllowUserToResizeColumns = False
        Me.grdCustomerPoints.AllowUserToResizeRows = False
        Me.grdCustomerPoints.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdCustomerPoints.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle9
        Me.grdCustomerPoints.ColumnHeadersHeight = 35
        Me.grdCustomerPoints.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5, Me.cExpiryDate, Me.cRemarks, Me.cRedeemed, Me.cInvoiceRedeem})
        Me.grdCustomerPoints.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdCustomerPoints.Location = New System.Drawing.Point(4, 4)
        Me.grdCustomerPoints.Name = "grdCustomerPoints"
        Me.grdCustomerPoints.RowHeadersVisible = False
        Me.grdCustomerPoints.Size = New System.Drawing.Size(766, 169)
        Me.grdCustomerPoints.TabIndex = 90
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "InvoiceNo"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "InvoiceDate"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Net Amount"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "Earned Points"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "Redeemed Points"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.Width = 90
        '
        'cExpiryDate
        '
        Me.cExpiryDate.HeaderText = "Expiry Date"
        Me.cExpiryDate.Name = "cExpiryDate"
        '
        'cRemarks
        '
        Me.cRemarks.HeaderText = "Remarks"
        Me.cRemarks.Name = "cRemarks"
        Me.cRemarks.Width = 170
        '
        'cRedeemed
        '
        Me.cRedeemed.HeaderText = "Redeemed"
        Me.cRedeemed.Name = "cRedeemed"
        '
        'cInvoiceRedeem
        '
        Me.cInvoiceRedeem.HeaderText = "Invoice Redeem"
        Me.cInvoiceRedeem.Name = "cInvoiceRedeem"
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(8, 3)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 92)
        Me.pic.TabIndex = 84
        Me.pic.TabStop = False
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(619, 369)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(8, 16)
        Me.Label15.TabIndex = 476244
        Me.Label15.Text = "/"
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(572, 370)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(8, 16)
        Me.Label16.TabIndex = 476243
        Me.Label16.Text = "/"
        '
        'txtAYYYY
        '
        Me.txtAYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAYYYY.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAYYYY.Location = New System.Drawing.Point(639, 366)
        Me.txtAYYYY.MaxLength = 20
        Me.txtAYYYY.Name = "txtAYYYY"
        Me.txtAYYYY.Size = New System.Drawing.Size(61, 21)
        Me.txtAYYYY.TabIndex = 476241
        Me.txtAYYYY.Tag = "NE;"
        '
        'txtAMM
        '
        Me.txtAMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAMM.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAMM.Location = New System.Drawing.Point(593, 365)
        Me.txtAMM.MaxLength = 20
        Me.txtAMM.Name = "txtAMM"
        Me.txtAMM.Size = New System.Drawing.Size(24, 21)
        Me.txtAMM.TabIndex = 476240
        Me.txtAMM.Tag = "NE;"
        '
        'txtADD
        '
        Me.txtADD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtADD.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtADD.Location = New System.Drawing.Point(546, 365)
        Me.txtADD.MaxLength = 20
        Me.txtADD.Name = "txtADD"
        Me.txtADD.Size = New System.Drawing.Size(25, 21)
        Me.txtADD.TabIndex = 476239
        Me.txtADD.Tag = "NE;"
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(408, 363)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(144, 20)
        Me.Label17.TabIndex = 476242
        Me.Label17.Text = "Date Of Anniversary"
        '
        'lblBalancePoints
        '
        Me.lblBalancePoints.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalancePoints.ForeColor = System.Drawing.Color.Red
        Me.lblBalancePoints.Location = New System.Drawing.Point(671, 68)
        Me.lblBalancePoints.Name = "lblBalancePoints"
        Me.lblBalancePoints.Size = New System.Drawing.Size(111, 26)
        Me.lblBalancePoints.TabIndex = 476245
        Me.lblBalancePoints.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(543, 69)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(111, 26)
        Me.Label19.TabIndex = 476246
        Me.Label19.Text = "Balance Points"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbNationality
        '
        Me.cmbNationality.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbNationality.Location = New System.Drawing.Point(173, 304)
        Me.cmbNationality.Name = "cmbNationality"
        Me.cmbNationality.Size = New System.Drawing.Size(203, 21)
        Me.cmbNationality.Sorted = True
        Me.cmbNationality.TabIndex = 476247
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.Location = New System.Drawing.Point(543, 17)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(111, 26)
        Me.Label20.TabIndex = 476249
        Me.Label20.Text = "Total Points"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalPoints
        '
        Me.lblTotalPoints.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalPoints.ForeColor = System.Drawing.Color.Red
        Me.lblTotalPoints.Location = New System.Drawing.Point(671, 16)
        Me.lblTotalPoints.Name = "lblTotalPoints"
        Me.lblTotalPoints.Size = New System.Drawing.Size(111, 26)
        Me.lblTotalPoints.TabIndex = 476248
        Me.lblTotalPoints.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label22
        '
        Me.Label22.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.Black
        Me.Label22.Location = New System.Drawing.Point(543, 43)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(124, 26)
        Me.Label22.TabIndex = 476251
        Me.Label22.Text = "Redeemed Points"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRedeemedPoints
        '
        Me.lblRedeemedPoints.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRedeemedPoints.ForeColor = System.Drawing.Color.Red
        Me.lblRedeemedPoints.Location = New System.Drawing.Point(671, 42)
        Me.lblRedeemedPoints.Name = "lblRedeemedPoints"
        Me.lblRedeemedPoints.Size = New System.Drawing.Size(111, 26)
        Me.lblRedeemedPoints.TabIndex = 476250
        Me.lblRedeemedPoints.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkActive
        '
        Me.chkActive.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkActive.ForeColor = System.Drawing.Color.Black
        Me.chkActive.Location = New System.Drawing.Point(716, 363)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkActive.Size = New System.Drawing.Size(66, 18)
        Me.chkActive.TabIndex = 476252
        Me.chkActive.Text = "Active"
        '
        'grpImage
        '
        Me.grpImage.Controls.Add(Me.btnBrowseImage)
        Me.grpImage.Controls.Add(Me.picImage)
        Me.grpImage.Controls.Add(Me.Button1)
        Me.grpImage.Location = New System.Drawing.Point(280, 199)
        Me.grpImage.Name = "grpImage"
        Me.grpImage.Size = New System.Drawing.Size(524, 437)
        Me.grpImage.TabIndex = 476263
        Me.grpImage.TabStop = False
        Me.grpImage.Visible = False
        '
        'btnBrowseImage
        '
        Me.btnBrowseImage.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnBrowseImage.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowseImage.Location = New System.Drawing.Point(32, 405)
        Me.btnBrowseImage.Name = "btnBrowseImage"
        Me.btnBrowseImage.Size = New System.Drawing.Size(58, 25)
        Me.btnBrowseImage.TabIndex = 771
        Me.btnBrowseImage.Text = "Browse"
        Me.btnBrowseImage.UseVisualStyleBackColor = False
        '
        'picImage
        '
        Me.picImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picImage.Location = New System.Drawing.Point(22, 15)
        Me.picImage.Name = "picImage"
        Me.picImage.Size = New System.Drawing.Size(488, 363)
        Me.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picImage.TabIndex = 770
        Me.picImage.TabStop = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Button1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.Black
        Me.Button1.Location = New System.Drawing.Point(177, 405)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(61, 25)
        Me.Button1.TabIndex = 45
        Me.Button1.Text = "OK"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'chkKYC
        '
        Me.chkKYC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkKYC.ForeColor = System.Drawing.Color.Black
        Me.chkKYC.Location = New System.Drawing.Point(16, 667)
        Me.chkKYC.Name = "chkKYC"
        Me.chkKYC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkKYC.Size = New System.Drawing.Size(138, 18)
        Me.chkKYC.TabIndex = 476264
        Me.chkKYC.Text = "Add KYC"
        '
        'chkShowKYC
        '
        Me.chkShowKYC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShowKYC.ForeColor = System.Drawing.Color.Black
        Me.chkShowKYC.Location = New System.Drawing.Point(160, 667)
        Me.chkShowKYC.Name = "chkShowKYC"
        Me.chkShowKYC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkShowKYC.Size = New System.Drawing.Size(138, 18)
        Me.chkShowKYC.TabIndex = 476265
        Me.chkShowKYC.Text = "Show  KYC"
        '
        'grpImage2
        '
        Me.grpImage2.Controls.Add(Me.PicImage2)
        Me.grpImage2.Controls.Add(Me.Button3)
        Me.grpImage2.Location = New System.Drawing.Point(185, 135)
        Me.grpImage2.Name = "grpImage2"
        Me.grpImage2.Size = New System.Drawing.Size(524, 437)
        Me.grpImage2.TabIndex = 476266
        Me.grpImage2.TabStop = False
        Me.grpImage2.Visible = False
        '
        'PicImage2
        '
        Me.PicImage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PicImage2.Location = New System.Drawing.Point(22, 15)
        Me.PicImage2.Name = "PicImage2"
        Me.PicImage2.Size = New System.Drawing.Size(488, 363)
        Me.PicImage2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicImage2.TabIndex = 770
        Me.PicImage2.TabStop = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Button3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.ForeColor = System.Drawing.Color.Black
        Me.Button3.Location = New System.Drawing.Point(177, 405)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(61, 25)
        Me.Button3.TabIndex = 45
        Me.Button3.Text = "OK"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'frmCustomer
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(7, 14)
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(827, 707)
        Me.Controls.Add(Me.grpImage2)
        Me.Controls.Add(Me.chkShowKYC)
        Me.Controls.Add(Me.chkKYC)
        Me.Controls.Add(Me.grpImage)
        Me.Controls.Add(Me.chkActive)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.lblRedeemedPoints)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.lblTotalPoints)
        Me.Controls.Add(Me.cmbNationality)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.lblBalancePoints)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.txtAYYYY)
        Me.Controls.Add(Me.txtAMM)
        Me.Controls.Add(Me.txtADD)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnDelete)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCustomer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Customer"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.grdSalesSummary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.grdSalesDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.grdCustomerPoints, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpImage.ResumeLayout(False)
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpImage2.ResumeLayout(False)
        CType(Me.PicImage2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub frmCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'subGridSetting(GrdSalesSummary)
        'subGridSetting(grdSalesDetail)
        ClearMe()
        'cboCustomerType.Text = "Normal"
        cboCustomerType.SelectedIndex = 0
        subAddNationality()
        addCboReligion()
        '  cboDateType.SelectedIndex = 0
        'cboDateType.Text = "BirthDay"
        'GetCustomerDetails()
        'LoadCustomerDetails(Trim(GetText(grdCustomer, grdCustomer.ActiveRow, 1)))
    End Sub

    Private Sub GetCustomerDetails()
        Try
            'Dim dsCustomer As New DataSet
            'Dim iCnt As Long
            'dsCustomer = objCustomer.GetCustomer
            'If Not IsNothing(dsCustomer) Then
            '    If dsCustomer.Tables("Customer").Rows.Count > 0 Then
            '        grdCustomer.MaxRows = 0
            '        For iCnt = 0 To dsCustomer.Tables("Customer").Rows.Count - 1
            '            grdCustomer.MaxRows = grdCustomer.MaxRows + 1
            '            SetText(grdCustomer, iCnt + 1, CustomerCode, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("CustomerCode") & ""))
            '            SetText(grdCustomer, iCnt + 1, CustomerName, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("CustomerName") & ""))
            '            SetText(grdCustomer, iCnt + 1, Phone, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("Phone1") & ""))
            '            SetText(grdCustomer, iCnt + 1, Fax, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("Fax") & ""))
            '            SetText(grdCustomer, iCnt + 1, Remarks, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("Remarks") & ""))
            '        Next
            '    End If
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If txtCustomerCode.Text.Trim <> "" Then
                Dim sCustomerCode As String
                sCustomerCode = txtCustomerCode.Text.Trim
                If fncGetCustomerTran(sCustomerCode) = True Then
                    MsgBox("Can Not Delete Customer , Beacuse This Customer Is Having Some Transaction", MsgBoxStyle.Information)
                    Exit Sub
                Else
                    If MessageBox.Show("Do you want to delete " & sCustomerCode & " Customer?", "GoldPOS", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        If objCustomer.DeleteCustomer(sCustomerCode) Then
                            MessageBox.Show("Customer detail deleted.")
                            ClearMe()
                            txtCustomerCode.Focus()
                            'GetCustomerDetails()
                            'LoadCustomerDetails(Trim(GetText(grdCustomer, grdCustomer.ActiveRow, 1)))
                            'grdCustomer.Focus()
                        Else
                            MessageBox.Show("Error in deleting Customer detail.")
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'Dim objfrm As New frmCustomerMnt
        'objfrm.sCustomercode = Trim(GetText(grdCustomer, grdCustomer.ActiveRow, 1))
        'objfrm.ShowDialog()
        Dim AccType As String
        Dim DOB As String
        Dim sGender As String
        Dim DOA As String
        Dim GLaccType As String
        Dim ByteArr() As Byte
        Try


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

            If ValidateMe() Then
                'If optCash.Checked = True Then
                '    AccType = "C"
                'Else
                '    AccType = "G"
                'End If
                If optSMS.Checked = True Then
                    AccType = "S"
                ElseIf optPhone.Checked = True Then
                    AccType = "P"
                Else
                    AccType = "N"
                End If
                If chkActive.Checked Then
                    GLaccType = "Y"
                Else
                    GLaccType = "N"
                End If
                DOB = txtDD.Text.Trim & "/" & txtMM.Text.Trim & "/" & txtYYYY.Text.Trim
                DOA = txtADD.Text.Trim & "/" & txtAMM.Text.Trim & "/" & txtAYYYY.Text.Trim
                ''HAJ Add gender Colum on 28 Jan 2008
                If optMale.Checked Then
                    sGender = "M"
                Else
                    sGender = "F"
                End If

                If objCustomer.CustomerSave(txtCustomerCode.Text.Trim, txtCustomerName.Text.Trim, txtAddress1.Text.Trim, txtAddress2.Text.Trim, txtAddress3.Text.Trim, cmbNationality.Text.Trim, _
                  txtPhone1.Text.Trim, txtPhone2.Text.Trim, txtMobile.Text.Trim, txtFax.Text.Trim, txtEmail.Text.Trim, txtURL.Text.Trim, "", AccType, GLaccType, _
                  "", txtRemarks.Text.Trim, txtNricNo.Text.Trim, txtPassportNo.Text.Trim, DOB, cboCustomerType.Text.Trim, cboDateType.Text.Trim, sGender, DOA) Then

                    gsConBatchPosting.ConnectionString = gsCon
                    gsConBatchPosting.Open()
                    gsTrans = gsConBatchPosting.BeginTransaction

                    If objCustomer.CustomerImageSave(txtCustomerCode.Text.Trim, _
                   ByteArr, picImage.Name, sUserID) Then
                        gsTrans.Commit()
                    Else
                        MsgBox("Transaction not saved", MsgBoxStyle.Information)
                        'TransactionRollBack(oTconn)
                        gsTrans.Rollback()
                        Exit Sub
                    End If


                    gsConBatchPosting.Close()

                    MessageBox.Show("Customer detail saved sucssfully.")
                    ClearMe()
                    txtCustomerCode.Focus()
                    'GetCustomerDetails()
                    'LoadCustomerDetails(Trim(GetText(grdCustomer, grdCustomer.ActiveRow, 1)))
                Else
                    MessageBox.Show("Error in Save")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Function EmailAddressCheck(ByVal emailAddress As String) As Boolean

        Dim pattern As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        Dim emailAddressMatch As Match = Regex.Match(emailAddress, pattern)
        If emailAddressMatch.Success Then
            EmailAddressCheck = True
        Else
            EmailAddressCheck = False
        End If
    End Function
    Private Function ValidateMe() As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            If txtCustomerCode.Text.Trim = "" Then
                MessageBox.Show("CustomerCode should not be blank.")
                txtCustomerCode.Focus()
                Exit Function
            End If
            If txtCustomerName.Text.Trim = "" Then
                MessageBox.Show("CustomerName should not be blank.")
                txtCustomerName.Focus()
                Exit Function
            End If
            If Trim(sCompanyShortCode) = "AJ" Then
                If txtMobile.Text.Trim = "" Then
                    MessageBox.Show("Mobile Number should not be blank.")
                    txtMobile.Focus()
                    Exit Function
                End If
                If Trim(txtEmail.Text) <> "" Then
                    If EmailAddressCheck(txtEmail.Text) = False Then
                        MessageBox.Show("Enter Proper Email Address")
                        txtEmail.Focus()
                        Exit Function
                    End If
                End If
                If cmbNationality.Text.Trim = "" Then
                    MessageBox.Show("Nationality should not be blank.")
                    cmbNationality.Focus()
                    Exit Function
                End If
            End If
            If Trim(sCompanyShortCode) = "MJ" Then
                If txtPassportNo.Text.Trim = "" Then
                    MessageBox.Show("Passport Number should not be blank.")
                    txtPassportNo.Focus()
                    Exit Function
                End If
            End If

            'If IsDate(txtDD.Text.Trim & "/" & txtMM.Text.Trim & "/" & txtYYYY.Text.Trim) = False Then
            '    MessageBox.Show("Enter valid Date of Birth")
            '    txtDD.Focus()
            '    Exit Function
            'End If


            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            bStatus = False
        Finally
            ValidateMe = bStatus
        End Try

    End Function

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim i As Long
        'If txtSearch.Text.Trim <> "" Then
        '    For i = 1 To grdCustomer.MaxRows
        '        If Trim(UCase(GetText(grdCustomer, i, 2))) Like UCase(txtSearch.Text.Trim) & "*" Then
        '            grdCustomer.Row = i
        '            grdCustomer.Col = 1
        '            grdCustomer.Action = FPSpread.ActionConstants.ActionActiveCell
        '            Exit For
        '        End If
        '    Next
        'Else
        '    If grdCustomer.MaxRows > 0 Then
        '        grdCustomer.Row = 1
        '        grdCustomer.Col = 1
        '        grdCustomer.Action = FPSpread.ActionConstants.ActionActiveCell
        '    End If
        'End If
    End Sub

    Private Sub frmCustomer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F6
                btnNew_Click(sender, e)
            Case Keys.F7
                btnSave_Click(sender, e)
            Case Keys.F8
                If btnDelete.Enabled Then btnDelete_Click(sender, e)
            Case Keys.Escape
                btnClose_Click(sender, e)
            Case Keys.Enter
                SendKeys.Send("{Tab}")
            Case Keys.Up
                SendKeys.Send("+{Tab}")
            Case Keys.Down
                SendKeys.Send("{Tab}")
        End Select
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        'Dim objfrm As New frmCustomerMnt
        'objfrm.sCustomercode = ""
        'objfrm.ShowDialog()
        ClearMe()
        txtCustomerCode.Focus()
    End Sub
    Private Sub ClearMe()
        txtCustomerCode.Text = ""
        txtCustomerName.Clear()
        txtAddress1.Clear()
        txtAddress2.Clear()
        txtAddress3.Clear()
        cmbNationality.Text = ""
        txtPhone1.Clear()
        txtPhone2.Clear()
        txtMobile.Clear()
        txtFax.Clear()
        txtURL.Clear()
        txtEmail.Clear()
        txtDD.Clear()
        txtMM.Clear()
        txtYYYY.Clear()
        txtADD.Clear()
        txtAMM.Clear()
        txtAYYYY.Clear()
        txtNricNo.Clear()
        txtPassportNo.Clear()
        'txtTerms.Clear()
        'txtGLAccount1.Clear()
        'txtGLAccount2.Clear()
        'optGold.Checked = True
        txtRemarks.Clear()
        cboCustomerType.Text = ""
        cboDateType.Text = "BirthDay"
        btnDelete.Enabled = False
        lblTotalPoints.Text = 0
        lblRedeemedPoints.Text = 0
        lblBalancePoints.Text = 0
        txtCustomerCode.Enabled = True
        grdSalesSummary.RowCount = 0
        grdSalesDetail.RowCount = 0
        chkActive.Checked = True
        optNone.Checked = True
        picImage.Name = ""
        picImage.Image = Nothing
    End Sub



    Private Sub grdCustomer_ClickEvent(ByVal sender As System.Object, ByVal e As AxFPSpread._DSpreadEvents_ClickEvent)
        'Dim objfrm As New frmCustomerMnt
        'objfrm.sCustomercode = Trim(GetText(grdCustomer, grdCustomer.ActiveRow, 1))
        'objfrm.ShowDialog()
        'LoadCustomerDetails(Trim(GetText(grdCustomer, grdCustomer.ActiveRow, 1)))
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
    Public Sub LoadCustomerDetails(ByVal sCustCode As String)
        Try
            Dim dsCustomer As New DataSet
            Dim iCnt As Long
            dsCustomer = objCustomer.GetCustomerwithCodeHQ(sCustCode)
            If Not IsNothing(dsCustomer) Then
                If dsCustomer.Tables("Customer").Rows.Count > 0 Then



                    Dim dsInventory As New DataSet
                    Dim objCustomer As New clsCustomer
                    dsInventory = objCustomer.GetCustomerImage(Trim(txtCustomerCode.Text))
                    If Not IsNothing(dsInventory) Then
                        If dsInventory.Tables("Customer").Rows.Count > 0 Then
                            Byte2Image(PicImage2.Image, dsInventory.Tables("Customer").Rows(0)("CustomerImage"))
                        End If
                    End If

                    txtCustomerCode.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("CustomerCode") & "")
                    txtCustomerName.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("CustomerName") & "")
                    cboCustomerType.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("CustomerType") & "")
                    txtAddress1.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("Address1") & "")
                    txtAddress2.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("Address2") & "")
                    txtAddress3.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("Address3") & "")
                    ''Nationality
                    cmbNationality.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("Address4") & "")
                    txtPhone1.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("Phone1") & "")
                    txtPhone2.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("Phone2") & "")
                    txtMobile.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("Mobile") & "")
                    txtFax.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("Fax") & "")
                    txtEmail.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("Email") & "")
                    txtURL.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("URL") & "")
                    txtNricNo.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("NricNo") & "")
                    txtPassportNo.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("PassportNo") & "")
                    txtDD.Text = Mid(Trim(dsCustomer.Tables("Customer").Rows(0)("DOB") & ""), 1, 2)
                    txtMM.Text = Mid(Trim(dsCustomer.Tables("Customer").Rows(0)("DOB") & ""), 4, 2)
                    txtYYYY.Text = Mid(Trim(dsCustomer.Tables("Customer").Rows(0)("DOB") & ""), 7, 4)

                    txtADD.Text = Mid(Trim(dsCustomer.Tables("Customer").Rows(0)("DOA") & ""), 1, 2)
                    txtAMM.Text = Mid(Trim(dsCustomer.Tables("Customer").Rows(0)("DOA") & ""), 4, 2)
                    txtAYYYY.Text = Mid(Trim(dsCustomer.Tables("Customer").Rows(0)("DOA") & ""), 7, 4)
                    lblTotalPoints.Text = Val(dsCustomer.Tables("Customer").Rows(0)("TotalRewardPoints") & "")
                    lblRedeemedPoints.Text = Val(dsCustomer.Tables("Customer").Rows(0)("TotalRedeemedPoints") & "")
                    lblBalancePoints.Text = Val(dsCustomer.Tables("Customer").Rows(0)("TotalRewardPoints") & "") - Val(dsCustomer.Tables("Customer").Rows(0)("TotalRedeemedPoints") & "")

                    'txtGLAccount1.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("GLAccount1") & "")
                    'txtGLAccount2.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("GLAccount2") & "")
                    'txtTerms.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("Terms") & "")
                    txtRemarks.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("Remarks") & "")
                    cboDateType.Text = Trim(dsCustomer.Tables("Customer").Rows(0)("DateType") & "")
                    ''HAJ ADD gender on 28 Jan 2008
                    If UCase(Trim(dsCustomer.Tables("Customer").Rows(0)("Gender") & "")) = "M" Then
                        optMale.Checked = True
                    Else
                        optFemale.Checked = True
                    End If
                    'If Trim(dsCustomer.Tables("Customer").Rows(0)("AccountType")) = "G" Then
                    '    optGold.Checked = True
                    'ElseIf Trim(dsCustomer.Tables("Customer").Rows(0)("AccountType")) = "C" Then
                    '    optCash.Checked = True
                    'End If
                    If Trim(dsCustomer.Tables("Customer").Rows(0)("AccountType")) = "S" Then
                        optSMS.Checked = True
                    ElseIf Trim(dsCustomer.Tables("Customer").Rows(0)("AccountType")) = "P" Then
                        optPhone.Checked = True
                    Else
                        optNone.Checked = True
                    End If

                    If Trim(dsCustomer.Tables("Customer").Rows(0)("GLAccount1")) = "Y" Then
                        chkActive.Checked = True
                    ElseIf Trim(dsCustomer.Tables("Customer").Rows(0)("GLAccount1")) = "N" Then
                        chkActive.Checked = False
                    End If

                    SubLoadSales()
                    SubLoadCustomerPoints()
                    btnDelete.Enabled = True
                Else
                End If
                End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub SubLoadSales()
        Try
            Dim dsCustomer As New DataSet
            Dim iCnt As Long
            grdSalesSummary.RowCount = 0
            grdSalesDetail.RowCount = 0
            dsCustomer = objCustomer.GetSalesByCustomer(txtCustomerCode.Text.Trim)
            If Not IsNothing(dsCustomer) Then
                If dsCustomer.Tables("Customer").Rows.Count > 0 Then
                    For iCnt = 0 To dsCustomer.Tables("Customer").Rows.Count - 1
                        grdSalesSummary.Rows.Add()
                        fnSetdgvText(grdSalesSummary, iCnt, 0, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("InvoiceNo") & ""))
                        fnSetdgvText(grdSalesSummary, iCnt, 1, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("InvoiceDate") & ""))
                        fnSetdgvText(grdSalesSummary, iCnt, 2, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("TotalWeight") & ""))
                        fnSetdgvText(grdSalesSummary, iCnt, 3, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("TotalPCS") & ""))
                        fnSetdgvText(grdSalesSummary, iCnt, 4, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("NetTotal") & ""))

                    Next
                    SubLoadSalesDetail()
                End If
                btnDelete.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub SubLoadSalesDetail()
        Try
            Dim dsCustomer As New DataSet
            Dim iCnt As Long
            Dim InvoiceNo As String
            grdSalesDetail.RowCount = 0
            InvoiceNo = Trim(fnGetdgvText(grdSalesSummary, grdSalesSummary.CurrentRow.Index, 0))
            If InvoiceNo.Trim <> "" Then
                dsCustomer = objCustomer.GetSalesDetailByInvoiceNo(InvoiceNo.Trim)
                If Not IsNothing(dsCustomer) Then
                    If dsCustomer.Tables("Customer").Rows.Count > 0 Then

                        For iCnt = 0 To dsCustomer.Tables("Customer").Rows.Count - 1
                            grdSalesDetail.Rows.Add()
                            fnSetdgvText(grdSalesDetail, iCnt, 0, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("InventoryCode") & ""))
                            fnSetdgvText(grdSalesDetail, iCnt, 1, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("Description") & ""))
                            fnSetdgvText(grdSalesDetail, iCnt, 2, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("Weight") & ""))
                            fnSetdgvText(grdSalesDetail, iCnt, 3, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("PCS") & ""))
                            fnSetdgvText(grdSalesDetail, iCnt, 4, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("NetAmount") & ""))
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub SubLoadCustomerPoints()
        Try
            Dim dsCustomer As New DataSet
            Dim iCnt As Long
            grdCustomerPoints.RowCount = 0
            dsCustomer = objCustomer.GetRewardPointsByCustomer(txtCustomerCode.Text.Trim)
            If Not IsNothing(dsCustomer) Then
                If dsCustomer.Tables("Customer").Rows.Count > 0 Then
                    For iCnt = 0 To dsCustomer.Tables("Customer").Rows.Count - 1
                        grdCustomerPoints.Rows.Add()
                        fnSetdgvText(grdCustomerPoints, iCnt, 0, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("InvoiceNo") & ""))
                        fnSetdgvText(grdCustomerPoints, iCnt, 1, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("InvoiceDate") & ""))
                        fnSetdgvText(grdCustomerPoints, iCnt, 2, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("InvoiceAmount") & ""))
                        fnSetdgvText(grdCustomerPoints, iCnt, 3, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("RewardPoints") & ""))
                        fnSetdgvText(grdCustomerPoints, iCnt, 4, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("RedeemPoints") & ""))
                        fnSetdgvText(grdCustomerPoints, iCnt, 5, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("ExpiryDate") & ""))
                        fnSetdgvText(grdCustomerPoints, iCnt, 6, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("Remarks") & ""))

                        fnSetdgvText(grdCustomerPoints, iCnt, 7, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("Redeemed") & ""))
                        fnSetdgvText(grdCustomerPoints, iCnt, 8, Trim(dsCustomer.Tables("Customer").Rows(iCnt)("InvoiceRedeemedPoints") & ""))
                    Next
                End If
                btnDelete.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub GrdSalesSummary_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_ClickEvent)
        SubLoadSalesDetail()
    End Sub
    Private Function fncGetCustomerTran(ByVal sCode As String) As Boolean
        Dim DvCustomer() As DataRow
        Dim DM As New DataManager
        fncGetCustomerTran = False
        DvCustomer = DM.RowReturnProjExecNonQryWithDataRow("Select * from tblSalesHeader where CustomerCode=" & fncFormatSQLText(sCode))
        If DvCustomer.Length > 0 Then
            fncGetCustomerTran = True
        Else
            fncGetCustomerTran = False
        End If
    End Function

    Private Sub cboDateType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDateType.SelectedIndexChanged

    End Sub


    Private Sub btnCustomerCodeSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnCustomerCodeSearch.Click
        If Trim(sCompanyShortCode) = "RJ" Then
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCustomer", "R", 8, "CustomerName", "", "CustomerCode", "CustomerCode", 80, "CustomerName", "CustomerName", 180, "Address1", "Address1", 180, "Address2", "Address2", 180, "Address3", "Address3", 180, "Address4", "Nationality", 180, "Phone1", "Phone1", 180, "Mobile", "Mobile", 180)
            frmHelpClass.ShowDialog()
            txtCustomerCode.Text = frmHelpClass.Var1.Trim
            txtCustomerCode_LostFocus(sender, e)
            ' SendKeys.Send(("{TAB}"))
        Else
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCustomer", "R", 4, "CustomerName", "", "CustomerCode", "CustomerCode", 80, "CustomerName", "CustomerName", 180, "Address1", "Address1", 180, "Mobile", "Mobile", 180)
            frmHelpClass.ShowDialog()
            txtCustomerCode.Text = frmHelpClass.Var1.Trim
            txtCustomerCode_LostFocus(sender, e)
            'SendKeys.Send(("{TAB}"))
        End If
    End Sub

    Private Sub txtCustomerCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtCustomerCode.KeyDown
        If e.KeyCode = Keys.F1 Then
            If Trim(sCompanyShortCode) = "RJ" Then
                Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCustomer", "L", 8, "CustomerName", "", "CustomerCode", "CustomerCode", 80, "CustomerName", "CustomerName", 180, "Address1", "Address1", 180, "Address2", "Address2", 180, "Address3", "Address3", 180, "Address4", "Nationality", 180, "Phone1", "Phone1", 180, "Mobile", "Mobile", 180)
                frmHelpClass.ShowDialog()
                txtCustomerCode.Text = frmHelpClass.Var1.Trim
            Else
                Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCustomer", "L", 4, "CustomerName", "", "CustomerCode", "CustomerCode", 80, "CustomerName", "CustomerName", 180, "Address1", "Address1", 180, "Address2", "Address2", 180)
                frmHelpClass.ShowDialog()
                txtCustomerCode.Text = frmHelpClass.Var1.Trim
            End If
        End If
    End Sub


    Private Sub txtCustomerCode_LostFocus(sender As Object, e As System.EventArgs) Handles txtCustomerCode.LostFocus
        If txtCustomerCode.Text.Trim <> "" Then
            txtCustomerCode.Enabled = False
            LoadCustomerDetails(txtCustomerCode.Text.Trim)
        End If
    End Sub
    Private Sub subAddNationality()
        Dim DrM As DataRow()
        Dim Dm As New DataManager
        Dim i As Integer
        DrM = Dm.RowReturnProjExecNonQryWithDataRow("Select * from tblNationality order by description")
        cmbNationality.Items.Clear()
        If DrM.Length > 0 Then
            For i = 0 To DrM.Length - 1
                cmbNationality.Items.Add(Trim(DrM(i)("Description") & ""))
            Next
        End If
        '''If cmbNationality.Items.Count > 0 Then
        '''    cmbNationality.SelectedIndex = 0
        '''End If
    End Sub
    Private Sub addCboReligion()
        cboDateType.Items.Add("")
        cboDateType.Items.Add("HINDU")
        cboDateType.Items.Add("MUSLIM")
        cboDateType.Items.Add("CHRISTIAN")
        cboDateType.Items.Add("CHINESE")
        cboDateType.SelectedIndex = 0
    End Sub


    Private Sub grdSalesSummary_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSalesSummary.CellClick
        SubLoadSalesDetail()
    End Sub

    Private Sub grdSalesSummary_CellDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSalesSummary.CellDoubleClick
        SubLoadSalesDetail()
    End Sub


    Private Sub btnBrowseImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseImage.Click
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

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        grpImage.Visible = False
    End Sub
    Private Sub chkKYC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkKYC.CheckedChanged
        If chkKYC.Checked = True Then
            grpImage.Visible = True
        Else
            grpImage.Visible = False
        End If
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

    Private Sub chkShowKYC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowKYC.CheckedChanged
        If chkShowKYC.Checked = True Then
            grpImage2.Visible = True
        Else
            grpImage2.Visible = False
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        grpImage2.Visible = False
    End Sub
End Class

