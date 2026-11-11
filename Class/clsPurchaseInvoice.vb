Public Class clsPurchaseInvoice
    Dim Dm As New DataManager
    Public Sub New()
    End Sub
    Public Function UpdateInvoiceWithRefJournlNo(ByVal RefJVNo As String, ByVal InvoiceNo As String) As Boolean
        On Error GoTo ErrTrap
        Dim DM As New DataManager

        UpdateInvoiceWithRefJournlNo = False
        Dim sExec As String
        sExec = ""
        sExec = " Exec Sp_UpdateInvoicePJUpdate " & fncFormatSQLText(InvoiceNo) & "," & fncFormatSQLText(RefJVNo)
        DM.ProjExecNonQryAcc(sExec)

        UpdateInvoiceWithRefJournlNo = True
        Exit Function
ErrTrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Function
    '    Public Sub FnFillPurchaseInvoiceData(ByVal FinMonth As Integer, ByVal FinYear As Integer)
    '        On Error GoTo ErrTrap
    '        ClsObjDataSet.SetMasterDataSet(" Exec sp_getInvoiceHeader " & Val(FinMonth) & "," & Val(FinYear) & "", "InvoiceHeader")
    '        ClsObjDataSet.SetMasterDataSetFromMasterDB(" Exec Sp_getGIDHeader", "GIDHeader")
    '        Exit Sub
    'ErrTrap:
    '        MsgBox(Err.Description)
    '        Err.Clear()
    '    End Sub
    '    Public Sub FnFillPurchaseInvoiceDataOnLoad()
    '        On Error GoTo ErrTrap
    '        ClsObjDataSet.SetMasterDataSet(" Exec sp_getCompanyInfo", "Company")
    '        ClsObjDataSet.SetMasterDataSetFromMasterDB(" Exec sp_getVendorList", "SupplierDetails")
    '        Exit Sub
    'ErrTrap:
    '        MsgBox(Err.Description)
    '        Err.Clear()
    '    End Sub
    Public Function NextPurchaseInvoiceNo() As String
        On Error GoTo errTrap
        Dim NextNo As String
        Dim NextPNo As String
        NextNo = Dm.ValReturnProjExecNonQryAcc("Select NextInvoice from tblCompanyAP where CompanyID='" & Trim(CompId) & "'")
        If Val(NextNo) <> 0 Then
            NextPNo = Trim(sLocationCode) & "-I" & Format(Val(NextNo) + 1, "0000000")
        Else
            NextPNo = Trim(sLocationCode) & "-I0000001"
        End If
        NextPurchaseInvoiceNo = NextPNo
        Exit Function
errTrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Function
    Public Function NextJournlNo() As String
        On Error GoTo errtrap
        Dim NextNo As String
        Dim NextJNo As String
        NextNo = Dm.ValReturnProjExecNonQryAcc("Select NextJournalNo from tblParameters where CompanyID='" & Trim(CompId) & "'")
        If Val(NextNo) <> 0 Then
            NextJNo = "PJ-" & Format(Val(NextNo), "00000000")
        Else
            NextJNo = "PJ-00000001"
        End If
        NextJournlNo = NextJNo
        Exit Function
errtrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Function
    Public Function fnSavePurchaseHeader(ByVal InvoiceNo As String, ByVal InvoiceDate As String, ByVal GidNo As String, ByVal VendorCode As String, _
            ByVal TermDays As Long, ByVal RefInvoiceNo As String, ByVal RefInvoiceDate As String, ByVal DONo As String, ByVal DODate As String, _
            ByVal CurrCode As String, ByVal CurrRate As Double, ByVal DeptCode As String, ByVal Import As Char, ByVal Discount1 As Double, ByVal Discount2 As Double, ByVal Discount3 As Double, ByVal Description As String, _
            ByVal TotalValue As Double, ByVal GSTax As Double, ByVal CustomCharges As Double, ByVal FreightCharges As Double, ByVal InsuranceCharges As Double, ByVal Discount As Double, ByVal Misc As Double, ByVal NetValue As Double, _
            ByVal RefJournlNo As String, ByVal Cancelled As Char, ByVal ApprovedBy As String, ByVal ApprovedDate As String, ByVal SuspendedBy As String, ByVal SuspendedDate As String, ByVal UnSuspendedBy As String, ByVal UnSuspendedDate As String, _
            ByVal ReasonCode As String, ByVal Status As Char, ByVal CompanyCode As String, ByVal CreatedBy As String, ByVal CreatedDate As String, ByVal ModifiedBy As String, ByVal ModifiedDate As String, _
            ByVal sVerifiedBy As String, ByVal Mode As String) As String
        On Error GoTo ErrTrap
        Dim Dm As New DataManager
        Dim sExec As String
        Dim sExecBool As Boolean
        Dim PayDate As String
        PayDate = DateAdd(DateInterval.Day, TermDays, CDate(RefInvoiceDate))
        sExec = "EXEC sp_InvoiceHeader "
        sExec &= fncFormatSQLText(InvoiceNo) & "," & fncFormatSQLDate(InvoiceDate) & "," & fncFormatSQLText(GidNo) & "," & fncFormatSQLText(VendorCode) & ","
        sExec &= TermDays & "," & fncFormatSQLText(RefInvoiceNo) & "," & fncFormatSQLDate(RefInvoiceDate) & "," & fncFormatSQLText(DONo) & "," & fncFormatSQLDate(DODate) & ","
        sExec &= fncFormatSQLText(CurrCode) & "," & CurrRate & "," & fncFormatSQLText(DeptCode) & "," & fncFormatSQLText(Import) & "," & Discount1 & "," & Discount2 & "," & Discount3 & ","
        sExec &= fncFormatSQLText(Description) & "," & TotalValue & "," & GSTax & "," & CustomCharges & "," & FreightCharges & "," & InsuranceCharges & "," & Discount & "," & Misc & "," & NetValue & ","
        sExec &= fncFormatSQLText(RefJournlNo) & "," & fncFormatSQLText(Cancelled) & "," & fncFormatSQLDate(PayDate) & "," & fncFormatSQLText(ApprovedBy) & "," & fncFormatSQLDate(ApprovedDate) & "," & fncFormatSQLText(SuspendedBy) & "," & fncFormatSQLDate(SuspendedDate) & "," & fncFormatSQLText(UnSuspendedBy) & "," & fncFormatSQLDate(UnSuspendedDate) & ","
        sExec &= fncFormatSQLText(ReasonCode) & "," & fncFormatSQLText(Status) & "," & fncFormatSQLText(CompanyCode) & "," & _
                    fncFormatSQLText(sVerifiedBy) & ", " & _
                    fncFormatSQLText(CreatedBy) & "," & fncFormatSQLDate(CreatedDate) & "," & _
                    fncFormatSQLText(ModifiedBy) & "," & fncFormatSQLDate(ModifiedDate) & ", " & _
                    fncFormatSQLText(Mode)

        If Mode = "I" Then
            fnSavePurchaseHeader = Dm.ReturnProjExecNonQryAcc(sExec)
        Else
            If Dm.ProjExecNonQry(sExec) = False Then
                Return "F"
            Else
                Return "T"
            End If
        End If
        '25 Jan anand
        sExec = "exec sp_UpdateGidNoBlock "
        sExec &= fncFormatSQLText(GidNo)
        'Dm.ProjExecNonQry(sExec, gsConMasterDB)
        Dm.ProjExecNonQryAcc(sExec)
        Exit Function
ErrTrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Function
    Public Function fnSavePurchaseDetails(ByVal InvoiceNo As String, ByVal SNo As Int32, ByVal GidNo As String, _
    ByVal PoNo As String, ByVal ReleaseNo As Int32, ByVal ItemCode As String, ByVal ItemDescription As String, _
    ByVal StoreCode As String, ByVal PrcCode As String, ByVal DeptCode As String, ByVal PoUomCode As String, _
    ByVal PoQty As Long, ByVal PoUnitPrice As Double, ByVal PoDisc As Double, ByVal InvUOMCode As String, _
    ByVal RecQty As Long, ByVal FOCQty As Long, ByVal InvRate As Double, ByVal Total As Double, _
    ByVal CreatedBy As String, ByVal CreatedDate As String, ByVal ModifiedBy As String, ByVal ModifiedDate As String, ByVal Mode As String) As Boolean
        On Error GoTo ErrTrap
        Dim Dm As New DataManager
        Dim sExec As String
        sExec = "Exec sp_invoiceDetails "
        sExec &= fncFormatSQLText(InvoiceNo) & "," & SNo & "," & fncFormatSQLText(GidNo) & ","
        sExec &= fncFormatSQLText(PoNo) & "," & ReleaseNo & "," & fncFormatSQLText(ItemCode) & "," & fncFormatSQLText(ItemDescription) & ","
        sExec &= fncFormatSQLText(StoreCode) & "," & fncFormatSQLText(PrcCode) & "," & fncFormatSQLText(DeptCode) & "," & fncFormatSQLText(PoUomCode) & ","
        sExec &= PoQty & "," & PoUnitPrice & "," & PoDisc & "," & fncFormatSQLText(InvUOMCode) & "," & RecQty & "," & FOCQty & "," & InvRate & "," & Total & ","
        sExec &= fncFormatSQLText(CreatedBy) & "," & fncFormatSQLDate(CreatedDate) & "," & fncFormatSQLText(ModifiedBy) & "," & fncFormatSQLDate(ModifiedDate) & "," & fncFormatSQLText(Mode)
        fnSavePurchaseDetails = Dm.ProjExecNonQryAcc(sExec)
        Exit Function
ErrTrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Function
    Public Function fnDeleteRecord(ByVal InvoiceNo As String, ByVal RefJVNo As String) As Boolean
        On Error GoTo ErrTrap
        fnDeleteRecord = False
        Dim Dm As New DataManager
        Dim sExec As String
        sExec = "Exec sp_DeletePurchaseInvoice " & fncFormatSQLText(InvoiceNo) & "," & fncFormatSQLText(RefJVNo)
        Dm.ProjExecNonQryAcc(sExec)
        fnDeleteRecord = True
        Exit Function
ErrTrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Function
    'Public Sub InvoiceDetails(ByVal InvoiceNo As String)
    '    Dim ClsObjDataSet As New DataSetCollection
    '    ClsObjDataSet.SetMasterDataSet(" Exec sp_getInvoiceDetail " & fncFormatSQLText(InvoiceNo), "InvoiceDetails")
    'End Sub
    'Public Sub GIDDetails(ByVal GIDNO As String)
    '    Dim ClsObjDataSet As New DataSetCollection
    '    ClsObjDataSet.SetMasterDataSetFromMasterDB(" Exec sp_getGIDDetail " & fncFormatSQLText(GIDNO), "GIDDetails")
    'End Sub
    Public Function fnValidPurchaseBill(ByVal VendorCode As String, ByVal BillDate As String, ByVal BillNo As String) As Boolean
        Dim Ds As New DataSet
        Dim Dr() As DataRow
        Dim Dt As DataTable
        Dim Cls As New DataManager
        On Error GoTo ErrTrap
        Ds = Cls.SetTabelsToDataSetAcc("Exec sp_InvoiceValidate " & fncFormatSQLText(BillNo) & "," & fncFormatSQLDate(BillDate) & "," & fncFormatSQLText(VendorCode), "ValidBill")
        Dt = Ds.Tables("ValidBill")
        Dr = Dt.Select()
        If Dr.Length > 0 Then
            fnValidPurchaseBill = False
        Else
            fnValidPurchaseBill = True
        End If
        Exit Function
ErrTrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Function
    Public Function FnonRefresh()
        Dim ClsObjDataSet As New DataManager
        On Error GoTo ErrTrap
        ClsObjDataSet.SetMasterDataSetAcc(" Exec sp_getCompanyInfo", "Company")
        'ClsObjDataSet.SetMasterDataSetFromMasterDB(" Exec sp_getVendorList", "SupplierDetails")
        'ClsObjDataSet.SetMasterDataSet(" Exec sp_getVendorList", "SupplierDetails")
        ClsObjDataSet.SetMasterDataSetAcc(" Exec sp_getAccountDept", "Department")
        'ClsObjDataSet.SetMasterDataSetFromMasterDB("Exec sp_getPrcPrice", "PrcList")
        'ClsObjDataSet.SetMasterDataSetAcc("Exec sp_getPrcPrice", "PrcList")
        Exit Function
ErrTrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Function

    Public Function OpenBatchPostinConnection()
        'Dim X As System.Data.SqlClient.SqlDataReader
        gsConBatchPosting.ConnectionString = gsConAcc
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
    Public Function fnSavePurchaseHeaderWithTran(ByVal InvoiceNo As String, ByVal InvoiceDate As String, ByVal GidNo As String, ByVal VendorCode As String, _
        ByVal TermDays As Long, ByVal RefInvoiceNo As String, ByVal RefInvoiceDate As String, ByVal DONo As String, ByVal DODate As String, _
        ByVal CurrCode As String, ByVal CurrRate As Double, ByVal DeptCode As String, ByVal Import As Char, ByVal Discount1 As Double, ByVal Discount2 As Double, ByVal Discount3 As Double, ByVal Description As String, _
        ByVal TotalValue As Double, ByVal GSTax As Double, ByVal CustomCharges As Double, ByVal FreightCharges As Double, ByVal InsuranceCharges As Double, ByVal Discount As Double, ByVal Misc As Double, ByVal NetValue As Double, _
        ByVal RefJournlNo As String, ByVal Cancelled As Char, ByVal ApprovedBy As String, ByVal ApprovedDate As String, ByVal SuspendedBy As String, ByVal SuspendedDate As String, ByVal UnSuspendedBy As String, ByVal UnSuspendedDate As String, _
        ByVal ReasonCode As String, ByVal Status As Char, ByVal CompanyCode As String, ByVal CreatedBy As String, ByVal CreatedDate As String, ByVal ModifiedBy As String, ByVal ModifiedDate As String, _
        ByVal sVerifiedBy As String, ByVal Mode As String) As String
        On Error GoTo ErrTrap
        Dim Dm As New DataManager
        Dim sExec As String
        Dim sExecBool As Boolean
        Dim PayDate As String
        PayDate = DateAdd(DateInterval.Day, TermDays, CDate(RefInvoiceDate))
        sExec = "EXEC sp_InvoiceHeader "
        sExec &= fncFormatSQLText(InvoiceNo) & "," & fncFormatSQLDate(InvoiceDate) & "," & fncFormatSQLText(GidNo) & "," & fncFormatSQLText(VendorCode) & ","
        sExec &= TermDays & "," & fncFormatSQLText(RefInvoiceNo) & "," & fncFormatSQLDate(RefInvoiceDate) & "," & fncFormatSQLText(DONo) & "," & fncFormatSQLDate(DODate) & ","
        sExec &= fncFormatSQLText(CurrCode) & "," & CurrRate & "," & fncFormatSQLText(DeptCode) & "," & fncFormatSQLText(Import) & "," & Discount1 & "," & Discount2 & "," & Discount3 & ","
        sExec &= fncFormatSQLText(Description) & "," & TotalValue & "," & GSTax & "," & CustomCharges & "," & FreightCharges & "," & InsuranceCharges & "," & Discount & "," & Misc & "," & NetValue & ","
        sExec &= fncFormatSQLText(RefJournlNo) & "," & fncFormatSQLText(Cancelled) & "," & fncFormatSQLDate(PayDate) & "," & fncFormatSQLText(ApprovedBy) & "," & fncFormatSQLDate(ApprovedDate) & "," & fncFormatSQLText(SuspendedBy) & "," & fncFormatSQLDate(SuspendedDate) & "," & fncFormatSQLText(UnSuspendedBy) & "," & fncFormatSQLDate(UnSuspendedDate) & ","
        sExec &= fncFormatSQLText(ReasonCode) & "," & fncFormatSQLText(Status) & "," & fncFormatSQLText(CompanyCode) & "," & _
        fncFormatSQLText(sVerifiedBy) & ", " & _
        fncFormatSQLText(CreatedBy) & "," & fncFormatSQLDate(CreatedDate) & "," & _
        fncFormatSQLText(ModifiedBy) & "," & fncFormatSQLDate(ModifiedDate) & ", " & _
        fncFormatSQLText(Mode)
        Dim Cmd As New SqlClient.SqlCommand(sExec, gsConBatchPosting)
        Cmd.Transaction = gsTrans
        fnSavePurchaseHeaderWithTran = Cmd.ExecuteScalar()
        'fnSaveReceiptOtherHeaderWithTran = True
        'sExec = "exec sp_UpdateGidNoBlock "
        'sExec &= fncFormatSQLText(GidNo)
        'Dm.ProjExecNonQry(sExec, gsConMasterDB)
        Exit Function
