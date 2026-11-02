Public Class frmTradeInMerge
    Dim SInvoiceNo As String
    Dim objTIS As New ClsTradeinSales
    Dim sInvNo As String
    Dim sInventoryCode As String
    Dim sInvCode As String
    Dim dABal As Double
    Dim dAIssueBal As Double
    Dim objTII As New ClsTradeInIssue
    Dim dSelectWeight As Double
    Dim dSelectStWeight As Double
    Dim dSelectCost As Double
    Public oTSalesConn As New ADODB.Connection
    Public oTSalesRs As New ADODB.Recordset
    Dim IbLoad As Boolean
    Private Sub txtTotalWt_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtTotalWt.TextChanged

    End Sub
    Private Sub Label12_Click(sender As System.Object, e As System.EventArgs) Handles Label12.Click

    End Sub

    Private Sub btnTradeBillNoSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnTradeBillNoSearch.Click
        Try
            lblSelweight.Text = "0.00"
            If Val(cboPurity.Text) = 0 Then
                MessageBox.Show("Purity Should not be Empty!")
                cboPurity.Focus()
                Exit Sub
            End If
            'If Val(txtPricePerGram.Text) = 0 Then
            '    MessageBox.Show("Cost Per Gram Should not be Empty!")
            '    txtPricePerGram.Focus()
            '    Exit Sub
            'End If
            subGetTradeInInvoices()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub subGetTradeInInvoices()
        Try
            Dim icnt As Long
            Dim sDate As Date
            Dim dsPur As New DataSet
            Dim sFromDate As String
            Dim sToDate As String
            'Dim SBll As Purchase
            'SBll = New Purchase
            pnlInvoice.Visible = True
            sFromDate = txtFYY.Text.Trim & txtFMM.Text.Trim & txtFDD.Text.Trim
            sToDate = txtTYY.Text.Trim & txtTMM.Text.Trim & txtTDD.Text.Trim
            dsPur = objTIS.GetTradeInInvoice(SInvoiceNo, Trim(cboPurity.Text), Trim(sFromDate), Trim(sToDate))
            If dsPur.Tables(0).Rows.Count > 0 Then
                '
                grdInvoice.RowCount = 0
                If Not IsNothing(dsPur) Then
                    If dsPur.Tables(0).Rows.Count > 0 Then
                        For icnt = 0 To dsPur.Tables(0).Rows.Count - 1
                            grdInvoice.Rows.Add()
                            Call fnSetdgvText(grdInvoice, icnt, 0, False)
                            fnSetdgvText(grdInvoice, icnt, 1, Trim(dsPur.Tables(0).Rows(icnt)("InvoiceNo") & ""))
                            fnSetdgvText(grdInvoice, icnt, 2, Trim(dsPur.Tables(0).Rows(icnt)("InvoiceDate") & ""))
                            fnSetdgvText(grdInvoice, icnt, 3, Trim(dsPur.Tables(0).Rows(icnt)("InventoryCode") & ""))
                            fnSetdgvText(grdInvoice, icnt, 4, Trim(dsPur.Tables(0).Rows(icnt)("Weight") & ""))
                            fnSetdgvText(grdInvoice, icnt, 5, Trim(dsPur.Tables(0).Rows(icnt)("StoneWeight") & ""))
                            fnSetdgvText(grdInvoice, icnt, 6, Trim(dsPur.Tables(0).Rows(icnt)("TotalCost") & ""))
                            fnSetdgvText(grdInvoice, icnt, 7, Trim(dsPur.Tables(0).Rows(icnt)("Weight") & ""))
                            fnSetdgvText(grdInvoice, icnt, 8, Trim(dsPur.Tables(0).Rows(icnt)("CustomerCode") & ""))

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
        'sInvNo = ""
        'sInvCode = ""

        'For i = 0 To grdInvoice.RowCount - 1
        '    If Trim(fnGetdgvText(grdInvoice, i, 0)) = True Then
        '        ''SInvoiceNo = SInvoiceNo & fncFormatSQLText(GetText(grdInvoice, i, 2)) & ","
        '        SInvoiceNo = SInvoiceNo & "," & fncFormatSQLText(fnGetdgvText(grdInvoice, i, 1))
        '        sInvNo = sInvNo & "," & fncFormatSQLText(fnGetdgvText(grdInvoice, i, 1))
        '        sInventoryCode = sInventoryCode & "," & fncFormatSQLText(fnGetdgvText(grdInvoice, i, 2))
        '        sInvCode = sInvCode & "," & fncFormatSQLText(fnGetdgvText(grdInvoice, i, 2))
        '    End If
        'Next
        ''SInvoiceNo = Strings.Left(SInvoiceNo, Len(SInvoiceNo) - 1)
        ''If IbLoad Then
        ''    SInvoiceNo = SInvoiceNo
        ''    sInvNo = Strings.Right(sInvNo, Len(sInvNo) - 1)
        ''    sInventoryCode = sInventoryCode
        ''    sInvCode = Strings.Right(sInvNo, Len(sInvCode) - 1)
        ''Else
        'SInvoiceNo = Strings.Right(SInvoiceNo, Len(SInvoiceNo) - 1)
        'sInvNo = Strings.Right(sInvNo, Len(sInvNo) - 1)
        'sInventoryCode = Strings.Right(sInventoryCode, Len(sInventoryCode) - 1)
        'sInvCode = Strings.Right(sInvCode, Len(sInvCode) - 1)
        ''End If
        ''subGetTradeBillNo()
        'fncAddInvoices()
        'subAddTradeInToGrid()
        'pnlInvoice.Visible = False
        '  Dim i As Long
        '  SInvoiceNo = ""
        'sInvNo = ""
        'sInvCode = ""

        'For i = 0 To grdInvoice.RowCount - 1
        '    If Trim(fnGetdgvText(grdInvoice, i, 0)) = True Then
        '        ''SInvoiceNo = SInvoiceNo & fncFormatSQLText(GetText(grdInvoice, i, 2)) & ","
        '        SInvoiceNo = SInvoiceNo & "," & fncFormatSQLText(fnGetdgvText(grdInvoice, i, 1))
        '        sInvNo = sInvNo & "," & fncFormatSQLText(fnGetdgvText(grdInvoice, i, 1))
        '        sInventoryCode = sInventoryCode & "," & fncFormatSQLText(fnGetdgvText(grdInvoice, i, 2))
        '        sInvCode = sInvCode & "," & fncFormatSQLText(fnGetdgvText(grdInvoice, i, 2))
        '    End If
        'Next
        ''SInvoiceNo = Strings.Left(SInvoiceNo, Len(SInvoiceNo) - 1)
        'If IbLoad Then
        '    SInvoiceNo = SInvoiceNo
        '    sInvNo = Strings.Right(sInvNo, Len(sInvNo) - 1)
        '    sInventoryCode = sInventoryCode
        '    sInvCode = Strings.Right(sInvNo, Len(sInvCode) - 1)
        'Else
        '    SInvoiceNo = Strings.Right(SInvoiceNo, Len(SInvoiceNo) - 1)
        '    sInvNo = Strings.Right(sInvNo, Len(sInvNo) - 1)
        '    sInventoryCode = Strings.Right(sInventoryCode, Len(sInventoryCode) - 1)
        '    sInvCode = Strings.Right(sInvCode, Len(sInvCode) - 1)
        'End If
        ''subGetTradeBillNo()
        'fncAddInvoices()
        'subAddTradeInToGrid()
        'pnlInvoice.Visible = False

        txtTotalWt.Text = Val(lblSelweight.Text)
        txtTotalSTWeight.Text = Val(lblSelStWeight.Text)
        txtTotalPrice.Text = Val(lblSelCost.Text) ' Val(txtTotalWt.Text) * Val(txtPricePerGram.Text)
        txtPricePerGram.Text = Val(lblSelCost.Text) / (Val(lblSelweight.Text) - Val(lblSelStWeight.Text))
        txtTradeInventoryCode.Text = objTII.GetNextTradeInventory
    End Sub
    Private Function fncAddInvoices() As Boolean
        Dim iCnt As Long
        Dim bStatus As Boolean
        Dim sInvoiceNo, sInventoryCode, sWeight, sIssueWeight, sBalanceWeight, sInvoiceDate
        Dim dr As DataRow
        Try
            bStatus = False
            For iCnt = 0 To grdInvoice.RowCount - 1
                If Trim(fnGetdgvText(grdInvoice, iCnt, 0)) = True Then
                    sInvoiceNo = Trim(fnGetdgvText(grdInvoice, iCnt, 1))
                    sInvoiceDate = Trim(fnGetdgvText(grdInvoice, iCnt, 2))
                    sInventoryCode = Trim(fnGetdgvText(grdInvoice, iCnt, 3))
                    sWeight = Trim(fnGetdgvText(grdInvoice, iCnt, 4))
                    sIssueWeight = Trim(fnGetdgvText(grdInvoice, iCnt, 5))
                    sBalanceWeight = Trim(fnGetdgvText(grdInvoice, iCnt, 6))


                    dr = dsTradeIn.Tables(0).NewRow()
                    dr.Item("sInvoiceNo") = Trim(sInvoiceNo)
                    dr.Item("sInvoiceDate") = Trim(sInvoiceDate)
                    dr.Item("sInventoryCode") = Trim(sInventoryCode)
                    dr.Item("sWeight") = Trim(sWeight)
                    dr.Item("sIssueWeight") = Trim(sIssueWeight)
                    dr.Item("sBalanceWeight") = Trim(sBalanceWeight)
                    dsTradeIn.Tables(0).Rows.Add(dr)
                End If
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncAddInvoices = bStatus
        End Try
    End Function
    Private Sub subSetDataTable()
        Dim dt As New DataTable
        dt = New DataTable
        dt.Columns.Add(New DataColumn("sInvoiceNo", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("sInvoiceDate", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("sInventoryCode", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("sWeight", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("sIssueWeight", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("sBalanceWeight", Type.GetType("System.String")))
        dsTradeIn.Tables.Add(dt)
    End Sub
    Private Sub subAddTradeInToGrid()
        Dim sInvoiceNo As String
        Dim sInventoryCode As String
        Dim sWeight As String
        Dim sIssueWeight As String
        Dim sBalanceWeight As String
        Dim sInvoiceDate As String
        Dim iRow As Long
        Dim i As Long

        Try
            dTradeInSales = 0
            For iRow = 0 To dsTradeIn.Tables(0).Rows.Count - 1
  
                sInvoiceNo = dsTradeIn.Tables(0).Rows(iRow).Item("sInvoiceNo") & "".Trim
                sInvoiceDate = dsTradeIn.Tables(0).Rows(iRow).Item("sInvoiceDate") & "".Trim
                sInventoryCode = dsTradeIn.Tables(0).Rows(iRow).Item("sInventoryCode") & "".Trim
                sWeight = dsTradeIn.Tables(0).Rows(iRow).Item("sWeight") & "".Trim
                sIssueWeight = dsTradeIn.Tables(0).Rows(iRow).Item("sIssueWeight") & "".Trim
                sBalanceWeight = dsTradeIn.Tables(0).Rows(iRow).Item("sBalanceWeight") & "".Trim
                sBalanceWeight = Val(sIssueWeight)
                dABal = dABal + Val(sBalanceWeight)
                dAIssueBal = dAIssueBal + Val(sIssueWeight)

            Next iRow
            
            txtTotalWt.Text = Format(Val(dAIssueBal), "0.00")
            txtTotalWt.Text = Format(CDbl(txtTotalWt.Text), "0.00")
            txtTradeInventoryCode.Text = objTII.GetNextTradeIssueNo
            txtTradeBillNo.Text = ""
            txtTradeBillNo.Focus()
            dsTradeIn.Tables(0).Rows.Clear()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub grdInvoice_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdInvoice.CellClick
        Dim i As Long
        dSelectWeight = 0
        dSelectCost = 0
        dSelectStWeight = 0
        If (fnGetdgvText(grdInvoice, grdInvoice.CurrentRow.Index, 0)) = False Then
            Call fnSetdgvText(grdInvoice, grdInvoice.CurrentRow.Index, 0, True)
            '    dSelectWeight = dSelectWeight + Val(fnGetdgvText(grdInvoice, grdInvoice.CurrentRow.Index, 5))
        Else
            Call fnSetdgvText(grdInvoice, grdInvoice.CurrentRow.Index, 0, False)
            '   dSelectWeight = dSelectWeight - Val(fnGetdgvText(grdInvoice, grdInvoice.CurrentRow.Index, 5))
        End If
        lblSelweight.Text = Val(dSelectWeight)
        lblSelCost.Text = Val(dSelectCost)
        lblSelStWeight.Text = Val(dSelectStWeight)
        For i = 0 To grdInvoice.RowCount - 1
            If (fnGetdgvText(grdInvoice, i, 0)) = True Then
                dSelectWeight = dSelectWeight + Val(fnGetdgvText(grdInvoice, i, 4))
                dSelectCost = dSelectCost + Val(fnGetdgvText(grdInvoice, i, 6))
                dSelectStWeight = dSelectStWeight + Val(fnGetdgvText(grdInvoice, i, 5))
            End If
        Next
        lblSelweight.Text = Val(dSelectWeight)
        lblSelCost.Text = Val(dSelectCost)
        lblSelStWeight.Text = Val(dSelectStWeight)
    End Sub

    Private Sub btnPanelClose_Click(sender As System.Object, e As System.EventArgs) Handles btnPanelClose.Click
        grdInvoice.RowCount = 0
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub btnBillClear_Click(sender As System.Object, e As System.EventArgs) Handles btnBillClear.Click
        grdInvoice.RowCount = 0
        txtTotalWt.Text = 0
        txtTotalSTWeight.Text = 0
        txtTotalPrice.Text = 0
        txtPricePerGram.Text = 0
        txtTradeInventoryCode.Text = ""
    End Sub

    Private Sub btnProceed_Click(sender As System.Object, e As System.EventArgs) Handles btnProceed.Click
        Dim sError As String
        If Not fncValidation() Then Exit Sub
        Dim iRet As Boolean
        Dim dsTH As New DataSet
        Dim i As Long
        For i = 0 To grdInvoice.RowCount - 1
            If fnGetdgvText(grdInvoice, i, 0) = True Then


                iRet = objTII.UpdateTradeInInventoryDetail(oTSalesConn, oTSalesRs, Trim(txtTradeInventoryCode.Text), fnGetdgvText(grdInvoice, i, 3), fnGetdgvText(grdInvoice, i, 4), fnGetdgvText(grdInvoice, i, 5), sError)


                If Trim(sError) <> "" Then
                    MessageBox.Show("Error in save")
                    Exit Sub
                End If
                dsTH = Nothing
            End If
        Next

        iRet = objTII.UpdateTradeInMergeInventory(oTSalesConn, oTSalesRs, Trim(txtTradeInventoryCode.Text), Val(txtTotalWt.Text.Trim), Val(txtTotalSTWeight.Text.Trim), _
                                                           Val(cboPurity.Text), Val(txtPricePerGram.Text.Trim), Val(txtTotalPrice.Text.Trim), sUserID, sError)
        If Trim(sError) <> "" Then
            MessageBox.Show("Error in save")
            Exit Sub
        End If
        MessageBox.Show("Tradein Gold Issued Sucessfully")
        ' subPrint(txtTradeIssueNo.Text.Trim)
        objTII.UpdateTradeInInventory()
        '  subClearMe()
        btnBillClear_Click(sender, e)
    End Sub

    Function fncValidation() As Boolean
        Dim bStatus As Boolean
        Dim dsSupp As New DataSet
        Try
            bStatus = False
            
            If Val(txtTotalWt.Text) = 0 Then
                MessageBox.Show("Total Weight Should Not Be 0 !!! ")
                Exit Function
            End If
            'If Val(txtTotalSTWeight.Text) = 0 Then
            '    MessageBox.Show("Total StoneWeight Should Not Be 0 !!! ")
            '    Exit Function
            'End If
            If Trim(txtTradeInventoryCode.Text) = "" Then
                MessageBox.Show("TradeInMerge Inventory Should not be Empty")
                Exit Function
            End If

            
            bStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncValidation = bStatus
        End Try
    End Function

    Private Sub frmTradeInMerge_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
                    btnBillClear_Click(Nothing, Nothing)
                Case Keys.F7
                    btnProceed_Click(Nothing, Nothing)
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub frmTradeInMerge_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        txtFYY.Text = Now.Year
        txtFMM.Text = Format(Now.Month, "00")
        txtFDD.Text = Format(Now.Day, "00")
        txtTYY.Text = Now.Year
        txtTMM.Text = Format(Now.Month, "00")
        txtTDD.Text = Format(Now.Day, "00")
        subSetDataTable()
        IbLoad = False
    End Sub

    Private Sub chkSelectAll_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkSelectAll.CheckedChanged
        Dim i As Long
        For i = 0 To grdInvoice.RowCount - 1
            'If Trim(GetText(grdInvoice, i, 1)) = 1 Then
            '    Call SetText(grdInvoice, i, 1, 0)
            'Else
            If chkSelectAll.Checked = True Then
                Call fnSetdgvText(grdInvoice, i, 0, True)
                dSelectWeight = dSelectWeight + Val(fnGetdgvText(grdInvoice, i, 4))
                dSelectStWeight = dSelectStWeight + Val(fnGetdgvText(grdInvoice, i, 5))
                dSelectCost = dSelectCost + Val(fnGetdgvText(grdInvoice, i, 6))
            Else
                Call fnSetdgvText(grdInvoice, i, 0, False)
                dSelectWeight = 0
                dSelectCost = 0
                dSelectStWeight = 0
            End If
            'End If
        Next
        lblSelweight.Text = Val(dSelectWeight)
        lblSelStWeight.Text = Val(dSelectStWeight)
        lblSelCost.Text = Val(dSelectCost)
    End Sub
End Class