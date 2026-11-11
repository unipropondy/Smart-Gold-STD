Public Class clsMisc
    Dim dm As New DataManager
    Public Function MiscSave(ByVal sType As String, ByVal iVouCherNo As Integer, ByVal sDate As String, ByVal sPerson As String, _
                            ByVal dAmount As Decimal, ByVal dGSTPerc As Decimal, ByVal dGST As Decimal, ByVal dNetAmount As Decimal, _
                            ByVal dWeight As Decimal, ByVal sRemarks As String) As Boolean

        Dim sQry As String
        Dim SaveStatus As Boolean
        Try

            sQry = "EXEC sp_Misc " & fncFormatSQLText(sType) & ","
            sQry = sQry & fncFormatSQLText(iVouCherNo) & ","
            sQry = sQry & fncFormatSQLDate(sDate) & ","
            sQry = sQry & fncFormatSQLText(sPerson) & ","
            sQry = sQry & fncFormatSQLText(dAmount) & ","
            sQry = sQry & fncFormatSQLText(dGSTPerc) & ","
            sQry = sQry & fncFormatSQLText(dGST) & ","
            sQry = sQry & fncFormatSQLText(dNetAmount) & ","
            sQry = sQry & fncFormatSQLText(dWeight) & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & fncFormatSQLText(sUserID)
            SaveStatus = dm.ProjExecNonQry(sQry)


        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            MiscSave = SaveStatus
            sQry = Nothing
        End Try
    End Function


    Public Function DeleteMisc(ByVal sType As String, ByVal iVoucherNo As Integer) As Boolean
        Dim sQry As String
        Dim DelStatus As Boolean
        'Dim sType As String
        Try
            sQry = " Delete from tblMisc where Type = " & fncFormatSQLText(sType) & " and VoucherNo=" & fncFormatSQLText(iVoucherNo)
            DelStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            DelStatus = False
        Finally
            DeleteMisc = DelStatus
            sQry = Nothing
        End Try
    End Function
End Class
