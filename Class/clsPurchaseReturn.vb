Public Class clsPurchaseReturn
    Dim Dm As New DataManager
    Public Sub New()
        'Contructor
    End Sub

    Public Function PurchaseReturnHeaderSave(ByVal sSupplierCode As String, ByVal sReturnDate As String, ByVal sLocationCode As String, _
                                        ByVal dTotalWeight As Double, ByVal dTotalPCS As Double, _
                                        ByVal dTotalValue As Double, ByVal dTotalGST As Double, _
                                        ByVal dNetTotal As Double, _
                                        ByVal sRemarks As String, ByVal sUserId As String) As DataSet
        Dim sQry As String
        Dim Ds As DataSet
        Try
            PurchaseReturnHeaderSave = Nothing
            sQry = ""
            sQry = sQry & "EXEC sp_PurchaseReturnHeader "
            sQry = sQry & fncFormatSQLDate(sReturnDate) & ","
            sQry = sQry & fncFormatSQLText(sSupplierCode) & ","
            sQry = sQry & fncFormatSQLText(sLocationCode) & ","
            sQry = sQry & Val(dTotalWeight) & ","
            sQry = sQry & Val(dTotalPCS) & ","
            sQry = sQry & Val(dTotalValue) & ","
            sQry = sQry & Val(dTotalGST) & ","
            sQry = sQry & Val(dNetTotal) & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & fncFormatSQLText(sUserId) & "" & vbCrLf

            Ds = Dm.getDataSetWithSqlAdapter(sQry, "PurChaseRet", "PurChaseRet")
            PurchaseReturnHeaderSave = Ds
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            Ds = Nothing
        Finally
            Ds = Nothing
        End Try
    End Function
    Public Function PurchaseReturnDetailSave(ByVal sPurRetNo As String, ByVal sInvCode As String, ByVal sInvDesc As String, _
                                        ByVal sLocationCode As String, ByVal dWeight As Double, ByVal dGoldLoss As Double, _
                                        ByVal dMC As Double, ByVal dCostPerGram As Double, _
                                        ByVal dPCS As Double, ByVal dTotValue As Double, _
                                        ByVal sRemarks As String, ByVal sUserId As String) As Boolean

        Dim sQry As String
        Dim Ds As DataSet

        Try
            PurchaseReturnDetailSave = False

            sQry = ""
            sQry = sQry & "EXEC sp_PurchaseReturnDetail "
            sQry = sQry & fncFormatSQLText(sPurRetNo) & ","
            sQry = sQry & fncFormatSQLText(sInvCode) & ","
            sQry = sQry & fncFormatSQLText(sInvDesc) & ","
            sQry = sQry & fncFormatSQLText(sLocationCode) & ","
            sQry = sQry & Val(dWeight) & ","
            sQry = sQry & Val(dGoldLoss) & ","
            sQry = sQry & Val(dMC) & ","
            sQry = sQry & Val(dCostPerGram) & ","
            sQry = sQry & Val(dPCS) & ","
            sQry = sQry & Val(dTotValue) & ","
            sQry = sQry & fncFormatSQLText(sUserId)

            Ds = Dm.getDataSetWithSqlAdapter(sQry, "PurchaseRet", "PurchaseRet")
            PurchaseReturnDetailSave = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try

    End Function

    Public Function NextPurchaseReturnNo() As String
        On Error GoTo errTrap
        Dim NextNo As String
        Dim NextRInvoice As String
        NextNo = Dm.ValReturnProjExecNonQry("Select NextPurReNo from tblCompanyAP where CompanyID='" & Trim(CompId) & "'")
        If Val(NextNo) <> 0 Then
            NextRInvoice = Trim(sLocationCode) & "-PR" & Format(Val(NextNo + 1), "0000000")
        Else
            NextRInvoice = Trim(sLocationCode) & "-PR0000001"
        End If
        NextPurchaseReturnNo = NextRInvoice
        Exit Function
errTrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Function
    Public Function fnSavePurchaseReturnHeader(ByVal ReturnNo As String, ByVal ReturnDate As String, ByVal ReturnType As String, _
    ByVal VendorCode As String, ByVal CurrCode As String, ByVal CurrRate As Double, ByVal Location As String, ByVal Department As String, _
    ByVal RefInvoiceNo As String, ByVal TotalAmount As Double, ByVal TotalGst As Double, _
    ByVal NetAmount As Double, ByVal NofItems As Long, ByVal TotalQty As Long, ByVal Remarks As String, _
    ByVal PRJournalNo As String, ByVal Status As String, ByVal CompanyCode As String, ByVal CreateUser As String, _
    ByVal CreateDate As String, ByVal ModifyUser As String, ByVal ModifyDate As String, ByVal Mode As String) As String
        Dim sExec As String
        On Error GoTo ErrTrap
        fnSavePurchaseReturnHeader = False
        sExec = ""
        sExec = "sp_PurchaseReturnHeader "
        sExec &= fncFormatSQLText(ReturnNo) & "," & fncFormatSQLDate(ReturnDate) & "," & fncFormatSQLText(ReturnType) & ","
        sExec &= fncFormatSQLText(VendorCode) & "," & fncFormatSQLText(CurrCode) & "," & CurrRate & "," & fncFormatSQLText(Location) & "," & fncFormatSQLText(Department) & ","
        sExec &= fncFormatSQLText(RefInvoiceNo) & "," & TotalAmount & "," & TotalGst & ","
        sExec &= NetAmount & "," & NofItems & "," & TotalQty & "," & fncFormatSQLText(Remarks) & ","
        sExec &= fncFormatSQLText(PRJournalNo) & "," & fncFormatSQLText(Status) & "," & fncFormatSQLText(CompanyCode) & "," & fncFormatSQLText(CreateUser) & ","
        sExec &= fncFormatSQLDate(CreateDate) & "," & fncFormatSQLText(ModifyUser) & "," & fncFormatSQLDate(ModifyDate) & "," & fncFormatSQLText(Mode)
        If Mode = "I" Then
            fnSavePurchaseReturnHeader = Dm.ReturnProjExecNonQry(sExec)
        Else
            If Dm.ProjExecNonQry(sExec) = False Then
                fnSavePurchaseReturnHeader = "F"
            Else
                fnSavePurchaseReturnHeader = "T"
            End If
        End If

        Exit Function
ErrTrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Function
    Public Function fnSavePurchaseReturnDetails(ByVal ReturnNo As String, ByVal SlNo As Long, ByVal InventoryCode As String, ByVal Description As String, _
    ByVal ReturnQty As Long, ByVal Price As Double, ByVal Amount As Double, ByVal UomCode As String, _
    ByVal ItemType As String, ByVal CreateUser As String, ByVal CreateDate As String, ByVal ModifyUser As String, ByVal ModifyDate As String, ByVal Mode As String) As Boolean
        Dim sExec As String
        On Error GoTo ErrTrap
        fnSavePurchaseReturnDetails = False
        sExec = ""
        sExec = "sp_PurchaseReturnDetail "
        sExec &= fncFormatSQLText(ReturnNo) & "," & SlNo & "," & fncFormatSQLText(InventoryCode) & "," & fncFormatSQLText(Description) & ","
        sExec &= ReturnQty & "," & Price & "," & Amount & "," & fncFormatSQLText(UomCode) & ","
        sExec &= fncFormatSQLText(ItemType) & "," & fncFormatSQLText(CreateUser) & "," & fncFormatSQLDate(CreateDate) & "," & fncFormatSQLText(ModifyUser) & "," & fncFormatSQLDate(ModifyDate) & "," & fncFormatSQLText(Mode)
        fnSavePurchaseReturnDetails = Dm.ProjExecNonQry(sExec)
        Exit Function

ErrTrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Function
    Public Function NextPurchaseReturnJournlNo() As String
        On Error GoTo errtrap
        Dim NextNo As String
        Dim NextJNo As String
        NextNo = Dm.ValReturnProjExecNonQry("Select NextPurchaceReturn from tblParameters where CompanyID='" & Trim(CompId) & "'")
        If Val(NextNo) <> 0 Then
            NextJNo = "PR-" & Format(Val(NextNo), "00000000")
        Else
            NextJNo = "PR-00000001"
        End If
        NextPurchaseReturnJournlNo = NextJNo
        Exit Function
errtrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Function
    Public Function UpdatePRInvoiceWithRefJournlNo(ByVal RefJVNo As String, ByVal InvoiceNo As String) As Boolean
        On Error GoTo ErrTrap
        UpdatePRInvoiceWithRefJournlNo = False
        Dim sExec As String
        sExec = ""
        sExec = " Exec Sp_UpdatePRInvoicePJUpdate " & fncFormatSQLText(InvoiceNo) & "," & fncFormatSQLText(RefJVNo)
        Dm.ProjExecNonQry(sExec)

        UpdatePRInvoiceWithRefJournlNo = True
        Exit Function
ErrTrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Function
    Public Sub subDeletePurchaseReturn(ByVal ReturnNo As String)
        Dm.ProjExecNonQry("Exec Sp_DeletePurchaseReturn " & fncFormatSQLText(ReturnNo))
    End Sub
    ''''''
    ''''''
    '''''' For Purchase Return HQ
    Public Function fnSavePurchaseReturnHeaderHQ(ByVal ReturnNo As String, ByVal ReturnDate As String, ByVal ReturnType As String, _
    ByVal VendorCode As String, ByVal CurrCode As String, ByVal CurrRate As Double, ByVal Location As String, ByVal Department As String, _
    ByVal RefInvoiceNo As String, ByVal TotalAmount As Double, ByVal TotalGst As Double, _
    ByVal NetAmount As Double, ByVal NofItems As Long, ByVal TotalWQty As Long, ByVal TotalLQty As Long, ByVal Remarks As String, _
    ByVal PRJournalNo As String, ByVal RefReturnNo As String, ByVal RefReturnDate As String, ByVal Status As String, ByVal CompanyCode As String, ByVal CreateUser As String, _
    ByVal CreateDate As String, ByVal ModifyUser As String, ByVal ModifyDate As String, ByVal Mode As String) As String
        Dim sExec As String
        On Error GoTo ErrTrap
        fnSavePurchaseReturnHeaderHQ = False
        sExec = ""
        sExec = "sp_PurchaseReturnHeaderHQ "
        sExec &= fncFormatSQLText(ReturnNo) & "," & fncFormatSQLDate(ReturnDate) & "," & fncFormatSQLText(ReturnType) & ","
        sExec &= fncFormatSQLText(VendorCode) & "," & fncFormatSQLText(CurrCode) & "," & CurrRate & "," & fncFormatSQLText(Location) & "," & fncFormatSQLText(Department) & ","
        sExec &= fncFormatSQLText(RefInvoiceNo) & "," & TotalAmount & "," & TotalGst & ","
        sExec &= NetAmount & "," & NofItems & "," & TotalWQty & "," & TotalLQty & "," & fncFormatSQLText(Remarks) & ","
        sExec &= fncFormatSQLText(PRJournalNo) & "," & fncFormatSQLText(RefReturnNo) & "," & fncFormatSQLDate(RefReturnDate) & "," & fncFormatSQLText(Status) & "," & fncFormatSQLText(CompanyCode) & "," & fncFormatSQLText(CreateUser) & ","
        sExec &= fncFormatSQLDate(CreateDate) & "," & fncFormatSQLText(ModifyUser) & "," & fncFormatSQLDate(ModifyDate) & "," & fncFormatSQLText(Mode)
        If Mode = "I" Then
            fnSavePurchaseReturnHeaderHQ = Dm.ReturnProjExecNonQry(sExec)
        Else
            If Dm.ProjExecNonQry(sExec) = False Then
                fnSavePurchaseReturnHeaderHQ = "F"
            Else
                fnSavePurchaseReturnHeaderHQ = "T"
            End If
        End If

        Exit Function
ErrTrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Function
    Public Function fnSavePurchaseReturnDetailsHQ(ByVal ReturnNo As String, _
                                                    ByVal SlNo As Long, _
                                                    ByVal InventoryCode As String, _
                                                    ByVal Description As String, _
                                                    ByVal WReturnQty As Long, _
                                                    ByVal LReturnQty As Long, _
                                                    ByVal Price As Double, _
                                                    ByVal Amount As Double, _
                                                    ByVal UomCode As String, _
                                                    ByVal WUnit As Long, _
                                                    ByVal PODiscount As Double, _
                                                    ByVal GST As Double, _
                                                    ByVal NetAmount As Double, _
                                                    ByVal CreateUser As String, _
                                                    ByVal CreateDate As String, _
                                                    ByVal ModifyUser As String, _
                                                    ByVal ModifyDate As String, _
                                                    ByVal Mode As String) As Boolean
        Dim sExec As String
        On Error GoTo ErrTrap
        fnSavePurchaseReturnDetailsHQ = False
        sExec = ""
        sExec = "sp_PurchaseReturnDetailHQ "
        sExec &= fncFormatSQLText(ReturnNo) & "," & SlNo & "," & fncFormatSQLText(InventoryCode) & "," & fncFormatSQLText(Description) & ","
        sExec &= WReturnQty & "," & LReturnQty & "," & Price & "," & Amount & "," & fncFormatSQLText(UomCode) & "," & WUnit & ","
        sExec &= (PODiscount) & "," & GST & "," & NetAmount & "," & fncFormatSQLText(CreateUser) & "," & fncFormatSQLDate(CreateDate) & "," & fncFormatSQLText(ModifyUser) & "," & fncFormatSQLDate(ModifyDate) & "," & fncFormatSQLText(Mode)
        fnSavePurchaseReturnDetailsHQ = Dm.ProjExecNonQry(sExec)
        Exit Function

ErrTrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Function
    Public Function subDeletePurchaseReturnHQ(ByVal ReturnNo As String) As Boolean
        On Error GoTo errTrap
        subDeletePurchaseReturnHQ = False
        Dm.ProjExecNonQry("Exec Sp_DeletePurchaseReturnHQ " & fncFormatSQLText(ReturnNo))
        subDeletePurchaseReturnHQ = True
        Exit Function
errTrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Function
    Public Function NextStkAdjNo() As String
        On Error GoTo errtrap
        Dim NextNo As String
        Dim NextJNo As String
        NextNo = Dm.ValReturnProjExecNonQry("Select NextStockAdjustment from tblParameters")
        If Val(NextNo) <> 0 Then
            NextJNo = "SP-" & Format(Val(NextNo), "00000000")
        Else
            NextJNo = "SP-00000001"
        End If
        NextStkAdjNo = NextJNo
        Exit Function
errtrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Function

    ' Batch 
    Public Function OpenBatchPostinConnection()
        'Dim X As System.Data.SqlClient.SqlDataReader
        gsConBatchPosting.ConnectionString = gsCon
        gsConBatchPosting.Open()
        ' Dim Cmd As New SqlClient.SqlCommand("Select * from tblParameters with (TABLOCKX)", gsConBatchPosting)
        ' X = Cmd.ExecuteReader()
    End Function
    Public Function gpBeginTransaction()
        gsTrans = gsConBatchPosting.BeginTransaction
    End Function
    Public Function gpCommitTransaction()
        gsTrans.Commit()
    End Function
    Public Function NonQryBatchPosting(ByVal Query As String) As Boolean
        On Error GoTo ErrTrap
        NonQryBatchPosting = False
        Dim Cmd As New SqlClient.SqlCommand(Query, gsConBatchPosting)
        Cmd.Transaction = gsTrans
        Cmd.ExecuteNonQuery()
        NonQryBatchPosting = True
        Exit Function
