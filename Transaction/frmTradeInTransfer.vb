Public Class frmTradeInTransfer
    Dim objTIS As New ClsTradeinSales
    Dim objTII As New ClsTradeInIssue
    Public oTSalesConn As New ADODB.Connection
    Public oTSalesRs As New ADODB.Recordset
    Private Sub btnSupplierCodeSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSupplierCodeSearch.Click
        Try
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSupplier", "L", 4, "SupplierName", "", "SupplierCode", "SupplierCode", 80, "SupplierName", "SupplierName", 180, "Address1", "Address1", 60, "Address2", "Address2", 80)
            frmHelpClass.ShowDialog()
            txtsupplierCode.Text = frmHelpClass.Var1.Trim
            txtSupplierName.Text = frmHelpClass.Var2.Trim
            txtsupplierCode_LostFocus(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnTradeBillNoSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnTradeBillNoSearch.Click
        subGetTradeInInvoices()
    End Sub
    Private Sub subGetTradeInInvoices()
        Try
            Dim icnt As Long
            Dim sDate As Date
            Dim dsPur As New DataSet
            Dim sFromDate As String
            Dim sStatus As String


            If optTransferOut.Checked Then
                sStatus = "PU"
            Else
                sStatus = "TO"
            End If
            'Dim SBll As Purchase
            'SBll = New Purchase
            pnlInvoice.Visible = True
            dsPur = objTIS.GetTradeInTransfer(Trim(sStatus))
            If dsPur.Tables(0).Rows.Count > 0 Then
                '
                grdInvoice.RowCount = 0
                If Not IsNothing(dsPur) Then
                    If dsPur.Tables(0).Rows.Count > 0 Then
                        For icnt = 0 To dsPur.Tables(0).Rows.Count - 1
                            grdInvoice.Rows.Add()
                            Call fnSetdgvText(grdInvoice, icnt, 0, False)
                            fnSetdgvText(grdInvoice, icnt, 1, Trim(dsPur.Tables(0).Rows(icnt)("TradeinInventoryCode") & ""))
                            fnSetdgvText(grdInvoice, icnt, 2, Trim(dsPur.Tables(0).Rows(icnt)("InvoiceDate") & ""))
                            fnSetdgvText(grdInvoice, icnt, 3, Trim(dsPur.Tables(0).Rows(icnt)("TotalWeight") & ""))
                            fnSetdgvText(grdInvoice, icnt, 4, Trim(dsPur.Tables(0).Rows(icnt)("TOtalPrice") & ""))
                        Next
                    End If

                End If
            Else
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub txtsupplierCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsupplierCode.LostFocus
        Try
            If Trim(txtsupplierCode.Text) <> "" Then
                '  subGetSupplier()
                'subGetPurchaseBill()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'Private Sub subGetSupplier()
    '    Try
    '        Dim dsSup As New DataSet
    '        dsSup = objSupp.GetSupplierDetail(Trim(txtsupplierCode.Text))
    '        '  dsSup = objSupp.GetCustomerwithCode(txtsupplierCode.Text.Trim)
    '        If dsSup.Tables(0).Rows.Count > 0 Then
    '            txtSupplierName.Text = Trim(dsSup.Tables(0).Rows(0)("SupplierName") & "")
    '            txtsupplierCode.ReadOnly = True
    '        Else
    '            txtsupplierCode.ReadOnly = False
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    'End Sub

    Private Sub btnPanelOK_Click(sender As System.Object, e As System.EventArgs) Handles btnPanelOK.Click

    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub btnBillClear_Click(sender As System.Object, e As System.EventArgs) Handles btnBillClear.Click
        grdInvoice.RowCount = 0
        txtTradeInventoryCode.Text = ""
    End Sub

    Private Sub btnProceed_Click(sender As System.Object, e As System.EventArgs) Handles btnProceed.Click
        Dim sError As String
        '  If Not fncValidation() Then Exit Sub
        Dim iRet As Boolean
        Dim dsTH As New DataSet
        Dim i As Long
        Dim sSaveStatus As String
        For i = 0 To grdInvoice.RowCount - 1
            If fnGetdgvText(grdInvoice, i, 0) = True Then

                If optTransferOut.Checked Then
                    sSaveStatus = "TO"
                Else
                    sSaveStatus = "PU"
                End If
                iRet = objTII.UpdateTradeInTransferDetail(oTSalesConn, oTSalesRs, Trim(sSaveStatus), fnGetdgvText(grdInvoice, i, 1), fnGetdgvText(grdInvoice, i, 3), fnGetdgvText(grdInvoice, i, 4), sError)


                If Trim(sError) <> "" Then
                    MessageBox.Show("Error in save")
                    Exit Sub
                End If
                dsTH = Nothing
            End If
        Next

        'iRet = objTII.UpdateTradeInMergeInventory(oTSalesConn, oTSalesRs, Trim(txtTradeInventoryCode.Text), Val(txtTotalWt.Text.Trim), _
        '                                                   Val(cboPurity.Text), Val(txtPricePerGram.Text.Trim), Val(txtTotalPrice.Text.Trim), sUserID, sError)
        If Trim(sError) <> "" Then
            MessageBox.Show("Error in save")
            Exit Sub
        End If
        MessageBox.Show("Tradein Gold Issued Sucessfully")
        ' subPrint(txtTradeIssueNo.Text.Trim)
        objTII.UpdateTradeInInventory()
        '  subClearMe()
        btnBillClear_Click(sender, e)
    End Sub
End Class