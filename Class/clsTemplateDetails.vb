Public Class clsTemplateDetails
    Dim Dv As DataView
    Dim Dm As New DataManager
    Public Sub SetDataOnLoad()

    End Sub
    Public Function fnSaveTemplateHeader(ByVal TCode As String, ByVal TDescription As String, ByVal NoofEntries As Integer, _
                                         ByVal CreateBy As String, ByVal CreateDate As String, ByVal ModifyBy As String, _
                                        ByVal Modifydate As String, ByVal Mode As String) As String
        On Error GoTo errtrap
        Dim SqlQry As String
        'Dim sExecBool As Boolean
        SqlQry = "EXEC TM_TemplateHeader "
        SqlQry &= fncFormatSQLText(TCode) & ","
        SqlQry &= fncFormatSQLText(TDescription) & "," & Val(NoofEntries) & ","
        SqlQry &= fncFormatSQLText(CreateBy) & "," & fncFormatSQLDate(CreateDate) & "," & fncFormatSQLText(ModifyBy) & ","
        SqlQry &= fncFormatSQLDate(Modifydate) & "," & fncFormatSQLText(Mode)
        'Dm.ReturnProjExecNonQry(SqlQry)
        If Dm.ProjExecNonQryAcc(SqlQry) = False Then
            Return "F"
        Else
            Return "T"
        End If
        Exit Function
errtrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Function
    Public Function fnSaveTemplateDetail(ByVal SNo As String, ByVal TCode As String, ByVal AccountCode As String, ByVal Description As String, _
                                         ByVal Memo As String, ByVal Reference As String, ByVal Debit As Double, ByVal Credit As Double) As String
        On Error GoTo errtrap
        Dim SqlQry As String
        'Dim sExecBool As Boolean
        SqlQry = "EXEC TM_TemplateDetail "
        SqlQry &= SNo & "," & fncFormatSQLText(TCode) & "," & fncFormatSQLText(AccountCode) & ","
        SqlQry &= fncFormatSQLText(Description) & "," & fncFormatSQLText(Memo) & "," & fncFormatSQLText(Reference) & ","
        SqlQry &= Val(Debit) & "," & Val(Credit) & ""
        'Dm.ReturnProjExecNonQry(SqlQry)
        Dm.ProjExecNonQryAcc(SqlQry)
        Exit Function
errtrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Function
    Public Function fnDeleteTemplateDetail(ByVal TCode As String) As Boolean
        fnDeleteTemplateDetail = False
        On Error GoTo errtrap
        Dm.ProjExecNonQryAcc("Exec TM_DelTemplateDetail '" & TCode & "'")
        fnDeleteTemplateDetail = True
        Exit Function
errtrap:
        MsgBox("Error in Delete Record" & Err.Description)
    End Function
    Public Function fnDeleteTemplate(ByVal TCode As String) As Boolean
        fnDeleteTemplate = False
        On Error GoTo errtrap
        Dm.ProjExecNonQryAcc("Exec TM_DeleteTemplate '" & TCode & "'")
        fnDeleteTemplate = True
        Exit Function
errtrap:
        MsgBox("Error in Delete Record" & Err.Description)
    End Function
End Class
