Public Class clsUpdateFromHQ
    Dim dm As New DataManager

    Public Function InsertToBranch(ByVal ds As DataSet)
        Dim sQry As String
        Dim i As Integer
        Try
            Dim r As Integer = ds.Tables(0).Rows.Count
            For i = 0 To r - 1
                sQry = "EXEC sp_UpdateInventoryFromHQ"
                sQry = sQry & fncFormatSQLText(Trim(ds.Tables(0).DefaultView.Item(i)("InventoryCode").ToString())) & ","
                sQry = sQry & fncFormatSQLText(Trim(ds.Tables(0).DefaultView.Item(i)("Description").ToString())) & ","
                sQry = sQry & fncFormatSQLText(Trim(ds.Tables(0).DefaultView.Item(i)("DiamondDescription").ToString())) & ","
                sQry = sQry & Convert.ToDecimal(ds.Tables(0).DefaultView.Item(i)("Weight")) & ","
                sQry = sQry & Convert.ToDecimal(ds.Tables(0).DefaultView.Item(i)("StoneWeight")) & ","
                sQry = sQry & Convert.ToDecimal(ds.Tables(0).DefaultView.Item(i)("CostPerGram")) & ","
                sQry = sQry & Convert.ToDecimal(ds.Tables(0).DefaultView.Item(i)("PMC")) & ","
                sQry = sQry & Convert.ToDecimal(ds.Tables(0).DefaultView.Item(i)("SMC")) & ","
                sQry = sQry & Convert.ToDecimal(ds.Tables(0).DefaultView.Item(i)("PCS")) & ","
                sQry = sQry & Convert.ToDecimal(ds.Tables(0).DefaultView.Item(i)("GoldLoss")) & ","
                sQry = sQry & Convert.ToDecimal(ds.Tables(0).DefaultView.Item(i)("TotalCost")) & ","
                sQry = sQry & fncFormatSQLText(Trim(ds.Tables(0).DefaultView.Item(i)("JobSheetNo").ToString())) & ","
                sQry = sQry & fncFormatSQLText(Trim(ds.Tables(0).DefaultView.Item(i)("PurchaseBillNo").ToString())) & ","
                sQry = sQry & fncFormatSQLDate(IIf(IsDBNull(ds.Tables(0).DefaultView.Item(i)("PurchaseDate")), 0, (ds.Tables(0).DefaultView.Item(i)("PurchaseDate")))) & ","
                sQry = sQry & fncFormatSQLText(Trim(ds.Tables(0).DefaultView.Item(i)("SupplierCode").ToString())) & ","
                sQry = sQry & fncFormatSQLText(Trim(ds.Tables(0).DefaultView.Item(i)("SalesBillNo").ToString())) & ","
                sQry = sQry & fncFormatSQLDate(IIf(IsDBNull(ds.Tables(0).DefaultView.Item(i)("SalesDate")), 0, (ds.Tables(0).DefaultView.Item(i)("SalesDate")))) & ","
                sQry = sQry & Convert.ToDecimal(ds.Tables(0).DefaultView.Item(i)("SalesCostPerGram")) & ","
                sQry = sQry & Convert.ToDecimal(ds.Tables(0).DefaultView.Item(i)("SalesMC")) & ","
                sQry = sQry & Convert.ToDecimal(ds.Tables(0).DefaultView.Item(i)("SalesAmount")) & ","
                sQry = sQry & fncFormatSQLText(Trim(ds.Tables(0).DefaultView.Item(i)("CategoryCode").ToString())) & ","
                sQry = sQry & fncFormatSQLText(Trim(ds.Tables(0).DefaultView.Item(i)("ClassCode").ToString())) & ","
                sQry = sQry & fncFormatSQLText(Trim(ds.Tables(0).DefaultView.Item(i)("CatalogueCode").ToString())) & ","
                sQry = sQry & fncFormatSQLText(Trim(ds.Tables(0).DefaultView.Item(i)("TrayId").ToString())) & ","
                sQry = sQry & fncFormatSQLText(Trim(ds.Tables(0).DefaultView.Item(i)("PMCType").ToString())) & ","
                sQry = sQry & fncFormatSQLText(Trim(ds.Tables(0).DefaultView.Item(i)("SMCType").ToString())) & ","
                sQry = sQry & Convert.ToDecimal(Trim(ds.Tables(0).DefaultView.Item(i)("QtyOnHand"))) & ","
                sQry = sQry & Convert.ToDecimal(Trim(ds.Tables(0).DefaultView.Item(i)("QtySold"))) & ","
                sQry = sQry & fncFormatSQLText(Trim(ds.Tables(0).DefaultView.Item(i)("Remarks").ToString())) & ","
                sQry = sQry & fncFormatSQLText(Trim(sUserID)) & ","
                sQry = sQry & fncFormatSQLText(gthisLocation)
                dm.ProjExecNonQry(sQry, gsCon)
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function fncUpdateLocationMaster(ByVal DrLocation As DataRow()) As Boolean
        Dim sQry As String
        Dim iCnt As Integer
        Dim bStatus As Boolean
        Try
            For iCnt = 0 To DrLocation.Length - 1
                sQry = "EXEC sp_LocationHQ " & fncFormatSQLText(Trim(DrLocation(iCnt)("LocationCode") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrLocation(iCnt)("LocationName") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrLocation(iCnt)("LocationType") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrLocation(iCnt)("Address1") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrLocation(iCnt)("Address2") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrLocation(iCnt)("Address3") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrLocation(iCnt)("Address4") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrLocation(iCnt)("Phone") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrLocation(iCnt)("Fax") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrLocation(iCnt)("PersonIncharge") & "")) & ", "
                sQry = sQry & Val(Trim(DrLocation(iCnt)("SortCode") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrLocation(iCnt)("EODDate") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrLocation(iCnt)("EODUser") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrLocation(iCnt)("CreateUser") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrLocation(iCnt)("CreateDate") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrLocation(iCnt)("ModifyUser") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrLocation(iCnt)("ModifyDate") & ""))
                bStatus = fncExecNonQry(sQry)
                If bStatus = False Then Exit Function
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            DrLocation = Nothing
            sQry = Nothing
            fncUpdateLocationMaster = bStatus
        End Try
    End Function

    Public Function fncUpdateClassMaster(ByVal DrClass As DataRow()) As Boolean
        Dim sQry As String
        Dim iCnt As Integer
        Dim bStatus As Boolean
        Try
            For iCnt = 0 To DrClass.Length - 1
                sQry = "EXEC sp_ClassHQ " & fncFormatSQLText(Trim(DrClass(iCnt)("ClassCode") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrClass(iCnt)("ClassName") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrClass(iCnt)("SalesType") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrClass(iCnt)("PurchaseType") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrClass(iCnt)("GoldLoss") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrClass(iCnt)("DisplayCost") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrClass(iCnt)("GoldBarcode") & "")) & ", "
                sQry = sQry & Val(Trim(DrClass(iCnt)("Rate") & "")) & ", "
                sQry = sQry & Val(Trim(DrClass(iCnt)("MCDiscount") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrClass(iCnt)("Remarks") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrClass(iCnt)("CreateUser") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrClass(iCnt)("CreateDate") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrClass(iCnt)("ModifyUser") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrClass(iCnt)("ModifyDate") & ""))
                bStatus = fncExecNonQry(sQry)
                If bStatus = False Then Exit Function
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            DrClass = Nothing
            sQry = Nothing
            fncUpdateClassMaster = bStatus
        End Try
    End Function

    Public Function fncUpdateCategoryMaster(ByVal DrCategory As DataRow()) As Boolean
        Dim sQry As String
        Dim iCnt As Integer
        Dim bStatus As Boolean
        Try

            For iCnt = 0 To DrCategory.Length - 1
                sQry = "EXEC sp_CategoryHQ " & fncFormatSQLText(Trim(DrCategory(iCnt)("CategoryCode") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrCategory(iCnt)("CategoryName") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrCategory(iCnt)("ClassCode") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrCategory(iCnt)("InventoryPrefix") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrCategory(iCnt)("FromInventoryCode") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrCategory(iCnt)("ToInventoryCode") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrCategory(iCnt)("NextInventoryCode") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrCategory(iCnt)("Remarks") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrCategory(iCnt)("CreateUser") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrCategory(iCnt)("CreateDate") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrCategory(iCnt)("ModifyUser") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrCategory(iCnt)("ModifyDate") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrCategory(iCnt)("SizeFactor") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrCategory(iCnt)("ShowInReceipt") & ""))
                bStatus = fncExecNonQry(sQry)
                If bStatus = False Then Exit Function
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            DrCategory = Nothing
            sQry = Nothing
            fncUpdateCategoryMaster = bStatus
        End Try
    End Function

    Public Function fncUpdateCatalogueMaster(ByVal DrCatalogue As DataRow()) As Boolean
        Dim sQry As String
        Dim iCnt As Integer
        Dim bStatus As Boolean
        Try
            For iCnt = 0 To DrCatalogue.Length - 1
                sQry = "EXEC sp_CatalogueHQ " & fncFormatSQLText(Trim(DrCatalogue(iCnt)("CatalogueCode") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrCatalogue(iCnt)("CatalogueName") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrCatalogue(iCnt)("CategoryCode") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrCatalogue(iCnt)("GoldLoss") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrCatalogue(iCnt)("Remarks") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrCatalogue(iCnt)("CreateUser") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrCatalogue(iCnt)("CreateDate") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrCatalogue(iCnt)("ModifyUser") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrCatalogue(iCnt)("ModifyDate") & ""))
                '& ","
                'sQry = sQry & fncFormatSQLText(Trim(DrCatalogue(iCnt)("CatalogueImage") & "")) & ", "
                'sQry = sQry & fncFormatSQLText(Trim(DrCatalogue(iCnt)("CatalogueImageName") & ""))
                bStatus = fncExecNonQry(sQry)
                If bStatus = False Then Exit Function
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            DrCatalogue = Nothing
            sQry = Nothing
            fncUpdateCatalogueMaster = bStatus
        End Try
    End Function

    Public Function fncUpdateSalesManMaster(ByVal DrSalesMan As DataRow()) As Boolean
        Dim sQry As String
        Dim iCnt As Integer
        Dim bStatus As Boolean
        Try
            For iCnt = 0 To DrSalesMan.Length - 1
                sQry = "EXEC sp_SalesManHQ " & fncFormatSQLText(Trim(DrSalesMan(iCnt)("SalesManCode") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrSalesMan(iCnt)("SalesManName") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrSalesMan(iCnt)("Password") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrSalesMan(iCnt)("Remarks") & "")) & ", "
                sQry = sQry & Val(Trim(DrSalesMan(iCnt)("LastSalesAmount") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrSalesMan(iCnt)("InvoiceNo") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrSalesMan(iCnt)("ShowInReport") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrSalesMan(iCnt)("SortOrder") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrSalesMan(iCnt)("CreateUser") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrSalesMan(iCnt)("CreateDate") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrSalesMan(iCnt)("ModifyUser") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrSalesMan(iCnt)("ModifyDate") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrSalesMan(iCnt)("AllowSalesReturn") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(DrSalesMan(iCnt)("SalesManGroup") & ""))
                bStatus = fncExecNonQry(sQry)
                If bStatus = False Then Exit Function
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            DrSalesMan = Nothing
            sQry = Nothing
            fncUpdateSalesManMaster = bStatus
        End Try
    End Function

    Public Function fncUpdateSupplierMaster(ByVal DrSupplier As DataRow()) As Boolean
        Dim sQry As String
        Dim iCnt As Integer
        Dim bStatus As Boolean
        Try
            For iCnt = 0 To DrSupplier.Length - 1
                sQry = "EXEC sp_SupplierHQ " & fncFormatSQLText(Trim(DrSupplier(iCnt)("SupplierCode") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrSupplier(iCnt)("SupplierName") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrSupplier(iCnt)("Address1") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrSupplier(iCnt)("Address2") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrSupplier(iCnt)("Address3") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrSupplier(iCnt)("Address4") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrSupplier(iCnt)("Phone1") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrSupplier(iCnt)("Phone2") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrSupplier(iCnt)("Mobile") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrSupplier(iCnt)("Fax") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrSupplier(iCnt)("eMail") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrSupplier(iCnt)("Url") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrSupplier(iCnt)("Terms") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrSupplier(iCnt)("AccountType") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrSupplier(iCnt)("GLAccount1") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrSupplier(iCnt)("GLAccount2") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrSupplier(iCnt)("Remarks") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrSupplier(iCnt)("GSTType") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrSupplier(iCnt)("NRICNo") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrSupplier(iCnt)("Gender") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrSupplier(iCnt)("PassportNo") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrSupplier(iCnt)("DOB") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrSupplier(iCnt)("CreateUser") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrSupplier(iCnt)("CreateDate") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrSupplier(iCnt)("ModifyUser") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrSupplier(iCnt)("ModifyDate") & ""))
                bStatus = fncExecNonQry(sQry)
                If bStatus = False Then Exit Function
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            DrSupplier = Nothing
            sQry = Nothing
            fncUpdateSupplierMaster = bStatus
        End Try
    End Function

    Public Function fncUpdateCurrencyMaster(ByVal DrCurrency As DataRow()) As Boolean
        Dim sQry As String
        Dim iCnt As Integer
        Dim bStatus As Boolean
        Try
            For iCnt = 0 To DrCurrency.Length - 1
                sQry = "EXEC sp_CurrencyHQ " & fncFormatSQLText(Trim(DrCurrency(iCnt)("CurrencyCode") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrCurrency(iCnt)("CurrencyName") & "")) & ", "
                sQry = sQry & Val(Trim(DrCurrency(iCnt)("CurrencyRate") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrCurrency(iCnt)("Remarks") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrCurrency(iCnt)("CreateUser") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrCurrency(iCnt)("CreateDate") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrCurrency(iCnt)("ModifyUser") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrCurrency(iCnt)("ModifyDate") & ""))
                bStatus = fncExecNonQry(sQry)
                If bStatus = False Then Exit Function
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            DrCurrency = Nothing
            sQry = Nothing
            fncUpdateCurrencyMaster = bStatus
        End Try
    End Function

    Public Function fncUpdatePaymodeMaster(ByVal DrPaymode As DataRow()) As Boolean
        Dim sQry As String
        Dim iCnt As Integer
        Dim bStatus As Boolean
        Try
            For iCnt = 0 To DrPaymode.Length - 1
                sQry = "EXEC sp_PaymodeHQ " & Val(Trim(DrPaymode(iCnt)("Position") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrPaymode(iCnt)("Paymode") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrPaymode(iCnt)("Description") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrPaymode(iCnt)("Commission") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrPaymode(iCnt)("GLAccountCode") & "")) & ", "
                sQry = sQry & Val(Trim(DrPaymode(iCnt)("Active") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrPaymode(iCnt)("CreateUser") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrPaymode(iCnt)("CreateDate") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrPaymode(iCnt)("ModifyUser") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrPaymode(iCnt)("ModifyDate") & ""))
                bStatus = fncExecNonQry(sQry)
                If bStatus = False Then Exit Function
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            DrPaymode = Nothing
            sQry = Nothing
            fncUpdatePaymodeMaster = bStatus
        End Try
    End Function

    Public Function fncUpdateTrayMaster(ByVal DrTray As DataRow()) As Boolean
        Dim sQry As String
        Dim iCnt As Integer
        Dim bStatus As Boolean
        Try
            For iCnt = 0 To DrTray.Length - 1
                sQry = "EXEC sp_TrayMasterHQ " & fncFormatSQLText(Trim(DrTray(iCnt)("TrayID") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrTray(iCnt)("TrayName") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrTray(iCnt)("SalesManID") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrTray(iCnt)("SalesManName") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrTray(iCnt)("IsCustomerTray") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrTray(iCnt)("CreateUser") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrTray(iCnt)("CreateDate") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrTray(iCnt)("ModifyUser") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrTray(iCnt)("ModifyDate") & ""))
                bStatus = fncExecNonQry(sQry)
                If bStatus = False Then Exit Function
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            DrTray = Nothing
            sQry = Nothing
            fncUpdateTrayMaster = bStatus
        End Try
    End Function

    Public Function fncUpdateClassRate(ByVal DrClassRate As DataRow()) As Boolean
        Dim sQry As String
        Dim iCnt As Integer
        Dim bStatus As Boolean
        Try
            For iCnt = 0 To DrClassRate.Length - 1

                sQry = "EXEC sp_ClassRateHQ " & fncFormatSQLText(Trim(DrClassRate(iCnt)("ClassCode") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrClassRate(iCnt)("ClassName") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrClassRate(iCnt)("FromDate") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrClassRate(iCnt)("ToDate") & "")) & ", "
                sQry = sQry & Val(Trim(DrClassRate(iCnt)("Rate") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrClassRate(iCnt)("CreateUser") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrClassRate(iCnt)("CreateDate") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrClassRate(iCnt)("ModifyUser") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrClassRate(iCnt)("ModifyDate") & ""))
                bStatus = fncExecNonQry(sQry)
                If bStatus = False Then Exit Function
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            DrClassRate = Nothing
            sQry = Nothing
            fncUpdateClassRate = bStatus
        End Try
    End Function
    Public Function fncUpdatePayOutMaster(ByVal DrClassRate As DataRow()) As Boolean
        Dim sQry As String
        Dim iCnt As Integer
        Dim bStatus As Boolean
        Try
            For iCnt = 0 To DrClassRate.Length - 1
                sQry = "EXEC sp_PayOutMasterHQ " & fncFormatSQLText(Trim(DrClassRate(iCnt)("Code") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrClassRate(iCnt)("Description") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrClassRate(iCnt)("Remarks") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrClassRate(iCnt)("CreateUser") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrClassRate(iCnt)("CreateDate") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrClassRate(iCnt)("ModifyUser") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrClassRate(iCnt)("ModifyDate") & ""))
                bStatus = fncExecNonQry(sQry)
                If bStatus = False Then Exit Function
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            DrClassRate = Nothing
            sQry = Nothing
            fncUpdatePayOutMaster = bStatus
        End Try
    End Function
    Public Function fncUpdateSizeMaster(ByVal DrClassRate As DataRow()) As Boolean
        Dim sQry As String
        Dim iCnt As Integer
        Dim bStatus As Boolean
        Try

            For iCnt = 0 To DrClassRate.Length - 1
                sQry = "EXEC sp_SizeMasterHQ " & fncFormatSQLText(Trim(DrClassRate(iCnt)("CategoryCode") & "")) & ", "
                sQry = sQry & Val(Trim(DrClassRate(iCnt)("SizeCode") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrClassRate(iCnt)("SizeType") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrClassRate(iCnt)("Remarks") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrClassRate(iCnt)("CreateUser") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrClassRate(iCnt)("CreateDate") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrClassRate(iCnt)("ModifyUser") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrClassRate(iCnt)("ModifyDate") & ""))
                bStatus = fncExecNonQry(sQry)
                If bStatus = False Then Exit Function
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            DrClassRate = Nothing
            sQry = Nothing
            fncUpdateSizeMaster = bStatus
        End Try
    End Function
    Public Function fncUpdateUserMaster(ByVal DrUser As DataRow()) As Boolean
        Dim sQry As String
        Dim iCnt As Integer
        Dim bStatus As Boolean
        Try
            For iCnt = 0 To DrUser.Length - 1
                sQry = "EXEC sp_UserMasterHQ " & fncFormatSQLText(Trim(DrUser(iCnt)("UserCode") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrUser(iCnt)("UserGroupCode") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrUser(iCnt)("UserName") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrUser(iCnt)("Password") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrUser(iCnt)("ExpiryDate") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrUser(iCnt)("Designation") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrUser(iCnt)("Email") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrUser(iCnt)("Phone") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrUser(iCnt)("Fax") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrUser(iCnt)("StoreCode") & "")) & ", "
                sQry = sQry & Val(Trim(DrUser(iCnt)("POLimit") & "")) & ", "
                sQry = sQry & Val(Trim(DrUser(iCnt)("Admin") & "")) & ", "
                sQry = sQry & Val(Trim(DrUser(iCnt)("UpdateApprovedPo") & "")) & ", "
                sQry = sQry & Val(Trim(DrUser(iCnt)("UpdateGst") & "")) & ", "
                sQry = sQry & Val(Trim(DrUser(iCnt)("DayOfValid") & "")) & ", "
                sQry = sQry & Val(Trim(DrUser(iCnt)("DaysOfWarning") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrUser(iCnt)("ChangePassword") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrUser(iCnt)("ChangeDate") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrUser(iCnt)("FirstLogDate") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrUser(iCnt)("LastLogDate") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrUser(iCnt)("CreateDate") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrUser(iCnt)("CreateUser") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrUser(iCnt)("ModifyDate") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrUser(iCnt)("ModifyUser") & ""))
                bStatus = fncExecNonQry(sQry)
                If bStatus = False Then Exit Function
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            DrUser = Nothing
            sQry = Nothing
            fncUpdateUserMaster = bStatus
        End Try
    End Function

    Public Function fncUpdateUserPermissionMaster(ByVal DrUserGroup As DataRow()) As Boolean
        Dim sQry As String
        Dim iCnt As Integer
        Dim bStatus As Boolean
        Try
            For iCnt = 0 To DrUserGroup.Length - 1
                sQry = "EXEC sp_UserPermissionHQ " & fncFormatSQLText(Trim(DrUserGroup(iCnt)("UserGroupCode") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrUserGroup(iCnt)("FormCode") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrUserGroup(iCnt)("AllowAdd") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrUserGroup(iCnt)("AllowUpdate") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrUserGroup(iCnt)("AllowDelete") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrUserGroup(iCnt)("AllowRead") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrUserGroup(iCnt)("CreateUser") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrUserGroup(iCnt)("CreateDate") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrUserGroup(iCnt)("ModifyUser") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrUserGroup(iCnt)("ModifyDate") & ""))
                bStatus = fncExecNonQry(sQry)
                If bStatus = False Then Exit Function
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            DrUserGroup = Nothing
            sQry = Nothing
            fncUpdateUserPermissionMaster = bStatus
        End Try
    End Function
    Public Function fncUpdateUserGroupMaster(ByVal DrUserGroup As DataRow()) As Boolean
        Dim sQry As String
        Dim iCnt As Integer
        Dim bStatus As Boolean
        Try
            For iCnt = 0 To DrUserGroup.Length - 1
                sQry = "EXEC sp_UserGroupHQ " & fncFormatSQLText(Trim(DrUserGroup(iCnt)("UserGroupCode") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrUserGroup(iCnt)("UserGroupName") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrUserGroup(iCnt)("CreateUser") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrUserGroup(iCnt)("CreateDate") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrUserGroup(iCnt)("ModifyUser") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrUserGroup(iCnt)("ModifyDate") & ""))
                bStatus = fncExecNonQry(sQry)
                If bStatus = False Then Exit Function
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            DrUserGroup = Nothing
            sQry = Nothing
            fncUpdateUserGroupMaster = bStatus
        End Try
    End Function
    Public Function fncGetTransferDetailFromLocal(ByVal sFromDate As String, ByVal sToDate As String) As DataSet
        Dim dsTD As New DataSet
        Dim sQry As String
        Try
            sQry = "Select * from TblInventoryTransferHeader Where Convert(Char(8), ModifyDate, 112) Between " & fncFormatSQLText(sFromDate) & " And " & fncFormatSQLText(sToDate) & vbCrLf
            sQry &= "Select * from TblInventoryTransferDetail Where TransferNo In (Select TransferNo from TblInventoryTransferHeader Where Convert(Char(8), ModifyDate, 112) Between " & fncFormatSQLText(sFromDate) & " And " & fncFormatSQLText(sToDate) & ")"
            dsTD = dm.getDataSetWithSqlAdapter(sQry, "Transfer", "Transfer")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncGetTransferDetailFromLocal = dsTD
            dsTD = Nothing
        End Try
    End Function
    Public Function fncGetOnlineTransferDetailFromLocal(ByVal sFromDate As String, ByVal sToDate As String) As DataSet
        Dim dsTD As New DataSet
        Dim sQry As String
        Try
            sQry = "Select * from TblInventoryOnlineTransferHeader Where Convert(Char(8), ModifyDate, 112) Between " & fncFormatSQLText(sFromDate) & " And " & fncFormatSQLText(sToDate) & vbCrLf
            sQry &= "Select * from TblInventoryOnlineTransferDetail Where TransferNo In (Select TransferNo from TblInventoryOnlineTransferHeader Where Convert(Char(8), ModifyDate, 112) Between " & fncFormatSQLText(sFromDate) & " And " & fncFormatSQLText(sToDate) & ")"
            dsTD = dm.getDataSetWithSqlAdapter(sQry, "Transfer", "Transfer")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncGetOnlineTransferDetailFromLocal = dsTD
            dsTD = Nothing
        End Try
    End Function
    Public Function fncGetSalesTransferDetailFromLocal(ByVal sFromDate As String, ByVal sToDate As String) As DataSet
        Dim dsTD As New DataSet
        Dim sQry As String
        Try
            sQry = "Select * from tblInventorySalesTransferHeader Where Convert(Char(8), ModifyDate, 112) Between " & fncFormatSQLText(sFromDate) & " And " & fncFormatSQLText(sToDate) & vbCrLf
            sQry &= "Select * from tblInventorySalesTransferDetail Where TransferNo In (Select TransferNo from tblInventorySalesTransferHeader Where Convert(Char(8), ModifyDate, 112) Between " & fncFormatSQLText(sFromDate) & " And " & fncFormatSQLText(sToDate) & ")"
            dsTD = dm.getDataSetWithSqlAdapter(sQry, "Transfer", "Transfer")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncGetSalesTransferDetailFromLocal = dsTD
            dsTD = Nothing
        End Try
    End Function
    Public Function fncGetTransferDetailFromHQ(ByVal sFromDate As String, ByVal sToDate As String) As DataSet
        Dim dsTD As New DataSet
        Dim sQry As String
        Try
            sQry = "Select * from tblInventoryTransferHeader Where Convert(Char(8), ModifyDate, 112) Between " & fncFormatSQLText(sFromDate) & " And " & fncFormatSQLText(sToDate) & " And Fromlocation = " & fncFormatSQLText(gthisLocation) & " And ToLocStatus <> 0 " & vbCrLf
            sQry &= "Select * from tblInventoryTransferDetail Where TransferNo In (Select TransferNo from tblInventoryTransferHeader Where Convert(Char(8), ModifyDate, 112) Between " & fncFormatSQLText(sFromDate) & " And " & fncFormatSQLText(sToDate) & " And Fromlocation = " & fncFormatSQLText(gthisLocation) & " And ToLocStatus <> 0 )"

            'sQry = "Select * from tblInventoryTransferHeader Where Convert(Char(8), ModifyDate, 112) Between " & fncFormatSQLText(sFromDate) & " And " & fncFormatSQLText(sToDate) & " And ToLocation = " & fncFormatSQLText(gthisLocation) & " And ToLocStatus <> 0 " & vbCrLf
            'sQry &= "Select * from tblInventoryTransferDetail Where TransferNo In (Select TransferNo from tblInventoryTransferHeader Where Convert(Char(8), ModifyDate, 112) Between " & fncFormatSQLText(sFromDate) & " And " & fncFormatSQLText(sToDate) & " And ToLocation = " & fncFormatSQLText(gthisLocation) & " And ToLocStatus <> 0 )"
            dsTD = dm.getDataSetWithSqlAdapterAcc(sQry, "Transfer", "Transfer")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncGetTransferDetailFromHQ = dsTD
            dsTD = Nothing
        End Try
    End Function
    Public Function fncGetDailyClassRateFromHQ(ByVal sFromDate As String, ByVal sToDate As String) As DataSet
        Dim dsTD As New DataSet
        Dim sQry As String
        Try
            sQry = "select * from tblClassRate" & vbCrLf
            'sQry &= "Select * from tblInventoryTransferDetail Where TransferNo In (Select TransferNo from tblInventoryTransferHeader Where Convert(Char(8), ModifyDate, 112) Between " & fncFormatSQLText(sFromDate) & " And " & fncFormatSQLText(sToDate) & " And Fromlocation = " & fncFormatSQLText(gthisLocation) & " And ToLocStatus <> 0 )"
            dsTD = dm.getDataSetWithSqlAdapterAcc(sQry, "ClassRate", "ClassRate")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncGetDailyClassRateFromHQ = dsTD
            dsTD = Nothing
        End Try
    End Function
    Public Function fncGetSalesTransferDetailFromHQ(ByVal sFromDate As String, ByVal sToDate As String) As DataSet
        Dim dsTD As New DataSet
        Dim sQry As String
        Try
            sQry = "Select * from tblInventorySalesTransferHeader Where Convert(Char(8), ModifyDate, 112) Between " & fncFormatSQLText(sFromDate) & " And " & fncFormatSQLText(sToDate) & " And Fromlocation = " & fncFormatSQLText(gthisLocation) & " And ToLocStatus <> 0 " & vbCrLf
            'sQry &= "Select * from tblInventoryTransferDetail Where TransferNo In (Select TransferNo from tblInventoryTransferHeader Where Convert(Char(8), ModifyDate, 112) Between " & fncFormatSQLText(sFromDate) & " And " & fncFormatSQLText(sToDate) & " And Fromlocation = " & fncFormatSQLText(gthisLocation) & " And ToLocStatus <> 0 )"
            dsTD = dm.getDataSetWithSqlAdapterAcc(sQry, "Transfer", "Transfer")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncGetSalesTransferDetailFromHQ = dsTD
            dsTD = Nothing
        End Try
    End Function
    Public Function fncUpdateTransferDetailHQ(ByVal dsTD As DataSet) As Boolean
        Dim sQry As String
        Dim iCnt As Integer
        Dim bStatus As Boolean
        Try
            For iCnt = 0 To dsTD.Tables(0).Rows.Count - 1
                sQry = " Exec sp_TransferHeaderSave "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(0).Rows(iCnt)("TransferNo") & "")) & ", "
                sQry &= fncFormatSQLDate(Trim(dsTD.Tables(0).Rows(iCnt)("TransferDate") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(0).Rows(iCnt)("FromLocation") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(0).Rows(iCnt)("ToLocation") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(0).Rows(iCnt)("Totalweight") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(0).Rows(iCnt)("TotalPCS") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(0).Rows(iCnt)("TotalCost") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(0).Rows(iCnt)("TotalPMC") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(0).Rows(iCnt)("TotalSMC") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(0).Rows(iCnt)("FromLocStatus") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(0).Rows(iCnt)("ToLocStatus") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(0).Rows(iCnt)("CreateUser") & "")) & ", "
                sQry &= fncFormatSQLText(sUserID) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(0).Rows(iCnt)("Remarks") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(0).Rows(iCnt)("SalesmanCode") & ""))
                bStatus = fncExecNonQryHQ(sQry)
                If bStatus = False Then Exit Function
            Next
            For iCnt = 0 To dsTD.Tables(1).Rows.Count - 1
                sQry = "Exec sp_TransferDetailSave "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(1).Rows(iCnt)("TransferNo") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(1).Rows(iCnt)("InventoryCode") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(1).Rows(iCnt)("Description") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(1).Rows(iCnt)("ClassCode") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(1).Rows(iCnt)("CategoryCode") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(1).Rows(iCnt)("CatalogueCode") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("QtyOnHand") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("Weight") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("SizeCode") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("CostPerGram") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("PMC") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("SMC") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("TotalCost") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("TransferTag") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("CreateUser") & "")) & ", "
                sQry &= fncFormatSQLText(sUserID) & ", "
                sQry &= fncFormatSQLText("I")
                'sQry &= fncFormatSQLText(Trim(dsTD.Tables(1).Rows(iCnt)("TransferNo") & "")) & ", "
                'sQry &= fncFormatSQLText(Trim(dsTD.Tables(1).Rows(iCnt)("InventoryCode") & "")) & ", "
                'sQry &= fncFormatSQLText(Trim(dsTD.Tables(1).Rows(iCnt)("Description") & "")) & ", "
                'sQry &= fncFormatSQLText(Trim(dsTD.Tables(1).Rows(iCnt)("ClassCode") & "")) & ", "
                'sQry &= fncFormatSQLText(Trim(dsTD.Tables(1).Rows(iCnt)("CategoryCode") & "")) & ", "
                'sQry &= fncFormatSQLText(Trim(dsTD.Tables(1).Rows(iCnt)("CatalogueCode") & "")) & ", "
                'sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("QtyOnHand") & "")) & ", "
                'sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("Weight") & "")) & ", "
                'sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("CostPerGram") & "")) & ", "
                'sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("PMC") & "")) & ", "
                'sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("SMC") & "")) & ", "
                'sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("PCS") & "")) & ", "
                'sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("TotalCost") & "")) & ", "
                'sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("MarginPerc") & "")) & ", "
                'sQry &= fncFormatSQLText(sUserID) & ", "
                'sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("SizeCode") & "")) & ", "
                'sQry &= fncFormatSQLText(Trim(dsTD.Tables(1).Rows(iCnt)("TransferTag") & ""))
                bStatus = fncExecNonQryHQ(sQry)
                If bStatus = False Then Exit Function
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsTD = Nothing
            sQry = Nothing
            fncUpdateTransferDetailHQ = bStatus
        End Try
    End Function
    Public Function fncUpdateTransferDetailLocalHQ(ByVal dsTD As DataSet) As Boolean
        Dim sQry As String
        Dim iCnt As Integer
        Dim bStatus As Boolean
        Try

            For iCnt = 0 To dsTD.Tables(0).Rows.Count - 1
                sQry = " Exec sp_LocalTransferHeaderSave "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(0).Rows(iCnt)("TransferNo") & "")) & ", "
                sQry &= fncFormatSQLDate(Trim(dsTD.Tables(0).Rows(iCnt)("TransferDate") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(0).Rows(iCnt)("FromLocation") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(0).Rows(iCnt)("FromLocStatus") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(0).Rows(iCnt)("ToLocation") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(0).Rows(iCnt)("ToLocStatus") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(0).Rows(iCnt)("Totalweight") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(0).Rows(iCnt)("TotalCost") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(0).Rows(iCnt)("TotalPCS") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(0).Rows(iCnt)("TotalPMC") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(0).Rows(iCnt)("TotalSMC") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(0).Rows(iCnt)("Remarks") & "")) & ", "
                sQry &= fncFormatSQLText(sUserID)
                bStatus = fncExecNonQryHQ(sQry)
                If bStatus = False Then Exit Function
            Next
            For iCnt = 0 To dsTD.Tables(1).Rows.Count - 1
                sQry = "Exec sp_LocalTransferDetailSave "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(1).Rows(iCnt)("TransferNo") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(1).Rows(iCnt)("InventoryCode") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(1).Rows(iCnt)("Description") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(1).Rows(iCnt)("ClassCode") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(1).Rows(iCnt)("CategoryCode") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(1).Rows(iCnt)("CatalogueCode") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("QtyOnHand") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("Weight") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("SizeCode") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("CostPerGram") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("PMC") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("SMC") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("PCS") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("TotalCost") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("TransferTag") & "")) & ", "
                sQry &= fncFormatSQLText(sUserID)
                bStatus = fncExecNonQryHQ(sQry)
                If bStatus = False Then Exit Function
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsTD = Nothing
            sQry = Nothing
            fncUpdateTransferDetailLocalHQ = bStatus
        End Try
    End Function
    Public Function fncUpdateOnlineTransferDetailHQ(ByVal dsTD As DataSet) As Boolean
        Dim sQry As String
        Dim iCnt As Integer
        Dim bStatus As Boolean
        Try
            For iCnt = 0 To dsTD.Tables(0).Rows.Count - 1
                sQry = " Exec sp_OnlineTransferHeaderSave "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(0).Rows(iCnt)("TransferNo") & "")) & ", "
                sQry &= fncFormatSQLDate(Trim(dsTD.Tables(0).Rows(iCnt)("TransferDate") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(0).Rows(iCnt)("FromLocation") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(0).Rows(iCnt)("FromLocStatus") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(0).Rows(iCnt)("ToLocation") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(0).Rows(iCnt)("ToLocStatus") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(0).Rows(iCnt)("Remarks") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(0).Rows(iCnt)("Totalweight") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(0).Rows(iCnt)("TotalPCS") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(0).Rows(iCnt)("TotalPMC") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(0).Rows(iCnt)("TotalSMC") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(0).Rows(iCnt)("TotalCost") & "")) & ", "
                sQry &= fncFormatSQLText(sUserID)
                bStatus = fncExecNonQryHQ(sQry)
                If bStatus = False Then Exit Function
            Next
            For iCnt = 0 To dsTD.Tables(1).Rows.Count - 1
                sQry = "Exec sp_OnlineTransferDetailSave "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(1).Rows(iCnt)("TransferNo") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(1).Rows(iCnt)("InventoryCode") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(1).Rows(iCnt)("Description") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(1).Rows(iCnt)("ClassCode") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(1).Rows(iCnt)("CategoryCode") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(1).Rows(iCnt)("CatalogueCode") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("QtyOnHand") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("Weight") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("CostPerGram") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("PMC") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("SMC") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("PCS") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("TotalCost") & "")) & ", "
                sQry &= fncFormatSQLText(sUserID) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("SizeCode") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(1).Rows(iCnt)("TransferTag") & ""))
                bStatus = fncExecNonQryHQ(sQry)
                If bStatus = False Then Exit Function
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsTD = Nothing
            sQry = Nothing
            fncUpdateOnlineTransferDetailHQ = bStatus
        End Try
    End Function
    Public Function fncUpdateSalesTransferDetailHQ(ByVal dsTD As DataSet) As Boolean
        Dim sQry As String
        Dim iCnt As Integer
        Dim bStatus As Boolean
        Try
            For iCnt = 0 To dsTD.Tables(0).Rows.Count - 1
                sQry = " Exec sp_SalesTransferHeaderSave "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(0).Rows(iCnt)("TransferNo") & "")) & ", "
                sQry &= fncFormatSQLDate(Trim(dsTD.Tables(0).Rows(iCnt)("TransferDate") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(0).Rows(iCnt)("FromLocation") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(0).Rows(iCnt)("FromLocStatus") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(0).Rows(iCnt)("ToLocation") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(0).Rows(iCnt)("ToLocStatus") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(0).Rows(iCnt)("Remarks") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(0).Rows(iCnt)("Totalweight") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(0).Rows(iCnt)("TotalPCS") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(0).Rows(iCnt)("TotalPMC") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(0).Rows(iCnt)("TotalSMC") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(0).Rows(iCnt)("TotalCost") & "")) & ", "
                sQry &= fncFormatSQLText(sUserID)
                bStatus = fncExecNonQryHQ(sQry)
                If bStatus = False Then Exit Function
            Next
            For iCnt = 0 To dsTD.Tables(1).Rows.Count - 1
                sQry = "Exec sp_SalesTransferDetailSave "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(1).Rows(iCnt)("TransferNo") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(1).Rows(iCnt)("InventoryCode") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(1).Rows(iCnt)("Description") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(1).Rows(iCnt)("ClassCode") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(1).Rows(iCnt)("CategoryCode") & "")) & ", "
                sQry &= fncFormatSQLText(Trim(dsTD.Tables(1).Rows(iCnt)("CatalogueCode") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("QtyOnHand") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("Weight") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("CostPerGram") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("PMC") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("SMC") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("PCS") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("TotalCost") & "")) & ", "
                sQry &= Val(Trim(dsTD.Tables(1).Rows(iCnt)("MarginPerc") & "")) & ", "
                sQry &= fncFormatSQLText(sUserID)
                bStatus = fncExecNonQryHQ(sQry)
                If bStatus = False Then Exit Function
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsTD = Nothing
            sQry = Nothing
            fncUpdateSalesTransferDetailHQ = bStatus
        End Try
    End Function
    Public Function fncUpdateTransferDetailLocal(ByVal drTH As DataRow()) As Boolean
        Dim sQry As String
        Dim iCnt As Integer
        Dim bStatus As Boolean
        Try
            For iCnt = 0 To drTH.Length - 1
                sQry = " Update tblInventoryTransferHeader Set "
                sQry &= " ToLocStatus = " & fncFormatSQLText(drTH(iCnt)("ToLocStatus") & "") & ", "
                sQry &= " ModifyUser = " & fncFormatSQLText(sUserID) & ", "
                sQry &= " ModifyDate = GetDate()"
                sQry &= " Where TransferNo = " & fncFormatSQLText(drTH(iCnt)("TransferNo") & "")
                bStatus = fncExecNonQry(sQry)
                If bStatus = False Then Exit Function
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            drTH = Nothing
            sQry = Nothing
            fncUpdateTransferDetailLocal = bStatus
        End Try
    End Function
    Public Function fncUpdateSalesTransferDetailLocal(ByVal drTH As DataRow()) As Boolean
        Dim sQry As String
        Dim iCnt As Integer
        Dim bStatus As Boolean
        Try
            For iCnt = 0 To drTH.Length - 1
                sQry = " Update tblInventorySalesTransferHeader Set "
                sQry &= " ToLocStatus = " & fncFormatSQLText(drTH(iCnt)("ToLocStatus") & "") & ", "
                sQry &= " ModifyUser = " & fncFormatSQLText(sUserID) & ", "
                sQry &= " ModifyDate = GetDate()"
                sQry &= " Where TransferNo = " & fncFormatSQLText(drTH(iCnt)("TransferNo") & "")
                bStatus = fncExecNonQry(sQry)
                If bStatus = False Then Exit Function
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            drTH = Nothing
            sQry = Nothing
            fncUpdateSalesTransferDetailLocal = bStatus
        End Try
    End Function
    Public Sub subOpenConnection()
        gsUpdateCon.ConnectionString = gsCon
        gsUpdateCon.Open()
    End Sub

    Public Sub subBeginTransaction()
        gsUpdateTrans = gsUpdateCon.BeginTransaction
    End Sub

    Public Sub subCommitTransaction()
        gsUpdateTrans.Commit()
    End Sub

    Public Sub subRollBackTransaction()
        gsUpdateTrans.Rollback()
    End Sub

    Public Sub subCloseConnection()
        gsUpdateCon.Close()
    End Sub

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

    Public Sub subOpenConnectionHQ()
        gsTransferConHQ.ConnectionString = gsConAcc
        gsTransferConHQ.Open()
    End Sub

    Public Sub subBeginTransactionHQ()
        gsTransferTransHQ = gsTransferConHQ.BeginTransaction
    End Sub

    Public Sub subCommitTransactionHQ()
        gsTransferTransHQ.Commit()
    End Sub

    Public Sub subRollBackTransactionHQ()
        gsTransferTransHQ.Rollback()
    End Sub

    Public Sub subCloseConnectionHQ()
        gsTransferConHQ.Close()
    End Sub

    Public Function fncExecNonQryHQ(ByVal Query As String) As Boolean
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
            fncExecNonQryHQ = bStatus
        End Try
    End Function
    Public Function fncUpdateDailyMarketRate(ByVal DrRate As DataRow()) As Boolean
        Dim sQry As String
        Dim iCnt As Integer
        Dim bStatus As Boolean
        Try
            For iCnt = 0 To DrRate.Length - 1
                sQry = "EXEC sp_DailyClassRate " & fncFormatSQLText(Trim(DrRate(iCnt)("ClassCode") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrRate(iCnt)("ClassName") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrRate(iCnt)("FromDate") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrRate(iCnt)("ToDate") & "")) & ", "
                sQry = sQry & Val(Trim(DrRate(iCnt)("Rate") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrRate(iCnt)("CreateUser") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrRate(iCnt)("CreateDate") & "")) & ", "
                sQry = sQry & fncFormatSQLText(Trim(DrRate(iCnt)("ModifyUser") & "")) & ", "
                sQry = sQry & fncFormatSQLDate(Trim(DrRate(iCnt)("ModifyDate") & ""))
                bStatus = fncExecNonQry(sQry)
                If bStatus = False Then Exit Function
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            DrRate = Nothing
            sQry = Nothing
            fncUpdateDailyMarketRate = bStatus
        End Try
    End Function
    Public Function fncUpdateInventoryStockToHQOLD(ByVal dsInvStk As DataSet) As Boolean
        Dim sQry As String
        Dim sQuery As String
        Dim iCnt As Integer
        Dim bStatus As Boolean
        Try
            
            For iCnt = 0 To dsInvStk.Tables(0).Rows.Count - 1
                sQry = "EXEC sp_UpdateTransferInventoryTOHQ "
                sQry = sQry & fncFormatSQLText(gthisLocation) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsInvStk.Tables(0).Rows(iCnt)("InventoryCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsInvStk.Tables(0).Rows(iCnt)("Description") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsInvStk.Tables(0).Rows(iCnt)("DiamondDescription") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(0).Rows(iCnt)("Weight") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(0).Rows(iCnt)("StoneWeight") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(0).Rows(iCnt)("CostPerGram") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(0).Rows(iCnt)("PMC") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(0).Rows(iCnt)("SMC") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(0).Rows(iCnt)("GoldLoss") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(0).Rows(iCnt)("TotalCost") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsInvStk.Tables(0).Rows(iCnt)("JobSheetNo") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsInvStk.Tables(0).Rows(iCnt)("PurchaseBillNo") & "")) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsInvStk.Tables(0).Rows(iCnt)("PurchaseDate") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsInvStk.Tables(0).Rows(iCnt)("SupplierCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsInvStk.Tables(0).Rows(iCnt)("SalesBillNo") & "")) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsInvStk.Tables(0).Rows(iCnt)("SalesDate") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(0).Rows(iCnt)("SalesCostPerGram") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(0).Rows(iCnt)("SalesMC") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(0).Rows(iCnt)("SalesAmount") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsInvStk.Tables(0).Rows(iCnt)("CategoryCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsInvStk.Tables(0).Rows(iCnt)("ClassCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsInvStk.Tables(0).Rows(iCnt)("CatalogueCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsInvStk.Tables(0).Rows(iCnt)("TrayId") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsInvStk.Tables(0).Rows(iCnt)("PMCType") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsInvStk.Tables(0).Rows(iCnt)("SMCType") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsInvStk.Tables(0).Rows(iCnt)("OrderNo") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsInvStk.Tables(0).Rows(iCnt)("Remarks") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(0).Rows(iCnt)("SizeCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(sUserID)
                'DM.ProjExecNonQry(sQry, gsConRemote)
                bStatus = fncExecNonQryHQ(sQry)
                If bStatus = False Then Exit Function
            Next

            For iCnt = 0 To dsInvStk.Tables(1).Rows.Count - 1
                sQry = "EXEC sp_UpdateTransferInventoryStockTOHQ "
                sQry = sQry & fncFormatSQLText(Trim(dsInvStk.Tables(1).Rows(iCnt)("LocationCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsInvStk.Tables(1).Rows(iCnt)("InventoryCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsInvStk.Tables(1).Rows(iCnt)("Description") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsInvStk.Tables(1).Rows(iCnt)("DiamondDescription") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(1).Rows(iCnt)("Weight") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(1).Rows(iCnt)("PCS") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(1).Rows(iCnt)("TotalCost") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(1).Rows(iCnt)("QtyOnHand") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(1).Rows(iCnt)("QtySold") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(1).Rows(iCnt)("OrderQty") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(1).Rows(iCnt)("ConsignmentQty") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsInvStk.Tables(1).Rows(iCnt)("Status") & "")) & ","
                sQry = sQry & fncFormatSQLText(sUserID)
                'DM.ProjExecNonQry(sQry, gsConRemote)
                bStatus = fncExecNonQryHQ(sQry)
                If bStatus = False Then Exit Function
            Next


            'sQuery = "EXEC sp_UpdateOnlineLog '', " & fncFormatSQLText("STK")
            'DM.ProjExecNonQry(sQuery, gsCon)
            '  If bStatus = False Then Exit Function

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateInventoryStockToHQOLD = bStatus
        End Try
    End Function
    Public Function fncUpdateInventoryStockToHQ(ByVal dsInvStk As DataSet) As Boolean
        Dim sQry As String
        Dim sQuery As String
        Dim iCnt As Integer
        Dim bStatus As Boolean
        Try
 
            For iCnt = 0 To dsInvStk.Tables(1).Rows.Count - 1
                sQry = "EXEC sp_UpdateTransferInventoryStockTOHQ "
                sQry = sQry & fncFormatSQLText(Trim(dsInvStk.Tables(1).Rows(iCnt)("LocationCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsInvStk.Tables(1).Rows(iCnt)("InventoryCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsInvStk.Tables(1).Rows(iCnt)("Description") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsInvStk.Tables(1).Rows(iCnt)("DiamondDescription") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(1).Rows(iCnt)("Weight") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(1).Rows(iCnt)("PCS") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(1).Rows(iCnt)("TotalCost") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(1).Rows(iCnt)("QtyOnHand") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(1).Rows(iCnt)("QtySold") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(1).Rows(iCnt)("OrderQty") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(1).Rows(iCnt)("ConsignmentQty") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsInvStk.Tables(1).Rows(iCnt)("Status") & "")) & ","
                sQry = sQry & fncFormatSQLText(sUserID)
                'DM.ProjExecNonQry(sQry, gsConRemote)
                bStatus = fncExecNonQryHQ(sQry)
                If bStatus = False Then Exit Function
            Next

            'sQuery = "EXEC sp_UpdateOnlineLog '', " & fncFormatSQLText("STK")
            'DM.ProjExecNonQry(sQuery, gsCon)
            '  If bStatus = False Then Exit Function

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateInventoryStockToHQ = bStatus
        End Try
    End Function
End Class
