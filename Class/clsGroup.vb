Public Class clsGroup
    Dim Dm As New DataManager
    Public Function getGroup(ByVal sGroupCode As String) As DataSet
        Dim objDs As New DataSet
        Dim sSql As String
        sSql = " GetGroup " & fncFormatSQLText(Trim(sGroupCode))
        objDs = Dm.getDataSetWithSqlAdapterAcc(sSql, "GetGroup", "GetGroup")
        Return objDs
        objDs = Nothing
    End Function
    Public Function getGroupByCode(ByVal sGroupCode As String) As DataSet
        Dim objDs As New DataSet
        Dim sSql As String
        sSql = " GetGroupByCode " & fncFormatSQLText(Trim(sGroupCode))
        objDs = Dm.getDataSetWithSqlAdapterAcc(sSql, "GetGroupByCode", "GetGroupByCode")
        Return objDs
        objDs = Nothing
    End Function

    Public Function fncGroupSave(ByVal sGroupCode As String, ByVal sGroupName As String, _
                ByVal sSortCode As String, ByVal sActive As String, ByVal TreatedAs As String, ByVal AccountType As String, ByVal sMode As String) As Boolean
        Dim sSql As String
        sSql = "Exec sp_Group "
        sSql = sSql & fncFormatSQLText(sGroupCode) & ","
        sSql = sSql & fncFormatSQLText(sGroupName) & ","
        sSql = sSql & fncFormatSQLText(sSortCode) & ","
        sSql = sSql & fncFormatSQLText(sActive) & ","
        sSql = sSql & fncFormatSQLText(TreatedAs) & ","
        sSql = sSql & fncFormatSQLText(AccountType) & ","
        sSql = sSql & fncFormatSQLText(sUserID) & ","
        sSql = sSql & fncFormatSQLText(sMode)
        fncGroupSave = Dm.ProjExecNonQryAcc(sSql)
        Return fncGroupSave
    End Function
End Class
