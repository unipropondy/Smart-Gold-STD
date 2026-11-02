Public Class frmPayingmode
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
    Friend WithEvents lblStart As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents picPayMode0 As System.Windows.Forms.PictureBox
    Friend WithEvents picPayMode1 As System.Windows.Forms.PictureBox
    Friend WithEvents picPayMode2 As System.Windows.Forms.PictureBox
    Friend WithEvents picPayMode3 As System.Windows.Forms.PictureBox
    Friend WithEvents picPayMode4 As System.Windows.Forms.PictureBox
    Friend WithEvents picPayMode5 As System.Windows.Forms.PictureBox
    Friend WithEvents picPayMode6 As System.Windows.Forms.PictureBox
    Friend WithEvents picPayMode7 As System.Windows.Forms.PictureBox
    Friend WithEvents lblPaymode0 As System.Windows.Forms.Label
    Friend WithEvents lblPaymode1 As System.Windows.Forms.Label
    Friend WithEvents lblPaymode2 As System.Windows.Forms.Label
    Friend WithEvents lblPaymode3 As System.Windows.Forms.Label
    Friend WithEvents lblPaymode4 As System.Windows.Forms.Label
    Friend WithEvents lblPaymode5 As System.Windows.Forms.Label
    Friend WithEvents lblPaymode6 As System.Windows.Forms.Label
    Friend WithEvents lblPaymode7 As System.Windows.Forms.Label
    Friend WithEvents picPayMode8 As System.Windows.Forms.PictureBox
    Friend WithEvents lblPaymode8 As System.Windows.Forms.Label
    Friend WithEvents txtAmount0 As System.Windows.Forms.TextBox
    Friend WithEvents txtAmount1 As System.Windows.Forms.TextBox
    Friend WithEvents txtAmount2 As System.Windows.Forms.TextBox
    Friend WithEvents txtAmount3 As System.Windows.Forms.TextBox
    Friend WithEvents txtAmount4 As System.Windows.Forms.TextBox
    Friend WithEvents txtAmount5 As System.Windows.Forms.TextBox
    Friend WithEvents txtAmount6 As System.Windows.Forms.TextBox
    Friend WithEvents txtAmount7 As System.Windows.Forms.TextBox
    Friend WithEvents txtAmount8 As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents txtReturnChange As System.Windows.Forms.Label
    Friend WithEvents txtCommission As System.Windows.Forms.Label
    Friend WithEvents txtBalanceAmount As System.Windows.Forms.Label
    Friend WithEvents txtPaidAmount As System.Windows.Forms.Label
    Friend WithEvents txtTotalAmount As System.Windows.Forms.Label
    Friend WithEvents txtSlipNo0 As System.Windows.Forms.TextBox
    Friend WithEvents txtSlipNo1 As System.Windows.Forms.TextBox
    Friend WithEvents txtSlipNo2 As System.Windows.Forms.TextBox
    Friend WithEvents txtSlipNo3 As System.Windows.Forms.TextBox
    Friend WithEvents txtSlipNo4 As System.Windows.Forms.TextBox
    Friend WithEvents txtSlipNo5 As System.Windows.Forms.TextBox
    Friend WithEvents txtSlipNo7 As System.Windows.Forms.TextBox
    Friend WithEvents txtSlipNo8 As System.Windows.Forms.TextBox
    Friend WithEvents txtSlipNo6 As System.Windows.Forms.TextBox
    Friend WithEvents frameforeign As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents grdPaymode As System.Windows.Forms.DataGridView
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents cImgPayMode As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents cLblPaymode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cTxtAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cTxtSlipNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cValueExist As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Label11 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPayingmode))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.lblStart = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.picPayMode0 = New System.Windows.Forms.PictureBox()
        Me.picPayMode1 = New System.Windows.Forms.PictureBox()
        Me.picPayMode2 = New System.Windows.Forms.PictureBox()
        Me.picPayMode3 = New System.Windows.Forms.PictureBox()
        Me.picPayMode4 = New System.Windows.Forms.PictureBox()
        Me.picPayMode5 = New System.Windows.Forms.PictureBox()
        Me.picPayMode6 = New System.Windows.Forms.PictureBox()
        Me.picPayMode7 = New System.Windows.Forms.PictureBox()
        Me.lblPaymode0 = New System.Windows.Forms.Label()
        Me.lblPaymode1 = New System.Windows.Forms.Label()
        Me.lblPaymode2 = New System.Windows.Forms.Label()
        Me.lblPaymode3 = New System.Windows.Forms.Label()
        Me.lblPaymode4 = New System.Windows.Forms.Label()
        Me.lblPaymode5 = New System.Windows.Forms.Label()
        Me.lblPaymode6 = New System.Windows.Forms.Label()
        Me.lblPaymode7 = New System.Windows.Forms.Label()
        Me.picPayMode8 = New System.Windows.Forms.PictureBox()
        Me.lblPaymode8 = New System.Windows.Forms.Label()
        Me.txtAmount0 = New System.Windows.Forms.TextBox()
        Me.txtAmount1 = New System.Windows.Forms.TextBox()
        Me.txtAmount2 = New System.Windows.Forms.TextBox()
        Me.txtAmount3 = New System.Windows.Forms.TextBox()
        Me.txtAmount4 = New System.Windows.Forms.TextBox()
        Me.txtAmount5 = New System.Windows.Forms.TextBox()
        Me.txtAmount6 = New System.Windows.Forms.TextBox()
        Me.txtAmount7 = New System.Windows.Forms.TextBox()
        Me.txtAmount8 = New System.Windows.Forms.TextBox()
        Me.txtSlipNo0 = New System.Windows.Forms.TextBox()
        Me.txtSlipNo1 = New System.Windows.Forms.TextBox()
        Me.txtSlipNo2 = New System.Windows.Forms.TextBox()
        Me.txtSlipNo3 = New System.Windows.Forms.TextBox()
        Me.txtSlipNo4 = New System.Windows.Forms.TextBox()
        Me.txtSlipNo5 = New System.Windows.Forms.TextBox()
        Me.txtSlipNo6 = New System.Windows.Forms.TextBox()
        Me.txtSlipNo7 = New System.Windows.Forms.TextBox()
        Me.txtSlipNo8 = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtReturnChange = New System.Windows.Forms.Label()
        Me.txtCommission = New System.Windows.Forms.Label()
        Me.txtBalanceAmount = New System.Windows.Forms.Label()
        Me.txtPaidAmount = New System.Windows.Forms.Label()
        Me.txtTotalAmount = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.frameforeign = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.grdPaymode = New System.Windows.Forms.DataGridView()
        Me.cImgPayMode = New System.Windows.Forms.DataGridViewImageColumn()
        Me.cLblPaymode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cTxtAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cTxtSlipNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cValueExist = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel1.SuspendLayout()
        CType(Me.picPayMode0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPayMode1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPayMode2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPayMode3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPayMode4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPayMode5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPayMode6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPayMode7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPayMode8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.frameforeign.SuspendLayout()
        CType(Me.grdPaymode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblStart
        '
        Me.lblStart.BackColor = System.Drawing.Color.White
        Me.lblStart.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStart.ForeColor = System.Drawing.Color.Black
        Me.lblStart.Location = New System.Drawing.Point(224, 8)
        Me.lblStart.Name = "lblStart"
        Me.lblStart.Size = New System.Drawing.Size(16, 20)
        Me.lblStart.TabIndex = 109
        Me.lblStart.Text = "N"
        Me.lblStart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblStart.Visible = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Navy
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(20, 120)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(796, 30)
        Me.Panel1.TabIndex = 114
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Navy
        Me.Label3.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(376, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(120, 30)
        Me.Label3.TabIndex = 112
        Me.Label3.Text = "Slip No"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label3.Visible = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Navy
        Me.Label2.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(240, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(136, 30)
        Me.Label2.TabIndex = 111
        Me.Label2.Text = "Amount"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label2.Visible = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Navy
        Me.Label1.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(240, 30)
        Me.Label1.TabIndex = 110
        Me.Label1.Text = "Pay Mode"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label1.Visible = False
        '
        'picPayMode0
        '
        Me.picPayMode0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picPayMode0.Location = New System.Drawing.Point(24, 120)
        Me.picPayMode0.Name = "picPayMode0"
        Me.picPayMode0.Size = New System.Drawing.Size(80, 30)
        Me.picPayMode0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picPayMode0.TabIndex = 115
        Me.picPayMode0.TabStop = False
        Me.picPayMode0.Visible = False
        '
        'picPayMode1
        '
        Me.picPayMode1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picPayMode1.Location = New System.Drawing.Point(24, 157)
        Me.picPayMode1.Name = "picPayMode1"
        Me.picPayMode1.Size = New System.Drawing.Size(80, 30)
        Me.picPayMode1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picPayMode1.TabIndex = 116
        Me.picPayMode1.TabStop = False
        Me.picPayMode1.Visible = False
        '
        'picPayMode2
        '
        Me.picPayMode2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picPayMode2.Location = New System.Drawing.Point(24, 194)
        Me.picPayMode2.Name = "picPayMode2"
        Me.picPayMode2.Size = New System.Drawing.Size(80, 30)
        Me.picPayMode2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picPayMode2.TabIndex = 117
        Me.picPayMode2.TabStop = False
        Me.picPayMode2.Visible = False
        '
        'picPayMode3
        '
        Me.picPayMode3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picPayMode3.Location = New System.Drawing.Point(24, 231)
        Me.picPayMode3.Name = "picPayMode3"
        Me.picPayMode3.Size = New System.Drawing.Size(80, 30)
        Me.picPayMode3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picPayMode3.TabIndex = 118
        Me.picPayMode3.TabStop = False
        Me.picPayMode3.Visible = False
        '
        'picPayMode4
        '
        Me.picPayMode4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picPayMode4.Location = New System.Drawing.Point(24, 268)
        Me.picPayMode4.Name = "picPayMode4"
        Me.picPayMode4.Size = New System.Drawing.Size(80, 30)
        Me.picPayMode4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picPayMode4.TabIndex = 119
        Me.picPayMode4.TabStop = False
        Me.picPayMode4.Visible = False
        '
        'picPayMode5
        '
        Me.picPayMode5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picPayMode5.Location = New System.Drawing.Point(24, 305)
        Me.picPayMode5.Name = "picPayMode5"
        Me.picPayMode5.Size = New System.Drawing.Size(80, 30)
        Me.picPayMode5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picPayMode5.TabIndex = 120
        Me.picPayMode5.TabStop = False
        Me.picPayMode5.Visible = False
        '
        'picPayMode6
        '
        Me.picPayMode6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picPayMode6.Location = New System.Drawing.Point(24, 342)
        Me.picPayMode6.Name = "picPayMode6"
        Me.picPayMode6.Size = New System.Drawing.Size(80, 30)
        Me.picPayMode6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picPayMode6.TabIndex = 121
        Me.picPayMode6.TabStop = False
        Me.picPayMode6.Visible = False
        '
        'picPayMode7
        '
        Me.picPayMode7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picPayMode7.Location = New System.Drawing.Point(24, 379)
        Me.picPayMode7.Name = "picPayMode7"
        Me.picPayMode7.Size = New System.Drawing.Size(80, 30)
        Me.picPayMode7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picPayMode7.TabIndex = 122
        Me.picPayMode7.TabStop = False
        Me.picPayMode7.Visible = False
        '
        'lblPaymode0
        '
        Me.lblPaymode0.BackColor = System.Drawing.Color.White
        Me.lblPaymode0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPaymode0.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymode0.ForeColor = System.Drawing.Color.Black
        Me.lblPaymode0.Location = New System.Drawing.Point(112, 120)
        Me.lblPaymode0.Name = "lblPaymode0"
        Me.lblPaymode0.Size = New System.Drawing.Size(144, 30)
        Me.lblPaymode0.TabIndex = 123
        Me.lblPaymode0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblPaymode0.Visible = False
        '
        'lblPaymode1
        '
        Me.lblPaymode1.BackColor = System.Drawing.Color.White
        Me.lblPaymode1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPaymode1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymode1.ForeColor = System.Drawing.Color.Black
        Me.lblPaymode1.Location = New System.Drawing.Point(112, 157)
        Me.lblPaymode1.Name = "lblPaymode1"
        Me.lblPaymode1.Size = New System.Drawing.Size(144, 30)
        Me.lblPaymode1.TabIndex = 124
        Me.lblPaymode1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblPaymode1.Visible = False
        '
        'lblPaymode2
        '
        Me.lblPaymode2.BackColor = System.Drawing.Color.White
        Me.lblPaymode2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPaymode2.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymode2.ForeColor = System.Drawing.Color.Black
        Me.lblPaymode2.Location = New System.Drawing.Point(112, 194)
        Me.lblPaymode2.Name = "lblPaymode2"
        Me.lblPaymode2.Size = New System.Drawing.Size(144, 30)
        Me.lblPaymode2.TabIndex = 125
        Me.lblPaymode2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblPaymode2.Visible = False
        '
        'lblPaymode3
        '
        Me.lblPaymode3.BackColor = System.Drawing.Color.White
        Me.lblPaymode3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPaymode3.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymode3.ForeColor = System.Drawing.Color.Black
        Me.lblPaymode3.Location = New System.Drawing.Point(112, 231)
        Me.lblPaymode3.Name = "lblPaymode3"
        Me.lblPaymode3.Size = New System.Drawing.Size(144, 30)
        Me.lblPaymode3.TabIndex = 126
        Me.lblPaymode3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblPaymode3.Visible = False
        '
        'lblPaymode4
        '
        Me.lblPaymode4.BackColor = System.Drawing.Color.White
        Me.lblPaymode4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPaymode4.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymode4.ForeColor = System.Drawing.Color.Black
        Me.lblPaymode4.Location = New System.Drawing.Point(112, 268)
        Me.lblPaymode4.Name = "lblPaymode4"
        Me.lblPaymode4.Size = New System.Drawing.Size(144, 30)
        Me.lblPaymode4.TabIndex = 127
        Me.lblPaymode4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblPaymode4.Visible = False
        '
        'lblPaymode5
        '
        Me.lblPaymode5.BackColor = System.Drawing.Color.White
        Me.lblPaymode5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPaymode5.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymode5.ForeColor = System.Drawing.Color.Black
        Me.lblPaymode5.Location = New System.Drawing.Point(112, 305)
        Me.lblPaymode5.Name = "lblPaymode5"
        Me.lblPaymode5.Size = New System.Drawing.Size(144, 30)
        Me.lblPaymode5.TabIndex = 128
        Me.lblPaymode5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblPaymode5.Visible = False
        '
        'lblPaymode6
        '
        Me.lblPaymode6.BackColor = System.Drawing.Color.White
        Me.lblPaymode6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPaymode6.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymode6.ForeColor = System.Drawing.Color.Black
        Me.lblPaymode6.Location = New System.Drawing.Point(112, 342)
        Me.lblPaymode6.Name = "lblPaymode6"
        Me.lblPaymode6.Size = New System.Drawing.Size(144, 30)
        Me.lblPaymode6.TabIndex = 129
        Me.lblPaymode6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblPaymode6.Visible = False
        '
        'lblPaymode7
        '
        Me.lblPaymode7.BackColor = System.Drawing.Color.White
        Me.lblPaymode7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPaymode7.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymode7.ForeColor = System.Drawing.Color.Black
        Me.lblPaymode7.Location = New System.Drawing.Point(112, 379)
        Me.lblPaymode7.Name = "lblPaymode7"
        Me.lblPaymode7.Size = New System.Drawing.Size(144, 30)
        Me.lblPaymode7.TabIndex = 130
        Me.lblPaymode7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblPaymode7.Visible = False
        '
        'picPayMode8
        '
        Me.picPayMode8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picPayMode8.Location = New System.Drawing.Point(24, 416)
        Me.picPayMode8.Name = "picPayMode8"
        Me.picPayMode8.Size = New System.Drawing.Size(80, 30)
        Me.picPayMode8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picPayMode8.TabIndex = 131
        Me.picPayMode8.TabStop = False
        Me.picPayMode8.Visible = False
        '
        'lblPaymode8
        '
        Me.lblPaymode8.BackColor = System.Drawing.Color.White
        Me.lblPaymode8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPaymode8.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymode8.ForeColor = System.Drawing.Color.Black
        Me.lblPaymode8.Location = New System.Drawing.Point(112, 416)
        Me.lblPaymode8.Name = "lblPaymode8"
        Me.lblPaymode8.Size = New System.Drawing.Size(144, 30)
        Me.lblPaymode8.TabIndex = 132
        Me.lblPaymode8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblPaymode8.Visible = False
        '
        'txtAmount0
        '
        Me.txtAmount0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmount0.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount0.ForeColor = System.Drawing.Color.Black
        Me.txtAmount0.Location = New System.Drawing.Point(264, 120)
        Me.txtAmount0.MaxLength = 20
        Me.txtAmount0.Name = "txtAmount0"
        Me.txtAmount0.Size = New System.Drawing.Size(128, 30)
        Me.txtAmount0.TabIndex = 133
        Me.txtAmount0.Tag = ""
        Me.txtAmount0.Visible = False
        '
        'txtAmount1
        '
        Me.txtAmount1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmount1.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount1.ForeColor = System.Drawing.Color.Black
        Me.txtAmount1.Location = New System.Drawing.Point(264, 157)
        Me.txtAmount1.MaxLength = 20
        Me.txtAmount1.Name = "txtAmount1"
        Me.txtAmount1.Size = New System.Drawing.Size(128, 30)
        Me.txtAmount1.TabIndex = 135
        Me.txtAmount1.Tag = ""
        Me.txtAmount1.Visible = False
        '
        'txtAmount2
        '
        Me.txtAmount2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmount2.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount2.ForeColor = System.Drawing.Color.Black
        Me.txtAmount2.Location = New System.Drawing.Point(264, 194)
        Me.txtAmount2.MaxLength = 20
        Me.txtAmount2.Name = "txtAmount2"
        Me.txtAmount2.Size = New System.Drawing.Size(128, 30)
        Me.txtAmount2.TabIndex = 136
        Me.txtAmount2.Tag = ""
        Me.txtAmount2.Visible = False
        '
        'txtAmount3
        '
        Me.txtAmount3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmount3.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount3.ForeColor = System.Drawing.Color.Black
        Me.txtAmount3.Location = New System.Drawing.Point(264, 231)
        Me.txtAmount3.MaxLength = 20
        Me.txtAmount3.Name = "txtAmount3"
        Me.txtAmount3.Size = New System.Drawing.Size(128, 30)
        Me.txtAmount3.TabIndex = 137
        Me.txtAmount3.Tag = ""
        Me.txtAmount3.Visible = False
        '
        'txtAmount4
        '
        Me.txtAmount4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmount4.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount4.ForeColor = System.Drawing.Color.Black
        Me.txtAmount4.Location = New System.Drawing.Point(264, 268)
        Me.txtAmount4.MaxLength = 20
        Me.txtAmount4.Name = "txtAmount4"
        Me.txtAmount4.Size = New System.Drawing.Size(128, 30)
        Me.txtAmount4.TabIndex = 138
        Me.txtAmount4.Tag = ""
        Me.txtAmount4.Visible = False
        '
        'txtAmount5
        '
        Me.txtAmount5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmount5.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount5.ForeColor = System.Drawing.Color.Black
        Me.txtAmount5.Location = New System.Drawing.Point(264, 305)
        Me.txtAmount5.MaxLength = 20
        Me.txtAmount5.Name = "txtAmount5"
        Me.txtAmount5.Size = New System.Drawing.Size(128, 30)
        Me.txtAmount5.TabIndex = 139
        Me.txtAmount5.Tag = ""
        Me.txtAmount5.Visible = False
        '
        'txtAmount6
        '
        Me.txtAmount6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmount6.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount6.ForeColor = System.Drawing.Color.Black
        Me.txtAmount6.Location = New System.Drawing.Point(264, 342)
        Me.txtAmount6.MaxLength = 20
        Me.txtAmount6.Name = "txtAmount6"
        Me.txtAmount6.Size = New System.Drawing.Size(128, 30)
        Me.txtAmount6.TabIndex = 140
        Me.txtAmount6.Tag = ""
        Me.txtAmount6.Visible = False
        '
        'txtAmount7
        '
        Me.txtAmount7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmount7.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount7.ForeColor = System.Drawing.Color.Black
        Me.txtAmount7.Location = New System.Drawing.Point(264, 379)
        Me.txtAmount7.MaxLength = 20
        Me.txtAmount7.Name = "txtAmount7"
        Me.txtAmount7.Size = New System.Drawing.Size(128, 30)
        Me.txtAmount7.TabIndex = 141
        Me.txtAmount7.Tag = ""
        Me.txtAmount7.Visible = False
        '
        'txtAmount8
        '
        Me.txtAmount8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmount8.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount8.ForeColor = System.Drawing.Color.Black
        Me.txtAmount8.Location = New System.Drawing.Point(264, 416)
        Me.txtAmount8.MaxLength = 20
        Me.txtAmount8.Name = "txtAmount8"
        Me.txtAmount8.Size = New System.Drawing.Size(128, 30)
        Me.txtAmount8.TabIndex = 142
        Me.txtAmount8.Tag = ""
        Me.txtAmount8.Visible = False
        '
        'txtSlipNo0
        '
        Me.txtSlipNo0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNo0.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipNo0.ForeColor = System.Drawing.Color.Black
        Me.txtSlipNo0.Location = New System.Drawing.Point(400, 120)
        Me.txtSlipNo0.MaxLength = 20
        Me.txtSlipNo0.Name = "txtSlipNo0"
        Me.txtSlipNo0.Size = New System.Drawing.Size(112, 30)
        Me.txtSlipNo0.TabIndex = 143
        Me.txtSlipNo0.Tag = ""
        Me.txtSlipNo0.Visible = False
        '
        'txtSlipNo1
        '
        Me.txtSlipNo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNo1.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipNo1.ForeColor = System.Drawing.Color.Black
        Me.txtSlipNo1.Location = New System.Drawing.Point(400, 157)
        Me.txtSlipNo1.MaxLength = 20
        Me.txtSlipNo1.Name = "txtSlipNo1"
        Me.txtSlipNo1.Size = New System.Drawing.Size(112, 30)
        Me.txtSlipNo1.TabIndex = 144
        Me.txtSlipNo1.Tag = ""
        Me.txtSlipNo1.Visible = False
        '
        'txtSlipNo2
        '
        Me.txtSlipNo2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNo2.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipNo2.ForeColor = System.Drawing.Color.Black
        Me.txtSlipNo2.Location = New System.Drawing.Point(400, 194)
        Me.txtSlipNo2.MaxLength = 20
        Me.txtSlipNo2.Name = "txtSlipNo2"
        Me.txtSlipNo2.Size = New System.Drawing.Size(112, 30)
        Me.txtSlipNo2.TabIndex = 145
        Me.txtSlipNo2.Tag = ""
        Me.txtSlipNo2.Visible = False
        '
        'txtSlipNo3
        '
        Me.txtSlipNo3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNo3.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipNo3.ForeColor = System.Drawing.Color.Black
        Me.txtSlipNo3.Location = New System.Drawing.Point(400, 231)
        Me.txtSlipNo3.MaxLength = 20
        Me.txtSlipNo3.Name = "txtSlipNo3"
        Me.txtSlipNo3.Size = New System.Drawing.Size(112, 30)
        Me.txtSlipNo3.TabIndex = 146
        Me.txtSlipNo3.Tag = ""
        Me.txtSlipNo3.Visible = False
        '
        'txtSlipNo4
        '
        Me.txtSlipNo4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNo4.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipNo4.ForeColor = System.Drawing.Color.Black
        Me.txtSlipNo4.Location = New System.Drawing.Point(400, 268)
        Me.txtSlipNo4.MaxLength = 20
        Me.txtSlipNo4.Name = "txtSlipNo4"
        Me.txtSlipNo4.Size = New System.Drawing.Size(112, 30)
        Me.txtSlipNo4.TabIndex = 147
        Me.txtSlipNo4.Tag = ""
        Me.txtSlipNo4.Visible = False
        '
        'txtSlipNo5
        '
        Me.txtSlipNo5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNo5.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipNo5.ForeColor = System.Drawing.Color.Black
        Me.txtSlipNo5.Location = New System.Drawing.Point(400, 305)
        Me.txtSlipNo5.MaxLength = 20
        Me.txtSlipNo5.Name = "txtSlipNo5"
        Me.txtSlipNo5.Size = New System.Drawing.Size(112, 30)
        Me.txtSlipNo5.TabIndex = 148
        Me.txtSlipNo5.Tag = ""
        Me.txtSlipNo5.Visible = False
        '
        'txtSlipNo6
        '
        Me.txtSlipNo6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNo6.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipNo6.ForeColor = System.Drawing.Color.Black
        Me.txtSlipNo6.Location = New System.Drawing.Point(400, 342)
        Me.txtSlipNo6.MaxLength = 20
        Me.txtSlipNo6.Name = "txtSlipNo6"
        Me.txtSlipNo6.Size = New System.Drawing.Size(112, 30)
        Me.txtSlipNo6.TabIndex = 149
        Me.txtSlipNo6.Tag = ""
        Me.txtSlipNo6.Visible = False
        '
        'txtSlipNo7
        '
        Me.txtSlipNo7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNo7.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipNo7.ForeColor = System.Drawing.Color.Black
        Me.txtSlipNo7.Location = New System.Drawing.Point(400, 379)
        Me.txtSlipNo7.MaxLength = 20
        Me.txtSlipNo7.Name = "txtSlipNo7"
        Me.txtSlipNo7.Size = New System.Drawing.Size(112, 30)
        Me.txtSlipNo7.TabIndex = 150
        Me.txtSlipNo7.Tag = ""
        Me.txtSlipNo7.Visible = False
        '
        'txtSlipNo8
        '
        Me.txtSlipNo8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNo8.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipNo8.ForeColor = System.Drawing.Color.Black
        Me.txtSlipNo8.Location = New System.Drawing.Point(400, 416)
        Me.txtSlipNo8.MaxLength = 20
        Me.txtSlipNo8.Name = "txtSlipNo8"
        Me.txtSlipNo8.Size = New System.Drawing.Size(112, 30)
        Me.txtSlipNo8.TabIndex = 151
        Me.txtSlipNo8.Tag = ""
        Me.txtSlipNo8.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.txtReturnChange)
        Me.Panel2.Controls.Add(Me.txtCommission)
        Me.Panel2.Controls.Add(Me.txtBalanceAmount)
        Me.Panel2.Controls.Add(Me.txtPaidAmount)
        Me.Panel2.Controls.Add(Me.txtTotalAmount)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Location = New System.Drawing.Point(536, 157)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(281, 160)
        Me.Panel2.TabIndex = 152
        '
        'txtReturnChange
        '
        Me.txtReturnChange.BackColor = System.Drawing.Color.Yellow
        Me.txtReturnChange.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReturnChange.ForeColor = System.Drawing.Color.Black
        Me.txtReturnChange.Location = New System.Drawing.Point(141, 126)
        Me.txtReturnChange.Name = "txtReturnChange"
        Me.txtReturnChange.Size = New System.Drawing.Size(133, 30)
        Me.txtReturnChange.TabIndex = 122
        Me.txtReturnChange.Text = "0.00"
        Me.txtReturnChange.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCommission
        '
        Me.txtCommission.BackColor = System.Drawing.Color.Yellow
        Me.txtCommission.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCommission.ForeColor = System.Drawing.Color.Black
        Me.txtCommission.Location = New System.Drawing.Point(141, 95)
        Me.txtCommission.Name = "txtCommission"
        Me.txtCommission.Size = New System.Drawing.Size(133, 30)
        Me.txtCommission.TabIndex = 121
        Me.txtCommission.Text = "0.00"
        Me.txtCommission.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBalanceAmount
        '
        Me.txtBalanceAmount.BackColor = System.Drawing.Color.Yellow
        Me.txtBalanceAmount.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBalanceAmount.ForeColor = System.Drawing.Color.Black
        Me.txtBalanceAmount.Location = New System.Drawing.Point(141, 64)
        Me.txtBalanceAmount.Name = "txtBalanceAmount"
        Me.txtBalanceAmount.Size = New System.Drawing.Size(133, 30)
        Me.txtBalanceAmount.TabIndex = 120
        Me.txtBalanceAmount.Text = "0.00"
        Me.txtBalanceAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPaidAmount
        '
        Me.txtPaidAmount.BackColor = System.Drawing.Color.Yellow
        Me.txtPaidAmount.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaidAmount.ForeColor = System.Drawing.Color.Black
        Me.txtPaidAmount.Location = New System.Drawing.Point(141, 33)
        Me.txtPaidAmount.Name = "txtPaidAmount"
        Me.txtPaidAmount.Size = New System.Drawing.Size(133, 30)
        Me.txtPaidAmount.TabIndex = 119
        Me.txtPaidAmount.Text = "0.00"
        Me.txtPaidAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTotalAmount
        '
        Me.txtTotalAmount.BackColor = System.Drawing.Color.Yellow
        Me.txtTotalAmount.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalAmount.ForeColor = System.Drawing.Color.Black
        Me.txtTotalAmount.Location = New System.Drawing.Point(141, 2)
        Me.txtTotalAmount.Name = "txtTotalAmount"
        Me.txtTotalAmount.Size = New System.Drawing.Size(133, 30)
        Me.txtTotalAmount.TabIndex = 118
        Me.txtTotalAmount.Text = "0.00"
        Me.txtTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Navy
        Me.Label8.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(5, 126)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(133, 30)
        Me.Label8.TabIndex = 117
        Me.Label8.Text = "Return"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Navy
        Me.Label6.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(6, 95)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(132, 30)
        Me.Label6.TabIndex = 116
        Me.Label6.Text = "Commission"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Navy
        Me.Label7.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(6, 64)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(132, 30)
        Me.Label7.TabIndex = 115
        Me.Label7.Text = "Balance"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Navy
        Me.Label5.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(5, 33)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(133, 30)
        Me.Label5.TabIndex = 114
        Me.Label5.Text = "Paid"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Navy
        Me.Label4.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(5, 2)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(133, 30)
        Me.Label4.TabIndex = 113
        Me.Label4.Text = "Total"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(680, 325)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(140, 65)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Cancel (Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.Location = New System.Drawing.Point(528, 325)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(140, 65)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "Save(F7)"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'frameforeign
        '
        Me.frameforeign.Controls.Add(Me.Label11)
        Me.frameforeign.Controls.Add(Me.Label10)
        Me.frameforeign.Controls.Add(Me.Label9)
        Me.frameforeign.Location = New System.Drawing.Point(520, 416)
        Me.frameforeign.Name = "frameforeign"
        Me.frameforeign.Size = New System.Drawing.Size(320, 152)
        Me.frameforeign.TabIndex = 156
        Me.frameforeign.Visible = False
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Navy
        Me.Label11.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.White
        Me.Label11.Location = New System.Drawing.Point(200, 112)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(80, 30)
        Me.Label11.TabIndex = 160
        Me.Label11.Text = "Clear"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Navy
        Me.Label10.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(112, 112)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(80, 30)
        Me.Label10.TabIndex = 159
        Me.Label10.Text = "Save"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Navy
        Me.Label9.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.Location = New System.Drawing.Point(24, 112)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(80, 30)
        Me.Label9.TabIndex = 158
        Me.Label9.Text = "Exit"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'grdPaymode
        '
        Me.grdPaymode.AllowUserToAddRows = False
        Me.grdPaymode.AllowUserToDeleteRows = False
        Me.grdPaymode.AllowUserToResizeColumns = False
        Me.grdPaymode.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdPaymode.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdPaymode.ColumnHeadersHeight = 25
        Me.grdPaymode.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cImgPayMode, Me.cLblPaymode, Me.cTxtAmount, Me.cTxtSlipNo, Me.cValueExist})
        Me.grdPaymode.Location = New System.Drawing.Point(21, 157)
        Me.grdPaymode.Name = "grdPaymode"
        Me.grdPaymode.RowHeadersVisible = False
        Me.grdPaymode.Size = New System.Drawing.Size(496, 385)
        Me.grdPaymode.TabIndex = 157
        '
        'cImgPayMode
        '
        Me.cImgPayMode.HeaderText = "Image"
        Me.cImgPayMode.Name = "cImgPayMode"
        Me.cImgPayMode.Visible = False
        Me.cImgPayMode.Width = 60
        '
        'cLblPaymode
        '
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cLblPaymode.DefaultCellStyle = DataGridViewCellStyle2
        Me.cLblPaymode.HeaderText = "Paymode"
        Me.cLblPaymode.Name = "cLblPaymode"
        Me.cLblPaymode.Width = 150
        '
        'cTxtAmount
        '
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cTxtAmount.DefaultCellStyle = DataGridViewCellStyle3
        Me.cTxtAmount.HeaderText = "Amount"
        Me.cTxtAmount.Name = "cTxtAmount"
        Me.cTxtAmount.Width = 150
        '
        'cTxtSlipNo
        '
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cTxtSlipNo.DefaultCellStyle = DataGridViewCellStyle4
        Me.cTxtSlipNo.HeaderText = "SlipNo"
        Me.cTxtSlipNo.Name = "cTxtSlipNo"
        Me.cTxtSlipNo.Width = 150
        '
        'cValueExist
        '
        Me.cValueExist.HeaderText = "valueExist"
        Me.cValueExist.Name = "cValueExist"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(21, 8)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(270, 100)
        Me.PictureBox1.TabIndex = 158
        Me.PictureBox1.TabStop = False
        '
        'frmPayingmode
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(851, 554)
        Me.ControlBox = False
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.grdPaymode)
        Me.Controls.Add(Me.frameforeign)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.txtSlipNo8)
        Me.Controls.Add(Me.txtSlipNo7)
        Me.Controls.Add(Me.txtSlipNo6)
        Me.Controls.Add(Me.txtSlipNo5)
        Me.Controls.Add(Me.txtSlipNo4)
        Me.Controls.Add(Me.txtSlipNo3)
        Me.Controls.Add(Me.txtSlipNo2)
        Me.Controls.Add(Me.txtSlipNo1)
        Me.Controls.Add(Me.txtSlipNo0)
        Me.Controls.Add(Me.txtAmount8)
        Me.Controls.Add(Me.txtAmount7)
        Me.Controls.Add(Me.txtAmount6)
        Me.Controls.Add(Me.txtAmount5)
        Me.Controls.Add(Me.txtAmount4)
        Me.Controls.Add(Me.txtAmount3)
        Me.Controls.Add(Me.txtAmount2)
        Me.Controls.Add(Me.txtAmount1)
        Me.Controls.Add(Me.txtAmount0)
        Me.Controls.Add(Me.lblPaymode8)
        Me.Controls.Add(Me.picPayMode8)
        Me.Controls.Add(Me.lblPaymode7)
        Me.Controls.Add(Me.lblPaymode6)
        Me.Controls.Add(Me.lblPaymode5)
        Me.Controls.Add(Me.lblPaymode4)
        Me.Controls.Add(Me.lblPaymode3)
        Me.Controls.Add(Me.lblPaymode2)
        Me.Controls.Add(Me.lblPaymode1)
        Me.Controls.Add(Me.lblPaymode0)
        Me.Controls.Add(Me.picPayMode7)
        Me.Controls.Add(Me.picPayMode6)
        Me.Controls.Add(Me.picPayMode5)
        Me.Controls.Add(Me.picPayMode4)
        Me.Controls.Add(Me.picPayMode3)
        Me.Controls.Add(Me.picPayMode2)
        Me.Controls.Add(Me.picPayMode1)
        Me.Controls.Add(Me.picPayMode0)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lblStart)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPayingmode"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Paying Mode"
        Me.Panel1.ResumeLayout(False)
        CType(Me.picPayMode0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPayMode1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPayMode2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPayMode3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPayMode4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPayMode5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPayMode6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPayMode7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPayMode8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.frameforeign.ResumeLayout(False)
        CType(Me.grdPaymode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Dim mdCommission#
    Dim msSalesType As String
    Dim bForeign As Boolean
    Dim iCnt As Long
    Dim IndValue As Long
    Dim iTCnt As Long
    Dim bAmountFlag As Boolean
    Dim sSalesType As String
    Dim sStart As String
    Dim RecCount As Integer
    Dim objPay As New clsPayMode
    Dim objGPOS As New frmGPOS
    Dim objInstallementEntry As New frmInstallment_AJ
    Dim sLoad As String
    Const ImgPaymode = 1
    Const LblPaymode = 2
    Const TxtAmount = 3
    Const TxtSlipNo = 4
    Public sCustomerType As String
    Public sCustomerCode As String
    Public sRedeemSalesType As String
    Public dAdvancePaidAmount As Double
    Public dTradeInPaidAmount As Double
    Public sOrderRefNo As String
    Dim dRewardPoints As Integer
    Dim dRedeemPoints As Integer
    Dim dRedeemedPoints As Integer
    Dim sSalesReturnNo As String
    Dim dDepositAmount As Double
    Dim dSalesRetTradeIn As Double

    Private Sub frmPayingmode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            dPayModeCommission = 0
            dSalesRetTradeIn = 0
            SubDisplaytotal()
            subLoadPayMode()
            SubLoadCustomerRewardPoints()
            SubLoadCustomerRedeemedPoints()
            SubLoadDeposit()
            SubLoadTradeINDeposit()
            bForeign = False
            iTCnt = Me.Height
            bAmountFlag = False
            'If grdPaymode.MaxRows > 0 Then
            '    If Val(dAdvancePaidAmount) = 0 Then
            '        grdPaymode.Col = TxtAmount
            '        grdPaymode.Row = 1
            '        grdPaymode.Text = Format(nTotalNet, "0.00")
            '        grdPaymode.Action = FPSpread.ActionConstants.ActionActiveCell
            '        If (bTradeinFlag = True And dTradeinBalance > 0) Then
            '            grdPaymode.Col = TxtAmount
            '            grdPaymode.Row = 4
            '            grdPaymode.Text = Format(dTradeInSales, "0.00")
            '            grdPaymode.Action = FPSpread.ActionConstants.ActionActiveCell
            '        End If
            '        sLoad = "1"
            '    Else
            '        grdPaymode.Col = TxtAmount
            '        grdPaymode.Row = 1
            '        grdPaymode.Text = Format((nTotalNet - dAdvancePaidAmount), "0.00")
            '        grdPaymode.Action = FPSpread.ActionConstants.ActionActiveCell
            '        grdPaymode.Row = grdPaymode.MaxRows
            '        grdPaymode.Col = 3
            '        grdPaymode.Text = Format(dAdvancePaidAmount, "0.00")
            '        grdPaymode.Lock = True
            '        grdPaymode.Col = 4
            '        grdPaymode.Text = sOrderRefNo
            '        grdPaymode.Lock = True
            '    End If
            'End If
            '   grdPaymode.Height = (grdPaymode.RowCount * 33.33)

            If Trim(sCompanyShortCode) = "AJ" Then
                fnSetNumeric(grdPaymode, 0, cTxtAmount.Index, txtTotalAmount.Text)

                fnSetNumeric(grdPaymode, 3, cTxtAmount.Index, Format(dTradeInSales, "0.00"))
                fnSetdgvText(grdPaymode, 3, cTxtSlipNo.Index, sSalesReturnNo)
                fnSetNumeric(grdPaymode, 10, cTxtAmount.Index, Format(dDepositAmount, "0.00"))

                fnSetdgvText(grdPaymode, 10, cTxtSlipNo.Index, sSalesReturnNo)
                fnSetNumeric(grdPaymode, 13, cTxtAmount.Index, Format(dFlexiFinalAmount, "0.00"))
                fnSetdgvText(grdPaymode, 13, cTxtSlipNo.Index, sSubscriptionId)
                fnSetNumeric(grdPaymode, 14, cTxtAmount.Index, Format(dFlexiFinalBonus, "0.00"))
                fnSetNumeric(grdPaymode, 15, cTxtAmount.Index, Format(dAdvDepositAmount, "0.00"))
                fnSetdgvText(grdPaymode, 15, cTxtSlipNo.Index, sAdvDepositNo)
                subCalcTotal()
                If bTradeinFlag = True Then
                    txtTotalAmount.Text = Format(nTotalNet + dTradeInSales - dSalesRetTradeIn, "0.00")
                Else
                    txtTotalAmount.Text = Format(nTotalNet, "0.00")
                End If
            Else
                fnSetNumeric(grdPaymode, 0, cTxtAmount.Index, txtTotalAmount.Text)

                'fnSetNumeric(grdPaymode, 6, cTxtAmount.Index, Format(dDepositAmount, "0.00"))

                'fnSetdgvText(grdPaymode, 6, cTxtSlipNo.Index, sSalesReturnNo)
                'fnSetNumeric(grdPaymode, 7, cTxtAmount.Index, Format(dAdvDepositAmount, "0.00"))
                'fnSetdgvText(grdPaymode, 7, cTxtSlipNo.Index, sAdvDepositNo)
                fnSetNumeric(grdPaymode, 3, cTxtAmount.Index, Format(dTradeInSales, "0.00"))

                If Trim(sCompanyShortCode) = "MJ" Then
                    'fnSetNumeric(grdPaymode, 8, cTxtAmount.Index, Format(dAdvDepositAmount, "0.00"))
                    fnSetNumeric(grdPaymode, 7, cTxtAmount.Index, Format(dDepositAmount, "0.00"))
                    'Shabeer20220714  fnSetdgvText(grdPaymode, 10, cTxtSlipNo.Index, sSalesReturnNo)
                End If

                'fnSetNumeric(grdPaymode, 10, cTxtAmount.Index, Format(dDepositAmount, "0.00"))

                'fnSetdgvText(grdPaymode, 10, cTxtSlipNo.Index, sSalesReturnNo)
                'fnSetNumeric(grdPaymode, 13, cTxtAmount.Index, Format(dFlexiFinalAmount, "0.00"))
                'fnSetdgvText(grdPaymode, 13, cTxtSlipNo.Index, sSubscriptionId)
                'fnSetNumeric(grdPaymode, 14, cTxtAmount.Index, Format(dFlexiFinalBonus, "0.00"))
                'fnSetNumeric(grdPaymode, 15, cTxtAmount.Index, Format(dAdvDepositAmount, "0.00"))
                'fnSetdgvText(grdPaymode, 15, cTxtSlipNo.Index, sAdvDepositNo)
                subCalcTotal()
                If bTradeinFlag = True Then
                    txtTotalAmount.Text = Format(nTotalNet + dTradeInSales, "0.00")
                Else
                    txtTotalAmount.Text = Format(nTotalNet, "0.00")
                End If
                txtBalanceAmount.Text = nTotalNet
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subCalcTotal()
        Dim iRow As Long
        Try
            txtPaidAmount.Text = 0
            txtBalanceAmount.Text = 0
            For iRow = 0 To grdPaymode.RowCount - 1
                'fnSetNumeric(dgvPaymode, iRow, cAmount.Index, Val(fnGetText(dgvPaymode, iRow, cAmount.Index)))
                If Val(fnGetdgvText(grdPaymode, grdPaymode.CurrentRow.Index, cTxtAmount.Index)) > 0 Then
                    fnSetdgvText(grdPaymode, grdPaymode.CurrentRow.Index, cValueExist.Index, True)
                Else
                    fnSetdgvText(grdPaymode, grdPaymode.CurrentRow.Index, cValueExist.Index, False)
                End If
                txtPaidAmount.Text = Val(txtPaidAmount.Text) + Val(fnGetdgvText(grdPaymode, iRow, cTxtAmount.Index))
                txtBalanceAmount.Text = Val(txtTotalAmount.Text) - Val(txtPaidAmount.Text)
            Next iRow
            txtPaidAmount.Text = FormatNumber(txtPaidAmount.Text, 2)
            txtBalanceAmount.Text = -FormatNumber(txtBalanceAmount.Text, 2)
            If subEnableButtons() Then
                btnSave.Enabled = True
            Else
                btnSave.Enabled = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Function subEnableButtons() As Boolean
        Dim nTotalAmount As Decimal
        Dim nCreditAmount As Decimal
        Dim nCashAmount As Decimal
        Dim irow
        Try
            For irow = 0 To grdPaymode.RowCount - 1
                If Convert.ToString(fnGetdgvText(grdPaymode, irow, cLblPaymode.Index)).ToUpper.Trim = "CASH" Then
                    nCashAmount = Val(fnGetdgvText(grdPaymode, irow, cTxtAmount.Index))
                    nTotalAmount = nTotalAmount + Val(fnGetdgvText(grdPaymode, irow, cTxtAmount.Index))
                Else
                    nTotalAmount = nTotalAmount + Val(fnGetdgvText(grdPaymode, irow, cTxtAmount.Index))
                End If
            Next irow
            'If nCreditAmount > nCreditAvailable Then
            '    subEnableButtons = False
            '    Exit Function
            'End If
            If Val(txtTotalAmount.Text) > 0 And nTotalAmount <= 0 Then
                subEnableButtons = False
                Exit Function
            End If

            If Val(txtTotalAmount.Text) > 0 Then
                If Not nCashAmount - (nTotalAmount - Val(txtTotalAmount.Text)) >= 0 Then
                    subEnableButtons = False
                    Exit Function
                End If
            End If

            'If Val(txtBalanceAmount.Text) < 0 Then
            '    subEnableButtons = False
            '    Exit Function
            'End If

            If Val(txtBalanceAmount.Text) <> 0 Then
                subEnableButtons = False
                Exit Function
            End If

            subEnableButtons = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            subEnableButtons = False
        End Try
    End Function


    Private Sub SubDisplaytotal()
        Try
            '
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'Private Sub subLoadPayMode()
    '    Dim dsPay As New DataSet

    '    Dim stest As String
    '    Try
    '        dsPay = objPay.GetPayMode()
    '        grdPaymode.MaxRows = 0
    '        If dsPay.Tables(0).Rows.Count > 0 Then
    '            RecCount = dsPay.Tables(0).Rows.Count
    '            For iCnt = 0 To dsPay.Tables(0).Rows.Count - 1
    '                grdPaymode.MaxRows = grdPaymode.MaxRows + 1
    '                SetText(grdPaymode, grdPaymode.MaxRows, LblPaymode, Trim(dsPay.Tables(0).Rows(iCnt)("PayMode") & ""))

    '                'Consider the commission 
    '                arrpay(iCnt, 0) = Val(dsPay.Tables(0).Rows(iCnt)("Commission") & "")
    '                'consider the commission only if the salestype is Cash or TradeIn.
    '                '''If UCase$(msSalesType) = "CASH" Or UCase$(msSalesType) = "TRADEIN" Then
    '                '''    arrpay(iCnt, 0) = Val(dsPay.Tables(0).Rows(iCnt)("Commission") & "")
    '                '''Else
    '                '''    arrpay(iCnt, 0) = 0
    '                '''End If

    '                arrpay(iCnt, 1) = 0
    '                arrpay(iCnt, 2) = 0
    '                arrpay(iCnt, 4) = ""
    '                arrpay(iCnt, 3) = Trim(dsPay.Tables(0).Rows(iCnt)("PayMode") & "")
    '                Call SetObjImage(grdPaymode, iCnt, ImgPaymode, Trim(dsPay.Tables(0).Rows(iCnt)("Position") & ""))

    '            Next
    '        End If
    '        If bTradeinFlag = True Then
    '            txtTotalAmount.Text = Format(nTotalNet + dTradeInSales, "0.00")
    '        Else
    '            txtTotalAmount.Text = Format(nTotalNet, "0.00")
    '        End If
    '        txtBalanceAmount.Text = nTotalNet

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try

    'End Sub
    Private Sub subLoadPayMode()
        Dim dsPaymode As New DataSet
        Dim objPaymode As clsPaymode
        Dim iRow As Long
        Try
            objPaymode = New clsPaymode
            dsPaymode = objPay.GetPayMode()
            grdPaymode.RowCount = 0
            If dsPaymode.Tables(0).Rows.Count > 0 Then
                For iRow = 0 To dsPaymode.Tables(0).Rows.Count - 1

                    arrpay(iCnt, 0) = Val(dsPaymode.Tables(0).Rows(iCnt)("Commission") & "")
                    arrpay(iCnt, 1) = 0
                    arrpay(iCnt, 2) = 0
                    arrpay(iCnt, 4) = ""
                    arrpay(iCnt, 3) = Trim(dsPaymode.Tables(0).Rows(iCnt)("PayMode") & "")

                    grdPaymode.Rows.Add()
                    fnSetImage(grdPaymode, iRow, 0, Nothing)
                    fnSetdgvText(grdPaymode, iRow, cLblPaymode.Index, Convert.ToString(dsPaymode.Tables(0).Rows(iRow).Item("Paymode")).Trim)
                    If Trim(sCompanyShortCode) = "AJ" Then
                        If Convert.ToString(dsPaymode.Tables(0).Rows(iRow).Item("Paymode")).ToUpper.Trim = "CASH" Then
                            grdPaymode(cTxtSlipNo.Index, grdPaymode.CurrentRow.Index).ReadOnly = True
                        ElseIf Convert.ToString(dsPaymode.Tables(0).Rows(iRow).Item("Paymode")).ToUpper.Trim = "TRADEIN" Then
                            fnSetdgvText(grdPaymode, iRow, cValueExist.Index, True)
                            grdPaymode(cTxtAmount.Index, 3).ReadOnly = True
                        ElseIf Convert.ToString(dsPaymode.Tables(0).Rows(iRow).Item("Paymode")).ToUpper.Trim = "FLEXIAMOUNT" Then
                            fnSetdgvText(grdPaymode, iRow, cValueExist.Index, True)
                            grdPaymode(cTxtAmount.Index, 13).ReadOnly = True
                        ElseIf Convert.ToString(dsPaymode.Tables(0).Rows(iRow).Item("Paymode")).ToUpper.Trim = "FLEXIBONUS" Then
                            fnSetdgvText(grdPaymode, iRow, cValueExist.Index, True)
                            grdPaymode(cTxtAmount.Index, 14).ReadOnly = True
                        ElseIf Convert.ToString(dsPaymode.Tables(0).Rows(iRow).Item("Paymode")).ToUpper.Trim = "ADVDEPOSIT" Then
                            fnSetdgvText(grdPaymode, iRow, cValueExist.Index, True)
                            grdPaymode(cTxtAmount.Index, 15).ReadOnly = True
                        Else
                            grdPaymode(cTxtSlipNo.Index, grdPaymode.CurrentRow.Index).ReadOnly = False
                        End If
                    ElseIf Trim(sCompanyShortCode) = "MD" Then
                        If Convert.ToString(dsPaymode.Tables(0).Rows(iRow).Item("Paymode")).ToUpper.Trim = "CASH" Then
                            grdPaymode(cTxtSlipNo.Index, grdPaymode.CurrentRow.Index).ReadOnly = True
                        ElseIf Convert.ToString(dsPaymode.Tables(0).Rows(iRow).Item("Paymode")).ToUpper.Trim = "TRADEIN" Then
                            fnSetdgvText(grdPaymode, iRow, cValueExist.Index, True)
                            grdPaymode(cTxtAmount.Index, 3).ReadOnly = True
                        Else
                            grdPaymode(cTxtSlipNo.Index, grdPaymode.CurrentRow.Index).ReadOnly = False
                        End If
                    Else
                        If Convert.ToString(dsPaymode.Tables(0).Rows(iRow).Item("Paymode")).ToUpper.Trim = "CASH" Then
                            grdPaymode(cTxtSlipNo.Index, grdPaymode.CurrentRow.Index).ReadOnly = True
                        Else
                            grdPaymode(cTxtSlipNo.Index, grdPaymode.CurrentRow.Index).ReadOnly = False
                        End If
                    End If
                    If Trim(sCompanyShortCode) = "AJ" Then
                        If Convert.ToString(dsPaymode.Tables(0).Rows(iRow).Item("Paymode")).ToUpper.Trim = "TRADEIN" Or Convert.ToString(dsPaymode.Tables(0).Rows(iRow).Item("Paymode")).ToUpper.Trim = "FLEXIAMOUNT" Or Convert.ToString(dsPaymode.Tables(0).Rows(iRow).Item("Paymode")).ToUpper.Trim = "FLEXIBONUS" Or Convert.ToString(dsPaymode.Tables(0).Rows(iRow).Item("Paymode")).ToUpper.Trim = "ADVDEPOSIT" Then
                            fnSetdgvText(grdPaymode, iRow, cValueExist.Index, True)
                        Else
                            fnSetdgvText(grdPaymode, iRow, cValueExist.Index, False)
                        End If
                    ElseIf Trim(sCompanyShortCode) = "MD" Then
                        If Convert.ToString(dsPaymode.Tables(0).Rows(iRow).Item("Paymode")).ToUpper.Trim = "TRADEIN" Then
                            fnSetdgvText(grdPaymode, iRow, cValueExist.Index, True)
                        Else
                            fnSetdgvText(grdPaymode, iRow, cValueExist.Index, False)
                        End If
                    End If
                Next iRow
                RecCount = dsPaymode.Tables(0).Rows.Count
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        Finally
            dsPaymode = Nothing
            objPaymode = Nothing
        End Try
    End Sub

    Public ReadOnly Property Commission() As Object
        Get
            Commission = mdCommission
        End Get
    End Property

    Public WriteOnly Property SalesType() As String
        Set(ByVal Value As String)
            msSalesType = Value
        End Set
    End Property

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        gPayCancel = True
        Me.Close()
    End Sub

    Private Sub frmPayingmode_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Dim iX As Long
        Try
            If (bTradeinFlag = True And dTradeinBalance < 0) Then
                txtAmount0.Text = Format(dTradeinBalance, "0.00")
                txtPaidAmount.Text = Format(dTradeinBalance, "0.00")
                btnSave.Focus()
            ElseIf (bTradeinFlag = True And dTradeinBalance > 0) Then
                'MessageBox.Show("Please choose Paymode !!!")
                txtAmount0.Text = Format(dTradeinBalance, "0.00")


            Else
                'MessageBox.Show("Please choose Paymode !!!")
                'txtAmount0.Text = Format(nTotalNet, "0.00")
                'murugan include 
                'If grdPaymode.MaxRows > 0 Then
                '    grdPaymode.Col = TxtAmount
                '    grdPaymode.Row = 1
                '    grdPaymode.Text = Format(nTotalNet, "0.00")
                'End If
            End If
            grdPaymode.Focus()
            grdPaymode.CurrentCell = grdPaymode(cTxtAmount.Index, 0)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmPayingmode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            btnClose_Click(sender, e)
        End If
        If e.KeyCode = Keys.F7 Then
            btnSave_Click(sender, e)
        End If
    End Sub
    Private Sub grdPaymode_LeaveCell(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_LeaveCellEvent)
        Try

            If e.newCol <> -1 Then
                If e.row > 0 And e.col = TxtAmount Then
                    subTxtAmount_Lostfocus()
                End If
                'got focus    
                If e.newRow > 0 And e.newRow <> e.row And e.newCol = TxtAmount Then
                    subTxtAmount_Gotfocus(e.newRow)
                End If


                'Slip no Lostfocus
                If e.row > 0 And e.col = TxtSlipNo Then
                    txtSlipNo_LostFocus(e.row)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subTxtAmount_Gotfocus(ByVal iIndex As Integer)
        Try

            If (bTradeinFlag = False) Or (bTradeinFlag = True And dTradeinBalance > 0) Then
                If arrpay(iIndex - 1, 1) + arrpay(iIndex - 1, 2) <= 0 Then

                    SetText(grdPaymode, iIndex, TxtAmount, Format(Val(txtBalanceAmount.Text) + (Val(txtBalanceAmount.Text) * arrpay(iIndex - 1, 0) / 100), "0.00"))
                Else

                    SetText(grdPaymode, iIndex, TxtAmount, Format(arrpay(iIndex - 1, 1) + arrpay(iIndex - 1, 2), "0.00"))
                End If


                If Trim(GetText(grdPaymode, iIndex, LblPaymode)) <> "CASH" And Trim(GetText(grdPaymode, iIndex, LblPaymode)) <> "FOREIGN" Then
                    'grdPaymode.Col = TxtAmount
                    'grdPaymode.Row = iIndex
                    'grdPaymode.Value()
                    'settext(grdpaymode,iTxtAmount(Index).MaxValue = Val(TxtAmount(Index))
                End If

            Else
                'TxtAmount(Index).Enabled = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subTxtAmount_Lostfocus()
        Try
            Dim iX As Long
            Dim dAmt As Double
            Dim dComm As Double

            dAmt = 0

            For iX = 0 To RecCount - 1
                arrpay(iX, 1) = Val(GetText(grdPaymode, iX + 1, TxtAmount)) / Val(1 + arrpay(iX, 0) / 100)
                dAmt = dAmt + Val(arrpay(iX, 1))
                arrpay(iX, 2) = Val(GetText(grdPaymode, iX + 1, TxtAmount)) - arrpay(iX, 1)
                dComm = dComm + arrpay(iX, 2)
            Next iX

            txtPaidAmount.Text = Format$(dAmt, "0.00")
            txtBalanceAmount.Text = Format(Val(txtTotalAmount.Text) - Val(txtPaidAmount.Text), "#####0.00")
            If Val(txtBalanceAmount.Text) < 0 Then txtBalanceAmount.Text = "0.00"
            txtCommission.Text = Format$(dComm, "0.00")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    ''HAFIL commented
    'Private Sub subTxtAmount_Keyup(ByVal iIndex As Integer, ByVal KeyCode As Integer)
    '    Try
    '        Dim iCnt As Integer
    '        If (bTradeinFlag = False) Or (bTradeinFlag = True And dTradeinBalance > 0) Then


    '            Dim dComm As Double
    '            Dim sFormula As String
    '            If KeyCode = 13 Then

    '                If Trim(GetText(grdPaymode, iIndex, LblPaymode)) = "FOREIGN" And arrpay(iIndex - 1, 1) = 0 Then
    '                    IndValue = iIndex

    '                    frameforeign.Visible = True

    '                    SubLoadForeignCurrency()
    '                    grdForeign.Row = 1
    '                    grdForeign.Col = 2
    '                    grdForeign.Action = FPSpread.ActionConstants.ActionActiveCell
    '                    Exit Sub
    '                ElseIf Trim(GetText(grdPaymode, iIndex, LblPaymode)) = "FOREIGN" And arrpay(iIndex - 1, 1) <> 0 Then
    '                    frameforeign.Visible = True
    '                    grdForeign.Focus()
    '                    For iCnt = 1 To grdForeign.MaxRows - 1
    '                        ' grdForeign.MaxRows = grdForeign.MaxRows + 1
    '                        grdForeign.Row = grdForeign.MaxRows
    '                        grdForeign.Col = 1
    '                        grdForeign.Text = ArrayForeign(iCnt, 0)
    '                        grdForeign.Col = 2
    '                        grdForeign.Text = ArrayForeign(iCnt, 1)
    '                        grdForeign.Col = 3
    '                        grdForeign.Text = ArrayForeign(iCnt, 2)
    '                        grdForeign.Col = 4
    '                        grdForeign.Text = ArrayForeign(iCnt, 3)
    '                    Next iCnt
    '                    'grdForeign.MaxRows = grdForeign.MaxRows + 1
    '                    SetText(grdForeign, grdForeign.MaxRows, 1, "PAID AMOUNT")
    '                    'RowColor(grdForeign, grdForeign.MaxRows, -1, grdForeign.MaxRows, -1, &HFF00&)
    '                    grdForeign.Row = grdForeign.MaxRows
    '                    grdForeign.Col = 2
    '                    sFormula = "sum(d1:d" & grdForeign.MaxRows - 1 & ")"
    '                    grdForeign.Formula = sFormula
    '                End If

    '                txtPaidAmount.Text = Format$(Val(txtPaidAmount.Text) - arrpay(iIndex - 1, 1), "0.00")
    '                txtBalanceAmount.Text = Format$(Val(txtBalanceAmount.Text) + arrpay(iIndex - 1, 1), "0.00")
    '                txtCommission.Text = Format$(Val(txtCommission.Text) - arrpay(iIndex - 1, 2), "0.00")

    '                arrpay(iIndex - 1, 1) = Val(GetText(grdPaymode, iIndex, TxtAmount)) / Val(1 + arrpay(iIndex - 1, 0) / 100)
    '                arrpay(iIndex - 1, 2) = Val(GetText(grdPaymode, iIndex, TxtAmount)) - arrpay(iIndex - 1, 1)

    '                'new value
    '                txtPaidAmount.Text = Format$(Val(txtPaidAmount.Text) + arrpay(iIndex - 1, 1), "0.00")
    '                txtCommission.Text = Format$(Val(txtCommission.Text) + arrpay(iIndex - 1, 2), "0.00")
    '                txtBalanceAmount.Text = Format$(IIf((Val(txtBalanceAmount.Text) - arrpay(iIndex - 1, 1)) < 0, 0, (Val(txtBalanceAmount.Text) - arrpay(iIndex - 1, 1))), "0.00")

    '                If Val(txtPaidAmount.Text) >= Val(txtTotalAmount.Text) Then
    '                    txtReturnChange.Text = Format(System.Math.Abs(Val(txtPaidAmount.Text) - Val(txtTotalAmount.Text)), "#0.00")
    '                End If
    '                If Trim(arrpay(iIndex - 1, 3)) = "CASH" And arrpay(iIndex - 1, 1) >= Val(txtBalanceAmount.Text) Then
    '                    arrpay(iIndex - 1, 1) = Format(Val(arrpay(iIndex - 1, 1)) - Val(txtReturnChange.Text), "0.00")
    '                End If

    '                If Trim(GetText(grdPaymode, iIndex, LblPaymode)) <> "CASH" And Trim(GetText(grdPaymode, iIndex, LblPaymode)) <> "FOREIGN" And Val(GetText(grdPaymode, iIndex, TxtAmount)) > 0 Then
    '                    grdPaymode.Col = grdPaymode.ActiveCol + 1
    '                    grdPaymode.Action = FPSpread.ActionConstants.ActionActiveCell
    '                Else
    '                    If Val(txtPaidAmount.Text) < Val(txtTotalAmount.Text) Then
    '                        SendKeys.Send("{DOWN}")
    '                    Else
    '                        btnSave.Focus()
    '                    End If
    '                End If
    '            ElseIf KeyCode = 40 Then
    '                If arrpay(iIndex - 1, 1) = 0 Then
    '                    SetText(grdPaymode, iIndex, TxtAmount, "")
    '                End If
    '                If iIndex < RecCount Then
    '                    grdPaymode.Row = grdPaymode.ActiveRow + 1
    '                    grdPaymode.Col = TxtAmount
    '                    grdPaymode.Action = FPSpread.ActionConstants.ActionActiveCell
    '                    'SendKeys.Send("{DOWN}")
    '                Else
    '                    grdPaymode.Col = TxtAmount
    '                    grdPaymode.Row = 0

    '                End If
    '            ElseIf KeyCode = 38 Then
    '                If arrpay(iIndex - 1, 1) = 0 Then
    '                    SetText(grdPaymode, iIndex, TxtAmount, "")
    '                End If
    '                If iIndex > 0 Then
    '                    'SendKeys.Send("{UP}")
    '                    grdPaymode.Row = grdPaymode.ActiveRow - 1
    '                    grdPaymode.Col = TxtAmount
    '                    grdPaymode.Action = FPSpread.ActionConstants.ActionActiveCell
    '                Else
    '                    grdPaymode.Row = RecCount
    '                    grdPaymode.Col = TxtAmount

    '                End If
    '            End If
    '            If Val(txtBalanceAmount.Text) = 0 And txtPaidAmount.Text >= txtTotalAmount.Text And Trim(GetText(grdPaymode, iIndex, LblPaymode)) = "CASH" And bAmountFlag = False Then
    '                '        Me.KeyPreview = True
    '                bAmountFlag = True
    '                '        cmdSave.Enabled = True
    '                'If Trim(arrpay(Index, 3)) = "CASH" And arrpay(Index, 1) > Val(txtTotalAmount) Then
    '                '            arrpay(Index, 1) = Format(Val(txtTotalAmount), "0.00")
    '                '
    '                '         End If
    '                btnSave.Focus()
    '                '    Else
    '                '    txtAmount(Index + 1).SetFocus
    '            End If

    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    'End Sub

    'Private Sub SubLoadForeignCurrency()
    '    Try
    '        Dim dsFC As New DataSet
    '        Dim PFOREIGNMAXROW As Long
    '        Dim sFormula As String
    '        Dim iCnt As Integer
    '        '  ArrayForeign(i, 1) = 0
    '        '   ArrayForeign(i, 2) = 0
    '        '  ArrayForeign(i, 4) = ""

    '        dsFC = objPay.GetForeignCurrency()
    '        '        If adoRS.RecordCount > 0 Then
    '        '         bForeig = True
    '        '        End If
    '        grdForeign.MaxRows = 0

    '        grdForeign.MaxRows = grdForeign.MaxRows - 1
    '        If dsFC.Tables(0).Rows.Count > 0 Then
    '            For iCnt = 0 To dsFC.Tables(0).Rows.Count - 1
    '                grdForeign.MaxRows = grdForeign.MaxRows + 1
    '                grdForeign.Row = grdForeign.MaxRows
    '                grdForeign.Col = 1
    '                grdForeign.Text = dsFC.Tables(0).Rows(iCnt)("CcyCode")
    '            Next
    '        End If


    '        grdForeign.MaxRows = grdForeign.MaxRows + 1
    '        SetText(grdForeign, grdForeign.MaxRows, 1, "PAID AMOUNT")
    '        'RowColor(grdForeign, grdForeign.MaxRows, -1, grdForeign.MaxRows, -1, &HFF00&)
    '        grdForeign.Row = grdForeign.MaxRows
    '        grdForeign.Col = 2
    '        sFormula = "sum(d1:d" & grdForeign.MaxRows - 1 & ")"
    '        grdForeign.Formula = sFormula
    '        grdForeign.Focus()
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    'End Sub
    ''Hafil Commented
    'Private Sub grdPaymode_KeyUpEvent(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_KeyUpEvent)
    '    Try
    '        If grdPaymode.ActiveCol = TxtAmount Then
    '            subTxtAmount_Keyup(grdPaymode.ActiveRow, e.keyCode)
    '        ElseIf grdPaymode.ActiveCol = TxtSlipNo Then
    '            txtSlipNo_KeyUp(grdPaymode.ActiveRow, e.keyCode)
    '        End If
    '        If sLoad = "1" Then
    '            SendKeys.Send("{TAB}")
    '            SendKeys.Send("{TAB}")
    '            sLoad = ""
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    'End Sub

    Public Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            dPayModeCommission = Val(txtCommission.Text)
            gPayCancel = False
            If Val(txtBalanceAmount.Text) >= 0 Then
                'If bInstallmentEntryFlag = True Then
                '    dPaidAmount = Format(Val(txtTotalAmount.Text), "0.00")
                '    Me.Close()
                '    Exit Sub
                '    ''objInstallementEntry .SPaymode=
                'End If
                'If bInstallmentFlag = False Then
                '    objGPOS.lblReturnAmount.Text = Format(Val(txtReturnChange.Text), "0.00")
                'End If
                'Hafil Commented
                '' ''If Val(dRedeemedPoints) >= 10000 Then
                '' ''    MessageBox.Show("Customer Redeemed 10000 Points Per day")
                '' ''    Exit Sub
                '' ''End If

            
                If txtPaidAmount.Text >= Val(txtTotalAmount.Text) And fnccheckPaid() Then
                    'If txtPaidAmount.Text >= Val(txtTotalAmount.Text) Then
                    ReturnAmount = txtReturnChange.Text
                    dPaidAmount = txtPaidAmount.Text
                    mdCommission = txtCommission.Text
                    dPaidAmount = dPaidAmount + mdCommission
                    objGPOS.lblReturnAmount.Text = Format(Val(txtReturnChange.Text), "0.00")
                    'dFlexiFinalAmount = 0
                    'dFlexiFinalBonus = 0
                    Me.Close()
                End If
            Else
                MsgBox("Balance Amount Should Be Zero")
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    'Dim iRow As Long
    '    Try
    '        Me.Cursor = Cursors.WaitCursor
    '        dgvPaymode.CurrentCell = dgvPaymode(cPaymode.Index, dgvPaymode.CurrentRow.Index)
    '        If sSalesType = "POS" Then
    '            subCalcTotal()
    '            Me.Close()
    '            frmPos.nMemberCreditUsed = nCreditUsed
    '            frmPos.lblTender.Text = lblPaid.Text
    '            frmPos.lblReturn.Text = lblBalance.Text
    '            frmPos.subSave()
    '            Me.Dispose()
    '       End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical)
    '    Finally
    '        Me.Cursor = Cursors.Default
    '    End Try
    'Hafil Commented
    Private Function fnccheckPaid() As Boolean
        Try
            '    Dim dAmt As Double
            '    Dim dNoCash As Double
            Dim iX As Long
            Dim sPaymode As String
            Dim dAmount As Double
            Dim dSlipno As String
            fnccheckPaid = False
            '    For iX = 0 To RecCount - 1
            '        If Trim(arrpay(iX, 3)) <> "CASH" Then
            '            dNoCash = dNoCash + arrpay(iX, 1)
            '        End If
            '    Next iX

            '    'txtReturnChange.Text = 0 - (Format(Val(txtBalanceAmount.Text), "#####0.00"))
            '    txtReturnChange.Text = txtBalanceAmount.Text
            '    For iX = 0 To RecCount - 1
            '        If Trim(arrpay(iX, 3)) = "CASH" And arrpay(iX, 1) >= txtBalanceAmount.Text Then
            '            arrpay(iX, 1) = Format(Val(arrpay(iX, 1)) - Val(txtReturnChange.Text), "0.00")
            '        End If
            '        If txtTotalAmount.Text > 0 And Val(fnGetdgvText(grdPaymode, iX, cTxtAmount.Index)) < 0 Then
            '            fnSetdgvText(grdPaymode, iX, TxtAmount, "0")
            '            arrpay(iX, 1) = 0
            '        End If


            '        dAmt = dAmt + Val(fnGetdgvText(grdPaymode, iX, TxtAmount))
            '    Next iX
            '    If Math.Round(dAmt, 2) >= Val(txtTotalAmount.Text) Then
            '        fnccheckPaid = True
            '    Else
            '        txtPaidAmount.Text = dAmt
            '        fnSetdgvText(grdPaymode, iX, cTxtAmount.Index, dAmt)
            '        'grdPaymode.Col = TxtAmount
            '        'grdPaymode.Row = 0
            '        'TxtAmount(0).SetFocus()
            '        If Trim(sCompanyShortCode) = "HN" Then
            '            fnccheckPaid = False
            '        End If
            '    End If
            For iX = 0 To grdPaymode.RowCount - 1
                If Val(fnGetdgvText(grdPaymode, iX, cTxtAmount.Index)) > 0 Then
                    sPaymode = fnGetdgvText(grdPaymode, iX, cLblPaymode.Index)
                    dAmount = Val(fnGetdgvText(grdPaymode, iX, cTxtAmount.Index))
                    dSlipno = fnGetdgvText(grdPaymode, iX, cTxtSlipNo.Index)
                    arrpay(iX, 3) = sPaymode
                    arrpay(iX, 1) = dAmount
                    arrpay(iX, 2) = 0
                    arrpay(iX, 4) = dSlipno
                    If fnGetdgvText(grdPaymode, iX, cLblPaymode.Index) = "REDEEMPOINTS" And Trim(sCompanyShortCode) = "AJ" Then
                        'If Val(fnGetdgvText(grdPaymode, e.RowIndex, cTxtAmount.Index)) Mod 1000 <> 0 Then
                        '    MessageBox.Show("AMout must in Multiples of 1000's")
                        '    Exit Sub
                        'End If

                        If Val(fnGetdgvText(grdPaymode, iX, cTxtAmount.Index)) > 0 And (Val(dRewardPoints) - Val(dRedeemPoints)) < ((Val(fnGetdgvText(grdPaymode, iX, cTxtAmount.Index)) / 5) * 1000) Then
                            MessageBox.Show("Cannot able to Redeem. Please Check available Points")
                            Exit Function
                        End If

                        If Val(fnGetdgvText(grdPaymode, iX, cTxtAmount.Index)) > 0 Then
                            If Val(dRedeemedPoints) >= 10000 Then
                                MessageBox.Show("Customer Redeemed 10000 Points Per day")
                                Exit Function
                            End If
                        End If

                        If Val(txtTotalAmount.Text) < 300 And Trim(sRedeemSalesType) <> "" Then
                            MessageBox.Show("Totalsales should be more than $300 to Redeem Points")
                            Exit Function
                        End If
                        If Trim(sCustomerType) = "RUBY" Then
                            If Val(fnGetdgvText(grdPaymode, iX, cTxtAmount.Index)) Mod 5 <> 0 Then
                                MessageBox.Show("Amount must in Multiples of 5's")
                                Exit Function
                            End If
                            If Val(fnGetdgvText(grdPaymode, iX, cTxtAmount.Index)) > 50 Then
                                MessageBox.Show("Amount Should be less than 50")
                                Exit Function
                            End If
                        ElseIf Trim(sCustomerType) = "EMERALD" Then
                            If Val(fnGetdgvText(grdPaymode, iX, cTxtAmount.Index)) Mod 7 <> 0 Then
                                MessageBox.Show("Amount must in Multiples of 7's")
                                Exit Function
                            End If
                            If Val(fnGetdgvText(grdPaymode, iX, cTxtAmount.Index)) > 70 Then
                                MessageBox.Show("Amount Should be less than 70")
                                Exit Function
                            End If
                        ElseIf Trim(sCustomerType) = "DIAMOND" Then
                            If Val(fnGetdgvText(grdPaymode, iX, cTxtAmount.Index)) Mod 9 <> 0 Then
                                MessageBox.Show("Amount must in Multiples of 9's")
                                Exit Function
                            End If
                            If Val(fnGetdgvText(grdPaymode, iX, cTxtAmount.Index)) > 90 Then
                                MessageBox.Show("Amount Should be less than 90")
                                Exit Function
                            End If
                        End If
                    End If
                Else
                    arrpay(iX, 1) = 0
                End If
            Next
            fnccheckPaid = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function
    'Hafil Commented

    'Private Sub txtSlipNo_KeyUp(ByVal iIndex As Integer, ByVal KeyCode As Integer)
    '    If KeyCode = 13 Then
    '        If iIndex < RecCount Then
    '            grdPaymode.Row = grdPaymode.ActiveRow + 1
    '            grdPaymode.Col = TxtAmount
    '            grdPaymode.Action = FPSpread.ActionConstants.ActionActiveCell
    '        End If
    '    ElseIf KeyCode = 40 Then
    '        If iIndex < RecCount Then
    '            grdPaymode.Row = grdPaymode.ActiveRow + 1
    '            grdPaymode.Col = TxtSlipNo
    '            grdPaymode.Action = FPSpread.ActionConstants.ActionActiveCell
    '            'TxtSlipNo(Index + 1).SetFocus()
    '        Else
    '            grdPaymode.Row = 1
    '            grdPaymode.Col = TxtSlipNo
    '            grdPaymode.Action = FPSpread.ActionConstants.ActionActiveCell
    '            'TxtSlipNo(0).SetFocus()
    '        End If
    '    ElseIf KeyCode = 38 Then
    '        If iIndex > 0 Then
    '            grdPaymode.Row = grdPaymode.ActiveRow - 1
    '            grdPaymode.Col = TxtSlipNo
    '            grdPaymode.Action = FPSpread.ActionConstants.ActionActiveCell
    '            'TxtSlipNo(Index - 1).SetFocus()
    '        Else
    '            grdPaymode.Row = RecCount
    '            grdPaymode.Col = TxtSlipNo
    '            grdPaymode.Action = FPSpread.ActionConstants.ActionActiveCell
    '            'TxtSlipNo(RecCount).SetFocus()
    '        End If
    '    End If
    'End Sub

    Private Sub txtSlipNo_LostFocus(ByVal iIndex As Integer)
        Try
            arrpay(iIndex - 1, 4) = GetText(grdPaymode, iIndex, TxtSlipNo)
            'If Val(txtPaidAmount.Text) >= Val(txtTotalAmount.Text) Then
            '    btnSave.Focus()
            'End If


            If Val(txtPaidAmount.Text) = Val(txtTotalAmount.Text) Then
                btnSave.Focus()
            End If


            'If arrpay(iIndex - 1, 0) = "REDEEMPOINTS" Then
            '    If Trim(sCustomerType) = "RUBY" Then
            '        arrpay(iIndex - 1, 1) = arrpay(iIndex - 1, 4) * 5
            '    ElseIf Trim(sCustomerType) = "EMERALD" Then
            '        arrpay(iIndex - 1, 1) = arrpay(iIndex - 1, 4) * 7
            '    ElseIf Trim(sCustomerType) = "DIAMOND" Then
            '        arrpay(iIndex - 1, 1) = arrpay(iIndex - 1, 4) * 9
            '    End If
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtSlipNo_Validate(ByVal iIndex As Integer)
        Try
            arrpay(iIndex - 1, 4) = Trim(GetText(grdPaymode, iIndex, TxtSlipNo))
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub grdPaymode_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPaymode.CellContentClick
        Try
            subCalcTotal()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub grdPaymode_CellEndEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPaymode.CellEndEdit
        Try
            If fnGetNumeric(grdPaymode, e.RowIndex, cTxtAmount.Index) = "0.00" Then
                fnSetdgvText(grdPaymode, e.RowIndex, cValueExist.Index, False)
            Else
                fnSetdgvText(grdPaymode, e.RowIndex, cValueExist.Index, True)
            End If
            If subEnableButtons() Then
                btnSave.Enabled = True
            Else
                btnSave.Enabled = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub grdPaymode_CellEnter(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPaymode.CellEnter
        Try
            If fnGetdgvText(grdPaymode, e.RowIndex, cValueExist.Index) <> "" Then
                If fnGetdgvText(grdPaymode, e.RowIndex, cValueExist.Index) = False Then
                    fnSetNumeric(grdPaymode, e.RowIndex, cTxtAmount.Index, txtTotalAmount.Text - txtPaidAmount.Text)
                End If
            End If
            If fnGetdgvText(grdPaymode, e.RowIndex, cLblPaymode.Index) = "REDEEMPOINTS" Then
                'If Val(fnGetdgvText(grdPaymode, e.RowIndex, cTxtAmount.Index)) Mod 1000 <> 0 Then
                '    MessageBox.Show("AMout must in Multiples of 1000's")
                '    Exit Sub
                'End If
                'If Trim(sCustomerType) = "RUBY" Then
                '    fnSetNumeric(grdPaymode, e.RowIndex, cTxtAmount.Index, ((Val(fnGetdgvText(grdPaymode, e.RowIndex, cTxtAmount.Index)) * 5) / 1000))
                'ElseIf Trim(sCustomerType) = "EMERALD" Then
                '    fnSetNumeric(grdPaymode, e.RowIndex, cTxtAmount.Index, ((Val(fnGetdgvText(grdPaymode, e.RowIndex, cTxtAmount.Index)) * 7) / 1000))
                'ElseIf Trim(sCustomerType) = "DIAMOND" Then
                '    fnSetNumeric(grdPaymode, e.RowIndex, cTxtAmount.Index, ((Val(fnGetdgvText(grdPaymode, e.RowIndex, cTxtAmount.Index)) * 9) / 1000))
                'End If
                'If Trim(sCustomerType) = "RUBY" Then
                '    If Val(fnGetdgvText(grdPaymode, e.RowIndex, cTxtAmount.Index)) Mod 5 <> 0 Then
                '        MessageBox.Show("Amount must in Multiples of 5's")
                '        Exit Sub
                '    End If
                '    If Val(fnGetdgvText(grdPaymode, e.RowIndex, cTxtAmount.Index)) > 50 Then
                '        MessageBox.Show("Amount Should be less than 50")
                '        Exit Sub
                '    End If
                'ElseIf Trim(sCustomerType) = "EMERALD" Then
                '    If Val(fnGetdgvText(grdPaymode, e.RowIndex, cTxtAmount.Index)) Mod 7 <> 0 Then
                '        MessageBox.Show("Amount must in Multiples of 7's")
                '        Exit Sub
                '    End If
                '    If Val(fnGetdgvText(grdPaymode, e.RowIndex, cTxtAmount.Index)) > 70 Then
                '        MessageBox.Show("Amount Should be less than 70")
                '        Exit Sub
                '    End If
                'ElseIf Trim(sCustomerType) = "DIAMOND" Then
                '    If Val(fnGetdgvText(grdPaymode, e.RowIndex, cTxtAmount.Index)) Mod 9 <> 0 Then
                '        MessageBox.Show("Amount must in Multiples of 9's")
                '        Exit Sub
                '    End If
                '    If Val(fnGetdgvText(grdPaymode, e.RowIndex, cTxtAmount.Index)) > 90 Then
                '        MessageBox.Show("Amount Should be less than 90")
                '        Exit Sub
                '    End If
                'End If
            End If
            subCalcTotal()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub grdPaymode_CellLeave(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPaymode.CellLeave
        Try
            If fnGetdgvText(grdPaymode, e.RowIndex, cValueExist.Index) = False Then
                fnSetNumeric(grdPaymode, e.RowIndex, cTxtAmount.Index, "0.00")
            End If
            fnSetNumeric(grdPaymode, e.RowIndex, cTxtAmount.Index, fnGetNumeric(grdPaymode, e.RowIndex, cTxtAmount.Index))
            If fnGetdgvText(grdPaymode, e.RowIndex, cLblPaymode.Index) = "REDEEMPOINTS" Then
                
                'If Trim(sCustomerType) = "RUBY" Then
                '    If Val(fnGetdgvText(grdPaymode, e.RowIndex, cTxtAmount.Index)) Mod 5 <> 0 Then
                '        MessageBox.Show("Amount must in Multiples of 5's")
                '        Exit Sub
                '    End If
                '    If Val(fnGetdgvText(grdPaymode, e.RowIndex, cTxtAmount.Index)) > 50 Then
                '        MessageBox.Show("Amount Should be less than 50")
                '        Exit Sub
                '    End If
                'ElseIf Trim(sCustomerType) = "EMERALD" Then
                '    If Val(fnGetdgvText(grdPaymode, e.RowIndex, cTxtAmount.Index)) Mod 7 <> 0 Then
                '        MessageBox.Show("Amount must in Multiples of 7's")
                '        Exit Sub
                '    End If
                '    If Val(fnGetdgvText(grdPaymode, e.RowIndex, cTxtAmount.Index)) > 70 Then
                '        MessageBox.Show("Amount Should be less than 70")
                '        Exit Sub
                '    End If
                'ElseIf Trim(sCustomerType) = "DIAMOND" Then
                '    If Val(fnGetdgvText(grdPaymode, e.RowIndex, cTxtAmount.Index)) Mod 9 <> 0 Then
                '        MessageBox.Show("Amount must in Multiples of 9's")
                '        fnSetNumeric(grdPaymode, e.RowIndex, cTxtAmount.Index, "0.00")
                '        Exit Sub
                '    End If
                '    If Val(fnGetdgvText(grdPaymode, e.RowIndex, cTxtAmount.Index)) > 90 Then
                '        MessageBox.Show("Amount Should be less than 90")
                '        fnSetNumeric(grdPaymode, e.RowIndex, cTxtAmount.Index, "0.00")
                '        Exit Sub
                '    End If
                'End If
            End If
            subCalcTotal()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub grdPaymode_CellValueChanged(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPaymode.CellValueChanged
        Try
            If e.ColumnIndex = cTxtAmount.Index Then
                subCalcTotal()
            End If
            'subLockControls()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub SubLoadCustomerRewardPoints()
        Try
            Dim dsCustomer As New DataSet
            Dim objCustomer As New clsCustomer
            Dim iCnt As Long
            dsCustomer = objCustomer.GetsumRewardPointsByCustomer(sCustomerCode)
            If Not IsNothing(dsCustomer) Then
                If dsCustomer.Tables("Customer").Rows.Count > 0 Then
                    dRewardPoints = Trim(dsCustomer.Tables("Customer").Rows(iCnt)("RewardPoints") & "")
                    dRedeemPoints = Trim(dsCustomer.Tables("Customer").Rows(iCnt)("RedeemPoints") & "")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub SubLoadCustomerRedeemedPoints()
        Try
            Dim dsCustomer As New DataSet
            Dim objCustomer As New clsCustomer
            Dim sDate As String
            Dim iCnt As Long
            sDate = Format(ServerDate, "dd/MM/yyyy")
            dsCustomer = objCustomer.GetsumRedeemedPointsByCustomer(sCustomerCode, sDate)
            If Not IsNothing(dsCustomer) Then
                If dsCustomer.Tables("Customer").Rows.Count > 0 Then
                    dRedeemedPoints = Trim(dsCustomer.Tables("Customer").Rows(iCnt)("RedeemPoints") & "")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub SubLoadDeposit()
        Try
            Dim dsDeposit As New DataSet
            Dim objSalesReturn As New ClsSalesReturn
            Dim sDate As String
            Dim iCnt As Long
            sDate = Format(ServerDate, "dd/MM/yyyy")
            dsDeposit = objSalesReturn.GetDepositDetails(sCustomerCode)
            If Not IsNothing(dsDeposit) Then
                If dsDeposit.Tables("Deposit").Rows.Count > 0 Then
                    sSalesReturnNo = Trim(dsDeposit.Tables("Deposit").Rows(iCnt)("DepositNo") & "")
                    dDepositAmount = Val(dsDeposit.Tables("Deposit").Rows(iCnt)("Depositamount") & "") + Val(dsDeposit.Tables("Deposit").Rows(iCnt)("AmountPaid") & "")
                    sDepositNo = Trim(dsDeposit.Tables("Deposit").Rows(iCnt)("DepositNo") & "")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub SubLoadTradeINDeposit()
        Try
            Dim dsDeposit As New DataSet
            Dim objSalesReturn As New ClsSalesReturn
            Dim sDate As String
            Dim iCnt As Long
            sDate = Format(ServerDate, "dd/MM/yyyy")
            bTradeInSR = False
            dsDeposit = objSalesReturn.GetDepositTradeInDetails(sCustomerCode)
            If Not IsNothing(dsDeposit) Then
                If dsDeposit.Tables("Deposit").Rows.Count > 0 Then
                    bTradeInSR = True
                    sSalesReturnNo = Trim(dsDeposit.Tables("Deposit").Rows(iCnt)("SalesReturnNo") & "")
                    dSalesRetTradeIn = Val(dsDeposit.Tables("Deposit").Rows(iCnt)("Amount") & "")
                    dTradeInSales = dTradeInSales + Val(dsDeposit.Tables("Deposit").Rows(iCnt)("Amount") & "")
                    sDepositNo = Trim(dsDeposit.Tables("Deposit").Rows(iCnt)("SalesReturnNo") & "")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
