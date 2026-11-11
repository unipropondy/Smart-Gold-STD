Public Class clsTransferOut
    Dim DM As New DataManager
    Public Function fncGetTransferNo() As String
        Dim sQry As String
        Dim Ds As New DataSet
        Dim sTransferNo As String
        Try
            sTransferNo = ""
            sQry = "EXEC sp_GetTransferNo "
            sQry = sQry & fncFormatSQLText(gthisLocation)
            Ds = DM.getDataSetWithSqlAdapter(sQry, "TransferNo", "TransferNo")
            If Ds.Tables("TransferNo").Rows.Count > 0 Then
                sTransferNo = Trim(Ds.Tables("TransferNo").Rows(0)("NextTransferNo") & "")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncGetTransferNo = sTransferNo
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function fncUpdateNextTransferNo() As Boolean
        Dim sQry As String
        Dim bStatus As Boolean
        Try
            sQry = " Update tblCompany Set NextTransferNo = NextTransferNo + 1  Where CompanyId = '1'"
            bStatus = fncExecNonQryTransfer(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateNextTransferNo = bStatus
            sQry = Nothing
        End Try
    End Function

    Public Function fncUpdateNextInvoiceNo() As Boolean
        Dim bStatus As Boolean
        Dim sQry As String
        Dim objsales As New ClsSales
        Try
            sQry = " update tblcompany set nextinvoice = nextinvoice + 1  "
            bStatus = fncExecNonQryTransfer(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateNextInvoiceNo = bStatus
            sQry = Nothing
        End Try
    End Function
    Public Function fncUpdateNextTransferInvoiceNo() As Boolean
        Dim bStatus As Boolean
        Dim sQry As String
        Dim objsales As New ClsSales
        Try
            sQry = " update tblcompany set nextTransferinvoiceNo = nextTransferinvoiceNo + 1  "
            bStatus = fncExecNonQryTransfer(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateNextTransferInvoiceNo = bStatus
            sQry = Nothing
        End Try
    End Function
    Public Function fncTransferHeaderSave(ByVal sTransferNo As String, ByVal sTransferDate As String, ByVal sFromLocation As String, _
                                       ByVal sToLocation As String, ByVal sRemarks As String, ByVal dTotalweight As Double, ByVal dTotalCost As Double, _
                                       ByVal dTotalPCS As Double, ByVal dTotalPMC As Double, ByVal dTotalSMC As Double, _
                                       ByVal sfromLocStatus As String, ByVal sToLocStatus As String, ByVal sUserID As String) As Boolean

        Dim sQry As String
        Dim bSaveStatus As Boolean
        Try
            sQry = "EXEC sp_TransferHeaderSave " & fncFormatSQLText(sTransferNo) & ","
            sQry = sQry & fncFormatSQLDate(sTransferDate) & ","
            sQry = sQry & fncFormatSQLText(Trim$(gthisLocation)) & ","
            sQry = sQry & sfromLocStatus & ","
            sQry = sQry & fncFormatSQLText(sToLocation) & ","
            sQry = sQry & sToLocStatus & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & Val(dTotalweight) & ","
            sQry = sQry & Val(dTotalPCS) & ","
            sQry = sQry & Val(dTotalPMC) & ","
            sQry = sQry & Val(dTotalSMC) & ","
            sQry = sQry & Val(dTotalCost) & ","
            sQry = sQry & fncFormatSQLText(sUserID)
            'bSaveStatus = DM.ProjExecNonQry(sQry)
            bSaveStatus = fncExecNonQryTransfer(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncTransferHeaderSave = bSaveStatus
            sQry = Nothing
        End Try
    End Function

    Public Function fncTransferDetailSave(ByVal sTransferNo As String, ByVal sInventorycode As String, ByVal sDescription As String, _
                                        ByVal sClassCode As String, ByVal sCategoryCode As String, ByVal sCatalogueCode As String, _
                                        ByVal dQtyOnHand As Decimal, ByVal dWeight As Decimal, ByVal dCostPerGram As Decimal, _
                                        ByVal dPMC As Decimal, ByVal dSMC As Decimal, ByVal dPCS As Decimal, ByVal dTotalCost As Decimal, ByVal nMarginPerc As Decimal, ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim bSaveStatus As Boolean
        Try
            sQry = "EXEC sp_TransferDetailSave " & fncFormatSQLText(sTransferNo) & ", "
            sQry = sQry & fncFormatSQLText(sInventorycode) & ", "
            sQry = sQry & fncFormatSQLText(sDescription) & ", "
            sQry = sQry & fncFormatSQLText(sClassCode) & ", "
            sQry = sQry & fncFormatSQLText(sCategoryCode) & ", "
            sQry = sQry & fncFormatSQLText(sCatalogueCode) & ", "
            sQry = sQry & Val(dPCS) & ", "
            sQry = sQry & Val(dWeight) & ", "
            sQry = sQry & Val(dCostPerGram) & ", "
            sQry = sQry & Val(dPMC) & ", "
            sQry = sQry & Val(dSMC) & ", "
            sQry = sQry & Val(dPCS) & ", "
            sQry = sQry & Val(dTotalCost) & ", "
            sQry = sQry & Val(nMarginPerc) & ", "
            sQry = sQry & fncFormatSQLText(sUserId)
            'bSaveStatus = DM.ProjExecNonQry(sQry)
            bSaveStatus = fncExecNonQryTransfer(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncTransferDetailSave = bSaveStatus
            sQry = Nothing
        End Try
    End Function

    Public Function fncupdateInventoryOnTransfer(ByVal sInventoryCode As String, _
        ByVal dQtyOnhand As Decimal, ByVal dWeight As Decimal, ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim bSaveStatus As Boolean
        Try
            sQry = "EXEC sp_UpdateInventoryTransfer "
            sQry = sQry & fncFormatSQLText(gthisLocation) & ", "
            sQry = sQry & fncFormatSQLText(sInventoryCode) & ", "
            sQry = sQry & Val(dWeight) & ", "
            sQry = sQry & Val(dQtyOnhand) & ", "
            sQry = sQry & fncFormatSQLText(sUserId)

            bSaveStatus = fncExecNonQryTransfer(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncupdateInventoryOnTransfer = bSaveStatus
        End Try
    End Function

    Public Function fncInventoryMovementSave(ByVal sLocationCode As String, ByVal sInventorycode As String, ByVal sDescription As String, ByVal sStatus As String, ByVal sRefNumber As String, _
                                        ByVal sRefDate As String, ByVal sQty As Integer, ByVal sWeight As Decimal, ByVal sSize As Decimal, ByVal sPMC As Decimal, ByVal sSMC As Decimal, _
                                       ByVal sCostPerGram As String, ByVal sTotalCost As String, _
                                      ByVal sSupplierCode As String, ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim bSaveStatus As Boolean
        Try


            sQry = ""
            sQry = sQry & "EXEC Sp_UpdateNewInventoryMovement " & fncFormatSQLText(sLocationCode) & ","
            sQry = sQry & fncFormatSQLText(sInventorycode) & ","
            sQry = sQry & fncFormatSQLText(sDescription) & ","
            sQry = sQry & fncFormatSQLText(sStatus) & ","
            sQry = sQry & fncFormatSQLText(sRefNumber) & ","
            sQry = sQry & fncFormatSQLDate(sRefDate) & ","
            sQry = sQry & Val(sQty) & ","
            sQry = sQry & Val(sWeight) & ","
            sQry = sQry & Val(sSize) & ","
            sQry = sQry & Val(sPMC) & ","
            sQry = sQry & Val(sSMC) & ","
            sQry = sQry & Val(sCostPerGram) & ","
            sQry = sQry & Val(sTotalCost) & ","
            sQry = sQry & fncFormatSQLText(sSupplierCode) & ","
            sQry = sQry & fncFormatSQLText(sUserId)
            bSaveStatus = fncExecNonQryTransfer(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        Finally
            fncInventoryMovementSave = bSaveStatus
        End Try
    End Function
    Public Function fncUpdateTransferOutToHQ(ByVal Ds As DataSet, ByVal Dsdetail As DataSet) As Boolean
        Dim iCnt As Integer
        Dim sQry As String
        Dim bStatus As Boolean
        Try
            For iCnt = 0 To Ds.Tables(0).Rows.Count - 1
                sQry = "Exec sp_TransferHeaderHQ " & fncFormatSQLText(Ds.Tables(0).Rows(iCnt)("TransferNo") & "") & ","
                sQry = sQry & fncFormatSQLDate(Ds.Tables(0).Rows(iCnt)("TransferDate") & "") & ","
                sQry = sQry & fncFormatSQLText(Ds.Tables(0).Rows(iCnt)("FromLocation") & "") & ","
                sQry = sQry & fncFormatSQLText(Trim(Ds.Tables(0).Rows(iCnt)("FromLocStatus")) & "") & ","
                sQry = sQry & fncFormatSQLText(Trim(Ds.Tables(0).Rows(iCnt)("ToLocation")) & "") & ","
                sQry = sQry & fncFormatSQLText(Ds.Tables(0).Rows(iCnt)("ToLocStatus") & "") & ","
                sQry = sQry & Val(Ds.Tables(0).Rows(iCnt)("TotalWeight") & "") & ","
                sQry = sQry & Val(Ds.Tables(0).Rows(iCnt)("TotalCost") & "") & ","
                sQry = sQry & Val(Ds.Tables(0).Rows(iCnt)("TotalPCS") & "") & ","
                sQry = sQry & Val(Ds.Tables(0).Rows(iCnt)("TotalPMC") & "") & ","
                sQry = sQry & Val(Ds.Tables(0).Rows(iCnt)("TotalSMC") & "") & ","
                sQry = sQry & fncFormatSQLText(Ds.Tables(0).Rows(iCnt)("Remarks") & "") & ","
                sQry = sQry & fncFormatSQLText(Ds.Tables(0).Rows(iCnt)("CreateUser") & "") & ","
                sQry = sQry & fncFormatSQLDate(Ds.Tables(0).Rows(iCnt)("createDate") & "") & ","
                sQry = sQry & fncFormatSQLText(Ds.Tables(0).Rows(iCnt)("ModifyUser") & "") & ","
                sQry = sQry & fncFormatSQLDate(Ds.Tables(0).Rows(iCnt)("ModifyDate") & "")
                '  sQry = sQry & " Where TransferNo = " & fncFormatSQLText((Ds.Tables(0).Rows(iCnt)("TransferNo") & "")) & "And  ToLocation <>" & fncFormatSQLText(gthisLocation)
                'DM.ProjExecNonQry(sQry, gsConRemote)
                bStatus = fncExecNonQryTransferHQ(sQry)
                If bStatus = False Then Exit Function
            Next iCnt
            If Dsdetail.Tables(0).Rows.Count > 0 Then
                For iCnt = 0 To Dsdetail.Tables(0).Rows.Count - 1
                    sQry = "Exec sp_TransferDetailToHQ " & fncFormatSQLText(Dsdetail.Tables(0).Rows(iCnt)("TransferNo") & "") & ","
                    sQry = sQry & fncFormatSQLText(Trim(Dsdetail.Tables(0).Rows(iCnt)("InventoryCode") & "")) & ","
                    sQry = sQry & fncFormatSQLText(Trim(Dsdetail.Tables(0).Rows(iCnt)("Description") & "")) & ","
                    sQry = sQry & Val(Dsdetail.Tables(0).Rows(iCnt)("QtyOnHand") & "") & ","
                    sQry = sQry & Val(Dsdetail.Tables(0).Rows(iCnt)("Weight") & "") & ","
                    sQry = sQry & Val(Dsdetail.Tables(0).Rows(iCnt)("CostPerGram") & "") & ","
                    sQry = sQry & Val(Dsdetail.Tables(0).Rows(iCnt)("PMC") & "") & ","
                    sQry = sQry & Val(Dsdetail.Tables(0).Rows(iCnt)("SMC") & "") & ","
                    sQry = sQry & Val(Dsdetail.Tables(0).Rows(iCnt)("PCS") & "") & ","
                    sQry = sQry & Val(Dsdetail.Tables(0).Rows(iCnt)("TotalCost") & "") & ","
                    sQry = sQry & fncFormatSQLText(Dsdetail.Tables(0).Rows(iCnt)("CreateUser") & "") & ","
                    sQry = sQry & fncFormatSQLDate(Dsdetail.Tables(0).Rows(iCnt)("CreateDate") & "") & ","
                    sQry = sQry & fncFormatSQLText(Dsdetail.Tables(0).Rows(iCnt)("ModifyUser") & "") & ","
                    sQry = sQry & fncFormatSQLDate(Dsdetail.Tables(0).Rows(iCnt)("ModifyDate") & "")
                    'sQry = sQry & fncFormatSQLDate((Dsdetail.Tables(0).Rows(iCnt)("TransferNo") & ""))
                    'DM.ProjExecNonQry(sQry, gsConRemote)
                    bStatus = fncExecNonQryTransferHQ(sQry)
                    If bStatus = False Then Exit Function
                Next iCnt
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateTransferOutToHQ = bStatus
            Ds = Nothing
            Dsdetail = Nothing
        End Try
    End Function

    Public Sub subOpenTransaferConnection()
        gsTransferCon.ConnectionString = gsCon
        gsTransferCon.Open()
    End Sub

    Public Sub subBeginTransaferTransaction()
        gsTransferTrans = gsTransferCon.BeginTransaction
    End Sub

    Public Sub subCommitTransaferTransaction()
        gsTransferTrans.Commit()
    End Sub

    Public Sub subRollBackTransaferTransaction()
        gsTransferTrans.Rollback()
    End Sub

    Public Sub subCloseTransaferConnection()
        gsTransferCon.Close()
    End Sub

    Public Function fncExecNonQryTransfer(ByVal Query As String) As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            Dim Cmd As New SqlClient.SqlCommand(Query, gsTransferCon)
            Cmd.Transaction = gsTransferTrans
            Cmd.ExecuteNonQuery()
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncExecNonQryTransfer = bStatus
        End Try
    End Function

    Public Sub subOpenTransaferConnectionHQ()
        gsTransferConHQ.ConnectionString = gsCon
        gsTransferConHQ.Open()
    End Sub

    Public Sub subBeginTransaferTransactionHQ()
        gsTransferTransHQ = gsTransferConHQ.BeginTransaction
    End Sub

    Public Sub subCommitTransaferTransactionHQ()
        gsTransferTransHQ.Commit()
    End Sub

    Public Sub subRollBackTransaferTransactionHQ()
        gsTransferTransHQ.Rollback()
    End Sub

    Public Sub subCloseTransaferConnectionHQ()
        gsTransferConHQ.Close()
    End Sub

    Public Function fncExecNonQryTransferHQ(ByVal Query As String) As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            Dim Cmd As New SqlClient.SqlCommand(Query, gsTransferConHQ)
            Cmd.Transaction = gsTransferTransHQ
            Cmd.ExecuteNonQuery()
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncExecNonQryTransferHQ = bStatus
        End Try
    End Function



    Public Function fncGetSalesTransferNo() As String
        Dim sQry As String
        Dim Ds As New DataSet
        Dim sTransferNo As String
        Try
            sTransferNo = ""
            sQry = "EXEC sp_GetSalesTransferNo "
            sQry = sQry & fncFormatSQLText(gthisLocation)
            Ds = DM.getDataSetWithSqlAdapter(sQry, "TransferNo", "TransferNo")
            If Ds.Tables("TransferNo").Rows.Count > 0 Then
                sTransferNo = Trim(Ds.Tables("TransferNo").Rows(0)("NextTransferNo") & "")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncGetSalesTransferNo = sTransferNo
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function fncSalesTransferHeaderSave(ByVal sTransferNo As String, ByVal sTransferDate As String, ByVal sFromLocation As String, _
                                       ByVal sToLocation As String, ByVal sRemarks As String, ByVal dTotalweight As Double, ByVal dTotalCost As Double, _
                                       ByVal dTotalPCS As Double, ByVal dTotalPMC As Double, ByVal dTotalSMC As Double, _
                                       ByVal sfromLocStatus As String, ByVal sToLocStatus As String, ByVal sUserID As String) As Boolean

        Dim sQry As String
        Dim bSaveStatus As Boolean
        Try
            sQry = "EXEC sp_SalesTransferHeaderSave " & fncFormatSQLText(sTransferNo) & ","
            sQry = sQry & fncFormatSQLDate(sTransferDate) & ","
            sQry = sQry & fncFormatSQLText(Trim$(gthisLocation)) & ","
            sQry = sQry & sfromLocStatus & ","
            sQry = sQry & fncFormatSQLText(sToLocation) & ","
            sQry = sQry & sToLocStatus & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & Val(dTotalweight) & ","
            sQry = sQry & Val(dTotalPCS) & ","
            sQry = sQry & Val(dTotalPMC) & ","
            sQry = sQry & Val(dTotalSMC) & ","
            sQry = sQry & Val(dTotalCost) & ","
            sQry = sQry & fncFormatSQLText(sUserID)
            'bSaveStatus = DM.ProjExecNonQry(sQry)
            bSaveStatus = fncExecNonQryTransfer(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncSalesTransferHeaderSave = bSaveStatus
            sQry = Nothing
        End Try
    End Function
    Public Function fncSalesTransferDetailSave(ByVal sTransferNo As String, ByVal sInventorycode As String, ByVal sDescription As String, _
                                        ByVal sClassCode As String, ByVal sCategoryCode As String, ByVal sCatalogueCode As String, _
                                        ByVal dQtyOnHand As Decimal, ByVal dWeight As Decimal, ByVal dCostPerGram As Decimal, _
                                        ByVal dPMC As Decimal, ByVal dSMC As Decimal, ByVal dPCS As Decimal, ByVal dTotalCost As Decimal, ByVal nMarginPerc As Decimal, ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim bSaveStatus As Boolean
        Try
            sQry = "EXEC sp_SalesTransferDetailSave " & fncFormatSQLText(sTransferNo) & ", "
            sQry = sQry & fncFormatSQLText(sInventorycode) & ", "
            sQry = sQry & fncFormatSQLText(sDescription) & ", "
            sQry = sQry & fncFormatSQLText(sClassCode) & ", "
            sQry = sQry & fncFormatSQLText(sCategoryCode) & ", "
            sQry = sQry & fncFormatSQLText(sCatalogueCode) & ", "
            sQry = sQry & Val(dPCS) & ", "
            sQry = sQry & Val(dWeight) & ", "
            sQry = sQry & Val(dCostPerGram) & ", "
            sQry = sQry & Val(dPMC) & ", "
            sQry = sQry & Val(dSMC) & ", "
            sQry = sQry & Val(dPCS) & ", "
            sQry = sQry & Val(dTotalCost) & ", "
            sQry = sQry & Val(nMarginPerc) & ", "
            sQry = sQry & fncFormatSQLText(sUserId)
            bSaveStatus = fncExecNonQryTransfer(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncSalesTransferDetailSave = bSaveStatus
            sQry = Nothing
        End Try
    End Function

    Public Function SalesHeaderSave(ByVal sInvoiceNo As String, ByVal sDate As String, _
                               ByVal sCompanyCode As String, ByVal sLocationCode As String, ByVal sTerminalCode As String, _
                                   ByVal sCustomerCode As String, ByVal sMembercode As String, ByVal sCustomerName As String, _
                                   ByVal sAddress1 As String, ByVal sAddress2 As String, _
                                   ByVal sAddress3 As String, _
                                   ByVal sSalesManCode As String, ByVal sSalesType As String, _
                                   ByVal nTotalWeight As Double, ByVal nTotalStoneWeight As Double, _
                                   ByVal nTotalGoldLoss As Double, ByVal nTotalSMC As Double, _
                                   ByVal nTotalPCS As Double, ByVal nGST As Double, _
                                   ByVal sGSTType As String, ByVal dDetailDiscount As Double, _
                                   ByVal dNetDiscount As Double, ByVal dDiscount As Double, _
                                   ByVal dNetTotal As Double, ByVal dPaidAmt As Double, ByVal dBalanceAmt As Double, _
                                   ByVal nTotalValue As Double, ByVal nTotalCost As Double, _
                                   ByVal dTradeInAmt As Double, ByVal dDepositAmt#, ByVal sInsAgreementNo$, _
                                   ByVal dCommission As Double, ByVal sRemarks As String, Optional ByVal sInvGstType As String = "") As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = ""
            sQry = sQry & " EXEC sp_SalesHeader " & fncFormatSQLText(sInvoiceNo) & ","
            sQry = sQry & fncFormatSQLDate(sDate) & ","
            sQry = sQry & fncFormatSQLText(sCompanyCode) & ","
            sQry = sQry & fncFormatSQLText(sLocationCode) & ","
            sQry = sQry & fncFormatSQLText(sTerminalCode) & ","
            sQry = sQry & fncFormatSQLText(sCustomerCode) & ","
            sQry = sQry & fncFormatSQLText(sMembercode) & ","
            sQry = sQry & fncFormatSQLText(sCustomerName) & ","
            sQry = sQry & fncFormatSQLText(sAddress1) & ","
            sQry = sQry & fncFormatSQLText(sAddress2) & ","
            sQry = sQry & fncFormatSQLText(sAddress3) & ","
            sQry = sQry & fncFormatSQLText(sSalesManCode) & ","
            sQry = sQry & fncFormatSQLText(sSalesType) & ","
            sQry = sQry & Val(nTotalWeight) & ","
            sQry = sQry & Val(nTotalStoneWeight) & ","
            sQry = sQry & Val(nTotalGoldLoss) & ","
            sQry = sQry & Val(nTotalSMC) & ","
            sQry = sQry & Val(nTotalPCS) & ","
            sQry = sQry & Val(nGST) & ","
            sQry = sQry & fncFormatSQLText(sGSTType) & ","
            sQry = sQry & Val(dDetailDiscount) & ","
            sQry = sQry & Val(dNetDiscount) & ","
            sQry = sQry & Val(dDiscount) & ","
            sQry = sQry & Val(dNetTotal) & ","
            sQry = sQry & Val(dPaidAmt) & ","
            sQry = sQry & Val(dBalanceAmt) & ","
            sQry = sQry & Val(nTotalValue) & ","
            sQry = sQry & Val(nTotalCost) & ","
            sQry = sQry & Val(dTradeInAmt) & ","
            sQry = sQry & Val(dDepositAmt) & ","
            sQry = sQry & fncFormatSQLText(sInsAgreementNo) & ","
            sQry = sQry & Val(dCommission) & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & fncFormatSQLText(sInvGstType) & ","
            sQry = sQry & fncFormatSQLText(sUserID)
            SaveStatus = fncExecNonQryTransfer(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            SalesHeaderSave = SaveStatus
            sQry = Nothing
        End Try
    End Function

    Public Function SalesDetailSave(ByVal sInvoiceNo As String, ByVal sinventorycode As String, _
                                    ByVal sDescription As String, ByVal sLocationCode As String, ByVal nWeight As Double, _
                                    ByVal nStoneWeight As Double, ByVal nGoldLoss As Double, _
                                    ByVal nRate As Double, ByVal NSMC As Double, _
                                    ByVal nPCS As Double, ByVal nDetailDiscount As Double, _
                                    ByVal dNetDiscount As Double, ByVal dGst As Double, _
                                    ByVal dNetAmount As Double, ByVal dActualSales As Double, _
                                    ByVal nTotalValue As Double, ByVal nTotalCost As Double, _
                                    ByVal dCommission As Double, ByVal sRemarks As String, ByVal dDailyClassRate As Double, ByVal iSortCode As Integer) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_SalesTransferDetail " & fncFormatSQLText(sInvoiceNo) & ","
            sQry = sQry & fncFormatSQLText(sinventorycode) & ","
            sQry = sQry & fncFormatSQLText(sDescription) & ","
            sQry = sQry & fncFormatSQLText(sLocationCode) & ","
            sQry = sQry & Val(nWeight) & ","
            sQry = sQry & Val(nStoneWeight) & ","
            sQry = sQry & Val(nGoldLoss) & ","
            sQry = sQry & Val(nRate) & ","
            sQry = sQry & Val(NSMC) & ","
            sQry = sQry & Val(nPCS) & ","
            sQry = sQry & Val(dGst) & ","
            sQry = sQry & Val(nDetailDiscount) & ","
            sQry = sQry & Val(dNetDiscount) & ","
            sQry = sQry & Val(dNetAmount) & ","
            sQry = sQry & Val(dActualSales) & ","
            sQry = sQry & Val(nTotalValue) & ","
            sQry = sQry & Val(nTotalCost) & ","
            sQry = sQry & Val(dCommission) & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & Val(dDailyClassRate) & ","
            sQry = sQry & Val(iSortCode) & ","
            sQry = sQry & fncFormatSQLText(sUserID) & "" & vbCrLf
            SaveStatus = fncExecNonQryTransfer(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            SalesDetailSave = SaveStatus
            sQry = Nothing
        End Try
    End Function

    Public Function fncGetTransferInventoryDetail() As DataSet
        Dim dsTD As New DataSet
        Dim sQry As String
        Try
            sQry = "select a.Inventorycode,b.CLassCOde,b.CategoryCode,b.CatalogueCode,b.Description,b.Qtyonhand,b.Weight,Costpergram,PMC,SMC,b.PCS,b.TotalCost from"
            sQry = sQry & " tblInventoryStock a,tblInventory b  where a.Inventorycode=b.Inventorycode and b.qtyonhand>0 and a.locationcode=" & fncFormatSQLText(gthisLocation)
            dsTD = DM.getDataSetWithSqlAdapter(sQry, "Inventory", "Inventory")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncGetTransferInventoryDetail = dsTD
            dsTD = Nothing
        End Try
    End Function

    Public Function fncGetTransferInventoryHeader() As DataSet
        Dim dsTD As New DataSet
        Dim sQry As String
        Try
            sQry = "select * from"
            sQry = sQry & " tblTradeInPurchase a,tblInventoryStock b  where a.Inventorycode=b.Inventorycode and b.qtyonhand>0  and b.locationcode=" & fncFormatSQLText(gthisLocation)


            'sQry = "select sum(b.Weight)Totalweight,sum(PMC)TotalPMC,sum(SMC)TotalSMC,sum(b.PCS)TotalPCS,sum(b.TotalCost)TotalCost from"
            'sQry = sQry & " tblInventoryStock a,tblInventory b  where a.Inventorycode=b.Inventorycode and b.qtyonhand>0  and a.locationcode=" & fncFormatSQLText(gthisLocation)


            dsTD = DM.getDataSetWithSqlAdapter(sQry, "Inventory", "Inventory")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncGetTransferInventoryHeader = dsTD
            dsTD = Nothing
        End Try
    End Function
    Public Function fncAutoTransferHeaderSave(ByVal drTH As DataSet, ByVal sTransferNo As String, ByVal sTransferDate As String, ByVal sLocationCode As String) As Boolean
        Dim sQry As String
        Dim iCnt As Integer
        Dim bStatus As Boolean
        Try
            For iCnt = 0 To drTH.Tables(0).Rows.Count - 1
                sQry = " Exec sp_TransferHeaderSave "
                sQry &= fncFormatSQLText(Trim(sTransferNo) & "") & ", "
                sQry &= fncFormatSQLText(Trim(sTransferDate) & "") & ", "
                sQry &= fncFormatSQLText(Trim(sLocationCode) & "") & ", "
                sQry &= fncFormatSQLText(Trim("2") & "") & ", "
                sQry &= fncFormatSQLText(Trim("HQ") & "") & ", "
                sQry &= fncFormatSQLText(Trim("0") & "") & ", "
                sQry &= fncFormatSQLText(Trim("") & "") & ", "
                sQry &= Val(Trim(drTH.Tables(0).Rows(iCnt)("Totalweight") & "")) & ", "
                sQry &= Val(Trim(drTH.Tables(0).Rows(iCnt)("TotalPCS") & "")) & ", "
                sQry &= Val(Trim(drTH.Tables(0).Rows(iCnt)("TotalPMC") & "")) & ", "
                sQry &= Val(Trim(drTH.Tables(0).Rows(iCnt)("TotalSMC") & "")) & ", "
                sQry &= Val(Trim(drTH.Tables(0).Rows(iCnt)("TotalCost") & "")) & ", "
                sQry &= fncFormatSQLText(sUserID)
                bStatus = fncExecNonQry(sQry)
                If bStatus = False Then Exit Function
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            drTH = Nothing
            sQry = Nothing
            fncAutoTransferHeaderSave = bStatus
        End Try
    End Function
    Public Function fncAutoTransferDetailSave(ByVal drTH As DataSet, ByVal STransferNo As String, ByVal sLocationCode As String, ByVal sUserId As String) As Boolean
        Dim iCnt As Integer
        Dim sQry As String
        Dim bSaveStatus As Boolean
        Try
            For iCnt = 0 To drTH.Tables(0).Rows.Count - 1
                sQry = "EXEC sp_TransferDetailSave " & fncFormatSQLText(STransferNo) & ", "
                sQry = sQry & fncFormatSQLText(Trim(drTH.Tables(0).Rows(iCnt)("INventoryCode") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(drTH.Tables(0).Rows(iCnt)("Description") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(drTH.Tables(0).Rows(iCnt)("ClassCode") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(drTH.Tables(0).Rows(iCnt)("CategoryCode") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(drTH.Tables(0).Rows(iCnt)("CatalogueCode") & "")) & ", "
                sQry = sQry & Val(Trim(drTH.Tables(0).Rows(iCnt)("PCS") & "")) & ", "
                sQry = sQry & Val(Trim(drTH.Tables(0).Rows(iCnt)("Weight") & "")) & ", "
                sQry = sQry & Val(Trim(drTH.Tables(0).Rows(iCnt)("CostPerGram") & "")) & ", "
                sQry = sQry & Val(Trim(drTH.Tables(0).Rows(iCnt)("PMC") & "")) & ", "
                sQry = sQry & Val(Trim(drTH.Tables(0).Rows(iCnt)("SMC") & "")) & ", "
                sQry = sQry & Val(Trim(drTH.Tables(0).Rows(iCnt)("PCS") & "")) & ", "
                sQry = sQry & Val(Trim(drTH.Tables(0).Rows(iCnt)("TotalCost") & "")) & ", "
                sQry = sQry & Val(0) & ", "
                sQry = sQry & fncFormatSQLText(sUserId)
                'bSaveStatus = DM.ProjExecNonQry(sQry)
                bSaveStatus = fncExecNonQryTransfer(sQry)
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncAutoTransferDetailSave = bSaveStatus
            sQry = Nothing
        End Try
    End Function
    Public Function fncExecNonQry(ByVal Query As String) As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            Dim Cmd As New SqlClient.SqlCommand(Query, gsUpdateCon)
            Cmd.Transaction = gsUpdateTrans
            Cmd.ExecuteNonQuery()
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncExecNonQry = bStatus
        End Try
    End Function

    Public Function GetUpdateTransferInvoice() As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " EXEC sp_GetUpdateTransferInvoice " & fncFormatSQLText(gthisLocation)
            Ds = DM.getDataSetWithSqlAdapter(sQry, "Invoice", "Invoice")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetUpdateTransferInvoice = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function UpdateTransfer(ByVal dsHeader As DataSet _
                        , ByVal dsDetail As DataSet _
                        , ByVal pMode As String _
                        , Optional ByVal pError As String = "") As String
        Dim sCmd As String
        Dim iRow As Integer
        Dim dsTranNo As New DataSet
        Try
            pError = ""
            'sType = "N"
            sCmd = "Exec sp_TransferHeaderSave "
            If pMode <> "I" Then
                sCmd = sCmd & fncFormatSQLText(Trim(dsHeader.Tables("Hdr").Rows(0).Item("TransferNo" & "")))
            Else
                sCmd = sCmd & fncFormatSQLText(Trim(dsHeader.Tables("Hdr").Rows(0).Item("TransferNo" & "")))
            End If
            sCmd = sCmd & ","
            sCmd = sCmd & fncFormatSQLDate(Trim(dsHeader.Tables("Hdr").Rows(0).Item("TransferDate" & "")))
            sCmd = sCmd & ","
            sCmd = sCmd & fncFormatSQLText(Trim(dsHeader.Tables("Hdr").Rows(0).Item("FromLocation" & "")))
            sCmd = sCmd & ","
            sCmd = sCmd & fncFormatSQLText(Trim(dsHeader.Tables("Hdr").Rows(0).Item("ToLocation" & "")))
            sCmd = sCmd & ","
            sCmd = sCmd & dsHeader.Tables("Hdr").Rows(iRow).Item("TotalWeight" & "")
            sCmd = sCmd & ","
            sCmd = sCmd & dsHeader.Tables("Hdr").Rows(iRow).Item("TotalPCS" & "")
            sCmd = sCmd & ","
            sCmd = sCmd & dsHeader.Tables("Hdr").Rows(iRow).Item("TotalCost" & "")
            sCmd = sCmd & ","
            sCmd = sCmd & dsHeader.Tables("Hdr").Rows(iRow).Item("TotalPMC" & "")
            sCmd = sCmd & ","
            sCmd = sCmd & dsHeader.Tables("Hdr").Rows(iRow).Item("TotalSMC" & "")
            sCmd = sCmd & ","
            sCmd = sCmd & dsHeader.Tables("Hdr").Rows(iRow).Item("FromLocStatus" & "")
            sCmd = sCmd & ","
            sCmd = sCmd & dsHeader.Tables("Hdr").Rows(iRow).Item("ToLocStatus" & "")
            sCmd = sCmd & ","
            sCmd = sCmd & fncFormatSQLText(Trim(dsHeader.Tables("Hdr").Rows(0).Item("CreateUser" & "")))
            sCmd = sCmd & ","
            sCmd = sCmd & fncFormatSQLText(Trim(dsHeader.Tables("Hdr").Rows(0).Item("ModifyUser" & "")))
            sCmd = sCmd & ","
            sCmd = sCmd & fncFormatSQLText(Trim(dsHeader.Tables("Hdr").Rows(0).Item("Remarks" & "")))
            sCmd = sCmd & ","
            sCmd = sCmd & fncFormatSQLText(Trim(dsHeader.Tables("Hdr").Rows(0).Item("SalesManCode" & "")))
            'sCmd = sCmd & fncFormatSQLText(pMode) ' I - Insert, U - Update, D - Delete, O - Unknown Option




            OpenConnection()
            BeginTransaction()

            dsTranNo = fncGetDatasetWithNonQuery(gsConnection, gsTrans, "TranNo", sCmd)
            If dsTranNo.Tables("TranNo").Rows.Count > 0 Then
                UpdateTransfer = dsTranNo.Tables("TranNo").Rows(0).Item("TransferNo" & "").ToString.Trim
            Else
                RollBackTransaction()
                CloseConnection()
                pError = "Error In Header"
                UpdateTransfer = ""
                Exit Function
            End If
            

            For iRow = 0 To dsDetail.Tables("Det").Rows.Count - 1
                sCmd = "Exec sp_TransferDetailSave "

                If pMode = "I" Then
                    sCmd = sCmd & fncFormatSQLText(Trim(UpdateTransfer))
                Else
                    sCmd = sCmd & fncFormatSQLText(Trim(dsHeader.Tables("Hdr").Rows(0).Item("TransferNo" & "")))
                End If
                sCmd = sCmd & ","
                sCmd = sCmd & fncFormatSQLText(dsDetail.Tables("Det").Rows(iRow).Item("InventoryCode" & ""))
                sCmd = sCmd & ","
                sCmd = sCmd & fncFormatSQLText(dsDetail.Tables("Det").Rows(iRow).Item("ClassCode" & ""))
                sCmd = sCmd & ","
                sCmd = sCmd & fncFormatSQLText(dsDetail.Tables("Det").Rows(iRow).Item("CategoryCode" & ""))
                sCmd = sCmd & ","
                sCmd = sCmd & fncFormatSQLText(dsDetail.Tables("Det").Rows(iRow).Item("CatalogueCode" & ""))
                sCmd = sCmd & ","
                sCmd = sCmd & fncFormatSQLText(dsDetail.Tables("Det").Rows(iRow).Item("Description" & ""))
                sCmd = sCmd & ","
                sCmd = sCmd & dsDetail.Tables("Det").Rows(iRow).Item("QtyonHand" & "")
                sCmd = sCmd & ","
                sCmd = sCmd & dsDetail.Tables("Det").Rows(iRow).Item("Weight" & "")
                sCmd = sCmd & ","
                sCmd = sCmd & dsDetail.Tables("Det").Rows(iRow).Item("SizeCode" & "")
                sCmd = sCmd & ","
                sCmd = sCmd & dsDetail.Tables("Det").Rows(iRow).Item("CostPerGram" & "")
                sCmd = sCmd & ","
                sCmd = sCmd & dsDetail.Tables("Det").Rows(iRow).Item("PMC" & "")
                sCmd = sCmd & ","
                sCmd = sCmd & dsDetail.Tables("Det").Rows(iRow).Item("SMC" & "")
                sCmd = sCmd & ","
                sCmd = sCmd & dsDetail.Tables("Det").Rows(iRow).Item("TotalCost" & "")
                sCmd = sCmd & ","
                sCmd = sCmd & (IIf(dsDetail.Tables("Det").Rows(iRow).Item("TransferTag") & "" = True, 1, 0))
                sCmd = sCmd & ","
                sCmd = sCmd & fncFormatSQLText(dsDetail.Tables("Det").Rows(iRow).Item("CreateUser" & ""))
                sCmd = sCmd & ","
                sCmd = sCmd & fncFormatSQLText(dsDetail.Tables("Det").Rows(iRow).Item("ModifyUser" & ""))
                sCmd = sCmd & ","
                sCmd = sCmd & fncFormatSQLText(pMode) ' I - Insert, U - Update, D - Delete, O - Unknown Option
                'SHabeer 20111111

                If Not fncExecuteNonQuery(sCmd, gsConnection, gsTrans) Then
                    pError = "Error In Detail"
                    UpdateTransfer = ""
                    RollBackTransaction()
                    CloseConnection()
                    Exit Function
                End If



            Next iRow


            CommitTransaction()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            RollBackTransaction()
            UpdateTransfer = ""
        Finally
            CloseConnection()
        End Try
    End Function
    Public Function fncGetTransferHeader(ByVal sConn As String, ByVal sDsName As String, _
                                         Optional ByVal sTranNo As String = "") As DataSet
        Dim DS As DataSet
        Dim sSql As String
        Try
            sSql = "Exec GetTransferHeader "
            sSql = sSql & fncFormatSQLText(sTranNo)
            DS = fncGetDatasetWithQuery(sConn, sDsName, sSql)
            fncGetTransferHeader = DS
            DS = Nothing
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            DS = Nothing
            fncGetTransferHeader = Nothing
        Finally
            DS = Nothing
        End Try
    End Function
    Public Function fncGetTransferDetail(ByVal sConn As String, ByVal sDsName As String, _
                                     Optional ByVal sTranNo As String = "") As DataSet
        Dim DS As DataSet
        Dim sSql As String
        Try
            sSql = "Exec GetTransferDetail "
            sSql = sSql & fncFormatSQLText(sTranNo)
            DS = fncGetDatasetWithQuery(sConn, sDsName, sSql)
            fncGetTransferDetail = DS
            DS = Nothing
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            DS = Nothing
            fncGetTransferDetail = Nothing
        Finally
            DS = Nothing
        End Try
    End Function

    Public Function GetScrapInventory() As DataSet
        Dim ds As New DataSet
        Dim sSql As String

        sSql = "SELECT * FROM vwInventoryStock WHERE Qtyonhand>0 and classcode='SCRAP'"
        sSql = sSql & " Order By a.InventoryCode"
        Try
            ds = DM.getDataSetWithSqlAdapter(sSql, "tblInventory", "tblInventory")
            GetScrapInventory = ds
            Exit Function
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
            ds = Nothing
        End Try
    End Function
End Class
