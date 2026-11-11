Public Class clsReceiptToOthers
    Dim Dm As New DataManager
    Public Function fnFindNextReceiptNo() As String
        On Error GoTo errTrap
        Dim NextNo As String
        Dim NextRNo As String
        NextNo = Dm.ValReturnProjExecNonQryAcc("Select NextReciptNo from tblCompanyAR where CompanyID='" & Trim(CompId) & "'")
        If Val(NextNo) <> 0 Then
            NextRNo = Trim(sLocationCode) & "-R" & Format(Val(NextNo + 1), "0000000")
        Else
            NextRNo = Trim(sLocationCode) & "-R0000001"
        End If
        fnFindNextReceiptNo = NextRNo
        Exit Function
errTrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Function
    Public Function fnSaveReceiptOtherHeader(ByVal ReceiptNo As String, ByVal VoucherRef As String, _
                                        ByVal ReceiptDate As String, ByVal ReceiptType As String, _
                                        ByVal Department As String, ByVal AccountCode As String, _
                                        ByVal AccountName As String, ByVal Description As String, _
                                        ByVal TotalAmount As Double, ByVal CurrCode As String, _
                                        ByVal CurrRate As Double, ByVal Post As String, _
                                        ByVal PostBy As String, ByVal JournalNo As String, _
                                        ByVal PostedDate As String, ByVal CompanyCode As String, _
                                        ByVal CreatedBy As String, ByVal CreatedDate As String, ByVal ModifiedBy As String, _
                                        ByVal Modifieddate As String, ByVal Mode As String) As String

        On Error GoTo errtrap
        Dim SqlQry As String
        SqlQry = "EXEC AR_ReceiptOtherHeader "
        SqlQry &= fncFormatSQLText(ReceiptNo) & ","
        SqlQry &= fncFormatSQLText(VoucherRef) & "," & fncFormatSQLDate(ReceiptDate) & ","
        SqlQry &= fncFormatSQLText(ReceiptType) & "," & fncFormatSQLText(Department) & "," & fncFormatSQLText(AccountCode) & ","
        SqlQry &= fncFormatSQLText(AccountName) & "," & fncFormatSQLText(Description) & ","
        SqlQry &= Val(TotalAmount) & "," & fncFormatSQLText(CurrCode) & ","
        SqlQry &= Val(CurrRate) & "," & fncFormatSQLText(Post) & "," & fncFormatSQLText(PostBy) & ","
        SqlQry &= fncFormatSQLText(JournalNo) & "," & fncFormatSQLDate(PostedDate) & "," & fncFormatSQLText(CompanyCode) & ","
        SqlQry &= fncFormatSQLText(CreatedBy) & "," & fncFormatSQLDate(CreatedDate) & "," & fncFormatSQLText(ModifiedBy) & ","
        SqlQry &= fncFormatSQLDate(Modifieddate) & "," & fncFormatSQLText(Mode)
        'Dm.ReturnProjExecNonQry(SqlQry)
        If Mode = "I" Then
            fnSaveReceiptOtherHeader = Dm.ReturnProjExecNonQryAcc(SqlQry)
        Else
            If Dm.ProjExecNonQryAcc(SqlQry) = False Then
                Return "F"
            Else
                Return "T"
            End If
        End If
        Exit Function
errtrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Function
    Function fnSaveReceiptOtherTransaction(ByVal ReceiptNo As String, ByVal SlNo As Integer, _
                                    ByVal AccountNo As String, ByVal Description As String, _
                                    ByVal Department As String, ByVal Memo As String, ByVal DocRef As String, ByVal DocDate As String, _
    ByVal BankName As String, ByVal Branch As String, ByVal CurCode As String, ByVal CurRate As Double, ByVal Amount As Double, _
                                    ByVal GST As Double, ByVal CreatedBy As String, ByVal CreatedDate As String, _
                                    ByVal ModifiedBy As String, ByVal ModifiedDate As String) As String
        On Error GoTo errtrap
        Dim SqlQry As String
        SqlQry = "EXEC AR_ReceiptOtherTransaction"
        SqlQry &= fncFormatSQLText(ReceiptNo) & "," & Val(SlNo) & ","
        SqlQry &= fncFormatSQLText(AccountNo) & "," & fncFormatSQLText(Description) & ","
        SqlQry &= fncFormatSQLText(Department) & "," & fncFormatSQLText(Memo) & "," & fncFormatSQLText(DocRef) & ","
        SqlQry &= fncFormatSQLDate(DocDate) & "," & fncFormatSQLText(BankName) & "," & fncFormatSQLText(Branch) & ","
        SqlQry &= fncFormatSQLText(CurCode) & "," & Val(CurRate) & "," & Val(Amount) & "," & Val(GST) & ","
        SqlQry &= fncFormatSQLText(CreatedBy) & "," & fncFormatSQLDate(CreatedDate) & "," & fncFormatSQLText(ModifiedBy) & ","
        SqlQry &= fncFormatSQLDate(ModifiedDate)
        Dm.ProjExecNonQryAcc(SqlQry)
        Exit Function
errtrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Function
    Public Function fnDeleteReceiptOtherHeader(ByVal RNo As String) As Boolean
        fnDeleteReceiptOtherHeader = False
        On Error GoTo errtrap
        Dm.ProjExecNonQryAcc("Exec AR_DelReceiptOtherHeader '" & RNo & "'")
        fnDeleteReceiptOtherHeader = True
        Exit Function
errtrap:
        MsgBox("Error in Delete AR Header Record" & Err.Description)
    End Function
    Public Function fnDeleteReceiptOtherTransactions(ByVal RNo As String) As Boolean
        fnDeleteReceiptOtherTransactions = False
        On Error GoTo errtrap
        Dm.ProjExecNonQryAcc("Exec AR_DelReceiptOtherTransactions '" & RNo & "'")
        fnDeleteReceiptOtherTransactions = True
        Exit Function
errtrap:
        MsgBox("Error in Delete AR transaction Record" & Err.Description)
    End Function
    Public Function NextREJournlNo() As String
        On Error GoTo errtrap
        Dim NextNo As String
        Dim NextJNo As String
        NextNo = Dm.ValReturnProjExecNonQryAcc("Select NextReceipt from tblParameters where CompanyID='" & Trim(CompId) & "'")
        If Val(NextNo) <> 0 Then
            NextJNo = "RE-" & Format(Val(NextNo), "00000000")
        Else
            NextJNo = "RE-00000001"
        End If
        NextREJournlNo = NextJNo
        Exit Function
errtrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Function
    Public Function UpdateJournalNo(ByVal RecNo As String, ByVal JounralNO As String) As Boolean
        On Error GoTo ErrTrap
        UpdateJournalNo = False
        UpdateJournalNo = Dm.ProjExecNonQryAcc("exec AR_UpdateOtherReceiptHeader " & fncFormatSQLText(RecNo) & "," & fncFormatSQLText(JounralNO))
        Exit Function
ErrTrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Function

    ' Batch 

    Public Function OpenBatchPostinConnection()
        'Dim X As System.Data.SqlClient.SqlDataReader
        gsConBatchPosting.ConnectionString = gsConAcc
        gsConBatchPosting.Open()
        ' Dim Cmd As New SqlClient.SqlCommand("Select * from tblParameters with (TABLOCKX)", gsConBatchPosting)
        ' X = Cmd.ExecuteReader()
    End Function
    Public Function gpBeginTransaction()
        gsTrans = gsConBatchPosting.BeginTransaction
    End Function
    Public Function gpCommitTransaction()
        gsTrans.Commit()
    End Function
    Public Function NonQryBatchPosting(ByVal Query As String) As Boolean
        On Error GoTo ErrTrap
        NonQryBatchPosting = False
        Dim Cmd As New SqlClient.SqlCommand(Query, gsConBatchPosting)
        Cmd.Transaction = gsTrans
        Cmd.ExecuteNonQuery()
        NonQryBatchPosting = True
        Exit Function
