Public Class clsRepair
    Dim dm As New DataManager
    Public Function GetNextOrderNo() As String
        Dim Ds As New DataSet
        Dim sQry As String
        Dim sNextOrderNo As String
        Try
            sQry = " Select rtrim(LocationCode)+Replicate('0',8-Len(Convert(char,NextRepairEntryNo)))+ convert(char,NextRepairEntryNo) NextOrderEntryNo from tblCompany "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "ONo", "ONo")
            If Ds.Tables("ONo").Rows.Count > 0 Then
                sNextOrderNo = Ds.Tables("ONo").Rows(0)("NextOrderEntryNo")
            End If
            'sQry = "Update   tblCompany Set NextRepairEntryNo = NextRepairEntryNo + 1 "
            dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            sNextOrderNo = ""
        Finally
            GetNextOrderNo = sNextOrderNo
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
#Region " Order Save"
    Public Function FncRepairHeaderSave(ByVal sOrderNo As String, ByVal sOrderDate As String, _
                                    ByVal sCustomerCode As String, ByVal sSuppliercode As String, _
                                    ByVal sDeliveryDate As String, ByVal dCostPerGram As Decimal, _
                                    ByVal sTradeInNo As String, ByVal sTradeInType As String, _
                                    ByVal sRateFixed As String, ByVal sTotalCostEstimated As Decimal, _
                                    ByVal dAdvancePaid As Decimal, ByVal sStatus As String, _
                                    ByVal sRemarks As String, ByVal sAdminUserId As String, _
                                    ByVal sInvType As String, ByVal dSupplierCost As Decimal, ByVal sIssueDate As String) As Boolean
        Dim sQry As String
        Try
            sQry = sQry & "EXEC sp_InventoryRepairHeader_save " & fncFormatSQLText(sOrderNo) & ","
            sQry = sQry & fncFormatSQLDate(sOrderDate) & ","
            sQry = sQry & fncFormatSQLText(sCustomerCode) & ","
            sQry = sQry & fncFormatSQLText(sSuppliercode) & ","
            sQry = sQry & fncFormatSQLDate(sDeliveryDate) & ","
            sQry = sQry & Val(dCostPerGram) & ","
            sQry = sQry & fncFormatSQLText(sTradeInNo) & ","
            sQry = sQry & fncFormatSQLText(sTradeInType) & ","
            sQry = sQry & fncFormatSQLText(sRateFixed) & ","
            sQry = sQry & Val(sTotalCostEstimated) & ","
            sQry = sQry & Val(dAdvancePaid) & ","
            sQry = sQry & fncFormatSQLText(sStatus) & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & fncFormatSQLText(gthisLocation) & ","
            sQry = sQry & fncFormatSQLText(sAdminUserId) & ","
            sQry = sQry & fncFormatSQLText(sInvType) & ","
            sQry = sQry & Val(dSupplierCost) & ","
            sQry = sQry & fncFormatSQLText(sUserID) & ","
            sQry = sQry & fncFormatSQLDate(sIssueDate)

            FncRepairHeaderSave = dm.ProjExecNonQry(sQry)

        Catch ex As Exception
            MsgBox(Err.Description)
            FncRepairHeaderSave = False
        Finally
            Err.Clear()
            sQry = Nothing
        End Try
    End Function
    Public Function FncRepairDetailSave(ByVal sOrderNo As String, ByVal sDesc As String, _
                                    ByVal dWeightEstimated As Decimal, _
                                    ByVal dEstimatedCost As Decimal, ByVal sStatus As String) As Boolean
        Dim sQry As String
        Try
            sQry = sQry & "EXEC sp_InventoryRepairDetail_save " & fncFormatSQLText(sOrderNo) & ","
            sQry = sQry & fncFormatSQLText(sDesc) & ","
            sQry = sQry & Val(dWeightEstimated) & ","
            sQry = sQry & Val(dEstimatedCost) & ","
            sQry = sQry & fncFormatSQLText(sStatus) & ","
            sQry = sQry & fncFormatSQLText(sUserID)

            FncRepairDetailSave = dm.ProjExecNonQry(sQry)

        Catch ex As Exception
            MsgBox(Err.Description)
            FncRepairDetailSave = False
        Finally
            Err.Clear()
            sQry = Nothing
        End Try
    End Function

    Public Function FncRepairPaymentSave(ByVal sOrderNo As String, ByVal sOrderPaymentNo As String, _
                                    ByVal dAmountPaid As Decimal, ByVal sRemarks As String) As Boolean
        Dim sQry As String
        Try
            sQry = sQry & "EXEC sp_OrderPaymentDetailSave " & fncFormatSQLText(sOrderNo) & ","
            sQry = sQry & fncFormatSQLText(sOrderPaymentNo) & ","
            sQry = sQry & Val(dAmountPaid) & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & fncFormatSQLText(sUserID)

            FncRepairPaymentSave = dm.ProjExecNonQry(sQry)

        Catch ex As Exception
            MsgBox(Err.Description)
            FncRepairPaymentSave = False
        Finally
            Err.Clear()
            sQry = Nothing
        End Try
    End Function

    Public Function fncRepairPaymentDetailDelete(ByVal sRepairNo As String, ByVal sRepairPaymentNo As String, ByVal dAmount As Double) As Boolean
        Dim sExec As String
        Try
            fncRepairPaymentDetailDelete = False
            sExec = "DELETE FROM tblRepairPaymentDetail WHERE Repairno =" & fncFormatSQLText(sRepairNo)
            If sRepairPaymentNo.Trim <> "" Then sExec = sExec & " and RepairPaymentNo = " & fncFormatSQLText(sRepairPaymentNo)
            dm.ProjExecNonQry(sExec)
            sExec = "Update tblInventoryRepairheader set AdvancePaid=AdvancePaid - " & dAmount & "Where RepairNo = " & fncFormatSQLText(sRepairNo)
            dm.ProjExecNonQry(sExec)
            fncRepairPaymentDetailDelete = True
        Catch ex As Exception
            fncRepairPaymentDetailDelete = False
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Function
#End Region
#Region "Connection Info"

    Public Function OpenRepairConnection()
        gsConRepair.ConnectionString = gsCon
        gsConRepair.Open()
    End Function

    Public Function gpBeginRepairTransaction()
        gsTrans = gsConRepair.BeginTransaction
    End Function

    Public Function gpCommitRepairTransaction()
        gsTrans.Commit()
    End Function

    Public Function gpRollBackRepairConnection()
        gsTrans.Rollback()
    End Function

    Public Function CloseRepairConnection()
        gsConRepair.Close()
    End Function

