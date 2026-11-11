Public Class ClsClass
    Dim dm As New DataManager

#Region "FetchClass "

    Public Function GetClassDetail(ByVal sClassCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select * from tblclass "
            If sClassCode.Trim <> "" Then sQry = sQry & " Where ClassCode = " & fncFormatSQLText(sClassCode)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Class", "Class")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetClassDetail = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

#End Region
    
#Region "DeleteCategroy"

    Public Function DeleteClassDetail(ByVal ClassCode As String) As Boolean
        Dim Sqry As String
        Dim DelStatus As Boolean
        Try
            Sqry = " Delete from tblClass where ClassCode = " & fncFormatSQLText(ClassCode)
            DelStatus = dm.ProjExecNonQry(Sqry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            DelStatus = False
        Finally
            DeleteClassDetail = DelStatus
            Sqry = Nothing
        End Try
    End Function

#End Region

#Region "SaveClass"

    Public Function ClassSave(ByVal sClassCode As String, ByVal sClassName As String, ByVal sSalesType As String, ByVal sPurchaseType As String, ByVal sGoldLoss As String, _
    ByVal sDisplayCost As String, ByVal sGoldBarCode As String, ByVal dRate As Double, ByVal dMCDiscount As Double, ByVal sRemarks As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_Class " & fncFormatSQLText(sClassCode) & ","
            sQry = sQry & fncFormatSQLText(sClassName) & ","
            sQry = sQry & fncFormatSQLText(sSalesType) & ","
            sQry = sQry & fncFormatSQLText(sPurchaseType) & ","
            sQry = sQry & fncFormatSQLText(sGoldLoss) & ","
            sQry = sQry & fncFormatSQLText(sDisplayCost) & ","
            sQry = sQry & fncFormatSQLText(sGoldBarCode) & ","
            sQry = sQry & Val(dRate) & ","
            sQry = sQry & Val(dMCDiscount) & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & fncFormatSQLText(sUserID)
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            ClassSave = SaveStatus
            sQry = Nothing
        End Try
    End Function

    Public Function DepositSave(ByVal sCustCode As String, ByVal sCustName As String, ByVal sDepositType As String, ByVal dAmount As Double, ByVal sRemarks As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_DepositSave " & fncFormatSQLText(sCustCode) & ","
            sQry = sQry & fncFormatSQLText(sCustName) & ","
            sQry = sQry & fncFormatSQLText(sDepositType) & ","
            sQry = sQry & Val(dAmount) & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & fncFormatSQLText(sUserID) & ","
            sQry = sQry & fncFormatSQLText(gthisLocation)
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            DepositSave = SaveStatus
            sQry = Nothing
        End Try
    End Function


#End Region

#Region "FetchGoldBarCode"

    Public Function GetGoldBarCode() As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " SELECT ClassCode,ClassName FROM tblClass WHERE GoldBarCode = " & fncFormatSQLText("Y")
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Class", "Class")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetGoldBarCode = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

#End Region

#Region "Fetch ClassName"

    Public Function GetClassName(ByVal sClassCode As String) As String
        Dim Ds As New DataSet
        Dim sQry As String
        Dim sClassName As String
        sClassName = Nothing
        Try
            sQry = " select ClassCode, ClassName from tblClass where ClassCode = " & fncFormatSQLText(sClassCode)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Class", "Class")
            If Ds.Tables("Class").Rows.Count > 0 Then
                sClassName = Ds.Tables("Class").Rows(0)("ClassName")
            Else
                sClassName = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            sClassName = Nothing
        Finally
            GetClassName = sClassName
            Ds = Nothing
            sQry = Nothing
            sClassName = Nothing
        End Try
    End Function

    Public Function GetClassForInventory(ByVal sinventorycode As String) As DataSet

        Dim Ds As DataSet
        Dim sQry As String

        Try
            GetClassForInventory = Nothing
            sQry = "SELECT * FROM tblPurchase WHERE InventoryCode =" & fncFormatSQLText(sinventorycode)

            Ds = dm.getDataSetWithSqlAdapter(sQry, "Purchase", "Purchase")

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        Finally
            GetClassForInventory = Ds
        End Try

    End Function

    Public Function GetClassRate() As DataSet

        Dim Ds As DataSet
        Dim sQry As String

        Try
            GetClassRate = Nothing
            sQry = "SELECT ClassCode,Rate FROM tblClass order by ClassCode "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Class", "Class")

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        Finally
            GetClassRate = Ds
        End Try

    End Function

#End Region

End Class
