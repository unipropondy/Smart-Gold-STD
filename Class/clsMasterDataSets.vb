Public Class clsMasterDataSets
    Dim ClsObj As New DataManager
    Public Sub SetDataOnPackageLoad1()
        ClsObj.SetMasterDataSetAcc("Exec sp_getChartOfAccounts", "ChartOfAccounts")
        ClsObj.SetMasterDataSetAcc(" Exec sp_getAccountDept", "Department")
        ClsObj.SetMasterDataSetAcc("Exec sp_getBankMaster", "Banks")
    End Sub
    Public Sub SetDataOnPackageLoad2()
        Dim sLoc As String
        If sLocationCodeAcc = "CM" Then
            ClsObj.SetMasterDataSetAcc(" Exec sp_getAPControlLedger  '001'", "APCLedger")
        Else
            sLoc = ClsObj.ValReturnProjExecNonQryAcc("Select LocationCode from tblAccountLocation ")
            If Len(Trim(sLoc)) = 0 Then
                MsgBox("Error in Location Code")
                Exit Sub
            End If
            ClsObj.SetMasterDataSetAcc(" Exec sp_getAPControlLedger " & fncFormatSQLText(Trim(sLoc)), "APCLedger")
        End If
        'ClsObj.SetMasterDataSet(" Exec sp_getAPControlLedger", "APCLedger")
        '  ClsObj.SetMasterDataSetFromMasterDB("Exec sp_getVendorList", "SupplierDetails")
    End Sub
    Public Sub SetDataOnPackageLoad3()

        Dim sLoc As String
        If sLocationCode = "CM" Then
            ClsObj.SetMasterDataSetAcc(" Exec AR_GetControlLedger '001'", "ARCLedger")
        Else
            sLoc = ClsObj.ValReturnProjExecNonQryAcc("Select LocationCode from tblAccountLocation ")
            If Len(Trim(sLoc)) = 0 Then
                MsgBox("Error in Location Code")
                Exit Sub
            End If
            ClsObj.SetMasterDataSetAcc(" Exec AR_GetControlLedger " & fncFormatSQLText(Trim(sLoc)), "ARCLedger")
        End If

        'ClsObj.SetMasterDataSet(" Exec AR_GetControlLedger", "ARCLedger")
        ClsObj.SetMasterDataSetAcc(" Exec AR_getCustomer", "CustomerDetails")
        ClsObj.SetMasterDataSetAcc("Exec AR_getSalesMan", "SalesMan")
    End Sub
    Public Sub SetDataOnGroupPost()
        Dim sSql As String
        Dim sSql1 As String
        Dim sLoc As String
        Dim Dr() As DataRow
        ClsObj.SetMasterDataSetAcc("Exec sp_getChartOfAccounts", "ChartOfAccounts")
        ClsObj.SetMasterDataSetAcc(" Exec sp_getAccountDept", "Department")
        ClsObj.SetMasterDataSetAcc("Exec sp_getBankMaster", "Banks")
        If sLocationCodeAcc = "CM" Then
            sSql = "Exec sp_getAPControlLedger '001'"
            sSql1 = "Exec AR_GetControlLedger '001'"
        Else
            Dr = ClsObj.RowReturnProjExecNonQryWithDataRowAcc("select * from tblAccountLocation")
            If Dr.Length > 0 Then
                sLoc = Trim(Dr(0)("LocationCode") & "")
            End If
            sSql = "Exec sp_getAPControlLedger " & fncFormatSQLText(sLoc)
            Dr = ClsObj.RowReturnProjExecNonQryWithDataRowAcc("select * from tblAccountLocation ")
            If Dr.Length > 0 Then
                sLoc = Trim(Dr(0)("LocationCode") & "")
            End If
            sSql1 = "Exec AR_GetControlLedger " & fncFormatSQLText(sLoc)
        End If
        ClsObj.SetMasterDataSetAcc(sSql, "APCLedger")
        ClsObj.SetMasterDataSetAcc(sSql1, "ARCLedger")
        'ClsObj.SetMasterDataSet(" Exec sp_getAPControlLedger", "APCLedger")
        'ClsObj.SetMasterDataSet(" Exec AR_GetControlLedger", "ARCLedger")
    End Sub
End Class
