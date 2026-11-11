Public Class clsScheme
    Dim DM As New DataManager
    Public Function GetScheme() As DataSet

        '-----
        Dim ds As New DataSet
        Dim sSql As String
        sSql = "SELECT * FROM tblSchemeMaster"
        ds = DM.getDataSetWithSqlAdapter(sSql, "getScheme", "getScheme")
        GetScheme = ds
        ds = Nothing
    End Function
    Public Function SchemeMasterDelete(ByVal sCode As String, ByVal sDescription As String) As Boolean
        Dim sQry As String
        Dim DelStatus As Boolean
        Try
            sQry = "Delete From tblSchemeMaster Where Code = " & fncFormatSQLText(sCode) & " and Description= " & fncFormatSQLText(sDescription)
            DelStatus = DM.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            DelStatus = False
        Finally
            SchemeMasterDelete = DelStatus
            sQry = Nothing
        End Try
    End Function
    Public Function SchemeMasterSave(ByVal sCode As String, ByVal sDescription As String, _
           ByVal sRemarks As String, ByVal sMode As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_SchemeMaster " & fncFormatSQLText(sCode) & ","
            sQry = sQry & fncFormatSQLText(sDescription) & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & fncFormatSQLText(sUserID) & ","
            sQry = sQry & fncFormatSQLText(sMode)
            SaveStatus = DM.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            SchemeMasterSave = SaveStatus
            sQry = Nothing
        End Try
    End Function
    Public Function SchemeDetailSave(ByVal sPayoutNo As String, ByVal sDate As String, ByVal sCode As String, ByVal sDescription As String, ByVal sPayOutDescription As String, _
             ByVal dAmount As String, ByVal sRemarks As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_SchemeDetailSave " & fncFormatSQLText(sPayoutNo) & "," & fncFormatSQLDate(sDate) & "," & fncFormatSQLText(sCode) & ","
            sQry = sQry & fncFormatSQLText(sDescription) & ","
            sQry = sQry & fncFormatSQLText(sPayOutDescription) & ","
            sQry = sQry & Val(dAmount) & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & fncFormatSQLText(sUserID)
            SaveStatus = DM.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            SchemeDetailSave = SaveStatus
            sQry = Nothing
        End Try
    End Function
    Public Function GetSchemeEntryNo() As String
        Dim Ds As New DataSet
        Dim sQry As String
        Dim sNextOrderNo As String
        Try
            sQry = " Select rtrim(locationcode) + Replicate('0',6-Len(Convert(char,NextPayOutNo)))+ convert(char,NextPayOutNo) NextPayOutNo from tblCompany "
            Ds = DM.getDataSetWithSqlAdapter(sQry, "ONo", "ONo")
            If Ds.Tables("ONo").Rows.Count > 0 Then
                sNextOrderNo = Ds.Tables("ONo").Rows(0)("NextPayOutNo")
            End If
            'sQry = "Update   tblCompany Set NextScrapInventoryNo = NextScrapInventoryNo + 1 "
            'dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            sNextOrderNo = ""
        Finally
            GetSchemeEntryNo = sNextOrderNo
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetSchemeNo() As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "SELECT * FROM tblCompany "
            Ds = DM.getDataSetWithSqlAdapter(sQry, "tblCompany", "tblCompany")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetSchemeNo = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

End Class
