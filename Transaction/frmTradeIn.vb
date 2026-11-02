Imports System.Math
Public Class frmTradeIn
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
    Friend WithEvents btnCustomerCodeSearch As System.Windows.Forms.Button
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress3 As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress2 As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress1 As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerCode As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lbl As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtItemDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtStoneDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtStoneWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtGoldLoss As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalCost As System.Windows.Forms.TextBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnFClear As System.Windows.Forms.Button
    Friend WithEvents grdTradeIn As AxFPSpread.AxvaSpread
    Friend WithEvents lblS As System.Windows.Forms.Label
    Friend WithEvents lblBalanceAmount As System.Windows.Forms.Label
    Friend WithEvents lblSalesAmount As System.Windows.Forms.Label
    Friend WithEvents lblTradeInAmount As System.Windows.Forms.Label
    Friend WithEvents lblTradeInLimit As System.Windows.Forms.Label
    Friend WithEvents txtLossPerc As System.Windows.Forms.TextBox
    Friend WithEvents txtCostPerGram As System.Windows.Forms.TextBox
    Friend WithEvents txtInvoiceNo As System.Windows.Forms.TextBox
    Friend WithEvents txtCostPerGram1 As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblCustomerType As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmTradeIn))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtCustomerName = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.lblCustomerType = New System.Windows.Forms.Label
        Me.txtLossPerc = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtCostPerGram = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnCustomerCodeSearch = New System.Windows.Forms.Button
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtRemarks = New System.Windows.Forms.TextBox
        Me.txtAddress3 = New System.Windows.Forms.TextBox
        Me.txtAddress2 = New System.Windows.Forms.TextBox
        Me.txtAddress1 = New System.Windows.Forms.TextBox
        Me.txtCustomerCode = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.grdTradeIn = New AxFPSpread.AxvaSpread
        Me.lbl = New System.Windows.Forms.Label
        Me.lblS = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtItemDescription = New System.Windows.Forms.TextBox
        Me.txtStoneDescription = New System.Windows.Forms.TextBox
        Me.txtWeight = New System.Windows.Forms.TextBox
        Me.txtStoneWeight = New System.Windows.Forms.TextBox
        Me.txtGoldLoss = New System.Windows.Forms.TextBox
        Me.txtCostPerGram1 = New System.Windows.Forms.TextBox
        Me.txtTotalCost = New System.Windows.Forms.TextBox
        Me.btnAdd = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.pic = New System.Windows.Forms.PictureBox
        Me.btnUpdate = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnFClear = New System.Windows.Forms.Button
        Me.lblTradeInLimit = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.lblBalanceAmount = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.lblSalesAmount = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.lblTradeInAmount = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        CType(Me.grdTradeIn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txtCustomerName)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.lblCustomerType)
        Me.Panel1.Controls.Add(Me.txtLossPerc)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txtCostPerGram)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtInvoiceNo)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.btnCustomerCodeSearch)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtRemarks)
        Me.Panel1.Controls.Add(Me.txtAddress3)
        Me.Panel1.Controls.Add(Me.txtAddress2)
        Me.Panel1.Controls.Add(Me.txtAddress1)
        Me.Panel1.Controls.Add(Me.txtCustomerCode)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Location = New System.Drawing.Point(20, 80)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(460, 192)
        Me.Panel1.TabIndex = 0
        '
        'txtCustomerName
        '
        Me.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerName.ForeColor = System.Drawing.Color.Black
        Me.txtCustomerName.Location = New System.Drawing.Point(72, 60)
        Me.txtCustomerName.MaxLength = 20
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(376, 20)
        Me.txtCustomerName.TabIndex = 113
        Me.txtCustomerName.Tag = ""
        Me.txtCustomerName.Text = ""
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(8, 60)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(66, 20)
        Me.Label11.TabIndex = 128
        Me.Label11.Text = "Name"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCustomerType
        '
        Me.lblCustomerType.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.lblCustomerType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustomerType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerType.ForeColor = System.Drawing.Color.Red
        Me.lblCustomerType.Location = New System.Drawing.Point(200, 35)
        Me.lblCustomerType.Name = "lblCustomerType"
        Me.lblCustomerType.Size = New System.Drawing.Size(248, 20)
        Me.lblCustomerType.TabIndex = 127
        Me.lblCustomerType.Text = "Customer Type"
        Me.lblCustomerType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtLossPerc
        '
        Me.txtLossPerc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLossPerc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLossPerc.ForeColor = System.Drawing.Color.Black
        Me.txtLossPerc.Location = New System.Drawing.Point(384, 8)
        Me.txtLossPerc.MaxLength = 20
        Me.txtLossPerc.Name = "txtLossPerc"
        Me.txtLossPerc.ReadOnly = True
        Me.txtLossPerc.Size = New System.Drawing.Size(64, 20)
        Me.txtLossPerc.TabIndex = 125
        Me.txtLossPerc.Tag = ""
        Me.txtLossPerc.Text = ""
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(328, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 20)
        Me.Label3.TabIndex = 126
        Me.Label3.Text = "Loss(%)"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCostPerGram
        '
        Me.txtCostPerGram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCostPerGram.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostPerGram.ForeColor = System.Drawing.Color.Black
        Me.txtCostPerGram.Location = New System.Drawing.Point(248, 8)
        Me.txtCostPerGram.MaxLength = 20
        Me.txtCostPerGram.Name = "txtCostPerGram"
        Me.txtCostPerGram.ReadOnly = True
        Me.txtCostPerGram.Size = New System.Drawing.Size(72, 20)
        Me.txtCostPerGram.TabIndex = 123
        Me.txtCostPerGram.Tag = ""
        Me.txtCostPerGram.Text = ""
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(176, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 20)
        Me.Label2.TabIndex = 124
        Me.Label2.Text = "Cost/ Gram"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvoiceNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceNo.ForeColor = System.Drawing.Color.Black
        Me.txtInvoiceNo.Location = New System.Drawing.Point(72, 8)
        Me.txtInvoiceNo.MaxLength = 20
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(96, 20)
        Me.txtInvoiceNo.TabIndex = 121
        Me.txtInvoiceNo.Tag = ""
        Me.txtInvoiceNo.Text = ""
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(6, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 20)
        Me.Label1.TabIndex = 122
        Me.Label1.Text = "Invoice No."
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnCustomerCodeSearch
        '
        Me.btnCustomerCodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnCustomerCodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCustomerCodeSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomerCodeSearch.Location = New System.Drawing.Point(168, 35)
        Me.btnCustomerCodeSearch.Name = "btnCustomerCodeSearch"
        Me.btnCustomerCodeSearch.Size = New System.Drawing.Size(24, 20)
        Me.btnCustomerCodeSearch.TabIndex = 120
        Me.btnCustomerCodeSearch.Text = "..."
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(6, 86)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(66, 20)
        Me.Label13.TabIndex = 119
        Me.Label13.Text = "Address"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(6, 164)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 20)
        Me.Label5.TabIndex = 118
        Me.Label5.Text = "Remarks"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.Black
        Me.txtRemarks.Location = New System.Drawing.Point(72, 164)
        Me.txtRemarks.MaxLength = 20
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(376, 20)
        Me.txtRemarks.TabIndex = 117
        Me.txtRemarks.Tag = ""
        Me.txtRemarks.Text = ""
        '
        'txtAddress3
        '
        Me.txtAddress3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress3.ForeColor = System.Drawing.Color.Black
        Me.txtAddress3.Location = New System.Drawing.Point(72, 138)
        Me.txtAddress3.MaxLength = 20
        Me.txtAddress3.Name = "txtAddress3"
        Me.txtAddress3.Size = New System.Drawing.Size(376, 20)
        Me.txtAddress3.TabIndex = 116
        Me.txtAddress3.Tag = ""
        Me.txtAddress3.Text = ""
        '
        'txtAddress2
        '
        Me.txtAddress2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress2.ForeColor = System.Drawing.Color.Black
        Me.txtAddress2.Location = New System.Drawing.Point(72, 112)
        Me.txtAddress2.MaxLength = 20
        Me.txtAddress2.Name = "txtAddress2"
        Me.txtAddress2.Size = New System.Drawing.Size(376, 20)
        Me.txtAddress2.TabIndex = 115
        Me.txtAddress2.Tag = ""
        Me.txtAddress2.Text = ""
        '
        'txtAddress1
        '
        Me.txtAddress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress1.ForeColor = System.Drawing.Color.Black
        Me.txtAddress1.Location = New System.Drawing.Point(72, 86)
        Me.txtAddress1.MaxLength = 20
        Me.txtAddress1.Name = "txtAddress1"
        Me.txtAddress1.Size = New System.Drawing.Size(376, 20)
        Me.txtAddress1.TabIndex = 114
        Me.txtAddress1.Tag = ""
        Me.txtAddress1.Text = ""
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerCode.ForeColor = System.Drawing.Color.Black
        Me.txtCustomerCode.Location = New System.Drawing.Point(72, 34)
        Me.txtCustomerCode.MaxLength = 20
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.Size = New System.Drawing.Size(96, 20)
        Me.txtCustomerCode.TabIndex = 111
        Me.txtCustomerCode.Tag = ""
        Me.txtCustomerCode.Text = ""
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(6, 34)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 20)
        Me.Label6.TabIndex = 112
        Me.Label6.Text = "Customer "
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'grdTradeIn
        '
        Me.grdTradeIn.Location = New System.Drawing.Point(20, 272)
        Me.grdTradeIn.Name = "grdTradeIn"
        Me.grdTradeIn.OcxState = CType(resources.GetObject("grdTradeIn.OcxState"), System.Windows.Forms.AxHost.State)
        Me.grdTradeIn.Size = New System.Drawing.Size(588, 192)
        Me.grdTradeIn.TabIndex = 58
        '
        'lbl
        '
        Me.lbl.BackColor = System.Drawing.Color.Navy
        Me.lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl.ForeColor = System.Drawing.Color.White
        Me.lbl.Location = New System.Drawing.Point(20, 464)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(220, 32)
        Me.lbl.TabIndex = 76
        Me.lbl.Text = "Description"
        Me.lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblS
        '
        Me.lblS.BackColor = System.Drawing.Color.Navy
        Me.lblS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblS.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblS.ForeColor = System.Drawing.Color.White
        Me.lblS.Location = New System.Drawing.Point(240, 464)
        Me.lblS.Name = "lblS"
        Me.lblS.Size = New System.Drawing.Size(88, 32)
        Me.lblS.TabIndex = 77
        Me.lblS.Text = "Weight"
        Me.lblS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Navy
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(328, 464)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 32)
        Me.Label4.TabIndex = 78
        Me.Label4.Text = "Gold Loss"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Navy
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(416, 464)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(88, 32)
        Me.Label7.TabIndex = 79
        Me.Label7.Text = "Cost Per Gram"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Navy
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(504, 464)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(104, 32)
        Me.Label8.TabIndex = 80
        Me.Label8.Text = "Total Cost"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtItemDescription
        '
        Me.txtItemDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemDescription.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemDescription.ForeColor = System.Drawing.Color.Black
        Me.txtItemDescription.Location = New System.Drawing.Point(20, 496)
        Me.txtItemDescription.MaxLength = 20
        Me.txtItemDescription.Name = "txtItemDescription"
        Me.txtItemDescription.Size = New System.Drawing.Size(220, 22)
        Me.txtItemDescription.TabIndex = 116
        Me.txtItemDescription.Tag = ""
        Me.txtItemDescription.Text = ""
        '
        'txtStoneDescription
        '
        Me.txtStoneDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStoneDescription.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStoneDescription.ForeColor = System.Drawing.Color.Black
        Me.txtStoneDescription.Location = New System.Drawing.Point(20, 518)
        Me.txtStoneDescription.MaxLength = 20
        Me.txtStoneDescription.Name = "txtStoneDescription"
        Me.txtStoneDescription.Size = New System.Drawing.Size(220, 22)
        Me.txtStoneDescription.TabIndex = 117
        Me.txtStoneDescription.Tag = ""
        Me.txtStoneDescription.Text = ""
        '
        'txtWeight
        '
        Me.txtWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWeight.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeight.ForeColor = System.Drawing.Color.Black
        Me.txtWeight.Location = New System.Drawing.Point(240, 496)
        Me.txtWeight.MaxLength = 20
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.Size = New System.Drawing.Size(88, 22)
        Me.txtWeight.TabIndex = 118
        Me.txtWeight.Tag = ""
        Me.txtWeight.Text = ""
        '
        'txtStoneWeight
        '
        Me.txtStoneWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStoneWeight.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStoneWeight.ForeColor = System.Drawing.Color.Black
        Me.txtStoneWeight.Location = New System.Drawing.Point(240, 518)
        Me.txtStoneWeight.MaxLength = 20
        Me.txtStoneWeight.Name = "txtStoneWeight"
        Me.txtStoneWeight.Size = New System.Drawing.Size(88, 22)
        Me.txtStoneWeight.TabIndex = 119
        Me.txtStoneWeight.Tag = ""
        Me.txtStoneWeight.Text = ""
        '
        'txtGoldLoss
        '
        Me.txtGoldLoss.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoldLoss.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoldLoss.ForeColor = System.Drawing.Color.Black
        Me.txtGoldLoss.Location = New System.Drawing.Point(328, 496)
        Me.txtGoldLoss.MaxLength = 20
        Me.txtGoldLoss.Name = "txtGoldLoss"
        Me.txtGoldLoss.Size = New System.Drawing.Size(88, 22)
        Me.txtGoldLoss.TabIndex = 120
        Me.txtGoldLoss.Tag = ""
        Me.txtGoldLoss.Text = ""
        '
        'txtCostPerGram1
        '
        Me.txtCostPerGram1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCostPerGram1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostPerGram1.ForeColor = System.Drawing.Color.Black
        Me.txtCostPerGram1.Location = New System.Drawing.Point(416, 496)
        Me.txtCostPerGram1.MaxLength = 20
        Me.txtCostPerGram1.Name = "txtCostPerGram1"
        Me.txtCostPerGram1.Size = New System.Drawing.Size(88, 22)
        Me.txtCostPerGram1.TabIndex = 121
        Me.txtCostPerGram1.Tag = ""
        Me.txtCostPerGram1.Text = ""
        '
        'txtTotalCost
        '
        Me.txtTotalCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalCost.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalCost.ForeColor = System.Drawing.Color.Black
        Me.txtTotalCost.Location = New System.Drawing.Point(504, 496)
        Me.txtTotalCost.MaxLength = 20
        Me.txtTotalCost.Name = "txtTotalCost"
        Me.txtTotalCost.Size = New System.Drawing.Size(104, 22)
        Me.txtTotalCost.TabIndex = 122
        Me.txtTotalCost.Tag = ""
        Me.txtTotalCost.Text = ""
        '
        'btnAdd
        '
        Me.btnAdd.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnAdd.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.Image = CType(resources.GetObject("btnAdd.Image"), System.Drawing.Image)
        Me.btnAdd.Location = New System.Drawing.Point(376, 524)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.TabIndex = 123
        Me.btnAdd.Text = "Add (+)"
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.Location = New System.Drawing.Point(454, 524)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.TabIndex = 124
        Me.btnDelete.Text = "Delete"
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(532, 524)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.TabIndex = 125
        Me.btnClear.Text = "Clear"
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(0, 0)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(210, 75)
        Me.pic.TabIndex = 126
        Me.pic.TabStop = False
        '
        'btnUpdate
        '
        Me.btnUpdate.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnUpdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdate.Image = CType(resources.GetObject("btnUpdate.Image"), System.Drawing.Image)
        Me.btnUpdate.Location = New System.Drawing.Point(414, 560)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(94, 23)
        Me.btnUpdate.TabIndex = 127
        Me.btnUpdate.Text = "Update (F7)"
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(515, 560)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(92, 23)
        Me.btnClose.TabIndex = 129
        Me.btnClose.Text = "Close(Esc)"
        '
        'btnFClear
        '
        Me.btnFClear.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnFClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFClear.Image = CType(resources.GetObject("btnFClear.Image"), System.Drawing.Image)
        Me.btnFClear.Location = New System.Drawing.Point(312, 560)
        Me.btnFClear.Name = "btnFClear"
        Me.btnFClear.Size = New System.Drawing.Size(95, 23)
        Me.btnFClear.TabIndex = 128
        Me.btnFClear.Text = "Clear (F6)"
        '
        'lblTradeInLimit
        '
        Me.lblTradeInLimit.BackColor = System.Drawing.Color.Yellow
        Me.lblTradeInLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTradeInLimit.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTradeInLimit.ForeColor = System.Drawing.Color.Black
        Me.lblTradeInLimit.Location = New System.Drawing.Point(480, 248)
        Me.lblTradeInLimit.Name = "lblTradeInLimit"
        Me.lblTradeInLimit.Size = New System.Drawing.Size(128, 24)
        Me.lblTradeInLimit.TabIndex = 130
        Me.lblTradeInLimit.Text = "0.00"
        Me.lblTradeInLimit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Navy
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(480, 224)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(128, 24)
        Me.Label10.TabIndex = 131
        Me.Label10.Text = "Trade In Limit"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBalanceAmount
        '
        Me.lblBalanceAmount.BackColor = System.Drawing.Color.Yellow
        Me.lblBalanceAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBalanceAmount.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalanceAmount.ForeColor = System.Drawing.Color.Black
        Me.lblBalanceAmount.Location = New System.Drawing.Point(480, 200)
        Me.lblBalanceAmount.Name = "lblBalanceAmount"
        Me.lblBalanceAmount.Size = New System.Drawing.Size(128, 24)
        Me.lblBalanceAmount.TabIndex = 132
        Me.lblBalanceAmount.Text = "0.00"
        Me.lblBalanceAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Navy
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label12.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.White
        Me.Label12.Location = New System.Drawing.Point(480, 176)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(128, 24)
        Me.Label12.TabIndex = 133
        Me.Label12.Text = "Balance Amount"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Navy
        Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label14.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.White
        Me.Label14.Location = New System.Drawing.Point(480, 80)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(128, 24)
        Me.Label14.TabIndex = 137
        Me.Label14.Text = "Sales Amount"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSalesAmount
        '
        Me.lblSalesAmount.BackColor = System.Drawing.Color.Yellow
        Me.lblSalesAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSalesAmount.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesAmount.ForeColor = System.Drawing.Color.Black
        Me.lblSalesAmount.Location = New System.Drawing.Point(480, 104)
        Me.lblSalesAmount.Name = "lblSalesAmount"
        Me.lblSalesAmount.Size = New System.Drawing.Size(128, 24)
        Me.lblSalesAmount.TabIndex = 136
        Me.lblSalesAmount.Text = "0.00"
        Me.lblSalesAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Navy
        Me.Label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label16.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.White
        Me.Label16.Location = New System.Drawing.Point(480, 128)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(128, 24)
        Me.Label16.TabIndex = 135
        Me.Label16.Text = "Trade In Amount"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTradeInAmount
        '
        Me.lblTradeInAmount.BackColor = System.Drawing.Color.Yellow
        Me.lblTradeInAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTradeInAmount.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTradeInAmount.ForeColor = System.Drawing.Color.Black
        Me.lblTradeInAmount.Location = New System.Drawing.Point(480, 152)
        Me.lblTradeInAmount.Name = "lblTradeInAmount"
        Me.lblTradeInAmount.Size = New System.Drawing.Size(128, 24)
        Me.lblTradeInAmount.TabIndex = 134
        Me.lblTradeInAmount.Text = "0.00"
        Me.lblTradeInAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmTradeIn
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(632, 598)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.lblSalesAmount)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.lblTradeInAmount)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.lblBalanceAmount)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.lblTradeInLimit)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnFClear)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.txtTotalCost)
        Me.Controls.Add(Me.txtCostPerGram1)
        Me.Controls.Add(Me.txtGoldLoss)
        Me.Controls.Add(Me.txtStoneWeight)
        Me.Controls.Add(Me.txtWeight)
        Me.Controls.Add(Me.txtStoneDescription)
        Me.Controls.Add(Me.txtItemDescription)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblS)
        Me.Controls.Add(Me.lbl)
        Me.Controls.Add(Me.grdTradeIn)
        Me.Controls.Add(Me.Panel1)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmTradeIn"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Trade In"
        Me.Panel1.ResumeLayout(False)
        CType(Me.grdTradeIn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim sinventorycode As String
    Public sInvoiceDate As String
    Dim dCost As Double
    Dim sItemNo As Long
    Dim dLocalCost As Double
    Dim dForeignCost As Double
    Dim bEdited As Boolean
    Dim dTotalWeight, dTotalGoldLoss, dTotalMC, dTotalPCS, dTotalLocalCost, dTotalForeignCost As Double
    Dim msPurchaseType$
    Const GRDROWHT = 17.708
    Public sInvoiceNo As String
    Public sCustomerCode As String
    Public sSalesAmount As Double
    Dim sSalesType As String
    Dim msPayMode$
    Dim mbTransaction As Boolean
    Dim mdCommission As Double
    Public sCalledFrom As String

    Const ItemCode = 1
    Const ItemDescription = 2
    Const Weight = 3
    Const GoldLoss = 4
    Const CostPerGram = 5
    Const PMC = 6
    Const PCS = 7
    Const TotalCost = 8
    Const StoneDescription = 9
    Const StoneWeight = 10

    Dim objTI As New clsTradeIN
    Dim objCust As New clsCustomer
    Dim objInv As New clsInventory
    Dim objGPOS As New frmGPOS



    Private Sub frmTradeIn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim objGPOS As frmGPOS
        Try
            subGridSetting(grdTradeIn)
            dNegBalance = False
            grdTradeIn.MaxRows = 0
            lblTradeInLimit.Text = Format(dTradeinLimit, "0.00")
            'Call GoldPosGridSetting(grdTradeIn, False)
            bEdited = False
            txtInvoiceNo.Text = Trim(sInvoiceNo)
            sinventorycode = "TI-" & Trim(sInvoiceNo)
            txtCustomerCode.Text = Trim(sCustomerCode)
            GetCustomerDetail()
            lblSalesAmount.Text = Format(Val(sSalesAmount), "0.00")
            'If UCase(Trim(sCalledFrom)) = "ZERO" Then
            ' frmSalesInvoiceZero replaced with objGPOS
            If Len(objGPOS.txtCustomerName.Text) <> 0 Then
                txtCustomerName.Text = objGPOS.txtCustomerName.Text
            End If
            If Len(objGPOS.lblCustomerType.Text) <> 0 Then
                lblCustomerType.Text = objGPOS.lblCustomerType.Text
            End If
            If Len(objGPOS.txtAddress1) <> 0 Then
                txtAddress1.Text = objGPOS.txtAddress1.Text
            End If
            If Len(objGPOS.txtAddress2.Text) <> 0 Then
                txtAddress2.Text = objGPOS.txtAddress2.Text
            End If
            If Len(objGPOS.txtAddress3.Text) <> 0 Then
                txtAddress3.Text = objGPOS.txtAddress3.Text
            End If
            'Else
            'If Len(objGPOS.txtCustomerName.Text) <> 0 Then
            '    txtCustomerName.Text = objGPOS.txtCustomerName.Text
            'End If
            'If Len(objGPOS.txtAddress1.Text) <> 0 Then
            '    txtAddress1.Text = objGPOS.txtAddress1.Text
            'End If
            'If Len(objGPOS.txtAddress2.Text) <> 0 Then
            '    txtAddress2.Text = frmSalesInvoice.txtAddress2
            'End If
            'If Len(frmSalesInvoice.txtAddress3) <> 0 Then
            '    txtAddress3.Text = frmSalesInvoice.txtAddress3
            'End If
            'End If
            subGetCostPerGram()

            'SetPosPinters("BILL")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try



    End Sub

    Private Sub frmTradeIn_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F6
                btnFClear_Click(sender, e)
            Case Keys.F7
                btnUpdate_Click(sender, e)
                'Case Keys.F8
                'If btnDelete.Enabled Then btnDelete_Click(sender, e)
            Case Keys.Escape
                btnClose_Click(sender, e)
            Case Keys.Return
                SendKeys.Send("{Tab}")
            Case Keys.Add
                btnAdd_Click(sender, e)
                'Case Keys.Down
                '    SendKeys.Send("{Tab}")
                'Case Keys.Up
                'SendKeys.Send("+{Tab}")
        End Select
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            If fncValidateTradeIn() Then
                subAddGrid()
                subGetSummary()
                txtItemDescription.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If bEdited Then
                '''        'delete the inventory from the Purchase and inventory table
                '''        Set SB11 = New Purchase
                '''        SB11.PurchaseDelete (CStr(sInventoryCode))
                '''        Set SB11 = New Inventory
                '''        SB11.InventoryDelete (CStr(sInventoryCode))
                '''remove the deleted row.
                grdTradeIn.Action = FPSpread.ActionConstants.ActionDeleteRow
                grdTradeIn.MaxRows = grdTradeIn.MaxRows - 1
                If grdTradeIn.MaxRows = 1 Then
                    grdTradeIn.MaxRows = 0
                Else
                    subTotal()
                End If
                subClearDetail()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Dim objPM As New frmPayingmode

        Try
            bTradeinFlag = True
            mbTransaction = False
            If fncValidate() Then
                If (Val(lblBalanceAmount) + dTradeinLimit > 0) Then
                    dTradeinBalance = Val(lblBalanceAmount)
                    sSalesType = "TradeIn"
                    nTotalNet = Format(Val(lblBalanceAmount), "0.00")
                    'frmPayingmode.Show(vbModal)
                    objPM.Show()
                    If gPayCancel = False Then
                        If (dPaidAmount + dTradeinLimit) > 0 Then
                            If Not fncSaveTradeIn() Then
                                MessageBox.Show("Transaction not saved")
                                'TransactionRollBack(oTSalesConn)

                            Else
                                mbTransaction = True
                            End If
                            'Unload(Me)
                            Me.Close()
                        End If
                    ElseIf gPayCancel = True Then
                        Exit Sub
                    End If
                Else
                    sSalesType = "TradeIn"
                    If Not fncSaveTradeIn() Then
                        MsgBox("Transaction not saved")
                        'TransactionRollBack(oTSalesConn)
                    Else
                        mbTransaction = True
                    End If
                    'If UCase(Trim(sCalledFrom)) = "ZERO" Then
                    '    frmSalesInvoiceZero.lblReturnAmount = 0
                    'Else
                    '    frmSalesInvoice.lblReturnAmount = 0
                    'End If
                    objGPOS.lblReturnAmount.Text = 0
                    'Unload(Me)
                    Me.Close()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnFClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFClear.Click
        subClearDetail()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        subClearDetail()
    End Sub

    Private Sub txtCustomerCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCustomerCode.TextChanged

    End Sub

    Private Sub txtCustomerCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustomerCode.LostFocus

    End Sub

    Private Sub subGetCostPerGram()
        Dim dsCost As New DataSet
        Try
            txtLossPerc.Text = 18
            dsCost = objTI.GetCostPerGram()
            If DS.Tables(0).Rows.Count > 0 Then
                txtCostPerGram.Text = Val(dsCost.Tables(0).Rows(0)("Rate") & "")
                txtCostPerGram1.Text = Val(txtCostPerGram.Text) - (Val(txtCostPerGram.Text) * Val(txtLossPerc.Text) / 100)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmTradeIn_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.VisibleChanged
        subUnload()
    End Sub

    Private Sub frmTradeIn_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        subUnload()
    End Sub

    Private Sub subUnload()
        Try
            'frmSalesInvoiceZero replaced with objGPOS

            'If Not mbTransaction Then
            '    If oTSalesConn.State = 1 Then TransactionRollBack(oTSalesConn)
            'End If

            'If UCase(Trim(sCalledFrom)) = "ZERO" Then
            If Len(objGPOS.txtCustomerName.Text) = 0 And Len(txtCustomerName.Text) <> 0 Then
                objGPOS.txtCustomerName.Text = Trim(txtCustomerName.Text)
            End If

            If Len(objGPOS.lblCustomerType.Text) = 0 And Len(lblCustomerType.Text) <> 0 Then
                objGPOS.lblCustomerType.Text = Trim(lblCustomerType.Text)
            End If

            If Len(objGPOS.txtAddress1.Text) = 0 And Len(txtAddress1.Text) <> 0 Then
                objGPOS.txtAddress1.Text = Trim(txtAddress1.Text)
            End If

            If Len(objGPOS.txtAddress2.Text) = 0 And Len(txtAddress2.Text) <> 0 Then
                objGPOS.txtAddress2.Text = Trim(txtAddress2.Text)
            End If

            If Len(objGPOS.txtAddress3.Text) = 0 And Len(txtAddress3.Text) <> 0 Then
                objGPOS.txtAddress3.Text = Trim(txtAddress3.Text)
            End If

            'Else
            'If Len(frmSalesInvoice.txtCustomerName) = 0 And Len(txtCustomerName) <> 0 Then
            '    frmSalesInvoice.txtCustomerName = Trim(txtCustomerName)
            'End If
            'If Len(frmSalesInvoice.txtAddress1) = 0 And Len(txtAddress1) <> 0 Then
            '    frmSalesInvoice.txtAddress1 = Trim(txtAddress1)
            'End If
            'If Len(frmSalesInvoice.txtAddress2) = 0 And Len(txtAddress2) <> 0 Then
            '    frmSalesInvoice.txtAddress2.Text = Trim(txtAddress2)
            'End If
            'If Len(frmSalesInvoice.txtAddress3) = 0 And Len(txtAddress3) <> 0 Then
            '    frmSalesInvoice.txtAddress3 = Trim(txtAddress3)
            'End If
            'End If
            sCalledFrom = ""
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function fncValidateTradeIn() As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            If Trim(txtItemDescription.Text) = "" Then
                MsgBox("Please enter Item Description.")
                txtItemDescription.Focus()
                Exit Function
            End If

            If IsNumeric(txtWeight.Text) = False Then
                MsgBox("Weight should be numeric.")
                txtWeight.Focus()
                Exit Function
            ElseIf Val(txtWeight.Text) <= 0 Then
                MsgBox("Weight should be greater than zero.")
                txtWeight.Focus()
                Exit Function
            End If


            If Trim(txtStoneWeight.Text) <> "" Then
                If IsNumeric(txtStoneWeight.Text) = False Then
                    MsgBox("Stone Weight should be numeric.")
                    txtStoneWeight.Focus()
                    Exit Function
                ElseIf Val(txtStoneWeight) < 0 Then
                    MsgBox("Stone Weight should be greater than zero.")
                    txtStoneWeight.Focus()
                    Exit Function
                End If
            End If
            If Val(txtGoldLoss.Text) < 0 Then
                MsgBox("Gold Loss is Invalid.")
                txtGoldLoss.Focus()
                Exit Function
            End If

            If IsNumeric(txtCostPerGram1.Text) = False Then
                MsgBox("Cost per Gram should be numeric.")
                txtCostPerGram1.Focus()
                Exit Function
            ElseIf Val(txtCostPerGram1.Text) <= 0 Then
                MsgBox("Cost Per Gram should be greater than zero.")
                txtCostPerGram1.Focus()
                Exit Function
            End If
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncValidateTradeIn = bStatus
        End Try
    End Function

    Private Sub subAddGrid()
        Dim icnt As Long
        Dim dTotalCost As Double
        Dim dCost As Double
        Try
            grdTradeIn.ForeColor = Color.Black
            grdTradeIn.FontBold = False
            If Not bEdited Then
                If grdTradeIn.MaxRows = 0 Then
                    grdTradeIn.MaxRows = grdTradeIn.MaxRows + 1
                Else
                    grdTradeIn.Row = grdTradeIn.MaxRows
                    grdTradeIn.Action = 5
                    grdTradeIn.MaxRows = grdTradeIn.MaxRows - 1
                    grdTradeIn.MaxRows = grdTradeIn.MaxRows + 1
                End If

                grdTradeIn.Row = grdTradeIn.MaxRows
            Else
                grdTradeIn.Row = grdTradeIn.ActiveRow
                'grdTradeIn.RowHeight(grdTradeIn.ActiveRow) = GRDROWHT

            End If

            Call SetText(grdTradeIn, grdTradeIn.Row, ItemCode, grdTradeIn.Row)
            Call SetText(grdTradeIn, grdTradeIn.Row, ItemDescription, Trim(txtItemDescription.Text))
            Call SetText(grdTradeIn, grdTradeIn.Row, Weight, Val(txtWeight.Text))
            Call SetText(grdTradeIn, grdTradeIn.Row, GoldLoss, Val(txtGoldLoss))
            Call SetText(grdTradeIn, grdTradeIn.Row, CostPerGram, Val(txtCostPerGram1))

            'Call SetText(grdTradeIn, grdTradeIn.Row, PMC, Val(txtMC))
            'Call SetText(grdTradeIn, grdTradeIn.Row, PCS, Val(txtPCS))
            'dCost = Round(Val(txtTotalCost), 2)
            Dim sTotalCost As String
            sTotalCost = Format(Val(txtTotalCost.Text), "#0.00")
            'dCost = Mid(sTotalCost, Len(sTotalCost) - 2, 2)
            dCost = Mid(sTotalCost, Len(sTotalCost) - 2, 3)

            If dCost > 0 And dCost < 0.5 Then
                dTotalCost = Round(Val(txtTotalCost.Text)) - 1
            ElseIf dCost >= 0.5 And dCost <= 0.99 Then
                dTotalCost = Round(Val(txtTotalCost.Text))
            Else
                dTotalCost = Val(txtTotalCost.Text)
            End If


            Call SetText(grdTradeIn, grdTradeIn.Row, TotalCost, Val(dTotalCost))
            Call SetText(grdTradeIn, grdTradeIn.Row, StoneDescription, Trim(txtStoneDescription.Text))
            Call SetText(grdTradeIn, grdTradeIn.Row, StoneWeight, Val(txtStoneWeight.Text))
            grdTradeIn.Row = grdTradeIn.MaxRows
            grdTradeIn.Col = 1
            grdTradeIn.Action = 0


            subTotal()
            subClearDetail()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subTotal()
        Try
            If Not bEdited Then
                grdTradeIn.MaxRows = grdTradeIn.MaxRows + 1
                'grdTradeIn.RowHeight(grdTradeIn.MaxRows) = GRDROWHT
            End If

            Call SetText(grdTradeIn, grdTradeIn.MaxRows, 1, "Total:")

            grdTradeIn.ForeColor = Color.Red
            grdTradeIn.FontBold = True

            grdTradeIn.Col = 3
            grdTradeIn.Formula = "sum(c1:c" & grdTradeIn.MaxRows - 1 & ")"


            grdTradeIn.ForeColor = Color.Red
            grdTradeIn.FontBold = True

            grdTradeIn.Col = 4
            grdTradeIn.Formula = "sum(d1:d" & grdTradeIn.MaxRows - 1 & ")"

            grdTradeIn.ForeColor = Color.Red
            grdTradeIn.FontBold = True

            grdTradeIn.Col = 6
            grdTradeIn.Formula = "sum(f1:f" & grdTradeIn.MaxRows - 1 & ")"

            grdTradeIn.ForeColor = Color.Red
            grdTradeIn.FontBold = True

            grdTradeIn.Col = 7
            grdTradeIn.Formula = "sum(g1:g" & grdTradeIn.MaxRows - 1 & ")"

            grdTradeIn.ForeColor = Color.Red
            grdTradeIn.FontBold = True

            grdTradeIn.Col = 8
            grdTradeIn.Formula = "sum(h1:h" & grdTradeIn.MaxRows - 1 & ")"
            grdTradeIn.ForeColor = Color.Red
            grdTradeIn.FontBold = True

            grdTradeIn.Col = 10
            grdTradeIn.Formula = "sum(j1:j" & grdTradeIn.MaxRows - 1 & ")"

            grdTradeIn.ForeColor = Color.Red
            grdTradeIn.FontBold = True

            lblTradeInAmount.Text = Format(GetText(grdTradeIn, grdTradeIn.MaxRows, TotalCost), "0.00")
            lblBalanceAmount.Text = Format(Val(lblSalesAmount.Text) - Val(lblTradeInAmount.Text), "0.00")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subClearDetail()
        Try
            bEdited = False
            txtItemDescription.Clear()
            txtStoneDescription.Clear()
            txtWeight.Clear()
            txtStoneWeight.Clear()
            txtGoldLoss.Clear()
            sItemNo = 0
            txtTotalCost.Clear()
            If Val(txtCostPerGram.Text) <> 0 Then
                txtCostPerGram1.Text = Val(txtCostPerGram.Text)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subGetSummary()
        'commented
    End Sub

    Private Sub btnCustomerCodeSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomerCodeSearch.Click
        Try
            Dim frmHelpCustomer As New frmHelpSearch(0, 0, "tblCustomer", "L", 4, "CustomerName", "", "CustomerCode", "CustomerCode", 80, "CustomerName", "CustomerName", 150, "Address1", "Address1", 90, "Address2", "Address2", 80)
            frmHelpCustomer.ShowDialog()
            txtCustomerCode.Text = frmHelpCustomer.Var1.Trim
            txtCustomerName.Text = frmHelpCustomer.Var2.Trim
            GetCustomerDetail()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub GetCustomerDetail()
        Dim dsCust As New DataSet
        Try
            If txtCustomerCode.Text.Trim <> "" Then
                sCustomerCode = Trim(txtCustomerCode.Text)
                dsCust = objCust.GetCustomerwithCode(txtCustomerCode.Text.Trim)
                If dsCust.Tables(0).Rows.Count > 0 Then
                    txtCustomerName.Text = Trim(dsCust.Tables(0).Rows(0)("CustomerName") & "")
                    lblCustomerType.Text = Trim(dsCust.Tables(0).Rows(0)("CustomerType") & "")
                    txtAddress1.Text = Trim(dsCust.Tables(0).Rows(0)("Address1") & "")
                    txtAddress1.Text = Trim(dsCust.Tables(0).Rows(0)("Address2") & "")
                    txtAddress1.Text = Trim(dsCust.Tables(0).Rows(0)("Address3") & "")
                Else
                    MessageBox.Show("Customer code not valid.")
                    txtCustomerCode.Clear()
                    txtCustomerCode.Focus()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subShowDetail()
        Try
            If Trim(GetText(grdTradeIn, grdTradeIn.ActiveRow, 1)) <> "Total:" Then
                bEdited = True
                sItemNo = grdTradeIn.ActiveRow
                txtItemDescription.Text = GetText(grdTradeIn, grdTradeIn.ActiveRow, ItemDescription)
                txtStoneDescription.Text = GetText(grdTradeIn, grdTradeIn.ActiveRow, StoneDescription)
                txtWeight.Text = GetText(grdTradeIn, grdTradeIn.ActiveRow, Weight)
                txtStoneWeight.Text = GetText(grdTradeIn, grdTradeIn.ActiveRow, StoneWeight)
                txtGoldLoss.Text = GetText(grdTradeIn, grdTradeIn.ActiveRow, GoldLoss)
                txtCostPerGram1.Text = GetText(grdTradeIn, grdTradeIn.ActiveRow, CostPerGram)
                'txtMC = CDbl(Format$(IIf(Val(txtWeight) > 0, Val(GetText(grdTradeIn, grdTradeIn.ActiveRow, 7)) / Val(txtWeight), _
                'Val(GetText(grdTradeIn, grdTradeIn.ActiveRow, 7))), "##.0000"))
                'txtMC = GetText(grdTradeIn, grdTradeIn.ActiveRow, 6)
                'txtPCS = GetText(grdTradeIn, grdTradeIn.ActiveRow, 7)
                txtTotalCost.Text = GetText(grdTradeIn, grdTradeIn.ActiveRow, TotalCost)
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub grdTradeIn_DblClick(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_DblClickEvent) Handles grdTradeIn.DblClick
        subShowDetail()
    End Sub

    Private Sub txtCostPerGram_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCostPerGram.LostFocus
        Try
            If Val(txtCostPerGram) <> 0 Then
                txtCostPerGram1.Text = Val(txtCostPerGram.Text)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtCostPerGram1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCostPerGram1.LostFocus
        Try
            If Val(txtCostPerGram1.Text) <> 0 Then
                dCost = (Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram1.Text) '+ Val(txtMC)
                txtTotalCost.Text = Format(dCost, "0.00")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtGoldLoss_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGoldLoss.LostFocus
        Try
            If Val(txtGoldLoss.Text) <> 0 Then
                dCost = (Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram1.Text) '+ Val(txtMC)
                txtTotalCost.Text = Format(dCost, "0.00")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtLossPerc_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLossPerc.TextChanged
        Try
            If Val(txtLossPerc.Text) And Val(txtLossPerc.Text) < 100 Then
                txtCostPerGram1.Text = Val(txtCostPerGram.Text) - (Val(txtCostPerGram.Text) * Val(txtLossPerc.Text) / 100)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtWeight_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWeight.LostFocus
        Try
            If Val(txtWeight) <> 0 Then
                dCost = (Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram1.Text) '+ Val(txtMC)
                txtTotalCost.Text = Format(dCost, "0.00")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function fncValidate() As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            If grdTradeIn.MaxRows <= 0 Then
                MessageBox.Show("Enter trade in item details!")
                Exit Function
            End If

            If Val(lblBalanceAmount.Text) + dTradeinLimit < 0 Then
                MessageBox.Show("Balance Exceeds the Limit!")
                Exit Function
            End If
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncValidate = bStatus
        End Try
    End Function

    Private Function fncSaveTradeIn() As Boolean
        Dim dsG As New DataSet
        Dim iRet As Boolean
        Dim iCnt As Long
        Dim sSql As String
        Try
            iRet = objTI.TradeInDelete(Trim(sinventorycode))
            If iRet Then
                iRet = objTI.TradeInHeaderSave(Trim(sinventorycode), Trim$(sInvoiceNo), _
                                    gthisLocation, Trim(txtCustomerCode.Text), Trim(txtCustomerName.Text), Trim(txtAddress1.Text), _
                                    Trim(txtAddress2.Text), Trim(txtAddress3.Text), _
                                    Val(txtCostPerGram), Val(GetText(grdTradeIn, grdTradeIn.MaxRows, 3)), _
                                    Val(GetText(grdTradeIn, grdTradeIn.MaxRows, 10)), _
                                    Val(GetText(grdTradeIn, grdTradeIn.MaxRows, 6)), Val(GetText(grdTradeIn, grdTradeIn.MaxRows, 7)), _
                                    Val(GetText(grdTradeIn, grdTradeIn.MaxRows, 4)), Val(GetText(grdTradeIn, grdTradeIn.MaxRows, 8)), _
                                    Val(dPaidAmount), "TRADEIN", "N")
                If iRet Then
                    fncSaveTradeIn = False
                    Exit Function
                End If

                For iCnt = 1 To grdTradeIn.MaxRows - 1
                    iRet = objTI.TradeInDetailSave(gthisLocation, Trim(sinventorycode), iCnt, Trim(GetText(grdTradeIn, iCnt, ItemDescription)), _
                            Trim(GetText(grdTradeIn, iCnt, StoneDescription)), Val(GetText(grdTradeIn, iCnt, Weight)), Val(GetText(grdTradeIn, iCnt, StoneWeight)), _
                            Val(GetText(grdTradeIn, iCnt, CostPerGram)), Val(GetText(grdTradeIn, iCnt, PMC)), Val(GetText(grdTradeIn, iCnt, PCS)), _
                            Val(GetText(grdTradeIn, iCnt, GoldLoss)), Val(GetText(grdTradeIn, iCnt, TotalCost)), "TRADEIN", "TRADEIN", "TRADEIN", "TRADEIN")
                    If iRet Then
                        fncSaveTradeIn = False
                        Exit Function
                    End If
                Next

                'If UCase(Trim(sCalledFrom)) = "ZERO" Then
                '    frmSalesInvoiceZero.TradeInAmt = Val(GetText(grdTradeIn, grdTradeIn.MaxRows, 8))
                'Else
                '    frmSalesInvoice.TradeInAmt = Val(GetText(grdTradeIn, grdTradeIn.MaxRows, 8))
                'End If
                objGPOS.TradeInAmt(Val(GetText(grdTradeIn, grdTradeIn.MaxRows, TotalCost)))
                If iRet Then
                    fncSaveTradeIn = False
                    Exit Function
                Else
                    fncSaveTradeIn = True
                    Exit Function
                End If
            Else
                fncSaveTradeIn = False
                Exit Function
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Sub btnAdd_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.GotFocus
        Try
            txtTotalCost.Text = (Val(txtWeight.Text) + Val(txtGoldLoss.Text)) * Val(txtCostPerGram1.Text) '+ Val(txtMC)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
