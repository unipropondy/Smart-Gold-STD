Imports System
Imports System.IO
Imports System.Text
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms
Imports System.Drawing.Image
Imports System.Drawing.Drawing2D
Imports System.Globalization




Public Class ClsSales
    Dim dm As New DataManager
    Dim ClsReceiptToCustomer As New ClsReceiptToCustomer
    Public Declare Function CloseUSBcr Lib "usbcr.dll" () As Long
    Public Declare Function DrawerOpen Lib "usbcr.dll" (ByVal ID As Long) As Long
    Public Declare Function DrawerState Lib "usbcr.dll" (ByVal ID As Long) As Long
    Public Declare Function OpenUSBcr Lib "usbcr.dll" () As Long



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

    Public Function GetUpdateInvoice() As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " EXEC sp_GetUpdateInvoice "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Invoice", "Invoice")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetUpdateInvoice = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetUpdateIPMInvoice() As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " EXEC sp_GetUpdateIPMInvoice "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Invoice", "Invoice")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetUpdateIPMInvoice = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function fncUpdateNextInvoiceNo() As Boolean
        Dim bStatus As Boolean
        Dim sQry As String
        Try
            sQry = " update tblcompany set NextInvoice = NextInvoice + 1 Where CompanyId = '1'"
            bStatus = fncExecNonQrySales(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateNextInvoiceNo = bStatus
            sQry = Nothing
        End Try
    End Function
    
    Public Function fncUpdateNextIPMInvoiceNo() As Boolean
        Dim bStatus As Boolean
        Dim sQry As String
        Try
            sQry = " update tblcompany set NextIPMInvoice = NextIPMInvoice + 1 Where CompanyId = '1'"
            bStatus = fncExecNonQrySales(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateNextIPMInvoiceNo = bStatus
            sQry = Nothing
        End Try
    End Function
    'Shabeer 20120619
    Public Function GetUpdateCreditInvoice() As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " EXEC sp_GetUpdateCreditInvoice "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Invoice", "Invoice")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetUpdateCreditInvoice = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function fncUpdateNextCreditInvoiceNo() As Boolean
        Dim bStatus As Boolean
        Dim sQry As String
        Try
            sQry = " update tblcompany set NextCreditInvoice = NextCreditInvoice + 1 Where CompanyId = '1'"
            bStatus = fncExecNonQrySales(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateNextCreditInvoiceNo = bStatus
            sQry = Nothing
        End Try
    End Function
    'Shabeer 20120619

    'Public Function OpenSalesConnection()
    '    gsConSales.ConnectionString = gsCon
    '    gsConSales.Open()
    'End Function

    'Public Function gpBeginSalesTransaction()
    '    gsTrans = gsConSales.BeginTransaction
    'End Function

    'Public Function gpCommitSalesTransaction()
    '    gsTrans.Commit()
    'End Function

    'Public Function gpRollBackSalesConnection()
    '    gsTrans.Rollback()
    'End Function

    'Public Function CloseSalesConnection()
    '    gsConSales.Close()
    'End Function

    'Public Function SalesConStatus() As Boolean
    '    If gsConSales.State = ConnectionState.Open Then
    '        SalesConStatus = True
    '    Else
    '        SalesConStatus = False
    '    End If
    'End Function

    'Public Function IsTransBegined() As Boolean
    '    If gsTrans.Connection.State = ConnectionState.Open Then
    '        IsTransBegined = True
    '    Else
    '        IsTransBegined = False
    '    End If

    'End Function

    Public Function SalesHeaderSave(ByVal sInvoiceNo As String, ByVal sDate As String, _
                               ByVal sCompanyCode As String, ByVal sLocationCode As String, ByVal sTerminalCode As String, _
                                   ByVal sCustomerCode As String, ByVal sMembercode As String, ByVal sCustomerName As String, _
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
                                   ByVal dCommission As Double, ByVal sRemarks As String, ByVal dTradeInGst As Double, ByVal seTRSType As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = ""
            sQry = sQry & " EXEC sp_SalesHeader " & fncFormatSQLText(sInvoiceNo) & ","
            sQry = sQry & fncFormatSQLDate(sDate) & ","
            sQry = sQry & fncFormatSQLText(sCompanyCode) & ","
            sQry = sQry & fncFormatSQLText(sLocationCode) & ","
            sQry = sQry & fncFormatSQLText(sTerminalCode) & ","
            sQry = sQry & fncFormatSQLText(sCustomerCode) & ","
            sQry = sQry & fncFormatSQLText(sMembercode) & ","
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
            sQry = sQry & Val(dTradeInGst) & ","
            sQry = sQry & fncFormatSQLText(seTRSType) & ","
            sQry = sQry & fncFormatSQLText(sUserID)
            SaveStatus = fncExecNonQrySales(sQry)
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
            SaveStatus = fncExecNonQrySales(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            AllowMarketPriceSave = SaveStatus
            sQry = Nothing
        End Try
    End Function
    Public Function UpdateDailySales(ByVal sInvoiceDate As Date, ByVal dCash As Double, ByVal dNets As Double, _
                                 ByVal dVisa As Double, _
                                 ByVal dMaster As Double, ByVal dAmex As Double, _
                                 ByVal dDiners As Double, ByVal dChq As Double, ByVal dTransferACC As Double, ByVal dInstallment As Double, ByVal d24K As Double, _
                                 ByVal dTradeInGold As Double, ByVal dAdvancePayment As Double, ByVal dPettyCash As Double, ByVal dAdvanceUtilise As Double, ByVal dTotalGSTSales As Double, ByVal dGST As Double, _
                                 ByVal dTotalGSTNonGSTSales As Double) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_DailySales " & fncFormatSQLDate(sInvoiceDate) & ","
            sQry = sQry & Val(dCash) & ","
            sQry = sQry & Val(dNets) & ","
            sQry = sQry & Val(dVisa) & ","
            sQry = sQry & Val(dMaster) & ","
            sQry = sQry & Val(dAmex) & ","
            sQry = sQry & Val(dDiners) & ","
            sQry = sQry & Val(dChq) & ","
            sQry = sQry & Val(dTransferACC) & ","
            sQry = sQry & Val(dInstallment) & ","
            sQry = sQry & Val(d24K) & ","
            sQry = sQry & Val(dTradeInGold) & ","
            sQry = sQry & Val(dAdvancePayment) & ","
            sQry = sQry & Val(dPettyCash) & ","
            sQry = sQry & Val(dAdvanceUtilise) & ","
            sQry = sQry & Val(dTotalGSTSales) & ","
            sQry = sQry & Val(dGST) & ","
            sQry = sQry & Val(dTotalGSTNonGSTSales) & ","
            sQry = sQry & fncFormatSQLText(sUserID)
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            UpdateDailySales = SaveStatus
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
            SaveStatus = fncExecNonQrySales(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            FindTotalMarketPriceSave = SaveStatus
            sQry = Nothing
        End Try
    End Function
    Public Function GetPayModeDetail(ByVal sInvoiceNo As String) As DataSet

        Dim Ds As DataSet
        Dim sQry As String

        Try
            GetPayModeDetail = Nothing
            sQry = "SELECT PayMode,Amount FROM tblPaymodeDetail where InvoiceNo= " & fncFormatSQLText(sInvoiceNo)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Class", "Class")

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        Finally
            GetPayModeDetail = Ds
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
                                    ByVal dCommission As Double, ByVal sRemarks As String, ByVal dDailyClassRate As Double, _
                                    ByVal iSortCode As Integer, ByVal sSalesType As String, ByVal dTradeInGst As Double) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_SalesDetail " & fncFormatSQLText(sInvoiceNo) & ","
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
            sQry = sQry & fncFormatSQLText(sSalesType) & ","
            sQry = sQry & Val(dTradeInGst) & ","
            sQry = sQry & fncFormatSQLText(sUserID) & "" & vbCrLf
            SaveStatus = fncExecNonQrySales(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            SalesDetailSave = SaveStatus
            sQry = Nothing
        End Try
    End Function

    Public Function RFIDItemStatusUpdate(ByVal sInventorycode As String) As Boolean
        Dim bStatus As Boolean
        Dim sQry As String
        Try
            sQry = "update Item set ToDisplayInStockTake = '0',Status='SALES' Where ItemCode=" & fncFormatSQLText(sInventorycode)
            bStatus = ProjExecNonQryRFID(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            RFIDItemStatusUpdate = bStatus
            sQry = Nothing
        End Try
    End Function
    Public Function CreditSalesHeaderSave(ByVal sCreditInvoiceNo As String, ByVal sDate As String, _
                                   ByVal sCompanyCode As String, ByVal sLocationCode As String, ByVal sTerminalCode As String, _
                                       ByVal sCustomerCode As String, ByVal sMembercode As String, ByVal sCustomerName As String, _
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
            sQry = sQry & " EXEC sp_CreditSalesHeader " & fncFormatSQLText(sCreditInvoiceNo) & ","
            sQry = sQry & fncFormatSQLDate(sDate) & ","
            sQry = sQry & fncFormatSQLText(sCompanyCode) & ","
            sQry = sQry & fncFormatSQLText(sLocationCode) & ","
            sQry = sQry & fncFormatSQLText(sTerminalCode) & ","
            sQry = sQry & fncFormatSQLText(sCustomerCode) & ","
            sQry = sQry & fncFormatSQLText(sMembercode) & ","
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
            SaveStatus = fncExecNonQrySales(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            CreditSalesHeaderSave = SaveStatus
            sQry = Nothing
        End Try
    End Function
    Public Function CreditSalesDetailSave(ByVal sCreditInvoiceNo As String, ByVal sinventorycode As String, _
                                ByVal sDescription As String, ByVal sLocationCode As String, ByVal nWeight As Double, _
                                ByVal nStoneWeight As Double, ByVal nGoldLoss As Double, _
                                ByVal nRate As Double, ByVal NSMC As Double, _
                                ByVal nPCS As Double, ByVal nDetailDiscount As Double, _
                                ByVal dNetDiscount As Double, ByVal dGst As Double, _
                                ByVal dNetAmount As Double, ByVal dActualSales As Double, _
                                ByVal nTotalValue As Double, ByVal nTotalCost As Double, _
                                ByVal dCommission As Double, ByVal sRemarks As String, ByVal dDailyClassRate As Double, _
                                ByVal iSortCode As Integer, ByVal sSalesType As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_CreditSalesDetail " & fncFormatSQLText(sCreditInvoiceNo) & ","
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
            sQry = sQry & fncFormatSQLText(sSalesType) & ","
            sQry = sQry & fncFormatSQLText(sUserID) & "" & vbCrLf
            SaveStatus = fncExecNonQrySales(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            CreditSalesDetailSave = SaveStatus
            sQry = Nothing
        End Try
    End Function

    Public Function GetSalesHeaderByInsAgreementNo(ByVal sInsAgreementNo As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "SELECT * FROM tblSalesHeader WHERE InsAgreementNo = " & fncFormatSQLText(sInsAgreementNo)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "SH", "SH")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetSalesHeaderByInsAgreementNo = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function GetSalesHeaderByInvoiceNo(ByVal sInvoiceNo As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "SELECT * FROM tblSalesHeader WHERE InvoiceNo =" & fncFormatSQLText(sInvoiceNo)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Invoice", "Invoice")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetSalesHeaderByInvoiceNo = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetCreditSalesHeaderByInvoiceNo(ByVal sInvoiceNo As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "SELECT * FROM tblCreditSalesHeader WHERE InvoiceNo =" & fncFormatSQLText(sInvoiceNo)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Invoice", "Invoice")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetCreditSalesHeaderByInvoiceNo = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function MemberTransactionSave(ByVal sMemberCode As String, ByVal sInvoiceNo As String, ByVal sDate As String, _
                                ByVal dDiscountSalesAmount As Double, ByVal dMemberDiscount As Double, ByVal dNetTotal As Double, ByVal sLocationCode As String) As Boolean

        Dim sQry As String
        Dim SaveStatus As Boolean
        Try

            sQry = sQry & " EXEC sp_MemberTransaction " & fncFormatSQLText(sMemberCode) & ","
            sQry = sQry & fncFormatSQLText(sInvoiceNo) & ","
            sQry = sQry & fncFormatSQLDate(sDate) & ","
            sQry = sQry & Val(dDiscountSalesAmount) & ","
            sQry = sQry & Val(dMemberDiscount) & ","
            sQry = sQry & Val(dNetTotal) & ","
            sQry = sQry & fncFormatSQLText(sLocationCode) & ","
            sQry = sQry & fncFormatSQLText(sUserID)
            SaveStatus = fncExecNonQrySales(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            MemberTransactionSave = SaveStatus
            sQry = Nothing
        End Try
    End Function
    Public Function GetUpdatePVNumber() As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " EXEC sp_GetUpdatePVNumber "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Invoice", "Invoice")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetUpdatePVNumber = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Sub subOpenSalesConnection()
        gsSalesCon.ConnectionString = gsCon
        gsSalesCon.Open()
    End Sub

    Public Sub subBeginSalesTransaction()
        gsSalesTrans = gsSalesCon.BeginTransaction
    End Sub

    Public Sub subCommitSalesTransaction()
        gsSalesTrans.Commit()
    End Sub

    Public Sub subRollBackSalesTransaction()
        gsSalesTrans.Rollback()
    End Sub

    Public Sub subCloseSalesConnection()
        gsSalesCon.Close()
    End Sub

    Public Sub subOpenSalesConnectionHQ()
        gsSalesConHQ.ConnectionString = gsConAcc
        gsSalesConHQ.Open()
    End Sub

    Public Sub subBeginSalesTransactionHQ()
        gsSalesTransHQ = gsSalesConHQ.BeginTransaction
    End Sub

    Public Sub subCommitSalesTransactionHQ()
        gsSalesTransHQ.Commit()
    End Sub

    Public Sub subRollBackSalesTransactionHQ()
        gsSalesTransHQ.Rollback()
    End Sub

    Public Sub subCloseSalesConnectionHQ()
        gsSalesConHQ.Close()
    End Sub

    Public Sub subOpenSalesConnectionFlexi()
        gsSalesConFlexi.ConnectionString = gsConFlexi
        gsSalesConFlexi.Open()
    End Sub

    Public Sub subBeginSalesTransactionFlexi()
        gsSalesTransFlexi = gsSalesConFlexi.BeginTransaction
    End Sub

    Public Sub subCommitSalesTransactionFlexi()
        gsSalesTransFlexi.Commit()
    End Sub

    Public Sub subRollBackSalesTransactionFlexi()
        gsSalesTransFlexi.Rollback()
    End Sub

    Public Sub subCloseSalesConnectionFlexi()
        gsSalesConFlexi.Close()
    End Sub

    Public Function UpdateNextInvoice() As Boolean
        Dim bStatus As Boolean
        Dim sQry As String
        Try
            sQry = " update tblcompany set nextinvoice = nextinvoice + 1  "
            'bStatus = dm.ProjExecNonQry(sQry, gsSalesCon)
            bStatus = fncExecNonQrySales(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            UpdateNextInvoice = bStatus
            sQry = Nothing
        End Try
    End Function
    Public Function fncUpdateInsAgreement() As Boolean
        Dim bStatus As Boolean
        Dim sQry As String
        Try
            sQry = "update tblcompany set NextInsAgreementNo = NextInsAgreementNo + 1 Where CompanyId = '1'"
            bStatus = fncExecNonQrySales(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateInsAgreement = bStatus
            sQry = Nothing
        End Try
    End Function

    Public Function fncExecNonQrySales(ByVal Query As String) As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            Dim Cmd As New SqlClient.SqlCommand(Query, gsSalesCon)
            Cmd.Transaction = gsSalesTrans
            Cmd.ExecuteNonQuery()
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        Finally
            fncExecNonQrySales = bStatus
        End Try
    End Function
    Public Function fncExecNonQrySalesFlexi(ByVal Query As String) As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            Dim Cmd As New SqlClient.SqlCommand(Query, gsSalesConFlexi)
            Cmd.Transaction = gsSalesTransFlexi
            Cmd.ExecuteNonQuery()
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        Finally
            fncExecNonQrySalesFlexi = bStatus
        End Try
    End Function
    Public Function fncExecNonQrySalesAcc(ByVal Query As String) As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            Dim Cmd As New SqlClient.SqlCommand(Query, gsSalesConHQ)
            Cmd.Transaction = gsSalesTransHQ
            Cmd.ExecuteNonQuery()
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncExecNonQrySalesAcc = bStatus
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

    Public Function PayModeDetailSave(ByVal sInvoiceNo As String, ByVal sDate As String, ByVal sPaymode As String, ByVal nAmount As Double, ByVal nCommission As Double, ByVal sSlipNo As String, ByVal sSalesType As String) As Boolean
        Dim sExec As String
        Dim SaveStatus As Boolean
        Try
            sExec = sExec & "EXEC sp_PayModeDetail " & fncFormatSQLText(sInvoiceNo) & ","
            sExec = sExec & fncFormatSQLDate(sDate) & ","
            sExec = sExec & fncFormatSQLText(sPaymode) & ","
            sExec = sExec & Val(nAmount) & ","
            sExec = sExec & Val(nCommission) & ","
            sExec = sExec & fncFormatSQLText(sSlipNo) & ","
            sExec = sExec & fncFormatSQLText(sSalesType) & ","
            sExec = sExec & fncFormatSQLText(sUserID)
            SaveStatus = fncExecNonQrySales(sExec)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            PayModeDetailSave = SaveStatus
            sExec = Nothing
        End Try
    End Function
    Public Function PosMemberTransationSave(ByVal sLocationCode As String, ByVal sInvoiceNo As String, ByVal sDate As String, ByVal sCustomerCode As String, ByVal nAmount As Double, _
                                            ByVal nRewardPoints As Integer, ByVal nRedeemedPoints As Integer) As Boolean
        Dim sExec As String
        Dim SaveStatus As Boolean
        Try
            sExec = sExec & "EXEC sp_POSMemberTransactionSave " & fncFormatSQLText(sLocationCode) & ","
            sExec = sExec & fncFormatSQLText(sInvoiceNo) & ","
            sExec = sExec & fncFormatSQLDate(sDate) & ","
            sExec = sExec & fncFormatSQLText(sCustomerCode) & ","
            sExec = sExec & Val(nAmount) & ","
            sExec = sExec & Val(nRewardPoints) & ","
            sExec = sExec & Val(nRedeemedPoints) & ","
            sExec = sExec & fncFormatSQLText(sUserID)
            SaveStatus = fncExecNonQrySalesAcc(sExec)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            PosMemberTransationSave = SaveStatus
            sExec = Nothing
        End Try
    End Function

    Public Function fncUpdateTradeInRefNo(ByVal sInvoiceNo As String, ByVal sTradeInRefNo As String) As Boolean
        Dim bStatus As Boolean
        Dim sQry, sInventoryNo As String
        Try
            sInventoryNo = "TI-" & sInvoiceNo
            sQry = " Update tblTradeInHeader Set InventoryCode = " & fncFormatSQLText(sInventoryNo) & " , InvoiceNo = " & fncFormatSQLText(sInvoiceNo) & " Where InvoiceNo = " & fncFormatSQLText(sTradeInRefNo) & vbCrLf
            sQry = sQry & " Update tblTradeInDetail Set InventoryCode = " & fncFormatSQLText(sInventoryNo) & " Where InventoryCode = " & fncFormatSQLText("TI-" & sTradeInRefNo)
            bStatus = fncExecNonQrySales(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateTradeInRefNo = bStatus
            sQry = Nothing
        End Try
    End Function

    Public Function fncUpdateInstallmentRefNo(ByVal sInvoiceNo As String, ByVal sInsAgreementNo As String) As Boolean
        Dim bStatus As Boolean
        Dim sQry, sInventoryNo As String
        Try
            sQry = " Update tblInstallmentHeader Set InvoiceNo = " & fncFormatSQLText(sInvoiceNo) & " Where InsAgreementNo = " & fncFormatSQLText(sInsAgreementNo)
            bStatus = fncExecNonQrySales(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateInstallmentRefNo = bStatus
            sQry = Nothing
        End Try
    End Function
    ''HAj 
    Public Function SalesDetailSaveTradeInGST(ByVal sInvoiceNo As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_SalesDetailTradeInGST " & fncFormatSQLText(sInvoiceNo) & ","
            sQry = sQry & fncFormatSQLText(sUserID) & "" & vbCrLf
            SaveStatus = fncExecNonQrySales(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            SalesDetailSaveTradeInGST = SaveStatus
            sQry = Nothing
        End Try
    End Function

    Public Function InstallmentHeaderSave(ByVal sInsAgreementNo As String, ByVal sInvoiceNo As String, ByVal sLocationCode As String, _
                            ByVal sCustomerCode As String, ByVal sMembercode As String, ByVal sCustomerName As String, _
                            ByVal Address1 As String, ByVal Address2 As String, _
                            ByVal Address3 As String, ByVal SalesmanCode As String, _
                            ByVal dDepositAmt As Double, ByVal nTotalInstallment As Integer, ByVal nPaidInstallment As Integer, ByVal dInstallmentAmt As Double, _
                            ByVal dAmtpaid As Double, ByVal dSalesAmt As Double, _
                            ByVal sRemarks As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_InstallmentHeader "
            sQry = sQry & fncFormatSQLText(sInsAgreementNo) & ", "
            sQry = sQry & fncFormatSQLText(sInvoiceNo) & ", "
            sQry = sQry & fncFormatSQLText(sLocationCode) & ", "
            sQry = sQry & fncFormatSQLText(sCustomerCode) & ", "
            sQry = sQry & fncFormatSQLText(sMembercode) & ", "
            sQry = sQry & fncFormatSQLText(sCustomerName) & ", "
            sQry = sQry & fncFormatSQLText(Address1) & ", "
            sQry = sQry & fncFormatSQLText(Address2) & ", "
            sQry = sQry & fncFormatSQLText(Address3) & ", "
            sQry = sQry & fncFormatSQLText(SalesmanCode) & ", "
            sQry = sQry & Val(dDepositAmt) & ", "
            sQry = sQry & Val(nTotalInstallment) & ", "
            sQry = sQry & Val(nPaidInstallment) & ", "
            sQry = sQry & Val(dInstallmentAmt) & ", "
            sQry = sQry & Val(dAmtpaid) & ", "
            sQry = sQry & Val(dSalesAmt) & ", "
            sQry = sQry & fncFormatSQLText(sRemarks) & ", "
            sQry = sQry & fncFormatSQLText(sUserID)
            SaveStatus = fncExecNonQrySales(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            InstallmentHeaderSave = SaveStatus
            sQry = Nothing
        End Try
    End Function
    Public Function fncGetTerminal(ByVal sConn As String, ByVal sDsName As String) As DataSet
        Dim DS As New DataSet
        Dim sSql As String
        Try
            sSql = "Select * From tblTerminal Where ComputerName = HOST_NAME()"
            DS = fncGetDatasetWithQuery(sConn, sDsName, sSql)
            fncGetTerminal = DS
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            fncGetTerminal = Nothing
        Finally
            DS = Nothing
        End Try
    End Function
    Public Function fncGetTerminalSalesList(ByVal sConn As String, ByVal sDsName As String, ByVal sTerminalCode As String, _
                                            ByVal sDate As String) As DataSet
        Dim DS As New DataSet
        Dim sSql As String
        Try
            sSql = "Select * From tblTerminalDinamination"
            sSql = sSql & " Where PostingDate = " & fncFormatSQLDate(sDate)
            'sSql = sSql & " And TerminalCode = " & fncFormatSQLText(sTerminalCode)
            sSql = sSql & " Order By Qty Desc"
            DS = fncGetDatasetWithQuery(sConn, sDsName, sSql)
            fncGetTerminalSalesList = DS
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            fncGetTerminalSalesList = Nothing
        Finally
            DS = Nothing
        End Try
    End Function
    Public Function fncGetTerminalSales(ByVal sConn As String, ByVal sDsName As String, ByVal sTerminalCode As String, _
                                           ByVal sDate As String) As DataSet
        Dim DS As New DataSet
        Dim sSql As String
        Try
            'sSql = "SELECT A.PayMode,Amount=sum(A.Amount),B.TerminalCode FROM  tblPaymodeDetail A,tblSalesHeader B ,tblPayMode c WHERE A.Invoiceno=B.InvoiceNo "
            'sSql = sSql & " And convert(char(8),B.InvoiceDate,112) = " & fncFormatTextDate(sDate)
            'sSql = sSql & " And B.TerminalCode = " & fncFormatSQLText(sTerminalCode)
            'sSql = sSql & "  And a.Paymode = c.Paymode " & "group By B.terminalCode,A.PayMode,c.Position Order By c.Position  "
            'sSql = "SELECT A.PayMode,Amount=sum(A.Amount),TerminalCode = '' FROM  tblPaymodeDetail A,tblSalesHeader B ,tblPayMode c WHERE A.Invoiceno=B.InvoiceNo "
            'sSql = sSql & " And B.InvoiceDate = " & fncFormatSQLDate(sDate)
            'sSql = sSql & " And B.TerminalCode = " & fncFormatSQLText(sTerminalCode)
            'sSql = sSql & "  And a.Paymode = c.Paymode " & "group By A.PayMode,c.Position Order By c.Position  "
            If Trim(sCompanyShortCode) = "AJ" Then
                sSql = "SELECT A.PayMode,Amount=sum(A.Amount),TerminalCode = '' FROM  tblPaymodeDetail A,tblPayMode c "
                sSql = sSql & " WHERE A.InvoiceDate =  " & fncFormatSQLDate(sDate) & "  And A.Paymode = C.Paymode and SalesType<>'PAYOUT' and IsPaid=1"
                sSql = sSql & " group By A.PayMode,c.Position Order By c.Position"
            Else
                sSql = "SELECT A.PayMode,Amount=sum(A.Amount),TerminalCode = '' FROM  tblPaymodeDetail A,tblPayMode c "
                sSql = sSql & " WHERE A.InvoiceDate =  " & fncFormatSQLDate(sDate) & "  And A.Paymode = C.Paymode and SalesType<>'PAYOUT'"
                sSql = sSql & " group By A.PayMode,c.Position Order By c.Position"
            End If
            
            DS = fncGetDatasetWithQuery(sConn, sDsName, sSql)
            fncGetTerminalSales = DS
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            fncGetTerminalSales = Nothing
        Finally
            DS = Nothing
        End Try
    End Function
    Public Function fncGetTerminalSalesPayOut(ByVal sConn As String, ByVal sDsName As String, ByVal sTerminalCode As String, _
                                       ByVal sDate As String) As DataSet
        Dim DS As New DataSet
        Dim sSql As String
        Try
            'sSql = "SELECT A.PayMode,Amount=sum(A.Amount),B.TerminalCode FROM  tblPaymodeDetail A,tblSalesHeader B ,tblPayMode c WHERE A.Invoiceno=B.InvoiceNo "
            'sSql = sSql & " And convert(char(8),B.InvoiceDate,112) = " & fncFormatTextDate(sDate)
            'sSql = sSql & " And B.TerminalCode = " & fncFormatSQLText(sTerminalCode)
            'sSql = sSql & "  And a.Paymode = c.Paymode " & "group By B.terminalCode,A.PayMode,c.Position Order By c.Position  "
            'sSql = "SELECT A.PayMode,Amount=sum(A.Amount),TerminalCode = '' FROM  tblPaymodeDetail A,tblSalesHeader B ,tblPayMode c WHERE A.Invoiceno=B.InvoiceNo "
            'sSql = sSql & " And B.InvoiceDate = " & fncFormatSQLDate(sDate)
            'sSql = sSql & " And B.TerminalCode = " & fncFormatSQLText(sTerminalCode)
            'sSql = sSql & "  And a.Paymode = c.Paymode " & "group By A.PayMode,c.Position Order By c.Position  "

            sSql = "SELECT 'EXPENSES' PayMode,Amount=sum(A.Amount),TerminalCode = '' FROM  tblPaymodeDetail A,tblPayMode c "
            sSql = sSql & " WHERE A.InvoiceDate =  " & fncFormatSQLDate(sDate) & "  And A.Paymode = C.Paymode and SalesType='PAYOUT' and IsPaid=1"
            sSql = sSql & " group By A.PayMode,c.Position Order By c.Position"

            DS = fncGetDatasetWithQuery(sConn, sDsName, sSql)
            fncGetTerminalSalesPayOut = DS
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            fncGetTerminalSalesPayOut = Nothing
        Finally
            DS = Nothing
        End Try
    End Function
    Public Function fncGetPaidStatus(ByVal sConn As String, ByVal sDsName As String, ByVal sTerminalCode As String, _
                                               ByVal sDate As String) As DataSet
        Dim DS As New DataSet
        Dim sSql As String
        Try
            'sSql = "SELECT A.PayMode,Amount=sum(A.Amount),B.TerminalCode FROM  tblPaymodeDetail A,tblSalesHeader B ,tblPayMode c WHERE A.Invoiceno=B.InvoiceNo "
            'sSql = sSql & " And convert(char(8),B.InvoiceDate,112) = " & fncFormatTextDate(sDate)
            'sSql = sSql & " And B.TerminalCode = " & fncFormatSQLText(sTerminalCode)
            'sSql = sSql & "  And a.Paymode = c.Paymode " & "group By B.terminalCode,A.PayMode,c.Position Order By c.Position  "
            'sSql = "SELECT A.PayMode,Amount=sum(A.Amount),TerminalCode = '' FROM  tblPaymodeDetail A,tblSalesHeader B ,tblPayMode c WHERE A.Invoiceno=B.InvoiceNo "
            'sSql = sSql & " And B.InvoiceDate = " & fncFormatSQLDate(sDate)
            'sSql = sSql & " And B.TerminalCode = " & fncFormatSQLText(sTerminalCode)
            'sSql = sSql & "  And a.Paymode = c.Paymode " & "group By A.PayMode,c.Position Order By c.Position  "

            sSql = "SELECT * FROM  tblPaymodeDetail A,tblPayMode c "
            sSql = sSql & " WHERE A.InvoiceDate =  " & fncFormatSQLDate(sDate) & "  And A.Paymode = C.Paymode and IsPaid=0"

            DS = fncGetDatasetWithQuery(sConn, sDsName, sSql)
            fncGetPaidStatus = DS
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            fncGetPaidStatus = Nothing
        Finally
            DS = Nothing
        End Try
    End Function
    Public Function fncGetFlexiPaidStatus(ByVal sConn As String, ByVal sDsName As String, _
                                           ByVal sDate As String) As DataSet
        Dim DS As New DataSet
        Dim sSql As String
        Try

            sSql = "SELECT * FROM  tblPaymodeDetail A,tblPayMode c "
            sSql = sSql & " WHERE A.InvoiceDate =  " & fncFormatSQLDate(sDate) & "  And A.Paymode = C.Paymode and IsPaid=0"

            DS = fncGetDatasetWithQuery(sConn, sDsName, sSql)
            fncGetFlexiPaidStatus = DS
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            fncGetFlexiPaidStatus = Nothing
        Finally
            DS = Nothing
        End Try
    End Function
    Public Function fncGetExistingPayModeList(ByVal sConn As String, ByVal sDsName As String, ByVal sTerminalCode As String, _
                                       ByVal sDate As String) As DataSet
        Dim DS As New DataSet
        Dim sSql As String
        Try
            sSql = "Select * from tblTerminalSales Where Date = " & fncFormatSQLDate(sDate)
            'sSql = sSql & " And Terminalcode = " & fncFormatSQLText(sTerminalCode)
            DS = fncGetDatasetWithQuery(sConn, sDsName, sSql)
            fncGetExistingPayModeList = DS
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            fncGetExistingPayModeList = Nothing
        Finally
            DS = Nothing
        End Try
    End Function
    Public Function fncCheckSettlementonOldDate(ByVal sConn As String, ByVal sTerminalCode As String, _
                                   ByVal sDate As String) As Boolean
        Dim DS As New DataSet
        Dim sSql As String
        Try
            sSql = "Select top 1 * From tblTerminalSales Where LocationCode = " & fncFormatSQLText(gthisLocation)
            sSql = sSql & " And Date > " & fncFormatSQLDate(sDate)
            DS = fncGetDatasetWithQuery(sConn, "Ter", sSql)
            If DS.Tables("Ter").Rows.Count > 0 Then
                fncCheckSettlementonOldDate = True
            Else
                fncCheckSettlementonOldDate = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            fncCheckSettlementonOldDate = False
        Finally
            DS = Nothing
        End Try
    End Function
    Public Function fncGetCurrencyDinamination(ByVal sConn As String, ByVal sDsName As String) As DataSet
        Dim DS As New DataSet
        Dim sSql As String
        Try
            sSql = "Select * From tblCurrencyDinamination Order By CurrencyValue Desc"
            DS = fncGetDatasetWithQuery(sConn, sDsName, sSql)
            fncGetCurrencyDinamination = DS
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            fncGetCurrencyDinamination = Nothing
        Finally
            DS = Nothing
        End Try
    End Function
    Public Function fncDeleteExsistingTerminalSales(ByVal sDate As String, ByVal sTerminalCode As String) As Boolean
        Dim sExec As String
        Dim bResult As Boolean
        Try
            sExec = "DELETE FROM tblTerminalSales WHERE Date=" & fncFormatSQLText(sDate) & "and TerminalCode=" & fncFormatSQLText(sTerminalCode) & vbCrLf
            sExec = sExec & "DELETE FROM tblTerminalDinamination WHERE  PostingDate=" & fncFormatSQLText(sDate) & "and TerminalCode=" & fncFormatSQLText(sTerminalCode)
            bResult = fncExecuteNonQuery(sExec, gsConnection, gsTrans)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            bResult = False
        Finally
            fncDeleteExsistingTerminalSales = bResult
        End Try
    End Function
    Public Function fncTerminalSalesDinaminationAmountSave(ByVal sCashierId As String, ByVal sTerminalCode As String, _
                                                 ByVal sDate As String, _
                                                 ByVal nValue As Decimal, ByVal nQty As Decimal, _
                                                 ByRef bResult As Boolean) As Boolean
        Dim sExec As String
        Try
            sExec = "Exec sp_Dinamination " & fncFormatSQLText(sCashierId) & ","
            sExec = sExec & fncFormatSQLText(sTerminalCode) & ","
            sExec = sExec & fncFormatSQLDate(sDate) & ","
            sExec = sExec & nValue & ","
            sExec = sExec & nQty
            bResult = fncExecuteNonQuery(sExec, gsConnection, gsTrans)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            bResult = False
        Finally
            fncTerminalSalesDinaminationAmountSave = bResult
        End Try
    End Function
    Public Function fncTerminalSalesPayModeAmountSave(ByVal sCashierId As String, ByVal sDate As String, _
                                                     ByVal sTerminalCode As String, ByVal sPaymode As String, _
                                                     ByVal nComputerAmount As Decimal, ByVal nManualAmount As Decimal, _
                                                     ByRef bResult As Boolean) As Boolean
        Dim sExec As String
        Try
            sExec = "Exec sp_TerminalSales " & fncFormatSQLText(sCashierId) & ","
            sExec = sExec & fncFormatSQLDate(sDate) & ","
            sExec = sExec & fncFormatSQLText(sTerminalCode) & ","
            sExec = sExec & fncFormatSQLText(sPaymode) & ","
            sExec = sExec & fncFormatSQLText(gthisLocation) & ","
            sExec = sExec & Val(nComputerAmount) & ","
            sExec = sExec & Val(nManualAmount)
            bResult = fncExecuteNonQuery(sExec, gsConnection, gsTrans)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            bResult = False
        Finally
            fncTerminalSalesPayModeAmountSave = bResult
        End Try
    End Function
    Public Function fncGetSalesHeader(ByVal sConn As String, ByVal sDsName As String, ByVal sTerminal As String) As DataSet
        Dim DS As New DataSet
        Dim sSql As String
        Try
            'sSql = "Select InvoiceNo , Custcode , ModifyDate , NetTotal ,TotalLines , TenderAmount , Balance , CashierID From tblSalesHeader Where Convert(Char(8) , InvoiceDate , 112)= " & fncFormatSQLDate(ServerDate)
            'sSql = sSql + " And TerminalCode =" & fncFormatSQLText(sTerminal)
            'sSql = "select distinct a.InvoiceNo , Customercode , TerminalCode,SalesManName, NetTotal ,TotalPCS , PaidAmt , BalanceAmt From tblSalesHeader a, "
            'sSql = sSql + " tblSalesman b,tblPaymodedetail c where a.SalesMancode=b.SalesMancode and a.Invoiceno=c.Invoiceno and IsPaid=0 and a.InvoiceDate= " & fncFormatSQLDate(ServerDate)
            ' sSql = "select InvoiceNo ,InvoiceDate, Customercode , TerminalCode,SalesManName, NetTotal ,TotalPCS , PaidAmt , BalanceAmt From [vwPaymodeEntry]  where InvoiceDate= " & fncFormatSQLDate(ServerDate)
            sSql = "select InvoiceNo ,InvoiceDate, Customercode , TerminalCode,SalesManName, NetTotal ,TotalPCS , PaidAmt , BalanceAmt,GST From [vwPaymodeEntry]  where InvoiceDate= " & fncFormatSQLDate(ServerDate)
            DS = fncGetDatasetWithQuery(sConn, sDsName, sSql)
            fncGetSalesHeader = DS
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            fncGetSalesHeader = Nothing
        Finally
            DS = Nothing
        End Try
    End Function
    Public Function fncGeteTRS(ByVal sConn As String, ByVal sDsName As String, ByVal sTerminal As String) As DataSet
        Dim DS As New DataSet
        Dim sSql As String
        Try
            'sSql = "Select InvoiceNo , Custcode , ModifyDate , NetTotal ,TotalLines , TenderAmount , Balance , CashierID From tblSalesHeader Where Convert(Char(8) , InvoiceDate , 112)= " & fncFormatSQLDate(ServerDate)
            'sSql = sSql + " And TerminalCode =" & fncFormatSQLText(sTerminal)
            'sSql = "select distinct a.InvoiceNo , Customercode , TerminalCode,SalesManName, NetTotal ,TotalPCS , PaidAmt , BalanceAmt From tblSalesHeader a, "
            'sSql = sSql + " tblSalesman b,tblPaymodedetail c where a.SalesMancode=b.SalesMancode and a.Invoiceno=c.Invoiceno and IsPaid=0 and a.InvoiceDate= " & fncFormatSQLDate(ServerDate)
            ' sSql = "select InvoiceNo ,InvoiceDate, Customercode , TerminalCode,SalesManName, NetTotal ,TotalPCS , PaidAmt , BalanceAmt From [vwPaymodeEntry]  where InvoiceDate= " & fncFormatSQLDate(ServerDate)
            If Trim(Objuser.CompanyName) = "ABIRAAME JEWELLERS" Then
                sSql = "select InvoiceNo ,InvoiceDate, Remarks , Gst,NetTotal From tblSalesHeader  where Remarks<>'' and  InvoiceDate= " & fncFormatSQLDate(ServerDate)
                sSql = sSql + " And InvoiceNo not in(Select BillNo from tbleTRsStatement)"
                DS = fncGetDatasetWithQuery(sConn, sDsName, sSql)
            Else
                sSql = "select InvoiceNo ,InvoiceDate, Remarks , Gst,NetTotal From tblSalesHeader  where JournalNo='Y' and  InvoiceDate= " & fncFormatSQLDate(ServerDate)
                sSql = sSql + " And InvoiceNo not in(Select BillNo from tbleTRsStatement)"
                DS = fncGetDatasetWithQuery(sConn, sDsName, sSql)
            End If

            fncGeteTRS = DS
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            fncGeteTRS = Nothing
        Finally
            DS = Nothing
        End Try
    End Function
    
    Public Function fncGetSalesHeaderAksayaTritiya(ByVal sConn As String, ByVal sDsName As String, ByVal sTerminal As String, ByVal sInvoiceNo As String) As DataSet
        Dim DS As New DataSet
        Dim sSql As String
        Try
            'sSql = "Select InvoiceNo , Custcode , ModifyDate , NetTotal ,TotalLines , TenderAmount , Balance , CashierID From tblSalesHeader Where Convert(Char(8) , InvoiceDate , 112)= " & fncFormatSQLDate(ServerDate)
            'sSql = sSql + " And TerminalCode =" & fncFormatSQLText(sTerminal)
            'sSql = "select distinct a.InvoiceNo , Customercode , TerminalCode,SalesManName, NetTotal ,TotalPCS , PaidAmt , BalanceAmt From tblSalesHeader a, "
            'sSql = sSql + " tblSalesman b,tblPaymodedetail c where a.SalesMancode=b.SalesMancode and a.Invoiceno=c.Invoiceno and IsPaid=0 and a.InvoiceDate= " & fncFormatSQLDate(ServerDate)
            If Trim(sInvoiceNo) <> "" Then
                sSql = "select InvoiceNo ,InvoiceDate, Customercode , TerminalCode,SalesManName, NetTotal ,TotalPCS , PaidAmt , BalanceAmt From [vwPaymodeEntryAksayaTritiya] where InvoiceNo = " & fncFormatSQLText(sInvoiceNo)
            Else
                sSql = "select InvoiceNo ,InvoiceDate, Customercode , TerminalCode,SalesManName, NetTotal ,TotalPCS , PaidAmt , BalanceAmt From [vwPaymodeEntryAksayaTritiya] where locationcode='85'"
            End If
            'sSql = sSql + " an  LocationCode = " & fncFormatSQLText(sLocationcode)
            DS = fncGetDatasetWithQuery(sConn, sDsName, sSql)
            fncGetSalesHeaderAksayaTritiya = DS
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            fncGetSalesHeaderAksayaTritiya = Nothing
        Finally
            DS = Nothing
        End Try
    End Function
    Public Function fncGetFlexiDetails(ByVal sConn As String, ByVal sDsName As String, ByVal sTerminal As String) As DataSet
        Dim DS As New DataSet
        Dim sSql As String
        Dim slocationCode As String
        Try
            slocationCode = Trim(gthisLocation)
            If Trim(slocationCode) = "99" Then
                slocationCode = "103"
            ElseIf Trim(slocationCode) = "27" Then
                slocationCode = "1"
            End If
            sSql = "select SubscriptionID,NricNo,FlexiType,InvoiceNo,PayMode,Amount,CreateUser from tblpaymodedetailforflexi where IsProcessed='N' and paymode<>'Online' and InvoiceDate= " & fncFormatSQLDate(ServerDate)
            sSql = sSql & " and LocationCode = " & fncFormatSQLText(slocationCode)

            'sSql = "select distinct a.InvoiceNo , Customercode , TerminalCode,SalesManName, NetTotal ,TotalPCS , PaidAmt , BalanceAmt From tblSalesHeader a, "
            'sSql = sSql + " tblSalesman b,tblPaymodedetail c where a.SalesMancode=b.SalesMancode and a.Invoiceno=c.Invoiceno and IsPaid=0 and a.InvoiceDate= " & fncFormatSQLDate(ServerDate)
            'sSql = sSql + " And TerminalCode =" & fncFormatSQLText(sTerminal)
            DS = fncGetDatasetWithQuery(sConn, sDsName, sSql)
            fncGetFlexiDetails = DS
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            fncGetFlexiDetails = Nothing
        Finally
            DS = Nothing
        End Try
    End Function
    Public Function fncGetSalesHeaderByInvoiceNo(ByVal sConn As String, ByVal sDsName As String, ByVal sInvoiceNo As String) As DataSet
        Dim DS As New DataSet
        Dim sSql As String
        Try
            'sSql = "Select InvoiceNo , Custcode , ModifyDate , NetTotal ,TotalLines , TenderAmount , Balance , CashierID From tblSalesHeader Where Convert(Char(8) , InvoiceDate , 112)= " & fncFormatSQLDate(ServerDate)
            'sSql = sSql + " And TerminalCode =" & fncFormatSQLText(sTerminal)
            If Trim(sCompanyShortCode) = "MJ" Then

                sSql = "select a.InvoiceNo , Customercode , TerminalCode,SalesManName, NetTotal ,TotalPCS , PaidAmt , BalanceAmt,GST From tblSalesHeader a, "
                sSql = sSql + " tblSalesman b where a.SalesMancode=b.SalesMancode and  a.InvoiceNo= " & fncFormatSQLText(sInvoiceNo)


            Else
                If Trim(gthisLocationType) <> "HQ" Then
                    sSql = "select a.InvoiceNo , Customercode , TerminalCode,SalesManName, NetTotal ,TotalPCS , PaidAmt , BalanceAmt,GST From tblSalesHeader a, "
                    sSql = sSql + " tblSalesman b where a.SalesMancode=b.SalesMancode and  a.InvoiceNo= " & fncFormatSQLText(sInvoiceNo)
                    sSql = sSql + " and a.Invoiceno in(Select InvoiceNo from tblPaymodeDetail where InvoiceNo = " & fncFormatSQLText(sInvoiceNo) & " and ispaid=0)"
                Else
                    sSql = "Select InvoiceNo , InvoiceDate Customercode , SalesType TerminalCode,Locationcode SalesManName, Amount  NetTotal,0 TotalPCS, Amount PaidAmt , Amount BalanceAmt,GST from tblPaymodeDetail with(nolock) where InvoiceNo = " & fncFormatSQLText(sInvoiceNo) & " and ispaid=0"

                End If
            End If


            'sSql = sSql + " And TerminalCode =" & fncFormatSQLText(sTerminal)
            DS = fncGetDatasetWithQuery(sConn, sDsName, sSql)
            fncGetSalesHeaderByInvoiceNo = DS
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            fncGetSalesHeaderByInvoiceNo = Nothing
        Finally
            DS = Nothing
        End Try
    End Function
    Public Function fncGetSalesHeaderHistoryByInvoiceNo(ByVal sInvoiceNo As String) As DataSet
        Dim DS As New DataSet
        Dim sSql As String
        Try
            'sSql = "Select InvoiceNo , Custcode , ModifyDate , NetTotal ,TotalLines , TenderAmount , Balance , CashierID From tblSalesHeader Where Convert(Char(8) , InvoiceDate , 112)= " & fncFormatSQLDate(ServerDate)
            'sSql = sSql + " And TerminalCode =" & fncFormatSQLText(sTerminal)
            sSql = "select * From tblSalesHeader a, "
            sSql = sSql + " tblSalesman b where a.SalesMancode=b.SalesMancode and  a.InvoiceNo= " & fncFormatSQLText(sInvoiceNo)
            'sSql = sSql + " And TerminalCode =" & fncFormatSQLText(sTerminal)
            DS = dm.getDataSetWithSqlAdapter(sSql, "Inventory", "Inventory")
            fncGetSalesHeaderHistoryByInvoiceNo = DS
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            fncGetSalesHeaderHistoryByInvoiceNo = Nothing
        Finally
            DS = Nothing
        End Try
    End Function
    Public Function fncGetSalesHeaderByInvoiceNoAksayaTritiya(ByVal sConn As String, ByVal sDsName As String, ByVal sLocationCode As String, ByVal sInvoiceNo As String) As DataSet
        Dim DS As New DataSet
        Dim sSql As String
        Try
            'sSql = "Select InvoiceNo , Custcode , ModifyDate , NetTotal ,TotalLines , TenderAmount , Balance , CashierID From tblSalesHeader Where Convert(Char(8) , InvoiceDate , 112)= " & fncFormatSQLDate(ServerDate)
            'sSql = sSql + " And TerminalCode =" & fncFormatSQLText(sTerminal)
            'sSql = "select InvoiceNo ,InvoiceDate, Customercode , TerminalCode,SalesManName, NetTotal ,TotalPCS , PaidAmt , BalanceAmt From [vwPaymodeEntryAksayaTritiya]  where InvoiceDate= " & fncFormatSQLDate(ServerDate)
            sSql = "select InvoiceNo ,InvoiceDate, Customercode , TerminalCode,SalesManName, NetTotal ,TotalPCS , PaidAmt , BalanceAmt From [vwPaymodeEntryAksayaTritiya]  where "
            sSql = sSql + " Invoiceno = " & fncFormatSQLText(sInvoiceNo)
            '  sSql = sSql + " and LocationCode = " & fncFormatSQLText(sLocationCode) & " and Invoiceno = " & fncFormatSQLText(sInvoiceNo)
            DS = fncGetDatasetWithQuery(sConn, sDsName, sSql)
            fncGetSalesHeaderByInvoiceNoAksayaTritiya = DS
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            fncGetSalesHeaderByInvoiceNoAksayaTritiya = Nothing
        Finally
            DS = Nothing
        End Try
    End Function
    Public Function fncGetPayModeDetail(ByVal sConn As String, ByVal sDsName As String, ByVal sInvoiveNo As String) As DataSet
        Dim ds As DataSet
        Dim sSql As String
        sSql = "Select A.Paymode , Amount = IsNull(B.Amount , 0) , SlipNo = IsNull(B.SlipNo , '') From tblPayMode A Left Outer Join tblPaymodeDetail B On A.Paymode = B.PayMode And InvoiceNo = " & fncFormatSQLText(sInvoiveNo)
        sSql = sSql & " Order By A.Position"
        ds = fncGetDatasetWithQuery(sConn, sDsName, sSql)
        fncGetPayModeDetail = ds
        ds = Nothing
    End Function
    Public Function fncGetPayModeDetailAkhsaya(ByVal sConn As String, ByVal sDsName As String, ByVal sInvoiveNo As String) As DataSet
        Dim ds As DataSet
        Dim sSql As String
        sSql = "Select A.Paymode , Amount = IsNull(B.Amount , 0) , SlipNo = IsNull(B.SlipNo , '') From tblPayMode A Left Outer Join tblPaymodeDetail2017 B On A.Paymode = B.PayMode And InvoiceNo = " & fncFormatSQLText(sInvoiveNo)
        sSql = sSql & " Order By A.Position"
        ds = fncGetDatasetWithQuery(sConn, sDsName, sSql)
        fncGetPayModeDetailAkhsaya = ds
        ds = Nothing
    End Function
    Public Function fncGetPayModeDetailFlexi(ByVal sConn As String, ByVal sDsName As String, ByVal sInvoiveNo As String) As DataSet
        Dim ds As DataSet
        Dim sSql As String
        'sSql = "Select Paymode , Amount = IsNull(Amount , 0) , SlipNo = IsNull(SlipNo , '') From tblPaymodeDetailForFlexi where InvoiceNo = " & fncFormatSQLText(sInvoiveNo)
        sSql = "Select A.Paymode , Amount = IsNull(B.Amount , 0) , SlipNo = IsNull(B.SlipNo , '') From tblPayMode A Left Outer Join tblPaymodeDetailForFlexi B On A.Paymode = B.PayMode And InvoiceNo = " & fncFormatSQLText(sInvoiveNo)
        sSql = sSql & " Order By A.Position"
        ds = fncGetDatasetWithQuery(sConn, sDsName, sSql)
        fncGetPayModeDetailFlexi = ds
        ds = Nothing
    End Function
    Public Function fncGetPayModeDetailByInvoice(ByVal sConn As String, ByVal sDsName As String, ByVal sInvoiveNo As String) As DataSet
        Dim ds As DataSet
        Dim sSql As String
        sSql = "select *  from tblPaymodeDetail where InvoiceNo = " & fncFormatSQLText(sInvoiveNo)
        ds = fncGetDatasetWithQuery(sConn, sDsName, sSql)
        fncGetPayModeDetailByInvoice = ds
        ds = Nothing
    End Function
    Public Function fncUpdatePayMode(ByVal sInvoiceNo As String, ByVal sUserId As String) As Boolean
        Dim sExec As String
        Dim bResult As Boolean
        Try
            sExec = "Exec Sp_PayModeUpdate " & fncFormatSQLText(sInvoiceNo) & ","
            sExec = sExec & fncFormatSQLText(sUserId)
            bResult = fncExecuteNonQuery(sExec, gsConnection, gsTrans)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            bResult = False
        Finally
            fncUpdatePayMode = bResult
        End Try
    End Function

    Public Function fncUpdatePayModeGST(ByVal sInvoiceNo As String, ByVal sActualAmount As Double, ByVal sGST As Double, ByVal sUserId As String) As Boolean
        Dim sExec As String
        Dim bResult As Boolean
        Try
            sExec = "Exec Sp_PayModeUpdateGST " & fncFormatSQLText(sInvoiceNo) & ","
            sExec = sExec & Val(sActualAmount) & ","
            sExec = sExec & Val(sGST) & ","
            sExec = sExec & fncFormatSQLText(sUserId)
            bResult = fncExecuteNonQuery(sExec, gsConnection, gsTrans)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            bResult = False
        Finally
            fncUpdatePayModeGST = bResult
        End Try
    End Function
    Public Function fncUpdatePayModeFlexi(ByVal sInvoiceNo As String, ByVal sUserId As String) As Boolean
        Dim sExec As String
        Dim bResult As Boolean
        Try
            sExec = "Exec Sp_PayModeUpdateFlexi " & fncFormatSQLText(sInvoiceNo) & ","
            sExec = sExec & fncFormatSQLText(sUserId)
            bResult = fncExecuteNonQuery(sExec, gsConnection, gsTrans)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            bResult = False
        Finally
            fncUpdatePayModeFlexi = bResult
        End Try
    End Function
    'Public Function fncUpdatePayModeFlexiClosure(ByVal sInvoiceNo As String, ByVal sLocationCode As String, ByVal sUserId As String) As Boolean
    '    Dim sExec As String
    '    Dim bResult As Boolean
    '    Try
    '        sExec = "Exec Sp_PayModeUpdateFlexiClosure " & fncFormatSQLText(sInvoiceNo) & ","
    '        sExec = sExec & fncFormatSQLText(sLocationCode) & ","
    '        sExec = sExec & fncFormatSQLText(sUserId)
    '        bResult = fncExecuteNonQuery(sExec, gsConnection, gsTrans)
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical)
    '        bResult = False
    '    Finally
    '        fncUpdatePayModeFlexiClosure = bResult
    '    End Try
    'End Function
    Public Function fncUpdatePayModeDetails(ByVal sInvoiceNo As String, ByVal sUserId As String) As Boolean
        Dim sExec As String
        Dim bResult As Boolean
        Try
            sExec = "Exec Sp_PayModeModification " & fncFormatSQLText(sInvoiceNo) & " , "
            sExec = sExec & fncFormatSQLText(sUserId)
            bResult = fncExecuteNonQuery(sExec, gsConnection, gsTrans)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            bResult = False
        Finally
            fncUpdatePayModeDetails = bResult
        End Try
    End Function
    Public Function fncUpdatePayModeDetailsAkshaya(ByVal sInvoiceNo As String, ByVal sUserId As String) As Boolean
        Dim sExec As String
        Dim bResult As Boolean
        Try
            sExec = "Exec Sp_PayModeModificationAkhsaya " & fncFormatSQLText(sInvoiceNo) & " , "
            sExec = sExec & fncFormatSQLText(sUserId)
            bResult = fncExecuteNonQuery(sExec, gsConnection, gsTrans)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            bResult = False
        Finally
            fncUpdatePayModeDetailsAkshaya = bResult
        End Try
    End Function
    Public Function fncUpdatePayModeDetailRemarks(ByVal sInvoiceNo As String, ByVal sSalesmanCode As String, ByVal sRemarks As String, ByVal sUserId As String) As Boolean
        Dim sExec As String
        Dim bResult As Boolean
        Try
            sExec = "Exec Sp_PayModeChangeRemarks " & fncFormatSQLText(sInvoiceNo) & " , "
            sExec = sExec & fncFormatSQLText(sSalesmanCode) & " , "
            sExec = sExec & fncFormatSQLText(sRemarks) & " , "
            sExec = sExec & fncFormatSQLText(sUserId)
            bResult = fncExecuteNonQuery(sExec, gsConnection, gsTrans)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            bResult = False
        Finally
            fncUpdatePayModeDetailRemarks = bResult
        End Try
    End Function
    Public Function fncSalesPaymodeDetailSave(ByVal sInvoiceNo As String, ByVal sInvoiceDate As String, _
                ByVal sPaymode As String, ByVal nAmount As Decimal, ByVal nCommision As Decimal, _
                ByVal sSlipNo As String, ByVal sSalesType As String) As Boolean
        Dim sExec As String
        Dim bResult As Boolean
        Try
            sExec = "Exec sp_PayModeDetail " & fncFormatSQLText(sInvoiceNo) & " , "
            sExec = sExec & fncFormatSQLDate(sInvoiceDate) & " , "
            sExec = sExec & fncFormatSQLText(sPaymode) & " , "
            sExec = sExec & Val(nAmount) & " , "
            sExec = sExec & Val(nCommision) & " , "
            sExec = sExec & fncFormatSQLText(sSlipNo) & " , "
            sExec = sExec & fncFormatSQLText(sSalesType) & " , "
            sExec = sExec & fncFormatSQLText(sUserID)
            bResult = fncExecuteNonQuery(sExec, gsConnection, gsTrans)
            fncSalesPaymodeDetailSave = bResult
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            fncSalesPaymodeDetailSave = False
        End Try
    End Function
    Public Function fncSalesPaymodeDetailGSTSave(ByVal sInvoiceNo As String, ByVal sInvoiceDate As String, _
               ByVal sPaymode As String, ByVal nAmount As Decimal, ByVal nCommision As Decimal, _
               ByVal sSlipNo As String, ByVal sSalesType As String) As Boolean
        Dim sExec As String
        Dim bResult As Boolean
        Try
            sExec = "Exec [sp_PayModeDetailEntry] " & fncFormatSQLText(sInvoiceNo) & " , "
            sExec = sExec & fncFormatSQLDate(sInvoiceDate) & " , "
            sExec = sExec & fncFormatSQLText(sPaymode) & " , "
            sExec = sExec & Val(nAmount) & " , "
            sExec = sExec & Val(nCommision) & " , "
            sExec = sExec & fncFormatSQLText(sSlipNo) & " , "
            sExec = sExec & fncFormatSQLText(sSalesType) & " , "
            sExec = sExec & fncFormatSQLText(sUserID)
            bResult = fncExecuteNonQuery(sExec, gsConnection, gsTrans)
            fncSalesPaymodeDetailGSTSave = bResult
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            fncSalesPaymodeDetailGSTSave = False
        End Try
    End Function
    Public Function fncSalesPaymodeDetailAkshayaSave(ByVal sInvoiceNo As String, ByVal sInvoiceDate As String, _
                ByVal sPaymode As String, ByVal nAmount As Decimal, ByVal nCommision As Decimal, _
                ByVal sSlipNo As String, ByVal sSalesType As String, ByVal sLocationcode As String) As Boolean
        Dim sExec As String
        Dim bResult As Boolean
        Try
            sExec = "Exec sp_PayModeDetailAkshaya " & fncFormatSQLText(sInvoiceNo) & " , "
            sExec = sExec & fncFormatSQLDate(sInvoiceDate) & " , "
            sExec = sExec & fncFormatSQLText(sPaymode) & " , "
            sExec = sExec & Val(nAmount) & " , "
            sExec = sExec & Val(nCommision) & " , "
            sExec = sExec & fncFormatSQLText(sSlipNo) & " , "
            sExec = sExec & fncFormatSQLText(sSalesType) & " , "
            sExec = sExec & fncFormatSQLText(sLocationcode) & " , "
            sExec = sExec & fncFormatSQLText(sUserID)
            bResult = fncExecuteNonQuery(sExec, gsConnection, gsTrans)
            fncSalesPaymodeDetailAkshayaSave = bResult
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            fncSalesPaymodeDetailAkshayaSave = False
        End Try
    End Function
    Public Function fncSalesPaymodeChangeLogSave(ByVal sInvoiceNo As String, ByVal sInvoiceDate As String, _
        ByVal sPaymode As String, ByVal nAmount As Decimal) As Boolean
        Dim sExec As String
        Dim bResult As Boolean
        Try
            sExec = "Exec sp_PaymodeDetailChangeLogSave " & fncFormatSQLText(sInvoiceNo) & " , "
            sExec = sExec & fncFormatSQLDate(sInvoiceDate) & " , "
            sExec = sExec & fncFormatSQLText(sPaymode) & " , "
            sExec = sExec & Val(nAmount) & " , "
            sExec = sExec & fncFormatSQLText(gsUserId)
            bResult = fncExecuteNonQuery(sExec, gsConnection, gsTrans)
            fncSalesPaymodeChangeLogSave = bResult
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            fncSalesPaymodeChangeLogSave = False
        End Try
    End Function
    Public Function fncGetExpense(ByVal sConn As String, ByVal sDsName As String, ByVal sTerminalCode As String, _
                                   ByVal sDate As String) As DataSet
        Dim DS As New DataSet
        Dim sSql As String
        Dim sLocationCode As String
        Try
            sLocationCode = Trim(gthisLocation)
            If Trim(sLocationCode) = "99" Then
                sLocationCode = "103"
            ElseIf Trim(sLocationCode) = "27" Then
                sLocationCode = "1"
            End If
            sSql = "Select Amount = Sum(Amount),Paymode From tblpaymodedetailforflexi Where IsProcessed='Y' and LocationCode = " & fncFormatSQLText(sLocationCode)
            sSql = sSql & " And InvoiceDate = " & fncFormatSQLDate(sDate) & " group by Paymode "
            DS = fncGetDatasetWithQuery(sConn, sDsName, sSql)
            fncGetExpense = DS
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            fncGetExpense = Nothing
        Finally
            DS = Nothing
        End Try
    End Function
    Public Function fncUpdateDeposit(ByVal sDepositNo As String, sInvoiceNo As String, ByVal sInvoiceDate As String) As Boolean
        Dim bStatus As Boolean
        Dim sQry As String
        Try
            sQry = " update tblDeposit set DepositUsed='Y',SalesInvoiceNo = " & fncFormatSQLText(sInvoiceNo)
            sQry = sQry & " , SalesInvoiceDate = " & fncFormatSQLDate(sInvoiceDate) & " where SalesReturnNo=" & fncFormatSQLText(sDepositNo)
            bStatus = fncExecNonQrySales(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateDeposit = bStatus
            sQry = Nothing
        End Try
    End Function
    Public Function fncUpdateCostChangeLogSave(ByVal sInvoiceNo As String, ByVal sInvoiceDate As String, _
    ByVal sSalesmanName As String, ByVal sCostChangeSalesmanName As String) As Boolean
        Dim sExec As String
        Dim bResult As Boolean
        Try
            sExec = "Exec sp_CostChangeLogSave " & fncFormatSQLText(sInvoiceNo) & " , "
            sExec = sExec & fncFormatSQLDate(sInvoiceDate) & " , "
            sExec = sExec & fncFormatSQLText(sSalesmanName) & " , "
            sExec = sExec & fncFormatSQLText(sCostChangeSalesmanName) & " , "
            sExec = sExec & fncFormatSQLText(gsUserId)
            bResult = fncExecNonQrySales(sExec)
            fncUpdateCostChangeLogSave = bResult
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            fncUpdateCostChangeLogSave = False
        End Try
    End Function
    Public Function fncUpdateAdvDeposit(ByVal sDepositNo As String, sInvoiceNo As String, ByVal sInvoiceDate As String) As Boolean
        Dim bStatus As Boolean
        Dim sQry As String
        Try
            sQry = " update tblCustomerDeposit set DepositUsed='Y',InvoiceNo = " & fncFormatSQLText(sInvoiceNo)
            sQry = sQry & " , InvoiceDate = " & fncFormatSQLDate(sInvoiceDate) & ",ModifiedBy= " & fncFormatSQLText(sUserID) & ",ModifiedDate = " & fncFormatSQLDate(ServerDate) & " where DepositNo=" & fncFormatSQLText(sDepositNo)
            bStatus = fncExecNonQrySales(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateAdvDeposit = bStatus
            sQry = Nothing
        End Try
    End Function
    Public Function fncUpdatePayModeFlexiClosure(ByVal sSubscriptionID As String, ByVal sLocationCode As String, ByVal sUserId As String) As Boolean
        Dim bStatus As Boolean
        Dim sQry As String
        Try
            sQry = "Update tblPayModeDetailForFlexiClosure set IsProcessed='Y'  where SubscriptionID in( " & Trim(sSubscriptionID) & " )"
            sQry = sQry & " and LocationCode = " & fncFormatSQLText(sLocationCode)
            bStatus = fncExecNonQrySalesFlexi(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdatePayModeFlexiClosure = bStatus
            sQry = Nothing
        End Try
    End Function

    Public Function subGetSubcription(ByVal sSubscriptionId As String, ByVal sLocationCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select sum(TotalAmount)TotalAmount,sum(TotalBonus)TotalBonus from vwFlexiFinalClosure "
            sQry = sQry & " where IsProcessed='N' and SubscriptionID in( " & Trim(sSubscriptionId) & " )"
            sQry = sQry & " and LocationCode = " & fncFormatSQLText(sLocationCode)
            Ds = dm.getDataSetWithSqlAdapterAcc(sQry, "Class", "Class")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            subGetSubcription = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function eTRSStatementSave(ByVal sBillNo As String, ByVal sBillDate As String, ByVal sSalesManCode As String, ByVal sPassportNo As String, _
                                 ByVal dGst As Double, ByVal dNetAmount As Double, ByVal sDocId As String, ByVal sIssueDate As String, ByVal sIssueTime As String, _
                                 ByVal sRemarks As String, ByVal sIsSalesAbove7000 As String, ByVal sIsSealBag As String, ByVal sSealBagNo As String, _
                                 ByVal seTRSStaffName As String, ByVal sStaffSign As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_eTRSStatementSave " & fncFormatSQLText(sBillNo) & ","
            sQry = sQry & fncFormatSQLDate(sBillDate) & ","
            sQry = sQry & fncFormatSQLText(sSalesManCode) & ","
            sQry = sQry & fncFormatSQLText(sPassportNo) & ","
            sQry = sQry & Val(dGst) & ","
            sQry = sQry & Val(dNetAmount) & ","
            sQry = sQry & fncFormatSQLText(sDocId) & ","
            sQry = sQry & fncFormatSQLText(sIssueDate) & ","
            sQry = sQry & fncFormatSQLText(sIssueTime) & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & fncFormatSQLText(sIsSalesAbove7000) & ","
            sQry = sQry & fncFormatSQLText(sIsSealBag) & ","
            sQry = sQry & fncFormatSQLText(sSealBagNo) & ","
            sQry = sQry & fncFormatSQLText(seTRSStaffName) & ","
            sQry = sQry & fncFormatSQLText(sStaffSign) & ","
            sQry = sQry & fncFormatSQLText(sUserID)
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            eTRSStatementSave = SaveStatus
            sQry = Nothing
        End Try
    End Function
    Public Sub subOpenCashDrawer(ByVal sPrinter As String)
        Dim sWriteStr As String
        Dim pd As New PrintDocument
        Dim pc As PrintController
        Dim ReceiptFileName As String = "/Receipt.txt"
        Try
            sWriteStr = Chr(27) & Chr(112) & Chr(0) & Chr(25) & Chr(250)
            subWriteToFile(sWriteStr, ReceiptFileName)
            '  ClsReceiptToCustomer.SendFileToPrinter(sPrinter, Application.StartupPath & ReceiptFileName)
            subDeleteFile(ReceiptFileName)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub subOpenCashDrawerInUSB(ByVal iPort As Long)
        OpenUSBcr()
        DrawerOpen(iPort)
        'DrawerState(7)
        CloseUSBcr()
    End Sub



    Public Sub subDeleteFile(ByVal ReceiptFileName As String)
        Try
            File.Delete(Application.StartupPath & ReceiptFileName)
            Exit Sub
        Catch err As Exception
            MsgBox(err.Message)
        Finally
        End Try
    End Sub
    Public Sub subWriteToFile(ByVal sWriteText As String, ByVal ReceiptFileName As String)
        Dim fp As StreamWriter
        Try
            fp = File.CreateText(Application.StartupPath & "\" & ReceiptFileName)
            fp.WriteLine(sWriteText)
            fp.Close()
            Exit Sub
        Catch err As Exception
            MsgBox(err.Message)
        Finally
            fp = Nothing
        End Try
    End Sub

End Class
