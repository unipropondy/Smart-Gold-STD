
Imports System.Data
Imports System.Data.SqlClient

Imports System.Globalization
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Text
Public Class frmMisc


    Inherits System.Windows.Forms.Form
    Dim objMisc As New clsMisc
    Dim objMiscHistory As New clsMiscHistory
    Public sSelect As String
    Public sGSTPerc As String
    Public gsReportPath As String = Application.StartupPath & "\Reports\"
    Public gsAddress As String
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Public gsTelephone As Integer
    


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
    Friend WithEvents cboType As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtVoucherNo As System.Windows.Forms.TextBox
    Friend WithEvents cmdFetch As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtNetAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtPerson As System.Windows.Forms.TextBox
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents txtUserId As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmdReprint As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtGST As System.Windows.Forms.TextBox
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtGSTPerc As System.Windows.Forms.TextBox
    Friend WithEvents chkZeroGST As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMisc))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboType = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtVoucherNo = New System.Windows.Forms.TextBox()
        Me.cmdFetch = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtNetAmount = New System.Windows.Forms.TextBox()
        Me.txtWeight = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtPerson = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtUserId = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtGSTPerc = New System.Windows.Forms.TextBox()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.txtGST = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.chkZeroGST = New System.Windows.Forms.CheckBox()
        Me.cmdReprint = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(32, 160)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 23)
        Me.Label1.TabIndex = 476203
        Me.Label1.Text = "Type"
        '
        'cboType
        '
        Me.cboType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.cboType.Location = New System.Drawing.Point(112, 160)
        Me.cboType.Name = "cboType"
        Me.cboType.Size = New System.Drawing.Size(121, 21)
        Me.cboType.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(256, 160)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 23)
        Me.Label2.TabIndex = 476205
        Me.Label2.Text = "Voucher No"
        '
        'txtVoucherNo
        '
        Me.txtVoucherNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtVoucherNo.Location = New System.Drawing.Point(336, 160)
        Me.txtVoucherNo.Name = "txtVoucherNo"
        Me.txtVoucherNo.Size = New System.Drawing.Size(100, 20)
        Me.txtVoucherNo.TabIndex = 2
        '
        'cmdFetch
        '
        Me.cmdFetch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdFetch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.cmdFetch.Location = New System.Drawing.Point(440, 160)
        Me.cmdFetch.Name = "cmdFetch"
        Me.cmdFetch.Size = New System.Drawing.Size(75, 23)
        Me.cmdFetch.TabIndex = 3
        Me.cmdFetch.Text = "Fetch"
        Me.cmdFetch.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(32, 200)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 23)
        Me.Label3.TabIndex = 476208
        Me.Label3.Text = "Date"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(32, 232)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 23)
        Me.Label4.TabIndex = 476210
        Me.Label4.Text = "Net Amount"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(32, 272)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 23)
        Me.Label5.TabIndex = 476211
        Me.Label5.Text = "Amount"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label6.Location = New System.Drawing.Point(32, 312)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 23)
        Me.Label6.TabIndex = 476212
        Me.Label6.Text = "Weight"
        '
        'txtNetAmount
        '
        Me.txtNetAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtNetAmount.Location = New System.Drawing.Point(112, 232)
        Me.txtNetAmount.Name = "txtNetAmount"
        Me.txtNetAmount.Size = New System.Drawing.Size(120, 20)
        Me.txtNetAmount.TabIndex = 5
        '
        'txtWeight
        '
        Me.txtWeight.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtWeight.Location = New System.Drawing.Point(112, 312)
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.Size = New System.Drawing.Size(120, 20)
        Me.txtWeight.TabIndex = 7
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(32, 344)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 23)
        Me.Label7.TabIndex = 476216
        Me.Label7.Text = "Person"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label8.Location = New System.Drawing.Point(32, 384)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(72, 23)
        Me.Label8.TabIndex = 476217
        Me.Label8.Text = "Remarks"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label9.Location = New System.Drawing.Point(32, 424)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(72, 23)
        Me.Label9.TabIndex = 476218
        Me.Label9.Text = "User Id"
        '
        'txtPerson
        '
        Me.txtPerson.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtPerson.Location = New System.Drawing.Point(112, 344)
        Me.txtPerson.Name = "txtPerson"
        Me.txtPerson.Size = New System.Drawing.Size(384, 20)
        Me.txtPerson.TabIndex = 10
        '
        'txtRemarks
        '
        Me.txtRemarks.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtRemarks.Location = New System.Drawing.Point(112, 384)
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(384, 20)
        Me.txtRemarks.TabIndex = 11
        '
        'txtUserId
        '
        Me.txtUserId.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtUserId.Location = New System.Drawing.Point(112, 424)
        Me.txtUserId.Name = "txtUserId"
        Me.txtUserId.Size = New System.Drawing.Size(120, 20)
        Me.txtUserId.TabIndex = 12
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label10.Location = New System.Drawing.Point(240, 120)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(72, 23)
        Me.Label10.TabIndex = 476222
        Me.Label10.Text = "GST %"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(336, 312)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(104, 20)
        Me.TextBox1.TabIndex = 476225
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txtGSTPerc)
        Me.Panel1.Controls.Add(Me.DateTimePicker1)
        Me.Panel1.Controls.Add(Me.txtGST)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.chkZeroGST)
        Me.Panel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Panel1.Location = New System.Drawing.Point(16, 112)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(512, 344)
        Me.Panel1.TabIndex = 476226
        '
        'txtGSTPerc
        '
        Me.txtGSTPerc.Location = New System.Drawing.Point(320, 120)
        Me.txtGSTPerc.Name = "txtGSTPerc"
        Me.txtGSTPerc.Size = New System.Drawing.Size(54, 20)
        Me.txtGSTPerc.TabIndex = 476229
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(96, 88)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(120, 20)
        Me.DateTimePicker1.TabIndex = 4
        Me.DateTimePicker1.Value = New Date(2011, 3, 7, 0, 0, 0, 0)
        '
        'txtGST
        '
        Me.txtGST.Location = New System.Drawing.Point(320, 160)
        Me.txtGST.Name = "txtGST"
        Me.txtGST.Size = New System.Drawing.Size(54, 20)
        Me.txtGST.TabIndex = 9
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(240, 160)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(72, 23)
        Me.Label11.TabIndex = 476228
        Me.Label11.Text = "GST "
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(160, 8)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(168, 23)
        Me.Label12.TabIndex = 476227
        Me.Label12.Text = "Miscellaneous Entry"
        '
        'chkZeroGST
        '
        Me.chkZeroGST.Location = New System.Drawing.Point(392, 120)
        Me.chkZeroGST.Name = "chkZeroGST"
        Me.chkZeroGST.Size = New System.Drawing.Size(96, 24)
        Me.chkZeroGST.TabIndex = 476231
        Me.chkZeroGST.Text = "Zero Rated"
        '
        'cmdReprint
        '
        Me.cmdReprint.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdReprint.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.cmdReprint.Location = New System.Drawing.Point(280, 472)
        Me.cmdReprint.Name = "cmdReprint"
        Me.cmdReprint.Size = New System.Drawing.Size(75, 23)
        Me.cmdReprint.TabIndex = 15
        Me.cmdReprint.Text = "&Reprint"
        Me.cmdReprint.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.cmdCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.cmdCancel.Location = New System.Drawing.Point(368, 472)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 16
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.cmdClose.Location = New System.Drawing.Point(456, 472)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(75, 23)
        Me.cmdClose.TabIndex = 17
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(368, 472)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(75, 23)
        Me.Button6.TabIndex = 476230
        Me.Button6.Text = "Button4"
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(280, 472)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(75, 23)
        Me.Button7.TabIndex = 476229
        Me.Button7.Text = "Button3"
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.cmdDelete.Location = New System.Drawing.Point(192, 472)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(75, 23)
        Me.cmdDelete.TabIndex = 14
        Me.cmdDelete.Text = "&Delete"
        Me.cmdDelete.UseVisualStyleBackColor = False
        '
        'cmdAdd
        '
        Me.cmdAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.cmdAdd.Location = New System.Drawing.Point(104, 472)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(75, 23)
        Me.cmdAdd.TabIndex = 13
        Me.cmdAdd.Text = "&Add"
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'txtAmount
        '
        Me.txtAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtAmount.Location = New System.Drawing.Point(112, 272)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(120, 20)
        Me.txtAmount.TabIndex = 6
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(16, 6)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(270, 100)
        Me.PictureBox1.TabIndex = 476231
        Me.PictureBox1.TabStop = False
        '
        'frmMisc
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(544, 509)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdReprint)
        Me.Controls.Add(Me.txtUserId)
        Me.Controls.Add(Me.txtRemarks)
        Me.Controls.Add(Me.txtPerson)
        Me.Controls.Add(Me.txtWeight)
        Me.Controls.Add(Me.txtAmount)
        Me.Controls.Add(Me.txtNetAmount)
        Me.Controls.Add(Me.txtVoucherNo)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmdFetch)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboType)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.cmdDelete)
        Me.Controls.Add(Me.cmdAdd)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmMisc"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Misc Entry"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub frmMisc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cboType.Text = ""
        If UCase(sCompanyShortCode) = "SSJ" Then
            cboType.Items.Add("Services")
        Else
            cboType.Items.Add("Purchase Note")
            cboType.Items.Add("Credit Note")
            cboType.Items.Add("Misc.Note")
            cboType.Items.Add("Petty Cash")
        End If
        DateTimePicker1.Value = CStr(Format(ServerDate, "dd/MM/yyyy"))

        Dim DM As New DataManager
        sSelect = "select GSTPerc from tblcompany"

        Dim DrCus() As DataRow
        DrCus = DM.RowReturnProjExecNonQryWithDataRow(sSelect)
        If DrCus.Length > 0 Then
            txtGSTPerc.Text = DrCus(0)("GSTPerc") & ""
            sGSTPerc = DrCus(0)("GSTPerc") & ""
        End If
        txtGSTPerc.ReadOnly = True
    End Sub

    Private Sub cmdReprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReprint.Click
        
        Dim sSql
        Dim strCompanyname, strAddress, strReportTitle, strpath, strselectformula, strDateFrom, strDateTo As String
        Dim strSubHead As String
        Dim CR As New ReportDocument
        Dim frmRep As New frmReports
        Dim SBll As New clsCompany1
        Dim ds As DataSet
        Dim SType As String
        On Error GoTo ErrTrap

        Me.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        ds = SBll.getCompanyName
        If ds.Tables("GetCompanyName").Rows.Count > 0 Then
            strCompanyname = Trim(ds.Tables("GetCompanyName").Rows(0)("Name") & "")
            strAddress = Trim(ds.Tables("GetCompanyName").Rows(0)("Address1") & "") & " " & _
                         Trim(ds.Tables("GetCompanyName").Rows(0)("Address2") & "") & " " & _
                         Trim(ds.Tables("GetCompanyName").Rows(0)("City") & "")
        End If
        
        strReportTitle = ""
        strpath = Application.StartupPath & "\Reports\rptMiscJournalNew.rpt"
        strReportTitle = "Miscellaneous Journal Report "

        CR.Load(strpath)
        If Not ReportLogOnToServer(CR) Then
            Exit Sub
        End If
        strselectformula = ""
        CR.Refresh()
        frmRep.ReportViewer.ReportSource = CR

        Select Case Trim(cboType.Text)
            Case "Purchase Note"
                SType = "PN"
            Case "Credit Note"
                SType = "CN"
            Case "Misc.Note"
                SType = "MC"
            Case "Petty Cash"
                SType = "PC"
            Case "Services"
                SType = "SR"
        End Select
        strselectformula = " {tblMisc.VoucherNo} = " & Val(txtVoucherNo.Text.Trim)
        strselectformula = strselectformula & " and {tblMisc.Type} = " & fncFormatSQLText(SType)
        frmRep.ReportViewer.SelectionFormula = strselectformula
        CR.DataDefinition.RecordSelectionFormula = strselectformula
        CR.PrintToPrinter(1, True, 0, 0)

        ''frmRep.ShowDialog()
        ''frmRep.ReportViewer.Visible = True
        ''frmRep.ReportViewer.DisplayToolbar = True
        ''frmRep.ReportViewer.CloseView(False)
        ''frmRep.WindowState = FormWindowState.Maximized
        frmRep = Nothing
        frmRep = New frmReports
        Me.Cursor.Current = System.Windows.Forms.Cursors.Default
        cmdAdd.Enabled = True
        Exit Sub
