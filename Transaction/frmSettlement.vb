Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmSettlement
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
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCashierName As System.Windows.Forms.TextBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents PnlSalesManPwd As System.Windows.Forms.Panel
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents grdTerminal As AxFPSpread.AxvaSpread
    Friend WithEvents txtCashierPassword As System.Windows.Forms.TextBox
    Friend WithEvents Drawer As AxMSCommLib.AxMSComm
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmSettlement))
        Me.txtCashierName = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.grdTerminal = New AxFPSpread.AxvaSpread
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.pic = New System.Windows.Forms.PictureBox
        Me.PnlSalesManPwd = New System.Windows.Forms.Panel
        Me.txtCashierPassword = New System.Windows.Forms.TextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.Drawer = New AxMSCommLib.AxMSComm
        CType(Me.grdTerminal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlSalesManPwd.SuspendLayout()
        CType(Me.Drawer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtCashierName
        '
        Me.txtCashierName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCashierName.Enabled = False
        Me.txtCashierName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCashierName.ForeColor = System.Drawing.Color.Black
        Me.txtCashierName.Location = New System.Drawing.Point(144, 80)
        Me.txtCashierName.MaxLength = 20
        Me.txtCashierName.Name = "txtCashierName"
        Me.txtCashierName.Size = New System.Drawing.Size(96, 20)
        Me.txtCashierName.TabIndex = 2
        Me.txtCashierName.Tag = ""
        Me.txtCashierName.Text = ""
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(48, 80)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 15)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "Cashier Name"
        '
        'grdTerminal
        '
        Me.grdTerminal.Location = New System.Drawing.Point(5, 107)
        Me.grdTerminal.Name = "grdTerminal"
        Me.grdTerminal.OcxState = CType(resources.GetObject("grdTerminal.OcxState"), System.Windows.Forms.AxHost.State)
        Me.grdTerminal.Size = New System.Drawing.Size(371, 445)
        Me.grdTerminal.TabIndex = 1
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.Location = New System.Drawing.Point(188, 558)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(94, 23)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save (F7)"
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(0, Byte))
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(284, 558)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(92, 23)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close(Esc)"
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(0, 0)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(210, 75)
        Me.pic.TabIndex = 112
        Me.pic.TabStop = False
        '
        'PnlSalesManPwd
        '
        Me.PnlSalesManPwd.BackColor = System.Drawing.Color.Pink
        Me.PnlSalesManPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PnlSalesManPwd.Controls.Add(Me.txtCashierPassword)
        Me.PnlSalesManPwd.Controls.Add(Me.Label21)
        Me.PnlSalesManPwd.Location = New System.Drawing.Point(92, 184)
        Me.PnlSalesManPwd.Name = "PnlSalesManPwd"
        Me.PnlSalesManPwd.Size = New System.Drawing.Size(199, 88)
        Me.PnlSalesManPwd.TabIndex = 113
        '
        'txtCashierPassword
        '
        Me.txtCashierPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCashierPassword.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCashierPassword.ForeColor = System.Drawing.Color.Black
        Me.txtCashierPassword.Location = New System.Drawing.Point(48, 40)
        Me.txtCashierPassword.MaxLength = 20
        Me.txtCashierPassword.Name = "txtCashierPassword"
        Me.txtCashierPassword.PasswordChar = Microsoft.VisualBasic.ChrW(42)
        Me.txtCashierPassword.Size = New System.Drawing.Size(136, 20)
        Me.txtCashierPassword.TabIndex = 0
        Me.txtCashierPassword.Tag = ""
        Me.txtCashierPassword.Text = ""
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Black
        Me.Label21.Location = New System.Drawing.Point(48, 14)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(128, 15)
        Me.Label21.TabIndex = 74
        Me.Label21.Text = "SalesMan Password"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Drawer
        '
        Me.Drawer.Enabled = True
        Me.Drawer.Location = New System.Drawing.Point(136, 424)
        Me.Drawer.Name = "Drawer"
        Me.Drawer.OcxState = CType(resources.GetObject("Drawer.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Drawer.Size = New System.Drawing.Size(38, 38)
        Me.Drawer.TabIndex = 114
        '
        'frmSettlement
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(382, 584)
        Me.Controls.Add(Me.Drawer)
        Me.Controls.Add(Me.PnlSalesManPwd)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.grdTerminal)
        Me.Controls.Add(Me.txtCashierName)
        Me.Controls.Add(Me.Label2)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmSettlement"
        Me.Text = "Terminal Sales Settlement"
        CType(Me.grdTerminal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlSalesManPwd.ResumeLayout(False)
        CType(Me.Drawer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region


    Dim iEndRow As Integer

    Private SBll As Object
    Dim TerminalId As String
    Dim TerminalCode As String
    Dim LocationCode As String
    Dim TotalMAmount As Double
    Dim sPassword As String
    Dim TotalCAmount As Double
    Dim TillAmount As Double
    Dim cName As String
    Dim Header As Long
    Dim GridSave As Long
    Dim ComputerName As String
    Dim sDate As String
    Dim sCashierId As String
    Dim sTerminal As String
    Dim CompName As String
    Dim Address1 As String
    Dim Address2 As String
    Dim Country As String
    Dim PhoneNo As String
    Dim FaxNo As String
    Dim ReturnAmount As Double
    Dim bExpences As Boolean
    Dim sError As String

    Dim objTS As New ClsTerminalSales
    Dim objSM As New clsSalesMan
    Dim objT As New clsTerminal
    Dim objSR As New ClsSalesReturn

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Do you want to quit?", MsgBoxStyle.Critical + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim MaxRow As Long
            Dim GridSave1 As Long
            Dim gridsave2 As Long
            Dim sSelection As String
            Dim TdATE As Date
            Dim frmrep As New frmReports
            Dim CR As New ReportDocument
            Dim strPath As String
            Dim objcompany As New clsCompany
            Dim sTelephone As String

            objTS.OpenSettlementConnection()
            objTS.BeginSettlementTransaction()

            If fncTerminalSalesSave() = False Then
                objTS.RollBackSettlementConnection()
                MessageBox.Show("Transaction not saved")
                Exit Sub
            End If
            objTS.CommitSettlementTransaction()

            DS = objcompany.GetCompanyList
            If DS.Tables(0).Rows.Count > 0 Then
                strCompanyname = Trim(DS.Tables(0).Rows(0)("Name") & "")
                strAddress = Trim(DS.Tables(0).Rows(0)("Address1") & "") & " " & _
                             Trim(DS.Tables(0).Rows(0)("Address2") & "") & " " & _
                             Trim(DS.Tables(0).Rows(0)("City") & "")
                sTelephone = Trim(DS.Tables(0).Rows(0)("Phone") & "")
            End If

            strPath = Application.StartupPath & "\Reports\TerminalUpdationSales.rpt"
            CR.Load(strPath)
            ReportLogOnToServer(CR)
            CR.Refresh()
            frmrep.ReportViewer.ReportSource = CR
            sSelection = "{tblTerminalSales.CashierId}=" & fncFormatSQLText(cName)
            sSelection = sSelection & " and " & "{tblTerminalSales.TerminalCode}=" & fncFormatSQLText(sTerminal)
            sSelection = sSelection & " and " & "{tblTerminalSales.Date}= DATE(" & Mid(sDate, 1, 4) + "," + Mid(sDate, 5, 2) + "," + Mid(sDate, 7, 2) + ")"

            CR.DataDefinition.FormulaFields("Name").Text = fncFormatSQLText(strCompanyname)
            CR.DataDefinition.FormulaFields("Address").Text = fncFormatSQLText(strAddress)
            CR.DataDefinition.FormulaFields("TelePhone").Text = fncFormatSQLText(sTelephone)
            CR.DataDefinition.FormulaFields("TillAmount").Text = Val(TillAmount)


            frmrep.ReportViewer.SelectionFormula = strselectformula
            frmrep.Show()
            frmrep.ReportViewer.Visible = True
            frmrep.ReportViewer.DisplayToolbar = True
            frmrep.ReportViewer.CloseView(False)
            frmrep.WindowState = FormWindowState.Maximized
            frmrep = Nothing

            If bExpences Then
                frmrep = New frmReports
                strPath = Application.StartupPath & "\Reports\TerminalExpences.rpt"
                CR.Load(strPath)
                ReportLogOnToServer(CR)
                CR.Refresh()
                frmrep.ReportViewer.ReportSource = CR
                sSelection = ""
                sSelection = "{tblExpences.TerminalCode}=" & fncFormatSQLText(sTerminal)
                sSelection = sSelection & " and " & "{tblExpences.ExpDate}= DATE(" & Mid(sDate, 1, 4) + "," + Mid(sDate, 5, 2) + "," + Mid(sDate, 7, 2) + ")"
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
            End If
            frmrep = New frmReports
            strPath = Application.StartupPath & "\Reports\TerminalBillCancel.rpt"
            CR.Load(strPath)
            ReportLogOnToServer(CR)
            CR.Refresh()
            frmrep.ReportViewer.ReportSource = CR
            sSelection = ""
            sSelection = "{tblSalesReturnHeader.InvoiceDate}= DATE(" & Mid(sDate, 1, 4) + "," + Mid(sDate, 5, 2) + "," + Mid(sDate, 7, 2) + ")"
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
        Catch ex As Exception
            objTS.RollBackSettlementConnection()
            MessageBox.Show(ex.Message)
        Finally
            objTS.CloseSettlementConnection()
        End Try
    End Sub

    Private Function fncTerminalSalesSave() As Boolean
        Try
            Dim i As Long
            Dim adoRS As ADODB.Recordset
            Dim MaxRow As Long
            Dim GridSave1 As Long
            Dim gridsave2 As Long
            Dim sSelection As String
            Dim TdATE As Date

            GridSave1 = GridSave - 1
            gridsave2 = GridSave + 1
            MaxRow = grdTerminal.MaxRows - 1

            If fncDeleteExisting() = False Then
                fncTerminalSalesSave = False
                Exit Function
            End If

            For i = gridsave2 To grdTerminal.MaxRows - 1
                iRet = objTS.TerminalSalesPayModeAmountSave(cName, sDate, TerminalId, GetText(grdTerminal, i, 1), Val(GetText(grdTerminal, i, 2)), Val(GetText(grdTerminal, i, 3)))
                If iRet = False Then
                    fncTerminalSalesSave = False
                    Exit Function
                End If
            Next i
            For i = 2 To GridSave1
                iRet = objTS.TerminalSalesDinaminationAmountSave(cName, TerminalId, sDate, Val(GetText(grdTerminal, i, 1)), Val(GetText(grdTerminal, i, 2)))
                If iRet = False Then
                    fncTerminalSalesSave = False
                    Exit Function
                End If
            Next i
            SBll = Nothing
            fncTerminalSalesSave = True
        Catch ex As Exception
            fncTerminalSalesSave = False
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Function fncDeleteExisting() As Boolean
        Try
            fncDeleteExisting = objTS.DeleteExsistingTerminalSales(cName, sDate, TerminalId)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Sub frmSettlement_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Return Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmSettlement_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim frmmain As New frmMain
            grdTerminal.Visible = False
            sDate = Format(ServerDate, "yyyyMMdd")
            frmmain.UGPSMenu.Visible = False
            frmmain.sbGPos.Visible = False
            txtCashierPassword.Focus()
            'subgetcompany()
            'SetPosPinters("BILL")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub txtCashierPassword_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCashierPassword.LostFocus
        Try
            If Trim(txtCashierPassword.Text) <> "" Then
                subGetPassword()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subGetPassword()
        Try
            Dim dsSM As New DataSet
            dsSM = objSM.GetSalesManwithPassword(Trim(txtCashierPassword.Text))
            If dsSM.Tables(0).Rows.Count > 0 Then
                cName = Trim(dsSM.Tables(0).Rows(0)("SalesmanCode") & "")
                txtCashierName.Text = Trim(dsSM.Tables(0).Rows(0)("SalesManName") & "")
                grdTerminal.Row = 1
                grdTerminal.Col = 3
                grdTerminal.Action = FPSpread.ActionConstants.ActionActiveCell
                SubGetTerminal()
                PnlSalesManPwd.Visible = False
            Else
                MessageBox.Show("Not Found Please Check Your Password.....")
                txtCashierPassword.Text = ""
                txtCashierPassword.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub SubGetTerminal()
        Try
            Dim j As Long
            Dim k As Long
            Dim i As Long
            Dim l As Long
            Dim Amount As Double
            Dim ExpencesAmount As Double
            Dim Qty As Object
            Dim sT As Long
            Dim flag As Long
            Dim PayExistAmount As Object
            Dim payamount As Double
            Dim bTemp As Boolean
            flag = GridSave - 1
            Dim PMaxRow As Long
            Dim sFormula As String
            Dim dsCDT As New DataSet
            Dim dsTL As New DataSet
            Dim dsPML As New DataSet
            Dim icnt As Integer
            Dim dsTS As New DataSet
            Dim dsTA As New DataSet
            Dim dsTradeIn As New DataSet
            Dim dsExpen As New DataSet
            Dim dsSRHBC As New DataSet

            dsCDT = objTS.GetCurrencyDinaminationAndTerminal()
            If dsCDT.Tables(1).Rows.Count > 0 Then
                sTerminal = Trim(dsCDT.Tables(1).Rows(0)("TerminalName") & "")
            End If

            subLoadTerminalParameter()

            dsTL = objTS.GetTerminalSalesList(cName, sDate, sTerminal)
            dsPML = objTS.GetPayModeList(cName, sDate, sTerminal)
            For icnt = 0 To dsPML.Tables(0).Rows.Count - 1
                If Trim(dsPML.Tables(0).Rows(icnt)("PayMode")) = "EXPENSES" Then
                    ExpencesAmount = Val(dsPML.Tables(0).Rows(icnt)("ManualAmount") & "")
                End If
            Next
            dsPML = Nothing

            dsPML = objTS.GetPayModeList(cName, sDate, sTerminal)
            If dsPML.Tables(0).Rows.Count > 0 Then
                'PayExistAmount = adoRS3.GetRows(adoRS3.RecordCount)
                PayExistAmount = dsPML.Tables(0).Rows.Count
            End If



            If dsTL.Tables(0).Rows.Count > 0 Then
                'Qty = adoRS2.GetRows(adoRS2.RecordCount)
                Qty = dsTL.Tables(0).Rows.Count
            End If
            j = 1

            For icnt = 0 To dsCDT.Tables(0).Rows.Count - 1
                grdTerminal.MaxRows = grdTerminal.MaxRows + 1
                j = j + 1
                grdTerminal.Row = j
                grdTerminal.Action = 7
                grdTerminal.Col = 1

                If dsCDT.Tables(0).Rows(icnt)("Currencyvalue") < 1 Then
                    grdTerminal.CellType = FPSpread.CellTypeConstants.CellTypeFloat
                    grdTerminal.TypeHAlign = FPSpread.TypeHAlignConstants.TypeHAlignCenter

                    grdTerminal.Text = Format(dsCDT.Tables(0).Rows(icnt)("Currencyvalue"))
                    If dsTL.Tables(0).Rows.Count > 0 Then
                        grdTerminal.CellType = FPSpread.CellTypeConstants.CellTypeFloat
                        grdTerminal.TypeHAlign = FPSpread.TypeHAlignConstants.TypeHAlignCenter
                        sT = Format(dsTL.Tables(0).Rows(i)(4), "0.00")
                        grdTerminal.Row = j
                        grdTerminal.Col = 2
                        grdTerminal.Text = Format(Val(sT), "0.00")
                    End If
                Else
                    grdTerminal.CellType = FPSpread.CellTypeConstants.CellTypeInteger
                    grdTerminal.TypeHAlign = FPSpread.TypeHAlignConstants.TypeHAlignCenter

                    grdTerminal.Text = dsCDT.Tables(0).Rows(icnt)("Currencyvalue") & ""
                    If dsTL.Tables(0).Rows.Count > 0 Then

                        sT = dsTL.Tables(0).Rows(i)(4)
                        grdTerminal.TypeHAlign = FPSpread.TypeHAlignConstants.TypeHAlignCenter
                        grdTerminal.CellType = FPSpread.CellTypeConstants.CellTypeInteger
                        grdTerminal.Row = j
                        grdTerminal.Col = 2
                        grdTerminal.Text = Val(sT)
                    End If
                End If
                i = i + 1
                sFormula = "A" & j & "*" & "B" & j
                grdTerminal.Row = j
                grdTerminal.Col = 3
                grdTerminal.CellType = FPSpread.CellTypeConstants.CellTypeFloat
                grdTerminal.Formula = sFormula
                grdTerminal.Lock = True

               
            Next icnt

            iEndRow = j
            sFormula = "sum(c2:c" & j & ")"
            k = j + 1
            Header = k
            GridSave = k
            dsCDT = Nothing
            dsCDT = objTS.GetTerminal()
            If dsCDT.Tables(0).Rows.Count > 0 Then
                TerminalId = dsCDT.Tables(0).Rows(0)("TerminalName")
                TerminalCode = dsCDT.Tables(0).Rows(0)("TerminalCode")
                LocationCode = dsCDT.Tables(0).Rows(0)("LocationCode")
            End If

            dsTS = objTS.GetTerminalSales(cName, TerminalId)
            If dsTS.Tables(0).Rows.Count = 0 Then
                MessageBox.Show("No Sales Detail For Today.")
                btnClose.Focus()
                btnSave.Enabled = False
                Exit Sub
            End If

            dsTA = objTS.GetTillAmount(TerminalId)
            TillAmount = dsTA.Tables(0).Rows(0)("TillAmount")

            For icnt = 0 To dsTS.Tables(0).Rows.Count - 1
                k = k + 1
                grdTerminal.MaxRows = grdTerminal.MaxRows + 1
                grdTerminal.Row = k
                grdTerminal.Action = 7
                grdTerminal.Col = 1
                grdTerminal.Text = Trim(dsTS.Tables(0).Rows(icnt)("PayMode") & "")
                grdTerminal.Lock = True
                grdTerminal.Col = 2
                grdTerminal.Text = Val(dsTS.Tables(0).Rows(icnt)("Amount") & "")

                If k = j + 2 Then
                    grdTerminal.Col = 3
                    grdTerminal.Formula = sFormula
                    grdTerminal.Lock = True
                End If
                l = l + 1
            Next icnt

            dsTradeIn = objTS.GetTerminalTradeIn(cName, TerminalCode)
            If dsTradeIn.Tables(0).Rows.Count > 0 Then
                grdTerminal.Row = grdTerminal.MaxRows
                grdTerminal.MaxRows = grdTerminal.MaxRows + 1
                grdTerminal.Action = 7
                grdTerminal.Col = 1
                grdTerminal.Text = "TRADEIN"
                grdTerminal.Col = 2
                grdTerminal.Text = Val(dsTradeIn.Tables(0).Rows(0)(0) & "")
                grdTerminal.Col = 3
                grdTerminal.Text = Val(dsTradeIn.Tables(0).Rows(0)(0) & "")
            End If


            dsExpen = objTS.GetExpencesAmount(cName, sDate, dsTA.Tables(0).Rows(0)("TerminalName"))
            Amount = 0
            If dsExpen.Tables(0).Rows.Count > 0 Then
                For icnt = 0 To dsExpen.Tables(0).Rows.Count
                    Amount = Amount + Val(dsExpen.Tables(0).Rows(icnt)("Amount") & "")
                Next icnt
            End If
            If Amount <> 0 Then
                bExpences = True
            Else
                bExpences = False
            End If
            Amount = -Amount

            grdTerminal.Row = grdTerminal.MaxRows


            grdTerminal.MaxRows = grdTerminal.MaxRows + 1
            grdTerminal.Action = 7
            grdTerminal.Col = 1

            grdTerminal.Text = "EXPENSES"
            grdTerminal.Col = 2
            grdTerminal.Text = Amount

            grdTerminal.Row = grdTerminal.MaxRows


            dsSRHBC = objSR.GetSalesReturnHeaderByCashier(sDate, cName)
            If dsSRHBC.Tables(0).Rows.Count > 0 Then
                ReturnAmount = Val(dsSRHBC.Tables(0).Rows(0)("ReturnAmount") & "")
                ReturnAmount = -ReturnAmount

                grdTerminal.MaxRows = grdTerminal.MaxRows + 1
                grdTerminal.Action = 7
                grdTerminal.Col = 1
                grdTerminal.Text = "RETURN"
                grdTerminal.Col = 2
                grdTerminal.Text = ReturnAmount
                grdTerminal.Col = 3
                grdTerminal.Text = ReturnAmount

            End If
            dsSRHBC = Nothing

            grdTerminal.Row = grdTerminal.MaxRows
            grdTerminal.Col = 2
            sFormula = "sum(B" & j + 2 & ":B" & grdTerminal.MaxRows - 1 & ")"
            grdTerminal.Formula = sFormula
            grdTerminal.Col = 3
            sFormula = "sum(C" & j + 2 & ":C" & grdTerminal.MaxRows - 1 & ")"
            grdTerminal.Formula = sFormula

            grdTerminal.Row = 1
            grdTerminal.Col = 1
            grdTerminal.Row2 = grdTerminal.MaxRows
            grdTerminal.Col2 = 3
            grdTerminal.BlockMode = True
            grdTerminal.TypeVAlign = FPSpread.TypeVAlignConstants.TypeVAlignCenter
            grdTerminal.BlockMode = False
            '22.7
            'Me.Height = grdTerminal.MaxRows * 14 * 20 + 1750
            ''FramePayMode.Height = grdTerminal.MaxRows * 14 * 20 + 850
            'grdTerminal.Height = grdTerminal.MaxRows * 14 * 21
            'btnSave.Top = grdTerminal.Height - 400
            'btnClose.Top = grdTerminal.Height - 400

            grdTerminal.Visible = True
            grdTerminal.Row = 2
            grdTerminal.Col = 2
            grdTerminal.Action = FPSpread.ActionConstants.ActionActiveCell
            grdTerminal.Focus()
            For icnt = iEndRow + 2 To grdTerminal.MaxRows - 1
                grdTerminal.Row = icnt
                grdTerminal.Col = 3
                grdTerminal.CellType = FPSpread.CellTypeConstants.CellTypeFloat
                grdTerminal.TypeVAlign = FPSpread.TypeVAlignConstants.TypeVAlignCenter
                grdTerminal.TypeHAlign = FPSpread.TypeHAlignConstants.TypeHAlignRight
                If icnt > iEndRow + 2 Then
                    grdTerminal.Lock = False
                Else
                    grdTerminal.Lock = True
                End If
            Next
            For icnt = 2 To iEndRow
                CalcTotal(icnt)
            Next
            SubTotal()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subLoadTerminalParameter()
        Try
            Dim dsT As New DataSet
            dsT = objT.GetTerminal(Trim(sTerminal))
            If dsT.Tables(0).Rows.Count > 0 Then
                If Val(dsT.Tables(0).Rows(0)("PrinterRequired") & "") = 1 Then
                    If Trim(dsT.Tables(0).Rows(0)("CashDrawerInterface") & "") = "S" Then
                        Drawer.PortOpen = True
                        Drawer.Output = "A"
                        Drawer.PortOpen = False
                    Else
                        'Call SetPosPinters("BILL")
                        'Printer.Font.Name = "Control"
                        'Printer.Print("A")
                        'Printer.EndDoc()
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        
    End Sub

    Private Sub grdTerminal_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdTerminal.GotFocus
        Try
            Me.KeyPreview = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub grdTerminal_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdTerminal.LostFocus
        Try
            Me.KeyPreview = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub grdTerminal_LeaveCell(ByVal sender As Object, ByVal e As AxFPSpread._DSpreadEvents_LeaveCellEvent) Handles grdTerminal.LeaveCell
        Try
            If e.newRow = 1 Then
                grdTerminal.Row = 2
                grdTerminal.Col = 2
                grdTerminal.Action = 0
            ElseIf e.newRow > 1 And e.newRow < Header Then
                If e.newCol <> 2 Then
                    grdTerminal.Row = e.newRow
                    grdTerminal.Col = 2
                    grdTerminal.Action = 0
                End If
            ElseIf e.newRow = Header Then
                If e.row <= Header Then
                    grdTerminal.Row = Header + 1
                    grdTerminal.Col = 3
                    grdTerminal.Action = 0
                Else
                    grdTerminal.Row = Header - 1
                    grdTerminal.Col = 2
                    grdTerminal.Action = 0
                End If
            ElseIf e.newRow > Header And e.newRow < grdTerminal.MaxRows Then
                If e.newCol <> 3 Then
                    grdTerminal.Row = e.newRow
                    grdTerminal.Col = 3
                    grdTerminal.Action = 0
                End If
            ElseIf e.newRow = grdTerminal.MaxRows Then
                grdTerminal.Row = grdTerminal.MaxRows - 1
                grdTerminal.Col = 3
                grdTerminal.Action = 0
            End If
            If e.newCol = 2 And e.col = 2 Then
                CalcTotal(e.row)
            End If
            If e.col = 3 Or e.newCol = 3 Or e.col = 2 Or e.newCol = 2 Then
                SubTotal()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub CalcTotal(ByVal Row As Integer)
        Dim iCnt As Integer
        Dim dMannualTotal As Double
        Try
            If Val(GetText(grdTerminal, Row, 2)) = 0 Then SetText(grdTerminal, Row, 2, "0.00")
            grdTerminal.Row = Row
            grdTerminal.Col = 3
            grdTerminal.TypeVAlign = FPSpread.TypeVAlignConstants.TypeVAlignCenter
            grdTerminal.TypeHAlign = FPSpread.TypeHAlignConstants.TypeHAlignRight
            grdTerminal.CellType = FPSpread.CellTypeConstants.CellTypeFloat
            grdTerminal.Col = 3
            SetText(grdTerminal, Row, 3, Format(Val(GetText(grdTerminal, Row, 1)) * Val(GetText(grdTerminal, Row, 2)), "0.00"))
            dMannualTotal = 0.0
            For iCnt = 2 To iEndRow
                dMannualTotal += Val(GetText(grdTerminal, iCnt, 3))
            Next
            grdTerminal.Row = iCnt + 1
            grdTerminal.Col = 3
            grdTerminal.TypeVAlign = FPSpread.TypeVAlignConstants.TypeVAlignCenter
            grdTerminal.TypeHAlign = FPSpread.TypeHAlignConstants.TypeHAlignRight
            grdTerminal.CellType = FPSpread.CellTypeConstants.CellTypeFloat
            SetText(grdTerminal, iCnt + 1, 3, Format(dMannualTotal, "0.00"))
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub SubTotal()
        Dim dMTotal As Double
        Dim dCTotal As Double
        Dim iCnt As Integer
        Try
            dMTotal = 0.0
            dCTotal = 0.0
            For iCnt = iEndRow + 2 To grdTerminal.MaxRows - 1
                dCTotal += Val(GetText(grdTerminal, iCnt, 2))
                dMTotal += Val(GetText(grdTerminal, iCnt, 3))
            Next
            SetText(grdTerminal, grdTerminal.MaxRows, 2, Format(dCTotal, "0.00"))
            SetText(grdTerminal, grdTerminal.MaxRows, 3, Format(dMTotal, "0.00"))
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
