<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTradeInSplit
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTradeInSplit))
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtStWeight = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chkScrap = New System.Windows.Forms.CheckBox()
        Me.txtSplitInvWeight = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtSplitInvCode = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtOriginalInvWeight = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnTradeBillNoSearch = New System.Windows.Forms.Button()
        Me.btnSplit = New System.Windows.Forms.Button()
        Me.txtSplitWeight = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtWeight = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtPurity = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtInventoryCode = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnInventoryCodeSearch = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.txtSplitStWeight = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(2, 3)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 100)
        Me.pic.TabIndex = 138
        Me.pic.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txtSplitStWeight)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.txtStWeight)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.chkScrap)
        Me.Panel1.Controls.Add(Me.txtSplitInvWeight)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txtSplitInvCode)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.txtOriginalInvWeight)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.btnTradeBillNoSearch)
        Me.Panel1.Controls.Add(Me.btnSplit)
        Me.Panel1.Controls.Add(Me.txtSplitWeight)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.txtWeight)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtPurity)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtInventoryCode)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.btnInventoryCodeSearch)
        Me.Panel1.Location = New System.Drawing.Point(2, 119)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(560, 216)
        Me.Panel1.TabIndex = 139
        '
        'txtStWeight
        '
        Me.txtStWeight.BackColor = System.Drawing.Color.White
        Me.txtStWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStWeight.Enabled = False
        Me.txtStWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStWeight.ForeColor = System.Drawing.Color.Black
        Me.txtStWeight.Location = New System.Drawing.Point(104, 89)
        Me.txtStWeight.MaxLength = 20
        Me.txtStWeight.Name = "txtStWeight"
        Me.txtStWeight.Size = New System.Drawing.Size(96, 20)
        Me.txtStWeight.TabIndex = 3865
        Me.txtStWeight.Tag = "NE;"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(8, 88)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 33)
        Me.Label7.TabIndex = 3866
        Me.Label7.Text = "Stone Weight"
        '
        'chkScrap
        '
        Me.chkScrap.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScrap.ForeColor = System.Drawing.Color.Black
        Me.chkScrap.Location = New System.Drawing.Point(398, 89)
        Me.chkScrap.Name = "chkScrap"
        Me.chkScrap.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkScrap.Size = New System.Drawing.Size(62, 16)
        Me.chkScrap.TabIndex = 3864
        Me.chkScrap.Text = "Scrap"
        '
        'txtSplitInvWeight
        '
        Me.txtSplitInvWeight.BackColor = System.Drawing.Color.White
        Me.txtSplitInvWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSplitInvWeight.Enabled = False
        Me.txtSplitInvWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSplitInvWeight.ForeColor = System.Drawing.Color.Black
        Me.txtSplitInvWeight.Location = New System.Drawing.Point(102, 162)
        Me.txtSplitInvWeight.MaxLength = 20
        Me.txtSplitInvWeight.Name = "txtSplitInvWeight"
        Me.txtSplitInvWeight.Size = New System.Drawing.Size(97, 20)
        Me.txtSplitInvWeight.TabIndex = 3862
        Me.txtSplitInvWeight.Tag = "NE;"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(8, 158)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 53)
        Me.Label3.TabIndex = 3863
        Me.Label3.Text = "Split Inventory Weight"
        '
        'txtSplitInvCode
        '
        Me.txtSplitInvCode.BackColor = System.Drawing.Color.White
        Me.txtSplitInvCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSplitInvCode.Enabled = False
        Me.txtSplitInvCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSplitInvCode.ForeColor = System.Drawing.Color.Black
        Me.txtSplitInvCode.Location = New System.Drawing.Point(295, 89)
        Me.txtSplitInvCode.MaxLength = 20
        Me.txtSplitInvCode.Name = "txtSplitInvCode"
        Me.txtSplitInvCode.Size = New System.Drawing.Size(97, 20)
        Me.txtSplitInvCode.TabIndex = 3860
        Me.txtSplitInvCode.Tag = "NE;"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(213, 91)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(89, 29)
        Me.Label6.TabIndex = 3861
        Me.Label6.Text = "Split Inventory Code"
        '
        'txtOriginalInvWeight
        '
        Me.txtOriginalInvWeight.BackColor = System.Drawing.Color.White
        Me.txtOriginalInvWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOriginalInvWeight.Enabled = False
        Me.txtOriginalInvWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOriginalInvWeight.ForeColor = System.Drawing.Color.Black
        Me.txtOriginalInvWeight.Location = New System.Drawing.Point(103, 126)
        Me.txtOriginalInvWeight.MaxLength = 20
        Me.txtOriginalInvWeight.Name = "txtOriginalInvWeight"
        Me.txtOriginalInvWeight.Size = New System.Drawing.Size(96, 20)
        Me.txtOriginalInvWeight.TabIndex = 3858
        Me.txtOriginalInvWeight.Tag = "NE;"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(8, 126)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 33)
        Me.Label4.TabIndex = 3859
        Me.Label4.Text = "Original Inv Weight"
        '
        'btnTradeBillNoSearch
        '
        Me.btnTradeBillNoSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnTradeBillNoSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTradeBillNoSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTradeBillNoSearch.Location = New System.Drawing.Point(176, 24)
        Me.btnTradeBillNoSearch.Name = "btnTradeBillNoSearch"
        Me.btnTradeBillNoSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnTradeBillNoSearch.TabIndex = 3857
        Me.btnTradeBillNoSearch.Text = "..."
        Me.btnTradeBillNoSearch.UseVisualStyleBackColor = False
        '
        'btnSplit
        '
        Me.btnSplit.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSplit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSplit.Location = New System.Drawing.Point(418, 24)
        Me.btnSplit.Name = "btnSplit"
        Me.btnSplit.Size = New System.Drawing.Size(80, 43)
        Me.btnSplit.TabIndex = 5
        Me.btnSplit.Text = "Split"
        Me.btnSplit.UseVisualStyleBackColor = False
        '
        'txtSplitWeight
        '
        Me.txtSplitWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSplitWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSplitWeight.Location = New System.Drawing.Point(295, 52)
        Me.txtSplitWeight.MaxLength = 20
        Me.txtSplitWeight.Name = "txtSplitWeight"
        Me.txtSplitWeight.Size = New System.Drawing.Size(97, 20)
        Me.txtSplitWeight.TabIndex = 4
        Me.txtSplitWeight.Tag = "NE;"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(213, 56)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(72, 16)
        Me.Label8.TabIndex = 56
        Me.Label8.Text = "SplitWeight"
        '
        'txtWeight
        '
        Me.txtWeight.BackColor = System.Drawing.Color.White
        Me.txtWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWeight.Enabled = False
        Me.txtWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeight.ForeColor = System.Drawing.Color.Black
        Me.txtWeight.Location = New System.Drawing.Point(103, 56)
        Me.txtWeight.MaxLength = 20
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.Size = New System.Drawing.Size(97, 20)
        Me.txtWeight.TabIndex = 3
        Me.txtWeight.Tag = "NE;"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 60)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 16)
        Me.Label5.TabIndex = 52
        Me.Label5.Text = "Weight"
        '
        'txtPurity
        '
        Me.txtPurity.BackColor = System.Drawing.Color.White
        Me.txtPurity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurity.Enabled = False
        Me.txtPurity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurity.ForeColor = System.Drawing.Color.Black
        Me.txtPurity.Location = New System.Drawing.Point(295, 22)
        Me.txtPurity.MaxLength = 50
        Me.txtPurity.Name = "txtPurity"
        Me.txtPurity.Size = New System.Drawing.Size(97, 20)
        Me.txtPurity.TabIndex = 2
        Me.txtPurity.Tag = "NE;"
        Me.txtPurity.Visible = False
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(213, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 16)
        Me.Label2.TabIndex = 40
        Me.Label2.Text = "Purity"
        Me.Label2.Visible = False
        '
        'txtInventoryCode
        '
        Me.txtInventoryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInventoryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInventoryCode.Location = New System.Drawing.Point(103, 24)
        Me.txtInventoryCode.MaxLength = 20
        Me.txtInventoryCode.Name = "txtInventoryCode"
        Me.txtInventoryCode.Size = New System.Drawing.Size(96, 20)
        Me.txtInventoryCode.TabIndex = 0
        Me.txtInventoryCode.Tag = "NE;"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 16)
        Me.Label1.TabIndex = 38
        Me.Label1.Text = "Inventory Code"
        '
        'btnInventoryCodeSearch
        '
        Me.btnInventoryCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnInventoryCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInventoryCodeSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInventoryCodeSearch.Location = New System.Drawing.Point(175, 24)
        Me.btnInventoryCodeSearch.Name = "btnInventoryCodeSearch"
        Me.btnInventoryCodeSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnInventoryCodeSearch.TabIndex = 1
        Me.btnInventoryCodeSearch.Text = "..."
        Me.btnInventoryCodeSearch.UseVisualStyleBackColor = False
        Me.btnInventoryCodeSearch.Visible = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(245, 350)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 40)
        Me.btnSave.TabIndex = 140
        Me.btnSave.Text = "Update(F7)"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.Location = New System.Drawing.Point(351, 350)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(100, 40)
        Me.btnClear.TabIndex = 141
        Me.btnClear.Text = "Clear(F6)"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(457, 350)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 142
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'txtSplitStWeight
        '
        Me.txtSplitStWeight.BackColor = System.Drawing.Color.White
        Me.txtSplitStWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSplitStWeight.Enabled = False
        Me.txtSplitStWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSplitStWeight.ForeColor = System.Drawing.Color.Black
        Me.txtSplitStWeight.Location = New System.Drawing.Point(295, 158)
        Me.txtSplitStWeight.MaxLength = 20
        Me.txtSplitStWeight.Name = "txtSplitStWeight"
        Me.txtSplitStWeight.Size = New System.Drawing.Size(97, 20)
        Me.txtSplitStWeight.TabIndex = 3867
        Me.txtSplitStWeight.Tag = "NE;"
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(213, 154)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(72, 53)
        Me.Label9.TabIndex = 3868
        Me.Label9.Text = "Split Stone Weight"
        '
        'frmTradeInSplit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(599, 402)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pic)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmTradeInSplit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TradeInSplit"
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnSplit As System.Windows.Forms.Button
    Friend WithEvents txtSplitWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtPurity As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtInventoryCode As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnInventoryCodeSearch As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnTradeBillNoSearch As System.Windows.Forms.Button
    Friend WithEvents txtOriginalInvWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtSplitInvCode As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtSplitInvWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chkScrap As System.Windows.Forms.CheckBox
    Friend WithEvents txtStWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtSplitStWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
End Class
