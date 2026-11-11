Imports System
Imports System.Collections
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization

Public Class clsJobSheet
    Public DM As New DataManager
    

#Region "Added by Murugan"
    Public Function GetJobSheetByInvoice(ByVal sSupplier As String, ByVal sInvoiceNo As String) As DataRow()
        Dim sSelect As String
        GetJobSheetByInvoice = Nothing
        sSelect = "SELECT *,Pdate = CONVERT(CHAR(8),PurchaseBillDate,112) FROM tblJobSheetHeader Where SupplierCode =" & _
                    fncFormatSQLText(sSupplier) & " And PurchaseBillNo = " & fncFormatSQLText(sInvoiceNo)
        Try
            GetJobSheetByInvoice = DM.RowReturnProjExecNonQryWithDataRow(sSelect)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Public Function fncGetDatasetWithQuery(ByVal sDsName As String, ByVal sSql As String) As DataSet
        Dim objCon As SqlConnection = New SqlConnection(gsCon)
        Dim Objcmd As SqlCommand = New SqlCommand(sSql, objCon)
        Objcmd.CommandTimeout = 60
        Dim objAdapter As SqlDataAdapter = New SqlDataAdapter(Objcmd) 'New SqlDataAdapter(sSql, objCon)
        Dim dsData As DataSet = New DataSet()
        objAdapter.Fill(dsData, sDsName)
        fncGetDatasetWithQuery = dsData
    End Function
    Public Function fncBillClose(ByVal sJobsheetNo As String) As Boolean
        Dim sQry As String
        Dim Status As Boolean
        Try
            sQry = "Update tblJobSheetHeader Set BillClose = 'Y', ModifyUser = " & fncFormatSQLText(sUserID) & " , ModifyDate = getdate() where JobSheetNo = " & fncFormatSQLText(sJobsheetNo)
            Status = DM.ProjExecNonQry(sqry)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            fncBillClose = False
        Finally
            fncBillClose = True
        End Try


    End Function
    Public Function GetUpdateJobSheetNo() As DataSet
        Dim sQry As String
        Dim Ds As New DataSet
        Try

            sQry = "EXEC sp_GetUpdateJobSheetNo "
            Ds = DM.getDataSetWithSqlAdapter(sQry, "JobSheetNo", "JobSheetNo")

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetUpdateJobSheetNo = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function JobSheetHeaderSave(ByVal sJobsheetNo As String, ByVal sPBillNo As String, ByVal sPDate As String, _
                                   ByVal sPayableType As String, ByVal sSupplierCode As String, ByVal sCurrencyCode As String, _
                                   ByVal nCurrencyRate As Double, ByVal nCostPerGram As Double, _
                                   ByVal nTotalWeight As Double, ByVal nTotalGoldLoss As Double, _
                                   ByVal nTotalMC As Double, ByVal nTotalPCS As Double, _
                                   ByVal nTotalLocalCost As Double, ByVal nGST As Double, ByVal nTotalForeignCost As Double, _
                                   ByVal nManualCost As Double, ByVal nManualMC As Double, ByVal nManualWeight As Double, ByVal nManualGoldLoss As Double, _
                                   ByVal sRemarks As String, ByVal sTranType As String, ByVal sTaelGST As String, ByVal nAdjustment As Double, _
                                   ByVal sUserId As String, ByVal sFixStatus As String, ByVal sSplCategory As String, ByVal sAccessories As String, _
                                   ByVal sMatchJobSheetNo As String, ByVal nMatchWeight As Double, ByVal nMatchPCS As Double, ByVal sSalesOrderNo As String, _
                                   ByVal nTaelPercentage As Double, ByVal nTaelrate As Double, ByVal sClassType As String, ByVal sDDate As String) As Boolean

        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = ""
            sQry = sQry & "EXEC sp_JobSheetHeader " & fncFormatSQLText(sJobsheetNo) & ","
            sQry = sQry & fncFormatSQLText(sPBillNo) & ","
            sQry = sQry & fncFormatSQLDate(sPDate) & ","
            sQry = sQry & fncFormatSQLText(sPayableType) & ","
            sQry = sQry & fncFormatSQLText(sSupplierCode) & ","
            sQry = sQry & fncFormatSQLText(sCurrencyCode) & ","
            sQry = sQry & Val(nCurrencyRate) & ","
            sQry = sQry & Val(nCostPerGram) & ","
            sQry = sQry & Val(nTotalWeight) & ","
            sQry = sQry & Val(nTotalGoldLoss) & ","
            sQry = sQry & Val(nTotalMC) & ","
            sQry = sQry & Val(nTotalPCS) & ","
            sQry = sQry & Val(nTotalLocalCost) & ","
            sQry = sQry & Val(nGST) & ","
            sQry = sQry & Val(nTotalForeignCost) & ","
            sQry = sQry & Val(nManualCost) & ","
            sQry = sQry & Val(nManualMC) & ","
            sQry = sQry & Val(nManualWeight) & ","
            sQry = sQry & Val(nManualGoldLoss) & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & fncFormatSQLText(sTranType) & ","
            sQry = sQry & fncFormatSQLText(sTaelGST) & ","
            sQry = sQry & fncFormatSQLText(sUserId) & ","
            sQry = sQry & fncFormatSQLText(Trim$(gthisLocation)) & ","
            sQry = sQry & Val(nAdjustment) & ","
            sQry = sQry & fncFormatSQLText(sFixStatus) & ","
            sQry = sQry & fncFormatSQLText(sSplCategory) & ","
            sQry = sQry & fncFormatSQLText(sAccessories) & ","
            sQry = sQry & fncFormatSQLText(sMatchJobSheetNo) & ","
            sQry = sQry & Val(nMatchWeight) & ","
            sQry = sQry & Val(nMatchPCS) & ","
            sQry = sQry & fncFormatSQLText(sSalesOrderNo) & ","
            sQry = sQry & Val(nTaelPercentage) & ","
            sQry = sQry & Val(nTaelrate) & ","
            sQry = sQry & fncFormatSQLText(sClassType) & ","
            sQry = sQry & fncFormatSQLDate(sDDate)
            SaveStatus = DM.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            JobSheetHeaderSave = SaveStatus
            sQry = Nothing
        End Try
    End Function
    Public Function JobSheetDetailSave(ByVal sJobsheetNo As String, ByVal sCatalogueNo As String, ByVal sCategoryCode As String, _
                               ByVal sDescription As String, _
                               ByVal nCostPerGram As Double, ByVal nWeight As Double, _
                               ByVal nMC As Double, ByVal nPCS As Double, _
                               ByVal nGoldLoss As Double, ByVal nTotalCost As Double, _
                               ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim DetailSave As Boolean

        Try
            JobSheetDetailSave = False

            sQry = ""
            sQry = sQry & "EXEC sp_JobSheetDetail " & fncFormatSQLText(sJobsheetNo) & ","
            sQry = sQry & fncFormatSQLText(sCatalogueNo) & ","
            sQry = sQry & fncFormatSQLText(sCategoryCode) & ","
            sQry = sQry & fncFormatSQLText(sDescription) & ","
            sQry = sQry & Val(nCostPerGram) & ","
            sQry = sQry & Val(nWeight) & ","
            sQry = sQry & Val(nMC) & ","
            sQry = sQry & Val(nPCS) & ","
            sQry = sQry & Val(nGoldLoss) & ","
            sQry = sQry & Val(nTotalCost) & ","
            sQry = sQry & fncFormatSQLText(sUserId)

            DetailSave = DM.ProjExecNonQry(sQry)

            JobSheetDetailSave = True

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            DetailSave = False
        Finally
            JobSheetDetailSave = DetailSave
            sQry = Nothing
        End Try

    End Function
    Public Function AccessoriesSave(ByVal sJobsheetNo As String, ByVal sCatalogueNo As String, ByVal sCategoryCode As String, _
                               ByVal sDescription As String, _
                               ByVal nCostPerGram As Double, ByVal nWeight As Double, _
                               ByVal nMC As Double, ByVal nPCS As Double, _
                               ByVal nGoldLoss As Double, ByVal nTotalCost As Double, _
                               ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim DetailSave As Boolean

        Try
            AccessoriesSave = False

            sQry = ""
            sQry = sQry & "EXEC sp_AccessoriesDetailSave " & fncFormatSQLText(sJobsheetNo) & ","
            sQry = sQry & fncFormatSQLText(sCatalogueNo) & ","
            sQry = sQry & fncFormatSQLText(sCategoryCode) & ","
            sQry = sQry & fncFormatSQLText(sDescription) & ","
            sQry = sQry & Val(nCostPerGram) & ","
            sQry = sQry & Val(nWeight) & ","
            sQry = sQry & Val(nMC) & ","
            sQry = sQry & Val(nPCS) & ","
            sQry = sQry & Val(nGoldLoss) & ","
            sQry = sQry & Val(nTotalCost) & ","
            sQry = sQry & fncFormatSQLText(sUserId)

            DetailSave = DM.ProjExecNonQry(sQry)

            AccessoriesSave = True

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            DetailSave = False
        Finally
            AccessoriesSave = DetailSave
            sQry = Nothing
        End Try

    End Function
    Public Function SalesOrderHeaderSave(ByVal sSalesOrderNo As String, ByVal sCatalogueNo As String, ByVal sCategoryCode As String, _
                                         ByVal nPCS As Double, ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim DetailSave As Boolean

        Try
            SalesOrderHeaderSave = False

            sQry = ""
            sQry = sQry & "EXEC sp_SalesOrderHeaderUpdate " & fncFormatSQLText(sSalesOrderNo) & ","
            sQry = sQry & fncFormatSQLText(sCatalogueNo) & ","
            sQry = sQry & fncFormatSQLText(sCategoryCode) & ","
            sQry = sQry & Val(nPCS) & ","
            sQry = sQry & fncFormatSQLText(sUserId)

            DetailSave = DM.ProjExecNonQry(sQry)

            SalesOrderHeaderSave = True

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            DetailSave = False
        Finally
            SalesOrderHeaderSave = DetailSave
            sQry = Nothing
        End Try

    End Function
    Public Function fncCheckJobSheetDelete(ByVal sJobsheetNo As String) As Boolean
        Dim sSql As String
        Dim Ds As New DataSet
        Try
            sSql = "select jobsheetno from tblJobsheetheader where JobSheetNo= " & fncFormatSQLText(sJobsheetNo) & _
                " and JobSheetno not in(select JobSheetNo from tblPurchase)"
            Ds = DM.getDataSetWithSqlAdapter(sSql, "Jobsheet", "Jobsheet")
            If Ds.Tables("JobSheet").Rows.Count = 0 Then
                fncCheckJobSheetDelete = False
            Else
                fncCheckJobSheetDelete = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Function
    Public Function UpdateModifyUser(ByVal sJobsheetNo As String, ByVal sCatalogueCode As String, _
            ByVal sUID As String, ByVal sMode As String) As Boolean
        Try
            Dim sQry As String
            Dim status As Boolean

            UpdateModifyUser = False
            sQry = ""
            sQry = sQry & "EXEC sp_UpdateModifyUser " & fncFormatSQLText(sJobsheetNo) & ","
            sQry = sQry & fncFormatSQLText(sCatalogueCode) & ","
            sQry = sQry & fncFormatSQLText(sUID) & ","
            sQry = sQry & fncFormatSQLText(sMode)
            status = DM.ProjExecNonQry(sQry)

            UpdateModifyUser = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            UpdateModifyUser = False
        End Try
    End Function
    Public Function DeleteSave(ByVal sJobsheetNo As String, ByVal sUID As String) As Boolean

        Dim sExec As String
        Dim Status As Boolean
        Try
            DeleteSave = False

            sExec = ""
            sExec = sExec & "EXEC sp_JobSheetDeleteLog" & fncFormatSQLText(sJobsheetNo) & ","
            sExec = sExec & fncFormatSQLText(sUID)
            Status = DM.ProjExecNonQry(sExec)
            DeleteSave = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            DeleteSave = False
        End Try
    End Function
    Public Function JobSheetDelete(ByVal sJobsheetNo As String, ByVal sUID As String) As Boolean
        Dim sExec As String
        Dim status As Boolean
        Try
            JobSheetDelete = False

            sExec = ""
            sExec = sExec & "EXEC sp_JobSheetDelete " & fncFormatSQLText(sJobsheetNo) & ","
            sExec = sExec & fncFormatSQLText(sUID)
            status = DM.ProjExecNonQry(sExec)
            JobSheetDelete = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            JobSheetDelete = False
        End Try
    End Function
    Public Function GetJobSheet(ByVal sJobsheetNo As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String

        Try
            GetJobSheet = Nothing
            sQry = "SELECT *,Pdate = CONVERT(CHAR(8),PurchaseBillDate,112),Ddate = CONVERT(CHAR(8),DueDate,112) FROM tblJobSheetHeader WHERE JobSheetNo =" & fncFormatSQLText(sJobsheetNo)
            Ds = DM.getDataSetWithSqlAdapter(sQry, "JobSheet", "JobSheet")

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            Ds = Nothing
        Finally
            GetJobSheet = Ds
            Ds = Nothing
        End Try
    End Function
    Public Function GetPurchaseFix(ByVal sInvoiceNo As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String

        Try
            GetPurchaseFix = Nothing
            sQry = "SELECT * FROM tblPurchaseFix WHERE InvoiceNo =" & fncFormatSQLText(sInvoiceNo)
            Ds = DM.getDataSetWithSqlAdapter(sQry, "JobSheet", "JobSheet")

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            Ds = Nothing
        Finally
            GetPurchaseFix = Ds
            Ds = Nothing
        End Try
    End Function
    Public Function GetPurchaseDetail(ByVal sJobsheetNo As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String

        Try
            GetPurchaseDetail = Nothing
            sQry = "SELECT sum(Weight)Weight,Sum(SMC)SMC,sum(TotalCost)TotalCost FROM tblInventory WHERE JobSheetNo =" & fncFormatSQLText(sJobsheetNo)
            Ds = DM.getDataSetWithSqlAdapter(sQry, "JobSheet", "JobSheet")

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            Ds = Nothing
        Finally
            GetPurchaseDetail = Ds
            Ds = Nothing
        End Try
    End Function
    Public Function GetJobSheetHeader(ByVal sJobsheetNo As String) As DataSet
        Dim Ds As New DataSet
        Dim sSelect As String
        Try
            GetJobSheetHeader = Nothing
            sSelect = "SELECT * FROM tblJobSheetHeader WHERE JobSheetNo =" & fncFormatSQLText(sJobsheetNo)
            Ds = DM.getDataSetWithSqlAdapter(sSelect, "JobSheet", "JobSheet")

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetJobSheetHeader = Ds
            Ds = Nothing
        End Try
    End Function
    Public Function GetJobSheetDetail(ByVal sJobsheetNo As String) As DataSet
        Dim Ds As New DataSet
        Dim sSelect As String
        Try
            GetJobSheetDetail = Nothing
            sSelect = "SELECT * FROM tblJobSheetDetail WHERE JobSheetNo =" & fncFormatSQLText(sJobsheetNo)
            Ds = DM.getDataSetWithSqlAdapter(sSelect, "JobSheet", "JobSheet")

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            Ds = Nothing
        Finally
            GetJobSheetDetail = Ds
            Ds = Nothing
        End Try
    End Function
    Public Function GetJobSheetDetailByCatCode(ByVal sJobsheetNo As String, ByVal sCatalogue As String) As DataSet
        Dim Ds As New DataSet
        Dim sSelect As String
        Try
            GetJobSheetDetailByCatCode = Nothing
            sSelect = "SELECT * FROM tblJobSheetDetail WHERE JobSheetNo =" & fncFormatSQLText(sJobsheetNo)
            sSelect = sSelect & " and CatalogueCode = " & fncFormatSQLText(sCatalogue)
            Ds = DM.getDataSetWithSqlAdapter(sSelect, "JobSheet", "JobSheet")

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            Ds = Nothing
        Finally
            GetJobSheetDetailByCatCode = Ds
            Ds = Nothing
        End Try
    End Function
    Public Function GetJobSheetDetailWeight(ByVal sJobsheetNo As String) As DataSet
        Dim Ds As New DataSet
        Dim ssql As String
        Try
            GetJobSheetDetailWeight = Nothing
            ssql = "SELECT CategoryCode,BillWt=Sum(Weight) FROM tblJobSheetDetail WHERE JobSheetNo =" & fncFormatSQLText(sJobsheetNo)
            ssql = ssql & " Group By CategoryCode"
            Ds = DM.getDataSetWithSqlAdapter(ssql, "JobSheet", "Jobsheet")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        Finally
            GetJobSheetDetailWeight = Ds
            Ds = Nothing
        End Try
    End Function
    Public Function GetJobSheetWeight(ByVal sJobsheetNo As String, ByVal sCategoryCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            GetJobSheetWeight = Nothing
            sQry = "SELECT BillWt = Sum(Weight),BillPcs = Sum(Pcs) FROM tblJobSheetDetail Where JobSheetNo = " & fncFormatSQLText(sJobsheetNo)
            sQry = sQry & " And CategoryCode = " & fncFormatSQLText(sCategoryCode)
            Ds = DM.getDataSetWithSqlAdapter(sQry, "JobSheet", "JobSheet")
            GetJobSheetWeight = Ds
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            Ds = Nothing
        Finally

        End Try
    End Function
    Public Function OpenBatchPostinConnection()
        gsConBatchPosting.ConnectionString = gsCon
        gsConBatchPosting.Open()
    End Function
    Public Function gpBeginTransaction()
        gsTrans = gsConBatchPosting.BeginTransaction
    End Function
    Public Function gpCommitTransaction()
        gsTrans.Commit()
    End Function
    Public Function CloseBatchConnection()
        gsConBatchPosting.Close()
    End Function
    Public Function gpRollBackBatchConnection()
        gsTrans.Rollback()
    End Function
    Public Function fncGetServerDate() As String
        Dim ds As DataSet
        Dim sSqlQry, sServerDate As String
        sServerDate = String.Empty
        sSqlQry = "select GetDate() ServerDate"
        ds = fncGetDatasetWithQuery("SD", sSqlQry)
        If ds.Tables("SD").Rows.Count > 0 Then
            sServerDate = ds.Tables("SD").Rows(0)("ServerDate").ToString()
        End If
        fncGetServerDate = sServerDate
        ds = Nothing
    End Function
    Public Function fncExecuteQuery(ByVal sExec As String) As Boolean
        Dim objCon As SqlConnection = New SqlConnection(gsCon)
        Try
            objCon.Open()
            Dim objCmd As SqlCommand = objCon.CreateCommand()
            objCmd.CommandTimeout = 90
            objCmd.CommandText = sExec
            objCmd.ExecuteNonQuery()
            objCon.Close()
            fncExecuteQuery = True
        Catch ex As Exception
            fncExecuteQuery = False
            Throw ex
        End Try
    End Function
#End Region
End Class
