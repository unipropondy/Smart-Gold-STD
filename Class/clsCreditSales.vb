Public Class clsCreditSales
    Dim Dm As New DataManager
    Public Sub New()
    End Sub
    Public Function fnFindNextSalesNo()
        On Error GoTo errTrap
        Dim NextNo As String
        Dim NextCSNo As String
        NextNo = Dm.ValReturnProjExecNonQry("Select NextInvoice from tblCompanyAR where CompanyID='" & Trim(CompId) & "'")
        If Val(NextNo) <> 0 Then
            NextCSNo = Trim(sLocationCode) & "-CS" & Format(Val(NextNo) + 1, "0000000")
        Else
            NextCSNo = Trim(sLocationCode) & "-CS0000001"
        End If
        fnFindNextSalesNo = NextCSNo
        Exit Function
errTrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Function
    Public Function fnFindNextDCNo()
        On Error GoTo errTrap
        Dim NextNo As String
        Dim NextCSNo As String
        NextNo = Dm.ValReturnProjExecNonQry("Select NextDC from tblCompanyAR where CompanyID='" & Trim(CompId) & "'")
        If Val(NextNo) <> 0 Then
            NextCSNo = Trim(sLocationCode) & "-DC" & Format(Val(NextNo) + 1, "0000000")
        Else
            NextCSNo = Trim(sLocationCode) & "-DC0000001"
        End If
        fnFindNextDCNo = NextCSNo
        Exit Function
errTrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Function
    Public Function fnSaveSalesInvoiceHeader(ByVal InvoiceNo As String, ByVal InvoiceDate As String, ByVal DeptCode As String, _
                                             ByVal LocationCode As String, ByVal CustomerCode As String, ByVal CusSASlNo As String, ByVal CusSACP As String, _
                                             ByVal Terms As Integer, ByVal DueDate As String, ByVal salesManID As String, ByVal Description As String, _
                                             ByVal TotalQty As Double, ByVal TotFOCQty As Double, ByVal TotalItems As Double, ByVal TotalValue As Double, _
                                             ByVal DiscountPercent As Double, ByVal Discount As Double, ByVal GstPercentage As Double, ByVal GSTValue As Double, ByVal NetValue As Double, _
                                             ByVal CurCode As String, ByVal CurRate As Double, ByVal Post As String, ByVal PostBy As String, _
                                             ByVal RefJournalNo As String, ByVal PostedDate As String, ByVal RefInvoiceNo As String, ByVal RefInvoiceDate As String, ByVal CompanyCode As String, ByVal CreateBy As String, _
                                             ByVal CreateDate As String, ByVal ModifiedBy As String, ByVal ModifiedDate As String, ByVal Mode As String) As String

        On Error GoTo errtrap
        Dim SqlQry As String
        SqlQry = "EXEC AR_SalesInvoiceHeader    "
        SqlQry &= fncFormatSQLText(InvoiceNo) & "," & fncFormatSQLDate(InvoiceDate) & ","
        SqlQry &= fncFormatSQLText(DeptCode) & "," & fncFormatSQLText(LocationCode) & ","
        SqlQry &= fncFormatSQLText(CustomerCode) & "," & fncFormatSQLText(CusSASlNo) & "," & fncFormatSQLText(CusSACP) & "," & Val(Terms) & ","
        SqlQry &= fncFormatSQLDate(DueDate) & "," & fncFormatSQLText(salesManID) & "," & fncFormatSQLText(Description) & ","
        SqlQry &= Val(TotalQty) & "," & Val(TotFOCQty) & "," & Val(TotalItems) & "," & Val(TotalValue) & "," & Val(DiscountPercent) & ","
        SqlQry &= Val(Discount) & "," & Val(GstPercentage) & "," & Val(GSTValue) & "," & Val(NetValue) & "," & fncFormatSQLText(CurCode) & ","
        SqlQry &= Val(CurRate) & "," & fncFormatSQLText(Post) & "," & fncFormatSQLText(PostBy) & ","
        SqlQry &= fncFormatSQLText(RefJournalNo) & "," & fncFormatSQLDate(PostedDate) & "," & fncFormatSQLText(CompanyCode) & ","
        SqlQry &= fncFormatSQLText(RefInvoiceNo) & "," & fncFormatSQLDate(RefInvoiceDate) & ","
        SqlQry &= fncFormatSQLText(CreateBy) & "," & fncFormatSQLDate(CreateDate) & "," & fncFormatSQLText(ModifiedBy) & ","
        SqlQry &= fncFormatSQLDate(ModifiedDate) & "," & fncFormatSQLText(Mode)
        'Dm.ReturnProjExecNonQry(SqlQry)
        If Mode = "I" Then
            fnSaveSalesInvoiceHeader = Dm.ReturnProjExecNonQry(SqlQry)
        Else
            If Dm.ProjExecNonQry(SqlQry) = False Then
                Return "F"
            Else
                Return "T"
            End If
        End If
        Exit Function
