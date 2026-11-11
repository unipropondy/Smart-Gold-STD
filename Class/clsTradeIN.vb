Public Class clsTradeIN
    Dim dm As New DataManager

    Public Function TradeInDelete(ByRef oConnection As ADODB.Connection, ByRef adoRS As ADODB.Recordset, _
                                ByVal SInventoryno As String, ByRef sError As String) As Boolean

        Dim ORs As DataSet
        Dim sSelect As String
        Dim sErrDesc As String
        Dim sVal As String
        Dim lErrNo As Long

        Try
            TradeInDelete = False
            'Header delete
            sSelect = "Delete from tblTradeInHeader WHERE InventoryCode =" & fncFormatSQLText(SInventoryno)
            'TransactionExecuteQuery(oConnection, sSelect, adoRS)
            ORs = dm.getDataSetWithSqlAdapter(sSelect, "tblTradeInHeader", "tblTradeInHeader")
            'Detail delete
            ORs = Nothing
            sSelect = "Delete from tblTradeInDetail WHERE InventoryCode =" & fncFormatSQLText(SInventoryno)
            'TransactionExecuteQuery(oConnection, sSelect, adoRS)
            ORs = dm.getDataSetWithSqlAdapter(sSelect, "tblTradeInDetail", "tblTradeInDetail")
            ORs = Nothing
            TradeInDelete = True
            sError = ""
            Exit Function

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            adoRS = Nothing
        End Try
    End Function

    
    'TradeInHeaderSave
    Public Function TradeInHeaderSave(ByRef oConnection As ADODB.Connection, ByRef adoRS As ADODB.Recordset, _
                                ByVal sinventorycode As String, ByVal sInvoiceNo As String, ByVal sLocationCode As String, _
                                ByVal sCustomerCode As String, ByVal sMemberCode As String, ByVal sCustomerName As String, _
                                ByVal sAddress1 As String, ByVal sAddress2 As String, _
                                ByVal sAddress3 As String, ByVal nCostPerGram As Double, _
                                ByVal nWeight As Double, ByVal nStoneWeight As Double, _
                                ByVal nMC As Double, ByVal nPCS As Double, ByVal nGoldLoss As Double, ByVal nTotalCost As Double, _
                                ByVal dAmount As Double, _
                                ByVal sRemarks As String, ByVal sSoldFlag As String, ByVal sUserId As String, ByRef sError As String) As Boolean
        Dim sExec As String
        Dim sErrDesc As String
        Dim lErrNo As Long
        Dim ORs As DataSet
        'Dim sPayMode$, dAmount As Double
        Try
            TradeInHeaderSave = False

            nMC = 0
            nPCS = 1

            sExec = ""
            sExec = "EXEC sp_TradeInHeader "
            sExec = sExec & fncFormatSQLText(sinventorycode) & ", "
            sExec = sExec & fncFormatSQLText(sInvoiceNo) & ", "
            sExec = sExec & fncFormatSQLText(sLocationCode) & ", "
            sExec = sExec & fncFormatSQLText(sCustomerCode) & ", "
            sExec = sExec & fncFormatSQLText(sMemberCode) & ", "
            sExec = sExec & fncFormatSQLText(sCustomerName) & ", "
            sExec = sExec & fncFormatSQLText(sAddress1) & ", "
            sExec = sExec & fncFormatSQLText(sAddress2) & ", "
            sExec = sExec & fncFormatSQLText(sAddress3) & ", "
            sExec = sExec & Val(nCostPerGram) & ", "
            sExec = sExec & Val(nWeight) & ", "
            sExec = sExec & Val(nStoneWeight) & ", "
            sExec = sExec & Val(nMC) & ", "
            sExec = sExec & Val(nPCS) & ", "
            sExec = sExec & Val(nGoldLoss) & ", "
            sExec = sExec & Val(nTotalCost) & ", "
            'sExec = sExec & fncFormatSQLText(sPayMode) & ", "
            sExec = sExec & Val(dAmount) & ", "
            sExec = sExec & fncFormatSQLText(sRemarks) & ", "
            sExec = sExec & fncFormatSQLText(sSoldFlag) & ", "
            sExec = sExec & fncFormatSQLText(sUserId)
            'TransactionExecuteQuery(oConnection, sExec, adoRS)
            ORs = dm.getDataSetWithSqlAdapter(sExec, "tblTradeInHeader", "tblTradeInHeader")
            TradeInHeaderSave = True
            sError = ""
            Exit Function
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function
    Public Function TradeInDetailSave(ByRef oConnection As ADODB.Connection, ByRef adoRS As ADODB.Recordset, _
                                ByVal sLocationCode As String, ByVal sinventorycode As String, ByVal nItemNo As Long, _
                                ByVal sDescription As String, ByVal sDiamondDescription As String, _
                                ByVal nWeight As Double, ByVal nStoneWeight As Double, ByVal nCostPerGram As Double, _
                                ByVal nMC As Double, ByVal nPCS As Double, ByVal nGoldLoss As Double, ByVal nTotalCost As Double, _
                                ByVal sCategoryCode As String, ByVal sClassCode As String, ByVal sCatalogueCode As String, _
                                ByVal sRemarks As String, ByVal sClass As String, ByVal sUserId As String, ByRef sError As String) As Boolean
        Dim sExec As String
        Dim sErrDesc As String
        Dim sVal As String
        Dim lErrNo As Long
        Dim lCustID As Long
        Dim Ors As DataSet


        'for trade-in, no need to save pcs and nMC
        nMC = 0
        nPCS = 1


        On Error GoTo ErrHandler
        TradeInDetailSave = False

        sExec = ""
        sExec = "EXEC sp_TradeInDetail"
        sExec = sExec & fncFormatSQLText(sLocationCode) & ", "
        sExec = sExec & fncFormatSQLText(sinventorycode) & ", "
        sExec = sExec & Val(nItemNo) & ", "
        sExec = sExec & fncFormatSQLText(sDescription) & ", "
        sExec = sExec & fncFormatSQLText(sDiamondDescription) & ", "
        sExec = sExec & Val(nWeight) & ", "
        sExec = sExec & Val(nStoneWeight) & ", "
        sExec = sExec & Val(nCostPerGram) & ", "
        sExec = sExec & Val(nMC) & ", "
        sExec = sExec & Val(nPCS) & ", "
        sExec = sExec & Val(nGoldLoss) & ", "
        sExec = sExec & Val(nTotalCost) & ", "
        sExec = sExec & fncFormatSQLText(sCategoryCode) & ", "
        sExec = sExec & fncFormatSQLText(sClassCode) & ", "
        sExec = sExec & fncFormatSQLText(sCatalogueCode) & ", "
        sExec = sExec & fncFormatSQLText(sRemarks) & ", "
        sExec = sExec & fncFormatSQLText(sClass) & ", "
        sExec = sExec & fncFormatSQLText("") & ", "
        sExec = sExec & fncFormatSQLText(sUserId)
        'TransactionExecuteQuery(oConnection, sExec, adoRS)
        Ors = dm.getDataSetWithSqlAdapter(sExec, "tblTradeInDetail", "tblTradeInDetail")

        TradeInDetailSave = True
        sError = ""
        Exit Function
