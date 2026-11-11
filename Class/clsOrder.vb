Public Class clsOrder
    Dim dm As New DataManager

#Region "Fetch Class"

    Public Function GetClass(ByVal sClasscode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select * from tblClass  where classcode = " & fncFormatSQLText(sClasscode)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "tblClass", "tblClass")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetClass = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

#End Region

#Region "Fetch Order Entry Detail"

    Public Function GetNextOrderNo() As String
        Dim Ds As New DataSet
        Dim sQry As String
        Dim sNextOrderNo As String
        Try
            sQry = " Select rtrim(locationcode) + Replicate('0',8-Len(Convert(char,NextOrderEntryNo)))+ convert(char,NextOrderEntryNo) NextOrderEntryNo from tblCompany "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "ONo", "ONo")
            If Ds.Tables("ONo").Rows.Count > 0 Then
                sNextOrderNo = Ds.Tables("ONo").Rows(0)("NextOrderEntryNo")
            End If
            sQry = "Update   tblCompany Set NextOrderEntryNo = NextOrderEntryNo + 1 "
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

    Public Function GetOrderDetail(ByVal sOrderNo As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " Select * from tblInventoryOrderDetail  where orderNo = " & fncFormatSQLText(sOrderNo)
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
            sQry = " Select * from tblInventoryOrderHeader  where orderNo = " & fncFormatSQLText(sOrderNo)
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

    Public Function GetOrderPaymentAmount(ByVal sOrderNo As String) As Decimal
        Dim Ds As New DataSet
        Dim sQry As String
        Dim dAmount As Decimal
        Try
            sQry = " select Sum(Amount) Amount From tblPaymodeDetail Where InvoiceNo = " & fncFormatSQLText(sOrderNo) & " Group By InvoiceNo "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "OPA", "OPA")
            If Ds.Tables("OPA").Rows.Count > 0 Then
                dAmount = Val(Ds.Tables("OPA").Rows(0)("Amount"))
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetOrderPaymentAmount = dAmount
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    'Public Function GetOrderDetailWithInventoryCode(ByVal sOrderNo As String, ByVal sInventoryCode As String) As DataSet
    '    Dim Ds As New DataSet
    '    Dim sQry As String
    '    Try
    '        sQry = " Select * from tblInventoryOrderHeader  where orderNo = " & fncFormatSQLText(sOrderNo) & " and InventoryCode = " & fncFormatSQLText(sInventoryCode)
    '        Ds = dm.getDataSetWithSqlAdapter(sQry, "tblInventoryOrderDetail", "tblInventoryOrderDetail")
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '        Ds = Nothing
    '    Finally
    '        GetOrderDetailWithInventoryCode = Ds
    '        Ds = Nothing
    '        sQry = Nothing
    '    End Try
    'End Function

    Public Function GetInventoryOrderHeader(ByVal sOrderNo As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select * from tblInventoryOrderHeader  where orderNo = " & fncFormatSQLText(sOrderNo)
            sQry = sQry & " select Sum(AmountPaid) AmountPaid from tblOrderPaymentDetail Where OrderNo = " & fncFormatSQLText(sOrderNo) & " group by OrderNo"
            Ds = dm.getDataSetWithSqlAdapter(sQry, "IOHeader", "IOHeader")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetInventoryOrderHeader = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function GetInventoryOrderDetail(ByVal sOrderNo As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select * from tblInventoryOrderDetail  where orderNo = " & fncFormatSQLText(sOrderNo)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "IODetail", "IODetail")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetInventoryOrderDetail = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function GetInstallmentStatus(ByVal sInsagreementNo As String) As Boolean
        Dim Ds As New DataSet
        Dim sQry As String
        Dim sStatus As Boolean
        Try
            sStatus = True
            sQry = " select * from tblInstallmentEntryHeader  where InsagreementNo = " & fncFormatSQLText(sInsagreementNo)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "OrderStatus", "OrderStatus")
            If Ds.Tables("OrderStatus").Rows.Count > 0 Then
                sStatus = True
            Else
                sStatus = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetInstallmentStatus = sStatus
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function GetOrderStatus(ByVal sOrderNo As String) As String
        Dim Ds As New DataSet
        Dim sQry As String
        Dim sStatus As String
        Try
            sQry = " select Status from tblInventoryOrderHeader  where orderNo = " & fncFormatSQLText(sOrderNo)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "OrderStatus", "OrderStatus")
            If Ds.Tables("OrderStatus").Rows.Count > 0 Then
                sStatus = Trim(Ds.Tables("OrderStatus").Rows(0)("Status") & "")
            Else
                sStatus = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetOrderStatus = sStatus
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
            'sQry = "Update   tblCompany Set NextOrderPaymentNo = NextOrderPaymentNo + 1 "
            'dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            sNextOrderPaymentNo = ""
        Finally
            fncGetNextOrderPaymentNo = sNextOrderPaymentNo
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function fncUpdateOrderInventory(ByVal sOrderInventoryCode As String, ByVal sInventoryCode As String, ByVal sWeight As Double, ByVal sUserId As String, ByVal sOrderNo As String) As Boolean
        Dim sQry As String
        Dim bStatus As Boolean
        Try
            sQry = "Update tblinventoryorderdetail Set InventoryCode =  " & fncFormatSQLText(sInventoryCode) & ",WeightEstimated = " & Val(sWeight) & ", Modifieduser = " & fncFormatSQLText(sUserId) & ", Modifieddate = GetDate()  Where OrderInventoryCode = " & fncFormatSQLText(sOrderInventoryCode) & " and OrderNo = " & fncFormatSQLText(sOrderNo)
            bStatus = dm.ProjExecNonQry(sQry)
            sQry = "Update tblinventoryorderdetail Set EstimatedCost = Round(((WeightEstimated * CostPerGram) + MC + ((WeightEstimated * CostPerGram) + MC) * 7 / 100),0) ,EstimatedGSt=((WeightEstimated*CostPerGram)+MC)*7/100 from tblinventoryorderdetail Where OrderInventoryCode = " & fncFormatSQLText(sOrderInventoryCode) & " and OrderNo = " & fncFormatSQLText(sOrderNo)
            bStatus = dm.ProjExecNonQry(sQry)
            sQry = "Update tblInventoryStock Set Status =  'OR', Modifydate = GetDate()  Where InventoryCode = " & fncFormatSQLText(sInventoryCode) & " And LocationCode=" & fncFormatSQLText(gthisLocation)
            bStatus = dm.ProjExecNonQry(sQry)
            sQry = "Update tblinventoryorderHeader Set Status =  'R'  Where OrderNo = " & fncFormatSQLText(sOrderNo) & " And LocationCode=" & fncFormatSQLText(gthisLocation)
            bStatus = dm.ProjExecNonQry(sQry)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateOrderInventory = bStatus
        End Try
    End Function
    Public Function fncUpdateOrderInventoryHeader(ByVal sOrderNo As String) As Boolean
        Dim sQry As String
        Dim bStatus As Boolean
        Dim sEstimatedCost As Double
        Dim sEstimatedGST As Double
        Try
            sQry = " Select sum(EstimatedCost)EstimatedCost,sum(EstimatedGST)EstimatedGST from tblinventoryorderdetail where OrderNo = " & fncFormatSQLText(sOrderNo)
            DS = dm.getDataSetWithSqlAdapter(sQry, "OPN", "OPN")
            If DS.Tables("OPN").Rows.Count > 0 Then
                sEstimatedCost = DS.Tables("OPN").Rows(0)("EstimatedCost")
                sEstimatedGST = DS.Tables("OPN").Rows(0)("EstimatedGST")
            End If
            sQry = "Update tblinventoryorderHeader Set TotalCostEstimated = " & Val(sEstimatedCost) & " ,TotalGSTEstimated = " & Val(sEstimatedGST) & "   Where OrderNo = " & fncFormatSQLText(sOrderNo)
            bStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateOrderInventoryHeader = bStatus
        End Try
    End Function