errtrap:
        'MsgBox(Err.Description)
        MsgBox("Error in Save Sales Invoice Header")
        Err.Clear()
    End Function
    Public Function fnSaveInvoiceDetail(ByVal InvoiceNo As String, ByVal SlNo As Integer, ByVal StoreCode As String, _
                                        ByVal PRCID As String, ByVal InventoryCode As String, _
                                        ByVal Description As String, ByVal UOM As String, ByVal TotQty As Double, ByVal UOMFOC As String, _
                                        ByVal FOCQty As Double, ByVal UnitPrice As Double, ByVal TotalValue As Double, ByVal CreateUser As String, _
                                        ByVal CreateDate As String, ByVal ModifyUser As String, ByVal ModifyDate As String) As String
        On Error GoTo errtrap
        Dim SqlQry As String
        SqlQry = "EXEC AR_SalesInvoiceDetail   "
        SqlQry &= fncFormatSQLText(InvoiceNo) & "," & Val(SlNo) & ","
        SqlQry &= fncFormatSQLText(StoreCode) & "," & fncFormatSQLText(PRCID) & ","
        SqlQry &= fncFormatSQLText(InventoryCode) & "," & fncFormatSQLText(Description) & ","
        SqlQry &= fncFormatSQLText(UOM) & "," & Val(TotQty) & "," & fncFormatSQLText(UOMFOC) & ","
        SqlQry &= Val(FOCQty) & "," & Val(UnitPrice) & "," & Val(TotalValue) & ","
        SqlQry &= fncFormatSQLText(CreateUser) & "," & fncFormatSQLDate(CreateDate) & "," & fncFormatSQLText(ModifyUser) & ","
        SqlQry &= fncFormatSQLDate(ModifyDate)
        Dm.ProjExecNonQry(SqlQry)
        Exit Function
errtrap:
        'MsgBox(Err.Description)
        MsgBox("Error in Save Sales Invoice Detail")
        Err.Clear()
    End Function
    Public Function fnDeleteInvoiceHeader(ByVal InvNo As String) As Boolean
        fnDeleteInvoiceHeader = False
        On Error GoTo errtrap
        Dm.ProjExecNonQry("Exec AR_DeleteInvoiceHeader '" & InvNo & "'")
        fnDeleteInvoiceHeader = True
        Exit Function
errtrap:
        MsgBox("Error in Delete Invoice Header Record" & Err.Description)
    End Function
    Public Function fnDeleteInvoiceDetail(ByVal InvNo As String) As Boolean
        fnDeleteInvoiceDetail = False
        On Error GoTo errtrap
        Dm.ProjExecNonQry("Exec AR_DelReceiptOtherTransactions '" & InvNo & "'")
        fnDeleteInvoiceDetail = True
        Exit Function
errtrap:
        MsgBox("Error in Delete Invoice Detail transaction Record" & Err.Description)
    End Function
    Public Function NextSIJournlNo() As String
        On Error GoTo errtrap
        Dim NextNo As String
        Dim NextJNo As String
        NextNo = Dm.ValReturnProjExecNonQry("Select NextSales from tblParameters where CompanyID='" & Trim(CompId) & "'")
        If Val(NextNo) <> 0 Then
            NextJNo = "CS-" & Format(Val(NextNo), "00000000")
        Else
            NextJNo = "CS-00000001"
        End If
        NextSIJournlNo = NextJNo
        Exit Function
errtrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Function
    Public Function fnUpDateJournalNo(ByVal InvoiceNo As String, ByVal JournalNo As String) As Boolean
        On Error GoTo errTrap
        fnUpDateJournalNo = False
        Dim DM As New DataManager
        fnUpDateJournalNo = DM.ProjExecNonQry(" Exec AR_UpdateSalesInvoiceJournal " & fncFormatSQLText(InvoiceNo) & "," & fncFormatSQLText(JournalNo))
        Exit Function
errTrap:
        MsgBox(Err.Description)
        Err.Clear()
    End Function
    'Anand, 26 Jan, Sales Invoice  in to Account
    Public Function fncFillDPSalesInvoice()
        'Dm.SetMasterDataSetFromMasterDB("Exec sp_ARPendingInvoiceHeader ''", "DPSalesInvoiceHeader")
        'Dm.SetMasterDataSetFromMasterDB("Exec sp_ARPendingInvoiceDetail ''", "DPSalesInvoiceDetail")
        Dm.SetMasterDataSet("Exec sp_ARPendingInvoiceHeader ''", "DPSalesInvoiceHeader")
        Dm.SetMasterDataSet("Exec sp_ARPendingInvoiceDetail ''", "DPSalesInvoiceDetail")
    End Function
    Public Function fnFindSalesInvoiceHeaderFromMaster(ByVal sInvoiceNo As String) As DataRow()
        Dim Dr() As DataRow
        Dr = Dm.RowReturnProjExecNonQryWithDataRow("Exec sp_ARPendingInvoiceHeader   " & fncFormatSQLText(sInvoiceNo))
        fnFindSalesInvoiceHeaderFromMaster = Dr
    End Function
    Public Function fnFindSalesInvoiceDetailFromMaster(ByVal sInvoiceNo As String) As DataRow()
        Dim Dr() As DataRow
        Dr = Dm.RowReturnProjExecNonQryWithDataRow("Exec sp_ARPendingInvoiceDetail    " & fncFormatSQLText(sInvoiceNo))
        fnFindSalesInvoiceDetailFromMaster = Dr
    End Function
    Public Function fnSaveSalesInvoiceTOAcc(ByVal sInvNo As String) As Boolean
        On Error GoTo errtrap
        Dim SqlQry As String

        Dim I As Integer
        Dim TotalQty, focQty As Integer


        Dim tempDr() As DataRow
        Dim sTerms, sContactPerson As String

        Dim Dr() As DataRow
        Dim Dt() As DataRow
        Dim Dt1() As DataRow

        Dr = fnFindSalesInvoiceHeaderFromMaster(sInvNo)
        If Dr.Length > 0 Then
            'Getting Details Information, calculating Qty & focQty
            Dt1 = fnFindSalesInvoiceDetailFromMaster(sInvNo)
            If Dt1.Length > 0 Then
                focQty = 0
                TotalQty = 0
                For I = 0 To Dt1.Length - 1
                    TotalQty = TotalQty + Val(Dt1(I)("Qty") & "")
                    focQty = focQty + Val(Dt1(I)("focQty") & "")
                Next I
            End If
            'Getting Customer Terms ID
            tempDr = Dm.RowReturnProjExecNonQryWithDataRow("select * from tblArCustomer where CustomerCode=" & fncFormatSQLText(Trim$(Dr(0)("CustomerCode") & "")))
            If tempDr.Length > 0 Then
                sTerms = Val(tempDr(0)("TermsCode") & "")
                sContactPerson = Trim(tempDr(0)("Attn") & "")
            End If
            tempDr = Nothing

            'Storing Header Information
            SqlQry = "EXEC AR_SalesInvoiceHeaderDP   "
            SqlQry = SqlQry & fncFormatSQLText(Trim(Dr(0)("InvoiceNo") & "")) & ","
            SqlQry = SqlQry & fncFormatSQLDate(Trim(Dr(0)("InvoiceDate") & "")) & ","
            SqlQry = SqlQry & fncFormatSQLText(Trim(Dr(0)("AccDeptCode") & "")) & ","
            SqlQry = SqlQry & fncFormatSQLText(Trim(Dr(0)("LocationCode") & "")) & ","
            SqlQry = SqlQry & fncFormatSQLText(Trim(Dr(0)("CustomerCode") & "")) & ","
            SqlQry = SqlQry & 1 & ","
            SqlQry = SqlQry & fncFormatSQLText(Trim(sContactPerson & "")) & ","
            SqlQry = SqlQry & Val(sTerms) & ","
            SqlQry = SqlQry & fncFormatSQLDate(Trim(Dr(0)("InvoiceDate") & "")) & ","
            SqlQry = SqlQry & fncFormatSQLText(Trim(Dr(0)("SalesManId") & "")) & ","
            SqlQry = SqlQry & fncFormatSQLText("") & ","
            SqlQry = SqlQry & Val(TotalQty) & ","
            SqlQry = SqlQry & Val(focQty) & ","
            SqlQry = SqlQry & Val(Dr(0)("TotalLines")) & ","
            SqlQry = SqlQry & Val(Dr(0)("TOTALVALUE")) & ","
            SqlQry = SqlQry & Val(0.0#) & ","
            SqlQry = SqlQry & Val(Dr(0)("Discount")) & ","
            SqlQry = SqlQry & Val(GSTValue) & ","
            SqlQry = SqlQry & Val(Dr(0)("GST")) & ","
            SqlQry = SqlQry & Val(Dr(0)("NetTotal")) & ","
            SqlQry = SqlQry & fncFormatSQLText(Trim(CurrCode & "")) & ","
            SqlQry = SqlQry & Val(CurrRate) & ","
            SqlQry = SqlQry & fncFormatSQLText("N") & ","
            SqlQry = SqlQry & fncFormatSQLText("") & ","
            SqlQry = SqlQry & fncFormatSQLText("") & ","
            SqlQry = SqlQry & fncFormatSQLDate(Today) & ","
            SqlQry = SqlQry & fncFormatSQLText(Trim(sLocationCode & "")) & ","
            SqlQry = SqlQry & fncFormatSQLText(Trim(Dr(0)("InvoiceNo") & "")) & ","
            SqlQry = SqlQry & fncFormatSQLDate(Trim(Dr(0)("InvoiceDate") & "")) & ","
            SqlQry = SqlQry & fncFormatSQLText(Trim(sUserID & ""))

            Dm.ProjExecNonQry(SqlQry)
            fnSaveSalesInvoiceTOAcc = True
            Dt = fnFindSalesInvoiceDetailFromMaster(sInvNo)
            If Dt.Length > 0 Then
                'Storing Detail Information
                For I = 0 To Dt.Length - 1
                    fnSaveInvoiceDetail( _
                            Dt(I)("InvoiceNo"), _
                            I, sLocationCode, _
                            Dt(I)("PrcCode"), _
                            Dt(I)("InventoryCode"), _
                            Dt(I)("Description"), _
                            Dt(I)("UomCode"), _
                            Dt(I)("Qty"), _
                            Dt(I)("UomCode"), _
                            Dt(I)("focQty"), _
                            Dt(I)("Price"), _
                            Dt(I)("TOTALVALUE"), _
                            Dt(I)("CreateUser"), Dt(I)("CreateDate"), Dt(I)("ModifyUser"), Dt(I)("ModifyDate"))
                Next I
            End If
        Else
            fnSaveSalesInvoiceTOAcc = False
            Exit Function
        End If
        Exit Function
errtrap:
        MsgBox("Error in Invoic Saving" & vbCrLf & Err.Description)
        Err.Clear()
        Exit Function
NextLine:
        On Error Resume Next
        On Error GoTo 0
        'DP  -> InvoiceNo,InventoryCode,Description,Qty,UnitPrice,GrossCost,Price,OutletPrice,TotalValue,Discount,NetDiscount,NetAmount,ActualSales,Gst,SalesManId,DeptCode,CategoryCode,BrandCode,AverageCost,MemberDiscount,LocationCode,UOMType,UOMCode,WUnit,FOCQty,BaseQty,PrcCode,SalesByMonth,SalesByYear,ReturnByMonth,ReturnByYear,BalanceStock,OutletStock,RecomRetailPrice,CreateUser,CreateDate,ModifyUser,ModifyDate
        'ACC -> InvoiceNo,SlNo,StoreCode,PRCID,InventoryCode,Description,UOM,TotQty,FOCUOM,FOCQty,UnitPrice,TotalValue,CreateUser,CreateDate,ModifyUser,ModifyDate
    End Function
    
    Public Function UpdateSalesInvoiceBlocked(ByVal sInvno As String) As Boolean
        'UpdateSalesInvoiceBlocked = Dm.ProjExecNonQry("Exec  sp_UpdateInvoiceBlocked  " & fncFormatSQLText(sInvno), gsConMasterDB)
        UpdateSalesInvoiceBlocked = Dm.ProjExecNonQry("Exec  sp_UpdateInvoiceBlocked  " & fncFormatSQLText(sInvno), gsCon)
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
    Public Function fnSaveSalesInvoiceHeaderWithTran(ByVal InvoiceNo As String, ByVal InvoiceDate As String, ByVal DeptCode As String, _
                                               ByVal LocationCode As String, ByVal CustomerCode As String, ByVal CusSASlNo As String, ByVal CusSACP As String, _
                                               ByVal Terms As Integer, ByVal DueDate As String, ByVal salesManID As String, ByVal Description As String, _
                                               ByVal TotalQty As Double, ByVal TotFOCQty As Double, ByVal TotalItems As Double, ByVal TotalValue As Double, _
                                               ByVal DiscountPercent As Double, ByVal Discount As Double, ByVal GstPercentage As Double, ByVal GSTValue As Double, ByVal NetValue As Double, _
                                               ByVal CurCode As String, ByVal CurRate As Double, ByVal Post As String, ByVal PostBy As String, _
                                               ByVal RefJournalNo As String, ByVal PostedDate As String, ByVal RefInvoiceNo As String, ByVal RefInvoiceDate As String, ByVal CompanyCode As String, ByVal CreateBy As String, _
                                               ByVal CreateDate As String, ByVal ModifiedBy As String, ByVal ModifiedDate As String, ByVal Mode As String) As String

        On Error GoTo ErrTrap
        Dim SqlQry As String
        SqlQry = "EXEC AR_SalesInvoiceHeader    "
        SqlQry &= fncFormatSQLText(InvoiceNo) & "," & fncFormatSQLDate(InvoiceDate) & ","
        SqlQry &= fncFormatSQLText(DeptCode) & "," & fncFormatSQLText(LocationCode) & ","
        SqlQry &= fncFormatSQLText(CustomerCode) & "," & fncFormatSQLText(CusSASlNo) & "," & fncFormatSQLText(CusSACP) & "," & Val(Terms) & ","
        SqlQry &= fncFormatSQLDate(DueDate) & "," & fncFormatSQLText(salesManID) & "," & fncFormatSQLText(Description) & ","
        SqlQry &= Val(TotalQty) & "," & Val(TotFOCQty) & "," & Val(TotalItems) & "," & Val(TotalValue) & "," & Val(DiscountPercent) & ","
        SqlQry &= Val(Discount) & "," & Val(GstPercentage) & "," & Val(GSTValue) & "," & Val(NetValue) & "," & fncFormatSQLText(CurCode) & ","
        SqlQry &= Val(CurRate) & "," & fncFormatSQLText(Post) & "," & fncFormatSQLText(PostBy) & ","
        SqlQry &= fncFormatSQLText(RefJournalNo) & "," & fncFormatSQLDate(PostedDate) & "," & fncFormatSQLText(CompanyCode) & ","
        SqlQry &= fncFormatSQLText(RefInvoiceNo) & "," & fncFormatSQLDate(RefInvoiceDate) & ","
        SqlQry &= fncFormatSQLText(CreateBy) & "," & fncFormatSQLDate(CreateDate) & "," & fncFormatSQLText(ModifiedBy) & ","
        SqlQry &= fncFormatSQLDate(ModifiedDate) & "," & fncFormatSQLText(Mode)
        Dim Cmd As New SqlClient.SqlCommand(SqlQry, gsConBatchPosting)
        Cmd.Transaction = gsTrans
        fnSaveSalesInvoiceHeaderWithTran = Cmd.ExecuteScalar()
        'fnSaveReceiptOtherHeaderWithTran = True
        Exit Function
ErrTrap:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
        Err.Clear()
    End Function

    Public Function fnSaveDCHeaderWithTran(ByVal InvoiceNo As String, ByVal InvoiceDate As String, ByVal DeptCode As String, _
                                              ByVal LocationCode As String, ByVal CustomerCode As String, ByVal CusSASlNo As String, ByVal CusSACP As String, _
                                              ByVal Terms As Integer, ByVal DueDate As String, ByVal salesManID As String, ByVal Description As String, _
                                              ByVal TotalQty As Double, ByVal TotFOCQty As Double, ByVal TotalItems As Double, ByVal TotalValue As Double, _
                                              ByVal DiscountPercent As Double, ByVal Discount As Double, ByVal GstPercentage As Double, ByVal GSTValue As Double, ByVal NetValue As Double, _
                                              ByVal CurCode As String, ByVal CurRate As Double, ByVal Post As String, ByVal PostBy As String, _
                                              ByVal RefJournalNo As String, ByVal PostedDate As String, ByVal RefInvoiceNo As String, ByVal RefInvoiceDate As String, ByVal CompanyCode As String, ByVal CreateBy As String, _
                                              ByVal CreateDate As String, ByVal ModifiedBy As String, ByVal ModifiedDate As String, ByVal Mode As String) As String

        On Error GoTo ErrTrap
        Dim SqlQry As String
        SqlQry = "EXEC AR_DCHeader    "
        SqlQry &= fncFormatSQLText(InvoiceNo) & "," & fncFormatSQLDate(InvoiceDate) & ","
        SqlQry &= fncFormatSQLText(DeptCode) & "," & fncFormatSQLText(LocationCode) & ","
        SqlQry &= fncFormatSQLText(CustomerCode) & "," & fncFormatSQLText(CusSASlNo) & "," & fncFormatSQLText(CusSACP) & "," & Val(Terms) & ","
        SqlQry &= fncFormatSQLDate(DueDate) & "," & fncFormatSQLText(salesManID) & "," & fncFormatSQLText(Description) & ","
        SqlQry &= Val(TotalQty) & "," & Val(TotFOCQty) & "," & Val(TotalItems) & "," & Val(TotalValue) & "," & Val(DiscountPercent) & ","
        SqlQry &= Val(Discount) & "," & Val(GstPercentage) & "," & Val(GSTValue) & "," & Val(NetValue) & "," & fncFormatSQLText(CurCode) & ","
        SqlQry &= Val(CurRate) & "," & fncFormatSQLText(Post) & "," & fncFormatSQLText(PostBy) & ","
        SqlQry &= fncFormatSQLText(RefJournalNo) & "," & fncFormatSQLDate(PostedDate) & "," & fncFormatSQLText(CompanyCode) & ","
        SqlQry &= fncFormatSQLText(RefInvoiceNo) & "," & fncFormatSQLDate(RefInvoiceDate) & ","
        SqlQry &= fncFormatSQLText(CreateBy) & "," & fncFormatSQLDate(CreateDate) & "," & fncFormatSQLText(ModifiedBy) & ","
        SqlQry &= fncFormatSQLDate(ModifiedDate) & "," & fncFormatSQLText(Mode)
        Dim Cmd As New SqlClient.SqlCommand(SqlQry, gsConBatchPosting)
        Cmd.Transaction = gsTrans
        fnSaveDCHeaderWithTran = Cmd.ExecuteScalar()
        'fnSaveReceiptOtherHeaderWithTran = True
        Exit Function
ErrTrap:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
        Err.Clear()
    End Function
    Public Function fnSaveInvoiceDetailWithTran(ByVal InvoiceNo As String, ByVal SlNo As Integer, ByVal StoreCode As String, _
                                        ByVal PRCID As String, ByVal InventoryCode As String, _
                                        ByVal Description As String, ByVal UOM As String, ByVal TotQty As Double, ByVal UOMFOC As String, _
                                        ByVal FOCQty As Double, ByVal UnitPrice As Double, ByVal TotalValue As Double, ByVal CreateUser As String, _
                                        ByVal CreateDate As String, ByVal ModifyUser As String, ByVal ModifyDate As String) As Boolean
        On Error GoTo ErrTrap
        Dim SqlQry As String
        SqlQry = "EXEC AR_SalesInvoiceDetail   "
        SqlQry &= fncFormatSQLText(InvoiceNo) & "," & Val(SlNo) & ","
        SqlQry &= fncFormatSQLText(StoreCode) & "," & fncFormatSQLText(PRCID) & ","
        SqlQry &= fncFormatSQLText(InventoryCode) & "," & fncFormatSQLText(Description) & ","
        SqlQry &= fncFormatSQLText(UOM) & "," & Val(TotQty) & "," & fncFormatSQLText(UOMFOC) & ","
        SqlQry &= Val(FOCQty) & "," & Val(UnitPrice) & "," & Val(TotalValue) & ","
        SqlQry &= fncFormatSQLText(CreateUser) & "," & fncFormatSQLDate(CreateDate) & "," & fncFormatSQLText(ModifyUser) & "," & fncFormatSQLDate(ModifyDate)
        Dim Cmd As New SqlClient.SqlCommand(SqlQry, gsConBatchPosting)
        Cmd.Transaction = gsTrans
        fnSaveInvoiceDetailWithTran = Cmd.ExecuteNonQuery()
        fnSaveInvoiceDetailWithTran = True
        Exit Function
ErrTrap:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
        fnSaveInvoiceDetailWithTran = False
        Err.Clear()

    End Function
    Public Function fnSaveDCDetailWithTran(ByVal InvoiceNo As String, ByVal SlNo As Integer, ByVal StoreCode As String, _
                                            ByVal PRCID As String, ByVal InventoryCode As String, _
                                            ByVal Description As String, ByVal UOM As String, ByVal TotQty As Double, ByVal UOMFOC As String, _
                                            ByVal FOCQty As Double, ByVal UnitPrice As Double, ByVal TotalValue As Double, ByVal CreateUser As String, _
                                            ByVal CreateDate As String, ByVal ModifyUser As String, ByVal ModifyDate As String) As Boolean
        On Error GoTo ErrTrap
        Dim SqlQry As String
        SqlQry = "EXEC AR_DCDetail   "
        SqlQry &= fncFormatSQLText(InvoiceNo) & "," & Val(SlNo) & ","
        SqlQry &= fncFormatSQLText(StoreCode) & "," & fncFormatSQLText(PRCID) & ","
        SqlQry &= fncFormatSQLText(InventoryCode) & "," & fncFormatSQLText(Description) & ","
        SqlQry &= fncFormatSQLText(UOM) & "," & Val(TotQty) & "," & fncFormatSQLText(UOMFOC) & ","
        SqlQry &= Val(FOCQty) & "," & Val(UnitPrice) & "," & Val(TotalValue) & ","
        SqlQry &= fncFormatSQLText(CreateUser) & "," & fncFormatSQLDate(CreateDate) & "," & fncFormatSQLText(ModifyUser) & "," & fncFormatSQLDate(ModifyDate)
        Dim Cmd As New SqlClient.SqlCommand(SqlQry, gsConBatchPosting)
        Cmd.Transaction = gsTrans
        fnSaveDCDetailWithTran = Cmd.ExecuteNonQuery()
        fnSaveDCDetailWithTran = True
        Exit Function
ErrTrap:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
        fnSaveDCDetailWithTran = False
        Err.Clear()

    End Function

    Public Function fnDeleteInvoiceHeaderWithTran(ByVal InvNo As String) As Boolean
        On Error GoTo errtrap
        fnDeleteInvoiceHeaderWithTran = False
        'Dm.ProjExecNonQry("Exec AR_DeleteInvoiceHeader '" & InvNo & "'")
        Dim Cmd As New SqlClient.SqlCommand("Exec AR_DeleteInvoiceHeader '" & InvNo & "'", gsConBatchPosting)
        Cmd.Transaction = gsTrans
        Cmd.ExecuteNonQuery()

        Dim Cmd1 As New SqlClient.SqlCommand("Exec AR_DeleteSalesInvoiceDetail '" & InvNo & "'", gsConBatchPosting)
        Cmd1.Transaction = gsTrans
        Cmd1.ExecuteNonQuery()

        fnDeleteInvoiceHeaderWithTran = True
        Exit Function
errtrap:
        fnDeleteInvoiceHeaderWithTran = False
    End Function

End Class
