Public Class frmInventoryMovement

    Private Sub cmdFetch_Click(sender As System.Object, e As System.EventArgs) Handles cmdFetch.Click
        Dim sSql As String
        Dim dr As DataRow()
        Dim i As Integer
        Dim sTransferType As String
        Try
            If Trim(txtInventoryCode.Text) = "" Then
                MessageBox.Show("Please enter the Inventory Code")
                If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
                Exit Sub
            End If


            'chkSelectAll.Checked = True
            fncFetchTransferDetails(txtInventoryCode.Text.Trim)

            If txtInventoryCode.Enabled Then txtInventoryCode.Focus()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
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
            sSql = sSql & " TotalCost,Status,RefNumber,RefDate from VwInventorymovement Where InventoryCode = " & fncFormatSQLText(sInventoryCode)
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
                MessageBox.Show("No data found in detail.")
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

    Private Sub cmdClear_Click(sender As System.Object, e As System.EventArgs) Handles cmdClear.Click
        txtInventoryCode.Text = ""
        dgvInventory.RowCount = 0
        If txtInventoryCode.CanFocus Then txtInventoryCode.Focus()
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub btnNew_Click(sender As System.Object, e As System.EventArgs)

    End Sub
End Class