Public Class clsPayMode
    Dim dm As New DataManager

#Region "Fetch Pay Mode Detail "

    Public Function GetPayMode() As DataSet
        Dim Ds As New DataSet
        Dim sExec As String
        Try
            sExec = " select * from tblPaymode order by POSITION "
            Ds = dm.getDataSetWithSqlAdapter(sExec, "PayMode", "PayMode")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetPayMode = Ds
            Ds = Nothing
            sExec = Nothing
        End Try
    End Function

#End Region

#Region "Fetch Pay Mode Detailfor paymode/position "

    Public Function GetPayModeDetail(ByVal sPaymode As String) As DataSet
        Dim Ds As New DataSet
        Dim sExec As String
        Try
            sExec = "select * from tblPaymode where paymode =  " & fncFormatSQLText(sPaymode)
            Ds = dm.getDataSetWithSqlAdapter(sExec, "PayMode", "PayMode")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetPayModeDetail = Ds
            Ds = Nothing
            sExec = Nothing
        End Try
    End Function

    Public Function GetPayModeDetailByPosition(ByVal sPosition As String) As DataSet
        Dim Ds As New DataSet
        Dim sExec As String
        Try
            sExec = "select * from tblPaymode where position =  " & sPosition.Trim

            Ds = dm.getDataSetWithSqlAdapter(sExec, "PayMode", "PayMode")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetPayModeDetailByPosition = Ds
            Ds = Nothing
            sExec = Nothing
        End Try
    End Function

#End Region

#Region "Delete Pay Mode"

    Public Function DeletePayMode(ByVal PayMode As String) As Boolean
        Dim sExec As String
        Dim DelStatus As Boolean
        Try
            sExec = " Delete from tblPayMode where PayMode = " & fncFormatSQLText(PayMode)
            DelStatus = dm.ProjExecNonQry(sExec)
            DelStatus = dm.ProjExecNonQryAcc(sExec)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            DelStatus = False
        Finally
            DeletePayMode = DelStatus
            sExec = Nothing
        End Try
    End Function

#End Region

#Region "Save Pay Mode"

    Public Function SavePayMode(ByVal nPosition As Integer, ByVal sPaymode As String, ByVal sDescription As String, ByVal nCommision As Double, ByVal sGLAccountcode As String, ByVal Active As String) As Boolean
        Dim sExec As String
        Dim SaveStatus As Boolean
        Try
            sExec = "EXEC sp_Paymode " & Val(nPosition) & ","
            sExec = sExec & fncFormatSQLText(sPaymode) & ","
            sExec = sExec & fncFormatSQLText(sDescription) & ","
            sExec = sExec & Val(nCommision) & ","
            sExec = sExec & fncFormatSQLText(sGLAccountcode) & ","
            sExec = sExec & fncFormatSQLText(Active) & ","
            sExec = sExec & fncFormatSQLText(sUserID)
            SaveStatus = dm.ProjExecNonQry(sExec)
            If sAccountsModule = "1" Then
                SaveStatus = dm.ProjExecNonQryAcc(sExec)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            SavePayMode = SaveStatus
            sExec = Nothing
        End Try
    End Function

#End Region

#Region "Added by Sarathy"

    Public Function PayModeDetailSave(ByVal sInvoiceNo As String, ByVal sDate As String, ByVal sPaymode As String, ByVal nAmount As Double, ByVal nCommission As Double, ByVal sSlipNo As String, ByVal sSalesType As String) As Boolean
        Dim sExec As String
        Dim SaveStatus As Boolean
        Try
            sExec = sExec & "EXEC sp_PayModeDetail " & fncFormatSQLText(sInvoiceNo) & ","
            sExec = sExec & fncFormatSQLDate(sDate) & ","
            sExec = sExec & fncFormatSQLText(sPaymode) & ","
            sExec = sExec & Val(nAmount) & ","
            sExec = sExec & Val(nCommission) & ","
            sExec = sExec & fncFormatSQLText(sSlipNo) & ","
            sExec = sExec & fncFormatSQLText(sSalesType) & ","
            sExec = sExec & fncFormatSQLText(sUserID)
            SaveStatus = dm.ProjExecNonQry(sExec)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            PayModeDetailSave = SaveStatus
            sExec = Nothing
        End Try
    End Function

    Public Function GetForeignCurrency() As DataSet
        Dim Ds As New DataSet
        Dim sExec As String
        Try
            sExec = "SELECT * FROM tblForeignCurrency "
            Ds = dm.getDataSetWithSqlAdapter(sExec, "FCur", "FCur")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetForeignCurrency = Ds
            Ds = Nothing
            sExec = Nothing
        End Try
    End Function

#End Region

    'murugan include for accounts
    Public Function GetPayModeacc() As DataSet
        Dim Ds As New DataSet
        Dim sExec As String
        Try
            sExec = " select * from tblPaymodeacc "
            Ds = dm.getDataSetWithSqlAdapterAcc(sExec, "PayMode", "PayMode")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetPayModeacc = Ds
            Ds = Nothing
            sExec = Nothing
        End Try
    End Function

    'murugan include for accounts 09/02/2007
    Public Function savePaymodeacc(ByVal paymode As String, ByVal accountcode As String) As Boolean
        On Error GoTo errpart
        Dim dm As New DataManager
        Dim sSelect As String
        savePaymodeacc = False
        sSelect = " sp_paymodeacc  "
        sSelect = sSelect & fncFormatSQLText(paymode) & ","
        sSelect = sSelect & fncFormatSQLText(accountcode)
        dm.ProjExecNonQryAcc(sSelect)
        savePaymodeacc = True
        Exit Function
errpart:
        MsgBox(Err.Description)
    End Function

End Class

