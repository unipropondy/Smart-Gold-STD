<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTradeInMerge
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTradeInMerge))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtTotalSTWeight = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtTYY = New System.Windows.Forms.TextBox()
        Me.txtTMM = New System.Windows.Forms.TextBox()
        Me.txtTDD = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtTotalPrice = New System.Windows.Forms.TextBox()
        Me.txtPricePerGram = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtFYY = New System.Windows.Forms.TextBox()
        Me.txtFMM = New System.Windows.Forms.TextBox()
        Me.txtFDD = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTradeInventoryCode = New System.Windows.Forms.TextBox()
        Me.txtTradeBillNo = New System.Windows.Forms.TextBox()
        Me.btnTradeBillNoSearch = New System.Windows.Forms.Button()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cboPurity = New System.Windows.Forms.ComboBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtTotalWt = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.pnlInvoice = New System.Windows.Forms.Panel()
        Me.lblSelStWeight = New System.Windows.Forms.Label()
        Me.lblSelCost = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblSelweight = New System.Windows.Forms.Label()
        Me.grdInvoice = New System.Windows.Forms.DataGridView()
        Me.cSel = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ColInvoiceNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cIDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cInventoryCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cStoneWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotalCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cIssueWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cCustomerCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cBalanceWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnPanelClose = New System.Windows.Forms.Button()
        Me.chkSelectAll = New System.Windows.Forms.CheckBox()
        Me.btnPanelOK = New System.Windows.Forms.Button()
        Me.btnProceed = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnBillClear = New System.Windows.Forms.Button()
        Me.grdGoldIssue = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cInvCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPweight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colClass = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.pnlInvoice.SuspendLayout()
        CType(Me.grdInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdGoldIssue, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(2, 2)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 100)
        Me.pic.TabIndex = 137
        Me.pic.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txtTotalSTWeight)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.txtTYY)
        Me.Panel1.Controls.Add(Me.txtTMM)
        Me.Panel1.Controls.Add(Me.txtTDD)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txtTotalPrice)
        Me.Panel1.Controls.Add(Me.txtPricePerGram)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtFYY)
        Me.Panel1.Controls.Add(Me.txtFMM)
        Me.Panel1.Controls.Add(Me.txtFDD)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtTradeInventoryCode)
        Me.Panel1.Controls.Add(Me.txtTradeBillNo)
        Me.Panel1.Controls.Add(Me.btnTradeBillNoSearch)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.cboPurity)
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Controls.Add(Me.txtRemarks)
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Controls.Add(Me.txtTotalWt)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Location = New System.Drawing.Point(2, 108)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(616, 152)
        Me.Panel1.TabIndex = 138
        '
        'txtTotalSTWeight
        '
        Me.txtTotalSTWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalSTWeight.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalSTWeight.ForeColor = System.Drawing.Color.BlueViolet
        Me.txtTotalSTWeight.Location = New System.Drawing.Point(74, 94)
        Me.txtTotalSTWeight.MaxLength = 20
        Me.txtTotalSTWeight.Name = "txtTotalSTWeight"
        Me.txtTotalSTWeight.ReadOnly = True
        Me.txtTotalSTWeight.Size = New System.Drawing.Size(120, 21)
        Me.txtTotalSTWeight.TabIndex = 476288
        Me.txtTotalSTWeight.Tag = ""
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(4, 97)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(74, 19)
        Me.Label10.TabIndex = 476289
        Me.Label10.Text = "Total St. Wt"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(431, 69)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(11, 14)
        Me.Label4.TabIndex = 476283
        Me.Label4.Text = "-"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(385, 70)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(11, 14)
        Me.Label8.TabIndex = 476282
        Me.Label8.Text = "-"
        '
        'txtTYY
        '
        Me.txtTYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTYY.Location = New System.Drawing.Point(444, 67)
        Me.txtTYY.MaxLength = 20
        Me.txtTYY.Name = "txtTYY"
        Me.txtTYY.Size = New System.Drawing.Size(32, 20)
        Me.txtTYY.TabIndex = 476286
        Me.txtTYY.Tag = "NE;"
        '
        'txtTMM
        '
        Me.txtTMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTMM.Location = New System.Drawing.Point(397, 67)
        Me.txtTMM.MaxLength = 20
        Me.txtTMM.Name = "txtTMM"
        Me.txtTMM.Size = New System.Drawing.Size(32, 20)
        Me.txtTMM.TabIndex = 476285
        Me.txtTMM.Tag = "NE;"
        '
        'txtTDD
        '
        Me.txtTDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTDD.Location = New System.Drawing.Point(351, 67)
        Me.txtTDD.MaxLength = 20
        Me.txtTDD.Name = "txtTDD"
        Me.txtTDD.Size = New System.Drawing.Size(32, 20)
        Me.txtTDD.TabIndex = 476284
        Me.txtTDD.Tag = "NE;"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(236, 70)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(57, 13)
        Me.Label9.TabIndex = 476287
        Me.Label9.Text = "To Date"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(235, 93)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(95, 22)
        Me.Label3.TabIndex = 476281
        Me.Label3.Text = "Total Price"
        '
        'txtTotalPrice
        '
        Me.txtTotalPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalPrice.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalPrice.ForeColor = System.Drawing.Color.Black
        Me.txtTotalPrice.Location = New System.Drawing.Point(351, 91)
        Me.txtTotalPrice.MaxLength = 8
        Me.txtTotalPrice.Name = "txtTotalPrice"
        Me.txtTotalPrice.Size = New System.Drawing.Size(96, 21)
        Me.txtTotalPrice.TabIndex = 476279
        Me.txtTotalPrice.Tag = ""
        '
        'txtPricePerGram
        '
        Me.txtPricePerGram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPricePerGram.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPricePerGram.ForeColor = System.Drawing.Color.Black
        Me.txtPricePerGram.Location = New System.Drawing.Point(75, 94)
        Me.txtPricePerGram.MaxLength = 8
        Me.txtPricePerGram.Name = "txtPricePerGram"
        Me.txtPricePerGram.Size = New System.Drawing.Size(72, 21)
        Me.txtPricePerGram.TabIndex = 476278
        Me.txtPricePerGram.Tag = ""
        Me.txtPricePerGram.Visible = False
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(3, 94)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(66, 24)
        Me.Label7.TabIndex = 476280
        Me.Label7.Text = "Cost/Gram"
        Me.Label7.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(155, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(11, 14)
        Me.Label2.TabIndex = 476273
        Me.Label2.Text = "-"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(109, 71)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(11, 14)
        Me.Label5.TabIndex = 476272
        Me.Label5.Text = "-"
        '
        'txtFYY
        '
        Me.txtFYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFYY.Location = New System.Drawing.Point(168, 68)
        Me.txtFYY.MaxLength = 20
        Me.txtFYY.Name = "txtFYY"
        Me.txtFYY.Size = New System.Drawing.Size(32, 20)
        Me.txtFYY.TabIndex = 476276
        Me.txtFYY.Tag = "NE;"
        '
        'txtFMM
        '
        Me.txtFMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFMM.Location = New System.Drawing.Point(121, 68)
        Me.txtFMM.MaxLength = 20
        Me.txtFMM.Name = "txtFMM"
        Me.txtFMM.Size = New System.Drawing.Size(32, 20)
        Me.txtFMM.TabIndex = 476275
        Me.txtFMM.Tag = "NE;"
        '
        'txtFDD
        '
        Me.txtFDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFDD.Location = New System.Drawing.Point(75, 68)
        Me.txtFDD.MaxLength = 20
        Me.txtFDD.Name = "txtFDD"
        Me.txtFDD.Size = New System.Drawing.Size(32, 20)
        Me.txtFDD.TabIndex = 476274
        Me.txtFDD.Tag = "NE;"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 70)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(75, 13)
        Me.Label6.TabIndex = 476277
        Me.Label6.Text = "From Date"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(235, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(107, 30)
        Me.Label1.TabIndex = 3859
        Me.Label1.Text = "Trade Inventory Code"
        '
        'txtTradeInventoryCode
        '
        Me.txtTradeInventoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTradeInventoryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTradeInventoryCode.ForeColor = System.Drawing.Color.Black
        Me.txtTradeInventoryCode.Location = New System.Drawing.Point(351, 15)
        Me.txtTradeInventoryCode.MaxLength = 20
        Me.txtTradeInventoryCode.Name = "txtTradeInventoryCode"
        Me.txtTradeInventoryCode.Size = New System.Drawing.Size(120, 20)
        Me.txtTradeInventoryCode.TabIndex = 3858
        Me.txtTradeInventoryCode.Tag = ""
        '
        'txtTradeBillNo
        '
        Me.txtTradeBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTradeBillNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTradeBillNo.ForeColor = System.Drawing.Color.Black
        Me.txtTradeBillNo.Location = New System.Drawing.Point(351, 41)
        Me.txtTradeBillNo.MaxLength = 20
        Me.txtTradeBillNo.Name = "txtTradeBillNo"
        Me.txtTradeBillNo.Size = New System.Drawing.Size(96, 20)
        Me.txtTradeBillNo.TabIndex = 3855
        Me.txtTradeBillNo.Tag = ""
        '
        'btnTradeBillNoSearch
        '
        Me.btnTradeBillNoSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnTradeBillNoSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTradeBillNoSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTradeBillNoSearch.Location = New System.Drawing.Point(444, 41)
        Me.btnTradeBillNoSearch.Name = "btnTradeBillNoSearch"
        Me.btnTradeBillNoSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnTradeBillNoSearch.TabIndex = 3856
        Me.btnTradeBillNoSearch.Text = "..."
        Me.btnTradeBillNoSearch.UseVisualStyleBackColor = False
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(235, 44)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(64, 15)
        Me.Label16.TabIndex = 3857
        Me.Label16.Text = "Trade Bill No"
        '
        'cboPurity
        '
        Me.cboPurity.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPurity.FormattingEnabled = True
        Me.cboPurity.Items.AddRange(New Object() {"750", "835", "875", "916", "999", "999.9", "22K"})
        Me.cboPurity.Location = New System.Drawing.Point(74, 14)
        Me.cboPurity.Name = "cboPurity"
        Me.cboPurity.Size = New System.Drawing.Size(64, 21)
        Me.cboPurity.TabIndex = 3854
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(10, 15)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(64, 16)
        Me.Label18.TabIndex = 3853
        Me.Label18.Text = "Purity"
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.Black
        Me.txtRemarks.Location = New System.Drawing.Point(74, 127)
        Me.txtRemarks.MaxLength = 50
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(477, 20)
        Me.txtRemarks.TabIndex = 3851
        Me.txtRemarks.Tag = ""
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(4, 129)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(71, 20)
        Me.Label17.TabIndex = 3852
        Me.Label17.Text = "Remarks"
        '
        'txtTotalWt
        '
        Me.txtTotalWt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalWt.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalWt.ForeColor = System.Drawing.Color.BlueViolet
        Me.txtTotalWt.Location = New System.Drawing.Point(74, 41)
        Me.txtTotalWt.MaxLength = 20
        Me.txtTotalWt.Name = "txtTotalWt"
        Me.txtTotalWt.ReadOnly = True
        Me.txtTotalWt.Size = New System.Drawing.Size(120, 21)
        Me.txtTotalWt.TabIndex = 801
        Me.txtTotalWt.Tag = ""
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(4, 44)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(64, 19)
        Me.Label12.TabIndex = 802
        Me.Label12.Text = "Total Wt"
        '
        'pnlInvoice
        '
        Me.pnlInvoice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlInvoice.Controls.Add(Me.lblSelStWeight)
        Me.pnlInvoice.Controls.Add(Me.lblSelCost)
        Me.pnlInvoice.Controls.Add(Me.Label15)
        Me.pnlInvoice.Controls.Add(Me.lblSelweight)
        Me.pnlInvoice.Controls.Add(Me.grdInvoice)
        Me.pnlInvoice.Controls.Add(Me.btnPanelClose)
        Me.pnlInvoice.Controls.Add(Me.chkSelectAll)
        Me.pnlInvoice.Controls.Add(Me.btnPanelOK)
        Me.pnlInvoice.Location = New System.Drawing.Point(2, 276)
        Me.pnlInvoice.Name = "pnlInvoice"
        Me.pnlInvoice.Size = New System.Drawing.Size(616, 256)
        Me.pnlInvoice.TabIndex = 139
        Me.pnlInvoice.Visible = False
        '
        'lblSelStWeight
        '
        Me.lblSelStWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelStWeight.ForeColor = System.Drawing.Color.Black
        Me.lblSelStWeight.Location = New System.Drawing.Point(332, 228)
        Me.lblSelStWeight.Name = "lblSelStWeight"
        Me.lblSelStWeight.Size = New System.Drawing.Size(64, 15)
        Me.lblSelStWeight.TabIndex = 144
        Me.lblSelStWeight.Visible = False
        '
        'lblSelCost
        '
        Me.lblSelCost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelCost.ForeColor = System.Drawing.Color.Black
        Me.lblSelCost.Location = New System.Drawing.Point(276, 228)
        Me.lblSelCost.Name = "lblSelCost"
        Me.lblSelCost.Size = New System.Drawing.Size(64, 15)
        Me.lblSelCost.TabIndex = 143
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(115, 230)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(85, 14)
        Me.Label15.TabIndex = 142
        Me.Label15.Text = "Total Weight"
        '
        'lblSelweight
        '
        Me.lblSelweight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelweight.ForeColor = System.Drawing.Color.Black
        Me.lblSelweight.Location = New System.Drawing.Point(206, 230)
        Me.lblSelweight.Name = "lblSelweight"
        Me.lblSelweight.Size = New System.Drawing.Size(64, 15)
        Me.lblSelweight.TabIndex = 141
        '
        'grdInvoice
        '
        Me.grdInvoice.AllowUserToAddRows = False
        Me.grdInvoice.AllowUserToDeleteRows = False
        Me.grdInvoice.AllowUserToOrderColumns = True
        Me.grdInvoice.AllowUserToResizeColumns = False
        Me.grdInvoice.AllowUserToResizeRows = False
        Me.grdInvoice.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdInvoice.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdInvoice.ColumnHeadersHeight = 40
        Me.grdInvoice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdInvoice.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cSel, Me.ColInvoiceNo, Me.cIDate, Me.cInventoryCode, Me.cWeight, Me.cStoneWeight, Me.TotalCost, Me.cIssueWeight, Me.cCustomerCode, Me.cBalanceWeight})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdInvoice.DefaultCellStyle = DataGridViewCellStyle2
        Me.grdInvoice.Location = New System.Drawing.Point(8, 3)
        Me.grdInvoice.Name = "grdInvoice"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdInvoice.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.grdInvoice.RowHeadersVisible = False
        Me.grdInvoice.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.grdInvoice.Size = New System.Drawing.Size(601, 205)
        Me.grdInvoice.TabIndex = 140
        '
        'cSel
        '
        Me.cSel.HeaderText = "Sel"
        Me.cSel.Name = "cSel"
        Me.cSel.Width = 30
        '
        'ColInvoiceNo
        '
        Me.ColInvoiceNo.HeaderText = "Invoice No"
        Me.ColInvoiceNo.Name = "ColInvoiceNo"
        '
        'cIDate
        '
        Me.cIDate.HeaderText = "Date"
        Me.cIDate.Name = "cIDate"
        Me.cIDate.Width = 90
        '
        'cInventoryCode
        '
        Me.cInventoryCode.HeaderText = "Inventory Code"
        Me.cInventoryCode.Name = "cInventoryCode"
        '
        'cWeight
        '
        Me.cWeight.HeaderText = "Weight"
        Me.cWeight.Name = "cWeight"
        Me.cWeight.Width = 80
        '
        'cStoneWeight
        '
        Me.cStoneWeight.HeaderText = "StoneWeight"
        Me.cStoneWeight.Name = "cStoneWeight"
        '
        'TotalCost
        '
        Me.TotalCost.HeaderText = "TotalCost"
        Me.TotalCost.Name = "TotalCost"
        '
        'cIssueWeight
        '
        Me.cIssueWeight.HeaderText = "Issue Weight"
        Me.cIssueWeight.Name = "cIssueWeight"
        Me.cIssueWeight.Visible = False
        '
        'cCustomerCode
        '
        Me.cCustomerCode.HeaderText = "Customer Code"
        Me.cCustomerCode.Name = "cCustomerCode"
        Me.cCustomerCode.Visible = False
        '
        'cBalanceWeight
        '
        Me.cBalanceWeight.HeaderText = "Balance Weight"
        Me.cBalanceWeight.Name = "cBalanceWeight"
        Me.cBalanceWeight.Visible = False
        Me.cBalanceWeight.Width = 80
        '
        'btnPanelClose
        '
        Me.btnPanelClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPanelClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPanelClose.Location = New System.Drawing.Point(512, 224)
        Me.btnPanelClose.Name = "btnPanelClose"
        Me.btnPanelClose.Size = New System.Drawing.Size(92, 23)
        Me.btnPanelClose.TabIndex = 139
        Me.btnPanelClose.Text = "Close"
        Me.btnPanelClose.UseVisualStyleBackColor = False
        '
        'chkSelectAll
        '
        Me.chkSelectAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSelectAll.Location = New System.Drawing.Point(8, 224)
        Me.chkSelectAll.Name = "chkSelectAll"
        Me.chkSelectAll.Size = New System.Drawing.Size(80, 24)
        Me.chkSelectAll.TabIndex = 138
        Me.chkSelectAll.Text = "Select All"
        '
        'btnPanelOK
        '
        Me.btnPanelOK.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPanelOK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPanelOK.Location = New System.Drawing.Point(416, 224)
        Me.btnPanelOK.Name = "btnPanelOK"
        Me.btnPanelOK.Size = New System.Drawing.Size(92, 23)
        Me.btnPanelOK.TabIndex = 136
        Me.btnPanelOK.Text = "OK"
        Me.btnPanelOK.UseVisualStyleBackColor = False
        '
        'btnProceed
        '
        Me.btnProceed.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnProceed.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnProceed.Location = New System.Drawing.Point(430, 538)
        Me.btnProceed.Name = "btnProceed"
        Me.btnProceed.Size = New System.Drawing.Size(92, 38)
        Me.btnProceed.TabIndex = 140
        Me.btnProceed.Text = "Proceed (F7)"
        Me.btnProceed.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(526, 538)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(92, 38)
        Me.btnClose.TabIndex = 142
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnBillClear
        '
        Me.btnBillClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnBillClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBillClear.Location = New System.Drawing.Point(334, 538)
        Me.btnBillClear.Name = "btnBillClear"
        Me.btnBillClear.Size = New System.Drawing.Size(92, 38)
        Me.btnBillClear.TabIndex = 141
        Me.btnBillClear.Text = "Clear (F6)"
        Me.btnBillClear.UseVisualStyleBackColor = False
        '
        'grdGoldIssue
        '
        Me.grdGoldIssue.AllowUserToAddRows = False
        Me.grdGoldIssue.AllowUserToDeleteRows = False
        Me.grdGoldIssue.AllowUserToResizeColumns = False
        Me.grdGoldIssue.AllowUserToResizeRows = False
        Me.grdGoldIssue.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdGoldIssue.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.grdGoldIssue.ColumnHeadersHeight = 40
        Me.grdGoldIssue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdGoldIssue.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.colDate, Me.cInvCode, Me.colPweight, Me.colIWeight, Me.DataGridViewTextBoxColumn5, Me.colClass})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdGoldIssue.DefaultCellStyle = DataGridViewCellStyle5
        Me.grdGoldIssue.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdGoldIssue.Location = New System.Drawing.Point(12, 266)
        Me.grdGoldIssue.Name = "grdGoldIssue"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdGoldIssue.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.grdGoldIssue.RowHeadersVisible = False
        Me.grdGoldIssue.Size = New System.Drawing.Size(601, 176)
        Me.grdGoldIssue.TabIndex = 143
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Invoice No"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'colDate
        '
        Me.colDate.HeaderText = "Date"
        Me.colDate.Name = "colDate"
        '
        'cInvCode
        '
        Me.cInvCode.HeaderText = "Inventory Code"
        Me.cInvCode.Name = "cInvCode"
        '
        'colPweight
        '
        Me.colPweight.HeaderText = "Purchase Weight"
        Me.colPweight.Name = "colPweight"
        Me.colPweight.Width = 80
        '
        'colIWeight
        '
        Me.colIWeight.HeaderText = "Issued Weight"
        Me.colIWeight.Name = "colIWeight"
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "Balance Weight"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.Visible = False
        Me.DataGridViewTextBoxColumn5.Width = 80
        '
        'colClass
        '
        Me.colClass.HeaderText = "Class"
        Me.colClass.Name = "colClass"
        Me.colClass.Visible = False
        '
        'frmTradeInMerge
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(658, 588)
        Me.Controls.Add(Me.pnlInvoice)
        Me.Controls.Add(Me.grdGoldIssue)
        Me.Controls.Add(Me.btnProceed)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnBillClear)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pic)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmTradeInMerge"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TradeIn Merge"
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlInvoice.ResumeLayout(False)
        CType(Me.grdInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdGoldIssue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cboPurity As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtTotalWt As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents pnlInvoice As System.Windows.Forms.Panel
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lblSelweight As System.Windows.Forms.Label
    Friend WithEvents grdInvoice As System.Windows.Forms.DataGridView
    Friend WithEvents btnPanelClose As System.Windows.Forms.Button
    Friend WithEvents chkSelectAll As System.Windows.Forms.CheckBox
    Friend WithEvents btnPanelOK As System.Windows.Forms.Button
    Friend WithEvents txtTradeBillNo As System.Windows.Forms.TextBox
    Friend WithEvents btnTradeBillNoSearch As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTradeInventoryCode As System.Windows.Forms.TextBox
    Friend WithEvents btnProceed As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnBillClear As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtFYY As System.Windows.Forms.TextBox
    Friend WithEvents txtFMM As System.Windows.Forms.TextBox
    Friend WithEvents txtFDD As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents grdGoldIssue As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cInvCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPweight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colIWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colClass As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtTotalPrice As System.Windows.Forms.TextBox
    Friend WithEvents txtPricePerGram As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblSelCost As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtTYY As System.Windows.Forms.TextBox
    Friend WithEvents txtTMM As System.Windows.Forms.TextBox
    Friend WithEvents txtTDD As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cSel As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents ColInvoiceNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cIDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cInventoryCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cStoneWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TotalCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cIssueWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCustomerCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cBalanceWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblSelStWeight As System.Windows.Forms.Label
    Friend WithEvents txtTotalSTWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
End Class
