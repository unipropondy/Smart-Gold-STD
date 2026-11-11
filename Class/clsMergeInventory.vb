Public Class clsMergeInventory
    Dim dm As New DataManager
#Region "Fetch Category"

    Public Function GetCategory(ByVal sClasscode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select * from tblCategory  where classcode = " & fncFormatSQLText(sClasscode)
            'sQry = " select * from tblCategory "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "tblCategory", "tblCategory")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetCategory = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetCatalogue(ByVal sCatagoryCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select * from tblCatalogue  where CategoryCode = " & fncFormatSQLText(sCatagoryCode)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "tblCategory", "tblCategory")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetCatalogue = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function GetMergeCategory(ByVal sClasscode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select * from tblCategory  "
            'sQry = " select * from tblCategory "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "tblCategory", "tblCategory")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetMergeCategory = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
#End Region

#Region "Purchase Updation"
    Public Function PurchaseSaveML(ByVal sJobSheetNo As String, ByVal sPurchaseBillNo As String, ByVal sDirectPurchaseNo As String, _
       ByVal sSuppllierCode As String, ByVal sInventoryCode As String, ByVal sDescription As String, ByVal sDiamondDescription As String, _
       ByVal dStoneWeight As Double, ByVal dCostPerGram As Double, ByVal dSize As Double, ByVal dWeight As Double, ByVal dMC As Double, ByVal dPCS As Double, _
       ByVal dGoldLoss As Double, ByVal dTotalCost As Double, ByVal sClassCode As String, ByVal sCategoryCode As String, _
       ByVal sCatalogueCode As String, ByVal sTrayId As String, ByVal sPurchaseType As String, ByVal sRemarks As String) As Boolean

        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = sQry & "EXEC sp_PurchaseML " & fncFormatSQLText(sJobSheetNo) & ","
            sQry = sQry & fncFormatSQLText(sPurchaseBillNo) & ","
            sQry = sQry & fncFormatSQLText(sDirectPurchaseNo) & ","
            sQry = sQry & fncFormatSQLText(sSuppllierCode) & ","
            sQry = sQry & fncFormatSQLText(sInventoryCode) & ","
            sQry = sQry & fncFormatSQLText(sDescription) & ","
            sQry = sQry & fncFormatSQLText(sDiamondDescription) & ","
            sQry = sQry & Val(dStoneWeight) & ","
            sQry = sQry & Val(dCostPerGram) & ","
            sQry = sQry & Val(dSize) & ","
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
            PurchaseSaveML = SaveStatus
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

#Region "Inventory merged Updation"

    Public Function InventoryMergedUptation(ByVal sInventorycode As String, ByVal sNewInventoryCode As String, ByVal sQtyOnHand As Double, ByVal sRemarks As String) As Boolean

        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = sQry & "EXEC sp_InventoryMergedUptation " & fncFormatSQLText(sInventorycode) & ","
            sQry = sQry & fncFormatSQLText(sNewInventoryCode) & ","
            sQry = sQry & fncFormatSQLText(sQtyOnHand) & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & fncFormatSQLText(gthisLocation) & ","
            sQry = sQry & fncFormatSQLText(sUserID) & ""
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            InventoryMergedUptation = SaveStatus
            sQry = Nothing
        End Try
    End Function


#End Region


#Region " To Check Inventory "

    Public Function IsInventory(ByVal sInventoryCode As String) As Boolean
        Dim Ds As New DataSet
        Dim sQry As String
        Dim bIsInventory As Boolean
        Try
            bIsInventory = False
            sQry = " select count(Inventorycode) InvCount from tblinventory where Inventorycode = " & fncFormatSQLText(sInventoryCode)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "tblinventory", "tblinventory")
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

#Region "Get Inventory Detail"

    Public Function GetInventoryDetail(ByVal sInventoryCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "SELECT *,Pdate = CONVERT(CHAR(8),PurchaseDate,112) FROM vwInventoryStock WHERE InventoryCode =" & fncFormatSQLText(sInventoryCode) & " and QtySold = 0 and PCS = 1 and QtyOnHand <> 0 "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "tblInventory", "tblInventory")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetInventoryDetail = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

