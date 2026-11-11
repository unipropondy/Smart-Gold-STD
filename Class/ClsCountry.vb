Public Class ClsCountry
    Dim dm As New DataManager
    Public Function CountryDelete(ByVal sCode As String) As Boolean
        Dim sExec As String
        Try
            CountryDelete = False
            sExec = "BEGIN TRANSACTION" & vbCrLf
            sExec = sExec & "DELETE FROM tblCountry WHERE CountryCode=" & fncFormatSQLText(sCode) & vbCrLf
            sExec = sExec & "COMMIT TRANSACTION" & vbCrLf
            dm.ProjExecNonQry(sExec)
            CountryDelete = True
            Exit Function
        Catch ex As Exception
            CountryDelete = False
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Function
    Public Function fncSaveCountry(ByVal sCode As String, ByVal sName As String, _
                                   ByVal sType As String, ByVal sUserId As String) As Boolean
        Try
            Dim sExec As String
            fncSaveCountry = False
            sExec = "Begin Transaction" & vbCrLf
            sExec = sExec & "EXEC sp_Country " & fncFormatSQLText(sCode) & ","
            sExec = sExec & fncFormatSQLText(sName) & ","
            sExec = sExec & fncFormatSQLText(sType) & ","
            sExec = sExec & fncFormatSQLText(sUserId) & vbCrLf
            sExec = sExec & "Commit Transaction" & vbCrLf
            fncSaveCountry = dm.ProjExecNonQry(sExec)
            Exit Function
        Catch ex As Exception
            fncSaveCountry = False
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Function
    Public Function GetCountryDetail(ByVal sCode As String) As DataSet
        Dim ds As New DataSet
        Dim sSql As String

        If sCode.Length = 0 Then
            sSql = "select * from tblCountry order by countryCode"
        Else
            sSql = "SELECT * FROM tblCountry WHERE countryCode =" & fncFormatSQLText(sCode)

        End If
        Try
            ds = dm.getDataSetWithSqlAdapter(sSql, "tblCountry", "tblCountry")
            GetCountryDetail = ds
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
