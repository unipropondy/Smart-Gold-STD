Public Class clsInventory
    Dim dm As New DataManager
#Region "InventoryManagement Save ML"
    Public Function InventoryMntML(ByVal sinventorycode As String, ByVal sOldDescription As String, _
                               ByVal sOldDiamondDescription As String, ByVal nOldSize As Double, ByVal sOldPMCType As String, _
                               ByVal sOldSMCType As String, ByVal nOldPMC As Double, _
                               ByVal nOldSMC As Double, ByVal nOldTotalCost As Double, _
                               ByVal nOldGoldLoss As Double, ByVal sNewDescription As String, _
                               ByVal sNewDiamondDescription As String, ByVal nNewSize As Double, ByVal sNewPMCType As String, _
                               ByVal sNewSMCType As String, ByVal nNewPMC As Double, _
                               ByVal nNewSMC As Double, ByVal nNewTotalCost As Double, _
                               ByVal nNewGoldLoss As Double, ByVal sUserId As String) As Boolean

        Try
            Dim sExec As String
            InventoryMntML = False
            sExec = "Begin Transaction" & vbCrLf
            sExec = sExec & "EXEC sp_UpdateInventoryML " & fncFormatSQLText(sinventorycode) & ","
            sExec = sExec & fncFormatSQLText(sOldDescription) & ","
            sExec = sExec & fncFormatSQLText(sOldDiamondDescription) & ","
            sExec = sExec & Val(nOldSize) & ","
            sExec = sExec & fncFormatSQLText(sOldPMCType) & ","
            sExec = sExec & fncFormatSQLText(sOldSMCType) & ","
            sExec = sExec & Val(nOldPMC) & ","
            sExec = sExec & Val(nOldSMC) & ","
            sExec = sExec & Val(nOldTotalCost) & ","
            sExec = sExec & Val(nOldGoldLoss) & ","
            sExec = sExec & fncFormatSQLText(sNewDescription) & ","
            sExec = sExec & fncFormatSQLText(sNewDiamondDescription) & ","
            sExec = sExec & Val(nNewSize) & ","
            sExec = sExec & fncFormatSQLText(sNewPMCType) & ","
            sExec = sExec & fncFormatSQLText(sNewSMCType) & ","
            sExec = sExec & Val(nNewPMC) & ","
            sExec = sExec & Val(nNewSMC) & ","
            sExec = sExec & Val(nNewTotalCost) & ","
            sExec = sExec & Val(nNewGoldLoss) & ","
            sExec = sExec & fncFormatSQLText(sUserId) & vbCrLf
            sExec = sExec & "Commit Transaction" & vbCrLf
            InventoryMntML = dm.ProjExecNonQry(sExec)
            Exit Function
        Catch ex As Exception
            InventoryMntML = False
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Function
#End Region

    
#Region "InventoryManagement Save"
    Public Function InventoryMnt(ByVal sinventorycode As String, ByVal sOldDescription As String, _
                               ByVal sOldDiamondDescription As String, ByVal sOldPMCType As String, _
                               ByVal sOldSMCType As String, ByVal nOldPMC As Double, _
                               ByVal nOldSMC As Double, ByVal nOldTotalCost As Double, _
                               ByVal nOldGoldLoss As Double, ByVal sNewDescription As String, _
                               ByVal sNewDiamondDescription As String, ByVal sNewPMCType As String, _
                               ByVal sNewSMCType As String, ByVal nNewPMC As Double, _
                               ByVal nNewSMC As Double, ByVal nNewTotalCost As Double, _
                               ByVal nNewGoldLoss As Double, _
                               ByVal nOldSizeCode As String, sOldSizeType As String, ByVal nNewSizeCode As String, _
                               ByVal sNewSizeType As String, _
                               ByVal sOldDesc1 As String, ByVal sOldDesc2 As String, ByVal sOldDesc3 As String, ByVal sOldDesc4 As String, _
                               ByVal nOldCost1 As Double, ByVal nOldCost2 As Double, ByVal nOldCost3 As Double, ByVal nOldCost4 As Double, _
                               ByVal nOldSellingPrice1 As Double, ByVal nOldSellingPrice2 As Double, ByVal nOldSellingPrice3 As Double, ByVal nOldSellingPrice4 As Double, _
                               ByVal sNewDesc1 As String, ByVal sNewDesc2 As String, ByVal sNewDesc3 As String, ByVal sNewDesc4 As String, _
                               ByVal nNewCost1 As Double, ByVal nNewCost2 As Double, ByVal nNewCost3 As Double, ByVal nNewCost4 As Double, _
                               ByVal nNewSellingPrice1 As Double, ByVal nNewSellingPrice2 As Double, ByVal nNewSellingPrice3 As Double, ByVal nNewSellingPrice4 As Double, _
                               ByVal sUserId As String) As Boolean

        Try
            Dim sExec As String
            InventoryMnt = False
            sExec = "Begin Transaction" & vbCrLf
            sExec = sExec & "EXEC sp_UpdateInventory " & fncFormatSQLText(sinventorycode) & ","
            sExec = sExec & fncFormatSQLText(sOldDescription) & ","
            sExec = sExec & fncFormatSQLText(sOldDiamondDescription) & ","
            sExec = sExec & fncFormatSQLText(sOldPMCType) & ","
            sExec = sExec & fncFormatSQLText(sOldSMCType) & ","
            sExec = sExec & Val(nOldPMC) & ","
            sExec = sExec & Val(nOldSMC) & ","
            sExec = sExec & Val(nOldTotalCost) & ","
            sExec = sExec & Val(nOldGoldLoss) & ","
            sExec = sExec & fncFormatSQLText(sNewDescription) & ","
            sExec = sExec & fncFormatSQLText(sNewDiamondDescription) & ","
            sExec = sExec & fncFormatSQLText(sNewPMCType) & ","
            sExec = sExec & fncFormatSQLText(sNewSMCType) & ","
            sExec = sExec & Val(nNewPMC) & ","
            sExec = sExec & Val(nNewSMC) & ","
            sExec = sExec & Val(nNewTotalCost) & ","
            sExec = sExec & Val(nNewGoldLoss) & ","
            sExec = sExec & Val(nOldSizeCode) & ","
            sExec = sExec & fncFormatSQLText(sOldSizeType) & ","
            sExec = sExec & Val(nNewSizeCode) & ","
            sExec = sExec & fncFormatSQLText(sNewSizeType) & ","
            sExec = sExec & fncFormatSQLText(sOldDesc1) & ","
            sExec = sExec & fncFormatSQLText(sOldDesc2) & ","
            sExec = sExec & fncFormatSQLText(sOldDesc3) & ","
            sExec = sExec & fncFormatSQLText(sOldDesc4) & ","
            sExec = sExec & Val(nOldCost1) & ","
            sExec = sExec & Val(nOldCost2) & ","
            sExec = sExec & Val(nOldCost3) & ","
            sExec = sExec & Val(nOldCost4) & ","
            sExec = sExec & Val(nOldSellingPrice1) & ","
            sExec = sExec & Val(nOldSellingPrice2) & ","
            sExec = sExec & Val(nOldSellingPrice3) & ","
            sExec = sExec & Val(nOldSellingPrice4) & ","
            sExec = sExec & fncFormatSQLText(sNewDesc1) & ","
            sExec = sExec & fncFormatSQLText(sNewDesc2) & ","
            sExec = sExec & fncFormatSQLText(sNewDesc3) & ","
            sExec = sExec & fncFormatSQLText(sNewDesc4) & ","
            sExec = sExec & Val(nNewCost1) & ","
            sExec = sExec & Val(nNewCost2) & ","
            sExec = sExec & Val(nNewCost3) & ","
            sExec = sExec & Val(nNewCost4) & ","
            sExec = sExec & Val(nNewSellingPrice1) & ","
            sExec = sExec & Val(nNewSellingPrice2) & ","
            sExec = sExec & Val(nNewSellingPrice3) & ","
            sExec = sExec & Val(nNewSellingPrice4) & ","
            sExec = sExec & fncFormatSQLText(sUserId) & vbCrLf
            sExec = sExec & "Commit Transaction" & vbCrLf
            InventoryMnt = dm.ProjExecNonQry(sExec)
            Exit Function
        Catch ex As Exception
            InventoryMnt = False
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Function
#End Region