ErrTrap:
        Me.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
        Err.Clear()

    End Sub


    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents cmdDelete As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox

    

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Dim sType As String
        Dim iVoucherNo As Integer
        Dim sDate As String
        Dim sPerson As String
        Dim dAmount As Decimal
        Dim dGSTPerc As Decimal
        Dim dGST As Decimal
        Dim dNetAmount As Decimal
        Dim dWeight As Decimal
        Dim sRemarks As String
        Dim sCreateUser As String
        Dim sModifyUser As String



        If cboType.Text = "" Then
            MsgBox("Select Type", MsgBoxStyle.Information)
            cboType.Focus()
        End If
        If txtVoucherNo.Text = "" Then
            MsgBox("Enter VoucherNo", MsgBoxStyle.Information)
            txtVoucherNo.Focus()
        End If
        If Val(txtAmount.Text & "") = 0 Then
            MsgBox("Enter Amount", MsgBoxStyle.Information)
            txtAmount.Focus()
        End If
        Select Case Trim(cboType.Text)
            Case "Purchase Note"
                sType = "PN"
            Case "Credit Note"
                sType = "CN"
            Case "Misc.Note"
                sType = "MC"
            Case "Petty Cash"
                sType = "PC"
            Case "Services"
                sType = "SR"
        End Select
        sDate = Format(DateTimePicker1.Value, "dd/MM/yyyy")
        Try

            If objMisc.MiscSave(sType, txtVoucherNo.Text, sDate, txtPerson.Text.Trim, _
                                Val(txtAmount.Text & ""), Val(txtGSTPerc.Text & ""), Val(txtGST.Text & ""), Val(txtNetAmount.Text & ""), Val(txtWeight.Text & ""), txtRemarks.Text.Trim) Then
                MessageBox.Show("Miscellaneous details saved Successfully")
                cmdReprint_Click(sender, e)
                cmdCancel_Click(sender, e)
                cboType.Focus()
            Else
                MessageBox.Show("Error in Save")
            End If


            '''If objMiscHistory.MiscSaveHistory(sType, txtVoucherNo.Text, sDate, txtPerson.Text.Trim, _
            '''                                  Val(txtAmount.Text & ""), Val(txtGSTPerc.Text & ""), Val(txtGST.Text & ""), Val(txtNetAmount.Text & ""), Val(txtWeight.Text & ""), txtRemarks.Text.Trim) Then
            '''    MessageBox.Show("Miscellaneous details saved Successfully")
            '''    cmdCancel_Click(sender, e)
            '''    cboType.Focus()
            '''Else
            '''    MessageBox.Show("Error in Save")
            '''End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub



    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        Dim sType As String

        Try
            If cboType.Text.Trim <> "" Then
                'Dim sType As String
                sType = cboType.Text.Trim
                Select Case Trim(cboType.Text)
                    Case "Purchase Note"
                        sType = "PN"
                    Case "Credit Note"
                        sType = "CN"
                    Case "Misc.Note"
                        sType = "MC"
                    Case "Petty Cash"
                        sType = "PC"
                    Case "Services"
                        sType = "SR"
                End Select

                If MessageBox.Show("Do you want to delete " & sType & "Type?", "GoldPOS", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    If objMisc.DeleteMisc(sType, txtVoucherNo.Text) Then
                        'MessageBox.Show("Miscellaneous details deleted.")
                        ''ClearMe()
                        'cboType.Focus()

                    Else
                        MessageBox.Show("Error in deleting Member detail.")
                    End If

                    If objMiscHistory.DeleteMiscHistory(sType, txtVoucherNo.Text) Then
                        MessageBox.Show("Miscellaneous details deleted.")
                        'ClearMe()
                        cmdCancel_Click(sender, e)
                        cboType.Focus()
                        cboType_TextChanged(sender, e)
                        cmdAdd.Enabled = True
                    Else
                        MessageBox.Show("Error in deleting Member detail.")
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Dispose()

    End Sub

    Private Sub cmdFetch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFetch.Click
        Dim sSelect As String
        Dim sType As String
        Dim Dm As New DataManager


        Select Case Trim(cboType.Text)
            Case "Purchase Note"
                sType = "PN"
            Case "Credit Note"
                sType = "CN"
            Case "Misc.Note"
                sType = "MC"
            Case "Petty Cash"
                sType = "PC"
            Case "Services"
                sType = "SR"
        End Select
        sSelect = "select * from tblMisc where Type=" & fncFormatSQLText(sType) & "and VoucherNo=" & fncFormatSQLText(txtVoucherNo.Text)

        Dim DrCus() As DataRow
        DrCus = Dm.RowReturnProjExecNonQryWithDataRow(sSelect)
        If DrCus.Length > 0 Then
            DateTimePicker1.Value = DrCus(0)("Date") & ""
            txtNetAmount.Text = DrCus(0)("NetAmount") & ""
            txtAmount.Text = DrCus(0)("Amount") & ""
            txtGSTPerc.Text = DrCus(0)("GSTPerc") & ""
            txtGST.Text = DrCus(0)("GST") & ""
            txtWeight.Text = DrCus(0)("Weight") & ""
            txtPerson.Text = DrCus(0)("Person") & ""
            txtRemarks.Text = DrCus(0)("Remarks") & ""
            txtUserId.Text = DrCus(0)("CreateUser") & ""

            'If Val((DrCus(0)("GSTType") & "")) = 1 Then
            '    chkGST.Checked = True
            'Else
            '    chkGST.Checked = False
            cmdAdd.Enabled = False
        End If

    End Sub

    

    Private Sub cboType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboType.SelectedIndexChanged
        'cboType.SelectedIndex = cboType.SelectedIndex
        'cboType.Text = Mid(cboType.Text, InStr(cboType.Text, ".") + 1)

        Dim iLoop As Integer
        Dim blnAuto As Boolean
        'Match capitalization if item entered is one on the list
        If cboType.Text <> "" Then
            For iLoop = 0 To cboType.Items.Count - 1
                If UCase$(cboType.Items(iLoop)) = UCase$(cboType.Text) Then
                    blnAuto = True
                    cboType.Text = cboType.Items(iLoop)
                    blnAuto = False
                    Exit For
                End If
            Next iLoop
        End If



    End Sub



    Private Sub txtPerson_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPerson.TextChanged

    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        ' cboType.Items.Clear()
        txtVoucherNo.Clear()
        'DateTimePicker1.Value = ""
        txtNetAmount.Clear()
        txtAmount.Clear()
        txtWeight.Clear()
        txtGSTPerc.Clear()
        txtGST.Clear()
        txtPerson.Clear()
        txtRemarks.Clear()
        txtUserId.Clear()

    End Sub

    Private Sub frmMisc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub txtNetAmount_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNetAmount.Leave
        txtGST.Text = txtNetAmount.Text - (txtNetAmount.Text / (1 + Val(txtGSTPerc.Text) / 100))
        txtAmount.Text = txtNetAmount.Text - txtGST.Text
        txtWeight.Focus()
    End Sub

    Private Sub txtNetAmount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNetAmount.TextChanged

    End Sub

    Private Sub cboType_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboType.KeyUp
        '        On Error GoTo ErrorSub
        '        If e.KeyCode = Keys.Down Then
        '            cboType.Select(1)
        '            cboType.Focus()
        '        End If
        '        'cmdSearch_Click(sender, e)
        '        Exit Sub
        'ErrorSub:
        '        MsgBox(Err.Description)
        '        Err.Clear()

        Dim sTypedText As String
        Dim iFoundIndex As Integer
        Dim oFoundItem As Object
        Dim sFoundText As String
        Dim sAppendText As String

        'Allow select keys without autocompleting
        Select Case e.KeyCode
            Case Keys.Back, Keys.Left, Keys.Right, Keys.Delete, Keys.Down, Keys.Up, Keys.Home, Keys.End
                Return
        End Select

        'Get the Typed Text and find it in the list
        sTypedText = cboType.Text
        iFoundIndex = cboType.FindString(sTypedText)

        'If we found the typed text in the list then autocomplete
        If iFoundIndex >= 0 Then
            'Get the item from the list (Return Type depends if Datasource was bound or List Created)
            oFoundItem = cboType.Items(iFoundIndex)

            'Use the ListControl.GetItemText to resolve the Name in case the Combo was Data bound
            sFoundText = cboType.GetItemText(oFoundItem)

            'Append then found text to the typed text to preserve case
            sAppendText = sFoundText.Substring(sTypedText.Length)
            cboType.Text = sTypedText & sAppendText

            'Select the Appended text
            cboType.SelectionStart = sTypedText.Length
            cboType.SelectionLength = sAppendText.Length
        End If
    End Sub

    Private Sub txtGSTPerc_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGSTPerc.TextChanged

    End Sub

    Private Sub cboType_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboType.TextChanged
        
        Dim sSelect As String
        Dim sType As String
        Dim Dm As New DataManager


        Select Case Trim(cboType.Text)
            Case "Purchase Note"
                sType = "PN"
            Case "Credit Note"
                sType = "CN"
            Case "Misc.Note"
                sType = "MC"
            Case "Petty Cash"
                sType = "PC"
            Case "Services"
                sType = "SR"
        End Select
        '''sSelect = "select right('000000' + cast(isnull(max(VoucherNO),0)+1 as varchar(20)),6) [VoucherNo] from tblmisc where Type=" & fncFormatSQLText(sType)
        sSelect = "select  isnull(max(VoucherNO),0)+1 [VoucherNo] from tblmisc where Type=" & fncFormatSQLText(sType)

        Dim DrCus() As DataRow
        DrCus = Dm.RowReturnProjExecNonQryWithDataRow(sSelect)
        If DrCus.Length > 0 Then
            txtVoucherNo.Text = DrCus(0)("VoucherNo") & ""
        End If


    End Sub

    Private Sub chkZeroGST_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkZeroGST.CheckedChanged

        If chkZeroGST.Checked = True Then
            txtGSTPerc.Text = 0
            If Val(txtNetAmount.Text) > 0 Then
                txtGST.Text = txtNetAmount.Text - (txtNetAmount.Text / (1 + Val(txtGSTPerc.Text) / 100))
                txtAmount.Text = txtNetAmount.Text - txtGST.Text
                txtAmount.Text = txtNetAmount.Text
            End If
        Else
            txtGSTPerc.Text = sGSTPerc
            If Val(txtNetAmount.Text) > 0 Then
                txtGST.Text = txtNetAmount.Text - (txtNetAmount.Text / (1 + Val(txtGSTPerc.Text) / 100))
                txtAmount.Text = txtNetAmount.Text - txtGST.Text
                txtWeight.Focus()
            End If
        End If
    End Sub
End Class
