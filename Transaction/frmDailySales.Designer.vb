<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDailySales
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDailySales))
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmdFetch = New System.Windows.Forms.Button()
        Me.txtAdvCreditUtilise = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtPettycash = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtTotalSales = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtGST = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtTotalGSTSales = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtAdvancePayment = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtTradeInGold = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtInvoiceDate = New System.Windows.Forms.DateTimePicker()
        Me.txt24K = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtInstallment = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtTransfer = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtChq = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtDiners = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtAmex = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtMaster = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtVisa = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtNets = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCash = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(12, 12)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 100)
        Me.pic.TabIndex = 570
        Me.pic.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cmdFetch)
        Me.Panel1.Controls.Add(Me.txtAdvCreditUtilise)
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Controls.Add(Me.txtPettycash)
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Controls.Add(Me.txtTotalSales)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.txtGST)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.txtTotalGSTSales)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.txtAdvancePayment)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.txtTradeInGold)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.txtInvoiceDate)
        Me.Panel1.Controls.Add(Me.txt24K)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.txtInstallment)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.txtTransfer)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.txtChq)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.txtDiners)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.txtAmex)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtMaster)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txtVisa)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txtNets)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtCash)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Location = New System.Drawing.Point(12, 127)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(645, 381)
        Me.Panel1.TabIndex = 571
        '
        'cmdFetch
        '
        Me.cmdFetch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdFetch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.cmdFetch.Location = New System.Drawing.Point(588, 15)
        Me.cmdFetch.Name = "cmdFetch"
        Me.cmdFetch.Size = New System.Drawing.Size(54, 23)
        Me.cmdFetch.TabIndex = 77
        Me.cmdFetch.Text = "Fetch"
        Me.cmdFetch.UseVisualStyleBackColor = False
        '
        'txtAdvCreditUtilise
        '
        Me.txtAdvCreditUtilise.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdvCreditUtilise.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdvCreditUtilise.Location = New System.Drawing.Point(462, 99)
        Me.txtAdvCreditUtilise.MaxLength = 20
        Me.txtAdvCreditUtilise.Name = "txtAdvCreditUtilise"
        Me.txtAdvCreditUtilise.Size = New System.Drawing.Size(120, 20)
        Me.txtAdvCreditUtilise.TabIndex = 67
        Me.txtAdvCreditUtilise.Tag = "NE;"
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(360, 99)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(68, 34)
        Me.Label18.TabIndex = 76
        Me.Label18.Text = "Adv Credit Utilise"
        '
        'txtPettycash
        '
        Me.txtPettycash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPettycash.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPettycash.Location = New System.Drawing.Point(462, 59)
        Me.txtPettycash.MaxLength = 20
        Me.txtPettycash.Name = "txtPettycash"
        Me.txtPettycash.Size = New System.Drawing.Size(120, 20)
        Me.txtPettycash.TabIndex = 66
        Me.txtPettycash.Tag = "NE;"
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(360, 59)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(68, 34)
        Me.Label17.TabIndex = 74
        Me.Label17.Text = "Petty Cash"
        '
        'txtTotalSales
        '
        Me.txtTotalSales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalSales.Enabled = False
        Me.txtTotalSales.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalSales.Location = New System.Drawing.Point(462, 291)
        Me.txtTotalSales.MaxLength = 20
        Me.txtTotalSales.Name = "txtTotalSales"
        Me.txtTotalSales.Size = New System.Drawing.Size(120, 20)
        Me.txtTotalSales.TabIndex = 71
        Me.txtTotalSales.Tag = "NE;"
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(360, 291)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(68, 46)
        Me.Label16.TabIndex = 72
        Me.Label16.Text = "Total Sales Value GST & NON GST"
        '
        'txtGST
        '
        Me.txtGST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGST.Enabled = False
        Me.txtGST.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGST.Location = New System.Drawing.Point(462, 248)
        Me.txtGST.MaxLength = 20
        Me.txtGST.Name = "txtGST"
        Me.txtGST.Size = New System.Drawing.Size(120, 20)
        Me.txtGST.TabIndex = 69
        Me.txtGST.Tag = "NE;"
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(360, 248)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(68, 34)
        Me.Label15.TabIndex = 70
        Me.Label15.Text = "GST"
        '
        'txtTotalGSTSales
        '
        Me.txtTotalGSTSales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalGSTSales.Enabled = False
        Me.txtTotalGSTSales.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalGSTSales.Location = New System.Drawing.Point(462, 199)
        Me.txtTotalGSTSales.MaxLength = 20
        Me.txtTotalGSTSales.Name = "txtTotalGSTSales"
        Me.txtTotalGSTSales.Size = New System.Drawing.Size(120, 20)
        Me.txtTotalGSTSales.TabIndex = 68
        Me.txtTotalGSTSales.Tag = "NE;"
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(360, 199)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(68, 34)
        Me.Label14.TabIndex = 68
        Me.Label14.Text = "Total GST Sales"
        '
        'txtAdvancePayment
        '
        Me.txtAdvancePayment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdvancePayment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdvancePayment.Location = New System.Drawing.Point(148, 317)
        Me.txtAdvancePayment.MaxLength = 20
        Me.txtAdvancePayment.Name = "txtAdvancePayment"
        Me.txtAdvancePayment.Size = New System.Drawing.Size(160, 20)
        Me.txtAdvancePayment.TabIndex = 65
        Me.txtAdvancePayment.Tag = "NE;"
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(20, 317)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(122, 20)
        Me.Label13.TabIndex = 66
        Me.Label13.Text = "Advance Payment"
        '
        'txtTradeInGold
        '
        Me.txtTradeInGold.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTradeInGold.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTradeInGold.Location = New System.Drawing.Point(148, 291)
        Me.txtTradeInGold.MaxLength = 20
        Me.txtTradeInGold.Name = "txtTradeInGold"
        Me.txtTradeInGold.Size = New System.Drawing.Size(160, 20)
        Me.txtTradeInGold.TabIndex = 63
        Me.txtTradeInGold.Tag = "NE;"
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(20, 291)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(122, 20)
        Me.Label12.TabIndex = 64
        Me.Label12.Text = "TradeIn Gold"
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(360, 20)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(96, 16)
        Me.Label11.TabIndex = 62
        Me.Label11.Text = "DATE"
        '
        'txtInvoiceDate
        '
        Me.txtInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.txtInvoiceDate.Location = New System.Drawing.Point(462, 16)
        Me.txtInvoiceDate.Name = "txtInvoiceDate"
        Me.txtInvoiceDate.Size = New System.Drawing.Size(120, 20)
        Me.txtInvoiceDate.TabIndex = 1
        Me.txtInvoiceDate.Value = New Date(2011, 3, 7, 0, 0, 0, 0)
        '
        'txt24K
        '
        Me.txt24K.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt24K.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt24K.Location = New System.Drawing.Point(148, 262)
        Me.txt24K.MaxLength = 20
        Me.txt24K.Name = "txt24K"
        Me.txt24K.Size = New System.Drawing.Size(160, 20)
        Me.txt24K.TabIndex = 59
        Me.txt24K.Tag = "NE;"
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(20, 262)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(122, 20)
        Me.Label10.TabIndex = 60
        Me.Label10.Text = "24K (NO GST)"
        '
        'txtInstallment
        '
        Me.txtInstallment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInstallment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInstallment.Location = New System.Drawing.Point(148, 236)
        Me.txtInstallment.MaxLength = 20
        Me.txtInstallment.Name = "txtInstallment"
        Me.txtInstallment.Size = New System.Drawing.Size(160, 20)
        Me.txtInstallment.TabIndex = 57
        Me.txtInstallment.Tag = "NE;"
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(20, 236)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(122, 20)
        Me.Label9.TabIndex = 58
        Me.Label9.Text = "INST"
        '
        'txtTransfer
        '
        Me.txtTransfer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTransfer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransfer.Location = New System.Drawing.Point(148, 210)
        Me.txtTransfer.MaxLength = 20
        Me.txtTransfer.Name = "txtTransfer"
        Me.txtTransfer.Size = New System.Drawing.Size(160, 20)
        Me.txtTransfer.TabIndex = 55
        Me.txtTransfer.Tag = "NE;"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(20, 210)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(122, 20)
        Me.Label8.TabIndex = 56
        Me.Label8.Text = "TRANSFER TO A/C"
        '
        'txtChq
        '
        Me.txtChq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChq.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChq.Location = New System.Drawing.Point(148, 184)
        Me.txtChq.MaxLength = 20
        Me.txtChq.Name = "txtChq"
        Me.txtChq.Size = New System.Drawing.Size(160, 20)
        Me.txtChq.TabIndex = 53
        Me.txtChq.Tag = "NE;"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(20, 184)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(96, 16)
        Me.Label7.TabIndex = 54
        Me.Label7.Text = "CHQ"
        '
        'txtDiners
        '
        Me.txtDiners.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDiners.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiners.Location = New System.Drawing.Point(148, 158)
        Me.txtDiners.MaxLength = 20
        Me.txtDiners.Name = "txtDiners"
        Me.txtDiners.Size = New System.Drawing.Size(160, 20)
        Me.txtDiners.TabIndex = 51
        Me.txtDiners.Tag = "NE;"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(20, 158)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(96, 16)
        Me.Label6.TabIndex = 52
        Me.Label6.Text = "DINERS"
        '
        'txtAmex
        '
        Me.txtAmex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmex.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmex.Location = New System.Drawing.Point(148, 131)
        Me.txtAmex.MaxLength = 20
        Me.txtAmex.Name = "txtAmex"
        Me.txtAmex.Size = New System.Drawing.Size(160, 20)
        Me.txtAmex.TabIndex = 49
        Me.txtAmex.Tag = "NE;"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(20, 131)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 16)
        Me.Label5.TabIndex = 50
        Me.Label5.Text = "AMEX"
        '
        'txtMaster
        '
        Me.txtMaster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMaster.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMaster.Location = New System.Drawing.Point(148, 103)
        Me.txtMaster.MaxLength = 20
        Me.txtMaster.Name = "txtMaster"
        Me.txtMaster.Size = New System.Drawing.Size(160, 20)
        Me.txtMaster.TabIndex = 47
        Me.txtMaster.Tag = "NE;"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(20, 103)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(96, 16)
        Me.Label4.TabIndex = 48
        Me.Label4.Text = "MASTER"
        '
        'txtVisa
        '
        Me.txtVisa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVisa.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVisa.Location = New System.Drawing.Point(148, 74)
        Me.txtVisa.MaxLength = 20
        Me.txtVisa.Name = "txtVisa"
        Me.txtVisa.Size = New System.Drawing.Size(160, 20)
        Me.txtVisa.TabIndex = 45
        Me.txtVisa.Tag = "NE;"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(20, 74)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 16)
        Me.Label3.TabIndex = 46
        Me.Label3.Text = "VISA"
        '
        'txtNets
        '
        Me.txtNets.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNets.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNets.Location = New System.Drawing.Point(148, 46)
        Me.txtNets.MaxLength = 20
        Me.txtNets.Name = "txtNets"
        Me.txtNets.Size = New System.Drawing.Size(160, 20)
        Me.txtNets.TabIndex = 43
        Me.txtNets.Tag = "NE;"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(20, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 16)
        Me.Label1.TabIndex = 44
        Me.Label1.Text = "NETS"
        '
        'txtCash
        '
        Me.txtCash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCash.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCash.Location = New System.Drawing.Point(148, 18)
        Me.txtCash.MaxLength = 20
        Me.txtCash.Name = "txtCash"
        Me.txtCash.Size = New System.Drawing.Size(160, 20)
        Me.txtCash.TabIndex = 41
        Me.txtCash.Tag = "NE;"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(20, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 16)
        Me.Label2.TabIndex = 42
        Me.Label2.Text = "CASH"
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.Location = New System.Drawing.Point(451, 514)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(100, 40)
        Me.btnClear.TabIndex = 574
        Me.btnClear.Text = "Clear(F6)"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(345, 514)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 40)
        Me.btnSave.TabIndex = 573
        Me.btnSave.Text = "Save(F7)"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(557, 514)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 572
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'frmDailySales
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(712, 598)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pic)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmDailySales"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DailySales"
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtInstallment As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtTransfer As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtChq As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtDiners As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtAmex As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtMaster As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtVisa As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtNets As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCash As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt24K As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtInvoiceDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtAdvancePayment As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtTradeInGold As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtGST As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtTotalGSTSales As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtAdvCreditUtilise As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtPettycash As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtTotalSales As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cmdFetch As System.Windows.Forms.Button
End Class
