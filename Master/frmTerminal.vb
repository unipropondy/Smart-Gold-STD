Public Class frmTerminal
    Inherits System.Windows.Forms.Form
    Dim Dm As New DataManager
    Dim clsD As New clsterminal
    Dim SearchCol, CCode As String
    Friend WithEvents grdTerminal As System.Windows.Forms.DataGridView
    Friend WithEvents cTerminalCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cTerminalName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cComputerName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox


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
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdDelete As System.Windows.Forms.Button
    Friend WithEvents cmdUpdate As System.Windows.Forms.Button
    Friend WithEvents cmdNew As System.Windows.Forms.Button
    Friend WithEvents txtTerminalCode As System.Windows.Forms.TextBox
    Friend WithEvents lblGroupCode As System.Windows.Forms.Label
    Friend WithEvents txtTerminalName As System.Windows.Forms.TextBox
    Friend WithEvents lbldescription As System.Windows.Forms.Label
    Friend WithEvents txtComputerName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents grpPrinterType As System.Windows.Forms.GroupBox
    Friend WithEvents optReceipt As System.Windows.Forms.RadioButton
    Friend WithEvents optThermal As System.Windows.Forms.RadioButton
    Friend WithEvents optDotMatrix As System.Windows.Forms.RadioButton
    Friend WithEvents optCrystalReports As System.Windows.Forms.RadioButton
    Friend WithEvents txtCustDispPortNo As System.Windows.Forms.TextBox
    Friend WithEvents txtBarcodePortNo As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents chkVoice As System.Windows.Forms.CheckBox
    Friend WithEvents chkCashDrawerAttach As System.Windows.Forms.CheckBox
    Friend WithEvents chkPrinterAttach As System.Windows.Forms.CheckBox
    Friend WithEvents chkBarcodeAttach As System.Windows.Forms.CheckBox
    Friend WithEvents chkCustDispAttach As System.Windows.Forms.CheckBox
    Friend WithEvents txtCashDrawerportNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grpTerminal As System.Windows.Forms.GroupBox
    Friend WithEvents cboLocation As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTerminal))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdUpdate = New System.Windows.Forms.Button()
        Me.cmdNew = New System.Windows.Forms.Button()
        Me.grpTerminal = New System.Windows.Forms.GroupBox()
        Me.txtCashDrawerportNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkVoice = New System.Windows.Forms.CheckBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.chkCashDrawerAttach = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.chkPrinterAttach = New System.Windows.Forms.CheckBox()
        Me.txtBarcodePortNo = New System.Windows.Forms.TextBox()
        Me.chkBarcodeAttach = New System.Windows.Forms.CheckBox()
        Me.txtCustDispPortNo = New System.Windows.Forms.TextBox()
        Me.chkCustDispAttach = New System.Windows.Forms.CheckBox()
        Me.grpPrinterType = New System.Windows.Forms.GroupBox()
        Me.optThermal = New System.Windows.Forms.RadioButton()
        Me.optCrystalReports = New System.Windows.Forms.RadioButton()
        Me.optDotMatrix = New System.Windows.Forms.RadioButton()
        Me.optReceipt = New System.Windows.Forms.RadioButton()
        Me.cboLocation = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtComputerName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtTerminalName = New System.Windows.Forms.TextBox()
        Me.lbldescription = New System.Windows.Forms.Label()
        Me.txtTerminalCode = New System.Windows.Forms.TextBox()
        Me.lblGroupCode = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.grdTerminal = New System.Windows.Forms.DataGridView()
        Me.cTerminalCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cTerminalName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cComputerName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grpTerminal.SuspendLayout()
        Me.grpPrinterType.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdTerminal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdClose
        '
        Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.Color.Black
        Me.cmdClose.Location = New System.Drawing.Point(805, 448)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(100, 40)
        Me.cmdClose.TabIndex = 25
        Me.cmdClose.Text = "Close  (Esc)"
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'cmdDelete
        '
        Me.cmdDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdDelete.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.ForeColor = System.Drawing.Color.Black
        Me.cmdDelete.Location = New System.Drawing.Point(687, 448)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(100, 40)
        Me.cmdDelete.TabIndex = 23
        Me.cmdDelete.Tag = "D"
        Me.cmdDelete.Text = "&Delete  (F8)"
        Me.cmdDelete.UseVisualStyleBackColor = False
        '
        'cmdUpdate
        '
        Me.cmdUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdUpdate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdUpdate.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUpdate.ForeColor = System.Drawing.Color.Black
        Me.cmdUpdate.Location = New System.Drawing.Point(569, 448)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.Size = New System.Drawing.Size(100, 40)
        Me.cmdUpdate.TabIndex = 22
        Me.cmdUpdate.Tag = "U"
        Me.cmdUpdate.Text = "&Save (F7)"
        Me.cmdUpdate.UseVisualStyleBackColor = False
        '
        'cmdNew
        '
        Me.cmdNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdNew.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdNew.ForeColor = System.Drawing.Color.Black
        Me.cmdNew.Location = New System.Drawing.Point(451, 448)
        Me.cmdNew.Name = "cmdNew"
        Me.cmdNew.Size = New System.Drawing.Size(100, 40)
        Me.cmdNew.TabIndex = 24
        Me.cmdNew.Tag = "A"
        Me.cmdNew.Text = "&New (F6)"
        Me.cmdNew.UseVisualStyleBackColor = False
        '
        'grpTerminal
        '
        Me.grpTerminal.Controls.Add(Me.txtCashDrawerportNo)
        Me.grpTerminal.Controls.Add(Me.Label1)
        Me.grpTerminal.Controls.Add(Me.chkVoice)
        Me.grpTerminal.Controls.Add(Me.Label11)
        Me.grpTerminal.Controls.Add(Me.txtRemarks)
        Me.grpTerminal.Controls.Add(Me.Label10)
        Me.grpTerminal.Controls.Add(Me.chkCashDrawerAttach)
        Me.grpTerminal.Controls.Add(Me.Label9)
        Me.grpTerminal.Controls.Add(Me.chkPrinterAttach)
        Me.grpTerminal.Controls.Add(Me.txtBarcodePortNo)
        Me.grpTerminal.Controls.Add(Me.chkBarcodeAttach)
        Me.grpTerminal.Controls.Add(Me.txtCustDispPortNo)
        Me.grpTerminal.Controls.Add(Me.chkCustDispAttach)
        Me.grpTerminal.Controls.Add(Me.grpPrinterType)
        Me.grpTerminal.Controls.Add(Me.cboLocation)
        Me.grpTerminal.Controls.Add(Me.Label8)
        Me.grpTerminal.Controls.Add(Me.Label7)
        Me.grpTerminal.Controls.Add(Me.Label6)
        Me.grpTerminal.Controls.Add(Me.Label5)
        Me.grpTerminal.Controls.Add(Me.Label4)
        Me.grpTerminal.Controls.Add(Me.Label3)
        Me.grpTerminal.Controls.Add(Me.txtComputerName)
        Me.grpTerminal.Controls.Add(Me.Label2)
        Me.grpTerminal.Controls.Add(Me.txtTerminalName)
        Me.grpTerminal.Controls.Add(Me.lbldescription)
        Me.grpTerminal.Controls.Add(Me.txtTerminalCode)
        Me.grpTerminal.Controls.Add(Me.lblGroupCode)
        Me.grpTerminal.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpTerminal.Location = New System.Drawing.Point(451, 115)
        Me.grpTerminal.Name = "grpTerminal"
        Me.grpTerminal.Size = New System.Drawing.Size(456, 326)
        Me.grpTerminal.TabIndex = 476164
        Me.grpTerminal.TabStop = False
        Me.grpTerminal.Text = "Terminal Information"
        '
        'txtCashDrawerportNo
        '
        Me.txtCashDrawerportNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCashDrawerportNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCashDrawerportNo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCashDrawerportNo.Location = New System.Drawing.Point(172, 226)
        Me.txtCashDrawerportNo.MaxLength = 50
        Me.txtCashDrawerportNo.Name = "txtCashDrawerportNo"
        Me.txtCashDrawerportNo.Size = New System.Drawing.Size(96, 21)
        Me.txtCashDrawerportNo.TabIndex = 476192
        Me.txtCashDrawerportNo.Tag = "NE"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(24, 226)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(215, 16)
        Me.Label1.TabIndex = 476193
        Me.Label1.Text = "Cash Drawer Port No"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkVoice
        '
        Me.chkVoice.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkVoice.Location = New System.Drawing.Point(435, 260)
        Me.chkVoice.Name = "chkVoice"
        Me.chkVoice.Size = New System.Drawing.Size(16, 16)
        Me.chkVoice.TabIndex = 21
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(277, 260)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(152, 21)
        Me.Label11.TabIndex = 476191
        Me.Label11.Text = "Voice Enabled"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRemarks.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(172, 260)
        Me.txtRemarks.MaxLength = 50
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(95, 21)
        Me.txtRemarks.TabIndex = 20
        Me.txtRemarks.Tag = "NE"
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(24, 260)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(121, 18)
        Me.Label10.TabIndex = 476189
        Me.Label10.Text = "Remarks"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkCashDrawerAttach
        '
        Me.chkCashDrawerAttach.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCashDrawerAttach.Location = New System.Drawing.Point(435, 226)
        Me.chkCashDrawerAttach.Name = "chkCashDrawerAttach"
        Me.chkCashDrawerAttach.Size = New System.Drawing.Size(16, 16)
        Me.chkCashDrawerAttach.TabIndex = 17
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(277, 226)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(161, 21)
        Me.Label9.TabIndex = 476186
        Me.Label9.Text = "Cash Drawer Attached"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkPrinterAttach
        '
        Me.chkPrinterAttach.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPrinterAttach.Location = New System.Drawing.Point(170, 301)
        Me.chkPrinterAttach.Name = "chkPrinterAttach"
        Me.chkPrinterAttach.Size = New System.Drawing.Size(16, 16)
        Me.chkPrinterAttach.TabIndex = 12
        '
        'txtBarcodePortNo
        '
        Me.txtBarcodePortNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBarcodePortNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBarcodePortNo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBarcodePortNo.Location = New System.Drawing.Point(172, 192)
        Me.txtBarcodePortNo.MaxLength = 50
        Me.txtBarcodePortNo.Name = "txtBarcodePortNo"
        Me.txtBarcodePortNo.Size = New System.Drawing.Size(96, 21)
        Me.txtBarcodePortNo.TabIndex = 11
        Me.txtBarcodePortNo.Tag = "NE"
        '
        'chkBarcodeAttach
        '
        Me.chkBarcodeAttach.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBarcodeAttach.Location = New System.Drawing.Point(435, 192)
        Me.chkBarcodeAttach.Name = "chkBarcodeAttach"
        Me.chkBarcodeAttach.Size = New System.Drawing.Size(16, 16)
        Me.chkBarcodeAttach.TabIndex = 8
        '
        'txtCustDispPortNo
        '
        Me.txtCustDispPortNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustDispPortNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCustDispPortNo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustDispPortNo.Location = New System.Drawing.Point(172, 156)
        Me.txtCustDispPortNo.MaxLength = 50
        Me.txtCustDispPortNo.Name = "txtCustDispPortNo"
        Me.txtCustDispPortNo.Size = New System.Drawing.Size(96, 21)
        Me.txtCustDispPortNo.TabIndex = 7
        Me.txtCustDispPortNo.Tag = "NE"
        '
        'chkCustDispAttach
        '
        Me.chkCustDispAttach.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCustDispAttach.Location = New System.Drawing.Point(435, 158)
        Me.chkCustDispAttach.Name = "chkCustDispAttach"
        Me.chkCustDispAttach.Size = New System.Drawing.Size(16, 16)
        Me.chkCustDispAttach.TabIndex = 6
        '
        'grpPrinterType
        '
        Me.grpPrinterType.Controls.Add(Me.optThermal)
        Me.grpPrinterType.Controls.Add(Me.optCrystalReports)
        Me.grpPrinterType.Controls.Add(Me.optDotMatrix)
        Me.grpPrinterType.Controls.Add(Me.optReceipt)
        Me.grpPrinterType.Location = New System.Drawing.Point(24, 328)
        Me.grpPrinterType.Name = "grpPrinterType"
        Me.grpPrinterType.Size = New System.Drawing.Size(427, 64)
        Me.grpPrinterType.TabIndex = 476179
        Me.grpPrinterType.TabStop = False
        Me.grpPrinterType.Text = "Printer Type"
        Me.grpPrinterType.Visible = False
        '
        'optThermal
        '
        Me.optThermal.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optThermal.Location = New System.Drawing.Point(163, 16)
        Me.optThermal.Name = "optThermal"
        Me.optThermal.Size = New System.Drawing.Size(189, 18)
        Me.optThermal.TabIndex = 14
        Me.optThermal.Tag = "2"
        Me.optThermal.Text = "40 Col Thermal"
        '
        'optCrystalReports
        '
        Me.optCrystalReports.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCrystalReports.Location = New System.Drawing.Point(163, 40)
        Me.optCrystalReports.Name = "optCrystalReports"
        Me.optCrystalReports.Size = New System.Drawing.Size(156, 18)
        Me.optCrystalReports.TabIndex = 16
        Me.optCrystalReports.Tag = "4"
        Me.optCrystalReports.Text = "Reports"
        '
        'optDotMatrix
        '
        Me.optDotMatrix.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optDotMatrix.Location = New System.Drawing.Point(16, 40)
        Me.optDotMatrix.Name = "optDotMatrix"
        Me.optDotMatrix.Size = New System.Drawing.Size(167, 16)
        Me.optDotMatrix.TabIndex = 15
        Me.optDotMatrix.Tag = "3"
        Me.optDotMatrix.Text = "Dot Matrix"
        '
        'optReceipt
        '
        Me.optReceipt.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optReceipt.Location = New System.Drawing.Point(16, 16)
        Me.optReceipt.Name = "optReceipt"
        Me.optReceipt.Size = New System.Drawing.Size(191, 16)
        Me.optReceipt.TabIndex = 13
        Me.optReceipt.Tag = "1"
        Me.optReceipt.Text = "40 Col Receipt"
        '
        'cboLocation
        '
        Me.cboLocation.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLocation.Location = New System.Drawing.Point(172, 120)
        Me.cboLocation.Name = "cboLocation"
        Me.cboLocation.Size = New System.Drawing.Size(275, 21)
        Me.cboLocation.TabIndex = 5
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(24, 296)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(215, 21)
        Me.Label8.TabIndex = 476176
        Me.Label8.Text = "Printer Attached"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(24, 192)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(215, 16)
        Me.Label7.TabIndex = 476175
        Me.Label7.Text = "BarCode Port No"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(277, 192)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(131, 18)
        Me.Label6.TabIndex = 476174
        Me.Label6.Text = "BarCode Attached"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Window
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(24, 156)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(162, 18)
        Me.Label5.TabIndex = 476173
        Me.Label5.Text = "Cust DisplayPort No"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(277, 156)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(170, 18)
        Me.Label4.TabIndex = 476172
        Me.Label4.Text = "Cust Display Attached"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(24, 120)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(183, 18)
        Me.Label3.TabIndex = 476170
        Me.Label3.Text = "Location Code"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtComputerName
        '
        Me.txtComputerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtComputerName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtComputerName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComputerName.Location = New System.Drawing.Point(172, 88)
        Me.txtComputerName.MaxLength = 50
        Me.txtComputerName.Name = "txtComputerName"
        Me.txtComputerName.Size = New System.Drawing.Size(275, 21)
        Me.txtComputerName.TabIndex = 4
        Me.txtComputerName.Tag = "NE"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(24, 88)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(183, 18)
        Me.Label2.TabIndex = 476168
        Me.Label2.Text = "Computer Name"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTerminalName
        '
        Me.txtTerminalName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTerminalName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTerminalName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTerminalName.Location = New System.Drawing.Point(172, 56)
        Me.txtTerminalName.MaxLength = 50
        Me.txtTerminalName.Name = "txtTerminalName"
        Me.txtTerminalName.Size = New System.Drawing.Size(275, 21)
        Me.txtTerminalName.TabIndex = 3
        Me.txtTerminalName.Tag = "NE"
        '
        'lbldescription
        '
        Me.lbldescription.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldescription.ForeColor = System.Drawing.Color.Black
        Me.lbldescription.Location = New System.Drawing.Point(24, 56)
        Me.lbldescription.Name = "lbldescription"
        Me.lbldescription.Size = New System.Drawing.Size(175, 18)
        Me.lbldescription.TabIndex = 476166
        Me.lbldescription.Text = "Terminal Name"
        Me.lbldescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTerminalCode
        '
        Me.txtTerminalCode.AcceptsTab = True
        Me.txtTerminalCode.BackColor = System.Drawing.SystemColors.HighlightText
        Me.txtTerminalCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTerminalCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTerminalCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTerminalCode.ForeColor = System.Drawing.Color.Black
        Me.txtTerminalCode.Location = New System.Drawing.Point(172, 24)
        Me.txtTerminalCode.MaxLength = 10
        Me.txtTerminalCode.Name = "txtTerminalCode"
        Me.txtTerminalCode.Size = New System.Drawing.Size(275, 21)
        Me.txtTerminalCode.TabIndex = 2
        Me.txtTerminalCode.Tag = "NE"
        '
        'lblGroupCode
        '
        Me.lblGroupCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGroupCode.ForeColor = System.Drawing.Color.Black
        Me.lblGroupCode.Location = New System.Drawing.Point(24, 25)
        Me.lblGroupCode.Name = "lblGroupCode"
        Me.lblGroupCode.Size = New System.Drawing.Size(181, 18)
        Me.lblGroupCode.TabIndex = 476164
        Me.lblGroupCode.Text = "Terminal Code"
        Me.lblGroupCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.InitialImage = Nothing
        Me.PictureBox1.Location = New System.Drawing.Point(3, -4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(290, 100)
        Me.PictureBox1.TabIndex = 476203
        Me.PictureBox1.TabStop = False
        '
        'grdTerminal
        '
        Me.grdTerminal.AllowUserToAddRows = False
        Me.grdTerminal.AllowUserToDeleteRows = False
        Me.grdTerminal.AllowUserToResizeColumns = False
        Me.grdTerminal.AllowUserToResizeRows = False
        Me.grdTerminal.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdTerminal.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdTerminal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdTerminal.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cTerminalCode, Me.cTerminalName, Me.cComputerName})
        Me.grdTerminal.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdTerminal.Location = New System.Drawing.Point(3, 115)
        Me.grdTerminal.Name = "grdTerminal"
        Me.grdTerminal.RowHeadersVisible = False
        Me.grdTerminal.Size = New System.Drawing.Size(442, 326)
        Me.grdTerminal.TabIndex = 476204
        '
        'cTerminalCode
        '
        Me.cTerminalCode.HeaderText = "Terminal Code"
        Me.cTerminalCode.Name = "cTerminalCode"
        '
        'cTerminalName
        '
        Me.cTerminalName.HeaderText = "Terminal Name"
        Me.cTerminalName.Name = "cTerminalName"
        Me.cTerminalName.Width = 130
        '
        'cComputerName
        '
        Me.cComputerName.HeaderText = "Computer Name"
        Me.cComputerName.Name = "cComputerName"
        Me.cComputerName.Width = 200
        '
        'frmTerminal
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(919, 500)
        Me.Controls.Add(Me.grdTerminal)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.grpTerminal)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.cmdDelete)
        Me.Controls.Add(Me.cmdUpdate)
        Me.Controls.Add(Me.cmdNew)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTerminal"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Terminal"
        Me.grpTerminal.ResumeLayout(False)
        Me.grpTerminal.PerformLayout()
        Me.grpPrinterType.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdTerminal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub cmdNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNew.Click
        txtTerminalCode.ReadOnly = False
        cmdDelete.Enabled = False
        subClearAll()
        txtTerminalCode.Focus()
    End Sub
    Private Sub subClearAll()
        txtTerminalCode.Text = ""
        txtTerminalName.Text = ""
        txtComputerName.Text = ""
        cboLocation.Text = ""
        txtCustDispPortNo.Text = ""
        txtBarcodePortNo.Text = ""
        txtCashDrawerportNo.Text = ""
        txtRemarks.Text = ""
        chkBarcodeAttach.Checked = False
        chkPrinterAttach.Checked = False
        chkCustDispAttach.Checked = False
        chkCashDrawerAttach.Checked = False
        chkVoice.Checked = False
        optReceipt.Checked = True

    End Sub
    Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click
        If fnScreenValidate() = True Then
            If GradeEdit("C") = True Then
                txtTerminalCode.ReadOnly = True
                cmdDelete.Enabled = True
                subClearAll()
                DispTerminalDetails()
                txtTerminalCode.Focus()
            End If
        End If
    End Sub

    Private Sub frmGradeMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'subGridSetting(grdTerminal)
        subLoadLocation()
        'fnGridClearText(grdTerminal)
        DispTerminalDetails()
        txtTerminalCode.ReadOnly = True
    End Sub
    Private Sub subLoadLocation()
        Dm.SetMasterDataSet(" Select * from tblLocation", "tblLocation")
        Dm.FillDatasToComboListWithFilter(cboLocation, "tblLocation", "LocationCode", "LocationCode")
    End Sub
    Public Function fnScreenValidate() As Boolean
        fnScreenValidate = False
        If Len(Trim(txtTerminalCode.Text)) = 0 Then
            MsgBox("Please enter Terminal Code !", MsgBoxStyle.Critical)
            txtTerminalCode.Focus()
            GoTo Validate
        End If
        If Len(Trim(txtTerminalName.Text)) = 0 Then
            MsgBox("Please enter Terminal Name !", MsgBoxStyle.Critical)
            txtTerminalName.Focus()
            GoTo Validate
        End If
        If Len(Trim(txtComputerName.Text)) = 0 Then
            MsgBox("Please enter Computer Name !", MsgBoxStyle.Critical)
            txtComputerName.Focus()
            GoTo Validate
        End If
        If Len(Trim(cboLocation.Text)) = 0 Then
            MsgBox("Please select a Location  !", MsgBoxStyle.Critical)
            txtComputerName.Focus()
            GoTo Validate
        End If
        If chkCustDispAttach.Checked = True And txtCustDispPortNo.Text.Trim = "" Then
            MsgBox("Please Enter Customer Display Port Number  !", MsgBoxStyle.Critical)
            txtCustDispPortNo.Focus()
            GoTo Validate
        End If
        If chkBarcodeAttach.Checked = True And txtBarcodePortNo.Text.Trim = "" Then
            MsgBox("Please Enter Barcode Display Port Number  !", MsgBoxStyle.Critical)
            txtBarcodePortNo.Focus()
            GoTo Validate
        End If
        If chkCashDrawerAttach.Checked = True And txtCashDrawerportNo.Text.Trim = "" Then
            MsgBox("Please Enter Cash Drawer Port Number  !", MsgBoxStyle.Critical)
            txtCashDrawerportNo.Focus()
            GoTo Validate
        End If
        fnScreenValidate = True
        Exit Function
