Public Class frmClass
    Inherits System.Windows.Forms.Form

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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkGoldLoss As System.Windows.Forms.CheckBox
    Friend WithEvents chkDisplayCost As System.Windows.Forms.CheckBox
    Friend WithEvents chkGoldBarCode As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtClassName As System.Windows.Forms.TextBox
    Friend WithEvents txtClassCode As System.Windows.Forms.TextBox
    Friend WithEvents optSP As System.Windows.Forms.RadioButton
    Friend WithEvents optSW As System.Windows.Forms.RadioButton
    Friend WithEvents optPP As System.Windows.Forms.RadioButton
    Friend WithEvents optPW As System.Windows.Forms.RadioButton
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents txtRate As System.Windows.Forms.TextBox
    Friend WithEvents ErrP As System.Windows.Forms.ErrorProvider
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtMCDiscount As System.Windows.Forms.TextBox
    Friend WithEvents optSBoth As System.Windows.Forms.RadioButton
    Friend WithEvents optPBoth As System.Windows.Forms.RadioButton
    Friend WithEvents pnlPurchaseType As System.Windows.Forms.Panel
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents grdClass As System.Windows.Forms.DataGridView
    Friend WithEvents cClassCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cClassName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSalesType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cPurchaseType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cGoldLoss As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cDisplayCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cGoldBarcode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cRate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cRemarks As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cMCDiscount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pnlSalesType As System.Windows.Forms.Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmClass))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtMCDiscount = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.pnlPurchaseType = New System.Windows.Forms.Panel()
        Me.optPBoth = New System.Windows.Forms.RadioButton()
        Me.optPP = New System.Windows.Forms.RadioButton()
        Me.optPW = New System.Windows.Forms.RadioButton()
        Me.pnlSalesType = New System.Windows.Forms.Panel()
        Me.optSBoth = New System.Windows.Forms.RadioButton()
        Me.optSP = New System.Windows.Forms.RadioButton()
        Me.optSW = New System.Windows.Forms.RadioButton()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtRate = New System.Windows.Forms.TextBox()
        Me.txtClassCode = New System.Windows.Forms.TextBox()
        Me.txtClassName = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.chkGoldBarCode = New System.Windows.Forms.CheckBox()
        Me.chkDisplayCost = New System.Windows.Forms.CheckBox()
        Me.chkGoldLoss = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.ErrP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.grdClass = New System.Windows.Forms.DataGridView()
        Me.cClassCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cClassName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cSalesType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cPurchaseType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cGoldLoss = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cDisplayCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cGoldBarcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cRate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cRemarks = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cMCDiscount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1.SuspendLayout()
        Me.pnlPurchaseType.SuspendLayout()
        Me.pnlSalesType.SuspendLayout()
        CType(Me.ErrP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdClass, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txtMCDiscount)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.pnlPurchaseType)
        Me.Panel1.Controls.Add(Me.pnlSalesType)
        Me.Panel1.Controls.Add(Me.txtRemarks)
        Me.Panel1.Controls.Add(Me.txtRate)
        Me.Panel1.Controls.Add(Me.txtClassCode)
        Me.Panel1.Controls.Add(Me.txtClassName)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.chkGoldBarCode)
        Me.Panel1.Controls.Add(Me.chkDisplayCost)
        Me.Panel1.Controls.Add(Me.chkGoldLoss)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(425, 110)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(355, 295)
        Me.Panel1.TabIndex = 24
        '
        'txtMCDiscount
        '
        Me.txtMCDiscount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMCDiscount.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMCDiscount.Location = New System.Drawing.Point(132, 253)
        Me.txtMCDiscount.MaxLength = 24
        Me.txtMCDiscount.Name = "txtMCDiscount"
        Me.txtMCDiscount.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtMCDiscount.Size = New System.Drawing.Size(102, 21)
        Me.txtMCDiscount.TabIndex = 8
        Me.txtMCDiscount.Tag = "ENN;"
        Me.txtMCDiscount.Visible = False
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(16, 252)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(96, 20)
        Me.Label7.TabIndex = 35
        Me.Label7.Text = "MCDiscount %"
        Me.Label7.Visible = False
        '
        'pnlPurchaseType
        '
        Me.pnlPurchaseType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlPurchaseType.Controls.Add(Me.optPBoth)
        Me.pnlPurchaseType.Controls.Add(Me.optPP)
        Me.pnlPurchaseType.Controls.Add(Me.optPW)
        Me.pnlPurchaseType.Location = New System.Drawing.Point(132, 97)
        Me.pnlPurchaseType.Name = "pnlPurchaseType"
        Me.pnlPurchaseType.Size = New System.Drawing.Size(208, 29)
        Me.pnlPurchaseType.TabIndex = 3
        '
        'optPBoth
        '
        Me.optPBoth.BackColor = System.Drawing.Color.White
        Me.optPBoth.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPBoth.Location = New System.Drawing.Point(144, 1)
        Me.optPBoth.Name = "optPBoth"
        Me.optPBoth.Size = New System.Drawing.Size(48, 24)
        Me.optPBoth.TabIndex = 46
        Me.optPBoth.Text = "Both"
        Me.optPBoth.UseVisualStyleBackColor = False
        '
        'optPP
        '
        Me.optPP.BackColor = System.Drawing.Color.White
        Me.optPP.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPP.Location = New System.Drawing.Point(80, 1)
        Me.optPP.Name = "optPP"
        Me.optPP.Size = New System.Drawing.Size(48, 24)
        Me.optPP.TabIndex = 45
        Me.optPP.Text = "PCS"
        Me.optPP.UseVisualStyleBackColor = False
        '
        'optPW
        '
        Me.optPW.BackColor = System.Drawing.Color.White
        Me.optPW.Checked = True
        Me.optPW.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPW.Location = New System.Drawing.Point(16, 1)
        Me.optPW.Name = "optPW"
        Me.optPW.Size = New System.Drawing.Size(48, 24)
        Me.optPW.TabIndex = 3
        Me.optPW.TabStop = True
        Me.optPW.Text = "WT"
        Me.optPW.UseVisualStyleBackColor = False
        '
        'pnlSalesType
        '
        Me.pnlSalesType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSalesType.Controls.Add(Me.optSBoth)
        Me.pnlSalesType.Controls.Add(Me.optSP)
        Me.pnlSalesType.Controls.Add(Me.optSW)
        Me.pnlSalesType.Location = New System.Drawing.Point(132, 61)
        Me.pnlSalesType.Name = "pnlSalesType"
        Me.pnlSalesType.Size = New System.Drawing.Size(208, 29)
        Me.pnlSalesType.TabIndex = 2
        '
        'optSBoth
        '
        Me.optSBoth.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSBoth.Location = New System.Drawing.Point(144, 1)
        Me.optSBoth.Name = "optSBoth"
        Me.optSBoth.Size = New System.Drawing.Size(48, 24)
        Me.optSBoth.TabIndex = 48
        Me.optSBoth.Text = "Both"
        '
        'optSP
        '
        Me.optSP.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSP.Location = New System.Drawing.Point(80, 1)
        Me.optSP.Name = "optSP"
        Me.optSP.Size = New System.Drawing.Size(48, 24)
        Me.optSP.TabIndex = 47
        Me.optSP.Text = "PCS"
        '
        'optSW
        '
        Me.optSW.Checked = True
        Me.optSW.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSW.Location = New System.Drawing.Point(16, 1)
        Me.optSW.Name = "optSW"
        Me.optSW.Size = New System.Drawing.Size(48, 24)
        Me.optSW.TabIndex = 2
        Me.optSW.TabStop = True
        Me.optSW.Text = "WT"
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(132, 253)
        Me.txtRemarks.MaxLength = 100
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(208, 21)
        Me.txtRemarks.TabIndex = 9
        '
        'txtRate
        '
        Me.txtRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRate.Location = New System.Drawing.Point(132, 226)
        Me.txtRate.MaxLength = 24
        Me.txtRate.Name = "txtRate"
        Me.txtRate.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtRate.Size = New System.Drawing.Size(102, 21)
        Me.txtRate.TabIndex = 7
        Me.txtRate.Tag = "ENN;"
        Me.txtRate.Visible = False
        '
        'txtClassCode
        '
        Me.txtClassCode.BackColor = System.Drawing.Color.White
        Me.txtClassCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtClassCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClassCode.Location = New System.Drawing.Point(132, 7)
        Me.txtClassCode.MaxLength = 20
        Me.txtClassCode.Name = "txtClassCode"
        Me.txtClassCode.Size = New System.Drawing.Size(112, 21)
        Me.txtClassCode.TabIndex = 0
        Me.txtClassCode.Tag = "NE;"
        '
        'txtClassName
        '
        Me.txtClassName.BackColor = System.Drawing.SystemColors.Window
        Me.txtClassName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtClassName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClassName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtClassName.Location = New System.Drawing.Point(132, 34)
        Me.txtClassName.MaxLength = 50
        Me.txtClassName.Name = "txtClassName"
        Me.txtClassName.Size = New System.Drawing.Size(208, 21)
        Me.txtClassName.TabIndex = 1
        Me.txtClassName.Tag = "NE;"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(16, 252)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(96, 23)
        Me.Label6.TabIndex = 33
        Me.Label6.Text = "Remarks"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(16, 225)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 20)
        Me.Label5.TabIndex = 32
        Me.Label5.Text = "Rate"
        Me.Label5.Visible = False
        '
        'chkGoldBarCode
        '
        Me.chkGoldBarCode.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkGoldBarCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkGoldBarCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGoldBarCode.Location = New System.Drawing.Point(16, 195)
        Me.chkGoldBarCode.Name = "chkGoldBarCode"
        Me.chkGoldBarCode.Size = New System.Drawing.Size(128, 24)
        Me.chkGoldBarCode.TabIndex = 6
        Me.chkGoldBarCode.Text = "Gold Bar Code"
        '
        'chkDisplayCost
        '
        Me.chkDisplayCost.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkDisplayCost.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkDisplayCost.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDisplayCost.Location = New System.Drawing.Point(16, 164)
        Me.chkDisplayCost.Name = "chkDisplayCost"
        Me.chkDisplayCost.Size = New System.Drawing.Size(128, 24)
        Me.chkDisplayCost.TabIndex = 5
        Me.chkDisplayCost.Text = "Display Cost"
        '
        'chkGoldLoss
        '
        Me.chkGoldLoss.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkGoldLoss.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkGoldLoss.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGoldLoss.Location = New System.Drawing.Point(16, 133)
        Me.chkGoldLoss.Name = "chkGoldLoss"
        Me.chkGoldLoss.Size = New System.Drawing.Size(128, 24)
        Me.chkGoldLoss.TabIndex = 4
        Me.chkGoldLoss.Text = "Gold Loss"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(15, 100)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(111, 23)
        Me.Label4.TabIndex = 28
        Me.Label4.Text = "Purchase Type"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 23)
        Me.Label3.TabIndex = 27
        Me.Label3.Text = "Sales Type"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 23)
        Me.Label2.TabIndex = 26
        Me.Label2.Text = "Class Name"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 23)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "Class Code"
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnNew.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Location = New System.Drawing.Point(365, 427)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(100, 40)
        Me.btnNew.TabIndex = 11
        Me.btnNew.Text = "New(F6)"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDelete.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(576, 427)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(100, 40)
        Me.btnDelete.TabIndex = 12
        Me.btnDelete.Text = "Delete(F8)"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(471, 427)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 40)
        Me.btnSave.TabIndex = 10
        Me.btnSave.Text = "Save(F7)"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(682, 427)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 40)
        Me.btnClose.TabIndex = 13
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'ErrP
        '
        Me.ErrP.ContainerControl = Me
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(5, 3)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(290, 100)
        Me.pic.TabIndex = 83
        Me.pic.TabStop = False
        '
        'grdClass
        '
        Me.grdClass.AllowUserToAddRows = False
        Me.grdClass.AllowUserToDeleteRows = False
        Me.grdClass.AllowUserToResizeColumns = False
        Me.grdClass.AllowUserToResizeRows = False
        Me.grdClass.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdClass.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdClass.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdClass.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cClassCode, Me.cClassName, Me.cSalesType, Me.cPurchaseType, Me.cGoldLoss, Me.cDisplayCost, Me.cGoldBarcode, Me.cRate, Me.cRemarks, Me.cMCDiscount})
        Me.grdClass.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdClass.Location = New System.Drawing.Point(5, 109)
        Me.grdClass.Name = "grdClass"
        Me.grdClass.RowHeadersVisible = False
        Me.grdClass.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdClass.Size = New System.Drawing.Size(414, 296)
        Me.grdClass.TabIndex = 84
        '
        'cClassCode
        '
        Me.cClassCode.HeaderText = "Class Code"
        Me.cClassCode.Name = "cClassCode"
        '
        'cClassName
        '
        Me.cClassName.HeaderText = "Class Name"
        Me.cClassName.Name = "cClassName"
        Me.cClassName.Width = 310
        '
        'cSalesType
        '
        Me.cSalesType.HeaderText = "Sales Type"
        Me.cSalesType.Name = "cSalesType"
        Me.cSalesType.Visible = False
        '
        'cPurchaseType
        '
        Me.cPurchaseType.HeaderText = "Purchase Type"
        Me.cPurchaseType.Name = "cPurchaseType"
        Me.cPurchaseType.Visible = False
        '
        'cGoldLoss
        '
        Me.cGoldLoss.HeaderText = "Gold Loss"
        Me.cGoldLoss.Name = "cGoldLoss"
        Me.cGoldLoss.Visible = False
        '
        'cDisplayCost
        '
        Me.cDisplayCost.HeaderText = "Display Cost"
        Me.cDisplayCost.Name = "cDisplayCost"
        Me.cDisplayCost.Visible = False
        '
        'cGoldBarcode
        '
        Me.cGoldBarcode.HeaderText = "Gold Barcode"
        Me.cGoldBarcode.Name = "cGoldBarcode"
        Me.cGoldBarcode.Visible = False
        '
        'cRate
        '
        Me.cRate.HeaderText = "Rate"
        Me.cRate.Name = "cRate"
        Me.cRate.Visible = False
        Me.cRate.Width = 70
        '
        'cRemarks
        '
        Me.cRemarks.HeaderText = "Remarks"
        Me.cRemarks.Name = "cRemarks"
        Me.cRemarks.Visible = False
        '
        'cMCDiscount
        '
        Me.cMCDiscount.HeaderText = "MC Discount"
        Me.cMCDiscount.Name = "cMCDiscount"
        Me.cMCDiscount.Visible = False
        '
        'frmClass
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(800, 505)
        Me.Controls.Add(Me.grdClass)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmClass"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Class"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlPurchaseType.ResumeLayout(False)
        Me.pnlSalesType.ResumeLayout(False)
        CType(Me.ErrP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdClass, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "obrevations"
    ' string variables starts with "s"
    ' dobule and money variables starts with "d"
    ' counter variables ends with "Cnt"
    ' dataset variables starts with "ds"
#End Region

    Dim objCls As New ClsClass
    Const ClassCode = 1
    Const ClassName = 2
    Const SalesType = 3
    Const PurchaseType = 4
    Const GoldLoss = 5
    Const DisplayCost = 6
    Const GoldBarCode = 7
    Const Rate = 8
    Const Remarks = 9
    Const MCDiscount = 10

    Private Sub frmClass_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim frm As New frmMain
        'Me.BackgroundImage = frm.ImageList1.Images(21)
        'subGridSetting(grdClass)
        GetClassDetails()
        ShowClassDetail(0)
        grdClass.Focus()
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmClass_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F6
                btnNew_Click(sender, e)
            Case Keys.F7
                btnSave_Click(sender, e)
            Case Keys.F8
                If btnDelete.Enabled Then btnDelete_Click(sender, e)
            Case Keys.Escape
                btnClose_Click(sender, e)
            Case Keys.Return
                SendKeys.Send("{Tab}")
                'Case Keys.Down
                '    SendKeys.Send("{Tab}")
                'Case Keys.Up
                'SendKeys.Send("+{Tab}")
        End Select
    End Sub
    Private Sub GetClassDetails()
        Try
            Dim dsClass As New DataSet
            Dim iCnt As Long
            dsClass = objCls.GetClassDetail("")
            If Not IsNothing(dsClass) Then
                If dsClass.Tables("Class").Rows.Count > 0 Then
                    grdClass.RowCount = 0
                    For iCnt = 0 To dsClass.Tables("CLASS").Rows.Count - 1
                        grdClass.Rows.Add()
                        fnSetdgvText(grdClass, iCnt, 0, Trim(dsClass.Tables("Class").Rows(iCnt)("ClassCode") & ""))
                        fnSetdgvText(grdClass, iCnt, 1, Trim(dsClass.Tables("Class").Rows(iCnt)("ClassName") & ""))

                        fnSetdgvText(grdClass, iCnt, 2, Trim(dsClass.Tables("Class").Rows(iCnt)("SalesType") & ""))
                        fnSetdgvText(grdClass, iCnt, 3, Trim(dsClass.Tables("Class").Rows(iCnt)("PurchaseType") & ""))

                        fnSetdgvText(grdClass, iCnt, 4, Trim(dsClass.Tables("Class").Rows(iCnt)("GoldLoss") & ""))
                        fnSetdgvText(grdClass, iCnt, 5, Trim(dsClass.Tables("Class").Rows(iCnt)("DisplayCost") & ""))


                        fnSetdgvText(grdClass, iCnt, 6, Trim(dsClass.Tables("Class").Rows(iCnt)("GoldBarCode") & ""))
                        fnSetdgvText(grdClass, iCnt, 7, Format(Val(Trim(dsClass.Tables("Class").Rows(iCnt)("Rate") & "")), "0.00"))


                        fnSetdgvText(grdClass, iCnt, 8, Trim(dsClass.Tables("Class").Rows(iCnt)("Remarks") & ""))
                        fnSetdgvText(grdClass, iCnt, 9, Format(Trim(dsClass.Tables("Class").Rows(iCnt)("MCDiscount") & ""), "0.00"))
                    Next
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sSalesType, sPurchaseType, sGoldLoss, sDisplayCost, sGoldBarCode, sGBClassCode As String
        Dim dsG As New DataSet

        Try
            If optSW.Checked Then
                sSalesType = "W"
            ElseIf optSP.Checked Then
                sSalesType = "P"
            ElseIf optSBoth.Checked Then
                sSalesType = "B"
            End If

            If optPW.Checked Then
                sPurchaseType = "W"
            ElseIf optPP.Checked Then
                sPurchaseType = "P"
            ElseIf optPBoth.Checked Then
                sPurchaseType = "B"
            End If

            If chkGoldLoss.Checked Then
                sGoldLoss = "Y"
            Else
                sGoldLoss = "N"
            End If

            If chkDisplayCost.Checked Then
                sDisplayCost = "Y"
            Else
                sDisplayCost = "N"
            End If

            If chkGoldBarCode.Checked Then
                sGoldBarCode = "Y"
            Else
                sGoldBarCode = "N"
            End If

            If CheckMe(Me) Then
                dsG = objCls.GetGoldBarCode()
                If dsG.Tables("Class").Rows.Count > 0 Then
                    sGBClassCode = Trim(dsG.Tables("Class").Rows(0)("ClassCode") & "")
                    If sGBClassCode <> txtClassCode.Text.Trim Then
                        If (MessageBox.Show("Do you want to change the GoldBar?", "GoldPOS", MessageBoxButtons.YesNo) = DialogResult.No) Then Exit Sub
                    End If
                End If
                If objCls.ClassSave(txtClassCode.Text.Trim, txtClassName.Text.Trim, sSalesType, sPurchaseType, sGoldLoss, sDisplayCost, sGoldBarCode, Val(txtRate.Text.Trim), Val(txtMCDiscount.Text.Trim), txtRemarks.Text.Trim) Then
                    MessageBox.Show("Class detail saved sucssfully.")
                    ClearMe()
                    GetClassDetails()
                    grdClass.Focus()
                    'ShowClassDetail(grdClass.)
                Else
                    MessageBox.Show("Error in Save")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If grdClass.RowCount > 0 And grdClass.CurrentRow.Index > 0 Then
                Dim sClassCode As String
                sClassCode = fnGetdgvText(grdClass, grdClass.CurrentRow.Index, cClassCode.Index)
                If fncGetClassTran(sClassCode) = True Then
                    MsgBox("Can Not Delete Class , Beacuse This Class Is Having Some Inventory", MsgBoxStyle.Information)
                    Exit Sub
                Else
                    If MessageBox.Show("Do you want to delete " & fnGetdgvText(grdClass, grdClass.CurrentRow.Index, cClassCode.Index) & " Class?", "GoldPOS", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        If objCls.DeleteClassDetail(sClassCode) Then
                            MessageBox.Show("Class detail deleted.")
                            ClearMe()
                            GetClassDetails()
                            grdClass.Focus()
                            'ShowClassDetail()
                        Else
                            MessageBox.Show("Error in deleting Class detail.")
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Function fncGetClassTran(ByVal sCode As String) As Boolean
        Dim DvCustomer() As DataRow
        Dim DM As New DataManager
        fncGetClassTran = False
        DvCustomer = DM.RowReturnProjExecNonQryWithDataRow("Select * from tblInventory where ClassCode=" & fncFormatSQLText(sCode))
        If DvCustomer.Length > 0 Then
            fncGetClassTran = True
        Else
            fncGetClassTran = False
        End If
    End Function

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        ClearMe()
    End Sub

    Private Sub ClearMe()
        txtClassCode.Clear()
        txtClassName.Clear()
        optPW.Checked = True
        optPP.Checked = False
        optSW.Checked = True
        optSP.Checked = False
        optSBoth.Checked = False
        optPBoth.Checked = False
        chkDisplayCost.Checked = False
        chkGoldBarCode.Checked = False
        chkGoldLoss.Checked = False
        txtRate.Clear()
        txtMCDiscount.Clear()
        txtRemarks.Clear()
        btnDelete.Enabled = False
        txtClassCode.ReadOnly = False
        pnlPurchaseType.Enabled = True
        pnlSalesType.Enabled = True
        txtClassCode.Focus()
    End Sub

    Private Sub grdClass_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdClass.CellClick
        ShowClassDetail(grdClass.CurrentRow.Index)
    End Sub

    Private Sub grdClass_CurrentCellChanged(sender As Object, e As System.EventArgs) Handles grdClass.CurrentCellChanged
        'ShowClassDetail(grdClass.CurrentRow.Index)
    End Sub

    Private Sub ShowClassDetail(ByVal Row As Integer)
        Try
            btnDelete.Enabled = True
            If Row >= 0 Then
                txtClassCode.Text = fnGetdgvText(grdClass, Row, 0)
                txtClassCode.ReadOnly = True
                txtClassName.Text = fnGetdgvText(grdClass, Row, 1)
                If fnGetdgvText(grdClass, Row, 2) = "W" Then
                    optPW.Checked = True
                ElseIf fnGetdgvText(grdClass, Row, 2) = "P" Then
                    optPP.Checked = True
                ElseIf fnGetdgvText(grdClass, Row, 2) = "B" Then
                    optPBoth.Checked = True
                End If
                If fnGetdgvText(grdClass, Row, 3) = "W" Then
                    optSW.Checked = True
                ElseIf fnGetdgvText(grdClass, Row, 3) = "P" Then
                    optSP.Checked = True
                ElseIf fnGetdgvText(grdClass, Row, 3) = "B" Then
                    optSBoth.Checked = True
                End If
                pnlPurchaseType.Enabled = False
                pnlSalesType.Enabled = False
                If fnGetdgvText(grdClass, Row, 4) = "Y" Then
                    chkGoldLoss.Checked = True
                Else
                    chkGoldLoss.Checked = False
                End If
                If fnGetdgvText(grdClass, Row, 5) = "Y" Then
                    chkDisplayCost.Checked = True
                Else
                    chkDisplayCost.Checked = False
                End If
                If fnGetdgvText(grdClass, Row, 6) = "Y" Then
                    chkGoldBarCode.Checked = True
                Else
                    chkGoldBarCode.Checked = False
                End If
                txtRate.Text = fnGetdgvText(grdClass, Row, 7)
                txtRemarks.Text = fnGetdgvText(grdClass, Row, 8)
                txtMCDiscount.Text = fnGetdgvText(grdClass, Row, 9)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub grdClass_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_ClickEvent)
        ShowClassDetail(e.row)
    End Sub

    Private Sub grdClass_LeaveRow(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_LeaveRowEvent)
        ShowClassDetail(e.newRow)
    End Sub

    Private Sub txtClassCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtClassCode.LostFocus
        Dim iCnt As Integer
        'If txtClassCode.Text.Trim <> "" Then
        '    For iCnt = 1 To grdClass.MaxRows
        '        If UCase(txtClassCode.Text.Trim) = UCase(Trim(GetText(grdClass, iCnt, ClassCode))) Then
        '            ShowClassDetail(iCnt)
        '            Exit For
        '        End If
        '    Next
        'End If
    End Sub

    Private Sub frmClass_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Disposed
        objCls = Nothing
    End Sub

    Private Sub txtClassCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtClassCode.Validating
        If txtClassCode.Text.Trim = "" Then
            ErrP.SetError(txtClassCode, "Should not be empty.")
            txtClassCode.Focus()
        Else
            ErrP.Dispose()
        End If
    End Sub

    Private Sub frmClass_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        objCls = Nothing
    End Sub

    Private Sub txtClassName_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtClassName.Validating
        If txtClassName.Text.Trim = "" Then
            ErrP.SetError(txtClassName, "Should not be empty.")
            txtClassName.Focus()
        Else
            ErrP.Dispose()
        End If
    End Sub

    Private Sub txtRate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtRate.Validating
        If txtRate.Text.Trim = "" Then
            ErrP.SetError(txtRate, "Should not be empty.")
            txtRate.Focus()
        ElseIf Not IsNumeric(txtRate.Text.Trim) Then
            ErrP.SetError(txtRate, "Should be a Numeric")
            txtRate.Focus()
        Else
            ErrP.Dispose()
        End If
    End Sub


    Private Sub grdClass_Advance(ByVal sender As System.Object, ByVal e As AxFPSpread._DSpreadEvents_AdvanceEvent)

    End Sub

    Private Sub grdClass_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdClass.CellContentClick

    End Sub

    Private Sub grdClass_Click(sender As Object, e As System.EventArgs) Handles grdClass.Click
        GetDetailsFromGrid()



    End Sub
    Public Function GetDetailsFromGrid()
        Dim sClassCode, sClassName, sSalesType, sPurchaseType, sGoldLoss, sDisplayCost, sGoldBarcode, sRemarks As String
        Dim dRate, dMCDiscount As Double
        btnDelete.Enabled = True
        sClassCode = fnGetdgvText(grdClass, grdClass.CurrentRow.Index, cClassCode.Index)
        sClassName = fnGetdgvText(grdClass, grdClass.CurrentRow.Index, cClassName.Index)

        sSalesType = fnGetdgvText(grdClass, grdClass.CurrentRow.Index, cSalesType.Index)
        sPurchaseType = fnGetdgvText(grdClass, grdClass.CurrentRow.Index, cPurchaseType.Index)

        sGoldLoss = fnGetdgvText(grdClass, grdClass.CurrentRow.Index, cGoldLoss.Index)

        sDisplayCost = fnGetdgvText(grdClass, grdClass.CurrentRow.Index, cDisplayCost.Index)
        sGoldBarcode = fnGetdgvText(grdClass, grdClass.CurrentRow.Index, cGoldBarcode.Index)

        sRemarks = fnGetdgvText(grdClass, grdClass.CurrentRow.Index, cRemarks.Index)
        txtRate.Text = Format(fnGetdgvText(grdClass, grdClass.CurrentRow.Index, cClassCode.Index), "0.00")

        txtMCDiscount.Text = Format(fnGetdgvText(grdClass, grdClass.CurrentRow.Index, cClassCode.Index), "0.00")

        txtClassCode.Text = sClassCode
        txtClassName.Text = sClassName

        If sSalesType = "W" Then
            optSW.Checked = True
        ElseIf sSalesType = "P" Then
            optSP.Checked = True
        Else
            optSBoth.Checked = True
        End If

        If sPurchaseType = "W" Then
            optPW.Checked = True
        ElseIf sPurchaseType = "P" Then
            optPP.Checked = True
        Else
            optPBoth.Checked = True
        End If

        If sGoldLoss = "Y" Then
            chkGoldLoss.Checked = True
        Else
            chkGoldLoss.Checked = False
        End If

        If sDisplayCost = "Y" Then
            chkDisplayCost.Checked = True
        Else
            chkDisplayCost.Checked = False
        End If

        If sGoldBarcode = "Y" Then
            chkGoldBarCode.Checked = True
        Else
            chkGoldBarCode.Checked = False
        End If


    End Function
End Class
