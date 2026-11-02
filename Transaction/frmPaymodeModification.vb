Public Class frmPaymodeModification
    Public nCreditAvailable As Decimal
    Dim sInvoice As String
    Dim sMemberCode As String
    Dim msSalesManCode As String
    Dim sSalesType As String
    Dim sLocationCode As String
    Dim sInvoiceDate As String
    Private Sub frmPaymodeModification_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                cmdCancel_Click(sender, e)
            Case Keys.F6
                cmdClear_Click(sender, e)
            Case Keys.F4
                cmdSave_Click(sender, e)
        End Select
    End Sub

    Private Sub frmPaymodeModification_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' setForm(Me)
        txtInvoiceNo.Focus()
        dgvBills.RowCount = 0
        dgvPaymode.RowCount = 0
        subLoadinvoices()
        subCalcTotal()
    End Sub
    Private Sub subLoadinvoices()
        Dim dsInvoice As New DataSet
        Dim objInvoice As New clsSales
        Dim iRow As Long
        Try
            objInvoice = New clsSales
            dsInvoice = objInvoice.fncGetSalesHeader(gsCon, "Invoice", sTerminalCode)
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
                        'If Convert.ToString(dsPaymode.Tables("Pay").Rows(iRow).Item("Paymode")).ToUpper.Trim = "TRADEIN" Then
                        '    dgvPaymode(cNewAmount.Index, 3).ReadOnly = True
                        'End If
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
            lblTotal.Text = Val(fnGetdgvText(dgvBills, dgvBills.CurrentRow.Index, cPaidAmt.Index))
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
        Dim objpaymode As New clsSales
        Dim iRows As Long
        fncgetpaymode = ""
        Try
            objpaymode = New clsSales
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
        lblTotal.Text = 0.0
        sInvoice = ""
        sMemberCode = ""
        nCreditAvailable = "0"
        txtInvoiceNo.Text = ""
        txtInvoiceNo.Focus()
        dgvPaymode.Rows.Clear()
    End Sub

    Private Sub cmdClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClear.Click
        subClear()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        ' ''PnlSalesManPwd.Visible = True
        ' ''txtSalesManPWD.Text = ""
        ' ''If PnlSalesManPwd.Enabled Then txtSalesManPWD.Focus()
        ' ''dgvPaymode.CurrentCell = dgvPaymode(cPaymode.Index, 0)
        If Trim(gthisLocationType) = "HQ" Then
            If Val(lblTotal.Text) = (lblPaid.Text) Then
                fncPaymodeDetailSave()
                ' dgvBills_Click(sender, e)
                subClear()
            Else
                MsgBox("Amount Not Tally. Please Key in Excact Amount.", MsgBoxStyle.Information)
            End If
        Else
            PnlSalesManPwd.Visible = True
            txtSalesManPWD.Text = ""
            If PnlSalesManPwd.Enabled Then txtSalesManPWD.Focus()
            dgvPaymode.CurrentCell = dgvPaymode(cPaymode.Index, 0)
        End If

    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Dispose()
    End Sub
    Private Sub dgvBills_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvBills.Click
        Try
            sInvoice = fnGetdgvText(dgvBills, dgvBills.CurrentRow.Index, cInvoiceNo.Index)
            '  sMemberCode = fnGetdgvText(dgvBills, dgvBills.CurrentRow.Index, cMemberCode.Index)
            subLoadPayMode(sInvoice)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
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
        Dim objSales As New clsSales
        Dim iRow As Long
        Dim sPayMode As String
        Dim nPayAmount, nCreditAmount As Decimal
        Dim sSlipNo As String
        Dim bResult As Boolean
        Dim nOldCreditAmount As Decimal
        Try
            nCreditAmount = 0.0

            

            OpenConnection()
            BeginTransaction()
            bResult = objSales.fncUpdatePayModeDetails(Trim(sInvoice), Trim(sUserID))
            If bResult = False Then
                RollBackTransaction()
                Exit Function
            End If
            bResult = objSales.fncUpdatePayModeDetailRemarks(Trim(sInvoice), Trim(msSalesManCode), Trim(txtRemarks.Text), Trim(sUserID))
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
                    bResult = objSales.fncSalesPaymodeDetailSave(sInvoice, ServerDate, sPayMode, nPayAmount, "0", sSlipNo, "CASH")
                    If bResult = False Then
                        RollBackTransaction()
                        Exit Function
                    End If
                    bResult = objSales.fncSalesPaymodeChangeLogSave(sInvoice, ServerDate, sPayMode, nPayAmount)
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
            For iRow = 0 To dgvPaymode.RowCount - 1
                fnSetNumeric(dgvPaymode, iRow, cNewAmount.Index, Val(fnGetdgvText(dgvPaymode, iRow, cNewAmount.Index)))
                lblPaid.Text = Val(lblPaid.Text) + Val(fnGetdgvText(dgvPaymode, iRow, cNewAmount.Index))
                lblBalance.Text = Val(lblTotal.Text) - Val(lblPaid.Text)
            Next iRow
            lblPaid.Text = FormatNumber(lblPaid.Text, 2)
            lblBalance.Text = FormatNumber(lblBalance.Text, 2)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
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
                    sSalesType = Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("TerminalCode")).Trim
                    sInvoiceDate = Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("CustomerCode")).Trim
                    sLocationCode = Convert.ToString(dsInvoice.Tables("Invoice").Rows(iRow).Item("SalesManName")).Trim
                Next
            Else
                MsgBox("Invoice No Not Found", MsgBoxStyle.Information)
                subClear()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        Finally
            dsInvoice = Nothing
            objInvoice = Nothing
        End Try
    End Sub

    Private Sub txtSalesManPWD_LostFocus(sender As Object, e As System.EventArgs) Handles txtSalesManPWD.LostFocus
        Try
            If txtSalesManPWD.Text.Trim <> "" Then
                subGetPassword()
                If Val(lblTotal.Text) = (lblPaid.Text) Then
                    fncPaymodeDetailSave()
                    dgvBills_Click(sender, e)
                Else
                    MsgBox("Amount Not Tally. Please Key in Excact Amount.", MsgBoxStyle.Information)
                End If
            Else
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Private Sub subGetPassword()
        Dim i As Long
        Dim Ds As DataSet
        Dim objSalesMan As New clsSalesMan
        Try

            Ds = objSalesMan.GetSalesManwithPassword(txtSalesManPWD.Text.Trim)
            If Ds.Tables("Salesman").Rows.Count > 0 Then
                msSalesManCode = Trim(Ds.Tables("Salesman").Rows(0)("SalesmanCode") & "")
                '  txtSalesMan.Text = Trim(Ds.Tables("Salesman").Rows(0)("SalesManName") & "")
                ' nAllowSalesReturn = Ds.Tables("Salesman").Rows(0)("AllowSalesReturn") & ""
            Else
                MsgBox("SalesMan Password is Incorrect!", MsgBoxStyle.Critical)
                txtSalesManPWD.Text = ""
                txtSalesManPWD.Focus()
                Exit Sub
            End If
            PnlSalesManPwd.Visible = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

  
End Class