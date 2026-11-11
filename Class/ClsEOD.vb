Public Class ClsEOD
    Dim dm As New DataManager

    Public Function GetPaymodeDetail(ByVal sDate As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select InvoiceDate, PayMode ,  sum(Amount) Amount  from tblpaymodedetail where convert(char(8),invoicedate,112) = " & fncFormatSQLText(sDate) & " group by PayMode, invoiceDate "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "PM", "PM")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetPaymodeDetail = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function POSSettlementSave(ByVal sPosNo As Integer, ByVal sPOSDate As String, ByVal sPayMode As String, ByVal dAmount As Double) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try

            sQry = "EXEC sp_POSSettlement " & sPosNo & ","
            sQry = sQry & fncFormatSQLDate(sPOSDate) & ","
            sQry = sQry & fncFormatSQLText(sPayMode) & ","
            sQry = sQry & fncFormatSQLText("0000") & ","
            sQry = sQry & Val(dAmount) & ","
            sQry = sQry & Val(0.0) & ","
            sQry = sQry & Val(dAmount) & ","
            sQry = sQry & fncFormatSQLText("") & ","
            sQry = sQry & fncFormatSQLText(sUserID)
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            POSSettlementSave = SaveStatus
            sQry = Nothing
        End Try
    End Function

    Public Sub subOpenEODConnection()
        gsEODCon.ConnectionString = gsCon
        gsEODCon.Open()
    End Sub

    Public Sub subBeginEODTransaction()
        gsEODTrans = gsEODCon.BeginTransaction
    End Sub

    Public Sub subCommitEODTransaction()
        gsEODTrans.Commit()
    End Sub

    Public Sub subRollBackEODTransaction()
        gsEODTrans.Rollback()
    End Sub

    Public Sub subCloseEODConnection()
        gsEODCon.Close()
    End Sub
    Public Sub subOpenEODConnectionHQ()
        gsEODHQCon.ConnectionString = gsConAcc
        gsEODHQCon.Open()
    End Sub

    Public Sub subBeginEODTransactionHQ()
        gsEODTrans = gsEODHQCon.BeginTransaction
    End Sub

    Public Sub subCommitEODTransactionHQ()
        gsEODTrans.Commit()
    End Sub

    Public Sub subRollBackEODTransactionHQ()
        gsEODTrans.Rollback()
    End Sub

    Public Sub subCloseEODConnectionHQ()
        gsEODHQCon.Close()
    End Sub
    Public Function fncExecNonQryEOD(ByVal Query As String) As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            Dim Cmd As New SqlClient.SqlCommand(Query, gsEODCon)
            Cmd.Transaction = gsEODTrans
            Cmd.ExecuteNonQuery()
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncExecNonQryEOD = bStatus
        End Try
    End Function

    Public Function fncEODByCategory(ByVal sEODDate As String) As Boolean
        Dim sSql As String
        Dim bStatus As Boolean
        Try
            sSql = " EXEC sp_EODByCategory "
            sSql = sSql & fncFormatSQLText(Trim$(gthisLocation)) & ", "
            sSql = sSql & fncFormatSQLDate(sEODDate) & ", "
            sSql = sSql & fncFormatSQLText(Trim$(sUserID))
            bStatus = fncExecNonQryEOD(sSql)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncEODByCategory = bStatus
        End Try
    End Function

    Public Function fncEODByTray(ByVal sEODDate As String) As Boolean
        Dim sSql As String
        Dim bStatus As Boolean
        Try
            sSql = " EXEC sp_EODByTray "
            sSql = sSql & fncFormatSQLText(Trim$(gthisLocation)) & ", "
            sSql = sSql & fncFormatSQLDate(sEODDate) & ", "
            sSql = sSql & fncFormatSQLText(Trim$(sUserID))
            bStatus = fncExecNonQryEOD(sSql)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncEODByTray = bStatus
        End Try
    End Function

    Public Function fncEOD(ByVal sEODDate As String) As Boolean
        Dim sSql As String
        Dim bStatus As Boolean
        Try
            sSql = " EXEC sp_EOD "
            sSql = sSql & fncFormatSQLDate(sEODDate) & ", "
            sSql = sSql & fncFormatSQLText(Trim$(gthisLocation)) & ", "
            sSql = sSql & fncFormatSQLText(Trim$(sUserID))
            bStatus = fncExecNonQryEOD(sSql)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncEOD = bStatus
        End Try
    End Function
    Public Function fncDailyStock(ByVal sEODDate As String) As Boolean
        Dim sSql As String
        Dim bStatus As Boolean
        Try
            sSql = " EXEC sp_DailyStock "
            bStatus = fncExecNonQryEOD(sSql)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncDailyStock = bStatus
        End Try
    End Function

    Public Function fncEODForMeena(ByVal sEODDate As String) As Boolean
        Dim sSql As String
        Dim bStatus As Boolean
        Try
            sSql = " EXEC sp_EODForMeena "
            sSql = sSql & fncFormatSQLDate(sEODDate) & ", "
            sSql = sSql & fncFormatSQLText(Trim$(gthisLocation)) & ", "
            sSql = sSql & fncFormatSQLText(Trim$(sUserID))
            bStatus = fncExecNonQryEOD(sSql)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncEODForMeena = bStatus
        End Try
    End Function

    '''''''Check Inventory Stock balance and Eod Stock Balance mismatch
    ''''''Public Function fncEOD(ByVal sEODDate As String) As Boolean
    ''''''    Dim sSql As String
    ''''''    Dim bStatus As Boolean
    ''''''    Try
    ''''''        sSql = " EXEC sp_EOD "
    ''''''        sSql = sSql & fncFormatSQLDate(sEODDate) & ", "
    ''''''        sSql = sSql & fncFormatSQLText(Trim$(gthisLocation)) & ", "
    ''''''        sSql = sSql & fncFormatSQLText(Trim$(sUserID))
    ''''''        bStatus = fncExecNonQryEOD(sSql)
    ''''''    Catch ex As Exception
    ''''''        MessageBox.Show(ex.Message)
    ''''''    Finally
    ''''''        fncEOD = bStatus
    ''''''    End Try
    ''''''End Function

    Public Function GetSalesHeaderByInvoiceDate(ByVal sInvoiceDate As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "SELECT * FROM tblSalesHeader WHERE InvoiceDate =" & fncFormatSQLText(sInvoiceDate)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Invoice", "Invoice")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetSalesHeaderByInvoiceDate = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function fncUpdateInventoryStockToHQ(ByVal dsInvStk As DataSet) As Boolean
        Dim sQry As String
        Dim sQuery As String
        Dim iCnt As Integer
        Dim bStatus As Boolean
        Try


            For iCnt = 0 To dsInvStk.Tables(0).Rows.Count - 1
                sQry = "EXEC sp_UpdateInventoryTOHQ "
                sQry = sQry & fncFormatSQLText(gthisLocation) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsInvStk.Tables(0).Rows(iCnt)("InventoryCode") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(0).Rows(iCnt)("Weight") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(0).Rows(iCnt)("StoneWeight") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(0).Rows(iCnt)("PMC") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(0).Rows(iCnt)("SMC") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(0).Rows(iCnt)("TotalCost") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsInvStk.Tables(0).Rows(iCnt)("SalesBillNo") & "")) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsInvStk.Tables(0).Rows(iCnt)("SalesDate") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(0).Rows(iCnt)("SalesCostPerGram") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(0).Rows(iCnt)("SalesMC") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(0).Rows(iCnt)("SalesAmount") & "")) & ","
                sQry = sQry & fncFormatSQLText(sUserID)
                'DM.ProjExecNonQry(sQry, gsConRemote)
                bStatus = fncExecNonQryHQ(sQry)
                If bStatus = False Then Exit Function
            Next

            For iCnt = 0 To dsInvStk.Tables(1).Rows.Count - 1
                sQry = "EXEC sp_UpdateInventoryStockTOHQ "
                sQry = sQry & fncFormatSQLText(gthisLocation) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsInvStk.Tables(1).Rows(iCnt)("InventoryCode") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(1).Rows(iCnt)("Weight") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(1).Rows(iCnt)("QtyOnHand") & "")) & ","
                sQry = sQry & Val(Trim(dsInvStk.Tables(1).Rows(iCnt)("QtySold") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsInvStk.Tables(1).Rows(iCnt)("Status") & "")) & ","
                sQry = sQry & fncFormatSQLText(sUserID)
                'DM.ProjExecNonQry(sQry, gsConRemote)
                bStatus = fncExecNonQryHQ(sQry)
                If bStatus = False Then Exit Function
            Next

            '  sQuery = "EXEC sp_UpdateOnlineLog ", " & fncFormatSQLText("STK")
            sQuery = "EXEC sp_UpdateOnlineLog '', " & fncFormatSQLText("STK")
            DM.ProjExecNonQry(sQuery, gsCon)
            '  If bStatus = False Then Exit Function

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateInventoryStockToHQ = bStatus
        End Try
    End Function
    Public Function fncUpdateLastEODDateToHQ() As Boolean
        Dim sQry As String
        Dim bStatus As Boolean
        Try
            sQry = "Update tblLocation Set LastEODDateToHQ = getdate() Where LocationCode = " & fncFormatSQLText(gthisLocation)
            bStatus = DM.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateLastEODDateToHQ = bStatus
        End Try
    End Function
    Public Function fncUpdateSalesToHQ(ByVal dsSales As DataSet) As Boolean
        Dim sQry As String
        Dim sQuery As String
        Dim bStatus As Boolean
        Dim iCnt As Integer
        Dim sInvoiceNo As String

        Try
            'Sales Header
            For iCnt = 0 To DsSales.Tables(0).Rows.Count - 1
                sQry = "EXEC sp_UpdateSalesHeader" & fncFormatSQLText(Trim(DsSales.Tables(0).Rows(iCnt)("InvoiceNo"))) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsSales.Tables(0).Rows(iCnt)("InvoiceDate" & ""))) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("CompanyCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("LocationCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("TerminalCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("CustomerCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("CustomerName") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("Address1") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("Address2") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(DsSales.Tables(0).Rows(iCnt)("Address3") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(DsSales.Tables(0).Rows(iCnt)("SalesManCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(DsSales.Tables(0).Rows(iCnt)("SalesType") & "")) & ","
                sQry = sQry & Val(Trim(DsSales.Tables(0).Rows(iCnt)("TotalWeight") & "")) & ","
                sQry = sQry & Val(Trim(DsSales.Tables(0).Rows(iCnt)("TotalStoneWeight") & "")) & ","
                sQry = sQry & Val(Trim(DsSales.Tables(0).Rows(iCnt)("TotalGoldLoss") & "")) & ","
                sQry = sQry & Val(Trim(DsSales.Tables(0).Rows(iCnt)("TotalSMC") & "")) & ","
                sQry = sQry & Val(Trim(DsSales.Tables(0).Rows(iCnt)("TotalPCS") & "")) & ","
                sQry = sQry & Val(Trim(DsSales.Tables(0).Rows(iCnt)("GST") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(DsSales.Tables(0).Rows(iCnt)("GSTType") & "")) & ","
                sQry = sQry & Val(Trim(DsSales.Tables(0).Rows(iCnt)("DetailDiscount") & "")) & ","
                sQry = sQry & Val(Trim(DsSales.Tables(0).Rows(iCnt)("NetDiscount") & "")) & ","
                sQry = sQry & Val(Trim(DsSales.Tables(0).Rows(iCnt)("Discount") & "")) & ","
                sQry = sQry & Val(Trim(DsSales.Tables(0).Rows(iCnt)("NetTotal") & "")) & ","
                sQry = sQry & Val(Trim(DsSales.Tables(0).Rows(iCnt)("PaidAmt") & "")) & ","
                sQry = sQry & Val(Trim(DsSales.Tables(0).Rows(iCnt)("BalanceAmt") & "")) & ","
                sQry = sQry & Val(Trim(DsSales.Tables(0).Rows(iCnt)("TotalValue") & "")) & ","
                sQry = sQry & Val(Trim(DsSales.Tables(0).Rows(iCnt)("TotalCost") & "")) & ","
                sQry = sQry & Val(Trim(DsSales.Tables(0).Rows(iCnt)("TradeInAmt") & "")) & ","
                sQry = sQry & Val(Trim(DsSales.Tables(0).Rows(iCnt)("DepositAmt") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(DsSales.Tables(0).Rows(iCnt)("InsAgreementNo") & "")) & ","
                sQry = sQry & Val(Trim(DsSales.Tables(0).Rows(iCnt)("Commission") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(DsSales.Tables(0).Rows(iCnt)("Remarks") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(DsSales.Tables(0).Rows(iCnt)("JournalNo") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(DsSales.Tables(0).Rows(iCnt)("CreateUser") & "")) & ","
                sQry = sQry & fncFormatSQLDate(Trim(DsSales.Tables(0).Rows(iCnt)("CreateDate") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(sUserID) & "") & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("MemberCode") & ""))
                sInvoiceNo = Trim(dsSales.Tables(0).Rows(iCnt)("InvoiceNo"))
                bStatus = fncExecNonQryHQ(sQry)
  
                If bStatus = False Then Exit Function
                'DM.ProjExecNonQry(sQry, gsConRemote)
            Next
            'Sales Detail
            For iCnt = 0 To dsSales.Tables(1).Rows.Count - 1
                sQry = "EXEC sp_UpdateSalesDetail" & fncFormatSQLText(Trim(dsSales.Tables(1).Rows(iCnt)("InvoiceNo"))) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(1).Rows(iCnt)("InventoryCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(1).Rows(iCnt)("Description") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("Weight") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("StoneWeight") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("GoldLoss") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("Rate") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("SMC") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("PCS") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("GST") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("DetailDiscount") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("NetDiscount") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("NetAmount") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("ActualSales") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("TotalValue") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("TotalCost") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("FixedCost") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("Commission") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(1).Rows(iCnt)("Remarks") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("DailyRate") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("SortCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(1).Rows(iCnt)("CreateUser") & "")) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsSales.Tables(1).Rows(iCnt)("CreateDate") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(sUserID) & "")
                'DM.ProjExecNonQry(sQry, gsConRemote)
                bStatus = fncExecNonQryHQ(sQry)
                If bStatus = False Then Exit Function
            Next
            'Trade In header
            For iCnt = 0 To dsSales.Tables(2).Rows.Count - 1
                sQry = "EXEC sp_UpdateTradeInHeaderHQ "
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(2).Rows(iCnt)("InventoryCode"))) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(2).Rows(iCnt)("InvoiceNo"))) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(2).Rows(iCnt)("LocationCode" & ""))) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(2).Rows(iCnt)("Customercode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(2).Rows(iCnt)("CustomerName") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(2).Rows(iCnt)("Address1") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(2).Rows(iCnt)("Address2") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(2).Rows(iCnt)("Address3") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(2).Rows(iCnt)("CostPerGram") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(2).Rows(iCnt)("TotalWeight") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(2).Rows(iCnt)("IssuedWeight") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(2).Rows(iCnt)("BalanceWeight") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(2).Rows(iCnt)("TotalStoneWeight") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(2).Rows(iCnt)("TotalMC") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(2).Rows(iCnt)("TotalPCS") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(2).Rows(iCnt)("TotalGoldLoss") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(2).Rows(iCnt)("TotalCost") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(2).Rows(iCnt)("Amount") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(2).Rows(iCnt)("Remarks") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(2).Rows(iCnt)("SoldFlag") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(2).Rows(iCnt)("IssueFlag") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(2).Rows(iCnt)("CreateUser") & "")) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsSales.Tables(2).Rows(iCnt)("CreateDate") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(sUserID) & "") & ", "
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(2).Rows(iCnt)("MemberCode") & ""))
                bStatus = fncExecNonQryHQ(sQry)
                If bStatus = False Then Exit Function
            Next
            'Trade In Detail 
            For iCnt = 0 To dsSales.Tables(3).Rows.Count - 1
                sQry = "EXEC sp_UpdateTradeInDetailHQ " & fncFormatSQLText(Trim(dsSales.Tables(3).Rows(iCnt)("LocationCode"))) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(3).Rows(iCnt)("InventoryCode") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(3).Rows(iCnt)("ItemNo") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(3).Rows(iCnt)("Description") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(3).Rows(iCnt)("DiamondDescription") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(3).Rows(iCnt)("Weight") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(3).Rows(iCnt)("StoneWeight") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(3).Rows(iCnt)("CostPerGram") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(3).Rows(iCnt)("MC") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(3).Rows(iCnt)("PCS") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(3).Rows(iCnt)("GoldLoss") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(3).Rows(iCnt)("TotalCost") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(3).Rows(iCnt)("CategoryCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(3).Rows(iCnt)("ClassCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(3).Rows(iCnt)("CatalogueCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(3).Rows(iCnt)("Remarks") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(3).Rows(iCnt)("Class") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(3).Rows(iCnt)("CreateUser") & "")) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsSales.Tables(3).Rows(iCnt)("CreateDate") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(3).Rows(iCnt)("InvoiceNo") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(sUserID) & "")
                bStatus = fncExecNonQryHQ(sQry)
                If bStatus = False Then Exit Function
            Next
            'Installment Header
            For iCnt = 0 To dsSales.Tables(4).Rows.Count - 1


                sQry = "EXEC sp_UpdateInstallmentEntryHeader " & fncFormatSQLText(Trim(dsSales.Tables(4).Rows(iCnt)("InsAgreementNo"))) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsSales.Tables(4).Rows(iCnt)("InvoiceDate") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(4).Rows(iCnt)("LocationCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(4).Rows(iCnt)("Customercode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(4).Rows(iCnt)("Name") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(4).Rows(iCnt)("Address1") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(4).Rows(iCnt)("Address2") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(4).Rows(iCnt)("Address3") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(4).Rows(iCnt)("Salesman") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(4).Rows(iCnt)("DepositAmt") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(4).Rows(iCnt)("TotalInstallment") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(4).Rows(iCnt)("InstallmentAmt") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(4).Rows(iCnt)("Remarks") & "")) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsSales.Tables(4).Rows(iCnt)("LastDate") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(4).Rows(iCnt)("membercode") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(4).Rows(iCnt)("TOtalCost") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(4).Rows(iCnt)("AmtPaid") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(4).Rows(iCnt)("InsPaid") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(4).Rows(iCnt)("TradeInAmt") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(4).Rows(iCnt)("TradeInGST") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(4).Rows(iCnt)("CreateBy") & "")) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsSales.Tables(4).Rows(iCnt)("CreateDate") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(sUserID) & "")
                'DM.ProjExecNonQry(sQry, gsConRemote)
                bStatus = fncExecNonQryHQ(sQry)
                If bStatus = False Then Exit Function
            Next
            'Installment Detail
            For iCnt = 0 To dsSales.Tables(5).Rows.Count - 1

                sQry = "EXEC sp_UpdateInstallmentEntryDetail" & fncFormatSQLText(Trim(dsSales.Tables(5).Rows(iCnt)("LocationCode"))) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(5).Rows(iCnt)("InsAgreementNo") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(5).Rows(iCnt)("InventoryCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(5).Rows(iCnt)("Description") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(5).Rows(iCnt)("Weight") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(5).Rows(iCnt)("GoldLoss") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(5).Rows(iCnt)("Cost") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(5).Rows(iCnt)("PCS") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(5).Rows(iCnt)("SMC") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(5).Rows(iCnt)("GST") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(5).Rows(iCnt)("TotalValue") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(5).Rows(iCnt)("DetailDiscount") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(5).Rows(iCnt)("CreateUser") & "")) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsSales.Tables(5).Rows(iCnt)("CreateDate") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(sUserID) & "")
                'DM.ProjExecNonQry(sQry, gsConRemote)
                bStatus = fncExecNonQryHQ(sQry)
                If bStatus = False Then Exit Function
            Next
            'Installment Header
            For iCnt = 0 To dsSales.Tables(6).Rows.Count - 1
                sQry = "EXEC sp_UpdateInstallmentDetail " & fncFormatSQLText(Trim(dsSales.Tables(6).Rows(iCnt)("InsAgreementNo"))) & ", "
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(6).Rows(iCnt)("InstallmentNo") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(6).Rows(iCnt)("InsPaid") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(6).Rows(iCnt)("InstallmentAmt") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(6).Rows(iCnt)("AmtPaid") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(6).Rows(iCnt)("Remarks") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(6).Rows(iCnt)("CreateUser") & "")) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsSales.Tables(6).Rows(iCnt)("CreateDate") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(sUserID) & "")
                'DM.ProjExecNonQry(sQry, gsConRemote)
                bStatus = fncExecNonQryHQ(sQry)
                If bStatus = False Then Exit Function
            Next
            'Paymode detail
            For iCnt = 0 To dsSales.Tables(7).Rows.Count - 1
                sQry = "Exec sp_UpdatePaymodeDetail " & fncFormatSQLText(Trim(dsSales.Tables(7).Rows(iCnt)("InvoiceNo") & "")) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsSales.Tables(7).Rows(iCnt)("InvoiceDate") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(7).Rows(iCnt)("PayMode") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(7).Rows(iCnt)("Amount") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(7).Rows(iCnt)("Commision") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(7).Rows(iCnt)("SlipNo") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(7).Rows(iCnt)("CreateUser") & "")) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsSales.Tables(7).Rows(iCnt)("CreateDate") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(sUserID & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(7).Rows(iCnt)("LocationCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(7).Rows(iCnt)("SalesType") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(7).Rows(iCnt)("IsPaid") & ""))
                bStatus = fncExecNonQryHQ(sQry)
                If bStatus = False Then Exit Function
            Next
            If dsSales.Tables(0).Rows.Count > 0 Then
                sQuery = "EXEC sp_UpdateOnlineLog " & fncFormatSQLText(sInvoiceNo) & ", " & fncFormatSQLText("SALES")
                DM.ProjExecNonQry(sQuery, gsCon)
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateSalesToHQ = bStatus
        End Try
    End Function
    Public Function fncUpdateSaleReturnDetailsToHQ(ByVal dsSales As DataSet) As Boolean
        Dim sQry As String
        Dim sQuery As String
        Dim bStatus As Boolean
        Dim iCnt As Integer
        Dim sLastSalesReturnNo As String

        Try

            'Sales Return Header
            For iCnt = 0 To dsSales.Tables(0).Rows.Count - 1
                sQry = "EXEC sp_UpdateSalesReturnHeader" & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("RefNo"))) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("InvoiceNo" & ""))) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsSales.Tables(0).Rows(iCnt)("InvoiceDate" & ""))) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("CompanyCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("LocationCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("TerminalCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("CustomerCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("SalesManCode") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("TotalWeight") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("TotalStoneWeight") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("TotalGoldLoss") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("TotalSMC") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("TotalPCS") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("GST") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("DetailDiscount") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("NetDiscount") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("Discount") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("NetTotal") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("PaidAmt") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("BalanceAmt") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("TotalValue") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("TotalCost") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("Remarks") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("ReturnType") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("JournalNo") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("ModifyUser") & "")) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsSales.Tables(0).Rows(iCnt)("ModifyDate") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("MemberCode") & ""))
                sLastSalesReturnNo = Trim(dsSales.Tables(0).Rows(iCnt)("RefNo"))
                bStatus = fncExecNonQryHQ(sQry)

                If bStatus = False Then Exit Function
                'DM.ProjExecNonQry(sQry, gsConRemote)
            Next
            'Sales Return Detail
            For iCnt = 0 To dsSales.Tables(1).Rows.Count - 1
                sQry = "EXEC sp_UpdateSalesReturnDetail" & fncFormatSQLText(Trim(dsSales.Tables(1).Rows(iCnt)("RefNo"))) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(1).Rows(iCnt)("InvoiceNo") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(1).Rows(iCnt)("InventoryCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(1).Rows(iCnt)("Description") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("Weight") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("StoneWeight") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("GoldLoss") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("Rate") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("SMC") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("PCS") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("GST") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("DetailDiscount") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("NetDiscount") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("NetAmount") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("ActualSales") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("TotalValue") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("TotalCost") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(1).Rows(iCnt)("Remarks") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(1).Rows(iCnt)("ModifyUser") & "")) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsSales.Tables(1).Rows(iCnt)("ModifyDate") & ""))

                bStatus = fncExecNonQryHQ(sQry)
                If bStatus = False Then Exit Function
            Next



            If dsSales.Tables(0).Rows.Count > 0 Then
                sQuery = ""
                sQuery = "EXEC sp_UpdateOnlineLog " & fncFormatSQLText(sLastSalesReturnNo) & ", " & fncFormatSQLText("SALESRETURN")
                DM.ProjExecNonQry(sQuery, gsCon)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateSaleReturnDetailsToHQ = bStatus
        End Try
    End Function
    Public Function fncUpdateOrderDetailsToHQ(ByVal dsSales As DataSet) As Boolean
        Dim sQry As String
        Dim sQuery As String
        Dim bStatus As Boolean
        Dim iCnt As Integer
        Dim sLastOrderNo As String

        Try

            'Sales Return Header
            For iCnt = 0 To dsSales.Tables(0).Rows.Count - 1
                sQry = "EXEC sp_UpdateInventoryOrderHeaderHQ" & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("OrderNo"))) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsSales.Tables(0).Rows(iCnt)("OrderDate" & ""))) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("CustomerCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("Suppliercode") & "")) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsSales.Tables(0).Rows(iCnt)("DeliveryDate") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("CostPerGram") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("TradeInNo") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("TradeInType") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("RateFixed") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("TotalCostEstimated") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("AdvancePaid") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("Status") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("Remarks") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("LocationCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("AdminUserId") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("InvType") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("SupplierCost") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("CreatedUser") & "")) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsSales.Tables(0).Rows(iCnt)("CreatedDate") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(sUserID & ""))
                sLastOrderNo = fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("OrderNo")) & "")
                bStatus = fncExecNonQryHQ(sQry)
                If bStatus = False Then Exit Function
                'DM.ProjExecNonQry(sQry, gsConRemote)
            Next
            'Sales Return Detail
            For iCnt = 0 To dsSales.Tables(1).Rows.Count - 1
                sQry = "EXEC sp_UpdateInventoryOrderDetailHQ" & fncFormatSQLText(Trim(dsSales.Tables(1).Rows(iCnt)("OrderNo"))) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(1).Rows(iCnt)("Description") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(1).Rows(iCnt)("OrderInventoryCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(1).Rows(iCnt)("StoneDescription") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(1).Rows(iCnt)("ClassCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(1).Rows(iCnt)("CategoryCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(1).Rows(iCnt)("CatalogueCode") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("WeightEstimated") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("StoneWeight") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("GoldLoss") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("MC") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("CostPerGram") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("EstimatedCost") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(1).Rows(iCnt)("Status") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(1).Rows(iCnt)("CreatedUser") & "")) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsSales.Tables(1).Rows(iCnt)("CreatedDate") & "")) & ","
                '     sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("StoneCost") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(sUserID & ""))

                bStatus = fncExecNonQryHQ(sQry)
                If bStatus = False Then Exit Function
            Next



            If dsSales.Tables(0).Rows.Count > 0 Then
                sQuery = ""
                sQuery = "EXEC sp_UpdateOnlineLog " & sLastOrderNo & ", " & fncFormatSQLText("ORDER")
                dm.ProjExecNonQry(sQuery, gsCon)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateOrderDetailsToHQ = bStatus
        End Try
    End Function
    Public Function fncUpdateOrderInventoryDetailLocal(ByVal dsSales As DataSet) As Boolean
        Dim sQry As String
        Dim sQuery As String
        Dim bStatus As Boolean
        Dim iCnt As Integer
        Dim sLastOrderNo As String

        Try

            For iCnt = 0 To dsSales.Tables(0).Rows.Count - 1
                sQry = "EXEC sp_UpdateOrderInventoryDetailLocal" & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("OrderNo"))) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("Description") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("OrderInventoryCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("InventoryCode") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("WeightEstimated") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("EstimatedCost") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("TotalCostEstimated") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("TotalGSTEstimated") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(sUserID & ""))

                'bStatus = fncExecNonQryEOD(sQry)
                dm.ProjExecNonQry(sQry, gsCon)
                bStatus = True
                If bStatus = False Then Exit Function
            Next



        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateOrderInventoryDetailLocal = bStatus
        End Try
    End Function
    Public Function fncUpdateRepairDetailsToHQ(ByVal dsSales As DataSet) As Boolean
        Dim sQry As String
        Dim sQuery As String
        Dim bStatus As Boolean
        Dim iCnt As Integer
        Dim sLastRepairNo As String

        Try

            
            'Sales Return Header
            For iCnt = 0 To dsSales.Tables(0).Rows.Count - 1
                sQry = "EXEC sp_UpdateInventoryRepairHeaderHQ" & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("RepairNo"))) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsSales.Tables(0).Rows(iCnt)("RepairDate" & ""))) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("CustomerCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("Suppliercode") & "")) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsSales.Tables(0).Rows(iCnt)("DeliveryDate") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("CostPerGram") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("TradeInNo") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("TradeInType") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("RateFixed") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("TotalCostEstimated") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("AdvancePaid") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("Status") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("Remarks") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("LocationCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("AdminUserId") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("InvType") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("SupplierCost") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("CreatedUser") & "")) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsSales.Tables(0).Rows(iCnt)("CreatedDate") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(sUserID & ""))
                sLastRepairNo = fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("RepairNo")) & "")
                bStatus = fncExecNonQryHQ(sQry)
                If bStatus = False Then Exit Function
                'DM.ProjExecNonQry(sQry, gsConRemote)
            Next
            'Sales Return Detail
            For iCnt = 0 To dsSales.Tables(1).Rows.Count - 1
                sQry = "EXEC sp_UpdateInventoryRepairDetailHQ" & fncFormatSQLText(Trim(dsSales.Tables(1).Rows(iCnt)("RepairNo"))) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(1).Rows(iCnt)("Description") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("WeightEstimated") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("EstimatedCost") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(1).Rows(iCnt)("Status") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(1).Rows(iCnt)("CreatedUser") & "")) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsSales.Tables(1).Rows(iCnt)("CreatedDate") & "")) & ","
                 sQry = sQry & fncFormatSQLText(Trim(sUserID & ""))


                bStatus = fncExecNonQryHQ(sQry)
                If bStatus = False Then Exit Function
            Next



            If dsSales.Tables(0).Rows.Count > 0 Then
                sQuery = ""
                sQuery = "EXEC sp_UpdateOnlineLog " & sLastRepairNo & ", " & fncFormatSQLText("REPAIR")
                dm.ProjExecNonQry(sQuery, gsCon)
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateRepairDetailsToHQ = bStatus
        End Try
    End Function
    Public Function fncUpdateTradeInStockToHQ(ByVal dsSales As DataSet) As Boolean
        Dim sQry As String
        Dim sQuery As String
        Dim bStatus As Boolean
        Dim iCnt As Integer
        Dim sLastRepairNo As String

        Try


            'Sales Return Header
            For iCnt = 0 To dsSales.Tables(0).Rows.Count - 1
                sQry = "EXEC sp_UpdateTradeInStockDetailHQ" & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("LocationCode"))) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("InventoryCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("ItemNo") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("Description") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("DiamondDescription") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("Weight") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("StoneWeight") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("CostPerGram") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("MC") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("PCS") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("GoldLoss") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("TotalCost") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("CategoryCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("ClassCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("CatalogueCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("Remarks") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("Class") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("CreatedUser") & "")) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsSales.Tables(0).Rows(iCnt)("CreatedDate") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(sUserID & ""))
                bStatus = fncExecNonQryHQ(sQry)
                If bStatus = False Then Exit Function
                'DM.ProjExecNonQry(sQry, gsConRemote)
            Next

          
        'Sales Return Detail
            For iCnt = 0 To dsSales.Tables(0).Rows.Count - 1
                sQry = "EXEC sp_UpdateTradeInStockHeaderHQ" & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("InventoryCode"))) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("InvoiceNo") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("LocationCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("CustomerCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("CustomerName") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("Address1") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("Address2") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("Address3") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("CostPerGram") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("TotalWeight") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("IssuedWeight") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("BalanceWeight") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("TotalStoneWeight") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("TotalMC") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("TotalPCS") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("TotalGoldLoss") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("TotalCost") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("Amount") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("Remarks") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("SoldFlag") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("IssueFlag") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("CreatedUser") & "")) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsSales.Tables(0).Rows(iCnt)("CreatedDate") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(sUserID & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("MemberCode") & ""))
                bStatus = fncExecNonQryHQ(sQry)
                If bStatus = False Then Exit Function

                sQry = "EXEC sp_UpdateTradeInStockHeader" & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("InvoiceNo"))) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("IssuedWeight") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(sUserID & ""))
                bStatus = fncExecNonQryEOD(sQry)
                If bStatus = False Then Exit Function
                'DM.ProjExecNonQry(sQry, gsConRemote)
            Next



        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateTradeInStockToHQ = bStatus
        End Try
    End Function

    Public Function fncUpdateOutletMovementToHQ(ByVal dsSales As DataSet) As Boolean
        Dim sQry As String
        Dim sQuery As String
        Dim bStatus As Boolean
        Dim iCnt As Integer
        Dim sLastOrderNo As String

        Try

            'Sales Return Header
            For iCnt = 0 To dsSales.Tables(0).Rows.Count - 1
                sQry = "EXEC sp_UpdateOutletMovementtoHQ " & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("LocationCode"))) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("InventoryCode" & ""))) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("Description") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("Status") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("RefNumber") & "")) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsSales.Tables(0).Rows(iCnt)("RefDate") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("Qty") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("Weight") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("Size") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("PMC") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("SMC") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("CostPerGram") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("TotalCost") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("SupplierCode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("CreateUser") & "")) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsSales.Tables(0).Rows(iCnt)("CreateDate") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("ModifyUser") & "")) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsSales.Tables(0).Rows(iCnt)("ModifyDate") & ""))
                bStatus = fncExecNonQryHQ(sQry)
                If bStatus = False Then Exit Function
                'DM.ProjExecNonQry(sQry, gsConRemote)
            Next




        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateOutletMovementToHQ = bStatus
        End Try
    End Function
    Public Function fncUpdateOutletUserLogToHQ(ByVal dsSales As DataSet) As Boolean
        Dim sQry As String
        Dim sQuery As String
        Dim bStatus As Boolean
        Dim iCnt As Integer
        Dim sLastOrderNo As String

        Try

            'Sales Return Header
            For iCnt = 0 To dsSales.Tables(0).Rows.Count - 1
                sQry = "EXEC sp_UpdateOutletUserLogtoHQ " & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("LocationCode"))) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("UserCode" & ""))) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("Type") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("CreateUser") & "")) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsSales.Tables(0).Rows(iCnt)("CreateDate") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("ModifyUser") & "")) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsSales.Tables(0).Rows(iCnt)("ModifyDate") & ""))
                bStatus = fncExecNonQryHQ(sQry)
                If bStatus = False Then Exit Function
                'DM.ProjExecNonQry(sQry, gsConRemote)
            Next




        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateOutletUserLogToHQ = bStatus
        End Try
    End Function
        Public Function fncExecNonQryHQ(ByVal Query As String) As Boolean
            Dim bStatus As Boolean
            Try
                bStatus = False
                Dim Cmd As New SqlClient.SqlCommand(Query, gsEODHQCon)
                Cmd.Transaction = gsEODTrans
                Cmd.ExecuteNonQuery()
                bStatus = True
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                fncExecNonQryHQ = bStatus
            End Try
        End Function

        Public Sub subOpenConnectionHQ()
            gsUpdateCon.ConnectionString = gsConAcc
            gsUpdateCon.Open()
        End Sub

        Public Sub subBeginTransactionHQ()
            gsUpdateTrans = gsUpdateCon.BeginTransaction
        End Sub

        Public Sub subCommitTransactionHQ()
            gsUpdateTrans.Commit()
        End Sub

        Public Sub subRollBackTransactionHQ()
            gsUpdateTrans.Rollback()
        End Sub

        Public Sub subCloseConnectionHQ()
            gsUpdateCon.Close()
    End Sub
    Public Function fncUpdateTerminalSalesToHQ(ByVal dsSales As DataSet) As Boolean
        Dim sQry As String
        Dim sQuery As String
        Dim bStatus As Boolean
        Dim iCnt As Integer
        Dim sInvoiceNo As String

        Try
            'Sales Header
            For iCnt = 0 To dsSales.Tables(0).Rows.Count - 1
                sQry = "EXEC sp_TerminalSales" & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("CashierID"))) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsSales.Tables(0).Rows(iCnt)("Date") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("TerminalCode" & ""))) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("PayMode") & "")) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(0).Rows(iCnt)("LocationCode") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("ComputerAmount") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("ManualAmount") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(0).Rows(iCnt)("SortOrExces") & ""))
                bStatus = fncExecNonQryHQ(sQry)

                If bStatus = False Then Exit Function
                'DM.ProjExecNonQry(sQry, gsConRemote)
            Next
            'Sales Detail
            For iCnt = 0 To dsSales.Tables(1).Rows.Count - 1
                sQry = "EXEC sp_Dinamination" & fncFormatSQLText(Trim(dsSales.Tables(1).Rows(iCnt)("CashierId"))) & ","
                sQry = sQry & fncFormatSQLText(Trim(dsSales.Tables(1).Rows(iCnt)("TerminalCode") & "")) & ","
                sQry = sQry & fncFormatSQLDate(Trim(dsSales.Tables(1).Rows(iCnt)("PostingDate") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("Qty") & "")) & ","
                sQry = sQry & Val(Trim(dsSales.Tables(1).Rows(iCnt)("Amount") & "")) 
                'DM.ProjExecNonQry(sQry, gsConRemote)
                bStatus = fncExecNonQryHQ(sQry)
                If bStatus = False Then Exit Function
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateTerminalSalesToHQ = bStatus
        End Try
    End Function
    End Class
