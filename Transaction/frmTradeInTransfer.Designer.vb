<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTradeInTransfer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTradeInTransfer))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnTradeBillNoSearch = New System.Windows.Forms.Button()
        Me.txtSupplierName = New System.Windows.Forms.TextBox()
        Me.btnSupplierCodeSearch = New System.Windows.Forms.Button()
        Me.txtsupplierCode = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTradeInventoryCode = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.pnlInvoice = New System.Windows.Forms.Panel()
        Me.lblSelCost = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblSelweight = New System.Windows.Forms.Label()
        Me.grdInvoice = New System.Windows.Forms.DataGridView()
        Me.btnPanelClose = New System.Windows.Forms.Button()
        Me.chkSelectAll = New System.Windows.Forms.CheckBox()
        Me.btnPanelOK = New System.Windows.Forms.Button()
        Me.cSel = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.cInventoryCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cIDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotalCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnProceed = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnBillClear = New System.Windows.Forms.Button()
        Me.pnlDate = New System.Windows.Forms.Panel()
        Me.optTransferOut = New System.Windows.Forms.RadioButton()
        Me.optTransferIn = New System.Windows.Forms.RadioButton()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.pnlInvoice.SuspendLayout()
        CType(Me.grdInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDate.SuspendLayout()
        Me.SuspendLayout()
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(1, 1)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 100)
        Me.pic.TabIndex = 492
        Me.pic.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.pnlDate)
        Me.Panel1.Controls.Add(Me.btnTradeBillNoSearch)
        Me.Panel1.Controls.Add(Me.txtSupplierName)
        Me.Panel1.Controls.Add(Me.btnSupplierCodeSearch)
        Me.Panel1.Controls.Add(Me.txtsupplierCode)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtTradeInventoryCode)
        Me.Panel1.Controls.Add(Me.txtRemarks)
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Location = New System.Drawing.Point(3, 107)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(616, 116)
        Me.Panel1.TabIndex = 493
        '
        'btnTradeBillNoSearch
        '
        Me.btnTradeBillNoSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnTradeBillNoSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTradeBillNoSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTradeBillNoSearch.Location = New System.Drawing.Point(202, 39)
        Me.btnTradeBillNoSearch.Name = "btnTradeBillNoSearch"
        Me.btnTradeBillNoSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnTradeBillNoSearch.TabIndex = 3865
        Me.btnTradeBillNoSearch.Text = "..."
        Me.btnTradeBillNoSearch.UseVisualStyleBackColor = False
        '
        'txtSupplierName
        '
        Me.txtSupplierName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierName.ForeColor = System.Drawing.Color.Black
        Me.txtSupplierName.Location = New System.Drawing.Point(232, 13)
        Me.txtSupplierName.MaxLength = 20
        Me.txtSupplierName.Name = "txtSupplierName"
        Me.txtSupplierName.ReadOnly = True
        Me.txtSupplierName.Size = New System.Drawing.Size(272, 20)
        Me.txtSupplierName.TabIndex = 3864
        Me.txtSupplierName.Tag = ""
        '
        'btnSupplierCodeSearch
        '
        Me.btnSupplierCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSupplierCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSupplierCodeSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSupplierCodeSearch.Location = New System.Drawing.Point(202, 12)
        Me.btnSupplierCodeSearch.Name = "btnSupplierCodeSearch"
        Me.btnSupplierCodeSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnSupplierCodeSearch.TabIndex = 3863
        Me.btnSupplierCodeSearch.Text = "..."
        Me.btnSupplierCodeSearch.UseVisualStyleBackColor = False
        '
        'txtsupplierCode
        '
        Me.txtsupplierCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtsupplierCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsupplierCode.ForeColor = System.Drawing.Color.Black
        Me.txtsupplierCode.Location = New System.Drawing.Point(109, 12)
        Me.txtsupplierCode.MaxLength = 20
        Me.txtsupplierCode.Name = "txtsupplierCode"
        Me.txtsupplierCode.Size = New System.Drawing.Size(96, 20)
        Me.txtsupplierCode.TabIndex = 3861
        Me.txtsupplierCode.Tag = ""
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(4, 12)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 15)
        Me.Label5.TabIndex = 3862
        Me.Label5.Text = "Supplier"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(4, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 30)
        Me.Label1.TabIndex = 3859
        Me.Label1.Text = "Trade Inventory Code"
        '
        'txtTradeInventoryCode
        '
        Me.txtTradeInventoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTradeInventoryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTradeInventoryCode.ForeColor = System.Drawing.Color.Black
        Me.txtTradeInventoryCode.Location = New System.Drawing.Point(109, 39)
        Me.txtTradeInventoryCode.MaxLength = 20
        Me.txtTradeInventoryCode.Name = "txtTradeInventoryCode"
        Me.txtTradeInventoryCode.Size = New System.Drawing.Size(115, 20)
        Me.txtTradeInventoryCode.TabIndex = 3858
        Me.txtTradeInventoryCode.Tag = ""
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.Black
        Me.txtRemarks.Location = New System.Drawing.Point(109, 65)
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
        Me.Label17.Location = New System.Drawing.Point(3, 67)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(71, 20)
        Me.Label17.TabIndex = 3852
        Me.Label17.Text = "Remarks"
        '
        'pnlInvoice
        '
        Me.pnlInvoice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlInvoice.Controls.Add(Me.lblSelCost)
        Me.pnlInvoice.Controls.Add(Me.Label15)
        Me.pnlInvoice.Controls.Add(Me.lblSelweight)
        Me.pnlInvoice.Controls.Add(Me.grdInvoice)
        Me.pnlInvoice.Controls.Add(Me.btnPanelClose)
        Me.pnlInvoice.Controls.Add(Me.chkSelectAll)
        Me.pnlInvoice.Controls.Add(Me.btnPanelOK)
        Me.pnlInvoice.Location = New System.Drawing.Point(3, 229)
        Me.pnlInvoice.Name = "pnlInvoice"
        Me.pnlInvoice.Size = New System.Drawing.Size(616, 256)
        Me.pnlInvoice.TabIndex = 494
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
        Me.Label15.Visible = False
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
        Me.grdInvoice.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cSel, Me.cInventoryCode, Me.cIDate, Me.cWeight, Me.TotalCost})
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
        Me.btnPanelClose.Visible = False
        '
        'chkSelectAll
        '
        Me.chkSelectAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSelectAll.Location = New System.Drawing.Point(8, 224)
        Me.chkSelectAll.Name = "chkSelectAll"
        Me.chkSelectAll.Size = New System.Drawing.Size(80, 24)
        Me.chkSelectAll.TabIndex = 138
        Me.chkSelectAll.Text = "Select All"
        Me.chkSelectAll.Visible = False
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
        Me.btnPanelOK.Visible = False
        '
        'cSel
        '
        Me.cSel.HeaderText = "Sel"
        Me.cSel.Name = "cSel"
        Me.cSel.Width = 30
        '
        'cInventoryCode
        '
        Me.cInventoryCode.HeaderText = "Inventory Code"
        Me.cInventoryCode.Name = "cInventoryCode"
        '
        'cIDate
        '
        Me.cIDate.HeaderText = "Date"
        Me.cIDate.Name = "cIDate"
        '
        'cWeight
        '
        Me.cWeight.HeaderText = "Weight"
        Me.cWeight.Name = "cWeight"
        Me.cWeight.Width = 80
        '
        'TotalCost
        '
        Me.TotalCost.HeaderText = "TotalCost"
        Me.TotalCost.Name = "TotalCost"
        '
        'btnProceed
        '
        Me.btnProceed.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnProceed.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnProceed.Location = New System.Drawing.Point(431, 491)
        Me.btnProceed.Name = "btnProceed"
        Me.btnProceed.Size = New System.Drawing.Size(92, 38)
        Me.btnProceed.TabIndex = 495
        Me.btnProceed.Text = "Proceed (F7)"
        Me.btnProceed.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(527, 491)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(92, 38)
        Me.btnClose.TabIndex = 497
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnBillClear
        '
        Me.btnBillClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnBillClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBillClear.Location = New System.Drawing.Point(335, 491)
        Me.btnBillClear.Name = "btnBillClear"
        Me.btnBillClear.Size = New System.Drawing.Size(92, 38)
        Me.btnBillClear.TabIndex = 496
        Me.btnBillClear.Text = "Clear (F6)"
        Me.btnBillClear.UseVisualStyleBackColor = False
        '
        'pnlDate
        '
        Me.pnlDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnlDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDate.Controls.Add(Me.optTransferOut)
        Me.pnlDate.Controls.Add(Me.optTransferIn)
        Me.pnlDate.Location = New System.Drawing.Point(6, 88)
        Me.pnlDate.Name = "pnlDate"
        Me.pnlDate.Size = New System.Drawing.Size(216, 27)
        Me.pnlDate.TabIndex = 3866
        '
        'optTransferOut
        '
        Me.optTransferOut.AutoSize = True
        Me.optTransferOut.Checked = True
        Me.optTransferOut.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTransferOut.Location = New System.Drawing.Point(11, 4)
        Me.optTransferOut.Name = "optTransferOut"
        Me.optTransferOut.Size = New System.Drawing.Size(95, 18)
        Me.optTransferOut.TabIndex = 1
        Me.optTransferOut.TabStop = True
        Me.optTransferOut.Text = "Transfer Out"
        Me.optTransferOut.UseVisualStyleBackColor = True
        '
        'optTransferIn
        '
        Me.optTransferIn.AutoSize = True
        Me.optTransferIn.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTransferIn.Location = New System.Drawing.Point(112, 4)
        Me.optTransferIn.Name = "optTransferIn"
        Me.optTransferIn.Size = New System.Drawing.Size(86, 18)
        Me.optTransferIn.TabIndex = 0
        Me.optTransferIn.Text = "Transfer In"
        Me.optTransferIn.UseVisualStyleBackColor = True
        '
        'frmTradeInTransfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(623, 540)
        Me.Controls.Add(Me.btnProceed)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnBillClear)
        Me.Controls.Add(Me.pnlInvoice)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pic)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmTradeInTransfer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TradeIn Transfer"
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlInvoice.ResumeLayout(False)
        CType(Me.grdInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDate.ResumeLayout(False)
        Me.pnlDate.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTradeInventoryCode As System.Windows.Forms.TextBox
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents btnSupplierCodeSearch As System.Windows.Forms.Button
    Friend WithEvents txtsupplierCode As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtSupplierName As System.Windows.Forms.TextBox
    Friend WithEvents btnTradeBillNoSearch As System.Windows.Forms.Button
    Friend WithEvents pnlInvoice As System.Windows.Forms.Panel
    Friend WithEvents lblSelCost As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lblSelweight As System.Windows.Forms.Label
    Friend WithEvents grdInvoice As System.Windows.Forms.DataGridView
    Friend WithEvents btnPanelClose As System.Windows.Forms.Button
    Friend WithEvents chkSelectAll As System.Windows.Forms.CheckBox
    Friend WithEvents btnPanelOK As System.Windows.Forms.Button
    Friend WithEvents cSel As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents cInventoryCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cIDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TotalCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnProceed As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnBillClear As System.Windows.Forms.Button
    Friend WithEvents pnlDate As System.Windows.Forms.Panel
    Friend WithEvents optTransferOut As System.Windows.Forms.RadioButton
    Friend WithEvents optTransferIn As System.Windows.Forms.RadioButton
End Class