#End Region
#Region " Merged Inventory  History save ML"



    Public Function MergeInvHistorySaveML(ByVal sInventoryCode1 As String, _
       ByVal dSize1 As Double, ByVal dWeight1 As Double, ByVal dPMC1 As Double, ByVal dSMC1 As Double, _
       ByVal dGoldLoss1 As Double, ByVal dTotalCost1 As Double, ByVal sInventoryCode2 As String, _
       ByVal dSize2 As Double, ByVal dWeight2 As Double, ByVal dPMC2 As Double, ByVal dSMC2 As Double, _
       ByVal dGoldLoss2 As Double, ByVal dTotalCost2 As Double, ByVal sInventoryCode As String, _
       ByVal dSize As Double, ByVal dWeight As Double, ByVal dPMC As Double, ByVal dSMC As Double, _
       ByVal dGoldLoss As Double, ByVal dTotalCost As Double, _
       ByVal sDescriptionNew As String, ByVal sJobSheetNo As String, ByVal sClassCode As String) As Boolean

        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = sQry & "EXEC sp_MergeInvHistorySaveML " & fncFormatSQLText(sInventoryCode1) & ","
            sQry = sQry & Val(dSize1) & ","
            sQry = sQry & Val(dWeight1) & ","
            sQry = sQry & Val(dPMC1) & ","
            sQry = sQry & Val(dSMC1) & ","
            sQry = sQry & Val(dGoldLoss1) & ","
            sQry = sQry & Val(dTotalCost1) & ","
            sQry = sQry & fncFormatSQLText(sInventoryCode2) & ","
            sQry = sQry & Val(dSize2) & ","
            sQry = sQry & Val(dWeight2) & ","
            sQry = sQry & Val(dPMC2) & ","
            sQry = sQry & Val(dSMC2) & ","
            sQry = sQry & Val(dGoldLoss2) & ","
            sQry = sQry & Val(dTotalCost2) & ","
            sQry = sQry & fncFormatSQLText(sInventoryCode) & ","
            sQry = sQry & Val(dSize) & ","
            sQry = sQry & Val(dWeight) & ","
            sQry = sQry & Val(dPMC) & ","
            sQry = sQry & Val(dSMC) & ","
            sQry = sQry & Val(dGoldLoss) & ","
            sQry = sQry & Val(dTotalCost) & ","
            sQry = sQry & fncFormatSQLText(sDescriptionNew) & ","
            sQry = sQry & fncFormatSQLDate(sJobSheetNo) & ","
            sQry = sQry & fncFormatSQLText(sClassCode) & ","
            sQry = sQry & fncFormatSQLText(sUserID) & ","
            sQry = sQry & fncFormatSQLText(gthisLocation)

            SaveStatus = dm.ProjExecNonQry(sQry)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            MergeInvHistorySaveML = SaveStatus
            sQry = Nothing
        End Try
    End Function

#End Region

