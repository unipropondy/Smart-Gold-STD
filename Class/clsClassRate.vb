Public Class clsClassRate
    Public dm As New DataManager

#Region "Fetch Class Rate"

    Public Function GetClassRateDetail(ByVal sClassCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " SELECT FromDate, ToDate, Rate from tblClassRate  where ClassCode = " & fncFormatSQLText(sClassCode)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "ClassRate", "ClassRate")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetClassRateDetail = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function GetClassRateDetail(ByVal sClassCode As String, ByVal sFromDate As String, ByVal sToDate As String) As Double
        Dim Ds As New DataSet
        Dim sQry As String
        Dim dRate As Double
        Try
            sQry = " SELECT Rate from tblClassRate  where ClassCode = " & fncFormatSQLText(sClassCode) & " and FromDate = " & fncFormatSQLDate(sFromDate) & " and ToDate = " & fncFormatSQLDate(sToDate)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "ClassRate", "ClassRate")
            If Ds.Tables("ClassRate").Rows.Count > 0 Then
                dRate = Ds.Tables("ClassRate").Rows(0)("Rate")
            Else
                dRate = 0.0
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetClassRateDetail = dRate
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function


#End Region

#Region "Class Rate Save"

    Public Function ClassRateSave(ByVal sClassCode As String, ByVal sClassName As String, ByVal sFromDate As String, ByVal sToDate As String, _
         ByVal dRate As Double, ByVal sMode As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = sQry & "EXEC sp_ClassRate "
            sQry = sQry & fncFormatSQLText(sClassCode) & ","
            sQry = sQry & fncFormatSQLText(sClassName) & ","
            sQry = sQry & fncFormatSQLDate(sFromDate) & ","
            sQry = sQry & fncFormatSQLDate(sToDate) & ","
            sQry = sQry & Val(dRate) & ","
            sQry = sQry & fncFormatSQLText(sMode) & ","
            sQry = sQry & fncFormatSQLText(sUserID)
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            ClassRateSave = SaveStatus
            sQry = Nothing
        End Try
    End Function

#End Region

End Class
