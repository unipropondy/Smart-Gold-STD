Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmSchemeTransactionDetail
    Dim objCust As New clsCustomer
    Dim sCustomerCode As String
    Dim objTer As New clsTerminal
    Dim objComp As New clsCompany
    Dim objIns As New ClsInstallment
    Dim objSales As New ClsSales
    Dim msSchemeNo As String
    Dim CompName$, Address1$, Address2$, Country$, PhoneNo$, FaxNo$
    Dim bReprint As Boolean
    Dim dTradeInGST As Double
    Dim s24K As String
    Dim s22K As String
    Dim s20K As String
    Dim s22S As String
    Dim s999 As String
    Dim s916 As String
    Dim s835 As String
    Dim s24K999 As String
    Dim s20K850 As String
    Dim s22KSOV As String
    Dim sInvGstType As String
    Dim dGstPerc As Double
    Private Sub btnCustomerCodeSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomerCodeSearch.Click
        Try
            Dim frmHelpCustomer As New frmHelpSearch(0, 0, "tblCustomer", "R", 4, "CustomerName", "", "CustomerCode", "CustomerCode", 80, "CustomerName", "CustomerName", 150, "NricNo", "NricNo", 90, "Address1", "Address1", 80)
            frmHelpCustomer.ShowDialog()
            txtCustomerCode.Text = frmHelpCustomer.Var1.Trim
            txtCustomerName.Text = frmHelpCustomer.Var2.Trim
            subGetCustomerDetail()
            sCustomerCode = Trim(txtCustomerCode.Text)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subGetCustomerDetail()
        Dim dsCust As New DataSet
        Dim dsCustPoints As New DataSet
        Dim sEmail As String

        Try
            If txtCustomerCode.Text.Trim <> "" Then
                sCustomerCode = Trim(txtCustomerCode.Text.Trim)
                dsCust = objCust.GetCustomerwithCodeHQ(txtCustomerCode.Text.Trim)
                If dsCust.Tables(0).Rows.Count > 0 Then
                    txtCustomerName.Text = Trim(dsCust.Tables(0).Rows(0)("CustomerName") & "")
                    lblCustomerType.Text = Trim(dsCust.Tables(0).Rows(0)("CustomerType") & "")
                    txtAddress1.Text = Trim(dsCust.Tables(0).Rows(0)("Address1") & "")
                    txtAddress2.Text = Trim(dsCust.Tables(0).Rows(0)("Address2") & "")
                    txtAddress3.Text = Trim(dsCust.Tables(0).Rows(0)("Address3") & "")
                    ' sPassportNo = Trim(dsCust.Tables(0).Rows(0)("NricNo") & "")
                    'sEmail = Trim(dsCust.Tables(0).Rows(0)("eMail") & "")
                    'lblMemberPoints.Text = Val(dsCust.Tables(0).Rows(0)("TotalRewardPoints") & "") - Val(dsCust.Tables(0).Rows(0)("TotalRedeemedPoints") & "")

                    'If Trim(sEmail) = "" Then

                    'End If

                    'dsCustPoints = objCust.GetCustomerPointswithCodeHQ(txtCustomerCode.Text.Trim)
                    'If dsCustPoints.Tables(0).Rows.Count > 0 Then
                    '    lblMemberPoints.Text = Val(dsCustPoints.Tables(0).Rows(0)("RewardPoints") & "") - Val(dsCustPoints.Tables(0).Rows(0)("Redeemedpoints") & "")
                    'End If
                    ''txtInventoryCode.Focus()
                    Exit Sub
                Else
                    txtCustomerCode.Text = "CASH"
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsCust = Nothing
        End Try
    End Sub

    Private Sub btnNewCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewCustomer.Click
        IsCalledFrom = "POS"
        If Trim(sCompanyShortCode) = "AJ" Then
            Dim frmCust As New frmOutletCustomers
            frmCust.ShowDialog()
            frmCust = Nothing
        ElseIf Trim(sCompanyShortCode) = "MJ" Then
            Dim frmCust As New frmCustomer
            frmCust.ShowDialog()
            frmCust = Nothing
        Else
            Dim frmCust As New frmOutletCustomersMJ
            frmCust.ShowDialog()
            frmCust = Nothing
        End If

        txtCustomerCode.Text = Trim(sOutCustomerCode)
        txtCustomerName.Text = Trim(sOutCustomerName)
    End Sub

    Private Sub btnSchemeCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSchemeCode.Click
        Try
            Dim frmHelpScheme As New frmHelpSearch(0, 0, "tblSchemeMaster", "R", 4, "Code", "", "Code", "Code", 80, "Description", "Description", 150, "Remarks", "Value", 90, "CreateUser", "CreateUser", 80)
            frmHelpScheme.ShowDialog()
            txtSchemeCode.Text = frmHelpScheme.Var1.Trim
            txtSchemeValue.Text = frmHelpScheme.Var4.Trim

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtSalesManPassword_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSalesManPassword.Leave
        Try
            If txtSalesManPassword.Text.Trim <> "" Then
                subGetSalesManPassword()
            Else
                MessageBox.Show("Please enter Sales Man password.")
                txtSalesManPassword.Clear()
                txtSalesManPassword.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subGetSalesManPassword()
        Dim dsSMPWD As New DataSet
        Dim dsInvoiceNo As New DataSet
        Dim dsTotalSalesAmt As New DataSet
        Dim sDate As String
        Dim sSalesManGroup As String
        Dim objSM As New clsSalesMan
        sDate = CStr(Format(CDate(ServerDate), "yyyy") + Format(CDate(ServerDate), "MM") + Format(CDate(ServerDate), "dd"))
        Try
            dsSMPWD = objSM.GetSalesManwithPassword(txtSalesManPassword.Text.Trim)
            If dsSMPWD.Tables(0).Rows.Count > 0 Then
                txtSalesManName.Text = Trim(dsSMPWD.Tables(0).Rows(0)("SalesManName") & "")
                sManCode = Trim(dsSMPWD.Tables(0).Rows(0)("SalesmanCode") & "")

            Else
                MessageBox.Show("Invalid Password.")
                txtSalesManPassword.Clear()
                txtSalesManPassword.Focus()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsSMPWD = Nothing
            dsInvoiceNo = Nothing
        End Try
    End Sub

    Private Sub frmSchemeTransactionDetail_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Escape
                    Me.Close()
                Case Keys.Enter
                    SendKeys.Send("{TAB}")
                Case Keys.F6
                    ' btnBillClear_Click(sender, e)
                Case Keys.F7
                    ' btnSAve_Click(sender, e)
                Case Keys.F10
                    '  btnCancel_Click(sender, e)
                Case Keys.F9
                    '  btnRePrint_Click(sender, e)
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmSchemeTransactionDetail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        txtIDD.Text = Format(CDate(ServerDate), "dd")
        txtIMM.Text = Format(CDate(ServerDate), "MM")
        txtIYYYY.Text = Format(CDate(ServerDate), "yyyy")
    End Sub

    Private Sub optGram_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optGram.CheckedChanged

    End Sub

    Private Sub optValue_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optValue.CheckedChanged

    End Sub

    Private Sub Panel8_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel8.Paint

    End Sub

    Private Sub txtSchemeValue_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSchemeValue.TextChanged

    End Sub

    Private Sub btnSAve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSAve.Click
        Dim frmPM As New frmPayingmode
        Try
            'If bInsMsg = True Then
            '    If Val(txtInstallmentAmount.Text) < Val(dbalAmt) Then
            '        MsgBox("Please give full amount..!")
            '        lblBalanceAmount.Text = Format(dbalAmt, "0.00")
            '        txtInstallmentAmount.Text = Format(dbalAmt, "0.00")
            '        insFocus()
            '        Exit Sub
            '    End If
            'End If

            'If Trim$(txtAgreementNo.Text) = "" Then
            '    MsgBox("Please enter the Installment Agreement No!")
            '    Exit Sub
            'End If

            If fncCheckValidity() Then
                objIns.OpenConnection()
                objIns.BeginTransaction()
                objSales.subOpenSalesConnection()
                objSales.subBeginSalesTransaction()
                dPaidAmount = 0
                ReturnAmount = 0

                nTotalNet = Val(txtSchemeValue.Text)
                'get the next Installment Number.
                If Not fncGetSchemeNo() Then Exit Sub
                frmPM.SalesType = ""
                frmPM.sCustomerType = Trim(lblCustomerType.Text)
                frmPM.sCustomerCode = Trim(txtCustomerCode.Text)
                frmPM.ShowDialog()
                If dPaidAmount > 0 Then
                    If Not fncPaymodeDetailSave(msSchemeNo) Then
                        MessageBox.Show("Transaction not saved")
                        objIns.RollBackTransaction()
                        'save the Installment Details.
                    ElseIf Not fncSaveSchemeDet() Then
                        MessageBox.Show("Transaction not saved")
                        objIns.RollBackTransaction()
                    Else

                        objIns.CommitTransaction()
                        objSales.subCommitSalesTransaction()
                        ' Call subPrintBill(Trim(msInstallmentNo))
                        If Trim(msSchemeNo) <> "" Then
                            '   Call subPrintBill(Trim(msSchemeNo))
                        End If

                    End If

                End If

                'btnBillClear_Click(sender, e)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            objIns.CloseConnection()
            objSales.subCloseSalesConnection()
            frmPM = Nothing
        End Try
    End Sub

    Private Sub subPrintBill(ByVal sInvoiceNo As String)
        Dim ReportTitle As String
        Dim sSelection As String
        Dim sSalesType$
        Dim sReply As String
        Dim dsSales As New DataSet
        Dim frmrep As New frmReports
        Dim CR As New ReportDocument
        Dim j As Integer
        Dim sCmd As String
        Dim sExec As String
        Dim sInsAgreementNo As String
        Dim Dm As New DataManager
        Dim sPrintCustomer As String
        Dim sSql As String
        Try
            Cursor = Cursors.WaitCursor
            dsSales = objSales.GetSalesHeaderByInvoiceNo(Trim(sInvoiceNo))

            If dsSales.Tables(0).Rows.Count > 0 Then
                sSalesType = Trim$(dsSales.Tables(0).Rows(0)("SalesType") & "")
                sInsAgreementNo = Trim$(dsSales.Tables(0).Rows(0)("InsAgreementNo") & "")
                sPrintCustomer = Trim$(dsSales.Tables(0).Rows(0)("customerCode") & "")
            End If

            sSql = "If exists (Select * from dbo.sysobjects where id = object_id(N'[dbo].[vwMemberPrintAJ]') and OBJECTPROPERTY(id, N'IsView') = 1)"
            sSql = sSql & " Drop View vwMemberPrintAJ" & vbCrLf
            Dm.ProjExecNonQryAcc(sSql)


            sSql = " Create View vwMemberPrintAJ" & vbCrLf
            sSql = sSql & " AS" & vbCrLf
            sSql = sSql & " select top 1 * from vwMemberTransationaj "
            sSql = sSql & " Where CustomerCode = " & fncFormatSQLText(sPrintCustomer)
            sSql = sSql & " order by ExpiryDate"

            Dm.ProjExecNonQryAcc(sSql)

            sSql = "If exists (Select * from dbo.sysobjects where id = object_id(N'[dbo].[vwMemberTranAJ]') and OBJECTPROPERTY(id, N'IsView') = 1)"
            sSql = sSql & " Drop View vwMemberTranAJ" & vbCrLf
            Dm.ProjExecNonQryAcc(sSql)


            sSql = " Create View vwMemberTranAJ" & vbCrLf
            sSql = sSql & " AS" & vbCrLf
            sSql = sSql & " select  * from vwMemberTransationaj "
            sSql = sSql & " Where CustomerCode = " & fncFormatSQLText(sPrintCustomer)
            sSql = sSql & " and InvoiceNo = " & fncFormatSQLText(sInvoiceNo)


            Dm.ProjExecNonQryAcc(sSql)

            If UCase(sCompanyShortCode) = "HN" Then
                Select Case sSalesType
                    Case "TRADEIN"
                        strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_HN.rpt"
                        CR.Load(strpath)
                        ReportLogOnToServer(CR)
                        CR.Refresh()

                        frmrep.ReportViewer.ReportSource = CR
                        sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                        frmrep.ReportViewer.SelectionFormula = sSelection
                        CR.DataDefinition.RecordSelectionFormula = sSelection
                        If UCase(sCompanyShortCode) = "HN" And bReprint Then
                            CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                        End If
                        CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                        CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                        CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                        CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                        CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                        CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                        CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                        CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                        CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                        CR.PrintToPrinter(1, True, 0, 0)
                        frmrep = Nothing
                        frmrep = New frmReports


                        ''''' Durai Changes
                        '''If (UCase(Objuser.CompanyName)) = "HANIFFA PTE LTD" Then
                        strpath = Application.StartupPath & "\Reports\rptTradeInDetailInvoice_HN.rpt"
                        CR.Load(strpath)
                        ReportLogOnToServer(CR)
                        CR.Refresh()

                        frmrep.ReportViewer.ReportSource = CR
                        sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                        frmrep.ReportViewer.SelectionFormula = sSelection
                        CR.DataDefinition.RecordSelectionFormula = sSelection
                        CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                        CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                        CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                        CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                        CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                        CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                        CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                        CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                        CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                        CR.PrintToPrinter(1, True, 0, 0)
                        frmrep = Nothing
                        frmrep = New frmReports
                        '''End If

                    Case "CASH"
                        strpath = Application.StartupPath & "\Reports\rptSalesInvoice_HN.rpt"

                        CR.Load(strpath)
                        ReportLogOnToServer(CR)
                        CR.Refresh()

                        frmrep.ReportViewer.ReportSource = CR
                        sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                        frmrep.ReportViewer.SelectionFormula = sSelection
                        CR.DataDefinition.RecordSelectionFormula = sSelection
                        If UCase(sCompanyShortCode) = "HN" And bReprint Then
                            CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                        End If
                        CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                        CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                        CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                        CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                        CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                        CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                        CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                        CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                        CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                        CR.PrintToPrinter(1, True, 0, 0)
                        frmrep = Nothing
                        frmrep = New frmReports
                End Select
            ElseIf UCase(sCompanyShortCode) = "MJ" Then


                sCmd = " Exec sp_InstallmentPayMode " & fncFormatSQLText(sInsAgreementNo)
                Dm.ProjExecNonQry(sCmd)

                ''strpath = Application.StartupPath & "\Reports\rptSalesInvoice_MJ.rpt"
                If sInvGstType = "" Then
                    strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_MJ.rpt"
                ElseIf sInvGstType = "Standard Rated Supply" Then
                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceStandard_MJ.rpt"
                ElseIf sInvGstType = "Exempt Supply" Or sInvGstType = "No GST" Then
                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceNoGST_MJ.rpt"
                End If
                CR.Load(strpath)
                ReportLogOnToServer(CR)
                CR.Refresh()

                frmrep.ReportViewer.ReportSource = CR
                sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                frmrep.ReportViewer.SelectionFormula = sSelection
                CR.DataDefinition.RecordSelectionFormula = sSelection
                If UCase(sCompanyShortCode) = "MJ" And bReprint Then
                    CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                End If
                CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s24K999)
                CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s20K850)
                CR.DataDefinition.FormulaFields("Rate4").Text = fncFormatSQLText(s22KSOV)
                ''CR.PrintOptions.PaperSize = PaperSize.DefaultPaperSize
                CR.PrintToPrinter(2, True, 0, 0)

                ''frmrep.ReportViewer.ReportSource = CR
                ''frmrep.ShowDialog()
                ''frmrep.ReportViewer.Visible = True
                ''frmrep.ReportViewer.CloseView(False)
                ''frmrep.WindowState = FormWindowState.Maximized
                frmrep = Nothing
                frmrep = New frmReports
                ''' End Select
            ElseIf UCase(sCompanyShortCode) = "SSJ" Then
                Select Case sSalesType
                    Case "INSTALLMENT"
                        strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_SSJ.rpt"

                        CR.Load(strpath)
                        ReportLogOnToServer(CR)
                        CR.Refresh()

                        frmrep.ReportViewer.ReportSource = CR
                        sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                        frmrep.ReportViewer.SelectionFormula = sSelection
                        CR.DataDefinition.RecordSelectionFormula = sSelection
                        If UCase(sCompanyShortCode) = "SSJ" And bReprint Then
                            CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                        End If
                        CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                        CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                        CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                        CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                        CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                        CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                        CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                        CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                        CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                        CR.PrintToPrinter(1, True, 0, 0)
                        frmrep = Nothing
                        frmrep = New frmReports
                    Case "TRADEIN"

                        strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_SSJ.rpt"
                        CR.Load(strpath)
                        ReportLogOnToServer(CR)
                        CR.Refresh()

                        frmrep.ReportViewer.ReportSource = CR
                        sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                        frmrep.ReportViewer.SelectionFormula = sSelection
                        CR.DataDefinition.RecordSelectionFormula = sSelection
                        If UCase(sCompanyShortCode) = "SSJ" And bReprint Then
                            CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                        End If
                        CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                        CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                        CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                        CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                        CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                        CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                        CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                        CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                        CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                        CR.PrintToPrinter(1, True, 0, 0)
                        frmrep = Nothing
                        frmrep = New frmReports
                    Case "CASH"
                        strpath = Application.StartupPath & "\Reports\rptSalesInvoice_SSJ.rpt"

                        CR.Load(strpath)
                        ReportLogOnToServer(CR)
                        CR.Refresh()

                        frmrep.ReportViewer.ReportSource = CR
                        sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                        frmrep.ReportViewer.SelectionFormula = sSelection
                        CR.DataDefinition.RecordSelectionFormula = sSelection
                        If UCase(sCompanyShortCode) = "SSJ" And bReprint Then
                            CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                        End If
                        CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                        CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                        CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                        CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                        CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                        CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                        CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                        CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                        CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                        CR.PrintToPrinter(1, True, 0, 0)
                        frmrep = Nothing
                        frmrep = New frmReports
                End Select
            ElseIf UCase(sCompanyShortCode) = "RJ" Then
                Select Case sSalesType
                    Case "INSTALLMENT"
                        j = 1
                        For j = 1 To 2
                            strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_RJ.rpt"

                            CR.Load(strpath)
                            ReportLogOnToServer(CR)
                            CR.Refresh()

                            frmrep.ReportViewer.ReportSource = CR
                            sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                            frmrep.ReportViewer.SelectionFormula = sSelection
                            CR.DataDefinition.RecordSelectionFormula = sSelection
                            If UCase(sCompanyShortCode) = "SSJ" And bReprint Then
                                CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                            End If
                            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                            CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                            CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                            CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                            CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                            CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                            CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                            CR.PrintToPrinter(1, True, 0, 0)
                            frmrep = Nothing
                            frmrep = New frmReports
                        Next
                        'frmrep.Show()
                        'frmrep.ReportViewer.Visible = True
                        'frmrep.ReportViewer.DisplayToolbar = True
                        'frmrep.ReportViewer.CloseView(False)
                        'frmrep.WindowState = FormWindowState.Maximized

                    Case "TRADEIN"
                        j = 1
                        For j = 1 To 2
                            strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_RJ.rpt"
                            CR.Load(strpath)
                            ReportLogOnToServer(CR)
                            CR.Refresh()

                            frmrep.ReportViewer.ReportSource = CR
                            sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                            frmrep.ReportViewer.SelectionFormula = sSelection
                            CR.DataDefinition.RecordSelectionFormula = sSelection
                            If UCase(sCompanyShortCode) = "SSJ" And bReprint Then
                                CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                            End If
                            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                            CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                            CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                            CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                            CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                            CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                            CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                            'frmrep.Show()
                            'frmrep.ReportViewer.Visible = True
                            'frmrep.ReportViewer.DisplayToolbar = True
                            'frmrep.ReportViewer.CloseView(False)
                            'frmrep.WindowState = FormWindowState.Maximized
                            CR.PrintToPrinter(1, True, 0, 0)
                            frmrep = Nothing
                            frmrep = New frmReports
                        Next
                    Case "CASH"
                        j = 1
                        For j = 1 To 2
                            strpath = Application.StartupPath & "\Reports\rptSalesInvoice_RJ.rpt"

                            CR.Load(strpath)
                            ReportLogOnToServer(CR)
                            CR.Refresh()

                            frmrep.ReportViewer.ReportSource = CR
                            sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                            frmrep.ReportViewer.SelectionFormula = sSelection
                            CR.DataDefinition.RecordSelectionFormula = sSelection
                            If UCase(sCompanyShortCode) = "SSJ" And bReprint Then
                                CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                            End If
                            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                            CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                            CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                            CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                            CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                            CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                            CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)


                            '  frmrep.ReportViewer.SelectionFormula = strselectformula
                            'frmrep.Show()
                            'frmrep.ReportViewer.Visible = True
                            'frmrep.ReportViewer.DisplayToolbar = True
                            'frmrep.ReportViewer.CloseView(False)
                            'frmrep.WindowState = FormWindowState.Maximized
                            ' frmrep = Nothing
                            '  CR.PrintOptions.PaperSize = PaperSize.DefaultPaperSize
                            CR.PrintToPrinter(1, True, 0, 0)
                            frmrep = Nothing
                            frmrep = New frmReports
                        Next
                End Select

            Else
                sReply = MsgBox("Do you want to print Net Bill?", vbYesNoCancel + vbQuestion, "Print Option")
                If sReply = vbYes Then
                    Select Case sSalesType
                        Case "INSTALLMENT"

                            sCmd = " Exec sp_InstallmentPayMode " & fncFormatSQLText(sInsAgreementNo)
                            Dm.ProjExecNonQry(sCmd)

                            sExec = " Exec sp_InstallmentTradeIn " & fncFormatSQLText(sInsAgreementNo)
                            Dm.ProjExecNonQry(sExec)

                            If sCompanyShortCode = "ES" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_ESP.rpt"
                            ElseIf sCompanyShortCode = "AS" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_AS.rpt"
                            ElseIf sCompanyShortCode = "ML" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_ML.rpt"
                            ElseIf UCase(sCompanyShortCode) = "GMT" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_GMT.rpt"
                            ElseIf UCase(sCompanyShortCode) = "AJ" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_AJ.rpt"
                            ElseIf UCase(sCompanyShortCode) = "MD" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_MD.rpt"
                            ElseIf UCase(sCompanyShortCode) = "MJ" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_MJ.rpt"
                            End If

                            CR.Load(strpath)
                            ReportLogOnToServer(CR)
                            CR.Refresh()
                            '  subGetClassRate()
                            frmrep.ReportViewer.ReportSource = CR
                            sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                            frmrep.ReportViewer.SelectionFormula = sSelection
                            CR.DataDefinition.RecordSelectionFormula = sSelection
                            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                            CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                            CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                            CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                            CR.PrintToPrinter(2, True, 0, 0)
                            frmrep = Nothing
                            frmrep = New frmReports
                        Case "TRADEIN"
                            If sCompanyShortCode = "ES" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_ESP.rpt"
                            ElseIf sCompanyShortCode = "AS" Then
                                '''strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_AS.rpt"
                                If sInvGstType = "" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_AS.rpt"
                                ElseIf sInvGstType = "Standard Rated Supply" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoiceStandard_AS.rpt"
                                ElseIf sInvGstType = "Exempt Supply" Or sInvGstType = "No GST" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoiceNoGST_AS.rpt"
                                End If
                            ElseIf sCompanyShortCode = "ML" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_ML.rpt"
                            ElseIf UCase(sCompanyShortCode) = "GMT" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_GMT.rpt"
                            ElseIf UCase(sCompanyShortCode) = "HN" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_HN.rpt"
                            End If
                            CR.Load(strpath)
                            ReportLogOnToServer(CR)
                            CR.Refresh()

                            frmrep.ReportViewer.ReportSource = CR
                            sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                            frmrep.ReportViewer.SelectionFormula = sSelection
                            CR.DataDefinition.RecordSelectionFormula = sSelection
                            If UCase(sCompanyShortCode) = "HN" And bReprint Then
                                CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                            End If
                            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                            CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                            CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                            CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                            CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                            CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                            CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                            CR.PrintToPrinter(1, True, 0, 0)
                            frmrep = Nothing
                            frmrep = New frmReports
                            If sCompanyShortCode = "ES" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInDetailInvoice_ESP.rpt"
                            End If

                            If ((UCase(sCompanyShortCode) <> "HN") And (UCase(sCompanyShortCode) <> "AS") And (UCase(sCompanyShortCode) <> "ML")) Then
                                CR.Load(strpath)
                                ReportLogOnToServer(CR)
                                CR.Refresh()

                                frmrep.ReportViewer.ReportSource = CR
                                sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                                frmrep.ReportViewer.SelectionFormula = sSelection
                                CR.DataDefinition.RecordSelectionFormula = sSelection
                                CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                                CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                                CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                                CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                                CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                                CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                                CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                                CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                                CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                                CR.PrintToPrinter(1, True, 0, 0)
                                frmrep = Nothing
                                frmrep = New frmReports
                            End If
                        Case Else
                            If UCase(sCompanyShortCode) = "ES" Then
                                strpath = Application.StartupPath & "\Reports\rptSalesInvoice_ESP.rpt"
                            ElseIf sCompanyShortCode = "AS" Then
                                '''strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AS.rpt"
                                If sInvGstType = "" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AS.rpt"
                                ElseIf sInvGstType = "Standard Rated Supply" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceStandard_AS.rpt"
                                ElseIf sInvGstType = "Exempt Supply" Or sInvGstType = "No GST" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceNoGST_AS.rpt"
                                End If
                            ElseIf sCompanyShortCode = "AJ" Then
                                '''strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AS.rpt"
                                If sInvGstType = "" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AJ.rpt"
                                ElseIf sInvGstType = "Standard Rated Supply" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceStandard_AJ.rpt"
                                ElseIf sInvGstType = "Exempt Supply" Or sInvGstType = "No GST" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceNoGST_AJ.rpt"
                                End If
                            ElseIf sCompanyShortCode = "ML" Then
                                strpath = Application.StartupPath & "\Reports\rptSalesInvoice_ML.rpt"
                            ElseIf UCase(sCompanyShortCode) = "GMT" Then
                                strpath = Application.StartupPath & "\Reports\rptSalesInvoice_GMT.rpt"
                            ElseIf UCase(sCompanyShortCode) = "HN" Then
                                strpath = Application.StartupPath & "\Reports\rptSalesInvoice_HN.rpt"
                            End If
                            CR.Load(strpath)
                            ReportLogOnToServer(CR)
                            CR.Refresh()

                            frmrep.ReportViewer.ReportSource = CR
                            sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                            frmrep.ReportViewer.SelectionFormula = sSelection
                            CR.DataDefinition.RecordSelectionFormula = sSelection
                            If UCase(sCompanyShortCode) = "HN" And bReprint Then
                                CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                            End If

                            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                            CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                            CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                            CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                            CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                            CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                            CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                            CR.PrintToPrinter(1, True, 0, 0)
                            frmrep = Nothing
                            frmrep = New frmReports
                    End Select
                End If

                If sReply = vbNo Then
                    Select Case sSalesType
                        Case "INSTALLMENT"
                            If sCompanyShortCode = "ES" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_ESP.rpt"
                            ElseIf sCompanyShortCode = "AS" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_AS.rpt"
                            ElseIf sCompanyShortCode = "ML" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_ML.rpt"
                            ElseIf UCase(sCompanyShortCode) = "GMT" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_GMT.rpt"
                            End If
                            CR.Load(strpath)
                            ReportLogOnToServer(CR)
                            CR.Refresh()

                            frmrep.ReportViewer.ReportSource = CR
                            sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                            frmrep.ReportViewer.SelectionFormula = sSelection
                            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                            CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                            CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                            CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)

                            frmrep.Show()
                            frmrep.ReportViewer.Visible = True
                            frmrep.ReportViewer.DisplayToolbar = True
                            frmrep.ReportViewer.CloseView(False)
                            frmrep.WindowState = FormWindowState.Maximized
                            frmrep = Nothing
                            frmrep = New frmReports

                        Case "TRADEIN"
                            If sCompanyShortCode = "ES" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_ESP.rpt"
                            ElseIf sCompanyShortCode = "AS" Then
                                '''  strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_AS.rpt"
                                If sInvGstType = "" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_AS.rpt"
                                ElseIf sInvGstType = "Standard Rated Supply" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoiceStandard_AS.rpt"
                                ElseIf sInvGstType = "Exempt Supply" Or sInvGstType = "No GST" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoiceNoGST_AS.rpt"
                                End If
                            ElseIf sCompanyShortCode = "AJ" Then
                                '''  strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_AS.rpt"
                                If sInvGstType = "" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_AJ.rpt"
                                ElseIf sInvGstType = "Standard Rated Supply" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoiceStandard_AJ.rpt"
                                ElseIf sInvGstType = "Exempt Supply" Or sInvGstType = "No GST" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoiceNoGST_AJ.rpt"
                                End If
                            ElseIf UCase(sCompanyShortCode) = "GMT" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_GMT.rpt"
                            ElseIf UCase(sCompanyShortCode) = "ML" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_ML.rpt"
                            ElseIf UCase(sCompanyShortCode) = "HN" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_HN.rpt"
                            End If
                            CR.Load(strpath)
                            ReportLogOnToServer(CR)
                            CR.Refresh()
                            frmrep.ReportViewer.ReportSource = CR

                            sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                            frmrep.ReportViewer.SelectionFormula = sSelection
                            If UCase(sCompanyShortCode) = "HN" And bReprint Then
                                CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                            End If
                            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                            CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                            CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                            CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                            CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                            CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                            CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)

                            frmrep.Show()
                            frmrep.ReportViewer.Visible = True
                            frmrep.ReportViewer.DisplayToolbar = True
                            frmrep.ReportViewer.CloseView(False)
                            frmrep.WindowState = FormWindowState.Maximized
                            frmrep = Nothing
                            frmrep = New frmReports

                            If sCompanyShortCode = "ES" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInDetailInvoice_ESP.rpt"
                            End If

                            If ((UCase(sCompanyShortCode) <> "HN") And (UCase(sCompanyShortCode) <> "AS") And (UCase(sCompanyShortCode) <> "ML")) Then
                                CR.Load(strpath)
                                ReportLogOnToServer(CR)
                                CR.Refresh()
                                frmrep.ReportViewer.ReportSource = CR

                                sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                                frmrep.ReportViewer.SelectionFormula = sSelection
                                CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                                CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                                CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                                CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                                CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                                CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                                CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                                CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                                CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)

                                frmrep.Show()
                                frmrep.ReportViewer.Visible = True
                                frmrep.ReportViewer.DisplayToolbar = True
                                frmrep.ReportViewer.CloseView(False)
                                frmrep.WindowState = FormWindowState.Maximized
                                frmrep = Nothing
                                frmrep = New frmReports
                            End If
                        Case Else
                            If sCompanyShortCode = "ES" Then
                                strpath = Application.StartupPath & "\Reports\rptSalesInvoice_ESP.rpt"
                            ElseIf sCompanyShortCode = "AS" Then
                                '''strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AS.rpt"
                                If sInvGstType = "" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AS.rpt"
                                ElseIf sInvGstType = "Standard Rated Supply" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceStandard_AS.rpt"
                                ElseIf sInvGstType = "Exempt Supply" Or sInvGstType = "No GST" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceNoGST_AS.rpt"
                                End If
                            ElseIf sCompanyShortCode = "AJ" Then
                                '''strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AS.rpt"
                                If sInvGstType = "" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AJ.rpt"
                                ElseIf sInvGstType = "Standard Rated Supply" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceStandard_AJ.rpt"
                                ElseIf sInvGstType = "Exempt Supply" Or sInvGstType = "No GST" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceNoGST_AJ.rpt"
                                End If
                            ElseIf sCompanyShortCode = "ML" Then
                                strpath = Application.StartupPath & "\Reports\rptSalesInvoice_ML.rpt"
                            ElseIf UCase(sCompanyShortCode) = "GMT" Then
                                strpath = Application.StartupPath & "\Reports\rptSalesInvoice_GMT.rpt"
                            ElseIf UCase(sCompanyShortCode) = "HN" Then
                                strpath = Application.StartupPath & "\Reports\rptSalesInvoice_HN.rpt"
                            End If
                            CR.Load(strpath)
                            ReportLogOnToServer(CR)
                            CR.Refresh()
                            frmrep.ReportViewer.ReportSource = CR

                            sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                            frmrep.ReportViewer.SelectionFormula = sSelection
                            If UCase(sCompanyShortCode) = "HN" And bReprint Then
                                CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                            End If
                            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                            CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                            CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                            CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                            CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                            CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                            CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                            frmrep.Show()
                            frmrep.ReportViewer.Visible = True
                            frmrep.ReportViewer.DisplayToolbar = True
                            frmrep.ReportViewer.CloseView(False)
                            frmrep.WindowState = FormWindowState.Maximized
                            frmrep = Nothing
                            frmrep = New frmReports
                    End Select
                End If

                If sReply = vbCancel Then
                    Exit Sub
                End If
                If sReply = vbCancel Then
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Function fncSaveSchemeDet() As Boolean
        Try
            Dim iRet As Boolean
            Dim i As Long
            Dim sRemarks$
            Dim sSql$

            'iRet = objIns.InstallmentDetDelete(Trim(msInsAgreementNo), Trim(msInstallmentNo))
            'If Not iRet Then
            '    MessageBox.Show("Error in InstallmentSave")
            '    fncSaveInstallmentDet = False
            '    Exit Function
            'End If
            'iRet = objIns.InstallmentHeaderUpdate(Trim(msInsAgreementNo), _
            '                        Val(txtNoofInstallment.Text), Val(txtInstallmentAmount.Text), Val(txtTotalAmount.Text), Val(dTradeInSales), Val(dTradeInGST))

            'If Not iRet Then
            '    MessageBox.Show("Error in InstallmentSave")
            '    fncSaveInstallmentDet = False
            '    Exit Function
            'End If

            'iRet = objIns.InstallmentDetailSave(Trim(msInsAgreementNo), Trim(msInstallmentNo), _
            '                        Val(txtNoofInstallment.Text), Val(txtInstallmentAmount.Text), Val(txtTotalAmount.Text), sRemarks)
            'If Not iRet Then
            '    MessageBox.Show("Error in Save")
            '    fncSaveInstallmentDet = False
            '    Exit Function
            'End If
            fncSaveSchemeDet = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function
    Private Function fncPaymodeDetailSave(ByVal sInstallmentNo As String) As Boolean
        Dim objPM As New clsPayMode
        Try
            Dim bPayModeFlag As Boolean
            Dim sDate As String
            Dim i As Long
            bPayModeFlag = False
            sDate = Format(Now, "dd/MM/yyyy")
            For i = 0 To UBound(arrpay, 1)
                If arrpay(i, 1) > 0 Then
                    iRet = objPM.PayModeDetailSave(sInstallmentNo, sDate, Trim(arrpay(i, 3)), _
                                                 Val(arrpay(i, 1)), Val(arrpay(i, 2)), _
                                                 Trim(arrpay(i, 4)), "SCHEME")
                    bPayModeFlag = True
                    If Not iRet Then
                        fncPaymodeDetailSave = False
                        Exit Function
                    End If

                End If


            Next i
            If bPayModeFlag = False Then
                iRet = objPM.PayModeDetailSave(nNextInvoice, sDate, "CASH", _
                                            Val(0), 0, _
                                            "MANUAL", "SCHEME")
                If Not iRet Then
                    fncPaymodeDetailSave = False
                    Exit Function
                End If
            End If
            fncPaymodeDetailSave = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function
    Private Function fncGetSchemeNo() As Boolean
        Try
            Dim dsInsNo As New DataSet

            dsInsNo = objIns.GetSchemeNo()
            If dsInsNo.Tables(0).Rows.Count > 0 Then
                msSchemeNo = "GS-" & Trim(dsInsNo.Tables(0).Rows(0)("NextSchemeNo") & "")
                fncGetSchemeNo = True
            Else
                fncGetSchemeNo = False
                MessageBox.Show("Error in generating Scheme No!")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function
    Function fncCheckValidity() As Boolean
        Try
            fncCheckValidity = True

            If Not IsNumeric(txtSchemeValue.Text) Then
                MessageBox.Show("Scheme Value should be numeric!")
                fncCheckValidity = False
                txtSchemeValue.Focus()
                Exit Function
            ElseIf CLng(Val(txtSchemeValue.Text)) <= 0 Then
                MessageBox.Show("Scheme Value should be greater than zero!")
                fncCheckValidity = False
                txtSchemeValue.Focus()
                Exit Function
            ElseIf Trim(txtCustomerCode.Text) = "" Then
                MessageBox.Show("CustomerCode Should not be Empty")
                fncCheckValidity = False
                txtSchemeCode.Focus()
                Exit Function
            ElseIf Trim(txtSchemeCode.Text) = "" Then
                MessageBox.Show("SchemeCode Should not be Empty")
                fncCheckValidity = False
                txtSchemeCode.Focus()
                Exit Function

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function
End Class