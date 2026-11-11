Public Class ClsInventorySearch
    Dim dm As New DataManager

#Region "Fetch Inventory"

    Public Function FetchInventoryDetails(ByVal sQry As String) As DataSet
        Dim Ds As New DataSet
        Try
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Inventory", "Inventory")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            FetchInventoryDetails = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function GetInventoryDetails(ByVal sInventoryCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "SELECT *,Pdate = CONVERT(CHAR(8),PurchaseDate,112) FROM tblInventory a,tblInventoryStock  b WHERE a.inventorycode=b.inventorycode and a.LocationCode=b.LocationCode and a.InventoryCode = " & fncFormatSQLText(sInventoryCode) & vbNewLine
            sQry = sQry & "SELECT *,Pdate = CONVERT(CHAR(8),PurchaseBillDate,112) FROM tblJobSheetHeader WHERE JobSheetNo =(SELECT JobSheetNo FROM tblInventory WHERE InventoryCode = " & fncFormatSQLText(sInventoryCode) & ")" & vbNewLine
            sQry = sQry & " SELECT  SalesManName FROM tblSalesMan WHERE SalesManCode = (SELECT salesmanCode FROM tblSalesHeader WHERE InvoiceNo = (SELECT SalesBillNo FROM tblInventory WHERE InventoryCode = " & fncFormatSQLText(sInventoryCode) & "))" & vbNewLine
            sQry = sQry & "SELECT NetAmount FROM tblSalesDetail WHERE InventoryCode = " & fncFormatSQLText(sInventoryCode) & vbNewLine
            sQry = sQry & "SELECT RefNo, ModifyDate,sMc, Rate, NetAmount, Pcs FROM tblSalesReturnDetail WHERE InventoryCode = " & fncFormatSQLText(sInventoryCode) & vbNewLine
            sQry = sQry & "SELECT ReturnNo, ModifyDate,Mc, CostPerGram, TotalValue, Pcs FROM tblPurchaseReturnDetail WHERE InventoryCode = " & fncFormatSQLText(sInventoryCode) & vbNewLine
            sQry = sQry & "SELECT DestInventoryCode,OriginalWeight,Weight1,Weight2 FROM tblSPlitHistory WHERE InventoryCode = " & fncFormatSQLText(sInventoryCode) & vbNewLine
            sQry = sQry & "SELECT InventoryCode1,Weight1,InventoryCode2,Weight2,InventoryCodeN,WeightN FROM tblMergeHistory WHERE InventoryCodeN = " & fncFormatSQLText(sInventoryCode) & vbNewLine
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Inventory", "Inventory")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetInventoryDetails = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

#End Region

End Class
