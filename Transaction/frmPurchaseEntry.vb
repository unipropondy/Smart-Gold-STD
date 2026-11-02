Public Class frmPurchaseEntry
    Dim objClsJobsheet As New ClsFixCostJobSheet
    Dim dm As New DataManager

    Private Sub btnJobSheetNoSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJobSheetNoSearch.Click
        Dim frmHelpJohSheet As New frmHelpSearch(0, 0, "tblJobSheetHeader", "L", 4, "JobSheetNo", "FixStatus='N'", "JobSheetNo", "JobSheetNo", 80, "CostPerGram", "CostPerGram", 80, "SupplierCode", "SupplierCode", 100, "PurchaseBillNo", "PurchaseBillNo", 80)
        Try
            frmHelpJohSheet.ShowDialog()
            txtJobSheetNo.Text = frmHelpJohSheet.Var1.Trim
            subGetJobSheetDetail()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            frmHelpJohSheet = Nothing
        End Try
    End Sub
    Private Sub txtJobSheetNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtJobSheetNo.KeyDown
        If e.KeyCode = Keys.F1 Then btnJobSheetNoSearch_Click(sender, e)
    End Sub

    Private Sub subGetJobSheetDetail()
        Dim dsJSD As New DataSet
        Dim dsPD As New DataSet
        Try
            If txtJobSheetNo.Text.Trim <> "" Then
                dsJSD = objClsJobsheet.GetJobSheetDetail(txtJobSheetNo.Text.Trim)
                If dsJSD.Tables(0).Rows.Count > 0 Then
                    txtInvoiceNo.Text = Trim(dsJSD.Tables(0).Rows(0)("PurchaseBillNo") & "")
                    txtIDD.Text = Format(CDate(Trim(dsJSD.Tables(0).Rows(0)("PurchaseBillDate") & "")), "dd")
                    txtIMM.Text = Format(CDate(Trim(dsJSD.Tables(0).Rows(0)("PurchaseBillDate") & "")), "MM")
                    txtIYYYY.Text = Format(CDate(Trim(dsJSD.Tables(0).Rows(0)("PurchaseBillDate") & "")), "yyyy")
                    txtSupplier.Text = Trim(dsJSD.Tables(0).Rows(0)("SupplierCode") & "")

                    dsPD = objClsJobsheet.GetPurchaseDetail(txtJobSheetNo.Text.Trim)
                    If dsPD.Tables(0).Rows.Count > 0 Then
                        txtTotalGSTSales.Text = Val(dsPD.Tables(0).Rows(0)("TotalBeforeGST") & "")
                        txtGST.Text = Val(dsPD.Tables(0).Rows(0)("GST") & "")
                        txtTotalSales.Text = Val(dsPD.Tables(0).Rows(0)("TotalSales") & "")
                    End If
                    txtTotalGSTSales.Focus()

                Else
                    MessageBox.Show("Jobsheet No not found.")
                    txtJobSheetNo.Text = ""
                    If txtJobSheetNo.Enabled = True Then txtJobSheetNo.Focus()
                End If
            Else
                '
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmPurchaseEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
    Private Sub subClearMe()
        txtJobSheetNo.Clear()
        txtInvoiceNo.Clear()
        txtIDD.Clear()
        txtIMM.Clear()
        txtIYYYY.Clear()
        txtSupplier.Clear()
        txtTotalGSTSales.Clear()
        txtTotalSales.Clear()
        txtGST.Clear()
    End Sub

    Private Sub subSave()
        Dim sClassCode As String
        Try
            If fncValidateMe() Then

                If objClsJobsheet.UpdatePurchaseDetails(txtJobSheetNo.Text.Trim, txtInvoiceNo.Text.Trim, txtSupplier.Text.Trim, Val(txtTotalGSTSales.Text), Val(txtGST.Text), Val(txtTotalSales.Text)) Then
                    MessageBox.Show("Purchase Details updated successfully!")
                    subClearMe()
                Else
                    MessageBox.Show("Error in Save.")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function fncValidateMe() As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            If txtJobSheetNo.Text.Trim = "" Then
                MessageBox.Show("JobsheeNo should not be blank.")
                txtJobSheetNo.Focus()
                Exit Function
            End If
            If txtInvoiceNo.Text.Trim = "" Then
                MessageBox.Show("Invoice No should not be blank.")
                txtInvoiceNo.Focus()
                Exit Function
            ElseIf txtSupplier.Text.Trim = "" Then
                MessageBox.Show("Supplier Code should not be blank.")
                txtSupplier.Focus()
                Exit Function
            End If
            'If Trim(cboClass.SelectedItem) = "" Then
            '    MessageBox.Show("Please select the classCode.")
            '    cboClass.Focus()
            '    Exit Function
            'End If
            'If objClsJobsheet.IsBillClosed(txtJobSheetNo.Text.Trim) Then
            '    MessageBox.Show("Bill already closed. Cannot Fix Cost.")
            '    Exit Function
            'End If
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            bStatus = False
        Finally
            fncValidateMe = bStatus
        End Try

    End Function

    Private Sub txtTotalGSTSales_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotalGSTSales.Leave
        If Val(txtTotalGSTSales.Text) <> 0 Then
            txtGST.Text = Format(Val(txtTotalGSTSales.Text) * 0.09, "0.00")   'Val(txtTotalGSTSales.Text) * 0.09
            txtTotalSales.Text = Format(Val(txtTotalGSTSales.Text) + Val(txtGST.Text), "0.00")   'Val(txtTotalGSTSales.Text) + Val(txtGST.Text)
        End If

    End Sub
End Class