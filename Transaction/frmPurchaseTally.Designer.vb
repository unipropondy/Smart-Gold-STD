<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPurchaseTally
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPurchaseTally))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.txtSupplierCode = New System.Windows.Forms.TextBox()
        Me.btnJobSheetNoSearch = New System.Windows.Forms.Button()
        Me.txtJobSheetNo = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtIYYYY = New System.Windows.Forms.TextBox()
        Me.txtIMM = New System.Windows.Forms.TextBox()
        Me.txtIDD = New System.Windows.Forms.TextBox()
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.txtHBillMC = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtHBillWeight = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtHBillCost = New System.Windows.Forms.TextBox()
        Me.txtHWeight = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.chkHGST = New System.Windows.Forms.CheckBox()
        Me.txtHGSTAmount = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.txtHTotalMC = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtHTotalCost = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.txtPurTotalWeight = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtPurTotalMC = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtPurTotalCost = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(2, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(270, 99)
        Me.PictureBox1.TabIndex = 476258
        Me.PictureBox1.TabStop = False
        '
        'txtSupplierCode
        '
        Me.txtSupplierCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierCode.Location = New System.Drawing.Point(107, 224)
        Me.txtSupplierCode.MaxLength = 20
        Me.txtSupplierCode.Name = "txtSupplierCode"
        Me.txtSupplierCode.Size = New System.Drawing.Size(112, 20)
        Me.txtSupplierCode.TabIndex = 476271
        Me.txtSupplierCode.Tag = "NE;"
        '
        'btnJobSheetNoSearch
        '
        Me.btnJobSheetNoSearch.BackColor = System.Drawing.Color.Orange
        Me.btnJobSheetNoSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnJobSheetNoSearch.Location = New System.Drawing.Point(193, 119)
        Me.btnJobSheetNoSearch.Name = "btnJobSheetNoSearch"
        Me.btnJobSheetNoSearch.Size = New System.Drawing.Size(28, 20)
        Me.btnJobSheetNoSearch.TabIndex = 476270
        Me.btnJobSheetNoSearch.Text = "..."
        Me.btnJobSheetNoSearch.UseVisualStyleBackColor = False
        '
        'txtJobSheetNo
        '
        Me.txtJobSheetNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJobSheetNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJobSheetNo.Location = New System.Drawing.Point(109, 119)
        Me.txtJobSheetNo.MaxLength = 20
        Me.txtJobSheetNo.Name = "txtJobSheetNo"
        Me.txtJobSheetNo.Size = New System.Drawing.Size(112, 20)
        Me.txtJobSheetNo.TabIndex = 476269
        Me.txtJobSheetNo.Tag = "NE;"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(21, 224)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(88, 15)
        Me.Label5.TabIndex = 476268
        Me.Label5.Text = "Supplier Code"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(171, 188)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(8, 16)
        Me.Label3.TabIndex = 476267
        Me.Label3.Text = "-"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(139, 188)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(8, 16)
        Me.Label4.TabIndex = 476266
        Me.Label4.Text = "-"
        '
        'txtIYYYY
        '
        Me.txtIYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIYYYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIYYYY.Location = New System.Drawing.Point(187, 188)
        Me.txtIYYYY.MaxLength = 4
        Me.txtIYYYY.Name = "txtIYYYY"
        Me.txtIYYYY.Size = New System.Drawing.Size(32, 20)
        Me.txtIYYYY.TabIndex = 476263
        Me.txtIYYYY.Tag = "ENN;"
        '
        'txtIMM
        '
        Me.txtIMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIMM.Location = New System.Drawing.Point(147, 188)
        Me.txtIMM.MaxLength = 2
        Me.txtIMM.Name = "txtIMM"
        Me.txtIMM.Size = New System.Drawing.Size(20, 20)
        Me.txtIMM.TabIndex = 476262
        Me.txtIMM.Tag = "ENN;"
        '
        'txtIDD
        '
        Me.txtIDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIDD.Location = New System.Drawing.Point(115, 188)
        Me.txtIDD.MaxLength = 2
        Me.txtIDD.Name = "txtIDD"
        Me.txtIDD.Size = New System.Drawing.Size(20, 20)
        Me.txtIDD.TabIndex = 476261
        Me.txtIDD.Tag = "ENN;"
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvoiceNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceNo.ForeColor = System.Drawing.Color.Black
        Me.txtInvoiceNo.Location = New System.Drawing.Point(107, 156)
        Me.txtInvoiceNo.MaxLength = 20
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(114, 20)
        Me.txtInvoiceNo.TabIndex = 476260
        Me.txtInvoiceNo.Tag = ""
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(21, 188)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 23)
        Me.Label7.TabIndex = 476265
        Me.Label7.Text = "Invoice Date"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(21, 156)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 15)
        Me.Label2.TabIndex = 476264
        Me.Label2.Text = "Invoice No"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(21, 124)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 15)
        Me.Label1.TabIndex = 476259
        Me.Label1.Text = "JobSheet No."
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.White
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.txtPurTotalWeight)
        Me.Panel5.Controls.Add(Me.Label9)
        Me.Panel5.Controls.Add(Me.txtPurTotalMC)
        Me.Panel5.Controls.Add(Me.Label10)
        Me.Panel5.Controls.Add(Me.txtPurTotalCost)
        Me.Panel5.Controls.Add(Me.Label11)
        Me.Panel5.Controls.Add(Me.txtHBillMC)
        Me.Panel5.Controls.Add(Me.Label8)
        Me.Panel5.Controls.Add(Me.txtHGSTAmount)
        Me.Panel5.Controls.Add(Me.txtHBillWeight)
        Me.Panel5.Controls.Add(Me.Label6)
        Me.Panel5.Controls.Add(Me.txtHBillCost)
        Me.Panel5.Controls.Add(Me.txtHWeight)
        Me.Panel5.Controls.Add(Me.Label32)
        Me.Panel5.Controls.Add(Me.chkHGST)
        Me.Panel5.Controls.Add(Me.Label31)
        Me.Panel5.Controls.Add(Me.txtHTotalMC)
        Me.Panel5.Controls.Add(Me.Label19)
        Me.Panel5.Controls.Add(Me.txtHTotalCost)
        Me.Panel5.Controls.Add(Me.Label17)
        Me.Panel5.ForeColor = System.Drawing.Color.Black
        Me.Panel5.Location = New System.Drawing.Point(9, 250)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(256, 230)
        Me.Panel5.TabIndex = 476272
        '
        'txtHBillMC
        '
        Me.txtHBillMC.AcceptsReturn = True
        Me.txtHBillMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHBillMC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHBillMC.ForeColor = System.Drawing.Color.Black
        Me.txtHBillMC.Location = New System.Drawing.Point(87, 26)
        Me.txtHBillMC.MaxLength = 20
        Me.txtHBillMC.Name = "txtHBillMC"
        Me.txtHBillMC.Size = New System.Drawing.Size(147, 20)
        Me.txtHBillMC.TabIndex = 54
        Me.txtHBillMC.Tag = ""
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(7, 26)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(71, 15)
        Me.Label8.TabIndex = 57
        Me.Label8.Text = "Bill MC"
        '
        'txtHBillWeight
        '
        Me.txtHBillWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHBillWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHBillWeight.ForeColor = System.Drawing.Color.Black
        Me.txtHBillWeight.Location = New System.Drawing.Point(86, 51)
        Me.txtHBillWeight.MaxLength = 20
        Me.txtHBillWeight.Name = "txtHBillWeight"
        Me.txtHBillWeight.Size = New System.Drawing.Size(147, 20)
        Me.txtHBillWeight.TabIndex = 56
        Me.txtHBillWeight.Tag = ""
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(6, 51)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(64, 16)
        Me.Label6.TabIndex = 55
        Me.Label6.Text = "Bill Weight"
        '
        'txtHBillCost
        '
        Me.txtHBillCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHBillCost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHBillCost.ForeColor = System.Drawing.Color.Black
        Me.txtHBillCost.Location = New System.Drawing.Point(86, 3)
        Me.txtHBillCost.MaxLength = 20
        Me.txtHBillCost.Name = "txtHBillCost"
        Me.txtHBillCost.Size = New System.Drawing.Size(147, 20)
        Me.txtHBillCost.TabIndex = 49
        Me.txtHBillCost.Tag = ""
        '
        'txtHWeight
        '
        Me.txtHWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHWeight.ForeColor = System.Drawing.Color.Black
        Me.txtHWeight.Location = New System.Drawing.Point(86, 127)
        Me.txtHWeight.MaxLength = 20
        Me.txtHWeight.Name = "txtHWeight"
        Me.txtHWeight.Size = New System.Drawing.Size(147, 20)
        Me.txtHWeight.TabIndex = 48
        Me.txtHWeight.Tag = ""
        '
        'Label32
        '
        Me.Label32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.Color.Black
        Me.Label32.Location = New System.Drawing.Point(6, 3)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(64, 15)
        Me.Label32.TabIndex = 53
        Me.Label32.Text = "Bill Cost"
        '
        'chkHGST
        '
        Me.chkHGST.ForeColor = System.Drawing.Color.Black
        Me.chkHGST.Location = New System.Drawing.Point(203, 253)
        Me.chkHGST.Name = "chkHGST"
        Me.chkHGST.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkHGST.Size = New System.Drawing.Size(48, 16)
        Me.chkHGST.TabIndex = 46
        Me.chkHGST.Text = "GST"
        Me.chkHGST.Visible = False
        '
        'txtHGSTAmount
        '
        Me.txtHGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHGSTAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHGSTAmount.ForeColor = System.Drawing.Color.Black
        Me.txtHGSTAmount.Location = New System.Drawing.Point(86, 245)
        Me.txtHGSTAmount.MaxLength = 20
        Me.txtHGSTAmount.Name = "txtHGSTAmount"
        Me.txtHGSTAmount.Size = New System.Drawing.Size(147, 20)
        Me.txtHGSTAmount.TabIndex = 47
        Me.txtHGSTAmount.Tag = ""
        Me.txtHGSTAmount.Visible = False
        '
        'Label31
        '
        Me.Label31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.Color.Black
        Me.Label31.Location = New System.Drawing.Point(6, 127)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(64, 15)
        Me.Label31.TabIndex = 51
        Me.Label31.Text = "Weight"
        '
        'txtHTotalMC
        '
        Me.txtHTotalMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHTotalMC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHTotalMC.ForeColor = System.Drawing.Color.Black
        Me.txtHTotalMC.Location = New System.Drawing.Point(86, 101)
        Me.txtHTotalMC.MaxLength = 20
        Me.txtHTotalMC.Name = "txtHTotalMC"
        Me.txtHTotalMC.Size = New System.Drawing.Size(147, 20)
        Me.txtHTotalMC.TabIndex = 45
        Me.txtHTotalMC.Tag = ""
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(6, 101)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(64, 15)
        Me.Label19.TabIndex = 49
        Me.Label19.Text = "Total MC"
        '
        'txtHTotalCost
        '
        Me.txtHTotalCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHTotalCost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHTotalCost.ForeColor = System.Drawing.Color.Black
        Me.txtHTotalCost.Location = New System.Drawing.Point(86, 77)
        Me.txtHTotalCost.MaxLength = 20
        Me.txtHTotalCost.Name = "txtHTotalCost"
        Me.txtHTotalCost.Size = New System.Drawing.Size(147, 20)
        Me.txtHTotalCost.TabIndex = 44
        Me.txtHTotalCost.Tag = ""
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(6, 77)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(64, 15)
        Me.Label17.TabIndex = 47
        Me.Label17.Text = "Total Cost"
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClear.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.Location = New System.Drawing.Point(9, 486)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(81, 40)
        Me.btnClear.TabIndex = 476274
        Me.btnClear.Text = "Clear(F6)"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(178, 486)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(87, 40)
        Me.btnClose.TabIndex = 476275
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(96, 486)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(79, 40)
        Me.btnSave.TabIndex = 476273
        Me.btnSave.Text = "Save(F7)"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'txtPurTotalWeight
        '
        Me.txtPurTotalWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurTotalWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurTotalWeight.ForeColor = System.Drawing.Color.Black
        Me.txtPurTotalWeight.Location = New System.Drawing.Point(86, 203)
        Me.txtPurTotalWeight.MaxLength = 20
        Me.txtPurTotalWeight.Name = "txtPurTotalWeight"
        Me.txtPurTotalWeight.Size = New System.Drawing.Size(147, 20)
        Me.txtPurTotalWeight.TabIndex = 61
        Me.txtPurTotalWeight.Tag = ""
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(6, 203)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(74, 15)
        Me.Label9.TabIndex = 63
        Me.Label9.Text = "Pur. Weight"
        '
        'txtPurTotalMC
        '
        Me.txtPurTotalMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurTotalMC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurTotalMC.ForeColor = System.Drawing.Color.Black
        Me.txtPurTotalMC.Location = New System.Drawing.Point(86, 177)
        Me.txtPurTotalMC.MaxLength = 20
        Me.txtPurTotalMC.Name = "txtPurTotalMC"
        Me.txtPurTotalMC.Size = New System.Drawing.Size(147, 20)
        Me.txtPurTotalMC.TabIndex = 59
        Me.txtPurTotalMC.Tag = ""
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(6, 177)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(64, 15)
        Me.Label10.TabIndex = 62
        Me.Label10.Text = "Pur. MC"
        '
        'txtPurTotalCost
        '
        Me.txtPurTotalCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurTotalCost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurTotalCost.ForeColor = System.Drawing.Color.Black
        Me.txtPurTotalCost.Location = New System.Drawing.Point(86, 153)
        Me.txtPurTotalCost.MaxLength = 20
        Me.txtPurTotalCost.Name = "txtPurTotalCost"
        Me.txtPurTotalCost.Size = New System.Drawing.Size(147, 20)
        Me.txtPurTotalCost.TabIndex = 58
        Me.txtPurTotalCost.Tag = ""
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(6, 153)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(64, 15)
        Me.Label11.TabIndex = 60
        Me.Label11.Text = "Pur. Cost"
        '
        'frmPurchaseTally
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(277, 544)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.txtSupplierCode)
        Me.Controls.Add(Me.btnJobSheetNoSearch)
        Me.Controls.Add(Me.txtJobSheetNo)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtIYYYY)
        Me.Controls.Add(Me.txtIMM)
        Me.Controls.Add(Me.txtIDD)
        Me.Controls.Add(Me.txtInvoiceNo)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPurchaseTally"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PurchaseTally"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents txtSupplierCode As System.Windows.Forms.TextBox
    Friend WithEvents btnJobSheetNoSearch As System.Windows.Forms.Button
    Friend WithEvents txtJobSheetNo As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtIYYYY As System.Windows.Forms.TextBox
    Friend WithEvents txtIMM As System.Windows.Forms.TextBox
    Friend WithEvents txtIDD As System.Windows.Forms.TextBox
    Friend WithEvents txtInvoiceNo As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents txtHBillMC As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtHBillWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtHBillCost As System.Windows.Forms.TextBox
    Friend WithEvents txtHWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents chkHGST As System.Windows.Forms.CheckBox
    Friend WithEvents txtHGSTAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txtHTotalMC As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtHTotalCost As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents txtPurTotalWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtPurTotalMC As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtPurTotalCost As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
End Class
