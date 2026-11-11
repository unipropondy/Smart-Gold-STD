Public Class clsGoldBarPurchase
    Dim dm As New DataManager
    Public Function GoldBarPurchaseSave(ByVal sInvoiceNo As String, ByVal sInvoiceDate As String, _
                                        ByVal sSupplierCode As String, ByVal sCurrencyCode As String, _
                                        ByVal dCurrencyRate As Double, ByVal sClassCode As String, ByVal sDescription As String, ByVal dTotalWeight As Double, _
                                        ByVal dTotalQty As Double, ByVal dIssuedQty As Double, ByVal dBalanceQty As Double, ByVal dTotalCost As Double, ByVal dTotalGST As Double, _
                                        ByVal sRemarks As String, ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            GoldBarPurchaseSave = False
            sQry = sQry & "EXEC sp_GoldBarPurchaseNew "
            sQry = sQry & fncFormatSQLText(sInvoiceNo) & ","
            sQry = sQry & fncFormatSQLDate(sInvoiceDate) & ","
            sQry = sQry & fncFormatSQLText(sSupplierCode) & ","
            sQry = sQry & fncFormatSQLText(sClassCode) & ","
            sQry = sQry & fncFormatSQLText(sDescription) & ","
            sQry = sQry & fncFormatSQLText(sCurrencyCode) & ","
            sQry = sQry & Val(dCurrencyRate) & ","
            sQry = sQry & Val(dTotalWeight) & ","
            sQry = sQry & Val(dTotalQty) & ","
            sQry = sQry & Val(dIssuedQty) & ","
            sQry = sQry & Val(dBalanceQty) & ","
            sQry = sQry & Val(dTotalGST) & ","
            sQry = sQry & Val(dTotalCost) & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & fncFormatSQLText(sUserId)
            SaveStatus = dm.ProjExecNonQry(sQry)
            GoldBarPurchaseSave = SaveStatus
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            GoldBarPurchaseSave = False
        End Try
    End Function
    Public Function GetInventoryDetail(ByVal sInventoryCode As String) As DataSet
        Dim ds As New DataSet
        Dim sSql As String

        sSql = "SELECT *,Pdate = CONVERT(CHAR(8),PurchaseDate,112) FROM vwInventoryStock WHERE InventoryCode= " & fncFormatSQLText(sInventoryCode) & " and qtyonhand > 0 "
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

    Public Function GoldBarNoSave(ByVal sGoldBarNo As String, ByVal sPBillNo As String, _
                                    ByVal sPBillDate As String, ByVal sPayableType As String, _
                                    ByVal sSupplierCode As String, ByVal sCurrencyCode As String, _
                                    ByVal sCurrencyRate As Double, ByVal sCatalogueCode As String, _
                                    ByVal sCategoryCode As String, ByVal sDescription As String, _
                                    ByVal sRemarks As String, ByVal sTranType As String, _
                                    ByVal sTaelGST As String) As Boolean
        Dim sQry As String
        Dim Status As Boolean
        Try
            GoldBarNoSave = False

            sQry = "Exec sp_GoldBarNoSave "
            sQry = sQry & fncFormatSQLText(sGoldBarNo)
            sQry = sQry & ", " & fncFormatSQLText(sPBillNo)
            sQry = sQry & ", " & fncFormatSQLDate(sPBillDate)
            sQry = sQry & ", " & fncFormatSQLText(sPayableType)
            sQry = sQry & ", " & fncFormatSQLText(sSupplierCode)
            sQry = sQry & ", " & fncFormatSQLText(sCurrencyCode)
            sQry = sQry & ", " & Val(sCurrencyRate)
            sQry = sQry & ", " & fncFormatSQLText(sCatalogueCode)
            sQry = sQry & ", " & fncFormatSQLText(sCategoryCode)
            sQry = sQry & ", " & fncFormatSQLText(sDescription)
            sQry = sQry & ", " & fncFormatSQLText(sRemarks)
            sQry = sQry & ", " & fncFormatSQLText(sTranType)
            sQry = sQry & ", " & fncFormatSQLText(sTaelGST)
            sQry = sQry & ", " & fncFormatSQLText(sUserID)
            sQry = sQry & ", " & fncFormatSQLText(gthisLocation)
            Status = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            GoldBarNoSave = False
        Finally
            GoldBarNoSave = True
        End Try
    End Function
    Public Function GetGoldBar(ByVal sGoldBarNo As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String

        Try
            GetGoldBar = Nothing
            sQry = "SELECT *,Pdate = CONVERT(CHAR(8),PurchaseBillDate,112) FROM tblGoldBarPurchase WHERE PurchaseNo =" & fncFormatSQLText(sGoldBarNo)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "JobSheet", "JobSheet")

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            Ds = Nothing
        Finally
            GetGoldBar = Ds
            Ds = Nothing
        End Try
    End Function
    Public Function GetGoldBarPurchaseSupplierBill(ByVal sSupplierCode As String, ByVal sInvoiceNo As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "SELECT * FROM tblGoldBarPurchase WHERE SupplierCode =" & fncFormatSQLText(sSupplierCode)
            sQry = sQry & " And InvoiceNo = " & fncFormatSQLText(sInvoiceNo)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Purchase", "Purchase")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetGoldBarPurchaseSupplierBill = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetGoldBarPurchaseWithPurchaseNo(ByVal sPurchaseNo As String) As DataSet
        Dim sQry As String
        Dim Ds As New DataSet
        Try
            sQry = " Select * from tblGoldBarPurchase where PurchaseNo=" & fncFormatSQLText(sPurchaseNo)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "GoldBarPurchase", "GoldBarPurchase")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        End Try
        GetGoldBarPurchaseWithPurchaseNo = Ds
        Ds = Nothing
        sQry = Nothing
    End Function
    Public Function DeleteGoldBarPurchase(ByVal sPurchaseNo As String) As Boolean
        Dim sQry As String
        Dim DelStatus As Boolean
        Try
            sQry = " Delete from tblGoldBarPurchase where PurchaseNo = " & fncFormatSQLText(sPurchaseNo)
            DelStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            DelStatus = False
        Finally
            DeleteGoldBarPurchase = DelStatus
            sQry = Nothing
        End Try
    End Function

    Public Function GetSupplier(ByVal sSupplierCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "SELECT * FROM tblSupplier WHERE SupplierCode =" & fncFormatSQLText(sSupplierCode)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Supplier", "Supplier")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetSupplier = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetGoldBarDetails() As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "SELECT * from tblGoldBarPurchase where BalanceWeight > 0 order By InvoiceDate"
            Ds = dm.getDataSetWithSqlAdapter(sQry, "GoldBarPurchase", "GoldBarPurchase")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetGoldBarDetails = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetGoldBarInvoice(ByVal sInvoiceNo As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "SELECT * from tblGoldBarPurchase Where InvoiceNo = " & fncFormatSQLText(sInvoiceNo)
            sQry = sQry & "and BalanceWeight > 0"
            Ds = dm.getDataSetWithSqlAdapter(sQry, "GoldBarPurchase", "GoldBarPurchase")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetGoldBarInvoice = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetPurchaseBill(ByVal sSupplierCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "SELECT * From tblPurchaseBills WHERE SupplierCode =" & fncFormatSQLText(sSupplierCode)
            ' instead of purchaseweight we have to use Balanceweight
            sQry = sQry & " And PurchaseWeight > 0 order By InvoiceDate"
            Ds = dm.getDataSetWithSqlAdapter(sQry, "PurchaseBill", "PurchaseBill")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetPurchaseBill = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GoldBarIssueProceed(ByVal sIssueNo As String, ByVal sPBillNo As String, ByVal sPDate As String, ByVal sSupplierCode As String, _
                                    ByVal dPurchaseWeight As Double, ByVal dPurchaseCost As Double, _
                                    ByVal dTempPurchaseWeight As Double, ByVal dIssuedWeight As Double, _
                                    ByVal sInvoiceNo As String, ByVal sInvoiceDate As String, ByVal sClassCode As String, _
                                    ByVal dPaidCost As Double, ByVal sTranType As String, _
                                    ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            GoldBarIssueProceed = False
            sQry = sQry & "EXEC sp_GoldBarIssued  "
            sQry = sQry & fncFormatSQLText(sIssueNo) & ","
            sQry = sQry & fncFormatSQLText(sPBillNo) & ","
            sQry = sQry & fncFormatSQLDate(sPDate) & ","
            sQry = sQry & fncFormatSQLText(sSupplierCode) & ","
            sQry = sQry & Val(dPurchaseWeight) & ","
            sQry = sQry & Val(dPurchaseCost) & ","
            sQry = sQry & Val(dTempPurchaseWeight) & ","
            sQry = sQry & Val(dIssuedWeight) & ","
            sQry = sQry & fncFormatSQLText(sInvoiceNo) & ","
            sQry = sQry & fncFormatSQLDate(sInvoiceDate) & ","
            sQry = sQry & fncFormatSQLText(sClassCode) & ","
            sQry = sQry & Val(dPaidCost) & ","
            sQry = sQry & fncFormatSQLText(sTranType) & ","
            sQry = sQry & fncFormatSQLText(sUserId)
            SaveStatus = dm.ProjExecNonQry(sQry)
            GoldBarIssueProceed = SaveStatus
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            GoldBarIssueProceed = False
        End Try
    End Function
    Public Function GoldBarIssueProceedPurchase(ByVal sIssueNo As String, ByVal sInventoryCode As String, ByVal sDescription As String, ByVal dWeight As Double, _
                                ByVal dQty As Double, ByVal dCostPerGram As Double, ByVal dTotalCost As Double, ByVal sBarNo As String, _
                                ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            GoldBarIssueProceedPurchase = False
            '''sQry = sQry & "EXEC sp_GoldBarIssued  "
            '''sQry = sQry & fncFormatSQLText(sPBillNo) & ","
            '''sQry = sQry & fncFormatSQLDate(sPDate) & ","
            '''sQry = sQry & Val(dPurchaseWeight) & ","
            '''sQry = sQry & Val(dPurchaseCost) & ","
            '''sQry = sQry & Val(dTempPurchaseWeight) & ","
            '''sQry = sQry & Val(dIssuedWeight) & ","
            '''sQry = sQry & fncFormatSQLText(sInvoiceNo) & ","
            '''sQry = sQry & fncFormatSQLDate(sInvoiceDate) & ","
            '''sQry = sQry & fncFormatSQLText(sClassCode) & ","
            '''sQry = sQry & Val(dPaidCost) & ","
            '''sQry = sQry & fncFormatSQLText(sTranType) & ","
            '''sQry = sQry & fncFormatSQLText(sUserId)
            '''SaveStatus = dm.ProjExecNonQry(sQry)
            sQry = sQry & "EXEC sp_GoldBarIssuedPurchase  "
            sQry = sQry & fncFormatSQLText(sIssueNo) & ","
            sQry = sQry & fncFormatSQLText(sInventoryCode) & ","
            sQry = sQry & fncFormatSQLText(sDescription) & ","
            sQry = sQry & Val(dWeight) & ","
            sQry = sQry & Val(dQty) & ","
            sQry = sQry & Val(dCostPerGram) & ","
            sQry = sQry & Val(dTotalCost) & ","
            sQry = sQry & fncFormatSQLText(sBarNo) & ","
            sQry = sQry & fncFormatSQLText(sUserId)
            SaveStatus = dm.ProjExecNonQry(sQry)
            GoldBarIssueProceedPurchase = SaveStatus
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            GoldBarIssueProceedPurchase = False
        End Try
    End Function
    Public Function GoldBarIssueProceedInvoice(ByVal sIssueNo As String, ByVal sPBillNo As String, ByVal sPDate As String, ByVal sSupplierCode As String, _
                                ByVal dPurchaseWeight As Double, ByVal dIssueWeight As Double, _
                                ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            GoldBarIssueProceedInvoice = False
            '''sQry = sQry & "EXEC sp_GoldBarIssued  "
            '''sQry = sQry & fncFormatSQLText(sPBillNo) & ","
            '''sQry = sQry & fncFormatSQLDate(sPDate) & ","
            '''sQry = sQry & Val(dPurchaseWeight) & ","
            '''sQry = sQry & Val(dPurchaseCost) & ","
            '''sQry = sQry & Val(dTempPurchaseWeight) & ","
            '''sQry = sQry & Val(dIssuedWeight) & ","
            '''sQry = sQry & fncFormatSQLText(sInvoiceNo) & ","
            '''sQry = sQry & fncFormatSQLDate(sInvoiceDate) & ","
            '''sQry = sQry & fncFormatSQLText(sClassCode) & ","
            '''sQry = sQry & Val(dPaidCost) & ","
            '''sQry = sQry & fncFormatSQLText(sTranType) & ","
            '''sQry = sQry & fncFormatSQLText(sUserId)
            '''SaveStatus = dm.ProjExecNonQry(sQry)
            sQry = sQry & "EXEC sp_GoldBarIssuedInvoice  "
            sQry = sQry & fncFormatSQLText(sIssueNo) & ","
            sQry = sQry & fncFormatSQLText(sPBillNo) & ","
            sQry = sQry & fncFormatSQLDate(sPDate) & ","
            sQry = sQry & fncFormatSQLText(sSupplierCode) & ","
            sQry = sQry & Val(dPurchaseWeight) & ","
            sQry = sQry & Val(dIssueWeight) & ","
            sQry = sQry & fncFormatSQLText(sUserId)
            SaveStatus = dm.ProjExecNonQry(sQry)
            GoldBarIssueProceedInvoice = SaveStatus
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            GoldBarIssueProceedInvoice = False
        End Try
    End Function
End Class
