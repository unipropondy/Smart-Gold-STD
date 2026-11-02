Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class FrmEODHistory
    Inherits System.Windows.Forms.Form
    Dim dm As New DataManager

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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents txtLYYYY As System.Windows.Forms.TextBox
    Friend WithEvents txtLMM As System.Windows.Forms.TextBox
    Friend WithEvents txtLDD As System.Windows.Forms.TextBox
    Friend WithEvents txtYYYY As System.Windows.Forms.TextBox
    Friend WithEvents txtMM As System.Windows.Forms.TextBox
    Friend WithEvents txtDD As System.Windows.Forms.TextBox
    Friend WithEvents OptPreview As System.Windows.Forms.RadioButton
    Friend WithEvents optPrint As System.Windows.Forms.RadioButton
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents ChkSalesDetailJournal As System.Windows.Forms.CheckBox
    Friend WithEvents ChkSalesmanWiseJournal As System.Windows.Forms.CheckBox
    Friend WithEvents ChkTerminalWiseSales As System.Windows.Forms.CheckBox
    Friend WithEvents chkRefundJournal As System.Windows.Forms.CheckBox
    Friend WithEvents chkTradeinJournal As System.Windows.Forms.CheckBox
    Friend WithEvents chkInstallmentJournal As System.Windows.Forms.CheckBox
    Friend WithEvents ChkCSRPbyCategory As System.Windows.Forms.CheckBox
    Friend WithEvents chkCSRWbyCategory As System.Windows.Forms.CheckBox
    Friend WithEvents chkCSRPbyTray As System.Windows.Forms.CheckBox
    Friend WithEvents chkBelowCostSales As System.Windows.Forms.CheckBox
    Friend WithEvents chkClosedBillsListing As System.Windows.Forms.CheckBox
    Friend WithEvents chkPurchaseReturnJournal As System.Windows.Forms.CheckBox
    Friend WithEvents chkGoldBarPurchase As System.Windows.Forms.CheckBox
    Friend WithEvents chkMiscellaneousJournal As System.Windows.Forms.CheckBox
    Friend WithEvents chkNormalSalesProfit As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmEODHistory))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkNormalSalesProfit = New System.Windows.Forms.CheckBox()
        Me.chkMiscellaneousJournal = New System.Windows.Forms.CheckBox()
        Me.chkGoldBarPurchase = New System.Windows.Forms.CheckBox()
        Me.chkPurchaseReturnJournal = New System.Windows.Forms.CheckBox()
        Me.chkClosedBillsListing = New System.Windows.Forms.CheckBox()
        Me.chkBelowCostSales = New System.Windows.Forms.CheckBox()
        Me.chkCSRPbyTray = New System.Windows.Forms.CheckBox()
        Me.chkCSRWbyCategory = New System.Windows.Forms.CheckBox()
        Me.ChkCSRPbyCategory = New System.Windows.Forms.CheckBox()
        Me.chkInstallmentJournal = New System.Windows.Forms.CheckBox()
        Me.chkTradeinJournal = New System.Windows.Forms.CheckBox()
        Me.chkRefundJournal = New System.Windows.Forms.CheckBox()
        Me.ChkTerminalWiseSales = New System.Windows.Forms.CheckBox()
        Me.ChkSalesmanWiseJournal = New System.Windows.Forms.CheckBox()
        Me.ChkSalesDetailJournal = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.optPrint = New System.Windows.Forms.RadioButton()
        Me.OptPreview = New System.Windows.Forms.RadioButton()
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
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkNormalSalesProfit)
        Me.GroupBox1.Controls.Add(Me.chkMiscellaneousJournal)
        Me.GroupBox1.Controls.Add(Me.chkGoldBarPurchase)
        Me.GroupBox1.Controls.Add(Me.chkPurchaseReturnJournal)
        Me.GroupBox1.Controls.Add(Me.chkClosedBillsListing)
        Me.GroupBox1.Controls.Add(Me.chkBelowCostSales)
        Me.GroupBox1.Controls.Add(Me.chkCSRPbyTray)
        Me.GroupBox1.Controls.Add(Me.chkCSRWbyCategory)
        Me.GroupBox1.Controls.Add(Me.ChkCSRPbyCategory)
        Me.GroupBox1.Controls.Add(Me.chkInstallmentJournal)
        Me.GroupBox1.Controls.Add(Me.chkTradeinJournal)
        Me.GroupBox1.Controls.Add(Me.chkRefundJournal)
        Me.GroupBox1.Controls.Add(Me.ChkTerminalWiseSales)
        Me.GroupBox1.Controls.Add(Me.ChkSalesmanWiseJournal)
        Me.GroupBox1.Controls.Add(Me.ChkSalesDetailJournal)
        Me.GroupBox1.Location = New System.Drawing.Point(24, 80)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(344, 384)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "EOD Reports"
        '
        'chkNormalSalesProfit
        '
        Me.chkNormalSalesProfit.Location = New System.Drawing.Point(16, 360)
        Me.chkNormalSalesProfit.Name = "chkNormalSalesProfit"
        Me.chkNormalSalesProfit.Size = New System.Drawing.Size(256, 16)
        Me.chkNormalSalesProfit.TabIndex = 15
        Me.chkNormalSalesProfit.Tag = "14"
        Me.chkNormalSalesProfit.Text = "Normal Sales Profit"
        '
        'chkMiscellaneousJournal
        '
        Me.chkMiscellaneousJournal.Location = New System.Drawing.Point(16, 336)
        Me.chkMiscellaneousJournal.Name = "chkMiscellaneousJournal"
        Me.chkMiscellaneousJournal.Size = New System.Drawing.Size(256, 16)
        Me.chkMiscellaneousJournal.TabIndex = 14
        Me.chkMiscellaneousJournal.Tag = "13"
        Me.chkMiscellaneousJournal.Text = "Miscellaneous Journal"
        '
        'chkGoldBarPurchase
        '
        Me.chkGoldBarPurchase.Location = New System.Drawing.Point(16, 312)
        Me.chkGoldBarPurchase.Name = "chkGoldBarPurchase"
        Me.chkGoldBarPurchase.Size = New System.Drawing.Size(256, 16)
        Me.chkGoldBarPurchase.TabIndex = 13
        Me.chkGoldBarPurchase.Tag = "12"
        Me.chkGoldBarPurchase.Text = "Gold Bar Purchase"
        '
        'chkPurchaseReturnJournal
        '
        Me.chkPurchaseReturnJournal.Location = New System.Drawing.Point(16, 288)
        Me.chkPurchaseReturnJournal.Name = "chkPurchaseReturnJournal"
        Me.chkPurchaseReturnJournal.Size = New System.Drawing.Size(256, 16)
        Me.chkPurchaseReturnJournal.TabIndex = 12
        Me.chkPurchaseReturnJournal.Tag = "11"
        Me.chkPurchaseReturnJournal.Text = "Purchase Return Journal"
        '
        'chkClosedBillsListing
        '
        Me.chkClosedBillsListing.Location = New System.Drawing.Point(16, 264)
        Me.chkClosedBillsListing.Name = "chkClosedBillsListing"
        Me.chkClosedBillsListing.Size = New System.Drawing.Size(256, 16)
        Me.chkClosedBillsListing.TabIndex = 11
        Me.chkClosedBillsListing.Tag = "10"
        Me.chkClosedBillsListing.Text = "Closed Bills Listing"
        '
        'chkBelowCostSales
        '
        Me.chkBelowCostSales.Location = New System.Drawing.Point(16, 240)
        Me.chkBelowCostSales.Name = "chkBelowCostSales"
        Me.chkBelowCostSales.Size = New System.Drawing.Size(256, 16)
        Me.chkBelowCostSales.TabIndex = 10
        Me.chkBelowCostSales.Tag = "9"
        Me.chkBelowCostSales.Text = "Below Cost Sales Report"
        '
        'chkCSRPbyTray
        '
        Me.chkCSRPbyTray.Location = New System.Drawing.Point(16, 216)
        Me.chkCSRPbyTray.Name = "chkCSRPbyTray"
        Me.chkCSRPbyTray.Size = New System.Drawing.Size(256, 16)
        Me.chkCSRPbyTray.TabIndex = 9
        Me.chkCSRPbyTray.Tag = "8"
        Me.chkCSRPbyTray.Text = "Closing Stock Report (Pieces) by Tray"
        '
        'chkCSRWbyCategory
        '
        Me.chkCSRWbyCategory.Location = New System.Drawing.Point(16, 192)
        Me.chkCSRWbyCategory.Name = "chkCSRWbyCategory"
        Me.chkCSRWbyCategory.Size = New System.Drawing.Size(256, 16)
        Me.chkCSRWbyCategory.TabIndex = 8
        Me.chkCSRWbyCategory.Tag = "7"
        Me.chkCSRWbyCategory.Text = "Closing Stock Report (Weight) by Category"
        '
        'ChkCSRPbyCategory
        '
        Me.ChkCSRPbyCategory.Location = New System.Drawing.Point(16, 168)
        Me.ChkCSRPbyCategory.Name = "ChkCSRPbyCategory"
        Me.ChkCSRPbyCategory.Size = New System.Drawing.Size(256, 16)
        Me.ChkCSRPbyCategory.TabIndex = 7
        Me.ChkCSRPbyCategory.Tag = "6"
        Me.ChkCSRPbyCategory.Text = "Closing Stock Report (Pieces) by Category"
        '
        'chkInstallmentJournal
        '
        Me.chkInstallmentJournal.Location = New System.Drawing.Point(16, 144)
        Me.chkInstallmentJournal.Name = "chkInstallmentJournal"
        Me.chkInstallmentJournal.Size = New System.Drawing.Size(168, 16)
        Me.chkInstallmentJournal.TabIndex = 6
        Me.chkInstallmentJournal.Tag = "5"
        Me.chkInstallmentJournal.Text = "Installment Journal"
        '
        'chkTradeinJournal
        '
        Me.chkTradeinJournal.Location = New System.Drawing.Point(16, 120)
        Me.chkTradeinJournal.Name = "chkTradeinJournal"
        Me.chkTradeinJournal.Size = New System.Drawing.Size(168, 16)
        Me.chkTradeinJournal.TabIndex = 5
        Me.chkTradeinJournal.Tag = "4"
        Me.chkTradeinJournal.Text = "Trade In Journal"
        '
        'chkRefundJournal
        '
        Me.chkRefundJournal.Location = New System.Drawing.Point(16, 96)
        Me.chkRefundJournal.Name = "chkRefundJournal"
        Me.chkRefundJournal.Size = New System.Drawing.Size(168, 16)
        Me.chkRefundJournal.TabIndex = 4
        Me.chkRefundJournal.Tag = "3"
        Me.chkRefundJournal.Text = "Refund Journal"
        '
        'ChkTerminalWiseSales
        '
        Me.ChkTerminalWiseSales.Location = New System.Drawing.Point(16, 72)
        Me.ChkTerminalWiseSales.Name = "ChkTerminalWiseSales"
        Me.ChkTerminalWiseSales.Size = New System.Drawing.Size(168, 16)
        Me.ChkTerminalWiseSales.TabIndex = 3
        Me.ChkTerminalWiseSales.Tag = "2"
        Me.ChkTerminalWiseSales.Text = "Terminal Wise Journal"
        '
        'ChkSalesmanWiseJournal
        '
        Me.ChkSalesmanWiseJournal.Location = New System.Drawing.Point(16, 48)
        Me.ChkSalesmanWiseJournal.Name = "ChkSalesmanWiseJournal"
        Me.ChkSalesmanWiseJournal.Size = New System.Drawing.Size(168, 16)
        Me.ChkSalesmanWiseJournal.TabIndex = 2
        Me.ChkSalesmanWiseJournal.Tag = "1"
        Me.ChkSalesmanWiseJournal.Text = "Salesman Wise Journal"
        '
        'ChkSalesDetailJournal
        '
        Me.ChkSalesDetailJournal.Location = New System.Drawing.Point(16, 24)
        Me.ChkSalesDetailJournal.Name = "ChkSalesDetailJournal"
        Me.ChkSalesDetailJournal.Size = New System.Drawing.Size(168, 16)
        Me.ChkSalesDetailJournal.TabIndex = 1
        Me.ChkSalesDetailJournal.Tag = "0"
        Me.ChkSalesDetailJournal.Text = "Sales Detail Journal"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.GroupBox3)
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
        Me.GroupBox2.Location = New System.Drawing.Point(384, 176)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(240, 120)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.optPrint)
        Me.GroupBox3.Controls.Add(Me.OptPreview)
        Me.GroupBox3.Location = New System.Drawing.Point(60, 75)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(148, 40)
        Me.GroupBox3.TabIndex = 71
        Me.GroupBox3.TabStop = False
        '
        'optPrint
        '
        Me.optPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPrint.Location = New System.Drawing.Point(95, 16)
        Me.optPrint.Name = "optPrint"
        Me.optPrint.Size = New System.Drawing.Size(48, 16)
        Me.optPrint.TabIndex = 25
        Me.optPrint.Text = "Print"
        '
        'OptPreview
        '
        Me.OptPreview.Checked = True
        Me.OptPreview.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptPreview.Location = New System.Drawing.Point(10, 16)
        Me.OptPreview.Name = "OptPreview"
        Me.OptPreview.Size = New System.Drawing.Size(72, 16)
        Me.OptPreview.TabIndex = 24
        Me.OptPreview.TabStop = True
        Me.OptPreview.Text = "Preview"
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
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(549, 304)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 27
        Me.btnClose.Text = "Close(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnOk
        '
        Me.btnOk.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnOk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOk.Image = CType(resources.GetObject("btnOk.Image"), System.Drawing.Image)
        Me.btnOk.Location = New System.Drawing.Point(469, 304)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(75, 23)
        Me.btnOk.TabIndex = 26
        Me.btnOk.Text = "Report(F9)"
        Me.btnOk.UseVisualStyleBackColor = False
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(24, 3)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(210, 75)
        Me.pic.TabIndex = 54
        Me.pic.TabStop = False
        '
        'FrmEODHistory
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 13)
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(632, 472)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmEODHistory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "EOD History"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub FrmEODHistory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        getLastEODDate()
        subFillReports()
    End Sub
    Private Sub getLastEODDate()
        Dim sSql As String
        Dim Dr As DataRow()
        Try
            txtDD.Text = Format(Date.Now.Day, "00")
            txtMM.Text = Format(Date.Now.Month, "00")
            txtYYYY.Text = Date.Now.Year

            sSql = "Select convert(char(8),EODDate,112) EODDate From tblLocation where LocationCode = " & fncFormatSQLText(Trim$(gthisLocation))
            Dr = dm.RowReturnProjExecNonQryWithDataRow(sSql)
            If Dr.Length > 0 And Trim(Dr(0)("EODDate") & "") <> "" Then
                txtLDD.Text = Mid(Dr(0)("EODDate"), 7, 2)
                txtLMM.Text = Mid(Dr(0)("EODDate"), 5, 2)
                txtLYYYY.Text = Mid(Dr(0)("EODDate"), 1, 4)
            Else
                txtLDD.Text = Now.Date.Day
                txtLMM.Text = Now.Date.Month
                txtLYYYY.Text = Now.Date.Year
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Dim Ds As DataSet
        Dim frmrep As New frmReports
        Dim frmrep1 As New frmReports
        Dim CR As New ReportDocument
        Dim CR1 As New ReportDocument
        Dim objcompany As New clsCompany
        Dim EODDate As String
        Dim sTelephone As String
        Dim sReportTitle As String
        Dim sTradeInReportTitle As String
        Dim dsSales As New DataSet
        Dim objEod As New ClsEOD
        Dim sSalesDate As String
        Try
            Me.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            EODDate = txtYYYY.Text.Trim & "," & txtMM.Text.Trim & "," & txtDD.Text.Trim
            If Not IsDate(EODDate) Then
                MessageBox.Show("Enter Valid EOD Date", "GoldPOS")
                Exit Sub
            End If
            Ds = objcompany.getCompanyName
            If Ds.Tables("GetCompanyName").Rows.Count > 0 Then
                strCompanyname = Trim(Ds.Tables("GetCompanyName").Rows(0)("Name") & "")
                strAddress = Trim(Ds.Tables("GetCompanyName").Rows(0)("Address1") & "") & " " & _
                             Trim(Ds.Tables("GetCompanyName").Rows(0)("Address2") & "") & " " & _
                             Trim(Ds.Tables("GetCompanyName").Rows(0)("City") & "")
                sTelephone = "Phone :" & Space(2) & Trim(Ds.Tables("GetCompanyName").Rows(0)("Phone") & "") & Space(2) & _
                             "Fax :" & Space(2) & Trim(Ds.Tables("GetCompanyName").Rows(0)("Fax") & "")

            End If
            sTradeInReportTitle = "TradeIn Sales Journal Detail For  " & txtDD.Text.Trim & "/" & txtMM.Text.Trim & "/" & txtYYYY.Text.Trim
            'load EOD sales
            If UCase(sCompanyShortCode) <> "MJ" Then
                strpath = Application.StartupPath & "\Reports\rptEOD.rpt"
            Else
                strpath = Application.StartupPath & "\Reports\rptEODForMeena.rpt"
            End If
            CR.Load(strpath)
            If Not ReportLogOnToServer(CR) Then
                Exit Sub
            End If

            CR.Refresh()
            frmrep.ReportViewer.ReportSource = CR
            strselectformula = "{tblEOD.LocationCode} = " & fncFormatSQLText(gthisLocation)
            strselectformula = strselectformula & " and {tblEOD.EODDate}= Date('" & EODDate & "')"

            CR.DataDefinition.FormulaFields("Name").Text = fncFormatSQLText(strCompanyname)
            CR.DataDefinition.FormulaFields("Address").Text = fncFormatSQLText(strAddress)
            CR.DataDefinition.FormulaFields("TelePhone").Text = fncFormatSQLText(sTelephone)



            frmrep.ReportViewer.SelectionFormula = strselectformula
            frmrep.Show()
            frmrep.ReportViewer.Visible = True
            frmrep.ReportViewer.DisplayToolbar = True
            frmrep.ReportViewer.CloseView(False)
            frmrep.WindowState = FormWindowState.Maximized
            frmrep = Nothing
            frmrep = New frmReports

            ' load day sales
            If UCase(sCompanyShortCode) = "RJ" Then
                sSalesDate = txtYYYY.Text.Trim & txtMM.Text.Trim & txtDD.Text.Trim
                dsSales = objEod.GetSalesHeaderByInvoiceDate(Trim(sSalesDate))
                If dsSales.Tables(0).Rows.Count > 0 Then
                    strpath = Application.StartupPath & "\Reports\rptDaySales.rpt"
                    CR1.Load(strpath)
                    'If Not ReportLogOnToServer(CR) Then
                    '    Exit Sub
                    'End If
                    ReportLogOnToServer(CR1)
                    CR1.Refresh()

                    frmrep1.ReportViewer.ReportSource = CR1
                    strselectformula = " {tblSalesHeader.InvoiceDate} = date('" & EODDate & "')"
                    If UCase(sCompanyShortCode) <> "HN" Then
                        strselectformula = strselectformula & " and {tblSalesHeader.SalesType} <> 'CRRETN '"
                        strselectformula = strselectformula & " and {tblSalesHeader.SalesType} <> 'CSRETN '"
                    End If
                    CR1.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(strCompanyname)
                    CR1.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(strAddress)
                    CR1.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(sTelephone)


                    frmrep1.ReportViewer.SelectionFormula = strselectformula
                    frmrep1.Show()
                    frmrep1.ReportViewer.Visible = True
                    frmrep1.ReportViewer.DisplayToolbar = True
                    frmrep1.ReportViewer.CloseView(False)
                    frmrep1.WindowState = FormWindowState.Maximized
                    frmrep1 = Nothing
                    frmrep1 = New frmReports
                Else
                    strselectformula = ""
                    strpath = Application.StartupPath & "\Reports\rptDaySales_RJ.rpt"
                    CR1.Load(strpath)
                    'If Not ReportLogOnToServer(CR) Then
                    '    Exit Sub
                    'End If
                    ReportLogOnToServer(CR1)
                    CR1.Refresh()

                    frmrep1.ReportViewer.ReportSource = CR1
                    '    strselectformula = " {tblSalesHeader.InvoiceDate} = date('" & EODDate & "')"
                    ''''If UCase(sCompanyShortCode) <> "HN" Then
                    ''''    strselectformula = strselectformula & " and {tblSalesHeader.SalesType} <> 'CRRETN '"
                    ''''    strselectformula = strselectformula & " and {tblSalesHeader.SalesType} <> 'CSRETN '"
                    ''''End If
                    strselectformula = " {tblEOD.EodDate} = date('" & EODDate & "')"
                    CR1.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(strCompanyname)
                    CR1.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(strAddress)
                    CR1.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(sTelephone)
                    '  CR1.DataDefinition.FormulaFields("InvoiceDate").Text = fncFormatSQLText(sSalesDate)


                    frmrep1.ReportViewer.SelectionFormula = strselectformula
                    frmrep1.Show()
                    frmrep1.ReportViewer.Visible = True
                    frmrep1.ReportViewer.DisplayToolbar = True
                    frmrep1.ReportViewer.CloseView(False)
                    frmrep1.WindowState = FormWindowState.Maximized
                    frmrep1 = Nothing
                    frmrep1 = New frmReports
                End If
            Else
                If UCase(sCompanyShortCode) = "AS" Then
                    strpath = Application.StartupPath & "\Reports\rptDaySales_AS.rpt"
                ElseIf UCase(sCompanyShortCode) = "HN" Then
                    strpath = Application.StartupPath & "\Reports\rptDaySales_HN_SG.rpt"
                ElseIf UCase(sCompanyShortCode) = "MJ" Then
                    strpath = Application.StartupPath & "\Reports\rptDaySales_MJ.rpt"
                ElseIf UCase(sCompanyShortCode) = "SSJ" Then
                    strpath = Application.StartupPath & "\Reports\rptDaySales_SSJ.rpt"
                Else
                    strpath = Application.StartupPath & "\Reports\rptDaySales.rpt"
                End If
                CR1.Load(strpath)
                'If Not ReportLogOnToServer(CR) Then
                '    Exit Sub
                'End If
                ReportLogOnToServer(CR1)
                CR1.Refresh()

                frmrep1.ReportViewer.ReportSource = CR1
                strselectformula = " {tblSalesHeader.InvoiceDate} = date('" & EODDate & "')"
                If UCase(sCompanyShortCode) <> "HN" Then
                    strselectformula = strselectformula & " and {tblSalesHeader.SalesType} <> 'CRRETN '"
                    strselectformula = strselectformula & " and {tblSalesHeader.SalesType} <> 'CSRETN '"
                End If
                CR1.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(strCompanyname)
                CR1.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(strAddress)
                CR1.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(sTelephone)


                frmrep1.ReportViewer.SelectionFormula = strselectformula
                frmrep1.Show()
                frmrep1.ReportViewer.Visible = True
                frmrep1.ReportViewer.DisplayToolbar = True
                frmrep1.ReportViewer.CloseView(False)
                frmrep1.WindowState = FormWindowState.Maximized
                frmrep1 = Nothing
                frmrep1 = New frmReports

            End If


            ' load sales detail journal    
            If ChkSalesDetailJournal.Checked Then
                If UCase(sCompanyShortCode) = "HN" And (UCase(Objuser.CompanyName)) = "JEWEL PALACE PTE LTD" Then
                    strpath = Application.StartupPath & "\Reports\rptSalesJournalDetail_JP.rpt"
                Else
                    strpath = Application.StartupPath & "\Reports\rptSalesJournalDetail.rpt"
                End If

                CR.Load(strpath)
                'If Not ReportLogOnToServer(CR) Then
                '    Exit Sub
                'End If
                ReportLogOnToServer(CR)
                CR.Refresh()
                frmrep.ReportViewer.ReportSource = CR
                sReportTitle = "Sales Journal Detail For  " & txtDD.Text.Trim & "/" & txtMM.Text.Trim & "/" & txtYYYY.Text.Trim
                strselectformula = " {tblSalesHeader.InvoiceDate} = date('" & EODDate & "')"
                If UCase(sCompanyShortCode) <> "HN" Then
                    strselectformula = strselectformula & " and {tblSalesHeader.TotalValue} > 0"
                End If
                CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(strCompanyname)
                CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(strAddress)
                CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(sTelephone)
                CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText(sReportTitle)
                frmrep.ReportViewer.SelectionFormula = strselectformula
                frmrep.Show()
                frmrep.ReportViewer.Visible = True
                frmrep.ReportViewer.DisplayToolbar = True
                frmrep.ReportViewer.CloseView(False)
                frmrep.WindowState = FormWindowState.Maximized
                frmrep = Nothing
                frmrep = New frmReports
            End If

            strpath = Application.StartupPath & "\Reports\rptInstallmentSummary.rpt"
            CR.Load(strpath)
            'If Not ReportLogOnToServer(CR) Then
            '    Exit Sub
            'End If
            ReportLogOnToServer(CR)
            CR.Refresh()
            frmrep.ReportViewer.ReportSource = CR
            ' strselectformula = " {tblInstallmentHeader.InvoiceDate} = date('" & EODDate & "')"
            strselectformula = " {tblInstallmentHeader.InvoiceDate} >=date(" & EODDate & ") and {tblInstallmentHeader.InvoiceDate} <= date(" & EODDate & ")"

            'If Trim(txtfrAgreementNo.Text) <> "" And Trim(txtToAgreementNo.Text) <> "" Then
            '    sSql = sSql & " and {tblInstallmentHeader.InsAgreementNo}>= " & fncFormatSQLText(txtfrAgreementNo.Text) & " and {tblInstallmentHeader.InsAgreementNo}<= " & fncFormatSQLText(txtToAgreementNo.Text)
            '    'sSql = sSql & " and {vwInstallmentSummary.InsAgreementNo}>= " & fncFormatSQLText(txtfrAgreementNo.Text) & " and {vwInstallmentSummary.InsAgreementNo}<= " & fncFormatSQLText(txtToAgreementNo.Text)
            'End If
            sReportTitle = "Installment Summary Report "
            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(strCompanyname)
            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(strAddress)
            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(sTelephone)
            CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText(sReportTitle)
            frmrep.ReportViewer.SelectionFormula = strselectformula
            frmrep.Show()
            frmrep.ReportViewer.Visible = True
            frmrep.ReportViewer.DisplayToolbar = True
            frmrep.ReportViewer.CloseView(False)
            frmrep.WindowState = FormWindowState.Maximized
            frmrep = Nothing
            frmrep = New frmReports
                ' load salesman wise journal    
                If ChkSalesmanWiseJournal.Checked Then
                    strpath = Application.StartupPath & "\Reports\rptEODSaleManAnalysis.rpt"
                    CR.Load(strpath)
                    'If Not ReportLogOnToServer(CR) Then
                    '    Exit Sub
                    'End If
                    ReportLogOnToServer(CR)
                    CR.Refresh()
                    frmrep.ReportViewer.ReportSource = CR
                    sReportTitle = "Sales Executive Report For  " & txtDD.Text.Trim & "/" & txtMM.Text.Trim & "/" & txtYYYY.Text.Trim
                    strselectformula = " {tblSalesHeader.InvoiceDate} = date('" & EODDate & "')"
                    If UCase(sCompanyShortCode) <> "HN" Then
                        strselectformula = strselectformula & " and {tblSalesHeader.TotalValue} > 0"
                    End If
                    CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(strCompanyname)
                    CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(strAddress)
                    CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(sTelephone)
                    CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText(sReportTitle)
                    frmrep.ReportViewer.SelectionFormula = strselectformula
                    frmrep.Show()
                    frmrep.ReportViewer.Visible = True
                    frmrep.ReportViewer.DisplayToolbar = True
                    frmrep.ReportViewer.CloseView(False)
                    frmrep.WindowState = FormWindowState.Maximized
                    frmrep = Nothing
                    frmrep = New frmReports
                End If
                'terminal wise sales
                If ChkTerminalWiseSales.Checked Then
                    strpath = Application.StartupPath & "\Reports\rptTerminalSalesNew.rpt"
                    CR.Load(strpath)
                    'If Not ReportLogOnToServer(CR) Then
                    '    Exit Sub
                    'End If
                    ReportLogOnToServer(CR)
                    CR.Refresh()
                    frmrep.ReportViewer.ReportSource = CR
                    strselectformula = " {tblPaymodeDetail.InvoiceDate} = date('" & EODDate & "')"
                    CR.DataDefinition.FormulaFields("Name").Text = fncFormatSQLText(strCompanyname)
                    CR.DataDefinition.FormulaFields("Address").Text = fncFormatSQLText(strAddress)
                    CR.DataDefinition.FormulaFields("TelePhone").Text = fncFormatSQLText(sTelephone)
                    frmrep.ReportViewer.SelectionFormula = strselectformula
                    frmrep.Show()
                    frmrep.ReportViewer.Visible = True
                    frmrep.ReportViewer.DisplayToolbar = True
                    frmrep.ReportViewer.CloseView(False)
                    frmrep.WindowState = FormWindowState.Maximized
                    frmrep = Nothing
                    frmrep = New frmReports
                End If
                'load sales refund journal
                If chkRefundJournal.Checked Then
                    strpath = Application.StartupPath & "\Reports\rptSalesReturnJournalDetail.rpt"
                    CR.Load(strpath)
                    'If Not ReportLogOnToServer(CR) Then
                    '    Exit Sub
                    'End If
                    ReportLogOnToServer(CR)
                    CR.Refresh()
                    frmrep.ReportViewer.ReportSource = CR
                    strselectformula = " {tblSalesReturnHeader.ModifyDate} = date('" & EODDate & "')"
                    CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(strCompanyname)
                    CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(strAddress)
                    CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(sTelephone)
                    frmrep.ReportViewer.SelectionFormula = strselectformula
                    frmrep.Show()
                    frmrep.ReportViewer.Visible = True
                    frmrep.ReportViewer.DisplayToolbar = True
                    frmrep.ReportViewer.CloseView(False)
                    frmrep.WindowState = FormWindowState.Maximized
                    frmrep = Nothing
                    frmrep = New frmReports
                End If
                'Trade in Journal
                If UCase(sCompanyShortCode) = "HN" Then
                    If chkTradeinJournal.Checked Then
                        strpath = Application.StartupPath & "\Reports\TradeInSalesSummary.rpt"
                        CR.Load(strpath)
                        'If Not ReportLogOnToServer(CR) Then
                        '    Exit Sub
                        'End If
                        strselectformula = " {tblSalesHeader.InvoiceDate} >=date(" & EODDate & ") and {tblSalesHeader.InvoiceDate} <= date(" & EODDate & ")"
                        strselectformula = strselectformula & " and {tblTradeInHeader.SoldFlag} <> 'Y'"
                        ReportLogOnToServer(CR)
                        CR.Refresh()
                        frmrep.ReportViewer.ReportSource = CR
                        strselectformula = strselectformula & " and {tblSalesHeader.LocationCode} = " & fncFormatSQLText(gthisLocation)
                        CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(strCompanyname)
                        CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(strAddress)
                        CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(sTelephone)
                        CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText(sTradeInReportTitle)
                        frmrep.ReportViewer.SelectionFormula = strselectformula
                        frmrep.Show()
                        frmrep.ReportViewer.Visible = True
                        frmrep.ReportViewer.DisplayToolbar = True
                        frmrep.ReportViewer.CloseView(False)
                        frmrep.WindowState = FormWindowState.Maximized
                        frmrep = Nothing
                        frmrep = New frmReports
                    End If
                Else
                    If chkTradeinJournal.Checked Then
                        strpath = Application.StartupPath & "\Reports\rptEODTradeIn.rpt"
                        CR.Load(strpath)
                        'If Not ReportLogOnToServer(CR) Then
                        '    Exit Sub
                        'End If

                        ReportLogOnToServer(CR)
                        CR.Refresh()
                        frmrep.ReportViewer.ReportSource = CR
                        strselectformula = " {tblEOD.LocationCode} = " & fncFormatSQLText(gthisLocation)
                        CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(strCompanyname)
                        CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(strAddress)
                        CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(sTelephone)
                        frmrep.ReportViewer.SelectionFormula = strselectformula
                        frmrep.Show()
                        frmrep.ReportViewer.Visible = True
                        frmrep.ReportViewer.DisplayToolbar = True
                        frmrep.ReportViewer.CloseView(False)
                        frmrep.WindowState = FormWindowState.Maximized
                        frmrep = Nothing
                        frmrep = New frmReports
                    End If
                End If
                'Installment journal
                If chkInstallmentJournal.Checked Then
                    strpath = Application.StartupPath & "\Reports\InstallmentSummaryRpt.rpt"
                    CR.Load(strpath)
                    'If Not ReportLogOnToServer(CR) Then
                    '    Exit Sub
                    'End If
                    ReportLogOnToServer(CR)
                    CR.Refresh()
                    frmrep.ReportViewer.ReportSource = CR
                    sReportTitle = "Installment Summary"
                    strselectformula = " {tblSalesHeader.InvoiceDate} =date('" & EODDate & "')"
                    CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(strCompanyname)
                    CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(strAddress)
                    CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(sTelephone)
                    CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText(sReportTitle)
                    frmrep.ReportViewer.SelectionFormula = strselectformula
                    frmrep.Show()
                    frmrep.ReportViewer.Visible = True
                    frmrep.ReportViewer.DisplayToolbar = True
                    frmrep.ReportViewer.CloseView(False)
                    frmrep.WindowState = FormWindowState.Maximized
                    frmrep = Nothing
                    frmrep = New frmReports
                End If
                'closing stock pcs by category
                If ChkCSRPbyCategory.Checked Then
                    Dim sExecsql As String
                    sExecSql = " EXEC sp_RptEODCategory "
                    sExecSql = sExecSql & fncFormatSQLText(Trim$(sLocationCode)) & ", "
                    sExecsql = sExecsql & fncFormatSQLDate(EODDate) & ", "
                    sExecsql = sExecsql & fncFormatSQLText(Trim$(sTerminalCode))
                    dm.ProjExecNonQry(sExecsql)

                    strpath = Application.StartupPath & "\Reports\rptEODCategoryStock.rpt"
                    CR.Load(strpath)
                    'If Not ReportLogOnToServer(CR) Then
                    '    Exit Sub
                    'End If
                    ReportLogOnToServer(CR)
                    CR.Refresh()
                    frmrep.ReportViewer.ReportSource = CR
                    strselectformula = " {tblRptEODCategory.EODDate} =date('" & EODDate & "')"
                    strselectformula = strselectformula & " and {tblRptEODCategory.ComputerName} = " & fncFormatSQLText(sTerminalCode)
                    CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(strCompanyname)
                    CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(strAddress)
                    CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(sTelephone)
                    frmrep.ReportViewer.SelectionFormula = strselectformula
                    frmrep.Show()
                    frmrep.ReportViewer.Visible = True
                    frmrep.ReportViewer.DisplayToolbar = True
                    frmrep.ReportViewer.CloseView(False)
                    frmrep.WindowState = FormWindowState.Maximized
                    frmrep = Nothing
                    frmrep = New frmReports
                End If
                'closing stock Weight by category
                If chkCSRWbyCategory.Checked Then
                    Dim sExecsql As String
                    sExecSql = " EXEC sp_RptEODCategory "
                    sExecSql = sExecSql & fncFormatSQLText(Trim$(sLocationCode)) & ", "
                    sExecsql = sExecsql & fncFormatSQLDate(EODDate) & ", "
                    sExecsql = sExecsql & fncFormatSQLText(Trim$(sTerminalCode))
                    dm.ProjExecNonQry(sExecsql)

                    strpath = Application.StartupPath & "\Reports\rptEODCategoryWeight.rpt"
                    CR.Load(strpath)
                    'If Not ReportLogOnToServer(CR) Then
                    '    Exit Sub
                    'End If
                    ReportLogOnToServer(CR)
                    CR.Refresh()
                    frmrep.ReportViewer.ReportSource = CR
                    strselectformula = " {tblRptEODCategory.EODDate} =date('" & EODDate & "')"
                    strselectformula = strselectformula & " and {tblRptEODCategory.ComputerName} = " & fncFormatSQLText(sTerminalCode)
                    CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(strCompanyname)
                    CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(strAddress)
                    CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(sTelephone)
                    frmrep.ReportViewer.SelectionFormula = strselectformula
                    frmrep.Show()
                    frmrep.ReportViewer.Visible = True
                    frmrep.ReportViewer.DisplayToolbar = True
                    frmrep.ReportViewer.CloseView(False)
                    frmrep.WindowState = FormWindowState.Maximized
                    frmrep = Nothing
                    frmrep = New frmReports
                End If
                'closing stock pcs by tray
                If chkCSRPbyTray.Checked Then
                    Dim sExecsql As String
                    sExecSql = " EXEC sp_RptEODTray "
                    sExecSql = sExecSql & fncFormatSQLText(Trim$(sLocationCode)) & ", "
                    sExecsql = sExecsql & fncFormatSQLDate(EODDate) & ", "
                    sExecsql = sExecsql & fncFormatSQLText(Trim$(sTerminalCode))
                    dm.ProjExecNonQry(sExecsql)

                    strpath = Application.StartupPath & "\Reports\rptEODTrayStock.rpt"
                    CR.Load(strpath)
                    'If Not ReportLogOnToServer(CR) Then
                    '    Exit Sub
                    'End If
                    ReportLogOnToServer(CR)
                    CR.Refresh()
                    frmrep.ReportViewer.ReportSource = CR
                    strselectformula = " {tblRptEODTray.EODDate} =date('" & EODDate & "')"
                    strselectformula = strselectformula & " and {tblRptEODTray.ComputerName} = " & fncFormatSQLText(sTerminalCode)
                    CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(strCompanyname)
                    CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(strAddress)
                    CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(sTelephone)
                    frmrep.ReportViewer.SelectionFormula = strselectformula
                    frmrep.Show()
                    frmrep.ReportViewer.Visible = True
                    frmrep.ReportViewer.DisplayToolbar = True
                    frmrep.ReportViewer.CloseView(False)
                    frmrep.WindowState = FormWindowState.Maximized
                    frmrep = Nothing
                    frmrep = New frmReports
                End If
                'Below cost Report
                If chkBelowCostSales.Checked Then

                    strpath = Application.StartupPath & "\Reports\rptSalesBelowCost.rpt"
                    'If Not ReportLogOnToServer(CR) Then
                    '    Exit Sub
                    'End If
                    ReportLogOnToServer(CR)
                    CR.Refresh()
                    frmrep.ReportViewer.ReportSource = CR
                    strselectformula = " {tblSalesHeader.InvoiceDate} = date('" & EODDate & "')"
                    strselectformula = strselectformula & " and {tblSalesHeader.TotalValue} > 0"
                    sReportTitle = "Below cost sales report"
                    CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(strCompanyname)
                    CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(strAddress)
                    CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(sTelephone)
                    CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText(sReportTitle)
                    frmrep.ReportViewer.SelectionFormula = strselectformula
                    frmrep.Show()
                    frmrep.ReportViewer.Visible = True
                    frmrep.ReportViewer.DisplayToolbar = True
                    frmrep.ReportViewer.CloseView(False)
                    frmrep.WindowState = FormWindowState.Maximized
                    frmrep = Nothing
                    frmrep = New frmReports
                End If
                'Closed bills listing
                If chkClosedBillsListing.Checked Then
                    strpath = Application.StartupPath & "\Reports\rptBillClosed.rpt"
                    CR.Load(strpath)
                    'If Not ReportLogOnToServer(CR) Then
                    '    Exit Sub
                    'End If
                    ReportLogOnToServer(CR)
                    CR.Refresh()
                    frmrep.ReportViewer.ReportSource = CR
                    strselectformula = "{tblJobsheetHeader.BillClose} = 'Y'"
                    strselectformula = strselectformula & " and {tblJobsheetHeader.JobSheetDate} = date('" & EODDate & "')"
                    CR.DataDefinition.FormulaFields("Name").Text = fncFormatSQLText(strCompanyname)
                    CR.DataDefinition.FormulaFields("Address").Text = fncFormatSQLText(strAddress)
                    CR.DataDefinition.FormulaFields("TelePhone").Text = fncFormatSQLText(sTelephone)
                    frmrep.ReportViewer.SelectionFormula = strselectformula
                    frmrep.Show()
                    frmrep.ReportViewer.Visible = True
                    frmrep.ReportViewer.DisplayToolbar = True
                    frmrep.ReportViewer.CloseView(False)
                    frmrep.WindowState = FormWindowState.Maximized
                    frmrep = Nothing
                    frmrep = New frmReports
                End If
                'Purchase return Detail
                If chkPurchaseReturnJournal.Checked Then
                    strpath = Application.StartupPath & "\Reports\rptPurchaseReturnDetail.rpt"
                    CR.Load(strpath)
                    'If Not ReportLogOnToServer(CR) Then
                    '    Exit Sub
                    'End If
                    ReportLogOnToServer(CR)
                    CR.Refresh()
                    frmrep.ReportViewer.ReportSource = CR
                    strselectformula = "{tblPurchaseReturnHeader.ReturnDate} = date('" & EODDate & "')"
                    CR.DataDefinition.FormulaFields("Name").Text = fncFormatSQLText(strCompanyname)
                    CR.DataDefinition.FormulaFields("Address").Text = fncFormatSQLText(strAddress)
                    CR.DataDefinition.FormulaFields("TelePhone").Text = fncFormatSQLText(sTelephone)
                    frmrep.ReportViewer.SelectionFormula = strselectformula
                    frmrep.Show()
                    frmrep.ReportViewer.Visible = True
                    frmrep.ReportViewer.DisplayToolbar = True
                    frmrep.ReportViewer.CloseView(False)
                    frmrep.WindowState = FormWindowState.Maximized
                    frmrep = Nothing
                    frmrep = New frmReports
                End If
                'goldbar Purchase
                If chkGoldBarPurchase.Checked Then
                    strpath = Application.StartupPath & "\Reports\rptGoldBarPurchaseSummary.rpt"
                    CR.Load(strpath)
                    'If Not ReportLogOnToServer(CR) Then
                    '    Exit Sub
                    'End If
                    ReportLogOnToServer(CR)
                    CR.Refresh()
                    frmrep.ReportViewer.ReportSource = CR
                    strselectformula = "{tblGoldBarPurchaseHeader.ModifyDate} = date('" & EODDate & "')"
                    CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(strCompanyname)
                    CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(strAddress)
                    CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(sTelephone)
                    frmrep.ReportViewer.SelectionFormula = strselectformula
                    frmrep.Show()
                    frmrep.ReportViewer.Visible = True
                    frmrep.ReportViewer.DisplayToolbar = True
                    frmrep.ReportViewer.CloseView(False)
                    frmrep.WindowState = FormWindowState.Maximized
                    frmrep = Nothing
                    frmrep = New frmReports
                End If
                'Misc. journal
                If chkMiscellaneousJournal.Checked Then
                    strpath = Application.StartupPath & "\Reports\rptMisc.rpt"
                    CR.Load(strpath)
                    'If Not ReportLogOnToServer(CR) Then
                    '    Exit Sub
                    'End If
                    ReportLogOnToServer(CR)
                    CR.Refresh()
                    frmrep.ReportViewer.ReportSource = CR
                    strselectformula = "{tblMisc.MiscDate} = date('" & EODDate & "')"
                    sReportTitle = "Miscellaneous Report For " & Trim(EODDate)
                    CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(strCompanyname)
                    CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(strAddress)
                    CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(sTelephone)
                    CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText(sReportTitle)
                    frmrep.ReportViewer.SelectionFormula = strselectformula
                    frmrep.Show()
                    frmrep.ReportViewer.Visible = True
                    frmrep.ReportViewer.DisplayToolbar = True
                    frmrep.ReportViewer.CloseView(False)
                    frmrep.WindowState = FormWindowState.Maximized
                    frmrep = Nothing
                    frmrep = New frmReports
                End If
                'Sales man wise Profit
                If chkNormalSalesProfit.Checked Then
                    ''HAJ Change report For AS on 29-05-2007
                    If UCase(sCompanyShortCode) = "AS" Then
                        strpath = Application.StartupPath & "\Reports\rptProfitByDate.rpt"
                        CR.Load(strpath)
                        'If Not ReportLogOnToServer(CR) Then
                        '    Exit Sub
                        'End If
                        ReportLogOnToServer(CR)
                        CR.Refresh()
                        frmrep.ReportViewer.ReportSource = CR
                        strselectformula = "{tblSalesHeader.InvoiceDate} = date('" & EODDate & "')"
                        strselectformula = strselectformula & " and {tblSalesHeader.TotalValue} > 0"
                        strselectformula = strselectformula & " and {tblSalesHeader.GSTType} <> 'Z'"
                        'strselectformula = strselectformula & " and {tblClass.SalesType}<>'P'"
                        'strselectformula = strselectformula & " and {tblClass.PurchaseType}<>'P'"
                        sReportTitle = "Profit Report "
                        sReportTitle = sReportTitle & " For " & Format(Trim(EODDate), "dd/MM/yyyy")
                        CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(strCompanyname)
                        CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(strAddress)
                        CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(sTelephone)
                        CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText(sReportTitle)
                        frmrep.ReportViewer.SelectionFormula = strselectformula
                        frmrep.Show()
                        frmrep.ReportViewer.Visible = True
                        frmrep.ReportViewer.DisplayToolbar = True
                        frmrep.ReportViewer.CloseView(False)
                        frmrep.WindowState = FormWindowState.Maximized
                        frmrep = Nothing
                        frmrep = New frmReports
                    Else
                        strpath = Application.StartupPath & "\Reports\rptNormalSalesProfitSummary.rpt"
                        CR.Load(strpath)
                        'If Not ReportLogOnToServer(CR) Then
                        '    Exit Sub
                        'End If
                        ReportLogOnToServer(CR)
                        CR.Refresh()
                        frmrep.ReportViewer.ReportSource = CR
                        strselectformula = "{tblSalesHeader.InvoiceDate} = date('" & EODDate & "')"
                        strselectformula = strselectformula & " and {tblSalesHeader.TotalValue} > 0"
                        strselectformula = strselectformula & " and {tblSalesHeader.GSTType} <> 'Z'"
                        strselectformula = strselectformula & " and {tblClass.SalesType}<>'P'"
                        strselectformula = strselectformula & " and {tblClass.PurchaseType}<>'P'"
                        sReportTitle = "Sales Executive Profit Report "
                        sReportTitle = sReportTitle & " For " & Trim(EODDate)
                        CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(strCompanyname)
                        CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(strAddress)
                        CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(sTelephone)
                        CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText(sReportTitle)
                        frmrep.ReportViewer.SelectionFormula = strselectformula
                        frmrep.Show()
                        frmrep.ReportViewer.Visible = True
                        frmrep.ReportViewer.DisplayToolbar = True
                        frmrep.ReportViewer.CloseView(False)
                        frmrep.WindowState = FormWindowState.Maximized
                        frmrep = Nothing
                        frmrep = New frmReports
                    End If
                End If
                Me.Cursor.Current = System.Windows.Forms.Cursors.Default
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subFillReports()
        Dim Ds As DataSet
        Dim sQry As String
        Dim i As Integer
        Try
            sQry = " SELECT * FROM tblEODReports order by CAST(reportname AS int) "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "EOD", "EOD")
            If Ds.Tables("EOD").Rows.Count > 0 Then
                For i = 0 To Ds.Tables("EOD").Rows.Count - 1
                    Select Case Trim(Ds.Tables("EOD").Rows(i)("ReportName"))
                        Case "0"
                            If Trim(Ds.Tables("EOD").Rows(i)("PrintYesorNo")) = "Y" Then
                                ChkSalesDetailJournal.Checked = True
                            Else
                                ChkSalesDetailJournal.Checked = False
                            End If
                        Case "1"
                            If Trim(Ds.Tables("EOD").Rows(i)("PrintYesorNo")) = "Y" Then
                                ChkSalesmanWiseJournal.Checked = True
                            Else
                                ChkSalesmanWiseJournal.Checked = False
                            End If
                        Case "2"
                            If Trim(Ds.Tables("EOD").Rows(i)("PrintYesorNo")) = "Y" Then
                                ChkTerminalWiseSales.Checked = True
                            Else
                                ChkTerminalWiseSales.Checked = False
                            End If
                        Case "3"
                            If Trim(Ds.Tables("EOD").Rows(i)("PrintYesorNo")) = "Y" Then
                                chkRefundJournal.Checked = True
                            Else
                                chkRefundJournal.Checked = False
                            End If
                        Case "4"
                            If Trim(Ds.Tables("EOD").Rows(i)("PrintYesorNo")) = "Y" Then
                                chkTradeinJournal.Checked = True
                            Else
                                chkTradeinJournal.Checked = False
                            End If
                        Case "5"
                            If Trim(Ds.Tables("EOD").Rows(i)("PrintYesorNo")) = "Y" Then
                                chkInstallmentJournal.Checked = True
                            Else
                                chkInstallmentJournal.Checked = False
                            End If
                        Case "6"
                            If Trim(Ds.Tables("EOD").Rows(i)("PrintYesorNo")) = "Y" Then
                                ChkCSRPbyCategory.Checked = True
                            Else
                                ChkCSRPbyCategory.Checked = False
                            End If
                        Case "7"
                            If Trim(Ds.Tables("EOD").Rows(i)("PrintYesorNo")) = "Y" Then
                                chkCSRWbyCategory.Checked = True
                            Else
                                chkCSRWbyCategory.Checked = False
                            End If
                        Case "8"
                            If Trim(Ds.Tables("EOD").Rows(i)("PrintYesorNo")) = "Y" Then
                                chkCSRPbyTray.Checked = True
                            Else
                                chkCSRPbyTray.Checked = False
                            End If
                        Case "9"
                            If Trim(Ds.Tables("EOD").Rows(i)("PrintYesorNo")) = "Y" Then
                                chkBelowCostSales.Checked = True
                            Else
                                chkBelowCostSales.Checked = False
                            End If
                        Case "10"
                            If Trim(Ds.Tables("EOD").Rows(i)("PrintYesorNo")) = "Y" Then
                                chkClosedBillsListing.Checked = True
                            Else
                                chkClosedBillsListing.Checked = False
                            End If
                        Case "11"
                            If Trim(Ds.Tables("EOD").Rows(i)("PrintYesorNo")) = "Y" Then
                                chkPurchaseReturnJournal.Checked = True
                            Else
                                chkPurchaseReturnJournal.Checked = False
                            End If
                        Case "12"
                            If Trim(Ds.Tables("EOD").Rows(i)("PrintYesorNo")) = "Y" Then
                                chkGoldBarPurchase.Checked = True
                            Else
                                chkGoldBarPurchase.Checked = False
                            End If
                        Case "13"
                            If Trim(Ds.Tables("EOD").Rows(i)("PrintYesorNo")) = "Y" Then
                                chkMiscellaneousJournal.Checked = True
                            Else
                                chkMiscellaneousJournal.Checked = False
                            End If
                        Case "14"
                            If Trim(Ds.Tables("EOD").Rows(i)("PrintYesorNo")) = "Y" Then
                                chkNormalSalesProfit.Checked = True
                            Else
                                chkNormalSalesProfit.Checked = False
                            End If
                    End Select
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

End Class
