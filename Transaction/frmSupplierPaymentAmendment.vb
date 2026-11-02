Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmSupplierPaymentAmendment
    Dim sSupplierPaymentNo As String
    Dim dm As New DataManager
    Dim sSupplierCode As String
    Private Sub cmdFetch_Click(sender As System.Object, e As System.EventArgs) Handles cmdFetch.Click
        subLoadPaymentDetails()
    End Sub
    Private Sub subLoadPaymentDetails()
        Dim sSql, sFromLocStatus As String
        Dim iRows, iCount As Integer
        Dim DM As New DataManager
        Dim drH As DataRow()
        Dim sFromDate, sToDate As String
        Dim sStatus As String

        'sSql &= "Select * from tblInventoryTransferHeader Where FromLocation = " & fncFormatSQLText(sLocation)
        'sSql &= " And TransferDate = " & fncFormatSQLDate(sTransferDate)

        sSql = "Select PaymentNo,PaymentDate,Paymode,PaymentAmount,Remarks from tblJobsheetPaymentdetail"
        sSql = sSql & " where VendorCode = " & fncFormatSQLText(txtSupplierCode.Text)
        sSql = sSql & " and  InvoiceNo = " & fncFormatSQLText(txtHeadInvoiceNo.Text)



        drH = DM.RowReturnProjExecNonQryWithDataRow(sSql)
        dgvSupplier.RowCount = 0
        If drH.Length > 0 Then
            dgvSupplier.Visible = True
            For iRows = 0 To drH.Length - 1
                dgvSupplier.Rows.Add()
                fnSetdgvText(dgvSupplier, iRows, 0, Trim(drH(iRows)("PaymentNo") & ""))
                fnSetdgvText(dgvSupplier, iRows, 1, Trim(drH(iRows)("PaymentDate") & ""))
                fnSetdgvText(dgvSupplier, iRows, 2, Trim(drH(iRows)("Paymode") & ""))
                fnSetdgvText(dgvSupplier, iRows, 3, Trim(drH(iRows)("PaymentAmount") & ""))
                fnSetdgvText(dgvSupplier, iRows, 4, Trim(drH(iRows)("Remarks") & ""))
               
            Next
        End If
        drH = Nothing
        sSql = Nothing
    End Sub
    Private Sub dgvSupplier_Click(sender As Object, e As System.EventArgs) Handles dgvSupplier.Click
        subSetPayment(dgvSupplier.CurrentRow.Index)
    End Sub
    Private Sub subSetPayment(ByVal iRow As Integer)
        Try
            If iRow >= 0 Then
                txtPaymentNo.Text = fnGetdgvText(dgvSupplier, iRow, 0)
                txtPaymentDate.Text = fnGetdgvText(dgvSupplier, iRow, 1)
                txtPaymode.Text = fnGetdgvText(dgvSupplier, iRow, 2)
                txtAmount.Text = fnGetdgvText(dgvSupplier, iRow, 3)
                txtRemarks.Text = fnGetdgvText(dgvSupplier, iRow, 4)

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        SubClearall()
    End Sub
    Public Function SubClearall()
        Dim objTran As New clsPurchase
        txtPaymentNo.Text = ""
        txtPaymentDate.Text = ""
        txtHeadInvoiceNo.Text = ""
        txtPaymode.Text = ""
        txtAmount.Text = ""
        dgvSupplier.RowCount = 0
        txtRemarks.Text = ""
        txtSupplierCode.Text = ""
        txtSupplierName.Text = ""
        chkCustomerCopy.Checked = False
    End Function

    Private Sub btnSupplierCodeSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSupplierCodeSearch.Click
        Try
            Dim e1 As New System.EventArgs
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSupplier", "L", 4, "SupplierName", "", "SupplierCode", "SupplierCode", 80, "SupplierName", "SupplierName", 180, "Address1", "Address1", 60, "Address2", "Address2", 80)
            frmHelpClass.ShowDialog()
            txtSupplierCode.Text = frmHelpClass.Var1.Trim
            txtSupplierName.Text = frmHelpClass.Var2.Trim
            'txtSupplierCode_LostFocus(sender, e1)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub cmdUpdate_Click(sender As System.Object, e As System.EventArgs) Handles cmdUpdate.Click
        Dim sExec As String
        Dim sPaymentDate As String
        Dim sRemarks As String
        Try
    
            If Trim(txtRemarks.Text) = "" Then
                MsgBox("Please Enter the Details", MsgBoxStyle.Information)
                txtRemarks.Focus()
                Exit Sub
            End If
          
            sExec = "Update tbljobsheetpaymentdetail set Remarks= " & fncFormatSQLText(txtRemarks.Text) & " Where PaymentNo=" & fncFormatSQLText(txtPaymentNo.Text)
            dm.ProjExecNonQry(sExec)

            MsgBox("Payment Details Update", MsgBoxStyle.Information)
            subPrint(txtHeadInvoiceNo.Text.Trim)
            cmdFetch_Click(sender, e)
            'SubClearall()
            SubClearall()
            Exit Sub
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Sub
    Private Sub subPrint(ByVal sTradeInIssueNo As String)
        Dim frmrep As New frmReports
        Dim CR As New ReportDocument
        Dim DS As New DataSet
        Dim objClsCompany As New clsCompany
        Dim sCompanyname, sAddress, sPhone, sFax, sReportTitle As String
        Try
            Dim ReportTitle As String
            Dim sSelection As String
            Dim sSalesType$
            Cursor = Cursors.WaitCursor
            DS = objClsCompany.getCompanyName
            If DS.Tables("GetCompanyName").Rows.Count > 0 Then
                sCompanyname = Trim(DS.Tables("GetCompanyName").Rows(0)("Name") & "")
                sAddress = Trim(DS.Tables("GetCompanyName").Rows(0)("Address1") & "") & " " & _
                             Trim(DS.Tables("GetCompanyName").Rows(0)("Address2") & "") & " " & _
                             Trim(DS.Tables("GetCompanyName").Rows(0)("Country") & "")
                sPhone = Trim(DS.Tables("GetCompanyName").Rows(0)("Phone") & "")
                sFax = Trim(DS.Tables("GetCompanyName").Rows(0)("Fax") & "")
            End If
            sReportTitle = "Trade In Issue Report For " & sTradeInIssueNo
            If UCase(sCompanyShortCode) = "HN" Then
                strpath = Application.StartupPath & "\Reports\rptSupplierVoucher.rpt"
            Else
                If chkCustomerCopy.Checked Then
                    strpath = Application.StartupPath & "\Reports\rptSupplierVoucher.rpt"
                Else
                    strpath = Application.StartupPath & "\Reports\rptSupplierVoucher_FileCopy.rpt"
                End If

            End If
            CR.Load(strpath)

            ReportLogOnToServer(CR)
            CR.Refresh()
            frmrep.ReportViewer.ReportSource = CR
            sSelection = "{tblJObSheetPaymentDetail.InvoiceNo} = " & fncFormatSQLText(Trim(sTradeInIssueNo))
            sSelection = sSelection & " And "
            sSelection = sSelection & " {tblJObSheetPaymentDetail.VendorCode} = " & Trim$(fncFormatSQLText(txtSupplierCode.Text))
            frmrep.ReportViewer.SelectionFormula = sSelection
            frmrep.Show()
            frmrep.ReportViewer.Visible = True
            frmrep.ReportViewer.DisplayToolbar = True
            frmrep.ReportViewer.CloseView(False)
            frmrep.WindowState = FormWindowState.Maximized
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor = Cursors.Default
            frmrep = Nothing
            CR = Nothing
            DS = Nothing
            objClsCompany = Nothing
        End Try
    End Sub

End Class