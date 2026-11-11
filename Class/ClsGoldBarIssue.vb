Public Class ClsGoldBarIssue
    Dim dm As New DataManager

    Public Function GoldBarIssueProceed(ByVal sPBillNo As String, ByVal sPDate As String, ByVal sSupplierCode As String, _
                                    ByVal dPurchaseWeight As Double, ByVal dPurchaseCost As Double, _
                                    ByVal dTempPurchaseWeight As Double, ByVal dIssuedWeight As Double, _
                                    ByVal sInvoiceNo As String, ByVal sInvoiceDate As String, ByVal sClassCode As String, _
                                    ByVal dPaidCost As Double, ByVal sTranType As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "sp_GoldBarIssued "
            sQry = sQry & fncFormatSQLText(sPBillNo) & ","
            sQry = sQry & fncFormatSQLDate(sPDate) & ","
            sQry = sQry & fncFormatSQLText(sSupplierCode) & ","
            sQry = sQry & Val(dPurchaseWeight) & ","
            sQry = sQry & Val(dPurchaseCost) & ","
            sQry = sQry & Val(dTempPurchaseWeight) & ","
            sQry = sQry & Val(dIssuedWeight) & ","
            sQry = sQry & fncFormatSQLText(sInvoiceNo) & ","
            sQry = sQry & fncFormatSQLDate(sInvoiceDate) & ","
            sQry = sQry & fncFormatSQLText(sClassCode) & ","
            sQry = sQry & Val(dPaidCost) & ","
            sQry = sQry & fncFormatSQLText(sTranType) & ","
            sQry = sQry & fncFormatSQLText(sUserID) & ""
            SaveStatus = DataManager.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            GoldBarIssueProceed = SaveStatus
            sQry = Nothing
        End Try
    End Function

    Public Function UpdateGoldBarPurchase(ByVal dIssued As Double, ByVal dNetIssue As Double, ByVal sIssueNo As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "UpDate tblGoldBarPurchase Set Issuedweight = IssuedWeight + "
            sQry = sQry & Val(dIssued) & ", BalanceWeight = BalanceWeight - "
            sQry = sQry & Val(dNetIssue)
            sQry = sQry & " Where InvoiceNo =" & fncFormatSQLText(sIssueNo)
            SaveStatus = DataManager.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            UpdateGoldBarPurchase = SaveStatus
            sQry = Nothing
        End Try
    End Function
    Public Function GetTradeInInvoiceWholeSale(ByVal sSupplierCode As String, ByVal sInvoiceNo As String) As DataSet
        Dim Ds As DataSet
        Dim sQry As String
        Ds = Nothing
        Try
            'If sInvoiceNo <> "" Then
            '    sQry = "SELECT * FROM tblJobsheetHeader where purchasebillno not in (select invoiceno from tbltradeissuehistory) and  PurchaseBillNo not  in (" & Trim(sInvoiceNo) & ") and suppliercode=" & fncFormatSQLText(sSupplierCode)
            '    sQry = sQry & " order By PurchaseBillNo"
            'Else
            '    sQry = "SELECT * FROM tblJobsheetHeader where purchasebillno not in (select invoiceno from tbltradeissuehistory) and suppliercode=" & fncFormatSQLText(sSupplierCode)
            '    sQry = sQry & " order By PurchaseBillNo"
            'End If
            If sInvoiceNo <> "" Then
                'sQry = "select PurchaseBillNo,PurchaseBillDate,a.SupplierCode,(TotalWeight24K)-isnull(Iss,0) PurchaseWeight from tbljobsheetheader a left join"
                'sQry = sQry & " (select PBillNo,SupplierCode,sum(isnull(IssuedWeight,0))Iss from"
                'sQry = sQry & " tblGoldBarIssuedInvoice  group by PBillNo,SupplierCode )b  on a.PurchaseBillNo=PBillNo and a.suppliercode=b.suppliercode"
                'sQry = sQry & " where a.suppliercode = " & fncFormatSQLText(sSupplierCode)
                'sQry = sQry & " and  PurchaseBillNo not  in (" & Trim(sInvoiceNo) & ")"
                'sQry = sQry & " order By PurchaseBillNo"
                ''sQry = " select InvoiceNo,InvoiceDate,SupplierCode,(isnull(PurchaseWeight,0)*.92)-isnull(PurchaseFix,0) PurchaseWeight "
                ''sQry = sQry & " from tblPurchaseFix where suppliercode = " & fncFormatSQLText(sSupplierCode)
                ''sQry = sQry & " and  InvoiceNo not  in (" & Trim(sInvoiceNo) & ")"
                ''sQry = sQry & " order By InvoiceNo"
                sQry = " select InvoiceNo,InvoiceDate,SupplierCode,isnull(BalanceWeight,0)-isnull(PurchaseFix,0)-isnull(issuedweight,0) PurchaseWeight "
                sQry = sQry & " from tblPurchaseFix where suppliercode = " & fncFormatSQLText(sSupplierCode)
                sQry = sQry & " and  InvoiceNo not  in (" & Trim(sInvoiceNo) & ")"
                sQry = sQry & " order By InvoiceNo"
            Else
                sQry = " select InvoiceNo,InvoiceDate,SupplierCode,(isnull(BalanceWeight,0)-isnull(PurchaseFix,0)-isnull(issuedweight,0)) PurchaseWeight "
                sQry = sQry & " from tblPurchaseFix where (isnull(BalanceWeight,0)-isnull(PurchaseFix,0)-isnull(issuedweight,0))>1 and  suppliercode = " & fncFormatSQLText(sSupplierCode)
                sQry = sQry & " order By InvoiceNo"
            End If
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Purchase", "Purchase")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetTradeInInvoiceWholeSale = Ds
            Ds = Nothing
        End Try
    End Function

    Public Function GetGoldBarNo(ByVal sInventoryCode As String) As DataSet
        Dim Ds As DataSet
        Dim sQry As String
        Try

          
            sQry = "select * from tblInventoryBarCoin "
            sQry = sQry & " where InventoryCode = " & fncFormatSQLText(sInventoryCode)
            sQry = sQry & " order By BarNo"

            Ds = dm.getDataSetWithSqlAdapter(sQry, "Purchase", "Purchase")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetGoldBarNo = Ds
            Ds = Nothing
        End Try
    End Function
    Public Function GetUpdateGoldBarIssue() As DataSet
        Dim sQry As String
        Dim Ds As New DataSet
        Try

            sQry = "EXEC sp_GetUpdateGoldBarIssue "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "GoldBarIssue", "GoldBarIssue")

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetUpdateGoldBarIssue = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetTradeInBalanceSales(ByVal sInvoiceNo As String, ByVal sInventoryCode As String, ByVal sSupplierCode As String) As DataSet
        Dim Ds As DataSet
        Dim sQry As String
        Try
            
            If sInvoiceNo <> "" Then
                'sQry = " select ''JobsheetNo,InvoiceNo PurchaseBillNo,InvoiceDate PurchaseBillDate,SupplierCode,(isnull(PurchaseWeight,0)*.92)-isnull(PurchaseFix,0)-isnull(issuedweight,0)  TotalWeight24K,ModifyDate "
                'sQry = sQry & " from tblPurchaseFix where suppliercode = " & fncFormatSQLText(sSupplierCode)
                'sQry = sQry & " and  InvoiceNo  in (" & Trim(sInvoiceNo) & ")"
                'sQry = sQry & " order By InvoiceNo"

                sQry = " select ''JobsheetNo,InvoiceNo PurchaseBillNo,InvoiceDate PurchaseBillDate,SupplierCode,(isnull(Balanceweight,0))-isnull(PurchaseFix,0)-isnull(issuedweight,0)  TotalWeight24K,ModifyDate "
                sQry = sQry & " from tblPurchaseFix where suppliercode = " & fncFormatSQLText(sSupplierCode)
                sQry = sQry & " and  InvoiceNo  in (" & Trim(sInvoiceNo) & ")"
                sQry = sQry & " order By InvoiceNo"
            Else
                sQry = "select JobsheetNo,PurchaseBillNo,PurchaseBillDate,a.SupplierCode,TotalWeight24K-isnull(Iss,0) TotalWeight24K,ModifyDate from tbljobsheetheader a left join"
                sQry = sQry & " (select PBillNo,SupplierCode,sum(isnull(IssuedWeight,0))Iss from"
                sQry = sQry & " tblGoldBarIssuedInvoice  group by PBillNo,SupplierCode )b  on a.PurchaseBillNo=PBillNo and a.suppliercode=b.suppliercode"
                sQry = sQry & " where a.suppliercode = " & fncFormatSQLText(sSupplierCode)
                sQry = sQry & " order By PurchaseBillNo"
            End If
            Ds = dm.getDataSetWithSqlAdapter(sQry, "TradeIn", "TradeIn")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetTradeInBalanceSales = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
End Class
