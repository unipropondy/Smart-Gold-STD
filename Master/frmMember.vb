Public Class frmMember
    Inherits System.Windows.Forms.Form
    Dim objMember As New clsMember
    Dim flag As String


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
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblAddress2 As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtMobile As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblPersonInCharge As System.Windows.Forms.Label
    Friend WithEvents lblFax As System.Windows.Forms.Label
    Friend WithEvents txtAddress2 As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress1 As System.Windows.Forms.TextBox
    Friend WithEvents lblAddress1 As System.Windows.Forms.Label
    Friend WithEvents lblLocationCode As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents GrdSalesSummary As AxFPSpread.AxvaSpread
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents grdSalesDetail As AxFPSpread.AxvaSpread
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtMembercode As AxEditLib.AxfpText
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtMemberType As AxEditLib.AxfpText
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtLocation As AxEditLib.AxfpText
    Friend WithEvents txtNricNo As System.Windows.Forms.TextBox
    Friend WithEvents txtMemberName As System.Windows.Forms.TextBox
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents txtEMM As System.Windows.Forms.TextBox
    Friend WithEvents txtEDD As System.Windows.Forms.TextBox
    Friend WithEvents txtJMM As System.Windows.Forms.TextBox
    Friend WithEvents txtJDD As System.Windows.Forms.TextBox
    Friend WithEvents txtDBMM As System.Windows.Forms.TextBox
    Friend WithEvents txtDBDD As System.Windows.Forms.TextBox
    Friend WithEvents txtREMM As System.Windows.Forms.TextBox
    Friend WithEvents txtREDD As System.Windows.Forms.TextBox
    Friend WithEvents txtRDMM As System.Windows.Forms.TextBox
    Friend WithEvents txtRDDD As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtTotalRewardPoint As System.Windows.Forms.TextBox
    Friend WithEvents txtDiscount As System.Windows.Forms.TextBox
    Friend WithEvents txtOffPhone As System.Windows.Forms.TextBox
    Friend WithEvents txtResPhone As System.Windows.Forms.TextBox
    Friend WithEvents txtZipcode As System.Windows.Forms.TextBox
    Friend WithEvents ChkActive As System.Windows.Forms.CheckBox
    Friend WithEvents txtTotalUsagePoint As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents txtREYYYY As System.Windows.Forms.TextBox
    Friend WithEvents txtDBYYYY As System.Windows.Forms.TextBox
    Friend WithEvents txtRDYYYY As System.Windows.Forms.TextBox
    Friend WithEvents txtEYYYY As System.Windows.Forms.TextBox
    Friend WithEvents txtJYYYY As System.Windows.Forms.TextBox
    Friend WithEvents txtNricType As AxEditLib.AxfpText
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtCountry As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress3 As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmMember))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtAddress3 = New System.Windows.Forms.TextBox
        Me.Label29 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.txtNricType = New AxEditLib.AxfpText
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtTotalUsagePoint = New System.Windows.Forms.TextBox
        Me.txtTotalRewardPoint = New System.Windows.Forms.TextBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.txtREYYYY = New System.Windows.Forms.TextBox
        Me.txtREMM = New System.Windows.Forms.TextBox
        Me.txtREDD = New System.Windows.Forms.TextBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.ChkActive = New System.Windows.Forms.CheckBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.txtLocation = New AxEditLib.AxfpText
        Me.txtMemberType = New AxEditLib.AxfpText
        Me.Label25 = New System.Windows.Forms.Label
        Me.txtRemarks = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.txtDiscount = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.txtDBYYYY = New System.Windows.Forms.TextBox
        Me.txtDBMM = New System.Windows.Forms.TextBox
        Me.txtDBDD = New System.Windows.Forms.TextBox
        Me.txtRDYYYY = New System.Windows.Forms.TextBox
        Me.txtRDMM = New System.Windows.Forms.TextBox
        Me.txtRDDD = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtEYYYY = New System.Windows.Forms.TextBox
        Me.txtEMM = New System.Windows.Forms.TextBox
        Me.txtEDD = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtJYYYY = New System.Windows.Forms.TextBox
        Me.txtJMM = New System.Windows.Forms.TextBox
        Me.txtJDD = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtMembercode = New AxEditLib.AxfpText
        Me.Label6 = New System.Windows.Forms.Label
        Me.lblAddress2 = New System.Windows.Forms.Label
        Me.txtEmail = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtMobile = New System.Windows.Forms.TextBox
        Me.txtOffPhone = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtCountry = New System.Windows.Forms.TextBox
        Me.txtResPhone = New System.Windows.Forms.TextBox
        Me.lblPersonInCharge = New System.Windows.Forms.Label
        Me.txtZipcode = New System.Windows.Forms.TextBox
        Me.lblFax = New System.Windows.Forms.Label
        Me.txtNricNo = New System.Windows.Forms.TextBox
        Me.txtAddress2 = New System.Windows.Forms.TextBox
        Me.txtAddress1 = New System.Windows.Forms.TextBox
        Me.lblAddress1 = New System.Windows.Forms.Label
        Me.txtMemberName = New System.Windows.Forms.TextBox
        Me.lblLocationCode = New System.Windows.Forms.Label
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.pic = New System.Windows.Forms.PictureBox
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.GrdSalesSummary = New AxFPSpread.AxvaSpread
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.grdSalesDetail = New AxFPSpread.AxvaSpread
        Me.btnNew = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        CType(Me.txtNricType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMemberType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMembercode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.GrdSalesSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.grdSalesDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtAddress3)
        Me.GroupBox1.Controls.Add(Me.Label29)
        Me.GroupBox1.Controls.Add(Me.Label23)
        Me.GroupBox1.Controls.Add(Me.txtNricType)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.txtTotalUsagePoint)
        Me.GroupBox1.Controls.Add(Me.txtTotalRewardPoint)
        Me.GroupBox1.Controls.Add(Me.Label28)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.txtREYYYY)
        Me.GroupBox1.Controls.Add(Me.txtREMM)
        Me.GroupBox1.Controls.Add(Me.txtREDD)
        Me.GroupBox1.Controls.Add(Me.Label27)
        Me.GroupBox1.Controls.Add(Me.ChkActive)
        Me.GroupBox1.Controls.Add(Me.Label26)
        Me.GroupBox1.Controls.Add(Me.txtLocation)
        Me.GroupBox1.Controls.Add(Me.txtMemberType)
        Me.GroupBox1.Controls.Add(Me.Label25)
        Me.GroupBox1.Controls.Add(Me.txtRemarks)
        Me.GroupBox1.Controls.Add(Me.Label24)
        Me.GroupBox1.Controls.Add(Me.txtDiscount)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.txtDBYYYY)
        Me.GroupBox1.Controls.Add(Me.txtDBMM)
        Me.GroupBox1.Controls.Add(Me.txtDBDD)
        Me.GroupBox1.Controls.Add(Me.txtRDYYYY)
        Me.GroupBox1.Controls.Add(Me.txtRDMM)
        Me.GroupBox1.Controls.Add(Me.txtRDDD)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.txtEYYYY)
        Me.GroupBox1.Controls.Add(Me.txtEMM)
        Me.GroupBox1.Controls.Add(Me.txtEDD)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtJYYYY)
        Me.GroupBox1.Controls.Add(Me.txtJMM)
        Me.GroupBox1.Controls.Add(Me.txtJDD)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtMembercode)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.lblAddress2)
        Me.GroupBox1.Controls.Add(Me.txtEmail)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtMobile)
        Me.GroupBox1.Controls.Add(Me.txtOffPhone)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtCountry)
        Me.GroupBox1.Controls.Add(Me.txtResPhone)
        Me.GroupBox1.Controls.Add(Me.lblPersonInCharge)
        Me.GroupBox1.Controls.Add(Me.txtZipcode)
        Me.GroupBox1.Controls.Add(Me.lblFax)
        Me.GroupBox1.Controls.Add(Me.txtNricNo)
        Me.GroupBox1.Controls.Add(Me.txtAddress2)
        Me.GroupBox1.Controls.Add(Me.txtAddress1)
        Me.GroupBox1.Controls.Add(Me.lblAddress1)
        Me.GroupBox1.Controls.Add(Me.txtMemberName)
        Me.GroupBox1.Controls.Add(Me.lblLocationCode)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(8, 104)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(664, 360)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Member Details"
        '
        'txtAddress3
        '
        Me.txtAddress3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress3.Location = New System.Drawing.Point(136, 120)
        Me.txtAddress3.Name = "txtAddress3"
        Me.txtAddress3.Size = New System.Drawing.Size(224, 21)
        Me.txtAddress3.TabIndex = 4
        Me.txtAddress3.Text = ""
        '
        'Label29
        '
        Me.Label29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(24, 128)
        Me.Label29.Name = "Label29"
        Me.Label29.TabIndex = 476289
        '
        'Label23
        '
        Me.Label23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(408, 48)
        Me.Label23.Name = "Label23"
        Me.Label23.TabIndex = 476288
        Me.Label23.Text = "Nric Type"
        '
        'txtNricType
        '
        Me.txtNricType.ContainingControl = Me
        Me.txtNricType.Location = New System.Drawing.Point(520, 40)
        Me.txtNricType.Name = "txtNricType"
        Me.txtNricType.OcxState = CType(resources.GetObject("txtNricType.OcxState"), System.Windows.Forms.AxHost.State)
        Me.txtNricType.Size = New System.Drawing.Size(136, 24)
        Me.txtNricType.TabIndex = 17
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(408, 288)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(112, 24)
        Me.Label16.TabIndex = 476286
        Me.Label16.Text = "Total Usage Point"
        '
        'txtTotalUsagePoint
        '
        Me.txtTotalUsagePoint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalUsagePoint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalUsagePoint.Location = New System.Drawing.Point(520, 288)
        Me.txtTotalUsagePoint.Name = "txtTotalUsagePoint"
        Me.txtTotalUsagePoint.Size = New System.Drawing.Size(136, 20)
        Me.txtTotalUsagePoint.TabIndex = 33
        Me.txtTotalUsagePoint.Text = ""
        Me.txtTotalUsagePoint.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotalRewardPoint
        '
        Me.txtTotalRewardPoint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalRewardPoint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalRewardPoint.Location = New System.Drawing.Point(520, 264)
        Me.txtTotalRewardPoint.Name = "txtTotalRewardPoint"
        Me.txtTotalRewardPoint.Size = New System.Drawing.Size(136, 20)
        Me.txtTotalRewardPoint.TabIndex = 32
        Me.txtTotalRewardPoint.Text = ""
        Me.txtTotalRewardPoint.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label28
        '
        Me.Label28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(408, 264)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(112, 18)
        Me.Label28.TabIndex = 476283
        Me.Label28.Text = "Total Reward Point"
        '
        'Label21
        '
        Me.Label21.Location = New System.Drawing.Point(592, 168)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(8, 16)
        Me.Label21.TabIndex = 476282
        Me.Label21.Text = "/"
        '
        'Label22
        '
        Me.Label22.Location = New System.Drawing.Point(552, 168)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(8, 16)
        Me.Label22.TabIndex = 476281
        Me.Label22.Text = "/"
        '
        'txtREYYYY
        '
        Me.txtREYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtREYYYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtREYYYY.Location = New System.Drawing.Point(600, 168)
        Me.txtREYYYY.MaxLength = 10
        Me.txtREYYYY.Name = "txtREYYYY"
        Me.txtREYYYY.Size = New System.Drawing.Size(56, 20)
        Me.txtREYYYY.TabIndex = 24
        Me.txtREYYYY.Tag = "NE;"
        Me.txtREYYYY.Text = ""
        Me.txtREYYYY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtREMM
        '
        Me.txtREMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtREMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtREMM.Location = New System.Drawing.Point(560, 168)
        Me.txtREMM.MaxLength = 2
        Me.txtREMM.Name = "txtREMM"
        Me.txtREMM.Size = New System.Drawing.Size(32, 20)
        Me.txtREMM.TabIndex = 23
        Me.txtREMM.Tag = "NE;"
        Me.txtREMM.Text = ""
        Me.txtREMM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtREDD
        '
        Me.txtREDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtREDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtREDD.Location = New System.Drawing.Point(520, 168)
        Me.txtREDD.MaxLength = 2
        Me.txtREDD.Name = "txtREDD"
        Me.txtREDD.Size = New System.Drawing.Size(32, 20)
        Me.txtREDD.TabIndex = 22
        Me.txtREDD.Tag = "NE;"
        Me.txtREDD.Text = ""
        Me.txtREDD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label27
        '
        Me.Label27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(24, 304)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(48, 23)
        Me.Label27.TabIndex = 476277
        Me.Label27.Text = "Active"
        '
        'ChkActive
        '
        Me.ChkActive.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ChkActive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkActive.Location = New System.Drawing.Point(128, 304)
        Me.ChkActive.Name = "ChkActive"
        Me.ChkActive.Size = New System.Drawing.Size(24, 24)
        Me.ChkActive.TabIndex = 15
        '
        'Label26
        '
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(24, 200)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(80, 24)
        Me.Label26.TabIndex = 476275
        Me.Label26.Text = "Location Code"
        '
        'txtLocation
        '
        Me.txtLocation.ContainingControl = Me
        Me.txtLocation.Location = New System.Drawing.Point(136, 192)
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.OcxState = CType(resources.GetObject("txtLocation.OcxState"), System.Windows.Forms.AxHost.State)
        Me.txtLocation.Size = New System.Drawing.Size(136, 24)
        Me.txtLocation.TabIndex = 7
        '
        'txtMemberType
        '
        Me.txtMemberType.ContainingControl = Me
        Me.txtMemberType.Location = New System.Drawing.Point(136, 272)
        Me.txtMemberType.Name = "txtMemberType"
        Me.txtMemberType.OcxState = CType(resources.GetObject("txtMemberType.OcxState"), System.Windows.Forms.AxHost.State)
        Me.txtMemberType.Size = New System.Drawing.Size(136, 24)
        Me.txtMemberType.TabIndex = 14
        '
        'Label25
        '
        Me.Label25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(24, 272)
        Me.Label25.Name = "Label25"
        Me.Label25.TabIndex = 476272
        Me.Label25.Text = "Member Type"
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Location = New System.Drawing.Point(136, 328)
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(520, 21)
        Me.txtRemarks.TabIndex = 34
        Me.txtRemarks.Text = ""
        '
        'Label24
        '
        Me.Label24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(24, 328)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(100, 16)
        Me.Label24.TabIndex = 476270
        Me.Label24.Text = "Remarks"
        '
        'txtDiscount
        '
        Me.txtDiscount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDiscount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiscount.Location = New System.Drawing.Point(520, 216)
        Me.txtDiscount.MaxLength = 3
        Me.txtDiscount.Name = "txtDiscount"
        Me.txtDiscount.Size = New System.Drawing.Size(56, 20)
        Me.txtDiscount.TabIndex = 28
        Me.txtDiscount.Text = "0.00"
        Me.txtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label20
        '
        Me.Label20.Location = New System.Drawing.Point(592, 240)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(8, 16)
        Me.Label20.TabIndex = 476261
        Me.Label20.Text = "/"
        '
        'Label19
        '
        Me.Label19.Location = New System.Drawing.Point(552, 240)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(8, 16)
        Me.Label19.TabIndex = 476260
        Me.Label19.Text = "/"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(592, 192)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(8, 16)
        Me.Label18.TabIndex = 476259
        Me.Label18.Text = "/"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(552, 192)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(8, 16)
        Me.Label17.TabIndex = 476258
        Me.Label17.Text = "/"
        '
        'txtDBYYYY
        '
        Me.txtDBYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDBYYYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDBYYYY.Location = New System.Drawing.Point(600, 240)
        Me.txtDBYYYY.MaxLength = 10
        Me.txtDBYYYY.Name = "txtDBYYYY"
        Me.txtDBYYYY.Size = New System.Drawing.Size(56, 20)
        Me.txtDBYYYY.TabIndex = 31
        Me.txtDBYYYY.Tag = "NE;"
        Me.txtDBYYYY.Text = ""
        Me.txtDBYYYY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtDBMM
        '
        Me.txtDBMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDBMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDBMM.Location = New System.Drawing.Point(560, 240)
        Me.txtDBMM.MaxLength = 2
        Me.txtDBMM.Name = "txtDBMM"
        Me.txtDBMM.Size = New System.Drawing.Size(32, 20)
        Me.txtDBMM.TabIndex = 30
        Me.txtDBMM.Tag = "NE;"
        Me.txtDBMM.Text = ""
        Me.txtDBMM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtDBDD
        '
        Me.txtDBDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDBDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDBDD.Location = New System.Drawing.Point(520, 240)
        Me.txtDBDD.MaxLength = 2
        Me.txtDBDD.Name = "txtDBDD"
        Me.txtDBDD.Size = New System.Drawing.Size(32, 20)
        Me.txtDBDD.TabIndex = 29
        Me.txtDBDD.Tag = "NE;"
        Me.txtDBDD.Text = ""
        Me.txtDBDD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtRDYYYY
        '
        Me.txtRDYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRDYYYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRDYYYY.Location = New System.Drawing.Point(600, 192)
        Me.txtRDYYYY.MaxLength = 20
        Me.txtRDYYYY.Name = "txtRDYYYY"
        Me.txtRDYYYY.Size = New System.Drawing.Size(56, 20)
        Me.txtRDYYYY.TabIndex = 27
        Me.txtRDYYYY.Tag = "NE;"
        Me.txtRDYYYY.Text = ""
        Me.txtRDYYYY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtRDMM
        '
        Me.txtRDMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRDMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRDMM.Location = New System.Drawing.Point(560, 192)
        Me.txtRDMM.MaxLength = 20
        Me.txtRDMM.Name = "txtRDMM"
        Me.txtRDMM.Size = New System.Drawing.Size(32, 20)
        Me.txtRDMM.TabIndex = 26
        Me.txtRDMM.Tag = "NE;"
        Me.txtRDMM.Text = ""
        Me.txtRDMM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtRDDD
        '
        Me.txtRDDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRDDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRDDD.Location = New System.Drawing.Point(520, 192)
        Me.txtRDDD.MaxLength = 2
        Me.txtRDDD.Name = "txtRDDD"
        Me.txtRDDD.Size = New System.Drawing.Size(32, 20)
        Me.txtRDDD.TabIndex = 25
        Me.txtRDDD.Tag = "NE;"
        Me.txtRDDD.Text = ""
        Me.txtRDDD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(408, 240)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(96, 16)
        Me.Label15.TabIndex = 476250
        Me.Label15.Text = "Date Of Birth"
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(208, 248)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(8, 51)
        Me.Label14.TabIndex = 476249
        Me.Label14.Text = "/"
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(168, 248)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(8, 51)
        Me.Label13.TabIndex = 476248
        Me.Label13.Text = "/"
        '
        'txtEYYYY
        '
        Me.txtEYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEYYYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEYYYY.Location = New System.Drawing.Point(216, 248)
        Me.txtEYYYY.MaxLength = 10
        Me.txtEYYYY.Name = "txtEYYYY"
        Me.txtEYYYY.Size = New System.Drawing.Size(56, 20)
        Me.txtEYYYY.TabIndex = 13
        Me.txtEYYYY.Tag = "NE;"
        Me.txtEYYYY.Text = ""
        Me.txtEYYYY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtEMM
        '
        Me.txtEMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEMM.Location = New System.Drawing.Point(176, 248)
        Me.txtEMM.MaxLength = 2
        Me.txtEMM.Name = "txtEMM"
        Me.txtEMM.Size = New System.Drawing.Size(32, 20)
        Me.txtEMM.TabIndex = 12
        Me.txtEMM.Tag = "NE;"
        Me.txtEMM.Text = ""
        Me.txtEMM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtEDD
        '
        Me.txtEDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEDD.Location = New System.Drawing.Point(136, 248)
        Me.txtEDD.MaxLength = 2
        Me.txtEDD.Name = "txtEDD"
        Me.txtEDD.Size = New System.Drawing.Size(32, 20)
        Me.txtEDD.TabIndex = 11
        Me.txtEDD.Tag = "NE;"
        Me.txtEDD.Text = ""
        Me.txtEDD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(24, 248)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(96, 23)
        Me.Label12.TabIndex = 476244
        Me.Label12.Text = "Expiry Date"
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(408, 216)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(87, 16)
        Me.Label11.TabIndex = 476243
        Me.Label11.Text = "Discount"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(24, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 16)
        Me.Label3.TabIndex = 476241
        Me.Label3.Text = "Name"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(24, 144)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 22)
        Me.Label2.TabIndex = 476240
        Me.Label2.Text = "Country"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(208, 224)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(8, 16)
        Me.Label10.TabIndex = 476238
        Me.Label10.Text = "/"
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(168, 224)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(8, 16)
        Me.Label9.TabIndex = 476237
        Me.Label9.Text = "/"
        '
        'txtJYYYY
        '
        Me.txtJYYYY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJYYYY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJYYYY.Location = New System.Drawing.Point(216, 224)
        Me.txtJYYYY.MaxLength = 10
        Me.txtJYYYY.Name = "txtJYYYY"
        Me.txtJYYYY.Size = New System.Drawing.Size(56, 20)
        Me.txtJYYYY.TabIndex = 10
        Me.txtJYYYY.Tag = "NE;"
        Me.txtJYYYY.Text = ""
        Me.txtJYYYY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtJMM
        '
        Me.txtJMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJMM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJMM.Location = New System.Drawing.Point(176, 224)
        Me.txtJMM.MaxLength = 2
        Me.txtJMM.Name = "txtJMM"
        Me.txtJMM.Size = New System.Drawing.Size(32, 20)
        Me.txtJMM.TabIndex = 9
        Me.txtJMM.Tag = "NE;"
        Me.txtJMM.Text = ""
        Me.txtJMM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtJDD
        '
        Me.txtJDD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJDD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJDD.Location = New System.Drawing.Point(136, 224)
        Me.txtJDD.MaxLength = 2
        Me.txtJDD.Name = "txtJDD"
        Me.txtJDD.Size = New System.Drawing.Size(32, 20)
        Me.txtJDD.TabIndex = 8
        Me.txtJDD.Tag = "NE;"
        Me.txtJDD.Text = ""
        Me.txtJDD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(408, 192)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(112, 16)
        Me.Label8.TabIndex = 476236
        Me.Label8.Text = "Reward Expiry Date"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(408, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 8)
        Me.Label1.TabIndex = 476232
        Me.Label1.Text = "Nric Number"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtMembercode
        '
        Me.txtMembercode.ContainingControl = Me
        Me.txtMembercode.Location = New System.Drawing.Point(136, 16)
        Me.txtMembercode.Name = "txtMembercode"
        Me.txtMembercode.OcxState = CType(resources.GetObject("txtMembercode.OcxState"), System.Windows.Forms.AxHost.State)
        Me.txtMembercode.Size = New System.Drawing.Size(168, 24)
        Me.txtMembercode.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(24, 232)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 8)
        Me.Label6.TabIndex = 476230
        Me.Label6.Text = "Join Date"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblAddress2
        '
        Me.lblAddress2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddress2.ForeColor = System.Drawing.Color.Black
        Me.lblAddress2.Location = New System.Drawing.Point(408, 168)
        Me.lblAddress2.Name = "lblAddress2"
        Me.lblAddress2.Size = New System.Drawing.Size(88, 15)
        Me.lblAddress2.TabIndex = 476223
        Me.lblAddress2.Text = "Renewal Date"
        Me.lblAddress2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtEmail
        '
        Me.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmail.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower
        Me.txtEmail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmail.Location = New System.Drawing.Point(520, 144)
        Me.txtEmail.MaxLength = 50
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(136, 20)
        Me.txtEmail.TabIndex = 21
        Me.txtEmail.Tag = "NE"
        Me.txtEmail.Text = ""
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(408, 152)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 8)
        Me.Label4.TabIndex = 476222
        Me.Label4.Text = "Email"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtMobile
        '
        Me.txtMobile.AcceptsTab = True
        Me.txtMobile.BackColor = System.Drawing.SystemColors.HighlightText
        Me.txtMobile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMobile.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMobile.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMobile.ForeColor = System.Drawing.Color.Black
        Me.txtMobile.Location = New System.Drawing.Point(520, 96)
        Me.txtMobile.MaxLength = 14
        Me.txtMobile.Name = "txtMobile"
        Me.txtMobile.Size = New System.Drawing.Size(136, 20)
        Me.txtMobile.TabIndex = 19
        Me.txtMobile.Tag = "NE"
        Me.txtMobile.Text = ""
        '
        'txtOffPhone
        '
        Me.txtOffPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOffPhone.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtOffPhone.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOffPhone.Location = New System.Drawing.Point(520, 120)
        Me.txtOffPhone.MaxLength = 50
        Me.txtOffPhone.Name = "txtOffPhone"
        Me.txtOffPhone.Size = New System.Drawing.Size(136, 20)
        Me.txtOffPhone.TabIndex = 20
        Me.txtOffPhone.Tag = "NE"
        Me.txtOffPhone.Text = ""
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(408, 104)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(88, 8)
        Me.Label5.TabIndex = 476220
        Me.Label5.Text = "Mobile"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(408, 128)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(88, 8)
        Me.Label7.TabIndex = 476221
        Me.Label7.Text = "Off-Phone"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCountry
        '
        Me.txtCountry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCountry.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCountry.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCountry.Location = New System.Drawing.Point(136, 144)
        Me.txtCountry.MaxLength = 50
        Me.txtCountry.Name = "txtCountry"
        Me.txtCountry.Size = New System.Drawing.Size(136, 20)
        Me.txtCountry.TabIndex = 5
        Me.txtCountry.Tag = "NE"
        Me.txtCountry.Text = ""
        '
        'txtResPhone
        '
        Me.txtResPhone.AcceptsTab = True
        Me.txtResPhone.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtResPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtResPhone.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtResPhone.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtResPhone.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtResPhone.Location = New System.Drawing.Point(520, 72)
        Me.txtResPhone.MaxLength = 50
        Me.txtResPhone.Name = "txtResPhone"
        Me.txtResPhone.Size = New System.Drawing.Size(136, 20)
        Me.txtResPhone.TabIndex = 18
        Me.txtResPhone.Tag = "NE"
        Me.txtResPhone.Text = ""
        '
        'lblPersonInCharge
        '
        Me.lblPersonInCharge.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPersonInCharge.ForeColor = System.Drawing.Color.Black
        Me.lblPersonInCharge.Location = New System.Drawing.Point(408, 72)
        Me.lblPersonInCharge.Name = "lblPersonInCharge"
        Me.lblPersonInCharge.Size = New System.Drawing.Size(88, 24)
        Me.lblPersonInCharge.TabIndex = 476211
        Me.lblPersonInCharge.Text = "Res-Phone"
        Me.lblPersonInCharge.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtZipcode
        '
        Me.txtZipcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtZipcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtZipcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtZipcode.Location = New System.Drawing.Point(136, 168)
        Me.txtZipcode.MaxLength = 50
        Me.txtZipcode.Name = "txtZipcode"
        Me.txtZipcode.Size = New System.Drawing.Size(136, 20)
        Me.txtZipcode.TabIndex = 6
        Me.txtZipcode.Tag = "NE"
        Me.txtZipcode.Text = ""
        '
        'lblFax
        '
        Me.lblFax.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFax.ForeColor = System.Drawing.Color.Black
        Me.lblFax.Location = New System.Drawing.Point(24, 168)
        Me.lblFax.Name = "lblFax"
        Me.lblFax.Size = New System.Drawing.Size(88, 14)
        Me.lblFax.TabIndex = 476210
        Me.lblFax.Text = "Zip Code"
        Me.lblFax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtNricNo
        '
        Me.txtNricNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNricNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNricNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNricNo.Location = New System.Drawing.Point(520, 16)
        Me.txtNricNo.MaxLength = 50
        Me.txtNricNo.Name = "txtNricNo"
        Me.txtNricNo.Size = New System.Drawing.Size(136, 20)
        Me.txtNricNo.TabIndex = 16
        Me.txtNricNo.Tag = "NE"
        Me.txtNricNo.Text = ""
        '
        'txtAddress2
        '
        Me.txtAddress2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAddress2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress2.Location = New System.Drawing.Point(136, 96)
        Me.txtAddress2.MaxLength = 50
        Me.txtAddress2.Name = "txtAddress2"
        Me.txtAddress2.Size = New System.Drawing.Size(224, 20)
        Me.txtAddress2.TabIndex = 3
        Me.txtAddress2.Tag = "NE"
        Me.txtAddress2.Text = ""
        '
        'txtAddress1
        '
        Me.txtAddress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAddress1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress1.Location = New System.Drawing.Point(136, 72)
        Me.txtAddress1.MaxLength = 50
        Me.txtAddress1.Name = "txtAddress1"
        Me.txtAddress1.Size = New System.Drawing.Size(224, 20)
        Me.txtAddress1.TabIndex = 2
        Me.txtAddress1.Tag = "NE"
        Me.txtAddress1.Text = ""
        '
        'lblAddress1
        '
        Me.lblAddress1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddress1.ForeColor = System.Drawing.Color.Black
        Me.lblAddress1.Location = New System.Drawing.Point(24, 80)
        Me.lblAddress1.Name = "lblAddress1"
        Me.lblAddress1.Size = New System.Drawing.Size(88, 8)
        Me.lblAddress1.TabIndex = 476206
        Me.lblAddress1.Text = "Address"
        Me.lblAddress1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtMemberName
        '
        Me.txtMemberName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMemberName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMemberName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMemberName.Location = New System.Drawing.Point(136, 48)
        Me.txtMemberName.MaxLength = 50
        Me.txtMemberName.Name = "txtMemberName"
        Me.txtMemberName.Size = New System.Drawing.Size(224, 20)
        Me.txtMemberName.TabIndex = 1
        Me.txtMemberName.Tag = "NE"
        Me.txtMemberName.Text = ""
        '
        'lblLocationCode
        '
        Me.lblLocationCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationCode.ForeColor = System.Drawing.Color.Black
        Me.lblLocationCode.Location = New System.Drawing.Point(24, 27)
        Me.lblLocationCode.Name = "lblLocationCode"
        Me.lblLocationCode.Size = New System.Drawing.Size(96, 18)
        Me.lblLocationCode.TabIndex = 476204
        Me.lblLocationCode.Text = "Member Code"
        Me.lblLocationCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(592, 656)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.TabIndex = 37
        Me.btnClose.Text = "Close(Esc)"
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.Location = New System.Drawing.Point(432, 656)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.TabIndex = 35
        Me.btnSave.Text = "Save(F7)"
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.Location = New System.Drawing.Point(512, 656)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.TabIndex = 36
        Me.btnDelete.Text = "Delete(F8)"
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(8, 8)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(210, 80)
        Me.pic.TabIndex = 55
        Me.pic.TabStop = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(8, 472)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(664, 176)
        Me.TabControl1.TabIndex = 45
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.HighlightText
        Me.TabPage1.Controls.Add(Me.GrdSalesSummary)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(656, 150)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Sales Summary                                   "
        '
        'GrdSalesSummary
        '
        Me.GrdSalesSummary.ContainingControl = Me
        Me.GrdSalesSummary.Location = New System.Drawing.Point(12, 8)
        Me.GrdSalesSummary.Name = "GrdSalesSummary"
        Me.GrdSalesSummary.OcxState = CType(resources.GetObject("GrdSalesSummary.OcxState"), System.Windows.Forms.AxHost.State)
        Me.GrdSalesSummary.Size = New System.Drawing.Size(641, 134)
        Me.GrdSalesSummary.TabIndex = 33
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.SystemColors.HighlightText
        Me.TabPage2.Controls.Add(Me.grdSalesDetail)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(656, 150)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Sales Detail                                                                     " & _
        ""
        Me.TabPage2.Visible = False
        '
        'grdSalesDetail
        '
        Me.grdSalesDetail.ContainingControl = Me
        Me.grdSalesDetail.Location = New System.Drawing.Point(16, 6)
        Me.grdSalesDetail.Name = "grdSalesDetail"
        Me.grdSalesDetail.OcxState = CType(resources.GetObject("grdSalesDetail.OcxState"), System.Windows.Forms.AxHost.State)
        Me.grdSalesDetail.Size = New System.Drawing.Size(641, 130)
        Me.grdSalesDetail.TabIndex = 4
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(352, 656)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.TabIndex = 38
        Me.btnNew.Text = "New(F6)"
        '
        'frmMember
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ClientSize = New System.Drawing.Size(678, 688)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmMember"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Member "
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.txtNricType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMemberType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMembercode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.GrdSalesSummary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.grdSalesDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    

    Private Sub txtMemberCode_ButtonHit(ByVal sender As System.Object, ByVal e As AxEditLib._DfpText_ButtonHitEvent) Handles txtMembercode.ButtonHit
        Dim frmHelpClass As New frmHelpSearch(0, 0, "tblMember", "L", 4, "MemberName", "", "MemberCode", "MemberCode", 80, "MemberName", "MemberName", 180, "Address1", "Address1", 60, "Address2", "Address2", 80)
        frmHelpClass.ShowDialog()
        txtMembercode.Text = frmHelpClass.Var1.Trim
        'txtMembercode.Enabled = False
        LoadMemberDetails(txtMembercode.Text.Trim)
    End Sub

    

    Private Sub txtMemberType_ButtonHit(ByVal sender As Object, ByVal e As AxEditLib._DfpText_ButtonHitEvent) Handles txtMemberType.ButtonHit
        Dim frmHelpClass As New frmHelpSearch(0, 0, "tblMemberType", "L", 4, "MemberTypeDescription", "", "MemberType", "MemberType", 80, "MemberTypeDescription", "MemberTypeDescription", 100, "MemberTypeDiscount", "MemberTypeDiscount", 170, "Active", "Active", 190)
        frmHelpClass.ShowDialog()
        txtMemberType.Text = frmHelpClass.Var1.Trim
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Dim JD, ED, RD, RED, DOB As String

        Dim sActive As String
        Try
            If ValidateMe() Then
                JD = txtJDD.Text.Trim & "/" & txtJMM.Text.Trim & "/" & txtJYYYY.Text.Trim
                ED = txtEDD.Text.Trim & "/" & txtEMM.Text.Trim & "/" & txtEYYYY.Text.Trim
                RD = txtREDD.Text.Trim & "/" & txtREMM.Text.Trim & "/" & txtREYYYY.Text.Trim
                RED = txtRDDD.Text.Trim & "/" & txtRDMM.Text.Trim & "/" & txtRDYYYY.Text.Trim
                DOB = txtDBDD.Text.Trim & "/" & txtDBMM.Text.Trim & "/" & txtDBYYYY.Text.Trim
                If ChkActive.Checked = True Then
                    sActive = 1
                Else
                    sActive = 0
                End If


                If objMember.MemberSave(txtMembercode.Text.Trim, txtNricNo.Text.Trim, txtNricType.Text.Trim, txtMemberName.Text.Trim, _
                txtAddress1.Text, txtAddress2.Text, txtAddress3.Text.Trim, txtCountry.Text.Trim, txtZipcode.Text.Trim, txtEmail.Text.Trim, _
                 txtResPhone.Text.Trim, txtOffPhone.Text.Trim, txtMobile.Text.Trim, JD, ED, RD, 0, 0, Val(txtTotalRewardPoint.Text.Trim), Val(txtTotalUsagePoint.Text.Trim), _
                RED, sActive, txtMemberType.Text.Trim, Val(txtDiscount.Text.Trim), DOB, txtRemarks.Text.Trim, txtLocation.Text.Trim) Then
                    MessageBox.Show("Member detail saved Successfully")
                    sMemberCode = txtMembercode.Text.Trim
                    ClearMe()
                    Me.Close()
                    'GetCustomerDetails()

                    '  LoadCustomerDetails(Trim(GetText(grdCustomer, grdCustomer.ActiveRow, 1)))
                Else
                    MessageBox.Show("Error in Save")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtLocation_ButtonHit(ByVal sender As Object, ByVal e As AxEditLib._DfpText_ButtonHitEvent) Handles txtLocation.ButtonHit
        Dim frmHelpClass As New frmHelpSearch(0, 0, "tbllocation", "L", 4, "LocationName", "", "LocationCode", "LocationCode", 80, "LocationName", "LocationName", 180, "Address1", "Address1", 60, "Address2", "Address2", 80)
        frmHelpClass.ShowDialog()
        txtLocation.Text = frmHelpClass.Var1.Trim

    End Sub

    Private Sub txtMembercode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMembercode.LostFocus
        If Trim(txtMembercode.Text) <> "" Then
            LoadMemberDetails(txtMembercode.Text.Trim)
        End If
    End Sub
    Public Sub LoadMemberDetails(ByVal sMemberCode As String)
        Try
            Dim dsMember As New DataSet
            Dim iCnt As Long
            dsMember = objMember.GetMemberwithCode(sMemberCode)
            If Not IsNothing(dsMember) Then
                If dsMember.Tables("Member").Rows.Count > 0 Then
                    txtMembercode.Text = Trim(dsMember.Tables("Member").Rows(0)("Membercode") & "")
                    txtMemberName.Text = Trim(dsMember.Tables("Member").Rows(0)("Membername") & "")
                    txtAddress1.Text = Trim(dsMember.Tables("member").Rows(0)("Address1") & "")
                    txtAddress2.Text = Trim(dsMember.Tables("Member").Rows(0)("Address2") & "")
                    txtAddress3.Text = Trim(dsMember.Tables("Member").Rows(0)("Address3") & "")
                    txtCountry.Text = Trim(dsMember.Tables("Member").Rows(0)("Country") & "")
                    txtZipcode.Text = Trim(dsMember.Tables("Member").Rows(0)("Zipcode") & "")
                    txtNricNo.Text = Trim(dsMember.Tables("Member").Rows(0)("nricno") & "")
                    txtLocation.Text = Trim(dsMember.Tables("Member").Rows(0)("Locationcode") & "")
                    txtNricType.Text = Trim(dsMember.Tables("Member").Rows(0)("NricTypeCode") & "")
                    txtMobile.Text = Trim(dsMember.Tables("Member").Rows(0)("Mobile") & "")
                    txtResPhone.Text = Trim(dsMember.Tables("Member").Rows(0)("Resphone") & "")
                    txtOffPhone.Text = Trim(dsMember.Tables("Member").Rows(0)("OffPhone") & "")
                    txtEmail.Text = Trim(dsMember.Tables("Member").Rows(0)("Email") & "")
                    txtDiscount.Text = Format(Val(dsMember.Tables(0).Rows(0)("MemberDiscount") & ""), "0.00")
                    txtTotalUsagePoint.Text = Trim(dsMember.Tables("Member").Rows(0)("TotalUsagePoint") & "")
                    txtTotalRewardPoint.Text = Trim(dsMember.Tables("Member").Rows(0)("TotalRewardPoint") & "")
                    txtLocation.Text = Trim(dsMember.Tables("Member").Rows(0)("Locationcode") & "")
                    txtRemarks.Text = Trim(dsMember.Tables("Member").Rows(0)("Remarks") & "")
                    txtJDD.Text = Format(CDate(dsMember.Tables(0).Rows(0)("Joindate") & ""), "dd")
                    txtJMM.Text = Format(CDate(dsMember.Tables(0).Rows(0)("Joindate") & ""), "MM")
                    txtJYYYY.Text = Format(CDate(dsMember.Tables(0).Rows(0)("Joindate") & ""), "yyyy")
                    txtEDD.Text = Format(CDate(dsMember.Tables(0).Rows(0)("Expirydate") & ""), "dd")
                    txtEMM.Text = Format(CDate(dsMember.Tables(0).Rows(0)("Expirydate") & ""), "MM")
                    txtEYYYY.Text = Format(CDate(dsMember.Tables(0).Rows(0)("Expirydate") & ""), "yyyy")
                    txtREDD.Text = Format(CDate(dsMember.Tables(0).Rows(0)("RenewalDate") & ""), "dd")
                    txtREMM.Text = Format(CDate(dsMember.Tables(0).Rows(0)("RenewalDate") & ""), "MM")
                    txtREYYYY.Text = Format(CDate(dsMember.Tables(0).Rows(0)("RenewalDate") & ""), "yyyy")
                    txtRDDD.Text = Format(CDate(dsMember.Tables(0).Rows(0)("RewardExpirydate") & ""), "dd")
                    txtRDMM.Text = Format(CDate(dsMember.Tables(0).Rows(0)("RewardExpirydate") & ""), "MM")
                    txtRDYYYY.Text = Format(CDate(dsMember.Tables(0).Rows(0)("RewardExpirydate") & ""), "yyyy")
                    txtDBDD.Text = Format(CDate(dsMember.Tables(0).Rows(0)("DateofBirth") & ""), "dd")
                    txtDBMM.Text = Format(CDate(dsMember.Tables(0).Rows(0)("DateofBirth") & ""), "MM")
                    txtDBYYYY.Text = Format(CDate(dsMember.Tables(0).Rows(0)("DateofBirth") & ""), "yyyy")
                    txtMemberType.Text = Trim(dsMember.Tables("Member").Rows(0)("MemberType") & "")
                    ChkActive.Checked = Trim(dsMember.Tables("Member").Rows(0)("Active") & "")
                    SubLoadSales()
                    txtMembercode.Enabled = False
                    btnDelete.Enabled = True
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub SubLoadSales()
        Try
            Dim dsmember As New DataSet
            Dim iCnt As Long
            GrdSalesSummary.MaxRows = 0
            grdSalesDetail.MaxRows = 0
            dsmember = objMember.GetSalesByMember(txtMembercode.Text.Trim)
            If Not IsNothing(dsmember) Then
                If dsmember.Tables("Member").Rows.Count > 0 Then
                    For iCnt = 0 To dsmember.Tables("Member").Rows.Count - 1
                        GrdSalesSummary.MaxRows = GrdSalesSummary.MaxRows + 1
                        SetText(GrdSalesSummary, iCnt + 1, 1, Trim(dsmember.Tables("Member").Rows(iCnt)("InvoiceNo") & ""))
                        SetText(GrdSalesSummary, iCnt + 1, 2, Trim(dsmember.Tables("Member").Rows(iCnt)("InvoiceDate") & ""))
                        SetText(GrdSalesSummary, iCnt + 1, 3, Trim(dsmember.Tables("Member").Rows(iCnt)("TotalWeight") & ""))
                        SetText(GrdSalesSummary, iCnt + 1, 4, Trim(dsmember.Tables("Member").Rows(iCnt)("TotalPCS") & ""))
                        SetText(GrdSalesSummary, iCnt + 1, 5, Trim(dsmember.Tables("Member").Rows(iCnt)("NetTotal") & ""))

                    Next
                    SubLoadSalesDetail()
                End If
                btnDelete.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmMember_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        subGridSetting(GrdSalesSummary)
        subGridSetting(grdSalesDetail)
        txtJYYYY.Text = Now.Year
        txtJMM.Text = Format(Now.Month, "00")
        txtJDD.Text = Format(Now.Day, "00")
    End Sub
    Private Sub SubLoadSalesDetail()
        Try
            Dim dsMember As New DataSet
            Dim iCnt As Long
            Dim InvoiceNo As String
            grdSalesDetail.MaxRows = 0
            InvoiceNo = Trim(GetText(GrdSalesSummary, GrdSalesSummary.ActiveRow, 1))
            If InvoiceNo.Trim <> "" Then
                dsMember = objMember.GetSalesDetailByInvoiceNo(InvoiceNo.Trim)
                If Not IsNothing(dsMember) Then
                    If dsMember.Tables("MEMBER").Rows.Count > 0 Then

                        For iCnt = 0 To dsMember.Tables("Member").Rows.Count - 1
                            grdSalesDetail.MaxRows = grdSalesDetail.MaxRows + 1
                            SetText(grdSalesDetail, iCnt + 1, 1, Trim(dsMember.Tables("Member").Rows(iCnt)("InventoryCode") & ""))
                            SetText(grdSalesDetail, iCnt + 1, 2, Trim(dsMember.Tables("Member").Rows(iCnt)("Description") & ""))
                            SetText(grdSalesDetail, iCnt + 1, 3, Trim(dsMember.Tables("Member").Rows(iCnt)("Weight") & ""))
                            SetText(grdSalesDetail, iCnt + 1, 4, Trim(dsMember.Tables("Member").Rows(iCnt)("PCS") & ""))
                            SetText(grdSalesDetail, iCnt + 1, 5, Trim(dsMember.Tables("Member").Rows(iCnt)("NetAmount") & ""))
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmMember_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F6
                btnNew_Click(sender, e)
            Case Keys.F7
                btnSave_Click(sender, e)
            Case Keys.F8
                If btnDelete.Enabled Then btnDelete_Click(sender, e)
            Case Keys.Escape
                btnClose_Click(sender, e)
            Case Keys.Enter
                SendKeys.Send("{Tab}")

        End Select
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If txtMembercode.Text.Trim <> "" Then
                Dim sMemberCode As String
                sMemberCode = txtMembercode.Text.Trim
                If MessageBox.Show("Do you want to delete " & sMemberCode & " Member?", "GoldPOS", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    If objMember.DeleteMember(sMemberCode) Then
                        MessageBox.Show("Member detail deleted.")
                        ClearMe()
                        txtMembercode.Focus()

                    Else
                        MessageBox.Show("Error in deleting Member detail.")
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ClearMe()
        txtMembercode.Focus()

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()

    End Sub
    Private Function ValidateMe() As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            If txtMembercode.Text.Trim = "" Then
                MessageBox.Show("Membercode should not be blank.")
                txtMembercode.Focus()
                Exit Function
            End If
            If txtMemberName.Text.Trim = "" Then
                MessageBox.Show("MemberName should not be blank.")
                txtMemberName.Focus()
                Exit Function
            End If

            If txtMemberType.Text.Trim = "" Then
                MessageBox.Show("MemberType should not be blank.")
                txtMemberType.Focus()
                Exit Function

            End If
            If txtNricNo.Text.Trim = "" Then
                MessageBox.Show("Nric Number should not be blank.")
                txtNricNo.Focus()
                Exit Function
            End If

            If txtCountry.Text.Trim = "" Then
                MessageBox.Show("Country Should not be blank.")
                txtCountry.Focus()
                Exit Function
            End If
            If txtLocation.Text.Trim = "" Then
                MessageBox.Show("LocationCode  Should not be blank.")
                txtLocation.Focus()
                Exit Function

            End If
            If IsDate(txtJDD.Text.Trim & "/" & txtJMM.Text.Trim & "/" & txtJYYYY.Text.Trim) = False Then
                MessageBox.Show("Enter valid Join Date")
                txtJDD.Focus()
                Exit Function
            End If
            If IsDate(txtEDD.Text.Trim & "/" & txtEMM.Text.Trim & "/" & txtEYYYY.Text.Trim) = False Then
                MessageBox.Show("Enter valid Expiry Date")
                txtEDD.Focus()
                Exit Function
            End If
            If IsDate(txtREDD.Text.Trim & "/" & txtREMM.Text.Trim & "/" & txtREYYYY.Text.Trim) = False Then
                MessageBox.Show("Enter valid Renewal  Date !")
                txtREDD.Focus()
                Exit Function
            End If
            If IsDate(txtRDDD.Text.Trim & "/" & txtRDMM.Text.Trim & "/" & txtRDYYYY.Text.Trim) = False Then
                MessageBox.Show("Enter valid Renewal Expiry Date! ")
                txtRDDD.Focus()
                Exit Function
            End If

            If IsDate(txtDBDD.Text.Trim & "/" & txtDBMM.Text.Trim & "/" & txtDBYYYY.Text.Trim) = False Then
                MessageBox.Show("Enter Valid Date of Birth")
                txtDBDD.Focus()
                Exit Function
            Else
                If CDate(txtDBDD.Text.Trim & "/" & txtDBMM.Text.Trim & "/" & txtDBYYYY.Text.Trim) > Now() Then
                    MsgBox("Date Of Birth is Greater than current date ", MsgBoxStyle.Critical)
                    txtEDD.Focus()
                    Exit Function
                End If
            End If

            If CDate(txtEDD.Text & "/" & txtEMM.Text & "/" & txtEYYYY.Text) < Now() Then
                MsgBox("Expiry Date Should be Greater than Join Date")
                txtEDD.Focus()
                Exit Function

            End If
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            bStatus = False
        Finally
            ValidateMe = bStatus
        End Try

    End Function
    Private Sub ClearMe()
        txtMembercode.Text = ""
        txtMemberName.Clear()
        txtAddress1.Clear()
        txtAddress2.Clear()
        txtAddress3.Clear()
        txtCountry.Clear()
        txtResPhone.Clear()
        ChkActive.Checked = False
        txtOffPhone.Clear()
        txtEmail.Clear()
        txtMobile.Clear()
        txtZipcode.Clear()
        txtEmail.Clear()
        txtTotalRewardPoint.Clear()
        txtEDD.Clear()
        txtEMM.Clear()
        txtEYYYY.Clear()
        txtRDDD.Clear()
        txtRDMM.Clear()
        txtRDYYYY.Clear()
        txtREDD.Clear()
        txtREMM.Clear()
        txtREYYYY.Clear()
        txtDBDD.Clear()
        txtDBMM.Clear()
        txtDBYYYY.Clear()
        txtDiscount.Clear()
        txtLocation.Text = ""
        txtTotalRewardPoint.Clear()
        txtMemberType.Text = ""
        txtNricType.Text = ""
        txtTotalUsagePoint.Clear()
        txtNricNo.Clear()
        txtRemarks.Clear()
        btnDelete.Enabled = False
        txtMembercode.Enabled = True
        GrdSalesSummary.MaxRows = 0
        grdSalesDetail.MaxRows = 0

    End Sub

    Private Sub txtLocation_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLocation.LostFocus
        Dim ds As New DataSet
        ds = objMember.SubGetMemberLocation(Trim(txtLocation.Text))
        If ds.Tables(0).Rows.Count > 0 Then
            txtLocation.Text = Trim(ds.Tables(0).Rows(0)("LocationCode") & "")
        Else
            txtLocation.Text = ""
        End If
    End Sub



    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click

        flag = "New"

        ClearMe()
        txtMembercode.Focus()

    End Sub



    Private Sub GrdSalesSummary_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_ClickEvent) Handles GrdSalesSummary.ClickEvent
        SubLoadSalesDetail()
    End Sub
    Private Sub txtMembercode_KeyDownEvent(ByVal sender As Object, ByVal e As AxEditLib._DfpText_KeyDownEvent) Handles txtMembercode.KeyDownEvent
        If e.keyCode = Keys.F1 Then
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblMember", "L", 4, "MemberName", "", "MemberCode", "MemberCode", 80, "MemberName", "MemberName", 180, "Address1", "Address1", 60, "Address2", "Address2", 80)
            frmHelpClass.ShowDialog()
            txtMembercode.Text = frmHelpClass.Var1.Trim
        End If
    End Sub

    Private Sub txtMemberType_KeyDownEvent(ByVal sender As Object, ByVal e As AxEditLib._DfpText_KeyDownEvent) Handles txtMemberType.KeyDownEvent
        If e.keyCode = Keys.F1 Then
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblMemberType", "L", 4, "MemberTypeDescription", "", "MemberType", "MemberType", 80, "MemberTypeDescription", "MemberTypeDescription", 100, "MemberTypeDiscount", "MemberTypeDiscount", 170, "Active", "Active", 190)
            frmHelpClass.ShowDialog()
            txtMemberType.Text = frmHelpClass.Var1.Trim
        End If

    End Sub

    Private Sub txtLocation_KeyDownEvent(ByVal sender As Object, ByVal e As AxEditLib._DfpText_KeyDownEvent) Handles txtLocation.KeyDownEvent
        If e.keyCode = Keys.F1 Then
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tbllocation", "L", 4, "LocationName", "", "LocationCode", "LocationCode", 80, "LocationName", "LocationName", 180, "Address1", "Address1", 60, "Address2", "Address2", 80)
            frmHelpClass.ShowDialog()
            txtLocation.Text = frmHelpClass.Var1.Trim
        End If


    End Sub

    Private Sub txtMemberType_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMemberType.LostFocus
        Dim ds As New DataSet
        ds = objMember.SubGetMemberType(Trim(txtMemberType.Text))
        If ds.Tables(0).Rows.Count > 0 Then
            txtMemberType.Text = Trim(ds.Tables(0).Rows(0)("membertype") & "")
        Else
            txtMemberType.Text = ""
        End If
    End Sub
    Private Sub MembercodeValidate()
        Try
            Dim dsMember As New DataSet
            Dim iCnt As Long
            dsMember = objMember.GetMemberwithCode(txtMembercode.Text.Trim)
            If Not IsNothing(dsMember) Then
                If dsMember.Tables("Member").Rows.Count > 0 Then
                    If dsMember.Tables(0).Rows(0)("Membercode") <> txtMembercode.Text.Trim Then
                        MsgBox("Invalid Membercode")
                        txtMembercode.Focus()

                    End If


                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub txtDiscount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDiscount.LostFocus
        If Not IsNumeric(txtDiscount.Text) Then
            MsgBox("Discount Should be Numeric")
            txtDiscount.Focus()

        End If
    End Sub

    Private Sub txtTotalRewardPoint_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotalRewardPoint.LostFocus
        If txtTotalRewardPoint.Text.Trim <> "" Then
            If Not IsNumeric(txtTotalRewardPoint.Text) Then
                MsgBox("Total Reward Point Should be Numeric")
                txtTotalRewardPoint.Focus()
            End If
        End If
    End Sub

    Private Sub txtTotalUsagePoint_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotalUsagePoint.LostFocus
        If txtTotalUsagePoint.Text.Trim <> "" Then
            If Not IsNumeric(txtTotalUsagePoint.Text) Then
                MsgBox("Total Usage Point Should be Numeric")
                txtTotalUsagePoint.Focus()
            End If
        End If
    End Sub

    Private Sub txtNricType_ButtonHit(ByVal sender As Object, ByVal e As AxEditLib._DfpText_ButtonHitEvent) Handles txtNricType.ButtonHit
        Dim frmHelpClass As New frmHelpSearch(0, 0, "tblNricType", "L", 3, "Description", "", "NricTypeCode", "NricTypeCode", 80, "Description", "Description", 100, "Active", "Active", 190)
        frmHelpClass.ShowDialog()
        txtNricType.Text = frmHelpClass.Var1.Trim
    End Sub

    Private Sub txtNricType_KeyDownEvent(ByVal sender As Object, ByVal e As AxEditLib._DfpText_KeyDownEvent) Handles txtNricType.KeyDownEvent
        If e.keyCode = Keys.F1 Then
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblNricType", "L", 3, "Description", "", "NricTypeCode", "NricTypeCode", 80, "Description", "Description", 100, "Active", "Active", 190)
            frmHelpClass.ShowDialog()
            txtNricType.Text = frmHelpClass.Var1.Trim
        End If
    End Sub
End Class