ErrTrap:
        NonQryBatchPosting = False
        MsgBox(Err.Description)
        Err.Clear()
    End Function
    Public Function CloseBatchConnection()
        gsConBatchPosting.Close()
    End Function
    Public Function gpRollBackBatchConnection()
        gsTrans.Rollback()
    End Function
    Public Function fnSavePurchaseReturnHeaderWithTran(ByVal ReturnNo As String, ByVal ReturnDate As String, ByVal ReturnType As String, _
   ByVal VendorCode As String, ByVal CurrCode As String, ByVal CurrRate As Double, ByVal Location As String, ByVal Department As String, _
   ByVal RefInvoiceNo As String, ByVal TotalAmount As Double, ByVal TotalGst As Double, _
   ByVal NetAmount As Double, ByVal NofItems As Long, ByVal TotalQty As Long, ByVal Remarks As String, _
   ByVal PRJournalNo As String, ByVal Status As String, ByVal CompanyCode As String, ByVal CreateUser As String, _
   ByVal CreateDate As String, ByVal ModifyUser As String, ByVal ModifyDate As String, ByVal Mode As String) As String
        Dim sExec As String
        On Error GoTo ErrTrap
        sExec = ""
        sExec = "sp_PurchaseReturnHeader "
        sExec &= fncFormatSQLText(ReturnNo) & "," & fncFormatSQLDate(ReturnDate) & "," & fncFormatSQLText(ReturnType) & ","
        sExec &= fncFormatSQLText(VendorCode) & "," & fncFormatSQLText(CurrCode) & "," & CurrRate & "," & fncFormatSQLText(Location) & "," & fncFormatSQLText(Department) & ","
        sExec &= fncFormatSQLText(RefInvoiceNo) & "," & TotalAmount & "," & TotalGst & ","
        sExec &= NetAmount & "," & NofItems & "," & TotalQty & "," & fncFormatSQLText(Remarks) & ","
        sExec &= fncFormatSQLText(PRJournalNo) & "," & fncFormatSQLText(Status) & "," & fncFormatSQLText(CompanyCode) & "," & fncFormatSQLText(CreateUser) & ","
        sExec &= fncFormatSQLDate(CreateDate) & "," & fncFormatSQLText(ModifyUser) & "," & fncFormatSQLDate(ModifyDate) & "," & fncFormatSQLText(Mode)
        'Dm.ReturnProjExecNonQry(SqlQry)
        Dim Cmd As New SqlClient.SqlCommand(sExec, gsConBatchPosting)
        Cmd.Transaction = gsTrans
        fnSavePurchaseReturnHeaderWithTran = Cmd.ExecuteScalar()
        'fnSaveReceiptOtherHeaderWithTran = True
        Exit Function
ErrTrap:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
        Err.Clear()
    End Function
    Public Function fnSavePurchaseReturnDetailsWithTran(ByVal ReturnNo As String, ByVal SlNo As Long, ByVal InventoryCode As String, ByVal Description As String, _
    ByVal ReturnQty As Long, ByVal Price As Double, ByVal Amount As Double, ByVal UomCode As String, _
    ByVal ItemType As String, ByVal CreateUser As String, ByVal CreateDate As String, ByVal ModifyUser As String, ByVal ModifyDate As String, ByVal Mode As String) As Boolean

        Dim sExec As String
        On Error GoTo ErrTrap

        sExec = ""
        sExec = "sp_PurchaseReturnDetail "
        sExec &= fncFormatSQLText(ReturnNo) & "," & SlNo & "," & fncFormatSQLText(InventoryCode) & "," & fncFormatSQLText(Description) & ","
        sExec &= ReturnQty & "," & Price & "," & Amount & "," & fncFormatSQLText(UomCode) & ","
        sExec &= fncFormatSQLText(ItemType) & "," & fncFormatSQLText(CreateUser) & "," & fncFormatSQLDate(CreateDate) & "," & fncFormatSQLText(ModifyUser) & "," & fncFormatSQLDate(ModifyDate) & "," & fncFormatSQLText(Mode)
        Dim Cmd As New SqlClient.SqlCommand(sExec, gsConBatchPosting)
        Cmd.Transaction = gsTrans
        fnSavePurchaseReturnDetailsWithTran = Cmd.ExecuteNonQuery()
        fnSavePurchaseReturnDetailsWithTran = True
        Exit Function
