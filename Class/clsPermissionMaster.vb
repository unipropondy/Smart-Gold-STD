Public Class clsPermissionMaster
    Dim Dm As New DataManager
    Public Function OpenBatchPostinConnection()
        usConBatchPosting.ConnectionString = usCon
        usConBatchPosting.Open()
        usTrans = usConBatchPosting.BeginTransaction
    End Function
    Public Function gpCommitTransaction()
        usTrans.Commit()
    End Function
    Public Function CloseBatchConnection()
        usConBatchPosting.Close()
    End Function
    Public Function gpRollBackBatchConnection()
        usTrans.Rollback()
    End Function

#Region "Permission Detail Save"
    Public Function SavePermissionDetails(ByVal UserGroupCode As String, _
                                            ByVal FormCode As String, _
                                            ByVal AllowAdd As String, _
                                            ByVal AllowUpdate As String, _
                                            ByVal AllowDelete As String, _
                                            ByVal AllowRead As String, _
                                            ByVal UserId As String) As Boolean
        Try
            Dim sExec As String
            SavePermissionDetails = False
            sExec = ""
            sExec &= " Sp_Userpermission"
            sExec &= fncFormatSQLText(UserGroupCode) & ","
            sExec &= fncFormatSQLText(FormCode) & ","
            sExec &= fncFormatSQLText(AllowAdd) & ","
            sExec &= fncFormatSQLText(AllowUpdate) & ","
            sExec &= fncFormatSQLText(AllowDelete) & ","
            sExec &= fncFormatSQLText(AllowRead) & ","
            sExec &= fncFormatSQLText(UserId)
            Dim Cmd As New SqlClient.SqlCommand(sExec, usConBatchPosting)
            Cmd.Transaction = usTrans
            Cmd.ExecuteNonQuery()
            SavePermissionDetails = True

        Catch ex As Exception
            Exit Function
            SavePermissionDetails = False
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Function
#End Region

#Region "Set Menu"
    Public Sub SetMenu(ByVal oForm As Form)
        Dim DrMenu As DataRow()
        Dim ctlTemp As MainMenu.MenuItemCollection
        Dim ctlT As MenuItem
        Dim i As Integer
        DrMenu = Dm.RowReturnProjExecNonQryWithDataRow("select t.MenuName  from tblUserPermission s,tblForms t where s.FormCode = t.FormCode and s.ChkFlag='1' and s.UserGroupCode = " & fncFormatSQLText(sUserGroup))
        If DrMenu.Length > 0 Then
            For Each ctlTemp In oForm.Controls
                For Each ctlT In ctlTemp
                    If ctlT.Text Like "mnu*" Then
                        For i = 0 To DrMenu.Length - 1
                            If UCase(ctlT.Text) = UCase(Trim(DrMenu(i)("MenuName") & "")) And Not ctlT.Enabled Then
                                ctlT.Enabled = True
                            End If
                        Next
                    End If
                Next
            Next
        End If
    End Sub
#End Region
End Class



