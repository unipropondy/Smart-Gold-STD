Public Class frmPaymentEntry
    Public nCreditAvailable As Decimal
    Dim sInvoice As String
    Dim sMemberCode As String
    Dim objPrint As New clsPrint
    Dim objgeneral As New clsJobSheet
    '  Public giCashDrawerUSBNumber As Integer = 7
    Dim dActualPaidAmount As Decimal
    Dim bCash As Boolean
    Private Sub frmPaymentEntry_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated

    End Sub

    Private Sub frmPaymentEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                cmdCancel_Click(sender, e)
            Case Keys.F6
                cmdClear_Click(sender, e)
            Case Keys.F4
                cmdSave_Click(sender, e)
            Case Keys.F8
                btnOpenCashDrawer_Click(sender, e)
        End Select
    End Sub

    Private Sub frmPaymentEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' setForm(Me)

        If Trim(Objuser.CompanyName) = "ABIRAAME JEWELLERS" Then
            opteTRS.Text = "Discount"
        End If
        subLoadinvoices()
        subLoadFlexi()

        subCalcTotal()
        dgvBills.Visible = True
        bCash = False
    End Sub
    Private Sub subLoadinvoices()
        Dim dsInvoice As New DataSet
        Dim objInvoice As New ClsSales
        Dim iRow As Long
        Try
            dgvBills.RowCount = 0
            objInvoice = New ClsSales
            dsInvoice = objInvoice.fncGetSalesHeader(gsCon, "Invoice", sTerminalCode)
            If dsInvoice.Tables("Invoice").Rows.Count > 0 Then
                For iRow = 0 To dsInvoice.Tables("Invoice").Rows.Count - 1
                    dgvBills.Rows.Add()
                    fnSetdgvText(dgvBills, iRow, cInvoiceNo.Index, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("InvoiceNo")).Trim)
                    fnSetdgvText(dgvBills, iRow, cInvoiceDate.Index, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("InvoiceDate")).Trim)
                    fnSetdgvText(dgvBills, iRow, cCustCode.Index, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("CustomerCode")).Trim)
                    fnSetdgvText(dgvBills, iRow, cTerminalCode.Index, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("TerminalCode")).Trim)
                    fnSetdgvText(dgvBills, iRow, cSalesmanCode.Index, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("SalesManName")).Trim)
                    fnSetdgvText(dgvBills, iRow, cInvoicePaymode.Index, fncgetpaymode(Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("InvoiceNo").Trim)))
                    fnSetdgvText(dgvBills, iRow, cNetAmount.Index, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("NetTotal")).Trim)
                    fnSetdgvText(dgvBills, iRow, cPaidAmt.Index, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("PaidAmt")).Trim)
                    fnSetdgvText(dgvBills, iRow, cChange.Index, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("BalanceAmt")).Trim)
                    fnSetdgvText(dgvBills, iRow, cGST.Index, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("GST")).Trim)
                Next

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        Finally
            dsInvoice = Nothing
            objInvoice = Nothing
        End Try
    End Sub
    Private Sub subLoadeTRS()
        Dim dsInvoice As New DataSet
        Dim objInvoice As New ClsSales
        Dim iRow As Long
        Try
            dgveTRS.RowCount = 0
            objInvoice = New ClsSales
            dsInvoice = objInvoice.fncGeteTRS(gsCon, "Invoice", sTerminalCode)
            If dsInvoice.Tables("Invoice").Rows.Count > 0 Then
                For iRow = 0 To dsInvoice.Tables("Invoice").Rows.Count - 1
                    dgveTRS.Rows.Add()
                    fnSetdgvText(dgveTRS, iRow, 0, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("InvoiceNo")).Trim)
                    fnSetdgvText(dgveTRS, iRow, 1, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("InvoiceDate")).Trim)
                    fnSetdgvText(dgveTRS, iRow, 2, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("Remarks")).Trim)
                    fnSetdgvText(dgveTRS, iRow, 3, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("Gst")).Trim)
                    fnSetdgvText(dgveTRS, iRow, 4, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("NetTotal")).Trim)

                Next

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        Finally
            dsInvoice = Nothing
            objInvoice = Nothing
        End Try
    End Sub
    Private Sub subLoadVoidedeTRS()
        Dim dsInvoice As New DataSet
        Dim objInvoice As New ClsSales
        Dim iRow As Long
        Try
            dgveTRS.RowCount = 0
            objInvoice = New ClsSales
            dsInvoice = objInvoice.fncGeteTRS(gsCon, "Invoice", sTerminalCode)
            If dsInvoice.Tables("Invoice").Rows.Count > 0 Then
                For iRow = 0 To dsInvoice.Tables("Invoice").Rows.Count - 1
                    dgveTRS.Rows.Add()
                    fnSetdgvText(dgveTRS, iRow, 0, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("InvoiceNo")).Trim)
                    fnSetdgvText(dgveTRS, iRow, 1, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("InvoiceDate")).Trim)
                    fnSetdgvText(dgveTRS, iRow, 2, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("Remarks")).Trim)
                    fnSetdgvText(dgveTRS, iRow, 3, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("Gst")).Trim)
                    fnSetdgvText(dgveTRS, iRow, 4, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("NetTotal")).Trim)

                Next

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        Finally
            dsInvoice = Nothing
            objInvoice = Nothing
        End Try
    End Sub
    Private Sub subLoadFlexi()
        Dim dsInvoice As New DataSet
        Dim objInvoice As New ClsSales
        Dim iRow As Long
        Try
            objInvoice = New ClsSales
            dsInvoice = objInvoice.fncGetFlexiDetails(gsConFlexi, "Invoice", sTerminalCode)
            If dsInvoice.Tables("Invoice").Rows.Count > 0 Then
                For iRow = 0 To dsInvoice.Tables("Invoice").Rows.Count - 1
                    dgvFlexi.Rows.Add()
                    fnSetdgvText(dgvFlexi, iRow, 0, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("SubscriptionID")).Trim)
                    fnSetdgvText(dgvFlexi, iRow, 1, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("NricNo")).Trim)
                    fnSetdgvText(dgvFlexi, iRow, 2, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("FlexiType")).Trim)
                    fnSetdgvText(dgvFlexi, iRow, 3, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("InvoiceNo")).Trim)
                    '  fnSetdgvText(dgvFlexi, iRow, cInvoicDate.Index, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("InvoiceDate")).Trim)
                    fnSetdgvText(dgvFlexi, iRow, 4, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("PayMode")).Trim)
                    fnSetdgvText(dgvFlexi, iRow, 5, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("Amount")).Trim)
                    fnSetdgvText(dgvFlexi, iRow, 6, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("Amount")).Trim)
                    fnSetdgvText(dgvFlexi, iRow, 7, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("CreateUser")).Trim)
                Next

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        Finally
            dsInvoice = Nothing
            objInvoice = Nothing
        End Try
    End Sub
    Private Sub subLoadPayMode(ByVal sInvoice As String)
        Dim dsPaymode As New DataSet
        Dim objPaymode As New ClsSales
        Dim iRow As Long
        Try
            objPaymode = New ClsSales
            ' subMemberCredit(sMemberCode)
            dsPaymode = objPaymode.fncGetPayModeDetail(gsCon, "Pay", sInvoice)

            If dsPaymode.Tables("Pay").Rows.Count > 0 Then
                dgvPaymode.Rows.Clear()
                For iRow = 0 To dsPaymode.Tables("Pay").Rows.Count - 1
                    If Convert.ToString(dsPaymode.Tables("Pay").Rows(iRow).Item("Paymode")).ToUpper.Trim <> "CREDIT" Then
                        dgvPaymode.Rows.Add()
                        fnSetdgvText(dgvPaymode, iRow, cPaymode.Index, Convert.ToString(dsPaymode.Tables("Pay").Rows(iRow).Item("Paymode")).Trim)
                        fnSetdgvText(dgvPaymode, iRow, cAmount.Index, Convert.ToString(dsPaymode.Tables("Pay").Rows(iRow).Item("Amount")).Trim)
                        fnSetdgvText(dgvPaymode, iRow, cSlipNo.Index, Convert.ToString(dsPaymode.Tables("Pay").Rows(iRow).Item("SlipNo")).Trim)
                        If Convert.ToString(dsPaymode.Tables("Pay").Rows(iRow).Item("Paymode")).ToUpper.Trim = "CASH" Then
                            dgvPaymode(cNewSlipNo.Index, dgvPaymode.CurrentRow.Index).ReadOnly = True
                        End If
                    Else
                        If nCreditAvailable > 0 Then
                            dgvPaymode.Rows.Add()
                            fnSetdgvText(dgvPaymode, iRow, cPaymode.Index, Convert.ToString(dsPaymode.Tables("Pay").Rows(iRow).Item("Paymode")).Trim)
                            fnSetdgvText(dgvPaymode, iRow, cAmount.Index, Convert.ToString(dsPaymode.Tables("Pay").Rows(iRow).Item("Amount")).Trim)
                            fnSetdgvText(dgvPaymode, iRow, cSlipNo.Index, Convert.ToString(dsPaymode.Tables("Pay").Rows(iRow).Item("SlipNo")).Trim)
                            If Convert.ToString(dsPaymode.Tables("Pay").Rows(iRow).Item("Paymode")).ToUpper.Trim = "CASH" Then
                                dgvPaymode(cNewSlipNo.Index, dgvPaymode.CurrentRow.Index).ReadOnly = True
                            End If
                        End If
                    End If
                Next iRow
            End If
            lblInvoice.Text = sInvoice
            lblPaid.Text = "0.00"
            dActualPaidAmount = 0
            lblTotal.Text = FormatNumber(fnGetdgvText(dgvBills, dgvBills.CurrentRow.Index, cNetAmount.Index), 2)
            lblTotal.Text = Val(fnGetdgvText(dgvBills, dgvBills.CurrentRow.Index, cNetAmount.Index))
            lblGST.Text = FormatNumber(fnGetdgvText(dgvBills, dgvBills.CurrentRow.Index, cGST.Index), 2)
            lblBalance.Text = "0.00"
            lblGST.Text = FormatNumber(lblGST.Text)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        Finally
            dsPaymode = Nothing
            objPaymode = Nothing
        End Try
    End Sub
    Private Sub subLoadPayModeFlexi(ByVal sInvoice As String)
        Dim dsPaymode As New DataSet
        Dim objPaymode As New ClsSales
        Dim iRow As Long
        Try
            objPaymode = New ClsSales
            ' subMemberCredit(sMemberCode)
            dsPaymode = objPaymode.fncGetPayModeDetailFlexi(gsConFlexi, "Pay", sInvoice)

            If dsPaymode.Tables("Pay").Rows.Count > 0 Then
                dgvPaymode.Rows.Clear()
                For iRow = 0 To dsPaymode.Tables("Pay").Rows.Count - 1
                    If Convert.ToString(dsPaymode.Tables("Pay").Rows(iRow).Item("Paymode")).ToUpper.Trim <> "CREDIT" Then
                        dgvPaymode.Rows.Add()
                        fnSetdgvText(dgvPaymode, iRow, cPaymode.Index, Convert.ToString(dsPaymode.Tables("Pay").Rows(iRow).Item("Paymode")).Trim)
                        fnSetdgvText(dgvPaymode, iRow, cAmount.Index, Convert.ToString(dsPaymode.Tables("Pay").Rows(iRow).Item("Amount")).Trim)
                        fnSetdgvText(dgvPaymode, iRow, cSlipNo.Index, Convert.ToString(dsPaymode.Tables("Pay").Rows(iRow).Item("SlipNo")).Trim)
                        If Convert.ToString(dsPaymode.Tables("Pay").Rows(iRow).Item("Paymode")).ToUpper.Trim = "CASH" Then
                            dgvPaymode(cNewSlipNo.Index, dgvPaymode.CurrentRow.Index).ReadOnly = True
                        End If
                    Else
                        If nCreditAvailable > 0 Then
                            dgvPaymode.Rows.Add()
                            fnSetdgvText(dgvPaymode, iRow, cPaymode.Index, Convert.ToString(dsPaymode.Tables("Pay").Rows(iRow).Item("Paymode")).Trim)
                            fnSetdgvText(dgvPaymode, iRow, cAmount.Index, Convert.ToString(dsPaymode.Tables("Pay").Rows(iRow).Item("Amount")).Trim)
                            fnSetdgvText(dgvPaymode, iRow, cSlipNo.Index, Convert.ToString(dsPaymode.Tables("Pay").Rows(iRow).Item("SlipNo")).Trim)
                            If Convert.ToString(dsPaymode.Tables("Pay").Rows(iRow).Item("Paymode")).ToUpper.Trim = "CASH" Then
                                dgvPaymode(cNewSlipNo.Index, dgvPaymode.CurrentRow.Index).ReadOnly = True
                            End If
                        End If
                    End If
                Next iRow
            End If
            lblInvoice.Text = sInvoice
            lblPaid.Text = "0.00"
            dActualPaidAmount = 0
            lblTotal.Text = FormatNumber(fnGetdgvText(dgvFlexi, dgvFlexi.CurrentRow.Index, cNetAmount.Index), 2)
            lblTotal.Text = Val(fnGetdgvText(dgvFlexi, dgvFlexi.CurrentRow.Index, cNetAmount.Index))
            lblBalance.Text = "0.00"
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        Finally
            dsPaymode = Nothing
            objPaymode = Nothing
        End Try
    End Sub
    Private Function fncgetpaymode(ByVal sInvoice As String) As String
        Dim dspaymode As New DataSet
        Dim objpaymode As New ClsSales
        Dim iRows As Long
        fncgetpaymode = ""
        Try
            objpaymode = New ClsSales
            dspaymode = objpaymode.fncGetPayModeDetailByInvoice(gsCon, "paymode", sInvoice)
            If dspaymode.Tables("paymode").Rows.Count > 0 Then
                For iRows = 0 To dspaymode.Tables("paymode").Rows.Count - 1
                    If fncgetpaymode = "" Then
                        fncgetpaymode = Convert.ToString(dspaymode.Tables("paymode").Rows(iRows).Item("paymode")).Trim
                    Else
                        fncgetpaymode = fncgetpaymode + " , " + Convert.ToString(dspaymode.Tables("paymode").Rows(iRows).Item("paymode")).Trim
                    End If

                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        Finally
            dspaymode = Nothing
            objpaymode = Nothing
        End Try
    End Function
    Private Sub subClear()
        lblInvoice.Text = ""
        lblBalance.Text = 0.0
        lblPaid.Text = 0.0
        dActualPaidAmount = 0
        lblTotal.Text = 0.0
        lblGST.Text = 0.0
        sInvoice = ""
        sMemberCode = ""
        bCash = False
        nCreditAvailable = "0"
        dgvPaymode.Rows.Clear()
        dgvBills.Rows.Clear()
        dgvFlexi.Rows.Clear()
    End Sub

    Private Sub cmdClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClear.Click
        subClear()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        dgvPaymode.CurrentCell = dgvPaymode(cPaymode.Index, 0)
        If fncValiDate() = False Then Exit Sub
        If Val(lblTotal.Text) = (lblPaid.Text) Then
            fncPaymodeDetailSave()
            'dgvBills_Click(sender, e)
            ' btnOpenCashDrawer_Click(sender, e)
            subClear()
            subLoadinvoices()
            subLoadFlexi()
            subCalcTotal()
        Else
            MsgBox("Amount Not Tally. Please Key in Excact Amount.", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Dispose()
    End Sub

    'Private Sub subMemberCredit(ByVal sMemberCode As String)
    '    Dim dsMC As New DataSet
    '    Dim objMC As New clsMember
    '    Dim iRow As Long
    '    Try
    '        nCreditAvailable = 0.0
    '        dsMC = objMC.fncGetMemberDetail(gsConn, "Mem", sMemberCode)
    '        If dsMC.Tables("Mem").Rows.Count > 0 Then
    '            For iRow = 0 To dsMC.Tables("Mem").Rows.Count - 1
    '                nCreditAvailable = Convert.ToDecimal(dsMC.Tables("Mem").Rows(iRow).Item("credit"))
    '            Next
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical)
    '    Finally
    '        dsMC = Nothing
    '        objMC = Nothing
    '    End Try
    'End Sub
    Private Function fncPaymodeDetailSave() As Boolean
        Dim objSales As New ClsSales
        Dim iRow As Long
        Dim sPayMode As String
        Dim nPayAmount, nCreditAmount As Decimal
        Dim sSlipNo As String
        Dim bResult As Boolean
        Dim nOldCreditAmount As Decimal
        Try
            nCreditAmount = 0.0


            If optInvoice.Checked Then
                OpenConnection()
                BeginTransaction()
                bResult = objSales.fncUpdatePayMode(Trim(sInvoice), Trim(sUserID))
                If bResult = False Then
                    RollBackTransaction()
                    Exit Function
                End If

                'If bResult = False Then
                '    RollBackTransaction()
                'Else
                '    CommitTransaction()
                'End If
                'CloseConnection()
                'OpenConnection()
                'BeginTransaction()
                bResult = objSales.fncUpdatePayModeGST(Trim(sInvoice), Val(dActualPaidAmount), Val(lblGST.Text), Trim(sUserID))
                If bResult = False Then
                    RollBackTransaction()
                    Exit Function
                End If



                For iRow = 0 To dgvPaymode.RowCount - 1
                    If Val(fnGetdgvText(dgvPaymode, iRow, cNewAmount.Index)) <> 0 Then
                        'sPayMode = Convert.ToString(fnGetdgvText(dgvPaymode, iRow, cPaymode.Index)).Trim
                        'nPayAmount = Val(fnGetdgvText(dgvPaymode, iRow, cNewAmount.Index))
                        'sSlipNo = Convert.ToString(fnGetdgvText(dgvPaymode, iRow, cNewSlipNo.Index)).Trim
                        'bResult = objSales.fncSalesPaymodeDetailSave(sInvoice, sInvoiceDate, sPayMode, nPayAmount, "0", sSlipNo, sSalesType)
                        sPayMode = Convert.ToString(fnGetdgvText(dgvPaymode, iRow, cPaymode.Index)).Trim
                        nPayAmount = Val(fnGetdgvText(dgvPaymode, iRow, cNewAmount.Index))
                        sSlipNo = Convert.ToString(fnGetdgvText(dgvPaymode, iRow, cNewSlipNo.Index)).Trim
                        bResult = objSales.fncSalesPaymodeDetailGSTSave(sInvoice, ServerDate, sPayMode, nPayAmount, "0", sSlipNo, "CASH")
                        If bResult = False Then
                            RollBackTransaction()
                            Exit Function
                        End If
                    End If
                Next iRow
                If bResult = False Then
                    RollBackTransaction()
                Else
                    CommitTransaction()
                End If
            ElseIf optFlexi.Checked Then
                OpenConnectionFlexi()
                BeginTransactionFlexi()
                bResult = objSales.fncUpdatePayModeFlexi(Trim(sInvoice), Trim(sUserID))
                If bResult = False Then
                    RollBackTransactionFlexi()
                    Exit Function
                End If

                If bResult = False Then
                    RollBackTransactionFlexi()
                Else
                    CommitTransactionFlexi()
                End If
            End If
        Catch ex As Exception
            bResult = False
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        Finally
            fncPaymodeDetailSave = bResult
            CloseConnection()
            objSales = Nothing
        End Try
    End Function
    Private Sub subCalcTotal()
        Dim iRow As Long
        Try
            lblPaid.Text = 0
            lblBalance.Text = 0
            dActualPaidAmount = 0
            For iRow = 0 To dgvPaymode.RowCount - 1
                fnSetNumeric(dgvPaymode, iRow, cNewAmount.Index, Val(fnGetdgvText(dgvPaymode, iRow, cNewAmount.Index)))
                lblPaid.Text = Val(lblPaid.Text) + Val(fnGetdgvText(dgvPaymode, iRow, cNewAmount.Index))
                lblBalance.Text = Val(lblTotal.Text) - (Val(lblPaid.Text) + Val(lblGST.Text))
            Next iRow
            dActualPaidAmount = (Val(lblPaid.Text))
            lblPaid.Text = (Val(lblPaid.Text) + Val(lblGST.Text))
            lblPaid.Text = FormatNumber(lblPaid.Text, 2)
            lblBalance.Text = FormatNumber(lblBalance.Text, 2)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Function fncValiDate() As Boolean
        Dim bstatus As Boolean
        Dim iRow As Long
        bstatus = False
        Try
            For iRow = 0 To dgvPaymode.RowCount - 1
                If Val(fnGetdgvText(dgvPaymode, iRow, cAmount.Index)) > 0 And Trim(fnGetdgvText(dgvPaymode, iRow, cPaymode.Index)) <> "TRADEIN" Then
                    If Val(lblGST.Text) = 0 Then
                        If Val(fnGetdgvText(dgvPaymode, iRow, cAmount.Index)) <> Val(fnGetdgvText(dgvPaymode, iRow, cNewAmount.Index)) Then
                            MsgBox("Paymode Differs. Please Check", MsgBoxStyle.Information)
                            Exit Function
                        End If
                    End If
                    If Trim(fnGetdgvText(dgvPaymode, iRow, cPaymode.Index)) = "CASH" Then
                        bCash = True
                    End If
                End If
            Next iRow
            bstatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncValiDate = bstatus
        End Try
    End Function
    Private Sub dgvPaymode_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPaymode.CellEnter
        Try
            'If e.ColumnIndex = cNewAmount.Index Then
            subCalcTotal()
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub dgvPaymode_CellLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPaymode.CellLeave
        Try
            'If e.ColumnIndex = cNewAmount.Index Then
            subCalcTotal()
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub txtInvoiceNo_LostFocus(sender As Object, e As System.EventArgs) Handles txtInvoiceNo.LostFocus
        If txtInvoiceNo.Text.Trim <> "" Then
            dgvBills.Rows.Clear()
            subLoadinvoicesbyInvoiceNo()
            If dgvBills.RowCount > 0 Then
                sInvoice = fnGetdgvText(dgvBills, dgvBills.CurrentRow.Index, cInvoiceNo.Index)
                subLoadPayMode(sInvoice)
            End If
        End If
    End Sub
    Private Sub subLoadinvoicesbyInvoiceNo()
        Dim dsInvoice As New DataSet
        Dim objInvoice As New ClsSales
        Dim iRow As Long
        Try
            objInvoice = New ClsSales
            dsInvoice = objInvoice.fncGetSalesHeaderByInvoiceNo(gsCon, "Invoice", txtInvoiceNo.Text.Trim)
            If dsInvoice.Tables("Invoice").Rows.Count > 0 Then
                For iRow = 0 To dsInvoice.Tables("Invoice").Rows.Count - 1
                    dgvBills.Rows.Add()
                    fnSetdgvText(dgvBills, iRow, cInvoiceNo.Index, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("InvoiceNo")).Trim)
                    fnSetdgvText(dgvBills, iRow, cCustCode.Index, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("CustomerCode")).Trim)
                    fnSetdgvText(dgvBills, iRow, cTerminalCode.Index, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("TerminalCode")).Trim)
                    fnSetdgvText(dgvBills, iRow, cSalesmanCode.Index, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("SalesManName")).Trim)
                    fnSetdgvText(dgvBills, iRow, cInvoicePaymode.Index, fncgetpaymode(Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("InvoiceNo").Trim)))
                    fnSetdgvText(dgvBills, iRow, cNetAmount.Index, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("NetTotal")).Trim)
                    fnSetdgvText(dgvBills, iRow, cPaidAmt.Index, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("PaidAmt")).Trim)
                    fnSetdgvText(dgvBills, iRow, cChange.Index, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("BalanceAmt")).Trim)
                    fnSetdgvText(dgvBills, iRow, cGST.Index, Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("GST")).Trim)
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        Finally
            dsInvoice = Nothing
            objInvoice = Nothing
        End Try
    End Sub

    Private Sub dgvBills_Click(sender As Object, e As System.EventArgs) Handles dgvBills.Click
        Try
            sInvoice = fnGetdgvText(dgvBills, dgvBills.CurrentRow.Index, cInvoiceNo.Index)
            '  sMemberCode = fnGetdgvText(dgvBills, dgvBills.CurrentRow.Index, cMemberCode.Index)
            subLoadPayMode(sInvoice)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub optInvoice_CheckedChanged(sender As Object, e As System.EventArgs) Handles optInvoice.CheckedChanged
        If optInvoice.Checked = True Then
            dgvBills.Visible = True
            subLoadinvoices()
        Else
            dgvBills.Visible = False
        End If
    End Sub

    Private Sub optFlexi_CheckedChanged(sender As Object, e As System.EventArgs) Handles optFlexi.CheckedChanged
        If optFlexi.Checked = True Then
            dgvFlexi.Visible = True
            subLoadFlexi()
        Else
            dgvFlexi.Visible = False
        End If
    End Sub

    Private Sub dgvFlexi_Click(sender As Object, e As System.EventArgs) Handles dgvFlexi.Click
        Try
            sInvoice = fnGetdgvText(dgvFlexi, dgvFlexi.CurrentRow.Index, 3)
            '  sMemberCode = fnGetdgvText(dgvBills, dgvBills.CurrentRow.Index, cMemberCode.Index)
            subLoadPayModeFlexi(sInvoice)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    'Private Sub subOpenCashDrawer()
    '    Dim objSales As New ClsSales
    '    Try
    '        objSales.subOpenCashDrawer(gsReceiptPrinterName)
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub dgvFlexi_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvFlexi.CellContentClick

    End Sub

    Private Sub btnOpenCashDrawer_Click(sender As System.Object, e As System.EventArgs) Handles btnOpenCashDrawer.Click
        ''subOpenCashDrawerInSP()
        'If UCase(gsCashDrawerPort) = "LPT1" Then
        '    objPrint = New ClsSales
        '    objPrint.subOpenCashDrawer(gsPrinterName)
        '    subCashDrawerSave()
        'ElseIf UCase(Trim(gsCashDrawerPort)) = "USB" Then
        '    objPrint.subOpenCashDrawerInUSB(giCashDrawerUSBNumber)
        '    subCashDrawerSave()
        '    'ElseIf gsCashDrawerPort.Contains("E20") Then
        '    '    objPrint = New ClsSales
        '    '    objPrint.subOpenRetailPOSCashDrawer(gsPrinterName)
        '    '    subCashDrawerSave()
        '    'ElseIf gsCashDrawerPort.Contains("COM") Then
        '    '    subOpenCashDrawerInSerialPort()
        '    '    subCashDrawerSave()
        '    'ElseIf UCase(gsCashDrawerPort) = "RP" Then
        '    '    objPrint = New ClsSales
        '    '    objPrint.subOpenCashDrawerRP(gsPrinterName)
        '    '    subCashDrawerSave()
        'End If
        objPrint.subOpenCashDrawer("Receipt Printer")
    End Sub

    Public Sub subCashDrawerSave()
        Dim objOrg As New clsCompany
        Dim sDate As String
        Try
            objOrg = New clsCompany
            objgeneral = New clsJobSheet

            objgeneral.OpenBatchPostinConnection()
            objgeneral.gpBeginTransaction()

            sDate = objgeneral.fncGetServerDate()
            If objOrg.fncSaveCashDrawerLog(sTerminalID, gTerminalCode, gTerminalCode, gsUserId, sDate, "Cash Drawer") Then
                objgeneral.gpCommitTransaction()
            Else
                objgeneral.gpRollBackBatchConnection()
            End If
        Catch ex As Exception
            'MainMod.ShowMessage(ex.Message)
            objgeneral.gpRollBackBatchConnection()
        Finally
            objgeneral.CloseBatchConnection()
        End Try
    End Sub


    Private Sub dgveTRS_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgveTRS.CellContentClick

    End Sub

    Private Sub dgveTRS_DoubleClick(sender As Object, e As System.EventArgs) Handles dgveTRS.DoubleClick
        sInvoice = fnGetdgvText(dgveTRS, dgveTRS.CurrentRow.Index, 0)
        Dim objeTRS As New frmeTRSStatement
        objeTRS.txtBillNo.Text = Trim(sInvoice)
        objeTRS.txtBillNo_LostFocus(sender, e)
        objeTRS.ShowDialog()

    End Sub

    Private Sub opteTRS_CheckedChanged(sender As Object, e As System.EventArgs) Handles opteTRS.CheckedChanged
        If opteTRS.Checked = True Then
            dgveTRS.Visible = True
            subLoadeTRS()
        Else
            dgveTRS.Visible = False
        End If
    End Sub

    Private Sub Label5_Click(sender As System.Object, e As System.EventArgs) Handles Label5.Click



    End Sub

    
End Class