#Region "Get InventoryRange"
    Public Function GetInventoryRange(ByVal frInventory As String, ByVal toInventory As String, ByVal sSupplierCode As String, _
                                    ByVal sPurchaseBillNo As String) As DataSet
        Dim ds As New DataSet
        Dim sSelect As String

        Try
            sSelect = "SELECT * FROM vwInventoryStock where inventorycode >= " & fncFormatSQLText(frInventory)
            sSelect = sSelect & " and InventoryCode <= " & fncFormatSQLText(toInventory)
            sSelect = sSelect & " and SupplierCode= " & fncFormatSQLText(sSupplierCode)
            sSelect = sSelect & " and PurchaseBillNo= " & fncFormatSQLText(sPurchaseBillNo)
            sSelect = sSelect & " and QtySold=0 and QtyOnHand>0 "
            sSelect = sSelect & " order by InventoryCode "

            ds = dm.getDataSetWithSqlAdapter(sSelect, "tblInventory", "tblInventory")
            GetInventoryRange = ds
            Exit Function
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
            ds = Nothing
        End Try
    End Function

#End Region


#Region "Get Mc Convertion"
    Public Function GetMcConvertion(ByVal dNo As Double) As DataSet
        Dim ds As New DataSet
        Dim sSql As String
        Try
            sSql = "EXEC sp_MCConversion " & Val(dNo)
            sSql = sSql & " , " & fncFormatSQLText("")
            ds = dm.getDataSetWithSqlAdapter(sSql, "sampletable", "SampleTable")
            GetMcConvertion = ds
            Exit Function
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
            ds = Nothing
        End Try
    End Function

