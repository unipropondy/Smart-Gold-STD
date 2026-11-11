Public Class clsCompany

    Dim objGeneral As New clsJobSheet
    Public Function GetCompany(ByVal sCompany As String) As String

        Dim sSelect As String
        Dim ds As New DataSet
        Dim DM As New DataManager
        On Error GoTo ErrorSub

        sSelect = " GetCompany " & fncFormatSQLText(Trim(sCompany))



        ds = DM.getDataSetWithSqlAdapter(sSelect, "getCompany", "getCompany")
        If ds.Tables("getCompany").Rows.Count > 0 Then
            GetCompany = ds.Tables("getCompany").Rows(0)("Name")
        End If
        Exit Function

ErrorSub:
        MsgBox(Err.Description)
        Err.Clear()

    End Function
    Public Function getCompanyName() As DataSet
        Dim objDs As New DataSet
        Dim DM As New DataManager
        Dim sSql As String
        On Error GoTo errhandler

        sSql = " Select * from tblcompany"
        objDs = DM.SetTabelsToDataSet(sSql, "GetCompanyName")
        Return objDs

cleanup:
        objDs = Nothing
        Exit Function
errhandler:
        getCompanyName = Nothing
        GoTo cleanup
    End Function


    Public Function GetCompanyList() As DataSet
        Dim DM As New DataManager
        Dim ds As New DataSet
        Dim sSql As String

        On Error GoTo ErrHandler

        sSql = " Select * from tblCompany "
        ds = DM.getDataSetWithSqlAdapter(sSql, "getCompanyList", "getCompanyList")
        GetCompanyList = ds
        ds = Nothing
CleanUp:
        ds = Nothing
        Exit Function

ErrHandler:

        GetCompanyList = Nothing
        GoTo CleanUp
    End Function

    Public Function CompanySave(ByVal sCompanyID As String, ByVal sName As String, _
                                ByVal sAddress1 As String, ByVal sAddress2 As String, _
                                ByVal sCity As String, ByVal sState As String, _
                                ByVal sCountry As String, ByVal sPostalcode As String, _
                                ByVal sPhone As String, ByVal sFax As String, ByVal sEmail As String, _
                                ByVal sUrl As String, _
                                ByVal sDateFormat As String, ByVal sTimeFormat As String, _
                                ByVal sCurrencySign As String, ByVal sDefaultCurrency As String, _
                                ByVal sGSTRegNo As String, ByVal sBillMessage1 As String, _
                                ByVal sBillMessage2 As String, ByVal sDisplaymessage1 As String, _
                                ByVal sDisplayMessage2 As String, ByVal dGst As Double, _
                                ByVal nNextInvoice As Long, ByVal nNextReceiptNo As Long, ByVal nMaxDiscount As Double, _
                                ByVal nMaxPriceChange As Double, ByVal nNextPoNo As Long, _
                                ByVal sTaxIn As Char, ByVal nAvgCost As Double, ByVal nExpiredTime As Double, _
                                ByVal sBillPrint As Char, ByVal nNextCreditNo As Long, ByVal sTaxTaken As Char, _
                                ByVal nTradeSalesNo As Long, ByVal nNextReturnNo As Long, ByVal NPaymentNo As Long) As Boolean

        Dim Dm As DataManager
        Dim sExec As String


        On Error GoTo ErrHandler

        sExec = "EXEC sp_Company"
        sExec = sExec & fncFormatSQLText(sCompanyID) & ", "
        sExec = sExec & fncFormatSQLText(sName) & ", "
        sExec = sExec & fncFormatSQLText(sAddress1) & ", "
        sExec = sExec & fncFormatSQLText(sAddress2) & ", "
        sExec = sExec & fncFormatSQLText(sCity) & ", "
        sExec = sExec & fncFormatSQLText(sState) & ", "
        sExec = sExec & fncFormatSQLText(sCountry) & ", "
        sExec = sExec & fncFormatSQLText(sPostalcode) & ", "
        sExec = sExec & fncFormatSQLText(sPhone) & ", "
        sExec = sExec & fncFormatSQLText(sFax) & ", "
        sExec = sExec & fncFormatSQLText(sEmail) & ", "
        sExec = sExec & fncFormatSQLText(sUrl) & ", "
        sExec = sExec & fncFormatSQLText(sDateFormat) & ", "
        sExec = sExec & fncFormatSQLText(sTimeFormat) & ", "
        sExec = sExec & fncFormatSQLText(sCurrencySign) & ", "
        sExec = sExec & fncFormatSQLText(sDefaultCurrency) & ", "
        sExec = sExec & fncFormatSQLText(sGSTRegNo) & ", "
        sExec = sExec & fncFormatSQLText(sBillMessage1) & ", "
        sExec = sExec & fncFormatSQLText(sBillMessage2) & ", "
        sExec = sExec & fncFormatSQLText(sDisplaymessage1) & ", "
        sExec = sExec & fncFormatSQLText(sDisplayMessage2) & ", "
        sExec = sExec & dGst & ", "
        sExec = sExec & nNextInvoice & ","
        sExec = sExec & nNextReceiptNo & ","
        sExec = sExec & nMaxDiscount & ","
        sExec = sExec & nMaxPriceChange & ","
        sExec = sExec & nNextPoNo & ","
        sExec = sExec & fncFormatSQLText(sTaxIn) & ","
        sExec = sExec & nAvgCost & ","
        sExec = sExec & nExpiredTime & ","
        sExec = sExec & fncFormatSQLText(sBillPrint) & ","
        sExec = sExec & nNextCreditNo & ","
        sExec = sExec & fncFormatSQLText(sTaxTaken) & ","
        sExec = sExec & nTradeSalesNo & ","
        sExec = sExec & nNextReturnNo & ","
        sExec = sExec & NPaymentNo & ","
        sExec = sExec & fncFormatSQLText(sUserID)
        CompanySave = Dm.ProjExecNonQry(sExec)
        CompanySave = True

CleanUp:
        Exit Function

ErrHandler:
        CompanySave = False
        GoTo CleanUp
    End Function
    Public Function fncSaveCashDrawerLog(ByVal sBusinessUnitId As String, _
                                            ByVal sTerminalCode As String, _
                                            ByVal sLocationCode As String, _
                                            ByVal sUserId As String, _
                                            ByVal sCreatedOn As String, _
                                            ByVal sDescription As String) As Boolean
        Dim sExec As String
        Try
            sExec = " Exec Sp_CashDrawerLog "
            sExec &= fncFormatSQLText(sBusinessUnitId) & ","
            sExec &= fncFormatSQLText(sTerminalCode) & ","
            sExec &= fncFormatSQLText(sLocationCode) & ","
            sExec &= fncFormatSQLText(sUserId) & ","
            sExec &= fncFormatSQLDate(sCreatedOn) & ","
            sExec &= fncFormatSQLText(sDescription)
            fncSaveCashDrawerLog = objGeneral.fncExecuteQuery(sExec)
        Catch ex As Exception
            Throw ex
            fncSaveCashDrawerLog = False
        End Try
    End Function


End Class

