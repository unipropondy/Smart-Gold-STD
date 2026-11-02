Public Class frmInventoryAJ

    Dim objClsInventorySearch As New ClsInventorySearch

    Private Sub btnRefresh_Click(sender As System.Object, e As System.EventArgs) Handles btnRefresh.Click
        subFetchInventoryDetails()
    End Sub

    Private Sub frmInventoryAJ_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F5
                If btnRefresh.Enabled Then btnRefresh_Click(sender, e)
            Case Keys.Escape
                btnClose_Click(sender, e)
            Case Keys.Return
                SendKeys.Send("{Tab}")
        End Select
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub subFetchInventoryDetails()
        Dim sQry As String
        Dim sCondition As String
        Dim dsInv As New DataSet
        Dim iCnt As Integer
        Try
            
            If Trim(txtinvCode.Text) <> "" Then
                subGetInvDetails()
            Else
                txtinvCode.Focus()
            End If
            dsInv = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub subClearDetails()
        Try
            txtinvCode.Text = ""
            txtInvDescription.Text = ""
            txtCategory.Text = ""
            txtCatalogue.Text = ""
            txtClass.Text = ""
            txtLocation.Text = ""
            txtWeight.Text = ""
            txtGoldLoss.Text = ""
            txtSizeCode.Text = ""
            txtPMC.Text = ""
            txtSMC.Text = ""
            txtCostPerGram.Text = ""
            txtTotalCost.Text = ""
            txtDiamondDescription.Text = ""
            txtStoneWeight.Text = ""
            txtRemarks.Text = ""
            txtPMC.Text = ""

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub subGetInvDetails()
        Dim dsInvDetail As New DataSet
        Dim sFileName As String
        Dim FSO As New Scripting.FileSystemObject
        Try
            Cursor = Cursors.WaitCursor

            If txtinvCode.Text.Trim <> "" Then
                dsInvDetail = objClsInventorySearch.GetInventoryDetails(txtinvCode.Text.Trim)
                If dsInvDetail.Tables.Count > 0 Then
                    If dsInvDetail.Tables(0).Rows.Count > 0 Then
                        txtInvDescription.Text = Trim(dsInvDetail.Tables(0).Rows(0)("Description") & "")
                        txtCategory.Text = Trim(dsInvDetail.Tables(0).Rows(0)("CategoryCode") & "")
                        txtCatalogue.Text = Trim(dsInvDetail.Tables(0).Rows(0)("CatalogueCode") & "")
                        txtClass.Text = Trim(dsInvDetail.Tables(0).Rows(0)("ClassCode") & "")
                        txtLocation.Text = Trim(dsInvDetail.Tables(0).Rows(0)("LocationCode") & "")
                        txtWeight.Text = Trim(dsInvDetail.Tables(0).Rows(0)("Weight") & "")
                        txtGoldLoss.Text = Trim(dsInvDetail.Tables(0).Rows(0)("GoldLoss") & "")
                        txtSizeCode.Text = Trim(dsInvDetail.Tables(0).Rows(0)("SizeCode") & "")
                        '   txtPMC.Text = Trim(dsInvDetail.Tables(0).Rows(0)("PMC") & "")
                        txtSMC.Text = Trim(dsInvDetail.Tables(0).Rows(0)("SMC") & "")
                        txtCostPerGram.Text = Trim(dsInvDetail.Tables(0).Rows(0)("CostPerGram") & "")
                        '  txtTotalCost.Text = Trim(dsInvDetail.Tables(0).Rows(0)("TotalCost") & "")
                        txtDiamondDescription.Text = Trim(dsInvDetail.Tables(0).Rows(0)("DiamondDescription") & "")
                        txtStoneWeight.Text = Trim(dsInvDetail.Tables(0).Rows(0)("StoneWeight") & "")
                        txtRemarks.Text = Trim(dsInvDetail.Tables(0).Rows(0)("Remarks") & "")
                        ' txtPMC.Text = Trim(dsInvDetail.Tables(0).Rows(0)("PMC") & "")
                        If Trim(sCompanyShortCode) = "MD" Then
                            txtPMC.Text = Format(Trim(dsInvDetail.Tables(0).Rows(0)("PMC") & "") + (Trim(dsInvDetail.Tables(0).Rows(0)("PMC") & "") * 0.1), "0.00")
                            txtTotalCost.Text = Format(Trim(dsInvDetail.Tables(0).Rows(0)("TotalCost") & "") + (Trim(dsInvDetail.Tables(0).Rows(0)("TotalCost") & "") * 0.1), "0.00")
                        Else
                            txtPMC.Text = Trim(dsInvDetail.Tables(0).Rows(0)("PMC") & "")
                            txtTotalCost.Text = Trim(dsInvDetail.Tables(0).Rows(0)("TotalCost") & "")
                        End If
                    End If
                    fncFetchTransferDetails(txtinvCode.Text.Trim)
                End If
                'code here for diplaying image
                sFileName = Application.StartupPath & "\Images\" & Trim(txtCatalogue.Text) & ".gif"
                If FSO.FileExists(sFileName) Then
                    PicCatalouge.Visible = True
                    PicCatalouge.Image = Image.FromFile(sFileName)
                Else
                    PicCatalouge.Visible = False
                End If
                FSO = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Function fncFetchTransferDetails(ByVal sInventoryCode As String) As Boolean
        Dim sSql As String
        Dim iCnt, j As Integer
        Dim bStatus As Boolean
        Dim drH As DataRow()
        Dim drD As DataRow()
        Dim drI As DataRow()
        Try
            bStatus = False


            dgvInventory.RowCount = 0
            sSql = "Select LocationCode,InventoryCode,Description,Weight,Size,CostPerGram,"
            sSql = sSql & " TotalCost,Status,RefNumber,RefDate from tblInventorymovement Where InventoryCode = " & fncFormatSQLText(sInventoryCode)
            sSql = sSql & " Order by modifyDate Desc"
            drD = DataManager.RowReturnProjExecNonQryWithDataRow(sSql)
            If drD.Length > 0 Then
                For iCnt = 0 To drD.Length - 1
                    dgvInventory.Rows.Add()

                    fnSetdgvText(dgvInventory, iCnt, 0, Trim(drD(iCnt)("InventoryCode") & ""))
                    fnSetdgvText(dgvInventory, iCnt, 1, Trim(drD(iCnt)("Description") & ""))

                    fnSetdgvText(dgvInventory, iCnt, 2, Trim(drD(iCnt)("Weight") & ""))
                    fnSetdgvText(dgvInventory, iCnt, 3, Trim(drD(iCnt)("Size") & ""))
                    fnSetdgvText(dgvInventory, iCnt, 4, Trim(drD(iCnt)("CostPerGram") & ""))
                    fnSetdgvText(dgvInventory, iCnt, 5, Trim(drD(iCnt)("TotalCost") & ""))
                    fnSetdgvText(dgvInventory, iCnt, 6, Trim(drD(iCnt)("LocationCode") & ""))
                    fnSetdgvText(dgvInventory, iCnt, 7, Trim(drD(iCnt)("Status") & ""))
                    fnSetdgvText(dgvInventory, iCnt, 8, Trim(drD(iCnt)("RefNumber") & ""))
                    fnSetdgvText(dgvInventory, iCnt, 9, Trim(drD(iCnt)("RefDate") & ""))

                    drI = Nothing
                Next iCnt
            Else
                MessageBox.Show("Inventory Not Found")
                txtinvCode.Text = ""
                subClearDetails()
                If txtinvCode.Enabled Then txtinvCode.Focus()
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

    Private Sub txtinvCode_LostFocus(sender As Object, e As System.EventArgs) Handles txtinvCode.LostFocus
        If (txtinvCode.Text.Trim) <> "" Then
            subGetInvDetails()
        End If
    End Sub
End Class