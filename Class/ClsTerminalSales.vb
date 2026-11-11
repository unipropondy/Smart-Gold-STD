Public Class ClsTerminalSales
    Dim dm As New DataManager

    Public Function OpenSettlementConnection()
        gsConSettlement.ConnectionString = gsCon
        gsConSettlement.Open()
    End Function

    Public Function BeginSettlementTransaction()
        gsTrans = gsConSettlement.BeginTransaction
    End Function

    Public Function CommitSettlementTransaction()
        gsTrans.Commit()
    End Function

    Public Function RollBackSettlementConnection()
        gsTrans.Rollback()
    End Function

    Public Function CloseSettlementConnection()
        If gsConSettlement.State = ConnectionState.Open Then gsConSettlement.Close()
    End Function

    Public Function DeleteExsistingTerminalSales(ByVal sCashier As String, ByVal sDate As String, ByVal sTerminalCode As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "DELETE FROM tblTerminalSales WHERE Date=" & fncFormatSQLText(sDate) & "and TerminalCode=" & fncFormatSQLText(sTerminalCode) & vbCrLf
            sQry = sQry & "DELETE FROM tblTerminalDinamination WHERE  PostingDate=" & fncFormatSQLText(sDate) & "and TerminalCode=" & fncFormatSQLText(sTerminalCode) & vbCrLf
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            DeleteExsistingTerminalSales = SaveStatus
            sQry = Nothing
        End Try
    End Function

    Public Function TerminalSalesPayModeAmountSave(ByVal CashierId As String, _
                          ByVal sDate As String, ByVal TerminalCode As String, ByVal sPayMode As String, ByVal scAmount As Double, ByVal smAmount As Double) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_TerminalSales" & fncFormatSQLText(CashierId) & ","
            sQry = sQry & fncFormatSQLText(sDate) & ","
            sQry = sQry & fncFormatSQLText(TerminalCode) & ","
            sQry = sQry & fncFormatSQLText(sPayMode) & ","
            sQry = sQry & fncFormatSQLText(gthisLocation) & ","
            sQry = sQry & Val(scAmount) & ","
            sQry = sQry & Val(smAmount) & vbCrLf
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            TerminalSalesPayModeAmountSave = SaveStatus
            sQry = Nothing
        End Try
    End Function

    Public Function TerminalSalesDinaminationAmountSave(ByVal CashierId As String, _
                           ByVal TerminalCode As String, ByVal sDate As String, ByVal scValue As Double, ByVal scQty As Double) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = sQry & "EXEC sp_Dinamination " & fncFormatSQLText(CashierId) & ","
            sQry = sQry & fncFormatSQLText(TerminalCode) & ","
            sQry = sQry & fncFormatSQLText(sDate) & ","
            sQry = sQry & Format(Val(scValue), "0.00") & ","
            sQry = sQry & Format(Val(scQty), "0.00") & vbCrLf
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            TerminalSalesDinaminationAmountSave = SaveStatus
            sQry = Nothing
        End Try
    End Function

    Public Function GetCurrencyDinaminationAndTerminal() As DataSet
        Dim Ds As DataSet
        Dim sQry As String
        Try
            sQry = "SELECT * from tblcurrencydinamination order by currencyvalue desc" & vbCrLf
            sQry = sQry & "SELECT * FROM tblTerminal where ComputerName=HOST_NAME()"

            Ds = dm.getDataSetWithSqlAdapter(sQry, "CD", "CD")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetCurrencyDinaminationAndTerminal = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function GetTerminal() As DataSet
        Dim Ds As DataSet
        Dim sQry As String
        Try
            sQry = sQry & "SELECT * FROM tblTerminal where ComputerName=HOST_NAME()"
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Terminal", "Terminal")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetTerminal = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function


    Public Function GetTerminalSalesList(ByVal cName As String, ByVal sDate As String, ByVal sTerminal As String) As DataSet
        Dim Ds As DataSet
        Dim sQry As String
        Try
            sQry = "SELECT * FROM tblTerminalDinamination"
            sQry = sQry & " Where PostingDate = " & fncFormatSQLText(sDate)
            'If UCase(Trim(sCompanyShortCode)) <> "LF" Then
            '    sQry = sQry & " And TerminalCode = " & fncFormatSQLText(sTerminal)
            'End If
            sQry = sQry & " Order By Qty Desc"

            Ds = dm.getDataSetWithSqlAdapter(sQry, "CD", "CD")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetTerminalSalesList = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function GetPayModeList(ByVal cName As String, ByVal sDate As String, ByVal sTerminal As String) As DataSet
        Dim Ds As DataSet
        Dim sQry As String
        Try
            sQry = "SELECT * FROM tblTerminalSales A, TBLPayMode b "
            sQry = sQry & " WHERE  Convert(Char(8),Date,112)  = " & fncFormatSQLText(sDate)
            sQry = sQry & " And a.Paymode = b.Paymode  "
            sQry = sQry & " And Terminalcode = " & fncFormatSQLText(sTerminal)
            sQry = sQry & " ORDER BY Position"
            

            Ds = dm.getDataSetWithSqlAdapter(sQry, "CD", "CD")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetPayModeList = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function GetTerminalSales(ByVal cName As String, ByVal TerminalId As String) As DataSet
        Dim Ds As DataSet
        Dim sQry As String
        Try
            sQry = "SELECT A.PayMode, Amount=sum(A.Amount) + sum(A.Commision) FROM  tblPaymodeDetail A, tblPayMode B , tblSalesHeader C " & _
                "WHERE convert(char(8),A.InvoiceDate,112) = " & Format(Now, "yyyyMMdd") & " And A.Paymode = B.Paymode and C.InvoiceNo = A.InvoiceNo " & _
                 " and SalesManCode = " & fncFormatSQLText(cName) & " and TerminalCode = " & fncFormatSQLText(Trim(TerminalId)) & "" & _
                 "group By A.PayMode, B.Position Order By B.Position"
            Ds = dm.getDataSetWithSqlAdapter(sQry, "TerminalSales", "TerminalSales")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetTerminalSales = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function GetTillAmount(ByVal TerminalId As String) As DataSet
        Dim Ds As DataSet
        Dim sQry As String
        Try
            sQry = "SELECT * from tblTerminal where TerminalName=" & fncFormatSQLText(TerminalId) & ""
            Ds = dm.getDataSetWithSqlAdapter(sQry, "TerminalSales", "TerminalSales")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetTillAmount = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function GetTerminalTradeIn(ByVal cName As String, ByVal TerminalId As String) As DataSet
        Dim Ds As DataSet
        Dim sQry As String
        Try
            sQry = "Select ISNULL(Sum(TotalCost), 0) From tblTradeInHeader where InvoiceNo in " & _
                "(Select InvoiceNo from tblSalesHeader where convert(char(8),InvoiceDate,112) = " & Format(Now, "yyyyMMdd") & _
                " and SalesManCode = " & fncFormatSQLText(cName) & " and TerminalCode = " & fncFormatSQLText(TerminalId) & ")"
            Ds = dm.getDataSetWithSqlAdapter(sQry, "TerminalSales", "TerminalSales")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetTerminalTradeIn = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function GetExpencesAmount(ByVal cName As String, ByVal sDate As String, ByVal sTerminal As String) As DataSet
        Dim Ds As DataSet
        Dim sQry As String
        Try
            sQry = "SELECT * FROM tblExpences"
            sQry = sQry & " Where Convert(char(8),ExpDate,112) = " & fncFormatSQLText(sDate)
            sQry = sQry & " And TerminalCode = " & fncFormatSQLText(sTerminal)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "TerminalSales", "TerminalSales")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetExpencesAmount = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function


End Class
