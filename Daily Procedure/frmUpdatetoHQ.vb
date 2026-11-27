Public Class frmUpdatetoHQ
    Dim DM As New DataManager

    Private Property Dda As Object
    Dim sTransferNo, sTransferDate, sDiamondDescription, sFromLocation, sToLocation, sRemarks, sFromLocStatus, sToLocStatus, sInventoryCode As String
    Dim dTotalweight, dTotalCost, dTotalPCS, dTotalPMC, dTotalSMC, dWeight, dCostPerGram, dPMC, dSMC, dPCS, dQtyOnHand, dQtySold, dTransferTag, dSizeCode As Double

    Private Sub frmUpdatetoHQ_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                SendKeys.Send("{Tab}")
            Case Keys.Up
                SendKeys.Send("+{Tab}")
            Case Keys.Down
                SendKeys.Send("{Tab}")
            Case Keys.Escape
                btnClose_Click(sender, e)
        End Select
    End Sub


    Public Sub frmUpdatetoHQ_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            txtFday.Text = Format(ServerDate, "dd")
            txtfMon.Text = Format(ServerDate, "MM")
            txtfyear.Text = Format(ServerDate, "yyyy")
            txtTday.Text = Format(ServerDate, "dd")
            txtTmon.Text = Format(ServerDate, "MM")
            txtTYear.Text = Format(ServerDate, "yyyy")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnUpdateMasters_Click(sender As System.Object, e As System.EventArgs) Handles btnUpdateMasters.Click
        Cursor.Current = Cursors.WaitCursor
        Dim sFromDate, sToDate As String
        Try
            If fncValidate() Then
                sFromDate = txtfyear.Text.Trim & txtfMon.Text.Trim & txtFday.Text.Trim
                sToDate = txtTYear.Text.Trim & txtTmon.Text.Trim & txtTday.Text.Trim
                subDownloadMasterFromHQ(sFromDate, sToDate)
                fncUpdateOrderDetails(sFromDate, sToDate)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub
    Private Function fncValidate() As Boolean
        Dim bStatus As Boolean
        Dim sFromDate, sToDate As String
        Try
            bStatus = False
            sFromDate = txtfyear.Text.Trim & txtfMon.Text.Trim & txtFday.Text.Trim
            sToDate = txtTYear.Text.Trim & txtTmon.Text.Trim & txtTday.Text.Trim
            If sFromDate = "" And sToDate = "" Then
                MessageBox.Show("Please enter date.")
                If txtFday.Enabled Then txtFday.Focus()
                Exit Function
            End If
            If sFromDate = "" Then
                MessageBox.Show("Please enter from date.")
                If txtFday.Enabled Then txtFday.Focus()
                Exit Function
            End If
            If Not IsDate(txtFday.Text.Trim & "/" & txtfMon.Text.Trim & "/" & txtfyear.Text.Trim) Then
                MessageBox.Show("Please enter valid from date.")
                If txtFday.Enabled Then txtFday.Focus()
                Exit Function
            End If
            If sToDate <> "" Then
                If Not IsDate(txtTday.Text.Trim & "/" & txtTmon.Text.Trim & "/" & txtTYear.Text.Trim) Then
                    MessageBox.Show("Please enter valid to date.")
                    If txtTday.Enabled Then txtTday.Focus()
                    Exit Function
                End If
            Else
                txtTday.Text = Format(ServerDate, "dd")
                txtTmon.Text = Format(ServerDate, "MM")
                txtTYear.Text = Format(ServerDate, "yyyy")
            End If
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncValidate = bStatus
        End Try
    End Function
    Private Sub subDownloadMasterFromHQ(ByVal sFromDate As String, ByVal sToDate As String)
        Dim dsMaster As New DataSet
        Dim iCnt As Integer
        Dim objUpdate As New clsUpdateFromHQ
        Try
            dsMaster = DM.getDataSetWithSqlAdapterAcc("Exec sp_GetMasterDetail " & fncFormatSQLText(sFromDate) & ", " & fncFormatSQLText(sToDate), "Master", "Master")
            If dsMaster Is Nothing Then
                MessageBox.Show("Error in fetching data from HQ.")
                Exit Sub
            End If
            If dsMaster.Tables.Count > 0 Then
                objUpdate.subOpenConnection()
                objUpdate.subBeginTransaction()
                'tblLocation
                'dont want to get the location info
                If dsMaster.Tables(0).Rows.Count > 0 Then
                    If Not objUpdate.fncUpdateLocationMaster(dsMaster.Tables(0).Select()) Then
                        objUpdate.subRollBackTransaction()
                        MessageBox.Show("Error in updating Location information")
                        Exit Sub
                    End If
                End If
                'tblClass
                If dsMaster.Tables(1).Rows.Count > 0 Then
                    If Not objUpdate.fncUpdateClassMaster(dsMaster.Tables(1).Select()) Then
                        objUpdate.subRollBackTransaction()
                        MessageBox.Show("Error in updating Class information")
                        Exit Sub
                    End If
                End If
                'tblCategory
                If dsMaster.Tables(2).Rows.Count > 0 Then
                    If Not objUpdate.fncUpdateCategoryMaster(dsMaster.Tables(2).Select()) Then
                        objUpdate.subRollBackTransaction()
                        MessageBox.Show("Error in updating Category information")
                        Exit Sub
                    End If
                End If
                'tblCatalogue 
                If dsMaster.Tables(3).Rows.Count > 0 Then
                    If Not objUpdate.fncUpdateCatalogueMaster(dsMaster.Tables(3).Select()) Then
                        objUpdate.subRollBackTransaction()
                        MessageBox.Show("Error in updating Catalogue information")
                        Exit Sub
                    End If
                End If
                'tblSalesMan
                If dsMaster.Tables(5).Rows.Count > 0 Then
                    If Not objUpdate.fncUpdateSalesManMaster(dsMaster.Tables(5).Select()) Then
                        objUpdate.subRollBackTransaction()
                        MessageBox.Show("Error in updating SalesMan information")
                        Exit Sub
                    End If
                End If
                'tblSupplier
                If dsMaster.Tables(6).Rows.Count > 0 Then
                    If Not objUpdate.fncUpdateSupplierMaster(dsMaster.Tables(6).Select()) Then
                        objUpdate.subRollBackTransaction()
                        MessageBox.Show("Error in updating Supplier information")
                        Exit Sub
                    End If
                End If
                'tblCurrency
                If dsMaster.Tables(7).Rows.Count > 0 Then
                    If Not objUpdate.fncUpdateCurrencyMaster(dsMaster.Tables(7).Select()) Then
                        objUpdate.subRollBackTransaction()
                        MessageBox.Show("Error in updating Currency information")
                        Exit Sub
                    End If
                End If
                'tblPaymode
                If dsMaster.Tables(8).Rows.Count > 0 Then
                    If Not objUpdate.fncUpdatePaymodeMaster(dsMaster.Tables(8).Select()) Then
                        objUpdate.subRollBackTransaction()
                        MessageBox.Show("Error in updating Paymode information")
                        Exit Sub
                    End If
                End If
                'tblTrayMaster
                If dsMaster.Tables(9).Rows.Count > 0 Then
                    If Not objUpdate.fncUpdateTrayMaster(dsMaster.Tables(9).Select()) Then
                        objUpdate.subRollBackTransaction()
                        MessageBox.Show("Error in updating Tray information")
                        Exit Sub
                    End If
                End If
                'tblClassRate
                'If dsMaster.Tables(10).Rows.Count > 0 Then
                '    If Not objUpdate.fncUpdateClassRate(dsMaster.Tables(10).Select()) Then
                '        objUpdate.subRollBackTransaction()
                '        MessageBox.Show("Error in updating Tray information")
                '        Exit Sub
                '    End If
                'End If
                'tblPayOutMaster
                If dsMaster.Tables(11).Rows.Count > 0 Then
                    If Not objUpdate.fncUpdatePayOutMaster(dsMaster.Tables(11).Select()) Then
                        objUpdate.subRollBackTransaction()
                        MessageBox.Show("Error in updating PayOut information")
                        Exit Sub
                    End If
                End If
                'tblSize
                If dsMaster.Tables(12).Rows.Count > 0 Then
                    If Not objUpdate.fncUpdateSizeMaster(dsMaster.Tables(12).Select()) Then
                        objUpdate.subRollBackTransaction()
                        MessageBox.Show("Error in updating Size information")
                        Exit Sub
                    End If
                End If
                'tblUserMaster
                If dsMaster.Tables(13).Rows.Count > 0 Then
                    If Not objUpdate.fncUpdateUserMaster(dsMaster.Tables(13).Select()) Then
                        objUpdate.subRollBackTransaction()
                        MessageBox.Show("Error in updating User information")
                        Exit Sub
                    End If
                End If
                'tblUserGroup
                If dsMaster.Tables(14).Rows.Count > 0 Then
                    If Not objUpdate.fncUpdateUserGroupMaster(dsMaster.Tables(14).Select()) Then
                        objUpdate.subRollBackTransaction()
                        MessageBox.Show("Error in updating Paymode information")
                        Exit Sub
                    End If
                End If
                objUpdate.subCommitTransaction()
                MsgBox(" Download Master Data Successfuly!!! ")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            objUpdate.subCloseConnection()
            dsMaster = Nothing
            objUpdate = Nothing
        End Try
    End Sub
    Private Sub subDownloadSecurityMasterFromHQ(ByVal sFromDate As String, ByVal sToDate As String)
        Dim dsMaster As New DataSet
        Dim iCnt As Integer
        Dim objUpdate As New clsUpdateFromHQ
        Try
            dsMaster = DM.getDataSetWithSqlAdapterAcc("Exec sp_GetSecurityMasterDetail " & fncFormatSQLText(sFromDate) & ", " & fncFormatSQLText(sToDate), "Master", "Master")
            If dsMaster Is Nothing Then
                MessageBox.Show("Error in fetching data from HQ.")
                Exit Sub
            End If
            If dsMaster.Tables.Count > 0 Then
                objUpdate.subOpenConnection()
                objUpdate.subBeginTransaction()
                'tblLocation

                'tblUserMaster
                If dsMaster.Tables(0).Rows.Count > 0 Then
                    If Not objUpdate.fncUpdateUserMaster(dsMaster.Tables(0).Select()) Then
                        objUpdate.subRollBackTransaction()
                        MessageBox.Show("Error in updating User information")
                        Exit Sub
                    End If
                End If
                'tblUserGroup
                If dsMaster.Tables(1).Rows.Count > 0 Then
                    If Not objUpdate.fncUpdateUserGroupMaster(dsMaster.Tables(1).Select()) Then
                        objUpdate.subRollBackTransaction()
                        MessageBox.Show("Error in updating User Group Information")
                        Exit Sub
                    End If
                End If
                'tblUserPermission
                If dsMaster.Tables(2).Rows.Count > 0 Then
                    If Not objUpdate.fncUpdateUserPermissionMaster(dsMaster.Tables(2).Select()) Then
                        objUpdate.subRollBackTransaction()
                        MessageBox.Show("Error in updating User Permission Information")
                        Exit Sub
                    End If
                End If
                objUpdate.subCommitTransaction()
                MsgBox(" Download Master Data Successfuly!!! ")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            objUpdate.subCloseConnection()
            dsMaster = Nothing
            objUpdate = Nothing
        End Try
    End Sub

    Public Sub btnUpdateTransfer_Click(sender As System.Object, e As System.EventArgs) Handles btnUpdateTransfer.Click
        Cursor.Current = Cursors.WaitCursor
        Dim sFromDate, sToDate As String
        Try
            If fncValidate() Then
                sFromDate = txtfyear.Text.Trim & txtfMon.Text.Trim & txtFday.Text.Trim
                sToDate = txtTYear.Text.Trim & txtTmon.Text.Trim & txtTday.Text.Trim
                subUpdateRefershTransfer(sFromDate, sToDate)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub
    Private Function fncTransferInventoryToLocal(ByVal sTransNo As String) As Boolean
        Dim sQry As String
        Dim drH As DataRow()
        Dim drD As DataRow()
        Dim bStatus As Boolean
        Dim iCnt As Integer
        Dim objTransferIn As New ClsTransferIn
        Try
            bStatus = False
            'fetch header info
            If Trim(gthisLocationType) = "HQ" Then
                sQry = "select * from tblInventoryTransferHeader where TransferNo=" & fncFormatSQLText(sTransNo)
                'sQry &= " and ToLocation = " & fncFormatSQLText(gthisLocation)
                drH = DataManager.RowReturnProjExecNonQryWithDataRow(sQry)
            Else
                sQry = "select * from tblInventoryTransferHeader where TransferNo=" & fncFormatSQLText(sTransNo)
                sQry &= " and ToLocation = " & fncFormatSQLText(gthisLocation)
                drH = DataManager.RowReturnProjExecNonQryWithDataRowAcc(sQry)
            End If
            'fetch detail info
            sQry = "select * from  tblInventoryTransferDetail where TransferNo=" & fncFormatSQLText(sTransNo)
            drD = DataManager.RowReturnProjExecNonQryWithDataRowAcc(sQry)

            objTransferIn.subOpenTransaferConnection()
            objTransferIn.subBeginTransaferTransaction()
            If drH.Length > 0 Then
                sTransferNo = Trim(drH(0)("TransferNo") & "")
                sTransferDate = Trim(drH(0)("TransferDate") & "")
                sFromLocation = Trim(drH(0)("FromLocation") & "")
                sToLocation = Trim(drH(0)("ToLocation") & "")
                sRemarks = Trim(drH(0)("Remarks") & "")
                dTotalweight = Trim(drH(0)("Totalweight") & "")
                dTotalCost = Trim(drH(0)("TotalCost") & "")
                dTotalPCS = Trim(drH(0)("TotalPCS") & "")
                dTotalPMC = Trim(drH(0)("TotalPMC") & "")
                dTotalSMC = Trim(drH(0)("TotalSMC") & "")
                sFromLocStatus = Trim(drH(0)("FromLocStatus") & "")
                sToLocStatus = Trim(drH(0)("ToLocStatus") & "")
                If objTransferIn.fncTransferHeadertoLocal(sTransferNo, sTransferDate, sFromLocation, sFromLocStatus, _
                                        sToLocation, sToLocStatus, sRemarks, dTotalweight, dTotalCost, _
                                        dTotalPCS, dTotalPMC, dTotalSMC, _
                                        sUserID) = False Then
                    MessageBox.Show("Error in fetching header information.")
                    objTransferIn.subRollBackTransaferTransaction()
                    Exit Function
                End If


                If (drD.Length > 0) Then
                    For iCnt = 0 To drD.Length - 1
                        sTransferNo = Trim(drD(iCnt)("TransferNo") & "")
                        sInventoryCode = Trim(drD(iCnt)("InventoryCode") & "")
                        sDescription = Trim(drD(iCnt)("Description") & "")
                        dQtyOnHand = Val(drD(iCnt)("QtyOnHand") & "")
                        dWeight = Val(drD(iCnt)("Weight") & "")
                        dSizeCode = Val(drD(iCnt)("SizeCode") & "")
                        dCostPerGram = Val(drD(iCnt)("CostPerGram") & "")
                        dPMC = Val(drD(iCnt)("PMC") & "")
                        dSMC = Val(drD(iCnt)("SMC") & "")
                        dPCS = Val(drD(iCnt)("PCS") & "")
                        dTotalCost = Val(drD(iCnt)("TotalCost") & "")
                        dTransferTag = (IIf(Trim(drD(iCnt)("TransferTag") & "") & "" = True, 1, 0))
                        If objTransferIn.fncTransferDetailtoLocal(sTransferNo, sInventoryCode, sDescription, _
                                 Trim(drD(iCnt)("ClassCode") & ""), Trim(drD(iCnt)("CategoryCode") & ""), Trim(drD(iCnt)("CatalogueCode") & ""), _
                                dQtyOnHand, dWeight, dSizeCode, dCostPerGram, dPMC, dSMC, dPCS, dTotalCost, dTransferTag, sUserID) = False Then
                            MessageBox.Show("Error in fetching detail information.")
                            objTransferIn.subRollBackTransaferTransaction()
                            Exit Function
                        End If
                    Next
                End If
                objTransferIn.subCommitTransaferTransaction()
                bStatus = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            objTransferIn.subCloseTransaferConnection()
            objTransferIn = Nothing
            fncTransferInventoryToLocal = bStatus
        End Try
    End Function
    Private Sub subUpdateRefershTransfer(ByVal sFromDate As String, ByVal sToDate As String)
        Dim dsTDLocal As New DataSet
        Dim dsTDHQ As New DataSet
        Dim dsOTDLocal As New DataSet
        Dim objUpdate As New clsUpdateFromHQ
        Try
            objUpdate.subOpenConnection()
            objUpdate.subOpenConnectionHQ()
            objUpdate.subBeginTransaction()
            objUpdate.subBeginTransactionHQ()
            dsTDLocal = objUpdate.fncGetTransferDetailFromLocal(sFromDate, sToDate)
            If dsTDLocal Is Nothing Then
                MessageBox.Show("Error in fetcheing transfer detail from local")
                Exit Sub
            End If
            dsTDHQ = objUpdate.fncGetTransferDetailFromHQ(sFromDate, sToDate)
            If dsTDHQ Is Nothing Then
                MessageBox.Show("Error in fetcheing transfer detail from HQ")
                Exit Sub
            End If
            dsOTDLocal = objUpdate.fncGetOnlineTransferDetailFromLocal(sFromDate, sToDate)
            If dsOTDLocal Is Nothing Then
                MessageBox.Show("Error in fetcheing transfer detail from local")
                Exit Sub
            End If
            If dsTDHQ.Tables(0).Rows.Count > 0 Then
                If Not objUpdate.fncUpdateTransferDetailLocal(dsTDHQ.Tables(0).Select()) Then
                    objUpdate.subRollBackTransaction()
                    objUpdate.subRollBackTransactionHQ()
                    MessageBox.Show("Error in updating transfer detail in local")
                    Exit Sub
                End If
            End If
            If dsTDLocal.Tables(0).Rows.Count > 0 And dsTDLocal.Tables(1).Rows.Count > 0 Then
                If Not fncUpdateInventoryDetails(sFromDate, sToDate) Then
                    objUpdate.subRollBackTransaction()
                    objUpdate.subRollBackTransactionHQ()
                    MessageBox.Show("Error in updating Inventory in HQ")
                    Exit Sub
                End If
                'If Not objUpdate.fncUpdateTransferDetailLocalHQ(dsTDLocal) Then
                '    objUpdate.subRollBackTransaction()
                '    objUpdate.subRollBackTransactionHQ()
                '    MessageBox.Show("Error in updating transfer detail in HQ")
                '    Exit Sub
                'End If
                If Not objUpdate.fncUpdateTransferDetailHQ(dsTDLocal) Then
                    objUpdate.subRollBackTransaction()
                    objUpdate.subRollBackTransactionHQ()
                    MessageBox.Show("Error in updating transfer detail in HQ")
                    Exit Sub
                End If
            End If
            If dsOTDLocal.Tables(0).Rows.Count > 0 Then
                If Not objUpdate.fncUpdateOnlineTransferDetailHQ(dsOTDLocal) Then
                    objUpdate.subRollBackTransaction()
                    objUpdate.subRollBackTransactionHQ()
                    MessageBox.Show("Error in updating transfer detail in local")
                    Exit Sub
                End If
            End If
            objUpdate.subCommitTransaction()
            objUpdate.subCommitTransactionHQ()
            MessageBox.Show("Transfer detail updated successfully")
        Catch ex As Exception
            objUpdate.subRollBackTransaction()
            objUpdate.subRollBackTransactionHQ()
            MessageBox.Show(ex.Message)
        Finally
            objUpdate.subCloseConnection()
            objUpdate.subCloseConnectionHQ()
            dsTDLocal = Nothing
            dsTDHQ = Nothing
            objUpdate = Nothing
        End Try
    End Sub
    Private Function fncUpdateInventoryDetails(ByVal sFromDate, ByVal sToDate) As Boolean
        Dim sSelect, sInventoryCodes As String
        Dim dsInvDetail As New DataSet
        Dim objEodToHQ As New clsUpdateFromHQ
        Dim bStatus As Boolean
        Try
            '        If fncDatevalidation(sFromDate, sToDate) Then
            sSelect = "Select * from tblInventory With (Nolock) Where InventoryCode in (Select InventoryCode from TblInventoryTransferDetail  With (Nolock)  Where TransferNo In (Select TransferNo from TblInventoryTransferHeader  With (Nolock)  Where Convert(Char(8), ModifyDate, 112) Between " & fncFormatSQLText(sFromDate) & " And " & fncFormatSQLText(sToDate) & "))" & vbCrLf
            sSelect &= "Select * from tblInventoryStock  With (Nolock)  Where InventoryCode in (Select InventoryCode from TblInventoryTransferDetail  With (Nolock)  Where TransferNo In (Select TransferNo from TblInventoryTransferHeader  With (Nolock)  Where Convert(Char(8), ModifyDate, 112) Between " & fncFormatSQLText(sFromDate) & " And " & fncFormatSQLText(sToDate) & "))" & vbCrLf
            dsInvDetail = DM.getDataSetWithSqlAdapter(sSelect, "Inv", "Inv")
            If dsInvDetail.Tables(0).Rows.Count > 0 Or dsInvDetail.Tables(1).Rows.Count > 0 Then
                bStatus = objEodToHQ.fncUpdateInventoryStockToHQ(dsInvDetail)
            Else
                bStatus = True
            End If

            '       End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateInventoryDetails = bStatus
        End Try
    End Function

    Private Sub btnUpdateSecurity_Click(sender As System.Object, e As System.EventArgs) Handles btnUpdateSecurity.Click
        Cursor.Current = Cursors.WaitCursor
        Dim sFromDate, sToDate As String
        Try
            If fncValidate() Then
                'sFromDate = txtFday.Text.Trim & txtfMon.Text.Trim & txtfyear.Text.Trim
                'sToDate = txtTday.Text.Trim & txtTmon.Text.Trim & txtTYear.Text.Trim
                sFromDate = txtfyear.Text.Trim & txtfMon.Text.Trim & txtFday.Text.Trim
                sToDate = txtTYear.Text.Trim & txtTmon.Text.Trim & txtTday.Text.Trim
                subDownloadSecurityMasterFromHQ(sFromDate, sToDate)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub
    Private Function fncUpdateOrderDetails(ByVal sLDate, ByVal sDate) As Boolean
        Dim sSelect As String
        Dim objEodToHq As New ClsEOD
        Dim dsSales As New DataSet
        Dim dsOrder As New DataSet
        Try
           

            sSelect = "Select b.OrderNo,b.OrderInventoryCode,b.InventoryCode,b.Description,b.WeightEstimated,b.EstimatedCost,a.TotalCostEstimated,a.TotalGSTEstimated From tblinventoryOrderHeader a with(nolock),tblinventoryOrderDetail b with(nolock) Where a.OrderNo=b.OrderNo and InventoryCode<>'' and Locationcode=" & fncFormatSQLText(gthisLocation) & "  and Convert(char(8), b.ModifiedDate, 112) >= " & fncFormatSQLText(sLDate) & vbCrLf
            dsOrder = DM.getDataSetWithSqlAdapterAcc(sSelect, "Order", "Order")
            If Not dsOrder Is Nothing Then
                If dsOrder.Tables(0).Rows.Count > 0 Then
                    If objEodToHq.fncUpdateOrderInventoryDetailLocal(dsOrder) Then
                        fncUpdateOrderDetails = True
                    End If
                Else
                    fncUpdateOrderDetails = True
                End If
            End If


            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsSales = Nothing
        End Try
    End Function
    Private Sub btnGetNewStock_Click(sender As System.Object, e As System.EventArgs) Handles btnGetNewStock.Click

    End Sub
End Class