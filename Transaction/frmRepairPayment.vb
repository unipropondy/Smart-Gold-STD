Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmRepairPayment
    Dim objCust As New clsCustomer
    Dim objOrder As New clsRepair
    Dim objComp As New clsCompany
    Dim objClassRate As New ClsClass
    Dim DM As New DataManager
    Dim msOrderpaymentNo As String
    Dim sInvoiceno As String
    Dim objSales As New ClsSales
    Dim CompName$, Address1$, Address2$, Country$, PhoneNo$, FaxNo$
    Dim bReprint As Boolean
    Dim s24K As String
    Dim s22K As String
    Dim s20K As String
    Dim s22S As String
    Dim s999 As String
    Dim s916 As String
    Dim s835 As String
    Dim s24K999 As String
    Dim s20K850 As String
    Dim s22KSOV As String
    Dim sInvGstType As String
    Dim tradeInRefNo As String
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

    Dim sBarLocationCode, sBarInvoiceNo, sBarInvoiceDate, sBarClassCode As String
    Dim dBarWeight, dBarStoneWeight, dBarTotalCost As Double
    Dim sType As String
    Private Sub btnOrderPaymentNoSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnOrderPaymentNoSearch.Click
        Try
            Dim frmHelpOrder As New frmHelpSearch(0, 0, "tblinventoryRepairHeader", "L", 4, "RepairNo", "(Status = 'R' or Status = 'P') and AdvancePaid < TotalCostEstimated ", "RepairNo", "RepairNo", 100, "RepairDate", "ReapirDate", 180, "CustomerCode", "CustomerCode", 60, "SupplierCode", "SupplierCode", 80)
            frmHelpOrder.ShowDialog()
            txtOrderNo.Text = frmHelpOrder.Var1.Trim
            If txtOrderNo.Text.Trim <> "" Then subGetOrderDetails()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub subGetOrderDetails()
        Dim dsOH As New DataSet
        Dim dsOP As New DataSet
        Dim drD As DataRow()
        Dim iCnt As Integer
        Dim sSql As String
        Dim dPaymentAmount As Decimal
        Try
            grdOrderPayment.RowCount = 0
            dPaymentAmount = 0
            dsOH = objOrder.GetOrderHeader(txtOrderNo.Text.Trim)
            If dsOH.Tables(0).Rows.Count > 0 Then
                txtCustomerCode.Text = Trim(dsOH.Tables(0).Rows(0)("CustomerCode") & "")
                If Not txtCustomerCode.Text.Trim = "" Then subGetCustomer()
                lblAdvancePaid.Text = Format(Val(dsOH.Tables(0).Rows(0)("AdvancePaid")), "#0.00")
                lblEstimatedCost.Text = Format(Val(dsOH.Tables(0).Rows(0)("TotalCostEstimated")), "#0.00")
                objOrder.GetOrderPaymentDetail(txtOrderNo.Text.Trim)
                sSql = " select * from tblRepairPaymentDetail  where RepairNo = " & fncFormatSQLText(txtOrderNo.Text.Trim)
                drD = DataManager.RowReturnProjExecNonQryWithDataRow(sSql)

                If drD.Length > 0 Then
                    For iCnt = 0 To drD.Length - 1
                        grdOrderPayment.Rows.Add()

                        fnSetdgvText(grdOrderPayment, iCnt, 0, Trim(drD(iCnt)("RepairNo") & ""))
                        fnSetdgvText(grdOrderPayment, iCnt, 1, Trim(drD(iCnt)("RepairPaymentNo") & ""))

                        fnSetdgvText(grdOrderPayment, iCnt, 2, Trim(drD(iCnt)("Remarks") & ""))
                        fnSetdgvText(grdOrderPayment, iCnt, 3, Trim(drD(iCnt)("AmountPaid") & ""))


                    Next iCnt
                    ' subTotal()
                    drD = Nothing
                Else
                    dPaymentAmount = 0
                End If
                lblBalanceAmount.Text = Format(Val(lblEstimatedCost.Text) - (Val(lblAdvancePaid.Text) + dPaymentAmount), "#0.00")
                txtPayAmount.Focus()
            Else
                MessageBox.Show("Could not find the RepairNo.")
                subClearMe()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsOH = Nothing
        End Try
    End Sub
    Private Sub subGetCustomer()
        Dim dsCust As New DataSet
        Try
            dsCust = objCust.GetCustomerwithCode(Trim(txtCustomerCode.Text))
            If dsCust.Tables(0).Rows.Count > 0 Then
                txtCustomerName.Text = Trim(dsCust.Tables(0).Rows(0)("CustomerName") & "")
                txtAddress1.Text = Trim(dsCust.Tables(0).Rows(0)("Address1") & "")
                txtAddress2.Text = Trim(dsCust.Tables(0).Rows(0)("Address2") & "")
                txtAddress3.Text = Trim(dsCust.Tables(0).Rows(0)("Address3") & "")
                lblCustomerType.Text = Trim(dsCust.Tables(0).Rows(0)("CustomerType") & "")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subClearMe()
        Try
            txtOrderNo.Clear()
            txtCustomerCode.Clear()
            txtCustomerName.Clear()
            txtAddress1.Clear()
            txtAddress2.Clear()
            txtAddress3.Clear()
            txtRemarks.Clear()
            lblCustomerType.Text = ""
            lblBalanceAmount.Text = "0.00"
            lblAdvancePaid.Text = "0.00"
            lblEstimatedCost.Text = "0.00"
            lblDepositAmount.Text = "0.00"
            txtPayAmount.Clear()
            btnTradeIn.Enabled = True
            optCash.Enabled = True
            grdOrderPayment.RowCount = 0
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmRepairPayment_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Escape
                    If btnClose.Enabled Then btnClose_Click(sender, e)
                Case Keys.Enter
                    SendKeys.Send("{TAB}")
                Case Keys.F6
                    If btnClear.Enabled Then btnClear_Click(sender, e)
                Case Keys.F7
                    If btnSave.Enabled Then btnSave_Click(sender, e)
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        subClearMe()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Cursor.Current = Cursors.WaitCursor
        Dim frmPM As New frmPayingmode
        Try
            If fncValidateMe() Then
                objOrder.OpenRepairConnection()
                objOrder.gpBeginRepairTransaction()
                objSales.subOpenSalesConnection()
                objSales.subBeginSalesTransaction()
                msOrderpaymentNo = Trim("RP" & objOrder.fncGetNextOrderPaymentNo())
                dPaidAmount = 0
                ReturnAmount = 0
                nTotalNet = Val(txtPayAmount.Text) - Val(dTradeInSales)
                frmPM.SalesType = ""
                frmPM.ShowDialog()
                If dPaidAmount > 0 Then
                    If Not fncPaymodeDetailSave(txtOrderNo.Text.Trim) Then
                        MessageBox.Show("Transaction not saved")
                        objOrder.gpRollBackRepairConnection()
                    ElseIf Not fncSaveOrderPaymentDetail() Then
                        MessageBox.Show("Transaction not saved")
                        objOrder.gpRollBackRepairConnection()
                        'ElseIf Not subChecktheAmount() Then
                        '    MessageBox.Show("Transaction not saved")
                        '    objOrder.gpRollBackRepairConnection()
                    ElseIf Not fncSaveTradeInSales(txtOrderNo.Text.Trim) Then
                        MessageBox.Show("Error in save Detail")
                        objSales.subRollBackSalesTransaction()

                        Exit Sub
                    Else
                        objOrder.gpCommitRepairTransaction()
                        objOrder.CloseRepairConnection()
                        objSales.subCommitSalesTransaction()
                        objSales.subCloseSalesConnection()
                        MessageBox.Show("Repair payment saved successfully.")
                        ' Call subPrintBill(Trim(sInvoiceno))
                        Call subPrintRepair(Trim(txtOrderNo.Text))
                        If Trim(sType) = "TRADEIN" And grdSales.RowCount > 0 Then
                            Call subGetBarInventory(txtOrderNo.Text.Trim)
                        End If

                        subClearMe()
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            frmPM = Nothing
            objOrder.CloseRepairConnection()
            objSales.subCloseSalesConnection()
            Cursor.Current = Cursors.Default
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
            CR.DataDefinition.FormulaFields("Salesman").Text = ""
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

    Private Function fncValidateMe() As Boolean
        Dim bValid As Boolean
        Try
            bValid = False
            If txtOrderNo.Text.Trim = "" Then
                MessageBox.Show("Please enter OrderNo.")
                txtOrderNo.Focus()
                Exit Function
            End If
            If Val(txtPayAmount.Text.Trim) = 0 Then
                MessageBox.Show("Please enter pay amount.")
                txtPayAmount.Focus()
                Exit Function
            End If
            If Not IsNumeric(txtPayAmount.Text.Trim) Then
                MessageBox.Show("Please enter valid Pay Amount.")
                txtPayAmount.Clear()
                txtPayAmount.Focus()
                Exit Function
            End If
            If Val(txtPayAmount.Text.Trim) < 0 Then
                MessageBox.Show("Pay amount should  be greater than zero.")
                txtPayAmount.Clear()
                txtPayAmount.Focus()
                Exit Function
            End If
            'If Val(txtPayAmount.Text.Trim) > Val(lblBalanceAmount.Text) Then
            '    MessageBox.Show("Pay amount is greater than balance amount.")
            '    'txtPayAmount.Text = Format(Val(lblBalanceAmount.Text), "#0.00")
            '    txtPayAmount.Focus()
            '    Exit Function
            'End If
            bValid = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncValidateMe = bValid
        End Try
    End Function

    Private Sub btnClose_Click(sender As Object, e As System.EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub
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
                                            Val(txtPayAmount.Text), 0, _
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

    Public Function subChecktheAmount() As Boolean
        Dim dsInsHdr As New DataSet
        Dim dTotalAmountPaid As Double
        subChecktheAmount = False

        Try
            If txtOrderNo.Text.Trim = "" Then Exit Function
            dsInsHdr = objOrder.GetInstallmentBalance(Trim$(txtOrderNo.Text))
            If dsInsHdr.Tables(0).Rows.Count > 0 Then
                dTotalAmountPaid = Format(Val(dsInsHdr.Tables(0).Rows(0)("AmountPaid") & "") + Val(lblDepositAmount.Text), "0.00")
                If Val(dTotalAmountPaid) >= Val(lblEstimatedCost.Text) Then
                    lblEstimatedCost.Text = Val(dTotalAmountPaid)
                    SubAddHeaderIntoSales()
                    subAddDetailsIntoSales()
                    subAddPaymodedetails()
                End If
            End If
            subChecktheAmount = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function
    Private Function fncSaveOrderPaymentDetail() As Boolean
        Try
            Dim iRet As Boolean
            Dim i As Long
            Dim sRemarks$
            Dim sSql$
            iRet = objOrder.FncOrderPaymentSave(txtOrderNo.Text.Trim, msOrderpaymentNo.Trim, Val(txtPayAmount.Text.Trim), txtRemarks.Text.Trim, Val(dTradeInSales), Val(dTradeInGST))
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
    Public Function subAddPaymodedetails()
        Dim objPM As New clsPayMode
        Try
            Dim bPayModeFlag As Boolean
            Dim sDate As String
            Dim i As Long
            bPayModeFlag = False
            sDate = Format(Now, "dd/MM/yyyy")
            If bPayModeFlag = False Then
                iRet = objPM.PayModeDetailSave(sInvoiceno, sDate, "REPAIR", _
                                            Format(Val(lblEstimatedCost.Text), "0.00"), 0, _
                                            "MANUAL", "REPAIR")
                If Not iRet Then
                    MessageBox.Show("Error in InstallmentSave")
                    subAddPaymodedetails = False
                    Exit Function
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        'Dim objPM As New clsPayMode
        'Try
        '    Dim bPayModeFlag As Boolean
        '    Dim sDate As String
        '    Dim i As Long
        '    bPayModeFlag = False
        '    sDate = Format(Now, "dd/MM/yyyy")
        '    For i = 0 To UBound(arrpay, 1)
        '        If arrpay(i, 1) > 0 Then
        '            iRet = objPM.PayModeDetailSave(sInvoiceno, sDate, Trim(arrpay(i, 3)), _
        '                                         Val(arrpay(i, 1)), Val(arrpay(i, 2)), _
        '                                         Trim(arrpay(i, 4)), "REPAIR")
        '            bPayModeFlag = True
        '            If Not iRet Then
        '                subAddPaymodedetails = False
        '                Exit Function
        '            End If

        '        End If
        '    Next i
        '    If bPayModeFlag = False Then
        '        iRet = objPM.PayModeDetailSave(sInvoiceno, sDate, "CASH", _
        '                                    Val(txtPayAmount.Text), 0, _
        '                                    "MANUAL", "REPAIR")
        '        If Not iRet Then
        '            subAddPaymodedetails = False
        '            Exit Function
        '        End If
        '    End If
        '    subAddPaymodedetails = True
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
    End Function
    Public Function subAddDetailsIntoSales()
        Dim InvoiceNo, InventoryCode, Description, Locationcode, InsAgreementNo As String
        Dim Goldloss, Cost, PCS, SMC, GST, TotalValue As Double
        Dim ds1 As New DataSet
        Dim dm As New DataManager
        Dim sSql As String
        Dim iret As Boolean
        Dim icnt As Long
        ' sSql = "Select * from tblInstallmentEntryDetail where InsAgreementNo=" & fncFormatSQLText(txtOrderNo.Text)
        sSql = "Select * from tblInventoryRepairDetail where RepairNo=" & fncFormatSQLText(txtOrderNo.Text)
        ds1 = dm.getDataSetWithSqlAdapter(sSql, "tblInstallmentEntryDetail", "tblInstallmentEntryDetail")
        For icnt = 0 To ds1.Tables("tblInstallmentEntryDetail").Rows.Count - 1
            InventoryCode = "REPAIR"
            Description = Trim(ds1.Tables("tblInstallmentEntryDetail").Rows(icnt)("Description"))
            Locationcode = Trim(gthisLocation)
            InsAgreementNo = Trim(ds1.Tables("tblInstallmentEntryDetail").Rows(icnt)("RepairNo"))
            Goldloss = 0
            Cost = Val(Trim(ds1.Tables("tblInstallmentEntryDetail").Rows(icnt)("EstimatedCost")))
            PCS = 1 'Val(Trim(ds1.Tables("tblInstallmentEntryDetail").Rows(icnt)("PCS")))
            SMC = 0 'Val(Trim(ds1.Tables("tblInstallmentEntryDetail").Rows(icnt)("SMC")))
            GST = Val(Trim(ds1.Tables("tblInstallmentEntryDetail").Rows(icnt)("EstimatedCost"))) * 0.07
            TotalValue = Val(Trim(ds1.Tables("tblInstallmentEntryDetail").Rows(icnt)("EstimatedCost")))

            iret = objOrder.SalesDetailSave(sInvoiceno, InventoryCode, Description, Trim(sLocationCode), 0, 0, Goldloss, 0, SMC, PCS, 0, 0, GST, TotalValue, 0, TotalValue, TotalValue, 0, "", 0, 0, 0, "INSTALLMENT")

            If Not iret Then
                MessageBox.Show("Error in InstallmentSave")
                subAddDetailsIntoSales = False
                Exit Function
            End If

        Next

    End Function
    Public Function SubAddHeaderIntoSales()
        Dim sCustomerCode, sName, sAddress1, sAddress2, sAddress3, sSalesManCode, sSalesType, sGstType As String
        Dim dTotalWeight, dTotalStoneWeight, dTotalGoldLoss, dTotalSMC, dGST, dDetailDiscont, dNetDiscount, dDiscount, dTotalCost, dTotalPCS As Double
        Dim dr1 As DataRow()
        Dim dm As DataManager
        Dim dsInvoiceno As DataSet

        Dim sSql, sInvDate As String
        Dim iret As Boolean

        sInvDate = CStr(Format(CDate(ServerDate), "yyyy") & "/" & Format(CDate(ServerDate), "MM") & "/" & Format(CDate(ServerDate), "dd"))
        dsInvoiceno = objSales.GetUpdateInvoice()
        If dsInvoiceno.Tables(0).Rows.Count > 0 Then
            sInvoiceno = Trim(dsInvoiceno.Tables(0).Rows(0)("NextInvoice") & "")
        End If
        ''sSql = "select a.customerCode as CustomerCode,a.name as Name,a.address1 as Address1,a.address2 as Address2, "
        ''sSql = sSql & " a.address3 as Address3,a.salesman as SalesManCode,'INSTALLMENT' as SalesType,sum(b.weight) as TotalWeight, "
        ''sSql = sSql & " 0 as TotalStoneWeight,sum(b.goldloss)as TotalGoldLoss,sum(b.smc)as TotalSMC,'I' as GSTType, "
        ''sSql = sSql & " sum(b.gst)as GST,0 as DetailDiscount,0 as NetDiscount,0 as Discount,a.totalcost as TotalCost,(select Count(*) from tblinstallmententrydetail where insagreementno=" & fncFormatSQLText(txtOrderNo.Text) & ")as TotalPCS "
        ''sSql = sSql & " from tblinstallmententryheader a,tblinstallmententrydetail b "
        ''sSql = sSql & " where a.insagreementno=b.insagreementno and a.insagreementno=" & fncFormatSQLText(txtOrderNo.Text)
        ''sSql = sSql & " group by a.customerCode,a.name,a.address1,a.address2,a.address3,a.salesman,a.totalcost"



        sSql = "Select a.customerCode as CustomerCode,c.CustomerName as Name,c.address1 as Address1,c.address2 as Address2,  c.address3 as Address3,"
        sSql = sSql & " '' as SalesManCode,'REPAIR' as SalesType,sum(b.WeightEstimated) as TotalWeight,  0 as TotalStoneWeight,"
        sSql = sSql & " 0 as TotalGoldLoss,0 as TotalSMC,'I' as GSTType,  AdvancePaid*.07 as GST,0 as DetailDiscount,"
        sSql = sSql & " 0 as NetDiscount,0 as Discount,a.AdvancePaid as TotalCost,(select Count(*) from tblInventoryRepairDetail where "
        sSql = sSql & " RepairNo=" & fncFormatSQLText(txtOrderNo.Text) & ")as TotalPCS  from tblInventoryRepairHeader a,tblInventoryRepairDetail b,tblcustomer c  "
        sSql = sSql & " where a.RepairNo=b.RepairNo and a.CustomerCode=c.CustomerCode and  a.RepairNo=" & fncFormatSQLText(txtOrderNo.Text)
        sSql = sSql & " group by a.customerCode,c.CustomerName,c.address1,c.address2,c.address3,a.AdvancePaid"

        dr1 = DataManager.RowReturnProjExecNonQryWithDataRow(sSql)
        If dr1.Length > 0 Then
            sCustomerCode = Trim(dr1(0)("CustomerCode"))
            sName = Trim(dr1(0)("Name"))
            sAddress1 = Trim(dr1(0)("Address1"))
            sAddress2 = Trim(dr1(0)("Address2"))
            sAddress3 = Trim(dr1(0)("Address3"))
            sSalesManCode = Trim(dr1(0)("SalesManCode"))
            sSalesType = Trim(dr1(0)("SalesType"))
            dTotalWeight = Val(dr1(0)("TotalWeight"))
            dTotalStoneWeight = Val(dr1(0)("TotalStoneWeight"))

            dTotalGoldLoss = Val(dr1(0)("TotalGoldLoss"))

            dTotalSMC = Val(dr1(0)("TotalSMC"))
            dTotalPCS = Val(dr1(0)("TotalPCS"))
            sGstType = Trim(dr1(0)("GSTType"))

            dGST = Val(dr1(0)("GST"))
            dDetailDiscont = Val(dr1(0)("DetailDiscount"))

            dDiscount = Val(dr1(0)("NetDiscount"))
            dTotalCost = Val(dr1(0)("TotalCost"))
            iret = objOrder.UpdateDetailsintoSales(sInvoiceno, sInvDate, txtOrderNo.Text, sCustomerCode, sName, sAddress1, sAddress2, sAddress3, _
                                                 sSalesManCode, sSalesType, dTotalWeight, dTotalStoneWeight, dTotalGoldLoss, _
                                                 dTotalSMC, sGstType, dGST, dDetailDiscont, dDiscount, dTotalCost, dTotalPCS, sUserID)

            If Not iret Then
                MessageBox.Show("Error in InstallmentSave")
                SubAddHeaderIntoSales = False
                Exit Function
            End If

        End If
    End Function
    Private Sub subPrintBill(ByVal sInvoiceNo As String)
        Dim ReportTitle As String
        Dim sSelection As String
        Dim sSalesType$
        Dim sReply As String
        Dim dsSales As New DataSet
        Dim frmrep As New frmReports
        Dim CR As New ReportDocument
        Dim j As Integer
        Try
            Cursor = Cursors.WaitCursor
            dsSales = objSales.GetSalesHeaderByInvoiceNo(Trim(sInvoiceNo))

            If dsSales.Tables(0).Rows.Count > 0 Then sSalesType = Trim$(dsSales.Tables(0).Rows(0)("SalesType") & "")

            If UCase(sCompanyShortCode) = "HN" Then
                Select Case sSalesType
                    Case "TRADEIN"
                        strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_HN.rpt"
                        CR.Load(strpath)
                        ReportLogOnToServer(CR)
                        CR.Refresh()
                        subGetClassRate()
                        frmrep.ReportViewer.ReportSource = CR
                        sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                        frmrep.ReportViewer.SelectionFormula = sSelection
                        CR.DataDefinition.RecordSelectionFormula = sSelection
                        If UCase(sCompanyShortCode) = "HN" And bReprint Then
                            CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                        End If
                        CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                        CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                        CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                        CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                        CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                        CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                        CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                        CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                        CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                        CR.PrintToPrinter(1, True, 0, 0)
                        frmrep = Nothing
                        frmrep = New frmReports


                        ''''' Durai Changes
                        '''If (UCase(Objuser.CompanyName)) = "HANIFFA PTE LTD" Then
                        strpath = Application.StartupPath & "\Reports\rptTradeInDetailInvoice_HN.rpt"
                        CR.Load(strpath)
                        ReportLogOnToServer(CR)
                        CR.Refresh()
                        subGetClassRate()
                        frmrep.ReportViewer.ReportSource = CR
                        sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                        frmrep.ReportViewer.SelectionFormula = sSelection
                        CR.DataDefinition.RecordSelectionFormula = sSelection
                        CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                        CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                        CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                        CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                        CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                        CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                        CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                        CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                        CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                        CR.PrintToPrinter(1, True, 0, 0)
                        frmrep = Nothing
                        frmrep = New frmReports
                        '''End If

                    Case "CASH"
                        strpath = Application.StartupPath & "\Reports\rptSalesInvoice_HN.rpt"

                        CR.Load(strpath)
                        ReportLogOnToServer(CR)
                        CR.Refresh()
                        subGetClassRate()
                        frmrep.ReportViewer.ReportSource = CR
                        sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                        frmrep.ReportViewer.SelectionFormula = sSelection
                        CR.DataDefinition.RecordSelectionFormula = sSelection
                        If UCase(sCompanyShortCode) = "HN" And bReprint Then
                            CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                        End If
                        CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                        CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                        CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                        CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                        CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                        CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                        CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                        CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                        CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                        CR.PrintToPrinter(1, True, 0, 0)
                        frmrep = Nothing
                        frmrep = New frmReports
                End Select
            ElseIf UCase(sCompanyShortCode) = "MJ" Then
                ''strpath = Application.StartupPath & "\Reports\rptSalesInvoice_MJ.rpt"
                If sInvGstType = "" Then
                    strpath = Application.StartupPath & "\Reports\rptSalesInvoice_MJ.rpt"
                ElseIf sInvGstType = "Standard Rated Supply" Then
                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceStandard_MJ.rpt"
                ElseIf sInvGstType = "Exempt Supply" Or sInvGstType = "No GST" Then
                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceNoGST_MJ.rpt"
                End If
                CR.Load(strpath)
                ReportLogOnToServer(CR)
                CR.Refresh()
                subGetClassRate()
                frmrep.ReportViewer.ReportSource = CR
                sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                frmrep.ReportViewer.SelectionFormula = sSelection
                CR.DataDefinition.RecordSelectionFormula = sSelection
                If UCase(sCompanyShortCode) = "MJ" And bReprint Then
                    CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                End If
                CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s24K999)
                CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s20K850)
                CR.DataDefinition.FormulaFields("Rate4").Text = fncFormatSQLText(s22KSOV)
                ''CR.PrintOptions.PaperSize = PaperSize.DefaultPaperSize
                CR.PrintToPrinter(1, True, 0, 0)

                ''frmrep.ReportViewer.ReportSource = CR
                ''frmrep.ShowDialog()
                ''frmrep.ReportViewer.Visible = True
                ''frmrep.ReportViewer.CloseView(False)
                ''frmrep.WindowState = FormWindowState.Maximized
                frmrep = Nothing
                frmrep = New frmReports
                ''' End Select
            ElseIf UCase(sCompanyShortCode) = "SSJ" Then
                Select Case sSalesType
                    Case "INSTALLMENT"
                        strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_SSJ.rpt"

                        CR.Load(strpath)
                        ReportLogOnToServer(CR)
                        CR.Refresh()
                        subGetClassRate()
                        frmrep.ReportViewer.ReportSource = CR
                        sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                        frmrep.ReportViewer.SelectionFormula = sSelection
                        CR.DataDefinition.RecordSelectionFormula = sSelection
                        If UCase(sCompanyShortCode) = "SSJ" And bReprint Then
                            CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                        End If
                        CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                        CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                        CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                        CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                        CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                        CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                        CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                        CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                        CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                        CR.PrintToPrinter(1, True, 0, 0)
                        frmrep = Nothing
                        frmrep = New frmReports
                    Case "TRADEIN"

                        strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_SSJ.rpt"
                        CR.Load(strpath)
                        ReportLogOnToServer(CR)
                        CR.Refresh()
                        subGetClassRate()
                        frmrep.ReportViewer.ReportSource = CR
                        sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                        frmrep.ReportViewer.SelectionFormula = sSelection
                        CR.DataDefinition.RecordSelectionFormula = sSelection
                        If UCase(sCompanyShortCode) = "SSJ" And bReprint Then
                            CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                        End If
                        CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                        CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                        CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                        CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                        CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                        CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                        CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                        CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                        CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                        CR.PrintToPrinter(1, True, 0, 0)
                        frmrep = Nothing
                        frmrep = New frmReports
                    Case "CASH"
                        strpath = Application.StartupPath & "\Reports\rptSalesInvoice_SSJ.rpt"

                        CR.Load(strpath)
                        ReportLogOnToServer(CR)
                        CR.Refresh()
                        subGetClassRate()
                        frmrep.ReportViewer.ReportSource = CR
                        sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                        frmrep.ReportViewer.SelectionFormula = sSelection
                        CR.DataDefinition.RecordSelectionFormula = sSelection
                        If UCase(sCompanyShortCode) = "SSJ" And bReprint Then
                            CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                        End If
                        CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                        CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                        CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                        CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                        CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                        CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                        CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                        CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                        CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                        CR.PrintToPrinter(1, True, 0, 0)
                        frmrep = Nothing
                        frmrep = New frmReports
                End Select
            ElseIf UCase(sCompanyShortCode) = "RJ" Then
                Select Case sSalesType
                    Case "INSTALLMENT"
                        j = 1
                        For j = 1 To 2
                            strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_RJ.rpt"

                            CR.Load(strpath)
                            ReportLogOnToServer(CR)
                            CR.Refresh()
                            subGetClassRate()
                            frmrep.ReportViewer.ReportSource = CR
                            sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                            frmrep.ReportViewer.SelectionFormula = sSelection
                            CR.DataDefinition.RecordSelectionFormula = sSelection
                            If UCase(sCompanyShortCode) = "SSJ" And bReprint Then
                                CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                            End If
                            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                            CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                            CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                            CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                            CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                            CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                            CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                            CR.PrintToPrinter(1, True, 0, 0)
                            frmrep = Nothing
                            frmrep = New frmReports
                        Next
                        'frmrep.Show()
                        'frmrep.ReportViewer.Visible = True
                        'frmrep.ReportViewer.DisplayToolbar = True
                        'frmrep.ReportViewer.CloseView(False)
                        'frmrep.WindowState = FormWindowState.Maximized

                    Case "TRADEIN"
                        j = 1
                        For j = 1 To 2
                            strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_RJ.rpt"
                            CR.Load(strpath)
                            ReportLogOnToServer(CR)
                            CR.Refresh()
                            subGetClassRate()
                            frmrep.ReportViewer.ReportSource = CR
                            sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                            frmrep.ReportViewer.SelectionFormula = sSelection
                            CR.DataDefinition.RecordSelectionFormula = sSelection
                            If UCase(sCompanyShortCode) = "SSJ" And bReprint Then
                                CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                            End If
                            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                            CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                            CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                            CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                            CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                            CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                            CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                            'frmrep.Show()
                            'frmrep.ReportViewer.Visible = True
                            'frmrep.ReportViewer.DisplayToolbar = True
                            'frmrep.ReportViewer.CloseView(False)
                            'frmrep.WindowState = FormWindowState.Maximized
                            CR.PrintToPrinter(1, True, 0, 0)
                            frmrep = Nothing
                            frmrep = New frmReports
                        Next
                    Case "CASH"
                        j = 1
                        For j = 1 To 2
                            strpath = Application.StartupPath & "\Reports\rptSalesInvoice_RJ.rpt"

                            CR.Load(strpath)
                            ReportLogOnToServer(CR)
                            CR.Refresh()
                            subGetClassRate()
                            frmrep.ReportViewer.ReportSource = CR
                            sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                            frmrep.ReportViewer.SelectionFormula = sSelection
                            CR.DataDefinition.RecordSelectionFormula = sSelection
                            If UCase(sCompanyShortCode) = "SSJ" And bReprint Then
                                CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                            End If
                            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                            CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                            CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                            CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                            CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                            CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                            CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)


                            '  frmrep.ReportViewer.SelectionFormula = strselectformula
                            'frmrep.Show()
                            'frmrep.ReportViewer.Visible = True
                            'frmrep.ReportViewer.DisplayToolbar = True
                            'frmrep.ReportViewer.CloseView(False)
                            'frmrep.WindowState = FormWindowState.Maximized
                            ' frmrep = Nothing
                            '  CR.PrintOptions.PaperSize = PaperSize.DefaultPaperSize
                            CR.PrintToPrinter(1, True, 0, 0)
                            frmrep = Nothing
                            frmrep = New frmReports
                        Next
                End Select

            Else
                sReply = MsgBox("Do you want to print Net Bill?", vbYesNoCancel + vbQuestion, "Print Option")
                If sReply = vbYes Then
                    Select Case sSalesType
                        Case "INSTALLMENT"
                            If sCompanyShortCode = "ES" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_ESP.rpt"
                            ElseIf sCompanyShortCode = "AS" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_AS.rpt"
                            ElseIf sCompanyShortCode = "ML" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_ML.rpt"
                            ElseIf UCase(sCompanyShortCode) = "GMT" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_GMT.rpt"
                            End If

                            CR.Load(strpath)
                            ReportLogOnToServer(CR)
                            CR.Refresh()
                            subGetClassRate()
                            frmrep.ReportViewer.ReportSource = CR
                            sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                            frmrep.ReportViewer.SelectionFormula = sSelection
                            CR.DataDefinition.RecordSelectionFormula = sSelection
                            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                            CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                            CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                            CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                            CR.PrintToPrinter(1, True, 0, 0)
                            frmrep = Nothing
                            frmrep = New frmReports
                        Case "TRADEIN"
                            If sCompanyShortCode = "ES" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_ESP.rpt"
                            ElseIf sCompanyShortCode = "AS" Then
                                '''strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_AS.rpt"
                                If sInvGstType = "" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_AS.rpt"
                                ElseIf sInvGstType = "Standard Rated Supply" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoiceStandard_AS.rpt"
                                ElseIf sInvGstType = "Exempt Supply" Or sInvGstType = "No GST" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoiceNoGST_AS.rpt"
                                End If
                            ElseIf sCompanyShortCode = "ML" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_ML.rpt"
                            ElseIf UCase(sCompanyShortCode) = "GMT" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_GMT.rpt"
                            ElseIf UCase(sCompanyShortCode) = "HN" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_HN.rpt"
                            End If
                            CR.Load(strpath)
                            ReportLogOnToServer(CR)
                            CR.Refresh()
                            subGetClassRate()
                            frmrep.ReportViewer.ReportSource = CR
                            sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                            frmrep.ReportViewer.SelectionFormula = sSelection
                            CR.DataDefinition.RecordSelectionFormula = sSelection
                            If UCase(sCompanyShortCode) = "HN" And bReprint Then
                                CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                            End If
                            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                            CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                            CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                            CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                            CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                            CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                            CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                            CR.PrintToPrinter(1, True, 0, 0)
                            frmrep = Nothing
                            frmrep = New frmReports
                            If sCompanyShortCode = "ES" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInDetailInvoice_ESP.rpt"
                            End If

                            If ((UCase(sCompanyShortCode) <> "HN") And (UCase(sCompanyShortCode) <> "AS") And (UCase(sCompanyShortCode) <> "ML")) Then
                                CR.Load(strpath)
                                ReportLogOnToServer(CR)
                                CR.Refresh()
                                subGetClassRate()
                                frmrep.ReportViewer.ReportSource = CR
                                sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                                frmrep.ReportViewer.SelectionFormula = sSelection
                                CR.DataDefinition.RecordSelectionFormula = sSelection
                                CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                                CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                                CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                                CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                                CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                                CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                                CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                                CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                                CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                                CR.PrintToPrinter(1, True, 0, 0)
                                frmrep = Nothing
                                frmrep = New frmReports
                            End If
                        Case Else
                            If UCase(sCompanyShortCode) = "ES" Then
                                strpath = Application.StartupPath & "\Reports\rptSalesInvoice_ESP.rpt"
                            ElseIf sCompanyShortCode = "AS" Then
                                '''strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AS.rpt"
                                If sInvGstType = "" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AS.rpt"
                                ElseIf sInvGstType = "Standard Rated Supply" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceStandard_AS.rpt"
                                ElseIf sInvGstType = "Exempt Supply" Or sInvGstType = "No GST" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceNoGST_AS.rpt"
                                End If
                            ElseIf sCompanyShortCode = "AJ" Then
                                '''strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AS.rpt"
                                If sInvGstType = "" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AJ.rpt"
                                ElseIf sInvGstType = "Standard Rated Supply" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceStandard_AJ.rpt"
                                ElseIf sInvGstType = "Exempt Supply" Or sInvGstType = "No GST" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceNoGST_AJ.rpt"
                                End If
                            ElseIf sCompanyShortCode = "ML" Then
                                strpath = Application.StartupPath & "\Reports\rptSalesInvoice_ML.rpt"
                            ElseIf UCase(sCompanyShortCode) = "GMT" Then
                                strpath = Application.StartupPath & "\Reports\rptSalesInvoice_GMT.rpt"
                            ElseIf UCase(sCompanyShortCode) = "HN" Then
                                strpath = Application.StartupPath & "\Reports\rptSalesInvoice_HN.rpt"
                            End If
                            CR.Load(strpath)
                            ReportLogOnToServer(CR)
                            CR.Refresh()
                            subGetClassRate()
                            frmrep.ReportViewer.ReportSource = CR
                            sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                            frmrep.ReportViewer.SelectionFormula = sSelection
                            CR.DataDefinition.RecordSelectionFormula = sSelection
                            If UCase(sCompanyShortCode) = "HN" And bReprint Then
                                CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                            End If

                            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                            CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                            CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                            CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                            CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                            CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                            CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                            CR.PrintToPrinter(1, True, 0, 0)
                            frmrep = Nothing
                            frmrep = New frmReports
                    End Select
                End If

                If sReply = vbNo Then
                    Select Case sSalesType
                        Case "INSTALLMENT"
                            If sCompanyShortCode = "ES" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_ESP.rpt"
                            ElseIf sCompanyShortCode = "AS" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_AS.rpt"
                            ElseIf sCompanyShortCode = "ML" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_ML.rpt"
                            ElseIf UCase(sCompanyShortCode) = "GMT" Then
                                strpath = Application.StartupPath & "\Reports\rptInstallmentSalesInvoice_GMT.rpt"
                            End If
                            CR.Load(strpath)
                            ReportLogOnToServer(CR)
                            CR.Refresh()
                            subGetClassRate()
                            frmrep.ReportViewer.ReportSource = CR
                            sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                            frmrep.ReportViewer.SelectionFormula = sSelection
                            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                            CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                            CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                            CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)

                            frmrep.Show()
                            frmrep.ReportViewer.Visible = True
                            frmrep.ReportViewer.DisplayToolbar = True
                            frmrep.ReportViewer.CloseView(False)
                            frmrep.WindowState = FormWindowState.Maximized
                            frmrep = Nothing
                            frmrep = New frmReports

                        Case "TRADEIN"
                            If sCompanyShortCode = "ES" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_ESP.rpt"
                            ElseIf sCompanyShortCode = "AS" Then
                                '''  strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_AS.rpt"
                                If sInvGstType = "" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_AS.rpt"
                                ElseIf sInvGstType = "Standard Rated Supply" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoiceStandard_AS.rpt"
                                ElseIf sInvGstType = "Exempt Supply" Or sInvGstType = "No GST" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoiceNoGST_AS.rpt"
                                End If
                            ElseIf sCompanyShortCode = "AJ" Then
                                '''  strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_AS.rpt"
                                If sInvGstType = "" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_AJ.rpt"
                                ElseIf sInvGstType = "Standard Rated Supply" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoiceStandard_AJ.rpt"
                                ElseIf sInvGstType = "Exempt Supply" Or sInvGstType = "No GST" Then
                                    strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoiceNoGST_AJ.rpt"
                                End If
                            ElseIf UCase(sCompanyShortCode) = "GMT" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_GMT.rpt"
                            ElseIf UCase(sCompanyShortCode) = "ML" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_ML.rpt"
                            ElseIf UCase(sCompanyShortCode) = "HN" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInSalesInvoice_HN.rpt"
                            End If
                            CR.Load(strpath)
                            ReportLogOnToServer(CR)
                            CR.Refresh()
                            frmrep.ReportViewer.ReportSource = CR
                            subGetClassRate()
                            sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                            frmrep.ReportViewer.SelectionFormula = sSelection
                            If UCase(sCompanyShortCode) = "HN" And bReprint Then
                                CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                            End If
                            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                            CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                            CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                            CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                            CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                            CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                            CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)

                            frmrep.Show()
                            frmrep.ReportViewer.Visible = True
                            frmrep.ReportViewer.DisplayToolbar = True
                            frmrep.ReportViewer.CloseView(False)
                            frmrep.WindowState = FormWindowState.Maximized
                            frmrep = Nothing
                            frmrep = New frmReports

                            If sCompanyShortCode = "ES" Then
                                strpath = Application.StartupPath & "\Reports\rptTradeInDetailInvoice_ESP.rpt"
                            End If

                            If ((UCase(sCompanyShortCode) <> "HN") And (UCase(sCompanyShortCode) <> "AS") And (UCase(sCompanyShortCode) <> "ML")) Then
                                CR.Load(strpath)
                                ReportLogOnToServer(CR)
                                CR.Refresh()
                                frmrep.ReportViewer.ReportSource = CR
                                subGetClassRate()
                                sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                                frmrep.ReportViewer.SelectionFormula = sSelection
                                CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                                CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                                CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                                CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                                CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                                CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                                CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                                CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                                CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)

                                frmrep.Show()
                                frmrep.ReportViewer.Visible = True
                                frmrep.ReportViewer.DisplayToolbar = True
                                frmrep.ReportViewer.CloseView(False)
                                frmrep.WindowState = FormWindowState.Maximized
                                frmrep = Nothing
                                frmrep = New frmReports
                            End If
                        Case Else
                            If sCompanyShortCode = "ES" Then
                                strpath = Application.StartupPath & "\Reports\rptSalesInvoice_ESP.rpt"
                            ElseIf sCompanyShortCode = "AS" Then
                                '''strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AS.rpt"
                                If sInvGstType = "" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AS.rpt"
                                ElseIf sInvGstType = "Standard Rated Supply" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceStandard_AS.rpt"
                                ElseIf sInvGstType = "Exempt Supply" Or sInvGstType = "No GST" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceNoGST_AS.rpt"
                                End If
                            ElseIf sCompanyShortCode = "AJ" Then
                                '''strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AS.rpt"
                                If sInvGstType = "" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoice_AJ.rpt"
                                ElseIf sInvGstType = "Standard Rated Supply" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceStandard_AJ.rpt"
                                ElseIf sInvGstType = "Exempt Supply" Or sInvGstType = "No GST" Then
                                    strpath = Application.StartupPath & "\Reports\rptSalesInvoiceNoGST_AJ.rpt"
                                End If
                            ElseIf sCompanyShortCode = "ML" Then
                                strpath = Application.StartupPath & "\Reports\rptSalesInvoice_ML.rpt"
                            ElseIf UCase(sCompanyShortCode) = "GMT" Then
                                strpath = Application.StartupPath & "\Reports\rptSalesInvoice_GMT.rpt"
                            ElseIf UCase(sCompanyShortCode) = "HN" Then
                                strpath = Application.StartupPath & "\Reports\rptSalesInvoice_HN.rpt"
                            End If
                            CR.Load(strpath)
                            ReportLogOnToServer(CR)
                            CR.Refresh()
                            frmrep.ReportViewer.ReportSource = CR
                            subGetClassRate()
                            sSelection = "{tblSalesHeader.InvoiceNo} = " & fncFormatSQLText(Trim(sInvoiceNo))
                            frmrep.ReportViewer.SelectionFormula = sSelection
                            If UCase(sCompanyShortCode) = "HN" And bReprint Then
                                CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
                            End If
                            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(CompName)
                            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(Address1 & " " & Address2 & " " & Country)
                            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(PhoneNo & " " & FaxNo)
                            CR.DataDefinition.FormulaFields("Customer1").Text = fncFormatSQLText(txtCustomerName.Text)
                            CR.DataDefinition.FormulaFields("Customer2").Text = fncFormatSQLText(txtAddress1.Text)
                            CR.DataDefinition.FormulaFields("Customer3").Text = fncFormatSQLText(txtAddress2.Text)
                            CR.DataDefinition.FormulaFields("Rate1").Text = fncFormatSQLText(s999)
                            CR.DataDefinition.FormulaFields("Rate2").Text = fncFormatSQLText(s916)
                            CR.DataDefinition.FormulaFields("Rate3").Text = fncFormatSQLText(s835)
                            frmrep.Show()
                            frmrep.ReportViewer.Visible = True
                            frmrep.ReportViewer.DisplayToolbar = True
                            frmrep.ReportViewer.CloseView(False)
                            frmrep.WindowState = FormWindowState.Maximized
                            frmrep = Nothing
                            frmrep = New frmReports
                    End Select
                End If

                If sReply = vbCancel Then
                    Exit Sub
                End If
                If sReply = vbCancel Then
                    Exit Sub
                End If
            End If
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
    Private Sub subGetClassRate()
        Dim dsClass As New DataSet
        Dim i As Integer
        Try
            dsClass = objClassRate.GetClassRate()
            If dsClass.Tables(0).Rows.Count > 0 Then
                For i = 0 To dsClass.Tables(0).Rows.Count - 1
                    If Trim(dsClass.Tables(0).Rows(i)("ClassCode") & "") = "22K" Then
                        s916 = Format(Val(dsClass.Tables(0).Rows(i)("Rate") & ""), "#0.00")
                    ElseIf Trim(dsClass.Tables(0).Rows(i)("ClassCode") & "") = "916" Then
                        s916 = Format(Val(dsClass.Tables(0).Rows(i)("Rate") & ""), "#0.00")
                    ElseIf Trim(dsClass.Tables(0).Rows(i)("ClassCode") & "") = "22K-916" Then
                        s916 = Format(Val(dsClass.Tables(0).Rows(i)("Rate") & ""), "#0.00")
                    ElseIf Trim(dsClass.Tables(0).Rows(i)("ClassCode") & "") = "999" Then
                        s999 = Format(Val(dsClass.Tables(0).Rows(i)("Rate") & ""), "#0.00")
                    ElseIf Trim(dsClass.Tables(0).Rows(i)("ClassCode") & "") = "20KJ" Then
                        s835 = Format(Val(dsClass.Tables(0).Rows(i)("Rate") & ""), "#0.00")
                    ElseIf Trim(dsClass.Tables(0).Rows(i)("ClassCode") & "") = "24K" Then
                        s999 = Format(Val(dsClass.Tables(0).Rows(i)("Rate") & ""), "#0.00")
                    ElseIf Trim(dsClass.Tables(0).Rows(i)("ClassCode") & "") = "24K-999" Then
                        s24K999 = Format(Val(dsClass.Tables(0).Rows(i)("Rate") & ""), "#0.00")
                    ElseIf Trim(dsClass.Tables(0).Rows(i)("ClassCode") & "") = "20K-850" Then
                        s20K850 = Format(Val(dsClass.Tables(0).Rows(i)("Rate") & ""), "#0.00")
                    ElseIf Trim(dsClass.Tables(0).Rows(i)("ClassCode") & "") = "22K-SOV" Then
                        s22KSOV = Format(Val(dsClass.Tables(0).Rows(i)("Rate") & ""), "#0.00")
                    End If
                Next i
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dsClass = Nothing
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click

        Try
            If txtOrderNo.Text.Trim = "" Then
                MessageBox.Show("Pleaes select order number.")
                txtOrderNo.Focus()
                Exit Sub
            End If

            If grdOrderPayment.RowCount > 0 Then
                ' SmartJewel ToDo
                If MessageBox.Show("Are you sure, do you want to cancel the Repair payment?", "GoldPos", MessageBoxButtons.YesNo) = DialogResult.Yes Then subCancelRepairPayment()
            End If
            subClearMe()
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try
    End Sub

    Private Sub subCancelRepairPayment()
        Dim objPM As New clsPayMode
        Try
            objOrder.OpenRepairConnection()
            objOrder.gpBeginRepairTransaction()
            If Not objOrder.fncRepairPaymentDetailDelete(txtOrderNo.Text.Trim, fnGetdgvText(grdOrderPayment, grdOrderPayment.CurrentRow.Index, 1), fnGetdgvText(grdOrderPayment, grdOrderPayment.CurrentRow.Index, 3)) Then
                objOrder.gpRollBackRepairConnection()
            Else
                If Not objPM.PayModeDetailSave(txtOrderNo.Text.Trim, Format(Now, "dd/MM/yyyy"), "CASH", -Val(fnGetdgvText(grdOrderPayment, grdOrderPayment.CurrentRow.Index, 3)), Val(0), "RP CANCEL", "REPAIR") Then
                    objOrder.gpRollBackRepairConnection()
                Else
                    MessageBox.Show("Order payment canceled.")
                    objOrder.gpCommitRepairTransaction()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            objOrder.CloseRepairConnection()
            objPM = Nothing
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
            subGetClassRate()
            frmRep.ReportViewer.ReportSource = CR
            sSelection = "{tblInventoryRepairHeader.RepairNo} = " & fncFormatSQLText(Trim(sInvoiceno))
            frmRep.ReportViewer.SelectionFormula = sSelection
            CR.DataDefinition.RecordSelectionFormula = sSelection
            If UCase(sCompanyShortCode) = "HN" And bReprint Then
                CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText("Duplicate Copy")
            End If

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

    Private Sub btnPrint_Click(sender As System.Object, e As System.EventArgs) Handles btnPrint.Click
        If txtOrderNo.Text.Trim = "" Then
            MessageBox.Show("Please enter OrderNo.")
            txtOrderNo.Focus()
            Exit Sub
        End If
        If Trim(txtOrderNo.Text) <> "" Then
            Call subPrintRepair(Trim(txtOrderNo.Text))
        End If
    End Sub

    Private Sub btnTradeIn_Click(sender As System.Object, e As System.EventArgs) Handles btnTradeIn.Click
        Try
            Dim TradeIn As New frmTradeInGold
            'subGetCompany()
            TradeIn.sCalledFrom = "ORDERFORM"
            TradeIn.sInvoiceNo = Format(Val(txtOrderNo.Text), "00000000")
            TradeIn.txtCustomerCode.Text = txtCustomerCode.Text
            TradeIn.txtCustomerName.Text = txtCustomerName.Text
            TradeIn.lblCustomerType.Text = lblCustomerType.Text
            TradeIn.txtAddress1.Text = txtAddress1.Text
            TradeIn.txtAddress2.Text = txtAddress2.Text
            TradeIn.txtAddress3.Text = txtAddress3.Text
            TradeIn.lblTradeInLimit.Text = dTradeinLimit
            TradeIn.lblSalesAmount.Text = Val(lblBalanceAmount.Text)
            TradeIn.sSalesAmount = Val(lblBalanceAmount.Text)
            'TradeIn.txtCostPerGram.Text = Val(txtCurrCostPerGram.Text)
            TradeIn.ShowDialog()
            txtPayAmount.Text = dTradeinAmount
            tradeInRefNo = sTradeInRefNo
            dTradeInSales = dTradeinAmount
            dTradeInSales = dTradeinAmount
            dTradeInGST = Val(-Format$((Val(dTradeinAmount) * 7) / (107), "#0.00"))
            '  dTradeInGST = Val(-Format$((Val(dTradeinAmount) * 7) / (107), "#0.00"))
            tradeInRefNo = sTradeInRefNo
            subAddTradeInToGrid()
            sType = "TRADEIN"
            If ChkTradeIn Then
                btnTradeIn.Enabled = False
                optCash.Enabled = False
                'txtPayAmount.Enabled = False
                If optBoth.Checked Then
                    txtPayAmount.Enabled = True
                Else
                    txtPayAmount.Enabled = False
                End If
            Else
                btnTradeIn.Enabled = True
                optCash.Enabled = True
                txtPayAmount.Enabled = True
            End If

           
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
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

                If UCase(sCompanyShortCode) = "MJ" Or UCase(sCompanyShortCode) = "SSJ" Or UCase(sCompanyShortCode) = "AJ" Then
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
End Class