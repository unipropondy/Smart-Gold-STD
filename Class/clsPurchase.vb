Public Class clsPurchase
    Dim dm As New DataManager
    Public Function GetGoldLossByWeight(ByVal dWeight As Double) As DataSet
        Dim Ds As DataSet
        Dim sQry As String

        Try
            GetGoldLossByWeight = Nothing
            sQry = "SELECT GoldLoss FROM tblGoldLoss where " & Val(dWeight) & " between FromWeight and ToWeight"
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Purchase", "Purchase")

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetGoldLossByWeight = Ds
            Ds = Nothing
        End Try
    End Function
    Public Function GetGoldBarIssue(ByVal sIssueNo As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "SELECT * FROM tblGoldBarIssued where IssueNo = " & fncFormatSQLText(sIssueNo)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Ins", "Ins")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetGoldBarIssue = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetTradeInIssue(ByVal sIssueNo As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "SELECT * FROM tblTradeissuehistoryHeader where TradeIssueNo = " & fncFormatSQLText(sIssueNo)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Ins", "Ins")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetTradeInIssue = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function PurchaseSave(ByVal sJobsheetNo As String, ByVal sPurchaseBillNo As String, _
                                ByVal sDirectPurchaseNo As String, ByVal sSuppllierCode As String, ByVal sinventorycode As String, _
                               ByVal sDescription As String, ByVal sDiamondDescription As String, _
                               ByVal nStoneWeight As Double, ByVal nCostPerGram As Double, ByVal nWeight As Double, _
                               ByVal nMC As Double, ByVal nPCS As Double, _
                               ByVal nGoldLoss As Double, ByVal nTotalCost As Double, _
                               ByVal sCatalogueCode As String, ByVal sCategoryCode As String, ByVal sClassCode As String, ByVal sTrayId As String, _
                               ByVal sPurchaseType As String, ByVal sRemarks As String, ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim Status As Boolean

        Try

            PurchaseSave = False

            sQry = ""
            sQry = sQry & "EXEC sp_Purchase " & fncFormatSQLText(sJobsheetNo) & ","
            sQry = sQry & fncFormatSQLText(sPurchaseBillNo) & ","
            sQry = sQry & fncFormatSQLText(sDirectPurchaseNo) & ","
            sQry = sQry & fncFormatSQLText(sSuppllierCode) & ","
            sQry = sQry & fncFormatSQLText(sinventorycode) & ","
            sQry = sQry & fncFormatSQLText(sDescription) & ","
            sQry = sQry & fncFormatSQLText(sDiamondDescription) & ","
            sQry = sQry & Val(nStoneWeight) & ","
            sQry = sQry & Val(nCostPerGram) & ","
            sQry = sQry & Val(nWeight) & ","
            sQry = sQry & Val(nMC) & ","
            sQry = sQry & Val(nPCS) & ","
            sQry = sQry & Val(nGoldLoss) & ","
            sQry = sQry & Val(nTotalCost) & ","
            sQry = sQry & fncFormatSQLText(sCatalogueCode) & ","
            sQry = sQry & fncFormatSQLText(sCategoryCode) & ","
            sQry = sQry & fncFormatSQLText(sClassCode) & ","
            sQry = sQry & fncFormatSQLText(sTrayId) & ","
            sQry = sQry & fncFormatSQLText(sPurchaseType) & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & fncFormatSQLText(gthisLocation) & ","
            sQry = sQry & fncFormatSQLText(sUserId) & ""

            Status = dm.ProjExecNonQry(sQry)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            PurchaseSave = False
        Finally
            PurchaseSave = True
        End Try
    End Function
    Public Function PurchaseSaveML(ByVal sJobsheetNo As String, ByVal sPurchaseBillNo As String, _
                                ByVal sDirectPurchaseNo As String, ByVal sSuppllierCode As String, ByVal sinventorycode As String, _
                               ByVal sDescription As String, ByVal sDiamondDescription As String, _
                               ByVal nStoneWeight As Double, ByVal nCostPerGram As Double, ByVal nSize As Double, ByVal nWeight As Double, _
                               ByVal nMC As Double, ByVal nPCS As Double, _
                               ByVal nGoldLoss As Double, ByVal nTotalCost As Double, _
                               ByVal sCatalogueCode As String, ByVal sCategoryCode As String, ByVal sClassCode As String, ByVal sTrayId As String, _
                               ByVal sPurchaseType As String, ByVal sRemarks As String, ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim Status As Boolean

        Try

            PurchaseSaveML = False

            sQry = ""
            sQry = sQry & "EXEC sp_PurchaseML " & fncFormatSQLText(sJobsheetNo) & ","
            sQry = sQry & fncFormatSQLText(sPurchaseBillNo) & ","
            sQry = sQry & fncFormatSQLText(sDirectPurchaseNo) & ","
            sQry = sQry & fncFormatSQLText(sSuppllierCode) & ","
            sQry = sQry & fncFormatSQLText(sinventorycode) & ","
            sQry = sQry & fncFormatSQLText(sDescription) & ","
            sQry = sQry & fncFormatSQLText(sDiamondDescription) & ","
            sQry = sQry & Val(nStoneWeight) & ","
            sQry = sQry & Val(nCostPerGram) & ","
            sQry = sQry & Val(nSize) & ","
            sQry = sQry & Val(nWeight) & ","
            sQry = sQry & Val(nMC) & ","
            sQry = sQry & Val(nPCS) & ","
            sQry = sQry & Val(nGoldLoss) & ","
            sQry = sQry & Val(nTotalCost) & ","
            sQry = sQry & fncFormatSQLText(sCatalogueCode) & ","
            sQry = sQry & fncFormatSQLText(sCategoryCode) & ","
            sQry = sQry & fncFormatSQLText(sClassCode) & ","
            sQry = sQry & fncFormatSQLText(sTrayId) & ","
            sQry = sQry & fncFormatSQLText(sPurchaseType) & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & fncFormatSQLText(gthisLocation) & ","
            sQry = sQry & fncFormatSQLText(sUserId) & ""

            Status = dm.ProjExecNonQry(sQry)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            PurchaseSaveML = False
        Finally
            PurchaseSaveML = True
        End Try
    End Function

    Public Function PurchaseBillSave(ByVal sSupplierCode As String, ByVal sInvoiceNo As String, _
                                   ByVal sInvoiceDate As String, ByVal sPaymentType As String, _
                                   ByVal nPurchaseWeight As Double, ByVal nPurchaseAmount As Double, _
                                   ByVal nPurchaseMC As Double, ByVal sTranType As String, _
                                   ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim Status As Boolean
        Try
            PurchaseBillSave = False

            sQry = ""
            sQry = sQry & "EXEC sp_PurchaseBills " & fncFormatSQLText(sSupplierCode) & ","
            sQry = sQry & fncFormatSQLText(sInvoiceNo) & ","
            sQry = sQry & fncFormatSQLDate(sInvoiceDate) & ","
            sQry = sQry & fncFormatSQLText(sPaymentType) & ","
            sQry = sQry & Val(nPurchaseWeight) & ","
            sQry = sQry & Val(nPurchaseAmount) & ","
            sQry = sQry & Val(nPurchaseMC) & ","
            sQry = sQry & fncFormatSQLText(sTranType) & ","
            sQry = sQry & fncFormatSQLText(sUserId) & ""

            Status = dm.ProjExecNonQry(sQry)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            PurchaseBillSave = False
        Finally
            PurchaseBillSave = True
        End Try
    End Function
    Public Function PurchaseFixSave(ByVal sSupplierCode As String, ByVal sInvoiceNo As String, _
                               ByVal sInvoiceDate As String, ByVal sPaymentType As String, _
                               ByVal nPurchaseWeight As Double, ByVal nPurchaseAmount As Double, _
                               ByVal nPurchaseMC As Double, ByVal sTranType As String, ByVal nPurchaseFix As Double, ByVal n24KWeight As Double, _
                               ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim Status As Boolean
        Try
            PurchaseFixSave = False

            sQry = ""
            sQry = sQry & "EXEC sp_PurchaseFix " & fncFormatSQLText(sSupplierCode) & ","
            sQry = sQry & fncFormatSQLText(sInvoiceNo) & ","
            sQry = sQry & fncFormatSQLDate(sInvoiceDate) & ","
            sQry = sQry & fncFormatSQLText(sPaymentType) & ","
            sQry = sQry & Val(nPurchaseWeight) & ","
            sQry = sQry & Val(nPurchaseAmount) & ","
            sQry = sQry & Val(nPurchaseMC) & ","
            sQry = sQry & fncFormatSQLText(sTranType) & ","
            sQry = sQry & Val(nPurchaseFix) & ","
            sQry = sQry & Val(n24KWeight) & ","
            sQry = sQry & fncFormatSQLText(sUserId) & ""

            Status = dm.ProjExecNonQry(sQry)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            PurchaseFixSave = False
        Finally
            PurchaseFixSave = True
        End Try
    End Function
    Public Function PurchaseDelete(ByVal sinventorycode As String, ByVal sInvoiceNo As String, _
                                ByVal sSupplierCode As String) As Boolean
        Dim SQry As String
        Dim Status As Boolean
        Try
            PurchaseDelete = False

            SQry = "Exec sp_JpInventorydelete "
            SQry = SQry & fncFormatSQLText(sinventorycode)
            SQry = SQry & ", " & fncFormatSQLText(sInvoiceNo)
            SQry = SQry & ", " & fncFormatSQLText(sSupplierCode)
            SQry = SQry & ", " & fncFormatSQLText(sUserID)
            Status = dm.ProjExecNonQry(SQry)
            PurchaseDelete = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Function
    Public Function PurchaseTallySave(ByVal sSupplierCode As String, ByVal sJobsheetNo As String, ByVal sInvoiceNo As String, _
                               ByVal sInvoiceDate As String, _
                               ByVal nPurchaseWeight As Double, ByVal nPurchaseAmount As Double, _
                               ByVal nPurchaseMC As Double, ByVal nBillWeight As Double, _
                               ByVal nBillCost As Double, ByVal nBillMC As Double, _
                               ByVal nPurWeight As Double, ByVal nPurCost As Double, ByVal nPurMC As Double, _
                               ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim Status As Boolean
        Try
            PurchaseTallySave = False

            sQry = ""
            sQry = sQry & "EXEC sp_PurchaseTally " & fncFormatSQLText(sSupplierCode) & ","
            sQry = sQry & fncFormatSQLText(sJobsheetNo) & ","
            sQry = sQry & fncFormatSQLText(sInvoiceNo) & ","
            sQry = sQry & fncFormatSQLDate(sInvoiceDate) & ","
            sQry = sQry & Val(nPurchaseWeight) & ","
            sQry = sQry & Val(nPurchaseAmount) & ","
            sQry = sQry & Val(nPurchaseMC) & ","
            sQry = sQry & Val(nBillWeight) & ","
            sQry = sQry & Val(nBillCost) & ","
            sQry = sQry & Val(nBillMC) & ","
            sQry = sQry & Val(nPurWeight) & ","
            sQry = sQry & Val(nPurCost) & ","
            sQry = sQry & Val(nPurMC) & ","
            sQry = sQry & fncFormatSQLText(sUserId) & ""

            Status = dm.ProjExecNonQry(sQry)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            PurchaseTallySave = False
        Finally
            PurchaseTallySave = True
        End Try
    End Function
    Public Function SupplierMCPercSave(ByVal sSupplierCode As String, ByVal sCatalogueCode As String, ByVal sType As String, ByVal sMCPercentage As Double, _
                            ByVal sFromWeight As Double, ByVal sToWeight As Double, ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim Status As Boolean
        Try
            SupplierMCPercSave = False

            sQry = ""
            sQry = sQry & "EXEC sp_SupplierMCPerc " & fncFormatSQLText(sSupplierCode) & ","
            sQry = sQry & fncFormatSQLText(sCatalogueCode) & ","
            sQry = sQry & fncFormatSQLText(sType) & ","
            sQry = sQry & Val(sMCPercentage) & ","
            sQry = sQry & Val(sFromWeight) & ","
            sQry = sQry & Val(sToWeight) & ","
            sQry = sQry & fncFormatSQLText(sUserId) & ""

            Status = dm.ProjExecNonQry(sQry)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            SupplierMCPercSave = False
        Finally
            SupplierMCPercSave = True
        End Try
    End Function
    Public Function fncGetSupplierPaymentNo() As String
        Dim sQry As String
        Dim Ds As New DataSet
        Dim sNextSupplierPaymentNo As String
        Try
            sNextSupplierPaymentNo = ""
            sQry = "EXEC sp_GetSupplierPaymentNo "
            sQry = sQry & fncFormatSQLText(gthisLocation)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "NextSupplierPaymentNo", "NextSupplierPaymentNo")
            If Ds.Tables("NextSupplierPaymentNo").Rows.Count > 0 Then
                sNextSupplierPaymentNo = Trim(Ds.Tables("NextSupplierPaymentNo").Rows(0)("NextSupplierPaymentNo") & "")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncGetSupplierPaymentNo = sNextSupplierPaymentNo
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function fncGetCreditSalesPaymentNo() As String
        Dim sQry As String
        Dim Ds As New DataSet
        Dim sNextCreditSalesPaymentNo As String
        Try
            sNextCreditSalesPaymentNo = ""
            sQry = "EXEC sp_GetCreditSalesPaymentNo "
            sQry = sQry & fncFormatSQLText(gthisLocation)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "NextCreditSalesPaymentNo", "NextCreditSalesPaymentNo")
            If Ds.Tables("NextCreditSalesPaymentNo").Rows.Count > 0 Then
                sNextCreditSalesPaymentNo = Trim(Ds.Tables("NextCreditSalesPaymentNo").Rows(0)("NextCreditSalesPaymentNo") & "")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncGetCreditSalesPaymentNo = sNextCreditSalesPaymentNo
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
#Region "Added by Sarathy"

    Public Function GetPurchaseBill(ByVal sSupplierCode As String) As DataSet
        Dim Ds As DataSet
        Dim sQry As String
        Try
            sQry = "SELECT * From tblPurchaseBills WHERE SupplierCode =" & fncFormatSQLText(sSupplierCode)
            sQry = sQry & " And BalanceWeight > 0 order By InvoiceDate,SNo"
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Purchase", "Purchase")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetPurchaseBill = Ds
            Ds = Nothing
        End Try
    End Function

#End Region

End Class
