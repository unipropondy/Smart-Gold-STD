Public Class frmPurchaseTally

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        Dim sinventorycode As String
        Try
            gsConBatchPosting.ConnectionString = gsCon
            gsConBatchPosting.Open()
            gsTrans = gsConBatchPosting.BeginTransaction
            If fncInventory() Then
                gsTrans.Commit()
                MsgBox("Transaction Saved Successfully")
                btnClear_Click(sender, e)
            Else
                MsgBox("Transaction not saved")
                gsTrans.Rollback()

            End If
            gsConBatchPosting.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            gsConBatchPosting.Close()
        End Try
    End Sub

    Private Sub txtJobSheetNo_LostFocus(sender As Object, e As System.EventArgs) Handles txtJobSheetNo.LostFocus
        Try
            If txtJobSheetNo.Text.Trim <> "" Then
                subGetJobSheet(sender, e)
            Else
                ''subLockAllFlds(False)
                ''subClear()
                ''subClearDetail(False)
                ''subClearCatalogue()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub subGetJobSheet(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Long
        Dim Ds As DataSet
        Dim objJobSheet As New clsJobSheet
        Try
            Ds = objJobSheet.GetJobSheet(txtJobSheetNo.Text.Trim)
            If Ds.Tables("JobSheet").Rows.Count > 0 Then
                txtJobSheetNo.Text = Trim(Ds.Tables("JobSheet").Rows(0)("JobSheetNo") & "")
                '''  sJobSheetCode = txtJobSheetNo.Text.Trim
                txtInvoiceNo.Text = Trim(Ds.Tables("JobSheet").Rows(0)("PurchaseBillNo") & "")
                txtIYYYY.Text = Mid(Ds.Tables("JobSheet").Rows(0)("Pdate") & "", 1, 4)
                txtIMM.Text = Mid(Ds.Tables("JobSheet").Rows(0)("Pdate") & "", 5, 2)
                txtIDD.Text = Mid(Ds.Tables("JobSheet").Rows(0)("Pdate") & "", 7, 2)
                txtSupplierCode.Text = Trim(Ds.Tables("JobSheet").Rows(0)("SupplierCode") & "")

                txtHTotalCost.Text = Format(Val(Ds.Tables("JobSheet").Rows(0)("ManualLocalCOST") & ""), "#0.00")
                txtHTotalMC.Text = Format(Val(Ds.Tables("JobSheet").Rows(0)("ManualMc") & ""), "#0.00")
                txtHWeight.Text = Format(Val(Ds.Tables("JobSheet").Rows(0)("ManualWeight") & ""), "#0.00")


                chkHGST.Checked = IIf(Val(Ds.Tables("JobSheet").Rows(0)("Gst") & "") > 0, 1, 0)
                txtHGSTAmount.Text = Format(Val(Ds.Tables("JobSheet").Rows(0)("Gst") & ""), "#0.00")
            Else
                MsgBox("Job sheet no. is invalid!", MsgBoxStyle.Critical)
                ''btnClear_Click(sender, e)
                Exit Sub
            End If
            subGetPurchaseDetails(sender, e)
            Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Private Sub subGetPurchaseDetails(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Long
        Dim Ds As DataSet
        Dim objJobSheet As New clsJobSheet
        Try
            Ds = objJobSheet.GetPurchaseDetail(txtJobSheetNo.Text.Trim)
            If Ds.Tables("JobSheet").Rows.Count > 0 Then

                txtPurTotalCost.Text = Format(Val(Ds.Tables("JobSheet").Rows(0)("TotalCost") & ""), "#0.00")
                txtPurTotalMC.Text = Format(Val(Ds.Tables("JobSheet").Rows(0)("SMC") & ""), "#0.00")
                txtPurTotalWeight.Text = Format(Val(Ds.Tables("JobSheet").Rows(0)("Weight") & ""), "#0.00")

            Else
                MsgBox("Job sheet no. is invalid!", MsgBoxStyle.Critical)
                ''btnClear_Click(sender, e)
                Exit Sub
            End If
            '  subGetPurchaseDetails(sender, e)
            Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Private Sub btnJobSheetNoSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnJobSheetNoSearch.Click
        Try
            Dim e1 As New System.EventArgs
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblJobSheetHeader", "L", 4, "JobSheetNo", "", "JobSheetNo", "JobSheetNo", 80, "SupplierCode", "SupplierCode", 180, "PurchaseBillNo", "PurchaseBillNo", 60, "TotalWeight24k", "TotalWeight24k", 80)
            frmHelpClass.ShowDialog()
            txtJobSheetNo.Text = frmHelpClass.Var1.Trim
            SendKeys.Send(("{TAB}"))
            txtJobSheetNo_LostFocus(sender, e1)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Function fncInventory() As Boolean
        Dim sPDate As String
        Dim Ds As DataSet
        Dim objInventory As New clsInventory
        Dim objpurchase As New clsPurchase
        Dim objDirectPurchase As New clsDirectPurchase
        Dim sError As String
        Dim bInventory As Boolean
        Dim bPurchase As Boolean
        Dim bPurchaseBill As Boolean
        Dim bJobsheet As Boolean
        Dim i As Long
        Dim sError1 As String
        Dim sError2 As String
        Dim sError3 As String
        Dim sPMCType As String
        Dim sSMCType As String
        Dim sPayType As String
        Dim bNewJobSheet As Boolean
        Try

            bInventory = False




            'Weight saved is the piece weight; purchase cost is without mc; purchase mc is the total mc

            bPurchaseBill = objpurchase.PurchaseTallySave(txtSupplierCode.Text.Trim, txtJobSheetNo.Text.Trim, txtInvoiceNo.Text.Trim, Trim(sPDate), _
                             Val(txtHWeight.Text), Val(txtHTotalCost.Text), Val(txtHTotalMC.Text), _
                            Val(txtHBillWeight.Text), Val(txtHBillCost.Text), Val(txtHBillMC.Text), Val(txtPurTotalWeight.Text), Val(txtPurTotalCost.Text), Val(txtPurTotalMC.Text), sUserID)
            If Not bPurchaseBill Then
                MsgBox("Error in Saving Purchase Fix", MsgBoxStyle.Critical)
                fncInventory = False
                Exit Function
            End If




            fncInventory = True
            
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Function

    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        txtJobSheetNo.Text = ""
        txtSupplierCode.Text = ""
        txtIDD.Text = ""
        txtIMM.Text = ""
        txtIYYYY.Text = ""
        txtInvoiceNo.Text = ""
        txtHTotalCost.Text = ""
        txtHTotalMC.Text = ""
        txtHWeight.Text = ""
        txtHBillCost.Text = ""
        txtHBillWeight.Text = ""
        txtHBillMC.Text = ""
        txtHGSTAmount.Text = ""
        txtJobSheetNo.Focus()
    End Sub

    Private Sub frmPurchaseTally_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Escape
                    Me.Close()
                Case Keys.Enter
                    SendKeys.Send("{TAB}")
                Case Keys.F6
                    btnClear_Click(sender, e)
                Case Keys.F7
                    btnSave_Click(sender, e)
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class