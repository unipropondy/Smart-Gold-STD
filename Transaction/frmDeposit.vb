Imports System
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmDeposit
    Dim sCustomercode As String
    Dim sDepositNo As String
    Dim sDepositPaymentNo As String
    Dim objCust As New clsCustomer
    Dim objIns As New ClsInstallment
    Dim msSalesManCode As String
    Dim CompName$, Address1$, Address2$, Country$, PhoneNo$, FaxNo$
    Dim objSales As New ClsSales
    Dim sPayModeDetail As String
    Dim sMode As String
    Private Sub btnCustomerCodeSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnCustomerCodeSearch.Click
        Try
            ' Dim frmHelpCustomer As New frmHelpSearch(0, 0, "tblCustomer", "R", 4, "CustomerName", "", "CustomerCode", "CustomerCode", 80, "CustomerName", "CustomerName", 150, "Address1", "Address1", 150, "Address2", "Address2", 150)
            Dim frmHelpCustomer As New frmHelpSearch(0, 0, "tblCustomer", "R", 4, "CustomerName", "", "CustomerCode", "CustomerCode", 80, "CustomerName", "CustomerName", 150, "NricNo", "NricNo", 90, "Address1", "Address1", 80)
            frmHelpCustomer.ShowDialog()
            txtCustomerCode.Text = frmHelpCustomer.Var1.Trim
            txtCustomerName.Text = frmHelpCustomer.Var2.Trim
            subGetCustomerDetail()
            sCustomercode = Trim(txtCustomerCode.Text)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subGetCustomerDetail()
        Dim dsCust As New DataSet
        Try
            If txtCustomerCode.Text.Trim <> "" Then
                sCustomercode = Trim(txtCustomerCode.Text.Trim)
                dsCust = objCust.GetCustomerwithCodeHQ(txtCustomerCode.Text.Trim)
                If dsCust.Tables(0).Rows.Count > 0 Then
                    txtCustomerName.Text = Trim(dsCust.Tables(0).Rows(0)("CustomerName") & "")
                    lblCustomerType.Text = Trim(dsCust.Tables(0).Rows(0)("CustomerType") & "")
                    txtAddress1.Text = Trim(dsCust.Tables(0).Rows(0)("Address1") & "")
                    txtAddress2.Text = Trim(dsCust.Tables(0).Rows(0)("Address2") & "")
                    txtAddress3.Text = Trim(dsCust.Tables(0).Rows(0)("Address3") & "")
                    txtTotalAmount.Focus()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsCust = Nothing
        End Try
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        ClearAll()
    End Sub
    Public Function ClearAll()
        txtInvoiceNo.Text = ""
        txtCustomerCode.Text = ""
        txtCustomerName.Text = ""
        txtAddress1.Text = ""
        txtAddress2.Text = ""
        txtAddress3.Text = ""
        txtRemarks.Text = ""

        'txtODD.Text = ""
        'txtOMM.Text = ""
        'txtOYYYY.Text = ""
        txtIDD.Text = ""
        txtIMM.Text = ""
        txtIYYYY.Text = ""

        txtTotalCost.Text = ""
        txtDepositAmount.Text = ""
        txtNoofInstallment.Text = ""
        txtAmountPaid.Text = ""
        txtInstallmentAmount.Text = ""
        txtTotalAmount.Text = ""
        txtInventoryCode.Text = ""
        txtInventoryCode1.Text = ""
        txtInventoryCode2.Text = ""
        txtBalanceAmount.Text = ""
        txtOrderNo.Text = ""
        optCash.Checked = True

        txtEstiDD.Text = ""
        txtEstiMM.Text = ""
        txtEstiYY.Text = ""

        txtIssDD.Text = ""
        txtIssMM.Text = ""
        txtIssYY.Text = ""

        sMode = "I"
        btnSave.Enabled = True


    End Function

    Private Function fncCheckValidity() As Boolean
        Dim bstatus As Boolean
        Try
            bstatus = False

            If Len(txtCustomerCode.Text) = 0 Then
                MsgBox("Please enter the customer Code")
                txtCustomerCode.Focus()
                Exit Function
            End If
            If Not IsNumeric(txtDepositAmount.Text) Then
                MsgBox("Deposit Amount should be numeric!")
                txtDepositAmount.Focus()
                Exit Function
            ElseIf Val(txtDepositAmount.Text) = 0 Then
                MsgBox("Deposit Amount should be greater than zero!")
                txtDepositAmount.Focus()
                Exit Function

                'ElseIf Val(txtDepositAmount.Text) > Val(txtTotalCost.Text) Then
                '    MsgBox("Deposit Amount should be lower than the Sales Amount!")
                '    txtDepositAmount.Focus()
                '    Exit Function

                'ElseIf Val(txtInstallmentAmount.Text) <= 0 Then
                '    MsgBox("Installment Amount should be Greater than Zero!")
                '    txtInstallmentAmount.Focus()
                '    Exit Function
                'ElseIf Not IsDate(Trim(txtIDD.Text & "/" & txtIMM.Text & "/" & txtIYYYY.Text)) Then
                '    MsgBox("Last Date is Invalid!")
                '    fncCheckValidity = False
                '    txtIDD.Focus()
                '    Exit Function
            End If
            bstatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncCheckValidity = bstatus
        End Try
    End Function

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        Dim sRateFixed As String
        Dim sTradeInType As String
        Dim sStatus As String
        Dim sInvType As String
        Dim iCnt As Integer
        Dim dm As New DataManager
        Dim sExec As String
        Dim objPM As New clsPayMode
        Dim frmPM As New frmPayingmode
        Dim objSales As New ClsSales
        Dim dAdvancePaid As Double
        Dim sDepositPayment As Double

        Try
            mbSalesTransaction = False
            If fncCheckValidity() Then
                objIns.subOpenInsConnection()
                objIns.subBeginInsTransaction()
                objSales.subOpenSalesConnection()
                objSales.subBeginSalesTransaction()
                '  If sInsAgreementNo = "" Then

                If Trim(sMode) = "I" Then
                    fncGetDepositNo()
                    txtInvoiceNo.Text = Trim(sDepositNo)
                    If sDepositNo = "" Then
                        objIns.subRollBackInsTransaction()
                        objIns.subCloseInsConnection()
                        Exit Sub
                    End If
                Else
                    fncGetDepositPaymentNo()
                    'txtInvoiceNo.Text = Trim(sDepositNo)
                    If sDepositPaymentNo = "" Then
                        objIns.subRollBackInsTransaction()
                        objIns.subCloseInsConnection()
                        Exit Sub
                    End If
                End If
        
                dPaidAmount = 0
                ReturnAmount = 0
                ' nTotalNet = Val(txtDepositAmount.Text)
                If Trim(sMode) = "I" Then
                    nTotalNet = Val(txtDepositAmount.Text)
                Else
                    nTotalNet = Val(txtAmountPaid.Text)
                End If

                frmPM.SalesType = ""
                bInstallmentEntryFlag = True
                ' If UCase(sCompanyShortCode) = "HN" And (UCase(Objuser.CompanyName)) = "HANIFFA PTE LTD" Then
                frmPM.ShowDialog()
                'End If

                If nTotalNet > 0 Then
                    'save paymode detail

                    '  If UCase(sCompanyShortCode) = "HN" And (UCase(Objuser.CompanyName)) = "HANIFFA PTE LTD" Then
                    If Trim(sMode) = "I" Then
                        If Not fncPaymodeDetailSave(sDepositNo) Then
                            MessageBox.Show("Error in save Paymode")
                            objIns.subRollBackInsTransaction()
                            objIns.subCloseInsConnection()
                            objSales.subRollBackSalesTransaction()
                            objSales.subCloseSalesConnection()
                            Exit Sub
                        End If
                    Else
                        If Not fncPaymodeDetailSave(sDepositPaymentNo) Then
                            MessageBox.Show("Error in save Paymode")
                            objIns.subRollBackInsTransaction()
                            objIns.subCloseInsConnection()
                            objSales.subRollBackSalesTransaction()
                            objSales.subCloseSalesConnection()
                            Exit Sub
                        End If
                    End If
           
                    'End If
                    If Not fncSaveDeposit() Then
                        MessageBox.Show("Error in save Header")
                        objIns.subRollBackInsTransaction()
                        objIns.subCloseInsConnection()
                        objSales.subRollBackSalesTransaction()
                        objSales.subCloseSalesConnection()
                        Exit Sub
                    End If
                    'If Not fncSaveInstallmentEntryDetail() Then
                    '    MessageBox.Show("Error in save Detail")
                    '    objIns.subRollBackInsTransaction()
                    '    objIns.subCloseInsConnection()
                    '    Exit Sub
                    'End If
                    Dim objCustomer As New clsCustomer
                    If objCustomer.CustomerSave(txtCustomerCode.Text.Trim, txtCustomerName.Text.Trim, txtAddress1.Text.Trim, txtAddress2.Text.Trim, txtAddress3.Text.Trim, _
                    "", _
                    "", "", "", "", "", "", "", "", "", _
                    "", "", "", "", "", "", "", "", "") Then
                    Else
                        MessageBox.Show("Error in Save")
                        objIns.subRollBackInsTransaction()
                        objIns.subCloseInsConnection()
                        objSales.subRollBackSalesTransaction()
                        objSales.subCloseSalesConnection()
                    End If

                    dTradeInSales = 0
                    objSales.subCommitSalesTransaction()
                    objSales.subCloseSalesConnection()
                    objIns.subCommitInsTransaction()
                    objIns.subCloseInsConnection()
                    If UCase(sCompanyShortCode) = "HN" And (UCase(Objuser.CompanyName)) = "HANIFFA PTE LTD" Then
                        If txtInvoiceNo.Text.Trim <> "" Then
                            subPrintInstallment(txtInvoiceNo.Text.Trim)
                        End If
                    End If
                    btnClear_Click(Nothing, Nothing)
                Else
                    objIns.subRollBackInsTransaction()
                    objIns.subCloseInsConnection()
                    objSales.subRollBackSalesTransaction()
                    objSales.subCloseSalesConnection()
                    Exit Sub
                End If




            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
        End Try
    End Sub
    Private Sub subGetPayModeDetail(ByVal sInvoiceno As String)

        Dim dsClass As New DataSet
        Dim i As Integer
        Try
            sPayModeDetail = ""
            dsClass = objSales.GetPayModeDetail(sInvoiceno)
            If dsClass.Tables(0).Rows.Count > 0 Then
                For i = 0 To dsClass.Tables(0).Rows.Count - 1
                    ''If sPayModeDetail <> "" Then
                    ''    sPayModeDetail = sPayModeDetail & Environment.NewLine & Trim(dsClass.Tables(0).Rows(i)("PayMode") & "") & "             " & Trim(dsClass.Tables(0).Rows(i)("Amount") & "")
                    ''Else
                    ''    sPayModeDetail = Trim(dsClass.Tables(0).Rows(i)("PayMode") & "") & "             " & Trim(dsClass.Tables(0).Rows(i)("Amount") & "")
                    ''End If

                    sPayModeDetail = sPayModeDetail & " " & Trim(dsClass.Tables(0).Rows(i)("PayMode") & "") & "             " & Trim(dsClass.Tables(0).Rows(i)("Amount") & "")

                Next i
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsClass = Nothing
        End Try
    End Sub
    Private Sub subPrintInstallment(ByVal sInvoiceNo As String)
        Try
            Dim ReportTitle As String
            Dim sSelection As String
            Dim sSalesType$
            Dim frmrep As New frmReports
            Dim CR As New ReportDocument
            Dim j As Integer
            Cursor = Cursors.WaitCursor

            strpath = Application.StartupPath & "\Reports\rptDeposit_HN.rpt"
            CR.Load(strpath)
            If Not ReportLogOnToServer(CR) Then
                Exit Sub
            End If
            ReportLogOnToServer(CR)
            CR.Refresh()
            subGetPayModeDetail(Trim(sInvoiceNo))
            frmrep.ReportViewer.ReportSource = CR
            sSelection = "{tblDepositSave.DepositNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
            frmrep.ReportViewer.SelectionFormula = sSelection
            CR.DataDefinition.RecordSelectionFormula = sSelection
            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
            CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
            CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
            CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
            If Trim(sCompanyShortCode) = "HN" Then
                CR.DataDefinition.FormulaFields("PaymodeDetail").Text = fncFormatSQLText(sPayModeDetail)
            End If
            ' frmrep.Show()
            '   If bPrinter Then CR.PrintToPrinter(1, True, 0, 0)
            ''frmrep.ReportViewer.Visible = True
            ''frmrep.ReportViewer.DisplayToolbar = True
            ''frmrep.ReportViewer.CloseView(False)
            ''frmrep.WindowState = FormWindowState.Maximized
            ''frmrep = Nothing
            ''frmrep = New frmReports

            CR.PrintToPrinter(1, True, 0, 0)


            frmrep = Nothing
            frmrep = New frmReports

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Function fncPaymodeDetailSave(ByVal sOrderNo As String) As Boolean
        Dim objPM As New clsPayMode
        Try
            Dim bPayModeFlag As Boolean
            Dim sDate As String
            Dim i As Long
            Dim sSalesType As String
            bPayModeFlag = False
            sDate = Format(Now, "dd/MM/yyyy")

            sSalesType = "DEPOSIT"

            For i = 0 To UBound(arrpay, 1)
                If arrpay(i, 1) > 0 Then
                    iRet = objPM.PayModeDetailSave(sOrderNo, sDate, Trim(arrpay(i, 3)), _
                                                 Val(arrpay(i, 1)), Val(arrpay(i, 2)), _
                                                 Trim(arrpay(i, 4)), Trim(sSalesType))
                    bPayModeFlag = True
                    If Not iRet Then
                        fncPaymodeDetailSave = False
                        Exit Function
                    End If

                End If
            Next i
            If bPayModeFlag = False Then
                iRet = objPM.PayModeDetailSave(sOrderNo, sDate, "CASH", _
                                            Val(txtDepositAmount.Text), 0, _
                                            "MANUAL", Trim(sSalesType))
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

    Private Function fncGetDepositNo() As Boolean
        Dim dsIns As New DataSet
        Dim bStatus As Boolean
        Try
            bStatus = False
            dsIns = objIns.GetUpdateDepositNo()
            If dsIns.Tables(0).Rows.Count > 0 Then
                sDepositNo = Trim(dsIns.Tables(0).Rows(0)("DepositNo") & "")
            End If


            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncGetDepositNo = bStatus
        End Try
    End Function
    Private Function fncGetDepositPaymentNo() As Boolean
        Dim dsIns As New DataSet
        Dim bStatus As Boolean
        Try
            bStatus = False
            dsIns = objIns.GetUpdateDepositPaymentNo()
            If dsIns.Tables(0).Rows.Count > 0 Then
                sDepositPaymentNo = Trim(dsIns.Tables(0).Rows(0)("DepositPaymentNo") & "")
            End If


            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncGetDepositPaymentNo = bStatus
        End Try
    End Function
    Private Function fncSaveDeposit() As Boolean
        Dim bstatus As Boolean
        Dim dsG As New DataSet
        Dim iRet As Boolean
        Dim iCnt As Long
        Dim sSql As String
        Dim sLastDate As String
        Dim sInvdate As String
        Dim sEstidate As String
        Dim sSupplierDate As String
        Dim sGSTType As String
        Dim sStatus As String
        Dim sPayStatus As String
        bstatus = False
        Try
            sInvdate = CStr(Format(CDate(ServerDate), "yyyy") & "/" & Format(CDate(ServerDate), "MM") & "/" & Format(CDate(ServerDate), "dd"))
            sLastDate = txtIYYYY.Text & "/" & txtIMM.Text & "/" & txtIDD.Text
            sEstidate = txtEstiYY.Text & "/" & txtEstiMM.Text & "/" & txtEstiDD.Text
            sSupplierDate = txtIssYY.Text & "/" & txtIssMM.Text & "/" & txtIssDD.Text


            If Trim(sEstidate) = "//" Then
                sEstidate = "2000/01/01"
            End If

            If cboStatus.Text.Trim = "PROCESSING" Then
                sStatus = "P"
            ElseIf cboStatus.Text.Trim = "READY" Then
                sStatus = "R"
            ElseIf cboStatus.Text.Trim = "DELIVERED" Then
                sStatus = "L"
            End If

            If optCash.Checked Then
                sPayStatus = "C"
            Else
                sPayStatus = "G"
            End If

            ' If UCase(sCompanyShortCode) = "HN" And (UCase(Objuser.CompanyName)) = "HANIFFA PTE LTD" Then
            iRet = objIns.DepositSave(Trim(txtInvoiceNo.Text), sInvdate, _
                            UCase(Trim(txtCustomerCode.Text)), UCase(Trim(txtCustomerName.Text)), Trim(msSalesManCode), Val(txtDepositAmount.Text), Val(txtTotalAmount.Text), _
                              Val(txtAmountPaid.Text), Trim(txtInventoryCode.Text), Trim(txtRemarks.Text), Trim(sMode), Trim(sDepositPaymentNo), Trim(txtInventoryCode1.Text), Trim(txtInventoryCode2.Text), sEstidate)
            'Else

            '    iRet = objIns.DepositSaveJP(Trim(txtInvoiceNo.Text), sInvdate, _
            '                UCase(Trim(txtCustomerCode.Text)), Trim(msSalesManCode), Val(txtDepositAmount.Text), _
            '                 Trim(txtRemarks.Text), Trim(txtSupplierCode.Text), sEstidate, sSupplierDate, sStatus, sPayStatus, Trim(sMode), Trim(txtOrderNo.Text))


          '  End If




            If Not iRet Then
                fncSaveDeposit = False
                Exit Function
            End If
            fncSaveDeposit = True
            bstatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncSaveDeposit = bstatus
        End Try
    End Function
    Private Sub subGetPassword()
        Dim i As Long
        Dim Ds As DataSet
        Dim objSalesMan As New clsSalesMan
        Try

            Ds = objSalesMan.GetSalesManwithPassword(txtSalesManPWD.Text.Trim)
            If Ds.Tables("Salesman").Rows.Count > 0 Then
                msSalesManCode = Trim(Ds.Tables("Salesman").Rows(0)("SalesmanCode") & "")
                'txtSalesMan.Text = Trim(Ds.Tables("Salesman").Rows(0)("SalesManName") & "")
                'nAllowSalesReturn = Ds.Tables("Salesman").Rows(0)("AllowSalesReturn") & ""
            Else
                MsgBox("SalesMan Password is Incorrect!", MsgBoxStyle.Critical)
                txtSalesManPWD.Text = ""
                txtSalesManPWD.Focus()
                Exit Sub
            End If

            PnlSalesManPwd.Visible = False
            'If UCase(sCompanyShortCode) = "HN" And (UCase(Objuser.CompanyName)) = "HANIFFA PTE LTD" Then
            '    pnlBillCancelAdminPwd.Visible = True
            '    txtBCAdminPassword.Focus()
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub txtSalesManPWD_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSalesManPWD.LostFocus
        Try
            If txtSalesManPWD.Text.Trim <> "" Then
                subGetPassword()
            Else
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As System.Object, e As System.EventArgs) Handles btnPrint.Click
        If txtInvoiceNo.Text.Trim <> "" Then
            subPrintInstallment(txtInvoiceNo.Text.Trim)
        End If
    End Sub

    Private Sub frmDeposit_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Up
                    SendKeys.Send("+{Tab}")
                Case Keys.Down
                    SendKeys.Send("{Tab}")
                Case Keys.Enter
                    SendKeys.Send("{Tab}")
                Case Keys.Escape
                    btnClose_Click(Nothing, Nothing)
                Case Keys.F6
                    btnClear_Click(Nothing, Nothing)
                Case Keys.F7
                    btnSave_Click(Nothing, Nothing)
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnOrderPaymentNoSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnOrderPaymentNoSearch.Click
        Try
            ' If UCase(sCompanyShortCode) = "HN" And (UCase(Objuser.CompanyName)) = "HANIFFA PTE LTD" Then
            Dim frmHelpAgreement As New frmHelpSearch(0, 0, "tblDepositSave", "L", 4, "DepositNo", "", "DepositNo", "DepositNo", 100, "DepositDate", "DepositDate", 60, "CustomerCode", "CustomerCode", 80, "DepositAmount", "DepositAmount", 80)
            frmHelpAgreement.ShowDialog()
            txtInvoiceNo.Text = frmHelpAgreement.Var1.Trim
            If txtInvoiceNo.Text.Trim <> "" Then txtInvoiceNo_LostFocus(sender, e)
            'Else
            '    Dim frmHelpAgreement As New frmHelpSearch(0, 0, "tblDepositSaveJP", "L", 4, "DepositNo", "", "DepositNo", "DepositNo", 100, "OrderNo", "OrderNo", 60, "CustomerCode", "CustomerCode", 80, "DepositAmount", "DepositAmount", 80)
            '    frmHelpAgreement.ShowDialog()
            '    txtInvoiceNo.Text = frmHelpAgreement.Var1.Trim
            '    If txtInvoiceNo.Text.Trim <> "" Then txtInvoiceNo_LostFocus(sender, e)
            'End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtInvoiceNo_LostFocus(sender As Object, e As System.EventArgs) Handles txtInvoiceNo.LostFocus

        Try
            Dim sSql, sSelect As String
            Dim sInvoiceno As String
            Dim DR As DataRow()
            Dim Dr1 As DataRow()
            'If UCase(sCompanyShortCode) = "HN" And (UCase(Objuser.CompanyName)) = "HANIFFA PTE LTD" Then
            sSql = "Select * from tblDepositSave where DepositNo=" & fncFormatSQLText(txtInvoiceNo.Text)
            DR = DataManager.RowReturnProjExecNonQryWithDataRow(sSql)
            If DR.Length > 0 Then
                SubLoadDeposit()
            End If
            'Else
            '    sSql = "Select * from tblDepositSaveJP where DepositNo=" & fncFormatSQLText(txtInvoiceNo.Text)
            '    DR = DataManager.RowReturnProjExecNonQryWithDataRow(sSql)
            '    If DR.Length > 0 Then
            '        SubLoadDepositJP()
            '    End If
            'End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Public Function SubLoadDeposit()
        Dim DR As DataRow()
        Dim sOrderDate, sIssueDate, sDeliveryDate As String
        Dim sSql As String
        sSql = "select * from [tblDepositSave] where DepositNo=" & fncFormatSQLText(Trim(txtInvoiceNo.Text.Trim))
        DR = DataManager.RowReturnProjExecNonQryWithDataRow(sSql)
        If DR.Length > 0 Then
       
            txtInvoiceNo.Text = Trim(DR(0)("DepositNO") & "").ToUpper
           
            txtCustomerCode.Text = Trim(DR(0)("CustomerCode") & "")
            txtCustomerName.Text = Trim(DR(0)("CustomerName") & "")
            txtInventoryCode.Text = Trim(DR(0)("InventoryCode") & "")
            txtInventoryCode1.Text = Trim(DR(0)("InventoryCode1") & "")
            txtInventoryCode2.Text = Trim(DR(0)("InventoryCode2") & "")
            txtRemarks.Text = Trim(DR(0)("Remarks") & "")
            '  subGetCustomerDetail()
            txtDepositAmount.Text = Format(Val(DR(0)("DepositAmount")), "0.00")
            txtTotalAmount.Text = Format(Val(DR(0)("TotalAmount")), "0.00")
            txtAmountPaid.Text = Format(Val(DR(0)("AmountPaid")), "0.00")
            txtDepositAmount.Text = Val(txtDepositAmount.Text) + Val(txtAmountPaid.Text)
            txtAmountPaid.Text = 0
            txtBalanceAmount.Text = Val(txtTotalAmount.Text) - Val(txtDepositAmount.Text) - Val(txtAmountPaid.Text)
            sMode = "U"
        End If
    End Function
    Private Sub addCboStatusType()
        cboStatus.Items.Add("PROCESSING")
        cboStatus.Items.Add("READY")
        cboStatus.Items.Add("DELIVERED")
        cboStatus.SelectedIndex = 0
    End Sub
    Public Function SubLoadDepositJP()
        Dim DR As DataRow()
        Dim sOrderDate, sIssueDate, sDeliveryDate As String
        Dim sSql As String
        Dim sEstiDate As String
        Dim sLoadStatus As String
        Dim sLoadPayStatus As String

        sSql = "select * from [tblDepositSaveJP] where DepositNo=" & fncFormatSQLText(Trim(txtInvoiceNo.Text.Trim))
        DR = DataManager.RowReturnProjExecNonQryWithDataRow(sSql)
        If DR.Length > 0 Then

            txtInvoiceNo.Text = Trim(DR(0)("DepositNO") & "").ToUpper

            txtCustomerCode.Text = Trim(DR(0)("CustomerCode") & "")

            txtRemarks.Text = Trim(DR(0)("Remarks") & "")
            subGetCustomerDetail()
            txtDepositAmount.Text = Format(Val(DR(0)("DepositAmount")), "0.00")

            txtSupplierCode.Text = Trim(DR(0)("SupplierCode") & "")

            txtOrderNo.Text = Trim(DR(0)("OrderNo") & "")

            sLoadStatus = Trim(DR(0)("Status") & "")
            If Trim(sLoadStatus) = "P" Then
                cboStatus.Text = "PROCESSING"
            ElseIf Trim(sLoadStatus) = "R" Then
                cboStatus.Text = "READY"
            ElseIf Trim(sLoadStatus) = "L" Then
                cboStatus.Text = "DELIVERED"
            End If

            sLoadPayStatus = Trim(DR(0)("PayStatus") & "")
            If Trim(sLoadPayStatus) = "C" Then
                optCash.Checked = True
            ElseIf Trim(sLoadPayStatus) = "G" Then
                optGold.Checked = True
            End If


            sOrderDate = Trim(DR(0)("DepositDate") & "")
            If sOrderDate <> "" Then
                txtODD.Text = sOrderDate.Substring(0, 2)
                txtOMM.Text = sOrderDate.Substring(3, 2)
                txtOYYYY.Text = sOrderDate.Substring(6, 4)
            End If
            sIssueDate = Trim(DR(0)("SupplierDate") & "")
            If sIssueDate <> "" Then
                txtIssDD.Text = sIssueDate.Substring(0, 2)
                txtIssMM.Text = sIssueDate.Substring(3, 2)
                txtIssYY.Text = sIssueDate.Substring(6, 4)
            End If

            sDeliveryDate = Trim(DR(0)("EsitmatedDate") & "")
            If sDeliveryDate <> "" Then
                txtEstiDD.Text = sDeliveryDate.Substring(0, 2)
                txtEstiMM.Text = sDeliveryDate.Substring(3, 2)
                txtEstiYY.Text = sDeliveryDate.Substring(6, 4)
            End If

            sMode = "U"
        End If
    End Function

    Private Sub frmDeposit_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        txtSalesManPWD.Focus()
        getCurrentDate()
        addCboStatusType()
        sMode = "I"
    End Sub

    Private Sub btnNewCustomer_Click(sender As System.Object, e As System.EventArgs) Handles btnNewCustomer.Click
        Dim frmCust As New frmCustomer
        frmCust.ShowDialog()
        frmCust = Nothing
    End Sub

    Private Sub txtCustomerCode_LostFocus(sender As Object, e As System.EventArgs) Handles txtCustomerCode.LostFocus
        subGetCustomerDetail()
    End Sub

    Private Sub btnSupplierCodeSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSupplierCodeSearch.Click
        Try
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSupplier", "L", 4, "SupplierName", "", "SupplierCode", "SupplierCode", 80, "SupplierName", "SupplierName", 180, "Address1", "Address1", 60, "Address2", "Address2", 80)
            Dim e1 As New System.EventArgs
            frmHelpClass.ShowDialog()
            txtSupplierCode.Text = frmHelpClass.Var1.Trim
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub getCurrentDate()
        txtODD.Text = Format(ServerDate, "dd")
        txtOMM.Text = Format(ServerDate, "MM")
        txtOYYYY.Text = Format(ServerDate, "yyyy")
    End Sub

    Private Sub grpMain_Enter(sender As System.Object, e As System.EventArgs) Handles grpMain.Enter

    End Sub

    Private Sub txtIssYY_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtIssYY.TextChanged

    End Sub

    Private Sub Label51_Click(sender As System.Object, e As System.EventArgs) Handles Label51.Click

    End Sub

    Private Sub txtEstiYY_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtEstiYY.TextChanged

    End Sub

    Private Sub txtIssMM_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtIssMM.TextChanged

    End Sub

    Private Sub txtODD_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtODD.TextChanged

    End Sub

    Private Sub txtOMM_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtOMM.TextChanged

    End Sub

    Private Sub btnDelete_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete.Click

    End Sub

    Private Sub Label9_Click(sender As System.Object, e As System.EventArgs) Handles Label9.Click

    End Sub

    Private Sub txtDepositAmount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDepositAmount.LostFocus
        txtBalanceAmount.Text = Val(txtTotalAmount.Text) - Val(txtDepositAmount.Text) - Val(txtAmountPaid.Text)
    End Sub

    Private Sub txtDepositAmount_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtDepositAmount.TextChanged

    End Sub

    Private Sub Label15_Click(sender As System.Object, e As System.EventArgs) Handles Label15.Click

    End Sub

    Private Sub txtTotalAmount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotalAmount.LostFocus
        txtBalanceAmount.Text = Val(txtTotalAmount.Text) - Val(txtDepositAmount.Text) - Val(txtAmountPaid.Text)
    End Sub

    Private Sub txtAmountPaid_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmountPaid.LostFocus
        txtBalanceAmount.Text = Val(txtTotalAmount.Text) - Val(txtDepositAmount.Text) - Val(txtAmountPaid.Text)
    End Sub
End Class