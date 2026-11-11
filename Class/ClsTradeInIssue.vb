Public Class ClsTradeInIssue
    Dim dm As New DataManager

    Public Function GetNextTradeIssueNo() As String
        Dim Ds As New DataSet
        Dim sQry As String
        Dim sNextTradeInIssueNo As String
        sNextTradeInIssueNo = ""
        Try
            sQry = "Select NextTradeIssueNo from tblCompany"
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Trade", "Trade")
            If Ds.Tables(0).Rows.Count > 0 Then
                If Val(Ds.Tables(0).Rows(0)("NextTradeIssueNo")) <> 0 Then
                    sNextTradeInIssueNo = Mid(Trim(gthisLocation), 1, 2) & "65" & Format(Val(Ds.Tables(0).Rows(0)("NextTradeIssueNo")), "000000")
                Else
                    sNextTradeInIssueNo = Mid(Trim(gthisLocation), 1, 2) & "65000000"
                End If
            Else
                    sNextTradeInIssueNo = Mid(Trim(gthisLocation), 1, 2) & "65000000"
                End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetNextTradeIssueNo = sNextTradeInIssueNo
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetNextTradeInventory() As String
        Dim Ds As New DataSet
        Dim sQry As String
        Dim sNextTradeInventory As String
        sNextTradeInventory = ""
        Try
            sQry = "Select NextTradeInventory from tblCompany"
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Trade", "Trade")
            If Ds.Tables(0).Rows.Count > 0 Then
                If Val(Ds.Tables(0).Rows(0)("NextTradeInventory")) <> 0 Then
                    sNextTradeInventory = Mid(Trim(gthisLocation), 1, 2) & "TI" & Format(Val(Ds.Tables(0).Rows(0)("NextTradeInventory")), "0000000")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetNextTradeInventory = sNextTradeInventory
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetTradeInHeader(ByVal sInvoiceNo As String) As DataSet
        Dim ds As New DataSet
        Dim sSql As String
        Try
            sSql = "Select * from tblTradeInHeader  Where BalanceWeight > 0 and InvoiceNo= " & fncFormatSQLText(sInvoiceNo)
            ds = dm.getDataSetWithSqlAdapter(sSql, "TradeInHeader", "TradeInHeader")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            GetTradeInHeader = ds
            ds = Nothing
        End Try
    End Function
    Public Function GetTradeInDetail(ByVal sInvoiceNo As String, ByVal sInventoryCode As String) As DataSet
        Dim ds As New DataSet
        Dim sSql As String
        Try
            'sSql = "Select * from tblTradeInDetail  Where BalanceWeight > 0 and InvoiceNo= " & fncFormatSQLText(sInvoiceNo)
            'sSql = sSql & " and InventoryCode = " & fncFormatSQLText(sInventoryCode)
            sSql = "Select * from tblTradeInDetail  Where  InvoiceNo= " & fncFormatSQLText(sInvoiceNo)
            ds = dm.getDataSetWithSqlAdapter(sSql, "TradeInHeader", "TradeInHeader")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            GetTradeInDetail = ds
            ds = Nothing
        End Try
    End Function
    Public Function GetTradeIssueHistory(ByVal sTradeIssueNo As String) As DataSet
        Dim ds As New DataSet
        Dim sSql As String
        Try
            sSql = "Select * from tblTradeIssueHistory  Where TradeIssueNo = " & fncFormatSQLText(sTradeIssueNo)
            ds = dm.getDataSetWithSqlAdapter(sSql, "TIHH", "TIHH")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            GetTradeIssueHistory = ds
            ds = Nothing
        End Try
    End Function

    Public Function OpenTradeInConnection()
        gsConTradeIn.ConnectionString = gsCon
        gsConSales.Open()
    End Function

    Public Function BeginTradeInTransaction()
        gsTrans = gsConTradeIn.BeginTransaction
    End Function

    Public Function CommitTradeInTransaction()
        gsTrans.Commit()
    End Function

    Public Function RollBackTradeInConnection()
        gsTrans.Rollback()
    End Function

    Public Function CloseTradeInConnection()
        If gsConTradeIn.State = ConnectionState.Open Then gsConTradeIn.Close()
    End Function

    Public Function TradeHistorySave(ByVal sTradeIssueNo As String, ByVal sinventorycode As String, ByVal sInvoiceNo As String, ByVal sLocationCode As String, _
                            ByVal sCustomerCode As String, ByVal sCustomerName As String, _
                            ByVal sAddress1 As String, ByVal sAddress2 As String, _
                            ByVal sAddress3 As String, ByVal nCostPerGram As Double, _
                            ByVal nTotalWeight As Double, ByVal nIssuedWeight As Double, ByVal nBalanceWeight As Double, _
                            ByVal nStoneWeight As Double, ByVal nMC As Double, ByVal nPCS As Double, ByVal nGoldLoss As Double, ByVal nTotalCost As Double, _
                            ByVal dAmount As Double, _
                            ByVal sRemarks As String, ByVal sSoldFlag As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_TradeIssueHistory "
            sQry = sQry & fncFormatSQLText(sTradeIssueNo) & ", "
            sQry = sQry & fncFormatSQLText(sinventorycode) & ", "
            sQry = sQry & fncFormatSQLText(sInvoiceNo) & ", "
            sQry = sQry & fncFormatSQLText(sLocationCode) & ", "
            sQry = sQry & fncFormatSQLText(sCustomerCode) & ", "
            sQry = sQry & fncFormatSQLText(sCustomerName) & ", "
            sQry = sQry & fncFormatSQLText(sAddress1) & ", "
            sQry = sQry & fncFormatSQLText(sAddress2) & ", "
            sQry = sQry & fncFormatSQLText(sAddress3) & ", "
            sQry = sQry & Val(nCostPerGram) & ", "
            sQry = sQry & Val(nTotalWeight) & ", "
            sQry = sQry & Val(nIssuedWeight) & ", "
            sQry = sQry & Val(nBalanceWeight) & ", "
            sQry = sQry & Val(nStoneWeight) & ", "
            sQry = sQry & Val(nMC) & ", "
            sQry = sQry & Val(nPCS) & ", "
            sQry = sQry & Val(nGoldLoss) & ", "
            sQry = sQry & Val(nTotalCost) & ", "
            'sQry = sQry & fncFormatSQLText(sPayMode) & ", "
            sQry = sQry & Val(dAmount) & ", "
            sQry = sQry & fncFormatSQLText(sRemarks) & ", "
            sQry = sQry & fncFormatSQLText(sSoldFlag) & ", "
            sQry = sQry & fncFormatSQLText(sUserID)
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            TradeHistorySave = SaveStatus
            sQry = Nothing
        End Try
    End Function
    Public Function UpdateTradeInHeaderMD(ByRef oConnection As ADODB.Connection, ByRef adoRS As ADODB.Recordset, _
                                     ByVal SIssueNo As String, ByVal dTempPurWeight As Double, ByVal sInvoiceNo As String, ByVal sInventoryCode As String, ByVal sSupplier As String, _
                                     ByVal dActualIssuedWeight As Double, ByVal sRemarks As String, ByRef sError As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        On Error GoTo ErrHandler
        Dim sErrDesc As String
        Dim sVal As String
        Dim lErrNo As Long
        Dim lCustID As Long
        Dim Ors As DataSet
        UpdateTradeInHeaderMD = False

        sQry = "EXEC sp_UpdateTradeInHeaderMD"
        sQry = sQry & fncFormatSQLText(SIssueNo) & ", "
        sQry = sQry & fncFormatSQLText(sInvoiceNo) & ", "
        sQry = sQry & fncFormatSQLText(sInventoryCode) & ", "
        sQry = sQry & Val(dTempPurWeight) & ", "
        sQry = sQry & fncFormatSQLText(sSupplier) & ", "
        sQry = sQry & fncFormatSQLText(sUserID) & ", "
        sQry = sQry & Val(dActualIssuedWeight) & ", "
        sQry = sQry & fncFormatSQLText(sRemarks)
        Ors = dm.getDataSetWithSqlAdapter(sQry, "tblTradeInHeader", "tblTradeInHeader")

        UpdateTradeInHeaderMD = True
        sError = ""
        Exit Function
ErrHandler:
        sError = Err.Description
    End Function
    Public Function UpdateTradeInHeader(ByRef oConnection As ADODB.Connection, ByRef adoRS As ADODB.Recordset, _
                                         ByVal SIssueNo As String, ByVal dTempPurWeight As Double, ByVal sInvoiceNo As String, ByVal sInventoryCode As String, ByVal sSupplier As String, _
                                         ByVal dActualIssuedWeight As Double, ByVal sRemarks As String, ByRef sError As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        On Error GoTo ErrHandler
        Dim sErrDesc As String
        Dim sVal As String
        Dim lErrNo As Long
        Dim lCustID As Long
        Dim Ors As DataSet
        UpdateTradeInHeader = False

        sQry = "EXEC sp_UpdateTradeInHeader"
        sQry = sQry & fncFormatSQLText(SIssueNo) & ", "
        sQry = sQry & fncFormatSQLText(sInvoiceNo) & ", "
        sQry = sQry & fncFormatSQLText(sInventoryCode) & ", "
        sQry = sQry & Val(dTempPurWeight) & ", "
        sQry = sQry & fncFormatSQLText(sSupplier) & ", "
        sQry = sQry & fncFormatSQLText(sUserID) & ", "
        sQry = sQry & Val(dActualIssuedWeight) & ", "
        sQry = sQry & fncFormatSQLText(sRemarks)
        Ors = dm.getDataSetWithSqlAdapter(sQry, "tblTradeInHeader", "tblTradeInHeader")

        UpdateTradeInHeader = True
        sError = ""
        Exit Function
ErrHandler:
        sError = Err.Description
    End Function
    Public Function UpdateTradeInDetails(ByRef oConnection As ADODB.Connection, ByRef adoRS As ADODB.Recordset, _
                                         ByVal SIssueNo As String, ByVal dTempPurWeight As Double, ByVal sInvoiceNo As String, ByVal sInventoryCode As String, ByVal sSupplier As String, _
                                         ByRef sError As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        On Error GoTo ErrHandler
        Dim sErrDesc As String
        Dim sVal As String
        Dim lErrNo As Long
        Dim lCustID As Long
        Dim Ors As DataSet
        UpdateTradeInDetails = False

        sQry = "EXEC [sp_UpdateTradeInDetails]"
        sQry = sQry & fncFormatSQLText(SIssueNo) & ", "
        sQry = sQry & fncFormatSQLText(sInvoiceNo) & ", "
        sQry = sQry & fncFormatSQLText(sInventoryCode) & ", "
        sQry = sQry & Val(dTempPurWeight) & ", "
        sQry = sQry & fncFormatSQLText(sSupplier) & ", "
        sQry = sQry & fncFormatSQLText(sUserID)

        Ors = dm.getDataSetWithSqlAdapter(sQry, "tblTradeInHeader", "tblTradeInHeader")

        UpdateTradeInDetails = True
        sError = ""
        Exit Function
ErrHandler:
        sError = Err.Description
    End Function
    Public Function UpdateTradeInInventoryDetail(ByRef oConnection As ADODB.Connection, ByRef adoRS As ADODB.Recordset, _
                                         ByVal sTradeInInventoryCode As String, ByVal sInventoryCode As String, _
                                         ByVal dActualMergedWeight As Double, ByVal dActualMergedStWeight As Double, ByRef sError As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        On Error GoTo ErrHandler
        Dim sErrDesc As String
        Dim sVal As String
        Dim lErrNo As Long
        Dim lCustID As Long
        Dim Ors As DataSet
        UpdateTradeInInventoryDetail = False

        sQry = "EXEC sp_TradeInMergeInventoryDetailSave"
        sQry = sQry & fncFormatSQLText(sTradeInInventoryCode) & ", "
        sQry = sQry & fncFormatSQLText(sInventoryCode) & ", "
        sQry = sQry & Val(dActualMergedWeight) & ", "
        sQry = sQry & Val(dActualMergedStWeight) & ", "
        sQry = sQry & fncFormatSQLText(sUserID)
        Ors = dm.getDataSetWithSqlAdapter(sQry, "tblTradeInHeader", "tblTradeInHeader")

        UpdateTradeInInventoryDetail = True
        sError = ""
        Exit Function
ErrHandler:
        sError = Err.Description
    End Function
    Public Function UpdateTradeInMergeInventoryDetail(ByRef oConnection As ADODB.Connection, ByRef adoRS As ADODB.Recordset, _
                                        ByVal sIssueNo As String, ByVal sSupplierCode As String, ByVal sRemarks As String, ByVal sTradeInInventoryCode As String, ByVal dweight As Double, _
                                        ByVal dIssuedWeight As Double, ByVal dTotalCost As Double, ByRef sError As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        On Error GoTo ErrHandler
        Dim sErrDesc As String
        Dim sVal As String
        Dim lErrNo As Long
        Dim lCustID As Long
        Dim Ors As DataSet
        UpdateTradeInMergeInventoryDetail = False

        sQry = "EXEC sp_UpdateTradeInMergeInventoryDetail"
        sQry = sQry & fncFormatSQLText(sIssueNo) & ", "
        sQry = sQry & fncFormatSQLText(sTradeInInventoryCode) & ", "
        sQry = sQry & Val(dweight) & ", "
        sQry = sQry & Val(dIssuedWeight) & ", "
        sQry = sQry & Val(dTotalCost) & ", "
        sQry = sQry & fncFormatSQLText(sSupplierCode) & ", "
        sQry = sQry & fncFormatSQLText(sRemarks) & ", "
        sQry = sQry & fncFormatSQLText(sUserID)
        Ors = dm.getDataSetWithSqlAdapter(sQry, "tblTradeInHeader", "tblTradeInHeader")

        UpdateTradeInMergeInventoryDetail = True
        sError = ""
        Exit Function
ErrHandler:
        sError = Err.Description
    End Function
    Public Function UpdateTradeInTransferDetail(ByRef oConnection As ADODB.Connection, ByRef adoRS As ADODB.Recordset, _
                                         ByVal sSaveStatus As String, ByVal sTradeInInventoryCode As String, ByVal dWeight As Double, _
                                         ByVal dTotalCOst As Double, ByRef sError As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        On Error GoTo ErrHandler
        Dim sErrDesc As String
        Dim sVal As String
        Dim lErrNo As Long
        Dim lCustID As Long
        Dim Ors As DataSet
        UpdateTradeInTransferDetail = False

        sQry = "EXEC sp_TradeInTransferDetailSave"
        sQry = sQry & fncFormatSQLText(sSaveStatus) & ", "
        sQry = sQry & fncFormatSQLText(sTradeInInventoryCode) & ", "
        sQry = sQry & Val(dWeight) & ", "
        sQry = sQry & Val(dTotalCOst) & ", "
        sQry = sQry & fncFormatSQLText(sUserID)
        Ors = dm.getDataSetWithSqlAdapter(sQry, "tblTradeInHeader", "tblTradeInHeader")

        UpdateTradeInTransferDetail = True
        sError = ""
        Exit Function
ErrHandler:
        sError = Err.Description
    End Function


    Public Function UpdateTradeIssueNo() As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "Update tblCompany  Set NextTradeIssueNo = NextTradeIssueNo + 1"
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            UpdateTradeIssueNo = SaveStatus
            sQry = Nothing
        End Try
    End Function
    Public Function UpdateTradeInInventory() As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "Update tblCompany  Set NextTradeInventory = NextTradeInventory + 1"
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            UpdateTradeInInventory = SaveStatus
            sQry = Nothing
        End Try
    End Function

    Public Function UpdateTradeInIssueHistoryHeaderMD(ByRef oConnection As ADODB.Connection, ByRef adoRS As ADODB.Recordset, _
                          ByVal SIssueNo As String, ByVal sIssueDate As String, ByVal sSupplierCode As String, _
                          ByVal nTotalNoOfBills As Integer, ByVal nTotalWeight As Double, ByVal nCostPerGram As Double, _
                          ByVal nTotalPrice As Double, ByVal nCostPercentage As Double, ByVal sUserId As String, ByRef sError As String) As Boolean
        Dim sExec As String
        Dim sErrDesc As String
        Dim lErrNo As Long
        Dim ORs As DataSet
        Try
            UpdateTradeInIssueHistoryHeaderMD = False

            sExec = ""
            sExec = "EXEC sp_TradeInIssueHistoryHeaderSaveMD "
            sExec = sExec & fncFormatSQLText(SIssueNo) & ", "
            sExec = sExec & fncFormatSQLDate(sIssueDate) & ", "
            sExec = sExec & fncFormatSQLText(sSupplierCode) & ", "
            sExec = sExec & Val(nTotalNoOfBills) & ", "
            sExec = sExec & Val(nTotalWeight) & ", "
            sExec = sExec & Val(nCostPerGram) & ", "
            sExec = sExec & Val(nCostPercentage) & ", "
            sExec = sExec & Val(nTotalPrice) & ", "
            sExec = sExec & fncFormatSQLText(sUserId)
            ORs = dm.getDataSetWithSqlAdapter(sExec, "tblTradeInIssueHistoryHeader", "tblTradeInIssueHistoryHeader")
            UpdateTradeInIssueHistoryHeaderMD = True
            sError = ""
            Exit Function
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function
    Public Function UpdateTradeInIssueHistoryHeader(ByRef oConnection As ADODB.Connection, ByRef adoRS As ADODB.Recordset, _
                            ByVal SIssueNo As String, ByVal sIssueDate As String, ByVal sSupplierCode As String, _
                            ByVal nTotalNoOfBills As Integer, ByVal nTotalWeight As Double, ByVal nCostPerGram As Double, _
                            ByVal nTotalPrice As Double, ByVal nCostPercentage As Double, ByVal sUserId As String, ByVal sPurity As String, ByRef sError As String) As Boolean
        Dim sExec As String
        Dim sErrDesc As String
        Dim lErrNo As Long
        Dim ORs As DataSet
        Try
            UpdateTradeInIssueHistoryHeader = False

            sExec = ""
            sExec = "EXEC sp_TradeInIssueHistoryHeaderSave "
            sExec = sExec & fncFormatSQLText(SIssueNo) & ", "
            sExec = sExec & fncFormatSQLDate(sIssueDate) & ", "
            sExec = sExec & fncFormatSQLText(sSupplierCode) & ", "
            sExec = sExec & Val(nTotalNoOfBills) & ", "
            sExec = sExec & Val(nTotalWeight) & ", "
            sExec = sExec & Val(nCostPerGram) & ", "
            sExec = sExec & Val(nCostPercentage) & ", "
            sExec = sExec & Val(nTotalPrice) & ", "
            sExec = sExec & fncFormatSQLText(sUserId) & ", "
            sExec = sExec & fncFormatSQLText(sPurity)
            ORs = dm.getDataSetWithSqlAdapter(sExec, "tblTradeInIssueHistoryHeader", "tblTradeInIssueHistoryHeader")
            UpdateTradeInIssueHistoryHeader = True
            sError = ""
            Exit Function
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function
    Public Function UpdateTradeInMergeInventory(ByRef oConnection As ADODB.Connection, ByRef adoRS As ADODB.Recordset, _
                           ByVal sTradeInInventory As String, ByVal nTotalWeight As Double, ByVal nTotalStWeight As Double, _
                           ByVal nPurity As Integer, ByVal nCostPerGram As Double, ByVal nTotalPrice As Double, ByVal sUserId As String, ByRef sError As String) As Boolean
        Dim sExec As String
        Dim sErrDesc As String
        Dim lErrNo As Long
        Dim ORs As DataSet
        Try
            UpdateTradeInMergeInventory = False

            sExec = ""
            sExec = "EXEC sp_TradeInMergeInventoryHeaderSave "
            sExec = sExec & fncFormatSQLText(sTradeInInventory) & ", "
            sExec = sExec & Val(nTotalWeight) & ", "
            sExec = sExec & Val(nTotalStWeight) & ", "
            sExec = sExec & Val(nPurity) & ", "
            sExec = sExec & Val(nCostPerGram) & ", "
            sExec = sExec & Val(nTotalPrice) & ", "
            sExec = sExec & fncFormatSQLText(sUserId)
            ORs = dm.getDataSetWithSqlAdapter(sExec, "tblTradeInMergeInventory", "tblTradeInMergeInventory")
            UpdateTradeInMergeInventory = True
            sError = ""
            Exit Function
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

End Class
