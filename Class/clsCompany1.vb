Public Class clsCompany1



    'for accounts
    Public Function GetCompanyListAcc() As DataSet
        Dim DM As New DataManager
        Dim ds As New DataSet
        Dim sSql As String
        sSql = "SELECT * FROM tblCompany"
        ds = DM.getDataSetWithSqlAdapterAcc(sSql, "getCompanyList", "getCompanyList")
        GetCompanyListAcc = ds
        ds = Nothing
    End Function


    Public Function GetCompany(ByVal sCompany As String) As String

        Dim sSelect As String
        Dim ds As New DataSet
        Dim DM As New DataManager
        On Error GoTo ErrorSub

        sSelect = " GetCompany " & fncFormatSQLText(Trim(sCompany))

        'sSelect = "SELECT * FROM tblCompany WHERE CompanyID =" & fncFormatSQLText(sCompany)

        ds = DM.getDataSetWithSqlAdapter(sSelect, "getCompany", "getCompany")
        If ds.Tables("getCompany").Rows.Count > 0 Then
            GetCompany = ds.Tables("getCompany").Rows(0)("Name")
        End If
        'CreateMySqlDataReader(sSelect, gsCon)
        'ds.Tables ("getCompany").

        Exit Function

ErrorSub:
        MsgBox(Err.Description)
        Err.Clear()

    End Function
    Public Function getCompanyName() As DataSet
        Dim objDs As New DataSet
        Dim DM As New DataManager
        Dim sSql As String
        sSql = " Select * from tblcompany"
        objDs = DM.SetTabelsToDataSet(sSql, "GetCompanyName")
        Return objDs
        objDs = Nothing
    End Function
    '    Public Function GetBranchLocation() As ADODB.Recordset

    '        Dim sSelect As String
    '        Dim ORs As ADODB.Recordset

    '        On Error GoTo ErrHandler

    '        sSelect = "SELECT * FROM tblLocation "

    '        ORs = ExecuteQueryUsingConnectString(gsConn, sSelect)

    '        GetBranchLocation = ORs

    'CleanUp:
    '        ORs = Nothing
    '        Exit Function

    'ErrHandler:
    '        ErrorSet()
    '        GetBranchLocation = Nothing
    '        GoTo CleanUp

    '    End Function

    Public Function GetCompanyList() As DataSet
        Dim DM As New DataManager
        '-----
        Dim ds As New DataSet
        Dim sSql As String
        sSql = "SELECT * FROM tblCompany"
        ds = DM.getDataSetWithSqlAdapter(sSql, "getCompanyList", "getCompanyList")
        GetCompanyList = ds
        ds = Nothing
        '-

        '        Dim sSelect As String
        '        Dim ORs As ADODB.Recordset

        '        On Error GoTo ErrHandler

        '        sSelect = "SELECT * FROM tblCompany"

        '        ORs = ExecuteQueryUsingConnectString(gsConn, sSelect)

        '        GetCompanyList = ORs

        'CleanUp:
        '        ORs = Nothing
        '        Exit Function

        'ErrHandler:
        '        ErrorSet()
        '        GetCompanyList = Nothing
        '        GoTo CleanUp
    End Function

    Public Function GetCompanyListHQ() As DataSet
        Dim DM As New DataManager
        '-----
        Dim ds As New DataSet
        Dim sSql As String
        sSql = "SELECT * FROM tblCompany"
        ds = DM.getDataSetWithSqlAdapterAcc(sSql, "getCompanyList", "getCompanyList")
        GetCompanyListHQ = ds
        ds = Nothing
        '-

        '        Dim sSelect As String
        '        Dim ORs As ADODB.Recordset

        '        On Error GoTo ErrHandler

        '        sSelect = "SELECT * FROM tblCompany"

        '        ORs = ExecuteQueryUsingConnectString(gsConn, sSelect)

        '        GetCompanyList = ORs

        'CleanUp:
        '        ORs = Nothing
        '        Exit Function

        'ErrHandler:
        '        ErrorSet()
        '        GetCompanyList = Nothing
        '        GoTo CleanUp
    End Function

    '    Public Function CompanySave(ByRef oConnection As ADODB.Connection, ByRef ORs As ADODB.Recordset, _
    '                                ByVal sCompanyID As String, ByVal sName As String, _
    '                                ByVal sAddress1 As String, ByVal sAddress2 As String, _
    '                                ByVal sCity As String, ByVal sState As String, _
    '                                ByVal sCountry As String, ByVal sPostalcode As String, _
    '                                ByVal sPhone As String, ByVal sFax As String, ByVal sEmail As String, _
    '                                ByVal sUrl As String, ByVal sLocationCode As String, ByVal sMainLocationCode As String, _
    '                                ByVal sDateFormat As String, ByVal sTimeFormat As String, _
    '                                ByVal sCurrencySign As String, ByVal sDefaultCurrency As String, _
    '                                ByVal sBRNNo As String, ByVal sGSTRegNo As String, ByVal nNextMailID As Long, _
    '                                ByVal nNextPosNo As Long, ByVal nNextInvoice As Long, _
    '                                ByVal nGstPerc As Long, ByVal sGstType As String, ByVal sMessage1 As String, _
    '                                ByVal sMessage2 As String, ByVal sDisplayMessage1 As String, _
    '                                ByVal sDisplayMessage2 As String, ByVal sDisplayComport As String, _
    '                                ByVal nNextPaymentVoucherNo As Long, ByVal nNextPosCreditNo As Long, _
    '                                ByVal nNextReciptNo As Long, ByVal nNextAvilableRange As Long, ByVal nNextTransNo As Long, _
    '                                ByVal nMaxDiscountAllowed As Long, ByVal nMaxPriceChangeAllowed As Long, _
    '                                ByVal nBarCodeCOMPort As Long, ByVal sInventoryAutoManual As Long, _
    '                                ByVal nExpiredTime As Long, ByVal nNextPONo As Long, _
    '                                ByVal nNextAverageCostPercentage As Long, ByVal sTaxIN As Long, _
    '                                ByVal nNextTransferNo As Long, ByVal nNextEODDate As String, _
    '                                ByVal nNextPurReNo As Long, ByVal nSalesReturnRefNo As Long, _
    '                                ByVal sTaxTaken As String, ByVal nHQTransNo As Long, _
    '                                ByVal sBillPrint As String, ByVal sUseSalesQty As String, _
    '                                ByVal sRemarks As String, ByVal sInventoryPrefix As Long, ByVal sInventoryRangeBasedOn As String, ByVal nMember As Long, ByVal nAllowMultipleDiscount As Long, ByVal sUser As String) As Boolean



    '        Dim sExec As String

    '        On Error GoTo ErrHandler

    '        sExec = "EXEC sp_Company"
    '        sExec = sExec & fncFormatSQLText(sCompanyID) & ", "
    '        sExec = sExec & fncFormatSQLText(sName) & ", "
    '        sExec = sExec & fncFormatSQLText(sAddress1) & ", "
    '        sExec = sExec & fncFormatSQLText(sAddress2) & ", "
    '        sExec = sExec & fncFormatSQLText(sCity) & ", "
    '        sExec = sExec & fncFormatSQLText(sState) & ", "
    '        sExec = sExec & fncFormatSQLText(sCountry) & ", "
    '        sExec = sExec & fncFormatSQLText(sPostalcode) & ", "
    '        sExec = sExec & fncFormatSQLText(sPhone) & ", "
    '        sExec = sExec & fncFormatSQLText(sFax) & ", "
    '        sExec = sExec & fncFormatSQLText(sEmail) & ", "
    '        sExec = sExec & fncFormatSQLText(sUrl) & ", "
    '        sExec = sExec & fncFormatSQLText(sLocationCode) & ", "
    '        sExec = sExec & fncFormatSQLText(sMainLocationCode) & ", "
    '        sExec = sExec & fncFormatSQLText(sDateFormat) & ", "
    '        sExec = sExec & fncFormatSQLText(sTimeFormat) & ", "
    '        sExec = sExec & fncFormatSQLText(sCurrencySign) & ", "
    '        sExec = sExec & fncFormatSQLText(sDefaultCurrency) & ", "
    '        sExec = sExec & fncFormatSQLText(sBRNNo) & ", "
    '        sExec = sExec & fncFormatSQLText(sGSTRegNo) & ", "
    '        sExec = sExec & Val(nNextMailID) & ", "
    '        sExec = sExec & Val(nNextPosNo) & ", "
    '        sExec = sExec & Val(nNextInvoice) & ", "
    '        sExec = sExec & Val(nGstPerc) & ", "
    '        sExec = sExec & fncFormatSQLText(sGstType) & ", "
    '        sExec = sExec & fncFormatSQLText(sMessage1) & ", "
    '        sExec = sExec & fncFormatSQLText(sMessage2) & ", "
    '        sExec = sExec & fncFormatSQLText(sDisplayMessage1) & ", "
    '        sExec = sExec & fncFormatSQLText(sDisplayMessage2) & ", "
    '        sExec = sExec & fncFormatSQLText(sDisplayComport) & ", "
    '        sExec = sExec & Val(nNextPaymentVoucherNo) & ", "
    '        sExec = sExec & Val(nNextPosCreditNo) & ", "
    '        sExec = sExec & Val(nNextReciptNo) & ", "
    '        sExec = sExec & Val(nNextAvilableRange) & ", "
    '        sExec = sExec & Val(nNextTransNo) & ", "
    '        sExec = sExec & Val(nMaxDiscountAllowed) & ", "
    '        sExec = sExec & Val(nMaxPriceChangeAllowed) & ", "
    '        sExec = sExec & Val(nBarCodeCOMPort) & ", "
    '        sExec = sExec & fncFormatSQLText(sInventoryAutoManual) & ", "
    '        sExec = sExec & Val(nExpiredTime) & ", "
    '        sExec = sExec & Val(nNextPONo) & ", "
    '        sExec = sExec & Val(nNextAverageCostPercentage) & ", "
    '        sExec = sExec & fncFormatSQLText(sTaxIN) & ", "
    '        sExec = sExec & Val(nNextTransferNo) & ", "
    '        sExec = sExec & DoSQLDate(nNextEODDate) & ", "
    '        sExec = sExec & Val(nNextPurReNo) & ", "
    '        sExec = sExec & Val(nSalesReturnRefNo) & ", "
    '        sExec = sExec & fncFormatSQLText(sTaxTaken) & ", "
    '        sExec = sExec & Val(nHQTransNo) & ", "
    '        sExec = sExec & fncFormatSQLText(sBillPrint) & ", "
    '        sExec = sExec & fncFormatSQLText(sUseSalesQty) & ", "
    '        sExec = sExec & fncFormatSQLText(sRemarks) & ", "
    '        sExec = sExec & Val(sInventoryPrefix) & ", "
    '        sExec = sExec & fncFormatSQLText(sInventoryRangeBasedOn) & ", "
    '        sExec = sExec & Val(nMember) & ", "
    '        sExec = sExec & Val(nAllowMultipleDiscount) & ", "
    '        sExec = sExec & fncFormatSQLText(sUser)
    '        TransactionExecuteQuery(oConnection, sExec, ORs)

    '        CompanySave = True

    'CleanUp:
    '        Exit Function

    'ErrHandler:
    '        CompanySave = False
    '        ErrorSet()
    '        GoTo CleanUp
    '    End Function

    '    Public Function GridColourSave(ByVal sEvenColour As String, ByVal sOddColour As String, _
    '                             ByVal sUserID As String) As Boolean
    '        Dim sExec As String
    '        Dim sErrDesc As String
    '        Dim sVal As String
    '        Dim lErrNo As Long
    '        Dim lCustID As Long

    '        On Error GoTo ErrHandler
    '        GridColourSave = False
    '        sExec = "Begin Transaction" & vbCrLf
    '        sExec = sExec & "EXEC sp_GridColour " & fncFormatSQLText(sEvenColour) & ","
    '        sExec = sExec & fncFormatSQLText(sOddColour) & ","
    '        sExec = sExec & fncFormatSQLText(sUserID) & vbCrLf
    '        sExec = sExec & "Commit Transaction" & vbCrLf
    '        ExecQuery(gsConn, sExec)
    '        GridColourSave = True
    '        'Set m_oDal = Nothing

    '        Exit Function
    'ErrHandler:
    '        lErrNo = Err.Number
    '        sErrDesc = Err.Description
    '        Resume NextLine
    'NextLine:
    '        On Error Resume Next
    '        'Set m_oDal = Nothing
    '        On Error GoTo 0
    '        Err.Raise(lErrNo, OBJNAME, sErrDesc)
    '    End Function

    '    Public Function GetGridColour(ByVal sUserCode As String) As ADODB.Recordset
    '        Dim ORs As ADODB.Recordset

    '        On Error GoTo ErrHandler

    '        sSelect = "SELECT * FROM tblGridColour where UserCode = " & fncFormatSQLText(sUserCode)
    '        ORs = ExecuteQueryUsingConnectString(gsConn, sSelect)
    '        GetGridColour = ORs

    'CleanUp:
    '        ORs = Nothing
    '        Exit Function

    'ErrHandler:
    '        ErrorSet()
    '        GetGridColour = Nothing
    '        GoTo CleanUp
    '    End Function

End Class
