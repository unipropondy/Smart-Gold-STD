Public Class clsSuppliers
    Dim dm As New DataManager

#Region "Supplier Delete"
    Public Function SupplierDelete(ByVal sCode As String) As Boolean
        Dim sExec As String
        Try
            SupplierDelete = False
            sExec = "BEGIN TRANSACTION" & vbCrLf
            sExec = sExec & "DELETE FROM tblSupplier WHERE Suppliercode=" & fncFormatSQLText(sCode) & vbCrLf
            sExec = sExec & "COMMIT TRANSACTION" & vbCrLf
            dm.ProjExecNonQry(sExec)
            SupplierDelete = True
            Exit Function
        Catch ex As Exception
            SupplierDelete = False
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Function
#End Region

#Region "Supplier Save"
    Public Function fncSaveSupplier(ByVal sSupplierCode As String, ByVal sSupplierName As String, _
                          ByVal sAddress1 As String, ByVal sAddress2 As String, _
                          ByVal sAddress3 As String, ByVal sAddress4 As String, _
                          ByVal sPhone1 As String, ByVal sPhone2 As String, _
                          ByVal sMobile As String, ByVal sFax As String, _
                          ByVal sEmail As String, ByVal sUrl As String, _
                          ByVal sTerms As String, ByVal sAccountType As String, _
                          ByVal sGLAccount1 As String, ByVal sGLAccount2 As String, _
                          ByVal sGSTType As String, _
                          ByVal sRemarks As String, ByVal sNricNo As String, ByVal sGender As String, ByVal sPassportNo As String, ByVal sDOB As String, ByVal sUserId As String, ByRef sError As String) As Boolean

        Try
            Dim sExec As String
            fncSaveSupplier = False
            sExec = "Begin Transaction" & vbCrLf
            sExec = sExec & "EXEC sp_Supplier " & fncFormatSQLText(sSupplierCode) & ","
            sExec = sExec & fncFormatSQLText(sSupplierName) & ","
            sExec = sExec & fncFormatSQLText(sAddress1) & ","
            sExec = sExec & fncFormatSQLText(sAddress2) & ","
            sExec = sExec & fncFormatSQLText(sAddress3) & ","
            sExec = sExec & fncFormatSQLText(sAddress4) & ","
            sExec = sExec & fncFormatSQLText(sPhone1) & ","
            sExec = sExec & fncFormatSQLText(sPhone2) & ","
            sExec = sExec & fncFormatSQLText(sMobile) & ","
            sExec = sExec & fncFormatSQLText(sFax) & ","
            sExec = sExec & fncFormatSQLText(sEmail) & ","
            sExec = sExec & fncFormatSQLText(sUrl) & ","
            sExec = sExec & fncFormatSQLText(sTerms) & ","
            sExec = sExec & fncFormatSQLText(sAccountType) & ","
            sExec = sExec & fncFormatSQLText(sGLAccount1) & ","
            sExec = sExec & fncFormatSQLText(sGLAccount2) & ","
            sExec = sExec & fncFormatSQLText(sRemarks) & ","
            sExec = sExec & fncFormatSQLText(sGSTType) & ","
            sExec = sExec & fncFormatSQLText(sNricNo) & ","
            sExec = sExec & fncFormatSQLText(sGender) & ","
            sExec = sExec & fncFormatSQLText(sPassportNo) & ","
            sExec = sExec & fncFormatSQLDate(sDOB) & ","
            sExec = sExec & fncFormatSQLText(sUserId) & vbCrLf
            sExec = sExec & "Commit Transaction" & vbCrLf
            fncSaveSupplier = dm.ProjExecNonQry(sExec)
            Exit Function
        Catch ex As Exception
            fncSaveSupplier = False
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Function
#End Region

#Region "Get Supplier Detail"
    Public Function GetSupplierDetail(ByVal sCode As String) As DataSet
        Dim ds As New DataSet
        Dim sSql As String

        If sCode.Length = 0 Then
            sSql = "select * from tblsupplier order by supplierCode"
        Else
            sSql = "SELECT * FROM tblsupplier WHERE supplierCode =" & fncFormatSQLText(sCode)

        End If
        Try
            ds = dm.getDataSetWithSqlAdapter(sSql, "tblsupplier", "tblsupplier")
            GetSupplierDetail = ds
            Exit Function
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
            ds = Nothing
        End Try
    End Function


#End Region

#Region "Get Supplier Search Detail"
    Public Function GetSupplierSearchDetail(ByVal sQuery As String) As DataSet
        Dim ds As New DataSet
        If sQuery.Length > 0 Then
            Try
                ds = dm.getDataSetWithSqlAdapter(sQuery, "tablename", "tablename")
                GetSupplierSearchDetail = ds
                Exit Function
            Catch ex As Exception
                GetSupplierSearchDetail = Nothing
                MsgBox(Err.Description)
            Finally
                Err.Clear()
                ds = Nothing
            End Try
        End If
    End Function

#End Region

#Region "Get Supplier Purchase Detail"
    Public Function GetSupplierPurchaseDetail(ByVal sSupplierCode As String) As DataSet
        Dim ds As New DataSet
        Dim sSql As String

        sSql = "Select JobSheetNo, PurchaseBillNo, PurchaseBillDate, TotalWeight24K, TotalPCS, TotalLocalCost from tblJobSheetHeader " & _
                "where SupplierCode = " & fncFormatSQLText(sSupplierCode) & " Order By JobSheetDate Desc"

        Try
            ds = dm.getDataSetWithSqlAdapter(sSql, "tblJobSheetHeader", "tblJobSheetHeader")
            GetSupplierPurchaseDetail = ds
            Exit Function
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
            ds = Nothing
        End Try
    End Function

#End Region

#Region "Get Supplier Purchase Detail by JobSheetNo"
    Public Function GetPurchaseDetaillByJSNo(ByVal sJobsheetNo As String) As DataSet
        Dim ds As New DataSet
        Dim sSql As String

        sSql = "Select InventoryCode, Description, Weight, PCS, TotalCost From tblPurchase where JobSheetNo = " & _
                fncFormatSQLText(sJobsheetNo) & " Order By InventoryCode"
        Try
            ds = dm.getDataSetWithSqlAdapter(sSql, "tblJobSheetHeader", "tblJobSheetHeader")
            GetPurchaseDetaillByJSNo = ds
            Exit Function
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            Err.Clear()
            ds = Nothing
        End Try
    End Function

#End Region

#Region "Added by Sarathy"

    Public Function IsSupplierCodeExist(ByVal sCode As String) As Boolean
        Dim ds As New DataSet
        Dim sSql As String
        Dim bStatus As Boolean
        sSql = "SELECT * FROM tblsupplier WHERE supplierCode =" & fncFormatSQLText(sCode)
        Try
            ds = dm.getDataSetWithSqlAdapter(sSql, "supplier", "supplier")
            If ds.Tables(0).Rows.Count > 0 Then
                bStatus = True
            Else
                bStatus = False
            End If
        Catch ex As Exception
            MsgBox(Err.Description)
        Finally
            IsSupplierCodeExist = bStatus
            ds = Nothing
        End Try
    End Function

#End Region
End Class
