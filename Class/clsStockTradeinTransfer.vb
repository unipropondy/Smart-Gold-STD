Public Class clsStockTradeinTransfer
    Dim DM As New DataManager
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

    Public Sub subOpenTransaferConnectionHQ()
        gsTransferConHQ.ConnectionString = gsCon
        gsTransferConHQ.Open()
    End Sub

    Public Sub subBeginTransaferTransactionHQ()
        gsTransferTransHQ = gsTransferConHQ.BeginTransaction
    End Sub

    Public Sub subCommitTransaferTransactionHQ()
        gsTransferTransHQ.Commit()
    End Sub

    Public Sub subRollBackTransaferTransactionHQ()
        gsTransferTransHQ.Rollback()
    End Sub

    Public Sub subCloseTransaferConnectionHQ()
        gsTransferConHQ.Close()
    End Sub

    Public Function fncExecNonQryTransferHQ(ByVal Query As String) As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            Dim Cmd As New SqlClient.SqlCommand(Query, gsTransferConHQ)
            Cmd.Transaction = gsTransferTransHQ
            Cmd.ExecuteNonQuery()
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncExecNonQryTransferHQ = bStatus
        End Try
    End Function

    Public Function fncGetTradeInTransferHeader(ByVal sConn As String, ByVal sDsName As String, _
                                         Optional ByVal sTranNo As String = "") As DataSet
        Dim DS As DataSet
        Dim sSql As String
        Try
            sSql = "Exec GetTradeInTransferHeader "
            sSql = sSql & fncFormatSQLText(sTranNo)
            DS = fncGetDatasetWithQuery(sConn, sDsName, sSql)
            fncGetTradeInTransferHeader = DS
            DS = Nothing
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            DS = Nothing
            fncGetTradeInTransferHeader = Nothing
        Finally
            DS = Nothing
        End Try
    End Function
    Public Function fncGetTradeInTransferDetail(ByVal sConn As String, ByVal sDsName As String, _
                                     Optional ByVal sTranNo As String = "") As DataSet
        Dim DS As DataSet
        Dim sSql As String
        Try
            sSql = "Exec GetTradeInTransferDetail "
            sSql = sSql & fncFormatSQLText(sTranNo)
            DS = fncGetDatasetWithQuery(sConn, sDsName, sSql)
            fncGetTradeInTransferDetail = DS
            DS = Nothing
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            DS = Nothing
            fncGetTradeInTransferDetail = Nothing
        Finally
            DS = Nothing
        End Try
    End Function
    Public Function fncGetTradeInTransferNo() As String
        Dim sQry As String
        Dim Ds As New DataSet
        Dim sTransferNo As String
        Try
            sTransferNo = ""
            sQry = "EXEC sp_GetTradeInTransferNo "
            sQry = sQry & fncFormatSQLText(gthisLocation)
            Ds = DM.getDataSetWithSqlAdapter(sQry, "TransferNo", "TransferNo")
            If Ds.Tables("TransferNo").Rows.Count > 0 Then
                sTransferNo = Trim(Ds.Tables("TransferNo").Rows(0)("NextTransferNo") & "")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncGetTradeInTransferNo = sTransferNo
            Ds = Nothing
            sQry = Nothing
        End Try
    End Function

    Public Function fncInventoryMovementSave(ByVal sLocationCode As String, ByVal sInventorycode As String, ByVal sDescription As String, ByVal sStatus As String, ByVal sRefNumber As String, _
                                        ByVal sRefDate As String, ByVal sQty As Integer, ByVal sWeight As Decimal, ByVal sSize As Decimal, ByVal sPMC As Decimal, ByVal sSMC As Decimal, _
                                       ByVal sCostPerGram As String, ByVal sTotalCost As String, _
                                      ByVal sSupplierCode As String, ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim bSaveStatus As Boolean
        Try


            sQry = ""
            sQry = sQry & "EXEC Sp_UpdateNewInventoryMovement " & fncFormatSQLText(sLocationCode) & ","
            sQry = sQry & fncFormatSQLText(sInventorycode) & ","
            sQry = sQry & fncFormatSQLText(sDescription) & ","
            sQry = sQry & fncFormatSQLText(sStatus) & ","
            sQry = sQry & fncFormatSQLText(sRefNumber) & ","
            sQry = sQry & fncFormatSQLDate(sRefDate) & ","
            sQry = sQry & Val(sQty) & ","
            sQry = sQry & Val(sWeight) & ","
            sQry = sQry & Val(sSize) & ","
            sQry = sQry & Val(sPMC) & ","
            sQry = sQry & Val(sSMC) & ","
            sQry = sQry & Val(sCostPerGram) & ","
            sQry = sQry & Val(sTotalCost) & ","
            sQry = sQry & fncFormatSQLText(sSupplierCode) & ","
            sQry = sQry & fncFormatSQLText(sUserId)
            bSaveStatus = fncExecNonQryTransfer(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        Finally
            fncInventoryMovementSave = bSaveStatus
        End Try
    End Function
    Public Function fncUpdateNextTradeInTransferNo() As Boolean
        Dim sQry As String
        Dim bStatus As Boolean
        Try
            sQry = " Update tblCompany Set NextTradeInTransferNo = NextTradeInTransferNo + 1  Where CompanyId = '1'"
            bStatus = fncExecNonQryTransfer(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateNextTradeInTransferNo = bStatus
            sQry = Nothing
        End Try
    End Function

    Public Function fncUpdateNextInvoiceNo() As Boolean
        Dim bStatus As Boolean
        Dim sQry As String
        Dim objsales As New ClsSales
        Try
            sQry = " update tblcompany set nextinvoice = nextinvoice + 1  "
            bStatus = fncExecNonQryTransfer(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateNextInvoiceNo = bStatus
            sQry = Nothing
        End Try
    End Function
    Public Function fncUpdateNextTransferInvoiceNo() As Boolean
        Dim bStatus As Boolean
        Dim sQry As String
        Dim objsales As New ClsSales
        Try
            sQry = " update tblcompany set nextTransferinvoiceNo = nextTransferinvoiceNo + 1  "
            bStatus = fncExecNonQryTransfer(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateNextTransferInvoiceNo = bStatus
            sQry = Nothing
        End Try

    End Function

    Public Function fncupdateInventoryOnTradeInTransfer(ByVal sInventoryCode As String, _
       ByVal dQtyOnhand As Decimal, ByVal dWeight As Decimal, ByVal sUserId As String) As Boolean
        Dim sQry As String
        Dim bSaveStatus As Boolean
        Try
            sQry = "EXEC sp_UpdateInventoryTradeInTransfer "
            sQry = sQry & fncFormatSQLText(gthisLocation) & ", "
            sQry = sQry & fncFormatSQLText(sInventoryCode) & ", "
            sQry = sQry & Val(dWeight) & ", "
            sQry = sQry & Val(dQtyOnhand) & ", "
            sQry = sQry & fncFormatSQLText(sUserId)

            bSaveStatus = fncExecNonQryTransfer(sQry)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncupdateInventoryOnTradeInTransfer = bSaveStatus
        End Try
    End Function
    Public Function UpdateTradeInTransfer(ByVal dsHeader As DataSet _
                        , ByVal dsDetail As DataSet _
                        , ByVal pMode As String _
                        , Optional ByVal pError As String = "") As String
        Dim sCmd As String
        Dim iRow As Integer
        Dim dsTranNo As New DataSet
        Try
            pError = ""
            'sType = "N"
            sCmd = "Exec sp_TradeInTransferHeaderSave "
            If pMode <> "I" Then
                sCmd = sCmd & fncFormatSQLText(Trim(dsHeader.Tables("Hdr").Rows(0).Item("TransferNo" & "")))
            Else
                sCmd = sCmd & fncFormatSQLText(Trim(dsHeader.Tables("Hdr").Rows(0).Item("TransferNo" & "")))
            End If
            sCmd = sCmd & ","
            sCmd = sCmd & fncFormatSQLDate(Trim(dsHeader.Tables("Hdr").Rows(0).Item("TransferDate" & "")))
            sCmd = sCmd & ","
            sCmd = sCmd & fncFormatSQLText(Trim(dsHeader.Tables("Hdr").Rows(0).Item("FromLocation" & "")))
            sCmd = sCmd & ","
            sCmd = sCmd & fncFormatSQLText(Trim(dsHeader.Tables("Hdr").Rows(0).Item("ToLocation" & "")))
            sCmd = sCmd & ","
            sCmd = sCmd & dsHeader.Tables("Hdr").Rows(iRow).Item("TotalWeight" & "")
            sCmd = sCmd & ","
            sCmd = sCmd & dsHeader.Tables("Hdr").Rows(iRow).Item("TotalPCS" & "")
            sCmd = sCmd & ","
            sCmd = sCmd & dsHeader.Tables("Hdr").Rows(iRow).Item("TotalCost" & "")
            sCmd = sCmd & ","
            sCmd = sCmd & dsHeader.Tables("Hdr").Rows(iRow).Item("TotalPMC" & "")
            sCmd = sCmd & ","
            sCmd = sCmd & dsHeader.Tables("Hdr").Rows(iRow).Item("TotalSMC" & "")
            sCmd = sCmd & ","
            sCmd = sCmd & dsHeader.Tables("Hdr").Rows(iRow).Item("FromLocStatus" & "")
            sCmd = sCmd & ","
            sCmd = sCmd & dsHeader.Tables("Hdr").Rows(iRow).Item("ToLocStatus" & "")
            sCmd = sCmd & ","
            sCmd = sCmd & fncFormatSQLText(Trim(dsHeader.Tables("Hdr").Rows(0).Item("CreateUser" & "")))
            sCmd = sCmd & ","
            sCmd = sCmd & fncFormatSQLText(Trim(dsHeader.Tables("Hdr").Rows(0).Item("ModifyUser" & "")))
            sCmd = sCmd & ","
            sCmd = sCmd & fncFormatSQLText(Trim(dsHeader.Tables("Hdr").Rows(0).Item("Remarks" & "")))
            sCmd = sCmd & ","
            sCmd = sCmd & fncFormatSQLText(Trim(dsHeader.Tables("Hdr").Rows(0).Item("SalesManCode" & "")))


            OpenConnection()
            BeginTransaction()

            dsTranNo = fncGetDatasetWithNonQuery(gsConnection, gsTrans, "TranNo", sCmd)
            If dsTranNo.Tables("TranNo").Rows.Count > 0 Then
                UpdateTradeInTransfer = dsTranNo.Tables("TranNo").Rows(0).Item("TransferNo" & "").ToString.Trim
            Else
                RollBackTransaction()
                CloseConnection()
                pError = "Error In Header"
                UpdateTradeInTransfer = ""
                Exit Function
            End If

            For iRow = 0 To dsDetail.Tables("Det").Rows.Count - 1
                sCmd = "Exec sp_TradeInTransferDetailSave "

                If pMode = "I" Then
                    sCmd = sCmd & fncFormatSQLText(Trim(UpdateTradeInTransfer))
                Else
                    sCmd = sCmd & fncFormatSQLText(Trim(dsHeader.Tables("Hdr").Rows(0).Item("TransferNo" & "")))
                End If
                sCmd = sCmd & ","
                sCmd = sCmd & fncFormatSQLText(dsDetail.Tables("Det").Rows(iRow).Item("InventoryCode" & ""))
                sCmd = sCmd & ","
                sCmd = sCmd & fncFormatSQLText(dsDetail.Tables("Det").Rows(iRow).Item("ClassCode" & ""))
                sCmd = sCmd & ","
                sCmd = sCmd & fncFormatSQLText(dsDetail.Tables("Det").Rows(iRow).Item("CategoryCode" & ""))
                sCmd = sCmd & ","
                sCmd = sCmd & fncFormatSQLText(dsDetail.Tables("Det").Rows(iRow).Item("CatalogueCode" & ""))
                sCmd = sCmd & ","
                sCmd = sCmd & fncFormatSQLText(dsDetail.Tables("Det").Rows(iRow).Item("Description" & ""))
                sCmd = sCmd & ","
                sCmd = sCmd & dsDetail.Tables("Det").Rows(iRow).Item("QtyonHand" & "")
                sCmd = sCmd & ","
                sCmd = sCmd & dsDetail.Tables("Det").Rows(iRow).Item("Weight" & "")
                sCmd = sCmd & ","
                sCmd = sCmd & dsDetail.Tables("Det").Rows(iRow).Item("SizeCode" & "")
                sCmd = sCmd & ","
                sCmd = sCmd & dsDetail.Tables("Det").Rows(iRow).Item("CostPerGram" & "")
                sCmd = sCmd & ","
                sCmd = sCmd & dsDetail.Tables("Det").Rows(iRow).Item("PMC" & "")
                sCmd = sCmd & ","
                sCmd = sCmd & dsDetail.Tables("Det").Rows(iRow).Item("SMC" & "")
                sCmd = sCmd & ","
                sCmd = sCmd & dsDetail.Tables("Det").Rows(iRow).Item("TotalCost" & "")
                sCmd = sCmd & ","
                sCmd = sCmd & (IIf(dsDetail.Tables("Det").Rows(iRow).Item("TransferTag") & "" = True, 1, 0))
                sCmd = sCmd & ","
                sCmd = sCmd & fncFormatSQLText(dsDetail.Tables("Det").Rows(iRow).Item("CreateUser" & ""))
                sCmd = sCmd & ","
                sCmd = sCmd & fncFormatSQLText(dsDetail.Tables("Det").Rows(iRow).Item("ModifyUser" & ""))
                sCmd = sCmd & ","
                sCmd = sCmd & fncFormatSQLText(pMode) ' I - Insert, U - Update, D - Delete, O - Unknown Option
                'SHabeer 20111111

                If Not fncExecuteNonQuery(sCmd, gsConnection, gsTrans) Then
                    pError = "Error In Detail"
                    UpdateTradeInTransfer = ""
                    RollBackTransaction()
                    CloseConnection()
                    Exit Function
                End If
            Next iRow


            CommitTransaction()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            RollBackTransaction()
            UpdateTradeInTransfer = ""
        Finally
            CloseConnection()
        End Try
    End Function
End Class
