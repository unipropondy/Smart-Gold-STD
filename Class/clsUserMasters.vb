Public Class clsUserMasters
    Dim dm As New DataManager

#Region "User Master Delete"
    Public Function UserMasterDelete(ByVal sUserCode As String) As Boolean
        Dim sExec As String
        Try
            UserMasterDelete = False
            sExec = "BEGIN TRANSACTION" & vbCrLf
            sExec = sExec & "DELETE FROM tblUserMaster WHERE UserCode=" & fncFormatSQLText(sUserCode) & vbCrLf
            sExec = sExec & "COMMIT TRANSACTION" & vbCrLf
            dm.ProjExecNonQry(sExec)
            UserMasterDelete = True
            Exit Function
        Catch ex As Exception
            UserMasterDelete = False
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Function
#End Region

#Region "User Master Save"
    Public Function fncSaveUserMaster(ByVal sUserCode As String, ByVal sUserName As String, ByVal sUserGroupCode As String, _
                         ByVal sExpiredDate As String, _
                         ByVal nDayWarning As Long, ByVal sUserId As String) As Boolean
        Try
            Dim sExec As String
            fncSaveUserMaster = False
            sExec = "Begin Transaction" & vbCrLf
            sExec = sExec & "EXEC sp_UserMaster " & fncFormatSQLText(sUserCode) & ","
            sExec = sExec & fncFormatSQLText(sUserGroupCode) & ","
            sExec = sExec & fncFormatSQLText(sUserName) & ","
            sExec = sExec & fncFormatSQLDate(sExpiredDate) & ","
            sExec = sExec & Val(nDayWarning) & ","
            sExec = sExec & fncFormatSQLText(sUserId) & vbCrLf
            sExec = sExec & "Commit Transaction" & vbCrLf
            fncSaveUserMaster = dm.ProjExecNonQry(sExec)
            Exit Function
        Catch ex As Exception
            fncSaveUserMaster = False
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Function
#End Region

#Region "Get user Master Detail"
    Public Function GetUserMasterDetail(ByVal sUserCode As String) As DataSet
        Dim ds As New DataSet
        Dim sSql As String

        If sUserCode.Length = 0 Then
            sSql = "select usercode,username,usergroupcode,expirydate,  daysofwarning  from tblusermaster order by UserCode"
        Else
            sSql = "SELECT usercode,username,usergroupcode,expirydate,  daysofwarning FROM tblUserMaster WHERE UserCode =" & fncFormatSQLText(sUserCode)

        End If
        Try
            ds = dm.getDataSetWithSqlAdapter(sSql, "tbluserMaster", "tbluserMaster")
            GetUserMasterDetail = ds
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

#Region "Added by Sarthy"

    Public Function GetAdminPassword(ByVal sPassword As String) As DataSet
        Dim ds As New DataSet
        Dim sSql As String
        Try
            sSql = "SELECT * FROM tblUserMaster WHERE Password =" & fncFormatSQLText(sPassword)
            ds = dm.getDataSetWithSqlAdapter(sSql, "tbluserMaster", "tbluserMaster")

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            GetAdminPassword = ds
            ds = Nothing
        End Try
    End Function

#End Region

End Class