ErrTrap:
        NonQryBatchPosting = False
        MsgBox(Err.Description)
        Err.Clear()
    End Function
    Public Function CloseBatchConnection()
        gsConBatchPosting.Close()
    End Function
    Public Function gpRollBackBatchConnection()
        gsTrans.Rollback()
    End Function
    Public Function fnSaveReceiptOtherHeaderWithTran(ByVal ReceiptNo As String, ByVal VoucherRef As String, _
                                        ByVal ReceiptDate As String, ByVal ReceiptType As String, _
                                        ByVal Department As String, ByVal AccountCode As String, _
                                        ByVal AccountName As String, ByVal Description As String, _
                                        ByVal TotalAmount As Double, ByVal CurrCode As String, _
                                        ByVal CurrRate As Double, ByVal Post As String, _
                                        ByVal PostBy As String, ByVal JournalNo As String, _
                                        ByVal PostedDate As String, ByVal CompanyCode As String, _
                                        ByVal CreatedBy As String, ByVal CreatedDate As String, ByVal ModifiedBy As String, _
                                        ByVal Modifieddate As String, ByVal Mode As String) As String

        On Error GoTo ErrTrap
        Dim SqlQry As String
        SqlQry = "EXEC AR_ReceiptOtherHeader "
        SqlQry &= fncFormatSQLText(ReceiptNo) & ","
        SqlQry &= fncFormatSQLText(VoucherRef) & "," & fncFormatSQLDate(ReceiptDate) & ","
        SqlQry &= fncFormatSQLText(ReceiptType) & "," & fncFormatSQLText(Department) & "," & fncFormatSQLText(AccountCode) & ","
        SqlQry &= fncFormatSQLText(AccountName) & "," & fncFormatSQLText(Description) & ","
        SqlQry &= Val(TotalAmount) & "," & fncFormatSQLText(CurrCode) & ","
        SqlQry &= Val(CurrRate) & "," & fncFormatSQLText(Post) & "," & fncFormatSQLText(PostBy) & ","
        SqlQry &= fncFormatSQLText(JournalNo) & "," & fncFormatSQLDate(PostedDate) & "," & fncFormatSQLText(CompanyCode) & ","
        SqlQry &= fncFormatSQLText(CreatedBy) & "," & fncFormatSQLDate(CreatedDate) & "," & fncFormatSQLText(ModifiedBy) & ","
        SqlQry &= fncFormatSQLDate(Modifieddate) & "," & fncFormatSQLText(Mode)
        'Dm.ReturnProjExecNonQry(SqlQry)
        Dim Cmd As New SqlClient.SqlCommand(SqlQry, gsConBatchPosting)
        Cmd.Transaction = gsTrans
        fnSaveReceiptOtherHeaderWithTran = Cmd.ExecuteScalar()
        'fnSaveReceiptOtherHeaderWithTran = True
        Exit Function
ErrTrap:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
        Err.Clear()
    End Function
    Function fnSaveReceiptOtherTransactionWithTran(ByVal ReceiptNo As String, ByVal SlNo As Integer, _
                                   ByVal AccountNo As String, ByVal Description As String, _
                                   ByVal Department As String, ByVal Memo As String, ByVal DocRef As String, ByVal DocDate As String, _
                                   ByVal BankName As String, ByVal Branch As String, ByVal CurCode As String, ByVal CurRate As Double, ByVal Amount As Double, _
                                   ByVal GST As Double, ByVal CreatedBy As String, ByVal CreatedDate As String, _
                                   ByVal ModifiedBy As String, ByVal ModifiedDate As String) As Boolean
        On Error GoTo ErrTrap
        Dim SqlQry As String
        SqlQry = "EXEC AR_ReceiptOtherTransaction"
        SqlQry &= fncFormatSQLText(ReceiptNo) & "," & Val(SlNo) & ","
        SqlQry &= fncFormatSQLText(AccountNo) & "," & fncFormatSQLText(Description) & ","
        SqlQry &= fncFormatSQLText(Department) & "," & fncFormatSQLText(Memo) & "," & fncFormatSQLText(DocRef) & ","
        SqlQry &= fncFormatSQLDate(DocDate) & "," & fncFormatSQLText(BankName) & "," & fncFormatSQLText(Branch) & ","
        SqlQry &= fncFormatSQLText(CurCode) & "," & Val(CurRate) & "," & Val(Amount) & "," & Val(GST) & ","
        SqlQry &= fncFormatSQLText(CreatedBy) & "," & fncFormatSQLDate(CreatedDate) & "," & fncFormatSQLText(ModifiedBy) & ","
        SqlQry &= fncFormatSQLDate(ModifiedDate)
        Dim Cmd As New SqlClient.SqlCommand(SqlQry, gsConBatchPosting)
        Cmd.Transaction = gsTrans
        Cmd.ExecuteNonQuery()
        fnSaveReceiptOtherTransactionWithTran = True
        Exit Function
ErrTrap:
        fnSaveReceiptOtherTransactionWithTran = False
        MsgBox(Err.Description, MsgBoxStyle.Critical)
        Err.Clear()
    End Function
    Public Function fnDeleteReceiptOtherHeaderWithTran(ByVal RNo As String) As Boolean
        On Error GoTo errtrap
        fnDeleteReceiptOtherHeaderWithTran = False
        Dim Cmd As New SqlClient.SqlCommand("Exec AR_DelReceiptOtherHeader '" & RNo & "'", gsConBatchPosting)
        Cmd.Transaction = gsTrans
        Cmd.ExecuteNonQuery()
        Dim Cmd1 As New SqlClient.SqlCommand("Exec AR_DelReceiptOtherTransactions '" & RNo & "'", gsConBatchPosting)
        Cmd1.Transaction = gsTrans
        Cmd1.ExecuteNonQuery()
        fnDeleteReceiptOtherHeaderWithTran = True
        Exit Function
errtrap:
        fnDeleteReceiptOtherHeaderWithTran = True
    End Function
End Class