ErrTrap:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
        Err.Clear()
    End Function
    Public Function fnSavePurchaseDetailsWithTran(ByVal InvoiceNo As String, ByVal SNo As Int32, ByVal GidNo As String, _
    ByVal PoNo As String, ByVal ReleaseNo As Int32, ByVal ItemCode As String, ByVal ItemDescription As String, _
    ByVal StoreCode As String, ByVal PrcCode As String, ByVal DeptCode As String, ByVal PoUomCode As String, _
    ByVal PoQty As Long, ByVal PoUnitPrice As Double, ByVal PoDisc As Double, ByVal InvUOMCode As String, _
    ByVal RecQty As Long, ByVal FOCQty As Long, ByVal InvRate As Double, ByVal Total As Double, _
    ByVal CreatedBy As String, ByVal CreatedDate As String, ByVal ModifiedBy As String, ByVal ModifiedDate As String, ByVal Mode As String) As Boolean
        On Error GoTo ErrTrap
        Dim Dm As New DataManager
        Dim sExec As String
        sExec = "Exec sp_invoiceDetails "
        sExec &= fncFormatSQLText(InvoiceNo) & "," & SNo & "," & fncFormatSQLText(GidNo) & ","
        sExec &= fncFormatSQLText(PoNo) & "," & ReleaseNo & "," & fncFormatSQLText(ItemCode) & "," & fncFormatSQLText(ItemDescription) & ","
        sExec &= fncFormatSQLText(StoreCode) & "," & fncFormatSQLText(PrcCode) & "," & fncFormatSQLText(DeptCode) & "," & fncFormatSQLText(PoUomCode) & ","
        sExec &= PoQty & "," & PoUnitPrice & "," & PoDisc & "," & fncFormatSQLText(InvUOMCode) & "," & RecQty & "," & FOCQty & "," & InvRate & "," & Total & ","
        sExec &= fncFormatSQLText(CreatedBy) & "," & fncFormatSQLDate(CreatedDate) & "," & fncFormatSQLText(ModifiedBy) & "," & fncFormatSQLDate(ModifiedDate) & "," & fncFormatSQLText(Mode)
        Dim Cmd As New SqlClient.SqlCommand(sExec, gsConBatchPosting)
        Cmd.Transaction = gsTrans
        fnSavePurchaseDetailsWithTran = Cmd.ExecuteNonQuery()
        fnSavePurchaseDetailsWithTran = True
        Exit Function
ErrTrap:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
        fnSavePurchaseDetailsWithTran = False
        Err.Clear()
    End Function
    Public Function fnDeleteRecordWithTran(ByVal InvoiceNo As String, ByVal RefJVNo As String) As Boolean
        On Error GoTo ErrTrap
        fnDeleteRecordWithTran = False
        Dim Dm As New DataManager
        Dim sExec As String

        sExec = "Exec sp_DeletePurchaseInvoice " & fncFormatSQLText(InvoiceNo) & "," & fncFormatSQLText(RefJVNo)
        Dim Cmd As New SqlClient.SqlCommand(sExec, gsConBatchPosting)
        Cmd.Transaction = gsTrans
        Cmd.ExecuteNonQuery()

        fnDeleteRecordWithTran = True
        Exit Function
ErrTrap:
        fnDeleteRecordWithTran = False
    End Function
End Class
