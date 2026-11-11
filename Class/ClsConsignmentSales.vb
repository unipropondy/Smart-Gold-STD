Public Class ClsConsignmentSales
    Dim dm As New DataManager

    Public Function GetMarketRate(ByVal sClassCode As String, ByVal sSalesDate As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "SELECT Rate FROM tblClassRate WHERE ClassCode =" & fncFormatSQLText(sClassCode) & " and " & fncFormatSQLDate(sSalesDate) & " between FromDate and ToDate"
            Ds = dm.getDataSetWithSqlAdapter(sQry, "MR", "MR")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetMarketRate = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function GetMCDiscoutFromClass(ByVal sClassCode As String) As Double
        Dim Ds As New DataSet
        Dim sQry As String
        Dim dMCDiscount As Double
        Try
            sQry = "select isnull(MCDiscount,0.00) MCDiscount from tblclass WHERE ClassCode =" & fncFormatSQLText(sClassCode)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Cls", "Cls")
            If Ds.Tables(0).Rows.Count > 0 Then
                dMCDiscount = Val(Ds.Tables(0).Rows(0)("MCDiscount"))
            Else
                dMCDiscount = 0.0
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetMCDiscoutFromClass = dMCDiscount
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function OpenSalesConnection()
        gsConSales.ConnectionString = gsCon
        gsConSales.Open()
    End Function

    Public Function gpBeginSalesTransaction()
        gsTrans = gsConSales.BeginTransaction
    End Function

    Public Function gpCommitSalesTransaction()
        gsTrans.Commit()
    End Function

    Public Function gpRollBackSalesConnection()
        gsTrans.Rollback()
    End Function

    Public Function CloseSalesConnection()
        If gsConSales.State = ConnectionState.Open Then gsConSales.Close()
    End Function

    Public Function GetUpdateInvoice() As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " EXEC sp_GetUpdateConsignmentSalesInvoiceNo "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "CS", "CS")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetUpdateInvoice = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function SalesHeaderSave(ByVal sInvoiceNo As String, ByVal sDate As String, _
                                   ByVal sCompanyCode As String, ByVal sLocationCode As String, ByVal sTerminalCode As String, _
                                   ByVal sCustomerCode As String, ByVal sCustomerName As String, _
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
                                   ByVal dCommission As Double, ByVal sRemarks As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = ""
            sQry = sQry & " EXEC sp_ConsignmentSalesHeaderSave " & fncFormatSQLText(sInvoiceNo) & ","
            sQry = sQry & fncFormatSQLDate(sDate) & ","
            sQry = sQry & fncFormatSQLText(sCompanyCode) & ","
            sQry = sQry & fncFormatSQLText(sLocationCode) & ","
            sQry = sQry & fncFormatSQLText(sTerminalCode) & ","
            sQry = sQry & fncFormatSQLText(sCustomerCode) & ","
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
            sQry = sQry & fncFormatSQLText(sUserID)
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            SalesHeaderSave = SaveStatus
            sQry = Nothing
        End Try
    End Function

    Public Function AllowMarketPriceSave(ByVal sInvoiceNo As String, ByVal sMarketDate As String, ByVal dMarketRate As Double, _
                                     ByVal dDiscountAmount As Double, _
                                     ByVal dGstAmount As Double, ByVal dNetTotalCost As Double, _
                                     ByVal dMarketNetTotalCost As Double) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_AllowMarketPrice " & fncFormatSQLText(sInvoiceNo) & ","
            sQry = sQry & fncFormatSQLDate(sMarketDate) & ","
            sQry = sQry & Val(dMarketRate) & ","
            sQry = sQry & Val(dDiscountAmount) & ","
            sQry = sQry & Val(dGstAmount) & ","
            sQry = sQry & Val(dNetTotalCost) & ","
            sQry = sQry & Val(dMarketNetTotalCost) & ","
            sQry = sQry & fncFormatSQLText(sUserID)
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            AllowMarketPriceSave = SaveStatus
            sQry = Nothing
        End Try
    End Function

    Public Function FindTotalMarketPriceSave(ByVal sInvoiceNo As String, ByVal dMarketTotalPrice As Double) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_FindTotalMarketPrice " & fncFormatSQLText(sInvoiceNo) & ","
            sQry = sQry & Val(dMarketTotalPrice) & ","
            sQry = sQry & fncFormatSQLText(sUserID)
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            FindTotalMarketPriceSave = SaveStatus
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
            sQry = "EXEC sp_ConsignmentSalesDetailSave " & fncFormatSQLText(sInvoiceNo) & ","
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
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            SalesDetailSave = SaveStatus
            sQry = Nothing
        End Try
    End Function

    Public Function GetSalesHeaderByInvoiceNo(ByVal sInvoiceNo As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "SELECT * FROM tblConsignmentSalesHeader WHERE InvoiceNo =" & fncFormatSQLText(sInvoiceNo)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "CSH", "CSH")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetSalesHeaderByInvoiceNo = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

End Class
