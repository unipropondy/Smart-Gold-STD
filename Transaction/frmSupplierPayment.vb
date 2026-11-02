Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmSupplierPayment
    Dim sSupplierPaymentNo As String
    Dim dm As New DataManager
    Dim sSupplierCode As String
    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmSupplierPayment_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub frmSupplierPayment_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim objTran As New clsPurchase
        txtPaymentNo.Text = objTran.fncGetSupplierPaymentNo()
        txtPYY.Text = Now.Year
        txtPMM.Text = Format(Now.Month, "00")
        txtPDD.Text = Format(Now.Day, "00")
        txtFYY.Text = Now.Year
        txtFMM.Text = Format(Now.Month, "00")
        txtFDD.Text = Format(Now.Day, "00")
        txtTYY.Text = Now.Year
        txtTMM.Text = Format(Now.Month, "00")
        txtTDD.Text = Format(Now.Day, "00")
        lblBank.Visible = False
        cboBank.Visible = False
        chkCustomerCopy.Checked = False
    End Sub

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

    Private Sub cmdFetch_Click(sender As System.Object, e As System.EventArgs) Handles cmdFetch.Click
        subLoadJobSheet()
    End Sub
    Private Sub subLoadJobSheet()
        Dim sSql, sFromLocStatus As String
        Dim iRows, iCount As Integer
        Dim DM As New DataManager
        Dim drH As DataRow()
        Dim sFromDate, sToDate As String
        Dim sStatus As String

        sFromDate = txtFYY.Text.Trim & txtFMM.Text.Trim & txtFDD.Text.Trim
        sToDate = txtTYY.Text.Trim & txtTMM.Text.Trim & txtTDD.Text.Trim
        'sSql &= "Select * from tblInventoryTransferHeader Where FromLocation = " & fncFormatSQLText(sLocation)
        'sSql &= " And TransferDate = " & fncFormatSQLDate(sTransferDate)

        sSql = "Select JobSheetNo,PurchaseBillNo,PurchaseBillDate,(isnull(TotalCost,0)+isnull(GST,0)+isnull(ManualAdjustment,0))TotalCost,isnull(PaidAmount,0)PaidAmount,isnull(BalanceAmount,0)BalanceAmount,Remarks from tbljobsheetheader"
        sSql = sSql & " where SupplierCode = " & fncFormatSQLText(txtSupplierCode.Text)
        sSql = sSql & " and  convert(char(10),JobSheetDate,112) >= " & fncFormatSQLText(sFromDate)
        sSql = sSql & " and  convert(char(10),JobSheetDate,112) <= " & fncFormatSQLText(sToDate)
        If optPaid.Checked = True Then
            sSql = sSql & " and  isnull(BalanceAmount,0)=0"
        ElseIf optUnPaid.Checked = True Then
            sSql = sSql & " and  isnull(BalanceAmount,0)>0"
        End If

        drH = DM.RowReturnProjExecNonQryWithDataRow(sSql)
        dgvSupplier.RowCount = 0
        If drH.Length > 0 Then
            dgvSupplier.Visible = True
            For iRows = 0 To drH.Length - 1
                dgvSupplier.Rows.Add()
                fnSetdgvText(dgvSupplier, iRows, 0, Trim(drH(iRows)("JobSheetNo") & ""))
                fnSetdgvText(dgvSupplier, iRows, 1, Trim(drH(iRows)("PurchaseBillNo") & ""))
                fnSetdgvText(dgvSupplier, iRows, 2, Trim(drH(iRows)("PurchaseBillDate") & ""))
                fnSetdgvText(dgvSupplier, iRows, 3, Trim(drH(iRows)("TotalCost") & ""))
                fnSetdgvText(dgvSupplier, iRows, 4, Trim(drH(iRows)("PaidAmount") & ""))
                fnSetdgvText(dgvSupplier, iRows, 5, Trim(drH(iRows)("BalanceAmount") & ""))
                If Val(drH(iRows)("TotalCost")) = Val(drH(iRows)("PaidAmount")) Then
                    fnSetdgvText(dgvSupplier, iRows, 6, "PAID")
                ElseIf Val(drH(iRows)("PaidAmount")) > 0 And Val(drH(iRows)("PaidAmount")) <> Val(drH(iRows)("TotalCost")) Then
                    fnSetdgvText(dgvSupplier, iRows, 6, "PARTIAL")
                Else
                    fnSetdgvText(dgvSupplier, iRows, 6, "UNPAID")
                End If
                fnSetdgvText(dgvSupplier, iRows, 7, Trim(drH(iRows)("Remarks") & ""))
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
                txtJobSheetNo.Text = fnGetdgvText(dgvSupplier, iRow, 0)
                txtInvoiceNo.Text = fnGetdgvText(dgvSupplier, iRow, 1)
                txtInvoiceDate.Text = fnGetdgvText(dgvSupplier, iRow, 2)
                txtTotalAmount.Text = fnGetdgvText(dgvSupplier, iRow, 3)
                txtPaidAmount.Text = fnGetdgvText(dgvSupplier, iRow, 4)
                txtBalanceAmount.Text = fnGetdgvText(dgvSupplier, iRow, 5)
                txtHeadInvoiceNo.Text = fnGetdgvText(dgvSupplier, iRow, 1)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtAmount_Leave(sender As Object, e As System.EventArgs) Handles txtAmount.Leave
        'If IsNumeric(txtAmount.Text) = False Then
        '    MsgBox("Please Enter Valid Numbers", MsgBoxStyle.Information)
        '    txtAmount.Text = ""
        '    txtAmount.Focus()
        '    Exit Sub
        'End If
    End Sub

    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        SubClearall()
    End Sub
    Public Function SubClearall()
        Dim objTran As New clsPurchase
        txtJobSheetNo.Text = ""
        txtInvoiceNo.Text = ""
        txtHeadInvoiceNo.Text = ""
        txtInvoiceDate.Text = ""
        txtTotalAmount.Text = ""
        txtPaidAmount.Text = "'"
        txtBalanceAmount.Text = ""
        dgvSupplier.RowCount = 0
        txtAmount.Text = ""
        txtRemarks.Text = ""
        txtSupplierCode.Text = ""
        txtSupplierName.Text = ""
        txtPaymentNo.Text = objTran.fncGetSupplierPaymentNo()
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
        chkFinalPayment.Checked = False
        chkCustomerCopy.Checked = False
    End Function
    Public Function SubClear()
        Dim objTran As New clsPurchase
        txtJobSheetNo.Text = ""
        txtInvoiceNo.Text = ""
        txtInvoiceDate.Text = ""
        txtTotalAmount.Text = ""
        txtPaidAmount.Text = "'"
        txtBalanceAmount.Text = ""
        txtAmount.Text = ""
        txtRemarks.Text = ""
        txtHeadInvoiceNo.Text = ""
        'txtSupplierCode.Text = ""
        'txtSupplierName.Text = ""
        txtPaymentNo.Text = objTran.fncGetSupplierPaymentNo()
        'txtPYY.Text = Now.Year
        'txtPMM.Text = Format(Now.Month, "00")
        'txtPDD.Text = Format(Now.Day, "00")
        'txtFYY.Text = Now.Year
        'txtFMM.Text = Format(Now.Month, "00")
        'txtFDD.Text = Format(Now.Day, "00")
        'txtTYY.Text = Now.Year
        'txtTMM.Text = Format(Now.Month, "00")
        'txtTDD.Text = Format(Now.Day, "00")
        optUnPaid.Checked = True
    End Function

    Private Sub cmdUpdate_Click(sender As System.Object, e As System.EventArgs) Handles cmdUpdate.Click
        Dim sExec As String
        Dim sPaymentDate As String
        Dim sRemarks As String
        Try
            'If Val(txtAmount.Text) + Val(txtPaidAmount.Text) > Val(txtTotalAmount.Text) Then
            '    MsgBox("Please Check the Amount, Total Amount Greater than Paid Amount", MsgBoxStyle.Information)
            '    txtAmount.Focus()
            '    Exit Sub
            'End If
            If Trim(cboPaymode.Text) = "CHEQUE" And Trim(txtRemarks.Text) = "" Then
                MsgBox("Please Enter the Cheque No", MsgBoxStyle.Information)
                txtRemarks.Focus()
                Exit Sub
            End If
            If Trim(cboPaymode.Text) = "GOLD BAR" And Trim(txtRemarks.Text) = "" Then
                MsgBox("Please Enter the Bar Weight", MsgBoxStyle.Information)
                txtRemarks.Focus()
                Exit Sub
            End If
            If Trim(cboPaymode.Text) = "TRADE-IN" And Trim(txtRemarks.Text) = "" Then
                MsgBox("Please Enter the TradeIn Weight", MsgBoxStyle.Information)
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
            If Trim(cboPaymode.Text) = "CHEQUE" Then
                sRemarks = Trim(cboBank.Text) & " " & Trim(txtRemarks.Text)
            ElseIf Trim(cboPaymode.Text) = "GOLD BAR" Then
                sRemarks = Trim(txtIssueNo.Text) & " " & Trim(txtRemarks.Text)
            ElseIf Trim(cboPaymode.Text) = "TRADE-IN" Then
                sRemarks = Trim(txtIssueNo.Text) & " " & Trim(txtRemarks.Text)
            Else
                sRemarks = Trim(txtRemarks.Text)
            End If

            sExec = "INSERT into tbljobsheetpaymentdetail Values (" & fncFormatSQLText(txtPaymentNo.Text) & ","
            sExec = sExec & fncFormatSQLText(sPaymentDate) & "," & Val(txtAmount.Text) & ","
            sExec = sExec & fncFormatSQLText(sRemarks) & "," & fncFormatSQLText(txtHeadInvoiceNo.Text) & ","
            sExec = sExec & fncFormatSQLDate(txtInvoiceDate.Text) & "," & fncFormatSQLText(txtSupplierCode.Text) & ","
            sExec = sExec & fncFormatSQLText(cboPaymode.Text) & "," & fncFormatSQLText(sUserID) & ", Getdate() ,"
            sExec = sExec & fncFormatSQLText(sUserID) & ", Getdate() )"
            dm.ProjExecNonQry(sExec)

            sExec = "Update tblJobSheetHeader set PaidAmount=PaidAMount + " & Val(txtAmount.Text) & ", BalanceAmount=BalanceAmount- " & Val(txtAmount.Text) & "Where Jobsheetno=" & fncFormatSQLText(txtJobSheetNo.Text)
            dm.ProjExecNonQry(sExec)

            sExec = "Update tblCompany set NextSupplierPayment=NextSupplierPayment + 1"
            dm.ProjExecNonQry(sExec)

            'If chkFinalPayment.Checked Then
            '    sExec = "INSERT into tbljobsheetpaymentdetail Values (" & fncFormatSQLText(txtPaymentNo.Text) & ","
            '    sExec = sExec & fncFormatSQLText(sPaymentDate) & "," & Val(txtAmount.Text) & ","
            '    sExec = sExec & fncFormatSQLText(sRemarks) & "," & fncFormatSQLText(txtHeadInvoiceNo.Text) & ","
            '    sExec = sExec & fncFormatSQLDate(txtInvoiceDate.Text) & "," & fncFormatSQLText(txtSupplierCode.Text) & ","
            '    sExec = sExec & fncFormatSQLText(cboPaymode.Text) & "," & fncFormatSQLText(sUserID) & ", Getdate() ,"
            '    sExec = sExec & fncFormatSQLText(sUserID) & ", Getdate() )"
            '    dm.ProjExecNonQry(sExec)

            '    sExec = "Update tblJobSheetHeader set PaidAmount=PaidAMount + " & Val(txtAmount.Text) & ", BalanceAmount=BalanceAmount- " & Val(txtAmount.Text) & "Where Jobsheetno=" & fncFormatSQLText(txtJobSheetNo.Text)
            '    dm.ProjExecNonQry(sExec)

            '    sExec = "Update tblCompany set NextSupplierPayment=NextSupplierPayment + 1"
            '    dm.ProjExecNonQry(sExec)

            'End If


            MsgBox("Payment Successful", MsgBoxStyle.Information)
            subPrint(txtHeadInvoiceNo.Text.Trim)
            cmdFetch_Click(sender, e)
            'SubClearall()
            SubClear()
            Exit Sub
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Sub
    Private Sub subGetIssueDetail()
        Dim dsInsHdr As New DataSet
        Dim objTran As New clsPurchase
        Try
            If txtIssueNo.Text.Trim = "" Then Exit Sub
            dsInsHdr = objTran.GetGoldBarIssue(Trim$(txtIssueNo.Text))
            If dsInsHdr.Tables(0).Rows.Count > 0 Then
             
                txtRemarks.Text = Val(dsInsHdr.Tables(0).Rows(0)("IssuedWeight"))

       
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subGetTradeInIssueDetail()
        Dim dsInsHdr As New DataSet
        Dim objTran As New clsPurchase
        Try
            If txtIssueNo.Text.Trim = "" Then Exit Sub
            dsInsHdr = objTran.GetTradeInIssue(Trim$(txtIssueNo.Text))
            If dsInsHdr.Tables(0).Rows.Count > 0 Then

                txtRemarks.Text = Val(dsInsHdr.Tables(0).Rows(0)("TotalWeight")) & " Purity " & Trim(dsInsHdr.Tables(0).Rows(0)("Purity"))
                txtAmount.Text = Val(dsInsHdr.Tables(0).Rows(0)("TotalPrice"))


            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtSupplierCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtSupplierCode.KeyDown
        Try
            Dim e1 As New System.EventArgs
            If e.KeyCode = Keys.F1 Then
                Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSupplier", "L", 4, "SupplierName", "", "SupplierCode", "SupplierCode", 80, "SupplierName", "SupplierName", 180, "Address1", "Address1", 60, "Address2", "Address2", 80)
                frmHelpClass.ShowDialog()
                txtSupplierCode.Text = frmHelpClass.Var1.Trim
                txtSupplierName.Text = frmHelpClass.Var2.Trim
                SendKeys.Send("{TAB}")
                'txtJobSheetNo_LostFocus(sender, e1)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtSupplierCode_LostFocus(sender As Object, e As System.EventArgs) Handles txtSupplierCode.LostFocus
        Try
            sSupplierCode = Trim(txtSupplierCode.Text)
            subGetSupplier(sender, e)
            Exit Sub
        Catch ex As Exception
        End Try
    End Sub
    Private Sub subGetSupplier(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim sSql As String
            Dim DR As DataRow()
            If Trim(sSupplierCode) <> "" Then
                sSql = "Select * from tblSupplier where SupplierCode=" & fncFormatSQLText(sSupplierCode)
                DR = DataManager.RowReturnProjExecNonQryWithDataRow(sSql)
                If DR.Length > 0 Then
                    txtSupplierName.Text = Trim(DR(0)("SupplierName") & "")
                Else
                    MsgBox("Invalid supplier code!", vbCritical)
                    txtSupplierCode.Text = ""
                    txtSupplierName.Text = ""
                    sSupplierCode = ""
                End If
            Else
                txtSupplierCode.Text = ""
                txtSupplierName.Text = ""
                sSupplierCode = ""
            End If
            Exit Sub
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtSupplierCode_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtSupplierCode.TextChanged

    End Sub

    Private Sub cboPaymode_LostFocus(sender As Object, e As System.EventArgs) Handles cboPaymode.LostFocus
        If Trim(cboPaymode.Text) = "CHEQUE" Then
            lblBank.Visible = True
            cboBank.Visible = True
            lblRemarks.Text = "Cheque No"
            lblIssueNo.Visible = False
            txtIssueNo.Visible = False
            btnIssueNo.Visible = False
        ElseIf Trim(cboPaymode.Text) = "TRADE-IN" Then
            lblBank.Visible = False
            cboBank.Visible = False
            lblRemarks.Text = "Gram"
            lblIssueNo.Visible = True
            txtIssueNo.Visible = True
            btnIssueNo.Visible = True
            lblIssueNo.Text = "TRADE-IN Issue No"
        ElseIf Trim(cboPaymode.Text) = "GOLD BAR" Then
            lblBank.Visible = False
            cboBank.Visible = False
            lblRemarks.Text = "Gram"
            lblIssueNo.Visible = True
            txtIssueNo.Visible = True
            btnIssueNo.Visible = True
            lblIssueNo.Text = "GoldBar Issue No"
        End If
    End Sub

    Private Sub btnReprint_Click(sender As System.Object, e As System.EventArgs) Handles btnReprint.Click
        If Trim(txtPaymentNo.Text) <> "" Then
            subPrint(txtHeadInvoiceNo.Text.Trim)
        End If
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

    Private Sub txtSupplierName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtSupplierName.TextChanged

    End Sub

    Private Sub Label4_Click(sender As System.Object, e As System.EventArgs) Handles Label4.Click

    End Sub

    Private Sub btnIssueNo_Click(sender As System.Object, e As System.EventArgs) Handles btnIssueNo.Click
        If Trim(cboPaymode.Text) = "GOLD BAR" Then
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblGoldBarIssued", "L", 4, "IssueNo", "", "IssueNo", "IssueNo", 80, "PDate", "PDate", 60, "SupplierCode", "SupplierCode", 60, "IssuedWeight", "IssuedWeight", 80)
            frmHelpClass.ShowDialog()
            txtIssueNo.Text = frmHelpClass.Var1.Trim
            subGetIssueDetail()
        ElseIf Trim(cboPaymode.Text) = "TRADE-IN" Then
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblTradeissuehistoryHeader", "L", 4, "TradeIssueNo", "", "TradeIssueNo", "TradeIssueNo", 80, "TradeIssueDate", "TradeIssueDate", 60, "SupplierCode", "SupplierCode", 60, "TotalWeight", "TotalWeight", 80)
            frmHelpClass.ShowDialog()
            txtIssueNo.Text = frmHelpClass.Var1.Trim
            subGetTradeInIssueDetail()
        End If
   
    End Sub
End Class