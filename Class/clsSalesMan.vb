Public Class clsSalesMan
    Dim dm As New DataManager
    Public Function GetSalesMan() As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " Select * from tblSalesMan "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "SalesMan", "SalesMan")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetSalesMan = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetSalesManwithCode(ByVal sSalesMancode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select * from tblSalesMan where SalesManCode=" & fncFormatSQLText(sSalesMancode)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "SalesMan", "SalesMan")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetSalesManwithCode = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function DeleteSalesMan(ByVal sSalesManCode As String) As Boolean
        Dim sQry As String
        Dim DelStatus As Boolean
        Try
            sQry = " Delete from tblSalesMan where SalesManCode = " & fncFormatSQLText(sSalesManCode)
            DelStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            DelStatus = False
        Finally
            DeleteSalesMan = DelStatus
            sQry = Nothing
        End Try
    End Function

    Public Function SalesManSave(ByVal sSalesManCode As String, ByVal sSalesManName As String, ByVal sPassword As String, _
                                 ByVal sRemarks As String, ByVal nSortOrder As Integer, ByVal nShowInReport As Integer, _
                                 ByVal nAllowSalesReturn As Integer, ByVal sSalesManGroup As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_SalesMan " & fncFormatSQLText(sSalesManCode) & ","
            sQry = sQry & fncFormatSQLText(sSalesManName) & ","
            sQry = sQry & fncFormatSQLText(sPassword) & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & Val(nSortOrder) & ","
            sQry = sQry & Val(nShowInReport) & ","
            sQry = sQry & Val(nAllowSalesReturn) & ","
            sQry = sQry & fncFormatSQLText(sSalesManGroup) & ","
            sQry = sQry & fncFormatSQLText(sUserID)
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            SalesManSave = SaveStatus
            sQry = Nothing
        End Try
    End Function

    Public Function SalesManLogSave(ByVal sSalesManCode As String, ByVal sSalesManName As String, _
                                    ByVal sType As String, ByVal nAllowSalesReturn As Integer) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_SalesManLog " & fncFormatSQLText(sSalesManCode) & ","
            sQry = sQry & fncFormatSQLText(sSalesManName) & ","
            sQry = sQry & fncFormatSQLText(sType) & ","
            sQry = sQry & Val(nAllowSalesReturn) & ","
            sQry = sQry & fncFormatSQLText(sUserID)
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            SalesManLogSave = SaveStatus
            sQry = Nothing
        End Try
    End Function

#Region "Added by Sarathiy"

    Public Function GetSalesManwithPassword(ByVal sSalesManPWD As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select * from tblSalesMan where password =" & fncFormatSQLText(sSalesManPWD)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "SalesMan", "SalesMan")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetSalesManwithPassword = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function GetLastBillDetail() As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "SELECT TOP 1 A.INVOICENO,A.NETTOTAL,A.SALESMANCODE,B.SALESMANCODE,B.SALESMANNAME FROM TBLSALESHEADER A,TBLSALESMAN B WHERE A.SALESMANCODE=B.SALESMANCODE ORDER BY A.CREATEDATE DESC"
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Bill", "Bill")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetLastBillDetail = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetSalesManSalesManTotalAmt(ByVal sSalesManCode As String, ByVal sDate As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " Select TotalAmount=Sum(NetTotal) from tblSalesHeader where SalesManCode =" & fncFormatSQLText(sSalesManCode)
            sQry = sQry & " And InvoiceDAte=" & fncFormatSQLText(sDate)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "SalesMan", "SalesMan")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetSalesManSalesManTotalAmt = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
#End Region


End Class
