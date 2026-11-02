Public Class frmStockAdjustment
    Inherits System.Windows.Forms.Form
    Dim DM As New DataManager
    Dim PostFlag As Boolean
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
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents fplNetTotal As AxEditLib.AxfpDoubleSingle
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents fplSubTotal As AxEditLib.AxfpDoubleSingle
    Friend WithEvents fplGst As AxEditLib.AxfpDoubleSingle
    Friend WithEvents grdDetails As AxFPSpread.AxvaSpread
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblFinPeriod As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblFinYear As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents lblFinStatus As System.Windows.Forms.Label
    Friend WithEvents cmbDepartment As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents fptStockAdjNo As AxEditLib.AxfpText
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cmdNew As System.Windows.Forms.Button
    Friend WithEvents cmdDelete As System.Windows.Forms.Button
    Friend WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents txtRefStockAdjDay As System.Windows.Forms.TextBox
    Friend WithEvents txtRefStockAdjMonth As System.Windows.Forms.TextBox
    Friend WithEvents txtRefStockAdjYear As System.Windows.Forms.TextBox
    Friend WithEvents txtRefStockAdjNo As System.Windows.Forms.TextBox
    Friend WithEvents txtStockAdjDay As System.Windows.Forms.TextBox
    Friend WithEvents txtStockAdjMonth As System.Windows.Forms.TextBox
    Friend WithEvents txtStockAdjYear As System.Windows.Forms.TextBox
    Friend WithEvents fptAccountCodeDebit As AxEditLib.AxfpText
    Friend WithEvents fptAccountNameDebit As AxEditLib.AxfpText
    Friend WithEvents fptAccountNameCredit As AxEditLib.AxfpText
    Friend WithEvents fptAccountCodeCredit As AxEditLib.AxfpText
    Friend WithEvents lnkStockRefAdjNo As System.Windows.Forms.LinkLabel
    Friend WithEvents cboADJTypeType As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmStockAdjustment))
        Me.Label17 = New System.Windows.Forms.Label
        Me.fplSubTotal = New AxEditLib.AxfpDoubleSingle
        Me.fplNetTotal = New AxEditLib.AxfpDoubleSingle
        Me.fplGst = New AxEditLib.AxfpDoubleSingle
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.grdDetails = New AxFPSpread.AxvaSpread
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.lnkStockRefAdjNo = New System.Windows.Forms.LinkLabel
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtRefStockAdjDay = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtRefStockAdjMonth = New System.Windows.Forms.TextBox
        Me.txtRefStockAdjYear = New System.Windows.Forms.TextBox
        Me.txtRefStockAdjNo = New System.Windows.Forms.TextBox
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.lblFinPeriod = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.lblFinYear = New System.Windows.Forms.Label
        Me.Label30 = New System.Windows.Forms.Label
        Me.lblFinStatus = New System.Windows.Forms.Label
        Me.txtStockAdjDay = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtStockAdjMonth = New System.Windows.Forms.TextBox
        Me.txtStockAdjYear = New System.Windows.Forms.TextBox
        Me.fptStockAdjNo = New AxEditLib.AxfpText
        Me.Label24 = New System.Windows.Forms.Label
        Me.cmbDepartment = New System.Windows.Forms.ComboBox
        Me.cboADJTypeType = New System.Windows.Forms.ComboBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.fptAccountCodeDebit = New AxEditLib.AxfpText
        Me.fptAccountNameDebit = New AxEditLib.AxfpText
        Me.fptAccountNameCredit = New AxEditLib.AxfpText
        Me.fptAccountCodeCredit = New AxEditLib.AxfpText
        Me.cmdNew = New System.Windows.Forms.Button
        Me.cmdDelete = New System.Windows.Forms.Button
        Me.cmdExit = New System.Windows.Forms.Button
        Me.cmdSave = New System.Windows.Forms.Button
        CType(Me.fplSubTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fplNetTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fplGst, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        CType(Me.fptStockAdjNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fptAccountCodeDebit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fptAccountNameDebit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fptAccountNameCredit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fptAccountCodeCredit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.White
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label17.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(711, 437)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(63, 22)
        Me.Label17.TabIndex = 352
        Me.Label17.Text = "Net Total "
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'fplSubTotal
        '
        Me.fplSubTotal.ContainingControl = Me
        Me.fplSubTotal.Location = New System.Drawing.Point(780, 223)
        Me.fplSubTotal.Name = "fplSubTotal"
        Me.fplSubTotal.OcxState = CType(resources.GetObject("fplSubTotal.OcxState"), System.Windows.Forms.AxHost.State)
        Me.fplSubTotal.Size = New System.Drawing.Size(124, 20)
        Me.fplSubTotal.TabIndex = 400
        '
        'fplNetTotal
        '
        Me.fplNetTotal.Location = New System.Drawing.Point(777, 435)
        Me.fplNetTotal.Name = "fplNetTotal"
        Me.fplNetTotal.OcxState = CType(resources.GetObject("fplNetTotal.OcxState"), System.Windows.Forms.AxHost.State)
        Me.fplNetTotal.Size = New System.Drawing.Size(124, 25)
        Me.fplNetTotal.TabIndex = 21
        '
        'fplGst
        '
        Me.fplGst.Location = New System.Drawing.Point(777, 409)
        Me.fplGst.Name = "fplGst"
        Me.fplGst.OcxState = CType(resources.GetObject("fplGst.OcxState"), System.Windows.Forms.AxHost.State)
        Me.fplGst.Size = New System.Drawing.Size(124, 20)
        Me.fplGst.TabIndex = 20
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.Controls.Add(Me.grdDetails)
        Me.GroupBox1.Controls.Add(Me.fplSubTotal)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.GroupBox1.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(1, 140)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(921, 264)
        Me.GroupBox1.TabIndex = 368
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Details"
        '
        'grdDetails
        '
        Me.grdDetails.ContainingControl = Me
        Me.grdDetails.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.grdDetails.Location = New System.Drawing.Point(5, 15)
        Me.grdDetails.Name = "grdDetails"
        Me.grdDetails.OcxState = CType(resources.GetObject("grdDetails.OcxState"), System.Windows.Forms.AxHost.State)
        Me.grdDetails.Size = New System.Drawing.Size(913, 206)
        Me.grdDetails.TabIndex = 401
        '
        'Label8
        '
        Me.Label8.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(711, 225)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(59, 16)
        Me.Label8.TabIndex = 355
        Me.Label8.Text = "Sub Total"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(716, 415)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(58, 16)
        Me.Label7.TabIndex = 369
        Me.Label7.Text = "Total GST"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.lnkStockRefAdjNo)
        Me.GroupBox4.Controls.Add(Me.Label5)
        Me.GroupBox4.Controls.Add(Me.txtRefStockAdjDay)
        Me.GroupBox4.Controls.Add(Me.Label11)
        Me.GroupBox4.Controls.Add(Me.txtRefStockAdjMonth)
        Me.GroupBox4.Controls.Add(Me.txtRefStockAdjYear)
        Me.GroupBox4.Controls.Add(Me.txtRefStockAdjNo)
        Me.GroupBox4.Controls.Add(Me.GroupBox5)
        Me.GroupBox4.Controls.Add(Me.txtStockAdjDay)
        Me.GroupBox4.Controls.Add(Me.Label1)
        Me.GroupBox4.Controls.Add(Me.Label2)
        Me.GroupBox4.Controls.Add(Me.txtStockAdjMonth)
        Me.GroupBox4.Controls.Add(Me.txtStockAdjYear)
        Me.GroupBox4.Controls.Add(Me.fptStockAdjNo)
        Me.GroupBox4.Controls.Add(Me.Label24)
        Me.GroupBox4.Controls.Add(Me.cmbDepartment)
        Me.GroupBox4.Controls.Add(Me.cboADJTypeType)
        Me.GroupBox4.Controls.Add(Me.Label25)
        Me.GroupBox4.Controls.Add(Me.Label12)
        Me.GroupBox4.Controls.Add(Me.fptAccountCodeDebit)
        Me.GroupBox4.Controls.Add(Me.fptAccountNameDebit)
        Me.GroupBox4.Controls.Add(Me.fptAccountNameCredit)
        Me.GroupBox4.Controls.Add(Me.fptAccountCodeCredit)
        Me.GroupBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.GroupBox4.Location = New System.Drawing.Point(2, -4)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(916, 142)
        Me.GroupBox4.TabIndex = 0
        Me.GroupBox4.TabStop = False
        '
        'lnkStockRefAdjNo
        '
        Me.lnkStockRefAdjNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkStockRefAdjNo.ForeColor = System.Drawing.Color.Black
        Me.lnkStockRefAdjNo.Location = New System.Drawing.Point(10, 65)
        Me.lnkStockRefAdjNo.Name = "lnkStockRefAdjNo"
        Me.lnkStockRefAdjNo.Size = New System.Drawing.Size(74, 18)
        Me.lnkStockRefAdjNo.TabIndex = 402
        Me.lnkStockRefAdjNo.TabStop = True
        Me.lnkStockRefAdjNo.Text = "Ref. Adj No"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(419, 42)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(128, 16)
        Me.Label5.TabIndex = 401
        Me.Label5.Text = "Account Code ( Debit )"
        '
        'txtRefStockAdjDay
        '
        Me.txtRefStockAdjDay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRefStockAdjDay.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefStockAdjDay.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRefStockAdjDay.Location = New System.Drawing.Point(96, 86)
        Me.txtRefStockAdjDay.MaxLength = 2
        Me.txtRefStockAdjDay.Name = "txtRefStockAdjDay"
        Me.txtRefStockAdjDay.Size = New System.Drawing.Size(21, 22)
        Me.txtRefStockAdjDay.TabIndex = 5
        Me.txtRefStockAdjDay.Tag = "NE;"
        Me.txtRefStockAdjDay.Text = "01"
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(9, 86)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(75, 16)
        Me.Label11.TabIndex = 398
        Me.Label11.Text = "Ref Adj Dt"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRefStockAdjMonth
        '
        Me.txtRefStockAdjMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRefStockAdjMonth.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefStockAdjMonth.Location = New System.Drawing.Point(124, 86)
        Me.txtRefStockAdjMonth.MaxLength = 2
        Me.txtRefStockAdjMonth.Name = "txtRefStockAdjMonth"
        Me.txtRefStockAdjMonth.Size = New System.Drawing.Size(21, 22)
        Me.txtRefStockAdjMonth.TabIndex = 6
        Me.txtRefStockAdjMonth.Tag = "NE;"
        Me.txtRefStockAdjMonth.Text = "01"
        '
        'txtRefStockAdjYear
        '
        Me.txtRefStockAdjYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRefStockAdjYear.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefStockAdjYear.Location = New System.Drawing.Point(154, 86)
        Me.txtRefStockAdjYear.MaxLength = 4
        Me.txtRefStockAdjYear.Name = "txtRefStockAdjYear"
        Me.txtRefStockAdjYear.Size = New System.Drawing.Size(34, 22)
        Me.txtRefStockAdjYear.TabIndex = 7
        Me.txtRefStockAdjYear.Tag = "NE;"
        Me.txtRefStockAdjYear.Text = "1980"
        '
        'txtRefStockAdjNo
        '
        Me.txtRefStockAdjNo.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtRefStockAdjNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRefStockAdjNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefStockAdjNo.Location = New System.Drawing.Point(96, 64)
        Me.txtRefStockAdjNo.Name = "txtRefStockAdjNo"
        Me.txtRefStockAdjNo.ReadOnly = True
        Me.txtRefStockAdjNo.Size = New System.Drawing.Size(113, 22)
        Me.txtRefStockAdjNo.TabIndex = 4
        Me.txtRefStockAdjNo.Text = ""
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.White
        Me.GroupBox5.Controls.Add(Me.Label10)
        Me.GroupBox5.Controls.Add(Me.lblFinPeriod)
        Me.GroupBox5.Controls.Add(Me.Label9)
        Me.GroupBox5.Controls.Add(Me.lblFinYear)
        Me.GroupBox5.Controls.Add(Me.Label30)
        Me.GroupBox5.Controls.Add(Me.lblFinStatus)
        Me.GroupBox5.Location = New System.Drawing.Point(213, 14)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(120, 74)
        Me.GroupBox5.TabIndex = 391
        Me.GroupBox5.TabStop = False
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(5, 10)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(55, 15)
        Me.Label10.TabIndex = 1
        Me.Label10.Text = "Period"
        '
        'lblFinPeriod
        '
        Me.lblFinPeriod.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFinPeriod.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFinPeriod.ForeColor = System.Drawing.Color.Sienna
        Me.lblFinPeriod.Location = New System.Drawing.Point(60, 10)
        Me.lblFinPeriod.Name = "lblFinPeriod"
        Me.lblFinPeriod.Size = New System.Drawing.Size(55, 20)
        Me.lblFinPeriod.TabIndex = 0
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(5, 30)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(55, 15)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "Year"
        '
        'lblFinYear
        '
        Me.lblFinYear.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFinYear.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFinYear.ForeColor = System.Drawing.Color.Sienna
        Me.lblFinYear.Location = New System.Drawing.Point(60, 30)
        Me.lblFinYear.Name = "lblFinYear"
        Me.lblFinYear.Size = New System.Drawing.Size(55, 20)
        Me.lblFinYear.TabIndex = 0
        '
        'Label30
        '
        Me.Label30.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.Color.Black
        Me.Label30.Location = New System.Drawing.Point(5, 55)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(55, 15)
        Me.Label30.TabIndex = 1
        Me.Label30.Text = "Status"
        '
        'lblFinStatus
        '
        Me.lblFinStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFinStatus.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFinStatus.ForeColor = System.Drawing.Color.Sienna
        Me.lblFinStatus.Location = New System.Drawing.Point(60, 50)
        Me.lblFinStatus.Name = "lblFinStatus"
        Me.lblFinStatus.Size = New System.Drawing.Size(55, 20)
        Me.lblFinStatus.TabIndex = 0
        '
        'txtStockAdjDay
        '
        Me.txtStockAdjDay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStockAdjDay.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStockAdjDay.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtStockAdjDay.Location = New System.Drawing.Point(96, 42)
        Me.txtStockAdjDay.MaxLength = 2
        Me.txtStockAdjDay.Name = "txtStockAdjDay"
        Me.txtStockAdjDay.Size = New System.Drawing.Size(21, 22)
        Me.txtStockAdjDay.TabIndex = 1
        Me.txtStockAdjDay.Tag = "NE;"
        Me.txtStockAdjDay.Text = "01"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(9, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 16)
        Me.Label1.TabIndex = 375
        Me.Label1.Text = "Stock Adj No."
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(9, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 16)
        Me.Label2.TabIndex = 377
        Me.Label2.Text = "Stock Adj Dt."
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtStockAdjMonth
        '
        Me.txtStockAdjMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStockAdjMonth.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStockAdjMonth.Location = New System.Drawing.Point(124, 42)
        Me.txtStockAdjMonth.MaxLength = 2
        Me.txtStockAdjMonth.Name = "txtStockAdjMonth"
        Me.txtStockAdjMonth.Size = New System.Drawing.Size(21, 22)
        Me.txtStockAdjMonth.TabIndex = 2
        Me.txtStockAdjMonth.Tag = "NE;"
        Me.txtStockAdjMonth.Text = "01"
        '
        'txtStockAdjYear
        '
        Me.txtStockAdjYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStockAdjYear.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStockAdjYear.Location = New System.Drawing.Point(154, 42)
        Me.txtStockAdjYear.MaxLength = 4
        Me.txtStockAdjYear.Name = "txtStockAdjYear"
        Me.txtStockAdjYear.Size = New System.Drawing.Size(34, 22)
        Me.txtStockAdjYear.TabIndex = 3
        Me.txtStockAdjYear.Tag = "NE;"
        Me.txtStockAdjYear.Text = "1980"
        '
        'fptStockAdjNo
        '
        Me.fptStockAdjNo.ContainingControl = Me
        Me.fptStockAdjNo.Location = New System.Drawing.Point(96, 19)
        Me.fptStockAdjNo.Name = "fptStockAdjNo"
        Me.fptStockAdjNo.OcxState = CType(resources.GetObject("fptStockAdjNo.OcxState"), System.Windows.Forms.AxHost.State)
        Me.fptStockAdjNo.Size = New System.Drawing.Size(112, 21)
        Me.fptStockAdjNo.TabIndex = 0
        Me.fptStockAdjNo.Tag = "NE;"
        '
        'Label24
        '
        Me.Label24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Black
        Me.Label24.Location = New System.Drawing.Point(422, 118)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(66, 16)
        Me.Label24.TabIndex = 382
        Me.Label24.Text = "Department"
        '
        'cmbDepartment
        '
        Me.cmbDepartment.AllowDrop = True
        Me.cmbDepartment.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDepartment.Location = New System.Drawing.Point(559, 112)
        Me.cmbDepartment.Name = "cmbDepartment"
        Me.cmbDepartment.Size = New System.Drawing.Size(349, 24)
        Me.cmbDepartment.TabIndex = 14
        Me.cmbDepartment.Tag = "NE;"
        '
        'cboADJTypeType
        '
        Me.cboADJTypeType.AllowDrop = True
        Me.cboADJTypeType.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboADJTypeType.Items.AddRange(New Object() {"Goods return", "Damaged Goods Return"})
        Me.cboADJTypeType.Location = New System.Drawing.Point(94, 112)
        Me.cboADJTypeType.Name = "cboADJTypeType"
        Me.cboADJTypeType.Size = New System.Drawing.Size(238, 24)
        Me.cboADJTypeType.TabIndex = 8
        Me.cboADJTypeType.Tag = "NE;"
        '
        'Label25
        '
        Me.Label25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.Black
        Me.Label25.Location = New System.Drawing.Point(9, 117)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(74, 16)
        Me.Label25.TabIndex = 382
        Me.Label25.Text = "Adj. Type"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(419, 69)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(133, 16)
        Me.Label12.TabIndex = 401
        Me.Label12.Text = "Account Code ( Credit )"
        '
        'fptAccountCodeDebit
        '
        Me.fptAccountCodeDebit.ContainingControl = Me
        Me.fptAccountCodeDebit.Location = New System.Drawing.Point(557, 40)
        Me.fptAccountCodeDebit.Name = "fptAccountCodeDebit"
        Me.fptAccountCodeDebit.OcxState = CType(resources.GetObject("fptAccountCodeDebit.OcxState"), System.Windows.Forms.AxHost.State)
        Me.fptAccountCodeDebit.Size = New System.Drawing.Size(91, 21)
        Me.fptAccountCodeDebit.TabIndex = 10
        Me.fptAccountCodeDebit.Tag = "NE;"
        '
        'fptAccountNameDebit
        '
        Me.fptAccountNameDebit.ContainingControl = Me
        Me.fptAccountNameDebit.Location = New System.Drawing.Point(652, 40)
        Me.fptAccountNameDebit.Name = "fptAccountNameDebit"
        Me.fptAccountNameDebit.OcxState = CType(resources.GetObject("fptAccountNameDebit.OcxState"), System.Windows.Forms.AxHost.State)
        Me.fptAccountNameDebit.Size = New System.Drawing.Size(256, 21)
        Me.fptAccountNameDebit.TabIndex = 11
        Me.fptAccountNameDebit.Tag = "NE;"
        '
        'fptAccountNameCredit
        '
        Me.fptAccountNameCredit.ContainingControl = Me
        Me.fptAccountNameCredit.Location = New System.Drawing.Point(652, 65)
        Me.fptAccountNameCredit.Name = "fptAccountNameCredit"
        Me.fptAccountNameCredit.OcxState = CType(resources.GetObject("fptAccountNameCredit.OcxState"), System.Windows.Forms.AxHost.State)
        Me.fptAccountNameCredit.Size = New System.Drawing.Size(256, 21)
        Me.fptAccountNameCredit.TabIndex = 13
        Me.fptAccountNameCredit.Tag = "NE;"
        '
        'fptAccountCodeCredit
        '
        Me.fptAccountCodeCredit.ContainingControl = Me
        Me.fptAccountCodeCredit.Location = New System.Drawing.Point(557, 66)
        Me.fptAccountCodeCredit.Name = "fptAccountCodeCredit"
        Me.fptAccountCodeCredit.OcxState = CType(resources.GetObject("fptAccountCodeCredit.OcxState"), System.Windows.Forms.AxHost.State)
        Me.fptAccountCodeCredit.Size = New System.Drawing.Size(91, 21)
        Me.fptAccountCodeCredit.TabIndex = 12
        Me.fptAccountCodeCredit.Tag = "NE;"
        '
        'cmdNew
        '
        Me.cmdNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdNew.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.cmdNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdNew.ForeColor = System.Drawing.Color.Black
        Me.cmdNew.Image = CType(resources.GetObject("cmdNew.Image"), System.Drawing.Image)
        Me.cmdNew.Location = New System.Drawing.Point(5, 433)
        Me.cmdNew.Name = "cmdNew"
        Me.cmdNew.Size = New System.Drawing.Size(99, 26)
        Me.cmdNew.TabIndex = 16
        Me.cmdNew.Text = "&Clear ( F5 )"
        '
        'cmdDelete
        '
        Me.cmdDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdDelete.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.cmdDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.ForeColor = System.Drawing.Color.Black
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.Location = New System.Drawing.Point(203, 433)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(108, 26)
        Me.cmdDelete.TabIndex = 17
        Me.cmdDelete.Tag = "D"
        Me.cmdDelete.Text = "&Delete  ( F8 )"
        '
        'cmdExit
        '
        Me.cmdExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdExit.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.ForeColor = System.Drawing.Color.Black
        Me.cmdExit.Image = CType(resources.GetObject("cmdExit.Image"), System.Drawing.Image)
        Me.cmdExit.Location = New System.Drawing.Point(313, 433)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(108, 26)
        Me.cmdExit.TabIndex = 18
        Me.cmdExit.Text = "Close  ( Esc )"
        '
        'cmdSave
        '
        Me.cmdSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSave.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.cmdSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.ForeColor = System.Drawing.Color.Black
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.Location = New System.Drawing.Point(107, 433)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(94, 26)
        Me.cmdSave.TabIndex = 15
        Me.cmdSave.Tag = "U"
        Me.cmdSave.Text = "&Save ( F6 )"
        '
        'frmStockAdjustment
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(921, 468)
        Me.Controls.Add(Me.cmdNew)
        Me.Controls.Add(Me.cmdDelete)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.fplNetTotal)
        Me.Controls.Add(Me.fplGst)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(5, 5)
        Me.Name = "frmStockAdjustment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Stock Adjustment"
        CType(Me.fplSubTotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fplNetTotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fplGst, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.grdDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        CType(Me.fptStockAdjNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fptAccountCodeDebit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fptAccountNameDebit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fptAccountNameCredit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fptAccountCodeCredit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        If MsgBox("Quit...?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then Me.Close()
    End Sub
    Private Function ScreenValidate() As Boolean
        ScreenValidate = False
        If fnDateValid(txtStockAdjDay.Text, txtStockAdjMonth.Text, txtStockAdjYear.Text) = False Then
            MsgBox("Invalid  Stock Adjustment Date  ", MsgBoxStyle.Critical)
            GoTo EndPara
        End If

        If fnDateValid(txtRefStockAdjDay.Text, txtRefStockAdjMonth.Text, txtRefStockAdjYear.Text) = False Then
            MsgBox("Invalid Reference Stock Adjustment Date  ", MsgBoxStyle.Critical)
            GoTo EndPara
        End If

        If grdDetails.MaxRows < 1 Then
            MsgBox("Invalid Invoice Details ", MsgBoxStyle.Critical)
            GoTo endPara
        End If
        If CheckMe(Me) = False Then
            GoTo endPara
        End If
        If fnValidateGrid() = False Then GoTo endPara
        ScreenValidate = True
        Exit Function
endPara:
        ScreenValidate = False
    End Function
    Public Function fnValidateGrid() As Boolean
        On Error GoTo err
        fnValidateGrid = False
        Dim i As Integer
        For i = 1 To grdDetails.MaxRows
            If Len(Trim(fnGetText(grdDetails, i, 1))) = 0 And fncUnPrimeMoney(Trim(fnGetText(grdDetails, i, 6))) = 0 Then
                'MsgBox(" Error in Item Details row no " & i)
            End If
            If Len(Trim(fnGetText(grdDetails, i, 1))) <> 0 And fncUnPrimeMoney(Trim(fnGetText(grdDetails, i, 6))) = 0 Then
                MsgBox(" Error in Item Details row no " & i)
                Exit Function
            End If
            If Len(Trim(fnGetText(grdDetails, i, 1))) = 0 And fncUnPrimeMoney(Trim(fnGetText(grdDetails, i, 6))) <> 0 Then
                MsgBox(" Error in Item Details row no " & i)
                Exit Function
            End If
        Next
        fnValidateGrid = True
        Exit Function
err:
        fnValidateGrid = False
        MsgBox(Err.Description)
        Err.Clear()
    End Function
    Private Sub txtRetYear_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtStockAdjYear.Leave
        fnFindnPeriodsAndFinYear(txtRefStockAdjYear.Text & "/" & txtRefStockAdjMonth.Text & "/" & txtRefStockAdjDay.Text)
        lblFinPeriod.Text = nPeriods
        lblFinYear.Text = sCurrentFinancialYear
        lblFinStatus.Text = IIf(sCurrFinStatus = "N", "Closed", "Open")
    End Sub
    Private Sub txtRetMonth_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtStockAdjMonth.Leave
        fnFindnPeriodsAndFinYear(txtRefStockAdjYear.Text & "/" & txtRefStockAdjMonth.Text & "/" & txtRefStockAdjDay.Text)
        lblFinPeriod.Text = nPeriods
        lblFinYear.Text = sCurrentFinancialYear
        lblFinStatus.Text = IIf(sCurrFinStatus = "N", "Closed", "Open")
    End Sub
    Private Sub DispRefReturnDetails()
        On Error GoTo ErrTrap
        Dim R As Int32
        Dim J As Long
        Dim GSTPer As Double
        Dim DM As New DataManager
        Dim DR() As DataRow
        Dim I As Int32
        DR = DM.RowReturnProjExecNonQryMDBWithDataRow("Select * from tblStockAdjHeader where stkAdjNo=" & fncFormatSQLText(txtRefStockAdjNo.Text))
        If DR.Length > 0 Then
            If Trim(DR(0)("AccFlag") & "") = "P" Or Trim(DR(0)("AccFlag") & "") = "S" Then
                MsgBox("Stock Adjustment is Already Accounted", MsgBoxStyle.Critical)
                Exit Sub
            End If
            txtRefStockAdjDay.Text = Format(CDate(DR(0)("stkAdjDate") & ""), "dd")
            txtRefStockAdjMonth.Text = Format(CDate(DR(0)("stkAdjDate") & ""), "MM")
            txtRefStockAdjYear.Text = Format(CDate(DR(0)("stkAdjDate") & ""), "yyyy")
            cboADJTypeType.Text = DR(0)("StkAdjTypeCode") & ""
            cmbDepartment.Text = DR(0)("AccDeptCOde") & ""
            'fptAccountCodeDebit.Text = DR(0)("DrAccount") & ""
            'fptAccountNameDebit.Text = DM.ValReturnProjExecNonQry("Select Description from tblChartofAccount where  AccountCode=" & fncFormatSQLText(fptAccountCodeDebit.Text))
            'fptAccountCodeCredit.Text = DR(0)("CrAccount") & ""
            'fptAccountNameCredit.Text = DM.ValReturnProjExecNonQry("Select Description from tblChartofAccount where  AccountCode=" & fncFormatSQLText(fptAccountCodeCredit.Text))
            fnFindnPeriodsAndFinYear(txtRefStockAdjYear.Text & "/" & txtRefStockAdjMonth.Text & "/" & txtRefStockAdjDay.Text)
            lblFinPeriod.Text = nPeriods
            lblFinYear.Text = FinYear
            lblFinStatus.Text = IIf(sCurrFinStatus = "N", "Closed", "Open")
            fplSubTotal.Text = Math.Abs(Val(DR(0)("TotalValue") & ""))
            fplGst.Text = DR(0)("Gst") & ""
            fplNetTotal.Text = Math.Abs(Val(DR(0)("NetTotal") & ""))
        End If
        DR = DM.RowReturnProjExecNonQryMDBWithDataRow("Select * from tblStockAdjDetail where stkAdjNo=" & fncFormatSQLText(txtRefStockAdjNo.Text))
        grdDetails.MaxRows = 0
        If DR.Length > 0 Then
            I = 0
            For J = 0 To DR.Length - 1
                grdDetails.MaxRows += 1
                fnSetText(grdDetails, I + 1, 1, DR(J)("InventoryCode") & "")
                fnSetText(grdDetails, I + 1, 2, DR(J)("Description") & "")
                fnSetText(grdDetails, I + 1, 3, DR(J)("Qty") & "")
                fnSetText(grdDetails, I + 1, 4, DR(J)("UomType") & "")
                fnSetText(grdDetails, I + 1, 5, Math.Abs(Val(DR(J)("Price") & "")))
                fnSetText(grdDetails, I + 1, 6, Math.Abs(Val(DR(J)("totalvalue") & "")))
                fnSetText(grdDetails, I + 1, 7, DR(J)("GST") & "")
                fnSetText(grdDetails, I + 1, 8, Math.Abs(Val(DR(J)("NetAmount") & "")))
                fnSetText(grdDetails, I + 1, 9, DR(J)("DeptCode") & "")
                I += 1
            Next
        End If
        DR = DM.RowReturnProjExecNonQryWithDataRow("Select * from tblStockAdjHeader where stkadjno=" & fncFormatSQLText(fptStockAdjNo.Text))
        If DR.Length > 0 Then
            If Len(Trim(DR(0)("JournalNo") & "")) = 0 Then
                PostFlag = False
            Else
                PostFlag = True
            End If
            fptAccountCodeDebit.Text = Trim(DR(0)("DrAccount") & "")
            fptAccountNameDebit.Text = Trim(DM.ValReturnProjExecNonQry("Select Description from tblChartofAccounts where  AccountCode=" & fncFormatSQLText(fptAccountCodeDebit.Text)))
            fptAccountCodeCredit.Text = Trim(DR(0)("CrAccount") & "")
            fptAccountNameCredit.Text = Trim(DM.ValReturnProjExecNonQry("Select Description from tblChartofAccounts where  AccountCode=" & fncFormatSQLText(fptAccountCodeCredit.Text)))
        Else
            PostFlag = False
        End If
        fptAccountCodeDebit.Focus()
        Exit Sub
ErrTrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Sub
    Private Sub lnkStockRefAdjNo_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkStockRefAdjNo.LinkClicked
        Dim frmHelp As New frmHelpSearch(0, 0, "tblStockAdjHeader", "S", "4", "StkAdjNo", " Accflag <> 'Y' ", "StkAdjNo", "StkAdjNo", 100, "StkAdjTypeCode", "StkAdjTypeCode", 75, "AccDeptName", "AccDeptName", 75, "NetTotal", "NetTotal", 60)
        frmHelp.ShowDialog()
        txtRefStockAdjNo.Text = Trim(frmHelp.Var1)
        DispRefReturnDetails()
    End Sub
    Private Sub txtRefStockAdjNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRefStockAdjNo.KeyDown
        If e.KeyCode = Keys.F1 Then
            Dim frmHelp As New frmHelpSearch(0, 0, "tblStockAdjHeader", "S", "4", "StkAdjNo", " Accflag <> 'Y' ", "StkAdjNo", "StkAdjNo", 100, "StkAdjTypeCode", "StkAdjTypeCode", 75, "AccDeptName", "AccDeptName", 75, "NetTotal", "NetTotal", 60)
            frmHelp.ShowDialog()
            txtRefStockAdjNo.Text = Trim(frmHelp.Var1)
            DispRefReturnDetails()
        End If
    End Sub
    Private Sub frmStockAdjustment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        subGridSetting(grdDetails)
        FunctionControl(Me, "APSTAD")
        DM.FillDatasToDeptCombo(cmbDepartment)
        NextStockAdjMentNo()
    End Sub
    Private Sub NextStockAdjMentNo()
        Dim Dr() As DataRow
        Dr = DM.RowReturnProjExecNonQryWithDataRow("Select NextStkAdjNo from tblCompanyAP")
        If Dr.Length > 0 Then
            fptStockAdjNo.Text = "SJ-" & Format(Val(Dr(0)("NextStkAdjNo") & ""), "0000000")
        Else
            fptStockAdjNo.Text = "SJ-" & Format(1, "0000000")
        End If
        PostFlag = False
        txtStockAdjDay.Text = Format(ServerDate, "dd")
        txtStockAdjMonth.Text = Format(ServerDate, "MM")
        txtStockAdjYear.Text = Format(ServerDate, "yyyy")
        txtRefStockAdjDay.Text = Format(ServerDate, "dd")
        txtRefStockAdjMonth.Text = Format(ServerDate, "MM")
        txtRefStockAdjYear.Text = Format(ServerDate, "yyyy")
    End Sub
    Private Sub cmdNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNew.Click
        fnClearMe(Me)
        fnGridClearText(grdDetails)
        NextStockAdjMentNo()
        PostFlag = False
        txtRefStockAdjNo.Focus()
    End Sub
    Private Sub fptAccountCodeDebit_KeyDownEvent(ByVal sender As Object, ByVal e As AxEditLib._DfpText_KeyDownEvent) Handles fptAccountCodeDebit.KeyDownEvent
        If e.keyCode = Keys.F1 Then
            Dim frmAccount As New frmHelpSearch(0, 0, "tblChartOfAccounts", "L", 4, "AccountCode", "right(rtrim(accountcode),3)='001'", "AccountCode", "AccountCode", 65, "Description", "Description", 175, "AccountType", "AccountType", 75, "SortCode", "SortCode", 60)
            frmAccount.ShowDialog()
            If Len(Trim(frmAccount.Var1)) <> 0 Then
                fptAccountCodeDebit.Text = frmAccount.Var1
                fptAccountNameDebit.Text = frmAccount.Var2
                fptAccountCodeCredit.Focus()
            End If
        End If
    End Sub
    Private Sub fptAccountCodeCredit_KeyDownEvent(ByVal sender As Object, ByVal e As AxEditLib._DfpText_KeyDownEvent) Handles fptAccountCodeCredit.KeyDownEvent
        If e.keyCode = Keys.F1 Then
            Dim frmAccount As New frmHelpSearch(0, 0, "tblChartOfAccounts", "L", 4, "AccountCode", "right(rtrim(accountcode),3)='001'", "AccountCode", "AccountCode", 65, "Description", "Description", 175, "AccountType", "AccountType", 75, "SortCode", "SortCode", 60)
            frmAccount.ShowDialog()
            If Len(Trim(frmAccount.Var1)) <> 0 Then
                fptAccountCodeCredit.Text = frmAccount.Var1
                fptAccountNameCredit.Text = frmAccount.Var2
            End If
        End If
    End Sub
    Private Sub fptAccountCodeDebit_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles fptAccountCodeDebit.LostFocus
        fptAccountNameDebit.Text = DM.ValReturnProjExecNonQry("Select Description from tblChartofaccounts where AccountCode=" & fncFormatSQLText(fptAccountCodeDebit.Text))
    End Sub
    Private Sub fptAccountCodeCredit_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles fptAccountCodeCredit.LostFocus
        fptAccountNameCredit.Text = DM.ValReturnProjExecNonQry("Select Description from tblChartofaccounts where AccountCode=" & fncFormatSQLText(fptAccountCodeCredit.Text))
    End Sub
    Private Sub frmStockAdjustment_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{tab}")
    End Sub
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrTrap
        If ScreenValidate() = False Then
            GoTo ErrTrap
        End If
        If PostFlag = True Then
            MsgBox("This is Posted Entry", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If fnFindPostingStatus(fptStockAdjNo.Text) = True Then
            MsgBox("Stock Entry is Already Posted ", MsgBoxStyle.Critical)
            Exit Sub
        End If


        DM.ProjExecNonQry("sp_StockAdjustmentHeader " & fncFormatSQLText(fptStockAdjNo.Text) & "," & fncFormatSQLDate(txtStockAdjYear.Text & "/" & txtStockAdjMonth.Text & "/" & txtStockAdjDay.Text) & "," & fncFormatSQLText(fptAccountCodeDebit.Text) & "," & fncFormatSQLText(fptAccountCodeCredit.Text) & "," & fncFormatSQLText(sUserID) & "," & fncFormatSQLText(txtRefStockAdjNo.Text))
        'DM.ProjExecNonQry("Update tblStockAdjHeader set AccFlag='S' where StkAdjNo=" & fncFormatSQLText(txtRefStockAdjNo.Text), gsConMasterDB)
        DM.ProjExecNonQry("Update tblStockAdjHeader set AccFlag='S' where StkAdjNo=" & fncFormatSQLText(txtRefStockAdjNo.Text), gsCon)
        MsgBox("Successfully Saved")
        cmdNew_Click(sender, e)
        Exit Sub
ErrTrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Sub
    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        If MsgBox("Do you want to Delete this Record ", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            DM.ProjExecNonQry("delete from tblStockAdjHeader where stkAdjNo=" & fncFormatSQLText(fptStockAdjNo.Text))
            DM.ProjExecNonQry("delete from tblStockAdjDetail where stkAdjNo=" & fncFormatSQLText(fptStockAdjNo.Text))
            'DM.ProjExecNonQry("Update tblStockAdjHeader set AccFlag='N' where StkAdjNo=" & fncFormatSQLText(txtRefStockAdjNo.Text), gsConMasterDB)
            DM.ProjExecNonQry("Update tblStockAdjHeader set AccFlag='N' where StkAdjNo=" & fncFormatSQLText(txtRefStockAdjNo.Text), gsCon)
            MsgBox("Successfully Deleted")
            cmdNew_Click(sender, e)
        End If
    End Sub

    Private Sub fptStockAdjNo_Advance(ByVal sender As System.Object, ByVal e As AxEditLib._DfpText_AdvanceEvent) Handles fptStockAdjNo.Advance

    End Sub
    Private Sub fptStockAdjNo_KeyDownEvent(ByVal sender As Object, ByVal e As AxEditLib._DfpText_KeyDownEvent) Handles fptStockAdjNo.KeyDownEvent
        If e.keyCode = Keys.F1 Then
            Dim frmHelp As New frmHelpSearch(0, 0, "tblStockAdjHeader", "L", "4", "StkAdjNo", "", "StkAdjNo", "StkAdjNo", 100, "StkAdjRefNo", "StkAdjRefNo", 100, "DrAccount", "DrAccount", 75, "NetTotal", "NetTotal", 60)
            frmHelp.ShowDialog()
            fptStockAdjNo.Text = Trim(frmHelp.Var1)
            txtRefStockAdjNo.Text = DM.ValReturnProjExecNonQry("Select StkAdjRefNo from tblStockAdjHeader where StkAdjNo=" & fncFormatSQLText(fptStockAdjNo.Text))
            DispRefReturnDetails()
        End If
    End Sub

    Private Function fnFindPostingStatus(ByVal sStkAdjNO As String) As Boolean
        On Error GoTo ErrTrap
        Dim Dm As New DataManager
        Dim sRes As String
        sRes = Dm.ValReturnProjExecNonQry("Select JournalNo from tblStockAdjHeader where StkAdjNo=" & fncFormatSQLText(sStkAdjNO))
        If Len(Trim(sRes)) = 0 Then
            fnFindPostingStatus = False
        Else
            fnFindPostingStatus = True
        End If

        Exit Function
ErrTrap:
        fnFindPostingStatus = False
    End Function
End Class
