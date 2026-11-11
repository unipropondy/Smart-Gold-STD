Public Class ClsCategory
    Dim dm As New DataManager

#Region "Fetch Category"

    Public Function GetCategory() As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select CategoryCode, CategoryName, ClassCode, FromInventoryCode, ToInventoryCode, NextInventoryCode, Remarks,SizeFactor,InventoryPrefix,ShowInReceipt from tblCategory "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Category", "Category")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetCategory = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetCategoryML() As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select CategoryCode, CategoryName, ClassCode,InventoryPrefix, FromInventoryCode, ToInventoryCode, NextInventoryCode, Remarks from tblCategory "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Category", "Category")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetCategoryML = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function


    Public Function GetCategoryName(ByVal sCategoryCode As String) As String
        Dim Ds As New DataSet
        Dim sQry As String
        Dim sCategoryName As String
        sCategoryName = Nothing
        Try
            sQry = " select CategoryCode, CategoryName from tblCategory where CategoryCode = " & fncFormatSQLText(sCategoryCode)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Category", "Category")
            If Ds.Tables("Category").Rows.Count > 0 Then
                sCategoryName = Ds.Tables("Category").Rows(0)("CategoryName")
            Else
                sCategoryName = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            sCategoryName = Nothing
        Finally
            GetCategoryName = sCategoryName
            Ds = Nothing
            sQry = Nothing
            sCategoryName = Nothing
        End Try
    End Function

    Public Function GetInventoryRange(ByVal lInventoryCode As Long, ByVal sInventoryPrefix As String) As Boolean
        Dim Ds As New DataSet
        Dim sQry As String
        Dim bExist As Boolean
        Try
            sQry = " select Count(CategoryCode) from tblCategory where InventoryPrefix =" & fncFormatSQLText(sInventoryPrefix) & "  and " & Val(lInventoryCode) & " between frominventorycode and toinventorycode "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Category", "Category")
            If Ds.Tables("Category").Rows(0)(0) > 0 Then
                bExist = True
            Else
                bExist = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            GetInventoryRange = bExist
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetInventoryPrefix(ByVal sCategoryCode As String) As String
        Dim Ds As New DataSet
        Dim sQry As String
        Dim sInventoryPrefix As String
        sInventoryPrefix = Nothing
        Try
            sQry = " select Inventoryprefix from tblCategory where CategoryCode = " & fncFormatSQLText(sCategoryCode)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Category", "Category")
            If Ds.Tables("Category").Rows.Count > 0 Then
                sInventoryPrefix = Ds.Tables("Category").Rows(0)("InventoryPrefix")
            Else
                sInventoryPrefix = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            sInventoryPrefix = Nothing
        Finally
            GetInventoryPrefix = sInventoryPrefix
            Ds = Nothing
            sQry = Nothing
            sInventoryPrefix = Nothing
        End Try
    End Function


#End Region

#Region "Delete Category"

    Public Function DeleteCategroy(ByVal sCategoryCode As String) As Boolean
        Dim sQry As String
        Dim DelStatus As Boolean
        Try
            sQry = " Delete from tblCategory where CategoryCode = " & fncFormatSQLText(sCategoryCode)
            DelStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            DelStatus = False
        Finally
            DeleteCategroy = DelStatus
            sQry = Nothing
        End Try
    End Function

#End Region
    Public Function GetInventoryListByCategoryCode(ByVal sCategoryCode As String) As Boolean
        Dim sQry As String
        Dim GetStatus As Boolean
        Try
            sQry = " Select * from tblInventory where CategoryCode= " & fncFormatSQLText(sCategoryCode)
            GetStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            GetStatus = False
        Finally
            GetInventoryListByCategoryCode = GetStatus
            sQry = Nothing
        End Try
    End Function


