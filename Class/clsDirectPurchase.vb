
Public Class clsDirectPurchase
    Dim dm As New DataManager

    Public Function GetCatalogueSummary(ByVal sCatalogueCode As String, ByVal sJobsheetNo As String) As DataSet
        Dim Ds As DataSet
        Dim sQry As String
        Try
            GetCatalogueSummary = Nothing
            sQry = "Select Weight = sum(Weight), PCS = sum(PCS) from tblPurchase where "
            sQry = sQry & " CatalogueCode = " & fncFormatSQLText(sCatalogueCode)
            sQry = sQry & " and JobsheetNo = " & fncFormatSQLText(sJobsheetNo)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "DirectPurchase", "DirectPurchase")

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetCatalogueSummary = Ds
            Ds = Nothing
        End Try
    End Function
    
    Public Function JobSheetSave(ByVal sJobsheetNo As String, ByVal sPBillNo As String, _
                                    ByVal sPBillDate As String, ByVal sPayableType As String, _
                                    ByVal sSupplierCode As String, ByVal sCurrencyCode As String, _
                                    ByVal sCurrencyRate As Double, ByVal sCatalogueCode As String, _
                                    ByVal sCategoryCode As String, ByVal sDescription As String, _
                                    ByVal sRemarks As String, ByVal sTranType As String, _
                                    ByVal sTaelGST As String, ByVal nTaelPercentage As Double, ByVal nTaelrate As Double, ByVal sFixType As String) As Boolean
        Dim sQry As String
        Dim Status As Boolean
        Try
            JobSheetSave = False

            sQry = "Exec sp_DPJobsheetSave "
            sQry = sQry & fncFormatSQLText(sJobsheetNo)
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
            sQry = sQry & ", " & Val(nTaelPercentage)
            sQry = sQry & ", " & Val(nTaelrate)
            sQry = sQry & ", " & fncFormatSQLText(sFixType)
            Status = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            JobSheetSave = False
        Finally
            JobSheetSave = True
        End Try
    End Function
    Public Function PurchaseDelete(ByVal sJobsheetNo As String, _
                                ByVal sinventorycode As String, ByVal sInvoiceNo As String, _
                                ByVal sSupplierCode As String) As Boolean
        Dim sQry As String
        Dim Status As Boolean
        Try
            PurchaseDelete = False
            sQry = ""
            sQry = "Exec sp_DPInventorydelete "
            sQry = sQry & fncFormatSQLText(sJobsheetNo)
            sQry = sQry & ", " & fncFormatSQLText(sinventorycode)
            sQry = sQry & ", " & fncFormatSQLText(sInvoiceNo)
            sQry = sQry & ", " & fncFormatSQLText(sSupplierCode)
            sQry = sQry & ", " & fncFormatSQLText(sUserID)
            Status = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            PurchaseDelete = False
        Finally
            PurchaseDelete = True
        End Try
    End Function
Public Function ChangeOrderStatusOnPurchaseDelete(ByVal sInventoryCode As String, ByVal sOrderNo As String) As Boolean
        Dim sQry As String
        Dim Status As Boolean
        Try
            ChangeOrderStatusOnPurchaseDelete = False
            sQry = ""
            sQry = "Exec sp_UpdateStatusOnDelete "
            sQry = sQry & fncFormatSQLText(sOrderNo)
            sQry = sQry & ", " & fncFormatSQLText(sInventoryCode)
            Status = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            ChangeOrderStatusOnPurchaseDelete = False
        Finally
            ChangeOrderStatusOnPurchaseDelete = True
        End Try
    End Function
    Public Function RFIDItemStatusUpdate(ByVal sInventorycode As String) As Boolean
        Dim bStatus As Boolean
        Dim sQry As String
        Try
            sQry = "update Item set ToDisplayInStockTake = '0',status='DELETED' Where ItemCode=" & fncFormatSQLText(sInventorycode)
            bStatus = ProjExecNonQryRFID(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            RFIDItemStatusUpdate = bStatus
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
