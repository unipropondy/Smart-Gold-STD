Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmJewelBarcode
    Inherits System.Windows.Forms.Form
    Dim masPriceCode(9) As String
    Dim sSupplierCode As String
    Dim sPurchaseDate As String
    Dim dm As New DataManager
    Dim sPrinterName As String

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
    Friend WithEvents txtCompany As System.Windows.Forms.TextBox
    Friend WithEvents lblLocationCode As System.Windows.Forms.Label
    Friend WithEvents txtBarcode As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtInventoryCode As AxEditLib.AxfpText
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtGoldLoss As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents txtSMC As System.Windows.Forms.TextBox
    Friend WithEvents txtClass As System.Windows.Forms.TextBox
    Friend WithEvents txtNoofLabels As System.Windows.Forms.TextBox
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents txtTrayID As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmJewelBarcode))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtTrayID = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtNoofLabels = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtClass = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtSMC = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtGoldLoss = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtWeight = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtDescription = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtInventoryCode = New AxEditLib.AxfpText
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtBarcode = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtCompany = New System.Windows.Forms.TextBox
        Me.lblLocationCode = New System.Windows.Forms.Label
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnPrint = New System.Windows.Forms.Button
        Me.btnNew = New System.Windows.Forms.Button
        Me.pic = New System.Windows.Forms.PictureBox
        Me.GroupBox1.SuspendLayout()
        CType(Me.txtInventoryCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtTrayID)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtNoofLabels)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtClass)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtSMC)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtGoldLoss)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtWeight)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtDescription)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtInventoryCode)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtBarcode)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtCompany)
        Me.GroupBox1.Controls.Add(Me.lblLocationCode)
        Me.GroupBox1.Location = New System.Drawing.Point(24, 80)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(416, 320)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'txtTrayID
        '
        Me.txtTrayID.AcceptsTab = True
        Me.txtTrayID.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtTrayID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTrayID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTrayID.Enabled = False
        Me.txtTrayID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTrayID.ForeColor = System.Drawing.Color.Black
        Me.txtTrayID.Location = New System.Drawing.Point(144, 256)
        Me.txtTrayID.MaxLength = 4
        Me.txtTrayID.Name = "txtTrayID"
        Me.txtTrayID.Size = New System.Drawing.Size(168, 20)
        Me.txtTrayID.TabIndex = 476199
        Me.txtTrayID.Tag = "NE"
        Me.txtTrayID.Text = ""
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(32, 256)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(88, 18)
        Me.Label9.TabIndex = 476200
        Me.Label9.Text = "Total Cost"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtNoofLabels
        '
        Me.txtNoofLabels.AcceptsTab = True
        Me.txtNoofLabels.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtNoofLabels.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNoofLabels.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNoofLabels.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoofLabels.ForeColor = System.Drawing.Color.Black
        Me.txtNoofLabels.Location = New System.Drawing.Point(144, 288)
        Me.txtNoofLabels.MaxLength = 4
        Me.txtNoofLabels.Name = "txtNoofLabels"
        Me.txtNoofLabels.Size = New System.Drawing.Size(168, 20)
        Me.txtNoofLabels.TabIndex = 9
        Me.txtNoofLabels.Tag = "NE"
        Me.txtNoofLabels.Text = ""
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(32, 288)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(88, 18)
        Me.Label8.TabIndex = 476198
        Me.Label8.Text = "No of Labels"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtClass
        '
        Me.txtClass.AcceptsTab = True
        Me.txtClass.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtClass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtClass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtClass.Enabled = False
        Me.txtClass.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClass.ForeColor = System.Drawing.Color.Black
        Me.txtClass.Location = New System.Drawing.Point(144, 224)
        Me.txtClass.MaxLength = 4
        Me.txtClass.Name = "txtClass"
        Me.txtClass.Size = New System.Drawing.Size(168, 20)
        Me.txtClass.TabIndex = 8
        Me.txtClass.Tag = "NE"
        Me.txtClass.Text = ""
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(32, 227)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(48, 18)
        Me.Label7.TabIndex = 476196
        Me.Label7.Text = "Class"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSMC
        '
        Me.txtSMC.AcceptsTab = True
        Me.txtSMC.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtSMC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSMC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSMC.Enabled = False
        Me.txtSMC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMC.ForeColor = System.Drawing.Color.Black
        Me.txtSMC.Location = New System.Drawing.Point(144, 196)
        Me.txtSMC.MaxLength = 4
        Me.txtSMC.Name = "txtSMC"
        Me.txtSMC.Size = New System.Drawing.Size(168, 20)
        Me.txtSMC.TabIndex = 7
        Me.txtSMC.Tag = "NE"
        Me.txtSMC.Text = ""
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(32, 198)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 18)
        Me.Label6.TabIndex = 476194
        Me.Label6.Text = "Stone Weight"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtGoldLoss
        '
        Me.txtGoldLoss.AcceptsTab = True
        Me.txtGoldLoss.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtGoldLoss.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldLoss.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtGoldLoss.Enabled = False
        Me.txtGoldLoss.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoldLoss.ForeColor = System.Drawing.Color.Black
        Me.txtGoldLoss.Location = New System.Drawing.Point(144, 168)
        Me.txtGoldLoss.MaxLength = 4
        Me.txtGoldLoss.Name = "txtGoldLoss"
        Me.txtGoldLoss.Size = New System.Drawing.Size(168, 20)
        Me.txtGoldLoss.TabIndex = 6
        Me.txtGoldLoss.Tag = "NE"
        Me.txtGoldLoss.Text = ""
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(32, 171)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(88, 18)
        Me.Label5.TabIndex = 476192
        Me.Label5.Text = "Gold Loss"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtWeight
        '
        Me.txtWeight.AcceptsTab = True
        Me.txtWeight.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWeight.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtWeight.Enabled = False
        Me.txtWeight.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeight.ForeColor = System.Drawing.Color.Black
        Me.txtWeight.Location = New System.Drawing.Point(144, 140)
        Me.txtWeight.MaxLength = 4
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.Size = New System.Drawing.Size(168, 20)
        Me.txtWeight.TabIndex = 5
        Me.txtWeight.Tag = "NE"
        Me.txtWeight.Text = ""
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(32, 143)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 18)
        Me.Label4.TabIndex = 476190
        Me.Label4.Text = "Weight"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDescription
        '
        Me.txtDescription.AcceptsTab = True
        Me.txtDescription.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescription.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDescription.Enabled = False
        Me.txtDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.ForeColor = System.Drawing.Color.Black
        Me.txtDescription.Location = New System.Drawing.Point(144, 112)
        Me.txtDescription.MaxLength = 4
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(264, 20)
        Me.txtDescription.TabIndex = 4
        Me.txtDescription.Tag = "NE"
        Me.txtDescription.Text = ""
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(32, 113)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 18)
        Me.Label3.TabIndex = 476188
        Me.Label3.Text = "Description"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtInventoryCode
        '
        Me.txtInventoryCode.ContainingControl = Me
        Me.txtInventoryCode.Location = New System.Drawing.Point(144, 80)
        Me.txtInventoryCode.Name = "txtInventoryCode"
        Me.txtInventoryCode.OcxState = CType(resources.GetObject("txtInventoryCode.OcxState"), System.Windows.Forms.AxHost.State)
        Me.txtInventoryCode.Size = New System.Drawing.Size(168, 24)
        Me.txtInventoryCode.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(32, 83)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 18)
        Me.Label2.TabIndex = 476185
        Me.Label2.Text = "Inventory Code"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBarcode
        '
        Me.txtBarcode.AcceptsTab = True
        Me.txtBarcode.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBarcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBarcode.Enabled = False
        Me.txtBarcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBarcode.ForeColor = System.Drawing.Color.Black
        Me.txtBarcode.Location = New System.Drawing.Point(144, 52)
        Me.txtBarcode.MaxLength = 4
        Me.txtBarcode.Name = "txtBarcode"
        Me.txtBarcode.Size = New System.Drawing.Size(168, 20)
        Me.txtBarcode.TabIndex = 2
        Me.txtBarcode.Tag = "NE"
        Me.txtBarcode.Text = ""
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(32, 55)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 18)
        Me.Label1.TabIndex = 476184
        Me.Label1.Text = "BarCode"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCompany
        '
        Me.txtCompany.AcceptsTab = True
        Me.txtCompany.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtCompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCompany.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCompany.Enabled = False
        Me.txtCompany.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompany.ForeColor = System.Drawing.Color.Black
        Me.txtCompany.Location = New System.Drawing.Point(144, 24)
        Me.txtCompany.MaxLength = 10
        Me.txtCompany.Name = "txtCompany"
        Me.txtCompany.Size = New System.Drawing.Size(168, 20)
        Me.txtCompany.TabIndex = 1
        Me.txtCompany.Tag = "NE"
        Me.txtCompany.Text = ""
        '
        'lblLocationCode
        '
        Me.lblLocationCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationCode.ForeColor = System.Drawing.Color.Black
        Me.lblLocationCode.Location = New System.Drawing.Point(32, 25)
        Me.lblLocationCode.Name = "lblLocationCode"
        Me.lblLocationCode.Size = New System.Drawing.Size(88, 18)
        Me.lblLocationCode.TabIndex = 476182
        Me.lblLocationCode.Text = "Company"
        Me.lblLocationCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(368, 416)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.TabIndex = 12
        Me.btnClose.Text = "Close(Esc)"
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.Location = New System.Drawing.Point(208, 416)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(76, 23)
        Me.btnPrint.TabIndex = 10
        Me.btnPrint.Text = "Print (F9)"
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(288, 416)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.TabIndex = 11
        Me.btnNew.Text = "New (F6)"
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(24, 0)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(210, 75)
        Me.pic.TabIndex = 54
        Me.pic.TabStop = False
        '
        'frmJewelBarcode
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ClientSize = New System.Drawing.Size(448, 447)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmJewelBarcode"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Jewellary Barcode"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.txtInventoryCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmJewelBarcode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        gsBarCodeFilePath = Application.StartupPath & "\" & gsBarCodeFile & ".txt"
        '''subFillPriceCodeArr()
        If UCase(sCompanyShortCode) = "ES" Then
            txtCompany.Text = "SRI PINANG"
        ElseIf UCase(sCompanyShortCode) = "ML" Then
            txtCompany.Text = "MILLION"
        ElseIf UCase(sCompanyShortCode) = "AS" Then
            txtCompany.Text = "APPOLLO"
        End If
        txtNoofLabels.Text = "1"
    End Sub

    Private Sub subFillPriceCodeArr()
        Try
            Dim sQry As String
            Dim Ds As DataSet
            Dim i As Long
            sQry = "Select PriceCode, PriceValue from tblPriceCode order by PriceValue"
            Ds = dm.getDataSetWithSqlAdapter(sQry, "PriceCode", "PriceCode")
            If Ds.Tables("PriceCode").Rows.Count > 0 Then
                For i = 0 To Ds.Tables("PriceCode").Rows.Count - 1
                    masPriceCode(Val(Ds.Tables("PriceCode").Rows(i)("PriceValue") & "")) = Trim(Ds.Tables("PriceCode").Rows(i)("PriceCode") & "")
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtInventoryCode_ButtonHit(ByVal sender As Object, ByVal e As AxEditLib._DfpText_ButtonHitEvent) Handles txtInventoryCode.ButtonHit
        Try
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblInventory", "L", 4, "Description", "", "InventoryCode", "InventoryCode", 80, "Description", "Description", 180, "SupplierCode", "SupplierCode", 60, "Weight", "Weight", 80)
            frmHelpClass.ShowDialog()
            txtInventoryCode.Text = frmHelpClass.Var1.Trim
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub txtInventoryCode_KeyDownEvent(ByVal sender As Object, ByVal e As AxEditLib._DfpText_KeyDownEvent) Handles txtInventoryCode.KeyDownEvent
        Try
            If e.keyCode = Keys.F1 Then
                Dim frmHelpClass As New frmHelpSearch(0, 0, "tblInventory", "L", 4, "Description", "", "InventoryCode", "InventoryCode", 80, "Description", "Description", 180, "SupplierCode", "SupplierCode", 60, "Weight", "Weight", 80)
                frmHelpClass.ShowDialog()
                txtInventoryCode.Text = frmHelpClass.Var1.Trim
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Public Sub txtInventoryCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInventoryCode.LostFocus
        Try
            If txtInventoryCode.Text.Trim <> "" Then
                subGetInventory()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub subGetInventory()
        Dim DsInv As DataSet
        Dim DsTrade As DataSet
        Dim objInv As New clsInventory
        Dim objTrade As New clsTradeIN
        Try
            DsInv = objInv.GetInventoryDetail(txtInventoryCode.Text.Trim)
            DsTrade = objTrade.getTradeIn(txtInventoryCode.Text.Trim)
            If DsInv.Tables("tblInventory").Rows.Count > 0 Then
                txtDescription.Text = DsInv.Tables("tblInventory").Rows(0)("Description") & ""
                txtWeight.Text = DsInv.Tables("tblInventory").Rows(0)("Weight") & ""
                txtGoldLoss.Text = DsInv.Tables("tblInventory").Rows(0)("GoldLoss") & ""
                If sCompanyShortCode = "AS" Then
                    txtSMC.Text = DsInv.Tables("tblInventory").Rows(0)("SMC") & ""
                Else
                    txtSMC.Text = DsInv.Tables("tblInventory").Rows(0)("StoneWeight") & ""
                End If

                txtClass.Text = DsInv.Tables("tblInventory").Rows(0)("ClassCode") & ""
                txtBarcode.Text = DsInv.Tables("tblInventory").Rows(0)("InventoryCode") & ""
                sSupplierCode = DsInv.Tables("tblInventory").Rows(0)("SupplierCode") & ""
                sPurchaseDate = DsInv.Tables("tblInventory").Rows(0)("PurchaseDate") & ""
                txtTrayID.Text = DsInv.Tables("tblInventory").Rows(0)("SMC") & ""
            Else
                    MessageBox.Show("InventoryCode Not Found", "GoldPOS")
                End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If UCase(sCompanyShortCode) = "AS" Then
            '''            subPritBarcodeReport() ''''CrystalRerpot
            If Len(txtInventoryCode.Text.Trim) = 0 Then
                MsgBox("Please Enter InventoryCode", MsgBoxStyle.Information)
                If txtInventoryCode.Enabled = True Then txtInventoryCode.Focus()
                Exit Sub
            End If
            subPritBarcodeReportLPT()
        ElseIf UCase(sCompanyShortCode) = "ES" Then
            sunPrintBarCode()
        End If
    End Sub

    Private Sub sunPrintBarCode()
        Dim sInvCode, sMC, sTrayID, sClass, sDesc, sWeight, sCompName, sBarcode As String, sSupplier As String
        Dim sQuote As String
        Dim sNoOfLabels As String
        subDeleteBarCodeFile(gsBarCodeFilePath)

        '''''
        '''''Please Do not Change the Order of printing 14.02.2007 Kumar
        subBarCodeDetailsToFile(gsBarCodeFilePath, "I8,1,001") ''''1
        subBarCodeDetailsToFile(gsBarCodeFilePath, "q200") ''''2
        subBarCodeDetailsToFile(gsBarCodeFilePath, "S5") ''''3
        subBarCodeDetailsToFile(gsBarCodeFilePath, "H20") ''''4
        subBarCodeDetailsToFile(gsBarCodeFilePath, "ZB") ''''5
        subBarCodeDetailsToFile(gsBarCodeFilePath, "R0,0") ''''6
        subBarCodeDetailsToFile(gsBarCodeFilePath, "N") ''''7

        sInvCode = "B68,179,0,1,1,3,38,N," & """" & txtInventoryCode.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sInvCode) ''''8
        If Len(sInvCode.Trim) = 0 Then
            MsgBox("Please Enter InventoryCode", MsgBoxStyle.Information)
            If txtInventoryCode.Enabled = True Then txtInventoryCode.Focus()
            Exit Sub
        End If

        sMC = "A17,33,0,2,1,1,N," & """Ct:" & txtSMC.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sMC) ''''9
        If Len(sMC.Trim) = 0 Then
            MsgBox("Please Enter SMC", MsgBoxStyle.Information)
            If txtSMC.Enabled = True Then txtSMC.Focus()
            Exit Sub
        End If

        sTrayID = "A17,93,0,2,1,1,N," & """PRC:" & txtTrayID.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sTrayID) ''''10
        If Len(sTrayID.Trim) = 0 Then
            MsgBox("Please Enter Tray ID", MsgBoxStyle.Information)
            If txtTrayID.Enabled = True Then txtTrayID.Focus()
            Exit Sub
        End If

        sSupplier = "A17,114,0,2,1,1,N," & """Supp:" & sSupplierCode.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sSupplier) ''''9

        sClass = "A17,72,0,2,1,1,N," & """" & txtClass.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sClass) ''''11
        If Len(sClass.Trim) = 0 Then
            MsgBox("Please Enter Class", MsgBoxStyle.Information)
            If txtClass.Enabled = True Then txtClass.Focus()
            Exit Sub
        End If

        sDesc = "A17,53,0,2,1,1,N," & """" & txtDescription.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sDesc) ''''12
        If Len(sDesc.Trim) = 0 Then
            MsgBox("Please Enter Description", MsgBoxStyle.Information)
            If txtDescription.Enabled = True Then txtDescription.Focus()
            Exit Sub
        End If

        sWeight = "A17,14,0,2,1,1,N," & """Gm:" & Format(Val(txtWeight.Text), "##0.00") & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sWeight) ''''13
        If Len(sWeight) = 0 Then
            MsgBox("Please Enter Weight", MsgBoxStyle.Information)
            If txtWeight.Enabled = True Then txtWeight.Focus()
            Exit Sub
        End If
        txtCompany.Text = "SRI PINANG"
        sCompName = "A25,149,0,3,1,1,N," & """" & txtCompany.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sCompName) ''''14
        If Len(sCompName.Trim) = 0 Then
            MsgBox("Please Enter Company Name", MsgBoxStyle.Information)
            If txtCompany.Enabled = True Then txtCompany.Focus()
            Exit Sub
        End If

        sBarcode = "A57,225,0,3,1,1,N," & """" & txtBarcode.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sBarcode) ''''15
        If Len(sBarcode.Trim) = 0 Then
            MsgBox("Please Enter BarCode", MsgBoxStyle.Information)
            If txtBarcode.Enabled = True Then txtBarcode.Focus()
            Exit Sub
        End If
        If txtNoofLabels.Text.Trim = "" Then
            txtNoofLabels.Text = "1"
        End If
        sNoOfLabels = "W1," & txtNoofLabels.Text.Trim & """"
        subBarCodeDetailsToFile(gsBarCodeFilePath, sNoOfLabels) ''''16

        Dim cPrinter As New clsBarcode
        sPrinterName = "POSTEK G-2108"
        cPrinter.SendFileToPrinter(sPrinterName, gsBarCodeFilePath)
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        txtBarcode.Text = ""
        'txtPrice = ""
        txtInventoryCode.Text = ""
        txtDescription.Text = ""
        txtBarcode.Text = ""
        'txtPrice = ""
        txtWeight.Text = ""
        txtGoldLoss.Text = ""
        txtSMC.Text = ""
        txtClass.Text = ""
        'txtRemarks = ""
        txtNoofLabels.Text = "1"
        txtInventoryCode.Focus()
    End Sub

    Private Sub frmJewelBarcode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F9
                btnPrint_Click(sender, e)
            Case Keys.F6
                btnNew_Click(sender, e)
            Case Keys.Escape
                btnClose_Click(sender, e)
            Case Keys.Up
                SendKeys.Send("+{Tab}")
            Case Keys.Down
                SendKeys.Send("{Tab}")
            Case Keys.Enter
                SendKeys.Send("{Tab}")
        End Select
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub subPritBarcodeReport() 'From CrystalReport
        Dim sSql As String
        Dim sCompanyname, sAddress, sPhone, sFax, sReportTitle, sPath As String
        Dim CR As New ReportDocument
        Dim frmRep As New frmReports
        Dim ds As DataSet
        Dim nLabels As Integer
        nLabels = Val(txtNoofLabels.Text)
        If nLabels = 0 Then
            nLabels = 1
        End If
        Try

            sPath = Application.StartupPath & "\Reports\rptBarcode_AS.rpt"

            '======== General ======================

            Me.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            CR.Load(sPath)
            If Not ReportLogOnToServer(CR) Then
                Exit Sub
            End If
            ReportLogOnToServer(CR)
            'CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(sCompanyname)
            'CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(sAddress)
            'CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText(sReportTitle)
            'CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(sPhone)
            'CR.DataDefinition.FormulaFields("CompAddress4").Text = fncFormatSQLText(sFax)
            'CR.DataDefinition.RecordSelectionFormula = sSql

            CR.DataDefinition.FormulaFields("Barcode").Text = fncFormatSQLText("*" & Trim(txtInventoryCode.Text) & "*")
            CR.DataDefinition.FormulaFields("InventoryCode").Text = fncFormatSQLText(Trim(txtInventoryCode.Text))
            CR.DataDefinition.FormulaFields("Description").Text = fncFormatSQLText(Trim(txtDescription.Text))
            CR.DataDefinition.FormulaFields("TotalWeight").Text = "'" & "WT : " & Trim(Format(Val(txtWeight.Text) + Val(txtGoldLoss.Text), "0.00")) & "gm" & "'"
            CR.DataDefinition.FormulaFields("GoldLoss").Text = "'" & Trim(sSupplierCode) & ":" & Trim(Format(CDate(sPurchaseDate), "MM/yy")) & "/" & Format(Val(txtGoldLoss.Text), "#.##") & "" & "" & "'"
            CR.DataDefinition.FormulaFields("Wt").Text = "'" & "WT :" & Trim(Format(txtWeight.Text, "0.00") + Val(txtGoldLoss.Text)) & "gm" & "'"
            CR.DataDefinition.FormulaFields("MC").Text = "'" & "MC : $" & Trim(Format(Val(txtSMC.Text), "0.00")) & "'"
            CR.DataDefinition.FormulaFields("ClassCode").Text = "'" & Trim(txtClass.Text) & "'"



            'CR.Refresh()
            'frmRep.ReportViewer.ReportSource = CR

            '==== Print/ Preview Option========

            'If optPreview.Checked = True Then
            'frmRep.ShowDialog()
            'frmRep.ReportViewer.Visible = True
            'frmRep.ReportViewer.CloseView(False)
            'frmRep.WindowState = FormWindowState.Maximized
            'Else

            CR.PrintToPrinter(nLabels, True, 0, 0)
            'End If
            Exit Sub

        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
            CR.Close()
            CR = Nothing
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub subPritBarcodeReportLPT()
        Dim sInventoryCode, sDescription, sGoldLoss, sWeight, sSMC, sSupplierCodeWithPurchaseDate, sClass As String
        
        subDeleteBarCodeFile(gsBarCodeFilePath)
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SIZE 27.5 mm, 34 mm")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "GAP 3 mm, 0 mm")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SPEED 5")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "DENSITY 7")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "DIRECTION 0,0")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "REFERENCE 0,0")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "OFFSET 0 mm")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SHIFT 0")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SET PEEL OFF")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SET CUTTER OFF")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SET PARTIAL_CUTTER OFF")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "SET TEAR ON")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "CLS")
        subBarCodeDetailsToFile(gsBarCodeFilePath, "CODEPAGE 850")

        ''''Gold Loss
        '''sGoldLoss = IIf(txtGoldLoss.Text.Trim <> "", "Gold Loss : " & txtGoldLoss.Text.Trim, "")
        '''sGoldLoss = "TEXT 193,213," & Chr(34) & "0" & Chr(34) & ",180,8,8," & Chr(34) & sGoldLoss & Chr(34)
        '''subBarCodeDetailsToFile(gsBarCodeFilePath, sGoldLoss)

        'Supplier and Purchase Date
        sSupplierCodeWithPurchaseDate = sSupplierCode.Trim & " " & Format(CDate(sPurchaseDate), "MM/yy")
        sSupplierCodeWithPurchaseDate = "TEXT 193,213," & Chr(34) & "0" & Chr(34) & ",180,8,8," & Chr(34) & sSupplierCodeWithPurchaseDate & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sSupplierCodeWithPurchaseDate)

        'Weight
        sWeight = IIf(txtWeight.Text.Trim <> "", "WT : " & txtWeight.Text.Trim & "gm", "")
        sWeight = "TEXT 193,189," & Chr(34) & "0" & Chr(34) & ",180,11,8," & Chr(34) & sWeight & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sWeight)

        'MC
        sSMC = IIf(txtSMC.Text.Trim <> "", "MC S$ " & txtSMC.Text.Trim, "")
        sSMC = "TEXT 193,165," & Chr(34) & "0" & Chr(34) & ",180,12,8," & Chr(34) & sSMC & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sSMC)

        'Barcode
        sInventoryCode = "BARCODE 185,79," & Chr(34) & "128M" & Chr(34) & ",38,0,180,2,4," & Chr(34) & "!105" & txtInventoryCode.Text.Trim & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sInventoryCode)
        'Inventory
        sInventoryCode = "TEXT 165,33," & Chr(34) & "3" & Chr(34) & ",180,1,1," & Chr(34) & txtInventoryCode.Text.Trim & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sInventoryCode)

        'Class
        sClass = txtClass.Text.Trim
        sClass = "TEXT 201,125," & Chr(34) & "ROMAN.TTF" & Chr(34) & ",180,1,8," & Chr(34) & sClass & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sClass)

        'Description
        sDescription = Mid(txtDescription.Text.Trim, 1, 15)
        sDescription = "TEXT 201,101," & Chr(34) & "ROMAN.TTF" & Chr(34) & ",180,1,8," & Chr(34) & sDescription & Chr(34)
        subBarCodeDetailsToFile(gsBarCodeFilePath, sDescription)


        'No of lables
        subBarCodeDetailsToFile(gsBarCodeFilePath, "PRINT 1," & IIf(txtNoofLabels.Text.Trim = "", 1, txtNoofLabels.Text.Trim))


        Dim cPrinter As New clsBarcode
        sPrinterName = "TSC TTP-247"
        cPrinter.SendFileToPrinter(sPrinterName, gsBarCodeFilePath)
    End Sub

End Class