#Region "Save Category"

    Public Function CategorySave(ByVal sCategoryCode As String, ByVal sCategoryName As String, ByVal sClassCode As String, ByVal sFromInventoryCode As String, _
        ByVal sToInventoryCode As String, ByVal sNextInventoryCode As String, ByVal sRemarks As String, ByVal sSize As String, ByVal sInventoryPrefix As String, ByVal sShowinReport As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_Category " & fncFormatSQLText(sCategoryCode) & ","
            sQry = sQry & fncFormatSQLText(sCategoryName) & ","
            sQry = sQry & fncFormatSQLText(sClassCode) & ","
            sQry = sQry & fncFormatSQLText(sFromInventoryCode) & ","
            sQry = sQry & fncFormatSQLText(sToInventoryCode) & ","
            sQry = sQry & fncFormatSQLText(sNextInventoryCode) & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & fncFormatSQLText(sUserID) & ","
            sQry = sQry & fncFormatSQLText(sSize) & ","
            sQry = sQry & fncFormatSQLText(sInventoryPrefix) & ","
            sQry = sQry & fncFormatSQLText(sShowinReport)
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            CategorySave = SaveStatus
            sQry = Nothing
        End Try
    End Function
    Public Function CategorySaveML(ByVal sCategoryCode As String, ByVal sCategoryName As String, ByVal sClassCode As String, ByVal sInventoryPrefix As String, ByVal sFromInventoryCode As String, _
       ByVal sToInventoryCode As String, ByVal sNextInventoryCode As String, ByVal sRemarks As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_CategoryML " & fncFormatSQLText(sCategoryCode) & ","
            sQry = sQry & fncFormatSQLText(sCategoryName) & ","
            sQry = sQry & fncFormatSQLText(sClassCode) & ","
            sQry = sQry & fncFormatSQLText(sInventoryPrefix) & ","
            sQry = sQry & fncFormatSQLText(sFromInventoryCode) & ","
            sQry = sQry & fncFormatSQLText(sToInventoryCode) & ","
            sQry = sQry & fncFormatSQLText(sNextInventoryCode) & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & fncFormatSQLText(sUserID)
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            CategorySaveML = SaveStatus
            sQry = Nothing
        End Try
    End Function

#End Region
    Public Function GetInventoryRangeML(ByVal lInventoryCode As String) As Boolean
        Dim Ds As New DataSet
        Dim sQry As String
        Dim bExist As Boolean
        Try
            sQry = " select Count(CategoryCode) from tblCategory where " & Trim(lInventoryCode) & " between substring(frominventorycode,5,6) and substring(toinventorycode,5,6) "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Category", "Category")
            If Ds.Tables("Category").Rows(0)(0) > 0 Then
                bExist = True
            Else
                bExist = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            GetInventoryRangeML = bExist
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function


#Region "Added by Murugan"

    Public Function GetCategorywithCode(ByVal sCategoryCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select * from tblCategory where CategoryCode = " & fncFormatSQLText(sCategoryCode)
            sQry = sQry & " And NextInventorycode Between FromInventoryCode and ToInventorycode"
            Ds = dm.getDataSetWithSqlAdapterAcc(sQry, "Category", "Category")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetCategorywithCode = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

#End Region

    Public Function GetCategorywithCodeML(ByVal sCategoryCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select * from tblCategory where CategoryCode = " & fncFormatSQLText(sCategoryCode)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Category", "Category")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetCategorywithCodeML = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetInventoryListByCategoryCodeML(ByVal sCategoryCode As String) As Boolean
        Dim sQry As String
        Dim GetStatus As Boolean
        Try
            sQry = " Select * from tblInventory where CategoryCode= " & fncFormatSQLText(sCategoryCode)
            GetStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            GetStatus = False
        Finally
            GetInventoryListByCategoryCodeML = GetStatus
            sQry = Nothing
        End Try
    End Function
    Public Function GetCategorywithSizeCode(ByVal sCategoryCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select SizeFactor from tblCategory  where CategoryCode= " & fncFormatSQLText(sCategoryCode)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Category", "Category")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetCategorywithSizeCode = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
End Class