#Region " Inventory save"

    Public Function InventorySave(ByVal sInventoryCode As String, ByVal sDescription As String, ByVal sDiamondDescription As String, _
       ByVal dWeight As Double, ByVal dStoneWeight As Double, ByVal dCostperGram As Double, ByVal dPMC As Double, ByVal dSMC As Double, ByVal dPCS As Double, _
       ByVal dGoldLoss As Double, ByVal dTotalCost As Double, ByVal sJobSheetNo As String, ByVal sPurchaseBillNo As String, ByVal sPurchaseDate As String, _
       ByVal sSupplierCode As String, ByVal sSalesBillNo As String, ByVal sSalesDate As String, ByVal dSalesCostPerGram As Double, ByVal dSalesMC As Double, _
       ByVal dSalesAmount As Double, ByVal sCategoryCode As String, ByVal sClassCode As String, ByVal sCatalogueCode As String, ByVal sTrayId As String, _
       ByVal sPMCType As String, ByVal sSMCType As String, ByVal lQtyOnHand As Long, ByVal lQtySold As Long, ByVal sRemarks As String, _
        ByVal sSizeCode As String, ByVal sSizeType As String) As Boolean

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
    Public Function InventorySaveHQ(ByVal sInventoryCode As String, ByVal sDescription As String, ByVal sDiamondDescription As String, _
       ByVal dWeight As Double, ByVal dStoneWeight As Double, ByVal dCostperGram As Double, ByVal dPMC As Double, ByVal dSMC As Double, ByVal dPCS As Double, _
       ByVal dGoldLoss As Double, ByVal dTotalCost As Double, ByVal sJobSheetNo As String, ByVal sPurchaseBillNo As String, ByVal sPurchaseDate As String, _
       ByVal sSupplierCode As String, ByVal sSalesBillNo As String, ByVal sSalesDate As String, ByVal dSalesCostPerGram As Double, ByVal dSalesMC As Double, _
       ByVal dSalesAmount As Double, ByVal sCategoryCode As String, ByVal sClassCode As String, ByVal sCatalogueCode As String, ByVal sTrayId As String, _
       ByVal sPMCType As String, ByVal sSMCType As String, ByVal lQtyOnHand As Long, ByVal lQtySold As Long, ByVal sRemarks As String, _
        ByVal sSizeCode As String, ByVal sSizeType As String) As Boolean

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

            SaveStatus = dm.ProjExecNonQryAcc(sQry)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            InventorySaveHQ = SaveStatus
            sQry = Nothing
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
            bSaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        Finally
            fncInventoryMovementSave = bSaveStatus
        End Try
    End Function
    Public Function fncInventoryMovementSaveHQ(ByVal sLocationCode As String, ByVal sInventorycode As String, ByVal sDescription As String, ByVal sStatus As String, ByVal sRefNumber As String, _
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
            bSaveStatus = dm.ProjExecNonQryAcc(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        Finally
            fncInventoryMovementSaveHQ = bSaveStatus
        End Try
    End Function
#End Region


#Region " Merged Inventory  History save"

    Public Function MergeInvHistorySave(ByVal sInventoryCode1 As String, _
       ByVal dWeight1 As Double, ByVal dPMC1 As Double, ByVal dSMC1 As Double, _
       ByVal dGoldLoss1 As Double, ByVal dTotalCost1 As Double, ByVal sInventoryCode2 As String, _
       ByVal dWeight2 As Double, ByVal dPMC2 As Double, ByVal dSMC2 As Double, _
       ByVal dGoldLoss2 As Double, ByVal dTotalCost2 As Double, ByVal sInventoryCode As String, _
       ByVal dWeight As Double, ByVal dPMC As Double, ByVal dSMC As Double, _
       ByVal dGoldLoss As Double, ByVal dTotalCost As Double, _
       ByVal sDescriptionNew As String, ByVal sJobSheetNo As String, ByVal sClassCode As String) As Boolean

        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = sQry & "EXEC sp_MergeInvHistorySave " & fncFormatSQLText(sInventoryCode1) & ","
            sQry = sQry & Val(dWeight1) & ","
            sQry = sQry & Val(dPMC1) & ","
            sQry = sQry & Val(dSMC1) & ","
            sQry = sQry & Val(dGoldLoss1) & ","
            sQry = sQry & Val(dTotalCost1) & ","
            sQry = sQry & fncFormatSQLText(sInventoryCode2) & ","
            sQry = sQry & Val(dWeight2) & ","
            sQry = sQry & Val(dPMC2) & ","
            sQry = sQry & Val(dSMC2) & ","
            sQry = sQry & Val(dGoldLoss2) & ","
            sQry = sQry & Val(dTotalCost2) & ","
            sQry = sQry & fncFormatSQLText(sInventoryCode) & ","
            sQry = sQry & Val(dWeight) & ","
            sQry = sQry & Val(dPMC) & ","
            sQry = sQry & Val(dSMC) & ","
            sQry = sQry & Val(dGoldLoss) & ","
            sQry = sQry & Val(dTotalCost) & ","
            sQry = sQry & fncFormatSQLText(sDescriptionNew) & ","
            sQry = sQry & fncFormatSQLText(sJobSheetNo) & ","
            sQry = sQry & fncFormatSQLText(sClassCode) & ","
            sQry = sQry & fncFormatSQLText(sUserID) & ","
            sQry = sQry & fncFormatSQLText(gthisLocation)

            SaveStatus = dm.ProjExecNonQry(sQry)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            MergeInvHistorySave = SaveStatus
            sQry = Nothing
        End Try
    End Function

#End Region


#Region "Get Gategory for class"

    Public Function GetDetailForCategory(ByVal sCategoryCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "SELECT * FROM tblCategory WHERE CategoryCode =" & fncFormatSQLText(sCategoryCode)
            Ds = dm.getDataSetWithSqlAdapterAcc(sQry, "tblCategory", "tblCategory")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetDetailForCategory = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetScrapInventory() As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "SELECT * FROM tblCompany "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "tblCompany", "tblCompany")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetScrapInventory = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetScrapInventoryNo() As String
        Dim Ds As New DataSet
        Dim sQry As String
        Dim sNextOrderNo As String
        Try
            sQry = " Select rtrim(locationcode) + Replicate('0',6-Len(Convert(char,NextScrapInventoryNo)))+ convert(char,NextScrapInventoryNo) NextScrapInventoryNo from tblCompany "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "ONo", "ONo")
            If Ds.Tables("ONo").Rows.Count > 0 Then
                sNextOrderNo = Ds.Tables("ONo").Rows(0)("NextScrapInventoryNo")
            End If
            'sQry = "Update   tblCompany Set NextScrapInventoryNo = NextScrapInventoryNo + 1 "
            'dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            sNextOrderNo = ""
        Finally
            GetScrapInventoryNo = sNextOrderNo
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

#End Region
#Region " Inventory save For ML"

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

#End Region


End Class