ErrTrap:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
        fnSavePurchaseReturnDetailsWithTran = False
        Err.Clear()
    End Function
    Public Function fnSavePurchaseReturnHeaderHQWithTran(ByVal ReturnNo As String, ByVal ReturnDate As String, ByVal ReturnType As String, _
    ByVal VendorCode As String, ByVal CurrCode As String, ByVal CurrRate As Double, ByVal Location As String, ByVal Department As String, _
    ByVal RefInvoiceNo As String, ByVal TotalAmount As Double, ByVal TotalGst As Double, _
    ByVal NetAmount As Double, ByVal NofItems As Long, ByVal TotalWQty As Long, ByVal TotalLQty As Long, ByVal Remarks As String, _
    ByVal PRJournalNo As String, ByVal RefReturnNo As String, ByVal RefReturnDate As String, ByVal Status As String, ByVal CompanyCode As String, ByVal CreateUser As String, _
    ByVal CreateDate As String, ByVal ModifyUser As String, ByVal ModifyDate As String, ByVal Mode As String) As String
        Dim sExec As String
        On Error GoTo ErrTrap
        sExec = ""
        sExec = "sp_PurchaseReturnHeaderHQ "
        sExec &= fncFormatSQLText(ReturnNo) & "," & fncFormatSQLDate(ReturnDate) & "," & fncFormatSQLText(ReturnType) & ","
        sExec &= fncFormatSQLText(VendorCode) & "," & fncFormatSQLText(CurrCode) & "," & CurrRate & "," & fncFormatSQLText(Location) & "," & fncFormatSQLText(Department) & ","
        sExec &= fncFormatSQLText(RefInvoiceNo) & "," & TotalAmount & "," & TotalGst & ","
        sExec &= NetAmount & "," & NofItems & "," & TotalWQty & "," & TotalLQty & "," & fncFormatSQLText(Remarks) & ","
        sExec &= fncFormatSQLText(PRJournalNo) & "," & fncFormatSQLText(RefReturnNo) & "," & fncFormatSQLDate(RefReturnDate) & "," & fncFormatSQLText(Status) & "," & fncFormatSQLText(CompanyCode) & "," & fncFormatSQLText(CreateUser) & ","
        sExec &= fncFormatSQLDate(CreateDate) & "," & fncFormatSQLText(ModifyUser) & "," & fncFormatSQLDate(ModifyDate) & "," & fncFormatSQLText(Mode)
        'Dm.ReturnProjExecNonQry(SqlQry)
        Dim Cmd As New SqlClient.SqlCommand(sExec, gsConBatchPosting)
        Cmd.Transaction = gsTrans
        fnSavePurchaseReturnHeaderHQWithTran = Cmd.ExecuteScalar()
        'fnSaveReceiptOtherHeaderWithTran = True
        Exit Function
ErrTrap:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
        Err.Clear()
    End Function
    Public Function fnSavePurchaseReturnDetailsHQWithTran(ByVal ReturnNo As String, _
                                                    ByVal SlNo As Long, _
                                                    ByVal InventoryCode As String, _
                                                    ByVal Description As String, _
                                                    ByVal WReturnQty As Long, _
                                                    ByVal LReturnQty As Long, _
                                                    ByVal Price As Double, _
                                                    ByVal Amount As Double, _
                                                    ByVal UomCode As String, _
                                                    ByVal WUnit As Long, _
                                                    ByVal PODiscount As Double, _
                                                    ByVal GST As Double, _
                                                    ByVal NetAmount As Double, _
                                                    ByVal CreateUser As String, _
                                                    ByVal CreateDate As String, _
                                                    ByVal ModifyUser As String, _
                                                    ByVal ModifyDate As String, _
                                                    ByVal Mode As String) As Boolean
        Dim sExec As String
        On Error GoTo ErrTrap

        sExec = ""
        sExec = "sp_PurchaseReturnDetailHQ "
        sExec &= fncFormatSQLText(ReturnNo) & "," & SlNo & "," & fncFormatSQLText(InventoryCode) & "," & fncFormatSQLText(Description) & ","
        sExec &= WReturnQty & "," & LReturnQty & "," & Price & "," & Amount & "," & fncFormatSQLText(UomCode) & "," & WUnit & ","
        sExec &= (PODiscount) & "," & GST & "," & NetAmount & "," & fncFormatSQLText(CreateUser) & "," & fncFormatSQLDate(CreateDate) & "," & fncFormatSQLText(ModifyUser) & "," & fncFormatSQLDate(ModifyDate) & "," & fncFormatSQLText(Mode)
        Dim Cmd As New SqlClient.SqlCommand(sExec, gsConBatchPosting)
        Cmd.Transaction = gsTrans
        fnSavePurchaseReturnDetailsHQWithTran = Cmd.ExecuteNonQuery()
        'fnSaveReceiptOtherHeaderWithTran = True
        Exit Function