Validate:
        fnScreenValidate = False
    End Function
    Public Function GradeEdit(ByVal Mode As String) As Boolean
        On Error GoTo err
        Dim CustDisp As String
        Dim Barcode As String
        Dim Printer As String
        Dim CashDrawer As String
        Dim PrinterType As String
        Dim VoiceEnabled As String

        GradeEdit = False
        CustDisp = IIf(chkCustDispAttach.Checked, "1", "0")
        Barcode = IIf(chkBarcodeAttach.Checked, "1", "0")
        Printer = IIf(chkPrinterAttach.Checked, "1", "0")
        CashDrawer = IIf(chkCashDrawerAttach.Checked, "1", "0")
        VoiceEnabled = IIf(chkVoice.Checked, "1", "0")
        If optReceipt.Checked Then
            PrinterType = optReceipt.Tag
        ElseIf optThermal.Checked Then
            PrinterType = optThermal.Tag
        ElseIf optDotMatrix.Checked Then
            PrinterType = optDotMatrix.Tag
        ElseIf optCrystalReports.Checked Then
            PrinterType = optCrystalReports.Tag
        End If
        If Mode = "C" Then
            If clsD.fnSaveTerminal(txtTerminalCode.Text.Trim, txtTerminalName.Text.Trim, txtComputerName.Text.Trim, cboLocation.Text.Trim, CustDisp, txtCustDispPortNo.Text, Barcode, txtBarcodePortNo.Text, Printer, PrinterType, CashDrawer, txtCashDrawerportNo.Text, txtRemarks.Text, VoiceEnabled, sUserID, Mode) = True Then
                MsgBox("Sucessfully Saved the Details ", MsgBoxStyle.Information)
            Else
                MsgBox("Error in Save Record", MsgBoxStyle.Information)
            End If
        ElseIf Mode = "D" Then
            If clsD.fnSaveTerminal(txtTerminalCode.Text.Trim, txtTerminalName.Text.Trim, txtComputerName.Text.Trim, cboLocation.Text.Trim, CustDisp, txtCustDispPortNo.Text, Barcode, txtBarcodePortNo.Text, Printer, PrinterType, CashDrawer, txtCashDrawerportNo.Text, txtRemarks.Text, VoiceEnabled, sUserID, Mode) = True Then
                MsgBox("Sucessfully Deleted the Details ", MsgBoxStyle.Information)
            Else
                MsgBox("Error in Save Record", MsgBoxStyle.Information)
            End If
        End If
        GradeEdit = True
        Exit Function
