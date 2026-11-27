Public Class frmDayEndtoHQ
    Dim DM As New DataManager
    Dim sStockUpdateDate As String
    Dim sInvoiceUpdateDate As String
    Dim sSalesReturnNo As String
    Dim sOrderNo As String
    Dim sRepairNo As String
    Dim sLastSalesReturnNo As String
    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        Dim objEodToHq As New ClsEOD
        Dim sLDate, sDate, SEODDate As String
        Cursor.Current = Cursors.WaitCursor
        Try
            If fncValidateDate() Then
                sLDate = Trim(txtLYYYY.Text) + Trim(txtLMM.Text) + Trim(txtLDD.Text)
                sDate = Trim(txtYYYY.Text) + Trim(txtMM.Text) + Trim(txtDD.Text)
                SEODDate = txtDD.Text.Trim & "/" & txtMM.Text.Trim & "/" & txtYYYY.Text.Trim
                objEodToHq.subOpenEODConnectionHQ()
                objEodToHq.subBeginEODTransactionHQ()
                Try
                    ''' Hide Durai for some Reasons on 20/02/2012
                    'Print Day End Report
                    '''subPrintReports(SEODDate)

                    ' Update Inventory Stock
                    If fncUpdateInventoryDetails(sLDate, sDate) Then
                        '
                    Else
                        '
                    End If

                    'Update Sales Information

                    If fncUpdateSalesDetails(sLDate, sDate) Then
                        chkSalesDetail.Checked = True
                    Else
                        chkSalesDetail.Checked = False
                    End If
                  

            'Update Sales Return Information

                    If fncUpdateSalesReturnDetails(sLDate, sDate) Then
                        chkSalesReturn.Checked = True
                    Else
                        chkSalesReturn.Checked = False
                    End If

                    If fncUpdateOrderDetails(sLDate, sDate) Then
                        chkOrderEntry.Checked = True
                    Else
                        chkOrderEntry.Checked = False
                    End If


                    If fncUpdateRepairDetails(sLDate, sDate) Then
                        chkRepairEntry.Checked = True
                    Else
                        chkRepairEntry.Checked = False
                    End If

                    If fncUpdateOutletMovement(sLDate, sDate) Then
                        chkOrderEntry.Checked = True
                    Else
                        chkOrderEntry.Checked = False
                    End If

                    If fncUpdateOutletUserLog(sLDate, sDate) Then
                        chkOrderEntry.Checked = True
                    Else
                        chkOrderEntry.Checked = False
                    End If


                    If fncUpdateTerminalDetails(sLDate, sDate) Then
                        chkSalesDetail.Checked = True
                    Else
                        chkSalesDetail.Checked = False
                    End If
                    'If fncUpdateTradeInStockDetails(sLDate, sDate) Then
                    '    chkTradeIn.Checked = True
                    'Else
                    '    chkTradeIn.Checked = False
                    'End If

            ''If picSalesDetail.Visible = False Or picInventoryDetail.Visible = False Or picSalesReturnDetail.Visible = False Or picPawnTicketDetail.Visible = False Or picPurchaseCustDetail.Visible = False Or picPurchaseCustDetailDelete.Visible = False Then
            ''    objEodToHq.subRollBackEODTransactionHQ()
            ''    Exit Sub
            ''End If
            ''FrmSignOn''
            ''fncCaputureMEStockinHQ()
            ''fncUpdateMEStockinHQ(sLDate, sDate)
            ''UpdateHq''
            'fncUpdateMonthlyStockAuto()
            objEodToHq.subCommitEODTransactionHQ()
            If objEodToHq.fncUpdateLastEODDateToHQ() Then
                MessageBox.Show("EOD to HQ completed successfully.")
            End If
                    Me.Dispose()
                    Dim objUpdateHQ As New frmUpdatetoHQ
                    objUpdateHQ.Show()
                    objUpdateHQ = Nothing
        Catch ex As Exception
            objEodToHq.subRollBackEODTransactionHQ()
            MessageBox.Show(ex.Message)
        Finally
            objEodToHq.subCloseEODConnectionHQ()
        End Try
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            objEodToHq = Nothing
            Cursor.Current = Cursors.Default
        End Try
    End Sub
    Private Function fncUpdateInventoryDetails(ByVal sLDate, ByVal sDate) As Boolean
        Dim sSelect, sInventoryCodes As String
        Dim dsInvDetail As New DataSet
        Dim objEodToHQ As New ClsEOD
        Dim bStatus As Boolean
        Try
            getLastStockUpdateDate()
            sLDate = sStockUpdateDate
            If fncDatevalidation(sLDate, sDate) Then
                'sSelect = "Select * From tblInventory Where Convert(char(8), ModifyDate, 112) >= " & fncFormatSQLText(sLDate)
                'sSelect = "Select * from tblInventoryStock Where InventoryCode in (Select InventoryCode From tblInventory Where Convert(char(8), ModifyDate, 112) >= " & fncFormatSQLText(sLDate) & ")"
                'dsInvDetail = DM.getDataSetWithSqlAdapter(sSelect, "Inv", "Inv")
                'If dsInvDetail.Tables("Inv").Rows.Count > 0 Then
                '    bStatus = objEodToHQ.fncUpdateInventoryStockToHQ(dsInvDetail)
                'End If
                sSelect = "Select * from tblInventory Where  Locationcode=" & fncFormatSQLText(gthisLocation) & "  and Convert(char(8), ModifyDate, 112) >= " & fncFormatSQLText(sLDate) & vbCrLf
                sSelect &= "Select * from tblInventoryStock Where InventoryCode in (Select InventoryCode From tblInventory Where Locationcode=" & fncFormatSQLText(gthisLocation) & "  and Convert(char(8), ModifyDate, 112) >= " & fncFormatSQLText(sLDate) & ") And locationcode = " & fncFormatSQLText(gthisLocation) & vbCrLf
                ' sSelect = "Select * from tblInventoryStock Where Locationcode=" & fncFormatSQLText(gthisLocation) & "  and Convert(char(8), ModifyDate, 112) >= " & fncFormatSQLText(sLDate)
                dsInvDetail = DM.getDataSetWithSqlAdapter(sSelect, "Inv", "Inv")
                If dsInvDetail.Tables(0).Rows.Count > 0 Then
                    bStatus = objEodToHQ.fncUpdateInventoryStockToHQ(dsInvDetail)
                Else
                    bStatus = True
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncUpdateInventoryDetails = bStatus
        End Try
    End Function
    Public Function fncDatevalidation(ByVal sDate1 As String, ByVal sDate2 As String) As Boolean
        Dim sLDate, sDate As String
        sLDate = sDate1.Substring(6, 2) & "/" & sDate1.Substring(4, 2) & "/" & sDate1.Substring(0, 4)
        sDate = sDate2.Substring(6, 2) & "/" & sDate2.Substring(4, 2) & "/" & sDate2.Substring(0, 4)
        If Not IsDate(sLDate) Or Len(Trim(txtLYYYY.Text.Trim)) <> 4 Then
            MsgBox("Enter valid From date.", MsgBoxStyle.Critical)
            If Not txtLDD.ReadOnly Then txtLDD.Focus()
            Exit Function
        ElseIf Not IsDate(sDate) Or Len(Trim(txtYYYY.Text.Trim)) <> 4 Then
            MsgBox("Enter valid To date.", MsgBoxStyle.Critical)
            If Not txtDD.ReadOnly Then txtDD.Focus()
            Exit Function
        Else
            fncDatevalidation = True
        End If
    End Function
    Private Sub getLastStockUpdateDate()
        Dim sSql As String
        Dim Dr As DataRow()
        Try
            txtDD.Text = Format(ServerDate, "dd")
            txtMM.Text = Format(ServerDate, "MM")
            txtYYYY.Text = Format(ServerDate, "yyyy")
            sSql = "Select convert(char(8), LastStockUpdateDate, 112) LastStockUpdateDate From tblOnlineLog"
            Dr = DM.RowReturnProjExecNonQryWithDataRow(sSql)
            If Dr.Length > 0 Then
                If IsDBNull(Dr(0)("LastStockUpdateDate")) = False Then
                    sStockUpdateDate = Dr(0)("LastStockUpdateDate")
                Else
                    sStockUpdateDate = Format(ServerDate, "yyyyMMdd")
                End If
            Else
                sStockUpdateDate = Format(ServerDate, "yyyyMMdd")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Function fncValidateDate() As Boolean
        Dim bStatus As Boolean
        Dim sPrevEODDate, sEODDate As String
        Dim dsTD As New DataSet
        Try
            bStatus = False
            If Not IsDate(txtDD.Text.Trim & "/" & txtMM.Text.Trim & "/" & txtYYYY.Text.Trim) Then
                MessageBox.Show("Enter Valid EOD Date", "GoldPOS")
                txtDD.Focus()
                Exit Function
            End If
            sPrevEODDate = txtLDD.Text.Trim & "/" & txtLMM.Text.Trim & "/" & txtLYYYY.Text.Trim
            sEODDate = txtDD.Text.Trim & "/" & txtMM.Text.Trim & "/" & txtYYYY.Text.Trim

            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncValidateDate = bStatus
        End Try
    End Function
    Private Sub getLastInvoiceUpdateDate()
        Dim sSql As String
        Dim Dr As DataRow()
        Try
            txtDD.Text = Format(ServerDate, "dd")
            txtMM.Text = Format(ServerDate, "MM")
            txtYYYY.Text = Format(ServerDate, "yyyy")
            sSql = "Select convert(char(8), LastInvoiceUpdateDate, 112) LastInvoiceUpdateDate,LastInvoiceNo LastInvoiceNo From tblOnlineLog"
            Dr = DM.RowReturnProjExecNonQryWithDataRow(sSql)
            If Dr.Length > 0 Then
                If IsDBNull(Dr(0)("LastInvoiceUpdateDate")) = False Then
                    sInvoiceUpdateDate = Dr(0)("LastInvoiceUpdateDate")
                Else
                    sInvoiceUpdateDate = Format(ServerDate, "yyyyMMdd")
                End If
            Else
                sInvoiceUpdateDate = Format(ServerDate, "yyyyMMdd")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub getLastSalesReturnNo()
        Dim sSql As String
        Dim Dr As DataRow()
        Try

            sSql = "Select  LastSalesReturnNo From tblOnlineLog"
            Dr = DM.RowReturnProjExecNonQryWithDataRow(sSql)
            If Dr.Length > 0 Then
                If IsDBNull(Dr(0)("LastSalesReturnNo")) = False Then
                    sSalesReturnNo = Dr(0)("LastSalesReturnNo")
                Else
                    sSalesReturnNo = 0
                End If
            Else
                sSalesReturnNo = 0
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub getLastOrderNo()
        Dim sSql As String
        Dim Dr As DataRow()
        Try

            sSql = "Select  LastOrderNo From tblOnlineLog"
            Dr = DM.RowReturnProjExecNonQryWithDataRow(sSql)
            If Dr.Length > 0 Then
                If IsDBNull(Dr(0)("LastOrderNo")) = False Then
                    sOrderNo = Dr(0)("LastOrderNo")
                Else
                    sOrderNo = 0
                End If
            Else
                sOrderNo = 0
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub getLastRepairNo()
        Dim sSql As String
        Dim Dr As DataRow()
        Try

            sSql = "Select  LastRepairNo From tblOnlineLog"
            Dr = DM.RowReturnProjExecNonQryWithDataRow(sSql)
            If Dr.Length > 0 Then
                If IsDBNull(Dr(0)("LastRepairNo")) = False Then
                    sRepairNo = Dr(0)("LastRepairNo")
                Else
                    sRepairNo = 0
                End If
            Else
                sRepairNo = 0
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Function fncUpdateSalesDetails(ByVal sLDate, ByVal sDate) As Boolean
        Dim sSelect As String
        Dim objEodToHq As New ClsEOD
        Dim dsSales As New DataSet
        Try
            getLastInvoiceUpdateDate()
            sLDate = sInvoiceUpdateDate
            ' If fncDatevalidation(sLDate, sDate) Then
            sSelect = "Select * From tblSalesHeader Where  Convert(char(8), CreateDate, 112) >= " & fncFormatSQLText(sLDate) & vbCrLf
            sSelect &= "Select * From tblSalesDetail Where InvoiceNo in (Select InvoiceNo From tblSalesHeader Where  Convert(char(8), CreateDate, 112) >= " & fncFormatSQLText(sLDate) & ")" & vbCrLf
            sSelect &= "Select * From tblTradeInHeader Where Convert(char(8), CreateDate, 112) >= " & fncFormatSQLText(sLDate) & vbCrLf
            sSelect &= "Select * From tblTradeInDetail Where InvoiceNo in (Select InvoiceNo From tblTradeInHeader Where Convert(char(8), CreateDate, 112) >= " & fncFormatSQLText(sLDate) & ")" & vbCrLf
            sSelect &= "Select * From tblInstallmentEntryHeader Where Convert(char(8), CreateDate, 112) >= " & fncFormatSQLText(sLDate) & vbCrLf
            sSelect &= "Select * From tblInstallmentEntryDetail Where Convert(char(8), CreateDate, 112) >= " & fncFormatSQLText(sLDate) & vbCrLf
            sSelect &= "Select * From tblInstallmentDetail Where Convert(char(8), CreateDate, 112) >= " & fncFormatSQLText(sLDate) & vbCrLf
            sSelect &= "Select * From tblPaymodeDetail Where InvoiceNo in (Select InvoiceNo from tblSalesHeader Where  Convert(char(8), CreateDate, 112) >= " & fncFormatSQLText(sLDate) & " Union Select InvoiceNo from tblTradeInHeader Where Convert(char(8), CreateDate, 112) >= " & fncFormatSQLText(sLDate) & " Union Select InstallmentNo from tblInstallmentDetail Where Convert(char(8), CreateDate, 112) >= " & fncFormatSQLText(sLDate) & " Union Select InsAgreementNo from tblInstallmentEntryHeader Where Convert(char(8), CreateDate, 112) >= " & fncFormatSQLText(sLDate) & "  Union Select OrderNo from tblInventoryOrderHeader Where Convert(char(8), CreatedDate, 112) >= " & fncFormatSQLText(sLDate) & " Union Select RepairNo from tblInventoryRepairHeader Where Convert(char(8), ModifiedDate, 112) >= " & fncFormatSQLText(sLDate) & " Union Select OrderPaymentNo from tblOrderPaymentDetail Where Convert(char(8), CreateDate, 112) >= " & fncFormatSQLText(sLDate) & " Union Select PayOutNo from tblPayOutDetail Where Convert(char(8), CreateDate, 112) >= " & fncFormatSQLText(sLDate) & ")" & vbCrLf
            dsSales = DM.getDataSetWithSqlAdapter(sSelect, "Sales", "Sales")
            If Not dsSales Is Nothing Then
                If dsSales.Tables(0).Rows.Count > 0 Or dsSales.Tables(1).Rows.Count > 0 Or dsSales.Tables(2).Rows.Count > 0 Or dsSales.Tables(3).Rows.Count > 0 Or dsSales.Tables(4).Rows.Count > 0 Or dsSales.Tables(5).Rows.Count > 0 Or dsSales.Tables(6).Rows.Count > 0 Or dsSales.Tables(7).Rows.Count > 0 Then
                    If objEodToHq.fncUpdateSalesToHQ(dsSales) Then
                        fncUpdateSalesDetails = True
                    End If
                Else
                    fncUpdateSalesDetails = True
                End If
            End If
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsSales = Nothing
        End Try
    End Function
    Private Function fncUpdateTerminalDetails(ByVal sLDate, ByVal sDate) As Boolean
        Dim sSelect As String
        Dim objEodToHq As New ClsEOD
        Dim dsSales As New DataSet
        Try
            'getLastInvoiceUpdateDate()
            'sLDate = sInvoiceUpdateDate
            ' If fncDatevalidation(sLDate, sDate) Then
            sSelect = "Select * From tblTerminalSales Where  Convert(char(8), Date, 112) >= " & fncFormatSQLText(sLDate) & vbCrLf
            sSelect &= "Select * From tblTerminalDinamination Where  Convert(char(8), PostingDate, 112) >= " & fncFormatSQLText(sLDate)
            dsSales = DM.getDataSetWithSqlAdapter(sSelect, "Sales", "Sales")
            If Not dsSales Is Nothing Then
                If dsSales.Tables(0).Rows.Count > 0 Or dsSales.Tables(1).Rows.Count > 0 Then
                    If objEodToHq.fncUpdateTerminalSalesToHQ(dsSales) Then
                        fncUpdateTerminalDetails = True
                    End If
                Else
                    fncUpdateTerminalDetails = True
                End If
            End If
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsSales = Nothing
        End Try
    End Function
    Private Function fncUpdateSalesReturnDetails(ByVal sLDate, ByVal sDate) As Boolean
        Dim sSelect As String
        Dim objEodToHq As New ClsEOD
        Dim dsSales As New DataSet
        Try
            getLastSalesReturnNo()
            '   If fncDatevalidation(sLDate, sDate) Then
            sSelect &= "Select * From tblSalesReturnHeader Where RefNo >= " & fncFormatSQLText(sSalesReturnNo) & vbCrLf
            sSelect &= "Select * From tblSalesReturnDetail Where RefNo in (Select RefNo From tblSalesReturnHeader Where RefNo >= " & fncFormatSQLText(sSalesReturnNo) & ")" & vbCrLf
            dsSales = DM.getDataSetWithSqlAdapter(sSelect, "Sales", "Sales")
            If Not dsSales Is Nothing Then
                If dsSales.Tables(0).Rows.Count > 0 Or dsSales.Tables(1).Rows.Count > 0 Then
                    If objEodToHq.fncUpdateSaleReturnDetailsToHQ(dsSales) Then
                        fncUpdateSalesReturnDetails = True
                    End If
                Else
                    fncUpdateSalesReturnDetails = True
                End If
            End If
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsSales = Nothing
        End Try
    End Function
    Private Function fncUpdateOrderDetails(ByVal sLDate, ByVal sDate) As Boolean
        Dim sSelect As String
        Dim objEodToHq As New ClsEOD
        Dim dsSales As New DataSet
        Dim dsOrder As New DataSet
        Try
            getLastOrderNo()

            '   If fncDatevalidation(sLDate, sDate) Then
            sSelect &= "Select * From tblinventoryOrderHeader Where OrderNo >= " & fncFormatSQLText(sOrderNo) & " Order by OrderNo " & vbCrLf
            sSelect &= "Select * From tblinventoryOrderDetail Where OrderNo in (Select OrderNo tblinventoryOrderHeader Where OrderNo >= " & fncFormatSQLText(sOrderNo) & ")" & " order by OrderNo" & vbCrLf
            dsSales = DM.getDataSetWithSqlAdapter(sSelect, "Sales", "Sales")
            If Not dsSales Is Nothing Then
                If dsSales.Tables(0).Rows.Count > 0 Or dsSales.Tables(1).Rows.Count > 0 Then
                    If objEodToHq.fncUpdateOrderDetailsToHQ(dsSales) Then
                        fncUpdateOrderDetails = True
                    End If
                Else
                    fncUpdateOrderDetails = True
                End If
            End If

            sSelect = "Select b.OrderNo,b.OrderInventoryCode,b.InventoryCode,b.Description,b.WeightEstimated,b.EstimatedCost,a.TotalCostEstimated,a.TotalGSTEstimated From tblinventoryOrderHeader a with(nolock),tblinventoryOrderDetail b with(nolock) Where a.OrderNo=b.OrderNo and Inventorycode<>'' and Locationcode=" & fncFormatSQLText(gthisLocation) & "  and Convert(char(8), b.ModifiedDate, 112) >= " & fncFormatSQLText(sLDate) & vbCrLf
            dsOrder = DM.getDataSetWithSqlAdapterAcc(sSelect, "Order", "Order")
            If Not dsOrder Is Nothing Then
                If dsOrder.Tables(0).Rows.Count > 0 Then
                    If objEodToHq.fncUpdateOrderInventoryDetailLocal(dsOrder) Then
                        fncUpdateOrderDetails = True
                    End If
                Else
                    fncUpdateOrderDetails = True
                End If
            End If


            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsSales = Nothing
        End Try
    End Function
    Private Function fncUpdateRepairDetails(ByVal sLDate, ByVal sDate) As Boolean
        Dim sSelect As String
        Dim objEodToHq As New ClsEOD
        Dim dsSales As New DataSet
        Try

            getLastRepairNo()
            '   If fncDatevalidation(sLDate, sDate) Then
            sSelect &= "Select * From tblinventoryRepairHeader Where RepairNo >= " & fncFormatSQLText(sRepairNo) & vbCrLf
            sSelect &= "Select * From tblinventoryRepairDetail Where RepairNo in (Select RepairNo From tblinventoryRepairHeader Where RepairNo >= " & fncFormatSQLText(sRepairNo) & ")" & vbCrLf
            dsSales = DM.getDataSetWithSqlAdapter(sSelect, "Sales", "Sales")
            If Not dsSales Is Nothing Then
                If dsSales.Tables(0).Rows.Count > 0 Or dsSales.Tables(1).Rows.Count > 0 Then
                    If objEodToHq.fncUpdateRepairDetailsToHQ(dsSales) Then
                        fncUpdateRepairDetails = True
                    End If
                Else
                    fncUpdateRepairDetails = True
                End If
            End If
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsSales = Nothing
        End Try
    End Function
    Private Function fncUpdateTradeInStockDetails(ByVal sLDate, ByVal sDate) As Boolean
        Dim sSelect As String
        Dim objEodToHq As New ClsEOD
        Dim dsSales As New DataSet
        Try

         
            sSelect &= "SELECT * FROM TBLTRADEINDETAIL WHERE INVOICENO IN(SELECT INVOICENO FROM TBLTRADEINHEADER with(nolock)  WHERE ISSUEDWEIGHT=0)" & vbCrLf
            sSelect &= "SELECT * FROM TBLTRADEINHEADER WHERE ISSUEDWEIGHT=0" & vbCrLf
            dsSales = DM.getDataSetWithSqlAdapter(sSelect, "Sales", "Sales")
            If Not dsSales Is Nothing Then
                If dsSales.Tables(0).Rows.Count > 0 Or dsSales.Tables(1).Rows.Count > 0 Then
                    If objEodToHq.fncUpdateTradeInStockToHQ(dsSales) Then
                        fncUpdateTradeInStockDetails = True
                    End If
                Else
                    fncUpdateTradeInStockDetails = True
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsSales = Nothing
        End Try
    End Function
    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub frmDayEndtoHQ_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        txtDD.Text = Format(Date.Now.Day, "00")
        txtMM.Text = Format(Date.Now.Month, "00")
        txtYYYY.Text = Date.Now.Year
        getLastEODDate()
    End Sub
    Private Sub getLastEODDate()
        Dim sSql As String
        Dim Dr As DataRow()
        Try
            txtDD.Text = Format(Date.Now.Day, "00")
            txtMM.Text = Format(Date.Now.Month, "00")
            txtYYYY.Text = Date.Now.Year

            sSql = "Select convert(char(8),EODDate,112) EODDate From tblLocation where LocationCode = " & fncFormatSQLText(Trim$(gthisLocation))
            Dr = dm.RowReturnProjExecNonQryWithDataRow(sSql)
            If Dr.Length > 0 And Trim(Dr(0)("EODDate") & "") <> "" Then
                txtLDD.Text = Mid(Dr(0)("EODDate"), 7, 2)
                txtLMM.Text = Mid(Dr(0)("EODDate"), 5, 2)
                txtLYYYY.Text = Mid(Dr(0)("EODDate"), 1, 4)
            Else
                txtLDD.Text = Now.Date.Day
                txtLMM.Text = Now.Date.Month
                txtLYYYY.Text = Now.Date.Year
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Function fncUpdateOutletMovement(ByVal sLDate, ByVal sDate) As Boolean
        Dim sSelect As String
        Dim objEodToHq As New ClsEOD
        Dim dsSales As New DataSet
        Dim dsOrder As New DataSet
        Try

            sSelect = "Select * From tblInventoryMovement Where Convert(char(8), ModifyDate, 112) >= " & fncFormatSQLText(sLDate)
            dsSales = DM.getDataSetWithSqlAdapter(sSelect, "Sales", "Sales")
            If Not dsSales Is Nothing Then
                If dsSales.Tables(0).Rows.Count > 0 Then
                    If objEodToHq.fncUpdateOutletMovementToHQ(dsSales) Then
                        fncUpdateOutletMovement = True
                    End If
                Else
                    fncUpdateOutletMovement = True
                End If
            End If



        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsSales = Nothing
        End Try
    End Function
    Private Function fncUpdateOutletUserLog(ByVal sLDate, ByVal sDate) As Boolean
        Dim sSelect As String
        Dim objEodToHq As New ClsEOD
        Dim dsSales As New DataSet
        Dim dsOrder As New DataSet
        Try

            sSelect = "Select * From tblUserLog Where Convert(char(8), ModifyDate, 112) >= " & fncFormatSQLText(sLDate)
            dsSales = DM.getDataSetWithSqlAdapter(sSelect, "Sales", "Sales")
            If Not dsSales Is Nothing Then
                If dsSales.Tables(0).Rows.Count > 0 Then
                    If objEodToHq.fncUpdateOutletUserLogToHQ(dsSales) Then
                        fncUpdateOutletUserLog = True
                    End If
                Else
                    fncUpdateOutletUserLog = True
                End If
            End If



        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsSales = Nothing
        End Try
    End Function
End Class