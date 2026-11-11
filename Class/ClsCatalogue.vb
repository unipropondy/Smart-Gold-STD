Public Class ClsCatalogue
    Dim dm As New DataManager

#Region "Fetch Catalogue"

    Public Function GetCatalogue(ByVal sCatalogueName As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select CatalogueCode, CatalogueName, CategoryCode, GoldLoss, Remarks,CatalogueImage,CatalogueImageName,ReorderLevel from tblCatalogue  "
            If sCatalogueName.Trim <> "" Then sQry = sQry & " where CatalogueName like " & fncFormatSQLText(sCatalogueName & "%")
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Catalogue", "Catalogue")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetCatalogue = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function


    Public Function GetCataloguePOSCode(ByVal sCatalogueCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select CatalogueCode, CatalogueName, CategoryCode, GoldLoss, Remarks,CatalogueImage,CatalogueImageName,isnull(ReorderLevel,0)ReorderLevel from tblCatalogue  "
            If sCatalogueCode.Trim <> "" Then sQry = sQry & " where CatalogueCode like " & fncFormatSQLText(sCatalogueCode & "%")
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Catalogue", "Catalogue")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetCataloguePOSCode = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
#End Region

#Region "Save Catalogue"

    Public Function CatalogueSave(ByVal sCatalogueCode As String, ByVal sCatalogueName As String, ByVal sCategoryCode As String, _
            ByVal sGoldLoss As String, ByVal sRemarks As String, ByVal CatImage() As Byte, ByVal sImageName As String, ByVal dReOrderLevel As Integer) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            'sQry = "EXEC sp_Catalogue " & fncFormatSQLText(sCatalogueCode) & ","
            'sQry = sQry & fncFormatSQLText(sCatalogueName) & ","
            'sQry = sQry & fncFormatSQLText(sCategoryCode) & ","
            'sQry = sQry & fncFormatSQLText(sGoldLoss) & ","
            'sQry = sQry & fncFormatSQLText(sRemarks) & ","
            'sQry = sQry & CatImage & ","
            'sQry = sQry & fncFormatSQLText(sUserID)
            'SaveStatus = dm.ProjExecNonQry(sQry)

            CatalogueSave = False
            Dim Cmd As New SqlClient.SqlCommand("sp_Catalogue", gsConBatchPosting)
            Cmd.Transaction = gsTrans
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.Parameters.Add("@CatalogueCode", SqlDbType.VarChar)
            Cmd.Parameters("@CatalogueCode").Value = sCatalogueCode
            Cmd.Parameters.Add("@CatalogueName", SqlDbType.VarChar)
            Cmd.Parameters("@CatalogueName").Value = sCatalogueName
            Cmd.Parameters.Add("@CategoryCode", SqlDbType.VarChar)
            Cmd.Parameters("@CategoryCode").Value = sCategoryCode
            Cmd.Parameters.Add("@GoldLoss", SqlDbType.VarChar)
            Cmd.Parameters("@GoldLoss").Value = sGoldLoss
            Cmd.Parameters.Add("@Remarks", SqlDbType.VarChar)
            Cmd.Parameters("@Remarks").Value = sRemarks
            Cmd.Parameters.Add("@CatalogueImage", SqlDbType.Image)
            Cmd.Parameters("@CatalogueImage").Value = CatImage
            Cmd.Parameters.Add("@UserID", SqlDbType.VarChar)
            Cmd.Parameters("@UserID").Value = sUserID
            Cmd.Parameters.Add("@CatalogueImageName", SqlDbType.VarChar)
            Cmd.Parameters("@CatalogueImageName").Value = sImageName
            Cmd.Parameters.Add("@ReOrderLevel", SqlDbType.Int)
            Cmd.Parameters("@ReOrderLevel").Value = dReOrderLevel
            Cmd.ExecuteNonQuery()
            CatalogueSave = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            CatalogueSave = True
            sQry = Nothing
        End Try
    End Function
    Public Function ReOrderLevelMaster(ByVal sCategoryCode As String, ByVal sCatalogueCode As String, _
            ByVal dFromWeight As Double, ByVal dToWeight As Double, ByVal sSizeCode As String, _
            ByVal sSizeType As String, ByVal dReOrderlevel As Integer, ByVal sRemarks As String, _
            ByVal sLocationCode As String, ByVal sToSizeCode As String, ByVal sToSizeType As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_ReOrderLevelMaster " & fncFormatSQLText(sCategoryCode) & ","
            sQry = sQry & fncFormatSQLText(sCatalogueCode) & ","
            sQry = sQry & Val(dFromWeight) & ","
            sQry = sQry & Val(dToWeight) & ","
            sQry = sQry & Val(sSizeCode) & ","
            sQry = sQry & fncFormatSQLText(sSizeType) & ","
            sQry = sQry & Val(dReOrderlevel) & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & fncFormatSQLText(sLocationCode) & ","
            sQry = sQry & Val(sToSizeCode) & ","
            sQry = sQry & fncFormatSQLText(sToSizeType) & ","
            sQry = sQry & fncFormatSQLText(sUserID)
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            ReOrderLevelMaster = True
            sQry = Nothing
        End Try
    End Function
    Public Function GetReOrderMaster() As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select LocationCode,CategoryCode,CatalogueCode, Weight, Toweight,SizeCode, SizeType, ReOrderLevel,Remarks,ToSizeCode,ToSizeType from tblReOrderLevelMaster  "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Catalogue", "Catalogue")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetReOrderMaster = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function SizeSave(ByVal sCategoryCode As String, ByVal sSizeCode As String, ByVal sSizeType As String, _
           ByVal sRemarks As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_SizeSave " & fncFormatSQLText(sCategoryCode) & ","
            sQry = sQry & fncFormatSQLText(sSizeCode) & ","
            sQry = sQry & fncFormatSQLText(sSizeType) & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & fncFormatSQLText(sUserID)
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            SizeSave = SaveStatus
            sQry = Nothing
        End Try
    End Function

    Public Function fncGetSize(ByVal sConn As String, ByVal sDsName As String) As DataSet
        Dim DS As New DataSet
        Dim DM As DataManager
        Dim sQry As String
        fncGetSize = DS
        Try
            'sSql = "Select * from tblRating order by RatingCode"
            sQry = " select * from tblSize Order by CategoryCode,SizeCode "

            DS = DM.fncGetDatasetWithQuery(sConn, sDsName, sQry)
            fncGetSize = DS
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            DS = Nothing
        End Try
    End Function

    Public Function GetSize() As DataSet
        Dim DM As New DataManager
        '-----
        Dim ds As New DataSet
        Dim sSql As String
        sSql = "SELECT * FROM tblSize"
        ds = DM.getDataSetWithSqlAdapter(sSql, "getSize", "getSize")
        GetSize = ds
        ds = Nothing
        '-

        '        Dim sSelect As String
        '        Dim ORs As ADODB.Recordset

        '        On Error GoTo ErrHandler

        '        sSelect = "SELECT * FROM tblCompany"

        '        ORs = ExecuteQueryUsingConnectString(gsConn, sSelect)

        '        GetCompanyList = ORs

        'CleanUp:
        '        ORs = Nothing
        '        Exit Function

        'ErrHandler:
        '        ErrorSet()
        '        GetCompanyList = Nothing
        '        GoTo CleanUp
    End Function
#End Region

#Region "Delete Catalogue"
    Public Function CatalogueDelete(ByVal sCatalogueCode As String) As Boolean
        Dim sQry As String
        Dim DelStatus As Boolean
        Try
            sQry = "Delete From tblCatalogue Where Cataloguecode = " & fncFormatSQLText(sCatalogueCode)
            DelStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            DelStatus = False
        Finally
            CatalogueDelete = DelStatus
            sQry = Nothing
        End Try
    End Function

    Public Function ReOrderDelete(ByVal sCatalogueCode As String, ByVal sCategoryCode As String, ByVal dFromWeight As Decimal, ByVal dToWeight As Decimal, ByVal dSizeCode As String, ByVal sSizeType As String, ByVal sLocationCode As String, ByVal dToSizeCode As String, ByVal sToSizeType As String) As Boolean
        Dim sQry As String
        Dim DelStatus As Boolean
        Try
            sQry = "Delete From tblReOrderLevelMaster Where CategoryCode = " & fncFormatSQLText(sCategoryCode) & " and Cataloguecode = " & fncFormatSQLText(sCatalogueCode)
            sQry = sQry & " and Weight = " & Val(dFromWeight) & " and ToWeight = " & Val(dToWeight) & "  and SizeCode = " & Val(dSizeCode) & " and SizeType = " & fncFormatSQLText(sSizeType)
            sQry = sQry & " and LocationCode = " & fncFormatSQLText(sLocationCode) & " and ToSizeCode = " & Val(dToSizeCode) & " and ToSizeType = " & fncFormatSQLText(sToSizeType)
            DelStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            DelStatus = False
        Finally
            ReOrderDelete = DelStatus
            sQry = Nothing
        End Try
    End Function

    Public Function SizeDelete(ByVal sSizeCode As String, ByVal sCategoryCode As String) As Boolean
        Dim sQry As String
        Dim DelStatus As Boolean
        Try
            sQry = "Delete From tblSize Where CategoryCode = " & fncFormatSQLText(sCategoryCode) & " and Sizecode= " & fncFormatSQLText(sSizeCode)
            DelStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            DelStatus = False
        Finally
            SizeDelete = DelStatus
            sQry = Nothing
        End Try
    End Function

    Public Function GetSizewithCode(ByVal sSizeCode As String, ByVal sCategoryCode As String) As DataSet
        Dim sQry As String
        Dim DS As New DataSet
        Try

            sQry = " select * from tblSize where SizeCode =  " & fncFormatSQLText(sSizeCode)
            sQry = sQry & " and Categorycode= " & fncFormatSQLText(sCategoryCode)
            DS = dm.getDataSetWithSqlAdapter(sQry, "Size", "Size")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            GetSizewithCode = DS
            DS = Nothing
        End Try
    End Function


#End Region

#Region "Added by Murugan"

    Public Function GetCatalogueCode(ByVal sCatalogueCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select * from tblCatalogue a,tblcategory b,tblclass c where a.categorycode=b.categorycode and"
            sQry = sQry & " b.classcode=c.classcode and CatalogueCode = " & fncFormatSQLText(sCatalogueCode)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Catalogue", "Catalogue")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetCatalogueCode = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function DeleteSave(ByVal sJobsheetNo As String, ByVal sCatalogueCode As String, ByVal sUID As String) As Boolean

        Dim sQry As String
        Dim status As Boolean
        Try
            DeleteSave = False
            sQry = ""
            sQry = sQry & "EXEC sp_CatalogueDeleteLog " & fncFormatSQLText(sJobsheetNo) & ","
            sQry = sQry & fncFormatSQLText(sCatalogueCode) & ","
            sQry = sQry & fncFormatSQLText(sUID)
            status = dm.ProjExecNonQry(sQry)

            DeleteSave = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Public Function CatalogueDeleteFromJobSheet(ByVal sCatalogueCode As String, ByVal sJobsheetNo As String, _
                ByVal sUID As String) As Boolean
        Dim sQry As String
        Dim Status As Boolean
        Try
            CatalogueDeleteFromJobSheet = False
            sQry = ""
            sQry = sQry & "EXEC sp_CatalogueDelete " & fncFormatSQLText(sJobsheetNo) & ","
            sQry = sQry & fncFormatSQLText(sCatalogueCode) & ","
            sQry = sQry & fncFormatSQLText(sUID)
            Status = dm.ProjExecNonQry(sQry)

            CatalogueDeleteFromJobSheet = Status

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Function

    Public Function fncCheckCatalogueDelete1(ByVal sCatalogue As String, ByVal sJobsheetNo As String) As Boolean
        Dim sSql As String
        Dim Ds As DataSet
        Try
            sSql = "select CatalogueCode from tblPurchase WHERE CatalogueCode=" & fncFormatSQLText(sCatalogue) & _
                    " and JobSheetNo=" & fncFormatSQLText(sJobsheetNo)

            Ds = dm.getDataSetWithSqlAdapter(sSql, "Catalogue", "Catalogue")
            If Ds.Tables("Catalogue").Rows.Count > 0 Then
                fncCheckCatalogueDelete1 = True
            Else
                fncCheckCatalogueDelete1 = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            fncCheckCatalogueDelete1 = False
        End Try
    End Function
    Public Function fncCheckCatalogueDeleteBefore(ByVal sCatalogue As String, ByVal sJobsheetNo As String) As Boolean
        Dim sSql As String
        Dim Ds As DataSet
        Try
            sSql = "select CatalogueCode from tblJobSheetDetail WHERE CatalogueCode=" & fncFormatSQLText(sCatalogue) & _
                    " and JobSheetNo=" & fncFormatSQLText(sJobsheetNo)

            Ds = dm.getDataSetWithSqlAdapter(sSql, "Catalogue", "Catalogue")
            If Ds.Tables("Catalogue").Rows.Count > 0 Then
                fncCheckCatalogueDeleteBefore = True
            Else
                fncCheckCatalogueDeleteBefore = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
            fncCheckCatalogueDeleteBefore = False
        End Try
    End Function

    Public Function GetCatalogueForReports(ByVal CatalogueImageName As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select CatalogueCode from tblCatalogue  where CatalogueImageName =" & fncFormatSQLText(CatalogueImageName)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Catalogue", "Catalogue")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetCatalogueForReports = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function GetCatalogueImage(ByVal CatalogueCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select * from tblCatalogue  where CatalogueCode =" & fncFormatSQLText(CatalogueCode)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Catalogue", "Catalogue")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetCatalogueImage = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
#End Region

    

End Class