err:
        GradeEdit = False
        Err.Clear()

    End Function


    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        Dim dr() As DataRow
        If MsgBox("Do you want to delete?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
            If GradeEdit("D") = True Then
                subClearAll()
                DispTerminalDetails()
                txtTerminalCode.Focus()
            End If
        Else
            Exit Sub
        End If

    End Sub
    Public Sub DispTerminalDetails()
        Dim DrD As DataRow()
        Dim r As Integer
        DrD = Dm.RowReturnProjExecNonQryWithDataRow("Select * from tblTerminal")
        grdTerminal.RowCount = 0

        If DrD.Length > 0 Then
            For r = 0 To DrD.Length - 1
                grdTerminal.Rows.Add()
                fnSetdgvText(grdTerminal, r, 0, DrD(r)("TerminalCode") & "")
                fnSetdgvText(grdTerminal, r, 1, DrD(r)("TerminalName") & "")
                fnSetdgvText(grdTerminal, r, 2, DrD(r)("ComputerName") & "")
            Next
            ' grdTerminal.MaxRows -= 1
            txtTerminalCode.Text = Trim(DrD(0)("TerminalCode") & "")
            txtTerminalName.Text = Trim(DrD(0)("TerminalName") & "")
            txtComputerName.Text = Trim(DrD(0)("ComputerName") & "")
            cboLocation.Text = Trim(DrD(0)("LocationCode") & "")
            If Trim(DrD(0)("CustDisplayAttached") & "") = "1" Then
                chkCustDispAttach.Checked = True
            End If
            txtCustDispPortNo.Text = Trim(DrD(0)("CusDisplayPortNo") & "")
            If Trim(DrD(0)("BarcodeAttached") & "") = "1" Then
                chkBarcodeAttach.Checked = True
            End If
            txtBarcodePortNo.Text = Trim(DrD(0)("BarcodePortNo") & "")
            If Trim(DrD(0)("CashDrawerAttached") & "") = "1" Then
                chkCashDrawerAttach.Checked = True
            End If
            txtCashDrawerportNo.Text = Trim(DrD(0)("DrawerPortNo") & "")
            If Trim(DrD(0)("PrinterAttached") & "") = "1" Then
                chkPrinterAttach.Checked = True
            End If
            If Trim(DrD(0)("PrinterType") & "") = "1" Then
                optReceipt.Checked = True
            ElseIf Trim(DrD(0)("PrinterType") & "") = "2" Then
                optThermal.Checked = True
            ElseIf Trim(DrD(0)("PrinterType") & "") = "3" Then
                optDotMatrix.Checked = True
            ElseIf Trim(DrD(0)("PrinterType") & "") = "4" Then
                optCrystalReports.Checked = True
            End If
            If Trim(DrD(0)("VoiceEnabled") & "") = "1" Then
                chkVoice.Checked = True
            End If
            txtRemarks.Text = Trim(DrD(0)("Remarks") & "")
            txtTerminalCode.Focus()
        End If
    End Sub
    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        'If MsgBox("Do you want to Exit?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        Me.Close()
        'End If
    End Sub
    Private Sub grdTerminal_KeyDownEvent(ByVal sender As System.Object, ByVal e As AxFPSpread._DSpreadEvents_KeyDownEvent)
        Dim R As Integer
        Dim DelCode As String
        If e.keyCode = Keys.Down Then
            R = grdTerminal.CurrentRow.Index + 1
            If R > grdTerminal.RowCount Then
                Exit Sub
            End If
            'txtTerminalCode.Text = Trim(fnGetText(grdTerminal, R, 1))
            'txtTerminalName.Text = Trim(fnGetText(grdTerminal, R, 2))
            'txtComputerName.Text = Trim(fnGetText(grdTerminal, R, 3))

            txtTerminalCode.Text = Trim(fnGetdgvText(grdTerminal, R, 1))
            txtTerminalName.Text = Trim(fnGetdgvText(grdTerminal, R, 2))
            txtComputerName.Text = Trim(fnGetdgvText(grdTerminal, R, 3))

        ElseIf e.keyCode = Keys.Up Then
            R = grdTerminal.CurrentRow.Index - 1
            If R < 1 Then
                Exit Sub
            End If
        End If
    End Sub

    Private Sub grdTerminal_ClickEvent(ByVal sender As System.Object, ByVal e As AxFPSpread._DSpreadEvents_ClickEvent)
        Dim dr() As DataRow
        Dim R As Integer
        cmdDelete.Enabled = True
        txtTerminalCode.ReadOnly = True
        dr = Dm.RowReturnProjExecNonQryWithDataRow("select * from tblTerminal where TerminalCode=" & fncFormatSQLText(GetText(grdTerminal, e.row, 1)))
        If dr.Length > 0 Then
            subClearAll()
            txtTerminalCode.Text = Trim(dr(0)("TerminalCode"))
            txtTerminalName.Text = Trim(dr(0)("TerminalName"))
            txtComputerName.Text = Trim(dr(0)("ComputerName"))
            cboLocation.Text = Trim(dr(0)("LocationCode") & "")
            If Trim(dr(0)("CustDisplayAttached") & "") = "1" Then
                chkCustDispAttach.Checked = True
            End If
            txtCustDispPortNo.Text = Trim(dr(0)("CusDisplayPortNo") & "")
            If Trim(dr(0)("BarcodeAttached") & "") = "1" Then
                chkBarcodeAttach.Checked = True
            End If
            txtBarcodePortNo.Text = Trim(dr(0)("BarcodePortNo") & "")
            If Trim(dr(0)("CashDrawerAttached") & "") = "1" Then
                chkCashDrawerAttach.Checked = True
            End If
            txtCashDrawerportNo.Text = Trim(dr(0)("DrawerPortNo") & "")
            If Trim(dr(0)("PrinterAttached") & "") = "1" Then
                chkPrinterAttach.Checked = True
            End If
            If Trim(dr(0)("PrinterType") & "") = "1" Then
                optReceipt.Checked = True
            ElseIf Trim(dr(0)("PrinterType") & "") = "2" Then
                optThermal.Checked = True
            ElseIf Trim(dr(0)("PrinterType") & "") = "3" Then
                optDotMatrix.Checked = True
            ElseIf Trim(dr(0)("PrinterType") & "") = "4" Then
                optCrystalReports.Checked = True
            End If
            If Trim(dr(0)("VoiceEnabled") & "") = "1" Then
                chkVoice.Checked = True
            End If
            txtRemarks.Text = Trim(dr(0)("Remarks") & "")

        End If

    End Sub

    Private Sub frmTerminal_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Up
                SendKeys.Send("+{Tab}")
            Case Keys.Down
                SendKeys.Send("{Tab}")
            Case Keys.Escape
                cmdClose_Click(sender, e)
            Case Keys.Enter
                SendKeys.Send("{tab}")
            Case Keys.F6
                cmdNew_Click(sender, e)
            Case Keys.F7
                If cmdUpdate.Enabled = True Then cmdUpdate_Click(sender, e)
            Case Keys.F8
                If cmdDelete.Enabled = True Then cmdDelete_Click(sender, e)
        End Select
    End Sub

    Private Sub grdTerminal_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CCode = txtTerminalCode.Text
    End Sub

    Private Sub txtTerminalCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTerminalCode.LostFocus
        Dim dr() As DataRow
        Dim R As Integer
        Dim i As Long
        dr = Dm.RowReturnProjExecNonQryWithDataRow("select * from tblTerminal where TerminalCode=" & fncFormatSQLText(txtTerminalCode.Text.Trim))
        If dr.Length > 0 Then
            txtTerminalCode.Text = Trim(dr(0)("TerminalCode"))
            txtTerminalName.Text = Trim(dr(0)("TerminalName"))
            txtComputerName.Text = Trim(dr(0)("ComputerName"))
            cboLocation.Text = Trim(dr(0)("LocationCode") & "")
            If Trim(dr(0)("CustDisplayAttached") & "") = "1" Then
                chkCustDispAttach.Checked = True
            End If
            txtCustDispPortNo.Text = Trim(dr(0)("CusDisplayPortNo") & "")
            If Trim(dr(0)("BarcodeAttached") & "") = "1" Then
                chkBarcodeAttach.Checked = True
            End If
            txtBarcodePortNo.Text = Trim(dr(0)("BarcodePortNo") & "")
            If Trim(dr(0)("CashDrawerAttached") & "") = "1" Then
                chkCashDrawerAttach.Checked = True
            End If
            txtCashDrawerportNo.Text = Trim(dr(0)("DrawerPortNo") & "")
            If Trim(dr(0)("PrinterAttached") & "") = "1" Then
                chkPrinterAttach.Checked = True
            End If
            If Trim(dr(0)("PrinterType") & "") = "1" Then
                optReceipt.Checked = True
            ElseIf Trim(dr(0)("PrinterType") & "") = "2" Then
                optThermal.Checked = True
            ElseIf Trim(dr(0)("PrinterType") & "") = "3" Then
                optDotMatrix.Checked = True
            ElseIf Trim(dr(0)("PrinterType") & "") = "4" Then
                optCrystalReports.Checked = True
            End If
            If Trim(dr(0)("VoiceEnabled") & "") = "1" Then
                chkVoice.Checked = True
            End If
            txtRemarks.Text = Trim(dr(0)("Remarks") & "")
            'For i = 1 To grdTerminal.RowCount
            '    If txtTerminalCode.Text.Trim = Trim(fnGetdgvText(grdTerminal, i, 1)) Then
            '        grdTerminal.Row = i
            '        grdTerminal.Col = 1
            '        grdTerminal.Action = FPSpread.ActionConstants.ActionActiveCell
            '        Exit For
            '    End If
            'Next


        End If

    End Sub

    Private Sub txtTerminalCode_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtTerminalCode.TextChanged

    End Sub

    Private Sub grdTerminal_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdTerminal.CellContentClick

    End Sub

    Private Sub grdTerminal_Click(sender As Object, e As System.EventArgs) Handles grdTerminal.Click
        txtTerminalCode.Text = fnGetdgvText(grdTerminal, grdTerminal.CurrentRow.Index, cTerminalCode.Index)
        txtTerminalCode_LostFocus(Nothing, Nothing)
    End Sub
End Class
