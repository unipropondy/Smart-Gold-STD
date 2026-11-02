Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmRepairEntry
    Dim sCustomerCode As String
    Dim objCust As New clsCustomer
    Dim objClsOrder As New clsRepair
    Dim objComp As New clsCompany
    Dim bReprint As Boolean
    Dim CompName$, Address1$, Address2$, Country$, PhoneNo$, FaxNo$
    Dim msOrderpaymentNo As String
    Dim sNextOrderNo As String
    Public tradeInRefNo As String
    Dim sType As String
    Dim dTradeInGST As Double
    Dim dGSTPerc As Double
    Dim bEdited As Boolean
    Const ICode = 0
    Const IClass = 1
    Const IDescription = 2
    Const IWeight = 3
    Const IGoldLoss = 4
    Const ICostPerGram = 5
    Const IDSWeight = 6
    Const ISMC = 7
    Const IPCS = 8
    Const ITotalValue = 9
    Const ITotalCost = 10
    Const ISalesType = 11
    Const IPMC = 12
    Const IGST = 13
    Const IDiscount = 14

    Dim msSalesManCode As String
    Dim sBarLocationCode, sBarInvoiceNo, sBarInvoiceDate, sBarClassCode As String
    Dim dBarWeight, dBarStoneWeight, dBarTotalCost As Double
    Private Sub subAddToGridOrder()
        Dim lQty As Long
        Dim lRow As Long
        '    Dim objTransfer As New clsTransfer
        Dim iRow As Long
        Try
            lQty = 0
            lRow = 0
            'If fncInventoryCodeExist(txtInventoryCode.Text.Trim, lRow, dQty) Then
            '    MsgBox(" Item found in tranfer list ")
            '    '' subClearTransfer()
            '    If txtInventoryCode.CanFocus Then txtInventoryCode.Focus()
            '    Exit Sub
            'End If

            If fncIsValidToAdd() = True Then


                If lQty = 0 Then

                    iRow = grdOrder.Rows.Add


                    Call fnSetdgvText(grdOrder, iRow, cInvDescription.Index, Trim(cboRepairDescription.Text))
                    Call fnSetdgvText(grdOrder, iRow, cInvWeight.Index, Format(Val(txtInvWeight.Text), "0.00"))
                    Call fnSetdgvText(grdOrder, iRow, cInvTotalCost.Index, Format(Val(txtCost.Text), "0.00"))
                    subCalcTotal()
                End If
                subClearControls()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub subdelToGridOrder()
        Dim lQty As Long
        Dim lRow As Long
        '    Dim objTransfer As New clsTransfer
        Dim iRow As Long
        Try
            lQty = 0
            lRow = 0
            'If fncInventoryCodeExist(txtInventoryCode.Text.Trim, lRow, dQty) Then
            '    MsgBox(" Item found in tranfer list ")
            '    '' subClearTransfer()
            '    If txtInventoryCode.CanFocus Then txtInventoryCode.Focus()
            '    Exit Sub
            'End If

            'If fncIsValidToAdd() = True Then


            If lQty = 0 Then

                iRow = grdOrder.Rows.Add


                Call fnSetdgvText(grdOrder, iRow, cInvDescription.Index, Trim(cboRepairDescription.Text))
                Call fnSetdgvText(grdOrder, iRow, cInvWeight.Index, Format(Val(txtInvWeight.Text), "0.00"))
                Call fnSetdgvText(grdOrder, iRow, cInvTotalCost.Index, Format(Val(txtCost.Text), "0.00"))
                subCalcTotal()
            End If
            subClearControls()
            ' End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub subCalcTotal()
        Dim iCnt As Integer
        Dim dTotalesticost As Double
        Dim dTotalestiweight As Double
        Dim i
        Try
            txtEstiWeight.Text = 0.0
            txtEstiTotalCost.Text = 0.0
            dTotalesticost = 0
            dTotalestiweight = 0
            For i = 0 To grdOrder.Rows.Count - 1
                dTotalesticost = Val(dTotalesticost) + Val(fnGetdgvText(grdOrder, i, cInvTotalCost.Index))
                dTotalestiweight = Val(dTotalestiweight) + Val(fnGetdgvText(grdOrder, i, cInvWeight.Index))
            Next i
            txtEstiWeight.Text = Format(dTotalestiweight, "0.00")
            txtEstiTotalCost.Text = Format(dTotalesticost, "0.00")
            txtBalanceAmount.Text = Format(Val(txtEstiTotalCost.Text) - Val(txtAdvancePaid.Text), "0.00")
            'For iCnt = 1 To grdOrderEntry.MaxRows
            '    txtEstiWeight.Text = Val(txtEstiWeight.Text.Trim) + Val(GetText(grdOrderEntry, iCnt, cWeight))
            '    txtEstiTotalCost.Text = Val(txtEstiTotalCost.Text.Trim) + Val(GetText(grdOrderEntry, iCnt, cTotalCost))
            'Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub btnCustomerCodeSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnCustomerCodeSearch.Click
        Try
            ' Dim frmHelpCustomer As New frmHelpSearch(0, 0, "tblCustomer", "L", 4, "CustomerName", "", "CustomerCode", "CustomerCode", 80, "CustomerName", "CustomerName", 150, "Address1", "Address1", 90, "Address2", "Address2", 80)
            Dim frmHelpCustomer As New frmHelpSearch(0, 0, "tblCustomer", "R", 4, "CustomerName", "", "CustomerCode", "CustomerCode", 80, "CustomerName", "CustomerName", 150, "NricNo", "NricNo", 90, "Address1", "Address1", 80)
            frmHelpCustomer.ShowDialog()
            txtCustomerCode.Text = frmHelpCustomer.Var1.Trim
            txtCustomerName.Text = frmHelpCustomer.Var2.Trim
            subGetCustomerDetail()
            sCustomerCode = Trim(txtCustomerCode.Text)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subGetCustomerDetail()
        Dim dsCust As New DataSet
        Try
            If txtCustomerCode.Text.Trim <> "" Then
                sCustomerCode = Trim(txtCustomerCode.Text.Trim)
                dsCust = objCust.GetCustomerwithCode(txtCustomerCode.Text.Trim)
                If dsCust.Tables(0).Rows.Count > 0 Then
                    txtCustomerName.Text = Trim(dsCust.Tables(0).Rows(0)("CustomerName") & "")
                    txtAddress1.Text = Trim(dsCust.Tables(0).Rows(0)("Address1") & "")
                    txtAddress2.Text = Trim(dsCust.Tables(0).Rows(0)("Address2") & "")
                    txtAddress3.Text = Trim(dsCust.Tables(0).Rows(0)("Address3") & "")
                    ' txtInventoryCode.Focus()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsCust = Nothing
        End Try
    End Sub

    Private Sub btnNewCustomer_Click(sender As System.Object, e As System.EventArgs) Handles btnNewCustomer.Click
        frmOutletCustomers.Show()
    End Sub
    Private Sub subClearControls()
        Try
            cboRepairDescription.Text = ""
            txtInvWeight.Clear()
            txtCost.Clear()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Function fncIsValidToAdd() As Boolean
        Dim PURCHASETYPE_WEIGHT As String = "W"
        Dim bStatus As Boolean
        Try
            bStatus = False

            If cboRepairDescription.Text.Trim = "" Then
                MessageBox.Show("Please enter description for the item.")
                cboRepairDescription.Focus()
                Exit Function
            End If
            'If (txtInvWeight.Text.Trim = "" Or Val(txtInvWeight.Text.Trim) = 0) Then
            '    MessageBox.Show("Please enter weight.")
            '    txtInvWeight.Focus()
            '    Exit Function
            'End If

            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncIsValidToAdd = bStatus
        End Try
    End Function

    Private Sub txtCost_LostFocus(sender As Object, e As System.EventArgs) Handles txtCost.LostFocus
        ' subAddToGridOrder()
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        Dim sRateFixed As String
        Dim sTradeInType As String
        Dim sInvType As String
        Dim iCnt As Integer
        Dim objPM As New clsPayMode
        Dim objSales As New ClsSales
        Dim frmPM As New frmPayingmode
        Dim orderedDate, IssueDate, sDeliveryDate As String
        Dim dm As DataManager
        Dim sExec As String
        Try

            orderedDate = txtODD.Text.Trim & "/" & txtOMM.Text.Trim & "/" & txtOYYYY.Text.Trim
            IssueDate = txtIDD.Text.Trim & "/" & txtIMM.Text.Trim & "/" & txtIYY.Text.Trim
            sDeliveryDate = txtEstiDD.Text.Trim & "/" & txtEstiMM.Text.Trim & "/" & txtEstiYY.Text.Trim
            If fncValidateMe() Then
                'If optTodayRate.Checked Then
                '    sRateFixed = "Y"
                'Else
                '    sRateFixed = "N"
                'End If
                If optCash.Checked Then
                    sTradeInType = "C"
                ElseIf optGold.Checked Then
                    sTradeInType = "G"
                ElseIf optBoth.Checked Then
                    sTradeInType = "B"
                End If
                sInvType = "R"
                subCalcTotal()

                objClsOrder.OpenRepairConnection()
                objClsOrder.gpBeginRepairTransaction()
                objSales.subOpenSalesConnection()
                objSales.subBeginSalesTransaction()
                If txtInvoiceNo.Text.Trim = "" Then

                    txtInvoiceNo.Text = "RP-" & objClsOrder.GetNextOrderNo()
                    If txtInvoiceNo.Text.Trim = "" Then
                        objClsOrder.gpRollBackRepairConnection()
                        objClsOrder.CloseRepairConnection()
                        Exit Sub
                    End If
                    dPaidAmount = 0
                    ReturnAmount = 0
                    'nTotalNet = Val(txtAdvancePaid.Text)
                    If optCash.Checked Then
                        nTotalNet = Val(txtAdvancePaid.Text)
                    Else
                        nTotalNet = 0
                    End If
                    If optBoth.Checked Then
                        nTotalNet = Val(txtAdvancePaid.Text) 'Val(txtAdvancePaid.Text) + Val(txtTradeINAmount.Text)
                    End If
                    If Val(nTotalNet) > 0 Then
                        frmPM.SalesType = ""
                        frmPM.ShowDialog()
                    ElseIf optGold.Checked Then
                        frmPM.SalesType = ""
                        frmPM.ShowDialog()
                    End If
                    If dPaidAmount > 0 Then
                        'save paymode detail
                        If Not fncPaymodeDetailSave(txtInvoiceNo.Text.Trim) Then
                            MessageBox.Show("Error in save.")
                            objClsOrder.gpRollBackRepairConnection()
                            objClsOrder.CloseRepairConnection()
                        End If
                    Else
                        Exit Sub
                    End If
                End If

                'save order header
                If Not objClsOrder.FncRepairHeaderSave(txtInvoiceNo.Text.Trim, orderedDate.Trim, txtCustomerCode.Text.Trim, txtSupplierCode.Text.Trim, _
                                            Trim(sDeliveryDate), Val(txtTradeINAmount.Text), "", sTradeInType, sRateFixed, Val(txtEstiTotalCost.Text.Trim), _
                                            Val(txtAdvancePaid.Text.Trim), Trim(sInvType), txtRemarks.Text.Trim, Trim(msSalesManCode), sInvType, Val(dTradeInGST), IssueDate) Then
                    MsgBox(" Error in Order Entry save !", MsgBoxStyle.Critical)
                    objClsOrder.gpRollBackRepairConnection()
                    objClsOrder.CloseRepairConnection()
                    Exit Sub
                End If
                'save order detail
                sExec = "Delete from [tblInventoryRepairDetail] where RepairNo=" & fncFormatSQLText(txtInvoiceNo.Text)
                dm.ProjExecNonQry(sExec)
                For iCnt = 0 To grdOrder.RowCount - 1
                    If Not objClsOrder.FncRepairDetailSave(txtInvoiceNo.Text.Trim, fnGetdgvText(grdOrder, iCnt, cInvDescription.Index).Trim, Val(fnGetdgvText(grdOrder, iCnt, cInvWeight.Index).Trim), Val(fnGetdgvText(grdOrder, iCnt, cInvTotalCost.Index).Trim), "") Then
                        objClsOrder.gpRollBackRepairConnection()
                        objClsOrder.CloseRepairConnection()
                        Exit Sub
                    End If
                Next
                If Not fncSaveTradeInSales(txtInvoiceNo.Text.Trim) Then
                    MessageBox.Show("Error in save Detail")
                    objSales.subRollBackSalesTransaction()
                    objSales.subCloseSalesConnection()
                    Exit Sub
                End If
                If Val(txtAdvancePaid.Text) > 0 Then
                    msOrderpaymentNo = Trim("RP" & objClsOrder.fncGetNextOrderPaymentNo())
                    If Not fncSaveOrderPaymentDetail() Then
                        MessageBox.Show("Transaction not saved")
                        objClsOrder.gpRollBackRepairConnection()
                        objClsOrder.CloseRepairConnection()
                    End If
                End If
                dTradeInSales = 0
                objSales.subCommitSalesTransaction()
                objSales.subCloseSalesConnection()
                objClsOrder.gpCommitRepairTransaction()
                objClsOrder.CloseRepairConnection()
                If sInvType = "O" Then
                    MsgBox(" Order Entry Saved  ", MsgBoxStyle.Information)
                ElseIf sInvType = "R" Then
                    MsgBox(" Repair Entry Saved  ", MsgBoxStyle.Information)
                End If
                If txtInvoiceNo.Text.Trim <> "" Then
                    subPrintBill(txtInvoiceNo.Text.Trim)
                    If chkService.Checked = False Then
                        Call subBarcodeReportPrintAJ(Trim(txtInvoiceNo.Text))
                    End If
                End If
                If Trim(sType) = "TRADEIN" And grdSales.RowCount > 0 Then
                    Call subGetBarInventory(txtInvoiceNo.Text.Trim)
                End If
                'If Trim(msOrderpaymentNo) <> "" Then
                '    Call subPrintRepair(Trim(txtInvoiceNo.Text))

                'End If
                ClearControls()
            End If



        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subGetBarInventory(ByVal sInvoiceNo As String)
        Dim DsInv As DataSet
        Dim DsTrade As DataSet
        Dim DsAdditional As DataSet
        Dim sender As System.Object
        Dim e As System.EventArgs
        Dim objInv As New clsInventory
        Dim objTrade As New clsTradeIN
        Dim Ds As DataSet
        Dim objTray As New clsTray
        Dim icnt As Long

        Try
            '   DsInv = objInv.GetInventoryDetail(txtInventoryCode.Text.Trim)
            DsTrade = objTrade.getTradeInDetailForBarCode(sInvoiceNo)
            ' DsAdditional = objInv.GetAdditionalInventoryDetail(txtInventoryCode.Text.Trim)

            If DsTrade.Tables("TradeIn").Rows.Count > 0 Then
                For icnt = 0 To DsTrade.Tables("TradeIn").Rows.Count - 1
                    sBarLocationCode = Trim(gthisLocation)
                    sBarInvoiceNo = Trim(DsTrade.Tables("TradeIn").Rows(icnt)("InvoiceNo") & "")
                    sBarInvoiceDate = Trim(DsTrade.Tables("TradeIn").Rows(icnt)("InvoiceDate") & "")
                    sBarClassCode = Trim(DsTrade.Tables("TradeIn").Rows(icnt)("Class") & "")
                    dBarWeight = DsTrade.Tables("TradeIn").Rows(icnt)("Weight") & ""
                    dBarStoneWeight = DsTrade.Tables("TradeIn").Rows(icnt)("StoneWeight") & ""
                    dBarTotalCost = DsTrade.Tables("TradeIn").Rows(icnt)("TotalCost") & ""
                    subTradeInBarcodeReportPrintAJ()
                Next
            Else
                MessageBox.Show("InventoryCode Not Found", "GoldPOS")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subTradeInBarcodeReportPrintAJ()

        Dim sSql$
        Dim sTemp As String
        Dim sChar As String
        Dim strCompanyname, strAddress, strReportTitle, strpath, strselectformula, strDateFrom, strDateTo As String
        Dim strSubHead As String
        Dim CR As New ReportDocument
        Dim frmRep As New frmReports
        Dim objCompany As New clsCompany
        Dim dsMCC As New DataSet
        Dim objinv As New clsInventory
        Dim ds As DataSet
        Try
            strpath = Application.StartupPath & "\Reports\rptTradeInBarcode_AJ.rpt"
            Me.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            ds = objCompany.getCompanyName
            If ds.Tables("GetCompanyName").Rows.Count > 0 Then
                strCompanyname = Trim(ds.Tables("GetCompanyName").Rows(0)("Name") & "")
                strAddress = Trim(ds.Tables("GetCompanyName").Rows(0)("Address1") & "") & " " & _
                             Trim(ds.Tables("GetCompanyName").Rows(0)("Address2") & "") & " " & _
                             Trim(ds.Tables("GetCompanyName").Rows(0)("City") & "")
            End If
            frmRep.GetModule = "Barcode"
            frmRep.GetReportType = "Summary"
            frmRep.GetPrintOption = "Preview"

            CR.Load(strpath)
            ReportLogOnToServer(CR)
            CR.Refresh()
            frmRep.ReportViewer.ReportSource = CR
            strselectformula = "{tblTradeInDetail.InvoiceNo}=" & fncFormatSQLText(Trim(sBarInvoiceNo)) & " and "
            strselectformula = strselectformula & "{tblTradeInDetail.Class}=" & fncFormatSQLText(Trim(sBarClassCode))

            CR.DataDefinition.FormulaFields("LocationCode").Text = fncFormatSQLText(sBarLocationCode)
            CR.DataDefinition.FormulaFields("InvoiceNo").Text = fncFormatSQLText(sBarInvoiceNo)
            CR.DataDefinition.FormulaFields("InvoiceDate").Text = fncFormatSQLText(sBarInvoiceDate)
            CR.DataDefinition.FormulaFields("ClassCode").Text = fncFormatSQLText(sBarClassCode)
            CR.DataDefinition.FormulaFields("Weight").Text = fncFormatSQLText(Format((Val(dBarWeight) - Val(dBarStoneWeight)), "###0.00") + "gm")
            CR.DataDefinition.FormulaFields("StoneWeight").Text = fncFormatSQLText(Format(Val(dBarWeight), "###0.00") + "gm") 'fncFormatSQLText(Format((Val(dBarWeight) + Val(dBarStoneWeight)), "###0.00") + "gm")
            CR.DataDefinition.FormulaFields("TotalCost").Text = Format(Val(dBarTotalCost), "###0.00")
            CR.DataDefinition.FormulaFields("Salesman").Text = fncFormatSQLText(msSalesManCode)
            frmRep.ReportViewer.SelectionFormula = strselectformula
            CR.DataDefinition.RecordSelectionFormula = strselectformula
            ' ''frmRep.ShowDialog()
            ' ''frmRep.ReportViewer.Visible = True
            ' ''frmRep.ReportViewer.DisplayToolbar = True
            ' ''frmRep.ReportViewer.CloseView(False)
            ' ''frmRep.WindowState = FormWindowState.Maximized
            CR.PrintToPrinter(1, True, 0, 0)
            frmRep = Nothing
            frmRep = New frmReports
            Me.Cursor.Current = System.Windows.Forms.Cursors.Default


            CR.Close()
            CR.Dispose()
            CR = Nothing


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subBarcodeReportPrintAJ(ByVal sInvoiceNo As String)

        Dim sSql$
        Dim sTemp As String
        Dim sChar As String
        Dim strCompanyname, strAddress, strReportTitle, strpath, strselectformula, strDateFrom, strDateTo As String
        Dim strSubHead As String
        Dim CR As New ReportDocument
        Dim frmRep As New frmReports
        Dim objCompany As New clsCompany
        Dim dsMCC As New DataSet
        Dim objinv As New clsInventory
        Dim ds As DataSet
        Try
            strpath = Application.StartupPath & "\Reports\rptRepairBarcode_AJ.rpt"
            Me.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            ds = objCompany.getCompanyName
            If ds.Tables("GetCompanyName").Rows.Count > 0 Then
                strCompanyname = Trim(ds.Tables("GetCompanyName").Rows(0)("Name") & "")
                strAddress = Trim(ds.Tables("GetCompanyName").Rows(0)("Address1") & "") & " " & _
                             Trim(ds.Tables("GetCompanyName").Rows(0)("Address2") & "") & " " & _
                             Trim(ds.Tables("GetCompanyName").Rows(0)("City") & "")
            End If
            frmRep.GetModule = "Barcode"
            frmRep.GetReportType = "Summary"
            frmRep.GetPrintOption = "Preview"

            CR.Load(strpath)
            ReportLogOnToServer(CR)
            CR.Refresh()
            frmRep.ReportViewer.ReportSource = CR
            strselectformula = "{tblInventoryRepairHeader.RepairNo}=" & fncFormatSQLText(Trim(sInvoiceNo))

            frmRep.ReportViewer.SelectionFormula = strselectformula
            CR.DataDefinition.RecordSelectionFormula = strselectformula
            ' ''frmRep.ShowDialog()
            ' ''frmRep.ReportViewer.Visible = True
            ' ''frmRep.ReportViewer.DisplayToolbar = True
            ' ''frmRep.ReportViewer.CloseView(False)
            ' ''frmRep.WindowState = FormWindowState.Maximized
            CR.PrintToPrinter(1, True, 0, 0)
            frmRep = Nothing
            frmRep = New frmReports
            Me.Cursor.Current = System.Windows.Forms.Cursors.Default


            CR.Close()
            CR.Dispose()
            CR = Nothing


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subPrintRepair(ByVal sInvoiceNo As String)
        Dim sSql As String
        Dim sCompanyname, sAddress, sReportTitle, sPath, sPhone, sFax, sSelectFormula, sPrintFDate, sPrintTDate, sFromDate, sToDate As String
        Dim CR As New ReportDocument
        Dim frmRep As New frmReports
        Dim sSelectFld As String
        Dim sSelection As String
        Dim dsCompany As DataSet
        Dim objClsCompany As New clsCompany1
        Try
            ' If UCase(sCompanyShortCode) = "JH" Then
            '    sPath = Application.StartupPath & "\Reports\rptTransferOutDetail_JH.rpt"
            'Else
            strpath = Application.StartupPath & "\Reports\rptRepairInvoice_AJ.rpt"
            'End If
            CR.Load(strpath)
            ReportLogOnToServer(CR)
            CR.Refresh()

            frmRep.ReportViewer.ReportSource = CR
            sSelection = "{tblInventoryRepairHeader.RepairNo} = " & fncFormatSQLText(Trim(sInvoiceno))
            frmRep.ReportViewer.SelectionFormula = sSelection
            CR.DataDefinition.RecordSelectionFormula = sSelection


            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
            CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
            CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
            CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
            'CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
            'CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
            'CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
            CR.PrintToPrinter(2, True, 0, 0)
            frmRep = Nothing
            frmRep = New frmReports
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CR.Close()
            CR = Nothing
            frmRep = Nothing
            objClsCompany = Nothing
            dsCompany = Nothing
        End Try
    End Sub
    Private Function fncSaveOrderPaymentDetail() As Boolean
        Try
            Dim iRet As Boolean
            Dim i As Long
            Dim sRemarks$
            Dim sSql$
            iRet = objClsOrder.FncOrderPaymentSave(txtInvoiceNo.Text.Trim, msOrderpaymentNo.Trim, Val(txtAdvancePaid.Text.Trim), "REPAIRENTRY", 0, 0)
            If Not iRet Then
                MessageBox.Show("Error in Repair Payment save.")
                fncSaveOrderPaymentDetail = False
                Exit Function
            End If
            fncSaveOrderPaymentDetail = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function
    Private Function fncPaymodeDetailSave(ByVal sOrderNo As String) As Boolean
        Dim objPM As New clsPayMode
        Try
            Dim bPayModeFlag As Boolean
            Dim sDate As String
            Dim i As Long
            bPayModeFlag = False
            sDate = Format(Now, "dd/MM/yyyy")
            For i = 0 To UBound(arrpay, 1)
                If arrpay(i, 1) > 0 Then
                    iRet = objPM.PayModeDetailSave(sOrderNo, sDate, Trim(arrpay(i, 3)), _
                                                 Val(arrpay(i, 1)), Val(arrpay(i, 2)), _
                                                 Trim(arrpay(i, 4)), "REPAIR")
                    bPayModeFlag = True
                    If Not iRet Then
                        fncPaymodeDetailSave = False
                        Exit Function
                    End If

                End If
            Next i
            If bPayModeFlag = False Then
                iRet = objPM.PayModeDetailSave(sOrderNo, sDate, "CASH", _
                                            Val(txtDepositAmount.Text), 0, _
                                            "MANUAL", "REPAIR")
                If Not iRet Then
                    fncPaymodeDetailSave = False
                    Exit Function
                End If
            End If
            fncPaymodeDetailSave = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function
    Private Function fncValidateMe() As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            If Not IsDate(txtOYYYY.Text.Trim & "/" & txtOMM.Text.Trim & "/" & txtODD.Text.Trim) Then
                MsgBox(" Enter valid datas for Order Date !", MsgBoxStyle.Exclamation)
                txtODD.Clear()
                txtOMM.Clear()
                txtOYYYY.Clear()
                txtODD.Focus()
                Exit Function
            End If

            If txtCustomerCode.Text.Trim.Length = 0 Then
                MsgBox(" Customer code should not be empty !", MsgBoxStyle.Exclamation)
                txtCustomerCode.Focus()
                Exit Function
            End If

            If grdOrder.RowCount = 0 Then
                MessageBox.Show("No Item to save.")
                cboRepairDescription.Focus()
                Exit Function
            End If

            'If Val(txtEstiWeight.Text) = 0 Then
            '    MsgBox("Estimated weight should not be zero!", MsgBoxStyle.Exclamation)
            '    txtEstiWeight.Focus()
            '    Exit Function
            'End If

            'If Val(txtAdvancePaid.Text) = 0 Then
            '    MsgBox("Please enter the Advance amount!", MsgBoxStyle.Exclamation)
            '    txtAdvancePaid.Focus()
            '    Exit Function
            'End If



            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncValidateMe = bStatus
        End Try
    End Function
    Private Sub ClearControls()
        getCurrentDate()
        txtInvoiceNo.Clear()
        txtCustomerCode.Clear()
        txtCustomerName.Clear()
        txtAddress1.Clear()
        txtAddress2.Clear()
        txtAddress3.Clear()
        txtRemarks.Clear()
        txtEstiTotalCost.Text = 0.0
        txtEstiWeight.Text = 0.0
        txtAdvancePaid.Text = 0
        txtBalanceAmount.Text = 0
        txtTradeINAmount.Text = 0
        txtEstiDD.Clear()
        txtEstiMM.Clear()
        txtEstiYY.Clear()
        txtAdvancePaid.ReadOnly = False
        optCash.Checked = True
        btnSave.Enabled = True
        subClearControls()
        ''subClearInventoryDetails()
        grdOrder.RowCount = 0
        ''sStatus = "O"
        txtCustomerCode.Focus()
    End Sub

    Private Sub getCurrentDate()
        txtODD.Text = Format(ServerDate, "dd")
        txtOMM.Text = Format(ServerDate, "MM")
        txtOYYYY.Text = Format(ServerDate, "yyyy")
    End Sub

    Private Sub frmRepairEntry_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Up
                    SendKeys.Send("+{Tab}")
                Case Keys.Down
                    SendKeys.Send("{Tab}")
                Case Keys.Enter
                    SendKeys.Send("{Tab}")
                Case Keys.Escape
                    btnClose_Click(Nothing, Nothing)
                Case Keys.F6
                    '  btnClear_Click(Nothing, Nothing)
                Case Keys.F7
                    btnSave_Click(Nothing, Nothing)
                Case Keys.F8
                    btncancel_Click(Nothing, Nothing)
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub frmRepairEntry_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        PnlSalesManPwd.Visible = True
        txtSalesManPWD.Focus()
        getCurrentDate()
        subgetcompany()
        chkService.Checked = False
    End Sub
    Private Sub subPrintBill(ByVal sInvoiceNo As String)
        Try
            Dim ReportTitle As String
            Dim sSelection As String
            Dim sSalesType$
            Dim frmrep As New frmReports
            Dim CR As New ReportDocument
            Dim j As Integer
            Cursor = Cursors.WaitCursor

            strpath = Application.StartupPath & "\Reports\rptRepairEntry.rpt"
            CR.Load(strpath)
            If Not ReportLogOnToServer(CR) Then
                Exit Sub
            End If
            ReportLogOnToServer(CR)
            CR.Refresh()
            frmrep.ReportViewer.ReportSource = CR
            sSelection = "{tblInventoryRepairHeader.RepairNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
            frmrep.ReportViewer.SelectionFormula = sSelection
            CR.DataDefinition.RecordSelectionFormula = sSelection
            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
            CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
            CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
            CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
            CR.PrintToPrinter(2, True, 0, 0)
            frmrep = Nothing
            frmrep = New frmReports
            ''frmrep.Show()
            '   If bPrinter Then CR.PrintToPrinter(1, True, 0, 0)
            ''frmrep.ReportViewer.Visible = True
            ''frmrep.ReportViewer.DisplayToolbar = True
            ''frmrep.ReportViewer.CloseView(False)
            ''frmrep.WindowState = FormWindowState.Maximized
            ''frmrep = Nothing
            ''frmrep = New frmReports
            '''CR.PrintToPrinter(1, True, 0, 0)
            '''frmrep = Nothing
            '''frmrep = New frmReports

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub subgetcompany()
        Dim dsComp As New DataSet
        Try
            dsComp = objComp.GetCompanyList()
            If dsComp.Tables(0).Rows.Count Then
                CompName = Trim(dsComp.Tables(0).Rows(0)("Name") & "")
                Address1 = Trim(dsComp.Tables(0).Rows(0)("Address1") & "")
                Address2 = Trim(dsComp.Tables(0).Rows(0)("Address2") & "")
                Country = Trim(dsComp.Tables(0).Rows(0)("Country") & "")
                PhoneNo = Trim(dsComp.Tables(0).Rows(0)("Phone") & "")
                FaxNo = Trim(dsComp.Tables(0).Rows(0)("Fax") & "")
                dGSTPerc = Val(dsComp.Tables(0).Rows(0)("GstPerc") & "")
            Else
                MsgBox("Locked")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        ClearAll()
    End Sub
    Public Function ClearAll()
        txtInvoiceNo.Text = ""
        txtCustomerCode.Text = ""
        txtCustomerName.Text = ""
        txtAddress1.Text = ""
        txtAddress2.Text = ""
        txtAddress3.Text = ""
        txtODD.Text = ""
        txtOMM.Text = ""
        txtOYYYY.Text = ""
        cboRepairDescription.Text = ""
        txtEstiWeight.Text = ""
        txtEstiTotalCost.Text = ""
        txtAdvancePaid.Text = ""
        txtDepositAmount.Text = ""
        txtBalanceAmount.Text = ""
        grdOrder.RowCount = 0
        grdSales.RowCount = 0
        If btnCancel.Enabled = False And btnSave.Enabled = False Then
            btnCancel.Enabled = True
            btnSave.Enabled = True
        End If
        getCurrentDate()
        chkService.Checked = False
    End Function


    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        Panel4.Visible = True
        txtInvoiceNoCancel.Focus()
    End Sub

    Private Sub btnInvCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnInvCancel.Click
        Panel4.Visible = False
        cboRepairDescription.Focus()
    End Sub

    Private Sub btnInvoiceCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnInvoiceCancel.Click
        If txtInvoiceNoCancel.Text = "" Then
            MsgBox("Please Enter the Invoiceno For Cancel", MsgBoxStyle.Information)
            txtInvoiceNoCancel.Focus()
            Exit Sub
        End If
        If subCheckInvoiceNo() Then
            subCancelRepairHeader()
            subCancelRepairDetail()
            SubCancelPaymodeDetail()
            MsgBox("The Repair Entry has been cancelled", MsgBoxStyle.Information)
            Panel4.Visible = False
            txtCustomerCode.Focus()
        Else
            MsgBox("Please Enter the Invoiceno For Cancel", MsgBoxStyle.Information)
            txtInvoiceNoCancel.Focus()
            Exit Sub
        End If

    End Sub
    Public Function SubCancelPaymodeDetail()
        Dim sQuery As String
        Dim dm As New DataManager
        sQuery = " Insert into tblpaymodedetail "
        sQuery = sQuery & "Select InvoiceNo,InvoiceDate,PayMode,-Amount as Amount,Commision,SlipNo,CreateUser,CreateDate,ModifyUser,ModifyDate,LocationCode,SalesType from tblpaymodedetail where Invoiceno=" & fncFormatSQLText(txtInvoiceNoCancel.Text)
        dm.ProjExecNonQry(sQuery)
    End Function

    'Private Sub subCancelOrderPayment()
    '    Dim objPM As New clsPayMode
    '    Try
    '        objClsOrder.OpenRepairConnection()
    '        objClsOrder.gpBeginRepairTransaction()
    '        If Not objPM.PayModeDetailSave(txtInvoiceNoCancel.Text.Trim, Format(Now, "dd/MM/yyyy"), "CASH", -Val(GetText(grdOrder, grdOrder.CurrentRow.Index, cInvTotalCost.Index)), Val(0), "RP CANCEL", "REPAIR") Then
    '            objClsOrder.gpRollBackRepairConnection()
    '        Else
    '            MessageBox.Show("Order payment canceled.")
    '            objClsOrder.gpRollBackRepairConnection()
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    Finally
    '        objClsOrder.CloseRepairConnection()
    '        objPM = Nothing
    '    End Try
    'End Sub
    Public Function subCancelRepairHeader()
        Dim sQuery As String
        Dim dm As New DataManager
        sQuery = " Insert into tblInventoryRepairHeaderCancel "
        sQuery = sQuery & "Select * from tblInventoryRepairHeader where RepairNo=" & fncFormatSQLText(txtInvoiceNoCancel.Text)
        dm.ProjExecNonQry(sQuery)
        sQuery = "Delete from tblInventoryRepairHeader where RepairNo=" & fncFormatSQLText(txtInvoiceNoCancel.Text)
        dm.ProjExecNonQry(sQuery)
    End Function
    Public Function subCancelRepairDetail()
        Dim sQuery As String
        Dim dm As New DataManager
        sQuery = " Insert into tblInventoryRepairDetailCancel "
        sQuery = sQuery & "Select * from tblInventoryRepairDetail where RepairNo=" & fncFormatSQLText(txtInvoiceNoCancel.Text)
        dm.ProjExecNonQry(sQuery)
        sQuery = "Delete from tblInventoryRepairDetail where RepairNo=" & fncFormatSQLText(txtInvoiceNoCancel.Text)
        dm.ProjExecNonQry(sQuery)
    End Function
    Public Function subCheckInvoiceNo() As Boolean
        Dim sSql As String
        Dim ds1 As New DataSet
        Dim dm As New DataManager
        subCheckInvoiceNo = False
        sSql = "Select * from tblInventoryRepairHeader where RepairNo=" & fncFormatSQLText(txtInvoiceNoCancel.Text)
        ds1 = dm.getDataSetWithSqlAdapter(sSql, "tblInventoryRepairHeader", "tblInventoryRepairHeader")
        If ds1.Tables("tblInventoryRepairHeader").Rows.Count > 0 Then
            subCheckInvoiceNo = True
        Else
            subCheckInvoiceNo = False
        End If
    End Function

    Private Sub Label6_Click(sender As System.Object, e As System.EventArgs) Handles Label6.Click

    End Sub

    Private Sub btnSupplierCodeSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSupplierCodeSearch.Click

        Try
            Dim e1 As New System.EventArgs
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSupplier", "L", 4, "SupplierName", "", "SupplierCode", "SupplierCode", 80, "SupplierName", "SupplierName", 180, "Address1", "Address1", 60, "Address2", "Address2", 80)
            frmHelpClass.ShowDialog()
            txtSupplierCode.Text = frmHelpClass.Var1.Trim
            'txtSupplierName.Text = frmHelpClass.Var2.Trim
            'txtSupplierCode_LostFocus(sender, e1)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtInvoiceNo_Leave(sender As Object, e As System.EventArgs) Handles txtInvoiceNo.Leave
        subGetRepair()
    End Sub
    Private Sub subGetRepair()
        Try

            Dim DR As DataRow()
            Dim sOrderDate, sIssueDate As String
            Dim sSql As String
            If Trim(txtInvoiceNo.Text.Trim) <> "" Then
                sSql = "select * from [tblInventoryRepairHeader] where RepairNo=" & fncFormatSQLText(Trim(txtInvoiceNo.Text.Trim))
                DR = DataManager.RowReturnProjExecNonQryWithDataRow(sSql)
                If DR.Length > 0 Then
                    txtCustomerCode.Text = Trim(DR(0)("CustomerCode") & "")
                    txtSupplierCode.Text = Trim(DR(0)("SupplierCode") & "")
                    subGetCustomerDetail()
                    sOrderDate = Trim(DR(0)("RepairDate") & "")
                    sIssueDate = Trim(DR(0)("IssueDate") & "")
                    If sIssueDate <> "" Then
                        txtIDD.Text = sIssueDate.Substring(0, 2)
                        txtIMM.Text = sIssueDate.Substring(3, 2)
                        txtIYY.Text = sIssueDate.Substring(6, 4)
                    End If
                    If sOrderDate <> "" Then
                        txtODD.Text = sOrderDate.Substring(0, 2)
                        txtOMM.Text = sOrderDate.Substring(3, 2)
                        txtOYYYY.Text = sOrderDate.Substring(6, 4)
                    End If
                    txtAdvancePaid.Text = Format(Val(DR(0)("AdvancePaid")), "0.00")

                    SubLoadRepairDetail()
                    txtAdvancePaid.Enabled = False
                    If Val(txtBalanceAmount.Text) <= Val(txtAdvancePaid.Text) Then
                        btnCancel.Enabled = False
                        btnSave.Enabled = False
                    Else
                        btnCancel.Enabled = True
                        btnSave.Enabled = True
                    End If
                Else
                    MsgBox("Please Give the Corect Repair Number", MsgBoxStyle.Information)
                    txtInvoiceNo.Text = ""
                    ClearAll()
                    txtInvoiceNo.Focus()
                End If

            End If
            Exit Sub

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Function SubLoadRepairDetail()
        Dim DR As DataRow()
        Dim sSql As String
        Dim dTotalWeightEstimated, dTotalEstimatedCost As Double
        Dim i
        dTotalWeightEstimated = 0
        dTotalEstimatedCost = 0

        sSql = "select * from [tblInventoryRepairDetail] where RepairNo=" & fncFormatSQLText(Trim(txtInvoiceNo.Text.Trim))
        DR = DataManager.RowReturnProjExecNonQryWithDataRow(sSql)
        grdOrder.RowCount = 0
        For i = 0 To DR.Length - 1
            grdOrder.Rows.Add()
            fnSetdgvText(grdOrder, i, 0, Trim(DR(i)("Description") & ""))
            fnSetdgvText(grdOrder, i, 1, Val(DR(i)("WeightEstimated") & ""))
            dTotalWeightEstimated = dTotalWeightEstimated + Val(DR(i)("WeightEstimated") & "")
            fnSetdgvText(grdOrder, i, 2, Val(DR(i)("EstimatedCost") & ""))
            dTotalEstimatedCost = dTotalEstimatedCost + Val(DR(i)("EstimatedCost") & "")
        Next
        txtEstiWeight.Text = Format(dTotalWeightEstimated, "0.00")
        txtEstiTotalCost.Text = Format(dTotalEstimatedCost, "0.00")
        txtBalanceAmount.Text = Format(Val(txtEstiTotalCost.Text) - Val(txtAdvancePaid.Text), "0.00")

    End Function

    Private Sub cmdAdd_Click(sender As System.Object, e As System.EventArgs) Handles cmdAdd.Click
        subAddToGridOrder()
    End Sub

    Private Sub cmdDel_Click(sender As System.Object, e As System.EventArgs) Handles cmdDel.Click
        If grdOrder.RowCount = 0 Then
            MsgBox("No Item for deletion.", vbCritical)
            Exit Sub
        End If
        grdOrder.Rows.RemoveAt(grdOrder.CurrentRow.Index)
        'If grdOrder.RowCount = 1 Then
        '    grdOrder.RowCount = 0
        'Else
        subCalcTotal()
        'End If
    End Sub

    Private Sub cmdClr_Click(sender As System.Object, e As System.EventArgs) Handles cmdClr.Click
        cboRepairDescription.Text = ""
        txtInvWeight.Text = ""
        txtCost.Text = ""
    End Sub

    Private Sub btnOrderPaymentNoSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnOrderPaymentNoSearch.Click
        Try
            Dim frmHelpOrder As New frmHelpSearch(0, 0, "tblinventoryRepairHeader", "L", 4, "RepairNo", "(Status = 'R' or Status = 'P') and AdvancePaid < TotalCostEstimated ", "RepairNo", "RepairNo", 100, "RepairDate", "ReapirDate", 180, "CustomerCode", "CustomerCode", 60, "SupplierCode", "SupplierCode", 80)
            frmHelpOrder.ShowDialog()
            txtInvoiceNo.Text = frmHelpOrder.Var1.Trim
            If txtInvoiceNo.Text.Trim <> "" Then txtInvoiceNo_Leave(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnTradeIn_Click(sender As System.Object, e As System.EventArgs) Handles btnTradeIn.Click
        Dim TradeIn As New frmTradeInGold
        'subGetCompany()
        TradeIn.sCalledFrom = "ORDERFORM"
        TradeIn.sInvoiceNo = Format(Val(sNextOrderNo), "00000000")
        TradeIn.txtCustomerCode.Text = txtCustomerCode.Text
        TradeIn.txtCustomerName.Text = txtCustomerName.Text
        TradeIn.lblCustomerType.Text = ""
        TradeIn.txtAddress1.Text = txtAddress1.Text
        TradeIn.txtAddress2.Text = txtAddress2.Text
        TradeIn.txtAddress3.Text = txtAddress3.Text
        TradeIn.lblTradeInLimit.Text = dTradeinLimit
        TradeIn.lblSalesAmount.Text = Val(txtEstiTotalCost.Text)
        TradeIn.sSalesAmount = Val(txtEstiTotalCost.Text)
        'TradeIn.txtCostPerGram.Text = Val(txtCurrCostPerGram.Text)
        TradeIn.ShowDialog()
        If optBoth.Checked = True Then
            txtTradeINAmount.Text = dTradeinAmount
            txtAdvancePaid.Text = "0.00"
            txtBalanceAmount.Text = Val(txtEstiTotalCost.Text) - Val(txtTradeINAmount.Text) - Val(txtAdvancePaid.Text)
        ElseIf optGold.Checked = True Then
            txtTradeINAmount.Text = dTradeinAmount
            txtAdvancePaid.Text = dTradeinAmount
            txtBalanceAmount.Text = Val(txtEstiTotalCost.Text) - Val(txtAdvancePaid.Text)
        End If
        ' txtAdvancePaid.Text = dTradeinAmount
        'txtBalanceAmount.Text = Val(txtEstiTotalCost.Text) - Val(txtAdvancePaid.Text)
        tradeInRefNo = sTradeInRefNo
        subAddTradeInToGrid()
        If ChkTradeIn Then
            btnTradeIn.Enabled = False
            optCash.Enabled = False
            txtAdvancePaid.Enabled = True
        Else
            btnTradeIn.Enabled = True
            optCash.Enabled = True
            txtAdvancePaid.Enabled = True
        End If
        sType = "TRADEIN"
    End Sub
    Private Sub optGold_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optGold.CheckedChanged
        If optGold.Checked = True Then
            btnTradeIn.Visible = True
        Else
            btnTradeIn.Visible = False
        End If
    End Sub

    Private Sub optCash_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optCash.CheckedChanged
        If optCash.Checked = True Then
            btnTradeIn.Visible = False
        Else
            btnTradeIn.Visible = True
        End If
    End Sub

    Private Sub optBoth_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optBoth.CheckedChanged
        If optBoth.Checked = True Then
            btnTradeIn.Visible = True
        Else
            btnTradeIn.Visible = False
        End If
    End Sub
    Private Sub subAddTradeInToGrid()
        Dim sTiInventoryCode As String
        Dim sTiClassCode As String
        Dim sTiDescription As String
        Dim sTiWeight As String
        Dim sTiGoldLoss As String
        Dim sTiCostPerGram As String
        Dim sTiStoneWeight As String
        Dim sTiSmc As String
        Dim sTiPurMC As String
        Dim sTiPCS As String
        Dim sTiTotalValue As String
        Dim sTiTotalCost As String
        Dim sTiSalesType As String
        Dim sTiGST As String
        Dim iRow As Long
        Dim i As Long

        Try
            dTradeInSales = 0
            For iRow = 0 To dsTradeIn.Tables(0).Rows.Count - 1
                grdSales.ForeColor = Color.Black

                'If Not bEdited Then
                '    If grdSales.MaxRows = 0 Then
                '        grdSales.MaxRows = grdSales.MaxRows + 1
                '    Else
                '        grdSales.Row = grdSales.MaxRows
                '        grdSales.Action = 5
                '        grdSales.MaxRows = grdSales.MaxRows - 1
                '        grdSales.MaxRows = grdSales.MaxRows + 1
                '    End If
                '    grdSales.Row = grdSales.MaxRows
                'Else
                '    grdSales.Row = grdSales.ActiveRow
                'End If
                If Not bEdited Then
                    If grdSales.RowCount = 0 Then
                        grdSales.Rows.Add()
                    Else
                        'grdSales.Row = grdSales.MaxRows
                        'grdSales.Action = 5
                        'grdSales.MaxRows = grdSales.MaxRows - 1
                        'grdJobSheet.MaxRows = grdJobSheet.MaxRows + 1
                        grdSales.Rows.Add()
                    End If
                    i = grdSales.RowCount - 1
                Else
                    i = grdSales.CurrentRow.Index
                End If
                sTiInventoryCode = dsTradeIn.Tables(0).Rows(iRow).Item("ItemNo") & "".Trim
                sTiClassCode = dsTradeIn.Tables(0).Rows(iRow).Item("sClass") & "".Trim
                sTiDescription = dsTradeIn.Tables(0).Rows(iRow).Item("sDesc") & "".Trim
                sTiWeight = dsTradeIn.Tables(0).Rows(iRow).Item("sWeight") & "".Trim
                sTiGoldLoss = dsTradeIn.Tables(0).Rows(iRow).Item("sGoldLoss") & "".Trim
                sTiCostPerGram = dsTradeIn.Tables(0).Rows(iRow).Item("sCostPerGram") & "".Trim
                sTiStoneWeight = dsTradeIn.Tables(0).Rows(iRow).Item("sStoneWeight") & "".Trim
                sTiSmc = "0"
                sTiPurMC = dsTradeIn.Tables(0).Rows(iRow).Item("sPMC") & "".Trim
                sTiPCS = dsTradeIn.Tables(0).Rows(iRow).Item("sPCS") & "".Trim
                'sTiTotalValue = (Val(sTiWeight) + Val(sTiGoldLoss)) * Val(sTiCostPerGram)
                sTiTotalValue = dsTradeIn.Tables(0).Rows(iRow).Item("sTotalCost") & "".Trim
                'sTiTotalValue = dsTradeIn.Tables(0).Rows(iRow).Item("sTotalCost") & "".Trim
                sTiTotalCost = dsTradeIn.Tables(0).Rows(iRow).Item("sTotalCost") & "".Trim
                sTiSalesType = "TI"

           
                If UCase(sCompanyShortCode) = "MJ" Or UCase(sCompanyShortCode) = "SSJ" Or UCase(sCompanyShortCode) = "AJ" Or UCase(sCompanyShortCode) = "MD" Then
                    sTiTotalValue = Format(Val(sTiTotalValue) + 0, "0.00")
                    dTradeInGST = dTradeInGST + Val(-Format$((Val(sTiTotalValue) * dGSTPerc) / (107), "#0.00"))
                    dTradeInSales = dTradeInSales + Format(Val(sTiTotalValue) + 0, "0.00")
                Else
                    sTiGST = Format$((Val(sTiTotalValue) * dGSTPerc) / (100), "#0.00")
                    sTiTotalValue = Format(Val(sTiTotalValue) + Val(sTiGST), "0.00")
                End If
                
                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(ICode).Style.ForeColor = Color.Red
                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(ICode).Style.Font = New Font(grdSales.Font.FontFamily, 9, FontStyle.Bold)

                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(IWeight).Style.ForeColor = Color.Red
                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(IWeight).Style.Font = New Font(grdSales.Font.FontFamily, 9, FontStyle.Bold)

                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(IGoldLoss).Style.ForeColor = Color.Red
                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(IGoldLoss).Style.Font = New Font(grdSales.Font.FontFamily, 9, FontStyle.Bold)

                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(ISMC).Style.ForeColor = Color.Red
                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(ISMC).Style.Font = New Font(grdSales.Font.FontFamily, 9, FontStyle.Bold)

                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(IPCS).Style.ForeColor = Color.Red
                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(IPCS).Style.Font = New Font(grdSales.Font.FontFamily, 9, FontStyle.Bold)

                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(ITotalValue).Style.ForeColor = Color.Red
                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(ITotalValue).Style.Font = New Font(grdSales.Font.FontFamily, 9, FontStyle.Bold)

                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(IDSWeight).Style.ForeColor = Color.Red
                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(IDSWeight).Style.Font = New Font(grdSales.Font.FontFamily, 9, FontStyle.Bold)

                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(ITotalCost).Style.ForeColor = Color.Red
                Me.grdSales.Rows(grdSales.RowCount - 1).Cells(ITotalCost).Style.Font = New Font(grdSales.Font.FontFamily, 9, FontStyle.Bold)

                Call fnSetdgvText(grdSales, i, ICode, sTiInventoryCode)
                Call fnSetdgvText(grdSales, i, IClass, sTiClassCode)
                Call fnSetdgvText(grdSales, i, IDescription, sTiDescription)
                Call fnSetdgvText(grdSales, i, IWeight, Val(sTiWeight))
                Call fnSetdgvText(grdSales, i, IGoldLoss, Val(sTiGoldLoss))
                Call fnSetdgvText(grdSales, i, ICostPerGram, Val(sTiCostPerGram))
                Call fnSetdgvText(grdSales, i, IDSWeight, Val(sTiStoneWeight))
                Call fnSetdgvText(grdSales, i, ISMC, Val(sTiSmc))
                Call fnSetdgvText(grdSales, i, IPMC, Val(sTiPurMC))
                Call fnSetdgvText(grdSales, i, IPCS, Val(sTiPCS))
                Call fnSetdgvText(grdSales, i, ITotalValue, Val(-sTiTotalValue))
                Call fnSetdgvText(grdSales, i, ITotalCost, Val(sTiTotalCost))
                Call fnSetdgvText(grdSales, i, ISalesType, sTiSalesType)
                Call fnSetdgvText(grdSales, i, IGST, Val(-sTiGST))
                Call fnSetdgvText(grdSales, i, IDiscount, Val(0))


                'subTotalFromGrid()
                'subTotal()
                'subClearDetail()

                bEdited = False
            Next iRow

            dsTradeIn.Tables(0).Rows.Clear()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Function fncSaveTradeInSales(ByVal sInvoiceNo As String) As Boolean
        Dim objTI As New clsTradeIN
        Dim sTIinventorycode As String
        Dim sTIInvoiceNo As String
        Dim iCnt As Long
        Dim sSql As String
        Dim nWeight As Decimal
        Dim nStoneWeight As Decimal
        Dim nSMC As Decimal
        Dim nPCS As Decimal
        Dim nGoldLoss As Decimal
        Dim nTotCost As Decimal
        Dim nTotalAmt As Decimal
        Dim bStatus As Boolean
        Dim bHaveTradeIn As Boolean
        Dim i As Long
        Try
            bStatus = False
            bHaveTradeIn = False
            For iCnt = 0 To grdSales.RowCount - 1
                If fnGetdgvText(grdSales, iCnt, ISalesType).Trim = "TI" Then
                    nWeight = nWeight + Val(fnGetdgvText(grdSales, iCnt, IWeight))
                    nStoneWeight = nStoneWeight + Val(fnGetdgvText(grdSales, iCnt, IDSWeight))
                    nSMC = nSMC + Val(fnGetdgvText(grdSales, iCnt, ISMC))
                    nPCS = nPCS + Val(fnGetdgvText(grdSales, iCnt, IPCS))
                    nGoldLoss = nGoldLoss + Val(fnGetdgvText(grdSales, iCnt, IGoldLoss))
                    nTotCost = nTotCost + Val(fnGetdgvText(grdSales, iCnt, ITotalCost))
                    nTotalAmt = nTotalAmt + Val(fnGetdgvText(grdSales, iCnt, ITotalValue))
                    bHaveTradeIn = True
                End If
            Next

            If bHaveTradeIn Then
                'sTIInvoiceNo = objTI.fncGetNextTrandeInRefNo()
                'sTIinventorycode = "TI-HQ" & sTIInvoiceNo
                'bStatus = objTI.fncUpdateNextTradeInRefNo()
                'If Not bStatus Then
                '    Exit Function
                'End If
                ' sTIinventorycode = "TI-" & sInvoiceNo
                'bStatus = objTI.TradeInDeleteSales(Trim(sTIinventorycode))
                'If Not bStatus Then
                '    Exit Function
                'End If

                ' sSalesType = "TRADEIN"

                bStatus = objTI.TradeInHeaderSaveSales(Trim(sTIinventorycode), Trim$(sInvoiceNo), _
                          gthisLocation, UCase(Trim(txtCustomerCode.Text)), "", Trim(txtCustomerName.Text), Trim(txtAddress1.Text), _
                          Trim(txtAddress2.Text), Trim(txtAddress3.Text), _
                          Val(0), Val(nWeight), _
                          Val(nStoneWeight), Val(nSMC), Val(nPCS), _
                          Val(nGoldLoss), Val(nTotCost), _
                          -Val(nTotalAmt), "TRADEIN", "N", sUserID)
                If Not bStatus Then
                    Exit Function
                End If
                i = 0
                For iCnt = 0 To grdSales.RowCount - 1
                    If fnGetdgvText(grdSales, iCnt, ISalesType).Trim.ToUpper = "TI" Then
                        sTIinventorycode = "TI-" & sInvoiceNo & "-" & i + 1
                        Call fnSetdgvText(grdSales, iCnt, 0, sTIinventorycode)
                        bStatus = objTI.TradeInDetailSaveSales(gthisLocation, Trim(sTIinventorycode), iCnt, Trim(fnGetdgvText(grdSales, iCnt, IDescription)), _
                                "", Val(fnGetdgvText(grdSales, iCnt, IWeight)), Val(fnGetdgvText(grdSales, iCnt, IDSWeight)), _
                                Val(fnGetdgvText(grdSales, iCnt, ICostPerGram)), Val(fnGetdgvText(grdSales, iCnt, ISMC)), Val(fnGetdgvText(grdSales, iCnt, IPCS)), _
                                Val(fnGetdgvText(grdSales, iCnt, IGoldLoss)), Val(fnGetdgvText(grdSales, iCnt, ITotalCost)), "TRADEIN", "TRADEIN", "TRADEIN", "TRADEIN", Trim(fnGetdgvText(grdSales, iCnt, IClass)), Trim$(sInvoiceNo), sUserID)
                        If Not bStatus Then
                            Exit Function
                        End If
                        i = i + 1
                    End If
                Next
            Else
                bStatus = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncSaveTradeInSales = bStatus
        End Try
    End Function

    Private Sub txtSalesManPWD_LostFocus(sender As Object, e As System.EventArgs) Handles txtSalesManPWD.LostFocus
        Try
            If txtSalesManPWD.Text.Trim <> "" Then
                subGetPassword()
            Else
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Private Sub subGetPassword()
        Dim i As Long
        Dim Ds As DataSet
        Dim objSalesMan As New clsSalesMan
        Try

            Ds = objSalesMan.GetSalesManwithPassword(txtSalesManPWD.Text.Trim)
            If Ds.Tables("Salesman").Rows.Count > 0 Then
                msSalesManCode = Trim(Ds.Tables("Salesman").Rows(0)("SalesmanCode") & "")
            Else
                MsgBox("SalesMan Password is Incorrect!", MsgBoxStyle.Critical)
                txtSalesManPWD.Text = ""
                txtSalesManPWD.Focus()
                Exit Sub
            End If

            PnlSalesManPwd.Visible = False

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub btnRePrint_Click(sender As System.Object, e As System.EventArgs) Handles btnRePrint.Click
        Try
            PnlReprint.Visible = True
            txtRePrintBillNo.Clear()
            txtRePrintBillNo.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnReprintCancel_Click(sender As Object, e As System.EventArgs) Handles btnReprintCancel.Click
        Try
            txtRePrintBillNo.Clear()
            PnlReprint.Visible = False
            txtInvoiceNo.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnReprintOK_Click(sender As Object, e As System.EventArgs) Handles btnReprintOK.Click
        Try
            If Trim(txtRePrintBillNo.Text) <> "" Then
                bReprint = True
                'Call subPrintBill(Trim(txtRePrintBillNo.Text))
                Call subPrintRepair(Trim(txtRePrintBillNo.Text))
                bReprint = False
                PnlReprint.Visible = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

  
End Class