Public Class frmGoldBarFix
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtIYYYY As System.Windows.Forms.TextBox
    Friend WithEvents txtIMM As System.Windows.Forms.TextBox
    Friend WithEvents txtIDD As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtInvoiceNo As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents txtHGoldLoss As System.Windows.Forms.TextBox
    Friend WithEvents txtHWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents chkHGST As System.Windows.Forms.CheckBox
    Friend WithEvents txtHGSTAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txtHTotalMC As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtHTotalCost As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtGoldBarFix As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnBillClear As System.Windows.Forms.Button
    Friend WithEvents txt24KWeight As System.Windows.Forms.TextBox
    Friend WithEvents btnJobSheetNoSearch As System.Windows.Forms.Button
    Friend WithEvents txtJobSheetNo As System.Windows.Forms.TextBox
    Friend WithEvents txtSupplierCode As System.Windows.Forms.TextBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents btnTaelPercent As System.Windows.Forms.Button
    Friend WithEvents cboTealPercent As System.Windows.Forms.TextBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGoldBarFix))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtIYYYY = New System.Windows.Forms.TextBox()
        Me.txtIMM = New System.Windows.Forms.TextBox()
        Me.txtIDD = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.txt24KWeight = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtGoldBarFix = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtHGoldLoss = New System.Windows.Forms.TextBox()
        Me.txtHWeight = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.chkHGST = New System.Windows.Forms.CheckBox()
        Me.txtHGSTAmount = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.txtHTotalMC = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtHTotalCost = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnBillClear = New System.Windows.Forms.Button()
        Me.btnJobSheetNoSearch = New System.Windows.Forms.Button()
        Me.txtJobSheetNo = New System.Windows.Forms.TextBox()
        Me.txtSupplierCode = New System.Windows.Forms.TextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnTaelPercent = New System.Windows.Forms.Button()
        Me.cboTealPercent = New System.Windows.Forms.TextBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Panel5.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(8, 112)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 15)
        Me.Label1.TabIndex = 44
        Me.Label1.Text = "JobSheet No."
        '
        'txtIYYYY
        '
        Me.txtIYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIYYYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIYYYY.Location = New System.Drawing.Point(174, 176)
        Me.txtIYYYY.MaxLength = 4
        Me.txtIYYYY.Name = "txtIYYYY"
        Me.txtIYYYY.Size = New System.Drawing.Size(32, 20)
        Me.txtIYYYY.TabIndex = 117
        Me.txtIYYYY.Tag = "ENN;"
        '
        'txtIMM
        '
        Me.txtIMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIMM.Location = New System.Drawing.Point(134, 176)
        Me.txtIMM.MaxLength = 2
        Me.txtIMM.Name = "txtIMM"
        Me.txtIMM.Size = New System.Drawing.Size(20, 20)
        Me.txtIMM.TabIndex = 116
        Me.txtIMM.Tag = "ENN;"
        '
        'txtIDD
        '
        Me.txtIDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIDD.Location = New System.Drawing.Point(102, 176)
        Me.txtIDD.MaxLength = 2
        Me.txtIDD.Name = "txtIDD"
        Me.txtIDD.Size = New System.Drawing.Size(20, 20)
        Me.txtIDD.TabIndex = 115
        Me.txtIDD.Tag = "ENN;"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(8, 176)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 23)
        Me.Label7.TabIndex = 119
        Me.Label7.Text = "Invoice Date"
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvoiceNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceNo.ForeColor = System.Drawing.Color.Black
        Me.txtInvoiceNo.Location = New System.Drawing.Point(94, 144)
        Me.txtInvoiceNo.MaxLength = 20
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(114, 20)
        Me.txtInvoiceNo.TabIndex = 114
        Me.txtInvoiceNo.Tag = ""
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(8, 144)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 15)
        Me.Label2.TabIndex = 118
        Me.Label2.Text = "Invoice No"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(158, 176)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(8, 16)
        Me.Label3.TabIndex = 121
        Me.Label3.Text = "-"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(126, 176)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(8, 16)
        Me.Label4.TabIndex = 120
        Me.Label4.Text = "-"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(8, 212)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(88, 15)
        Me.Label5.TabIndex = 123
        Me.Label5.Text = "Supplier Code"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.White
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.txt24KWeight)
        Me.Panel5.Controls.Add(Me.Label8)
        Me.Panel5.Controls.Add(Me.txtGoldBarFix)
        Me.Panel5.Controls.Add(Me.Label6)
        Me.Panel5.Controls.Add(Me.txtHGoldLoss)
        Me.Panel5.Controls.Add(Me.txtHWeight)
        Me.Panel5.Controls.Add(Me.Label32)
        Me.Panel5.Controls.Add(Me.chkHGST)
        Me.Panel5.Controls.Add(Me.txtHGSTAmount)
        Me.Panel5.Controls.Add(Me.Label31)
        Me.Panel5.Controls.Add(Me.txtHTotalMC)
        Me.Panel5.Controls.Add(Me.Label19)
        Me.Panel5.Controls.Add(Me.txtHTotalCost)
        Me.Panel5.Controls.Add(Me.Label17)
        Me.Panel5.ForeColor = System.Drawing.Color.Black
        Me.Panel5.Location = New System.Drawing.Point(9, 270)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(256, 208)
        Me.Panel5.TabIndex = 124
        '
        'txt24KWeight
        '
        Me.txt24KWeight.AcceptsReturn = True
        Me.txt24KWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt24KWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt24KWeight.ForeColor = System.Drawing.Color.Black
        Me.txt24KWeight.Location = New System.Drawing.Point(89, 143)
        Me.txt24KWeight.MaxLength = 20
        Me.txt24KWeight.Name = "txt24KWeight"
        Me.txt24KWeight.Size = New System.Drawing.Size(147, 20)
        Me.txt24KWeight.TabIndex = 56
        Me.txt24KWeight.Tag = ""
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(9, 143)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(71, 15)
        Me.Label8.TabIndex = 57
        Me.Label8.Text = "24K Weight"
        '
        'txtGoldBarFix
        '
        Me.txtGoldBarFix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldBarFix.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoldBarFix.ForeColor = System.Drawing.Color.Black
        Me.txtGoldBarFix.Location = New System.Drawing.Point(88, 168)
        Me.txtGoldBarFix.MaxLength = 20
        Me.txtGoldBarFix.Name = "txtGoldBarFix"
        Me.txtGoldBarFix.Size = New System.Drawing.Size(147, 20)
        Me.txtGoldBarFix.TabIndex = 54
        Me.txtGoldBarFix.Tag = ""
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(8, 168)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(64, 16)
        Me.Label6.TabIndex = 55
        Me.Label6.Text = "Fix"
        '
        'txtHGoldLoss
        '
        Me.txtHGoldLoss.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHGoldLoss.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHGoldLoss.ForeColor = System.Drawing.Color.Black
        Me.txtHGoldLoss.Location = New System.Drawing.Point(88, 120)
        Me.txtHGoldLoss.MaxLength = 20
        Me.txtHGoldLoss.Name = "txtHGoldLoss"
        Me.txtHGoldLoss.Size = New System.Drawing.Size(147, 20)
        Me.txtHGoldLoss.TabIndex = 49
        Me.txtHGoldLoss.Tag = ""
        '
        'txtHWeight
        '
        Me.txtHWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHWeight.ForeColor = System.Drawing.Color.Black
        Me.txtHWeight.Location = New System.Drawing.Point(88, 96)
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
        Me.Label32.Location = New System.Drawing.Point(8, 120)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(64, 15)
        Me.Label32.TabIndex = 53
        Me.Label32.Text = "Gold Loss"
        '
        'chkHGST
        '
        Me.chkHGST.ForeColor = System.Drawing.Color.Black
        Me.chkHGST.Location = New System.Drawing.Point(8, 72)
        Me.chkHGST.Name = "chkHGST"
        Me.chkHGST.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkHGST.Size = New System.Drawing.Size(48, 16)
        Me.chkHGST.TabIndex = 46
        Me.chkHGST.Text = "GST"
        '
        'txtHGSTAmount
        '
        Me.txtHGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHGSTAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHGSTAmount.ForeColor = System.Drawing.Color.Black
        Me.txtHGSTAmount.Location = New System.Drawing.Point(88, 72)
        Me.txtHGSTAmount.MaxLength = 20
        Me.txtHGSTAmount.Name = "txtHGSTAmount"
        Me.txtHGSTAmount.Size = New System.Drawing.Size(147, 20)
        Me.txtHGSTAmount.TabIndex = 47
        Me.txtHGSTAmount.Tag = ""
        '
        'Label31
        '
        Me.Label31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.Color.Black
        Me.Label31.Location = New System.Drawing.Point(8, 96)
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
        Me.txtHTotalMC.Location = New System.Drawing.Point(88, 40)
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
        Me.Label19.Location = New System.Drawing.Point(8, 40)
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
        Me.txtHTotalCost.Location = New System.Drawing.Point(88, 16)
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
        Me.Label17.Location = New System.Drawing.Point(8, 16)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(64, 15)
        Me.Label17.TabIndex = 47
        Me.Label17.Text = "Total Cost"
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.Orange
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.Location = New System.Drawing.Point(0, 484)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(93, 23)
        Me.btnSave.TabIndex = 127
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.Orange
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(192, 484)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(92, 23)
        Me.btnClose.TabIndex = 126
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnBillClear
        '
        Me.btnBillClear.BackColor = System.Drawing.Color.Orange
        Me.btnBillClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBillClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBillClear.Image = CType(resources.GetObject("btnBillClear.Image"), System.Drawing.Image)
        Me.btnBillClear.Location = New System.Drawing.Point(96, 484)
        Me.btnBillClear.Name = "btnBillClear"
        Me.btnBillClear.Size = New System.Drawing.Size(95, 23)
        Me.btnBillClear.TabIndex = 125
        Me.btnBillClear.Text = "Clear (F6)"
        Me.btnBillClear.UseVisualStyleBackColor = False
        '
        'btnJobSheetNoSearch
        '
        Me.btnJobSheetNoSearch.BackColor = System.Drawing.Color.Orange
        Me.btnJobSheetNoSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnJobSheetNoSearch.Location = New System.Drawing.Point(180, 107)
        Me.btnJobSheetNoSearch.Name = "btnJobSheetNoSearch"
        Me.btnJobSheetNoSearch.Size = New System.Drawing.Size(28, 20)
        Me.btnJobSheetNoSearch.TabIndex = 476255
        Me.btnJobSheetNoSearch.Text = "..."
        Me.btnJobSheetNoSearch.UseVisualStyleBackColor = False
        '
        'txtJobSheetNo
        '
        Me.txtJobSheetNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJobSheetNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJobSheetNo.Location = New System.Drawing.Point(96, 107)
        Me.txtJobSheetNo.MaxLength = 20
        Me.txtJobSheetNo.Name = "txtJobSheetNo"
        Me.txtJobSheetNo.Size = New System.Drawing.Size(112, 20)
        Me.txtJobSheetNo.TabIndex = 476254
        Me.txtJobSheetNo.Tag = "NE;"
        '
        'txtSupplierCode
        '
        Me.txtSupplierCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierCode.Location = New System.Drawing.Point(94, 212)
        Me.txtSupplierCode.MaxLength = 20
        Me.txtSupplierCode.Name = "txtSupplierCode"
        Me.txtSupplierCode.Size = New System.Drawing.Size(112, 20)
        Me.txtSupplierCode.TabIndex = 476256
        Me.txtSupplierCode.Tag = "NE;"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(0, 1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(270, 100)
        Me.PictureBox1.TabIndex = 476257
        Me.PictureBox1.TabStop = False
        '
        'btnTaelPercent
        '
        Me.btnTaelPercent.BackColor = System.Drawing.Color.Orange
        Me.btnTaelPercent.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTaelPercent.Location = New System.Drawing.Point(178, 238)
        Me.btnTaelPercent.Name = "btnTaelPercent"
        Me.btnTaelPercent.Size = New System.Drawing.Size(28, 20)
        Me.btnTaelPercent.TabIndex = 476261
        Me.btnTaelPercent.Text = "..."
        Me.btnTaelPercent.UseVisualStyleBackColor = False
        '
        'cboTealPercent
        '
        Me.cboTealPercent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboTealPercent.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTealPercent.Location = New System.Drawing.Point(94, 238)
        Me.cboTealPercent.MaxLength = 20
        Me.cboTealPercent.Name = "cboTealPercent"
        Me.cboTealPercent.Size = New System.Drawing.Size(89, 20)
        Me.cboTealPercent.TabIndex = 476260
        Me.cboTealPercent.Tag = "NE;"
        '
        'Label38
        '
        Me.Label38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.Color.Black
        Me.Label38.Location = New System.Drawing.Point(6, 238)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(90, 29)
        Me.Label38.TabIndex = 476259
        Me.Label38.Text = "TEAL Percent"
        '
        'frmGoldBarFix
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(296, 531)
        Me.Controls.Add(Me.btnTaelPercent)
        Me.Controls.Add(Me.cboTealPercent)
        Me.Controls.Add(Me.Label38)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.txtSupplierCode)
        Me.Controls.Add(Me.btnJobSheetNoSearch)
        Me.Controls.Add(Me.txtJobSheetNo)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnBillClear)
        Me.Controls.Add(Me.Panel5)
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
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmGoldBarFix"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "GoldBar Fix"
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub frmGoldBarFix_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub


    Private Sub subGetJobSheet(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Long
        Dim Ds As DataSet
        Dim Dsp As DataSet
        Dim objJobSheet As New clsJobSheet
        Try
            Ds = objJobSheet.GetJobSheet(txtJobSheetNo.Text.Trim)
            If Ds.Tables("JobSheet").Rows.Count > 0 Then
                txtJobSheetNo.Text = Trim(Ds.Tables("JobSheet").Rows(0)("JobSheetNo") & "")
                '''  sJobSheetCode = txtJobSheetNo.Text.Trim
                txtInvoiceNo.Text = Trim(Ds.Tables("JobSheet").Rows(0)("PurchaseBillNo") & "")
                txtIYYYY.Text = Mid(Ds.Tables("JobSheet").Rows(0)("Pdate") & "", 1, 4)
                txtIMM.Text = Mid(Ds.Tables("JobSheet").Rows(0)("Pdate") & "", 5, 2)
                txtIDD.Text = Mid(Ds.Tables("JobSheet").Rows(0)("Pdate") & "", 7, 2)
                txtSupplierCode.Text = Trim(Ds.Tables("JobSheet").Rows(0)("SupplierCode") & "")

                cboTealPercent.Text = Trim(Ds.Tables("JobSheet").Rows(0)("TaelPercent") & "")
                '''txtSupplierCode_LostFocus(sender, e)
                '''txtCurrencyCode.Text = Trim(Ds.Tables("JobSheet").Rows(0)("CurrencyCode") & " ")
                '''txtCurrencyCode_LostFocus(sender, e)
                '''txtCurrencyRate.Text = Format(Val(Ds.Tables("JobSheet").Rows(0)("CurrencyRate") & ""), "#0.000000")

                '''txtCostPerGram.Text = Format(Val(Ds.Tables("JobSheet").Rows(0)("CostperGram") & ""), "#0.00")

                '''chkGST.Checked = IIf(UCase(Trim(Ds.Tables("JobSheet").Rows(0)("TaelGST") & "")) = "Y", 1, 0)
                '''txtCostPerGram.Text = Format(Val(Ds.Tables("JobSheet").Rows(0)("CostperGram") & ""), "#0.00")
                '''txtCostPerGram_LostFocus(sender, e)
                '''txtRemarks.Text = Trim(Ds.Tables("JobSheet").Rows(0)("REMARKS") & "")

                '''sTranType = UCase(Trim(Ds.Tables("JobSheet").Rows(0)("TranType") & ""))
                '''sBillClose = UCase(Trim$(Ds.Tables("JobSheet").Rows(0)("BillClose") & ""))

                txtHTotalCost.Text = Format(Val(Ds.Tables("JobSheet").Rows(0)("ManualLocalCOST") & ""), "#0.00")
                txtHTotalMC.Text = Format(Val(Ds.Tables("JobSheet").Rows(0)("ManualMc") & ""), "#0.00")
                txtHWeight.Text = Format(Val(Ds.Tables("JobSheet").Rows(0)("ManualWeight") & ""), "#0.00")
                txtHGoldLoss.Text = Format(Val(Ds.Tables("JobSheet").Rows(0)("ManualGoldLoss") & ""), "#0.00")
                txt24KWeight.Text = Val(txtHWeight.Text) * Val(cboTealPercent.Text)

                ''If Trim(Ds.Tables("JobSheet").Rows(0)("PaymentType") & "") = "G" Then
                ''    optGold.Checked = True
                ''Else
                ''    optCash.Checked = True
                ''End If

                chkHGST.Checked = IIf(Val(Ds.Tables("JobSheet").Rows(0)("Gst") & "") > 0, 1, 0)
                txtHGSTAmount.Text = Format(Val(Ds.Tables("JobSheet").Rows(0)("Gst") & ""), "#0.00")


                Ds = objJobSheet.GetPurchaseFix(txtInvoiceNo.Text.Trim)
                If Ds.Tables("JobSheet").Rows.Count > 0 Then
                    txtGoldBarFix.Text = Format(Val(Ds.Tables("JobSheet").Rows(0)("PurchaseFix") & ""), "#0.00")
                End If

                'lblLastUpdated.text = Format(adoRS!ModifyDate, "dd/MM/yyyy")
                'lock everything
                '''If sBillClose = "Y" Then
                '''    MsgBox("This bill is closed! Cannot modify!", MsgBoxStyle.Critical)
                '''    subLockAllFlds(True)
                '''ElseIf sTranType <> "DP" Then
                '''    MsgBox("This is not a direct purchase bill. Cannot modify!", MsgBoxStyle.Critical)
                '''    subLockAllFlds(True)
                '''Else
                '''    subLockAllFlds(False)
                '''End If
                '''subLockHeader(True)
                '''If txtCatalogueCode.Enabled Then txtCatalogueCode.Focus()
            Else
                MsgBox("Job sheet no. is invalid!", MsgBoxStyle.Critical)
                ''btnClear_Click(sender, e)
                Exit Sub
            End If
            '''subGetPurchaseDetails(sender, e)
            Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub


    Private Function fncInventory() As Boolean
        Dim sPDate As String
        Dim Ds As DataSet
        Dim objInventory As New clsInventory
        Dim objpurchase As New clsPurchase
        Dim objDirectPurchase As New clsDirectPurchase
        Dim sError As String
        Dim bInventory As Boolean
        Dim bPurchase As Boolean
        Dim bPurchaseBill As Boolean
        Dim bJobsheet As Boolean
        Dim i As Long
        Dim sError1 As String
        Dim sError2 As String
        Dim sError3 As String
        Dim sPMCType As String
        Dim sSMCType As String
        Dim sPayType As String
        Dim bNewJobSheet As Boolean
        Try

            bInventory = False




            'Weight saved is the piece weight; purchase cost is without mc; purchase mc is the total mc

            bPurchaseBill = objpurchase.PurchaseFixSave(txtSupplierCode.Text.Trim, txtInvoiceNo.Text.Trim, Trim(sPDate), _
                            Trim(sPayType), Val(txtHWeight.Text), Val(txtHTotalCost.Text) - Val(txtHTotalMC.Text), Val(txtHTotalMC.Text), _
                            "DP", Val(txtGoldBarFix.Text), Val(txt24KWeight.Text), sUserID)
            If Not bPurchaseBill Then
                MsgBox("Error in Saving Purchase Fix", MsgBoxStyle.Critical)
                fncInventory = False
                Exit Function
            End If


            'Weight saved is the piece weight; purchase cost is without mc; purchase mc is the total mc

            '''bJobsheet = objDirectPurchase.JobSheetSave(Trim(sJobSheetCode), txtInvoiceNo.Text.Trim, Trim(sPDate), _
            '''                Trim(sPayType), txtSupplierCode.Text.Trim, txtCurrencyCode.Text.Trim, Val(txtCurrencyRate.Text), txtCatalogueCode.Text.Trim, _
            '''                txtCategoryCode.Text.Trim, txtInventoryDescription.Text.Trim, txtRemarks.Text.Trim, "DP", _
            '''                IIf(chkGST.Checked, "Y", "N"))


            fncInventory = True
            'Set the jobsheetno and lock the header
            '''txtJobSheetNo.Text = sJobSheetCode
            '''subLockHeader(True)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Function
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sinventorycode As String
        Try
            gsConBatchPosting.ConnectionString = gsCon
            gsConBatchPosting.Open()
            gsTrans = gsConBatchPosting.BeginTransaction
            If fncInventory() Then
                gsTrans.Commit()
                MsgBox("Transaction Saved Successfully")
            Else
                MsgBox("Transaction not saved")
                gsTrans.Rollback()

            End If
            gsConBatchPosting.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            gsConBatchPosting.Close()
        End Try
    End Sub

    Public Sub txtJobSheetNo_LostFocus(sender As System.Object, e As System.EventArgs)
        Try
            If txtJobSheetNo.Text.Trim <> "" Then
                subGetJobSheet(sender, e)
            Else
                ''subLockAllFlds(False)
                ''subClear()
                ''subClearDetail(False)
                ''subClearCatalogue()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnJobSheetNoSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnJobSheetNoSearch.Click
        Try
            Dim e1 As New System.EventArgs
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblJobSheetHeader", "L", 4, "JobSheetNo", "SupplierCode='STM'", "JobSheetNo", "JobSheetNo", 80, "SupplierCode", "SupplierCode", 180, "PurchaseBillNo", "PurchaseBillNo", 60, "TotalWeight24k", "TotalWeight24k", 80)
            frmHelpClass.ShowDialog()
            txtJobSheetNo.Text = frmHelpClass.Var1.Trim
            SendKeys.Send(("{TAB}"))
            txtJobSheetNo_LostFocus(sender, e1)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnTaelPercent_Click(sender As System.Object, e As System.EventArgs) Handles btnTaelPercent.Click
        Try
            Dim e1 As New System.EventArgs
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblTaelPercent", "L", 4, "TaelPercent", "", "TaelPercent", "TaelPercent", "80", "TaelPercent1", "TaelPercent", "80", "TaelPercent2", "TaelPercent", "80", "TaelPercent3", "TaelPercent", "80")
            frmHelpClass.ShowDialog()
            cboTealPercent.Text = frmHelpClass.Var1.Trim
            SendKeys.Send("{TAB}")
            txt24KWeight.Text = Val(txtHWeight.Text) * Val(cboTealPercent.Text)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub btnBillClear_Click(sender As System.Object, e As System.EventArgs) Handles btnBillClear.Click
        
    End Sub
End Class
