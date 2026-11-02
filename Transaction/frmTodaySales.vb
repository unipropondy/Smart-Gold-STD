Public Class frmTodaySales
    Inherits System.Windows.Forms.Form
    Dim objSales As New ClsSales
    Dim dm As DataManager

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
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtInstallment As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalSales As System.Windows.Forms.TextBox
    Friend WithEvents txtNetSales As System.Windows.Forms.TextBox
    Friend WithEvents txtInstallmentRcd As System.Windows.Forms.TextBox
    Friend WithEvents txtTradeInSales As System.Windows.Forms.TextBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPayMode As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtDifference As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmTodaySales))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtDifference = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtInstallment = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtTotalSales = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtPayMode = New System.Windows.Forms.TextBox
        Me.txtNetSales = New System.Windows.Forms.TextBox
        Me.txtInstallmentRcd = New System.Windows.Forms.TextBox
        Me.txtTradeInSales = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.pic = New System.Windows.Forms.PictureBox
        Me.btnClose = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txtDifference)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtInstallment)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.txtTotalSales)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtPayMode)
        Me.Panel1.Controls.Add(Me.txtNetSales)
        Me.Panel1.Controls.Add(Me.txtInstallmentRcd)
        Me.Panel1.Controls.Add(Me.txtTradeInSales)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Location = New System.Drawing.Point(8, 88)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(392, 336)
        Me.Panel1.TabIndex = 2
        '
        'txtDifference
        '
        Me.txtDifference.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(192, Byte))
        Me.txtDifference.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDifference.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDifference.ForeColor = System.Drawing.Color.Red
        Me.txtDifference.Location = New System.Drawing.Point(160, 272)
        Me.txtDifference.MaxLength = 20
        Me.txtDifference.Name = "txtDifference"
        Me.txtDifference.ReadOnly = True
        Me.txtDifference.Size = New System.Drawing.Size(216, 32)
        Me.txtDifference.TabIndex = 132
        Me.txtDifference.Tag = ""
        Me.txtDifference.Text = ""
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Label4.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(16, 278)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(144, 20)
        Me.Label4.TabIndex = 131
        Me.Label4.Text = "Excess/Shortage If Any"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(16, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(360, 20)
        Me.Label3.TabIndex = 130
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Label2.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(16, 198)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(144, 20)
        Me.Label2.TabIndex = 129
        Me.Label2.Text = "Net Sales"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtInstallment
        '
        Me.txtInstallment.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(192, Byte))
        Me.txtInstallment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInstallment.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInstallment.ForeColor = System.Drawing.Color.Black
        Me.txtInstallment.Location = New System.Drawing.Point(160, 112)
        Me.txtInstallment.MaxLength = 20
        Me.txtInstallment.Name = "txtInstallment"
        Me.txtInstallment.ReadOnly = True
        Me.txtInstallment.Size = New System.Drawing.Size(216, 32)
        Me.txtInstallment.TabIndex = 113
        Me.txtInstallment.Tag = ""
        Me.txtInstallment.Text = ""
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Label11.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(16, 118)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(144, 20)
        Me.Label11.TabIndex = 128
        Me.Label11.Text = "Installment"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTotalSales
        '
        Me.txtTotalSales.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(192, Byte))
        Me.txtTotalSales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalSales.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalSales.ForeColor = System.Drawing.Color.Black
        Me.txtTotalSales.Location = New System.Drawing.Point(160, 32)
        Me.txtTotalSales.MaxLength = 20
        Me.txtTotalSales.Name = "txtTotalSales"
        Me.txtTotalSales.ReadOnly = True
        Me.txtTotalSales.Size = New System.Drawing.Size(216, 32)
        Me.txtTotalSales.TabIndex = 121
        Me.txtTotalSales.Tag = ""
        Me.txtTotalSales.Text = ""
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Label1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(16, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(144, 20)
        Me.Label1.TabIndex = 122
        Me.Label1.Text = "Total Sales"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Label13.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(16, 158)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(144, 20)
        Me.Label13.TabIndex = 119
        Me.Label13.Text = "Installment.Received"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Label5.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(16, 238)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(144, 20)
        Me.Label5.TabIndex = 118
        Me.Label5.Text = "PayMode"
        '
        'txtPayMode
        '
        Me.txtPayMode.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(192, Byte))
        Me.txtPayMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPayMode.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayMode.ForeColor = System.Drawing.Color.Black
        Me.txtPayMode.Location = New System.Drawing.Point(160, 232)
        Me.txtPayMode.MaxLength = 20
        Me.txtPayMode.Name = "txtPayMode"
        Me.txtPayMode.ReadOnly = True
        Me.txtPayMode.Size = New System.Drawing.Size(216, 32)
        Me.txtPayMode.TabIndex = 117
        Me.txtPayMode.Tag = ""
        Me.txtPayMode.Text = ""
        '
        'txtNetSales
        '
        Me.txtNetSales.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(192, Byte))
        Me.txtNetSales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNetSales.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetSales.ForeColor = System.Drawing.Color.Black
        Me.txtNetSales.Location = New System.Drawing.Point(160, 192)
        Me.txtNetSales.MaxLength = 20
        Me.txtNetSales.Name = "txtNetSales"
        Me.txtNetSales.ReadOnly = True
        Me.txtNetSales.Size = New System.Drawing.Size(216, 32)
        Me.txtNetSales.TabIndex = 116
        Me.txtNetSales.Tag = ""
        Me.txtNetSales.Text = ""
        '
        'txtInstallmentRcd
        '
        Me.txtInstallmentRcd.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(192, Byte))
        Me.txtInstallmentRcd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInstallmentRcd.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInstallmentRcd.ForeColor = System.Drawing.Color.Black
        Me.txtInstallmentRcd.Location = New System.Drawing.Point(160, 152)
        Me.txtInstallmentRcd.MaxLength = 20
        Me.txtInstallmentRcd.Name = "txtInstallmentRcd"
        Me.txtInstallmentRcd.ReadOnly = True
        Me.txtInstallmentRcd.Size = New System.Drawing.Size(216, 32)
        Me.txtInstallmentRcd.TabIndex = 114
        Me.txtInstallmentRcd.Tag = ""
        Me.txtInstallmentRcd.Text = ""
        '
        'txtTradeInSales
        '
        Me.txtTradeInSales.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(192, Byte))
        Me.txtTradeInSales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTradeInSales.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTradeInSales.ForeColor = System.Drawing.Color.Black
        Me.txtTradeInSales.Location = New System.Drawing.Point(160, 72)
        Me.txtTradeInSales.MaxLength = 20
        Me.txtTradeInSales.Name = "txtTradeInSales"
        Me.txtTradeInSales.ReadOnly = True
        Me.txtTradeInSales.Size = New System.Drawing.Size(216, 32)
        Me.txtTradeInSales.TabIndex = 111
        Me.txtTradeInSales.Tag = ""
        Me.txtTradeInSales.Text = ""
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.Label6.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(16, 78)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(144, 20)
        Me.Label6.TabIndex = 112
        Me.Label6.Text = "Trade In Sales"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(8, 8)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(216, 75)
        Me.pic.TabIndex = 476166
        Me.pic.TabStop = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(325, 432)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.TabIndex = 476167
        Me.btnClose.Text = "Close"
        '
        'frmTodaySales
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(410, 464)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTodaySales"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Today Sales"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmTodaySales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        fncGetSalesToday()
        If UCase(sCompanyShortCode) = "HN" Then
            Label4.Visible = False
            Label5.Visible = False
            txtDifference.Visible = False
            txtPayMode.Visible = False
        End If

    End Sub
    Private Sub fncGetSalesToday()
        Dim sSql As String
        Dim drGetdate As DataRow()
        Dim DrNetSales As DataRow()
        Dim DrIns As DataRow()
        Dim DrTradeOnly As DataRow()
        Dim DrTradeAmt As DataRow()
        Dim DrInsRcd As DataRow()
        Dim DrPayMode As DataRow()
        Dim dTradeSales As Double
        Dim dTradeOnly As Double
        Dim dTradeTotal As Double
        Dim dDifference As Double
        Try
            'For TotalSales
            sSql = "Select TotalSales =Isnull(Sum(a.NetTotal),0) from tblSalesheader a where Invoicedate=convert(char(8),getdate(),112)   "
            If UCase(sCompanyShortCode) <> "HN" Then
                sSql = sSql & "and SalesType Not in('CRRETN','CSRETN')"
            End If
            DrNetSales = dm.RowReturnProjExecNonQryWithDataRow(sSql)
            If DrNetSales.Length > 0 Then
                txtTotalSales.Text = Format(DrNetSales(0)("TotalSales" & ""), "0.00")
            End If
            'For InstallmentC:\Durai\UGPS\UGPS\frmTradeInGold.vb
            sSql = "select  Installment =isnull(sum(a.nettotal -a.paidamt),0) from tblSalesHeader a where Invoicedate=convert(char(8),getdate(),112) and SalesType='INSTALLMENT'"
            DrIns = dm.RowReturnProjExecNonQryWithDataRow(sSql)
            If DrIns.Length > 0 Then
                txtInstallment.Text = Format(DrIns(0)("Installment" & ""), "0.00")
            End If
            'For Tradeinonly
            sSql = "select Tradeinonly =isnull(Sum(a.Totalcost),0) from tblTradeinheader a where invoiceno liKe'PV%' AND Convert(CHAR(8),CreateDate,112)=CONVERT(CHAR(8),getdate(),112)"
            DrTradeOnly = dm.RowReturnProjExecNonQryWithDataRow(sSql)
            If DrTradeOnly.Length > 0 Then
                dTradeSales = DrTradeOnly(0)("Tradeinonly" & "")
            End If
            'For TradeAmt
            sSql = "select Tradeamt =Isnull(Sum(a.Totalcost),0) from tblTradeinheader a where INVOICENO NOT LIKE'PV%' AND  Convert(CHAR(8),CreateDate,112)=CONVERT(CHAR(8),getdate(),112)"
            DrTradeAmt = dm.RowReturnProjExecNonQryWithDataRow(sSql)
            If DrTradeAmt.Length > 0 Then
                dTradeOnly = DrTradeAmt(0)("Tradeamt" & "")
            End If
            dTradeTotal = Val(dTradeSales) + Val(dTradeOnly)
            txtTradeInSales.Text = Format(dTradeTotal, "0.00")
            'For InstallmentRcd
            sSql = "select InstallmentRcd =isnull(Sum(amtPaid),0) from tblInstallmentdetail where Convert(CHAR(8),CreateDate,112)=CONVERT(CHAR(8),getdate(),112)"
            DrInsRcd = dm.RowReturnProjExecNonQryWithDataRow(sSql)
            If DrInsRcd.Length > 0 Then
                txtInstallmentRcd.Text = Format(DrInsRcd(0)("InstallmentRcd" & ""), "0.00")
            End If
            'NetSales
            txtNetSales.Text = Format(Val(txtTotalSales.Text) - Val(txtTradeInSales.Text) - Val(txtInstallment.Text) + Val(txtInstallmentRcd.Text), "0.00")
            'PayMode
            sSql = "select PayMode =isnull(Sum(amount + Commision) ,0) from tblPayModeDetail where Convert(CHAR(8),InvoiceDate,112)=CONVERT(CHAR(8),getdate(),112)"
            DrPayMode = dm.RowReturnProjExecNonQryWithDataRow(sSql)
            If DrPayMode.Length > 0 Then
                txtPayMode.Text = Format(DrPayMode(0)("PayMode" & ""), "0.00")
            End If
            'If any Excess or Shortage ''''
            If UCase(sCompanyShortCode) <> "HN" Then
                dDifference = Val(txtNetSales.Text) - Val(txtPayMode.Text)
                If dDifference <> 0 Then
                    txtDifference.Text = Format(dDifference, "0.00")
                Else
                    txtDifference.Visible = False
                    Label4.Visible = False
                End If
            Else
                txtDifference.Visible = False
                txtPayMode.Visible = False
                Label4.Visible = False
                Label5.Visible = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
