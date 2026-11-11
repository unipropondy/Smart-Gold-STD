Public Class clsMember
    Dim dm As New DataManager

    Public Function MemberTypeSave(ByVal sMemberType As String, ByVal sMemberTypeDescription As String, ByVal dMemberTypeDiscount As Double, _
                                 ByVal sActive As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_MemberType " & fncFormatSQLText(sMemberType) & ","
            sQry = sQry & fncFormatSQLText(sMemberTypeDescription) & ","
            sQry = sQry & Val(dMemberTypeDiscount) & ","
            sQry = sQry & fncFormatSQLText(sActive) & ","
            'sQry = sQry & Val(sActive) & ","
            sQry = sQry & fncFormatSQLText(sUserID)

            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            MemberTypeSave = SaveStatus
            sQry = Nothing
        End Try
    End Function

    Public Function GetMemberType() As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select * from tblMemberType order by membertype asc "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "MemberType", "MemberType")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetMemberType = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function SubGetMemberType(ByVal sMemberType As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select * from tblMemberType where membertype =  " & fncFormatSQLText(Trim(sMemberType))
            Ds = dm.getDataSetWithSqlAdapter(sQry, "MemberType", "MemberType")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            SubGetMemberType = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function SubGetMemberLocation(ByVal sLocationcode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select * from tblLocation where LocationCode =  " & fncFormatSQLText(Trim(sLocationcode))
            Ds = dm.getDataSetWithSqlAdapter(sQry, "MemberType", "MemberType")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            SubGetMemberLocation = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function DeleteMemberType(ByVal sMemberType As String) As Boolean
        Dim sQry As String
        Dim DelStatus As Boolean
        Try
            sQry = " Delete from tblMemberType where MemberType = " & fncFormatSQLText(sMemberType)
            DelStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            DelStatus = False
        Finally
            DeleteMemberType = DelStatus
            sQry = Nothing
        End Try
    End Function
    Public Function MemberSave(ByVal sMemberCode As String, ByVal sNricNo As String, ByVal sNricType As String, ByVal sMemberName As String, _
                            ByVal sAddress1 As String, ByVal sAddress2 As String, ByVal sAddress3 As String, _
                            ByVal sCountry As String, ByVal sZipCode As String, ByVal sEmail As String, _
                            ByVal sResPhone As String, ByVal sOffPhone As String, ByVal sMobile As String, _
                            ByVal sJoinDate As String, ByVal sExpiryDate As String, ByVal sRenewalDate As String, _
                            ByVal dTotalsales As Double, ByVal dTotalDiscount As Double, _
                            ByVal dTotalRewardPoint As Double, ByVal dTotalUsagePoint As Double, _
                            ByVal sRewardExpiryDate As String, ByVal sActive As String, _
                            ByVal sMemberType As String, ByVal dMemberDiscount As Double, _
                            ByVal sDateOfBirth As String, _
                            ByVal sRemarks As String, ByVal sLocationCode As String) As Boolean

        Dim sQry As String
        Dim SaveStatus As Boolean
        Try

            sQry = "EXEC sp_Member " & fncFormatSQLText(sMemberCode) & ","
            sQry = sQry & fncFormatSQLText(sNricNo) & ","
            sQry = sQry & fncFormatSQLText(sNricType) & ","
            sQry = sQry & fncFormatSQLText(sMemberName) & ","
            sQry = sQry & fncFormatSQLText(sAddress1) & ","
            sQry = sQry & fncFormatSQLText(sAddress2) & ","
            sQry = sQry & fncFormatSQLText(sAddress3) & ","
            sQry = sQry & fncFormatSQLText(sCountry) & ","
            sQry = sQry & fncFormatSQLText(sZipCode) & ","
            sQry = sQry & fncFormatSQLText(sEmail) & ","
            sQry = sQry & fncFormatSQLText(sResPhone) & ","
            sQry = sQry & fncFormatSQLText(sOffPhone) & ","
            sQry = sQry & fncFormatSQLText(sMobile) & ","
            sQry = sQry & fncFormatSQLDate(sJoinDate) & ","
            sQry = sQry & fncFormatSQLDate(sExpiryDate) & ","
            sQry = sQry & fncFormatSQLDate(sRenewalDate) & ","
            sQry = sQry & Val(dTotalsales) & ","
            sQry = sQry & Val(dTotalDiscount) & ","
            sQry = sQry & Val(dTotalRewardPoint) & ","
            sQry = sQry & Val(dTotalUsagePoint) & ","
            sQry = sQry & fncFormatSQLDate(sRewardExpiryDate) & ","
            sQry = sQry & fncFormatSQLText(sActive) & ","
            sQry = sQry & fncFormatSQLText(sMemberType) & ","
            sQry = sQry & Val(dMemberDiscount) & ","
            sQry = sQry & fncFormatSQLDate(sDateOfBirth) & ","
            sQry = sQry & fncFormatSQLText(sRemarks) & ","
            sQry = sQry & fncFormatSQLText(sLocationCode) & ","
            sQry = sQry & fncFormatSQLText(sUserID)
            SaveStatus = dm.ProjExecNonQry(sQry)


        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            MemberSave = SaveStatus
            sQry = Nothing
        End Try
    End Function
    Public Function GetMemberwithCode(ByVal sMembercode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select * from tblMember where Membercode=" & fncFormatSQLText(sMembercode)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Member", "Member")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetMemberwithCode = Ds
            Ds = Nothing
            sQry = Nothing
        End Try

    End Function
    Public Function GetSalesByMember(ByVal sMemberCode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "Select InvoiceNo, InvoiceDate, TotalWeight, TotalPCS, NetTotal from tblSalesHeader " & _
                                   "where MemberCode = " & fncFormatSQLText(sMemberCode) & " Order By InvoiceDate Desc"
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Member", "Member")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetSalesByMember = Ds
            Ds = Nothing
            sQry = Nothing
        End Try

    End Function
    Public Function GetSalesDetailByInvoiceNo(ByVal sInvoiceNo As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = "Select InventoryCode, Description, Weight, PCS, NetAmount From tblSalesDetail where InvoiceNo = " & _
                    fncFormatSQLText(sInvoiceNo)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Member", "Member")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetSalesDetailByInvoiceNo = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function DeleteMember(ByVal sMemberCode As String) As Boolean
        Dim sQry As String
        Dim DelStatus As Boolean
        Try
            sQry = " Delete from tblMember where Membercode = " & fncFormatSQLText(sMemberCode)
            DelStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            DelStatus = False
        Finally
            DeleteMember = DelStatus
            sQry = Nothing
        End Try
    End Function
    Public Function GetMemberTypewithCode(ByVal sMemberType As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select * from tblMemberType where MemberType=" & fncFormatSQLText(sMemberType)
            Ds = dm.getDataSetWithSqlAdapter(sQry, "MemberType", "MemberType")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetMemberTypewithCode = Ds
            Ds = Nothing
            sQry = Nothing
        End Try

    End Function
    Public Function NricTypeSave(ByVal sNricTypeCode As String, ByVal sNricDescription As String, ByVal sActive As String) As Boolean
        Dim sQry As String
        Dim SaveStatus As Boolean
        Try
            sQry = "EXEC sp_NricType " & fncFormatSQLText(sNricTypeCode) & ","
            sQry = sQry & fncFormatSQLText(sNricDescription) & ","
            sQry = sQry & fncFormatSQLText(sActive) & ","
            sQry = sQry & fncFormatSQLText(sUserID)
            SaveStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            SaveStatus = False
        Finally
            NricTypeSave = SaveStatus
            sQry = Nothing
        End Try
    End Function
    Public Function GetNricType() As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select * from tblNricType order by NricTypeCode asc "
            Ds = dm.getDataSetWithSqlAdapter(sQry, "NricType", "NricType")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            GetNricType = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function DeleteNricType(ByVal sNricTypeCode As String) As Boolean
        Dim sQry As String
        Dim DelStatus As Boolean
        Try
            sQry = " Delete from tblNricType where NricTypeCode = " & fncFormatSQLText(sNricTypeCode)
            DelStatus = dm.ProjExecNonQry(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            DelStatus = False
        Finally
            DeleteNricType = DelStatus
            sQry = Nothing
        End Try
    End Function
    Public Function SubGetMemberCode(ByVal sMembercode As String) As DataSet
        Dim Ds As New DataSet
        Dim sQry As String
        Try
            sQry = " select * from tblMember where Membercode =  " & fncFormatSQLText(Trim(sMembercode))
            Ds = dm.getDataSetWithSqlAdapter(sQry, "Member", "Member")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Ds = Nothing
        Finally
            SubGetMemberCode = Ds
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
End Class