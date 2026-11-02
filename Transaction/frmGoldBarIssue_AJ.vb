Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmGoldBarIssue_AJ
    Dim objPurchase As New clsGoldBarPurchase
    Dim objTIS As New ClsGoldBarIssue
    Dim dm As New DataManager
    Dim SInvoiceNo As String
    Dim sInventoryCode As String
    Dim sInvNo As String
    Dim sInvCode As String
    Dim IbLoad As Boolean
    Dim dWeight As Double
    Dim bEdited As Boolean
    Dim dAIssueBal As Double
    Dim dIssueWeight As Double
    Dim d24KIssueWeight As Double
    Dim sInvoicedate As String
    Dim sTDate As String
    Dim dABal As Double
    Dim dPaidCost As Double
    Dim sBarNo As String
    Dim sGoldBarIssueNo As String
    Dim bProceed As Boolean
    Private Sub btnSupplierCode_Click(sender As System.Object, e As System.EventArgs) Handles btnSupplierCode.Click
        Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSupplier", "L", 4, "SupplierName", "", "SupplierCode", "SupplierCode", 80, "SupplierName", "SupplierName", 180, "Address1", "Address1", 60, "Address2", "Address2", 80)
        frmHelpClass.ShowDialog()
        txtSupplierCode1.Text = frmHelpClass.Var1.Trim
        txtsupplierName.Text = frmHelpClass.Var2.Trim
    End Sub

    Private Sub txtSupplierCode1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtSupplierCode1.KeyDown
        If e.KeyCode = Keys.F1 Then
            Dim frmHelpClass As New frmHelpSearch(0, 0, "tblSupplier", "L", 4, "SupplierName", "", "SupplierCode", "SupplierCode", 80, "SupplierName", "SupplierName", 180, "Address1", "Address1", 60, "Address2", "Address2", 80)
            frmHelpClass.ShowDialog()
            txtSupplierCode1.Text = frmHelpClass.Var1.Trim
            txtsupplierName.Text = frmHelpClass.Var2.Trim
        End If
    End Sub

    Private Sub btnInvoiceNo_Click(sender As System.Object, e As System.EventArgs) Handles btnInvoiceNo.Click
        If txtSupplierCode1.Text.Trim = "" Then
            MsgBox("Select the Supplier Code!")
            txtSupplierCode1.Focus()
            Exit Sub
        End If
        subGetTradeInInvoices()
    End Sub
    Private Sub subGetTradeInInvoices()
        Try
            Dim icnt As Long
            Dim sDate As Date
            Dim dsPur As New DataSet
            pnlInvoice.Visible = True
            dsPur = objTIS.GetTradeInInvoiceWholeSale(txtSupplierCode1.Text.Trim, SInvoiceNo)
            If dsPur.Tables(0).Rows.Count > 0 Then
                grdInvoice.RowCount = 0
                If Not IsNothing(dsPur) Then
                    If dsPur.Tables(0).Rows.Count > 0 Then
                        For icnt = 0 To dsPur.Tables(0).Rows.Count - 1
                            grdInvoice.Rows.Add()
                            Call fnSetdgvText(grdInvoice, icnt, 0, False)
                            fnSetdgvText(grdInvoice, icnt, 1, Trim(dsPur.Tables(0).Rows(icnt)("InvoiceDate") & ""))
                            fnSetdgvText(grdInvoice, icnt, 2, Trim(dsPur.Tables(0).Rows(icnt)("InvoiceNo") & ""))
                            fnSetdgvText(grdInvoice, icnt, 3, Trim(dsPur.Tables(0).Rows(icnt)("SupplierCode") & ""))
                            fnSetdgvText(grdInvoice, icnt, 4, Trim(dsPur.Tables(0).Rows(icnt)("PurchaseWeight") & ""))
                            fnSetdgvText(grdInvoice, icnt, 5, Trim(dsPur.Tables(0).Rows(icnt)("PurchaseWeight") & ""))
                        Next
                    End If

                End If
            Else
  
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub btnPanelOK_Click(sender As System.Object, e As System.EventArgs) Handles btnPanelOK.Click
        Dim i As Long
        '  SInvoiceNo = ""
        ' sInvNo = ""
        ' sInvCode = ""

        For i = 0 To grdInvoice.RowCount - 1
            If Trim(fnGetdgvText(grdInvoice, i, 0)) = True Then
                ''SInvoiceNo = SInvoiceNo & fncFormatSQLText(GetText(grdInvoice, i, 2)) & ","
                SInvoiceNo = SInvoiceNo & "," & fncFormatSQLText(fnGetdgvText(grdInvoice, i, 2))
                sInvNo = sInvNo & "," & fncFormatSQLText(fnGetdgvText(grdInvoice, i, 2))
                sInventoryCode = sInventoryCode & "," & fncFormatSQLText(fnGetdgvText(grdInvoice, i, 2))
                sInvCode = sInvCode & "," & fncFormatSQLText(fnGetdgvText(grdInvoice, i, 2))
                dWeight = dWeight + Val(fnGetdgvText(grdInvoice, i, 4))
            End If
        Next
        'SInvoiceNo = Strings.Left(SInvoiceNo, Len(SInvoiceNo) - 1)
        If IbLoad Then
            SInvoiceNo = SInvoiceNo
            sInventoryCode = sInventoryCode
            sInvNo = sInvNo ''Strings.Right(sInvNo, Len(sInvNo) - 1)
            sInvCode = Strings.Right(sInvCode, Len(sInvCode) - 1)
        Else
            SInvoiceNo = Strings.Right(SInvoiceNo, Len(SInvoiceNo) - 1)
            sInventoryCode = Strings.Right(sInventoryCode, Len(sInventoryCode) - 1)
            sInvNo = Strings.Right(sInvNo, Len(sInvNo) - 1)
            sInvCode = Strings.Right(sInvCode, Len(sInvCode) - 1)
        End If
        subGetTradeBillNo()
        pnlInvoice.Visible = False
    End Sub
    Private Sub subGetTradeBillNo()
        Try
            Dim dsTIB As New DataSet
            Dim iCnt As Long

            dsTIB = objTIS.GetTradeInBalanceSales(Trim(sInvNo), Trim(sInvCode), Trim(txtSupplierCode1.Text))

            grdGoldIssue.RowCount = 0
            If dsTIB.Tables(0).Rows.Count > 0 Then
                'txtInvoiceNo1 = Trim(adoRs!InvoiceNo & "")
                sInvoicedate = Trim(dsTIB.Tables(0).Rows(0)("ModifyDate") & "")
                sTDate = Trim(dsTIB.Tables(0).Rows(0)("ModifyDate") & "")
                sTDate = DateValue(sTDate)
                For iCnt = 0 To dsTIB.Tables(0).Rows.Count - 1

                    grdGoldIssue.Rows.Add()
                    Call fnSetdgvText(grdGoldIssue, iCnt, 0, Trim(dsTIB.Tables(0).Rows(iCnt)("JObsheetNo") & ""))
                    Call fnSetdgvText(grdGoldIssue, iCnt, 2, Trim(dsTIB.Tables(0).Rows(iCnt)("PurchaseBillNo") & ""))
                    Call fnSetdgvText(grdGoldIssue, iCnt, 1, sTDate)
                    Call fnSetdgvText(grdGoldIssue, iCnt, 3, "TRADE-IN")
                    Call fnSetdgvText(grdGoldIssue, iCnt, 4, Val(dsTIB.Tables(0).Rows(iCnt)("TotalWeight24K") & ""))

                    Call fnSetdgvText(grdGoldIssue, iCnt, 5, Val(dsTIB.Tables(0).Rows(iCnt)("TotalWeight24K") * Val(1) & ""))
                    Call fnSetdgvText(grdGoldIssue, iCnt, 6, Val(dsTIB.Tables(0).Rows(iCnt)("TotalWeight24K") & ""))
                    dABal = dABal + Val(dsTIB.Tables(0).Rows(iCnt)("TotalWeight24K") & "")
                    dAIssueBal = dAIssueBal + Val(dsTIB.Tables(0).Rows(iCnt)("TotalWeight24K") * Val(1) & "")
           
                    dPaidCost = 0

                Next iCnt

            Else

                Exit Sub
            End If


            txtIssued2.Text = Val(dABal)
            txtIssued2.Text = CDbl(txtIssued2.Text)
            
            IbLoad = True
   
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd.Click
        Try
            subAddGrid(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Private Sub subAddGrid(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As Long
        Dim lQty As Long
        Dim lRow As Long
        Dim iRow As Long
        Try
    
            lQty = 0
            lRow = 0
            iRow = grdIssue1.Rows.Add
            Call fnSetdgvText(grdIssue1, iRow, 1, txtInventoryCode1.Text.Trim)
            Call fnSetdgvText(grdIssue1, iRow, 2, txtDescription.Text.Trim)
            Call fnSetdgvText(grdIssue1, iRow, 3, Format(Val(TxtTotalWeight.Text.Trim), "#0.00"))
            Call fnSetdgvText(grdIssue1, iRow, 4, Format(Val(txtPcs.Text.Trim), "#0"))
            Call fnSetdgvText(grdIssue1, iRow, 5, Format(Val(txtCostperGram.Text.Trim), "#0.00"))
            Call fnSetdgvText(grdIssue1, iRow, 6, Format(Val(txtTotalCost.Text.Trim), "#0.00"))
            Call fnSetdgvText(grdIssue1, iRow, 7, Trim(sBarNo))
            d24KIssueWeight = d24KIssueWeight + Format(Val(TxtTotalWeight.Text.Trim) * Val(txtPcs.Text.Trim), "#0.00")


            txtInventoryCode1.Tag = txtInventoryCode1
            txtNetIssue1.Text = d24KIssueWeight

            subClearDetail(True)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub
    Private Sub subClearDetail(ByVal bFocus As Boolean)
        Try
            txtInventoryCode1.Tag = ""
            bEdited = False
            txtInventoryCode1.Text = ""
            txtDescription.Text = ""
            TxtTotalWeight.Text = ""
            txtCostperGram.Text = ""
            txtPcs.Text = ""
            txtTotalCost.Text = ""

            'chkIPM.Checked = False
            If (bFocus And txtInventoryCode1.Enabled) Then txtInventoryCode1.Focus()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete.Click

    End Sub

    Private Sub btnRePrint_Click(sender As System.Object, e As System.EventArgs) Handles btnRePrint.Click
        If Trim(txtIssueNo1.Text) <> "" Then
            subPrint(txtIssueNo1.Text.Trim)
        End If
    End Sub
    Private Sub subPrint(ByVal sTradeInIssueNo As String)
        Dim frmrep As New frmReports
        Dim CR As New ReportDocument
        Dim DS As New DataSet
        Dim objClsCompany As New clsCompany
        Dim sCompanyname, sAddress, sPhone, sFax, sReportTitle As String
        Try
            Dim ReportTitle As String
            Dim sSelection As String
            Dim sSalesType$
            Cursor = Cursors.WaitCursor
            DS = objClsCompany.getCompanyName
            If DS.Tables("GetCompanyName").Rows.Count > 0 Then
                sCompanyname = Trim(DS.Tables("GetCompanyName").Rows(0)("Name") & "")
                sAddress = Trim(DS.Tables("GetCompanyName").Rows(0)("Address1") & "") & " " & _
                             Trim(DS.Tables("GetCompanyName").Rows(0)("Address2") & "") & " " & _
                             Trim(DS.Tables("GetCompanyName").Rows(0)("Country") & "")
                sPhone = Trim(DS.Tables("GetCompanyName").Rows(0)("Phone") & "")
                sFax = Trim(DS.Tables("GetCompanyName").Rows(0)("Fax") & "")
            End If
            sReportTitle = "Trade In Issue Report For " & sTradeInIssueNo
            If UCase(sCompanyShortCode) = "HN" Then
                strpath = Application.StartupPath & "\Reports\rptGoldBarIssue_HN.rpt"
            Else
                strpath = Application.StartupPath & "\Reports\rptGoldBarIssue_HN.rpt"
            End If
            CR.Load(strpath)

            ReportLogOnToServer(CR)
            CR.Refresh()
            frmrep.ReportViewer.ReportSource = CR
            sSelection = "{tblGoldBarIssued.IssueNo} = " & fncFormatSQLText(Trim(sTradeInIssueNo))
            frmrep.ReportViewer.SelectionFormula = sSelection
            frmrep.Show()
            frmrep.ReportViewer.Visible = True
            frmrep.ReportViewer.DisplayToolbar = True
            frmrep.ReportViewer.CloseView(False)
            frmrep.WindowState = FormWindowState.Maximized
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor = Cursors.Default
            frmrep = Nothing
            CR = Nothing
            DS = Nothing
            objClsCompany = Nothing
        End Try
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub btnNew_Click(sender As System.Object, e As System.EventArgs) Handles btnNew.Click
        clearall()
    End Sub
    Private Sub clearall()
        txtSupplierCode1.Text = ""
        txtsupplierName.Text = ""
        txtInvoiceNo1.Text = ""
        txtIssueNo1.Text = ""
        txtIssued2.Text = "0.00"
        txtNetIssue1.Text = "0.00"
        grdGoldIssue.RowCount = 0
        grdIssue1.RowCount = 0
    End Sub

    Private Sub frmGoldBarIssue_AJ_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
                    btnNew_Click(Nothing, Nothing)
                Case Keys.F7
                    btnSave_Click(Nothing, Nothing)
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub frmGoldBarIssue_AJ_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        txtDD.Text = Format(Date.Now.Day, "00")
        txtMM.Text = Format(Date.Now.Month, "00")
        txtYYYY.Text = Date.Now.Year
        clearall()
    End Sub
    Private Function fncGoldBarIssueNo() As Boolean
        Dim Ds As DataSet
        Dim objGoldBarIssue As New ClsGoldBarIssue
        Try
            Ds = objGoldBarIssue.GetUpdateGoldBarIssue
            If Ds.Tables("GoldBarIssue").Rows.Count <= 0 Then
                fncGoldBarIssueNo = False
                MsgBox("Error in generating JobSheet No!", MsgBoxStyle.Critical)
                Exit Function
            Else
                sGoldBarIssueNo = Trim(Ds.Tables("GoldBarIssue").Rows(0)("NextGoldBarIssueNo") & "")
                fncGoldBarIssueNo = True
            End If
            Ds = Nothing
            objGoldBarIssue = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        Finally

        End Try
    End Function
    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        Dim Ds As DataSet
        Dim TotPurchaseweight As Double
        Dim TempPurchaseWeight As Double
        Dim i As Long
        Dim sExec As String
        Dim sDate As String
        Try

            '''      If Not fncValidation() Then Exit Sub
            bProceed = False

            If Not fncGoldBarIssueNo() Then
                Exit Sub
            End If
            If Val(txtNetIssue1.Text) <> Val(txtIssued2.Text) Then
                MessageBox.Show("Issued Weight and Invoice Weight Not Tally")
                Exit Sub
            End If


            sDate = Trim(txtDD.Text) & "/" & Trim(txtMM.Text) & "/" & Trim(txtYYYY.Text)

            If objPurchase.GoldBarIssueProceed(Trim(sGoldBarIssueNo), "", Trim(sDate), txtSupplierCode1.Text.Trim, 0, 0, Val(txtIssued2.Text), Val(txtNetIssue1.Text), "", "", "24K-999", 0, "G", sUserID) Then

            End If


            For i = 0 To grdGoldIssue.RowCount - 1
                If objPurchase.GoldBarIssueProceedInvoice(Trim(sGoldBarIssueNo), Trim(fnGetdgvText(grdGoldIssue, i, 2)), Trim(fnGetdgvText(grdGoldIssue, i, 1)), txtSupplierCode1.Text.Trim, Val(fnGetdgvText(grdGoldIssue, i, 4)), Val(fnGetdgvText(grdGoldIssue, i, 5)), sUserID) Then
                End If

                sExec = "UpDate tblPurchaseFix Set Issuedweight = IssuedWeight + "
                sExec = sExec & Val(fnGetdgvText(grdGoldIssue, i, 5)) & ""
                sExec = sExec & " Where InvoiceNo =" & fncFormatSQLText(fnGetdgvText(grdGoldIssue, i, 2))
                dm.ProjExecNonQry(sExec)

            Next

            For i = 0 To grdIssue1.RowCount - 1
                If objPurchase.GoldBarIssueProceedPurchase(Trim(sGoldBarIssueNo), Trim(fnGetdgvText(grdIssue1, i, 1)), Trim(fnGetdgvText(grdIssue1, i, 2)), Val(fnGetdgvText(grdIssue1, i, 3)), Val(fnGetdgvText(grdIssue1, i, 4)), Val(fnGetdgvText(grdIssue1, i, 5)), Val(fnGetdgvText(grdIssue1, i, 6)), Trim(fnGetdgvText(grdIssue1, i, 7)), sUserID) Then
                End If



                sExec = "UpDate tblinventoryStock Set Pcs = Pcs - "
                sExec = sExec & Val(fnGetdgvText(grdIssue1, i, 4)) & ", QtyonHand = QtyonHand - "
                sExec = sExec & Val(fnGetdgvText(grdIssue1, i, 4))
                sExec = sExec & " Where Inventorycode =" & fncFormatSQLText(fnGetdgvText(grdIssue1, i, 1))
                dm.ProjExecNonQry(sExec)


            Next


            'sExec = "INSERT into tbljobsheetpaymentdetail Values (" & fncFormatSQLText(txtPaymentNo.Text) & ","
            'sExec = sExec & fncFormatSQLText(sPaymentDate) & "," & Val(txtAmount.Text) & ","
            'sExec = sExec & fncFormatSQLText(sRemarks) & "," & fncFormatSQLText(txtHeadInvoiceNo.Text) & ","
            'sExec = sExec & fncFormatSQLDate(txtInvoiceDate.Text) & "," & fncFormatSQLText(txtSupplierCode.Text) & ","
            'sExec = sExec & fncFormatSQLText(cboPaymode.Text) & "," & fncFormatSQLText(sUserID) & ", Getdate() ,"
            'sExec = sExec & fncFormatSQLText(sUserID) & ", Getdate() )"
            'dm.ProjExecNonQry(sExec)

            'sExec = "Update tblJobSheetHeader set PaidAmount=PaidAMount + " & Val(txtAmount.Text) & ", BalanceAmount=BalanceAmount- " & Val(txtAmount.Text) & "Where Jobsheetno=" & fncFormatSQLText(txtJobSheetNo.Text)
            'dm.ProjExecNonQry(sExec)

            'sExec = "Update tblCompany set NextSupplierPayment=NextSupplierPayment + 1"
            'dm.ProjExecNonQry(sExec)


      
            '   txtPayable.Text = Format((GetText(grdUnpaidBill, grdUnpaidBill.Row, 5)), "0.00")
            txtInvoiceNo1.Text = ""
            txtIssued2.Text = ""
            txtNetIssue1.Text = ""
            '''End If
            MessageBox.Show("Issued Sucessfully")
            subPrint(Trim(sGoldBarIssueNo))
            subClear()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
   
    End Sub
    Private Sub subClear()
        Try
            grdGoldIssue.RowCount = 0
            grdIssue1.RowCount = 0
            txtSupplierCode1.Text = ""
            txtsupplierName.Text = ""
            SInvoiceNo = ""
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Sub



    Private Sub btnPanelClose_Click(sender As System.Object, e As System.EventArgs) Handles btnPanelClose.Click
        pnlInvoice.Visible = False
    End Sub

    Private Sub btnInventoryCode_Click(sender As System.Object, e As System.EventArgs) Handles btnInventoryCode.Click

        Dim frmHelpClass As New frmHelpSearch(0, 0, "vwInventoryStock", "L", 4, "InventoryCode", "ClassCode = '999.9' and Qtyonhand > 0", "InventoryCode", "InventoryCode", 80, "Description", "Description", 150, "PurchaseBillNo", "PurchaseBillNo", 50, "ClassCode", "ClassCode", 50)
        frmHelpClass.ShowDialog()
        txtInventoryCode1.Text = frmHelpClass.Var1.Trim
        'subGetInventory(txtInventoryCode1.Text.Trim)
        txtInventoryCode1_LostFocus(sender, e)
    End Sub

    Private Sub subGetInventory(ByVal sInventoryCode As String)
        ', ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim sPMC As Long
        Dim ds As DataSet
        Dim ds1 As DataSet
        Dim objJobSheet As New clsGoldBarPurchase
        ds = objJobSheet.GetInventoryDetail(sInventoryCode.Trim)
        If ds.Tables(0).Rows.Count > 0 Then
            If Val(Trim(ds.Tables(0).Rows(0)("QtyOnHand") & "")) > 0 Then
                txtInventoryCode1.Text = Trim(ds.Tables(0).Rows(0)("Inventorycode") & "")
                txtDescription.Text = Trim(ds.Tables(0).Rows(0)("Description") & "")
                '''txtSupplierCode1.Text = Trim(ds.Tables(0).Rows(0)("SupplierCode") & "")
                TxtTotalWeight.Text = Val(Trim(ds.Tables(0).Rows(0)("Weight") & ""))
                txtCostperGram.Text = Val(Trim(ds.Tables(0).Rows(0)("CostperGram") & ""))
                txtPcs.Text = Val(Trim(ds.Tables(0).Rows(0)("Pcs") & ""))
                txtTotalCost.Text = Val(Trim(ds.Tables(0).Rows(0)("TotalCost") & ""))
                subGetGoldBarNo()
            Else
                MsgBox("No Stock for this Inventory!", MsgBoxStyle.Information)
                txtInventoryCode1.Text = ""
                ' subClearItems()
                txtInventoryCode1.Focus()
            End If
        Else
            MsgBox("This Inventory Not Found... ", MsgBoxStyle.Exclamation)
            txtInventoryCode1.Text = ""
            ' subClearItems()
            txtInventoryCode1.Focus()
        End If


    End Sub

    Private Sub btnPCS_Click(sender As System.Object, e As System.EventArgs) Handles btnPCS.Click
        Try
            subGetGoldBarNo()
        Catch ex As Exception
            MessageBox.Show(Err.Description)
        End Try
    End Sub
    Private Sub subGetGoldBarNo()
        Try
            Dim icnt As Long
            Dim sDate As Date
            Dim dsPur As New DataSet

            ' pnlPCS.Visible = True
            dsPur = objTIS.GetGoldBarNo(txtInventoryCode1.Text.Trim)
            If dsPur.Tables(0).Rows.Count > 0 Then
                'grdPCS1.RowCount = 0

                ' For icnt = 0 To dsPur.Tables(0).Rows.Count - 1
                'grdPCS.MaxRows = grdPCS.MaxRows + 1
                'grdPCS.Row = grdPCS.MaxRows
                'grdPCS1.Rows.Add()
                'Call fnSetdgvText(grdPCS1, icnt, 0, 0)
                ' Call fnSetdgvText(grdPCS1, icnt, 1, Trim(dsPur.Tables(0).Rows(icnt)("BarNo") & ""))

                'Next icnt
                sBarNo = Trim(dsPur.Tables(0).Rows(icnt)("BarNo") & "")
                'grdPCS.Row = 1
                'grdPCS.Col = 1
                'grdPCS.Action = 0
            Else
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub btnPanelPCS_Click(sender As System.Object, e As System.EventArgs) Handles btnPanelPCS.Click
        Dim i As Long
        Dim j As Long
        sBarNo = ""
        j = 0
        For i = 0 To grdPCS1.RowCount - 1
            If Trim(GetText(grdPCS1, i, 0)) = 1 Then
                sBarNo = sBarNo & "," & fncFormatSQLText(GetText(grdPCS1, i, 1))
                j = j + 1
            End If
        Next
        sBarNo = Strings.Right(sBarNo, Len(sBarNo) - 1)
        If Val(j) <> Val(txtPcs.Text) Then
            MsgBox("PCS and Selected BarNo Not Tally")
            Exit Sub
        End If
        pnlPCS.Visible = False
    End Sub

    Private Sub btnPanePCSClose_Click(sender As System.Object, e As System.EventArgs) Handles btnPanePCSClose.Click
        pnlPCS.Visible = False
    End Sub

    Private Sub chkPcsSelectAll_Click(sender As Object, e As System.EventArgs) Handles chkPcsSelectAll.Click
        Dim i As Long
        For i = 0 To grdPCS1.RowCount - 1

            If chkPcsSelectAll.Checked = True Then
                Call SetText(grdPCS1, i, 0, 1)
            Else
                Call SetText(grdPCS1, i, 0, 0)
            End If
        Next
    End Sub

    Private Sub grdPCS1_Click(sender As Object, e As System.EventArgs) Handles grdPCS1.Click
        'Dim i As Long
        'If Trim(GetText(grdPCS1, grdPCS1., 1)) = 0 Or Trim(GetText(grdPCS1, grdPCS1.CurrentRow, 1)) = "" Then
        '    Call SetText(grdPCS1, grdPCS1.CurrentRow, 1, 1)
        'Else
        '    Call SetText(grdPCS1, grdPCS1.CurrentRow, 1, 0)
        'End If
    End Sub

    Private Sub btnIssueOk_Click(sender As System.Object, e As System.EventArgs) Handles btnIssueOk.Click
        Dim i As Long
        dIssueWeight = 0
        For i = 0 To grdGoldIssue.RowCount - 1
            dIssueWeight = dIssueWeight + Val(fnGetdgvText(grdGoldIssue, i, 5))
        Next
        txtIssued2.Text = Val(dIssueWeight)
        txtIssued2.Text = CDbl(txtIssued2.Text)
    End Sub

    Private Sub txtInventoryCode1_LostFocus(sender As Object, e As System.EventArgs) Handles txtInventoryCode1.LostFocus
        If Trim(txtInventoryCode1.Text) <> "" Then
            subGetInventory(txtInventoryCode1.Text.Trim)
        End If
    End Sub

    

    Private Sub btnIssueNo_Click(sender As System.Object, e As System.EventArgs) Handles btnIssueNo.Click
        Dim frmHelpClass As New frmHelpSearch(0, 0, "tblGoldBarIssued", "L", 4, "IssueNo", "", "IssueNo", "IssueNo", 80, "PDate", "PDate", 60, "SupplierCode", "SupplierCode", 60, "IssuedWeight", "IssuedWeight", 80)
        frmHelpClass.ShowDialog()
        txtIssueNo1.Text = frmHelpClass.Var1.Trim
    End Sub


    Public Sub subGetSupplier(ByVal sender As Object, ByVal e As System.EventArgs)
        Try


            Dim sSql As String
            Dim DR As DataRow()
            If Trim(txtSupplierCode1.Text) <> "" Then
                sSql = "Select * from tblSupplier where SupplierCode=" & fncFormatSQLText(txtSupplierCode1.Text)
                DR = DataManager.RowReturnProjExecNonQryWithDataRow(sSql)
                If DR.Length > 0 Then
                    txtsupplierName.Text = Trim(DR(0)("SupplierName") & "")

                Else
                    MsgBox("Invalid supplier code!", vbCritical)
                    txtSupplierCode1.Text = ""
                    txtsupplierName.Text = ""
                End If
            Else
                txtSupplierCode1.Text = ""
                txtsupplierName.Text = ""
            End If

            Exit Sub
        Catch ex As Exception
        End Try
    End Sub
End Class