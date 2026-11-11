Public Class clsUserGroup
    Dim Dm As New DataManager
#Region "User Group Delete"
    Public Function UserGroupDelete(ByVal sUserGroupCode As String) As Boolean
        Dim sExec As String
        Try
            UserGroupDelete = False
            sExec = "BEGIN TRANSACTION" & vbCrLf
            sExec = sExec & "DELETE FROM tblUserGroup WHERE UserGroupCode=" & fncFormatSQLText(sUserGroupCode) & vbCrLf
            sExec = sExec & "COMMIT TRANSACTION" & vbCrLf
            Dm.ProjExecNonQry(sExec)
            UserGroupDelete = True
            Exit Function
        Catch ex As Exception
            UserGroupDelete = False
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Function
#End Region
#Region "User Group Save"
    Public Function fncSaveUserGroup(ByVal UserGroupCode As String, ByVal UserGroupName As String, _
                                  ByVal CreateUser As String) As Boolean
        Try
            Dim sExec As String
            fncSaveUserGroup = False
            sExec = "BEGIN TRANSACTION" & vbCrLf
            sExec &= "Exec Sp_usergroup"
            sExec &= fncFormatSQLText(UserGroupCode) & "," & fncFormatSQLText(UserGroupName) & "," & fncFormatSQLText(CreateUser) & vbCrLf
            sExec = sExec & "COMMIT TRANSACTION" & vbCrLf
            fncSaveUserGroup = Dm.ProjExecNonQry(sExec)
            Exit Function
        Catch ex As Exception
            fncSaveUserGroup = False
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Function
#End Region
#Region "Get User group Detail"

    Public Function GetUserGroupDetail(ByVal sCode As String) As DataSet
        Dim ds As New DataSet
        Dim sSql As String

        If sCode.Length > 0 Then
            sSql = "select * from tblusergroup where  usergroupcode = " & fncFormatSQLText(sCode)
        Else
            sSql = "select * from tblusergroup"
        End If
        Try
            ds = Dm.getDataSetWithSqlAdapter(sSql, "tblusergroup", "tblusergroup")
            GetUserGroupDetail = ds
            ds = Nothing
            Exit Function
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
            ds = Nothing
        End Try
    End Function
#End Region
End Class

