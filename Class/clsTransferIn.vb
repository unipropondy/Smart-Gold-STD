Imports System
Imports System.IO
Public Class ClsTransferIn
    Dim DM As DataManager
    'Public Function TransferHeadertoLocal(ByVal sTransNo As String, ByVal sFromLocation As String) As Boolean
    '    Dim sQry As String
    '    Dim SaveStatus As Boolean
    '    Try

    '        sQry = ""
    '        sQry = sQry & "EXEC sp_TransferHeaderToLocal"
    '        sQry = sQry & fncFormatSQLText(sTransNo) & ","
    '        sQry = sQry & fncFormatSQLText(sFromLocation)
    '        SaveStatus = DM.ProjExecNonQry(sQry, gsConRemote)
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '        SaveStatus = False

    '    Finally
    '        TransferHeadertoLocal = SaveStatus
    '        sQry = Nothing
    '    End Try

    'End Function

    'mm
    Public Function fncTransferHeadertoLocal(ByVal sTransferNo As String, ByVal sTransferDate As String, ByVal sFromLocation As String, _
                                       ByVal sfromLocStatus As String, ByVal sToLocation As String, ByVal sToLocStatus As String, _
                                       ByVal sRemarks As String, ByVal dTotalweight As Double, ByVal dTotalCost As Double, _
                                       ByVal dTotalPCS As Double, ByVal dTotalPMC As Double, ByVal dTotalSMC As Double, _
                                      ByVal sUserID As String) As Boolean
        Dim sQry As String
        Dim bSaveStatus As Boolean
        Try
            bSaveStatus = False
            sQry = sQry & "EXEC sp_LocalTransferHeaderSave " & fncFormatSQLText(sTransferNo) & ", "
            sQry = sQry & fncFormatSQLDate(sTransferDate) & ", "
            sQry = sQry & fncFormatSQLText(sFromLocation) & ", "
            sQry = sQry & fncFormatSQLText(sfromLocStatus) & ", "
            sQry = sQry & fncFormatSQLText(sToLocation) & ", "
            sQry = sQry & fncFormatSQLText(sToLocStatus) & ", "
            sQry = sQry & Val(dTotalweight) & ", "
            sQry = sQry & Val(dTotalCost) & ", "
            sQry = sQry & Val(dTotalPCS) & ", "
            sQry = sQry & Val(dTotalPMC) & ", "
            sQry = sQry & Val(dTotalSMC) & ", "
            sQry = sQry & fncFormatSQLText(sRemarks) & ", "
            sQry = sQry & fncFormatSQLText(sUserID)
            bSaveStatus = fncExecNonQryTransfer(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncTransferHeadertoLocal = bSaveStatus
            sQry = Nothing
        End Try
    End Function

    Public Function fncSalesTransferHeadertoLocal(ByVal sTransferNo As String, ByVal sTransferDate As String, ByVal sFromLocation As String, _
                                       ByVal sfromLocStatus As String, ByVal sToLocation As String, ByVal sToLocStatus As String, _
                                       ByVal sRemarks As String, ByVal dTotalweight As Double, ByVal dTotalCost As Double, _
                                       ByVal dTotalPCS As Double, ByVal dTotalPMC As Double, ByVal dTotalSMC As Double, _
                                      ByVal sUserID As String) As Boolean
        Dim sQry As String
        Dim bSaveStatus As Boolean
        Try
            bSaveStatus = False
            sQry = sQry & "EXEC sp_SalesTransferHeaderToLocal " & fncFormatSQLText(sTransferNo) & ", "
            sQry = sQry & fncFormatSQLDate(sTransferDate) & ", "
            sQry = sQry & fncFormatSQLText(sFromLocation) & ", "
            sQry = sQry & fncFormatSQLText(sfromLocStatus) & ", "
            sQry = sQry & fncFormatSQLText(sToLocation) & ", "
            sQry = sQry & fncFormatSQLText(sToLocStatus) & ", "
            sQry = sQry & Val(dTotalweight) & ", "
            sQry = sQry & Val(dTotalCost) & ", "
            sQry = sQry & Val(dTotalPCS) & ", "
            sQry = sQry & Val(dTotalPMC) & ", "
            sQry = sQry & Val(dTotalSMC) & ", "
            sQry = sQry & fncFormatSQLText(sRemarks) & ", "
            sQry = sQry & fncFormatSQLText(sUserID)
            bSaveStatus = fncExecNonQryTransfer(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncSalesTransferHeadertoLocal = bSaveStatus
            sQry = Nothing
        End Try
    End Function

    Public Function fncTransferDetailtoLocal(ByVal sTransferNo As String, ByVal sInventoryCode As String, _
                    ByVal sDescription As String, ByVal sClassCode As String, ByVal sCategoryCode As String, _
                    ByVal sCatalogueCode As String, ByVal dQtyOnHand As Decimal, ByVal dWeight As Decimal, ByVal dSize As Decimal, _
                    ByVal dCostPerGram As Decimal, ByVal dPMC As Decimal, ByVal dSMC As Decimal, _
                    ByVal dPCS As Decimal, ByVal dTotalCost As Decimal, ByVal dTransferTag As Boolean, ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim bSaveStatus As Boolean
        Try
            bSaveStatus = False
            sQry = sQry & "EXEC sp_LocalTransferDetailSave "
            sQry = sQry & fncFormatSQLText(sTransferNo) & ", "
            sQry = sQry & fncFormatSQLText(sInventoryCode) & ", "
            sQry = sQry & fncFormatSQLText(sDescription) & ", "
            sQry = sQry & fncFormatSQLText(sClassCode) & ", "
            sQry = sQry & fncFormatSQLText(sCategoryCode) & ", "
            sQry = sQry & fncFormatSQLText(sCatalogueCode) & ", "
            sQry = sQry & Val(dQtyOnHand) & ", "
            sQry = sQry & Val(dWeight) & ", "
            sQry = sQry & Val(dSize) & ", "
            sQry = sQry & Val(dCostPerGram) & ", "
            sQry = sQry & Val(dPMC) & ", "
            sQry = sQry & Val(dSMC) & ", "
            sQry = sQry & Val(dPCS) & ", "
            sQry = sQry & Val(dTotalCost) & ", "
            sQry = sQry & dTransferTag & ", "
            sQry = sQry & fncFormatSQLText(sUserId)
            bSaveStatus = fncExecNonQryTransfer(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncTransferDetailtoLocal = bSaveStatus
            sQry = Nothing
        End Try

    End Function


    Public Function fncSalesTransferDetailtoLocal(ByVal sTransferNo As String, ByVal sInventoryCode As String, _
                ByVal sDescription As String, ByVal sClassCode As String, ByVal sCategoryCode As String, _
                ByVal sCatalogueCode As String, ByVal dQtyOnHand As Decimal, ByVal dWeight As Decimal, _
                ByVal dCostPerGram As Decimal, ByVal dPMC As Decimal, ByVal dSMC As Decimal, _
                ByVal dPCS As Decimal, ByVal dTotalCost As Decimal, ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim bSaveStatus As Boolean
        Try
            bSaveStatus = False
            sQry = sQry & "EXEC sp_SalesTransferDetailToLocal "
            sQry = sQry & fncFormatSQLText(sTransferNo) & ", "
            sQry = sQry & fncFormatSQLText(sInventoryCode) & ", "
            sQry = sQry & fncFormatSQLText(sDescription) & ", "
            sQry = sQry & fncFormatSQLText(sClassCode) & ", "
            sQry = sQry & fncFormatSQLText(sCategoryCode) & ", "
            sQry = sQry & fncFormatSQLText(sCatalogueCode) & ", "
            sQry = sQry & Val(dQtyOnHand) & ", "
            sQry = sQry & Val(dWeight) & ", "
            sQry = sQry & Val(dCostPerGram) & ", "
            sQry = sQry & Val(dPMC) & ", "
            sQry = sQry & Val(dSMC) & ", "
            sQry = sQry & Val(dPCS) & ", "
            sQry = sQry & Val(dTotalCost) & ", "
            sQry = sQry & fncFormatSQLText(sUserId)
            bSaveStatus = fncExecNonQryTransfer(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncSalesTransferDetailtoLocal = bSaveStatus
            sQry = Nothing
        End Try

    End Function


    Public Function fncUpdateTransferHeaderHQ(ByVal sTransferNo As String, ByVal sUserID As String) As Boolean
        Dim sQry As String
        Dim bUpdateStatus As Boolean
        Try
            sQry = "EXEC sp_UpdateTransferHeaderHQ "
            sQry = sQry & fncFormatSQLText(sTransferNo) & ", "
            sQry = sQry & fncFormatSQLText(sUserID)
            'bUpdateStatus = DM.ProjExecNonQry(sQry, gsConRemote)
            bUpdateStatus = fncExecNonQryTransferHQ(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            bUpdateStatus = False
        Finally
            fncUpdateTransferHeaderHQ = bUpdateStatus
            sQry = Nothing
        End Try
    End Function
    Public Function fncUpdateSalesTransferHeaderHQ(ByVal sTransferNo, ByVal sUserID) As Boolean
        Dim sQry As String
        Dim bUpdateStatus As Boolean
        Try
            sQry = "EXEC sp_UpdateSalesTransferHeaderHQ "
            sQry = sQry & fncFormatSQLText(sTransferNo) & ", "
            sQry = sQry & fncFormatSQLText(sUserID)
            'bUpdateStatus = DM.ProjExecNonQry(sQry, gsConRemote)
            bUpdateStatus = fncExecNonQryTransferHQ(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            bUpdateStatus = False
        Finally
            fncUpdateSalesTransferHeaderHQ = bUpdateStatus
            sQry = Nothing
        End Try
    End Function
    Public Function fncUpdateInventoryStockHQ(ByVal gthisLocation, ByVal sInventoryCode, ByVal sDescription, ByVal sDiamondDescription, _
       ByVal dWeight, ByVal dPCS, ByVal dTotalCost, ByVal dQtyOnHand, ByVal dQtySold, ByVal sUserID) As Boolean
        Dim sQry As String
        Dim bUpdateStatus As Boolean
        Try
            sQry = "EXEC sp_UpdateInventoryStockHQ "
            sQry = sQry & fncFormatSQLText(gthisLocation) & ", "
            sQry = sQry & fncFormatSQLText(sInventoryCode) & ", "
            sQry = sQry & fncFormatSQLText(sDescription) & ", "
            sQry = sQry & fncFormatSQLText(sDiamondDescription) & ", "
            sQry = sQry & dWeight & ", "
            sQry = sQry & dPCS & ", "
            sQry = sQry & dTotalCost & ", "
            sQry = sQry & dQtyOnHand & ", "
            sQry = sQry & dQtySold & ", "
            sQry = sQry & fncFormatSQLText(sUserID)
            'UpdateStatus = DM.ProjExecNonQry(sQry, gsConRemote)
            bUpdateStatus = fncExecNonQryTransferHQ(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateInventoryStockHQ = bUpdateStatus
            sQry = Nothing
        End Try
    End Function

    '''Public Function fncUpdateInventoryStockForFromLocation(ByVal sFromLocation, ByVal sInventoryCode, ByVal sDescription, ByVal sDiamondDescription, _
    '''   ByVal dWeight, ByVal dPCS, ByVal dTotalCost, ByVal dQtyOnHand, ByVal dQtySold, ByVal sUserID) As Boolean
    '''    Dim sQry As String
    '''    Dim bUpdateStatus As Boolean
    '''    Try
    '''        sQry = "EXEC sp_UpdateInventoryStockHQ "
    '''        sQry = sQry & fncFormatSQLText(sFromLocation) & ", "
    '''        sQry = sQry & fncFormatSQLText(sInventoryCode) & ", "
    '''        sQry = sQry & fncFormatSQLText(sDescription) & ", "
    '''        sQry = sQry & fncFormatSQLText(sDiamondDescription) & ", "
    '''        sQry = sQry & dWeight & ", "
    '''        sQry = sQry & dPCS & ", "
    '''        sQry = sQry & dTotalCost & ", "
    '''        sQry = sQry & dQtyOnHand & ", "
    '''        sQry = sQry & dQtySold & ", "
    '''        sQry = sQry & fncFormatSQLText(sUserID)
    '''        'UpdateStatus = DM.ProjExecNonQry(sQry, gsConRemote)
    '''        bUpdateStatus = fncExecNonQryTransferHQ(sQry)
    '''    Catch ex As Exception
    '''        MessageBox.Show(ex.Message)
    '''    Finally
    '''        fncUpdateInventoryStockHQ = bUpdateStatus
    '''        sQry = Nothing
    '''    End Try
    '''End Function

    Public Function fncUpdateTransferHeaderLocal(ByVal sTransferNo As String, ByVal sSalesManCode As String, ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim bUpdateStatus As Boolean
        Try
            sQry = "EXEC sp_UpdateTransferHeaderLocal "
            sQry = sQry & fncFormatSQLText(sTransferNo) & ", "
            sQry = sQry & fncFormatSQLText(sSalesManCode) & ", "
            sQry = sQry & fncFormatSQLText(sUserId)
            'UpdateStatus = DM.ProjExecNonQry(sQry)
            bUpdateStatus = fncExecNonQryTransfer(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateTransferHeaderLocal = bUpdateStatus
            sQry = Nothing
        End Try
    End Function
    Public Function fncUpdateSalesTransferHeaderLocal(ByVal sTransferNo As String, ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim bUpdateStatus As Boolean
        Try
            sQry = "EXEC sp_UpdateSalesTransferHeaderLocal "
            sQry = sQry & fncFormatSQLText(sTransferNo) & ", "
            sQry = sQry & fncFormatSQLText(sUserId)
            'UpdateStatus = DM.ProjExecNonQry(sQry)
            bUpdateStatus = fncExecNonQryTransfer(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateSalesTransferHeaderLocal = bUpdateStatus
            sQry = Nothing
        End Try
    End Function
    Public Function fncUpdateInventoryStockLocal(ByVal gthisLocation, ByVal sInventoryCode, ByVal sDescription, ByVal sDiamondDescription, _
        ByVal dWeight, ByVal dPCS, ByVal dTotalCost, ByVal dQtyOnHand, ByVal dQtySold, ByVal sUserID, ByVal sFromLocation) As Boolean
        Dim sQry As String
        Dim bUpdateStatus As Boolean
        Try
            sQry = "EXEC sp_UpdateInventoryStockLocal "
            sQry = sQry & fncFormatSQLText(gthisLocation) & ", "
            sQry = sQry & fncFormatSQLText(sInventoryCode) & ", "
            sQry = sQry & fncFormatSQLText(sDescription) & ", "
            sQry = sQry & fncFormatSQLText(sDiamondDescription) & ", "
            sQry = sQry & dWeight & ", "
            sQry = sQry & dPCS & ", "
            sQry = sQry & dTotalCost & ", "
            sQry = sQry & dQtyOnHand & ", "
            sQry = sQry & dQtySold & ", "
            sQry = sQry & fncFormatSQLText(sUserID) & ", "
            sQry = sQry & fncFormatSQLText(sFromLocation)
            'UpdateStatus = DM.ProjExecNonQry(sQry)
            bUpdateStatus = fncExecNonQryTransfer(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateInventoryStockLocal = bUpdateStatus
            sQry = Nothing
        End Try
    End Function

    Public Function fncInsertInventoryOnAccept(ByVal dsDetail As DataSet) As Boolean
        Dim sQry As String
        Dim dPmc, dSmc, dPcs, dLPcs As Decimal
        Dim iCnt As Integer
        Dim bStatus As Boolean
        Try
            bStatus = False
            For iCnt = 0 To dsDetail.Tables(0).Rows.Count - 1
                sQry = "EXEC sp_UpdateInventoryFromHQ "
                sQry = sQry & fncFormatSQLText(Trim(dsDetail.Tables(0).DefaultView.Item(iCnt)("InventoryCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsDetail.Tables(0).DefaultView.Item(iCnt)("Description") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsDetail.Tables(0).DefaultView.Item(iCnt)("DiamondDescription") & "") & "") & ","
                sQry = sQry & Val(dsDetail.Tables(0).DefaultView.Item(iCnt)("Weight") & "") & ","
                sQry = sQry & Val(dsDetail.Tables(0).DefaultView.Item(iCnt)("StoneWeight") & "") & ","
                sQry = sQry & Val(dsDetail.Tables(0).DefaultView.Item(iCnt)("CostPerGram") & "") & ","
                'sQry = sQry & Val(Val(dsDetail.Tables(0).DefaultView.Item(iCnt)("PMC") & "") / (Val(dsDetail.Tables(0).DefaultView.Item(iCnt)("PCS")))) & ","
                dPmc = Val(dsDetail.Tables(0).DefaultView.Item(iCnt)("PMC") & "")
                dSmc = Val(dsDetail.Tables(0).DefaultView.Item(iCnt)("SMC") & "")
                dPcs = (Val(dsDetail.Tables(0).DefaultView.Item(iCnt)("PCS")))
                dLPcs = (Val(dsDetail.Tables(0).DefaultView.Item(iCnt)("TransferedPCS")))
                sQry = sQry & Val((dPmc / dPcs) * dLPcs) & ","
                sQry = sQry & Val((dSmc / dPcs) * dLPcs) & ","
                ' sQry = sQry & Val(Val(dsDetail.Tables(0).DefaultView.Item(iCnt)("SMC") & "") / (Val(dsDetail.Tables(0).DefaultView.Item(iCnt)("PCS")))) & ","
                sQry = sQry & Val(dsDetail.Tables(0).DefaultView.Item(iCnt)("TransferedPCS") & "") & ","   '"0, " 
                sQry = sQry & Val(dsDetail.Tables(0).DefaultView.Item(iCnt)("GoldLoss") & "") & ","
                sQry = sQry & Val(dsDetail.Tables(0).DefaultView.Item(iCnt)("TotalCost") & "") & ","
                sQry = sQry & fncFormatSQLText(Trim(dsDetail.Tables(0).DefaultView.Item(iCnt)("JobSheetNo") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsDetail.Tables(0).DefaultView.Item(iCnt)("PurchaseBillNo") & "")) & ","
                sQry = sQry & fncFormatSQLDate((dsDetail.Tables(0).DefaultView.Item(iCnt)("PurchaseDate") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsDetail.Tables(0).DefaultView.Item(iCnt)("SupplierCode") & "")) & ","
                sQry = sQry & "'', " 'fncFormatSQLText(Trim(dsDetail.Tables(0).DefaultView.Item(iCnt)("SalesBillNo") & "")) & ","
                sQry = sQry & "NULL, " 'fncFormatSQLDate((dsDetail.Tables(0).DefaultView.Item(iCnt)("SalesDate") & "")) & ","
                sQry = sQry & Val(Trim(dsDetail.Tables(0).DefaultView.Item(iCnt)("SalesCostPerGram") & "")) & ","
                sQry = sQry & Val(Trim(dsDetail.Tables(0).DefaultView.Item(iCnt)("SalesMC") & "")) & ","
                sQry = sQry & Val(Trim(dsDetail.Tables(0).DefaultView.Item(iCnt)("SalesAmount") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsDetail.Tables(0).DefaultView.Item(iCnt)("CategoryCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsDetail.Tables(0).DefaultView.Item(iCnt)("ClassCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsDetail.Tables(0).DefaultView.Item(iCnt)("CatalogueCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsDetail.Tables(0).DefaultView.Item(iCnt)("TrayId") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsDetail.Tables(0).DefaultView.Item(iCnt)("PMCType") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsDetail.Tables(0).DefaultView.Item(iCnt)("SMCType") & "")) & ","
                sQry = sQry & "0, " 'Val(Trim(dsDetail.Tables(0).DefaultView.Item(iCnt)("PCS") & "")) & "," 'For QuantityOnHand
                sQry = sQry & Val(Trim(dsDetail.Tables(0).DefaultView.Item(iCnt)("QtySold") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsDetail.Tables(0).DefaultView.Item(iCnt)("Remarks") & "") & "") & ","
                sQry = sQry & fncFormatSQLText(Trim(dsDetail.Tables(0).DefaultView.Item(iCnt)("CreateUser") & "")) & ","
                sQry = sQry & fncFormatSQLDate(dsDetail.Tables(0).DefaultView.Item(iCnt)("CreateDate") & "") & ","
                sQry = sQry & fncFormatSQLText(Trim(sUserID)) & ","
                sQry = sQry & fncFormatSQLText(gthisLocation)
                'DM.ProjExecNonQry(sQry, gsCon)
                bStatus = fncExecNonQryTransfer(sQry)
                If bStatus = False Then Exit For
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsDetail = Nothing
            sQry = Nothing
            fncInsertInventoryOnAccept = bStatus
        End Try
    End Function

    Public Function fncInsertInventoryStockOnAccept(ByVal dsIS As DataSet) As Boolean
        Dim iCnt As Integer
        Dim sQry As String
        Dim bStatus As Boolean
        Try
            bStatus = False
            For iCnt = 0 To dsIS.Tables(0).Rows.Count - 1
                sQry = "EXEC sp_UpdateInventoryStock "
                sQry = sQry & fncFormatSQLText(Trim(dsIS.Tables(0).DefaultView.Item(iCnt)("LocationCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsIS.Tables(0).DefaultView.Item(iCnt)("InventoryCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsIS.Tables(0).DefaultView.Item(iCnt)("Description") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsIS.Tables(0).DefaultView.Item(iCnt)("DiamondDescription") & "")) & ","
                sQry = sQry & Val(Trim(dsIS.Tables(0).DefaultView.Item(iCnt)("Weight") & "")) & ","
                sQry = sQry & Val(Trim(dsIS.Tables(0).DefaultView.Item(iCnt)("PCS") & "")) & ","
                sQry = sQry & Val(Trim(dsIS.Tables(0).DefaultView.Item(iCnt)("TotalCost") & "")) & ","
                sQry = sQry & Val(Trim(dsIS.Tables(0).DefaultView.Item(iCnt)("QtyOnHand") & "")) & ","
                sQry = sQry & Val(Trim(dsIS.Tables(0).DefaultView.Item(iCnt)("QtySold") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsIS.Tables(0).DefaultView.Item(iCnt)("CreateUser") & "")) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsIS.Tables(0).DefaultView.Item(iCnt)("CreateDate") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(sUserID))
                'DM.ProjExecNonQry(sQry, gsCon)
                bStatus = fncExecNonQryTransfer(sQry)
                If bStatus = False Then
                    Exit For
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsIS = Nothing
            sQry = Nothing
            fncInsertInventoryStockOnAccept = bStatus
        End Try
    End Function

    Public Function fncInsertInventoryAndInventoryStock(ByVal drInventory() As DataRow) As Boolean
        Dim sQry As String
        Dim bStatus As Boolean
        Dim iCnt As Integer
        Dim ByteArr() As Byte
        Try
            For iCnt = 0 To drInventory.Length - 1

                ' Dim ImageinBytes As Byte() = drInventory(iCnt)("InventoryImage")

                Dim Cmd As New SqlClient.SqlCommand("sp_InsertInventoryAndStock", gsConBatchPosting)
                Cmd.Transaction = gsTrans
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Parameters.Add("@LocationCode", SqlDbType.VarChar)
                Cmd.Parameters("@LocationCode").Value = (Trim(drInventory(iCnt)("LocationCode") & ""))
                Cmd.Parameters.Add("@InventoryCode", SqlDbType.VarChar)
                Cmd.Parameters("@InventoryCode").Value = (Trim(drInventory(iCnt)("InventoryCode") & ""))
                Cmd.Parameters.Add("@Description", SqlDbType.VarChar)
                Cmd.Parameters("@Description").Value = (Trim(drInventory(iCnt)("Description") & ""))
                Cmd.Parameters.Add("@DiamondDescription", SqlDbType.VarChar)
                Cmd.Parameters("@DiamondDescription").Value = (Trim(drInventory(iCnt)("DiamondDescription") & ""))
                Cmd.Parameters.Add("@Weight", SqlDbType.Decimal)
                Cmd.Parameters("@Weight").Value = Val(Trim(drInventory(iCnt)("Weight") & ""))
                Cmd.Parameters.Add("@StoneWeight", SqlDbType.Decimal)
                Cmd.Parameters("@StoneWeight").Value = Val(Trim(drInventory(iCnt)("StoneWeight") & ""))
                Cmd.Parameters.Add("@CostPerGram", SqlDbType.Decimal)
                Cmd.Parameters("@CostPerGram").Value = Val(Trim(drInventory(iCnt)("CostPerGram") & ""))
                Cmd.Parameters.Add("@PMC", SqlDbType.Decimal)
                Cmd.Parameters("@PMC").Value = 0
                Cmd.Parameters.Add("@SMC", SqlDbType.Decimal)
                Cmd.Parameters("@SMC").Value = 0
                Cmd.Parameters.Add("@PCS", SqlDbType.Decimal)
                Cmd.Parameters("@PCS").Value = Val(Trim(drInventory(iCnt)("PCS") & ""))
                Cmd.Parameters.Add("@GoldLoss", SqlDbType.Decimal)
                Cmd.Parameters("@GoldLoss").Value = Val(Trim(drInventory(iCnt)("PCS") & ""))
                Cmd.Parameters.Add("@TotalCost", SqlDbType.Decimal)
                Cmd.Parameters("@TotalCost").Value = Val(Trim(drInventory(iCnt)("TotalCost") & ""))
                Cmd.Parameters.Add("@JobSheetNo", SqlDbType.VarChar)
                Cmd.Parameters("@JobSheetNo").Value = (Trim(drInventory(iCnt)("JobSheetNo") & ""))
                Cmd.Parameters.Add("@PurchaseBillNo", SqlDbType.VarChar)
                Cmd.Parameters("@PurchaseBillNo").Value = (Trim(drInventory(iCnt)("PurchaseBillNo") & ""))
                Cmd.Parameters.Add("@PurchaseDate", SqlDbType.SmallDateTime)
                Cmd.Parameters("@PurchaseDate").Value = (Trim(drInventory(iCnt)("PurchaseDate") & ""))
                Cmd.Parameters.Add("@SupplierCode", SqlDbType.VarChar)
                Cmd.Parameters("@SupplierCode").Value = (Trim(drInventory(iCnt)("SupplierCode") & ""))
                Cmd.Parameters.Add("@SalesBillNo", SqlDbType.VarChar)
                Cmd.Parameters("@SalesBillNo").Value = (Trim(drInventory(iCnt)("SalesBillNo") & ""))
                Cmd.Parameters.Add("@SalesDate", SqlDbType.SmallDateTime)
                Cmd.Parameters("@SalesDate").Value = (Trim(drInventory(iCnt)("PurchaseDate") & ""))
                Cmd.Parameters.Add("@SalesCostPerGram", SqlDbType.Decimal)
                Cmd.Parameters("@SalesCostPerGram").Value = Val(Trim(drInventory(iCnt)("SalesCostPerGram") & ""))
                Cmd.Parameters.Add("@SalesMC", SqlDbType.Decimal)
                Cmd.Parameters("@SalesMC").Value = Val(Trim(drInventory(iCnt)("SalesMC") & ""))
                Cmd.Parameters.Add("@SalesAmount", SqlDbType.Decimal)
                Cmd.Parameters("@SalesAmount").Value = Val(Trim(drInventory(iCnt)("SalesAmount") & ""))
                Cmd.Parameters.Add("@CategoryCode", SqlDbType.VarChar)
                Cmd.Parameters("@CategoryCode").Value = (Trim(drInventory(iCnt)("CategoryCode") & ""))
                Cmd.Parameters.Add("@ClassCode", SqlDbType.VarChar)
                Cmd.Parameters("@ClassCode").Value = (Trim(drInventory(iCnt)("ClassCode") & ""))
                Cmd.Parameters.Add("@CatalogueCode", SqlDbType.VarChar)
                Cmd.Parameters("@CatalogueCode").Value = (Trim(drInventory(iCnt)("CatalogueCode") & ""))
                Cmd.Parameters.Add("@TrayId", SqlDbType.VarChar)
                Cmd.Parameters("@TrayId").Value = (Trim(drInventory(iCnt)("TrayId") & ""))
                Cmd.Parameters.Add("@PMCType", SqlDbType.VarChar)
                Cmd.Parameters("@PMCType").Value = (Trim(drInventory(iCnt)("PMCType") & ""))
                Cmd.Parameters.Add("@SMCType", SqlDbType.VarChar)
                Cmd.Parameters("@SMCType").Value = (Trim(drInventory(iCnt)("SMCType") & ""))
                Cmd.Parameters.Add("@QtyOnHand", SqlDbType.Int)
                Cmd.Parameters("@QtyOnHand").Value = 0
                Cmd.Parameters.Add("@QtySold", SqlDbType.Int)
                Cmd.Parameters("@QtySold").Value = 0
                Cmd.Parameters.Add("@OrderNo", SqlDbType.VarChar)
                Cmd.Parameters("@OrderNo").Value = (Trim(drInventory(iCnt)("OrderNo") & ""))
                Cmd.Parameters.Add("@Remarks", SqlDbType.VarChar)
                Cmd.Parameters("@Remarks").Value = (Trim(drInventory(iCnt)("Remarks") & ""))
                Cmd.Parameters.Add("@CreateUser", SqlDbType.VarChar)
                Cmd.Parameters("@CreateUser").Value = (Trim(drInventory(iCnt)("CreateUser") & ""))
                Cmd.Parameters.Add("@CreateDate", SqlDbType.DateTime)
                Cmd.Parameters("@CreateDate").Value = (Trim(drInventory(iCnt)("CreateDate") & ""))
                Cmd.Parameters.Add("@UserID", SqlDbType.VarChar)
                Cmd.Parameters("@UserID").Value = (sUserID)
                Cmd.Parameters.Add("@SizeCode", SqlDbType.VarChar)
                Cmd.Parameters("@SizeCode").Value = Val(Trim(drInventory(iCnt)("SizeCode") & ""))
                Cmd.Parameters.Add("@SizeType", SqlDbType.VarChar)
                Cmd.Parameters("@SizeType").Value = (Trim(drInventory(iCnt)("SizeType") & ""))
                Cmd.Parameters.Add("@SalesPCS", SqlDbType.Bit)
                Cmd.Parameters("@SalesPCS").Value = (Trim(drInventory(iCnt)("SalesPCS") & ""))
                Cmd.Parameters.Add("@InventoryImage", SqlDbType.Image)
                Cmd.Parameters("@InventoryImage").Value = drInventory(iCnt)("InventoryImage")
                'Cmd.Parameters.Add("@InventoryImage", SqlDbType.Image)
                'Cmd.Parameters("@InventoryImage").Value = (Trim(drInventory(iCnt)("SalesPCS") & ""))
                Cmd.Parameters.Add("@InventoryImageName", SqlDbType.VarChar)
                Cmd.Parameters("@InventoryImageName").Value = (Trim(drInventory(iCnt)("InventoryImageName") & ""))
                Cmd.ExecuteNonQuery()
                ' sCreateInventory = Cmd.ExecuteScalar

                'sQry = "Exec sp_InsertInventoryAndStock "
                'sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("LocationCode") & "")) & ", "                                          'LocationCode		char(20), "
                ''sQry &= fncFormatSQLText(Trim(gthisLocation & "")) & ", "
                'sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("InventoryCode") & "")) & ", "          '@InventoryCode		char(20), 
                'sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("Description") & "")) & ", "            '@Description			char(50),
                'sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("DiamondDescription") & "")) & ", "     '@DiamondDescription	char(50),
                'sQry &= Val(Trim(drInventory(iCnt)("Weight") & "")) & ", "                                                                        '@Weight			numeric(18, 2),
                'sQry &= Val(Trim(drInventory(iCnt)("StoneWeight") & "")) & ", "                         '@StoneWeight		numeric(18, 2),
                'sQry &= Val(Trim(drInventory(iCnt)("CostPerGram") & "")) & ", "                         '@CostPerGram		numeric(18, 2),
                'sQry &= 0 & ", "                                                                        '@PMC			numeric(18, 2),
                'sQry &= 0 & ", "                                                                        '@SMC				numeric(18, 2),
                'sQry &= Val(Trim(drInventory(iCnt)("PCS") & "")) & ", "                                                                        '@PCS				numeric(18, 2),
                'sQry &= Val(Trim(drInventory(iCnt)("GoldLoss") & "")) & ", "                            '@GoldLoss			numeric(18, 2),
                'sQry &= Val(Trim(drInventory(iCnt)("TotalCost") & "")) & ", "                            '@TotalCost			numeric(18, 2),			
                'sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("JobSheetNo") & "")) & ", "             '@JobSheetNo			char(20), 
                'sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("PurchaseBillNo") & "")) & ", "         '@PurchaseBillNo		char(20), 
                'sQry &= fncFormatSQLDate(Trim(drInventory(iCnt)("PurchaseDate") & "")) & ", "           '@PurchaseDate		smalldatetime, 
                'sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("SupplierCode") & "")) & ", "           '@SupplierCode		char(20), 
                'sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("SalesBillNo") & "")) & ", "            '@SalesBillNo			char(20), 
                'sQry &= fncFormatSQLDate(Trim(drInventory(iCnt)("SalesDate") & "")) & ", "              '@SalesDate			smalldatetime, 
                'sQry &= Val(Trim(drInventory(iCnt)("SalesCostPerGram") & "")) & ", "                    ' @SalesCostPerGram		numeric(18, 2),
                'sQry &= Val(Trim(drInventory(iCnt)("SalesMC") & "")) & ", "                             '@SalesMC			numeric(18, 2),
                'sQry &= Val(Trim(drInventory(iCnt)("SalesAmount") & "")) & ", "                         '@SalesAmount		numeric(18, 2),
                'sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("CategoryCode") & "")) & ", "           '@CategoryCode		char(20),
                'sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("ClassCode") & "")) & ", "              '@ClassCode			char(20), 
                'sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("CatalogueCode") & "")) & ", "          '@CatalogueCode		char(20), 
                'sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("TrayId") & "")) & ", "                 '@TrayId			char(20), 
                'sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("PMCType") & "")) & ", "                '@PMCType			char(1), 
                'sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("SMCType") & "")) & ", "                '@SMCType			char(1), 
                'sQry &= 0 & ", "                                                                        '@QtyOnHand			numeric(18, 0),
                'sQry &= 0 & ", "                                                                        '@QtySold			numeric(18, 0),
                'sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("OrderNo") & "")) & ", "                '@OrderNo			char(10), 
                'sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("Remarks") & "")) & ", "                '@Remarks			char(50),
                'sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("CreateUser") & "")) & ", "             '@CreateUser			char(10),
                'sQry &= fncFormatSQLDate(Trim(drInventory(iCnt)("CreateDate") & "")) & ", "             '@CreateDate			smalldatetime,	
                'sQry &= fncFormatSQLText(sUserID) & ", "                                                 '@UserId			char(10)
                'sQry &= Val(Trim(drInventory(iCnt)("SizeCode") & "")) & ","
                'sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("SizeType") & ""))
                '' & ","
                ''sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("SalesPCS") & "")) & ", "
                ''sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("InventoryImage") & "")) & ", "
                ''sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("InventoryImageName") & ""))
                'bStatus = fncExecNonQryTransfer(sQry)
                'If bStatus = False Then
                '    Exit For
                'End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            fncInsertInventoryAndInventoryStock = False
        Finally
            drInventory = Nothing
            sQry = Nothing
            fncInsertInventoryAndInventoryStock = True
        End Try
    End Function
    Public Function fncInsertInventoryAndInventoryStockOLD(ByVal drInventory() As DataRow) As Boolean
        Dim sQry As String
        Dim bStatus As Boolean
        Dim iCnt As Integer
        Try
            For iCnt = 0 To drInventory.Length - 1
                sQry = "Exec sp_InsertInventoryAndStock "
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("LocationCode") & "")) & ", "                                          'LocationCode		char(20), "
                'sQry &= fncFormatSQLText(Trim(gthisLocation & "")) & ", "
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("InventoryCode") & "")) & ", "          '@InventoryCode		char(20), 
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("Description") & "")) & ", "            '@Description			char(50),
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("DiamondDescription") & "")) & ", "     '@DiamondDescription	char(50),
                sQry &= Val(Trim(drInventory(iCnt)("Weight") & "")) & ", "                                                                        '@Weight			numeric(18, 2),
                sQry &= Val(Trim(drInventory(iCnt)("StoneWeight") & "")) & ", "                         '@StoneWeight		numeric(18, 2),
                sQry &= Val(Trim(drInventory(iCnt)("CostPerGram") & "")) & ", "                         '@CostPerGram		numeric(18, 2),
                sQry &= 0 & ", "                                                                        '@PMC			numeric(18, 2),
                sQry &= 0 & ", "                                                                        '@SMC				numeric(18, 2),
                sQry &= Val(Trim(drInventory(iCnt)("PCS") & "")) & ", "                                                                        '@PCS				numeric(18, 2),
                sQry &= Val(Trim(drInventory(iCnt)("GoldLoss") & "")) & ", "                            '@GoldLoss			numeric(18, 2),
                sQry &= Val(Trim(drInventory(iCnt)("TotalCost") & "")) & ", "                            '@TotalCost			numeric(18, 2),			
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("JobSheetNo") & "")) & ", "             '@JobSheetNo			char(20), 
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("PurchaseBillNo") & "")) & ", "         '@PurchaseBillNo		char(20), 
                sQry &= fncFormatSQLDate(Trim(drInventory(iCnt)("PurchaseDate") & "")) & ", "           '@PurchaseDate		smalldatetime, 
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("SupplierCode") & "")) & ", "           '@SupplierCode		char(20), 
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("SalesBillNo") & "")) & ", "            '@SalesBillNo			char(20), 
                sQry &= fncFormatSQLDate(Trim(drInventory(iCnt)("SalesDate") & "")) & ", "              '@SalesDate			smalldatetime, 
                sQry &= Val(Trim(drInventory(iCnt)("SalesCostPerGram") & "")) & ", "                    ' @SalesCostPerGram		numeric(18, 2),
                sQry &= Val(Trim(drInventory(iCnt)("SalesMC") & "")) & ", "                             '@SalesMC			numeric(18, 2),
                sQry &= Val(Trim(drInventory(iCnt)("SalesAmount") & "")) & ", "                         '@SalesAmount		numeric(18, 2),
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("CategoryCode") & "")) & ", "           '@CategoryCode		char(20),
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("ClassCode") & "")) & ", "              '@ClassCode			char(20), 
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("CatalogueCode") & "")) & ", "          '@CatalogueCode		char(20), 
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("TrayId") & "")) & ", "                 '@TrayId			char(20), 
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("PMCType") & "")) & ", "                '@PMCType			char(1), 
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("SMCType") & "")) & ", "                '@SMCType			char(1), 
                sQry &= 0 & ", "                                                                        '@QtyOnHand			numeric(18, 0),
                sQry &= 0 & ", "                                                                        '@QtySold			numeric(18, 0),
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("OrderNo") & "")) & ", "                '@OrderNo			char(10), 
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("Remarks") & "")) & ", "                '@Remarks			char(50),
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("CreateUser") & "")) & ", "             '@CreateUser			char(10),
                sQry &= fncFormatSQLDate(Trim(drInventory(iCnt)("CreateDate") & "")) & ", "             '@CreateDate			smalldatetime,	
                sQry &= fncFormatSQLText(sUserID) & ", "                                                 '@UserId			char(10)
                sQry &= Val(Trim(drInventory(iCnt)("SizeCode") & "")) & ","
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("SizeType") & ""))
                '& ","
                'sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("SalesPCS") & "")) & ", "
                'sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("InventoryImage") & "")) & ", "
                'sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("InventoryImageName") & ""))
                bStatus = fncExecNonQryTransfer(sQry)
                If bStatus = False Then
                    Exit For
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            drInventory = Nothing
            sQry = Nothing
            fncInsertInventoryAndInventoryStockOLD = bStatus
        End Try
    End Function
    Public Function fncInsertInventoryAddlDescription(ByVal drInventory() As DataRow) As Boolean
        Dim sQry As String
        Dim bStatus As Boolean
        Dim iCnt As Integer
        Try

            For iCnt = 0 To drInventory.Length - 1
                sQry = "Exec sp_InsertInventoryAddlDescription "
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("InventoryCode") & "")) & ", "                                          'LocationCode		char(20), "
                'sQry &= fncFormatSQLText(Trim(gthisLocation & "")) & ", "
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("JobSheetNo") & "")) & ", "          '@InventoryCode		char(20), 
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("Description1") & "")) & ", "            '@Description			char(50),
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("Description2") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("Description3") & "")) & ", "            '@Description			char(50),
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("Description4") & "")) & ", "      '@DiamondDescription	char(50),
                sQry &= Val(Trim(drInventory(iCnt)("Cost1") & "")) & ", "                                                                        '@Weight			numeric(18, 2),
                sQry &= Val(Trim(drInventory(iCnt)("Cost2") & "")) & ", "                         '@StoneWeight		numeric(18, 2),
                sQry &= Val(Trim(drInventory(iCnt)("Cost3") & "")) & ", "
                sQry &= Val(Trim(drInventory(iCnt)("Cost4") & "")) & ", "                                                                        '@Weight			numeric(18, 2),
                sQry &= Val(Trim(drInventory(iCnt)("SellingPrice1") & "")) & ", "                         '@StoneWeight		numeric(18, 2),
                sQry &= Val(Trim(drInventory(iCnt)("SellingPrice2") & "")) & ", "
                sQry &= Val(Trim(drInventory(iCnt)("SellingPrice3") & "")) & ", "                                                                        '@Weight			numeric(18, 2),
                sQry &= Val(Trim(drInventory(iCnt)("SellingPrice4") & "")) & ", "                         '@StoneWeight		numeric(18, 2),
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("CreateUser") & "")) & ", "             '@CreateUser			char(10),
                sQry &= fncFormatSQLDate(Trim(drInventory(iCnt)("CreateDate") & "")) & ", "             '@CreateDate			smalldatetime,	
                sQry &= fncFormatSQLText(sUserID)
                bStatus = fncExecNonQryTransfer(sQry)
                If bStatus = False Then
                    Exit For
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            drInventory = Nothing
            sQry = Nothing
            fncInsertInventoryAddlDescription = bStatus
        End Try
    End Function
    Public Function fncInsertInventoryBarCoin(ByVal drInventory() As DataRow) As Boolean
        Dim sQry As String
        Dim bStatus As Boolean
        Dim iCnt As Integer
        Try

            For iCnt = 0 To drInventory.Length - 1
                sQry = "Exec sp_InsertInventoryBarCoin "
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("InventoryCode") & "")) & ", "                                          'LocationCode		char(20), "
                'sQry &= fncFormatSQLText(Trim(gthisLocation & "")) & ", "
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("MetalType") & "")) & ", "          '@InventoryCode		char(20), 
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("GstType") & "")) & ", "            '@Description			char(50),
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("Purity") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("Refiner") & "")) & ", "            '@Description			char(50),
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("Country") & "")) & ", "      '@DiamondDescription	char(50),
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("CreateUser") & "")) & ", "             '@CreateUser			char(10),
                sQry &= fncFormatSQLDate(Trim(drInventory(iCnt)("CreateDate") & "")) & ", "             '@CreateDate			smalldatetime,	
                sQry &= fncFormatSQLText(sUserID) & ", "
                sQry &= fncFormatSQLText(Trim(drInventory(iCnt)("BarNo") & ""))
                bStatus = fncExecNonQryTransfer(sQry)
                If bStatus = False Then
                    Exit For
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            drInventory = Nothing
            sQry = Nothing
            fncInsertInventoryBarCoin = bStatus
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
    Function fncUpdateInventoryStock(ByVal drDetail() As DataRow) As Boolean
        Dim sQry As String
        Dim bStatus As Boolean
        Dim iCnt As Integer
        Try
            For iCnt = 0 To drDetail.Length - 1
                sQry = "Exec sp_UpdateInventoryAndStock "
                sQry &= fncFormatSQLText(gthisLocation) & ", "  '@LocationCode Char(20), 
                sQry &= fncFormatSQLText(Trim(drDetail(iCnt)("InventoryCode") & "")) & ", "  '@InventoryCode Char(20), 
                sQry &= Val(Trim(drDetail(iCnt)("TransferredWeight") & "")) & ", "  '@Weight Numeric(18,2),
                sQry &= Val(Trim(drDetail(iCnt)("TransferredPMC") & "")) & ", "  '@PMC Numeric(18,2),
                sQry &= Val(Trim(drDetail(iCnt)("TransferredSMC") & "")) & ", "  '@SMC Numeric(18,2),
                sQry &= Val(Trim(drDetail(iCnt)("TransferredPCS") & "")) & ", "  '@PCS Numeric(18,2),
                sQry &= Val(Trim(drDetail(iCnt)("TransferredTotalCost") & "")) & ", "  '@TotalCost Numeric(18,2),
                sQry &= Val(Trim(drDetail(iCnt)("TransferredQtyOnHand") & "")) & ", "  '@QtyOnHand Numeric(18,0),
                sQry &= fncFormatSQLText(Trim(drDetail(iCnt)("PurchaseType") & "")) & ", "  '@PurchaseType char(1), 
                sQry &= fncFormatSQLText(sUserID) & ", "
                sQry &= fncFormatSQLText(Trim(drDetail(iCnt)("Description") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(drDetail(iCnt)("DiamondDescription") & "")) & ","
                sQry &= Val(Trim(drDetail(iCnt)("GoldLoss") & "")) & ", "
                sQry &= Val(Trim(drDetail(iCnt)("SizeCode") & ""))
                bStatus = fncExecNonQryTransfer(sQry)
                If bStatus = False Then
                    Exit For
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            drDetail = Nothing
            sQry = Nothing
            fncUpdateInventoryStock = bStatus
        End Try
    End Function
    Function fncUpdateInventoryStockHQ(ByVal drDetail() As DataRow) As Boolean
        Dim sQry As String
        Dim bStatus As Boolean
        Dim iCnt As Integer
        Try
            For iCnt = 0 To drDetail.Length - 1
                sQry = "Exec sp_UpdateInventoryAndStock "
                sQry &= fncFormatSQLText(gthisLocation) & ", "  '@LocationCode Char(20), 
                sQry &= fncFormatSQLText(Trim(drDetail(iCnt)("InventoryCode") & "")) & ", "  '@InventoryCode Char(20), 
                sQry &= Val(Trim(drDetail(iCnt)("TransferredWeight") & "")) & ", "  '@Weight Numeric(18,2),
                sQry &= Val(Trim(drDetail(iCnt)("TransferredPMC") & "")) & ", "  '@PMC Numeric(18,2),
                sQry &= Val(Trim(drDetail(iCnt)("TransferredSMC") & "")) & ", "  '@SMC Numeric(18,2),
                sQry &= Val(Trim(drDetail(iCnt)("TransferredPCS") & "")) & ", "  '@PCS Numeric(18,2),
                sQry &= Val(Trim(drDetail(iCnt)("TransferredTotalCost") & "")) & ", "  '@TotalCost Numeric(18,2),
                sQry &= Val(Trim(drDetail(iCnt)("TransferredQtyOnHand") & "")) & ", "  '@QtyOnHand Numeric(18,0),
                sQry &= fncFormatSQLText(Trim(drDetail(iCnt)("PurchaseType") & "")) & ", "  '@PurchaseType char(1), 
                sQry &= fncFormatSQLText(sUserID) & ", "
                sQry &= fncFormatSQLText(Trim(drDetail(iCnt)("Description") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(drDetail(iCnt)("DiamondDescription") & "")) & ","
                sQry &= Val(Trim(drDetail(iCnt)("GoldLoss") & "")) & ", "
                sQry &= Val(Trim(drDetail(iCnt)("SizeCode") & ""))
                bStatus = fncExecNonQryTransferHQ(sQry)
                If bStatus = False Then
                    Exit For
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            drDetail = Nothing
            sQry = Nothing
            fncUpdateInventoryStockHQ = bStatus
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
        gsTransferConHQ.ConnectionString = gsConAcc
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

End Class
