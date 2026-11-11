Public Class clsTransactionLog
    Public Function SaveTransactionLog(ByVal TranNumber As String, ByVal TranType As String, ByVal TranDate As String, ByVal Description As String, ByVal sQry As String) As Boolean
        On Error GoTo ErrTrap
        Dim sExec As String
        Dim DM As New DataManager
        Dim sCreateUser As String
        Dim sCreateDate As String
        Dim sModifyUser As String
        Dim sModifyDate As String
        Dim lAMount As Double
        Dim Dr() As DataRow
        Dim sStr As String
        SaveTransactionLog = False
        sStr = sQry
        Dr = DM.RowReturnProjExecNonQryWithDataRowAcc(sStr)
        If Dr.Length > 0 Then
            sCreateUser = Trim(Dr(0)(1) & "")
            sCreateDate = Trim(Dr(0)(2) & "")
            sModifyUser = Trim(Dr(0)(3) & "")
            sModifyDate = Trim(Dr(0)(4) & "")
            lAMount = Val(Trim(Dr(0)(0) & ""))
        Else
            sCreateUser = ""
            sCreateDate = ""
            sModifyUser = ""
            sModifyDate = ""
            lAMount = 0
        End If
        sExec = ""
        sExec &= " exec sp_TransactionLog "
        sExec &= fncFormatSQLText(TranNumber) & "," & fncFormatSQLText(TranType) & "," & fncFormatSQLDate(TranDate) & "," & fncFormatSQLText(Description) & "," & fncFormatSQLText(sUserID) & "," & fncFormatSQLText(sCreateUser) & "," & fncFormatSQLDate(sCreateDate) & "," & fncFormatSQLText(sModifyUser) & "," & fncFormatSQLDate(sModifyDate) & "," & lAMount
        DM.ProjExecNonQryAcc(sExec)
        SaveTransactionLog = True
        Exit Function
ErrTrap:
        SaveTransactionLog = False
    End Function

    Public Function SaveTransactionLogWithTran(ByVal TranNumber As String, ByVal TranType As String, ByVal TranDate As String, ByVal Description As String, ByVal sQry As String) As Boolean
        On Error GoTo ErrTrap
        Dim sExec As String
        Dim DM As New DataManager
        Dim sCreateUser As String
        Dim sCreateDate As String
        Dim sModifyUser As String
        Dim sModifyDate As String
        Dim lAMount As Double
        Dim Dr() As DataRow
        Dim sStr As String
        SaveTransactionLogWithTran = False
        sStr = sQry
        Dr = DM.RowReturnProjExecNonQryWithDataRowAcc(sStr)
        If Dr.Length > 0 Then
            sCreateUser = Trim(Dr(0)(1) & "")
            sCreateDate = Trim(Dr(0)(2) & "")
            sModifyUser = Trim(Dr(0)(3) & "")
            sModifyDate = Trim(Dr(0)(4) & "")
            lAMount = Val(Trim(Dr(0)(0) & ""))
        Else
            sCreateUser = ""
            sCreateDate = ""
            sModifyUser = ""
            sModifyDate = ""
            lAMount = 0
        End If
        sExec = ""
        sExec &= " exec sp_TransactionLog "
        sExec &= fncFormatSQLText(TranNumber) & "," & fncFormatSQLText(TranType) & "," & fncFormatSQLDate(TranDate) & "," & fncFormatSQLText(Description) & "," & fncFormatSQLText(sUserID) & "," & fncFormatSQLText(sCreateUser) & "," & fncFormatSQLDate(sCreateDate) & "," & fncFormatSQLText(sModifyUser) & "," & fncFormatSQLDate(sModifyDate) & "," & lAMount
        Dim Cmd As New SqlClient.SqlCommand(sExec, gsConBatchPosting)
        Cmd.Transaction = gsTrans
        Cmd.ExecuteNonQuery()
        SaveTransactionLogWithTran = True
        Exit Function
ErrTrap:
        SaveTransactionLogWithTran = False
    End Function
End Class
