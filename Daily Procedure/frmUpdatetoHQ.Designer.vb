<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUpdatetoHQ
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUpdatetoHQ))
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtTday = New System.Windows.Forms.TextBox()
        Me.txtfyear = New System.Windows.Forms.TextBox()
        Me.txtFday = New System.Windows.Forms.TextBox()
        Me.txtfMon = New System.Windows.Forms.TextBox()
        Me.txtTmon = New System.Windows.Forms.TextBox()
        Me.txtTYear = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnUpdateSecurity = New System.Windows.Forms.Button()
        Me.btnUpdateTransfer = New System.Windows.Forms.Button()
        Me.btnUpdateMasters = New System.Windows.Forms.Button()
        Me.btnGetNewStock = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(2, 0)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 100)
        Me.pic.TabIndex = 513
        Me.pic.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtTday)
        Me.GroupBox1.Controls.Add(Me.txtfyear)
        Me.GroupBox1.Controls.Add(Me.txtFday)
        Me.GroupBox1.Controls.Add(Me.txtfMon)
        Me.GroupBox1.Controls.Add(Me.txtTmon)
        Me.GroupBox1.Controls.Add(Me.txtTYear)
        Me.GroupBox1.Location = New System.Drawing.Point(2, 106)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(423, 66)
        Me.GroupBox1.TabIndex = 514
        Me.GroupBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label1.Location = New System.Drawing.Point(208, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 13)
        Me.Label1.TabIndex = 513
        Me.Label1.Text = "To Date"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label4.Location = New System.Drawing.Point(6, 21)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 13)
        Me.Label4.TabIndex = 512
        Me.Label4.Text = "From Date"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTday
        '
        Me.txtTday.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTday.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTday.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTday.Location = New System.Drawing.Point(276, 19)
        Me.txtTday.MaxLength = 2
        Me.txtTday.Name = "txtTday"
        Me.txtTday.Size = New System.Drawing.Size(21, 21)
        Me.txtTday.TabIndex = 465
        Me.txtTday.Tag = "NE;"
        '
        'txtfyear
        '
        Me.txtfyear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtfyear.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtfyear.Location = New System.Drawing.Point(168, 19)
        Me.txtfyear.MaxLength = 4
        Me.txtfyear.Name = "txtfyear"
        Me.txtfyear.Size = New System.Drawing.Size(34, 21)
        Me.txtfyear.TabIndex = 464
        Me.txtfyear.Tag = "NE;"
        '
        'txtFday
        '
        Me.txtFday.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFday.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFday.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFday.Location = New System.Drawing.Point(88, 19)
        Me.txtFday.MaxLength = 2
        Me.txtFday.Name = "txtFday"
        Me.txtFday.Size = New System.Drawing.Size(21, 21)
        Me.txtFday.TabIndex = 462
        Me.txtFday.Tag = "NE;"
        '
        'txtfMon
        '
        Me.txtfMon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtfMon.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtfMon.Location = New System.Drawing.Point(128, 19)
        Me.txtfMon.MaxLength = 2
        Me.txtfMon.Name = "txtfMon"
        Me.txtfMon.Size = New System.Drawing.Size(21, 21)
        Me.txtfMon.TabIndex = 463
        Me.txtfMon.Tag = "NE;"
        '
        'txtTmon
        '
        Me.txtTmon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTmon.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTmon.Location = New System.Drawing.Point(316, 19)
        Me.txtTmon.MaxLength = 2
        Me.txtTmon.Name = "txtTmon"
        Me.txtTmon.Size = New System.Drawing.Size(21, 21)
        Me.txtTmon.TabIndex = 466
        Me.txtTmon.Tag = "NE;"
        '
        'txtTYear
        '
        Me.txtTYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTYear.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTYear.ForeColor = System.Drawing.Color.Black
        Me.txtTYear.Location = New System.Drawing.Point(356, 19)
        Me.txtTYear.MaxLength = 4
        Me.txtTYear.Name = "txtTYear"
        Me.txtTYear.Size = New System.Drawing.Size(34, 21)
        Me.txtTYear.TabIndex = 467
        Me.txtTYear.Tag = "NE;"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnUpdateSecurity)
        Me.GroupBox2.Controls.Add(Me.btnUpdateTransfer)
        Me.GroupBox2.Controls.Add(Me.btnUpdateMasters)
        Me.GroupBox2.Controls.Add(Me.btnGetNewStock)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 178)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(422, 236)
        Me.GroupBox2.TabIndex = 515
        Me.GroupBox2.TabStop = False
        '
        'btnUpdateSecurity
        '
        Me.btnUpdateSecurity.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnUpdateSecurity.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnUpdateSecurity.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdateSecurity.Location = New System.Drawing.Point(9, 171)
        Me.btnUpdateSecurity.Name = "btnUpdateSecurity"
        Me.btnUpdateSecurity.Size = New System.Drawing.Size(383, 40)
        Me.btnUpdateSecurity.TabIndex = 498
        Me.btnUpdateSecurity.Text = "Update Security"
        Me.btnUpdateSecurity.UseVisualStyleBackColor = False
        '
        'btnUpdateTransfer
        '
        Me.btnUpdateTransfer.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnUpdateTransfer.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnUpdateTransfer.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdateTransfer.Location = New System.Drawing.Point(9, 125)
        Me.btnUpdateTransfer.Name = "btnUpdateTransfer"
        Me.btnUpdateTransfer.Size = New System.Drawing.Size(383, 40)
        Me.btnUpdateTransfer.TabIndex = 497
        Me.btnUpdateTransfer.Text = "Update Refresh Transfer"
        Me.btnUpdateTransfer.UseVisualStyleBackColor = False
        '
        'btnUpdateMasters
        '
        Me.btnUpdateMasters.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnUpdateMasters.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnUpdateMasters.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdateMasters.Location = New System.Drawing.Point(9, 79)
        Me.btnUpdateMasters.Name = "btnUpdateMasters"
        Me.btnUpdateMasters.Size = New System.Drawing.Size(383, 40)
        Me.btnUpdateMasters.TabIndex = 496
        Me.btnUpdateMasters.Text = "Update Masters"
        Me.btnUpdateMasters.UseVisualStyleBackColor = False
        '
        'btnGetNewStock
        '
        Me.btnGetNewStock.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnGetNewStock.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnGetNewStock.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGetNewStock.Location = New System.Drawing.Point(9, 33)
        Me.btnGetNewStock.Name = "btnGetNewStock"
        Me.btnGetNewStock.Size = New System.Drawing.Size(383, 40)
        Me.btnGetNewStock.TabIndex = 495
        Me.btnGetNewStock.Text = "Get New Stock"
        Me.btnGetNewStock.UseVisualStyleBackColor = False
        Me.btnGetNewStock.Visible = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(325, 435)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 546
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'frmUpdatetoHQ
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(449, 492)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.pic)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmUpdatetoHQ"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Update to HQ"
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtTday As System.Windows.Forms.TextBox
    Friend WithEvents txtfyear As System.Windows.Forms.TextBox
    Friend WithEvents txtFday As System.Windows.Forms.TextBox
    Friend WithEvents txtfMon As System.Windows.Forms.TextBox
    Friend WithEvents txtTmon As System.Windows.Forms.TextBox
    Friend WithEvents txtTYear As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnUpdateSecurity As System.Windows.Forms.Button
    Friend WithEvents btnUpdateTransfer As System.Windows.Forms.Button
    Friend WithEvents btnUpdateMasters As System.Windows.Forms.Button
    Friend WithEvents btnGetNewStock As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
End Class
