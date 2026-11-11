
Public Class clsConsignSalesReturn
    Dim dm As New DataManager

    Public Function BillCancel(ByVal sInvoiceNo As String, ByVal sLocation As String, _
                           ByVal sUserId As String) As Boolean
        Dim sExec As String
        Dim SaveStatus As Boolean
        Try

            sExec = sExec & "EXEC sp_SalesBillCancel " & fncFormatSQLText(sInvoiceNo) & ","
            sExec = sExec & fncFormatSQLText(sLocation) & ","
            sExec = sExec & fncFormatSQLText(sUserId) & ""

            SaveStatus = dm.ProjExecNonQry(sExec)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            BillCancel = SaveStatus
            sExec = Nothing
        End Try
    End Function


    Public Function GetConsignmentSalesHeaderByInvoiceNo(ByVal sInvoiceNo As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "SELECT *,BDate = Convert(char(8),InvoiceDate,112) FROM tblConsignmentSalesHeader WHERE InvoiceNo =" & fncFormatSQLText(sInvoiceNo)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Invoice", "Invoice")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetConsignmentSalesHeaderByInvoiceNo = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetConsignmentSalesDetailByInvoiceNo(ByVal sInvoiceNo As String) As DataSet

        Dim Ds As DataSet
        Dim sQry As String

        Try
            GetConsignmentSalesDetailByInvoiceNo = Nothing
            sQry = "SELECT * FROM tblConsignmentSalesDetail WHERE InvoiceNo =" & fncFormatSQLText(sInvoiceNo)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "SalesDetail", "SalesDetail")

            GetConsignmentSalesDetailByInvoiceNo = Ds
            Ds = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            Ds = Nothing
        End Try
    End Function

    Public Function GetCustomerPrice(ByVal sinventorycode As String) As DataSet

        Dim ds As DataSet
        Dim sQry As String

        Try
            GetCustomerPrice = Nothing

            sQry = "Select * from tblConsignmentSalesDetail where InventoryCode = " & fncFormatSQLText(sinventorycode)

            ds = dm.getDataSetWithSqlAdapter(sQry, "SalesDetail", "SalesDetail")
            GetCustomerPrice = ds
            ds = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try

    End Function

    Public Function GetConsignmentSalesReturnHeaderByRefNo(ByVal sRefNo As String) As DataSet

        Dim Ds As DataSet
        Dim sQry As String
        Try
            GetConsignmentSalesReturnHeaderByRefNo = Nothing
            sQry = "SELECT * FROM tblConsignmentSalesReturnHeader WHERE RefNo =" & fncFormatSQLText(sRefNo)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "SalesReturn", "SalesReturn")
            GetConsignmentSalesReturnHeaderByRefNo = Ds
            Ds = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Function

    Public Function GetConsignmentSalesReturnHeader(ByVal sBillNo As String) As DataSet

        Dim ds As DataSet
        Dim sQry As String

        Try
            GetConsignmentSalesReturnHeader = Nothing
            sQry = "SELECT * FROM tblConsignmentSalesReturnHeader WHERE InvoiceNo =" & fncFormatSQLText(sBillNo)
            sQry = sQry & " or RefNo = " & fncFormatSQLText(sBillNo)

            ds = dm.getDataSetWithSqlAdapter(sQry, "SalesReturn", "SalesReturn")
            GetConsignmentSalesReturnHeader = ds
            ds = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Function

    Public Function GetConsignmentSalesReturnHeaderByCashier(ByVal sDate As String, ByVal sSalesManCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "Select SalesManCode, ReturnAmount=Sum(NetTotal) from tblConsignmentSalesReturnHeader where RefNo like '" & Trim$(gthisLocation) & "02%' and " & _
                " convert(char(8), InvoiceDate, 112) = convert(char(8), " & Trim$(sDate) & ", 112) Group By SalesManCode"
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Invoice", "Invoice")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetConsignmentSalesReturnHeaderByCashier = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function


End Class
