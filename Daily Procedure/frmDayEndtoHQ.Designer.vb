<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDayEndtoHQ
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDayEndtoHQ))
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtYYYY = New System.Windows.Forms.TextBox()
        Me.txtMM = New System.Windows.Forms.TextBox()
        Me.txtDD = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtLYYYY = New System.Windows.Forms.TextBox()
        Me.txtLMM = New System.Windows.Forms.TextBox()
        Me.txtLDD = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkRepairEntry = New System.Windows.Forms.CheckBox()
        Me.chkOrderEntry = New System.Windows.Forms.CheckBox()
        Me.chkSalesReturn = New System.Windows.Forms.CheckBox()
        Me.chkSalesDetail = New System.Windows.Forms.CheckBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.chkTradeIn = New System.Windows.Forms.CheckBox()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(1, 12)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 100)
        Me.pic.TabIndex = 514
        Me.pic.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.txtYYYY)
        Me.GroupBox2.Controls.Add(Me.txtMM)
        Me.GroupBox2.Controls.Add(Me.txtDD)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.txtLYYYY)
        Me.GroupBox2.Controls.Add(Me.txtLMM)
        Me.GroupBox2.Controls.Add(Me.txtLDD)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Location = New System.Drawing.Point(1, 118)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(287, 87)
        Me.GroupBox2.TabIndex = 515
        Me.GroupBox2.TabStop = False
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(176, 53)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(8, 16)
        Me.Label1.TabIndex = 70
        Me.Label1.Text = "/"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(139, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(8, 16)
        Me.Label2.TabIndex = 69
        Me.Label2.Text = "/"
        '
        'txtYYYY
        '
        Me.txtYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtYYYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtYYYY.Location = New System.Drawing.Point(187, 53)
        Me.txtYYYY.MaxLength = 20
        Me.txtYYYY.Name = "txtYYYY"
        Me.txtYYYY.Size = New System.Drawing.Size(48, 20)
        Me.txtYYYY.TabIndex = 23
        Me.txtYYYY.Tag = "NE;"
        '
        'txtMM
        '
        Me.txtMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMM.Location = New System.Drawing.Point(149, 53)
        Me.txtMM.MaxLength = 20
        Me.txtMM.Name = "txtMM"
        Me.txtMM.Size = New System.Drawing.Size(24, 20)
        Me.txtMM.TabIndex = 22
        Me.txtMM.Tag = "NE;"
        '
        'txtDD
        '
        Me.txtDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDD.Location = New System.Drawing.Point(114, 53)
        Me.txtDD.MaxLength = 20
        Me.txtDD.Name = "txtDD"
        Me.txtDD.Size = New System.Drawing.Size(25, 20)
        Me.txtDD.TabIndex = 21
        Me.txtDD.Tag = "NE;"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(20, 53)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 16)
        Me.Label3.TabIndex = 68
        Me.Label3.Text = "EOD Date"
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(176, 25)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(8, 16)
        Me.Label10.TabIndex = 64
        Me.Label10.Text = "/"
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(139, 25)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(8, 16)
        Me.Label9.TabIndex = 63
        Me.Label9.Text = "/"
        '
        'txtLYYYY
        '
        Me.txtLYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLYYYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLYYYY.Location = New System.Drawing.Point(187, 21)
        Me.txtLYYYY.MaxLength = 20
        Me.txtLYYYY.Name = "txtLYYYY"
        Me.txtLYYYY.ReadOnly = True
        Me.txtLYYYY.Size = New System.Drawing.Size(48, 20)
        Me.txtLYYYY.TabIndex = 20
        Me.txtLYYYY.Tag = "NE;"
        '
        'txtLMM
        '
        Me.txtLMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLMM.Location = New System.Drawing.Point(149, 21)
        Me.txtLMM.MaxLength = 20
        Me.txtLMM.Name = "txtLMM"
        Me.txtLMM.ReadOnly = True
        Me.txtLMM.Size = New System.Drawing.Size(24, 20)
        Me.txtLMM.TabIndex = 19
        Me.txtLMM.Tag = "NE;"
        '
        'txtLDD
        '
        Me.txtLDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLDD.Location = New System.Drawing.Point(114, 21)
        Me.txtLDD.MaxLength = 20
        Me.txtLDD.Name = "txtLDD"
        Me.txtLDD.ReadOnly = True
        Me.txtLDD.Size = New System.Drawing.Size(25, 20)
        Me.txtLDD.TabIndex = 18
        Me.txtLDD.Tag = "NE;"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(17, 21)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(87, 16)
        Me.Label8.TabIndex = 62
        Me.Label8.Text = "Last EOD Date"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.chkTradeIn)
        Me.GroupBox1.Controls.Add(Me.chkRepairEntry)
        Me.GroupBox1.Controls.Add(Me.chkOrderEntry)
        Me.GroupBox1.Controls.Add(Me.chkSalesReturn)
        Me.GroupBox1.Controls.Add(Me.chkSalesDetail)
        Me.GroupBox1.Location = New System.Drawing.Point(1, 211)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(287, 153)
        Me.GroupBox1.TabIndex = 516
        Me.GroupBox1.TabStop = False
        '
        'chkRepairEntry
        '
        Me.chkRepairEntry.AutoSize = True
        Me.chkRepairEntry.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRepairEntry.Location = New System.Drawing.Point(69, 96)
        Me.chkRepairEntry.Name = "chkRepairEntry"
        Me.chkRepairEntry.Size = New System.Drawing.Size(105, 19)
        Me.chkRepairEntry.TabIndex = 3
        Me.chkRepairEntry.Text = "Repair Entry"
        Me.chkRepairEntry.UseVisualStyleBackColor = True
        '
        'chkOrderEntry
        '
        Me.chkOrderEntry.AutoSize = True
        Me.chkOrderEntry.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOrderEntry.Location = New System.Drawing.Point(69, 71)
        Me.chkOrderEntry.Name = "chkOrderEntry"
        Me.chkOrderEntry.Size = New System.Drawing.Size(98, 19)
        Me.chkOrderEntry.TabIndex = 2
        Me.chkOrderEntry.Text = "Order Entry"
        Me.chkOrderEntry.UseVisualStyleBackColor = True
        '
        'chkSalesReturn
        '
        Me.chkSalesReturn.AutoSize = True
        Me.chkSalesReturn.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSalesReturn.Location = New System.Drawing.Point(69, 46)
        Me.chkSalesReturn.Name = "chkSalesReturn"
        Me.chkSalesReturn.Size = New System.Drawing.Size(109, 19)
        Me.chkSalesReturn.TabIndex = 1
        Me.chkSalesReturn.Text = "Sales Return"
        Me.chkSalesReturn.UseVisualStyleBackColor = True
        '
        'chkSalesDetail
        '
        Me.chkSalesDetail.AutoSize = True
        Me.chkSalesDetail.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSalesDetail.Location = New System.Drawing.Point(71, 19)
        Me.chkSalesDetail.Name = "chkSalesDetail"
        Me.chkSalesDetail.Size = New System.Drawing.Size(104, 19)
        Me.chkSalesDetail.TabIndex = 0
        Me.chkSalesDetail.Text = "Sales Detail"
        Me.chkSalesDetail.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(79, 370)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 40)
        Me.btnSave.TabIndex = 517
        Me.btnSave.Text = "Save (F7)"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(180, 370)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 518
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'chkTradeIn
        '
        Me.chkTradeIn.AutoSize = True
        Me.chkTradeIn.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTradeIn.Location = New System.Drawing.Point(69, 121)
        Me.chkTradeIn.Name = "chkTradeIn"
        Me.chkTradeIn.Size = New System.Drawing.Size(114, 19)
        Me.chkTradeIn.TabIndex = 4
        Me.chkTradeIn.Text = "TradeIn Stock"
        Me.chkTradeIn.UseVisualStyleBackColor = True
        '
        'frmDayEndtoHQ
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(292, 432)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.pic)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmDayEndtoHQ"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DayEnd to HQ"
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtYYYY As System.Windows.Forms.TextBox
    Friend WithEvents txtMM As System.Windows.Forms.TextBox
    Friend WithEvents txtDD As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtLYYYY As System.Windows.Forms.TextBox
    Friend WithEvents txtLMM As System.Windows.Forms.TextBox
    Friend WithEvents txtLDD As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents chkRepairEntry As System.Windows.Forms.CheckBox
    Friend WithEvents chkOrderEntry As System.Windows.Forms.CheckBox
    Friend WithEvents chkSalesReturn As System.Windows.Forms.CheckBox
    Friend WithEvents chkSalesDetail As System.Windows.Forms.CheckBox
    Friend WithEvents chkTradeIn As System.Windows.Forms.CheckBox
End Class
