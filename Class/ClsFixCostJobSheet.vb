Public Class ClsFixCostJobSheet
    Dim dm As New DataManager

#Region "Fetch JobSheet Details"

    Public Function GetJobSheetDetail(ByVal sJobSheetNo As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " SELECT CostPerGram, SupplierCode, PurchaseBillNo, PurchaseBillDate, BillClose FROM tblJobSheetHeader WHERE JobSheetNo = " & fncFormatSQLText(sJobSheetNo)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "JobSheet", "JobSheet")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetJobSheetDetail = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetPurchaseDetail(ByVal sJobSheetNo As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " SELECT JobsheetNo,InvoiceNo,SupplierCode,TotalBeforeGST,GST,TotalSales FROM tblPurchaseDetails WHERE JobSheetNo = " & fncFormatSQLText(sJobSheetNo)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "JobSheet", "JobSheet")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetPurchaseDetail = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetJobSheetClass(ByVal sJobSheetNo As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "Select rtrim(ClassCode) ClassCode, rtrim(ClassName) ClassName From tblClass where ClassCode in " & _
            "(Select ClassCode from tblCategory where CategoryCode in " & _
            "(Select CategoryCode from tblJobSheetDetail where JobSheetNo = " & fncFormatSQLText(sJobSheetNo) & "))"
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Class", "Class")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetJobSheetClass = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function GetOldCostPerGram(ByVal sQry As String) As DataSet
        Dim Ds As New DataSet
        Try
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Cost", "Cost")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetOldCostPerGram = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function IsBillClosed(ByVal sJobSheetNo As String) As Boolean
        Dim Ds As New DataSet
        Dim sQry As String
        Dim bBillStatus As Boolean
        Try
            bBillStatus = False
            sQry = "Select COUNT(*) from tblJobSheetHeader where JobSheetNo = " & fncFormatSQLText(sJobSheetNo) & " And BILLCLOSE = 'Y'"
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Job", "Job")
            If Ds.Tables(0).Rows.Count > 0 Then
                If Val(Ds.Tables(0).Rows(0)(0)) > 0 Then
                    bBillStatus = True
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            IsBillClosed = bBillStatus
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function GetClassCode(ByVal sClassName As String) As String
        Dim Ds As New DataSet
        Dim sQry As String
        Dim sClassCode As String
        Try
            sClassCode = ""
            sQry = "Select ClassCode from tblClass where ClassName = " & fncFormatSQLText(sClassName)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "ClassCode", "ClassCode")
            If Ds.Tables("ClassCode").Rows.Count > 0 Then
                sClassCode = Ds.Tables("ClassCode").Rows(0)("ClassCode")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetClassCode = sClassCode
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function GetStockTakeID() As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "Select rtrim(Id) Id, rtrim(StockTakeList) StockTakeList From vwRFIDStockTakeReports "
            Ds = dm.getDataSetWithSqlAdapterRFID(sQry, "Class", "Class")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetStockTakeID = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
#End Region

#Region "Update Cost"

    Public Function UpdateCost(ByVal sJobSheetNO As String, ByVal sClassCode As String, ByVal dOldCostPerGram As Double, ByVal dnewCostPerGram As Double, ByVal dGoldValue As Double, ByVal dCashValue As Double) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_JobSheetFixCost " & fncFormatSQLText(sJobSheetNO) & ","
            sQry = sQry & fncFormatSQLText(sClassCode) & ","
            sQry = sQry & Val(dOldCostPerGram) & ","
            sQry = sQry & Val(dnewCostPerGram) & ","
            sQry = sQry & Val(dGoldValue) & ","
            sQry = sQry & Val(dCashValue) & ","
            sQry = sQry & fncFormatSQLText(sUserID)
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            UpdateCost = SaveStatus
            sQry = Nothing
        End Try
    End Function


    Public Function UpdatePurchaseDetails(ByVal sJobSheetNO As String, ByVal sInvoiceNo As String, ByVal sSupplierCode As String, ByVal txtTotalCostBeforeGST As Double, ByVal dGST As Double, ByVal dTotalCost As Double) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_UpdatePurchaseDetails " & fncFormatSQLText(sJobSheetNO) & ","
            sQry = sQry & fncFormatSQLText(sInvoiceNo) & ","
            sQry = sQry & fncFormatSQLText(sSupplierCode) & ","
            sQry = sQry & Val(txtTotalCostBeforeGST) & ","
            sQry = sQry & Val(dGST) & ","
            sQry = sQry & Val(dTotalCost) & ","
            sQry = sQry & fncFormatSQLText(sUserID)
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            UpdatePurchaseDetails = SaveStatus
            sQry = Nothing
        End Try
    End Function
#End Region

End Class
