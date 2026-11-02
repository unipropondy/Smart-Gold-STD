Public Class frmDailySales
    Dim objSales As New ClsSales
    Private Sub frmDailySales_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F6
                btnClear_Click(sender, e)
            Case Keys.F7
                btnSave_Click(sender, e)
            Case Keys.Escape
                btnClose_Click(sender, e)
            Case Keys.Return
                SendKeys.Send("{Tab}")
            Case Keys.Down
                SendKeys.Send("{Tab}")
            Case Keys.Up
                SendKeys.Send("+{Tab}")
        End Select
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        subSave()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        subClearMe()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub subSave()

        Try
            If fncValidateMe() Then

                If objSales.UpdateDailySales(txtInvoiceDate.Text.Trim, Val(txtCash.Text), Val(txtNets.Text), Val(txtVisa.Text), Val(txtMaster.Text), Val(txtAmex.Text), _
                                            Val(txtDiners.Text), Val(txtChq.Text), Val(txtTransfer.Text), Val(txtInstallment.Text), Val(txt24K.Text), Val(txtTradeInGold.Text), _
                                            Val(txtAdvancePayment.Text), Val(txtPettycash.Text), Val(txtAdvCreditUtilise.Text), Val(txtTotalGSTSales.Text), Val(txtGST.Text), Val(txtTotalSales.Text)) Then
                    MessageBox.Show("DailySales Updated Successfully!")
                    subClearMe()
                Else
                    MessageBox.Show("Error in Save.")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subClearMe()
        txtCash.Clear()
        txtNets.Clear()
        txtVisa.Clear()
        txtMaster.Clear()
        txtDiners.Clear()
        txtAmex.Clear()
        txtInstallment.Clear()
        txtTransfer.Clear()
        txtChq.Clear()
        txt24K.Clear()
        txtTradeInGold.Clear()
        txtAdvancePayment.Clear()
        txtPettycash.Clear()
        txtAdvCreditUtilise.Clear()
        txtTotalGSTSales.Clear()
        txtGST.Clear()
        txtTotalSales.Clear()

    End Sub

    Private Function fncValidateMe() As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            If txtInvoiceDate.Text.Trim = "" Then
                MessageBox.Show("InvoiceDate should not be blank.")
                txtInvoiceDate.Focus()
                Exit Function
            End If
    
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            bStatus = False
        Finally
            fncValidateMe = bStatus
        End Try

    End Function

    Private Sub frmDailySales_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtInvoiceDate.Value = CStr(Format(ServerDate, "dd/MM/yyyy"))

    End Sub

    Private Sub cmdFetch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFetch.Click
        Dim sSelect As String
        Dim sType As String
        Dim Dm As New DataManager


        sSelect = "select * from tblDailySales where  InvoiceDate=" & fncFormatSQLDate(txtInvoiceDate.Text)

        Dim DrCus() As DataRow
        DrCus = Dm.RowReturnProjExecNonQryWithDataRow(sSelect)
        If DrCus.Length > 0 Then
            txtCash.Text = DrCus(0)("Cash") & ""
            txtNets.Text = DrCus(0)("Nets") & ""
            txtVisa.Text = DrCus(0)("Visa") & ""
            txtMaster.Text = DrCus(0)("Master") & ""
            txtAmex.Text = DrCus(0)("Amex") & ""
            txtDiners.Text = DrCus(0)("Diners") & ""
            txtTransfer.Text = DrCus(0)("TransfertoAcc") & ""
            txtChq.Text = DrCus(0)("Chq") & ""
            txtInstallment.Text = DrCus(0)("Installment") & ""
            txt24K.Text = DrCus(0)("NonGST24K") & ""
            txtTradeInGold.Text = DrCus(0)("TradeInGold") & ""
            txtAdvancePayment.Text = DrCus(0)("AdvancePayment") & ""
            txtPettycash.Text = DrCus(0)("PettyCash") & ""
            txtAdvCreditUtilise.Text = DrCus(0)("AdvanceUtil") & ""
            txtTotalGSTSales.Text = DrCus(0)("TotalGSTSales") & ""
            txtGST.Text = DrCus(0)("GST") & ""
            txtTotalSales.Text = DrCus(0)("TotalSales") & ""


        End If
    End Sub

    Private Sub txtCash_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCash.Leave
        'If Val(txtCash.Text) >= 0 Then
        '    txtTotalGSTSales.Text = Val(txtTotalGSTSales.Text) + Val(txtCash.Text)
        '    txtGST.Text = Format(Val(txtTotalGSTSales.Text) * (9 / 109), "0.00")
        '    txtTotalSales.Text = Format(Val(txtTotalGSTSales.Text) + Val(txtGST.Text), "0.00")
        'End If
        subGetCalculate(sender, e)
    End Sub

    Private Sub txtNets_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNets.Leave
        'If Val(txtNets.Text) >= 0 Then
        '    txtTotalGSTSales.Text = Val(txtTotalGSTSales.Text) + Val(txtNets.Text)
        '    txtGST.Text = Format(Val(txtTotalGSTSales.Text) * (9 / 109), "0.00")
        '    txtTotalSales.Text = Format(Val(txtTotalGSTSales.Text) + Val(txtGST.Text), "0.00")
        'End If
        subGetCalculate(sender, e)
    End Sub

    Private Sub txtVisa_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtVisa.Leave
        'If Val(txtVisa.Text) >= 0 Then
        '    txtTotalGSTSales.Text = Val(txtTotalGSTSales.Text) + Val(txtVisa.Text)
        '    txtGST.Text = Format(Val(txtTotalGSTSales.Text) * (9 / 109), "0.00")
        '    txtTotalSales.Text = Format(Val(txtTotalGSTSales.Text) + Val(txtGST.Text), "0.00")
        'End If
        subGetCalculate(sender, e)
    End Sub

    Private Sub txtMaster_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMaster.Leave
        'If Val(txtMaster.Text) >= 0 Then
        '    txtTotalGSTSales.Text = Val(txtTotalGSTSales.Text) + Val(txtMaster.Text)
        '    txtGST.Text = Format(Val(txtTotalGSTSales.Text) * (9 / 109), "0.00")
        '    txtTotalSales.Text = Format(Val(txtTotalGSTSales.Text) + Val(txtGST.Text), "0.00")
        'End If
        subGetCalculate(sender, e)
    End Sub

    Private Sub txtAmex_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmex.Leave
        'If Val(txtAmex.Text) >= 0 Then
        '    txtTotalGSTSales.Text = Val(txtTotalGSTSales.Text) + Val(txtAmex.Text)
        '    txtGST.Text = Format(Val(txtTotalGSTSales.Text) * (9 / 109), "0.00")
        '    txtTotalSales.Text = Format(Val(txtTotalGSTSales.Text) + Val(txtGST.Text), "0.00")
        'End If
        subGetCalculate(sender, e)
    End Sub

    Private Sub txtDiners_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDiners.Leave
        'If Val(txtDiners.Text) >= 0 Then
        '    txtTotalGSTSales.Text = Val(txtTotalGSTSales.Text) + Val(txtDiners.Text)
        '    txtGST.Text = Format(Val(txtTotalGSTSales.Text) * (9 / 109), "0.00")
        '    txtTotalSales.Text = Format(Val(txtTotalGSTSales.Text) + Val(txtGST.Text), "0.00")
        'End If
        subGetCalculate(sender, e)
    End Sub

    Private Sub txtChq_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtChq.Leave
        'If Val(txtChq.Text) >= 0 Then
        '    txtTotalGSTSales.Text = Val(txtTotalGSTSales.Text) + Val(txtChq.Text)
        '    txtGST.Text = Format(Val(txtTotalGSTSales.Text) * (9 / 109), "0.00")
        '    txtTotalSales.Text = Format(Val(txtTotalGSTSales.Text) + Val(txtGST.Text), "0.00")
        'End If
        subGetCalculate(sender, e)
    End Sub

    Private Sub txtTransfer_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTransfer.Leave
        'If Val(txtTransfer.Text) >= 0 Then
        '    txtTotalGSTSales.Text = Val(txtTotalGSTSales.Text) + Val(txtTransfer.Text)
        '    txtGST.Text = Format(Val(txtTotalGSTSales.Text) * (9 / 109), "0.00")
        '    txtTotalSales.Text = Format(Val(txtTotalGSTSales.Text) + Val(txtGST.Text), "0.00")
        'End If
        subGetCalculate(sender, e)
    End Sub

    Private Sub txtInstallment_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInstallment.Leave
        'If Val(txtInstallment.Text) >= 0 Then
        '    txtTotalGSTSales.Text = Val(txtTotalGSTSales.Text) + Val(txtInstallment.Text)
        '    txtGST.Text = Format(Val(txtTotalGSTSales.Text) * (9 / 109), "0.00")
        '    txtTotalSales.Text = Format(Val(txtTotalGSTSales.Text) + Val(txtGST.Text), "0.00")
        'End If
        subGetCalculate(sender, e)
    End Sub

    Private Sub txtAdvCreditUtilise_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAdvCreditUtilise.Leave
        'If Val(txtAdvCreditUtilise.Text) >= 0 Then
        '    txtTotalGSTSales.Text = Val(txtTotalGSTSales.Text) + Val(txtAdvCreditUtilise.Text)
        '    txtGST.Text = Format(Val(txtTotalGSTSales.Text) * (9 / 109), "0.00")
        '    txtTotalSales.Text = Format(Val(txtTotalGSTSales.Text) + Val(txtGST.Text), "0.00")
        'End If
        subGetCalculate(sender, e)
    End Sub

    Private Sub txtPettycash_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPettycash.Leave
        'If Val(txtPettycash.Text) >= 0 Then
        '    txtTotalGSTSales.Text = Val(txtTotalGSTSales.Text) + Val(txtPettycash.Text)
        '    txtGST.Text = Format(Val(txtTotalGSTSales.Text) * (9 / 109), "0.00")
        '    txtTotalSales.Text = Format(Val(txtTotalGSTSales.Text) + Val(txtGST.Text), "0.00")
        'End If
        subGetCalculate(sender, e)
    End Sub

    Private Sub subGetCalculate(ByVal sender As Object, ByVal e As System.EventArgs)
        txtTotalGSTSales.Text = Val(txtCash.Text) + Val(txtNets.Text) + Val(txtVisa.Text) + Val(txtMaster.Text) + Val(txtAmex.Text) + Val(txtDiners.Text) + Val(txtChq.Text) + Val(txtTransfer.Text) + Val(txtInstallment.Text) + Val(txtPettycash.Text) + Val(txtAdvCreditUtilise.Text) - Val(txt24K.Text)
        txtGST.Text = Format(Val(txtTotalGSTSales.Text) * (9 / 109), "0.00")
        txtTotalSales.Text = Format(Val(txtTotalGSTSales.Text) + Val(txtTradeInGold.Text) + Val(txt24K.Text), "0.00")
    End Sub

    Private Sub txtTradeInGold_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTradeInGold.Leave
        subGetCalculate(sender, e)
    End Sub

    Private Sub txt24K_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt24K.Leave
        subGetCalculate(sender, e)
    End Sub
End Class