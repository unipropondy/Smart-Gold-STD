<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTransferList
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTransferList))
        Me.dgvTransfer = New System.Windows.Forms.DataGridView()
        Me.colTransferNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTransferDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFromLoc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colToLoc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colLocation = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNoofQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColNoofItems = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.rbtnTransferout = New System.Windows.Forms.RadioButton()
        Me.rbtnTransferIn = New System.Windows.Forms.RadioButton()
        Me.cmdFetch = New System.Windows.Forms.Button()
        Me.btnPending = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTransferYear = New System.Windows.Forms.TextBox()
        Me.txtTransferDate = New System.Windows.Forms.TextBox()
        Me.txtTransferMonth = New System.Windows.Forms.TextBox()
        Me.cboLocation = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.btnTransferClear = New System.Windows.Forms.Button()
        Me.btnView = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        CType(Me.dgvTransfer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvTransfer
        '
        Me.dgvTransfer.AllowUserToAddRows = False
        Me.dgvTransfer.AllowUserToDeleteRows = False
        Me.dgvTransfer.AllowUserToOrderColumns = True
        Me.dgvTransfer.AllowUserToResizeColumns = False
        Me.dgvTransfer.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvTransfer.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvTransfer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTransfer.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colTransferNo, Me.colTransferDate, Me.colFromLoc, Me.colToLoc, Me.colLocation, Me.colNoofQty, Me.ColNoofItems})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvTransfer.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvTransfer.Location = New System.Drawing.Point(2, 205)
        Me.dgvTransfer.Name = "dgvTransfer"
        Me.dgvTransfer.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvTransfer.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvTransfer.RowHeadersVisible = False
        Me.dgvTransfer.Size = New System.Drawing.Size(613, 199)
        Me.dgvTransfer.TabIndex = 4
        '
        'colTransferNo
        '
        Me.colTransferNo.HeaderText = "Transfer No"
        Me.colTransferNo.Name = "colTransferNo"
        Me.colTransferNo.ReadOnly = True
        Me.colTransferNo.Width = 110
        '
        'colTransferDate
        '
        Me.colTransferDate.HeaderText = "Transfer Date"
        Me.colTransferDate.Name = "colTransferDate"
        Me.colTransferDate.ReadOnly = True
        Me.colTransferDate.Width = 110
        '
        'colFromLoc
        '
        Me.colFromLoc.HeaderText = "From Loc. Status"
        Me.colFromLoc.Name = "colFromLoc"
        Me.colFromLoc.ReadOnly = True
        Me.colFromLoc.Width = 90
        '
        'colToLoc
        '
        Me.colToLoc.HeaderText = "To Loc. Status"
        Me.colToLoc.Name = "colToLoc"
        Me.colToLoc.ReadOnly = True
        Me.colToLoc.Width = 90
        '
        'colLocation
        '
        Me.colLocation.HeaderText = "Location"
        Me.colLocation.Name = "colLocation"
        Me.colLocation.ReadOnly = True
        Me.colLocation.Width = 90
        '
        'colNoofQty
        '
        Me.colNoofQty.HeaderText = "No. of PCS"
        Me.colNoofQty.Name = "colNoofQty"
        Me.colNoofQty.ReadOnly = True
        Me.colNoofQty.Width = 50
        '
        'ColNoofItems
        '
        Me.ColNoofItems.HeaderText = "No of Items"
        Me.ColNoofItems.Name = "ColNoofItems"
        Me.ColNoofItems.ReadOnly = True
        Me.ColNoofItems.Width = 80
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(2, 2)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 100)
        Me.pic.TabIndex = 512
        Me.pic.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox5)
        Me.GroupBox1.Controls.Add(Me.cmdFetch)
        Me.GroupBox1.Controls.Add(Me.btnPending)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtTransferYear)
        Me.GroupBox1.Controls.Add(Me.txtTransferDate)
        Me.GroupBox1.Controls.Add(Me.txtTransferMonth)
        Me.GroupBox1.Controls.Add(Me.cboLocation)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Location = New System.Drawing.Point(2, 103)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(611, 96)
        Me.GroupBox1.TabIndex = 513
        Me.GroupBox1.TabStop = False
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.rbtnTransferout)
        Me.GroupBox5.Controls.Add(Me.rbtnTransferIn)
        Me.GroupBox5.Location = New System.Drawing.Point(7, 49)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(231, 32)
        Me.GroupBox5.TabIndex = 3848
        Me.GroupBox5.TabStop = False
        '
        'rbtnTransferout
        '
        Me.rbtnTransferout.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnTransferout.Location = New System.Drawing.Point(105, 10)
        Me.rbtnTransferout.Name = "rbtnTransferout"
        Me.rbtnTransferout.Size = New System.Drawing.Size(120, 16)
        Me.rbtnTransferout.TabIndex = 12
        Me.rbtnTransferout.Text = "Transfer Out"
        '
        'rbtnTransferIn
        '
        Me.rbtnTransferIn.Checked = True
        Me.rbtnTransferIn.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnTransferIn.Location = New System.Drawing.Point(4, 10)
        Me.rbtnTransferIn.Name = "rbtnTransferIn"
        Me.rbtnTransferIn.Size = New System.Drawing.Size(104, 16)
        Me.rbtnTransferIn.TabIndex = 11
        Me.rbtnTransferIn.TabStop = True
        Me.rbtnTransferIn.Text = "Transfer In"
        '
        'cmdFetch
        '
        Me.cmdFetch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdFetch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFetch.ForeColor = System.Drawing.Color.Black
        Me.cmdFetch.Location = New System.Drawing.Point(511, 13)
        Me.cmdFetch.Name = "cmdFetch"
        Me.cmdFetch.Size = New System.Drawing.Size(75, 23)
        Me.cmdFetch.TabIndex = 540
        Me.cmdFetch.Text = "Fetch"
        Me.cmdFetch.UseVisualStyleBackColor = False
        '
        'btnPending
        '
        Me.btnPending.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPending.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPending.Location = New System.Drawing.Point(511, 37)
        Me.btnPending.Name = "btnPending"
        Me.btnPending.Size = New System.Drawing.Size(75, 23)
        Me.btnPending.TabIndex = 541
        Me.btnPending.Text = "Pending"
        Me.btnPending.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label1.Location = New System.Drawing.Point(219, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 14)
        Me.Label1.TabIndex = 513
        Me.Label1.Text = "Transfer Date"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTransferYear
        '
        Me.txtTransferYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTransferYear.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransferYear.Location = New System.Drawing.Point(391, 22)
        Me.txtTransferYear.MaxLength = 4
        Me.txtTransferYear.Name = "txtTransferYear"
        Me.txtTransferYear.Size = New System.Drawing.Size(34, 21)
        Me.txtTransferYear.TabIndex = 512
        Me.txtTransferYear.Tag = "NE;"
        '
        'txtTransferDate
        '
        Me.txtTransferDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTransferDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransferDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTransferDate.Location = New System.Drawing.Point(311, 22)
        Me.txtTransferDate.MaxLength = 2
        Me.txtTransferDate.Name = "txtTransferDate"
        Me.txtTransferDate.Size = New System.Drawing.Size(21, 21)
        Me.txtTransferDate.TabIndex = 510
        Me.txtTransferDate.Tag = "NE;"
        '
        'txtTransferMonth
        '
        Me.txtTransferMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTransferMonth.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransferMonth.Location = New System.Drawing.Point(351, 22)
        Me.txtTransferMonth.MaxLength = 2
        Me.txtTransferMonth.Name = "txtTransferMonth"
        Me.txtTransferMonth.Size = New System.Drawing.Size(21, 21)
        Me.txtTransferMonth.TabIndex = 511
        Me.txtTransferMonth.Tag = "NE;"
        '
        'cboLocation
        '
        Me.cboLocation.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLocation.Location = New System.Drawing.Point(89, 19)
        Me.cboLocation.Name = "cboLocation"
        Me.cboLocation.Size = New System.Drawing.Size(117, 24)
        Me.cboLocation.TabIndex = 509
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label15.Location = New System.Drawing.Point(4, 19)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(54, 14)
        Me.Label15.TabIndex = 508
        Me.Label15.Text = "Location"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnTransferClear
        '
        Me.btnTransferClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnTransferClear.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTransferClear.Location = New System.Drawing.Point(407, 425)
        Me.btnTransferClear.Name = "btnTransferClear"
        Me.btnTransferClear.Size = New System.Drawing.Size(100, 40)
        Me.btnTransferClear.TabIndex = 544
        Me.btnTransferClear.Text = "Clear (F6)"
        Me.btnTransferClear.UseVisualStyleBackColor = False
        '
        'btnView
        '
        Me.btnView.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnView.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnView.Location = New System.Drawing.Point(291, 425)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(110, 40)
        Me.btnView.TabIndex = 543
        Me.btnView.Text = "View"
        Me.btnView.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(513, 425)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 545
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnRefresh.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.Location = New System.Drawing.Point(175, 425)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(110, 40)
        Me.btnRefresh.TabIndex = 546
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'frmTransferList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(680, 477)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.btnTransferClear)
        Me.Controls.Add(Me.btnView)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.dgvTransfer)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmTransferList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Transfer List"
        CType(Me.dgvTransfer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvTransfer As System.Windows.Forms.DataGridView
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents colCategoryCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colClassCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colInventoryCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCatalogueCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnTransferClear As System.Windows.Forms.Button
    Friend WithEvents btnView As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents cboLocation As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTransferYear As System.Windows.Forms.TextBox
    Friend WithEvents txtTransferDate As System.Windows.Forms.TextBox
    Friend WithEvents txtTransferMonth As System.Windows.Forms.TextBox
    Friend WithEvents cmdFetch As System.Windows.Forms.Button
    Friend WithEvents btnPending As System.Windows.Forms.Button
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnTransferout As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnTransferIn As System.Windows.Forms.RadioButton
    Friend WithEvents colTransferNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTransferDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFromLoc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colToLoc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colLocation As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colNoofQty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColNoofItems As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