ErrHandler:
        sError = Err.Description
    End Function
    Public Function GetTradeInList(ByVal sinventorycode As String) As ADODB.Recordset

        Dim ORs As ADODB.Recordset
        Dim sSelect As String
        Try
            GetTradeInList = Nothing
            sSelect = "SELECT * FROM tblTradeIn where InventoryCode = " & fncFormatSQLText(sinventorycode)
            'GetRecordset(gsConn, sSelect, ORs)
            ORs = dm.getDataSetWithSqlAdapter(sSelect, "tblTradeIn", "tblTradeIn")
            GetTradeInList = ORs
            ORs = Nothing
            Exit Function
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        'ErrHandler:
        '        lErrNo = Err.Number
        '        sErrDesc = Err.Description
        '        Resume NextLine
        'NextLine:
        '        On Error Resume Next
        '        ORs = Nothing
        '        'Set m_oDal = Nothing
        '        On Error GoTo 0
        '        Err.Raise(lErrNo, OBJNAME, sErrDesc)
    End Function

    '====================Old Code====================

    Public Function GetTradeInBarCode(ByVal sInventoryCode As String) As DataSet
        Dim Ds As DataSet
        Dim sQry As String
        Try
            sQry = "SELECT * FROM tblTradeInHeader where InvoiceNo = " & fncFormatSQLText(sInventoryCode)
            sQry = sQry & "  and SoldFlag='N'"
            Ds = dm.getDataSetWithSqlAdapter(sQry, "TradeIn", "TradeIn")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetTradeInBarCode = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function getTradeIn(ByVal sInventoryCode As String) As DataSet
        Dim ds As DataSet
        Dim sQry As String
        Try
            sQry = "SELECT * FROM tblTradeInHeader where InventoryCode = " & fncFormatSQLText(sInventoryCode)
            ds = dm.getDataSetWithSqlAdapter(sQry, "TradeIn", "TradeIn")

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            ds = Nothing
        Finally
            getTradeIn = ds
            ds = Nothing
        End Try
    End Function
    Public Function getTradeInDetailForBarCode(ByVal sInvoiceNo As String) As DataSet
        Dim ds As DataSet
        Dim sQry As String
        Try
            sQry = "Select LocationCode,InvoiceNo,convert(varchar(20),CreateDate,3)InvoiceDate,Class,Weight,StoneWeight,TotalCost,InventoryCode from tblTradeinDetail where Invoiceno = " & fncFormatSQLText(sInvoiceNo)
            ds = dm.getDataSetWithSqlAdapter(sQry, "TradeIn", "TradeIn")

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            ds = Nothing
        Finally
            getTradeInDetailForBarCode = ds
            ds = Nothing
        End Try
    End Function
