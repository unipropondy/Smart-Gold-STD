Public Class ClsTradeinSales
    Dim dm As New DataManager

    Public Function GetTradeInBalance(ByVal sInvoiceNo As String) As DataSet
        Dim Ds As DataSet
        Dim sQry As String
        Try
            sQry = "SELECT * FROM tblTradeInHeader where InvoiceNo = " & fncFormatSQLText(sInvoiceNo) & " And BalanceWeight > 0 "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "TradeIn", "TradeIn")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetTradeInBalance = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetTradeInTransfer(ByVal sStatus As String) As DataSet
        Dim Ds As DataSet
        Dim sQry As String
        Try

            sQry = "SELECT TradeinInventoryCode,TotalWeight,TotalPrice,Status,Convert(varchar(10),Createdate,105) InvoiceDate FROM tblTradeMergeInventoryheader where Status = " & fncFormatSQLText(sStatus)
            sQry = sQry & " order By TradeinInventoryCode"

            Ds = dm.getDataSetWithSqlAdapter(sQry, "Purchase", "Purchase")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetTradeInTransfer = Ds
            Ds = Nothing
        End Try
    End Function
    Public Function GetTradeInInvoice(ByVal sInvoiceNo As String, ByVal sPurity As String, ByVal sDate As String, ByVal sTDate As String) As DataSet
        Dim Ds As DataSet
        Dim sQry As String
        Try
            If sInvoiceNo <> "" Then
                'sQry = "SELECT * FROM tblTradeInHeader where ((IssueFlag is Null) or (IssueFlag<> 'Y')) and InvoiceNo Not in( " & Trim(sInvoiceNo) & ") "
                'sQry = sQry & " order By InvoiceNo"
                sQry = "SELECT a.InvoiceNo,b.InventoryCode,b.BalanceWeight Weight,b.TotalCost TotalCost,a.CustomerCode,Convert(varchar(10),a.Createdate,105) InvoiceDate,StoneWeight FROM tblTradeInHeader a,tblTradeinDetail b where a.InvoiceNo=b.InvoiceNo and b.BalanceWeight > 0/*and ((IssueFlag is Null) or (IssueFlag<> 'Y'))*/ and a.InvoiceNo Not in( " & Trim(sInvoiceNo) & ")"
                sQry = sQry & " order By a.InvoiceNo"
            Else
                sQry = "SELECT a.InvoiceNo,b.InventoryCode,b.BalanceWeight Weight,b.TotalCost TotalCost,a.CustomerCode,Convert(varchar(10),a.Createdate,105) InvoiceDate,StoneWeight  FROM tblTradeInHeader a,tblTradeinDetail b where a.InvoiceNo=b.InvoiceNo and b.BalanceWeight > 0 /*and ((IssueFlag is Null) or (IssueFlag<> 'Y'))*/ and b.Class  in( " & fncFormatSQLText(sPurity) & ")"
                sQry = sQry & " and Convert(varchar(10),a.Createdate,112) >=" & fncFormatSQLText(sDate) & " and Convert(varchar(10),a.Createdate,112) <=" & fncFormatSQLText(sTDate) & " order By a.InvoiceNo"
            End If
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Purchase", "Purchase")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetTradeInInvoice = Ds
            Ds = Nothing
        End Try
    End Function
    Public Function GetTradeInIssue(ByVal sInvoiceNo As String, ByVal sPurity As String, ByVal sDate As String) As DataSet
        Dim Ds As DataSet
        Dim sQry As String
        Try
            If Trim(sCompanyShortCode) = "MJ" Then

                'If sInvoiceNo <> "" Then
                '    sQry = "SELECT * FROM tblTradeInHeader where ((IssueFlag is Null) or (IssueFlag<> 'Y')) and InvoiceNo Not in( " & Trim(sInvoiceNo) & ") "
                '    sQry = sQry & " order By InvoiceNo"
                'Else
                '    sQry = "SELECT * FROM tblTradeInHeader where ((IssueFlag is Null) or (IssueFlag<> 'Y')) "
                '    sQry = sQry & " order By InvoiceNo"
                'End If

                If sInvoiceNo <> "" Then
                    sQry = "SELECT * FROM tblTradeInHeader where BalanceWeight > 0 and InvoiceNo Not in( " & Trim(sInvoiceNo) & ") "
                    sQry = sQry & " order By InvoiceNo"
                Else
                    sQry = "SELECT * FROM tblTradeInHeader where BalanceWeight > 0  "
                    sQry = sQry & " order By InvoiceNo"
                End If
            Else
                sQry = "SELECT TradeinInventoryCode,isnull(balanceweight,0)TotalWeight,TotalPrice,round(isnull(TotalPrice,0)/isnull(totalweight,0),2) AvgCost,Status,Convert(varchar(10),Createdate,105) InvoiceDate FROM tblTradeMergeInventoryheader where isnull(BalanceWeight,0) > 0"
                sQry = sQry & " and Purity = " & fncFormatSQLText(sPurity) & " order By TradeinInventoryCode"
            End If
            'sQry = "SELECT TradeinInventoryCode,isnull(balanceweight,0)TotalWeight,TotalPrice,round(isnull(TotalPrice,0)/isnull(totalweight,0),2) AvgCost,Status,Convert(varchar(10),Createdate,105) InvoiceDate FROM tblTradeMergeInventoryheader where isnull(BalanceWeight,0) > 0"
            'sQry = sQry & " and Purity = " & fncFormatSQLText(sPurity) & " order By TradeinInventoryCode"

            Ds = dm.getDataSetWithSqlAdapter(sQry, "Purchase", "Purchase")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetTradeInIssue = Ds
            Ds = Nothing
        End Try
    End Function
    Public Function GetTradeInBalanceSales(ByVal sInvoiceNo As String, ByVal sInventoryCode As String) As DataSet
        Dim Ds As DataSet
        Dim sQry As String
        Try
            sQry = "SELECT a.InvoiceNo,b.ModifyDate,b.Weight TotalWeight,b.Weight BalanceWeight,b.TotalCost Amount FROM tblTradeInHeader a,tblTradeInDetail b where a.Invoiceno=b.Invoiceno and  a.InvoiceNo in( " & Trim(sInvoiceNo) & ") and b.InventoryCode in( " & Trim(sInventoryCode) & ") And BalanceWeight > 0 "
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
