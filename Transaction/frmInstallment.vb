Public Class frmInstallment
    Inherits System.Windows.Forms.Form
    Dim objMember As New clsMember
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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblDepositAmount As System.Windows.Forms.Label
    Friend WithEvents lblBalanceAmount As System.Windows.Forms.Label
    Friend WithEvents txtInvoiceNo As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerCode As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress1 As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress2 As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress3 As System.Windows.Forms.TextBox
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents btnSAve As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents txtIYYYY As System.Windows.Forms.TextBox
    Friend WithEvents txtIMM As System.Windows.Forms.TextBox
    Friend WithEvents txtIDD As System.Windows.Forms.TextBox
    Friend WithEvents txtInstallmentAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtNoofInstallment As System.Windows.Forms.TextBox
    Friend WithEvents txtDepositAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lblInstallmentAmount As System.Windows.Forms.Label
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents lblSalesAmount As System.Windows.Forms.Label
    Friend WithEvents lblCustomerType As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents pnlAdminPwd As System.Windows.Forms.Panel
    Friend WithEvents btnAdminPwdCancel As System.Windows.Forms.Button
    Friend WithEvents btnAdminPwdOK As System.Windows.Forms.Button
    Friend WithEvents txtAdminPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtMemberCode As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmInstallment))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtMemberCode = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.pnlAdminPwd = New System.Windows.Forms.Panel
        Me.btnAdminPwdCancel = New System.Windows.Forms.Button
        Me.btnAdminPwdOK = New System.Windows.Forms.Button
        Me.txtAdminPassword = New System.Windows.Forms.TextBox
        Me.Label51 = New System.Windows.Forms.Label
        Me.Label52 = New System.Windows.Forms.Label
        Me.lblCustomerType = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtDepositAmount = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtNoofInstallment = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtInstallmentAmount = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtIYYYY = New System.Windows.Forms.TextBox
        Me.txtIMM = New System.Windows.Forms.TextBox
        Me.txtIDD = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtRemarks = New System.Windows.Forms.TextBox
        Me.txtAddress3 = New System.Windows.Forms.TextBox
        Me.txtAddress2 = New System.Windows.Forms.TextBox
        Me.txtAddress1 = New System.Windows.Forms.TextBox
        Me.txtCustomerName = New System.Windows.Forms.TextBox
        Me.txtCustomerCode = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lblSalesAmount = New System.Windows.Forms.Label
        Me.lblInstallmentAmount = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.lblDepositAmount = New System.Windows.Forms.Label
        Me.lblBalanceAmount = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.btnSAve = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.pic = New System.Windows.Forms.PictureBox
        Me.Panel1.SuspendLayout()
        Me.pnlAdminPwd.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txtMemberCode)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.pnlAdminPwd)
        Me.Panel1.Controls.Add(Me.lblCustomerType)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.txtDepositAmount)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.txtNoofInstallment)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.txtInstallmentAmount)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txtIYYYY)
        Me.Panel1.Controls.Add(Me.txtIMM)
        Me.Panel1.Controls.Add(Me.txtIDD)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtRemarks)
        Me.Panel1.Controls.Add(Me.txtAddress3)
        Me.Panel1.Controls.Add(Me.txtAddress2)
        Me.Panel1.Controls.Add(Me.txtAddress1)
        Me.Panel1.Controls.Add(Me.txtCustomerName)
        Me.Panel1.Controls.Add(Me.txtCustomerCode)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtInvoiceNo)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Location = New System.Drawing.Point(5, 80)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(658, 208)
        Me.Panel1.TabIndex = 0
        '
        'txtMemberCode
        '
        Me.txtMemberCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMemberCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMemberCode.ForeColor = System.Drawing.Color.Black
        Me.txtMemberCode.Location = New System.Drawing.Point(232, 48)
        Me.txtMemberCode.MaxLength = 20
        Me.txtMemberCode.Name = "txtMemberCode"
        Me.txtMemberCode.ReadOnly = True
        Me.txtMemberCode.Size = New System.Drawing.Size(192, 20)
        Me.txtMemberCode.TabIndex = 131
        Me.txtMemberCode.Tag = ""
        Me.txtMemberCode.Text = ""
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(8, 80)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(82, 15)
        Me.Label14.TabIndex = 130
        Me.Label14.Text = "Name"
        '
        'pnlAdminPwd
        '
        Me.pnlAdminPwd.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(192, Byte))
        Me.pnlAdminPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlAdminPwd.Controls.Add(Me.btnAdminPwdCancel)
        Me.pnlAdminPwd.Controls.Add(Me.btnAdminPwdOK)
        Me.pnlAdminPwd.Controls.Add(Me.txtAdminPassword)
        Me.pnlAdminPwd.Controls.Add(Me.Label51)
        Me.pnlAdminPwd.Controls.Add(Me.Label52)
        Me.pnlAdminPwd.Location = New System.Drawing.Point(160, 80)
        Me.pnlAdminPwd.Name = "pnlAdminPwd"
        Me.pnlAdminPwd.Size = New System.Drawing.Size(246, 88)
        Me.pnlAdminPwd.TabIndex = 129
        Me.pnlAdminPwd.Visible = False
        '
        'btnAdminPwdCancel
        '
        Me.btnAdminPwdCancel.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnAdminPwdCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdminPwdCancel.Image = CType(resources.GetObject("btnAdminPwdCancel.Image"), System.Drawing.Image)
        Me.btnAdminPwdCancel.Location = New System.Drawing.Point(160, 56)
        Me.btnAdminPwdCancel.Name = "btnAdminPwdCancel"
        Me.btnAdminPwdCancel.Size = New System.Drawing.Size(64, 23)
        Me.btnAdminPwdCancel.TabIndex = 75
        Me.btnAdminPwdCancel.Text = "Cancel"
        '
        'btnAdminPwdOK
        '
        Me.btnAdminPwdOK.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnAdminPwdOK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdminPwdOK.Image = CType(resources.GetObject("btnAdminPwdOK.Image"), System.Drawing.Image)
        Me.btnAdminPwdOK.Location = New System.Drawing.Point(88, 56)
        Me.btnAdminPwdOK.Name = "btnAdminPwdOK"
        Me.btnAdminPwdOK.Size = New System.Drawing.Size(64, 23)
        Me.btnAdminPwdOK.TabIndex = 74
        Me.btnAdminPwdOK.Text = "OK"
        '
        'txtAdminPassword
        '
        Me.txtAdminPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdminPassword.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdminPassword.ForeColor = System.Drawing.Color.Black
        Me.txtAdminPassword.Location = New System.Drawing.Point(88, 32)
        Me.txtAdminPassword.MaxLength = 20
        Me.txtAdminPassword.Name = "txtAdminPassword"
        Me.txtAdminPassword.PasswordChar = Microsoft.VisualBasic.ChrW(42)
        Me.txtAdminPassword.Size = New System.Drawing.Size(136, 20)
        Me.txtAdminPassword.TabIndex = 73
        Me.txtAdminPassword.Tag = ""
        Me.txtAdminPassword.Text = ""
        '
        'Label51
        '
        Me.Label51.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.ForeColor = System.Drawing.Color.Black
        Me.Label51.Location = New System.Drawing.Point(16, 33)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(64, 15)
        Me.Label51.TabIndex = 72
        Me.Label51.Text = "Password"
        '
        'Label52
        '
        Me.Label52.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.ForeColor = System.Drawing.Color.Black
        Me.Label52.Location = New System.Drawing.Point(8, 8)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(56, 15)
        Me.Label52.TabIndex = 72
        Me.Label52.Text = "Admin"
        Me.Label52.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCustomerType
        '
        Me.lblCustomerType.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.lblCustomerType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustomerType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerType.ForeColor = System.Drawing.Color.Red
        Me.lblCustomerType.Location = New System.Drawing.Point(432, 48)
        Me.lblCustomerType.Name = "lblCustomerType"
        Me.lblCustomerType.Size = New System.Drawing.Size(216, 20)
        Me.lblCustomerType.TabIndex = 128
        Me.lblCustomerType.Text = "Customer Type"
        Me.lblCustomerType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(8, 112)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(82, 15)
        Me.Label13.TabIndex = 100
        Me.Label13.Text = "Address"
        '
        'txtDepositAmount
        '
        Me.txtDepositAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDepositAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepositAmount.ForeColor = System.Drawing.Color.Black
        Me.txtDepositAmount.Location = New System.Drawing.Point(547, 80)
        Me.txtDepositAmount.MaxLength = 20
        Me.txtDepositAmount.Name = "txtDepositAmount"
        Me.txtDepositAmount.Size = New System.Drawing.Size(101, 20)
        Me.txtDepositAmount.TabIndex = 9
        Me.txtDepositAmount.Tag = ""
        Me.txtDepositAmount.Text = ""
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(435, 80)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(96, 15)
        Me.Label9.TabIndex = 99
        Me.Label9.Text = "Deposit Amount"
        '
        'txtNoofInstallment
        '
        Me.txtNoofInstallment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNoofInstallment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoofInstallment.ForeColor = System.Drawing.Color.Black
        Me.txtNoofInstallment.Location = New System.Drawing.Point(547, 112)
        Me.txtNoofInstallment.MaxLength = 20
        Me.txtNoofInstallment.Name = "txtNoofInstallment"
        Me.txtNoofInstallment.Size = New System.Drawing.Size(101, 20)
        Me.txtNoofInstallment.TabIndex = 10
        Me.txtNoofInstallment.Tag = ""
        Me.txtNoofInstallment.Text = ""
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(435, 112)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(104, 15)
        Me.Label8.TabIndex = 97
        Me.Label8.Text = "No. of Installment"
        '
        'txtInstallmentAmount
        '
        Me.txtInstallmentAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInstallmentAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInstallmentAmount.ForeColor = System.Drawing.Color.Black
        Me.txtInstallmentAmount.Location = New System.Drawing.Point(547, 144)
        Me.txtInstallmentAmount.MaxLength = 20
        Me.txtInstallmentAmount.Name = "txtInstallmentAmount"
        Me.txtInstallmentAmount.Size = New System.Drawing.Size(101, 20)
        Me.txtInstallmentAmount.TabIndex = 11
        Me.txtInstallmentAmount.Tag = ""
        Me.txtInstallmentAmount.Text = ""
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(435, 144)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(104, 15)
        Me.Label6.TabIndex = 95
        Me.Label6.Text = "Installment Amout"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(608, 176)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(8, 16)
        Me.Label3.TabIndex = 93
        Me.Label3.Text = "-"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(573, 176)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(8, 16)
        Me.Label4.TabIndex = 92
        Me.Label4.Text = "-"
        '
        'txtIYYYY
        '
        Me.txtIYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIYYYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIYYYY.Location = New System.Drawing.Point(616, 176)
        Me.txtIYYYY.MaxLength = 4
        Me.txtIYYYY.Name = "txtIYYYY"
        Me.txtIYYYY.Size = New System.Drawing.Size(32, 20)
        Me.txtIYYYY.TabIndex = 14
        Me.txtIYYYY.Tag = "ENN;"
        Me.txtIYYYY.Text = ""
        '
        'txtIMM
        '
        Me.txtIMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIMM.Location = New System.Drawing.Point(584, 176)
        Me.txtIMM.MaxLength = 2
        Me.txtIMM.Name = "txtIMM"
        Me.txtIMM.Size = New System.Drawing.Size(21, 20)
        Me.txtIMM.TabIndex = 13
        Me.txtIMM.Tag = "ENN;"
        Me.txtIMM.Text = ""
        '
        'txtIDD
        '
        Me.txtIDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIDD.Location = New System.Drawing.Point(547, 176)
        Me.txtIDD.MaxLength = 2
        Me.txtIDD.Name = "txtIDD"
        Me.txtIDD.Size = New System.Drawing.Size(21, 20)
        Me.txtIDD.TabIndex = 12
        Me.txtIDD.Tag = "ENN;"
        Me.txtIDD.Text = ""
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(435, 176)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 20)
        Me.Label7.TabIndex = 91
        Me.Label7.Text = "Last Date"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(8, 181)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 15)
        Me.Label1.TabIndex = 71
        Me.Label1.Text = "Remarks"
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.Black
        Me.txtRemarks.Location = New System.Drawing.Point(104, 176)
        Me.txtRemarks.MaxLength = 20
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(320, 20)
        Me.txtRemarks.TabIndex = 8
        Me.txtRemarks.Tag = ""
        Me.txtRemarks.Text = ""
        '
        'txtAddress3
        '
        Me.txtAddress3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress3.ForeColor = System.Drawing.Color.Black
        Me.txtAddress3.Location = New System.Drawing.Point(288, 144)
        Me.txtAddress3.MaxLength = 20
        Me.txtAddress3.Name = "txtAddress3"
        Me.txtAddress3.Size = New System.Drawing.Size(136, 20)
        Me.txtAddress3.TabIndex = 7
        Me.txtAddress3.Tag = ""
        Me.txtAddress3.Text = ""
        '
        'txtAddress2
        '
        Me.txtAddress2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress2.ForeColor = System.Drawing.Color.Black
        Me.txtAddress2.Location = New System.Drawing.Point(104, 144)
        Me.txtAddress2.MaxLength = 20
        Me.txtAddress2.Name = "txtAddress2"
        Me.txtAddress2.ReadOnly = True
        Me.txtAddress2.Size = New System.Drawing.Size(176, 20)
        Me.txtAddress2.TabIndex = 6
        Me.txtAddress2.Tag = ""
        Me.txtAddress2.Text = ""
        '
        'txtAddress1
        '
        Me.txtAddress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress1.ForeColor = System.Drawing.Color.Black
        Me.txtAddress1.Location = New System.Drawing.Point(104, 112)
        Me.txtAddress1.MaxLength = 20
        Me.txtAddress1.Name = "txtAddress1"
        Me.txtAddress1.ReadOnly = True
        Me.txtAddress1.Size = New System.Drawing.Size(320, 20)
        Me.txtAddress1.TabIndex = 5
        Me.txtAddress1.Tag = ""
        Me.txtAddress1.Text = ""
        '
        'txtCustomerName
        '
        Me.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerName.ForeColor = System.Drawing.Color.Black
        Me.txtCustomerName.Location = New System.Drawing.Point(104, 80)
        Me.txtCustomerName.MaxLength = 20
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.ReadOnly = True
        Me.txtCustomerName.Size = New System.Drawing.Size(192, 20)
        Me.txtCustomerName.TabIndex = 4
        Me.txtCustomerName.Tag = ""
        Me.txtCustomerName.Text = ""
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerCode.ForeColor = System.Drawing.Color.Black
        Me.txtCustomerCode.Location = New System.Drawing.Point(104, 48)
        Me.txtCustomerCode.MaxLength = 20
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.ReadOnly = True
        Me.txtCustomerCode.Size = New System.Drawing.Size(120, 20)
        Me.txtCustomerCode.TabIndex = 2
        Me.txtCustomerCode.Tag = ""
        Me.txtCustomerCode.Text = ""
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(8, 48)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 15)
        Me.Label5.TabIndex = 65
        Me.Label5.Text = "Customer Code"
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvoiceNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceNo.ForeColor = System.Drawing.Color.Black
        Me.txtInvoiceNo.Location = New System.Drawing.Point(104, 16)
        Me.txtInvoiceNo.MaxLength = 20
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.ReadOnly = True
        Me.txtInvoiceNo.Size = New System.Drawing.Size(120, 20)
        Me.txtInvoiceNo.TabIndex = 1
        Me.txtInvoiceNo.Tag = ""
        Me.txtInvoiceNo.Text = ""
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(8, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 15)
        Me.Label2.TabIndex = 49
        Me.Label2.Text = "Invoice No"
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.lblSalesAmount)
        Me.Panel2.Controls.Add(Me.lblInstallmentAmount)
        Me.Panel2.Controls.Add(Me.Label15)
        Me.Panel2.Controls.Add(Me.lblDepositAmount)
        Me.Panel2.Controls.Add(Me.lblBalanceAmount)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Location = New System.Drawing.Point(5, 288)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(658, 66)
        Me.Panel2.TabIndex = 1
        '
        'lblSalesAmount
        '
        Me.lblSalesAmount.BackColor = System.Drawing.Color.LightCyan
        Me.lblSalesAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSalesAmount.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesAmount.ForeColor = System.Drawing.Color.Black
        Me.lblSalesAmount.Location = New System.Drawing.Point(0, 32)
        Me.lblSalesAmount.Name = "lblSalesAmount"
        Me.lblSalesAmount.Size = New System.Drawing.Size(168, 32)
        Me.lblSalesAmount.TabIndex = 80
        Me.lblSalesAmount.Text = "0.00"
        Me.lblSalesAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblInstallmentAmount
        '
        Me.lblInstallmentAmount.BackColor = System.Drawing.Color.LightCyan
        Me.lblInstallmentAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblInstallmentAmount.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInstallmentAmount.ForeColor = System.Drawing.Color.Black
        Me.lblInstallmentAmount.Location = New System.Drawing.Point(496, 32)
        Me.lblInstallmentAmount.Name = "lblInstallmentAmount"
        Me.lblInstallmentAmount.Size = New System.Drawing.Size(160, 32)
        Me.lblInstallmentAmount.TabIndex = 79
        Me.lblInstallmentAmount.Text = "0.00"
        Me.lblInstallmentAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Pink
        Me.Label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(496, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(160, 32)
        Me.Label15.TabIndex = 78
        Me.Label15.Text = "Installment Amount"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDepositAmount
        '
        Me.lblDepositAmount.BackColor = System.Drawing.Color.LightCyan
        Me.lblDepositAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDepositAmount.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepositAmount.ForeColor = System.Drawing.Color.Black
        Me.lblDepositAmount.Location = New System.Drawing.Point(168, 32)
        Me.lblDepositAmount.Name = "lblDepositAmount"
        Me.lblDepositAmount.Size = New System.Drawing.Size(168, 32)
        Me.lblDepositAmount.TabIndex = 77
        Me.lblDepositAmount.Text = "0.00"
        Me.lblDepositAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBalanceAmount
        '
        Me.lblBalanceAmount.BackColor = System.Drawing.Color.LightCyan
        Me.lblBalanceAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBalanceAmount.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalanceAmount.ForeColor = System.Drawing.Color.Black
        Me.lblBalanceAmount.Location = New System.Drawing.Point(336, 32)
        Me.lblBalanceAmount.Name = "lblBalanceAmount"
        Me.lblBalanceAmount.Size = New System.Drawing.Size(160, 32)
        Me.lblBalanceAmount.TabIndex = 76
        Me.lblBalanceAmount.Text = "0.00"
        Me.lblBalanceAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Pink
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label12.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(168, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(168, 32)
        Me.Label12.TabIndex = 74
        Me.Label12.Text = "Deposit Amount"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Pink
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(336, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(160, 32)
        Me.Label11.TabIndex = 73
        Me.Label11.Text = "Balance Amount"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Pink
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(0, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(168, 32)
        Me.Label10.TabIndex = 72
        Me.Label10.Text = "Sales Amount"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnSAve
        '
        Me.btnSAve.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnSAve.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnSAve.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSAve.Image = CType(resources.GetObject("btnSAve.Image"), System.Drawing.Image)
        Me.btnSAve.Location = New System.Drawing.Point(471, 368)
        Me.btnSAve.Name = "btnSAve"
        Me.btnSAve.Size = New System.Drawing.Size(94, 23)
        Me.btnSAve.TabIndex = 15
        Me.btnSAve.Text = "Save (F7)"
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(570, 368)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(92, 23)
        Me.btnClose.TabIndex = 17
        Me.btnClose.Text = "Close(Esc)"
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(371, 368)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(95, 23)
        Me.btnClear.TabIndex = 16
        Me.btnClear.Text = "Clear (F6)"
        Me.btnClear.Visible = False
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
        'frmInstallment
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(669, 400)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.btnSAve)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmInstallment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Installment"
        Me.Panel1.ResumeLayout(False)
        Me.pnlAdminPwd.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Public sInvoiceNo As String
    Public sCustomerCode As String
    Public sMemberCode As String
    Public sSalesAmount As Double
    Public sInvoiceDate As String
    Dim msInsAgreementNo As String
    Dim mbTransaction As Boolean
    Public sCalledFrom As String
    Dim bPassword As Boolean

    Dim objCust As New clsCustomer
    Dim objGPOS As New frmGPOS
    Dim objPM As New frmPayingmode
    Dim objIns As New ClsInstallment
    Dim objUM As New clsUserMasters

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If sCalledFrom = "POS" Then
            mbSalesTransaction = False
        End If
        Me.Close()
    End Sub

    Private Sub frmInstallment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            txtInvoiceNo.Text = Trim(sInvoiceNo)
            If Len(txtCustomerCode.Text) > 0 Then
                If UCase(txtCustomerCode.Text.Trim) = "CASH" And sMemberModule = "1" Then
                    GetMemberDetail()
                ElseIf UCase(txtCustomerCode.Text.Trim) = "CASH" And sMemberModule = "0" Then
                    MessageBox.Show("Installment Sales Not For CASH Customer!!!")
                    Me.Close()
                Else
                    GetCustomerDetail()
                End If
            End If
            lblSalesAmount.Text = Format(Val(sSalesAmount), "0.00")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub GetCustomerDetail()
        Dim dsCust As New DataSet
        Try
            If txtCustomerCode.Text.Trim <> "" Then
                sCustomerCode = Trim(txtCustomerCode.Text)
                dsCust = objCust.GetCustomerwithCode(txtCustomerCode.Text.Trim)
                If dsCust.Tables(0).Rows.Count > 0 Then
                    txtCustomerName.Text = Trim(dsCust.Tables(0).Rows(0)("CustomerName") & "")
                    lblCustomerType.Text = Trim(dsCust.Tables(0).Rows(0)("CustomerType") & "")
                    txtAddress1.Text = Trim(dsCust.Tables(0).Rows(0)("Address1") & "")
                    txtAddress2.Text = Trim(dsCust.Tables(0).Rows(0)("Address2") & "")
                    txtAddress3.Text = Trim(dsCust.Tables(0).Rows(0)("Address3") & "")
                    lblCustomerType.Text = Trim(dsCust.Tables(0).Rows(0)("CustomerType") & "")
                Else
                    MessageBox.Show("Customer code not valid.")
                    txtCustomerCode.Clear()
                    txtCustomerCode.Focus()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub GetMemberDetail()
        Dim dsCust As New DataSet
        Try
            If UCase(txtCustomerCode.Text.Trim) = "CASH" Then
                dsCust = objMember.GetMemberwithCode(txtMemberCode.Text.Trim)
                If dsCust.Tables(0).Rows.Count > 0 Then
                    txtCustomerName.Text = Trim(dsCust.Tables(0).Rows(0)("MemberName") & "")
                    lblCustomerType.Text = Trim(dsCust.Tables(0).Rows(0)("MemberType") & "")
                    txtAddress1.Text = Trim(dsCust.Tables(0).Rows(0)("Address1") & "")
                    txtAddress2.Text = Trim(dsCust.Tables(0).Rows(0)("Address2") & "")
                    txtAddress3.Text = Trim(dsCust.Tables(0).Rows(0)("Address3") & "")
                    lblCustomerType.Text = Trim(dsCust.Tables(0).Rows(0)("MemberType") & "")
                Else
                    MessageBox.Show("Member code not valid.")
                    txtMemberCode.Clear()
                    txtMemberCode.Focus()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub frmInstallment_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Escape
                    Me.Close()
                Case Keys.Enter
                    SendKeys.Send("{TAB}")
                Case Keys.F6
                    btnClear_Click(sender, e)
                Case Keys.F7
                    btnSave_Click(sender, e)
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSAve.Click
        Try
            mbSalesTransaction = False
            If fncCheckValidity() Then
                nTotalNet = Val(txtDepositAmount.Text)
                objPM.SalesType = ""
                objPM.ShowDialog()
                If dPaidAmount > 0 Then
                    If Not fncSaveInstallment() Then
                        MessageBox.Show("Transaction not saved")
                        mbSalesTransaction = False
                    Else
                        mbSalesTransaction = True
                    End If
                    Me.Close()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function fncCheckValidity() As Boolean
        Dim bstatus As Boolean
        Try
            bstatus = False
            If UCase(txtCustomerCode.Text) = "CASH" Then
                If txtMemberCode.Text.Trim = "" Then
                    MsgBox("MemberCode is NOT entered !", MsgBoxStyle.Critical)
                    txtCustomerCode.Focus()
                    Exit Function
                End If
            End If
            If Not IsNumeric(txtDepositAmount.Text) Then
                MsgBox("Deposit Amount should be numeric!")
                txtDepositAmount.Focus()
                Exit Function
            ElseIf Val(txtDepositAmount.Text) = 0 Then
                MsgBox("Deposit Amount should be greater than zero!")
                txtDepositAmount.Focus()
                Exit Function
            ElseIf Val(txtDepositAmount.Text) > Val(sSalesAmount) Then
                MsgBox("Deposit Amount should be lower than the Sales Amount!")
                txtDepositAmount.Focus()
                Exit Function
            ElseIf Not IsNumeric(txtNoofInstallment.Text) Then
                MsgBox("No. of Installment should be numeric!")
                txtNoofInstallment.Focus()
                Exit Function
            ElseIf Val(txtNoofInstallment.Text) = 0 Then
                MsgBox("No. of Installment should be greater than zero!")
                txtNoofInstallment.Focus()
                Exit Function
            ElseIf Val(txtInstallmentAmount.Text) <= 0 Then
                MsgBox("Installment Amount should be Greater than Zero!")
                txtInstallmentAmount.Focus()
                Exit Function
            ElseIf Not IsDate(Trim(txtIDD.Text & "/" & txtIMM.Text & "/" & txtIYYYY.Text)) Then
                MsgBox("Last Date is Invalid!")
                fncCheckValidity = False
                txtIDD.Focus()
                Exit Function
            End If
            bstatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncCheckValidity = bstatus
        End Try
    End Function

    Private Function fncSaveInstallment() As Boolean
        Dim bstatus As Boolean
        Dim dsG As New DataSet
        Dim iRet As Boolean
        Dim iCnt As Long
        Dim sSql As String
        bstatus = False
        objIns.subOpenInsConnection()
        Try
            objIns.subBeginInsTransaction()

            sInvoiceNo = "" 
            'get the next InstallmentAgreement Number.
            If Not fncGetInsAgreement() Then
                objIns.subRollBackInsTransaction()
                Exit Function
            End If
            If Not objIns.fncUpdateInsAgreement() Then
                objIns.subRollBackInsTransaction()
                Exit Function
            End If
            iRet = objIns.InstallmentDelete(Trim(msInsAgreementNo))
            If Not iRet Then
                objIns.subRollBackInsTransaction()
                Exit Function
            End If

            iRet = objIns.InstallmentHeaderSave(Trim(msInsAgreementNo), Trim(sInvoiceNo), gthisLocation, _
                                UCase(Trim(txtCustomerCode.Text)), Trim(txtMemberCode.Text), Trim(txtCustomerName.Text), Trim(txtAddress1.Text), _
                                Trim(txtAddress2.Text), Trim(txtAddress3.Text), Trim(sManCode), Val(txtDepositAmount.Text), Val(txtNoofInstallment.Text), 0, _
                                 Val(txtInstallmentAmount.Text), 0, Val(sSalesAmount), "INSTALLMENT")

            If Not iRet Then
                objIns.subRollBackInsTransaction()
                Exit Function
            End If
            objIns.subCommitInsTransaction()
            objGPOS.InsAgreementNo = msInsAgreementNo
            objGPOS.DepositAmt = Val(txtDepositAmount.Text)
            gsInsAgreementNo = msInsAgreementNo
            gdDepositAmt = Val(txtDepositAmount.Text)
            bstatus = True
        Catch ex As Exception
            objIns.subRollBackInsTransaction()
            MessageBox.Show(ex.Message)
        Finally
            objIns.subCloseInsConnection()
            fncSaveInstallment = bstatus
        End Try
    End Function

    Private Function fncGetInsAgreement() As Boolean
        Dim dsIns As New DataSet
        Dim bStatus As Boolean
        Try
            bStatus = False
            dsIns = objIns.GetUpdateInsAgreement()
            If dsIns.Tables(0).Rows.Count > 0 Then
                msInsAgreementNo = "IA-" & Trim(dsIns.Tables(0).Rows(0)("NextInsAgreementNo") & "")
            End If
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncGetInsAgreement = bStatus
        End Try
    End Function

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        subClearDetail()
    End Sub
    Private Sub subClearDetail()
        Try
            txtCustomerCode.Clear()
            txtMemberCode.Text = ""
            lblCustomerType.Text = "Type"
            txtCustomerName.Clear()
            txtAddress1.Clear()
            txtAddress2.Clear()
            txtAddress3.Clear()
            txtDepositAmount.Clear()
            txtNoofInstallment.Clear()
            txtInstallmentAmount.Clear()
            txtRemarks.Clear()
            txtIDD.Clear()
            txtIMM.Clear()
            txtIYYYY.Clear()
            lblBalanceAmount.Text = lblSalesAmount.Text
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subUnloadForm()
        Try
            If Len(objGPOS.txtCustomerName.Text) = 0 And Len(txtCustomerName.Text) <> 0 Then
                objGPOS.txtCustomerName.Text = Trim(txtCustomerName.Text)
            End If

            If Len(objGPOS.lblCustomerType.Text) = 0 And Len(lblCustomerType.Text) <> 0 Then
                objGPOS.lblCustomerType.Text = Trim(lblCustomerType.Text)
            End If

            If Len(objGPOS.txtAddress1.Text) = 0 And Len(txtAddress1.Text) <> 0 Then
                objGPOS.txtAddress1.Text = Trim(txtAddress1.Text)
            End If

            If Len(objGPOS.txtAddress2.Text) = 0 And Len(txtAddress2.Text) <> 0 Then
                objGPOS.txtAddress2.Text = Trim(txtAddress2.Text)
            End If

            If Len(objGPOS.txtAddress3.Text) = 0 And Len(txtAddress3.Text) <> 0 Then
                objGPOS.txtAddress3.Text = Trim(txtAddress3.Text)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmInstallment_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.VisibleChanged
        subUnloadForm()
    End Sub

    Private Sub frmInstallment_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        subUnloadForm()
    End Sub

    Private Sub subFillDate(ByVal InsNo As String)
        Dim InsDate As Date
        Try
            If Len(InsNo) <> 0 Then
                InsDate = DateAdd("m", Val(InsNo), Now())
            End If
            If IsDate(InsDate) Then
                txtIDD.Text = Format(InsDate, "dd")
                txtIMM.Text = Format(Month(InsDate), "00")
                txtIYYYY.Text = Format(Year(InsDate), "0000")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtNoofInstallment_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNoofInstallment.Leave
        Try
            subGetInsAmt()
            subFillDate(Val(txtNoofInstallment.Text))
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subGetInsAmt()
        Try
            If Val(txtDepositAmount.Text) > 0 And Val(txtNoofInstallment.Text) > 0 Then
                txtInstallmentAmount.Text = Format$((Val(sSalesAmount) - Val(txtDepositAmount.Text)) / Val(txtNoofInstallment.Text), "0.00")
                lblInstallmentAmount.Text = Format(Val(txtInstallmentAmount.Text), "0.00")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtInstallmentAmount_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInstallmentAmount.Leave
        Try
            fncGetTotalInstallment()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub fncGetTotalInstallment()
        Try
            Dim dInst As Double
            If Val(txtDepositAmount.Text) > 0 Then
                dInst = Format(Val(lblSalesAmount.Text - txtDepositAmount.Text) / txtInstallmentAmount.Text, "#0.00")
                dInst = Math.Round(dInst)
                txtNoofInstallment.Text = dInst
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtDepositAmount_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDepositAmount.Leave
        Dim sMsg As String
        Try
            If Val(txtDepositAmount.Text) > 0 Then
                txtDepositAmount.Text = Format$(Val(txtDepositAmount.Text), "0.00")
                lblDepositAmount.Text = Format$(Val(txtDepositAmount.Text), "0.00")
                lblBalanceAmount.Text = Format$(Val(lblSalesAmount.Text) - Val(lblDepositAmount.Text), "0.00")
            Else
                lblDepositAmount.Text = ""
                lblBalanceAmount.Text = lblSalesAmount.Text
            End If
            If Val(txtDepositAmount.Text) < ((Val(lblSalesAmount.Text)) * 0.3) Then

                sMsg = MsgBox("The Deposit amount Should be 30% Of Sales Amount , Do you want to proceed with negative profit? ", vbYesNo)
                If sMsg = vbYes Then
                    subGetAdminPasword()
                Else
                    txtDepositAmount.Focus()
                    pnlAdminPwd.Visible = False
                    Exit Sub
                End If
                Exit Sub
            End If
            subGetInsAmt()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subGetAdminPasword()
        Try
            pnlAdminPwd.Visible = True
            txtAdminPassword.Clear()
            txtAdminPassword.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnAdminPwdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdminPwdCancel.Click
        Try
            txtAdminPassword.Clear()
            pnlAdminPwd.Visible = False
            bPassword = False
            txtDepositAmount.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnAdminPwdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdminPwdOK.Click
        Dim dsPWD As New DataSet
        Try
            If Trim(txtAdminPassword.Text) <> "" Then
                dsPWD = objUM.GetAdminPassword(Trim(txtAdminPassword.Text))
                If dsPWD.Tables(0).Rows.Count > 0 Then
                    If Trim(UCase(dsPWD.Tables(0).Rows(0)("UserGroupCode") & "")) = "ADMIN" Then
                        bPassword = True
                        pnlAdminPwd.Visible = False
                        txtNoofInstallment.Focus()
                    Else
                        MessageBox.Show("Please Check the Password")
                        txtAdminPassword.Focus()
                        bPassword = False
                        Exit Sub
                    End If
                Else
                    MessageBox.Show("Please Check the Password")
                    txtAdminPassword.Focus()
                    bPassword = False
                    Exit Sub
                End If
            Else
                MessageBox.Show("Enter the Password")
                txtAdminPassword.Focus()
                bPassword = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnMemberCodeSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim frmHelpCustomer As New frmHelpSearch(0, 0, "tblMember", "L", 4, "MemberName", "", "MemberCode", "MemberCode", 80, "MemberName", "MemberName", 150, "Address1", "Address1", 90, "Address2", "Address2", 80)
            frmHelpCustomer.ShowDialog()
            txtMemberCode.Text = frmHelpCustomer.Var1.Trim
            txtCustomerName.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub


End Class
