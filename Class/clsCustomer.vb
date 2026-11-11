Public Class clsCustomer
    Dim dm As New DataManager
    Public Function GetCustomer() As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select * from tblCustomer "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Customer", "Customer")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetCustomer = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetCustomerwithCode(ByVal sCustomercode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select * from tblCustomer where CustomerCode=" & fncFormatSQLText(sCustomercode)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Customer", "Customer")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetCustomerwithCode = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetCustomerwithCodeHQ(ByVal sCustomercode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select * from tblCustomer where CustomerCode=" & fncFormatSQLText(sCustomercode)
            Ds = dm.getDataSetWithSqlAdapterAcc(sQry, "Customer", "Customer")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetCustomerwithCodeHQ = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetCustomerPointswithCodeHQ(ByVal sCustomercode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select sum(RewardPoints)RewardPoints,sum(RedeemPoints)RedeemedPoints from tblmembertransactionaj where CustomerCode=" & fncFormatSQLText(sCustomercode)
            Ds = dm.getDataSetWithSqlAdapterAcc(sQry, "Customer", "Customer")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetCustomerPointswithCodeHQ = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function DeleteCustomer(ByVal sCustomerCode As String) As Boolean
        Dim sQry As String
        Dim DelStatus As Boolean
        Try
            sQry = " Delete from tblCustomer where CustomerCode = " & fncFormatSQLText(sCustomerCode)
            DelStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            DelStatus = False
        Finally
            DeleteCustomer = DelStatus
            sQry = Nothing
        End Try
    End Function
    Public Function GetCustomerImage(ByVal sCustomerCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select isnull(CustomerImage,'')CustomerImage from tblCustomer  where CustomerCode =" & fncFormatSQLText(sCustomerCode)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Customer", "Customer")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetCustomerImage = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function CustomerImageSave(ByVal sCustomerCode As String, _
                              ByVal InvImage() As Byte, ByVal sImageName As String, _
                              ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim Status As Boolean
        sCreateInventory = ""
        Try

            Dim Cmd As New SqlClient.SqlCommand("sp_CustomerImageSave", gsConBatchPosting)
            Cmd.Transaction = gsTrans
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.Parameters.Add("@CustomerCode", SqlDbType.VarChar)
            Cmd.Parameters("@CustomerCode").Value = sCustomerCode
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
            CustomerImageSave = False
        Finally
            Status = True
            CustomerImageSave = True
        End Try
    End Function
    Public Function CustomerSave(ByVal sCustomerCode As String, ByVal sCustomerName As String, ByVal sAddress1 As String, ByVal sAddress2 As String, _
                                 ByVal sAddress3 As String, ByVal sAddress4 As String, ByVal sPhone1 As String, ByVal sPhone2 As String, ByVal sMobile As String, _
                                 ByVal sFax As String, ByVal sEmail As String, ByVal sURL As String, _
                                 ByVal sTerms As String, ByVal sAccountType As String, ByVal GLAccount1 As String, ByVal GLAccount2 As String, ByVal sRemarks As String, _
                                 ByVal sNricno As String, ByVal sPassPortNo As String, ByVal sDOB As String, ByVal sCustomerType As String, ByVal sDateFor As String, _
                                 ByVal sGender As String, ByVal sDOA As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_Customer " & fncFormatSQLText(sCustomerCode) & ","
            sQry = sQry & fncFormatSQLText(sCustomerName) & ","
            sQry = sQry & fncFormatSQLText(sAddress1) & ","
            sQry = sQry & fncFormatSQLText(sAddress2) & ","
            sQry = sQry & fncFormatSQLText(sAddress3) & ","
            sQry = sQry & fncFormatSQLText(sAddress4) & ","
            sQry = sQry & fncFormatSQLText(sPhone1) & ","
            sQry = sQry & fncFormatSQLText(sPhone2) & ","
            sQry = sQry & fncFormatSQLText(sMobile) & ","
            sQry = sQry & fncFormatSQLText(sFax) & ","
            sQry = sQry & fncFormatSQLText(sEmail) & ","
            sQry = sQry & fncFormatSQLText(sURL) & ","
            sQry = sQry & fncFormatSQLText(sTerms) & ","
            sQry = sQry & fncFormatSQLText(sAccountType) & ","
            sQry = sQry & fncFormatSQLText(GLAccount1) & ","
            sQry = sQry & fncFormatSQLText(GLAccount2) & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & fncFormatSQLText(sNricno) & ","
            sQry = sQry & fncFormatSQLText(sPassPortNo) & ","
            sQry = sQry & fncFormatSQLDate(sDOB) & ","
            sQry = sQry & fncFormatSQLText(sCustomerType) & ","
            sQry = sQry & fncFormatSQLText(sDateFor) & ","
            sQry = sQry & fncFormatSQLText(sGender) & ","
            sQry = sQry & fncFormatSQLDate(sDOA) & ","
            sQry = sQry & fncFormatSQLText(sUserID)
            SaveStatus = dm.ProjExecNonQryAcc(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            CustomerSave = SaveStatus
            sQry = Nothing
        End Try
    End Function
    Public Function GetCustomerWithCustomerType(ByVal cCustomerType As String, ByVal sCustomerCode As String, ByVal dFromPoints As Integer, ByVal dToPoints As Integer) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select CustomerCode,CustomerName,TotalRewardPoints from tblcustomer "
            sQry = sQry & " where CustomerType = " & fncFormatSQLText(cCustomerType)
            If Trim(sCustomerCode) <> "" Then
                sQry = sQry & " and CustomerCode = " & fncFormatSQLText(sCustomerCode)
            End If
            If Val(dFromPoints) > 0 And Val(dToPoints) > 0 Then
                sQry = sQry & " and TotalRewardPoints between " & Val(dFromPoints) & " and " & Val(dToPoints)
            End If
            Ds = dm.getDataSetWithSqlAdapterAcc(sQry, "Customer", "Customer")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetCustomerWithCustomerType = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    'Public Function GetSalesByCustomer(ByVal sCustomerCode As String) As DataSet
    '    Dim Ds As New DataSet
    '    Dim sQry As String
    '    Try
    '        sQry = "Select InvoiceNo, InvoiceDate, TotalWeight, TotalPCS, NetTotal from tblSalesHeader " & _
    '                               "where CustomerCode = " & fncFormatSQLText(sCustomerCode) & " Order By InvoiceDate Desc"
    '        Ds = dm.getDataSetWithSqlAdapter(sQry, "Customer", "Customer")
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '        Ds = Nothing
    '    Finally
    '        GetSalesByCustomer = Ds
    '        Ds = Nothing
    '        sQry = Nothing
    '    End Try

    'End Function
    Public Function GetRewardPointsByCustomer(ByVal sCustomerCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "Select InvoiceNo, InvoiceDate, InvoiceAmount, RewardPoints, RedeemPoints,ExpiryDate,Remarks,Redeemed,InvoiceRedeemedPoints from tblMemberTransactionaj " & _
                                   "where CustomerCode = " & fncFormatSQLText(sCustomerCode) & " Order By InvoiceDate Desc"
            Ds = dm.getDataSetWithSqlAdapterAcc(sQry, "Customer", "Customer")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetRewardPointsByCustomer = Ds
            Ds = Nothing
            sQry = Nothing
        End Try

    End Function
    Public Function GetsumRewardPointsByCustomer(ByVal sCustomerCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "Select CustomerCode,sum(RewardPoints)RewardPoints, Sum(RedeemPoints)RedeemPoints from tblMemberTransactionaj " & _
                                   "where CustomerCode = " & fncFormatSQLText(sCustomerCode) & " group by CustomerCode"
            Ds = dm.getDataSetWithSqlAdapterAcc(sQry, "Customer", "Customer")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetsumRewardPointsByCustomer = Ds
            Ds = Nothing
            sQry = Nothing
        End Try

    End Function
    Public Function GetsumRedeemedPointsByCustomer(ByVal sCustomerCode As String, ByVal sDate As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "Select CustomerCode,Sum(RedeemPoints)RedeemPoints from tblMemberTransactionaj " & _
                                   "where CustomerCode = " & fncFormatSQLText(sCustomerCode) & " and Invoicedate= " & fncFormatSQLDate(sDate) & " group by CustomerCode"
            Ds = dm.getDataSetWithSqlAdapterAcc(sQry, "Customer", "Customer")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetsumRedeemedPointsByCustomer = Ds
            Ds = Nothing
            sQry = Nothing
        End Try

    End Function
    Public Function GetSalesDetailByInvoiceNo(ByVal sInvoiceNo As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "Select InventoryCode, Description, Weight, PCS, NetAmount From tblSalesDetail where InvoiceNo = " & _
                    fncFormatSQLText(sInvoiceNo)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Customer", "Customer")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetSalesDetailByInvoiceNo = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function CustomerPointAdjustmentSave(ByVal sCustomerCode As String, ByVal sCustomerType As String, ByVal sCustomerName As String, ByVal sPreviousPoints As String, ByVal sAdjustmentPoints As String, ByVal sInvoiceNo As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean

        Try
            sQry = "EXEC sp_CustomerPointsAdjustment " & fncFormatSQLText(sCustomerCode) & ","
            sQry = sQry & fncFormatSQLText(sCustomerName) & ","
            sQry = sQry & fncFormatSQLText(sCustomerType) & ","
            sQry = sQry & fncFormatSQLText(sPreviousPoints) & ","
            sQry = sQry & fncFormatSQLText(sAdjustmentPoints) & ","
            sQry = sQry & fncFormatSQLText(sInvoiceNo) & ","
            sQry = sQry & fncFormatSQLText(sUserID) & ","
            sQry = sQry & fncFormatSQLText(sUserID)
            SaveStatus = dm.ProjExecNonQryAcc(sQry)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            ' SaveStatus = False
        Finally
            CustomerPointAdjustmentSave = SaveStatus
            sQry = Nothing
        End Try

    End Function
    Public Function CustomerTypeChange(ByVal sCustomerCode As String, ByVal sCustomerName As String, ByVal sTotalPoints As String, ByVal sOldCustomerType As String, ByVal sNewCustomerType As String, ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean

        Try
            sQry = "EXEC sp_CustomerTypeChangeLog" & fncFormatSQLText(sCustomerCode) & ","
            sQry = sQry & fncFormatSQLText(sCustomerName) & ","
            sQry = sQry & Val(sTotalPoints) & ","
            sQry = sQry & fncFormatSQLText(sOldCustomerType) & ","
            sQry = sQry & fncFormatSQLText(sNewCustomerType) & ","
            sQry = sQry & fncFormatSQLText(sUserId) & ","
            sQry = sQry & fncFormatSQLText(sUserId)
            SaveStatus = dm.ProjExecNonQryAcc(sQry)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            ' SaveStatus = False
        Finally
            CustomerTypeChange = SaveStatus
            sQry = Nothing
        End Try

    End Function

    Public Function GetSalesByCustomer(ByVal sCustomerCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "Select InvoiceNo, CONVERT(VARCHAR(10), InvoiceDate, 105)InvoiceDate, TotalWeight, TotalPCS, NetTotal from tblSalesHeader " & _
                                   "where CustomerCode = " & fncFormatSQLText(sCustomerCode) & " Order By InvoiceDate Desc"
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Customer", "Customer")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetSalesByCustomer = Ds
            Ds = Nothing
            sQry = Nothing
        End Try

    End Function

    Public Function fncCheckInvoiceNo(ByVal sInvoiceNo As String) As Boolean
        Dim sSql As String
        Dim Ds As New DataSet
        Try
            sSql = "select InvoiceNo from tblCustomersPointAdjustment where InvoiceNo= " & fncFormatSQLText(sInvoiceNo)

            Ds = dm.getDataSetWithSqlAdapter(sSql, "InvoiceNo", "InvoiceNo")
            If Ds.Tables("InvoiceNo").Rows.Count = 0 Then
                fncCheckInvoiceNo = False
            Else
                fncCheckInvoiceNo = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Function
    Public Function GetCustomerwithTypeHQ(ByVal sCustomercode As String, ByVal sCustomertype As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select * from tblCustomer where CustomerCode=" & fncFormatSQLText(sCustomercode)
            sQry = sQry & " and CustomerType=" & fncFormatSQLText(sCustomertype)
            Ds = dm.getDataSetWithSqlAdapterAcc(sQry, "Customer", "Customer")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetCustomerwithTypeHQ = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function CustomerTypeChange(ByVal sCustomerCode As String, ByVal sCustomerName As String, _
                                      ByVal sTotalPoints As String, ByVal sOldCustomerType As String, _
                                      ByVal sNewCustomerType As String, ByVal dFPoints As Integer, ByVal dTPoints As Integer, ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean

        Try
            sQry = "EXEC sp_CustomerTypeChangeLog" & fncFormatSQLText(sCustomerCode) & ","
            sQry = sQry & fncFormatSQLText(sCustomerName) & ","
            sQry = sQry & Val(sTotalPoints) & ","
            sQry = sQry & fncFormatSQLText(sOldCustomerType) & ","
            sQry = sQry & fncFormatSQLText(sNewCustomerType) & ","
            sQry = sQry & Val(dFPoints) & ","
            sQry = sQry & Val(dTPoints) & ","
            'sQry = sQry & Val(dNewTotalPoints) & ","
            sQry = sQry & fncFormatSQLText(sUserId) & ","
            sQry = sQry & fncFormatSQLText(sUserId)
            SaveStatus = dm.ProjExecNonQryAcc(sQry)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            ' SaveStatus = False
        Finally
            CustomerTypeChange = SaveStatus
            sQry = Nothing
        End Try

    End Function
    Public Sub Customer180ExpiryUpdate()
        Dim SaveStatus As Boolean
        Dim sQry As String

        Try
            sQry = "EXEC sp_CustomerUpdate180"
            SaveStatus = dm.ProjExecNonQryAcc(sQry)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            ' SaveStatus = False
        Finally
            sQry = Nothing
        End Try

    End Sub
    Public Sub Customer270ExpiryUpdate()
        Dim SaveStatus As Boolean
        Dim sQry As String

        Try
            sQry = "EXEC sp_CustomerUpdate270"
            SaveStatus = dm.ProjExecNonQryAcc(sQry)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            ' SaveStatus = False
        Finally
            sQry = Nothing
        End Try

    End Sub
End Class
