Public Class frmCreditSalespayment
    Dim sSupplierPaymentNo As String
    Dim dm As New DataManager
    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmCreditSalespayment_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim objTran As New clsPurchase
        txtPaymentNo.Text = objTran.fncGetCreditSalesPaymentNo()
        txtPYY.Text = Now.Year
        txtPMM.Text = Format(Now.Month, "00")
        txtPDD.Text = Format(Now.Day, "00")
        txtFYY.Text = Now.Year
        txtFMM.Text = Format(Now.Month, "00")
        txtFDD.Text = Format(Now.Day, "00")
        txtTYY.Text = Now.Year
        txtTMM.Text = Format(Now.Month, "00")
        txtTDD.Text = Format(Now.Day, "00")
    End Sub


    Private Sub cmdFetch_Click(sender As System.Object, e As System.EventArgs) Handles cmdFetch.Click
        subLoadInvoice()
    End Sub
    Private Sub subLoadInvoice()
        Dim sSql, sFromLocStatus As String
        Dim iRows, iCount As Integer
        Dim DA As New DataManager
        Dim drH As DataRow()
        Dim sFromDate, sToDate As String
        Dim sStatus As String

        sFromDate = txtFYY.Text.Trim & txtFMM.Text.Trim & txtFDD.Text.Trim
        sToDate = txtTYY.Text.Trim & txtTMM.Text.Trim & txtTDD.Text.Trim
        sSql = "Select InvoiceNo,InvoiceDate,CustomerCode,CustomerName,NetTotal,PaidAmt,BalanceAmt,Remarks from tblCreditSalesHeader"
        sSql = sSql & " where  InvoiceDate >= " & fncFormatSQLText(sFromDate)
        sSql = sSql & " and  InvoiceDate <= " & fncFormatSQLText(sToDate)
        If optPaid.Checked = True Then
            sSql = sSql & " and  BalanceAmt=0"
        ElseIf optUnPaid.Checked = True Then
            sSql = sSql & " and  BalanceAmt>0"
        End If
        If Trim(txtCustomerCode.Text) <= "" Then
            sSql = sSql & " and CustomerCode = " & fncFormatSQLText(txtCustomerCode.Text)
        End If

        drH = dm.RowReturnProjExecNonQryWithDataRow(sSql)
        dgvSupplier.RowCount = 0
        If drH.Length > 0 Then
            dgvSupplier.Visible = True
            For iRows = 0 To drH.Length - 1
                dgvSupplier.Rows.Add()
                fnSetdgvText(dgvSupplier, iRows, 0, Trim(drH(iRows)("InvoiceNo") & ""))
                fnSetdgvText(dgvSupplier, iRows, 1, Trim(drH(iRows)("InvoiceDate") & ""))
                fnSetdgvText(dgvSupplier, iRows, 2, Trim(drH(iRows)("CustomerCode") & ""))
                fnSetdgvText(dgvSupplier, iRows, 3, Trim(drH(iRows)("CustomerName") & ""))
                fnSetdgvText(dgvSupplier, iRows, 4, Trim(drH(iRows)("NetTotal") & ""))
                fnSetdgvText(dgvSupplier, iRows, 5, Trim(drH(iRows)("PaidAmt") & ""))
                fnSetdgvText(dgvSupplier, iRows, 6, Trim(drH(iRows)("BalanceAmt") & ""))
                If Val(drH(iRows)("NetTotal")) = Val(drH(iRows)("PaidAmt")) Then
                    fnSetdgvText(dgvSupplier, iRows, 7, "PAID")
                ElseIf Val(drH(iRows)("PaidAmt")) > 0 And Val(drH(iRows)("PaidAmt")) <> Val(drH(iRows)("NetTotal")) Then
                    fnSetdgvText(dgvSupplier, iRows, 7, "PARTIAL")
                Else
                    fnSetdgvText(dgvSupplier, iRows, 7, "UNPAID")
                End If
                fnSetdgvText(dgvSupplier, iRows, 8, Trim(drH(iRows)("Remarks") & ""))
            Next
        End If
        drH = Nothing
        sSql = Nothing
    End Sub

    Private Sub dgvSupplier_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSupplier.CellContentClick

    End Sub

    Private Sub dgvSupplier_Click(sender As Object, e As System.EventArgs) Handles dgvSupplier.Click
        subSetJobsheet(dgvSupplier.CurrentRow.Index)
    End Sub
    Private Sub subSetJobsheet(ByVal iRow As Integer)
        Try
            If iRow >= 0 Then
                txtInvoiceNo.Text = fnGetdgvText(dgvSupplier, iRow, 0)
                txtInvoiceDate.Text = fnGetdgvText(dgvSupplier, iRow, 1)
                txtCustCode.Text = fnGetdgvText(dgvSupplier, iRow, 2)
                txtTotalAmount.Text = fnGetdgvText(dgvSupplier, iRow, 4)
                txtPaidAmount.Text = fnGetdgvText(dgvSupplier, iRow, 5)
                txtBalanceAmount.Text = fnGetdgvText(dgvSupplier, iRow, 6)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtAmount_Leave(sender As Object, e As System.EventArgs) Handles txtAmount.Leave
        If IsNumeric(txtAmount.Text) = False Then
            MsgBox("Please Enter Valid Numbers", MsgBoxStyle.Information)
            txtAmount.Text = ""
            txtAmount.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        SubClearall()
    End Sub
    Public Function SubClearall()
        Dim objTran As New clsPurchase
        txtCustCode.Text = ""
        txtInvoiceNo.Text = ""
        txtInvoiceDate.Text = ""
        txtTotalAmount.Text = ""
        txtPaidAmount.Text = "'"
        txtBalanceAmount.Text = ""
        dgvSupplier.RowCount = 0
        txtAmount.Text = ""
        txtRemarks.Text = ""
        txtPaymentNo.Text = objTran.fncGetCreditSalesPaymentNo()
        txtPYY.Text = Now.Year
        txtPMM.Text = Format(Now.Month, "00")
        txtPDD.Text = Format(Now.Day, "00")
        txtFYY.Text = Now.Year
        txtFMM.Text = Format(Now.Month, "00")
        txtFDD.Text = Format(Now.Day, "00")
        txtTYY.Text = Now.Year
        txtTMM.Text = Format(Now.Month, "00")
        txtTDD.Text = Format(Now.Day, "00")
        optUnPaid.Checked = True
    End Function


    Private Sub cmdUpdate_Click(sender As System.Object, e As System.EventArgs) Handles cmdUpdate.Click
        Dim sExec As String
        Dim sPaymentDate As String
        Try
            If Val(txtAmount.Text) + Val(txtPaidAmount.Text) > Val(txtTotalAmount.Text) Then
                MsgBox("Please Check the Amount, Total Amount Greater than Paid Amount", MsgBoxStyle.Information)
                txtAmount.Focus()
                Exit Sub
            End If
            If Trim(cboPaymode.Text) = "CHEQUE" And Trim(txtRemarks.Text) = "" Then
                MsgBox("Please Enter the Remarks", MsgBoxStyle.Information)
                txtRemarks.Focus()
                Exit Sub
            End If
            If Trim(cboPaymode.Text) = "" Then
                MsgBox("Please Enter the Paymode", MsgBoxStyle.Information)
                cboPaymode.Focus()
                Exit Sub
            End If
            If Val(txtAmount.Text) = 0 Then
                MsgBox("Please Enter the Amount", MsgBoxStyle.Information)
                txtAmount.Focus()
                Exit Sub
            End If
            If Len(txtInvoiceNo.Text) = 0 Then
                MsgBox("Please Select The Invoice details to update", MsgBoxStyle.Information)
                Exit Sub
            End If
            sPaymentDate = txtPYY.Text.Trim & txtPMM.Text.Trim & txtPDD.Text.Trim
            sExec = "INSERT into tblCreditSalespaymentdetail Values (" & fncFormatSQLText(txtPaymentNo.Text) & ","
            sExec = sExec & fncFormatSQLText(sPaymentDate) & "," & Val(txtAmount.Text) & ","
            sExec = sExec & fncFormatSQLText(txtRemarks.Text) & "," & fncFormatSQLText(txtInvoiceNo.Text) & ","
            sExec = sExec & fncFormatSQLDate(txtInvoiceDate.Text) & "," & fncFormatSQLText(txtCustCode.Text) & ","
            sExec = sExec & fncFormatSQLText(cboPaymode.Text) & "," & fncFormatSQLText(sUserID) & ", Getdate() ,"
            sExec = sExec & fncFormatSQLText(sUserID) & ", Getdate() )"
            dm.ProjExecNonQry(sExec)

            sExec = "Update tblCreditSalesHeader set PaidAmt=PaidAmt + " & Val(txtAmount.Text) & ", BalanceAmt=BalanceAmt- " & Val(txtAmount.Text) & "Where Invoiceno=" & fncFormatSQLText(txtInvoiceNo.Text)
            dm.ProjExecNonQry(sExec)

            sExec = "Update tblCompany set NextCreditSalesPaymentNo=NextCreditSalesPaymentNo + 1"
            dm.ProjExecNonQry(sExec)
            SubClearall()
            Exit Sub
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Sub
    Private Sub txtSupplierCode_TextChanged(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub btnCustomerCodeSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnCustomerCodeSearch.Click
        Try
            Dim e1 As New System.EventArgs
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblCustomer", "L", 4, "CustomerName", "", "CustomerCode", "CustomerCode", 80, "CustomerName", "CustomerName", 180, "Address1", "Address1", 60, "Address2", "Address2", 80)
            frmHelpClass.ShowDialog()
            txtCustomerCode.Text = frmHelpClass.Var1.Trim
            txtCustomerName.Text = frmHelpClass.Var2.Trim
            'txtSupplierCode_LostFocus(sender, e1)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class