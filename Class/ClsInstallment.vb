Public Class ClsInstallment
    Dim dm As New DataManager
    Public Function GetInventoryViewDetail(ByVal sInventoryCode As String) As DataSet
        Dim ds As New DataSet
        Dim sSql As String

        sSql = "SELECT *,Pdate = CONVERT(CHAR(8),PurchaseDate,112) FROM vwInventorystock WHERE InventoryCode =" & fncFormatSQLText(sInventoryCode)
        Try
            ds = dm.getDataSetWithSqlAdapter(sSql, "tblInventory", "tblInventory")
            GetInventoryViewDetail = ds
            Exit Function
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
            ds = Nothing
        End Try
    End Function
    Public Function GetUpdateDepositNo() As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " Select DepositNo=rtrim(locationcode) + '09'+ replicate('0',6-len(convert(char,NextPayOutNo)))+convert(char,NextPayOutNo) from tblcompany with (Nolock) where CompanyID = '1' "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Ins", "Ins")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetUpdateDepositNo = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetUpdateDepositPaymentNo() As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " Select DepositPaymentNo=rtrim(locationcode) + '04'+ replicate('0',6-len(convert(char,NextCustomerDepositNumber)))+convert(char,NextCustomerDepositNumber) from tblcompany with (Nolock) where CompanyID = '1' "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Ins", "Ins")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetUpdateDepositPaymentNo = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function DepositSave(ByVal sInvoiceNo As String, ByVal sInvoicedate As Date, _
                           ByVal sCustomerCode As String, ByVal sCustomerName As String, ByVal SalesmanCode As String, _
                           ByVal dDepositAmt As Double, ByVal dTotalAmount As Double, ByVal dAmountPaid As Double, _
                           ByVal sInventoryCode As String, ByVal sRemarks As String, ByVal sMode As String, ByVal sDepositPaymentNo As String, ByVal sInventoryCode1 As String, ByVal sInventoryCode2 As String, ByVal sPaymentDate As Date) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_DepositEntrySave "
            sQry = sQry & fncFormatSQLText(sInvoiceNo) & ", "
            sQry = sQry & fncFormatSQLDate(sInvoicedate) & ", "
            sQry = sQry & fncFormatSQLText(sCustomerCode) & ", "
            sQry = sQry & fncFormatSQLText(sCustomerName) & ", "
            sQry = sQry & fncFormatSQLText(SalesmanCode) & ", "
            sQry = sQry & Val(dDepositAmt) & ", "
            sQry = sQry & Val(dTotalAmount) & ", "
            sQry = sQry & Val(dAmountPaid) & ", "
            sQry = sQry & fncFormatSQLText(sInventoryCode) & ", "
            sQry = sQry & fncFormatSQLText(sRemarks) & ", "
            sQry = sQry & fncFormatSQLText(sMode) & ", "
            sQry = sQry & fncFormatSQLText(sDepositPaymentNo) & ", "
            sQry = sQry & fncFormatSQLText(sInventoryCode1) & ", "
            sQry = sQry & fncFormatSQLText(sInventoryCode2) & ", "
            sQry = sQry & fncFormatSQLDate(sPaymentDate) & ", "
            sQry = sQry & fncFormatSQLText(sUserID)
            SaveStatus = fncExecNonQryIns(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            DepositSave = SaveStatus
            sQry = Nothing
        End Try
    End Function
    Public Function DepositSaveJP(ByVal sInvoiceNo As String, ByVal sInvoicedate As Date, _
                       ByVal sCustomerCode As String, ByVal SalesmanCode As String, _
                       ByVal dDepositAmt As Double, _
                       ByVal sRemarks As String, ByVal sSupplierCode As String, ByVal sEstiDate As Date, _
                       ByVal sSupplierDate As Date, ByVal sStatus As String, ByVal sPayStatus As String, ByVal sMode As String, ByVal sOrderNo As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_DepositEntrySaveJP "
            sQry = sQry & fncFormatSQLText(sInvoiceNo) & ", "
            sQry = sQry & fncFormatSQLDate(sInvoicedate) & ", "
            sQry = sQry & fncFormatSQLText(sCustomerCode) & ", "
            sQry = sQry & fncFormatSQLText(SalesmanCode) & ", "
            sQry = sQry & Val(dDepositAmt) & ", "
            sQry = sQry & fncFormatSQLText(sRemarks) & ", "
            sQry = sQry & fncFormatSQLText(sSupplierCode) & ", "
            sQry = sQry & fncFormatSQLDate(sEstiDate) & ", "
            sQry = sQry & fncFormatSQLDate(sSupplierDate) & ", "
            sQry = sQry & fncFormatSQLText(sStatus) & ", "
            sQry = sQry & fncFormatSQLText(sPayStatus) & ", "
            sQry = sQry & fncFormatSQLText(sMode) & ", "
            sQry = sQry & fncFormatSQLText(sOrderNo) & ", "
            sQry = sQry & fncFormatSQLText(sUserID)
            SaveStatus = fncExecNonQryIns(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            DepositSaveJP = SaveStatus
            sQry = Nothing
        End Try
    End Function
    Public Function GetUpdateInsAgreement() As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " Select NextInsAgreementNo=rtrim(locationcode) + '03'+ replicate('0',6-len(convert(char,NextInsAgreementNo)))+convert(char,NextInsAgreementNo) from tblcompany with (Nolock) where CompanyID = '1' "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Ins", "Ins")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetUpdateInsAgreement = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function fncUpdateInsAgreement() As Boolean
        Dim bStatus As Boolean
        Dim sQry As String
        Try
            sQry = "update tblcompany set NextInsAgreementNo = NextInsAgreementNo + 1 Where CompanyId = '1'"
            bStatus = fncExecNonQryIns(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateInsAgreement = bStatus
            sQry = Nothing
        End Try
    End Function

    Public Function InstallmentDelete(ByVal sInsAgreementNo As String) As Boolean
        Dim sExec As String
        Dim SaveStatus As Boolean
        'Try
        '    sQry = "Delete from tblInstallmentHeader WHERE InsAgreementNo = " & fncFormatSQLText(sInsAgreementNo)
        '    SaveStatus = fncExecNonQryIns(sQry)
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        '    SaveStatus = False
        'Finally
        '    Dim sExec As String
        Try
            SaveStatus = False

            sExec = "INSERT into tblInstallmentEntryHeaderDelete" & vbCrLf
            sExec = sExec & "Select * FROM tblInstallmentEntryHeader  WHERE InsAgreementNo =" & fncFormatSQLText(sInsAgreementNo)
            dm.ProjExecNonQry(sExec)

            sExec = "INSERT into tblInstallmentEntryDetailDelete" & vbCrLf
            sExec = sExec & "Select * FROM tblInstallmentEntryDetail  WHERE InsAgreementNo =" & fncFormatSQLText(sInsAgreementNo)
            dm.ProjExecNonQry(sExec)

            sExec = "DELETE FROM tblInstallmentEntryDetail WHERE InsAgreementNo =" & fncFormatSQLText(sInsAgreementNo) & vbCrLf
            sExec = sExec & "DELETE FROM tblInstallmentEntryHeader  WHERE InsAgreementNo =" & fncFormatSQLText(sInsAgreementNo)
            dm.ProjExecNonQry(sExec)
            SaveStatus = True
            Exit Function
        Catch ex As Exception
            SaveStatus = False
            MsgBox(Err.Description)
        Finally
            Err.Clear()
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
            SaveStatus = fncExecNonQryIns(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            InstallmentHeaderSave = SaveStatus
            sQry = Nothing
        End Try
    End Function
    Public Function InstallmentEntryHeaderSave(ByVal sInvoiceNo As String, ByVal sInvoicedate As Date, ByVal sGSTType As String, ByVal sLocationCode As String, _
                           ByVal sCustomerCode As String, ByVal sMemberCode As String, ByVal sCustomerName As String, _
                           ByVal Address1 As String, ByVal Address2 As String, _
                           ByVal Address3 As String, ByVal SalesmanCode As String, _
                           ByVal dDepositAmt As Double, ByVal nTotalInstallment As Integer, ByVal nPaidInstallment As Integer, ByVal dInstallmentAmt As Double, _
                           ByVal dAmtpaid As Double, ByVal sLastdate As Date, _
                           ByVal sRemarks As String, ByVal dTotalCost As Double, ByVal dTradeInAmt As Double, ByVal dTradeInGST As Double) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_InstallmentEntryHeader "
            sQry = sQry & fncFormatSQLText(sInvoiceNo) & ", "
            sQry = sQry & fncFormatSQLDate(sInvoicedate) & ", "
            sQry = sQry & fncFormatSQLDate(sGSTType) & ", "
            sQry = sQry & fncFormatSQLText(sLocationCode) & ", "
            sQry = sQry & fncFormatSQLText(sCustomerCode) & ", "
            sQry = sQry & fncFormatSQLText(sCustomerName) & ", "
            sQry = sQry & fncFormatSQLText(Address1) & ", "
            sQry = sQry & fncFormatSQLText(Address2) & ", "
            sQry = sQry & fncFormatSQLText(Address3) & ", "
            sQry = sQry & fncFormatSQLText(SalesmanCode) & ", "
            sQry = sQry & Val(dDepositAmt) & ", "
            sQry = sQry & Val(nTotalInstallment) & ", "
            sQry = sQry & Val(nPaidInstallment) & ", "
            sQry = sQry & Val(dInstallmentAmt) & ", "
            sQry = sQry & fncFormatSQLText(sRemarks) & ", "
            sQry = sQry & fncFormatSQLDate(sLastdate) & ", "
            sQry = sQry & fncFormatSQLText(sUserID) & ", "
            sQry = sQry & fncFormatSQLText(sMemberCode) & ", "
            sQry = sQry & Val(dTotalCost) & ", "
            sQry = sQry & Val(dTradeInAmt) & ", "
            sQry = sQry & Val(dTradeInGST) 
            SaveStatus = fncExecNonQryIns(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            InstallmentEntryHeaderSave = SaveStatus
            sQry = Nothing
        End Try
    End Function
    Public Function InstallmentEntryDetailSave(ByVal sLocationCode As String, ByVal sInsAgreementNo As String, ByVal SInvCode As String, ByVal sDescription As String, ByVal dWeight As Double, ByVal dGoldLoss As Double, _
                           ByVal dCost As Double, ByVal dPCS As Double, ByVal dSMC As Double, _
                           ByVal dGST As Double, ByVal dTotalValue As Double, ByVal dDetailDiscount As Double, ByVal sUserid As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_InstallmentEntryDetail "
            sQry = sQry & fncFormatSQLText(sLocationCode) & ", "
            sQry = sQry & fncFormatSQLText(sInsAgreementNo) & ", "
            sQry = sQry & fncFormatSQLText(SInvCode) & ", "
            sQry = sQry & fncFormatSQLText(sDescription) & ", "
            sQry = sQry & Val(dWeight) & ", "
            sQry = sQry & Val(dGoldLoss) & ", "
            sQry = sQry & Val(dCost) & ", "
            sQry = sQry & Val(dPCS) & ", "
            sQry = sQry & Val(dSMC) & ", "
            sQry = sQry & Val(dGST) & ", "
            sQry = sQry & Val(dTotalValue) & ", "
            sQry = sQry & Val(dDetailDiscount) & ", "
            sQry = sQry & fncFormatSQLText(sUserid)
            SaveStatus = fncExecNonQryIns(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            InstallmentEntryDetailSave = SaveStatus
            sQry = Nothing
        End Try
    End Function

#Region "InstallmentPay"

    Public Function GetInstallmentHdr(ByVal sInsAgreement As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            ''    sQry = "SELECT * FROM tblInstallmentEntryHeader where InsAgreementNo = " & fncFormatSQLText(sInsAgreement)
            sQry = "SELECT * FROM tblInstallmentEntryHeader where InsAgreementNo = " & fncFormatSQLText(sInsAgreement)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Ins", "Ins")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetInstallmentHdr = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetInstallmentBalance(ByVal sInsAgreement As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            ''    sQry = "SELECT * FROM tblInstallmentEntryHeader where InsAgreementNo = " & fncFormatSQLText(sInsAgreement)
            sQry = "SELECT sum(amtpaid)as AmountPaid FROM tblinstallmentdetail where insagreementno = " & fncFormatSQLText(sInsAgreement)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Ins", "Ins")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetInstallmentBalance = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function GetInstallmentDetail(ByVal sInsAgreement As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "SELECT * FROM tblInstallmentDetail where InsAgreementNo = " & fncFormatSQLText(sInsAgreement)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Ins", "Ins")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetInstallmentDetail = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function OpenConnection()
        gsConIns.ConnectionString = gsCon
        gsConIns.Open()
    End Function

    Public Function BeginTransaction()
        gsTrans = gsConIns.BeginTransaction
    End Function

    Public Function CommitTransaction()
        gsTrans.Commit()
    End Function

    Public Function RollBackTransaction()
        gsTrans.Rollback()
    End Function

    Public Function CloseConnection()
        If gsConIns.State = ConnectionState.Open Then gsConIns.Close()
    End Function

    Public Function GetUpdateInstallmentNo() As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "EXEC sp_GetUpdateInstallmentNo"
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Ins", "Ins")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetUpdateInstallmentNo = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function GetSchemeNo() As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "EXEC sp_GetUpdateSchemeNo"
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Ins", "Ins")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetSchemeNo = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function InstallmentDetDelete(ByVal sInsAgreementNo As String, ByVal sInstallmentNo As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "Delete from tblInstallmentDetail WHERE InsAgreementNo = " & fncFormatSQLText(sInsAgreementNo) & _
                " And InstallmentNo = " & fncFormatSQLText(sInstallmentNo)
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            InstallmentDetDelete = SaveStatus
            sQry = Nothing
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
            SaveStatus = dm.ProjExecNonQryAcc(sExec)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            PosMemberTransationSave = SaveStatus
            sExec = Nothing
        End Try
    End Function
    Public Function fncUpdatePayModeFlexiClosure(ByVal sSubscriptionID As String, ByVal sLocationCode As String, ByVal sUserId As String) As Boolean
        Dim bStatus As Boolean
        Dim sQry As String
        Try
            'sQry = "Update tblPayModeDetailForFlexiClosure set IsProcessed='Y'  where SubscriptionID=" & fncFormatSQLText(sSubscriptionID)
            'sQry = sQry & " and LocationCode = " & fncFormatSQLText(sLocationCode)
            sQry = "Update tblPayModeDetailForFlexiClosure set IsProcessed='Y'  where SubscriptionID in( " & Trim(sSubscriptionID) & " )"
            sQry = sQry & " and LocationCode = " & fncFormatSQLText(sLocationCode)
            bStatus = dm.ProjExecNonQryFlexi(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdatePayModeFlexiClosure = bStatus
            sQry = Nothing
        End Try
    End Function

    Public Function UpdateDetailsintoSales(ByVal sInvoiceNo As String, ByVal sInvoiceDate As String, ByVal msInstallmentNo As String, ByVal sCustomerCode As String, ByVal sName As String, ByVal sAddress1 As String, _
                                           ByVal sAddress2 As String, ByVal sAddress3 As String, ByVal sSalesManCode As String, ByVal sSalesType As String, ByVal dTotalWeight As Double, _
                                           ByVal dTotalStoneWeight As Double, ByVal dTotalGoldLoss As Double, ByVal dTotalSMC As Double, ByVal sGstType As String, ByVal dGST As Double, _
                                           ByVal dDetailDiscont As Double, ByVal dDiscount As Double, ByVal dTotalCost As Double, ByVal dTotalPCS As Double, _
                                           ByVal dTradeInAmt As Double, ByVal dTradeInGST As Double, ByVal sJournalNo As String, ByVal sUserid As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_SaveInstallmentEntrySales "
            sQry = sQry & fncFormatSQLText(sInvoiceNo) & ", "
            sQry = sQry & fncFormatSQLDate(sInvoiceDate) & ", "
            sQry = sQry & fncFormatSQLText(sCompanyID) & ", "
            sQry = sQry & fncFormatSQLText(gthisLocation) & ", "
            sQry = sQry & fncFormatSQLText(sTerminalID) & ", "
            sQry = sQry & fncFormatSQLText(msInstallmentNo) & ", "
            sQry = sQry & fncFormatSQLText(sCustomerCode) & ", "
            sQry = sQry & fncFormatSQLText(sName) & ", "
            sQry = sQry & fncFormatSQLText(sAddress1) & ", "
            sQry = sQry & fncFormatSQLText(sAddress2) & ", "
            sQry = sQry & fncFormatSQLText(sAddress3) & ", "
            sQry = sQry & fncFormatSQLText(sSalesManCode) & ", "
            sQry = sQry & fncFormatSQLText(sSalesType) & ", "
            sQry = sQry & Val(dTotalWeight) & ", "
            sQry = sQry & Val(dTotalStoneWeight) & ", "
            sQry = sQry & Val(dTotalGoldLoss) & ", "
            sQry = sQry & Val(dTotalSMC) & ", "
            sQry = sQry & fncFormatSQLText(sGstType) & ", "
            sQry = sQry & Val(dGST) & ", "
            sQry = sQry & Val(dDetailDiscont) & ", "
            sQry = sQry & Val(dDiscount) & ", "
            sQry = sQry & Val(dTotalCost) & ", "
            sQry = sQry & Val(dTotalPCS) & ", "
            sQry = sQry & Val(dTradeInAmt) & ", "
            sQry = sQry & Val(dTradeInGST) & ", "
            sQry = sQry & fncFormatSQLText(sJournalNo) & ", "
            sQry = sQry & fncFormatSQLText(sUserid)
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            UpdateDetailsintoSales = SaveStatus
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
                                    ByVal dCommission As Double, ByVal sRemarks As String, ByVal dDailyClassRate As Double, _
                                    ByVal iSortCode As Integer, ByVal dTradeinGST As Double, ByVal sSalesType As String) As Boolean
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
            sQry = sQry & Val(dTradeinGST) & ","
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

    Public Function InstallmentHeaderUpdate(ByVal sInsAgreementNo As String, ByVal nInsPaid As Integer, ByVal dInsAmt As Double, _
                            ByVal dInsAmtPaid As Double, ByVal dTradeInSales As Double, ByVal dTradeinGST As Double) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "Update tblInstallmentEntryHeader Set "
            sQry = sQry & " InsPaid = InsPaid + " & Val(nInsPaid) & ", "
            sQry = sQry & " AmtPaid = AmtPaid + " & Val(dInsAmtPaid) & ", "
            sQry = sQry & " TradeinAmt = TradeinAmt + " & Val(dTradeInSales) & ", "
            sQry = sQry & " TradeinGST = TradeinGST + " & Val(dTradeinGST) & ", "
            sQry = sQry & " Modifydate = getdate() "
            sQry = sQry & " Where InsAgreementNo = " & fncFormatSQLText(sInsAgreementNo)
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            InstallmentHeaderUpdate = SaveStatus
            sQry = Nothing
        End Try
    End Function

    Public Function InstallmentDetailSave(ByVal sInsAgreementNo As String, ByVal sInstallmentNo As String, ByVal nInsPaid As Integer, _
                            ByVal dInsAmt As Double, ByVal dInsAmtPaid As Double, ByVal sRemarks As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_InstallmentDetail "
            sQry = sQry & fncFormatSQLText(sInsAgreementNo) & ", "
            sQry = sQry & fncFormatSQLText(sInstallmentNo) & ", "
            sQry = sQry & Val(nInsPaid) & ", "
            sQry = sQry & Val(dInsAmt) & ", "
            sQry = sQry & Val(dInsAmtPaid) & ", "
            sQry = sQry & fncFormatSQLText(sRemarks) & ", "
            sQry = sQry & fncFormatSQLText(sUserID)

            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            InstallmentDetailSave = SaveStatus
            sQry = Nothing
        End Try
    End Function

    Public Function GetInsDetlByInsNo(ByVal sInstallmentNo As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "SELECT *, BDate = Convert(char(8), CreateDate, 112) FROM tblInstallmentDetail WHERE InstallmentNo =" & fncFormatSQLText(sInstallmentNo)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Ins", "Ins")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetInsDetlByInsNo = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function InsBillCancel(ByVal sInstallmentNo As String, ByVal sLocation As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = sQry & "EXEC sp_InstallmentBillCancel " & fncFormatSQLText(sInstallmentNo) & ","
            sQry = sQry & fncFormatSQLText(sLocation) & ","
            sQry = sQry & fncFormatSQLText(sUserID) & ""
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            InsBillCancel = SaveStatus
            sQry = Nothing
        End Try
    End Function

    Public Function InstallmentInventoryStatusUpdate(ByVal sInsAgreementNo As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "update tblinventoryStock set Status='',modifydate=getdate() from tblinventoryStock a, "
            sQry = sQry & " tblinstallmententrydetail b where a.Inventorycode=b.Inventorycode"
            sQry = sQry & " and InsAgreementno=" & fncFormatSQLText(sInsAgreementNo) & " and a.locationcode=b.Locationcode"
            SaveStatus = dm.ProjExecNonQry(sQry)

            sQry = "Delete from [tblInstallmentEntryDetail] where InsAgreementNo=" & fncFormatSQLText(sInsAgreementNo)
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            InstallmentInventoryStatusUpdate = SaveStatus
            sQry = Nothing
        End Try
    End Function


    Public Function InstallmentStockStatusUpdate(ByVal sInventoryCode As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "update tblinventoryStock set Status='',modifydate=getdate() where InventoryCode " & fncFormatSQLText(sInventoryCode)
            SaveStatus = dm.ProjExecNonQry(sQry)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            InstallmentStockStatusUpdate = SaveStatus
            sQry = Nothing
        End Try
    End Function
#End Region

#Region "Transaction Info"

    Public Sub subOpenInsConnection()
        gsInsCon.ConnectionString = gsCon
        gsInsCon.Open()
    End Sub

    Public Sub subBeginInsTransaction()
        gsInsTrans = gsInsCon.BeginTransaction
    End Sub

    Public Sub subCommitInsTransaction()
        gsInsTrans.Commit()
    End Sub

    Public Sub subRollBackInsTransaction()
        gsInsTrans.Rollback()
    End Sub

    Public Sub subCloseInsConnection()
        gsInsCon.Close()
    End Sub

    Public Function fncExecNonQryIns(ByVal Query As String) As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            Dim Cmd As New SqlClient.SqlCommand(Query, gsInsCon)
            Cmd.Transaction = gsInsTrans
            Cmd.ExecuteNonQuery()
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncExecNonQryIns = bStatus
        End Try
    End Function

#End Region

End Class
