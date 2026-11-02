Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmTransferIn

    Dim bEdited As Boolean
    Dim DM As New DataManager
    Dim sTransferReferenceNumber As String
    Dim sTransferNo, sTransferDate, sDiamondDescription, sFromLocation, sToLocation, sRemarks, sFromLocStatus, sToLocStatus, sInventoryCode As String
    Dim dTotalweight, dTotalCost, dTotalPCS, dTotalPMC, dTotalSMC, dWeight, dCostPerGram, dPMC, dSMC, dPCS, dQtyOnHand, dQtySold, dTransferTag, dSizeCode As Double
    Dim dTotalDisplayPCS, dSelectDisplayPCS As Double
    Dim msSalesManCode As String


    Private Sub cmdFetch_Click(sender As System.Object, e As System.EventArgs) Handles cmdFetch.Click
        Dim sSql As String
        Dim dr As DataRow()
        Dim i As Integer
        Dim sTransferType As String
        Try
            If Trim(txtTransferNo.Text) = "" Then
                MessageBox.Show("Please enter the transfer no.")
                If txtTransferNo.Enabled Then txtTransferNo.Focus()
                Exit Sub
            End If

            If fncTransferInventoryToLocal(txtTransferNo.Text.Trim) Then
                'chkSelectAll.Checked = True
                fncFetchTransferDetails(txtTransferNo.Text.Trim)
            Else
                'clear the form
                fncClearData()
                fncClearGrid()
                If txtTransferNo.Enabled Then txtTransferNo.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Function fncTransferInventoryToLocal(ByVal sTransNo As String) As Boolean
        Dim sQry As String
        Dim drH As DataRow()
        Dim drD As DataRow()
        Dim bStatus As Boolean
        Dim iCnt As Integer
        Dim objTransferIn As New ClsTransferIn
        Try
            bStatus = False
            'fetch header info
            'Shabeer 2015 01 16
            'If Trim(gthisLocationType) = "HQ" Then
            '    sQry = "select * from LocalInventoryTransferHeader where TransferNo=" & fncFormatSQLText(sTransNo)
            '    'sQry &= " and ToLocation = " & fncFormatSQLText(gthisLocation)
            '    drH = DataManager.RowReturnProjExecNonQryWithDataRow(sQry)
            'Else
            '    sQry = "select * from tblInventoryTransferHeader where TransferNo=" & fncFormatSQLText(sTransNo)
            '    sQry &= " and ToLocation = " & fncFormatSQLText(gthisLocation)
            '    drH = DataManager.RowReturnProjExecNonQryWithDataRowAcc(sQry)
            'End If
            sQry = "select * from tblInventoryTransferHeader where TransferNo=" & fncFormatSQLText(sTransNo)
            sQry &= " and ToLocation = " & fncFormatSQLText(gthisLocation)
            drH = DataManager.RowReturnProjExecNonQryWithDataRowAcc(sQry)
            'fetch detail info
            sQry = "select * from  tblInventoryTransferDetail where TransferNo=" & fncFormatSQLText(sTransNo)
            drD = DataManager.RowReturnProjExecNonQryWithDataRowAcc(sQry)

            objTransferIn.subOpenTransaferConnection()
            objTransferIn.subBeginTransaferTransaction()
            If drH.Length > 0 Then
                sTransferNo = Trim(drH(0)("TransferNo") & "")
                sTransferDate = Trim(drH(0)("TransferDate") & "")
                sFromLocation = Trim(drH(0)("FromLocation") & "")
                sToLocation = Trim(drH(0)("ToLocation") & "")
                sRemarks = Trim(drH(0)("Remarks") & "")
                dTotalweight = Trim(drH(0)("Totalweight") & "")
                dTotalCost = Trim(drH(0)("TotalCost") & "")
                dTotalPCS = Trim(drH(0)("TotalPCS") & "")
                dTotalPMC = Trim(drH(0)("TotalPMC") & "")
                dTotalSMC = Trim(drH(0)("TotalSMC") & "")
                sFromLocStatus = Trim(drH(0)("FromLocStatus") & "")
                sToLocStatus = Trim(drH(0)("ToLocStatus") & "")
                If objTransferIn.fncTransferHeadertoLocal(sTransferNo, sTransferDate, sFromLocation, sFromLocStatus, _
                                        sToLocation, sToLocStatus, sRemarks, dTotalweight, dTotalCost, _
                                        dTotalPCS, dTotalPMC, dTotalSMC, _
                                        sUserID) = False Then
                    MessageBox.Show("Error in fetching header information.")
                    objTransferIn.subRollBackTransaferTransaction()
                    Exit Function
                End If


                If (drD.Length > 0) Then
                    For iCnt = 0 To drD.Length - 1
                        sTransferNo = Trim(drD(iCnt)("TransferNo") & "")
                        sInventoryCode = Trim(drD(iCnt)("InventoryCode") & "")
                        sDescription = Trim(drD(iCnt)("Description") & "")
                        dQtyOnHand = Val(drD(iCnt)("QtyOnHand") & "")
                        dWeight = Val(drD(iCnt)("Weight") & "")
                        dSizeCode = Val(drD(iCnt)("SizeCode") & "")
                        dCostPerGram = Val(drD(iCnt)("CostPerGram") & "")
                        dPMC = Val(drD(iCnt)("PMC") & "")
                        dSMC = Val(drD(iCnt)("SMC") & "")
                        dPCS = Val(drD(iCnt)("PCS") & "")
                        dTotalCost = Val(drD(iCnt)("TotalCost") & "")
                        dTransferTag = (IIf(Trim(drD(iCnt)("TransferTag") & "") & "" = True, 1, 0))
                        If objTransferIn.fncTransferDetailtoLocal(sTransferNo, sInventoryCode, sDescription, _
                                 Trim(drD(iCnt)("ClassCode") & ""), Trim(drD(iCnt)("CategoryCode") & ""), Trim(drD(iCnt)("CatalogueCode") & ""), _
                                dQtyOnHand, dWeight, dSizeCode, dCostPerGram, dPMC, dSMC, dPCS, dTotalCost, dTransferTag, sUserID) = False Then
                            MessageBox.Show("Error in fetching detail information.")
                            objTransferIn.subRollBackTransaferTransaction()
                            Exit Function
                        End If
                    Next
                End If
                objTransferIn.subCommitTransaferTransaction()
                bStatus = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            objTransferIn.subCloseTransaferConnection()
            objTransferIn = Nothing
            fncTransferInventoryToLocal = bStatus
        End Try
    End Function
    Private Function fncUpdateInventoryMovement(ByVal sTransNo As String) As Boolean
        Dim sQry As String
        Dim drH As DataRow()
        Dim drD As DataRow()
        Dim bStatus As Boolean
        Dim iCnt As Integer
        Dim objTransferIn As New ClsTransferIn
        Try
            bStatus = False
            'fetch header info
            If Trim(gthisLocationType) = "HQ" Then
                sQry = "select * from tblInventoryTransferHeader where TransferNo=" & fncFormatSQLText(sTransNo)
                'sQry &= " and ToLocation = " & fncFormatSQLText(gthisLocation)
                drH = DataManager.RowReturnProjExecNonQryWithDataRow(sQry)
            Else
                sQry = "select * from tblInventoryTransferHeader where TransferNo=" & fncFormatSQLText(sTransNo)
                sQry &= " and ToLocation = " & fncFormatSQLText(gthisLocation)
                drH = DataManager.RowReturnProjExecNonQryWithDataRowAcc(sQry)
            End If
            'fetch detail info
            sQry = "select * from  tblInventoryTransferDetail where TransferNo=" & fncFormatSQLText(sTransNo)
            drD = DataManager.RowReturnProjExecNonQryWithDataRowAcc(sQry)

            objTransferIn.subOpenTransaferConnection()
            objTransferIn.subBeginTransaferTransaction()
            If (drD.Length > 0) Then
                For iCnt = 0 To drD.Length - 1
                    sTransferNo = Trim(drD(iCnt)("TransferNo") & "")
                    sInventoryCode = Trim(drD(iCnt)("InventoryCode") & "")
                    sDescription = Trim(drD(iCnt)("Description") & "")
                    dQtyOnHand = Val(drD(iCnt)("QtyOnHand") & "")
                    dWeight = Val(drD(iCnt)("Weight") & "")
                    dSizeCode = Val(drD(iCnt)("SizeCode") & "")
                    dCostPerGram = Val(drD(iCnt)("CostPerGram") & "")
                    dPMC = Val(drD(iCnt)("PMC") & "")
                    dSMC = Val(drD(iCnt)("SMC") & "")
                    dPCS = Val(drD(iCnt)("PCS") & "")
                    dTotalCost = Val(drD(iCnt)("TotalCost") & "")
                    dTransferTag = (IIf(Trim(drD(iCnt)("TransferTag") & "") & "" = True, 1, 0))
                    If objTransferIn.fncInventoryMovementSave(Trim(gthisLocation), Trim(sInventoryCode), Trim(sDescription), "TI", Trim(sTransferNo), sTransferDate, _
                                       Val(dQtyOnHand), Val(dWeight), Val(dSizeCode), Val(dPMC), _
                                       Val(dSMC), Val(dCostPerGram), Val(dTotalCost), _
                                        "", sUserID) = False Then
                        MessageBox.Show("Error in fetching detail information.")
                        objTransferIn.subRollBackTransaferTransaction()
                        Exit Function
                    End If
                Next
            End If
            objTransferIn.subCommitTransaferTransaction()
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            objTransferIn.subCloseTransaferConnection()
            objTransferIn = Nothing
            fncUpdateInventoryMovement = bStatus
        End Try
    End Function
    Private Function fncFetchTransferDetails(ByVal sTransNo As String) As Boolean
        Dim sSql As String
        Dim iCnt, j As Integer
        Dim bStatus As Boolean
        Dim drH As DataRow()
        Dim drD As DataRow()
        Dim drI As DataRow()
        Try
            bStatus = False
            sSql = "Select TotalCost, TotalPCS, TotalWeight from LocalInventoryTransferHeader Where TransferNo = " & fncFormatSQLText(sTransNo)
            drH = DataManager.RowReturnProjExecNonQryWithDataRow(sSql)
            If drH.Length > 0 Then
                'txtTotalCost.Text = Val(drH(0)("TotalCost") & "")
                'txtTotalWeight.Text = Val(drH(0)("TotalWeight") & "")
                'txtTotalPCS.Text = Val(drH(0)("TotalPCS") & "")
                'txtStoneCost.Text = "0.00"
            Else
                MessageBox.Show("No data found in header.")
                Exit Function
            End If

            dgvTransfer.RowCount = 0
            sSql = "Select * From LocalInventoryTransferDetail Where TransferNo = " & fncFormatSQLText(sTransNo)
            drD = DataManager.RowReturnProjExecNonQryWithDataRow(sSql)
            If drD.Length > 0 Then
                For iCnt = 0 To drD.Length - 1
                    dgvTransfer.Rows.Add()

                    fnSetdgvText(dgvTransfer, iCnt, 0, Trim(drD(iCnt)("InventoryCode") & ""))
                    fnSetdgvText(dgvTransfer, iCnt, 1, Trim(drD(iCnt)("Description") & ""))

                    fnSetdgvText(dgvTransfer, iCnt, 2, Trim(drD(iCnt)("Weight") & ""))
                    fnSetdgvText(dgvTransfer, iCnt, 3, Trim(drD(iCnt)("SizeCode") & ""))

                    fnSetdgvText(dgvTransfer, iCnt, 4, Trim(drD(iCnt)("TotalCost") & ""))
                    fnSetdgvText(dgvTransfer, iCnt, 5, Trim(drD(iCnt)("TransferTag") & ""))
                    drI = Nothing
                Next iCnt
                dTotalDisplayPCS = iCnt
                lblDisplayPCS.Text = Val(dTotalDisplayPCS)
            Else
                MessageBox.Show("No data found in detail.")
                Exit Function
            End If
            '''   subCalcTotal()
            bStatus = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            drH = Nothing
            drI = Nothing
            drD = Nothing
        End Try
    End Function
    Private Sub fncClearData()
        txtFromLocationCode.Text = ""
        'txtStoneCost.Text = ""
        'txtTotalCost.Text = ""
        'txtTotalItem.Text = ""
        'txtTotalPCS.Text = ""
        'txtTotalWeight.Text = ""
        txtTransferDate.Text = ""
        txtTransferMonth.Text = ""
        txtTransferNo.Text = ""
        txtTransferYear.Text = ""
        txtTransferNo.ReadOnly = False
        lblDisplayPCS.Text = "00"
        lblSelectDisplayPCS.Text = "00"
    End Sub

    Private Sub fncClearGrid()
        dgvTransfer.RowCount = 0
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(ByVal sTransferNumber As String)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        sTransferReferenceNumber = sTransferNumber
    End Sub
    Private Sub txtInventoryCode_Leave(sender As Object, e As System.EventArgs) Handles txtInventoryCode.Leave
        Dim iRow As Long
        Dim iSelect As Long
        iSelect = 0
        For iRow = 0 To dgvTransfer.Rows.Count - 1
            'If Trim(GetText(grdInvoice, i, 1)) = 1 Then
            '    Call SetText(grdInvoice, i, 1, 0)
            'Else
            If Trim(fnGetdgvText(dgvTransfer, iRow, 0)) = Trim(txtInventoryCode.Text) Then
                If Trim(fnGetdgvText(dgvTransfer, iRow, 5)) = True Then
                    MessageBox.Show("Inventory already Exists in this Transfer List")
                    txtInventoryCode.Text = ""
                    txtInventoryCode.Focus()
                    Exit Sub
                End If
                Call fnSetdgvText(dgvTransfer, iRow, 5, True)
                'Else
                '    Call SetText(grdInventory, i, 1, 0)
                'MessageBox.Show("Inventory Not Found in this Transfer List")
                'Exit Sub
                iSelect = iSelect + 1
            End If

            'End If
        Next
        dSelectDisplayPCS = dSelectDisplayPCS + iSelect
        lblSelectDisplayPCS.Text = Val(dSelectDisplayPCS)
        txtInventoryCode.Text = ""
        If txtInventoryCode.Enabled Then txtInventoryCode.Focus()
        If Val(lblSelectDisplayPCS.Text) = Val(lblDisplayPCS.Text) Then
            btnAccept.Enabled = True
        End If
    End Sub

    Private Sub btnAccept_Click(sender As System.Object, e As System.EventArgs) Handles btnAccept.Click

        'PnlSalesManPwd.Visible = True
        'txtSalesManPWD.Text = ""
        ''If PnlSalesManPwd.Enabled Then

        ''End If
        'txtInventoryCode.Enabled = False
        'txtSalesManPWD.Focus()


        Dim sTransferType As String
        Try
            Cursor.Current = Cursors.WaitCursor
            'If txtSalesManPWD.Text.Trim <> "" Then
            '    If subGetPassword() = False Then Exit Sub
            If txtTransferNo.Text <> "" Then
                sTransferNo = Trim(txtTransferNo.Text)
                If fncValidate() = True Then
                    If fncCheckAcceptanceStatus(sTransferNo, gthisLocation) = True Then
                        MessageBox.Show("Transfer Already Accepted.")
                        Exit Sub
                    End If
                    If fncAcceptTransfer(sTransferNo) Then
                        MessageBox.Show("Inventory Transferred Successfully")
                        If btnTransferClear.Enabled Then btnTransferClear_Click(sender, e)
                        'If MsgBox("Do you want to print ?", vbYesNo + MsgBoxStyle.Information) = MsgBoxResult.Yes Then
                        '    subPrintTransferIN(Trim(sTransferNo))
                        'End If
                    Else
                        MessageBox.Show("Inventory Not Accepted")
                    End If
                End If
            Else
                MessageBox.Show("Enter Transfer Number ")
            End If
            'Else
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor.Current = Cursors.Default
        End Try


    End Sub
    Private Sub btnTransferClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransferClear.Click
        Try
            If MsgBox("Do you want to clear all details ? ", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                fncClearData()
                fncClearGrid()
                If txtTransferNo.Enabled Then txtTransferNo.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subPrintTransferIN(ByVal sTransferNo As String)
        Dim sSql As String
        Dim sCompanyname, sAddress, sReportTitle, sPath, sPhone, sFax, sSelectFormula, sPrintFDate, sPrintTDate, sFromDate, sToDate As String
        Dim CR As New ReportDocument
        Dim frmRep As New frmReports
        Dim sSelectFld As String
        Dim dsCompany As DataSet
        Dim objClsCompany As New clsCompany1
        Try
            ' If UCase(sCompanyShortCode) = "JH" Then
            '    sPath = Application.StartupPath & "\Reports\rptTransferOutDetail_JH.rpt"
            'Else
            sPath = Application.StartupPath & "\Reports\rptTRansferInDetail.rpt"
            'End If
            sReportTitle = "Transfer Note"
            sSql = " {tblInventoryTransferHeader.TransferNo} = " & fncFormatSQLText(sTransferNo)
            dsCompany = objClsCompany.getCompanyName
            If dsCompany.Tables("GetCompanyName").Rows.Count > 0 Then
                sCompanyname = Trim(dsCompany.Tables("GetCompanyName").Rows(0)("Name") & "")
                sAddress = Trim(dsCompany.Tables("GetCompanyName").Rows(0)("Address1") & "") & " " & _
                             Trim(dsCompany.Tables("GetCompanyName").Rows(0)("Address2") & "") & " " & _
                             Trim(dsCompany.Tables("GetCompanyName").Rows(0)("Country") & "")
                sPhone = Trim(dsCompany.Tables("GetCompanyName").Rows(0)("Phone") & "")
                sFax = Trim(dsCompany.Tables("GetCompanyName").Rows(0)("Fax") & "")
            End If
            CR.Load(sPath)
            ReportLogOnToServer(CR)
            CR.DataDefinition.FormulaFields("CompAddress1").Text = fncFormatSQLText(sCompanyname)
            CR.DataDefinition.FormulaFields("CompAddress2").Text = fncFormatSQLText(sAddress)
            CR.DataDefinition.FormulaFields("ReportTitle").Text = fncFormatSQLText(sReportTitle)
            CR.DataDefinition.FormulaFields("RPTSELECTION").Text = fncFormatSQLText(sSelectFld)
            CR.DataDefinition.FormulaFields("CompAddress3").Text = fncFormatSQLText(sPhone)
            CR.DataDefinition.FormulaFields("CompAddress4").Text = fncFormatSQLText(sFax)
            CR.DataDefinition.RecordSelectionFormula = sSql

            CR.Refresh()
            frmRep.ReportViewer.SelectionFormula = sSql
            frmRep.ReportViewer.ReportSource = CR
            frmRep.ShowDialog()
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
    Private Function fncAcceptTransfer(ByVal sTransferNo As String) As Boolean
        Dim i As Integer
        Dim drInventory() As DataRow
        Dim drInventoryAddl() As DataRow
        Dim drInventoryBarCoin() As DataRow
        Dim drDetail() As DataRow
        Dim drHQDetail() As DataRow
        Dim sInventoryCode, sToLocation, sFromLocation As String
        Dim objTransferIn As New ClsTransferIn
        Dim bStatus As Boolean
        Dim sQry As String
        Dim drH As DataRow()
        Dim drD As DataRow()
        Dim iCnt As Integer
        Try
            bStatus = False
            objTransferIn.subOpenTransaferConnection()
            objTransferIn.subBeginTransaferTransaction()
            objTransferIn.subOpenTransaferConnectionHQ()
            objTransferIn.subBeginTransaferTransactionHQ()

            gsConBatchPosting.ConnectionString = gsCon
            gsConBatchPosting.Open()
            gsTrans = gsConBatchPosting.BeginTransaction

            sQry = "Select * from vwInventoryStockTransfer  where "
            sQry = sQry & " InventoryCode in (Select InventoryCode From tblInventoryTransferDetail Where TransferNo = " & fncFormatSQLText(txtTransferNo.Text.Trim) & ")"
            sQry = sQry & " and (Locationcode in( " & fncFormatSQLText(gthisLocation) & "))"
            drInventory = DataManager.RowReturnProjExecNonQryWithDataRowAcc(sQry)
            If drInventory.Length > 0 Then
                If objTransferIn.fncInsertInventoryAndInventoryStock(drInventory) = False Then
                    MessageBox.Show("Error in inserting inventory detail")
                    objTransferIn.subRollBackTransaferTransaction()
                    objTransferIn.subRollBackTransaferTransactionHQ()
                    gsTrans.Rollback()
                    gsConBatchPosting.Close()
                Else
                    gsTrans.Commit()
                    gsConBatchPosting.Close()
                End If
            End If


            sQry = "Select * from tblInventoryAddlDescription  where "
            sQry = sQry & " InventoryCode in (Select InventoryCode From tblInventoryTransferDetail Where TransferNo = " & fncFormatSQLText(txtTransferNo.Text.Trim) & ")"
            drInventoryAddl = DataManager.RowReturnProjExecNonQryWithDataRowAcc(sQry)
            If drInventoryAddl.Length > 0 Then
                If objTransferIn.fncInsertInventoryAddlDescription(drInventoryAddl) = False Then
                    MessageBox.Show("Error in inserting inventory Additional Description detail")
                    objTransferIn.subRollBackTransaferTransaction()
                    objTransferIn.subRollBackTransaferTransactionHQ()

                End If
            End If

            sQry = "Select * from tblInventoryBarCoin  where "
            sQry = sQry & " InventoryCode in (Select InventoryCode From tblInventoryTransferDetail Where TransferNo = " & fncFormatSQLText(txtTransferNo.Text.Trim) & ")"
            drInventoryBarCoin = DataManager.RowReturnProjExecNonQryWithDataRowAcc(sQry)
            If drInventoryBarCoin.Length > 0 Then
                If objTransferIn.fncInsertInventoryBarCoin(drInventoryBarCoin) = False Then
                    MessageBox.Show("Error in inserting Inventory Bar Details")
                    objTransferIn.subRollBackTransaferTransaction()
                    objTransferIn.subRollBackTransaferTransactionHQ()
                End If
            End If

            sQry = "Select Inv.InventoryCode, Inv.Weight, Inv.PMC, Inv.Description,Inv.DiamondDescription,Inv.SMC, Inv.PCS, Inv.TotalCost, Itd.Weight TransferredWeight, Itd.PMC TransferredPMC, Itd.SMC TransferredSMC,Itd.QtyOnHand TransferredPCS, Itd.TotalCost TransferredTotalCost, Itd.QtyOnHand TransferredQtyOnHand, C.PurchaseType,Inv.GoldLoss,Inv.SizeCode"
            sQry &= " from vwInventoryStock Inv with(nolock), tblClass C with(nolock), tblInventoryTransferDetail  Itd with(nolock) Where C.ClassCode = Inv.ClassCode And Itd.InventoryCode = Inv.InventoryCode And Itd.TransferNo =  " & fncFormatSQLText(txtTransferNo.Text.Trim)
            drDetail = DataManager.RowReturnProjExecNonQryWithDataRowAcc(sQry)
            If drDetail.Length > 0 Then
                If objTransferIn.fncUpdateInventoryStock(drDetail) = False Then
                    MessageBox.Show("Error in updating inventory detail")
                    objTransferIn.subRollBackTransaferTransaction()
                    objTransferIn.subRollBackTransaferTransactionHQ()
                    Exit Function
                End If
            End If

            If Trim(gthisLocationType) <> "HQ" Then

                sQry = "Select Inv.InventoryCode, Inv.Weight, Inv.PMC, Inv.Description,Inv.DiamondDescription,Inv.SMC, Inv.PCS, Inv.TotalCost, Itd.Weight TransferredWeight, Itd.PMC TransferredPMC, Itd.SMC TransferredSMC,Itd.QtyOnHand TransferredPCS, Itd.TotalCost TransferredTotalCost, Itd.QtyOnHand TransferredQtyOnHand, C.PurchaseType,Inv.GoldLoss,Inv.SizeCode"
                sQry &= " from vwInventoryStock Inv with(nolock), tblClass C with(nolock), LocalInventoryTransferDetail  Itd with(nolock) Where C.ClassCode = Inv.ClassCode And Itd.InventoryCode = Inv.InventoryCode And Itd.TransferNo =  " & fncFormatSQLText(txtTransferNo.Text.Trim)
                drHQDetail = DataManager.RowReturnProjExecNonQryWithDataRow(sQry)
                If drHQDetail.Length > 0 Then
                    If objTransferIn.fncUpdateInventoryStockHQ(drHQDetail) = False Then
                        MessageBox.Show("Error in inserting inventory detail")
                        objTransferIn.subRollBackTransaferTransaction()
                        objTransferIn.subRollBackTransaferTransactionHQ()
                        Exit Function
                    End If
                End If

            End If

            If Not objTransferIn.fncUpdateTransferHeaderHQ(Trim(sTransferNo), sUserID) Then
                objTransferIn.subRollBackTransaferTransaction()
                objTransferIn.subRollBackTransaferTransactionHQ()
                MessageBox.Show("Error in updating status in HQ.")
                Exit Function
            End If

            If Not objTransferIn.fncUpdateTransferHeaderLocal(Trim(sTransferNo), Trim(msSalesManCode), sUserID) Then
                objTransferIn.subRollBackTransaferTransaction()
                objTransferIn.subRollBackTransaferTransactionHQ()
                MessageBox.Show("Error in updating transfer in local.")
                Exit Function
            End If

            sQry = "select * from  tblInventoryTransferDetail where TransferNo=" & fncFormatSQLText(sTransferNo)
            drD = DataManager.RowReturnProjExecNonQryWithDataRowAcc(sQry)

            'objTransferIn.subOpenTransaferConnection()
            'objTransferIn.subBeginTransaferTransaction()
            If (drD.Length > 0) Then
                For iCnt = 0 To drD.Length - 1
                    sTransferNo = Trim(drD(iCnt)("TransferNo") & "")
                    sInventoryCode = Trim(drD(iCnt)("InventoryCode") & "")
                    sDescription = Trim(drD(iCnt)("Description") & "")
                    dQtyOnHand = Val(drD(iCnt)("QtyOnHand") & "")
                    dWeight = Val(drD(iCnt)("Weight") & "")
                    dSizeCode = Val(drD(iCnt)("SizeCode") & "")
                    dCostPerGram = Val(drD(iCnt)("CostPerGram") & "")
                    dPMC = Val(drD(iCnt)("PMC") & "")
                    dSMC = Val(drD(iCnt)("SMC") & "")
                    dPCS = Val(drD(iCnt)("PCS") & "")
                    dTotalCost = Val(drD(iCnt)("TotalCost") & "")
                    dTransferTag = (IIf(Trim(drD(iCnt)("TransferTag") & "") & "" = True, 1, 0))
                    If objTransferIn.fncInventoryMovementSave(Trim(gthisLocation), Trim(sInventoryCode), Trim(sDescription), "TI", Trim(sTransferNo), sTransferDate, _
                                       Val(dQtyOnHand), Val(dWeight), Val(dSizeCode), Val(dPMC), _
                                       Val(dSMC), Val(dCostPerGram), Val(dTotalCost), _
                                        "", sUserID) = False Then
                        MessageBox.Show("Error in fetching detail information.")
                        objTransferIn.subRollBackTransaferTransaction()
                        objTransferIn.subRollBackTransaferTransactionHQ()
                        Exit Function
                    End If
                Next
            End If

            objTransferIn.subCommitTransaferTransaction()
            objTransferIn.subCommitTransaferTransactionHQ()
           
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            objTransferIn.subRollBackTransaferTransaction()
            objTransferIn.subRollBackTransaferTransactionHQ()
            gsTrans.Rollback()
        Finally
            objTransferIn.subCloseTransaferConnection()
            objTransferIn.subCloseTransaferConnectionHQ()
            objTransferIn = Nothing
            fncAcceptTransfer = bStatus
        End Try
    End Function
    Private Function fncCheckAcceptanceStatus(ByVal sTransferNo, ByVal gThisLocation) As Boolean
        Dim sQry As String
        Dim sFromLocStatus, sToLocStatus As String
        Dim dr As DataRow()
        Dim bStatus As Boolean
        Try
            bStatus = False
            sQry = "select FromLocStatus,ToLocStatus from LocalInventoryTransferHeader "
            sQry &= " where TransferNo = " & fncFormatSQLText(sTransferNo)
            sQry &= " and ToLocation = " & fncFormatSQLText(gThisLocation)
            dr = DataManager.RowReturnProjExecNonQryWithDataRow(sQry)
            If dr.Length > 0 Then
                sFromLocStatus = Trim(dr(0)("FromLocStatus") & "")
                sToLocStatus = Trim(dr(0)("ToLocStatus") & "")
            End If
            If Trim(sFromLocStatus) = "2" And Trim(sToLocStatus) = "2" Then
                bStatus = True
            ElseIf Trim(sFromLocStatus) = "2" And Trim(sToLocStatus) = "0" Then
                bStatus = False
            Else
                MessageBox.Show("Error in Transfer Status ")
                bStatus = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dr = Nothing
            fncCheckAcceptanceStatus = bStatus
        End Try
    End Function
    Private Function fncValidate() As Boolean
        Dim bStatus As Boolean
        Try
            bStatus = False
            If Trim(txtTransferNo.Text) = "" Then
                MessageBox.Show("Enter Transfer No.")
                If txtTransferNo.Enabled Then txtTransferNo.Focus()
                Exit Function
            End If
            If txtTransferDate.Text = "" Or txtTransferMonth.Text = "" Or txtTransferYear.Text = "" Then
                MessageBox.Show("Enter Date")
                If txtTransferDate.Enabled Then txtTransferDate.Focus()
                Exit Function
            End If
            If txtFromLocationCode.Text = "" Then
                MessageBox.Show("From Location is Empty")
                Exit Function
            End If
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncValidate = bStatus
        End Try
    End Function
    Private Function fncValidateTransferNo(ByVal sTransNo) As Boolean
        Dim sQry, sTransferDate As String
        Dim bStatus As Boolean
        Dim dr As DataRow()
        Dim sTransferType As String
        Try
            bStatus = False
            If Not sTransNo = "" Then

                If Trim(gthisLocationType) = "HQ" Then
                    sQry = "Select TransferNo,FromLocation,TransferDate from tblInventoryTransferheader   where TransferNo=" & fncFormatSQLText(sTransNo)
                    'sQry &= " and ToLocation = " & fncFormatSQLText(gthisLocation)
                    dr = DataManager.RowReturnProjExecNonQryWithDataRow(sQry)
                Else
                    sQry = "Select TransferNo,FromLocation,TransferDate from tblInventoryTransferheader   where TransferNo=" & fncFormatSQLText(sTransNo)
                    sQry &= " and ToLocation = " & fncFormatSQLText(gthisLocation)
                    dr = DataManager.RowReturnProjExecNonQryWithDataRowAcc(sQry)
                End If
                If Not dr.Length > 0 Then
                    MsgBox("Enter valid Transfer No")
                    txtTransferNo.Text = ""
                    If txtTransferNo.Enabled Then txtTransferNo.Focus()
                Else
                    txtFromLocationCode.Text = Trim(dr(0)("FromLocation") & "")
                    sTransferDate = Trim(dr(0)("TransferDate") & "")
                    txtTransferDate.Text = Trim(sTransferDate.Substring(0, 2))
                    txtTransferMonth.Text = Trim(sTransferDate.Substring(3, 2))
                    txtTransferYear.Text = Trim(sTransferDate.Substring(6, 4))
                    txtTransferNo.ReadOnly = True
                    bStatus = True
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dr = Nothing
            sQry = Nothing
            fncValidateTransferNo = bStatus
        End Try

    End Function

    Private Sub txtTransferNo_Leave(sender As Object, e As System.EventArgs) Handles txtTransferNo.Leave
        Try
            If fncValidateTransferNo(txtTransferNo.Text.Trim) Then
                If cmdFetch.Enabled Then cmdFetch_Click(sender, e)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub dgvTransfer_Click(sender As Object, e As System.EventArgs) Handles dgvTransfer.Click
        Dim iSelect As Long
        iSelect = 0
        If Trim(fnGetdgvText(dgvTransfer, dgvTransfer.CurrentRow.Index, 5)) = False Then
            Call fnSetdgvText(dgvTransfer, dgvTransfer.CurrentRow.Index, 5, True)
            iSelect = iSelect + 1
        Else
            Call fnSetdgvText(dgvTransfer, dgvTransfer.CurrentRow.Index, 5, False)
            iSelect = -1
        End If
        dSelectDisplayPCS = dSelectDisplayPCS + iSelect
        lblSelectDisplayPCS.Text = Val(dSelectDisplayPCS)
        If Val(lblSelectDisplayPCS.Text) = Val(lblDisplayPCS.Text) Then
            btnAccept.Enabled = True
        End If
    End Sub

    Private Sub frmTransferIn_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
                    btnTransferClear_Click(Nothing, Nothing)
                Case Keys.F7
                    btnAccept.Focus()
                    btnAccept_Click(sender, e)
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub frmTransferIn_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If sTransferReferenceNumber Is Nothing Then
            sTransferReferenceNumber = ""
        Else
            If sTransferReferenceNumber <> "" Then
                txtTransferNo.Text = sTransferReferenceNumber
                cmdFetch_Click(sender, e)
            End If
        End If
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub txtSalesManPWD_LostFocus(sender As Object, e As System.EventArgs) Handles txtSalesManPWD.LostFocus

        Dim sTransferType As String
        Try
            Cursor.Current = Cursors.WaitCursor
            If txtSalesManPWD.Text.Trim <> "" Then
                If subGetPassword() = False Then Exit Sub
                If txtTransferNo.Text <> "" Then
                    sTransferNo = Trim(txtTransferNo.Text)
                    If fncValidate() = True Then
                        If fncCheckAcceptanceStatus(sTransferNo, gthisLocation) = True Then
                            MessageBox.Show("Transfer Already Accepted.")
                            Exit Sub
                        End If
                        If fncAcceptTransfer(sTransferNo) Then
                            MessageBox.Show("Inventory Transferred Successfully")
                            If btnTransferClear.Enabled Then btnTransferClear_Click(sender, e)
                            'If MsgBox("Do you want to print ?", vbYesNo + MsgBoxStyle.Information) = MsgBoxResult.Yes Then
                            '    subPrintTransferIN(Trim(sTransferNo))
                            'End If
                        Else
                            MessageBox.Show("Inventory Not Accepted")
                        End If
                    End If
                Else
                    MessageBox.Show("Enter Transfer Number ")
                End If
            Else
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor.Current = Cursors.Default
        End Try



    End Sub
    Private Function subGetPassword() As Boolean
        Dim i As Long
        Dim Ds As DataSet
        Dim objSalesMan As New clsSalesMan
        Try
            subGetPassword = False
            Ds = objSalesMan.GetSalesManwithPassword(txtSalesManPWD.Text.Trim)
            If Ds.Tables("Salesman").Rows.Count > 0 Then
                msSalesManCode = Trim(Ds.Tables("Salesman").Rows(0)("SalesmanCode") & "")
                '  txtSalesMan.Text = Trim(Ds.Tables("Salesman").Rows(0)("SalesManName") & "")
                ' nAllowSalesReturn = Ds.Tables("Salesman").Rows(0)("AllowSalesReturn") & ""
            Else
                MsgBox("SalesMan Password is Incorrect!", MsgBoxStyle.Critical)
                txtSalesManPWD.Text = ""
                txtSalesManPWD.Focus()
                subGetPassword = False
                Exit Function
            End If
            subGetPassword = True
            PnlSalesManPwd.Visible = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "GoldPOS")
        End Try
    End Function
End Class