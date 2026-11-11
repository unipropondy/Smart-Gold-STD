Public Class ClsSliptInventory
    Dim dm As New DataManager

#Region "Get Inventory Detail"

    Public Function GetInventoryDetail(ByVal sInventoryCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "SELECT *,Pdate = CONVERT(CHAR(8),PurchaseDate,112) FROM vwInventoryStock WHERE InventoryCode =" & fncFormatSQLText(sInventoryCode) & " and QtySold = 0 and PCS = 1 and QtyOnHand <> 0 "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Inv", "Inv")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetInventoryDetail = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function GetSplitInventoryDetail(ByVal sInventoryCode As String, ByVal sCategoryCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "SELECT *,Pdate = CONVERT(CHAR(8),PurchaseDate,112) FROM vwInventoryStock WHERE InventoryCode =" & fncFormatSQLText(sInventoryCode)
            sQry = sQry & " and QtySold = 0 and PCS = 1 and QtyOnHand <> 0" & vbNewLine
            sQry = sQry & "SELECT * FROM tblCategory WHERE CategoryCode =" & fncFormatSQLText(sCategoryCode)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Inv", "Inv")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetSplitInventoryDetail = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetTradeInSplitInventoryDetail(ByVal sInventoryCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "SELECT * FROM tblTradeMergeInventoryheader WHERE TradeInInventoryCode =" & fncFormatSQLText(sInventoryCode)
            sQry = sQry & " and QtySold = 0 and QtyOnHand <> 0" & vbNewLine
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Inv", "Inv")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetTradeInSplitInventoryDetail = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function IsInventory(ByVal sInventoryCode As String) As Boolean
        Dim Ds As New DataSet
        Dim sQry As String
        Dim bIsInventory As Boolean
        Try
            bIsInventory = False
            sQry = " select count(Inventorycode) InvCount from tblinventory where Inventorycode = " & fncFormatSQLText(sInventoryCode)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Inv", "Inv")
            If Ds.Tables(0).Rows.Count > 0 Then
                If Val(Ds.Tables(0).Rows(0)(0)) > 0 Then
                    bIsInventory = True
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            IsInventory = bIsInventory
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

#End Region

#Region "Inventory Save"

    

    Public Function UpdateSplitInventory(ByVal sInventoryCode As String, ByVal dSourceWeight As Double, ByVal sOrgInventoryCode As String, ByVal sOrgDescription As String, _
                                         ByVal sOrgCategory As String, ByVal sOrgCatalogue As String, _
                                         ByVal sOrgSizeCode As String, ByVal sOrgSizeType As String, ByVal dSourceStWeight As Double, ByVal dOriginalSMC As Double, ByVal sComShortCode As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "sp_UpdateSplitInventory "
            sQry = sQry & fncFormatSQLText(sInventoryCode) & ","
            sQry = sQry & Val(dSourceWeight) & ","
            sQry = sQry & fncFormatSQLText(sOrgInventoryCode) & ","
            sQry = sQry & fncFormatSQLText(sOrgDescription) & ","
            sQry = sQry & fncFormatSQLText(sOrgCategory) & ","
            sQry = sQry & fncFormatSQLText(sOrgCatalogue) & ","
            sQry = sQry & Val(sOrgSizeCode) & ","
            sQry = sQry & fncFormatSQLText(sOrgSizeType) & ","
            sQry = sQry & Val(dSourceStWeight) & ","
            sQry = sQry & Val(dOriginalSMC) & ","
            sQry = sQry & fncFormatSQLText(sComShortCode) & ","
            sQry = sQry & fncFormatSQLText(sUserID)
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            UpdateSplitInventory = SaveStatus
            sQry = Nothing
        End Try
    End Function
    Public Function UpdateSplitInventoryDiff(ByVal sInventoryCode As String, ByVal dSourceWeight As Double, ByVal sOrgInventoryCode As String, ByVal sOrgDescription As String, _
                                     ByVal sOrgCategory As String, ByVal sOrgCatalogue As String, _
                                     ByVal sOrgSizeCode As String, ByVal sOrgSizeType As String, ByVal dSourceStWeight As Double, ByVal dOriginalSMC As Double, ByVal sComShortCode As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "sp_UpdateSplitInventoryDiff "
            sQry = sQry & fncFormatSQLText(sInventoryCode) & ","
            sQry = sQry & Val(dSourceWeight) & ","
            sQry = sQry & fncFormatSQLText(sOrgInventoryCode) & ","
            sQry = sQry & fncFormatSQLText(sOrgDescription) & ","
            sQry = sQry & fncFormatSQLText(sOrgCategory) & ","
            sQry = sQry & fncFormatSQLText(sOrgCatalogue) & ","
            sQry = sQry & Val(sOrgSizeCode) & ","
            sQry = sQry & fncFormatSQLText(sOrgSizeType) & ","
            sQry = sQry & Val(dSourceStWeight) & ","
            sQry = sQry & Val(dOriginalSMC) & ","
            sQry = sQry & fncFormatSQLText(sComShortCode) & ","
            sQry = sQry & fncFormatSQLText(sUserID)
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            UpdateSplitInventoryDiff = SaveStatus
            sQry = Nothing
        End Try
    End Function

    Public Function InventorySave(ByVal sInventoryCode As String, ByVal sDescription As String, ByVal sDiamondDescription As String, _
        ByVal dWeight As Double, ByVal dStoneWeight As Double, ByVal dCostperGram As Double, ByVal dPMC As Double, ByVal dSMC As Double, ByVal dPCS As Double, _
        ByVal dGoldLoss As Double, ByVal dTotalCost As Double, ByVal sJobSheetNo As String, ByVal sPurchaseBillNo As String, ByVal sPurchaseDate As String, _
        ByVal sSupplierCode As String, ByVal sSalesBillNo As String, ByVal sSalesDate As String, ByVal dSalesCostPerGram As Double, ByVal dSalesMC As Double, _
        ByVal dSalesAmount As Double, ByVal sCategoryCode As String, ByVal sClassCode As String, ByVal sCatalogueCode As String, ByVal sTrayId As String, _
        ByVal sPMCType As String, ByVal sSMCType As String, ByVal lQtyOnHand As Long, ByVal lQtySold As Long, ByVal sRemarks As String, ByVal sSizeCode As String, ByVal sSizeType As String) As Boolean

        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = sQry & "EXEC sp_Inventory " & fncFormatSQLText(sInventoryCode) & ","
            sQry = sQry & fncFormatSQLText(sDescription) & ","
            sQry = sQry & fncFormatSQLText(sDiamondDescription) & ","
            sQry = sQry & Val(dWeight) & ","
            sQry = sQry & Val(dStoneWeight) & ","
            sQry = sQry & Val(dCostperGram) & ","
            sQry = sQry & Val(dPMC) & ","
            sQry = sQry & Val(dSMC) & ","
            sQry = sQry & Val(dPCS) & ","
            sQry = sQry & Val(dGoldLoss) & ","
            sQry = sQry & Val(dTotalCost) & ","
            sQry = sQry & fncFormatSQLText(sJobSheetNo) & ","
            sQry = sQry & fncFormatSQLText(sPurchaseBillNo) & ","
            sQry = sQry & fncFormatSQLDate(sPurchaseDate) & ","
            sQry = sQry & fncFormatSQLText(sSupplierCode) & ","
            sQry = sQry & fncFormatSQLText(sSalesBillNo) & ","
            sQry = sQry & fncFormatSQLDate(sSalesDate) & ","
            sQry = sQry & Val(dSalesCostPerGram) & ","
            sQry = sQry & Val(dSalesMC) & ","
            sQry = sQry & Val(dSalesAmount) & ","
            sQry = sQry & fncFormatSQLText(sCategoryCode) & ","
            sQry = sQry & fncFormatSQLText(sClassCode) & ","
            sQry = sQry & fncFormatSQLText(sCatalogueCode) & ","
            sQry = sQry & fncFormatSQLText(sTrayId) & ","
            sQry = sQry & fncFormatSQLText(sPMCType) & ","
            sQry = sQry & fncFormatSQLText(sSMCType) & ","
            sQry = sQry & Val(lQtyOnHand) & ","
            sQry = sQry & Val(lQtySold) & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & Val(sSizeCode) & ","
            sQry = sQry & fncFormatSQLText(sSizeType) & ","
            sQry = sQry & fncFormatSQLText(sUserID) & ","
            sQry = sQry & fncFormatSQLText(gthisLocation)

            SaveStatus = dm.ProjExecNonQry(sQry)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            InventorySave = SaveStatus
            sQry = Nothing
        End Try
    End Function

    Public Function SplitInventorySave(ByVal sInventoryCode As String, ByVal sDescription As String, ByVal dWeight As Double, _
        ByVal dOriginalPMC As Double, ByVal dOriginalSMC As Double, ByVal dOriginalGoldLoss As Double, ByVal dOriginalTotalCost As Double, _
        ByVal dSourceWeight As Double, ByVal sDestInventoryCode As String, ByVal sDestDescription As String, _
        ByVal dDestWeight As Double, ByVal dPMC As Double, ByVal dSMC As Double, ByVal dGoldLoss As Double, _
        ByVal dTotalCost As Double, ByVal sOrgInventoryCode As String, ByVal dStoneWeight As Double, ByVal dSourceStoneWeight As Double, ByVal dDestStoneWeight As Double) As Boolean

        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = sQry & "EXEC sp_SplitHistory " & fncFormatSQLText(sInventoryCode) & ","
            sQry = sQry & fncFormatSQLText(sDescription) & ","
            sQry = sQry & Val(dWeight) & ","
            sQry = sQry & Val(dOriginalPMC) & ","
            sQry = sQry & Val(dOriginalSMC) & ","
            sQry = sQry & Val(dOriginalGoldLoss) & ","
            sQry = sQry & Val(dOriginalTotalCost) & ","
            sQry = sQry & Val(dSourceWeight) & ","
            sQry = sQry & fncFormatSQLText(sDestInventoryCode) & ","
            sQry = sQry & fncFormatSQLText(sDestDescription) & ","
            sQry = sQry & Val(dDestWeight) & ","
            sQry = sQry & Val(dPMC) & ","
            sQry = sQry & Val(dSMC) & ","
            sQry = sQry & Val(dGoldLoss) & ","
            sQry = sQry & Val(dTotalCost) & ","
            sQry = sQry & fncFormatSQLText(sUserID) & ","
            sQry = sQry & fncFormatSQLText(gthisLocation) & ","
            sQry = sQry & fncFormatSQLText(sOrgInventoryCode) & ","
            sQry = sQry & Val(dStoneWeight) & ","
            sQry = sQry & Val(dSourceStoneWeight) & ","
            sQry = sQry & Val(dDestStoneWeight)

            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            SplitInventorySave = SaveStatus
            sQry = Nothing
        End Try
    End Function
    Public Function SplitTradeInInventorySave(ByVal sInventoryCode As String, ByVal sPurity As String, ByVal dWeight As Double, _
        ByVal dOriginalTotalCost As Double, _
        ByVal dSourceWeight As Double, ByVal sDestInventoryCode As String, _
        ByVal dDestWeight As Double, _
        ByVal dTotalCost As Double) As Boolean

        Dim sQry As String
        Dim SaveStatus As Boolean
        Try


            sQry = sQry & "EXEC sp_SplitTradeInHistory " & fncFormatSQLText(sInventoryCode) & ","
            sQry = sQry & fncFormatSQLText(sPurity) & ","
            sQry = sQry & Val(dWeight) & ","
            sQry = sQry & Val(dOriginalTotalCost) & ","
            sQry = sQry & Val(dSourceWeight) & ","
            sQry = sQry & fncFormatSQLText(sDestInventoryCode) & ","
            sQry = sQry & Val(dDestWeight) & ","
            sQry = sQry & Val(dTotalCost) & ","
            sQry = sQry & fncFormatSQLText(sUserID)

            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            SplitTradeInInventorySave = SaveStatus
            sQry = Nothing
        End Try
    End Function
    Public Function InventorySaveHQ(ByVal sInventoryCode As String, ByVal sDescription As String, ByVal sDiamondDescription As String, _
        ByVal dWeight As Double, ByVal dStoneWeight As Double, ByVal dCostperGram As Double, ByVal dPMC As Double, ByVal dSMC As Double, ByVal dPCS As Double, _
        ByVal dGoldLoss As Double, ByVal dTotalCost As Double, ByVal sJobSheetNo As String, ByVal sPurchaseBillNo As String, ByVal sPurchaseDate As String, _
        ByVal sSupplierCode As String, ByVal sSalesBillNo As String, ByVal sSalesDate As String, ByVal dSalesCostPerGram As Double, ByVal dSalesMC As Double, _
        ByVal dSalesAmount As Double, ByVal sCategoryCode As String, ByVal sClassCode As String, ByVal sCatalogueCode As String, ByVal sTrayId As String, _
        ByVal sPMCType As String, ByVal sSMCType As String, ByVal lQtyOnHand As Long, ByVal lQtySold As Long, ByVal sRemarks As String, ByVal sSizeCode As String, ByVal sSizeType As String) As Boolean

        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = sQry & "EXEC sp_Inventory " & fncFormatSQLText(sInventoryCode) & ","
            sQry = sQry & fncFormatSQLText(sDescription) & ","
            sQry = sQry & fncFormatSQLText(sDiamondDescription) & ","
            sQry = sQry & Val(dWeight) & ","
            sQry = sQry & Val(dStoneWeight) & ","
            sQry = sQry & Val(dCostperGram) & ","
            sQry = sQry & Val(dPMC) & ","
            sQry = sQry & Val(dSMC) & ","
            sQry = sQry & Val(dPCS) & ","
            sQry = sQry & Val(dGoldLoss) & ","
            sQry = sQry & Val(dTotalCost) & ","
            sQry = sQry & fncFormatSQLText(sJobSheetNo) & ","
            sQry = sQry & fncFormatSQLText(sPurchaseBillNo) & ","
            sQry = sQry & fncFormatSQLDate(sPurchaseDate) & ","
            sQry = sQry & fncFormatSQLText(sSupplierCode) & ","
            sQry = sQry & fncFormatSQLText(sSalesBillNo) & ","
            sQry = sQry & fncFormatSQLDate(sSalesDate) & ","
            sQry = sQry & Val(dSalesCostPerGram) & ","
            sQry = sQry & Val(dSalesMC) & ","
            sQry = sQry & Val(dSalesAmount) & ","
            sQry = sQry & fncFormatSQLText(sCategoryCode) & ","
            sQry = sQry & fncFormatSQLText(sClassCode) & ","
            sQry = sQry & fncFormatSQLText(sCatalogueCode) & ","
            sQry = sQry & fncFormatSQLText(sTrayId) & ","
            sQry = sQry & fncFormatSQLText(sPMCType) & ","
            sQry = sQry & fncFormatSQLText(sSMCType) & ","
            sQry = sQry & Val(lQtyOnHand) & ","
            sQry = sQry & Val(lQtySold) & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & Val(sSizeCode) & ","
            sQry = sQry & fncFormatSQLText(sSizeType) & ","
            sQry = sQry & fncFormatSQLText(sUserID) & ","
            sQry = sQry & fncFormatSQLText(gthisLocation)
            '   SaveStatus = dm.ProjExecNonQryAcc(sQry)
            ' sCreateInventory = dm.ValReturnProjExecNonQryAcc(sQry)
            sCreateInventory = dm.ReturnProjExecNonQryAcc(sQry)
            SaveStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            InventorySaveHQ = SaveStatus
            sQry = Nothing
        End Try
    End Function

    Public Function SplitInventorySaveHQ(ByVal sInventoryCode As String, ByVal sDescription As String, ByVal dWeight As Double, _
        ByVal dOriginalPMC As Double, ByVal dOriginalSMC As Double, ByVal dOriginalGoldLoss As Double, ByVal dOriginalTotalCost As Double, _
        ByVal dSourceWeight As Double, ByVal sDestInventoryCode As String, ByVal sDestDescription As String, _
        ByVal dDestWeight As Double, ByVal dPMC As Double, ByVal dSMC As Double, ByVal dGoldLoss As Double, ByVal dTotalCost As Double, ByVal sOrgInventoryCode As String) As Boolean

        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = sQry & "EXEC sp_SplitHistory " & fncFormatSQLText(sInventoryCode) & ","
            sQry = sQry & fncFormatSQLText(sDescription) & ","
            sQry = sQry & Val(dWeight) & ","
            sQry = sQry & Val(dOriginalPMC) & ","
            sQry = sQry & Val(dOriginalSMC) & ","
            sQry = sQry & Val(dOriginalGoldLoss) & ","
            sQry = sQry & Val(dOriginalTotalCost) & ","
            sQry = sQry & Val(dSourceWeight) & ","
            sQry = sQry & fncFormatSQLText(sDestInventoryCode) & ","
            sQry = sQry & fncFormatSQLText(sDestDescription) & ","
            sQry = sQry & Val(dDestWeight) & ","
            sQry = sQry & Val(dPMC) & ","
            sQry = sQry & Val(dSMC) & ","
            sQry = sQry & Val(dGoldLoss) & ","
            sQry = sQry & Val(dTotalCost) & ","
            sQry = sQry & fncFormatSQLText(sUserID) & ","
            sQry = sQry & fncFormatSQLText(gthisLocation) & ","
            sQry = sQry & fncFormatSQLText(sOrgInventoryCode)

            SaveStatus = dm.ProjExecNonQryAcc(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            SplitInventorySaveHQ = SaveStatus
            sQry = Nothing
        End Try
    End Function
    Public Function PurchaseSave(ByVal sJobSheetNo As String, ByVal sPurchaseBillNo As String, ByVal sDirectPurchaseNo As String, _
        ByVal sSuppllierCode As String, ByVal sInventoryCode As String, ByVal sDescription As String, ByVal sDiamondDescription As String, _
        ByVal dStoneWeight As Double, ByVal dCostPerGram As Double, ByVal dWeight As Double, ByVal dMC As Double, ByVal dPCS As Double, _
        ByVal dGoldLoss As Double, ByVal dTotalCost As Double, ByVal sClassCode As String, ByVal sCategoryCode As String, _
        ByVal sCatalogueCode As String, ByVal sTrayId As String, ByVal sPurchaseType As String, ByVal sRemarks As String) As Boolean

        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = sQry & "EXEC sp_Purchase " & fncFormatSQLText(sJobSheetNo) & ","
            sQry = sQry & fncFormatSQLText(sPurchaseBillNo) & ","
            sQry = sQry & fncFormatSQLText(sDirectPurchaseNo) & ","
            sQry = sQry & fncFormatSQLText(sSuppllierCode) & ","
            sQry = sQry & fncFormatSQLText(sInventoryCode) & ","
            sQry = sQry & fncFormatSQLText(sDescription) & ","
            sQry = sQry & fncFormatSQLText(sDiamondDescription) & ","
            sQry = sQry & Val(dStoneWeight) & ","
            sQry = sQry & Val(dCostPerGram) & ","
            sQry = sQry & Val(dWeight) & ","
            sQry = sQry & Val(dMC) & ","
            sQry = sQry & Val(dPCS) & ","
            sQry = sQry & Val(dGoldLoss) & ","
            sQry = sQry & Val(dTotalCost) & ","
            sQry = sQry & fncFormatSQLText(sCatalogueCode) & ","
            sQry = sQry & fncFormatSQLText(sCategoryCode) & ","
            sQry = sQry & fncFormatSQLText(sClassCode) & ","
            sQry = sQry & fncFormatSQLText(sTrayId) & ","
            sQry = sQry & fncFormatSQLText(sPurchaseType) & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & fncFormatSQLText(gthisLocation) & ","
            sQry = sQry & fncFormatSQLText(sUserID) & ""

            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            PurchaseSave = SaveStatus
            sQry = Nothing
        End Try
    End Function


#End Region


    Public Function InventorySaveML(ByVal sInventoryCode As String, ByVal sDescription As String, ByVal sDiamondDescription As String, _
           ByVal dSize As Double, ByVal dWeight As Double, ByVal dStoneWeight As Double, ByVal dCostperGram As Double, ByVal dPMC As Double, ByVal dSMC As Double, ByVal dPCS As Double, _
           ByVal dGoldLoss As Double, ByVal dTotalCost As Double, ByVal sJobSheetNo As String, ByVal sPurchaseBillNo As String, ByVal sPurchaseDate As String, _
           ByVal sSupplierCode As String, ByVal sSalesBillNo As String, ByVal sSalesDate As String, ByVal dSalesCostPerGram As Double, ByVal dSalesMC As Double, _
           ByVal dSalesAmount As Double, ByVal sCategoryCode As String, ByVal sClassCode As String, ByVal sCatalogueCode As String, ByVal sTrayId As String, _
           ByVal sPMCType As String, ByVal sSMCType As String, ByVal lQtyOnHand As Long, ByVal lQtySold As Long, ByVal sRemarks As String) As Boolean

        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = sQry & "EXEC sp_InventoryML " & fncFormatSQLText(sInventoryCode) & ","
            sQry = sQry & fncFormatSQLText(sDescription) & ","
            sQry = sQry & fncFormatSQLText(sDiamondDescription) & ","
            sQry = sQry & Val(dSize) & ","
            sQry = sQry & Val(dWeight) & ","
            sQry = sQry & Val(dStoneWeight) & ","
            sQry = sQry & Val(dCostperGram) & ","
            sQry = sQry & Val(dPMC) & ","
            sQry = sQry & Val(dSMC) & ","
            sQry = sQry & Val(dPCS) & ","
            sQry = sQry & Val(dGoldLoss) & ","
            sQry = sQry & Val(dTotalCost) & ","
            sQry = sQry & fncFormatSQLText(sJobSheetNo) & ","
            sQry = sQry & fncFormatSQLText(sPurchaseBillNo) & ","
            sQry = sQry & fncFormatSQLDate(sPurchaseDate) & ","
            sQry = sQry & fncFormatSQLText(sSupplierCode) & ","
            sQry = sQry & fncFormatSQLText(sSalesBillNo) & ","
            sQry = sQry & fncFormatSQLDate(sSalesDate) & ","
            sQry = sQry & Val(dSalesCostPerGram) & ","
            sQry = sQry & Val(dSalesMC) & ","
            sQry = sQry & Val(dSalesAmount) & ","
            sQry = sQry & fncFormatSQLText(sCategoryCode) & ","
            sQry = sQry & fncFormatSQLText(sClassCode) & ","
            sQry = sQry & fncFormatSQLText(sCatalogueCode) & ","
            sQry = sQry & fncFormatSQLText(sTrayId) & ","
            sQry = sQry & fncFormatSQLText(sPMCType) & ","
            sQry = sQry & fncFormatSQLText(sSMCType) & ","
            sQry = sQry & Val(lQtyOnHand) & ","
            sQry = sQry & Val(lQtySold) & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & fncFormatSQLText(sUserID) & ","
            sQry = sQry & fncFormatSQLText(gthisLocation)

            SaveStatus = dm.ProjExecNonQry(sQry)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            InventorySaveML = SaveStatus
            sQry = Nothing
        End Try
    End Function
    Public Function SplitInventorySaveML(ByVal sInventoryCode As String, ByVal sDescription As String, ByVal dSize As Double, ByVal dWeight As Double, _
        ByVal dOriginalPMC As Double, ByVal dOriginalSMC As Double, ByVal dOriginalGoldLoss As Double, ByVal dOriginalTotalCost As Double, _
        ByVal dSourceSize As Double, ByVal dSourceWeight As Double, ByVal sDestInventoryCode As String, ByVal sDestDescription As String, _
        ByVal dDestSize As Double, ByVal dDestWeight As Double, ByVal dPMC As Double, ByVal dSMC As Double, ByVal dGoldLoss As Double, ByVal dTotalCost As Double) As Boolean

        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = sQry & "EXEC sp_SplitHistoryML " & fncFormatSQLText(sInventoryCode) & ","
            sQry = sQry & fncFormatSQLText(sDescription) & ","
            sQry = sQry & Val(dSize) & ","
            sQry = sQry & Val(dWeight) & ","
            sQry = sQry & Val(dOriginalPMC) & ","
            sQry = sQry & Val(dOriginalSMC) & ","
            sQry = sQry & Val(dOriginalGoldLoss) & ","
            sQry = sQry & Val(dOriginalTotalCost) & ","
            sQry = sQry & Val(dSourceSize) & ","
            sQry = sQry & Val(dSourceWeight) & ","
            sQry = sQry & fncFormatSQLText(sDestInventoryCode) & ","
            sQry = sQry & fncFormatSQLText(sDestDescription) & ","
            sQry = sQry & Val(dDestSize) & ","
            sQry = sQry & Val(dDestWeight) & ","
            sQry = sQry & Val(dPMC) & ","
            sQry = sQry & Val(dSMC) & ","
            sQry = sQry & Val(dGoldLoss) & ","
            sQry = sQry & Val(dTotalCost) & ","
            sQry = sQry & fncFormatSQLText(sUserID)

            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            SplitInventorySaveML = SaveStatus
            sQry = Nothing
        End Try
    End Function
    Public Function fncSplitIPMInsert(ByVal sInventoryCode As String, ByVal sSplitInventoryCode As String) As Boolean
        Dim sExec As String
        Try
            fncSplitIPMInsert = False
            sExec = "INSERT into tblInventoryBarCoin" & vbCrLf
            sExec = sExec & "Select " & fncFormatSQLText(sSplitInventoryCode) & ",MetalType,GstType,Purity,Refiner,Country,CreateUser,CreateDate,ModifyUser,ModifyDate,BarNo FROM tblInventoryBarCoin  WHERE InventoryCode =" & fncFormatSQLText(sInventoryCode)
            dm.ProjExecNonQry(sExec)
            fncSplitIPMInsert = True
        Catch ex As Exception
            fncSplitIPMInsert = False
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Function
    Public Function RFIDItemWtMCUpdate(ByVal sInventorycode As String, ByVal dWeight As Double, ByVal dSMC As Double) As Boolean
        Dim bStatus As Boolean
        Dim sQry As String
        Try
            'sQry = "update Item set Weight= " & Val(dWeight) & ", PMC=" & Val(dWeight) & " * PMC / (Weight) , SMC=" & Val(dWeight) & " * sMc / (Weight)  from Item Where ItemCode=" & fncFormatSQLText(sInventorycode)

            sQry = "update Item set Weight = " & Val(dWeight) & " ,SMC = " & Val(dSMC) & " , status='SP' Where ItemCode=" & fncFormatSQLText(sInventorycode)
            bStatus = ProjExecNonQryRFID(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            RFIDItemWtMCUpdate = bStatus
            sQry = Nothing
        End Try
    End Function
    Public Shared Function ProjExecNonQryRFID(ByVal Query As String) As Boolean
        ProjExecNonQryRFID = True
        Try
            Dim Cn As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(gsConRFID)
            Cn.Open()
            Dim Cmd As New SqlClient.SqlCommand(Query)
            Cmd.Connection = Cn
            Cmd.CommandTimeout = 600
            Cmd.ExecuteNonQuery()
            Cn.Close()
            Cn.Dispose()
            Cn = Nothing
        Catch ex As Exception
            Try
                MsgBox("Error in Exec Record")
                ProjExecNonQryRFID = False
            Catch
            End Try
            Throw
            MsgBox("unexpected error")
            ProjExecNonQryRFID = False
        End Try
    End Function
End Class