ErrTrap:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
        Err.Clear()
    End Function
    Public Function subDeletePurchaseReturnHQWithTran(ByVal ReturnNo As String) As Boolean
        On Error GoTo errTrap
        subDeletePurchaseReturnHQWithTran = False
        'Dm.ProjExecNonQry("Exec Sp_DeletePurchaseReturnHQ " & fncFormatSQLText(ReturnNo))
        Dim Cmd As New SqlClient.SqlCommand("Exec Sp_DeletePurchaseReturnHQ " & fncFormatSQLText(ReturnNo), gsConBatchPosting)
        Cmd.Transaction = gsTrans
        Cmd.ExecuteNonQuery()
        subDeletePurchaseReturnHQWithTran = True
        Exit Function
errTrap:
        subDeletePurchaseReturnHQWithTran = False
    End Function

    Public Function subDeletePurchaseReturnWithTran(ByVal ReturnNo As String) As Boolean
        On Error GoTo ErrTrap
        subDeletePurchaseReturnWithTran = False
        Dim Cmd As New SqlClient.SqlCommand("Exec Sp_DeletePurchaseReturn " & fncFormatSQLText(ReturnNo), gsConBatchPosting)
        Cmd.Transaction = gsTrans
        Cmd.ExecuteNonQuery()
        'Dm.ProjExecNonQry("Exec Sp_DeletePurchaseReturn " & fncFormatSQLText(ReturnNo))
        subDeletePurchaseReturnWithTran = True
        Exit Function
ErrTrap:
        subDeletePurchaseReturnWithTran = False
    End Function

    Public Function fnSavePurchaseReturnDetailsRPWithTran(ByVal ReturnNo As String, ByVal SlNo As Long, _
                                                    ByVal InventoryCode As String, ByVal Description As String, _
                                                    ByVal ReturnQty As Long, ByVal Price As Double, _
                                                    ByVal Amount As Double, ByVal UomCode As String, _
                                                    ByVal ItemType As String, ByVal PODiscount As Double, _
                                                    ByVal NetAmount As Double, ByVal CreateUser As String, _
                                                    ByVal CreateDate As String, ByVal ModifyUser As String, _
                                                    ByVal ModifyDate As String, ByVal Mode As String) As Boolean
        Dim sExec As String
        On Error GoTo ErrTrap

        sExec = ""
        sExec = "SP_PurchaseReturnDetailRP "
        sExec &= fncFormatSQLText(ReturnNo) & "," & SlNo & "," & fncFormatSQLText(InventoryCode) & "," & fncFormatSQLText(Description) & ","
        sExec &= ReturnQty & "," & Price & "," & Amount & "," & fncFormatSQLText(UomCode) & "," & fncFormatSQLText(ItemType) & ","
        sExec &= PODiscount & "," & NetAmount & "," & fncFormatSQLText(CreateUser) & "," & fncFormatSQLDate(CreateDate) & "," & fncFormatSQLText(ModifyUser) & "," & fncFormatSQLDate(ModifyDate) & "," & fncFormatSQLText(Mode)
        Dim Cmd As New SqlClient.SqlCommand(sExec, gsConBatchPosting)
        Cmd.Transaction = gsTrans
        fnSavePurchaseReturnDetailsRPWithTran = Cmd.ExecuteNonQuery()
        Exit Function
