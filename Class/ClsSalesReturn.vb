Public Class ClsSalesReturn
    Dim dm As New DataManager

    Public Function BillCancel(ByVal sInvoiceNo As String, ByVal sLocation As String, _
                        ByVal sUserId As String) As Boolean
        Dim sExec As String
        Dim SaveStatus As Boolean
        Try

            sExec = "EXEC sp_SalesBillCancelCustomerUpdate " & fncFormatSQLText(sInvoiceNo) 

            SaveStatus = dm.ProjExecNonQryAcc(sExec)

            sExec = "EXEC sp_SalesBillCancel " & fncFormatSQLText(sInvoiceNo) & ","
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
    Public Function RFIDItemStatusUpdate(ByVal sInventorycode As String) As Boolean
        Dim bStatus As Boolean
        Dim sQry As String
        Try
            sQry = "update Item set ToDisplayInStockTake = '1',status='SR' Where ItemCode=" & fncFormatSQLText(sInventorycode)
            bStatus = ProjExecNonQryRFID(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            RFIDItemStatusUpdate = bStatus
            sQry = Nothing
        End Try
    End Function
    Public Function GetSalesHeaderByInvoiceNo(ByVal sInvoiceNo As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            'sQry = "SELECT *,BDate = Convert(char(8),InvoiceDate,112) FROM tblSalesHeader WHERE InvoiceNo =" & fncFormatSQLText(sInvoiceNo)
            sQry = "SELECT *,BDate= InvoiceDate FROM tblSalesHeader WHERE InvoiceNo =" & fncFormatSQLText(sInvoiceNo)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Invoice", "Invoice")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetSalesHeaderByInvoiceNo = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GeteTRSStatementInvoiceNo(ByVal sInvoiceNo As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            'sQry = "SELECT *,BDate = Convert(char(8),InvoiceDate,112) FROM tblSalesHeader WHERE InvoiceNo =" & fncFormatSQLText(sInvoiceNo)
            sQry = "SELECT BillNo,BillDate,SalesmanCode,PassPortNo,Gst,NetAmount,DocId,eTRSIssueDate,eTRSIssueTime,"
            sQry = sQry & " Remarks,CreateUser,CreateDate,ModifyUser,ModifyDate,IsSalesAbove700,IsSealBag,SealedBagNo,"
            sQry = sQry & " eTRSStaffName,StaffSign,CustomerPhoto,CustomerImageName FROM tbleTRSStatement WHERE BillNo =" & fncFormatSQLText(sInvoiceNo)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Invoice", "Invoice")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GeteTRSStatementInvoiceNo = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetSalesHeaderByPassportCount(ByVal sPassportNo As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            'sQry = "SELECT *,BDate = Convert(char(8),InvoiceDate,112) FROM tblSalesHeader WHERE InvoiceNo =" & fncFormatSQLText(sInvoiceNo)
            sQry = "SELECT Count(*)Count FROM tblSalesHeader WHERE Remarks =" & fncFormatSQLText(sPassportNo) & "  group by remarks "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Invoice", "Invoice")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetSalesHeaderByPassportCount = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetCreditSalesHeaderByInvoiceNo(ByVal sInvoiceNo As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            'sQry = "SELECT *,BDate = Convert(char(8),InvoiceDate,112) FROM tblSalesHeader WHERE InvoiceNo =" & fncFormatSQLText(sInvoiceNo)
            sQry = "SELECT *,BDate= InvoiceDate FROM tblCreditSalesHeader WHERE InvoiceNo =" & fncFormatSQLText(sInvoiceNo)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Invoice", "Invoice")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetCreditSalesHeaderByInvoiceNo = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function GetSalesReturnHeaderByCashier(ByVal sDate As String, ByVal sSalesManCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "Select SalesManCode, ReturnAmount=Sum(NetTotal) from tblSalesReturnHeader where RefNo like '" & Trim$(gthisLocation) & "02%' and " & _
                " convert(char(8), InvoiceDate, 112) = convert(char(8), " & Trim$(sDate) & ", 112) Group By SalesManCode"
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Invoice", "Invoice")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetSalesReturnHeaderByCashier = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function GetDepositDetails(ByVal sCustomerCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            'sQry = "SELECT *,BDate = Convert(char(8),InvoiceDate,112) FROM tblSalesHeader WHERE InvoiceNo =" & fncFormatSQLText(sInvoiceNo)
            'sQry = "SELECT * FROM tblDeposit WHERE SalesType='DEPOSIT' and  CustomerCode =" & fncFormatSQLText(sCustomerCode)
            'sQry = sQry & " and DepositUsed='N'"
            sQry = "SELECT * FROM tblDepositSave WHERE CustomerCode =" & fncFormatSQLText(sCustomerCode)
            sQry = sQry & " and DepositUsed='N'"

            Ds = dm.getDataSetWithSqlAdapter(sQry, "Deposit", "Deposit")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetDepositDetails = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetDepositTradeInDetails(ByVal sCustomerCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            'sQry = "SELECT *,BDate = Convert(char(8),InvoiceDate,112) FROM tblSalesHeader WHERE InvoiceNo =" & fncFormatSQLText(sInvoiceNo)
            sQry = "SELECT * FROM tblDeposit WHERE SalesType='TRADEIN' and  CustomerCode =" & fncFormatSQLText(sCustomerCode)
            sQry = sQry & " and DepositUsed='N'"
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Deposit", "Deposit")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetDepositTradeInDetails = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function eTRSStatementDelete(ByVal sInvoiceNo As String, ByVal sRemarks As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try

            sQry = " Update tbleTRSStatement Set Remarks=" & fncFormatSQLText(sRemarks) & " where BillNo = " & fncFormatSQLText(sInvoiceNo)
            SaveStatus = dm.ProjExecNonQry(sQry)

            sQry = " insert into tbleTRSStatementVoid Select * from tbleTRSStatement where BillNo = " & fncFormatSQLText(sInvoiceNo)
            SaveStatus = dm.ProjExecNonQry(sQry)

            sQry = "Delete from [tbleTRSStatement] where BillNo=" & fncFormatSQLText(sInvoiceNo)
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            eTRSStatementDelete = SaveStatus
            sQry = Nothing
        End Try
    End Function

    Public Function eTRSStatementImageSave(ByVal sinventorycode As String, _
                              ByVal InvImage() As Byte, ByVal sImageName As String, _
                              ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim Status As Boolean
        sCreateInventory = ""
        Try

            Dim Cmd As New SqlClient.SqlCommand("sp_eTRSStatementImageSave", gsConBatchPosting)
            Cmd.Transaction = gsTrans
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.Parameters.Add("@InvoiceNo", SqlDbType.VarChar)
            Cmd.Parameters("@InvoiceNo").Value = sinventorycode
            Cmd.Parameters.Add("@CustomerImage", SqlDbType.Image)
            Cmd.Parameters("@CustomerImage").Value = InvImage
            Cmd.Parameters.Add("@CustomerImageName", SqlDbType.VarChar)
            Cmd.Parameters("@CustomerImageName").Value = sImageName
            Cmd.Parameters.Add("@UserID", SqlDbType.VarChar)
            Cmd.Parameters("@UserID").Value = sUserId
            Cmd.ExecuteNonQuery()
            'sCreateInventory = Cmd.ExecuteScalar

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            eTRSStatementImageSave = False
        Finally
            Status = True
            eTRSStatementImageSave = True
        End Try
    End Function
    Public Function GetCustomerImage(ByVal sInvoiceno As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select isnull(CustomerPhoto,'')CustomerPhoto from tbletrsStatement  where BillNo =" & fncFormatSQLText(sInvoiceno)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Inventory", "Inventory")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetCustomerImage = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
#Region "Added by Murugan"

    Public Function GetSalesReturnHeader(ByVal sBillNo As String) As DataSet

        Dim ds As DataSet
        Dim sQry As String

        Try
            GetSalesReturnHeader = Nothing
            sQry = "SELECT * FROM tblSalesReturnHeader WHERE InvoiceNo =" & fncFormatSQLText(sBillNo)
            sQry = sQry & " or RefNo = " & fncFormatSQLText(sBillNo)

            ds = dm.getDataSetWithSqlAdapter(sQry, "SalesReturn", "SalesReturn")
            GetSalesReturnHeader = ds
            ds = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Function

    Public Function GetSalesDetailByInvoiceNo(ByVal sInvoiceNo As String) As DataSet

        Dim Ds As DataSet
        Dim sQry As String

        Try
            GetSalesDetailByInvoiceNo = Nothing
            sQry = "SELECT * FROM tblSalesDetail WHERE InvoiceNo =" & fncFormatSQLText(sInvoiceNo) & " and  left(inventorycode,2)<>'TI'"
            Ds = dm.getDataSetWithSqlAdapter(sQry, "SalesDetail", "SalesDetail")

            GetSalesDetailByInvoiceNo = Ds
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

            sQry = "Select * from tblSalesDetail where InventoryCode = " & fncFormatSQLText(sinventorycode)

            ds = dm.getDataSetWithSqlAdapter(sQry, "SalesDetail", "SalesDetail")
            GetCustomerPrice = ds
            ds = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try

    End Function

    Public Function GetSalesReturnHeaderByRefNo(ByVal sRefNo As String) As DataSet

        Dim Ds As DataSet
        Dim sQry As String
        Try
            GetSalesReturnHeaderByRefNo = Nothing
            sQry = "SELECT * FROM tblSalesReturnHeader WHERE RefNo =" & fncFormatSQLText(sRefNo)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "SalesReturn", "SalesReturn")
            GetSalesReturnHeaderByRefNo = Ds
            Ds = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Function
    Public Shared Function ProjExecNonQryRFID(ByVal Query As String) As Boolean
        ProjExecNonQryRFID = True
        Try
            Dim Cn As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(gsConRFID)
            Cn.Open()
            Dim Cmd As New SqlClient.SqlCommand(Query)
            Cmd.Connection = Cn
            Cmd.CommandTimeout = 600
            Cmd.ExecuteNonQuery()
            Cn.Close()
            Cn.Dispose()
            Cn = Nothing
        Catch ex As Exception
            Try
                MsgBox("Error in Exec Record")
                ProjExecNonQryRFID = False
            Catch
            End Try
            Throw
            MsgBox("unexpected error")
            ProjExecNonQryRFID = False
        End Try
    End Function

#End Region

End Class
