Public Class clsCurrency
    Dim dm As New DataManager

#Region "Currency Delete"
    Public Function CurrencyDelete(ByVal sCode As String) As Boolean
        Dim sExec As String
        Try
            CurrencyDelete = False
            sExec = "BEGIN TRANSACTION" & vbCrLf
            sExec = sExec & "DELETE FROM tblCurrency WHERE currencyCode=" & fncFormatSQLText(sCode) & vbCrLf
            sExec = sExec & "COMMIT TRANSACTION" & vbCrLf
            dm.ProjExecNonQry(sExec)
            CurrencyDelete = True
            Exit Function
        Catch ex As Exception
            CurrencyDelete = False
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Function
#End Region

#Region "Currency Save"
    Public Function fncSaveCurrency(ByVal sCode As String, ByVal sName As String, ByVal sRate As Double, _
                                    ByVal sRemarks As String, ByVal sUserId As String) As Boolean
        Try
            Dim sExec As String
            fncSaveCurrency = False
            sExec = "Begin Transaction" & vbCrLf
            sExec = sExec & "EXEC sp_Currency " & fncFormatSQLText(sCode) & ","
            sExec = sExec & fncFormatSQLText(sName) & ","
            sExec = sExec & Val(sRate) & ","
            sExec = sExec & fncFormatSQLText(sRemarks) & ","
            sExec = sExec & fncFormatSQLText(sUserId) & vbCrLf
            sExec = sExec & "Commit Transaction" & vbCrLf
            fncSaveCurrency = dm.ProjExecNonQry(sExec)
            Exit Function
        Catch ex As Exception
            fncSaveCurrency = False
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Function
#End Region

#Region "Get Currency Detail"
    Public Function GetCurrencyDetail(ByVal sCode As String) As DataSet
        Dim ds As New DataSet
        Dim sSql As String

        If sCode.Length = 0 Then
            sSql = "select * from tblCurrency order by currencyCode"
        Else
            sSql = "SELECT * FROM tblcurrency WHERE currencyCode =" & fncFormatSQLText(sCode)

        End If
        Try
            ds = dm.getDataSetWithSqlAdapter(sSql, "tblCurrency", "tblcurrency")
            GetCurrencyDetail = ds
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

#Region "Added By Murugan"

    Public Function GetCurrency(ByVal sCurrencyCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "SELECT * FROM tblCurrency WHERE CurrencyCode =" & fncFormatSQLText(sCurrencyCode)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Currency", "Currency")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetCurrency = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function GetClassCodeForGoldBar() As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "SELECT ClassCode,ClassName FROM tblClass WHERE GoldBarCode =" & fncFormatSQLText("Y")
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Currency", "Currency")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetClassCodeForGoldBar = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

#End Region

#Region "Added by Sarathy"

    Public Function IsCurrencyExist(ByVal sCode As String) As Boolean
        Dim ds As New DataSet
        Dim sSql As String
        Dim bStatus As Boolean
        sSql = "SELECT * FROM tblcurrency WHERE currencyCode =" & fncFormatSQLText(sCode)
        Try
            ds = dm.getDataSetWithSqlAdapter(sSql, "Currency", "currency")
            If ds.Tables(0).Rows.Count > 0 Then
                bStatus = True
            Else
                bStatus = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            IsCurrencyExist = bStatus
            ds = Nothing
        End Try
    End Function

#End Region

End Class