#End Region

    '#Region "Get Class Detail"
    '    Public Function GetClassDetail(ByVal sClassCode As String) As DataSet
    '        Dim ds As New DataSet
    '        Dim sSql As String

    '        Try
    '            sSql = "SELECT * FROM tblClass WHERE ClassCode =" & fncFormatSQLText(sClassCode)
    '            ds = dm.getDataSetWithSqlAdapter(sSql, "tblInventory", "tblInventory")
    '            GetClassDetail = ds
    '            Exit Function
    '        Catch ex As Exception
    '            MsgBox(Err.Description)
    '        Finally
    '            Err.Clear()
    '            ds = Nothing
    '        End Try
    '    End Function

    '#End Region

    Public Function fncStock(ByVal sCategoryCode As String, ByVal sUserid As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_Stock " & fncFormatSQLText(sCategoryCode) & ","
            sQry = sQry & fncFormatSQLText(sUserid)
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            fncStock = SaveStatus
            sQry = Nothing
        End Try
    End Function
    Public Function fncPurchaseStock(ByVal sCategoryCode As String, ByVal sUserid As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_PurchaseStock " & fncFormatSQLText(sCategoryCode) & ","
            sQry = sQry & fncFormatSQLText(sUserid)
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            fncPurchaseStock = SaveStatus
            sQry = Nothing
        End Try
    End Function
    Public Function StockDelete() As Boolean
        Dim sExec As String
        Try
            StockDelete = False
            sExec = "BEGIN TRANSACTION" & vbCrLf
            sExec = sExec & "DELETE FROM tblStock "
            sExec = sExec & "COMMIT TRANSACTION" & vbCrLf
            dm.ProjExecNonQry(sExec)
            StockDelete = True
            Exit Function
        Catch ex As Exception
            StockDelete = False
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Function
    Public Function PurchaseStockDelete() As Boolean
        Dim sExec As String
        Try
            PurchaseStockDelete = False
            sExec = "BEGIN TRANSACTION" & vbCrLf
            sExec = sExec & "DELETE FROM tblPurchaseStock "
            sExec = sExec & "COMMIT TRANSACTION" & vbCrLf
            dm.ProjExecNonQry(sExec)
            PurchaseStockDelete = True
            Exit Function
        Catch ex As Exception
            PurchaseStockDelete = False
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Function
    Public Function StockDeleteForTrayX() As Boolean
        Dim sExec As String
        Try
            StockDeleteForTrayX = False
            sExec = "BEGIN TRANSACTION" & vbCrLf
            sExec = sExec & "DELETE FROM tblStock where TrayID='X' "
            sExec = sExec & "COMMIT TRANSACTION" & vbCrLf
            dm.ProjExecNonQry(sExec)
            StockDeleteForTrayX = True
            Exit Function
        Catch ex As Exception
            StockDeleteForTrayX = False
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Function
    Public Function UpdateStockTake() As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_UpdateStockTake "
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            UpdateStockTake = SaveStatus
            sQry = Nothing
        End Try
    End Function
    Public Function UpdateModifyUser(ByVal sCategoryCode As String, ByVal sUserid As String) As Boolean
        Dim sExec As String
        Try
            UpdateModifyUser = False
            If sCategoryCode = "ALL" Then
                sExec = "Update tblStock set ModifyUser=" & fncFormatSQLText(sUserid)
                sExec = sExec & " ,ModifyDate=Getdate()"
            Else
                sExec = "Update tblStock set ModifyUser=" & fncFormatSQLText(sUserid)
                sExec = sExec & " ,ModifyDate=Getdate()"
                sExec = sExec & " where CategoryCode =" & fncFormatSQLText(sCategoryCode)
            End If
            dm.ProjExecNonQry(sExec)
            UpdateModifyUser = True
            Exit Function
        Catch ex As Exception
            UpdateModifyUser = False
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Function
    Public Function UpdatePurchaseModifyUser(ByVal sCategoryCode As String, ByVal sUserid As String) As Boolean
        Dim sExec As String
        Try
            UpdatePurchaseModifyUser = False
            If sCategoryCode = "ALL" Then
                sExec = "Update tblPurchaseStock set ModifyUser=" & fncFormatSQLText(sUserid)
                sExec = sExec & " ,ModifyDate=Getdate()"
            Else
                sExec = "Update tblPurchaseStock set ModifyUser=" & fncFormatSQLText(sUserid)
                sExec = sExec & " ,ModifyDate=Getdate()"
                sExec = sExec & " where JobsheetNo =" & fncFormatSQLText(sCategoryCode)
            End If
            dm.ProjExecNonQry(sExec)
            UpdatePurchaseModifyUser = True
            Exit Function
        Catch ex As Exception
            UpdatePurchaseModifyUser = False
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Function
    Public Function GetTray(ByVal sTrayID As String) As DataSet
        Dim Ds As New DataSet
        Dim sExec As String
        Try
            sExec = " select * from tblStock  where TrayID=" & fncFormatSQLText(sTrayID)
            sExec = sExec & " AND TrayID <>'X'"
            Ds = dm.getDataSetWithSqlAdapter(sExec, "Tray", "Tray")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetTray = Ds
            Ds = Nothing
            sExec = Nothing
        End Try
    End Function
    Public Function GetPurchaseTray(ByVal sTrayID As String) As DataSet
        Dim Ds As New DataSet
        Dim sExec As String
        Try
            sExec = " select * from tblPurchaseStock  where TrayID=" & fncFormatSQLText(sTrayID)
            sExec = sExec & " AND TrayID <>'X'"
            Ds = dm.getDataSetWithSqlAdapter(sExec, "Tray", "Tray")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetPurchaseTray = Ds
            Ds = Nothing
            sExec = Nothing
        End Try
    End Function
    Public Function GetInventoryFromStock(ByVal sInventoryCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sExec As String
        Try
            sExec = " select * from tblStock  where InventoryCode=" & fncFormatSQLText(sInventoryCode)
            Ds = dm.getDataSetWithSqlAdapter(sExec, "Inventory", "Inventory")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetInventoryFromStock = Ds
            Ds = Nothing
            sExec = Nothing
        End Try
    End Function
    Public Function GetInventoryFromStockPurchase(ByVal sInventoryCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sExec As String
        Try
            sExec = " select * from tblPurchaseStock  where InventoryCode=" & fncFormatSQLText(sInventoryCode)
            Ds = dm.getDataSetWithSqlAdapter(sExec, "Inventory", "Inventory")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetInventoryFromStockPurchase = Ds
            Ds = Nothing
            sExec = Nothing
        End Try
    End Function
    Public Function UpdateStock(ByVal sInventoryCode As String, ByVal sTrayid As String, ByVal TotWeight As Double) As DataSet
        Dim Ds As New DataSet
        Dim sExec As String
        Try
            sExec = ""
            sExec = sExec & " EXEC sp_UpdateStock " & fncFormatSQLText(sInventoryCode) & ","
            sExec = sExec & fncFormatSQLText(sTrayid) & ","
            sExec = sExec & Val(TotWeight) & ","
            sExec = sExec & fncFormatSQLText(sUserID)
            Ds = dm.getDataSetWithSqlAdapter(sExec, "Inventory", "Inventory")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            UpdateStock = Ds
            Ds = Nothing
            sExec = Nothing
        End Try
    End Function
    Public Function UpdatePurchaseStock(ByVal sInventoryCode As String, ByVal sTrayid As String, ByVal TotWeight As Double) As DataSet
        Dim Ds As New DataSet
        Dim sExec As String
        Try
            sExec = ""
            sExec = sExec & " EXEC UpdatePurchaseStock " & fncFormatSQLText(sInventoryCode) & ","
            sExec = sExec & fncFormatSQLText(sTrayid) & ","
            sExec = sExec & Val(TotWeight) & ","
            sExec = sExec & fncFormatSQLText(sUserID)
            Ds = dm.getDataSetWithSqlAdapter(sExec, "Inventory", "Inventory")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            UpdatePurchaseStock = Ds
            Ds = Nothing
            sExec = Nothing
        End Try
    End Function

#Region "Get Inventory Detail"
    Public Function GetInventoryDetail(ByVal sInventoryCode As String) As DataSet
        Dim ds As New DataSet
        Dim sSql As String

        sSql = "SELECT *,Pdate = CONVERT(CHAR(8),PurchaseDate,112) FROM vwInventoryStock WHERE InventoryCode =" & fncFormatSQLText(sInventoryCode)
        Try
            ds = dm.getDataSetWithSqlAdapter(sSql, "tblInventory", "tblInventory")
            GetInventoryDetail = ds
            Exit Function
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
            ds = Nothing
        End Try
    End Function
    Public Function GetInstallmentDetail(ByVal sInventoryCode As String) As DataSet
        Dim ds As New DataSet
        Dim sSql As String

        sSql = "SELECT * FROM tblInstallmentEntryDetail WHERE InventoryCode =" & fncFormatSQLText(sInventoryCode)
        Try
            ds = dm.getDataSetWithSqlAdapter(sSql, "tblInventory", "tblInventory")
            GetInstallmentDetail = ds
            Exit Function
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
            ds = Nothing
        End Try
    End Function
    Public Function GetAdditionalInventoryDetail(ByVal sInventoryCode As String) As DataSet
        Dim ds As New DataSet
        Dim sSql As String

        sSql = "SELECT * FROM tblInventoryAddlDescription WHERE InventoryCode =" & fncFormatSQLText(sInventoryCode) & " order by Createdate Desc"
        Try
            ds = dm.getDataSetWithSqlAdapter(sSql, "tblAddInventory", "tblAddInventory")
            GetAdditionalInventoryDetail = ds
            Exit Function
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
            ds = Nothing
        End Try
    End Function
    Public Function GetInventoryDetailTransfer(ByVal sInventoryCode As String) As DataSet
        Dim ds As New DataSet
        Dim sSql As String

        sSql = "SELECT a.InventoryCode,a.Description,a.ClassCode,a.CategoryCode,a.CatalogueCode,b.Weight,a.SizeCode,b.QtyonHand,a.PMC,a.SMC,a.CostPerGram,a.TotalCost,a.SupplierCode,a.GOldLoss,b.PCs FROM tblInventory a,tblInventoryStock b WHERE a.InventoryCode=b.InventoryCode and a.LocationCode=b.LocationCode and a.InventoryCode =" & fncFormatSQLText(sInventoryCode)
        Try
            ds = dm.getDataSetWithSqlAdapter(sSql, "tblInventory", "tblInventory")
            GetInventoryDetailTransfer = ds
            Exit Function
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
            ds = Nothing
        End Try
    End Function
    Public Function GetInventoryDetailByTray(ByVal sTrayID As String) As DataSet
        Dim ds As New DataSet
        Dim sSql As String

        sSql = "SELECT *,Pdate = CONVERT(CHAR(8),PurchaseDate,112) FROM tblInventory WHERE TrayID =" & fncFormatSQLText(sTrayID)
        Try
            ds = dm.getDataSetWithSqlAdapter(sSql, "tblInventory", "tblInventory")
            GetInventoryDetailByTray = ds
            Exit Function
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
            ds = Nothing
        End Try
    End Function
#End Region
    Public Function CheckStock() As DataSet
        Dim ds As New DataSet
        Dim sSql As String

        sSql = "Select * from tblStock WHERE TrayID <>'X'"
        Try
            ds = dm.getDataSetWithSqlAdapter(sSql, "tblStock", "tblStock")
            CheckStock = ds
            Exit Function
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
            ds = Nothing
        End Try
    End Function
    Public Function CheckPurchaseStock() As DataSet
        Dim ds As New DataSet
        Dim sSql As String

        sSql = "Select * from tblPurchaseStock WHERE TrayID <>'X'"
        Try
            ds = dm.getDataSetWithSqlAdapter(sSql, "tblStock", "tblStock")
            CheckPurchaseStock = ds
            Exit Function
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
            ds = Nothing
        End Try
    End Function
    ''HAJ on 18 sep 2007
    Public Function StockError(ByVal sInventoryCode As String, ByVal sTrayID As String) As Boolean
        Dim sExec As String
        Try
            StockError = False
            sExec = "Insert Into tblError Values" & "("
            sExec = sExec & fncFormatSQLText(sInventoryCode) & ","
            sExec = sExec & fncFormatSQLText(sTrayID) & ","
            sExec = sExec & fncFormatSQLText(sUserID) & ","
            sExec = sExec & "Getdate())"

            dm.ProjExecNonQry(sExec)
            StockError = True
            Exit Function
        Catch ex As Exception
            StockError = False
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Function
    Public Function InventoryBarCoinSave(ByVal sInventorycode As String, ByVal sMetalType As String, _
                             ByVal sGSTType As String, ByVal sPurity As String, _
                             ByVal sRefiner As String, ByVal sCountry As String, ByVal sBarNo As String, _
                             ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim Status As Boolean
        Try


            sQry = ""
            sQry = sQry & "EXEC sp_InventoryBarCoinSave " & fncFormatSQLText(sInventorycode) & ","
            sQry = sQry & fncFormatSQLText(sMetalType) & ","
            sQry = sQry & fncFormatSQLText(sGSTType) & ","
            sQry = sQry & fncFormatSQLText(sPurity) & ","
            sQry = sQry & fncFormatSQLText(sRefiner) & ","
            sQry = sQry & fncFormatSQLText(sCountry) & ","
            sQry = sQry & fncFormatSQLText(sBarNo) & ","
            sQry = sQry & fncFormatSQLText(sUserId)
            Status = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        Finally
            InventoryBarCoinSave = Status
        End Try
    End Function
    Public Function InventoryMovementSave(ByVal sLocationCode As String, ByVal sInventorycode As String, ByVal sDescription As String, ByVal sStatus As String, ByVal sRefNumber As String, _
                                          ByVal sRefDate As String, ByVal sQty As Integer, ByVal sWeight As Decimal, ByVal sSize As Decimal, ByVal sPMC As Decimal, ByVal sSMC As Decimal, _
                                         ByVal sCostPerGram As String, ByVal sTotalCost As String, _
                                        ByVal sSupplierCode As String, ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim Status As Boolean
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
            Status = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        Finally
            InventoryMovementSave = Status
        End Try
    End Function
    Public Function InventoryAddlDescSave(ByVal sInventorycode As String, ByVal sJobSheetNo As String, ByVal sDesc1 As String, _
                             ByVal sDesc2 As String, ByVal sDesc3 As String, _
                             ByVal sDesc4 As String, _
                             ByVal sCost1 As Double, ByVal sCost2 As Double, _
                             ByVal sCost3 As Double, ByVal sCost4 As Double, _
                             ByVal sSellingPrice1 As Double, ByVal sSellingPrice2 As Double, _
                             ByVal sSellingPrice3 As Double, ByVal sSellingPrice4 As Double, _
                             ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim Status As Boolean
        Try


            sQry = ""
            sQry = sQry & "EXEC sp_InventoryAddlDescSave " & fncFormatSQLText(sInventorycode) & ","
            sQry = sQry & fncFormatSQLText(sJobSheetNo) & ","
            sQry = sQry & fncFormatSQLText(sDesc1) & ","
            sQry = sQry & fncFormatSQLText(sDesc2) & ","
            sQry = sQry & fncFormatSQLText(sDesc3) & ","
            sQry = sQry & fncFormatSQLText(sDesc4) & ","
            sQry = sQry & Val(sCost1) & ","
            sQry = sQry & Val(sCost2) & ","
            sQry = sQry & Val(sCost3) & ","
            sQry = sQry & Val(sCost4) & ","
            sQry = sQry & Val(sSellingPrice1) & ","
            sQry = sQry & Val(sSellingPrice2) & ","
            sQry = sQry & Val(sSellingPrice3) & ","
            sQry = sQry & Val(sSellingPrice4) & ","
            sQry = sQry & fncFormatSQLText(sUserId)
            Status = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        Finally
            InventoryAddlDescSave = Status
        End Try
    End Function
    Public Function InventoryGoldBarSaveTmp(ByVal sInventorycode As String, ByVal sMetalType As String, _
                         ByVal sGSTType As String, ByVal sPurity As String, ByVal sBarNo As String, _
                         ByVal sRefiner As String, ByVal sCountry As String, ByVal sStatus As String, _
                         ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim Status As Boolean
        Try


            sQry = ""
            sQry = sQry & "EXEC sp_InventoryGoldBarSaveTmp " & fncFormatSQLText(sInventorycode) & ","
            sQry = sQry & fncFormatSQLText(sMetalType) & ","
            sQry = sQry & fncFormatSQLText(sGSTType) & ","
            sQry = sQry & fncFormatSQLText(sPurity) & ","
            sQry = sQry & fncFormatSQLText(sBarNo) & ","
            sQry = sQry & fncFormatSQLText(sRefiner) & ","
            sQry = sQry & fncFormatSQLText(sCountry) & ","
            sQry = sQry & fncFormatSQLText(sStatus) & ","
            sQry = sQry & fncFormatSQLText(sUserId)
            Status = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        Finally
            InventoryGoldBarSaveTmp = Status
        End Try
    End Function
    Public Function InventoryGoldBarSave(ByVal sInventorycode As String, ByVal sMetalType As String, _
                     ByVal sGSTType As String, ByVal sPurity As String, ByVal sBarNo As String, _
                     ByVal sRefiner As String, ByVal sCountry As String, ByVal sStatus As String, _
                     ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim Status As Boolean
        Try


            sQry = ""
            sQry = sQry & "EXEC sp_InventoryGoldBarSave " & fncFormatSQLText(sInventorycode) & ","
            sQry = sQry & fncFormatSQLText(sMetalType) & ","
            sQry = sQry & fncFormatSQLText(sGSTType) & ","
            sQry = sQry & fncFormatSQLText(sPurity) & ","
            sQry = sQry & fncFormatSQLText(sBarNo) & ","
            sQry = sQry & fncFormatSQLText(sRefiner) & ","
            sQry = sQry & fncFormatSQLText(sCountry) & ","
            sQry = sQry & fncFormatSQLText(sStatus) & ","
            sQry = sQry & fncFormatSQLText(sUserId)
            Status = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        Finally
            InventoryGoldBarSave = Status
        End Try
    End Function
    Public Function GetInventoryBarCoin(ByVal sInventoryCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select * from tblInventoryBarCoin  "
            sQry = sQry & " Where InventoryCode = " & fncFormatSQLText(sInventoryCode)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Class", "Class")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetInventoryBarCoin = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

#Region "Added by Murugan"
    Public Function InventorySaveML(ByVal sinventorycode As String, ByVal sDescription As String, _
                              ByVal sDiamondDescription As String, ByVal nSize As Double, ByVal nWeight As Double, _
                              ByVal nStoneWeight As Double, ByVal nCostPerGram As Double, _
                              ByVal nPMC As Double, ByVal NSMC As Double, ByVal nPCS As Double, _
                              ByVal nGoldLoss As Double, ByVal nTotalCost As Double, _
                              ByVal sJobsheetNo As String, ByVal sPurchaseBillNo As String, _
                              ByVal sPurchaseDate As String, ByVal sSupplierCode As String, _
                              ByVal sSalesBillNo As String, ByVal sSalesDate As String, _
                              ByVal nSalesCostPerGram As Double, ByVal nSalesMC As Double, ByVal nSalesAmount As Double, _
                              ByVal sCategoryCode As String, ByVal sClassCode As String, ByVal sCatalogueCode As String, ByVal sTrayId As String, _
                              ByVal sPMCType As String, ByVal sSMCType As String, ByVal lQtyOnHand As Long, _
                              ByVal lQtySold As Long, ByVal sRemarks As String, _
                              ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim Status As Boolean
        Try


            sQry = ""
            sQry = sQry & "EXEC sp_InventoryML " & fncFormatSQLText(sinventorycode) & ","
            sQry = sQry & fncFormatSQLText(sDescription) & ","
            sQry = sQry & fncFormatSQLText(sDiamondDescription) & ","
            sQry = sQry & Val(nSize) & ","
            sQry = sQry & Val(nWeight) & ","
            sQry = sQry & Val(nStoneWeight) & ","
            sQry = sQry & Val(nCostPerGram) & ","
            sQry = sQry & Val(nPMC) & ","
            sQry = sQry & Val(NSMC) & ","
            sQry = sQry & Val(nPCS) & ","
            sQry = sQry & Val(nGoldLoss) & ","
            sQry = sQry & Val(nTotalCost) & ","
            sQry = sQry & fncFormatSQLText(sJobsheetNo) & ","
            sQry = sQry & fncFormatSQLText(sPurchaseBillNo) & ","
            sQry = sQry & fncFormatSQLDate(sPurchaseDate) & ","
            sQry = sQry & fncFormatSQLText(sSupplierCode) & ","
            sQry = sQry & fncFormatSQLText(sSalesBillNo) & ","
            sQry = sQry & fncFormatSQLDate(sSalesDate) & ","
            sQry = sQry & Val(nSalesCostPerGram) & ","
            sQry = sQry & Val(nSalesMC) & ","
            sQry = sQry & Val(nSalesAmount) & ","
            sQry = sQry & fncFormatSQLText(sCategoryCode) & ","
            sQry = sQry & fncFormatSQLText(sClassCode) & ","
            sQry = sQry & fncFormatSQLText(sCatalogueCode) & ","
            sQry = sQry & fncFormatSQLText(sTrayId) & ","
            sQry = sQry & fncFormatSQLText(sPMCType) & ","
            sQry = sQry & fncFormatSQLText(sSMCType) & ","
            sQry = sQry & Val(lQtyOnHand) & ","
            sQry = sQry & Val(lQtySold) & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & fncFormatSQLText(sUserId) & ","
            sQry = sQry & fncFormatSQLText(gthisLocation)
            Status = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        Finally
            InventorySaveML = Status
        End Try
    End Function



    Public Function InventoryDPSave(ByVal sinventorycode As String, ByVal sDescription As String, _
                              ByVal sDiamondDescription As String, ByVal nWeight As Double, _
                              ByVal nStoneWeight As Double, ByVal nCostPerGram As Double, _
                              ByVal nPMC As Double, ByVal NSMC As Double, ByVal nPCS As Double, _
                              ByVal nGoldLoss As Double, ByVal nTotalCost As Double, _
                              ByVal sJobsheetNo As String, ByVal sPurchaseBillNo As String, _
                              ByVal sPurchaseDate As String, ByVal sSupplierCode As String, _
                              ByVal sSalesBillNo As String, ByVal sSalesDate As String, _
                              ByVal nSalesCostPerGram As Double, ByVal nSalesMC As Double, ByVal nSalesAmount As Double, _
                              ByVal sCategoryCode As String, ByVal sClassCode As String, ByVal sCatalogueCode As String, ByVal sTrayId As String, _
                              ByVal sPMCType As String, ByVal sSMCType As String, ByVal lQtyOnHand As Long, _
                              ByVal lQtySold As Long, ByVal sRemarks As String, ByVal sSizeCode As String, ByVal sSizeType As String, _
                              ByVal InvImage() As Byte, ByVal sImageName As String, _
                              ByVal sUserId As String, Optional ByVal sSalesPCS As Long = 0) As Boolean
        Dim sQry As String
        Dim Status As Boolean
        sCreateInventory = ""
        Try

            Dim Cmd As New SqlClient.SqlCommand("sp_DPInventory", gsConBatchPosting)
            Cmd.Transaction = gsTrans
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.Parameters.Add("@InventoryCode", SqlDbType.VarChar)
            Cmd.Parameters("@InventoryCode").Value = sinventorycode
            Cmd.Parameters.Add("@Description", SqlDbType.VarChar)
            Cmd.Parameters("@Description").Value = sDescription
            Cmd.Parameters.Add("@DiamondDescription", SqlDbType.VarChar)
            Cmd.Parameters("@DiamondDescription").Value = sDiamondDescription
            Cmd.Parameters.Add("@Weight", SqlDbType.Decimal)
            Cmd.Parameters("@Weight").Value = nWeight
            Cmd.Parameters.Add("@StoneWeight", SqlDbType.Decimal)
            Cmd.Parameters("@StoneWeight").Value = nStoneWeight
            Cmd.Parameters.Add("@CostPerGram", SqlDbType.Decimal)
            Cmd.Parameters("@CostPerGram").Value = nCostPerGram
            Cmd.Parameters.Add("@PMC", SqlDbType.Decimal)
            Cmd.Parameters("@PMC").Value = nPMC
            Cmd.Parameters.Add("@SMC", SqlDbType.Decimal)
            Cmd.Parameters("@SMC").Value = NSMC
            Cmd.Parameters.Add("@PCS", SqlDbType.Decimal)
            Cmd.Parameters("@PCS").Value = nPCS
            Cmd.Parameters.Add("@GoldLoss", SqlDbType.Decimal)
            Cmd.Parameters("@GoldLoss").Value = nGoldLoss
            Cmd.Parameters.Add("@TotalCost", SqlDbType.Decimal)
            Cmd.Parameters("@TotalCost").Value = nTotalCost
            Cmd.Parameters.Add("@JobSheetNo", SqlDbType.VarChar)
            Cmd.Parameters("@JobSheetNo").Value = sJobsheetNo
            Cmd.Parameters.Add("@PurchaseBillNo", SqlDbType.VarChar)
            Cmd.Parameters("@PurchaseBillNo").Value = sPurchaseBillNo
            Cmd.Parameters.Add("@PurchaseDate", SqlDbType.SmallDateTime)
            Cmd.Parameters("@PurchaseDate").Value = sPurchaseDate
            Cmd.Parameters.Add("@SupplierCode", SqlDbType.VarChar)
            Cmd.Parameters("@SupplierCode").Value = sSupplierCode
            Cmd.Parameters.Add("@SalesBillNo", SqlDbType.VarChar)
            Cmd.Parameters("@SalesBillNo").Value = sSalesBillNo
            Cmd.Parameters.Add("@SalesDate", SqlDbType.SmallDateTime)
            Cmd.Parameters("@SalesDate").Value = sSalesDate
            Cmd.Parameters.Add("@SalesCostPerGram", SqlDbType.Decimal)
            Cmd.Parameters("@SalesCostPerGram").Value = nSalesCostPerGram
            Cmd.Parameters.Add("@SalesMC", SqlDbType.Decimal)
            Cmd.Parameters("@SalesMC").Value = nSalesMC
            Cmd.Parameters.Add("@SalesAmount", SqlDbType.Decimal)
            Cmd.Parameters("@SalesAmount").Value = nSalesAmount
            Cmd.Parameters.Add("@CategoryCode", SqlDbType.VarChar)
            Cmd.Parameters("@CategoryCode").Value = sCategoryCode
            Cmd.Parameters.Add("@ClassCode", SqlDbType.VarChar)
            Cmd.Parameters("@ClassCode").Value = sClassCode
            Cmd.Parameters.Add("@CatalogueCode", SqlDbType.VarChar)
            Cmd.Parameters("@CatalogueCode").Value = sCatalogueCode
            Cmd.Parameters.Add("@TrayId", SqlDbType.VarChar)
            Cmd.Parameters("@TrayId").Value = sTrayId
            Cmd.Parameters.Add("@PMCType", SqlDbType.VarChar)
            Cmd.Parameters("@PMCType").Value = sPMCType
            Cmd.Parameters.Add("@SMCType", SqlDbType.VarChar)
            Cmd.Parameters("@SMCType").Value = sSMCType
            Cmd.Parameters.Add("@QtyOnHand", SqlDbType.Int)
            Cmd.Parameters("@QtyOnHand").Value = lQtyOnHand
            Cmd.Parameters.Add("@QtySold", SqlDbType.Int)
            Cmd.Parameters("@QtySold").Value = lQtySold
            Cmd.Parameters.Add("@Remarks", SqlDbType.VarChar)
            Cmd.Parameters("@Remarks").Value = sRemarks
            Cmd.Parameters.Add("@SizeCode", SqlDbType.VarChar)
            Cmd.Parameters("@SizeCode").Value = sSizeCode
            Cmd.Parameters.Add("@SizeType", SqlDbType.VarChar)
            Cmd.Parameters("@SizeType").Value = sSizeType
            Cmd.Parameters.Add("@InventoryImage", SqlDbType.Image)
            Cmd.Parameters("@InventoryImage").Value = InvImage
            Cmd.Parameters.Add("@InventoryImageName", SqlDbType.VarChar)
            Cmd.Parameters("@InventoryImageName").Value = sImageName
            Cmd.Parameters.Add("@UserID", SqlDbType.VarChar)
            Cmd.Parameters("@UserID").Value = sUserId
            Cmd.Parameters.Add("@LocationCode", SqlDbType.VarChar)
            Cmd.Parameters("@LocationCode").Value = gthisLocation
            Cmd.Parameters.Add("@SalesPCS", SqlDbType.Bit)
            Cmd.Parameters("@SalesPCS").Value = sSalesPCS

            'Cmd.ExecuteNonQuery()
            sCreateInventory = Cmd.ExecuteScalar

            'sQry = ""
            'sQry = sQry & "EXEC sp_Inventory " & fncFormatSQLText(sinventorycode) & ","
            'sQry = sQry & fncFormatSQLText(sDescription) & ","
            'sQry = sQry & fncFormatSQLText(sDiamondDescription) & ","
            'sQry = sQry & Val(nWeight) & ","
            'sQry = sQry & Val(nStoneWeight) & ","
            'sQry = sQry & Val(nCostPerGram) & ","
            'sQry = sQry & Val(nPMC) & ","
            'sQry = sQry & Val(NSMC) & ","
            'sQry = sQry & Val(nPCS) & ","
            'sQry = sQry & Val(nGoldLoss) & ","
            'sQry = sQry & Val(nTotalCost) & ","
            'sQry = sQry & fncFormatSQLText(sJobsheetNo) & ","
            'sQry = sQry & fncFormatSQLText(sPurchaseBillNo) & ","
            'sQry = sQry & fncFormatSQLDate(sPurchaseDate) & ","
            'sQry = sQry & fncFormatSQLText(sSupplierCode) & ","
            'sQry = sQry & fncFormatSQLText(sSalesBillNo) & ","
            'sQry = sQry & fncFormatSQLDate(sSalesDate) & ","
            'sQry = sQry & Val(nSalesCostPerGram) & ","
            'sQry = sQry & Val(nSalesMC) & ","
            'sQry = sQry & Val(nSalesAmount) & ","
            'sQry = sQry & fncFormatSQLText(sCategoryCode) & ","
            'sQry = sQry & fncFormatSQLText(sClassCode) & ","
            'sQry = sQry & fncFormatSQLText(sCatalogueCode) & ","
            'sQry = sQry & fncFormatSQLText(sTrayId) & ","
            'sQry = sQry & fncFormatSQLText(sPMCType) & ","
            'sQry = sQry & fncFormatSQLText(sSMCType) & ","
            'sQry = sQry & Val(lQtyOnHand) & ","
            'sQry = sQry & Val(lQtySold) & ","
            'sQry = sQry & fncFormatSQLText(sRemarks) & ","
            'sQry = sQry & Val(sSizeCode) & ","
            'sQry = sQry & fncFormatSQLText(sSizeType) & ","
            'sQry = sQry & fncFormatSQLText(sUserId) & ","
            'sQry = sQry & fncFormatSQLText(gthisLocation) & ","
            'sQry = sQry & Val(sSalesPCS)
            'sCreateInventory = dm.ReturnProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            InventoryDPSave = False
        Finally
            Status = True
            InventoryDPSave = True
        End Try
    End Function
    Public Function InventoryDPPCSSave(ByVal sinventorycode As String, ByVal sDescription As String, _
                              ByVal sDiamondDescription As String, ByVal nWeight As Double, _
                              ByVal nStoneWeight As Double, ByVal nCostPerGram As Double, _
                              ByVal nPMC As Double, ByVal NSMC As Double, ByVal nPCS As Double, _
                              ByVal nGoldLoss As Double, ByVal nTotalCost As Double, _
                              ByVal sJobsheetNo As String, ByVal sPurchaseBillNo As String, _
                              ByVal sPurchaseDate As String, ByVal sSupplierCode As String, _
                              ByVal sSalesBillNo As String, ByVal sSalesDate As String, _
                              ByVal nSalesCostPerGram As Double, ByVal nSalesMC As Double, ByVal nSalesAmount As Double, _
                              ByVal sCategoryCode As String, ByVal sClassCode As String, ByVal sCatalogueCode As String, ByVal sTrayId As String, _
                              ByVal sPMCType As String, ByVal sSMCType As String, ByVal lQtyOnHand As Long, _
                              ByVal lQtySold As Long, ByVal sRemarks As String, ByVal sSizeCode As String, ByVal sSizeType As String, _
                              ByVal InvImage() As Byte, ByVal sImageName As String, _
                              ByVal sUserId As String, Optional ByVal sSalesPCS As Long = 0) As Boolean
        Dim sQry As String
        Dim Status As Boolean
        sCreateInventory = ""
        Try

            sQry = ""
            sQry = sQry & "EXEC sp_InventoryDPPCSSave " & fncFormatSQLText(sinventorycode) & ","
            sQry = sQry & fncFormatSQLText(sDescription) & ","
            sQry = sQry & fncFormatSQLText(sDiamondDescription) & ","
            sQry = sQry & Val(nWeight) & ","
            sQry = sQry & Val(nStoneWeight) & ","
            sQry = sQry & Val(nCostPerGram) & ","
            sQry = sQry & Val(nPMC) & ","
            sQry = sQry & Val(NSMC) & ","
            sQry = sQry & Val(nPCS) & ","
            sQry = sQry & Val(nGoldLoss) & ","
            sQry = sQry & Val(nTotalCost) & ","
            sQry = sQry & fncFormatSQLText(sJobsheetNo) & ","
            sQry = sQry & fncFormatSQLText(sPurchaseBillNo) & ","
            sQry = sQry & fncFormatSQLDate(sPurchaseDate) & ","
            sQry = sQry & fncFormatSQLText(sSupplierCode) & ","
            sQry = sQry & fncFormatSQLText(sSalesBillNo) & ","
            sQry = sQry & fncFormatSQLDate(sSalesDate) & ","
            sQry = sQry & Val(nSalesCostPerGram) & ","
            sQry = sQry & Val(nSalesMC) & ","
            sQry = sQry & Val(nSalesAmount) & ","
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
            sQry = sQry & fncFormatSQLText(sUserId) & ","
            sQry = sQry & fncFormatSQLText(gthisLocation) & ","
            sQry = sQry & Val(sSalesPCS)
            sCreateInventory = dm.ReturnProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            InventoryDPPCSSave = False
        Finally
            Status = True
            InventoryDPPCSSave = True
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

    Public Function InventoryImageSave(ByVal sinventorycode As String, _
                              ByVal InvImage() As Byte, ByVal sImageName As String, _
                              ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim Status As Boolean
        sCreateInventory = ""
        Try

            Dim Cmd As New SqlClient.SqlCommand("sp_InventoryImageSave", gsConBatchPosting)
            Cmd.Transaction = gsTrans
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.Parameters.Add("@InventoryCode", SqlDbType.VarChar)
            Cmd.Parameters("@InventoryCode").Value = sinventorycode
            Cmd.Parameters.Add("@InventoryImage", SqlDbType.Image)
            Cmd.Parameters("@InventoryImage").Value = InvImage
            Cmd.Parameters.Add("@InventoryImageName", SqlDbType.VarChar)
            Cmd.Parameters("@InventoryImageName").Value = sImageName
            Cmd.Parameters.Add("@UserID", SqlDbType.VarChar)
            Cmd.Parameters("@UserID").Value = sUserId
            Cmd.ExecuteNonQuery()
            'sCreateInventory = Cmd.ExecuteScalar

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            InventoryImageSave = False
        Finally
            Status = True
            InventoryImageSave = True
        End Try
    End Function
    Public Function InventorySave(ByVal sinventorycode As String, ByVal sDescription As String, _
                              ByVal sDiamondDescription As String, ByVal nWeight As Double, _
                              ByVal nStoneWeight As Double, ByVal nCostPerGram As Double, _
                              ByVal nPMC As Double, ByVal NSMC As Double, ByVal nPCS As Double, _
                              ByVal nGoldLoss As Double, ByVal nTotalCost As Double, _
                              ByVal sJobsheetNo As String, ByVal sPurchaseBillNo As String, _
                              ByVal sPurchaseDate As String, ByVal sSupplierCode As String, _
                              ByVal sSalesBillNo As String, ByVal sSalesDate As String, _
                              ByVal nSalesCostPerGram As Double, ByVal nSalesMC As Double, ByVal nSalesAmount As Double, _
                              ByVal sCategoryCode As String, ByVal sClassCode As String, ByVal sCatalogueCode As String, ByVal sTrayId As String, _
                              ByVal sPMCType As String, ByVal sSMCType As String, ByVal lQtyOnHand As Long, _
                              ByVal lQtySold As Long, ByVal sRemarks As String, ByVal sSizeCode As String, ByVal sSizeType As String, _
                              ByVal sUserId As String, Optional ByVal sSalesPCS As Long = 0) As Boolean
        Dim sQry As String
        Dim Status As Boolean
        sCreateInventory = ""
        Try


            sQry = ""
            sQry = sQry & "EXEC sp_Inventory " & fncFormatSQLText(sinventorycode) & ","
            sQry = sQry & fncFormatSQLText(sDescription) & ","
            sQry = sQry & fncFormatSQLText(sDiamondDescription) & ","
            sQry = sQry & Val(nWeight) & ","
            sQry = sQry & Val(nStoneWeight) & ","
            sQry = sQry & Val(nCostPerGram) & ","
            sQry = sQry & Val(nPMC) & ","
            sQry = sQry & Val(NSMC) & ","
            sQry = sQry & Val(nPCS) & ","
            sQry = sQry & Val(nGoldLoss) & ","
            sQry = sQry & Val(nTotalCost) & ","
            sQry = sQry & fncFormatSQLText(sJobsheetNo) & ","
            sQry = sQry & fncFormatSQLText(sPurchaseBillNo) & ","
            sQry = sQry & fncFormatSQLDate(sPurchaseDate) & ","
            sQry = sQry & fncFormatSQLText(sSupplierCode) & ","
            sQry = sQry & fncFormatSQLText(sSalesBillNo) & ","
            sQry = sQry & fncFormatSQLDate(sSalesDate) & ","
            sQry = sQry & Val(nSalesCostPerGram) & ","
            sQry = sQry & Val(nSalesMC) & ","
            sQry = sQry & Val(nSalesAmount) & ","
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
            sQry = sQry & fncFormatSQLText(sUserId) & ","
            sQry = sQry & fncFormatSQLText(gthisLocation) & ","
            sQry = sQry & Val(sSalesPCS)
            sCreateInventory = dm.ReturnProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            InventorySave = False
        Finally
            Status = True
            InventorySave = True
        End Try
    End Function

    Public Function InventoryRFIDSave(ByVal sinventorycode As String, ByVal sDescription As String, _
                              ByVal sDiamondDescription As String, ByVal nWeight As Double, _
                              ByVal nPMC As Double, ByVal NSMC As Double, _
                              ByVal nGoldLoss As Double, _
                              ByVal sSupplierCode As String, _
                              ByVal sCategoryCode As String, ByVal sClassCode As String, ByVal sCatalogueCode As String, ByVal sTrayId As String, ByVal sTrayGroup As String) As Boolean
        Dim sQry As String
        Dim Status As Boolean
        Try


            sQry = ""
            sQry = sQry & "EXEC sp_ItemInsert " & fncFormatSQLText(sinventorycode) & ","
            sQry = sQry & fncFormatSQLText(sDescription) & ","
            sQry = sQry & fncFormatSQLText(sDiamondDescription) & ","
            sQry = sQry & Val(nWeight) & ","
            sQry = sQry & Val(nPMC) & ","
            sQry = sQry & Val(NSMC) & ","
            sQry = sQry & Val(nGoldLoss) & ","
            sQry = sQry & fncFormatSQLText(sSupplierCode) & ","
            sQry = sQry & fncFormatSQLText(sCategoryCode) & ","
            sQry = sQry & fncFormatSQLText(sClassCode) & ","
            sQry = sQry & fncFormatSQLText(sCatalogueCode) & ","
            sQry = sQry & fncFormatSQLText(sTrayId) & ","
            sQry = sQry & fncFormatSQLText(sTrayGroup)
            Status = dm.ProjExecNonQryRFID(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        Finally
            InventoryRFIDSave = Status
        End Try
    End Function
    Public Function GetInventoryWeight(ByVal sJobsheetNo As String, ByVal sCategoryCode As String) As DataSet
        Dim ds As DataSet
        Dim sQry As String
        Try
            GetInventoryWeight = Nothing
            sQry = "SELECT BillWt = Sum(Weight),BillPcs = Sum(Pcs) FROM tblPurchase Where JobSheetNo = " & fncFormatSQLText(sJobsheetNo)
            sQry = sQry & " And CategoryCode = " & fncFormatSQLText(sCategoryCode)
            ds = dm.getDataSetWithSqlAdapter(sQry, "Inventory", "Inventory")

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            ds = Nothing
        Finally
            GetInventoryWeight = ds
            ds = Nothing
        End Try
    End Function

    Public Function GetInventorybyJobSheet(ByVal sJobsheetNo As String) As DataSet
        Dim ds As New DataSet
        Dim sSql As String

        sSql = "SELECT a.*,b.Pcs,b.TotalCost FROM tblInventory a,tblPurchase b WHERE a.JobSheetNo=b.Jobsheetno and  a.InventoryCode=b.InventoryCode and  a.JobSheetNo =" & fncFormatSQLText(sJobsheetNo)
        sSql = sSql & " Order By a.InventoryCode"
        Try
            ds = dm.getDataSetWithSqlAdapter(sSql, "tblInventory", "tblInventory")
            GetInventorybyJobSheet = ds
            Exit Function
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
            ds = Nothing
        End Try
    End Function
    Public Function GetInventoryImage(ByVal sInventoryCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select isnull(InventoryImage,'')InventoryImage from tblInventory  where InventoryCode =" & fncFormatSQLText(sInventoryCode)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Inventory", "Inventory")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetInventoryImage = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function UserLogSave(ByVal sUserId As String, _
                                  ByVal sType As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_UserLog " & fncFormatSQLText(gthisLocation) & "," & fncFormatSQLText(sUserId) & ","
            sQry = sQry & fncFormatSQLText(sType)
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            UserLogSave = SaveStatus
            sQry = Nothing
        End Try
    End Function

#End Region

#Region "Added by Sarathiy"
    Public Function UpdateInventoryAndOrder(ByVal sInventoryCode As String, ByVal sOrderNo As String, ByVal sOrderInventoryCode As String) As Boolean
        Dim sQry As String
        Dim Status As Boolean
        Try
            'sQry = " Update tblInventoryOrderDetail Set InventoryCode = " & fncFormatSQLText(sInventoryCode) & " , Status = 'P' ,Modifieddate = getdate(), ModifiedUser = " & fncFormatSQLText(sUserID) & " where OrderNo = " & fncFormatSQLText(sOrderNo) & " and OrderInventoryCode = " & fncFormatSQLText(sOrderInventoryCode)
            'Status = dm.ProjExecNonQry(sQry)
            'If Status Then
            '    sQry = "Update tblInventory set OrderNo = " & fncFormatSQLText(sOrderNo) & " , Modifydate = getdate(), ModifyUser = " & fncFormatSQLText(sUserID) & " where InventoryCode = " & fncFormatSQLText(sInventoryCode)
            '    Status = dm.ProjExecNonQry(sQry)
            'End If
            sQry = " Exec sp_UpdateInventoryOrderStatus "
            sQry = sQry & fncFormatSQLText(sInventoryCode) & ","
            sQry = sQry & fncFormatSQLText(sOrderNo) & ","
            sQry = sQry & fncFormatSQLText(sOrderInventoryCode) & ","
            sQry = sQry & fncFormatSQLText(sUserID)
            Status = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        Finally
            UpdateInventoryAndOrder = Status
        End Try
    End Function


#End Region
End Class