ErrTrap:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
        Err.Clear()
    End Function

    Public Function fnSavePurchaseReturnHeaderRPWithTran(ByVal ReturnNo As String, ByVal ReturnDate As String, ByVal ReturnType As String, _
    ByVal VendorCode As String, ByVal CurrCode As String, ByVal CurrRate As Double, ByVal Location As String, ByVal Department As String, _
    ByVal RefInvoiceNo As String, ByVal TotalAmount As Double, ByVal TotalGst As Double, _
    ByVal NetAmount As Double, ByVal NofItems As Long, ByVal TotalWQty As Long, ByVal TotalLQty As Long, ByVal Remarks As String, _
    ByVal PRJournalNo As String, ByVal RefReturnNo As String, ByVal RefReturnDate As String, ByVal Status As String, ByVal CompanyCode As String, ByVal CreateUser As String, _
    ByVal CreateDate As String, ByVal ModifyUser As String, ByVal ModifyDate As String, ByVal Mode As String) As String
        Dim sExec As String
        On Error GoTo ErrTrap
        sExec = ""
        sExec = "sp_PurchaseReturnHeaderRP "
        sExec &= fncFormatSQLText(ReturnNo) & "," & fncFormatSQLDate(ReturnDate) & "," & fncFormatSQLText(ReturnType) & ","
        sExec &= fncFormatSQLText(VendorCode) & "," & fncFormatSQLText(CurrCode) & "," & CurrRate & "," & fncFormatSQLText(Location) & "," & fncFormatSQLText(Department) & ","
        sExec &= fncFormatSQLText(RefInvoiceNo) & "," & TotalAmount & "," & TotalGst & ","
        sExec &= NetAmount & "," & NofItems & "," & TotalWQty & "," & TotalLQty & "," & fncFormatSQLText(Remarks) & ","
        sExec &= fncFormatSQLText(PRJournalNo) & "," & fncFormatSQLText(RefReturnNo) & "," & fncFormatSQLDate(RefReturnDate) & "," & fncFormatSQLText(Status) & "," & fncFormatSQLText(CompanyCode) & "," & fncFormatSQLText(CreateUser) & ","
        sExec &= fncFormatSQLDate(CreateDate) & "," & fncFormatSQLText(ModifyUser) & "," & fncFormatSQLDate(ModifyDate) & "," & fncFormatSQLText(Mode)
        'Dm.ReturnProjExecNonQry(SqlQry)
        Dim Cmd As New SqlClient.SqlCommand(sExec, gsConBatchPosting)
        Cmd.Transaction = gsTrans
        fnSavePurchaseReturnHeaderRPWithTran = Cmd.ExecuteScalar()
        'fnSaveReceiptOtherHeaderWithTran = True
        Exit Function
ErrTrap:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
        Err.Clear()
    End Function
    Public Function RFIDItemStatusUpdate(ByVal sInventorycode As String) As Boolean
        Dim bStatus As Boolean
        Dim sQry As String
        Try
            sQry = "update Item set ToDisplayInStockTake = '0',status='PR' Where ItemCode=" & fncFormatSQLText(sInventorycode)
            bStatus = ProjExecNonQryRFID(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            RFIDItemStatusUpdate = bStatus
            sQry = Nothing
        End Try
    End Function
    Public Shared Function ProjExecNonQryRFID(ByVal Query As String) As Boolean
        ProjExecNonQryRFID = True
        Try
            Dim Cn As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(gsConRFID)
            Cn.Open()
            Dim Cmd As New SqlClient.SqlCommand(Query)
            Cmd.Connection = Cn
            Cmd.CommandTimeout = 600
            Cmd.ExecuteNonQuery()
            Cn.Close()
            Cn.Dispose()
            Cn = Nothing
        Catch ex As Exception
            Try
                MsgBox("Error in Exec Record")
                ProjExecNonQryRFID = False
            Catch
            End Try
            Throw
            MsgBox("unexpected error")
            ProjExecNonQryRFID = False
        End Try
    End Function
End Class
