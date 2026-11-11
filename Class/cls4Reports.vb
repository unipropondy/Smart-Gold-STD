Public Class Cls4Reports
    Public Sub New()
        Dim clsFillData As New DataManager
        'clsFillData.SetMasterDataSetFromMasterDB("Exec sp_getVendorlist ", "SupplierDetails")
        clsFillData.SetMasterDataSet("Exec sp_getVendorlist ", "SupplierDetails")
        'clsFillData.SetMasterDataSet("Exec AR_getCustomer", "CustomerDetails")
        clsFillData.SetMasterDataSetAcc("Exec sp_getBankMaster ", "BankMaster")
        clsFillData.SetMasterDataSetAcc(" Exec sp_getAccountDept", "Department")
    End Sub
    Public Function fnPeriodRecords() As DataRow()
        Dim Dr() As DataRow
        Dim Dm As New DataManager
        Dr = Dm.RowReturnProjExecNonQryWithDataRowAcc(" exec Sp_GetMasterAgingRpt")
        fnPeriodRecords = Dr
    End Function
End Class