#End Region

#Region "Fetch Category"

    Public Function GetCategory(ByVal sClasscode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select * from tblCategory  where classcode = " & fncFormatSQLText(sClasscode.Trim)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "tblCategory", "tblCategory")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetCategory = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

#End Region

#Region "Get Inventory Detail"

    Public Function GetInventoryDetail(ByVal sInventoryCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            'sQry = "SELECT * FROM tblInventory WHERE InventoryCode =" & fncFormatSQLText(sInventoryCode) & " and QtySold = 0 and PCS = 1 and QtyOnHand <> 0 "
            sQry = "SELECT * FROM tblInventory WHERE InventoryCode =" & fncFormatSQLText(sInventoryCode)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Inv", "Inv")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetInventoryDetail = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

#End Region

#Region "Fetch Catalogue"

    Public Function GetCatelogue(ByVal sCategoryCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select * from tblCatalogue  where CategoryCode = " & fncFormatSQLText(sCategoryCode)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "tblCatalogue", "tblCatalogue")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetCatelogue = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

#End Region

#Region "Order Delete"

    Public Function OrderDelete(ByVal sOrderNo As String) As Boolean
        Dim sExec As String
        Try
            OrderDelete = False

            sExec = "INSERT into tblInventoryOrderHeaderDelete" & vbCrLf
            sExec = sExec & "Select * FROM tblInventoryOrderHeader  WHERE orderno =" & fncFormatSQLText(sOrderNo)
            dm.ProjExecNonQry(sExec)

            sExec = "INSERT into tblInventoryOrderDetailDelete" & vbCrLf
            sExec = sExec & "Select * FROM tblInventoryOrderDetail  WHERE orderno =" & fncFormatSQLText(sOrderNo)
            dm.ProjExecNonQry(sExec)

            sExec = "DELETE FROM tblInventoryOrderDetail WHERE orderno =" & fncFormatSQLText(sOrderNo) & vbCrLf
            sExec = sExec & "DELETE FROM tblInventoryOrderHeader  WHERE orderno =" & fncFormatSQLText(sOrderNo)
            dm.ProjExecNonQry(sExec)
            OrderDelete = True
            Exit Function
        Catch ex As Exception
            OrderDelete = False
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Function

    Public Function fncOrderDetailDelete(ByVal sOrderNo As String) As Boolean
        Dim sExec As String
        Try
            fncOrderDetailDelete = False
            sExec = sExec & "DELETE FROM tblInventoryOrderDetail WHERE orderno =" & fncFormatSQLText(sOrderNo)
            dm.ProjExecNonQry(sExec)
            fncOrderDetailDelete = True
        Catch ex As Exception
            fncOrderDetailDelete = False
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Function

    Public Function fncDeleteOrderDetail(ByVal sOrderNo As String, ByVal sOrderInventoryCode As String) As Boolean
        Dim sExec As String
        Try
            fncDeleteOrderDetail = False
            sExec = sExec & "DELETE FROM tblInventoryOrderDetail WHERE orderno =" & fncFormatSQLText(sOrderNo) & " and OrderInventoryCode = " & fncFormatSQLText(sOrderInventoryCode)
            dm.ProjExecNonQry(sExec)
            fncDeleteOrderDetail = True
        Catch ex As Exception
            fncDeleteOrderDetail = False
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Function

    Public Function fncOrderPaymentDetailDelete(ByVal sOrderNo As String, ByVal sOrderPaymentNo As String, ByVal dAmount As Double) As Boolean
        Dim sExec As String
        Try
            fncOrderPaymentDetailDelete = False
            sExec = "INSERT into tblOrderPaymentDetailDelete" & vbCrLf
            sExec = sExec & "Select * FROM tblOrderPaymentDetail  WHERE orderno =" & fncFormatSQLText(sOrderNo)
            dm.ProjExecNonQry(sExec)
            sExec = "DELETE FROM tblOrderPaymentDetail WHERE orderno =" & fncFormatSQLText(sOrderNo)
            If sOrderPaymentNo.Trim <> "" Then sExec = sExec & " and OrderPaymentNo = " & fncFormatSQLText(sOrderPaymentNo)
            dm.ProjExecNonQry(sExec)
            sExec = "Update tblInventoryOrderHeader Set AdvancePaid=AdvancePaid - " & dAmount & "Where Orderno=" & fncFormatSQLText(sOrderNo)
            dm.ProjExecNonQry(sExec)
            fncOrderPaymentDetailDelete = True
        Catch ex As Exception
            fncOrderPaymentDetailDelete = False
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Function

#End Region

#Region " Order Save"

    Public Function FncOrderSave(ByVal sOrderNo As String, ByVal sOrderDate As String, _
                                    ByVal sCustomerCode As String, ByVal sClassCode As String, _
                                    ByVal sCategoryCode As String, ByVal sCatalogueCode As String, _
                                    ByVal sSuppliercode As String, ByVal sDeliveryDate As String, _
                                    ByVal sWeightEstimated As Decimal, ByVal sGoldLoss As Decimal, _
                                    ByVal sMC As Decimal, ByVal sCostPerGram As Decimal, _
                                    ByVal sStoneDescription As String, ByVal sStoneWeight As Decimal, _
                                    ByVal sTradeInNo As String, ByVal sTradeInType As String, _
                                    ByVal sRateFixed As String, ByVal sTotalCostEstimated As Decimal, _
                                    ByVal sAdvancePaid As Decimal, ByVal sInventoryCode As String, _
                                    ByVal sStatus As String, ByVal sRemarks As String, ByVal sInvType As String, ByVal sAdminUserId As String) As Boolean
        Dim sQry As String

        Try
            sQry = sQry & "EXEC sp_OrderInventorySave " & fncFormatSQLText(sOrderNo) & ","
            sQry = sQry & fncFormatSQLDate(sOrderDate) & ","
            sQry = sQry & fncFormatSQLText(sCustomerCode) & ","
            sQry = sQry & fncFormatSQLText(sClassCode) & ","
            sQry = sQry & fncFormatSQLText(sCategoryCode) & ","
            sQry = sQry & fncFormatSQLText(sCatalogueCode) & ","
            sQry = sQry & fncFormatSQLText(sSuppliercode) & ","
            sQry = sQry & fncFormatSQLDate(sDeliveryDate) & ","
            sQry = sQry & Val(sWeightEstimated) & ","
            sQry = sQry & Val(sGoldLoss) & ","
            sQry = sQry & Val(sMC) & ","
            sQry = sQry & Val(sCostPerGram) & ","
            sQry = sQry & fncFormatSQLText(sStoneDescription) & ","
            sQry = sQry & Val(sStoneWeight) & ","
            sQry = sQry & fncFormatSQLText(sTradeInNo) & ","
            sQry = sQry & fncFormatSQLText(sTradeInType) & ","
            sQry = sQry & fncFormatSQLText(sRateFixed) & ","
            sQry = sQry & Val(sTotalCostEstimated) & ","
            sQry = sQry & Val(sAdvancePaid) & ","
            sQry = sQry & fncFormatSQLText(sInventoryCode) & ","
            sQry = sQry & fncFormatSQLText(sStatus) & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & fncFormatSQLText(sInvType) & ","
            sQry = sQry & fncFormatSQLText(sUserID) & ","
            sQry = sQry & fncFormatSQLText(sAdminUserId) & ","
            sQry = sQry & fncFormatSQLText(gthisLocation)
            FncOrderSave = dm.ProjExecNonQry(sQry)

        Catch ex As Exception
            MsgBox(Err.Description)
            FncOrderSave = False
        Finally
            Err.Clear()
            sQry = Nothing
        End Try
    End Function

    Public Function FncOrderHeaderSave(ByVal sOrderNo As String, ByVal sOrderDate As String, _
                                    ByVal sCustomerCode As String, ByVal sSuppliercode As String, _
                                    ByVal sDeliveryDate As String, ByVal dCostPerGram As Decimal, _
                                    ByVal sTradeInNo As String, ByVal sTradeInType As String, _
                                    ByVal sRateFixed As String, ByVal sTotalCostEstimated As Decimal, _
                                    ByVal dAdvancePaid As Decimal, ByVal sStatus As String, _
                                    ByVal sRemarks As String, ByVal sAdminUserId As String, _
                                    ByVal sInvType As String, ByVal dSupplierCost As Decimal, _
                                    ByVal sTotalGSTEstimated As Decimal, ByVal sIssueDate As String, ByVal sSalesManCode As String, ByVal sTradeInAmt As Decimal, ByVal sTradeInGST As Decimal) As Boolean
        Dim sQry As String
        Try
            sQry = sQry & "EXEC sp_InventoryOrderHeader_save " & fncFormatSQLText(sOrderNo) & ","
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
            sQry = sQry & Val(sTotalGSTEstimated) & ","
            sQry = sQry & fncFormatSQLText(sUserID) & ","
            sQry = sQry & fncFormatSQLDate(sIssueDate) & ","
            sQry = sQry & fncFormatSQLText(sSalesManCode) & ","
            sQry = sQry & Val(sTradeInAmt) & ","
            sQry = sQry & Val(sTradeInGST)

            FncOrderHeaderSave = dm.ProjExecNonQry(sQry)

        Catch ex As Exception
            MsgBox(Err.Description)
            FncOrderHeaderSave = False
        Finally
            Err.Clear()
            sQry = Nothing
        End Try
    End Function

    Public Function FncOrderDetailSave(ByVal sOrderNo As String, ByVal sDesc As String, ByVal sOrderInventoryCode As String, _
                                    ByVal sStoneDesc As String, ByVal sClassCode As String, _
                                    ByVal sCategoryCode As String, ByVal sCatalogueCode As String, _
                                    ByVal dWeightEstimated As Decimal, ByVal dStoneWeight As Decimal, _
                                    ByVal dGoldLoss As Decimal, ByVal dMC As Decimal, _
                                    ByVal dCostPerGram As Decimal, ByVal dEstimatedCost As Decimal, ByVal sStatus As String, _
                                    ByVal dEstimatedGST As Decimal, ByVal dStoneCost As Decimal, ByVal sSizeCode As String, ByVal sSizeType As String) As Boolean
        Dim sQry As String
        Try
            sQry = sQry & "EXEC sp_InventoryOrderDetail_save " & fncFormatSQLText(sOrderNo) & ","
            sQry = sQry & fncFormatSQLText(sDesc) & ","
            sQry = sQry & fncFormatSQLText(sOrderInventoryCode) & ","
            sQry = sQry & fncFormatSQLText(sStoneDesc) & ","
            sQry = sQry & fncFormatSQLText(sClassCode) & ","
            sQry = sQry & fncFormatSQLText(sCategoryCode) & ","
            sQry = sQry & fncFormatSQLText(sCatalogueCode) & ","
            sQry = sQry & Val(dWeightEstimated) & ","
            sQry = sQry & Val(dStoneWeight) & ","
            sQry = sQry & Val(dGoldLoss) & ","
            sQry = sQry & Val(dMC) & ","
            sQry = sQry & Val(dCostPerGram) & ","
            sQry = sQry & Val(dEstimatedCost) & ","
            sQry = sQry & fncFormatSQLText(sStatus) & ","
            sQry = sQry & fncFormatSQLText(dEstimatedGST) & ","
            sQry = sQry & fncFormatSQLText(dStoneCost) & ","
            sQry = sQry & Val(sSizeCode) & ","
            sQry = sQry & fncFormatSQLText(sSizeType) & ","
            sQry = sQry & fncFormatSQLText(sUserID)

            FncOrderDetailSave = dm.ProjExecNonQry(sQry)

        Catch ex As Exception
            MsgBox(Err.Description)
            FncOrderDetailSave = False
        Finally
            Err.Clear()
            sQry = Nothing
        End Try
    End Function

    Public Function FncOrderPaymentSave(ByVal sOrderNo As String, ByVal sOrderPaymentNo As String, _
                                    ByVal dAmountPaid As Decimal, ByVal sRemarks As String, ByVal sTradeInAmt As Decimal, ByVal sTradeInGST As Decimal) As Boolean
        Dim sQry As String
        Try
            sQry = sQry & "EXEC sp_OrderPaymentDetailSave " & fncFormatSQLText(sOrderNo) & ","
            sQry = sQry & fncFormatSQLText(sOrderPaymentNo) & ","
            sQry = sQry & Val(dAmountPaid) & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & fncFormatSQLText(sUserID) & ","
            sQry = sQry & Val(sTradeInAmt) & ","
            sQry = sQry & Val(sTradeInGST)

            FncOrderPaymentSave = dm.ProjExecNonQry(sQry)

        Catch ex As Exception
            MsgBox(Err.Description)
            FncOrderPaymentSave = False
        Finally
            Err.Clear()
            sQry = Nothing
        End Try
    End Function
    Public Function GetOrderBalance(ByVal sOrderNo As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            ''    sQry = "SELECT * FROM tblInstallmentEntryHeader where InsAgreementNo = " & fncFormatSQLText(sInsAgreement)
            sQry = "SELECT AdvancePaid,TotalCostEstimated  FROM tblInventoryOrderHeader where OrderNo = " & fncFormatSQLText(sOrderNo)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Ord", "Ord")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetOrderBalance = Ds
            Ds = Nothing
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
                                        ByVal dDetailDiscont As Double, ByVal dDiscount As Double, ByVal dTotalCost As Double, ByVal dTotalPCS As Double, ByVal sJournalNo As String, ByVal sUserid As String, _
                                        ByVal dTradeInAmt As Double, ByVal dTradeInGST As Double) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_SaveOrderEntrySales "
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
            sQry = sQry & fncFormatSQLText(sJournalNo) & ", "
            sQry = sQry & fncFormatSQLText(sUserid) & ", "
            sQry = sQry & Val(dTradeInAmt) & ", "
            sQry = sQry & Val(dTradeInGST)
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
            sQry = "EXEC sp_SalesOrderDetail " & fncFormatSQLText(sInvoiceNo) & ","
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
    Public Function GetOrderStatus(ByVal sConn As String, ByVal sDsName As String, _
                     ByVal sLocation As String) As DataSet
        Dim DS As DataSet
        Dim sSql As String
        Try
            sSql = "SELECT OrderNo,OrderDate,DeliveryDate,a.CustomerCode,Mobile FROM TBLINVENTORYORDERHEADER a,tblCustomer b WHERE "
            sSql = sSql & " a.CustomerCode = b.CustomerCode And DateDiff(Day, getdate(), deliverydate) <= 5 and InvType='O'"
            sSql = sSql & fncFormatSQLText(sLocation)
            DS = fncGetDatasetWithQuery(sConn, sDsName, sSql)
            GetOrderStatus = DS
            DS = Nothing
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            DS = Nothing
            GetOrderStatus = Nothing
        Finally
            DS = Nothing
        End Try
    End Function
#End Region

#Region "Connection Info"

    Public Function OpenOrderConnection()
        gsConOrder.ConnectionString = gsCon
        gsConOrder.Open()
    End Function

    Public Function gpBeginOrderTransaction()
        gsTrans = gsConOrder.BeginTransaction
    End Function

    Public Function gpCommitOrderTransaction()
        gsTrans.Commit()
    End Function

    Public Function gpRollBackOrderConnection()
        gsTrans.Rollback()
    End Function

    Public Function CloseOrderConnection()
        gsConOrder.Close()
    End Function

#End Region

End Class
