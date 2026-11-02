Public Class frmSalesHistory
    Dim objSales As New ClsSales
    Private Sub txtInvoiceNo_LostFocus(sender As Object, e As System.EventArgs) Handles txtInvoiceNo.LostFocus
        If (txtInvoiceNo.Text.Trim) <> "" Then
            subGetInvDetails()
        End If
    End Sub
    Private Sub subGetInvDetails()
        Dim dsInvDetail As New DataSet
        Dim sFileName As String
        '   Dim FSO As New Scripting.FileSystemObject
        Try
            Cursor = Cursors.WaitCursor

            If txtInvoiceNo.Text.Trim <> "" Then
                dsInvDetail = objSales.fncGetSalesHeaderHistoryByInvoiceNo(txtInvoiceNo.Text.Trim)
                If dsInvDetail.Tables.Count > 0 Then
                    If dsInvDetail.Tables(0).Rows.Count > 0 Then
                        txtInvoiceDate.Text = Trim(dsInvDetail.Tables(0).Rows(0)("InvoiceDate") & "")
                        txtLocationCode.Text = Trim(dsInvDetail.Tables(0).Rows(0)("LOcationCode") & "")
                        txtSalesType.Text = Trim(dsInvDetail.Tables(0).Rows(0)("SalesType") & "")
                        txtSalesManCode.Text = Trim(dsInvDetail.Tables(0).Rows(0)("SalesManCode") & "")
                        txtCustomerCode.Text = Trim(dsInvDetail.Tables(0).Rows(0)("CustomerCode") & "")
                        txtCustomerName.Text = Trim(dsInvDetail.Tables(0).Rows(0)("CustomerName") & "")
                        txtTotalWeight.Text = Trim(dsInvDetail.Tables(0).Rows(0)("TOtalWeight") & "")
                        txtTotStoneWeight.Text = Trim(dsInvDetail.Tables(0).Rows(0)("TOtalStoneWeight") & "")
                        txtTotalPCS.Text = Trim(dsInvDetail.Tables(0).Rows(0)("TOtalPCS") & "")
                        txtGST.Text = Trim(dsInvDetail.Tables(0).Rows(0)("GST") & "")
                        txtNetTotal.Text = Trim(dsInvDetail.Tables(0).Rows(0)("NetTotal") & "")
                        txtOrderInsNo.Text = Trim(dsInvDetail.Tables(0).Rows(0)("InsAgreementNo") & "")
                        txtTradeInAmt.Text = Trim(dsInvDetail.Tables(0).Rows(0)("TRadeInAmt") & "")
                        txtRemarks.Text = Trim(dsInvDetail.Tables(0).Rows(0)("Remarks") & "")

                    End If
                    fncFetchTransferDetails(txtInvoiceNo.Text.Trim)
                    fncFetchPaymentDetails(txtInvoiceNo.Text.Trim)
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Function fncFetchTransferDetails(ByVal sInvoiceNo As String) As Boolean
        Dim sSql As String
        Dim iCnt, j As Integer
        Dim bStatus As Boolean
        Dim drH As DataRow()
        Dim drD As DataRow()
        Dim drI As DataRow()
        Try
            bStatus = False


            dgvInventory.RowCount = 0
            sSql = "Select a.InventoryCode,a.Description,a.Weight,a.StoneWeight,a.GSt,"
            sSql = sSql & " a.NetAMount,b.TotalCost from tblSalesDetail a,tblInventory b Where a.InventoryCode=b.InventoryCode and  a.InvoiceNo = " & fncFormatSQLText(sInvoiceNo)
            sSql = sSql & " Order by a.modifyDate Desc"
            drD = DataManager.RowReturnProjExecNonQryWithDataRow(sSql)
            If drD.Length > 0 Then
                For iCnt = 0 To drD.Length - 1
                    dgvInventory.Rows.Add()

                    fnSetdgvText(dgvInventory, iCnt, 0, Trim(drD(iCnt)("InventoryCode") & ""))
                    fnSetdgvText(dgvInventory, iCnt, 1, Trim(drD(iCnt)("Description") & ""))

                    fnSetdgvText(dgvInventory, iCnt, 2, Trim(drD(iCnt)("Weight") & ""))
                    fnSetdgvText(dgvInventory, iCnt, 3, Trim(drD(iCnt)("StoneWeight") & ""))
                    fnSetdgvText(dgvInventory, iCnt, 4, Trim(drD(iCnt)("GST") & ""))
                    fnSetdgvText(dgvInventory, iCnt, 5, Trim(drD(iCnt)("NetAMount") & ""))
                    fnSetdgvText(dgvInventory, iCnt, 6, Trim(drD(iCnt)("TotalCost") & ""))
                    drI = Nothing
                Next iCnt
            Else
                MessageBox.Show("Inventory Not Found")
                txtInvoiceNo.Text = ""
                ' subClearDetails()
                If txtInvoiceNo.Enabled Then txtInvoiceNo.Focus()
                Exit Function
            End If
            '''   subCalcTotal()
            bStatus = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            drH = Nothing
            drI = Nothing
            drD = Nothing
        End Try
    End Function
    Private Function fncFetchPaymentDetails(ByVal sInvoiceNo As String) As Boolean
        Dim sSql As String
        Dim iCnt, j As Integer
        Dim bStatus As Boolean
        Dim drH As DataRow()
        Dim drD As DataRow()
        Dim drI As DataRow()
        Try
            bStatus = False


            dgvPaymode.RowCount = 0
            sSql = "Select PayMode,Amount,SlipNo from tblPayModeDetail Where InvoiceNo = " & fncFormatSQLText(sInvoiceNo)
            sSql = sSql & " Order by PayMode Desc"
            drD = DataManager.RowReturnProjExecNonQryWithDataRow(sSql)
            If drD.Length > 0 Then
                For iCnt = 0 To drD.Length - 1
                    dgvPaymode.Rows.Add()

                    fnSetdgvText(dgvPaymode, iCnt, 0, Trim(drD(iCnt)("PayMode") & ""))
                    fnSetdgvText(dgvPaymode, iCnt, 1, Trim(drD(iCnt)("Amount") & ""))
                    fnSetdgvText(dgvPaymode, iCnt, 2, Trim(drD(iCnt)("SlipNo") & ""))
                    drI = Nothing
                Next iCnt
            Else
                ' MessageBox.Show("Inventory Not Found")
                txtInvoiceNo.Text = ""
                ' subClearDetails()
                If txtInvoiceNo.Enabled Then txtInvoiceNo.Focus()
                Exit Function
            End If
            '''   subCalcTotal()
            bStatus = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            drH = Nothing
            drI = Nothing
            drD = Nothing
        End Try
    End Function
    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub btnBillClear_Click(sender As System.Object, e As System.EventArgs) Handles btnBillClear.Click
        txtInvoiceNo.Text = ""
        txtInvoiceDate.Text = ""
        txtSalesManCode.Text = ""
        txtSalesType.Text = ""
        txtGST.Text = ""
        txtNetTotal.Text = ""
        txtLocationCode.Text = ""
        txtCustomerCode.Text = ""
        txtCustomerName.Text = ""
        txtOrderInsNo.Text = ""
        txtTotalPCS.Text = ""
        txtTotalWeight.Text = ""
        txtTotStoneWeight.Text = ""
        txtTradeInAmt.Text = ""
        txtRemarks.Text = ""
        dgvInventory.RowCount = 0
        dgvPaymode.RowCount = 0
        If txtInvoiceNo.Enabled Then txtInvoiceNo.Focus()
    End Sub

    Private Sub frmSalesHistory_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F6
                btnBillClear_Click(sender, e)
            Case Keys.Escape
                btnClose_Click(sender, e)
            Case Keys.Up
                SendKeys.Send("+{Tab}")
            Case Keys.Down
                SendKeys.Send("{Tab}")
            Case Keys.Enter
                SendKeys.Send("{Tab}")
        End Select
    End Sub
End Class