Public Class clsSalesOrder
    Dim DM As New DataManager
    Public Function fncSalesOrderNo() As String
        Dim sQry As String
        Dim Ds As New DataSet
        Dim sSalesOrderNo As String
        Try
            sSalesOrderNo = ""
            sQry = "EXEC sp_GetSalesOrderNo "
            sQry = sQry & fncFormatSQLText(gthisLocation)
            Ds = DM.getDataSetWithSqlAdapter(sQry, "SalesOrdeNo", "SalesOrdeNo")
            If Ds.Tables("SalesOrdeNo").Rows.Count > 0 Then
                sSalesOrderNo = Trim(Ds.Tables("SalesOrdeNo").Rows(0)("NextSalesOrderNo") & "")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncSalesOrderNo = sSalesOrderNo
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function
    Public Function fncGetSalesOrderDetail(ByVal sConn As String, ByVal sDsName As String, _
                                     Optional ByVal sTranNo As String = "") As DataSet
        Dim DS As DataSet
        Dim sSql As String
        Try
            sSql = "Exec GetSalesOrderDetail "
            sSql = sSql & fncFormatSQLText(sTranNo)
            DS = fncGetDatasetWithQuery(sConn, sDsName, sSql)
            fncGetSalesOrderDetail = DS
            DS = Nothing
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            DS = Nothing
            fncGetSalesOrderDetail = Nothing
        Finally
            DS = Nothing
        End Try
    End Function
    Public Function UpdateSalesOrder( ByVal dsDetail As DataSet _
                        , ByVal pMode As String _
                        , Optional ByVal pError As String = "") As String
        Dim sCmd As String
        Dim iRow As Integer
        Dim dsTranNo As New DataSet
        Try
            pError = ""
            'sType = "N"
            

            OpenConnection()
            BeginTransaction()

            For iRow = 0 To dsDetail.Tables("Det").Rows.Count - 1
                sCmd = "Exec sp_SalesOrderDetailSave "

                sCmd = sCmd & fncFormatSQLText(Trim(dsDetail.Tables("Det").Rows(0).Item("SalesOrderNo" & "")))
                sCmd = sCmd & ","
                sCmd = sCmd & fncFormatSQLDate(dsDetail.Tables("Det").Rows(iRow).Item("SalesOrderDate" & ""))
                sCmd = sCmd & ","
                sCmd = sCmd & fncFormatSQLDate(dsDetail.Tables("Det").Rows(iRow).Item("DueDate" & ""))
                sCmd = sCmd & ","
                sCmd = sCmd & fncFormatSQLText(dsDetail.Tables("Det").Rows(iRow).Item("LocationCode" & ""))
                sCmd = sCmd & ","
                sCmd = sCmd & fncFormatSQLText(dsDetail.Tables("Det").Rows(iRow).Item("SupplierCode" & ""))
                sCmd = sCmd & ","
                sCmd = sCmd & fncFormatSQLText(dsDetail.Tables("Det").Rows(iRow).Item("CategoryCode" & ""))
                sCmd = sCmd & ","
                sCmd = sCmd & fncFormatSQLText(dsDetail.Tables("Det").Rows(iRow).Item("CatalogueCode" & ""))
                sCmd = sCmd & ","
                sCmd = sCmd & dsDetail.Tables("Det").Rows(iRow).Item("Weight" & "")
                sCmd = sCmd & ","
                sCmd = sCmd & dsDetail.Tables("Det").Rows(iRow).Item("ToWeight" & "")
                sCmd = sCmd & ","
                sCmd = sCmd & dsDetail.Tables("Det").Rows(iRow).Item("SizeCode" & "")
                sCmd = sCmd & ","
                sCmd = sCmd & fncFormatSQLText(dsDetail.Tables("Det").Rows(iRow).Item("SizeType" & ""))
                sCmd = sCmd & ","
                sCmd = sCmd & dsDetail.Tables("Det").Rows(iRow).Item("ReOrderLevel" & "")
                sCmd = sCmd & ","
                sCmd = sCmd & dsDetail.Tables("Det").Rows(iRow).Item("SalesOrderQty" & "")
                sCmd = sCmd & ","
                sCmd = sCmd & fncFormatSQLText(dsDetail.Tables("Det").Rows(iRow).Item("Remarks" & ""))
                sCmd = sCmd & ","
                sCmd = sCmd & fncFormatSQLText(dsDetail.Tables("Det").Rows(iRow).Item("CreateUser" & ""))
                sCmd = sCmd & ","
                sCmd = sCmd & fncFormatSQLText(pMode) ' I - Insert, U - Update, D - Delete, O - Unknown Option
                'SHabeer 20111111

                If Not fncExecuteNonQuery(sCmd, gsConnection, gsTrans) Then
                    pError = "Error In Detail"
                    UpdateSalesOrder = ""
                    RollBackTransaction()
                    CloseConnection()
                    Exit Function
                End If
            Next iRow


            CommitTransaction()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            RollBackTransaction()
            UpdateSalesOrder = ""
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function fncUpdateNextSalesOrderNo() As Boolean
        Dim sQry As String
        Dim bStatus As Boolean
        Try
            sQry = " Update tblCompany Set NextSalesOrderNo = NextSalesOrderNo + 1  Where CompanyId = '1'"
            bStatus = fncExecNonQryTransfer(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateNextSalesOrderNo = bStatus
            sQry = Nothing
        End Try
    End Function

    Public Function fncExecNonQryTransfer(ByVal Query As String) As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            Dim Cmd As New SqlClient.SqlCommand(Query, gsTransferCon)
            Cmd.Transaction = gsTransferTrans
            Cmd.ExecuteNonQuery()
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncExecNonQryTransfer = bStatus
        End Try
    End Function

    Public Sub subOpenTransaferConnection()
        gsTransferCon.ConnectionString = gsCon
        gsTransferCon.Open()
    End Sub

    Public Sub subBeginTransaferTransaction()
        gsTransferTrans = gsTransferCon.BeginTransaction
    End Sub

    Public Sub subCommitTransaferTransaction()
        gsTransferTrans.Commit()
    End Sub

    Public Sub subRollBackTransaferTransaction()
        gsTransferTrans.Rollback()
    End Sub

    Public Sub subCloseTransaferConnection()
        gsTransferCon.Close()
    End Sub
    Public Function SalesOrderDelete(ByVal sSalesOrderNo As String) As Boolean
        Dim sExec As String
        Try
            SalesOrderDelete = False

            sExec = "INSERT into tblSalesOrderDetailDelete" & vbCrLf
            sExec = sExec & "Select * FROM tblSalesOrderDetail  WHERE SalesOrderNo =" & fncFormatSQLText(sSalesOrderNo)
            DM.ProjExecNonQry(sExec)

            sExec = "DELETE FROM tblSalesOrderDetail WHERE SalesOrderNo =" & fncFormatSQLText(sSalesOrderNo)
            DM.ProjExecNonQry(sExec)
            SalesOrderDelete = True
            Exit Function
        Catch ex As Exception
            SalesOrderDelete = False
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Function
    Public Function SalesOrderHeaderInsert(ByVal sSalesOrderNo As String) As Boolean
        Dim sExec As String
        Try
            SalesOrderHeaderInsert = False

            sExec = "INSERT into tblSalesOrderHeader" & vbCrLf
            sExec = sExec & "select SalesOrderNo,SalesOrderDate,DueDate,LocationCode,SupplierCode,CategoryCode,Cataloguecode,sum(Weight),Sum(SalesOrderQty),0,CreateUser,getdate(),ModifyUser,getdate() from tblSalesOrderDetail WHERE SalesOrderNo =" & fncFormatSQLText(sSalesOrderNo) & " group by SalesOrderNo,SalesOrderDate,DueDate,LocationCode,SupplierCode,CategoryCode,Cataloguecode,CreateUser,ModifyUser"
            DM.ProjExecNonQry(sExec)

            SalesOrderHeaderInsert = True
            Exit Function
        Catch ex As Exception
            SalesOrderHeaderInsert = False
            MsgBox(Err.Description)
        Finally
            Err.Clear()
        End Try
    End Function
End Class