#Region "Added by Sarathiy"

    Public Function GetCostPerGram() As DataSet
        Dim Ds As DataSet
        Dim sQry As String
        Try
            sQry = "Select Rate from tblClass where ClassCode like '916%'"
            Ds = dm.getDataSetWithSqlAdapter(sQry, "TradeIn", "TradeIn")

            If Ds.Tables(0).Rows.Count = 0 Then
                sQry = "Select Rate from tblClass where ClassCode like '18K%'"
                Ds = Nothing
                Ds = dm.getDataSetWithSqlAdapter(sQry, "TradeIn", "TradeIn")
                If Ds.Tables(0).Rows.Count > 0 Then
                    Exit Function
                End If
            Else
                Exit Function
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetCostPerGram = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function


    Public Function TradeInDelete(ByVal SInventoryno As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            'Header delete
            sQry = "Delete from tblTradeInHeader WHERE InventoryCode =" & fncFormatSQLText(SInventoryno)
            SaveStatus = dm.ProjExecNonQry(sQry)
            'Detail delete
            sQry = "Delete from tblTradeInDetail WHERE InventoryCode =" & fncFormatSQLText(SInventoryno)
            SaveStatus = dm.ProjExecNonQry(sQry)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            TradeInDelete = SaveStatus
            sQry = Nothing
        End Try
    End Function


    Public Function TradeInHeaderSave(ByVal sinventorycode As String, ByVal sInvoiceNo As String, ByVal sLocationCode As String, _
                            ByVal sCustomerCode As String, ByVal sCustomerName As String, _
                            ByVal sAddress1 As String, ByVal sAddress2 As String, _
                            ByVal sAddress3 As String, ByVal nCostPerGram As Double, _
                            ByVal nWeight As Double, ByVal nStoneWeight As Double, _
                            ByVal nMC As Double, ByVal nPCS As Double, ByVal nGoldLoss As Double, ByVal nTotalCost As Double, _
                            ByVal dAmount As Double, _
                            ByVal sRemarks As String, ByVal sSoldFlag As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            'for trade-in, no need to save pcs and nMC
            nMC = 0
            nPCS = 1
            sQry = "EXEC sp_TradeInHeader "
            sQry = sQry & fncFormatSQLText(sinventorycode) & ", "
            sQry = sQry & fncFormatSQLText(sInvoiceNo) & ", "
            sQry = sQry & fncFormatSQLText(sLocationCode) & ", "
            sQry = sQry & fncFormatSQLText(sCustomerCode) & ", "
            sQry = sQry & fncFormatSQLText(sCustomerName) & ", "
            sQry = sQry & fncFormatSQLText(sAddress1) & ", "
            sQry = sQry & fncFormatSQLText(sAddress2) & ", "
            sQry = sQry & fncFormatSQLText(sAddress3) & ", "
            sQry = sQry & Val(nCostPerGram) & ", "
            sQry = sQry & Val(nWeight) & ", "
            sQry = sQry & Val(nStoneWeight) & ", "
            sQry = sQry & Val(nMC) & ", "
            sQry = sQry & Val(nPCS) & ", "
            sQry = sQry & Val(nGoldLoss) & ", "
            sQry = sQry & Val(nTotalCost) & ", "
            sQry = sQry & Val(dAmount) & ", "
            sQry = sQry & fncFormatSQLText(sRemarks) & ", "
            sQry = sQry & fncFormatSQLText(sSoldFlag) & ", "
            sQry = sQry & fncFormatSQLText(sUserID)

            SaveStatus = dm.ProjExecNonQry(sQry)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            TradeInHeaderSave = SaveStatus
            sQry = Nothing
        End Try
    End Function

    Public Function TradeInDetailSave(ByVal sLocationCode As String, ByVal sinventorycode As String, ByVal nItemNo As Long, _
                            ByVal sDescription As String, ByVal sDiamondDescription As String, _
                            ByVal nWeight As Double, ByVal nStoneWeight As Double, ByVal nCostPerGram As Double, _
                            ByVal nMC As Double, ByVal nPCS As Double, ByVal nGoldLoss As Double, ByVal nTotalCost As Double, _
                            ByVal sCategoryCode As String, ByVal sClassCode As String, ByVal sCatalogueCode As String, _
                            ByVal sRemarks As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            'for trade-in, no need to save pcs and nMC
            nMC = 0
            nPCS = 1
            sQry = ""
            sQry = "EXEC sp_TradeInDetail"
            sQry = sQry & fncFormatSQLText(sLocationCode) & ", "
            sQry = sQry & fncFormatSQLText(sinventorycode) & ", "
            sQry = sQry & Val(nItemNo) & ", "
            sQry = sQry & fncFormatSQLText(sDescription) & ", "
            sQry = sQry & fncFormatSQLText(sDiamondDescription) & ", "
            sQry = sQry & Val(nWeight) & ", "
            sQry = sQry & Val(nStoneWeight) & ", "
            sQry = sQry & Val(nCostPerGram) & ", "
            sQry = sQry & Val(nMC) & ", "
            sQry = sQry & Val(nPCS) & ", "
            sQry = sQry & Val(nGoldLoss) & ", "
            sQry = sQry & Val(nTotalCost) & ", "
            sQry = sQry & fncFormatSQLText(sCategoryCode) & ", "
            sQry = sQry & fncFormatSQLText(sClassCode) & ", "
            sQry = sQry & fncFormatSQLText(sCatalogueCode) & ", "
            sQry = sQry & fncFormatSQLText(sRemarks) & ", "
            sQry = sQry & fncFormatSQLText(sUserID)

            SaveStatus = dm.ProjExecNonQry(sQry)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            TradeInDetailSave = SaveStatus
            sQry = Nothing
        End Try
    End Function
#End Region

#Region "Trade In from Sales"

    Public Function TradeInDeleteSales(ByVal SInventoryno As String) As Boolean
        Dim sSqry As String
        Try
            TradeInDeleteSales = False
            'Header delete
            sSqry = "Delete from tblTradeInHeader WHERE InventoryCode =" & fncFormatSQLText(SInventoryno)
            'If Not fncExecNonQryTI(sSqry) Then
            If Not fncExecNonQrySales(sSqry) Then
                Exit Function
            End If
            'Detail delete
            sSqry = "Delete from tblTradeInDetail WHERE InventoryCode =" & fncFormatSQLText(SInventoryno)
            'If Not fncExecNonQryTI(sSqry) Then
            If Not fncExecNonQrySales(sSqry) Then
                Exit Function
            End If
            TradeInDeleteSales = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Public Function TradeInHeaderSaveSales(ByVal sinventorycode As String, ByVal sInvoiceNo As String, ByVal sLocationCode As String, _
                                ByVal sCustomerCode As String, ByVal sMemberCode As String, ByVal sCustomerName As String, _
                                ByVal sAddress1 As String, ByVal sAddress2 As String, _
                                ByVal sAddress3 As String, ByVal nCostPerGram As Double, _
                                ByVal nWeight As Double, ByVal nStoneWeight As Double, _
                                ByVal nMC As Double, ByVal nPCS As Double, ByVal nGoldLoss As Double, ByVal nTotalCost As Double, _
                                ByVal dAmount As Double, ByVal sRemarks As String, ByVal sSoldFlag As String, ByVal sUserId As String) As Boolean
        Dim sExec As String
        Dim bStatus As Boolean
        Try
            bStatus = False
            nMC = 0
            nPCS = 1
            sExec = " EXEC sp_TradeInHeader "
            sExec = sExec & fncFormatSQLText(sinventorycode) & ", "
            sExec = sExec & fncFormatSQLText(sInvoiceNo) & ", "
            sExec = sExec & fncFormatSQLText(sLocationCode) & ", "
            sExec = sExec & fncFormatSQLText(sCustomerCode) & ", "
            sExec = sExec & fncFormatSQLText(sMemberCode) & ", "
            sExec = sExec & fncFormatSQLText(sCustomerName) & ", "
            sExec = sExec & fncFormatSQLText(sAddress1) & ", "
            sExec = sExec & fncFormatSQLText(sAddress2) & ", "
            sExec = sExec & fncFormatSQLText(sAddress3) & ", "
            sExec = sExec & Val(nCostPerGram) & ", "
            sExec = sExec & Val(nWeight) & ", "
            sExec = sExec & Val(nStoneWeight) & ", "
            sExec = sExec & Val(nMC) & ", "
            sExec = sExec & Val(nPCS) & ", "
            sExec = sExec & Val(nGoldLoss) & ", "
            sExec = sExec & Val(nTotalCost) & ", "
            'sExec = sExec & fncFormatSQLText(sPayMode) & ", "
            sExec = sExec & Val(dAmount) & ", "
            sExec = sExec & fncFormatSQLText(sRemarks) & ", "
            sExec = sExec & fncFormatSQLText(sSoldFlag) & ", "
            sExec = sExec & fncFormatSQLText(sUserId)
            'bStatus = fncExecNonQryTI(sExec)
            bStatus = fncExecNonQrySales(sExec)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            TradeInHeaderSaveSales = bStatus
        End Try
    End Function
    Public Function fncExecNonQrySales(ByVal Query As String) As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            Dim Cmd As New SqlClient.SqlCommand(Query, gsSalesCon)
            Cmd.Transaction = gsSalesTrans
            Cmd.ExecuteNonQuery()
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncExecNonQrySales = bStatus
        End Try
    End Function
    Public Function TradeInDetailSaveSales(ByVal sLocationCode As String, ByVal sinventorycode As String, ByVal nItemNo As Long, _
                                ByVal sDescription As String, ByVal sDiamondDescription As String, _
                                ByVal nWeight As Double, ByVal nStoneWeight As Double, ByVal nCostPerGram As Double, _
                                ByVal nMC As Double, ByVal nPCS As Double, ByVal nGoldLoss As Double, ByVal nTotalCost As Double, _
                                ByVal sCategoryCode As String, ByVal sClassCode As String, ByVal sCatalogueCode As String, _
                                ByVal sRemarks As String, ByVal sClass As String, ByVal sInvoiceNo As String, ByVal sUserId As String) As Boolean
        Dim sExec As String
        Dim bStatus As Boolean
        Try
            bStatus = False
            nMC = 0
            nPCS = 1
            sExec = "EXEC sp_TradeInDetail"
            sExec = sExec & fncFormatSQLText(sLocationCode) & ", "
            sExec = sExec & fncFormatSQLText(sinventorycode) & ", "
            sExec = sExec & Val(nItemNo) & ", "
            sExec = sExec & fncFormatSQLText(sDescription) & ", "
            sExec = sExec & fncFormatSQLText(sDiamondDescription) & ", "
            sExec = sExec & Val(nWeight) & ", "
            sExec = sExec & Val(nStoneWeight) & ", "
            sExec = sExec & Val(nCostPerGram) & ", "
            sExec = sExec & Val(nMC) & ", "
            sExec = sExec & Val(nPCS) & ", "
            sExec = sExec & Val(nGoldLoss) & ", "
            sExec = sExec & Val(nTotalCost) & ", "
            sExec = sExec & fncFormatSQLText(sCategoryCode) & ", "
            sExec = sExec & fncFormatSQLText(sClassCode) & ", "
            sExec = sExec & fncFormatSQLText(sCatalogueCode) & ", "
            sExec = sExec & fncFormatSQLText(sRemarks) & ", "
            sExec = sExec & fncFormatSQLText(sClass) & ", "
            sExec = sExec & fncFormatSQLText(sInvoiceNo) & ", "
            sExec = sExec & fncFormatSQLText(sUserId)
            'bStatus = fncExecNonQryTI(sExec)
            bStatus = fncExecNonQrySales(sExec)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            TradeInDetailSaveSales = bStatus
        End Try
    End Function

#End Region

#Region "Transaction Info"

    Public Sub subOpenTIConnection()
        gsTICon.ConnectionString = gsCon
        gsTICon.Open()
    End Sub

    Public Sub subBeginTITransaction()
        gsTITrans = gsTICon.BeginTransaction
    End Sub

    Public Sub subCommitTITransaction()
        gsTITrans.Commit()
    End Sub

    Public Sub subRollBackTITransaction()
        gsTITrans.Rollback()
    End Sub

    Public Sub subCloseTIConnection()
        gsTICon.Close()
    End Sub

    Public Function fncExecNonQryTI(ByVal Query As String) As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            Dim Cmd As New SqlClient.SqlCommand(Query, gsTICon)
            Cmd.Transaction = gsTITrans
            Cmd.ExecuteNonQuery()
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncExecNonQryTI = bStatus
        End Try
    End Function

    Public Function fncGetNextTrandeInRefNo() As String
        Dim Ds As New DataSet
        Dim sQry, sNextTradeInRefNo As String
        Try
            sNextTradeInRefNo = ""
            sQry = " Select NextTradeInNo = replicate('0',8-len(convert(char,NextTradeInRefNo)))+convert(char,NextTradeInRefNo) from tblcompany where CompanyID = '1'"
            'Ds = dm.getDataSetWithSqlAdapter(sQry, "Ref", "Ref")
            Ds = dm.getDataSetWithSqlAdapterSales(sQry, "Ref", "Ref")
            If Ds.Tables("Ref").Rows.Count > 0 Then
                sNextTradeInRefNo = Trim(Ds.Tables("Ref").Rows(0)("NextTradeInNo") & "")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncGetNextTrandeInRefNo = sNextTradeInRefNo
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function fncUpdateNextTradeInRefNo() As Boolean
        Dim bStatus As Boolean
        Dim sQry As String
        Try
            sQry = " update tblcompany set NextTradeInRefNo = NextTradeInRefNo + 1 Where CompanyID = '1' "
            'bStatus = fncExecNonQryTI(sQry)
            bStatus = fncExecNonQrySales(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateNextTradeInRefNo = bStatus
            sQry = Nothing
        End Try
    End Function

#End Region


End Class
