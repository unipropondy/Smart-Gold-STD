Public Class clsLocation
    Dim dm As New DataManager
    Public Function GetLocation() As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " Select * from tblLocation "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Location", "Location")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetLocation = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetLocationwithCode(ByVal sLocationCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " Select * from tblLocation where LocationCode=" & fncFormatSQLText(sLocationCode)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Location", "Location")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetLocationwithCode = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function DeleteLocation(ByVal sLocationCode As String) As Boolean
        Dim sQry As String
        Dim DelStatus As Boolean
        Try
            sQry = " Delete from tblLocation where LocationCode = " & fncFormatSQLText(sLocationCode)
            DelStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            DelStatus = False
        Finally
            DeleteLocation = DelStatus
            sQry = Nothing
        End Try
    End Function

    Public Function LocationSave(ByVal sLocationCode As String, ByVal sLocationName As String, ByVal sLocationType As String, ByVal sAddress1 As String, _
                                 ByVal sAddress2 As String, ByVal sAddress3 As String, ByVal sAddress4 As String, ByVal sPhone As String, _
                                 ByVal sFax As String, ByVal sPersonInCharge As String, ByVal dSortCode As Int32) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_Location " & fncFormatSQLText(sLocationCode) & ","
            sQry = sQry & fncFormatSQLText(sLocationName) & ","
            sQry = sQry & fncFormatSQLText(sLocationType) & ","
            sQry = sQry & fncFormatSQLText(sAddress1) & ","
            sQry = sQry & fncFormatSQLText(sAddress2) & ","
            sQry = sQry & fncFormatSQLText(sAddress3) & ","
            sQry = sQry & fncFormatSQLText(sAddress4) & ","
            sQry = sQry & fncFormatSQLText(sPhone) & ","
            sQry = sQry & fncFormatSQLText(sFax) & ","
            sQry = sQry & fncFormatSQLText(sPersonInCharge) & ","
            sQry = sQry & Val(dSortCode) & ","
            sQry = sQry & fncFormatSQLText(sUserID)
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            LocationSave = SaveStatus
            sQry = Nothing
        End Try
    End Function

End Class
