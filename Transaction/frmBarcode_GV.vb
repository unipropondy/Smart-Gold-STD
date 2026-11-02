Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmBarcode_GV


    Inherits System.Windows.Forms.Form
    Dim objInventory As New clsInventory
    Dim objTrade As New clsTradeIN
    Dim sClass As String
    Dim sCat As String
    Dim sMc As Double
    Dim GlOSS As Double
    Dim MCTitle As String


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
    Friend WithEvents lblLocationCode As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents optInventory As System.Windows.Forms.RadioButton
    Friend WithEvents optTrade As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents txtWeight As AxEditLib.AxfpDoubleSingle
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    'Friend WithEvents comBarCode As AxMSCommLib.AxMSComm
    Friend WithEvents lblInventory As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtQty As AxEditLib.AxfpDoubleSingle
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents txtInventoryCode As AxEditLib.AxfpText
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmBarcode_GV))
        Me.lblLocationCode = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.optTrade = New System.Windows.Forms.RadioButton
        Me.optInventory = New System.Windows.Forms.RadioButton
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtInventoryCode = New AxEditLib.AxfpText
        Me.txtQty = New AxEditLib.AxfpDoubleSingle
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtWeight = New AxEditLib.AxfpDoubleSingle
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtDescription = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblInventory = New System.Windows.Forms.Label
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnPrint = New System.Windows.Forms.Button
        Me.pic = New System.Windows.Forms.PictureBox
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.txtInventoryCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblLocationCode
        '
        Me.lblLocationCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLocationCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationCode.ForeColor = System.Drawing.Color.Black
        Me.lblLocationCode.Location = New System.Drawing.Point(96, 96)
        Me.lblLocationCode.Name = "lblLocationCode"
        Me.lblLocationCode.Size = New System.Drawing.Size(192, 32)
        Me.lblLocationCode.TabIndex = 476188
        Me.lblLocationCode.Text = "Reprint Barcode Tag"
        Me.lblLocationCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.optTrade)
        Me.GroupBox1.Controls.Add(Me.optInventory)
        Me.GroupBox1.Location = New System.Drawing.Point(112, 152)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(169, 48)
        Me.GroupBox1.TabIndex = 476189
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Reprint Barcode Choice By"
        '
        'optTrade
        '
        Me.optTrade.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTrade.Location = New System.Drawing.Point(102, 19)
        Me.optTrade.Name = "optTrade"
        Me.optTrade.Size = New System.Drawing.Size(56, 16)
        Me.optTrade.TabIndex = 1
        Me.optTrade.Text = "Trade"
        '
        'optInventory
        '
        Me.optInventory.Checked = True
        Me.optInventory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optInventory.Location = New System.Drawing.Point(12, 19)
        Me.optInventory.Name = "optInventory"
        Me.optInventory.Size = New System.Drawing.Size(80, 16)
        Me.optInventory.TabIndex = 1
        Me.optInventory.TabStop = True
        Me.optInventory.Text = "Inventory"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtInventoryCode)
        Me.GroupBox2.Controls.Add(Me.txtQty)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.txtWeight)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.txtDescription)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.lblInventory)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(16, 200)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(360, 94)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'txtInventoryCode
        '
        Me.txtInventoryCode.ContainingControl = Me
        Me.txtInventoryCode.Location = New System.Drawing.Point(123, 13)
        Me.txtInventoryCode.Name = "txtInventoryCode"
        Me.txtInventoryCode.OcxState = CType(resources.GetObject("txtInventoryCode.OcxState"), System.Windows.Forms.AxHost.State)
        Me.txtInventoryCode.Size = New System.Drawing.Size(233, 21)
        Me.txtInventoryCode.TabIndex = 2
        '
        'txtQty
        '
        Me.txtQty.ContainingControl = Me
        Me.txtQty.Location = New System.Drawing.Point(288, 64)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.OcxState = CType(resources.GetObject("txtQty.OcxState"), System.Windows.Forms.AxHost.State)
        Me.txtQty.Size = New System.Drawing.Size(64, 23)
        Me.txtQty.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(240, 69)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 16)
        Me.Label2.TabIndex = 43
        Me.Label2.Text = "Qty"
        '
        'txtWeight
        '
        Me.txtWeight.ContainingControl = Me
        Me.txtWeight.Location = New System.Drawing.Point(124, 65)
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.OcxState = CType(resources.GetObject("txtWeight.OcxState"), System.Windows.Forms.AxHost.State)
        Me.txtWeight.Size = New System.Drawing.Size(104, 23)
        Me.txtWeight.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(24, 65)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 16)
        Me.Label3.TabIndex = 42
        Me.Label3.Text = "Weight"
        '
        'txtDescription
        '
        Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.Location = New System.Drawing.Point(124, 40)
        Me.txtDescription.MaxLength = 20
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(228, 20)
        Me.txtDescription.TabIndex = 3
        Me.txtDescription.Tag = "NE;"
        Me.txtDescription.Text = ""
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(24, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 16)
        Me.Label1.TabIndex = 38
        Me.Label1.Text = "Description"
        '
        'lblInventory
        '
        Me.lblInventory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInventory.Location = New System.Drawing.Point(24, 16)
        Me.lblInventory.Name = "lblInventory"
        Me.lblInventory.Size = New System.Drawing.Size(104, 16)
        Me.lblInventory.TabIndex = 36
        Me.lblInventory.Text = "Inventory Code"
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(298, 304)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(76, 23)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "Close(Esc)"
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.Location = New System.Drawing.Point(218, 304)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(76, 23)
        Me.btnPrint.TabIndex = 6
        Me.btnPrint.Text = "Print(F9)"
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(8, 8)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(210, 75)
        Me.pic.TabIndex = 476190
        Me.pic.TabStop = False
        '
        'frmBarcode_GV
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 13)
        Me.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ClientSize = New System.Drawing.Size(386, 328)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lblLocationCode)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmBarcode_GV"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BarCode"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.txtInventoryCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWeight, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If txtInventoryCode.Text.Trim <> "" Then
                subBarcodeReportPrint()
                MCTitle = ""
                sClass = ""
                sMc = 0
                GlOSS = 0
                txtInventoryCode.Text = ""
                txtDescription.Text = ""
                txtWeight.Text = ""
                txtInventoryCode.Focus()
            Else
                MessageBox.Show("Select InventoryCode!")
                txtInventoryCode.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subBarcodeReportPrint()

        Dim sSql$
        Dim sTemp As String
        Dim sChar As String
        Dim strCompanyname, strAddress, strReportTitle, strpath, strselectformula, strDateFrom, strDateTo As String
        Dim strSubHead As String
        Dim CR As New ReportDocument
        Dim frmRep As New frmReports
        Dim objCompany As New clsCompany
        Dim ds As DataSet
        Try
            strpath = Application.StartupPath & "\Reports\BARCODE_GVPOSTEK.rpt"
            Me.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            ds = objCompany.getCompanyName
            If ds.Tables("GetCompanyName").Rows.Count > 0 Then
                strCompanyname = Trim(ds.Tables("GetCompanyName").Rows(0)("Name") & "")
                strAddress = Trim(ds.Tables("GetCompanyName").Rows(0)("Address1") & "") & " " & _
                             Trim(ds.Tables("GetCompanyName").Rows(0)("Address2") & "") & " " & _
                             Trim(ds.Tables("GetCompanyName").Rows(0)("City") & "")
            End If
            frmRep.GetModule = "Barcode"
            frmRep.GetReportType = "Summary"
            frmRep.GetPrintOption = "Preview"

            CR.Load(strpath)
            ReportLogOnToServer(CR)
            CR.Refresh()
            frmRep.ReportViewer.ReportSource = CR
            strselectformula = "{tblInventory.InventoryCode}=" & fncFormatSQLText(Trim(txtInventoryCode.Text))

            CR.DataDefinition.FormulaFields("InventoryCode").Text = fncFormatSQLText(txtInventoryCode.Text.Trim)
            CR.DataDefinition.FormulaFields("Weight").Text = fncFormatSQLText(Format(Val(txtWeight.Text.Trim), "###0.00") + "gm")
            CR.DataDefinition.FormulaFields("GLOSS").Text = fncFormatSQLText(Format(GlOSS, "###0.00") + "gm")
            CR.DataDefinition.FormulaFields("TotWeight").Text = fncFormatSQLText(Format(Val(txtWeight.Text) + Val(GlOSS), "###0.00") + "gm")
            CR.DataDefinition.FormulaFields("sTemp").Text = fncFormatSQLText(sTemp)
            CR.DataDefinition.FormulaFields("MC").Text = fncFormatSQLText(Trim("MC:") + "$" + Trim(sChar))
            CR.DataDefinition.FormulaFields("GOLDEN VILLA").Text = fncFormatSQLText("GOLDEN VILLA")


            frmRep.ReportViewer.SelectionFormula = strselectformula

            frmRep.ShowDialog()
            frmRep.ReportViewer.Visible = True
            frmRep.ReportViewer.DisplayToolbar = True
            frmRep.ReportViewer.CloseView(False)
            frmRep.WindowState = FormWindowState.Maximized
            Me.Cursor.Current = System.Windows.Forms.Cursors.Default
            CR.Close()
            CR.Dispose()
            CR = Nothing


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub frmBarcode_GV_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Clearall()
        'comBarCode.PortOpen = False
    End Sub
    Private Sub Clearall()
        txtInventoryCode.Text = ""
        txtDescription.Clear()
        txtWeight.Text = ""
        txtQty.Text = "1"
        optInventory.Checked = True
    End Sub

    Private Sub optInventory_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optInventory.CheckedChanged
        Try
            lblInventory.Text = "Inventory Code"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub optTrade_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optTrade.CheckedChanged
        Try
            lblInventory.Text = "Cash Bill No"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtInventoryCode_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInventoryCode.LostFocus
        Try
            If txtInventoryCode.Text.Trim <> "" Then
                If optInventory.Checked Then
                    subGetInventory()
                Else
                    subGetTradeInventory()
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subGetInventory()
        Dim Ds As DataSet
        Try
            Ds = objInventory.GetInventoryDetail(txtInventoryCode.Text.Trim)
            If Ds.Tables("tblInventory").Rows.Count > 0 Then
                txtDescription.Text = Ds.Tables("tblInventory").Rows(0)("Description")
                txtWeight.Text = Ds.Tables("tblInventory").Rows(0)("Weight")
                GlOSS = Ds.Tables("tblInventory").Rows(0)("GoldLoss")
                sMc = Ds.Tables("tblInventory").Rows(0)("sMc")
                sClass = Ds.Tables("tblInventory").Rows(0)("ClassCode")
                sCat = Ds.Tables("tblInventory").Rows(0)("CategoryCode")
                MCTitle = "MC:"
            End If
            Ds = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subGetTradeInventory()
        Dim ds As DataSet
        Try
            ds = objTrade.GetTradeInBarCode(txtInventoryCode.Text.Trim)
            If ds.Tables("TradeIn").Rows.Count > 0 Then
                txtDescription.Text = "TRADE IN GOLD "
                txtWeight.Text = ds.Tables("TradeIn").Rows(0)("TotalWeight")
                GlOSS = ds.Tables("TradeIn").Rows(0)("GoldLoss")
                sMc = ds.Tables("TradeIn").Rows(0)("sMc")
                sClass = ""
                MCTitle = "COST"
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtQty_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.LostFocus
        If Val(txtQty.Text) = 0 Then
            txtQty.Text = 1
        End If
    End Sub

    Private Sub frmBarcode_GV_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                btnClose_Click(sender, e)
            Case Keys.F9
                btnPrint_Click(sender, e)
            Case Keys.Up
                SendKeys.Send("+{Tab}")
            Case Keys.Down
                SendKeys.Send("{Tab}")
            Case Keys.Enter
                SendKeys.Send("{Tab}")
        End Select
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub txtInventoryCode_ButtonHit(ByVal sender As Object, ByVal e As AxEditLib._DfpText_ButtonHitEvent) Handles txtInventoryCode.ButtonHit
        Dim frmHelpClass As New frmHelpSearch(0, 0, "tblInventory", "L", 4, "Description", "", "InventoryCode", "InventoryCode", 80, "Description", "Description", 180, "SupplierCode", "SupplierCode", 60, "Weight", "Weight", 80)
        frmHelpClass.ShowDialog()
        txtInventoryCode.Text = frmHelpClass.Var1.Trim
    End Sub

    Private Sub txtInventoryCode_KeyDownEvent(ByVal sender As Object, ByVal e As AxEditLib._DfpText_KeyDownEvent) Handles txtInventoryCode.KeyDownEvent
        If e.keyCode = Keys.F1 Then
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblInventory", "L", 4, "Description", "", "InventoryCode", "InventoryCode", 80, "Description", "Description", 180, "SupplierCode", "SupplierCode", 60, "Weight", "Weight", 80)
            frmHelpClass.ShowDialog()
            txtInventoryCode.Text = frmHelpClass.Var1.Trim
        End If
    End Sub

    Private Sub txtInventoryCode_Advance(ByVal sender As System.Object, ByVal e As AxEditLib._DfpText_AdvanceEvent) Handles txtInventoryCode.Advance

    End Sub
End Class