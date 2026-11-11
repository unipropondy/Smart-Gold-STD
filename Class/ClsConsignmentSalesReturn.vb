Public Class ClsConsignmentSalesReturn
    Dim dm As New DataManager

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

    

End Class
