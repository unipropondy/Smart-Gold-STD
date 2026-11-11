Public Class clsUserMaster
    Dim Dm As New DataManager
    Public Function GetUserMasterList() As DataSet
        Dim objDs As New DataSet
        Dim sSql As String
        sSql = "SELECT * FROM tblUserMaster order by UserCode"
        objDs = Dm.getDataSetWithSqlAdapter(sSql, "S", "S")
        Return objDs
        objDs = Nothing
        Exit Function
    End Function
    Public Function UserLogin(ByVal sUserID As String, _
                            ByVal sPasswd As String) As DataSet

        Dim ds As New DataSet
        Dim sSql As String

        UserLogin = Nothing
        sSql = " SELECT * FROM tblUserMaster WHERE UserCode= " & fncFormatSQLText(sUserID) & " and Password = " & fncFormatSQLText(sPasswd)
        ds = Dm.getDataSetWithSqlAdapter(sSql, "getUser", "getUser")
        UserLogin = ds
        ds = Nothing

    End Function
    'Public Function ChangePassword(ByVal sUserID As String, ByVal spasswd As String) As DataSet
    '    Dim ds As DataSet
    '    Dim sSql As String
    '    Dim sErrDesc As String
    '    Dim sVal As String
    '    Dim lErrNo As Long
    '    sSql = "Exec sp_ChangePassword "
    '    sSql = sSql & fncFormatSQLText(sUserID) & ","
    '    sSql = sSql & fncFormatSQLText(spasswd)

    'End Function

    Public Function ChangePassword(ByVal sUserID As String, ByVal spasswd As String) As Boolean
        Dim sQry As String
        Dim bSaveStatus As Boolean
        Try
            sQry = "Exec sp_ChangePassword "
            sQry = sQry & fncFormatSQLText(sUserID) & ","
            sQry = sQry & fncFormatSQLText(spasswd)
            bSaveStatus = DataManager.ProjExecNonQryAcc(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            bSaveStatus = False
        Finally
            ChangePassword = bSaveStatus
            sQry = Nothing
        End Try
    End Function

    Public Function getCheckPassword(ByVal sUserID As String, ByVal spasswd As String) As DataSet
        Dim ds As New DataSet
        Dim sSql As String
     
        getCheckPassword = Nothing
        sSql = " SELECT * FROM tblUserMaster WHERE UserCode= " & fncFormatSQLText(sUserID) & " and Password = " & fncFormatSQLText(spasswd)
        ds = Dm.getDataSetWithSqlAdapterAcc(sSql, "getUser", "getUser")
        getCheckPassword = ds
        ds = Nothing
    End Function

End Class
