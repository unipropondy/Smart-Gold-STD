<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPaymodeModification
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPaymodeModification))
        Me.dgvPaymode = New System.Windows.Forms.DataGridView()
        Me.cPaymode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cSlipNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cNewAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cNewSlipNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvBills = New System.Windows.Forms.DataGridView()
        Me.cInvoiceNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cCustCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cTerminalCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cSalesmanCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cInvoicePaymode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cNetAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cPaidAmt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cChange = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblBalance = New System.Windows.Forms.Label()
        Me.lblPaid = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblInvoice = New System.Windows.Forms.Label()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdClear = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.PnlSalesManPwd = New System.Windows.Forms.Panel()
        Me.txtSalesManPWD = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        CType(Me.dgvPaymode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvBills, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlSalesManPwd.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvPaymode
        '
        Me.dgvPaymode.AllowUserToAddRows = False
        Me.dgvPaymode.AllowUserToDeleteRows = False
        Me.dgvPaymode.AllowUserToResizeColumns = False
        Me.dgvPaymode.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPaymode.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvPaymode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPaymode.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cPaymode, Me.cAmount, Me.cSlipNo, Me.cNewAmount, Me.cNewSlipNo})
        Me.dgvPaymode.Location = New System.Drawing.Point(12, 318)
        Me.dgvPaymode.Name = "dgvPaymode"
        Me.dgvPaymode.RowHeadersVisible = False
        Me.dgvPaymode.RowTemplate.Height = 30
        Me.dgvPaymode.Size = New System.Drawing.Size(644, 293)
        Me.dgvPaymode.TabIndex = 3
        '
        'cPaymode
        '
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cPaymode.DefaultCellStyle = DataGridViewCellStyle2
        Me.cPaymode.Frozen = True
        Me.cPaymode.HeaderText = "Paymode"
        Me.cPaymode.Name = "cPaymode"
        Me.cPaymode.ReadOnly = True
        Me.cPaymode.Width = 150
        '
        'cAmount
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.NullValue = "0.00"
        Me.cAmount.DefaultCellStyle = DataGridViewCellStyle3
        Me.cAmount.Frozen = True
        Me.cAmount.HeaderText = "Amount"
        Me.cAmount.Name = "cAmount"
        Me.cAmount.ReadOnly = True
        Me.cAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cAmount.Width = 120
        '
        'cSlipNo
        '
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cSlipNo.DefaultCellStyle = DataGridViewCellStyle4
        Me.cSlipNo.Frozen = True
        Me.cSlipNo.HeaderText = "SlipNo"
        Me.cSlipNo.Name = "cSlipNo"
        Me.cSlipNo.ReadOnly = True
        Me.cSlipNo.Width = 120
        '
        'cNewAmount
        '
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cNewAmount.DefaultCellStyle = DataGridViewCellStyle5
        Me.cNewAmount.HeaderText = "Amount"
        Me.cNewAmount.Name = "cNewAmount"
        '
        'cNewSlipNo
        '
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cNewSlipNo.DefaultCellStyle = DataGridViewCellStyle6
        Me.cNewSlipNo.HeaderText = "SlipNo"
        Me.cNewSlipNo.Name = "cNewSlipNo"
        '
        'dgvBills
        '
        Me.dgvBills.AllowUserToAddRows = False
        Me.dgvBills.AllowUserToDeleteRows = False
        Me.dgvBills.AllowUserToResizeColumns = False
        Me.dgvBills.AllowUserToResizeRows = False
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvBills.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.dgvBills.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvBills.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cInvoiceNo, Me.cCustCode, Me.cTerminalCode, Me.cSalesmanCode, Me.cInvoicePaymode, Me.cNetAmount, Me.cPaidAmt, Me.cChange})
        Me.dgvBills.Location = New System.Drawing.Point(12, 164)
        Me.dgvBills.Name = "dgvBills"
        Me.dgvBills.ReadOnly = True
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvBills.RowHeadersDefaultCellStyle = DataGridViewCellStyle16
        Me.dgvBills.RowHeadersVisible = False
        Me.dgvBills.RowTemplate.Height = 30
        Me.dgvBills.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvBills.Size = New System.Drawing.Size(931, 151)
        Me.dgvBills.TabIndex = 4
        '
        'cInvoiceNo
        '
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cInvoiceNo.DefaultCellStyle = DataGridViewCellStyle8
        Me.cInvoiceNo.Frozen = True
        Me.cInvoiceNo.HeaderText = "Invoice No."
        Me.cInvoiceNo.Name = "cInvoiceNo"
        Me.cInvoiceNo.ReadOnly = True
        Me.cInvoiceNo.Width = 150
        '
        'cCustCode
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.NullValue = "0.00"
        Me.cCustCode.DefaultCellStyle = DataGridViewCellStyle9
        Me.cCustCode.Frozen = True
        Me.cCustCode.HeaderText = "Cust Code"
        Me.cCustCode.Name = "cCustCode"
        Me.cCustCode.ReadOnly = True
        Me.cCustCode.Width = 80
        '
        'cTerminalCode
        '
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cTerminalCode.DefaultCellStyle = DataGridViewCellStyle10
        Me.cTerminalCode.Frozen = True
        Me.cTerminalCode.HeaderText = "Terminal Code"
        Me.cTerminalCode.Name = "cTerminalCode"
        Me.cTerminalCode.ReadOnly = True
        '
        'cSalesmanCode
        '
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cSalesmanCode.DefaultCellStyle = DataGridViewCellStyle11
        Me.cSalesmanCode.Frozen = True
        Me.cSalesmanCode.HeaderText = "SalesManCode"
        Me.cSalesmanCode.Name = "cSalesmanCode"
        Me.cSalesmanCode.ReadOnly = True
        '
        'cInvoicePaymode
        '
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cInvoicePaymode.DefaultCellStyle = DataGridViewCellStyle12
        Me.cInvoicePaymode.Frozen = True
        Me.cInvoicePaymode.HeaderText = "Paymode"
        Me.cInvoicePaymode.Name = "cInvoicePaymode"
        Me.cInvoicePaymode.ReadOnly = True
        Me.cInvoicePaymode.Width = 175
        '
        'cNetAmount
        '
        DataGridViewCellStyle13.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cNetAmount.DefaultCellStyle = DataGridViewCellStyle13
        Me.cNetAmount.Frozen = True
        Me.cNetAmount.HeaderText = "NetAmount"
        Me.cNetAmount.Name = "cNetAmount"
        Me.cNetAmount.ReadOnly = True
        '
        'cPaidAmt
        '
        DataGridViewCellStyle14.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cPaidAmt.DefaultCellStyle = DataGridViewCellStyle14
        Me.cPaidAmt.Frozen = True
        Me.cPaidAmt.HeaderText = "Paid Amt"
        Me.cPaidAmt.Name = "cPaidAmt"
        Me.cPaidAmt.ReadOnly = True
        '
        'cChange
        '
        DataGridViewCellStyle15.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cChange.DefaultCellStyle = DataGridViewCellStyle15
        Me.cChange.Frozen = True
        Me.cChange.HeaderText = "Change"
        Me.cChange.Name = "cChange"
        Me.cChange.ReadOnly = True
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(662, 442)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(111, 23)
        Me.Label7.TabIndex = 22
        Me.Label7.Text = "Balance"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(662, 402)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(111, 23)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "Paid"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(662, 364)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(136, 25)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "Net Amount"
        '
        'lblBalance
        '
        Me.lblBalance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblBalance.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalance.ForeColor = System.Drawing.Color.Black
        Me.lblBalance.Location = New System.Drawing.Point(804, 436)
        Me.lblBalance.Name = "lblBalance"
        Me.lblBalance.Size = New System.Drawing.Size(139, 36)
        Me.lblBalance.TabIndex = 19
        Me.lblBalance.Tag = "T;"
        Me.lblBalance.Text = "0.00"
        Me.lblBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPaid
        '
        Me.lblPaid.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblPaid.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaid.ForeColor = System.Drawing.Color.Black
        Me.lblPaid.Location = New System.Drawing.Point(804, 396)
        Me.lblPaid.Name = "lblPaid"
        Me.lblPaid.Size = New System.Drawing.Size(139, 36)
        Me.lblPaid.TabIndex = 18
        Me.lblPaid.Tag = "T;"
        Me.lblPaid.Text = "0.00"
        Me.lblPaid.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotal
        '
        Me.lblTotal.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotal.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.ForeColor = System.Drawing.Color.Black
        Me.lblTotal.Location = New System.Drawing.Point(804, 356)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(139, 36)
        Me.lblTotal.TabIndex = 17
        Me.lblTotal.Tag = "T;"
        Me.lblTotal.Text = "0.00"
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(662, 324)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(119, 30)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "Invoice No"
        '
        'lblInvoice
        '
        Me.lblInvoice.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblInvoice.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvoice.ForeColor = System.Drawing.Color.Black
        Me.lblInvoice.Location = New System.Drawing.Point(804, 318)
        Me.lblInvoice.Name = "lblInvoice"
        Me.lblInvoice.Size = New System.Drawing.Size(139, 36)
        Me.lblInvoice.TabIndex = 24
        Me.lblInvoice.Tag = "T;"
        Me.lblInvoice.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmdCancel
        '
        Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.Location = New System.Drawing.Point(843, 578)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(100, 32)
        Me.cmdCancel.TabIndex = 26
        Me.cmdCancel.Text = "Cancel (Esc)"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.Location = New System.Drawing.Point(843, 502)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(100, 32)
        Me.cmdSave.TabIndex = 25
        Me.cmdSave.Text = "Save (F4)"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmdClear
        '
        Me.cmdClear.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClear.Location = New System.Drawing.Point(843, 540)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.Size = New System.Drawing.Size(100, 32)
        Me.cmdClear.TabIndex = 27
        Me.cmdClear.Text = "Clear (F6)"
        Me.cmdClear.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(270, 100)
        Me.PictureBox1.TabIndex = 56
        Me.PictureBox1.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label2.Location = New System.Drawing.Point(15, 125)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 13)
        Me.Label2.TabIndex = 517
        Me.Label2.Text = "Invoice No"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvoiceNo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtInvoiceNo.Location = New System.Drawing.Point(98, 123)
        Me.txtInvoiceNo.MaxLength = 50
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(188, 21)
        Me.txtInvoiceNo.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label3.Location = New System.Drawing.Point(354, 127)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 13)
        Me.Label3.TabIndex = 519
        Me.Label3.Text = "Remarks"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRemarks
        '
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtRemarks.Location = New System.Drawing.Point(437, 125)
        Me.txtRemarks.MaxLength = 50
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(506, 21)
        Me.txtRemarks.TabIndex = 520
        '
        'PnlSalesManPwd
        '
        Me.PnlSalesManPwd.BackColor = System.Drawing.Color.Pink
        Me.PnlSalesManPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PnlSalesManPwd.Controls.Add(Me.txtSalesManPWD)
        Me.PnlSalesManPwd.Controls.Add(Me.Label21)
        Me.PnlSalesManPwd.Location = New System.Drawing.Point(378, 267)
        Me.PnlSalesManPwd.Name = "PnlSalesManPwd"
        Me.PnlSalesManPwd.Size = New System.Drawing.Size(199, 88)
        Me.PnlSalesManPwd.TabIndex = 521
        Me.PnlSalesManPwd.Visible = False
        '
        'txtSalesManPWD
        '
        Me.txtSalesManPWD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesManPWD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesManPWD.ForeColor = System.Drawing.Color.Black
        Me.txtSalesManPWD.Location = New System.Drawing.Point(48, 40)
        Me.txtSalesManPWD.MaxLength = 20
        Me.txtSalesManPWD.Name = "txtSalesManPWD"
        Me.txtSalesManPWD.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtSalesManPWD.Size = New System.Drawing.Size(136, 20)
        Me.txtSalesManPWD.TabIndex = 0
        Me.txtSalesManPWD.Tag = ""
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Black
        Me.Label21.Location = New System.Drawing.Point(48, 14)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(128, 15)
        Me.Label21.TabIndex = 74
        Me.Label21.Text = "SalesMan Password"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmPaymodeModification
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(955, 623)
        Me.Controls.Add(Me.PnlSalesManPwd)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtRemarks)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtInvoiceNo)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.cmdClear)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.lblInvoice)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblBalance)
        Me.Controls.Add(Me.lblPaid)
        Me.Controls.Add(Me.lblTotal)
        Me.Controls.Add(Me.dgvBills)
        Me.Controls.Add(Me.dgvPaymode)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPaymodeModification"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Paymode Modification"
        CType(Me.dgvPaymode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvBills, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlSalesManPwd.ResumeLayout(False)
        Me.PnlSalesManPwd.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvPaymode As System.Windows.Forms.DataGridView
    Friend WithEvents dgvBills As System.Windows.Forms.DataGridView
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblBalance As System.Windows.Forms.Label
    Friend WithEvents lblPaid As System.Windows.Forms.Label
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblInvoice As System.Windows.Forms.Label
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdClear As System.Windows.Forms.Button
    Friend WithEvents cPaymode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSlipNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cNewAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cNewSlipNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cInvoiceNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCustCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cTerminalCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSalesmanCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cInvoicePaymode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cNetAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cPaidAmt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cChange As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtInvoiceNo As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents PnlSalesManPwd As System.Windows.Forms.Panel
    Friend WithEvents txtSalesManPWD As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
End Class