#End Region

    Public Function GetOrderDetail(ByVal sOrderNo As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " Select * from tblInventoryRepairDetail  where RepairNo = " & fncFormatSQLText(sOrderNo)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "tblInventoryOrderDetail", "tblInventoryOrderDetail")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetOrderDetail = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function GetOrderHeader(ByVal sOrderNo As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " Select * from tblInventoryRepairHeader  where RepairNo = " & fncFormatSQLText(sOrderNo)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "tblInventoryOrderHeader", "tblInventoryOrderHeader")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetOrderHeader = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function GetOrderPaymentDetail(ByVal sOrderNo As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select * from tblOrderPaymentDetail  where orderNo = " & fncFormatSQLText(sOrderNo)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "OP", "OP")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetOrderPaymentDetail = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function fncGetNextOrderPaymentNo() As String
        Dim Ds As New DataSet
        Dim sQry As String
        Dim sNextOrderPaymentNo As String
        Try
            sQry = " Select Replicate('0',8-Len(Convert(char,NextOrderPaymentNo)))+ convert(char,NextOrderPaymentNo) NextOrderPaymentNo from tblCompany "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "OPN", "OPN")
            If Ds.Tables("OPN").Rows.Count > 0 Then
                sNextOrderPaymentNo = Ds.Tables("OPN").Rows(0)("NextOrderPaymentNo")
            End If
            sQry = "Update   tblCompany Set NextOrderPaymentNo = NextOrderPaymentNo + 1 "
            dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            sNextOrderPaymentNo = ""
        Finally
            fncGetNextOrderPaymentNo = sNextOrderPaymentNo
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function FncOrderPaymentSave(ByVal sOrderNo As String, ByVal sOrderPaymentNo As String, _
                              ByVal dAmountPaid As Decimal, ByVal sRemarks As String, ByVal dTradeInSales As Double, ByVal dTradeinGST As Double) As Boolean
        Dim sQry As String
        Try
            sQry = sQry & "EXEC sp_RepairPaymentDetailSave " & fncFormatSQLText(sOrderNo) & ","
            sQry = sQry & fncFormatSQLText(sOrderPaymentNo) & ","
            sQry = sQry & Val(dAmountPaid) & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & fncFormatSQLText(sUserID) & ","
            sQry = sQry & Val(dTradeInSales) & ","
            sQry = sQry & Val(dTradeinGST)

            FncOrderPaymentSave = dm.ProjExecNonQry(sQry)

        Catch ex As Exception
            MsgBox(Err.Description)
            FncOrderPaymentSave = False
        Finally
            Err.Clear()
            sQry = Nothing
        End Try
    End Function

    Public Function GetInstallmentBalance(ByVal sInsAgreement As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            ''    sQry = "SELECT * FROM tblInstallmentEntryHeader where InsAgreementNo = " & fncFormatSQLText(sInsAgreement)
            sQry = "SELECT sum(Advancepaid)as AmountPaid FROM tblInventoryRepairHeader where RepairNo = " & fncFormatSQLText(sInsAgreement)
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
    Public Function UpdateDetailsintoSales(ByVal sInvoiceNo As String, ByVal sInvoiceDate As String, ByVal msInstallmentNo As String, ByVal sCustomerCode As String, ByVal sName As String, ByVal sAddress1 As String, _
                                         ByVal sAddress2 As String, ByVal sAddress3 As String, ByVal sSalesManCode As String, ByVal sSalesType As String, ByVal dTotalWeight As Double, _
                                         ByVal dTotalStoneWeight As Double, ByVal dTotalGoldLoss As Double, ByVal dTotalSMC As Double, ByVal sGstType As String, ByVal dGST As Double, _
                                         ByVal dDetailDiscont As Double, ByVal dDiscount As Double, ByVal dTotalCost As Double, ByVal dTotalPCS As Double, ByVal sUserid As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_SaveInstallmentEntrySales "
            sQry = sQry & fncFormatSQLText(sInvoiceNo) & ", "
            sQry = sQry & fncFormatSQLDate(sInvoiceDate) & ", "
            sQry = sQry & fncFormatSQLText(sCompanyID) & ", "
            sQry = sQry & fncFormatSQLText(sLocationCode) & ", "
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
            sQry = sQry & fncFormatSQLText(sUserID)
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
End Class
