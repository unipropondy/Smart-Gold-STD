
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmTradeInSplit
    Dim dWeight1 As Double
    Dim dWeight2 As Double
    Dim dStoneWeight1 As Double
    Dim dStoneWeight2 As Double
    Dim dMcCost1 As Double
    Dim dMcCost2 As Double
    Dim dMcPrice1 As Double
    Dim dMcPrice2 As Double
    Dim dOriginalPMC As Double
    Dim dOriginalSMC As Double
    Dim dOriginalGoldLoss As Double
    Dim dOriginalTotalCost As Double
    Dim oTconn As ADODB.Connection
    Dim dsT As DataSet
    Dim dTotalCost1 As Double
    Dim dTotalcost2 As Double
    Dim sTrayId As String
    Dim sCatalogueCode As String
    Dim sCategoryCode As String
    Dim dCostPerGram As Double
    Dim dPMC As Double
    Dim dSMC As Double
    Dim dPCS As Double
    Dim dGoldLoss As Double
    Dim dStoneWeight As Double
    Dim sJobsheetNo As String
    Dim sPurchaseBillNo As String
    Dim dPurchaseDate As Date
    Dim dCreateDate As Date
    Dim sSupplierCode As String
    Dim sSalesBillNo As String
    Dim sDiamondDescription As String
    Dim dTotalCost As Double
    Dim sTotalCost As Double
    Dim objTII As New ClsTradeInIssue
    Public oTSalesConn As New ADODB.Connection
    Public oTSalesRs As New ADODB.Recordset

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnTradeBillNoSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnTradeBillNoSearch.Click
        Try
            Dim frmInvHelp As New frmHelpSearch(0, 0, "tblTradeMergeInventoryheader", "L", 4, "TradeInInventoryCode", " QtyOnHand <> 0 ", "TradeInInventoryCode", "TradeInInventoryCode", 80, "TotalWeight", "TotalWeight", 80, "TotalStWeight", "TotalStWeight", 80, "TotalPrice", "TotalPrice", 80)
            frmInvHelp.ShowDialog()
            txtInventoryCode.Text = frmInvHelp.Var1.Trim
            txtStWeight.Text = frmInvHelp.Var4.Trim
            txtWeight.Text = frmInvHelp.Var2.Trim
            sTotalCost = frmInvHelp.Var3.Trim
            txtSplitWeight.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnSplit_Click(sender As System.Object, e As System.EventArgs) Handles btnSplit.Click
        Try
            If Val(txtSplitWeight.Text.Trim) > 0 Then
                If Val(txtSplitWeight.Text.Trim) >= Val(txtSplitStWeight.Text.Trim) Then
                    subSplitWeight()
                    txtSplitInvCode.Text = objTII.GetNextTradeInventory
                Else
                    MessageBox.Show("Split weight shold not be greater than StoneWeight.")
                End If

            Else
                MessageBox.Show("Split weight shold be greater than Zero.")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub subSplitWeight()
        Dim dsSInv As New DataSet
        Dim dTemp As Double
        Dim icnt As Integer
        Dim objClsSplit As New ClsSliptInventory
        Try
            If Not (Val(txtSplitWeight.Text.Trim) < Val(txtWeight.Text.Trim)) Then
                MessageBox.Show("Split Weight should be less then Inventory Weight.")
                Exit Sub
                txtSplitWeight.Focus()
            End If
            dsSInv = objClsSplit.GetTradeInSplitInventoryDetail(txtInventoryCode.Text.Trim)
            If dsSInv.Tables(0).Rows.Count > 0 Then
                If chkScrap.Checked = False Then
                    dWeight1 = Val(txtWeight.Text.Trim) - Val(txtSplitWeight.Text.Trim)
                    txtOriginalInvWeight.Text = dWeight1
                    dWeight2 = Val(txtSplitWeight.Text.Trim)
                    txtSplitInvWeight.Text = Val(txtSplitWeight.Text.Trim)
                    dTemp = sTotalCost / Val(txtWeight.Text.Trim)
                    dTotalCost1 = dTemp * dWeight1
                    dTotalcost2 = sTotalCost - dTotalCost1
                    dWeight1 = Val(txtWeight.Text.Trim) - Val(txtSplitWeight.Text.Trim)
                Else
                    dWeight1 = Val(txtWeight.Text.Trim)
                    txtOriginalInvWeight.Text = dWeight1
                    dWeight2 = Val(txtSplitWeight.Text.Trim)
                    txtSplitInvWeight.Text = Val(txtSplitWeight.Text.Trim)
                    txtSplitStWeight.Text = Val(txtSplitWeight.Text.Trim)
                End If
            End If

                '    If Trim$(dsSInv.Tables(0).Rows(0)("CatalogueCode")) <> "" Then
                '        sCatalogueCode = Trim(dsSInv.Tables(0).Rows(0)("CatalogueCode"))
                '        sCategoryCode = Trim(dsSInv.Tables(0).Rows(0)("CategoryCode"))
                '        sClassCode = Trim(dsSInv.Tables(0).Rows(0)("ClassCode"))
                '        '''  If dsSInv.Tables(1).Rows.Count > 0 Then txtSplitInvCode.Text = CStr(dsSInv.Tables(1).Rows(0)("NextInventoryCode"))
                '        ''' 
                '        DS = objClsInvMerge.GetCategory(sClassCode)
                '        If DS.Tables(0).Rows.Count > 0 Then
                '            For icnt = 0 To DS.Tables(0).Rows.Count - 1
                '                cboCategory.Items.Add(Trim(DS.Tables(0).Rows(icnt)("categorycode") & ""))
                '            Next
                '        End If
                '        cboCategory.SelectedIndex = 0

                '        DS = objClsInvMerge.GetCategory(sClassCode)
                '        If DS.Tables(0).Rows.Count > 0 Then
                '            For icnt = 0 To DS.Tables(0).Rows.Count - 1
                '                cboOrgCategory.Items.Add(Trim(DS.Tables(0).Rows(icnt)("categorycode") & ""))
                '            Next
                '        End If
                '        cboOrgCategory.SelectedIndex = 0
                '        cboOrgCategory.Text = Trim(dsSInv.Tables(0).Rows(0)("CategoryCode"))


                '        DS = objClsInvMerge.GetCatalogue(sCategoryCode)
                '        If DS.Tables(0).Rows.Count > 0 Then
                '            For icnt = 0 To DS.Tables(0).Rows.Count - 1
                '                cboOrgCatalogue.Items.Add(Trim(DS.Tables(0).Rows(icnt)("CatalogueCode") & ""))
                '            Next
                '        End If
                '        cboOrgCatalogue.SelectedIndex = 0
                '        cboOrgCatalogue.Text = Trim(dsSInv.Tables(0).Rows(0)("CatalogueCode"))

                '    End If
                'Else
                '    MsgBox("Invalid Split Option for this inventory")
                '    txtInventoryCode.Text = ""
                '    Exit Sub
                'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        subSave()
        MessageBox.Show("Updated sucessfully")
        btnClear_Click(sender, e)
    End Sub
    Private Sub subSave()
        Try
            If txtSplitInvCode.Text.Trim = "" Then
                MessageBox.Show("The split InventoryCode should not be blank.")
                txtSplitInvCode.Focus()
                Exit Sub
            End If
            If Val(txtOriginalInvWeight.Text.Trim) = 0 Or Val(txtSplitInvWeight.Text.Trim) = 0 Then
                MessageBox.Show("Invalid weight entered.")
                txtSplitInvWeight.Focus()
                Exit Sub
            End If
            If fncInventorySplitSave() Then
            Else
                MessageBox.Show("Error in save")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Function fncInventorySplitSave() As Boolean
        Dim bSaveStatus As Boolean
        Dim bPurchase As Boolean
        Dim iRet As Boolean
        Dim sError As String
        Dim objClsSplit As New ClsSliptInventory
        bSaveStatus = False
        Try
            If chkScrap.Checked Then
                txtPurity.Text = "S"
            Else
                txtPurity.Text = ""
            End If
            If Not objClsSplit.SplitTradeInInventorySave(Trim(txtInventoryCode.Text), Trim(txtPurity.Text), _
                    Val(txtWeight.Text.Trim), Val(dOriginalTotalCost), _
                    Val(txtOriginalInvWeight.Text), Trim(txtSplitInvCode.Text), _
                     Val(txtSplitInvWeight.Text), Val(dTotalCost)) Then
                Exit Function
            End If
            iRet = objTII.UpdateTradeInMergeInventory(oTSalesConn, oTSalesRs, Trim(txtSplitInvCode.Text), Val(txtStWeight.Text.Trim), Val(txtStWeight.Text.Trim), _
                                                        Val(txtPurity.Text), 0, Val(dTotalcost2), sUserID, sError)

            objTII.UpdateTradeInInventory()
            If Trim(sError) <> "" Then
                MessageBox.Show("Error in save")
                Exit Function
            End If
            bSaveStatus = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            fncInventorySplitSave = bSaveStatus
        End Try
    End Function

    Private Sub chkScrap_Click(sender As Object, e As System.EventArgs) Handles chkScrap.Click
        If chkScrap.Checked Then
            txtSplitWeight.Text = Val(txtStWeight.Text)
            txtSplitWeight.Enabled = False
        Else
            txtSplitWeight.Text = 0
            txtSplitWeight.Enabled = True
        End If
    End Sub

    Private Sub Label9_Click(sender As System.Object, e As System.EventArgs) Handles Label9.Click

    End Sub

    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        txtInventoryCode.Text = ""
        txtOriginalInvWeight.Text = ""
        txtPurity.Text = ""
        txtSplitInvCode.Text = ""
        txtSplitInvWeight.Text = ""
        txtSplitStWeight.Text = ""
        txtSplitWeight.Text = ""
        txtStWeight.Text = ""
        txtWeight.Text = ""
        chkScrap.Checked = False
    End Sub
End Class