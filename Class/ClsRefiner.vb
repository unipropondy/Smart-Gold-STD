Public Class ClsRefiner
    Dim dm As New DataManager
    Public Function RefinerDelete(ByVal sCode As String) As Boolean
        Dim sExec As String
        Try
            RefinerDelete = False
            sExec = "BEGIN TRANSACTION" & vbCrLf
            sExec = sExec & "DELETE FROM tblRefiner WHERE RefinerCode=" & fncFormatSQLText(sCode) & vbCrLf
            sExec = sExec & "COMMIT TRANSACTION" & vbCrLf
            dm.ProjExecNonQry(sExec)
            RefinerDelete = True
            Exit Function
        Catch ex As Exception
            RefinerDelete = False
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Function
    Public Function fncSaveRefiner(ByVal sCode As String, ByVal sName As String, _
                                 ByVal sType As String, ByVal sUserId As String) As Boolean
        Try
            Dim sExec As String
            fncSaveRefiner = False
            sExec = "Begin Transaction" & vbCrLf
            sExec = sExec & "EXEC sp_Refiner" & fncFormatSQLText(sCode) & ","
            sExec = sExec & fncFormatSQLText(sName) & ","
            sExec = sExec & fncFormatSQLText(sType) & ","
            sExec = sExec & fncFormatSQLText(sUserId) & vbCrLf
            sExec = sExec & "Commit Transaction" & vbCrLf
            fncSaveRefiner = dm.ProjExecNonQry(sExec)
            Exit Function
        Catch ex As Exception
            fncSaveRefiner = False
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Function
    Public Function GetRefinerDetail(ByVal sCode As String) As DataSet
        Dim ds As New DataSet
        Dim sSql As String

        If sCode.Length = 0 Then
            sSql = "select * from tblRefiner order by RefinerCode"
        Else
            sSql = "SELECT * FROM tblCountry WHERE RefinerCode =" & fncFormatSQLText(sCode)

        End If
        Try
            ds = dm.getDataSetWithSqlAdapter(sSql, "tblRefiner", "tblRefiner")
            GetRefinerDetail = ds
            ds = Nothing
            Exit Function
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
            ds = Nothing
        End Try
    End Function
End Class
